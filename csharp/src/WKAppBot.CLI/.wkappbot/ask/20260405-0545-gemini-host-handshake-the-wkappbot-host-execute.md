# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=14936  time=05:43:42  uptime=14323s  mem=436MB  system_processes=354"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_054024.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 570-105-571 574-571-514-570-517-507-157-157-570-571-715-157-175-107-170-170-170 574-570-517-571-157-175-510-750-570-571-570-571-507-105-175-510 570-750-157-157-150-571-175-750-1_V.mp3)
  Segment 2: 1576ms ~ 8632ms (file: 576-123-321-327-326-367-762-236-267-615-263-765-165-627-756-576-106-103-657-326-612-321-321-327-267-263-273-230-253-317-531-273-267-627-572-627-123-120-716-267-210-207-201-276-2_W.mp3)
  Segment 3: 9632ms ~ 10208ms (file: 137-136-103-103-134-103-136-103-103-103-132-103-132-103-103-103-103-103-102-136 517-315-716-763-130-137-312-132-310-312-160-736-750-370-175-571-763_V.mp3)
  Segment 4: 11208ms ~ 11784ms (file: 765-670-670-632-312-671-136-176-657-657-103-103-162-160-251-156-103-613-312-763-132_V.mp3)
  Segment 5: 12784ms ~ 21532ms (file: 132-321-213-213-620-267-531-670-617 103-134-103-627-625-602 162-263-531-610-217-267-275 120-162_V.mp3)
  Segment 6: 22532ms ~ 23108ms (file: 510-105-157-507-510-501-517-157-150-150-501-150-150-501-150-103-150-157-501-150-571-105-157-150-150-501-516-501 510-150-105-157-501-513-510-501-517-150-510-517-150-105-503-150-1_V.mp3)
  Segment 7: 24108ms ~ 24684ms (file: 510-517-150-105-501-507-517-501-501-510-510-150-510-150-510-510-501-510-507-507-501-501-501-501-570-570 504-570-570-510-507-517-157-507-501-510-501-501-501-510-510-501-510-510-5_V.mp3)
  Segment 8: 25684ms ~ 26260ms (file: 627-657-672-675-765-756 627-675-675-652-625-675-261-162-126-265-205 361-213-326-672-627-162_W.mp3)
  Segment 9: 27260ms ~ 34568ms (file: 123-753-106-160-321-270-173-106-751-106-756-263-312-130-327-167-756-167-136-651-261-126-210-716-276-312-327-276-756-276-160-321-231-574-321-753-752-102-237-375-175-106-123-765-2_W.mp3)
  Segment 10: 35568ms ~ 36144ms (file: 750-270-362-123-327-237-327-327-326-130-136-671-672-126-163-130-237-230-327-327-326-236-137-157-531-123-524-135-103-132-301-351-236-725-620-270-765-217-527-276-627-320-325-137-1_W.mp3)
  Segment 11: 37144ms ~ 37720ms (file: 576-675-675-675-765-672-672-265-672-167-567-657-765-175-756_W.mp3)
  Segment 12: 38720ms ~ 39296ms (file: 105-157-175-571-570-750-157-570-571-750-175-501 157-105-175-105-107-107-107-103-107-107-170-715-170-765-751-756-765-675-671-657-657-657-657-657 153-105-107-103-103-107-107-103-1_W.mp3)
  Segment 13: 40296ms ~ 40872ms (file: 762-160-102-203-316-617-136-130-163-165-156-120-301-627-167-302-326-236-213-320-526-135-103-136-126-672-123-623-312-163-126-610-103-236-321-310-102-160-637-326-132-617-163-263-1_W.mp3)
  Segment 14: 41872ms ~ 42448ms (file: 756-756-756-735-753-765-736-753-735-753 715-751-751-715-173-371-107-137-715-173-731-701-107-371-731-175-701-735-713-710-713-751-715-730 715-175-715-713-105-710-317-731-137-760-7_V.mp3)
  Segment 15: 43448ms ~ 44024ms (file: 756-762-716-237-736-576-702-731-576-726-752-763-362-267-762-573-215-670-257-326-257-675-530-632-725-326-256-257-762-573-726-267-265-572-705-276-627-627-725-236-203-362-574-137-7_V.mp3)
  Segment 16: 45024ms ~ 45600ms (file: 571-715-701-157-105-571-751-715-170-175-715-751-715-175-175-105-715-751-715-175-173-701-175-713-751 570-570-571-157-715-751-157-157-751-715-175-175-571-175-715-715-751-715-753-7_W.mp3)
  Segment 17: 46600ms ~ 54088ms (file: 210-271-231-271-153-312-260-270-102-273-310-267-123-203-103-310-257-172-130-257-217-130-103-310-235-203-267-270-726-572-157-156-132-302-267-231-132-327-372-570-756-102-267-702-3_V.mp3)
  Segment 18: 55088ms ~ 55664ms (file: 435-150-104-145-103-147-104-104-104-140-153-104-103-143-103-143-105-104-103-154-135-134-143 431-304-174-410-301-134-314-134-341-314-345-134-130-130-103-314-103-451-415-130-413-1_V.mp3)
  Segment 19: 56664ms ~ 57240ms (file: 320-372-703-765-602-206-765-267-706-760-762-370-306-765-765-230-301-756-750-367-752-207-236-230-103-123-613-320-632-632-267-203-320-302-301-306-132-312-316-163-316-306-301-361-3_V.mp3)
  Segment 20: 58240ms ~ 58816ms (file: 735-756-765-756-756-765-753-756-765-751-715-765-765-765-715 756-756-751-756-756-715-751-765-753-751-753-751-752_W.mp3)
  Segment 21: 59816ms ~ 70184ms (file: 316-103-167-763-672-760-765-756-321-132-123-103-765-756-765-621-160-162-675-167-756-302-176-143-675-754-750-726-301-201-201-123-103-130-351-261-210-203-257-620-106-312-210-103-1_W.mp3)
  Segment 22: 71184ms ~ 71760ms (file: 132-216-326-573-325-527-132-375-326-726-207-267-270-574-207-267-326-276-726-706-263-306-327-312-765-317-301-317-507-317-573-752-571-712-750-137-265-571-317-326-570-162-567-123-2_V.mp3)
  Segment 23: 72760ms ~ 73336ms (file: 103-102-106-104-145-142-145-136 163-176-132-137-134-125-127 571-715-751-761-617-671-761-671-672-671-761-671-765-761-751-765-175-756-567-756-715-175-157-157-516-157-615-562-162-1_W.mp3)
  Segment 24: 74336ms ~ 74912ms (file: 567-576-756-567-123-751-751-751-765-753-751-712-765-756 263 632-326-163-106-132-321-126-153-103-135-103-561-106-136-106-126-136-657-316-167-136-167-103-106-136-617-167-163-153-1_V.mp3)
  Segment 25: 75912ms ~ 76488ms (file: 756-537-276-713-726-132-351-527-357-275-572-216-213-756-627-546-546-512-570-512-513-132-570-561-726-671-213-275-257-217-570-576-752-165-635-625-132-735-321-351-571-321-572-132-7_V.mp3)
  Segment 26: 77488ms ~ 78064ms (file: 261-263-106-631-160-106-621-126-306-162-621-612-261-106-102-605-176-162-126 362-326-263-376-765-672-670-167-621-612-162-632-132-627-672-675-751-327-312-621-765-672-762-136-632-5_V.mp3)
  Segment 27: 79064ms ~ 79640ms (file: 126-127-137-136-103-103-103-123-124-316-136-132-413-132-324-312-132-735-237-326-127-213-321-216-216-267-261-231 106-106-103-167-163-316-123-632-316-130-620-627-260-261-620-627-6_V.mp3)
  Segment 28: 80640ms ~ 81216ms (file: 576-567-167-163-136 136 301-103-130 163-631-613-316-631-261-623-621-623-632-627-632-623-602-612-130-312-326-136_W.mp3)
  Segment 29: 82216ms ~ 82792ms (file: 126-671-720-261-765-765-607-765-716-716_W.mp3)
  Segment 30: 83792ms ~ 88544ms (file: 132-126-752-210-123-731-132-172-132-512-127-132-126-132-210-210-213-132-123-216-132-123-132-217-157-261-126-213-216-123-127-123-132-127-132-136-126-132-127-267-263-216-231-317-7_W.mp3)
  Segment 31: 89544ms ~ 90120ms (file: 132-750-657-267 675-160-106-176-162-175-301-236-236-236-326-230-267-263-621-152-134-143-135-137-137-137-126-136-310-301-173-175-620-260-263-621-263-672-263-136-216-621-623-326_V.mp3)
  Segment 32: 91120ms ~ 113584ms (file: 103-102-123-236-230-132-103-103-126-321-130-103-103-123-102-216-260-326-230-123-163 130-543-153-260-206-206-231-132-213-103-137-312-357-376-765-673-756-321-302-316-105-132-312-3_V.mp3)
  Segment 33: 114584ms ~ 124088ms (file: 302-203-102-210-210-301-276-230-753-231-201-765-357-753-750-756-320-231-213-732-327-517-123-312-132-123-132-321-213-123-132-201-206-206-206-132-621-615-162-612-615-261-672-621-6_W.mp3)
  Segment 34: 125088ms ~ 135240ms (file: 316-567-356-627-103-176 236-102-312-572-620-356-765-176-632-507-547-357-312-130-103-132-321-723-732-167-617-106-162-267-213-231-753-725-216-236-231-316-576-372-237-320-312-301-7_W_stt_the.mp3)
  Segment 35: 136240ms ~ 136816ms (file: 367-367-673-673-673-671-673-673-367-376-317-673-372-376-167-736-617-673-671-761-167-731-763-613-376-371-637-673-673-367-623-362-326-361-326-263-236-316_W.mp3)
  Segment 36: 137816ms ~ 138392ms (file: 627-627-627-621-621-627-671-672-672-672-621-621-623-316-675-670-672-670_W.mp3)
  Segment 37: 139392ms ~ 145476ms (file: 310-132-130-107-163-537-567-506-137-312-203-213-201-207-126-126-723-270-102-706-761-765-736-736-762-763-230-176-201-270-273-316-312-701-326-106-236-102-103-267-237-217-302-102-2_W.mp3)
  Segment 38: 146476ms ~ 147052ms (file: 107-132-130-321-321-302-301-756-703-327-302-302-327-372-273 672-670 765-675 723-213-207-231-132-213-207-310-321-327-273-756-137-135-321-231-203-103-103-301-231-207-231-321-372-2_V.mp3)
  Segment 39: 148052ms ~ 158060ms (file: 236-752-230-231-120-216-572-230-235-201-627-526-576-257-547-120-612-231-572-267-230-675-672-123-327-126-162-102-103-612-126-567-105-126-167-617-617-103-231-102-302-107-126-310-1_W.mp3)
  Segment 40: 159060ms ~ 159636ms (file: 173-136-136-123-526-562-650-632-361-652-675-671-716 162 567-652-627-672-625-627-176-362-176-621-267 562-625-216-213-126-162-132-123-136-361-136-317-317-365-376-167-356-120-135-5_W.mp3)
  Segment 41: 160636ms ~ 161212ms (file: 103-260-163-136-106-205-107-167-176-576-512-136-623-137-610-160-160-176-625-127-126-165-103-106-106-106-654-167-761-756-612-176-601-162-610-320 621-136-106-601-165-137-163-761-1_V.mp3)
  Segment 42: 162212ms ~ 162788ms (file: 576-517-612-103-102-132-167-136-317-167-130-136-103-651-236-132-103-132-103-103-132-751-102-130-136-316-165-376-513-103-103-561-516-172-137-127-106-136-132-103-130-132-136-103-1_V.mp3)
  Segment 43: 163788ms ~ 164364ms (file: 162-263-136-162-316-123-123-162-130-321-213-136-157-576-765-756-312-103-217-162-103-132 675-675-675 136_V.mp3)
  Segment 44: 165364ms ~ 165940ms (file: 672-203-573-321-570-257-367-216-326-712-312-752-256-257-162-136-753-375-513-135-317-137-367-137-136-132-136-316-675-675-675-675-675-751-173-751-132-267-261-610 625-367-672-671-6_W.mp3)
  Segment 45: 166940ms ~ 167516ms (file: 327-562-675-625-237-267-263-326-627-536-567-267-236-627-623-627-657-675-675 103-103-136-103-567-576-576-571-576-765-672-761-761-170-623-567-567-615-657-567-576-567-165-256-135-5_W.mp3)
  Segment 46: 168516ms ~ 182412ms (file: 123-231-126-126-103-132-375-752-257-315-150-574-130-231-130-137-201-210-132-216-756-267-261-725-765-715-170-756-756_W.mp3)
  Segment 47: 183412ms ~ 194068ms (file: 125-621-130-571-236-371-675-167-106-213-103-172-167-123-715-741-174-216-210-216-217-267-213-213-312-371-310-321-132-613-617-617-103-106-175-106-617-673-132-312-327-213-751-172-6_W.mp3)
  Segment 48: 195068ms ~ 195644ms (file: 637-621-617-362-632-627-672-123-312-617-617-623-167-672-671-632-637-613-631-617-217-257-627-621-632-162-371-126-672-612-621-612-126-675-621-675-672-627-762-612-126-672-627 175-1_V.mp3)
  Segment 49: 196644ms ~ 197220ms (file: 132-103-130-103-130-312-132-126-136-136-123-136-132-567-506-576-123-637-321-132_V.mp3)
  Segment 50: 198220ms ~ 198796ms (file: 376-176-756-675-672-762-561-756-572-726-751-256-507-725-576-765-572-265-712-752-765-567-502-572-725-726-573-576-726-725-723-627-267-507-723-720-267-267-257-672-572-570-706-316-5_V.mp3)
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
> **Time**: 2026-04-05 05:45:12
> **AI**: gemini

