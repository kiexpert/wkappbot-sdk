# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=09:20:49  uptime=28895s  mem=1778MB  system_processes=446"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_092039.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 135-531-315-153-103-150-105-105-150-156-516-516-576-571-517-516-513-517-153-516-517-516-536-513-516-517-517-561-567-516-517-576-567-570-507-517-573-567-576-573-576-567-567-537-5_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 316-671-167-675-675-156 156-316-136-163-163-103-132-361_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 263-623-621-625-623-625-625-265-625-267-627-267-265-652-256-265-265-623-623-612-162-612-653-637-526-562-567-562-657-675-152-215-315-526-312-312-361-316-316-162-136-361-635-163-3_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 326-236-236-216-216-263-236-126-123-312-321-213-312-152-216-236-352-523-321-312-231-213-540-547-254-265-265-265-263-263-265-265-326-326-361-312-236-547-526-562-316-316-312-316-3_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 762-765-765-762-762-765 201 123 316-163-601-671-167-136 765-136-160 761-756-103-103-137-762-127-716-103-176-361-103-567-167-217-751-167-103-102-312-175-173-271-752-162-107-136-1_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 163-167-106-651-623-162-136-125-132-760-173-367-657-631-610-176-167-160-167-136-107-265-167-162-103-163-106-765-176-167-130-167-263_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 103-107-103-103-157-103-320-321-532-326-635-576-576-756-657-276-725-257-276-276-510-351-275-207-207-320-203-203-203-762-765-762-302-132-163-312-326-172-123-302-576-675-510-251-3_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 136-316-132-620-261-132-310-103-136-534-354-103-163-201-251-301-103-174-132-761-317-136-301-103-302-301-570-752-756-756-705-706-623-267-206-265-203-320-671-320-620-326-302-136-3_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 103-103-175-103-137-103-132-623-176-132-613-163-136-165-617-601-261-601-176-612-163-103-106-103-156-136-136-137-103-102-106-130-136-716-176-567-617-612-617-316-637-576-612-103-6_V.mp3)
  Segment 10: 14184ms ~ 27540ms (file: 765-675-673-672-672-672-756-765-627-765-765-765-675-765-765-765-765-756-671-617-617-612-617-621-623-627-672-601-610-167-176-106-103-236-371-137-135-156_W.mp3)
  Segment 11: 28540ms ~ 29116ms (file: 267-260-267-360-631-132-130-310-621-260-620-260-206-263-260-102-301-132-306-301-260-263-267-260-267-265-236-132-136-167-106-106-623-236-263-301-326-136-310-613-675-756-365-312-7_W.mp3)
  Segment 12: 30116ms ~ 30692ms (file: 314-167-167-106-167-612-163-705-756 570-753-763-750-507-106-710-706-671-103-132-172-701-761-705-751-132-672-627-624-127-127-163-136-136-675-127-243-312-163-163-627-267-623-216-1_V.mp3)
  Segment 13: 31692ms ~ 32268ms (file: 316-137-751-135-762-567-136-162-136-312-176-167-164-236-261-261-263-201-261-123-123-163-136-126-263-130-163-301-672-123-562-673-612-136-613-167-136-671-106-162-231-213-163-603-3_V.mp3)
  Segment 14: 33268ms ~ 33844ms (file: 126 261-126-103-632-362-372-126-103-256-102 765-103-301-167-617-263-126 125-413-107 176-761-132-132-102-103-132-163-106-136-106-301-103-310-130-301-310-301-312-103-132-136-130 6_V.mp3)
  Segment 15: 34844ms ~ 35420ms (file: 312-123-123-321-162-165-125-165-517-657-576-576-576-571-265-326-127-627-625-652-126-657-657-157-157-725-267-261-263-263-236-123-326-236-213-312-163-231-261-136-513-132-231-361-3_W.mp3)
  Segment 16: 36420ms ~ 36996ms (file: 216-732-762-672-756-576-567-765-756-675-612-123-126 576-613-167-657-156-176-617-623-136-657-657-176-672-267-263-326-756-675-672-156-157 507-726-203-201-601-160-316-602-130-126-1_W.mp3)
  Segment 17: 37996ms ~ 38572ms (file: 167-167-107-176-170-671-617-671-760-617-176-670-607-671-632-216 176-621-267-617-762-671-673-613-261-312-136-267-126-765-671-627-231-362-172-765-672-627-764-162-672-671-617-312-7_W.mp3)
  Segment 18: 39572ms ~ 40148ms (file: 106-130-651-130-103-132-103-175-160-167-167-143-106-130-623-602-620-103-173-127-632-362-603-162-126-103-167-561-136-153-615-167-671-103-725-167-157-163-610-136-103-613-103-715-1_V.mp3)
  Segment 19: 41148ms ~ 41724ms (file: 752-165-517-672-567-567-567-657-237-627-651-567-752-657-765-657-765-572-576-651-651-165-573-576-517-762-573-562-567-563-576-657-657-765-657-671-657-657-576-567-657-562-527-567-5_W.mp3)
  Segment 20: 42724ms ~ 43300ms (file: 106-106-132-126-136 675-627 162-613-621-632-617-167-137-136-360-610-107-130-106-103-105-106-301-103-126-671-132-356-236-236-236-172-617-651-621-126-130-123-103-163-103-160-316-1_V.mp3)
  Segment 21: 44300ms ~ 44876ms (file: 573-621-267-627-562-567-627-576-756-123-162-631-625-651-321-327-231-213-263-163-576-576-675-267-263-672-612-612-675-760-612-621_W.mp3)
  Segment 22: 45876ms ~ 46452ms (file: 165-217-237-316-756-162-162-267-162-672-627-632-756-567-167-213-236-231-120-132-312 612-637-672-657-675 267-273-267-675-765-756-675-756-765-276-237-756 547-574-312-132-312 167-2_W.mp3)
  Segment 23: 47452ms ~ 48028ms (file: 765-761-765-765-756-753-756-756-765-765-765-765-756-675-765-756-765-765-761-756-756 762-672-763-765-765-756-752-756-756-752-756-765-765-756-765-576-765-765 765-765-761_W.mp3)
  Segment 24: 49028ms ~ 49604ms (file: 132-104-103-124-175-103-132-103-123-102-104-132-124_V.mp3)
  Segment 25: 50604ms ~ 51180ms (file: 672-670-670-670-672-672-672-672-637-206-650-761-673-762-267-761-671-760-617_W.mp3)
  Segment 26: 52180ms ~ 52756ms (file: 623-362-132-152-163-162-106-163-163-106-103-312-136-130-132-162-130-601-216-630-620-160-106-106-123-306-163-216-163-103-103-132-631-617-126-103-102-130-612-157-576-765-267 126-1_V.mp3)
  Segment 27: 53756ms ~ 54332ms (file: 356-362-165-165-175-103-106-136-160-673-651-165-576-657-625-675-657-657-265-567-576-357-365-356-315-635-563-576-675-567-576-137-157-120-517-571-571-157-157-567-567-567-571-576-5_W.mp3)
  Segment 28: 55332ms ~ 55908ms (file: 671-164-126-102-162-162-163-615-136-103-136-136-163-163-106-103-713-106-675-176-137-103-132-236-263-106-631-567-623-176 132-136 632-163-126-236-136-261-167-123-103-106-136-516-1_V.mp3)
  Segment 29: 56908ms ~ 57484ms (file: 206-230-260-203-236-102-620-361-267-126-263-267-263-261-126-263-203-126-260-263-216-206-236-102-160-302-612-213-675-137-127-213-162-162-162-136-365-105-176-261-261-213-576-756-7_W.mp3)
  Segment 30: 58484ms ~ 59060ms (file: 267-267-267-627-672-627-627-627-671-765-765-762-574-675-672-267-627-276-321-321-273-327-327-372-215-376-672-176-267-267-726-762-712-261-267-267-375-317-713-713-731-267-267-267-2_W.mp3)
  Segment 31: 60060ms ~ 60636ms (file: 126-672-670-657-560-105-631-316-672-613-756-760-675-267-263-267-627-761-765-765-576-574-756-657-657-362-312-163-103-130-267-267-723-756-263-162-263-621-102-106-163-167-627-361-6_W.mp3)
  Segment 32: 61636ms ~ 62212ms (file: 675-675-673-756-310-310-312-620-310-231-263-276-123-123-130-132-162-201-106-562-516-756-765-706-716-650-631-621-620-256-267-620-625-623-263-361-236-361-312-132-754-705-301-103-5_W.mp3)
  Segment 33: 63212ms ~ 63788ms (file: 705-175-317-763-136-135-750-573-756-672-761-672-673-750-103-310-103-170-317-136-316-137-316-612-361-637-612-613-670-576-607-761-176-126-316-176-675-132-231-321-213-573-753-756-7_V.mp3)
  Segment 34: 64788ms ~ 65364ms (file: 326-302-102-321-316-261-126-236-310-310-321-320-231-627-627-627-263-623-160-517-620-160-152-167-673-617-562-167-612-601-610-617-765-765-756-756-761-765-675-761-715-170-137-261-1_W.mp3)
  Segment 35: 66364ms ~ 66940ms (file: 132-132-130-102 267 751-761-261-236-236-267-672 675-235-625-576-576-102-216-630-623-673-671-617 134-135-103-136-603-613-621-320-206-127-216-216-231-123-132-132-312-321-312-132-1_V.mp3)
  Segment 36: 67940ms ~ 68516ms (file: 103-517-541-504-103-135-570-574-257-613-560-751-152-163-603-165-301-167-261-103-162-263-321-132-276-103-106-623-612-137-103-261-612-165-136-615-216-312-132-176-756-567-613-130-6_V.mp3)
  Segment 37: 69516ms ~ 70092ms (file: 103-175-261-103-103-163-163-136-136-167-130-651-136-106-103-127-176-127-612-612-132-165-621-176 756-765-765-673-675-673-672-671-615-675-675-160 157-567-576-571-157-267-367-716-7_W.mp3)
  Segment 38: 71092ms ~ 71668ms (file: 237-761-162-672-627-657-765-675-675-627-672-675-627-672-672-567-576-675-675-672-567-657-675-765-675-675-672-675-627-267-627-672-672-672-672-657-567-576-675-765-627-627-627-627-6_W.mp3)
  Segment 39: 72668ms ~ 73244ms (file: 627-621-627-632-260-203-752-765-762-302-317 123-631-605-162 651-650-675-165-657-657-765-576-756-671-657-765-162-167-617-570-176-130-716-176-176-137-167-617-710-760-136-162-362-6_W.mp3)
  Segment 40: 74244ms ~ 74820ms (file: 672-632-162-306-637-657-605-106-106-210-163-126-162-103-310-236-620-260-206-261-576-657-576-527-132-102-103-130-260-652-260-231-217-213-126-312-301-320-320-320-362-367-306-305-7_W.mp3)
  Segment 41: 75820ms ~ 76396ms (file: 361-632-316-163-132-136-716-176-301-130-301-312-235-176-312-301-123-613-632-263-326-602-263-326-760-176-136-103-136-703-756-705-570-576-162-706-620-362-163-143-627-163-137-317-1_V.mp3)
  Segment 42: 77396ms ~ 77972ms (file: 756-576 756-567-576-675-756-765-756-765-675-763-765-320-312-321-307-574-526-601-162-132-320-326-123-130-123-350-675-673-316-160-310-176-137-160-137-106-102-106-102-103-235-261-3_W.mp3)
  Segment 43: 78972ms ~ 79548ms (file: 160-671-316 631-236-751-765-167-216-126-627-620-623-260-260-126-103-123-310-531-513-315-576-576-362-567-672-627 627-672-670-106-162-126-162-103-102-106-103-162-612-102-103-106-6_V.mp3)
  Segment 44: 80548ms ~ 81124ms (file: 670-607-607-627-260-260-206-120-163-103-165-103-312-361-132-326-130-132-273-627-106-163-210-260-260-160-617-167-126-213-123-320-236-231-236-230-123-165-672-265-203-271-172-136-3_V.mp3)
  Segment 45: 82124ms ~ 82700ms (file: 162-627-621-267-312-136-134-103 124-163 174-126-136-316-126-321-312-102-103-631-612-263-360-674-267-124-526-617-314-306-360-362-302-306-675-762-712-316-321_V.mp3)
  Segment 46: 83700ms ~ 84276ms (file: 106-765-756-103-160-607-125-176-150-127-276-127-103-617-103-172-176-106-617-103-123-106-527-761-106-137-617-167-106-125-716-175-516-152-236-126-576-627-317 301 627-623 756-756-6_V.mp3)
  Segment 47: 85276ms ~ 85852ms (file: 267-276-267-721-260-302-736-103-125-376-371-176-671-621-267-627-706-617-271-213-316-106-102-120-216-160-620-321-623-627-627-620-132-103-327-610-602-627-623-267-256-267-206-261-2_W.mp3)
  Segment 48: 86852ms ~ 87428ms (file: 103-132-671-136-126-216-106-106-103-123-123-106-136-106-163-103-632-612-162-613-103-137-103-123-615-216-617-163-617-176-167-176-167-102-317-103-170-176-236-160-615-301-576-261 7_V.mp3)
  Segment 49: 88428ms ~ 89004ms (file: 312-132-102-132-312-231-123-312-312-132-312-321-132-312-321-213-312-231-321-312-132-321-213-126-326-213-216-321-231-321-312-321-326-213-261-326-216-261-326-213-312-132-321-326-2_W.mp3)
  Segment 50: 90004ms ~ 90580ms (file: 301-312-167-132-137-136-132-162-132-321-135-316-136-132-126-130-103-106-103-123-136-130-361-651-176-612-261-301-163-316-310-623-163-130-167-136-163-513-165-765-176-765-675-167-1_W.mp3)
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
> **Time**: 2026-04-03 09:21:43
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "더럽다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "더럽다", "start_ms": 40, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "조용하다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "조용하다", "start_ms": 55, "dur_ms": 460, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "시끄럽다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "시끄럽다", "start_ms": 35, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "밝다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "밝다", "start_ms": 60, "dur_ms": 420, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "어둡다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "어둡다", "start_ms": 45, "dur_ms": 485, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "강하다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "강하다", "start_ms": 60, "dur_ms": 455, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "약하다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "약하다", "start_ms": 40, "dur_ms": 495, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "무겁다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "무겁다", "start_ms": 65, "dur_ms": 475, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "가볍다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "가볍다", "start_ms": 40, "dur_ms": 490, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "두껍다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "두껍다", "start_ms": 70, "dur_ms": 470, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "얇다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "얇다", "start_ms": 50, "dur_ms": 415, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "깊다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "깊다", "start_ms": 80, "dur_ms": 435, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "얕다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "얕다", "start_ms": 30, "dur_ms": 445, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "단단하다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "단단하다", "start_ms": 65, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "부드럽다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "부드럽다", "start_ms": 40, "dur_ms": 505, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "날카롭다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "날카롭다", "start_ms": 55, "dur_ms": 515, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "뭉툭하다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "뭉툭하다", "start_ms": 35, "dur_ms": 525, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "길다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "길다", "start_ms": 65, "dur_ms": 425, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "짧다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "짧다", "start_ms": 45, "dur_ms": 410, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "넓다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 75,
"audio_type": "speech",
"words": [
{"word": "넓다", "start_ms": 75, "dur_ms": 410, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "좁다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "좁다", "start_ms": 50, "dur_ms": 425, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "높다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "높다", "start_ms": 80, "dur_ms": 405, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "낮다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "낮다", "start_ms": 30, "dur_ms": 435, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "동그랗다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "동그랗다", "start_ms": 65, "dur_ms": 485, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "네모나다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네모나다", "start_ms": 40, "dur_ms": 515, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "세모나다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "세모나다", "start_ms": 55, "dur_ms": 505, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "빨갛다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "빨갛다", "start_ms": 35, "dur_ms": 495, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "파랗다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "파랗다", "start_ms": 70, "dur_ms": 505, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "노랗다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "노랗다", "start_ms": 40, "dur_ms": 515, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "초록색",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 75,
"audio_type": "speech",
"words": [
{"word": "초록색", "start_ms": 75, "dur_ms": 475, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "검정색",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "검정색", "start_ms": 50, "dur_ms": 485, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "하얀색",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "하얀색", "start_ms": 80, "dur_ms": 495, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "하나",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "하나", "start_ms": 30, "dur_ms": 385, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "둘",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "둘", "start_ms": 65, "dur_ms": 325, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "셋",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "셋", "start_ms": 40, "dur_ms": 335, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "넷",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "넷", "start_ms": 55, "dur_ms": 345, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "다섯",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "다섯", "start_ms": 35, "dur_ms": 415, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "여섯",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "여섯", "start_ms": 65, "dur_ms": 425, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "일곱",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "일곱", "start_ms": 40, "dur_ms": 435, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "여덟",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "여덟", "start_ms": 70, "dur_ms": 445, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "아홉",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "아홉", "start_ms": 55, "dur_ms": 425, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "열",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "열", "start_ms": 80, "dur_ms": 315, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "일",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "일", "start_ms": 30, "dur_ms": 285, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "이",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 60, "dur_ms": 295, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "삼",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "삼", "start_ms": 45, "dur_ms": 315, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "사",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "사", "start_ms": 55, "dur_ms": 325, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "오",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "오", "start_ms": 35, "dur_ms": 305, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "육",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "육", "start_ms": 70, "dur_ms": 345, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "칠",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "칠", "start_ms": 40, "dur_ms": 335, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "팔",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 75,
"audio_type": "speech",
"words": [
{"word": "팔", "start_ms": 75, "dur_ms": 325, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 09:21:43*
