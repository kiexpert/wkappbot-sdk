# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=14524  time=05:35:22  uptime=6713s  mem=2106MB  system_processes=824"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260401_053512.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 167-165-736-607-706-672-672-607-670-632-607-670-672-617-672-361-632-103-130-103-103-137-137-132-103-103-321-316-673-673-673-310-102-102-261-132-301-310-103-310-130-627-267-206-3_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 203-302-326-320-103-102-123-106-106-120-123_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 175-107-162-102-132-203-230-206-203-261-206-320-310-320-310-312-312-321-307-302-320-360-362-637-637-607-267-263-321-127-316-601-263-362-172-173-217-237-230-326-236-261-263-326-5_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 671-123-316-627-613-651-312 675-617-612-617-126-361-316-613-613-617-613-136-657 165-761-631-136-361-312-132-137-316-136-301-327-327-371-375-230-230-231-312-312-312-312-567-576-2_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 574-576-576-675-672-675-627-627-627-124-136-103-130-231-132 617-671 106-123-267-201-123-672-607-263 312 621-612-621-617-236-132-132_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 321-356-136-315-625-627-623-263-312-132-312-132-610-160-316-310-316-316-132-362-306-320-312-603-631-310-317-537-361-312-301-123-123-130-103-103-571-315-206-276-726-206-263-672-2_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 213-231-216-213-215-216-213-216-231-213-216-216-213-216-132-213-126-216-123-126-216-213-213-216-126-261-216-231-132-231-230-321-326-321-231-123-251-256-526-256-652-236-625-236-2_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 120-231-152-132-627-136-672-675-567-765-567-631-316-136-136-167-136-601-132-261-612-126-267-612-267-267-671-267-621-261-267-627-136-236-263-231-236-267-263-621-317-761-376-763-3_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 310-612-132-123-267-627-123-302-320-613-136-103-613-361-216-163-621-106-263-127-276-267-326-127-167-173-127-672-316-126-135-761-317-362-136-567-716-756-123-213-321-213-612-163-5_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 615-165-312-361-326-362-627-576-657-567-765-756-567-675-625-362-163-136-672-621-126-231-361-163-516-156-615-132-163-613-361-675-635-675-671-617-615-617-617-361-136-312-132 106-6_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 216-203-263-261-126-315-103-136-702-307-756-756-576 576-576-263-206-327-231-326-260-132-326-732-765-756-273-236-123-162-236-504-503-135-317-136-132-103-731-163-316-132-301-301-1_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 547-567-716-167-126-310-231-126-130-527-325-132-631-651-613-130 763-736-637-365-362-301-127-137-321-213-765-672-216-627-627-237-326-263-623-162-302-320-631-630-675-607-672-163-6_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 765-762-765-765-765-765-761 756-675-675-576-576 235-632 231-126-123-216-213-231-312-316-321-213-236-231-312-216-132-321-302-132-123-132-132-213-213-126-316-132-612-321-263-260-6_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 126-216-327-321-267-672-627-671-617-627-621-267-623-627-627-621-627-253-236-576-765-672-627-267-627-621-627-621-627-526-256-627-263-627-267-267-623-625-625-627-621-267-627-267-6_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 312-236-326-236-260-312-310-130-103-106-136-103-163-106-162-162-267 574-576-574-351-567-210-102-210-236-236-263-362-326-301-312-132-751-756-132-213-263-263-263-126-163-321-352-3_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 167-375-312 765-627-327-237-623-362-321-132-612-621-623-621-621-263-623-621-263-621-261-621-621-264-623-625-625-623-263-261-621-621-623-265-621-621-621-623-621-621-621-627-621-6_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 762-675-627-607-657-625-613-216-231-627-631-130-136-136-263-162-672-273-621-675-765-765-675-657-627-652-657-627-625-652-657-672-627-267-672-672-765-675-763-236-675-627-612-671-2_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 263-267-276-267-726-376-356-357-735-736-367-367-376-573-572-357-537-536-537-356-365-375-576-356-652-675-672-652-265-567-567-654-657-654-657-617-674-654-654-654-654-651-635-613-6_W.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 627-263-216-216-756-765-276-207-260-230-732-756-672-627-762-162-627-675-672-627-671-657-635-657-675-625-567 361-362-326 671-631-613-362-163-163-261-576-567-673-671-675-637-657-2_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 312-127-301-103 136-312-103-163-132-315-163-103-103-103-132-134-147-103-103-125 267-176-132-162-176-162-316-137-132 103-103-310-301-310-362-316-316-316-361-301-103-103-132-316 6_V.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 172-162-163-623-263-126-102-126-501-265-750-765-763-130-310-130-103-132-163-572-570-570-760-326-260-326-301-370-307-301-301-753-730-207_V.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 103-167-516-752-521-135-136-123-372-102-123-132 136-601-132-162-130-137-103-167-132-127-132-762-257-163-102-312-103-103-571-106-263-631-612-612-261-312_V.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 516-165-163 127-124 320-236-263-263-263-263-236-236 312-312_W.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 756-167-657-176-106-615-106-765 607-571-123-615-123-316-136-361-657-167-163-670-617-671-761-671-621-103-765-675-607-167-675-627-263-607-136-157-103-132-765-136-617-125-751-672-6_W.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 132-163 261-120-206-216-257-627-632-236-637-537 162-612-106_V.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 315-173-163-610-610-312-621-260-263-612-136-162-613-630-612-601-620-632-231-621-132-621-261-163-302-103-130-103-163-103-617-621-173-132-316-631-615-362-632-236-236-621-617-103-1_V.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 610-201-163-106 103-620-206-672-675-765-765-765-760-136-167-167-167-617-617-617-671-617-617-671-671_W.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 132-123-132-103-312-231-231-206-126-176-103-107-106-206-620-627-670-162-602-320-320-123-312-301-320-321-312-321-312-321-231-321-301-321-216-230-210-231-236-230-203-260-206-213-2_V.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 576-567-576-657-132 265-625 540-547-540 267-547-524-253-267-236-321-236-312-321-213 574-576-756-261-261-261 623-627-620-623-267-627-623-562-356-213-263-627-261-657-567 326-623-5_W.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 217-317-301-302-301-362-203-261-201-130-167-130-107-102-210-136-130-132-316-103-312-310-103-130-360-201-207-206-267-206-623-316-136-106-130-632-326-310-610-301-306-301-301-312-3_V.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 762-312-312-132-321-132-126-231-623-263-627-657-132-321-376-267-261-267-362-326-623-361-321-326-312-637-763-652-657-675-762-756-675-326-326-326-326-673-765-672-762-627-651 136-6_W.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 267-236-321-132-573-752-257-270-527-572-547-260-627-627-672-670-627-312-236-231-537-576-572-572-327-261-162-260-620-316-351-356-675-623-537-512-523-125-712-627-257-572-567-753-6_W.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 632-167-612-617-167-132-312-607-612-163-162-153-316-136-365-167-136-136-167-160-135-763-610-136-602-163-130-672-107-176-301-612-261-672-276-123-725-765-675-672-670-132-123-520-1_V.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 675-574-576-675-567-576-765-756-657-526-562-652-657-765-675-765-765-675-576-765-567-756-756-675-675-765-675-765-576-567-675-657-675-675-675-756-765-675-675-765-576-573-576-765-7_W.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 257-265-106-172-132-162-612-312-132-321-276-265-625-265-267-267_W.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 765-751-756-574-572-574-574-576-574-572-574-573-534-574-573-537-573-357-357-173-350-534-530-523-315-517-543-541-513-512-520-152-517-526-567-651-617-652-526-257-765-132-156-213-1_W.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 620-623-263-362-623-236 726-326-312-312-163-167_W.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 601-301-357-103-312-302-301-705-307-675-765-765-761-765-765-765-756-736 715-165-160-106-160-163-160-160-312-620-302-316-130-301-570-275-265-263-236-102-106-160-106-106-107-160-1_V.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 136-312-123-162-561-523-652-563-652-516-653-613-563-526-532-562-356-531-156-512-356-561-536-652-653-652-653-657-657-652-561-561-156-316-635-136-356-532-531-635-516-516-547-537-5_W.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 631-631-706-167-260-620-260-260-132-321-236-231-312-301-310-301-321-320-213-236-276-206-627-602-601-612-102-120-132-130-130-201-102-123-216-123-162-162-132-236-132-123-136-632-2_W.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 657-610-671-106-103-163-103-162-160-167-672-103-136-130-306-652-162-103-136-761-160 170-103-167-103-106-136-106-163-163-106-612-613-136-567-610-103-632-103-123-163-136-162-615-1_V.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 123-136-362-306-726-761-326-236-327-276-326-613-314-613-675-706-136-136-316-136-603-621-620-632-673-630-136-163-136-163-103-710-701-671-607-160-162-672-672-603-236-126-162-103-6_V.mp3)
  Segment 43: 66192ms ~ 66768ms (file: 316-160-163-136-136-136 267-761-675-761-761-671-675-762-671-765-627-620-612-612-162-162-367-621-326-162-632 134-310-120-123 673-126-162-326-126-132-362-236-623-621-623-621-632-6_W.mp3)
  Segment 44: 67768ms ~ 68344ms (file: 752-257-275-725-752-275-725-765-276 615-267-612-312 216-321-162-132-162-623-360-321-123-261-162 574-576-562-567-132-763_W.mp3)
  Segment 45: 69344ms ~ 69920ms (file: 567-567-567-576-561-516-561-576-576-517-567-156-156-157-156-516-576-576-563-563-573-516-567-567-567-517-576-571-576-657-156-165-765-675-765-675-765-516-576-157-567-175-132-137-1_W.mp3)
  Segment 46: 70920ms ~ 71496ms (file: 123-312-312 312-312-123-132-756-756-765-765-675-672-103-132-310-320-320-312-312-310-306-236-312-672-162-153-153-510-260-263-261-263-623-261-507-301-302-321-320-632-162-631-163-6_V.mp3)
  Segment 47: 72496ms ~ 73072ms (file: 321-301-203-201-135-132-130-132-135-132-123-312-123-321-103-136-316-163-623-231-321-657-612-263-136-127-102-317-657-751-570-351-213-321-231-321-312-302-312-175-206-260-632-360-3_V.mp3)
  Segment 48: 74072ms ~ 74648ms (file: 132-310-312 172-547-547 765-756-756_W.mp3)
  Segment 49: 75648ms ~ 76224ms (file: 126-231-126-130-123-263-623-362-130 136-132-173-176-673-301-136-103-103-136-106-106-106-106-106-620-260-620-260-620-260-321-315-167-162-621-126-126-260-263-260-203-312-236-675-6_V.mp3)
  Segment 50: 77224ms ~ 77800ms (file: 102-127-123-162-162 162-126-162-123-123-136-103 754-217-123-162-261-612-163-136-162-162-267-326-236-675-671-302-162-260-206-102-126-162 162-167-175-570-750-132-123-760-765 627-6_V.mp3)
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
> **Time**: 2026-04-01 05:36:14
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
*Generated by WKAppBot ask gemini — 2026-04-01 05:36:14*
