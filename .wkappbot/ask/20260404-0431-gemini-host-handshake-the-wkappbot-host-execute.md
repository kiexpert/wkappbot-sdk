# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=29532  time=04:29:10  uptime=10644s  mem=999MB  system_processes=363"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260404_042857.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 136-135-315-356-315-312-135-315-315-531-531-356-357-537-513-531-562-572 321-325-325-352-357-357-352-532-325-352-352-325-352-352-352-352-352-532-352-532-352-532-253-253-567-657-6_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 763-765-761-763-672-675-765-756-751-756-175-675-765-763-765-751-765-621-621-263-756-756-756-765-765-756-756-756-765-756-753-754-765 756-750-167-735-756-765-765-765-761-761-172-1_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 267-205-206-203-206-312-310-103-310-103_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 163-130-176-167-136-612-107-236-216-236-172-127-127-127-103-102-671-136-310-312-623-136-130-136-760-517-163-132-103-135-163-312-157-610-763-613-136-160-130-517-163-130-156-157-6_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 602-267-265-261-623-231-261-321-102-120-312-570-267-267-621-602-263-602-265-627-260-623-261-261-216-213-236-167_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 103 576-106-617-316-531-675-672-627-612-167-617-136-316-167-106-163-670-657-615-617-167-106-620-260-620-612-106-602-260-261-162-167-167-617 135-675-657-675-675-675-675 672-627-6_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 132-312-132-127-137-136-163-132-132-135-132-136-137_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 657-657-657-675-527-156-562-576-657-316-163-127-132-612-125-175-632-367-361-315-356-365-315-356-376-316-376-315-657-361-675-316-167-176-176-137-576-175-716-617-136-615-167-176-1_W.mp3)
  Segment 9: 12608ms ~ 17900ms (file: 612-315-753-501-265-207-126-257-143-521-215-507-310-135-135-517-267-201-527-621-130-326-132-136-316-302-351-321-263-536-352-562-205-126-152-751-165-170-713-376-173-173-150-726-2_W.mp3)
  Segment 10: 18900ms ~ 25668ms (file: 132-132-231-162-120-231-132-126-231-320-120-123-765-657-267-231-275-125-120-230-102-152-125-102-123-102-230-236-230-123-102-103-235-102-210-123-120-231-132-130-120-213-231-120-5_V.mp3)
  Segment 11: 26668ms ~ 27244ms (file: 321-236-260-260-267-326-203-260-260-102-130-176-620-160-603-160-132-125-716-160-617-136-206-620-627-167-617-136-143-103-106-123-612-120-132-136-103-103-106-163-316-130-312-321-3_V.mp3)
  Segment 12: 28244ms ~ 28820ms (file: 625-267-623-102-132-123-103-302-376 163-136-310-316-132-316-312-206-260-230-302-130-130-103-320-230-230-167-617-621-621-162 376-371-317-726-735-721-671-675-765-762-765-761-765-6_V.mp3)
  Segment 13: 29820ms ~ 30396ms (file: 627-627-673-627 621 170-716-627-216 716-126-126-132-576-510-217-126-236-321-321-236-625-762-617-627-671-675-316-361-316-361-136-316 123-312-627-267-621-672-172-263-267-621-652 7_W.mp3)
  Segment 14: 31396ms ~ 31972ms (file: 762-736-763-763-762-765-762-762-765-762-762-726-762-276-726-762-725-726-726-762-762-275-726-725-763-725-726_W.mp3)
  Segment 15: 32972ms ~ 38552ms (file: 756-726-672-216-762-127-203-372-312-176-621-126-126-127-271-206-261-217-102-261-217-126-120-206-267-216-216-267-126-123-217-263-123-216-123-236-312-123-321-103-123-126-517-216-2_W.mp3)
  Segment 16: 39552ms ~ 40128ms (file: 136-165-705-675-670-671-163-103-136-163-362-302-236-326-673-106-162-106-163-316-326-623-602-623-623-672-206-360-106-103-136-103-312 126-136-132_V.mp3)
  Segment 17: 41128ms ~ 41704ms (file: 312-312-516-537-567-536-513-537-675-675-672-267-267-263-263-236-263-261-627-620-607-602-620-310-231-263-236-657-367-362-537-547-546-627-267-506-365-632-560-567-657-652-326-750-7_W.mp3)
  Segment 18: 42704ms ~ 48860ms (file: 132-123-217-120-102-132-312-123-231-123-213-123-315-321-267-231-213-216-213-237-123-162-267-261-267-263-216-213-625-265-273-675-213-132-160-216-721-127-126-120-213-231-261-213-2_W.mp3)
  Segment 19: 49860ms ~ 50436ms (file: 132-103-256-132-126-610-312-137-102-130-157-267-167-175-362-261-632-623-130-632-236-236-136-612-635-576-650-176-231 160-123-132-756-132-673-103-623-301-137-136-316-136-123-106-1_V.mp3)
  Segment 20: 51436ms ~ 52012ms (file: 152-126-165-106-105-130-136-163-137-632-306-361-103-103-135-160-175-103-103-103-126-103-103-103-103-103-153-132-130-123-316-167-136_V.mp3)
  Segment 21: 53012ms ~ 53588ms (file: 107-310-137-301-172-130-310-170-371-130-137-103-307-731-317-320 135-317-137-130-103-132-317-137-317-172-102-310-103-132-137-310-135-137-137-317-317-103-173-317-130-317-731 135-1_V.mp3)
  Segment 22: 54588ms ~ 55164ms (file: 167-602-102-103-762-761-671-671-671-601-136-167-163-103-132-213-236-263-236-130-213-216-261-231-672-130-375-321-321-203-524-351-531-574-137-106-361-103-163-156-167-570-657-612-1_W.mp3)
  Segment 23: 56164ms ~ 56740ms (file: 312-310-261-632-236-320-302-310-163-306-320-310-301-316-132-163-162-132-321-316 163-136-132-136-321-231-123-132-162-312-321-123-132-362-326-312-132-216-310-132-123-132-321-610 6_V.mp3)
  Segment 24: 57740ms ~ 58316ms (file: 321-126 567-564-562-567-562-567-562-271 176-316-267-175-157-617-615-130-130-321-613-536-136-631-102-765-756-675-627-627-627-627-765-706-716-675-123-756-326-562-567-536-753-261-1_W.mp3)
  Segment 25: 59316ms ~ 59892ms (file: 567-576-576-576-123-126-106-103-123-103-276-617-152-106-657-613-106-165-621-601-162-176-137-163-103-103-107-601-167-103-176-103-136-176-512-156-516-126-163-175-130-160-130-623-1_V.mp3)
  Segment 26: 60892ms ~ 61468ms (file: 103-315-135-103-175-135-315-135-103 135-105-135-135-312-135-132-153-315-317-137-135-103-137-310-103-315-135-317 137-130-135-135-315-153-135-315-137-132-137-103-103-103-132-317-3_V.mp3)
  Segment 27: 62468ms ~ 63044ms (file: 132-301-302-103-132-106-163-216-136-236-267-267-163-675-567-361-613-132-302-310-132-132-137-103-132-132-236-607-236-301-310-312-302-321-310-327-372-317-123-720-123-103-132-150-1_V.mp3)
  Segment 28: 64044ms ~ 64620ms (file: 135-156-136-123-136-102-126-123-136-563-627-361-657-671-623-612-106-106-132-162-173-365-615-617-163-103-657-175-130-613-167-162-170-103-316-371-107-103-167-106-136-167-103-136-6_V.mp3)
  Segment 29: 65620ms ~ 66196ms (file: 136-302-103-567-615-106-163-165-316-136-316-103-123 103-312-137-136_V.mp3)
  Segment 30: 67196ms ~ 67772ms (file: 237-726-127-312-367-317-310-327-327-723-726-721-371-231-317-173-173-731-572-524-547-534-513-217-317-327-137-217-321-237-327-273-725-237-617-213-617-165-651-236-123-162-265-235-7_W.mp3)
  Segment 31: 68772ms ~ 69348ms (file: 316-107-103-206-160-160-163-162-621-613-231-310-312-261-123-763-271-107-312-615-756-756-756-765-763-167 132 125-231 103-132-310-130-673-672-673-765 167-516-215-627-627-267-126-1_W.mp3)
  Segment 32: 70348ms ~ 70924ms (file: 625-561-625-126-657-756-516-752-126-672-265-237-625-627-621-623-625-627-672-672-672-672-761-165-176-261-574-754-765-756-675-756-756-756-206-621-621-261-261-562-572-531-572-576-5_W.mp3)
  Segment 33: 71924ms ~ 72500ms (file: 102-124 716-673-103-162-103-103-135-102-106 134-137-132-103-132-125-152-107-103-165_V.mp3)
  Segment 34: 73500ms ~ 74076ms (file: 627 671-162-132 263 671-612-631 261-216-126-261-216-263-216-163-621-263-261-162-126-162-216-236-263 163 120-132_W.mp3)
  Segment 35: 75076ms ~ 84940ms (file: 312-103-165-173-167-163-130-137-123-231-726-312-132-321-736-762-267-261-267-127-310-765-675-267-732-751-756-267-267-754-762-267-725-106-721-217-753-103-132-756-765-120-312-617-1_W.mp3)
  Segment 36: 85940ms ~ 86516ms (file: 576-137-735-756-217-126 276-267-726-276-276-276_W.mp3)
  Segment 37: 87516ms ~ 88092ms (file: 153-135 103-103-102-136-136-103-136-176-130-173-176-316-361-136-570-571-570-612-126-216_V.mp3)
  Segment 38: 89092ms ~ 89668ms (file: 267-106-617-672-672-672-612-671-126-261-123-163-123-162-126_W.mp3)
  Segment 39: 90668ms ~ 91244ms (file: 516-536-540-602-546-576-547-574-567-172-671-176-327-137-167-617-130-150-163-123-123-162-612-126-132-316-631-231-132-310-130-132-103-106-120-165-120-132-123-361-513-675-657-613-5_W.mp3)
  Segment 40: 92244ms ~ 92820ms (file: 631-602-261-120-263-726-627-267-261-261-216-173-376-316-561-107-130-176-176-657-576-576-571-157-756-756-765-675-675-765-675-675-675-236-326-632-612-163-156 163-375-761-675-637-6_W.mp3)
  Segment 41: 93820ms ~ 94396ms (file: 231 657-713-621-327-657-267-236-623-632-612-265-326-132-123-231-231-321-632-672-673-675-675-671-132-107-216-617-165-567-571-317-312-321-312-362-327-327-316-261-613-612-326-162-1_W.mp3)
  Segment 42: 95396ms ~ 95972ms (file: 132-136-316-137-135-176-651-671-106-136-103-160-176-601-160-136-136-316-507-561-315-132-136-316-504-574-570-571-132-137-136-167-107-167-617-670-165-615-576-574-576-574 132-132-3_V.mp3)
  Segment 43: 96972ms ~ 97548ms (file: 163-651-312-263-623-632-123-361-617-623-627-267-542-105-107-276-276-261-216-261-263-236-263-236-120-751-756-137-103-165-561-130-160-130 162-175-165-126-137 671-126-612-671-216-6_W.mp3)
  Segment 44: 98548ms ~ 99124ms (file: 301-312-613-301-206-126-236-326-301-361-326-106-162-231-231-106-310-123-326-326-321-312-103-137-312-130-136-106-210-206-267-620-631-123-130-531-312-231-230-231-723-312-236-320-2_V.mp3)
  Segment 45: 100124ms ~ 100700ms (file: 213-310-126-102-102-165-571-756-103-315-215-765-752-670-761-716-756-765-765-765-765-675-765-176-610-170-760-673-673-671-617-673-657-623-265-627-652-761-756-627-362-372-216-517-5_V.mp3)
  Segment 46: 101700ms ~ 102276ms (file: 126-261-162-230-302-574-167-165-130-203-243-270-123-231-502-605-570-307-312-320-230-203-315-107-615-106-603-632-362-103-173-260-260-620-260-623-620-260-362-302-567-574-562-574-6_W.mp3)
  Segment 47: 103276ms ~ 103852ms (file: 627 672 623 134-106-132-134-103-124-132-132-163 134-123-132-132-126-134-132-132-132-542-540 527-276-275-612-162-167-126-261-621-763-671-316-613-103-163-163-361-162-126-103-162-1_V.mp3)
  Segment 48: 104852ms ~ 105428ms (file: 671-167 672-627-627 621-672-627-632-623-627-627-627-623-627-627-627-627-623-627-672-267 267-627-267_W.mp3)
  Segment 49: 106428ms ~ 107004ms (file: 672-736-672-675-736-754-376-371-371-137-136-317-316-752-237-267-726-176-357-127-327-213-726-756-756-756-657-265-621-651-625-672-267-267-267-321-672-627-271-627-362-163-163-526-5_W.mp3)
  Segment 50: 108004ms ~ 118156ms (file: 675-123-125-276-765-725-753-352-510-135-531-310-265-613-213-216-526-567-567-657-657-312-265-263-635-653-567-657-567-567-105-574-627 675-316-267-236-672-263-276-267-276-267-201-2_W.mp3)
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
> **Time**: 2026-04-04 04:31:05
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "뭘 살까 고기를 살까 사과를 살까 우리 아이들이 좋아하는 피자도 한 판 살까",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "뭘", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "살까", "start_ms": 180, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "고기를", "start_ms": 400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "살까", "start_ms": 720, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "사과를", "start_ms": 950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "살까", "start_ms": 1280, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "우리", "start_ms": 1500, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "아이들이", "start_ms": 1700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "좋아하는", "start_ms": 2180, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "피자도", "start_ms": 2600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 2980, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "판", "start_ms": 3120, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "살까", "start_ms": 3300, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "하지만 저희가 마트 갈 시간이 부족할 것 같아요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "하지만", "start_ms": 15, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "저희가", "start_ms": 380, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "마트", "start_ms": 700, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "갈", "start_ms": 970, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "시간이", "start_ms": 1100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "부족할", "start_ms": 1420, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 1780, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 1880, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "빨리 공항부터 가야 하거든요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "빨리", "start_ms": 10, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "공항부터", "start_ms": 280, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "가야", "start_ms": 700, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "하거든요", "start_ms": 980, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "이 추운 미네소타까지 정말 반가운 그리고 소중한 손님이 찾아오셨다는 소식",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "추운", "start_ms": 140, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "미네소타까지", "start_ms": 410, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 1050, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "반가운", "start_ms": 1320, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 1700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "소중한", "start_ms": 2020, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "손님이", "start_ms": 2400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "찾아오셨다는", "start_ms": 2780, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "소식", "start_ms": 3400, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "방금 막 비행기가 착륙했다는데요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "방금", "start_ms": 30, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "막", "start_ms": 300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "비행기가", "start_ms": 480, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "착륙했다는데요", "start_ms": 950, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "비법을 듬뿍 담아 볶아낸 간짜장 그 고소한 풍미는 입맛 까다로운 아이들에게도 단연 인기 만점입니다",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "비법을", "start_ms": 40, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "듬뿍", "start_ms": 410, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "담아", "start_ms": 680, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "볶아낸", "start_ms": 950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "간짜장", "start_ms": 1320, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 1750, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "고소한", "start_ms": 1870, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "풍미는", "start_ms": 2250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "입맛", "start_ms": 2620, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "까다로운", "start_ms": 2900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "아이들에게도", "start_ms": 3380, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "단연", "start_ms": 4050, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "인기", "start_ms": 4320, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "만점입니다", "start_ms": 4600, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "내놓기가 무섭게 바닥을 드러낼 때가 많다네요",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "내놓기가", "start_ms": 20, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "무섭게", "start_ms": 480, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "바닥을", "start_ms": 850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "드러낼", "start_ms": 1220, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "때가", "start_ms": 1580, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "많다네요", "start_ms": 1850, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "간짜장 진짜 맛있어",
"speaker": "speaker_4",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "간짜장", "start_ms": 100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 520, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "맛있어", "start_ms": 780, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "모자랄 때 넘어질 수 있고요 두 번째는 이제 각속도가 모자랄 때 넘어질 수 있는데 둘 중에 뭐가 이유인지",
"speaker": "speaker_5",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "모자랄", "start_ms": 60, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "넘어질", "start_ms": 600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 980, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "있고요", "start_ms": 1120, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "두", "start_ms": 1500, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "번째는", "start_ms": 1680, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 2050, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "각속도가", "start_ms": 2320, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "모자랄", "start_ms": 2850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 3220, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "넘어질", "start_ms": 3400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 3780, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "있는데", "start_ms": 3920, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "둘", "start_ms": 4300, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "중에", "start_ms": 4520, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "뭐가", "start_ms": 4800, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "이유인지", "start_ms": 5080, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "음식 진짜 많이 준비를 해오셨네요",
"speaker": "speaker_6",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "음식", "start_ms": 40, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 360, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 630, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "준비를", "start_ms": 900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "해오셨네요", "start_ms": 1280, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "선물도 씨가 참 진짜 많이 노력을 하신 거 같아요",
"speaker": "speaker_6",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "선물도", "start_ms": 30, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "씨가", "start_ms": 450, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "참", "start_ms": 720, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 880, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 1150, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "노력을", "start_ms": 1420, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "하신", "start_ms": 1800, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 2080, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 2200, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "어쨌든 제가 어떤",
"speaker": "speaker_7",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "어쨌든", "start_ms": 50, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 420, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 650, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "십분 정도 만드는데 주문은 금세 동이 난다나요",
"speaker": "speaker_8",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "십분", "start_ms": 35, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "정도", "start_ms": 400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "만드는데", "start_ms": 680, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "주문은", "start_ms": 1150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "금세", "start_ms": 1520, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "동이", "start_ms": 1800, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "난다나요", "start_ms": 2080, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "손님들이 오셔가지고 여러 가지 음식을 드시고",
"speaker": "speaker_9",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "손님들이", "start_ms": 60, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "오셔가지고", "start_ms": 550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "여러", "start_ms": 1100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "가지", "start_ms": 1320, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "음식을", "start_ms": 1600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "드시고", "start_ms": 1980, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "준비한 음식은 정해진 인분이 있습니다",
"speaker": "speaker_9",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "준비한", "start_ms": 20, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "음식은", "start_ms": 380, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "정해진", "start_ms": 750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "인분이", "start_ms": 1120, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 1500, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "어 손님들이 제일 평균적으로 좋아하시는 음식 위주로 준비를 해놓습니다",
"speaker": "speaker_9",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "어", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "손님들이", "start_ms": 160, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "제일", "start_ms": 650, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "평균적으로", "start_ms": 920, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "좋아하시는", "start_ms": 1550, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "음식", "start_ms": 2150, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "위주로", "start_ms": 2420, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "준비를", "start_ms": 2800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "해놓습니다", "start_ms": 3180, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "어떤 손님 오실지 모르기 때문에",
"speaker": "speaker_9",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "어떤", "start_ms": 15, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "손님", "start_ms": 280, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "오실지", "start_ms": 550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "모르기", "start_ms": 920, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "때문에", "start_ms": 1300, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "열리는 주간 3일 동안 방한 외국인의 카드 결제액은 전년 대비 20.2% 증가했다고 합니다",
"speaker": "speaker_10",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "열리는", "start_ms": 25, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "주간", "start_ms": 400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "3일", "start_ms": 680, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "동안", "start_ms": 950, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "방한", "start_ms": 1220, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "외국인의", "start_ms": 1500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "카드", "start_ms": 1980, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "결제액은", "start_ms": 2250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "전년", "start_ms": 2720, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "대비", "start_ms": 3000, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "20.2%", "start_ms": 3280, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "증가했다고", "start_ms": 3900, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "합니다", "start_ms": 4480, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "콘서트가 열리는 서울 광화문 시청 일대만 놓고 보면 32.4%라는 더욱 극적인 수치가 나왔다고 하는데요",
"speaker": "speaker_10",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "콘서트가", "start_ms": 30, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "열리는", "start_ms": 450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "서울", "start_ms": 820, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "광화문", "start_ms": 1100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "시청", "start_ms": 1480, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "일대만", "start_ms": 1750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "놓고", "start_ms": 2120, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "보면", "start_ms": 2400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "32.4%라는", "start_ms": 2680, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "더욱", "start_ms": 3450, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "극적인", "start_ms": 3720, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "수치가", "start_ms": 4100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "나왔다고", "start_ms": 4480, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "하는데요", "start_ms": 4950, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "이렇게 늘어난 수치는 BTS",
"speaker": "speaker_10",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "이렇게", "start_ms": 15, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "늘어난", "start_ms": 350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "수치는", "start_ms": 720, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "BTS", "start_ms": 1100, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "미니 버전 팔로 바로 카페트 위를 돌아다니는 진돗개들",
"speaker": "speaker_11",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "미니", "start_ms": 45, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "버전", "start_ms": 320, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "팔로", "start_ms": 600, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 880, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "카페트", "start_ms": 1150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "위를", "start_ms": 1520, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "돌아다니는", "start_ms": 1800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "진돗개들", "start_ms": 2320, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "이대로 바로 집안으로 들어갈 수는 없지",
"speaker": "speaker_11",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "이대로", "start_ms": 20, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "집안으로", "start_ms": 680, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "들어갈", "start_ms": 1100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "수는", "start_ms": 1480, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "없지", "start_ms": 1750, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "포지가 공주야",
"speaker": "speaker_11",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "포지가", "start_ms": 80, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "공주야", "start_ms": 450, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "저희가 웃으니까 그제서야 꼬리를 흔들고 애교를 부리는 공주",
"speaker": "speaker_11",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "저희가", "start_ms": 25, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "웃으니까", "start_ms": 400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "그제서야", "start_ms": 880, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "꼬리를", "start_ms": 1350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "흔들고", "start_ms": 1720, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "애교를", "start_ms": 2100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "부리는", "start_ms": 2480, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "공주", "start_ms": 2850, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "아이고 너희들도 고생이 많지 조금만 기다려",
"speaker": "speaker_11",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "아이고", "start_ms": 40, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "너희들도", "start_ms": 410, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "고생이", "start_ms": 880, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "많지", "start_ms": 1250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "조금만", "start_ms": 1580, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "기다려", "start_ms": 1950, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "조금만 참으면 앞으로 좋은 일만 가득할 테니까",
"speaker": "speaker_11",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "조금만", "start_ms": 20, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "참으면", "start_ms": 400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "앞으로", "start_ms": 780, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 1150, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "일만", "start_ms": 1420, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "가득할", "start_ms": 1700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "테니까", "start_ms": 2080, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "중요하구나 내가 현금을 가지고 있으면 나는 다 계좌에 넣어서 풀베팅을 할 놈이구나",
"speaker": "speaker_12",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "중요하구나", "start_ms": 35, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "내가", "start_ms": 620, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "현금을", "start_ms": 900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "가지고", "start_ms": 1280, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있으면", "start_ms": 1650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "나는", "start_ms": 2020, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 2300, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "계좌에", "start_ms": 2420, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "넣어서", "start_ms": 2800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "풀베팅을", "start_ms": 3180, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 3700, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "놈이구나", "start_ms": 3880, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "도박쟁이 새끼",
"speaker": "speaker_13",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "도박쟁이", "start_ms": 100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "새끼", "start_ms": 580, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "이거를 크게 깨달아서 이제 적금도 여러 군데 이제 많이 들고",
"speaker": "speaker_12",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "이거를", "start_ms": 30, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "크게", "start_ms": 400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "깨달아서", "start_ms": 680, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 1150, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "적금도", "start_ms": 1420, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "여러", "start_ms": 1800, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "군데", "start_ms": 2080, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 2350, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 2620, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "들고", "start_ms": 2900, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "너무 잘했다 이제 오늘도 되게 이전과는 달랐던 게",
"speaker": "speaker_13",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "너무", "start_ms": 40, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "잘했다", "start_ms": 320, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 700, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "오늘도", "start_ms": 980, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "되게", "start_ms": 1350, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "이전과는", "start_ms": 1620, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "달랐던", "start_ms": 2100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 2480, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "오늘 2500만 원 수익을 냈는데 누군가의 연봉이잖아요",
"speaker": "speaker_12",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 25, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "2500만", "start_ms": 300, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "원", "start_ms": 920, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "수익을", "start_ms": 1100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "냈는데", "start_ms": 1480, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "누군가의", "start_ms": 1850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "연봉이잖아요", "start_ms": 2320, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "그렇죠 그런 생각을 많이 하면서 또 제 마음을 좀 누르는 걸 제가 스스로 느끼니까 아 차라리",
"speaker": "speaker_13",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "그렇죠", "start_ms": 50, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그런", "start_ms": 420, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "생각을", "start_ms": 700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 1080, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "하면서", "start_ms": 1350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 1720, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "제", "start_ms": 1900, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "마음을", "start_ms": 2080, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 2450, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "누르는", "start_ms": 2620, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "걸", "start_ms": 3000, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 3150, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "스스로", "start_ms": 3420, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "느끼니까", "start_ms": 3850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 4350, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "차라리", "start_ms": 4520, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "벌써 음식 준비 중입니다 이건 뭘 만드는 걸까요",
"speaker": "speaker_14",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "벌써", "start_ms": 60, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "음식", "start_ms": 450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "준비", "start_ms": 780, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "중입니다", "start_ms": 1100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이건", "start_ms": 1600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "뭘", "start_ms": 1950, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "만드는", "start_ms": 2180, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "걸까요", "start_ms": 2550, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "소갈비 잘 준비하고 있습니다",
"speaker": "speaker_15",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "소갈비", "start_ms": 40, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 520, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "준비하고", "start_ms": 700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 1180, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "코스 양보가 필요합니다",
"speaker": "speaker_16",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "코스", "start_ms": 20, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "양보가", "start_ms": 400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "필요합니다", "start_ms": 780, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "코스 양보가 필요합니다",
"speaker": "speaker_16",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "코스", "start_ms": 20, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "양보가", "start_ms": 400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "필요합니다", "start_ms": 780, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "코스 양보가 필요합니다",
"speaker": "speaker_16",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "코스", "start_ms": 20, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "양보가", "start_ms": 400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "필요합니다", "start_ms": 780, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "무엇보다 대만 사회가 한국의 방위 산업에 대해 큰 충격을 받은 가장 큰 이유는",
"speaker": "speaker_17",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "무엇보다", "start_ms": 30, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "대만", "start_ms": 450, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "사회가", "start_ms": 720, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "한국의", "start_ms": 1100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "방위", "start_ms": 1420, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "산업에", "start_ms": 1700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "대해", "start_ms": 2080, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 2350, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "충격을", "start_ms": 2520, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "받은", "start_ms": 2950, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 3220, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 3500, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이유는", "start_ms": 3680, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "한국이 단순히 무기를 개발하는 수준을 넘어 실제로 대량 생산과 보급을",
"speaker": "speaker_17",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "한국이", "start_ms": 25, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "단순히", "start_ms": 400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "무기를", "start_ms": 780, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "개발하는", "start_ms": 1150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "수준을", "start_ms": 1620, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "넘어", "start_ms": 2000, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "실제로", "start_ms": 2280, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "대량", "start_ms": 2650, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "생산과", "start_ms": 2920, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "보급을", "start_ms": 3350, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "특수한 상황이 아니라 주도주에서 자주 나오는 흐름이라는 걸 다른 사례로 보여드리겠습니다",
"speaker": "speaker_18",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "특수한", "start_ms": 30, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "상황이", "start_ms": 450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 82

---
*Generated by WKAppBot ask gemini — 2026-04-04 04:31:05*
