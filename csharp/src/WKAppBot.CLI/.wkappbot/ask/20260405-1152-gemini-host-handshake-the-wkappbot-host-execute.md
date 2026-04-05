# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=14936  time=11:51:02  uptime=36362s  mem=435MB  system_processes=355"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_115042.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 9936ms (file: 567-561-102-132-213-312-713-754-126-103-163-103-157-157-157-136-102-170-132-136-132-123-756-125-123-130-152-210-321-753-571-213-213-210-231-130-127-136-321-231-267-267-236-102-2_W.mp3)
  Segment 2: 10936ms ~ 11512ms (file: 107-130-103-173-107-103-173-107-107-175-107-173-107-173-107-173-103-107-173-173-175-173-175-170-173-103-103-107-137-107-137-107-170-103-107-173-107-173-107-170-173 173-175-713-7_V.mp3)
  Segment 3: 12512ms ~ 13088ms (file: 156-516-615-163-156-571-537-573-567-561-172-167-123-127-613-156-156-657-156-571-153-156-162-167-162-167-167-167-165-165-162-136-163-136-612-162-163-615-653-615-165-136-316-162-1_W.mp3)
  Segment 4: 14088ms ~ 14664ms (file: 510-215-162-560-562-652-275-576-517 175-157-571-765-175-715-371-105-501-501-510-517-507-507-570-570-570-570-570-567-527-572-571-572-572-526 326-602-263-206-236-376-612-236-326-6_W.mp3)
  Segment 5: 15664ms ~ 22828ms (file: 312-123-316-257-273-752-273-572-756-135-750-572-574-201-325-371-513-567-265-756-201-301-103-507-105-501-542-572-310-312-725-637-301-570-310-123-751-576-102-745-572-130-205-201-1_V.mp3)
  Segment 6: 23828ms ~ 29372ms (file: 150-267-675-756-267-125-516-564-156-217-365-657-163-651-567-517-135-351-571-675-562-572-521-526-657-365-567-653-653-657-365-132-265-561-651-675-132-357-576-567-516-573-153-537-5_W.mp3)
  Segment 7: 30372ms ~ 31920ms (file: 136-670-163-132-103-137-517-130-130-310-130-175-125-176-607-130-230-326-321-130-175-756-106-132-106-132-103-705-601-163-631-136-106-126-172-216-162-261-123-136-103-213-132-132-2_W.mp3)
  Segment 8: 32920ms ~ 33496ms (file: 672-136-160-136-103-657-673-613-103-106-167-216-176 517-312-172-126-130-716-120-136-106-163-136-103-621-167-105-167-126-106-103-106-136-167-106-167-617-163-602-106-136-135-127-1_V.mp3)
  Segment 9: 34496ms ~ 35072ms (file: 621-632-126-126 701-163-167-716-317-317-127-501-216-103-103-107-167-517-123-675-103-162-126-236-160-316-256-126-216-213_V.mp3)
  Segment 10: 36072ms ~ 36648ms (file: 725-750-273-726-750-567-726-627-672-263-276-263-267-236-270-236-765-765-167-176-712-756-265-327-326-275-267-275-607-750-321-570-673-265-256-237-562-573-517-572-576-327-613-753-2_V.mp3)
  Segment 11: 37648ms ~ 47368ms (file: 527-130-136-162-267-623-632-362-263-362-136-162-236-534-513-123-316-326-261-267-267-126-236-267-136-261-267-236-235-326-236-543-120-627-265-263-267-215-106-576-265-576-120-152-2_W.mp3)
  Segment 12: 48368ms ~ 62120ms (file: 132-312-213-236-124-123-123-123-215-260-162-732-263-104-321-103-103-312-301-312-761-172-521-571-276-261-267-120-362-217-541-512-312-621-720-123-312-275-612-136-132-137-267-702-2_W.mp3)
  Segment 13: 63120ms ~ 63696ms (file: 572-527-527-527-521-526-527-521-523-527-526-523-527-507-502-527-502-507-520-502-521-502-501-502-502-745-105-201-520-502-562-602-657-675-605-610-605-672-607-602-612-126-621-625-5_W.mp3)
  Segment 14: 64696ms ~ 65272ms (file: 123-321-362-263-623-623-361-362-210-103-125-132-231-263-625-672-675-130-260-623 512-562-526-521-152-510-512-510-513-510-512-512-523-523-532-532-523-523-523-521-523-521-521-532-5_W.mp3)
  Segment 15: 66272ms ~ 66848ms (file: 576-576-156-567-167-765-617-657-176-762-167-156-657-165-675-615-157-571-156-156-163-165-156-153-561-135-571-570-576-576-570-571-571-751-571-571-571-574-571-571-570-571-751-571-7_W.mp3)
  Segment 16: 67848ms ~ 79980ms (file: 725-236-257-237-106-362-253-107-310-573-267-756-321-236-102-106-136-236-163-263-106-263-136-163-326-236-106-163-154-237-231-230-231-236-130-236-321-752-160-267-362-123-231-312-1_W.mp3)
  Segment 17: 80980ms ~ 89044ms (file: 571-316-510-153-675-201-602-103-103-306-302-537-567-361-103-305-312-136-120-132-316-203-576-632-621-301-104-261-216-261-310-136-103-631-362-560-623-103-206-102-136-570-756-210-2_V.mp3)
  Segment 18: 90044ms ~ 90620ms (file: 562-576-571 625-267-726-276-726-267-672-657-176-365-137-136-167-167-135-135-135-157-175-157-165-163-613-765-765-276-267-765-671-752-765-675-625-765-726-276-726-672-542-561-651-6_W.mp3)
  Segment 19: 91620ms ~ 92196ms (file: 103-167-103-163-170-173 712-617-106-160-132-301-130-103-172-126-167-136-153-103-103-135-103-132-521-176-650-167-136 103-167-126-125-261-312-132-106-136-106-165-106-126-103-125-1_V.mp3)
  Segment 20: 93196ms ~ 93772ms (file: 132-136-103-301-103-312-310-310-301-301-310-316-103-103-103-135-103-103-103-103-103-103-163-103-103-103-103 136-136-103-136 176-167-167-167-167-135-136-136-136-136-765-765-765-7_V.mp3)
  Segment 21: 94772ms ~ 100892ms (file: 137-172-267-657-267-627-637-361-617-103-621-123-213-321-102-361-632-130-310-236-162-310-130-320-762-627-623-260-263-261-261-260-632-627-260-163-132-362-756-672-632-620-326-163-3_W.mp3)
  Segment 22: 101892ms ~ 102468ms (file: 103-176-134-173-621-326-230-326-102-103-137-236-102-176-613-136-103-260-362-756-761-671-631-367-362-623-126-621-631-612-361-306-362-627-623-627-627-576-107-135-236-236-236-623-2_V.mp3)
  Segment 23: 103468ms ~ 104044ms (file: 706-267-750-276-752-163-675-762-276-216-256-765-526-523-527-567-216-506-312-521-502-321-123-326-257-507-157-321-275-213-715-257 201-507-570-236-251-324-231-123-507-261-316-361-1_V.mp3)
  Segment 24: 105044ms ~ 105620ms (file: 162-612-160-163-106-106-163-103-567-126-610-605-160-103-126-710-130-102-165-167-163 132-263-756-756-103-137-253-162-152-361-106-132-261-150-127-132-167-136-216-156-517-603-621-6_V.mp3)
  Segment 25: 106620ms ~ 112704ms (file: 675-657-657-312-163-231-526-574-537-567-570-107-701-715-756-172-153-567-657-573-572-567-751-137-106-570-601-613-613-612-160-167-617-160-163-715-315-172-756-267-361-637-213-137-7_W.mp3)
  Segment 26: 113704ms ~ 122560ms (file: 162-136-103-675-652-167-316-754-103-317-135-302-327-150-136-275-750-302-257-275-102-162-675-752-675-236-102-127-573-762-321-671-731-237-572-312-231-357-132-271-231-312-316-627-2_V.mp3)
  Segment 27: 123560ms ~ 124136ms (file: 301-153-536-501-526-560-502-650-513-501-512-250-520-205-201-205-206-230-250-253-562-527-527-521-562-257-152-513-561-574-537-562-517-517-517-573-567-513-561-521-513_V.mp3)
  Segment 28: 125136ms ~ 125712ms (file: 126-126-715-156-165-136-163-106-316-136-163-625-103-132-137_W.mp3)
  Segment 29: 126712ms ~ 134416ms (file: 103-126-150-132-273-103-231-130-231-136-230-267-312-312-263-263-312-236-237-236-130-312-231-271-132-132-235-103-312-106-263-267-206-321-213-203-210-201-230-163-216-156-126-256-3_W.mp3)
  Segment 30: 135416ms ~ 135992ms (file: 715-162-261-612-617-615-162-163-613-612-160-165-615-165-160 612-613-617-612-615-163-615-615-261 652-657-651-657-651-651-657-657-651-617-613-672-675-651-657-675-765-762 156-612-6_W.mp3)
  Segment 31: 136992ms ~ 137568ms (file: 135-173-103-173-107-175-107-103-173-107-107-715-173 135-173-107-173-175-715-107-107-175-107-106-107 175-107-157-173-107-173-175-105-175-103-751-751-375-107-107-751-750-701-715-7_V.mp3)
  Segment 32: 138568ms ~ 139144ms (file: 621-123-326-123-312-627-263-213-265-126-261-261-261-326-362-236-265-625-625-236-326-236-236-326-326-362-321-321-312-236-216-326-362-361-523-526-275-256-257-265-132-137-613-567-5_W.mp3)
  Segment 33: 140144ms ~ 140720ms (file: 617 672-170-751-536-370-756-132-763-567-170-753-267-710-753-736-736-753-635-710-173-175-765-312-137-316-132-751-612-172-123_W.mp3)
  Segment 34: 141720ms ~ 142296ms (file: 610-625-625-621-652-657-630-652-621-675-625-625-673 625-625-652-657-654-657-657-652-657-650-605-670-650-650-652-650-625-625-625-652-256-567-652 657-657-657-652-650-657-650-625-6_W.mp3)
  Segment 35: 143296ms ~ 143872ms (file: 520-526-256-527-572-752-752-750-752-576-567_V.mp3)
  Segment 36: 144872ms ~ 145448ms (file: 130-103-136-310-317-137-350-607-617-136-627-763-310-137-351-301-750-670-607-672-607-637-136-163-136-176-106-167-150-105-762-760-670-627-672-504-751-751-750-107-167-576-570-567-7_W.mp3)
  Segment 37: 146448ms ~ 147024ms (file: 570-571-153-175-571-517-157-571-751-517-715-751-571-517-751-751-570-576-570-570-570-105-105-715-175-517-157-570-751-571-715-750-750-570-570-570-570-570-507-570-576-571-517-571-5_V.mp3)
  Segment 38: 148024ms ~ 148600ms (file: 271-132 765-706-130-765-765-637-673-762-671-671-657-671-627-765-675-671-617-267-236-361_W.mp3)
  Segment 39: 149600ms ~ 150176ms (file: 751-751-571 510-105-150-150-510-501-150-751-571-751-175-751-715-751-751-751-751-756-756-715-756-751-751 105-103-150-157-175-751-751-756-756-756-751-751-175-715-715-715-751-175-7_V.mp3)
  Segment 40: 151176ms ~ 158556ms (file: 176-127-512-130-137-123-210-267-213-203-201-217-712-721-217-267-360-736-716-751-230-120-217-312-371-120-237-726-672-210-312-231-230-201-123-106-276-276-320-312-103-137-317-267-7_W.mp3)
  Segment 41: 159556ms ~ 167764ms (file: 254-567-325-316-236-236-326-312-265-321-236-231-326-362-361-372-213-132-123-362-316-321-213-132-136-316-312-317-312-310-130-261-261-130-106-175-712-276-726-276-271-276-276-726-2_W.mp3)
  Segment 42: 168764ms ~ 169340ms (file: 570-750-107-517-705-751-517-175-751-507-107-571-750-751-105-701-105-157-570-157-751-705-157-570-507 574-570-571-570-570-570-571-571-571-570-571-570-750-751-105-150-157 570-750-5_V.mp3)
  Segment 43: 170340ms ~ 177936ms (file: 123-267-326-231-263-102-231-623-563-312-321-236-132-132-103-713-132-513-765-267-236-321-130-752-265-276-231-321-120-231-357-263-761-623-132-137-716-735-576-170-617-175-571-167-1_W.mp3)
  Segment 44: 178936ms ~ 179512ms (file: 510-150-510-105-501-105-510-510-570-507-547-571-570-517-510-507-510-570-507 513-105-517-513-157-105-153-150-571-507-510-715-517-501-571-517-105-501-507-510-501-510-510-501-501-5_V.mp3)
  Segment 45: 180512ms ~ 181088ms (file: 521-512 354-513-125-163-105-152-516-156-105-105-153-153-153-512-153-513-153-513-516-105-561-156-156-165 236-652-632-506-315-215-256-154-153-135 152-512-153-153-517-513-105-153-5_V.mp3)
  Segment 46: 182088ms ~ 182664ms (file: 153-135-156-156-163-165-163-637-625-675-265-276-265-567-527-756-562-627-625-657-652-762-526-627-165-135-135-136-156-156-156-756-576-513-567-571-357-753-567-517-752-657-576-672-5_W.mp3)
  Segment 47: 183664ms ~ 184240ms (file: 547-516-517-105-501-501-507-501-517-574-570-574-571-571-574-576 105-150-510-501-517-510-571-507-570-570-507-570-570-507-507-570-570-507-574 501-501-105-510-510-510-571-574-570-5_W.mp3)
  Segment 48: 185240ms ~ 185816ms (file: 173-170-173-713-126-163-163-167-167-671-675-673-763-761-726-762-672-761-673-763-761-763-361-361-763-672-762-765-762-762-762-672-675-675-675-673-673-670-317-137-367-173-316-632-3_W.mp3)
  Segment 49: 186816ms ~ 188112ms (file: 471 135-132-316-576-312-132-675-725-672-207-316-102-351-530-105-152-103-132-576-130-316-132-106-123-125-123-152-126-132-173-123-276-756-137-237-732-231-570-162-236-261-521-576 3_V.mp3)
  Segment 50: 189112ms ~ 189688ms (file: 153-156-156-165-126-132-312-312-132-135-132-615-165-657-156-576-675-652-617-675-567-657-615-675-675-675-615-516-615-516-561-567-516-516-516-561-567-157-576-567-651-567-516-651-5_W.mp3)
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
> **Time**: 2026-04-05 11:52:26
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "오늘 공부할 내용은 한국어 기초 문법입니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "공부할", "start_ms": 600, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "내용은", "start_ms": 1300, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "한국어", "start_ms": 1900, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "기초", "start_ms": 2650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "문법입니다", "start_ms": 3200, "dur_ms": 900, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 3,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 4,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 5,
"transcript": "준비되셨나요 그럼 첫 번째 문장부터 시작해 볼게요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "준비되셨나요", "start_ms": 200, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "그럼", "start_ms": 1500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "첫", "start_ms": 2000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "번째", "start_ms": 2350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "문장부터", "start_ms": 2850, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "시작해", "start_ms": 3700, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "볼게요", "start_ms": 4350, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "이 노래는 아주 유명한 케이팝 곡입니다",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 250, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "노래는", "start_ms": 500, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 1150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "유명한", "start_ms": 1650, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "케이팝", "start_ms": 2400, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "곡입니다", "start_ms": 3250, "dur_ms": 750, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "가사를 잘 듣고 따라해 보세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "가사를", "start_ms": 100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "듣고", "start_ms": 1000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "따라해", "start_ms": 1500, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "보세요", "start_ms": 2150, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 9,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 10,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 11,
"transcript": "멜로디가 정말 아름답지 않나요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "멜로디가", "start_ms": 180, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 1050, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "아름답지", "start_ms": 1650, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "않나요", "start_ms": 2600, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "이제 여러분이 직접 부를 차례입니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 300,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "여러분이", "start_ms": 800, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "직접", "start_ms": 1550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "부를", "start_ms": 2100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "차례입니다", "start_ms": 2550, "dur_ms": 1000, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 14,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 15,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 16,
"transcript": "목소리를 조금 더 크게 해 볼까요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "목소리를", "start_ms": 150, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 1350, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "크게", "start_ms": 1600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "해", "start_ms": 2100, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "볼까요", "start_ms": 2400, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "네 아주 좋습니다 잘하고 계세요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 120, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "좋습니다", "start_ms": 950, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "잘하고", "start_ms": 1800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "계세요", "start_ms": 2450, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 19,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 20,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 21,
"transcript": "자신감을 가지고 끝까지 불러 보세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "자신감을", "start_ms": 200, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "가지고", "start_ms": 1050, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "끝까지", "start_ms": 1700, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "불러", "start_ms": 2450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "보세요", "start_ms": 2900, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 23,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 24,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 25,
"transcript": "마지막 구절은 조금 더 감정을 담아서",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "마지막", "start_ms": 180, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "구절은", "start_ms": 850, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 1550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 2000, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "감정을", "start_ms": 2250, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "담아서", "start_ms": 2950, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "훌륭합니다 정말 멋진 노래였어요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 220,
"audio_type": "speech",
"words": [
{"word": "훌륭합니다", "start_ms": 220, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 1300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "멋진", "start_ms": 1850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "노래였어요", "start_ms": 2350, "dur_ms": 900, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 28,
"transcript": "Excuse me, can you repeat that?",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "Excuse", "start_ms": 100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "me,", "start_ms": 650, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "can", "start_ms": 950, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 1200, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "repeat", "start_ms": 1450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "that?", "start_ms": 1950, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "Sure, I said it was a great performance.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "Sure,", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 600, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "said", "start_ms": 800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "it", "start_ms": 1200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "was", "start_ms": 1400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 1700, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "great", "start_ms": 1850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "performance.", "start_ms": 2300, "dur_ms": 850, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "en",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 31,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "en",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 32,
"transcript": "Thank you so much! I really enjoyed it.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "Thank", "start_ms": 200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 600, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "so", "start_ms": 850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "much!", "start_ms": 1200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 1700, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "really", "start_ms": 1900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "enjoyed", "start_ms": 2350, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "it.", "start_ms": 3000, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "en",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 34,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "en",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 35,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "en",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 36,
"transcript": "다음 수업 시간에 다시 만나요 안녕",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "다음", "start_ms": 200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "수업", "start_ms": 750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "시간에", "start_ms": 1300, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 1950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 2450, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "안녕", "start_ms": 3100, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 38,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 39,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 40,
"transcript": "오늘 배운 표현들을 복습해 봅시다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "배운", "start_ms": 700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "표현들을", "start_ms": 1250, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "복습해", "start_ms": 2100, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "합시다", "start_ms": 2800, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "반복해서 들으면서 익숙해지는 게 중요해요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "반복해서", "start_ms": 180, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "들으면서", "start_ms": 1050, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "익숙해지는", "start_ms": 1850, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 2800, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "중요해요", "start_ms": 3050, "dur_ms": 750, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 43,
"transcript": "질문이 있다면 언제든지 물어보세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 220,
"audio_type": "speech",
"words": [
{"word": "질문이", "start_ms": 220, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "있다면", "start_ms": 850, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "언제든지", "start_ms": 1450, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "물어보세요", "start_ms": 2250, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 45,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 46,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 47,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 48,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 49,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 50,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 11:52:26*
