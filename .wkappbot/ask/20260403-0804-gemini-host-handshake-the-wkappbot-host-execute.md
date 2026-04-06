# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=08:03:18  uptime=24244s  mem=1532MB  system_processes=447"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_080309.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 617-176-163-326-132-134-142-657-651-654-617-167-571-127-167-267-736-327-756-576 361-623-673-216-123-326-570-315-526-602-312-132-310-103-623-576-156-675-167-627-672-316-306-635-6_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 165-162-576-576-756-671-672-123-136-103-132-104-617-607-602-160-201-206-206-320-312-132-136-312-132-571-106-106-312-301-765-726-276-230-260-130-102-103-130-673-671-162-163-136-1_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 102-261-312-617-631-132-657-136-103-136-175-132 163-601-765-172-762-207-217-237-621-612-162-620-132-103-316-103-160-136-176-136-152-130-361-132-610-163-615-163-673-136-136-310-1_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 103-321 261-130-176-617-672-760-672-267-321-361-617-103-361-103-162-316-301-607-206-321-132-160-162-103-136-316-130-672-612-126-132-136-132-316-310-130-136-132-132-630-163-163-1_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 123-617-123-621-103-317-317-516-576_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 136-620-612-263-273-237-231-136-301-103-106-130-123-320-326-302-607-312-320-320-320-321-302-301-103-167-201-231-260-526-326-621-623-621-564-576-657-132-615-163-612-750-756-760-2_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 376-367-376-376-756-537-567-765-276-276-267-672-267-627-672-632-267-627-672-672-263-627-672-670-672-652-567-567-356-675-675-567-576-567-365-612-256-316-361-631-514-516-567-357-5_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 267-267-267-267-260-263-236-265-265-526-256-235-257-257-567-567-534-547-534-514-547-543-547-325-231-315-135-312-351-356-325-356-365-351-315-365-356-325-315-315-365_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 103-106-174-103-106-165-156-617-316-157-206-165-610-163-163-160-176-162-103-132-103-103-137-103-106-106-154-326-162-103-163-102-103-715-176-601-163-165-125-172-162-132-103-657-7_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 316-132-301 567-567-564-564-576-576-576-576-567-567-567-567-706-167-163-267-263-123-306-163-301-310-132-312-301-136-612-261-216-126-123-312-716-176-173-671-136-610-362-216-103-7_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 132-103-136-163-107-167-167-316-103-172-657-567-571-574-361-301-206-260-260-632-260-261-102-213-216-310-136-316-130-103-136-103-310-103-312-126-532-576-576-205-650-652-135-106-1_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 361-123-132-162-610-106-126-106-126-301-103-312-265-621-627-570-756-752-765-627-632-621-236-613-312-316-126-321-236-324-106-613-153-132-675-607-103-103-162-170-167-607-163-162-3_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 126-163-136-316-627-675-162-675-675-672-627_W.mp3)
  Segment 14: 20488ms ~ 29092ms (file: 167-106-106-103-163-136-153-136-165-621-126-612-163-106-756-176-167-136-610-603-612-106-106-206-502-103-162-136-107-124-132-316-651-107-160-127-126-162-301-302-206-326-301-136-6_V.mp3)
  Segment 15: 30092ms ~ 30668ms (file: 162-132-213-231-123-126-132-321-126-123-312-132-132-321-126-123-123-163_V.mp3)
  Segment 16: 31668ms ~ 32244ms (file: 312-756-672-156-621-160-621-162-261-621-623-620-672-130-260-206-120-206-123-102-163-162-167-613-206-206-206-201-627-620-123-301-132-263-206-706-612-756-620-632-601 267-276-160-3_V.mp3)
  Segment 17: 33244ms ~ 33820ms (file: 132-567-231-275-273-273-162-765-237-203-321-132-320-321-576-567-756-321-103-301-103-316-165-637-136-160-130-310-310-301-310-316-136-163-106-102-623-260-610-162-103-136-312-136-1_W.mp3)
  Segment 18: 34820ms ~ 35396ms (file: 650-651-671-126-163-167-163-167-130-136-103-163-137-320-302-103-126-312-613-671-716-176-103-201-120-162-123-126-102-206-321-123-201-120-123-302-317-162-503-126-160-306-671-607-1_V.mp3)
  Segment 19: 36396ms ~ 36972ms (file: 620-762-162-312-103-263-163-361-136-167-165-260-217-265-162-316-132-162-103-106-176-126-136-612-163-165-132-103-136-163-130-163-561-106-612-617-620-126-632-621-126-126-167-106-1_V.mp3)
  Segment 20: 37972ms ~ 38548ms (file: 237-321-231-123-126-576-576-617-706-143-132-103-123-136-627-612-576-571-713-173-132-321-754-751 236 132-103-132-312-103-130-312-302-713-701-107-701-701 106-716-136-617-162-675-1_V.mp3)
  Segment 21: 39548ms ~ 40124ms (file: 672-162-612-163-162-136-316-136-123-126-612-162-176-672 165-516-165-671-167-671-160-126-132-132-136-163-136 675-123-671 312_W.mp3)
  Segment 22: 41124ms ~ 41700ms (file: 736-612-103-132-163-612-612-216-623-621-261-123-126-132-123-216-123-265-126 130-132-132-123-137-732-172-261-631-132-123-736-123-670-163-167-630-601-631-136-103-136-103-627-106-1_V.mp3)
  Segment 23: 42700ms ~ 43276ms (file: 526-256-625-365-632-175-367-136-153-531-351-325-123-235-123-523-537-537-527-537-652-563-356-562-325-531-357-536-536-532-657-567-567-562-356-635-567-357-357-315-371-375-753-375-3_W.mp3)
  Segment 24: 44276ms ~ 44852ms (file: 132-136-261-621-261-103-162-163-162-126-621-320-236-231-130-136-301-312-530-573-503-315-312-756-631-670-160-613-236-263-260-260-265-265-206-263-623-623-625-267-267-562-567-160-3_V.mp3)
  Segment 25: 45852ms ~ 46428ms (file: 203-230-102-612-236-210-216-103-231-526-276-263-203-260-627-206-263-260-236-765-672-270-260-213-126-326-316-301-750-176-362-276-627-132-267-627-627-621-627-627-623-267-216-103-1_V.mp3)
  Segment 26: 47428ms ~ 48004ms (file: 675-672-675-576-576-576-574-574-576-576 571-567-675-576_W.mp3)
  Segment 27: 49004ms ~ 49580ms (file: 102-130-213-103-230-236-276-271-102-176-102-623-316-612-725-137-703-725-167-130-310-126-260-267-126-362-103-106-136-716-106-106-206-206-520-570-135-102-321-610-130-102-106-163-1_V.mp3)
  Segment 28: 50580ms ~ 51156ms (file: 150-127-132-321-132-362-612-623-501-506-175-167-753-751-567-162-132-173-137-301-547-576-576-574-576-576-574-574-547-576-570-765-756-130-352-203-302-103-321-310-321-312-301-316-1_W.mp3)
  Segment 29: 52156ms ~ 52732ms (file: 263-263-623-632-261-632-276-267-213-321-216-267-162-623-126-672-236-326-136-312-367-623-362-312-316-623-632-623-632-321-312-163-261-672 312-316-362-623-263-326-263-675-765-764 6_W.mp3)
  Segment 30: 53732ms ~ 54308ms (file: 670-501-350-320-263-260-263-263-263 260-320-321-230-326-230-203-310-320-320-654-657-675 571-756-752-632-527-716-123-132-130-132-103-103-132-312-310-360-163-165-120-620-260-162-3_V.mp3)
  Segment 31: 55308ms ~ 55884ms (file: 326-231-321-213-321-326-216-261-271-270-721-237-321-216-231-254-521-257-275-275-231-236-276-236-327-217-127-267-267-236-361-316-231-326-230-231-231-236-320 514-517-357-275-235-3_W.mp3)
  Segment 32: 56884ms ~ 57460ms (file: 163-130-163-160-103-176-137-167-137-176-103-163-167-132-160-103-512-165-162-651-167-103-165-106-615-163-631-136-162-160-103-651-103-130-132-163-136-103-103-103-103-267-263-623-2_V.mp3)
  Segment 33: 58460ms ~ 59036ms (file: 612-367-627-367-623-637-621-362-213-362-367-362-632-632-261-367-361-362-362-623-362-362-632-623-632-263-621-627-621-261-612-613-632-362-632-263-361-613-261-632-631-637-623-326-2_W.mp3)
  Segment 34: 60036ms ~ 60612ms (file: 617-137-570-570-765-672-672-362 547-576-756-756-765-765-675-675-670-605-507-657-637-765 325-315-725-172-103-130-103-137-756-765-670-132 673-675-637-672-206-756-752_W.mp3)
  Segment 35: 61612ms ~ 62188ms (file: 156-157-157-135-135-135-157-135-315-157-157-156-156-156-613-163-176-561-156-157-156-165-157-617-136-316-351-367-756-657-176-675-165-157-176-617-165-615-567-561-165-157-561-157-5_W.mp3)
  Segment 36: 63188ms ~ 63764ms (file: 261-231-126-132-367-623-267-621-236-321-236-263-627-625-627-675-162-617-671-627-601-671-617-617-162-651-627-671-671-671-671-762-361-632-126-126-621-673-670-675-627-672-762-671-6_W.mp3)
  Segment 37: 64764ms ~ 65340ms (file: 102-102-126-106-167-610-621-127-354-627-672-627-672-421-672-612-602-675-634-126-607-316-132-567-573 321 126_V.mp3)
  Segment 38: 66340ms ~ 66916ms (file: 571-765-716-267-103-106-567-236-236-263-236-621-231-761-137-103-136-132-312-507-570-312-310-103 567-564-567-567-715-675-657-576-567-657-657-657-657-625-632-627-675-321-263-567-6_W.mp3)
  Segment 39: 67916ms ~ 68492ms (file: 203-207-236-627-672-672-762-236-362-237-723-267-320-203-532-257-206-250-302-302-326-327-320-326-270-760-276-627-273-532-542-672-207-206-230-203-203-103-136-301-132-302-672-276-7_W.mp3)
  Segment 40: 69492ms ~ 70068ms (file: 612-621-612-160-312-301-302-375-567-637-362-301-102-231-236-260-360-236-205-203-263-123-126-236-216-126-326-703-176-567-103-103-563-302-320-321-321-326-312-310-612-326-235-326-3_W.mp3)
  Segment 41: 71068ms ~ 71644ms (file: 163-106-136-316-162-263-231-621-137-726-762 136-126_W.mp3)
  Segment 42: 72644ms ~ 73220ms (file: 652-657-675-267-260-206-670-360-361-574-547 760-765-675-627-627-627-627-621-607-675-675-672-675-675-765-675-765-657-672-675-765-756-576-670-765-137-673-601-627-612-672-675-670-6_W.mp3)
  Segment 43: 74220ms ~ 74796ms (file: 562-756-375-365-375-376-367-267-267-267-652-675-621-617-617-627-613-617-675-673-617-316-675-765-167-167-751-716-716-137-173-137-167-167-137-713-723-276-271-267-265-257-267-652-2_W.mp3)
  Segment 44: 75796ms ~ 76372ms (file: 261-261-621-261-637-163-623-572-576-657-672-162-621-612-765-761-756-160-106-306-326-372-160-316-607-760-607-675-617-170-206-613-162-106-103-160-106-307-103-612-236-756-672-672-1_W.mp3)
  Segment 45: 77372ms ~ 77948ms (file: 136 175-167 321-103-103-103-136-136-165-103-231-216-231-256-312-136-132-316-136-106-516-136-136-162-263-216-213-102-103-106-106-106-136-105-136-132-103-316-103-103-162-123-126-6_V.mp3)
  Segment 46: 78948ms ~ 79524ms (file: 217-136-651-167-102-632-106-617-617-673-602-602-106-601-672-123-176-167-263-621-621-726-360-612-276-320-127-136-132-617-167-106-136-123-621-165-167-165-610-613-106-612-126-175-1_V.mp3)
  Segment 47: 80524ms ~ 81100ms (file: 765-276-105-165-162-631-236-123-103-103-201-203-321-321-306-301-301-305-301-610-306-263-306-310-301-361-137-371-751-735-723-726-163-631-651-632-673-617-162-671-632-103-163-316-6_V.mp3)
  Segment 48: 82100ms ~ 82676ms (file: 570-574-750 136-160-672-617-657-167-132-610-617-617-163-312-160-567-675-672-163-756-735-375-631-167-163-136-231-264-321-326-312-231 103-672-167-163-615-231-265-103-612-263-236-2_W.mp3)
  Segment 49: 83676ms ~ 84252ms (file: 132-231-326-236-576-765-765-267-132-132-162-103-102-102-130-162-136-162-132-312-317-321-312-321-216-132-326-172-213-231-135-154-501-156-617-165-657-123-136-560-675-671-162-361-3_W.mp3)
  Segment 50: 85252ms ~ 85828ms (file: 126-617-267-267-267 621-612 321-321-213_W.mp3)
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
> **Time**: 2026-04-03 08:04:19
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "오늘 배운 내용을 최종적으로 정리해 보겠습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "배운", "start_ms": 160, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "내용을", "start_ms": 290, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "최종적으로", "start_ms": 440, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "정리해", "start_ms": 670, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "보겠습니다", "start_ms": 830, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "격식 있는 표현과 조사의 활용이 가장 기억에 남아요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "격식", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "있는", "start_ms": 140, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "표현과", "start_ms": 260, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "조사의", "start_ms": 410, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "활용이", "start_ms": 540, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "가장", "start_ms": 680, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "기억에", "start_ms": 800, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "남아요", "start_ms": 940, "dur_ms": 160, "phase_lr_deg": 5.0}
]
},
{
"segment": 3,
"transcript": "실생활에서 자주 써보는 것이 중요합니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "실생활에서", "start_ms": 55, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "자주", "start_ms": 305, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "써보는", "start_ms": 425, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "것이", "start_ms": 585, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "중요합니다", "start_ms": 695, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "매일 조금씩이라도 연습할게요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "매일", "start_ms": 25, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "조금씩이라도", "start_ms": 145, "dur_ms": 280, "phase_lr_deg": 5.0},
{"word": "연습할게요", "start_ms": 435, "dur_ms": 220, "phase_lr_deg": 5.0}
]
},
{
"segment": 5,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 6,
"transcript": "수업 시간에 질문을 많이 해서 좋았습니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "수업", "start_ms": 35, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "시간에", "start_ms": 155, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "질문을", "start_ms": 285, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 425, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "해서", "start_ms": 545, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "좋았습니다", "start_ms": 655, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "모르는 걸 바로 해결하니 속이 시원해요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "모르는", "start_ms": 50, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "걸", "start_ms": 190, "dur_ms": 50, "phase_lr_deg": 5.0},
{"word": "바로", "start_ms": 250, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "해결하니", "start_ms": 370, "dur_ms": 180, "phase_lr_deg": 5.0},
{"word": "속이", "start_ms": 560, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "시원해요", "start_ms": 670, "dur_ms": 190, "phase_lr_deg": 5.0}
]
},
{
"segment": 8,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 9,
"transcript": "오늘 고생 많으셨습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "고생", "start_ms": 160, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "많으셨습니다", "start_ms": 290, "dur_ms": 320, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "그럼 이제 진짜 작별 인사를 할까요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 120, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 220, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "작별", "start_ms": 340, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "인사를", "start_ms": 470, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "할까요", "start_ms": 620, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "네 선생님 안녕히 가세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 20, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "선생님", "start_ms": 90, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "안녕히", "start_ms": 240, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "가세요", "start_ms": 400, "dur_ms": 160, "phase_lr_deg": 5.0}
]
},
{
"segment": 12,
"transcript": "다음에 다시 만날 때까지 건강하세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 60, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 210, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "만날", "start_ms": 330, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "때까지", "start_ms": 460, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "건강하세요", "start_ms": 610, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 14,
"transcript": "안녕히 가세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 35, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 195, "dur_ms": 340, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "모두 잘 가요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "모두", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 165, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "가요", "start_ms": 245, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 17,
"transcript": "행복한 시간 보내길 바라요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "행복한", "start_ms": 30, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "시간", "start_ms": 180, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "보내길", "start_ms": 310, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "바라요", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "감사합니다 선생님도요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 55, "dur_ms": 240, "phase_lr_deg": 5.0},
{"word": "선생님도요", "start_ms": 305, "dur_ms": 220, "phase_lr_deg": 5.0}
]
},
{
"segment": 19,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 20,
"transcript": "조심히 들어가세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "조심히", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "들어가세요", "start_ms": 190, "dur_ms": 360, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 22,
"transcript": "언제든지 또 찾아오세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "언제든지", "start_ms": 25, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 195, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "찾아오세요", "start_ms": 275, "dur_ms": 260, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 24,
"transcript": "기다리고 있을게요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "기다리고", "start_ms": 60, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "있을게요", "start_ms": 250, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "네 다음에 봬요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 35, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "다음에", "start_ms": 105, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "봬요", "start_ms": 245, "dur_ms": 240, "phase_lr_deg": 5.0}
]
},
{
"segment": 26,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 27,
"transcript": "잘 가요 안녕",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "잘", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "가요", "start_ms": 170, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "안녕", "start_ms": 320, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 29,
"transcript": "바이 바이",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "바이", "start_ms": 40, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "바이", "start_ms": 210, "dur_ms": 370, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 31,
"transcript": "진짜 안녕",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 25, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "안녕", "start_ms": 175, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 33,
"transcript": "다음에 봐요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 45, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "봐요", "start_ms": 215, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 35,
"transcript": "안녕히",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 30, "dur_ms": 530, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 37,
"transcript": "작별 인사",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "작별", "start_ms": 60, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "인사", "start_ms": 180, "dur_ms": 340, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 39,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 40,
"transcript": "끝",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "끝", "start_ms": 40, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 42,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 43,
"transcript": "수고",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "수고", "start_ms": 55, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 45,
"transcript": "고생",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "고생", "start_ms": 35, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 47,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 48,
"transcript": "바이",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "바이", "start_ms": 25, "dur_ms": 530, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 50,
"transcript": "안녕",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "안녕", "start_ms": 50, "dur_ms": 510, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 08:04:19*
