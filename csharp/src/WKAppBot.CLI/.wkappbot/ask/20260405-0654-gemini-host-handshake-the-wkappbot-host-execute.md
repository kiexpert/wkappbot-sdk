# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=17708  time=06:52:45  uptime=9826s  mem=426MB  system_processes=353"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_065156.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 5652ms (file: 625-276-213-271-120-120-752-217-176-123-321-236-231-762-756-130-123-120-172-120-173-231-213-276-123-213-217-217-126-210-326-213-312-320-163-231-126-216-102-173-312-213-312-726-1_W.mp3)
  Segment 2: 6652ms ~ 7228ms (file: 310-150-153-135-173-513-153-135-137-137-510-513-513-315-517-571-517-537-537-357-571-351-157-135 510-153-130-137-153-135-510-135-130-163-135-157-157-157-571-571-513-537-573-571-5_W.mp3)
  Segment 3: 8228ms ~ 16076ms (file: 230-231-102-320-130-320-321-537-573-135-276-126-167-312-762-672-320-310-267-267-176-267-762-765-672-670-102-162-107-162-571-130-321-316-237-765-756-276-276-231-276-267-132-132-3_W.mp3)
  Segment 4: 17076ms ~ 17652ms (file: 123-102 206-126-123-261-562-621-361-765-765-765-765_W.mp3)
  Segment 5: 18652ms ~ 24016ms (file: 372-176-270-627-126-271-325-263-163-302-312-257-752-736-732-127-213-712-712-572-761-276-132-132-123-726-261-276-267-213-126-237-237-213-137-173-173-216-231-273-213-123-216-230-1_W_stt_i_have_one.mp3)
  Segment 6: 25016ms ~ 41072ms (file: 312-103-607-216-213-752-267-276-102-201-231-206-203-103-120-206-267-627-216-216-706-206-201-765-203-321-213-120-130-261-654-507-132-503-675-670-736-361-534-536-562-135-152-321-6_W.mp3)
  Segment 7: 42072ms ~ 42648ms (file: 574 547-547-574-504-501-157-105-501-570-571-510-507-105-517-150-573-351-540-570-574-574-574-574-574-754-754-574-570-570-501-510-570-574-570-517-157-150-150-510-571-517 510-571-5_W.mp3)
  Segment 8: 43648ms ~ 44224ms (file: 317-361-356-617-106-267-617-302-120-706-167-754-756-750-231-231-231-657-567-761-126-621-716-260-732-627-106-612-607-103-103-301-310-130-361-765-236-312-103-506-567-506-630-627-6_W.mp3)
  Segment 9: 45224ms ~ 45800ms (file: 175-710-175-170-710-170-107-175-175-571-751-751-716-762-726-723-762-726-271-712-176-726-762-276-175-175-751-175-157-751-175-175-710-175-715-170-170-710-175-710-715-176-701 107-1_V.mp3)
  Segment 10: 46800ms ~ 56340ms (file: 163-261-276-607-210-130-273-231-216-276-263-236-326-320-102-103-230-206-206-216-267-216-750-123-312 761-542-130-107-132-576-527-265-261-132-136-312-132-130-527-103-210-236-253-7_W.mp3)
  Segment 11: 57340ms ~ 57916ms (file: 173-106-137-107-607-516-716-615-613_V.mp3)
  Segment 12: 58916ms ~ 59492ms (file: 267-627-267-267-267-627-267-276-267-627-267-267-267-627-627-627-213-263-267-261-267-267-267-267-261-267-267-263-267-267-261-261-263-267-263-621-261-267-261-261-236-627-267-216-2_W.mp3)
  Segment 13: 60492ms ~ 61068ms (file: 510-105-150-570-150-150-510 501-501-501-510-501-517-105-571-501-510-510-516-105-150-105-105-510-175-501-517-510-510-510-501-150-501-517-501-150 715-150-150-157-507-501-150-517-5_V.mp3)
  Segment 14: 62068ms ~ 65452ms (file: 321 103-123-150-310-213-231-621-301-102-365 251-217-164-326-647-745-136_V.mp3)
  Segment 15: 66452ms ~ 67028ms (file: 312-103-132-136-135-710-176-371-173-765-675-675_W.mp3)
  Segment 16: 68028ms ~ 68604ms (file: 132-132-175-176-107-103-612-750-760-710-731-710-175-172-107-103-132-120-172-317-716-137-172-312-126-127-710-136-176-712-137-567-135-172-103-726-721-132-216-125-716-257-127-372-1_V.mp3)
  Segment 17: 69604ms ~ 79108ms (file: 723-267-132-231-301-576-172-231-235-270-230-253-260-321-310-517-103-130-312-310-137-301-713-570-157-137-302-137-103-176-312-230-213-127-726-762-270-103-210-207-271-270-172-137-1_V.mp3)
  Segment 18: 80108ms ~ 91736ms (file: 123-231-102-263-231-236-102-236-203-123-231-103-206-236-263-123-260-231-261-236-236-321-236-102-120-236-263-725-236-326-162-210-231-136-275-321-120-137-173-130-137-317-312-175-1_W.mp3)
  Segment 19: 92736ms ~ 93312ms (file: 127-217-217-712-712-127-172-720-271-702-705-751_W.mp3)
  Segment 20: 94312ms ~ 94888ms (file: 157-715-517-517-571-571-175-157-751-571-573-571-517-571-571-570-576-570 157-751-175-715-175-175-157-175-175-715-175-157-715-571-751-175-751-571-570-570-153-517-510-153-157-751-1_V.mp3)
  Segment 21: 95888ms ~ 96464ms (file: 754-754-756-754-756-754-754-754-754-754-754-157-105-150-150-150-150-157-175-156-157-715-716-167-176-175-167-761-175-761-751-765-615-175-176-156-174-754-754-754-754-754-754-754-7_W.mp3)
  Segment 22: 97464ms ~ 109668ms (file: 627-672-672-135-263-267-130-132-316-327-753-276-267-267-271 132-574-574-102-132-367-103-513-137-751-754-720-207-713-230-321-130-136-230-203-120-102-103-174-672-670-263-367-362-2_W.mp3)
  Segment 23: 110668ms ~ 111244ms (file: 315-315-315-315-765-657-735-357-753-763-371-317-315-312-316-136-315-312-137-136-136-257-721-163-136-136-136-162-136-136-132-136-132-132-312-126-625-635-163-136-136-162-163-132-1_W.mp3)
  Segment 24: 112244ms ~ 112820ms (file: 637-613-673-675-637-167-163-167-637-617-617-617-637-617 750-750-375-370-631-632-361-632-627-675-672-671-617-617-631-637-631-316-316-137-136-316-137-175-307 371-703-730-310-137-1_W.mp3)
  Segment 25: 113820ms ~ 114396ms (file: 173-107-175-751-317-170-710-173-105-157 571-517-153-517-517-150-175-715-153-157-751-517-175-105-173-157-173-175-173-715-175-175-157-571-573 571-570-510-175-157-157-157-517-715-1_V.mp3)
  Segment 26: 115396ms ~ 126484ms (file: 765-210-201-271-103-130-302-321-210-321-230-310-123-213-301-102-310-102-752-627-756-103-102-312-201-312-231-201-725-752-275-302-230-157-250-130-201-213-203-173-174-123-261-765-2_V.mp3)
  Segment 27: 127484ms ~ 128060ms (file: 167-156-165-156-163 123-123-120-136-163_W.mp3)
  Segment 28: 129060ms ~ 136944ms (file: 205-320-302-301-310-310-206-162-127-103-310-621-123-620-150-501-257-132-203-621-123-250-263-120-527-273-726-210-120-123-103-132-570-165-310-310-207-672-715-123-537-120-206-132-1_V.mp3)
  Segment 29: 137944ms ~ 138520ms (file: 765-726-763-762-765-763-763-230-312-620-627-236-301-317-137-103-137_V.mp3)
  Segment 30: 139520ms ~ 148196ms (file: 231-203-127-260-210-326-120-127-301-601-263-106-230-237-265-230-236-213-362-106-176-103-123-130-276-756-267-267-261-123-320-167-270-753-712-756-123-327-726-327-273-207-203-107-2_W.mp3)
  Segment 31: 149196ms ~ 149772ms (file: 567-567 517-156-157-156-561-561-572-567-576-561-561-567-516-561-516-517-516-531-516-531-576-576-675-674-675-675-675-617-761-617-671-167-631-136 653-651-615-615-612-657-657-635-6_W.mp3)
  Segment 32: 150772ms ~ 161608ms (file: 103-107-103-231-572-231-103-302-176-572-230-132-723-102-270-170-310-276-210-320-360-106-136-312-537-235-572-256-267-532-316-317-302-573-137-213-571-310-237-231-320-327-325-253-2_W.mp3)
  Segment 33: 162608ms ~ 163184ms (file: 510-501-574-576-574-547-574-574-574-574-574-573 570-517-510-513-157-507-501-501-157-150-501-150-507-510-105-503-510-517-570-571-570-517-517-570-576-574-574-574-576-574-547-574-5_W.mp3)
  Segment 34: 164184ms ~ 164760ms (file: 165-574-574-574-312-301-301-312 136-103-136-617-312-136-103-167-320-163-213-103-132-156-126-126-165-651-106-613-312-136-513-136-103-130-316-731-326-106-325-206-103-132-130-751-7_V.mp3)
  Segment 35: 165760ms ~ 166336ms (file: 310-312-102-361-607-627-612-302-203-260-206-260-206-260-260-103-756-762-762-176-103-170-312-260-362-623 765-675-132-312-126-123-321-725-130 621-627-672-623-623-361 627-267-263-2_W.mp3)
  Segment 36: 167336ms ~ 167912ms (file: 130-341-143-103-130-143-130-103-104-103-103-134-104-314-134-143-134-103-143-134-134-103 301-310-134-103-314-103-103-103-143-135-134-130-103-143-104-105-143-134 301-341-103-317-3_V.mp3)
  Segment 37: 168912ms ~ 181908ms (file: 507-572-257-754-203-527-265-325-275-526-627-523-352-206-206-102-237-706-756-567-103-320-231-361-657-573-651-301-206-361-316-316-136-136-731-726 236-123-102-103-163-761-623-326-1_W.mp3)
  Segment 38: 182908ms ~ 183484ms (file: 163-312-135-163-310-130-103-106-615-103-160-106-356-165_V.mp3)
  Segment 39: 184484ms ~ 185060ms (file: 213-312-103-106-132-136-163-150-165-315-167-136-760-176-103-176-312-103-103 136-130-136-130-136-103-130-130-136-135-136-762-517-673-671-136-316-136-103-137-617-701-310-316-106-1_V.mp3)
  Segment 40: 186060ms ~ 186636ms (file: 257-725-312-261-357-301-371-713-657-216-257-275-507-513-751-135-617 256-752-572-756-263-312-572-256-235-234-706-132-672-523-756-265-267-675-326-256-153-237-276-372-725-527-517-5_V.mp3)
  Segment 41: 187636ms ~ 194368ms (file: 574-230-237-312-750-132-725-127-351-136-713-756-752-547-725-725-263-706-162-675-162-205-205-236-263-123-765-376-276-273-375-750-103-276-217-723-673-671-672-627-756-312-673-765-7_W.mp3)
  Segment 42: 195368ms ~ 203216ms (file: 327-273-267-230-237-130-273-317-237-236-327-726-267-326-267-231-230-237-230-275-103-136-276-120-276-236-726-276-321-217-726-726-706-273-237-237-237-263-213-127-206-102-372-321-7_W.mp3)
  Segment 43: 204216ms ~ 204792ms (file: 105-105-105-150-157-157-176-167-756-160-176-157-175-175-517-157-157-715-157-571-517-517-517-571-176 514-105-105-105-150-105-170-156-157-175-170-167-176-175-157-157-517-157-175-1_V.mp3)
  Segment 44: 205792ms ~ 206368ms (file: 316-231-123-126-123-136-213-326-130-103-132-163-130-126-130-126-103-102-136-126-310-102-106_V.mp3)
  Segment 45: 207368ms ~ 207944ms (file: 615-762-750-173-753-367-527-267-276-235-132-213-215 267-672-231 756-752-362-217-365-725-756-726 217-572-625-751-326-536-570-513-571-376-263-312-527-376-573 756-270-203-362-713-7_V.mp3)
  Segment 46: 208944ms ~ 217656ms (file: 217-367-173-321-162-367-716-736-136-217-301-213-120-102-163-310-103-671-316-126-132-123-103-617-130-132-216-726-136-762-120-123-216-167-127-126-103-130-261-162-613-267-176-376-1_W.mp3)
  Segment 47: 218656ms ~ 219232ms (file: 621-167-631-326-136-312-261-306-310-132-316-627-236_V.mp3)
  Segment 48: 220232ms ~ 220808ms (file: 172-702-276-723-756-375-523-157-256-267-537-153-571-750-526-625-750-172-573-302-216-267-125-153-570-167-571-326-713-273-235-576-657-753-750-670-572-265-521-527-123-265-527-126-5_V.mp3)
  Segment 49: 221808ms ~ 222384ms (file: 652-627 671-675-765-765-765-675-673-763-673-673-673-675-673-673-675-763-675-672-623-621-623-632-623-612-632-623-362-632-631-362-632-623-632-623-675-675-675-675-675-675-765-765-7_W.mp3)
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
> **Time**: 2026-04-05 06:54:32
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "I hope they like it a lot because I like the kimchi",
"speaker": "speaker_1",
"confidence": 0.92,
"language": "en",
"silence_before_ms": 250,
"audio_type": "mixed",
"words": [
{"word": "I", "start_ms": 250, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "hope", "start_ms": 400, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "they", "start_ms": 700, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "like", "start_ms": 900, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "it", "start_ms": 1150, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "a", "start_ms": 1250, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "lot", "start_ms": 1350, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "because", "start_ms": 1750, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "I", "start_ms": 2150, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "like", "start_ms": 2300, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "the", "start_ms": 2550, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "kimchi", "start_ms": 2700, "dur_ms": 500, "phase_lr_deg": -5.0}
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
"transcript": "버텨야 하는 나라였습니다 195, 60년대 미국의 식량 원조가 없었다면 그 시기를 견디기 훨씬 더 어려웠을",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "버텨야", "start_ms": 150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "나라였습니다", "start_ms": 1050, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "195,", ""start_ms": 2000, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "60년대", ""start_ms": 2700, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "미국의", "start_ms": 3500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "식량", "start_ms": 4000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "원조가", "start_ms": 4450, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "없었다면", "start_ms": 5000, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 5700, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "시기를", "start_ms": 5900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "견디기", "start_ms": 6400, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "훨씬", "start_ms": 6950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 7350, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "어려웠을", "start_ms": 7550, "dur_ms": 300, "phase_lr_deg": 0.0}
]
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
"transcript": "그리고 실제 납품으로 증명되기 때문입니다 군에스북 영상을 유익하게 보셨다면 좋아요와 구독 꼭 부탁드리겠습니다",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "그리고", "start_ms": 100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "실제", "start_ms": 550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "납품으로", "start_ms": 950, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "증명되기", "start_ms": 1550, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "때문입니다", "start_ms": 2200, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "군에스북", "start_ms": 3000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "영상을", "start_ms": 3600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "유익하게", "start_ms": 4050, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "보셨다면", "start_ms": 4650, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "좋아요와", "start_ms": 5200, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "구독", "start_ms": 5850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "꼭", "start_ms": 6250, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "부탁드리겠습니다", "start_ms": 6450, "dur_ms": 900, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "자연에 묻혀서 살면은 마음이 편해요 교통사고와 질병으로 건강이 나빠지고 코로나까지 겪으면서",
"speaker": "speaker_4",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 1100,
"audio_type": "speech",
"words": [
{"word": "자연에", "start_ms": 1100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "묻혀서", "start_ms": 1600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "살면은", "start_ms": 2050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "마음이", "start_ms": 2550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "편해요", "start_ms": 2950, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "교통사고와", "start_ms": 11000, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "질병으로", "start_ms": 11800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "건강이", "start_ms": 12400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "나빠지고", "start_ms": 12900, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "코로나까지", "start_ms": 13500, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "겪으면서", "start_ms": 14200, "dur_ms": 600, "phase_lr_deg": 0.0}
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
"transcript": "포커스 양보가 필요합니다",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "포커스", "start_ms": 50, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "양보가", "start_ms": 550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "필요합니다", "start_ms": 1000, "dur_ms": 570, "phase_lr_deg": 0.0}
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
"transcript": "죽을 수도 있겠다는 생각을 처음으로 했습니다 선원들은 보통 승선하면 선사와 정보 서약을 맺는데요",
"speaker": "speaker_6",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "죽을", "start_ms": 100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "수도", "start_ms": 400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "있겠다는", "start_ms": 650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "생각을", "start_ms": 1150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "처음으로", "start_ms": 1550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "했습니다", "start_ms": 2050, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "선원들은", "start_ms": 4000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "보통", "start_ms": 4600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "승선하면", "start_ms": 4950, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "선사와", "start_ms": 5550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "정보", "start_ms": 6000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "서약을", "start_ms": 6350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "맺는데요", "start_ms": 6800, "dur_ms": 500, "phase_lr_deg": 0.0}
]
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
"transcript": "스페이스 X와 제주지사가 뭔가 했다는 거는 확실한 걸로 보입니다 사업용으로는 스페이스 X",
"speaker": "speaker_7",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "스페이스", "start_ms": 50, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "X와", "start_ms": 500, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "제주지사가", "start_ms": 750, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "뭔가", "start_ms": 1450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "했다는", "start_ms": 1800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "거는", "start_ms": 2250, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "확실한", "start_ms": 2500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "걸로", "start_ms": 3000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "보입니다", "start_ms": 3300, "dur_ms": 550, "phase_lr_deg": 0.0}
]
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
"transcript": "기둥 덕분에 입구부터 압도적인 분위기입니다 그 검색까지 치니까 마치 친한 거 같이",
"speaker": "speaker_8",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "기둥", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "덕분에", "start_ms": 550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "입구부터", "start_ms": 1000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "압도적인", "start_ms": 1600, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "분위기입니다", "start_ms": 2250, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 6000, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "검색까지", "start_ms": 6200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "치니까", "start_ms": 6800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "마치", "start_ms": 7250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "친한", "start_ms": 7650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 8000, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 8150, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "이 미사일의 목표물 요격률은 놀랍게도 무려 96%에 달했다고 합니다 이 한 번의 실전으로 한국의 방공 미사일은 단숨에 신급 평가를 받게 되었습니다 더 치명적인 점은 가격이었습니다",
"speaker": "speaker_9",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "미사일의", "start_ms": 250, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "목표물", "start_ms": 750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "요격률은", "start_ms": 1250, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "놀랍게도", "start_ms": 1900, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "무려", "start_ms": 2550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "96%에", "start_ms": 2900, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "달했다고", "start_ms": 3700, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "합니다", "start_ms": 4300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 4700, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 4850, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "번의", "start_ms": 5000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "실전으로", "start_ms": 5300, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "한국의", "start_ms": 5950, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "방공", "start_ms": 6450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "미사일은", "start_ms": 6850, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "단숨에", "start_ms": 7500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "신급", "start_ms": 7950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "평가를", "start_ms": 8350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "받게", "start_ms": 8850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "되었습니다", "start_ms": 9200, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 9900, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "치명적인", "start_ms": 10100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "점은", "start_ms": 10700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "가격이었습니다", "start_ms": 11050, "dur_ms": 900, "phase_lr_deg": 0.0}
]
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
"transcript": "관객들 반응을 보면 그냥 재미있다 수준이 아니라 거의 폭발적이에요 그런데 이 영화는 작품성만큼이나 역대급 관전 포인트가 하나 더 있죠 바로 영화의 중심을",
"speaker": "speaker_10",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "관객들", "start_ms": 200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "반응을", "start_ms": 700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "보면", "start_ms": 1150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 1500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "재미있다", "start_ms": 1850, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "수준이", "start_ms": 2450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 2900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "거의", "start_ms": 3300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "폭발적이에요", "start_ms": 3650, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "그런데", "start_ms": 6000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 6450, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "영화는", "start_ms": 6600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "작품성만큼이나", "start_ms": 7050, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "역대급", "start_ms": 8050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "관전", "start_ms": 8500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "포인트가", "start_ms": 8850, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "하나", "start_ms": 9450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 9750, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "있죠", "start_ms": 9950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 10500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "영화의", "start_ms": 10850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "중심을", "start_ms": 11300, "dur_ms": 500, "phase_lr_deg": 0.0}
]
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
"transcript": "내가 빼는 속도를 줄이면서 금액을 높여나가면 그렇게 하면은 나한테 맞는 시장에서 금액을 계속 높여나갈 수 있고",
"speaker": "speaker_11",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "내가", "start_ms": 100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "빼는", "start_ms": 400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "속도를", "start_ms": 750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "줄이면서", "start_ms": 1250, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "금액을", "start_ms": 1850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "높여나가면", "start_ms": 2350, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "그렇게", "start_ms": 6000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "하면은", "start_ms": 6450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "나한테", "start_ms": 6900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "맞는", "start_ms": 7400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "시장에서", "start_ms": 7750, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "금액을", "start_ms": 8400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 8900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "높여나갈", "start_ms": 9300, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 10000, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있고", "start_ms": 10150, "dur_ms": 300, "phase_lr_deg": 0.0}
]
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
"transcript": "아 리스크 관리가 그냥 되는 거네요 그렇게 하는 것 자체만으로 보급 받아 석 달 치 식량이 남았습니다",
"speaker": "speaker_12",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "아", "start_ms": 100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "리스크", "start_ms": 300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "관리가", "start_ms": 800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 1250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "되는", "start_ms": 1600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "거네요", "start_ms": 1900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "그렇게", "start_ms": 2350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 2800, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 3100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "자체만으로", "start_ms": 3250, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "보급", "start_ms": 5000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "받아", "start_ms": 5450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "석", "start_ms": 5800, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "달", "start_ms": 6000, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "치", "start_ms": 6150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "식량이", "start_ms": 6300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "남았습니다", "start_ms": 6800, "dur_ms": 700, "phase_lr_deg": 0.0}
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
"transcript": "고추장과 간장 같은 양념이 부족할까 봐 조리장이 걱정하지만 음식량은 충분하다고 합니다",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "고추장과", "start_ms": 100, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "간장", "start_ms": 800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "같은", "start_ms": 1250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "양념이", "start_ms": 1600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "부족할까", "start_ms": 2100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "봐", "start_ms": 2700, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "조리장이", "start_ms": 3000, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "걱정하지만", "start_ms": 3650, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "음식량은", "start_ms": 4500, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "충분하다고", "start_ms": 5150, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "합니다", "start_ms": 5900, "dur_ms": 450, "phase_lr_deg": 0.0}
]
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
"transcript": "샤워는 평소처럼 바닷물을 정수했습니다 태안대실 실습생과 계약이 끝난 선원 등 10명이 하선해 현재",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "샤워는", "start_ms": 150, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "평소처럼", "start_ms": 700, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "바닷물을", "start_ms": 1350, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "정수했습니다", "start_ms": 1950, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "태안대실", "start_ms": 6000, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "실습생과", "start_ms": 6700, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "계약이", "start_ms": 7400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "끝난", "start_ms": 7900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "선원", "start_ms": 8300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "등", "start_ms": 8750, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "10명이", "start_ms": 8950, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "하선해", "start_ms": 9550, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "현재", "start_ms": 10150, "dur_ms": 500, "phase_lr_deg": 0.0}
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
"transcript": "기술 MCP 플러그인 이런 데 집중을 했습니다 하네스는 조금 다른데요",
"speaker": "speaker_13",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "기술", "start_ms": 200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "MCP", "start_ms": 650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "플러그인", "start_ms": 1050, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 1700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "데", "start_ms": 2050, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "집중을", "start_ms": 2200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "했습니다", "start_ms": 2700, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "하네스는", "start_ms": 6000, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 6700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "다른데요", "start_ms": 7100, "dur_ms": 600, "phase_lr_deg": 0.0}
]
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
"transcript": "이제 그 강력한 에이전트를 어떻게 통제하고 컨트롤해서 우리가 원하는 것을 이루어내게 할 것인가에 달려 있습니다",
"speaker": "speaker_13",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 500, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "강력한", "start_ms": 650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "에이전트를", "start_ms": 1150, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 1850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "통제하고", "start_ms": 2300, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "컨트롤해서", "start_ms": 2950, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "우리가", "start_ms": 3750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "원하는", "start_ms": 4200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "것을", "start_ms": 4600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이루어내게", "start_ms": 4900, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 5650, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "것인가에", "start_ms": 5850, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "달려", "start_ms": 6500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 6850, "dur_ms": 600, "phase_lr_deg": 0.0}
]
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
"transcript": "매번 이건 틀렸어 저기로 가야 해라고 고쳐줄 필요가",
"speaker": "speaker_13",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "매번", "start_ms": 100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이건", "start_ms": 450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "틀렸어", "start_ms": 750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "저기로", "start_ms": 1250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "가야", "start_ms": 1700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "해라고", "start_ms": 2050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "고쳐줄", "start_ms": 2550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "필요가", "start_ms": 3050, "dur_ms": 500, "phase_lr_deg": 0.0}
]
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
"transcript": "해서는 좀 긍정적으로 보는 것 같습니다 그리고 실제로 어제 호르무즈 해협을 지나가는 선박들의 횟수가 통행 횟수가 굉장히 많이 증가했다고 해요",
"speaker": "speaker_7",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "해서는", "start_ms": 200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 650, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "긍정적으로", "start_ms": 850, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "보는", "start_ms": 1600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 1950, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "같습니다", "start_ms": 2100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 3000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "실제로", "start_ms": 3450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "어제", "start_ms": 3950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "호르무즈", "start_ms": 4300, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "해협을", "start_ms": 4900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "지나가는", "start_ms": 5400, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "선박들의", "start_ms": 6050, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "횟수가", "start_ms": 6700, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "통행", "start_ms": 7250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "횟수가", "start_ms": 7650, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "굉장히", "start_ms": 8200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 8700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "증가했다고", "start_ms": 9050, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "해요", "start_ms": 9850, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "폭발적인 경제 성장과 전 세계를 매료시키는 문화적 저력 우리가 일상적으로 누리고 있는 이 자부심 넘치는 모습들은 조건이 완벽하게 갖춰진 비옥한 평야에 온실 속",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "폭발적인", "start_ms": 100, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "경제", "start_ms": 750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "성장과", "start_ms": 1150, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "전", "start_ms": 1700, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "세계를", "start_ms": 1900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "매료시키는", "start_ms": 2400, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "문화적", "start_ms": 3150, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "저력", "start_ms": 3700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "우리가", "start_ms": 4150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "일상적으로", "start_ms": 4600, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "누리고", "start_ms": 5450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 5950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 6250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "자부심", "start_ms": 6400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "넘치는", "start_ms": 6900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "모습들은", "start_ms": 7400, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "조건이", "start_ms": 8050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "완벽하게", "start_ms": 8550, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "갖춰진", "start_ms": 9200, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "비옥한", "start_ms": 9750, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "평야에", "start_ms": 10300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "온실", "start_ms": 10800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "속", "start_ms": 11200, "dur_ms": 200, "phase_lr_deg": 0.0}
]
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
"transcript": "어떻게 일어나 나오면 저렇게 지네들 저렇게 다 치고 있잖아 고양이가 저렇게",
"speaker": "speaker_14",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 1000,
"audio_type": "speech",
"words": [
{"word": "어떻게", "start_ms": 1000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "일어나", "start_ms": 1500, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "나오면", "start_ms": 2050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "저렇게", "start_ms": 2550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "지네들", "start_ms": 3050, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "저렇게", "start_ms": 3650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 4150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "치고", "start_ms": 4300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "있잖아", "start_ms": 4700, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "고양이가", "start_ms": 5250, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "저렇게", "start_ms": 5900, "dur_ms": 500, "phase_lr_deg": 0.0}
]
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
*Generated by WKAppBot ask gemini — 2026-04-05 06:54:32*
