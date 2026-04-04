# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=14524  time=05:28:59  uptime=6330s  mem=2012MB  system_processes=807"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260401_052851.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 216-321-231-267-276-236-261-326-236-326-236-263-236-263-275-263-613-162-261-365-536-527-325-132-316-163-235-523-512-532-532-321-136-316-236-316-123-316-136-127-132-126-216-127-2_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 316-736-713-172-731-371-732-371-312-316 167 627-162-627-125-126-632-675-301-136-627-627-623-263-625_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 103-132-132-123-106-316-132-205-672-507-570-105-513-315-136-136-103-176-135-137-137-160-106-136-132-135-132-136-103-136-517-136-103-167-176-625-617-163-310-302-123-321-310-230-1_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 316-267-672-725-103-127-173-306-167-106-317-316-671-267-162-130-617-103-675-160-163-160-107-326-126-163-571-132-103-136-102-316-136-160-163-106-103-103-153-132-136-216-160-103-1_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 762-761-765-765-165 631-631-367-675-765-762-765-756-762-765-765-260-672-716-762-762-275-726-327-712-210-207-276-126_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 263-321-326-326-325-521-652-653-356-351-534-540-534-351-356-351-315-315 315-326-326-362-531-362-326-362-637-632-165-631-136-163-361-315-316-156-315-543-513-513-517-153-156-156-1_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 312 103-175-156 102-316-103-360-601-576-362-360-143 103-123-172-170-107-172-176-107-261-120-130-103-157-157-236-326-571-136-103-612-132-516-672-601-167-625-106-321-675-756-103-1_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 106-136-617-163-136-137-136-103-132-136-160-715-136-136-106-613-165-103-102-162-103-106-103-160-136-106-657-126-716-172-167-136-567-361-163-136-106-301-160-152-612_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 162-106-765-627-326-162-130-360-670-607-756-756-671-302-620-362-362-132 563-156-156-126-236-671-632-123-106-361-210-217-261-163-326-671-136-162-126-657-126-612-130-260-102-127-2_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 253-537-165-265-256-625-652-672-621-267-362-356-312-356-365-632-267-126-125-176-261-267-261-516-135-615-126-625-162-547-516-536-153-534-534-537-543-534-253-253-612-162-251-256-5_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 132-132-165-176-165-617-567-657-567-165-561-567-576-576-567-657-576-567-576-567-576-765-576-576-756-756-576-576-756-765-567-657-567-576-657-567-657-675-675-675-675-756-657-576-5_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 163-261-316-103-167-132-761-106-136-651-162-216-601-601-612-130-106-165-106-532-167-216-231-632-612-516-501-126-315-312-672-672-623-627-267-721-574-756-567-130-103-126-136-157-1_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 207-602-672-670 602-362-267-623-302-103-316-216-236-136-632-612-163-167-564-675-670-627-306-310-130-132-153-571-257-257-302-321-103-316-301-132-137-761-176-716-176-574-754-756 1_V.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 267-675-763-126-627-261-173-716-632-316-361-267-261-126-312-316-675-672-672-265-625-263-625-625-612-267-623-621-623-263-362-361-316-531-261 531-537-357-357 567-657-627-265-625-6_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 765-756-157 612-617-162-613-617-167-162_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 230-320-306-630-136-163-106-301-301-302-312-136-310-320-326-312-302-613-631-162-320-362-302-301-621-261-267-261-312-132-107-765-216-627-263-623-261-621-251-125-102-107-271-175-1_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 627-601-725-276-736-675-672-625-672-263-230-267-136-632-162-216-261-625-160-612-617-162-123-671-162-671-675-671-574-576-263-603-603-632-631-613-612-620-260-260-620-260-620-637-6_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 132-217-123-213-173-176-163-312-316-301-570-613-167-631-637-301-316-136-312-367-627-760-760-760-612-162-213-263-263-267-263-265-203-120-126-162-612-610-162-516-570-576-576-576-1_V.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 170-167-170-126-123 137-326-632-132-132-672-621-675-613-632-162-612-627-612-163-136-123-621-627-123-167-216-123-162-312-152-312-312-132-123-136-631-123-632_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 163-132-236-321-103-130-230-103-213 105-106-136-756-301-103-163-316 123-132 102-103-132-134-103-761-170-176-107-107-173-623-612 167-167-167-167-673-673_V.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 167-136-103-136-163-316-615-102-162-301-265-127-126 136-103-136-136-167-263-321_V.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 136-132-762-672-672-670-623-261-132-312-162-136-572-207-130-301-316-132-570-574-527-263-231-123-302-236-362-106-103-162-163-167 237-321 613-362-132-103-106-163-136-103-160-136-7_V.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 623-126-301-102-103-102-321-102-136-312-213-312-123-320-361-326-316-236-216-213-312-321-310-312-163-132-163-216-316-321-326-312-130-316-612-562-176-261-316-325-326-236-601-261-1_W.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 310-103-513-702-703-162-160-106-103-756-230-627-136-362-126-260-102-236-263-756-753-762-267-276-627-257-657-675-267-375-765-675-756-726-765-762-107-675-627-176-657-672 102-104-1_W.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 762-762-657-672-652-672-627-256-652-625-623-627-267-526-527-236-362-275-615-265-762-632-675-267-635-672-765-267-612-326-627-572-753-675-627-675-657-573-276-652-216-267-635-576-6_W.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 273-167-136-130-106-103-312-162-621-263-162-162-132-326-136 621-632-126-632-610-160-163-103-215-132-610-675-120-136-103-103-106-316-176-312-160-106-761-671-701-675-716-275-617-1_V.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 675-675-657-156-176 765 372-307-320-302-307-376-376-367-637 356-307-756-670-301-175_W.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 567-564 267-627-263-627-623-627-623-623-627-263-623-627-625-263-265-267-627-267-267-267-265-261-267-627-265-267-265-261-263-621-263-263-263-632-362-627-267-263-623-263-623-263-2_W.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 563-536-567-567-567-561-576-567-657-567-561-563-537-536-356-536-315-356-513-560-530-503-305-306-516-517-305-653-563-506-150-130-657-506-571-531-507-507-357-563-571-561-531-176-6_V.mp3)
  Segment 30: 45704ms ~ 46748ms (file: 610-617-612-103-361-167-216-765-162-671-176-137-172-175-136-137-105-175-301-326-136-126-160-106-156-216-136-137-671-607-607-306-367-673-637-763-763-675-756-765-673-673-670-607-3_V.mp3)
  Segment 31: 47748ms ~ 48324ms (file: 132-123-132-127-753-765-765-765-765-765-765-765-763 130-321-230-203-267-103-167-675-675-601-160-763-765-756-675-765-765-763-761-302-320-362-623-306-301-306-261-672 657-760-632-7_W.mp3)
  Segment 32: 49324ms ~ 49900ms (file: 541-276-275-276-627-675-765-672-675-567-675-761-765-762-267-762-276-276-273-276-276-265-760-675-675-675-716-627-752-625-637-627-362-316-231-213-312-312-315-132-312 102-217-265 3_V.mp3)
  Segment 33: 50900ms ~ 51476ms (file: 317-136-120-210-260-203-260-263-302-302-610-160-163-106-130-267-602-627-306-367-673-627-316-632-627-627-267-627-627-761-765-176-671-765-765-672-675-765-652-321-312-136-317-162-6_W.mp3)
  Segment 34: 52476ms ~ 53052ms (file: 675-657-103-103-107-105-162-621-613-165-156-576-167-621-627-163-162-165-162-102-126-126-105-501-576-501-105-157-576-167-156-152-751-315-157-137-675-657-567-657-567-750-765-672-6_V.mp3)
  Segment 35: 54052ms ~ 54628ms (file: 756-613-576-574-572-170 765-762-765-762-765-751-765_W.mp3)
  Segment 36: 55628ms ~ 56204ms (file: 213-526-673-632-267-762-756-621-231-261-362-230-320-301-310-312-310-301-103-132-136_V.mp3)
  Segment 37: 57204ms ~ 57780ms (file: 706-756-756-763-576-573-256 675-675_W.mp3)
  Segment 38: 58780ms ~ 59356ms (file: 314-261-260 236-301-302-321-302-506-573-325-132-103-106-103-235-530-572-507-602-602-562-756-765-761-765-706-673-670-672-302-326-362-613-756-157-310-305-675-672-167-163-316-671-1_V.mp3)
  Segment 39: 60356ms ~ 60932ms (file: 176-163-164-103-670-670-756-762-312-103-162-136-317-132-164-163-134-132-756-260-302-103-576-657-167-765-672-362-623-162-106-130-567-265-260-620-620-620-765-765-675-672-627-620-6_W.mp3)
  Segment 40: 61932ms ~ 62508ms (file: 163-126-126-123-602-260-632-163-671-761-176-176-162-360-632-627-675-613-602-621-637-163-162-162-163-672-756-756-206-126-136-162-103-132-213-302-162-601-106-612-162-617-230-130-6_V.mp3)
  Segment 41: 63508ms ~ 64084ms (file: 571-567-571-576-576-351-356-357-357-735-637-637-635-357-576-567-637-367-713-763-356-651-657-765-657-675-567-567-576-567-576-567-576-567-576-576-576-537-573-536-573-576-567-567-5_W.mp3)
  Segment 42: 65084ms ~ 65660ms (file: 763-367 756-705-607-321-312-130-310-132 673-527-540-653-365-756-756-765-267-637-132-716-326 126-132-312-301-312-103-103-103-163-103-132 316-216-236-206-261-206-206-312-613-132-3_V.mp3)
  Segment 43: 66660ms ~ 67236ms (file: 203-276-263-237-237-627-267-263-620-637-627-267-627-675-216-267-265-625-652-657-675-675-675-675-675-675-675-675-675-675-675-765-765-675-725-657-675-756-765-276-675-672-762-675-6_W.mp3)
  Segment 44: 68236ms ~ 68812ms (file: 163-106-501-615-167-136-136-316-675-627-167-162-136-106-163-627-617-267-107-163-103-106-601-602-602-620-765-670-607_W.mp3)
  Segment 45: 69812ms ~ 70388ms (file: 610-103-103-103-132-103-312-605-136-136-132-106-175-176-162-620-103-136-136-163-174-201-231-123-123-132-317-132-106-156-574-206-203-321-302-312-136-130-132-103-576-752-725-750-1_V.mp3)
  Segment 46: 71388ms ~ 71964ms (file: 132-310-361-167-602-602-617-671-612-627-216-136-167-176-162-167-163-130-301-312-730-736-137-754-617-605-651-106-162-106-136-136-627-106-213-102-135-137-312-132-106-132-261-630-3_V.mp3)
  Segment 47: 72964ms ~ 73540ms (file: 261-216-126-261-267-261-612-261-627-261-263-216-623-612-263-261-261-132-236-253-236-326-162-261-126-132-216-125 623-312-617-617-760-612-610 362 761-367-267-610-163-162-136-163-1_W.mp3)
  Segment 48: 74540ms ~ 75116ms (file: 103-123-167-217-230-236-206-257-765-627-162-607-675-675-675-627-627-621-263-162-612-671-673-623-136-617-623-670-620-260-263-260-261-621-260-206-620-612-206-620-627-167-170-765-1_W.mp3)
  Segment 49: 76116ms ~ 76692ms (file: 675-673-617-126-625-627-236-132-231-321-263-326-627-672-126-132-261-236-236-263-231-261-231-261-126-612-107-103-301-301-762-762-327-672-176-236-163-132-316-106-260-130-751-725-7_W.mp3)
  Segment 50: 77692ms ~ 78268ms (file: 603-675-265-561-576-561-216-216-265-570-675-756-175-576-751-756-576-657-615-675-267-261-617-671-617-612-602-607-671-672-627 103-167-610-163-156-126-103-156-167-137-176-106-671-1_V.mp3)
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
> **Time**: 2026-04-01 05:29:51
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "포커스 양보가 필요합니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "포커스", "start_ms": 15, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "양보가", "start_ms": 200, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "필요합니다", "start_ms": 370, "dur_ms": 195, "phase_lr_deg": 0.0}
]
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
*Generated by WKAppBot ask gemini — 2026-04-01 05:29:51*
