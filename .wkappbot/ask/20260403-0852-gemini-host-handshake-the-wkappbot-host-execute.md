# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=08:52:01  uptime=27166s  mem=1547MB  system_processes=448"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_085151.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 213-216-231-326-163-261-612-312-163-326-216-132-367-627-261-621-230-130-637-632-216-137-367-172-362-132-326-367-632-126-123-102-312-132-162-631-631-365-216-130-362-326-236-136-2_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 162-320-320-310-321-132 176-107-136-163-761-176-726-267-602-206-103-106-176-167-136-132-106-163-301-310-132-137-203-627-361-162-706-627-236-326-162-106-103-261-201-567-657-657-6_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 271-275-312-612-265-627-267-762-376-327-376-315-365 756 625-263-260-256-265-265-625-625-265-265-562-567-263-236-325-256-125-153-567-654-567-574-762-672-760-265-365-217-132-213-2_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 213-263-231-263-236-236-236 103-321-574-574-576-163-315-537-571 754-756-302-370-763-736-301-310-312-132 762-726-672-762-301-320-260-632-532-360-320-263-302-102-103-120-102-106-1_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 612-276-267-326-206-236-263-263-265-256-236-623-627-621-230-102-627-236-326-130-103-102-106-617-632-602-627-267-261-726-675-613-627-217-230-230-326-320-230-320-230-320-320-321-1_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 362-123-621-603-206-263-261-261-574-756 715-602-621-326-302-302-321-302-370-713-123-231-762-360-103-132-320-231-203-231-216-613-106-132-261-263-526-356-301-321-310-103-765-756-7_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 106-675-675-260-260-167-130-103-132-263-261-675-621-106-632-613-627-657-102-610-103-546-657-136-103-103-103-167-136-671-617-106-136-617-231-126-130-173-753-157-751-123-106-612-1_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 361-301-316-301-572-270-235-231-321-135-351-136-312-103-135-320-130-103-320-302-231-103-137-751-321-627-206-675-132-123-617-756 231-302-267-321-326-301-312-312-352-357-657-312-3_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 236-361-627-231-175-163-106-762-637-167-176 310-103-136-657-617-130-674-160-610-617-162-621-162-103-126-216-175-136-132-152-132-137-135-162 617-163-167-163-136-160-103-136-130-1_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 267-126-165-103-261-574-574-213-132-547-123-126_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 123-167-103-361-156-167-716-126-715-123-167-576-120-156-214-167-160-134-132_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 706-267-276-627-362-372-302-306-367-765-576-576-765-756-765-506-546-205-230-265-312-657-132-206-260-206-267-236-230-203-102-132-136-613-267-547-547-547-675-675-267-326-321-310-3_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 567-536-573-326-102-263-136-715-617 761-162 672-627-126-632 617-617-167 127-762-716 635-170-306-175-162-573-571-157-536-576-627-627-671-613-316-130-176-576-167-751-176-617-376-3_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 176-326-301-136-301-376-261-326-236-312-361_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 172-573-756-756-761-756-756-765-756-762_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 167 756-765-761-610-103-106-716-132-130-163-310-362-160-165-671-601-126-163-615-671-267-721-516-576-267-567_V.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 176 342-265-206-260-621-273-632-162-576-321-236-231-312-321-310-316-103-130-130-310-652-160-162-132-163-617-613-612-301-302-312-310-103-163-102-162-106-153-103-126 103-123-136-1_V.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 316-615-574-576-576-756-750-726-203-203-620-526-562-126-321-320-273-623-236-230-320-573-753-267-206-203-206-675-765-672-327-203-230-316-103-751-715-726-217-213-756-765-762-207-2_W.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 125-103-312-715-127-163-671-167-106-167-761-627-613-167-106-160-617-621-623-610-617-623-162-623-160-167-102-106-103-103-136-137-316-137-103-103-106-103-276-176-561-165-362-126-1_V.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 106-125-126-102-162-123-103-102-102-167-106-321-306-321-516-175-132-230-206-107-105-107-157-167-105-126-621-602-206-320-207-206-210-261-657-501-135-263-231-621-103-130-206-102-1_W.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 137-572-572-576-572-327-321-375-521-732-752-372-374 267-276-726-320-236-207-702-672-527-762-276 167-167-216-756-672-162-126-623-362-175-132-130-132-136-310-167-165-126-316-163-1_V.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 627-325-136-312-163-206-203-231-235-257-123-562-260-206-376-607-630-236-756-152-657-576-275-762-312-312-132-137-136-312-657-567-167-360-156-613-206-236-273-231-123-321-321-351-1_V.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 765-675-602-267-267-267-570-572-652-613 627-760-672-260-206-237-326-570-205-321-706-206-602-265-572-260-720-706-762-267-602-206-206-236-231-236-207-267-203-203-236-206-230 231-7_V.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 126-621-126-160-312 310-165-612-162 341-132-672-312-123-123-167-172-176-173-713-174-176-617-627-601-621-165-126-123-136-167-132-236-361-213-132-341 367-136-631-314-312-316-173-2_V.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 576-576-675-576-576-567-576-765-765-675-607-356-675-765-675-321-631-657-706-657-567-756-756-675-765-675-657-725-261-623-621-627-673-163-631-765-627-652-576-567-765-652-657-567-5_W.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 123-132-126-657-615-162-263-143-132-312-163-627-163-316-167-123-617-613-613-132-163-102-103-132-126-136-213-167-327-623-123-573-157-170-136-162-163-136-103-145-102-163-265-705-1_V.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 236-361-160-163-103-167-206-276-207-206-206-206-670-625-627-276-260-623-267-672-261-260-620-620-627-123-612-260 750-316-103-130-103-163-613-362-601-610-627-627-623-267-632-236-3_W.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 576-572-175-132-312-132-167-612-321-136 576-574-231-132 267-236-765 136 163-126-615-162-103-361-167-162-136-136-623-623-132-163-103-143-106-106-156-671-163-103-765-716-106-134-1_V.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 136-103-136-176-103-136-103-156-615-761-162-137-106-103-137-316-652-162-612-103-103-103-613-263-162-516-612-623-621-103-163-130-136-103-103-162-103-312-615-612-130_V.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 317-312-103-103-132-635-163-621-136-132-206-263-607-670-132-162 267-627-627-162-623-162-756-547-574-574-574-574-574-571-571-571-573-312 621-621-672-670-162-176-173-167-176-167-1_W.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 627-620 671-671-671-671-671-617-671-167-162 312-206-123-623-126-126-165-162-632-625-126-326-312-326-135-312-526-132-132-103-756-675-756-751-751-751-651-567-765-675-576-761-617-1_W.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 102-312-162-103-312-136-103-137-627-231-163-362-261-123-362-120-203-206-301-130-312-120-126-320-321-310-236-301-320-326-302-136-316-301-362-316-306-326-326-362-621-623-362-163-3_W.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 537-352-625-627-263-326-263-136-632-361-513-523-516-263-326-326-623-312-621-623-623-531-532-526-321-356-625-623-621-623-351-351-235-231-132-163-261-632-136-231-132-132-312-213-3_W.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 126-756-576-276-623-321-132 103-160-672-316-301-302-350-301-301-310-310-357-103-107-216-162-576-756-236-213-216-276-623-627-672-206-576-321-312-321-275-123-563-526-312-132-620-5_W.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 106-103-136-316-136-137-136-762-630-106-106-670-102-573-357-756-107-163-103-167-103-136-103-213-236-163-637-615-607-612-671-163-615-301-153-130-132-103-103-106-173-617-135-103-1_V.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 567-576-567-567-657-657-657-657-610-612-625-627-765-675-763-672-526-513-162-126-126-216-126-126 102-120-102-102_W.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 162-316-132-136-230-132-167-621-637-316-132-103-136-671-163-165-160-176-106-176-106-103-103-671-175-103-123-126-103-175-176-103-136-261-167-236-123-154-106-106-160-671-165-136-1_V.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 310-310-123-301-312-321-302-326-230-720-267-327-502-576-513-542-672-172-312-167-316-136-167-562-521-216-231-315-516-572-576-576-126-236-215-371-726-273-263-123-120-370-703-760-5_W.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 107-312-623-260-526-301-102-106-162-206-362-123-126-163-105-612-106-162-162-603-301-362-316-632-260-302-102-627-261-320-230-230-310-301-316-302-531-534-503-573-503-256-623-167-1_V.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 172-765 312 756-761-576-162 123-106-106-103-106 132-176-126-167-165-126-162-106-267-621-627-675-167-167-102-126-612-120-627-672-267-612-631-621-672-561-612-136-167-126 103-130-1_V.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 675-671-675-705-107-675-576-156-167-312-123-103-621-362-215-236-103-201-321-127-103-120-130-165-106-560-723-165-623-132-136-136-360-516-130-123-231-236-213-231-312-123-316-130-3_W.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 263-320-623-236-203-230-236-563-130-103-103-301-314-136-236_V.mp3)
  Segment 43: 66192ms ~ 66768ms (file: 170-107-731-316-201-231-627-160-102-316-132-321-320-276-231-312-321-230-236-623-620-620-206-672-607-763-162-136-103 167-163-132 102-162-106-107-106-632-267-263-236-652-130-136-1_V.mp3)
  Segment 44: 67768ms ~ 68344ms (file: 627-675-627-627-621-627-267-627-602-762-672-726-267-126-621-612-106-106-106-103-130-103-103-102-123-106-216-316-102-106-156-172-103-301-301-326-236-362-320-203-302-103-132-102-1_V.mp3)
  Segment 45: 69344ms ~ 69920ms (file: 301-160-103-517-321-172-621-130-103-106-657-142-103-675-312-106-173-163-126-561-126-567-105-132-103-176-163-165-103-213-120-172-123-167-106-107-163-761-176-106-576-153-657-715-1_V.mp3)
  Segment 46: 70920ms ~ 71496ms (file: 263-306-576-312-632-163-106-130-325-652-326-625-601-630-601-156-165-103-102-103-106-123-765-675-625-625-620-263-102-362-260-627-572-756-276-201-216-162-620-765-207-263-132-302-1_V.mp3)
  Segment 47: 72496ms ~ 73072ms (file: 310-132-316-312-132-620-216-612-136_V.mp3)
  Segment 48: 74072ms ~ 74648ms (file: 627-601-163-162-160-160-160-106-610-162-603-361-316-657-106-167-163-617-160-617-106-106-106-106-106-167-627-627-627-620-620-547-567-567-657-605-165-617-167-617-671-617-310-361-3_V.mp3)
  Segment 49: 75648ms ~ 76224ms (file: 153-354 276-765-762-672-672 574-576-106 372-374-163-302_V.mp3)
  Segment 50: 77224ms ~ 77800ms (file: 572-756-657-675-276-625-265-627-621 162-263-765-672 326-263 675-675-675-657_W.mp3)
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
> **Time**: 2026-04-03 08:52:42
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
{"word": "포커스", "start_ms": 10, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "양보가", "start_ms": 170, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "필요합니다", "start_ms": 360, "dur_ms": 200, "phase_lr_deg": 0.0}
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
*Generated by WKAppBot ask gemini — 2026-04-03 08:52:42*
