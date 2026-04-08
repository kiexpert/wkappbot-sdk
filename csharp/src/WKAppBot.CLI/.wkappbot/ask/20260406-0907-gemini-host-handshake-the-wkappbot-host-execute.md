# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=19672  time=09:05:27  uptime=16344s  mem=280MB  system_processes=412"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_090515.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 7416ms (file: 236-752-123-260-572-150-571-107-265-574-571-574-263-752-752-257-310-574-157-320-574-253-215-572-275-527-103-265-625-260-276-123-721-621-153-756-765-276-267-627-321-316-130-132-2_W.mp3)
  Segment 2: 8416ms ~ 15544ms (file: 132-136-103-132-132-132-312-312-132-103-132-132-103-132-132-132-132-132-312-103-102-153-103-132-103-312-103-103-103-102-210-103-130-103-132-201-105-103-276-531-527-215-132-201-2_V.mp3)
  Segment 3: 16544ms ~ 17120ms (file: 715-762-751-765-765 136-176-167-167-176-176-167_W.mp3)
  Segment 4: 18120ms ~ 18696ms (file: 157-507-105-710-175-107-157-517-751-150-235 105-105-175-105-107-107-751-715-157-170-751-107-170-157-175-105 105-105-105-517-150-105-157-150-150-170-701-105-157-501-107-170-157-1_V.mp3)
  Segment 5: 19696ms ~ 20272ms (file: 574-574-574-574-571-574-574-570-574-570-570-571-574 574-574-574-574-574-574-574-574-574-574-574-574-574-570-574-574-573-570-570-574-574-574 574-574-574-574-574-574-574-574-570 5_W.mp3)
  Segment 6: 21272ms ~ 21848ms (file: 172-157-215-721-157-172-571-157-725-572-517-215-512-217-752-357-563-513-175-172-713-371-756-765-716-765-675-275-756-751-571-751-715-715-176-713-176-713-716-715-175-731-137-137-7_W.mp3)
  Segment 7: 22848ms ~ 23424ms (file: 762-572-126-615-762-675-756-175-127-126-137-723-137-137-537-372-516-316-273-361-657-732-267-576-732-705-576-573-537-632-507-576-567-326-356-570 753-562-216-576-263-326 713-325-2_V.mp3)
  Segment 8: 24424ms ~ 31660ms (file: 761-237-503-231-203-302-102-321-276-267-213-321-132-231-375-327-316-320-217-120-157-126-125-130-136-302-203-326-207-217-230-372-320-321-570-105-132-130-310-672-132-217-720-275-1_W.mp3)
  Segment 9: 32660ms ~ 45080ms (file: 270-267-752-273-320-130-201-312-267-132-103-136-127-103-107-107-107-705-754-756-754-756-132-321-210-201-103-216-123-213-103-301-231-103-103-102-312-201-542-206-206-206-206-231-3_V.mp3)
  Segment 10: 46080ms ~ 46656ms (file: 705-175-107-150-107-107-571-107-105-701 501-570-510-105-751-105-750-507-751-710-710-170-170-175-715-517-175-571-156 517-105-501_W.mp3)
  Segment 11: 47656ms ~ 56440ms (file: 106-103-753-230-321-326-301-130-103-321-260-231-236-326-326-321-132-321-130-136-132-203-126-351-102-601-621-237-236-327-723-236-312-321-213-237-213-217-130-107-107-213-127-752-1_W.mp3)
  Segment 12: 57440ms ~ 67052ms (file: 301-672-165-160-261-163-312-325-326-312-102-310-231-376-213-312-320-675-756-576-130-130-752-275-106-237-527-327-357-320-276-672-126-321-136-716-263-103-326-372-367-310-267-326-1_W.mp3)
  Segment 13: 68052ms ~ 74064ms (file: 136-230-235-572-567-302-326-320-326-627-672-627-572-130-672-302-752-267-320-762-206-752-672-320-326-627-327-326-172-362-602-167-670-620-263-672-276-632-216-206-617-567-620-130-5_W.mp3)
  Segment 14: 75064ms ~ 75640ms (file: 726-753-613-132-276-326-721-123-762-206-267-206-326-206-236-765-250-756-752-507-127-623-652-546-531-675-236-570-526-206-236-267-267-526-567-357-251-267-316-260-361-376-316-371-1_V.mp3)
  Segment 15: 76640ms ~ 77216ms (file: 137-137-137-175 157-157-175-175-157-175-175-107-107 157-157-571-157-157-175-170-153-175-103-175-157-157-175-135-150 105-105-157-105-157-517-517-571-105-105-170-507-157-157-105_W.mp3)
  Segment 16: 78216ms ~ 78792ms (file: 157-150-105-105-107-751-501-175-150-157-517-105-107-710-107-751 150-517-150-157-105-170-175-150-107-105-170-105-517-710-175-175-710-705-750 750-105-715-751-507-107-705-107-175-1_V.mp3)
  Segment 17: 79792ms ~ 88612ms (file: 213-103-301-267-627-137-213-732-173-726-276-276-276-527-276-257-572-130-270-275-127-132-127-270-572-527-257-276-276-267-267-726-765-751-572-230-236-132-312-276-267-213-312-621-2_W.mp3)
  Segment 18: 89612ms ~ 90188ms (file: 762-673 571-571-517-750-751-735-751-753-751-752-752-752-173-751-753-753-753-751-315 572-571-571-571-756-571-576-571-576-576-576-753-357-573-576-572-537 537-573-576-573-576-756-7_W.mp3)
  Segment 19: 91188ms ~ 91764ms (file: 751-715 105-150-150-510-157-150-510-175-510-571-510-105-150-710-751-105-157-157-175-750-571-175-571-157 105-715-507-150-501-150-517-175-107-751-107-710-107-170-710-107-751-705-7_V.mp3)
  Segment 20: 92764ms ~ 93340ms (file: 312-160-106-160-175-157-137-137-107-157-120-201-102-127-125-127-712-127-621-263-621-612-651-652-651-652-652_V.mp3)
  Segment 21: 94340ms ~ 94916ms (file: 571 570-570-750-713-751-715-715-715-715-756-715-713-715-715-731-716-751-753 573-571-570-517-175-715-157-751-752-751-715-135-753-753 571-571-537-571-750-751-750-570-751-715-751-1_V.mp3)
  Segment 22: 95916ms ~ 96492ms (file: 627-607-132-103-576-125-750-765-352-750-326-126-162-216-261-216-206-273-572-157 756-712-657-275-261-672-263-721-532-572-627-257-367-321-126-237-576-263-765-627-320-321-574-567-5_W.mp3)
  Segment 23: 97492ms ~ 98068ms (file: 570-507-507-570-750-750-570-517-150-573-507-517-517-573-570-517-573-751-571-573-537-571-573-571-573-570-571-573-573-572-571 571-571-570-570-571-571-751-107-751-735-710-173-753-7_V.mp3)
  Segment 24: 99068ms ~ 99644ms (file: 573-570-571-537-573-573-517-570-537-571-537-531-537-513-573-513-571-571-573-537-571-571-573-517-571-537 571-571-573-157-517-135-153-513-531-513-573-517-571 571-571-157-150-157-5_W.mp3)
  Segment 25: 100644ms ~ 101220ms (file: 315-271-572-572-725-751-275-752-725-527-526-537-571-537-357-572-576-517-130-127-132-137-137-312-132 173-137-173-127-217-173-137-125-517-152_W.mp3)
  Segment 26: 102220ms ~ 102796ms (file: 175-175-170-175-715-710-157-175 617-167-627-617-671-765-576-715-106-107-706-701-176-170-715-715-175-751-715-175-713-175-170 675-671-617-675-175-765-716-715-170-701-715-750-751-7_W.mp3)
  Segment 27: 103796ms ~ 116576ms (file: 132-132-123-320-261-201-132-132-103-312-260-230-310-230-310-102-571-312-310-130-320-203-103-321-203-302-103-230-325-130-301-320-102-310-163-263-321-102-102-230-236-320-103-103-3_V.mp3)
  Segment 28: 117576ms ~ 123588ms (file: 537-657-563-537-315-231-213-752-261-172-176-761-706-267-263-267-627-716-310-130-312-321-321-752-327-230-623-103-126-321-123-716-321-753-357-213-762-756-231-675-725-752-312-672-6_W.mp3)
  Segment 29: 124588ms ~ 125164ms (file: 574-517-501-574-574-574-574-571-574-571-571-571-517 574-574-574-571-517-157-175-761-107-127-130-105-513-571-571-576-571-751-175-157-513-315-137-175-137-173-257-753-756-517-537-5_W.mp3)
  Segment 30: 126164ms ~ 126740ms (file: 130-130-170-175-715-175-715-756-715-715-175-167-175-175-176-175-175-176-176-175-176-175-176-176-167-765-715 103-130-130-153-175-157-175-715-751-175-175-175-176-175-175-175-175-1_V.mp3)
  Segment 31: 127740ms ~ 128316ms (file: 157-510-507-507-170-107-175-107-175-170-170-175-107-751-150-175-507-170-157 175-105-507-150-510-175-175-107-175-157-105-105-715-571-750-751-107-105-705-105-135 150-751-107-105-1_V.mp3)
  Segment 32: 129316ms ~ 129892ms (file: 752-137-312-301-316-135-705-756-572-572-620 275-750-316-570-163-213-175-321-726-213-571-762-571-312-752-237-560-752-752-356-635-573-572-765-576-672-326-732-327-516-312-570-237-6_V.mp3)
  Segment 33: 130892ms ~ 131468ms (file: 135-132-317-132-132-315-315 315-135-315-153-513-173-153-513-127-571-372-132-317 517-157-137-157-570-157-537-571-517-375-715-305-173 573-571-573-571-570-573-570-573_W.mp3)
  Segment 34: 132468ms ~ 133044ms (file: 571-571-517-573-571-571-571-571-571-571-517-517-517-571-571-574-573-574 513-517-517-571-751-571 574-574-571-574-571-157-570-510-517-731-571-571-571-570-571-571-157-756-753-715 5_W.mp3)
  Segment 35: 134044ms ~ 134620ms (file: 301-160-502-560-163-762-671-206-621-172-163-167-216-103-162-213-612-126-236-132-123-310-216-513-637-617-623-256-216-321-573-751-312-507-267-371-357-203-567-325-271-136-163-316-6_V.mp3)
  Segment 36: 135620ms ~ 143720ms (file: 260-326-130-103-163-230-126-167-576-627-230-320-136-260-572-103-263-756-263-236-326-321-167-231-263-627-675-756-275-127-231-206-127-310-372-273-167-310-127-130-157-312-230-203-3_W.mp3)
  Segment 37: 144720ms ~ 145296ms (file: 157-571-571-571-571-157-517-751-571-756 150-157-517-570 574-574-576-572-574-571-572-572-572-572-571-513-571-517-513-571-517-150-157 517-513-510-570-517-517-510-571-510-503-517-5_W.mp3)
  Segment 38: 146296ms ~ 146872ms (file: 576-574-576-752-573 570-571-750-570-750-571-750-175-570-570-570-530-570-570-571-570-571-576-572-571-572-573-537-521 571-501-510-573-571-571-710-751-571-531-517-751-573-357-130-5_W.mp3)
  Segment 39: 147872ms ~ 148448ms (file: 175-157-517-510-571-517-507-150-517_W.mp3)
  Segment 40: 149448ms ~ 150024ms (file: 150-130-157-153-105-157-105-105-150-513-150-103-107-103-107-170-163-105-157-103-173-715-703-105 105-105-173-153-156-175-165-107-107-137-175-175-157-157-103-105-176-750-710-105-7_V.mp3)
  Segment 41: 151024ms ~ 151600ms (file: 206-206-207-125-120-716-106-130-123-103-102-103-326-321-321-132-137-173-173-123-130-136-361-231-231-231-123-265-253-172-310-301-310-301-753-376-365-175-751-153-135-351-135-135-7_W.mp3)
  Segment 42: 152600ms ~ 160232ms (file: 203-273-276-207-310-301-123-102-132-301-301-753-106-612-310-326-102-310-756-270-230-321-130-132-756-207-752-370-570-210-201-527-206-103-230-123-123-260-201-206-172-103-102-107-2_V.mp3)
  Segment 43: 161232ms ~ 161808ms (file: 716 750-105-170-107-107-701-175-705-715-107-710-715-715-175-170-170-176-175-715-716-175-175-715 170-567-170-501-715-710-170-710-107-715-710-715-176-710-107-170-107-170-107-173-1_V.mp3)
  Segment 44: 162808ms ~ 169432ms (file: 201-127-162-127-217-201-267-263-752-672-756-310-132-157-361-621-230-260-143-261-754-201-210-756-765-672-675-756-567-126-123-201-261-267-236-762-524-723-132-130-123-230-103-123-1_W.mp3)
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
> **Time**: 2026-04-06 09:07:09
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "결과에 있어서 그리 만족스럽지 못할 것 같습니다 대신 앞서도 말했듯 이제 드디어 우리 KF-21의 개발이 완료되었고 양산을 시작했기 때문에 그리고",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "결과에", "start_ms": 20, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "있어서", "start_ms": 350, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "그리", "start_ms": 650, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "만족스럽지", "start_ms": 880, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "못할", "start_ms": 1400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 1680, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "같습니다", "start_ms": 1800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "대신", "start_ms": 2300, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "앞서도", "start_ms": 2580, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "말했듯", "start_ms": 2900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 3250, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "드디어", "start_ms": 3550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "우리", "start_ms": 3950, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "KF-21의", "start_ms": 4180, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "개발이", "start_ms": 4900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "완료되었고", "start_ms": 5280, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "양산을", "start_ms": 5950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "시작했기", "start_ms": 6400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "때문에", "start_ms": 6950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 7300, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "이것은 미래의 모습이 아니",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "이것은", "start_ms": 30, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "미래의", "start_ms": 420, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "모습이", "start_ms": 750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "아니", "start_ms": 1150, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 4,
"transcript": "효율 면에서 좀 떨어지는 것 같습니다",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "효율", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "면에서", "start_ms": 180, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 390, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "떨어지는", "start_ms": 480, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 740, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "같습니다", "start_ms": 800, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "공간이 협소해서 문을 달 수",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "공간이", "start_ms": 15, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "협소해서", "start_ms": 210, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "문을", "start_ms": 480, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "달", "start_ms": 610, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 700, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "직접 만들었답니다 비닐 깔고 그다음 철근 양생하고 이런 모든 과정들을 가족들이 같이 했는데 저희는 이제 인건비 안 들어가잖아요 그러면 이곳을 지을 때 재료는 아끼지 말자 그래서 강",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "직접", "start_ms": 35, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "만들었답니다", "start_ms": 250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "비닐", "start_ms": 750, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "깔고", "start_ms": 1050, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "그다음", "start_ms": 1300, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "철근", "start_ms": 1600, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "양생하고", "start_ms": 1850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 2300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "모든", "start_ms": 2500, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "과정들을", "start_ms": 2720, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "가족들이", "start_ms": 3150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 3550, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "했는데", "start_ms": 3800, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "저희는", "start_ms": 4200, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 4500, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "인건비", "start_ms": 4700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 5050, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "들어가잖아요", "start_ms": 5200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "그러면", "start_ms": 5750, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "이곳을", "start_ms": 6050, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "지을", "start_ms": 6350, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 6550, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "재료는", "start_ms": 6700, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "아끼지", "start_ms": 7000, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "말자", "start_ms": 7300, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "와 끈기가 진짜 대단하시다 그래서 누가 이기나 보자 세화라 네가 이기나 내가 이기나 보자 뭐 거의 그 수준인 거 같은데요",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "와", "start_ms": 20, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "끈기가", "start_ms": 150, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 420, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "대단하시다", "start_ms": 650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 1200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "누가", "start_ms": 1550, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이기나", "start_ms": 1750, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "보자", "start_ms": 2050, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "세화라", "start_ms": 2300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "네가", "start_ms": 2650, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이기나", "start_ms": 2850, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "내가", "start_ms": 3150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이기나", "start_ms": 3350, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "보자", "start_ms": 3650, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 3900, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "거의", "start_ms": 4000, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 4200, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "수준인", "start_ms": 4300, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 4600, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "같은데요", "start_ms": 4750, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "주변에서 집 짓다 망했다는 소리",
"speaker": "speaker_6",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "주변에서", "start_ms": 15, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "집", "start_ms": 350, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "짓다", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "망했다는", "start_ms": 600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "소리", "start_ms": 980, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "적극적으로 이런 하드웨어를 많이 돌리는 걸 이용하려고 할 때 훨씬 더 가속을 잘 할 수가 있겠죠 그만큼 맥 사용자들의 비중이 많아지다 보니까 맥이라든가 아니면은 이런 아이폰이라든가 이런 것들을 바로바로 빠르게 처리를 할 수 있도록 오픈 모델을 이용해 가지고 이렇게 되다 보니까",
"speaker": "speaker_7",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "적극적으로", "start_ms": 25, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "하드웨어를", "start_ms": 580, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 950, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "돌리는", "start_ms": 1150, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "걸", "start_ms": 1450, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이용하려고", "start_ms": 1600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 2000, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 2100, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "훨씬", "start_ms": 2250, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 2500, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "가속을", "start_ms": 2620, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 2900, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 3050, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "수가", "start_ms": 3150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있겠죠", "start_ms": 3350, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "그만큼", "start_ms": 3700, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "맥", "start_ms": 4000, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "사용자들의", "start_ms": 4120, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "비중이", "start_ms": 4550, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "많아지다", "start_ms": 4850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "보니까", "start_ms": 5200, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "맥이라든가", "start_ms": 5500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아니면은", "start_ms": 5950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 6300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "아이폰이라든가", "start_ms": 6500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 7050, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "것들을", "start_ms": 7250, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "바로바로", "start_ms": 7500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "빠르게", "start_ms": 7850, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "처리를", "start_ms": 8150, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 8450, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 8550, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "있도록", "start_ms": 8650, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "오픈", "start_ms": 8950, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "모델을", "start_ms": 9200, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "이용해", "start_ms": 9500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "가지고", "start_ms": 9850, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 10200, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "되다", "start_ms": 10500, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "보니까", "start_ms": 10750, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "텍스트 길이도 작은 모델은 128k 토큰까지 그리고 큰 모델은 256k 토큰까지 되니까 정말 쓸만해졌거든요 아까도 말씀드렸다시피",
"speaker": "speaker_7",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "텍스트", "start_ms": 15, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "길이도", "start_ms": 300, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "작은", "start_ms": 600, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "모델은", "start_ms": 800, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "128k", "start_ms": 1100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "토큰까지", "start_ms": 1550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 2000, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 2300, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "모델은", "start_ms": 2450, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "256k", "start_ms": 2750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "토큰까지", "start_ms": 3200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "되니까", "start_ms": 3650, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 3950, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "쓸만해졌거든요", "start_ms": 4200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "아까도", "start_ms": 4750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "말씀드렸다시피", "start_ms": 5100, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "간격을 이제 좀 좁혔어요",
"speaker": "speaker_6",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "간격을", "start_ms": 20, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 420, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "좁혔어요", "start_ms": 530, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "인건비가 제로니 누릴 수 있었던 작은 사치 그렇게 기초에만 오 개월이 걸렸다네요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "인건비가", "start_ms": 25, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "제로니", "start_ms": 350, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "누릴", "start_ms": 650, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 880, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "있었던", "start_ms": 1000, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "작은", "start_ms": 1300, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "사치", "start_ms": 1550, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "그렇게", "start_ms": 1850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "기초에만", "start_ms": 2200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "오", "start_ms": 2600, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "개월이", "start_ms": 2750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "걸렸다네요", "start_ms": 3100, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "관련해서는 요 얘기를 좀 해 보도록 하겠습니다 일단 제가 특사경 수사 지휘 권한을 왜 지적을 했느냐라는 것부터 설명을 드릴게요 특사경 수사 지휘 권한은 사실 중요하지 않았습니다 검찰 입장에서 볼 때는 검찰이 특사경과 짬짬이를 해서 사건을 만들거나 특사경 수사 영역에",
"speaker": "speaker_8",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "관련해서는", "start_ms": 30, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "요", "start_ms": 400, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "얘기를", "start_ms": 500, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 800, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "해", "start_ms": 950, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "보도록", "start_ms": 1050, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "하겠습니다", "start_ms": 1350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "일단", "start_ms": 1800, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 2100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "특사경", "start_ms": 2350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "수사", "start_ms": 2700, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "지휘", "start_ms": 2950, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "권한을", "start_ms": 3250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "왜", "start_ms": 3600, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "지적을", "start_ms": 3750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "했느냐라는", "start_ms": 4100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "것부터", "start_ms": 4550, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "설명을", "start_ms": 4850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "드릴게요", "start_ms": 5200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "특사경", "start_ms": 5600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "수사", "start_ms": 5950, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "지휘", "start_ms": 6200, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "권한은", "start_ms": 6500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "사실", "start_ms": 6850, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "중요하지", "start_ms": 7100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "않았습니다", "start_ms": 7500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "검찰", "start_ms": 7950, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "입장에서", "start_ms": 8250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "볼", "start_ms": 8650, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "때는", "start_ms": 8800, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "검찰이", "start_ms": 9100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "특사경과", "start_ms": 9450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "짬짬이를", "start_ms": 9900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "해서", "start_ms": 10300, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "사건을", "start_ms": 10550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "만들거나", "start_ms": 10900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "특사경", "start_ms": 11300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "수사", "start_ms": 11650, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "영역에", "start_ms": 11900, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "국무부 핵심 정보 기관들이 판단하는 거죠 승인 조건 사실상 핵심은 기술 이전 제한 미국 내 생산 유지",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "국무부", "start_ms": 15, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "핵심", "start_ms": 350, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "정보", "start_ms": 600, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "기관들이", "start_ms": 900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "판단하는", "start_ms": 1350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "거죠", "start_ms": 1800, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "승인", "start_ms": 2050, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "조건", "start_ms": 2350, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "사실상", "start_ms": 2650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "핵심은", "start_ms": 3000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "기술", "start_ms": 3400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "이전", "start_ms": 3700, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "제한", "start_ms": 4000, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "미국", "start_ms": 4300, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "내", "start_ms": 4600, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "생산", "start_ms": 4750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "유지", "start_ms": 5100, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "Good protein We do Now it's time to eat your favorite Korean gukbap My favorite? Your favorite I don't trust you",
"speaker": "speaker_9",
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
"segment": 16,
"transcript": "대만이 보이콧한 등이 포함된다 그러나 전문가들과 일부 정치인들은 이러한 조치의 실효성에 의문을 제기하고 있다 대부분의 국가들이 외교적으로 베이징을 인정하고 있기 때문이다 이번 논란",
"speaker": "speaker_10",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "대만이", "start_ms": 20, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "보이콧한", "start_ms": 350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "등이", "start_ms": 800, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "포함된다", "start_ms": 1050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "그러나", "start_ms": 1500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "전문가들과", "start_ms": 1850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "일부", "start_ms": 2400, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "정치인들은", "start_ms": 2650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이러한", "start_ms": 3200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "조치의", "start_ms": 3550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "실효성에", "start_ms": 3900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "의문을", "start_ms": 4400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "제기하고", "start_ms": 4750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "있다", "start_ms": 5200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "대부분의", "start_ms": 5400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "국가들이", "start_ms": 5850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "외교적으로", "start_ms": 6300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "베이징을", "start_ms": 6850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "인정하고", "start_ms": 7300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "있기", "start_ms": 7750, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "때문이다", "start_ms": 8000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이번", "start_ms": 8450, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "논란", "start_ms": 8700, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "것은 참여입니다 자리를 지키고 목소리를 내고 관계를 유지하는 것이 핵심입니다 그런데 스스로 자리를 비워 버리면 그 순간부터 존재감은 급격히 죽",
"speaker": "speaker_11",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "것은", "start_ms": 10, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "참여입니다", "start_ms": 250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "자리를", "start_ms": 750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "지키고", "start_ms": 1100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "목소리를", "start_ms": 1500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "내고", "start_ms": 1950, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "관계를", "start_ms": 2250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "유지하는", "start_ms": 2650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "것이", "start_ms": 3100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "핵심입니다", "start_ms": 3350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "그런데", "start_ms": 3850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "스스로", "start_ms": 4200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "자리를", "start_ms": 4600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "비워", "start_ms": 4950, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "버리면", "start_ms": 5250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 5650, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "순간부터", "start_ms": 5750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "존재감은", "start_ms": 6200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "급격히", "start_ms": 6700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "죽", "start_ms": 7100, "dur_ms": 50, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 09:07:09*
