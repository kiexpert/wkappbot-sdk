# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=08:54:05  uptime=27290s  mem=1651MB  system_processes=446"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_085355.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 672-627-651-564-627-267-627-672-765-675-672-627-546-265-267-213-514-675-672-267-267-672-546-564-652-367-367-367-671-673-263-261-210-216-126-127-312-540-513-653-263-316-351-135-3_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 360-610-160-160-601-610-601-106-617-165-213-126-321-123-621-671-637-162-670-671-567-567-576-756-165-167-163-631-657-130-657-567-560-571-576-162-671-106-316-672-672-617-167-273-6_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 163-152-160-176-106-106-163-102-107-135-163-172-617-675-765-321-152-261-261-126-263-236-263-326-521-123-235-213-576-567-574-675-571-576-163-620-617-765-765-627-260-627-260-216-2_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 132-213-136-132-103-132-103-213-106-123-103-172-130-326-126-613-615-162-136-351-134-103-765-741-132-675-617-562-631-675-102-260-627-176-675-670-263-261-627-275-216-136-132-612-1_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 152-315-672-163-216-632-165-216-623-103-613-613-162-160-132-136-106-216-627-126-236-637-261-312-501-765-176-106-123-136-312-103-267-367-152-106-106-136-130-263-176-106-103-137-1_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 123-623-672-261-605-167-631-612-621-657-163-316-612-162-312-231-326-763-637-320-201-312-670-103-162-675-627-301-132-301-316-103-102-326-302-321-231-321-301-326-320-231-261-260-3_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 123 615-620-173-176 612 136 160-123-312-675-675 576-657-756-756-756-756-756-756-765-765-765-765-761_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 275-526-265-652-652-672-673-627-627_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 103-137-103-105-310-235-326 230-230-231-302-320-103-123-132-123-106-136-123-206-302-312-302-203-206-263-527-102-130-301-320-230-203-203-523-215-132-206-250-250-235-230-617-617-1_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 316-126-612-126-165-716-765-657-615-102-162-671-652-657-652-625-621-132-651-263-253-527-357-123-127-123-162-123-152-125-517-213-215-127-213-257-213-376-763-726-576-217-267-257-5_V.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 761-167 276-267-765-756-756-765-502-126-613-657-675-625-621-160-135-153-157-517-507-675-526-567-761-672-672-610-627-617-162-276-267-627 632-260-760-567-273-276-276-267-602-602-6_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 132-265-106-671-752-602-657-236-263-326-213-203-230-123-120-302-267-236-316-312-725-723-623-132-321-231-671-657-567-567-675-657-275-237-352-502-756-123-617-675-627-632-307-326-2_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 263-267-265-261-261-263-216-263-236-261-261-217-126-123-126-231-261-261-261-267-276-267-321-236-267-263-263-236-260-260-275-267-276-267-267-236-236-213-216-270-207-260-210-260-2_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 156-163-163-621-621-657-765-267-126-213-102-123-672-627-263-130-310-312-136-302-136-312-320-230-320-103-103-136-103-310-601-132-107-162-601-102-601 263-236-617-167-163 627-612-6_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 326-516-135-123-160-576-103-316-136-103-126-106-213-123-136-103-136-132-137-136-617-316-631-103-561-136-310-162-103-136-103-103-163-103-123-172-153-172-123-167-162 132-123-137-6_V.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 672-126 102-132 675-657-675-675-675-657-176_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 126-216 763-762-765-673-623-621-163-162-613-617-163-136-102-310-276-762-167-136-621-517-572 130-153-150-153-317-132-136-132-132-132-621-613-216-132-137-576-573-753-756-573-756-7_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 623-632-176-162-321-236-263-236-321-302-231-302-321-236-231-213-203-237-302-237-253-126-162-261-213-203-302-206-320-130-162-316-301-301-132-306-103-306-301-103-130-301-310-301-3_W.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 361-632-361-216-623-216-321-612-612-126-316-213-627-236-231-612-312-362-625-132-213-302-263-136-621-102-132-103-301-301-312-321-320-132-103-103-130-123-123-123-126-132-326-312-6_V.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 162-564-567-632-260-621-162-261-126-612-631-321-620-261-307-627-602-625-162-103-132-163-103-361-132-175-105-532-615-163-160-163-103-106-103-135-103-571-167-106-316-615-130-103-1_V.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 201-203-206-526-250-236-312 120-632-261-267-216-263-123-621-216-132-263-123-102-601-361-613-136-263-160-617-162-623 623-623-570-576-574-756 657-576-567-657-267-267-621-627-267-2_W.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 320-320-302-312-132-130-302-306-621-327-206-276-627-321-321-302-312-301-312-230-230-520-502-321-320-302-326-203-230-316-362-621-106-126-162-312-321-312-130-123-215-725-726-236-1_W.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 136-615-136-127-165-176-601-632-517-623-257-615-675-623-136-163-561-135-156-175-231-213-527-521_V.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 163-261-165 124-765-612-106-652-365-651-657-601-760-102-613-136-160-612-613-216-321-102-136-126-160-516-215-136-637-103-761-103-215-576-613-176-160-103-165-236-106-167-103-106-6_V.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 762-326-236-621-321-621-321-213-216-263-326-236-261-623-263-213-321 605-306-130-316-310-631-136-326-130-160-102-162-672-132-312-321-210-236-236-136-261-126-263-230-213-362-321-2_W.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 301-317-301-301-312-312-310-130-130-103-301-106-136-574-756-230-203-237-102-135-752-750-702-207-275-312-132-103-301-362-632-163-163-320-230-320-230-203-206-260-102-236-326-263-2_V.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 713-612-136-137-136-301-137-672-673-756-756-715-765-702-726-276-502-572-570-572-756-735-735-762-765-576-307-570-576-576-756-756-765-756-765-756-756-765-756-765-756-756-756-760-5_W.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 362-631-756-675-163-617-631-163-136-213-673-617-615-672-612-671-675-762-672-612-167-362-617-167-351-316-136-362-132-321-321-123-231-132-136-612-162-163-617-617-612-617-615-163-6_W.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 607-602-623-326-236-162-301-103-103-126-756-706-671-670-206-260-260-620-612-760-716-371-263_W.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 517-102-615-156-125-160-106-176-162-561-167-176-132-312-132-123-312-372-213 106-103-106-715-176-576-137-716-613-621-162-501-507-106-106-132-756-765-675-670-123-672-627-201-621-2_V.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 627-572-567-576-351-574-571-571-576-576-312-265-263-263-261-261-263-263-261-263-263-267-267-256-576-657-672-261-261-261-263-261-236-216-213-216-321-236-126-126-261-574-576-365-3_W.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 326-231-316-236 675-657-657-567-657-756-675-567-576-567-657-657-567-652-657-567-657-657-675-627-672-657-675-657-567-576-576-675-657-657-765-576-567-675-756-567-657-576-576-567-7_W.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 612-623-621-263-267-231-326 675-657 167-167-106-106-106-107-102-102-312-321-310-130-302-302-301-130-132-103-302-102-103-103-132-106-103-301-136-106-301-310-320-765-376-103-103-3_V.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 326-617-576-756-725-576-567-576-305-610 167-162 236-265-261 574-756-102-162-163-132-763-716-710-716-657-132-173-176-176-160-136-617-167-617-671-617-675-267-607-160-163-301-756-7_W.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 652-360-103-615-106-126-136-153-670-136 176-162-627-612-756-260-621-617-107-103-106-107-103-621-132-103-103-127-671-136-103-216-137-126-163-263-261-263 213-671-761-765-765-756-7_W.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 136-163-610-136-157-613-162-136-160-162-136-103-156-162-620-156-612-162-312-106-670-103-631-103-617-162-267-267-261-321-103-613-756-316-103-136-103-162-316-312-130-163-136-126-3_V.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 320-310-170-301-321-326-206-623-621-126-132-312-326-236-326-236-302-302-236-326-671-601-106-136-201-236-261-260-203-123-316-326-236-321-230-321-362-617-376-132-136-157-160-106-1_W.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 623-625-267-106-261-216-263-137-132-136-126-107-316-576-650-631-236-103-102-103-316-132-672-163-261-216-216-610-136-613-136-261-132-326-263-263-236 263-261-263-672-326-236_V.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 126-261-123-123 260-263-715-103-623-675-135-103-216-132-301-126-123-136-134-312 675-675 361-163-671-672-675-675-675-650-675-576-572-561-652-260-670-675-670-671-762-760-126-126-1_W.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 574-167-136-167 673-136-135-103 627-306-103-137-631-627-673-326-236 316-103 326-750-576 517-574-507-136-176-136-317-671-627-203-230-312-123 301-136-132-310-132-137-631-613-752-7_W.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 612-362-612 627-267 756-706-701 316_W.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 103-621-631-632-103-132-162-602-162-103-135-163-103-617-106-103-132-673-136-137-132-165-132-103-657-231-162-167-103-617-136-106-612-361-165-623-103-170-263-613-326-627-613-106-6_V.mp3)
  Segment 43: 66192ms ~ 66768ms (file: 527-260-132 307-172-723-576 564-576-657-567-657-657 326-126-127-321-317-312-670-670-670-672-673-675-672-213-123-132-672-673 671-627-263-327-263-263-231 265-623-256-235-265-275-2_W.mp3)
  Segment 44: 67768ms ~ 68344ms (file: 273-230-320-237-123-621-620-763-716-371-236-263-236-321-632-613-362-162-671-621-615-623-617-260-263-620-326-276-276-210-627-632-627-612-621-216-265-326-362-621-650-312-321-175-3_W.mp3)
  Segment 45: 69344ms ~ 69920ms (file: 106-137-217-615-163-675-167-160-153-132-103-130-136-136-217-260-165-102-316-103-136-103-167-631-160-103-163-125-612-652-761-160-130-316-103-162-657-162-610-612-657-675-163-103-1_V.mp3)
  Segment 46: 70920ms ~ 71496ms (file: 574-507-510-516-156-170-762-261-321_W.mp3)
  Segment 47: 72496ms ~ 73072ms (file: 173-162-106-651-163-671-106-163-103-613-236-167-103-106-631-136-103-136-671-163-362-103-107-126-136-657-672-576-613-106-312-316-163-163-621-103-617-163-106-251-517-136-163-167-1_V.mp3)
  Segment 48: 74072ms ~ 74648ms (file: 130-103-157-316-165-162-103-136-136-160-163-136-167-136-361-620-670-621-617-163-163-216-621-167-162-105-126-561-162-107-152-167-623-672-162-162-163-132-165-106-162-617-362-132-1_V.mp3)
  Segment 49: 75648ms ~ 76224ms (file: 576-562-537-567-267-523-256-256-531-532-527-325-356-352-531-230-523-512-351-125-125-325-325-531-526-653-135-315-351-352-312-132-315-213-352-312-513-513-315-325-521-531-516-136-3_W.mp3)
  Segment 50: 77224ms ~ 77800ms (file: 623-376-316-310-312-103-310-236-213-213-132-321-312-103-160-106-165-126-321-137-160-236-160-320-302-302-103-130-126-162-506-621-162-362-126-265-562-526-625-612-627-267-231-213-2_W.mp3)
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
> **Time**: 2026-04-03 08:54:47
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
*Generated by WKAppBot ask gemini — 2026-04-03 08:54:47*
