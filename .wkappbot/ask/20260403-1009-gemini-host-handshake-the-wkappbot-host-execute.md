# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=10:09:07  uptime=31793s  mem=1749MB  system_processes=449"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_100857.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 270-206-476-162-162-675-672-627-156-257-206-320-751-612-567-162-567-570-573-173-571-106-136-132-312-136-152-163-103-163-103-107-107-107-173-157-617-672-675-670-162-102-162-132-1_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 263-216-132-103-134-130-136-136-623-576-176-176-756-613-103-136-106-160-167-176-623 163-167-103-213-123-106-620-120-617_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 207-765-107-172-167-613-627-260-267-267-206-126-163-301-167-261-103-103-165-301-301-130-627-103-356-172-106-162-160-102-103-137-132-517-571-507-513-150-517-156-165-175-163-605-1_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 236-326-512-521-273-725-275-572-257-752-573-325-352-325-321-325-530-573-537-357-371-574-576-536-756-251-231-567-536-562-527-265-632-627-625-657-653-675-576-576-526-253-275-235-3_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 103-102-123-163 367-136-756-657-675-765-760-672-672-236-623-650-652-672-675-627-612-267-162-216-126-612-657-675-607-675-765-716-215-175-106-106-163-106-167-163-136-631-256-561-7_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 617-160-102-321-327-321-310-237-132-106-136-102-310-260-130-276-270-765-126-312-315-362-326-620-102-102-102-102-136-103-102-123-361-126-136-602-321-321-130-132-301-203-126-561-1_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 132-321-230-326-312-632-657-653-126-163-265-163-102-652-560-613-312 627-672-361-361-361-630-632-260-260-102-513-123-126-106-103-132-203-206-260-263-102-230-302-305-763-637-136-5_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 206-260-620-265-206-250-527-325-203-752-702-263-206-260-206-203-206-321-260-230-302-705-756-703-206-126-163-162-132-316-236-310-132-301-756-705-310-310-136-310-530-350-203-760-2_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 126-756-756-751-753-567-657-576-506-576-576-567-620-635-675-765-751-615 623-630 760-623-362-326-362 567-607-574-574-574-237-621-163-326-321-235-457-261-762-267-163-675-507-570-1_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 623-507-576-315-136-206-210-102-102-126-162-162-165-165-201-260-267-725-623-267-750-607-207-260-620-316-103-103-627-162-106-160-103-103-137-167-173-136-163-136-123-263-361-360-2_V.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 231-671-301-106-103-612-136-132-130-237-157_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 216-213 574-570-126-132 675-675-673-672-671-673-617 612-167-162-136 103-103-103-132-657-615-173-136-103-136-302-130-106-126-260-316-356-106-103-136-270-260-206-623-263-263-312-3_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 237-302-302-321-671-237-623-631-623-162-621-236-760-760-175-132-216-236-530-573-325-321-302-231-312-623-627-632-132-510-530-136-316-126-132-321-625-672-263-756-306-137-103-106-1_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 136-132-134-132-143-312-127-241-341-134-621-413-143-163-136-157-175-175-261-261-261-103-132-102-213-316 413-132-127-127-174-124-132-165-613-613-163-167-165-712-172-762-762-673-5_V.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 675-765-617-756-762-672-267-162-267-627-216-267-126-261-215-265-231-726-712-261-231-761-217-276-261-213-123-216-627-176-267-103-136-672-261-321 612-236-213-123-621_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 761-130-675-167-621-657-631-107-132-132-613-307-162-162-103-132-103-136-301-130-265-361-160-613-163-617-361-136-106-136-632-136-564-312-623-167-670-106-103-167-176-106-136-102-1_V.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 627-267-216-261-103-103 672 621-261-201-123-312-213-612-602-541-263-130-103-301-312-132-312-623-631-326-623-630-631-623-612-312-310-301-132-312-201-206-301-132-261-103-213-321-1_V.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 120-132-321-765-607-167-275-123-275-326-320-302-320-302-754-574-754-307-312-162-613-136-167-302-103-517-571-627-627-620-602-306-320-230-572-705-570-574-106-136-163-631-176-371-5_W.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 261-167-162-106-167-136-761-167-126-362-516-316-136-713-317-316_V.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 265-657-657-672-675-627-326-637-612-621-627-267-236-321-263-263-263-265-275-653-672-762-623-567-574-312-321-637-672-673-637_W.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 137 206-263-261-326-320-321-312-302-302-327-231-327-327-230-203-320-206-236-206-276-213-267-267-267-326-623-623-123-237-267-203-213-273-721-320-321-230-230-327-231-263-206-206-3_W.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 761-267-276-756-302-301-321-607-167 103-132 613-610-263-126-213-253-206-210-103-106-162-310-301-312-132-320-210-620-261-261-623-326-306-260-362-627-326-236-263-263-236-276-276-2_W.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 103-576-316-130-103-162-103-173-756-175-627-620-756-765-756-751-756-137-173-576-517-627-637-362-762-726-167-163-167-167-617-612-162-231-132-736-736-513-265-165 267-671 162_W.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 716-617-315-312-567-576-567-576-547-576-576-574-576-756-756-756-576-574-576-756-675 125-567-576-675-675-576-756-765-576-675-657-576-765-675-576-657-675-675-176-716-675-576-617-1_W.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 261-130-106-321-103-107-103-320-301-312-561-130-106-321-312-130-165-632-673-106-136-761-251-523-576-576-675-756-567-576-547-570-256-567-162-720-627-316-321-673-650-657-375-130-1_W.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 513-235-512-523-512-513-215-327-517-256-256-265-526-265-267-267-623-263-263-263-267-267-265-267-267-263-263-620-326-236-541-524-527-653-275-327-123-127-135-137-137-317-317-317-1_W.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 206-520-524-130-103-103-130-312-132-312-302-263-260-620-206-206-203-236-260-263-260 537-537-207-302-321-532-132-152-120-123-126-672-607-517-507-572-215-517-102-216-132-106-165-3_V.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 632-362-163-631-326-136-312-236-162-672-671-673-762-761-716-671-672-765-762-675-765-176-617-167-163-603-126-574-507-675-126-167-762-762-672-165-751-650-615-506-301-306-316-260-2_W.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 261-263-213-135-213-637-136-310-312-316-306-765-675-675-763-672-637-167-720-617-613-316-163-163-160-361-316-316-361-632-362-361-632-756-753-753-752-752-215-261-627-627-621-132-6_W.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 176-167-107-736-137-103-103-167-302-307-163-103-136-176-570-725-273-275-103-201-203-320-230-203-235-375-312-103-302-325-213-236-206-201-321-230-326-263-230-320-320-326-302-320-3_V.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 106-162-672-627-253-637-216-625-652-217-675-756-736-527-576-726-675-675-672-675-735-573-631-167-576-657-576-576-675 312-103-132-132-301-126-213-213-120-123-103-103-310-136-136-6_W.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 362-326-623-263-326-321-362-621-163-263-612-172-167-612-132-236-236-637-362-267-236-162-312-231-326-263-623-673-637-106-136-361-321-103-136-613-612-620-236-206-301-607-610-106-6_V.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 267-627-627-627-627-236-263-632-263-623-267-263-542-526-216-123-236-216-627-627-672-672-672-213-231-261-261-265-263-261-623-261-236-126-261-261-216-265-625-536-253-236-261-621-6_W.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 106-326-301-316-325-102-103-162-602-627-236-206-612-602-362-631-163-102-201-201-730-657-657-675-617-675-617-167-267-267-126-106-120-102-120-120-637-612-673-635-562-657-567-657-5_V.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 106-167-167-103-317-163-612-162-162-106-165-156-173-136-130-631-615-175-612-621-136-103-765-106-103-167-156-321-126-617-627-167-130-163-176-751-615-165-106-103-106-765-126-173-1_V.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 103-132-176-675-617-671-671-136-136-127-176-567-620-621-620-162-130-320-103-312-132-230-135-750-167-163-610-160-160-620-617-103-136-167-103-167-107-612-621-612-216-263-162-126-6_V.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 132-136-103-132 216-123-217-213 105-312-106 103-103 321-316-316-361-321-136-316 231-231-231-231-231-635-362-620-627-261-267-206-216-132-162-206-206-623-260-706-271-723-671-207-2_W.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 213-120-316-123-321-371-361-371-725-752-572-203-320-231-237-572-576-576-253-302-316-213-217-127-362-156-175-752-312-132-125-516-653-326-326-236-327-175-351-527-231-312-573-576-5_W.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 137-632-672-670-670-607-673-672-673-761-674-627-627-627-627-607-673-671-617-637-627-132-670-672-672-672-632-326-302-362-602-602-675-657-756-165-567-762-765-657-105-673-623-670-6_W.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 756-570-750-752-205-302-213-301-367-312-376-362-132-321-327-273-276-750-135-157-123-152-572-513-532-126-572-235-302-132-257-217-132-321-517-235-507-527-263-230-236-206-321-217-1_V.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 361-136-631-130-163-136-321-301-163-160-165-157-150-563-135-105-150-312-357-312-132-123-126-130-103-106-136-267-236-637-710-715-176-765-514-560-571-571-571-574 672-672-671-671-1_W.mp3)
  Segment 42: 64616ms ~ 77072ms (file: 162-167-367-362-627-621-317-726-765-136-756-175-176-176-137-713-167-167-136-132-137-316-230-320-132-312-312-632-201-162-153-236-216-216 627-672-632-765-765-621-267-201-203-231-2_W.mp3)
  Segment 43: 78072ms ~ 78648ms (file: 320-326-106-123-106-302-126-102-103-102-163-126-162-613-316-612-621-326-316-361-136-312-301-310-130-361-316-301-320-310-306 361-637-632-627-261-123-123-132-130-132-321-130-132-3_W.mp3)
  Segment 44: 79648ms ~ 80224ms (file: 762-761-176-126-216-162-132-126-137-123-261-213-120-123-123-120-123-132-213-312-612-123-312-671-673-765-673-762-673-632-160-672-673-267-672-326 163-625-675-637-760-132-312-136-1_W.mp3)
  Segment 45: 81224ms ~ 81800ms (file: 362-316-602-632-136-123-312-302-623-653-103-132-123-136-132-162-126-321-103-132-312-315-103-321-236-623-237-137-532-132-123-103-132-301-301-752-752-725-632-123-312-302-765-765-7_V.mp3)
  Segment 46: 82800ms ~ 83376ms (file: 167-765-507-657-671-605-576-657-651-612-360-621-361-265-162-105-603-563-605-612-612-106-567-506-105-605-160-651-560-657-657-617-136-675-637-657-162-621-625-652-651-657-670-106-7_W.mp3)
  Segment 47: 84376ms ~ 84952ms (file: 361-216-756-574-574-574-576-527-652-573-576-576-576-576-574-576-576 326-236-236-236-326-236-326-326-236-671-613-613-632-123-321-162-312-321-231-631-631-613-163-316-617-632-631-1_W.mp3)
  Segment 48: 85952ms ~ 86528ms (file: 716-163-762-765-760-675-672-607-312-326-360-263-362-613-321-103-136-106-312-302-530-503-356-657-631-361-260-602-312-632-326-631-263-231-320-206-231-302-302-321-326-306-302-236-3_V.mp3)
  Segment 49: 87528ms ~ 88104ms (file: 763 716-716-716-176-167-123-132 760-763_W.mp3)
  Segment 50: 89104ms ~ 89680ms (file: 576-657-672-675-617-231-726-756-756-765-672-675-567-675-675-765-675-756-576-657-765-756-756-765-671-670-671-756-756-576-765-756-675-675-756-576-576-657-765-675-675-756-756-675-7_W.mp3)
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
> **Time**: 2026-04-03 10:09:49
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
*Generated by WKAppBot ask gemini — 2026-04-03 10:09:49*
