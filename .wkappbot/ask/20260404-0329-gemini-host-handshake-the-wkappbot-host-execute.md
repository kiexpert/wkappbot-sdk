# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=29532  time=03:27:56  uptime=6971s  mem=1924MB  system_processes=363"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260404_032525.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 361-130-610-160-160-610-136-301-163-613-163-167-310-130-310-216-576-756-130-201-627-631-326-756-765-637-302-730-137-126-167-162-362-362-632 576-571-517-517-157-136-132-675-756-7_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 216-120-163-126 752-275-276-372-320-132-321-236-267-317-576-310 710-102-172 136-362-267-237-534-513-572-756-752-201-130-103-302-316-301-136-162-763-673-167-576-657-574-756-567-6_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 107-106-167-362-316-263-631-610-160-610-657-610-163-312-103-136-561-657-265-126-103-301-136-137-134-574-136-170-561 136-136-170-167-516-103-130-103-103-132-136-136-123-617-105-1_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 310-301-310-103-312-231-163-316-301-203-203-321-312-301-237-163-310-130-103-132-312-302-362-230-621-263-106-103-130-130-310-103-130-367-326-136-132-103-607-367-371-316-310-136-1_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 372-367-615-631-351-351-351-534-513-315-312-325-312-321-231-163-123-317-312-137-315-321-321-316-326-312-136-163-672-317-271-367-173-361-316-127-217-132-271-271-127-367-365-327-6_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 567-756-265-612-275-625-672-675-652-756-265-765-756-576-627-672-752-765-675-576-567-657-576-627-675-756-675-675-627-672-671-675-231-263-236-265-652-625-567-657-675-567-527-675-5_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 361-163-613-612-163-231-321 275-276-276_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 376-362-165-603-623-265-206-120-213-631-613-341-132-674-671-672-765-672-637-627-621-645-651-657-657-172-713-317-132-341-637-316-316-312-541-123-165-143-127-617-675-675-756-756-5_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 567-567-516-675-576-675-756-576-756-576-657-567-576-576-576-574-576-576-657-567-567-576-576-567-657-567-576 162-621 756-576-576-752 574-574-620-231-310-316-613-312-302-130-310 5_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 312-123-215-237-123-251-207-207-276-207-207-203-132-132-275-732-123-315-132-321-207-271-201-265-265-302-532-312-312-132-531-370-325-203-312-130-306-301-725-521-516-325-352-351-5_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 216-167-327 261-621-602-126-621-162-126-162_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 602-620-620-570-165-610-132-130-103-761-160-130-610-316-326-351-357-302-312-203-532-361-301-703-321-130-130-156-103-615-136-136-165-621-261-612-672-756-756-726-762-276-735-561-7_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 126-236-126-562-567-576-567-576-576-567-567-576-576-576-576-567-132-130-320-206-231-320-132-321-326-735-365-673-650-567-576-576-125-361-316-263-265-236-236-231-132-672-136-675-6_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 103-316-162-517-123-162-137-103-160-163-167-136-163-103-362-162-130-106-103-160-162-167-670-132-136-132-136-516-627-126-237-654-621-527-162-130-136-167-163-163-106-103-617-361-6_V.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 361-756-765-230-236-236-203-310-103-130-301-321-263-765-671-671-106-517-576-350-106-103-136-130-103-136-103-316-630-316-316-163-106-163-163-103-132-301-123-321-571-754-316-130-3_V.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 103-765-137-136-136-312-157-102-135-124-710-356-315-132-130-637-162-612-236-210-156-263-132-136-132-316-132-103-516-532-160-326 657 156 516 126 637-306-306-637-367-306 167-156-5_V.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 362-632-653-362-362-265-263-263-256-265-265-265-623-625-265-265-625-365-625-263-526-625-365-362-356-653-635-327-572-356-365-365-316-653-615-135-165-165-167-167-631-163-632-627-6_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 673-623-637-673-627-673-762-627-627-672-675-627-675-623-625-627-672-652-657-625-657-673-325-673-173-162-651-130-136-126-162-167-361-136-360-637-610-601-630-631-367-637-631-106-1_W.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 217-265-562-756-275-123-312-621-621 267-316-136-321-312-126-623-216-136 213-621-167-132-123_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 136-201-762-632-170-376 613-103-136-103-132-602-632-137-162-610-132-132-260-167-127-130-632-136-132-136-617-610-163-617-163-102-163-517-136-136 561-201-765-106-176-160-671-106-5_V.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 612-162-627-213-316 573-203 670-621-237 605-326-312-321-302-351-375-360-532-132-132-132-153-135-315-327-327-673-136-310-206-207-256-362-602-567-236-103-321-103-132-123-132-132-1_W.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 615-675-617-762-273-326-205-250-621-620-216-621-612-216-123-216-126-162-231-263-361-103-163-130-316-301-136-103-136-617-312-132-103-132-341-137-136-716-107-107-105-137-132-612-6_V.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 126-675-675-756-162-756-521-162-752-271-276-765-762-756-571-237-175-731-327-167-163-576-756-623-563-367-236-725-567-657-576-756-265-274-627_W.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 236-321-302-230-231-120-130-260-261-163-162-103-320-236-210-123-120-162-612-613-756-750-307-321-320-203-236-321-106-623-612-602-162-132-217-236-612-126-765-213-120-132-102-136-1_V.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 150-103-163-321-276-620-621-260-602-632-632-162-602 162-132 136-106-571-103-103-312-103-103-371-261-136-126-173-162-651-106-231-715-130-236-173-136-126-613-103-617-137-162-106-1_V.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 615-167-576 627-621-632-621-326-321-312-123-312-321-623-163-762-761-165_W.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 756-756-617-672-672-736-316-261-625-135-271-720-276-106-612-176-765-765-132-376-762-756-675-567-576-160-601-130-625-567-162-102-671-762-765-627-627-675-671-106-130-517-576-567-6_W.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 217-126-231-132-123-136-621 165-576-173-167-320_W.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 765-765-756-765-120-103-763-763-167-716-716-716-670-672-620-671-761-761-627-267-672-627-261-756-612-263-261-623-261-627-263-627-621-267-621-162-216-251-621-623-623-102-621-167-5_W.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 657-567-576-567-567-562-657-265-567-576-376-567-620-610-627-615-567-705-620-276-650-657-672-625-627-267-652-657-567 637-673-567-657-632-621-627-672-632-621_V.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 257-135-103-167-136-136-162-126-120-130-136-301-312-103-136-134-310-130-310-103-123-162-275-216-621-263-362-162-163 705-756-613-673-627-672-137-103-316-706-706-761-706-716-176-7_V.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 137-173-137-762-574-574-620-260-162-236-612-130-123-503-365-627-167-167-167-167-106-130-106-567-106-163-612-610-613-106-106-103-103-310-103-130-360-302-306-236-267-627-623-620-2_W.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 103-301-210-620-260-620-127-260-261-102-612-160-316-531-213-726-672-670-602-362-623-362-326-326-326-236-206-157-756-567-107-105-605-571-752-756-103-130-136-130-570-573-375-703-7_W.mp3)
  Segment 34: 52008ms ~ 61512ms (file: 305-570-530-301-103-312-132-620-672-123-210-670-123-540-206-260-134-753-267-123-102-102-126-201-210-567-201-206-260-132-216-120-753-752-103-210-261-574-276-210-126-260-312-103-6_W.mp3)
  Segment 35: 62512ms ~ 63088ms (file: 134-610-136-136-153-173-615-103-103-136-617-126-162-102-103-136-123-120-162-307-135-123-672-607-160-651-156-106-130-132-126-365-135-137-106-103-103-102-130-172-120-162-103-153-1_V.mp3)
  Segment 36: 64088ms ~ 64664ms (file: 657-765-267-216-672-672-657-367-652-652-765-675-176-631-167-631-765-673-203-203-236-610-263-126-306-351-627-620-620-672-672-625-670-712-312-317-127-130-132-310-102-650-651-564-1_W.mp3)
  Segment 37: 65664ms ~ 66240ms (file: 123-326-507-573-126-357-172-732-217-123-103-130-153-516-572-652-675-265-627-263-765-756-265-260-265-326-362-321-263-621-602-546-567-567-562-605-675-607-361-316-136-163-621-621-6_W.mp3)
  Segment 38: 67240ms ~ 67816ms (file: 657-657-605-312-365-652-265-175-627-261-752-572-705-172-725-360-627-673-275-267-326-652-263-263-103-132-326-312-152-263-167-160-620-756-627-657-327-632-623-260-610-675-260-672-7_W.mp3)
  Segment 39: 68816ms ~ 74504ms (file: 162-621-136-103 132 142-756-735-103-136-573-573 627-612-103-163-621-765-675 216 756-765-675-321-267-237-326-236-623-216-623-263-621-261-260-261-120-106-107-130-105-102-130-312-1_W.mp3)
  Segment 40: 75504ms ~ 76080ms (file: 752-752-756-752-752-756-765-760-752-765-675-576-756-756-671-675-576-765 623 601-136-765-317-753-167-176-321-103-106-316-162-136-631-167-612-103-167-167-163-130-103-610-103-176-7_W.mp3)
  Segment 41: 77080ms ~ 77656ms (file: 726-213-267-263 716 216-231-623-632-362-213-263_W.mp3)
  Segment 42: 78656ms ~ 79232ms (file: 167-167-627-602-206-602-136-103-136-153-167-765-756-103-317-157-567-127-671-751-136-103-761-607-371-103-510-103-725-671-231-651-263-263-263-263 671-612-167-567-652-567-657-657-2_W.mp3)
  Segment 43: 80232ms ~ 85452ms (file: 127-260-271-136-132-172-216-761-130-123-152-103-126-126-136-120-120-120-123-501-102-126-123-213-167-176-371-236-106-150-312-570-263-657-452-231-260-261-532-216-260-510-527-237-7_W.mp3)
  Segment 44: 86452ms ~ 87028ms (file: 637-263-103-162-136-106-106-102-103-123-103-573-756-712-627-620-310-301-321-103-132-213-176-710-176-102-103-162-103-301-106-103-106-106-103-167-103-106-162-601-602-260-260-206-2_V.mp3)
  Segment 45: 88028ms ~ 88604ms (file: 761-571-167-103-136-315-135-134-132-632-165-316-125-130-136-123-317-310-123-263-236-217-172-123-130-152-612-652-657-371-762-726-762-132-765 132-541 106-106-102-167-536-316 732-7_V.mp3)
  Segment 46: 89604ms ~ 90180ms (file: 167-137-132-301-506-504-570-576-576-562-602-162-132-312-132-513-156-317-176-167-136-164-156-134-657-675-163-167-316-162-136-617-106-617-672-672-607-132-134-103-130-361-750-765-6_V.mp3)
  Segment 47: 91180ms ~ 95032ms (file: 237-236-321-231-231-123-213-621-321-216-213-263-312-136-132-132-231-725-327-326-273-263-236-263-236-326-236-362-231-326-236-231-321-326-326-326-326-321-231-321-326-236-326-326-3_W.mp3)
  Segment 48: 96032ms ~ 96608ms (file: 607-162-623-123-312-236-106-136-365-362-362-210-106-103-620-162-627-261-523-126-362-206-216-136-526-215-726-326-612-625-765-657-651-256-725-320-206-627-265-650-562-132-601-601-5_W.mp3)
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
> **Time**: 2026-04-04 03:29:18
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
{"word": "안녕", "start_ms": 50, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "하세요", "start_ms": 300, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "만나서 반갑습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "만나서", "start_ms": 45, "dur_ms": 260, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 310, "dur_ms": 260, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "오늘 날씨가 좋네요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 60, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 250, "dur_ms": 170, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "What is your name?",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "What", "start_ms": 30, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "your", "start_ms": 250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "name?", "start_ms": 360, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "제 이름은 민수입니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "제", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "이름은", "start_ms": 130, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "민수입니다", "start_ms": 340, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "어디에 가세요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "어디에", "start_ms": 55, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "가세요?", "start_ms": 300, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "학교에 가요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "학교에", "start_ms": 40, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "가요", "start_ms": 330, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 9,
"transcript": "배가 고파요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "배가", "start_ms": 70, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "고파요", "start_ms": 230, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "같이 밥 먹을까요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "같이", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "밥", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "먹을까요?", "start_ms": 310, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "좋아요, 갑시다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "좋아요,", "start_ms": 40, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "갑시다", "start_ms": 260, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "이것은 무엇입니까?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "이것은", "start_ms": 60, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "무엇입니까?", "start_ms": 280, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "그것은 책이에요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "그것은", "start_ms": 45, "dur_ms": 215, "phase_lr_deg": 0.0},
{"word": "책이에요", "start_ms": 270, "dur_ms": 260, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "한국어 공부가 재미있어요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 80, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "공부가", "start_ms": 250, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "재미있어요", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "정말요? 다행이네요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "정말요?", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "다행이네요", "start_ms": 240, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "Excuse me, where is the toilet?",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Excuse", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "me,", "start_ms": 190, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "where", "start_ms": 280, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 390, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 450, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "toilet?", "start_ms": 500, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "화장실은 저기에 있어요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "화장실은", "start_ms": 40, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "저기에", "start_ms": 270, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "감사합니다",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "감사", "start_ms": 60, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "합니다", "start_ms": 320, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "천만에요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "천만", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "에요", "start_ms": 260, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "지금 몇 시예요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "지금", "start_ms": 55, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "몇", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "시예요?", "start_ms": 310, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "세 시 삼십 분입니다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "세", "start_ms": 70, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "시", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "삼십", "start_ms": 250, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "분입니다", "start_ms": 380, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "도와드릴까요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "도와", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "드릴까요?", "start_ms": 230, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "네, 부탁합니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "부탁합니다", "start_ms": 180, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "이거 얼마예요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "이거", "start_ms": 35, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "얼마예요?", "start_ms": 230, "dur_ms": 260, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "만원입니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "만원", "start_ms": 60, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "입니다", "start_ms": 290, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "조금 깎아 주세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "조금", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "깎아", "start_ms": 210, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 370, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "안 돼요, 정가예요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "안", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "돼요,", "start_ms": 130, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "정가예요", "start_ms": 280, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "알겠습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "알겠", "start_ms": 65, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "습니다", "start_ms": 300, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "다음에 또 오세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 40, "dur_ms": 170, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 220, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "오세요", "start_ms": 310, "dur_ms": 180, "phase_lr_deg": 0.0}
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
"transcript": "안녕히 계세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 50, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "계세요", "start_ms": 290, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "안녕히 가세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 45, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 295, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "취미가 뭐예요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "취미가", "start_ms": 60, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "뭐예요?", "start_ms": 290, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "저는 영화 보는 것을 좋아해요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "영화", "start_ms": 260, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "보는", "start_ms": 410, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "것을", "start_ms": 550, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "좋아해요", "start_ms": 680, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "어떤 영화를 좋아하세요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "어떤", "start_ms": 50, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "영화를", "start_ms": 190, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "좋아하세요?", "start_ms": 360, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "액션 영화를 좋아해요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "액션", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "영화를", "start_ms": 190, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "좋아해요", "start_ms": 350, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "가장 좋아하는 배우는 누구예요?",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "가장", "start_ms": 60, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "좋아하는", "start_ms": 170, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "배우는", "start_ms": 330, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "누구예요?", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "송강호를 좋아해요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "송강호를", "start_ms": 45, "dur_ms": 260, "phase_lr_deg": 0.0},
{"word": "좋아해요", "start_ms": 315, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "생일이 언제예요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "생일이", "start_ms": 55, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "언제예요?", "start_ms": 245, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "제 생일은 오월 삼일입니다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "제", "start_ms": 70, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "생일은", "start_ms": 140, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "오월", "start_ms": 290, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "삼일입니다", "start_ms": 400, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "축하합니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "축하", "start_ms": 40, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "합니다", "start_ms": 270, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "선물이에요, 받으세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "선물이에요,", "start_ms": 80, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "받으세요", "start_ms": 300, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "와, 고맙습니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "와,", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "고맙습니다", "start_ms": 150, "dur_ms": 380, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "가족이 몇 명이에요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "가족이", "start_ms": 60, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "몇", "start_ms": 280, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "명이에요?", "start_ms": 350, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "네 명이에요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 45, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "명이에요", "start_ms": 180, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "부모님과 여동생이 있어요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "부모님과", "start_ms": 70, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "여동생이", "start_ms": 260, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 420, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 48,
"transcript": "다음에 또 만나요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 240, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 330, "dur_ms": 210, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-04 03:29:18*
