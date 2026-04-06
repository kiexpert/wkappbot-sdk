# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=10:04:48  uptime=31533s  mem=1939MB  system_processes=448"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_100439.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 361-123-613-361-162-321-165-136-106-617-316-615-236-260-103-632-327-136-106-106-670-136-160-167-162-635-265-316-716-167-160-130-765-136-126-136-361-103-132-106-326-126-165-130-6_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 756-765-160-163-765-756-267-267-762-762 627-126-671-267-162-765-765 627-632-627 361 612-126-612 312-312 206-276-106-176-136-126-261-216-126-126-162-130-132-127-362-263-326-136-1_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 135-126-756-756-756-672-276-302-631-120-136-671-617-317-723-267-627-236-201-320-632-675-762-761-367-137-136-162-136-126-106-106-163-103 756-103-316-130-132-206-367-310-162-302-3_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 157-721-721-103-301-132-207-203-136-312-302-306-302-130-163-176-132-312-163-162-321-321-632-123-123-213-123-201-132-123-132-203-325-352-357-753-756-756-576-756-576-175-310-371-3_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 570-504-574-175-213-321-312-750-756-705-756-752-317-132-132-312-316-316-136-376-137-126-124-143-613-413 213-126-361-134-312-231-507-570-570-572-251-735-173-756-765-756-756-607-3_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 765-675-657-657-675-675-102-160-612-563-657-165-126-671-715-657-762-725-372-175-735-657-567-517-561-657-673-672-516-652-652-316-162-615-671-265-625-256-265-567-612-576-675-526-7_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 106-716-312-126-103-371 103-130-130-162-361-127-615-160-162-103-163-631-216-632-361-630-617-623-326-103_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 123-312-316-123-126-126-167-136-167-713-273-175 672-361 312-123-153-165 612-607-607-671-617-625-756-765 672-674-614-756-765 126-612-627 632_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 362-312-547-617-352-327-325-321-126-132-675-765-765-752-326-123-705-761-167-167-317-627-260-203-732-207-206-321-302-302-321-132-132-301-672-632-216-302-312-123-132-132-320-216-2_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 631-613-613-657-267-371-613-136-613-132-624-163-216-571-570-576-756-762-765-163-167-176-765-756-750-671-103-617-675-106-167-627-372-143-132-123-132-107-132-317-371-132-217-136-1_V.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 612-302-123-670-627-672-627-621-612-623-126-651-612-607-601-607-327-302-302-357 627-132-132-132-137-657-567-576 567-652-576-675-652-652 653-627_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 213-542-547-123-316-103-135-136-231-621-162-206-627-206-263-263-263-206-260-206-136-167-106-167-163-132-203-230-203-203-203-206-206-203 617-170-106-546-165-302-310-103-130-103-1_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 312-312-321-316-362-312-162-361-321-136-132-126-126-632-631-321-312-263-216-627-637-623-632-632-623-637-362 637-673-637-672-613-627-637 361-361-631-367-632-362_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 317-312-137-312-317-173-317-135-312-213-123-132-216-123-162-216-216-165-675-165-165-231-316-327-137-326-321-316-321-356-562-315-263-362-312-362-321-651-563-175-217-213-127-123-1_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 631-162-613-162-102-167-167-756-675-672-620-651-652-632-326-263-326-320-621-631-637-263-260-672-675-206-621-617-672-213-123-213-267-236-231-316-312-321-326-132-312-672-762-765-7_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 236-162-163-302-203-103-132-736-327-752-675-756-752-567-726-752-265-267-267-267-267-627-216-762-760-756-576-157-617-314-632-312-517-637-673-627-670-316-576-576-576 672-672-672_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 132-316-132-123-213-126-162-136-103-126-126-102-312-162-102-103-163_V.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 632-170-170-137-763-726-672-367-376-672-267-263-627-623-627-263-627-267-267-623-362-362-326-623-362-236-326-362 573-354-351-627-621-236-263-326-251-126-236-231-267-627-637-362-6_W.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 765-760-620-627-627-602-132-123-675-617-607-602-607-361-106-103-312-103-136-123-617-760-672-672-627-602-651-617-167-167-617-267-267-632-625-167-102-132-213-321-123-231-103-130-3_V.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 321-523-136-132-762-236-321-326-326-326-302-256-236-256-265-216-201-213-261-261_W.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 157-567-135-136-132-167-106-130-132-137-312-321-130-103-126-321-627-163-132-123-231-132-136-312-132-213-216-261-261-762-762-762-765-763-752-172-752-671-765-276-167-756-120-361-1_W.mp3)
  Segment 22: 33096ms ~ 42060ms (file: 130-316-163-174-475-231-312-137-176-124-317-213-301-632_V.mp3)
  Segment 23: 43060ms ~ 43636ms (file: 213-123-213-123-213-123-213-123-213-123-213-123-123-213-123-213-132-123-213-123-216-126-123-126-123-126-123-321-123-123-126-123-132-126-123_V.mp3)
  Segment 24: 44636ms ~ 45212ms (file: 534-523 723-703-375-761-713-732-573-517-153-317 125-125-512-156-265-567-567-567-152-152-156-156-125-653-652-561-653-564-540-561-653-657-657-654-675-657 572-542-527-523-635-156-6_W.mp3)
  Segment 25: 46212ms ~ 46788ms (file: 673-716-236-236-301-261-210-302-213-320-316-312-152-132-130-267-612-321-321-136-103-103-130-236-632-362-102-120-102-163-162-102-132-162-132-310-301-165-267-762-263-326-236-623-2_W.mp3)
  Segment 26: 47788ms ~ 48364ms (file: 162-106-103-136-136-106-631-613-216-613-267-160-612-736-312-612-137-320-126-103-130-130-130-103-631-165-103-136-715-172-176-312-160-103-136-103-103-132-263-637-176-173-163-316_V.mp3)
  Segment 27: 49364ms ~ 49940ms (file: 310-310-132-761-160-106-163-106-750-302-302-312-627-106-132-302-312-307-376-103-120-236-756-705-317-751-576-576-657-654-615-326-362-132-310-317-735-703-730-517-564-106-617-132-1_W.mp3)
  Segment 28: 50940ms ~ 51516ms (file: 106-136-317-163-162-162-163-216-231-132-560-612-316-103-601-163-127-216-126-716-126-563-136-163-760-762-672-316-631-623-163-136-631-613-316-136-163-612-613-136-613-670-675-136-1_V.mp3)
  Segment 29: 52516ms ~ 53092ms (file: 562-516-571-751-756-675-675-675-756-765-321-236-162-123-261-236-236-627-326-326-263-267-765-762-576-756-657-567-675-627-263-236-263-267-263-267-132-123-756-765-765-765-675-673-6_W.mp3)
  Segment 30: 54092ms ~ 54668ms (file: 562-562-326-567-635-236-362-326-576-670-357-567-673-672-706-360-576-136-612-136-167-173-167-167-160-716-362-635-657-657-753-271-263-765-706-267-326_W.mp3)
  Segment 31: 55668ms ~ 56244ms (file: 753-675-574-576-576-765-756-574-675-657-576-574-571-752-715-723-567-657-327-276-123 567-652-657-652-675-756-765-675-671-765-765-657-765-627-613-632-627-671-653-263-321-617-671-6_W.mp3)
  Segment 32: 57244ms ~ 57820ms (file: 376-370-316-317-317-317-361-376-376-367-376-367-367-317-321-265-267-276-256-276-756-572-756-265-623-265-267-265-756-165-561-156-156-571-265-256-325-132-125-135-132-135-153-531-5_W.mp3)
  Segment 33: 58820ms ~ 59396ms (file: 651-213-623 261-261-261-523-263-263-362-263-562-576-265-625-627-625-627-263-657-657-762-657-576-567-567-567-657-756-756-132-163-136-263-132-106-162-165-561-103-351-103-167-765-6_W.mp3)
  Segment 34: 60396ms ~ 60972ms (file: 173-165-561-132-316-657-175-217-675-156-160-163-103-623-673-602-620-136-716-231 136-315-301-301-132-603-136-162-162-163-103-206-210-102-301-236-126-162-203-103-310-301-130-301-1_V.mp3)
  Segment 35: 61972ms ~ 62548ms (file: 261-216-652-310-173-163-167-267-206-203-102-752-765-132-132-310-130-103-321-132-320-570-172-206-312-761-617-751-127-673-657-570-165-167-517-106-167-103-651-715-625-576-167-615-6_V.mp3)
  Segment 36: 63548ms ~ 64124ms (file: 721-176-210-612-601-602-236-235-507-265-203-263-206 154-153 763-675 570-507-751-705 123-765-762-172-172-267-132-231-213-276-273-172-762-276-126-216-231-231-217-721-127-103-120-2_W.mp3)
  Segment 37: 65124ms ~ 65700ms (file: 132-312-132-132-216-263-216-123-162-126-123-216-126-217-216-217-217-275-627-275-127-627-361-637-367-765-367-752-752-573-756-567-756-627-261-527-527-325-325-356-256-135-325-326-3_W.mp3)
  Segment 38: 66700ms ~ 67276ms (file: 126-613-165-675-316-132-136-103-106-103-107-132-126-132-164-176-167-124-127-132-351-356-213-312-321-627-602-602-263-213-672 521-521-152-176-260-103-130-103-312-132 756-756-312-6_V.mp3)
  Segment 39: 68276ms ~ 68852ms (file: 213-261-231-216-652-236-126-123-216-231-312-326-316-362-612-136-312-235-352-365-235-267-326-126-126-126-162-162-162-216-267-612-261-276-216-261-236-236-236-325-326-216-231-267-2_W.mp3)
  Segment 40: 69852ms ~ 70428ms (file: 162-137-126-162-620 120-132-137-162-156-132-123-132-163 367-623-236-326-326-276-627-672-362-362-632-632-362-326-236-623-630-123-612-326-130-362-136-136-671-167-103-132-136-617-1_V.mp3)
  Segment 41: 71428ms ~ 72004ms (file: 326-231-362-612-321-231-123-632-263-623-635-627-623-632-627-721-627-726-672-637-276-627-765-672-627-672-263-672-267-765-627-627-265-635-627-762-627-263-265-726-276-625-162-623-2_W.mp3)
  Segment 42: 73004ms ~ 73580ms (file: 120-103-103-103-123-106-106-102-150-506-156-561-576-576-756-756-162-176-126-620-206-260-130-610-635-610-123-576-567-657-310-316-107-162-670-736-216-675-637-362-321-621-312-367-3_W.mp3)
  Segment 43: 74580ms ~ 75156ms (file: 671-617-761-763-765-763-762-176-761-167-765-765-761-762-671-765-765-627-321-761-761-765-762-673 621-126-231-671-617-617-632-576-675-627-102-673-675-167-312-132-132_W.mp3)
  Segment 44: 76156ms ~ 76732ms (file: 316-231-317-216-761-276-637-236-753-736-756-352-753-176-735-753-751-163-371-723-712-725-716-716-765-671-271-736-236-631-167-175-125-132-176-765-137-127-215-267-716-167-671-157-7_W.mp3)
  Segment 45: 77732ms ~ 78308ms (file: 162-126-123-132-136-362 126-312 675_V.mp3)
  Segment 46: 79308ms ~ 79884ms (file: 362-102-130-136-136-162-671-103-561-103-156-103-104-106-167-164-167-326-321-132_V.mp3)
  Segment 47: 80884ms ~ 81460ms (file: 612-162-106-162-160-172-267-126-261-216-657-275-267-267-256-275-256-265-265-260-137-132-143-413-132-413-132-312-132-312_V.mp3)
  Segment 48: 82460ms ~ 83036ms (file: 352-251-126-263-263 627-362-216-261-627-627-367-623 162-167-123-126-126-621-236-321-702-726-137-137-126-312-231-312-213-312-237-673-607-672-126-123-321-107-302-310-321-120-102-1_V.mp3)
  Segment 49: 84036ms ~ 84612ms (file: 106-103-137-106-612-613-360-103-162-170-302-612-163-162-106-136-106-316-137-675-671-306-601-106-136-160-106-103-136-657-675-630-163-316-103-735-750-762-206-327-762-675-671-612 3_V.mp3)
  Segment 50: 85612ms ~ 86188ms (file: 352-356-537-735-365-153-573-531-351-567-651-576-561-657-651-561-567-576-567-675-673-176-156-716-123-153-756-765-765-576-567-573-537-735-735-371-317-573-135-357-537-357-376-175-1_W.mp3)
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
> **Time**: 2026-04-03 10:05:37
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
*Generated by WKAppBot ask gemini — 2026-04-03 10:05:37*
