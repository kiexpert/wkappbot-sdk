# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=09:00:29  uptime=27674s  mem=1861MB  system_processes=446"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_090019.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 316-756-156-765-671-617-671-167-675-675-657-657-261-321-120-321-123-132-230-321-216-132-237-130-621-103-162-672-162-637-617 176-136-103-526-351-167-136-103-617-106-103-321-106-2_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 236-527-260-321-510-213-521-576-367-126-320-302-326-320-732-275-230-236-102-301-126-326-521-514-537-573-376-753-756-751-731-310-130-310-103-103-751-756-176-167-167-130-103-570-6_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 312-761-167-172-316-216-126-216-132-261-126-361-321-316-762_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 572-572-753-763-267-172-267-627-756-574-574-576-576-107-173-176-163-561-301-302-132-312-236-236-326-327-132-236-753-574-751-157-103-213-312-123-216-612-216-312-132-176-123-126-2_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 617-612-617 261-613-163-136-571-167-103-612-671-106-103-103-312-157-107-127-135-673-561-103-651-136-526-612-670-106-103-137-601-102-106-571-175-132-307-172-163-167-136-163-163-1_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 615-125-301-312-301-132-130-621-162-261-261-103-130-132-163-602-602-206-623-132-217-103-123-132-732-216-526-537-103-310-102-136-103-301-203-321-312-310-321-230-103-120-172-167-7_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 756-617-765-613-623-163-623 103-163-126-136-132-136-617-165-160-657-162-176-612-610-615-136-610-756-163-617-617-103-103-175-165-173-612-176-173 163-651-675-671-160-136-612-106-1_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 621-126-162-136-360-673-367-631-137-623-261-132-106-132-617-620-621-602-620-163-106-175-106-106-361-103-105-315-126-102-160-657-102-126-213-103-126-136-126-163-130-136-316-132-1_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 765-765-765 103-601-176-160-106-106-175-301-136-102-210-230-250-547-571-257-103-132-301-573-735-302-230-203-572-305-103-132-570-573-301-517-261-316-103-163-567-617-136-316-671-1_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 312-136-106-261-620 321-312-162-612-162-216-321_V.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 362-523-573-275-236-326-126-210-201-132-162-320-320-236-320-201-213-260-320-265-267-261-216-260-260-260-230-302-361-621-632-263-206-203-362-756-326-263-620-231-260-260-162-260-2_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 176-576-576-567-756-567-657-567-576-135-157-176-173-176-173-173-176-136-317-137-317-317-137-136-756-635-653-657-651-612-621-231-671-623-657-657-576-567-562-567-361-126-312-562-3_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 126-301-162-612-612-612-603-615-312-316-163-312-103-136-371-103-105-130-176-106-103-103-103-106-136-152-736-621-301-312-132-136-136-106-613-165-126-106-651-312 176-163-365-316-1_V.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 126-310-316-126-612-162-725-671-567-105-162-263-267_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 652-675-562-562-675-675-572-572-572-576-572-257-627-625-361-326-356-574-576-574-735-315-657-675-675-675-607-260-261-260-620-253-625-265-621-612-617-361-367-326-326-263-236-261-6_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 317-362-316-317-675-671-623-632-637-376-617-540-576-567-563-153-173-137-176-136-126-216-625-265-126-162-631-126-621-627-625-562-652-623-236-623-675-167-132-216-261-637-132-756-3_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 361-627-563 137-103-103 173-316-136-103 627-201-612-103-136-635-657-260-136-102-167-670-762-372-326-613-136-207-326-236-621-237-216-623-365-261-312-321-362-361-367-326-362-765-6_V.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 162-312-235-310-213-612-612-361-130-320-230-326-126-312-136-132-326-136-316-130-321-361-136-123-132-362-267-126 321-103-132-627-231-236-231-213-231-672-672-672-236-752-765-612-1_W.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 162-153-176-716 567-567-135-501-571-571-573-627-621-627-321-306-310-531-531-216-627-261-106-621-216-216-315-312-326-613-620-367-160-625-657-160-265-620-617-612-106-160-106-123-6_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 765-672-321-213-231-251-123-213-267-327-617-157-152-235-372-325-123-270-217-273-203-237-365-625-613-371-317-732-235-327-321-327-325-203-205-261-267-235-321-136-312-321-236-203-2_W.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 175-136-162-152-316-625-136-762-671-132-163 162-576-571-715-574-137-136-632-612-631-163-103-136-130-136-103-176-163-130-136-316-163-163-106-130-103-316-316-756-651-126-126-610-1_V.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 201-213-610-162-617-612-302-320-310-765-320-310-310-321-230-213-103 765-765-736-326-320-210-321-132-312-302-301-213-627 675-236-302-320-320-203-260-267-725-756-756-756-756-167-1_V.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 321-126 672-617-672-627-672-267-613-163-637-621-362_W.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 627-527-256-275-137-217-725-275-237-253-257-126-162-623 163-627-126_W.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 127-126-173-651-617-627-627-156-627-612-716-756-657-756-316-631-136-136-132-103-617-152-526-172-157-130-136-136-132-123-637 135-317-106-673-103-137-156-172-672-167-106-163-716-3_W.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 127-132-126-326-602-207-132-172-607-102-510-762-762-207-375-321-137-502-765-302-132-167-321-261-301-106-123-132-136-137-105-103-312-316-513-735-260-276-120-160-162-137-607-670-6_V.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 125-135-137-170-127-157-327-237-327-327-231-327-237-231-613-136-127-102-637-603-316-163-361-132-130-312-632-126-102-210-175-126 103-102-103-615-675-651-106-103-132-167-362-126-1_V.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 213-103-710-675-571-167-762-130-321-172-321-317-327-312-127-126-612-106-162-613-103-103-106-102-106-670-517-135-103-516-123-165-163-167-176-103-615-621-126-130-103-132-612-517-1_W.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 263 672-623 627-265 627-627-312-310-163-126-261-621_W.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 136-136-516-761-263-326-362-136-316-316-316-137-312-137-136-130-137-127-123-162-163-610-160-163 517-162-106_W.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 236-306-306-302-320-306-302-306-326-306-230-321-320-321-236-260-302-236-302-326-306-302-360-362-320-326-361-231-632-306-326-360-630-623-320-203-321-320-632-236-261-236-326-362-6_V.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 310-136-136-132-130-103-103-317-136-756-631-103-132-102-125_V.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 152-671-716-130-106 301-103-301-310-302-310-301-123-302-320-307-213-601-623-263-627-103-136-103-130-310-103-136-320-327-326-162-316-602-312-216-574-256-260-261-260-261-213-210-6_W.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 150-567-165-162-326-316-127-123-130-130-102-126-761-103-756-756-123-325-326-236-236-325-352-352-725-132-136-316-137-103-302-302-203-106-103-102-163-126-126-130-162-316-106-105-1_V.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 765-623-316-615-126-136-623-362-127 753-576-756-157-163-617-623-106-327-172-167-163-762-136-132-316-167-160-172-162-130-167-103-512-125-731-106-163-162-103-136-163-751-103-106-1_V.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 312-167 123-123-136-312-326-472-362-326-216-137-163-136-172-671-314-637-123-167 274-713-317-216-526-147-375-615-267-365-361-316-327-167-516-316-617-241-164-132-317-137-237-163_V.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 162-162-675-126-162-103-712-163-615-651-103-162-167-163-546-576-163-152-103-130-621-163-105-135-315-106-317-127-301-517-125-136-210-123-123-162-103-103-175-167-106-167-263-123-1_V.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 231-316-236-623-623-356-516-517-316-163-316-637-176-327-672-256-357-576-576-627-625-627-627-627-652-561-651-567-574-547-361-631-361-563-576-657-625-356-531-516-156-136-163-610-1_W.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 136-136-103-103-216-136 136-632-160-132-167-617-136-167-136-157-216-103-162-105-163-167-671-617-103 103-316 102-103-132-167-106-136-316-106-167-107-316-756-152-137-136-132-103-1_V.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 651-106-137-132-106-160-163-150-517-765-732-165-657-576-572-571-315-165-362-625-267-625-167-351-652-136-163-103-160-356-573-567-763-375-136-175-506-562-567-635-365-123-310-623-1_W.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 163-263-236-261-231-123-621-263-276-261-275-317-751-157-751-107-726-721-217 102-263 132 632-213 167-756-261-567-265-123-673-703-136-162-162-172-672-136-136-765-316-672-716-712-1_W.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 132-320-301-130-103-163-231-120-621-675-672-675 316-103-365-361-136 567-567-103-137-657-765-675-754-756-765-765-132-273-234-354-621 103 576-657-657-567-657-263-321-627-263-267-2_W.mp3)
  Segment 43: 66192ms ~ 66768ms (file: 162-326-167-317-103-167-132-103-132-103-137-132-607-760-306-760-673-657 127-162-162-126-216-361-312-321-573-765-675-167-136-136-301-316-312-302-301_V.mp3)
  Segment 44: 67768ms ~ 68344ms (file: 236-216-573-576-103-103-673-765-765-754-756-756-675-756-574-756-756_W.mp3)
  Segment 45: 69344ms ~ 69920ms (file: 763-761-576-576-157-173-762-675-376-167-160-136-103-123-312-236-206-261-231-621-623-267-612-621-612-261-216-623-637-126-623-136-312-762-726-267-263-623-627-627-625-657-627-265-6_W.mp3)
  Segment 46: 70920ms ~ 71496ms (file: 236-216-621-157-632-263-103-265-103-136-670-136-231-625-623-574-756-576-567-576-756-756-576-754-621-576-564-567_W.mp3)
  Segment 47: 72496ms ~ 73072ms (file: 261-126-261-216-263-216-135 576-567_W.mp3)
  Segment 48: 74072ms ~ 74648ms (file: 136-263-136-165-723-207-275-275-752-765-756-675-326-126-312-132-213-231-312-231-327-302-162-120-623-162-103-130-310-310-132-301-312-532-531-163-136-326-675-165-321-237-236-305-3_V.mp3)
  Segment 49: 75648ms ~ 76224ms (file: 671-165-325-367-362-672-261-236-762-726-716-706-732-127-721-762-276-721-127-276-273-721-261-267-273-127-301-103-127-270-273-206-271-267-207-127-321-172-123-136-137-176-163-163-6_W.mp3)
  Segment 50: 77224ms ~ 77800ms (file: 573-267-132-751-257-752-765-726-276-237-237-231-201-312-321-312-132-312-213-312-713-173-251-261-532-106-126-216-612-162-165-261-105-576-576-765-657-670-675-765-675-672-726-275-3_W.mp3)
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
> **Time**: 2026-04-03 09:01:10
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
*Generated by WKAppBot ask gemini — 2026-04-03 09:01:10*