---

## Response

AnalysisAnalysisGemini의 응답[
{
"segment": 1,
"transcript": "반갑습니다.",
"speaker": "speaker_1",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "반갑습니다", "start_ms": 100, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "아주 너무 열심히 하시고 어 그 다음에 거기에 맞춰서 성과도 잘 내고 있고 그다음에 학생들도 굉장히",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "아주", "start_ms": 50, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 480, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "열심히", "start_ms": 1050, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "하시고", "start_ms": 1700, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "어", "start_ms": 2500, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 2750, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "다음에", "start_ms": 2950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "거기에", "start_ms": 3450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "맞춰서", "start_ms": 3900, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "성과도", "start_ms": 4500, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 5150, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "내고", "start_ms": 5450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있고", "start_ms": 5850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그다음에", "start_ms": 6250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "학생들도", "start_ms": 6750, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "굉장히", "start_ms": 7450, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "좋아하십니다.",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "좋아하십니다", "start_ms": 20, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "이게 뭘 의미한다고 했죠 리듬이 끊어지는 순간 시장이 하락 방향으로 완전히 전환되는 구간",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "이게", "start_ms": 80, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "뭘", "start_ms": 400, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "의미한다고", "start_ms": 650, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "했죠", "start_ms": 1350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "리듬이", "start_ms": 1800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "끊어지는", "start_ms": 2300, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "순간", "start_ms": 2900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "시장이", "start_ms": 3400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "하락", "start_ms": 3900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "방향으로", "start_ms": 4350, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "완전히", "start_ms": 4950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "전환되는", "start_ms": 5450, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "구간", "start_ms": 6050, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "노이즈가 완전히 제거된 구간",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "노이즈가", "start_ms": 30, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "완전히", "start_ms": 550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "제거된", "start_ms": 1050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "구간", "start_ms": 1550, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "진입하기 가장 좋은 타점",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "진입하기", "start_ms": 20, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "타점", "start_ms": 1300, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "이단이라는 게 할머니의 마음입니다.",
"speaker": "speaker_3",
"confidence": 0.91,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "이단이라는", "start_ms": 40, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 650, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "할머니의", "start_ms": 850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "마음입니다", "start_ms": 1400, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "우리 손녀딸이 하나라는 건 해야지 내가 아무것도 모를 리가 없어",
"speaker": "speaker_3",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "우리", "start_ms": 100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "손녀딸이", "start_ms": 450, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "하나라는", "start_ms": 1100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "건", "start_ms": 1650, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "해야지", "start_ms": 1900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "내가", "start_ms": 2400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "아무것도", "start_ms": 2800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "모를", "start_ms": 3350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "리가", "start_ms": 3700, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "없어", "start_ms": 4000, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "파트너들에게 방공포대를 실제로 공급할 수 있었는지를 잘 설명해준다 투자자들 역시 국방부들 못지 않게 빠르게 반응했다",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "파트너들에게", "start_ms": 50, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "방공포대를", "start_ms": 800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "실제로", "start_ms": 1450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "공급할", "start_ms": 1950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 2450, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있었는지를", "start_ms": 2600, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 3250, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "설명해준다", "start_ms": 3500, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "투자자들", "start_ms": 4250, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "역시", "start_ms": 4800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "국방부들", "start_ms": 5200, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "못지", "start_ms": 5800, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "않게", "start_ms": 6100, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "빠르게", "start_ms": 6400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "반응했다", "start_ms": 6900, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 11,
"transcript": "한국 매체의 보도에 따르면 천궁 2의 개발사 가운데 하나인 LIG넥스원의 주가는 성공적인 요격 소식이 전해진",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "한국", "start_ms": 40, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "매체의", "start_ms": 450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "보도에", "start_ms": 900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "따르면", "start_ms": 1350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "천궁", "start_ms": 1850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "2의", "start_ms": 2250, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "개발사", "start_ms": 2500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "가운데", "start_ms": 3000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "하나인", "start_ms": 3500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "LIG넥스원의", "start_ms": 4000, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "주가는", "start_ms": 4850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "성공적인", "start_ms": 5350, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "요격", "start_ms": 5950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "소식이", "start_ms": 6350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "전해진", "start_ms": 6850, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "그렇죠 바람에 한 줄기 빛까지 드리는 창이랍니다.",
"speaker": "speaker_5",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "그렇죠", "start_ms": 120, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "바람에", "start_ms": 600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 1100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "줄기", "start_ms": 1300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "빛까지", "start_ms": 1750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "드리는", "start_ms": 2300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "창이랍니다", "start_ms": 2800, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 14,
"transcript": "아 진짜 기가 막히긴 하네",
"speaker": "speaker_6",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "아", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "기가", "start_ms": 600, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "막히긴", "start_ms": 900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "하네", "start_ms": 1400, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "손바닥만한 딱 이 빛이 들어와요",
"speaker": "speaker_5",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "손바닥만한", "start_ms": 60, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "딱", "start_ms": 750, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 1000, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "빛이", "start_ms": 1200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "들어와요", "start_ms": 1600, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "했더라도 일부는 과거에 있는 것들을 많이 쓰는 경우도 있고 이렇다 보니까 하루아침에 폐기를 할 수는 없겠죠 그대신에 이제",
"speaker": "speaker_7",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "했더라도", "start_ms": 100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "일부는", "start_ms": 750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "과거에", "start_ms": 1250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 1750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "것들을", "start_ms": 2100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 2600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "쓰는", "start_ms": 3000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "경우도", "start_ms": 3350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "있고", "start_ms": 3850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이렇다", "start_ms": 4200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "보니까", "start_ms": 4650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "하루아침에", "start_ms": 5100, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "폐기를", "start_ms": 5850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 6350, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "수는", "start_ms": 6600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "없겠죠", "start_ms": 6950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "그대신에", "start_ms": 7450, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 8050, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 18,
"transcript": "물 맑아서 사람도 해맑은 걸까요 대신에",
"speaker": "speaker_8",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "물", "start_ms": 150, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "맑아서", "start_ms": 450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "사람도", "start_ms": 1000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "해맑은", "start_ms": 1500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "걸까요", "start_ms": 2050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "대신에", "start_ms": 2550, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "But instead of the trail being muddy, right, from all the moisture, this is dirt here. It fits this on the go, and it's just such a much better walking experience because my shoes aren't going to get dirty, and",
"speaker": "speaker_9",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "But", "start_ms": 100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "instead", "start_ms": 350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "of", "start_ms": 800, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 1000, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "trail", "start_ms": 1200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "being", "start_ms": 1600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "muddy,", "start_ms": 1950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "right,", "start_ms": 2450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "from", "start_ms": 2800, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "all", "start_ms": 3100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 3350, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "moisture,", "start_ms": 3550, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "this", "start_ms": 4200, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 4500, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "dirt", "start_ms": 4700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "here.", "start_ms": 5050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "It", "start_ms": 5450, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "fits", "start_ms": 5650, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "this", "start_ms": 5950, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "on", "start_ms": 6200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 6400, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "go,", "start_ms": 6600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "and", "start_ms": 6950, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "it's", "start_ms": 7200, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "just", "start_ms": 7500, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "such", "start_ms": 7800, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 8100, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "much", "start_ms": 8250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "better", "start_ms": 8600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "walking", "start_ms": 9000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "experience", "start_ms": 9450, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "because", "start_ms": 10200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "my", "start_ms": 10700, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "shoes", "start_ms": 10950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "aren't", "start_ms": 11350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "going", "start_ms": 11750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 12100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "get", "start_ms": 12300, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "dirty,", "start_ms": 12600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "and", "start_ms": 13050, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
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
"transcript": "유연하게 생각하는 뇌구조를 만들어야 된다고 해요 결국 내 조직 내 분야에 딱 맞는 AI 전략은 AI가 알아서 짜주는 게 아니라 비즈니스 리더 본인만이 만들 수",
"speaker": "speaker_10",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "유연하게", "start_ms": 50, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "생각하는", "start_ms": 700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "뇌구조를", "start_ms": 1250, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "만들어야", "start_ms": 1900, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "된다고", "start_ms": 2500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "해요", "start_ms": 3000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "결국", "start_ms": 3400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "내", "start_ms": 3850, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "조직", "start_ms": 4050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "내", "start_ms": 4450, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "분야에", "start_ms": 4650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "딱", "start_ms": 5150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "맞는", "start_ms": 5400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "AI", "start_ms": 5750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "전략은", "start_ms": 6150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "AI가", "start_ms": 6700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "알아서", "start_ms": 7200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "짜주는", "start_ms": 7700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 8200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 8400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "비즈니스", "start_ms": 8900, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "리더", "start_ms": 9550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "본인만이", "start_ms": 9950, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "만들", "start_ms": 10650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 11050, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "We had high change with last year's volunteers and it was really good function. I thought they liked it.",
"speaker": "speaker_11",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "We", "start_ms": 120, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "had", "start_ms": 300, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "high", "start_ms": 600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "change", "start_ms": 950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "with", "start_ms": 1450, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "last", "start_ms": 1750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "year's", "start_ms": 2100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "volunteers", "start_ms": 2550, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "and", "start_ms": 3300, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "it", "start_ms": 3550, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "was", "start_ms": 3750, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "really", "start_ms": 4050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "good", "start_ms": 4450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "function.", "start_ms": 4800, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 5550, "dur_ms": 100, "phase_lr_deg": 0.0

---
*Generated by WKAppBot ask gemini — 2026-04-05 05:45:12*
