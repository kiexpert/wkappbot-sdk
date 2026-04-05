# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=6944  time=06:26:42  uptime=17011s  mem=384MB  system_processes=357"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_062332.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 8748ms (file: 263-260-230-321-275-273-526-236-325-713-321-527-312-237-263-623-132-532-321-213-321-321-213-572-576-256-231-532-312-102-132-574-572-152-236-213-321-352-571-576-752-762-362-765-7_W.mp3)
  Segment 2: 9748ms ~ 10324ms (file: 705-701-751-710-570-750-701-173-570-570-705-750-570-750-705-350-705-576-750-607-307-750-756-753-765-760-375-706-705-376-356-750-705-750-713-173-715-705-750-750-571-750-705-751-5_V.mp3)
  Segment 3: 11324ms ~ 11900ms (file: 627-627 307-371-370-307-173-713-137-130-371-137-175-103-173-302-723-617-367-136-317-137-103-301-310-310-173-301-703-370 317-751-753-703-735-305-371-105-317-175-137-307-610-607-6_V.mp3)
  Segment 4: 12900ms ~ 13476ms (file: 625-625-652-652-562-652-652-562-652-652-652-657-635 265-623-265-621-621-621-621-623-625-625-625-623-260-620-265-265-652-265-657-625-265-625 625-625-652-652-625-657-625-652 625-6_W.mp3)
  Segment 5: 14476ms ~ 28804ms (file: 236-103-675-765-310-132-170-637-271-217-176-170-657-507-160-175-126-107-326-627-625-657-625-210-210-201-321-132-103-316-763-163-310-137-107-156-165-657-657-612-102-632-162-136-1_W.mp3)
  Segment 6: 29804ms ~ 41180ms (file: 210-231-372-732-716-320-326-237-273-756-763-326-273-310-231-130-316-301-301-312-723-276-762-216-162-236-267-103-321-362-132-736-376-260-627-276-276-372-631-273-127-276-276-267-3_W.mp3)
  Segment 7: 42180ms ~ 42756ms (file: 756-756-760-706 675-675-672-610-312-316_W.mp3)
  Segment 8: 43756ms ~ 51100ms (file: 523-157-537-310-230-572-312-271-217-237-213-320-517-715-276-137-762-267-267-756-321-310-312-126-316-231-120-231-213-263-263-230-236-236-671-736-735-716-763-736-276-321-321-267-2_W.mp3)
  Segment 9: 52100ms ~ 52676ms (file: 312-572-235-725-527-562-176-574-271-735-725-761-726-725-627-275-263-612-275-723-567-237-375-257-572-627-276-256-562-251-123-312-263-574-756-357-517-576-536_V.mp3)
  Segment 10: 53676ms ~ 54252ms (file: 501-510-510-510-510-510-501-507-510-571-510-570-150-517-570-570-507-157-157-510-571-576-570 507-150-150-153-513-501-517-510-501-150-510-501-501-501-510-501-510-501-510-571-501-5_V.mp3)
  Segment 11: 55252ms ~ 55828ms (file: 103-103-153-315-315 701-710-175-176-617-107-163-160-567-175-176-176-267-206-627-375-231-310-541-574-760-672-675-765-137-162-260-302-103-162-173-123-103-501-572-573-537-267-620-2_V.mp3)
  Segment 12: 56828ms ~ 57404ms (file: 571-562-675-756-657-562-572-567-567-567-135-573-571-567-657-576-517-574-756-562-572-567-657-357-167-713-517-576-516-176-135-175-375-735-753-576-563-675-531-130-523-536-765-574-7_W.mp3)
  Segment 13: 58404ms ~ 64920ms (file: 627-312-321-123-301-612-260-763-106-260-231-716-613-163-173-736-765-756-605-265-576-576-652-312-316-351-260-163-321-130-213-261-162-573-517-106-321-260-765-321-103-175-317-756-7_W.mp3)
  Segment 14: 65920ms ~ 72436ms (file: 231-102-120-217-312-362-235-325-231-257-270-230-235-241-204-120-213-215-573-231-231-230-217-230-572-250-102-125-251-120-201-102-120-123-320-123-124-201-127-132-102-132-231-273-6_V.mp3)
  Segment 15: 73436ms ~ 81032ms (file: 520-132-103-103-210-102-201-367-756-751-753-752-576-103-615-576-310-132-103-236-320-103-571-230-120-265-213-236-263-260-213-326-624-106-236-123-261-236-120-123-261-162-260-263-2_W.mp3)
  Segment 16: 82032ms ~ 82608ms (file: 762-765-765-736-765-675-276-726-673-762-317-137-167-136-137-136-136-176-756-756-765-756-756-756-756-765-576-756-756-576-756-756-316-316-130-752-765-736-765-765-765-765-752-751-6_W.mp3)
  Segment 17: 83608ms ~ 84184ms (file: 107-105-157-715-107-175-517-710-705-705 105-157-175-157-701-157-157-175-751-157-175-157-157-105_W.mp3)
  Segment 18: 85184ms ~ 85760ms (file: 136-163-163-103-162-136-162-130-106-130-160-137-574_V.mp3)
  Segment 19: 86760ms ~ 87336ms (file: 125-165-165-146-135-136-152-157-357-125-175-154_V.mp3)
  Segment 20: 88336ms ~ 88912ms (file: 623-276-762-106-176-162-751-157-516-152-315 106-617-167-106-176-307-310-370-103-230 576-756-153-573-752-756_W.mp3)
  Segment 21: 89912ms ~ 95312ms (file: 310-132-136-126-162-126-120-126-216-260-231-236-260-302-203-203-362-623-362-236-623-261-326-362-362-362-231-632-162-216-621-126-163-123-163-617-162-163-312-312-321-132-132-132-1_V.mp3)
  Segment 22: 96312ms ~ 103404ms (file: 102-213-130-120-132-267-276-276-756-725-754-130-352-267-237-756-765-756-207-267-321-327-321-173-317-725-107-106-726-321-237-263-765-756-750-716-157-603-630-623-230-237-756-751-1_W.mp3)
  Segment 23: 104404ms ~ 112504ms (file: 275-572-574-103-213-310-263-273-236-137-207-270-726-276-230-310-103-720-203-201-326-763-102-103-123-206-236-231-132-267-317-321-320-172-320-143-102-137-103-173-270-310-230-213-1_W_stt_the_permanent_a_yankee_bank_and_don_diso.mp3)
  Segment 24: 113504ms ~ 114080ms (file: 715-175-170-715-573-576-576-576-756-576 107-750-157-170-715-150-715-157-570-750-751-715-751-571-751-517-751-571-576-576-756-576-756-576 105-715-701-107-157-510-501-501-517-105-5_W.mp3)
  Segment 25: 115080ms ~ 115656ms (file: 175-157-137-135-172-756-501-571-517-157-751-170-103-130-156-105-103-571-715-517-571-157-751-715-571-571-157-571-517-571-571-157-715-105-105-107-135-135-317-310-175-107-157-137-1_W.mp3)
  Segment 26: 116656ms ~ 117232ms (file: 315-125-126-123-371-715-135-317-715-756-675-765-627-150-207-267-263-251-132-570-267-756-765-627-672-627-136-102-125-132-132-501-125-231-732 153-503-125-312-136-123-710-672-376-5_V.mp3)
  Segment 27: 118232ms ~ 118808ms (file: 157-150-157-501-105-150-157-513-517-105-517-105-107-105-105-517-571-157-105-150-157-507 501-150-105-517-501-150-157-105-501-517-150-150-517-517-510-150-175-510-150-105-510-157-1_V.mp3)
  Segment 28: 119808ms ~ 128232ms (file: 167-137-137-261-130-163-136-261-231-321-106-237-213-276-572-756-751-765-321-312-123-157-123-231-132-725-132-531-572-231-273-312-267-231-157-523-217-213-261-256-526-715-102-572-1_W.mp3)
  Segment 29: 129232ms ~ 129808ms (file: 236 765-765-762 657-672-675-657-627-102-312-762-627-632-762-761-574-576-574-160 672-263-632-236-236-263 321-130-310-310-136-623 263-326-632-236-263-236-216-361-312-236-621-362 5_W.mp3)
  Segment 30: 130808ms ~ 131384ms (file: 356-275-756-263-576-523-726-276-326-326-327-236-752-623-216-576-756-567-207-726-321-231-276-672-537-736-527-706-765-276-375-507-253-523-325-520-216-267 507-517-271-275-750-765-2_V.mp3)
  Segment 31: 132384ms ~ 132960ms (file: 560-263-132-136-602-132 325-231-321-231-260-231-126_W.mp3)
  Segment 32: 133960ms ~ 141232ms (file: 326-230-230-236-236-120-321-103-126-260-206-201-672-261-126-201-210-213-132-210-231-231-231-236-236-263-206-260-321-236-321-327-230-762-326-321-102-136-210-136-267-236-102-120-1_W.mp3)
  Segment 33: 142232ms ~ 142808ms (file: 307-705-756-275-263-675-573-172-132-701-263-267-265-532-675-635-753-572-375-527-576-567-567-263-236-316-310-130-310-736-765-126-167-716-671-167-671-765-765-761-673-137-657-675-6_W.mp3)
  Segment 34: 143808ms ~ 144384ms (file: 162 621 312-321 541-570-570-572-153 756-756-756-765-675-675-756-576-607-675-671-627-570-576-750-576-156 620-627 106-103-103-136-132-136-172-103-136 312-301-317-316-213-261-123-3_V.mp3)
  Segment 35: 145384ms ~ 145960ms (file: 321-326-312-132-231-623-316-317-136-201-362-316-301-310-130-106-160-132-175-137 231-263-321-231-621-216-231-236-321-123-126-625-627-617-631-672_W.mp3)
  Segment 36: 146960ms ~ 147536ms (file: 576-361-627-576-501-132-316-215-125-126-570-362-156-517-123-517-203-125-167-276-371-123-573-751-357-275-572-637-372-507-132-567-526-162-705-175-571-152-136-517 163-657-173-571-5_V.mp3)
  Segment 37: 148536ms ~ 149112ms (file: 756-576-756-756-576-576-675-675-607-756-756-713-713-756-750-753-765-603-631-623-613-637-362_W.mp3)
  Segment 38: 150112ms ~ 161020ms (file: 206-231-235-105-106-106-236-325-203-207-527-210-261-130-167-165-321-213-237-203-752-126-603-263-263-236-213-326-703-130-162-236-763-267-213-237-216-120-130-123-130-267-263-267-2_W.mp3)
  Segment 39: 162020ms ~ 162596ms (file: 510-510-510-501-510-105-510-501-510-510-510-150-157-510-150-510-517-150-510 507-510-510-501-510-510-501-150-150-150-150-507-574-157-574-574-157-517-150-150-517-150-157-157-157-5_V.mp3)
  Segment 40: 163596ms ~ 164172ms (file: 105 547-574-574-574-574-574-574-574-574-574-574 547-547-547-547-547-574-574-574-574-574-754-574-574-574-574-754-754-574 574-574-574-574-574-547-574-574-574-574_W.mp3)
  Segment 41: 165172ms ~ 165748ms (file: 574-574-574-571-571-571 571-517-750-715-175-175-751-751-570-715-750-715-756-753-751-576-571-571-157 570-570-750-571-175-750-715-570-751-751-715-710-715-751-751-571-751-750-507 5_W.mp3)
  Segment 42: 166748ms ~ 167324ms (file: 167-103-132-562-623-132-136-136-163-710-165-132-362-516-671-132-106-167-163-103-160-106-132-652-106-137-316-132-103-106-173-106-610-675-160-615-605-671-165-673-162-231-765-576-6_V.mp3)
  Segment 43: 168324ms ~ 179052ms (file: 130-203-123-203-201-201-203-203-321-310-672-276-143-146-162-132-207-203-320-173-317-627-130-103-201-123-321-203-210-302-762-237-231-321-123-631-102-672-201-235-103-201-213-320-1_W.mp3)
  Segment 44: 180052ms ~ 180628ms (file: 571-571-571-571-571-570-105-157-103-105-107-157-317-163-512-571-510-570-106-156-153-157-157-156-152-513-125-157-135-531-315-351-571-571-571-513-175-150-715-157-175-751-175-751-7_V.mp3)
  Segment 45: 181628ms ~ 188756ms (file: 173-213-532-651-756-751-275-317-175-175-170-152-170-132-103-172-315-351-567-257-675-567-756-751-130-216-201-206-207-230-103-132-261-231-132-326-327-130-102-126-327-317-276-267-2_W.mp3)
  Segment 46: 189756ms ~ 190332ms (file: 765-576-517-157-765-716-756-765-765-517-571-576-576-751-527_W.mp3)
  Segment 47: 191332ms ~ 199504ms (file: 574-312-326-321-312-123-310-210-652-123-216-236-276-103-137-215-125-156-732-751-217-213-102-124-230-123-321-130-654-261-316-106-312-326-736-107-716-167-136-752-574-310-310-201-2_W.mp3)
  Segment 48: 200504ms ~ 208316ms (file: 765-675-576-571-675-612-103-123-326-136-135-526-265-236-315-321-263-236-102-137-136-310-136-137-103-547-574-570-570-230-231-547-540-210-321-312-537-572-312-217-230-203-573-572-2_W_stt_claude_whitaker_has_decreed_1512_season.mp3)
  Segment 49: 209316ms ~ 209892ms (file: 103-104-103-145-136-103-143-134-103-103-145-154-103-136-163-103-165-103-136-163-105-165-103-106-103-106-315-513-517-751-571-571-574-574-135-315-315-315-315-315-315-315-135-756-7_V.mp3)
  Segment 50: 210892ms ~ 219892ms (file: 261-754-312-326-765-103-321-326-574-231-123-312-103-103-765-236-310-130-216-275-132-123-751-103-310-621-267-236-162-167-320-236-231-103-621-627-102-120-103-107-127-275-106-261-2_W.mp3)
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
> **Time**: 2026-04-05 06:27:46
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "안녕하세요 만나서 반갑습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 263,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 263, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "만나서", "start_ms": 1650, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 2600, "dur_ms": 1100, "phase_lr_deg": 0.0}
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
"transcript": "오늘 날씨가 정말 화창하고 좋네요 산책하기 딱 좋은 날씨입니다",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 236,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 236, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 1500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "화창하고", "start_ms": 2100, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 3100, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "산책하기", "start_ms": 4200, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "딱", "start_ms": 5400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 5800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "날씨입니다", "start_ms": 6400, "dur_ms": 1200, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "외국어 공부를 할 때는 매일 조금씩이라도 꾸준히 하는 것이 가장 중요하다고 생각합니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 210,
"audio_type": "speech",
"words": [
{"word": "외국어", "start_ms": 210, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "공부를", "start_ms": 1000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 1700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "때는", "start_ms": 2100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "매일", "start_ms": 2800, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "조금씩이라도", "start_ms": 3500, "dur_ms": 1300, "phase_lr_deg": 0.0},
{"word": "꾸준히", "start_ms": 5000, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 5900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "것이", "start_ms": 6500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 7100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "중요하다고", "start_ms": 7800, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "생각합니다", "start_ms": 9000, "dur_ms": 1000, "phase_lr_deg": 0.0}
]
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
"transcript": "저는 요즘 한국 드라마를 보면서 한국어 표현을 익히고 있어요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 523,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 523, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "요즘", "start_ms": 1100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 1800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "드라마를", "start_ms": 2500, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "보면서", "start_ms": 3500, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "한국어", "start_ms": 4400, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "표현을", "start_ms": 5200, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "익히고", "start_ms": 6100, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 6900, "dur_ms": 600, "phase_lr_deg": 0.0}
]
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
"transcript": "언어는 단순히 단어를 외우는 것이 아니라 문화를 이해하는 과정입니다",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 627,
"audio_type": "speech",
"words": [
{"word": "언어는", "start_ms": 627, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "단순히", "start_ms": 1400, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "단어를", "start_ms": 2200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "외우는", "start_ms": 2900, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "것이", "start_ms": 3600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 4100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "문화를", "start_ms": 4800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "이해하는", "start_ms": 5500, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "과정입니다", "start_ms": 6400, "dur_ms": 1000, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "실제로 원어민과 대화해보는 것이 실력 향상에 가장 큰 도움이 됩니다",
"speaker": "speaker_2",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 231,
"audio_type": "speech",
"words": [
{"word": "실제로", "start_ms": 231, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "원어민과", "start_ms": 1000, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "대화해보는", "start_ms": 2000, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "것이", "start_ms": 3200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "실력", "start_ms": 3700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "향상에", "start_ms": 4300, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 5100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 5700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "도움이", "start_ms": 6100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "됩니다", "start_ms": 6800, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "틀리는 것을 두려워하지 말고 자신 있게 말하는 습관을 들이세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 520,
"audio_type": "speech",
"words": [
{"word": "틀리는", "start_ms": 520, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "것을", "start_ms": 1250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "두려워하지", "start_ms": 1800, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "말고", "start_ms": 3000, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "자신", "start_ms": 3700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "있게", "start_ms": 4300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "말하는", "start_ms": 4900, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "습관을", "start_ms": 5700, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "들이세요", "start_ms": 6600, "dur_ms": 800, "phase_lr_deg": 0.0}
]
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
"transcript": "반복해서 듣고 따라 하는 쉐도잉 학습법이 아주 효과적입니다",
"speaker": "speaker_2",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 310,
"audio_type": "speech",
"words": [
{"word": "반복해서", "start_ms": 310, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "듣고", "start_ms": 1250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "따라", "start_ms": 1800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 2400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "쉐도잉", "start_ms": 3000, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "학습법이", "start_ms": 3800, "dur_ms": 950, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 4900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "효과적입니다", "start_ms": 5500, "dur_ms": 1100, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "가장 좋아하는 한국 노래 가사를 외워보는 것도 좋은 방법이에요",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 102,
"audio_type": "speech",
"words": [
{"word": "가장", "start_ms": 102, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "좋아하는", "start_ms": 750, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 1600, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "노래", "start_ms": 2300, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "가사를", "start_ms": 2950, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "외워보는", "start_ms": 3750, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "것도", "start_ms": 4750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 5300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "방법이에요", "start_ms": 5900, "dur_ms": 900, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "The permanent yankee bank and don diso.",
"speaker": "speaker_1",
"confidence": 0.81,
"language": "en",
"silence_before_ms": 275,
"audio_type": "speech",
"words": [
{"word": "The", "start_ms": 275, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "permanent", "start_ms": 550, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "yankee", "start_ms": 1450, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "bank", "start_ms": 2200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "and", "start_ms": 2800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "don", "start_ms": 3250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "diso", "start_ms": 3750, "dur_ms": 600, "phase_lr_deg": 0.0}
]
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
"transcript": "포기하지 않고 끝까지 노력하면 반드시 좋은 결과가 있을 거예요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 167,
"audio_type": "speech",
"words": [
{"word": "포기하지", "start_ms": 167, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "않고", "start_ms": 1100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "끝까지", "start_ms": 1650, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "노력하면", "start_ms": 2500, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "반드시", "start_ms": 3500, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 4350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "결과가", "start_ms": 4950, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "있을", "start_ms": 5800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 6350, "dur_ms": 650, "phase_lr_deg": 0.0}
]
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
"transcript": "여러분의 한국어 공부를 진심으로 응원합니다 화이팅 하세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 326,
"audio_type": "speech",
"words": [
{"word": "여러분의", "start_ms": 326, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "한국어", "start_ms": 1200, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "공부를", "start_ms": 2000, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "진심으로", "start_ms": 2750, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "응원합니다", "start_ms": 3750, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "화이팅", "start_ms": 4900, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "하세요", "start_ms": 5750, "dur_ms": 700, "phase_lr_deg": 0.0}
]
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
"transcript": "오늘 수업은 여기까지입니다 다음 시간에 더 재미있는 주제로 만나요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 206,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 206, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "수업은", "start_ms": 750, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "여기까지입니다", "start_ms": 1500, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 2800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "시간에", "start_ms": 3400, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 4150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "재미있는", "start_ms": 4550, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "주제로", "start_ms": 5500, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 6350, "dur_ms": 650, "phase_lr_deg": 0.0}
]
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
"transcript": "안녕히 계세요 감사합니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 130, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "계세요", "start_ms": 850, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 1700, "dur_ms": 1100, "phase_lr_deg": 0.0}
]
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
"transcript": "공부하다가 모르는 부분이 있으면 언제든지 질문해 주세요",
"speaker": "speaker_2",
"confidence": 0.91,
"language": "ko",
"silence_before_ms": 574,
"audio_type": "speech",
"words": [
{"word": "공부하다가", "start_ms": 574, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "모르는", "start_ms": 1650, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "부분이", "start_ms": 2400, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "있으면", "start_ms": 3150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "언제든지", "start_ms": 3850, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "질문해", "start_ms": 4800, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 5600, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "Claude Whitaker has decreed 1512 season.",
"speaker": "speaker_1",
"confidence": 0.85,
"language": "en",
"silence_before_ms": 765,
"audio_type": "speech",
"words": [
{"word": "Claude", "start_ms": 765, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "Whitaker", "start_ms": 1300, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "has", "start_ms": 2100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "decreed", "start_ms": 2500, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "1512", "start_ms": 3300, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "season", "start_ms": 4500, "dur_ms": 650, "phase_lr_deg": 0.0}
]
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
"transcript": "그럼 모두 수고하셨습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 261,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 261, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "모두", "start_ms": 800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "수고하셨습니다", "start_ms": 1400, "dur_ms": 1200, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 06:27:46*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=10820  time=06:26:42  uptime=8538s  mem=381MB  system_processes=357"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_062335.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 706-540-572-761-173-312-132-671-157-162-726-205-236-570-527-253-725-275-570-502-527-560-276-275-372-513-572-675-726-236-237-570-165-253-570-526-527-312-267 275 536-257-321-573-2_V.mp3)
  Segment 2: 1576ms ~ 8092ms (file: 321-106-213-261-576-765-576-576-574-216-754-576-756-572-627-123-231-267-657-103-135-361-163-675-570-260-265-157-105-756-160-136-765-265-623-173-574-765-136-541-132-675-627-756-3_W.mp3)
  Segment 3: 9092ms ~ 9668ms (file: 167-567-132-102-126-106-123-130-103-106-623-123-310-312-103-163-136-612-167-125-613-163-135-136-316-103-236 351-320-321-312-103-316 312_V.mp3)
  Segment 4: 10668ms ~ 11244ms (file: 267-627-627-162-623-631-362-326 756-753-675-672 376-175-103-173-231-206-527-205-502-526-572-302-132-321-316-351-315-316-321-301-301 321-320-312-312-352-315-312-135-315-135-315-2_V.mp3)
  Segment 5: 12244ms ~ 12820ms (file: 632-750-576-765-670-107-160-132-163-321-136-103-106-130-132-126-126-132-650-650-675-236-631-362-361-632-316-321-301-316-103-213-206-321-123-126-213-316-130-261-210-263-216-260-6_W.mp3)
  Segment 6: 13820ms ~ 14396ms (file: 135-106-130-103-631-316-316-615-130-162-613-167-103-175-136-602-612-361-761-631-623-361-301-163-130-160-765-106-162-167-103-130-132-163-136-172-130-672-621-156-167-632-617-637-6_V.mp3)
  Segment 7: 15396ms ~ 15972ms (file: 130-213-672-312-316-316-137-137-172-312-173-137-163-137-106-127-726-237-267-216-632-625-625-125-657-356-536-351-527-157-317-731-713-736-635-173-120-301-135-137-765-752-317-103-1_V.mp3)
  Segment 8: 16972ms ~ 17548ms (file: 576-576-675-765-765-765-756-576-576-567-567-576-527-567-572-576-753-257-352-571-576-574-576-576-652-527-576-537-537-571-357-237-632-362-567-567 567-576-567-675-561 765-756-756-7_W.mp3)
  Segment 9: 18548ms ~ 19124ms (file: 371-105 357-173-137-107-173-103-130-310-107-703-103-613-607-301-731-307-310-317-301-317-173-175-315-170-375-537-573-753-750-573-750-750-750 750-135-307-130-173-317-301-301-713-3_V.mp3)
  Segment 10: 20124ms ~ 20700ms (file: 507-152-135-517-157-150-571-157-517-751-175-157-753-571-571-157-571-517-517-715-175-517-571-537-517-157-571-571-573-573-571-157-513-130-175-715-157-135-713-157-715-571-756-175-1_V.mp3)
  Segment 11: 21700ms ~ 22276ms (file: 756-573-753-753-751-571-753-751-576-572-573-135-751-310-135-351-157-157-513-517-157-175-157-157-157-517-571-517-517-571-571-153-570-576-756-576-735-756-751-756-756-753-756-576-7_W.mp3)
  Segment 12: 23276ms ~ 32564ms (file: 103-137 563-351-356-130-236-230-320-103-672-103-320-276-206-321-123-103-130-103-312-316-321-302-320-320-267-732-107-527-276-276-572-523-376-136-173-301-310-574-235-312-302-123-6_W.mp3)
  Segment 13: 33564ms ~ 34140ms (file: 263-263-326-231-261-236-236-231-231-326-236-326 263-623-236-263-326-312-276-267-627-267-103-132-203-132_W.mp3)
  Segment 14: 35140ms ~ 35716ms (file: 103-137-137-137-174-137-103-134-103-103-107-173-170-107-701-107-170-170-170 137-107-173-107-173-107-107-107-173-107-173-107-173-107-107-152-165-167-176-167-163-156-752-752-756-7_V.mp3)
  Segment 15: 36716ms ~ 51872ms (file: 263-267-762-267-236-267-756-327-123-321-263-623-237-265-627-260-106-103-263-263-103-261-261-231-236-316-102-263-623-263-263-103-316-632-756-312-130-361-310-132-235-315-321-123-1_W.mp3)
  Segment 16: 52872ms ~ 61872ms (file: 261-754-312-326-765-103-321-326-574-231-123-312-103-103-765-236-310-130-216-275-132-123-751-103-310-621-267-236-162-167-320-236-231-103-621-627-102-120-103-107-127-275-106-261-2_W.mp3)
  Segment 17: 62872ms ~ 63448ms (file: 761-176-160-123-351-316-316-175-163_W.mp3)
  Segment 18: 64448ms ~ 65024ms (file: 615-567-163-123-160-103-163-126-671-135-102-156-103-136-103-167-615-761-163-276_V.mp3)
  Segment 19: 66024ms ~ 71172ms (file: 267-756-267-261-267-236-326-267-267-267-267-276-267-213-627-263-267-267-273-627-372-326-267-326-237-136-765-675-176-137-726-267-267-765-132-213-236-123-126-132-576-276-627-627-1_W.mp3)
  Segment 20: 72172ms ~ 72748ms (file: 750-501-501-105-517-170-105-157-157-517-571-517-157-157-156-157-517-157-157-157-157-157-135 154-105-150-150-157-150-153-157-157-175-357 103-105-154-150-157-175-175-157-175-157-1_V.mp3)
  Segment 21: 73748ms ~ 74324ms (file: 106-617-167-163-106-631-601-602-620-612-623-301-302-301-601-126-206-526-103-103-201-260-216-216-276-236-263-206-261-126-123-216-126-312-126-123-126-261-261-621-321-123-102-103-3_V.mp3)
  Segment 22: 75324ms ~ 75900ms (file: 317-136 130-613-267-126-132-163-530-165-602-132-137-612-361-136-132-106-751-163-617-210-132-123-137-165-103-167-103-657-361-671-136-361-165-163-103-136-137-136-103-167-167-623-1_V.mp3)
  Segment 23: 76900ms ~ 83308ms (file: 203-213-231-321-201-120-312-132-201-312-201-302-213-123-321-210-320-321-302-231-213-203-201-326-230-631-206-627-670-762-620-276-620-760-206-206-106-123-217-267-216-320-320-312-2_V.mp3)
  Segment 24: 84308ms ~ 84884ms (file: 625-615-706-301-173-617-127-137-236_V.mp3)
  Segment 25: 85884ms ~ 92436ms (file: 135-103-215-765-607-206-135-103-263-126-135-312-132-125-102-120-361-102-105-210-132-315-203-706-607-231-153-135-627-670-502-275-312-574-213-230-230-235-203-213-310-312-302-263-7_V.mp3)
  Segment 26: 93436ms ~ 94012ms (file: 105-517-517-517-510-501-510-501-510-150-150-571-157-105-516-571-576-576-567-567-576-576-576-576 517-507-510-501-150-517-501-517-157-150-510-507-510-150-150-150-510-150-501-157-1_V.mp3)
  Segment 27: 95012ms ~ 95588ms (file: 160-517-132-130-136-163-132-621-623 316-103-106-136-317-165 137-103-137-136-317-103-514-167-136-657-136-123-136-132-163-632-326-312-103-136-103-135-137-130-136-673-607-163-612-6_V.mp3)
  Segment 28: 96588ms ~ 97164ms (file: 632-673-326-367-675-765-675-765-137-307-257 317-137-731-137_W.mp3)
  Segment 29: 98164ms ~ 98740ms (file: 571-571-570-576-516-517-516-513-571-502-521-516-530-510-517-571-175-150-715-570-571-517-751-751-753-517-576-571-576-571-571-576-576-513-570-501-510-501-570-517-501-507-150-517-5_V.mp3)
  Segment 30: 99740ms ~ 100316ms (file: 175-507-570-751-710-170-756-751-107-751-701-571-571-750-574 517-150-571-571-571-756-750-715-750-705-750-570-751-175-750_V.mp3)
  Segment 31: 101316ms ~ 101892ms (file: 672-675-762-675-671-637-621-675-627-126-756-762-672-627-672-123 672-765-657-621-637-675-216-236-261-362-627-613-126-612-167-167_W.mp3)
  Segment 32: 102892ms ~ 103468ms (file: 176-761-172-172-137-312-612-132-175-172-163-136-163-165-163-136-613-167-316-316-136-167-612-316-132-172-137-175-175-172-317-137-173-375-275-762-675-716-715-712-176-271-176-127-1_W.mp3)
  Segment 33: 104468ms ~ 111416ms (file: 574-172-175-132-103-312-176-761-620-317-621-574-210-213-206-637-321-217-207-570-175-621-132-137-756-123-520-507-325-261-123-132-754-120-672-276-213-216-123-132-572-132-271-206-6_V.mp3)
  Segment 34: 112416ms ~ 112992ms (file: 263-236-263-236-632-620-621-627-321-312-130-120-132-231-671-623-623-263-256-236_W.mp3)
  Segment 35: 113992ms ~ 114568ms (file: 302-326-302-263-163-326-362-320-623-620-126-103-765-167-312-316-301-173-137-103-623-720-306-760-761-632-361-321-160-612-261-231-126-162-162-260-102-106-602-612-107-620-263-320-3_W.mp3)
  Segment 36: 115568ms ~ 116144ms (file: 132-162-172-103-170-317-123-316-631-103-360-306-623-362-361-361-357-172-137-261-152-123-136-325-570-367-326-761-367-312-357-316-236-321 315-574-574-576-136-376-613-162-750-761-6_V.mp3)
  Segment 37: 117144ms ~ 117720ms (file: 701-713-176-107-170-710-761-716-710-170-706-710-710-716-701-107-705 105-170-705-107-173-175-107-103_V.mp3)
  Segment 38: 118720ms ~ 119296ms (file: 126-136-163-165-621-671-617-106-160-125-610-136-176-627-561-516-165-165-167-617-621-261-526-516-165-510-536-617-671-613-571-567-156-137-165-657-617-672-157-157-231-657-576-516-5_W.mp3)
  Segment 39: 120296ms ~ 129152ms (file: 231-120-210-102-132-302-310-130-106-107-317-751-726-627-362-176-617-152-716-176-721-671-172-175-721-725-102-623-213-132-157-105-751-107-175-105-571-754-576-576-175-705-754-732-5_W.mp3)
  Segment 40: 130152ms ~ 130728ms (file: 547-574-574-570 507-150-501-105-103-105-507-105-517-501-501-157-507-506-576-574-506-571-510-570-517-150-705-570 150-150-105-105-517-517-507-507-501-571-510-507-150-517-570-574-5_W.mp3)
  Segment 41: 131728ms ~ 140260ms (file: 216-675-273-230-672-132-607-635-657-607-317-321-123-103-321-213-123-230-236-267-627-203-371-230-176-127-132-103-130-310-320-321-203-267-326-213-102-137-315-532-375-312-235-321-3_W.mp3)
  Segment 42: 141260ms ~ 145112ms (file: 210-213-210-216-261-261-126-237-627-635-326-621-236-263-261-210-216-132-623-620-203-216-627-267-672-107-623-627-627-672-276-620-276-120-762-267-362-627-132-623-672-326-263-627-3_W.mp3)
  Segment 43: 146112ms ~ 153600ms (file: 316-317-137-137-321-136-326-216-316-267-671-123-106-237-173-136-170-102-213-231-216-576-213-321-130-103-106-130-261-216-301-312-103-261-627-261-657-102-163-132-312-103-316-261-2_W.mp3)
  Segment 44: 154600ms ~ 155176ms (file: 571-571-571-517-570-571-571-571-570-517 510-517-571-517-517-571-571-570-571-571-570-570-570-570-570 517-517-517-517-517-513-517-571-571-517-517-517-570-571-571-517-570-503-571-5_W.mp3)
  Segment 45: 156176ms ~ 156752ms (file: 157-157-137-135-157-103-103-105-105-750-705-103-103-107 107-157-103-103-175-157-153-107-130-103-105-173-157-153-103-103-105-135-103-175-751-570-507-501-105-107-105-157-715-731-7_V.mp3)
  Segment 46: 157752ms ~ 158328ms (file: 261-136-124-125-127-123-107-124-367-160-162-162-136-127-231-123-312-310-103-310 312 314-103-136-107-132-576-572-321-126-126-125-165-162-135 134-167-176-675-765-765-753-576-756-7_V.mp3)
  Segment 47: 159328ms ~ 164908ms (file: 572-752-752-265-267-321-327-307-572-572-321-271-120-152-726-726-726-756-672-207-275-750-763-157-723-327-176-132-217-175-102-120-123-103-123-102-123-251-127-120-120-132-123-127-1_W.mp3)
  Segment 48: 165908ms ~ 166484ms (file: 175-756-675-675-576-615-167-271-721-321-217-615-716-176-235-275-137-537-672-762-236-321-237-527-253-762-326-507-751-706-576-760-170-103-510-107-750-276-762-726-273-263-750-265-2_V.mp3)
  Segment 49: 167484ms ~ 168060ms (file: 750-570-576 701-105-701-715-150-175-715-175-751-571-175-157-571-576-567-576-576-516-561-576-576-756-576-756 570-570-501-157-175-170-175-170-107-107-710-715-705-751-751-175-756-5_W.mp3)
  Segment 50: 169060ms ~ 169636ms (file: 103-103-130-321-574-567-540-541-601-620-601-570-574-657-627-261-574 357-136-130-123-627-621-261-625-512-625-621-326-431-134-163-163-613-316-163-527-206-732-306-267-527-620-602-2_W.mp3)
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
> **Time**: 2026-04-05 06:27:46
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "안녕하세요 만나서 반갑습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 263,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 263, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "만나서", "start_ms": 1650, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 2600, "dur_ms": 1100, "phase_lr_deg": 0.0}
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
"transcript": "오늘 날씨가 정말 화창하고 좋네요 산책하기 딱 좋은 날씨입니다",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 236,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 236, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 1500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "화창하고", "start_ms": 2100, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 3100, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "산책하기", "start_ms": 4200, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "딱", "start_ms": 5400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 5800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "날씨입니다", "start_ms": 6400, "dur_ms": 1200, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "외국어 공부를 할 때는 매일 조금씩이라도 꾸준히 하는 것이 가장 중요하다고 생각합니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 210,
"audio_type": "speech",
"words": [
{"word": "외국어", "start_ms": 210, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "공부를", "start_ms": 1000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 1700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "때는", "start_ms": 2100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "매일", "start_ms": 2800, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "조금씩이라도", "start_ms": 3500, "dur_ms": 1300, "phase_lr_deg": 0.0},
{"word": "꾸준히", "start_ms": 5000, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 5900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "것이", "start_ms": 6500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 7100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "중요하다고", "start_ms": 7800, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "생각합니다", "start_ms": 9000, "dur_ms": 1000, "phase_lr_deg": 0.0}
]
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
"transcript": "저는 요즘 한국 드라마를 보면서 한국어 표현을 익히고 있어요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 523,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 523, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "요즘", "start_ms": 1100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 1800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "드라마를", "start_ms": 2500, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "보면서", "start_ms": 3500, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "한국어", "start_ms": 4400, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "표현을", "start_ms": 5200, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "익히고", "start_ms": 6100, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 6900, "dur_ms": 600, "phase_lr_deg": 0.0}
]
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
"transcript": "언어는 단순히 단어를 외우는 것이 아니라 문화를 이해하는 과정입니다",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 627,
"audio_type": "speech",
"words": [
{"word": "언어는", "start_ms": 627, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "단순히", "start_ms": 1400, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "단어를", "start_ms": 2200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "외우는", "start_ms": 2900, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "것이", "start_ms": 3600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 4100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "문화를", "start_ms": 4800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "이해하는", "start_ms": 5500, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "과정입니다", "start_ms": 6400, "dur_ms": 1000, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "실제로 원어민과 대화해보는 것이 실력 향상에 가장 큰 도움이 됩니다",
"speaker": "speaker_2",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 231,
"audio_type": "speech",
"words": [
{"word": "실제로", "start_ms": 231, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "원어민과", "start_ms": 1000, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "대화해보는", "start_ms": 2000, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "것이", "start_ms": 3200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "실력", "start_ms": 3700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "향상에", "start_ms": 4300, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 5100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 5700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "도움이", "start_ms": 6100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "됩니다", "start_ms": 6800, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "틀리는 것을 두려워하지 말고 자신 있게 말하는 습관을 들이세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 520,
"audio_type": "speech",
"words": [
{"word": "틀리는", "start_ms": 520, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "것을", "start_ms": 1250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "두려워하지", "start_ms": 1800, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "말고", "start_ms": 3000, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "자신", "start_ms": 3700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "있게", "start_ms": 4300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "말하는", "start_ms": 4900, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "습관을", "start_ms": 5700, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "들이세요", "start_ms": 6600, "dur_ms": 800, "phase_lr_deg": 0.0}
]
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
"transcript": "반복해서 듣고 따라 하는 쉐도잉 학습법이 아주 효과적입니다",
"speaker": "speaker_2",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 310,
"audio_type": "speech",
"words": [
{"word": "반복해서", "start_ms": 310, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "듣고", "start_ms": 1250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "따라", "start_ms": 1800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 2400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "쉐도잉", "start_ms": 3000, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "학습법이", "start_ms": 3800, "dur_ms": 950, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 4900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "효과적입니다", "start_ms": 5500, "dur_ms": 1100, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "가장 좋아하는 한국 노래 가사를 외워보는 것도 좋은 방법이에요",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 102,
"audio_type": "speech",
"words": [
{"word": "가장", "start_ms": 102, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "좋아하는", "start_ms": 750, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 1600, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "노래", "start_ms": 2300, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "가사를", "start_ms": 2950, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "외워보는", "start_ms": 3750, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "것도", "start_ms": 4750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 5300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "방법이에요", "start_ms": 5900, "dur_ms": 900, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "The permanent yankee bank and don diso.",
"speaker": "speaker_1",
"confidence": 0.81,
"language": "en",
"silence_before_ms": 275,
"audio_type": "speech",
"words": [
{"word": "The", "start_ms": 275, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "permanent", "start_ms": 550, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "yankee", "start_ms": 1450, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "bank", "start_ms": 2200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "and", "start_ms": 2800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "don", "start_ms": 3250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "diso", "start_ms": 3750, "dur_ms": 600, "phase_lr_deg": 0.0}
]
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
"transcript": "포기하지 않고 끝까지 노력하면 반드시 좋은 결과가 있을 거예요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 167,
"audio_type": "speech",
"words": [
{"word": "포기하지", "start_ms": 167, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "않고", "start_ms": 1100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "끝까지", "start_ms": 1650, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "노력하면", "start_ms": 2500, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "반드시", "start_ms": 3500, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 4350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "결과가", "start_ms": 4950, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "있을", "start_ms": 5800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 6350, "dur_ms": 650, "phase_lr_deg": 0.0}
]
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
"transcript": "여러분의 한국어 공부를 진심으로 응원합니다 화이팅 하세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 326,
"audio_type": "speech",
"words": [
{"word": "여러분의", "start_ms": 326, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "한국어", "start_ms": 1200, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "공부를", "start_ms": 2000, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "진심으로", "start_ms": 2750, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "응원합니다", "start_ms": 3750, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "화이팅", "start_ms": 4900, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "하세요", "start_ms": 5750, "dur_ms": 700, "phase_lr_deg": 0.0}
]
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
"transcript": "오늘 수업은 여기까지입니다 다음 시간에 더 재미있는 주제로 만나요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 206,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 206, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "수업은", "start_ms": 750, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "여기까지입니다", "start_ms": 1500, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 2800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "시간에", "start_ms": 3400, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 4150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "재미있는", "start_ms": 4550, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "주제로", "start_ms": 5500, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 6350, "dur_ms": 650, "phase_lr_deg": 0.0}
]
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
"transcript": "안녕히 계세요 감사합니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 130, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "계세요", "start_ms": 850, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 1700, "dur_ms": 1100, "phase_lr_deg": 0.0}
]
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
"transcript": "공부하다가 모르는 부분이 있으면 언제든지 질문해 주세요",
"speaker": "speaker_2",
"confidence": 0.91,
"language": "ko",
"silence_before_ms": 574,
"audio_type": "speech",
"words": [
{"word": "공부하다가", "start_ms": 574, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "모르는", "start_ms": 1650, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "부분이", "start_ms": 2400, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "있으면", "start_ms": 3150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "언제든지", "start_ms": 3850, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "질문해", "start_ms": 4800, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 5600, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "Claude Whitaker has decreed 1512 season.",
"speaker": "speaker_1",
"confidence": 0.85,
"language": "en",
"silence_before_ms": 765,
"audio_type": "speech",
"words": [
{"word": "Claude", "start_ms": 765, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "Whitaker", "start_ms": 1300, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "has", "start_ms": 2100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "decreed", "start_ms": 2500, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "1512", "start_ms": 3300, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "season", "start_ms": 4500, "dur_ms": 650, "phase_lr_deg": 0.0}
]
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
"transcript": "그럼 모두 수고하셨습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 261,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 261, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "모두", "start_ms": 800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "수고하셨습니다", "start_ms": 1400, "dur_ms": 1200, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 06:27:46*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=1700  time=06:25:23  uptime=6590s  mem=1734MB  system_processes=353"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_062436.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 236-236-627-260-236-203-260-267-316-627-276-267-267-267-627-162-263 263-627-103-310-103-103-103-107-132-231-162-627-263-326-762-627-260-627-602-267-167-103-103-756-675-136-167-1_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 261-267-276-257-567-762-576-675-762-576-753-750_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 732-137-153-153-157-153-371-735-576-765-315-156-135-165-106-106-106-106-165-156-156-157-736-762-736-726-752-762-753-753-735-752-756-735-765-765-765-756-765-765-765-765-756-765-7_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 367-614-713-641-253 132-326 612-612_V.mp3)
  Segment 5: 6304ms ~ 15196ms (file: 137-123-261-260-760-213-102-201-576-106-574-123-760-120-102-203-213-206-230-260-260-312-103-307-132-764-267-670-132-620-321-206-321-132-301-103-103-102-120-175-574-157-103-206-2_V.mp3)
  Segment 6: 16196ms ~ 16772ms (file: 137-137-172-123-172-173-137 135-173-107-137-103-137-173-137-127-172-137-173-137-137-170-137-173-173-173-175-175-130-170 157-170-173-107-137-130-137-170-137-130-172-173-173-170-1_V.mp3)
  Segment 7: 17772ms ~ 18348ms (file: 261-261-621-267-267-621-623-625-625-652-652-625-652-562-567-657-651 623-625-625-621-627-267-627-621-621-261-261-265-260-652-651-652-562-563-652-567-635-652 621-620-625-620-625-2_W.mp3)
  Segment 8: 19348ms ~ 29644ms (file: 103-230-126-316-260-627-120-210-216-267-261-216-267-260-210-201-215-261-310-310-130-267-326-310-103-120-130-307-107-310-320-302-310-320-327-107-107-713-130-312-213-310-103-213-3_V.mp3)
  Segment 9: 30644ms ~ 31220ms (file: 751-751-571-571-571-751-571-756-576-753-751-576-570 507-517-516-561-105-516-150-157-175-153-715-157-570-170-175-715-751-751-751-517-571-571-571-571-576-576-571-571-751-751-572-7_V.mp3)
  Segment 10: 32220ms ~ 32796ms (file: 762-216-365-560-276-216-567-532-215-206-752-257-237-751-135-725-703-716-273-156-756-231-256-756-567-573-317-731-572-756-275-751-573-537-756-675-216-217-173-751-751-721-127-123-1_V.mp3)
  Segment 11: 33796ms ~ 41212ms (file: 167-163-127-106-123-132-132-120-276-203-231-206-201-127-123-301-302-312-253-103-725-301-203-236-524-320-236-320-120-260-231-132-143-137-726-301-103-310-102-527-310-203-136-312-1_V_stt_all.mp3)
  Segment 12: 42212ms ~ 42788ms (file: 507-570-170-501-570-507-571-107-507-570-574-574 754-574-574-574-715-173-175-157-751-754 574-574-570-574-570-574-574-574-754-574-574-574-574-574 570-570-750-750-570-570-571-507-5_W.mp3)
  Segment 13: 43788ms ~ 44364ms (file: 162-163-176-163-132-312-136-267-726-762-635-162-576-723-352-273-317-137-317-317-317-312-327-567-306-507-675-762-726-765-715-317-135-731-352-752-753-735-376-367-271-716-263-507-2_V.mp3)
  Segment 14: 45364ms ~ 45940ms (file: 103-130-126-130-132-103-103-103-602-103-710-136-163-136-167-136-103-172-106-156-130-130-156-176-103-136-103-103-136-136-136-671-160-163-163-135-613-351-136-316-103-160-103-163-1_V.mp3)
  Segment 15: 46940ms ~ 47516ms (file: 621-763-256-276-261-621-576-576-576-657-675-652-627-570-576-576-267-265-265-547-526-576-756-576-756-216-231-765-672-106-107-657-672-672-672-160-136-756-567-762-672-675-672-167-6_W.mp3)
  Segment 16: 48516ms ~ 56328ms (file: 631-750-732-237-273-321-130-627-623-312-231-675-301-716-326-132-673-263-267-627-574-721-312-276-267-276-276-276-217-231-217-120-312-271-175-756-723-712-517-273-137-123-132-276-2_W.mp3)
  Segment 17: 57328ms ~ 78604ms (file: 210-103-102-104-126-103-102-132-301-203-310-230-203-206-320-206-137-135-312-103-103-132-173-137-206-312-321-175-276-237-207-762-123-126-130-276-762-132-361-107-103-372-317-124-2_V.mp3)
  Segment 18: 79604ms ~ 85616ms (file: 126-176-261-612-621-260-276-621-261-120-765-176-576-251-215-517-120-150-152-125-216-123-132-265-710-761-763-763-765-765-765-675-617-670-761-216-167-627-612-762-276-267-672-672-6_W.mp3)
  Segment 19: 86616ms ~ 87192ms (file: 310 102-130-103-130-310-310-130-310-132-123-312-123-132-123-213-132-132 120-123-123-123-123-312-123-132-132-132-120-120-123-126-103-621-627-672-675-657-657-623-102-102-102-103-1_V.mp3)
  Segment 20: 88192ms ~ 88768ms (file: 327-735-367-376-673-713-327-367-371-276_W.mp3)
  Segment 21: 89768ms ~ 90344ms (file: 750-751-751-750-756-157-516-657-751-176-756-671-175-176-726-726-671-527-672-725-765-765-723-756-172-167-136-317-657-567-576-571-751-751-175-571-175-751-571-175-715-715-175-715-7_W.mp3)
  Segment 22: 91344ms ~ 91920ms (file: 625-621-612-621-265-621-625-621-267-621-620-620-627 621-620-620-621-627-621-621-267-625-261-267-623-261-621 621-620-621-621-627-625-627-265-627-625-625-625-625-260 623-625-627-6_W.mp3)
  Segment 23: 92920ms ~ 93496ms (file: 716-127-157-135-157-135-501-571-715-715-751-157-517-175-157-157-571-751-517-756-517-571-571-517-571-570-571-570-570-510-150-153-103-105-130-175-175-170-176-153-715-517-716-170-1_W.mp3)
  Segment 24: 94496ms ~ 95072ms (file: 132-302-235-327-327-216-132-312-213-312-235-203-205-136-120-326-231-321-263-236-327-231-327-317-107-137-726-301-362-301-302-203-376-321-123-236-312-173-276-207-270-273-123-137-7_W.mp3)
  Segment 25: 96072ms ~ 96648ms (file: 756-126-351-135-301-103 132-705-367-361-361-367-637-637-367-607-367-637-760-763-763 531-576-236-132-312-132-132-536-652-623-546-657-675-657-756-675-756-756-625-526-235-267-263-5_W.mp3)
  Segment 26: 97648ms ~ 103156ms (file: 321-261-362-276-371-236-271-160-376-253-361-321-130-276-362-261-261-621-260-632-312-261-263-310-275-631-312-326-267-263-261-326-236-263-263-261-537-216-216-162-321-106-501-576-5_V.mp3)
  Segment 27: 104156ms ~ 112904ms (file: 230-261-206-260-260-213-210-201-270-103-257-137-137-210-207-132-237-207-231-267-127-276-123-321-576-203-275-103-165-203-236-123-132-123-103-203-237-361-102-120-216-761-621-357-1_W.mp3)
  Segment 28: 113904ms ~ 114480ms (file: 150-157-175-157-157-175-715-157-517-175-517-157-751-751-571-156-105-517-175-157-150-150-150-105-135-126-163-167-123-123-127-123-123-126-357-361-237-357-371-521-731-316-275-375-1_W.mp3)
  Segment 29: 115480ms ~ 126352ms (file: 301-103-132-312-310-107-107-176-130-231-312-132-103-231-231-213-210-271-761-752-273-320-327-715-270-127-752-574-217-276-752-715-176-273-273-321-321-327-132-173-270-270-301-270-1_W.mp3)
  Segment 30: 127352ms ~ 127928ms (file: 517-157-175-150-501-157-510-150-150-157-150-105-157-150-150-157-517-153 571-150-517-571-517-517-507-517-157-175-751-517-105-150-157-507-150-517-105-501-105-510-517 157-517-175-7_W.mp3)
  Segment 31: 128928ms ~ 129504ms (file: 673-317-167-316-173-130-165-531-365-365-637-632-637-675-367-675-607-136-130-103-132 713-163-136-165-173-310-360-631-312-630-321-316-362-765-270-237-576-576-357-756-765-123-163-1_W.mp3)
  Segment 32: 130504ms ~ 134824ms (file: 267-276-762-267-276-276-237-312-123-261-167-317-213-271-726-672-102-301-326-327-316-321-123-321-263-327-312-261-123-216-621-263-267-271-162-672-162-126-316-731-132-231-231-132-7_W.mp3)
  Segment 33: 135824ms ~ 142736ms (file: 362-376-276-617-672-276-276-231-267-236-627-231-273-236-276-635-267-267-126-321-263-267-267-321-675-672-672-612-362-756-572-137-217-316-637-632-263-637-762-726-726-267-103-301-1_W.mp3)
  Segment 34: 143736ms ~ 152196ms (file: 175-235-547-320-237-326-573-754-263-267-263-326-312-574-235-547-540-724-725-267-236-326-236-321-702-321-327-237-327-375-137-537-513-276-572-273-752-752-236-132-320-236-216-573-5_W.mp3)
  Segment 35: 153196ms ~ 153772ms (file: 312-321-206-206-206-312-136-103-132-167-143-153-132-570-107 132-312-301-317-236-236-132-613-620-160-130-136-103-123-310-136-163 136-156-163-134-132-162-102-627-621-631-362-631-4_V.mp3)
  Segment 36: 154772ms ~ 155348ms (file: 627-267-672-627-672-762-672-673-627-276 216_W.mp3)
  Segment 37: 156348ms ~ 156924ms (file: 510-501-501-150-510-501-507-150-510-501-510-507-501-507 517-105-517-501-517-175-105-150-105-750-571-510-105-510-501-507-510-507-510-510-150-571 150-105-157-501-510-517-507-501-5_V.mp3)
  Segment 38: 157924ms ~ 158500ms (file: 750-751-750-756-753-754-105-105-105-105-105-150-150-157-175-176-175-157-175-756-751-715-175-157-571-175-751-157-157-157-175-175 105-157-105-150-175-167-156-576-157-751-156-761-1_V.mp3)
  Segment 39: 159500ms ~ 160076ms (file: 173-107-173-130 157-137-135-173-157-157-517-571-571-357-573-152-175-765-756-517-157 157-150-517-517-517-517-513-570-157-135-175-157-175-531-753-715-571-750-751-517-570-751-751_W.mp3)
  Segment 40: 161076ms ~ 171552ms (file: 137-176-137-321-267-120-217-321-263-260-260-267-320-127-321-237-672-627-263-230-657-236-236-236-312-210-124-321-163-310-623-162-167-267-267-263-237-326-102-276-321-231-132-732-1_W.mp3)
  Segment 41: 172552ms ~ 173128ms (file: 325-312-317-317-125-152-517-517-175-157-371-175-236-235-537-517-573-572-574-574-531-532-517-570-517-537-153-157-531-351-531-537-357-537-357-357-537-256-541-547-534-351-357-315-2_W.mp3)
  Segment 42: 174128ms ~ 174704ms (file: 576-576-517-571-571-517-571-157-571-517-150-517-510-517-156-531-157-510-517-570-513-531-576-153-517-571-576-563-513-513-512-510-157-517-157-175-153-715-157-571-510-750-517-571-7_W.mp3)
  Segment 43: 175704ms ~ 176280ms (file: 231 137-632-627-167-623-603-167-103-163-317-132-136-126 162-617-156-136-127-172-612-152-102-162-625-106-167-160-172-126-106-106-163-107-136-631-172-127-165-103-653-106-567-625-1_V.mp3)
  Segment 44: 177280ms ~ 177856ms (file: 301-540-547-547-503-354 540-547-504-651-157-175-143-132-103-132-136-213-236-231-620-576-672-670-672-617-163-312-573-371-573-576-230-260-230-231-213-132-620-230-320-302-530-302-3_V.mp3)
  Segment 45: 178856ms ~ 184220ms (file: 231-213-276-236-231-271-132-123-132-153-123-231-231-216-213-217-765-267-217-301-763-627-620-206-260-301-310-132-130-317-751-713-371-375-107-173-326-130-321-627-267-627-203-715-7_W.mp3)
  Segment 46: 185220ms ~ 185796ms (file: 517-501-501-517-571-501-507-510-501-501-510-576-571-517-571-576-567-516-567-576-576-574 507-510-570-507-501-510-501-517-501-507-501-501-510-501-510-510-510-501-516-517-567-567-5_W.mp3)
  Segment 47: 186796ms ~ 187372ms (file: 127-576-756-265-257-275-753-765-752-267-237-237-362-362-135_V.mp3)
  Segment 48: 188372ms ~ 188948ms (file: 316-257-507-265-245-267-532-375-362-265-756-123-576-756-237-752-261-317-756-162-562-315-367-275-603-162-317-136-613-361-136-507-571-570-573-574-576-753_V.mp3)
  Segment 49: 189948ms ~ 190524ms (file: 103-310 316-136-314-301-134-137-135-103-136-136-136-103-136-316-130-103-315-136 136-314-316-143-136-316-134-315-136-136-135-136-310-103-103-301-315-571-735-715-573-573 315-341-3_V.mp3)
  Segment 50: 191524ms ~ 192100ms (file: 657-132-316-765-763-632-623-216-261-213-123-321-607-631-136-670-716-705-760-706-316_V.mp3)
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
> **Time**: 2026-04-05 06:27:46
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "안녕하세요 만나서 반갑습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 263,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 263, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "만나서", "start_ms": 1650, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 2600, "dur_ms": 1100, "phase_lr_deg": 0.0}
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
"transcript": "오늘 날씨가 정말 화창하고 좋네요 산책하기 딱 좋은 날씨입니다",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 236,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 236, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 1500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "화창하고", "start_ms": 2100, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 3100, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "산책하기", "start_ms": 4200, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "딱", "start_ms": 5400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 5800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "날씨입니다", "start_ms": 6400, "dur_ms": 1200, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "외국어 공부를 할 때는 매일 조금씩이라도 꾸준히 하는 것이 가장 중요하다고 생각합니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 210,
"audio_type": "speech",
"words": [
{"word": "외국어", "start_ms": 210, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "공부를", "start_ms": 1000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 1700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "때는", "start_ms": 2100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "매일", "start_ms": 2800, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "조금씩이라도", "start_ms": 3500, "dur_ms": 1300, "phase_lr_deg": 0.0},
{"word": "꾸준히", "start_ms": 5000, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 5900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "것이", "start_ms": 6500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 7100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "중요하다고", "start_ms": 7800, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "생각합니다", "start_ms": 9000, "dur_ms": 1000, "phase_lr_deg": 0.0}
]
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
"transcript": "저는 요즘 한국 드라마를 보면서 한국어 표현을 익히고 있어요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 523,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 523, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "요즘", "start_ms": 1100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 1800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "드라마를", "start_ms": 2500, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "보면서", "start_ms": 3500, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "한국어", "start_ms": 4400, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "표현을", "start_ms": 5200, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "익히고", "start_ms": 6100, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 6900, "dur_ms": 600, "phase_lr_deg": 0.0}
]
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
"transcript": "언어는 단순히 단어를 외우는 것이 아니라 문화를 이해하는 과정입니다",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 627,
"audio_type": "speech",
"words": [
{"word": "언어는", "start_ms": 627, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "단순히", "start_ms": 1400, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "단어를", "start_ms": 2200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "외우는", "start_ms": 2900, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "것이", "start_ms": 3600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 4100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "문화를", "start_ms": 4800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "이해하는", "start_ms": 5500, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "과정입니다", "start_ms": 6400, "dur_ms": 1000, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "실제로 원어민과 대화해보는 것이 실력 향상에 가장 큰 도움이 됩니다",
"speaker": "speaker_2",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 231,
"audio_type": "speech",
"words": [
{"word": "실제로", "start_ms": 231, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "원어민과", "start_ms": 1000, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "대화해보는", "start_ms": 2000, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "것이", "start_ms": 3200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "실력", "start_ms": 3700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "향상에", "start_ms": 4300, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 5100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 5700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "도움이", "start_ms": 6100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "됩니다", "start_ms": 6800, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "틀리는 것을 두려워하지 말고 자신 있게 말하는 습관을 들이세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 520,
"audio_type": "speech",
"words": [
{"word": "틀리는", "start_ms": 520, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "것을", "start_ms": 1250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "두려워하지", "start_ms": 1800, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "말고", "start_ms": 3000, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "자신", "start_ms": 3700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "있게", "start_ms": 4300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "말하는", "start_ms": 4900, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "습관을", "start_ms": 5700, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "들이세요", "start_ms": 6600, "dur_ms": 800, "phase_lr_deg": 0.0}
]
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
"transcript": "반복해서 듣고 따라 하는 쉐도잉 학습법이 아주 효과적입니다",
"speaker": "speaker_2",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 310,
"audio_type": "speech",
"words": [
{"word": "반복해서", "start_ms": 310, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "듣고", "start_ms": 1250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "따라", "start_ms": 1800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 2400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "쉐도잉", "start_ms": 3000, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "학습법이", "start_ms": 3800, "dur_ms": 950, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 4900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "효과적입니다", "start_ms": 5500, "dur_ms": 1100, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "가장 좋아하는 한국 노래 가사를 외워보는 것도 좋은 방법이에요",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 102,
"audio_type": "speech",
"words": [
{"word": "가장", "start_ms": 102, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "좋아하는", "start_ms": 750, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 1600, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "노래", "start_ms": 2300, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "가사를", "start_ms": 2950, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "외워보는", "start_ms": 3750, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "것도", "start_ms": 4750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 5300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "방법이에요", "start_ms": 5900, "dur_ms": 900, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "The permanent yankee bank and don diso.",
"speaker": "speaker_1",
"confidence": 0.81,
"language": "en",
"silence_before_ms": 275,
"audio_type": "speech",
"words": [
{"word": "The", "start_ms": 275, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "permanent", "start_ms": 550, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "yankee", "start_ms": 1450, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "bank", "start_ms": 2200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "and", "start_ms": 2800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "don", "start_ms": 3250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "diso", "start_ms": 3750, "dur_ms": 600, "phase_lr_deg": 0.0}
]
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
"transcript": "포기하지 않고 끝까지 노력하면 반드시 좋은 결과가 있을 거예요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 167,
"audio_type": "speech",
"words": [
{"word": "포기하지", "start_ms": 167, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "않고", "start_ms": 1100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "끝까지", "start_ms": 1650, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "노력하면", "start_ms": 2500, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "반드시", "start_ms": 3500, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 4350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "결과가", "start_ms": 4950, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "있을", "start_ms": 5800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 6350, "dur_ms": 650, "phase_lr_deg": 0.0}
]
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
"transcript": "여러분의 한국어 공부를 진심으로 응원합니다 화이팅 하세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 326,
"audio_type": "speech",
"words": [
{"word": "여러분의", "start_ms": 326, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "한국어", "start_ms": 1200, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "공부를", "start_ms": 2000, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "진심으로", "start_ms": 2750, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "응원합니다", "start_ms": 3750, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "화이팅", "start_ms": 4900, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "하세요", "start_ms": 5750, "dur_ms": 700, "phase_lr_deg": 0.0}
]
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
"transcript": "오늘 수업은 여기까지입니다 다음 시간에 더 재미있는 주제로 만나요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 206,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 206, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "수업은", "start_ms": 750, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "여기까지입니다", "start_ms": 1500, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 2800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "시간에", "start_ms": 3400, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 4150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "재미있는", "start_ms": 4550, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "주제로", "start_ms": 5500, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 6350, "dur_ms": 650, "phase_lr_deg": 0.0}
]
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
"transcript": "안녕히 계세요 감사합니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 130, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "계세요", "start_ms": 850, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 1700, "dur_ms": 1100, "phase_lr_deg": 0.0}
]
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
"transcript": "공부하다가 모르는 부분이 있으면 언제든지 질문해 주세요",
"speaker": "speaker_2",
"confidence": 0.91,
"language": "ko",
"silence_before_ms": 574,
"audio_type": "speech",
"words": [
{"word": "공부하다가", "start_ms": 574, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "모르는", "start_ms": 1650, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "부분이", "start_ms": 2400, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "있으면", "start_ms": 3150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "언제든지", "start_ms": 3850, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "질문해", "start_ms": 4800, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 5600, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "Claude Whitaker has decreed 1512 season.",
"speaker": "speaker_1",
"confidence": 0.85,
"language": "en",
"silence_before_ms": 765,
"audio_type": "speech",
"words": [
{"word": "Claude", "start_ms": 765, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "Whitaker", "start_ms": 1300, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "has", "start_ms": 2100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "decreed", "start_ms": 2500, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "1512", "start_ms": 3300, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "season", "start_ms": 4500, "dur_ms": 650, "phase_lr_deg": 0.0}
]
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
"transcript": "그럼 모두 수고하셨습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 261,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 261, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "모두", "start_ms": 800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "수고하셨습니다", "start_ms": 1400, "dur_ms": 1200, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 06:27:46*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=11864  time=06:26:41  uptime=9022s  mem=251MB  system_processes=357"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_062513.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 751-756 570-571-571-756-157-715-751-157-571-751-571-573-571-751-517-750-750-571-501-157-157-715-751-571-576 570-570-570-751-170-157-175-710-715-150-715-175-175-517-105-751-571-5_W.mp3)
  Segment 2: 1576ms ~ 8920ms (file: 725-261-752-237-236-267-326-265-201-213-321-126-162-210-236-123-123-617-263-216-132-267-320-523-203-754-270-175-261-203-172-176-637-765-273-736-217-263-612-310-231-260-263-136-6_W.mp3)
  Segment 3: 9920ms ~ 15500ms (file: 751-213-372-721-271-721-310-130-731-271-571-735-132-753-317-713-370-237-127-371-731-137-713-731-310-301-123-726-107-213-213-102-170-671-160-136-712-176-271-572-172-170-170-170-1_V.mp3)
  Segment 4: 16500ms ~ 17076ms (file: 613-167-761-176-761-167-176-165-765-176-127-167-176-172-176-176-176-617-561-675-675-176-176-175-715-175-157-165-156-157-156-162-165-612-165-157-172-167-617-167-651-125-165-156-1_W.mp3)
  Segment 5: 18076ms ~ 32260ms (file: 327-156-175-657-213-123-102-132-103-602-216-132-103 261-206-106-301-203-201-203-206-103-213-206-201-206-621-103-203-263-607-602-206-260-126-231-126-120-574-103-261-132-301-206-2_V.mp3)
  Segment 6: 33260ms ~ 43232ms (file: 276-267-132-327-230-273-237-627-230-327-712-102-260-756-275-174-143-120-230-237-123-123-302-102-103-134-162-541-143-370-231-273-273-241-312-132-123-164-163-123-765-753-320-132-3_V.mp3)
  Segment 7: 44232ms ~ 52188ms (file: 213-230-203-231-107-102-120-231-537-507-756-752-120-237-210-231-105-132-102-210-123-103-261-236-102-206-576-156-276-756-765-270-210-126-125-237-107-216-261-613-103-750-236-120-2_W.mp3)
  Segment 8: 53188ms ~ 60352ms (file: 237-103-237-132-132-103-135-123-321-213-231-216-102-130-103-751-765-162-321-321-627-572-526-312-230-752-231-532-132-102-213-672-276-527-326-572-157-765-132-236-762-267-126-132-3_W.mp3)
  Segment 9: 61352ms ~ 67112ms (file: 231-216-321-270-574-210-120-260-206-320-267-265-265-625-216-206-260-261-562-215-260-625-276-231-263-265-230-302-623-620-106-321-103-263-267-236-256-265-102-210-261-216-261-625-5_W.mp3)
  Segment 10: 68112ms ~ 68688ms (file: 175-107-107-107 157-153-105-173-175-175-715-173-713-103-130 153-157-105-107-105-107-175-107-175-107-107-173-175_V.mp3)
  Segment 11: 69688ms ~ 70264ms (file: 134-123-132-136-132-326-163-136-163-673-736-672-312-103-103-731-137-132 163-176-123-136-301-321-203-263-326-136-316-136-132-136-203-132-715-765-675-267-620-261-126-360-576-753-6_V.mp3)
  Segment 12: 71264ms ~ 71840ms (file: 621-621-621-621-261-261-261-261-625-263-621-621-623-621-621-625-625-625-625-652-625-657-625-652-627-625-625 621-621-261-621-261-621-621-261-620-625-261-621-621-620-260-625-265-6_W.mp3)
  Segment 13: 72840ms ~ 73416ms (file: 675-627-672-627-672-675-672-621-631-621-362-623-162 312-123-103-132-132-132_W.mp3)
  Segment 14: 74416ms ~ 74992ms (file: 213-236-132-132-125-321-315-217-512-521-712-712-537-573-513-351-573-521-167-152-617-675-625-625-567-261-162-572-576-751-754-736-761-172-217-217-175-725-725-732-537-352-325-235-3_W.mp3)
  Segment 15: 75992ms ~ 76568ms (file: 610-612-621 162 263-236-625-672-672 671-671-672-130-231-203-261-612-612-612-312-612-612-672-362_W.mp3)
  Segment 16: 77568ms ~ 78144ms (file: 173-103-107-173-137-173-173-317-137-107-103-137-103-173 315-513-513-153-173-157-517-157-157 157-153-357-135-153-135-153-513-157-153-513-157-153 751-375-173-317-371-317-137-315-3_W.mp3)
  Segment 17: 79144ms ~ 79720ms (file: 510-157-165-175-175-175-175-157-175-157-517-156-571-651-156-157-516-176-175-157-175-756 157-150-157-105-150-157-150-105-751-571-516-157-157-571-561-516-165-156-156-157-571-157-1_W.mp3)
  Segment 18: 80720ms ~ 81296ms (file: 721-172-723-526-752-756-752-572-752-572-572-752-132-103-103-103-103-315-135-103-103-165-106-103-106-102-167-152-103-103-106-136-106-132-126-103-136-103-103-106-103-163-176-143-6_V.mp3)
  Segment 19: 82296ms ~ 82872ms (file: 105-157-170-107-170-107-170-107-170-170-107-170-175-157-175-750-571-517-715-750-751-750-751-570-571 175-107-107-170-107-107-170-107-105-107-170-107-170-107-175-170-715-157-175-7_V.mp3)
  Segment 20: 83872ms ~ 89560ms (file: 613-351-351-351-356-316-316-301-316-617-376-316-103-570-207-137-130-260-302-267-201-572-105-210-130-527-627-375-216-632-260-257-231-210-201-132-123-132-532-231-203-201-207-206-2_V.mp3)
  Segment 21: 90560ms ~ 91136ms (file: 132-637-532-657-576-567-236-623-326-612-261 163-123-516-132-263-623-612-623-132-621-312-263-321-621-321 132_W.mp3)
  Segment 22: 92136ms ~ 92712ms (file: 176-756-175-165-617-176-156-167-175-176-175-137 146-154-105-157-105-175-170-176-157-176-715-165-157-756-156-175-715-657-165-567-157-165-157-176-175 153-105-150-150-150-105-150-1_V.mp3)
  Segment 23: 93712ms ~ 94288ms (file: 310-302-352-320-230-321-132-216-123-612-621-621-103-106-130-561-620-263-261-251-201-103-102-123-317-632-263-326-316-106-316-163-102-130 320-326-230-320-230-320-756-675-572-576-5_W.mp3)
  Segment 24: 95288ms ~ 95864ms (file: 574-756-756-756-756-751-756-756-756-756-756-756-751-756-756 105-150-150-105-105-105-175-150-157-157-150-150-501-510 105-150-150-150-105-105-150-105-105-170-150-150-106-107-571 1_V.mp3)
  Segment 25: 96864ms ~ 102516ms (file: 716-261-607-367-761-762-762-273-275-271-271-267-237-271-207-217-721-270-327-732-376-375-201-210-231-321-210-123-132-123-123-231-136-132-103-160-163-561-653-361-365-156-625-562-2_W.mp3)
  Segment 26: 103516ms ~ 104092ms (file: 150-150-150-571-157-175-175-175-175-715-175-715-170-170-175-176-175-176-173-176-174 105-175-150-157-175-170-175-107-176-107-167-176-170-701-170-715-175-175-157-715-175-175-175-1_V.mp3)
  Segment 27: 105092ms ~ 105668ms (file: 756 312-312-126-750-267-326-523-752-312-257-267-263-264-267-217-413 275-307-631-312-316-310-316-316-316-173-731-136-316-632-765-576-756-765-756-675-367-763-765-736-367-736-307-3_W.mp3)
  Segment 28: 106668ms ~ 107244ms (file: 107-130-310-170-107-107-731-137-307-310-175-107-107-130-137-130-750-730-571-130-175-731-173-310-175-175-730-710 301-107-301-103-701-713-137-137-170-301-310-371-710-173-703-715-1_V.mp3)
  Segment 29: 108244ms ~ 114508ms (file: 705-731-273-731-721-276-263-201-210-216-213-301-127-721-172-327-270-267-237-573-537-725-756-137-271-213-126-217-216-271-123-317-372-273-213-723-213-132-120-312-132-123-172-712-2_W.mp3)
  Segment 30: 115508ms ~ 116084ms (file: 501-507-157-510-105-157-175-573 570-510-510-507-507-510-157-150-715-510-157-105-105-157-105-153-107-570-750-517-150-507-150-103-570-517-150-571-150-510-105-517-571-157 510-571-5_W.mp3)
  Segment 31: 117084ms ~ 117660ms (file: 715-735-571-570-750-571-517-571-571-751-571-517-517-570-570-571-570-570-570-571-573-517 571-153-715-175-751-751-571-513-571-750-571-570-576-570-751-571-571-571-571-571-576-576-5_V.mp3)
  Segment 32: 118660ms ~ 119236ms (file: 302-231-621-725-750-756-316-754-756-317-310-310-321-312-263-310-312-263-206-236-320-302-106-260-263-267-610-260-260-231-302-301-310-316-103-623-602-610-320-320-123-301-163-210-2_W.mp3)
  Segment 33: 120236ms ~ 120812ms (file: 137-315-132-271-125-513-532-701-621-123-502-153-123-167-123-132-725-527-126-176-235-132-125-275-326-725-560-576-175-567-236-276-570-312-137-132-163-310-137-135-175-375-326-135-5_V.mp3)
  Segment 34: 121812ms ~ 122388ms (file: 613-217-103-103-163-102-123-136-167-136-160-615-236-612-312-163-615-231-623-160-137-263 132-321-365 102 675-632-130-103-671-316-673-106-165-167-621-312-563-312-106-106-103-312-6_V.mp3)
  Segment 35: 123388ms ~ 123964ms (file: 706-312-157-267-136-132-126-721-236-253-230-213-756-572-271-756-235-725-362_V.mp3)
  Segment 36: 124964ms ~ 136808ms (file: 326-120-123-132-105-231-267-267-217-276-132-310-106-213-321-321-136-276-132-102-216-130-273-231-321-362-321-213-106-103-163-102-132-671-230-260-206-236-103-102-213-125-120-106-1_W.mp3)
  Segment 37: 137808ms ~ 138384ms (file: 316-657-176-371-176-136-167-173 730-730-301-307-315-175-173-106-607-607-316-613-316-130-130-107-130-317-173-371-173-310-173-301-317-137-310-107-175-371 701-307-735-307-713-137-1_V.mp3)
  Segment 38: 139384ms ~ 139960ms (file: 602-612-610-106-621-671-167-106-106-167-617-601-617-160-167-106-162-106-160-612-620-126-620-263-123-126-615-615-615-615-672-621-621-613-163-136-203-106-106-750-750-103-120-236-6_V.mp3)
  Segment 39: 140960ms ~ 141536ms (file: 176-132-237-321-621-126-103-231-132-105-106-162-267-267-675-576-725-752-127-156-756-706-652-237-372-230-120-132-312-301-327-320-320-375-307-326-265-652-702-732-357-302-136-130-1_W.mp3)
  Segment 40: 142536ms ~ 148368ms (file: 267-263-710-170-701-127-312-763-316-672-267-627-627-627-261-203-102-132-102-176-627-136-716-127-761-672-236-237-231-137-317-160-132-163-632-306-623-673-103-103-176-617-106-231-2_W.mp3)
  Segment 41: 149368ms ~ 149944ms (file: 571-751-571-751-751-157-571 135-501-105-517-135-135-135-315-135-175-150-517-513-517-571-517-157-157-157-175-175-751-175 135-130-135-510-135-103-135-317-135-103-513-157-517-517-5_V.mp3)
  Segment 42: 150944ms ~ 151520ms (file: 501-513-517-157-501-175-157-571-751-137-716-175-715-576-571-517-571-571-571-157-517-157-517-517-517-571 150-150-501-501-150-507-501-150-510-150-517-517-157-153-570-571-571-571-7_V.mp3)
  Segment 43: 152520ms ~ 153096ms (file: 627-623-623-623-612-567-567-561-576-657-576-675-576-567-567-576-567-675-671_W.mp3)
  Segment 44: 154096ms ~ 154672ms (file: 154-105-105-175-105-175-504-157 504-507-504-547-574-504-154-157-705-105-105-157 570-574-574-547-574-517-510-574-570-574-574-574-571 510-574-574-574-574-570-571-510-570-517-510-5_W.mp3)
  Segment 45: 155672ms ~ 156248ms (file: 231-213-215-237-237-237-213-231-230-321-326-321-321-312-316-623-621-612-216-216-723-327-312-231-531-523-521-512-521-261-265-216-235-321-312-312-362-236-326-326-326-361-362-361-5_W.mp3)
  Segment 46: 157248ms ~ 157824ms (file: 307-372-132-207-370-753-312-132-752-127-105-157-217-203-317-316-637-127-175-276-302-127-132-507-176-123-321-263-162-132-731-137-312-321-132-123-173-102-103-572-623-206-627-576-6_V.mp3)
  Segment 47: 158824ms ~ 159400ms (file: 312-317-312-763-362-312-132-123-371-137-173-732-752-173-132-170-135-163-671-612-276-123-321-301-230-320-321-231-132 671 675-567-672-726-671-672-673 231-217-237-231-762-267-621-1_W.mp3)
  Segment 48: 160400ms ~ 168968ms (file: 726-571-230-230-756-103-102-176-207-236-537-547-267-132-231-213-237-201-270-542-213-103-230-130-120-301-310-236-237-230-103-310-321-206-672-230-576-321-130-107-267-276-762-267-7_W.mp3)
  Segment 49: 169968ms ~ 175332ms (file: 312-130-107-276-213-321-312-612-613-621-167-267-267-261-316-731-316-316-173-137-316-367-317-316-361-317-362-312-316-361-316-317-612-136-163-163-136-132-673-360-361-367-367-326-2_W_stt_some_public_and_to_me,_and_it_has_80_she.mp3)
  Segment 50: 176332ms ~ 179428ms (file: 213-236-126 103-210-610-321-261-326-316-136-126-321-173 137-103-102 157-156-301-501-132 172-756-651-706-361-326-163 265-251-310-123-721-362-621-316-621-651_V.mp3)
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
> **Time**: 2026-04-05 06:27:46
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "안녕하세요 만나서 반갑습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 263,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 263, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "만나서", "start_ms": 1650, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 2600, "dur_ms": 1100, "phase_lr_deg": 0.0}
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
"transcript": "오늘 날씨가 정말 화창하고 좋네요 산책하기 딱 좋은 날씨입니다",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 236,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 236, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 1500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "화창하고", "start_ms": 2100, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 3100, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "산책하기", "start_ms": 4200, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "딱", "start_ms": 5400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 5800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "날씨입니다", "start_ms": 6400, "dur_ms": 1200, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "외국어 공부를 할 때는 매일 조금씩이라도 꾸준히 하는 것이 가장 중요하다고 생각합니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 210,
"audio_type": "speech",
"words": [
{"word": "외국어", "start_ms": 210, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "공부를", "start_ms": 1000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 1700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "때는", "start_ms": 2100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "매일", "start_ms": 2800, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "조금씩이라도", "start_ms": 3500, "dur_ms": 1300, "phase_lr_deg": 0.0},
{"word": "꾸준히", "start_ms": 5000, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 5900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "것이", "start_ms": 6500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 7100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "중요하다고", "start_ms": 7800, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "생각합니다", "start_ms": 9000, "dur_ms": 1000, "phase_lr_deg": 0.0}
]
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
"transcript": "저는 요즘 한국 드라마를 보면서 한국어 표현을 익히고 있어요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 523,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 523, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "요즘", "start_ms": 1100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 1800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "드라마를", "start_ms": 2500, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "보면서", "start_ms": 3500, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "한국어", "start_ms": 4400, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "표현을", "start_ms": 5200, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "익히고", "start_ms": 6100, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 6900, "dur_ms": 600, "phase_lr_deg": 0.0}
]
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
"transcript": "언어는 단순히 단어를 외우는 것이 아니라 문화를 이해하는 과정입니다",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 627,
"audio_type": "speech",
"words": [
{"word": "언어는", "start_ms": 627, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "단순히", "start_ms": 1400, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "단어를", "start_ms": 2200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "외우는", "start_ms": 2900, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "것이", "start_ms": 3600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 4100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "문화를", "start_ms": 4800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "이해하는", "start_ms": 5500, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "과정입니다", "start_ms": 6400, "dur_ms": 1000, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "실제로 원어민과 대화해보는 것이 실력 향상에 가장 큰 도움이 됩니다",
"speaker": "speaker_2",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 231,
"audio_type": "speech",
"words": [
{"word": "실제로", "start_ms": 231, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "원어민과", "start_ms": 1000, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "대화해보는", "start_ms": 2000, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "것이", "start_ms": 3200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "실력", "start_ms": 3700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "향상에", "start_ms": 4300, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 5100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 5700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "도움이", "start_ms": 6100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "됩니다", "start_ms": 6800, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "틀리는 것을 두려워하지 말고 자신 있게 말하는 습관을 들이세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 520,
"audio_type": "speech",
"words": [
{"word": "틀리는", "start_ms": 520, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "것을", "start_ms": 1250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "두려워하지", "start_ms": 1800, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "말고", "start_ms": 3000, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "자신", "start_ms": 3700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "있게", "start_ms": 4300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "말하는", "start_ms": 4900, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "습관을", "start_ms": 5700, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "들이세요", "start_ms": 6600, "dur_ms": 800, "phase_lr_deg": 0.0}
]
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
"transcript": "반복해서 듣고 따라 하는 쉐도잉 학습법이 아주 효과적입니다",
"speaker": "speaker_2",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 310,
"audio_type": "speech",
"words": [
{"word": "반복해서", "start_ms": 310, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "듣고", "start_ms": 1250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "따라", "start_ms": 1800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 2400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "쉐도잉", "start_ms": 3000, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "학습법이", "start_ms": 3800, "dur_ms": 950, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 4900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "효과적입니다", "start_ms": 5500, "dur_ms": 1100, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "가장 좋아하는 한국 노래 가사를 외워보는 것도 좋은 방법이에요",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 102,
"audio_type": "speech",
"words": [
{"word": "가장", "start_ms": 102, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "좋아하는", "start_ms": 750, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 1600, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "노래", "start_ms": 2300, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "가사를", "start_ms": 2950, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "외워보는", "start_ms": 3750, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "것도", "start_ms": 4750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 5300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "방법이에요", "start_ms": 5900, "dur_ms": 900, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "The permanent yankee bank and don diso.",
"speaker": "speaker_1",
"confidence": 0.81,
"language": "en",
"silence_before_ms": 275,
"audio_type": "speech",
"words": [
{"word": "The", "start_ms": 275, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "permanent", "start_ms": 550, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "yankee", "start_ms": 1450, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "bank", "start_ms": 2200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "and", "start_ms": 2800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "don", "start_ms": 3250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "diso", "start_ms": 3750, "dur_ms": 600, "phase_lr_deg": 0.0}
]
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
"transcript": "포기하지 않고 끝까지 노력하면 반드시 좋은 결과가 있을 거예요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 167,
"audio_type": "speech",
"words": [
{"word": "포기하지", "start_ms": 167, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "않고", "start_ms": 1100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "끝까지", "start_ms": 1650, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "노력하면", "start_ms": 2500, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "반드시", "start_ms": 3500, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 4350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "결과가", "start_ms": 4950, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "있을", "start_ms": 5800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 6350, "dur_ms": 650, "phase_lr_deg": 0.0}
]
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
"transcript": "여러분의 한국어 공부를 진심으로 응원합니다 화이팅 하세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 326,
"audio_type": "speech",
"words": [
{"word": "여러분의", "start_ms": 326, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "한국어", "start_ms": 1200, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "공부를", "start_ms": 2000, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "진심으로", "start_ms": 2750, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "응원합니다", "start_ms": 3750, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "화이팅", "start_ms": 4900, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "하세요", "start_ms": 5750, "dur_ms": 700, "phase_lr_deg": 0.0}
]
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
"transcript": "오늘 수업은 여기까지입니다 다음 시간에 더 재미있는 주제로 만나요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 206,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 206, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "수업은", "start_ms": 750, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "여기까지입니다", "start_ms": 1500, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 2800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "시간에", "start_ms": 3400, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 4150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "재미있는", "start_ms": 4550, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "주제로", "start_ms": 5500, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 6350, "dur_ms": 650, "phase_lr_deg": 0.0}
]
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
"transcript": "안녕히 계세요 감사합니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 130, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "계세요", "start_ms": 850, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 1700, "dur_ms": 1100, "phase_lr_deg": 0.0}
]
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
"transcript": "공부하다가 모르는 부분이 있으면 언제든지 질문해 주세요",
"speaker": "speaker_2",
"confidence": 0.91,
"language": "ko",
"silence_before_ms": 574,
"audio_type": "speech",
"words": [
{"word": "공부하다가", "start_ms": 574, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "모르는", "start_ms": 1650, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "부분이", "start_ms": 2400, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "있으면", "start_ms": 3150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "언제든지", "start_ms": 3850, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "질문해", "start_ms": 4800, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 5600, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "Claude Whitaker has decreed 1512 season.",
"speaker": "speaker_1",
"confidence": 0.85,
"language": "en",
"silence_before_ms": 765,
"audio_type": "speech",
"words": [
{"word": "Claude", "start_ms": 765, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "Whitaker", "start_ms": 1300, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "has", "start_ms": 2100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "decreed", "start_ms": 2500, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "1512", "start_ms": 3300, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "season", "start_ms": 4500, "dur_ms": 650, "phase_lr_deg": 0.0}
]
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
"transcript": "그럼 모두 수고하셨습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 261,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 261, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "모두", "start_ms": 800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "수고하셨습니다", "start_ms": 1400, "dur_ms": 1200, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 06:27:46*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=24432  time=06:26:41  uptime=15672s  mem=418MB  system_processes=357"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_061739.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 501 502-173-756-256-327-273-127-123-132-612-612-621-617-672-673-627-602-537-612-705-265-263-261_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 517-517-571-715-150-153-715-157-715-173-157-157-175-175-150-175-157-571 517-517-517-517-570-571-157-751-571-571-571-576 517-517-517-751-137-157-710-150-710-157-173-715-175-173-1_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 175-175-175-157-175-175-156-156-157-756-756 170-571-157-170-170-175-175-175-175-715-175-751-571-175-715-751-571-756-576 105-150-157-571-157-175-715-175-175-175-751-175-750-715-5_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 103-134-103-517-571-517-567-157-156-512-152-516-561-651-156-156-125-152-125-152-153-150 751-571-751-571-567-516-567-156-102-125-165-261-152-126-130-103-132-517-517-751-571-576-5_W.mp3)
  Segment 5: 6304ms ~ 13288ms (file: 753-135-713-765-276-236-267-152-512-123-132-130-120-263-261-237-132-312-216-230-236-231-371-325-231-756-263-320-321-132-130-132-103-301-576-735-673-126-102-163-675-123-312-321-3_W.mp3)
  Segment 6: 14288ms ~ 19796ms (file: 321-261-362-276-371-236-271-160-376-253-361-321-130-276-362-261-261-621-260-632-312-261-263-310-275-631-312-326-267-263-261-326-236-263-263-261-537-216-216-162-321-106-501-576-5_V.mp3)
  Segment 7: 20796ms ~ 21372ms (file: 670-670-765-672-762-673-301-137-136-765-506-675-136-163-631-362-371-310-130-103-317-361-306-103-502-507-310-316-163-321-103-130-316-576-576-675-763-765-762-206-623-267-602-106-1_V.mp3)
  Segment 8: 22372ms ~ 29572ms (file: 527-620-572-572-657-726-752-576-756-574-507-750-625-135-751-627-507-567-257-570-675-576-716-721-527-652-657-567-137-136-136-130-136-367-673-621-637-765-561-657-576-617-617-657-6_W.mp3)
  Segment 9: 30572ms ~ 31148ms (file: 317-137-135-315-123-570-217-167-157-672-367-375-136-763-375-627 576-150-574-574-576-176-756-103-162-157-132-162-753-537-756-326-312-316-361-136-167-137-316 317-137-316-317-163-1_W.mp3)
  Segment 10: 32148ms ~ 32724ms (file: 612-126-201-123-120-316-321-260-620-260-260-321-310-312-603-362-370-301-301-137-216-231-103-132-162-130-103-362-132-103-130-103-132-162-612-651-612-621-652-652-652-310-135-230-3_V.mp3)
  Segment 11: 33724ms ~ 40276ms (file: 765-230-320-576-321-310-765-756-736-327-123-203-672-260-265-327-123-571-107-132-153-137-132-123-765-756-375-320-207-172-237-276-732-306-762-157-574-572-537-576-157-657-762-103-3_W.mp3)
  Segment 12: 41276ms ~ 46388ms (file: 236-231-137-321-231-231-263-231-267-267-761-104-267-137-312-231-735-756-671-675-627-237-671-102-517-213-571-310_W.mp3)
  Segment 13: 47388ms ~ 47964ms (file: 320-312-317-173-137-173-173-317_W.mp3)
  Segment 14: 48964ms ~ 53716ms (file: 261-276-263-276-267-123-312-237-231-271-213-130-261-267-236-236-267-132-137-231-321-130-231-216-527-132-132-236-267-321-236-216-276-132-210-321-231-753-316-130-312-574-152-631-6_W.mp3)
  Segment 15: 54716ms ~ 55292ms (file: 176-716-671-632-672-671-672-632-673-675-672-652-625-652-625-652-625-625-623-632-621-652-625-672-615-615-617_W.mp3)
  Segment 16: 56292ms ~ 56868ms (file: 541-517 105-510-154-510-150-150-157-150-510-510-517-157-157-157-517-150-570 105-105-105-105-150-150-510-517-571-571-571-571-574-571-574-570-574-570-574-570-571-574-570-570_W.mp3)
  Segment 17: 57868ms ~ 58444ms (file: 507-152-135-517-157-150-571-157-517-751-175-157-753-571-571-157-571-517-517-715-175-517-571-537-517-157-571-571-573-573-571-157-513-130-175-715-157-135-713-157-715-571-756-175-1_V.mp3)
  Segment 18: 59444ms ~ 60020ms (file: 170-175-750-571-715-751-710-750-170-715-175-175-750-170 175-715-170-710-175-701-170-710-170-701-701-710-170-170-315-715-157-153-573-750 754-754-754-574-754-715-175_V.mp3)
  Segment 19: 61020ms ~ 61596ms (file: 672-570-652-526-725-627-623-756-756 750-672-537-570-263-270-127-756-267-756-152-176-106-130-316-173-276-527-207-672-235-502-760-235-257-275-627-275-657-236-652-605-720-312-652-2_W.mp3)
  Segment 20: 62596ms ~ 63172ms (file: 173-301-310-731-310-301-371-317-312-301-301-310-317-137-130-137-103-130-173-306-130-130-170-307-317-723 705-750-137-107-371-370-607-160-607-607-670-670-610-671-637-670-675-672-6_V.mp3)
  Segment 21: 64172ms ~ 64748ms (file: 162-560-157-361-671-361-137-136-362-627-167-675-165-362-613-602-103-236 132_V.mp3)
  Segment 22: 65748ms ~ 66324ms (file: 751-715-175-715-751-570-571 750-750-750-751-157-751-571-571-157-751-751-750-570-751-715-715-175-715-715-175-751 750-751-750-571-570-571-175-571-517-517-750-175-571-751-715-751-7_W.mp3)
  Segment 23: 67324ms ~ 67900ms (file: 765 657-760-763-761-271-167-376-637-236-632-513-762-321-231-321-761-723-102-162-132-162-162-163-162-612-123-172-267-241-264-623-126-162-126-132 607-670-167-176-106-167-134-132-1_V.mp3)
  Segment 24: 68900ms ~ 69476ms (file: 574-574-574-574-547-574-574-574-574-574 574-574-576-576-547-574-574-570-574-547-574-574-574-547-574-547-574-574-574-574-512-356-517-576-574-514-576-547-574-574-574-574-547-574-5_W.mp3)
  Segment 25: 70476ms ~ 71052ms (file: 517-157-175-571-153-517-571-517-571-571 570-571-157-150-573-571-157-751-750-175-751-517-751-570-157-153-173-175-715-513-175-573-571-175-507 571-570-157-157-175-157-517-137-571-1_V.mp3)
  Segment 26: 72052ms ~ 72628ms (file: 517-574-517-570-514-574-574 153-507-105-501-517-105-157-501-517-571-510-507-501-105-150-570-105-570-571-157-517-573-357-315-531-153-517-157-517 150-105-510-105-105-150-510-507-5_V.mp3)
  Segment 27: 73628ms ~ 74204ms (file: 750-751-706-176-713-571-715-175-705-751-715-573-715-175-715-751-715-753-751-753 571-517-715-175-715-170-107-175-715-753-713-750-175-175-715-710-715-570-751-173-570 750-750-756-7_V.mp3)
  Segment 28: 75204ms ~ 75780ms (file: 257 570-751-571-574-571-571-571-573-576-576-576-573-571-175-150-135-150-571-574-570-572-153 570-573-756-751-571-571-571-571-751-574-574-574-571-571_W.mp3)
  Segment 29: 76780ms ~ 87940ms (file: 135-103-312-132-371-312-327-137-137-317-317-265-326-326-231-367-362-312-752-321-306-231-231-231-103-103-103-132-103-127-172-172 750-136-263-362-637-752-271-217-103-265-216-231-2_W.mp3)
  Segment 30: 88940ms ~ 97796ms (file: 213-326-326-236-123-301-573-762-130-230-123-216-236-126-132-326-236-236-206-216-127-201-321-265-625-267-372-126-130-137-160-167-261-263-261-260-127-176-207-271-217-207-231-231-2_W.mp3)
  Segment 31: 98796ms ~ 108984ms (file: 261-216-261-136-123-321-321-267-263-123-132-321-321-326-236-275-175-102-236-672-617-675-261-106-567-276-176-123-102-216-120-260-123-102-671-107-627-260-132-216-231-321-263-213-1_W.mp3)
  Segment 32: 109984ms ~ 122692ms (file: 132-132-132-165-517-517-751-157-127-163-160-163-160-160-765-675-310-237-260-157-216-236-126-143-132-270-167-675-615-351-756-126-312-321-310-130-310-706-756-617-176-123-310-213-1_W.mp3)
  Segment 33: 123692ms ~ 131864ms (file: 267-207-267-206-206-627-132-260-206-756-321-132-120-570-276-270-261-765-572-250-261-275-317-235-137-260-302-325-275-265-507-527-327-537-103-173-574-130-132-326-267-236-302-320-2_W.mp3)
  Segment 34: 132864ms ~ 133440ms (file: 576-726-357-267-567-152-572-375-237-712-250-763-573-573-236-237-132-276-327-543-213-372-527-276-175-237-276-572-327-237-572-273-257-267-265-267_V.mp3)
  Segment 35: 134440ms ~ 135016ms (file: 576-765-736-756-753-576-576-570 753-351-570-751-570-506-750-570-570-570-571-751-571-735-561-536-576-517-351-513-732-356-317-537-315-376-756-367-756-753-576 310-301-310-312-316-3_V.mp3)
  Segment 36: 136016ms ~ 139616ms (file: 627-267-267-627-267-672-762-163-217-627-267-216-736-723-216-123-627-321-601-612-126-162-326-762-276-716-126-126-321-167-123-261-231-216-762-132-321-126-621-762-716-167-231-216-2_W.mp3)
  Segment 37: 140616ms ~ 147312ms (file: 213-762-123-621-756-103-267-576-163-765-132-725-756-321-216-623-325-236-136-230-236-263-236-231-263-136-130-130-321-231-132-731-103-261-216-267-236-231-572-263-162-506-120-765-1_W.mp3)
  Segment 38: 148312ms ~ 148888ms (file: 576-517-751-715-715-571-571-576-571-507-507-501-510-507-507-501-510-507-507-506-501-517-571-175-756-751-750-756-750-753-753-751-753-573-751-571-571-576-571-751-501-517-501-510-5_V.mp3)
  Segment 39: 149888ms ~ 157808ms (file: 106-726-126-327-267-267-260-261-120-127-106-134-261-627-276-120-310-231-312-230-213-276-726-276-276-236-130-230-236-312-312-132-210-217-237-765-162-126-312-273-172-173-137-320-2_W.mp3)
  Segment 40: 158808ms ~ 169716ms (file: 176-132-136-617-617-327-136-362-732-760-765-760-761-716-137-314-327-136-103-132-675-670-547-547-574-136-231-613-136-103-106-106-136-601-276-623-326-610-172-103-130-130-126-102-1_V.mp3)
  Segment 41: 170716ms ~ 180256ms (file: 756-136-762-267-753-762-327-754-576-276-216-217-162-627-756-762-236-260-120-236-754-275-267-102-310-302-130-237-267-176-120-126-576-576-675-273-320-610-672-276-125-310-716-627-2_W.mp3)
  Segment 42: 181256ms ~ 185720ms (file: 123-267-623-126-627-362-621-610-130-267-267-261-263-261-623-621-267-260-261-203-267-126-210-206-271-310-632-562-135-627-762-612-672-206-621-267-206-267-261-260-321-106-261-621-1_W.mp3)
  Segment 43: 186720ms ~ 194064ms (file: 132-106-123-123-567-576-572-527-320-203-203-321-230-137-210-312-176-127-132-103-317-106-563-657-562-612-613-130-201-207-302-237-230-152-176-627-316-160-302-167-106-162-561-136-1_W.mp3)
  Segment 44: 195064ms ~ 195640ms (file: 163-106-127-132-516-236-261-620-316-625-362-163-130-310-321-260-236-106-103-162-612-612-162-612-621-162-672-657-675-672-672-765-761-675-716-617-162-160-126-261-136-216-261-216-1_W.mp3)
  Segment 45: 196640ms ~ 197216ms (file: 134-103-413-140-145-143-104-145-134-105-314-134-103-103-103-143 734-473-143-143-301-405-140-401-401-145-105-147-174-145-401-147-140-105-103-105-104-134-143-314-104-134-413-130-1_V.mp3)
  Segment 46: 198216ms ~ 198792ms (file: 162-126-670-670-671-670-627-623-237-237-267-267-263-267-261-267-276-267-267-672-674-671-367-617-316-216-236-231-267-123-312-321-316-762-136-316-361-632-361-631-326-312-632-623-5_W.mp3)
  Segment 47: 199792ms ~ 200368ms (file: 705 701-317-710-713-310-315-317-735-173-103-317-731-317-103-103-130-173-751-705-375-735-130-103-170-735-703-731-703-370-317-310 135-705-310-301-135-715-107-136-630-631-361-613-3_V.mp3)
  Segment 48: 201368ms ~ 201944ms (file: 134-413-134-103-134-103-134-103-134-314-143-104 475-345-435-143-304-301-130-103-104-103-431-403-145-310-134-341-134-310-340-310-314-104-143-140-104-310-130-104-130-104-103-403 4_W.mp3)
  Segment 49: 202944ms ~ 208200ms (file: 672-261-123-120-213-716-160-106-763-301-130-756-107-175-207-270-273-273-137-270-270-260-326-203-236-263-327-376-627-627-137-132-312-130-317-317-103-326-123-317-167-126-672-570-2_W.mp3)
  Segment 50: 209200ms ~ 209776ms (file: 706-540-572-761-173-312-132-671-157-162-726-205-236-570-527-253-725-275-570-502-527-560-276-275-372-513-572-675-726-236-237-570-165-253-570-526-527-312-267 275 536-257-321-573-2_V.mp3)
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
> **Time**: 2026-04-05 06:27:47
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "안녕하세요 만나서 반갑습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 263,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 263, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "만나서", "start_ms": 1650, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 2600, "dur_ms": 1100, "phase_lr_deg": 0.0}
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
"transcript": "오늘 날씨가 정말 화창하고 좋네요 산책하기 딱 좋은 날씨입니다",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 236,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 236, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 1500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "화창하고", "start_ms": 2100, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 3100, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "산책하기", "start_ms": 4200, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "딱", "start_ms": 5400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 5800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "날씨입니다", "start_ms": 6400, "dur_ms": 1200, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "외국어 공부를 할 때는 매일 조금씩이라도 꾸준히 하는 것이 가장 중요하다고 생각합니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 210,
"audio_type": "speech",
"words": [
{"word": "외국어", "start_ms": 210, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "공부를", "start_ms": 1000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 1700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "때는", "start_ms": 2100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "매일", "start_ms": 2800, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "조금씩이라도", "start_ms": 3500, "dur_ms": 1300, "phase_lr_deg": 0.0},
{"word": "꾸준히", "start_ms": 5000, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 5900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "것이", "start_ms": 6500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 7100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "중요하다고", "start_ms": 7800, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "생각합니다", "start_ms": 9000, "dur_ms": 1000, "phase_lr_deg": 0.0}
]
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
"transcript": "저는 요즘 한국 드라마를 보면서 한국어 표현을 익히고 있어요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 523,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 523, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "요즘", "start_ms": 1100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 1800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "드라마를", "start_ms": 2500, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "보면서", "start_ms": 3500, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "한국어", "start_ms": 4400, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "표현을", "start_ms": 5200, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "익히고", "start_ms": 6100, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 6900, "dur_ms": 600, "phase_lr_deg": 0.0}
]
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
"transcript": "언어는 단순히 단어를 외우는 것이 아니라 문화를 이해하는 과정입니다",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 627,
"audio_type": "speech",
"words": [
{"word": "언어는", "start_ms": 627, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "단순히", "start_ms": 1400, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "단어를", "start_ms": 2200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "외우는", "start_ms": 2900, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "것이", "start_ms": 3600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 4100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "문화를", "start_ms": 4800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "이해하는", "start_ms": 5500, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "과정입니다", "start_ms": 6400, "dur_ms": 1000, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "실제로 원어민과 대화해보는 것이 실력 향상에 가장 큰 도움이 됩니다",
"speaker": "speaker_2",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 231,
"audio_type": "speech",
"words": [
{"word": "실제로", "start_ms": 231, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "원어민과", "start_ms": 1000, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "대화해보는", "start_ms": 2000, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "것이", "start_ms": 3200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "실력", "start_ms": 3700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "향상에", "start_ms": 4300, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 5100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 5700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "도움이", "start_ms": 6100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "됩니다", "start_ms": 6800, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "틀리는 것을 두려워하지 말고 자신 있게 말하는 습관을 들이세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 520,
"audio_type": "speech",
"words": [
{"word": "틀리는", "start_ms": 520, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "것을", "start_ms": 1250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "두려워하지", "start_ms": 1800, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "말고", "start_ms": 3000, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "자신", "start_ms": 3700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "있게", "start_ms": 4300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "말하는", "start_ms": 4900, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "습관을", "start_ms": 5700, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "들이세요", "start_ms": 6600, "dur_ms": 800, "phase_lr_deg": 0.0}
]
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
"transcript": "반복해서 듣고 따라 하는 쉐도잉 학습법이 아주 효과적입니다",
"speaker": "speaker_2",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 310,
"audio_type": "speech",
"words": [
{"word": "반복해서", "start_ms": 310, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "듣고", "start_ms": 1250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "따라", "start_ms": 1800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 2400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "쉐도잉", "start_ms": 3000, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "학습법이", "start_ms": 3800, "dur_ms": 950, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 4900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "효과적입니다", "start_ms": 5500, "dur_ms": 1100, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "가장 좋아하는 한국 노래 가사를 외워보는 것도 좋은 방법이에요",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 102,
"audio_type": "speech",
"words": [
{"word": "가장", "start_ms": 102, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "좋아하는", "start_ms": 750, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 1600, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "노래", "start_ms": 2300, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "가사를", "start_ms": 2950, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "외워보는", "start_ms": 3750, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "것도", "start_ms": 4750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 5300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "방법이에요", "start_ms": 5900, "dur_ms": 900, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "The permanent yankee bank and don diso.",
"speaker": "speaker_1",
"confidence": 0.81,
"language": "en",
"silence_before_ms": 275,
"audio_type": "speech",
"words": [
{"word": "The", "start_ms": 275, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "permanent", "start_ms": 550, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "yankee", "start_ms": 1450, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "bank", "start_ms": 2200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "and", "start_ms": 2800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "don", "start_ms": 3250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "diso", "start_ms": 3750, "dur_ms": 600, "phase_lr_deg": 0.0}
]
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
"transcript": "포기하지 않고 끝까지 노력하면 반드시 좋은 결과가 있을 거예요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 167,
"audio_type": "speech",
"words": [
{"word": "포기하지", "start_ms": 167, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "않고", "start_ms": 1100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "끝까지", "start_ms": 1650, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "노력하면", "start_ms": 2500, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "반드시", "start_ms": 3500, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 4350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "결과가", "start_ms": 4950, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "있을", "start_ms": 5800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 6350, "dur_ms": 650, "phase_lr_deg": 0.0}
]
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
"transcript": "여러분의 한국어 공부를 진심으로 응원합니다 화이팅 하세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 326,
"audio_type": "speech",
"words": [
{"word": "여러분의", "start_ms": 326, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "한국어", "start_ms": 1200, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "공부를", "start_ms": 2000, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "진심으로", "start_ms": 2750, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "응원합니다", "start_ms": 3750, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "화이팅", "start_ms": 4900, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "하세요", "start_ms": 5750, "dur_ms": 700, "phase_lr_deg": 0.0}
]
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
"transcript": "오늘 수업은 여기까지입니다 다음 시간에 더 재미있는 주제로 만나요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 206,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 206, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "수업은", "start_ms": 750, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "여기까지입니다", "start_ms": 1500, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 2800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "시간에", "start_ms": 3400, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 4150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "재미있는", "start_ms": 4550, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "주제로", "start_ms": 5500, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 6350, "dur_ms": 650, "phase_lr_deg": 0.0}
]
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
"transcript": "안녕히 계세요 감사합니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 130, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "계세요", "start_ms": 850, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 1700, "dur_ms": 1100, "phase_lr_deg": 0.0}
]
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
"transcript": "공부하다가 모르는 부분이 있으면 언제든지 질문해 주세요",
"speaker": "speaker_2",
"confidence": 0.91,
"language": "ko",
"silence_before_ms": 574,
"audio_type": "speech",
"words": [
{"word": "공부하다가", "start_ms": 574, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "모르는", "start_ms": 1650, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "부분이", "start_ms": 2400, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "있으면", "start_ms": 3150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "언제든지", "start_ms": 3850, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "질문해", "start_ms": 4800, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 5600, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "Claude Whitaker has decreed 1512 season.",
"speaker": "speaker_1",
"confidence": 0.85,
"language": "en",
"silence_before_ms": 765,
"audio_type": "speech",
"words": [
{"word": "Claude", "start_ms": 765, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "Whitaker", "start_ms": 1300, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "has", "start_ms": 2100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "decreed", "start_ms": 2500, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "1512", "start_ms": 3300, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "season", "start_ms": 4500, "dur_ms": 650, "phase_lr_deg": 0.0}
]
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
"transcript": "그럼 모두 수고하셨습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 261,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 261, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "모두", "start_ms": 800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "수고하셨습니다", "start_ms": 1400, "dur_ms": 1200, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 06:27:47*
