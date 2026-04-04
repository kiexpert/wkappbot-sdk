# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=14524  time=05:32:26  uptime=6536s  mem=2460MB  system_processes=815"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260401_053216.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 257-725-275-231-217-326-217-327-321-253-215-215-215-231-213-210-123-213-213-213-213-213-216-231-213-325-231-325-321-213-236-231-321-123-326-321-321-231-235-231-321-216-265-215-2_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 613-162-126-635-136-136-316-136-316-103-136-157-612-136-316-126-617-165-162-165-652-106-617-132-156-126-167_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 612-601 613-136-136-103-160-136-167-126-617-103-136-103-310-136-136-613-316-316-612-136-106-136-136-160-162-671-162-136-321-630-106-163-123-613-163-103-136-103-163-163-136-130-1_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 312-123-672-362-136-321-312-136-132-312-706-163-162-216-312-716-361-637-316-762-127-132-631-132-301-316-126-356-615-132-136-537-372-267-236-621-261-162-162-132-671-302-163-312-1_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 263-126-267-267-213-123-321-132-163-630-126-637-163-627-172-261-621-126-261-612-623-326-362-260-261-160-167-610-627-612-160-160-106-613-163-316-620-601-103-162-263-621-162-301-1_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 613-312-130-231-126-362-126-671-362-310-132-316-132-613-632-263-326-316-102-301-136-216-623-136-123 623-756-765-526-502_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 617-605-256-260-560-527-267-265-362-365_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 172-102-132-107-137-310-127-657-163-132-103-136-315-671-675-517-615-136-601-136-132-160-163-506-123-251-765-516-106-671-612-516-167-261-162-215-213-106-167-136-136-301-103-213-2_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 632-623-652-652-123-203-302-320-312-605-567-756-601-206-103-165-362-306-302-301-263-206-267-206-625-130-310-130-106-103-316-103-132-627-672-230-302-672-672-103-130-513-314-301-3_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 163-103-314-175-361-136-107-130-162-103-136-103-617-103-163-160-156-652-516-156-132-136-516-136-103-136-136-130-136-657-651-137-613-637-136-316-310-562-132-126-567-132-132-103-1_V.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 571-153-752-132-215-267-756-152-130-310-321-315-216-175-657-103-136-263-132-312-126-267-623-216-263-216-236-261-263-236-361-301-176-576-165-105-201-103 132-546-576-132-173-301-1_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 231-320-230-260-263-230-362-206-236-320-326-236-320-302-320-230-206-632-321-203 103-165-130-136-316-312-103-312-106-162-163-267-160-162-602-136-106-130-167-160-612-106-610-631-6_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 167-162-126-123-134-316-351-132-312-531 173-657-675-675-316-631-361-132-123-301-316_V.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 574-576-261-576-516-571-173-103-123-163-376-705-713-361-213-210-301-103-570-571-150-167-367-675-167-126-127-256-367-651-612-754-237-321-235-136-765-162-162-612-162-612-163-137-6_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 576-215-126-162-132-126-132-103-176-106-102-103-610-136-103-160-103-126-612-136-620-312-260-103-126-136-103-167-132-132-106-167-172-316-162-136-613-136-165-136-162-162-126-162-1_V.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 312-317-631-316-617-623-263-103-312-132-312-623-603-206-263-301-103-135-132-132-752-327-231-213-231-216-123-263-206-260-267-267-267-206-267-750-750-675-765-732-731-756-570-756-7_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 621-632-326-623-623-362-326-623-362-621-632-623-632-236-632-627-627-672-627-632-631-637-621-637-627-632-623-621-627-612-617-217-127-176-672-672-762-671-627-621-627-617-672-672-6_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 136-362-132-321-312-316-312 253-105-216-162-162-675-275-617-530-527-123-215-615-612-576-536-357-305-267-752-751_W.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 621-162-602-327-321-327-231-312-625-627 236-236-236-236-261-321-231 627-623-621 627-627-623 362 326 127 623-632-623-623-362-236_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 601-675-657-671-617-617-163-167-106-160-136-103-130-627-675-675-673-670-637-132-130-103-132-361-602-236-623-103-123-126-132-126-163-126-263-263-260-236-260-260-236-362-765-756-7_V.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 156-601-617-106-102-706-103-710-761-710-176-763-176-761-760-761-706-170-670-756-750-705-756-163-574-570-567-576-650-612-617-163-627-167-675-671-675-706-103-137-172-137-167-760-7_W.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 756-765-716-765-761-756-756-751-763-761-761-706-260-316-123-120-302-102-123 124-136-132-124-132-671-617-176-162-672-671 756-713-176-162-367-162-751-761-753-176-765-736-761-675-6_W.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 360-671-176-316-132-213-326-320-321-320-326-312-203-203-230-621-261-361-320-316-136-132-310-132-231-301-307-302-703-376-720-726-673-267-203-260-321-637-203-206-362-320-130-310-6_V.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 130-531-152-561-561-567-567-657-657-652-563-352-516-157-157-123-135-275-237-326-127-126-316-312-317-361-317-376-312-327-276-532-517-251-276-267-267-236-267-265-256-256-562-526-2_W.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 235-203-235-207-302-103-310-301-321-302-302-362-236-326-326-263-326-562-260-230-203-230-203-263-620-216-230-213-231-321-267-263-160-236-261-231-230-263-260-206-260-620-623-236-5_V.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 762-765-675-567-657-576-756-765-765-672-673-576-576-754-132-163-316-315-357-175-325-237-321-312-132-213-413-132-271 132-627-267-132-312-132-312-326-326-167-124-132-756-756-750-6_V.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 312 302-362-321-612-627-621 671-672-617-670-671-261-361-216-326-136-132-231-123-672-136-361-136-163-136-126-132-132-231-231-230-231-723-327-317-236-263-237-301-132-217-210-726-7_W.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 675-136-136-216-617-361-167-526-123-132-123-162-127-621-561-623-167-163-132-136-130-312-165-176-615 564-132-613-263-617-162-162-621 263-265-267-265-672-627-267_W.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 261-612-260-612-260-126-621-621-213-213-263-261-216-261-261-261-231-206-216-236-625-132-123-126-610-612-216-261-162-570-570-756-756-672-620-560-675-612-627-607-765-713-637-106-6_W.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 762-167-617-176 127-136-136-130-130-316-137-173-316-613-132-106-130-163-310-317-763-765-675-765-367-362-671-760-675-106-761-760-725-675-627-607-217-612-102-176-367-362-302-310-1_V.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 631-627-623-623-263-263-263-261-216-217-726-276-762-762-761-712-726-217-276-276-276-276-276-267-236-216-261-267-216-326-321-132-163-261-263-621-321_W.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 130-132-160-130-106-615-136-567-213-167-163-602-130-130-106-163-265-127-132-651-163-165-103-261-156-132-103-130-613-103-136-253-162-571-103 231-620-653-657-657 167-615 651-526-7_V.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 672-627-673-627-672-672-627-637-632-672-632-367-672-672-673-672-672-672-673-672-672-672-627-672-672-765-672-673-672-316-672-672-672-672-627-601-362-623-632-632-673-672-675-672-6_W.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 751-753-164-617 576-572-136-103-612-163-316-627-627-162-637-316-136-163-613-367-612-163-364-123-312-136-621-673-613-162-762-763-706-321-136-316-316-267-627-267-326-321-301-312-6_V.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 672-627-627-627-672-627-672-650-602-620-627-625-625-652-572-326-276-725-273-236-263-621-612-615-136-310-235-235-253-256-215-210-321-326-231-231-261-231-235-231-321-235-257-237-2_W.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 135-152-156-152-157-165-523-576-517-571-157-172-271-231-123-576-576-765-657-657-761-267-213-261-276-571-567-562-576-576-761-627-267-216-517-571-517-657-567-153-267-236-216-512-5_W.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 765-672-302-302-310-756-573-576-165-163-106-316-136-132-132-312-132-163-162-312-316-137-310-136-136-312-310-310-130-170-756-307-137-617-136-167-106-103-106-316-531-176-301-312-5_V.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 627-675-756-765-267-260-236-162-613-612-106-160-106-675-675-267-231-130-132-130-103-610-362-326-206-602-620-267-206-267-263-302-123-103-132-657-630-126-602-602-602-621-625-102-1_W.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 135-316-765 312-362-167-132-136-132-312-126-312-216-263-531-351-137-763-367-165-136-126-312-621-326-632-103-156-135-160-106-612-316-127-613-103-167-153-167-135-137-167-172-167-6_W.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 762-127 567-562-623 562-135-173-176-763-765-762-627-612-136-623-163-620-623-167-106-136-132-321-162-263-752-623-621-126-216-163-106-312-136-375-375-316-162-132-610-162-167-126-1_V.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 567-567-657-567-576-613-361-673-675-756-576-675-620-657-623-132 137-312-567-675-367-576-756-576-657-567-657-756-576-576-567-537-567-576-567-756-576-137-135-765-620-602-672-762-6_W.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 574-576-162-312-621-123-213-162-756-576-765-765-321-173-713-754-574 574-576-576-574-576_W.mp3)
  Segment 43: 66192ms ~ 66768ms (file: 571-574-571-635-261-657-567-653-570-263-103-103-162-106-610-136-105-671-361-123-672-102-167-601-761-103-316-316-610-567-261-610-163-617-106-163-617-136-671-167-514-362-127-216 1_W.mp3)
  Segment 44: 67768ms ~ 68344ms (file: 602-620-602-765-761-765-762-670-761-761-761 760-706-756-760-762-763-672-765-670-760-762-762-763-763-765-672-267-756-752-762-756-765-765-765-267-726-261-217-756-706-756-136-316-6_W.mp3)
  Segment 45: 69344ms ~ 69920ms (file: 756 675-765-762-756-756 176-167 106-231-326-276-527-657-276-672-657-265-762-675-576-567-657-567-576-756-576-756-312-326-321-573-753-573-570-621-567-675-602-675-127-103-136-571-7_W.mp3)
  Segment 46: 70920ms ~ 71496ms (file: 623-632-620-160-102-326-201-106-706-761-176-362-631-673-675-621-176-156-567-106-156-176-162-132-607-671-625-671-103-631-316-601-163-756-627-610-236-263-120-302-203-126-136-123-6_V.mp3)
  Segment 47: 72496ms ~ 73072ms (file: 312-321-261-321-263-236-621-326-623-613-326-312-316-571-576-567-576-571-576-361-321-316-312-132-316-231-321-316-132-312-312-312-321-312-316-312-312-312-321-321-321-312-312-132-3_W.mp3)
  Segment 48: 74072ms ~ 74648ms (file: 712-162 675-165-316-371-136-657-176-160-136-301-326-165-362-167-671-106-351-652-613-576-567-756-756-765-756-607-607-623-261-263-236-136-135-750-716-103-312-362-162-176-106-316-1_V.mp3)
  Segment 49: 75648ms ~ 76224ms (file: 312-136 761-213-231-623 576-756-126-162 567 162-163-103-163-103-106-120-103-126-102-231-103-257-127-671-610-612-136-163-236-235-135-136-132-132-152-531-326-126-106-126-627-103-6_V.mp3)
  Segment 50: 77224ms ~ 77800ms (file: 316-310-316-216-526-236-312-236-230-302-301-320-603-602-620-563-120-317-320-362-136-163-617-560-571-750-675-613-326-206-316-106-102-132-126-162-167-132-105-125-261-621-106-126-1_V.mp3)
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
> **Time**: 2026-04-01 05:33:16
> **AI**: gemini

---

## Response

AnalysisAnalysisGemini의 응답[
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
*Generated by WKAppBot ask gemini — 2026-04-01 05:33:16*
