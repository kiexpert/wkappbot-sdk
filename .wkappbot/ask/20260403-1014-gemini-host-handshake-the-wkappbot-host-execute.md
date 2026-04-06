# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=10:13:45  uptime=32071s  mem=1764MB  system_processes=446"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_101335.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 617-130-106-602-312-301-601-260-126-601-760-705-273-102-103-167-261-231-236-231-312-302-203-231-236-203-231-310-132-132-103-657-260-106-126-630-162-150-510-105-105-176-135-103-1_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 617-167-167-672-206-620-162-163-613-607-670-602-762-610-162-612-576-570_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 641-126-756-576-765-136-316-650-163-320-623-263-623-302-326-307-713-137-761-671-507-203-203-263-213-236-123-126-103-136-106-625-601-162-162-106-162-765-675-675-671-675-267-260-2_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 637-375-506-507-351-631-365-361-157-153-675-725-321-362-612-316-167-621-620-602-725-675-620-120-102-126-167-105-730-107 507-560-136-321-312-126-260-623-263-126-261-326-263-632-2_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 621-106-321-102-103-362-326-130-312-765-621-621-760-726-762-675-607-607-670-301-320-236-630-576-301-130-173-103-106-503-517-132-673-632-167-621-362-160-602-602-562-504-301-312-3_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 103-756-107-610-136-156-103-271-167-106-167-136-167-263-162-160-103-156-167-217-126-726-271-102-213-260-210-210-172-123-126-124-150-102-120-210-102-120-210_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 620-627-620-103-765-765-312-763-367-316-371-623-713-103-132-325-617-106-301-310-103-301-132-130-102-160-216-316-215-630-163-526-162-261-627-267-267-126-756-751-123-172-756-753_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 362-326-215-531-157-561-567 167-103-620-351_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 235-275-215-251-524-532-321-231-251-257-276-276-276-265-265-251-321-263-132-126-162-127-356-152-172-275-275-571-527-523-523-251-251-613-316-312-152-215-265-250-572-567-125-126-1_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 751-175-574-517-571-751-731-754-157-725-723-721-725-752-271-123-132-312-362-632-361-163-162-136-102-310-103-312-310-316-312-316-705-317-173-137-136-236-261-623-627-703-763-376-3_V.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 132-137-160-316-130-301-132-325-172-210-301-610-632-613-136-610-136-103-136-506-103-107-162-576-756-756-765-276-765-726-576_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 136-167-173-571-761-376-732-326-136-312-163-173-127-126-216-127-213-231-213-521-756-572-572-572-572-527-562-257-257-521-256-523-325-257-235-251-215-265-127-301-316_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 320-263-362-136-163-302-102-263-263-263-627-627-261-310-301-137-301-370-173-176 574-756-765-675-567-675-627-206-260-620-132-102-162-167-312-261 312-103-130-310-301-310-130-361-3_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 376-631-132-312-136-175-170-265-627-672-675-765-765-705-150-130-612-103-136 126-132-673-672-365-613-132-103-173 172-103 736-761-672-762-762-715-301-376-127-756-763-574-570-157-3_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 132-137-103-136-103-605-617-136-516-132-670-571-175-106-176-130-136-130-173-701-705-176-176-123-175-765-760-750-750-107-623-162-617-127-126-136-176-756-102-705-163-167-301-306-3_V.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 637-631-617-612-123-321-671-612 173-721-172-271-613-631-617-106-672-670-162-126_W.mp3)
  Segment 17: 25216ms ~ 26332ms (file: 132-263-103-130-130-130-103-163-761-103-263-163-103-130-761-126-603-276-176 106-132-623-362-613-637-103-106-361-512-132-103-103-123-316-172-127-137-216-156-162-762-162-136-765-1_V.mp3)
  Segment 18: 27332ms ~ 27908ms (file: 236-236-301 617-523 625-627-627-672-671 671-673-671-671-672 765-762-765-675-675-632-672-673-617-673 276-213-216-230-236-341-213-326-362-310-310-627-607-623-630-263-627_W.mp3)
  Segment 19: 28908ms ~ 29484ms (file: 560-567-316-163-621-621-263-623-310-103-310-321-326-106-267-625-261-126-327-321-301-632-261-574-574-574-105 672-175-672-623-263-236-236-623-130-561-657-675-261-267-267-267-621-6_V.mp3)
  Segment 20: 30484ms ~ 31060ms (file: 123 671-167-167-132-132-615-167-301-320-312 132 673-210 132-765-756-765 671-267 712-751 162-765 301-321-132-362-675-761-716-765-670_W.mp3)
  Segment 21: 32060ms ~ 32636ms (file: 365-130-103-130-106-210-231-106-106-106-106-106-103-326-623-260-260-302-206-103-260-260-620-632-263-261-652-605-612-162-657-605-613-612-617-612-260-326-203-103-102-106-103-130-2_V.mp3)
  Segment 22: 33636ms ~ 34212ms (file: 765-760-657-765-675-671-761-760-765-760-675-675 765-761-672-675 372-236-326-137-210-210_W.mp3)
  Segment 23: 35212ms ~ 35788ms (file: 123-102-216-613-760-756-760-675-167-316-106-103-103-163-106-316-132-126-130-310-301-103-136-613-601-613-612-601-613-203-230-320-320 321-213-132-231-123-321-321-621-321-321-321-1_V.mp3)
  Segment 24: 36788ms ~ 37364ms (file: 170-312-130-130-761-367-621-123-213-123-162-163-106-175-167-127-132-263-256-231-176-765-762-163-130-306-310-130-627-561-652-153-760-756-136-657-752-507-132-732-632-267-213-132-2_W.mp3)
  Segment 25: 38364ms ~ 38940ms (file: 263-321-132-231 137-657 132 675-612-617-627-673-627-621-617-761-673 263-327-217-102-613-132-136-136-106-103-231-263-632-162-312-130-163-571-136-167-176-715-107-136-130-136-162-1_V.mp3)
  Segment 26: 39940ms ~ 40516ms (file: 326-321-312-103-167-231-327-523-135-265-576-362-136-103-276-267-302-132-132-123-123-201-312-132-216-231-312-231-275-275-321-103-310-132-136-132-537-310-132-137-572-152-102-132-3_V.mp3)
  Segment 27: 41516ms ~ 42092ms (file: 761-106-376-627-627-756-621-216-126-236-126-310-130-231-102-276-132-612-260-623-236-263-627-265-261-162-321-103-102-126-562-103-163-136-135-125-157-615-263-231-130-627-103-106-1_V.mp3)
  Segment 28: 43092ms ~ 43668ms (file: 123-162-123-163-103-301-316-312-135-570-576-756-716-305-531-672-625-620-263-263-507-365-302-301 520-526-102-103-236-325-103-130-312-130-310-301-102-203-230-320-302-312-312-315-1_W.mp3)
  Segment 29: 44668ms ~ 45244ms (file: 572-576-357-357-537-356-357-537-576-135-215-365-375-361-137-574-765-157-357-657-657-675-617-637-765-574-756-752-752-652-157-562-576-756-574-517-576-576-573-513-563-537-567-574-7_W.mp3)
  Segment 30: 46244ms ~ 46820ms (file: 257-263-346-214-623-216 176-162-136_V.mp3)
  Segment 31: 47820ms ~ 48396ms (file: 612-162-136-126-137-216-132-134-143-132-143_V.mp3)
  Segment 32: 49396ms ~ 49972ms (file: 163-512-261-106-176-316-163-136-106-136-756-167-106-106-167-165-132-231-261-125-167-103-126-652-103-163-167-613-316-215-316-162-136-362-163-106-136-361-136-103-132-103-126-312-1_V.mp3)
  Segment 33: 50972ms ~ 51548ms (file: 621-261-261-507-576-576-756-103-160-106-137-136-103-136-135-153-105-154-157-136-157-261-547-504-625-103-103-136-106-163-106-103-136-103-107-107-137-107-103-106-103-107-103-136-2_V.mp3)
  Segment 34: 52548ms ~ 53124ms (file: 126-167-103-160-162-103-671-167-615-610-106-672-326-103-715-162-263-312-671-167-106-617-301-162-106-176-106-136-163-163-576-176-136-615-132-103-136-107-103-615-150-156-756-126-1_V.mp3)
  Segment 35: 54124ms ~ 54700ms (file: 135-137-137-371-763-135-316-315-357-357-167-361-361-317-563-516-625-562-517-130-301-105-521-567-571-517-627-627-627-267-263-627-267-627-236-352-357-365-267-236-562-523-536-576-5_W.mp3)
  Segment 36: 55700ms ~ 56276ms (file: 521-132-513-132-137 172-762-657-657 172-127-176-761-715-716-675-762 751-176-173-715-317-721-761-312-725-176-127-762-671-762-715-751-756-761-127_W.mp3)
  Segment 37: 57276ms ~ 57852ms (file: 576-354-631-126-627-216-612-213-136-163-154-617-630-132-103-126-263 752-752-713-314-136-123-135-574-574-574-132-361-213-712-301-316-132-312-326-241-361-123-132-612-360-632 213-3_V.mp3)
  Segment 38: 58852ms ~ 59428ms (file: 725-751-752-705-750-753-273-273-273-276-276-275-132-123-123-275-275-237-203-203-231-302-175-162-253-235-326-235-325-372-312-125-213-213-237-237-321-123 175-120-752-157-237-216-3_W.mp3)
  Segment 39: 60428ms ~ 61004ms (file: 106-103-106-103-136-103-103-105-137-574-156-561-672-612-162-106-126-120-106-671-136-160-103-571-206-123-162-176-165-165-167-301-106-163-705-765-672-167-750-276-162-102-107-136-2_V.mp3)
  Segment 40: 62004ms ~ 62580ms (file: 657-572-762-675-267-267-267-756-705-672-672-625-670_W.mp3)
  Segment 41: 63580ms ~ 64156ms (file: 672-162-316-132-231-267-276-276-267-321-216-213-231-236-213-162-216-726-276-672-726-672-627-672-762-726-261-261-267-263-263-263-236-265-265-263-312-132-136-132-321-261-267-312-3_W.mp3)
  Segment 42: 65156ms ~ 65732ms (file: 765-657-670-762-762-620-625-602-621-621-576-675-607-765-762-674-163-172-126-706-765-701-716-162-123-130-365 236-451-613 136-126-123-123-213-341-721-561-765-765_V.mp3)
  Segment 43: 66732ms ~ 67308ms (file: 351-312-312-132-561-165-135-136-136-531-543-537-513-357-735-751-375-351-536-157-157-571-125-523-572-275-217-265-261-576-572-567-375-371-371-236-321-213-532-576-526-265-625-263-2_W.mp3)
  Segment 44: 68308ms ~ 68884ms (file: 672-627-760-760-706-736-713-572-756-570-574-627-627-170-132-103-176-162-301-321-327-273-602-720-236-627-627-267-675-765-675-675-675-615-167-756-576-706-670-607-627-627-637-157-7_W.mp3)
  Segment 45: 69884ms ~ 70460ms (file: 312-132-316-163-136-231-276-127-362-623-376-162-162-760-670-763-637-705-750-703-756-612-167-623-756-750-706-267-263-763-765-176-167-176-176-175-705-573-570-713-376-136-672-671-5_V.mp3)
  Segment 46: 71460ms ~ 72036ms (file: 321-132-173 107-107-167-613-620-106-103-137-130-103-517-132-675-612-621-106-106-675-675-107-167-675-576-631-613-675-163 617-316-167-103-135-136-163-137-103-165-163-163-127-126-1_V.mp3)
  Segment 47: 73036ms ~ 73612ms (file: 620-620-261-623-260-260-623-263-261-260-261-231-261-213-625-265-261-623-627-627-621-617-612-621-621-362-612-621-162-617_W.mp3)
  Segment 48: 74612ms ~ 75188ms (file: 576-567-561-567-567-265-267-267-267-267-265-256-265-267-267-267-265-263-672-367-635-367 571-631-610-623-621-163-635-635-631-512-351-316-513-536-631-613-136-563-635-162-136-231-3_W.mp3)
  Segment 49: 76188ms ~ 76764ms (file: 136-613-216-160-120-106-163-130-271-136-621-162-613-136-623-312-165-321-132-631-615-103-760-175-156-163-613-136-130-103-130-103-130-123-130-106-132-321-136-137-613-136-103-106-1_V.mp3)
  Segment 50: 77764ms ~ 78340ms (file: 362-637-723-375-375-523-253-235-527-612-621-125-162-165-352-165-365-153-315-325-265-362-326-136-516-536-356-532-356-325-356-365-375-357-356-375-653-325-352-325-215-132-235-326-2_W.mp3)
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
> **Time**: 2026-04-03 10:14:25
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
*Generated by WKAppBot ask gemini — 2026-04-03 10:14:25*
