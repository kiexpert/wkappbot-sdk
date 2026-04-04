# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=14524  time=05:17:12  uptime=5622s  mem=1784MB  system_processes=785"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260401_051702.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 302-625-627-263-623-623-316-326-136-103-167-361-671-637-316-136-316-132-143-257-675-316-316-312-132-123-314-132-132-132-164-134-135-132-134-143-213-132-153-673-126-123-136-167-7_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 247-162-163-124-312-132-123-314-132-312-132-312 267-126-162-756-106-106-165-136-103-623-621-106-617-657-612-162-761-103-651-130-167-106-103-263-162-163-306-623-123-103-536-362-1_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 653-160-316-137-136-135-106-625-123-631-631-623-603-617-657-103-167-371 761-765-760-761-760-765-761-762-672-120-132-132-130-370-570-765-765-763-765-765-765-760-763_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 623-327-236-627-263-326-632-236-260-327-237-326-237-326-326-237-237-273-237-372-732-372-371-736-372-637-367-627-326-627-652-632-362-326-362-360-637-367-367-265-263-276-235-672-2_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 267-726 765-167-106-136-316-301-136-764-634 126-617-637-267-261-261 567-235-267-267-627-263-103-103-103-103-103-130-301-306-623 623-602-560-675-671-763-675-673-361-361-316-612-3_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 267-207-210-265-546-564-540-351-354-540-546-501-540-573-357-534-514-517-514-562-567-657-317-316-371-362-362-172-326-236-237-261-261-263-321-213-126-312-123-312-321-631-621-126-6_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 236-263-263-623-625-265-657-231-136-236-623-351-562-652-625-265-265-263-263-263-265-362-216-261-265-263-267-261-621-261-263-362-625-623-263-623-263-162-621-576-657-652-625-236-5_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 261-537-253-512-652-216-126-137-156-517-152-156-156-256-251-135-157-532-517-521-523-352-213-312-231-321-132-312-312-352-213-321-231-261-236-236-623-326-125-125-561-572-526-512-1_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 765-764-763-756-267-270-762-762-765-765-765-671-765-765-765-762-716-761-765-765-706-750-756-756-765-762 672-267-627-267-163-261-612-621-627-623-326-316-623-261-605-524-542 756-7_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 237-267-267-326-351-315-153-375-351-325-135-326-213-312-321-631-321-325-312-365-325-213-362-263-621-213-236-261-623-263-627-623-637-263-326-327-372-376-365-136-317-367-361-136-3_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 621-617-612-621-621-612-621-216-326_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 517-567-537-516-531-537-567-531-516-561-156-561-516-165-156-165-156-135-351-153-651-561-567-567-165-615-657-651-517-561-567-657-657-657-567-567-567-567-657-657-567-675-657-576-5_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 623-620-326-320-302-231-107-157-173-107-306-602-520-572-527-321-307-307-726-260-265-210-203-130-320-130-132-301-321-130-103-130-106-136-613 237-236-326-176-263-236-326-230-326-1_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 125-672-160-765-765-765-670-657-765-765-672-670-670-670-760-671-362-263-326-623-621-163-162-163-136-617 136-310-103-672-136-136-126-675-765-627-671-610-163-163-123-136-513 261-2_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 273-732-325-352-235-362-523-527-573-752-752-725-752-735-725-723-612-261-532-326-216-621-326-540-235-562-236-213-562-526-256-263-572-275-275-275-276-273-726-276-276-253-236-263-2_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 627-621-612-162-126-627-627-167-612-675-675-132-710-761-705_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 612-162-617-137-103-156-137-136-547-156-135-103-106-103-103-132-136-136-107-152-106-541-751-765-765-657-156-610-263-206-413-762-263-713-103-603-613-106-162-132-312-132-413-132 1_V.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 576-572-576 756-753-756-751-750-760-167-173-162 612-671-162-762-126-163-103-621-316-156-613-316-123-165-162-132-162-102-617-671-167-657-160-162-163-106-103-106-160-137-365-671-6_V.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 576-576-516-572-103-671-106-167-136-106-136-163-103-617-106-176-167-132-102-671-362-526-231-263-216-127-276-527-521-576-275-576-275-321-576-120-632-672_V.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 231-213-231-103-162-231-321-326-321-326-576-361-316-162-367-236-326-623-237-736-326-361-312-312-132-163-160-162-163-316-130-160-163-163-721-312-312-320-213-130-102-253-126-167-5_W.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 302-210-263-321-103-206-260-620-627-260-126-623-126-362-321-132-136-362-621-613-623-136-126-206-625-632-620-623-216-267-627-263-267-261-261-675-627-567-103-160-610-106-103-106-1_V.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 465-602-163-601-601-621-620-765-750-567-765-716-132-231-326-261-176-274-126-142-352-316-623-607-703-706-673_V.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 627-627-761-632-261-236 130-312-162-316 102-130-130-126-201-123-231-106-160-610-320-130-132-310-132-136 574-706-160-612-132-570-107-132-130-137-123-301-103-163-760-612-162-621-6_V.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 576-576-576-567-576-576-567-576-576-576-567-567-567-567-567-652-567-657-657-567-567-563-653-567-567-567-567-576-657-567-562-523-562-536-657-567-563-536-652-562-567-567-562-567-5_W.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 371-564-623-317 675-561-106-136-163-126-136-620-623-167-621-136-136-103-103-126-176-162-106-607-671-671-361-103-170-617-136-162-612-657-751-657-672-671-612-670-167-761-617-627-6_W.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 201-261-574-751-765-765-132-102-302-320-236-320-230-362-263-326-320-132-106-136-103-136-163-136-150-165-167-162-163-210-321-301-310-301-103-260-206-576-756-752-237-237-203-206-2_V.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 316-650-651-613-631-106-106-176-137-316-103-651-107-107-107-576-716-571-517-157-301-326-302-362-326-362-326-132-603-236-465-267-103-103-106-102-102-172-167-162-132-316-673-126-1_V.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 103-167-106-130-301-675-163-106-105-130-165-153-301-107-716-361-176-265-132-103-517-571-576-675-657-657-163-517-567-165-673 163-163-176-625-625-625-371-361-362-673-360-632-362-3_W.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 361-162-716-673-167-623-631-167-756-716-576-170-612-126-675-675-675-617-176-576-675-675-652-615-651-657-627-620-263-260-236-263-236-136-162-731-716-371-120-123-126-127-632-106-3_W.mp3)
  Segment 30: 45704ms ~ 47612ms (file: 321-132-123-216 165-615-106-527-235-753-762-726-176-137-752-310-132-103-372-327-316-301-106-162-156-206-312-321-312-325-726-762-672-765-621-617-132-572-571-153-106-103-123-163-1_W.mp3)
  Segment 31: 48612ms ~ 49188ms (file: 312-215-213-123-136-326-312-231-326-231-213-123-231-321-321-321-321-531-354-531-351-231-312-123-127-321-127-672-327-375-273-713-231-256-231-231-532-312-132-123-217-251-217-127-2_W.mp3)
  Segment 32: 50188ms ~ 50764ms (file: 613-632-367-136-310-301-360-167-736-706-706-706-132-623-316-302-320-326-236-132-301-361-732-231-263-230-321-605-675-750-652-765-765-316-316-632-362-613-706-106-256-625-627-602-3_W.mp3)
  Segment 33: 51764ms ~ 52340ms (file: 135-675-136-652-132-612-102-617-130-162-613-617-136-612-361-163-106-671-106-123-671-176-106-167-163-671-160-236-756-631-136-256-163-675-162-163-316-312-162-102-150-263-213-123_V.mp3)
  Segment 34: 53340ms ~ 53916ms (file: 103-312-361-631-361-316-362-260-206-620-206-206-327-327-257-762-763-760 765-675-756-765-765-267-267-206-620-675-675-612-163-360-632-263-263-236-265-256-621 763-763-670-763-670-1_W.mp3)
  Segment 35: 54916ms ~ 55492ms (file: 137-673-162-126-162-612 627-675-670-760-760-317-123-162-627-672-326-132-213-130-132-761 213-612-123-123-213_W.mp3)
  Segment 36: 56492ms ~ 57068ms (file: 360-260-306-320-160-617-560-561-540-765-675-620-627-627-652-230-302-302-302-103-263-267-326-320-136-132-136-103-106-302-310-612-163-103-130-675-605-601-106-610-326-236-610-106-1_W.mp3)
  Segment 37: 58068ms ~ 58644ms (file: 563-563-261-321-216-126-236-236-321-623-213-613-236-352-356-256-326-326-326-153-532-231-312-326-321-362-267-265-612-213-125-315-562-312-361-326-325-321-261-261-236-263-263-260-6_W.mp3)
  Segment 38: 59644ms ~ 60220ms (file: 361-367-617-361-367-137-135-513-361-316-637-367-321-267-326-326-132-162-132-136-236-236-263-236-263-263-261-263-371-531-521-267-627-267-267-267-263-267-625-326-236-253-365-635-3_W.mp3)
  Segment 39: 61220ms ~ 61796ms (file: 617-162-312-162-162-621-106-570-576-756-132-326-675-675-675-675-675-136-675-705-106-123-715-107-173-316-301-651-176-106-103-137-103-567-607-306-162-102-617-106-602-201-126-621-1_W.mp3)
  Segment 40: 62796ms ~ 63372ms (file: 570-105-105-756-716 627-672-267-265-627-267-267-627-627-627-621-267-627-267-267 762-762_W.mp3)
  Segment 41: 64372ms ~ 64948ms (file: 320-310-310-231-361-136-632-213-236-126-312-620-623-132-103-132-263-623-623-261-216-132-321-132-136-132-162-326-136-623-136-123-312-163-103-160-312-163-623-623-620-163-136-132-6_W.mp3)
  Segment 42: 65948ms ~ 66524ms (file: 103 136-361-316-173-132-137-163-107-216-206-260-260-206-206-206-103-162-316-136-260-623-163-106-103-102-201-213-132-126-126-213-620-260-260-206-260-621-632-132-206-260-320-230-3_V.mp3)
  Segment 43: 67524ms ~ 68100ms (file: 260-310-657-675-657-567-657-650-260-302-326-130-132-103-103-103-106-312-574-570-206-206-206-206-302-504-570-750-307-312-203-236-326-103-132-362-231-143-132-413-132-413-132-143-1_V.mp3)
  Segment 44: 69100ms ~ 69676ms (file: 132-127-126-263-236-216-267-267-263-216-231-261-236-261-261-261-216-261-265-261-216-216-213-213-216-126-263-261-261-267-216-267-261-216-267-276-267-276-267-267-265-267-267-267-2_W.mp3)
  Segment 45: 70676ms ~ 71252ms (file: 607-672-607-132-637-176-173-671-670-670-607-627-267-627-267-236-263-231-263-265-320-302-320-623-230-602-326-321-132-312-301-310-310-630-601-136-176-173-106-601-615-136-102-130-3_V.mp3)
  Segment 46: 72252ms ~ 72828ms (file: 103-106-163-103-517-765-706-761-614-301-327-320-236-376-103-105-351-103-136-316-162-103-103-132-103-312-302-230-132-173-132-321-320-132-103-123-132-126-103-312-321-326-263-623-6_V.mp3)
  Segment 47: 73828ms ~ 74404ms (file: 267-627-267-267-627-261-263-236-263-261-261-621-267-621-261-621-261-621-261-621-261-263-326-267-267-263-267-267-267-627-267-261-267-627-276-672-263-267-267-263-267-263-267-267-2_W.mp3)
  Segment 48: 75404ms ~ 75980ms (file: 263-621-263-236-263-623-623-263-362-263-263-326-231-267-236-276-627-627-612-627-627-627-627-261-621-162-627-621-326-326-265-362-312-321-261-632-326-326-265-263-267-267-326-236-3_W.mp3)
  Segment 49: 76980ms ~ 77916ms (file: 310-236-263-362-176-673-623-302-627-576-635-631-637-632-362-573-756-320-302-230-326-302-306-102-302-301-301-301-360-310-206-632-103-136-163-130-301-132-103-205-257-176-167-106-1_W.mp3)
  Segment 50: 78916ms ~ 79492ms (file: 675-756-671-163-602-602-162-106-163-320-326-675-657-605-657-657-216-621-126-125-123-102-102-236-203-260-206-206-751-756-631-620-623-160-106-126-217-216-216-620-602-162-162-601-6_V.mp3)
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
> **Time**: 2026-04-01 05:18:04
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
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "포커스", "start_ms": 10, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "양보가", "start_ms": 200, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "필요합니다", "start_ms": 370, "dur_ms": 190, "phase_lr_deg": 0.0}
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
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-01 05:18:04*
