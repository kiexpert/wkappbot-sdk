# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43084  time=07:47:23  uptime=8217s  mem=413MB  system_processes=411"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_074449.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 15804ms (file: 132-135-362-312-327-230-321-132-230-310-230-321-230-312-320-260-206-123-312-326-320-310-301-312-320-320-327-217-273-270-276-726-276-217-625-726-137-312-103-317-103-263-267-103-3_W.mp3)
  Segment 2: 16804ms ~ 22708ms (file: 312-123-321-231-130-351-321-132-123-602-267-217-231-231-725-237-725-765-726-160-156-576-130-367-372-536-312-360-526-271-205-572-720-120-132-310-162-267-312-361-126-136-163-163-6_W.mp3)
  Segment 3: 23708ms ~ 32312ms (file: 310-301-236-130-312-216-136-162-612-127-301-231-213-130-132-312-123-162-126-261-216-612-623-623-632-310-630-603-673-670-571-573-316-130-726-127-217-267-756-517-130-312-103-132-1_W.mp3)
  Segment 4: 33312ms ~ 42744ms (file: 765-672-267-263-312-672-236-103-236-765-237-326-753-136-230-213-236-263-267-321-627-261-267-762-123-213-763-173-326-765-267-756-267-267-213-321-675-657-765-263-123-143-163-231-3_W.mp3)
  Segment 5: 43744ms ~ 44320ms (file: 750-756-517-750-751-756 571-756-576-570-517-571-756-576-576-756-570-571-576-574-751-751-170-705-756-751-756 570-571-571-570-750-570_W.mp3)
  Segment 6: 45320ms ~ 45896ms (file: 705-705-706-751-710-751-705-701-705-715-175-756-576-562-561-125-126-175-156-526-125-657-256-156-157-162-165-126-160-102-561-572-125-156-157-156-576-175-175-170-763-671-761-157-1_V.mp3)
  Segment 7: 46896ms ~ 47472ms (file: 610-361-631-612-163-621 236-362 127-102-120-201-352-523-235-235-273-357-526-527-502-507-765-675-763-763-263-623-216-103-765-763-765-763-765-765-765-137-657-675-675-637-267-675-6_W.mp3)
  Segment 8: 48472ms ~ 49048ms (file: 310-675-316-726-761-765-765-127 123-231-716-716-375-367-367-673-526-102-261-513-625-215-106-752-621-210-175-512-216-123-561-765-123-621-532-326-127-126-572-613-137-576 136-312-5_V.mp3)
  Segment 9: 50048ms ~ 50624ms (file: 157-517-517-571-571-517-157-517-157-157-517-517-571-517-157-517-571-571-103-103-135-103-103-103-130-107-137-103-137-130-157-157-157-517-517-517-157-517-517-157-517-517-157-516-5_V.mp3)
  Segment 10: 51624ms ~ 62532ms (file: 230-230-231-321-130-103-132-103-123-231-261-127-123-321-230-230-213-310-103-301-320-230-231-321-213-230-312-741-725-172-237-320-236-130-312-236-326-312-267-267-120-123-312-325-1_W.mp3)
  Segment 11: 63532ms ~ 69724ms (file: 231-312-321-213-312-231-130-132-273-273-251-253-102-103-310-756-573-652-623-621-623-716-276-132-537-301-310-107-712-327-310-301-103-135-120-123-213-231-263-375-725-120-231-321-5_W.mp3)
  Segment 12: 70724ms ~ 71300ms (file: 730-752-157-136-163-130-672-762-761-716-721-127-172-271-761-765-726-712-571-571-157-751-571-157-751-175-751-157-571-750-571-157-175-571-750-705-753-751-571-751-157-751-715 571-5_V.mp3)
  Segment 13: 72300ms ~ 72876ms (file: 126-572-276-263-321-257-267-572-750-762-267-265-312-761-572-675-312-502-235-256-325-213-567-231-705-536-527-236-135-316-657-376-326-135-315-517-750-136-312-754_V.mp3)
  Segment 14: 73876ms ~ 74452ms (file: 163-307-165-132-175-175-105-751-175-761-613-610-576-765-621-123-275-570-172-572-173-371-216-213-123-256-705-756-572-125-253-127-312-265-103-130-163-103-123-130-132-206-312-371-2_V.mp3)
  Segment 15: 75452ms ~ 76028ms (file: 576-537-652-567-754 752-572-572-735-756-735-576-567-567-657-652-567-356-576-563-527-576-726-657-652-567-562-756-523-657-652-562-652-576-752-527-756-576-567-576-527-617-675-657-7_V.mp3)
  Segment 16: 77028ms ~ 77604ms (file: 126-512-310-307-367-376-362-132-507-652-652-567-521-256-263-130-756-265-567-754-567-760-650-637-130-576-162-312-136-621-657-675-615-650-627-376-361-603-310-306-376-236-163-601-6_V.mp3)
  Segment 17: 78604ms ~ 86668ms (file: 257-302-735-276-206-765-230-760-265-267-231-312-312-712-127-173-725-756-263-251-275-160-617-160-765-675-321-123-231-572-756-576-125-621-105-312-317-103-132-127-612-162-216-263-1_W.mp3)
  Segment 18: 87668ms ~ 95588ms (file: 102-267-754-132-750-130-236-231-753-523-562-536-672-236-160-210-230-263-203-607-102-132-753-672-265-520-213-236-231-321-236-254-231-236-102-123-765-750-312-231-102-261-237-102-2_W.mp3)
  Segment 19: 96588ms ~ 97164ms (file: 357-237-735-137-130-761-375-130-312-750-675-762-570-675-574-573-612-732-261-206-216-231-123-572-617-507-517-527-756-527-216-157-136-751-213-257-527-736-726-746-253-547-725-317-2_V.mp3)
  Segment 20: 98164ms ~ 108208ms (file: 260-203-276-627-260-203-263-102-326-130-102-263-231-230-130-132-236-263-631-231-321-216-260-267-267-267-103-132-102-310-236-132-231-357-136-632-137-635-671-672-267-106-761-537-1_W.mp3)
  Segment 21: 109208ms ~ 119432ms (file: 207-213-213-230-132-231-213-231-316-305-130-576-120-217-320-752-310-301-573-756-765-103-753-753-573-312-102-102-107-765-213-765-723-302-257-725-301-132-167-310-173-230-310-203-1_W.mp3)
  Segment 22: 120432ms ~ 128064ms (file: 251-251-156-170-213-231-213-312-102-102-123-130-310-357-301-123-132-103-301-143-103-312-320-201-103-106-517-216-620-260-130-206-201-123-126-756-206-320-321-731-317-130-570-567-3_W.mp3)
  Segment 23: 129064ms ~ 129640ms (file: 736-376-731-371-713-367-376-376-736-716-762-760_W.mp3)
  Segment 24: 130640ms ~ 136580ms (file: 236-620-602-362-231-362-367-317-317-310-310-103-312-230-132-312-103-163-613-657-675-657-675-675-657-657-675-132-213-670-261-102-162-106-130-103-106-102-312-120-312-105-157-517-1_V.mp3)
  Segment 25: 137580ms ~ 138156ms (file: 751-157-175-175-175-175-176-167-176-176 135-317-715-756-761-137-173-176-175-176-167-176-167-176-731 315-137-173-376-167-173-137-176-176-176-167-176-176-167-176 317-137-137-715-5_V.mp3)
  Segment 26: 139156ms ~ 139732ms (file: 561-216-136-165-137-602-612-136-172-167-137-137-172-173-172-126-127-172-126-127-127-127-172-721-276-726-271-726-725-725-756-176-752-713-127_W.mp3)
  Segment 27: 140732ms ~ 145988ms (file: 321-302-632-623-362-723-725-320-735-715-736-736-537-726-276-762-627-136-103-321-167-321-321-301-231-301-326-312-230-312-312-132-321-103-310-317-310-136-316-321-312-320-310-357-1_W.mp3)
  Segment 28: 146988ms ~ 154548ms (file: 102-236-231-230-321-612-163-167-261-206-216-621-201-230-203-213-201-231-312-130-230-321-312-326-103-132-650-206-321-357-125-275-752-312-537-751-571-257-102-123-216-210-230-320-2_W.mp3)
  Segment 29: 155548ms ~ 156124ms (file: 517-576-167-675-671-316-627-265-657-572-573-165-317-715-725-163-761-361-176-613-316-132-326-256-612-716-175-162-267-613-312-261-260-136-572-507-657-156 326-672-621-132-267-362-7_W.mp3)
  Segment 30: 157124ms ~ 157700ms (file: 756-675-175-576-657-567-576-756-756-571-576-571-576-751-756-576-517-175-163 561-571-576-751-571-576-132-321-126 517-157-571-576-576-576-576-571-576-517-163_W.mp3)
  Segment 31: 158700ms ~ 159276ms (file: 153-576-375-175-513 126 672 163-137_W.mp3)
  Segment 32: 160276ms ~ 160852ms (file: 576-265-752-750-263-675-537-623-536-673-675-265-163-302-516-531-716-167-137-516-615-635-365-635-617-361_W.mp3)
  Segment 33: 161852ms ~ 162428ms (file: 617-176-517-107-175-750-706-701-705-705-710-751-705-715-751-750-751-710-750-751 107-170-176-705-715-170-107-715-710-715-715-175-175-715-571 105-170-170-170-705-751-751-756-675-7_V.mp3)
  Segment 34: 163428ms ~ 164004ms (file: 217-132-502-623-312-623-573-365-357-235-572-517-257-726-725-627-526-153-607-162-127-265-137-367-265-235-507-567-725-573-756-750-627-725-123-132-725-217-125-765-156-576-367-602 7_V.mp3)
  Segment 35: 165004ms ~ 165580ms (file: 132-136-367-136-376-675-615-163-715-317-165-361-312-103-136-162-163-175-137-571-256-351-613-716-736-536-213-312-567-657-517-517-537-536-672-675-763-137-163-637-156-130-163-156-6_W.mp3)
  Segment 36: 166580ms ~ 167156ms (file: 135-103-130-103-135-153-105-107-175-105-105-157-130-317-103-175-150 150-105-531-153-125-173-107-105-107-153-510-305-103-150-102-102-102-105-105-105-102-132 150-157-157-517-135-1_V.mp3)
  Segment 37: 168156ms ~ 175680ms (file: 602-206-163-567-362-154-106-510-571-321-753-301-310-301-316-326-360-326-263-326-316-673-621-231-263-756-576-576-576-756-765-675-675-572-106-165-236-260-206-627-210-671-673-236-6_W.mp3)
  Segment 38: 176680ms ~ 177256ms (file: 176-176-157-176-175-173-175-176-175-167-751 130-137-103-173-175-157-157-175-157-157-175-175-157-176-175-176-157-751 130-137-175-175-157-175-176-751-175-157-156-157-176-167-176-1_V.mp3)
  Segment 39: 178256ms ~ 194888ms (file: 126-152-276-732-103-310-231-321-312-231-312-137-103-627 321-310-213-267-267-276-260-320-576-130-103-267-137-107-213-267-260-301-157-213-123-253-673-316-534-213-731 672-567-152-7_W.mp3)
  Segment 40: 195888ms ~ 196464ms (file: 761-167-671-763-176-756-765-673-671-761-123-137-317-317-367-317-136-157-157-751-715-107-703-670-716-716-716-176-710-761-176-761-761-715-176-710-761-713-175-176-176-137-135-173-1_W.mp3)
  Segment 41: 197464ms ~ 205708ms (file: 210-751-573-756-725-130-231-723-130-206-102-216-725-127-170-612-316-103-132-310-207-310-216-753-320-210-126-720-103-715-213-103-132-312-267-270-231-103-301-231-752-756-756-103-3_W.mp3)
  Segment 42: 206708ms ~ 207284ms (file: 103-657-213-130-137-176-137-716-713-317-371-132-132-312-250-152-275-273-271-126-257-751-627-216-716-510-615-531-362-276-312-361-165-316-312-310-372-325-307-735-263-532-751-372-6_V.mp3)
  Segment 43: 208284ms ~ 208860ms (file: 371-371-317-317-371-137-371-503-571-573-576-576-756-573-571-570 765_W.mp3)
  Segment 44: 209860ms ~ 210436ms (file: 576-765-571-574-576-765-617-765-165-715-107-605-671-756-751 671-603-631 176 312-163-102 123 576-576 136 725-152 263-126-106-176-160-102-570-107-210-312-103-163-132-160-160-162-1_W.mp3)
  Segment 45: 211436ms ~ 219752ms (file: 213-310-260-206-301-136-301-103-176-107-326-301-103-326-320-276-213-260-302-306-176-107-126-273-321-130-132-103-106-723-213-627-206-230-160-106-107-160-170-130-132-102-301-321-2_W.mp3)
  Segment 46: 220752ms ~ 228168ms (file: 213-526-231-103-132-102-612-623-267-235-623-136-231-576-102-165-752-263-267-756-120-267-136-123-123-130-321-326-160-361-160-106-317-672-756-237-163-312-543-276-576-235-267-321-2_W.mp3)
  Segment 47: 229168ms ~ 236080ms (file: 130-103-102-130-130-102-132-103-103-123-103-132-123-123-132-130-132-312-132-102-123-310-106-310-132-312-103-123-312-103-123-103-132-132-102-103-321-132-132-102-103-103-103-132-3_V.mp3)
  Segment 48: 237080ms ~ 237656ms (file: 365-572-753-752-756-275-671-650-216-326-267-276-236-752-725-265-275-725-752-275-723-752-721-716-725-723-721-725-732-726-725-276-732-752-357-375-752-765-726-721-275-273-726-215-7_W.mp3)
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
> **Time**: 2026-04-06 07:49:20
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "안녕하세요 여러분 오늘은 한국어 회화에서 자주 쓰이는 유용한 표현들을 함께 배워보도록 하겠습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "여러분", "start_ms": 800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "오늘은", "start_ms": 1300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "한국어", "start_ms": 1750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "회화에서", "start_ms": 2250, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "자주", "start_ms": 2850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "쓰이는", "start_ms": 3200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "유용한", "start_ms": 3650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "표현들을", "start_ms": 4150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "함께", "start_ms": 4700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "배워보도록", "start_ms": 5100, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "하겠습니다", "start_ms": 5800, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "첫 번째 표현은 바로 밥 먹었어입니다 한국인들이 인사 대신 정말 많이 사용하죠",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "첫", "start_ms": 120, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "번째", "start_ms": 300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "표현은", "start_ms": 650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 1100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "밥", "start_ms": 1450, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "먹었어", "start_ms": 1700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "입니다", "start_ms": 2200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "한국인들이", "start_ms": 2600, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "인사", "start_ms": 3250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "대신", "start_ms": 3600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 4000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 4400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "사용하죠", "start_ms": 4750, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "상대방의 안부를 묻는 아주 친근한 표현이에요 자 그럼 다 같이 한번 큰 소리로 따라해 볼까요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "상대방의", "start_ms": 180, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "안부를", "start_ms": 730, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "묻는", "start_ms": 1180, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 1530, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "친근한", "start_ms": 1880, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "표현이에요", "start_ms": 2380, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "자", "start_ms": 3100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "그럼", "start_ms": 3350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 3700, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 3900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "한번", "start_ms": 4250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 4650, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "소리로", "start_ms": 4900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "따라해", "start_ms": 5400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "볼까요", "start_ms": 5900, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "두 번째는 대박이라는 단어입니다 놀라운 일이 생겼을 때나 감탄할 때 아주 유용하게 쓰이죠",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "두", "start_ms": 140, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "번째는", "start_ms": 320, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "대박이라는", "start_ms": 720, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "단어입니다", "start_ms": 1370, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "놀라운", "start_ms": 2100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "일이", "start_ms": 2600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "생겼을", "start_ms": 2950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "때나", "start_ms": 3450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "감탄할", "start_ms": 3800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 4300, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 4550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "유용하게", "start_ms": 4900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "쓰이죠", "start_ms": 5450, "dur_ms": 500, "phase_lr_deg": 0.0}
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
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
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
"transcript": "세 번째는 헐입니다 보통 어이가 없거나 황당한 상황에서 쓰이는 감탄사라고 할 수 있죠",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 160,
"audio_type": "speech",
"words": [
{"word": "세", "start_ms": 160, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "번째는", "start_ms": 350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "헐입니다", "start_ms": 750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "보통", "start_ms": 1350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "어이가", "start_ms": 1750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "없거나", "start_ms": 2200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "황당한", "start_ms": 2700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "상황에서", "start_ms": 3200, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "쓰이는", "start_ms": 3800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "감탄사라고", "start_ms": 4250, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 4950, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 5200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있죠", "start_ms": 5400, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "이 표현들은 친구들 사이에서 아주 가볍게 쓸 수 있는 리얼 한국어 표현들입니다",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 130, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "표현들은", "start_ms": 300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "친구들", "start_ms": 800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "사이에서", "start_ms": 1250, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 1850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "가볍게", "start_ms": 2200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "쓸", "start_ms": 2700, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 2950, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 3150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "리얼", "start_ms": 3500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "한국어", "start_ms": 3900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "표현들입니다", "start_ms": 4400, "dur_ms": 750, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "자 그럼 오늘 배운 내용들을 복습하면서 실제로 어떻게 활용할 수 있는지 예문을 살펴볼게요",
"speaker": "speaker_2",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "자", "start_ms": 200, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "그럼", "start_ms": 450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "배운", "start_ms": 1200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "내용들을", "start_ms": 1600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "복습하면서", "start_ms": 2150, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "실제로", "start_ms": 2850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 3350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "활용할", "start_ms": 3800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 4300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있는지", "start_ms": 4500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "예문을", "start_ms": 4950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "살펴볼게요", "start_ms": 5400, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "친구를 만났을 때 민수야 밥 먹었어 라고 물어보면 아주 자연스러운 인사가 됩니다",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "친구를", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "만났을", "start_ms": 600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 1050, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "민수야", "start_ms": 1250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "밥", "start_ms": 1700, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "먹었어", "start_ms": 1900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "라고", "start_ms": 2350, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "물어보면", "start_ms": 2650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 3200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "자연스러운", "start_ms": 3550, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "인사가", "start_ms": 4200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "됩니다", "start_ms": 4650, "dur_ms": 400, "phase_lr_deg": 0.0}
]
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
"transcript": "시험에서 백 점을 맞았다면 옆에 있는 친구가 대박 정말 잘했다 라고 축하해줄 수 있겠죠",
"speaker": "speaker_2",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "시험에서", "start_ms": 180, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "백", "start_ms": 730, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "점을", "start_ms": 980, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "맞았다면", "start_ms": 1280, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "옆에", "start_ms": 1880, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 2280, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "친구가", "start_ms": 2630, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "대박", "start_ms": 3130, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 3530, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "잘했다", "start_ms": 3930, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "라고", "start_ms": 4430, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "축하해줄", "start_ms": 4780, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 5330, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있겠죠", "start_ms": 5530, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "반대로 약속 시간에 늦었을 때 친구가 헐 아직도 집이야 라고 황당해할 수도 있습니다",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "반대로", "start_ms": 120, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "약속", "start_ms": 570, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "시간에", "start_ms": 920, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "늦었을", "start_ms": 1370, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 1870, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "친구가", "start_ms": 2070, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "헐", "start_ms": 2520, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "아직도", "start_ms": 2820, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "집이야", "start_ms": 3320, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "라고", "start_ms": 3770, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "황당해할", "start_ms": 4120, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "수도", "start_ms": 4720, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 5070, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "이처럼 한국어에는 상황에 따라 다양한 뉘앙스를 가진 표현들이 정말 많답니다",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "이처럼", "start_ms": 140, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "한국어에는", "start_ms": 590, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "상황에", "start_ms": 1240, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "따라", "start_ms": 1690, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "다양한", "start_ms": 2090, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "뉘앙스를", "start_ms": 2590, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "가진", "start_ms": 3190, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "표현들이", "start_ms": 3590, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 4140, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "많답니다", "start_ms": 4540, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "잘하고 계세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "잘하고", "start_ms": 80, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "계세요", "start_ms": 450, "dur_ms": 400, "phase_lr_deg": 0.0}
]
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
"transcript": "Great job everyone!",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "Great", "start_ms": 100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "job", "start_ms": 450, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "everyone!", "start_ms": 750, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "오늘 배운 세 가지 표현 밥 먹었어 대박 헐 잊지 말고 꼭 연습해 보세요",
"speaker": "speaker_3",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "배운", "start_ms": 550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "세", "start_ms": 950, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가지", "start_ms": 1150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "표현", "start_ms": 1500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "밥", "start_ms": 1900, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "먹었어", "start_ms": 2150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "대박", "start_ms": 2600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "헐", "start_ms": 3000, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "잊지", "start_ms": 3300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "말고", "start_ms": 3650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "꼭", "start_ms": 4000, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "연습해", "start_ms": 4250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "보세요", "start_ms": 4750, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "꾸준히 연습하다 보면 어느새 한국인처럼 자연스럽게 말하는 여러분의 모습을 발견하게 될 거예요",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "꾸준히", "start_ms": 180, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "연습하다", "start_ms": 680, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "보면", "start_ms": 1280, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "어느새", "start_ms": 1680, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "한국인처럼", "start_ms": 2180, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "자연스럽게", "start_ms": 2880, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "말하는", "start_ms": 3530, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "여러분의", "start_ms": 4030, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "모습을", "start_ms": 4580, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "발견하게", "start_ms": 5080, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "될", "start_ms": 5680, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 5930, "dur_ms": 450, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "오늘 수업은 여기까지입니다 다음 시간에는 더 재미있고 유익한 한국어 표현으로 찾아올게요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 160,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 160, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "수업은", "start_ms": 560, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "여기까지입니다", "start_ms": 1010, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 1860, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "시간에는", "start_ms": 2260, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 2810, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "재미있고", "start_ms": 3060, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "유익한", "start_ms": 3660, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "한국어", "start_ms": 4160, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "표현으로", "start_ms": 4660, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "찾아올게요", "start_ms": 5260, "dur_ms": 600, "phase_lr_deg": 0.0}
]
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
"transcript": "그럼 모두 수고하셨습니다 다음에 또 만나요 안녕히 계세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 140, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "모두", "start_ms": 490, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "수고하셨습니다", "start_ms": 890, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "다음에", "start_ms": 1740, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 2190, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 2440, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 2890, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "계세요", "start_ms": 3340, "dur_ms": 450, "phase_lr_deg": 0.0}
]
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
"transcript": "끝까지 함께 해주셔서 감사합니다 여러분의 한국어 공부를 항상 응원합니다",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "끝까지", "start_ms": 130, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "함께", "start_ms": 630, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "해주셔서", "start_ms": 1030, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 1580, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "여러분의", "start_ms": 2330, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "한국어", "start_ms": 2880, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "공부를", "start_ms": 3380, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "항상", "start_ms": 3880, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "응원합니다", "start_ms": 4330, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "질문이 있다면 댓글로 남겨주세요 최대한 빨리 답변 드릴게요",
"speaker": "speaker_3",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "질문이", "start_ms": 110, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "있다면", "start_ms": 610, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "댓글로", "start_ms": 1110, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "남겨주세요", "start_ms": 1610, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "최대한", "start_ms": 2310, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "빨리", "start_ms": 2810, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "답변", "start_ms": 3210, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "드릴게요", "start_ms": 3610, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "구독과 좋아요 알림 설정까지 잊지 마세요 그럼 안녕",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "구독과", "start_ms": 120, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "좋아요", "start_ms": 620, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "알림", "start_ms": 1070, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "설정까지", "start_ms": 1420, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "잊지", "start_ms": 2020, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "마세요", "start_ms": 2370, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "그럼", "start_ms": 2870, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "안녕", "start_ms": 3220, "dur_ms": 400, "phase_lr_deg": 0.0}
]
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
*Generated by WKAppBot ask gemini — 2026-04-06 07:49:20*
