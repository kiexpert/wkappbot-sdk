# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=36536  time=07:55:15  uptime=7196s  mem=746MB  system_processes=413"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_075148.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 175-175-157-175-517-517-175-175-756-175-716-175-715-175-167-671-176-136-167-716-673-175-716-176 107-153-157-715-175-715-751-175-517-571-517-517-571-517-751-756-765-756-176-716-7_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 175-701-107-157-135-105-150-150-517-170-157 105-157-751-517-751-157-157-135-571-135-157-135-157-713-175-175-351-135-150 571-510-513-135-103-152-105-750-107-710-731-715-705-715-7_W.mp3)
  Segment 3: 3152ms ~ 12404ms (file: 527-231-236-120-261-263-275-756-321-231-237-231-267-312-102-123-120-627-126-167-120-210-102-267-627-263-627-276-672-213-263-267-210-573-756-320-123-102-261-236-236-267-267-752-2_W.mp3)
  Segment 4: 13404ms ~ 13980ms (file: 312-130-675-103-320-256-217-761-137-163-756-372-213-157-716-132-276-316-130-310-301-130-310-135-135-135-315-135-315-153-153-150-175-105-130-103-135-517-175-135-130-103-310-137-3_W.mp3)
  Segment 5: 14980ms ~ 15556ms (file: 573-576-572-576-576-571-576-573-572-573-572-571-573-752 576-573-576-576-576-571-576-572-572-571-175-571-157-175-517-751-317-573-513-571-157-157-751-305-375-175 713-712-715-751-3_W.mp3)
  Segment 6: 16556ms ~ 17132ms (file: 175-176-167-176 150-751-715-751-715-175-176-170-176-176-176-175-761-173 157-175-175-175-167-175-175-173-176-175-157-176-176-716-176-761-176-137-167-176 157-137-715-571-715-175-7_W.mp3)
  Segment 7: 18132ms ~ 30012ms (file: 762-531-105-615-103-176-103-310-231-321-167-132-723-315-537-573-754-327-103-627-263-260-203-756-752-263-267-752-135-375-130-106-321-132-316-317-260-320-756-657-103-751-167-276-2_W.mp3)
  Segment 8: 31012ms ~ 31588ms (file: 571-570-571-571-570-570-570-571-570-571-570 754-756-573-572-573-572-573-752-573-752-571-571-571 756-751-753-573-576-576-572-752-756-573-572-753-573-573-574 752-572-576-572-573-5_W.mp3)
  Segment 9: 32588ms ~ 39320ms (file: 120-765-756-371-103-103-321-267-213-312-675-237-120-137-671-657-237-367-326-765-123-130-103-130-301-213-321-532-132-123-172-271-132-132-753-106-137-632-326-263-763-237-372-765-7_W.mp3)
  Segment 10: 40320ms ~ 40896ms (file: 506-756-756-576-567-765-765-675-675-615-675-617-657-671-637-761-673-675-176-673-617-763-123-316-673-756-376-703-376-671-316-651-617-623-270-123-625-217-623-312-312-153-136-567-2_W.mp3)
  Segment 11: 41896ms ~ 47836ms (file: 230-321-327-327-372-372-572-375-723-310-310-623-206-263-102-326-360-306-326-302-320-316-716-617-160-102-103-672-162-206-261-761-236-267-267-261-263-260-267-362-267-267-267-312-6_W.mp3)
  Segment 12: 48836ms ~ 49412ms (file: 752-570-576-756-763-517-536-576-527-576-576-725-167-316-361-361-361-316-361_W.mp3)
  Segment 13: 50412ms ~ 50988ms (file: 675-765-675-671-135-132-137-136-137-716-173-715-761-671-163-167-765-765-765-675-763-675-675-762-675-672-763-672-763-675-671-673-675-675-617-167-675-617-672-675-763-763-672-675-6_W.mp3)
  Segment 14: 51988ms ~ 58432ms (file: 517-217-260-132-173-312-203-236-231-230-236-203-217-302-237-257-103-206-213-206-230-132-203-123-102-103-576-260-201-216-231-210-213-231-321-230-230-230-231-123-230-123-230-321-2_V.mp3)
  Segment 15: 59432ms ~ 66308ms (file: 236-756-326-372-236-321-160-130-621-267-201-102-123-102-276-216-267-126-601-267-731-106-736-726-267-576-257-276-216-627-267-261-106-132-160-610-132-275-276-321-376-637-361-761-6_W.mp3)
  Segment 16: 67308ms ~ 67884ms (file: 753-302-753 517-571-157-150-105-712-173-137-137-173-731-375-713-170-137-713-307-710-137-701-750-731-137-715_V.mp3)
  Segment 17: 68884ms ~ 69460ms (file: 672-672 257-672-327-231-127-761-762-726-123-162-312-273-267-216-502-125-216-236-762-325-703-276-327-236-250-267-263-726-265-312-135-351-357-315-153-130_V.mp3)
  Segment 18: 70460ms ~ 77660ms (file: 130-305-320-136-752-107-623-230-312-310-310-316-316-312-312-317-310-306-362-327-236-623-236-326-312-130-310-130-231-213-176-762-726-271-321-312-231-123-132-517-312-261-137-712-3_W.mp3)
  Segment 19: 78660ms ~ 86220ms (file: 207-275-207-207-756-107-756-725-207-752-327-236-132-571-250-310-230-201-312-302-213-210-120-102-176-206-126-201-132-627-756-752-102-123-136-312-120-102-120-201-210-120-213-231-1_V.mp3)
  Segment 20: 87220ms ~ 87796ms (file: 571-756-576-627-716-271-721 576-756-576-576-751-576-570-750-517-751-567-750-751-570-756-705-750-157-175-751-750-570-157-756-576-756-756 576-576-576 574-574-574-574-574-576-576 5_W.mp3)
  Segment 21: 88796ms ~ 94592ms (file: 102-261-321-103-127-617-671-236-321-263-135-312-216-320-627-167-570-756-670-103-216-130-765-760-265-157-517-126-751-126-157-103-321-321-231-276-301-750-132-572-175-601-206-103-1_V.mp3)
  Segment 22: 95592ms ~ 108228ms (file: 217-123-321-173-316-103-102-132-216-263-276-210-302-513-236-267-731-137-357-723-302-237-130-201-201-130-172-372-302-376-302-312-362-310-237-263-301-237-217-230-231-317-312-310-3_W.mp3)
  Segment 23: 109228ms ~ 109804ms (file: 675-321-125-754-756-135-315-361-316-136-135-135-136-316-267-620-163_W.mp3)
  Segment 24: 110804ms ~ 117896ms (file: 167-325-736-267-726-375-263-276-672-574-351-537-102-123-102-132-125-263-236-237-760-637-320-236-562-235-321-517-713-237-125-103-271-231-263-267-157-310-103-321-261-132-132-763-2_W.mp3)
  Segment 25: 118896ms ~ 119472ms (file: 170-107-716 571-716-175-715-175-175-751-716-761-765-761-765-763-765-713-761-716-716-170-175-170-170-175-715 571-107-710-170-170-175-710-715-175-710-175-176-716-761-765-715-716-7_V.mp3)
  Segment 26: 120472ms ~ 127852ms (file: 576-576-574-756-132-132-136-132-132-143-301-103-130-321-125-123-135-362-173-276-132-321-231-351-365-375-753-253-165-762-765-751-312-762-765-216-713-317-132-602-602-620-620-206-7_W.mp3)
  Segment 27: 128852ms ~ 129428ms (file: 571-751-170-175-571-105-107 135-157-175-172-175-123-136-135-156-135-136-136-163-167-176-173-716-157-137-157-107-103-170-103-176-176-176-761-716-756-716-756-137-137-175-175-173-1_W.mp3)
  Segment 28: 130428ms ~ 137304ms (file: 276-627-672-276-267-213-137-130-316-312-102-301-310-301-316-203-231-275-127-132-235-123-321-375-573-752-572-157-107-213-231-273-271-273-263-237-236-576-172-572-215-137-130-271-1_W.mp3)
  Segment 29: 138304ms ~ 138880ms (file: 325-132-164-135-123-136-162-163-134-135-163-136-135-137-132-135-164-173-271-372-134-136-326-367-365-352-327-217-321-317-761-671-125-157-273-157-237-267-312-371-637-275-517-537-7_V.mp3)
  Segment 30: 139880ms ~ 144344ms (file: 321-270-206-720-762-627-276-270-276-237-273-327-237-231-231-213-213-217-271-237-327-273-217-216-271-237-237-237-270-237-321-203-237-260-236-213-237-312-726-732-631-126-621-637-3_W_stt_then.mp3)
  Segment 31: 145344ms ~ 145920ms (file: 753-527-276-316-237-532-327-206-135-732-276-236-256-567-265-237-523-237-576-756-563-765 705-675-267-261-760-627-570-261-536-756-760-627-312-675_V.mp3)
  Segment 32: 146920ms ~ 147496ms (file: 756-750-751-751-751-756-750 756-756-657-715-756-751-753-750-703-751-735-375 570-751-751-756-751-751-750 150-571-751-750-705-751-571-107-751 750-570 517-570-150-157-157-750-157-7_W.mp3)
  Segment 33: 148496ms ~ 156596ms (file: 716-130-753-765-620-361-160-106-137-163-132-236-321-136-326-236-102-132-106-123-627-130-632-321-312-326-321-130-236-136-103-102-561-236-123-103-263-312-153-531-310-576-352-236-2_W.mp3)
  Segment 34: 157596ms ~ 165156ms (file: 126-371-761-267-263-263-267-765-576-576-367-372-326-756-236-102-765-763-672-362-263-352-162-123-267-237-362-123-310-213-102-302-326-327-132-130-132-231-132-726-651-321-276-102-1_W.mp3)
  Segment 35: 166156ms ~ 166732ms (file: 136-135-137-136-306-316-310-301-301-301-136-302-301-367-301-136-103-103-136-132-137-137-135-135-136-106-102-106-106-102-106-103-106-106-106-106-106 134-132-147-132 132-625-654-7_V.mp3)
  Segment 36: 167732ms ~ 168308ms (file: 165-153-163-176-156-573-315-573-657-675-657-675-316 623-573-652-316-125-362-735-267-213-516_W.mp3)
  Segment 37: 169308ms ~ 169884ms (file: 573-571-574-175-157-135-176-176-136-163-167-631-167-763-631-327-326-107-673-713-612-763-672-673-637-765-613-673-367-671-371-617-671-312-213-321-672-612_W.mp3)
  Segment 38: 170884ms ~ 177256ms (file: 321-320-132-762-276-213-320-217-172-732-103-137-317-321-371-735-751-275-325-137-713-320-721-712-106-162-162-316-317-726-321-327-123-260-236-263-236-236-276-273-132-716-157-167-6_W.mp3)
  Segment 39: 178256ms ~ 178832ms (file: 736-637-672-576-576-531-571-574-573-375-571-726-357-653-672-671-675-617-675-637-136-316-612-162-572-765-213-206-213-213-526-132-612-132-216-217-215-321-312-371-135-217-213-152-7_W.mp3)
  Segment 40: 179832ms ~ 180408ms (file: 750-751-170-751-150-160-163-130-167-615-120-165-165-106-615-567-126-165-126-106-107-156-150-102-156-576-657-576-150-617-165-160-175-657-165-162-756-576-571-531-576-302-302-325-7_W.mp3)
  Segment 41: 181408ms ~ 191560ms (file: 163-103-627-756-230-216-302-375-130-135-206-210-527-751-265-571-236-132-250-320-173-301-201-320-361-136-132-103-136-136-261-612-613-632-627-361-136-631-613-610-316-310-751-375-7_W.mp3)
  Segment 42: 192560ms ~ 201920ms (file: 236-267-257-263-267-263-321-237-310-130-132-102-753-231-321-102-231-236-213-213-130-103-127-574-502-263-325-235-231-230-230-260-312-263-256-562-256-267-260-263-321-263-236-231-3_W.mp3)
  Segment 43: 202920ms ~ 209904ms (file: 103-351-316-317-326-376-136-217-617-615-107-312-106-132-317-367-106-362-106-561-167-106-261-136-675-765-762-276-326-105-102-732-360-537-275-672-137-310-315-310-312-236-326-237-3_W.mp3)
  Segment 44: 210904ms ~ 211480ms (file: 751-625-753-753-371 571-571-571-571-571-517-317-750-753-130-756-571-750-571-576-573-157-135-751-375 571-751-517-517-517-157-517-715-175-753-715-751-572-571-157-572-175-751 517-5_W.mp3)
  Segment 45: 212480ms ~ 214784ms (file: 675-567-512-513-132-672-631-123-675-537 753-576-137-310-130-130-573-237-326-253-312-276-132-307-267-730-126-321-532-123-256-527-126-602-576-276-236-570-206-236-753-326-502-752-3_W.mp3)
  Segment 46: 215784ms ~ 223380ms (file: 130-375-103-371-312-531-571-235-132-315-750-310-230-310-103-120-321-132-103-136-312-201-206-260-236-256-260-326-213-236-267-276-267-123-132-231-132-120-261-130-130-103-123-267-2_W.mp3)
  Segment 47: 224380ms ~ 224956ms (file: 267-325-756-123-617-750-321-562-326-162-763-675-725-261-537-267-321-137-317-276-576-725-367-756-762-276-540-257-726-236-275-357-136-256-267-321-372-253-726-756-316-756-257-256 3_V.mp3)
  Segment 48: 225956ms ~ 226532ms (file: 372-236-276-752-726-216-127-527 506-357-702-756-570-671-135-672-567-263-537-652-275-735-205-326-301-312-763-237-613-261-267-526-267-532-623-670-627-267-627-267-276-326-526-672-6_V.mp3)
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
> **Time**: 2026-04-06 07:57:04
> **AI**: gemini

