# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=09:54:12  uptime=30898s  mem=1830MB  system_processes=445"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_095402.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 632-326 263-623-267-627-623-627-206 267-623-621-627-627-267-627-627-652-627-263-623-627-623-627-623-612-623-672-326-623-326-263-236-623-672-623-632-623-623-621-231-132-123-637-6_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 312-301-301-206-263-325-302-103-103-103-361-362-370-103-675-725-627-263-230-203-236-326-231-320-320-230-320-503-521-106-103-236-526-520-132-321-753-756-206-326-326-236-627-526-5_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 675-657-576-526-765-765-671-576-765-617-675-617-167-267-621-612-621-267-526-263-263-627-627-627-236-623-763-276-267-267-624-765-576-657-576-576-576-576-675-672-672-576-756-756-7_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 167-615-765-756-157-156-657-561-156-653-651-156-563-516-163-152-165-615-615-167-176-167-156-165-157-651-135-157-567-153-617-657-157-165-156-567-175-517-156-561-651-613-756-165-1_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 627-675-673-736-317-610-630-361-316-361-675-756-706-705-750-571-135-260-236-301-103-310-106-130-102-106-361-760-621-167-603-132-765-765-765-765-765-762-765-312-321-216-126-236-2_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 107-167-701-751 316-750-175-132-312-314-312-413-132-314-132-267-527-540-504-367-302-301-316-312-750-751-754-754-574-570-627-103-136-103-132-134-143-126-261-613-123-134-143-132-1_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 576-176-651-657-735-567-571-561-571-657-657-751-657-516-567-657-637-763-357-753-657-675-567-756-756-756-576-576-657-765-576-576-576-567-765-657-567-657-576-567-657-576-567-571-5_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 567-657-627-627-627-627-632 672-627-627-612-130-672-536-675-672-260-263-362-126-612-267-627-625-276-263-756-627-621-672-672-672-675-657-627-672-627-652-627-627-627-627-567-657-6_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 524-325-602-625-567-675-572-567-567-576-675-673-617-716-261-267-126-231-163-263-620-620-260-267-540-672-267-627-273-602-602-602-601-601-162-613-361-632-362-615-167-106-570-760-1_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 103-756-315-367-103-574-506-756-760-760-670-672-672-627-725-720-267-751-670-760-602-267-206-102-302-302-126-213-216-624-126-623-310-301-670-317-170-327-301-103-130-301-574-317-1_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 263-216-362-126-126-162 621-612-657-162-316-657-657-136-126-130-132-316-312-621-612-623-136-132-136-623-632-316-216-136-126-306-612-625-236-126-362-213-623-267-612-126-102-160-6_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 132-130-230-130-326-621-103-132-103-136-167-162-651 160-162-761-610-167-206-206-136-265-136-571-107-136-132-132-316-102-351-103-136-136-650-126-150-215-162-106-136-103-167-167-1_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 107-132-312-136-361-103-165-136-103-103-137-630-176 675-210-103-210-120-210-103-126-126-106-126-120-216-267-132-123-102-132-163-132-312-132-213-162-136-137_V.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 261-751-675 756-756-765-675-756-756-361-613-510-172-627-160-761-163-213-612-163-612-167-103-761-160-163-316-136-103 546-216-153-162-130-715-163-163-130-765-136-107-130-715-542-1_W.mp3)
  Segment 15: 22064ms ~ 22748ms (file: 573-370-320-320-320-302-302-730-165-103-126-610-176-136-167-167-136-103-176-673-136-103-715-107-103-106-167-172-137-175-106-136-376-175 106-160-126-102-103-231-106-126-123-260-1_V.mp3)
  Segment 16: 23748ms ~ 24324ms (file: 562-625-625-672-627-625-752-132 136-654-562-675-267-657-265-627-261-576-572-765-716-237-765-672-675-760-675-126-102-760-106-720-576-157-102-126-627-671-721-672-672-312-625-672-6_W.mp3)
  Segment 17: 25324ms ~ 25900ms (file: 675-623-267 541-150-105-350-176-176 132-261-623-213-316-631-261-627-267 576-756-236-612-261-265-267-627-627-126-231_W.mp3)
  Segment 18: 26900ms ~ 27476ms (file: 657 672-621-367-261-675-675-657-632-675-672-675-627-627-652-625-623-627-627-675-675-625-267-623-623-672-657-625-652-261-576-765-236-675-675-672-765-765-627-765-726-672-627 627-6_W.mp3)
  Segment 19: 28476ms ~ 29052ms (file: 765-617-670-176-170-127-156-120-167-316-564-136-760-107-175-136-761-765-765-670-657-570-574-754-754-163-671-267-132-301-675-675-260-260-620-260-236-231-236-236-206-236-263-261-1_W.mp3)
  Segment 20: 30052ms ~ 30628ms (file: 136 137-163-137-167-160-610-167-132-132-163-103-136-103-136-106-136-103-136-617-163-136-167-107-136-162-103-316-130-103-176-613-106-263-612-123-516_V.mp3)
  Segment 21: 31628ms ~ 32204ms (file: 620-517-612-172-263 631-106-163-136-126-516-136-263-376 163-157-165-106-126-175-517-765-167-756-576-136-657-617-672-673-132-315-756-765-671-617-617-367-631-632 576-567_W.mp3)
  Segment 22: 33204ms ~ 33780ms (file: 123-653-653-106-612-310-316-137-103-103-103-130-106-132-607-103-763-716-176-671-156-163-617-301-136-362-371-165-631-617-160-103-126-571-536-165-167-173-761-317-160-137-160-106-2_V.mp3)
  Segment 23: 34780ms ~ 35356ms (file: 167-167-106-306-675-567-567-765-756-572-725-732-132-261-267-203-132-103-132-103-132-130-301-231-321-260-725-765-306-320-632-630-231-263-103-105-105-157-135-316-132-562-562-261-2_V.mp3)
  Segment 24: 36356ms ~ 36932ms (file: 761-763-631-671 134-132 671-623-632-623-263 167-617-163-613-163 576-567-652-612_W.mp3)
  Segment 25: 37932ms ~ 38508ms (file: 267-267-267-267-627-267-620-620-103-132-136-130-267-627-267-621-103-132-312-367-136-361-167-617-132-136-106-103-136-130-576-756-726-765-765-765-675-167-675-153-632-165-153-576-1_W.mp3)
  Segment 26: 39508ms ~ 40084ms (file: 301-103-106-106-574-157-107-175-103-271-627-206-261-132-320-302-312-627-260-326-120-126-625-625-705-657-567-574-576-750-756-657-627-371-675-167-106-130-607-576-756-756-672-106-1_W.mp3)
  Segment 27: 41084ms ~ 41660ms (file: 136-126-362-326-315-362-316-651-576-165-361-326-321-321 316-316-361-312-316-362-152-576-657-675-627-671-361-612-613-361 716-637-361-153-157-670-751-674-763 675-672-673-567-576-5_W.mp3)
  Segment 28: 42660ms ~ 43236ms (file: 102-136-670-672-267-263-623-627-276-267-216-312-132-736-716-237-327-213-631-102-672-362-302-132-162-302-176-631-103-132-136-162-165-162-260-263-236-203-123-132-231-162-126-317-1_V.mp3)
  Segment 29: 44236ms ~ 44812ms (file: 136-716-167-675-670 316-321-312 146-132-123-132-132-137-136-132-136-123-136-137-123-134-123-132-632-326 231-376-152-615-571-261-572-126-210-275-275-270-273-271-273-273-271-237_V.mp3)
  Segment 30: 45812ms ~ 46388ms (file: 136-103-103-163-623-351-162-126-610-612-136-106-163-167-617-136-106-136-167-102-613-136-160-103-136-167-103-317-162-156-167-162-167-103-136-615-216-631-136-136-130-103-126-106-1_V.mp3)
  Segment 31: 47388ms ~ 47964ms (file: 123-132-761-715-127-236-206-527-725-725-726-721-706-273-752-725-275-760-765-261-526-206-206-236-132-132-312-312-132-143-413-132-302-326-312-213-326-312-312-317-302-360-603-261-2_V.mp3)
  Segment 32: 48964ms ~ 49540ms (file: 102-102-120-163-312-132-103-613 132-135-132-326-302 657-617-126-132-143-103-157-136-132-132-136-651-517-162-613-261-132-321-321-216-276-216-263-126-263-613-106-102-176-127-107-7_V.mp3)
  Segment 33: 50540ms ~ 51116ms (file: 162-126-216-216-127-127-120-125-512-521-521-572-251-325-132-315-125-123-213-215-123-231-215-123-257-321-213-532-572-357-123-135-312-126-231-231-235-256-251-256-256-251-236-213-5_W.mp3)
  Segment 34: 52116ms ~ 52692ms (file: 106-103-654-156-602-602-260-206-260-672-602-103-657-230-261-312-612-206-320-321-231-103-172-103-132-123-203-632-263-623-263-136-132-162-137-136-137-163-376-103-160-306-130-136-1_V.mp3)
  Segment 35: 53692ms ~ 54268ms (file: 572-237-236-574-567-576-756-657-567-567-567-576-657-567-657-576-576-567-567-567-567-576-576-657-547-567-576-536-576-567-576-576-547-567-576-567-576-576-567-576-567-576-574-576-5_W.mp3)
  Segment 36: 55268ms ~ 55844ms (file: 267-267-276-627-267-126-312-132-627-217-123-527-567-762-236-237-372-216-762-726-726-761-672-672-762-721-267-617-715-271-276-237-762-726-276-672-627-276-267-726-762-627-276-761-7_W.mp3)
  Segment 37: 56844ms ~ 57420ms (file: 612-326-267-765-725-326-306-160-260-260-625-562-362-263-362-603-132-126-360-106-167-610-163-621-267-627 165-657-671-617-576-756-263-306-267-567-574-540-105-154-576-326-635-631-5_W.mp3)
  Segment 38: 58420ms ~ 58996ms (file: 103-176-612-263-210-106-164-135-176-106-617-261-160-130-163-160-316-126-125-627-163-672-623-623-132-761-167-106-123-103-132-132-362-261-160-160-103-160-106-106-106-103-136-103-1_V.mp3)
  Segment 39: 59996ms ~ 60572ms (file: 126-560-361-652-312-123-231-537-573-601-162-106-136-361-163-617-106-167-163-136-301-316-173-137-163-132-127-653-652-126-631-126-620-260-621-602-261-501-105-103-163-756-316-163-3_V.mp3)
  Segment 40: 61572ms ~ 62148ms (file: 316-561-630-163-176-136-106-352-615-610-163-136-136-167-126-612-167 574-163-132-103-216-613-156-103-162-103 657-167-103-176-165-175-120-716-103-106-123-301-312-165-715-761-127-6_V.mp3)
  Segment 41: 63148ms ~ 63724ms (file: 612 126-743-162-312-106-176-750-576-574-765-576-720-203-107-260-130-627-260-162-301-301-312-376-531-513-602-136-756-756-753-672-675-576-572-576-165-206-301-321-315-175-173-637-7_W.mp3)
  Segment 42: 64724ms ~ 65300ms (file: 620-620-267-106-106-301-130-312 362-360-765-765-165-756-750-705-705-106-106-103-162-106-316-561-136-263-276-672-627-263-726-261-132-620-602-620-602-627-627-607-671-106-132-132-3_V.mp3)
  Segment 43: 66300ms ~ 66876ms (file: 216-710-706-671-652-163-213-376-576-367-102-156-530-532-325-315-127-756-123-312-236-307-302-302-307-231-301-312-320-302-301-376-317-136-103-310-302-321-132-327-327-320-231-230-2_V.mp3)
  Segment 44: 67876ms ~ 68452ms (file: 627-236-326-637-367-362-326-362-631-137-163-163-361-267-261-236-257-263-276-267-627-325-326-367-267-762-367-675-567-567-317-361-371-176-716-765-672-627-367-763-376-317-362-261-7_W.mp3)
  Segment 45: 69452ms ~ 70028ms (file: 316-162-162-132-126-126-132-162-126-123-123-126-126-132-210-201-102-127-126-103-132-132_V.mp3)
  Segment 46: 71028ms ~ 71604ms (file: 136-312-632-326-316-263-267-623-672-627-672-635-672-627-675-163-267-237-672-276-632-672-762-273-263-623-627-627-627-672-623-276-627-263-127-263-571-576-621 312-603-132-312-163 6_W.mp3)
  Segment 47: 72604ms ~ 73180ms (file: 165-156-156-165 756-765 213-312-321-312-132-126-162-123-123-312-312-123-132-157-157-517-315-321-312-132-312-312-126-123-321-135-132-135-132-213-517-576-572-157-157-157-157-126-1_W.mp3)
  Segment 48: 74180ms ~ 74756ms (file: 671-621-615-612-612-621-612-612-163-612-617-163-162-621-612-612-612-621-621-261-627-627-612-617-126-261-621-316-612-612-672-617-613-163-617-617-162-162-127-261-126-126-162-261-6_W.mp3)
  Segment 49: 75756ms ~ 76332ms (file: 673-130-310-610-163-160-610-167-631-610-612-620-103-162-102-103-106-103-136-132-615-612-617-316-163-756-765-672-267-670-617-671-672-162-613-620-617-167-617-167-612-126-613 705-7_W.mp3)
  Segment 50: 77332ms ~ 77908ms (file: 613-163-167-312-136-132-136-613-123-312-625-576-657-263-127-312-653-325-615-632-362-321-653-652-321-326-267-236-652-657-627-652-365-163-761-657-510-567-657-135-627-216-162-162-3_W.mp3)
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
> **Time**: 2026-04-03 09:54:53
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
*Generated by WKAppBot ask gemini — 2026-04-03 09:54:53*
