# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=14524  time=05:39:14  uptime=6944s  mem=1894MB  system_processes=833"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260401_053731.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 132-263-312-301-360-613-301-312-162-360-237-362-326-206-267-267-236-326-236-756-576-315-106-301-306-360-367-136-607-657-605 263-263-752-756-756-705-735-756-576-576-576 671-762-2_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 103-162-130-316-103-103-132-102-176-162-617-163-316-163-132-103-136-106-162-163-136-236 103-615-506-576-567-657-576-657-567-657-651-657-162-573-576-130-106-103-160-165-163-316-1_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 231-321-261-316-361-165-135-351-356-316-136-135-135-265-231-125-213-215-126-265-165-563-153-135-135-156-572-253-125-265-216-261-265-275-267-157-153-531-351-351-315-531-351-371-5_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 160-163-632-621-126-126-123-321-132-312-132-761-671-326-263-627-261-623-201-103-132-103-132-136-102-310-310-132-301-231-326-231-263-326-504-506-503-312-301-302-310-301-310-361-3_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 126-136-102-127-103-163-675-216-167-167-102-617-621-106-167_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 671-106-607-361-615-162-160-106-123-607-160-761-167-546-165-167-127-267-362-106-135-106-613-162-167-705-765-756-756-675-673-316-176-316-123-170-321-310-326-532-650-574-576-576-7_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 527-620-602-602-106-163-602-153-156-651-657-615-516-652-651-562-563-561-561-356-615-657-652-657-567-567-657-657-651-517-567-561-561-165-567-561-156-165-651-263-231-253-256-652-6_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 627-136-631-310-657-265-526-627-362-132-312-521-267-267-231-623-621-623-627-267-326-263-263-623-612-672-627-672-625-637-632-362-362-326-623-625-536-635-632-263-675-627-672-637-6_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 213-213-123-132-123-127-162-617-126-526-265-562-651-672-621-326-263-236-231-126-102-103-132-175-103-762-275-267-267-675-637-627-672 576-267-762-613-106-567-135-317-637-623-621-3_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 562-652-625-562-652-576-576-571-567-516-576-574-574-573-567-657-576-576-574-563-365-356-532-523-561-156-561-567-516-315-571-315-315-123-163-253-265-251-351-573-657-267-267-265-6_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 167-745-751-765-361-136-136-103-267-263-217-261-127_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 756-576-573-537 627-627-627-627-547-574_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 672-103-630-236-236-321-213-320-352-365-126-612-263-236-267-123-213-235-236-526-625-265-236-230-231-256-573-563-735-725-672-362-621-263-632-326-201-236-675-632-326-362-326-302-2_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 765-671-371-765-675-631-613-160-567-653-710-136-716-715-765-576-657-617-312-716-756-657-756-167-671-216-635-136-316-163-160-675-576-635-675-675-672 762-267-126-631-567-652-627-6_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 675-107-201-625-702-721-137-123-173-271-762-762-672-765-726-725-257-756-572-576-526-576-572-576-574-567-567-562-567-175-160-765-762-672-265-165-762-152-723-126-260-312-136-103-1_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 102-132-301-102-106-132-123-162-106-106-167-751-132-126-517 163-160-172-621-602-126-132-106-175-165-167-167-123-127-176-620-625-613-613-163-106-106-136-126-715-754-156-701-312-1_V.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 136-132-132-103-132-163-123-106-136-613-162-132_V.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 321-136-167-163-316-316-156-652-265-612-612-163-164-162-621-621-162-162-621-162-621-652-365-361-316-316-631-326-365-516-514-514-512-514-216-562-526-562-612-621-613-615-615-563-5_W.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 265-236-562-265-267-235-251-256-256-627-265-265-625-265-267-567-357-315-153-513-530-534-534-501-570-657-627-632-256-256-526-526-256-532-352-351-325-352-352-325-235-231-263-265-2_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 312-206-236-206-320-267-276-312-320-230-723-230-236-726-203-623-320-307-237-327-670-267-260-102-637-123-623-103-627-260-120-620-627-627-312-132-123-273-362-236-620-230-237-632-3_W.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 672-612-672-627-672-637-762-627-765-673-756-716-236-576-675-672-672-760-760-672-672-672-672-623-637-672-632-672-637-627-632-134-314-143-362-261-632-627-362-263-672-671-602-675-6_W.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 513-536-536-536-563-523-562-153-153-135-315-135-536-531-653-567-157-135-513-651-351-153-356-356-513-517-567-516-315-136-653-563-657-351-516-576-561-536-531-567-567-576-571-536-5_W.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 631-612-617-617-167-617-621-612-612-654-561-365-362-362-623-637-361-367-367-623-263-623-627-267-627-627-623-263-672-617-263-263-263-263-263-263-263-123-163-312-361-567-651-657-6_W.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 126-316-103-136-326-213-120-130-671-176-103-263-216-261-765-132-263-120-130-136-176-231-216-361-163-103-126-103-132-312-126-612-216-160-302-326-301-310-310-367-103-163-605-173-1_V.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 326-236-236-275-273-526-725-572-652-273-236-231-231-213-325-326-153-215-261-236-261-267-235-357-275-253-231-213-261-627-132-321-231-231-321-312-213-325-231-352-325-235-326-236-2_W.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 164-173-710-750-571-576-267-627-217-632-136-617-136-620-341-312-132-314-132-413-235-750-705-723-312-357-321-726-372-312-312-327-324-321-713-132-327-321-756-756-750-756-321-312-3_V.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 136-316-312-132-213-206-263-260-263-216_V.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 567-657-526-526-516-152-562-106-125-625-251-651-760-762 762-763-263-621-675-276-163-157 547 574-562-267-267-236-621-627 326-312-123-136-132-267-265-632-310 263-165-103-136-103-3_W.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 541-567-567-675-672-276-726-276-576-765-326-623-316-543-543-534-265-236-260-613-261-261-627-267-263-326-534-532-562-623-632-672-675-671-652-675-672-765-762-637-672-756-756-275-2_W.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 671-103-106-103-132-126-103-610-607-103-132-315-207-167-567-136-167-751-123-627-126-762-760-175-176-160 176-632-162-760-164-130-163-106-136-310-156-167-670-136-362-136-671-173-5_V.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 136-361-216-136-167-136-103-163-617-316-671-162-615-176-602-162-263-103-167-136-367-526-675-132-136-130-165-163-136-163-103-751-165-751-120-210-126-612-623-165-316-631-136-263-1_V.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 260-260-260-276-206-526-627-672-267-267-213-361-560-561-165 130-310-132-756-670-162-163-312-201-627-213-261-213-123-231-203-320-362-627-320-132-210-206-620-206-263-206-206-261-2_W.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 260-602-620-602-756-765-765-765-260-263-260-206-632-236-632-630 316-670-673-132-162-261-312-352-307-302-310-103-312-302-362-302-321 267-260-162-126-102-517-517-310-301-302-302-6_V.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 167-163-103-132 123-123-123-763-762-763-762-765-762-763-763 675-765-765-657-567-143-620-123-672-263-671-136-657-326-132-765-765-765_W.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 716-130-136-130-612-123-316-103-107-137-312-132-106-163-163-163-627-263 132-132-316-126-321-130-130-103-132-130-103-231-321-672-627 126-126 632-362-263 130-672-362-213-163-312 1_V.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 576-675-765-756-756-571-547-574-574-574-576-576-765-657-573-576-756-675-756-265-657-675-576-576-576-756-567-756-367-765-657-576-547-675-567-653-265-625-564-756-567-576-675-675-5_W.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 137-671-136-657-126-632-362-612-123-163-103-136-132-716-136-261-126-132-106-106-103-765-623-612-620-216-637_V.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 623-316-317-301-132-163-316-762-176-675-671-106-167-132-143-103-130-126-132-310-130-130-132-310-163-137-136-701-126-612-760-602-763-675-136-163-501-705-715-627-627-621-607-627-6_V.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 136-163-135-103-105 132-136-106-162-137-132-103-136-136-106-316-163-316-615-651-231-671-106-312-106-162-163-102-132-136-360-671-162-132-263 756-126-267_V.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 576-756-756-761-761-571-153-735-763-765-756-756-576-576-137-576-576-675_W.mp3)
  Segment 41: 63040ms ~ 65344ms (file: 567-576-237-276-315 576-603-312-132-756-507-163-176-162-617-256-265-613-314-652-126-162-361-136-276_V.mp3)
  Segment 42: 66344ms ~ 66920ms (file: 367-675-362-367-376-317-326-273-276-762-267-257-721-372-361-761-371-673-367-673-627-625-621-621-623-625-627-623-265-265-625-265-265-625-261-326-263-564-564-540-561-256-267-276-2_W.mp3)
  Segment 43: 67920ms ~ 68496ms (file: 375-537-321-572-576-574 675-756-765-572-623 657-563-765-267_W.mp3)
  Segment 44: 69496ms ~ 70072ms (file: 362 231-623-126-130-103-132-316-163-106-103-106-160-130-301-316-136-236-312-126-570-570-765-762-765-761-267-206-216-761-702-617-107-176-317-623-621-620-621-267-673-167-167-162-6_V.mp3)
  Segment 45: 71072ms ~ 71648ms (file: 302-103-361-321-302-302-321-302-321-320-765-173-362-657-576-756-273-623-603-163-103-751-756-760-675-617-617-627-162-613-361-603-613-601-613-163-102-106-136-162-106-123-327-362-6_V.mp3)
  Segment 46: 72648ms ~ 73224ms (file: 610-675-156-367-601-160-601-601-610-617-167-167-136-107-710-760-761-627-163-106-163-136-316-326-627-673-362-163-136-163-631-316-301-316-167-167-136-310-136-675-612-360-106-130-3_V.mp3)
  Segment 47: 74224ms ~ 74800ms (file: 365-623-613-136-534-657-167-715-675-657-631-105 547-574-574-574-576-765-675_W.mp3)
  Segment 48: 75800ms ~ 76376ms (file: 236-672-203-162-621-627-675-605-260-312-102-103-107-103-176-320-230-236-312-173-673-103-617-567-106-231-623-623-613-632-650-765-167-162-623-672-132-546-567-632-216-621-261-263-2_W.mp3)
  Segment 49: 77376ms ~ 77952ms (file: 627-136-316-310-302-310-310-632-360-672-672-627-612-632-321-620 512-567-362-620-620-601-612-162-613-617-613-620-260-263-301-301-163-310-361-301-731-617-306-106-103-301-301-316-2_V.mp3)
  Segment 50: 78952ms ~ 79528ms (file: 612-106-103-163-165-162-132-326-312-132-163-102-132-163-136-132-231-621-617-627-137-123-310-106-167-617-167-136-673-603-301-316-312-301-103-103-106-136-106-162-167-601-617-765-7_V.mp3)
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
> **Time**: 2026-04-01 05:40:37
> **AI**: gemini

