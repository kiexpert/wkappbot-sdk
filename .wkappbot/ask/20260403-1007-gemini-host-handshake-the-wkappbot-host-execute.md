# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=10:06:59  uptime=31664s  mem=1739MB  system_processes=445"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_100649.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 157-507-675-167-326-301-326-123-136-316-312-134-132-316-103-103-617-163-130-160-163-130-136-316-162-612-176-362-102-136-213-123-103-106-106-163-106-106-103-103-106-712-102-103-7_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 312-135-130-312-312-106-623-675-236-632_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 705-765-756-765-675-613-627-756-612-136-361-756-756-756-726-273-235-572-750-752-127-107-675-760-236-107-102-167-715-106-301-261-765-652-176-751-261-617-102-132-136-126-716-235-5_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 574-675-123-102-132-123-103-136 216-231-312-132-132-327-103-132-167-203-213-130-613-136-130-573-735-612-206-267-261-136-231-361-607-765-675-136-321-302-632-263-231-236-621-123-2_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 360-362-361-671-672-361-631-617-761-761-630-163-132-136-621-260-126-163-261-613-761-716-371-316-362-126-126-107-760-176-160-312-160-623-321-263-163-307-103-130-312-572-213 231-1_V.mp3)
  Segment 6: 7880ms ~ 9788ms (file: 132-267-132-627-267-263-627-260-261-236-632-623-302-321-736-762-267-620-367-327-230-320-312-310-176-263-267-326-236-236-321-621-623-620-602-620-602-623-637-261-213-301-203-230-5_V.mp3)
  Segment 7: 10788ms ~ 11364ms (file: 132-574-572-102-213-302-312-130-103-106-103-301-132-132-267-206-206-325-572-570-152-170-103-316-163-106-107-167-107-103-310-507-570-523-572-572-520-103-136-301-302-570-527-235-2_V.mp3)
  Segment 8: 12364ms ~ 12940ms (file: 367-602-630-675-670-657-652-760-675-132-706-631-106-637-623-263-162-631 167-167-230 642-167 621-620-613_W.mp3)
  Segment 9: 13940ms ~ 14516ms (file: 564-163-123-137-163-126-316-652-650-306-163-160-361-301-301-317-130-163 761-671_V.mp3)
  Segment 10: 15516ms ~ 16092ms (file: 176-631-106-162-132-103-130-163-106-106-615-316-123-126-132-106-103-130-657-675-621-126-103-173-671-103-316-130-105-132-126-162-213-123-162-306-106-130-213-123-310-261-512-572 1_V.mp3)
  Segment 11: 17092ms ~ 17668ms (file: 162-765-675-362-132-657-652-263-312-132-160-613-136-216-261_W.mp3)
  Segment 12: 18668ms ~ 19244ms (file: 263-216 310-301-123 621-362 123-126-123-623-132-162 762-672-726-627 613-123-176 236-123-235-236-213-312 621-261-321-321-263 312-132 675-672-267-316-126-216-267-236-326-236-512-1_W.mp3)
  Segment 13: 20244ms ~ 20820ms (file: 132-126-176-163-672-156-163-136-231-167-130-756-163-102-216-103-132-167-167-175-213-176-103-310-615-136-162-123-206-123-321-136-567-136-176-126-103-103-136-167-160-102-163-106-1_V.mp3)
  Segment 14: 21820ms ~ 22396ms (file: 372-637 521 621-321-321-623-231-627-613-263-610-675-736_W.mp3)
  Segment 15: 23396ms ~ 23972ms (file: 725-137-317-263-210-321-103-316-126-102-107-103-132-102-132-167-136-623-162-103-106-106-613-102-103-136-106-127-316-163-165-107-167-132-106-106-136-165-167-103-720-167-136-162-1_V.mp3)
  Segment 16: 24972ms ~ 25548ms (file: 124-162-132-123-321-361-123-132-165-135 132-326_V.mp3)
  Segment 17: 26548ms ~ 27124ms (file: 103-765-762-263-260-623-102-135-130-103-136-132-632-102-601-610-106-106-613-106-162-132-130-132-102-132-102-567-675-167-162-302-231-765-576-765-265-213-310-126-610-672-123-132-3_V.mp3)
  Segment 18: 28124ms ~ 28700ms (file: 163-135-610-203-132-326-206-203-237-207-621-317-507-136-231-237-671-671-762-672-671-673-672-546-762-726-627-621-275-207-275-267-761-671-761_W.mp3)
  Segment 19: 29700ms ~ 30276ms (file: 517-106 621-321-726-637-136-126-162-172-102-120-132-130-126-102-123-126-260-132-120-316-162-126-102-621-213-321-167-127-123-216-126 620-213-136-231-326-567-675-267-256-326-623-5_W.mp3)
  Segment 20: 31276ms ~ 31852ms (file: 617-136-723-376-361-376-375-365-357-237-326-527-571-653-367-367-761-736-607-657-567-265-357-357-375-357-354-517-547-543-547-157-514-521-547-526-153-351-357-517-513-357-573-537-5_W.mp3)
  Segment 21: 32852ms ~ 33428ms (file: 136-102-601-610-316-103-136-130-132-765-162-163-621 672-132-106-673-160-103-103-316-136-563-156-261-716-160-106-136-103-523-130-316-136-130-317-103-130-603-103-136-167-635-517-3_V.mp3)
  Segment 22: 34428ms ~ 35004ms (file: 326-321-123-213-612-126-261-213-236-312-132-216-231-362-132-321-321-321-132-236-312-132-216-136-312-316-236-321-231-213-321-126-162-163-326-261-316-627-216-215-216-213-263-236-1_W.mp3)
  Segment 23: 36004ms ~ 36580ms (file: 176-167 312-632 613-236-621-123-216-216-213 216-126-612-126-216-123-132-162-123-126-154-143-173-162-126-172-176 312-625-632-627-601-627-267-321-652-627-267 672-617-136-613-506-5_W.mp3)
  Segment 24: 37580ms ~ 38156ms (file: 726-213-621-267-672-627-756-523-162-672-216-267-107-673-367-765-537-576-567-576-572-573-527-576-567-576-572-150-617-106-163-310_W.mp3)
  Segment 25: 39156ms ~ 39732ms (file: 756-765-725-657-765-725-271-756-167-162-175-135-321-301-103-132-326-126-120-132-316-231-532-523-265-261-263-236-261-236-103-130-261-625-652-625-236-672-765-765-675-671-763-132-6_W.mp3)
  Segment 26: 40732ms ~ 41308ms (file: 106-103-576-561-637-641-620-620-260-106-103-320-302-753-572-265-132-236-302-305-532-532-103-163-652-617-162-162-236-623-326-362-213-103-231-216-263-123-736-276-672-672-236-632-6_W.mp3)
  Segment 27: 42308ms ~ 42884ms (file: 165-135-136-625-562-672-612-165-651-657-106-625-672-613-267-607-306-136-601-612-106-163-106-103-106-162-163-163-610-675-675-607-670-617-657-675-670-106-607-216-263-623-621-106-1_V.mp3)
  Segment 28: 43884ms ~ 44460ms (file: 725-752-135-275-276-326-312-175-206-267-237-573-567-675-627-627-362-310-130-203-305-375-362-261-263-620-301-103-130-132-132-132-312-312-132-126-612-621-120-726-132-203-203-572-5_W.mp3)
  Segment 29: 45460ms ~ 46036ms (file: 136-231-327-312-675-657-756-253-572-326-235-236-205-325-276-126-721-761-367-176-317-621-635-673-675-362-267-652-601-635-716-167-310-576-576-675-712-657-673-726-107-132-127-172-2_W.mp3)
  Segment 30: 47036ms ~ 47612ms (file: 612-103-671-671-263-132-176-107-103-756-167-167-167-163-160-136-106-123-153-316-102-261-263-260-623-236-263-210-230-103-213-321 126 162-675_V.mp3)
  Segment 31: 48612ms ~ 49188ms (file: 372-721-230-357-136-312-326-362-321-735-137-521-256-325-513-207-203-123-213-127-376-126-236-213-357-571-750-675-137-514-514-134-132 765-276-672-267-547-574-576-576-562-612-765-7_V.mp3)
  Segment 32: 50188ms ~ 50764ms (file: 127-651-216-162-107-162-156-106-657-130-256-362-613-610-103-765-756-713-103-103-106-130-670-130-127-312-120-613-176-321-132-123-176-136-136-613-317-216-236-316-103-106-136-103-1_V.mp3)
  Segment 33: 51764ms ~ 52340ms (file: 301-263-162-126-216-621-631-612-261-361-163-361-631-610-261-126-263-267-267-261-376-261-623-162-236-263-672-763-670-260-627-652-126-263-201-320-237-203-302-203-236-203-301-136-6_W.mp3)
  Segment 34: 53340ms ~ 53916ms (file: 625-652-267-623-263-267-625-267-536-615-103-130-137-132-657-625-657-675-675-670-607-760-107-612-213-103-123-135-573-157 132-143-132-310-325-576-753-517-541-153-312-123-312-725-7_V.mp3)
  Segment 35: 54916ms ~ 55492ms (file: 207-302-721-756-270-207-302-320-321-302-217-102-762-163-170-726-107-173-137-317-723-732-725-270-270-203-273-230-762-230-123-123-132-623-657-675-671-217-712-175-705-703-706-672-6_W.mp3)
  Segment 36: 56492ms ~ 57068ms (file: 621-627-623-326-263-236-263-125-126-152-126-163-123-316-102-547-547-574-627-276-756-765-765-762-672-627-756-675-627-621-671-672 765-756-756-756-675-627-627-260-260-621-765-576-5_W.mp3)
  Segment 37: 58068ms ~ 58644ms (file: 326-136-312-361-615-165-367-651-165-516-756-365-326-623-216-237-236-275-231-231-372-157-163-167-371-371-316-312-326-321-137-632-632-326-372-326-365-351-371-132-123-213-231-213-2_W.mp3)
  Segment 38: 59644ms ~ 60220ms (file: 163-613-316-162-320-310-102-263-306-316-623-316-361-601-610-601-310-103-103-132-570-573-130-301-132-620-260-623-263-326-256-325-231-231-126-132-231-301-132-132-163-612-573-725-7_W.mp3)
  Segment 39: 61220ms ~ 61796ms (file: 173 106-263-316-261-103-132-103-106-157-162-167-136-310-527-175-125-263-103-106-756-235-571-132-103-137-126-126-106-103-103-103-106-106-167-564-625-671-176-627-623-163-132-102-1_V.mp3)
  Segment 40: 62796ms ~ 63372ms (file: 132 263-261-326-326-231-321-216-326-362-726-362 753-375 251-716-413-132-123-215-130-103-103-132-316-123-126-163-126-136-275-126-216-126-136-365-126-124-314-374-123-261-716-203-2_V.mp3)
  Segment 41: 64372ms ~ 64948ms (file: 623-132-126-157-527-752-721-567-162-726-216-163-126-123-567-562-527-573-106-120-132-361-106-102-310-306-312-136-167-726-617-671-672-267-612-617-623-765-675-675-672-675-765-762-6_W.mp3)
  Segment 42: 65948ms ~ 66524ms (file: 134-576-574 574-571-607-670-672-675-675-675-756-765-623-620-162-123-135-315-175-716-107-157-350-173-601-103-103-126-716-136-173-136-312-136-103-102-103-162-103-123-310-312-176-3_V.mp3)
  Segment 43: 67524ms ~ 68100ms (file: 326-476-136-524-356-312-162-136-716-721-327-527-265-263-327-213-325-176-124-316-271-261-156-721-251-142-637-123-316-162-215-371-364-724-571-317-742-136-265-236-526-351-375-132-3_V.mp3)
  Segment 44: 69100ms ~ 69676ms (file: 231-203-326-350-107-615-156-617-301-103-132-301-163-132-312-132-137-162-617-510-103-510-135-302-207-320-137-271-103-302-357-356-137-136-126-103-103-571-305-310-310-310-132-315-1_V.mp3)
  Segment 45: 70676ms ~ 71252ms (file: 162-765-610-206-127-162-372-106-671-136-731-621-163-163-136-601-103-521-126-326-176-312-136-312-103-563-106-167-103-167-176-136-175-265-162-162 103-516-175-617-261-106-516-152 7_V.mp3)
  Segment 46: 72252ms ~ 72828ms (file: 206-236-261-230-203-230-206-260-267-762-206-672 260-620-620-627-765-675-167-571 637-206-670-627-260-260-260-206-260-206-206-230-326-236-631-162-236-261-260-260-267-206-236-762-2_W.mp3)
  Segment 47: 73828ms ~ 74404ms (file: 123-627-765-123-671-672-672-672-631-136-361-612-672-267-263-163-156-136-136-316-136-136-216-132-136-316-163-316-163-617-673 574-527-612-213-267-326-361-136-263-213-527-267-163-5_W.mp3)
  Segment 48: 75404ms ~ 75980ms (file: 216-623-361-635-317-136-612-137-162-716_V.mp3)
  Segment 49: 76980ms ~ 78420ms (file: 201-126-123-132-103-130-132-136 754-675-765-673-612-362-160-601-620-627-160-106-102-170-132-103-172-710-170-176-176-170-713-102-127-167-170-612-267-627-176-762-265-726-702-760-6_W.mp3)
  Segment 50: 79420ms ~ 79996ms (file: 574-756-710-130-103 631-167 167-103-316-631-617-621-106-136-132-561-103-316-216-612-316-103-163-106-162-103-163-621-132_V.mp3)
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
> **Time**: 2026-04-03 10:07:39
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
*Generated by WKAppBot ask gemini — 2026-04-03 10:07:39*
