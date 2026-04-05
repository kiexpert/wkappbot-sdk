# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=1700  time=11:22:33  uptime=24420s  mem=1653MB  system_processes=354"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_112221.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 510-510-510-510-510-510-517-510-150-153-150-105-150-153-153 501-510-501-150-510-150-150-105-150-105-150-157-150-157-157-513-150-105-150-150-571-510 501-150-517-510-517-157-517-5_V.mp3)
  Segment 2: 1576ms ~ 11620ms (file: 732-236-263-237-320-764-137-231-267-135-312-321-713-312-102-213-267-130-102-103-236-203-132-210-615-267-216-627-102-123-102-312-310-762-273-627-627-267-237-102-716-267-260-263-2_W.mp3)
  Segment 3: 12620ms ~ 13196ms (file: 312-213 126-130-316-360-362-362-361-316-367-173-671-167-576-157-351-136-765-672-621-265-625-576 325-247-312-735-276-213-752-321-507-203-703-321-326-507-726-135-136-126-657-217-7_W.mp3)
  Segment 4: 14196ms ~ 14772ms (file: 137-130-173-137-107-137-130-153-752-731-371-103-301-710 103-307-307-705-317-103-130-107-170-107-301-107-130-715-371-135-307-137-103-137-173-735-173-715-170-103-137-137 310-107-3_V.mp3)
  Segment 5: 15772ms ~ 16348ms (file: 527-526-651-527-217-765-712-657-725-165 502-501-507-507-507-507-576-573_W.mp3)
  Segment 6: 17348ms ~ 17924ms (file: 761-132-163-126-137-231-230-203-263-260-362-326-625-632-625-316-162-675-362-106-106-106-562-625-620-160-620-670-751-702-712-172-123-106-135-132-163-162-163-125-571-627-652-103-2_V.mp3)
  Segment 7: 18924ms ~ 19500ms (file: 517-513-517-517-157-157-517 517-135-130-137-513-571-315-513-150-170-130-135-157-517-513-517-517-571-571-571-517-573-571-537 135-105-153-150-135-105-157-105-105-150-105-135-137-1_V.mp3)
  Segment 8: 20500ms ~ 21076ms (file: 547-574-547-574-574-574-574-574-576-574-574-574-574 574-574-547-547-574-547-547-547-574-576-574-756-567-756-765-756-756-576-675-675 547-574-576-567-546-567-756-765-765-756-756-7_W.mp3)
  Segment 9: 22076ms ~ 22652ms (file: 130-103-103-103-103-103 136-357-651-157-451-105-103-103-130-103-137-143-310-130-135-145-134-135-135-134-143-134-135-103-134 413-314-301-143-134-143-104-134-143-310-130-134-315-1_V.mp3)
  Segment 10: 23652ms ~ 30996ms (file: 574-756-312-236-216-312-213-216-756-231-321-537-132-267-275-752-125-213-102-516-263-312-571-103-316-261-265-256-216-215-215-210-267-123-312-326-165-157-123-163-576-576-267-675-1_W.mp3)
  Segment 11: 31996ms ~ 39664ms (file: 753-123-750-123-316-253-754-236-203-267-261-270-230-675-761-237-260-263-752-576-260-260-276-206-273-260-267-267-621-267-156-572-263-213-321-312-765-206-230-527-267-206-256-206-2_W_stt_gerhard_earnhardt_so_here_we_have_in_thi.mp3)
  Segment 12: 40664ms ~ 49124ms (file: 213-627-132-312-217-231-123-326-130-756-756-326-123-321-267-102-267-627-756-276-123-236-326-132-102-267-120-237-273-712-276-267-756-765-326-756-321-103-320 160-312-765-756-572-3_W.mp3)
  Segment 13: 50124ms ~ 56568ms (file: 267-267-327-706-736-321-231-762-312-103-312-210-236-320-267-726-120-702-203-312-216-267-206-752-753-267-260-263-160-126-570-215-570-230-715-276-207-206-235-132-236-275-367-576-5_V_stt_this_is_the_only_major_arguments_as_the.mp3)
  Segment 14: 57568ms ~ 63220ms (file: 210-126-547-351-102-213-213-236-271-102-215-723-275-203-265-205-203-215-203-251-215-210-270-201-265-501-257-510-357-301-135-173-357-325-123-201-302-230-203-237-576-127-321-213-3_W.mp3)
  Segment 15: 64220ms ~ 64796ms (file: 154-547-541-543-541-154-561-534-574-536-541-564-135-541-534-541-514-541-265-657-657-657-654-564-567-654-654-654-645-654-645-654-654-617-613-165-354-531-536-563-514-514-574-536-5_V.mp3)
  Segment 16: 65796ms ~ 66372ms (file: 162-231 637-176-362-137-103-103-137-103-136-167-125-136-136-103-103-576-132-167-176-103-132-137-120-160-103-102-126-162-103-301-162-610-613-103-103-102-103-162-103-130-102-162-1_V.mp3)
  Segment 17: 67372ms ~ 67948ms (file: 504 572-572-756-752-752-567-576-756_W.mp3)
  Segment 18: 68948ms ~ 69524ms (file: 103-150-175-570-517-310-571-150-571 175-105-571-517-751-751-571-751-705-750-701-701-751-175-751-570-175-571-150-517-753-571-573-135-375-517-157-135-570-571-701-571-571-571-107-7_W.mp3)
  Segment 19: 70524ms ~ 71100ms (file: 572-526-572-657-637-623-136-270-165-610-167-175 256-205-570-562-276-632-326-706-703-157-207-107-102-175-157-176-615-106-160-106-106 506-560-527-576-705-572-756-237-526-572-257-2_V.mp3)
  Segment 20: 72100ms ~ 72676ms (file: 627-162-136-137 103-126-167-106-106-612-362-362-103-103-160-136-103-136-103 617-102-103-153-162-312-160-163-613-106-136-105-153-135-361-671-103-136-106-123-617-106-306-516-315-3_V.mp3)
  Segment 21: 73676ms ~ 74252ms (file: 657-651-651-615-651-651-514-541-504-504-504-504-504-504-504-504-540-504-504-503-503-503-527-502-657-657-671-671-617-671-675-675-657-675-657-675-657-675-367-631-162-157-156-156-2_W.mp3)
  Segment 22: 75252ms ~ 79680ms (file: 625-267-261-267-273-263-231-231-312-123-312-321-213-123-123-213-216-216-103-301-312-260-231-213-230-213-123-627-132-132-310-123-126-102-217-312-132-126-326-321-326-213-216-312-3_W.mp3)
  Segment 23: 80680ms ~ 88852ms (file: 167-170-157-751-231-753-123-261-756-765-675-261-610-106-607-617-132-263-321-361-267-576-276-762-263-573-523-623-617-671-621-672-672-267-621-136-263-352-315-312-625-326-312-310-2_W.mp3)
  Segment 24: 89852ms ~ 98204ms (file: 106-321-321-627-627-326-312-231-130-312-236-762-216-267-627-672-320-254-250-215-640-624-245-254-250-201-250-201-250-254-256-245-230-201-240-250-257-250-254-102-123-132-206-102-1_W.mp3)
  Segment 25: 99204ms ~ 99780ms (file: 170-170-170-157-175-137-710-170-756-571-567-576-567-576-576-576-567-657-567-657-756 510-107-710-731-750-157-710-170-170-701-170-157-715-576-157-576-567-517-751-765-751-715-176-7_V.mp3)
  Segment 26: 100780ms ~ 111832ms (file: 237-123-132-164-173-261-675-612-567-165-261-231-213-173-130-120-321-135-312-102-103-267-170-613-236-123-103-267-321-103-137-760-273-725-231-103-672-213-321-163-102-320-752-257-2_W.mp3)
  Segment 27: 112832ms ~ 113408ms (file: 731-173-730-715-712-712-175-710-137-173-173-716-713-715-713-173-713-713-712-713-173-713-137-751 571-715-715-751-715-173-175-751-715-126-271-715-750-175-710-715-715-735-715-713-7_V.mp3)
  Segment 28: 114408ms ~ 114984ms (file: 753-731-106-607-607-610-631-610-670-673-617-673-163-671-613-671-176-170-731 301-703-705-730-103-317-173-175-301-130-310-317-310-673-301-137-137-730-310-130-301-317-107-730-713-7_V.mp3)
  Segment 29: 115984ms ~ 116560ms (file: 176-103-316-310-163-132-306-130-103-136-106-541-162-167 236-231-103-106-631-621-130-617-162-317-132-165-102-106-136-126-123-163-106-103-136-160-672-167-106-615-316-163-167-136-1_V.mp3)
  Segment 30: 117560ms ~ 123536ms (file: 260-205-210-213-321-263-275-320-312-321-210-123-126-102-132-102-132-376-236-326-376-326-327-327-276-276-217-276-267-267-237-107-276-123-263-236-102-310-216-123-216-213-267-260-7_W.mp3)
  Segment 31: 124536ms ~ 128676ms (file: 132-210-201-237-267-617-130-126-310-524-123-201-572-205-503-230-135-315-206-127-206-230-207-321-321-310-102-574-157-176-156-175-167-637-576-153-165-175-216-126-130-301-576-275-6_W.mp3)
  Segment 32: 129676ms ~ 131116ms (file: 621-126-106-106-106-130-675-670 301-310-310-130-103-136-162-103-310-103-132-163-130-301-762-670-627-672-627-360-365-563-653-365-306-361-136-306-301-103-132-316-312-132-312-531-1_V.mp3)
  Segment 33: 132116ms ~ 134960ms (file: 653-675-675-657-657-675-675-675-675-675-657-657-657-675-657-657-657-657-657_W.mp3)
  Segment 34: 135960ms ~ 136536ms (file: 574-574-574-574-576-571-574 574-576-574-517-571-517-157-751-571-571-157-571-157 571-157-157-157 150-510-517-517-571 574-574-150-517-157-517-157-571-517-157-571-571-571-517-517 5_W.mp3)
  Segment 35: 137536ms ~ 147688ms (file: 765-537-153-130-231-103-312-725-136-756-627-752-367-537-175-526-321-312-723-127-103-102-310-612-236-356-153-132-230-312-607-213-602-301-376-756-756-547-316-360-320-217-207-207-3_W.mp3)
  Segment 36: 148688ms ~ 149264ms (file: 361-167-231-213-136-106-617-631-163-672-362-156-132-761-361-751-310-163-126-160-267-160-106-164-165-602-637-512-126 162-715-107-105-136-136-106-156-163 137-106-165-106-103-567-3_V.mp3)
  Segment 37: 150264ms ~ 150840ms (file: 671-126-312 163 231-132-231-132-123-132-143-165 236-253-325-213 162-176-751-136-627-762-761-567-576-657-672-267-673-625-627-236-162-657-163-106-710-762-763-761-761-762-761-762-7_W.mp3)
  Segment 38: 151840ms ~ 152416ms (file: 564-562-635-132-675-762-672-260-160-510-531 637-637-672-623-326-623-361-260-362-632-623-263-672-672-630-627-627-263-612-162-213-263-236-263-562-571-673-653-163-103-136-106-617-6_W.mp3)
  Segment 39: 153416ms ~ 153992ms (file: 573-175-517-517-571-571-573-571-571-571-570-570-571-570 157-517-517-517-531-517-157-513-517-517-157-517-517-517-531-571-537-571-501 517-175-751-175-751-153-517-157-750-517-153-1_V.mp3)
  Segment 40: 154992ms ~ 155568ms (file: 507-526-520-205-527-527-752-572-752-570-527-675-657-756-576-765-756-765-765-756-765-765-765-765 562-502-506-250-576-260-567-657-765-675-756-657-765-567-756-765-675-765-765-756-7_V.mp3)
  Segment 41: 156568ms ~ 157144ms (file: 627-627-673-627 621 170-716-627-216 716-126-126-132-576-510-217-126-236-321-321-236-625-762-617-627-671-675-316-361-316-361-136-316 123-312-627-267-621-672-172-263-267-621-652 7_W.mp3)
  Segment 42: 158144ms ~ 165560ms (file: 127-210-120-201-120-216-106-610-263-620-201-316-103-271-572-213-231-263-267-173-217-321-103-261-627-267-267-213-102-315-326-621-172-672-623-231-201-735-150-106-571-137-136-130-1_W.mp3)
  Segment 43: 166560ms ~ 175668ms (file: 237-123-320-312-107-123-321-321-753-320-627-106-173-310-312-103-103-216-715-754-123-276-126-230-310-375-762-207-213-102-210-302-103-753-273-312-102-230-236-132-102-273-576-327-2_W.mp3)
  Segment 44: 176668ms ~ 177244ms (file: 316-361-316-163-310-601-612-620-765-657-657-657-567-756_W.mp3)
  Segment 45: 178244ms ~ 178820ms (file: 716-721-713-172-173-371-712-762-761-721-176-172-716-176-721-617-731-712-713-172-172-271-176-156-153-137-136-167-136-136-132-130-123-132-123-132-213-231-217-132-123-312-127-132-2_W.mp3)
  Segment 46: 179820ms ~ 180396ms (file: 175-157-751-756-751-715-175-175-175-715-715-715-715-715-756-751-756-751-751-751-571-547 150-150-510-157-105-137-571-751-751-751-751-175-175-175-751-715-175-715-751-756-751-756-7_V.mp3)
  Segment 47: 181396ms ~ 186292ms (file: 265-267-132-260-526-265-256-236-320-123-132-310-273-371-170-217-120-130-120-163-276-236-762-261-263-261-627-172-276-321-571-362-326-301-132-123-716-263-267-261-623-276-671-276-7_W.mp3)
  Segment 48: 187292ms ~ 187868ms (file: 504-150-150-510-517-517 105-105-715-157-105-510-157-571-571-571-571-573-571-571 150-150-510-517-510-517-571-571-571-571-751-517-756-756-756-765-765-756-756-507-507-570-510-571-5_W.mp3)
  Segment 49: 188868ms ~ 199380ms (file: 326-572-236-263-267-576-327-267-271-735-326-610-123-710-103-137-756-765-136-162-613-756-236-326-326-326-321-236-130-325-130-312-321-132-236-267-236-167-136-236-236-372-137-571-3_W.mp3)
  Segment 50: 200380ms ~ 200956ms (file: 230-126-613-615-612-162-567-103-105-673-613-206-267-765-726-362-302-362-502-503-206-230-105-103-621-631-136-362-236-231-130-103-612-631-302-631-136-760-106-163-167-163-765-765-6_V.mp3)
