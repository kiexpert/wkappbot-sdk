# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=29532  time=04:25:10  uptime=10405s  mem=1629MB  system_processes=365"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260404_042418.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 106-756-316-367-130-123-167-130-163-106-163-106-123-160-201-201-607-135-165-605-156-103-135-160-136-106-106-107-502-567-312-316-316-651-103-106-162-167-617-103-103-167-106-715-1_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 135-312-310-203-203-231-312-103-230-503-157-123-321-213-231-106-120-217-136-235-103-132-312-103-312-132-103-312-321-312-312-137-173-276-135-175-231-327-312-156-301-213-103-270-2_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 312-301-312-130-307-316-710-671-132-671-762-675-675-756-756-756-765-675-765-675-163-716-617-362_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 160-136-156-165-312-617-316-130-361-136-671-132-571-163-103-136-103-130-176-516-136-132-134-103 671-132-106-103-167-615-163-107-132-136-631-137-132-132-167-160-130-163-163-103-1_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 163-103-103-136-612-657-103-136-103-103-130-137-136-136-315-136-163_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 673-627-263-102-134-143-132-754-754-756-756-175-103-167-136-175-175-135-137-123-132-103-132-143 675-672-607-613 236-237-273-274-271-261-236-571-657-605-265-627-652-267-263-236-1_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 301-130-137-103-137-301-317-103-317-103-312-103-137-310-317-173-137-731-137-372-137-715-103-307-130-317 315-105-135-137-312-137-103-137-130-135-123-103-137-107-307-310-130-130-1_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 163-163-301-713-706-103-103-136-163-317-312-561-167-163-316-136-167-132-136-632-136-617-672-675-567-576-567-657-675-672-206-206-276-267-572-572-576-312-326-657-276-672-103-712-6_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 135-316-136-136-136-632-120-165-103-103-312-132-361-136-132-136-136-136-317-103-103-765-607-671-615-316-625-621-136-617-160-657-627-631-672-621-620-312-103-317-170-107-267_V.mp3)
  Segment 10: 14184ms ~ 21816ms (file: 572-752-726-723-627-176-723-276-237-623-637-671-106-107-175-751-754-237-103-273-276-276-107-571-103-276-267-726-762-762-312-103-103-617-106-136-675-652-657-723-312-316-765-276-1_W.mp3)
  Segment 11: 22816ms ~ 23392ms (file: 230-203-302-301-103-130-103-130-132-132-136-132-316-317-163-136-136-134-103-132-103-103-103-103-103-103-137-132-153-153-165-176-163-163-160-123-216-162-132 132-103 132-103 621-6_V.mp3)
  Segment 12: 24392ms ~ 24968ms (file: 326-635-361-612-264-173-134-132-574-753-175-675-675-672-670 236-265-756-103-137-673-726-672-726-267-203-671-607-610-672-167-103-132-751-571-173-176-316-675-670-762-103-136-106-1_W.mp3)
  Segment 13: 25968ms ~ 26544ms (file: 136-103-621-267-623-362-632-263-236-260-231-132-123-517-510-216-627-627-263-263-621-263-621-261-260-327-302-321-267-163-301-263-206-263-312-312-203-260-236-261-627-260-607-260-6_W.mp3)
  Segment 14: 27544ms ~ 28120ms (file: 316-136-163-136-106-103-172-136-132-163-165-167-132-154-143-132-167-175-145-713-312-314-132-315-132-372-132-174-164-132-136-175-127-135-145-132-132-127-127-132-134-314-132-173-1_V.mp3)
  Segment 15: 29120ms ~ 29696ms (file: 570-176-570-756-756-756-612-632-137-173-136-617-617-135-103-103-316-165-127-103-637-514-617-613-316-130-137 136-321-156-162-136-130-675-673-610-162-621-162-602-621-675-136-130-3_V.mp3)
  Segment 16: 30696ms ~ 31272ms (file: 317-137-317-361 675 371-376-376-736-736-716 310-312-136-130-627-176-160-610-610-163-165-160-163-167-371-312-253-321-315-315-103-130-326-321-136-317-736-763-367-372-376-367-367-3_W.mp3)
  Segment 17: 32272ms ~ 32848ms (file: 527-527-521-253-756-576-523-635-635-652-652-623-610-610-625-162-670-756-672-760-726-672-765-760-761-671-675-762-176-765-672-176-756-725 516-561-567-571-501-675-657-650-567-560-6_V.mp3)
  Segment 18: 33848ms ~ 34424ms (file: 756-756-567-675-765-675-675-657-756-756-765-675-765-756-756-754-657-675-576-567-576-126-621 371-312_W.mp3)
  Segment 19: 35424ms ~ 36000ms (file: 365-756-132-213-206-126-632-236-316-132-617-163-362-621-236-326-126-213-215-213-621-162-162 567-513-352-513_W.mp3)
  Segment 20: 37000ms ~ 37576ms (file: 513-531-315-317-316-321-327-326-231-132-312-123-153-215-261-126-123-312-316-361-231-123-213-213-216-321-126-261-321-132-126-163-612-125-321-321-312-312-127-176-167-126-162-176-3_W.mp3)
  Segment 21: 38576ms ~ 39152ms (file: 316-123-136-162-103-713-761-130-617-137-165-163-103-163-576-103-176-136-165-137-317-123-756-632-621-631-163-617-132-123-127-310-167-130-617-617-103-625-367-215-160-653-316-613-1_V.mp3)
  Segment 22: 40152ms ~ 40728ms (file: 167-627-671-627-167-671-612-617-167-617-167-176-617-167-612-167-162-167-617-671-617-617-617-167-627-671-167-617-167-612-162-167-612-627-612-162-612-617-617-617-617-167-162-617-6_W.mp3)
  Segment 23: 41728ms ~ 42304ms (file: 652-365-362-625-256-256-250-510-532-653-635-316-315-537-574-357-752-235-236-571-527-654-564-231-261-265-257-354-354-351-315-316-357-315-261-256-356-534-326-362-365-316-312-136-1_W.mp3)
  Segment 24: 43304ms ~ 43880ms (file: 621-162-163-631-756-627-621-236-261-612-562-576-567-657-567-357-316-615-623-621-162-712-136-130-621-126-236-623-132-632-612-632-263-263-627-612-675-236-136-316-137-635-163-653-6_W.mp3)
  Segment 25: 44880ms ~ 52044ms (file: 627-216-625-316-312-316-230-312-702-261-623-312-321-132-765-765-203-310-123-123-715-672-263-210-632-120-263-123-312-231-263-637-136-103-301-162-135-153-675-726-207-312-126-167-6_W.mp3)
  Segment 26: 53044ms ~ 53620ms (file: 126-623-236-312-216-263-126-621-625-507-501-312-310-103-103-103-132-657-673-623-231-312-123-143-132-103-103-107-136-627-301-320-263-320-230-206-103-765-762-657-610-106-617-302-3_V.mp3)
  Segment 27: 54620ms ~ 55196ms (file: 657-567-657-657-576-576-657-726-267-672-621-627-627-623-326-362-612-621-562-657-136-123-576-765-103-132-716-103-130-130-136-231-261 560-563 351-132-103-103-672-267-267-627-627-6_W.mp3)
  Segment 28: 56196ms ~ 56772ms (file: 102-106-120-103-123-102-123-162-126-301-320-230-263-601-162-765-137-136-103-162-632-312-102-103-320-123-321-231-203-320-326-162-162-103-316-312-302-301-132-130-310-312-715-370-5_V.mp3)
  Segment 29: 57772ms ~ 58348ms (file: 576-576-576-576-756-576-576-576-657-576-567 605-576-567-576-756-576-567-576-576-576-576-576-576-576-576-576-576 657-567-576-567-567-576-576-576-576-576-576-576-576-576-576-576-5_W.mp3)
  Segment 30: 59348ms ~ 64568ms (file: 120-260-261-261-267-263-261-265-231-263-102-570-657-260-625-261-625-654-562-102-210-751-102-132-120-103-320-231-230-130-124-527-120-231-230-263-236-263-123-201-623-265-213-621-2_W.mp3)
  Segment 31: 65568ms ~ 66144ms (file: 175-157-751-267-716-756-631-162-127-672-623-756-637-126-672-672-673-637-162-631-375-316-327-623-620-602-627-601-361-103-102-216-203-206-126-756-206-163-163-610-627-630-326-602-6_W.mp3)
  Segment 32: 67144ms ~ 67720ms (file: 176-756-123-130-130-172-130-103-132-136-132-135-301-310-103-130-103-103-312-567-106-163-106-167-163-106-163-601-130-163-310-216-175-160-136-106-312-103-136-135-136-130-103-136 2_V.mp3)
  Segment 33: 68720ms ~ 69296ms (file: 316-260-126-103-136-316-130-130-617-103-103-136-260-157-132-160-103-617-106-103-136-361-651-617 612-172-165-126_V.mp3)
  Segment 34: 70296ms ~ 70872ms (file: 756-675-756-657-761-765-675-765-672-657-267-576-756-576-751-756-675-756-756-716-756-756-576-756-576-576-576-756-672-576-765-675-547-756-675-576-574-576-574-576-567-576-576-576-5_W.mp3)
  Segment 35: 71872ms ~ 72448ms (file: 756-361-317-312-326-267-362-237-637-674-736-371-364-163-607-605-602-263-627-623-362-163-362-762-261-602-765-765-765-675-135-371-527-312-362-213-162-706-317-316-103-312-301-163-1_V.mp3)
  Segment 36: 73448ms ~ 74024ms (file: 130-265-260-206-256-275-276-275-726-276-267-726-762-627-762_W.mp3)
  Segment 37: 75024ms ~ 75600ms (file: 136-756-613-102-671-106-103-136-517-623-132_V.mp3)
  Segment 38: 76600ms ~ 77176ms (file: 137-103-103-512-756-325-132-167-615-127-765-761-716-316-173-761-103-756-736-716-765-160-176-307-173-175-756-716-130-162-160-613-163-130-301-135-136-136-163-132-103-263-206-326-2_V.mp3)
  Segment 39: 78176ms ~ 78752ms (file: 715-310-130-310-310-317-103-317-137-135-317-371-132-102-317-317-157-751-103-310-752-715 153-137-310-317-301-132-317-175-103-310-317-132-371-317-315-713-371-173-172-130-375-103-3_V.mp3)
  Segment 40: 79752ms ~ 80328ms (file: 756-756-237-241-461-106-167-163-716-173-673-621-312-761-623-213-261-213-231-675-132-670-671-637-163-675-657-657 103-763-621-163-167-124-574-570-560-567-132-316-103-167-176-136-6_V.mp3)
  Segment 41: 81328ms ~ 91984ms (file: 670-756-261-127-102-716-176-132-103-326-263-106-103-160-162-621-160-216-756-725-102-162-762-167-261-762-762-671-316-613-675-672-567-260-762-176-231-601-132-316-716-361-361-361-7_W.mp3)
  Segment 42: 92984ms ~ 93560ms (file: 136-103-130-156-613-316-135-516-163-261-136-130-103-365-103-136-163-136-103-160-163-130-132-136-137-163-165-601-130-136-103-103-136-125-160-621-162-165-132-103-163-103-163-315-1_V.mp3)
  Segment 43: 94560ms ~ 95136ms (file: 162-102-165-167-612-103-546-671-162-316-102-163-367-102-526-675-163-671-103-165-632-123-216-132-267-632-621-673-106-620-326-631-361-106-167-621-612-567-625-265-623-103-162-625-6_W.mp3)
  Segment 44: 96136ms ~ 96712ms (file: 236 326-236-136-632-362-326-231 236-263-320-657-657-675-657-562-627-162-123-610-162-672-627-627-632-260-260-623-675-627-673-627-627-263-236-263-301-203-236-236-631-613-362-236-2_W.mp3)
  Segment 45: 97712ms ~ 98288ms (file: 765-673-567-607-165-175-621-652-610-567-573-613-615-105-156-103-302-103-301-230-163-206-235-216-123-236-306-321-263-167-265-265-627-657-562-625-652-256-625-312-263-325-235-325-2_W.mp3)
  Segment 46: 99288ms ~ 105480ms (file: 207-267-302-132-123-170-102-671-302-237-316-362-312-316-361-230-756-165-176-106-156-751-167-673-756-675-752-576-765-675-765-657-265-275-162-652-562-653-231-263-261-210-231-132-6_W.mp3)
  Segment 47: 106480ms ~ 107056ms (file: 103-637-137-765-267-260-617-167-360-615-163-123-567-162-103-130-675-670-657-765-167-167-103-576-130-623-623-612-670-612-316-567-163-137-562-310-673-132-137-130-132-167-103-136-3_V.mp3)
  Segment 48: 108056ms ~ 108632ms (file: 576-173-176-671-176-107-602-672-762-716-261-106-103-163-126-206-267-263-216-126-106-761-160-103-263-625-627-106-206-206-263-126-726-203-261-136-132-127-162-321-132-326-201-136-1_V.mp3)
  Segment 49: 109632ms ~ 110208ms (file: 675-675-657-156-576-765 132 256-627 273-672 320-541-136-123-153-123-132-130-163-564-576-136 623-236 160-123_W.mp3)
  Segment 50: 111208ms ~ 111784ms (file: 627-627-267-261-627-627-672-267-562-672-263-621-267-237-547-576-756-576-756-576-576-576-576 675-675-672-567-657-576-657-527-672-216-675-675-576-602-621-621-627-621-126-261-672-6_W.mp3)
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
> **Time**: 2026-04-04 04:26:55
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "안녕하세요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "안녕", "start_ms": 40, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "하세요", "start_ms": 260, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "오늘 수업은 여기서 마칠게요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "수업은", "start_ms": 180, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "여기서", "start_ms": 380, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "마칠게요", "start_ms": 520, "dur_ms": 56, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 4,
"transcript": "질문 있으신 분 계신가요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "질문", "start_ms": 45, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "있으신", "start_ms": 225, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "분", "start_ms": 385, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "계신가요?", "start_ms": 435, "dur_ms": 141, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "저요!",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "저요!", "start_ms": 20, "dur_ms": 556, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "이 문법이 이해가 안 가요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 35, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "문법이", "start_ms": 115, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "이해가", "start_ms": 295, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 445, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "가요", "start_ms": 485, "dur_ms": 91, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "다시 한번 설명해 주시겠어요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "다시", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "한번", "start_ms": 160, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "설명해", "start_ms": 300, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "주시겠어요?", "start_ms": 420, "dur_ms": 156, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "네, 물론이죠. 천천히 다시 볼게요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "물론이죠.", "start_ms": 110, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "천천히", "start_ms": 290, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 440, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "볼게요.", "start_ms": 520, "dur_ms": 56, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "이 부분은 격식체 문장이에요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "부분은", "start_ms": 100, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "격식체", "start_ms": 240, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "문장이에요", "start_ms": 420, "dur_ms": 156, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "자, 화면을 보세요. 이렇게 변화합니다.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "자,", "start_ms": 60, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "화면을", "start_ms": 100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "보세요.", "start_ms": 300, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "이제 이해가 되나요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "이해가", "start_ms": 155, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "되나요?", "start_ms": 335, "dur_ms": 241, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "아, 그렇군요! 감사합니다 선생님.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "아,", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "그렇군요!", "start_ms": 100, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 280, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "선생님.", "start_ms": 480, "dur_ms": 96, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "천만에요. 다른 질문 있나요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "천만에요.", "start_ms": 30, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "다른", "start_ms": 210, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "질문", "start_ms": 330, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있나요?", "start_ms": 430, "dur_ms": 146, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "아니요, 없습니다. 수고하셨습니다.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "아니요,", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "없습니다.", "start_ms": 160, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "수고하셨습니다.", "start_ms": 300, "dur_ms": 276, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "네, 다음 주에 봐요!",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 25, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 125, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "주에", "start_ms": 245, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "봐요!", "start_ms": 345, "dur_ms": 231, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "See you next time!",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 130, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "next", "start_ms": 210, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "time!", "start_ms": 350, "dur_ms": 226, "phase_lr_deg": 0.0}
]
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
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 19,
"transcript": "자, 이제 점심 먹으러 갑시다",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "자,", "start_ms": 50, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 110, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "점심", "start_ms": 210, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "먹으러", "start_ms": 350, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "갑시다", "start_ms": 470, "dur_ms": 106, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "배가 너무 고프네요. 근처에 식당 있나요?",
"speaker": "speaker_4",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "배가", "start_ms": 60, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 160, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "고프네요.", "start_ms": 280, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "비빔밥 집이 유명해요 그리로 가요",
"speaker": "speaker_3",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "비빔밥", "start_ms": 45, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "집이", "start_ms": 225, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "유명해요", "start_ms": 325, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "그리로", "start_ms": 465, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "가요", "start_ms": 565, "dur_ms": 11, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "좋아요, 비빔밥 맛있겠어요!",
"speaker": "speaker_4",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "좋아요,", "start_ms": 35, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "비빔밥", "start_ms": 185, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "맛있겠어요!", "start_ms": 385, "dur_ms": 191, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "(mixed: speech over music)",
"speaker": "speaker_3",
"confidence": 0.85,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "mixed",
"words": [
{"word": "출발합시다", "start_ms": 40, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "날씨가 정말 화창하네요",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "날씨가", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 220, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "화창하네요", "start_ms": 340, "dur_ms": 236, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "이런 날씨에는 소풍 가고 싶어요",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "이런", "start_ms": 55, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "날씨에는", "start_ms": 175, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "소풍", "start_ms": 335, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "동감입니다. 다음에 같이 가요.",
"speaker": "speaker_4",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "동감입니다.", "start_ms": 40, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "다음에", "start_ms": 280, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 430, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "가요.", "start_ms": 510, "dur_ms": 66, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "네, 꼭 그래요. 식당 다 왔네요.",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 50, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "꼭", "start_ms": 110, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "그래요.", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "식당", "start_ms": 270, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 410, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "왔네요.", "start_ms": 470, "dur_ms": 106, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "여기가 입구인가요?",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "여기가", "start_ms": 30, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "입구인가요?", "start_ms": 210, "dur_ms": 346, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 30,
"transcript": "어서 오세요! 몇 분이세요?",
"speaker": "speaker_5",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "어서", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "오세요!", "start_ms": 160, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "몇", "start_ms": 340, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "분이세요?", "start_ms": 400, "dur_ms": 176, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "두 명입니다. 조용한 자리 있나요?",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "두", "start_ms": 50, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "명입니다.", "start_ms": 110, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "조용한", "start_ms": 290, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "자리", "start_ms": 440, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "있나요?", "start_ms": 520, "dur_ms": 56, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "네, 이쪽 창가 자리로 안내해 드릴게요",
"speaker": "speaker_5",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "이쪽", "start_ms": 100, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "창가", "start_ms": 220, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "자리로", "start_ms": 320, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "경치가 참 좋네요. 메뉴판 좀 주시겠어요?",
"speaker": "speaker_4",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "경치가", "start_ms": 60, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "참", "start_ms": 240, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "좋네요.", "start_ms": 300, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 35,
"transcript": "주문하시겠어요? 비빔밥 두 개 맞나요?",
"speaker": "speaker_5",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "주문하시겠어요?", "start_ms": 70, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "비빔밥", "start_ms": 370, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "두", "start_ms": 520, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "개", "start_ms": 560, "dur_ms": 16, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "네, 맞습니다. 안 맵게 해주세요.",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 45, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "맞습니다.", "start_ms": 125, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 305, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "맵게", "start_ms": 365, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "해주세요.", "start_ms": 485, "dur_ms": 91, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "알겠습니다",
"speaker": "speaker_5",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "알겠습니다", "start_ms": 20, "dur_ms": 556, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "음식이 정말 빨리 나오네요. 비주얼이 최고예요!",
"speaker": "speaker_4",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "음식이", "start_ms": 55, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 235, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "빨리", "start_ms": 355, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "나오네요.", "start_ms": 475, "dur_ms": 101, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "그러게요. 맛있게 드세요!",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "그러게요.", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "맛있게", "start_ms": 220, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "드세요!", "start_ms": 380, "dur_ms": 196, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "네, 잘 먹겠습니다! 음, 고소한 냄새!",
"speaker": "speaker_4",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 65, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 145, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "먹겠습니다!", "start_ms": 205, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "참기름 향이 정말 좋죠? 한 입 드셔보세요.",
"speaker": "speaker_3",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "참기름", "start_ms": 80, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "향이", "start_ms": 260, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 380, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "좋죠?", "start_ms": 500, "dur_ms": 76, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "와, 진짜 꿀맛이에요! 최근에 먹은 것 중 최고예요.",
"speaker": "speaker_4",
"confidence": 0.91,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "와,", "start_ms": 90, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 170, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "꿀맛이에요!", "start_ms": 290, "dur_ms": 286, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "마음에 든다니 다행이네요. 여기 김치도 맛있어요.",
"speaker": "speaker_3",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "마음에", "start_ms": 55, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "든다니", "start_ms": 195, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "다행이네요.", "start_ms": 315, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "김치가 아삭아삭하고 시원하네요. 조합이 아주 좋아요.",
"speaker": "speaker_4",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "김치가", "start_ms": 65, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "아삭아삭하고", "start_ms": 225, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "시원하네요.", "start_ms": 465, "dur_ms": 111, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "더 필요한 건 없으세요? 물 좀 더 드릴까요?",
"speaker": "speaker_5",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "더", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "필요한", "start_ms": 100, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "건", "start_ms": 220, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "없으세요?", "start_ms": 260, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "아니요, 괜찮습니다. 충분해요. 잘 먹었습니다!",
"speaker": "speaker_3",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "아니요,", "start_ms": 60, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "괜찮습니다.", "start_ms": 180, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "충분해요.", "start_ms": 320, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "벌써 다 드셨네요. 계산은 제가 할게요.",
"speaker": "speaker_3",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "벌써", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 170, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "드셨네요.", "start_ms": 230, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "어머, 정말요? 그럼 다음엔 제가 대접할게요.",
"speaker": "speaker_4",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "어머,", "start_ms": 70, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "정말요?", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "그럼", "start_ms": 270, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "다음엔", "start_ms": 350, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "그럴까요? 자, 이제 차 한 잔 하러 갈까요?",
"speaker": "speaker_3",
"confidence": 0.91,
"language": "ko",
"silence_before_ms": 85,
"audio_type": "speech",
"words": [
{"word": "그럴까요?", "start_ms": 85, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "자,", "start_ms": 265, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 325, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-04 04:26:55*
