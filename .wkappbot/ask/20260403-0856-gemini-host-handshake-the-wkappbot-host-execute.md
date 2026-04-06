# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=08:56:14  uptime=27419s  mem=1736MB  system_processes=445"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_085604.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 351-321 261-156 102-156-167 631-103-130-103-106-310-763-765-765-567-756-637-765-123_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 567-657-567-652-637-657-673-675-567-675-657-652-675-652-567-625-132-635-675-567-562-567-756-657-657-652-671-672-627-562-576-756-576-671-671-567-675-675-615-657-657-657-567-657-6_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 326-236 162-216-123-312-765-163-136-106-765_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 136-567-765-765-675-657-567-206-157-165_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 765-672-135-123-102-723-627 672-612-167-126-236-612-631_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 576-567-672-631-267-756-652-657-576-301-301-167-160-321-136-321-301-361-326-621-261-102-576-657-675-567-576-567-567-657-567-567-576-756-756-657-657-567-576-576-561-216-325-653-5_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 263-267 136-165-167-103-103-106-123-132-176-152-571-157-135-106-103-163-103-136-176-617-167-106-106-216-162-715-321-651-106-107-103-175-175-176-106-103-236-567-567-526-651-765-7_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 360-603-367-673-637-706-672-607-763-673-673-673-620-672-367-607-673-672-672-672-670-670-670-675-675-763-675-673-675-673-675-672-672-672-673-672-673-672-673-672-762-765-765-765-6_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 756-675-326-261-123-213-310-310-612-105-510-156-165-167-561-561-617-617-167-106-167-310-103-136-126-612-631-106-620-320-612-167-165-750-103-106-316-163-162-162-213-130-126-312-3_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 136-672-631-206-163-103-136-312-301-130-320-310-302-132-320-260-265-206-236-320-231-250-275-120-213-163-230-310-103-106-103-305-302-326-352-236-263-320-320-302-326-321-230-236-3_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 751-713-261-627-627-762-672-725-765-765-263-216-671-671-613-123-163-160-607-167 132-316-317-106-715-216-160-276-261-137-103-160-103-175-106-570-130-160-617-267-671-761-763-167-7_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 160-617-106-176-157-165-657-602-103-102-267-261-326-325-132-103-102-135-612-106-103-130-126-126-130-102-106-160-162-106-612-167-165-567-576-167-750-705-175-715-571-150-105-571-1_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 632-637-672-627-672-316-165-132-621-326-627-265-651-657-265-627-632-675-637-756-576-576-576-675-673-361-362-657-672-765-765 627-265-176 765-675-675-567-675-167-361 321-261-316-3_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 130-132-216-213-216-267-623-261-126-567-526-672-675-657-256-261 762-765-762-672-672 765-157-615-267-627-267-267-216-236-231-130-103-132 312 312-567-765-576 627-576-762-376-672-6_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 267-576-576-567-537-136-361-361-136-361-326-326-326-326-267-237-236-267-321-236-263-263-623-267-627-627-627-627-627-627-625-627-267-627-627-261-763-637-765-672-617-165-652-635-3_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 132-256-672-267-267-627 267-103-136-105-102-612-163-162-261-761-162-612-216-263-310-362-176-167-257-621-276-162-103-103-130-103-103-103-103-103-103-136-716-673-103-316-562-176-1_V.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 123-123-315-673-576-570-127-271-235-251-357-236-275-275-235-235-251-573-257-275-657-537-153-136-165-126-156-125-756-356-357-356-256-156-572-257-321-217-125-257-237-125-165-651-2_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 132-316-136-126-231-610-631-167-612-106 162-163-132-106-123-175-623-167-103-216-713-103-627-163-672-126-126-152-106-106-610-162-127-162-761-671-167-612-675-621-136-160-361-162-1_V.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 261-236-312-251-162-267-263-632-163-135-756-106-326-106-671-263-102-103-361-617-136-316-321-126-621-326-613-627-627-621-623-236-216-261-621-260-627-265-265-263-125-106-562-365-1_V.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 627-621-267-321-130-741-754-750-725-172-301-136-136-315-617-321-162-672-576-317-306-750-705-537-627-103-132-231-206-263-163-267-136-630-361-321-657-625-625-263-236-321-325-675-6_V.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 530-305-103-372-327-362-236-231-302-132-163-567-651-617-610-637-613-163-136-316-615-162-617-675-165-605-601-106-162-652-165-567-621-621-617-613-761-612-167-672-627-672-136-132-3_W.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 576-236-632-362-160-106-160-761-167-136-136-160-167-103-167-167-601-261-326-261-627-162-136-216-326-126-632-570-705-765-765-765-706-703-504-547-123-132-103-216-263-765-607-623-1_V.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 623-621-623-623-162-136-620-610-167-123-103-623-317-162-613-632-261-623-126-157-170-715-710-715-713-263-120-167-765-765-756-103-132-263-136-603-670-602-576-657-720-602-106-106-1_V.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 235-261-263-327-256-276-257-753-753-574-352-352-531-351-357-357-375-326-574-543-537-351-357-351-351-315-315-315-315 365-365-315-726-126-162-123-526-625-257-572-517-157-126-126-2_W.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 675-576-657-176-267-627-267-627-206-413-126-103-132-123-132-126-132-316-376-736-750-750-672-756-765-750-762-312-301-301-312-750-627-602-672-102-301-132-123-103-126-312-103-132-3_V.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 261-263-327-761-736-756-625-265-265-627-623-652-362-627-672-625-623-625-265-625-625-625-652-657-625-652-657-726-657-672-267-267-326-537-265-321-312 617-621-672-675-756_W.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 572-567-261-263-135-137-561-542-263-267-276-267-265-627-216-132-312-317-137-316-317-176-267-267-267-621-627-267-672-627-621-526-256-231-231-523-542-236-236-236-312-123-261-527-2_W.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 175-570-106 103-130-136-176-173-163-126-165-103-132-176-317-137-130-136-106-163-103-160-136-103-130-106-126-127-126-162-137-637-126-120-103-107-316-106-671-160-106-517-612-126-1_V.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 625-620-163-612-617-163-625-362-627-761-163-612-526-715-576-765-675-657-567-615-657-657-675-165-132-675-163-106-675-613-316-136-256-125-215-627-716-657-167-613-136-163-176-136-6_W.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 136-301-316-136-756-756-605-132-670-657-615-657-657 163-615-613-617-615_W.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 150-306-306-675-672-627-607-160-107-106-172-302-132-526-567-671-675-620-321-132-260-301-321-312-763-765-627-620-627-267-627-623-231-761-675-236-326-132-213-123-134-132-103 103-2_W.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 163 361-326-321-231-263-123-312-362-163-632-623-362-103-310-130-362-312-123 301-302-326-321-310-312-675-675-627-236-265-216 670-620-652-675-652-675 135-106-567-675-672-547-576-5_W.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 623-621-612-613-610-567-267-267-632-623-326-236-263-260-652-654-652-625-625-265-236-263-267-267-267-562-231-231-236-637-673-637-162-612-126-261-217-237-237-231-321-267-651-267-2_W.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 132-321-312-136 327-726-672 671-236-103-310-351-231-201-316-623 316-132-103-103-103-320-302-302-630-601-617-102-136-136-135-136-130-310-130-130-103-103-312-132-312-132-132-136-3_V.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 504-123-102-312-310-312-130-310-103-130-301 672-602-623-261-231-102-126-126-321-103-132-136-263-263-627-236-632-621-261-260-265-236-216-231-213-236-263-235-235-321-312-231-216-3_W.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 261-170-136-710-130-175-173-132-143-132-163-160-107-165-716-670-165-165-541-126-126-765-617-135-615-162-162-136-167-173-163-162-130-165-106-163-123-123-123-312-136-135-123-162-1_V.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 673-103-103-130-102-617-163-137-176-103-362-132-126-106-165-103-103-103-106-132-175-325-135-126-132-136-152-631-302-137-130-137-163-167-106-103-136-103-106-106-103-136-136-102-6_V.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 657 126-162-576-576-765-103-102-102-127-126-570-574-570-175-261-130-130-310-103-301-563-356-630-570-570-572-672-106-607-361-361-602-621-310-312-612-612-163-103-136-103-103-206-1_V.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 132-657-675-627-103-321-156-267-216-136-312-301 672-632-316-632-162-216 153-362-623-261-621-162-132-321-327-173-326-321-576-762-372-376-132-130 561-316_W.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 127-167 263-762-762-752-725-765-765-713-735-756-756_W.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 301-130-617-675-672-206-267-627-627-205-207-627-267-206-260-123-132-361-321-301-320-320-310-236-203-103-261-267-302-326-362-326-326-320-206-206-607-675-267-276-726-762-725-762-7_W.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 170-716-765-172-172-760-716-216-761-761-130-310 617-671-613-163-160-132-216-160 231-163-574-756-126-162-576-635-316-126-213-130-316-541-123-651-167-657-672-367-675-627-321-162-7_W.mp3)
  Segment 43: 66192ms ~ 68136ms (file: 163-263-126-612-120-217-107-271-176-217-176-167-167-617-176-617-617-167-617-671-617-167-167-160-617-160-617-176-167-623-602-610-163-130-651-605-167-652-706-135 372-326-276-321-3_V.mp3)
  Segment 44: 69136ms ~ 69712ms (file: 162-361-671-163 160-167-156-126-103-130-567-605-710-167-126-103-631-103-123-574-637-617-120-125-152-265-236-612-672-623-362-261-236-126-213-127-127-127-127-213-127-127-125-126-2_W.mp3)
  Segment 45: 70712ms ~ 71288ms (file: 326-231-260-206-120-132-162-103-610-167-672-210-106-136-103-136-103-312-105-136-670-260-326-126-627-263-216-263-267-267-756-172-267-136-765-210-132-132-163-567-617-231-103-516-1_W.mp3)
  Segment 46: 72288ms ~ 72864ms (file: 163-671-136-132-213-612-612-163-126-213-132-236-136-162-137-631-621-625-167-312-103-231-765-136-103-160-175-102-760-106-167-623-316-126-163-106-216-160-136-103-163-160-612-126-7_W.mp3)
  Segment 47: 73864ms ~ 74440ms (file: 106-260-106-163-162-756-756-756-675-623-213-612-765-706-302-320-203-230-326-103-106-157-571-127-130-321-136-132-136-265-126-675-546-126-135-613-531-170-167-674-136-312-103-103-1_V.mp3)
  Segment 48: 75440ms ~ 76016ms (file: 306-602-632-362-302-230-502-620-620-620 162-126-162-612-612-312-132-310-610-601-621-260-201-263-672-326-623-321-263-360-321-130-136-312-623-136-316-126-312-761-601-163-103-601-2_V.mp3)
  Segment 49: 77016ms ~ 77592ms (file: 632-261-231-231-132-103-102-132-126-321-231-160-103-106 103-102-102-162-162-126-123-231-653-657-675-675-675-617-765-672-672 326-326-263 620-627-637-260-576-657-326-263-321-672-3_W.mp3)
  Segment 50: 78592ms ~ 79168ms (file: 213-102-152-571-157-102-267-671-260-371-302-321-627-312-130-130-103-326-136-315-126-136-130-210-627-627-136-136-136-163-136-137-213-231-167-671-576-167-761-137-507 137-126-162-1_V.mp3)
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
> **Time**: 2026-04-03 08:56:59
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
*Generated by WKAppBot ask gemini — 2026-04-03 08:56:59*
