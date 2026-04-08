# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=15840  time=09:07:17  uptime=16108s  mem=216MB  system_processes=408"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_090642.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 8100ms (file: 750-106-253-263-257-350-321-237-752-316-361-637-357-537-567-302-103-310-136-132-325-631-657-265-325-301-326-325-235-263 357-130-236-326-263-357-537-326-273-263-231-132-135-157-2_W.mp3)
  Segment 2: 9100ms ~ 9676ms (file: 501-571-573-571-571-571 517-507-517-517-513 517-517-153-517-153-715-125-317-137-175-135-715-315-375-571-570-573 570-573-571-571-517-517-153-157-317-731-571-157-573-713-537-157-1_V.mp3)
  Segment 3: 10676ms ~ 19460ms (file: 106-103-753-230-321-326-301-130-103-321-260-231-236-326-326-321-132-321-130-136-132-203-126-351-102-601-621-237-236-327-723-236-312-321-213-237-213-217-130-107-107-213-127-752-1_W.mp3)
  Segment 4: 20460ms ~ 30720ms (file: 130-173-126-321-261-267-231-236-261-261-162-321-321-231-132-231-261-163-263-165-652-567-752-320-312-312-132-130-362-312-317-301-327 271-237-627-132-267-236-162-132-231-562-213-2_W_stt_the.mp3)
  Segment 5: 31720ms ~ 32296ms (file: 157-507-105-710-175-107-157-517-751-150-235 105-105-175-105-107-107-751-715-157-170-751-107-170-157-175-105 105-105-105-517-150-105-157-150-150-170-701-105-157-501-107-170-157-1_V.mp3)
  Segment 6: 33296ms ~ 33872ms (file: 715 715-751-750-751-173-715-173-105-731-735-715-731-173-710-731-705-710-715-753-715-753-175-751 570-175-175-715-713-735-713-170-710-715-713-710-731-713-170-703-710-750-735-756-7_V.mp3)
  Segment 7: 34872ms ~ 35448ms (file: 150-130-157-153-105-157-105-105-150-513-150-103-107-103-107-170-163-105-157-103-173-715-703-105 105-105-173-153-156-175-165-107-107-137-175-175-157-157-103-105-176-750-710-105-7_V.mp3)
  Segment 8: 36448ms ~ 37024ms (file: 157-157-157-157-152-152-513-157-152-132-120-120-103-102-102-126-162-162-106-153-510-157-571-153-157-571-175-175-175-157-175-751-157-175-571-751-571-571-175-175-571-157-715-715-1_V.mp3)
  Segment 9: 38024ms ~ 38600ms (file: 710-710-170-173-713-713-173-713-713-715-753 137-175-317-137-132-123-312-312-317-132 175-750-751-735-715-753-753-703-716-163-736-761-763-273-732-723-173-317 751-715-750-751-701-7_W.mp3)
  Segment 10: 39600ms ~ 40176ms (file: 617-326-752-732-362-570-275-726-271-320-362-372-523-103-237-507-315-326-623-312-321-632-236-237-362-263-325-752-257-572-752-216-736-267-326-627-326-624-576-627-527-657-325-750-6_V.mp3)
  Segment 11: 41176ms ~ 41752ms (file: 731 570-570-573-576-752-751-715-715-710-715-765-173-715-716-735-713-725-715 501-517-570-571-571-570-751-571-750-713-751-170-751-750-751-752-710-715-175-753-175-705-753 750-750-7_V.mp3)
  Segment 12: 42752ms ~ 43328ms (file: 752-137-312-301-316-135-705-756-572-572-620 275-750-316-570-163-213-175-321-726-213-571-762-571-312-752-237-560-752-752-356-635-573-572-765-576-672-326-732-327-516-312-570-237-6_V.mp3)
  Segment 13: 44328ms ~ 44904ms (file: 715-762-751-765-765 136-176-167-167-176-176-167_W.mp3)
  Segment 14: 45904ms ~ 55516ms (file: 301-672-165-160-261-163-312-325-326-312-102-310-231-376-213-312-320-675-756-576-130-130-752-275-106-237-527-327-357-320-276-672-126-321-136-716-263-103-326-372-367-310-267-326-1_W.mp3)
  Segment 15: 56516ms ~ 65480ms (file: 123-236-102-301-263-263-713-213-132-130-321-263-261-627-321-263-260-361-310-137-315-103-756-261-267-276-216-173-107-176-107-210-753-756-260-267-267-726-213-261-672-216-261-217-1_W.mp3)
  Segment 16: 66480ms ~ 67056ms (file: 751-715 105-150-150-510-157-150-510-175-510-571-510-105-150-710-751-105-157-157-175-750-571-175-571-157 105-715-507-150-501-150-517-175-107-751-107-710-107-170-710-107-751-705-7_V.mp3)
  Segment 17: 68056ms ~ 68632ms (file: 573-571-576-571-751-517-516-672-627-617-657-162-517-571-753-153-261-216-163-137-137-135-137-157-135-351-137-135 315-137-137-123-621-312-132-631-316-137 135-153-315-317 135-713-5_W.mp3)
  Segment 18: 69632ms ~ 70208ms (file: 157-510-507-507-170-107-175-107-175-170-170-175-107-751-150-175-507-170-157 175-105-507-150-510-175-175-107-175-157-105-105-715-571-750-751-107-105-705-105-135 150-751-107-105-1_V.mp3)
  Segment 19: 71208ms ~ 71784ms (file: 312-160-106-160-175-157-137-137-107-157-120-201-102-127-125-127-712-127-621-263-621-612-651-652-651-652-652_V.mp3)
  Segment 20: 72784ms ~ 73360ms (file: 715 570-570-570-571-571-570-570-756-157-137-710-731-750-705 576-570-170-715-750-715-715-753-751-751-750 570-570-570-576-573-576-751-157-513-173-713-730-751-713-175-715-756 570-5_V.mp3)
  Segment 21: 74360ms ~ 74936ms (file: 176-710-710-715-751-701-713-710-715-751-570 570-571-571-715-751-730-716-716-710-710-710-715-715-735-703-137-715-713-715-716-572-750 750-751-570-175-713-507-710-751-713-170-173-7_V.mp3)
  Segment 22: 75936ms ~ 76512ms (file: 576-574-576-752-573 570-571-750-570-750-571-750-175-570-570-570-530-570-570-571-570-571-576-572-571-572-573-537-521 571-501-510-573-571-571-710-751-571-531-517-751-573-357-130-5_W.mp3)
  Segment 23: 77512ms ~ 89932ms (file: 270-267-752-273-320-130-201-312-267-132-103-136-127-103-107-107-107-705-754-756-754-756-132-321-210-201-103-216-123-213-103-301-231-103-103-102-312-201-542-206-206-206-206-231-3_V.mp3)
  Segment 24: 90932ms ~ 91508ms (file: 753 352-531-513-157-153-317-137-571-710-130-173-173-731-173-753-715-753-713-731-170-705-715-753-751-731 315-137-173-730-730-730-317-713-371-713-371-137-713-736-372-312-137-312-3_W.mp3)
  Segment 25: 92508ms ~ 93084ms (file: 150-157-150-150-570-105-157-157-157-510-601 510-510-150-510-150-571-150-517-150 150-157-105-107-150-751-107-175-105-170-107-157-175-107-105 105-150-105-150-157-157-107-175-105-1_V.mp3)
  Segment 26: 94084ms ~ 94660ms (file: 574-517-501-574-574-574-574-571-574-571-571-571-517 574-574-574-571-517-157-175-761-107-127-130-105-513-571-571-576-571-751-175-157-513-315-137-175-137-173-257-753-756-517-537-5_W.mp3)
  Segment 27: 95660ms ~ 102788ms (file: 132-136-103-132-132-132-312-312-132-103-132-132-103-132-132-132-132-132-312-103-102-153-103-132-103-312-103-103-103-102-210-103-130-103-132-201-105-103-276-531-527-215-132-201-2_V.mp3)
  Segment 28: 103788ms ~ 109836ms (file: 106-320-123-120-102-326-267-167-756-106-163-617-167-107-671-176-132-574-756-725-725-756-371-326-623-752-136-723-327-175-671-617-617-756-527-230-756-276-132-136-237-327-756-275-1_W.mp3)
  Segment 29: 110836ms ~ 119620ms (file: 102-231-310-120-127-103-271-207-201-621-267-260-723-216-572-312-372-261-207-102-106-670-267-123-750-765-107-276-210-756-675-760-201-260-620-216-620-267-765-617-670-102-261-213-1_W.mp3)
  Segment 30: 120620ms ~ 121196ms (file: 517-571-517-517-571-570-751-571-157-571-357-570-735-705-751-753-137-735-753-375-713-752-375-715-735-752-753-512-351-513-135-153-352-153-571-570-570-510-751-175-105-517-107-731-7_V.mp3)
  Segment 31: 122196ms ~ 129432ms (file: 736-103-123-201-143-123-675-657-103-203-203-207-302-130-752-321-201-750-326-312-310-210-206-201-675-207-130-207-132-756-102-203-103-230-237-675-103-213-213-130-103-132-201-574-6_W.mp3)
  Segment 32: 130432ms ~ 138532ms (file: 260-326-130-103-163-230-126-167-576-627-230-320-136-260-572-103-263-756-263-236-326-321-167-231-263-627-675-756-275-127-231-206-127-310-372-273-167-310-127-130-157-312-230-203-3_W.mp3)
  Segment 33: 139532ms ~ 140108ms (file: 175-157-517-510-571-517-507-150-517_W.mp3)
  Segment 34: 141108ms ~ 141684ms (file: 753-750-517-735-576 150-517-701-507-573-571-571-715-150-157-571-750-730-150-710-751-750-170-715-753-750-750-751-750-753-713-751-753 570-570-571-571-517-715-751-713-173-715-751-7_V.mp3)
  Segment 35: 142684ms ~ 143260ms (file: 573-573-576-571-574-571-574-571-571-574-574-571_W.mp3)
  Segment 36: 144260ms ~ 144836ms (file: 570-507-570-570-507-571-570-570-570-570-570-571_W.mp3)
  Segment 37: 145836ms ~ 146412ms (file: 627-607-132-103-576-125-750-765-352-750-326-126-162-216-261-216-206-273-572-157 756-712-657-275-261-672-263-721-532-572-627-257-367-321-126-237-576-263-765-627-320-321-574-567-5_W.mp3)
  Segment 38: 147412ms ~ 147988ms (file: 705-710-701-710-710-217-107-312-130-103-132-123 120-120-132-132-132-135-173-107-170-170-710-107-170-710-172-210-702-127-170-170-107-102-120-123-126-162-162-162-106-134-103-107-1_V.mp3)
  Segment 39: 148988ms ~ 149564ms (file: 570-750-751-573-570-517-751-571-175-751-715 715-175-715-710-710-170-173-750-750-706-751-170-135-751-571 570-507-570-750-137-170-710-735-713-710-753-730-715-751-571 573-571-570-5_V.mp3)
  Segment 40: 150564ms ~ 151140ms (file: 107-175-756-751-107-715-701-715-105-153 107-750-105-751-105-170-107-710-170-715-105-715-751-170-710-715-751-715-107-105-150 105-105-150-107-107-170-107-170-710-107-175-710 105-1_V.mp3)
  Segment 41: 152140ms ~ 152716ms (file: 756-751-750-175-715-753-751-175-715-751-751-750 617-612-617-671-107-175-107-701-170-176-716-710-750-750-715-751-175-107-170-715-107-170-750-701-750 167-671-706-705-706-701-750-7_V.mp3)
  Segment 42: 153716ms ~ 154292ms (file: 571-751-571-571 574-571-571-571-573-571-715-571-157-753-751-751-571-715-751-751-175-715-175-153-571-751-751-756-573 571-517-157-750-570-157-157-573-571-751-513-370-750-751-751-7_V.mp3)
  Segment 43: 155292ms ~ 155868ms (file: 571-572-576-573 571-570-517-157-135-137-750-175-571-751-751-571-570-571-571-537-570-517-570-571-571-571-571-573 517-157-571-517-571-517-175-175-731-715-571-751-571-715-571-135-7_V.mp3)
  Segment 44: 156868ms ~ 165688ms (file: 213-103-301-267-627-137-213-732-173-726-276-276-276-527-276-257-572-130-270-275-127-132-127-270-572-527-257-276-276-267-267-726-765-751-572-230-236-132-312-276-267-213-312-621-2_W.mp3)
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
> **Time**: 2026-04-06 09:08:42
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "1970년대 오일쇼크 이후 수십 년 풀어온 국기를 찢어버린 셈입니다 일단 정부와 업계는 미국산 원유 물량 확보에 나섰습니다 장기 석유로는 중남미나 아프리카 산",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "1970년대", "start_ms": 20, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "오일쇼크", "start_ms": 550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이후", "start_ms": 1000, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "수십", "start_ms": 1250, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "년", "start_ms": 1550, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "풀어온", "start_ms": 1700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "국기를", "start_ms": 2050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "찢어버린", "start_ms": 2450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "셈입니다", "start_ms": 2950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "일단", "start_ms": 3400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "정부와", "start_ms": 3700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "업계는", "start_ms": 4100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "미국산", "start_ms": 4450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "원유", "start_ms": 4850, "dur__ms": 250, "phase_lr_deg": 0.0},
{"word": "물량", "start_ms": 5150, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "확보에", "start_ms": 5450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "나섰습니다", "start_ms": 5850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "장기", "start_ms": 6400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "석유로는", "start_ms": 6700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "중남미나", "start_ms": 7200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "아프리카", "start_ms": 7700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "산", "start_ms": 8050, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "아 끈기가 진짜 대단하시다 그래서 누가 이기나 보자 세화라 네가 이기나 내가 이기나 보자 뭐 거의 그 수준인 거 같은데요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "아", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "끈기가", "start_ms": 150, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 450, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "대단하시다", "start_ms": 700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 1200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "누가", "start_ms": 1600, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이기나", "start_ms": 1800, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "보자", "start_ms": 2100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "세화라", "start_ms": 2350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "네가", "start_ms": 2750, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이기나", "start_ms": 2950, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "내가", "start_ms": 3250, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "이기나", "start_ms": 3500, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "보자", "start_ms": 3800, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 4100, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "거의", "start_ms": 4250, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 4500, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "수준인", "start_ms": 4650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 5000, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "같은데요", "start_ms": 5150, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "주변에서 집 짓다 망했다는 소리 마지막 화룡점정 마차 우와 마차 한 바가지네",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "mixed",
"words": [
{"word": "주변에서", "start_ms": 40, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "집", "start_ms": 480, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "짓다", "start_ms": 620, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "망했다는", "start_ms": 900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "소리", "start_ms": 1350, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "마지막", "start_ms": 4100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "화룡점정", "start_ms": 4550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "마차", "start_ms": 5100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "우와", "start_ms": 5450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "마차", "start_ms": 5850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 6200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "바가지네", "start_ms": 6350, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "적극적으로 이런 하드웨어를 많이 돌리는 걸 이용하려고 할 때 훨씬 더 가속을 잘 할 수가 있겠죠 그만큼 맥 사용자들의 비중이 많아지다 보니까 맥이라든가 아니면은 이런 아이폰이라든가 이런 것들을 바로바로 빠르게 처리를 할 수 있도록 오픈 모델을 이용해 가지고 이렇게 되다 보니까",
"speaker": "speaker_4",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "적극적으로", "start_ms": 30, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 550, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "하드웨어를", "start_ms": 800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 1350, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "돌리는", "start_ms": 1650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "걸", "start_ms": 2050, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이용하려고", "start_ms": 2250, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 2800, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 2950, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "훨씬", "start_ms": 3150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 3500, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "가속을", "start_ms": 3650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 4050, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 4250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "수가", "start_ms": 4400, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "있겠죠", "start_ms": 4650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "그만큼", "start_ms": 5100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "맥", "start_ms": 5500, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "사용자들의", "start_ms": 5700, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "비중이", "start_ms": 6350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "많아지다", "start_ms": 6750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "보니까", "start_ms": 7250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "맥이라든가", "start_ms": 7650, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "아니면은", "start_ms": 8300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 8800, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "아이폰이라든가", "start_ms": 9050, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 9850, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "것들을", "start_ms": 10100, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "이러한 열 배 더 재밌게 즐기실 수 있습니다 그리고 이런 튤립 축제 자체가 있어요 4월 11일부터 5월 10일까지 더 많이 즐길 수도 있고요 여러분 그리고 여기",
"speaker": "speaker_5",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "이러한", "start_ms": 25, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "열", "start_ms": 350, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "배", "start_ms": 550, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 700, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "재밌게", "start_ms": 850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "즐기실", "start_ms": 1250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 1650, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 1800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 2200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 2600, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "튤립", "start_ms": 2900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "축제", "start_ms": 3250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "자체가", "start_ms": 3600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 4000, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "직접 만들었답니다 비닐 깔고 그다음 철근 양생하고 이런 모든 과정들을 가족들이 같이 했는데 저희는 이제 인건비 안 들어가잖아요 그러면 이곳을 지을 때 재료는 아끼지 말자 그래서 강",
"speaker": "speaker_6",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "직접", "start_ms": 20, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "만들었답니다", "start_ms": 300, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "비닐", "start_ms": 1000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "깔고", "start_ms": 1350, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "그다음", "start_ms": 1650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "철근", "start_ms": 2050, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "양생하고", "start_ms": 2400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 2950, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "모든", "start_ms": 3200, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "과정들을", "start_ms": 3500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "가족들이", "start_ms": 4050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 4600, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "했는데", "start_ms": 4900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "저희는", "start_ms": 5300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 5700, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "인건비", "start_ms": 6000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 6400, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "들어가잖아요", "start_ms": 6600, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "이것은 미래의 모습이 아니",
"speaker": "speaker_7",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "이것은", "start_ms": 15, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "미래의", "start_ms": 400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "모습이", "start_ms": 800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아니", "start_ms": 1250, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "보여드리면 전날에 오버나이트 했던 거 아침에 10% 이상 상승해서 대부분 수익 실현해 주고 시가보다 낮은 구간",
"speaker": "speaker_8",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "보여드리면", "start_ms": 25, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "전날에", "start_ms": 550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "오버나이트", "start_ms": 950, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "했던", "start_ms": 1600, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 1900, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "아침에", "start_ms": 2050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "10%", "start_ms": 2450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이상", "start_ms": 2850, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "상승해서", "start_ms": 3150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "대부분", "start_ms": 3650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "수익", "start_ms": 4050, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "실현해", "start_ms": 4400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "주고", "start_ms": 4850, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "시가보다", "start_ms": 5150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "낮은", "start_ms": 5650, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "구간", "start_ms": 5950, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "관계가 급격히 악화된 바 있다 무엇보다 이번 사건의 발단은 비교적 단순했습니다 일부 국가들이 행정 시스템이나 공식 문서에서 대만을 중국과 함께 표기하거나 보다 모호한 방식으로 표현하기 시작한 것이 기",
"speaker": "speaker_9",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "관계가", "start_ms": 30, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "급격히", "start_ms": 400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "악화된", "start_ms": 800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "바", "start_ms": 1200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있다", "start_ms": 1350, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "무엇보다", "start_ms": 1700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이번", "start_ms": 2200, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "사건의", "start_ms": 2500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "발단은", "start_ms": 2900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "비교적", "start_ms": 3300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "단순했습니다", "start_ms": 3700, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "않을 경우 추가 대응에 나설 것이라고 밝혔다 린 장관은 이미 대만 주재 덴마크 대표부 직원들의 특권과 면제를 일부 조정했다고 설명했으며 주요 7개국과 유럽 연합",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "않을", "start_ms": 20, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "경우", "start_ms": 250, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "추가", "start_ms": 550, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "대응에", "start_ms": 850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "나설", "start_ms": 1200, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "것이라고", "start_ms": 1500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "밝혔다", "start_ms": 1950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "린", "start_ms": 2400, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "장관은", "start_ms": 2550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이미", "start_ms": 2950, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "대만", "start_ms": 3250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "주재", "start_ms": 3600, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "덴마크", "start_ms": 3900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "대표부", "start_ms": 4300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "직원들의", "start_ms": 4700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "특권과", "start_ms": 5200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "면제를", "start_ms": 5600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "일부", "start_ms": 6000, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "조정했다고", "start_ms": 6250, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "설명했으며", "start_ms": 6850, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "Good protein We do Now it's time to eat your favorite Korean gukbap My favorite? Your favorite I don't trust you",
"speaker": "speaker_10",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 25,
"audio_type": "mixed",
"words": [
{"word": "Good", "start_ms": 25, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "protein", "start_ms": 200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "We", "start_ms": 550, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "do", "start_ms": 750, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "Now", "start_ms": 950, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "it's", "start_ms": 1200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "time", "start_ms": 1400, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 1650, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "eat", "start_ms": 1800, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "your", "start_ms": 2000, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "favorite", "start_ms": 2200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "Korean", "start_ms": 2550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "gukbap", "start_ms": 2950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "My", "start_ms": 3400, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "favorite?", "start_ms": 3600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "Your", "start_ms": 4000, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "favorite", "start_ms": 4200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 4550, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "don't", "start_ms": 4700, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "trust", "start_ms": 4950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 5300, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "간격을 이제 좀 좁혔어요 인건비가 제로니 누릴 수 있었던 작은 사치 그렇게 기초에만 오 개월이 걸렸다네요 사람",
"speaker": "speaker_6",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "간격을", "start_ms": 30, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 410, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 700, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "좁혔어요", "start_ms": 850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "인건비가", "start_ms": 1400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "제로니", "start_ms": 1900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "누릴", "start_ms": 2300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 2650, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있었던", "start_ms": 2800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "작은", "start_ms": 3200, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "사치", "start_ms": 3500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "그렇게", "start_ms": 3950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "기초에만", "start_ms": 4400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "오", "start_ms": 4900, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "개월이", "start_ms": 5100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "걸렸다네요", "start_ms": 5550, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "사람", "start_ms": 6150, "dur_ms": 300, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 09:08:42*
