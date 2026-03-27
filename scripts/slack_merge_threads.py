#!/usr/bin/env python3
import sys, io
sys.stdout = io.TextIOWrapper(sys.stdout.buffer, encoding='utf-8', errors='replace', line_buffering=True)
"""One-time migration: merge consecutive same-author messages in 정반합 Slack threads.
Run after Slack message limit resets (usually midnight).

Usage: python3 scripts/slack_merge_threads.py [--dry-run] [--limit 100]
"""
import json, time, sys
from urllib.request import Request, urlopen

cfg = json.load(open('W:/SDK/bin/wkappbot.hq/profiles/slack_exp/webhook.json', encoding='utf-8'))
TOKEN = cfg['bot_token']
CHANNEL = cfg['channel']

DRY_RUN = '--dry-run' in sys.argv
LIMIT = 100
for i, a in enumerate(sys.argv):
    if a == '--limit' and i + 1 < len(sys.argv):
        LIMIT = int(sys.argv[i + 1])

def api(method, **kwargs):
    url = f'https://slack.com/api/{method}'
    data = json.dumps(kwargs).encode()
    req = Request(url, data, {'Content-Type': 'application/json', 'Authorization': f'Bearer {TOKEN}'})
    try:
        resp = json.loads(urlopen(req).read().decode('utf-8'))
    except Exception as e:
        if '429' in str(e):
            print(f'  HTTP 429 — waiting 5s...')
            time.sleep(5)
            try:
                resp = json.loads(urlopen(req).read().decode('utf-8'))
            except:
                return {'ok': False, 'error': 'ratelimited'}
        else:
            return {'ok': False, 'error': str(e)}
    if not resp.get('ok') and resp.get('error') == 'ratelimited':
        print(f'  Ratelimited, waiting 5s...')
        time.sleep(5)
        return api(method, **kwargs)
    time.sleep(1.5)  # global throttle between ALL API calls
    return resp

def get_author(msg):
    """Get consistent author identifier from message."""
    return msg.get('username') or msg.get('user') or msg.get('bot_id') or ''

def get_icon(msg):
    """Get profile icon emoji from message."""
    icons = msg.get('icons', {})
    return icons.get('emoji', '')

def smart_time(prev_ts, curr_ts):
    """Smart timestamp: only show differing time units vs previous."""
    from datetime import datetime
    try:
        prev = datetime.fromtimestamp(float(prev_ts))
        curr = datetime.fromtimestamp(float(curr_ts))
        full = curr.strftime('%m-%d %H:%M:%S')
        prev_full = prev.strftime('%m-%d %H:%M:%S')
        # Left-trim common prefix at delimiter boundary
        common = 0
        while common < len(prev_full) and common < len(full) and prev_full[common] == full[common]:
            common += 1
        if common == 0:
            return full
        if common >= len(full):
            return ':' + full[-2:]
        # Back up to delimiter
        delims = ':-  '
        cut = common
        while cut > 0 and full[cut-1] not in delims:
            cut -= 1
        result = full[cut:] if cut > 0 else full
        # Units suffix
        if ':' not in result and '-' not in result and ' ' not in result:
            if prev.minute != curr.minute:
                # Trim :ss, add m
                return result.split(':')[0] + 'm' if ':' in full[cut:] else result + 'm'
            return result + 's'
        # Trim trailing :ss if minutes+ changed
        if len(result) >= 5 and result[-3] == ':':
            trimmed = result[:-3]
            if ':' not in trimmed and '-' not in trimmed and ' ' not in trimmed:
                return trimmed + 'm'
            return trimmed
        return result
    except:
        from datetime import datetime as dt
        return dt.now().strftime('%H:%M')

def merge_thread(thread_ts):
    """Merge consecutive same-author messages in a thread. Returns merge count."""
    replies = api('conversations.replies', channel=CHANNEL, ts=thread_ts, limit=200)
    msgs = replies.get('messages', [])[1:]  # skip thread starter
    if len(msgs) < 2:
        return 0

    # Bottom-up scan: start from end, merge same-author pairs upward.
    # Less confusing — indices don't shift below current position.
    merged = 0
    i = len(msgs) - 1
    while i >= 1 and merged < LIMIT:
        curr = msgs[i]      # current (lower)
        prev = msgs[i - 1]  # previous (upper) — merge target

        ca = get_author(curr)
        pa = get_author(prev)
        cf = curr.get('files', [])
        ct = curr.get('text', '')
        pt = prev.get('text', '')

        # Same bot author, no files on current → merge into previous
        if ca and ca == pa and not cf:
            icon = get_icon(curr) or get_icon(prev)
            sep_icon = f'{icon} ' if icon else ''
            time_mark = smart_time(prev.get('ts', ''), curr.get('ts', ''))
            separator = f'\n{sep_icon}*{ca}* ━━ {time_mark} ━━\n'
            combined = pt + separator + ct
            if len(combined) > 3800:
                i -= 1  # too long → skip
                continue
            if DRY_RUN:
                print(f'  [DRY] Would merge: {ca[:25]} ({len(pt)}+{len(ct)}ch)')
                msgs.pop(i)
                msgs[i - 1] = {**prev, 'text': combined}
                merged += 1
                # don't decrement i — re-check same position (prev may chain-merge)
                continue

            upd = api('chat.update', channel=CHANNEL, ts=prev['ts'], text=combined)
            if upd.get('ok'):
                time.sleep(0.8)
                dlt = api('chat.delete', channel=CHANNEL, ts=curr['ts'])
                if dlt.get('ok'):
                    msgs.pop(i)
                    msgs[i - 1] = {**prev, 'text': combined}
                    merged += 1
                    print(f'  Merged+deleted: {ca[:25]} ({len(pt)}+{len(ct)}ch)')
                    # don't decrement — re-check for chain merge
                    continue
                else:
                    print(f'  Delete fail: {dlt.get("error")}')
                time.sleep(0.8)
            else:
                err = upd.get('error', '')
                print(f'  Update fail: {err}')
                if err == 'message_limit_exceeded':
                    print('  Message limit! Stopping.')
                    return merged
        i -= 1

    return merged

# Find all 정반합 threads
print(f'Scanning channel {CHANNEL} for 정반합 threads...')
print(f'Mode: {"DRY-RUN" if DRY_RUN else "LIVE"}, limit={LIMIT}')

cursor = None
total_merged = 0
threads_processed = 0

while True:
    kwargs = dict(channel=CHANNEL, limit=50)
    if cursor:
        kwargs['cursor'] = cursor
    hist = api('conversations.history', **kwargs)

    for msg in hist.get('messages', []):
        text = msg.get('text', '')
        rc = msg.get('reply_count', 0)
        # Only 정반합 threads with replies
        if rc < 2:
            continue
        if '정반합' not in text and 'TRIAD' not in text and 'debate' not in text.lower() and '클롣' not in text:
            continue

        ts = msg['ts']
        preview = text[:50].replace('\n', ' ')
        print(f'\nThread [{rc} replies]: {preview}...')
        count = merge_thread(ts)
        total_merged += count
        threads_processed += 1
        if total_merged >= LIMIT:
            break

    if total_merged >= LIMIT:
        break
    cursor = hist.get('response_metadata', {}).get('next_cursor')
    if not cursor:
        break

print(f'\n=== Done! {threads_processed} threads, {total_merged} messages merged ===')
