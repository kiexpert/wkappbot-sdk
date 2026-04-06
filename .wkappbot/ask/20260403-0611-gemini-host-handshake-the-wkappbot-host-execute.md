# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=06:09:53  uptime=17439s  mem=1403MB  system_processes=448"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_060945.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 301-752-752-756-756-621-620-206-102-162-302-231-576-567-756-576-752-576-265-236-236-203-306-312-106-106-160-126-620-620-620-316-230-260-627-260-120-132-260-230-106-102-630-236-2_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 130-136-756-106-103-102-103-162-106-125-103-123-635-162-126-175-671-672-103-162-106-761-162-163-103-761-132-176-172-167 167-136-173-615-103-132-102-320-167-162-162-103 103-517-2_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 132-413-625-672-206-236-230-206-230-263-206-302-236-236-263-267-625-572-570-256-265-260-236-206-260-765-675-513-103-135-362-672-621-627-621-267-601-136-312-312-756-173-710-312-3_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 375-302-312-136-103-102-103-756-765-756-675-756-675-260-230-130-301-312-203-302-236-326-230-320-301-321-532-530-513-163-362-320-231-321-231-321-321-301-302-574-325-230-320-203-3_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 326-675-306-316-135-315-305-514-517-136-103-132-135-137-137-130-120-163-631-361-756-321-302-123-236-213-621-126-120-271-756-765-261-107-310-576-576-567-576-657-567-567-132-657-5_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 237-237-132-765-675-167-164-163-167-162-765-627-620-172-325-305-327-301-132-215-310-321-631-231-153-162-167-107-120-172-367-732-623-763-371-327-302-675-302-316-641-361-263-267-2_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 672-675-756-657-165-167-671-671-576-507-625-267-762-561-621-675-267-672-726-157-706-175-657-572-562-126-176-657-657-675-675-756-576-567-167-175-576-567-657-162-617-162-721-761-6_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 231-123-360-312-126-316-120-126-621-236-206-623-623-236-326-362_W.mp3)
  Segment 9: 12608ms ~ 24524ms (file: 310-301-632-623-203-627-206-206-260-320-236-103-312-123-132-504-135-135-602 312-132-230_V.mp3)
  Segment 10: 25524ms ~ 26100ms (file: 230-260-206-206-206-123-123-103-103-301-130-157-765-326-103-130-302-657-263-362-605-106-132-571-125-137-321-320-510-523-235-201-702-260-320-320-203-203-263-261-103-102-162-162-5_V.mp3)
  Segment 11: 27100ms ~ 27676ms (file: 627-123-637-213-623-362-637-623-362-765-756-756-756-132-123-321-123-103-627-163-216-321-627-672-321-602-136-731-675-675-627-236-263-213-120-213-125-167-123-103-321-321-361-267-3_W.mp3)
  Segment 12: 28676ms ~ 29252ms (file: 765-675-267-127-136-316-137-316-103-136-136-132-134-106-106-167-167-132-132-713-173-136-236-236-321-705-753-103-103-103-132-623-726-176 627-623-670-167-316-367-617-376-136-136-3_V.mp3)
  Segment 13: 30252ms ~ 30828ms (file: 132 132-132-132-136-162-103 671-615_V.mp3)
  Segment 14: 31828ms ~ 32404ms (file: 576-576-571-536-756-576-625-536-567-576-567-265-657-567-576-576-567-572-576-657-527-652-672-563-576-756-576-657-564-567-576-567-517-567-756-517-765-657-567-765-675-367-765-675-7_W.mp3)
  Segment 15: 33404ms ~ 33980ms (file: 547-576-127-312-765-162-716-317-123-716-671-671-675-167-613-167-716-132-162-136-316-312-316-672-672-675-671-762-671-752-167-672-176_W.mp3)
  Segment 16: 34980ms ~ 35556ms (file: 276-257-271-275-267-273-271-273-213-236-527-317-261-621-652-562-567-572-576-123-312-162-157 647_W.mp3)
  Segment 17: 36556ms ~ 37132ms (file: 310-310-360-321-312-312-327-235-257-237-256-257-203-230-271-372-321-321-231-310-132-230-103-103-130-230-310-123-260-263-362-130-103-107-107-103-652-103-157-105-126-612-602-376-6_V.mp3)
  Segment 18: 38132ms ~ 38708ms (file: 756-762-752-716-276-163-126-107-172-126-312-326-126-102-316-216-231-672-762-721-761-726-127-102-123-567-356-231-102-312-123-136-102-132-316-321-326-231-236-267-326-523-563-527-5_V.mp3)
  Segment 19: 39708ms ~ 40284ms (file: 103-106-136-316-617-326-627-162-267-621-715-602-130-673-153-106-326-267-120-103-310-261-630-106-130-620-620-132-623-167-617-136-617-617-607-617-167-163-136-123-176-160-236-265-2_V.mp3)
  Segment 20: 41284ms ~ 41860ms (file: 265-526-562-103-201-203-263-263-206-206-205-102-132-206-261-206-106-102-103-576-576-762-672-762-167-310-213-260-657-326-302-230-301-260-203-237-301-312-320-302-675-216-307-302-3_W.mp3)
  Segment 21: 42860ms ~ 43436ms (file: 126-126-102-162-126-162-132-102-175-267-173-163-130-301-312-216-217 126-601-263-260-326-762-672-672-327-132-260-206-201-267_V.mp3)
  Segment 22: 44436ms ~ 45012ms (file: 103-132-312-103-167-130-160-567-152-762-106-167-136-617-136-617-103-176-516-106-160-615-136-371-176-513-537-531-761-713-741-471-132-351-756-271-627-312-321-167-607-267-620-260-2_V.mp3)
  Segment 23: 46012ms ~ 46588ms (file: 627-267-627-627-267-265-265-276-136-612-216-267-267-267-267-267-326-167-167-627-627-672-672-672-672-627-627-627-623-632-564-625-267-276-236-267-267-267-263-267-267-267-627-267-6_W.mp3)
  Segment 24: 47588ms ~ 48164ms (file: 756-507-564-267-625-265-675-645-605-235-523-250-625-527-507-527-567-650-654-657-236-372-623 132-162-162_W.mp3)
  Segment 25: 49164ms ~ 49740ms (file: 621-657-576-756-765-675-762-263-621-261-365-657-576-657-361-713_W.mp3)
  Segment 26: 50740ms ~ 51316ms (file: 326-216-362-162-302-321-132-123-130-103-672-362-230-352-230-203-276-672-367-103-316 217 750-750-760-672-627-627-627-623-612-132 617-621-261-167-132-103-126-216-123-132-103-137-1_V.mp3)
  Segment 27: 52316ms ~ 52892ms (file: 756-275-567-567-576-265-657-217-321-312-301-326-326-263-613-267-627-627 627-603-623-572-652-531-567-250-265-130-312-157-165-312-126-621-562-256-362-132-273-675-635-621-672-657-7_W.mp3)
  Segment 28: 53892ms ~ 54468ms (file: 165-612-617-621-756-675-672-571-570-570-507-152-612-560-103-103-103-132-123-167-167-617-607-607-607-103-106-103-106-210-126-213-210-543-157-136-106-310-103-312-213-210-632-263-2_W.mp3)
  Segment 29: 55468ms ~ 56044ms (file: 627-261-162-123-231-234-672-672-620-261-627-267-627-627 126-126-126-126-715-562-562-125 236-132_W.mp3)
  Segment 30: 57044ms ~ 71444ms (file: 576-574-574-124-176-107-107-176-106-123-167 716-713-716-306-321-261-657-106-103-136-107-103-137-675-167-203-617-715-106-102-103-175-135-105-157-567-570-632-653-160-130-153-106-5_V.mp3)
  Segment 31: 72444ms ~ 73992ms (file: 326-132-136-157-165-106-136-103-301-132-631-136-561-105-163-136-137-136-351-316-361-103-103-651-567-570-103-103-103-103-316-301-136-135-540-301-130-130-103-765-650-637-103-163-1_V.mp3)
  Segment 32: 74992ms ~ 75568ms (file: 206-602-203-236-206-230-302-320-302-302-105-312-326-236-312-103-261-756-570-756-756-756-750-103-130-162-106-103-132-136-130-123-103-136-623-167-516-560-562-256-261-561-620-362-1_V.mp3)
  Segment 33: 76568ms ~ 77144ms (file: 267-632-267-365-567-756 230-132-320-132-317-657-672-652-672-672-673-632-672-673-627-725-675-765-675-765-675-632-675-675-675-765-623-360-312-312-310-230-312-321-301-316-263-657-7_W.mp3)
  Segment 34: 78144ms ~ 79044ms (file: 675-765-675-706-706-765-576-756-756-167-672-672-607-673-261-206-206-312-206-203-216-263-705-725-675-126-137-617-620-632-362-603-123-610-261-612-216 765-230-271 765-176 163-361_W.mp3)
  Segment 35: 80044ms ~ 80620ms (file: 321-231-231-321-132-231-321-123-312-213-321-312-213-231-321-127-213-213-263-762-627-612-132-153-132-521-321-527-312-256-213-321-126-126-132-231-123-127-132-213-261-216-231-123-2_W.mp3)
  Segment 36: 81620ms ~ 82196ms (file: 261-627-621-621-621-312-621-632-263-632-625-675-675-675-675-675 617-612-167-672-316-326 261-263-126-612-162-672-621-162-612-267-627-627-621-362-316-123 321-123-623-627-617-173-7_W.mp3)
  Segment 37: 83196ms ~ 83772ms (file: 625-361-126-162-162-132-326-312-263-162 756-756-756-321-163-763-735-627-576-751-753-735-756-735-375-756-627-612-621 613-306-163-617-617-162-167-162-160-612-162-612-617-607-612-6_W.mp3)
  Segment 38: 84772ms ~ 85348ms (file: 617-106 136-172-205-612-136-106-163-326-613-160-106-610-103-106-562-623-136-103-103-136-126-167-605-512-126-136-627-263-261-675-567_V.mp3)
  Segment 39: 86348ms ~ 86924ms (file: 103-132-321-132-203-321-120-231-312-126-326-127-175-126-201-201-261-316-301-316-316-301-672-672-134-103-136-126-627-765-765-706-547-752-627-627-316-167-602-765-756-765-760-541-1_V.mp3)
  Segment 40: 87924ms ~ 88500ms (file: 761-271-176-106-126-627-623-670-670-672-567-256-267-612-517-756 103-312 576-576-567-657-672-260-310-301-310-130-106-321-621-312-310-136-306-620-260-362-301-316-321-126-302-360-6_W.mp3)
  Segment 41: 89500ms ~ 90076ms (file: 317-375 276-267-621-617-267-625-265-625-261-267-265-261-267-236-261-260-512-325-235-261-236-621-261-612-162-562-365-356-356-356-375-356-356-562-532-351-576-571-537-576-576-574-5_W.mp3)
  Segment 42: 91076ms ~ 91652ms (file: 562-567 321-321-321-762-367 321-316-327-126-612-263-261-627-261 162-162-621-261-321-316-107-163-576-567-762-756-756-756-756-756-623-621 162-213-312-263-267-627 623-261-201-261-6_W.mp3)
  Segment 43: 92652ms ~ 93228ms (file: 120-123-132-261-136-261-236-362-263-623-123-362-263-261-621-623 672-627-731-261-267-265-267-261-621-106-136-160-365-671-106-107-105-361-316-365-613-312-231-637-123-261-312-657-6_W.mp3)
  Segment 44: 94228ms ~ 94804ms (file: 675-765-675-675-574-570-675-673-607-756-765-750-570-547-756-576-675-465-607-756-675-765-675-675-657-675-607-560-504-657-670-760-307-376-376-735-735-765-675-760-706-570-576-567-7_W.mp3)
  Segment 45: 95804ms ~ 96380ms (file: 621-263-310-301-136-132-610-106-126-302-301-106-106-617-671-162-627-162-126-206-652-657-312-312-236-623-261-612-673-216-261-612-136-301-310-132-326-123-301-132-326-236-362-624-2_W.mp3)
  Segment 46: 97380ms ~ 97956ms (file: 167 126-132-173-106-167-136-106-102-103-126-371-163-102-213-615-163-213-123-123-276-736-231-326-236-263-263-675-163-317-136-137-376-167-371-625-625 632-612-162-617-162-132-306-6_V.mp3)
  Segment 47: 98956ms ~ 99532ms (file: 263-317-103-136-360-632-362-623-621-326-236-203-231 620-260-206-263-206-203_V.mp3)
  Segment 48: 100532ms ~ 101108ms (file: 135-312-132-156-567-512-125-623-267-571-321-321-132-623-132-327-207-206-203-327-753-376-260-236-321-231-237-231-235-321-231-351-675-153-605-675-256-237-132-163-276-152-135-561-7_W.mp3)
  Segment 49: 102108ms ~ 102684ms (file: 103-136-132-137-716-136-103-103-130-106-163-132-136-126-216-261-302-231-620-301-320-602-260-621-231-326-203-260-123-756-706-106-130-103-132-136-132-312-132-312 671-761-716-176 5_V.mp3)
  Segment 50: 103684ms ~ 104260ms (file: 216-213-231-216-132-260-612-362-163-160-162-120-612-526-652-756-765-726-762-627-672-763-765-762-672-672-762-762-765-762-162-627-162-621-312-130-103-103-310-316-126-162-162-126-1_W.mp3)
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
> **Time**: 2026-04-03 06:11:00
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "안녕하세요 반갑습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 40, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 280, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "오늘 날씨가 정말 화창하네요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 180, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 340, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "화창하네요", "start_ms": 450, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "산책하기 좋은 날씨입니다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "산책하기", "start_ms": 35, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "좋은", "start_ms": 185, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "날씨입니다", "start_ms": 305, "dur_ms": 200, "phase_lr_deg": 5.0}
]
},
{
"segment": 4,
"transcript": "커피 한 잔 할까요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "커피", "start_ms": 60, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 170, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "잔", "start_ms": 240, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "할까요", "start_ms": 320, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "네 근처 카페로 가요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 45, "dur_ms": 70, "phase_lr_deg": 5.0},
{"word": "근처", "start_ms": 125, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "카페로", "start_ms": 255, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "가요", "start_ms": 395, "dur_ms": 100, "phase_lr_deg": 5.0}
]
},
{
"segment": 6,
"transcript": "아이스 아메리카노 주세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "아이스", "start_ms": 55, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "아메리카노", "start_ms": 205, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 435, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "(music)",
"speaker": "null",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 8,
"transcript": "오늘 기분은 어떠세요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "기분은", "start_ms": 180, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "어떠세요", "start_ms": 330, "dur_ms": 180, "phase_lr_deg": 5.0}
]
},
{
"segment": 9,
"transcript": "아주 상쾌하고 좋네요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "아주", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "상쾌하고", "start_ms": 150, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 340, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "What are your plans for tomorrow?",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "What", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": -2.0},
{"word": "are", "start_ms": 120, "dur_ms": 40, "phase_lr_deg": -2.0},
{"word": "your", "start_ms": 170, "dur_ms": 60, "phase_lr_deg": -2.0},
{"word": "plans", "start_ms": 240, "dur_ms": 110, "phase_lr_deg": -2.0},
{"word": "for", "start_ms": 360, "dur_ms": 50, "phase_lr_deg": -2.0},
{"word": "tomorrow?", "start_ms": 420, "dur_ms": 130, "phase_lr_deg": -2.0}
]
},
{
"segment": 11,
"transcript": "친구를 만나기로 했어요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "친구를", "start_ms": 70, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "만나기로", "start_ms": 220, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "했어요", "start_ms": 410, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "즐거운 시간 보내세요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "즐거운", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "시간", "start_ms": 210, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "보내세요", "start_ms": 330, "dur_ms": 200, "phase_lr_deg": 5.0}
]
},
{
"segment": 13,
"transcript": "감사합니다 당신도요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 45, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "당신도요", "start_ms": 275, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "주말에 영화 볼까요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "주말에", "start_ms": 65, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "영화", "start_ms": 205, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "볼까요", "start_ms": 335, "dur_ms": 150, "phase_lr_deg": 5.0}
]
},
{
"segment": 15,
"transcript": "네 어떤 영화가 좋을까요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 55, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 125, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "영화가", "start_ms": 255, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "좋을까요", "start_ms": 395, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "액션 영화는 어떠세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "액션", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "영화는", "start_ms": 170, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "어떠세요", "start_ms": 320, "dur_ms": 180, "phase_lr_deg": 5.0}
]
},
{
"segment": 17,
"transcript": "좋아요 저도 액션을 좋아해요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "좋아요", "start_ms": 70, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "저도", "start_ms": 200, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "액션을", "start_ms": 290, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "좋아해요", "start_ms": 440, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "그럼 일요일에 만나요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "일요일에", "start_ms": 155, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "만나요", "start_ms": 315, "dur_ms": 180, "phase_lr_deg": 5.0}
]
},
{
"segment": 19,
"transcript": "I'll see you then",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "I'll", "start_ms": 35, "dur_ms": 50, "phase_lr_deg": -2.0},
{"word": "see", "start_ms": 95, "dur_ms": 70, "phase_lr_deg": -2.0},
{"word": "you", "start_ms": 175, "dur_ms": 60, "phase_lr_deg": -2.0},
{"word": "then", "start_ms": 245, "dur_ms": 100, "phase_lr_deg": -2.0}
]
},
{
"segment": 20,
"transcript": "알겠습니다 이따 봐요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "알겠습니다", "start_ms": 60, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "이따", "start_ms": 270, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "봐요", "start_ms": 380, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "점심 메뉴는 정하셨나요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "점심", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "메뉴는", "start_ms": 180, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "정하셨나요", "start_ms": 330, "dur_ms": 200, "phase_lr_deg": 5.0}
]
},
{
"segment": 22,
"transcript": "비빔밥 어떠세요 맛있어요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "비빔밥", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "어떠세요", "start_ms": 200, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "맛있어요", "start_ms": 350, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "네 저도 비빔밥 좋아해요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 55, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "저도", "start_ms": 125, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "비빔밥", "start_ms": 215, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "좋아해요", "start_ms": 365, "dur_ms": 150, "phase_lr_deg": 5.0}
]
},
{
"segment": 24,
"transcript": "그럼 식당으로 갑시다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "식당으로", "start_ms": 155, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "갑시다", "start_ms": 325, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "Let's go have lunch",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Let's", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": -2.0},
{"word": "go", "start_ms": 120, "dur_ms": 50, "phase_lr_deg": -2.0},
{"word": "have", "start_ms": 180, "dur_ms": 70, "phase_lr_deg": -2.0},
{"word": "lunch", "start_ms": 260, "dur_ms": 130, "phase_lr_deg": -2.0}
]
},
{
"segment": 26,
"transcript": "맛있게 드셨나요 배불러요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "맛있게", "start_ms": 65, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "드셨나요", "start_ms": 215, "dur_ms": 160, "phase_lr_deg": 5.0},
{"word": "배불러요", "start_ms": 385, "dur_ms": 140, "phase_lr_deg": 5.0}
]
},
{
"segment": 27,
"transcript": "네 아주 든든하네요 하하",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 55, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 125, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "든든하네요", "start_ms": 235, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "하하", "start_ms": 445, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "소화도 시킬 겸 걸어요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "소화도", "start_ms": 40, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "시킬", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "겸", "start_ms": 290, "dur_ms": 50, "phase_lr_deg": 5.0},
{"word": "걸어요", "start_ms": 350, "dur_ms": 180, "phase_lr_deg": 5.0}
]
},
{
"segment": 29,
"transcript": "It's good to walk after eating",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "It's", "start_ms": 25, "dur_ms": 60, "phase_lr_deg": -2.0},
{"word": "good", "start_ms": 95, "dur_ms": 80, "phase_lr_deg": -2.0},
{"word": "to", "start_ms": 185, "dur_ms": 40, "phase_lr_deg": -2.0},
{"word": "walk", "start_ms": 235, "dur_ms": 100, "phase_lr_deg": -2.0},
{"word": "after", "start_ms": 345, "dur_ms": 110, "phase_lr_deg": -2.0},
{"word": "eating", "start_ms": 465, "dur_ms": 90, "phase_lr_deg": -2.0}
]
},
{
"segment": 30,
"transcript": "오늘 정말 즐거웠습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 60, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 190, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "즐거웠습니다", "start_ms": 300, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "다음에 또 만나요 안녕",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "또", "start_ms": 200, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "만나요", "start_ms": 270, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "안녕", "start_ms": 400, "dur_ms": 130, "phase_lr_deg": 5.0}
]
},
{
"segment": 32,
"transcript": "조심히 들어가세요 바이",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "조심히", "start_ms": 45, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "들어가세요", "start_ms": 195, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "바이", "start_ms": 425, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "See you again soon",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 30, "dur_ms": 60, "phase_lr_deg": -2.0},
{"word": "you", "start_ms": 100, "dur_ms": 50, "phase_lr_deg": -2.0},
{"word": "again", "start_ms": 160, "dur_ms": 100, "phase_lr_deg": -2.0},
{"word": "soon", "start_ms": 270, "dur_ms": 150, "phase_lr_deg": -2.0}
]
},
{
"segment": 34,
"transcript": "안녕히 가세요 화이팅",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 65, "dur_ms": 160, "phase_lr_deg": 5.0},
{"word": "가세요", "start_ms": 235, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "화이팅", "start_ms": 385, "dur_ms": 130, "phase_lr_deg": 5.0}
]
},
{
"segment": 35,
"transcript": "Have a wonderful evening",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "Have", "start_ms": 55, "dur_ms": 100, "phase_lr_deg": -2.0},
{"word": "a", "start_ms": 165, "dur_ms": 30, "phase_lr_deg": -2.0},
{"word": "wonderful", "start_ms": 205, "dur_ms": 150, "phase_lr_deg": -2.0},
{"word": "evening", "start_ms": 365, "dur_ms": 140, "phase_lr_deg": -2.0}
]
},
{
"segment": 36,
"transcript": "저도 들어가 볼게요 슝",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "저도", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "들어가", "start_ms": 150, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "볼게요", "start_ms": 270, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "슝", "start_ms": 420, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "오늘 수고 많으셨어요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 70, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "수고", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "많으셨어요", "start_ms": 310, "dur_ms": 200, "phase_lr_deg": 5.0}
]
},
{
"segment": 38,
"transcript": "내일도 힘내봅시다 얍",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "내일도", "start_ms": 45, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "힘내봅시다", "start_ms": 195, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "얍", "start_ms": 405, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "Good night everyone",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "Good", "start_ms": 35, "dur_ms": 80, "phase_lr_deg": -2.0},
{"word": "night", "start_ms": 125, "dur_ms": 100, "phase_lr_deg": -2.0},
{"word": "everyone", "start_ms": 235, "dur_ms": 220, "phase_lr_deg": -2.0}
]
},
{
"segment": 40,
"transcript": "안녕히 주무세요 꿀잠",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 60, "dur_ms": 160, "phase_lr_deg": 5.0},
{"word": "주무세요", "start_ms": 230, "dur_ms": 180, "phase_lr_deg": 5.0},
{"word": "꿀잠", "start_ms": 420, "dur_ms": 120, "phase_lr_deg": 5.0}
]
},
{
"segment": 41,
"transcript": "내일 만나요 안녕",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "내일", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 160, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "안녕", "start_ms": 320, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "Sweet dreams to you",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Sweet", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": -2.0},
{"word": "dreams", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": -2.0},
{"word": "to", "start_ms": 280, "dur_ms": 60, "phase_lr_deg": -2.0},
{"word": "you", "start_ms": 350, "dur_ms": 100, "phase_lr_deg": -2.0}
]
},
{
"segment": 43,
"transcript": "드디어 금요일입니다 예이",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "드디어", "start_ms": 70, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "금요일입니다", "start_ms": 200, "dur_ms": 220, "phase_lr_deg": 5.0},
{"word": "예이", "start_ms": 430, "dur_ms": 100, "phase_lr_deg": 5.0}
]
},
{
"segment": 44,
"transcript": "불금을 즐겨봅시다 앗싸",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "불금을", "start_ms": 45, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "즐겨봅시다", "start_ms": 195, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "앗싸", "start_ms": 405, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "Happy Friday everyone",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Happy", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": -2.0},
{"word": "Friday", "start_ms": 140, "dur_ms": 120, "phase_lr_deg": -2.0},
{"word": "everyone", "start_ms": 270, "dur_ms": 200, "phase_lr_deg": -2.0}
]
},
{
"segment": 46,
"transcript": "한 주 동안 고생했어요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "한", "start_ms": 65, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "주", "start_ms": 135, "dur_ms": 70, "phase_lr_deg": 5.0},
{"word": "동안", "start_ms": 215, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "고생했어요", "start_ms": 325, "dur_ms": 180, "phase_lr_deg": 5.0}
]
},
{
"segment": 47,
"transcript": "주말 푹 쉬세요 안녕",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "주말", "start_ms": 55, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "푹", "start_ms": 165, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "쉬세요", "start_ms": 235, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "안녕", "start_ms": 395, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "Enjoy your weekend guys",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Enjoy", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": -2.0},
{"word": "your", "start_ms": 150, "dur_ms": 60, "phase_lr_deg": -2.0},
{"word": "weekend", "start_ms": 220, "dur_ms": 140, "phase_lr_deg": -2.0},
{"word": "guys", "start_ms": 370, "dur_ms": 130, "phase_lr_deg": -2.0}
]
},
{
"segment": 49,
"transcript": "월요일에 다시 만나요 흑",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "월요일에", "start_ms": 60, "dur_ms": 160, "phase_lr_deg": 5.0},
{"word": "다시", "start_ms": 230, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "만나요", "start_ms": 340, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "흑", "start_ms": 490, "dur_ms": 50, "phase_lr_deg": 5.0}
]
},
{
"segment": 50,
"transcript": "(noise)",
"speaker": "null",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 06:11:00*
