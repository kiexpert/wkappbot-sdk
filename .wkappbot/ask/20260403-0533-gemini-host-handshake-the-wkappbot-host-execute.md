# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=05:32:27  uptime=15192s  mem=1591MB  system_processes=446"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_053218.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 620-260-260-260-260-260-230-752-320-230-230-321-726-672-657-625-652-572-572-256-650-620-206-260-562-657-216-302-361-126-320-132-106-321-137-652-261-103-657-367-160-165-162-162-2_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 127-725-132-210-326-302-132-320-231-301-201-321-321-236-320-623-362-623-265-326-312-326-320-326-321-321-273-273-327-732-327-270-324-302-325-325-307-237-312-321-132-136-236-312-3_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 213-203-213-205-326-612-106-107-103-136-163-150-576-312-310-102-106-132-312-103-231-652-602-135-132-315-761-326-310-573-165-637-230-310-306-103-132-136-531-165-571-320-230-327-6_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 326-632-263-236-326-230-203-320-237-203-327-321-236-235-321-620-260-263-623-260-263-206-320-130-547-103-321-321-230-230-231-126-103-120-230-210-361-160-517-536-230-132-321-236-2_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 631-162-302-657-675 632-362 261-261-162-130-657-163-123-132-103-136-160-615-106-152-365-160-751-165-167-163-106-610-675-617-761-106-163-173-651-165-167-167-136-163-127-671 627-1_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 132-132-137-725-130-317-103-312-132-125-201-260-321-231-320-132-213-236-562-756-752-157-527-576-372-321-206-207-267-327-132-130-107-756-705-275-627-672-301-201-321-130-123-732-2_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 574-576-756-576-574-105-106-574-574-156 756-754-756-756-756 567-561-236-237-523-312-312-312-132-312-132-302-230-206-236-231-302-326-132-123-103-132-321-231-362-301-103-132 761-6_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 561-516-165-165-615-167-165-165-165-153-135-136-165-135-135-136-136-136-135-136-165-163-165-516-615-635-625-652-657-657-537-651-615-613-516-560-650-675-560-165-615-651-657-567-5_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 123-163-132-312-136-132-317-572-513-513-312-301-750-756-235-273-230-237-523-235-321-603-620-263-250-213-217-574-135-532-512-157-237-320-310-132-504-532-531-103-132-540-547-574-5_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 567-106-106-136-132-617-675-162-167-617-167-176-160-106-106-137-103-736-163-516-137-106-315-162-176-136-162-106-156-106-130-173-126-156-123 576-756-216-567-352 160-610-162-576-7_V.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 312-601-670-672-607-607-506-126-132-103-106-617-260-162-103-167-106-216-216-613-163-613-136-103-632-163-601-167-627-561-627-216-617-136-167-164-163-615-613-132-136-136-307-705-6_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 217-217-210-201-106-216-213 261-103-653-236-126 167-761-102-136-102-132-167-176-103-136-163-672-671-261-132-153 103-103-312-106-167-163-163-157-263-127-162-316-103-160-216-236-1_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 756-756 362-173-362-621-162-621-167-765-670-106-657-657-576-572-576-756-526-160-167-167-163-136-157-651-621-167 231-123_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 261-261-263-236-263-261-261-263-263-213-231-321-125-136-125-162-615-165-162-321-236-231-321-312-132-312-126-216-316-136-632-163-162-123-126-216-263-236-216-632-675-657-657-657-3_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 657-267-627-627-276-263-276-267-267-261-261-263-236-263-236-261-236-263-263-236-260-261-261-263-621-261-265-265-216-572-571-256-263-263-263-261-263-236-263-263-236-326-312-231-1_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 167-610-160-162-123-320-657-126-160-657-312-167-620-162-106-103-316-326-621-672-752-573-172-527-751-623-271-710-716-571-316-176-502-167-760-167-736-765-156-321-316-263-367-615 7_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 206-267-752-726-267-206-207-267-302-307-706-130-123-132-103-102-136-301-132-301-310-756-675-576-571-301-301 260-672-672-627-260-672-576-203-236-103-102-102-506-507-671-576-564-5_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 201-106-261-261-170-176-107-672-627-627-267-312-231-231-312-206-302-123-312-132-123 124-201-231-132 123-312-123-301_V.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 316-136-136-137-107-316-376-173-317-361-137-165-316-136-103-761-176-675-162-362-136-760-103-132-312-130-265-167-275-162-627-316-601-103-103-576-160-167-765-263-103-130-132-312-1_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 302-231-627-267-236-276-103-175-571-174-103-107-761-716-716-675-657-156-165-317-372-321-721-673-162-236-326-302-103-123-760-627-106-501-510-103-152-206-275-267-150-751-750-750-1_W.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 132-541-547-546-517-576-574-136-213-312 760-516-576-576-657 526-135-154-135-132-135-132-123-132-103-176-176-136-613-571-157-657-715-137-163-137-165-157-157-157-517-157-675-263_W.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 572-562-265-562-625-675-625-761-675-576-576-752-365-352-612-176-163-162-631-621-615-625-652-576-736-136-316-316-136-567-576-365-312-321-312-216-125-261-312-312-312-102-310-316-1_V.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 136-306-136-103-102-160-316-163-103-361-362-302-672-627-760-627-601-320-163-103-130-201-320-203-231-725-320-103-301-326-302-132-316-136-103-103-103-701-136-103-316-705-705-736-3_W.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 106-106-163-132-103-103-175-756-706-671-267-263-316-106-126-623-362-326-302-302-310-301-130-301-306-301-302-573-753-756-750-672-602-627-623-365-362-352-310-301-312-325-754-754-7_V.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 531-106-312-106-102-613-652-574-570-103-301-136-132-213-312-623-106-152-501-350-305-350-103-316-163-136-163-617-106-756-756-576-752-547-547-574-574-576-547-526-620-576-576-574-5_W.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 675-672-627-361-312-136-132-625-265-312-312-301-301-132-103-106 162-162-321-602-765-765-760-326-362-163-361-132-136-132-167-317-657-657-607-276-276 675-675-627-267-263-726-726-3_V.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 675-610-675-206-201-201-126-320-326-263-623-162-620-620-260-263-206-302-231-236-230-261-216-302-320-607-162-612-162-162-126-106-162-321-260-236-126-123-162-167-321-106-301-530-5_V.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 103-130-301-160-106-130-120-126-610-102-620-206-201-201-631-263-106-217-716-120-135-106-160-130-620-230-312-236-132-132-765-173-176-176-160-301-316-120-260-263-236-263-213-106-1_V.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 267-230-251-321-532 137-107-106-231-352-175-137-106-103-326-301-132-102-130-103-106-103-106-103-176-163-162-156-165-102-130-671-163-162-310-301-231-203-312-302-302-320-201-102-1_V.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 123 102-261-213-617-513-126-102 136-137 756-576-576-756-756-567-756-576-657-672-765-675-627-672-765-672-627-627-675-627-126 150-670-675-657-567-507-506_W.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 136-132-317-137-371-136-137-507-135-617-627-263-263-267-163-136-362-703-230 143-173-671-621-132-247-136-162-136-132-103-312-136-312-136-132-362-326-267-263-236-103-123-132-312-1_V.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 236-321-312-321-312-312-125-123-231-132-125-125-213-235-327-352-326-325-235-237-326-230-203-312-312-320-230-230-320-302-531-531-351-135-316-136-137-317-312-231-365-371-763-731-6_W.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 627-627-620-276-627-263-627-276-612-627-612-632-260-620-620-621-620-670-627-625-675-675-657-576-357-652-672-657-567-576-576-675-765-627-675-657-627-675-675-657-365-673-762-675-7_W.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 562-726-621-126-136-103-163-132-103-105-316-612-236-526-267-256-103-107-167-167-136-613-103-125_V.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 106-761-106-167-136-103-136-103-132-163-103-103-615-103-103-761-361-175-567-261-562-163-103-136-132-160-671-103-106-610-136-130-136-615-165-103-675-163-130-126-103-176-671-160-2_V.mp3)
  Segment 36: 55160ms ~ 56456ms (file: 163-102-126-157-175-162-127-512-176-106-761-167-162-105-125-105-156-570-103-760-576-176 657-615-156-126-367-127-167-136-721-136-136-603-617-176-167-136-167-627-103-567-176-103-6_V.mp3)
  Segment 37: 57456ms ~ 58032ms (file: 136-102-150-571-263-362-203-721-206-623-276-123-120-103-571-213-756-765-175-137-137-607-137-751-106-162-167-570-106-137-137-153-153-156-615-450-540-506-163-126-162-165-156-165-1_V.mp3)
  Segment 38: 59032ms ~ 59608ms (file: 762-732-732-261-217-216-613-261-132-321-132-216-315-256-265-672-206-136-163-127-106-132-765-765-761_V.mp3)
  Segment 39: 60608ms ~ 61184ms (file: 321-216-326-106-312-126-132-126-302-312-263-316-326-103-603-162-120-526-123-136-162-126-230-320-263-265-102-263-321-263-263-306-310-236-306-105-136-315-316-162-621-267-613-236-1_V.mp3)
  Segment 40: 62184ms ~ 62760ms (file: 517-132-162-103-143-135-136-572-320-163-621-267-627-675-603-726 620-712-176-617-176-653-231-167-612-617-103-135-103-165-106-132-760 567-127-236-715-132-136-163-103-123-217-176-1_V.mp3)
  Segment 41: 63760ms ~ 64336ms (file: 617-167-513-153-351-315-576-567-576-576-567-103-103-103-576-576-675-567-756-576-567-657-576-567-657-576-765-756-657-576-576 567-756-765-567-576-761-675-576-163-167-671-367-317-1_W.mp3)
  Segment 42: 65336ms ~ 65912ms (file: 216-265-265-625-257-265-632-261-162-672-265-126-625-632-613-625-675-625-256-635-275-657-675-625-635-637-625-652-627-675-627-657-652-256-627-625-231-132-621-321-312-132-306-302-2_W.mp3)
  Segment 43: 66912ms ~ 67488ms (file: 302-123-216-267-170-321-162-132-156-752-230-210-231-123-632-623-306-670-623-672-132-310-167-102-617-326-312-130-672-160-351-675-312-312-657_W.mp3)
  Segment 44: 68488ms ~ 69064ms (file: 675-675-675-675-657-576-617-657-625-672-675-267-675-675-672-671-675-657-673-576-671-625-675-637-631-675-106-675-653-675-675-673-617-613-765-765-765-675-675-760-760-615-617-316-6_W.mp3)
  Segment 45: 70064ms ~ 70640ms (file: 156-156-516-576-315-135-163-136-315-136-132-135-136-156-156-167-136-135-156-176-135-136-135-153-136-163-516-156-165-132-135-315-153-156-315-165-156-175-157-157-157-157-156-152-1_W.mp3)
  Segment 46: 71640ms ~ 72216ms (file: 613-312-134-124-624-127-132-213-673-617-167-627-621-621-261-602-162-612-617-671-637-167 765-176-735-357-375-317-321-132-607-602-627-621-602 263-324-761-706-716-763-327-172-137-6_V.mp3)
  Segment 47: 73216ms ~ 73792ms (file: 302-753-302-372-316-172-130-751-302-312-132-361-357-351-361-371-312-301-231-321-231-301-107-123-175-132-752-267-576-276-207-207-267-206-310-517-132-102-106-103-571-173-671-143-3_W.mp3)
  Segment 48: 74792ms ~ 75368ms (file: 231-621-762-627-621-263-610-612-621-126-136-316-371-732-627-132-627-623-612-263-231-261-321-361-312-132-312-321-312-362-637-136-621-612-263-621-623-326-316-623-162-632-136-312-6_W.mp3)
  Segment 49: 76368ms ~ 76944ms (file: 106-132-132-130-103-102-167-167-617-162-167-106-106-136 316-317-310-136-132-312-547-574-567-576-576-576-567-567-571-756-765-675-576-756-753-756-576-756-765-576-375-275-357-307-3_W.mp3)
  Segment 50: 77944ms ~ 78520ms (file: 623-326-206-620-263-670-602-620-623-631-261-213-132-302-362-306-310-361-632-310-103-103-103-136-731-137-321-320-263-367-216-326-362-620-536-310-301-306-320-362-316-132-136-316-1_V.mp3)
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
> **Time**: 2026-04-03 05:33:29
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "오늘의 명언을 들려드릴게요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "오늘의", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "명언을", "start_ms": 200, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "들려드릴게요", "start_ms": 390, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "인생은 짧고 예술은 길다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "인생은", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "짧고", "start_ms": 165, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "예술은", "start_ms": 275, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "길다", "start_ms": 435, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 4,
"transcript": "Life is short, and art is long.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "Life", "start_ms": 45, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 205, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "short,", "start_ms": 295, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "and", "start_ms": 425, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "art", "start_ms": 515, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 625, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "long.", "start_ms": 715, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "히포크라테스의 명언입니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "히포크라테스의", "start_ms": 30, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "명언입니다", "start_ms": 290, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "시간을 소중히 아껴서 사용하세요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "시간을", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "소중히", "start_ms": 180, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "아껴서", "start_ms": 340, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "사용하세요", "start_ms": 490, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "Use your time wisely and cherish it.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Use", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "your", "start_ms": 150, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "time", "start_ms": 240, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "wisely", "start_ms": 370, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "and", "start_ms": 530, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "cherish", "start_ms": 620, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "it.", "start_ms": 780, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 9,
"transcript": "오늘이 당신의 남은 인생의 첫 번째 날입니다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "오늘이", "start_ms": 60, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "당신의", "start_ms": 190, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "남은", "start_ms": 300, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "인생의", "start_ms": 390, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "첫", "start_ms": 550, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "번째", "start_ms": 610, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "날입니다", "start_ms": 720, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "Today is the first day of the rest of your life.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Today", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 210, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 300, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "first", "start_ms": 360, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "day", "start_ms": 490, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "of", "start_ms": 600, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 690, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "rest", "start_ms": 750, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "of", "start_ms": 880, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "your", "start_ms": 970, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "life.", "start_ms": 1060, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "자신을 믿고 오늘 하루도 힘차게 시작해 보세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "자신을", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "믿고", "start_ms": 170, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 280, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "하루도", "start_ms": 390, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "힘차게", "start_ms": 540, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "시작해", "start_ms": 700, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "보세요", "start_ms": 830, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "Trust yourself and start today vigorously.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Trust", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "yourself", "start_ms": 180, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "and", "start_ms": 390, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "start", "start_ms": 480, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "today", "start_ms": 590, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "vigorously.", "start_ms": 720, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 14,
"transcript": "포기하지 않는 마음이 꿈을 현실로 만듭니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "포기하지", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "않는", "start_ms": 230, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "마음이", "start_ms": 340, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "꿈을", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "현실로", "start_ms": 580, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "만듭니다", "start_ms": 730, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "A heart that never gives up makes dreams a reality.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "A", "start_ms": 55, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "heart", "start_ms": 115, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "that", "start_ms": 225, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "never", "start_ms": 315, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "gives", "start_ms": 445, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "up", "start_ms": 555, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "makes", "start_ms": 645, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "dreams", "start_ms": 755, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 885, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "reality.", "start_ms": 945, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "오늘의 명언이었습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "오늘의", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "명언이었습니다", "start_ms": 190, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 18,
"transcript": "감사합니다. 내일 또 만나요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "감사합니다.", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "내일", "start_ms": 250, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 380, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "만나요.", "start_ms": 470, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "Thank you. See you again tomorrow.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Thank", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "you.", "start_ms": 180, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "See", "start_ms": 270, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 380, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "again", "start_ms": 470, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "tomorrow.", "start_ms": 600, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 21,
"transcript": "안녕히 가세요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 30, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 220, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "Goodbye.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "Goodbye.", "start_ms": 35, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "(instrument)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 24,
"transcript": "행복한 하루 되세요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "행복한", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "하루", "start_ms": 190, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "되세요", "start_ms": 320, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "Have a happy day.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Have", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 150, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "happy", "start_ms": 210, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "day.", "start_ms": 340, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 27,
"transcript": "다음에 또 뵙겠습니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 35, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 195, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "뵙겠습니다", "start_ms": 305, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "See you again next time.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 150, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "again", "start_ms": 240, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "next", "start_ms": 370, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "time.", "start_ms": 480, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "고맙습니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "고맙습니다", "start_ms": 30, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "Thank you.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "Thank", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "you.", "start_ms": 165, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 32,
"transcript": "언제나 여러분의 곁에 있겠습니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "언제나", "start_ms": 60, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "여러분의", "start_ms": 220, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "곁에", "start_ms": 410, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있겠습니다", "start_ms": 520, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "We will always be by your side.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "We", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "will", "start_ms": 140, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "always", "start_ms": 230, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "be", "start_ms": 390, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "by", "start_ms": 480, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "your", "start_ms": 570, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "side.", "start_ms": 660, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 35,
"transcript": "행복하세요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "행복하세요", "start_ms": 35, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "Be happy.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Be", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "happy.", "start_ms": 120, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 38,
"transcript": "좋은 꿈 꾸세요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 35, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "꿈", "start_ms": 145, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "꾸세요", "start_ms": 235, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "Sweet dreams.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Sweet", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "dreams.", "start_ms": 170, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "평안한 밤 되시기 바랍니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "평안한", "start_ms": 45, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "밤", "start_ms": 205, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "되시기", "start_ms": 295, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "바랍니다", "start_ms": 455, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "Have a peaceful night.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Have", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 160, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "peaceful", "start_ms": 220, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "night.", "start_ms": 410, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "내일 아침에 다시 만나요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "내일", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "아침에", "start_ms": 170, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 320, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 430, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "See you again tomorrow morning.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "again", "start_ms": 250, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "tomorrow", "start_ms": 380, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "morning.", "start_ms": 540, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 45,
"transcript": "잘 자요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "잘", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "자요", "start_ms": 120, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "Sleep tight.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "Sleep", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "tight.", "start_ms": 165, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 48,
"transcript": "사랑합니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "사랑합니다", "start_ms": 30, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "I love you.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 35, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "love", "start_ms": 95, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "you.", "start_ms": 205, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 05:33:29*
