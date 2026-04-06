# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=09:51:52  uptime=30758s  mem=1835MB  system_processes=447"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_095142.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 621-603-306-562-213-103-312-237-320-312-103-136-610-167-162-361-216-621-123-136-130-136-103-123-215-163-312-102-310-572-613-621-163-657-216-361-610-572-576-752-267-621-672-132-5_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 163-137-716-176-127-137-351-356-765_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 132-230-321-621-130-512-176-576-156-163-156-260-623-620-326-312-312-163-312-106-320-325-576-567-756-657-357-653-312-301-362-301-301-576-567-567-316-106-103-106-102-162-130-320-1_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 132-132-127-167-136-613-106-126-163-316-103-126-103-136-136-316-136-136-607-671-276-103-310-213-123-136-136-516-561-657-165-106-132-103-132-320-163-315-236-261-326-136-103-136-3_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 231-236-231-231-236-263-236-231-236-263-321-261-231-236-326-216-231-321-236-231-236-216-123-136-312-123-213-276-217-236-231-236-256-521-523-217-216-231-216-231-236-231-216-236-2_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 625-621-672-621-126-627-621-216-236_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 167-367-671-257-267-612-163-310-751-756-765-670-617-617-163-756-671-617-675-751-675-657-657-675-572-756-576-756-756-576-756-756-576-756-574-576-756-574-756-756-574-567 765-672-6_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 652-657-576-102-106-160-136-136-621-106-160-612-160-167-136-167-103-130-310-103-651-152-310-130-315-361-136-103-160-137-167-610-165-316-162-167-136-217-102-213-132-103-231-213-1_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 765-765-675-672-576-567-675-675-675-176-756-725-632-672-675-675-756-756-576-675-576-576-567-756-576-567-576-756-765-675-267-675-163-136-657-567-672-675-765-672-675-765-675-675-7_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 321-231-523-532-135-615-657-670-236-236-632-267-627-267-627-312-321-123-253-521-526-256-263-236-267-276-267-321-321-326-356-563-526-562-562-561-216-216-275-265-236-261-621-267-6_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 567-576-576-675-107-617-653-165-602-672-216-102-106-102-136-316-760-760-760-107-130-136-136-162-163 612-657-567-574-573-756-756-765-756-341-627-632-547-574-756-756-760 761-761-1_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 123-231-213-213-231-231-213-231-532-523-527-512-512-235-251-251-152-126-162-516-576-625-265-265-265-265-265-265-260-265-265-265-265-261-126-216-265-261-126-162-675-617-617-126-1_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 132-203-163-175-317-301-132-716-137-371-537-413-132-160-175-625-236-675-574-175-716-205-752 571-176 576-127-136-675-126-751-263-216-715-276-671-765-713_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 571-537-571-573-572-572-576-753-753-725-574-532-561-513-326-132-176-167-137-752-273-752-725-532-235-263-261-236-576-156-571-516-567-761-713-176-157-156-531-156-316-365-563-567-6_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 576-105-103-153-156-576-756-765-765-756-765-567-576-574-576-576-574-576-567-576-567-567-756 123-231-216 312-176-621-623-132 617-316-162-675-126-162-257-561-103-632-136-613-617-1_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 317-372-357-315 265 263-162-263-231-216-312-261-126-165-265-216-265-265-621-610-165-126-621-162-261-261-265-625-625-265-261-216-326-213-326-361-362-351-356-351-251-152-153-154-1_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 126-306-267-602-103-136-127-156-321-103-756-167-160-163-137-675-356-152-106-106-106-127-127-103-103-106-172-103-103-103-106-106-106-126-260-127-126-106 173-103-165-576-103-167-1_V.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 231-320-230-312-321-321-321-320-132-103-231-726-736-327-362-320-321-132-312-135-150-310-130-316-310-306-136-106-103-123-103-231-302-213-126-132-312-162-136-312-162-261-216-267-7_W.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 267-276-276-726-627-652-625-652-526-652-261-132-362-326-321-216-231-327-317-126-132-132-123-132-312-317-132-132-132-136-217-162-126-162-136-163-136-126-127-127-137-316-361-361-3_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 103-106-165-162-561-612-106-316-317-136-365-132-610-132-316-130-326-162-132-162-130-321-617-316-163-136-163-136-316-167-630-126-163-162-631-136-167-132-165-130-130-571-576-715-7_V.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 123-162-261-136-123-210-236-236-103-132-106-316-312-167-627-213-312_W.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 236-326-236-261-263-217-267-267-261-261-267-261-276-236-326-326-236-326-326-326-235-327-327-237-321-237-326-321-312-326-362-326-372-362-317-376-362-321-312-326-321-326-326-362-3_W.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 627-176-163-132-136-631-623-623-621-263-267-263-267-627-627-620-263-765-765-765-762 132-126-103-301-136-106-106-106 305-736-671-765-675-576-517-531-573-537-236-253-213-132-325-3_W.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 106-163-136-106-103-106-631-132-160-157-310-165-163-106-651-163-316-316-316-106-136-103-106-130-165-610-156-213-326_V.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 613-617-720-670-630-276-267-362-236-263-623-627-675-672-670-263-672-267-312-103-134-130-302-310-312-315-327-635-263-231-237-627-620-623-637-673-360-327-312-572-573-657-632-132-3_V.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 601-107-170-175-175-576-517-576-576-576-571-517-130-103-132-632-263-625-623-621-162-612-162-576-573-576-756-573-163-513-572-657-657-165-651-657-617-601-637-261-106-167-167-316-6_W.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 320-321-601-632-263-206-612-263-260-602-206-306-320-230-230-260-162-103-621-602-602-603-260-320-301-756-765-670-267-106-236-126-216-203-270-625-625-610-620-260-612-210-761-167-1_V.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 537-352-352-537-573-574-571-562-561-561-156-567-563-576-235-321-321-312-675-617-167-265-572-572-576-567-562-672-627-632-236-625-261-627-621-267-267-261-632-621-267-263-261-263-2_W.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 132-130-123-627-213-623-621-621-627-627-267-621-261-726-126-675-726-765-705-762-572-725-165-137-367-162-671-163-136-103-316-106-152-132-253-621-152-162-136-672-631-163-106-103-5_V.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 623-632-632-263-267-263-623-263-267-236-362-263-326-236-521-523-527-352-562-253-235-652-625-526-562-256-365-625-623-576-756-672-612-621-267-261-265-216-265-271-572-572-517-512-1_W.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 627-672-673-671-671-567-567-265-165-215-315-513-657-657-213-312-321-127-712-761-763-762-312-132-213-176-671-761-617-123-126-321-765-267-317-132-312-312-567-527-263-261-236-627-6_W.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 216-206-561-671-160-165-605-106-162-167-106-135-607-570-510-106-107-106-167-217-621 573-572-276-263-326-672-657-657-657-762-672-267-623-130-130-312-103-312-675-675-675-765-675-6_W.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 213-265-236-263-216-132-231-213-312-312-265-263-236-265-256-263-231-253-215-236-123-251-532-261-216-253-123-235-251-235-123-216-256-215-231-215-521-526-526-652-257-265-256-625-6_W.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 576-507-570-256-610-613-163-106-126-126-160-106-162-167-107-106-210-261-103-623-236-261-623-310-326-260-260-206-162-162-160-106-160-160-267-603-106-106-167-612-261-206-603-160-1_W.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 726-372-271-267-267-367-675-627-126-627-613-762-672-675-675-672-627-657-657-657-657-650-657-657-657-675-675-160-130-675-567-307-376-765-657-106-102-102-356-673-536-657-526-657-3_W.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 273-726-276-726-261-132-160-710-617-762-672-612-613-162-106-106-675-130-162-167-361-613-163-136-103-621-721-263-136-106-136-312-310-516-312 163-160-130-165-132-617-165-651-157-3_V.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 124-123 751-750-756-765-765-103-176-157-137-316-613-312-136-157-213-102-216-231-126-130-163-136-103-103-132-126-301-301-132-231-216-310-321-310-130-132-132-362-321-312-136-132-1_V.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 263 312-312-357-637-317-132-213-326-316-321-123-132-102-301-312-132-231 576-567-567-567-673-756-562 576-573-132-751-756-751-762-267 132-103 631-613 574-576-756-765-765_W.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 257-275-542-352-357-351-534-537-375-357-574-570-576-572-537-321-576-762-752-753-576-265-627-625-632-625-632-625-627-567-652-352-326-541-251-520-574-570-357-571-315-531-357-237-5_W.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 312-132-103-306-326-206-320-230-236-326-320-230-320-326-321-306-320-321-320-302-312-321-236-312-326-326-316-136-312-321-206-132-321-326-326-231-361-321-320-320-321-213-321-312-3_W.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 326 621-673-672-765 632-623-216-761-763-672-361-312-574-570-576-527-526-617-672-672-163-615-361_W.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 130 567-567-567-536-576-576-567-576-576-570-570-506-352-136-312-321_W.mp3)
  Segment 43: 66192ms ~ 66768ms (file: 653-617-612-671-625-562-326-367-123-152-167-174-671-617-765-673-531-352-563-365-365-253-326-276-726-217-271-763-132-127-673-673-617-371-172-675-567-271-173-623-613-637-673-673-6_W.mp3)
  Segment 44: 67768ms ~ 68344ms (file: 260-261-263-260-621-623-623-123-231-132-631-163-136-623-621-263-267-267-267-267-267-267-260-624 321-132-312-123-326-301-670-627-762-637-672-263-236-103-312-102-103-326-236-326-2_W.mp3)
  Segment 45: 69344ms ~ 69920ms (file: 102-103-310-126-106-320-546-567-615-261-301-136-316-136-362-321-103-132-160-132-310-136-632-326-320-312-132-312-361-103-132-312-206-102-612-316-621-263-103-103-317-130-103-310-1_V.mp3)
  Segment 46: 70920ms ~ 71496ms (file: 260-527-602-230-265-623-632-612-631-632-632-653-657-651-365-126-236-316-631-612-623-103-176-765-625-673-267-261-261-267-256-256-526-562-547-526-576-567-576-576-567-567-576-574-5_W.mp3)
  Segment 47: 72496ms ~ 73072ms (file: 362-613-671-623-362-613-615-567-560-657-652-326-236-620-625-602-627-670-671-601-106-106-136-560-651-265-136-312-132-301-132-276-673-132-106-167-752-753-713-567-657-602-521-317-1_V.mp3)
  Segment 48: 74072ms ~ 74648ms (file: 254-256-256-256-256-572-576-265-265-265-325-326-326-325-326-326-326-352-357-376-236-267-236-263-365-263-263-362-263-362-361-362-632-367-632-351-356-356-357-351-541-154-154-571-3_W.mp3)
  Segment 49: 75648ms ~ 76224ms (file: 167-164-167-167-167-132-143-203-502-125-102-126-132-317-703-760-507-103-103-123-132-154-132-103-136-301-103-136-132-103-134-103-132-132-132-504-540-574 216-267-206-267-237-260-2_V.mp3)
  Segment 50: 77224ms ~ 77800ms (file: 167-136-103-261-316 136-162-107-163-615-627-675 561-120-672-126-160-136-106-136-136-136-617-316-103-165-136-103-562-261-615-126-106-102-321-163-126-276-167-627-136-163-671-306-7_V.mp3)
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
> **Time**: 2026-04-03 09:52:36
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
*Generated by WKAppBot ask gemini — 2026-04-03 09:52:36*
