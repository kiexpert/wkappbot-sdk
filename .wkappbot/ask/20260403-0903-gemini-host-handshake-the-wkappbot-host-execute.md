# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=09:02:35  uptime=27801s  mem=1862MB  system_processes=445"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_090226.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 620-361-175-157-176-157-136-136-267-231-652-675-627-267-263-267-261-263-261-261-675 612-136-623-612-106-132-137-162-137-160-312-137-176-165-132-576-127-125-126-256-162-612-167-1_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 547-625-235-623-372-126-126-123-126-327-326-367-607-267-675-613-675-632-361 103-130-134-316-162-620-631-367-136-310-162-126-610-106-621-206-236-756-576-176-163-136-162-126-162-1_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 167-172-167-163 136-627-176-621-263-260-761-167-103-123-176-103-130-103-136-103-106-135-102-752-572-752-572-572-327-326-216-735-731-753-107-173-132-173-317-312-316-314-136-132-1_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 603-620-610 163-103-136-631-361 546-675-576 310-635-671-163-361-167-163-162-106-106-132-163-312-103-361-136-316-160-136-365-567_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 130-316-310-310-631-376-307-310-613-631-306 103-152-137-176-162-617-106-130-167-167-671-136-136-316-765-103-136-263-623-236-176-132-163-136-614-716-103-137-106-362-172-126-132-1_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 157-126 670 672 315-615-651-316-135-157-165-156-135-162-167-356-132-136_V.mp3)
  Segment 7: 9456ms ~ 11760ms (file: 175-765 136-603-631-376-673-673-617-673-612-361-136-312-137-130-326-132-162-627-627-621 136-175-312-103-103-261-312-312-301-301-321 103-652-625-130-326-301-136-267-632 162-132-6_V.mp3)
  Segment 8: 12760ms ~ 13336ms (file: 136-103-260-627-620-627-124-123 765-765-765-751-517_V.mp3)
  Segment 9: 14336ms ~ 14912ms (file: 267-627-627-621-627-265-126-123-164-263-236-203-231-203-123-236-620-261-102-267-203-126-103-312-310-201-210-201-216-326-132-201-671 672-623-362_W.mp3)
  Segment 10: 15912ms ~ 16488ms (file: 630-132-132-135-132 316-216-250-210-231-132-136-316-167-316-136 215-265-326-230-623-631-267-267-267-267-267-612-612 627-627 361-612-612-263-361-160-167-610-163-671-627-632-106-6_W.mp3)
  Segment 11: 17488ms ~ 18064ms (file: 623-361 162-612-132-126-123 621-612-621 671-671-627-612-672 673-627-627-672-672-627-627-627-263 671-612-617_W.mp3)
  Segment 12: 19064ms ~ 19640ms (file: 163-613-106-103-136-137-120-103-612-162-312-317-130-736-763-736-672-617-627-167-631-612-623-673-675-607-162-106-106-675-675-675-607-672-672-526-610-162-621-162-103-162-102-103-1_W.mp3)
  Segment 13: 20640ms ~ 21216ms (file: 326-761-765-756-765-751-752-657-316-136-176-671-672-765-716-765-675-765-675-576-625-617-621-263-263-623-267-657-620-765-652-657-657-576-675-651-612-126-312-132-536-537-572-672-6_W.mp3)
  Segment 14: 22216ms ~ 22792ms (file: 136-136-163-316-137-315-576-317-673-361-136-713_W.mp3)
  Segment 15: 23792ms ~ 24368ms (file: 263-136-127-206-127-126-123-621-206-126-123-126-203-231-123-236-321-320-103-621-631-176-103-136-126-271-167-126-267-765-307-672-126-321-132-132-271-317-123-213-127-213-617-231-3_V.mp3)
  Segment 16: 25368ms ~ 25944ms (file: 625-265-623-326-526-126-621-623-267-627-672-671-627-657-672-657-675-675-672-657-257-652-627-673-761-627-621-627-216-132-213-216-261-215-527-216-231-123-165-627-267-265-261-267-2_W.mp3)
  Segment 17: 26944ms ~ 27520ms (file: 672-726-762-761-765-763-765-735-765-756-756-765-716-172-765 765-765-572-574-765-673-176-760-765-765-761-763-761 753-572-765-765-765-176-175-167-176-765-617-167 756-753-176-167-7_W.mp3)
  Segment 18: 28520ms ~ 29096ms (file: 362-120-162-102-310-627-672-672-762-762-765-765-675-762-765-765-756-765-765-762-765-672-103-102-103-321-132-321-302-312-321-301-302-312-732 316-602-203-207-572-752-572-750-307-1_W.mp3)
  Segment 19: 30096ms ~ 30672ms (file: 261-260-260-620-602-610-621-601-261-106-621-613-607-671-167-106-156-167-613-167-163-617-167-106-167-126-126-106-126-106-574-154-103-156 213-251-601-201-216-213-231-201-203-203-2_V.mp3)
  Segment 20: 31672ms ~ 32248ms (file: 312-143-317-312-321-312-132-527 132-573-573-357-301-316-312-362-231-203-632-631-132-162-163-162-132-127-213-312-136-372-236-136-160-163_V.mp3)
  Segment 21: 33248ms ~ 33824ms (file: 132-103-173-103-134-163-134-165-107-103-103-136-761-607-230-320-320-103-103-135-613-143-132-261-132-163-132-157-107-163-210-316-620-675-574-162-132-501-573-576-650-602-627-103-1_V.mp3)
  Segment 22: 34824ms ~ 35400ms (file: 126-612-163-613-236-762-620-765-675-765-762-672-632-261-612-362-263-260-306-623-163-613-162-162-310-216-751-756-765-617-163-316-163-167-136-263-621-631-103-130-106-320-321-102-7_V.mp3)
  Segment 23: 36400ms ~ 36976ms (file: 143-312-213-627-736-371-271-726-762-726-721-725-732-723-713-735-752-721-725-723-172-712-721-275-231-231-123-327-123-231-321-123-132-315-132_W.mp3)
  Segment 24: 37976ms ~ 38552ms (file: 657-765-675-657-675-567-163-130-105-370-710-107-105-107-103-137-132-576-132-137-103-317-317-137-317-130-103-137-107-170-130-120-137-136-617-317-632-160-631-132-126-107-107-310-1_W.mp3)
  Segment 25: 39552ms ~ 40128ms (file: 705-570-574-756-750-754-756-765-675-624-756-763-176-103-620-326-310-103-103-301-103-173-106-106-163-103-103-137-620-602-620-620-627-260-672-632-613-167-671-167-162-136-106-163-7_V.mp3)
  Segment 26: 41128ms ~ 41704ms (file: 756-705-751-167-167-167-162-320-107-107-710-765-760-716-165-167-617-612-126-216-120-172-765-567-570-750-654-106-601-627-602-326-132-605-657-657-362-236-627-620-132-361-542-132-6_W.mp3)
  Segment 27: 42704ms ~ 43280ms (file: 132-136-132-210-216-123-106-163-162-301-103-103-301-132-310-130-310-103-132-123-236-231_V.mp3)
  Segment 28: 44280ms ~ 44856ms (file: 576-351-573-175-762-276-237-726-276-327-273-371-572-257-675-675-765-657-657-675-267-362-371-325-315-137-153-753-573-315-623-627-627-672-237-326-352-526-176-574-573-576-405-570-5_W.mp3)
  Segment 29: 45856ms ~ 46432ms (file: 310-312-673-675-765-763-765-765-576-765-213-273-261-267-103-130-104-132-134-132-176-207-206-263-357-705-670-307-106-106-160-617-160-763-106-613-612-612-106-136-102-103 260-206-6_V.mp3)
  Segment 30: 47432ms ~ 48008ms (file: 371 675-705-765-765-123-136-361-516-132-132-176-103-102-132-162-103-106-106-163-136-613-136-136-165-601-176-572-163-103-367-136-167-106-157-267-136-162-164-160-601-176-103-130-1_V.mp3)
  Segment 31: 49008ms ~ 49584ms (file: 167 261-216-265-165-326-163-103-103-167-162-167-716-106-167-167-162-612-127-631-612 163-130-675-163-106-607-261-357-156-216-574-132 365-356-527-167-612-765-716-120-651-567-652_V.mp3)
  Segment 32: 50584ms ~ 51160ms (file: 263-326-123-132-123-623-263-206-206-206-263-126-623-623-632-126-631-651-625-312-362-167-326-123-103-132-602-362-362-362-231-136 126-172-127-132-576-765-175 765-765-675-173-103-1_V.mp3)
  Segment 33: 52160ms ~ 52736ms (file: 126-671-675-172 765-176-675-176-176-162-716-675-310-120-163 657 675-672-167-162-617-123-123-230-605-627-361-126 637-621-213-671-761-762-765-762-672-627-627-276-672-726-627-267-6_W.mp3)
  Segment 34: 53736ms ~ 54312ms (file: 756 132-316-765-605-670-103-132 306-230-320-312-321-623-124-627-132-103-132-672-637-675-675-132-301-752-715-103-132-621-620-602-627-617-765-576-576-567-261-265-507-570-506-562-2_V.mp3)
  Segment 35: 55312ms ~ 55888ms (file: 356-365-653-567-567-537-536-567-561-576-651-567-561-651-567-651-576-765-675-765-765-756-576-651-165-165-163-156-156-175-315-135-576-156-561-516-156-615-167-163-163-615-651-651-1_W.mp3)
  Segment 36: 56888ms ~ 57464ms (file: 675-675 123-136-261-261-263-132-162-612-263-632-267-627-267-627 236-367-637 706-706-267-267 612-612-123-310-176-103-173-105-135-713-167-623-102 261-726-106-130-103-310-567-132-3_W.mp3)
  Segment 37: 58464ms ~ 59040ms (file: 567-651-657-635-356-657-653-657-165-517-356-536-573-563-357-657-567-563-567-536-356-563-652-657-567-653-567-106-651-651-537-615-135-156-157-567-651-561-657-567-561-165-165-156-1_W.mp3)
  Segment 38: 60040ms ~ 60616ms (file: 213-257-126-102-126-132-125-152-516-526-502-547-570-756-574-576-150-501-150-157-157-751-675-756-750-657-754-130-130-316-321-735-765-106-136-571-705-175-756-576-756-571-135-317-6_W.mp3)
  Segment 39: 61616ms ~ 62192ms (file: 316-167-163-137-617-610-612-671-130-123-167-126-261-623-627-267-103-130-132-130-157-602-167-130-106-163-263-267-257-576-756-126-102-162-675-576-751-546-567-136 571-765-167-167-1_W.mp3)
  Segment 40: 63192ms ~ 63768ms (file: 527-652-547-567-576-657-605-567-567-567-156-657-657-156-135-165-215-263-316-203-263-203-254-207-103-103-310-130-301-310-627-612-621-261-123-231-236-267-726-671-160-607-675-637-6_W.mp3)
  Segment 41: 64768ms ~ 65344ms (file: 132-106-120-632-632-230-607-670-361-326-320-326-320-320-321 132-317-612-564-567-627-672-267-216-102-102 613-163-162-167-675-762-627-627-672-162-567-623-567-256-216-627-653-657-3_W.mp3)
  Segment 42: 66344ms ~ 66920ms (file: 127-217-216-210-721-237-217-271-210-213-267-275-275-521_W.mp3)
  Segment 43: 67920ms ~ 68496ms (file: 163-106-301-306-301-302-326-260-263-316-106-136-162-130-316-301-132-123-132-302-136-103-602-620-620-620-260-260-236-320-235-106-126-670-263-206-302-236-370-312-162-160-612-162-1_W.mp3)
  Segment 44: 69496ms ~ 70072ms (file: 162-103-562-156-567-106-106-136-156-261-321-162-236-165-106-612-106-163-136-132-501-105-301-132-153-103-162-132-132-756-106-107-261-136-673-153-165-612-162-136-715-753-715-176-1_V.mp3)
  Segment 45: 71072ms ~ 71648ms (file: 612-163-103-126-765-126-103-163 761-163-106-613-136-617-613-106-103-106-162-167-621-761-130-167-176-106-263-162-517-165-127-321-601-162-136-316-162-576-103-103-635-651-152-106-7_V.mp3)
  Segment 46: 72648ms ~ 73224ms (file: 162-361-163-153-760-132-130-105-107-103-176-617-163-126-267-123-130-103-136-106-160-162-160-162-103-617-106-102-601-106-102-136-731-102-137-162-123-102-312-132 136-136-316-136-3_V.mp3)
  Segment 47: 74224ms ~ 74800ms (file: 132-657-627-765-613-153-607-607-625-751-136-310-752-752-627-672-672-672-652-625-627-672-672-310-302-367-573-576-754-756-765-236-627-627-632-672-627-623-326-230-236-230-236-103-1_W.mp3)
  Segment 48: 75800ms ~ 76376ms (file: 672-607 154-620-602-607-671-361-163-136-602-754-756-756-576-675-576-762-276-267-267-542-514-501-302-132-312-126-134-571-173-135-137-136-163-136-103-167-136-136-103-216-216-136-1_V.mp3)
  Segment 49: 77376ms ~ 77952ms (file: 657 726-763-130-301-360-120-231-537-573-513_V.mp3)
  Segment 50: 78952ms ~ 79528ms (file: 360-326-632-620-605-602-160-612-162-613-310-106-103-301-302-673-623-321-126-102-165-165-651-576-650-765-657-267-263-216-675-627-236-320-126-260-623-605-576-106-162-126-301-312-2_V.mp3)
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
> **Time**: 2026-04-03 09:03:20
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
*Generated by WKAppBot ask gemini — 2026-04-03 09:03:20*
