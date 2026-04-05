# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=1700  time=06:42:34  uptime=7621s  mem=1905MB  system_processes=351"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_063822.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 632-106-713 574-236-207-273 574-657-635-312-162-270-572-631-267-321-263-267-752-572-572-265-751-752-325-326-756-726-756-756-507-675-271-750-765-271-625-567-135-351-317-317-216-7_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 315-137-136-134-132-137-712-673-672-176-134-132-213-261-726-726-271-137-132-136-106-612-621-672-230-623-217-126-627-136-312-162-126-136-123-136-132-143-132-143-312-675-670-276-7_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 263-271-237-271-216-237-267-216-276-672-751-371-753-351-375-135-376-652-627-263-612-136-132_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 132-132 546-576-576-675-762-672-625-267-132-316-213-213-321-273-276-752-725-726 106-126-123-106-163-612-672-672-762-762-726-275-725-572-756-756-736-106-136-103-106-526-103-103-1_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 175-173-513-715-571-751-517-751-571-517-751-571-517-571-576-576-576-576-517 501-157-751-571-571-175-570-570-576-571-517-571-571-576-571-576-576-576-576-576-571-576-657-756-570-5_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 572-276-265-725-271-261-276-237-513-236-765-267-570-237-236-267-357-574-173-730-362-567-153-750-705-127-152-527-265-326-731-257-265-237-276-574-573-375-237-725-627-617-753-231-5_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 137-310-107-137-103-173-130-173-310-321-301-137-135-705-170-170-310 137-137-130-317-130-301-312-130-312-317-125-137-137-310-310-310-125-137-103-173-301-137-107-137-130 130-310-3_V.mp3)
  Segment 8: 11032ms ~ 24136ms (file: 256-574-576-213-237-231-253-267-571-265-542-540-312-103-160 103-130-213-216-102-132-236-263-270-167-106-132-231-172-267-267-236-206-102-160-312-327-102-163-613-674-672-203-213-2_W.mp3)
  Segment 9: 25136ms ~ 25712ms (file: 216-162-216-216-216-126_W.mp3)
  Segment 10: 26712ms ~ 27288ms (file: 317-263-236-267-623-675-625-625-216-162-261-672 167-671-670-670 652-652-236_W.mp3)
  Segment 11: 28288ms ~ 36064ms (file: 754-276-275-301-273-726-267-627-206-267-756-206-123-163-120-213-672-230-201-102-302-102-320-310-230-102-267-103-627-230-127-106-231-230-524-263-231-120-320-276-130-236-260-325-5_W.mp3)
  Segment 12: 37064ms ~ 37640ms (file: 610-765-756-765 627-267-216-261-267-627-612-126-627-612-126-132-102-102_W.mp3)
  Segment 13: 38640ms ~ 39216ms (file: 236-236 605-125-756-765-675 627-632_W.mp3)
  Segment 14: 40216ms ~ 40792ms (file: 715-715-175-715 105-153-157-154-105-175-175-170-715-170-715-716 103-103-103-104-105-150-150-157-715-751-715-751-765-715-175-715-715-715-715-175 751-754-751 153-105-150-150-105-1_W.mp3)
  Segment 15: 41792ms ~ 42368ms (file: 136-126-672-653-261-517-157-153-517-751-123-132-130-167-175-715-175-715-756-175-716-176-170-612-761-173-732-762-732-713-376-756-765-765-761-765-765-765-675-637-763-765-326-136-1_W.mp3)
  Segment 16: 43368ms ~ 43944ms (file: 105-105-157-105-570-750-570-751-170-710-710-170-175-175-710-715-170-751-750-571-175-750-750-751-751-750-751-570-576 150-517-105-107-107-170-107-175-105-170-517-175-157-157-510-5_V.mp3)
  Segment 17: 44944ms ~ 45520ms (file: 625-652-625-652-651-625-625-652-562-652-652-653-657 612-621-625-612-625-652-625-625-620-260-621-263-261-263-267-621-265-265-625-625-623-625-625-265-652-562-652-562-652-562-562-6_W.mp3)
  Segment 18: 46520ms ~ 47096ms (file: 710-715-675-623-627-672-637-732-627-561-673-132-132-102-361-312-103-136-631-362-326-317-621-152-103-507-103-132-312-321-106-136-136-130-316-163-326-716-167-213-216-657-617-671-1_W.mp3)
  Segment 19: 48096ms ~ 48672ms (file: 571-517-507-507-570-150-175-751-715-517-710-715-712-130-123-321 571-157-537-157-513-517-175-715-175-713-731-261-621 570-570-571-750-751-175-710-753-570-715-715-705-715-672-627_W.mp3)
  Segment 20: 49672ms ~ 50248ms (file: 163-361-631-361-132-367-316-230-623-321-570-576-572-573-507-306-165-361-261-106-361-126-671-361-162-316-326-235-672-301-167-623-263-612-123-136-620-623-602-265-275-326-123-612-1_W.mp3)
  Segment 21: 51248ms ~ 64820ms (file: 574-201-201-213-106-134-542-150-201-213-210-230-213-321-637-207-213-132-753-753-357-107-210-621-136-321-316-361-316-106-167-103-170-152-231-103-201-571-576-265-125-157-310-371-3_W.mp3)
  Segment 22: 65820ms ~ 66396ms (file: 605-570-506-652-602-650-562-562-652-625-652-527-275-672-627-756-265-652-627 103-132-502-572-507-507_W.mp3)
  Segment 23: 67396ms ~ 67972ms (file: 103-103-143-106-103-164-103-103-103-136-135-103-103-103-106-163-103-163-103-153-103-153-135-315-315-317-315-513-573-574-315-315-135-315-357-573-571-571-574-754-574-574 103-103-1_V.mp3)
  Segment 24: 68972ms ~ 78476ms (file: 301-760-312-152-715-631-672-507-236-367-301-537-536-351-301-312-365-567-132-371-571-756-367-260-106-610-163-317-137-137-137-176-137-137-173-261-276-102-716-761-675-675-671-210-3_W.mp3)
  Segment 25: 79476ms ~ 80052ms (file: 317-103-207-217-231-270-276-213-132-103-203-275-276-620-267-307-273-320-230-273-203-574-352-326-672-362-326-312-312-312-327-273-207-720-273-273-201-231-271-207-312-136-273-137-2_V.mp3)
  Segment 26: 81052ms ~ 81628ms (file: 762 763-673-672-762-672-162-236-312-326_W.mp3)
  Segment 27: 82628ms ~ 83204ms (file: 105-150-150-105-157-157-150-150-157-150-105-150-157-157-107-175 105-150-150-153-517-517-150-156-150-105-105-103-150-150-105-105-105-150-150 105-105-105-150-157-175-167-175-176-1_V.mp3)
  Segment 28: 84204ms ~ 84780ms (file: 610-162-130-130-105-132-126-160-163-103-675-106-163-136-106-136-137-216_V.mp3)
  Segment 29: 85780ms ~ 86356ms (file: 750-751-701-750-570 570-574-571-571-570-105-150-175-510-157-715-517-150-157-107-170-157-517-157-701-175-751-715-571-571-750 570-705-570-570-715-510-107-710-570-157-175-715-170-1_W.mp3)
  Segment 30: 87356ms ~ 87932ms (file: 517-157-167-160-175-165-175-157-106-150-107-517-105-107-157-175-175 150-150-105-105-105-105-106-103-105-103 157-150-105-105-105-105-105-105-105-106-135-105-105-106-135-105-150 1_V.mp3)
  Segment 31: 88932ms ~ 89508ms (file: 103-103-540-273-372-750-321-715-175-127-572-673-156-516-327-315-317-507-267-716-507-576-267-627-726-263-160-627-165-561-120-132-123-157-172-570-237-236-570-751-276-670-276-572-3_V.mp3)
  Segment 32: 90508ms ~ 91084ms (file: 567 570-507-507-506-516-162-605-605-267-672-267-627-267-176 762-675_W.mp3)
  Segment 33: 92084ms ~ 92660ms (file: 150-510-517-507-507-507 105-157-105-157-105-517-517-517-150-105-510-510-501-501-510-507-507-517-501-510-571-501-510-501-150-510-150-571 105-157-105-157-105-150-517-150-517-150-7_V.mp3)
  Segment 34: 93660ms ~ 94236ms (file: 751-751-175-517-751-571-571-105-501-507-701-501-153-531-503-517-513-517-751-175-157-175-175-751-751-571-751-175-751-751-751-715-517-715-517-715-751-571-517-571-751-751-573 513-5_V.mp3)
  Segment 35: 95236ms ~ 103264ms (file: 237-325-312-132-132-263-105-261-120-132-103-106-136-102-123-123-501-230-137-126-276-127-321-231-237-601-105-152-136-136-210-123-325-546-136-301-103-367-310-103-310-126-321-310-1_W.mp3)
  Segment 36: 104264ms ~ 110996ms (file: 310-120-253-260-524-126-210-301-123-201-132-261-106-103-132-517-541-130-103-625-321-231-541-206-260-541-210-103-301-310-102-152-213-120-130-163-261-231-103-320-213-203-210-260-4_W.mp3)
  Segment 37: 111996ms ~ 125424ms (file: 263-312-762-267-132-627-312-132-301-312-765-321-310-136-761-120-132-213-120-276-132-102-273-230-230-267-102-762-123-102-162-103-320-137-103-201-327-231-207-103-321-103-271-312-1_V.mp3)
  Segment 38: 126424ms ~ 127000ms (file: 653-231-316-301-357-376-576-672-705-675-617-102-132-534-312-312-213-267-617-321-361-231-651-657-367-627-567 627-216-216-276-537-527-572-506-326-321-352-236-562-326-162-706-627-2_V.mp3)
  Segment 39: 128000ms ~ 131168ms (file: 257-267-267-627-267-267-627-361-362-263-236-263-327-321-126-102-621-265-260-576-576-751-231-356-352-362-150-215-210-126-260-562-675-327-325-254-625-652-236-623-263-236-263-263-2_W.mp3)
  Segment 40: 132168ms ~ 132744ms (file: 672-761-765-761-167-671-716-167-621-675-167-126-326-103-130-127-316-163-372-326-312-312-675-167-162-612-263-231-261-627-267-361-263-267-673-623-261 163 516 105-267-162-163-136-6_W.mp3)
  Segment 41: 133744ms ~ 134320ms (file: 132-361-731-763-672-756-735-761-726-762-762-763-761-627-751-375-765-735-765-672-675-673-576-750-316-617-607-763-376-670-672-572-576-756-567-765-756-762-172-765-765-765-765-765-6_W.mp3)
  Segment 42: 135320ms ~ 135896ms (file: 135-136-316-136-135-136-103-136-135 314-316-361-316-136-136-136-136-135-136-136-136-163-136-103 357-573-357-571-571-574 135-301-315-361-134-364-164-135-136-136-136-134-136-135-1_V.mp3)
  Segment 43: 136896ms ~ 137472ms (file: 103-132-163-317-107-160-106-156-126-602-610-621-361-671-603-621-620-132-132-260-102-132-132-136-136-312-103-163-625-621-261-152-102-210-201-201-201-213-136-132-126-675-671-670-7_V.mp3)
  Segment 44: 138472ms ~ 149380ms (file: 276-754-267-361-312-213-756-103-257-120-127-103-317-236-267-263-321-103-230-102-263-261-713-326-527-265-267-136-107-361-361-103-137-132-236-236-762-321-321-517-130-103-263-350-2_W.mp3)
  Segment 45: 150380ms ~ 150956ms (file: 570-751-571-517-513-571-510-571-571 751-753-715-715-571-517-571-571-571-517-571-517-157-157-517-157-517-517-571 517-561-157-517-517-571-570-517-715-571-517-517-157-571-571-571-5_V.mp3)
  Segment 46: 151956ms ~ 152532ms (file: 751-571-570-571-715-571-715-175-750-571-157-571-571-157-517-751-175-751-175-517-751-571 571-571-715-175-571-751-705-510-517-571-751-751-571 571-571-571-750-107-571-105-517-571-1_W.mp3)
  Segment 47: 153532ms ~ 154108ms (file: 672-673-201-210-312-361-362-623-136-367_W.mp3)
  Segment 48: 155108ms ~ 162632ms (file: 632-627-726-327-132-627-657-652-163-165-123-302-617-260-267-107-302-103-310-361-371-107-310-237-203-307-376-312-234-103-310-213-213-231-130-103-103-204-231-201-204-201-231-302-3_W.mp3)
  Segment 49: 163632ms ~ 172668ms (file: 267-127-132-137-165-507-571-275-756-521-625-762-260-760-276-726-756-726-120-715-752-137-672-276-127-216-213-120-206-231-276-326-126-126-123-123-312-301-302-120-132-321-312-123-2_W_stt_tom_flat_black_be_a.mp3)
  Segment 50: 173668ms ~ 174244ms (file: 132-102-163-672-672-627-123-103-301-301-103-130-310-301-130-136-132-136_V.mp3)
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
> **Time**: 2026-04-05 06:44:36
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "이거 갤럭시 에스 이십육에서만 쓸 수 있는 기능이냐",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "이거", "start_ms": 20, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "갤럭시", "start_ms": 130, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "에스", "start_ms": 260, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "이십육에서만", "start_ms": 320, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "쓸", "start_ms": 460, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 490, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 510, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "기능이냐", "start_ms": 540, "dur_ms": 36, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "일단 아이폰에도요 통화 스크리닝 기능 들어가 있거든요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "일단", "start_ms": 10, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "아이폰에도요", "start_ms": 100, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "통화", "start_ms": 240, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "스크리닝", "start_ms": 310, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "기능", "start_ms": 410, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "들어가", "start_ms": 470, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "있거든요", "start_ms": 520, "dur_ms": 56, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "근데 이건 제가 가지고 있는 한 육 년 전쯤에 나온",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "근데", "start_ms": 10, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "이건", "start_ms": 90, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 150, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "가지고", "start_ms": 210, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 290, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 340, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "육", "start_ms": 380, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "년", "start_ms": 410, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "전쯤에", "start_ms": 440, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "나온", "start_ms": 510, "dur_ms": 66, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "이 아이폰 에스이 이 세대 요거에서도 이 기능을 쓸 수가 있고요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 10, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "아이폰", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "에스이", "start_ms": 140, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 210, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "세대", "start_ms": 250, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "요거에서도", "start_ms": 310, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 410, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "기능을", "start_ms": 440, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "쓸", "start_ms": 500, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "수가", "start_ms": 530, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "있고요", "start_ms": 560, "dur_ms": 16, "phase_lr_deg": 0.0}
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
"transcript": "두 번째",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "두", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "번째", "start_ms": 150, "dur_ms": 426, "phase_lr_deg": 0.0}
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
"transcript": "이천이십이년 십일월 무함마드 빈 살만 사우디아라비아 왕세자가 직접 한국을 방문했습니다 이 방문을 통해 양국 간에는 스마트팜을 포함한 스물한 개 분야에 걸친 협력",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "이천이십이년", "start_ms": 200, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "십일월", "start_ms": 1100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "무함마드", "start_ms": 1700, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "빈", "start_ms": 2300, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "살만", "start_ms": 2500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "사우디아라비아", "start_ms": 3100, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "왕세자가", "start_ms": 4000, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "직접", "start_ms": 4800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "한국을", "start_ms": 5300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "방문했습니다", "start_ms": 5900, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 7000, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "방문을", "start_ms": 7300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "통해", "start_ms": 7900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "양국", "start_ms": 8400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "간에는", "start_ms": 8900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "스마트팜을", "start_ms": 9500, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "포함한", "start_ms": 10400, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "스물한", "start_ms": 11100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "개", "start_ms": 11700, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "분야에", "start_ms": 12000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "걸친", "start_ms": 12600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "협력", "start_ms": 13100, "dur_ms": 4, "phase_lr_deg": 0.0}
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
"transcript": "나이의 열 배가 걸립니다 우주가 백삼십팔억 년인데 그 열 배 천삼백팔십억 년이 걸리는 것이죠 두 번째 보관이 지옥입니다 어디에 담을 수 있을까요 어떤 그릇에도 담을 수는 없습니다 우리가 이 세상",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "나이의", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "열", "start_ms": 600, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "배가", "start_ms": 850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "걸립니다", "start_ms": 1200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "우주가", "start_ms": 1800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "백삼십팔억", "start_ms": 2300, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "년인데", "start_ms": 3100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 3600, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "열", "start_ms": 3800, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "배", "start_ms": 4050, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "천삼백팔십억", "start_ms": 4300, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "년이", "start_ms": 5200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "걸리는", "start_ms": 5600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "것이죠", "start_ms": 6100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "두", "start_ms": 6600, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "번째", "start_ms": 6800, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "보관이", "start_ms": 7200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "지옥입니다", "start_ms": 7750, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "어디에", "start_ms": 8600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "담을", "start_ms": 9100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 9450, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있을까요", "start_ms": 9600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 10200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "그릇에도", "start_ms": 10600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "담을", "start_ms": 11200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "수는", "start_ms": 11550, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "없습니다", "start_ms": 11800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "우리가", "start_ms": 12400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 12900, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "세상", "start_ms": 13100, "dur_ms": 276, "phase_lr_deg": 0.0}
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
"transcript": "가장 중요한 질문에 한번 답해 보겠습니다 이 변화 속에서 우리의 역할은 어떻게 달라질까요 오늘 다룬 네 가지 축에서 저희의 역할이 어떻게 진화하는지 한번 정리를 간단하게 해 봤는데요 에이아이",
"speaker": "speaker_4",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "가장", "start_ms": 200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "중요한", "start_ms": 600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "질문에", "start_ms": 1050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "한번", "start_ms": 1500, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "답해", "start_ms": 1800, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "보겠습니다", "start_ms": 2150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 2850, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "변화", "start_ms": 3000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "속에서", "start_ms": 3400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "우리의", "start_ms": 3950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "역할은", "start_ms": 4400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 4900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "달라질까요", "start_ms": 5350, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 6200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "다룬", "start_ms": 6550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "네", "start_ms": 6950, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "가지", "start_ms": 7100, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "축에서", "start_ms": 7400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "저희의", "start_ms": 7950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "역할이", "start_ms": 8400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 8900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "진화하는지", "start_ms": 9350, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "한번", "start_ms": 10100, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "정리를", "start_ms": 10400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "간단하게", "start_ms": 10850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "해", "start_ms": 11400, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "봤는데요", "start_ms": 11600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "에이아이", "start_ms": 12200, "dur_ms": 576, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 18,
"transcript": "성실하게 답변을 달아주고 있습니다 제가 최근에 그 삼성 워치 영상에서 삼성 담당자가 쓴 글을 직접 보여드리고요 해당 글 링크까지 좀 남기면서",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "성실하게", "start_ms": 200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "답변을", "start_ms": 750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "달아주고", "start_ms": 1200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 1700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 2300, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "최근에", "start_ms": 2600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 3050, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "삼성", "start_ms": 3200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "워치", "start_ms": 3550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "영상에서", "start_ms": 3900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "삼성", "start_ms": 4450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "담당자가", "start_ms": 4800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "쓴", "start_ms": 5350, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "글을", "start_ms": 5550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "직접", "start_ms": 5900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "보여드리고요", "start_ms": 6350, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "해당", "start_ms": 7100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "글", "start_ms": 7450, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "링크까지", "start_ms": 7650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 8200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "남기면서", "start_ms": 8400, "dur_ms": 576, "phase_lr_deg": 0.0}
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
"transcript": "가야산과 맞닿은 백운산 품속 시끄러운 세상과 동떨어진 산속에 농장 하나가 있습니다",
"speaker": "speaker_5",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 300,
"audio_type": "speech",
"words": [
{"word": "가야산과", "start_ms": 300, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "맞닿은", "start_ms": 950, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "백운산", "start_ms": 1500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "품속", "start_ms": 2050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "시끄러운", "start_ms": 2500, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "세상과", "start_ms": 3150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "동떨어진", "start_ms": 3700, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "산속에", "start_ms": 4350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "농장", "start_ms": 4900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "하나가", "start_ms": 5350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 5900, "dur_ms": 576, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "평균 고도가 천 미터를 훌쩍 넘기는 것과 비교하면 한참 낮은 수치죠 그런데 경사도는 높습니다 한반도의 평균 경사도는 약 오 점 칠 도로 동아시아 전체 평균인 삼 점 구 도보다 이 도 가까이 더 가파릅니다 정리하자면 높지도 않으면서 경사는 가파르고 단단하기만 해서 아무것도 할 수 없는 산",
"speaker": "speaker_6",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "평균", "start_ms": 200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "고도가", "start_ms": 550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "천", "start_ms": 1000, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "미터를", "start_ms": 1250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "훌쩍", "start_ms": 1700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "넘기는", "start_ms": 2100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "것과", "start_ms": 2600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "비교하면", "start_ms": 2950, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "한참", "start_ms": 3500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "낮은", "start_ms": 3900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "수치죠", "start_ms": 4300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "그런데", "start_ms": 4800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "경사도는", "start_ms": 5250, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "높습니다", "start_ms": 5800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "한반도의", "start_ms": 6400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "평균", "start_ms": 6950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "경사도는", "start_ms": 7300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "약", "start_ms": 7850, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "오", "start_ms": 8050, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "점", "start_ms": 8250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "칠", "start_ms": 8450, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "도로", "start_ms": 8650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "동아시아", "start_ms": 9000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "전체", "start_ms": 9550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "평균인", "start_ms": 9900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "삼", "start_ms": 10400, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "점", "start_ms": 10600, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "구", "start_ms": 10800, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "도보다", "start_ms": 11000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 11450, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "도", "start_ms": 11650, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가까이", "start_ms": 11850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 12300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가파릅니다", "start_ms": 12500, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "정리하자면", "start_ms": 13150, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "높지도", "start_ms": 13900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "않으면서", "start_ms": 14400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "경사는", "start_ms": 14950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "가파르고", "start_ms": 15450, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "단단하기만", "start_ms": 16050, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "해서", "start_ms": 16800, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "아무것도", "start_ms": 17150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 17700, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 17900, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "없는", "start_ms": 18050, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "산", "start_ms": 18400, "dur_ms": 172, "phase_lr_deg": 0.0}
]
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
"transcript": "포커스 양보가 필요합니다",
"speaker": "speaker_7",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "포커스", "start_ms": 100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "양보가", "start_ms": 450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "필요합니다", "start_ms": 850, "dur_ms": 576, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "자 요렇게 뭐 이런 천광이나 뭐 천궁 같은 거를 지원을 하자 간접적으로 자 요런 내용들이 나왔던 것도 이제 요 배경이 있는 겁니다 이렇게 참고하시면 될 것 같고요 다음 주 주요 이슈입니다 삼성전자 잠정 실적 발표가 나오는데 화요일이 가능성이 높은 거죠",
"speaker": "speaker_8",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "자", "start_ms": 150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "요렇게", "start_ms": 400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 800, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 1000, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "천광이나", "start_ms": 1300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 1850, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "천궁", "start_ms": 2050, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "같은", "start_ms": 2400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "거를", "start_ms": 2750, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "지원을", "start_ms": 3050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "하자", "start_ms": 3500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "간접적으로", "start_ms": 3850, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "자", "start_ms": 4500, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "요런", "start_ms": 4750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "내용들이", "start_ms": 5100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "나왔던", "start_ms": 5650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "것도", "start_ms": 6100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 6450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "요", "start_ms": 6800, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "배경이", "start_ms": 7000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 7450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "겁니다", "start_ms": 7800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 8250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "참고하시면", "start_ms": 8700, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "될", "start_ms": 9350, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 9550, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "같고요", "start_ms": 9750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 10200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "주", "start_ms": 10550, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "주요", "start_ms": 10750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이슈입니다", "start_ms": 11100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "삼성전자", "start_ms": 11750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "잠정", "start_ms": 12300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "실적", "start_ms": 12700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "발표가", "start_ms": 13100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "나오는데", "start_ms": 13600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "화요일이", "start_ms": 14150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "가능성이", "start_ms": 14700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "높은", "start_ms": 15250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "것이죠", "start_ms": 15650, "dur_ms": 576, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "와 와 향이 있네요 미나리가 그냥 그 나무인데 아 조금도 아 진짜요 조금도 향 있는 나물이에요 야 이거",
"speaker": "speaker_9",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "와", "start_ms": 100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "와", "start_ms": 350, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "향이", "start_ms": 600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "있네요", "start_ms": 950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "미나리가", "start_ms": 1350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 1900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 2250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "나무인데", "start_ms": 2450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 3000, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "조금도", "start_ms": 3200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 3650, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "진짜요", "start_ms": 3850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "조금도", "start_ms": 4300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "향", "start_ms": 4750, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 5000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "나물이에요", "start_ms": 5350, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "야", "start_ms": 6100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "이거", "start_ms": 6350, "dur_ms": 576, "phase_lr_deg": 0.0}
]
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
"transcript": "오 와 이거다 뒤지지 아 야",
"speaker": "speaker_9",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "오", "start_ms": 100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "와", "start_ms": 350, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "이거다", "start_ms": 600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "뒤지지", "start_ms": 1050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 1600, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "야", "start_ms": 1850, "dur_ms": 576, "phase_lr_deg": 0.0}
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
},
{
"segment": 50,
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
*Generated by WKAppBot ask gemini — 2026-04-05 06:44:36*
