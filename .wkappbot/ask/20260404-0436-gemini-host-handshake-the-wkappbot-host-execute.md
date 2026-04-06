# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=30288  time=04:35:06  uptime=11750s  mem=771MB  system_processes=364"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260404_043454.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 103-135-103-165-103-137-152-216-132 137-360-135-163-106-103-107-106-103-547-325-163-103-316-103-135-132-130-103-130-137-103-163-136-103-130-163-106-103-630-671-130-163-137-163-1_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 537-637-163-165-163-136-103-136-136-136-103-163-103-132-103-516-102-167-176-176-631-167-175-137_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 672-627-265-267-260-257-326-216-106-673-106-625-263-562-256-756-725-627-267-526-625-235-267-276-273-627-231-657-132 124-176-654-136-673-105-126-162-167-136-103-162-162-261-623-2_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 573-357-621-632-163-103-132-123-751-125-137-136-103-163-106-316-136-103-136-103-167-106-167-106-107-103-126-630-652-136-136-106-651-362-312-167-103-176-130-512-263-267-267_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 231-230-235-325-576-751-540-546-573-573-570-574-657-627-361-137-132-123 756_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 523-132-312-316-136-326-352-526-156-351-123-213-567-357-536-365-135-351-326-132-231-312-136-136-325-213-321-123-231-321-260-267-216-261-325-265-235-263-352-321-256-567-265-205-3_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 263-263-236-261-216-263-126-312-601-615-612-675-762-672-671-163-136-167-617-103-130-103-326-301-136-123-216-376-312-362-601-670-675-251-312-102-316-632-623-630-163-130-312-321-7_V.mp3)
  Segment 8: 11032ms ~ 20068ms (file: 312-130-761-612-216-316-267-216-160-627-263-326-267-120-136-132-120-276-312-103-217-217-102-167-126-721-607-671-617-765-765-237-231-271-276-123-132-276-132-321-103-576-230-173-2_W.mp3)
  Segment 9: 21068ms ~ 21644ms (file: 752-261-162-261-312-136-306-326-362-362-632-203-302-236-302-236-750-301-137-316-160-263-261-106-136-103-326-263-123-132-312-526-210-126-652-230-263-632-621-261-213-102-607-762-6_V.mp3)
  Segment 10: 22644ms ~ 23220ms (file: 310-175-103-526-523-253-235-562-532-532-236-263-326-326-326-632-236-562-652-623-625-620-265-263-623-523-526-256-265-325-325-532-215-235-237-321-327-321-253-325-325-236-562-365-3_W.mp3)
  Segment 11: 24220ms ~ 24796ms (file: 615-651-236-153-152-175-145-316-613-650-615-671-716-106-126-162-620-127-126-107-312-120-216-162-162-162-136-103-106-125-106-132-671-103-571-106-172-162-106-103-167-136-236-102-1_V.mp3)
  Segment 12: 25796ms ~ 26372ms (file: 103-137-361-563-312-106-576-657-130-156-106-136-167-106-672-613-162-130-136-631-136-136-132-136-160-106-236-162-316-132-137-103-132-761-175-756-756-137-127-132-751-517-156-157-6_V.mp3)
  Segment 13: 27372ms ~ 27948ms (file: 312-103-601-162-103-130-136-765-675-675-627-126-132-203-302-103-612-627-261-621-265-625-610-263-216-120-203-312-105-105-105-102-105-105-601-627-576-314 132-314-132 130-301 572-5_V.mp3)
  Segment 14: 28948ms ~ 29524ms (file: 107-107-130-713-312-310-137-715-701-701 301-301-137-173-310-137-371-103-712-170-301-317-130-173-310-103-137-310-173-130-137-301-310-130-730-730-175-375-103-715-130-713-713 103-1_V.mp3)
  Segment 15: 30524ms ~ 31100ms (file: 651-672-276-621-367-627-672-362-567-256-725-273-362-267-126-621-253-213-216-231-123-137-671-627-316-526-267-621-612-126-213-216-126-261-167-617-627-261-256-627-623-267-162-136-3_W.mp3)
  Segment 16: 32100ms ~ 32676ms (file: 613-623-126-321-123-103-103-132-134-132-175-715-715-130-371-136-103-301-671-610-327-326-260-203-203-203-217-267-263-267-267-726-216-126-136-206-206-260-260-260-267-267-127-276-3_V.mp3)
  Segment 17: 33676ms ~ 34252ms (file: 572-576-576-567-576-576-576-652-567-576-567-576-567-576-576-576-567-567-567-576-567-576-576-567-576-567-572-576-567-576-576-567-567-576-567-576-567-567-576-576-576-567-576-567-5_W.mp3)
  Segment 18: 35252ms ~ 35828ms (file: 301-137-173-107-173-751-137-103-375-310 135-153-315-130-135-301-103-103-135-315-301-175-173-130-107-135-153-137-135-175-375-317-137 135-135-531-571-315-317-103-371-137-713-103-3_W.mp3)
  Segment 19: 36828ms ~ 37404ms (file: 632-137-103-136-103-176-173-137-206-165-106-130-137-571-326-130-623-136-132-130-167-163-267-612_V.mp3)
  Segment 20: 38404ms ~ 44200ms (file: 206-321-321-123-213-267-123-231-123-126-157-123-162-123-126-120-203-231-237-321-231-231-321-231-320-326-231-312-213-213-231-321-123-123-213-213-312-132-132-312-132-123-123-123-1_W.mp3)
  Segment 21: 45200ms ~ 45776ms (file: 672-625-762-276-267-276-267-276 754-574-756-273-237-273-273-267-267-756-763-706-762-625-172-675-657-607-637-627-673-627-765-673-367-372-237-362-312-326-316-317-671-213-321-137-3_V.mp3)
  Segment 22: 46776ms ~ 47352ms (file: 623-267-162-127-362-671-547-547-540-503-627-632-623-726-612-231-570-517-570-570-751-130-602-326-216-261-602-602-276-270-270-762-276-260-206-620 163-316-162-760-103-630-160-670-7_W.mp3)
  Segment 23: 48352ms ~ 56668ms (file: 126-256-123-620-170-123-761-574-257-507-576-216-320-526-257-256-136-312-167-765-162-175-132-621-517-176-132-132-136-136-765-762-762-762-632-107-321-753-236-231-312-213-213-123-1_W.mp3)
  Segment 24: 57668ms ~ 58244ms (file: 765-765-756-765-675-765-675 675-617-671-671-671-756-765-765-765-765-765-765-675-675-765-675-756-675-756-576-721-165-526-567-567-527-572-567-615-651-561-165-765-167-176-761-716-7_W.mp3)
  Segment 25: 59244ms ~ 59820ms (file: 657-576-672-673-675-576-675-765-547-574 576-576-316-132-316-163_W.mp3)
  Segment 26: 60820ms ~ 61396ms (file: 312-621-612-162-126-106-621-612-620-361-136-562-216-162-102-106-137-756-765-672-610-627-102-623-106-132-316-132-152-310-167-250-561-576-524-542-574-572-621-621 756-756-756-126_W.mp3)
  Segment 27: 62396ms ~ 62972ms (file: 312-312-657 312-267-321-231-326-326-327-326-265-675-765-657-567-562-265-623-263-672-612-617-163-163-765-376-361-263-326-216-156-362-136-361-631-361-136-132 135-132-613-321-106-1_W.mp3)
  Segment 28: 63972ms ~ 64548ms (file: 632-123-261-213-312-132-120-102-210-256-123-361-316-165-623-612-130-103-103-132-103-610-612-176-136-163-136-167-610-316-176-615-156-675-675-671-675-627-160-603-673-106-674-106-6_V.mp3)
  Segment 29: 65548ms ~ 70048ms (file: 372-173-561-607-753-573-136-736-763-312-123-107-267-210-265-106-735-173-173-762-162-671-526-276-267-260-527-237-372-716-574-713-172-137-326-260-267-276-267-276-675-127-132-237-3_W.mp3)
  Segment 30: 71048ms ~ 71624ms (file: 125 163-326-361-103-126-310-310-310-310 106-610-106-160-160-103-103-136_V.mp3)
  Segment 31: 72624ms ~ 73200ms (file: 762-623-317-316-316-132-132-312-765-136-375-257-165-501-172 321-261-570-573-321-132-372-206-236-267-756-173-132-237-635-216-271-205-321-702-276-576-657-570-357-623-265-762-271-7_V.mp3)
  Segment 32: 74200ms ~ 74776ms (file: 326-236-320-230-173-106-162-132-163-173-315-370-316-325-130-576-137-136-103-136-136-650-756-137-103-132-217-136-160-127-103-130-103-136-517-103-316-136-137-135-106-163-123-302-3_V.mp3)
  Segment 33: 75776ms ~ 76352ms (file: 513-516-501-657-625-236-213-216-263-623-236-236-236-236-326-326-236-263-321-236-231-236-261-236-231-627-231-326-526-256-236-623-263-261-623-623-326-326-236-236-267-263-265-267-2_W.mp3)
  Segment 34: 77352ms ~ 77928ms (file: 106-317-671-176-312-163-103-701-123-216-620-632-316-160-137-136-761-167-671-103-132-617-163-136-103-167-617-627-216-163-613-130-673-135-615-136-316-761-316-167-160-167-167-163-3_V.mp3)
  Segment 35: 78928ms ~ 85120ms (file: 261-207-516-354-137-706-506-564-567-103-135-312-563-751-510-256-276-123-102-153-106-310-317-316-276-201-215-527-230-103-301-320-321-123-312-301-350-265-130-370-762-261-762-136-7_V.mp3)
  Segment 36: 86120ms ~ 86696ms (file: 106-106-106-106-201-206-263-167-167-176-671-163-163-162-316-321-102-306-326-617-106-617-601-612-106-106-103-670-672 361-175-132-126-263-627-326-310-301-362-160-130-675-675-765-6_V.mp3)
  Segment 37: 87696ms ~ 88272ms (file: 627-267-627-267-267-216-627-627-725-765-574-567-572-576-157-571-315-367-365-315-361-267-123-231-237-237-265-263-267-267-267-256-257-572-576-574-570-574-574-527-275-657-536-517-5_W.mp3)
  Segment 38: 89272ms ~ 90712ms (file: 570-216-173-103-103-102-531-576-637-102-756-157-136-601-310-703-137-517-310-132-135-172-162-102-106-132-503-103-316-231-657-516-651-765-321-215-532-625-327-157-127-367-312 137-1_W.mp3)
  Segment 39: 91712ms ~ 92288ms (file: 725-216-120-123-201-103-326-236-123-126-216-256-621-673-267-623-623-675-267-632-261-623-671-621-612-267-653-623-672-623-715-602-217-631-236-102-103-106-136-160-756-765-627-276-6_W.mp3)
  Segment 40: 93288ms ~ 93864ms (file: 571 627 672-160-162-160-162-127-602-712-160-127-176-126-326-762-673-675-726-567-367-261-527-756-572-651-576-576-673-657-567-576-653-765-756-751-752-675-567-367-756-765-675-576-5_W.mp3)
  Segment 41: 94864ms ~ 95440ms (file: 127-251-256-126-126-216-261-267-263-265-623-267-623-216-216-216-612-617-276-617-627-615-672-721-762-167-761-526-572-576-126-537-512-567-275-721-175-267-621-261-623-263-623-621-2_W.mp3)
  Segment 42: 96440ms ~ 97016ms (file: 153-513-156-571-516-153-571-715-756-657-576-576-576-651-576-675-567-576-657-576-567-765-657-576-576-567-576-756-576-576-675-675-671-761-675-657-165-615-675-567-675-657-567-576-5_W.mp3)
  Segment 43: 98016ms ~ 98592ms (file: 576-651-137-631-136-731-671-651-675-675-635-517-573-567-567-567-526 216-213-123_W.mp3)
  Segment 44: 99592ms ~ 100168ms (file: 235-312-123-162-215-251-561-561-562-263-156-165-512-517-657-625-216-162-162-352-256-123-125-541-571-517-512-527-165-152-517-516-136-165-571-576-627-261-621-236-265-125-567-567-5_W.mp3)
  Segment 45: 101168ms ~ 101744ms (file: 263-675-652-361-326-362-326-176-675-170-106-265-512-623-632-326-752-231-326-236-632-627-627-637-362-321-312-312-132-627-637-623-267-132-163-610-263-231-326-652-657-756-210-362-3_W.mp3)
  Segment 46: 102744ms ~ 103320ms (file: 132 612-163-136-316-136-130-613-312-126-163-316-136-132-136-130-160-167-136-120-123-321-312-127-210-321-326-312-120-103-123-267-261-321-103-216-657-617-261-216-132 517-132_W.mp3)
  Segment 47: 104320ms ~ 104896ms (file: 106-103-153-613-136-136-103-567-261-316-160-132-136-123-132-261-127-132-135-751-175-173-310-671-310-367-126-102-106-103-165-136-106-163-103-167-516-106-176-321-316-612-631-361-5_V.mp3)
  Segment 48: 105896ms ~ 106472ms (file: 103-160-103-312-206-623-516-163-106-761-631-156-126-136-103-165-103-671-103-102-173-137-103-157-132-356-103-136-132-763-163-610-103-307-137-563-516-632-130-103-167-130-132-123-1_V.mp3)
  Segment 49: 107472ms ~ 108048ms (file: 540-276-540-153-132-231-216-201-627-376-317-320-276-540-735-736-367-657-567-576-657-657-657-657-625-623-627-265-352-521-527-572-574-540-524-251-352-352-352-523-325-326-235-532-5_W.mp3)
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
> **Time**: 2026-04-04 04:36:19
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "안녕하세요 만나서 반갑습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 103,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 103, "dur_ms": 232, "phase_lr_deg": 0.0},
{"word": "만나서", "start_ms": 335, "dur_ms": 102, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 437, "dur_ms": 139, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "오늘 날씨가 정말 좋네요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 537,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 537, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 637, "dur_ms": 328, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 965, "dur_ms": 299, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 1264, "dur_ms": 888, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "What is your favorite food?",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 672,
"audio_type": "speech",
"words": [
{"word": "What", "start_ms": 672, "dur_ms": 627, "phase_lr_deg": 5.0},
{"word": "is", "start_ms": 1299, "dur_ms": 265, "phase_lr_deg": 5.0},
{"word": "your", "start_ms": 1564, "dur_ms": 267, "phase_lr_deg": 5.0},
{"word": "favorite", "start_ms": 1831, "dur_ms": 517, "phase_lr_deg": 5.0},
{"word": "food?", "start_ms": 2348, "dur_ms": 1380, "phase_lr_deg": 5.0}
]
},
{
"segment": 4,
"transcript": "저는 한국 요리를 좋아합니다",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 573,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 573, "dur_ms": 357, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 930, "dur_ms": 1253, "phase_lr_deg": 0.0},
{"word": "요리를", "start_ms": 2183, "dur_ms": 255, "phase_lr_deg": 0.0},
{"word": "좋아합니다", "start_ms": 2438, "dur_ms": 2866, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 231,
"audio_type": "music",
"words": []
},
{
"segment": 6,
"transcript": "Can you speak Japanese?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 523,
"audio_type": "speech",
"words": [
{"word": "Can", "start_ms": 523, "dur_ms": 132, "phase_lr_deg": -2.0},
{"word": "you", "start_ms": 655, "dur_ms": 312, "phase_lr_deg": -2.0},
{"word": "speak", "start_ms": 967, "dur_ms": 316, "phase_lr_deg": -2.0},
{"word": "Japanese?", "start_ms": 1283, "dur_ms": 4173, "phase_lr_deg": -2.0}
]
},
{
"segment": 7,
"transcript": "조금 할 수 있습니다",
"speaker": "speaker_3",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 263,
"audio_type": "speech",
"words": [
{"word": "조금", "start_ms": 263, "dur_ms": 263, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 526, "dur_ms": 236, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 762, "dur_ms": 261, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 1023, "dur_ms": 4953, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "Where are you from originally?",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 312,
"audio_type": "speech",
"words": [
{"word": "Where", "start_ms": 312, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "are", "start_ms": 442, "dur_ms": 761, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 1203, "dur_ms": 612, "phase_lr_deg": 0.0},
{"word": "from", "start_ms": 1815, "dur_ms": 216, "phase_lr_deg": 0.0},
{"word": "originally?", "start_ms": 2031, "dur_ms": 7005, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "서울에서 태어났습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 752,
"audio_type": "speech",
"words": [
{"word": "서울에서", "start_ms": 752, "dur_ms": 261, "phase_lr_deg": 0.0},
{"word": "태어났습니다", "start_ms": 1013, "dur_ms": 162, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 310,
"audio_type": "instrument",
"words": []
},
{
"segment": 11,
"transcript": "Would you like some coffee?",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 615,
"audio_type": "speech",
"words": [
{"word": "Would", "start_ms": 615, "dur_ms": 651, "phase_lr_deg": 10.0},
{"word": "you", "start_ms": 1266, "dur_ms": 236, "phase_lr_deg": 10.0},
{"word": "like", "start_ms": 1502, "dur_ms": 153, "phase_lr_deg": 10.0},
{"word": "some", "start_ms": 1655, "dur_ms": 152, "phase_lr_deg": 10.0},
{"word": "coffee?", "start_ms": 1807, "dur_ms": 175, "phase_lr_deg": 10.0}
]
},
{
"segment": 12,
"transcript": "네 부탁드립니다 감사합니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 103,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 103, "dur_ms": 137, "phase_lr_deg": 0.0},
{"word": "부탁드립니다", "start_ms": 240, "dur_ms": 361, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 601, "dur_ms": 563, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "How much is this book?",
"speaker": "speaker_4",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 312,
"audio_type": "speech",
"words": [
{"word": "How", "start_ms": 312, "dur_ms": 103, "phase_lr_deg": 0.0},
{"word": "much", "start_ms": 415, "dur_ms": 601, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 1016, "dur_ms": 162, "phase_lr_deg": 0.0},
{"word": "this", "start_ms": 1178, "dur_ms": 103, "phase_lr_deg": 0.0},
{"word": "book?", "start_ms": 1281, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "이 책은 오천원입니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 107,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 107, "dur_ms": 107, "phase_lr_deg": 0.0},
{"word": "책은", "start_ms": 214, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "오천원입니다", "start_ms": 344, "dur_ms": 713, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "It is quite cheap really",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 651,
"audio_type": "speech",
"words": [
{"word": "It", "start_ms": 651, "dur_ms": 672, "phase_lr_deg": 2.0},
{"word": "is", "start_ms": 1323, "dur_ms": 276, "phase_lr_deg": 2.0},
{"word": "quite", "start_ms": 1599, "dur_ms": 621, "phase_lr_deg": 2.0},
{"word": "cheap", "start_ms": 2220, "dur_ms": 367, "phase_lr_deg": 2.0},
{"word": "really", "start_ms": 2587, "dur_ms": 627, "phase_lr_deg": 2.0}
]
},
{
"segment": 16,
"transcript": "맞아요 세일 중이거든요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 613,
"audio_type": "speech",
"words": [
{"word": "맞아요", "start_ms": 613, "dur_ms": 623, "phase_lr_deg": 0.0},
{"word": "세일", "start_ms": 1236, "dur_ms": 126, "phase_lr_deg": 0.0},
{"word": "중이거든요", "start_ms": 1362, "dur_ms": 321, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 572,
"audio_type": "noise",
"words": []
},
{
"segment": 18,
"transcript": "What time does it close?",
"speaker": "speaker_4",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 301,
"audio_type": "speech",
"words": [
{"word": "What", "start_ms": 301, "dur_ms": 137, "phase_lr_deg": 0.0},
{"word": "time", "start_ms": 438, "dur_ms": 173, "phase_lr_deg": 0.0},
{"word": "does", "start_ms": 611, "dur_ms": 107, "phase_lr_deg": 0.0},
{"word": "it", "start_ms": 718, "dur_ms": 173, "phase_lr_deg": 0.0},
{"word": "close?", "start_ms": 891, "dur_ms": 751, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "아홉 시에 닫습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 632,
"audio_type": "speech",
"words": [
{"word": "아홉", "start_ms": 632, "dur_ms": 137, "phase_lr_deg": 0.0},
{"word": "시에", "start_ms": 769, "dur_ms": 103, "phase_lr_deg": 0.0},
{"word": "닫습니다", "start_ms": 872, "dur_ms": 136, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 206,
"audio_type": "instrument",
"words": []
},
{
"segment": 21,
"transcript": "Excuse me where is the station?",
"speaker": "speaker_5",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 672,
"audio_type": "speech",
"words": [
{"word": "Excuse", "start_ms": 672, "dur_ms": 625, "phase_lr_deg": -15.0},
{"word": "me", "start_ms": 1297, "dur_ms": 762, "phase_lr_deg": -15.0},
{"word": "where", "start_ms": 2059, "dur_ms": 276, "phase_lr_deg": -15.0},
{"word": "is", "start_ms": 2335, "dur_ms": 267, "phase_lr_deg": -15.0},
{"word": "the", "start_ms": 2602, "dur_ms": 276, "phase_lr_deg": -15.0},
{"word": "station?", "start_ms": 2878, "dur_ms": 267, "phase_lr_deg": -15.0}
]
},
{
"segment": 22,
"transcript": "저기 사거리에서 오른쪽입니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 623,
"audio_type": "speech",
"words": [
{"word": "저기", "start_ms": 623, "dur_ms": 267, "phase_lr_deg": 0.0},
{"word": "사거리에서", "start_ms": 890, "dur_ms": 162, "phase_lr_deg": 0.0},
{"word": "오른쪽입니다", "start_ms": 1052, "dur_ms": 127, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 126,
"audio_type": "music",
"words": []
},
{
"segment": 24,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 765,
"audio_type": "noise",
"words": []
},
{
"segment": 25,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 657,
"audio_type": "noise",
"words": []
},
{
"segment": 26,
"transcript": "Thank you for your help",
"speaker": "speaker_5",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 312,
"audio_type": "speech",
"words": [
{"word": "Thank", "start_ms": 312, "dur_ms": 621, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 933, "dur_ms": 612, "phase_lr_deg": 0.0},
{"word": "for", "start_ms": 1545, "dur_ms": 162, "phase_lr_deg": 0.0},
{"word": "your", "start_ms": 1707, "dur_ms": 126, "phase_lr_deg": 0.0},
{"word": "help", "start_ms": 1833, "dur_ms": 106, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "아니에요 천만에요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 312,
"audio_type": "speech",
"words": [
{"word": "아니에요", "start_ms": 312, "dur_ms": 312, "phase_lr_deg": 0.0},
{"word": "천만에요", "start_ms": 624, "dur_ms": 657, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "Do you have any brothers?",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 632,
"audio_type": "speech",
"words": [
{"word": "Do", "start_ms": 632, "dur_ms": 123, "phase_lr_deg": 4.0},
{"word": "you", "start_ms": 755, "dur_ms": 261, "phase_lr_deg": 4.0},
{"word": "have", "start_ms": 1016, "dur_ms": 213, "phase_lr_deg": 4.0},
{"word": "any", "start_ms": 1229, "dur_ms": 312, "phase_lr_deg": 4.0},
{"word": "brothers?", "start_ms": 1541, "dur_ms": 132, "phase_lr_deg": 4.0}
]
},
{
"segment": 29,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 372,
"audio_type": "music",
"words": []
},
{
"segment": 30,
"transcript": "네 남동생 하나 있어요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 125,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 125, "dur_ms": 163, "phase_lr_deg": 0.0},
{"word": "남동생", "start_ms": 288, "dur_ms": 326, "phase_lr_deg": 0.0},
{"word": "하나", "start_ms": 614, "dur_ms": 361, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 975, "dur_ms": 103, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 762,
"audio_type": "noise",
"words": []
},
{
"segment": 32,
"transcript": "What are you doing now?",
"speaker": "speaker_6",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 326,
"audio_type": "speech",
"words": [
{"word": "What", "start_ms": 326, "dur_ms": 236, "phase_lr_deg": -5.0},
{"word": "are", "start_ms": 562, "dur_ms": 320, "phase_lr_deg": -5.0},
{"word": "you", "start_ms": 882, "dur_ms": 230, "phase_lr_deg": -5.0},
{"word": "doing", "start_ms": 1112, "dur_ms": 173, "phase_lr_deg": -5.0},
{"word": "now?", "start_ms": 1285, "dur_ms": 106, "phase_lr_deg": -5.0}
]
},
{
"segment": 33,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 513,
"audio_type": "instrument",
"words": []
},
{
"segment": 34,
"transcript": "친구를 기다리고 있어요",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 106,
"audio_type": "speech",
"words": [
{"word": "친구를", "start_ms": 106, "dur_ms": 317, "phase_lr_deg": 0.0},
{"word": "기다리고", "start_ms": 423, "dur_ms": 671, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 1094, "dur_ms": 176, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "(mixed)",
"speaker": "speaker_1",
"confidence": 0.5,
"language": "ko",
"silence_before_ms": 261,
"audio_type": "mixed",
"words": []
},
{
"segment": 36,
"transcript": "See you later alligator",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 106,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 106, "dur_ms": 106, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 212, "dur_ms": 106, "phase_lr_deg": 0.0},
{"word": "later", "start_ms": 318, "dur_ms": 106, "phase_lr_deg": 0.0},
{"word": "alligator", "start_ms": 424, "dur_ms": 201, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 627,
"audio_type": "music",
"words": []
},
{
"segment": 38,
"transcript": "잘 가요 다음에 봐요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 570,
"audio_type": "speech",
"words": [
{"word": "잘", "start_ms": 570, "dur_ms": 216, "phase_lr_deg": 0.0},
{"word": "가요", "start_ms": 786, "dur_ms": 173, "phase_lr_deg": 0.0},
{"word": "다음에", "start_ms": 959, "dur_ms": 103, "phase_lr_deg": 0.0},
{"word": "봐요", "start_ms": 1062, "dur_ms": 103, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "Where did I put it?",
"speaker": "speaker_6",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 725,
"audio_type": "speech",
"words": [
{"word": "Where", "start_ms": 725, "dur_ms": 216, "phase_lr_deg": 0.0},
{"word": "did", "start_ms": 941, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 1061, "dur_ms": 123, "phase_lr_deg": 0.0},
{"word": "put", "start_ms": 1184, "dur_ms": 201, "phase_lr_deg": 0.0},
{"word": "it?", "start_ms": 1385, "dur_ms": 103, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 571,
"audio_type": "noise",
"words": []
},
{
"segment": 41,
"transcript": "It was here a minute ago",
"speaker": "speaker_6",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 127,
"audio_type": "speech",
"words": [
{"word": "It", "start_ms": 127, "dur_ms": 251, "phase_lr_deg": 0.0},
{"word": "was", "start_ms": 378, "dur_ms": 256, "phase_lr_deg": 0.0},
{"word": "here", "start_ms": 634, "dur_ms": 126, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 760, "dur_ms": 126, "phase_lr_deg": 0.0},
{"word": "minute", "start_ms": 886, "dur_ms": 216, "phase_lr_deg": 0.0},
{"word": "ago", "start_ms": 1102, "dur_ms": 261, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 153,
"audio_type": "noise",
"words": []
},
{
"segment": 43,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 576,
"audio_type": "noise",
"words": []
},
{
"segment": 44,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 235,
"audio_type": "noise",
"words": []
},
{
"segment": 45,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 263,
"audio_type": "music",
"words": []
},
{
"segment": 46,
"transcript": "Everything is going to be okay",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 132,
"audio_type": "speech",
"words": [
{"word": "Everything", "start_ms": 132, "dur_ms": 612, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 744, "dur_ms": 163, "phase_lr_deg": 0.0},
{"word": "going", "start_ms": 907, "dur_ms": 136, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 1043, "dur_ms": 316, "phase_lr_deg": 0.0},
{"word": "be", "start_ms": 1359, "dur_ms": 136, "phase_lr_deg": 0.0},
{"word": "okay", "start_ms": 1495, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "걱정 마세요 다 잘될 거예요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 106,
"audio_type": "speech",
"words": [
{"word": "걱정", "start_ms": 106, "dur_ms": 103, "phase_lr_deg": 0.0},
{"word": "마세요", "start_ms": 209, "dur_ms": 153, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 362, "dur_ms": 613, "phase_lr_deg": 0.0},
{"word": "잘될", "start_ms": 975, "dur_ms": 136, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 1111, "dur_ms": 136, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "믿어줘서 고마워요",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 103,
"audio_type": "speech",
"words": [
{"word": "믿어줘서", "start_ms": 103, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "고마워요", "start_ms": 263, "dur_ms": 103, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 540,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-04 04:36:19*
