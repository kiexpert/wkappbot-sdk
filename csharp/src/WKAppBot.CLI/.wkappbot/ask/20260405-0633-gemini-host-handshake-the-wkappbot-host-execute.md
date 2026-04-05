# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=11716  time=06:31:59  uptime=16975s  mem=437MB  system_processes=351"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_062941.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 576-637-106-162-601-612-613-607-516-106-136-126-106-103-163-310-574-371-623-361-326-316-130-371-574-756-631-106-123-106-162-106-162-260-260-102-102-310-263-136-136-136-136-675-3_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 571-571-517-571-571-751-571-567-576-576-576-576-576-576-576-157-517-715-571-501-157-517-571-157-517-571-751-756-750-756-751-175-751-715-175-710-751-175-571-571-571-571-570-570-5_V.mp3)
  Segment 3: 3152ms ~ 9668ms (file: 163-756-713-206-231-312-103-130-571-357-571-517-371-376-136-367-136-371-163-317-137-310-137-136-317-312-130-137-176-137-763-217-210-251-574-205-201-160-167-675-675-617-675-317-7_W.mp3)
  Segment 4: 10668ms ~ 17436ms (file: 132-132-231-162-120-231-132-126-231-320-120-123-765-657-267-231-275-125-120-230-102-152-125-102-123-102-230-236-230-123-102-103-235-102-210-123-120-231-132-130-120-213-231-120-5_V.mp3)
  Segment 5: 18436ms ~ 22648ms (file: 612-316-312-160-163-621-126-216-231-612-326-123-231-213-126-123-216-217-126-265-726-672-217-236-126-263-132-210-126-120-362-621-203-236-126-261-762-127-132-123-271-705-176-671-1_W_stt_if.mp3)
  Segment 6: 23648ms ~ 24224ms (file: 705-750-574-753-705-705-570 765 506-153-375_W.mp3)
  Segment 7: 25224ms ~ 25800ms (file: 163-260-263-326-103-306-306-321-370-360-326-265-621-162-321-301-230-275-210-302-210-203-206-213-231-236-326-362-627-623-263-301-302-573-753-726-206-206-260-206-236-362-360-302-7_W.mp3)
  Segment 8: 26800ms ~ 39220ms (file: 267-263-325-572-231-230-320-320-136-265-754-571-573-103-320-547-310-230-103-103-230-362-630-302-752-263-206-571-750-574-130-132-103 203-206-206-263-673-637-326-236-320-357-571-1_W_stt_as_if_i'm_a_file_is_not_a_real_as_the_fi.mp3)
  Segment 9: 40220ms ~ 40796ms (file: 127-143-641-275-261-216-125-231-136-675-123-167-732-623-213-132-231-213-231-135-312-321-316-375-321-276-136-612-321-325-217-351-132-125-245-126-326-265-367-367-251-162-231-145-3_V.mp3)
  Segment 10: 41796ms ~ 50832ms (file: 726-672-237-267-267-176-326-672-263-267-623-236-230-762-637-321-236-673-263-326-763-675-265-263-576-657-361-675-106-576-650-103-675-134-576-574-130-320-326-210-321-213-361-132-1_W.mp3)
  Segment 11: 51832ms ~ 52408ms (file: 705-267-570-265-723-276-136-503-352-312-326-675-216-215-623-326-573-235-325-321-560-725-267-237-153-127-507-762-576 231-570-620-657-576-750-572-512-751-574-753-735-576-756-756-2_V.mp3)
  Segment 12: 53408ms ~ 53984ms (file: 150-315-150-137-715-175-135-150-175-175-137-137-103-130-107-105-130_V.mp3)
  Segment 13: 54984ms ~ 55560ms (file: 136-167 617-601-103-136-103-167-261-132-613-132-163-621-517-126-217-201-176-126-671-143-675-167-617-612-126-627-621-623-621-621-657-602-102-136-106-167-617-613 761-316-367-103-1_V.mp3)
  Segment 14: 56560ms ~ 63328ms (file: 230-103-763-152-126-257-123-627-627-260-517-574-261-216-621-367-632-316-137-163-136-263-627-574-210-237-265-260-120-271-267-267-126-103-216-216-216-126-162-132-210-132-201-201-7_W.mp3)
  Segment 15: 64328ms ~ 64904ms (file: 175-751-570-750-751-517-571-571-517-751-570-105-175-570-107-107-105-570-510-175-501-150-507-571-510-150-501-501-501-517-571-715-751-137-715-157-715-571-715-715-751-751-175-751-5_V.mp3)
  Segment 16: 65904ms ~ 66480ms (file: 135-102-716-173-176-175-167-670-726-610-671-617-103-163-306-103-106-176-316-136-163-201-206-623-103-165-716-106-103-130-765-617-651-567-167-167-612-621-612-263-621-261-267-261-1_V.mp3)
  Segment 17: 67480ms ~ 68056ms (file: 163-103-106-106-150-163-367-316-316-176-163-361-751-136-136-750-162-362-136-671-617-165-623-630-165-130-317-126-106-163-317-631-516-731-103-103-160-631-136-103-361-701-136-103-6_V.mp3)
  Segment 18: 69056ms ~ 69632ms (file: 570-751-751-571 574-574-574-574-574-574-547-574-574-547-574-574-574-574-574-574_W.mp3)
  Segment 19: 70632ms ~ 71208ms (file: 136 765-765 567-612-672-761-763-765-706-671-761-760-106-136-163-367-361-163-163-316-623-735-126-301 106-137-130-613-163-106-160-107-163-670-630-615-106-165-510-762-132-162-310-3_W.mp3)
  Segment 20: 72208ms ~ 72784ms (file: 103-130-137-713-751-715-517-571-531-631-106-163-132-135-136-135-156-136-165-136-165-165-163-165-165-163-102-123-102-123-106-160-165-162-173-162-167-136-317-136-631-167-173-617-6_W.mp3)
  Segment 21: 73784ms ~ 74360ms (file: 137-175-571-157-137-163-163-136-176-765-735-731-763-765-735-376-735-716-371-763-736-765-716-765-175-157-137-651-561-136-156-153-165-156-153-135-132-135-103-617-306-306-130-317-1_W.mp3)
  Segment 22: 75360ms ~ 75936ms (file: 102-124 716-673-103-162-103-103-135-102-106 134-137-132-103-132-125-152-107-103-165_V.mp3)
  Segment 23: 76936ms ~ 77512ms (file: 765-756-573-753-573-753-576-675 163-612_W.mp3)
  Segment 24: 78512ms ~ 79088ms (file: 715-130-317-370-175-571-371-317-735-307-713-137-751-375 751-130-751-173-375-753-137-317-713-173-301-307-137-103-107-716-137-310-301-376-703-137-371-137-137-137-136-137-371-731 3_W.mp3)
  Segment 25: 80088ms ~ 80664ms (file: 627-670-652-672 657-765-652-572-652-627-627-567-657-657-567-657-627-652 625-620-625-265-265-625-657-627-265-625-265-265_W.mp3)
  Segment 26: 81664ms ~ 82240ms (file: 137-637-657-536-735-651-351-563-321-376-631-132-261-627-675-574-751-136-753-563-675-657-652-516-576-531-651-536-536-563-561-574-573-356-315-567-612-316-356-315-517-536-315-237-3_W.mp3)
  Segment 27: 83240ms ~ 83816ms (file: 541-574-105-150-175-176-765-175-176-175-176-175-617-167-175-176-175-176-167-176-751-517 105-105-150-105-105-105-105-175-760-175-176-175-176-175-156-175-157-175-175-175-176-176 1_V.mp3)
  Segment 28: 84816ms ~ 85392ms (file: 567-576-576-756-567-576-576 605-567-576-576-576-576-576-567-576-576-567-576-657-576-576-576-576 165-567-576-576-576-576-576-576-576-576-576-576-567-576-576 657-567-567-567-576-5_W.mp3)
  Segment 29: 86392ms ~ 86968ms (file: 510-150-510-501-150-501-150-517-150-501-157-510-501-510-501-510-501-507-507-507 510-150-517-501-510-157-517-507-501-510-501-501-150-510-510-510-150-150-105-510-501-501-507-501-5_V.mp3)
  Segment 30: 87968ms ~ 88544ms (file: 103-135-136-135-135-153-156-163-136-164-134-104-143-103-106-167-136-103-106-106-163-146-106-163-106-163-156-104-136 153-715-751-157-574-574-317-315-351-315-315-315-513-751-574-5_V.mp3)
  Segment 31: 89544ms ~ 90120ms (file: 517-570-517-715-517-570 574-157-750-751-517-750-715-705-170-750-175-751-750-157-751-701-570-751-170-175-710-750-570-750-570-751-517-571-570-751-751-751 570-574-571-570-570-570-7_W.mp3)
  Segment 32: 91120ms ~ 99796ms (file: 213-216-150-514-106-167-173-123-261-527-102-231-672-236-620-263-103-765-267-126-362-132-275-160-754-723-213-132-312-612-614-751-310-312-732-705-267-261-231-326-236-230-612-102-2_W.mp3)
  Segment 33: 100796ms ~ 111092ms (file: 726-236-253-531-271-567-762-725-137-320-275-576-206-620-256-265-267-620-602-502-165-532-132-103-316-263-267-267-236-527-672-260-260-320-235-263-260-206-573-367-263-715-230-756-2_W.mp3)
  Segment 34: 112092ms ~ 112668ms (file: 312-617-163-157-567-651-567-567-362-317-537-315_W.mp3)
  Segment 35: 113668ms ~ 117664ms (file: 302-327-217-173-107-213-102-301-231-201-210-321-231-132-321-236-273-703-102-312-237-273-273-236-276-672-372-130-217-312-263-231-237-237-271-123-237-273-721-726-712-327-237-217-3_W.mp3)
  Segment 36: 118664ms ~ 119240ms (file: 175-105-103-126-163-165-163-163-631-163-163-136-136-126-130 570-157-573-750-751-756-576-576-517-537-571-750-571-571 756-701-571-570-507-750-570-570-571-576-765-756-576-751-571-7_W.mp3)
  Segment 37: 120240ms ~ 127584ms (file: 275-672-260-250-527-250-201-203-106-623-130-301-132-301-572-210-207-216-217-541-570-102-217-206-206-130-572-572-627-312-276-632-672-756-260-123-213-320-310-526-574-210-130-572-2_W.mp3)
  Segment 38: 128584ms ~ 129160ms (file: 103-570-150-236-263-203-632-263-527-570-105-165-126-214 123-127-672-607-673-760-760-317-126-137-501-570-750-705-736-376-132_V.mp3)
  Segment 39: 130160ms ~ 130736ms (file: 765-126-560-572-716-267-236-617-317-317-162-705-327-371-523-312-501-251-517-175-512-263-257-756-136-271-326-172-317-267-752-175-657-273-216-562-325-236-263-752-623-756-135-130-3_V.mp3)
  Segment 40: 131736ms ~ 132312ms (file: 130-301-162-260-206-201 612-716-162-167-316-216-102-612-106-162-562-163-176-175-637-730-756-765-612-106-613-613-106-162-615-162-612-162-612-672-672-672-607 320-320-302-321-213 6_W.mp3)
  Segment 41: 133312ms ~ 140656ms (file: 327-763-765-237-327-534-102-126-326-320-573-301-275-123-267-263-260-617-236-321-310-163-163-613-613-362-316-316-136-130-320-320-623-136-106-163-672-263-321-103-165-376-130-217-1_W.mp3)
  Segment 42: 141656ms ~ 142232ms (file: 672-312-136-106-265-170-132-103-125-150-370-571 125-160-103-103-517-103-617-136-103-103-103-103-567-617-103-260-263-136-162-135-206-106-123-631-132-126-576-263-136-130-103-261-2_V.mp3)
  Segment 43: 143232ms ~ 143808ms (file: 162-361-130-130-721-127-107-571-715-105-150-106-106_V.mp3)
  Segment 44: 144808ms ~ 145384ms (file: 314-104-104-145-134-143-413-514-105-105-135-103-105-143-104 135-431-310-104-143-104-174-154-541-145-105-105-134-154-145-103-143-104-105-103-153-134-104-143-410-145-401 310-173-3_V.mp3)
  Segment 45: 146384ms ~ 146960ms (file: 576-571-571-517-517 157-105-501-501-517-510-105-501-105-510-105-105-105-510-150-510-175-510-150-105-157-510-507-514-570-571-571-570-507-570 501-510-517-501-517-501-501-501-501-1_V.mp3)
  Segment 46: 147960ms ~ 148536ms (file: 576-356-735-753-376-375-312-325-317-571-517-153-573-753-763-735-576-753-756-576-756-576-756-576-576-573-573-576-573 576-576-573-576-756-576-576-751-751-576-517-627-675-627-672-2_W.mp3)
  Segment 47: 149536ms ~ 150112ms (file: 235-136-605-620-621-672-675-627-627-621-267-726-672-627-672-267-627-263-265-267-627-267-672-657-672-726-612-167-726-276_W.mp3)
  Segment 48: 151112ms ~ 151688ms (file: 132-132-103-216-123-132-123-136-136-310-103-132-312-103_V.mp3)
  Segment 49: 152688ms ~ 153264ms (file: 756-765-716-176-176-170-751-756-756-756-751-751-751-571-751-751-751-576-751-571-571-157-571_W.mp3)
  Segment 50: 154264ms ~ 154840ms (file: 315 157-167-715-536-301-157-150-156-576-675-762-176-315-167-617-132-123-137-137-317-173-137-317-316-371-163-637-631-136-163-175-176-157-561-751-617-615-673-675-175-716-175-512-5_W.mp3)
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
> **Time**: 2026-04-05 06:33:02
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "습니다 그런데 반물질은 실제로 존재합니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "습니다", "start_ms": 10, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "그런데", "start_ms": 140, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "반물질은", "start_ms": 300, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "실제로", "start_ms": 450, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "존재합니다", "start_ms": 560, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 3,
"transcript": "SF 소설 속 이야기가 아니고요 CERN에서는 실제로 반양성자를",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "SF", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "소설", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "속", "start_ms": 290, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "이야기가", "start_ms": 380, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "아니고요", "start_ms": 540, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "미국은 대학 갈 필요 없단 얘기가 나오고 있고 인간 지성의 마지막 보루마저 AI에게 뚫렸습니다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "미국은", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 2.5},
{"word": "대학", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 2.5},
{"word": "갈", "start_ms": 310, "dur_ms": 80, "phase_lr_deg": 2.5}
]
},
{
"segment": 5,
"transcript": "하지만 가장 놀라운 건 중국이에요 중국 지도부는",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "하지만", "start_ms": 30, "dur_ms": 140, "phase_lr_deg": 2.5},
{"word": "가장", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": 2.5}
]
},
{
"segment": 6,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 7,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 8,
"transcript": "I said I got 5 out of 5 but it's not very likely okay so the first one I got that correct",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 120, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "said", "start_ms": 230, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "The second one I think I got that one correct alright",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "The", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "second", "start_ms": 130, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "The third one oh man oh gosh okay missed that one",
"speaker": "speaker_3",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "The", "start_ms": 60, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "이거 그냥 기름 못 닦는다고 보는데요 네 맞아요",
"speaker": "speaker_4",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "mixed",
"words": [
{"word": "이거", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "그냥", "start_ms": 180, "dur_ms": 120, "phase_lr_deg": -5.0}
]
},
{
"segment": 12,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 13,
"transcript": "기름 소스 정리할 때도 초벌과 재벌 소스 각각의 방법이 다르다고 합니다",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "기름", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "것입니다 한국산 세탁기가 미국 시장에 본격적으로 들어가기 시작했을 때 미국 소비자들은 정말 놀랐다고 하는데요",
"speaker": "speaker_6",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "것입니다", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 16,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 17,
"transcript": "세탁기가 너무나 조용",
"speaker": "speaker_6",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "세탁기가", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 19,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 20,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 21,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 22,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 23,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 24,
"transcript": "맞서 당당한 대한민국의 주권을 지켜내야 할 때입니다 기구란 동맹은 영원할 수 없지만",
"speaker": "speaker_7",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "맞서", "start_ms": 100, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "당당하고 자강을 이룬 국가의 동맹은 그 누구도 함부로 흔들 수 없습니다 트럼프의 청구서",
"speaker": "speaker_7",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "당당하고", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "초 발사요 그렇기 때문에 여기에 따른 실제 비행 성능을 검증하는 구간이기 때문에",
"speaker": "speaker_8",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "초", "start_ms": 60, "dur_ms": 80, "phase_lr_deg": -1.0}
]
},
{
"segment": 27,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 28,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 29,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 30,
"transcript": "스페이스X 또는 우주 관련된 주식 투자하시는 분들도 관심 있게 보실 것 같습니다",
"speaker": "speaker_8",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "스페이스X", "start_ms": 45, "dur_ms": 250, "phase_lr_deg": -1.0}
]
},
{
"segment": 31,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 32,
"transcript": "이게 엄청 화려하고요 계속 메뉴가 바뀌면서 나와요 이게 떨어지면 다른 메뉴가 나오고",
"speaker": "speaker_9",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "이게", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 10.0}
]
},
{
"segment": 33,
"transcript": "알려지자 일본에서는 분위기가 완전히 달라집니다 이번엔 진짜 제대로 나올 수도 있겠다",
"speaker": "speaker_10",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "알려지자", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 35,
"transcript": "이렇게 기대감이 올라가기 시작한 거죠 사실 처음 반응은 완전히 반대였거든요",
"speaker": "speaker_10",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "이렇게", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 37,
"transcript": "시대니까요 가장 먼저 눈을 돌린 것은 당연히 네덜란드였습니다 세계 농산물 수출 3위",
"speaker": "speaker_11",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "시대니까요", "start_ms": 20, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 39,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 40,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 41,
"transcript": "유리 온실의 나라 작은 영토에서 기적적인 농업 생산성을 이뤄낸",
"speaker": "speaker_11",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "유리", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 43,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 44,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 45,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 46,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 47,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 48,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 49,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 50,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 06:33:02*
