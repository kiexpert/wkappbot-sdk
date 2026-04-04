# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=14524  time=05:21:28  uptime=5879s  mem=2431MB  system_processes=791"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260401_052118.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 160-136-103-136-136-613-163-103-126-163-317-106-106-176-103-162-167-176-163-761-167-107-176-103-617-231-126-106-326-312-312-326-261-263-230-263-276-276-263-213-265-263-263-547-5_W.mp3)
  Segment 2: 1576ms ~ 2188ms (file: 130-307-316-306-137-360-301-301-321-302-320-362-320-230-132-130-302-310-361-301-103-102-504-504-532-362-301-132-170-173-132-302-320-213-751-213-236-532-542-524-320-206-257-253-2_W.mp3)
  Segment 3: 3188ms ~ 3764ms (file: 413-261-127-123-132 213-263-236-261-621-627-230-203-321-324-236-261-264-216-265-234-237-263 103-137-136-301-312-316-123-132-153-135-760-765-706-706-176-607-671-136-361-310-702-3_V.mp3)
  Segment 4: 4764ms ~ 5340ms (file: 107-631-613-162-132-174-175-143-132-675-675-675-357-754-756-756-627-627-576-507-752-713-132 632-621 652-652-625-620-263-123-132-756-756-765-657-625 631 372-361-361-751-705_W.mp3)
  Segment 5: 6340ms ~ 6916ms (file: 103-617-103-167-617-162-165-130-163-623-632-160-126-157-103-564-106-136-126-162-675-617-623-761-376-132-103 125-123-126-126-123-127-123-127-102-127-172-126-127-172 326-675-621-3_W.mp3)
  Segment 6: 7916ms ~ 8492ms (file: 367-316-671-367-627-761-276-672-316-167-162-236-263-267-613-362-631-613-362-326-236-362-673-736-673-637-627-627-672-637-267-267-267-627-351-132-126-632-312-612-631-632-623-621-1_W.mp3)
  Segment 7: 9492ms ~ 10068ms (file: 576-567-615-576-675-657-576-657-657-657-675-632-326-316-657-615-607-602-203-237-237-130-102-103-301-302-136-120-206-321-306-130-316-531-517-105-130 637-267-567-652-657-562-637-6_W.mp3)
  Segment 8: 11068ms ~ 11644ms (file: 132-326-261-132-317-132-327-317-536-267-132-203-213-231-321-102-132-312-316-326-263-613-675-765-675-672 173-650-621-620 762-267-213-617-123-132-312-316-231-132-624-613-612-241-3_V.mp3)
  Segment 9: 12644ms ~ 13220ms (file: 160-162-627-103-167-301-165-630-160-106-130-615-316-620-610-162-167-106-162-761-170-617-163-163-613-137-561-123-206-126-126-163-136-136-103-610-160-601-516-163 167-163-263-156-6_V.mp3)
  Segment 10: 14220ms ~ 14796ms (file: 623-621-263-627-267-267-267-267-623-263-621 103-601-106-623-316-213-132-163-102-103-106-103-106-160-360-163-613-163-623-621-216 621-261-107-170-167-167-621-162-765-762-756-675-1_W.mp3)
  Segment 11: 15796ms ~ 16372ms (file: 136-163-106-163-613-120-152-102-156-165-610-163-106-103-132-173-102-162-613-716-103-163-130-106-103-136-172-127-615-516 103-167-126-167-561-615-310-316-705-137-316-623-206-130-2_V.mp3)
  Segment 12: 17372ms ~ 17948ms (file: 201-102-236-136-132-627-236-206-267-357-235-265-267-263-621-732-326-230-132-126-361-136-216-361-561-651-673-612-163-612-163-631-132-136-316-613-612-621-162-615-136-132-263-236-2_W.mp3)
  Segment 13: 18948ms ~ 19524ms (file: 213 135-301-206-524-123-567-576-517-756-625-167-163-106-106-160-106-150-540-514-206-126-620-620-613-106-601-106-167-105-106-562-123-132-576-105-126-103-201-652-320-517-103-130-6_V.mp3)
  Segment 14: 20524ms ~ 21100ms (file: 635-265-362-625-372-367-362-367-235-265-321-273-723-321-267-273-217-327-376-736-732-327-627-627-267-267-213-217-273-236-372-725-762-756-276-657-762-276-215-127-271-216-312-321-3_W.mp3)
  Segment 15: 22100ms ~ 22676ms (file: 137-312-361-362-361-305-301-103-261-316-130-310-126-672-607-103-163_V.mp3)
  Segment 16: 23676ms ~ 24252ms (file: 163-761-136-136-103-105-561-267-126-612-617-362-621-134-132-162 601-610-617-136-701-603-103-136-126-167-160-631-601-326-103-162-362-261-123-256-162-263-236-236-231-623-623-672-1_V.mp3)
  Segment 17: 25252ms ~ 25828ms (file: 261-756-756-756-765-675-357-315-132-231-312-251-326-321-540-507-517-153-123-132-312-316-136-132-315-312-136 675-607-607-675-672-627-276-762-637-312-301-316-317-316-301-312-601-2_V.mp3)
  Segment 18: 26828ms ~ 27404ms (file: 627-627-261-617-163-672-762-276-267-673-276-267-623-267-261-267-263-213-267-267-627-627-267-627-627-627-672-627-675-627-623-316-236-672-627-361-163-672-327-672-267-263-267-362-2_W.mp3)
  Segment 19: 28404ms ~ 28980ms (file: 213-213-237-217-235-325-132-312-372-327-376-327-237-237-237-237-273-273-372-732-372-372-237-237-273-236-236-263-236-263-263-236-236-273-327-327-372-327-267-267-261-725-276-726-7_W.mp3)
  Segment 20: 29980ms ~ 30556ms (file: 132-261-237-263-261-326-236-237-267-261-213-261-216-726-275-267-621-213-231-216-276-276-627-163-176-521-526-326-362-271-213-271-213-216-236-172-372-127-625-162-216-271-672-160-1_W.mp3)
  Segment 21: 31556ms ~ 32132ms (file: 176-162-316-123-216-106-362-216-263-102-213-326-750-675-312-103-160-106-132-561-326-162-312-103-103-137-107-106-136-132-162-670-675 617-162-360-613-675-765-670-765-213-103-103-1_V.mp3)
  Segment 22: 33132ms ~ 33708ms (file: 235-321-312-672-571-752-175-716-756-765-735-756-756-706-751-165-751-162-267-267-260-126_W.mp3)
  Segment 23: 34708ms ~ 35284ms (file: 263-763-167-263-136-231-623-675-672-675-652-574-574-162-321-316-321-361-136-236-316-653-136-312-317-126-613-671-167-617-310-560-326-631-367-360-653-627-627-321-130-310-615-312-1_W.mp3)
  Segment 24: 36284ms ~ 36860ms (file: 361-326-376-356-532-536-567-547-540-543-354-523-253-275-765-235-532-615-132-312-317-357-136-736-371-136-316-613-316-367-361-763-127-237-726-762-326-673-361-756-725-275-276-327-1_W.mp3)
  Segment 25: 37860ms ~ 38436ms (file: 362-263-623-263-201-120-315-106-510-102-765-620-260-607-620-756-756-765-207-236-301-302-236-365-306-320-302-752-725-627-627-316-163-726-263-267-206-130 620-263-623-326-371-306-3_W.mp3)
  Segment 26: 39436ms ~ 40012ms (file: 716-170-176-136-413-275-207-270-276-276 710-163 137-127-143 147-170-175 765-175-106-170-567-576-275-237-632-762-732-356-375-307-657-326-325-315-361-267-236-162-167-162-765-756-5_W.mp3)
  Segment 27: 41012ms ~ 41588ms (file: 153-516-375-152-321-652-632-623-261-123-326-312-672-265-312-132-312-372-123-137-174-137-172-217-213-127-127-173-176-671-761-513-273-712-217-274-217-173-173-137-317-631-261-231-2_W.mp3)
  Segment 28: 42588ms ~ 43164ms (file: 736-657-632-362-675-576-673-637-750-706-705_W.mp3)
  Segment 29: 44164ms ~ 44740ms (file: 213-231-217-537-572-265-276-276-312-672-326-623-675-627-267-267-627-625-203-625-576-367-713-156-106 607-670-602-620-607-612-231-236-263-236-320-162-163-675-675-620-263-267-261-2_W.mp3)
  Segment 30: 45740ms ~ 46316ms (file: 250-256-256-265-256-265-235-235-362-257-256-265-326-326-236-265-265-256-625-265-523-635-652-627-265-612-623-126-625-125-163-126-165-165-165-265-265-165-562-156-135-156-163-315-2_W.mp3)
  Segment 31: 47316ms ~ 47892ms (file: 163-162-301-312-136-271-132-132-103-612-632-652-506-567-263-652-106-132-130-103-106-106-102-103-362-306-301-362-762-627-260-320-103-132-103-103-137-765-203-203-620-602-607-620-2_V.mp3)
  Segment 32: 48892ms ~ 49468ms (file: 302-302-163-126-260-260-260-302-320-320-326-236-320-326-312-321-210-132-103-132-301-103-103-167-106-167-163-536-103-310-136-302-301-103-312-103-130-312-103-132-136-163-136-157-3_V.mp3)
  Segment 33: 50468ms ~ 51044ms (file: 316-130-130-153-516-576-576-507-576-576-574-507-157-567-507-265-516-670-136-261-671-765-167-765-761-107-167-167-671-615-630-362-237-326-361-326-302-326-362-326-507-570-574-657-7_V.mp3)
  Segment 34: 52044ms ~ 52620ms (file: 625-263-231-263-675-657-326-523-163-753-762-126-216-321-231-136-132-123-312-130-231-162-361-156-675-106-516-513-657-576-576-756-675-567-564-560-132-312-123-132-123-132-312-132-5_W.mp3)
  Segment 35: 53620ms ~ 54196ms (file: 675-657-567-657-675-576-657-561-657-657-567-567-675-675-657-567-576-651-561-653-573-567-576-572-315-563-563-357-651-356-567-561-316-567-361-365-315-365-653-136-367-136-316-617-6_W.mp3)
  Segment 36: 55196ms ~ 55772ms (file: 756-576-657-265-267-267-765-675-765-672-675-672-672-602-627-601-160-167-612-163-163-106-610-670-672-762-362-136-301-361-637-617-316-163-613-623-361-316-167-716-715-756-705-761-1_W.mp3)
  Segment 37: 56772ms ~ 57348ms (file: 675-657-567-576-576-150-136-310-761-763-765-736-756-765-716-761-765-765-760-175-762-675 657-760-761-763-675-756-756-671-106-173-376-103 216-316-260-312 516-315 106-136-103-103-1_W.mp3)
  Segment 38: 58348ms ~ 58924ms (file: 620-632-236-632-762-312-265-236-263-623-157-657-567-657-672-562-576-625-623-163-623-657-162-612-613-316-621-671-321-617-613-612-162-621-612-136-361-162-612-210-216-321-631-671-6_W.mp3)
  Segment 39: 59924ms ~ 60500ms (file: 206-236-632-632-362-136-507-507-574-576-367-671-106-103-301-310-326-756-206-261-206-126-612-162-613-163-612-312-321-302-326-326-326-263-256-623-261-603-235-523-325-316-135-103-1_W.mp3)
  Segment 40: 61500ms ~ 62076ms (file: 617-625-321 572-257-673 126-103 103-167-137-103-176-170-176-351-136-125-130 176-103-167-623-106-106-103-316-163-130-123-130-514-126-256-127-106-103-136-130-106-761-356-136-163-1_V.mp3)
  Segment 41: 63076ms ~ 63652ms (file: 167-732-106-312-132-103-756-675-126-670-361-623-162-270-321-162-610-312-132-263-162-167-150-576-617-637-632-657-751-367-317-162-162-123-132-136-126-613-126-576-576-567-612-263-2_W.mp3)
  Segment 42: 64652ms ~ 65228ms (file: 762-672-670-672-672-672-675-671-672-627-276-675-765-675-612 615-617-631-623-261-236-263-621-162-126-623-326-613-623-362-362-236-261-623-631-637-236-261-632-612-623-362-126-623-6_W.mp3)
  Segment 43: 66228ms ~ 66804ms (file: 236-235-257-257-256-216-236-236-236-236-123-326-312-321-237-263-326-236-512-657-135-317-327-617-627-671-621-163-326-326-236-236-261-361-123-312-123-213 261-231-236-316-326-261-3_W.mp3)
  Segment 44: 67804ms ~ 68380ms (file: 136-136-617-106-123-260-120-671-527-621-610-612-162-130-162-165-162-132-106-761-361-610-136-160-103-163-321-716_W.mp3)
  Segment 45: 69380ms ~ 69956ms (file: 652-627-672-657-567-762-762-567-625-673-652-567-257-576-765-627-657-576-762-672-627-623 625-623-657-362-267-672-631-653-657-567-653-657-576-576-567-567-567-562-567-567-576-576-5_W.mp3)
  Segment 46: 70956ms ~ 71532ms (file: 627-562-627-326-627-627-275-627-652-627-627-607-167-162-632-167-671-657-672-627-567-627-672-625-567-672-765-672-526-672-762-672-263-302-236-632-306-367-130-103-102-106-671-672-6_V.mp3)
  Segment 47: 72532ms ~ 73108ms (file: 256-132-267-562-127-512-251-132-162-126-162-162-132-136-163-164-157-175_V.mp3)
  Segment 48: 74108ms ~ 74684ms (file: 176-137-136-132-671-160-162-675-136-261-162-103-360-136-126-132-126-251-671-621-136-267 136-167-136-106-163-136-106-103-137-103-631-163-316-165-317-521-132-106-103-126-163-601-6_V.mp3)
  Segment 49: 75684ms ~ 76260ms (file: 132-301-132-136-312-451-135-261-162-261-567-652-132-127 567-162-263-126-102-302-312 623-206-203-621-263-621-623-627-261-312-132-136-126-312-754-751-715-361-317-721-312-103-130-5_V.mp3)
  Segment 50: 77260ms ~ 77836ms (file: 312 537-203-213-312-312-230-306-620-630-602-103-163-132-163-501-517-562-103-301-312-103-153-153-135-135-351_V.mp3)
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
> **Time**: 2026-04-01 05:22:20
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
{"word": "필요합니다", "start_ms": 370, "dur_ms": 190, "phase_lr_deg": 0.0}
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
*Generated by WKAppBot ask gemini — 2026-04-01 05:22:20*
