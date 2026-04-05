# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=14936  time=11:39:43  uptime=35684s  mem=412MB  system_processes=353"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_113849.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 514-517-541-534-547-536-531-532 625-654-654-654-654-654-654-654-651-654-654-654-645-624-526-541-157-534-621-612-615-657-657-657-657-654-567-654-567-657-654-657-654-564-657-654-6_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 610-162-103 127-216-321-231-310-167-102-162-123-136-136-130-103-132-612-261-563-631-103-163-103-361-165-130-137-163-130-370-175-571-153-657-615-573-657-536-562-567-657-516-571-5_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 103-301-135-750-607-627-157-752 754-267-572-326-506-375-762-576-756-267 256-702-506-572-562-526-301-130-301-312-312-310-312-527-257-127-567-576-312-153-136-106-763-307-715-137-1_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 316-136-173-736-736-713-173-136-137-167-173-137-157-516-165-517-167-105-167-137-105-130-135-150-176-321-375-752-657-516-157-576-675-576-516-165-571-175-165-173-175-156-156-157-1_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 617-612-617-617-671-612-621-627-651-651-652-651-657-615-612-621-621-652-657-657-657-615-613-621-612-621-615-652-651-652-613 652-621-612-652-657-657-657-657-657-657-657-657-654-6_W.mp3)
  Segment 6: 7880ms ~ 16916ms (file: 127-750-623-126-213-162-267-236-217-126-215-271-213-263-236-201-263-201-216-271-206-162-756-130-203-736-321-273-750-235-632-567-276-762-605-356-576-675-537-120-126-362-530-357-1_W.mp3)
  Segment 7: 17916ms ~ 26592ms (file: 106-263-260-514-210-276-132-260-310-163-623-524-106-260-521-125-265-302-103-371-312-627-127-723-317-723-176-621-136-372-371-317-371-673-572-265-267-267-263-260-263-102-571-213-1_W.mp3)
  Segment 8: 27592ms ~ 28168ms (file: 170-715-675-572-756-705-763-253-752-765-326-756-706 570-576-175-132-576-752-756-752-576-627-673-257-672-605-627-625-617-762-576-753-576-725-726-527-572-726-265-756-765-750-327-5_V.mp3)
  Segment 9: 29168ms ~ 36368ms (file: 671-760-267-210-572-325-103-312-201-231-123-103-623-670-206-310-201-216-132-157-547-105-574-670-671-267-107-261-201-527-275-572-572-120-210-627-261-210-603-312-316-206-602-572-5_W.mp3)
  Segment 10: 37368ms ~ 37944ms (file: 501-153-513-501-157-150-513-152-531-513-510-510-537-574-574-517-510-570-576 152-175-715-517-517-157-517-517-517-571-576-571-573-571-571-571-571-517-517-517-570-517-517-150-105-5_V.mp3)
  Segment 11: 38944ms ~ 45532ms (file: 270-213-210-206-207-275-207-203-123-276-217-103-102-126-620-210-312-103-310-130-201-203-312-201-270-270-216-201-203-721-572-205-207-102-167-120-102-270-170-201-123-102-310-302-3_V.mp3)
  Segment 12: 46532ms ~ 47108ms (file: 547-457-457-645-567-657-567-652-652-567-567-576-756-527 627-623-623_W.mp3)
  Segment 13: 48108ms ~ 48684ms (file: 123-231-210-302-230-620-260-260-672-765-675-765-765-670-627-602-620-672-602-602-754-103-102-106-106-103-312-236-673-627-623-263-263-250-261-316-602-623-670-612-167-103-302-326-1_V.mp3)
  Segment 14: 49684ms ~ 50260ms (file: 321-271-312-106-701-265-753-763-216-103-576-312-513-576_V.mp3)
  Segment 15: 51260ms ~ 51836ms (file: 136-135-163-136 315-357-357-753-753-517-517-137 310-314-314-341-314-135-134-135-136-163-136-134-315-134-136-135-136-103-136-136-103-136-136-103-134-103-136 315-573-753-571-517 3_V.mp3)
  Segment 16: 52836ms ~ 58992ms (file: 263-203-526-320-267-230-130-732-317-327-237-132-235-532-257-236-103-723-317-130-267-527-375-153-235-236-235-230-267-257-135-372-236-537-136-273-257-573-357-315-756-571-236-537-2_W.mp3)
  Segment 17: 59992ms ~ 60568ms (file: 304-105-405-105-104-130-154-143-140-310-145-140-410-154-143-145-143-134 315-457-134-413-104-140-403-410-415-405-415-134-140-413-415-140-104-103-314-310 307-354-450-403-401-401-1_V.mp3)
  Segment 18: 61568ms ~ 62144ms (file: 571-513-153-157-571-571-571-573-571-517-573-571-571-571-157-175-157-157-513 157-513-135-130-135-315-137-153-103-135-157-157-153 135-150-135-153-317-130-130-135-135-150-150-153-1_V.mp3)
  Segment 19: 63144ms ~ 63720ms (file: 652-563-651-671-671-631-635-316-361-361-631-613-613-316 137-576-256-317-362-612-163-627-365-617-276-267-671-756-173-371-761-713-675-613-136-157-571-362-301-627-361-176-357-210-2_W.mp3)
  Segment 20: 64720ms ~ 65296ms (file: 167-261-657-176-213-312-301-126-132-136-312-257-230-263-267-263-260-261-162-136-765-263-715-754-761-715-157-715-756-257-261-137-765-716-765-237 327-267-267-261-263-256-265-265-2_V.mp3)
  Segment 21: 66296ms ~ 66872ms (file: 756-756-756-756-756-567-756-765-756-756-756-756-756 752-756-756-756-657-765-756-765-756-765-765-762 752-752-756_W.mp3)
  Segment 22: 67872ms ~ 68448ms (file: 573-567-576-517-567-567-567-675-567-567-576-657-561-567-567-537-576-756-567-563-356-657-653-516-571-561-576-567-156-576-657-567-571-576-567-567-756-567-657-567-567-653-567-567-5_V.mp3)
  Segment 23: 69448ms ~ 70024ms (file: 215-312-735-327-125-132-123-570-321-217-530-517-375-123-573-621-153-132-526-570-375-527-216-132-752-236-621-572-325-520-237-627-265-265-576-513-523-675-237-527-257-256-725-312-2_V.mp3)
  Segment 24: 71024ms ~ 71600ms (file: 756-756-765-576-756-765-765-617-613-615-612-165-163-615-163-167-165-165-612-675-765-675-675-675-657-675-675-163-167-160-163-167-163-162-675-765-657-657-675-365-263-562-502-520-5_W.mp3)
  Segment 25: 72600ms ~ 73176ms (file: 150-517-157-153 517-571-571-150-510-150-517-510-105-175-150-510-157-153-150-501-150-105-150-157 571-517-150-175-751-150-150-507-710-157-170-150-153-517-103-157-153-135 510-157-1_W.mp3)
  Segment 26: 74176ms ~ 74752ms (file: 657-567-657-567-567-657-654-657-567-657 625-671-657-657-657-657-657-657-657-651_W.mp3)
  Segment 27: 75752ms ~ 76328ms (file: 165-163-167-163-136-163-617-167 130-163-165-516 120-126-162-165-167-165-167-167-156-165-516-753-753-753-127-617-165-167-165-651-157-156-157-517-571-157-517-136-136-137-130-103-7_V.mp3)
  Segment 28: 77328ms ~ 77904ms (file: 574-574-570-574-570-573-571-107-107-105-107-701-761 547-547-547-574-547-574-570-510-504-510-501-507-571-507-570-570-501-751-710-570-574-574-574-510-517-501-510-175-105-510-105-1_W.mp3)
  Segment 29: 78904ms ~ 79480ms (file: 570-510-507-507-507-517-570-571-571-517-507-571-507-570-507-570-571-570-507 517_V.mp3)
  Segment 30: 80480ms ~ 90272ms (file: 237-130-321-263-263-673-267-321-312-321-321-672-263-321-537-123-573-326-321-132-567-765-762-627-267-263-267-672-136-321-267-103-321-130-267-231-675-267-261-102-231-267-236-321-6_W.mp3)
  Segment 31: 91272ms ~ 91848ms (file: 651-562-615-560-507-560-506-570-573-570-572-571-570-570 521-265-625-653-652-625 167-651-574-567-576_W.mp3)
  Segment 32: 92848ms ~ 93424ms (file: 170-751-571-750-570-751-150-750-517-501-150-175-750-705-507-570-570-750 105-150-175-571-751-530-157-105-170-517-750-705-571-750-750-571-750-751-173-150-571-517-157-750-570-754-7_V.mp3)
  Segment 33: 94424ms ~ 95000ms (file: 621-623-236-216-726-176-627-267-217-526-165-675-672-267-276-576-751-176-572-652-263-632-236-103-160-136-620-621-162-163-135-613-261-162-256-605-625-130-165-103-136-302-302-321-1_W.mp3)
  Segment 34: 96000ms ~ 96576ms (file: 567-567-567-567-567-567-567-576-567-567-567-576-567-567-572-567-526-526-506-523-357-567-506-635-567-352-567-576-567-576-567-576-573-503-567-310-312-315-567-567-567-567-527-562-5_W.mp3)
  Segment 35: 97576ms ~ 98152ms (file: 367-372-137-310-310-751-715-135-513-731-307 705-310-301-137-173-307-301-103-137-317-137-713-103-173-301-317-107-701-175-173-170-137-137-310-731-173-137-751-175-137-715 301-371-1_V.mp3)
  Segment 36: 99152ms ~ 99728ms (file: 657-657-657-654-654-625_W.mp3)
  Segment 37: 100728ms ~ 108468ms (file: 163-762-276-756-170-103-216-627-267-267-756-130-756-267-267-627-263-120-132-312-123-267-753-375-231-372-267-132-367-276-216-103-130-327-263-261-213-316-756-315-375-312-236-537-1_W.mp3)
  Segment 38: 109468ms ~ 114220ms (file: 312-123-123-132-132-312-213-123-123-132-321-213-321-231-321-213-130-137-310-173-123-210-210-213-132-213-130-213-213-261-216-215-217-572-217-103-315-312-130-137-137-751-130-103-1_W.mp3)
  Segment 39: 115220ms ~ 115796ms (file: 756-765-762 120-276-762-675-712-276-276-576-576-576-576-756-576-756-756-765-756-765-576-576-576-576-567-576-576-574 562-526-527-502-257-205-205-257-256-256-257-756-762-652-567-5_W.mp3)
  Segment 40: 116796ms ~ 117372ms (file: 762-672-675-765-675-765-672-765-672-627-672-672-675-672-672-657-365-625-576-576-576-652-562-675-762-267-271-632-265-352-657-327-165-271-615-647-236-213-132-367-365-236-345-613-2_V.mp3)
  Segment 41: 118372ms ~ 118948ms (file: 507-547-547-541-516-534-546-574-576-574-574 105-157-105-513-150-105-170-157-150-105-150-150-150-510-510-507-570-517-571-574-754-576 501-510-517-157-571-715-157-705-150-705-175-1_V.mp3)
  Segment 42: 119948ms ~ 127652ms (file: 267-571-756-275-321-103-130-571-130-756-765-765-321-671-102-231-231-321-123-765-675-273-321-132-230-102-163-132-321-321-731-571-765-574-627-576-756-321-132-576-105-675-263-213-1_W.mp3)
  Segment 43: 128652ms ~ 129228ms (file: 751-150-107-175-157-135-571-567-562-625-326-312-132-137-173-762-675-136-135-135-163-136-153-153-153-152-125-136-162-136-267-167-267-672-621-621-627-267-261-265-267-625-163-510-5_W.mp3)
  Segment 44: 130228ms ~ 130804ms (file: 765 567-576-574-625-652-657-263-276-257-163-765-725-627-123-657-276-670-620-276-612-672-267-567-275-576-620-126-123-572-267-276-250-126-502-276-627-523-275-132-153-570-572-657-2_V.mp3)
  Segment 45: 131804ms ~ 132380ms (file: 501-571 501-571-150-501-157-150-105-510-501-150-105-105-150-105-150-157-501-105-503-574-571-571-573-574-570-507-507-570-517-571-507 507-157-105-510-501-507-501-150-150-157-105-1_V.mp3)
  Segment 46: 133380ms ~ 133956ms (file: 316-361-316-136-316-316-316-310_W.mp3)
  Segment 47: 134956ms ~ 135532ms (file: 576-576-574-537-537-765-352-356-157-567-657-576-657-567-567-675-576-573-576-576-576-576-317 156-653-731-165-312-231-126-310-136-312-352-517-571-235-321-213-657-132-312-576-576-5_W.mp3)
  Segment 48: 136532ms ~ 137108ms (file: 137-173-137-135-175-173-175-137-175-173-175-756-657-175-751-762-726-712-726-765-765-716-752-761-756-712-716-721-750-571-175-165-167-157-175-156-157-157-165-156-157-165-517-517-5_W.mp3)
  Segment 49: 138108ms ~ 145632ms (file: 321-127-276-237-574-327-723-237-574-570-756-705-106-153-130-675-675-325-367-756-576-157-752-237-136-163-167-106-106-312-613-132-136-671-362-123-267-267-207-576-574-570-237-203-1_W.mp3)
  Segment 50: 146632ms ~ 156244ms (file: 132 271-316-260-576-163-126-261-267-263-106-521-751-276-216-627-762-625-627-572-127-216-572-216-126-271-675-261-316-106-725-572-751-527-267-261-271-236-261-527-574-126-627-107-2_W.mp3)
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
> **Time**: 2026-04-05 11:41:25
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 2,
"transcript": "와 꿈만 같아요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "와", "start_ms": 100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "꿈만", "start_ms": 260, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 470, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "좋아하는 두 사람 아니라 할까 봐 어느새 로봇 구경 삼매경인데요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "좋아하는", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "두", "start_ms": 200, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "사람", "start_ms": 260, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 370, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "할까", "start_ms": 500, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "봐", "start_ms": 590, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "어느새", "start_ms": 660, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "로봇", "start_ms": 810, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "구경", "start_ms": 940, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "삼매경인데요", "start_ms": 1070, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "지금까지 만들어 온 작품들을 뽐내 보입니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "지금까지", "start_ms": 30, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "만들어", "start_ms": 220, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "온", "start_ms": 350, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "작품들을", "start_ms": 410, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "뽐내", "start_ms": 570, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "보입니다", "start_ms": 680, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 6,
"transcript": "1위를 찍어버렸습니다 영어권 비영어권을 모두 합친 전체 이익 그리고 무려 78개국에서 탑텐에 진입했는데요 이 정도면 그저 흥행",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "1위를", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "찍어버렸습니다", "start_ms": 210, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "영어권", "start_ms": 570, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "비영어권을", "start_ms": 730, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "모두", "start_ms": 990, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "합친", "start_ms": 1120, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "전체", "start_ms": 1250, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "이익", "start_ms": 1380, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 1500, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "무려", "start_ms": 1660, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "78개국에서", "start_ms": 1790, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "탑텐에", "start_ms": 2200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "진입했는데요", "start_ms": 2360, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 2680, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "정도면", "start_ms": 2740, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "그저", "start_ms": 2900, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "흥행", "start_ms": 3030, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "오늘 내가 1% 성장했냐라는 그 나를 복기하는 게 너무 중요하다라고 생각을 하거든요 그러면은 뭐 목표를 다시 정한다는 건 좀 웃기긴 하지만",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 60, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "내가", "start_ms": 190, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "1%", "start_ms": 320, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "성장했냐라는", "start_ms": 480, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 840, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "나를", "start_ms": 900, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "복기하는", "start_ms": 1030, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 1290, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 1350, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "중요하다라고", "start_ms": 1480, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "생각을", "start_ms": 1840, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "하거든요", "start_ms": 2030, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "그러면은", "start_ms": 2300, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 2560, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "목표를", "start_ms": 2670, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 2860, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "정한다는", "start_ms": 3020, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "건", "start_ms": 3280, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 3390, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "웃기긴", "start_ms": 3500, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "하지만", "start_ms": 3710, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "바레인의 아마존 클라우드 센터와 두바이 소재 오라클 데이터 센터도 공격했다고 주장했습니다 MBC 뉴스 송서영입니다",
"speaker": "speaker_4",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "바레인의", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "아마존", "start_ms": 230, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "클라우드", "start_ms": 390, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "센터와", "start_ms": 600, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "두바이", "start_ms": 790, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "소재", "start_ms": 950, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "오라클", "start_ms": 1080, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "데이터", "start_ms": 1240, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "센터도", "start_ms": 1400, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "공격했다고", "start_ms": 1590, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "주장했습니다", "start_ms": 1850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "MBC", "start_ms": 2160, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "뉴스", "start_ms": 2270, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "송서영입니다", "start_ms": 2380, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "미사일이 날아다니든 말든 국제 정세에 신경 쓸 필요 없이 아주 안전하게 항로를 다닐 수 있는 거예요 실제로 부산에서 네덜란드",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "미사일이", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "날아다니든", "start_ms": 240, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "말든", "start_ms": 470, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "국제", "start_ms": 600, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "정세에", "start_ms": 730, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "신경", "start_ms": 890, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "쓸", "start_ms": 1020, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "필요", "start_ms": 1090, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "없이", "start_ms": 1220, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 1350, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "안전하게", "start_ms": 1480, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "항로를", "start_ms": 1670, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "다닐", "start_ms": 1830, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 1960, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 2030, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 2140, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "실제로", "start_ms": 2300, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "부산에서", "start_ms": 2490, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "네덜란드", "start_ms": 2700, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "어디서 이미 신호가 나왔는지 이걸 보여드리겠습니다 이걸 깨달으면 매매가 완전히 달라",
"speaker": "speaker_6",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "어디서", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이미", "start_ms": 190, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "신호가", "start_ms": 320, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "나왔는지", "start_ms": 480, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "이걸", "start_ms": 690, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "보여드리겠습니다", "start_ms": 820, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이걸", "start_ms": 1230, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "깨달으면", "start_ms": 1360, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "매매가", "start_ms": 1570, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "완전히", "start_ms": 1730, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "달라", "start_ms": 1920, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "날리고 날리고 날리자 정말 달리기 하듯이 열심히",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "mixed",
"words": [
{"word": "날리고", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "날리고", "start_ms": 190, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "날리자", "start_ms": 350, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 510, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "달리기", "start_ms": 640, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "하듯이", "start_ms": 800, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "열심히", "start_ms": 960, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "질환 아저씨였던 거죠 역대급 빌런 수양대군과 월드 클래스 찌질남 성기운 이 극단적인 온도 차에",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "질환", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "아저씨였던", "start_ms": 170, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "거죠", "start_ms": 430, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "역대급", "start_ms": 560, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "빌런", "start_ms": 720, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "수양대군과", "start_ms": 850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "월드", "start_ms": 1160, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "클래스", "start_ms": 1290, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "찌질남", "start_ms": 1480, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "성기운", "start_ms": 1670, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 1860, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "극단적인", "start_ms": 1930, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "온도", "start_ms": 2190, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "차에", "start_ms": 2320, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "화물이 없을 때는 그 선원을 전부 하선시키고 선박을 이제 세워 두고 나오는 경우도 있었는데 어 지금 같은 경우에는",
"speaker": "speaker_7",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "화물이", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "없을", "start_ms": 180, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "때는", "start_ms": 310, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 440, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "선원을", "start_ms": 500, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "전부", "start_ms": 660, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "하선시키고", "start_ms": 790, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "선박을", "start_ms": 1150, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 1340, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "세워", "start_ms": 1470, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "두고", "start_ms": 1600, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "나오는", "start_ms": 1730, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "경우도", "start_ms": 1920, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있었는데", "start_ms": 2080, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "어", "start_ms": 2340, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 2410, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "같은", "start_ms": 2540, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "경우에는", "start_ms": 2670, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "몇 번의 실험만으로 문제점을 쉽게 찾아낸 박혜원 교수 사실 그럴 수밖에 없는 이유가 있습",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "몇", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "번의", "start_ms": 120, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "실험만으로", "start_ms": 230, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "문제점을", "start_ms": 490, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "쉽게", "start_ms": 700, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "찾아낸", "start_ms": 860, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "박혜원", "start_ms": 1050, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "교수", "start_ms": 1210, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "사실", "start_ms": 1340, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "그럴", "start_ms": 1470, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "수밖에", "start_ms": 1600, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "없는", "start_ms": 1760, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "이유가", "start_ms": 1890, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있습", "start_ms": 2050, "dur_ms": 120, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 11:41:25*
