# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=28596  time=06:58:31  uptime=11193s  mem=434MB  system_processes=409"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_065501.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 9396ms (file: 261-267-132-765-623-132-132-135-517-105-317-210-752-753-371-130-103-132-612-671-653-362-263-230-752-756-752-260-547-573-751-256-260-756-103-267-260-672-265-265-321-735-215-265-7_W.mp3)
  Segment 2: 10396ms ~ 26560ms (file: 176-671-321-236-276-207-320-103-132-713-176-312-103-103-371-276-726-276-327-103 326-321-762-357-103-326-326-310-321-310-326-572-257-310-103-236-236-352-576-276-537-547_W.mp3)
  Segment 3: 27560ms ~ 34004ms (file: 312-370-617-317-617-107-710-106-103-176-106-267-321-326-310-106-107-320-263-765-756-725-750-175-715-175-571-726-276-275-170-107-176-167-310-320-736-376-756-175-106-237-267-132-7_W.mp3)
  Segment 4: 35004ms ~ 41448ms (file: 361-710-103-576-571-153-137-567-603-567-105-165-617-705-576-507-750-570-571-135-756-750-754-756-216-312-735-276-327-375-726-137-726-715-271-276-321-721-275-267-271-725-152-276-2_W.mp3)
  Segment 5: 42448ms ~ 50116ms (file: 506-574-217-137-312-317-761-136-132-310-130-571-715-135-753-235-754-751-524-574-572-712-120-512-756-765-675-352-132-573-570-257-572-267-507-576-123-267-756-756-756-576-576-675-2_W_stt_the_tough_sell_its.mp3)
  Segment 6: 51116ms ~ 51692ms (file: 321-271-263-312-675-637-632-231-213-326-523-632-675-316-732-321-506-561-352-275-750-351-576-231-205-627-326-627-321-236-136-321-312-312-267-276-275-726-125-215-725-213-216-251-2_V.mp3)
  Segment 7: 52692ms ~ 61008ms (file: 213-310-260-206-301-136-301-103-176-107-326-301-103-326-320-276-213-260-302-306-176-107-126-273-321-130-132-103-106-723-213-627-206-230-160-106-107-160-170-130-132-102-301-321-2_W.mp3)
  Segment 8: 62008ms ~ 62584ms (file: 367-367-361-361-361-316-573-537-573-503-357-765-673-376-673-623-362-316-367-632-361-316-710-175-715-675-726-571-517-576-517-561-625-637-627-627-627_W.mp3)
  Segment 9: 63584ms ~ 64160ms (file: 613-253-132-213-103-130-132-123-312-123-132-231-173-136-256-576-326-132_W.mp3)
  Segment 10: 65160ms ~ 65736ms (file: 657-657-567-567-765-567 765-736-765-765-765-576-321-623-216-375-537-573-567-675-673-763-327-546-546-576-675-576-657-321-630-627-310-657-567-765-756-657-675-765-765-765-627-602-6_W.mp3)
  Segment 11: 66736ms ~ 67312ms (file: 517-576-167-675-671-316-627-265-657-572-573-165-317-715-725-163-761-361-176-613-316-132-326-256-612-716-175-162-267-613-312-261-260-136-572-507-657-156 326-672-621-132-267-362-7_W.mp3)
  Segment 12: 68312ms ~ 74432ms (file: 103-273-273-123-621-326-160-261-276-261-326-120-517-125-231-263-106-710-276-126-213-273-257-275-267-235-215-617-671-260-621-261-237-256-102-312-102-536-302-106-320-267-256-267-2_W.mp3)
  Segment 13: 75432ms ~ 76008ms (file: 735-136-261-276-321-762-257-326-237-562-361-752-327-237-326-567-531-725-547-372-236-132-351-715-672-175-752-230-605-367-176-316_V.mp3)
  Segment 14: 77008ms ~ 84532ms (file: 230-756-120-260-126-260-203-123-130-132-627-627-267-162-213-107-132-760-172-120-301-103-261-267-705-756-756-627-276-102-127-103-762-756-675-216-126-120-210-210-267-672-160-123-6_W.mp3)
  Segment 15: 85532ms ~ 91976ms (file: 103-701-136-301-321-320-267-236-352-235-356-653-106-130-675-130-167-637-136-361-130-761-261-631-163-673-675-675-675-675-612-671-765-672-167-130-130-136-132-137-173-736-170-671-6_W.mp3)
  Segment 16: 92976ms ~ 93552ms (file: 216-726-507-531-263-317-126-263-726-125-352-570-270-751-361-573-275-316-317-306-103-576-571-765-136-163-632-671-637-167-617-361-631-137-176-371-317-163-617-176-176-675-675-672-7_W.mp3)
  Segment 17: 94552ms ~ 102292ms (file: 263-206-261-261-263-203-230-176-675-674-167-267-130-326-216-132-106-310-310-103-267-763-267-276-132-320-625-572-236-236-263-137-132-301-321-236-267-267-312-310-163-261-267-236-2_W.mp3)
  Segment 18: 103292ms ~ 113480ms (file: 372-326-317-753-267-627-537-327-153-136-172-376-317-726-263-317-237-327-137-316-160-326-372-175-715-261-627-756-672-762-267-326-327-263-132-321-267-213-623-237-326-371-132-327-3_W.mp3)
  Segment 19: 114480ms ~ 119340ms (file: 752-761-756-376-765-130-167-160-675-236-321-267-267-261-572-120-210-271-107-163-361-316-613-316-173-163-137-750-106-271-175-174-571-175-321-162-716-756-103-250-103-571-576-765-1_W.mp3)
  Segment 20: 120340ms ~ 130024ms (file: 216-310-753-750-302-735-260-572-203-261-725-263-231-265-526-165-135-263-231-751-576-137-267-321-572-752-230-574-572-301-132-107-165-175-320-574-526-271-103-102-574-573-716-275-7_W.mp3)
  Segment 21: 131024ms ~ 140600ms (file: 157-104-130-370-231-312-206-206-236-725-756-715-750-754-756-573-130-130-203-312-130-103-130-302-316-320-675-756-754-310-753-312-750-276-103-203-120-217-326-236-361-312-320-263-1_W.mp3)
  Segment 22: 141600ms ~ 142176ms (file: 532-521-753 157-526-352-176-156-157-157-650-157-157-156-315-351-652-635-756-576-756-756-671-657-657-567-576-157-510-756-517-675-765-735-756-675-627-237-326-256 312-763-326-567-5_W.mp3)
  Segment 23: 143176ms ~ 151060ms (file: 267-173-762-276-762-726-756-267-321-132-632-107-103-216-267-276-273-321-231-206-267-132-761-175-627-216-326-302-302-103-312-326-206-736-276-237-320-106-167-103-160-236-321-321-2_W.mp3)
  Segment 24: 152060ms ~ 152636ms (file: 576-675-176-705-512-126-750-754-725-567-753-136-572-321-572-256-502 267-623-316-735-167-327-237-215-756-732-176-507-321-735-253-527-132-716-123-315-752-123-256-376-725-326-312-7_V.mp3)
  Segment 25: 153636ms ~ 154212ms (file: 326-362-132-316-627-315-135-317-310 362-371-371-315_W.mp3)
  Segment 26: 155212ms ~ 155788ms (file: 312-673-675-317-756-576-537-103-105-315-362-631-361-637-136-312-361-631-316-617-136-631-361-316-617-167-367-631-361-361-631-613-163-130-576-574-157-675-765-675-763-675-763-675-7_W.mp3)
  Segment 27: 156788ms ~ 168236ms (file: 130-231-236-213-267-321-310-136-267-627-627-321-136-213-627-102-261-627-126-756-106-102-126-236-102-236-320-753-672-627-620-321-672-623-102-175-162-167-231-261-627-627-260-263-1_W.mp3)
  Segment 28: 169236ms ~ 169812ms (file: 123-612-671-673-716-756-753-507-321-571-572-573-537-571-576-576_W.mp3)
  Segment 29: 170812ms ~ 171388ms (file: 673-753-576-570-576-547-563-573-537-537-615_W.mp3)
  Segment 30: 172388ms ~ 172964ms (file: 617-176-517-107-175-750-706-701-705-705-710-751-705-715-751-750-751-710-750-751 107-170-176-705-715-170-107-715-710-715-715-175-175-715-571 105-170-170-170-705-751-751-756-675-7_V.mp3)
  Segment 31: 173964ms ~ 183576ms (file: 760-136-216-107-675-106-201-203-103-230-102-260-120-102-135-132-162-106-130-231-321-230-230-320-106-126-106-102-267-256-216-312-321-210-123-106-206-201-130-235-312-102-312-321-3_V.mp3)
  Segment 32: 184576ms ~ 185152ms (file: 157-157-517-157-157-517-517-517-576-517-571-576-571-135-315-103-103-135-137-135-137-103-107-175-157-157-175-157-157-156-157-157-157-517-157-571-157-517-103-103-157-157-157-175-1_V.mp3)
  Segment 33: 186152ms ~ 194684ms (file: 167-103-312-126-216-206-267-217-201-536-752-572-507-126-276-572-275-675-276-720-263-312-612-206-270-273-127-152-123 754-275-106-172-163-261-235-572-275-127-213-207-217-276-123-3_V.mp3)
  Segment 34: 195684ms ~ 204180ms (file: 236-230-320-630-167-603-102-137-320-236-623-231-273-320-130-710-136-106-130-132-760-376-326-267-327-231-103-310-103-102-150-312-312-134-103-132-310-132-321-320-231-230-326-203-1_W.mp3)
  Segment 35: 205180ms ~ 205756ms (file: 765-531-126-615-675-673-361-673-715-165-165-167-136-315-527-751-127-137-132-760-627-163-160-173 675-265 163-132-312-132-143-602-623-623-321-371-631-361-610-671-716-137-132-126-1_W.mp3)
  Segment 36: 206756ms ~ 213668ms (file: 327-103-601-627-726-607-367-267-263-301-236-276-327-267-726-726-570-571-275-276-327-237-276-273-723-675-675-107-276-627-167-132-263-321-762-102-237-263-267-267-237-230-126-170-7_W.mp3)
  Segment 37: 214668ms ~ 215244ms (file: 134 156-625-763-673-132-367-351-362-326-321_W.mp3)
  Segment 38: 216244ms ~ 216820ms (file: 765-765-762-765-751-756-712-765-756-753-576-576-756-756-756-756-752-725-752-756-572-725-751-752-756-752-756-750-275-132-572-756-752-752-572-756-756-573-576-576-567-675-567-756-7_W.mp3)
  Segment 39: 217820ms ~ 229016ms (file: 260-213-123-120-103-103-136-103-172-301-162-310-236-752-130-756-372-120-201-206-267-261-547-574-276-103-123-103-236-230-263-213-261-263-267-263-263-260-627-213-102-120-263-163-3_W.mp3)
  Segment 40: 230016ms ~ 237792ms (file: 270-753-321-106-103-671-103-163-107-103-126-321-230-671-312-573-276-731-276-267-132-762-726-572-756-627-106-317-716-173-267-267-231-103-267-321-107-173-173-321-532-572-620-167-5_W.mp3)
  Segment 41: 238792ms ~ 239368ms (file: 572-372-275-670-367-526-351-173-750-132-207-726-235-726-236-267-653-267-576-271-520-136-167-375-756-253-572-752-326-572-237-237-327-236-267-216-235-572-325-312-235-312-726-657-6_V.mp3)
  Segment 42: 240368ms ~ 249008ms (file: 627-103-167-362-623-120-216-102-671-130-321-263-263-620-267-627-263-326-130-571-270-752-173-103-312-103-123-321-627-672-261-260-621-230-176-657-567-123-102-162-267-267-132-516-7_W.mp3)
  Segment 43: 250008ms ~ 257352ms (file: 175-572-127-175-675-130-157-710-106-761-672-126-312-130-620-136-176-612-310-267-230-312-310-310-310-102-173-175-537-163-210-231-267-620-267-107-103-103-231-230-351-316-613-143-6_W.mp3)
  Segment 44: 258352ms ~ 265660ms (file: 213-621-321-263-162-261-216-120-162-572-251-132-267-256-302-263-726-162-130-617-627-265-267-206-276-762-263-310-312-762-276-216-267-257-321-213-102-102-620-231-263-123-302-130-1_W.mp3)
  Segment 45: 266660ms ~ 274724ms (file: 672-627-276-267-267-132-216-263-620-210-103-130-320-276-237-206-105-321-572-751-275-752-574-725-215-201-203-750-752-574-130-752-127-265-237-275-312-276-260-527-520-132-672-627-6_W.mp3)
  Segment 46: 275724ms ~ 276300ms (file: 756-725-527-123-756-672-527-527-625-250-253-325-301-526-326-136-362-635 657-675-361-316-310-712-107-370_V.mp3)
  Segment 47: 277300ms ~ 283132ms (file: 627-206-236-607-627-625-627-362-263-236-236-320-326-765-725-672-236-263-163-260-516-362-327-576-576-103-163-231-623-236-263-312-321-312-132-236-236-265-236-260-230-102-123-721-1_W.mp3)
  Segment 48: 284132ms ~ 284708ms (file: 657-132-136 571-361-163-136-613-167-756-652-276-276-725-712-627-617-576-754-326-162-702-517-567-516-125-723-325-106-163-725-136-157-126-570-236-762-705-216-132-167-712-513-751-5_V.mp3)
  Segment 49: 285708ms ~ 286284ms (file: 365-175-657-156-312-136-136-163-136-137-173-731-371-732-657-371-736-576-751-753-675-673-376-763-765-751-765-765-657-635-576-513-567-576-576-567-561-571-576-567-175-176-756-576-5_W.mp3)
  Segment 50: 287284ms ~ 287860ms (file: 310-675-316-726-761-765-765-127 123-231-716-716-375-367-367-673-526-102-261-513-625-215-106-752-621-210-175-512-216-123-561-765-123-621-532-326-127-126-572-613-137-576 136-312-5_V.mp3)
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
> **Time**: 2026-04-06 07:01:02
> **AI**: gemini

