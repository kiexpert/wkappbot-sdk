# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=14524  time=05:19:15  uptime=5746s  mem=2099MB  system_processes=788"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260401_051905.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 657-675-675-675-576-567-652-637-362-576-567-657-673-672-671-765-672-672-673-617-627-267-627-216-126-672-267-612-627-762-762-672-672-627-627-627-627-627-672-672-672-672-672-672-6_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 627-162-623-621-326-612-613-621-351-631-621-613-673-163-163-136-165-316-376-361-163-657-765-126-623-362-216-672-103-132-310-162-631-612-160-216-261-761-761-765-675-765-762-765-7_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 617-163-216-362-657-631-167-163-613-632-312-132-316-316-631-637-160-631-610-163-613-617-632-167-561-163-315-671-673-637-617-316-361-132-163-361-613-532-621-621-621-631-613-316-1_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 236-236-263-261-231-326-321-213-213-321-362-316-136-132-213-312-216-216-216-261-236-213-216-231-261-216-216-126-126-126-213-216-321-321-231-321-216-263-261-215-263-263-261-265-2_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 765-765-675-765-106-176-753-713-731-761-671-621-601-671-602-307-301-350-530-136-301-310-360-130-756-573-763-765-762-765-760-573-576-310-306-326-320-263-261-203-236-236-260-236-6_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 213-265-265-654-156-251-165-312-361-312-315-365-356-354-352-253-352-354-352-357-357-351-567-315-173-157-517-754-570-560-150-105-510-507-517-352-352-352-351-540-256-256-256-326-2_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 756-316-326-236-261-263-560-362-236-236-263-263-267-265-263-561-567-651-136-132-105-160-610-106-130-231-236-123-126-162-623-657-156-160-560-567-576-567-326-321-213-612-136-261-5_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 312-126-621-163-675-765-670-670-316-163-103-132-312-213-106-103-132-607-567-765-760-265-206-132-254-324-320-136-130-162-316-136-103-103-132-136-361-326-362-213-263-623-261-627-6_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 750-751-570-576-576-576-576-267-620-632-623-260-630-603-632-607-167-103-167-106-167-167-106-160-357-375-230-203-130-132-301-123-312-206-312-671-310-623-203-216-102-167-106-602-6_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 671-167-675-612-162-625-163-126-167-126-136-136-167-167-165-162-165-167-167-176-172-215-126-267-127-176-671-675-632-672-261-625-675-376-567-752-271-723-762-253-125-175-156-157-1_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 215-652-265-526-236-236-123-123-126-123-136-162-126-126-213-321-312-362-123-176-167-136-362-261-361-326-216-312-326-213-136-216-514-547-356-132-216-326-316-632-326-637-265-265-6_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 167-672-167-613-627-321-620-362-326-263-125-126-672-670-675-706-607-603-162-106-613-126-163-106-162-106-103-263-120-123-106-136-125-632-617-675-103-106-610-160-106-136-160-631-1_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 217-672-612-372-623-312-130-126-165-162-263-103-103-261-136-132-103-612-167-127-162-632-263-362-103-132-314-312-136-163-761-312-154-324-213-260-267-672-321-132-167-760-326-326-3_V.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 163-105-103-103-310-321-312-321-152-106-106-106-162-106-756-267-276-756-756-760-603-632-260-263-206-106-130-103-160-236-132-162-263-621-326-672-601-106-362-367-362 327-275-210-7_V.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 134-125-156-135-570-756-672-675-576-576-507-570-176-132-136-301 132-167-137-271-127-132-154-135-173-761-176-176-764-761-761-671-162-163-136-762-237-372-376-371-132-143-132-613-6_V.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 361-236-312-135-301-736-136-123 567-576 627-627_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 615-163-106-360-361-673-617-163-163-165-136-162-163-361-631-671-613-765-123-132-310-102-310-630-263-672-762-627-623-162-276-163-531-573-206-267-621-136-531-356-362-360-237-627-6_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 302-375-203-230-321-123-132-123-271-256-261-176-762-726-276-216-163-631-612-562-756-726-713-267-320-321-137-756-260-230-302-306-357-132-136-651-627-621-612-605-672-672-627-105-1_V.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 203-627-756-172-627-156-162-165-601-615-613-167-761-167-571-576-261-213 620-267-263-320-620-620-621-762-765-360-361-613-106-617-163-613-326-601-607-607-672-675-672-672-625-657-6_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 175-162-507-756-756-107-260-620-620-627-627-231-715-735-762-756-735-756-756-765-756-765-276-275-765-765-316-123-576-705-761-751-716-762-132-126-612-167-671-672-672-627-627-621-2_W.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 215-203-523-352-123-265-215-517-752-732-703-275-275-275-725-524-547-540-530-250-203-236-231-321-206-231-260-213-216-213-231-325-321-123-231-312-235-325-321-547-543-547-574-257-2_W.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 615-371-673-673-564-576-526-256-256-326-316-136-531-561-257-256-235-163-627-672-267-526-527-326-312-132-136-136-106-156-156-563-615-613-657-615-256-256-265-321-312-321-123-126-2_W.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 361-632-612-123-126-316-736-672-631-136-263-123-310-236-276-327-213-102-236-162-126-231-201-206-321-302-302-263-620-602-130-103-103-136-206-260-120-102-130-162-615-670-302-263-1_V.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 261-312 610-130-736-126-567-102-106-120-125-275 753-735-753-671-617-610-176-170-173-630-361-167-103-163-601-160-162-602-216-276-630-167-136-163-217-623-621-623-617-103-675-106-1_W.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 625-627 625-603 670-675 657-672-260-612-260 627-260 623-623 631-163-527-672-765-762-761-627 762-623-267-215-132 263 142-265-231-672-267-672-620_W.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 652-126-613-135 102 765-765-726-627 321-326-567-765-326-123-672-132-627-312_W.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 320-312-310-123-652-136-135-167-106-160-617-167-106-106-106-106-621-260-261-230-320-201-263-106-103-103-135-132-103-130-103-312-263-763-761-167-751-367-316-301-132-102-102-106-1_W.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 106-651-605-675-615-153-765-675-762-136-613-162-106-167-162-623-216-126-627-613-572-265-320-673-635-613-136-301-167-301-765-230-263-206-261-326-621-613-631-613-612-614-506-657-6_V.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 627-652-263-672-236-625-675-605-326-235-253-623_W.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 162-671-613-761-672-672-156-165-130-310-574-576-675-675-574 172 671-671-126-213-143-721-516-627-361-627-612-163-301-307-326-672-136-105-310-301-670-765-675-761-671-602-716-715-7_W.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 316-672-675-672-627-762-627-531-315-576-617-657-567-576-761-163-675-162-601-621-261-162-613-163-163-163-106-613-632-621-627-632-265-263-236-627-263-623 103-123-765-732 132-103-1_W.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 261-261-263-267-263-236-126-512-512-352-352-315-312-263-621-156-315-263-236-263-326-321-312-321-326-361-316-326-325-235-523-536-523-352-537-235-236-265-162-263-263-526-562-652-3_W.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 652-263-326-320-326-326-320-326-236-620-620-620-620-260-136-617-621-673-102-617-162-316-153-312 160-103-136-312-236-213-130-102-136-103-103-103-516-130-167-627-765-675-156-613-7_V.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 163-136-316-107-167-163-617-617-670-136-136-103-675-761-617-673-103-175-517-265-316-136-103-316-132-301-132-316-167-136-107-136-675-123-126-167-162-167-167-176-136-567-216-621-1_V.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 123-216-236-276-723-673-173-632-261-261-267-361-627-672-312-312-326-762-627-276-367-216-321 123-763-607-602-607-567-706-761-763-652-215 102-301-162-546-637-316-627_W.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 652-623-621 675-367-320 213-162-163-123-130-613 216-162-165-163-312-132-136-213-162-210-120-321-312-352-576-567-576 260-260-675-362-203 261-126-261-136-132-627-163-321-521-716-6_W.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 623-632-362-236-326-632-326-623-236-362-263-236-263-263-263-236-623-263-263-623-263-326-236-263-263-236-261-236-263-263-236-236-263-263-263-263-236-236-263-236-263-672-632-617-6_W.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 542-523-265-265-256-526-362-326-326-326-321-237-231-325-256-562-513-573-356-316-360-136-527-537-576-576-265-261-312-136-627-256-527-257-527-572-267-621-362-621-162-126-231-231-2_W.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 170-163-107-713-567-103-130-103-675-167-610-160-317-136-327-721-130-107-136-316-103-316-136-106-316-136-162-362-312-130-136-650-265-136-756-716-261-317 163-167-130-362-123-621-1_V.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 106-162-612-612-162-236-263-362-263-162-312-310-312-231-261-326-316-362-312-321-301-320-231-102-302-153-157-105-156-102-130-102-310-160-610-106-162-621-623-610-163-613-621-620-6_V.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 653-653-651-615-620-765-675-672-672-672-627-620-267-672-620-657-602-261-672-657-637-306-302-320-326-360-536-657-765-675-567-567-675-765-675-267-672-263-203-267-276-620-260-620-2_W.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 753-176-671-637-674-614-613-765-765-327-326-263-236-231-263-236-526-521-526-312-136-163-163-617-630-367-630-627-367-263-236-136-165-167-136-213-123-132-756-765-670-756-302-367-3_V.mp3)
  Segment 43: 66192ms ~ 66768ms (file: 103-167-761-762-617-761-760-670 150-150-135-125-301-302-316-132-234-123-162-176-175-251-251-765-671-162-132-165-156-653-561-156-601-756-752-752-760-276-627-576-603-673-567-756-5_W.mp3)
  Segment 44: 67768ms ~ 68344ms (file: 623-231-236-265-326-261-236-235-261-213-126-263-615-126-162-125-126-126-162-157-756-321-132-136-231-123-321-162-136-631-631-362-325-231-625-612-123-165-216-126-126-126-312-326-3_W.mp3)
  Segment 45: 69344ms ~ 71252ms (file: 136-316-361-136-312-132-137-123-527-531-215-312-321-136-316-132-317-132-143-361-306-302-312-301-276-756-132 123 672-672-632 673-317-176-107-173-761-736-173-132-372-372-361-137-1_V.mp3)
  Segment 46: 72252ms ~ 72828ms (file: 316 610-617-163-103-132-103-102-103-123-132-162-102-751-126-312-321-312-326-325-152-132-130-132-103-132-256-206-260-263-503-523-132-216-120-623-361-503-103-120-612-671-607-163-6_W.mp3)
  Segment 47: 73828ms ~ 74404ms (file: 321-237-326-236-231-123-126-321-567-576-635-316-361-726-326-632-236-237-653-657-126-756-326-321-321-754 576-506-621-162-106-326-326-625-612-621-657-156 576-756-716-716-156-567-5_W.mp3)
  Segment 48: 75404ms ~ 75980ms (file: 103-326-136-756-765-312-136 132-103-103_V.mp3)
  Segment 49: 76980ms ~ 77556ms (file: 567-123-172-763-726-762-276-127-701-760-736-176-617-657-756-657-536-236-620-236-261-627-627-503-572-572-312-103-130-103-326-102-310-201-621-672-261-302-316-621-631-306-637-316-3_W.mp3)
  Segment 50: 78556ms ~ 79132ms (file: 162-106-167-103-136-167-176-103-106-167-106-263-230-236-623-207-267-276-671-671-765-672-761-627-136-321-126-136-765-705-317-327 613-132-162-125-312-314-132 163-103-617-160-167-1_V.mp3)
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
> **Time**: 2026-04-01 05:20:08
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
*Generated by WKAppBot ask gemini — 2026-04-01 05:20:08*