---

## Response

[
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 3,
"transcript": "안녕하세요 여러분 오늘은 쇼핑할 때 사용하는 표현을 배워봅시다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 150, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "여러분", "start_ms": 1000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "오늘은", "start_ms": 1600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "쇼핑할", "start_ms": 2100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 2700, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "사용하는", "start_ms": 2950, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "표현을", "start_ms": 3600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "배워봅시다", "start_ms": 4150, "dur_ms": 900, "phase_lr_deg": 0.0}
]
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
"transcript": "먼저 가게에 들어가면 점원이 인사를 합니다 어서오세요 무엇을 도와드릴까요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "먼저", "start_ms": 200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "가게에", "start_ms": 650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "들어가면", "start_ms": 1150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "점원이", "start_ms": 1800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "인사를", "start_ms": 2300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "합니다", "start_ms": 2800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "어서오세요", "start_ms": 3400, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "무엇을", "start_ms": 4300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "도와드릴까요", "start_ms": 4800, "dur_ms": 900, "phase_lr_deg": 0.0}
]
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
"transcript": "Welcome How can I help you Just looking thank you",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "Welcome", "start_ms": 250, "dur_ms": 600, "phase_lr_deg": 10.0},
{"word": "How", "start_ms": 1000, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "can", "start_ms": 1250, "dur_ms": 150, "phase_lr_deg": 10.0},
{"word": "I", "start_ms": 1450, "dur_ms": 100, "phase_lr_deg": 10.0},
{"word": "help", "start_ms": 1600, "dur_ms": 300, "phase_lr_deg": 10.0},
{"word": "you", "start_ms": 1950, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "Just", "start_ms": 2300, "dur_ms": 350, "phase_lr_deg": 10.0},
{"word": "looking", "start_ms": 2700, "dur_ms": 500, "phase_lr_deg": 10.0},
{"word": "thank", "start_ms": 3300, "dur_ms": 300, "phase_lr_deg": 10.0},
{"word": "you", "start_ms": 3650, "dur_ms": 200, "phase_lr_deg": 10.0}
]
},
{
"segment": 10,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 11,
"transcript": "가격을 물어볼 때는 얼마예요 라고 말합니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "가격을", "start_ms": 180, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "물어볼", "start_ms": 700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "때는", "start_ms": 1200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "얼마예요", "start_ms": 1650, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "라고", "start_ms": 2400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "말합니다", "start_ms": 2750, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 13,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 14,
"transcript": "How much is this It's fifty dollars",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 220,
"audio_type": "speech",
"words": [
{"word": "How", "start_ms": 220, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "much", "start_ms": 450, "dur_ms": 300, "phase_lr_deg": 10.0},
{"word": "is", "start_ms": 800, "dur_ms": 150, "phase_lr_deg": 10.0},
{"word": "this", "start_ms": 1000, "dur_ms": 300, "phase_lr_deg": 10.0},
{"word": "It's", "start_ms": 1500, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "fifty", "start_ms": 1750, "dur_ms": 400, "phase_lr_deg": 10.0},
{"word": "dollars", "start_ms": 2200, "dur_ms": 600, "phase_lr_deg": 10.0}
]
},
{
"segment": 15,
"transcript": "조금 비싸네요 깎아주실 수 있나요",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 300,
"audio_type": "speech",
"words": [
{"word": "조금", "start_ms": 300, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "비싸네요", "start_ms": 750, "dur_ms": 600, "phase_lr_deg": -5.0},
{"word": "깎아주실", "start_ms": 1500, "dur_ms": 600, "phase_lr_deg": -5.0},
{"word": "수", "start_ms": 2150, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "있나요", "start_ms": 2350, "dur_ms": 500, "phase_lr_deg": -5.0}
]
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
"transcript": "It's a bit expensive Can I get a discount",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "It's", "start_ms": 200, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "a", "start_ms": 450, "dur_ms": 100, "phase_lr_deg": 10.0},
{"word": "bit", "start_ms": 600, "dur_ms": 250, "phase_lr_deg": 10.0},
{"word": "expensive", "start_ms": 900, "dur_ms": 700, "phase_lr_deg": 10.0},
{"word": "Can", "start_ms": 1800, "dur_ms": 250, "phase_lr_deg": 10.0},
{"word": "I", "start_ms": 2100, "dur_ms": 150, "phase_lr_deg": 10.0},
{"word": "get", "start_ms": 2300, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "a", "start_ms": 2550, "dur_ms": 100, "phase_lr_deg": 10.0},
{"word": "discount", "start_ms": 2700, "dur_ms": 600, "phase_lr_deg": 10.0}
]
},
{
"segment": 19,
"transcript": "일본어로도 연습해 볼까요 いくらですか",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "일본어로도", "start_ms": 250, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "연습해", "start_ms": 1000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "볼까요", "start_ms": 1500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "いくらですか", "start_ms": 2200, "dur_ms": 900, "phase_lr_deg": 0.0}
]
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 22,
"transcript": "잘 하셨습니다 물건을 살 때는 신용카드나 현금으로 계산합니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "잘", "start_ms": 150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "하셨습니다", "start_ms": 400, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "물건을", "start_ms": 1300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "살", "start_ms": 1850, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "때는", "start_ms": 2100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "신용카드나", "start_ms": 2600, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "현금으로", "start_ms": 3450, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "계산합니다", "start_ms": 4100, "dur_ms": 800, "phase_lr_deg": 0.0}
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
"transcript": "카드 되나요 아니면 현금만 받나요",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "카드", "start_ms": 200, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "되나요", "start_ms": 650, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "아니면", "start_ms": 1300, "dur_ms": 450, "phase_lr_deg": -5.0},
{"word": "현금만", "start_ms": 1800, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "받나요", "start_ms": 2350, "dur_ms": 500, "phase_lr_deg": -5.0}
]
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
"transcript": "Do you take cards Or cash only",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "Do", "start_ms": 150, "dur_ms": 150, "phase_lr_deg": 10.0},
{"word": "you", "start_ms": 350, "dur_ms": 150, "phase_lr_deg": 10.0},
{"word": "take", "start_ms": 550, "dur_ms": 300, "phase_lr_deg": 10.0},
{"word": "cards", "start_ms": 900, "dur_ms": 500, "phase_lr_deg": 10.0},
{"word": "Or", "start_ms": 1600, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "cash", "start_ms": 1850, "dur_ms": 400, "phase_lr_deg": 10.0},
{"word": "only", "start_ms": 2300, "dur_ms": 500, "phase_lr_deg": 10.0}
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
"transcript": "쇼핑은 정말 즐거운 일이죠 하지만 과소비는 조심해야 해요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "쇼핑은", "start_ms": 250, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "즐거운", "start_ms": 1250, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "일이죠", "start_ms": 1800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "하지만", "start_ms": 2450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "과소비는", "start_ms": 3000, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "조심해야", "start_ms": 3750, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "해요", "start_ms": 4400, "dur_ms": 400, "phase_lr_deg": 0.0}
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
"transcript": "맞아요 계획적인 쇼핑이 중요합니다",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "맞아요", "start_ms": 200, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "계획적인", "start_ms": 850, "dur_ms": 700, "phase_lr_deg": -5.0},
{"word": "쇼핑이", "start_ms": 1600, "dur_ms": 550, "phase_lr_deg": -5.0},
{"word": "중요합니다", "start_ms": 2200, "dur_ms": 800, "phase_lr_deg": -5.0}
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
"transcript": "그럼 이제 쇼핑 가방을 들고 출발해 볼까요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "쇼핑", "start_ms": 900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "가방을", "start_ms": 1350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "들고", "start_ms": 1900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "출발해", "start_ms": 2300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "볼까요", "start_ms": 2850, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "백화점은 너무 넓어서 길을 잃을 것 같아요",
"speaker": "speaker_3",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "백화점은", "start_ms": 200, "dur_ms": 650, "phase_lr_deg": -5.0},
{"word": "너무", "start_ms": 900, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "넓어서", "start_ms": 1300, "dur_ms": 550, "phase_lr_deg": -5.0},
{"word": "길을", "start_ms": 1950, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "잃을", "start_ms": 2300, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "것", "start_ms": 2650, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "같아요", "start_ms": 2850, "dur_ms": 500, "phase_lr_deg": -5.0}
]
},
{
"segment": 35,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "걱정 마세요 안내도가 잘 되어 있으니까요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "걱정", "start_ms": 150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "마세요", "start_ms": 550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "안내도가", "start_ms": 1200, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 1900, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "되어", "start_ms": 2200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있으니까요", "start_ms": 2600, "dur_ms": 800, "phase_lr_deg": 0.0}
]
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
"transcript": "오늘 배운 표현들을 복습하고 실전에서 사용해 보세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "배운", "start_ms": 600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "표현들을", "start_ms": 1050, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "복습하고", "start_ms": 1800, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "실전에서", "start_ms": 2550, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "사용해", "start_ms": 3250, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "보세요", "start_ms": 3800, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "그럼 오늘 수업은 여기까지입니다 감사합니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "수업은", "start_ms": 1000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "여기까지입니다", "start_ms": 1550, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 2600, "dur_ms": 900, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "선생님 감사합니다 수고하셨습니다",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "선생님", "start_ms": 200, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "감사합니다", "start_ms": 750, "dur_ms": 800, "phase_lr_deg": -5.0},
{"word": "수고하셨습니다", "start_ms": 1600, "dur_ms": 1000, "phase_lr_deg": -5.0}
]
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
"transcript": "See you next time Goodbye",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 150, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "you", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": 10.0},
{"word": "next", "start_ms": 600, "dur_ms": 350, "phase_lr_deg": 10.0},
{"word": "time", "start_ms": 1000, "dur_ms": 400, "phase_lr_deg": 10.0},
{"word": "Goodbye", "start_ms": 1500, "dur_ms": 600, "phase_lr_deg": 10.0}
]
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 07:57:04*
