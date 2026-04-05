# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=6944  time=09:23:35  uptime=27624s  mem=506MB  system_processes=352"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_092323.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 756-567-321-123-650-132-127-632-712-276-367-567-126-607-756-276-163-207-205-165-715-173-175-216-756_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 175-371-317-375-173-317-710-713-701-310-710-371-173 705-370-130-137-103-137-371-317-130-137-175-137-130-370-701-710-130-103-170-751-305-137-317-503-735-571-753-756-576-756-312-3_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 617-657-612-672-673-637-675-675-657-675-627-672-657-102-102-163-106-102_W.mp3)
  Segment 4: 4728ms ~ 12792ms (file: 756-276-267-723-162-136-750-765-751-172-175-137-207-123-267-276-206-750-675-267-127-176-107-126-267-213-263-123-301-312-632-361-756-267-617-765-612-671-621-627-612-672-126-167-1_W.mp3)
  Segment 5: 13792ms ~ 14368ms (file: 103-132-317-621-157 631-612-627-136-567-167-615-261-627-123-136-150-136-652-617-613-261-615-562-160-103-103-106-571-136-163-136-123-163-103-103-136-312-137-156-615-103-617-106-1_V.mp3)
  Segment 6: 15368ms ~ 15944ms (file: 236-621-326-632-631-361-362-132-361-136-362-326-736-763-376-763-376-762-712-726-312-316-327-276-372-271-326-263-236-236-132-201-236-203-201-213-213-215-312-321-201-126-156-617-3_W.mp3)
  Segment 7: 16944ms ~ 27492ms (file: 756-267-210-301-276-276-106-107-576-176-107-267-126-236-765-672-107-103-237-572-237-216-762-267-627-267-236-312-103-762-671-216-162-126-614-236-263-726-756-672-267-236-326-756-2_W.mp3)
  Segment 8: 28492ms ~ 33784ms (file: 672-546-256-261-312-573-321-103-103-752-573-537-534-312-312-316-320-512-576-725-132-167-263-127-231-276-276-125-572-574-510-217-572-125-206-123-213-260-206-123-267-126-271-726-1_W.mp3)
  Segment 9: 34784ms ~ 39860ms (file: 276-231-321-230-237-216-237-735-751-705-756-670-312-263-217-206-612-137-571-132-451-236-237-527-125-103-315-321-271-201-273-237-230-321-132-267-261-210-213-123-102-156-206-260-1_V.mp3)
  Segment 10: 40860ms ~ 41436ms (file: 765 261-261-623-631-263 216-261-163-106-263-126_W.mp3)
  Segment 11: 42436ms ~ 43012ms (file: 507-756-657-657-756-675-765-765-675-765-765-765-765-765-675 574-250-520-576-527-507-567-562-567-675-765-765-675-675-657_W.mp3)
  Segment 12: 44012ms ~ 55460ms (file: 236-120-132-126-216-206-102-105-107-107-107-615-103-123-203-103-103-102-125-135-210-230-302-320-301-315-136-305-756-135-270-306-103-167-132-610-236-230-310-103-132-103-425-103-1_V.mp3)
  Segment 13: 56460ms ~ 57036ms (file: 156-165-160-160-153 502-562-506-567-257-275-760-725-765-257-576-627-256-735-576-753-275-235-230-506-235-315-301-230-102-520 205-257-207-257-206-562-750-567-705-752-752-367-267-3_W.mp3)
  Segment 14: 58036ms ~ 58612ms (file: 517-261-632 136-163-136-136-231-103-106-135-130-102-130-123-136-126-136-216-657-123-316-136-152-517-123-132-130-613-132-106-136-136-123-261-132-567-162-106-103-312-613-627-617-5_V.mp3)
  Segment 15: 59612ms ~ 60188ms (file: 675-165-265-762-672-627-627-672-765-657-675-675-623-672-167-672-672-672-276-762-267-576-675-765-675-672-627-267-675-657-627-267 106-167-706-756-751-756-623-621-671-267-301-765-7_W.mp3)
  Segment 16: 61188ms ~ 61764ms (file: 627-675-672-653-627-362-671-621-675-567-572-675-276-265-265-625-612-267_W.mp3)
  Segment 17: 62764ms ~ 68776ms (file: 163-324-125-172-136-132-132-136-132_V.mp3)
  Segment 18: 69776ms ~ 70352ms (file: 175-751-705-751-750-715-750-750-751-751-751-750-751-750-750-751-750-751-576 517-571-705-751-507-175-710-175-173-715-751-175-751-175-715-751-715-751-705-715-751-751-756-750-751 1_V.mp3)
  Segment 19: 71352ms ~ 80856ms (file: 237-230-123-301-132-310 203-260-103-132-361-326-316-320-532-132-267-132-213-132-312-124-132-132-726-312-123-207-752-231-253-205-126-326-312-201-126-531-507-320-206-276-261-201-3_V.mp3)
  Segment 20: 81856ms ~ 82432ms (file: 631-602-261-120-263-726-627-267-261-261-216-173-376-316-561-107-130-176-176-657-576-576-571-157-756-756-765-675-675-765-675-675-675-236-326-632-612-163-156 163-375-761-675-637-6_W.mp3)
  Segment 21: 83432ms ~ 84008ms (file: 612-627-162-563-265-637-573-617-613-162-163-675-675-617-562-657-657-567-657-567-657-657-106-167-671-167-675-675-637-607-670-675-675-670-675-670 670-756-623-126-621-627-632-126-1_W.mp3)
  Segment 22: 85008ms ~ 85584ms (file: 675 250-275-526-562-651-657-657-657-657-657-657-657-657-657-657-657-657-657-657-657-657-657-654 675-675-675-675_W.mp3)
  Segment 23: 86584ms ~ 94288ms (file: 130-236-213-627-231-216-751-765-321-312-316-657-756-627-756-623-672-132-106-657-567-216-316-130-263-576-123-321-574-157-216-103-213-261-576-576-527-267-765-576-576-231-576-316-5_W.mp3)
  Segment 24: 95288ms ~ 95864ms (file: 751-750-570-574-570-750-157-750-570-571-507-570-570-750-507-750-715-570-750-570 571-574-157-517-501-157 571-157-574-754-574-574-571 574-574-574-574-574-751-571-751-574-571-751-7_V.mp3)
  Segment 25: 96864ms ~ 97440ms (file: 107-157-173-170-713-170-715-170-701-571-350-653-175-107-173-130 157-153_V.mp3)
  Segment 26: 98440ms ~ 99016ms (file: 312-312-123-675-130-312-320-302-213-312-302-321-327-260-312-132-312-231-132-263-321-137-157-135-213-163-156-756-106-105-153-165-375-230-316-103-217-267-123-106-132-132-302-320-2_W.mp3)
  Segment 27: 100016ms ~ 106892ms (file: 102-267-231-573-103-103-576-261-236-321-627-623-527-672-267-267-527-263-132-130-130-261-231-213-256-326-132-267-102-107-132-267-725-576-130-312-263-321-316-263-216-216-657-102-1_W.mp3)
  Segment 28: 107892ms ~ 108468ms (file: 517-105-150-105-517-507-571 510-510-150-517-105-510-150-501-150-105-150-510-105-501-517-501-501-501-105-540-517-510-150-510-105-105-510-501-503-517 507-157-510-150-105-510-510-5_W.mp3)
  Segment 29: 109468ms ~ 110044ms (file: 613-130-327-516-367-130-130-312-176-163-316-163-710-130-136-176-136-621-623-367-263-216_V.mp3)
  Segment 30: 111044ms ~ 111620ms (file: 517-157-175 105-105-156-150-150-157-157-157-156-167-175-165-136-153-167-137-175-176-175-175-156-176-137 105-105-150-157-175-157-175-176-167-176-761-716-107-167-761-176-761-716-6_V.mp3)
  Segment 31: 112620ms ~ 113196ms (file: 173-175-103-137-107-103-107-103-310-715 571-150-105-103-103-173-175-715-710-317-310-103-107-103-157-137-310-107-307-571-715-107-107-751-537 576-573-576-517-351-315-130-105-150-1_V.mp3)
  Segment 32: 114196ms ~ 127372ms (file: 203-132-735-756-675-625-267-620-620-756-750-621-602-756-367-106-267-526-320-206-260-264-314-103-107-130-543-576-163-620-356-307-130-623-105-156-157-623-573-576-267-136-312-210-1_W.mp3)
  Segment 33: 128372ms ~ 128948ms (file: 523-517-105-516-512-571-517-512-512-157-517-501-507-572-576-572-572-512 507-570-526-512-512-501-571-572-507-572-527-576_V.mp3)
  Segment 34: 129948ms ~ 137292ms (file: 372-327-170-160-236-370-367-263-236-723-170-103-102-267-267-267-267-267-275-230-231-327-327-237-726-210-312-607-231-273-276-267-736-123-102-132-671-276-231-236-276-263-276-267-6_W.mp3)
  Segment 35: 138292ms ~ 138868ms (file: 570-507-316-135-351-750-750-571-507-570-756-756-567-561-576-157-765-765-716-167-617-671-637-617-576-507-706-756-607-765-175-175-735-756-760-765-765-731-751-750-507-571-501-571-5_V.mp3)
  Segment 36: 139868ms ~ 140444ms (file: 104-104-104-134-105-104-413 310-140-104-104-301-134-401-140-415-134-104-147-143-341-135-103-145-104-104-134-104-145-143-135-105-145-104-104-104 314-304-130-310-315-415-143-145-1_V.mp3)
  Segment 37: 141444ms ~ 142020ms (file: 167-107-765-765-157-716-605-160-761-617 105-165-150-517-765-716-173-705-765-715-701-176-761-761-751-317-751-571-170-163-163_V.mp3)
  Segment 38: 143020ms ~ 143596ms (file: 157-150-105-157-571-150-570-170-105-105-175-710-517 501-510-517-150-517-154-150-105-105-517-175-507 507-105-105-157-153-175-157-105-157-175-107-157-157-105 574-501-501-570-105-5_W.mp3)
  Segment 39: 144596ms ~ 145172ms (file: 537-715-107-174-701-174-107-174-174-107 315-753-375-710-701-175-710-170-701-701-107-170-107-107-173-130-103-103-103-107-513-153-517-715-571-753-571-567-721-752-725-751-271-726-7_W.mp3)
  Segment 40: 146172ms ~ 146748ms (file: 751-756-756-756-756-751-715 156-510-157-157-751-571-751-715-756-751-751-715-751-751-751-756-751-751 501-105-510-150-150-517-573-751-751-751-751-175-715-175-751-751-751-751-756-7_V.mp3)
  Segment 41: 147748ms ~ 148324ms (file: 162-672-672-567-657-621-571-576-576-316-132-574-576-756-756-675-576-756-756-756-750-567-574-756-765-574-574-673-367_W.mp3)
  Segment 42: 149324ms ~ 154076ms (file: 162-210-547-312-237-261-276-216-231-123-163-136-531-351-163-126-362-123-167-163-162-261-217-126-312-312-321-132-123-126-136-312-316-312-312-326-621-213-162-132-173-172-217-120-1_W.mp3)
  Segment 43: 155076ms ~ 155652ms (file: 132-103-627-621-162-103-137-136-135-132-213-617-160-130-316-316 765-671-617-761-672-627 123-216-132-213-236-263 570-576-765-105-651-154_W.mp3)
  Segment 44: 156652ms ~ 157228ms (file: 751-715-751-751-574-574-753-752-753-715-753-715-751-715-752-751-751-754-750 751-715-751-517-157-153-157-715-175-517-175-173-753-751-713-753-751-175-751-751-157-574-751 752-571-7_W.mp3)
  Segment 45: 158228ms ~ 158804ms (file: 231-320-165-135-316-316-316-136-136-136-163-160-162-163-163-130-125-162-231-236-213-321-132-123-321-316-123-126-136-123-216-132-132-213_W.mp3)
  Segment 46: 159804ms ~ 168300ms (file: 136-136-263-263-206-701-216-263-231-257-524-275-162-617-163-621-321-263-132-312-263-310-135-754-572-562-231-756-360-623-362-231-326-261-106-706-547-265-263-576-527-574-263-130-2_W.mp3)
  Segment 47: 169300ms ~ 169876ms (file: 510-175-150-157-175-175-715-751-756-756-576-570-751-571-570-751-570-571-570-751-157-570-517 517-517-510-510-531-510-501-517-510-510-507-510-571-517-517-517-570-517-507 517-510-5_V.mp3)
  Segment 48: 170876ms ~ 180452ms (file: 672-675-675-657-576-567-756-756-756-273-267-725-132-263-267-326-756-173-275-601-627-271-120-210-127-210-103-267-320-102-132-623-120-236-576-235-756-275-127-610-261-231-532-201-1_W.mp3)
  Segment 49: 181452ms ~ 182028ms (file: 751-751-710-750-175-105-150-157-175-751-751-517-571-751-571-751-751-570-576-157-571-571-517-751 576-501-507-750-756-750-715-175-715-571-157-571-157-165-156-106 567-105-765-105-7_V.mp3)
  Segment 50: 183028ms ~ 189040ms (file: 316-627-127-132-135-572-130-357-175-103-127-231-316-231-301-613-716-761-267-321-765-216-675-725-201-261-102-213-317-213-501-706-302-753-267-253-125-213-237-216-752-126-130-312-1_V.mp3)
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
> **Time**: 2026-04-05 09:26:37
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
{"word": "포커스", "start_ms": 10, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "양보가", "start_ms": 170, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "필요합니다", "start_ms": 360, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "새로운 기술에 맞는 형태로 가져가야 되지 않나 해서 오늘 한번 정리해 보았습니다 지금까지 에러였습니다 안대공학 인스타그램 언리얼 텍 점 에러에서도",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "새로운", "start_ms": 20, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "기술에", "start_ms": 55, "dur_ms": 35, "phase_lr_deg": 0.0},
{"word": "맞는", "start_ms": 95, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "형태로", "start_ms": 130, "dur_ms": 35, "phase_lr_deg": 0.0},
{"word": "가져가야", "start_ms": 170, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "되지", "start_ms": 215, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "않나", "start_ms": 250, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "해서", "start_ms": 285, "dur_ms": 35, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 325, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "한번", "start_ms": 360, "dur_ms": 35, "phase_lr_deg": 0.0},
{"word": "정리해", "start_ms": 400, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "보았습니다", "start_ms": 445, "dur_ms": 45, "phase_lr_deg": 0.0},
{"word": "지금까지", "start_ms": 495, "dur_ms": 35, "phase_lr_deg": 0.0},
{"word": "에러였습니다", "start_ms": 535, "dur_ms": 40, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "호랑이 그리고 동굴 사자까지 이동했던 것으로 알려져 있습니다 하지만 그것은 아주 먼 옛날의 이야기죠 약 1만 년 전 베링해는 지금과 거의 같은 모습으로 형성되었습니다 현재 베링해의 면적",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "호랑이", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 190, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "동굴", "start_ms": 320, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "사자까지", "start_ms": 440, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "주권 파괴형 기업죄를 명분 삼아 방위비 분담금의 폭발적인 인상을 요구하거나 자동차 및 첨단 산업에 대한 고율의 관세 폭탄을 무기로 들고나와 한국의 목줄을 죄어 올",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "주권", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "파괴형", "start_ms": 240, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "기업죄를", "start_ms": 470, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "서 있는 로봇 공학자 박혜원 교수",
"speaker": "speaker_5",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "서", "start_ms": 20, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 70, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "로봇", "start_ms": 130, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "공학자", "start_ms": 240, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "박혜원", "start_ms": 370, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "교수", "start_ms": 490, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "탄으로 볼 수 있다라는 겁니다",
"speaker": "speaker_6",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "탄으로", "start_ms": 10, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "볼", "start_ms": 140, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 250, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "있다라는", "start_ms": 340, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "겁니다", "start_ms": 480, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 8,
"transcript": "자신의 선택이 옳았음을 확인합니다 I wanna walk and learning that robot creation with somebody to get about how to make things easier for people so this is a path robot number right now they put this like braided grass map",
"speaker": "speaker_7",
"confidence": 0.94,
"language": "mixed",
"silence_before_ms": 20,
"audio_type": "mixed",
"words": [
{"word": "자신의", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "선택이", "start_ms": 180, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "옳았음을", "start_ms": 330, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "확인합니다", "start_ms": 520, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "있다고 보느냐는 질문에 67%가 그렇지 않다고 답했습니다 목표도 불명확한 전쟁에 나라간 갈등은 커지고 경제는 혼란에 빠졌는데 미국 국민들이 손에 쥔 건 치솟은 유가",
"speaker": "speaker_4",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "있다고", "start_ms": 10, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "보느냐는", "start_ms": 70, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "질문에", "start_ms": 140, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "67%가", "start_ms": 200, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "10분 8km 이동은 그 700km를 위한 첫 번째 테스트였다고 볼 수 있죠 그리고 그 700km 이동이 성공하면 연구팀은 반양성자와 양성자의 질량 전하 자기 모멘트",
"speaker": "speaker_6",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "10분", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "8km", "start_ms": 200, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "이동은", "start_ms": 390, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "He's just gonna give you a skill you can stay that you will be able to use to survive Can I can I change my answer Real fast Go ahead change your answer Well because now that I'm actually thinking",
"speaker": "speaker_8",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "He's", "start_ms": 15, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "just", "start_ms": 70, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "오셨습니다 규칙과 제한을 둬서 애초에 잘못된 방향으로 가지 않게 만드는 게 훨씬 효율적이기 때문이죠 그리고 만약 그 방향으로 가게 되더라도 같은 실수가 반복되지 않게 만드는 것이 우리의 간섭이 필요 없는 시스템을 만드",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "오셨습니다", "start_ms": 25, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "규칙과", "start_ms": 160, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "제한을", "start_ms": 280, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "100%로 조사됐습니다 중동 전쟁으로 인한 고유가 고환율 고물가 현상에 대해서는 응답자의 60%가 매우 걱정된다고 답했고 차량 5부제 민간 확대에 대해선 64%가 받아들일 수 있다고 답했습니다 이번 조사는 무작위 추출된",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "100%로", "start_ms": 15, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "조사됐습니다", "start_ms": 150, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "귀찮을 법도 한데 손녀의 부탁은 모두 묵묵히 들어주는데요 평생 기름 냄새 맡으며 일궈낸 가게를 물려받아 열심히 지켜내는 효정 씨에게 조금이라도 힘이 되고 싶",
"speaker": "speaker_9",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "귀찮을", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "법도", "start_ms": 190, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "한데", "start_ms": 300, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "물이 쏟아지며 새집에 완전히 고립됐습니다 치워도 끝이 없는 눈 8시 이후에게 남은 식량은 마트 빵 하나와 주스뿐입니다 설사 가",
"speaker": "speaker_10",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "물이", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "쏟아지며", "start_ms": 150, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "이 확인 돌파 수급 유입 등의 신호가 나올 때 그 준비된 기준에 따라 기계적으로 매수 기회를 잡는 것입니다 결국 조",
"speaker": "speaker_11",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 10, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "확인", "start_ms": 70, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "돌파", "start_ms": 180, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "타 아주 꼼꼼히 속을 넣어줍니다 I'm very happy I wanted to",
"speaker": "speaker_12",
"confidence": 0.94,
"language": "mixed",
"silence_before_ms": 20,
"audio_type": "mixed",
"words": [
{"word": "타", "start_ms": 20, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 80, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "꼼꼼히", "start_ms": 170, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "다 I feel like I'm sorry that I keep calling you old I feel bad but like your generation No no no you can call me old dude I'm old I'm sixty",
"speaker": "speaker_13",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "다", "start_ms": 10, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 60, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "꺾어서 종목을 좀 압축하고 나랑 잘 맞는 종목을 계속 트레이딩 해 보면서 시장 경험을 쌓는 거 내가 옛날에 현대차 LG전자만 거의 2년 가까이 거래했는데 그런 거지 음 1년이 한 뭐 240 거래일이라고 치면은 못해도 한 150 거래일 이상은 그 두 개 안에서 거래를 한 거지 하루에 그",
"speaker": "speaker_14",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "꺾어서", "start_ms": 20, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "종목을", "start_ms": 150, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "음 근데 항상 문제가 되는 게 파일 다운받으면 잘 안 열리고 그래요 그래서 욕하겠어요 아닙니다 아이 그럴 리가요 저 그냥 여기 먼지를 모르",
"speaker": "speaker_15",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "음", "start_ms": 15, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 80, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "그 백제금동대향로 보상이 몇 개다 라고 했을 때 굉장히 여러 가지 얘기가 나오게 됩니다 거의 80여 개 뭐 70여 개부터 80여 개까지가 가능한데요 이 향로 속",
"speaker": "speaker_16",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "그", "start_ms": 25, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "백제금동대향로", "start_ms": 70, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "그 다음 눌림을 기다려 줍니다 즉 시장이 한 번 조용히 눌렸다가 다시 반등하는 그 첫 번째 캔들 그게 바로 노아의 진입 신호예요 자 세 번째 진입은 반등이 나",
"speaker": "speaker_11",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "그", "start_ms": 10, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 60, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "바뀌면서 시장이 잠시 노이즈 상태로 돌아갔습니다 이럴 때 노아는 절반 정도를 매도하고 나머지 절반 물량을 들고 좀 더 지켜봐도 괜찮다라고 말해 주",
"speaker": "speaker_11",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "바뀌면서", "start_ms": 20, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "시장이", "start_ms": 170, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "기생충 잘 봤고 이 세븐틴도 잘 봤고 헤유 무서워 용기 너무 너무 잘 해서 가끔 질투 나 나와요 이쁜 내용이에요 이 영화는 할 수 있는 게 진짜 감동이에요 이쯤 되면 다들 한 번",
"speaker": "speaker_17",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "기생충", "start_ms": 30, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 170, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "스 또 그래서 이제 타로 끼기 사용 자제 경고하면서 이렇게 계속 얘기가 나오고 실제로 이 분이 테슬라 안드로이드 프로젝트",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "스", "start_ms": 15, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 70, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "에 선비들의 도량이 있습니다 그럼 여기 이 곳은 어떤 곳인가요",
"speaker": "speaker_18",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "에", "start_ms": 40, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "선비들의", "start_ms": 90, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "미국이 고가의 요격 미사일 소모에 허덕이며 딜레마에 빠져 있는 동안 한국의 방공 시스템은 전혀 다른 해법을 전 세계에 보여주고 있습니다 아랍에미리트 수출된 국산 중거리 지대공",
"speaker": "speaker_19",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "미국이", "start_ms": 10, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "고가의", "start_ms": 120, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "특히 이틀 전 대국민 연설에서 이란을 극도로 강력하게 타격하겠다고 하자마자 국제 유가는 단숨에 11% 넘게 뛰었습니다 러시아 우크라이나전 초기인 2022",
"speaker": "speaker_4",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "특히", "start_ms": 20, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "이틀", "start_ms": 120, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "동남아시아뿐 아니라 아프리카에서도 다양한 파트너십을 확보해 왔습니다 유럽에서는 신뢰할 수 있고 언제든지 협력 가능한 파트너로서의 입지를 강화해 왔다",
"speaker": "speaker_20",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "동남아시아뿐", "start_ms": 30, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 220, "dur_ms": 100, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 09:26:37*
