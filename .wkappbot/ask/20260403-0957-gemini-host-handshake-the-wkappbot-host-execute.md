# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=09:56:25  uptime=31030s  mem=1834MB  system_processes=445"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_095615.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 12456ms (file: 103-163-312-103-765-756-756-765-732-162-162-126-261-672-672-675-765-765-762-612-621-612-157-301-761-136-162-106-163-106-103-106-103-635-675-162-671-136-163-267-126-127-162-132-1_V.mp3)
  Segment 2: 13456ms ~ 14032ms (file: 312-271-167-103-123-201-236-260-210-210-302-260-216-162-162-103-123-102-213-260-231-216_V.mp3)
  Segment 3: 15032ms ~ 16580ms (file: 627-362-132-610-162-576-615-613-136-136-103-675-653-136-130-163-136-120-163-106-126-167-265-261-136-103-103-617-103-173-127-162 163-765-176-176-137-130-136-137-615-167-756-316-1_V.mp3)
  Segment 4: 17580ms ~ 18156ms (file: 130-367-137-103-130-106-316-760-167-130-136-103-106-163-126-765-765-106-761-172-637-562-236-132-130-316-137-163-136-106-136-103-103-160-176-361-615-167-167-102-712-162-136-217-1_V.mp3)
  Segment 5: 19156ms ~ 19732ms (file: 762-763-701-127-721-762-762-761-726-762-765-732-675-675-765-675-765-675-763-762-762-726-317 163-167-673-371-761-762-367-746-136-174-167-136-216-135-316-103-716-126-135-671-126-6_V.mp3)
  Segment 6: 20732ms ~ 21308ms (file: 310-103-765-326-263-326-236-263-261-627-672-672-765-675 103-134-132 621-627_W.mp3)
  Segment 7: 22308ms ~ 22884ms (file: 326-132-134-103-123-136-136-625-617-612-107-136-162-103-631-163-103-567-103-167_V.mp3)
  Segment 8: 23884ms ~ 24460ms (file: 670-672-673-673-672-671-671-765-675-675-675-673-671-672-672-672 621-623-623-326-756-731-321-236-326-675-675-631-103-236-216-326-267-576-567-316-136-163-103-165-103-163-136-103-3_W.mp3)
  Segment 9: 25460ms ~ 26036ms (file: 317-321-235-124-376-345-163-123-132-364-321-312-765-756-756-567-765-261-165 672-675-765-671-675-675-672-672-672-671-672-675-162-671-762-762-672-274-164-312-241-620-620-621-612-6_V.mp3)
  Segment 10: 27036ms ~ 27612ms (file: 613-316-163-601-263-106-613-632-362-372-735-763-723-175-173-617-637-736-765-573-657-621-617-625-632-310-302-126-613-735-256-637-136-163-576-675-675-675-763-617-672-635-657-657-6_W.mp3)
  Segment 11: 28612ms ~ 29188ms (file: 620-167-576-157-756-176-132-610-620-261-216-106-165-165-210-213-302-302-350-130-123-302-136-163-316-170-750-670-760-603-760-706-273-132-132-123-132-312-316-301-206-105-627-610-6_V.mp3)
  Segment 12: 30188ms ~ 30764ms (file: 127-102 172-175-201-201-210-201-120-206-213_V.mp3)
  Segment 13: 31764ms ~ 32340ms (file: 627-762-672-671-627-672-675-276-672-672-675-675-652-672-675-652-752-627-672-762-672-623-612-160-670-617-675-672-673-726-612-620-621-617-576-756-756-621-627-576-617-672-567-675-6_W.mp3)
  Segment 14: 33340ms ~ 33916ms (file: 613-264-216-263-267-216-267-261-163-123-167-132-126-162-123-670-102-102-103-102-137-132-627-612-371-162-106-103-312-631-137-621-607-627-132-124-125-413-570-574-517-135-132-136_V.mp3)
  Segment 15: 34916ms ~ 35492ms (file: 672-762 675-631-672-726-627-673-672-762-267-326-627-765-672-637-673-672-326-627-567-627-627-261 756-765-657-635-657-675-167-615-657-567-657-675-715-657-657-675-675-756-756-756-6_W.mp3)
  Segment 16: 36492ms ~ 37068ms (file: 137-671-167-157-716-136-136-165-160-106-105-673-167_W.mp3)
  Segment 17: 38068ms ~ 38644ms (file: 136-310-102-245-132-123-265-125-513-721-625-267-627-627-623-627-326-302-302-623-136-103-135-312-130-206-621-163-260-216-132-567-765-675-572-612-267-267-754-751-157-576-756-756-7_V.mp3)
  Segment 18: 39644ms ~ 40220ms (file: 206-263-362-206-260-260-302-326-657-605-620-206-106-102-103-162-260-106-162-163-670-615-106-162-613-136-106-162-132-760-765-673-160-316-623-260-620-276-260-206-620-261-260-672-1_V.mp3)
  Segment 19: 41220ms ~ 41796ms (file: 576-576-357-123-213-263-532-531-516-526-675-637-657-210-302-627-263-631-236-567-562-362-125-602-326-623-263-623-623-263-267-623-621 136-312-123-231-326-123-312-132-130 546-567-5_W.mp3)
  Segment 20: 42796ms ~ 43372ms (file: 172 610-106-613-160-270-725-752-712-370-307-376-132-123-103-216-103-106-613-671-671-176-671-672-672-130-610-607-627-601-620-756-765-765-725-236-316-362-267-627-216-265-621-321-1_W.mp3)
  Segment 21: 44372ms ~ 44948ms (file: 576-706-320-302-326-612-130-316-271-107-107-716-671-612-167-213-126-731-317 765-576-756-756-756-765-236-206-270-276-260-620-260-726-260-216-162-231-320-320-302-312-302-570-207-2_V.mp3)
  Segment 22: 45948ms ~ 46524ms (file: 317-576-572-263-326-625-625-326-236-267-273-625-265-625-326-326-623-263-263-231-321-236-236-236-263-623-625-267-265-627-526-527-627-627-267-623-136-163-156-153-537-523-261-265-5_W.mp3)
  Segment 23: 47524ms ~ 48100ms (file: 257-562-325-765-576-576-253-236-326-326-763-276-627-627-627-627-326-231-236-236-263-723-372-267-267-321-321-327-267-265-257-576-572-752-572-257-327-276-237-257-273-237-574-572-2_W.mp3)
  Segment 24: 49100ms ~ 49676ms (file: 562-627-236-273-236-326-267-236-625-275-631-712-762-265-257-321-362-576-615-651-130-153-413-327 617-623-316-162-621-160_W.mp3)
  Segment 25: 50676ms ~ 51252ms (file: 517-527-516-316-132-625-236-263-236-216 631 627-617 162-162-617-267-261-136-103-163-136-162-765-756-756-175-176-765-765-675-765-756-576-657-675-267-576-675-762-672-765-576-567-6_W.mp3)
  Segment 26: 52252ms ~ 52828ms (file: 765-756-756-756-763-576-756-756-765-756-765-761-763_W.mp3)
  Segment 27: 53828ms ~ 54404ms (file: 351-514-315-371-372-123-217-267-627-267-261-261-261-261-267-263-267-261-216-261-562-657-263-625-231-632-267-216-670-162-126-625-651-365-356-356-352-367-356-562-256-235-315-517-1_W.mp3)
  Segment 28: 55404ms ~ 55980ms (file: 537-567-527-562-576-256-567-103-312-571-657-675-657-576-567-576-571-756-576-105-175-576-576-675-673-625-576-716-657-675-162-370-675-726-130-156-567-725-725-752-275-371-312-362-6_W.mp3)
  Segment 29: 56980ms ~ 57556ms (file: 314-162-153-364-263-167-706-613-623 761-765-672_V.mp3)
  Segment 30: 58556ms ~ 59132ms (file: 173-175-107-760-107-716-170-103-165-365-617-167-675-765-706-750-574-750-752-413-761-670-163-507-506-561-621-612-261-162-236-216-256-267-267 623-263-132-573-103-136-136-173-172-7_V.mp3)
  Segment 31: 60132ms ~ 60708ms (file: 731-120-612-265-617-213-123 265-162-170-601-123-103-137-162-612-601-602-316-263-621-106-657-105-130-321-612-261-126-216-316-216-627-103-156-561-162-312-103-203-136-163-362-126-3_W.mp3)
  Segment 32: 61708ms ~ 62284ms (file: 627-672-762-762-726-765-725-265-267_W.mp3)
  Segment 33: 63284ms ~ 63860ms (file: 216-623-576 230-307-627-623-301-316-312-321-103 602-267-573-753-375-671-672-362-310-132-627-312-706-731-137-107-137-751-715-132-132-621-130-310-312-136-103-312-316-163-750-753-7_V.mp3)
  Segment 34: 64860ms ~ 65436ms (file: 216-123-321-123-126-132-126-126-126-261-126-216-126-162-216-216-261-621-261-261-621-261-621-263-263-621-261-236-261-126-261-216-126-216-213-216-216-126-126-236-216-127-217-126-1_W.mp3)
  Segment 35: 66436ms ~ 67012ms (file: 761-761-760 576 316-612-361-613-631-631-126 326_V.mp3)
  Segment 36: 68012ms ~ 68588ms (file: 761-756-756-765-763-765-756-576-576-576-765-765-761-756-756-756-756-765-765-765-765-756-762-756-765-167-761-623-216-126-751 126-765-163-160-103-173-103-610-371-651-316-106-265-1_W.mp3)
  Segment 37: 69588ms ~ 70164ms (file: 136-123-671-671-613-612-613-627-671-617-671-167-162-621 165-126-301-326-176-127-103-136-631-613-143-126-765-167-613-136-130-136-136-106-135-172-127-312-103-163-167-160-765-167-2_V.mp3)
  Segment 38: 71164ms ~ 71740ms (file: 130-301-103-103-615-160-106-162-160-106-160-625-162-625-106-106-310-371-310-231-723-317-213-261-315-321-231-321-236-321-321-320-312-312-321-312-231-236-265-123-231-216-231-321-2_W.mp3)
  Segment 39: 72740ms ~ 73316ms (file: 103-167-106-160-162-125-617-615-601-126-627-620-657-612-163-163-610-103-623-123-103-163-763-132-163 103-136-136-103-130-123-132-265-163-761-103-106-103-103-132-132-356-371-130-1_V.mp3)
  Segment 40: 74316ms ~ 74892ms (file: 521-123-130-103-103-310-130 120-120-162-326-321-130-103-130-310-321-316-132-361-160-312-361-126-103-167-132-160-103-176-126-721-103-210-321-213-231-123-326-312-103-103-261-167-3_V.mp3)
  Segment 41: 75892ms ~ 76468ms (file: 163-136-136-346-156-167-631-317-163-570-507-670-607-316-136_V.mp3)
  Segment 42: 77468ms ~ 78044ms (file: 312-352-523-523-125-316-361-315-531-562-257-521-652-675-574-753-327-267-256-256-325-236-756-756-765-672-627-263-627-263-627-267-327-327-326-321-236-267-574-574-540-574-351-253-2_W.mp3)
  Segment 43: 79044ms ~ 79620ms (file: 623-627-673-624-261-263-326 352-231-657-652-625-652-567-576-567-562-256-261-263-623-526-526-256-265-620-526-265-265-672-625-261-620-625-620-627-627-652-564-567-265-265-213-260-6_W.mp3)
  Segment 44: 80620ms ~ 81196ms (file: 136-103-210-321-102-213-601-130-216-120-231-126-231-213-123-132-316-312-210-203-206-201-120-130_V.mp3)
  Segment 45: 82196ms ~ 82772ms (file: 136-106-103-713-136-165-512-102-256-126-267-617-267-621-126-123-762-765-762-765 213-567-153-123-123-263-216-213-712 135-153-125-312-312-136-132-302-137-162-265-230-253_V.mp3)
  Segment 46: 83772ms ~ 84348ms (file: 316-162-671-671-653-160-132-162-126-612-136-312-236-361-167-621-267 627-672-762-670-672-670-675-761-765-756-756-316-312 627-623-627-256-561-153 162-627-672-267-267-261-267-261-6_W.mp3)
  Segment 47: 85348ms ~ 85924ms (file: 132-167-621-176-162-102-602-136-162-136-137-607-167-623-602-173-617-163-160-617-670-607-107-617-761-361-367-672-670-672-760-672-763-673-671-672-762-763-765-763-763-673-176-671-1_W.mp3)
  Segment 48: 86924ms ~ 88832ms (file: 623-102-670-612-671-632-621-610-102-215-631-106-561-167-635-126-546-546-617-106-123-201-102-102-203-263-571-576-735-307-312-302-103-106-310-306-576-576-576-617-625-612-160-612-1_W.mp3)
  Segment 49: 89832ms ~ 90408ms (file: 671-136-103-106-635-157-267-127-162-765-213-231-132 123-573-375-576-765-576-613_V.mp3)
  Segment 50: 91408ms ~ 91984ms (file: 276-123-132-132-375-627-753-276-726-756-726-762-275-726-521-602-162-126-261-201-210-273-213-231-236-276-623-326-276-276-247-572-126-760-762-672-765-726-217-312-132-312-132-136-2_W.mp3)
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
> **Time**: 2026-04-03 09:57:06
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
*Generated by WKAppBot ask gemini — 2026-04-03 09:57:06*
