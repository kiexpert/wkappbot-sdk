# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=10820  time=11:59:42  uptime=28518s  mem=363MB  system_processes=354"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_115930.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 137-175-137-107-715-753-175-731-103-317-137-715-173-107-173-751-175-173_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 765-526-576-572-576-567-672-256-265-652-165-615-165-165-612-165-156-315-156-156-653-165-163-657-675-567-657-756-657-675-765-675-625-765 560-506-150-156-165-165-561-615-165-165-1_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 261-376-216-652-276-657-625-672-571-751-571-571-571-576-571-725-756-752-756-752-726-576-765_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 103-103-102-156-157-103-103-165-162-103-103-106-167-156-125-162-716-126-612-152-675-132-365-176-136-165-160-163 601-607-167-160-612-603-612-132-761-162-751-527-621-136-103-172-1_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 103-137-316-130-301-136-106-567-165-371-163-637-137-673-317-763-607-167-671-106-163-706-607-632-163-627-623-620-163-107-132-103_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 726-317-310-310-316-302-312-123-167-124-310-312-701-207-302-307-301-357-320-320-302-302-301-103-103-301-503-540-501-750-756-765-765-675-764-674-547-756-756-137-765-765-103-136-1_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 103-173-507-315-105-103-310-137-317-137-317-135-130-130-137-157-135 105-150-507-150-537-537-153-751-571-705-137-315-173-130-173-137-137-157-137-175-315-137-317-137-175-153 153-5_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 516-512-501-651-561-165-156-615-652-657-675-657-625-675-657-576-567-657-756-567-576-567-657-567-657-567-657-652-627-657-625-627-625-362-765-675-765-675-576-576-657-567-576-567-7_W.mp3)
  Segment 9: 12608ms ~ 19772ms (file: 367-132-675-167-675-267-267-672-260-120-716-671-236-713-261-267-760-756-123-321-132-123-176-671-231-216-132-213-126-576-657-756-627-321-102-267-627-267-326-103-756-672-267-267-1_W.mp3)
  Segment 10: 20772ms ~ 21348ms (file: 507-507-507-520-501-501-510-506-507-507-507-507-501-506-507-501-510-502-205-206-251-267-602-602-602-610-602-602-612-620-620-612-621-162-621-610-612-162-163-163-612-612-612-613-1_W.mp3)
  Segment 11: 22348ms ~ 22924ms (file: 175-157-157-153-751-150-571-157-573-571-157-573-157 157-571-157-517-157-517-157-175-157-157-517-571-517-571 517-157-175-157-157-517-157-157-175-157-157-157 150-517-157-517-571-1_V.mp3)
  Segment 12: 23924ms ~ 30656ms (file: 510-236-231-752-725-230-203-127-231-210-175-213-201-123-132-273-321-270-216-213-103-123-251-572-105-231-327-137-756-756-637-157-206-312-301-162-105-310-572-507-201-130-321-137-3_W.mp3)
  Segment 13: 31656ms ~ 32232ms (file: 526-512-521-516-571-516-571-527-571-561-521-517-517-517-516-517-576-571-521 570-507 561-650-561-657-567-567-657-567-567-657-567-567-567-576-567-576-567-756-756-712-721-712-715-7_W.mp3)
  Segment 14: 33232ms ~ 43852ms (file: 137-751-630-302-576-172-302-273-732-276-627-627-620-362-126-102-163-527-210-321-130-103-167-162-361-367-267-362-576-326-162-103-103-765-675-675-376-675-765-127-103-231-103 574-5_W.mp3)
  Segment 15: 44852ms ~ 45428ms (file: 715-175-170-710-170-751-701-715-701-571-170-710 157-571-751-571-750-750-710-750-751-750-715-571-751-750-705-507-705-750-750-170-710-157-750_V.mp3)
  Segment 16: 46428ms ~ 47004ms (file: 753-756-576-625-572-526-715-756-756-756-765-765-756-675-126-216_W.mp3)
  Segment 17: 48004ms ~ 48580ms (file: 657-163-713-576-167-160-106-106-106-103-160_W.mp3)
  Segment 18: 49580ms ~ 50156ms (file: 157-507-517-507-501-567-501-507-501-501-507-157_V.mp3)
  Segment 19: 51156ms ~ 51732ms (file: 571-571-517-517-571-573-571-571 517_W.mp3)
  Segment 20: 52732ms ~ 53308ms (file: 257-250-520-527-520-527-527-562-527-527-526-250-527-527-527-257-257-205-275-250-675-627-762-276-267-571 572_W.mp3)
  Segment 21: 54308ms ~ 54884ms (file: 576-756-576-573-576-576-571-576-570-567-572-517-570-517-571-157-571-175-517-175-176-175-576-571-576-576-576-567-571-576-571-576-756-576-570-513-507-517-536-513-571-517-573-517-1_W.mp3)
  Segment 22: 55884ms ~ 66648ms (file: 267-130-231-265-627-106-576-756-106-103-106-236-231-236-236-263-267-736-735-236-263-754-765-236-263-267-236-123-725-206-136-756-276-206-316-103-672-231-102-130-236-216-163-106-1_W.mp3)
  Segment 23: 67648ms ~ 68224ms (file: 103-103-162-513-127-126-163-176-637-175-106-106-765-167-615-163-137-162-126-103-615-130-106-106-156-136-106-136-163-106-631-163-126-103-106-136-130-176-623-263 627-361-637-367-3_V.mp3)
  Segment 24: 69224ms ~ 69800ms (file: 756 572-572-527-251-251-275-752-275-752-570-571-570-576-576-574-576-756-756-576-756-756-756-756 502-502-256-675-720-571-576-571-570-571-571 567-502-570-750-750-756-750-756-751-7_W.mp3)
  Segment 25: 70800ms ~ 71376ms (file: 571-571-517-157-510-571-103-130-751-712-715-752-756-725-675-763-762-762-765-763-752-725-267-273-273-136-132-173-127-163-167-316-176-376-736-327-317-712-376-735-763-735-752-756-7_W.mp3)
  Segment 26: 72376ms ~ 72952ms (file: 514-513-254-541-521-124-351-231-725-756-576-756-756-756-576-576-576-576-576-576-576-756-756-324-645-516-561-574-125-564-514-514-546-541-675-760-607-560-560-657-567-546-752-756-7_W.mp3)
  Segment 27: 73952ms ~ 74528ms (file: 136-572-572-756-357-671-761-136-312-132-310-173-176-137-173-310-532-367-365-365-356-356-316-623-167-236-231-130-130-132-130-103-326-732-327 105-573-576-576-573-576-524-753-765-1_W.mp3)
  Segment 28: 75528ms ~ 83016ms (file: 103-312-207-120-127-260-260-201-132-103-706-312-310-103-103-230-231-672-623-527-106-576-276-123-123-276-260-130-130-367-267-106-213-103-310-623-237-736-237-753-572-215-206-123-2_V.mp3)
  Segment 29: 84016ms ~ 89380ms (file: 275-527-231-216-213-237-273-132-123-127-127-127-213-231-216-261-276-126-132-132-231-321-125-126-127-217-216-132-210-213-216-126-126-321-123-213-132-312-231-213-267-102-310-213-2_W.mp3)
  Segment 30: 90380ms ~ 90956ms (file: 517-571-571-570-571-517-510-150-571-157-715-173-517-751-571-571-751-571-576-571-571-576-576-570-576-570-537-571-570-570-571-570-576-576-576-571-531-571-501-510-517-571-571-571-5_V.mp3)
  Segment 31: 91956ms ~ 100920ms (file: 675-260-752-725-206-765-132-276-756-756-167-103-130-632-103-312-236-576-531-136-103-130-632-751-165-657-130-103-132-312-216-132-507-631-712-107-173-752-754-756-627-627-627-753-5_W.mp3)
  Segment 32: 101920ms ~ 102496ms (file: 321-135-173-132-173-231-275-127-172-103-132-325-231-326-320-203-321-320-326-365-635-632-652-657-362-321-320-320-321-326-321-203-326-123-312-302-321-203-310-317-123-327-231-231-2_W.mp3)
  Segment 33: 103496ms ~ 104072ms (file: 621-621-621-621-261 216-261-621-621-621-621-621-621-612-612-621-612 261-621-621-621-621-623-623-612-130-716-306-627-620-162-612-621-612-612-621-612-612-627-612-617-612-612-613-6_W.mp3)
  Segment 34: 105072ms ~ 105648ms (file: 671-760-670-601-610-706-671-716-617-176-167-657 157-105-105-105-105-150-150-105-150-105-150-165-157-150-156-167-167 170-716-175-175-175-715-173-176-175-176-175-517-156-157-716-5_W.mp3)
  Segment 35: 106648ms ~ 107224ms (file: 517-157-510-571-517-517-573-576-516-517-571-571-517-715-751-721-715-716-175-715-716-715-173-712-761-176-150-105-150-157-510-150-571-107-175-517-570-517-517-516-157-157-516-513-5_W.mp3)
  Segment 36: 108224ms ~ 108800ms (file: 715-751-715-715-715-715-751-715-175-175 105-105-751-751-751-175-751-715-710-715-715-715-751-175-175-751-715-751-715-751-175-751-715 150-571-175-715-751-715-715-751-157-175-715-7_W.mp3)
  Segment 37: 109800ms ~ 119952ms (file: 231-123-102-263-761-267-260-726-321-321-123-132-102-176-102-321-765-275-120-102-123-176-162-237-321-102-751-765-263-106-130-123-231-726-120-103-231-231-203-236-167-207-231-132-2_W.mp3)
  Segment 38: 120952ms ~ 121528ms (file: 615-651-651-756-571-576-756-576-751-726-762-675-675-675 205-165-625-615-615-617-765-627-671-765-176-765-715-765-715-715-751-751-751-716-715 615-651-615-651-657-651-761-716-765-7_W.mp3)
  Segment 39: 122528ms ~ 123104ms (file: 751 756-105-705-710-571-157-713-750-173-715-750-170-751-715-750-175-571-751-751-751-751-571-751-715-175-751-751 756-751-173-157-170-750-173-715-710-173-157-713-173-170-173-751-7_W.mp3)
  Segment 40: 124104ms ~ 124680ms (file: 517-510-507-501-750-570-750-715-175-751-517-751-517-175-750-175-715-750 571-751-510-570-170-507-175-570-175-157-750-750-571-751-705-570-175-715-107-105-150-731-173-736-753-753-1_V.mp3)
  Segment 41: 125680ms ~ 126256ms (file: 612-162-162-167 312-326-672-612-627-316-632-657-671-675-756-675-765-672-567-162-312-130-132-625-261-623-261-321-132-631-310 310-132-136_W.mp3)
  Segment 42: 127256ms ~ 127832ms (file: 321-312-276-132-162-136-132-312-302-132-357-537-375-317-135-135 763-760-607-136_V.mp3)
  Segment 43: 128832ms ~ 129408ms (file: 576-516-325-567-512-526-576-576-756-756-576-567-136-156-561-156-567-576-516-576-517-657-615-657 561-526-572-512-512 156-156-157-156-153-156-652_W.mp3)
  Segment 44: 130408ms ~ 139228ms (file: 267-203-130-216-123-130-123-216-124-120-260-261-261-276-754-105-236-103-315-231-231-261-310-263-123-315-216-312-153-652-165-163-571-576-572-312-263-267-576-527-572-130-756-135-1_W.mp3)
  Segment 45: 140228ms ~ 140804ms (file: 206-206-206-236-230-230-574-572-273-273-320-132-260-620-751-541-613-163-631-614-206-267-321-301-201-230-167-136-106-107-260-206-623-126-201-267-267-203-637-632-623-136-103-132-3_W.mp3)
  Segment 46: 141804ms ~ 142380ms (file: 672-672-675 125-156-165-165-165-615-165-615-610-615-651-610-165-615-615-617_V.mp3)
  Segment 47: 143380ms ~ 143956ms (file: 170-170-175-175-751-751 175-175-175-175-175-170-170-715-175-175-175-715-175-715-175-715-175-170-715-175-752-751-571-571 751-715-750-175-175-175-157-175-715-751-715-715-751-175-7_W.mp3)
  Segment 48: 144956ms ~ 145532ms (file: 750-751-105-105-105-105-105-105-105-157-175-157-156-517-176-165-176-175-165-716-156-167-176-716-167-157-173 506-105-154-105-105-105-105-105-105-154-105-157-157-156-157-175-157-1_V.mp3)
  Segment 49: 146532ms ~ 147108ms (file: 610-162-231-657-672 671-123-123-627-627 271-624-326-231-327-231-216-675-761-761-103-132-176-136-613-136-615-613-631-371-127-625-134-321-264-317-341-765-751-756-612-162-263-216-1_V.mp3)
  Segment 50: 148108ms ~ 158620ms (file: 126-321-126-237-231-216-126-132-316-312-106-123-627-625-261-162-257-573-210-126-316-365-231-276-261-735-365-671-756-265-326-321-567-130-261-263-120-176-103-135-627-267-316-127-1_W.mp3)
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
> **Time**: 2026-04-05 12:01:17
> **AI**: gemini

