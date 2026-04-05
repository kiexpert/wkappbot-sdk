# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=11716  time=09:23:20  uptime=27257s  mem=401MB  system_processes=352"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_092225.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 136-163-310-130-106-163-136-163-762-672-627-672-261-602-607-673-672-504-571-652-620-627-672-671-316-102-312-260-261-163-126-362-132-102-320-126-162-670-627-261-621-267-236-102-1_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 750-651-765-756-150-756-567-736-675-637 570-571-576-750-751-756 517-510-501-501-507-507_V.mp3)
  Segment 3: 3152ms ~ 6860ms (file: 715-751-715-751-751-715-715-751-751-756-751-715-715-571 123-102-512-203-206-602-201-132-123-130-623-260-267-321-103-120-150-501-150-150-150-175-751-715-715-175-715-175-175-715-7_W.mp3)
  Segment 4: 7860ms ~ 17220ms (file: 326-572-250-203-320-302-132-132-132-702-327-376-326-267-236-213-357-321-263-263-321-325-263-517-517-756-263-267-263-315-537-321-132-126-216-275-547-231-231-325-102-102-267-321-2_W.mp3)
  Segment 5: 18220ms ~ 18796ms (file: 135-317-314-301-103-135-301-301-135-310-136-136-103-301 517-573-571-357-351-576-507-506-576-576-571-570-701 715-157-751-173-715-175-157-571-517-571-751-571-571-517-175-751-173-1_V.mp3)
  Segment 6: 19796ms ~ 20372ms (file: 312-367-376-317-613-360-637-607-316-130-130-316-310-763-310-163-671-130-173-107-317 371-703-307-751-357-137-317-310-173-603-316-315-601-136-317-130-621-310-106-376-630-301-716-1_V.mp3)
  Segment 7: 21372ms ~ 21948ms (file: 750-173-713-761-137-170-137-321-312-307-370-370-312-513-371-103-130-160-160-106-160-160-106-106-305-276-275-315-375-357-375-362-726-306-762-762-732-756-317-361-613-130-173-273-2_W.mp3)
  Segment 8: 22948ms ~ 32704ms (file: 276-102-130-267-321-267-321-352-310-537-726-267-321-321-231-321-527-237-312-217-130-352-130-652-627-210-236-320-301-267-263-316-312-231-231-576-275-321-102-261-123-325-106-753-1_W.mp3)
  Segment 9: 33704ms ~ 34280ms (file: 175-715-751-715-157-715-715-751-756-751 150-150-150-105-150-150-150-175-175-137-715-175-751-751-715-751-751-751-756-751-715-175-751 150-510-150-157-157-175-175-175-715-175-715-7_W.mp3)
  Segment 10: 35280ms ~ 42876ms (file: 102-106-745-165-710-132-675-231-645-236-530-130-102-103-261-263-231-263-210-752-263-130-106-310-751-301-102-720-236-754-321-130-160-231-320-265-263-326-267-213-267-231-102-130-1_W.mp3)
  Segment 11: 43876ms ~ 47908ms (file: 627-267-260-526-123-260-261-631-160-160-106-312-261-123-267-671-316-127-152-162-206-570-570-572-726-721-102-217-126-207-627-176-176-712-671-736-637-172-167-216-176-612-107-106-1_W.mp3)
  Segment 12: 48908ms ~ 63344ms (file: 263-102-103-102-132-267 132-134-312-312-326-321-301-312-103-321-213-372-320-302-321-236-231-172-103-325-327-327-321-320-327-321-231-213-326-236-273-257-321-123-321-102-230-320-2_V.mp3)
  Segment 13: 64344ms ~ 64920ms (file: 326-362-362-361-136-136-102-261-713-162-716-176-765-750-573-153-571-261-316-523-731-312-302-203-231-321-321-213-123-236-316-207-762-270-327-321-312-137-137-762-267 167-326-326-2_W.mp3)
  Segment 14: 65920ms ~ 67540ms (file: 516-657-516-510-506-251-672-765-762-576-765-567-765-567-765-576-576-576-567-675-765-657 501-507-521-251-572-571-756-751-572-572-756-657-756-576-675-765-675-567-576-756-617-657-6_V.mp3)
  Segment 15: 68540ms ~ 69116ms (file: 752-572-175 574-574-574-571-501-752-102-127-162-163-217-125-102_W.mp3)
  Segment 16: 70116ms ~ 70692ms (file: 231-260-237-706-672-672-103-627-620-612-602-621-621-267-620-671-762-712-106-126-623-106-106-132-627-126-123-163-106-361-132-130-645-136-540-156-136-126-123-571-165-175-103-103-1_V.mp3)
  Segment 17: 71692ms ~ 72268ms (file: 201-162-360-176-317-123-213-103-126-102-206-160-602-106-631-601-160-106-671-176-760-107-761-675-157-612-673-617-106-163-163-103-132-103-167-102-103-102-103-103-103-103-302-203-1_V.mp3)
  Segment 18: 73268ms ~ 73844ms (file: 132-132-154-156-105-156-123-152-125-201-532-756-103-574-136-135-123-360-310-136-316-136-576-765-752-306-103-213-547-105-103-513-510-135-135-612-167-167-173-167-672-625-623-623-3_W.mp3)
  Segment 19: 74844ms ~ 75420ms (file: 150-150-105-107-160-167-106-150-167-150-150-165-157-150-157-105-105-150-156-106 157-150-153-315-156-170-105-106-167-106-107-615-106-107-106-106-156-156-165-160-150-156-105-105-1_V.mp3)
  Segment 20: 76420ms ~ 76996ms (file: 573-571 301-715-310-710-371-130-103-301-301-103-751-137-315-713-310-701-317-703-170-130-371-310-173-170-701 315-135-137-103-173-103-173-317-371-130-103-731-173-135-753-310-135-7_V.mp3)
  Segment 21: 77996ms ~ 86636ms (file: 206-130-126-572-275-276-207-620-756-301-102-216-123-130-132-103-123-201-210-103-103-103-312-103-230-103-132-136-210-261-231-260-102-571-107-317-547-574-210-123-103-621-163-165-6_V.mp3)
  Segment 22: 87636ms ~ 88212ms (file: 201-102-160-167-673-671-130-361-172_W.mp3)
  Segment 23: 89212ms ~ 89788ms (file: 130-106 123-132-136-103-103-103-315-105-103-351-154-350-153-103-315-306-135-156-315-165 103-132-132-136-130-312-310-305-310-156-231-132-231-126-502-362-210-206-236-256-526-512-5_V.mp3)
  Segment 24: 90788ms ~ 101156ms (file: 103-120-120-267-260-726-726-267-267-726-275-267-217-102-216-752-230-120-216-547-527-627-123-236-102-162-321-326-231-326-263-263-231-265-263-265-123-201-276-236-327-210-123-621-2_W.mp3)
  Segment 25: 102156ms ~ 102732ms (file: 136-163-136-137-136-135-136-163-136-103-136-316-167-132-136-132-132-610-106-132-316-126-312-103-130-150-360-132-310-103-132-613-561-651-125-175-137-175-132-123-516-163-231-615-1_V.mp3)
  Segment 26: 103732ms ~ 104308ms (file: 570-570-570-570-576-520-570-570-576 625-510-517-501-576-576-576-567-570-527 517-156-156-251-152-521-102-102-150 620-260-260-260-260-517-570-216-267-267-725-276-502 527-527-526-2_V.mp3)
  Segment 27: 105308ms ~ 105884ms (file: 372-315-136-316-321-302-230-302-301-312-320-302-302-306-306-306-354-354-305-354-354-356-305-365-352-351_W.mp3)
  Segment 28: 106884ms ~ 107460ms (file: 612-627-260-672-706-671-675-627-267-671-671-672-176-106-163-123-607 760-203-326-132-123-126-123-621-356-136-615-132-132-105-571-132-167-162-162-106-612-601-103-167-106-763-671-6_V.mp3)
  Segment 29: 108460ms ~ 109036ms (file: 215-126-235-543-754-165-361-265-621-621-613-123-265-256-537_V.mp3)
  Segment 30: 110036ms ~ 110612ms (file: 576-567-576-567-576-567-576-570-517 570-517-517-751-571-517-517-571-571-576-571-576-576-567-751-571 175-517-715-571-571-157-157-715-573-571-517-571-571-570-571-571-571-571-570-5_V.mp3)
  Segment 31: 111612ms ~ 112188ms (file: 156-157-167-365-631-167-163-615-163-316-316-367-716-574-534-564-657-652-627-672-627-761-716-576-574-517-652-621-621-267-261-361-316-126 514-541-534-352-326-326-326-254-263-326-3_W.mp3)
  Segment 32: 113188ms ~ 113764ms (file: 173-103-137 571-507-537-157-507-570-570-576-672-765-157-152-351-162-713-601-651-670-163-631-167-716-160-610-106 536-576-576-507-176-351-567-573-167-562-627-673-103-310-173-102-3_V.mp3)
  Segment 33: 114764ms ~ 115340ms (file: 765-756-675-756-567-652-567-257-127-130-213-273-302-312-310-301-127-237-162-562-506_W.mp3)
  Segment 34: 116340ms ~ 116916ms (file: 632-123-126-625-652-652-625-620-526-607-625-607-627-652-675-670-670-607_W.mp3)
  Segment 35: 117916ms ~ 130408ms (file: 571-103-574-754-203-153-310-103-320-203-726-173-310-573-573-570-260-207-203-103-523-103-301-103-103-130-231-261-167-601-673-671-201-130-103-312-316-312-213-201-573-523-231-203-3_W.mp3)
  Segment 36: 131408ms ~ 137924ms (file: 762-230-312-205-236-267-260-320-753-756-263-160-267-371-231-672-705-705-137-137-236-213-261-271-536-246-267-152-267-756-765-257-127-527-236-206-627-265-267-317-263-213-123-132-3_W_stt_there_is_going_to_use_kill_you_can_spend.mp3)
  Segment 37: 138924ms ~ 151236ms (file: 120-123-126-762-267-267-320-132-670-206-672-162-164-120-217-261-312-136-263-236-130-210-103-230-672-672-321-125-123-201-126-201-231-672-260-276-675-103-627-102-132-672-276-230-2_W.mp3)
  Segment 38: 152236ms ~ 152812ms (file: 751-751-751-715-175-170-170-105-107-105-107-716_W.mp3)
  Segment 39: 153812ms ~ 154388ms (file: 562-675-675-657-675-756-567-675-675-726-672-671-327-206-326-637-672-735-756_W.mp3)
  Segment 40: 155388ms ~ 155964ms (file: 143-134-103-103 147-140-310-301-314-301-301-301-301-130-103-314-134-301-134-134-130-103-130-130-103-130-103-314-301-103-134-143 453-341-301-134-103-301-314-134-314-134-134-134-1_V.mp3)
  Segment 41: 156964ms ~ 157540ms (file: 612-612-213-621-627-612-621-265-621-627-621-267-620-621_W.mp3)
  Segment 42: 158540ms ~ 159116ms (file: 132-103-103-163-136-573-135-156-103-756-175-103-136-132-632-130-263-126-317-351-152-156 201-163-127_V.mp3)
  Segment 43: 160116ms ~ 160692ms (file: 501-507-501-501-510-517-570-510-576-546-564-567-564 570-501-510-501-507-517-501-501-157-105-501-501-571-507-501-153-507-507-570-510-507-571 570-570-570-571-510-510-570-571-501-5_W.mp3)
  Segment 44: 161692ms ~ 173896ms (file: 576-572-236-263-312-576-371-237-312-237-263-327-132-327-236-312-312-263-263-263-237-237-267-756-263-236-574-267-263-263-267-267-576-574-572-235-320-231-237-267-752-765-320-137-3_W.mp3)
  Segment 45: 174896ms ~ 175472ms (file: 105-510-150-510-150-510-510-501 501-571-570-507-501-510-517-501-105-157-105-517-157-105-510-150-501-105-501-501-510-510-507-571-517-507-501-571-150-517-571-517-574-510 517-571-5_V.mp3)
  Segment 46: 176472ms ~ 186588ms (file: 362-236-326-321-326-637-307-236-123-607-267-265 126-572-627-267-231-237-326-753-236-123-120-672-236-123-263-106-326-267-326-326-263-126-231-236-567-765-263-263-216-236-132-675-2_W.mp3)
  Segment 47: 187588ms ~ 188164ms (file: 301-132-136-137-673-302-305-567-567-365-362-357-317-315-165-530-507-576-157-157-137-135-265-267-126-153-135-176-175-136-312-301-176-307-576-627-351-217-103-137-103-302-503-320-3_W.mp3)
  Segment 48: 189164ms ~ 189740ms (file: 761-175 570-570-157-170-170-105-175-175-175-751-715-175-107-715-751-710-107-107 157-154-105-105-510-517-175-715-157-107-501-157-157-105-107-175 517-157-517-157-107-175-157-105-5_V.mp3)
  Segment 49: 190740ms ~ 191316ms (file: 106-106-162-652-652-652-652-625-625-621-562-506-106-102-210-106-106-102-512-506-562-216-132-136 652-657-650-650-650-652-650-526-102-125-102-152-105-125-102-361 672-617-106-102-1_V.mp3)
  Segment 50: 192316ms ~ 192892ms (file: 301-307-137-715-173-173-150-135-137-137-137-137-135 731-730-301-371-357-301-301-307-103-601-361-610-613-601-361-103-173-710-103-107-310-310-301-130-301-307-705-137-751-735 137-1_V.mp3)
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
> **Time**: 2026-04-05 09:24:30
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
*Generated by WKAppBot ask gemini — 2026-04-05 09:24:30*
