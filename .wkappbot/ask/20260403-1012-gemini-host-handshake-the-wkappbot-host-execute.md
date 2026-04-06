# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=10:11:33  uptime=31939s  mem=1763MB  system_processes=447"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_101123.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 265-265-625-265-236-263-261-326-123-265-265-231-672-236-326-327-763-367-326-231-271-236-321-326-632-623-267-273-236-263-263-237-237-263-623-267-627-327-327-261-321-263-326-362-6_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 320-320-361-673-623-635-601-603-365-675-576-651-160-310-236-603-602-602-620-260-260-260-620-612-162-617-506-623-316-631-613-612-160-607-602-162-627-602-206-231-123-103-631-361-6_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 216-316-136-162-631-130-715-107 574-103-106-103-136-136-675-106-135-106-652-132-231-106-651-612-136-132-176-130-613-361-106-162-106-106-106-162-126-263-651-672-612-675-103-176-1_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 576-756-567-675-657-657-765-756-765-675-672-672-671-675-625-672-657-627-261-627-673-672-675-672-673-671-675-675-627-261 547-506-567-350-150-301-132-312-162-756-756-507-507-507-1_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 213-320-132-103-136-132-123-251-756-173-576-321-321-725-175-132-301-132-321-301-261-672-237-321-321-320-321-310-310-301-206-623-632-260-230-236-302-321-302-305-350-157-310-132-1_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 307-320-302-302-312-316-612-162-610-627-627-631-306-530-253-250-253-325-267-106-103-623-260-621-126-102-132-167-765-632-236-261-126-136-163-302-527-130-216-132-301-302-320-362-2_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 756-765-765-675-675-621-516-126-103-106-567-326-512-107-167-361-167-130-635-106-163-163-132-103-103-130-103-132-103-136-167-167-162-563-176-265-316-162-136-106-130-163-160-163-5_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 165-167-612-613-162 762-762-672-762-206-301-306-263-632-206-206-203-320-320-301-132-103-312-306-136-102-136-316-136-137-136-613-106-163-103-301 736-317-103-310-167-103 103-163-1_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 260-620-143-165-156-165-136-237-312-103-620-601-106-103-126-103-103-136-312-312-675 301-317 576-576-576-302-320-237-273-162-106-103-132-312-320-230-230-320-326-236-267-326-267-6_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 123-132-312-132-312-132-312-352-312-231-251-125-312-126-132-312-153-321-235-321-135-312-312-321-512-526-126-321-312-126-132-123-157-156-125-132-132-132-312-132-123-213-132-132-1_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 106-610-160-162-613 627-276-627-726-675-632-637_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 301-310-130-310-103-130-103-130-103-312-103-301-136-126-103-103-103-132-103-103_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 136 176-310-621-163-162-132-123-321-102-601-130-316-103-103-162-102-103-162-103-130-610-165-162-103-631-137-617-130-132-103-102-162-165-617-160-102-136-103-106-675-617-132-175-7_V.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 267-267-572-523-543-524-253-251-237-327-132-617-627-672-267-162-126-326-132-213-132-137-123 276-762-127-267-276-237-217-215-213-175-137-130-257-217-276-617-126-265-265-625-263-6_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 126-162-103-635-136-106-136-103-167-136-136-213-756-631 153-267-160-617-103-103-125-261-172-217-106-610-136-135-675-106-615-103-162-123-103-160-132-107-610-102-105-165-261-176-1_V.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 126 362-632-623-623-623-326-263-261-263-267-326-123-765-276-657 627-263-263-651-652-263-312-130-132-102-567-576-567-672-267-326-132-301-312-321-132-326-612-632-210-546-547-516-7_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 602-620-620-623-130-631-360-326-362-320-203-263-230-623-301-361-310-230-236-201-320-123-103-102-103-210-260-657-502-506-305-201-260-263-260-260-260-327-721-732-376-621-652-672-2_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 106-167-617-310-103-310-301-302-301-612-632-163-361-136-361-621-617-326-321-310-235-236-312-132-675-612-136-362-631-316-136-316-621-657-136-316-163-163-326-632-632-130-623-316-1_W.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 267-612-163-132-756-163-126-547-756-576-657-576-673-756-713-726-163-762-673-763-673-617-136-103-670-103-130-361-203-671-612-610-136-317-126-715-167-130-160-125-162-136-126-103-1_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 627-607-610-670-627-672-627-672-672-675-576-675 260-206-207-203-230-207-267-261-150-103-103-103-106-607-627-267-627-627-106-106-362-361-132-163-163-163-163-132-167-132-316-315-1_V.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 602-652-603-652-657-652-652-657-652-657-652-765-362-362-627-106-103-132-103-260-756-620-206-206-126-160-103-613-162-136-123-162-106-106-163-620-623-126-103-321-371-753-163-316-3_V.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 315-163-135-106-136-132-106-675-612-163-310-621-671-136-632-123-621-632-362-627-103-316-312-320-312-236-123-136-103-136-103-672-763-526-163-137-162-103-621-167-163-162-623-615-1_V.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 603 517-132-127-106-526-103-167-617-610-163-123-176-136-652-132-367-103-167-637-136-132-106-136-162-612-306-162-103-617-132-261-136-165 162-163-103-106-610-135-761-150-163-135-1_V.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 163-617-763-163-651-162-627-267-361-163-163-316-301-310 312-165-621-162-657-615-312 621-267-625 617-675-672-675-160-637-671-613_W.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 127-562-132-126-136-651-132-623 136-123-132-136-160-165-163-316-361-136-162-167-625-326-632-627-326-261-231-123_W.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 621-621-163-163-167-236-213-153-612-316-130-127-361-236-163-320-657-603-216-627-765-762-762-617-657-617-136-106-163-136-130-132-135-615-267-157-163-136-103-136-302-157-106-160-2_W.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 106-136-103-106-507-561-615 167-106-162-163-136-103-106-765-601-623-625-103-160-617-163-613-136-627-321-126-173-613-103-136-103-152-126-123-103-617-162-621-671-106-612-170-632-1_V.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 526-352-531-537-625-657-625-625-625-623-627-512-526-256-256-542-574-507-520-520-570-507-163-623-263-623-265-235-230-213-256-265-657-567-562-567-235-236-524-256-265-526-547-576-6_W.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 621-263-675-106-126-163-106-162-312-106-132-103-603-163-157-157-301-132-130-136-136-132-613-316-136-301-302-201-132-130-236-623-106-216-521-752-157-137-136-310-163-316-136-132-1_V.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 316-362-236-261-260-261-132-603-607-672-172-760-760-706-670-261-213-132-263-143-362-362-316-132-312-301 260-162-103-176-670-620-602-613-162-621-106-163-162-136-310-106-136-316-3_V.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 362-362-326-362-367-763-762-765-765-765-675-607-603-352-352-365-365-352-325-327-637-635-625-652-657-326-325-325-325-352-367-361-365-635-635-653-635-635-635-635-365-365-635-652-1_W.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 123-627-263-612-236-216-236-236-235-236-326-365-216-312-321-132-612-312-326-132-312-312-312-172-713-136-325-562-365-562-562-527-312-132-326-361-316-136-367-315-362-326-326-326-6_W.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 162-126-162 163-631-612 267-670-163-761_W.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 725-256-362-213-236-236-326-103-213-716-627-312-627-627-627 120 657-675-675-675-652-627-672-627-657-756-657-675-675-657-675-672-672-675-672-675-672-765-602-657-625-265-627-675-6_W.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 123-652-653-657-603-627-607-672-762-126-265-206-206-260-216-310-102-123-201-263-216-210-210-103-132-312-637-602-607-726-762-312-302-301-312-132-621-163-136-132-136-103-106-103-1_V.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 627-261-756-607-671-165-163-134-132-312-534-615-162-162-617-613-163-631-361-162-362-631-632-621-137-317-317-237-167-172-167-136_V.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 567-326-326-316-362-326-312-267-216-621-602-621-627-267-765-765-627-267-602-163-106-132-163-162-605-765-756-203-231-213-123-123-213-123-123-102-103-123-216-312-102-126-123-126-2_W.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 651-106-106-136-163-612-125-306-176-316-617-106-103-167-103-613-136-613-163-167-167-162-160-123-165-103-130-103-261-612-103-675_V.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 326-251-230-352-275-362-672-627-362-362-513-352-562-126-312-621-216-267-361-316-123-316-362-532-352-523-235-326-236-625-320-126-376-631-132-362-537-532-562-216-632-136-231-236-3_W.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 163-106-106-136-143-321-103-163-120-103-132-376-571-165-163-132-106-103-106-106-167-160-136-761-106-576-261-136-613-105-126-132-106-673-176-670-137-206-126-562-216-126-123-217 1_V.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 756-765-653-765-725-765-726-752-572-752-275_W.mp3)
  Segment 42: 64616ms ~ 66560ms (file: 627-726-761-765-765-673-651-362 612-613-621-361-621-632-126-265-236-263-625-263-263-267-612-627-612-620-106-132-163-106 562-152-103-312-126-675-716-132-267 450-571-312-602-362-7_W.mp3)
  Segment 43: 67560ms ~ 68136ms (file: 627-625-675-163-105-562-567-726-627-627-623-671-706-132-216 672-623-236-352-623-236-310-103-123-230-302-302 163-160-160-106-136-132-612-620-162-613-302-312-106-106 612-162-261-6_W.mp3)
  Segment 44: 69136ms ~ 69712ms (file: 106-107-136-312-670-103-153-106-103-106-163-671-617-135-102-627-107-237-613-651-765_V.mp3)
  Segment 45: 70712ms ~ 71288ms (file: 652-265-312-361-167-571-156-165-735-675-765-635-573-635-236-610-615-675-562-576-125-615-615-657-576-657-765-756 567-567-657-572-567-756-576-567-567-576-576-657-756-567-756-567-5_W.mp3)
  Segment 46: 72288ms ~ 72864ms (file: 136-631-176-670-261-621-163-612-132-123-514-576-756-760-165-567-167-561-263-651-615-261-362-123-316-367-130-106-301-163-617-106-105-106-160-165-136-516-103-126-167-160-136-103-1_V.mp3)
  Segment 47: 73864ms ~ 74440ms (file: 756-756-756-217-136-756-756-567-213-357-326-302-103-132-314-317-341-132-765-765-761-130-637-627-362-326-615-132-675-675-617-617-631-675-132-264-143-267-136-123-162-621-123-126-2_V.mp3)
  Segment 48: 75440ms ~ 76016ms (file: 132 310-130-123-127-301-576-576-762-162-126-123-625-625-673-672-673-671-263-213-206-623-672-623-362-621-263-263-627-261 673-167-631-613-236-621-163-162-236-627-236-132-623-631-1_W.mp3)
  Segment 49: 77016ms ~ 77592ms (file: 103-132-103-130-103-163-106-103-261-162-123-623-627-263-236-623-261-136-316-650-360-317-213-123-312 732-627-723-237-623-627-326-326-632-657 452-126_W.mp3)
  Segment 50: 78592ms ~ 79168ms (file: 627-627-310-621-672-107-107-172-675-675-672-627-162-216-706-765-706-673-167-167-163-706-607-760-173-175-165-134-132 726-175-132-152-153-125-312-610-610-136-136-167-136-213-206-2_V.mp3)
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
> **Time**: 2026-04-03 10:12:14
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 2,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 3,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 4,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 5,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
*Generated by WKAppBot ask gemini — 2026-04-03 10:12:14*
