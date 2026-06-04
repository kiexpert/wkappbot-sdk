#!/usr/bin/env python3
"""Fast agent model detection via ctypes (target <100ms vs 2900ms .ps1)

Resolution order:
1. env WKHARNESS_AGENT_MODEL (override)
2. Walk parent chain, check --model flag in cmdlines
3. Read ~/.claude/wkharness/active-session-<slug>.json (8h staleness)
4. Return 'ROOT' if nothing found

Output: stdout=model name, stderr=pid+cmdline stats
"""
import os
import sys
import json
import time
import ctypes
import re
from ctypes import c_void_p, c_int, c_uint, c_char_p, c_wchar_p, POINTER, Structure, pointer
from pathlib import Path
from datetime import datetime, timedelta

start_time = time.time()

class PROCESSENTRY32(Structure):
    _fields_ = [
        ("dwSize", c_uint),
        ("cntUsage", c_uint),
        ("th32ProcessID", c_uint),
        ("th32ParentProcessID", c_uint),
        ("th32MemoryBase", c_uint),
        ("th32ModuleID", c_uint),
        ("cntThreads", c_uint),
        ("pcPriClassBase", c_int),
        ("dwFlags", c_uint),
        ("szExeFile", ctypes.c_char * 260),
    ]

class UNICODE_STRING(Structure):
    _fields_ = [("Length", c_uint), ("MaximumLength", c_uint), ("Buffer", c_wchar_p)]

class PROCESS_BASIC_INFORMATION(Structure):
    _fields_ = [("Reserved1", c_void_p), ("PebBaseAddress", c_void_p),
                ("Reserved2", c_void_p * 2), ("UniqueProcessId", c_void_p), ("Reserved3", c_void_p)]

TH32CS_SNAPPROCESS = 0x00000002
PROCESS_QUERY_INFORMATION = 0x0400
PROCESS_VM_READ = 0x0010

kernel32 = ctypes.windll.kernel32
ntdll = ctypes.windll.ntdll

CreateToolhelp32Snapshot = kernel32.CreateToolhelp32Snapshot
CreateToolhelp32Snapshot.argtypes = [c_uint, c_uint]
CreateToolhelp32Snapshot.restype = c_void_p

Process32First = kernel32.Process32First
Process32First.argtypes = [c_void_p, POINTER(PROCESSENTRY32)]
Process32First.restype = ctypes.c_bool

Process32Next = kernel32.Process32Next
Process32Next.argtypes = [c_void_p, POINTER(PROCESSENTRY32)]
Process32Next.restype = ctypes.c_bool

OpenProcess = kernel32.OpenProcess
OpenProcess.argtypes = [c_uint, ctypes.c_bool, c_uint]
OpenProcess.restype = c_void_p

CloseHandle = kernel32.CloseHandle
CloseHandle.argtypes = [c_void_p]
CloseHandle.restype = ctypes.c_bool

GetCurrentProcessId = kernel32.GetCurrentProcessId
GetCurrentProcessId.restype = c_uint

NtQueryInformationProcess = ntdll.NtQueryInformationProcess
NtQueryInformationProcess.argtypes = [c_void_p, c_int, c_void_p, c_uint, POINTER(c_uint)]
NtQueryInformationProcess.restype = c_int

ReadProcessMemory = kernel32.ReadProcessMemory
ReadProcessMemory.argtypes = [c_void_p, c_void_p, c_void_p, c_uint, POINTER(c_uint)]
ReadProcessMemory.restype = ctypes.c_bool

MODEL_PAT = re.compile(
    r'(?i)(?:(?:^|\s)--model(?=[\s=])|(?:^|\s)-m(?:odel)?\b)[\s=]+([A-Za-z0-9][A-Za-z0-9._\-]*)',
    re.IGNORECASE
)

AI_MODELS = {'haiku', 'sonnet', 'opus', 'gpt', 'gemini', 'claude',
             'claude-haiku-4-5-20251001', 'claude-sonnet-4-6', 'claude-opus-4-8'}


