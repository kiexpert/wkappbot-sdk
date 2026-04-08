# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=36536  time=08:53:27  uptime=10687s  mem=371MB  system_processes=408"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_085133.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 716-710-765 705-107-176-701-571-175-710-715-710-175-170-175-175-715-716-715-176-715-716-761-715-756-715-751 570-175-157-617-657-170-175-710-715-107-175-170-715-175-715-715-715-1_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 571-753 570-517-571-571-570-513-751-573-750-751-750-751-751-571-571-751-571-571-751-751-751-751 573-571-517-571-571-571-571-571-576-571-573-576-571-576-751-751-750-752 574-574-5_W.mp3)
  Segment 3: 3152ms ~ 11108ms (file: 375-357-317-301-236-267-517-267-312-321-362-632-762-106-107-136-167-312-137-301-301-315-132-132-107-130-257-157-267-360-321-275-317-267-312-723-726-157-263-103-157-312-136-137-1_W.mp3)
  Segment 4: 12108ms ~ 17976ms (file: 231-312-765-103-631-273-302-756-752-217-763-263-735-731-721-627-102-271-103-152-203-261-213-512-312-571-267-210-267-206-670-216-263-371-673-627-132-213-301-207-765-103-607-201-2_V_stt_dole.mp3)
  Segment 5: 18976ms ~ 19552ms (file: 675-675-615-651-165-651-165-651-651-615-615-516-613-765 547-547-547-574-574-574-574_W.mp3)
  Segment 6: 20552ms ~ 21128ms (file: 713-710-571-571-570-753-751-751-571-570-752-750 574-571-570-571-571-571-751-570-571-517-571-751-571-573-157-571-751-571-751-751-753-571-517 574-576-576-576-571-576-571-517-513-5_W.mp3)
  Segment 7: 22128ms ~ 22704ms (file: 571-571-573 517-517-571-574-574-574-574-576-571-576-574-571-571-571-574_W.mp3)
  Segment 8: 23704ms ~ 24280ms (file: 751-571-751-751-571-571-573-573-571-576-756-756 570-570-157-571-517-570-157-750-157-750-517-517-570-713-715-571-751-753-735-157-713-715-750-571-570-571-750-570-751-576-576-576-5_V.mp3)
  Segment 9: 25280ms ~ 32624ms (file: 132-231-231-102-257-260-216-120-756-132-213-236-123-316-367-316-316-132-316-136-136-126-125-136-231-576-673-672-316-763-376-637-376-317-627-126-163-671-132-167-216-213-267-267-2_W.mp3)
  Segment 10: 33624ms ~ 34200ms (file: 672-721-762-763-670-617-673-673-631-126-176-617-136-376-163-167-627-761-671-765-167-125-165-167-165-316-631-312-127-176-170-135-170-127-713-637-730-702-713-736-701-730-710-735-7_V.mp3)
  Segment 11: 35200ms ~ 35776ms (file: 173-127-172-732-712-762-721-716-671-671-763-617-167-726-726-671-165-127-157-756-312-625-617-712-675-163-715-765-156-615-175-613-615-316-516-156-152-356-176-716-762-725 671-176-1_W.mp3)
  Segment 12: 36776ms ~ 37352ms (file: 571-571-571-715-715-571-756-571-715-157-753-715-571-735-570-157-753-573 571-517-517-517-517-517-517-517-571-571-576-571-571-571 574-574-574-574-571-574-574-571-571-517-571-571-5_W.mp3)
  Segment 13: 38352ms ~ 38928ms (file: 571-576-571-275-267-316-326-312-137-567-210-376-236-361-305-536-135-351-651-657-625-652-156-567-567-561-521-106-573-375-657-657-765-527-512-375-562-153-157-310-123-361-316-576-5_V.mp3)
  Segment 14: 39928ms ~ 40504ms (file: 570-570-510-173-731 574-574-571 571-571-571-576-571-573-756 574-574-574-574-571-574-571-574-574-517-157-517-571-517-571-517-571-517-157-517-571-571-572_W.mp3)
  Segment 15: 41504ms ~ 46364ms (file: 752-761-756-376-765-130-167-160-675-236-321-267-267-261-572-120-210-271-107-163-361-316-613-316-173-163-137-750-106-271-175-174-571-175-321-162-716-756-103-250-103-571-576-765-1_W.mp3)
  Segment 16: 47364ms ~ 47940ms (file: 753-571-753-571-751 574-574-574-574-576-574-574-571-576-571-571 574-574-576-571-571-576-571-576-571 574-574-574-574-576 572-572 574-571-517-571-574-573-517-571-571-572-571-571-5_W.mp3)
  Segment 17: 48940ms ~ 49516ms (file: 574-574-574-574-573-573-571-571-574-576-576-574-576-573 574-574-571-517-571-571-571-517-517-751-573-750-571-756-751-157-571-573-570-751-571 571-571-751-517-571-175-157-571-517-7_V.mp3)
  Segment 18: 50516ms ~ 51092ms (file: 153-571-157-510-157-517-571-175-715-150-371-713-157-572-170-751-753-157-715-571-571-571-571-570 571-571-517-570-571-751-715-571-705-715-571-751-751-157-753-715-751-751-751-715-7_V.mp3)
  Segment 19: 52092ms ~ 59436ms (file: 267-267-132-276-217-671-607-760-762-721-172-710-107-723-726-723-317-321-320-320-230-302-307-310-130-132-123-123-102-126-310-301-301-302-321-302-632-306-306-132-316-162-130-310-1_W.mp3)
  Segment 20: 60436ms ~ 61012ms (file: 517-571-517-715-750-715-715-173-750 571-517-751-517-705-153-750-730-751-570-750-175-715-715-571-571-715-571-751-175-751-576-753-573-573 570-571-573-571-573-715-573-175-571-175-5_W.mp3)
  Segment 21: 62012ms ~ 62588ms (file: 705-751-571-570-157-573-571-571-751-751-570-571-751 576-570-573-573-571-751-175-157-571-751-157-570-157-570-570-570-571-751-571-750-751-751 574-576-570-571-576-570-573-571-570-5_W.mp3)
  Segment 22: 63588ms ~ 64164ms (file: 231-236-576-756-573-372-572-275-216-123-615-617-376-316-613-653-625-327-632-752-237-671-376-765-367-312-367-617-132-271-750-317-526-325-317-321-572-162-736-762-536-526-325-750 5_V.mp3)
  Segment 23: 65164ms ~ 65740ms (file: 316-312-321-321-231-321-612 516-657-756-157-517-567-371 715-731-352-103-765-351-237-102-701-701-173-130-301-312-321-736-632-325-137-130-106-136-103-136-162-105-510-516-516-516 2_V.mp3)
  Segment 24: 66740ms ~ 67316ms (file: 531-571-751-751-751-517-751-753-756-571-570-751-751-570-571-576-576-571-571-571-570-570-576-571-573-756 517-153-501-751-571-175-576-751-517-751-175-571-751-175-751-751-715-576-7_V.mp3)
  Segment 25: 68316ms ~ 68892ms (file: 731-671-756-756-756 176-315-136-132-136-173-130-716-763-673-175-317-574-571-571-576-751-567-506-507-567-765-106-350-576-637-736-763-365-765-765-756-756-657-765-573-756-657-136-1_W.mp3)
  Segment 26: 69892ms ~ 75796ms (file: 132-276-267-726-106-267-267-123-130-231-230-237-267-103-621-167-102-716-260-267-261-210-102-236-672-123-163-627-206-316-276-327-102-752-237-276-120-106-617-206-267-263-106-167-3_V.mp3)
  Segment 27: 76796ms ~ 77372ms (file: 574-547-571-571-570-570-574-573-571-576-571-576-751-571-571-571-571 576-574-574-571-574-574-574-574-574_W.mp3)
  Segment 28: 78372ms ~ 84780ms (file: 206-120-126-103-206-201-756-276-721-571-206-672-620-126-267-672-765-756-102-103-127-201-620-216-201-567-176-167-167-617-216-102-216-201-201-201-206-762-201-201-120-230-230-120-1_V.mp3)
  Segment 29: 85780ms ~ 86356ms (file: 321-673-762-623 123-312-537-573-536-537_V.mp3)
  Segment 30: 87356ms ~ 87932ms (file: 675-576-576-567-527-213-261-276-175-213-237-105-175-570-716-273-625-312-532-312-327-261-216-263-132-712-215-512-726-216-273-725-251-217-750 217-567-507-275-527-217-513-720-276-3_V.mp3)
  Segment 31: 88932ms ~ 100236ms (file: 632-132-752-312-375-263-753-271-624-765-756-132-571-531-130-753-230-756-761-672-623-301-206-216-572-570-263-326-267-267-260-103-201-320-573-275-725-756-237-257-301-321-132-726-2_W.mp3)
  Segment 32: 101236ms ~ 101812ms (file: 372-751-173-375-375-735-501-517-567-753-361-136-563-675-317-107-163-126-615-705-765-217-236-507-132-123-163-257-321-317-137-173-376-710-726-136-705-572-653-175-715-123-176-762-3_W.mp3)
  Segment 33: 102812ms ~ 103388ms (file: 517-501-570-501-510-531-571-513-351-137-573-153-537-517-571-513-573-153-315-135 570-574-574-507-571-531-510-571-531-157-735-150-157-573-753-713-315-750-573-571-175-517-751-750-5_V.mp3)
  Segment 34: 104388ms ~ 104964ms (file: 107-751-715-175-173-715-175-175-175-175-175-176-176-715-175-170-710 106-176-170-716-176-175-105-175-701-701-175-710-170-170-713-751-715-715-175-715-715-715-175-715-170-170-710-7_V.mp3)
  Segment 35: 105964ms ~ 116692ms (file: 756-736-625-267-130-312-312-213-317-275-210-167-735-123-320-132-123-132-537-317-751-753-573-572-137-571-273-320-213-362 320-132-123-213-102-301-312-132-312-361-130-301-136-137-3_W.mp3)
  Segment 36: 117692ms ~ 123740ms (file: 623-320-362-267-176-751-372-675-236-263-627-310-236-231-612-136-675-120-267-267-123-102-120-216-273-237-273-372-316-321-325-574-514-361-263-537-130-160-617-167-267-162-270-257-2_W.mp3)
  Segment 37: 124740ms ~ 125316ms (file: 753-756-675-657-765-657-136-157-120-571-613-162-132-762-751-130-106-120-531-156-315-130-316-132-130-517-105-137-316-130-613-157-310-301-132-317-371-312-316-362-123-136-163-632-1_W.mp3)
  Segment 38: 126316ms ~ 126892ms (file: 105-107-150-510-517 105-157-510-517 513-105-105-513-157-351-105-150-510 517-150-510-517-105-135-103-150-105-153-501-105-150-105 103-315-103-105-150-105-157-135-715-236 513-315-5_V.mp3)
  Segment 39: 127892ms ~ 128468ms (file: 173-170-175-175-715-715-715-715-715-715-751-715-715-715 175-175-157-715-170-710-750-710-105-175-710-701-715-710-175-175-175-715-715-176-175-716-715-751-715-175-756-576-756-576-7_V.mp3)
  Segment 40: 129468ms ~ 130044ms (file: 157-157-517-157-157-517-517-517-576-517-571-576-571-135-315-103-103-135-137-135-137-103-107-175-157-157-175-157-157-156-157-157-157-517-157-571-157-517-103-103-157-157-157-175-1_V.mp3)
  Segment 41: 131044ms ~ 131620ms (file: 275-761-725-765-167-657-351-527-567-761-715-675-615-571-567-516-157-536-756-576-751-765-736-163-127-167-136-765-756-765-762-765-376-765-762-765-137-167-612-175-735-765-531-756-3_W.mp3)
  Segment 42: 132620ms ~ 142124ms (file: 620-621-632-231-216-216-263-263-260-236-326-321-267-127-127-372-213-107-215-257-273-213-236-267-260-725-132-123-320-213-201-216-372-726-210-765-760-107-765-763-230-135-312-731-1_W.mp3)
  Segment 43: 143124ms ~ 143700ms (file: 137 671-176-517-367-135-163-523-571-756-756-762-673-760-673-672-765-726-605-637-631 610-302-270-726-673-637-673-167-715-376 176-267-635-523-136 310-130-136-316-136-316-706-372-7_W.mp3)
  Segment 44: 144700ms ~ 145276ms (file: 627-236-276-756-572-576-571-276-265-253-657-570-756-627-675-502-273-507-325-526-326_V.mp3)
  Segment 45: 146276ms ~ 154520ms (file: 163-134-301-261-134-761-637-213-216-132-216-312-312-325-765-671-671-276-213-103-103-132-123-321-263-236-103-170-123-102-765-721-127-126-213-230-312-317-361-763-753-705-735-715-7_W.mp3)
  Segment 46: 155520ms ~ 166428ms (file: 574-103-216-621-524-235-532-236-312-513-312-260-260-260-267-267-326-103-157-301-310-762-326-765-375-123-216-762-675-617-762-367-730-571-716-317-612-620-357-137-132-132-126-672-6_W.mp3)
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
> **Time**: 2026-04-06 08:54:33
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
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 50, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 3,
"transcript": "오늘 날씨가 정말 화창하고 좋네요 산책 가고 싶어요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 120, "dur_ms": 400, "phase_lr_deg": 2.5},
{"word": "날씨가", "start_ms": 550, "dur_ms": 500, "phase_lr_deg": 2.0},
{"word": "정말", "start_ms": 1100, "dur_ms": 450, "phase_lr_deg": 1.5},
{"word": "화창하고", "start_ms": 1600, "dur_ms": 700, "phase_lr_deg": 1.0},
{"word": "좋네요", "start_ms": 2400, "dur_ms": 550, "phase_lr_deg": 0.5},
{"word": "산책", "start_ms": 3200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "가고", "start_ms": 3700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "싶어요", "start_ms": 4150, "dur_ms": 500, "phase_lr_deg": -0.5}
]
},
{
"segment": 4,
"transcript": "I think learning Korean is very interesting but difficult",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 80, "dur_ms": 150, "phase_lr_deg": -1.0},
{"word": "think", "start_ms": 250, "dur_ms": 300, "phase_lr_deg": -1.0},
{"word": "learning", "start_ms": 600, "dur_ms": 500, "phase_lr_deg": -0.5},
{"word": "Korean", "start_ms": 1150, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 1750, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "very", "start_ms": 2000, "dur_ms": 350, "phase_lr_deg": 0.5},
{"word": "interesting", "start_ms": 2400, "dur_ms": 800, "phase_lr_deg": 1.0},
{"word": "but", "start_ms": 3250, "dur_ms": 250, "phase_lr_deg": 1.0},
{"word": "difficult", "start_ms": 3550, "dur_ms": 700, "phase_lr_deg": 1.5}
]
},
{
"segment": 5,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 6,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "네 맞아요",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "맞아요", "start_ms": 280, "dur_ms": 250, "phase_lr_deg": -5.0}
]
},
{
"segment": 9,
"transcript": "가장 좋아하는 한국 음식은 무엇인가요 저는 비빔밥을 좋아해요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "가장", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "좋아하는", "start_ms": 600, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 1250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "음식은", "start_ms": 1750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "무엇인가요", "start_ms": 2300, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "저는", "start_ms": 3400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "비빔밥을", "start_ms": 3850, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "좋아해요", "start_ms": 4600, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "불고기도 맛있어요",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "불고기도", "start_ms": 60, "dur_ms": 300, "phase_lr_deg": -3.0},
{"word": "맛있어요", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": -3.0}
]
},
{
"segment": 11,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 12,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 13,
"transcript": "점심 메뉴 정하셨나요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "점심", "start_ms": 90, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "메뉴", "start_ms": 320, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "정하셨나요", "start_ms": 500, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 15,
"transcript": "아직요 냉면 어때요 오늘 좀 덥네요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "아직요", "start_ms": 100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "냉면", "start_ms": 800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "어때요", "start_ms": 1300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 2000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 2450, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "덥네요", "start_ms": 2700, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 17,
"transcript": "좋은 생각이에요",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 50, "dur_ms": 250, "phase_lr_deg": -4.0},
{"word": "생각이에요", "start_ms": 320, "dur_ms": 200, "phase_lr_deg": -4.0}
]
},
{
"segment": 18,
"transcript": "그럼 가시죠",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 40, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "가시죠", "start_ms": 320, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "혹시 일본어도 할 줄 아세요 아니요 조금밖에 못해요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "혹시", "start_ms": 200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "일본어도", "start_ms": 650, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 1350, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "줄", "start_ms": 1550, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "아세요", "start_ms": 1750, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "아니요", "start_ms": 2800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "조금밖에", "start_ms": 3400, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "못해요", "start_ms": 4150, "dur_ms": 600, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 22,
"transcript": "공부하면 금방 늘 거예요 화이팅",
"speaker": "speaker_3",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "공부하면", "start_ms": 80, "dur_ms": 120, "phase_lr_deg": -2.0},
{"word": "금방", "start_ms": 220, "dur_ms": 80, "phase_lr_deg": -2.0},
{"word": "늘", "start_ms": 310, "dur_ms": 50, "phase_lr_deg": -2.0},
{"word": "거예요", "start_ms": 370, "dur_ms": 100, "phase_lr_deg": -2.0},
{"word": "화이팅", "start_ms": 480, "dur_ms": 90, "phase_lr_deg": -2.0}
]
},
{
"segment": 23,
"transcript": "고마워요 열심히 해볼게요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "고마워요", "start_ms": 70, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "열심히", "start_ms": 350, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "해볼게요", "start_ms": 480, "dur_ms": 90, "phase_lr_deg": 0.0}
]
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
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 26,
"transcript": "주말에는 보통 뭐 하세요 저는 집에서 영화 보는 걸 좋아해요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "주말에는", "start_ms": 180, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "보통", "start_ms": 850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 1300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "하세요", "start_ms": 1500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "저는", "start_ms": 2400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "집에서", "start_ms": 2850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "영화", "start_ms": 3400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "보는", "start_ms": 3900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "걸", "start_ms": 4300, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "좋아해요", "start_ms": 4550, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 28,
"transcript": "오 저도요 어떤 장르 좋아하세요 액션 영화 좋아해요",
"speaker": "speaker_3",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "오", "start_ms": 120, "dur_ms": 300, "phase_lr_deg": -3.0},
{"word": "저도요", "start_ms": 500, "dur_ms": 500, "phase_lr_deg": -3.0},
{"word": "어떤", "start_ms": 1200, "dur_ms": 400, "phase_lr_deg": -3.0},
{"word": "장르", "start_ms": 1650, "dur_ms": 450, "phase_lr_deg": -3.0},
{"word": "좋아하세요", "start_ms": 2150, "dur_ms": 700, "phase_lr_deg": -3.0},
{"word": "액션", "start_ms": 3200, "dur_ms": 500, "phase_lr_deg": -3.0},
{"word": "영화", "start_ms": 3750, "dur_ms": 450, "phase_lr_deg": -3.0},
{"word": "좋아해요", "start_ms": 4250, "dur_ms": 600, "phase_lr_deg": -3.0}
]
},
{
"segment": 29,
"transcript": "멋지네요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "멋지네요", "start_ms": 30, "dur_ms": 540, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "나중에 같이 보러 가요",
"speaker": "speaker_1",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "나중에", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 270, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "보러", "start_ms": 390, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "가요", "start_ms": 510, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "그럴까요 시간 날 때 연락 주세요 알겠어요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "그럴까요", "start_ms": 150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "시간", "start_ms": 1100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "날", "start_ms": 1550, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 1750, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "연락", "start_ms": 2000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 2500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "알겠어요", "start_ms": 3400, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 33,
"transcript": "벌써 시간이 이렇게 됐네요",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "벌써", "start_ms": 60, "dur_ms": 150, "phase_lr_deg": -2.0},
{"word": "시간이", "start_ms": 230, "dur_ms": 120, "phase_lr_deg": -2.0},
{"word": "이렇게", "start_ms": 360, "dur_ms": 100, "phase_lr_deg": -2.0},
{"word": "됐네요", "start_ms": 480, "dur_ms": 90, "phase_lr_deg": -2.0}
]
},
{
"segment": 34,
"transcript": "이제 가봐야겠어요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 40, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "가봐야겠어요", "start_ms": 320, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "오늘 만나서 즐거웠습니다 조심히 들어가세요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 140, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "만나서", "start_ms": 650, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "즐거웠습니다", "start_ms": 1300, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "조심히", "start_ms": 2500, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "들어가세요", "start_ms": 3200, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "다음에 또 봬요 안녕히 계세요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 120, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 650, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "봬요", "start_ms": 900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 1600, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "계세요", "start_ms": 2300, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 38,
"transcript": "잘 가요",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "잘", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "가요", "start_ms": 210, "dur_ms": 320, "phase_lr_deg": -5.0}
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
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 42,
"transcript": "한국어 공부는 꾸준히 하는 게 제일 중요해요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "공부는", "start_ms": 700, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "꾸준히", "start_ms": 1300, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 1950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 2400, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "제일", "start_ms": 2650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "중요해요", "start_ms": 3150, "dur_ms": 700, "phase_lr_deg": 0.0}
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
"transcript": "포기하지 마세요",
"speaker": "speaker_3",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "포기하지", "start_ms": 60, "dur_ms": 250, "phase_lr_deg": -2.0},
{"word": "마세요", "start_ms": 350, "dur_ms": 160, "phase_lr_deg": -2.0}
]
},
{
"segment": 45,
"transcript": "매일 조금씩이라도 듣고 말하는 연습을 하세요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "매일", "start_ms": 120, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "조금씩이라도", "start_ms": 550, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "듣고", "start_ms": 1400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "말하는", "start_ms": 1850, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "연습을", "start_ms": 2450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "하세요", "start_ms": 3000, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "그러면 어느새 실력이 많이 늘어 있을 거예요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "그러면", "start_ms": 100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "어느새", "start_ms": 650, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "실력이", "start_ms": 1300, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 1900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "늘어", "start_ms": 2350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "있을", "start_ms": 2800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 3250, "dur_ms": 500, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 08:54:33*
