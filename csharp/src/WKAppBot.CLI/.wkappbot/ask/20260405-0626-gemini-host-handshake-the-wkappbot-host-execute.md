# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=6944  time=06:24:24  uptime=16873s  mem=414MB  system_processes=354"}],"isError":false}}]
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
> **Time**: 2026-04-05 06:26:17
> **AI**: gemini

---

## Response

AnalysisAnalysisGemini의 응답[
{
"segment": 1,
"transcript": "안 그래도 지금 촬영이라 봐요 이게",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "안", "start_ms": 10, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "그래도", "start_ms": 90, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 210, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "촬영이라", "start_ms": 310, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "봐요", "start_ms": 440, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 520, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "아파도 준비할 음식이 많아 엄살을 부릴 틈이 없습니다 총 42kg의 양념 게장 중",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "아파도", "start_ms": 30, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "준비할", "start_ms": 430, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "음식이", "start_ms": 780, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "많아", "start_ms": 1080, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "엄살을", "start_ms": 1330, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "부릴", "start_ms": 1730, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "틈이", "start_ms": 1980, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "없습니다", "start_ms": 2230, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "총", "start_ms": 2800, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "42kg의", "start_ms": 2950, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "양념", "start_ms": 3750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "게장", "start_ms": 4100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "중", "start_ms": 4400, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "한국을 좋아하는 다른 나라 젊은이들이 많아가지고",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "한국을", "start_ms": 20, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "좋아하는", "start_ms": 120, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "다른", "start_ms": 240, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "나라", "start_ms": 320, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "젊은이들이", "start_ms": 400, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "많아가지고", "start_ms": 520, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "굉장히 많이 와서 그 저희랑 같이 일을 하면서",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "굉장히", "start_ms": 10, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 110, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "와서", "start_ms": 210, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 310, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "저희랑", "start_ms": 360, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 460, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "일을", "start_ms": 510, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "하면서", "start_ms": 560, "dur_ms": 10, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "한국 문화 공유하고 같이 지내고 있어요",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "한국", "start_ms": 10, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "문화", "start_ms": 90, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "공유하고", "start_ms": 190, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 310, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "지내고", "start_ms": 390, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 510, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "농촌에 머물며 노동력과 숙식을 교환하는 팜 스테이 전통적인",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "농촌에", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "머물며", "start_ms": 150, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "노동력과", "start_ms": 250, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "숙식을", "start_ms": 370, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "교환하는", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "팜", "start_ms": 570, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "스테이", "start_ms": 620, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "전통적인", "start_ms": 720, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "뭐 이 효과가 뭐 계속 급등할거라고 본다면 시점은 제한적이지 않을까 그렇게 좀 생각이 듭니다",
"speaker": "speaker_5",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "뭐", "start_ms": 20, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 70, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "효과가", "start_ms": 120, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 200, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 250, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "급등할거라고", "start_ms": 330, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "본다면", "start_ms": 450, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "시점은", "start_ms": 550, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "제한적이지", "start_ms": 670, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "않을까", "start_ms": 770, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "그렇게", "start_ms": 870, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 970, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "생각이", "start_ms": 1020, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "듭니다", "start_ms": 1120, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "오히려 빨리 일단 나중에 협상을 하더라도 일단 빨리 수급을 받는게 더 중요하지 않을까 이렇게 생각이 들고",
"speaker": "speaker_5",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "오히려", "start_ms": 20, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "빨리", "start_ms": 120, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "일단", "start_ms": 200, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "나중에", "start_ms": 280, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "협상을", "start_ms": 380, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "하더라도", "start_ms": 500, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "일단", "start_ms": 600, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "빨리", "start_ms": 680, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "수급을", "start_ms": 760, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "받는게", "start_ms": 880, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 980, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "중요하지", "start_ms": 1030, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "않을까", "start_ms": 1130, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 1230, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "생각이", "start_ms": 1310, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "들고", "start_ms": 1410, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "나중에 뭐 우회를 하는 뭐 방법을 찾든 그건 나중에 할 일이고 일단 급한 거는 좀 하는게 나은거죠",
"speaker": "speaker_5",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "나중에", "start_ms": 20, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 120, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "우회를", "start_ms": 170, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 270, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 350, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "방법을", "start_ms": 400, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "찾든", "start_ms": 500, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "그건", "start_ms": 580, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "나중에", "start_ms": 660, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 760, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "일이고", "start_ms": 810, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "일단", "start_ms": 890, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "급한", "start_ms": 970, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "거는", "start_ms": 1050, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 1130, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "하는게", "start_ms": 1180, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "나은거죠", "start_ms": 1280, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "자 그래서 요기 보시면은 회당 통행료와 소식",
"speaker": "speaker_5",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "자", "start_ms": 10, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 60, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "요기", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "보시면은", "start_ms": 240, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "회당", "start_ms": 360, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "통행료와", "start_ms": 460, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "소식", "start_ms": 580, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "통해 NMN은 인간의 세포 나이를 되돌릴 수 있는 가장 확실하고 과학적인 분자라고 거듭 강조",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "통해", "start_ms": 20, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "NMN은", "start_ms": 220, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "인간의", "start_ms": 720, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "세포", "start_ms": 1070, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "나이를", "start_ms": 1370, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "되돌릴", "start_ms": 1720, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 2120, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 2270, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 2520, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "확실하고", "start_ms": 2870, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "과학적인", "start_ms": 3370, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "분자라고", "start_ms": 3870, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "거듭", "start_ms": 4370, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "강조", "start_ms": 4720, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "대놓고 외면할 수는 없는 상황에서 전쟁 상황을 주시하며 이란과의 직접 충돌을 최대한 피하겠다는 뜻으로 보입니다",
"speaker": "speaker_6",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "대놓고", "start_ms": 30, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "외면할", "start_ms": 430, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "수는", "start_ms": 830, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "없는", "start_ms": 1130, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "상황에서", "start_ms": 1430, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "전쟁", "start_ms": 2030, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "상황을", "start_ms": 2330, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "주시하며", "start_ms": 2730, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이란과의", "start_ms": 3230, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "직접", "start_ms": 3730, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "충돌을", "start_ms": 4080, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "최대한", "start_ms": 4480, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "피하겠다는", "start_ms": 4930, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "뜻으로", "start_ms": 5530, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "보입니다", "start_ms": 5930, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "호르무즈 해협엔 우리 선박 20",
"speaker": "speaker_6",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "호르무즈", "start_ms": 10, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "해협엔", "start_ms": 210, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "우리", "start_ms": 360, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "선박", "start_ms": 440, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "20", "start_ms": 540, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "좀 맛있거든요 이건 그냥 진짜 그냥 던졌어요 진짜 와 맞아줘야 돼 오라이",
"speaker": "speaker_7",
"confidence": 0.91,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "mixed",
"words": [
{"word": "좀", "start_ms": 10, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "맛있거든요", "start_ms": 60, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "이건", "start_ms": 180, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 260, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 340, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 420, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "던졌어요", "start_ms": 500, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 600, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "와", "start_ms": 680, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "맞아줘야", "start_ms": 730, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "돼", "start_ms": 830, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "오라이", "start_ms": 880, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "둘이 자리를 하고 있죠 거기다가 이 지붕에 지금 팔작지붕의 끝 부분인 거죠 저기 있는 창이 사실 정말로 저게 최대 사이즈",
"speaker": "speaker_8",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "둘이", "start_ms": 20, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "자리를", "start_ms": 320, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "하고", "start_ms": 720, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "있죠", "start_ms": 1020, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "거기다가", "start_ms": 1320, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 1820, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "지붕에", "start_ms": 1970, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 2370, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "팔작지붕의", "start_ms": 2670, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "끝", "start_ms": 3370, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "부분인", "start_ms": 3520, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "거죠", "start_ms": 3920, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "저기", "start_ms": 4220, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 4520, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "창이", "start_ms": 4770, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "사실", "start_ms": 5120, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "정말로", "start_ms": 5470, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "저게", "start_ms": 5970, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "최대", "start_ms": 6270, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "사이즈", "start_ms": 6620, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "진짜랍니다 한국 여행 한국 회사 취업 한국 대학 유학 심지어 그냥 한국에서 한 달 살아보고 싶다는 사람까지 엄청나게 늘었다고 해요 도대체 어떤 매력이 전 세계를 휩쓸",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "진짜랍니다", "start_ms": 20, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 620, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "여행", "start_ms": 970, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 1320, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "회사", "start_ms": 1670, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "취업", "start_ms": 2020, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 2370, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "대학", "start_ms": 2720, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "유학", "start_ms": 3070, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "심지어", "start_ms": 3420, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 3870, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "한국에서", "start_ms": 4170, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 4670, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "달", "start_ms": 4870, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "살아보고", "start_ms": 5070, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "싶다는", "start_ms": 5570, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "사람까지", "start_ms": 5970, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "엄청나게", "start_ms": 6470, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "늘었다고", "start_ms": 6970, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "해요", "start_ms": 7420, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "도대체", "start_ms": 7720, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 8120, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "매력이", "start_ms": 8420, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "전", "start_ms": 8820, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "세계를", "start_ms": 8970, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "휩쓸", "start_ms": 9370, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "약속을 해놨는데 갑자기 이게 탁 해갖고 뭐가 들어오게 되면은 어 얘도 믿는 거네 하고 해 해서 그냥 FSD를 켜버리는 그리고 차량 멀",
"speaker": "speaker_9",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "약속을", "start_ms": 20, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "해놨는데", "start_ms": 420, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "갑자기", "start_ms": 920, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 1320, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "탁", "start_ms": 1620, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "해갖고", "start_ms": 1820, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "뭐가", "start_ms": 2220, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "들어오게", "start_ms": 2520, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "되면은", "start_ms": 2920, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "어", "start_ms": 3320, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "얘도", "start_ms": 3470, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "믿는", "start_ms": 3770, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "거네", "start_ms": 4070, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "하고", "start_ms": 4370, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "해", "start_ms": 4670, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "해서", "start_ms": 4870, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 5170, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "FSD를", "start_ms": 5470, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "켜버리는", "start_ms": 5970, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 6470, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "차량", "start_ms": 6870, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "멀", "start_ms": 7220, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 19,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 20,
"transcript": "인류 거실 오른편에는 따로 주방을 뒀고요",
"speaker": "speaker_7",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "mixed",
"words": [
{"word": "인류", "start_ms": 30, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "거실", "start_ms": 330, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "오른편에는", "start_ms": 680, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "따로", "start_ms": 1180, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "주방을", "start_ms": 1480, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "뒀고요", "start_ms": 1880, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "에는 수백년째 영국의 지배 아래 있었습니다 아일랜드의 비옥한 농지에서 생산된 밀 귀리 보리는 대부분 영국인 지",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "에는", "start_ms": 20, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "수백년째", "start_ms": 270, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "영국의", "start_ms": 870, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "지배", "start_ms": 1270, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "아래", "start_ms": 1620, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있었습니다", "start_ms": 1970, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "아일랜드의", "start_ms": 2570, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "비옥한", "start_ms": 3270, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "농지에서", "start_ms": 3720, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "생산된", "start_ms": 4320, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "밀", "start_ms": 4820, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "귀리", "start_ms": 5020, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "보리는", "start_ms": 5370, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "대부분", "start_ms": 5820, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "영국인", "start_ms": 6270, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "지", "start_ms": 6770, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "세븐 이상은 모든 기능을 다 지원하고 식스 이하는 기능의 차이가 있지 지원 자체는",
"speaker": "speaker_9",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "세븐", "start_ms": 20, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이상은", "start_ms": 420, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "모든", "start_ms": 870, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "기능을", "start_ms": 1220, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 1670, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "지원하고", "start_ms": 1870, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "식스", "start_ms": 2470, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이하는", "start_ms": 2870, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "기능의", "start_ms": 3320, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "차이가", "start_ms": 3720, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "있지", "start_ms": 4120, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "지원", "start_ms": 4420, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "자체는", "start_ms": 4770, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "도 구현될 수 있도록 로봇을 제어해야 하기 때문에 그의 연구는 특히 중요합니다",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "도", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "구현될", "start_ms": 180, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 630, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있도록", "start_ms": 780, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "로봇을", "start_ms": 1180, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "제어해야", "start_ms": 1680, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "하기", "start_ms": 2280, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "때문에", "start_ms": 2630, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "그의", "start_ms": 3080, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "연구는", "start_ms": 3380, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "특히", "start_ms": 3880, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "중요합니다", "start_ms": 4280, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "논의한다고 합니다 여기서 한국도 참여를 한다고 하고요 이게 아마 최근에 트럼프가 너네들이 알아서 해결하라 라는 거에 대해서 좀 요런 모습",
"speaker": "speaker_5",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "논의한다고", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "합니다", "start_ms": 170, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "여기서", "start_ms": 270, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "한국도", "start_ms": 370, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "참여를", "start_ms": 490, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "한다고", "start_ms": 610, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "하고요", "start_ms": 710, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 810, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "아마", "start_ms": 890, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "최근에", "start_ms": 970, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "트럼프가", "start_ms": 1070, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "너네들이", "start_ms": 1190, "dur_

---
*Generated by WKAppBot ask gemini — 2026-04-05 06:26:17*
