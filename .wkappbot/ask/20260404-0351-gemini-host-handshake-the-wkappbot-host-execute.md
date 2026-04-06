# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=30288  time=03:49:58  uptime=9042s  mem=705MB  system_processes=366"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260404_034917.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 576-756-750-576-357-315-567-135-135-572-570-572-763-637-726-673-627-675-267-267-675-567-657-567-652-625-625-265-625-620-620-625-265-620-260-265-573-532-627-624-267-267-260-267-2_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 321-302-306-321-320-310-231-206-547-546-627-627-236-230-253-326-301-310-321-123-136-106-516-265-236-236-203-230-263-206-316-130-236-542-526-203-320-302-305-573-576-357-253-327-3_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 302-302-321-312-103-312-236-236-621-267-620-263-602-136-162-206-216-261-123-130-317-136-317-130-310-301-601-620-630-623-163-103-631-632-603-603-632-260-312-163-103-106-106-102-1_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 652-312-105-231-312-130-312-301-130-106-103-362-360-230-362-625-260-263-302-302-130-103-130-613-613-623-603-236-620-236-326-620-206-620-620-321-136-103-136-103-102-126-136-301-3_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 361-136-631-361-361-316-136-613-361-361-631-316 672-736-361-351-306-561-635-162-653-163-361-361-316-631-613-163-637-632-326-623-632-361-623-632-632_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 103-106-160-567-167-130-130-670-127-103-103-160-173-321-163-610-103-136-165-163-163-123-630-136-310-167-176-126-126-321-361_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 567-756-576-567-567-657-576-562-567-652-576-567-675-576-765-765-756-657-760-657-675-627-627_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 176-132-173-135-627-367-307-203-237-257-235-302-301-301-302-320-230-327-326-263-326-132-263-132-132-217-215-126-135-132-132-103-132-321-301-103-123-132-327-312-672-207-312-132-1_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 302-163-613-163-361-631-361-745-736-375-765-760_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 675-657-136-650-675-540 235-635-362-627-632-631-612-623-316-361-312-362-261-163-326-136-316 130-316-612-260-316-756-756-627-762-671-367-671-671-617-623-627-612-320-167-672-765-7_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 103-163-617-163-675-613-176-162-106-357-175-607-167-651-607-621-621-362-260_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 316-310-136-231-306-136-103-130-310-130-103-630-325-132-162-103-210-301-230-230-206-320-132-102-103-103-571-310-136-103 103-106-130-130-320-321-320-320-230-320-302-321-302-130-1_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 167-103-106-236-726-216-103-132-132-167-167-137-716-267-263-672-167-156-156-132-167-162-162-102-160-126-160-160-160-165-165-165-561-567-657-576-567-567-652-576-567-567_V.mp3)
  Segment 14: 20488ms ~ 32800ms (file: 207-163-130-261-615-135-162-162-631-163-567-576-576-657-576-725-126-267-236-160-132-267-267-206-230-572-576-526-230-201-102-267-263-267-263-267-276-236-260-210-620 261-267-267-6_W.mp3)
  Segment 15: 33800ms ~ 34376ms (file: 623-362-623-623-627-627-627-623-236-263-236-263-621-263-623-623-327-216-326-236-213-276-627-627-673-613-627-627-627-627-261-261-612-627-627-263-263-265-231-216-362-326-267-263-6_W.mp3)
  Segment 16: 35376ms ~ 35952ms (file: 567-157-167-163-136-136-173-136-167-716-657-126-320-136-316-230-132-617-675-132-361-361-317-761-765-756-165-531-257-275-372-710-715-710-716-716-750-170-170-371-613-176-172-127-1_W.mp3)
  Segment 17: 36952ms ~ 37528ms (file: 365-325-352-653-563-365-352-352-356-536-563_W.mp3)
  Segment 18: 38528ms ~ 43532ms (file: 216-137-627-627-130-317-320-713-376-317-312-317-371-317-371-102-316-136-176-213-132-321-216-102-725-216-673-675-102-267-231-130-613-672-137-120-216-756-507-573-215-756-357-356-5_W.mp3)
  Segment 19: 44532ms ~ 45108ms (file: 514-251-257-256-265-251-251-132-312-325-315-315-351-135-316-532-652-652-652-546-547-543-514-657-657-675-265-625-625-513-517-514-514-574-527-126-123-135-132-123-132-132-312-251-1_W.mp3)
  Segment 20: 46108ms ~ 46684ms (file: 627-176-167-761-176-167-106-167-167-136-201-231-230-236-302-260-206-765-276-260-173-103-170-173-146-736-762-206-562-652 762-672-126-320-251-321-672-762-267-726-672-627-261-612-3_W.mp3)
  Segment 21: 47684ms ~ 48260ms (file: 153-167-671-167-173-163-126-267-657-673-617-673-637-526-156-635-365-361-165-176-715-537-573-157-531-153-357-537-571-671-617-157-537-573-517-537-573-352-537-574-574-573-537-350-7_W.mp3)
  Segment 22: 49260ms ~ 49836ms (file: 231-231-302-302-231-230-132-302-103-301 546-562-150-150-213-235-574-576-576-756-532-267-201-213-231-102-312-320-130-310-130-103-132-136-162-361-163-136-312-216-206-106-102-103-1_V.mp3)
  Segment 23: 50836ms ~ 51412ms (file: 672 617-613-671-617-361-612-671-106-670-671-617-672-602-137-760-132-321-362-136-765-762-627-726-263-265-625-261-236-162 752-576-715-671-617-675-657-756-765-657-765-756-675-765-6_W.mp3)
  Segment 24: 52412ms ~ 52988ms (file: 176-316-136-102-132-312 362 103-162-136-261-216-765-162-162-130-136-236-213-361-216-103-160-162-102-102-216-267-176-652-123-361-132-261-162-627-762-127-217-167-162-621-321-576-5_W.mp3)
  Segment 25: 53988ms ~ 54564ms (file: 136-105-216-165-620-613-132-671-761-163-103-126-103-130-576-165-601-160-106-615-671-167-617-103-103-132-103-165-176-316-103-165-612-315-136-361-316-631-613-136-103-136-157-136-3_V.mp3)
  Segment 26: 55564ms ~ 56140ms (file: 362-326-326 261-103-675-163-562-256-127-615-162-162-231-312-316-361-120-203-236-132-126-136-526-317-672-627-623-267-162-165-136-162-136-163-163-163-671-216-621-162 103-132-316-1_W.mp3)
  Segment 27: 57140ms ~ 57716ms (file: 754-107-106-153-106-103-103-163-316-623-103-123-136-107-163-130-651-106-103-106-103-103-106-163-107-103-362-167-106-376-103-130-765-617-130-103-136-163-160-136-163-612-176-126-1_V.mp3)
  Segment 28: 58716ms ~ 59292ms (file: 612-763-762-672-762-673-765-176 130-102-213-210-120-231-123-213-210-120-210-120-213-231-216-321-132-231-321-132-321-312-230-312-231-213-231-132-123-231-321-230-321-231-132-231-3_V.mp3)
  Segment 29: 60292ms ~ 60868ms (file: 507-621-176-167-167-657-102-136-102-675-263-132-320-126-217-137-106-536-132-132-102-132-130-562-106-160-103-165-130-615-521-136-716-621-103-157-536-103-103-156-617-163-103-136-1_V.mp3)
  Segment 30: 61868ms ~ 62444ms (file: 162-162-216-160-123-162-132 765-760-761-672-602-106-136-130-613-617-617-167-610-160-160-160-126-102-106-263-621-162-165-621-623-672-623-263-621-632-672-267-672-160-153-106-356-6_W.mp3)
  Segment 31: 63444ms ~ 64020ms (file: 537-526-572-365-325-312-316-632-613-601-103-362-165-632-613-265-326-165-136-312-326-627-362-367-326-136-625-630-160-503-136-672-627-623-675-632-637-763-672-621-137-132-376-312-1_W.mp3)
  Segment 32: 65020ms ~ 65596ms (file: 627-623-320-203-302-301-613-310-302-203-230-320-302-326-163-103-136-106-360-120-106 576-756-576-657-576-567-756-576-576-715-675-670-620-630-576-574 136 576 165-570-507-560-546 5_W.mp3)
  Segment 33: 66596ms ~ 73364ms (file: 132-263-236-261-136-123-623-263-231-102-613-162-263-312-310-236-263-621-167-213-321-263-231-326-132-120-103-102-236-372-327-231-327-213-210-236-326-326-261-371-132-321-132-312-2_W.mp3)
  Segment 34: 74364ms ~ 74940ms (file: 326-267-263-132-257-265-126-321-321-621-160-326-612-123-120-216-316-672-672-672-623-625-627-625-625-621 163-162_W.mp3)
  Segment 35: 75940ms ~ 76516ms (file: 167-236-316-261-326-765-762-762-627-726-316-273-123 621-265-267-267-267-263-120-263-263-267-263-136-261-312-236-316-216-263-236-261-621-103-671-106-167-526 163-165-657-136-103-1_W.mp3)
  Segment 36: 77516ms ~ 78092ms (file: 567-526-576-572-256-256-136-715-631-621-623-673-627-637-623-627-672-716-762-267-267-637-756-671-615-675-615-615-156-152-562-256-237-236-256-627-657-657-675-567-657-567-756-736-6_W.mp3)
  Segment 37: 79092ms ~ 79668ms (file: 156-132-316-136-136-312-136-216-213-126-132-312-132-316-132-132-132-312-123-213-132-263-613-162-162-165-135-517-215-167-127-213-162-126-123-216-261-216-126-216-263-263-513-516-5_W.mp3)
  Segment 38: 80668ms ~ 81244ms (file: 167-130-361-621-601-126-103-631-163-316-167-106-106-103-106-103-130-136-613-361-106-103-163-136-265-267-267-627-261-652-627-635-637-762-136-547-150-103-130-162-163-136-571-103-1_V.mp3)
  Segment 39: 82244ms ~ 82820ms (file: 501-136-130-617-130-132-103-316-160-132-167-612-163-162-637-156-156-163-103-130-316-316-615-162-136-231-130-125-132-567-162-136-137-756-103-156-756-167-613-136-136-103-103-136-1_V.mp3)
  Segment 40: 83820ms ~ 84396ms (file: 263-231-351-365-325-236-326-361-613-236-263-213-612-513-325-253-231-326-132-216-326-326-653-351-156-156-156-315-536-267-623-621-362-623-612-632-623-362-236-263-261-516-162-135-1_W.mp3)
  Segment 41: 85396ms ~ 96016ms (file: 231-102-130-106-167-102-103-261-130-230-230-315-230-312-107-321-236-261-267-326-321-310-312-362-326-376-327-130-301-263-236-267-362-267-236-326-130-236-263-260-725-162-321-312-6_W.mp3)
  Segment 42: 97016ms ~ 97592ms (file: 302-312-321-206-206-216-320 627-621-163-613-301-623-107-137-312-321-574-105-103-103-673-321-756-765-657-715-623-621-652-521-675-615-316-673-715 361 625-263-162-312-103-132-126-1_W.mp3)
  Segment 43: 98592ms ~ 99168ms (file: 123-326-627-623-657-756-576-756-572-751 231-163-106-136-130-216-103-135-137-617-165-627-263 321-312 103-612-106-160-162-126-136-132-163-136-361-157-136-130-162-103-136-613-361-1_V.mp3)
  Segment 44: 100168ms ~ 100744ms (file: 623-602-236-321-613-172-167-172-617-167-623-601-263-123-167-123-162-236-623-632-276-617-261-627-623-236 103 172-156-162_W.mp3)
  Segment 45: 101744ms ~ 102320ms (file: 213-612 672 362 163-610-165-312-312-123-316-612-362-321-312-135-102-621-163-137-326-132-236-321-312-132-612-126-123-132-312-132-135-132-123-132-263-126-261-136-105-617-160-365-6_V.mp3)
  Segment 46: 103320ms ~ 103896ms (file: 562-563-263-267-320-532-307-375-574-574-705-301-310-301-312-132-103-312-531-601-150-157-173-106-516-157-176-530-132-302-301-316-301-321-301-312-301-302-260-206-206-236-623-621-2_W.mp3)
  Segment 47: 104896ms ~ 105472ms (file: 163-617-301-613-163-162-136-163-136-103-130-231-321-302-136-103-706-715-103-102-123-106-762-672-216-126 165-607-607-632-645-750-765-736-620-637-160-167-675-673-106-167-132-124-6_V.mp3)
  Segment 48: 106472ms ~ 107048ms (file: 612-126-162-216-216-126-213-621-261-621-627-261-621-261-261-261-621-261-261-621-126_V.mp3)
  Segment 49: 108048ms ~ 108624ms (file: 365-325-635-623-263-236-325-365-365-350-325-563-256-156-526-520-265-256-265-356-357-574-316-365-365-357-356-573-357-357-356-315-153-136-135-613-326-632-362-623-362-165-105-361-3_W.mp3)
  Segment 50: 109624ms ~ 110200ms (file: 721-762-726-712-716-126-673-576-675-637-617-267-621-162-176-136-136-123-123-132-216-213-135-213-236 637-671-627-231-321-310-302 571-751-137-175 250-672-156-136_W.mp3)
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
> **Time**: 2026-04-04 03:51:26
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "여러분들을 위해 준비한 시원한 커피 쿠폰 이벤트까지 준비했으니까",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "여러분들을", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "위해", "start_ms": 175, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "준비한", "start_ms": 240, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "시원한", "start_ms": 325, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "커피", "start_ms": 390, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "쿠폰", "start_ms": 435, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "이벤트까지", "start_ms": 480, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "준비했으니까", "start_ms": 545, "dur_ms": 31, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "다음 에피소드를 기대해 주세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "다음", "start_ms": 60, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "에피소드를", "start_ms": 165, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "기대해", "start_ms": 320, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 445, "dur_ms": 131, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "오늘의 표현 길이 엇갈리다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "오늘의", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "표현", "start_ms": 165, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "길이", "start_ms": 270, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "엇갈리다", "start_ms": 375, "dur_ms": 201, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "Miss each other. 여기서 미스",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 35,
"audio_type": "mixed",
"words": [
{"word": "Miss", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": 1.5},
{"word": "each", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": 1.5},
{"word": "other.", "start_ms": 245, "dur_ms": 100, "phase_lr_deg": 1.5},
{"word": "여기서", "start_ms": 350, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "미스", "start_ms": 475, "dur_ms": 101, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "로 바꾸는 단계에 들어섰다는 점입니다",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "로", "start_ms": 50, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "바꾸는", "start_ms": 95, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "단계에", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "들어섰다는", "start_ms": 285, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "점입니다", "start_ms": 440, "dur_ms": 136, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "빠른 생산과 현실적인 가격 그리고 꾸준한 기술 축적과 수출 전략까",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "빠른", "start_ms": 45, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "생산과", "start_ms": 110, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "현실적인", "start_ms": 215, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "가격", "start_ms": 340, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 405, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "꾸준한", "start_ms": 470, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "기술", "start_ms": 535, "dur_ms": 41, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 8,
"transcript": "불을 돌리면서 튀기면 그 누룽지 사이로 기름이 들어가면서 빨리 튀겨지기 때문에 불을 돌리고 있습니다",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "mixed",
"words": [
{"word": "불을", "start_ms": 70, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "돌리면서", "start_ms": 135, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "튀기면", "start_ms": 220, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 305, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "누룽지", "start_ms": 330, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "사이로", "start_ms": 435, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "기름이", "start_ms": 520, "dur_ms": 56, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "누룽지가 바삭",
"speaker": "speaker_4",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "mixed",
"words": [
{"word": "누룽지가", "start_ms": 40, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "바삭", "start_ms": 300, "dur_ms": 236, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "부분 겁나서 못 들어갑니다 근데 중요한 건 눌림이 아니라 지지가 나오느냐입니다",
"speaker": "speaker_5",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "부분", "start_ms": 55, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "겁나서", "start_ms": 120, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "못", "start_ms": 205, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "들어갑니다", "start_ms": 250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 355, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "중요한", "start_ms": 420, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "건", "start_ms": 505, "dur_ms": 71, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "엘리스 넥스트는 3월 초에 정말 강하게 상승하면서",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "엘리스", "start_ms": 45, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "넥스트는", "start_ms": 170, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "3월", "start_ms": 275, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "초에", "start_ms": 340, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 405, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "강하게", "start_ms": 470, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "상승하면서", "start_ms": 535, "dur_ms": 41, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 13,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 14,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 15,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 16,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 17,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 18,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 19,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 20,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 21,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 22,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 23,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 24,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 25,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 26,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 27,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 28,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 29,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 30,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 31,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 32,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 33,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 34,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 35,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 36,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 37,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 38,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 39,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 40,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 41,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 42,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 43,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 44,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 45,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 46,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 47,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 48,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 49,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 50,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-04 03:51:26*
