# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=10:02:41  uptime=31406s  mem=1946MB  system_processes=445"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_100231.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 2304ms (file: 231-213-321-125-126-206-326-456-654-206-126-130-163-623-103-132-132-126-231-261-216-125-321-163-651-105-136-103-132-326-123-612-102-135-267-126-176-102-107-756-132-652-162-130-1_V_stt_the_middle.mp3)
  Segment 2: 3304ms ~ 3880ms (file: 572-765-672-712-576-765-172-715-176-672 765-672-371-671-267-572-756-675-672-765-752-726-671-176-376-327-276-761-316-132-712-731-123-126-127-257-752-271-321-362-621-625-265-273-2_W.mp3)
  Segment 3: 4880ms ~ 5456ms (file: 512-563-357-365-137-710-317-367-310-637-675-165-631-135-167-175-317-371-135-156-567-561-561-356-615-671-637-167-527-651-136-261-261-267-267-627-267-621-621-621-621-621-260-620-6_W.mp3)
  Segment 4: 6456ms ~ 7032ms (file: 765-163-326-236-261-716-301-307-107-163-126-123-213-126-167-176-176-736-716-167-613-203-203-216-263-206-231-306-106-316-512-153-531-351-317-316-675-612-361-306-320-325-675-756-7_W.mp3)
  Segment 5: 8032ms ~ 8608ms (file: 143-126-125-230-302-230-263-236-263-632-321-163-362-623-610-260-612-371-317-312-317-312-231-317-103-163-312-132-312-703-321-732-321-137-201-231-572-263-362-120-321-312-132-125-2_V.mp3)
  Segment 6: 9608ms ~ 10184ms (file: 627 632-362-632-362-632-362-612-672-367-632-623-637-632-362-623-263-623-627-623-362-326-632-263-623-621-632-621-326-632-263-632-637-362-627-621-362-326-263-316-361-362-312-321-2_W.mp3)
  Segment 7: 11184ms ~ 11760ms (file: 263-261-627-627-627-261-267-267-256-256-260-326-763-567-657-567-562-625-265-261-267-267-267-267-267-267-276-263-261-263-260 132-213-231-213-132-241-231-213-214-241-132 564-516-5_W.mp3)
  Segment 8: 12760ms ~ 13336ms (file: 263-236-261-263-621-236-263-263-236-627-621-627-612-263-267-267-236-263-216-126-672-267-562-652-261-267-106-126-276-765-612-612-657-627-621-265-625 130-607-561-627-621-610-310-6_W.mp3)
  Segment 9: 14336ms ~ 14912ms (file: 573-543-540-317-321-132-315-312-231-326-327-376-376-731-376-701-756-673-671-617-576-637-263-623-623-623-621-263-263-267-260-263-263-263-263-263-526-562-526-325-236-260-627-620-6_W.mp3)
  Segment 10: 15912ms ~ 16488ms (file: 216-163-316-261-261-623-623-623-657-367-627-612-613-612-163-213-261-275-546-536-527-276-237-236-276-256-526-256-267-276-265-265-265-261-267-261-236-236-236-527-256-263-623-625-2_W.mp3)
  Segment 11: 17488ms ~ 18064ms (file: 620-263-306-352-526-251-205-312-761-574-571-572-507-517-261-627-132-750-756-132-176-137-132-670-621-162-132-756-756-756-130-302-160-706-701-765-765-106-167-617-107-607-675-627-6_V.mp3)
  Segment 12: 19064ms ~ 19640ms (file: 103-123-362-162-126-632-627-267-163-143 103-132-316-136-607-675-675-607-673-367-627-623-645-627-621-513-165-162-163-613-136-167-126-162-312-316-750-765-670-671-301-362-326-321-3_V.mp3)
  Segment 13: 20640ms ~ 21216ms (file: 652-675-670 263-130-312 756-675-725-732-261-132-127-301-302-216-623-263-627-627-627-167-123-762-361-263-623-263-362-361-627-631-321-236-621-621-607-132-106-160-130-610-160-130-1_W.mp3)
  Segment 14: 22216ms ~ 22792ms (file: 602-612-123-310-513-130-163-206-620-267-527-527-256-237-574-507-620-326-327-206-623-653-167-103-172-167-172-372-357-576-567-130-574-573-103-310-132-321-573-573-576-576-157-205-2_W.mp3)
  Segment 15: 23792ms ~ 24368ms (file: 126-162-612-576-570-750-765-376-673-601-106-320-231-623-573-576-756-765-763-263-321-302-320-321-103-302-321-574-754-576-756-627 574-576-574-756-576-576-576-675-132-301-132-123-1_V.mp3)
  Segment 16: 25368ms ~ 25944ms (file: 261-231-162-203-325-567-576-756-752-325-325-263-230-102-130-123-231-302-312-263-320-312-312-326-362-320-326-302-326-362-263-631-365-127-157-175-732-301-312-321-132-301-132-137-1_W.mp3)
  Segment 17: 26944ms ~ 27520ms (file: 362-307-352-370-357-375-367-126-507-507-157-571-137-302-371-371-371-327-175-107-132-137-137-106-123-132-376-302-327-132-103-312-132-123-326-123-102-320-675-765-657-716-765-706-7_W.mp3)
  Segment 18: 28520ms ~ 29096ms (file: 106-103-103-103-103-135-657-631-106-103-163-136-103-130-132-130-671-136-136-716-136-167-213-612-631-136-316-106-516-615-312-312-657-316-163-136-126-163-132-160-651-615-261-601-1_V.mp3)
  Segment 19: 30096ms ~ 30672ms (file: 305-351 136-132 213-765-574-547-574-576-765-675-672-627 560-675-612-126-621 236_W.mp3)
  Segment 20: 31672ms ~ 32248ms (file: 231-132-725-725-730-730-372-173-213-213-215-532-312-312-130-236-236-327-372-327-327-321-321-375-257-725-276-726-540-572-275-276-267-263-257-257-213-135-312-327-372-123-127-120-2_W.mp3)
  Segment 21: 33248ms ~ 33824ms (file: 326-261-103-132-165-103-160-162-261-176-162-156-165-610-136-162-106-103-316-165-136-137-160-103-165-163-103-671-162-176-126-106-165-763_V.mp3)
  Segment 22: 34824ms ~ 35400ms (file: 162-672-672-620-632-106-160-163-657-653 157-165-576-576-321-263-263-136-362-623-562-672-627-276 672-672-617-612-316-675-637-673-672-675-675-527-756-671-627-617-312-763-672-627-6_W.mp3)
  Segment 23: 36400ms ~ 36976ms (file: 126-132-361-612-613 621-621-276-672-627-627-625-625-627-672-673-623-627-623-675-627-620-620-657-625-627-621-621-625-675-675-761-671-136-120-672-160-136-312-260-620-620-627-672-6_W.mp3)
  Segment 24: 37976ms ~ 38552ms (file: 325-547-534-534-352-504-537-253-231-320-263-765-756-672-620-601-612-106-203-103-260-620-602-263-206-203-206-201-765-267-103-102-610-103-310-126-706-751-765-136-306-123-230-620-2_W.mp3)
  Segment 25: 39552ms ~ 40128ms (file: 217-175-162-612-107-163-671-167-316-137-567-175-306-167-671-167-173-136-103-301-613-176-103-103-106-136-167-361-615-602-602-627-621-130-765-167-102-162-103-526-106-136-623-106-6_V.mp3)
  Segment 26: 41128ms ~ 42820ms (file: 132 673-672-263 617-621-123-672-617-136-136-163 316 312-213-671-675-670-670-762-671-765-765-765-675-765-765-716-675-675-675-607-675-675-675-675-675-675-675-756-675-675-751-123-1_W.mp3)
  Segment 27: 43820ms ~ 44396ms (file: 132-126-136-132-132-135-136-132-136-132-132-162_V.mp3)
  Segment 28: 45396ms ~ 45972ms (file: 657-657-653-365-163-657-657-563-576-537-567-567-576-576-576-561-563-537-576-657-576-576-167-517-571-753-517-576-576-576-576-576-517-567-567-567-657-567-567-517-516-567-567-576-5_W.mp3)
  Segment 29: 46972ms ~ 47548ms (file: 107-103-107-162-261-625-136-130-136-106-672-103-103-106-564-176-165-136-103-572-602-163-601-617-627-671-160-562-352-167-216-136-167-106-167-167-102-125-106-612-617-106-163-671-1_V.mp3)
  Segment 30: 48548ms ~ 49124ms (file: 103-102-316-621-136-130-310-163-301-310-126-361-312-136-631-136-361-367-361-326-236-267-216-312-132-351-573-276-270-102-102-571-367-326-326-267-103-103-130-315-127-120-103-106-3_V.mp3)
  Segment 31: 50124ms ~ 50700ms (file: 163-167-612-362-321-316-132-126-132-312-312-136-136-316-631-267-726-761-176-617-156-127-163-632-126-632-612-671-671 517-362-357-276-207 762-163-263-635-613-361-672-362-627-671-1_W.mp3)
  Segment 32: 51700ms ~ 52276ms (file: 671-671-617-136-132-537-361-603-126-613-163-321-312-125-154-137-136-312-132-312-251-275-324-251-754-765-706-263-621-574-561-653-361-632-603-632-601-162-230-136-132-137-163-160-1_V.mp3)
  Segment 33: 53276ms ~ 53852ms (file: 571-573-325-362-321-352-531-531-523-523-527-572-572-567-572-520-257-251-512-527-527-352-135-571-276-276-267-276-317-157-137-153-165-152-571-517-765-137-135-152-510-316-176-716-7_W.mp3)
  Segment 34: 54852ms ~ 55428ms (file: 103-213-326-132-176-263-123-236-261-236-235-327-673-673_W.mp3)
  Segment 35: 56428ms ~ 57004ms (file: 163-136-576-576 326-102-103-621-213-102-106-613-613-236-321-321-503-513-315-106-612-631-301-615-130-610-126-316-316-132-103-103-632-261-312-106-301-312-301-312-231-623-267-267-1_V.mp3)
  Segment 36: 58004ms ~ 58580ms (file: 632-267-623-261-263-361-126-623 132-103 130-132-130-103-132-130-102-713-730-370-371-103-130-103-301-132_V.mp3)
  Segment 37: 59580ms ~ 60156ms (file: 167 576-657-673-637-637-672-627-162-612-623-316-623-672-627-167-267-136-673-632-672-617-613-623-231-263-637-267-361-621-623-236-236-321-362-132-136-312-316-261-136-126-362-216-1_W.mp3)
  Segment 38: 61156ms ~ 61732ms (file: 375-327-327-371-317-156-621-126-621-237-263-321-125-126-213-261-137-175-715-371-236-326-567-752-713-153-173-137-265-625-136-251-216-365-316-135-215-273-236-627-217-175-123-217-1_W.mp3)
  Segment 39: 62732ms ~ 63308ms (file: 312-613-623-276-213-713-650-326-612-162 163_V.mp3)
  Segment 40: 64308ms ~ 64992ms (file: 126-236-230-261 756-675-756 176-317-725-612-621-362-103-136-615-167-136-136-201-103-132-137-136-103-135-130-153-103-135-132-126-126-106-156-162-623-103-560-617-136-103-137-376-3_V.mp3)
  Segment 41: 65992ms ~ 66568ms (file: 753-132-261-123-103-136-167-103-102-127-321-120-627-362-136-123-327-236-236-327-135-157-513-160-160-362-610-136-312-371-230-203-103-137-107-136-106-163-132-103-132-126-602-672-6_V.mp3)
  Segment 42: 67568ms ~ 68144ms (file: 216-631-316-136-567-625-163-163-623-621-103-617-160-173-316-103-613-316-617-103-163-651-615-617-132-103-216-230-132-136-136-102-106-136-136-617-136-167-617-103-652-536-162-136-1_V.mp3)
  Segment 43: 69144ms ~ 69720ms (file: 625 672-367-165-627-621-762-731-763-675-267-267-263-267-672-267-675-765-265-263 675-675-671-637-672-657-675 632-672 570-752-123-162_W.mp3)
  Segment 44: 70720ms ~ 71296ms (file: 361-326-352-352-356-534-162-613-613-610-751-315-627-237-320-561-637-261-362-316-613 327_W.mp3)
  Segment 45: 72296ms ~ 74204ms (file: 103-562-136-157-361-130-173-236-163-125-321-176-356-176 716-125-125-123-123-132 132-106-107-175-670-760-670-103-103-132-103-132-130-103-710-163-132-135-176-167-137-136-312-302-3_V.mp3)
  Segment 46: 75204ms ~ 75780ms (file: 132-315-627-137-657-607-320-236-562-527-750-260-267-206-260-267-507-576-567-750-175-132-672-672-312-756-756-756-675-627-132-361-103-106-631-567-567-136-132-657-675-657-602-165-2_V.mp3)
  Segment 47: 76780ms ~ 77356ms (file: 216-106-105-156-106-167-617-613-617-601-621-263-163-126-541-567-623-613-162-617-132-152-513-567-672-672-601-610-612-610-601-617-617-613-260-362-623-167-126-167-163-106-621-620-2_W.mp3)
  Segment 48: 78356ms ~ 78932ms (file: 527-675-623-657-652-215-627-627-627-625-623-623-625-627-652-625-265-635-625-672-637-625-672-627-726-276-625-267-627-672-627-672-672-627-675-675-675-572-625-263-276-326-362-267-2_W.mp3)
  Segment 49: 79932ms ~ 80508ms (file: 237-763-731-761-763-316-312-317-317-632-326-312-236-231-213-126-721-520-512-351-327-357-327-321-312-267-103-562-265-267-135-125-172-123-673-673-673-672-673-675-670-761-762-621-2_W.mp3)
  Segment 50: 81508ms ~ 82084ms (file: 672 324-431-651 675-312-216-271-762-765-715-765 253-106-170-103-760-753-756-761-326-312-103-236-236-176-105-157-175-137-543-127-175-613-367-631-165 671-263-276-267-623-630-602-6_V.mp3)
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
> **Time**: 2026-04-03 10:03:24
> **AI**: gemini

---

## Response

AnalysisAnalysisGemini의 응답[
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
*Generated by WKAppBot ask gemini — 2026-04-03 10:03:24*