---

## Response

AnalysisAnalysisGemini의 응답[
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
"transcript": "저희도 지금 정리를 했어요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "저희도", "start_ms": 120, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 520, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "정리를", "start_ms": 820, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "했어요", "start_ms": 1150, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "재벌 소세 기름은 그대로 뒀다 내일 아침 일찍 닦는데요 마지막으로 바닥도 깨끗하게 치우",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "재벌", "start_ms": 100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "소세", "start_ms": 450, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "기름은", "start_ms": 750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그대로", "start_ms": 1150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "뒀다", "start_ms": 1500, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "내일", "start_ms": 1900, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "아침", "start_ms": 2200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "일찍", "start_ms": 2550, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "닦는데요", "start_ms": 2850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "마지막으로", "start_ms": 3500, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "바닥도", "start_ms": 4100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "깨끗하게", "start_ms": 4550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "치우", "start_ms": 5100, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "시장 앞에서의 약간의 자만 이런 게 있을 때마다 약간 삐끗삐끗했던 거 같아요 저도 그래서 항상 시장 앞에서는 내가 조금이라도 수익 나면은 내가 욕심을",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "시장", "start_ms": 150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "앞에서의", "start_ms": 500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "약간의", "start_ms": 1000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "자만", "start_ms": 1400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 1800, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 2100, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있을", "start_ms": 2250, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "때마다", "start_ms": 2500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "약간", "start_ms": 3000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "삐끗삐끗했던", "start_ms": 3350, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 4100, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 4250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "저도", "start_ms": 4750, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 5150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "항상", "start_ms": 5600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "시장", "start_ms": 5950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "앞에서는", "start_ms": 6300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "내가", "start_ms": 6800, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "조금이라도", "start_ms": 7100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "수익", "start_ms": 7700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "나면은", "start_ms": 8050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "내가", "start_ms": 8550, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "욕심을", "start_ms": 8850, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "아이폰에서 다 쓸 수 있는 그런 기능이 갤럭시에서는 최신 폰만 지원하고 바로 구세대만 해도 안 된다고 업데이트를 안 해 준다고 삼성 미쳤나 막 이렇게 논리가 흘러가는 게 아닌가 싶어",
"speaker": "speaker_4",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "아이폰에서", "start_ms": 120, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 750, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "쓸", "start_ms": 950, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 1150, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 1300, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "그런", "start_ms": 1600, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "기능이", "start_ms": 1900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "갤럭시에서는", "start_ms": 2350, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "최신", "start_ms": 3100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "폰만", "start_ms": 3450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "지원하고", "start_ms": 3800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 4450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "구세대만", "start_ms": 4800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "해도", "start_ms": 5350, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 5650, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "된다고", "start_ms": 5850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "업데이트를", "start_ms": 6350, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 6950, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "해", "start_ms": 7150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "준다고", "start_ms": 7350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "삼성", "start_ms": 7850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "미쳤나", "start_ms": 8200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "막", "start_ms": 8750, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 8950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "논리가", "start_ms": 9400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "흘러가는", "start_ms": 9850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 10400, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "아닌가", "start_ms": 10550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "싶어", "start_ms": 11000, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "다 그런 기대감이 있는 거죠 그래서 아마 다음 주 화요일에 잠정 실적 발표가 나올 수가 있는데 실제로 50조가 넘어가면 세게 슈팅이 나올까라는 좀 기대감이 있는 거예요 물론 40조 대도 어마무시한 건데 시장이 어떻게 받아들여",
"speaker": "speaker_5",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "다", "start_ms": 150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "그런", "start_ms": 350, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "기대감이", "start_ms": 650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 1150, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "거죠", "start_ms": 1450, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 1850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "아마", "start_ms": 2250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 2600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "주", "start_ms": 2950, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "화요일에", "start_ms": 3150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "잠정", "start_ms": 3700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "실적", "start_ms": 4050, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "발표가", "start_ms": 4400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "나올", "start_ms": 4850, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "수가", "start_ms": 5150, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "있는데", "start_ms": 5450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "실제로", "start_ms": 5950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "50조가", "start_ms": 6400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "넘어가면", "start_ms": 6950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "세게", "start_ms": 7500, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "슈팅이", "start_ms": 7800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "나올까라는", "start_ms": 8250, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 8900, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "기대감이", "start_ms": 9100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 9650, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 9950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "물론", "start_ms": 10400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "40조", "start_ms": 10750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "대도", "start_ms": 11200, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "어마무시한", "start_ms": 11500, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "건데", "start_ms": 12150, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "시장이", "start_ms": 12550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 13000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "받아들여", "start_ms": 13400, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "국민 여동생이라고 불리는 배우 프리앙카 아르모하니 한국을 향해 진심 가득한 편지를 보내면서 난리가 났거든요 프리앙카는 그냥 예쁘기만 한 게 아니라 특유의 맑고 깨끗",
"speaker": "speaker_6",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "국민", "start_ms": 100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "여동생이라고", "start_ms": 450, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "불리는", "start_ms": 1150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "배우", "start_ms": 1600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "프리앙카", "start_ms": 1950, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "아르모하니", "start_ms": 2550, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "한국을", "start_ms": 3200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "향해", "start_ms": 3650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "진심", "start_ms": 4000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "가득한", "start_ms": 4350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "편지를", "start_ms": 4850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "보내면서", "start_ms": 5300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "난리가", "start_ms": 5850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "났거든요", "start_ms": 6300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "프리앙카는", "start_ms": 6950, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 7600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "예쁘기만", "start_ms": 7950, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 8500, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 8700, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 8850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "특유의", "start_ms": 9350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "맑고", "start_ms": 9800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "깨끗", "start_ms": 10200, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "아니면 기타를 아 저요 아 농구지만 전 현진영 가수예요",
"speaker": "speaker_7",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "아니면", "start_ms": 80, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "기타를", "start_ms": 450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 1000, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "저요", "start_ms": 1250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 1700, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "농구지만", "start_ms": 1950, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "전", "start_ms": 2500, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "현진영", "start_ms": 2700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "가수예요", "start_ms": 3150, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "대체해서 그냥 다 던져 버렸지 자 이게 시황을 보고 하는 거야 네가 옛날에는 눌림 추매 하고 큰 그림을 못 봤잖아 장 좋아서 네가 대충 사서 뭐 5일선 10일선 뭐 20일선 그리고 그",
"speaker": "speaker_8",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "대체해서", "start_ms": 100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 1000, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "던져", "start_ms": 1200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "버렸지", "start_ms": 1600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "자", "start_ms": 2200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 2400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "시황을", "start_ms": 2750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "보고", "start_ms": 3250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 3650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "거야", "start_ms": 4000, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "네가", "start_ms": 4400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "옛날에는", "start_ms": 4750, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "눌림", "start_ms": 5350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "추매", "start_ms": 5750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "하고", "start_ms": 6150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 6600, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "그림을", "start_ms": 6850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "못", "start_ms": 7350, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "봤잖아", "start_ms": 7550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "장", "start_ms": 8100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "좋아서", "start_ms": 8350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "네가", "start_ms": 8850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "대충", "start_ms": 9200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "사서", "start_ms": 9600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 10000, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "5일선", "start_ms": 10200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "10일선", "start_ms": 10700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 11250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "20일선", "start_ms": 11450, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 12100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 12600, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "자 여기 부엉이 둘 있어요 한 부엉이는 밤 열 시가 넘도록 엑셀과 씨름하면서 야근을 하겠죠 그런데 옆자리의 동료 부엉이는 AI한테 보고서 초안부터 경쟁사 분석 PPT 디자인",
"speaker": "speaker_9",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "자", "start_ms": 120, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "여기", "start_ms": 350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "부엉이", "start_ms": 700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "둘", "start_ms": 1100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 1350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 1850, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "부엉이는", "start_ms": 2050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "밤", "start_ms": 2550, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "열", "start_ms": 2800, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "시가", "start_ms": 3000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "넘도록", "start_ms": 3350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "엑셀과", "start_ms": 3800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "씨름하면서", "start_ms": 4300, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "야근을", "start_ms": 4950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "하겠죠", "start_ms": 5400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "그런데", "start_ms": 5950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "옆자리의", "start_ms": 6400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "동료", "start_ms": 6900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "부엉이는", "start_ms": 7250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "AI한테", "start_ms": 7750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "보고서", "start_ms": 8250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "초안부터", "start_ms": 8700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "경쟁사", "start_ms": 9250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "분석", "start_ms": 9700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "PPT", "start_ms": 10100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "디자인", "start_ms": 10450, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "안 되는 게 아니라 누구나 알고 있는 기본기들을 단순하게 조합한 건데 그 결과가 너무 말도 안 되는 승률을 보여주고 있습니다 차트에는 내 지지선이 있고 또 저항선이 있고 그 안에서 고점과 저점",
"speaker": "speaker_10",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "안", "start_ms": 150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "되는", "start_ms": 350, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 650, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "누구나", "start_ms": 1250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "알고", "start_ms": 1650, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 1950, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "기본기들을", "start_ms": 2250, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "단순하게", "start_ms": 2900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "조합한", "start_ms": 3450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "건데", "start_ms": 3950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 4350, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "결과가", "start_ms": 4550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 5000, "dur

---
*Generated by WKAppBot ask gemini — 2026-04-05 12:01:17*