def get_process_cmdline(pid):
    try:
        handle = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_READ, False, pid)
        if not handle:
            return None
        try:
            pbi = PROCESS_BASIC_INFORMATION()
            ret_len = c_uint()
            status = NtQueryInformationProcess(handle, 0, pointer(pbi), ctypes.sizeof(PROCESS_BASIC_INFORMATION), pointer(ret_len))
            if status != 0:
                return None
            peb_addr = pbi.PebBaseAddress
            if not peb_addr:
                return None
            peb_data = (c_uint * 16)()
            bytes_read = c_uint()
            if not ReadProcessMemory(handle, peb_addr, pointer(peb_data), ctypes.sizeof(peb_data), pointer(bytes_read)):
                return None
            rtl_ptr_addr = c_void_p(int(peb_addr) + 0x20)
            rtl_ptr = c_void_p()
            if not ReadProcessMemory(handle, rtl_ptr_addr, pointer(rtl_ptr), 8, pointer(bytes_read)):
                return None
            cmd_offset = int(rtl_ptr.value) + 0x70
            cmd_str = UNICODE_STRING()
            if not ReadProcessMemory(handle, cmd_offset, pointer(cmd_str), ctypes.sizeof(UNICODE_STRING), pointer(bytes_read)):
                return None
            if not cmd_str.Buffer or cmd_str.Length == 0:
                return None
            cmd_buf = (ctypes.c_wchar * (cmd_str.Length // 2 + 1))()
            if not ReadProcessMemory(handle, cmd_str.Buffer, pointer(cmd_buf), cmd_str.Length, pointer(bytes_read)):
                return None
            return ''.join(cmd_buf[:cmd_str.Length // 2])
        finally:
            CloseHandle(handle)
    except:
        return None


def get_process_cmdline_fallback(pid):
    try:
        import subprocess
        result = subprocess.run(['wmic', 'process', 'where', f'ProcessId={pid}', 'get', 'CommandLine', '/format:value'],
                              capture_output=True, text=True, timeout=2)
        for line in result.stdout.split('\n'):
            if line.startswith('CommandLine='):
                return line[12:].strip()
        return None
    except:
        return None


def get_process_name(pid):
    try:
        snapshot = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0)
        if snapshot == -1:
            return None
        try:
            pe = PROCESSENTRY32()
            pe.dwSize = ctypes.sizeof(PROCESSENTRY32)
            if not Process32First(snapshot, pointer(pe)):
                return None
            while Process32Next(snapshot, pointer(pe)):
                if pe.th32ProcessID == pid:
                    return pe.szExeFile.decode('utf-8', errors='ignore') if pe.szExeFile else None
            return None
        finally:
            CloseHandle(snapshot)
    except:
        return None


def get_parent_chain():
    try:
        snapshot = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0)
        if snapshot == -1:
            return {}
        try:
            parent_map = {}
            pe = PROCESSENTRY32()
            pe.dwSize = ctypes.sizeof(PROCESSENTRY32)
            if not Process32First(snapshot, pointer(pe)):
                return {}
            while Process32Next(snapshot, pointer(pe)):
                parent_map[pe.th32ProcessID] = (pe.th32ParentProcessID, pe.szExeFile.decode('utf-8', errors='ignore') if pe.szExeFile else '')
            return parent_map
        finally:
            CloseHandle(snapshot)
    except:
        return {}


def extract_model(cmdline):
    if not cmdline:
        return None
    match = MODEL_PAT.search(cmdline)
    if match:
        model = match.group(1)
        if any(part in model.lower() for part in AI_MODELS) or model.startswith('claude-'):
            return model
    return None


def is_ai_process(name, cmdline, has_model=False):
    if not name:
        return False
    name_lower = name.lower()
    ai_exes = {'agent.cmd', 'agent.exe', 'wkclaude', 'wkclaude.cmd', 'wkopus', 'wksonnet', 'wkhaiku', 'codex', 'codex.exe'}
    if any(name_lower.endswith(exe) for exe in ai_exes):
        return True
    if has_model:
        return True
    return False


def get_active_session_model():
    try:
        wkharness_dir = Path.home() / '.claude' / 'wkharness'
        if not wkharness_dir.exists():
            return None
        for session_file in wkharness_dir.glob('active-session-*.json'):
            try:
                with open(session_file) as f:
                    data = json.load(f)
                    if 'model' in data:
                        ts_raw = data.get('ts', '') or data.get('timestamp', '')
                        if not ts_raw:
                            continue
                        ts = datetime.fromisoformat(ts_raw.replace('Z', '+00:00'))
                        if datetime.now() - ts < timedelta(hours=8):
                            return data['model']
            except:
                pass
        return None
    except:
        return None


def main():
    env_model = os.environ.get('WKHARNESS_AGENT_MODEL')
    if env_model:
        print(env_model, file=sys.stdout)
        elapsed = (time.time() - start_time) * 1000
        print(f'[{elapsed:.0f}ms]', file=sys.stderr)
        return

    current_pid = GetCurrentProcessId()
    parent_map = get_parent_chain()

    if not parent_map:
        print('ROOT', file=sys.stdout)
        elapsed = (time.time() - start_time) * 1000
        print(f'[{elapsed:.0f}ms]', file=sys.stderr)
        return

    current = current_pid
    chain = []
    seen = set()

    while current and current not in seen:
        seen.add(current)
        name = get_process_name(current)
        cmdline = get_process_cmdline(current)
        if not cmdline:
            cmdline = get_process_cmdline_fallback(current)
        model = extract_model(cmdline)
        if is_ai_process(name, cmdline, model is not None):
            chain.append((current, name, cmdline, model))
        if current not in parent_map:
            break
        current = parent_map[current][0]

    found_model = None
    for pid, name, cmdline, model in chain:
        if model:
            found_model = model
            break

    if not found_model:
        found_model = get_active_session_model()

    if found_model:
        print(found_model, file=sys.stdout)
    else:
        print('ROOT', file=sys.stdout)

    if chain:
        pid, name, cmdline, model = chain[0]
        truncated = cmdline[:77] + '...' if cmdline and len(cmdline) > 80 else cmdline or ''
        print(f'pid={pid} {truncated}', file=sys.stderr)

    elapsed = (time.time() - start_time) * 1000
    print(f'[{elapsed:.0f}ms]', file=sys.stderr)


if __name__ == '__main__':
    main()
