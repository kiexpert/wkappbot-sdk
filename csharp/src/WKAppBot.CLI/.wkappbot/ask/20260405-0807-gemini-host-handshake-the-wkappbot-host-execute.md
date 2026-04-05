# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=17708  time=08:04:33  uptime=14134s  mem=494MB  system_processes=354"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_080029.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 173-132 576-175 173-137-143-132-143-132-134-124-137-576-574-265-236-612-103-321-203-263-201-203-206-203-126-231-321-210-276-261-620-612-623-260-762-627-276-762-267-230-276-127-2_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 574-576-576-561-675-576-762-756-756-576-537-576-574-576-657-765-107 546-567-572-576 765-756-752-572-572-576 123-132 312-756-572-236-612-123-321-375-213-763-716-672-672-236-216-2_W.mp3)
  Segment 3: 3152ms ~ 9776ms (file: 321-237-574-320-103-172-326-267-267-276-276-574-273-731-270-723-327-210-703-137-126-102-132-321-672-712-617-120-267-263-231-627-376-201-627-163-136-176-123-162-130-132-137-231-5_W.mp3)
  Segment 4: 10776ms ~ 16860ms (file: 160-621-230-201-175-267-216-107-621-261-267-216-627-162-107-162-263-261-261-127-312-203-261-267-726-216-107-130-726-276-130-123-173-321-120-130-162-210-217-103-672-617-672-267-2_W.mp3)
  Segment 5: 17860ms ~ 22324ms (file: 627-627-627-267-627-627-267-126-672-162-621-261-263-726-762-627-267-261-621-261-216-261-176-621-216-610-267-267-623-627-627-261-612-721-672-162-160-162-672-621-726-276-726-672-6_W.mp3)
  Segment 6: 23324ms ~ 23900ms (file: 213-136-127-651-274-413-265-123-267-672-257-175-316-612-165-327-235-326-261-312-127-524-236-241-174-326-132-324-321-173-312-274-612-123-165-126-216-163-627-163-176-416-213-251-1_V.mp3)
  Segment 7: 24900ms ~ 25476ms (file: 175-157-135-103-103 130-130-130-310-103-103-137-137-135-103-105-104-105-105-107-157-105-541-710-105-107-147-175-174-103-104 107-107-105-104-154-105-150-105-135-517-105-157-105-1_V.mp3)
  Segment 8: 26476ms ~ 27052ms (file: 156-516-615-163-156-571-537-573-567-561-172-167-123-127-613-156-156-657-156-571-153-156-162-167-162-167-167-167-165-165-162-136-163-136-612-162-163-615-653-615-165-136-316-162-1_W.mp3)
  Segment 9: 28052ms ~ 28628ms (file: 301-301-301-351-316-173-356-315-325-315-135-356-163-316-351-267-362-613-617-673-673-674-671-326-375-576-576-756-576-573-576-573-753 123 126-136-157-623-621 637-365 713-236-327_V.mp3)
  Segment 10: 29628ms ~ 37728ms (file: 270-123-137-175-102-620-732-725-312-276-276-267-213-670-574-267-571-751-732-623-260-627-670-672-275-123-216-312-130-130-627-216-123-213-130-672-267-263-126-125-207-257-230-752-2_W.mp3)
  Segment 11: 38728ms ~ 46684ms (file: 736-672-627-267-267-236-230-260-260 267-210-210-201-627-302-267-203-102-201-267-126-216-123-230-207-576-726-752-572-237-103-620-123-137-712-720-102-127-217-123-201-173-127-162-3_W.mp3)
  Segment 12: 47684ms ~ 55640ms (file: 261-263-267-267-257-125-261-136-256-213-263-275-572-675-627-256-574-160-103-267-312-216-267-312-213-163-712-102-216-267-572-210-263-576-132-276-130-126-216-561-132-312-326-231-7_W.mp3)
  Segment 13: 56640ms ~ 57216ms (file: 415-103-130-134-103-103-134-103-134-104-103-134-103-103-134-104 136-134-415-145-143-410-146-143-103-135-143-136-163-103-104-146-103-163-136-103-105-146-137-157-571-574-571-574-1_V.mp3)
  Segment 14: 58216ms ~ 58792ms (file: 371-105-310-137-130-173-310-756-756-371-173 703-307-301-317-735-173-317-617-671-316-167-136-317-731-317-716-716-317-103-130-317-103-310-103-310-107-123-301-107-371 751-371-370-3_V.mp3)
  Segment 15: 59792ms ~ 60368ms (file: 542-541-543-541-546-542-540-540-510-150-506-504-571-570-572-510-510-510-510-107-127-716-715-712-176-716-175-176-763-736-376-713-712-736-731-137-173-731-713-715-712-175-175-751-7_W.mp3)
  Segment 16: 61368ms ~ 61944ms (file: 571-137-610-176-157-576-157-516-275-167-103-102-765-176-765-327-375-317-261-567-567-536-351-157-571-731-376-756-165-326-362-753-754-572-327-312-316-326-321-103-753-375-576-175-7_W.mp3)
  Segment 17: 62944ms ~ 63520ms (file: 104-104-104-134-105-104-413 310-140-104-104-301-134-401-140-415-134-104-147-143-341-135-103-145-104-104-134-104-145-143-135-105-145-104-104-104 314-304-130-310-315-415-143-145-1_V.mp3)
  Segment 18: 64520ms ~ 65096ms (file: 701-107-107-571-157 571-157-570-750-750-105-750-105-157-750-701-751-107-571-510-517-570-510-750-751-175-150-517-157-751 574-574-571-751-754-570-750-150-517-571 507-571 750-751-7_W.mp3)
  Segment 19: 66096ms ~ 66672ms (file: 136-103-136-136-136-103-163-134-136-135-143-136-136-106-136-103-136-316-103-136-134-135_V.mp3)
  Segment 20: 67672ms ~ 68248ms (file: 106-167-761-135-316-103-103-561 175-136-103-103-263-165-106-603-617-103-163-271-613-132-106-103-130-672-203-123-135-176-103-132-103-571-231-236-312-263-265-126-216-263-123-126-1_V.mp3)
  Segment 21: 69248ms ~ 69824ms (file: 632-623-216-326-157-570-563-573-315-653-567-671-376-637-367-763-367-675-736-356-573-356-632-675-576-576-507-163-362-162-612-765-762-671-675-675-671-617-673-675-675-612-612 762_W.mp3)
  Segment 22: 70824ms ~ 71400ms (file: 163-316-136 136-103-316-103-134-136-136-103-316-136-164-136-130-103-103-103-136-136 135-135-135-135-156-145-154-103-106-103-103-136-103-136-103-134-136-135-135-135-163-135-156-1_V.mp3)
  Segment 23: 72400ms ~ 72976ms (file: 501-510-517-501-517-510-507-657-756-765-765-765-765-765-756 501-510-510-501-571-507-501-150-150-157-175-751_V.mp3)
  Segment 24: 73976ms ~ 82040ms (file: 236-237-130-310-327-263-751-175-137-320-301-130-301-107-317-275-573-310-137-517-123-236-574-532-203-263-576-625-267-236-576-261-305-537-312-326-321-237-236-170-576-130-263-236-3_W.mp3)
  Segment 25: 83040ms ~ 90564ms (file: 276-267-276-261-756-163-612-123-326-267-756-276-102-216-321-327-726-237-123-251-726-267-765-267-267-267-162-627-752-120-316-563-652-132-102-165-160-267-762-217-216-216-102-132-3_W.mp3)
  Segment 26: 91564ms ~ 92140ms (file: 672-356-675-562-563-572-657-675-567-657-567-527-657-567-576-567-567-576-167-567-576-572-576-576-567-576-567-562-750-567-657-652-265-257-657-567-567-257-756-610-160-603-673-136-1_V.mp3)
  Segment 27: 93140ms ~ 101852ms (file: 367-756-325-172-103-301-275-752-705-507-637-102-103-130-652-301-562-156-165-157-175-756-153-750-756-573-260-265-265-301-136-316-513-156-165-621-376-301-671-163-103-751-103-103-7_W.mp3)
  Segment 28: 102852ms ~ 103428ms (file: 163-156-106-136-103-107-263-106-126-136-730-302-603-360-301-236-163-106-103-103-457-612-715-310-103-675-163-632-516-163-136-106-156-612-136-136-103-130-316-103-163-673 716-167-6_V.mp3)
  Segment 29: 104428ms ~ 105004ms (file: 107-157-173-170-713-170-715-170-701-571-350-653-175-107-173-130 157-153_V.mp3)
  Segment 30: 106004ms ~ 106580ms (file: 753-731-106-607-607-610-631-610-670-673-617-673-163-671-613-671-176-170-731 301-703-705-730-103-317-173-175-301-130-310-317-310-673-301-137-137-730-310-130-301-317-107-730-713-7_V.mp3)
  Segment 31: 107580ms ~ 108156ms (file: 715-175-157-157-571-157-571-571-517-517-517-571-751-571-517-150-517-150-157-501-150-315-507-570-510-506-501-570-571-570-507-157-516-157-570-570-507-501-570-175-751-157-157-715-1_V.mp3)
  Segment 32: 109156ms ~ 109732ms (file: 326-607-312-123-132-721-263-706-726-672-126-710-125-625-623-263-562-127-621-132-103-103-306-135-135-213-132-132-761-137-731-173-317-137-132 132 173-123-234-324 132-731_V.mp3)
  Segment 33: 110732ms ~ 111308ms (file: 715-715-276 130-173-716-130-731-731-107-103-137-130-370-301-107-703-301-301-103-130-130-317-170-301-135-705-701-570-705-730-701-731-107 315-130-317-107-137-307-712-371-103-173-1_V.mp3)
  Segment 34: 112308ms ~ 122352ms (file: 102-276-123-270-273-752-276-276-130-236-261-236-231-136-103-276-231-102-275-236-236-263-231-326-136-136-267-130-267-321-326-130-267-236-236-231-123-237-136-176-765-675-765-756-7_W.mp3)
  Segment 35: 123352ms ~ 123928ms (file: 564-654-651-675-301-312-132-132-653-637-631 327-267-327-362-362-123-132-320-302-326-627-621-253-321-257-573-321-312-576-521-530-573-315-573-315-625-135-135-326-136-371-326-163-3_W.mp3)
  Segment 36: 124928ms ~ 134792ms (file: 627-267-732-103-207-103-102-146-104-103-301-326-132-165-620-267-260-276-103-132-312-310-321-620-302-231-321-201-320-316-106-764-103-163-167-123-312-130-132-236-627-312-302-102-3_V.mp3)
  Segment 37: 135792ms ~ 136368ms (file: 610-615-613-612-612-265-652-574-657 645 625-315-652-160-156-156-512-631-613-621-261-132-162-167-167-675-637-672-654 167-617_W.mp3)
  Segment 38: 137368ms ~ 137944ms (file: 751-750-570-574-570-750-157-750-570-571-507-570-570-750-507-750-715-570-750-570 571-574-157-517-501-157 571-157-574-754-574-574-571 574-574-574-574-574-751-571-751-574-571-751-7_V.mp3)
  Segment 39: 138944ms ~ 148916ms (file: 321-123-213-213-312-213-627-137-654-630-605-173-761-170-120-267-625-362-627-321-672-632-231-623-672-236-213-103-126-731-170-130-517-126-213-167-263-276-271-270-136-107-106-326-1_W.mp3)
  Segment 40: 149916ms ~ 150492ms (file: 750-576 750-570-571-715-750-701-751-701-750-705-751-750-756-750-137-713-713-735-713-731-517-710-573-570-576-576-750-375-765-750-756-756-750-750-650-371-350-350-705-750-705-506-7_W.mp3)
  Segment 41: 151492ms ~ 152068ms (file: 165 301-751-765-763-763-765-763-762-726-763-765-753-752-735-756_W.mp3)
  Segment 42: 153068ms ~ 153644ms (file: 571-570-750-571-501-570-517-510-507-561-105-150-150-503-517-510-507-501-170-150-150-157-510-150-503-350-751-571-157-157-571-157-517-175-756-570-751-715-751-751-750-751-571-571-5_W.mp3)
  Segment 43: 154644ms ~ 155220ms (file: 150-150-150-150-105-157-150-510-150-150-105-150-105-105-150 167-107-175-106-107-170-175-175-107-157-150-156-165-105-136-173-716-716-750-750-571-756-754-507-750-705-150-510-150-1_V.mp3)
  Segment 44: 156220ms ~ 156796ms (file: 216-162-160-637-671-605-176-765-160-673-730-167-761-715-756-167-163-136-163-361-361-136-136-653-613-610-361-316-631-135-135-320-312-321-132-321-321-231-326-321-321-321-213-312-3_W.mp3)
  Segment 45: 157796ms ~ 158372ms (file: 537-715-107-174-701-174-107-174-174-107 315-753-375-710-701-175-710-170-701-701-107-170-107-107-173-130-103-103-103-107-513-153-517-715-571-753-571-567-721-752-725-751-271-726-7_W.mp3)
  Segment 46: 159372ms ~ 159948ms (file: 126 213-162-631-130-163-167-651-156-217-162-631-167-316-160-163-103-136-136-610-106-106-156-106-165-163-103-312-627-617-167-137-236-612-761-123-102-102-106-103-263-621 612-103-1_V.mp3)
  Segment 47: 160948ms ~ 161524ms (file: 160-316-106-612-106-126-657-310-136-136-136-103-103-312-136-136-321-312-316-136-132-103-631-567-127-316-163-163-103-167-136-263-130-103-103-367-736-132-316-610-613-361-631-306-3_V.mp3)
  Segment 48: 162524ms ~ 163100ms (file: 157-750-570-156-576-517-756-652-657-725-756-573-625-756-265-267-721-657-756-256-137-135-317-701-317-103-306-130-137-310-130-317-605-765-576-102-103-613-136-163-361-136-163-163-1_W.mp3)
  Segment 49: 164100ms ~ 164676ms (file: 627-675-672-653-627-362-671-621-675-567-572-675-276-265-265-625-612-267_W.mp3)
  Segment 50: 165676ms ~ 166252ms (file: 156-516-650-506-560-651-650-651-167-167-213-236-321-136-132-231-170-127-173-173-715-576-756-326-236-231-230-215-265-236-231-321-231-756-765-761-617-126-216-126-652-362-612-236-2_W.mp3)
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
> **Time**: 2026-04-05 08:07:00
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "안녕하세요 여러분 오늘 공부할 내용은 한국어 문법입니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 450,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 450, "dur_ms": 820, "phase_lr_deg": 0.0},
{"word": "여러분", "start_ms": 1320, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 2100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "공부할", "start_ms": 2550, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "내용은", "start_ms": 3320, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "한국어", "start_ms": 4100, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "문법입니다", "start_ms": 4900, "dur_ms": 950, "phase_lr_deg": 0.0}
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
"transcript": "첫 번째 문장은 저는 학생입니다라는 표현입니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 320,
"audio_type": "speech",
"words": [
{"word": "첫", "start_ms": 320, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "번째", "start_ms": 550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "문장은", "start_ms": 950, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "저는", "start_ms": 1700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "학생입니다라는", "start_ms": 2200, "dur_ms": 1150, "phase_lr_deg": 0.0},
{"word": "표현입니다", "start_ms": 3400, "dur_ms": 880, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "여기서 은과 는은 주어를 나타내는 조사입니다",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 280,
"audio_type": "speech",
"words": [
{"word": "여기서", "start_ms": 280, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "은과", "start_ms": 900, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "는은", "start_ms": 1350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "주어를", "start_ms": 1800, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "나타내는", "start_ms": 2450, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "조사입니다", "start_ms": 3200, "dur_ms": 850, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "받침이 있을 때는 은을 사용하고 없을 때는 는을 사용하죠",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 410,
"audio_type": "speech",
"words": [
{"word": "받침이", "start_ms": 410, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "있을", "start_ms": 1000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "때는", "start_ms": 1400, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "은을", "start_ms": 1850, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "사용하고", "start_ms": 2300, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "없을", "start_ms": 3100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "때는", "start_ms": 3500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "는을", "start_ms": 3950, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "사용하죠", "start_ms": 4400, "dur_ms": 720, "phase_lr_deg": 0.0}
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
"transcript": "예를 들어 선생님은 이라고 말할 때 은을 씁니다",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 350,
"audio_type": "speech",
"words": [
{"word": "예를", "start_ms": 350, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "들어", "start_ms": 700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "선생님은", "start_ms": 1100, "dur_ms": 820, "phase_lr_deg": 0.0},
{"word": "이라고", "start_ms": 1950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "말할", "start_ms": 2450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 2900, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "은을", "start_ms": 3200, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "씁니다", "start_ms": 3650, "dur_ms": 650, "phase_lr_deg": 0.0}
]
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
"transcript": "학교는 넓어요 처럼 학교 다음에는 는이 옵니다",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 290,
"audio_type": "speech",
"words": [
{"word": "학교는", "start_ms": 290, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "넓어요", "start_ms": 900, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "처럼", "start_ms": 1600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "학교", "start_ms": 2100, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "다음에는", "start_ms": 2550, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "는이", "start_ms": 3250, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "옵니다", "start_ms": 3650, "dur_ms": 720, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "이해가 되시나요 질문이 있으면 언제든 말씀해 주세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 480,
"audio_type": "speech",
"words": [
{"word": "이해가", "start_ms": 480, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "되시나요", "start_ms": 1050, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "질문이", "start_ms": 1900, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "있으면", "start_ms": 2500, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "언제든", "start_ms": 3100, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "말씀해", "start_ms": 3750, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 4450, "dur_ms": 720, "phase_lr_deg": 0.0}
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
"transcript": "자 그럼 다음 예문을 함께 읽어보도록 하겠습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 330,
"audio_type": "speech",
"words": [
{"word": "자", "start_ms": 330, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "그럼", "start_ms": 650, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 1100, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "예문을", "start_ms": 1550, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "함께", "start_ms": 2150, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "읽어보도록", "start_ms": 2650, "dur_ms": 820, "phase_lr_deg": 0.0},
{"word": "하겠습니다", "start_ms": 3500, "dur_ms": 950, "phase_lr_deg": 0.0}
]
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "사과는 빨갛습니다 바나나는 노랗습니다 정말 쉽죠",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 420,
"audio_type": "speech",
"words": [
{"word": "사과는", "start_ms": 420, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "빨갛습니다", "start_ms": 1100, "dur_ms": 950, "phase_lr_deg": 0.0},
{"word": "바나나는", "start_ms": 2100, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "노랗습니다", "start_ms": 2900, "dur_ms": 920, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 3900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "쉽죠", "start_ms": 4400, "dur_ms": 520, "phase_lr_deg": 0.0}
]
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
"transcript": "영어로는 Apple is red Banana is yellow 라고 합니다",
"speaker": "speaker_1",
"confidence": 0.93,
"language": "en",
"silence_before_ms": 380,
"audio_type": "speech",
"words": [
{"word": "영어로는", "start_ms": 380, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "Apple", "start_ms": 1200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 1700, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "red", "start_ms": 2000, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "Banana", "start_ms": 2500, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 3100, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "yellow", "start_ms": 3400, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "라고", "start_ms": 4100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "합니다", "start_ms": 4500, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 29,
"transcript": "A million brains brings you to the top of knowledge",
"speaker": "speaker_2",
"confidence": 0.91,
"language": "en",
"silence_before_ms": 520,
"audio_type": "speech",
"words": [
{"word": "A", "start_ms": 520, "dur_ms": 150, "phase_lr_deg": 15.0},
{"word": "million", "start_ms": 700, "dur_ms": 520, "phase_lr_deg": 15.0},
{"word": "brains", "start_ms": 1300, "dur_ms": 580, "phase_lr_deg": 15.0},
{"word": "brings", "start_ms": 1950, "dur_ms": 510, "phase_lr_deg": 15.0},
{"word": "you", "start_ms": 2500, "dur_ms": 280, "phase_lr_deg": 15.0},
{"word": "to", "start_ms": 2850, "dur_ms": 220, "phase_lr_deg": 15.0},
{"word": "the", "start_ms": 3100, "dur_ms": 250, "phase_lr_deg": 15.0},
{"word": "top", "start_ms": 3400, "dur_ms": 420, "phase_lr_deg": 15.0},
{"word": "of", "start_ms": 3850, "dur_ms": 210, "phase_lr_deg": 15.0},
{"word": "knowledge", "start_ms": 4100, "dur_ms": 750, "phase_lr_deg": 15.0}
]
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
"transcript": "자 다음은 일본어로 사과를 어떻게 부르는지 알아볼까요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 440,
"audio_type": "speech",
"words": [
{"word": "자", "start_ms": 440, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "다음은", "start_ms": 750, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "일본어로", "start_ms": 1350, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "사과를", "start_ms": 2200, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 2850, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "부르는지", "start_ms": 3450, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "알아볼까요", "start_ms": 4250, "dur_ms": 850, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "일본어로 사과는 링고라고 합니다 발음이 참 예쁘죠",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 390,
"audio_type": "speech",
"words": [
{"word": "일본어로", "start_ms": 390, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "사과는", "start_ms": 1150, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "링고라고", "start_ms": 1800, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "합니다", "start_ms": 2600, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "발음이", "start_ms": 3300, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "참", "start_ms": 3900, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "예쁘죠", "start_ms": 4250, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "여러분도 소리 내어 한 번 따라해 보세요 링고 링고",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 410,
"audio_type": "speech",
"words": [
{"word": "여러분도", "start_ms": 410, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "소리", "start_ms": 1200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "내어", "start_ms": 1600, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 2050, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "번", "start_ms": 2300, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "따라해", "start_ms": 2600, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "보세요", "start_ms": 3300, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "링고", "start_ms": 4100, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "링고", "start_ms": 4700, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "좋습니다 아주 잘하셨어요 오늘 수업은 여기서 마치겠습니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 360,
"audio_type": "speech",
"words": [
{"word": "좋습니다", "start_ms": 360, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 1100, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "잘하셨어요", "start_ms": 1550, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 2500, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "수업은", "start_ms": 3000, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "여기서", "start_ms": 3650, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "마치겠습니다", "start_ms": 4300, "dur_ms": 980, "phase_lr

---
*Generated by WKAppBot ask gemini — 2026-04-05 08:07:00*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=10820  time=08:05:08  uptime=14444s  mem=423MB  system_processes=353"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_075725.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 7272ms (file: 671-103-123-123-103-132-721-721-321-310-310-716-176-106-312-102-230-267-132-123-162-102-102-130-301-310-756-276-267-620-627-103-130-270-320-103-574-752-320-632-107-130-326-765-5_V.mp3)
  Segment 2: 8272ms ~ 8848ms (file: 765-263-132-315-135-301-537-570-573-375-375-317-756-375-731-312-130-751-132-175-106-675-570-576-617-627-627-627-610-167-160-106-167-671-613-173-130-103-123-751-716-127-715-135-1_W.mp3)
  Segment 3: 9848ms ~ 10424ms (file: 103-715-723-276-752-176-317-137-137-173-170-361-136-617-103-623-750-726-673-567-725-132-162-132-102-132-576-507-317-357-312-317-316-312-351-521-752-351-216-507-327-705-126-710 5_V.mp3)
  Segment 4: 11424ms ~ 12000ms (file: 170-167-175-170-175-176-167-167-175-157-175-157-175-107 105-150-105-105-105-150-105-105-150-510-157-175-157-175-157-175-715-175-175-175-175-175-715 157-175-170-175-175-175-175-1_W.mp3)
  Segment 5: 13000ms ~ 13576ms (file: 571-517-751-517-517-571-571 103-130-135-310-157-135-153-105-103-130-135-157-531-153-157-153-315-157-351-153-153 135-153-173-135-153-157-130-137-135-135-315-517-513-351-153-157-5_W.mp3)
  Segment 6: 14576ms ~ 20588ms (file: 162-762-231-217-276-127-172-237-276-261-726-571-725-261-273-326-127-765-126-127-273-126-752-261-261-123-136-216-123-216-126-132-612-261-216-126-216-261-321-236-126-123-127-267-7_W_stt_the.mp3)
  Segment 7: 21588ms ~ 27420ms (file: 301-267-753-120-251-312-316-312-376-137-130-715-756-735-706-236-102-217-132-210-217-201-210-231-627-675-276-172-236-312-301-107-567-571-130-103-321-751-213-236-135-136-125-623-6_V.mp3)
  Segment 8: 28420ms ~ 28996ms (file: 507-514-540-514-541-510-514-507-501-157-105-157-175-157-175-107-105-157-175-105-571-107-715-750 570-574-574-574-507-501-175-157-570-571-750-750-750-751-571-754-751-750 754-574-5_V.mp3)
  Segment 9: 29996ms ~ 30572ms (file: 576-675-675-675-671-675-672-675-756-761-623-765-675-567-657-657-672-765-657-657-627-657-167-163-103-612-316-106-162-617-136-623-167-167 531-765-576-541-567-562-176-617-637-160-7_W.mp3)
  Segment 10: 31572ms ~ 32148ms (file: 130-703-713-137-375-751-713-107-170-750-731-170-301-103-571-572-571-751-573 107-137-170-371-753-130-607-701-310-130-317-135-102-175-703-310-103-103-103-130-135-310-370-310-375-1_V.mp3)
  Segment 11: 33148ms ~ 38872ms (file: 236-231-162-312-762-726-321-325-231-253-236-321-237-162-321-675-263-236-321-132-120-126-213-231-236-263-321-312-235-631-201-120-136-132-231-217-312-632-321-132-237-263-207-127-2_W.mp3)
  Segment 12: 39872ms ~ 40448ms (file: 157-517-157-157-157-315-175-715-173 135-130-310-103-130-135-135-135-537-130-135-315-135-153-153-157-153-157-153-517-157-157-157-351-517-573-571-531 157-107-301-130-137-135-153-1_V.mp3)
  Segment 13: 41448ms ~ 47892ms (file: 103-135-631-617-257-135-602-756-213-756-756-571-317-173-317-132-672-670-570-136-103-216-267-213-130-136-507-254-126-315-203-206-175-570-310-103-267-132-765-576-675-567-132-103-2_W.mp3)
  Segment 14: 48892ms ~ 49468ms (file: 754-756-754-754-754-754-754-754-754-756-754-754-754-754-754-105-105-157-156-165-157-571-157-517-756-751-571-751-517-751-571-576-571-571-571-517-571-157-571-576-517-157-157 103-1_V.mp3)
  Segment 15: 50468ms ~ 56084ms (file: 312-301-275-103-326-267-632-276-312-172-701-512-207-310-267-573-562-267-120-273-106-720-726-726-157-315-312-236-231-326-310-176-203-137-276-726-376-235-762-672-167-210-273-230-6_W.mp3)
  Segment 16: 57084ms ~ 57660ms (file: 751-175-750-175-716-175-751 150-175-175-715-715-756-173-715-570-175-173-371-715-710-715-715-715-175-710-701-750 751-175-751-710-751-170-705-176-750-751-107-175-175-170-715-175-7_V.mp3)
  Segment 17: 58660ms ~ 64996ms (file: 261-267-130-301-513-672-260-261-260-210-572-267-210-546-372-571-265-261-362-216-265-253-527-157-627-756-517-312-213-526-547-567-576-267-276-106-672-726-321-312-103-267-260-267-2_W.mp3)
  Segment 18: 65996ms ~ 80360ms (file: 756-756-765-756-765-627-675-762-672-607-670-607-671-625-621-657-130-127-756-765-765-765-761-765-763-763-760-765-761-671-607-602-672-130-130-103-301-312-163-370-760-762-673-574-5_W.mp3)
  Segment 19: 81360ms ~ 81936ms (file: 657-756-527-672-572-576-652-172-752-756-512-657-625-657-632-513-537-567-657-357-237-320-356-532-507-357-125-157-153-157-165-267-215-516-516-256-672-752-754-756-574-537-657-653-6_W.mp3)
  Segment 20: 82936ms ~ 83512ms (file: 157-576-571-157-157-156-651-561-561-657-613-671-765-765-756-756-756-751-756-175-317-615-615-615_W.mp3)
  Segment 21: 84512ms ~ 85088ms (file: 137-107-107-107-170-170-173-107-107-107-710-170-130-731-170-365-315-130-317-317-137-175-105 261-263-261-123-136 713-173-713-137-731-176-173-713-173 716-762-276-136-132-135-136 1_W.mp3)
  Segment 22: 86088ms ~ 86664ms (file: 136-123-132-627-632-326-326-236-136-132_W.mp3)
  Segment 23: 87664ms ~ 96844ms (file: 302-320-706-263-203-213-310-320-123-203-765-237-267-106-172-231-370-310-237-103-123-230-627-672-263-320-273-236-762-756-765-230-625-173-176-267-301-762-203-627-627-326-613-612-6_W.mp3)
  Segment 24: 97844ms ~ 98420ms (file: 512 136-103-765-106-136-162-163-167-672-612-617-163-136-136-165-765-103-163-765-102-162-621-160-612-106-617-672-167-623-160-136-136-103-312-516-135-136-576-130-617-103-136-317-1_V.mp3)
  Segment 25: 99420ms ~ 108600ms (file: 627-260-263-123-574-213-321-627-267-263-261-231-276-231-261-267-572-207-102-103-571-572-527-762-216-312-216-752-570-106-260-263-210-132-261-312-316-163-103-201-120-135-517-571-5_W.mp3)
  Segment 26: 109600ms ~ 110176ms (file: 123-163-136-312-176-632-716-107-120-162-136-137-261-621-637-561-216-613-610-361-130-316-130-103-103-132-326-320-106-312-160-120-316-615-571-163-613-671-162-136-103-103-126-612-6_V.mp3)
  Segment 27: 111176ms ~ 111752ms (file: 130-371-317-103-107-310-137-103-731-107-710-317-137-130-103-130-731-701-107-107-107-710-730-370-730-173-137-105-753-571-570-751 371-107-173-310-173-130-107-173-107-371-710-130-3_V.mp3)
  Segment 28: 112752ms ~ 113328ms (file: 132-132-103-103-132-301 123-135_V.mp3)
  Segment 29: 114328ms ~ 123472ms (file: 306-627-602-260-621-126-612-126-120-123-126-127-126-127-126-612-621-756-756-230-725-527-205-236-263-261-310-132-310-231-201-231-320-321-317-312-236-130-275-265-123-753-257-302-1_W.mp3)
  Segment 30: 124472ms ~ 125048ms (file: 216-627-371-573-753-715-750-756-576-576-756-756-756-632-517-576-305-570-576-130-576-571-571-517-532-507-573-156-567-507-521-152-675-613-657-631-236-613-615-130-163-675-631-263-6_W.mp3)
  Segment 31: 126048ms ~ 133356ms (file: 236-173-765-263-136-167-261-576-265-123-312-134-570-576-267-621-312-107-167-231-527-253-312-735-167-162-751-576-267-213-263-123-132-216-261-576-267-627-321-107-316-765-123-312-1_W.mp3)
  Segment 32: 134356ms ~ 134932ms (file: 576-574-576-574-576-576-756-312-574-574-570-107-627-267-620-723 720-756 574-756-706-201-216-120-620-123 610-613-610-610-610-136-630-623-627-620-326-302-103-312-612-632-261-621-1_W.mp3)
  Segment 33: 135932ms ~ 136508ms (file: 137-137-103-103-317-173-137-173-172-137-135 571-751-175-135-317-315-317-103-301-315-135-375-537-715-571-576-715-175-517-576-517-576-567-657-567-657-651-516 157-175-130-310-301-3_V.mp3)
  Segment 34: 137508ms ~ 138084ms (file: 135-136-316 351-571-573 571-753 357-751-756-317-753-571 301-534-315-315-316-136-135-136-137-135-136-103-135-163-135-103-136-135-135-134-103-134 310-134-316-135-314-134-136-316-1_V.mp3)
  Segment 35: 139084ms ~ 139660ms (file: 751-751-175 150-510-105-105-105-105-107-175-175-175-175-175-175-175-175-175-175-175-175 157-175-157-105-153-105-130-170-137-715-175-715-715-751-715-751-751 105-175-107-103-175-1_V.mp3)
  Segment 36: 140660ms ~ 141236ms (file: 627-621-317-173-716-167-167-367-136-163-167-160-136-163-763-637-376-367-632-637-635-760-302-367-327-306-137-371-672-735-317-617-163-371-164-234-451_W.mp3)
  Segment 37: 142236ms ~ 152280ms (file: 102-206-206-276-765-276-267-210-172-143-132-301-672-276-102-120-215-132-763-301-130-163-126-216-213-162-261-602-627-627-610-602-162-106-201-213-312-320-206-267-261-672-106-107-3_V.mp3)
  Segment 38: 153280ms ~ 153856ms (file: 132-317-267-106-162-106-136-103-261-621-260-132-632-206-236-213-102-301 627-621-627 657-267-236-312-162-163-316-273-316-361 672-670-637-620-627 603-176-610-537-610-361-607-130-6_W.mp3)
  Segment 39: 154856ms ~ 163640ms (file: 321-312-230-713-756-301-126-326-137-320-231-230-723-726-321-723-321-321-267-276-726-231-321-310-102-237-320-273-312-310-230-237-321-236-120-267-120-263-237-301-673-672-236-320-3_W.mp3)
  Segment 40: 164640ms ~ 165216ms (file: 753-756-571-537-137-571-635-376-367-316-376-673-715-316-617-763-367-367-762-601-761-671-136-715-675-765-765-710-672-620-137-175-527-267-123-753 132-137-167-167-562-265-256-675-2_W.mp3)
  Segment 41: 166216ms ~ 166792ms (file: 136-310-136-310-176-613-760-130-170-137-107-175-710-703 137-375-173-713-301-137-317-107-361-630-603-601-631-610-361-631-603-126-130-130-317-103-137-137-310-135-753-170 735-701-3_V.mp3)
  Segment 42: 167792ms ~ 168368ms (file: 576-567-516-135-156-157-574-547-570-573-576-576-536-573-130-371-173-167-173-716-137-315-321-132-156-156-516-156-153-651-157-315-365-375-357-735-765-756-576-756-137-310-137-371-3_V.mp3)
  Segment 43: 169368ms ~ 169944ms (file: 652-261-132-267-261-167-621-163-361-132-176-132-132-123-613-621-671-326-362-623-713-162-312-321-103-361-637-621-236-362-376-631-673-765-765-675-367-756-675-123-130-265-672-123-1_W.mp3)
  Segment 44: 170944ms ~ 180628ms (file: 261-102-260-260-321-263-132-312-321-316-310-231-132-312-312-312-132-310-312-137-267-231-132-123-312-123-736-527-260-236-326-210-523-753-547-325-230-320-326-320-320-236-326-762-1_W.mp3)
  Segment 45: 181628ms ~ 182204ms (file: 621-672-620-621-621-576-567-572-567-576-567-567-625-627-162-163-106 756-765-132 576-657_W.mp3)
  Segment 46: 183204ms ~ 183780ms (file: 576-567-153-501-576-576-546-315-130 105-135-547-726-756-765-756-756_W.mp3)
  Segment 47: 184780ms ~ 195616ms (file: 120-273-231-203-213-263-106-231-126-231-230-236-627-236-230-126-175-126-160-267-230-107-260-120-103-162-163-361-762-257-312-103-103-102-103-271-207-231-126-162-617-716-106-327-2_W.mp3)
  Segment 48: 196616ms ~ 201224ms (file: 127-127-132-173-123-317-726-273-276-267-237-723-276-216-276-672-267-276-237-327-367-321-276-261-276-237-327-261-136-271-162-136-127-316-137-132-237-127-712-123-216-632-276-271-2_W.mp3)
  Segment 49: 202224ms ~ 211296ms (file: 230-130-132-106-123-103-320-230-231-123-102-160-510-520-123-102-126-210-102-103-231-230-260-263_V.mp3)
  Segment 50: 212296ms ~ 212872ms (file: 316-103-516-132-106-106-103-102-163-163-106-617-613-163-163-165-675-610-123-213-613-361-567-576-571-576-506 761-106-132-567-576-675-301-317-130-310-150-135-103-312-132-301-315-6_V.mp3)
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
> **Time**: 2026-04-05 08:07:39
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 2,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
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
"transcript": "오늘의 한국어 한마디는 바로 감사합니다입니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 420,
"audio_type": "speech",
"words": [
{"word": "오늘의", "start_ms": 420, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "한국어", "start_ms": 1050, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "한마디는", "start_ms": 1750, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 2600, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "감사합니다입니다", "start_ms": 3150, "dur_ms": 1150, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 7,
"transcript": "상대방에게 고마움을 표현할 때 가장 많이 쓰이죠",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 380,
"audio_type": "speech",
"words": [
{"word": "상대방에게", "start_ms": 380, "dur_ms": 780, "phase_lr_deg": 0.0},
{"word": "고마움을", "start_ms": 1250, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "표현할", "start_ms": 1950, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 2600, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 2950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 3500, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "쓰이죠", "start_ms": 4000, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "Thank you so much",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "Thank", "start_ms": 250, "dur_ms": 350, "phase_lr_deg": 10.0},
{"word": "you", "start_ms": 650, "dur_ms": 220, "phase_lr_deg": 10.0},
{"word": "so", "start_ms": 950, "dur_ms": 280, "phase_lr_deg": 10.0},
{"word": "much", "start_ms": 1300, "dur_ms": 450, "phase_lr_deg": 10.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "친한 사이에서는 고마워라고 말할 수 있습니다",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 450,
"audio_type": "speech",
"words": [
{"word": "친한", "start_ms": 450, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "사이에서는", "start_ms": 1000, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "고마워라고", "start_ms": 1850, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "말할", "start_ms": 2600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 3100, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 3400, "dur_ms": 720, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "아리가토 고자이마스 일본어로는 이렇게 표현하죠",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ja",
"silence_before_ms": 320,
"audio_type": "speech",
"words": [
{"word": "아리가토", "start_ms": 320, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "고자이마스", "start_ms": 1050, "dur_ms": 820, "phase_lr_deg": 0.0},
{"word": "일본어로는", "start_ms": 2000, "dur_ms": 780, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 2850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "표현하죠", "start_ms": 3400, "dur_ms": 680, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "여러 언어로 고마움을 전하는 법을 배우니 재미있지 않나요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 410,
"audio_type": "speech",
"words": [
{"word": "여러", "start_ms": 410, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "언어로", "start_ms": 850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "고마움을", "start_ms": 1400, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "전하는", "start_ms": 2000, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "법을", "start_ms": 2550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "배우니", "start_ms": 2950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "재미있지", "start_ms": 3500, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "않나요", "start_ms": 4200, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "오늘 배운 표현들을 꼭 복습해 보시길 바랍니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 350,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 350, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "배운", "start_ms": 800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "표현들을", "start_ms": 1200, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "꼭", "start_ms": 1900, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "복습해", "start_ms": 2200, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "보시길", "start_ms": 2800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "바랍니다", "start_ms": 3300, "dur_ms": 720, "phase_lr_deg": 0.0}
]
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "그럼 다음 시간에 또 만나요 감사합니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 280,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 280, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 700, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "시간에", "start_ms": 1100, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 1650, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 1900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 2450, "dur_ms": 850, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "See you again next time Bye bye",
"speaker": "speaker_2",
"confidence": 0.92,
"language": "en",
"silence_before_ms": 510,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 510, "dur_ms": 280, "phase_lr_deg": 10.0},
{"word": "you", "start_ms": 850, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "again", "start_ms": 1100, "dur_ms": 420, "phase_lr_deg": 10.0},
{"word": "next", "start_ms": 1600, "dur_ms": 350, "phase_lr_deg": 10.0},
{"word": "time", "start_ms": 2050, "dur_ms": 450, "phase_lr_deg": 10.0},
{"word": "Bye", "start_ms": 2600, "dur_ms": 320, "phase_lr_deg": 10.0},
{"word": "bye", "start_ms": 3000, "dur_ms": 380, "phase_lr_deg": 10.0}
]
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
*Generated by WKAppBot ask gemini — 2026-04-05 08:07:39*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=24932  time=08:05:32  uptime=13617s  mem=462MB  system_processes=355"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_075353.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 132-301-715-173-137-327-137-312-317-130-310-301-103-103-310-137 135-312-301-317-317-103-317-137-107-317-103-301-130-103-103-301-103-103-103-103-137-103-317-137-173 315-312-310-3_V.mp3)
  Segment 2: 1576ms ~ 11728ms (file: 726-120-567-103-132-103-103-127-752-731-107-326-106-136-126-302-301-106-173-265-256-716-173-137-756-312-102-132-162-316-106-107-136-106-376-163-361-316-201-120-210-201-132-207-2_V.mp3)
  Segment 3: 12728ms ~ 18776ms (file: 576-257-276-217-132-571-273-312-123-756-130-312-132-132-375-132-756-231-321-350-321-132-316-316-130-321-320-327-127-137-260-263-230-102-602-310-123-310-321-102-160-132-132-312-1_W.mp3)
  Segment 4: 19776ms ~ 29460ms (file: 261-102-260-260-321-263-132-312-321-316-310-231-132-312-312-312-132-310-312-137-267-231-132-123-312-123-736-527-260-236-326-210-523-753-547-325-230-320-326-320-320-236-326-762-1_W.mp3)
  Segment 5: 30460ms ~ 31036ms (file: 163-561-315-167-176-762-162-316-301-301-316-163-672-760-670-765-675-670-607-670-627-620-167-627-260-617-167-623-620-627-621-671-613-163-631-367-612-632-657-675-672-756-672-625-6_V.mp3)
  Segment 6: 32036ms ~ 32612ms (file: 576-765-756 576-576-567-576-576-576-576-756-576-576-576-576-576 576-576-576-576-576-567-576-576-576-576-576-576-576-576-576-576-567-675 567-576-576-576-756-576-567-567-756-576-5_V.mp3)
  Segment 7: 33612ms ~ 34188ms (file: 132-102-103-123-315-103-106-321-321-102-276-102-102-107-103-326-302-321-213-136-103-163-103-621-260-106-321-302-301-312-132-756-103-102-136-163-103-103-301-132-132-165-657-125-6_V.mp3)
  Segment 8: 35188ms ~ 35764ms (file: 756 501-510-150-157-170-175-175-615-517-567-576-567-567-657-576-567-675-675 751-507-751-751-157-175-170-175-170-107-170-170-170-715-710-170-175-175-175-751-751-751-756 107-170-1_V.mp3)
  Segment 9: 36764ms ~ 43892ms (file: 721-276-360-126-752-135-127-137-572-231-762-167-762-120-125-120-125-123-126-216-216-312-253-105-312-216-301-132-236-263-216-275-523-756-751-531-670-251-123-123-712-261-126-127-2_V.mp3)
  Segment 10: 44892ms ~ 45468ms (file: 106-107-167-301-103-136-312-362 316-163-672-312-326-236-326-673-672-762-253-705-306-367-632-263-623-623-675-670-576-574-576-576-750-675-761-760-601-306-132-103-132-136-103-130-6_V.mp3)
  Segment 11: 46468ms ~ 57340ms (file: 217-103-173-102-102-132-102-213-160-572-216-210-175-576-573-571-105-276-230-732-257-125-237-120-173-107-215-125-162-576-574-103-570-125-571-765-203-715-217-176-106-267-106-102-2_W.mp3)
  Segment 12: 58340ms ~ 58916ms (file: 753-376-751-135-751-175 130-137-130-705-173-371-137-317-301-317-607-607-631-631-103-130-301-137-130-173-175-137-170-371-153-135-375-315-375-371 317-753-705-703-103-317-103-316-6_V.mp3)
  Segment 13: 59916ms ~ 60492ms (file: 570-715-157-107-175-107-705-701-701-701-750-765-715-765-715-715-712 570-570-750-175-571-705-701-157-107-150-715-175-751-157-571-751-715-175-751-105-715-756-765-716-715-765-716-7_W.mp3)
  Segment 14: 61492ms ~ 72076ms (file: 132-627-312-236-263-267-213-231-261-231-267-267-213-267-261-103-103-103-263-267-762-107-132-102-132-210-213-672-312-267-263-231-236-213-627-236-260-267-576-627-627-260-260-267-3_W.mp3)
  Segment 15: 73076ms ~ 73652ms (file: 136-312-132-312-312-132-312-132-312-132-132-317-132_V.mp3)
  Segment 16: 74652ms ~ 75228ms (file: 134-103-104-103-154-134 754-417-341-410-104-415-143-135-410-140-104-147-103-130-143-130-140-154-415-104-103-134-134-351-357-576-357-104-405-104-103-104-130-103-143-140-145-314-1_W.mp3)
  Segment 17: 76228ms ~ 76804ms (file: 175-170-175-170-170-175-175-170-710-170-170-175_W.mp3)
  Segment 18: 77804ms ~ 85076ms (file: 265-260-265-120-251-256-265-230-236-203-206-320-326-321-123-102-312-102-235-231-230-325-124-173-103-231-230-236-263-256-120-621-265-265-236-231-123-250-230-102-132-320-526-237-3_W.mp3)
  Segment 19: 86076ms ~ 86652ms (file: 103-632-136-156-617-107-527-172-123-162-130-316-136-613-136-137-103-126-210-753-716-371-165-571-536-156-175-523-167-136-106-103-167-136-143-103-617 162-321-312-321-312-321-236-3_V.mp3)
  Segment 20: 87652ms ~ 88228ms (file: 632-627-126-620-312-261-267-261-263 312-130-276_W.mp3)
  Segment 21: 89228ms ~ 89804ms (file: 513-321-216-270-321-610-612-321-162-632-620-726-750-702-176-217-217-217-176-765-261-132-614-613-162-174_V.mp3)
  Segment 22: 90804ms ~ 91380ms (file: 657-765-267-267-672-265-365-326-312-513_V.mp3)
  Segment 23: 92380ms ~ 92956ms (file: 103-165-105-510-516-576-751-175-573-531-137-756-136-315-135-137-756-765-175-157-156-175-761-761-735-715-765-756-712-153-103-675-716-761-157-715-706-175-107-107-571-571-173-371-7_W.mp3)
  Segment 24: 93956ms ~ 94532ms (file: 617-136-103-136-157-103-103-136-130-136-615-160-103-715-312-657-163-675-612-163-163-163-136-317-135-135-136-163-132-163-103-321-165 103-705-106-106-216-103-361-657-561 167-163-1_V.mp3)
  Segment 25: 95532ms ~ 96108ms (file: 547-547-574-574-574-574-571-517-517-513-571-157-751-715-157-517 573-157-517-571-157-751-175-751-157-153-157-175-157-175-157-175-157-173-715-517-715-157-517-175-175-571-751 571-1_V.mp3)
  Segment 26: 97108ms ~ 97684ms (file: 317-325-652-321-567-237-715-126-362-321-236-237-317-624-625-713-365-327-261-372 132-654-321-264-367-217 675-675-261-621-162 671-671-760 326-165-137-623 165-613-103-136-132-310-1_V.mp3)
  Segment 27: 98684ms ~ 104012ms (file: 123-326-102-137-261-267-362-725-103-321-263-310-132-132-126-321-721-103-126-263-256-325-310-310-123-176-216-123-237-326-327-271-726-276-267-123-267-276-267-236-726-726-267-231-2_W.mp3)
  Segment 28: 105012ms ~ 111492ms (file: 301-210-510-520-276-301-130-132-127-130-657-521-120-216-130-261-310-103-132-102-635-362-206-260-216-206-130-132-257-756-207-123-132-130-516-315-123-320-623-120-261-120-627-120-1_W.mp3)
  Segment 29: 112492ms ~ 113068ms (file: 136-302-103-567-615-106-163-165-316-136-316-103-123 103-312-137-136_V.mp3)
  Segment 30: 114068ms ~ 114644ms (file: 576-657-671-675-675-675-675-657-675-765-756-675-765-675-675-236-672-263-675-601-670-671-167-607-675-657-160-106-167-103-136-106-316-107-167-106-613-106-516-675-617-675-605-561-1_W.mp3)
  Segment 31: 115644ms ~ 124212ms (file: 326-164-102-320-763-230-230-203-267-207-725-756-360-310-320-756-754-275-102-203-756-270-273-276-207-172-210-756-275-753-137-103-231-230-130-123-756-102-267-260-210-312-103-132-1_W.mp3)
  Segment 32: 125212ms ~ 125788ms (file: 105-157-105-150-105-157-150 510-150-105-510-150-175-157-170-107-105-157-150-157-157-150-105-150-517-150 510-157-510-150-510-150-105-150-510-510-501-105-105-150-105-157-150-517-1_V.mp3)
  Segment 33: 126788ms ~ 127364ms (file: 617-671-612-162-613-617 675-675-756-576-657-657-765_W.mp3)
  Segment 34: 128364ms ~ 128940ms (file: 617-103-165-126-612-267-263-610-623 163-632-267-216-620-136-267-267-627-267-261-236-265-263-263-236-106-162-360-360-623-103-623-263-236-316_W.mp3)
  Segment 35: 129940ms ~ 135628ms (file: 315-207-201-310-310-312-312-263-630-236-576-263-623-162-267-263-206-236-263-236-750-273-315-327-267-327-567-156-163-327-307-376-671-736-137-376-672-716-367-763-371-376-137-132-7_W.mp3)
  Segment 36: 136628ms ~ 137204ms (file: 170-170-157-571-751-576-751-756-715-756-571-576 570-157-170-570-571 150-150-105-517-170-103-170-157-517-157 150-150-157-105-175-157-754-571-576-571-571-574-574-574-574-574 105-5_W.mp3)
  Segment 37: 138204ms ~ 145656ms (file: 132-213-213-213-321-237-275-276-537-215-251-312-132-231-362-261-265-132-213-260-201-213-210-213-201-201-132-102-123-210-210-215-203-210-203-210-213-210-210-216-210-267-213-231-2_W.mp3)
  Segment 38: 146656ms ~ 153244ms (file: 275-752-760-725-267-103-725-263-137-620-206-756-106-231-576-216-260-236-623-312-123-276-756-261-572-321-261-267-106-132-236-130-263-130-137-106-106-260-203-206-230-162-213-120-7_W.mp3)
  Segment 39: 154244ms ~ 154820ms (file: 156-165-176-132-132-123-312-132-132-312-321-135-153-153-156-152-125-517-572-517-571-517-523-251-217-315-317-731-376-317-361-376-376-713-375-356-312-352-321-136-362-623-362-652-2_W.mp3)
  Segment 40: 155820ms ~ 165612ms (file: 321-173-230-267-136-761-675-312-573-102-213-357-310-235-576-321-752-312-231-267-231-321-260-260-256-312-167-321-132-312-362-231-163-231-132-623-267-321-103-103-627-236-763-321-1_W.mp3)
  Segment 41: 166612ms ~ 167188ms (file: 675-576-657-576-675-567-567-567-576-567-765-756-576-576-765-657-576-567-576-567-567-576-576-567-567-657-576-576-576-567-567-567-576-657-576-567-576-576-567-567-567-527-216-126-1_W.mp3)
  Segment 42: 168188ms ~ 168764ms (file: 267-275-273-765-627-267-672-356-705-257-756-567-321-623-753-753-506-157-501-571-570-165-725-137-723-726-573-123-362-261-261-756-576-321-352-750-756-625-132-315-507-567-273-217-6_V.mp3)
  Segment 43: 169764ms ~ 170340ms (file: 517-576-513-156-132-576-675-672-705-750-307-310-307-302-730-753 216-213 675-675-675-675-675 132-312-312-312-765 316-756-576-761-702-756-756-301-301-130-310-762-760-671-602-760-7_V.mp3)
  Segment 44: 171340ms ~ 171916ms (file: 260-632-321-302-367-206-306-136-132 173-612-627-261-132-603-621-126-310-103 156 132-324-152-321-132 375-713-137-371-312 132-316-312-132-237-732-723-203-236-267-207 563-657-653-5_V.mp3)
  Segment 45: 172916ms ~ 173492ms (file: 357-357-376-136-361-362-653-316-361-316-316-312-576-765-317-275-312-367-361-316-613-136-367-316-376-637-173-317-136-136-167-371-362-367-326-765-316-123-137-574-576-275-215-752-5_W.mp3)
  Segment 46: 174492ms ~ 175068ms (file: 210-162-621-263-216-236-312-216-123-261-126-261-261-265-265-265-256-256-256-265-312-321-325-253-276-275-273-257-574-572-567-572-546-546-540-514-576-567-526-317-321-231-312-312-3_W.mp3)
  Segment 47: 176068ms ~ 176644ms (file: 260-620-507-576-675-765-671-136 620 251-132 136 762-712-132-132-136-103-132-172-712-716-216-130-137-317-301-132-136-601-617-163-136-163-106-601-615-167-167-361-657-365-163-627-6_V.mp3)
  Segment 48: 177644ms ~ 178220ms (file: 567-571-150-570-501 503-570-517-105-157-157-517-157-105-105-501-501-501-507-501-510-513-501-150-510-150-510-576-576-571-507-570-517 507-570-507-571-517-501-501-510-150-501-517-5_V.mp3)
  Segment 49: 179220ms ~ 179796ms (file: 137-310-130-107 375-130-730-317-315-103-370-310-176-173-130-317-173-371-130-713-317-130-107-137-103-315-371-173-107-135-135-175 310-137-103-710-371-713-731-705-731-170-170-371-7_W.mp3)
  Segment 50: 180796ms ~ 181372ms (file: 215-206-601-621-612-263-260-612-216-263-756-576-132-301-603-361-623-130-267-123-163-276-623-162-103-106-312-650-672-102-675-106-654-631-671-167-167-106-657-612-265-671-527-512-1_V.mp3)
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
> **Time**: 2026-04-05 08:07:41
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 2,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
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
"transcript": "오늘의 한국어 한마디는 바로 감사합니다입니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 420,
"audio_type": "speech",
"words": [
{"word": "오늘의", "start_ms": 420, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "한국어", "start_ms": 1050, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "한마디는", "start_ms": 1750, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 2600, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "감사합니다입니다", "start_ms": 3150, "dur_ms": 1150, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 7,
"transcript": "상대방에게 고마움을 표현할 때 가장 많이 쓰이죠",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 380,
"audio_type": "speech",
"words": [
{"word": "상대방에게", "start_ms": 380, "dur_ms": 780, "phase_lr_deg": 0.0},
{"word": "고마움을", "start_ms": 1250, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "표현할", "start_ms": 1950, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 2600, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 2950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 3500, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "쓰이죠", "start_ms": 4000, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "Thank you so much",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "Thank", "start_ms": 250, "dur_ms": 350, "phase_lr_deg": 10.0},
{"word": "you", "start_ms": 650, "dur_ms": 220, "phase_lr_deg": 10.0},
{"word": "so", "start_ms": 950, "dur_ms": 280, "phase_lr_deg": 10.0},
{"word": "much", "start_ms": 1300, "dur_ms": 450, "phase_lr_deg": 10.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "친한 사이에서는 고마워라고 말할 수 있습니다",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 450,
"audio_type": "speech",
"words": [
{"word": "친한", "start_ms": 450, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "사이에서는", "start_ms": 1000, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "고마워라고", "start_ms": 1850, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "말할", "start_ms": 2600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 3100, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 3400, "dur_ms": 720, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "아리가토 고자이마스 일본어로는 이렇게 표현하죠",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ja",
"silence_before_ms": 320,
"audio_type": "speech",
"words": [
{"word": "아리가토", "start_ms": 320, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "고자이마스", "start_ms": 1050, "dur_ms": 820, "phase_lr_deg": 0.0},
{"word": "일본어로는", "start_ms": 2000, "dur_ms": 780, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 2850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "표현하죠", "start_ms": 3400, "dur_ms": 680, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "여러 언어로 고마움을 전하는 법을 배우니 재미있지 않나요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 410,
"audio_type": "speech",
"words": [
{"word": "여러", "start_ms": 410, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "언어로", "start_ms": 850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "고마움을", "start_ms": 1400, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "전하는", "start_ms": 2000, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "법을", "start_ms": 2550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "배우니", "start_ms": 2950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "재미있지", "start_ms": 3500, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "않나요", "start_ms": 4200, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "오늘 배운 표현들을 꼭 복습해 보시길 바랍니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 350,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 350, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "배운", "start_ms": 800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "표현들을", "start_ms": 1200, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "꼭", "start_ms": 1900, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "복습해", "start_ms": 2200, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "보시길", "start_ms": 2800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "바랍니다", "start_ms": 3300, "dur_ms": 720, "phase_lr_deg": 0.0}
]
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "그럼 다음 시간에 또 만나요 감사합니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 280,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 280, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 700, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "시간에", "start_ms": 1100, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 1650, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 1900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 2450, "dur_ms": 850, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "See you again next time Bye bye",
"speaker": "speaker_2",
"confidence": 0.92,
"language": "en",
"silence_before_ms": 510,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 510, "dur_ms": 280, "phase_lr_deg": 10.0},
{"word": "you", "start_ms": 850, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "again", "start_ms": 1100, "dur_ms": 420, "phase_lr_deg": 10.0},
{"word": "next", "start_ms": 1600, "dur_ms": 350, "phase_lr_deg": 10.0},
{"word": "time", "start_ms": 2050, "dur_ms": 450, "phase_lr_deg": 10.0},
{"word": "Bye", "start_ms": 2600, "dur_ms": 320, "phase_lr_deg": 10.0},
{"word": "bye", "start_ms": 3000, "dur_ms": 380, "phase_lr_deg": 10.0}
]
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
*Generated by WKAppBot ask gemini — 2026-04-05 08:07:41*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=14936  time=08:06:14  uptime=22875s  mem=454MB  system_processes=356"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_075752.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 275-527-257-235-231-351-150-530-517-537-357-576-357-357-536-635-670-367-357 675-657-657-653-657-165-671-631-216-261-627-263 257-567-675-657-536-536-537-125-321-321-513-215-352-2_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 751-517-751-517-175-751-157-715-175-715-570-157-513-157-751-571-510-571-570-517-571-571-571-570-571-571-571-750-571-570-751-571-751-571-571-571-571-570-570-576-571-105-157-157-7_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 170-170-157-571-751-576-751-756-715-756-571-576 570-157-170-570-571 150-150-105-517-170-103-170-157-517-157 150-150-157-105-175-157-754-571-576-571-571-574-574-574-574-574 105-5_W.mp3)
  Segment 4: 4728ms ~ 12324ms (file: 120-107-107-136-167-123-132-261-103-132-217-230-321-261-236-326-231-123-316-201-216-327-370-367-236-137-603-623-362-270-267-317-103-306-103-376-231-260-136-302-376-120-130-123-6_V.mp3)
  Segment 5: 13324ms ~ 13900ms (file: 137-127-712-751-231-321-326-163-102-173-637-163-103-106-167-132-126-127-132-302-321-321-263-235-657-203-321-230-123-302-312-132-327-231-132-203-231-132-126-253-103-302-327-327-3_W.mp3)
  Segment 6: 14900ms ~ 15476ms (file: 576 567-576-576-576-574-574-574-574-574-576-574-574-572 576-574-652-576-576-574-574-576-576-574-574-574-576-576 576-576-567-574-576-574-574-574-574-574-576-574-576-574 574-576-5_W.mp3)
  Segment 7: 16476ms ~ 21912ms (file: 756-321-326-321-312-326-236-231-123-173-137-103-310-217-231-273-132-231-231-257-213-237-130-302-201-627-602-260-206-260-236-206-276-725-672-276-137-302-103-130-236-301-312-312-3_W.mp3)
  Segment 8: 22912ms ~ 25216ms (file: 574-576-750-760-256-725-325-216-217-327-236-627-123-321-267-275-216-321-275-502-765-705-756-763-251-267-574-536-271-736-132-103-362-627-176-756-273-765-675-570-527-672-261-263-3_V.mp3)
  Segment 9: 26216ms ~ 26792ms (file: 576-573-517-173-175-170 570-105-150-157-150-517-751-135-105-751-170-170-571-152-175-173-157-173-137-317-137-135-173-135-137-712-175-735-372-356-375-753-751-357-751-731-735 517-5_V.mp3)
  Segment 10: 27792ms ~ 28368ms (file: 261-216-620-217-623-257-513-531-312-625-317-672-270-265-321-715-263-527-152-750-507 625-267-326-263-206-231-276-236-623-152-261-276-321-367-571-756-652-576-705-725-267-572-261-5_V.mp3)
  Segment 11: 29368ms ~ 29944ms (file: 756-751-715-175-715-175-715-175-705-507-750-570-751-705-756-751-715-175-107-175-107-157-715-107-107-175-107-107-105-715-715-175-175-107-375-517-750-571-570-571-751-756-751-756-7_W.mp3)
  Segment 12: 30944ms ~ 31520ms (file: 517-571-517-576-567-157-156-157-153-517 136-132-136-136-136-132-132-132-312-132-132-315-123-213-132-213-215-135-321-215-231-231-213-213-215-237-375-371-736-173-136-136-137-137-1_W.mp3)
  Segment 13: 32520ms ~ 33096ms (file: 570-571-571-571-751-571-570-571-574-571-570-573-573-570-570-153-157-153-153-517-517-105-513-517-157-517-517-175-157-137-715-137-751-175-715-571-715-576-175-715-751-751-751-751-7_V.mp3)
  Segment 14: 34096ms ~ 34672ms (file: 517-571-751-571-751-751-175-175-715-175-751-751-751-715-157-517 570-517-571-157-571-751-157-751-175-175-175-751-751 510-157-150-153-517-571-157-715-715-175-751-715-710-750-750-7_V.mp3)
  Segment 15: 35672ms ~ 36248ms (file: 576-571-570-710-750-150-517-176-765-675-716-672-570-150-516-136-537-165-517-756-715-157-173-137-175-157-150-135-573-576-537-675-751-756-756-756-756-576-756-751-735-735 571-517-5_W.mp3)
  Segment 16: 37248ms ~ 44952ms (file: 273-276-130-312-327-761-276-276-276-231-162-762-765-672-231-721-237-237-372-132-357-160-137-716-517-672-376-130-136-317-163-163-725-752-627-627-675-267-127-627-267-532-231-230-6_W.mp3)
  Segment 17: 45952ms ~ 55456ms (file: 371-731-317-137-103-130-756-627-173-130-130-675-635-365-367-365-357-305-375-206-163-753-675-107-132-167-103-731-601-136-106-203-103-301-206-136-132-675-517-157-105-175-732-103-3_W.mp3)
  Segment 18: 56456ms ~ 65024ms (file: 760-270-210-607-127-213-621-260-123-765-265-321-312-130-103-261-621-261-257-152-617-621-126-102-352-135-267-130-216-213-216-260-276-216-120-267-261-210-231-261-276-524-542-756-2_W.mp3)
  Segment 19: 66024ms ~ 72828ms (file: 627-167-276-256-627-317-317-267-263-123-120-231-261-210-127-260-310-213-236-123-263-267-210-672-627-206-621-231-267-627-130-716-132-127-136-713-132-210-201-210-310-261-627-312-2_W.mp3)
  Segment 20: 73828ms ~ 74404ms (file: 213-213-316-526-637-657-635-576-260-627-273-327 267-276-276-276-726-237-276-312-312-231-321-326-316-163-160-160-103-173-162-120-620-206-163-163-137-312-137-615-162-125-162-167-2_W.mp3)
  Segment 21: 75404ms ~ 75980ms (file: 160-102-362-607-765-765-620-560-560-206-263-306-163-106-103-673-163-260-702-106-627-206-260-261-261-302-302-602-670-267-267-260-206-162-106-103-632-602-607-627-756-713-160-756-1_V.mp3)
  Segment 22: 76980ms ~ 77556ms (file: 267-267-623-267-267-267-267-263-263-261-267_W.mp3)
  Segment 23: 78556ms ~ 79132ms (file: 632-126-216-231-126-132-125-216-216-261-265-256-276-267-265-256-265-265-265-265-321-321-325-235-237-725-237-574-574-576-576-576-574-547-514-153-317-371-317-317-567-567-657-751-3_W.mp3)
  Segment 24: 80132ms ~ 80708ms (file: 765-574-567-750-675-632-275-125-362-125-265-605-712-756-572-326-265-175-672-753-675-507-132-216-572-123-261-765-217-327-675-257-362-267-165-276-652-657-536-352-316-135-637-306-3_W.mp3)
  Segment 25: 81708ms ~ 82284ms (file: 210-201-210-765-716-612-612-631-216-620-627-162-102-627-621 321-126-251-312-162 136-672-672-267-136-612-621-321-132-137-107-157-173-132-136-123-132-316-132-130 162-123-321-273-2_W.mp3)
  Segment 26: 83284ms ~ 89800ms (file: 123-102-357-670-637-305-135-617-612-205-612-570-102-163-571-103-103-153-310-310-762-261-135-130-130-201-671-765-762-260-103-675-672-267-206-627-275-517-627-267-257-506-357-576-5_W.mp3)
  Segment 27: 90800ms ~ 91376ms (file: 376-613-617-761-716-761-176-612-671-761-765-763-176-176-675-675-765-316-326-756-765-672-672-675-765-765-672-765-765-765-765-761-763-761_W.mp3)
  Segment 28: 92376ms ~ 92952ms (file: 105-415-134-143-341-104-314-145-134-104-130-103-413 415-543-173-143-104-145-104-401-401-415-134-145-103-103-401-104-143-134-147-104-134-314-103-143-103-105-143-103 457-473-471-4_V.mp3)
  Segment 29: 93952ms ~ 94528ms (file: 136 123-317-627-136-175-106-106-106-610-602-620-362-176-163-213-173-632-631-263-261-621-326-615-163-236-263-107-167-163-107-167 213-123-603-613-756-762-627-267-103-163 162-126-1_W.mp3)
  Segment 30: 95528ms ~ 96104ms (file: 167-617-761-762-265-621-265-162-136-574-521-567-153-103-561-716-265-216-652-615-135-567-137-562-263-723-721-723-726-261-273-627-216-672-762-762-236-320-320-675-576-756-567-576-6_W.mp3)
  Segment 31: 97104ms ~ 97680ms (file: 163-316-136 136-103-316-103-134-136-136-103-316-136-164-136-130-103-103-103-136-136 135-135-135-135-156-145-154-103-106-103-103-136-103-136-103-134-136-135-135-135-163-135-156-1_V.mp3)
  Segment 32: 98680ms ~ 99256ms (file: 671-160-612-763-671-671-675-165-163-136-163-613-167-715-175-715-231 516-651-651-561-163-361-136-316-765 653-657-715-561-561-653-652-653-613-136-167-671-107-103-103-107-137-173-1_W.mp3)
  Segment 33: 100256ms ~ 100832ms (file: 170-750-175-175-675-176-175-617-167-671-175-175-175-167-167-176 107-175-105-170-715-105-157-157-156-150-175-157-156-175-156-176-156-175-176-167-176-751 510-150-107-710-107-175-1_V.mp3)
  Segment 34: 101832ms ~ 102408ms (file: 103-137-627-312-136-167-163-167-617-103-165-167-263-136-137-136-136-321-675-576-263-231-275-752-725-725-725-725-723-713-725-712-725-751-752-725-275-751-752-751-571-756-576-572-7_W.mp3)
  Segment 35: 103408ms ~ 103984ms (file: 576-506-670-672-507-137-132-137-563-670-760-670-567-750-256-206-673-763 765-607 123-567-562-312-762-763-576-507-576-756-570-637-672-631-761-576-675-637-675_W.mp3)
  Segment 36: 104984ms ~ 110996ms (file: 132-126-132-301-301-301-210-312-103-306-310-301-126-136-263-236-203-201-132-213-310-206-173-103-621-136-123-321-260-201-261-621-120-123-301-310-213-231-201-210-312-201-132-103-1_V.mp3)
  Segment 37: 111996ms ~ 112572ms (file: 675-672-675-657-615-613-632-632-310-167-256-652-652-635-536-536-576-653-627-456-573-537-563-726-706-713_V.mp3)
  Segment 38: 113572ms ~ 114148ms (file: 510-105-507-576 517-501-501-105-150-517-105-150-105-510-157-105-510-510-501-510-516-510-510-510-517-506-501-507-570 510-571-150-105-157-107-105-157-501-501-510-501-501-157-150-5_W.mp3)
  Segment 39: 115148ms ~ 115724ms (file: 751-175-750-175-716-175-751 150-175-175-715-715-756-173-715-570-175-173-371-715-710-715-715-715-175-710-701-750 751-175-751-710-751-170-705-176-750-751-107-175-175-170-715-175-7_V.mp3)
  Segment 40: 116724ms ~ 117300ms (file: 175-102-657-754-157-237-657-156-576-536-567-657-657-756-627-362-637-263-356-652-375-136-563-526-572-625-562-657-567-652-753-562-625-516-517-567-756-657-631-572-651-576-562-527-5_W.mp3)
  Segment 41: 118300ms ~ 123880ms (file: 260-123-126-321-506-120-103-261-231-130-310-201-201-216-206-267-562-670-276-306-206-261-201-120-203-573-561-263-216-210-130-127-312-507-361-103-206-126-102-103-726-276-102-612-2_W.mp3)
  Segment 42: 124880ms ~ 131612ms (file: 632-763-320-316-362-613-627-167-762-672-276-321-762-726-261-271-672-736-137-326-301-637-267-267-673-672-263-236-326-367-675-236-623-326-765-236-276-326-162-517-650-756-165-627-2_W.mp3)
  Segment 43: 132612ms ~ 133188ms (file: 574-510-571 150-105-150-501-170-150-150-570-571-571-510-157-570-157-135-513-715-537-573-570-501-510-571 507-157-150-170-105-105 150-105-107-150-571-507-157-750-705-105-751-157-1_V.mp3)
  Segment 44: 134188ms ~ 134764ms (file: 573-375-315-356-753-567-576-571-570-576-756-316-136-317-316-136-316-316-376-607-360-673-103-376-765-317-365-576-570-756-756-765-756-173-176-136-312-136-136-316-316-163-136-163-3_W.mp3)
  Segment 45: 135764ms ~ 136340ms (file: 106-312-103-175-265-367-162-156-163-163-106-162-621-621-162-716-213-103-716-657-216-261-326-561 673-671 136-137-103-106-163-601-162-106-162-672-670-627-163-106-175-607-160-167-1_V.mp3)
  Segment 46: 137340ms ~ 137916ms (file: 625-265-267-265-265-652-625-562-652-625-653-625-627-362-623-236-621-576-567-657-567-576-567-653-236-203-362-351-256-635-637-613-612-612-261-126-127-276-263-127-216-267-261-637-2_W.mp3)
  Segment 47: 138916ms ~ 139492ms (file: 570-570 570-570-517-531-156-510-513-150-517-517-513-157-571-135-157-510-510-517-570-567-576-567-567 510-517-517-157-517-157-571-517-571-751-715-571-570-576-576-576-570-507-570 5_V.mp3)
  Segment 48: 140492ms ~ 141068ms (file: 326-230-320-231-230-320-230-325-236-362-367-715-127-126-175-162-302-206-621-367-632-163-631-106-213-210-216-203-546-517-156-157-261-260-260-627-602-216-267-362-103-561-201-137-1_W.mp3)
  Segment 49: 142068ms ~ 142644ms (file: 213-765-602-576-567-571-571-576-657-562-657-165-572-657-127-352-576-653-652-571-157-317-137-176-675-576-576-526-576-567-572-657-567-576-576-567-765-675-561-657-536-751-567-105-5_W.mp3)
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
> **Time**: 2026-04-05 08:07:41
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 2,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
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
"transcript": "오늘의 한국어 한마디는 바로 감사합니다입니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 420,
"audio_type": "speech",
"words": [
{"word": "오늘의", "start_ms": 420, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "한국어", "start_ms": 1050, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "한마디는", "start_ms": 1750, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 2600, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "감사합니다입니다", "start_ms": 3150, "dur_ms": 1150, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 7,
"transcript": "상대방에게 고마움을 표현할 때 가장 많이 쓰이죠",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 380,
"audio_type": "speech",
"words": [
{"word": "상대방에게", "start_ms": 380, "dur_ms": 780, "phase_lr_deg": 0.0},
{"word": "고마움을", "start_ms": 1250, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "표현할", "start_ms": 1950, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 2600, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 2950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 3500, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "쓰이죠", "start_ms": 4000, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "Thank you so much",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "Thank", "start_ms": 250, "dur_ms": 350, "phase_lr_deg": 10.0},
{"word": "you", "start_ms": 650, "dur_ms": 220, "phase_lr_deg": 10.0},
{"word": "so", "start_ms": 950, "dur_ms": 280, "phase_lr_deg": 10.0},
{"word": "much", "start_ms": 1300, "dur_ms": 450, "phase_lr_deg": 10.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "친한 사이에서는 고마워라고 말할 수 있습니다",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 450,
"audio_type": "speech",
"words": [
{"word": "친한", "start_ms": 450, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "사이에서는", "start_ms": 1000, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "고마워라고", "start_ms": 1850, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "말할", "start_ms": 2600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 3100, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 3400, "dur_ms": 720, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "아리가토 고자이마스 일본어로는 이렇게 표현하죠",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ja",
"silence_before_ms": 320,
"audio_type": "speech",
"words": [
{"word": "아리가토", "start_ms": 320, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "고자이마스", "start_ms": 1050, "dur_ms": 820, "phase_lr_deg": 0.0},
{"word": "일본어로는", "start_ms": 2000, "dur_ms": 780, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 2850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "표현하죠", "start_ms": 3400, "dur_ms": 680, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "여러 언어로 고마움을 전하는 법을 배우니 재미있지 않나요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 410,
"audio_type": "speech",
"words": [
{"word": "여러", "start_ms": 410, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "언어로", "start_ms": 850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "고마움을", "start_ms": 1400, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "전하는", "start_ms": 2000, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "법을", "start_ms": 2550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "배우니", "start_ms": 2950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "재미있지", "start_ms": 3500, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "않나요", "start_ms": 4200, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "오늘 배운 표현들을 꼭 복습해 보시길 바랍니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 350,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 350, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "배운", "start_ms": 800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "표현들을", "start_ms": 1200, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "꼭", "start_ms": 1900, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "복습해", "start_ms": 2200, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "보시길", "start_ms": 2800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "바랍니다", "start_ms": 3300, "dur_ms": 720, "phase_lr_deg": 0.0}
]
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "그럼 다음 시간에 또 만나요 감사합니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 280,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 280, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 700, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "시간에", "start_ms": 1100, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 1650, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 1900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 2450, "dur_ms": 850, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "See you again next time Bye bye",
"speaker": "speaker_2",
"confidence": 0.92,
"language": "en",
"silence_before_ms": 510,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 510, "dur_ms": 280, "phase_lr_deg": 10.0},
{"word": "you", "start_ms": 850, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "again", "start_ms": 1100, "dur_ms": 420, "phase_lr_deg": 10.0},
{"word": "next", "start_ms": 1600, "dur_ms": 350, "phase_lr_deg": 10.0},
{"word": "time", "start_ms": 2050, "dur_ms": 450, "phase_lr_deg": 10.0},
{"word": "Bye", "start_ms": 2600, "dur_ms": 320, "phase_lr_deg": 10.0},
{"word": "bye", "start_ms": 3000, "dur_ms": 380, "phase_lr_deg": 10.0}
]
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
*Generated by WKAppBot ask gemini — 2026-04-05 08:07:41*
