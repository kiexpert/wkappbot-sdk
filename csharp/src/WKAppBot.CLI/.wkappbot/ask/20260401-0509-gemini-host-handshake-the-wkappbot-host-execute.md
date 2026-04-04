# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=14524  time=05:08:25  uptime=5095s  mem=2332MB  system_processes=767"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260401_050815.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 537 216-263-216-126-267-126-261-261-562-267-132-672_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 315-123-516-156-576-567-576-567-576-567-576-261-123-231-120-136-653-560-576-567-576-537-567-561-576-325-357-315-756-756-537-523-317-301-571-305-573-705-750-567-567-657-765-567-1_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 326-312-136-321-136-163-132-621-162-361-623-623-126-367-326_W.mp3)
  Segment 4: 4728ms ~ 6276ms (file: 316-315-231-267-236-123-657-765-657-526 130-130-103-561-612-360-162-132-106-163-361-136-126-632-631-312-103-163-657-103-613-103-163-160-163-163-103-716-263-671-162-136-103-137-1_V.mp3)
  Segment 5: 7276ms ~ 7852ms (file: 671-106-106-162-160-162-716-762-132-123-615-103-103-103-273-617-176-761-716 103-316-137-136-617-103-132-106-176-132-103-106-617-103-136-103-103-103-106 672-627-762-765-756-765-7_W.mp3)
  Segment 6: 8852ms ~ 9428ms (file: 123-231-675-672-603-103-162-132-136-260-206-267-206-260-102-162-320-301-102-312-132-310-210-361-230-750-372-123-106-156-103-103-103-135-163-765-321-230-312-316-130-316-316-675-7_V.mp3)
  Segment 7: 10428ms ~ 11004ms (file: 267-627-672-672-672-621-632-132-261-236-627-672-613-756-275-276-672-672-613-136 613-263-627-263-162-167-176-160-631-621-162-627-167-267-627 673-671-163-136-671-672-371-637-623-6_W.mp3)
  Segment 8: 12004ms ~ 12580ms (file: 613-617-617-163 103-106-160-160-167-162-301-263-326-103-123-102-103-136-302-301-312-137-273-203-230-230-203-201-206-201-162-102-126-301-103-130 103-106-315-312-213-130-326-320-1_V.mp3)
  Segment 9: 13580ms ~ 14156ms (file: 316-627-621-102-123-103-106-163-136-317-312-306-302-132-301-136-175-126-312-321-312-135-756-765-760-675-762-372-372-701-167-120-163-312-162-163-751-361-301-312-123-231-103-532-6_V.mp3)
  Segment 10: 15156ms ~ 15732ms (file: 160-130-213-273-730-317-573-706-263-203-602-362-263-206-206-203-623-216-716-761-715-206-230-320-623-732-751-372-302-613-176-175-751-701-362-230-203-206-302-132-321-103-206-236-2_V.mp3)
  Segment 11: 16732ms ~ 17308ms (file: 627-267-632-237-627-627-637-627-263-263-627-632-276-263-623-267-326-231-263-362-261-627-236-625-623-236-267-352-756-376-362-326-316-317-361-136-537-356-562-263-326-356-362-623-3_W.mp3)
  Segment 12: 18308ms ~ 18884ms (file: 310-130-301-132-317-236-263-162-136-132-123-326-321-602-261-762-273-602-206-263-213-261-123-623-136-675-132-102-321-136-132-123-326-312-132-312-562-625-756-107-103-163-167-106-5_V.mp3)
  Segment 13: 19884ms ~ 20460ms (file: 715-765-103-756-167-167-160-136-106-601-670-675-607-601-617-103-103-136-176-327-301-362-263-620-276-260-206-231-361-136-571-357-632-203-632-213-173-703-713-156-162-123-103-601-1_V.mp3)
  Segment 14: 21460ms ~ 22036ms (file: 761-163-136-103-162-103-167-167-136-765-103-310-130-301-136-301-163-106-613-620-620-620-136-302-321-302-302-260-620-620-621-612-632-263-206-135-132-310-367-316-163-213-261-103-1_V.mp3)
  Segment 15: 23036ms ~ 23612ms (file: 236-302-260-302-236-256-162-612-126-261-627-621-601-612-612-612-162-106-130-310-102-631-267-256-507-527-206-620-576-756-361-163-130-163-574-570-327-306-320-726 756-316-102-136-1_V.mp3)
  Segment 16: 24612ms ~ 25188ms (file: 306-312-103-162-172-126 574-576-572-567-732-317-576-167-627-612-157-312-165-163-126-103-316-316-163-625-631-165-612-362-132-132-167-162-623-602-167-316-163-617-162-613-160-167-1_W.mp3)
  Segment 17: 26188ms ~ 26764ms (file: 256-216-271-312-620-627-623-652-675-670-756-576-756-657-627-673-320-352-365-376-632 527-756-206-205-250-102-201 612-162-671-261-671-176-617-617-672-617-163-637-136-617-672-671-1_W.mp3)
  Segment 18: 27764ms ~ 28340ms (file: 231-321-236-237-524-527-652-362-316-316-136-136-163-361-135-362-312-316-315-516-315-236-265-236-326-326-321-326-376-547-537-253-256-256-326-136-531-517-257-256-235-132-312-231-2_W.mp3)
  Segment 19: 29340ms ~ 29916ms (file: 627-625-625-765-627-627-627-267-627-627-627-627-267-623-362-362-263-326-627-562-564-126-132-312-236-623-612-762-672-621-216-213-126-236-261-263-261-263-326-162-216-263-213-625-5_W.mp3)
  Segment 20: 30916ms ~ 31492ms (file: 765-765-765-716-765-765-756-765-671-672-672-165-631-675-673-637 137-106-102-715-715-176-715-712-163-620-623-236-624-165-216-105-123-301-320 315-165-167-301-302-756-135-165-107-1_V.mp3)
  Segment 21: 32492ms ~ 33068ms (file: 320-302-310-136-301-302-320-632-326-302-574-602-620-163-751-307-320-230-213-126-163-612-360-623-603-362 124-321 267-321-132-130-130-316-310-612-672-163-675-621-263-260-267-267-6_W.mp3)
  Segment 22: 34068ms ~ 34644ms (file: 607-132-162-126 163-103-156-103-671-167-567-716-167-362-136-137-136-617-136-765-623-152-256-617-176-671-623-632-637-671-126-163-261-623-316-213-567-675-627-267-216-267-216-132-3_V.mp3)
  Segment 23: 35644ms ~ 36220ms (file: 132-236-532-527-542-574-534-572-237-672-765-602-627-206-263-302-103-571-570-301-316 675-627-236-167-172-163-167-672-627-765-702-726-367-237-753-765-706-607-263-206-207-260-276-2_V.mp3)
  Segment 24: 37220ms ~ 37796ms (file: 607-103-763-165-163-103-612-761-103-670-765-167-652-126-167-701-167-176-671-136-103-136-610-650-136-162-541-615-613-106 130-610-613-160-176-637-163-564-627-162-631-103-627-260-6_V.mp3)
  Segment 25: 38796ms ~ 39372ms (file: 567-632-123-127-637-103-136-103-316-103-175-165-167-632-132-103-673-103-321-310-136-132-361-136-316-136-103-671-312-301-653-173-163-675-325-105-170-263-261 160-316-132-675-153-5_V.mp3)
  Segment 26: 40372ms ~ 40948ms (file: 156-536-513-536-513-751-567-157-571-157-516-531-517-576-571-567-561-576-567-576-537-653-573-635-536-517-567-567-165-615-561-516-657-527-537-567-536-635-651-516-561-567-536-653-7_V.mp3)
  Segment 27: 41948ms ~ 42524ms (file: 603-361-106-136-630-610-137-167-321-321-230-326-573-523-135-317-571-157-713-302-350-753-532-236-203-123-612-312-132-127-103-102-175-765-761-607-173-635-576-576-675-176-103-316-3_V.mp3)
  Segment 28: 43524ms ~ 44100ms (file: 326-231-126-216-321-216-236-612-532-675-167-716-321-361-761-620-135-672-321-326-326-126-132-321-537-537-576-571-231-130-301-310-216-310-103-312-136-672-267-216-267-230-231-570-5_W.mp3)
  Segment 29: 45100ms ~ 45676ms (file: 726-276-236-263-726-237-275-275-627-276-326-653-267-572-572-573-572-523-325-752-325-352-235-250-230-257-523-532-572-523-325-235-321-352-325-325-325-352-527-527-254-152-253-513-2_W.mp3)
  Segment 30: 46676ms ~ 47252ms (file: 326-325-326-235-231-321-312-612-312-361-361-362-276-326-327-236-326-235-325-325-326-326-236-236-326-216-321-321-273-325-236-236-236-325-375-352-325-532-352-253-534-352-251-521-5_W.mp3)
  Segment 31: 48252ms ~ 48828ms (file: 261-326-362-653-632-356-362-263-635-632-613-351-615-165-165-163-167-163-163-163-261-261-672-612-261-620-260-326-563-326-326-632-263-623-627-625-265-265-625-625-126-162-514-514-1_W.mp3)
  Segment 32: 49828ms ~ 50404ms (file: 132-713-125-651-132-137-735-103-654-135-623-260-361-671-623-761-267-621-627-163-617-127-103-103-103-160-627-176-156-612-157-561-106-716-176-625-602-103-103-167-567-216-614-361-2_V.mp3)
  Segment 33: 51404ms ~ 51980ms (file: 576-657-165-567-162-326-163-351-657-576-567-576-576-576-567-576-765-576-567-675-576-765-567-756-765-576-576-756-567-756-765-517-652-576-576-576-657-657-567-576-756-675-567 657-5_W.mp3)
  Segment 34: 52980ms ~ 53556ms (file: 657-675-657-567-567-567-567-567-563-563-163-635-567-637-653-657-657-576-657-651-165-157-576-517-571-576-567-576-571-576-157-157-751-167-516-651-571-576-567-576-567-576-761-567-5_W.mp3)
  Segment 35: 54556ms ~ 55132ms (file: 326-627-612-162-621-126-132-316 126-621-261-627-136-217 276-263-623-576-261-231-321-132-321-236-602-206-213-235-362-356-123-312-621-123-316 675-615-671-651-653-623-567-256-265-5_W.mp3)
  Segment 36: 56132ms ~ 56708ms (file: 312-361-123 326-651-562-652-123-534-514-523-573-152-512-321-312-231-231-132-352-236-263-261-263-236-263-321-237-231-532-753-672-627-362-135-162-163-362-362-321-321-321-132-312-3_W.mp3)
  Segment 37: 57708ms ~ 58284ms (file: 316 163-103-135-162-176-165-623-136-126-126-275-156-136-237-613-671-162-673-123-162-621 312-102-765-756-756-750-765-765-765-627-231 136-163-263_W.mp3)
  Segment 38: 59284ms ~ 59860ms (file: 107-132-623-673-263-527-570-705-137-602-630-623-367-314-317-376-621-263-326-312-103-163-671-716-736-706-754-756-765-765-167-176-143-137-315-314 570-571-567-156-134-316-567-360-3_V.mp3)
  Segment 39: 60860ms ~ 61436ms (file: 620-206-260-163-160-623-162-610-160-123-321-203 102-103-312-216-132-631-136-631-163-657-672-615-153-316 671-321-312-321-321-167-163-162 672-316-312 670-672-670-312-132-316-236-3_W.mp3)
  Segment 40: 62436ms ~ 63012ms (file: 567-615-157-156-165-156-165-165-162-165-165-651-651-651-356-652-675-675-657-657-506-650-650-506-651-560-562-567-562-561-561-567-567-657-657-567-567-576-657-561-567-576-657-765-5_W.mp3)
  Segment 41: 64012ms ~ 64588ms (file: 657-765-167-105-567-567-165-765-150-176-716-176-621-617-160-136-106-567-765-312-701-637-612-163-732-651-176-571-507-136-176-625-107-105-172-105-103-106-671-103-621-267-620-612-1_V.mp3)
  Segment 42: 65588ms ~ 66164ms (file: 360 612-326-312-103 574-574-574-547-546-547-547-546-547-547 263-261-312-136-130 750-756-756-706-602-630-623-260-263-260-260-326-631-751-765-736-301-310-132-103-103-103-657-617-1_W.mp3)
  Segment 43: 67164ms ~ 67740ms (file: 632-321-326-216-316-316-362-361-317-657-136-316-136-316-631-167-130-675-765-163-637-613-615-316-610-630-236-316-617-673-617-361-613-163-130-637-617-673-723-763-367-675-671-267-6_W.mp3)
  Segment 44: 68740ms ~ 69316ms (file: 765-103-103-136-710-750-726-103-310-136-231-672-657-603-573-305-137-317-312_V.mp3)
  Segment 45: 70316ms ~ 70892ms (file: 237-213-321-301-301-301-301-301-312-321-316-756-276-765-756-675-675-670-760-617-167-261-641-132-614-613-413-107-107-107-137-731-712-132-132-301-512-765-106-601-103-360-617-672-6_V.mp3)
  Segment 46: 71892ms ~ 72468ms (file: 562-567-563-657 267-526-652-536-627-672-625-265-327-376-261-567-526-576-761-751-657-567-657 763-371-126-132-621-623-621-263-623-162-726-267-126-316-312-567-675-567-627-627-625-6_W.mp3)
  Segment 47: 73468ms ~ 74044ms (file: 216-612-162-106-120-132-163-156-126-162-516-106-132-514-132-176-617-106-130-156-213-137-162-160-167-163-167-136-167-627-261-263-130-173-367-263-623-620-261-157-316-126-162-576-6_V.mp3)
  Segment 48: 75044ms ~ 75620ms (file: 627-672-672-675-657-726-632-627-263-216-251-235-213-137-123-132-123-213-123-132-123-217-132-213-621-621-617-675-627-627-621-627-671-671-516-357-137-326-673-361-532-213-213-123-3_W.mp3)
  Segment 49: 76620ms ~ 77196ms (file: 576-562-527-576-576-564-567-567-576-576-574-567-756-576-567-576-576-527-532-567-265-265-362-567-652-657-652-657-625-756-675-657-762-726 576-567-576-567-526-576-576-576-567-576-5_W.mp3)
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
> **Time**: 2026-04-01 05:09:16
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
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "포커스", "start_ms": 15, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "양보가", "start_ms": 200, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "필요합니다", "start_ms": 370, "dur_ms": 180, "phase_lr_deg": 0.0}
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
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-01 05:09:16*
