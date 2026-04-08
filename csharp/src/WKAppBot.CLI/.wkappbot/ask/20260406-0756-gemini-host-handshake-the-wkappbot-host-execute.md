# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=30344  time=07:54:11  uptime=15267s  mem=1079MB  system_processes=413"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_075116.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 13212ms (file: 230-123-162-167-716-751-672-102-321-257-321-713-751-132-126-265 574_W.mp3)
  Segment 2: 14212ms ~ 14788ms (file: 361-631-312 136-213-312-760-761 312-136-312_V.mp3)
  Segment 3: 15788ms ~ 16364ms (file: 275-256-263-527-752-752-175-265 265-726-217-627-721-127-312-705-576-236-265-512-235-576-562-527-507-752-265-623-627-621-753-273-572-275-675-127-702-267-672-507-267-527-567-760-2_V.mp3)
  Segment 4: 17364ms ~ 17940ms (file: 751-735-657-517-631-361-316-316-136-316-316-136-136-316-136-130-312-132-130-132-312_W.mp3)
  Segment 5: 18940ms ~ 19516ms (file: 731-715-713-756-715-756-756-756-756-765-756-756-765-753-753-765-317-301_W.mp3)
  Segment 6: 20516ms ~ 21092ms (file: 135-315-315-135-312-607-130-751-137-317-317-517-327-321-312-103-127-271-137-213-231-321-312-103-132-312-130-317-317-236-312-367-357-351-506-675-672-627-627-267-762-375-371-361-6_V.mp3)
  Segment 7: 22092ms ~ 22668ms (file: 672-612-612-267-672-726-316-631-627-361-267-237-635-765-762-762-756-126-630-672-106-153-120-102-105-130-103-130-213-253-325-321-325-321-312-723-753-357-312-735_W.mp3)
  Segment 8: 23668ms ~ 24244ms (file: 170-130-130-130-137-135-136-130-130-675-675-675-675-675-675-657-657-657-756-765-756-756-765-762-675-172-671-132-317-135-315-135-137-173-175-137-172-135-317-357-513-735-753-765-1_W.mp3)
  Segment 9: 25244ms ~ 31652ms (file: 260-327-231-216-576-302-312-321-321-231-257-705-376-273-267-572-275-723-372-263-276-765-276-752-765-260-672-576-271-320-312-231-752-537-276-756-567-756-370-310-103-213-752-175-1_W.mp3)
  Segment 10: 32652ms ~ 33228ms (file: 756-576-576-567-750-617-751-175-715-715-716-716-715-715-715-576-157-167-156-176-574 167-163-103-715-710-170-705-710-710-701-701-701-715-170-175-170-701-710-715-701-705-710-715-7_V.mp3)
  Segment 11: 34228ms ~ 42940ms (file: 120-263-130-102-267-627-367-132-532-267-752-306-372-320-253-236-163-325-306-362-620-325-350-325-273-206-267-765-673-763-207-230-257-236-765-273-320-312-357-321-325-130-176-157-1_W.mp3)
  Segment 12: 43940ms ~ 50312ms (file: 257-627-362-316-765-726-237-312-317-312-253-572-756-106-160-361-316-321-236-375-312-601-107-310-310-570-267-726-276-670-326-132-273-316-537-301-263-271-507-627-762-367-123-160-1_W.mp3)
  Segment 13: 51312ms ~ 51888ms (file: 672-312-136-632-176-635-375-657-673-675-675-675-675-657-765-675-675-675-657-675-706-760-761-765-613 312_W.mp3)
  Segment 14: 52888ms ~ 53464ms (file: 517-310-103-132-735-713-103-312-263-657-706-136-317-310-316-310-316-136-310-130-306-321-312-375-735-357-504-756-371-716-631-312-306-601_W.mp3)
  Segment 15: 54464ms ~ 62240ms (file: 760-756-753-132-273-573-312-103-127-201-237-762-760-716-207-132-201-206-127-213-231-673-670-120-754-627-126-217-230-260-207-261-267-260-213-102-321-607-621-725-765-675-123-132-2_W.mp3)
  Segment 16: 63240ms ~ 71916ms (file: 210-173-712-231-751-275-230-321-217-750-175-752-205-201-756-715-130-203-756-132-574-735-102-210-753-173-175-167-320-237-217-752-756-130-102-103-103-132-312-123-231-102-210-206-1_W.mp3)
  Segment 17: 72916ms ~ 82348ms (file: 312-763-312-102-752-201-130-310-130-136-312-132-216-123-312-103-102-130-157-275-213-132-321-130-670-320-213-143-203-310-752-752-163-207-237-257-201-370-702-103-310-310-260-102-1_W.mp3)
  Segment 18: 83348ms ~ 90872ms (file: 132-310-756-172-267-261-270-276-276-726-716-137-672-756-201-752-756-712-756-136-267-752-276-276-762-167-762-726-261-260-276-102-164-137-731-107-671-701-761-726-710-703-765-756-6_W.mp3)
  Segment 19: 91872ms ~ 92448ms (file: 571-756-576-627-716-271-721 576-756-576-576-751-576-570-750-517-751-567-750-751-570-756-705-750-157-175-751-750-570-157-756-576-756-756 576-576-576 574-574-574-574-574-576-576 5_W.mp3)
  Segment 20: 93448ms ~ 100900ms (file: 132-136-120-157-301-310-301-132-167-127-310-730-312-163-371-165-106-173-230-321-216-163-631-316-627-756-572-237-273-102-302-127-312-135-130-132-570-157-167-176-130-675-132-103-2_V.mp3)
  Segment 21: 101900ms ~ 102476ms (file: 627-275-312-237-570-752-132-560-532-617-675-671-673-671-657-653-657-315-132-651-657-315-157-156-536-365-136-765-735_W.mp3)
  Segment 22: 103476ms ~ 104052ms (file: 572-250-725-267-502-765-726-547-576-325-507-762-570-502-265-276-267-312-257-567-257-652-236-275-615-725-625-623-206-570-236-162-726-265-623-672-361-316-106-176-376-357-715-567-7_W.mp3)
  Segment 23: 105052ms ~ 111964ms (file: 213-102-106-130-317-316-103-130-761-126-320-327-756-672-312-162-627-362-327-302-213-236-376-367-276-301-132-361-263-312-723-207-170-352-317-672-236-326-253-106-675-301-260-625-6_W.mp3)
  Segment 24: 112964ms ~ 113540ms (file: 762-765-763-756-172-132-137-136-172-763-376-673-176 513-517-751-756-765-765-571-751-715-736-176-703-751-765-765-760-750-756-765-765-756-762-705-756-765-715_V.mp3)
  Segment 25: 114540ms ~ 122208ms (file: 103-123-236-231-263-620-263-123-103-310-107-627-263-103-302-301-103-103-312-302-312-213-132-103-263-236-123-103-326-206-320-231-302-623-302-123-260-620-102-160-317-706-673-376-2_V.mp3)
  Segment 26: 123208ms ~ 130948ms (file: 102-675-571-127-231-236-130-163-175-152-231-236-372-275-752-132-132-236-267-123-263-267-263-325-267-236-132-267-162-617-103-123-143-765-267-756-103-312-321-327-132-103-132-103-1_W.mp3)
  Segment 27: 131948ms ~ 138176ms (file: 213-620-235-207-263-217-120-310-720-201-312-530-213-132-130-270-260-701-170-136-231-716-760-126-312-213-201-507-726-762-126-726-107-157-316-312-236-631-320-652-301-301-263-627-5_V.mp3)
  Segment 28: 139176ms ~ 139752ms (file: 750-756-753-753-735 735-753-735-735-750-756-756-375-735-312-130-765-761-130-135 163 275_W.mp3)
  Segment 29: 140752ms ~ 148852ms (file: 167-617-657-675-675-657-167-613-617-671-105-175-136-571-153-135-315-135-105-130-672-176-130-126-312-613-301-132-173-326-672-603-103-167-103-306-627-627-317-301-137-761-260-231-3_W.mp3)
  Segment 30: 149852ms ~ 150428ms (file: 157-571-571-571-572-572-571-572-751-571-157-573-573 547-574-574-574-574-571-571-517-157-571-517-517-517-513-531-517-517-571-517-753-571-537 517-517-571-517-571-573-571_W.mp3)
  Segment 31: 151428ms ~ 152004ms (file: 570-207-217-627-235-705-753-325-236-536-725-572-257-267-736-312-762-752-231-326-621-752-236-132-321-365-325-627-570-625-235-657-504-325-726-725-275-570-726-312-675-526-152-752-3_V.mp3)
  Segment 32: 153004ms ~ 158332ms (file: 270-623-573-127-213-276-267-672-206-572-576-157-107-107-150-327-570-672-627-726-765-762-607-723-231-217-231-263-725-371-320-201-752-276-120-216-267-267-723-756-267-231-671-312-1_W.mp3)
  Segment 33: 159332ms ~ 159908ms (file: 310-573-715-157-135-173-315-137-175-175-571-351-375-325-312-103_W.mp3)
  Segment 34: 160908ms ~ 161484ms (file: 751-501-125-756-176-572-217-756-172-576-357-365-321-236-172-312-731-132-762-576-236-736-736-132-756-173-572-152-715-756-765-617-751-507-257-251-574-671-765-705-570-756-752-675-7_V.mp3)
  Segment 35: 162484ms ~ 163060ms (file: 160-631-617-671-671-657-657-653-132-132-312-132-132-312-132-136 571-570-571-570-753-137-152-153-531-153-157-571-175-312-173-375-715_V.mp3)
  Segment 36: 164060ms ~ 170108ms (file: 265-527-375-370-137-137-310-207-607-307-715-132-705-576-275-261-213-301-271-132-263-762-265-672-267-201-205-123-206-267-750-572-216-321-623-576-267-157-102-312-762-276-136-123-7_W.mp3)
  Segment 37: 171108ms ~ 171684ms (file: 576-675-176-705-512-126-750-754-725-567-753-136-572-321-572-256-502 267-623-316-735-167-327-237-215-756-732-176-507-321-735-253-527-132-716-123-315-752-123-256-376-725-326-312-7_V.mp3)
  Segment 38: 172684ms ~ 180064ms (file: 213-326-527-263-103-513-132-230-312-132-320-167-167-261-206-123-213-132-237-103-621-267-231-106-103-125-136-102-126-261-326-627-132-236-321-316-602-572-750-136-627-267-126-236-5_W.mp3)
  Segment 39: 181064ms ~ 193160ms (file: 761-762-367-317-130-123-102-312-236-574-356-354-253-236-103-230-325-326-725-210-326-672-271-723-372-273-362-237-102-136-103-236-753-310-267-627-276-237-362-602-620-675-675-310-1_W.mp3)
  Segment 40: 194160ms ~ 200460ms (file: 361-132-302-320-627-627-267-730-301-163-267-627-627-260-260-260-672-162-132-564-206-762-627-130-137-217-123-123-120-103-137-372-213-102-372-216-613-267-316-726-123-316-312-231-7_W.mp3)
  Segment 41: 201460ms ~ 210784ms (file: 301-230-562-230-320-320-263-602-263-132-230-621-104-310-231-301-310-103-230-236-230-230-206-321-320-230-103-301-132-302-320-321-321-230-320-267-230-203-230-206-527-236-231-263-2_W_stt_every_security_assessment_of_routed_by_s.mp3)
  Segment 42: 211784ms ~ 212360ms (file: 621-163-675-763-261-632-326-371-326-572-752-756-762-203-312 326-130_W.mp3)
  Segment 43: 213360ms ~ 221532ms (file: 123-753-130-103-123-123-251-107-120-132-231-213-762-172-130-217-271-276-271-271-217-271-210-217-271-217-276-120-126-123-321-267-276-213-731-312-573-231-120-302-312-751-752-276-2_W.mp3)
  Segment 44: 222532ms ~ 223108ms (file: 576-571-567-576-576-576-576-756-756-567-576-756-576-756-574-507-576-576-135-546-576-756-765-756-576-570-574-316-316-762-672 635-263-571-125-627-627-517-312-705-726-762-672-237-7_W.mp3)
  Segment 45: 224108ms ~ 224684ms (file: 756-576-576-756-576-750-570-376-123-762-167-216-267-507-527-510-761-631-137-750-632-321-572-235-537-317-756-765-351-750 527-672-762-257-625-276-257-265-526-751-736-617-163-756-1_V.mp3)
  Segment 46: 225684ms ~ 226260ms (file: 157-165-517-567-576-516-570-576-567-156-516-156-751-576-652-657-567-125-152-157-135-132-615-165-517-571-572-657-576-572-562-567-125-756-576-567-756-527-576-567-756-567-615-756-1_W.mp3)
  Segment 47: 227260ms ~ 227836ms (file: 750-756-751-756-751-576-571 576-216-253-162-137-532-315 316-125-570-162-263-163-723-267-756-756-357-756-506-560-576-573-547-576-576-756-753-573-753-731-375-623_W.mp3)
  Segment 48: 228836ms ~ 229412ms (file: 715-175-716-751-715-567-526-267-567-576-651-103-130-102-102-102-523-201-210-105-163-130-102-106-103-172-165-561-125-120-160-516-751-176-127-107-710-170-716-751-176-175-175-710-1_W.mp3)
  Segment 49: 230412ms ~ 230988ms (file: 312-236-671-267-162-632-362-362-263-637-672-672-362-736-721-635-361-627-675-312-372-371-127-317-756-756-756-751-571-751-576-576-657-675-617-162-312-163-126-162-152-652-625-625-6_W.mp3)
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
> **Time**: 2026-04-06 07:56:09
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "여러분 안녕하세요 오늘 공부할 단어들을 함께 살펴봅시다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "여러분", "start_ms": 150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "안녕하세요", "start_ms": 800, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 1800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "공부할", "start_ms": 2250, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "단어들을", "start_ms": 2900, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "함께", "start_ms": 3650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "살펴봅시다", "start_ms": 4150, "dur_ms": 1100, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 4,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 9,
"transcript": "첫 번째 단어는 공항입니다 공항에 가서 비행기를 타요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "첫", "start_ms": 180, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "번째", "start_ms": 400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "단어는", "start_ms": 750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "공항입니다", "start_ms": 1300, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "공항에", "start_ms": 2400, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "가서", "start_ms": 3050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "비행기를", "start_ms": 3600, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "타요", "start_ms": 4400, "dur_ms": 500, "phase_lr_deg": 0.0}
]
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
"transcript": "Airport I go to the airport to take a plane",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 220,
"audio_type": "speech",
"words": [
{"word": "Airport", "start_ms": 220, "dur_ms": 800, "phase_lr_deg": 5.0},
{"word": "I", "start_ms": 1500, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "go", "start_ms": 1700, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "to", "start_ms": 1950, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "the", "start_ms": 2150, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "airport", "start_ms": 2350, "dur_ms": 650, "phase_lr_deg": 5.0},
{"word": "to", "start_ms": 3100, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "take", "start_ms": 3350, "dur_ms": 300, "phase_lr_deg": 5.0},
{"word": "a", "start_ms": 3700, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "plane", "start_ms": 3850, "dur_ms": 600, "phase_lr_deg": 5.0}
]
},
{
"segment": 12,
"transcript": "두 번째 단어는 도서관입니다 도서관에서 책을 읽어요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "두", "start_ms": 150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "번째", "start_ms": 350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "단어는", "start_ms": 750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "도서관입니다", "start_ms": 1300, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "도서관에서", "start_ms": 2500, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "책을", "start_ms": 3450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "읽어요", "start_ms": 3900, "dur_ms": 700, "phase_lr_deg": 0.0}
]
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
"transcript": "Library I read books at the library",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "Library", "start_ms": 200, "dur_ms": 800, "phase_lr_deg": 5.0},
{"word": "I", "start_ms": 1500, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "read", "start_ms": 1700, "dur_ms": 300, "phase_lr_deg": 5.0},
{"word": "books", "start_ms": 2050, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "at", "start_ms": 2500, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "the", "start_ms": 2700, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "library", "start_ms": 2900, "dur_ms": 700, "phase_lr_deg": 5.0}
]
},
{
"segment": 16,
"transcript": "마지막 단어는 식당입니다 식당에서 맛있는 음식을 먹어요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 300,
"audio_type": "speech",
"words": [
{"word": "마지막", "start_ms": 300, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "단어는", "start_ms": 950, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "식당입니다", "start_ms": 1500, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "식당에서", "start_ms": 2600, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "맛있는", "start_ms": 3450, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "음식을", "start_ms": 4100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "먹어요", "start_ms": 4650, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "Restaurant I eat delicious food at the restaurant",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "Restaurant", "start_ms": 250, "dur_ms": 900, "phase_lr_deg": 5.0},
{"word": "I", "start_ms": 1600, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "eat", "start_ms": 1800, "dur_ms": 250, "phase_lr_deg": 5.0},
{"word": "delicious", "start_ms": 2100, "dur_ms": 700, "phase_lr_deg": 5.0},
{"word": "food", "start_ms": 2850, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "at", "start_ms": 3300, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "the", "start_ms": 3500, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "restaurant", "start_ms": 3700, "dur_ms": 800, "phase_lr_deg": 5.0}
]
},
{
"segment": 18,
"transcript": "오늘 배운 내용을 잘 기억하세요 다음 시간에 또 만나요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "배운", "start_ms": 550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "내용을", "start_ms": 1000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 1550, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "기억하세요", "start_ms": 1800, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 2900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "시간에", "start_ms": 3350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 3900, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 4150, "dur_ms": 600, "phase_lr_deg": 0.0}
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
"transcript": "Goodbye everyone Have a great day",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "Goodbye", "start_ms": 200, "dur_ms": 700, "phase_lr_deg": 5.0},
{"word": "everyone", "start_ms": 1000, "dur_ms": 700, "phase_lr_deg": 5.0},
{"word": "Have", "start_ms": 2000, "dur_ms": 300, "phase_lr_deg": 5.0},
{"word": "a", "start_ms": 2350, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "great", "start_ms": 2500, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "day", "start_ms": 2950, "dur_ms": 500, "phase_lr_deg": 5.0}
]
},
{
"segment": 21,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
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
"transcript": "그럼 이제부터는 일본어로 인사를 배워볼까요",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 350,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이제부터는", "start_ms": 800, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "일본어로", "start_ms": 1650, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "인사를", "start_ms": 2300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "배워볼까요", "start_ms": 2850, "dur_ms": 900, "phase_lr_deg": 0.0}
]
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
"transcript": "こんにちは はじめまして よろしくお願いします",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "ja",
"silence_before_ms": 400,
"audio_type": "speech",
"words": [
{"word": "こんにちは", "start_ms": 400, "dur_ms": 1000, "phase_lr_deg": -10.0},
{"word": "はじめまして", "start_ms": 1600, "dur_ms": 1200, "phase_lr_deg": -10.0},
{"word": "よろしくお願いします", "start_ms": 3000, "dur_ms": 1800, "phase_lr_deg": -10.0}
]
},
{
"segment": 26,
"transcript": "아주 잘하셨어요 발음이 정말 좋습니다",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "아주", "start_ms": 150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "잘하셨어요", "start_ms": 550, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "발음이", "start_ms": 1400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 1950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "좋습니다", "start_ms": 2400, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "이제 마지막으로 한 번만 더 큰 소리로 따라하세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "마지막으로", "start_ms": 600, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 1450, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "번만", "start_ms": 1700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 2100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 2350, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "소리로", "start_ms": 2650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "따라하세요", "start_ms": 3200, "dur_ms": 1000, "phase_lr_deg": 0.0}
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
"transcript": "감사합니다 수고하셨습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 150, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "수고하셨습니다", "start_ms": 1150, "dur_ms": 1100, "phase_lr_deg": 0.0}
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
"transcript": "자 그럼 노래와 함께 마무리하겠습니다",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "자", "start_ms": 200, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "그럼", "start_ms": 450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "노래와", "start_ms": 900, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "함께", "start_ms": 1550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "마무리하겠습니다", "start_ms": 2050, "dur_ms": 1200, "phase_lr_deg": 0.0}
]
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
"transcript": "라라라 즐거운 한국어 공부 시간",
"speaker": "speaker_4",
"confidence": 0.88,
"language": "ko",
"silence_before_ms": 400,
"audio_type": "mixed",
"words": [
{"word": "라라라", "start_ms": 400, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "즐거운", "start_ms": 1600, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "한국어", "start_ms": 2300, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "공부", "start_ms": 3100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "시간", "start_ms": 3700, "dur_ms": 600, "phase_lr_deg": 0.0}
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
"transcript": "모두가 웃으며 인사해요 안녕 안녕",
"speaker": "speaker_4",
"confidence": 0.89,
"language": "ko",
"silence_before_ms": 300,
"audio_type": "mixed",
"words": [
{"word": "모두가", "start_ms": 300, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "웃으며", "start_ms": 1000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "인사해요", "start_ms": 1700, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "안녕", "start_ms": 2600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "안녕", "start_ms": 3100, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "정말 고생 많으셨습니다 안녕히 가세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "고생", "start_ms": 600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "많으셨습니다", "start_ms": 1050, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 2200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 2800, "dur_ms": 600, "phase_lr_deg": 0.0}
]
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
"transcript": "Every security assessment is routed by service.",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "Every", "start_ms": 250, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "security", "start_ms": 700, "dur_ms": 600, "phase_lr_deg": 5.0},
{"word": "assessment", "start_ms": 1350, "dur_ms": 800, "phase_lr_deg": 5.0},
{"word": "is", "start_ms": 2200, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "routed", "start_ms": 2400, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "by", "start_ms": 2950, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "service.", "start_ms": 3200, "dur_ms": 600, "phase_lr_deg": 5.0}
]
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
"transcript": "끝까지 시청해주셔서 대단히 감사합니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "끝까지", "start_ms": 150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "시청해주셔서", "start_ms": 800, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "대단히", "start_ms": 1900, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 2600, "dur_ms": 900, "phase_lr_deg": 0.0}
]
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
*Generated by WKAppBot ask gemini — 2026-04-06 07:56:09*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=19672  time=07:54:16  uptime=12073s  mem=235MB  system_processes=413"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_075202.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 4032ms (file: 613-650-650-630-657-657-657-653-356-351-315-165-163-136-135-571-157-531-517-157-136-137-267-157-752-265-672-651-105-170-321-173-563-362-365-326-362-362-321-725-362-362-327-752-3_W.mp3)
  Segment 2: 5032ms ~ 5608ms (file: 751 574-574-574-576-752-751-756-752-752-751-756-570-572-574-576-574-570-571-570-750-571-507-570 756-576-570-571-570-570-570-751-571-571-517-571-750-751 574-572-570-570-570-576-5_W.mp3)
  Segment 3: 6608ms ~ 23960ms (file: 302-320-267-136-321-230-102-267-201-237-213-103-623-130-231-260-312-320-352-312-132-103-312-214-172-372-261-173-132-130-102-130-367-517-120-103-267-267-170-752-726-261-213-123-1_W.mp3)
  Segment 4: 24960ms ~ 38856ms (file: 126-310-231-321-312-132-310-310-672-213-216-215-257-213-320-230-321-312-107-312-320-231-267-627-760-207-276-130-163-173-271-237-236-103-267-327-572-572-675-172-127-312-132-137-1_W.mp3)
  Segment 5: 39856ms ~ 40432ms (file: 507-150-507-103-753 513-517-571-571-517-572-517-507-510-570-576-517-170-157-157-571 507-570-573-517-517-517-510-513-503-571-570-507 573-571-571-571-571-576-157-513-503-517-571-1_W.mp3)
  Segment 6: 41432ms ~ 52268ms (file: 267-170-213-103-210-276-620-621-126-276-307-703-103-601-673-135-312-310-521-102-756-751-132-672-670-327-312-756-102-273-752-715-207-136-103-321-326-312-106-573-675-326-326-236-3_W.mp3)
  Segment 7: 53268ms ~ 60144ms (file: 276-627-672-276-267-213-137-130-316-312-102-301-310-301-316-203-231-275-127-132-235-123-321-375-573-752-572-157-107-213-231-273-271-273-263-237-236-576-172-572-215-137-130-271-1_W.mp3)
  Segment 8: 61144ms ~ 61720ms (file: 571-752-571-751-571-750-572-573-751-756 576-756-572-756-571-571-574-571-576-751-576-573-570-751-571-572-572-572-572 751-751-753-750-753-751-750 572-752-576-753-751-571-572-752-7_W.mp3)
  Segment 9: 62720ms ~ 75140ms (file: 237-213-631-576-136-367-276-572-567-573-623-675-765-574-237-276-207-352-671-263-675-705-415-670-761-250-560-156 627-275-163-256-576-256-726-237-705-123-521-750-365-725-321-256-5_V_stt_at.mp3)
  Segment 10: 76140ms ~ 76716ms (file: 321-537-765-725-756-705-321-256-263-352-670-263-327-267-267-261-263-321-231-750-315-251-231-750-312-132-123-352-506-267-216-637-723-761-637-672-675-701-571-753-751-715-715-753-7_W.mp3)
  Segment 11: 77716ms ~ 78292ms (file: 321-271-263-312-675-637-632-231-213-326-523-632-675-316-732-321-506-561-352-275-750-351-576-231-205-627-326-627-321-236-136-321-312-312-267-276-275-726-125-215-725-213-216-251-2_V.mp3)
  Segment 12: 79292ms ~ 86060ms (file: 127-132-576-132-613-213-176-627-102-130-231-230-267-237-123-276-321-312-532-263-216-231-236-231-236-237-320-132-312-352-276-317-312-637-637-317-136-321-327-327-237-230-132-267-2_W.mp3)
  Segment 13: 87060ms ~ 87636ms (file: 572-215-520-312-723-316-273-216-537-376-502-237-723-321-572-203-572-627-263-235-132-754-213-123-602-132-375-673-360-326-572-256-752-672-765-321-236-326_V.mp3)
  Segment 14: 88636ms ~ 106384ms (file: 103-267-260-207-267-756-765-275-542-571-301-754-574-527-231-207-524-275-267-267-572-567-710-213 712-213-201-730-172-173-260-267-230-572-210-103-657-732-702-276-132-210-546-210-2_W.mp3)
  Segment 15: 107384ms ~ 107960ms (file: 507-631-261-125-607-725-267-302-310-301-310-310-136-312-261-605-236-236-263-763-321-237-706-567-672-572_V.mp3)
  Segment 16: 108960ms ~ 109536ms (file: 135-136-135-315-135-132-132-135-132-132-132-135-135-135-253-315-351-132-315-321-312-357-156-317-137-136-165-163-106-137-316-163-136-176-167-162-127-136-126-316-136-573-153-173-1_W.mp3)
  Segment 17: 110536ms ~ 118852ms (file: 103-103-132-206-206-524-201-216-317-130-372-173-123-213-213-276-273-267-213-217-217-726-123-137-513-230-132-217-312-103-731-216-210-302-216-231-213-527-231-260-206-236-273-231-3_W.mp3)
  Segment 18: 119852ms ~ 132704ms (file: 136-103-132-132-320-326-320-301-312-236-302-103-132-102-310-307-236-573-103-213-231-321-312-126-310-312-102-163-231-201-312-310-301-672-736-327-713-613-674-731-371-316-317-321-2_W_stt_of.mp3)
  Segment 19: 133704ms ~ 134280ms (file: 627-316-316-316-163-312-317-361-163-310_W.mp3)
  Segment 20: 135280ms ~ 143524ms (file: 756-736-367-736-137-237-267-267-756-627-267-627-762-267-267-321-236-160-172-236-132-376-765-217-267-267-216-326-627-276-217-317-167-671-572-375-276-236-315-513-103-132-316-321-2_W.mp3)
  Segment 21: 144524ms ~ 145100ms (file: 673-617-672-617-672-671-671-613-631-627-637-637-617-673-675-762-367-670-631-637-673-631-671-670_W.mp3)
  Segment 22: 146100ms ~ 146676ms (file: 157-750-173-357-750-170-150-715-731-715-753-507-130-137-103-107-157-157-571-571-751-751-713-705 571-751-571-571-517-107-175-175-751-175-750-750-735-750-751-107-175-571-157-153-7_W.mp3)
  Segment 23: 147676ms ~ 148252ms (file: 135-132-123-132-123-157-157-521-572-217-175-125-517-216-261-267-265-126-261-123 150-571-571-715-751-750-170-751-157-751-751-175-751-751 517-517-517-751-750-750-705-750-750-750-7_W.mp3)
  Segment 24: 149252ms ~ 149828ms (file: 613-136-413-146-314-741-132-713-132-361-654-341 371-312-143-653-261-142-134-162-327-351-341-621-374-261-371-167-162-471-751-413-312-321-127-364-174-132-341-312-413-127-623-267-2_V.mp3)
  Segment 25: 150828ms ~ 158604ms (file: 702-756-756-756-710-726-276-576-753-317-703-701-753-702-702-753-107-103-103-702-276-270-107-137-317-753-706-756-726-716-571-316-130-576-751-756-726-753-572-103-107-107-756-572-7_W.mp3)
  Segment 26: 159604ms ~ 160180ms (file: 756 321-103-321-351-316-352-576-537-352-532-536-357-527-573-753-356-576-567-531-351-356-537-653-357-572-325-365-175-326-356-357-326-375-350-573-375-356-532-352-675-576-576-756-5_W.mp3)
  Segment 27: 161180ms ~ 167228ms (file: 127-103-102-107-127-267-267-210-620-260-562-165-675-126-210-203-201-267-276-527-102-102-210-120-261-137-576-567-567-267-206-206-206-520-206-276-526-102-130-135-301-210-123-137-1_W.mp3)
  Segment 28: 168228ms ~ 174780ms (file: 236-276-276-230-312-257-106-675-236-120-612-120-362-162-726-271-715-321-765-237-320-736-627-327-307-136-607-671-167-620-273-237-527-265-263-267-276-267-167-617-362-572-276-321-7_W.mp3)
  Segment 29: 175780ms ~ 180388ms (file: 276-102-176-376-371-136-570-301-301-216-203-201-102-120-265-263-637-102-136-105-105-216-123-256-627-620-276-271-627-725-213-237-326-312-301-130-103-136-612-302-206-263-362-361-3_W.mp3)
  Segment 30: 181388ms ~ 186500ms (file: 172-102-123-132-320-710-130-123-102-120-123-130-130-320-123-130-312-132-132-130-312-213-123-302-321-321-230-312-130-123-120-123-231-123-201-130-132-102-210-102-270-213-120-103-1_W.mp3)
  Segment 31: 187500ms ~ 200352ms (file: 136-132-621-367-136-130-107-732-210-276-267-752-576-137-132-165-132-136-160-107-130-130-752-130-310-624-106-106-132-130-312-173-167-162-130-103-217-601-620-260-213-120-612-267-3_W.mp3)
  Segment 32: 201352ms ~ 201928ms (file: 625-576-257-652-627-576-267-672-625-726-527-621-526-572-261-135-574-761-672-621-765-536-325-702-537-325-672-570-567-126-651-315-531-317-670-536-315-350-135_V.mp3)
  Segment 33: 202928ms ~ 203504ms (file: 573-576-756-756-756-765-756-765-756_W.mp3)
  Segment 34: 204504ms ~ 205080ms (file: 752-570-576-756-763-517-536-576-527-576-576-725-167-316-361-361-361-316-361_W.mp3)
  Segment 35: 206080ms ~ 213064ms (file: 321-236-267-236-260-203-540-542-620-627-206-106-571-103-216-261-621-103-137-615-137-143-132-213-234-321-132-143-301-312-132-321-312-314-312-321-324-321-312-216-321-312-174-312-3_V.mp3)
  Segment 36: 214064ms ~ 219896ms (file: 723-103-267-217-231-721-726-276-127-103-130-103-132-517-572-253-210-574-265-127-230-213-120-672-267-267-120-206-267-267-267-756-203-273-162-276-207-120-302-123-120-127-726-172-7_W.mp3)
  Segment 37: 220896ms ~ 227268ms (file: 230-310-310-107-137-216-206-301-206-675-162-126-132-750-102-123-102-123-123-130-102-321-160-312-102-301-103-310-675-675-206-126-102-206-230-216-207-230-206-316-316-213-703-675-5_W.mp3)
  Segment 38: 228268ms ~ 228844ms (file: 317-132-312-126-523-571-217-725-725-321-570-125-132-136-675-310-310-310-301-760-765-735-356-537-357-357-576-316-301-761-657-621 574-765-175-312 326-637-763-376-637-367-671-365-1_W.mp3)
  Segment 39: 229844ms ~ 236900ms (file: 167-176-216-765-207-276-203-230-231-652-602-321-327-102-102-267-267-123-276-705-217-103-265-231-572-103-236-276-137-237-576-576-273-756-237-130-167-617-312-253-257-106-571-671-7_W.mp3)
  Segment 40: 237900ms ~ 246108ms (file: 751-573-163-271-132-103-316-670-627-320-327-316-675-237-574-716-132-265-261-163-167-130-312-362-176-103-617-752-672-267-267-261-167-673-671-107-617-576-627-672-136-167-106-632-7_W.mp3)
  Segment 41: 247108ms ~ 247684ms (file: 176-176-176 137-137-137-176-176-167-716-137-175-173-173-175-176-176-173 175-173-163-756-765-175-137-173-175-176-175-176-176-176-175-176-176-176-176-173 137-713-173-176-175-175-1_V.mp3)
  Segment 42: 248684ms ~ 249260ms (file: 671-761-126-123-135-631 267-120-612 621_W.mp3)
  Segment 43: 250260ms ~ 256380ms (file: 102-210-123-213-102-302-210-201-102-231-213-201-132-213-231-231-236-230-321-321-130-230-321-230-132-132-102-102-102-103-106-162-123-261-762-276-301-327-730-731-713-321-120-231-1_V.mp3)
  Segment 44: 257380ms ~ 262276ms (file: 567-127-571-130-570-156-576-675-621-725-572-572-752-657-357-576-267-162-216-123-612-231-576-275-625-267-106-127-120-312-130-162-672-376-312-267-312-273-715-175-657-651-103-103-1_W.mp3)
  Segment 45: 263276ms ~ 267740ms (file: 321-270-206-720-762-627-276-270-276-237-273-327-237-231-231-213-213-217-271-237-327-273-217-216-271-237-237-237-270-237-321-203-237-260-236-213-237-312-726-732-631-126-621-637-3_W_stt_then.mp3)
  Segment 46: 268740ms ~ 269316ms (file: 351-157-751-317-321-573-512-137-107-731-317-120-630-102-312-127-231-167-136-167-163-167-613-673-671-765-672-617-617-617-675-675-615-675-613-623-136-312-312-317-312-321-312-231-5_W.mp3)
  Segment 47: 270316ms ~ 270892ms (file: 750-537-512-261-503-325-756-312-327-136-237-567-216-716-153-130-137-173-103-132-312-176-176-127-206-236-612-275-267-762-261-612-756-176-750-132-127-705-276-275-326-316-754-572-7_V.mp3)
  Segment 48: 271892ms ~ 277760ms (file: 762-276-627-267-267-273-572-261-231-312-765-756-321-312-623-762-765-760-672-267-627-267-320-371-762-572-137-237-267-126-103-136-132-612-617-162-326-231-276-236-627-106-276-267-2_W.mp3)
  Segment 49: 278760ms ~ 288984ms (file: 172-751-572-127-210-201-213-175-107-712-137-321-176-107-712-123-751-175-231-261-216-276-267-512-310-320-120-102-132-103-132-103-201-120-261-271-102-201-206-103-230-203-201-132-1_W.mp3)
  Segment 50: 289984ms ~ 290560ms (file: 316-375-526-376-657-617-716-325-213-527-132-275-750-312-271-613-135-137-157-617-123-627-276-367-371-632-236-312-513-231-305-572-251-623-365-273-576-326-570-507-253-752-236-602-5_W.mp3)
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
> **Time**: 2026-04-06 07:56:09
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "여러분 안녕하세요 오늘 공부할 단어들을 함께 살펴봅시다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "여러분", "start_ms": 150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "안녕하세요", "start_ms": 800, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 1800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "공부할", "start_ms": 2250, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "단어들을", "start_ms": 2900, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "함께", "start_ms": 3650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "살펴봅시다", "start_ms": 4150, "dur_ms": 1100, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 4,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 9,
"transcript": "첫 번째 단어는 공항입니다 공항에 가서 비행기를 타요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "첫", "start_ms": 180, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "번째", "start_ms": 400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "단어는", "start_ms": 750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "공항입니다", "start_ms": 1300, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "공항에", "start_ms": 2400, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "가서", "start_ms": 3050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "비행기를", "start_ms": 3600, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "타요", "start_ms": 4400, "dur_ms": 500, "phase_lr_deg": 0.0}
]
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
"transcript": "Airport I go to the airport to take a plane",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 220,
"audio_type": "speech",
"words": [
{"word": "Airport", "start_ms": 220, "dur_ms": 800, "phase_lr_deg": 5.0},
{"word": "I", "start_ms": 1500, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "go", "start_ms": 1700, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "to", "start_ms": 1950, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "the", "start_ms": 2150, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "airport", "start_ms": 2350, "dur_ms": 650, "phase_lr_deg": 5.0},
{"word": "to", "start_ms": 3100, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "take", "start_ms": 3350, "dur_ms": 300, "phase_lr_deg": 5.0},
{"word": "a", "start_ms": 3700, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "plane", "start_ms": 3850, "dur_ms": 600, "phase_lr_deg": 5.0}
]
},
{
"segment": 12,
"transcript": "두 번째 단어는 도서관입니다 도서관에서 책을 읽어요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "두", "start_ms": 150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "번째", "start_ms": 350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "단어는", "start_ms": 750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "도서관입니다", "start_ms": 1300, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "도서관에서", "start_ms": 2500, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "책을", "start_ms": 3450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "읽어요", "start_ms": 3900, "dur_ms": 700, "phase_lr_deg": 0.0}
]
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
"transcript": "Library I read books at the library",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "Library", "start_ms": 200, "dur_ms": 800, "phase_lr_deg": 5.0},
{"word": "I", "start_ms": 1500, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "read", "start_ms": 1700, "dur_ms": 300, "phase_lr_deg": 5.0},
{"word": "books", "start_ms": 2050, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "at", "start_ms": 2500, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "the", "start_ms": 2700, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "library", "start_ms": 2900, "dur_ms": 700, "phase_lr_deg": 5.0}
]
},
{
"segment": 16,
"transcript": "마지막 단어는 식당입니다 식당에서 맛있는 음식을 먹어요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 300,
"audio_type": "speech",
"words": [
{"word": "마지막", "start_ms": 300, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "단어는", "start_ms": 950, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "식당입니다", "start_ms": 1500, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "식당에서", "start_ms": 2600, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "맛있는", "start_ms": 3450, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "음식을", "start_ms": 4100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "먹어요", "start_ms": 4650, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "Restaurant I eat delicious food at the restaurant",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "Restaurant", "start_ms": 250, "dur_ms": 900, "phase_lr_deg": 5.0},
{"word": "I", "start_ms": 1600, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "eat", "start_ms": 1800, "dur_ms": 250, "phase_lr_deg": 5.0},
{"word": "delicious", "start_ms": 2100, "dur_ms": 700, "phase_lr_deg": 5.0},
{"word": "food", "start_ms": 2850, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "at", "start_ms": 3300, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "the", "start_ms": 3500, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "restaurant", "start_ms": 3700, "dur_ms": 800, "phase_lr_deg": 5.0}
]
},
{
"segment": 18,
"transcript": "오늘 배운 내용을 잘 기억하세요 다음 시간에 또 만나요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "배운", "start_ms": 550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "내용을", "start_ms": 1000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 1550, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "기억하세요", "start_ms": 1800, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 2900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "시간에", "start_ms": 3350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 3900, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 4150, "dur_ms": 600, "phase_lr_deg": 0.0}
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
"transcript": "Goodbye everyone Have a great day",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "Goodbye", "start_ms": 200, "dur_ms": 700, "phase_lr_deg": 5.0},
{"word": "everyone", "start_ms": 1000, "dur_ms": 700, "phase_lr_deg": 5.0},
{"word": "Have", "start_ms": 2000, "dur_ms": 300, "phase_lr_deg": 5.0},
{"word": "a", "start_ms": 2350, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "great", "start_ms": 2500, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "day", "start_ms": 2950, "dur_ms": 500, "phase_lr_deg": 5.0}
]
},
{
"segment": 21,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
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
"transcript": "그럼 이제부터는 일본어로 인사를 배워볼까요",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 350,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이제부터는", "start_ms": 800, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "일본어로", "start_ms": 1650, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "인사를", "start_ms": 2300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "배워볼까요", "start_ms": 2850, "dur_ms": 900, "phase_lr_deg": 0.0}
]
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
"transcript": "こんにちは はじめまして よろしくお願いします",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "ja",
"silence_before_ms": 400,
"audio_type": "speech",
"words": [
{"word": "こんにちは", "start_ms": 400, "dur_ms": 1000, "phase_lr_deg": -10.0},
{"word": "はじめまして", "start_ms": 1600, "dur_ms": 1200, "phase_lr_deg": -10.0},
{"word": "よろしくお願いします", "start_ms": 3000, "dur_ms": 1800, "phase_lr_deg": -10.0}
]
},
{
"segment": 26,
"transcript": "아주 잘하셨어요 발음이 정말 좋습니다",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "아주", "start_ms": 150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "잘하셨어요", "start_ms": 550, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "발음이", "start_ms": 1400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 1950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "좋습니다", "start_ms": 2400, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "이제 마지막으로 한 번만 더 큰 소리로 따라하세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "마지막으로", "start_ms": 600, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 1450, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "번만", "start_ms": 1700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 2100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 2350, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "소리로", "start_ms": 2650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "따라하세요", "start_ms": 3200, "dur_ms": 1000, "phase_lr_deg": 0.0}
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
"transcript": "감사합니다 수고하셨습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 150, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "수고하셨습니다", "start_ms": 1150, "dur_ms": 1100, "phase_lr_deg": 0.0}
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
"transcript": "자 그럼 노래와 함께 마무리하겠습니다",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "자", "start_ms": 200, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "그럼", "start_ms": 450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "노래와", "start_ms": 900, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "함께", "start_ms": 1550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "마무리하겠습니다", "start_ms": 2050, "dur_ms": 1200, "phase_lr_deg": 0.0}
]
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
"transcript": "라라라 즐거운 한국어 공부 시간",
"speaker": "speaker_4",
"confidence": 0.88,
"language": "ko",
"silence_before_ms": 400,
"audio_type": "mixed",
"words": [
{"word": "라라라", "start_ms": 400, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "즐거운", "start_ms": 1600, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "한국어", "start_ms": 2300, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "공부", "start_ms": 3100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "시간", "start_ms": 3700, "dur_ms": 600, "phase_lr_deg": 0.0}
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
"transcript": "모두가 웃으며 인사해요 안녕 안녕",
"speaker": "speaker_4",
"confidence": 0.89,
"language": "ko",
"silence_before_ms": 300,
"audio_type": "mixed",
"words": [
{"word": "모두가", "start_ms": 300, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "웃으며", "start_ms": 1000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "인사해요", "start_ms": 1700, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "안녕", "start_ms": 2600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "안녕", "start_ms": 3100, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "정말 고생 많으셨습니다 안녕히 가세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "고생", "start_ms": 600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "많으셨습니다", "start_ms": 1050, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 2200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 2800, "dur_ms": 600, "phase_lr_deg": 0.0}
]
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
"transcript": "Every security assessment is routed by service.",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "Every", "start_ms": 250, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "security", "start_ms": 700, "dur_ms": 600, "phase_lr_deg": 5.0},
{"word": "assessment", "start_ms": 1350, "dur_ms": 800, "phase_lr_deg": 5.0},
{"word": "is", "start_ms": 2200, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "routed", "start_ms": 2400, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "by", "start_ms": 2950, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "service.", "start_ms": 3200, "dur_ms": 600, "phase_lr_deg": 5.0}
]
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
"transcript": "끝까지 시청해주셔서 대단히 감사합니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "끝까지", "start_ms": 150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "시청해주셔서", "start_ms": 800, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "대단히", "start_ms": 1900, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 2600, "dur_ms": 900, "phase_lr_deg": 0.0}
]
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
*Generated by WKAppBot ask gemini — 2026-04-06 07:56:09*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43084  time=07:54:16  uptime=8630s  mem=594MB  system_processes=413"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_075158.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 317-230 612-610-625-216-126-126-126-627-627-205-520-312-312-263-720-735 217-230 573 675-613-615-657-620-231-273-725-730-730-731 321-732-576-257-572_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 257-725-275-235-560-267-213-126-157-563-367-675-617-376-675-367-673-657-675-574-547_W.mp3)
  Segment 3: 3152ms ~ 12188ms (file: 312-230-130-301-137-103-106-136-216-325-573-673-637-132-305-351-310-236-230-326-137-107-127-675-372-372-236-312-231-710-675-136-301-130-130-103-312-635-267-623-310-316-312-231-1_W.mp3)
  Segment 4: 13188ms ~ 13764ms (file: 675-756-576-576-756-756-756-756-637-361-673-756-765-765-765-763-765-765-765-765-765-756-756-270-570-671-163-132-136-163-576-362-562-576-657-567-576-562-163-213 156-136-312-130-3_W.mp3)
  Segment 5: 14764ms ~ 21352ms (file: 230-103-103-276-627-267-217-203-201-276-276-267-276-267-321-237-231-261-261-320-201-210-123-120-132-163-316-301-310-671-760-670-201-326-103-574-261-761-726-201-176-103-137-132-1_W.mp3)
  Segment 6: 22352ms ~ 22928ms (file: 735-136-261-276-321-762-257-326-237-562-361-752-327-237-326-567-531-725-547-372-236-132-351-715-672-175-752-230-605-367-176-316_V.mp3)
  Segment 7: 23928ms ~ 24504ms (file: 537-517-531-310-154-517-517-157-351-315-120-163-613-617-651-153-567-571-657-567-657-567-576-356-153 567-567-516-315-672-675-672-762-765-765-765-672-672-672-132-762-672-623-316-1_W.mp3)
  Segment 8: 25504ms ~ 26080ms (file: 321-537-765-725-756-705-321-256-263-352-670-263-327-267-267-261-263-321-231-750-315-251-231-750-312-132-123-352-506-267-216-637-723-761-637-672-675-701-571-753-751-715-715-753-7_W.mp3)
  Segment 9: 27080ms ~ 27656ms (file: 157-165-162-562-657-157-657-567-657-761-576-756-576-576-675-657-617-163-156-175-567-567-567-157-675-650-750-576-715-103-750-175-715-751-751-716-751-710-175-651-157-261-216-261-6_W.mp3)
  Segment 10: 28656ms ~ 36791ms (file: 130-210-320-302-320-127-103-310-201-310-130-301-102-103-312-320-203-312-207-203-201-273-176-203-576-210-236-276-201-230-130-571-316-310-102-103-135-103-753-752-756-572-130-201-1_V.mp3)
  Segment 11: 37791ms ~ 38367ms (file: 267-261-527-362-756-567-750-675-527-572-362-627-765-267-572-750-376-176-670-371-573-507-672-632-527-326-576-237-762-672-657-571_V.mp3)
  Segment 12: 39367ms ~ 39943ms (file: 517-137-157-170-750-715-157-751-157-751-571-751-571 574-574-574-574-574-571-571-570-571-517-175-751-157-157-157-107-153-751-175-173-571-751 571-175-573-571-517-573-571-157-571-7_W.mp3)
  Segment 13: 40943ms ~ 49259ms (file: 213-263-610-670-176-130-321-317-173-130-173-312-317-507-570-153-103-301-310-762-276-210-210-120-713-732-173-372-317-312-126-672-130-102-352-503-503-103-230-320-321-132-312-203-1_W.mp3)
  Segment 14: 50259ms ~ 55371ms (file: 132-321-213-312-312-375-237-123-312-320-230-321-236-325-731-132-312-213-213-321-321-312-231-321-312-326-130-130-327-213-321-132-312-231-271-237-207-127-371-236-263-236-320-732-3_W.mp3)
  Segment 15: 56371ms ~ 56947ms (file: 756-510-561-307-571-735-312-631-352-137-176-173-136-175-176-571-756-273-751-731-750-215-235-527-263-256-165-261-572-327-752-271-761-765-176-752-275-574-315-753-157-576 326-270-3_V.mp3)
  Segment 16: 57947ms ~ 58523ms (file: 321-123-167-312-325-231-206-275-126-263-627-237-315-263-165-576-261-721-756-671-275-261-327-351-256-726-752-267-317-307-735_V.mp3)
  Segment 17: 59523ms ~ 60099ms (file: 571-657-573-761-361-367-631-671-765-751-175-613-517-567-756-675-765-763-751-675-516-156-675-675-756-765-537-351-312-305-302-362-615-675-576-735-357-315-327-315-317-137-715-723-1_W.mp3)
  Segment 18: 61099ms ~ 61675ms (file: 321-312-321-321-312-631-612-635-315-315-315-315-315-315-316-316-365-352-351-135-175-317-135-351-315-312-326-321-312-325-372-547-316-310-321-163-657-165-327 126-315_W.mp3)
  Segment 19: 62675ms ~ 68723ms (file: 127-103-102-107-127-267-267-210-620-260-562-165-675-126-210-203-201-267-276-527-102-102-210-120-261-137-576-567-567-267-206-206-206-520-206-276-526-102-130-135-301-210-123-137-1_W.mp3)
  Segment 20: 69723ms ~ 70299ms (file: 576-176-567-576-567-754-576-312-167-312-137-163-312-132-136-132-316-317-361-132-312-126-726-672-261-715-570-760-761-765-761-765-671-761-576-765-756-670-671-631-753-715-751-576-7_W.mp3)
  Segment 21: 71299ms ~ 71875ms (file: 231-132-567-751-527-620-123-162-267-756-167-761-126-574-751 321-237-251-352-572-267-570-263-763-265-726-270-267-572-267-267-276-235-265-527-675-236-725-207-236-726-726-263-325-6_V.mp3)
  Segment 22: 72875ms ~ 73451ms (file: 672-627-675-365-617-721-175-357-325-132-126-132-132-213-231_W.mp3)
  Segment 23: 74451ms ~ 75027ms (file: 361 571-537-573-576-574-571-543-574-534-547-534-534-312-103-103-132-136-107-103-107-173-174-157-513-103-103-104-103-104-174-104-104-107-104-145-146-104-174-104-104-160-140-175-6_W.mp3)
  Segment 24: 76027ms ~ 85279ms (file: 527-231-236-120-261-263-275-756-321-231-237-231-267-312-102-123-120-627-126-167-120-210-102-267-627-263-627-276-672-213-263-267-210-573-756-320-123-102-261-236-236-267-267-752-2_W.mp3)
  Segment 25: 86279ms ~ 86855ms (file: 621-653-521-712-257-562-257-136-571-576-751-765-263-571-273-321-570-621-526-623-123-261-261-526-652-316-567-256-317-325-562-574-217-756-235-321-572-725-507-572-762-576-623-327-2_V.mp3)
  Segment 26: 87855ms ~ 97287ms (file: 312-763-312-102-752-201-130-310-130-136-312-132-216-123-312-103-102-130-157-275-213-132-321-130-670-320-213-143-203-310-752-752-163-207-237-257-201-370-702-103-310-310-260-102-1_W.mp3)
  Segment 27: 98287ms ~ 106315ms (file: 765-671-276-216-216-612-276-176-127-721-671-316-321-132-126-123-263-236-267-120-267-267-276-326-201-276-213-162-671-750-321-273-231-312-173-167-162-162-756-260-260-107-107-321-1_W.mp3)
  Segment 28: 107315ms ~ 115991ms (file: 753-573-326-217-213-371-751-102-132-732-132-325-765-236-671-132-203-725-312-257-527-756-260-231-271-132-267-301-263-237-620-627-326-573-327-132-103-103-310-132-312-263-625-526-1_W.mp3)
  Segment 29: 116991ms ~ 127107ms (file: 267-260-260-310-102-210-103-263-627-321-123-317-320-310-123-260-103-103-130-230-320-231-312-362-136-260-160-103-361-126-370-301-136-130-236-321-321-145-130-317-307-132-103-120-1_V.mp3)
  Segment 30: 128107ms ~ 128683ms (file: 157-165-517-567-576-516-570-576-567-156-516-156-751-576-652-657-567-125-152-157-135-132-615-165-517-571-572-657-576-572-562-567-125-756-576-567-756-527-576-567-756-567-615-756-1_W.mp3)
  Segment 31: 129683ms ~ 135479ms (file: 103-132-312-132-312-132-321-231-132-231-123-132-132-132-312-326-132-130-310-310-132-231-236-321-132-136-130-120-612-132-173-317-213-103-237-231-671-631-231-160-610-136-563-167-1_W.mp3)
  Segment 32: 136479ms ~ 137055ms (file: 751-175-170-710-701-710-170-170-175-175-175-175-715-715-175-713-715-715-715-756-715-715-107 670-156-715-167-107-175-170-751-170-710-706-705-701-715-710-176-716-761-716-751-715-1_V.mp3)
  Segment 33: 138055ms ~ 138631ms (file: 165-176-576-156-125-102-132-130-130-130-657-756-652-210-527-625-576-657-615-236-576-162-136-163-167-165-576-725-216-563-136-162-136-136-615-125-120-170-175-751-710-701-170-701-7_W.mp3)
  Segment 34: 139631ms ~ 140207ms (file: 327-752-132-312-573-261-235-627-235-572-574-725-756-765-765-361-257-753-312-275-756-365-570-615-237-574-532-571-572-215-267-527-362-132-752-271-267-271-273-215-560-572-527-267-7_V.mp3)
  Segment 35: 141207ms ~ 141783ms (file: 321-327-162-572-263-567-267-762-162-312-725-156-263-312-250-132-317-507-236-502-635-325-352-672-267-612-127-162-351-372-612-271-706-360-163-503-375-371-653-537-657-376-567-567-3_W.mp3)
  Segment 36: 142783ms ~ 143359ms (file: 136-765-756-371-673-735-736-673-756-736-376-735-765-756_W.mp3)
  Segment 37: 144359ms ~ 144935ms (file: 507-571-237-276-725-275-273-765-752-705-570-257-265-265-263-265-621-526-576-526-532-657-576-621-371-175-231-130-137-316-136-213-132 671-216-527-572-215-275-351-231-137-123-327-7_W.mp3)
  Segment 38: 145935ms ~ 153099ms (file: 615-256-206-105-106-306-301-632-672-267-216-201-130-502-527-253-235-126-716-136-125-521-276-657-265-261-752-725-263-316-675-302-672-267-132-317-657-657-130-135-571-260-527-103-1_W.mp3)
  Segment 39: 154099ms ~ 162019ms (file: 260-312-135-176-617-615-107-307-103-237-176-106-167-150-167-267-765-627-326-230-132-756-276-267-230-103-312-267-612-136-172-756-321-173-175-316-326-312-135-106-321-761-103-230-2_W.mp3)
  Segment 40: 163019ms ~ 163595ms (file: 567-563-576-567-576-576-567-576-321-651-652-572-567-367-756-765-576-536-356-365-657-576-765-756-573-567-567-356-567-752-315-371-751-527-526-567-567-257-526-263-145-316-576-576-5_W.mp3)
  Segment 41: 164595ms ~ 173307ms (file: 672-367-136-160-376-532-237-327-120-763-741-761-723-213-173-130-136-327-357-167-167-107-106-126-236-210-312-267-267-576-625-265-267-267-230-756-265-267-236-162-310-256-132-236-2_W.mp3)
  Segment 42: 174307ms ~ 185647ms (file: 267-627-213-103-217-216-123-123-231-123-103-231-132-310-120-312-761-216-270-137-267-310-201-320-571-213-320-276-267-672-123-201-132-310-103-206-203-230-321-302-301-123-132-123-3_W.mp3)
  Segment 43: 186647ms ~ 198707ms (file: 153-102-103-134-132 106-721-102-173-213-162-150-123-126-612-230-237-237-362-170-573-127-713-573-361-765-756-657-672-106-671-617-206-260-625-265-260-267-762-271-236-103-167-162-1_W.mp3)
  Segment 44: 199707ms ~ 200283ms (file: 751-571 517-571-571-571-571-570-157-571-750-107-751-715-105-750-315-715-750-756 517-571-574-572-574-574-571-571-571-571-571-157-175-157-570-175-573-571-517-751-753-751-175-573 5_W.mp3)
  Segment 45: 201283ms ~ 201859ms (file: 715-756-157-576-135-150-157-157-175-157-517-157-157-157-157-517-571-517-157-157-175-157-751-157-576-157 157-135-103-175-571-157-517-157-517-157-157-571-157-157-571-157-516-517-7_V.mp3)
  Segment 46: 202859ms ~ 210383ms (file: 267-267-276-206-261-260-531-157-173-137-762-310-175-715-201-237-132-136-321-310-123-671-765-175-173-301-173-174-173-167-172-260-201-203-206-301-103-752-362-236-753-605-102-321-2_V.mp3)
  Segment 47: 211383ms ~ 211959ms (file: 756-756-765-675-673-576-576-710-675-765-356-657-765-675-675-573-675-376-756-530-637_W.mp3)
  Segment 48: 212959ms ~ 213535ms (file: 573-235-270-237-675-365-576-316-362-312-356-173-517-126-751-275-567-315-765-567-352-637-237-726-507-576-576-271-261-265-276-261-630-316-317-326-316-316-130-316-326-351-351-567-7_V.mp3)
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
> **Time**: 2026-04-06 07:56:09
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "여러분 안녕하세요 오늘 공부할 단어들을 함께 살펴봅시다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "여러분", "start_ms": 150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "안녕하세요", "start_ms": 800, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 1800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "공부할", "start_ms": 2250, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "단어들을", "start_ms": 2900, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "함께", "start_ms": 3650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "살펴봅시다", "start_ms": 4150, "dur_ms": 1100, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 4,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 9,
"transcript": "첫 번째 단어는 공항입니다 공항에 가서 비행기를 타요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "첫", "start_ms": 180, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "번째", "start_ms": 400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "단어는", "start_ms": 750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "공항입니다", "start_ms": 1300, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "공항에", "start_ms": 2400, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "가서", "start_ms": 3050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "비행기를", "start_ms": 3600, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "타요", "start_ms": 4400, "dur_ms": 500, "phase_lr_deg": 0.0}
]
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
"transcript": "Airport I go to the airport to take a plane",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 220,
"audio_type": "speech",
"words": [
{"word": "Airport", "start_ms": 220, "dur_ms": 800, "phase_lr_deg": 5.0},
{"word": "I", "start_ms": 1500, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "go", "start_ms": 1700, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "to", "start_ms": 1950, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "the", "start_ms": 2150, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "airport", "start_ms": 2350, "dur_ms": 650, "phase_lr_deg": 5.0},
{"word": "to", "start_ms": 3100, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "take", "start_ms": 3350, "dur_ms": 300, "phase_lr_deg": 5.0},
{"word": "a", "start_ms": 3700, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "plane", "start_ms": 3850, "dur_ms": 600, "phase_lr_deg": 5.0}
]
},
{
"segment": 12,
"transcript": "두 번째 단어는 도서관입니다 도서관에서 책을 읽어요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "두", "start_ms": 150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "번째", "start_ms": 350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "단어는", "start_ms": 750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "도서관입니다", "start_ms": 1300, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "도서관에서", "start_ms": 2500, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "책을", "start_ms": 3450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "읽어요", "start_ms": 3900, "dur_ms": 700, "phase_lr_deg": 0.0}
]
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
"transcript": "Library I read books at the library",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "Library", "start_ms": 200, "dur_ms": 800, "phase_lr_deg": 5.0},
{"word": "I", "start_ms": 1500, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "read", "start_ms": 1700, "dur_ms": 300, "phase_lr_deg": 5.0},
{"word": "books", "start_ms": 2050, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "at", "start_ms": 2500, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "the", "start_ms": 2700, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "library", "start_ms": 2900, "dur_ms": 700, "phase_lr_deg": 5.0}
]
},
{
"segment": 16,
"transcript": "마지막 단어는 식당입니다 식당에서 맛있는 음식을 먹어요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 300,
"audio_type": "speech",
"words": [
{"word": "마지막", "start_ms": 300, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "단어는", "start_ms": 950, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "식당입니다", "start_ms": 1500, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "식당에서", "start_ms": 2600, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "맛있는", "start_ms": 3450, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "음식을", "start_ms": 4100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "먹어요", "start_ms": 4650, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "Restaurant I eat delicious food at the restaurant",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "Restaurant", "start_ms": 250, "dur_ms": 900, "phase_lr_deg": 5.0},
{"word": "I", "start_ms": 1600, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "eat", "start_ms": 1800, "dur_ms": 250, "phase_lr_deg": 5.0},
{"word": "delicious", "start_ms": 2100, "dur_ms": 700, "phase_lr_deg": 5.0},
{"word": "food", "start_ms": 2850, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "at", "start_ms": 3300, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "the", "start_ms": 3500, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "restaurant", "start_ms": 3700, "dur_ms": 800, "phase_lr_deg": 5.0}
]
},
{
"segment": 18,
"transcript": "오늘 배운 내용을 잘 기억하세요 다음 시간에 또 만나요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "배운", "start_ms": 550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "내용을", "start_ms": 1000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 1550, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "기억하세요", "start_ms": 1800, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 2900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "시간에", "start_ms": 3350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 3900, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 4150, "dur_ms": 600, "phase_lr_deg": 0.0}
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
"transcript": "Goodbye everyone Have a great day",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "Goodbye", "start_ms": 200, "dur_ms": 700, "phase_lr_deg": 5.0},
{"word": "everyone", "start_ms": 1000, "dur_ms": 700, "phase_lr_deg": 5.0},
{"word": "Have", "start_ms": 2000, "dur_ms": 300, "phase_lr_deg": 5.0},
{"word": "a", "start_ms": 2350, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "great", "start_ms": 2500, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "day", "start_ms": 2950, "dur_ms": 500, "phase_lr_deg": 5.0}
]
},
{
"segment": 21,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
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
"transcript": "그럼 이제부터는 일본어로 인사를 배워볼까요",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 350,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이제부터는", "start_ms": 800, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "일본어로", "start_ms": 1650, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "인사를", "start_ms": 2300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "배워볼까요", "start_ms": 2850, "dur_ms": 900, "phase_lr_deg": 0.0}
]
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
"transcript": "こんにちは はじめまして よろしくお願いします",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "ja",
"silence_before_ms": 400,
"audio_type": "speech",
"words": [
{"word": "こんにちは", "start_ms": 400, "dur_ms": 1000, "phase_lr_deg": -10.0},
{"word": "はじめまして", "start_ms": 1600, "dur_ms": 1200, "phase_lr_deg": -10.0},
{"word": "よろしくお願いします", "start_ms": 3000, "dur_ms": 1800, "phase_lr_deg": -10.0}
]
},
{
"segment": 26,
"transcript": "아주 잘하셨어요 발음이 정말 좋습니다",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "아주", "start_ms": 150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "잘하셨어요", "start_ms": 550, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "발음이", "start_ms": 1400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 1950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "좋습니다", "start_ms": 2400, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "이제 마지막으로 한 번만 더 큰 소리로 따라하세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "마지막으로", "start_ms": 600, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 1450, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "번만", "start_ms": 1700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 2100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 2350, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "소리로", "start_ms": 2650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "따라하세요", "start_ms": 3200, "dur_ms": 1000, "phase_lr_deg": 0.0}
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
"transcript": "감사합니다 수고하셨습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 150, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "수고하셨습니다", "start_ms": 1150, "dur_ms": 1100, "phase_lr_deg": 0.0}
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
"transcript": "자 그럼 노래와 함께 마무리하겠습니다",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "자", "start_ms": 200, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "그럼", "start_ms": 450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "노래와", "start_ms": 900, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "함께", "start_ms": 1550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "마무리하겠습니다", "start_ms": 2050, "dur_ms": 1200, "phase_lr_deg": 0.0}
]
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
"transcript": "라라라 즐거운 한국어 공부 시간",
"speaker": "speaker_4",
"confidence": 0.88,
"language": "ko",
"silence_before_ms": 400,
"audio_type": "mixed",
"words": [
{"word": "라라라", "start_ms": 400, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "즐거운", "start_ms": 1600, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "한국어", "start_ms": 2300, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "공부", "start_ms": 3100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "시간", "start_ms": 3700, "dur_ms": 600, "phase_lr_deg": 0.0}
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
"transcript": "모두가 웃으며 인사해요 안녕 안녕",
"speaker": "speaker_4",
"confidence": 0.89,
"language": "ko",
"silence_before_ms": 300,
"audio_type": "mixed",
"words": [
{"word": "모두가", "start_ms": 300, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "웃으며", "start_ms": 1000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "인사해요", "start_ms": 1700, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "안녕", "start_ms": 2600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "안녕", "start_ms": 3100, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "정말 고생 많으셨습니다 안녕히 가세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "고생", "start_ms": 600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "많으셨습니다", "start_ms": 1050, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 2200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 2800, "dur_ms": 600, "phase_lr_deg": 0.0}
]
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
"transcript": "Every security assessment is routed by service.",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "Every", "start_ms": 250, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "security", "start_ms": 700, "dur_ms": 600, "phase_lr_deg": 5.0},
{"word": "assessment", "start_ms": 1350, "dur_ms": 800, "phase_lr_deg": 5.0},
{"word": "is", "start_ms": 2200, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "routed", "start_ms": 2400, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "by", "start_ms": 2950, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "service.", "start_ms": 3200, "dur_ms": 600, "phase_lr_deg": 5.0}
]
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
"transcript": "끝까지 시청해주셔서 대단히 감사합니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "끝까지", "start_ms": 150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "시청해주셔서", "start_ms": 800, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "대단히", "start_ms": 1900, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 2600, "dur_ms": 900, "phase_lr_deg": 0.0}
]
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
*Generated by WKAppBot ask gemini — 2026-04-06 07:56:09*
