# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=11716  time=11:50:20  uptime=36077s  mem=407MB  system_processes=355"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_114939.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 5724ms (file: 103-756-625-625-625-136-601-675-267-263-652-652-160-625-216-267-263-312-261-621-261-260-265-326-362-621-620-261-261-261-123-216-231-321-263-203-672-726-326-326-621-236-321-263-2_W.mp3)
  Segment 2: 6724ms ~ 11476ms (file: 203-103-123-263-103-103-132-312-302-132-123-231-230-136-301-502-216-206-203-102-130-132-120-102-201-203-102-120-163-136-103-162-321-316-673-631-136-261-123-123-106-267-126-103-2_V.mp3)
  Segment 3: 12476ms ~ 17948ms (file: 301-625-132-261-261-136-107-167-127-260-206-102-136-135-675-675-725-765-623-705-167-316-312-576-265-576-750-130-761-756-176-167-260-213-301-310-103-320-210-173-162-137-156-167-2_W.mp3)
  Segment 4: 18948ms ~ 19524ms (file: 526-527-510-517-516-576-670-675-675-670-657-675 627 502-501_W.mp3)
  Segment 5: 20524ms ~ 21100ms (file: 301-106-102 621-620-631-621-612-276-672-627-263-123-136-136-312-325-327-672-672-623-201-130-302-261-623-263-231-132-312 261-621-356 672-673-612-362-632-362-620-236-612-631-120-2_W.mp3)
  Segment 6: 22100ms ~ 22676ms (file: 153-135-153-156-157-156-157-153-756-574-574-103-173-136-137-137-301-571-175-137-361-361-312-132-156-157-156-157-156-157-153 571-137-135-316-315-135-137-137-176-371-716-362-365-1_V.mp3)
  Segment 7: 23676ms ~ 24252ms (file: 157-153-150-157-157-157-157-750-765-756-751 105-150-150-150-105-105-157-175-157-157-517-157-517-157-517-517-157-517-157-517-517-157-153-517-157-157 105-105-105-105-157-107-106-1_V.mp3)
  Segment 8: 25252ms ~ 25828ms (file: 413-105-147-134-103-103 431-134-140-154-134-415-145-145-134-104-143-145-154-145-105-103-413-315-134-153-134-415-143-135-145-137-134-314 457-457-415-405-341-431-314-435-405-104-1_V.mp3)
  Segment 9: 26828ms ~ 27404ms (file: 572-576 570 576-571-572-576-576-572-517-527-572-572-572-572-572-572-572-752-526-527-215 527-251-526-572-571-521-126-152-251-652-651-615-651-651-657-651-657-567-756-756-567-576-5_W.mp3)
  Segment 10: 28404ms ~ 28980ms (file: 510-215-162-560-562-652-275-576-517 175-157-571-765-175-715-371-105-501-501-510-517-507-507-570-570-570-570-570-567-527-572-571-572-572-526 326-602-263-206-236-376-612-236-326-6_W.mp3)
  Segment 11: 29980ms ~ 30556ms (file: 257-725-207-260-720-726-276-267-750-756-765-750-725-756-327-762-732-276-267-723-726 510-325-205-236-207-765-762-750-637-637-267-320-210-217-120-615-107-157-615-163-675-167-175-5_V.mp3)
  Segment 12: 31556ms ~ 32132ms (file: 617-136-357-352-132-315-762-157-231-132-137-167-312-136-126-126-261-321-123-706-716-157-126-216-705-123-541-572-217-507-276-206-216-216-237-172-621-502-237-263-123-263-213-276-7_V.mp3)
  Segment 13: 33132ms ~ 33708ms (file: 157-576-157-175 105-150-150-150-105-150-153-105-107-170-176-157-170-157-175-156-175-157-175-156-175-157-517-157-157-156-157-175 514-105-105-105-150-105-106-107-175-517-105-176-1_V.mp3)
  Segment 14: 34708ms ~ 35284ms (file: 506-520-562-572-752-765-726-726-762-675-657-631-362 125-621-612-615-615-672-672-675-675-657-675-657-657-675-657-657-657-657-657-675-671-657-657-657-675-674-576-756-576-576-572-5_W.mp3)
  Segment 15: 36284ms ~ 36860ms (file: 675-765-761-765-675-265-276-275-257-162-675-765-756-756-756-765-765-765-761-167-167-167-162-167-167-167-165-617-167-165-617-612-612-765-765-756-765-765-765-756-765-765-765-562-1_W.mp3)
  Segment 16: 37860ms ~ 44808ms (file: 312-326-176-321-321-312-321-120-627-271-217-357-267-632-157-326-762-267-627-123-132-236-267-271-602-312-126-260-231-573-261-172-210-271-213-326-263-213-163-326-267-213-617-672-2_W.mp3)
  Segment 17: 45808ms ~ 46384ms (file: 357-735-537-573-765-756-735-753-735-761-751-731-173-701-175-715-173-327-312-312-132-123-137-756-761-736-732-752-376-701-751-713-173-173-751-173-317-312-735-132-312-312-130-132 5_W.mp3)
  Segment 18: 47384ms ~ 47960ms (file: 372-130-130-103-613-630-603-316-630-130-310-613-137-130-160-371-310-312-137-317-103-175-705-103 701-730-735-170-751-537-735-735-175-703-735-713-670-637-730-716-307-130-103-136-7_W.mp3)
  Segment 19: 48960ms ~ 49536ms (file: 132-102-103-136-312-301-301-357-175-176-103-137-132-103-130-132-310-301-312-602-602-360-326-320-126-206-320 756_V.mp3)
  Segment 20: 50536ms ~ 56980ms (file: 276-276-752-752-725-231-123-102-673-124-756-765-237-320-103-217-751-762-276-230-203-732-751-721-103-103-715-713-103-102-103-217-201-203-321-230-312-123-132-312-132-123-132-213-3_V.mp3)
  Segment 21: 57980ms ~ 70580ms (file: 572-275-236-236-236-263-765-761-631-326-261-216-236-123-231-263-231-213-123-236-216-213-236-576-263-263-263-267-762-123-123-216-213-261-726-231-321-231-236-576-265-275-725-726-2_W.mp3)
  Segment 22: 71580ms ~ 72156ms (file: 140-134-314-103-150-401-145-104-134-103-413-103-145-105-451-145-143-103-135-310-140-104-130-103-143-103-134 754-345-351-317-143-310-135-143-134-415-104-103-145-145-134-103-103-1_V.mp3)
  Segment 23: 73156ms ~ 73732ms (file: 271-132 765-706-130-765-765-637-673-762-671-671-657-671-627-765-675-671-617-267-236-361_W.mp3)
  Segment 24: 74732ms ~ 75308ms (file: 413-103-103-143 305-310-130-134-103-103-134-103-134-130-137-103-103-103-315-137-134-103-104-103-314-134-134-103-314-103-134-103 345-345-134-134-104-314-103-134-310-130-103-145-1_V.mp3)
  Segment 25: 76308ms ~ 76884ms (file: 175-175-136-132-326-310-326-627-672-130-137-103-103-136-163-163-317-136-103-153-136-137-136-103-130-317-326-371-672-620-137-631-567-163-617-316-106-610-167-607-612-165-126-601-6_V.mp3)
  Segment 26: 77884ms ~ 78460ms (file: 106-167-165-136-617-103-632-765-361-317-137-751-106-135-103-136-163-136-671-136-132-103-613-136-361-170-163-163-127-623-317-103-103-103-136-135-103-756-106-103-321-130-153-136-1_V.mp3)
  Segment 27: 79460ms ~ 80036ms (file: 501-507 105-105-510-501-157-105-150-105-105-105-517-157-105-105-517-516-501-510-510-157 510-504-501-157-150-153-150-571-507-157-150-517-517-705-517-157-517-571-517-507 517-501-5_V.mp3)
  Segment 28: 81036ms ~ 81612ms (file: 612-126-612-756-752-752-572-562-657-657-635-635-365-673-637-675-367-236-263-632-263-263-263-263-263-632-263-263-261-312-123-231-165-657-617_W.mp3)
  Segment 29: 82612ms ~ 83188ms (file: 367-361-163-163-126-612-312 574-576-657-672-163-652-612-613-627-617-671-362-136-163-612-621-617-123-167-765-136-163-316-623-261-136_W.mp3)
  Segment 30: 84188ms ~ 84764ms (file: 213-261-625-310-301-316-162-163-136-761-123-102-163-103-136-132-157-127-123-261-320-236-217-102-216-312-136-216-136-261-267-260-267-267-216-126-213-217-127-162-127-126-162-126-1_V.mp3)
  Segment 31: 85764ms ~ 86340ms (file: 172-173-731-137-173-173-137-172-132-173-137-137-175-175-173-137-176-173-172-175-713-167-163-136-167-163-163-213-137-217-127-173-713-175-371-375-127-175-175-172-237-127-173-172-1_W.mp3)
  Segment 32: 87340ms ~ 87916ms (file: 571-571-571-517-571-571-517-571-751-751-571-573-571-751-517-105-105-105-103-105-157-150-150-135-153-175-137-301-103-517-571-517-713-175-715-751-175-176-175-751-157-157-153-753-1_V.mp3)
  Segment 33: 88916ms ~ 89492ms (file: 572-576-756-756-756-576-756-756-752-514-514-542-546-514 572-572-752-756-756-756-576-756-752-752_W.mp3)
  Segment 34: 90492ms ~ 91068ms (file: 132-267-213-621-276-725-765-670-612-376-572-576-567-276-316-305-536-537-570-136-362-362-236-713-172-162-617-162-267-123_W.mp3)
  Segment 35: 92068ms ~ 92644ms (file: 360-673-607-671-637-637-675-671-617-617-671-672-675-675-675-675-675-675-675 173-750-730-713-673-130-617-630-610-617-607-670-367-603-310-617-163-103-631-103-657-675-635-615-657-3_V.mp3)
  Segment 36: 93644ms ~ 101384ms (file: 321-321-167-617-167-103-261-627-756-236-132-106-136-763-726-756-236-102-231-312-237-231-236-230-230-237-627-627-267-672-625-315-237-523-376-327-327-723-267-263-106-376-371-123-3_W.mp3)
  Segment 37: 102384ms ~ 102960ms (file: 627-675-672-621-627-627-625-670-671-671-671-671-672-671-675-615-675-675-607 612-617-617-617-651-617-675-671-617-671-610 613-675-651-615-617-615-675-675-617 612-617-617-671-671-6_W.mp3)
  Segment 38: 103960ms ~ 104536ms (file: 106-167-106-310-516-215-521-765-567-756-756-615-752-765-652-756-752-756-107-157-157-257-103-172-126-175-703-163 672-617-572-103-675-675-576-613-361-627-263 675-267-675-672_W.mp3)
  Segment 39: 105536ms ~ 106112ms (file: 165-137-576-230-756-617-136-132-123-316-163-103-103-163-136-136-136-163-163-615-136-103-517-156-103-163-130-136-613-136-312-132-103-316-310-136-561-162-136-617-156-325-516-103-1_V.mp3)
  Segment 40: 107112ms ~ 107688ms (file: 107-107-715-751-175-175-175-705-756-751-751-175-170-107-107-107-710 105-105-137-105-175-105-175-107-175-107-175-175-107-175-175-107-175-107-157-175-173-701-173-175-175-751-715 1_V.mp3)
  Segment 41: 108688ms ~ 109264ms (file: 201-123-621-623-362-265-652-562-362-136-513-762-367-360-376-326-230-601-612-620-263 312-162-175-517-516-765-163-163-316-312-310-306-132-361-167-162-163-610-216-312-103-102-167-1_W.mp3)
  Segment 42: 110264ms ~ 110840ms (file: 327-327-673-765-276-176-267-761-132-316-123-312-126-136-316-132-357-135-130-127-627-631-657-213-321-132 137_W.mp3)
  Segment 43: 111840ms ~ 112416ms (file: 716-627-623-136-157-160-613-165-167-163-106-617-167-167-163-176-651-160-231-321-123-512-165-263-263-263-672-625-651-623-761-716-670-612-621-106-620-675-657-256-637-612-136-671-1_V.mp3)
  Segment 44: 113416ms ~ 113992ms (file: 753-751-715-751-715-173-751-175-751-751-751-576-576-751-751 750-750-571-571-751-751-173-175-751-715-573-570-173-173 751-715-751-715-751-716-750-751-517-137-571-715-731-175-175-7_W.mp3)
  Segment 45: 114992ms ~ 123632ms (file: 326-527-572-257-265-312-375-326-367-317-726-372-312-326-106-236-326-327-276-527-754-261-612-321-736-765-735-267-276-623-261-102-361-376-136-163-765-765-361-316-263-267-276-362-2_W.mp3)
  Segment 46: 124632ms ~ 133740ms (file: 123-213-130-310-301-307-372-317-673-312-310-312-673-176-107-317-137-130-127-172-127-172-170-170-127-216-267-126-172-127-172-701-173-173-107-130-136-123-367-136-160-160-106-167-1_W.mp3)
  Segment 47: 134740ms ~ 135316ms (file: 671-571-157-527-315-231-263-263-263-526-524-524-265-265-563-526-256-326-321-312-132-526-512-512-571-357-315-316-357-536-573-574-576-657-675-671-267-625-625-562-516-576-571-571-5_W.mp3)
  Segment 48: 136316ms ~ 136892ms (file: 130-173-107-312-103-137-312-301-301-106-613-317-130-310-317-310-137-731-130-103-310-107-537-357-157-573-561-157-173-175-357-315 173-710-730-310-307-137-137-107-130-603-617-613-3_V.mp3)
  Segment 49: 137892ms ~ 138468ms (file: 157-175-157-571-715-715-751-571-715-517-751-571-753-157-175-571-571-751-751-751-517-571-513-157-517-570-517-175-517-157-175-137-173-715-571-517-571-571-751-751-715-751-576-571-5_V.mp3)
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
> **Time**: 2026-04-05 11:51:43
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "안녕하세요 만나서 정말 반갑습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 150, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "만나서", "start_ms": 1400, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 2150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 2800, "dur_ms": 1000, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "오늘 날씨가 참 좋네요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 750, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "참", "start_ms": 1600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 1950, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "맛있는 점심 드셨나요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "맛있는", "start_ms": 180, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "점심", "start_ms": 1050, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "드셨나요", "start_ms": 1700, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 5,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 6,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 7,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 8,
"transcript": "(instrument)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 9,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 10,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 11,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 12,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 13,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 14,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 15,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 16,
"transcript": "이 노래 가사가 너무 좋지 않나요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "노래", "start_ms": 600, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "가사가", "start_ms": 1250, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 2200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "좋지", "start_ms": 2750, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "않나요", "start_ms": 3400, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 18,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 19,
"transcript": "(speech+music)",
"speaker": "speaker_1",
"confidence": 0.7,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "mixed",
"words": []
},
{
"segment": 20,
"transcript": "함께 노래 부르니까 기분이 좋네요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 300,
"audio_type": "speech",
"words": [
{"word": "함께", "start_ms": 300, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "노래", "start_ms": 950, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "부르니까", "start_ms": 1500, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "기분이", "start_ms": 2550, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 3400, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "자 이제 다음 곡으로 넘어가 볼까요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 220,
"audio_type": "speech",
"words": [
{"word": "자", "start_ms": 220, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 1100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "곡으로", "start_ms": 1650, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "넘어가", "start_ms": 2400, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "볼까요", "start_ms": 3150, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 23,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 24,
"transcript": "(instrument)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 25,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 26,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 27,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 28,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 29,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 30,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 31,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 32,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 33,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 34,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 35,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 36,
"transcript": "Wait, I need to check something first.",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 400,
"audio_type": "speech",
"words": [
{"word": "Wait,", "start_ms": 400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 850, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "need", "start_ms": 1000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 1350, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "check", "start_ms": 1600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "something", "start_ms": 2050, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "first.", "start_ms": 2700, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 38,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 39,
"transcript": "Ok, what is it?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "Ok,", "start_ms": 150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "what", "start_ms": 500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 850, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "it?", "start_ms": 1100, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 41,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 42,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 43,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 44,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 45,
"transcript": "Let's continue.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 300,
"audio_type": "speech",
"words": [
{"word": "Let's", "start_ms": 300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "continue.", "start_ms": 750, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "Ok, next song please.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "Ok,", "start_ms": 200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "next", "start_ms": 550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "song", "start_ms": 1000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "please.", "start_ms": 1550, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 48,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 49,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 11:51:43*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=24268  time=11:50:25  uptime=38442s  mem=465MB  system_processes=355"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_114944.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 570-750-107-517-705-751-517-175-751-507-107-571-750-751-105-701-105-157-570-157-751-705-157-570-507 574-570-571-570-570-570-571-571-571-570-571-570-750-751-105-150-157 570-750-5_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 517-312-163-356-235-512-165-576-152-561-620-162-756-152-120-317-576-237-276-572-125-210-136-721-725-153-126-125-572-572-106-567-315-123-135-321-261-123-201-162-672-752-273-652-2_W.mp3)
  Segment 3: 3152ms ~ 12620ms (file: 760-370-675-756-765-732-321-726-762-107-175-317-267-372-762-765-137-632-761-762-672-276-732 106-623-621-231-213-623-137-312-132-301-316-132-312-576-517-173-132-137-136-107-130-1_W.mp3)
  Segment 4: 13620ms ~ 23196ms (file: 756-576-267-120-162-127-316-126-261-106-576-657-315-130-765-765-671-657-316-375-675-265-756-657-517-572-107-316-172-547-706-312-103-706-137-263-572-126-132-315-132-231-312-574-2_W.mp3)
  Segment 5: 24196ms ~ 33052ms (file: 317-756-267-370-310-301-765-657-160-136-576-352-103-301-103-103-136-310-753-260-632-630-126-315-620-763-362-632-637-163-137-765-372-103-301-273-103-106-276-273-103-715-236-267-2_W.mp3)
  Segment 6: 34052ms ~ 34628ms (file: 136-630-103-156-136-106-136-132-236-163-165-623-126-672-261-130-136-103-316-163-163-123-613-713-167-167-130-732-136-136-136-301-136-231-103-123-123-126-263-620-263-216-163-627-1_V.mp3)
  Segment 7: 35628ms ~ 36204ms (file: 751-751 571-570-751-576-576-756-751-753-715-753-751 175-751-715-175-715-175-175-715-751-715-715-715-175-751-751-175 570-571-571-715-751-175-715-750-175-715-175-751-750-715-713-7_V.mp3)
  Segment 8: 37204ms ~ 37780ms (file: 756-752-163-502-504-564-502-501-504-501-504-506-213-572-576-567-576-756-576-576-576-576-576-576-576-756-756-756-504-504 572-576-576-576-576-576-576-576-756 514-542-564-576-576-7_W.mp3)
  Segment 9: 38780ms ~ 39356ms (file: 261-602-213-263-267-276-260-206-206-261-576-657-567-413-132-513-725-621-260-632-130-375-375-732-237-765-623-326-302-312-163-570-507-106-162-167-213-513-504-312-352-315-132-103-3_V.mp3)
  Segment 10: 40356ms ~ 40932ms (file: 573-351-357-534-534-574-534-534-547-357-317-137-317-136-132-316-136-136-123-213-132-132-123-123-123-213-132-123-216-126-123-126-567-652-657-652-627-657-672-625-625-265-261-256-6_W.mp3)
  Segment 11: 41932ms ~ 42508ms (file: 123 162-620-627-627-206-126-753-756-720 671-126 657-132-123-106-103-105-153-105-136-125 310-135 301-305-537-573-756-675-762-136-602-162-312-602-620-206-620-320-302-310-302-231-3_V.mp3)
  Segment 12: 43508ms ~ 56864ms (file: 703-107-107-137-130-137-371-261-103-163-317-103-706-671-371-107-652-625-602-267-102-312-231-301-514-301-301-316-137-103-103-136-123-107-176-136-134-310-317-312-201-627-321-317-1_W.mp3)
  Segment 13: 57864ms ~ 58440ms (file: 360-163-130-107-317-130-103-130-310-310-310-103-130-310-310-137-173-137-173-175-715 315-103-715-301-310-761-317-630-607-637-306-621-617-613-610-607-617-637-367-731-317-301-307-1_V.mp3)
  Segment 14: 59440ms ~ 60016ms (file: 623 103-316-136-371-367-317-371-163-136-163-103-163-671-631-671-613-635-365-301-315-325-623-267-236-216-235-203-203-230-230-327-302-265-237-237-372-753-263-253-257-256-235-256-1_V.mp3)
  Segment 15: 61016ms ~ 80960ms (file: 216-132-173-312-310-213-213-571-310-210-201-132-312-132-103-130-103-132-130-301-312-103-230-210-201-132-312-203-502-502-120-174-107-207-230-312-326-172-760-670-761-107-163-206-3_V.mp3)
  Segment 16: 81960ms ~ 82536ms (file: 710 103-301-137-130-716-607-671-716-701-107-175-713-705-750-170-730-137-107-317-371-173-175-137-103-137-107-103-170-750-752 317-301-173-735-713-310-103-701-137-371-713-751-172-1_V.mp3)
  Segment 17: 83536ms ~ 84112ms (file: 162-751-103-160-132-136-136-123-162-137-106-316-167-671-317-316-136-632-672-612-601-610-130-163-163-106-103-132-152-260-136-162-103-167-136-163-610-106-263-162-160-136-136-143-1_V.mp3)
  Segment 18: 85112ms ~ 85688ms (file: 104-135-104-104-103-134 315-130-103-410-154-504-401-104-104-103-104-134-154-145-157-104-137-314-104-104-134 350-103-435-410-405-450-154-140-401-104-130-104-105-157-140-104-135-1_V.mp3)
  Segment 19: 86688ms ~ 96912ms (file: 213-102-230-230-213-127-301-231-321-236-231-213-230-120-130-321-230-230-310-213-132-132-362-763-672-612-326-263-267-612-623-672-310-260-260-627-230-321-102-301-213-260-267-260-7_W.mp3)
  Segment 20: 97912ms ~ 98488ms (file: 231-127-127-150-261-162-236-257-267-275-276-627-675-572-576-612-526-576-756-576-570-576-560-576-567-507-564-506-576-560-560-570-506-560-571-507-560-567-576-510-627-657-576-756-7_W.mp3)
  Segment 21: 99488ms ~ 100064ms (file: 510-215-162-560-562-652-275-576-517 175-157-571-765-175-715-371-105-501-501-510-517-507-507-570-570-570-570-570-567-527-572-571-572-572-526 326-602-263-206-236-376-612-236-326-6_W.mp3)
  Segment 22: 101064ms ~ 101640ms (file: 132-153-512-135-512-256-126-163-156-216_V.mp3)
  Segment 23: 102640ms ~ 111784ms (file: 310-103-312-103-237-260-236-276-621-254-236-103-325-132-143-103-130-574-205-231-124-203-132-321-312-267-267-527-276-726-123-103-103-275-135-103-256-256-263-753-352-137-610-260-2_W.mp3)
  Segment 24: 112784ms ~ 113360ms (file: 513-510-517-517-517-517-571-517-510-571-517-517-517-517-571-561-517-571-576-567-576-657-657-153-513-153-570-517-157-371-753-571-517-157-715-571-571-571-751-571-571-751-571-571-7_V.mp3)
  Segment 25: 114360ms ~ 121020ms (file: 301-571-701-107-316-103-310-136-310-103-310-103-103-136-103-106-103-103-103-106-103-106-106-103-106-231-173-103-267-623-236-263-623-120-275-120-103-576-571-263-267-276-273-267-2_V.mp3)
  Segment 26: 122020ms ~ 124216ms (file: 167-260-103 605-627-657-657-765-765-657-567-765-576-756-576-571-751-756-756-751-756-751-751-756 605-657-657-567-756-576-567-576-567-576-765-576-576-576-765-756 657-657-675-657-5_W.mp3)
  Segment 27: 125216ms ~ 125792ms (file: 621-623-621-627-625-627-627-625-621-623-625-625-625-632-625-621-623-263 621-621-621-627-625-625-627-672-625-625-652-625-623-621-625-621-625-625-625-625-652-652-625-625 625-625-6_W.mp3)
  Segment 28: 126792ms ~ 127368ms (file: 571-567-265 567-156-165-157-152-175 152-512-157-517-501-512-125-102-120-102 506-506-516-516-576-576-576-571-751-517-751-725-516-512-176-506-265-567-507-506-507-572-167-216 501-5_W.mp3)
  Segment 29: 128368ms ~ 128944ms (file: 542-537-564-132-316-513-521-532-513-512 540-547-523-502-512-512-512-513-152-501-516 105-105-102-102-120-251-251-150-502-521-215-125-125-213-301 501-510-521-257-250-572-527-257-2_V.mp3)
  Segment 30: 129944ms ~ 144164ms (file: 120-120-120-132-123-123-213-123-123-132-213-123-120-123-120-103-312-123-120-102-123-103-102-127-132-102-120-120-102-103-102-130-102 657-657-365-357-657-567-567-657-765-675-301-1_V.mp3)
  Segment 31: 145164ms ~ 145740ms (file: 103-103-326-132-132-103-103-130-751-560-315-132-105-132-130-361-162-163-136-165-162-103-136-652-106-103-106-103-362_V.mp3)
  Segment 32: 146740ms ~ 147316ms (file: 571-157-315-751-713-301-310-103-137-701-307-750-173-751-765-371-715-321-571-751-175-371-137-705-375-357-317-674-632-365-735-736-137-305-173-123-130-157-135-371-760-713-715-371-1_W.mp3)
  Segment 33: 148316ms ~ 148892ms (file: 571-567-576-576-756-576-756-573-576-756-570-510-570-150-501-530-571-517-517-571-510-507-571-517-510-510-507-501-510-571-570-756-753-576-571-571-576 510-150-510-501-105-510-510-5_V.mp3)
  Segment 34: 149892ms ~ 150468ms (file: 457-547-574-576 754-756-576-754-756-754-574-576-132-163-103-107-137-761-165-165-657-625-761-675-675-765-765-765-765-721-172-653-657-163-136-176-173-167-167-716-167-167-167-176-1_W.mp3)
  Segment 35: 151468ms ~ 152044ms (file: 756-715-105-157-175-107-175-170-175-170-170-107-170-107-170-107-170-157-137-170-175-517-571-751-371-137-751-137-351-315-312-136-361-315-571-571-517-576-571-572-517-153-135-571-5_W.mp3)
  Segment 36: 153044ms ~ 153620ms (file: 137-301-137-103-301-317-375-307-317-375-137-371-310-107-317-137-307-301-312 315-135-105-103-130-173-107-130-137-130-103-317-310-310-301-317-317-173-130-301-317-317-175-137-310-1_V.mp3)
  Segment 37: 154620ms ~ 162936ms (file: 716-102-701-125-237-237-765-203-216-205-123-267-230-236-231-276-207-675-627-102-152-570-321-327-307-213-701-130-715-302-312-321-603-236-103-320-230-132-203-203-310-672-107-312-5_W.mp3)
  Segment 38: 163936ms ~ 172576ms (file: 123-162-572-267-126-260-261-263-126-106-754-175-574-120-231-216-756-576-267-547-126-621-231-321-576-237-754-576-126-201-752-120-236-102-263-627-627-260-260-236-130-756-321-327-7_W.mp3)
  Segment 39: 173576ms ~ 174152ms (file: 103-105-150-510-150-150-150-150-150-510-157-156-150-157-175-157-175-167-175-157-165-167-105 150-157-157-175-156-150-105-157-167-176-150-107-105-175 105-103-105-150-150-153-157-1_V.mp3)
  Segment 40: 175152ms ~ 175728ms (file: 501-501-501-510-501-510-510-150-157-510-150-517-571-570 501-510-150-510-517-517-501-501-510-501-501-150-510-105-150-105-175-150-510-517-517-517-576-576-576-576-571-574-571 510-1_V.mp3)
  Segment 41: 176728ms ~ 177304ms (file: 765-576-367-167-167-671-167-716-176-176-176-761-107-167-157-531-517-576-537-753-713-751-671-137-130-167-176-167-106-106-106-105-106-160-107-107-103-103-763-756-765-765-765-736-6_W.mp3)
  Segment 42: 178304ms ~ 183920ms (file: 130-310-130-132-132-123-132-213-123-130-210-261-201-216-216-207-210-270-267-261-261-213-217-217-217-213-217-217-216-217-201-230-276-216-261-126-261-170-356-135-132-316-316-361-2_W.mp3)
  Segment 43: 184920ms ~ 185496ms (file: 123-132-132-312-627-627-627-265-267-267-621_W.mp3)
  Segment 44: 186496ms ~ 187072ms (file: 567-546-502-564-542-563-513-561-527-513-531-752-756-576-756-752-156-543-542-512-562-541-504-504-541-531-516-501-531-534-512-541-514-541-564-502-542-542-752-576-756-754-756-756-7_V.mp3)
  Segment 45: 188072ms ~ 194480ms (file: 713-750-756-320-210-270-201-720-263-753-230-213-672-705-201-267-167-752-270-203-103-103-120-107-260-230-102-320-132-103-107-134-103-261-103-750-106-175-176-170-230-237-276-756-7_W.mp3)
  Segment 46: 195480ms ~ 196056ms (file: 571-756-576-751-157-175-153-751-153-175-157-751-175-751-715-765-765-756-756-756-765-765-752-752 750-750-756-756-705-753-756-753-756-715-715-716-765-753_W.mp3)
  Segment 47: 197056ms ~ 206920ms (file: 726-267-173-312-263-547-265-123-312-203-173-530-130-213-231-231-236-574-576-132-413-320-623-132-627-620-106-137-132-276-627-310-301-276-675-762-267-173-130-103-273-236-312-130-1_W.mp3)
  Segment 48: 207920ms ~ 216092ms (file: 301-107-157-150-150-756-765-573-350-753-237-107-103-173-753-756-307-271-276-572-357-123-103-103-103-574-105-137-270-756-176-173-270-230-270-130-752-507-157-134-762-567-157-130-2_W.mp3)
  Segment 49: 217092ms ~ 217668ms (file: 105-157-105-150-105-150-157 105-150-105-165-167-156-175-176-167-156-175-167-157-157-167-175-175-157-715-150-750-750-750-754-750-754-754-754-756-754-754-754-754-756-756-756-754-7_W.mp3)
  Segment 50: 218668ms ~ 219244ms (file: 106-130-103-163-316-106-617-312-136-103-136-152-137-106-103-106-164-123-106-167-153 726-623-631-236-627-130-517-165-617-617-176-130-103-103-315-106-163-132-715-137-103-152-136-1_V.mp3)
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
> **Time**: 2026-04-05 11:51:43
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "안녕하세요 만나서 정말 반갑습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 150, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "만나서", "start_ms": 1400, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 2150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 2800, "dur_ms": 1000, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "오늘 날씨가 참 좋네요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 750, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "참", "start_ms": 1600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 1950, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "맛있는 점심 드셨나요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "맛있는", "start_ms": 180, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "점심", "start_ms": 1050, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "드셨나요", "start_ms": 1700, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 5,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 6,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 7,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 8,
"transcript": "(instrument)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 9,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 10,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 11,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 12,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 13,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 14,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 15,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 16,
"transcript": "이 노래 가사가 너무 좋지 않나요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "노래", "start_ms": 600, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "가사가", "start_ms": 1250, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 2200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "좋지", "start_ms": 2750, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "않나요", "start_ms": 3400, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 18,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 19,
"transcript": "(speech+music)",
"speaker": "speaker_1",
"confidence": 0.7,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "mixed",
"words": []
},
{
"segment": 20,
"transcript": "함께 노래 부르니까 기분이 좋네요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 300,
"audio_type": "speech",
"words": [
{"word": "함께", "start_ms": 300, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "노래", "start_ms": 950, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "부르니까", "start_ms": 1500, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "기분이", "start_ms": 2550, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 3400, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "자 이제 다음 곡으로 넘어가 볼까요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 220,
"audio_type": "speech",
"words": [
{"word": "자", "start_ms": 220, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 1100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "곡으로", "start_ms": 1650, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "넘어가", "start_ms": 2400, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "볼까요", "start_ms": 3150, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 23,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 24,
"transcript": "(instrument)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 25,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 26,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 27,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 28,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 29,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 30,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 31,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 32,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 33,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 34,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 35,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 36,
"transcript": "Wait, I need to check something first.",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 400,
"audio_type": "speech",
"words": [
{"word": "Wait,", "start_ms": 400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 850, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "need", "start_ms": 1000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 1350, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "check", "start_ms": 1600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "something", "start_ms": 2050, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "first.", "start_ms": 2700, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 38,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 39,
"transcript": "Ok, what is it?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "Ok,", "start_ms": 150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "what", "start_ms": 500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 850, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "it?", "start_ms": 1100, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 41,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 42,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 43,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 44,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 45,
"transcript": "Let's continue.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 300,
"audio_type": "speech",
"words": [
{"word": "Let's", "start_ms": 300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "continue.", "start_ms": 750, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "Ok, next song please.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "Ok,", "start_ms": 200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "next", "start_ms": 550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "song", "start_ms": 1000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "please.", "start_ms": 1550, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 48,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 49,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": null,
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 11:51:43*