For EACH segment, provide a JSON object with:
1. Full transcript of the speech
2. Word-level timing (start_ms relative to segment start, duration_ms)
3. Speaker identification (speaker_1, speaker_2, etc. — different voices get different IDs)
4. L/R stereo phase analysis if noticeable (phase_lr_deg: estimated degrees, 0=center)
5. Confidence score (0.0~1.0) — how confident you are in the transcript accuracy
6. Detected language code (e.g. "en", "ko", "ja", "zh")
7. silence_before_ms — milliseconds of silence/noise BEFORE the first word starts speaking
8. audio_type — one of: speech, music, instrument, noise, mixed
Respond with ONLY a JSON array, no markdown fences, no explanation:
[
  {
    "segment": 1,
    "transcript": "Hello, how are you today?",
    "speaker": "speaker_1",
    "confidence": 0.95,
    "language": "en",
    "silence_before_ms": 120,
    "audio_type": "speech",
    "words": [
      {"word": "Hello", "start_ms": 120, "dur_ms": 350, "phase_lr_deg": 0.0},
      {"word": "how", "start_ms": 520, "dur_ms": 200, "phase_lr_deg": 0.0}
    ]
  }
]
> **Time**: 2026-04-05 11:24:28
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 2,
"transcript": "역사는 전 근대 기준으로 단 한 번도 없었습니다 거란이 왔다 돌아갔고 몽골이 왔다가 결국 협상했고 왜군이 쳐들어왔다가 밀려났습니다 이게 순전히 저항 정신의 결과이기만 할까요 침략자 입장에서 한반도를 떠올려",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "역사는", "start_ms": 40, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "전", "start_ms": 550, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "근대", "start_ms": 780, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "기준으로", "start_ms": 1150, "dur_ms": 510, "phase_lr_deg": 0.0},
{"word": "단", "start_ms": 1720, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 1920, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "번도", "start_ms": 2150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "없었습니다", "start_ms": 2550, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "거란이", "start_ms": 3350, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "왔다", "start_ms": 3820, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "돌아갔고", "start_ms": 4180, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "몽골이", "start_ms": 4820, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "왔다가", "start_ms": 5280, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "결국", "start_ms": 5710, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "협상했고", "start_ms": 6050, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "왜군이", "start_ms": 6750, "dur_ms": 430, "phase_lr_deg": 0.0},
{"word": "쳐들어왔다가", "start_ms": 7230, "dur_ms": 710, "phase_lr_deg": 0.0},
{"word": "밀려났습니다", "start_ms": 8010, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 8850, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "순전히", "start_ms": 9180, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "저항", "start_ms": 9680, "dur_ms": 290, "phase_lr_deg": 0.0},
{"word": "정신의", "start_ms": 10010, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "결과이기만", "start_ms": 10450, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "할까요", "start_ms": 11080, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "침략자", "start_ms": 11500, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "입장에서", "start_ms": 11950, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "한반도를", "start_ms": 12480, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "떠올려", "start_ms": 12980, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 4,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 5,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 6,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 7,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 8,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 9,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 10,
"transcript": "지표가 노란색으로 흔들립니다 방향이 확실하지 않고 매수 매도세가 섞여서 리듬이 깨진 상태인 거죠 그런데 초록색으로 바뀌는 순간은 시장이 정확한 방향성을 되찾았다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "지표가", "start_ms": 120, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "노란색으로", "start_ms": 580, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "흔들립니다", "start_ms": 1180, "dur_ms": 610, "phase_lr_deg": 0.0},
{"word": "방향이", "start_ms": 1920, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "확실하지", "start_ms": 2380, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "않고", "start_ms": 2920, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "매수", "start_ms": 3350, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "매도세가", "start_ms": 3720, "dur_ms": 510, "phase_lr_deg": 0.0},
{"word": "섞여서", "start_ms": 4280, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "리듬이", "start_ms": 4710, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "깨진", "start_ms": 5120, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "상태인", "start_ms": 5450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "거죠", "start_ms": 5850, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "그런데", "start_ms": 6250, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "초록색으로", "start_ms": 6680, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "바뀌는", "start_ms": 7280, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "순간은", "start_ms": 7710, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "시장이", "start_ms": 8250, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "정확한", "start_ms": 8680, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "방향성을", "start_ms": 9150, "dur_ms": 510, "phase_lr_deg": 0.0},
{"word": "되찾았다", "start_ms": 9710, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "get harder and harder so you know we have in this need to distinguish between people you know in the US of course we have the SAT and the ACT but every",
"speaker": "speaker_3",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "get", "start_ms": 80, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "harder", "start_ms": 310, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "and", "start_ms": 710, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "harder", "start_ms": 910, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "so", "start_ms": 1450, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 1720, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "know", "start_ms": 1920, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "we", "start_ms": 2150, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "have", "start_ms": 2320, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 2550, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "this", "start_ms": 2710, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "need", "start_ms": 2950, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 3250, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "distinguish", "start_ms": 3420, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "between", "start_ms": 4050, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "people", "start_ms": 4510, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 4920, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "know", "start_ms": 5120, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 5350, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 5510, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "US", "start_ms": 5680, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "of", "start_ms": 6010, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "course", "start_ms": 6170, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "we", "start_ms": 6580, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "have", "start_ms": 6750, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 6980, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "SAT", "start_ms": 7150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "and", "start_ms": 7550, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 7750, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "ACT", "start_ms": 7920, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "but", "start_ms": 8280, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "every", "start_ms": 8510, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "덩달아 뛴다고 설명합니다 지난 한 달간의 유가 폭등이 끝이 아니라는 말입니다 뉴욕에서 MBC 뉴스 손명선입니다",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "덩달아", "start_ms": 150, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "뛴다고", "start_ms": 580, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "설명합니다", "start_ms": 980, "dur_ms": 610, "phase_lr_deg": 0.0},
{"word": "지난", "start_ms": 1720, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 2080, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "달간의", "start_ms": 2310, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "유가", "start_ms": 2720, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "폭등이", "start_ms": 3080, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "끝이", "start_ms": 3510, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "아니라는", "start_ms": 3880, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "말입니다", "start_ms": 4350, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "뉴욕에서", "start_ms": 5050, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "MBC", "start_ms": 5620, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "뉴스", "start_ms": 6050, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "손명선입니다", "start_ms": 6410, "dur_ms": 680, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "담수화 시설까지 파괴할 수 있다는",
"speaker": "speaker_5",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "담수화", "start_ms": 200, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "시설까지", "start_ms": 650, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "파괴할", "start_ms": 1180, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 1650, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있다는", "start_ms": 1850, "dur_ms": 380, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "you as a sociology major are going to sociology is a field that will really prepare people for this new world that we're",
"speaker": "speaker_6",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "you", "start_ms": 100, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "as", "start_ms": 250, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 400, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "sociology", "start_ms": 520, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "major", "start_ms": 1150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "are", "start_ms": 1550, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "going", "start_ms": 1710, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 2010, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "sociology", "start_ms": 2180, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 2810, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 2960, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "field", "start_ms": 3080, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "that", "start_ms": 3450, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "will", "start_ms": 3680, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "really", "start_ms": 3880, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "prepare", "start_ms": 4280, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "people", "start_ms": 4750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "for", "start_ms": 5150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "this", "start_ms": 5350, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "new", "start_ms": 5610, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "world", "start_ms": 5910, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "that", "start_ms": 6280, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "we're", "start_ms": 6510, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 16,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 17,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 18,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 19,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 20,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 21,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 22,
"transcript": "사용자 입장에서는 굉장히 긍정적인 부분이 많다고 생각하거든요 그래서 저는 최근에 이 약간 업데이트 사",
"speaker": "speaker_7",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "사용자", "start_ms": 150, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "입장에서는", "start_ms": 610, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "굉장히", "start_ms": 1210, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "긍정적인", "start_ms": 1650, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "부분이", "start_ms": 2180, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "많다고", "start_ms": 2550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "생각하거든요", "start_ms": 2950, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 3620, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "저는", "start_ms": 3980, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "최근에", "start_ms": 4250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 4650, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "약간", "start_ms": 4810, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "업데이트", "start_ms": 5070, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "사", "start_ms": 5570, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 24,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 25,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 26,
"transcript": "흐르고 있는 것 중에 특정 이슈가 어 나한테 필요한 메시지네 하면서 딱 받아가지고 이 메시지를 그냥 받아가지고 처리를 하는 거지 뭔가 외",
"speaker": "speaker_8",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "흐르고", "start_ms": 180, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 520, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 780, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "중에", "start_ms": 950, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "특정", "start_ms": 1210, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "이슈가", "start_ms": 1580, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "어", "start_ms": 2010, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "나한테", "start_ms": 2210, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "필요한", "start_ms": 2580, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "메시지네", "start_ms": 2980, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "하면서", "start_ms": 3510, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "딱", "start_ms": 3910, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "받아가지고", "start_ms": 4150, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 4720, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "메시지를", "start_ms": 4880, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 5350, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "받아가지고", "start_ms": 5650, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "처리를", "start_ms": 6220, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 6620, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "거지", "start_ms": 6950, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "뭔가", "start_ms": 7250, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "외", "start_ms": 7580, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 28,
"transcript": "과정에서 혁명을 불러일으키고 있는 한국의 이것은 바로",
"speaker": "speaker_9",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "과정에서", "start_ms": 120, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "혁명을", "start_ms": 550, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "불러일으키고", "start_ms": 920, "dur_ms": 610, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 1580, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "한국의", "start_ms": 1850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이것은", "start_ms": 2250, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 2620, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 30,
"transcript": "속도와 결과물을 압도적으로 증폭시켜 줘요 호프먼은 이렇게 말했습니다 앞으로 모든 사람이 자신만의 강력한 AI 비서를 곁에 두고 완전히 새로운 체급으로 일하게 되는 업무의 재편이 일어날 것이다 우리가 진짜",
"speaker": "speaker_10",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "속도와", "start_ms": 150, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "결과물을", "start_ms": 510, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "압도적으로", "start_ms": 980, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "증폭시켜", "start_ms": 1580, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "줘요", "start_ms": 2010, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "호프먼은", "start_ms": 2280, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 2710, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "말했습니다", "start_ms": 3080, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "앞으로", "start_ms": 3650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "모든", "start_ms": 4050, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "사람이", "start_ms": 4350, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "자신만의", "start_ms": 4710, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "강력한", "start_ms": 5180, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "AI", "start_ms": 5610, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "비서를", "start_ms": 5880, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "곁에", "start_ms": 6280, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "두고", "start_ms": 6580, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "완전히", "start_ms": 6910, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "새로운", "start_ms": 7350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "체급으로", "start_ms": 7750, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "일하게", "start_ms": 8210, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "되는", "start_ms": 8610, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "업무의", "start_ms": 8880, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "재편이", "start_ms": 9280, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "일어날", "start_ms": 9710, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "것이다", "start_ms": 10110, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "우리가", "start_ms": 10550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 10950, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 32,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 33,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 34,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 35,
"transcript": "항상 유연한 작업 방식이 가장 중요하대요 아까전에 제가 이제 궁금한 것 중에 하나가 가장의 무게를 안고 매매하시잖아요 토끼 같은 두 자녀도 있으시고 이 자녀들을 지금 키우시면서 가장의 무게를 어깨 등에",
"speaker": "speaker_11",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "항상", "start_ms": 150, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "유연한", "start_ms": 510, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "작업", "start_ms": 920, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "방식이", "start_ms": 1220, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 1650, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "중요하대요", "start_ms": 1980, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "아까전에", "start_ms": 2550, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 3020, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 3280, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "궁금한", "start_ms": 3550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 3950, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "중에", "start_ms": 4120, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "하나가", "start_ms": 4380, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "가장의", "start_ms": 4750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "무게를", "start_ms": 5150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "안고", "start_ms": 5550, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "매매하시잖아요", "start_ms": 5850, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "토끼", "start_ms": 6620, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "같은", "start_ms": 6920, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "두", "start_ms": 7220, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "자녀도", "start_ms": 7420, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "있으시고", "start_ms": 7780, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 8280, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "자녀들을", "start_ms": 8450, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 8910, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "키우시면서", "start_ms": 9210, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "가장의", "start_ms": 9780, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "무게를", "start_ms": 10180, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "어깨", "start_ms": 10580, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "등에", "start_ms": 10880, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 37,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 38,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 39,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 40,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 41,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 42,
"transcript": "얼었나 녹으면서 갈라졌습니다 그래서 조선은 세금을 강과 바다로 거뒀습니다 좋은 제도입니다 각 지방에서 현물로 거둔 세곡 그러니까 쌀과 곡물을 조창이라는 지역 창고에 모은 다음 배에 실어",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "얼었나", "start_ms": 180, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "녹으면서", "start_ms": 610, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "갈라졌습니다", "start_ms": 1080, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 1850, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "조선은", "start_ms": 2210, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "세금을", "start_ms": 2650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "강과", "start_ms": 3050, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "바다로", "start_ms": 3380, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "거뒀습니다", "start_ms": 3810, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 4480, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "제도입니다", "start_ms": 4780, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "각", "start_ms": 5410, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "지방에서", "start_ms": 5610, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "현물로", "start_ms": 6150, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "거둔", "start_ms": 6580, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "세곡", "start_ms": 6910, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "그러니까", "start_ms": 7280, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "쌀과", "start_ms": 7750, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "곡물을", "start_ms": 8080, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "조창이라는", "start_ms": 8510, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "지역", "start_ms": 9110, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "창고에", "start_ms": 9450, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "모은", "start_ms": 9880, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 10210, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "배에", "start_ms": 10510, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "실어", "start_ms": 10780, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 44,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 45,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 46,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 47,
"transcript": "제국주의 시절까지 거슬러 올라갑니다 1429년부터 400여 년간 북유럽 외레순 해협에서 덴마크가 통행료를 안 낸 선박들의 대포를 쐈습니다 호르무즈 해협 통행료 부과는 전 세계",
"speaker": "speaker_12",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "제국주의", "start_ms": 150, "dur_ms": 510, "phase_lr_deg": 0.0},
{"word": "시절까지", "start_ms": 710, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "거슬러", "start_ms": 1250, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "올라갑니다", "start_ms": 1680, "dur_ms": 610, "phase_lr_deg": 0.0},
{"word": "1429년부터", "start_ms": 2350, "dur_ms": 820, "phase_lr_deg": 0.0},
{"word": "400여", "start_ms": 3210, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "년간", "start_ms": 3610, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "북유럽", "start_ms": 3950, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "외레순", "start_ms": 4380, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "해협에서", "start_ms": 4810, "dur_ms": 510, "phase_lr_deg": 0.0},
{"word": "덴마크가", "start_ms": 5350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "통행료를", "start_ms": 5850, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 6380, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "낸", "start_ms": 6580, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "선박들의", "start_ms": 6810, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "대포를", "start_ms": 7310, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "쐈습니다", "start_ms": 7710, "dur_ms": 510, "phase_lr_deg": 0.0},
{"word": "호르무즈", "start_ms": 8280, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "해협", "start_ms": 8780, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "통행료", "start_ms": 9150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "부과는", "start_ms": 9550, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "전", "start_ms": 9980, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "세계", "start_ms": 10180, "dur_ms": 310, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 49,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 50,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 11:24:28*
