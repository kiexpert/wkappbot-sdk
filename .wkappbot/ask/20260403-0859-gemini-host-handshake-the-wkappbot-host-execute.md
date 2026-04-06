# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=08:58:23  uptime=27549s  mem=1832MB  system_processes=446"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_085813.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 130-306-267-537-547-637-236-263-216-162-106-103-136-216-216-701-750-132-136-314-132-134-702-607-705-705-607 134-132-132-132-162-132-312-301-130-103-312-301-627-602-602-623-261-2_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 651-657-567-573-567-567-657-567-576-567-657-657-576-576-576-657-567-756-657-576-576-537-576-567-576-756-567-736-635-365-537-357-537-536-573-567-735-365-567-657-316-326-251-312-1_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 675-765-716-765-756-765-675-657-657-671-675-167-765-675-765-673-127-162-136-231-652-627-612-621-617-675-675-612-657-657-671_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 103-136-103-103-136-103-135-576-675-607-163-163-316-136-163-507-276-103-751-756-137-306-376-326-302-326-102-301-263-320-302-765-657-637-631_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 315-312-317-137-317-571-157-135-136-532-657-546-546-547-576-572-756-725-736-547-574-571-517-521-547-547-752-763-371-317-537-573-256-326-321-231-725-257-576-567-627-167-213-132-3_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 167-106-135-123-160 521-123-176-630-162-671-617-126-261-126-267-726-201-216-216-236-231-213-123-261-216-321-127-231-213-321-135-765-153-136-126-167-103-310-132 103-316-136-317-1_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 367-671-627-672-762-762-617-675-672-261-657-657-617-574-756-576-253-267-276-567-562-265-326-263-261-261-526-562-567-562-317-263-267-627-627-652-625-526-567-356-135-362-263-627-6_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 251-620-602-632-261-617-176-761-612-126-601-125-143-136-136-146-124_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 372-376-376-321-371-371-317-165-265-236-236-621-321-316-132-265-236-326-326 236-263-276-270-274-231-267-263-236-236-326-326-524-276-217-267-260-261-260-236-236-326-325-235-235-2_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 301-537-573-173-137-107-132-312-162-132-103-103-574-301-310-130-317-315-130-103-132-756-765-765-765-413-132-301-163-163-162-570-576-750 320-326-261-103-136-713-126-127-127-162-7_V.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 132-103-103-165-157-612-136-103-361-136-136-136-167-103-103-162-103-136-103-160-137-276-167-167-106-137-132-160-261-172-160-136-127-310-176-126-102-615-136-103-716-163-567-107-1_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 213-301-302-106-160-126-162-172-125-176-126-762-172-127-762-276-271-217-276-762-217-231-261-312-627-260-126-620-612-620-267-261-261-627-627-236-762-762-672-627-761-312-360-103-6_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 123-126-103-105-756-720-230-723-327-750-574-576-573-137-132-673-160-132-216-260-620-260-326-236-103-103 316-130-130-130-130-103-106-102-126-102-106-756-765-103-103-132-302-126-1_V.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 276-265-627-236-623-637-625-621-261-213-627-625-326-261-652-625-625-261-132-632-236-216-263 627-312-132-175-376-627-302-312-130-136-310-103-312-312-321-123-360-103-132-321-312-2_V.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 756-765-765-612-256-621-632-623-126-321-263-623-316-623-326-136-123-256-256-265_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 162-612-132-301-670-607-765-760-716-137-632-632-526-623-302-320-132-127-132-126-372-763-162-610-206-326-360-670-675-632-306-103-312-132-132-302-203-573-570-507-574-513-632-576-7_V.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 267-310-132-103-320-320-236-301-301-321-312-321-750-736-376-750-756-706-720-276-574-756-750-570-705-307-371-701-730-731-753-756-760-705-753-765-765-675-716-716-617-170-576-657-6_W.mp3)
  Segment 18: 26792ms ~ 41840ms (file: 102-207-531-251-512-203-213 135-310-135-237-132-251-235-315-132-723-172-175-107-137-752-207-720-720-725-705-327-731-307-253-672-670-673 725-270-270-275_V.mp3)
  Segment 19: 42840ms ~ 43416ms (file: 625-672-325-267-623-302-321-236-176-672-623-170-167-701-750-750-307-362-627-620-206-527-574-570-275-237-326-203-320-176-765-756-705-762-260-206-203-527-520-302-302-320_V.mp3)
  Segment 20: 44416ms ~ 44992ms (file: 103-103-571-756-754-312-530-573-753-752-504-547-537-321-103-132-301-312-675-321-136-163-136-132-617-601-617-203-205-230-137-236-520-523-236-302-203-206-302-302-263-203-135-162-7_V.mp3)
  Segment 21: 45992ms ~ 60932ms (file: 132 261-231-321-326-316-376-371-370-761-760-765-765-675-607-675-671-162-167-716-623-621-701-716-761-671-617-361-361-136-623-623-137-157-103-136-163-103-763-306-175-723-574-574 3_V.mp3)
  Segment 22: 61932ms ~ 62508ms (file: 321-613-162-617-621-672-765-136-103-617-163-176-106-127-572-175-756-761-317-716-136-136-706-107-721-765-761-136-106-612-103 756-215-135-123-631-701-163-632-637-130-175-312-261-1_V.mp3)
  Segment 23: 63508ms ~ 64084ms (file: 261-126-267-136-367-130-107-173-102-132-102-137-103-721-716-721-721-126-126-216-231-102-126-132-163-136-163-167-167-672-103-172-106-316-675-576-675-732-720-276-312-167-761-675-6_W.mp3)
  Segment 24: 65084ms ~ 65660ms (file: 316-671-760-132-372-320-231-123-102-120-136-156-213-236-126-312-136-103-153-103-625-256-312-302-302-302-203-623-632-603-537-547-235-260-203-201-102-162-360-321-102-120-210-261-3_V.mp3)
  Segment 25: 66660ms ~ 67236ms (file: 213-201-123-103-130-132-320-301-103-126-103-102-123-102-123-102_V.mp3)
  Segment 26: 68236ms ~ 68812ms (file: 312-143-602-136-260-302-103-302-320-230-625-263-236-320-132-102-136-316-136-136-617-167-570-507-301-310-351-350-301-302-267-263-320-123-132-170-137-103-103-132-167-617-260-206-2_V.mp3)
  Segment 27: 69812ms ~ 70388ms (file: 567-576-231-103-103-103 163-312-160-123-126-175-137-167-613-165-516-106-517-631-671-162-106-765-136-671-160-163-132-160-106-106-106-165-136-103-173-176-126-167-136-176-136-672-6_V.mp3)
  Segment 28: 71388ms ~ 71964ms (file: 635-652-132-167-167-106-120-321-302-320-230-130-620-612-132-162-261-132-103-103-167-103-136-163-106-106-103-132-163-165-106-621-163-130-167-136-165-163-163-103-312-176-162-206-3_V.mp3)
  Segment 29: 72964ms ~ 73540ms (file: 574-576-567-576-576-657-574-576-576-567-576-567-574-576-576-576-576-567-567-562-567-576-526-567-576-576-576-576-567-574-567-576-567-576-574-573-576-547-567-574-576-756-547-574-5_W.mp3)
  Segment 30: 74540ms ~ 75116ms (file: 765-756-756-673-672-627 547-754-751-765-765-765-761-756-765_W.mp3)
  Segment 31: 76116ms ~ 76692ms (file: 621-162-326-203-130-576-103-310-301-612-326-312-203-102-507-517-276-675-265-267-162-632-136-102-651-103-130-103-651-542-312-162-126-132-673-610-326-103-102-162-326-736-721-731-1_W.mp3)
  Segment 32: 77692ms ~ 78268ms (file: 301-310-301-173-316-132-230-263-236-236-263-130-130-260-260-260-261-132-326-263-261-260-206-130-163-167-123-103-261-613-316-213-627-163-613-326-132-137-126-632-316-160-310-321-6_V.mp3)
  Segment 33: 79268ms ~ 79844ms (file: 673-361 137-132-135-751-671-167-620-167-213-761-612-762-761-672-716-761-267-671-216-263-216-106-261-652-160-130-136-615-362-607-657-617-657-675-675-657-675-657-602-653-756-571-7_W.mp3)
  Segment 34: 80844ms ~ 81420ms (file: 263-263-103-132-136-106-216-216-672-617-621 602-601-651-627-612-153-103-173-132-103-546-675-561-103-106-106-157-136-132-123-103-103-613-106-103-514-132-621-163-106-136-106-761-5_V.mp3)
  Segment 35: 82420ms ~ 82996ms (file: 165-617-106-236-312-213-316-165-162-670-165-130-361-102-675-362-316-610-136-153-376-637-621-612-362-571-572-572-572-152-150-516-607-103-173-356-165-173-163-301-136-103-136-573-1_W.mp3)
  Segment 36: 83996ms ~ 84572ms (file: 607-675-657-263-276-627-246 671-672-627-672-627-672-267-627-546-571-516-615-162-670-637 675-176 627-762-632_W.mp3)
  Segment 37: 85572ms ~ 86148ms (file: 163-160-360-675-617-670-675-675-267-756-573-756-752-765-756-765-756-576-352-531-576-576-574-574-574-517-167-762-762-213-261-625-615-657-562-625-612-165-672-162-132-103-612-675-6_W.mp3)
  Segment 38: 87148ms ~ 87724ms (file: 132-103-132 301-301-310-163 267-627-617-126-132-123-126-102-106-106-172-237-160-316-312-103 132-132-136-103-132-136-316-106-310 675-631-163-632-126-621-632-126-231-263-621-623-6_V.mp3)
  Segment 39: 88724ms ~ 89300ms (file: 507-571-502-170-765-760-706-765-301-367-637-627-613-673-617-306-306-305-756-726-273-372-263-362-327-237-263-103-136-316-103-136-301-312-132-312-127-263-276-267-723-267-123-132-1_V.mp3)
  Segment 40: 90300ms ~ 90876ms (file: 123-126-126-216-265-261-231-261-621-261-163-263-621_W.mp3)
  Segment 41: 91876ms ~ 92452ms (file: 567-673-376-231-317-167-162-675-672-627-675-672-621-675-657-765-657-675-106-617-160-361-673-367-657-657-675-765-576-756-756-765-276-627-675-675-672-675-673-637-765-765-762-675-7_W.mp3)
  Segment 42: 93452ms ~ 94028ms (file: 106-165-610-106-163-216-236-126-601-106-160-106-167-126-103-102-103-507-675-167-617-160-761-610-362-301-136-136-631-362-123-623-156-132-605-605-106-603-136-567-136-327-126-613-3_V.mp3)
  Segment 43: 95028ms ~ 95604ms (file: 312-103-132-143-130-316-136-103-130-316-136-132-132-126-126-341-123-106-103-136-760-756-716-672-162-162 623 631-321-130-103-754-753-751 132-132-103-361-276-102-126-132-123-326-1_V.mp3)
  Segment 44: 96604ms ~ 97180ms (file: 756-756-756-321-130-130-136-132-631-120-267-612-627-623-167-762-163-136-163-167-106-562-165-652-657-576-615-103-103-136-102-136-275-516-612-621-126-613-126-623-316-236-623-263-3_W.mp3)
  Segment 45: 98180ms ~ 98756ms (file: 672-675-670-607-672-671-103-106-176-130-310-317-317-731-756-756-107-754-756-607-672-673-607-627-570-574 103-705-710-106-167 123-132-106-326-263-260-263-260-263-260-136-126-613-6_V.mp3)
  Segment 46: 99756ms ~ 100332ms (file: 137-136-102-237-623-162-103-103-103 106-675-675-671-172-671-375-135-312-326-312 265-153-254-231-261-103-106-162-130-103-103-167-157-360-627-136-126-321-106-126-617-176-130-103-1_V.mp3)
  Segment 47: 101332ms ~ 101908ms (file: 163-126-132-143-514-540-546-627-601-632-627-627-672-630-160-106-601-162-760-612-312-316-603-630-321-103-216-603-326-362-360-321-132-312-132-103-136-612-612-710-672-126-167-165-6_W.mp3)
  Segment 48: 102908ms ~ 103484ms (file: 162-167-103-301-103-301-310-236-321-321-103-167-360-163-162-163-162-162-123-106-106-103-106-160-612-120-620-610-612-162-120-107-302-321-302-103-567-526-235-320-326-306-367-103-1_V.mp3)
  Segment 49: 104484ms ~ 105060ms (file: 765-762 760-760-756-765-627-675-672-603-163-176-173-607-607-601-675-706-765-765-761-675-675-672-627-627 237-216-236-321-572-574-213-321-315-261-126-321-130-162-312-103-123-163-6_W.mp3)
  Segment 50: 106060ms ~ 106636ms (file: 216-263-261-213-261-621-653-761-602-210-156 362-326-360-321-673-106-610-715-176-130-367-627-620-627-631-263-762-726-102-106-617-620-301-103-610-631-312-306-635-632-672-756-317-1_W.mp3)
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
> **Time**: 2026-04-03 08:59:08
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
*Generated by WKAppBot ask gemini — 2026-04-03 08:59:08*
