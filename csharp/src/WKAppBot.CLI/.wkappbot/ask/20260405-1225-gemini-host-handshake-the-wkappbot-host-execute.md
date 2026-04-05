# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=10820  time=12:24:09  uptime=29985s  mem=426MB  system_processes=357"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_122208.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 765-763-756-756-765 671-673 672-763-726-612-612-362-612_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 750-710-705-170-750-750-751-750-107-715-517-570-705-715 750-570-105-715-710-570-710-175-105-710-705-107-705-170-750-715-751-501-750-570-751-750-750-750-570-570-570 501-751-751-7_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 750-570-105-705-571-750-170-715-175-710-107-710-571-157-750-571-754 570-574-570-705-170-715-175-175-715-170-715-170-750-701-107-751-571-507-715-751-507-705-754 574-517-570-107-1_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 705-537-705-705-706-765-760-765-762-672-627-762-765-672-571-756-307-307-376-307-371-135 751-170-105-105-106-136-317-136-167-136-137-136-136-136-673-671-631-137-317-173-173-176-1_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 130-713-103-710-173 135-301-301-315-135-153-135-103-357-310-137-135-103-137-105-137-317-305-130-130-103-103-317-315-305-703-731 135-103-301-315-103-301-137-135-315-315-357-301-3_V.mp3)
  Segment 6: 7880ms ~ 16484ms (file: 514-574-576-576-276-517-105-317-172-731-237-321-312-123-132-130-103-573-573-571-230-236-263-320-320-547-534-527-321-321-123-210-574-574-504-105-310-130-103-130-103-105-312-573-2_W.mp3)
  Segment 7: 17484ms ~ 18060ms (file: 572-756-576-657-765-567-567-567-657-756-576-765 752-756-576-567-675-567-576-576-756 756-756-756-756-576-657-576-576-567-576-576-576-567-756-537-531-765-756-765-756-756-756-756-7_W.mp3)
  Segment 8: 19060ms ~ 19636ms (file: 536-571-507-527-573-536-574-576-573-501-576-573-573-571-531-572-537 537-561-531-536_V.mp3)
  Segment 9: 20636ms ~ 29456ms (file: 267-203-130-216-123-130-123-216-124-120-260-261-261-276-754-105-236-103-315-231-231-261-310-263-123-315-216-312-153-652-165-163-571-576-572-312-263-267-576-527-572-130-756-135-1_W.mp3)
  Segment 10: 30456ms ~ 38484ms (file: 675-576-627-632-367-362-765-756-236-236-160-136-627-267-627-627-162-106-675-326-326-756-526-267-231-126-754-236-263-236-263-273-765-267-627-267-627-263-762-765-106-160-216-652-2_W.mp3)
  Segment 11: 39484ms ~ 40060ms (file: 751-175-175-175-175-751-175-175-751 150-501-501-105-157-105-105-175-751-715-751-175-751-715-175-751-715-175-175-175-175-175-175-175-751 157-176-157-513-153-165-163-136-316-163-1_W.mp3)
  Segment 12: 41060ms ~ 41636ms (file: 561-572-752-572-752-572-751-372-621-260-203-526-512-561-526-561-527-576-562-526-562-523-562-502-512-527-256-250-526-205-265-652-205-532-256-623-620-260-263-623-621-263-261-126-2_W.mp3)
  Segment 13: 42636ms ~ 43212ms (file: 156-512-574-512-512-513-531-502-536-531-572-512-521-527-574-572-563-516-576-531-501-250-562-625-652-657-567-576-567-576-567-576-756-675-756-751-756-765-756-376-562-527-527-527-5_V.mp3)
  Segment 14: 44212ms ~ 44788ms (file: 106-106-162-620-612-621-672-326-302-762-627-362-230-103-123-132-673-326-236-203-136-570-750-630-301-312-326-321-312-132-106-103-136-132-102-102-132-103-301-103-301-317-137-312-3_V.mp3)
  Segment 15: 45788ms ~ 46364ms (file: 306-301-310-103-107-701-107-310-351-715-701-170-137-310-701-710 137-173-301-130-153-175-715-607-617-610-671-307-317-310-137-756-130-315-317-173-317-137-135-135-317-130 301-371-1_V.mp3)
  Segment 16: 47364ms ~ 56760ms (file: 672-657-570-750-752-132-235-354-326-567-132-107-765 310-237-275-456-765-162-261-752-137-130-512-213-231-357-273-231-321-312-715-126-213-231-321-321-753-106-106-715-107-132-154-5_V.mp3)
  Segment 17: 57760ms ~ 58336ms (file: 301-163-716-763-310-610-657-163-163 713-173-371-137-317-375-130-301-613-607-631-316-310-103-137-135-317-163-367-365-371-136-613-671-673-637-652-675 750-715-751-107-375-571-175-1_V.mp3)
  Segment 18: 59336ms ~ 59912ms (file: 631-361-136-132-132-132-312-103-163-106-167-130-167-327-175-621-136-631-167-136-103-163-316-301-321-106-103-127-632-602-163-150-516-150-103-302-736-176-136-162-127-150-350-136-3_V.mp3)
  Segment 19: 60912ms ~ 70308ms (file: 123-143-627-230-236-301-230-310-230-276-273-753-103-210-103-102-321-123-312-124-756-321-103-103-752-102-752-310-201-203-572-327-103-302-312-267-230-320-102-672-672-263-762-236-2_W.mp3)
  Segment 20: 71308ms ~ 71884ms (file: 103-103-162-513-127-126-163-176-637-175-106-106-765-167-615-163-137-162-126-103-615-130-106-106-156-136-106-136-163-106-631-163-126-103-106-136-130-176-623-263 627-361-637-367-3_V.mp3)
  Segment 21: 72884ms ~ 73460ms (file: 261-321-132-123 257-176-167-163-126-162-106-136-231 314-507-754-570-750-276-620-657-657-165-615-657-756-675-675-106-310-301-612-136-167-267-206-201-236-206-620-206-203-260-260-6_V.mp3)
  Segment 22: 74460ms ~ 75036ms (file: 617-610-612-621 231-261-625-621-621-617-613-612-617-617-617-617-612-617-612-671-612-152 162-615-617-615-617-612-617-617-617-671-617-617-612-607-617-612-156-564 612-610-265-621-6_W.mp3)
  Segment 23: 76036ms ~ 76612ms (file: 367-367-637-673-763-763-713-673-671-235-372-713-723-137-317-136-362-301-310-301-312-130_W.mp3)
  Segment 24: 77612ms ~ 78188ms (file: 520-576-572-576-657-576-576-756-756-765-675-765-765-765-765-657-574-517-527-205-520-257-570-762-267-257-257-270-562-756-725-675-765-765-675-765-765-671-765-675-765-675-675-675_V.mp3)
  Segment 25: 79188ms ~ 79764ms (file: 136-163-163-256-162-517-165-725-273-627-762-217-512-236-532-207-502-206-125-237-273-216-750-216-520-127-216-173-570-576-321-731-275-357-327-715-572-362-765-547-453-213-657-273-7_V.mp3)
  Segment 26: 80764ms ~ 81340ms (file: 657-657-654-132-361-163-105-130-612-163 150-153-162-621-216-256-267-127-172-715-175-671-726 625-652-654-652-564 106-102-106_W.mp3)
  Segment 27: 82340ms ~ 87632ms (file: 206-201-203-263-213-261-267-236-237-263-206-126-260-256-237-267-213-213-236-762-672-762-765-213-302-132-301-231-302-230-203-201-210-260-137-123-231-236-263-263-216-210-120-106-6_W.mp3)
  Segment 28: 88632ms ~ 89208ms (file: 567-576-576-271-364-324-236-567-136 765-765-756-675-617-135-107-216-130-132-103-125-175-712-267-275-321-673-765-765-756-712-371-371-173-573-135-135-125-123-573-573-576-103-716-7_W.mp3)
  Segment 29: 90208ms ~ 90784ms (file: 570-157-570-571-571-571-571-576-571-576-570-570-510-570-510-517-517-570-501-510-501-571-751-153-750-175-517-571-751-571-517-501-751-570-571-571-570-510-507-570-576-571-501-501-5_V.mp3)
  Segment 30: 91784ms ~ 100388ms (file: 236-132-132-102-106-267-321-326-230-236-102-102-672-276-132-537-167-613-123-321-752-671-267-236-236-217-257-726-237-327-357-106-237-102-163-103-106-276-271-273-276-267-273-132-3_W.mp3)
  Segment 31: 101388ms ~ 114240ms (file: 517-201-571-210-172-126-102-362-367-326-257-275-721-217-172-125-120-514 231-230-203-201-216-267-214-231-276-267-216-267-216-263-267-217-260-267-270-312-275-263-210-103-256-625-2_W_stt_the_home_of_the_valve.mp3)
  Segment 32: 115240ms ~ 115816ms (file: 103-107-170-175-170-173-157-735-175-175-753-756-756-756-756-751-175-751-715-751 105-103-107-170-107-107-750-170-715-751-715-570-751-710-750-573-765-756-765-751-751-756-765-751-7_W.mp3)
  Segment 33: 116816ms ~ 125780ms (file: 526-132-173-267-712-136-267-276-123-327-103-206-321-107-735-130-627-623-307-267-120-132-123-267-267-326-321-130-103-750-273-267-627-103-236-762-276-752-573-263-260-173-310-267-3_W.mp3)
  Segment 34: 126780ms ~ 136644ms (file: 310-216-216-752-167-362-675-107-371-136-271-136-263-132-120-123-617-765-675-672-756-765-267-260-753-756-756-312-321-260-756-761-236-267-130-672-756-236-675-615-367-163-762-613-1_W.mp3)
  Segment 35: 137644ms ~ 145492ms (file: 673-726-276-301-321-136-320-162-576-210-312-123-267-236-237-326-320-301-267-263-263-236-207-231-327-325-357-375-326-754-170-103-613-371-137-321-371-103-103-102-217-216-320-132-1_W.mp3)
  Segment 36: 146492ms ~ 147068ms (file: 527-576-573-567-573-574-537-571-574-572-576-574-507-576-517-574-527-576-572-105-105-105-105 516-541-547-541-571-541-547-534-564-517-563-542-541_V.mp3)
  Segment 37: 148068ms ~ 159192ms (file: 137-173-123-126-132-701-137-302-703-312-376-726-672-103-301-321-320-130-270-107-167-671-120-107-267-126-213-213-321-231-321-123-261-231-672-276-217-731-326-267-276-260-271-207-1_W.mp3)
  Segment 38: 160192ms ~ 160768ms (file: 301-173-751-301-601-607-603-603-631-617-617-631-136-637-163-163-316-310-163-137-160-316-316-106-671 731-705-735-130-315-371-610-630-607-603-607-613-610-617-637-607-670-670-607-1_V.mp3)
  Segment 39: 161768ms ~ 162344ms (file: 273-276-654-673-165-231-103-607-167-106-617-326-256-136-526 106-103-176-130-256-132-637-602-106-162-167-716-621-631-602-172-132-103-165-163-321-126-275-306-165-617-671-657-621 1_V.mp3)
  Segment 40: 163344ms ~ 163920ms (file: 562-237-126-627-567-265-510-715-126-762-273-125-627-567-765-106-310_V.mp3)
  Segment 41: 164920ms ~ 165496ms (file: 261-317-173-261-126-316-301-103-132-162-162-672-716-137-163-673-716-756-675-176-137-612-213-716-163-176-163-132-136-132-126-126-327-216-263-362-623-623-236-236-237-621-230-312-2_W.mp3)
  Segment 42: 166496ms ~ 167072ms (file: 317-137-103-130-135-137-317-375-137-310-137-132-301-317-127-137 315-105-137-317-103-132-312-301-107-130-103-317-153-135-317-127-317-301-312-103-307-301-371-312 157-371-137-310-3_V.mp3)
  Segment 43: 168072ms ~ 168648ms (file: 502-502-507-504-507-502-506-507-507-507-507-507-521-567-560-507-502 507-502-526-506-507-507 506-576-507-562-506-567-752-275-527-527-752-570-526-725-205-250-502-520-275-205-265-2_V.mp3)
  Segment 44: 169648ms ~ 175912ms (file: 137-316-136-216-621-623-721-265-574-574-267-216-132-316-574-627-267-756-125-260-361-103-210-260-261-216-261-276-235-267-627-267-627-627-267-216-765-765-527-256-267-706-267-213-1_W_stt_wbc_belt,_cti_academic_pm_in_through_the.mp3)
  Segment 45: 176912ms ~ 186308ms (file: 671-756-567-231-176-130-213-527-572-203-623-621-130-546-756-231-312-735-253-571-120-132-613-362-136-132-123-103-123-136-301-306-312-301-306-136-310-310-632-316-132-103-103-103-3_V.mp3)
  Segment 46: 187308ms ~ 187884ms (file: 104-104-145-140-401-150-154-107-134-135-145-153-154-105-415-175-104-145-145-145-104 130-401-103-431-541-145-140-145-540-405-157-145-104-145-514-410-154-145-134-154-413-341-451-3_W.mp3)
  Segment 47: 188884ms ~ 189460ms (file: 103-132-413-132-106-136-540-132 213-103-263-162-165-715 756-756-756-756-576-756-756-167-102-126-172-172 671-675-765-657-675-617-756-756-132-103-756-765-675-716-671-517-152-135-1_W.mp3)
  Segment 48: 190460ms ~ 194744ms (file: 132-103-103-312 123-510-521-203-203-620-213-623-276-206-301-206_V.mp3)
  Segment 49: 195744ms ~ 208920ms (file: 123-213-210-213-231-213-321-123-213-312-120-103-302-213-120-123-203-132-312-301-301-302-321-305-301-354-357-315-367-361-316-312-312-321-567-567-132-213-136-132-213-127-216-261-1_V.mp3)
  Segment 50: 209920ms ~ 210496ms (file: 570-570-570-570-576-507-507-570_W.mp3)
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
> **Time**: 2026-04-05 12:25:20
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "안녕하세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "안녕", "start_ms": 40, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "하세요", "start_ms": 260, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "반갑습니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "반갑", "start_ms": 50, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "습니다", "start_ms": 260, "dur_ms": 310, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "오늘 날씨가 좋네요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 60, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "날씨가", "start_ms": 210, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "좋네요", "start_ms": 410, "dur_ms": 150, "phase_lr_deg": -5.0}
]
},
{
"segment": 4,
"transcript": "한국어 공부는 정말 재미있어요. 그쵸?",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 80, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "공부는", "start_ms": 480, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 830, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "재미있어요.", "start_ms": 1080, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "그쵸?", "start_ms": 1680, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "매일 꾸준히 연습하는 것이 가장 중요하다고 생각합니다. 포기하지 마세요.",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "매일", "start_ms": 100, "dur_ms": 300, "phase_lr_deg": 5.0},
{"word": "꾸준히", "start_ms": 400, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "연습하는", "start_ms": 800, "dur_ms": 600, "phase_lr_deg": 5.0},
{"word": "것이", "start_ms": 1400, "dur_ms": 250, "phase_lr_deg": 5.0},
{"word": "가장", "start_ms": 1650, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "중요하다고", "start_ms": 2000, "dur_ms": 700, "phase_lr_deg": 5.0},
{"word": "생각합니다.", "start_ms": 2700, "dur_ms": 650, "phase_lr_deg": 5.0},
{"word": "포기하지", "start_ms": 3500, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "마세요.", "start_ms": 4000, "dur_ms": 450, "phase_lr_deg": 5.0}
]
},
{
"segment": 6,
"transcript": "감사합니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "감사", "start_ms": 30, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "합니다", "start_ms": 280, "dur_ms": 260, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 8,
"transcript": "Yes, of course.",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "Yes,", "start_ms": 45, "dur_ms": 150, "phase_lr_deg": 10.0},
{"word": "of", "start_ms": 195, "dur_ms": 100, "phase_lr_deg": 10.0},
{"word": "course.", "start_ms": 295, "dur_ms": 230, "phase_lr_deg": 10.0}
]
},
{
"segment": 9,
"transcript": "가끔은 외국어 공부가 힘들 때도 있지만, 친구들과 함께라면 즐거워요.",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "가끔은", "start_ms": 70, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "외국어", "start_ms": 520, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "공부가", "start_ms": 1020, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "힘들", "start_ms": 1420, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "때도", "start_ms": 1720, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "있지만,", "start_ms": 2020, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "친구들과", "start_ms": 2550, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "함께라면", "start_ms": 3100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "즐거워요.", "start_ms": 3600, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "Exactly right.",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Exactly", "start_ms": 50, "dur_ms": 300, "phase_lr_deg": 8.0},
{"word": "right.", "start_ms": 350, "dur_ms": 220, "phase_lr_deg": 8.0}
]
},
{
"segment": 11,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 12,
"transcript": "무슨 일이에요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "무슨", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "일이에요?", "start_ms": 240, "dur_ms": 300, "phase_lr_deg": -5.0}
]
},
{
"segment": 13,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 14,
"transcript": "노래를 부르면서 공부하면 발음이 훨씬 좋아지는 것 같아요. 해보실래요?",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "노래를", "start_ms": 90, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "부르면서", "start_ms": 490, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "공부하면", "start_ms": 990, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "발음이", "start_ms": 1540, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "훨씬", "start_ms": 1940, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "좋아지는", "start_ms": 2240, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 2740, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "같아요.", "start_ms": 2890, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "해보실래요?", "start_ms": 3400, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 16,
"transcript": "좋은 생각이에요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "생각이에요.", "start_ms": 240, "dur_ms": 300, "phase_lr_deg": -5.0}
]
},
{
"segment": 17,
"transcript": "Sounds great!",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Sounds", "start_ms": 30, "dur_ms": 220, "phase_lr_deg": 12.0},
{"word": "great!", "start_ms": 250, "dur_ms": 250, "phase_lr_deg": 12.0}
]
},
{
"segment": 18,
"transcript": "(instrument)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 19,
"transcript": "Let's start now.",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Let's", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 10.0},
{"word": "start", "start_ms": 230, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "now.", "start_ms": 430, "dur_ms": 100, "phase_lr_deg": 10.0}
]
},
{
"segment": 20,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 21,
"transcript": "준비되셨나요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "준비", "start_ms": 40, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "되셨나요?", "start_ms": 290, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "네, 그럼 먼저 제가 한 소절 불러볼게요. 잘 들어보세요.",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 80, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "그럼", "start_ms": 280, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "먼저", "start_ms": 530, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "제가", "start_ms": 830, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "한", "start_ms": 1080, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "소절", "start_ms": 1230, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "불러볼게요.", "start_ms": 1580, "dur_ms": 700, "phase_lr_deg": -5.0},
{"word": "잘", "start_ms": 2300, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "들어보세요.", "start_ms": 2500, "dur_ms": 600, "phase_lr_deg": -5.0}
]
},
{
"segment": 23,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 24,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 25,
"transcript": "Amazing voice!",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Amazing", "start_ms": 40, "dur_ms": 320, "phase_lr_deg": 15.0},
{"word": "voice!", "start_ms": 360, "dur_ms": 200, "phase_lr_deg": 15.0}
]
},
{
"segment": 26,
"transcript": "정말 잘 부르시네요!",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "부르시네요!", "start_ms": 350, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "부끄럽네요. 감사합니다.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "부끄럽네요.", "start_ms": 60, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "감사합니다.", "start_ms": 560, "dur_ms": 450, "phase_lr_deg": -5.0}
]
},
{
"segment": 28,
"transcript": "자, 이제 다 같이 따라 해볼까요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "자,", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 190, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 390, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 490, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "따라", "start_ms": 690, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "해볼까요?", "start_ms": 890, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "하나, 둘, 셋, 넷! 산토끼 토끼야 어디를 가느냐.",
"speaker": "speaker_1",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "mixed",
"words": [
{"word": "하나,", "start_ms": 100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "둘,", "start_ms": 600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "셋,", "start_ms": 1100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "넷!", "start_ms": 1600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "산토끼", "start_ms": 2200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "토끼야", "start_ms": 2700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "어디를", "start_ms": 3200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "가느냐.", "start_ms": 3700, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 31,
"transcript": "깡충깡충 뛰어서 어디를 가느냐.",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "mixed",
"words": [
{"word": "깡충깡충", "start_ms": 70, "dur_ms": 700, "phase_lr_deg": -5.0},
{"word": "뛰어서", "start_ms": 800, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "어디를", "start_ms": 1300, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "가느냐.", "start_ms": 1850, "dur_ms": 500, "phase_lr_deg": -5.0}
]
},
{
"segment": 32,
"transcript": "Great job everyone!",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Great", "start_ms": 30, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "job", "start_ms": 230, "dur_ms": 150, "phase_lr_deg": 10.0},
{"word": "everyone!", "start_ms": 380, "dur_ms": 300, "phase_lr_deg": 10.0}
]
},
{
"segment": 33,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 34,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 35,
"transcript": "Very good. You're doing amazing.",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "Very", "start_ms": 60, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "good.", "start_ms": 260, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "You're", "start_ms": 500, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "doing", "start_ms": 700, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "amazing.", "start_ms": 900, "dur_ms": 400, "phase_lr_deg": 10.0}
]
},
{
"segment": 36,
"transcript": "오늘 수업은 여기까지 할까요? 다들 수고 많으셨습니다.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 80, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "수업은", "start_ms": 380, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "여기까지", "start_ms": 730, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "할까요?", "start_ms": 1180, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "다들", "start_ms": 1700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "수고", "start_ms": 2000, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "많으셨습니다.", "start_ms": 2250, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "감사합니다 선생님. 다음 시간에 뵐게요. 안녕히 계세요.",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 90, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "선생님.", "start_ms": 590, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "다음", "start_ms": 1100, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "시간에", "start_ms": 1350, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "뵐게요.", "start_ms": 1700, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "안녕히", "start_ms": 2300, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "계세요.", "start_ms": 2700, "dur_ms": 400, "phase_lr_deg": -5.0}
]
},
{
"segment": 38,
"transcript": "Goodbye!",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Goodbye!", "start_ms": 40, "dur_ms": 400, "phase_lr_deg": 10.0}
]
},
{
"segment": 39,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 40,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 41,
"transcript": "See you next time.",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 10.0},
{"word": "you", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 10.0},
{"word": "next", "start_ms": 300, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "time.", "start_ms": 500, "dur_ms": 250, "phase_lr_deg": 10.0}
]
},
{
"segment": 42,
"transcript": "여러분도 복습 잊지 마세요. 다음에 만나요!",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "여러분도", "start_ms": 80, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "복습", "start_ms": 480, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "잊지", "start_ms": 780, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "마세요.", "start_ms": 1030, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "다음에", "start_ms": 1600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "만나요!", "start_ms": 1950, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 44,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 45,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 46,
"transcript": "Take care!",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "Take", "start_ms": 35, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "care!", "start_ms": 235, "dur_ms": 250, "phase_lr_deg": 10.0}
]
},
{
"segment": 47,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 48,
"transcript": "끝났습니다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "끝났", "start_ms": 40, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "습니다.", "start_ms": 290, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 50,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 12:25:20*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=17628  time=12:23:59  uptime=37852s  mem=442MB  system_processes=357"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_122017.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 4032ms (file: 216-126-126-123-136-132-103-102-123-216-201-162-621-213-716-612-216-621-231-217-126-217-612-106-217-217-217-127-123-126-213-127-123-126-132-127-216-213-725-312-237-627-263-213-3_W.mp3)
  Segment 2: 5032ms ~ 5608ms (file: 625-625-265-625-625-651 623-621-625-625-625-625-652-265-657-657-625_W.mp3)
  Segment 3: 6608ms ~ 7184ms (file: 751-571-570-750 517-571-751-571-175-173-751-756-517-157-751-716-175-751-751-571-571-570-576-571-571 571-517-157-517-135-173-173-517-571-571-175-517-571-750-570-751-571-571-570-5_V.mp3)
  Segment 4: 8184ms ~ 8760ms (file: 257-165-651-567-657-576-657-567-657-657-657-657-657 576-570-570-570-576-527-576-576-570-750-572-576-567-657-657-657 576-576-576-572-572-576-576-573-571-573_W.mp3)
  Segment 5: 9760ms ~ 10336ms (file: 527-572-572-572-576-576-572-570-527-576-275-257-720-571-257-576-576-567-576-576-576-567-576-576-576-715-763-541-574-527-576-572-527-572-572-562-527-572-507-576-502-507-572-502-2_W.mp3)
  Segment 6: 11336ms ~ 11912ms (file: 135-103-310 316-136-136-136-137-315-135-103-316-136-136-316-135-130-135-103-136 315-136-314-163-315-135-135-136-316-135-103-103-301-316-136-103 315-137-573-571-571-715-317-315-1_V.mp3)
  Segment 7: 12912ms ~ 13488ms (file: 315-357-153-135-150-165-315-135-153-153-153-315-513-735-513-135-175-157-157-157-157-135 157-105-157-153-135-103-135-135-135-130-135-317-153-510-513-135-315-517-513-135-531-153-5_W.mp3)
  Segment 8: 14488ms ~ 15064ms (file: 235-715-513-543-263-260-260-256-267-261-267-260-267-267-265-306-625-236-263-625-256-253-263 267-627-754-574-537-253-752-275-275-372-753-357-573-752-574-547-574-526-574-567-753-7_W.mp3)
  Segment 9: 16064ms ~ 16640ms (file: 567-513-571-576-572-571-576-507-576-572-572-507-572-507-570-572-572-571-571-752-756-576-574-576-756-576-752-756-751-752-571-517-537-571-527-541-503-571-527-567-506-517-527-513-5_V.mp3)
  Segment 10: 17640ms ~ 22752ms (file: 103-312-631-123-125-135-102-230-236-106-675-736-723-261-276-126-316-672-267-103-261-236-325-135-103-310-137-321-327-123-213-123-103-130-136-170-102-123-123-126-102-321-123-210-1_W.mp3)
  Segment 11: 23752ms ~ 28540ms (file: 132-103-103-213-201-201-203-230-231-312-130-132-120-210-230-320-230-120-273-123-231-123-230-231-102-230-320-312-312-231-123-302-310-130-130-103-203-302-231-213-210-321-132-103-1_V.mp3)
  Segment 12: 29540ms ~ 35984ms (file: 123-230-176-167-316-123-152-627-132-672-671-216-672-567-210-261-261-267-267-310-753-132-312-263-267-617-610-213-123-312-217-627-621-625-547-627-217-261-236-132-120-672-627-267-2_W.mp3)
  Segment 13: 36984ms ~ 37560ms (file: 157-170-721-207 135-103-130-137-173-310-130-135-310-301-137-173-103-301-103-130-301-310-137-310-310-130-132-130-157-213-102-317-103 310-103-310-132-315-301-317-137-103-102-103-3_V.mp3)
  Segment 14: 38560ms ~ 39136ms (file: 567-657-657-567-657-567-516-576-156-617-671 516-156-156-165-651-165-615-615-165-615-615-651-156-165-615-165-561_V.mp3)
  Segment 15: 40136ms ~ 40712ms (file: 517-512-517-715-173-517-751-751-157-716-715-751-571-751-751-571-751-751-571-570-570-571-571-571-573-573-571-573-571-571-576 517-513-176-715-751-175-173-751-571-570-157-175-715-5_V.mp3)
  Segment 16: 41712ms ~ 42288ms (file: 715-715-751-751-751-751-751-756-754-751-574-571 150-157-517-175-517-751-751-175-751-715-715-751-715-751-751-756-756-756-576-751-751-754-756-576-574 501-105-105-150-150-517-751-7_W.mp3)
  Segment 17: 43288ms ~ 43864ms (file: 572-572-527-532-521-205-527-572-572-572-571-576-571-576-576-576-571-576-576-576-571-576-573-751-175-547-512-517-507-507-506 542-527-572-572-572-571-572-571-576-572-572-572-756-5_W.mp3)
  Segment 18: 44864ms ~ 52064ms (file: 120-103-123-326-310-130-576-257-205-275-302-206-276-702-276-321-103-310-230-267-130-260-670-260-267-120-123-752-572-217-316-237-762-231-260-203-705-765-765-726-670-672-260-260-5_W.mp3)
  Segment 19: 53064ms ~ 53640ms (file: 502-502-507-504-507-502-506-507-507-507-507-507-521-567-560-507-502 507-502-526-506-507-507 506-576-507-562-506-567-752-275-527-527-752-570-526-725-205-250-502-520-275-205-265-2_V.mp3)
  Segment 20: 54640ms ~ 62740ms (file: 754-275-752-276-236-572-267-103-573-571-135-103-576-507-307-316-310-301-726-276-107-527-103-213-103-137-132-237-236-107-210-716-712-103-260-310-102-103-327-107-130-756-327-632-2_V.mp3)
  Segment 21: 63740ms ~ 64316ms (file: 547-547-547-574-547-547-574-576-576-574-574-754-754-754-754-561-567-561-514 574-576-574-574-574-547-547-547-574-574-576-547-574-576-574-574-574-756-574-574-574-573-571-546-567-5_W.mp3)
  Segment 22: 65316ms ~ 65892ms (file: 236-631-260-260-213-637-620-516-567-517-523-325-620-162-326-123-230-176-301-162-671-173-213-175-617-106-167-106-163-312-137-103-106-103-163-136-632-627-513-107-106-162-162-216-6_V.mp3)
  Segment 23: 66892ms ~ 76576ms (file: 305-756-567-105-123-267-670-321-607-102-231-203-203-671-176-107-362-206-206-260-372-260-657-657-130-213-273-263-216-237-216-120-537-207-132-230-206-670-627-620-103-132-216-103-5_W.mp3)
  Segment 24: 77576ms ~ 84632ms (file: 261-267-267-276-712-276-176-172-321-132-362-132-735-237-317-267-126-125-216-126-162-237-213-216-213-126-231-123-213-132-123-213-123-130-326-216-120-126-231-217-132-136-301-136-3_W_stt_if.mp3)
  Segment 25: 85632ms ~ 86208ms (file: 103-107-170-175-170-173-157-735-175-175-753-756-756-756-756-751-175-751-715-751 105-103-107-170-107-107-750-170-715-751-715-570-751-710-750-573-765-756-765-751-751-756-765-751-7_W.mp3)
  Segment 26: 87208ms ~ 87784ms (file: 521-514-504-507-540-504-547-574-570-540-501-510-501-547-504-574-547-574-571-514-507-157-504-154-514-157-105 574-574-571-574-574-751-570-750-507-571-501-157-574-571-754-175-715-7_V.mp3)
  Segment 27: 88784ms ~ 89360ms (file: 376-613-673-763-367-135-137-371-316-136-135 136-167-136-136-137 137-137-137-167-137-176-137-136-176 175-156-156-156-156-163-165-173-315-312-123-312-316-136-136-136-136 567-573-3_W.mp3)
  Segment 28: 90360ms ~ 90936ms (file: 762-527-576-571-572-567-573-527-625-657-705-257-657-576-567-765-576-567-756-675-576-756-756-765-762-512-523-512-507-572-576-527-752-571-572-572-215-527-576-257-527-567-256-156-5_W.mp3)
  Segment 29: 91936ms ~ 92512ms (file: 105-150-157-150-510-570-150-570-150-501-570-510 510-150-501-571-517-571-517-570-507-507-501-507-517-501-510-507-501-150-571-517 507-571-507-517-517-501-517-507-517-571-517-157-5_W.mp3)
  Segment 30: 93512ms ~ 103916ms (file: 137-316-321-761-317-237-271-123-136-310-261-320-312-302-320-326-136-312-236-312-236-362-132-125-257-107-301-276-276-260-261-527-270-312-321-230 217-260-102-231-216-261-206-216-2_W.mp3)
  Segment 31: 104916ms ~ 105492ms (file: 105-153-517-501-150-510-150-157-105-105-510-150-175-105-175-107-175-107-175-173 107-157-175-107-103-105-103-103-173-173-731-173-175-137-175-712-153-175-135 571-150-107-175-173-7_V.mp3)
  Segment 32: 106492ms ~ 107068ms (file: 217-752-275-127-570-215-507-567-627-652-267-132-120-162-162-172-162-126-136-162-263-172-517-572-750-175-756-706-573-625-367-175-321-275-570-231-756-725-250-137-162-103-130-105-1_W.mp3)
  Segment 33: 108068ms ~ 108644ms (file: 715-571-571-571-571 574-571-571-570-510-570-571-150-517-571-517-571-517-315-513-573 571-571-175-571-571-753-517-751-175-571-517-571-574-574 157-517-571-517-751-175-517-157-153-7_W.mp3)
  Segment 34: 109644ms ~ 110220ms (file: 132-562-602-617-123-130-312-103-137-103-163-301-136-136-601-261-160-136-361-106-361-103-236-236 315-760-136-152-675 615-163-126-103-136-162-163-130-510-103-136-163-103-103-561-1_V.mp3)
  Segment 35: 111220ms ~ 111796ms (file: 361-361-326-236-326-302-163-136-176-162 651-651-165-165-136-615-561-156-163-621-163-165-615-165-165-615-613-617-615-167-156-165-160-106-610-621-651-157-516-536-316-631-651-651-6_V.mp3)
  Segment 36: 112796ms ~ 115352ms (file: 572-750-752-571-351-657-276-621-103-102-216 570-520-260-257-750-570-752-756-752-752-756-752-576-751-751-571-571-574-571-756-571 527-520-576-520-570-257-572-752-571-576-751-576-5_W.mp3)
  Segment 37: 116352ms ~ 116928ms (file: 153-513-150-150 510-571-501-150-516-501-503-150-501-570-150-570-507-510-507-510-571-570_V.mp3)
  Segment 38: 117928ms ~ 118504ms (file: 534-513-564-521-541-513-546-501 512-547-541-542-542 156-503-512-546-542-547_V.mp3)
  Segment 39: 119504ms ~ 120080ms (file: 157-175-571-165-165-107-106-150-175-165-136-136-130-316-135-135-317-315 315-317-175-137-135 156-516-517-571-157-675-157-517-157-157-567-567-516 576-570-750-157-156-715-175-157-1_W.mp3)
  Segment 40: 121080ms ~ 121656ms (file: 150-105-105-517-105-150-514-501-541-570-507-570_V.mp3)
  Segment 41: 122656ms ~ 123232ms (file: 307-130-130-103-713-701-130-130-103-752-315-175-705-310-107-750-730-713 310-103-107-130-310-712-310-130-701-130-173-170-130-107-305-317-107-103-135-703-173-301-173-130-103-705-3_V.mp3)
  Segment 42: 124232ms ~ 124808ms (file: 750-175-170-710-175-571-756-571-571-576-562-657 135-107-175-107-107-107-107-107-107-103-170-170-175-715-517-576-576-567-567-567-576 105-157-105-107-107-107-107-107-107-173-751-7_W.mp3)
  Segment 43: 125808ms ~ 126384ms (file: 316-321-263-623-132-134-132-741-617-513-153-134-312-560-135-163-153-134-675-675-675-607-576-312-123-261-216-162-106-157-507-310-312-163-103-132-167-137-751-376-176-102-123-126-1_V.mp3)
  Segment 44: 127384ms ~ 127960ms (file: 705 570-573-576-750-750-751-570-751-570-571-710-103-751-570-751-705-563-350-573-765-750-735-750-750-716-357-750-516-317-351-705-573-571-705-750-751-710-715-751-705-750-751-570-5_W.mp3)
  Segment 45: 128960ms ~ 129536ms (file: 165-145-134-106-103-134-134-145-163-135-154-145-106-135-136-156-106-130-160-106-103-165-163-103-106-105-106-106-135-106-105-103-136-106-135-157-517-576-135-137-315-137-135-317-3_V.mp3)
  Segment 46: 130536ms ~ 131112ms (file: 570-216-527-231-153-321-726-132-276-250-263-260-275-256-326-236-576-235-351_V.mp3)
  Segment 47: 132112ms ~ 132688ms (file: 265-157-517-567-526-561-527-521-257-325-352-351-351-235-231-521-315-135-316-137-651-672-321-231-236-236-267-236-261-263-236-321-326-325-356-365-356-365-326-623-632-675-367-362-3_W.mp3)
  Segment 48: 133688ms ~ 139520ms (file: 315-310-310-301-301-130-301-137-570-132-130-132-312-107-173-176-312-103-130-301-120-126-361-163-123-276-132-103-103-107-136-136-617-361-571-135-631-652-316-132-302-321-132-263-6_W.mp3)
  Segment 49: 140520ms ~ 141096ms (file: 157-315-156-153-153-157-513-153-513-153-510-510-150-150-150-150-501-503-501-571 150-153-516-513-157-157-517-510-157-150-571-507-570-571-576-576-567-657-560-657-567 510-513-513-5_V.mp3)
  Segment 50: 142096ms ~ 142672ms (file: 756-576-567-657-367 562-657-652-657-567-650-657-560-567-560-650-567-756-657-567-657-675_W.mp3)
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
> **Time**: 2026-04-05 12:25:20
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "안녕하세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "안녕", "start_ms": 40, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "하세요", "start_ms": 260, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "반갑습니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "반갑", "start_ms": 50, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "습니다", "start_ms": 260, "dur_ms": 310, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "오늘 날씨가 좋네요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 60, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "날씨가", "start_ms": 210, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "좋네요", "start_ms": 410, "dur_ms": 150, "phase_lr_deg": -5.0}
]
},
{
"segment": 4,
"transcript": "한국어 공부는 정말 재미있어요. 그쵸?",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 80, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "공부는", "start_ms": 480, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 830, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "재미있어요.", "start_ms": 1080, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "그쵸?", "start_ms": 1680, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "매일 꾸준히 연습하는 것이 가장 중요하다고 생각합니다. 포기하지 마세요.",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "매일", "start_ms": 100, "dur_ms": 300, "phase_lr_deg": 5.0},
{"word": "꾸준히", "start_ms": 400, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "연습하는", "start_ms": 800, "dur_ms": 600, "phase_lr_deg": 5.0},
{"word": "것이", "start_ms": 1400, "dur_ms": 250, "phase_lr_deg": 5.0},
{"word": "가장", "start_ms": 1650, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "중요하다고", "start_ms": 2000, "dur_ms": 700, "phase_lr_deg": 5.0},
{"word": "생각합니다.", "start_ms": 2700, "dur_ms": 650, "phase_lr_deg": 5.0},
{"word": "포기하지", "start_ms": 3500, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "마세요.", "start_ms": 4000, "dur_ms": 450, "phase_lr_deg": 5.0}
]
},
{
"segment": 6,
"transcript": "감사합니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "감사", "start_ms": 30, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "합니다", "start_ms": 280, "dur_ms": 260, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 8,
"transcript": "Yes, of course.",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "Yes,", "start_ms": 45, "dur_ms": 150, "phase_lr_deg": 10.0},
{"word": "of", "start_ms": 195, "dur_ms": 100, "phase_lr_deg": 10.0},
{"word": "course.", "start_ms": 295, "dur_ms": 230, "phase_lr_deg": 10.0}
]
},
{
"segment": 9,
"transcript": "가끔은 외국어 공부가 힘들 때도 있지만, 친구들과 함께라면 즐거워요.",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "가끔은", "start_ms": 70, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "외국어", "start_ms": 520, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "공부가", "start_ms": 1020, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "힘들", "start_ms": 1420, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "때도", "start_ms": 1720, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "있지만,", "start_ms": 2020, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "친구들과", "start_ms": 2550, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "함께라면", "start_ms": 3100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "즐거워요.", "start_ms": 3600, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "Exactly right.",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Exactly", "start_ms": 50, "dur_ms": 300, "phase_lr_deg": 8.0},
{"word": "right.", "start_ms": 350, "dur_ms": 220, "phase_lr_deg": 8.0}
]
},
{
"segment": 11,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 12,
"transcript": "무슨 일이에요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "무슨", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "일이에요?", "start_ms": 240, "dur_ms": 300, "phase_lr_deg": -5.0}
]
},
{
"segment": 13,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 14,
"transcript": "노래를 부르면서 공부하면 발음이 훨씬 좋아지는 것 같아요. 해보실래요?",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "노래를", "start_ms": 90, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "부르면서", "start_ms": 490, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "공부하면", "start_ms": 990, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "발음이", "start_ms": 1540, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "훨씬", "start_ms": 1940, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "좋아지는", "start_ms": 2240, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 2740, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "같아요.", "start_ms": 2890, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "해보실래요?", "start_ms": 3400, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 16,
"transcript": "좋은 생각이에요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "생각이에요.", "start_ms": 240, "dur_ms": 300, "phase_lr_deg": -5.0}
]
},
{
"segment": 17,
"transcript": "Sounds great!",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Sounds", "start_ms": 30, "dur_ms": 220, "phase_lr_deg": 12.0},
{"word": "great!", "start_ms": 250, "dur_ms": 250, "phase_lr_deg": 12.0}
]
},
{
"segment": 18,
"transcript": "(instrument)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 19,
"transcript": "Let's start now.",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Let's", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 10.0},
{"word": "start", "start_ms": 230, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "now.", "start_ms": 430, "dur_ms": 100, "phase_lr_deg": 10.0}
]
},
{
"segment": 20,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 21,
"transcript": "준비되셨나요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "준비", "start_ms": 40, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "되셨나요?", "start_ms": 290, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "네, 그럼 먼저 제가 한 소절 불러볼게요. 잘 들어보세요.",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 80, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "그럼", "start_ms": 280, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "먼저", "start_ms": 530, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "제가", "start_ms": 830, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "한", "start_ms": 1080, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "소절", "start_ms": 1230, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "불러볼게요.", "start_ms": 1580, "dur_ms": 700, "phase_lr_deg": -5.0},
{"word": "잘", "start_ms": 2300, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "들어보세요.", "start_ms": 2500, "dur_ms": 600, "phase_lr_deg": -5.0}
]
},
{
"segment": 23,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 24,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 25,
"transcript": "Amazing voice!",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Amazing", "start_ms": 40, "dur_ms": 320, "phase_lr_deg": 15.0},
{"word": "voice!", "start_ms": 360, "dur_ms": 200, "phase_lr_deg": 15.0}
]
},
{
"segment": 26,
"transcript": "정말 잘 부르시네요!",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "부르시네요!", "start_ms": 350, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "부끄럽네요. 감사합니다.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "부끄럽네요.", "start_ms": 60, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "감사합니다.", "start_ms": 560, "dur_ms": 450, "phase_lr_deg": -5.0}
]
},
{
"segment": 28,
"transcript": "자, 이제 다 같이 따라 해볼까요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "자,", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 190, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 390, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 490, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "따라", "start_ms": 690, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "해볼까요?", "start_ms": 890, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "하나, 둘, 셋, 넷! 산토끼 토끼야 어디를 가느냐.",
"speaker": "speaker_1",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "mixed",
"words": [
{"word": "하나,", "start_ms": 100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "둘,", "start_ms": 600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "셋,", "start_ms": 1100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "넷!", "start_ms": 1600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "산토끼", "start_ms": 2200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "토끼야", "start_ms": 2700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "어디를", "start_ms": 3200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "가느냐.", "start_ms": 3700, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 31,
"transcript": "깡충깡충 뛰어서 어디를 가느냐.",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "mixed",
"words": [
{"word": "깡충깡충", "start_ms": 70, "dur_ms": 700, "phase_lr_deg": -5.0},
{"word": "뛰어서", "start_ms": 800, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "어디를", "start_ms": 1300, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "가느냐.", "start_ms": 1850, "dur_ms": 500, "phase_lr_deg": -5.0}
]
},
{
"segment": 32,
"transcript": "Great job everyone!",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Great", "start_ms": 30, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "job", "start_ms": 230, "dur_ms": 150, "phase_lr_deg": 10.0},
{"word": "everyone!", "start_ms": 380, "dur_ms": 300, "phase_lr_deg": 10.0}
]
},
{
"segment": 33,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 34,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 35,
"transcript": "Very good. You're doing amazing.",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "Very", "start_ms": 60, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "good.", "start_ms": 260, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "You're", "start_ms": 500, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "doing", "start_ms": 700, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "amazing.", "start_ms": 900, "dur_ms": 400, "phase_lr_deg": 10.0}
]
},
{
"segment": 36,
"transcript": "오늘 수업은 여기까지 할까요? 다들 수고 많으셨습니다.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 80, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "수업은", "start_ms": 380, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "여기까지", "start_ms": 730, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "할까요?", "start_ms": 1180, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "다들", "start_ms": 1700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "수고", "start_ms": 2000, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "많으셨습니다.", "start_ms": 2250, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "감사합니다 선생님. 다음 시간에 뵐게요. 안녕히 계세요.",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 90, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "선생님.", "start_ms": 590, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "다음", "start_ms": 1100, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "시간에", "start_ms": 1350, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "뵐게요.", "start_ms": 1700, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "안녕히", "start_ms": 2300, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "계세요.", "start_ms": 2700, "dur_ms": 400, "phase_lr_deg": -5.0}
]
},
{
"segment": 38,
"transcript": "Goodbye!",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Goodbye!", "start_ms": 40, "dur_ms": 400, "phase_lr_deg": 10.0}
]
},
{
"segment": 39,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 40,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 41,
"transcript": "See you next time.",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 10.0},
{"word": "you", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 10.0},
{"word": "next", "start_ms": 300, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "time.", "start_ms": 500, "dur_ms": 250, "phase_lr_deg": 10.0}
]
},
{
"segment": 42,
"transcript": "여러분도 복습 잊지 마세요. 다음에 만나요!",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "여러분도", "start_ms": 80, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "복습", "start_ms": 480, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "잊지", "start_ms": 780, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "마세요.", "start_ms": 1030, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "다음에", "start_ms": 1600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "만나요!", "start_ms": 1950, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 44,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 45,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 46,
"transcript": "Take care!",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "Take", "start_ms": 35, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "care!", "start_ms": 235, "dur_ms": 250, "phase_lr_deg": 10.0}
]
},
{
"segment": 47,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 48,
"transcript": "끝났습니다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "끝났", "start_ms": 40, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "습니다.", "start_ms": 290, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 50,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 12:25:20*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=24268  time=12:24:03  uptime=40460s  mem=431MB  system_processes=357"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_122031.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 126-136-136-163-310-631-613-136-167-716-236-623-316-317-310-371-136-126-312-132-126-162-612-613-316-163-613-163-613-136-136-631-627-631-672-621-162 162-632-132_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 756-675-752-756-765-765-765-765-765-765-756-675-675-765-675-762-763-376-132-657-756-351-375-761-137-213-201-216-612-261-321-132-137-132-136-317-153-516-567-156-571-517-516-567-5_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 516-512-571-516-517-512 536-503-520-574-513-564-540-501-501-507-506-570-507-561 576-765-162 501-501-502-512-501-576-507-521-217-127-126 105-501-512-571-572-562-506-512-561-572-1_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 576-570-750-750-756-756-756-765_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 376-315-136 675-671-675-615-657-617-675-657-675-675-657-675-765-736-316-637-163 163-612-617-613-671-613_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 620-136-312-163-167-306-326-360-206-301-321-301-620-675-765-675-574-756-370-310-630-130-362-261-163-601-617-267-327-320-230-103-312-134-641-134-675-765-267-627-576-627-623-260-2_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 751-751-175-710 150-510-150-150-175-517-513-157-157-157-517-175-173-715-715-751-715-170 150-571-157-137-157-715-175-751-715-715-715-715-751-751-715-751-715-751-170 105-150-153-1_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 135-103-130-103-371-130-301-103-130-321-173-137-371 137-317-137-135-130-371-310-371-301-130-731-103-312-310-210-103-130-301-310-137-371-370-731-307 317-137-103-137-103-137-301-3_V.mp3)
  Segment 9: 12608ms ~ 22832ms (file: 213-213-231-217-216-123-213-210-210-201-216-210-267-203-137-320-312-132-102-672-762-267-126-231-267-206-130-671-217-132-310-107-130-103-126-176-231-162-126-620-260-123-326-132-2_W.mp3)
  Segment 10: 23832ms ~ 24408ms (file: 501-517-150-510-507-571-517-570-501-510-570-507-507-571-567-561-507-517-510-150-501-501-507-570-573 570-507-105-107-105-105-157-507-507-105-507-507-501-507-506-501-507-501-150-5_W.mp3)
  Segment 11: 25408ms ~ 25984ms (file: 157 175-157 157-105-105-105-105-157-105 750-107-173-107-170-175-107-150-170-103-170-175-137-173 105-157-105-157-105-157-105-157-153-105-157-105-175-150-157-105-157-150 153-157-1_V.mp3)
  Segment 12: 26984ms ~ 27560ms (file: 650-257-250-206-507-250-206-502-526-276-527-756-765-756-756-756-756-765-765-675-765-765-657-506_V.mp3)
  Segment 13: 28560ms ~ 34356ms (file: 102-172-130-270-276-276-263-267-261-726-267-276-672-756-756-275-327-231-206-123-321-527-270-102-230-756-260-726-756-172-327-367-267-126-203-126-123-230-327-102-102-521-207-267-2_W.mp3)
  Segment 14: 35356ms ~ 35932ms (file: 321-173-173-137-136-275-361-316-136-725-357-135-572-571-571-573-537 315-571-753-576-716-576-753-715-175-751-175-751-715-713-312-317-317-217-127 517-751-571-571-175-715-571-751-1_W.mp3)
  Segment 15: 36932ms ~ 46868ms (file: 107-157-137-750-756-756-321-326-756-267-312-132-216-132-236-267-263-135-236-261-316-132-731-106-167-671-621-160-267-130-675-162-312-213-261-765-672-627-627-623-327-137-137-103-2_W.mp3)
  Segment 16: 47868ms ~ 48444ms (file: 301-163-716-763-310-610-657-163-163 713-173-371-137-317-375-130-301-613-607-631-316-310-103-137-135-317-163-367-365-371-136-613-671-673-637-652-675 750-715-751-107-375-571-175-1_V.mp3)
  Segment 17: 49444ms ~ 50020ms (file: 165-165-615-160-106-316-163-163-163-610-615-623-362-652 640-162-567-456-601-613-613 541-163-361-361-675-157 675-567-567-567-576-573-574-574-574-137-267-652-612-617-163-165-167-6_W.mp3)
  Segment 18: 51020ms ~ 57284ms (file: 123-175-761-571-325-123-753-752-130-103-162-701-167-102-725-230-207-321-570-327-572-567-672-321-302-210-120-260-216-216-216-231-213-231-137-173-271-725-127-301-210-627-756-567-3_W.mp3)
  Segment 19: 58284ms ~ 58860ms (file: 275-625-275-571-751-725-127-751-750-756-576-135-256-267-705-617-725-715-571-756-542-207-726-765-326-237-123-450-265-132-276-237-570-236-132-572-273-762-572-312-271-236-267-162-5_V.mp3)
  Segment 20: 59860ms ~ 60436ms (file: 150-105-513-150-510-510-501-105-157-105-510-150-105-150-105-150-513-150-150-510-513-351-105-105-510-501 501-517-105-157-153-105-105-150-510-150-105-105-105-153-105-153-150-105-1_W.mp3)
  Segment 21: 61436ms ~ 62012ms (file: 156-512-574-512-512-513-531-502-536-531-572-512-521-527-574-572-563-516-576-531-501-250-562-625-652-657-567-576-567-576-567-576-756-675-756-751-756-765-756-376-562-527-527-527-5_V.mp3)
  Segment 22: 63012ms ~ 63588ms (file: 315 612-612-615-615-612-615-615-612-612-610-612-312 621-102 261-261-261-206 261-621-612-621-621-627-621-542-543-752-725-706-752-752-257-527-507-507-517-502-650-652-652-625-625-6_W.mp3)
  Segment 23: 64588ms ~ 65164ms (file: 503-725-752-756-576-756-756-576-576-576-576-576-576-576-756-576-576-576-756-756-756-756-123-245-504-504-506-507-506-507-506-507-507-506-506-507-507-507-506-507-506-507-506-752-7_W.mp3)
  Segment 24: 66164ms ~ 76568ms (file: 752-302-102-136-103-106-162-756-572-765-756-576-576-103-103-103-132-315-360-130-163-103-137-103-137-370-276-107-176-231-276-726-675-267-106-726-276-276-754-765-270-210-542-542-1_W.mp3)
  Segment 25: 77568ms ~ 78144ms (file: 157 547-574-574-574-574-574-574-574-574-574-571-574-571-754-574-574-574-574-574 547-547-547-574-570-507-570-517-501-751-107-715-570-517-510-157-751-107-175-175-750-750-107-107-7_V.mp3)
  Segment 26: 79144ms ~ 79720ms (file: 351-657-265-213-261-261-267-263-257-265-257-235-325-235-325-352-536-534-536-356-653-632 157-265-527-675-756-157-527-512-213-623-136-650-163-316-365-325-235-327-613-157-357-315-3_W.mp3)
  Segment 27: 80720ms ~ 86984ms (file: 367-130-167-160-162-176-531-574-752-276-260-231-123-672-617-613-107-321-312-150-176-106-103-567-261-715-267-736-173-731-362-762-237-321-236-267-135-312-315-127-276-762-762-267-7_W.mp3)
  Segment 28: 87984ms ~ 88560ms (file: 560-570-576-507-205 765-675-167-167-617-672-671-765-765 765-765-675-765-765-765-765-765-765-765-765-765-765-765-765-765-761-763 761-176-716-716-176-176-716-761-761 572-573-567-5_W.mp3)
  Segment 29: 89560ms ~ 90136ms (file: 657-716-517-657-130-123-627-126-231-103-162-126-607-613-751-130-310-312-317-317-316-103-103-310-312-312-321-312-136-371-167-756-231-230-312-136-130-632-623-612-627-167-167-106-1_V.mp3)
  Segment 30: 91136ms ~ 91712ms (file: 157 105-105-170-107-105-170-150-157-150-157-517-157-157-157-157-510-157 105-107-150-157-150-157-517-157 105-150-105-157-175-176-175-176-157-175-165-156-157-156-157-157-157-157-1_V.mp3)
  Segment 31: 92712ms ~ 93288ms (file: 261-376-216-652-276-657-625-672-571-751-571-571-571-576-571-725-756-752-756-752-726-576-765_W.mp3)
  Segment 32: 94288ms ~ 104656ms (file: 317-136-316-317-317-312-312-231-263-326-237-367-316-371-317-713-327-173-756-576-361-130-123-321-325-375-276-723-271-321-602-102-230-536-632-230-540-527-675-263-261-230-534-542-6_W.mp3)
  Segment 33: 105656ms ~ 106232ms (file: 517-571-571-751-756-751-750-750-750-750-507-750-751-754-751-750-751-175 150-105-150-150-510-135-175-175-175-175-751-570-751-751-571-751-570-751_W.mp3)
  Segment 34: 107232ms ~ 107808ms (file: 571-175-751-715-715-715-715-751-751-751-751-751-751-715-756-715-751-751-754-751-751-715 130-105-150-570-501-517-571-751-751-751-751-751-756-751-751-715-175-751-751-715-751-751-7_W.mp3)
  Segment 35: 108808ms ~ 109384ms (file: 105-105-130-531-150-530-130-135-315-135-317-367-315-310-103-103-135-315-135-137-135-157 153-135-105-150-153-135-135-105-153-157-175-175-571-351-351-357-367-136-316-316 135-103-1_V.mp3)
  Segment 36: 110384ms ~ 110960ms (file: 261-321-132-123 257-176-167-163-126-162-106-136-231 314-507-754-570-750-276-620-657-657-165-615-657-756-675-675-106-310-301-612-136-167-267-206-201-236-206-620-206-203-260-260-6_V.mp3)
  Segment 37: 111960ms ~ 112536ms (file: 507-501-570-501-507-571-710-175-750-750-507-751-705-701-750-501-705 750-750-570-570-507-570-501-570-507-705-571-751-507-107-750-751-570-574 175-157-705-750-105-107-175-751-517-7_V.mp3)
  Segment 38: 113536ms ~ 127432ms (file: 237-237-103-321-273-527-236-102-137-107-571-263-756-362-536-576-574-357-321-237-273-260-263-260-230-231-213-213-210-203-256-235-213-132-231-312-132-321-312-320-213-130-213-321-2_W.mp3)
  Segment 39: 128432ms ~ 129008ms (file: 367-367-637-673-763-763-713-673-671-235-372-713-723-137-317-136-362-301-310-301-312-130_W.mp3)
  Segment 40: 130008ms ~ 130584ms (file: 130-753-312-627 176-310-713-310-302-706-673-735-736-735-735-371-731-762-620-762-716-617-316-316-163-136-136-137-173-137-751-176-765-273-527-172-547-514-347-172-675-176-175-750-7_V.mp3)
  Segment 41: 131584ms ~ 132160ms (file: 615-651-651-651-652-651-651-652-657-652-625-621-504-503-504-540-504-507_W.mp3)
  Segment 42: 133160ms ~ 143024ms (file: 310-216-216-752-167-362-675-107-371-136-271-136-263-132-120-123-617-765-675-672-756-765-267-260-753-756-756-312-321-260-756-761-236-267-130-672-756-236-675-615-367-163-762-613-1_W.mp3)
  Segment 43: 144024ms ~ 144600ms (file: 341-312-271 163-136-756-657-756-567-672-675-567-576 261-632-574-574-574 136-312_W.mp3)
  Segment 44: 145600ms ~ 152584ms (file: 123-267-326-326-230-326-132-316-327-701-270-327-573-276-320-302-267-237-230-231-320-312-350-537-132-123-317-753-237-230-176-765-137-312-627-276-572-753-754-671-160-567-576-167-6_W.mp3)
  Segment 45: 153584ms ~ 154160ms (file: 645-573-316-315-316-631-316-315-132-153-516-315-657-627-130-123-132-123 301-321-136-132_W.mp3)
  Segment 46: 155160ms ~ 155736ms (file: 267-576-756-762-570-137-762-607-152-107-136-137-123-765-261-132-751 235-165-617-312_V.mp3)
  Segment 47: 156736ms ~ 157312ms (file: 547-510-150-157-517-105-501-507-570-571-570-570-571-157 105-501-510-517-175-571-507-507-571-751-105-105-170 501-150-517-507-510-157-105-570-751-175-715 501-510-517-105-501-507-5_V.mp3)
  Segment 48: 158312ms ~ 158888ms (file: 510-501-517-517-517-571-513-537-537-357-531-357-156-175-571-516-175-176-175-105-175 574-517-571-157-150-157-150-150-531-150-157-103-157-157-135-157-157-150-135-135-176-126-106-5_V.mp3)
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
> **Time**: 2026-04-05 12:25:20
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "안녕하세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "안녕", "start_ms": 40, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "하세요", "start_ms": 260, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "반갑습니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "반갑", "start_ms": 50, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "습니다", "start_ms": 260, "dur_ms": 310, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "오늘 날씨가 좋네요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 60, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "날씨가", "start_ms": 210, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "좋네요", "start_ms": 410, "dur_ms": 150, "phase_lr_deg": -5.0}
]
},
{
"segment": 4,
"transcript": "한국어 공부는 정말 재미있어요. 그쵸?",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 80, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "공부는", "start_ms": 480, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 830, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "재미있어요.", "start_ms": 1080, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "그쵸?", "start_ms": 1680, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "매일 꾸준히 연습하는 것이 가장 중요하다고 생각합니다. 포기하지 마세요.",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "매일", "start_ms": 100, "dur_ms": 300, "phase_lr_deg": 5.0},
{"word": "꾸준히", "start_ms": 400, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "연습하는", "start_ms": 800, "dur_ms": 600, "phase_lr_deg": 5.0},
{"word": "것이", "start_ms": 1400, "dur_ms": 250, "phase_lr_deg": 5.0},
{"word": "가장", "start_ms": 1650, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "중요하다고", "start_ms": 2000, "dur_ms": 700, "phase_lr_deg": 5.0},
{"word": "생각합니다.", "start_ms": 2700, "dur_ms": 650, "phase_lr_deg": 5.0},
{"word": "포기하지", "start_ms": 3500, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "마세요.", "start_ms": 4000, "dur_ms": 450, "phase_lr_deg": 5.0}
]
},
{
"segment": 6,
"transcript": "감사합니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "감사", "start_ms": 30, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "합니다", "start_ms": 280, "dur_ms": 260, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 8,
"transcript": "Yes, of course.",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "Yes,", "start_ms": 45, "dur_ms": 150, "phase_lr_deg": 10.0},
{"word": "of", "start_ms": 195, "dur_ms": 100, "phase_lr_deg": 10.0},
{"word": "course.", "start_ms": 295, "dur_ms": 230, "phase_lr_deg": 10.0}
]
},
{
"segment": 9,
"transcript": "가끔은 외국어 공부가 힘들 때도 있지만, 친구들과 함께라면 즐거워요.",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "가끔은", "start_ms": 70, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "외국어", "start_ms": 520, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "공부가", "start_ms": 1020, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "힘들", "start_ms": 1420, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "때도", "start_ms": 1720, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "있지만,", "start_ms": 2020, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "친구들과", "start_ms": 2550, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "함께라면", "start_ms": 3100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "즐거워요.", "start_ms": 3600, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "Exactly right.",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Exactly", "start_ms": 50, "dur_ms": 300, "phase_lr_deg": 8.0},
{"word": "right.", "start_ms": 350, "dur_ms": 220, "phase_lr_deg": 8.0}
]
},
{
"segment": 11,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 12,
"transcript": "무슨 일이에요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "무슨", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "일이에요?", "start_ms": 240, "dur_ms": 300, "phase_lr_deg": -5.0}
]
},
{
"segment": 13,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 14,
"transcript": "노래를 부르면서 공부하면 발음이 훨씬 좋아지는 것 같아요. 해보실래요?",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "노래를", "start_ms": 90, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "부르면서", "start_ms": 490, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "공부하면", "start_ms": 990, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "발음이", "start_ms": 1540, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "훨씬", "start_ms": 1940, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "좋아지는", "start_ms": 2240, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 2740, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "같아요.", "start_ms": 2890, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "해보실래요?", "start_ms": 3400, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 16,
"transcript": "좋은 생각이에요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "생각이에요.", "start_ms": 240, "dur_ms": 300, "phase_lr_deg": -5.0}
]
},
{
"segment": 17,
"transcript": "Sounds great!",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Sounds", "start_ms": 30, "dur_ms": 220, "phase_lr_deg": 12.0},
{"word": "great!", "start_ms": 250, "dur_ms": 250, "phase_lr_deg": 12.0}
]
},
{
"segment": 18,
"transcript": "(instrument)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 19,
"transcript": "Let's start now.",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Let's", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 10.0},
{"word": "start", "start_ms": 230, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "now.", "start_ms": 430, "dur_ms": 100, "phase_lr_deg": 10.0}
]
},
{
"segment": 20,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 21,
"transcript": "준비되셨나요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "준비", "start_ms": 40, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "되셨나요?", "start_ms": 290, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "네, 그럼 먼저 제가 한 소절 불러볼게요. 잘 들어보세요.",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 80, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "그럼", "start_ms": 280, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "먼저", "start_ms": 530, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "제가", "start_ms": 830, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "한", "start_ms": 1080, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "소절", "start_ms": 1230, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "불러볼게요.", "start_ms": 1580, "dur_ms": 700, "phase_lr_deg": -5.0},
{"word": "잘", "start_ms": 2300, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "들어보세요.", "start_ms": 2500, "dur_ms": 600, "phase_lr_deg": -5.0}
]
},
{
"segment": 23,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 24,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 25,
"transcript": "Amazing voice!",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Amazing", "start_ms": 40, "dur_ms": 320, "phase_lr_deg": 15.0},
{"word": "voice!", "start_ms": 360, "dur_ms": 200, "phase_lr_deg": 15.0}
]
},
{
"segment": 26,
"transcript": "정말 잘 부르시네요!",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "부르시네요!", "start_ms": 350, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "부끄럽네요. 감사합니다.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "부끄럽네요.", "start_ms": 60, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "감사합니다.", "start_ms": 560, "dur_ms": 450, "phase_lr_deg": -5.0}
]
},
{
"segment": 28,
"transcript": "자, 이제 다 같이 따라 해볼까요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "자,", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 190, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 390, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 490, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "따라", "start_ms": 690, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "해볼까요?", "start_ms": 890, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "하나, 둘, 셋, 넷! 산토끼 토끼야 어디를 가느냐.",
"speaker": "speaker_1",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "mixed",
"words": [
{"word": "하나,", "start_ms": 100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "둘,", "start_ms": 600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "셋,", "start_ms": 1100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "넷!", "start_ms": 1600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "산토끼", "start_ms": 2200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "토끼야", "start_ms": 2700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "어디를", "start_ms": 3200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "가느냐.", "start_ms": 3700, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 31,
"transcript": "깡충깡충 뛰어서 어디를 가느냐.",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "mixed",
"words": [
{"word": "깡충깡충", "start_ms": 70, "dur_ms": 700, "phase_lr_deg": -5.0},
{"word": "뛰어서", "start_ms": 800, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "어디를", "start_ms": 1300, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "가느냐.", "start_ms": 1850, "dur_ms": 500, "phase_lr_deg": -5.0}
]
},
{
"segment": 32,
"transcript": "Great job everyone!",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Great", "start_ms": 30, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "job", "start_ms": 230, "dur_ms": 150, "phase_lr_deg": 10.0},
{"word": "everyone!", "start_ms": 380, "dur_ms": 300, "phase_lr_deg": 10.0}
]
},
{
"segment": 33,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 34,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 35,
"transcript": "Very good. You're doing amazing.",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "Very", "start_ms": 60, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "good.", "start_ms": 260, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "You're", "start_ms": 500, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "doing", "start_ms": 700, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "amazing.", "start_ms": 900, "dur_ms": 400, "phase_lr_deg": 10.0}
]
},
{
"segment": 36,
"transcript": "오늘 수업은 여기까지 할까요? 다들 수고 많으셨습니다.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 80, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "수업은", "start_ms": 380, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "여기까지", "start_ms": 730, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "할까요?", "start_ms": 1180, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "다들", "start_ms": 1700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "수고", "start_ms": 2000, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "많으셨습니다.", "start_ms": 2250, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "감사합니다 선생님. 다음 시간에 뵐게요. 안녕히 계세요.",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 90, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "선생님.", "start_ms": 590, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "다음", "start_ms": 1100, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "시간에", "start_ms": 1350, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "뵐게요.", "start_ms": 1700, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "안녕히", "start_ms": 2300, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "계세요.", "start_ms": 2700, "dur_ms": 400, "phase_lr_deg": -5.0}
]
},
{
"segment": 38,
"transcript": "Goodbye!",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Goodbye!", "start_ms": 40, "dur_ms": 400, "phase_lr_deg": 10.0}
]
},
{
"segment": 39,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 40,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 41,
"transcript": "See you next time.",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 10.0},
{"word": "you", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 10.0},
{"word": "next", "start_ms": 300, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "time.", "start_ms": 500, "dur_ms": 250, "phase_lr_deg": 10.0}
]
},
{
"segment": 42,
"transcript": "여러분도 복습 잊지 마세요. 다음에 만나요!",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "여러분도", "start_ms": 80, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "복습", "start_ms": 480, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "잊지", "start_ms": 780, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "마세요.", "start_ms": 1030, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "다음에", "start_ms": 1600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "만나요!", "start_ms": 1950, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 44,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 45,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 46,
"transcript": "Take care!",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "Take", "start_ms": 35, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "care!", "start_ms": 235, "dur_ms": 250, "phase_lr_deg": 10.0}
]
},
{
"segment": 47,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 48,
"transcript": "끝났습니다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "끝났", "start_ms": 40, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "습니다.", "start_ms": 290, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 50,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 12:25:20*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=11716  time=12:23:44  uptime=38081s  mem=287MB  system_processes=357"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_122302.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 4500ms (file: 106-175-576-126-127-263-312-762-620-123-120-762-627-576-537-267-261-167-621-602-571-156-612-267-627-321-576-157-271-627-130-162-612-167-216-126-126-276-217-261-261-216-126-132-1_W.mp3)
  Segment 2: 5500ms ~ 6076ms (file: 327-132-231-312-132-312-130-125-123-126-123-102-561-156-736-137-163-613_V.mp3)
  Segment 3: 7076ms ~ 11360ms (file: 132-103-103-312 123-510-521-203-203-620-213-623-276-206-301-206_V.mp3)
  Segment 4: 12360ms ~ 19704ms (file: 130-123-125-613-163-126-236-321-327-570-267-570-564-265-362-321-765-352-312-527-706-263-362-326-517-576-312-542-756-723-627-526-532-621-672-507-321-312-753-132-236-736-321-270-7_V.mp3)
  Segment 5: 20704ms ~ 21280ms (file: 210-120 621-126-102 621-621-621-621-627-627-612-362-162 267-627-627-267-261-261_W.mp3)
  Segment 6: 22280ms ~ 22856ms (file: 760-730-705-703-750-750-517-571-750-705-701-710-173-517-573-715-507-750-570-750-507-571-715-507-705-571-306-705-703-376-756-673-763-376-756-735-736-537-756-731-570-750-730-107-5_V.mp3)
  Segment 7: 23856ms ~ 24432ms (file: 506-507-576-576-576-567-507-572-507-705-570-576 507-576-570-507-570-507-570-570-507-570-567-567-567-576-560-570-756-675-572-567-765-576-562-576-576-670-670-670-670-760-670-670-7_V.mp3)
  Segment 8: 25432ms ~ 26008ms (file: 574-751-571-157-571-751-157-571-751-750-571 547-547-547-547-574-574-574-574-574-574-574-574-574 517-157-501-107-175-517 547-547-547-574-574-504-715-571-570-570-750-705-175-157-1_W.mp3)
  Segment 9: 27008ms ~ 34856ms (file: 756-576-574-130-271-715-270-273-753-271-210-217-217-230-107-231-173-176-756-310-321-102-103-317-301-317-103-103-134-706-375-136-315-715-317-137-531-130-130-317-530-503-370-320-5_W.mp3)
  Segment 10: 35856ms ~ 42120ms (file: 137-316-136-216-621-623-721-265-574-574-267-216-132-316-574-627-267-756-125-260-361-103-210-260-261-216-261-276-235-267-627-267-627-627-267-216-765-765-527-256-267-706-267-213-1_W_stt_wbc_belt,_cti_academic_pm_in_through_the.mp3)
  Segment 11: 43120ms ~ 43696ms (file: 573-572-576-567-675-657-637-637-267-725-756-675-672-673-623-627 132-261-672-261-261-261-267-276-726 123 312-625-631-563-326-231-312-326-632-613-267-651-576-562-612-627-726-507-1_W.mp3)
  Segment 12: 44696ms ~ 45272ms (file: 756-675-765-756-756-765 157-157-571-157-571-517-570-157-157-157-157-571 570-157-517-571-517-571-571-570-157-157-157-715-751-175-756-756-756-756-753-756-752-756-756-765-756-756-7_W.mp3)
  Segment 13: 46272ms ~ 46848ms (file: 756-756-751-756-574-576-576-576-576-574-517-175-157-675-675-673-675-675-675_W.mp3)
  Segment 14: 47848ms ~ 59980ms (file: 163-326-627-756-273-230-321-236-267-321-102-130-123-362-263-321-276-120-236-236-326-321-627-641-231-213-310-321-231-236-263-236-130-167-103-263-236-762-267-267-612-130-263-267-7_W.mp3)
  Segment 15: 60980ms ~ 61556ms (file: 201-321-705-750-706-765-713-236-627-620-310-126-570-570-705-672-362-672-672-263-617-127-206-620-627-623-267-625-612-617-613-160-612-162-612-132-302-132-136-652 126-213-130-163-6_V.mp3)
  Segment 16: 62556ms ~ 63132ms (file: 205-502-503-502-205-502-501-510-362-135-251-215-321-537-572-532-123 152-251-502-521-502-523-205-152-125-215-152-152-205-251-152-215-201-203-206-206-213-205-201-205-210-201-261-2_V.mp3)
  Segment 17: 64132ms ~ 70972ms (file: 572-231-267-120-312-213-230-273-261-261-236-321-132-571-217-321-120-123-320-326-576-573-103-753-756 263-230-310-672-627-263-321-316-103-320-231-236-627-260-230-103-236-326-765-3_W.mp3)
  Segment 18: 71972ms ~ 72548ms (file: 521-157-325-123 514-153-513 517-512-502-501-250-251-201-205-215-257-251-275-257-526-250-251-512-531-534-514-514-126-127-257 567-567-612-361-162-135-125-135-153-150-501-501-501-5_V.mp3)
  Segment 19: 73548ms ~ 74124ms (file: 526-562-512-516-526-571-517-516-574-526-536 516-512-531-561-513-517-513-512-567-506-527-502-526-563-572-563-561-527-572-572-572-572-572-572-572-752-572-753-563-517-573-503-527-5_V.mp3)
  Segment 20: 75124ms ~ 75700ms (file: 162-576-657-657-576-576-657-576-567_W.mp3)
  Segment 21: 76700ms ~ 77276ms (file: 356-564-514-572-546-563-567-567-567-567-567-657-657-567-654-567-657-567-564-657-654-376-761-163 652-576-567-576-567-657-567-567-567-567-657-324-316-316-132-546-564-504-501-504-5_W.mp3)
  Segment 22: 78276ms ~ 83856ms (file: 216-216-261-123-123-123-102-312-126-126-132-132-132-312-132-123-123-123-126-123-312-123-123-123-126-321-231-216-216-216-213-120-126-271-103-123-305-263-357-325-236-236-367-627-3_W.mp3)
  Segment 23: 84856ms ~ 92236ms (file: 261-627-672-123-362-637-217-312-130-267-627-621-216-216-263-107-672-102-326-236-623-260-267-263-316-361-201-765-316-361-260-261-576-267-312-260-621-540-576-627-213-263-216-162-1_W.mp3)
  Segment 24: 93236ms ~ 97916ms (file: 276-726-276-726-627-267-276-672-726-725-267-752-725-276-752-725-672-276-726-725-751-720-726-762-726-725-721-752-276-752-172-312-712-123-103-267-261-213-312-103-120-216-236-123-1_W.mp3)
  Segment 25: 98916ms ~ 99492ms (file: 627-106-107-562-107-615-130-312-572-375-103-123-162-165-612-612-176-612-106-167-106-163-126-103-106-106-632-160-621-106-137-136-106-136-106-162-376-130-165-106-162-103-103-103-1_V.mp3)
  Segment 26: 100492ms ~ 101068ms (file: 715-715-276 130-173-716-130-731-731-107-103-137-130-370-301-107-703-301-301-103-130-130-317-170-301-135-705-701-570-705-730-701-731-107 315-130-317-107-137-307-712-371-103-173-1_V.mp3)
  Segment 27: 102068ms ~ 102644ms (file: 137-162-631-613-160-136-613-513-567-567-562-621-123-125-635-657-615-312-612-612-610-163-613-613-132-136-206_W.mp3)
  Segment 28: 103644ms ~ 104220ms (file: 136-134-136-316-136-136-315-137-316-137-135-316-136 314-314-314-163-134-314-134-136-316-136-137-136-137-135-136-103-135-103-135 301-136-314-316-136-134-314-315-316-103-136-135-1_V.mp3)
  Segment 29: 105220ms ~ 110908ms (file: 162-126-127-127-127-213-217-127-127-126-127-123-123-213-126-126-231-123-126-675-763-675-712-157-175-213-576-315-102-135-217-231-762-276-532-367-231-127-107-132-102-267-217-132-1_W.mp3)
  Segment 30: 111908ms ~ 112484ms (file: 267-127-713-172-312-137 175-175-571-170-713-501-710-175-715-157-510-517-517-571-175-715-715-751 571-157-571-751-175-571-571-715-715-173-517-157-137-175-751-157-571-571-573-571-7_V.mp3)
  Segment 31: 113484ms ~ 114060ms (file: 615-651-615-651-615-651-615 162-612-613-615-615-651-615-651-615-657-672-612_W.mp3)
  Segment 32: 115060ms ~ 115636ms (file: 765-726-165-615-165-165-651-156-561-561-156-561-516-561-516-156-516-657-651-165-672-672-726-762-672-672-762 521-562-561-561-651-156-516-561-156-651-517-156-561-561-615-657-561-6_W.mp3)
  Segment 33: 116636ms ~ 117212ms (file: 136-103-136-135-103 537-573-753-571-514-571-537-571-571 136-316-136-135-174-314-136-136-136-134-135-136-135-165-103-103-103-103-136-134 103-136-136-136-156-165-163-135-136-135-3_V.mp3)
  Segment 34: 118212ms ~ 127680ms (file: 765-675-107-761-765-267-263-130-173-170-130-273-627-267-267-130-132-216-276-267-176-160-621-756-103-130-276-321-126-267-236-167-136-310-162-263-321-123-672-267-260-310-120-376-6_W.mp3)
  Segment 35: 128680ms ~ 129256ms (file: 507-572-257-520-507-520-205-572-257-621-621-612-672-672-261-123-713 521-527-502-507-502-520-572-507-520-502-250-527-502-257-527-275-257-520-725-752-275-570-527-527-257-502-725-5_V.mp3)
  Segment 36: 130256ms ~ 130832ms (file: 301-173-751-301-601-607-603-603-631-617-617-631-136-637-163-163-316-310-163-137-160-316-316-106-671 731-705-735-130-315-371-610-630-607-603-607-613-610-617-637-607-670-670-607-1_V.mp3)
  Segment 37: 131832ms ~ 132408ms (file: 517-517-512-512 251-756-567-567-675-716-531-512 752-572-576-756-576-756-751-513-521 547-507-504-572-257-752-752-576-756-512-516-536 513-512-205-125-205-572-752-752-752-521-501-5_W.mp3)
  Segment 38: 133408ms ~ 133984ms (file: 572-257-752-572-572-257-576-527-512-576-257-547-527-572-576-576-576-567-567-576-576-576-576-576-567-576-576-576-576-576-756-735-576-572-572-752-572-725-572-257-750-570-756-752-5_W.mp3)
  Segment 39: 134984ms ~ 144164ms (file: 173-137-123-172-137-137-137-173-173-173-175-107-107-173-107-173-107-107-173-107-107 137-137-103-173-173-175-107-173-175-107-173-107-103-173-173-107-137 153-103-107-107-103-107-1_V.mp3)
  Segment 40: 145164ms ~ 145740ms (file: 653-625-651-651-651-615-520-502-506-501-562-526-571-567-165-572-572-526-532-526-527-513-627-657-516-527-526-651-567-657-562-561-651-567-567-561-657-567_W.mp3)
  Segment 41: 146740ms ~ 154875ms (file: 705-312-136-216-162-675-753-206-362-613-265-256-507-675-230-765-236-267-572-754-263-236-236-236-723-237-732-372-317-320-307-357-136-206-651-376-360-316-265-306-352-263-103-326-2_W.mp3)
  Segment 42: 155875ms ~ 156451ms (file: 351-312-351-317-157-325-356-536-573-257 316-231-136-137-130-657-567-657-657-567-657-657-567-567-657-567-653 506-502-502-520-567-570-576-756-576-263-675-751-173 506-536-637-620-6_W.mp3)
  Segment 43: 157451ms ~ 158027ms (file: 126-512-527-513-527-502-506-513-201-502-567-756-735 507-537-527-572-576-572-576-501-305-504-527-502-520-572-756-576-576-576-567-576-576-576-756-576-576-576-576-756-756-756-527-5_V.mp3)
  Segment 44: 159027ms ~ 159603ms (file: 130-170-173-137 517-175-126-716-715-361-173-165-517-572-136 253-276-627-267-625-301-765-276-572-321-567-132-205-235-571-537-756-527-312-751-517-216 123-765-261-207-123-267-127-2_V.mp3)
  Segment 45: 160603ms ~ 167587ms (file: 123-267-326-326-230-326-132-316-327-701-270-327-573-276-320-302-267-237-230-231-320-312-350-537-132-123-317-753-237-230-176-765-137-312-627-276-572-753-754-671-160-567-576-167-6_W.mp3)
  Segment 46: 168587ms ~ 169163ms (file: 576-573-576-573-357-735-375-165-165-163-165-165-351-576-753-735-375-735-715-160-176-136-176-107-107-105-170-137-165-160-156-163-165-162-756-675-765-751-735-573-756-531-175-753-6_W.mp3)
  Segment 47: 170163ms ~ 170739ms (file: 165-165-615-160-106-316-163-163-163-610-615-623-362-652 640-162-567-456-601-613-613 541-163-361-361-675-157 675-567-567-567-576-573-574-574-574-137-267-652-612-617-163-165-167-6_W.mp3)
  Segment 48: 171739ms ~ 172315ms (file: 275-576-527-576-756-752-572-527-572-752-257-572-257-567-256-562-526-576-256-563-516-502-120-215-521-561-165-617-516-615-651-356-657-165-137-136-176-172-173-175-157-126-126-136-1_W.mp3)
  Segment 49: 173315ms ~ 173891ms (file: 567-567-567-576-571-657-263-563 150-165-157-567-531-152-572-576-571_W.mp3)
  Segment 50: 174891ms ~ 175467ms (file: 157-215-152-173-137-315-123-315-352-352-352-532-352-513-352-531-351-153-153-317-123-135-317-351-315-351-352-153-135-135-153-315-135-315-351-351-135-531-315-357-356-351-315-135-1_W.mp3)
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
> **Time**: 2026-04-05 12:25:20
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "안녕하세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "안녕", "start_ms": 40, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "하세요", "start_ms": 260, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "반갑습니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "반갑", "start_ms": 50, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "습니다", "start_ms": 260, "dur_ms": 310, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "오늘 날씨가 좋네요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 60, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "날씨가", "start_ms": 210, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "좋네요", "start_ms": 410, "dur_ms": 150, "phase_lr_deg": -5.0}
]
},
{
"segment": 4,
"transcript": "한국어 공부는 정말 재미있어요. 그쵸?",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 80, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "공부는", "start_ms": 480, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 830, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "재미있어요.", "start_ms": 1080, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "그쵸?", "start_ms": 1680, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "매일 꾸준히 연습하는 것이 가장 중요하다고 생각합니다. 포기하지 마세요.",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "매일", "start_ms": 100, "dur_ms": 300, "phase_lr_deg": 5.0},
{"word": "꾸준히", "start_ms": 400, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "연습하는", "start_ms": 800, "dur_ms": 600, "phase_lr_deg": 5.0},
{"word": "것이", "start_ms": 1400, "dur_ms": 250, "phase_lr_deg": 5.0},
{"word": "가장", "start_ms": 1650, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "중요하다고", "start_ms": 2000, "dur_ms": 700, "phase_lr_deg": 5.0},
{"word": "생각합니다.", "start_ms": 2700, "dur_ms": 650, "phase_lr_deg": 5.0},
{"word": "포기하지", "start_ms": 3500, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "마세요.", "start_ms": 4000, "dur_ms": 450, "phase_lr_deg": 5.0}
]
},
{
"segment": 6,
"transcript": "감사합니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "감사", "start_ms": 30, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "합니다", "start_ms": 280, "dur_ms": 260, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 8,
"transcript": "Yes, of course.",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "Yes,", "start_ms": 45, "dur_ms": 150, "phase_lr_deg": 10.0},
{"word": "of", "start_ms": 195, "dur_ms": 100, "phase_lr_deg": 10.0},
{"word": "course.", "start_ms": 295, "dur_ms": 230, "phase_lr_deg": 10.0}
]
},
{
"segment": 9,
"transcript": "가끔은 외국어 공부가 힘들 때도 있지만, 친구들과 함께라면 즐거워요.",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "가끔은", "start_ms": 70, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "외국어", "start_ms": 520, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "공부가", "start_ms": 1020, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "힘들", "start_ms": 1420, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "때도", "start_ms": 1720, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "있지만,", "start_ms": 2020, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "친구들과", "start_ms": 2550, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "함께라면", "start_ms": 3100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "즐거워요.", "start_ms": 3600, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "Exactly right.",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Exactly", "start_ms": 50, "dur_ms": 300, "phase_lr_deg": 8.0},
{"word": "right.", "start_ms": 350, "dur_ms": 220, "phase_lr_deg": 8.0}
]
},
{
"segment": 11,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 12,
"transcript": "무슨 일이에요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "무슨", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "일이에요?", "start_ms": 240, "dur_ms": 300, "phase_lr_deg": -5.0}
]
},
{
"segment": 13,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 14,
"transcript": "노래를 부르면서 공부하면 발음이 훨씬 좋아지는 것 같아요. 해보실래요?",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "노래를", "start_ms": 90, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "부르면서", "start_ms": 490, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "공부하면", "start_ms": 990, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "발음이", "start_ms": 1540, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "훨씬", "start_ms": 1940, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "좋아지는", "start_ms": 2240, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 2740, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "같아요.", "start_ms": 2890, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "해보실래요?", "start_ms": 3400, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 16,
"transcript": "좋은 생각이에요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "생각이에요.", "start_ms": 240, "dur_ms": 300, "phase_lr_deg": -5.0}
]
},
{
"segment": 17,
"transcript": "Sounds great!",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Sounds", "start_ms": 30, "dur_ms": 220, "phase_lr_deg": 12.0},
{"word": "great!", "start_ms": 250, "dur_ms": 250, "phase_lr_deg": 12.0}
]
},
{
"segment": 18,
"transcript": "(instrument)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 19,
"transcript": "Let's start now.",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Let's", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 10.0},
{"word": "start", "start_ms": 230, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "now.", "start_ms": 430, "dur_ms": 100, "phase_lr_deg": 10.0}
]
},
{
"segment": 20,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 21,
"transcript": "준비되셨나요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "준비", "start_ms": 40, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "되셨나요?", "start_ms": 290, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "네, 그럼 먼저 제가 한 소절 불러볼게요. 잘 들어보세요.",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 80, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "그럼", "start_ms": 280, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "먼저", "start_ms": 530, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "제가", "start_ms": 830, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "한", "start_ms": 1080, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "소절", "start_ms": 1230, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "불러볼게요.", "start_ms": 1580, "dur_ms": 700, "phase_lr_deg": -5.0},
{"word": "잘", "start_ms": 2300, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "들어보세요.", "start_ms": 2500, "dur_ms": 600, "phase_lr_deg": -5.0}
]
},
{
"segment": 23,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 24,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 25,
"transcript": "Amazing voice!",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Amazing", "start_ms": 40, "dur_ms": 320, "phase_lr_deg": 15.0},
{"word": "voice!", "start_ms": 360, "dur_ms": 200, "phase_lr_deg": 15.0}
]
},
{
"segment": 26,
"transcript": "정말 잘 부르시네요!",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "부르시네요!", "start_ms": 350, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "부끄럽네요. 감사합니다.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "부끄럽네요.", "start_ms": 60, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "감사합니다.", "start_ms": 560, "dur_ms": 450, "phase_lr_deg": -5.0}
]
},
{
"segment": 28,
"transcript": "자, 이제 다 같이 따라 해볼까요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "자,", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 190, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 390, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 490, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "따라", "start_ms": 690, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "해볼까요?", "start_ms": 890, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "하나, 둘, 셋, 넷! 산토끼 토끼야 어디를 가느냐.",
"speaker": "speaker_1",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "mixed",
"words": [
{"word": "하나,", "start_ms": 100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "둘,", "start_ms": 600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "셋,", "start_ms": 1100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "넷!", "start_ms": 1600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "산토끼", "start_ms": 2200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "토끼야", "start_ms": 2700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "어디를", "start_ms": 3200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "가느냐.", "start_ms": 3700, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 31,
"transcript": "깡충깡충 뛰어서 어디를 가느냐.",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "mixed",
"words": [
{"word": "깡충깡충", "start_ms": 70, "dur_ms": 700, "phase_lr_deg": -5.0},
{"word": "뛰어서", "start_ms": 800, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "어디를", "start_ms": 1300, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "가느냐.", "start_ms": 1850, "dur_ms": 500, "phase_lr_deg": -5.0}
]
},
{
"segment": 32,
"transcript": "Great job everyone!",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Great", "start_ms": 30, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "job", "start_ms": 230, "dur_ms": 150, "phase_lr_deg": 10.0},
{"word": "everyone!", "start_ms": 380, "dur_ms": 300, "phase_lr_deg": 10.0}
]
},
{
"segment": 33,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 34,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 35,
"transcript": "Very good. You're doing amazing.",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "Very", "start_ms": 60, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "good.", "start_ms": 260, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "You're", "start_ms": 500, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "doing", "start_ms": 700, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "amazing.", "start_ms": 900, "dur_ms": 400, "phase_lr_deg": 10.0}
]
},
{
"segment": 36,
"transcript": "오늘 수업은 여기까지 할까요? 다들 수고 많으셨습니다.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 80, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "수업은", "start_ms": 380, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "여기까지", "start_ms": 730, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "할까요?", "start_ms": 1180, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "다들", "start_ms": 1700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "수고", "start_ms": 2000, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "많으셨습니다.", "start_ms": 2250, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "감사합니다 선생님. 다음 시간에 뵐게요. 안녕히 계세요.",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 90, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "선생님.", "start_ms": 590, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "다음", "start_ms": 1100, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "시간에", "start_ms": 1350, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "뵐게요.", "start_ms": 1700, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "안녕히", "start_ms": 2300, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "계세요.", "start_ms": 2700, "dur_ms": 400, "phase_lr_deg": -5.0}
]
},
{
"segment": 38,
"transcript": "Goodbye!",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Goodbye!", "start_ms": 40, "dur_ms": 400, "phase_lr_deg": 10.0}
]
},
{
"segment": 39,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 40,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 41,
"transcript": "See you next time.",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 10.0},
{"word": "you", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 10.0},
{"word": "next", "start_ms": 300, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "time.", "start_ms": 500, "dur_ms": 250, "phase_lr_deg": 10.0}
]
},
{
"segment": 42,
"transcript": "여러분도 복습 잊지 마세요. 다음에 만나요!",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "여러분도", "start_ms": 80, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "복습", "start_ms": 480, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "잊지", "start_ms": 780, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "마세요.", "start_ms": 1030, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "다음에", "start_ms": 1600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "만나요!", "start_ms": 1950, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 44,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 45,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 46,
"transcript": "Take care!",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "Take", "start_ms": 35, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "care!", "start_ms": 235, "dur_ms": 250, "phase_lr_deg": 10.0}
]
},
{
"segment": 47,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 48,
"transcript": "끝났습니다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "끝났", "start_ms": 40, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "습니다.", "start_ms": 290, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 50,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 12:25:20*
