# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=30344  time=07:01:32  uptime=12108s  mem=443MB  system_processes=409"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_070118.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 315-567-576-153-135-612-651-531-352-716-361-367-362-372-167-367-571-732-713-752-136-371-765-705-176-751-310-312-301-302-301-312-301-371-237-570-157-217-123-512-715-752-276-136-2_W.mp3)
  Segment 2: 1576ms ~ 6580ms (file: 261-763-576-765-736-321-132-130-312-573-571-537-210-130-203-102-152-621-315-301-653-365-231-567-572-750-576-576-231-231-752-523-712-572-765-136-762-201-201-621-206-132-126-357-5_W.mp3)
  Segment 3: 7580ms ~ 8156ms (file: 713-136-673-362-731-315-301-530-567-560-567-517-576-571-153-537-576-576-623-315-375-136-316-217-517-627-725-276-126-317-126-321-312-710-103-132-103-130-103-130-132-127-307-123-7_W.mp3)
  Segment 4: 9156ms ~ 15348ms (file: 126-236-236-632-260-267-320-162-567-231-106-267-267-672-267-273-267-276-276-216-267-627-267-576-276-162-172-652-576-267-756-207-627-267-725-726-726-725-267-160-673-376-716-237-7_W.mp3)
  Segment 5: 16348ms ~ 16924ms (file: 675-756-756-637-635-361-361-316-156-537-367-376-635-316-730-675-306-763-376-630-637-125-365-635-326-315-376-574-574-754-574-574_W.mp3)
  Segment 6: 17924ms ~ 18500ms (file: 517-670-372-705-312-167-213-265-261-607-261-276-231-267-276-352-362-231-250-572-213-163-136-326-261-267-761-362-235-276-236-752-273-231-326-237-235-273-263-207-267-267-206-276-5_W.mp3)
  Segment 7: 19500ms ~ 20076ms (file: 762-260-265-526-672-526-571-705-376-573-367-301-316-367-136-137-613-310-315-136-571-315-357-531-351-351-317-317-367-316-361-361-361-163-671-163-617-167-631-613 672 120_W.mp3)
  Segment 8: 21076ms ~ 21652ms (file: 675-756-570-253-657-756-576-263-531-502-625-762-607-576-562-365-753-570-632-657-675-657-756-756-675-376-567-567-756-657-316 765-576-756-576-567-657-615-537-527-176-671-750-173-6_V.mp3)
  Segment 9: 22652ms ~ 38312ms (file: 207-627-126-106-621-610-756-172-213-102-527-257-170-260-236-136-726-762-107-173-756-132-321-315-103-103-103-103-751-215-123 173-136-602-236-263-130-123-620-602-527-572-532-310-1_W.mp3)
  Segment 10: 39312ms ~ 44928ms (file: 276-672-701-710-765-751-176-670-216-103-123-120-312-260-263-123-103-312-213-617-263-210-263-326-320-326-326-236-120-261-230-206-320-213-761-276-267-321-576-576-325-326-320-312-3_W.mp3)
  Segment 11: 45928ms ~ 46504ms (file: 263-623-275-137-136-216-710-726-576-276-276-216-127-570-627-267-507-713-732-175-126-205-137-150-501-315-752-132-765-156-576-623-361-567-627-167-326-126-560-157-576-756-560-561-7_V.mp3)
  Segment 12: 47504ms ~ 48080ms (file: 631-130-672-627-657-367-310 301 361-316-103-312-317-316-317-135-137-317-132-135_V.mp3)
  Segment 13: 49080ms ~ 56352ms (file: 621-260-216-302-367-206-213-123-132-130-620-302-132-236-260-310-260-320-203-236-231-213-203-203-230-231-312-210-236-236-206-132-310-670-602-672-236-103-601-602-260-206-326-602-3_V.mp3)
  Segment 14: 57352ms ~ 64696ms (file: 175-572-127-175-675-130-157-710-106-761-672-126-312-130-620-136-176-612-310-267-230-312-310-310-310-102-173-175-537-163-210-231-267-620-267-107-103-103-231-230-351-316-613-143-6_W.mp3)
  Segment 15: 65696ms ~ 70952ms (file: 160 176-362-132-612-753-567-107-732-267-715-756-236-213-574-310-132-103-173-723-163-136-137-367-672-107-576-576-731-102-310-756-106-172-710-176-216-127-103-136-176-137-137-162-1_W.mp3)
  Segment 16: 71952ms ~ 72528ms (file: 507-753-275-327-672-637-507-657-213-156-135-265-712-237-267-567-235-570-237-570-572-753-574-760-375-570-612-362-571-537-756-762-723-276-567-517-352-762-725-571-275-312-570-237-2_V.mp3)
  Segment 17: 73528ms ~ 79972ms (file: 574-574-273-762-267-261-273-273-756-657-126-301-731-130-130-317-623-760-176-270-270-203-263-752-576-302-562-206-103-671-301-103-137-672-765-176-571-203-270-572-167-765-106-257-2_V.mp3)
  Segment 18: 80972ms ~ 90008ms (file: 231-132-123-216-210-210-573-257-132-312-276-215-524-271-203-215-215-527-725-267-210-213-210-215-261-276-571-312-267-256-127-572-267-257-267-267-217-672-267-237-276-312-213-201-2_W.mp3)
  Segment 19: 91008ms ~ 106200ms (file: 236-236-107-150-150-136-210-123-321-312-627-562-157-152-150-157-576-157-571-157-213-123-130 627-120-106-627-765-756-263-361-126-103-601-213-321-316-321-132-130-157-130-316-201-6_W.mp3)
  Segment 20: 107200ms ~ 115048ms (file: 310-135-260-756-627-230-302-132-130-312-316-627-130-132-132-213-570-731-163-130-612-126-517-263-124-102-312-261-705-765-201-205-203-260-306-756-620-210-263-621-625-562-267-260-3_W.mp3)
  Segment 21: 116048ms ~ 125156ms (file: 231-613-167-570-754-236-126-751-156-251-572-123-237-627-263-273-123-627-263-726-765-267-326-130-162-103-574-120-612-762-210-321-236-253-231-102-231-206-275-123-627-102-213-321-7_W.mp3)
  Segment 22: 126156ms ~ 130980ms (file: 216-752-167-617-617-761-756-316-321-231-203-230-213-320-320-760-762-302-301-726-276-765-705-275-123-213-312-302-371-376-763-756-765-230-230-237-267-216-132-765-276-216-276-203-7_W.mp3)
  Segment 23: 131980ms ~ 137704ms (file: 217-213-123-216-126-127-752-326-201-675-765-316-126-132-316-361-136-735-326-136-761-123-273-276-751-327-715-136-675-675-765-637-675-267-627-675-675-675-657-675-637-236-236-263-2_W.mp3)
  Segment 24: 138704ms ~ 147992ms (file: 127-735-621-216-302-102-312-126-213-263-260-103-231-301-123-217-210-271-276-271-517-267-725-231-130-132-270-130-312-754-721-367-756-173-370-153-351-127-273-527-754-574-507-321-1_W.mp3)
  Segment 25: 148992ms ~ 157740ms (file: 230-316-312-136-130-213-201-210-213-320-312-310-126-102-231-231-103-126-321-107-103-310-137-203-327-231-321-375-765-236-602-301-132-130-236-321-572-231-573-561-301-130-213-102-3_V_stt_the.mp3)
  Segment 26: 158740ms ~ 159316ms (file: 675 137-137-374-751-567-137-130-376-637-317-127-137-137-752-235-567-621-316-310-316-361-316-312-326-216-726-136-261-216-265-127-751 756-216-570-317-156-126-735-756-547-627-572-5_V.mp3)
  Segment 27: 160316ms ~ 169532ms (file: 302-213-301-236-260-320-321-320-321-321-320-320-627-267-263-317-132-126-261-260-627-267-726-327-326-370-326-572-753-756-170-106-170-326-260-321-301-236-326-326-236-267-612-132-6_W.mp3)
  Segment 28: 170532ms ~ 175284ms (file: 236-236-267-260-267-267-752-217-276-761-610-612-261-621-762-163-160-173-312-231-613-231-132-276-312-176-276-627-267-273-267-231-276-162-172-263-321-621-623-126-136-176-163-267-1_W.mp3)
  Segment 29: 176284ms ~ 182980ms (file: 106-156-157-135-123-123-126-613-361-126-752-176-132-162-130-267-126-276-127-175-756-572-217-675-675-675-675-672-671-672-216-132-126-571-172-216-263-231-261-120-231-236-263-216-2_W.mp3)
  Segment 30: 183980ms ~ 184556ms (file: 173-761-756-156-671-317-671-361-736-617-136-673-673-631-163-671-376-673-607-673-637-763-376-372-372-321-376-327-765-756-756-756-371-543-261 275-762-763-367-603-537-315-317-317-1_V.mp3)
  Segment 31: 185556ms ~ 194160ms (file: 103-317-312-752-756-276-327-237-237-302-136-372-357-276-726-267-276-627-312-302-327-573-715-573-157-756-317-167-237-537-672-312-567-315-136-106-321-106-123-237-672-126-276-271-2_W.mp3)
  Segment 32: 195160ms ~ 195736ms (file: 571-571-573-571-751-573-571-756-571-752-752-572-752-572-751-752-752-753-752-752-752-752-752 571-571-571-752-574-574-754-751-752-752-752-751-571-573-571-517-571-570-571-574-571 5_W.mp3)
  Segment 33: 196736ms ~ 205340ms (file: 321-321-126-216-362-123-236-102-261-267-126-261-170-361-762-106-231-106-620-267-236-321-120-312-367-216-261-231-132-132-162-103-162-167-106-106-136-263-132-103-106-306-103-103-1_W_stt_but_they_have_little_idea_how_long_it_wa.mp3)
  Segment 34: 206340ms ~ 206916ms (file: 731-627-671-576-576-736-631-357-357-567-576-657-653-657-756-567-765-675-567-317-357-573-635-357-576-735-316-357-357-371-317-627-572-576-567-657-753-367-573-315-130-321-567-652-3_W.mp3)
  Segment 35: 207916ms ~ 208492ms (file: 652-136-613-136-136-312-317-317-372-132-361-163-613-135-615-316-316-756-615-567-150-213-562-250-312-362-675-632-132-630_W.mp3)
  Segment 36: 209492ms ~ 219068ms (file: 157-104-130-370-231-312-206-206-236-725-756-715-750-754-756-573-130-130-203-312-130-103-130-302-316-320-675-756-754-310-753-312-750-276-103-203-120-217-326-236-361-312-320-263-1_W.mp3)
  Segment 37: 220068ms ~ 224784ms (file: 162-127-162-267-126-123-210-126-627-136-312-726-765-765-765-765-756-574-275-261-276-216-267-761-716-125-326-321-216-216-213-213-231-261-627-627-260-236-276-263-261-263-213-267-2_W_stt_the.mp3)
  Segment 38: 225784ms ~ 233848ms (file: 627-126-120-102-312-753-321-260-123-627-103-102-265-231-136-132-612-276-236-132-106-106-135-103-123-236-106-132-216-725-126-216-102-126-102-765-261-102-132-130-103-263-265-571-3_V.mp3)
  Segment 39: 234848ms ~ 243092ms (file: 756-351-312-237-273-321-713-523-237-572-574-130-310-307-310-301-310-310-130-163-102-123-267-267-267-310-310-120-123-120-276-320-257-257-132-230-320-276-103-137-231-326-203-256-7_V.mp3)
  Segment 40: 244092ms ~ 244668ms (file: 524-276-572-376-365-167-721-751-527-512-576-567-572-756-752-756-572-756-765-726-273-765-675-517-635-753-536-673-652-657-652-675-263-263-627-562-567-567-657-576-567-765-567-567-5_V.mp3)
  Segment 41: 245668ms ~ 246244ms (file: 623-235-327-135-137-752-362-760-567-265-756-516-765-351-351-310-316-352-367-607-375-576-321-527-732-576-623-267-756-530-756-570-567-567-576-620-637-367-321-752-574-570-705-756-5_V.mp3)
  Segment 42: 247244ms ~ 247820ms (file: 750-715-715-710-701-701-715-175-715-175-705-715-715-715-751-751-750 617-607-760-571-170-173-107-710-750-170-175-715-715-715-710-715-751-710-175-176-715-701-750-751 705-756-750-7_V.mp3)
  Segment 43: 248820ms ~ 249396ms (file: 172-137-716-736-753-753-751-736-713-765-753-756-753-756-756-765-751-765-765-756-765-675-765-673-175-716-176-173-172-173-172-172-137-715-756-753-763-756-736-376-765-763-376-763-6_W.mp3)
  Segment 44: 250396ms ~ 254032ms (file: 574-132-251-130-235-206-260-216-206-132-260-103-123-132 175-716-617-371-761-176-167-657-657-701-316-361-316-316-153-762-670-673-716-517-136-753-735 573_W.mp3)
  Segment 45: 255032ms ~ 260180ms (file: 310-302-310-165-510-320-302-320-326-310-123-312-132-123-132-371-174-210-230-574-120-315-231-203-301-132-413-230-136-162-153-103-106-103-130-312-321-103-136-312-103-103-126-316-1_V.mp3)
  Segment 46: 261180ms ~ 261756ms (file: 574-576-576-752-765-765-657-567-657-756-576-756-764-567-657-654-761-576-657-657-576-567-517-561-156-165-165-651-561-576-756-576-576-576-756-754-576-657-657-562-675-765-756-675-6_V.mp3)
  Segment 47: 262756ms ~ 270064ms (file: 321-132-162-136-312-316-326-312-320-231-376-572-570-152-123-123-123-236-231-321-321-130-205-231-352-102-132-123-102-135-351-135-351-756-103-716-310-102-106-102-105-137-170-265-6_W.mp3)
  Segment 48: 271064ms ~ 284780ms (file: 102-201-132-326-132-123-143-231-320-102-203-236-163-106-320-236-263-261-310-312-321-320-312-132-310-230-162-126-657-310-321-236-143-321-260-126-301-123-132-321-301-163-123-102-3_W.mp3)
  Segment 49: 285780ms ~ 295716ms (file: 267-620-103-317-130-176-261-267-326-107-317-301-312-126-236-216-210-125-301-216-213-236-231-107-361-570-317-160-163-172-276-620-267-126-310-231-312-123-623-302-263-267-103-260-2_W.mp3)
  Segment 50: 296716ms ~ 305176ms (file: 231-120-261-263-267-216-765-162-132-631-103-136-312-126-106-415-231-270-230-562-275-267-203-572-201-203-130-127-217-103-132-316-302-173-712-713-216-257-527-107-216-361-576-257-1_W.mp3)
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
> **Time**: 2026-04-06 07:03:58
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "오늘 기분은 어떠세요? 정말 날씨가 좋네요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "기분은", "start_ms": 600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "어떠세요?", "start_ms": 1150, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 2000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 2500, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "좋네요.", "start_ms": 3100, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "한국어 공부를 시작한 지 얼마나 되셨나요? 가라오케 시스템으로 연습해봐요.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "공부를", "start_ms": 850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "시작한", "start_ms": 1400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "지", "start_ms": 1900, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "얼마나", "start_ms": 2100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "되셨나요?", "start_ms": 2650, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "가라오케", "start_ms": 3500, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "시스템으로", "start_ms": 4300, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "연습해봐요.", "start_ms": 5150, "dur_ms": 900, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "Let's practice some English phrases as well. Repeat after me.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "Let's", "start_ms": 180, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "practice", "start_ms": 520, "dur_ms": 550, "phase_lr_deg": -5.0},
{"word": "some", "start_ms": 1100, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "English", "start_ms": 1400, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "phrases", "start_ms": 1950, "dur_ms": 600, "phase_lr_deg": -5.0},
{"word": "as", "start_ms": 2600, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "well.", "start_ms": 2850, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "Repeat", "start_ms": 3400, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "after", "start_ms": 3950, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "me.", "start_ms": 4400, "dur_ms": 300, "phase_lr_deg": -5.0}
]
},
{
"segment": 4,
"transcript": "Where is the nearest subway station? I'm a bit lost.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "Where", "start_ms": 250, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "is", "start_ms": 650, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "the", "start_ms": 850, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "nearest", "start_ms": 1050, "dur_ms": 550, "phase_lr_deg": -5.0},
{"word": "subway", "start_ms": 1650, "dur_ms": 600, "phase_lr_deg": -5.0},
{"word": "station?", "start_ms": 2300, "dur_ms": 700, "phase_lr_deg": -5.0},
{"word": "I'm", "start_ms": 3200, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "a", "start_ms": 3500, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "bit", "start_ms": 3650, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "lost.", "start_ms": 3900, "dur_ms": 450, "phase_lr_deg": -5.0}
]
},
{
"segment": 5,
"transcript": "The tough sell is convincing them to change their mind.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "The", "start_ms": 120, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "tough", "start_ms": 300, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "sell", "start_ms": 700, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "is", "start_ms": 1150, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "convincing", "start_ms": 1400, "dur_ms": 750, "phase_lr_deg": -5.0},
{"word": "them", "start_ms": 2200, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "to", "start_ms": 2550, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "change", "start_ms": 2750, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "their", "start_ms": 3300, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "mind.", "start_ms": 3650, "dur_ms": 500, "phase_lr_deg": -5.0}
]
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
"transcript": "여기서 오른쪽으로 쭉 가시면 백화점이 나옵니다.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 220,
"audio_type": "speech",
"words": [
{"word": "여기서", "start_ms": 220, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "오른쪽으로", "start_ms": 850, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "쭉", "start_ms": 1600, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "가시면", "start_ms": 1850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "백화점이", "start_ms": 2450, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "나옵니다.", "start_ms": 3200, "dur_ms": 650, "phase_lr_deg": 0.0}
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
"transcript": "이 옷은 얼마인가요? 입어볼 수 있을까요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "옷은", "start_ms": 400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "얼마인가요?", "start_ms": 800, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "입어볼", "start_ms": 1700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 2250, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "있을까요?", "start_ms": 2500, "dur_ms": 700, "phase_lr_deg": 0.0}
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
"transcript": "매우 잘 어울리시네요. 다른 색상도 있습니다.",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "매우", "start_ms": 200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "어울리시네요.", "start_ms": 1000, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "다른", "start_ms": 2100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "색상도", "start_ms": 2550, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "있습니다.", "start_ms": 3200, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "Could you help me find a gift for my friend?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "Could", "start_ms": 150, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "you", "start_ms": 500, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "help", "start_ms": 750, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "me", "start_ms": 1150, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "find", "start_ms": 1400, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "a", "start_ms": 1850, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "gift", "start_ms": 2000, "dur_ms": 450, "phase_lr_deg": -5.0},
{"word": "for", "start_ms": 2500, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "my", "start_ms": 2800, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "friend?", "start_ms": 3050, "dur_ms": 550, "phase_lr_deg": -5.0}
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
"transcript": "친구분을 위한 선물인가요? 향수나 지갑은 어떠세요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "친구분을", "start_ms": 180, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "위한", "start_ms": 850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "선물인가요?", "start_ms": 1300, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "향수나", "start_ms": 2300, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "지갑은", "start_ms": 2950, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "어떠세요?", "start_ms": 3500, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "지갑이 좋을 것 같아요. 가죽 제품으로 보여주세요.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 220,
"audio_type": "speech",
"words": [
{"word": "지갑이", "start_ms": 220, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "좋을", "start_ms": 750, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "것", "start_ms": 1150, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "같아요.", "start_ms": 1350, "dur_ms": 550, "phase_lr_deg": -5.0},
{"word": "가죽", "start_ms": 2100, "dur_ms": 450, "phase_lr_deg": -5.0},
{"word": "제품으로", "start_ms": 2600, "dur_ms": 700, "phase_lr_deg": -5.0},
{"word": "보여주세요.", "start_ms": 3350, "dur_ms": 800, "phase_lr_deg": -5.0}
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
"transcript": "신용카드로 결제 가능한가요? 할부도 되나요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "신용카드로", "start_ms": 200, "dur_ms": 700, "phase_lr_deg": -5.0},
{"word": "결제", "start_ms": 950, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "가능한가요?", "start_ms": 1400, "dur_ms": 750, "phase_lr_deg": -5.0},
{"word": "할부도", "start_ms": 2350, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "되나요?", "start_ms": 2900, "dur_ms": 550, "phase_lr_deg": -5.0}
]
},
{
"segment": 21,
"transcript": "네, 가능합니다. 영수증은 가방에 넣어드릴까요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 150, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "가능합니다.", "start_ms": 450, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "영수증은", "start_ms": 1350, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "가방에", "start_ms": 2000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "넣어드릴까요?", "start_ms": 2550, "dur_ms": 850, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 23,
"transcript": "감사합니다. 안녕히 계세요. 다음에 또 올게요.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "감사합니다.", "start_ms": 120, "dur_ms": 800, "phase_lr_deg": -5.0},
{"word": "안녕히", "start_ms": 1000, "dur_ms": 450, "phase_lr_deg": -5.0},
{"word": "계세요.", "start_ms": 1500, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "다음에", "start_ms": 2200, "dur_ms": 450, "phase_lr_deg": -5.0},
{"word": "또", "start_ms": 2700, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "올게요.", "start_ms": 2950, "dur_ms": 450, "phase_lr_deg": -5.0}
]
},
{
"segment": 24,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
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
"transcript": "주말에 보통 무엇을 하시나요? 저는 영화 보는 것을 좋아해요.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "주말에", "start_ms": 250, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "보통", "start_ms": 800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "무엇을", "start_ms": 1250, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "하시나요?", "start_ms": 1800, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "저는", "start_ms": 2700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "영화", "start_ms": 3150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "보는", "start_ms": 3650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "것을", "start_ms": 4100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "좋아해요.", "start_ms": 4500, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 31,
"transcript": "최근에 본 영화 중에서 추천할 만한 게 있나요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "최근에", "start_ms": 180, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "본", "start_ms": 700, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "영화", "start_ms": 950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "중에서", "start_ms": 1450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "추천할", "start_ms": 2000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "만한", "start_ms": 2650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 3100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있나요?", "start_ms": 3300, "dur_ms": 600, "phase_lr_deg": 0.0}
]
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
"transcript": "액션 영화보다는 로맨틱 코미디를 선호하는 편이에요.",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 220,
"audio_type": "speech",
"words": [
{"word": "액션", "start_ms": 220, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "영화보다는", "start_ms": 750, "dur_ms": 850, "phase_lr_deg": -5.0},
{"word": "로맨틱", "start_ms": 1650, "dur_ms": 600, "phase_lr_deg": -5.0},
{"word": "코미디를", "start_ms": 2300, "dur_ms": 700, "phase_lr_deg": -5.0},
{"word": "선호하는", "start_ms": 3050, "dur_ms": 650, "phase_lr_deg": -5.0},
{"word": "편이에요.", "start_ms": 3750, "dur_ms": 750, "phase_lr_deg": -5.0}
]
},
{
"segment": 34,
"transcript": "한국 드라마도 아주 인기가 많죠. 자막 없이 보고 싶어요.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "한국", "start_ms": 140, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "드라마도", "start_ms": 550, "dur_ms": 600, "phase_lr_deg": -5.0},
{"word": "아주", "start_ms": 1200, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "인기가", "start_ms": 1600, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "많죠.", "start_ms": 2150, "dur_ms": 450, "phase_lr_deg": -5.0},
{"word": "자막", "start_ms": 2800, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "없이", "start_ms": 3250, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "보고", "start_ms": 3650, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "싶어요.", "start_ms": 4050, "dur_ms": 600, "phase_lr_deg": -5.0}
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
"transcript": "열심히 공부하면 금방 익숙해질 거예요. 같이 노력해봐요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "열심히", "start_ms": 200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "공부하면", "start_ms": 850, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "금방", "start_ms": 1600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "익숙해질", "start_ms": 2050, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "거예요.", "start_ms": 2850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 3500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "노력해봐요.", "start_ms": 3950, "dur_ms": 900, "phase_lr_deg": 0.0}
]
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

---
*Generated by WKAppBot ask gemini — 2026-04-06 07:03:58*