---

## Response

AnalysisAnalysisGemini의 응답TITLE: Professional Audio Transcription and Analysis

{
"segment": 1,
"transcript": "그 피와 땀과 눈물로 써낸 지난 10년간의 그 핵심 개발 데이터들을 얻을 수 있었지만",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "그", "start_ms": 100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "피와", "start_ms": 350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "땀과", "start_ms": 800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "눈물로", "start_ms": 1250, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "써낸", "start_ms": 1800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "지난", "start_ms": 2250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "10년간의", "start_ms": 2650, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 3500, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "핵심", "start_ms": 3750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "개발", "start_ms": 4250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "데이터들을", "start_ms": 4700, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "얻을", "start_ms": 5550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 5950, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있었지만", "start_ms": 6150, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "그 귀한 보물을 고작 1조 원의 돈과 맞바꿨고 이제 KF-21이 보란 듯이 완벽하게 개발",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "그", "start_ms": 150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "귀한", "start_ms": 400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "보물을", "start_ms": 900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "고작", "start_ms": 1450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "1조", "start_ms": 1900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "원의", "start_ms": 2300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "돈과", "start_ms": 2650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "맞바꿨고", "start_ms": 3150, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 4000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "KF-21이", "start_ms": 4450, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "보란", "start_ms": 5400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "듯이", "start_ms": 5850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "완벽하게", "start_ms": 6250, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "개발", "start_ms": 7000, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "라고 말씀드리긴 어렵지만 지금 상당한 물량이 계속 확보되고 있다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "라고", "start_ms": 120, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "말씀드리긴", "start_ms": 450, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "어렵지만", "start_ms": 1300, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 2100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "상당한", "start_ms": 2600, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "물량이", "start_ms": 3200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 3750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "확보되고", "start_ms": 4250, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "있다", "start_ms": 5000, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "오늘부터 자원 안보 위기 경보가 3단계 경계로 격상됐고 오는 8일부터",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "오늘부터", "start_ms": 80, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "자원", "start_ms": 750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "안보", "start_ms": 1200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "위기", "start_ms": 1650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "경보가", "start_ms": 2100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "3단계", "start_ms": 2700, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "경계로", "start_ms": 3350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "격상됐고", "start_ms": 3900, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "오는", "start_ms": 4700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "8일부터", "start_ms": 5100, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "공공기관 승용차 5부제는 2부제로 강화되고 공영 주차장에도 승용차 5부제가 시작됩니다",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "공공기관", "start_ms": 100, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "승용차", "start_ms": 850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "5부제는", "start_ms": 1400, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "2부제로", "start_ms": 2050, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "강화되고", "start_ms": 2700, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "공영", "start_ms": 3450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "주차장에도", "start_ms": 3900, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "승용차", "start_ms": 4750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "5부제가", "start_ms": 5300, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "시작됩니다", "start_ms": 6000, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "MBC 뉴스 김민형입니다",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "MBC", "start_ms": 50, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "뉴스", "start_ms": 600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "김민형입니다", "start_ms": 1050, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "다음날도 상한가 그리고 또 그 다음날이 24%까지 상승하면서 한 달쯤 전에 광동신",
"speaker": "speaker_4",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "다음날도", "start_ms": 130, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "상한가", "start_ms": 780, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 1350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 1850, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 2100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "다음날이", "start_ms": 2350, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "24%까지", "start_ms": 3050, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "상승하면서", "start_ms": 4000, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 4850, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "달쯤", "start_ms": 5100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "전에", "start_ms": 5550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "광동신", "start_ms": 5950, "dur_ms": 600, "phase_lr_deg": 0.0}
]
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
"transcript": "준비됐어? 물론이죠 사실 저도 엄청 기대하고 있었어요",
"speaker": "speaker_5",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "준비됐어?", "start_ms": 150, "dur_ms": 700, "phase_lr_deg": -10.0},
{"word": "물론이죠", "start_ms": 1000, "dur_ms": 600, "phase_lr_deg": 10.0},
{"word": "사실", "start_ms": 1650, "dur_ms": 400, "phase_lr_deg": 10.0},
{"word": "저도", "start_ms": 2100, "dur_ms": 350, "phase_lr_deg": 10.0},
{"word": "엄청", "start_ms": 2500, "dur_ms": 400, "phase_lr_deg": 10.0},
{"word": "기대하고", "start_ms": 2950, "dur_ms": 600, "phase_lr_deg": 10.0},
{"word": "있었어요", "start_ms": 3600, "dur_ms": 600, "phase_lr_deg": 10.0}
]
},
{
"segment": 10,
"transcript": "김문성 씨가",
"speaker": "speaker_6",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "김문성", "start_ms": 50, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "씨가", "start_ms": 700, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "저 배고파요. 음 You're happy. Let's start the baiting.",
"speaker": "speaker_7",
"confidence": 0.91,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "저", "start_ms": 120, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "배고파요.", "start_ms": 350, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "음", "start_ms": 1000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "You're", "start_ms": 1400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "happy.", "start_ms": 1850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "Let's", "start_ms": 2400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "start", "start_ms": 2850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 3300, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "baiting.", "start_ms": 3550, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "아 시장 와서 서비스 받는 이렇게 많이 서비스 받는 거 처음이에요 그게 바로 광주의 정의야 확실히",
"speaker": "speaker_8",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "아", "start_ms": 150, "dur_ms": 200, "phase_lr_deg": -15.0},
{"word": "시장", "start_ms": 400, "dur_ms": 400, "phase_lr_deg": -15.0},
{"word": "와서", "start_ms": 850, "dur_ms": 400, "phase_lr_deg": -15.0},
{"word": "서비스", "start_ms": 1300, "dur_ms": 500, "phase_lr_deg": -15.0},
{"word": "받는", "start_ms": 1850, "dur_ms": 400, "phase_lr_deg": -15.0},
{"word": "이렇게", "start_ms": 2300, "dur_ms": 450, "phase_lr_deg": -15.0},
{"word": "많이", "start_ms": 2800, "dur_ms": 400, "phase_lr_deg": -15.0},
{"word": "서비스", "start_ms": 3250, "dur_ms": 500, "phase_lr_deg": -15.0},
{"word": "받는", "start_ms": 3800, "dur_ms": 400, "phase_lr_deg": -15.0},
{"word": "거", "start_ms": 4250, "dur_ms": 200, "phase_lr_deg": -15.0},
{"word": "처음이에요", "start_ms": 4500, "dur_ms": 800, "phase_lr_deg": -15.0},
{"word": "그게", "start_ms": 5400, "dur_ms": 400, "phase_lr_deg": 15.0},
{"word": "바로", "start_ms": 5850, "dur_ms": 400, "phase_lr_deg": 15.0},
{"word": "광주의", "start_ms": 6300, "dur_ms": 500, "phase_lr_deg": 15.0},
{"word": "정의야", "start_ms": 6850, "dur_ms": 600, "phase_lr_deg": 15.0},
{"word": "확실히", "start_ms": 7500, "dur_ms": 600, "phase_lr_deg": -15.0}
]
},
{
"segment": 13,
"transcript": "점입니다 국민을 막아라 미국 플로리다 올랜도 지역 매체 더 메인랜드는 올랜도가 이기기 위해서 꼭 해야 할 세 가지",
"speaker": "speaker_9",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "점입니다", "start_ms": 90, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "국민을", "start_ms": 750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "막아라", "start_ms": 1250, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "미국", "start_ms": 1800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "플로리다", "start_ms": 2250, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "올랜도", "start_ms": 2900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "지역", "start_ms": 3450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "매체", "start_ms": 3900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 4350, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "메인랜드는", "start_ms": 4600, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "올랜도가", "start_ms": 5450, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "이기기", "start_ms": 6100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "위해서", "start_ms": 6650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "꼭", "start_ms": 7200, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "해야", "start_ms": 7500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 7900, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "세", "start_ms": 8150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "가지", "start_ms": 8400, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "최근 대만 방송을 통해 공개된 한국 방위산업 관련 소식이 예상보다 훨씬 강한 파장을 일으키면서 대만 사회 전체가 큰 요동을 치고 있기 때문입니다 무엇보다 충격",
"speaker": "speaker_10",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "최근", "start_ms": 110, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "대만", "start_ms": 600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "방송을", "start_ms": 1050, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "통해", "start_ms": 1650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "공개된", "start_ms": 2100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 2650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "방위산업", "start_ms": 3100, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "관련", "start_ms": 3850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "소식이", "start_ms": 4350, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "예상보다", "start_ms": 4950, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "훨씬", "start_ms": 5650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "강한", "start_ms": 6100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "파장을", "start_ms": 6600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "일으키면서", "start_ms": 7150, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "대만", "start_ms": 8000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "사회", "start_ms": 8450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "전체가", "start_ms": 8900, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 9500, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "요동을", "start_ms": 9750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "치고", "start_ms": 10300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있기", "start_ms": 10700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "때문입니다", "start_ms": 11100, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "무엇보다", "start_ms": 11950, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "충격", "start_ms": 12600, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "꽃매산이네 아 그죠 꽃매산 너무 바삭바삭해 진짜 꽃매산 뭔가 호떡이로",
"speaker": "speaker_11",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "꽃매산이네", "start_ms": 140, "dur_ms": 800, "phase_lr_deg": -10.0},
{"word": "아", "start_ms": 1000, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "그죠", "start_ms": 1250, "dur_ms": 400, "phase_lr_deg": 10.0},
{"word": "꽃매산", "start_ms": 1700, "dur_ms": 600, "phase_lr_deg": 10.0},
{"word": "너무", "start_ms": 2350, "dur_ms": 450, "phase_lr_deg": -10.0},
{"word": "바삭바삭해", "start_ms": 2850, "dur_ms": 800, "phase_lr_deg": -10.0},
{"word": "진짜", "start_ms": 3700, "dur_ms": 450, "phase_lr_deg": -10.0},
{"word": "꽃매산", "start_ms": 4200, "dur_ms": 600, "phase_lr_deg": -10.0},
{"word": "뭔가", "start_ms": 4850, "dur_ms": 400, "phase_lr_deg": -10.0},
{"word": "호떡이로", "start_ms": 5300, "dur_ms": 700, "phase_lr_deg": -10.0}
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
"transcript": "오꼬노미야끼 하지 복분자 왔네 진짜 신기하다 그렇지 많이 여기에 소금빵이 유명해 소금빵 아 여기가 소금빵집 유명해 유명해요 오",
"speaker": "speaker_12",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 160,
"audio_type": "speech",
"words": [
{"word": "오꼬노미야끼", "start_ms": 160, "dur_ms": 900, "phase_lr_deg": 5.0},
{"word": "하지", "start_ms": 1100, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "복분자", "start_ms": 1550, "dur_ms": 600, "phase_lr_deg": 5.0},
{"word": "왔네", "start_ms": 2200, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "진짜", "start_ms": 2650, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "신기하다", "start_ms": 3100, "dur_ms": 650, "phase_lr_deg": 5.0},
{"word": "그렇지", "start_ms": 3800, "dur_ms": 450, "phase_lr_deg": -5.0},
{"word": "많이", "start_ms": 4300, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "여기에", "start_ms": 4750, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "소금빵이", "start_ms": 5300, "dur_ms": 650, "phase_lr_deg": -5.0},
{"word": "유명해", "start_ms": 6000, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "소금빵", "start_ms": 6550, "dur_ms": 600, "phase_lr_deg": 5.0},
{"word": "아", "start_ms": 7200, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "여기가", "start_ms": 7450, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "소금빵집", "start_ms": 8000, "dur_ms": 700, "phase_lr_deg": 5.0},
{"word": "유명해", "start_ms": 8750, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "유명해요", "start_ms": 9300, "dur_ms": 700, "phase_lr_deg": -5.0},
{"word": "오", "start_ms": 10050, "dur_ms": 300, "phase_lr_deg": 5.0}
]
},
{
"segment": 18,
"transcript": "라고 경고했습니다 대미 협상 창구로 주목받는 탈리반 부의회 의장은 또 다른 해상 요충지 바브 엘 만데브 해협 봉쇄를 우회적으로 거론했습니다 이 해협을 지나는 석유와 곡물 비료 물동량",
"speaker": "speaker_13",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "라고", "start_ms": 100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "경고했습니다", "start_ms": 450, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "대미", "start_ms": 1400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "협상", "start_ms": 1850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "창구로", "start_ms": 2300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "주목받는", "start_ms": 2850, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "탈리반", "start_ms": 3500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "부의회", "start_ms": 4050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "의장은", "start_ms": 4600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 5150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "다른", "start_ms": 5400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "해상", "start_ms": 5800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "요충지", "start_ms": 6250, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "바브", "start_ms": 6800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "엘", "start_ms": 7200, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "만데브", "start_ms": 7450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "해협", "start_ms": 8000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "봉쇄를", "start_ms": 8450, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "우회적으로", "start_ms": 9050, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "거론했습니다", "start_ms": 9850, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 10800, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "해협을", "start_ms": 11050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "지나는", "start_ms": 11600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "석유와", "start_ms": 12150, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "곡물", "start_ms": 12750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "비료", "start_ms": 13200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "물동량", "start_ms": 13650, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "지부자로 선출된 뒤 종적을 감춰 폭격으로 큰 부상을 입고 러시아에서 수술까지 받았다는 소문이 돌",
"speaker": "speaker_14",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "지부자로", "start_ms": 120, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "선출된", "start_ms": 750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "뒤", "start_ms": 1300, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "종적을", "start_ms": 1600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "감춰", "start_ms": 2150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "폭격으로", "start_ms": 2650, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 3300, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "부상을", "start_ms": 3550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "입고", "start_ms": 4100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "러시아에서", "start_ms": 4550, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "수술까지", "start_ms": 5350, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "받았다는", "start_ms": 6050, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "소문이", "start_ms": 6750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "돌", "start_ms": 7300, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "부터 시작해 미사일 장착 및 발사 그리고 최근 성공한 주간 및 야간 공중 급유 테스트를 비롯한 수백 권도 넘는 어려운 난관과 테스트들을 단 한 번의 사고 그리고 심지어 실패도 없이 전부 다 합",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "부터", "start_ms": 90, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "시작해", "start_ms": 450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "미사일", "start_ms": 1000, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "장착", "start_ms": 1600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "및", "start_ms": 2050, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "발사", "start_ms": 2250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 2700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "최근", "start_ms": 3200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "성공한", "start_ms": 3650, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "주간", "start_ms": 4250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "및", "start_ms": 4700, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "야간", "start_ms": 4900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "공중", "start_ms": 5350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "급유", "start_ms": 5800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "테스트를", "start_ms": 6250, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "비롯한", "start_ms": 6950, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "수백", "start_ms": 7550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "권도", "start_ms": 8000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "넘는", "start_ms": 8450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "어려운", "start_ms": 8900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "난관과", "start_ms": 9450, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "테스트들을", "start_ms": 10100, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "단", "start_ms": 10950, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 11200, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "번의", "start_ms": 11450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "사고", "start_ms": 11900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 12400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "심지어", "start_ms": 12900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "실패도", "start_ms": 13450, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "없이", "start_ms": 14050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "전부", "start_ms": 14500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 14950, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "합", "start_ms": 15200, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "언급한 냉전형 생산 능력 외에도 가장 핵심적이고 치명적인 경쟁력 하나가 더 있기 때문입니다 바로 아낌없는 기술 이전 전략입니다 국제 무기 거래에서 구매국들을 가장 괴롭히는 것은 판매국이 마치 도둑을 경계",
"speaker": "speaker_15",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "언급한", "start_ms": 110, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "냉전형", "start_ms": 650, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "생산", "start_ms": 1250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "능력", "start_ms": 1700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "외에도", "start_ms": 2150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 2700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "핵심적이고", "start_ms": 3150, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "치명적인", "start_ms": 3950, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "경쟁력", "start_ms": 4650, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "하나가", "start_ms": 5300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 5850, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "있기", "start_ms": 6100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "때문입니다", "start_ms": 6550, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 7400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아낌없는", "start_ms": 7850, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "기술", "start_ms": 8550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이전", "start_ms": 9000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "전략입니다", "start_ms": 9450, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "국제", "start_ms": 10300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "무기", "start_ms": 10750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "거래에서", "start_ms": 11200, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "구매국들을", "start_ms": 11900, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 12700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "괴롭히는", "start_ms": 13150, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "것은", "start_ms": 13850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "판매국이", "start_ms": 14250, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "마치", "start_ms": 14900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "도둑을", "start_ms": 15350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "경계", "start_ms": 15900, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "주식 주시하잖아요 잘하네 그리고 밖에 껍질이 고소하다 너무 맛있는데 야 너무 맛있다 다행이네 맛있게 먹으니까",
"speaker": "speaker_16",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "주식", "start_ms": 140, "dur_ms": 400, "phase_lr_deg": 10.0},
{"word": "주시하잖아요", "start_ms": 600, "dur_ms": 800, "phase_lr_deg": 10.0},
{"word": "잘하네", "start_ms": 1450, "dur_ms": 500, "phase_lr_deg": -10.0},
{"word": "그리고", "start_ms": 2000, "dur_ms": 450, "phase_lr_deg": 10.0},
{"word": "밖에", "start_ms": 2500, "dur_ms": 400, "phase_lr_deg": 10.0},
{"word": "껍질이", "start_ms": 2950, "dur_ms": 500, "phase_lr_deg": 10.0},
{"word": "고소하다", "start_ms": 3500, "dur_ms": 650, "phase_lr_deg": 10.0},
{"word": "너무", "start_ms": 4200, "dur_ms": 400, "phase_lr_deg": 10.0},
{"word": "맛있는데", "start_ms": 4650, "dur_ms": 600, "phase_lr_deg": 10.0},
{"word": "야", "start_ms": 5300, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "너무", "start_ms": 5550, "dur_ms": 400, "phase_lr_deg": 10.0},
{"word": "맛있다", "start_ms": 6000, "dur_ms": 600, "phase_lr_deg": 10.0},
{"word": "다행이네", "start_ms": 6700, "dur_ms": 600, "phase_lr_deg": -10.0},
{"word": "맛있게", "start_ms": 7350, "dur_ms": 500, "phase_lr_deg": -10.0},
{"word": "먹으니까", "start_ms": 7900, "dur_ms": 600, "phase_lr_deg": -10.0}
]
},
{
"segment": 23,
"transcript": "뉴스도 약합니다 그래서 엔비디아 구글 딥마인드 등이 들어오는 거죠 서로 얻을 건 얻으면 되는 겁니다 이런 관점에서 테슬라 옵티머스는 한국 및 중국 생산이 가능할까요 이 부분도 상당히 애매합니다 이게 어떻게 될 건지 테슬라의 전략으로 보",
"speaker": "speaker_17",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "뉴스도", "start_ms": 120, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "약합니다", "start_ms": 620, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 1250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "엔비디아", "start_ms": 1750, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "구글", "start_ms": 2400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "딥마인드", "start_ms": 2850, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "등이", "start_ms": 3550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "들어오는", "start_ms": 3950, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "거죠", "start_ms": 4550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "서로", "start_ms": 4950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "얻을", "start_ms": 5400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "건", "start_ms": 5800, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "얻으면", "start_ms": 6050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "되는", "start_ms": 6550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "겁니다", "start_ms": 6950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 7450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "관점에서", "start_ms": 7850, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "테슬라", "start_ms": 8500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "옵티머스는", "start_ms": 9050, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 9800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "및", "start_ms": 10250, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "중국", "start_ms": 10500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "생산이", "start_ms": 10950, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "가능할까요", "start_ms": 11550, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 12300, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "부분도", "start_ms": 12550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "상당히", "start_ms": 13050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "애매합니다", "start_ms": 13600, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 14350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 14750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "될", "start_ms": 15250, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "건지", "start_ms": 15550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "테슬라의", "start_ms": 16000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "전략으로", "start_ms": 16650, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "보", "start_ms": 17350, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "이게 삼각형 창이 된 거지? 야 이거 한번 물어봐야 되겠네 계세요? 계십니까? 어 안녕하세요 안녕하세요 아 반갑습니다 안녕하세요",
"speaker": "speaker_18",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "이게", "start_ms": 150, "dur_ms": 350, "phase_lr_deg": -10.0},
{"word": "삼각형", "start_ms": 550, "dur_ms": 500, "phase_lr_deg": -10.0},
{"word": "창이", "start_ms": 1100, "dur_ms": 400, "phase_lr_deg": -10.0},
{"word": "된", "start_ms": 1550, "dur_ms": 250, "phase_lr_deg": -10.0},
{"word": "거지?", "start_ms": 1850, "dur_ms": 450, "phase_lr_deg": -10.0},
{"word": "야", "start_ms": 2400, "dur_ms": 250, "phase_lr_deg": -10.0},
{"word": "이거", "start_ms": 2700, "dur_ms": 350, "phase_lr_deg": -10.0},
{"word": "한번", "start_ms": 3100, "dur_ms": 400, "phase_lr_deg": -10.0},
{"word": "물어봐야", "start_ms": 3550, "dur_ms": 600, "phase_lr_deg": -10.0},
{"word": "되겠네", "start_ms": 4200, "dur_ms": 500, "phase_lr_deg": -10.0},
{"word": "계세요?", "start_ms": 4800, "dur_ms": 600, "phase_lr_deg": -10.0},
{"word": "계십니까?", "start_ms": 5500, "dur_ms": 700, "phase_lr_deg": -10.0},
{"word": "어", "start_ms": 6300, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "안녕하세요", "start_ms": 6550, "dur_ms": 750, "phase_lr_deg": 10.0},
{"word": "안녕하세요", "start_ms": 7350, "dur_ms": 750, "phase_lr_deg": 15.0},
{"word": "아", "start_ms": 8150, "dur_ms": 200, "phase_lr_deg": -10.0},
{"word": "반갑습니다", "start_ms": 8400, "dur_ms": 700, "phase_lr_deg": -10.0},
{"word": "안녕하세요", "start_ms": 9150, "dur_ms": 750, "phase_lr_deg": 15.0}
]
},
{
"segment": 25,
"transcript": "올리는 게 집 짓기 제일 큰 난관이었다죠 돈이 똑 떨어져서 처음으로 은행을 찾아야 했답니다 이게 돈도 원하는 만큼",
"speaker": "speaker_19",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "올리는", "start_ms": 130, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 650, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "집", "start_ms": 900, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "짓기", "start_ms": 1200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "제일", "start_ms": 1650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 2100, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "난관이었다죠", "start_ms": 2400, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "돈이", "start_ms": 3350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "똑", "start_ms": 3800, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "떨어져서", "start_ms": 4100, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "처음으로", "start_ms": 4800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "은행을", "start_ms": 5450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "찾아야", "start_ms": 6000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "했답니다", "start_ms": 6550, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 7350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "돈도", "start_ms": 7750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "원하는", "start_ms": 8200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "만큼", "start_ms": 8750, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "광주 토박이? 어머 선배님 30살이세요? 엄청 어려 보이시는데 광주 생각하면 맛있는 거 많아요 광주 하면은 여기 송정 이잖아요 저기 송정",
"speaker": "speaker_20",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "광주", "start_ms": 140, "dur_ms": 400, "phase_lr_deg": -10.0},
{"word": "토박이?", "start_ms": 600, "dur_ms": 600, "phase_lr_deg": -10.0},
{"word": "어머", "start_ms": 1300, "dur_ms": 400, "phase_lr_deg": 10.0},
{"word": "선배님", "start_ms": 1750, "dur_ms": 500, "phase_lr_deg": 10.0},
{"word": "30살이세요?", "start_ms": 2300, "dur_ms": 850, "phase_lr_deg": 10.0},
{"word": "엄청", "start_ms": 3250, "dur_ms": 450, "phase_lr_deg": -10.0},
{"word": "어려", "start_ms": 3750, "dur_ms": 400, "phase_lr_deg": -10.0},
{"word": "보이시는데", "start_ms": 4200, "dur_ms": 750, "phase_lr_deg": -10.0},
{"word": "광주", "start_ms": 5050, "dur_ms": 450, "phase_lr_deg": -10.0},
{"word": "생각하면", "start_ms": 5550, "dur_ms": 600, "phase_lr_deg": -10.0},
{"word": "맛있는", "start_ms": 6200, "dur_ms": 500, "phase_lr_deg": -10.0},
{"word": "거", "start_ms": 6750, "dur_ms": 200, "phase_lr_deg": -10.0},
{"word": "많아요", "start_ms": 7000, "dur_ms": 500, "phase_lr_deg": -10.0},
{"word": "광주", "start_ms": 7600, "dur_ms": 450, "phase_lr_deg": 10.0},
{"word": "하면은", "start_ms": 8100, "dur_ms": 500, "phase_lr_deg": 10.0},
{"word": "여기", "start_ms": 8650, "dur_ms": 400, "phase_lr_deg": 10.0},
{"word": "송정", "start_ms": 9100, "dur_ms": 450, "phase_lr_deg": 10.0},
{"word": "이잖아요", "start_ms": 9600, "dur_ms": 650, "phase_lr_deg": 10.0},
{"word": "저기", "start_ms": 10350, "dur_ms": 400, "phase_lr_deg": 10.0},
{"word": "송정", "start_ms": 10800, "dur_ms": 500, "phase_lr_deg": 10.0}
]
},
{
"segment": 27,
"transcript": "나도 정확해 소니는 여전히 프리미어 리그에서 뛸 수 있어 내 생각에는 소니가 없었다면 부항가는 해트트릭 할 수 없었을 것 같아 소니의 존재감이 엘리",
"speaker": "speaker_21",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "나도", "start_ms": 120, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "정확해", "start_ms": 600, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "소니는", "start_ms": 1250, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "여전히", "start_ms": 1800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "프리미어", "start_ms": 2350, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "리그에서", "start_ms": 3000, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "뛸", "start_ms": 3700, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 3950, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "있어", "start_ms": 4200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "내", "start_ms": 4700, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "생각에는", "start_ms": 4950, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "소니가", "start_ms": 5650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "없었다면", "start_ms": 6200, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "부항가는", "start_ms": 6900, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "해트트릭", "start_ms": 7550, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 8300, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 8600, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "없었을", "start_ms": 8850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 9400, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "같아", "start_ms": 9650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "소니의", "start_ms": 10150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "존재감이", "start_ms": 10700, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "엘리", "start_ms": 11400, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 29,
"transcript": "서로의 공간을 존중했기에 누릴 수 있는 평화일까요? 지금 보시는 저 뒤의 집이 지금 어머니하고 누님이 살고 계시는 집이거든요 아 저기가 어머니 댁이에요? 네",
"speaker": "speaker_22",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "서로의", "start_ms": 110, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "공간을", "start_ms": 650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "존중했기에", "start_ms": 1200, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "누릴", "start_ms": 2050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 2500, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 2750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "평화일까요?", "start_ms": 3150, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 4050, "dur_ms": 450, "phase_lr_deg": 10.0},
{"word": "보시는", "start_ms": 4550, "dur_ms": 500, "phase_lr_deg": 10.0},
{"word": "저", "start_ms": 5100, "dur_ms": 250, "phase_lr_deg": 10.0},
{"word": "뒤의", "start_ms": 5400, "dur_ms": 400, "phase_lr_deg": 10.0},
{"word": "집이", "start_ms": 5850, "dur_ms": 450, "phase_lr_deg": 10.0},
{"word": "지금", "start_ms": 6350, "dur_ms": 450, "phase_lr_deg": 10.0},
{"word": "어머니하고", "start_ms": 6850, "dur_ms": 700, "phase_lr_deg": 10.0},
{"word": "누님이", "start_ms": 7600, "dur_ms": 500, "phase_lr_deg": 10.0},
{"word": "살고", "start_ms": 8150, "dur_ms": 400, "phase_lr_deg": 10.0},
{"word": "계시는", "start_ms": 8600, "dur_ms": 500, "phase_lr_deg": 10.0},
{"word": "집이거든요", "start_ms": 9150, "dur_ms": 800, "phase_lr_deg": 10.0},
{"word": "아", "start_ms": 10000, "dur_ms": 200, "phase_lr_deg": -10.0},
{"word": "저기가", "start_ms": 10250, "dur_ms": 500, "phase_lr_deg": -10.0},
{"word": "어머니", "start_ms": 10800, "dur_ms": 500, "phase_lr_deg": -10.0},
{"word": "댁이에요?", "start_ms": 11350, "dur_ms": 650, "phase_lr_deg": -10.0},
{"word": "네", "start_ms": 12050, "dur_ms": 300, "phase_lr_deg": 10.0}
]
},
{
"segment": 30,
"transcript": "앞으로 나아졌다라고 하면은 그리고 맥북이라든가 최근 이런 컴퓨팅을 충분히 지원하면서 디램이 확보된 이런 맥북프로 같은 디바이스들 아니면 맥 스튜디오 같은 디바이스들을 가져와서 처리하게 되면은 굉장히",
"speaker": "speaker_23",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "앞으로", "start_ms": 100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "나아졌다라고", "start_ms": 650, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "하면은", "start_ms": 1500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 2050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "맥북이라든가", "start_ms": 2550, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "최근", "start_ms": 3400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 3850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "컴퓨팅을", "start_ms": 4250, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "충분히", "start_ms": 5000, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "지원하면서", "start_ms": 5600, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "디램이", "start_ms": 6400, "dur_ms": 500, "phase_lr_deg": 0.

---
*Generated by WKAppBot ask gemini — 2026-04-06 07:01:02*
