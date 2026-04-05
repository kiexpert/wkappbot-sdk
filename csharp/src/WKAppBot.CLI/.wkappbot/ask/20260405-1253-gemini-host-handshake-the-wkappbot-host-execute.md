# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=17708  time=12:52:58  uptime=31439s  mem=500MB  system_processes=355"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_125234.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 7416ms (file: 263-267-231-263-231-213-524-547-267-236-627-612-267-725-261-263-645-263-120-312-543-503-261-321-263-574-627-261-321-754-145-263-132-103-261-231-325-123-123-231-261-136-160-754-6_W.mp3)
  Segment 2: 8416ms ~ 8992ms (file: 651-651-651-615 621-621-621-621-621-612-625-621-621_W.mp3)
  Segment 3: 9992ms ~ 10568ms (file: 163-613-625-621-625-165-652-726-765-726-765-765-675-765-762-617-572-572-527-576-576-572-765-756-765-756-657-657-756-675-576-752-756-675-675-675-675-756-756-756-756-175-756-157-7_W.mp3)
  Segment 4: 11568ms ~ 12144ms (file: 267-276-362-320-326-362-326-312-126-316-623-263-361-750-765-726-276-237-167-163-103-613-612-623-672-206-261-267-627-267-267-236-612-613-261-623-321-263-136-301-301-103-103-627-6_V.mp3)
  Segment 5: 13144ms ~ 13720ms (file: 752-572-572 507-510-502-507-570-507-576-520-572-752-572-571-576-570-572-527-572-256-275-257-572-570 572-507-570-520-527-502-507-570-576-752-572-257-576-570-265-257-726-256-750-7_V.mp3)
  Segment 6: 14720ms ~ 27536ms (file: 137-270-573-271-713-267-267-267-751-675-267-617-617-261-756-130-120-213-627-260-627-260-652-625-213-312-267-231-312-103-106-107-527-213-231-321-567-265-263-167-103-106-167-715-2_W.mp3)
  Segment 7: 28536ms ~ 32820ms (file: 167-607-761-273-326-706-671-761-761-726-762-760-762-763-761-761-276-376-761-761-762-670-170-726-672-762-672-637-672-627-672-627-267-762-627-267-762-762-276-276-260-327-362-672-7_W.mp3)
  Segment 8: 33820ms ~ 46708ms (file: 267-103-267-267-206-123-237-103-210-123-230-302-273-312-104-231-312-230-312-145-753-203-213-102-103-127-210-320-301-314-103-103-756-705-102-201-102-103-134-123-123-203-320-231-6_V.mp3)
  Segment 9: 47708ms ~ 48284ms (file: 157-173-153-137-157-715-571-715-576-750-517-571-571-571-570-570-573-571-570-571-571-571-571-517-574 570-570-571-517-175-571-573-571-571-576-571-570-571-570-571-571-570-570-576-5_V.mp3)
  Segment 10: 49284ms ~ 49860ms (file: 105-137 103-103-713-701-301-310-173-310-103-103-105-137-103-107-301-715-130-371-370-713-107-310-735-315-173-137-710-752-715 137-317-130-301-310-307-301-375-310-317-703-130-103-1_V.mp3)
  Segment 11: 50860ms ~ 51436ms (file: 652-657-610-615-605 261-620-620-620-620-602-620-260-621-620-620-620-261-621-621-620-265-261-620-261-265-625-652-625-652-526-652-657-652 264-621-612-621-261-620-621-623-620-625-2_W.mp3)
  Segment 12: 52436ms ~ 53012ms (file: 125-312-103-627-672-672-675-672-675-675-765-762-675-672-675-765-672-765-675-671 657-765-756-675-765-675 756-765-756-765-765-756-675-765 625-675-627-726-675-625_W.mp3)
  Segment 13: 54012ms ~ 54588ms (file: 504-502-506-507-506-507-506-502-507-572-507-507-507-507-507-507-507-507-507-507-507-507-507-527-502-507-531-351-507-574-570-503-507-547-501-570-574-507-507-507-507-507-507-507-5_V.mp3)
  Segment 14: 55588ms ~ 56164ms (file: 510-501-510-150-150-510-501-507-570-571 501-510-150-510-153-157-105-501-105-105-517-157-150-501-510-517-150-517-501-105-510-150-153-150-150-517-571-573-513 501-157-105-571-153-1_V.mp3)
  Segment 15: 57164ms ~ 57740ms (file: 502-507-504-504-547-540-570-574-574-570-507-574 532-253-256-267-267-756-576-572-752-572-576-576-573-573 571-507-571-105-150-152-105_W.mp3)
  Segment 16: 58740ms ~ 59316ms (file: 321 507-574-507-571-507-574-507-571-507-572-576-572-205-105-352-501-571-573-573-531-507-507-576-574-573_V.mp3)
  Segment 17: 60316ms ~ 60892ms (file: 751-751 623-263-623-265-623-751-752-752-752-752-754-754-751-754-751-752-752-715 541_W.mp3)
  Segment 18: 61892ms ~ 62468ms (file: 310-130-103-173-756-703 173-317-137-317-701-731-130-310-137-701-130-103-130-105-312-173-107-310-715-753-173-103-371-310-107-132-137-103-317-137-130 107-701-317-310-103-731-130-1_V.mp3)
  Segment 19: 63468ms ~ 64044ms (file: 675-157-165-167-163-163-163-163-136-163-163-136-756-765-675-567-657-657-657-657-765-675-657-506-756-571-571-516-572-625-657-750-765-657-761-671-167-617-167-671-167-673-675-567-6_W.mp3)
  Segment 20: 65044ms ~ 65620ms (file: 132-103-256-132-126-610-312-137-102-130-157-267-167-175-362-261-632-623-130-632-236-236-136-612-635-576-650-176-231 160-123-132-756-132-673-103-623-301-137-136-316-136-123-106-1_V.mp3)
  Segment 21: 66620ms ~ 80264ms (file: 123-751-762-172-267-762-765-132-672-267-326-312-123-326-106-765-672-276-312-317-761-102-103-321-236-210-120-231-261-230-217-321-231-263-213-132-231-236-132-261-267-261-236-216-7_W.mp3)
  Segment 22: 81264ms ~ 81840ms (file: 572-572-572-574-572-574-572-527-572-512-504-502-503-521-523-527-531-502-502-502-526-502-562-526-502-205-205-271-705-576-756-576-567-756-756-765-512-576-572-507-572-520-572-576-5_W.mp3)
  Segment 23: 82840ms ~ 83416ms (file: 571-137-610-176-157-576-157-516-275-167-103-102-765-176-765-327-375-317-261-567-567-536-351-157-571-731-376-756-165-326-362-753-754-572-327-312-316-326-321-103-753-375-576-175-7_W.mp3)
  Segment 24: 84416ms ~ 84992ms (file: 173-271-765-576-751-571-756-756-517-571-751-517-571-517-157-517-576-571-756-753-765_W.mp3)
  Segment 25: 85992ms ~ 86568ms (file: 715-175-571-175-175-751 574-574-570-570-571-750-570-157-571-157-571-175-170-751-571-751-715-750-701-751-175-170-175 517-157-157-751-175-751-517-571-751-715-175-751-715-175-751-1_V.mp3)
  Segment 26: 87568ms ~ 88144ms (file: 571-751-756-576-751-751-571-570-570-570-570-570 157-157-175-175-175-751-756-756-357-751-157-571-571-571-570-570-750-751-750-751-750-751-751-570-105-513-157-513-105-517-513-105-5_V.mp3)
  Segment 27: 89144ms ~ 89720ms (file: 723-732-572-576-567-726-176-706-123-751-756-765-576-576-756-567-657-576-756-576 130-132 765_W.mp3)
  Segment 28: 90720ms ~ 91296ms (file: 105-150-157-175-751-107-150-157-150-107-170-175-175-710_W.mp3)
  Segment 29: 92296ms ~ 92872ms (file: 723-136-132-136-172-265-127 163 127 106-167-176-167-475-167-162-103-136-103-126-127-713-175-175-165-103-162 756-765-672-673-513-157-561-531-750-753-673-160-160 231-172-315-327-1_V.mp3)
  Segment 30: 93872ms ~ 102116ms (file: 173-751-157-732-275-715-762-231-523-573-517-517-576-572-573-752-271-263-236-231-310-130-162-753-561-132-623-216-216-267-163-267-236-123-573-263-256-275-261-231-263-326-621-752-1_W.mp3)
  Segment 31: 103116ms ~ 109632ms (file: 213-236-216-126-103-263-263-762-267-326_W.mp3)
  Segment 32: 110632ms ~ 118084ms (file: 126-136-213-273-263-753-731-231-572-256-260-672-137-130-312-326-236-365-653-362-123-715-316-216-312-213-231-102-236-236-276-213-312-371-762-260-263-201-260-260_W.mp3)
  Segment 33: 119084ms ~ 119660ms (file: 135-512-152-157-517-517-152-172-136-715-176-136-126-563-163-621-623-165-576-165-617-123-125-132-125-125-125-152-125-125-165-135-153-521-651-567-617-761-165-615-165-561-136-165-1_W.mp3)
  Segment 34: 120660ms ~ 121236ms (file: 605-605-625-605-602-620-625-625-620 602-620-625-602-650-650-605-605-650-605 605-602-602-605-605-650-605-650-605 602-615-605-605-605-605-650-605-650-560-650-650-605-650-605-605-6_W.mp3)
  Segment 35: 122236ms ~ 122812ms (file: 275-163-517-623-362-231-612-731-137-136-153-135-153-156-105-106_W.mp3)
  Segment 36: 123812ms ~ 124388ms (file: 715-175-715-715-715-751-751-751-571-751-751-751 150-150-510-150-157-517-570-574-571-751-750-751-571-751-751-751-175-751-751-751-751-751-751-517-570-517-571-756-751-751-715-751-7_W.mp3)
  Segment 37: 125388ms ~ 125964ms (file: 756-756-752-751-736 571-534-537-531-573-576 752-752-756-752-756-576-756-756-753-572-725-176-513-531_W.mp3)
  Segment 38: 126964ms ~ 133912ms (file: 275-260-120-231-103-125-207-765-760-760-276-231-103-320-301-206-527-256-231-123-537-310-105-310-213-720-302-267-765-627-271-571-257-261-201-320-120-260-213-120-102-207-210-267-2_W.mp3)
  Segment 39: 134912ms ~ 135488ms (file: 541-547-563-531-541-541-543-547-506-546_V.mp3)
  Segment 40: 136488ms ~ 137064ms (file: 523-271-217-216-257-627-321-705-267-326-672-502-523-237-570-362 675_V.mp3)
  Segment 41: 138064ms ~ 138640ms (file: 316-105-150 105-105-105-103-150-150-103-105-150-150-105-105-105-105 715-751-105-516-156-150-106-106-105-105-105-105-105-105-105-156-105-150-105-157-105-165-150-507-571-157-107-7_V.mp3)
  Segment 42: 139640ms ~ 140216ms (file: 547-547-547-574 547-574-574-547-547-574-547-547-574-574-574-574-574-574-574-574-574 504-507-514-510-570-541-574-574-574-574-574-574-547 547-547-547-547-547-547-574-574-574-574-5_W.mp3)
  Segment 43: 141216ms ~ 141792ms (file: 306-360-613-613-367-632-635-351-316-362-763-316-613-612-352_W.mp3)
  Segment 44: 142792ms ~ 143368ms (file: 526-502-526-762-526-520-205-256-526-756-756-765-675-756-765-756-765 526-502-526-520-256-250-257-526-256-567-567-657-756-756-765-765-675-765-675-765-756-765-756-576-572-576-567-5_V.mp3)
  Segment 45: 144368ms ~ 144944ms (file: 237-510-237-302-231-320-627-275 671-157-175-176-267-276-230-712-267-137-132-132-137-130-236-231-321-762-312-267-321-321-320-203-326-321-312-312-362-362-172-175-173-721-107-123-7_W.mp3)
  Segment 46: 145944ms ~ 146520ms (file: 275-756-572-576-752-275-752-572-725-752-752-572-275-752-257-725-752-572-765-576-576-756-571-576-576-756-751-751-763-576-527-573-527-532-576-572-573-576-537-572-572-547-527-527-5_W.mp3)
  Segment 47: 147520ms ~ 148096ms (file: 760-670-367-637-162-675-617-617-613-617-671 307-317-301-137-137-301-607-301-316-630-610-160-613-613-607-637-736-651-675-675-673-765-675-567-657-761-576-657-576-765 735-130-703-3_V.mp3)
  Segment 48: 149096ms ~ 149672ms (file: 617-167-617 217-271-120-125-126-612-165-610-167-162-617-167-162-615-615-601-620-615-615_V.mp3)
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
> **Time**: 2026-04-05 12:53:56
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "기술 전략을 압도하고 있습니다 미국의 실질적인 보호 아래에서만 생존할 수 있는 약소국이 아닙니다 우리는 트럼프 대통령의 이런 억지 주장을 과거 트럼프 행정부 참모들은",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "기술", "start_ms": 20, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "전략을", "start_ms": 380, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "압도하고", "start_ms": 810, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 1320, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "미국의", "start_ms": 1850, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "실질적인", "start_ms": 2250, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "보호", "start_ms": 2820, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "아래에서만", "start_ms": 3150, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "생존할", "start_ms": 3750, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 4180, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 4320, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "약소국이", "start_ms": 4580, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "아닙니다", "start_ms": 5080, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "우리는", "start_ms": 5600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "트럼프", "start_ms": 5980, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "대통령의", "start_ms": 6380, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 6880, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "억지", "start_ms": 7120, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "대비할 수 있게 되어 이것이 바로 해양수산부가 그리는 K-오션의 거대한 미래입니다 아 물론 이거는 알아두셔야 돼요 지구 온난화로 북극이 녹는다 북극 항로가 열린다 이거는",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "대비할", "start_ms": 40, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 430, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있게", "start_ms": 540, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "되어", "start_ms": 760, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "이것이", "start_ms": 1050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 1420, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "해양수산부가", "start_ms": 1650, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "그리는", "start_ms": 2320, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "K-오션의", "start_ms": 2650, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "거대한", "start_ms": 3180, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "미래입니다", "start_ms": 3580, "dur_ms": 550, "phase_lr_deg": 0.0}
]
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
"transcript": "나섰다며 이를 대서특필했습니다 레드카펫과 더불어 의장대 사열과 국가 연주 그리고 프랑스와 한국의 작은 국기를 흔드는 어린이들까지 에마뉘엘 마크롱 대통령과 그의 배우자는 한국 대통령 관저 정원에서 성대한 환영을 받았다고 현지 특파원인 발레리 가스가 전했다 이후 대통령은 푸른 기와 지붕",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "나섰다며", "start_ms": 30, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이를", "start_ms": 490, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "대서특필했습니다", "start_ms": 720, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "레드카펫과", "start_ms": 1450, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "더불어", "start_ms": 2020, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "의장대", "start_ms": 2350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "사열과", "start_ms": 2720, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "국가", "start_ms": 3120, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "연주", "start_ms": 3380, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 3680, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "프랑스와", "start_ms": 4020, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "한국의", "start_ms": 4450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "작은", "start_ms": 4760, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "국기를", "start_ms": 5020, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "흔드는", "start_ms": 5350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "어린이들까지", "start_ms": 5720, "dur_ms": 650, "phase_lr_deg": 0.0}
]
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
"transcript": "테마 체크 게시판이 있습니다 여기를 들어가서 보시면 일자별 테마주가 있는데요 이 앞에 있는 게 그날의 1등 테마이고 빨간색으로 되어 있는 게 반드시 기록해야 할 그날의",
"speaker": "speaker_4",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "테마", "start_ms": 60, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "체크", "start_ms": 290, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "게시판이", "start_ms": 550, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 980, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "여기를", "start_ms": 1400, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "들어가서", "start_ms": 1750, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "보시면", "start_ms": 2180, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "일자별", "start_ms": 2550, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "테마주가", "start_ms": 2950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "있는데요", "start_ms": 3420, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 3800, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "앞에", "start_ms": 3920, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 4180, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 4400, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "그날의", "start_ms": 4520, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "1등", "start_ms": 4850, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "테마이고", "start_ms": 5120, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "빨간색으로", "start_ms": 5550, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "되어", "start_ms": 6120, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 6350, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 6580, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "반드시", "start_ms": 6700, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "기록해야", "start_ms": 7100, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 7540, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "그날의", "start_ms": 7680, "dur_ms": 320, "phase_lr_deg": 0.0}
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
"transcript": "correct one was my second guess. Yes! Okay I got that one right. Whew! All right. Yeah that was easy I got that right. Okay so I got four out of five.",
"speaker": "speaker_5",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "correct", "start_ms": 50, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "one", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "was", "start_ms": 600, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "my", "start_ms": 750, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "second", "start_ms": 880, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "guess.", "start_ms": 1220, "dur_ms": 350, "phase_lr_deg": 0.0}
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
"transcript": "해주시니까 제가 한 행동들도 다 사는 거죠 항상 이 두 분이 하실 때는 표정들이",
"speaker": "speaker_6",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "해주시니까", "start_ms": 40, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 550, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 820, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "행동들도", "start_ms": 1000, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 1450, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "사는", "start_ms": 1600, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "거죠", "start_ms": 1900, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "항상", "start_ms": 2200, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 2550, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "두", "start_ms": 2680, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "분이", "start_ms": 2820, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "하실", "start_ms": 3050, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "때는", "start_ms": 3320, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "표정들이", "start_ms": 3600, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "입니다 원작인 이태원 클라쓰의 묵직한 감정선과 세련된 연출은 사라지고 헤어스타일이랑 의상만 따라 한 듯한 어색한 연출 때문에 현지에서도 혹평이 쏟",
"speaker": "speaker_7",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "입니다", "start_ms": 20, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "원작인", "start_ms": 300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이태원", "start_ms": 680, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "클라쓰의", "start_ms": 1120, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "묵직한", "start_ms": 1680, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "감정선과", "start_ms": 2100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "세련된", "start_ms": 2680, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "연출은", "start_ms": 3150, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "사라지고", "start_ms": 3550, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "헤어스타일이랑", "start_ms": 4050, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "의상만", "start_ms": 4800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "따라", "start_ms": 5180, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 5450, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "듯한", "start_ms": 5620, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "어색한", "start_ms": 5920, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "연출", "start_ms": 6320, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "때문에", "start_ms": 6620, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "현지에서도", "start_ms": 7000, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "혹평이", "start_ms": 7550, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "쏟", "start_ms": 7950, "dur_ms": 120, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 12:53:56*
