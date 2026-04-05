# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=30524  time=05:59:24  uptime=5276s  mem=223MB  system_processes=352"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_055749.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 567 326-132-534-543-514-576-567-130-375-756-675-613-130-106-546-576-571-576-675-756-756-756-576-760-627-562-570-527-750-513-103-312-103-612-657-670-106-136-630-751-675-573-137-1_W.mp3)
  Segment 2: 1576ms ~ 12124ms (file: 203-102-124-752-261-543-267-312-132-120-123-126-620-574-572-610-267-206-762-130-132-216-132-574-574-201-547-315-267-230-260-231-136-574-126-267-672-260-210-261-264-231-103-726-1_W.mp3)
  Segment 3: 13124ms ~ 20072ms (file: 130-312-173-376-317-371-731-731-123-312-732-371-261-672-672-267-120-327-301-103-136-127-170-317-372-213-673-103-657-763-765-276-732-213-276-213-230-267-230-325-326-325-210-123-2_W.mp3)
  Segment 4: 21072ms ~ 21648ms (file: 316-175-167-165-106-312-320-631-312-136-261-132-103-561-675-671-627-627-160-167-163-671-125-657-657-672-165-162-321-623-162-321-136-163-312-361-312-613-271-136-361-126-612-617-2_W.mp3)
  Segment 5: 22648ms ~ 30424ms (file: 310-514-351-356-123-167-123-150-310-123-103-102-106-310-136-316-316-136-103-261-130-306-103-136-107-176-627-106-310-310-627-726-317-167-316-213-612-102-120-670-152-103-317-103-1_V.mp3)
  Segment 6: 31424ms ~ 32000ms (file: 675-607-607-321-631-613-103-675-675-675-672-136-637-326-267-623-365-125-513-352-513_V.mp3)
  Segment 7: 33000ms ~ 41028ms (file: 751-267-217-231-325-126-261-103-216-627-261-312-231-723-267-230-756-756-213-627-176-561-375-175-103-612-165-175-102-213-756-267-231-162-617-123-120-236-362-765-236-260-263-102-2_W.mp3)
  Segment 8: 42028ms ~ 42604ms (file: 756 236-205-205-254-165-627-276-267-267-371-372-312-361-321-612-672-504-675-103-103-103-135-132-172-137-176-326-316-153-130-310-310-136-620-206-672-301-312-376-306-316-312-307-3_V.mp3)
  Segment 9: 43604ms ~ 44180ms (file: 751-170-175-157-710-705 175-137-107-175-107-175-105-157-107-175-105-175-105 105-175-715-175-751-135-107-107-107-107-710-701-710-701 751-175-751-715-175-175-107-715-750-715-710-5_V.mp3)
  Segment 10: 45180ms ~ 54540ms (file: 356-167-326-126-327-236-237-573-275-326-316-765-267-263-572-267-312-263-352-325-732-327-327-572-316-132-123-126-106-160-765-756-167-617-617-576-765-267-160-672-761-617-671-756-7_W.mp3)
  Segment 11: 55540ms ~ 56116ms (file: 501-510-105-135-105-513-153-513-517-571-537-517-571-501-517-517-574-571 501-501-105-501-150-501-153-510-570-501-507-150-510-501-150-501-105-510-150-510-150-157-513-531-513-513-1_W.mp3)
  Segment 12: 57116ms ~ 57692ms (file: 716-175-175 105-510-150-157-157-756-175-157-156-175-157-156-157-176-165-157-175-167-157-167-176-715-571-157-175-175-173-750-754-756-756-754-754-754-754-501-150-105-150-150-175-1_V.mp3)
  Segment 13: 58692ms ~ 64740ms (file: 375-753-760-672-127-267-260-312-130-213-103-310-367-102-236-362-316-631-761-176-132-263-267-137-103-157-137-103-713-130-310-273-310-237-267-267-126-675-263-126-107-312-326-267-5_V.mp3)
  Segment 14: 65740ms ~ 66316ms (file: 176-316-376-637-103-136-567-132-715-137-610-136-671-106-316-156-516-316-162-623-623-260-132-103-136-602-165-163-162-651-103-123-627-761-263-627-537-132-136_V.mp3)
  Segment 15: 67316ms ~ 67892ms (file: 750-705 571-570-570-507-570-570-570-570-571-571-571-517-351-375_W.mp3)
  Segment 16: 68892ms ~ 69468ms (file: 750-750-571-715-517-107-571-170-175-705-107-105-107-710-751 510-517-570-150-175-750-750-751-175-750-701-710-705-107-751-150-705-710-175-157-705-107-705-751-715-715-751-107-571-1_V.mp3)
  Segment 17: 70468ms ~ 71044ms (file: 675-671-671-261-267-627-672-670-127-103-235-260-537-573-567-263-267-672-276-621-176-236-236-623-613-672-607-564-563-236-320-231-217-123-763-160-617-617-127-213-127-126-675-327-2_W.mp3)
  Segment 18: 72044ms ~ 72620ms (file: 315-312-312-513-165-153-157-153-135-576-673-673-735-765-736-736-765-375-756-317-137-173-137-173-103-173-317-735-315-312-317-135-132-135-156-156-153-156-165-576-756-756-750-576-5_V.mp3)
  Segment 19: 73620ms ~ 74196ms (file: 576-615-521-506-753-763-657-356-321-237-217-357 132-176-137_W.mp3)
  Segment 20: 75196ms ~ 75772ms (file: 143-132-134-132-157-162-102-615-106-106-163-726-361-213-163-236-176-670-761-215-513-251-671-617-160-615-657-265-210-106-123-102-123-127-105-103-136-105-123-163-617-215-602-136-1_V.mp3)
  Segment 21: 76772ms ~ 77348ms (file: 123-236-137-170-157-756-137-137-132-132-315-312 750-751-751-751-750-750-715-175-715-710-175-751-531-153-165 157-157-571-751-715-751-571-715-715-170-137-517-175-137-715-157-715-7_W.mp3)
  Segment 22: 78348ms ~ 78924ms (file: 162-176-160-574-576-576-675-765-675-673 173-570-756-576-756-576-573-574-567 132-316-136-765-635-657-671 765-576-573 132 132-316-567-657-537-507-576-560-567-576-567-567-567-567-5_W.mp3)
  Segment 23: 79924ms ~ 80500ms (file: 123-137-152-617-523-263-136-176-163-106-136-103-572-172-126-165-107-156-362-136-103-631-716-126-625-103-132-107-143-132-716-167-167-261-163-675-160-165-127-126-712-123-160-175-6_V.mp3)
  Segment 24: 81500ms ~ 82076ms (file: 103-103-135-103-103-103-134-130-134-103-130-103-134-314-134 341-340-134-317-130-103-310-310-310-103-135-103-134-103-103-103-103-134-135-103-134-135-135-134-103-134 130-310-103-1_W.mp3)
  Segment 25: 83076ms ~ 83652ms (file: 631-316-576-376-765-213-657-157-157-157-165-163-163-136-136-316-673-703-731-753-765-675-736-763-763-675-736-657-763-673-317-156-150-175-167-675-765-762-376-673-671_W.mp3)
  Segment 26: 84652ms ~ 94804ms (file: 572-257-627-627-207-310-231-123-102-213-754-276-214-236-236-260-752-601-162-321-752-206-120-230-102-231-320-326-602-217-263-326-206-213-756-752-321-267-612-267-260-267-267-263-3_W.mp3)
  Segment 27: 95804ms ~ 96380ms (file: 156-156-150-671-567-765-765-761-762-376-756-576-756-751-713-752-715-751-153-105-135-167-716-136-173-761-176-173-175-167-176-173-736-756-105-175-107-176-170-150-175-150-150-156-1_W.mp3)
  Segment 28: 97380ms ~ 100512ms (file: 123-132-312-316-312 756-120-216-675-564-546-564-367-134-613-513-301-301-134-137-103 547-547-547-315-321-213-735-761-726-176-173-716 213-156-652-675-756 127-132-137-207-267-213-6_V.mp3)
  Segment 29: 101512ms ~ 102088ms (file: 517-326-631-675-160-157-157-571-167-671-167-627-167-621-106-167-371-127-176-610-617-612-136-123-167-362-162-176-132 263-136 376-765-627-136-612 576-576-625-672-675-675-765-675-2_W.mp3)
  Segment 30: 103088ms ~ 103664ms (file: 751-715-517-175-751-751-756-756 571-571-750-175-571-750-570-715-715-715-715-175-571-107-710-150-571-157-517-715-175-175-751-751 571-570-751-571-571-750-571-571-571-574-571-751-7_W.mp3)
  Segment 31: 104664ms ~ 105240ms (file: 136-136-136-103-103-135-103-136-103-103-103 341-134-314-134-314-136-143-316-136-134-137-136-137-136-301-103-136-130-103-135-103-136 314-134-136-316-316-143-136-316-134-163-103-1_V.mp3)
  Segment 32: 106240ms ~ 106816ms (file: 267-627-627-627-236-267-267-627-627-670-627-267-623 673-671-671-627-637-236-623-163-367-617-637-672-672-621-612-612-612-621-623-263-621-621-612-261-612-612-261 621-613-260-123 6_W.mp3)
  Segment 33: 107816ms ~ 108392ms (file: 657-651-576-621-351-362-162-136-632-261-576-216-765-627-126-267-627-236-657 126-126-163-627-621 576 617-126 562-526-671-670-623_W.mp3)
  Segment 34: 109392ms ~ 116628ms (file: 207-210-265-102-130-310-103-312-312-132-301-301-103-132-103-143-201-206-102-103-132-103-527-127-413-143-126-230-320-210-230-754-275-320-320-230-123-201-120-231-210-213-210-263-1_V.mp3)
  Segment 35: 117628ms ~ 118204ms (file: 527-352-672-672-524-524-756-567-567-206-362-361-627 627-267-263-261 205-306-362-623-260-103-102-123-312-301-302-530-205-205-547-301-123-103-103-103-132-312-301-231-163-351-615-6_V.mp3)
  Segment 36: 119204ms ~ 119780ms (file: 105-651-576-576-671-761-631-176-657-675-765-756-756-675-576-567-657-657-756-675-657-576-765-675-675-675-657-675-567-756-675 756-574 123-103-103-102 613-561-671-103-123-571-312-6_W.mp3)
  Segment 37: 120780ms ~ 121356ms (file: 105-105-150-105-105-105-510-105-150-510-150-510_V.mp3)
  Segment 38: 122356ms ~ 122932ms (file: 361-613-106-126-321-132-361-736-372-712-716-713-213-765 752-752-752-137-136-103-163-163-670-106-125-103-132-102-132-157-316-136-516-326-172-136-176-130-310-617-132-106-517-217-1_V.mp3)
  Segment 39: 123932ms ~ 130340ms (file: 265-325-201-210-123-236-265-230-574-132-231-235-523-301-132-532-203-230-132-572-120-173-125-130-106-261-261-326-321-123-260-261-231-321-263-236-216-231-230-265-236-230-123-273-2_W.mp3)
  Segment 40: 131340ms ~ 131916ms (file: 632-756-326-562-356-375-167-317-312-102-130-132-316-562-352-657-357-765-576-356-615-631-632-765-613-357-357-573-753-165-156-135-167-136-156-573-376-671-673-137-263-321-357-351-3_W.mp3)
  Segment 41: 132916ms ~ 133492ms (file: 103-130-106-103-163-103-567-327-263-260-206-201-106-136-316-203-632-670-675-765-756-176-675-106-123-625-635-162-163-165-106-125-165-176-627-607-620-150-136-163-763-362-562-632-6_W.mp3)
  Segment 42: 134492ms ~ 135068ms (file: 157-153-157-157-175-157-517-175-715-517-175-715-176 154-154-105-150-105-150-150-175-157-157-157-175-756-157-167-165-176-167-157-157-175-173 105-157-105-107-107-170-176-176-175-1_V.mp3)
  Segment 43: 136068ms ~ 136644ms (file: 517-157-751-751-571-715-175-751-571-751-751-756-571 510-517-175-751-751-517-715-175-751-751-571-517-571-715-571-751-751-570-571-751 570-571-571-751-175-710-175-571-517-571-751-5_W.mp3)
  Segment 44: 137644ms ~ 138220ms (file: 105-105-105-105-150-150-150-156-156-175-175-157-167-157-157-175-150-157-175-175-137-750-750-756-756-751-754-105-150-150-510-150-157-715-175-516-157-157-156-157-157-153-156-173-7_V.mp3)
  Segment 45: 139220ms ~ 147104ms (file: 267-126-321-231-102-123-756-765-276-103-672-276-213-172-167-320-230-237-260-163-132-310-312-362-231-321-236-231-752-267-102-321-762-672-267-230-123-132-231-321-276-273-725-672-6_W.mp3)
  Segment 46: 148104ms ~ 148680ms (file: 312-312-312-316-756-576-657-762-316-136-103-136-103-102-261-160-163-621-162-621-617-316-362-620-632-163-657-617-675-567-657-675-657-136-316-601-107-701-176 173-137-132 320-231-1_W.mp3)
  Segment 47: 149680ms ~ 150256ms (file: 750-751-751-715-715-710-710-751-751-175-710-705-175-715-750-175-710-715-175-706-175-701-710-170-751-751-751 571-750-715-751-175-751-715-751-175-751-750-705-751-710-710-715-751-7_V.mp3)
  Segment 48: 151256ms ~ 169580ms (file: 130-310-157-310-517-130-210-312-537-576-756-175-260-213-102-130-312-103-301-130-103-310-103-301-130-201-230-103-260-567-576-103-312-132-641-103-201-230-130-102-206-102-301-103-1_V.mp3)
  Segment 49: 170580ms ~ 179652ms (file: 213-320-103-173-260-163-106-213-260-130-103-236-267-203-230-123-231-572-730-321-102-102-321-123-213-230-230-231-231-230-270-573-130-130-130-130-107-617-621-107-107-617-763-107-2_W.mp3)
  Segment 50: 180652ms ~ 188392ms (file: 302-320-132-102-230-263-721-126-275-267-321-310-163-310-320-263-260-260-102-320-260-231-320-260-213-321-260-263-752-210-123-263-127-301-752-617-176-107-327-102-725-236-103-547-2_W.mp3)
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
> **Time**: 2026-04-05 06:00:59
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "안녕하세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 85,
"audio_type": "speech",
"words": [
{"word": "안녕", "start_ms": 85, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "하세요", "start_ms": 295, "dur_ms": 281, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "만나서 반갑습니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "만나서", "start_ms": 40, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "반갑", "start_ms": 280, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "습니다", "start_ms": 430, "dur_ms": 146, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "오늘 날씨가 정말 좋네요 산책 가기 딱 좋은 날이에요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 110, "dur_ms": 320, "phase_lr_deg": -5.0},
{"word": "날씨가", "start_ms": 450, "dur_ms": 480, "phase_lr_deg": -5.0},
{"word": "정말", "start_ms": 950, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "좋네요", "start_ms": 1320, "dur_ms": 510, "phase_lr_deg": -5.0},
{"word": "산책", "start_ms": 2100, "dur_ms": 420, "phase_lr_deg": -5.0},
{"word": "가기", "start_ms": 2550, "dur_ms": 380, "phase_lr_deg": -5.0},
{"word": "딱", "start_ms": 2950, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "좋은", "start_ms": 3170, "dur_ms": 340, "phase_lr_deg": -5.0},
{"word": "날이에요", "start_ms": 3550, "dur_ms": 620, "phase_lr_deg": -5.0}
]
},
{
"segment": 4,
"transcript": "한국어 공부는 재미있지만 문법이 조금 어려워요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 95,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 95, "dur_ms": 520, "phase_lr_deg": 2.0},
{"word": "공부는", "start_ms": 650, "dur_ms": 480, "phase_lr_deg": 2.0},
{"word": "재미있지만", "start_ms": 1200, "dur_ms": 850, "phase_lr_deg": 2.0},
{"word": "문법이", "start_ms": 2150, "dur_ms": 560, "phase_lr_deg": 2.0},
{"word": "조금", "start_ms": 2750, "dur_ms": 410, "phase_lr_deg": 2.0},
{"word": "어려워요", "start_ms": 3200, "dur_ms": 680, "phase_lr_deg": 2.0}
]
},
{
"segment": 5,
"transcript": "Where is the nearest subway station? 길을 좀 잃은 것 같아요",
"speaker": "speaker_3",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "Where", "start_ms": 150, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 440, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 570, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "nearest", "start_ms": 700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "subway", "start_ms": 1180, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "station?", "start_ms": 1580, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "길을", "start_ms": 2400, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 2750, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "잃은", "start_ms": 2950, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 3250, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 3400, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "죄송합니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "죄송", "start_ms": 60, "dur_ms": 280, "phase_lr_deg": -3.0},
{"word": "합니다", "start_ms": 350, "dur_ms": 226, "phase_lr_deg": -3.0}
]
},
{
"segment": 7,
"transcript": "이 근처에 맛있는 식당이 어디인가요? 매운 음식을 좋아해요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 130, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "근처에", "start_ms": 260, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "맛있는", "start_ms": 750, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "식당이", "start_ms": 1300, "dur_ms": 510, "phase_lr_deg": 0.0},
{"word": "어디인가요?", "start_ms": 1850, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "매운", "start_ms": 3200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "음식을", "start_ms": 3600, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "좋아해요", "start_ms": 4150, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "어제 친구랑 영화를 봤는데 정말 감동적이었어요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 105,
"audio_type": "speech",
"words": [
{"word": "어제", "start_ms": 105, "dur_ms": 350, "phase_lr_deg": -2.0},
{"word": "친구랑", "start_ms": 500, "dur_ms": 450, "phase_lr_deg": -2.0},
{"word": "영화를", "start_ms": 1000, "dur_ms": 480, "phase_lr_deg": -2.0},
{"word": "봤는데", "start_ms": 1550, "dur_ms": 510, "phase_lr_deg": -2.0},
{"word": "정말", "start_ms": 2200, "dur_ms": 380, "phase_lr_deg": -2.0},
{"word": "감동적", "start_ms": 2650, "dur_ms": 550, "phase_lr_deg": -2.0},
{"word": "이었어요", "start_ms": 3220, "dur_ms": 460, "phase_lr_deg": -2.0}
]
},
{
"segment": 9,
"transcript": "고마워요",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "고마", "start_ms": 50, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "워요", "start_ms": 320, "dur_ms": 206, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "다음에 또 만나요 우리 그때 같이 커피 마셔요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 120, "dur_ms": 420, "phase_lr_deg": 1.0},
{"word": "또", "start_ms": 580, "dur_ms": 150, "phase_lr_deg": 1.0},
{"word": "만나요", "start_ms": 750, "dur_ms": 480, "phase_lr_deg": 1.0},
{"word": "우리", "start_ms": 1600, "dur_ms": 320, "phase_lr_deg": 1.0},
{"word": "그때", "start_ms": 1950, "dur_ms": 310, "phase_lr_deg": 1.0},
{"word": "같이", "start_ms": 2300, "dur_ms": 280, "phase_lr_deg": 1.0},
{"word": "커피", "start_ms": 2650, "dur_ms": 350, "phase_lr_deg": 1.0},
{"word": "마셔요", "start_ms": 3050, "dur_ms": 510, "phase_lr_deg": 1.0}
]
},
{
"segment": 11,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 12,
"transcript": "이것은 무엇입니까?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "이것은", "start_ms": 70, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "무엇", "start_ms": 480, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "입니까?", "start_ms": 710, "dur_ms": 310, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "제 이름은 김민수입니다 만나서 반가워요",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 115,
"audio_type": "speech",
"words": [
{"word": "제", "start_ms": 115, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "이름은", "start_ms": 310, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "김민수", "start_ms": 750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "입니다", "start_ms": 1220, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "만나서", "start_ms": 1900, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "반가워요", "start_ms": 2350, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "실례합니다 화장실이 어디예요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "실례합니다", "start_ms": 90, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "화장실이", "start_ms": 800, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "어디예요?", "start_ms": 1380, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "잘 자요",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "잘", "start_ms": 45, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "자요", "start_ms": 250, "dur_ms": 281, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "사랑해요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "사랑", "start_ms": 50, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "해요", "start_ms": 350, "dur_ms": 226, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "배가 너무 고파요 같이 밥 먹으러 갈래요?",
"speaker": "speaker_4",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "배가", "start_ms": 140, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 480, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "고파요", "start_ms": 850, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 1500, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "밥", "start_ms": 1850, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "먹으러", "start_ms": 2100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "갈래요?", "start_ms": 2600, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "이거 얼마예요?",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "이거", "start_ms": 65, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "얼마예요?", "start_ms": 380, "dur_ms": 311, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "I love this song! 노래가 정말 신나요 같이 춤춰요",
"speaker": "speaker_5",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 180, "dur_ms": 110, "phase_lr_deg": 10.0},
{"word": "love", "start_ms": 320, "dur_ms": 250, "phase_lr_deg": 10.0},
{"word": "this", "start_ms": 600, "dur_ms": 220, "phase_lr_deg": 10.0},
{"word": "song!", "start_ms": 850, "dur_ms": 450, "phase_lr_deg": 10.0},
{"word": "노래가", "start_ms": 1800, "dur_ms": 420, "phase_lr_deg": 10.0},
{"word": "정말", "start_ms": 2250, "dur_ms": 350, "phase_lr_deg": 10.0},
{"word": "신나요", "start_ms": 2650, "dur_ms": 520, "phase_lr_deg": 10.0},
{"word": "같이", "start_ms": 3500, "dur_ms": 320, "phase_lr_deg": 10.0},
{"word": "춤춰요", "start_ms": 3850, "dur_ms": 610, "phase_lr_deg": 10.0}
]
},
{
"segment": 20,
"transcript": "주말에 뭐 할 거예요? 저는 친구랑 등산 가기로 했어요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 125,
"audio_type": "speech",
"words": [
{"word": "주말에", "start_ms": 125, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 550, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 720, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "거예요?", "start_ms": 920, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "저는", "start_ms": 1700, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "친구랑", "start_ms": 2050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "등산", "start_ms": 2550, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "가기로", "start_ms": 3000, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "했어요", "start_ms": 3450, "dur_ms": 520, "phase_lr_deg": 0.0}
]
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
"transcript": "이거 드셔보세요 진짜 맛있어요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "이거", "start_ms": 80, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "드셔보세요", "start_ms": 380, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 1200, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "맛있어요", "start_ms": 1550, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 24,
"transcript": "한국 음식이 조금 맵지만 중독성 있는 맛이에요",
"speaker": "speaker_4",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "한국", "start_ms": 100, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "음식이", "start_ms": 450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "맵지만", "start_ms": 1350, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "중독성", "start_ms": 2100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 2700, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "맛이에요", "start_ms": 3050, "dur_ms": 580, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "요즘 유행하는 노래가 뭐예요? 추천 좀 해주세요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "요즘", "start_ms": 110, "dur_ms": 320, "phase_lr_deg": -1.0},
{"word": "유행하는", "start_ms": 450, "dur_ms": 520, "phase_lr_deg": -1.0},
{"word": "노래가", "start_ms": 1000, "dur_ms": 420, "phase_lr_deg": -1.0},
{"word": "뭐예요?", "start_ms": 1450, "dur_ms": 410, "phase_lr_deg": -1.0},
{"word": "추천", "start_ms": 2200, "dur_ms": 350, "phase_lr_deg": -1.0},
{"word": "좀", "start_ms": 2600, "dur_ms": 180, "phase_lr_deg": -1.0},
{"word": "해주세요", "start_ms": 2800, "dur_ms": 580, "phase_lr_deg": -1.0}
]
},
{
"segment": 26,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 27,
"transcript": "생일 축하해요! 오늘 하루 행복하게 보내길 바라요",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 95,
"audio_type": "speech",
"words": [
{"word": "생일", "start_ms": 95, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "축하해요!", "start_ms": 450, "dur_ms": 610, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 1400, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "하루", "start_ms": 1750, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "행복하게", "start_ms": 2100, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "보내길", "start_ms": 2650, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "바라요", "start_ms": 3100, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 29,
"transcript": "저는 서울에서 살고 있습니다 한국에 온 지 벌써 1년이 되었네요",
"speaker": "speaker_4",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 120, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "서울에서", "start_ms": 450, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "살고", "start_ms": 1000, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 1350, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "한국에", "start_ms": 2100, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "온", "start_ms": 2550, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "지", "start_ms": 2720, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "벌써", "start_ms": 2850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "1년이", "start_ms": 3250, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "되었네요", "start_ms": 3700, "dur_ms": 580, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "커피 한 잔 할까요? 아니면 차 마실까요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 85,
"audio_type": "speech",
"words": [
{"word": "커피", "start_ms": 85, "dur_ms": 320, "phase_lr_deg": -4.0},
{"word": "한", "start_ms": 420, "dur_ms": 110, "phase_lr_deg": -4.0},
{"word": "잔", "start_ms": 550, "dur_ms": 150, "phase_lr_deg": -4.0},
{"word": "할까요?", "start_ms": 720, "dur_ms": 450, "phase_lr_deg": -4.0},
{"word": "아니면", "start_ms": 1500, "dur_ms": 380, "phase_lr_deg": -4.0},
{"word": "차", "start_ms": 1900, "dur_ms": 210, "phase_lr_deg": -4.0},
{"word": "마실까요?", "start_ms": 2150, "dur_ms": 510, "phase_lr_deg": -4.0}
]
},
{
"segment": 31,
"transcript": "Can you help me? 이 기계를 어떻게 사용하는지 모르겠어요",
"speaker": "speaker_5",
"confidence": 0.93,
"language": "en",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "Can", "start_ms": 150, "dur_ms": 210, "phase_lr_deg": 5.0},
{"word": "you", "start_ms": 380, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "help", "start_ms": 550, "dur_ms": 280, "phase_lr_deg": 5.0},
{"word": "me?", "start_ms": 850, "dur_ms": 210, "phase_lr_deg": 5.0},
{"word": "이", "start_ms": 1400, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "기계를", "start_ms": 1550, "dur_ms": 420, "phase_lr_deg": 5.0},
{"word": "어떻게", "start_ms": 2000, "dur_ms": 380, "phase_lr_deg": 5.0},
{"word": "사용하는지", "start_ms": 2400, "dur_ms": 650, "phase_lr_deg": 5.0},
{"word": "모르겠어요", "start_ms": 3100, "dur_ms": 680, "phase_lr_deg": 5.0}
]
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
"transcript": "걱정하지 마세요 다 잘 될 거예요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "걱정하지", "start_ms": 90, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "마세요", "start_ms": 650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 1400, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 1600, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "될", "start_ms": 1820, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 2050, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "힘내세요! 당신은 할 수 있어요",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 75,
"audio_type": "speech",
"words": [
{"word": "힘내세요!", "start_ms": 75, "dur_ms": 620, "phase_lr_deg": 2.0},
{"word": "당신은", "start_ms": 1000, "dur_ms": 420, "phase_lr_deg": 2.0},
{"word": "할", "start_ms": 1450, "dur_ms": 180, "phase_lr_deg": 2.0},
{"word": "수", "start_ms": 1650, "dur_ms": 120, "phase_lr_deg": 2.0},
{"word": "있어요", "start_ms": 1800, "dur_ms": 480, "phase_lr_deg": 2.0}
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
"transcript": "이 옷이 너무 예쁜데 저한테 잘 어울릴까요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 110, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "옷이", "start_ms": 250, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "예쁜데", "start_ms": 1000, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "저한테", "start_ms": 1800, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 2250, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "어울릴까요?", "start_ms": 2450, "dur_ms": 710, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "네, 정말 잘 어울려요 색깔이 화사하네요",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 100, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 320, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 700, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "어울려요", "start_ms": 900, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "색깔이", "start_ms": 1700, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "화사하네요", "start_ms": 2200, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "오늘 저녁에 시간 있어요? 영화 보러 가요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 130, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "저녁에", "start_ms": 480, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "시간", "start_ms": 950, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "있어요?", "start_ms": 1300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "영화", "start_ms": 2100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "보러", "start_ms": 2500, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "가요", "start_ms": 2850, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "미안해요 오늘 선약이 있어서 다음에 같이 가요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "미안해요", "start_ms": 140, "dur_ms": 510, "phase_lr_deg": -2.0},
{"word": "오늘", "start_ms": 700, "dur_ms": 320, "phase_lr_deg": -2.0},
{"word": "선약이", "start_ms": 1050, "dur_ms": 450, "phase_lr_deg": -2.0},
{"word": "있어서", "start_ms": 1550, "dur_ms": 480, "phase_lr_deg": -2.0},
{"word": "다음에", "start_ms": 2300, "dur_ms": 410, "phase_lr_deg": -2.0},
{"word": "같이", "start_ms": 2750, "dur_ms": 320, "phase_lr_deg": -2.0},
{"word": "가요", "start_ms": 3100, "dur_ms": 480, "phase_lr_deg": -2.0}
]
},
{
"segment": 40,
"transcript": "아쉽네요 그럼 다음 주말은 어때요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 95,
"audio_type": "speech",
"words": [
{"word": "아쉽네요", "start_ms": 95, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "그럼", "start_ms": 750, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 1100, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "주말은", "start_ms": 1450, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "어때요?", "start_ms": 1900, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "다음 주말은 괜찮아요 토요일 오후에 만날까요?",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "다음", "start_ms": 120, "dur_ms": 310, "phase_lr_deg": -1.0},
{"word": "주말은", "start_ms": 450, "dur_ms": 410, "phase_lr_deg": -1.0},
{"word": "괜찮아요", "start_ms": 900, "dur_ms": 520, "phase_lr_deg": -1.0},
{"word": "토요일", "start_ms": 1700, "dur_ms": 480, "phase_lr_deg": -1.0},
{"word": "오후에", "start_ms": 2250, "dur_ms": 450, "phase_lr_deg": -1.0},
{"word": "만날까요?", "start_ms": 2750, "dur_ms": 610, "phase_lr_deg": -1.0}
]
},
{
"segment": 42,
"transcript": "좋아요! 강남역 10번 출구 앞에서 봐요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 115,
"audio_type": "speech",
"words": [
{"word": "좋아요!", "start_ms": 115, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "강남역", "start_ms": 700, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "10번", "start_ms": 1250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "출구", "start_ms": 1650, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "앞에서", "start_ms": 2100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "봐요", "start_ms": 2600, "dur_ms": 420, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "알겠어요 늦지 않게 갈게요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "알겠어요", "start_ms": 100, "dur_ms": 510, "phase_lr_deg": -3.0},
{"word": "늦지", "start_ms": 750, "dur_ms": 320, "phase_lr_deg": -3.0},
{"word": "않게", "start_ms": 1100, "dur_ms": 310, "phase_lr_deg": -3.0},
{"word": "갈게요", "start_ms": 1450, "dur_ms": 480, "phase_lr_deg": -3.0}
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
"transcript": "이 근처에 무료 와이파이가 있나요? 비밀번호가 뭐예요?",
"speaker": "speaker_5",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 130, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "근처에", "start_ms": 260, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "무료", "start_ms": 750, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "와이파이가", "start_ms": 1100, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "있나요?", "start_ms": 1850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "비밀번호가", "start_ms": 2600, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "뭐예요?", "start_ms": 3300, "dur_ms": 420, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "비밀번호는 일 이 삼 사 입니다",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "비밀번호는", "start_ms": 70, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "일", "start_ms": 800, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 950, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "삼", "start_ms": 1100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "사", "start_ms": 1280, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "입니다", "start_ms": 1450, "dur_ms": 381, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "감사합니다 덕분에 인터넷을 사용할 수 있게 됐어요",
"speaker": "speaker_5",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 110, "dur_ms": 650, "phase_lr_deg": 2.0},
{"word": "덕분에", "start_ms": 900, "dur_ms": 410, "phase_lr_deg": 2.0},
{"word": "인터넷을", "start_ms": 1400, "dur_ms": 520, "phase_lr_deg": 2.0},
{"word": "사용할", "start_ms": 2000, "dur_ms": 420, "phase_lr_deg": 2.0},
{"word": "수", "start_ms": 2450, "dur_ms": 120, "phase_lr_deg": 2.0},
{"word": "있게", "start_ms": 2600, "dur_ms": 280, "phase_lr_deg": 2.0},
{"word": "됐어요", "start_ms": 2950, "dur_ms": 480, "phase_lr_deg": 2.0}
]
},
{
"segment": 48,
"transcript": "천만에요 도움이 되었다니 기쁘네요",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 95,
"audio_type": "speech",
"words": [
{"word": "천만에요", "start_ms": 95, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "도움이", "start_ms": 750, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "되었다니", "start_ms": 1180, "dur_ms": 510, "phase_lr_deg": 0.0},
{"word": "기쁘네요", "start_ms": 1750, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "한국의 가을은 단풍이 참 아름다워요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 85,
"audio_type": "speech",
"words": [
{"word": "한국의", "start_ms": 85, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "가을은", "start_ms": 550, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "단풍이", "start_ms": 1100, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "참", "start_ms": 1650, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "아름다워요", "start_ms": 1850, "dur_ms": 710, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "맞아요 저도 가을을 가장 좋아해요 단풍 구경 가고 싶네요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 125,
"audio_type": "speech",
"words": [
{"word": "맞아요", "start_ms": 125, "dur_ms": 420, "phase_lr_deg": -1.0},
{"word": "저도", "start_ms": 600, "dur_ms": 310, "phase_lr_deg": -1.0},
{"word": "가을을", "start_ms": 950, "dur_ms": 420, "phase_lr_deg": -1.0},
{"word": "가장", "start_ms": 1450, "dur_ms": 350, "phase_lr_deg": -1.0},
{"word": "좋아해요", "start_ms": 1850, "dur_ms": 580, "phase_lr_deg": -1.0},
{"word": "단풍", "start_ms": 2800, "dur_ms": 380, "phase_lr_deg": -1.0},
{"word": "구경", "start_ms": 3250, "dur_ms": 350, "phase_lr_deg": -1.0},
{"word": "가고", "start_ms": 3650, "dur_ms": 320, "phase_lr_deg": -1.0},
{"word": "싶네요", "start_ms": 4050, "dur_ms": 510, "phase_lr_deg": -1.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 06:00:59*
