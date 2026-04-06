# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=09:04:49  uptime=27934s  mem=1776MB  system_processes=449"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_090439.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 675-276-236-261-261 631-267-367-261-625-627-631-715-136-136-136-627-567-675-637 356-132-316-321-672-756-675-273-657-652-623-261-267-362-265-627-263-236-263-265-326-265-635-263-2_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 571-372-236-302-301-326-362-326-265-260-261-361-613-301-361-637-360-316-367-301-631-237-312-253-237-206-167-312-603-263-207-132-153-571-362-736-103-316-670-162-163-316-136-376-3_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 612-326-312-136-312-312-132-132-312-312-216-312-312-126-162-356-652-615-603-627-130-103-132-312-312-132-316-136-231-203-612-260-621-261-213-321-601-623-360-362-632-326-130-261-1_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 125-130-173-126-103-107-301-310-316-301-306-631-103-136-103-162-103-106-102-201-306-130-106-310-302-301-310-357-720-620-623-236-263-230-326-263-326-621-631-617-612-613-106-163-1_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 263-261 756-751-136-176-675-627-136-167-167-574-617-361-132-120-631-103-310-316-310-105-573-153-103-637-236-261-536-576-576-567-576-576-756-675-765-725-760-716-621-162-756-756-6_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 621 613-671-160-132-613-612-163-316-136-137-176-167-136-301-106-301-103-130-130-310-130-763-761-670-706-763-261-612-647-756-750-756-756-756-107-106 627-260-106-103-136-321-130-1_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 103-602-106-102-103-310-261-620-620-620-602-602-760-320-602-261-236-106-613-362-162-213-321-123-523-231-260-256-301-601-106-612-617-162-326-526-265-652-625-562-206-672-765-672-3_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 612-261-312-316-162-716-632-132-156-706-136-130-310-306-130-103-623-376-176-263-261-123-163-216-261-126-132-310-632-620-206-206-620-602-206-136-163-106-106-106-103-513-752-163-3_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 371-176-316-361-612-263-261-106-672-167-136-163-321-316-106-162-162-576-567-137-312-301-312-351-537-167-620-106-312-130-123-312-312-210-210-216-120-213-672-670-765-675-764-761-6_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 547-547-567-765-756-765-765-670-361-136-102-251-675-705-705-206-263-102-160-601-612-260-260-162-162-716-106-167-106-105-157-150-546-574-576-567-765-675-675-265-206-130-362-610-2_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 756-270-206-203-261-260-206-263-301-230-320-103-132-310 620-620-106-162-672-625-620-630-670-670-670-675-670-607-670-675-760-607-627-627-206-260-752-725-765-675-673-607-163-163-6_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 670-672-765-762-567-756-672-675-627-627-756-765-672-765-675-672-675-625-675-610-657-672-176-617-176-762-267-675-675-756-675-765-675-675-651-751-621-631-735-376-132 103-167-173-1_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 657-306-273-361-613-607-603-632-263-672-567-765-365-103-310-136-317-107-106-130-163-103-163-136-574-576-570-561-261-267-602-312-132 136 132-123_V.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 672-563-625-236-162-632-621-672-326-263-573-637-362-651-765-316-612-675-635-152-675-362-632-276-236-327-653-627-267-625-623-652-657-367-632-236-627-623-267-126-125-275-362-126-3_V.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 716-706-756-107-167-103-750-705-756-765-765-762-763-306-320-260-207-136-713-137-715-675-701-752-705-132-312-103-735-716-321-136-132-103-167-163-107-137-130-106-316-103-136-165-1_V.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 136-106-103-136-103-167-235-265-562-135-675-765-263-263-260 162-106-512-362-167-106-103-106-132-106-623-716-261-132-615-126-130-106-165-712-256-617-231-136-156-617-631-162-102-6_V.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 167-106-612-106-106-103-576-576-657-657-607 163-627-263-261-263-216-213-312-756-752-725-627-723-625-367-260-620-176-673_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 125-657-267-312-130-126-123-312-103-310-307-735-735-672-756-705-756-571-357-623-320-132-316-315-103-631-612-621-627-162-631-163-136-136-163-613-507-507-527-607-612-637-627-673-6_V.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 103-106-263-263-263-130-326-612-162-170-567-126-263-623-263-126-756-267-672-136-635-625-617-167-130-137-103-160-576-162-613 562-163-106-106-675-106-165-561-563-165-765-567-675-7_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 132-136-165-176-136-136-671-132-136-126-176-126-165-126-163-162-156-167-125-103-162-165-213-512-263-675-576-675-675-675-561_V.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 726-673-621-623-726-601-163 102-130-132 671_W.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 261-206-210-103-160-621-621-612-267-652-567-567-567-153-671-163-126-360-106-130-301-103-260-260-360-362-236-260-260-627-267-320-136-160-106-160-301-106-130-570-236-136-103-321-1_W.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 570-750-713-706-107-106-167-102-103-361-301-301-306-360-637-635-675-670-130-132-210-260-526-256-362-320-203-632-320-617-123-637-612 361-127-103-132-301-301-756-570-265-236-203-5_W.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 276-261-621-326-236-672-612-641-312-134-132-734-312-132-315-413-562-576-136-673-673-613-413-276-627-623-312-316-316-671-173-172-176-173-137-132-173-132-143-132-143-172-134-132-1_V.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 675-176-617-267-231-132-615 321 617-561-260-627-316-176-136-162-132-136-130-103-736-156-651-765-627-261-260-657-675-675-756-576-156_W.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 123-126-756-756-756-765-275-567-506-127-231-260-263-302-362-520-621-132 362-312-321-310-762-132-631-360-175 103-163-160-136-132-126-162-167-125-215-205-276-167-103-627-621-126-2_W.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 546 132 261-263-263-236 260-132-630-672 610-671-617-765-675-631-513-325-320-301 136-136-163-312-130-103_W.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 103-130-106 576-567-567-613 765-675-675-275-276-267-627-627 651-632-672-632-731-631 103-310-625-567-652 261-321-130-576-756-102-263-231-263-675-672-671-265 567-567-574-574-567-5_W.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 106-312-103-751-263-261-623-675-657-130-103-107-106-102-123-517-271-706-132-107-670-675-671-160-617-627-675-571-675-765-162-675-671-216-123-602-105-103-165-176-103-310-361-106-1_W.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 302-301-302-123-163-162-163-162-167-162-623-613-137-612-627-206-126-602-103-132-317-607-367-316-301-316-372-765-720-217-163-201-206-253-574-572-507-136-176-136-501-576-312-310-1_V.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 562-523-325-527-135-671-715-267-657-157-175-175-572-157-574-576-567-251-213-321-623-561-521-567-526-517-572-357-315-371-375-237-275-267-267-267-267-265-267-263-236-267-261-213-2_W.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 326-623-120-120-612-162-163-260-263-206-106-162-612-106-106-260-326-263-610-621-612-163-167-301-310-321-632-103-236-320-123-103-132-132-132-612-163-601-613-163-163-203 675-625-6_W.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 765-675-267-263-567-756-567-675-675-716-567-675-657-573-756-652-627-613-621-572-562-325-657-126-216-621-372-325-675-132-126-312-123-576-576-572-576-567-756-657-517-175-136-652-5_W.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 257-257-265-265-562-256-513-256-254-356-325-152-562-126-265-652-125-132-257-251-532-513-352-357-534-534-530-513-150-514-513-154-157-571-573-532-257-235-253-253-361-321-152-123-1_W.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 167 126-176-625-276-657-576-136 765-675-617_W.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 627-672-632-603-607-127-263-123-231-312-136-132-312-132-574-312-176-301-132-574-570-136-361-367-750-670-602-726-267-760-627-267-261-672-312-321-312 706-230-302-307-573-237-316-3_V.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 120-210 574-657-657-756-765-675-567-756-623-625-675-632-726-675-756-765-715-365-376-367-517-517-756-163-136-761-672-673-637-603-326-263-126-163-123-136-162-163-361-163-103-136-3_W.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 152-135-127-351-357-317-213-123-231-123-231-235-132-321-312-312-312-123-532-534-531-325-327-213-263-236-267-265-267-267-263-236-263-265-236-327-237-326-267-637-326-362-325-356-3_W.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 163-516-527-176-165-751-567-356-316-631-613-163-105-310-310-201-652-631-627-260-620-265-576-561-167-751-651-560-672-576-567-753-576-576-356-657-675-672-657-673-567-657-657-567-5_W.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 762-763-372-267-762-672-762-762-712-327-761-376-732-753-726-267-762-762-716-765-756-675-627-137-213-321-126-236-276-261-321-132-213-210-231-231-263-623-236-263-231-263-123-312-6_W.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 163-136-627-762-657-563-567-567-567-657-576-516-516-537-576-561-537-356-567-567-567-536-356-156-652-567-651-651-563-563-567-567-567-576-562-567-562-567-563-567-572-567-567-576-5_V.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 703-165-103-562-123-312-163-132-617-361-132-320-302-162-126-765-173-673-672-106-261-623-301-672-537-260-106-162-126-123-163-136-136-163-135-316-136-132-216-637-167-631-130-671-7_V.mp3)
  Segment 43: 66192ms ~ 66768ms (file: 576-576-607-607-602-621-310-103-107-716-136-136-106-316-103-156-563-567-615-617-136-657-567-365-132-605-362-126-136-603-567-156-652-267-260-157-263-612-263-357-675-617-136-610-1_W.mp3)
  Segment 44: 67768ms ~ 68344ms (file: 236-673-721-627-620-260-260-362-326-236-236-236-236-632-627-617-257-627-236-263-637-637-263-627-637-672-672-632-326-706-567-267-260-236-765-675-576-675-236-216-265_W.mp3)
  Segment 45: 69344ms ~ 69920ms (file: 613-316-261-236-267-625-263-264-236-256-635-375-137-136-236-236-326-261-236-326-326-652-567-562-675-562-563-576-175-163-130-130-310-562-567-621-261-263-326-263-126-163-613-562-5_W.mp3)
  Segment 46: 70920ms ~ 71496ms (file: 621-321-301-213-312-301-236-263-316-361-316-310-621-627 623-267-672-536-756-765-176_W.mp3)
  Segment 47: 72496ms ~ 73072ms (file: 362-623-263-627-236-326-621-326-631_W.mp3)
  Segment 48: 74072ms ~ 74648ms (file: 136-103-103-153 576-567-576-714-231-631-132-136-105-620-132-512-120-315-132-132-301-231-327-524-357-275-327-176-312-123-137-132-213-513-532-251-325-301-203-132-312-352-315-321-3_V.mp3)
  Segment 49: 75648ms ~ 76224ms (file: 657-756-567-567-756-756-657-756-657-576-567-675-765-675-576-567-675-576-657-657-657-657-756-651-625-672-675-576-756_W.mp3)
  Segment 50: 77224ms ~ 77800ms (file: 213-361-576-576-574-576-756-675-765-756-763-752-756-765-762-765-675-102_W.mp3)
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
> **Time**: 2026-04-03 09:05:34
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
*Generated by WKAppBot ask gemini — 2026-04-03 09:05:34*
