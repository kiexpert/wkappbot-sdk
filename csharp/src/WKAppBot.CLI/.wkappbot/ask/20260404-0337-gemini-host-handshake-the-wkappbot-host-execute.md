# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=25708  time=03:36:33  uptime=9078s  mem=731MB  system_processes=366"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260404_033240.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 9072ms (file: 231-106-132-103-130-120-251-201-321-120-102-123-123-103-230-527-132-107-167-103-310-574-230-320-216-206-231-123-137-235-213-130-103-103-571-275-237-130-102-103-321-213-103-310-1_V.mp3)
  Segment 2: 10072ms ~ 10648ms (file: 631-136-607-167-601-612-126-261-621-260-216-126-613-261-632-103-136-610-267-163-213-672-761-103-130-167-310-162-237-175-126-103-315-517-631-132-103-130-536-576-136 136-165-132-1_V.mp3)
  Segment 3: 11648ms ~ 12224ms (file: 132-362-165-167-132-175-675 627-627-263-627-672-621-673-310-130-103-103-103-675-617-602-607-620-620-602-620-612-167-261-260-267-627-623-627-163-310-132-316-312-120-602-607-670-6_V.mp3)
  Segment 4: 13224ms ~ 13800ms (file: 675-672-712-762-765-765-237-726-765-726-237-265-637-621-231-126-261-106-630-621-263-627-103-103-136-173-137-136-216-612 317-316-316_W.mp3)
  Segment 5: 14800ms ~ 15376ms (file: 567-567-567 276-267-627 765-756-675-671-267-267-627-621-627-627-673_W.mp3)
  Segment 6: 16376ms ~ 16952ms (file: 153-132-312-236-312-136-236-103-162-506-512-126-362-620-623-360-263-263-263-623-236-620-675-632-623-132-310-312-132-235-267-267-326-263-136-312-321-612-621-350-350-260-312-263-6_W.mp3)
  Segment 7: 17952ms ~ 18528ms (file: 763-675-672 312-627-267-261-206-670-760-670-670-670-760-675-627-672-672-627-673-627-623-762-276-672-670-675-627-267-672-762-763-672-672-672-267-263-621-236-136-123-762-361-326-1_W.mp3)
  Segment 8: 19528ms ~ 20104ms (file: 617-163-106-163-610-163-172-175-615-613-762-160-167-623-126-160-106-103-163-126-165-175-157-621-162-612-136-106-621-623-617-103-617-103-162-126-321-163-106-162-103-126-615-136-5_V.mp3)
  Segment 9: 21104ms ~ 21680ms (file: 312-312-312-310-316-231-230-321-512-562-263-623-265-562-265-267-261-316-231-207-253-657-563-275-635-367-132-365-632-367-365-672-173-135-306-302-673-306-103-316-307-302-361-326-3_W.mp3)
  Segment 10: 22680ms ~ 23256ms (file: 123-103-137-137-130-130-103-132-216-301-310-130-103-132-102-103-160-301-132-103-132-103-326-312-103-621-261-261-326-523-567-567-156-123-576-571-751-217-102-261 627-621-162-126-1_V.mp3)
  Segment 11: 24256ms ~ 24832ms (file: 103-536-136-132-672-136-132-621-630-106-567-175-136-103-172-716-316-106-103-531-176 103-132-103-103-163-103-132-315-261-623-157-652-761-571-105-135-235-302-320-230-230-325-235_V.mp3)
  Segment 12: 25832ms ~ 35300ms (file: 213-321-103-317-137-756-103-165-260-657-574-230-276-257-105-137-260-231-271-231-175-715-103-320-102-103-213-213-276-201-321-765-765-267-601-132-103-310-312-276-236-310-132-120-2_W.mp3)
  Segment 13: 36300ms ~ 36876ms (file: 371-163-136-327-123-136-176-167-136-613-612-103-103-576-610-132-276-317-160-136-130-613-631-106-637-103-136-504-132-132-103-130-136-263-326-126-136-561-312-105-625-716-132-367-1_V.mp3)
  Segment 14: 37876ms ~ 46516ms (file: 675-103-574-103-102-103-751-132-723-173-132-320-230-302-715-103-320-273-132-213-132-162-231-756-327-720-750-754-357-754-756-126-107-320-301-106-103-230-230-672-103-765-756-103-1_W.mp3)
  Segment 15: 47516ms ~ 48092ms (file: 623-623-162-306-310-613-136-137-671-712-267-123-261-312-673-167-152-617-612-135-617-632-765-756-765-761-617-627-672-357-365-327-321_W.mp3)
  Segment 16: 49092ms ~ 49668ms (file: 623-261-263-263-263-623-623-627-627-263-627-627-623-627-627-231-261-621-612-620-621-236-326-612-165-631-613-163-613-612-612-162-162-162-163-361-236-721-716-167-102-170-627-627-3_W.mp3)
  Segment 17: 50668ms ~ 51244ms (file: 571-570-301-671-612-613-123-123-265-261-621-267-261-261-267-263 760-107 261-621-162-627-567-576-675-675-263-627-623-712-763-627-627-516-576-756-756-362-265-236-261-136-126-172-7_W.mp3)
  Segment 18: 52244ms ~ 52820ms (file: 136-162-103-136-103-135-564-765-106-130-136-137-105-136-316-157-167-136-172-103-576-136-103-135-107-136-617-765-215-176_V.mp3)
  Segment 19: 53820ms ~ 58104ms (file: 723-756-756-756-705-571-623-756-751-326-236-301-123-672-312-176-761-765-765-760-672-657-763-672-607-672-362-607-602-627-602-602-627-260-267-267-260-267-623-132-327-752-267-267-6_W.mp3)
  Segment 20: 59104ms ~ 59680ms (file: 310-264-201-312-162-156-275-217-167-126-652-603-657-137-156-617-617-126-507-156-102-326-126 132-132-376-726-263-236-320-320-213-301-123-107-107-267-267-267-765-572-576-576-570-5_W.mp3)
  Segment 21: 60680ms ~ 61256ms (file: 265-672-617-632-123-725-132-203-132-635-312-537-753-576-765-672-735 613-610-613-631-631-631-631-635-317-371-735-735-375-756-375-763-736-570-623-623-236-361-106-103-132-130-130-3_W.mp3)
  Segment 22: 62256ms ~ 62832ms (file: 615-564-261-167 136-162-521-316-136-103-263-261-231_V.mp3)
  Segment 23: 63832ms ~ 64408ms (file: 160-637-160-160-261-106-210-203-321-260-261-263-130-120-301-203-201-123-210-623-216-162-301-632-603-316-132-632-612-136-162-103-630-130-632-607-620-621-613-162-162-106-102-216-6_W.mp3)
  Segment 24: 65408ms ~ 65984ms (file: 670-167-263-102-163-263-261-126-675-617-167-267-267-230-132-201-301-362-103-360-326-620-326-103-130-103-321-261-236-261-657-621-320-103-103-132-625-623-657-126-612-617-106-106-1_V.mp3)
  Segment 25: 66984ms ~ 67560ms (file: 627-625-671-132-106-672-675-261 627-612-623-756 136-261-162 627 167-361-612-136-261-261-623 672-675-627-671-670-637-762-672-756-612-123-160-102-621-103-102-102-130-136-326-321-3_W.mp3)
  Segment 26: 68560ms ~ 69136ms (file: 327-326-357-537-537-752-732-237-267-267-263-276-267-267-267-267-276-276-756-756-765-756-573-537-253-236-136-162-261-267-574-573-751-567-365-326-573-357-371-567-765-672-627-627-2_W.mp3)
  Segment 27: 70136ms ~ 70712ms (file: 631-173-107-126-106-102-132-103-546-132-620-132-162-612-106-317-107-176-316-136-126-126 167-103-316-715-163-365-613-126-160-103-130-106-316-103-635-136-130-103-130-130-103-103-1_V.mp3)
  Segment 28: 71712ms ~ 72288ms (file: 150-132-601-126-362-201-612-620-160-236-126-102-120-126-621-612-167-136-657-657-256-206-657-675-675-126-173-607-607-605-672-670-675-637-607-675-675-765-675-675-670-627-627-672-6_W.mp3)
  Segment 29: 73288ms ~ 73864ms (file: 310-267-260-302-371-231-213-206-321-132-713-302-327-127-127-103-361-231-132-103-132-312-132-123-132-102-507-105-137-571-570-103-103-301-231-132-132-132-310-103-132-163-316-136-3_V.mp3)
  Segment 30: 74864ms ~ 75440ms (file: 130-130-627-627-627-613-620-263-216-301_W.mp3)
  Segment 31: 76440ms ~ 77016ms (file: 162-162-623-627-617-621-623-621-632-631-612-627-672-617-613-627-632 617-613-621 617-617-163 617-612-671-632-613-627-627-627-612-617-672-627-672-672-672-627-627-627-627-627-627-6_W.mp3)
  Segment 32: 78016ms ~ 78592ms (file: 136-132-105-160-103-175-315-351-136-103-103-751-130-132-103-130-126-201-126-175-176-576-723-157-752-572-576-756-576-156-256-157-531-502-217-320-162-126_V.mp3)
  Segment 33: 79592ms ~ 89924ms (file: 106-136-706-620-623-263-723-752-675-106-361-360-213-621-136-107-165-705-275-103-310-321-320-276-103-132-103-317-175-502-713-576-172-136-760-236-310-125-236-261-316-326-163-571-2_V.mp3)
  Segment 34: 90924ms ~ 94056ms (file: 162-675-652 172-127-217-621-236-126-261-673-762-627-675-627-657-675-256-572-637-612-762-671-763-627-267-627-276-267-267-726-726-675-276-627-235-652-632-627-761-672-672-672-613-6_W.mp3)
  Segment 35: 95056ms ~ 95632ms (file: 167-213-213-326-153-361-236 371-263-361-124-716-651 627-607-620-163-136-103-106 451-124-132-237-762-763-362-627-513-314-653-621-673-672-756 753-756-756-765-765-765-765-765_V.mp3)
  Segment 36: 96632ms ~ 97208ms (file: 126-623-657-276-652-167-576-756-765-752-627-672-657-675-157-137-102-103-261-627-762-672-756-672-621-627-675-673-621-162-613-631-130-312-103-213-631-216-126-126-126-162-316-612-1_W.mp3)
  Segment 37: 98208ms ~ 98784ms (file: 726-267-260-230-236-306-360-607-136-102-127-163-132-162 673-671-632-613-231 160-120-123-612-752-716-172-126-612-132-632-316-613-310-635-621-320-321-132-320-213-312-123-213-231-2_W.mp3)
  Segment 38: 99784ms ~ 100360ms (file: 760-670-671-162-617-167-612-176 132_V.mp3)
  Segment 39: 101360ms ~ 101936ms (file: 251-156-153-127-137-132-137-671-267-267-267-627-627-267-627-627-621-627-267-627-263-267-263-236-623-267-263-236-267-267-267-267-267-263-263-621-263-263-261-236-263-261-216-216-1_W.mp3)
  Segment 40: 102936ms ~ 103512ms (file: 621-261-267-162-621-671-671-675-765-761-367-637-723-756-725-167-163-675-716-716-103-173-130-153-763-126-356-716-176-173-765-136-163-612-360-173-301-167-103-136-103-316-137-317-6_W.mp3)
  Segment 41: 104512ms ~ 105088ms (file: 612-617-716-362-123-162-123-126-132-130-103-201-216-261-130-601-623-362-236-230-302-320-326-231-752-756-570-576-576-576-572-537-756-270-217-321-162-103-123-132-231-302-236-265-6_W.mp3)
  Segment 42: 106088ms ~ 106664ms (file: 354-130-135-130-561-163-571-132-567-213-162-163-136-136-316-163-103-137-136-601-136-160-103-136-136-103-136-163-106-607-165-671-267-153-576-167-176 627_V.mp3)
  Segment 43: 107664ms ~ 108240ms (file: 576-326-176-521-570-572-572-571-751-576-527-675-517-562-276-762-751-751-236-271-257-572-257-276-521-153-357-312-361-365-362-635-365-261-326-571-571-573-573-573-574-574-567-576-5_W.mp3)
  Segment 44: 109240ms ~ 109816ms (file: 765 325-276-526-256-537-576-576-256-620-765-726-365-526-635-627-627-576-657-657-726-756-524-653-325-760-671-612-163-651-276-617-673-672-637-675-602-617-627-627-627-627-620-625-3_W.mp3)
  Segment 45: 110816ms ~ 111392ms (file: 571-615-517-560-126-162-327-367-310-231-567-537-261-613-126-356-632-267-153-635-237-562-163-567-317-162-617-637-302-572-567-276-153-312-163-637-103-621-367-376-531-673-167-672-1_W.mp3)
  Segment 46: 112392ms ~ 112968ms (file: 302-132-301-310-130-132-602-617-620-623-106-623-123-501-501-531-165-130-301-130-671-316-103-136-106-153-215-625-261-206-261-520-752-756-136-316-106-362-761-701-576-506-103-103-1_V.mp3)
  Segment 47: 113968ms ~ 114544ms (file: 326-312 103-756-715-731-137-132-132-132-132-301-163-163-362-162-362-631-765-715-162-321-326-362-326-361-317-567-756-137-673-715-756-613-362-623-621-637-321-631-562-162-617-271-2_W.mp3)
  Segment 48: 115544ms ~ 116120ms (file: 260-172-162 103-107-167-163-163-172-106-103-312-130-136-136-103-106-136-160-163-632-726-316-136-103-136-617-716-163-321-263-136-160-613-617-163-326-623-167-160-136-132-136-103-6_V.mp3)
  Segment 49: 117120ms ~ 117696ms (file: 162-126-627-617-163-106-163-165-617-162-102-106-671-160-267-627-132-217-672-126-206-132-210-213-602-761-762-765-765-765-765-675 106-670-671-671-706 657-567-573-567-576-756-675-6_W.mp3)
  Segment 50: 118696ms ~ 119272ms (file: 627-670-672-670-607-620-602-621-632-612-163-312-631-362-612-362-360-670-620-620-620-206-326-612-610-106-136-316-163-301-130-301-306-132-312-312-310-317-310-136-631-320-132-451-1_W.mp3)
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
> **Time**: 2026-04-04 03:37:43
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "안녕하세요 반갑습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 50, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 380, "dur_ms": 196, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "오늘 날씨가 정말 좋네요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 170, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 330, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 450, "dur_ms": 126, "phase_lr_deg": 0.0}
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
"transcript": "How are you doing?",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "How", "start_ms": 60, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "are", "start_ms": 170, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 260, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "doing?", "start_ms": 360, "dur_ms": 216, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "커피 한 잔 하실래요?",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "커피", "start_ms": 30, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "잔", "start_ms": 250, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "하실래요?", "start_ms": 350, "dur_ms": 226, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "Great idea, let's go.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "Great", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "idea,", "start_ms": 165, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "let's", "start_ms": 315, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "go.", "start_ms": 425, "dur_ms": 151, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "Where should we go?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "Where", "start_ms": 55, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "should", "start_ms": 185, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "we", "start_ms": 295, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "go?", "start_ms": 385, "dur_ms": 191, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "I know a good place.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "know", "start_ms": 110, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 220, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "good", "start_ms": 270, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "place.", "start_ms": 390, "dur_ms": 186, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "그곳은 어디에 있나요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "그곳은", "start_ms": 65, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "어디에", "start_ms": 225, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "있나요?", "start_ms": 375, "dur_ms": 201, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "근처에 아주 유명한 카페가 있어요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "근처에", "start_ms": 120, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 380, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "유명한", "start_ms": 570, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "카페가", "start_ms": 860, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 1090, "dur_ms": 310, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "Sounds perfect to me.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Sounds", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "perfect", "start_ms": 200, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 390, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "me.", "start_ms": 460, "dur_ms": 116, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "그럼 지금 바로 가요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 45, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 175, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 295, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "가요", "start_ms": 405, "dur_ms": 171, "phase_lr_deg": 0.0}
]
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
"transcript": "What are you going to order?",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "What", "start_ms": 55, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "are", "start_ms": 165, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 235, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "going", "start_ms": 315, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 425, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "order?", "start_ms": 485, "dur_ms": 91, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "저는 아이스 아메리카노요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 60, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "아이스", "start_ms": 180, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "아메리카노요", "start_ms": 330, "dur_ms": 246, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "좋은 선택이에요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "선택이에요", "start_ms": 200, "dur_ms": 376, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "I'll have a latte then.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "I'll", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "have", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 250, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "latte", "start_ms": 300, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "then.", "start_ms": 450, "dur_ms": 102, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "라떼도 맛있겠네요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "라떼도", "start_ms": 65, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "맛있겠네요", "start_ms": 255, "dur_ms": 321, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "여기 분위기가 참 좋죠?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "여기", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "분위기가", "start_ms": 165, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "참", "start_ms": 325, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "좋죠?", "start_ms": 415, "dur_ms": 161, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "Yes, I really like it here.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Yes,", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 170, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "really", "start_ms": 240, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "like", "start_ms": 360, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "it", "start_ms": 460, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "here.", "start_ms": 530, "dur_ms": 144, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 22,
"transcript": "음악 소리도 들리네요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "음악", "start_ms": 55, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "소리도", "start_ms": 185, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "들리네요", "start_ms": 345, "dur_ms": 231, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "It's very relaxing.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "It's", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "very", "start_ms": 155, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "relaxing.", "start_ms": 275, "dur_ms": 301, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "맞아요, 힐링되는 기분이에요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "맞아요,", "start_ms": 60, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "힐링되는", "start_ms": 200, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "기분이에요", "start_ms": 420, "dur_ms": 252, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "Do you come here often?",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Do", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 110, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "come", "start_ms": 190, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "here", "start_ms": 290, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "often?", "start_ms": 380, "dur_ms": 196, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "자주는 아니지만 가끔 와요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "자주는", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "아니지만", "start_ms": 200, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "가끔", "start_ms": 370, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "와요", "start_ms": 490, "dur_ms": 86, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "다음에 또 같이 와요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 45, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 205, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 285, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "와요", "start_ms": 405, "dur_ms": 171, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "I'd love to.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "I'd", "start_ms": 35, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "love", "start_ms": 155, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "to.", "start_ms": 295, "dur_ms": 181, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "벌써 시간이 이렇게 됐네요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "벌써", "start_ms": 70, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "시간이", "start_ms": 210, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 370, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "됐네요", "start_ms": 490, "dur_ms": 186, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "Time flies when you're having fun.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Time", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "flies", "start_ms": 150, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "when", "start_ms": 270, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "you're", "start_ms": 370, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "having", "start_ms": 480, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "fun.", "start_ms": 600, "dur_ms": 152, "phase_lr_deg": 0.0}
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
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 33,
"transcript": "이제 슬슬 일어나볼까요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "슬슬", "start_ms": 170, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "일어나볼까요?", "start_ms": 300, "dur_ms": 276, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "Sure, let's head out.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "Sure,", "start_ms": 45, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "let's", "start_ms": 175, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "head", "start_ms": 285, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "out.", "start_ms": 405, "dur_ms": 171, "phase_lr_deg": 0.0}
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
"transcript": "조심히 들어가세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "조심히", "start_ms": 55, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "들어가세요", "start_ms": 225, "dur_ms": 351, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "You too, take care.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "You", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "too,", "start_ms": 130, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "take", "start_ms": 250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "care.", "start_ms": 360, "dur_ms": 216, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "연락할게요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "연락", "start_ms": 60, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "할게요", "start_ms": 220, "dur_ms": 197, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "See you later.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 155, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "later.", "start_ms": 245, "dur_ms": 231, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "안녕히 계세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "계세요", "start_ms": 240, "dur_ms": 286, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "Bye for now.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Bye", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "for", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "now.", "start_ms": 250, "dur_ms": 186, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "오늘 즐거웠어요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 55, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "즐거웠어요", "start_ms": 195, "dur_ms": 321, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "Me too, thanks.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "Me", "start_ms": 45, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "too,", "start_ms": 145, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "thanks.", "start_ms": 255, "dur_ms": 176, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "잘 가요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "잘", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "가요", "start_ms": 140, "dur_ms": 136, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "Goodbye.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Goodbye.", "start_ms": 40, "dur_ms": 236, "phase_lr_deg": 0.0}
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
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 48,
"transcript": "다음에 봬요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "봬요", "start_ms": 210, "dur_ms": 166, "phase_lr_deg": 0.0}
]
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
*Generated by WKAppBot ask gemini — 2026-04-04 03:37:43*