---

## Response

AnalysisAnalysisGemini의 응답[
{
"segment": 1,
"transcript": "안녕, 오늘 기분 어때?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 105,
"audio_type": "speech",
"words": [
{"word": "안녕,", "start_ms": 105, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 320, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "기분", "start_ms": 435, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "어때?", "start_ms": 520, "dur_ms": 40, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "Hello, how are you today?",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "Hello,", "start_ms": 80, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "how", "start_ms": 240, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "are", "start_ms": 335, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 400, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "today?", "start_ms": 475, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "날씨가 정말 좋네요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "날씨가", "start_ms": 120, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 330, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "좋네요.", "start_ms": 460, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "The weather is so nice.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 95,
"audio_type": "speech",
"words": [
{"word": "The", "start_ms": 95, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "weather", "start_ms": 160, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 310, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "so", "start_ms": 370, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "nice.", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "배고파요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "배고파요.", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "I am hungry.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 110, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "am", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "hungry.", "start_ms": 310, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "점심 뭐 먹을까요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "점심", "start_ms": 130, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 290, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "먹을까요?", "start_ms": 370, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "What should we have for lunch?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "What", "start_ms": 90, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "should", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "we", "start_ms": 290, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "have", "start_ms": 360, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "for", "start_ms": 440, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "lunch?", "start_ms": 510, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "피자 어때요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "피자", "start_ms": 140, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "어때요?", "start_ms": 330, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "How about pizza?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 115,
"audio_type": "speech",
"words": [
{"word": "How", "start_ms": 115, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "about", "start_ms": 245, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "pizza?", "start_ms": 405, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "좋아요, 갑시다!",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 160,
"audio_type": "speech",
"words": [
{"word": "좋아요,", "start_ms": 160, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "갑시다!", "start_ms": 370, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "Sounds good, let's go!",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "Sounds", "start_ms": 100, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "good,", "start_ms": 230, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "let's", "start_ms": 330, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "go!", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "어디로 가야 하나요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 125,
"audio_type": "speech",
"words": [
{"word": "어디로", "start_ms": 125, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가야", "start_ms": 285, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "하나요?", "start_ms": 415, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "Where should we go?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "Where", "start_ms": 90, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "should", "start_ms": 230, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "we", "start_ms": 340, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "go?", "start_ms": 410, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "지하철역 근처에 있어요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "지하철역", "start_ms": 110, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "근처에", "start_ms": 360, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "있어요.", "start_ms": 480, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "It's near the subway station.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 95,
"audio_type": "speech",
"words": [
{"word": "It's", "start_ms": 95, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "near", "start_ms": 175, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 295, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "subway", "start_ms": 365, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "station.", "start_ms": 485, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "걸어서 갈 수 있나요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "걸어서", "start_ms": 140, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "갈", "start_ms": 330, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 410, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "있나요?", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "Can we walk there?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "Can", "start_ms": 100, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "we", "start_ms": 200, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "walk", "start_ms": 280, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "there?", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "네, 5분 정도 걸려요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 130, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "5분", "start_ms": 210, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "정도", "start_ms": 340, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "걸려요.", "start_ms": 450, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "Yes, it takes about five minutes.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "Yes,", "start_ms": 110, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "it", "start_ms": 210, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "takes", "start_ms": 270, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "about", "start_ms": 370, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "five", "start_ms": 440, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "minutes.", "start_ms": 520, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "지금 몇 시예요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 145,
"audio_type": "speech",
"words": [
{"word": "지금", "start_ms": 145, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "몇", "start_ms": 265, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "시예요?", "start_ms": 335, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "What time is it now?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 105,
"audio_type": "speech",
"words": [
{"word": "What", "start_ms": 105, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "time", "start_ms": 195, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 305, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "it", "start_ms": 365, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "now?", "start_ms": 435, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "12시 반입니다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "12시", "start_ms": 130, "dur_ms": 190, "phase_lr_deg": 0.0},
{"word": "반입니다.", "start_ms": 330, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "It's half past twelve.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 115,
"audio_type": "speech",
"words": [
{"word": "It's", "start_ms": 115, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "half", "start_ms": 205, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "past", "start_ms": 305, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "twelve.", "start_ms": 425, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "오늘 무슨 요일이에요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 140, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "무슨", "start_ms": 250, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "요일이에요?", "start_ms": 350, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "What day is it today?",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "What", "start_ms": 110, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "day", "start_ms": 200, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 300, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "it", "start_ms": 360, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "today?", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "수요일입니다.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 160,
"audio_type": "speech",
"words": [
{"word": "수요일입니다.", "start_ms": 160, "dur_ms": 410, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "It's Wednesday.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "It's", "start_ms": 120, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "Wednesday.", "start_ms": 240, "dur_ms": 330, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "어제는 뭘 했나요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 135,
"audio_type": "speech",
"words": [
{"word": "어제는", "start_ms": 135, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "뭘", "start_ms": 285, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "했나요?", "start_ms": 365, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "What did you do yesterday?",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 105,
"audio_type": "speech",
"words": [
{"word": "What", "start_ms": 105, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "did", "start_ms": 195, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 275, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "do", "start_ms": 345, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "yesterday?", "start_ms": 445, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "친구를 만났어요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "친구를", "start_ms": 150, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "만났어요.", "start_ms": 340, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "I met my friend.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 115,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 115, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "met", "start_ms": 185, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "my", "start_ms": 305, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "friend.", "start_ms": 385, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "영화도 봤나요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "영화도", "start_ms": 140, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "봤나요?", "start_ms": 330, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "Did you watch a movie too?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "Did", "start_ms": 100, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 180, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "watch", "start_ms": 250, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 370, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "movie", "start_ms": 420, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "too?", "start_ms": 550, "dur_ms": 20, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "아니요, 카페에 갔어요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 155,
"audio_type": "speech",
"words": [
{"word": "아니요,", "start_ms": 155, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "카페에", "start_ms": 315, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "갔어요.", "start_ms": 455, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "No, we went to a cafe.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "No,", "start_ms": 110, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "we", "start_ms": 200, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "went", "start_ms": 270, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 380, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 440, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "cafe.", "start_ms": 490, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "커피 마셨나요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 145,
"audio_type": "speech",
"words": [
{"word": "커피", "start_ms": 145, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "마셨나요?", "start_ms": 305, "dur_ms": 260, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "Did you drink coffee?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "Did", "start_ms": 100, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 180, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "drink", "start_ms": 250, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "coffee?", "start_ms": 370, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "네, 저는 라떼를 마셨어요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 150, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "저는", "start_ms": 220, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "라떼를", "start_ms": 320, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "마셨어요.", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "Yes, I drank a latte.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 115,
"audio_type": "speech",
"words": [
{"word": "Yes,", "start_ms": 115, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 195, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "drank", "start_ms": 265, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 385, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "latte.", "start_ms": 435, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 42,
"transcript": "주말에 뭐 할 거예요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "주말에", "start_ms": 130, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 280, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 360, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "거예요?", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "What will you do this weekend?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 95,
"audio_type": "speech",
"words": [
{"word": "What", "start_ms": 95, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "will", "start_ms": 185, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 265, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "do", "start_ms": 335, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "this", "start_ms": 425, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "weekend?", "start_ms": 495, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "등산을 가려고 해요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 145,
"audio_type": "speech",
"words": [
{"word": "등산을", "start_ms": 145, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "가려고", "start_ms": 335, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "해요.", "start_ms": 475, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "I'm going to go hiking.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "I'm", "start_ms": 110, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "going", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 310, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "go", "start_ms": 370, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "hiking.", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "어느 산으로 가나요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "어느", "start_ms": 140, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "산으로", "start_ms": 250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가나요?", "start_ms": 410, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "Which mountain are you going to?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 95,
"audio_type": "speech",
"words": [
{"word": "Which", "start_ms": 95, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "mountain", "start_ms": 215, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "are", "start_ms": 365, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 425, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "going", "start_ms": 495, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "to?", "start_ms": 565, "dur_ms": 10, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "북한산에 가려고 해요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "북한산에", "start_ms": 150, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "가려고", "start_ms": 380, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "해요.", "start_ms": 510, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "I'm going to Bukhansan.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "I'm", "start_ms": 110, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "going", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 310, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "Bukhansan.", "start_ms": 370, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "즐거운 시간 보내세요!",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "즐거운", "start_ms": 140, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "시간", "start_ms": 310, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "보내세요!", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-01 05:40:37*
