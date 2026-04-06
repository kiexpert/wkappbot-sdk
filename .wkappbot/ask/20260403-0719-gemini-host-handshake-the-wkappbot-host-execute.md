# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=07:17:40  uptime=21506s  mem=1663MB  system_processes=446"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_071533.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 162-312-621-263-362-632-216-326-302-230-103-130-613-167-162-265-125-126-316-103-162-167-617-106-132-136-317-134-137-136-137-106-136-376-123-617-623-316-631-103-520-362-135-231-1_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 132-170-765-620-206-260-762-762-713-705-716-271-127-675-610-625-562-652-362-236-267-276-276-276-265-132-201-167-172-627-312-316-316-106-765-107-123-712-237-127-312-123-132-162-5_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 315-716-136-765-176-132-561-617-261-312-167-130-176-103-263_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 236-627-625-675-657-576-756-576-567-130-236-726-260-136-163-167-612-102-106-163-570-160-615-367-130-156-136-160-130-163-263-613-136-103-173-163-326-167-137-130-130-310-136-130-1_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 316-103-316-107-157-673-103-216-716-160-162-103-106-163-106-163-130-136-130-160-136-132-136-163-261-260-260-301-301-301-132-163-162-631-617-316-317-103-361-123-106-165-637 106-1_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 671-612-761-671-617-623-627-321-621-361-631-613-627-672-601-627-602-675-672-671-567-621-367-623-671-670-607-670-126-136-213-123 761-106-163-126-632-167-106-123-571-127-127-106-1_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 726-162-263-127-631-132-102-132-627-267-607-765-276-213-126-102-627-672-726-267-127-302-203-632-362-326-765-765-756-671-632-610-365-612-301-216-310-126-167-760-765-176-172-137-1_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 203-312-213-132-213-231-326-230-235-325-326-326-130-130-213-123-126-312-726-301-312-307-375-132-213-302-321-237-273-725-765-157-207-312-321-213-125-123-261-263-126-256-627-312-6_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 167-706-106-103-106-132-167-150-576-576-362-267 601-106-716-162-167-160-160-752-671-137-106-167-132-312-103-106-106-671-173-176-132-517-130-130-102-103-163-132-321-312-160-361-1_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 176-617-361-162-621-312-267-261-261_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 621-621-610-675-163-162-160-102-167-657-362-623-620-162-136-527-671-106-103-160-601-167-617-167-617-617-761-103-167-675-167-521-167-157-163-167-316-617-167-103-601-106-103-137-1_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 623-613-126-361-263-126-263-120-512-325-326-236-326-326-236-236-263-236-523-563-253-321-321-312-362-162-521-523-531-516-125-623-261-362-326-316-321-126-362-236-231-136-132-263-2_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 756-367-310-130-310-301-103-136-302-132-413-301-574-576-756-756-751-127-136-134-361-360-654-637-167-541-513-301-567-765-706-276-236 726-762-762-726-756-576-567-576-657-567-267-6_V.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 620-621-263-612-632-326-132-263-123-213-321_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 627-627-267-265-276-625-265-326-325-326-321-352-320-351-105-305-756-675 231-235-236-623-236-623-263-625-762-762-762-756 235-265-263-263-236-236-260-263-326 635-613-165-316-136-1_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 160-132-125-123-657-675-617-615-165-106-617-163-106-136-623-167 107-256-175-126-132-103-163-163-167-312-103-123-263-162-136-106-162-103-165-136-126-156-176-123-106-167-516-162-1_V.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 263-762-260-103-103-102-671-613-167-130-163-167-107-136-126-162-127-126-167-130-162-163-130-562-165-716-625-310-136-621-621-172-127-167-163-103-167-103-136-136-671-167-167-176-1_V.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 321 103-163-157-167-176-160-106-762-136-502-261-175-127-163-130-517-167-175-103-162-106-136-613-103-106-167-176-162-670-132-162-123-106-135-216-163-163-132-160-165-751-601-160-1_V.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 267-261-627-623-627-627-675-675-672-672-632-263-623-263-654-627-627-627-267-261-321-321-571-576-675-675-713-170-173-176-236-216-623-672-673-617-671-671-621-673-672-617-213-231-3_W.mp3)
  Segment 20: 29944ms ~ 32248ms (file: 762-765-675-675-756-756-675-163-321-326-503-312-130-136-615-136-106-130-716-756-765-675-132-123-231-623-623-263-176-623-362-623-216-261-261-621-236-261-267-263-263-261-321-326 1_W.mp3)
  Segment 21: 33248ms ~ 33824ms (file: 357-312-237-201-162-302-176-721-302-371-267-726-132-132-623-632-670-267-136-517-107-627-125-321-620-315-315-103-130-132-312-201-132 576-752-756-756-756-756-567-154-315-132-123-1_W.mp3)
  Segment 22: 34824ms ~ 35400ms (file: 107-136-136-163-162-165-165-601-126-261-706-103-106-132-301-316-160-103-106-631-162-162-261-206-621-106-301-160-316-350-126-150-103-136-103-130-103-130-126-106-126-103-163-130-1_V.mp3)
  Segment 23: 36400ms ~ 36976ms (file: 310-163-601-136-103-103-135-627-132-631-612-621-627-261-260-261-621-261-267-267-267-267-261-312-321-362-572-165-163-764-675-675-607-607-672-206-102-301-103-312-623-627-216-276-6_V.mp3)
  Segment 24: 37976ms ~ 38552ms (file: 265-376-507-165-106-165-260-561-156-521-261-260-621-321-231-201-137-371-103-136-156-130-715-103-301-310-602-623-103-106-106-362-130-136-162-136-267-263 157-136-130-132-316-263-1_W.mp3)
  Segment 25: 39552ms ~ 40128ms (file: 103-637-103-162-617-123-126-103-125-715-213-156-675-167-163-130-136-106-163-216-613-162-126-716-136-130-103-107-617-132-163-635-123-150 547-517-571-156_V.mp3)
  Segment 26: 41128ms ~ 41704ms (file: 756-756-762-561-756-765-156-756-650-576-756-765-132-756-756-756-705-301 756-762-762-623-632-261-263-216-754-765-763-756-765-607-627-602-637-675-675-675-760-321-327-507-756-765-6_W.mp3)
  Segment 27: 42704ms ~ 43712ms (file: 612-321-163-362-312-631-613-671-236-657-127-357-132-163-625-261-261-731-627-132-623-627-621-163-126-625-652-625-576-576-631-130-321-213-276 172 176-167-127-765-127-716-172-175-7_W.mp3)
  Segment 28: 44712ms ~ 45288ms (file: 123-267-627-267-267-263-236-261-216-213-312-236-312-123-231-326-213-752-751-265-217-265-326-132-215-231-130-106-106-105-105-510-517-574-537-520-216-210-310-130-102 130-316-316-1_W.mp3)
  Segment 29: 46288ms ~ 46864ms (file: 126-103-612-671-617-612-567-657-631-563-312-216-126-167-517-517-576-261-163-103-132-261-621-236-132-312-135-103-103-167-617-617-672-607-607-602-607-670-760-670-176-106-106-103-1_W.mp3)
  Segment 30: 47864ms ~ 48440ms (file: 102-562-127-613-103-162-160-603-167-123-651-102-126-132-167-106-132-631-136-162-653-102-132-163-136-167-165-172-106-102-612-312-165-103-312-105-167-312-631-103-106-261-136 672-2_V.mp3)
  Segment 31: 49440ms ~ 50016ms (file: 167-163-132-136-132-137-312-132-102-132-134-132-162-621-627-627-621-263-316-632-607-206-612-321-375-503-607-367-321-132-312-132-312-312-526-567-756-176-173-716-176-761-763-176-1_V.mp3)
  Segment 32: 51016ms ~ 51592ms (file: 321-726-276-207-736-726-327-236-617-170-162-576-275-267-267-627-267-267-567-567-605-603-301-216-621-261-726-672-627-672-761-671-670-760-607-672-764-756-762-762-675-672-672-672-7_W.mp3)
  Segment 33: 52592ms ~ 53168ms (file: 132-752-735-103-136-132-130-103-613-123-612-612-162-126-213-213-261-621-621-621-621-612-621 165 156-157-123-623-612-623-627-627-601-617-267-675-675-576-675-236_W.mp3)
  Segment 34: 54168ms ~ 54744ms (file: 102-162-637-106-236-137-706-762-763-763-172-103-716-327-326-302-301-531-130-132-310-130-310-103-513-253-302-132-102-163-160-137-610-260-263-762-672-260-267-231-230-312-320-103-3_V.mp3)
  Segment 35: 55744ms ~ 56320ms (file: 275-276-276-267-267-326-231-263-261-216-312-216-231-263-236-261-216-216-231-216-321-531-263-132-312-652-635-365-615-163-153-213-312-231-217-237-267-267-216-263-326-216-126-213-6_W.mp3)
  Segment 36: 57320ms ~ 57896ms (file: 267-267-623-672-672 675-763 673-675-675-762-657-615 675-675-657-103-103-162-623-376-672 765-675-672-671-675-671-672-623-671-216-263-612-612-216-567-673 627-627-675-673-672-567-5_W.mp3)
  Segment 37: 58896ms ~ 59472ms (file: 130-213-625-627-671-627-627-132-132-134-143-137-137-765-672-672-672-130-310-312-132-126-276-261-231-135-134-143-135-153-103 124-613-163-750-176-761-167-136-136-136-637-163-107-1_V.mp3)
  Segment 38: 60472ms ~ 61048ms (file: 672-671-671-627-627-672-671-163-560-650-165-150-610-162-162-162-172-261-123-263 134-137-201-120-102-231-120-102-163-216-132-102-312 325-352_W.mp3)
  Segment 39: 62048ms ~ 62624ms (file: 126-103 157-517-136-263-130-162-162-236-562-216-765-130-613-132-613-312-130-103-132-764 761-162-162-657-627-521-571-571-576-572-572-750-201-210_V.mp3)
  Segment 40: 63624ms ~ 64200ms (file: 675-675-765-765-567-547-547-570-574-173-136-632-236 671-613-610-621-670-671-627-267-627_W.mp3)
  Segment 41: 65200ms ~ 65776ms (file: 257-327-372-321-576-576-350-627-236-706-167-102-137-123-321-123-132-163-621-267-265-203-236-236-560-132-132-510-623-216-236-256-567-257-520-572-206-172-375-713-312-136-162-124-1_W.mp3)
  Segment 42: 66776ms ~ 67352ms (file: 167-617-612-760-765-167-617-132-176-106-167-312-615-265-165-761-326-164-106-167-106-163-170-705-106-176-167-167-267-236-627-162-167-236-210-602-620-623-162-261-210-721-126-137-2_V.mp3)
  Segment 43: 68352ms ~ 68928ms (file: 312-317-657-607-670-670-675-673-360-261-362-631-361-612-637-670-671-167-362-610-706-176 132-162-623-631-312-136-135-136-136-135-316-314-372-312-312-216-132 132-134-102-326-316-1_V.mp3)
  Segment 44: 69928ms ~ 70504ms (file: 765-576-576-765-567-765-517-250-652-657-657-675-652-156-625-105-265-152-572-257-123-127-235-256-213-251-317-261 326-621-267-762-675-762-670-670-652-675-765-756-675-657-576-657-6_W.mp3)
  Segment 45: 71504ms ~ 72080ms (file: 675-671-761-617-617-167-162-651-301-175-317-135-136-617-621-136_V.mp3)
  Segment 46: 73080ms ~ 73656ms (file: 216-132 126-216 627-267 632-632-317-361 671-675-675-671-675-765-765-106-103-167-106-527-231-617-162 132-136-106-160-165-216-712 160-106-576-675-167-106-106-136-312-167-162-103-3_W.mp3)
  Segment 47: 74656ms ~ 75232ms (file: 125-103-136-160-671-563-126-165-623-103-163-136-106-361-167-671-561-126 103-136-163-263-156-106-651-617-615-176-126-160-132-710-167-167-675-165-162_V.mp3)
  Segment 48: 76232ms ~ 76808ms (file: 675-637-675-761-576-756-176-621-213-756-106-671-617-657-627-657-137-310-547-573-627-657-623-163-126-106-765-103-170-106-716-651-516-103-765-317-671-761-106-167-170-106-617-160-1_W.mp3)
  Segment 49: 77808ms ~ 78384ms (file: 576-527-326-312 136-213-765-765-627-216_W.mp3)
  Segment 50: 79384ms ~ 79960ms (file: 576-526-526-652-523-235-123-120-102-231-263-236-216-260-261-261-231-231-263-267-625-362-236-362-361-326-236-236-263-261-236-543-574-527-256-263-261-263-261-543-543-547-542-204-2_W.mp3)
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
> **Time**: 2026-04-03 07:19:07
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "책 읽는 게 너무 즐거워요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "책", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "읽는", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 270, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 330, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "즐거워요", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "오늘 점심 메뉴는 비빔밥 어때요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 35, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "점심", "start_ms": 125, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "메뉴는", "start_ms": 225, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "비빔밥", "start_ms": 335, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "어때요", "start_ms": 465, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "창밖을 보니까 비가 오네요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "창밖을", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "보니까", "start_ms": 190, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "비가", "start_ms": 310, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "오네요", "start_ms": 410, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "주말에는 가족들과 여행을 가고 싶어요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "주말에는", "start_ms": 25, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "가족들과", "start_ms": 155, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "여행을", "start_ms": 305, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "가고", "start_ms": 415, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "싶어요", "start_ms": 485, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "새로운 취미를 하나 만들었어요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "새로운", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "취미를", "start_ms": 155, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "하나", "start_ms": 275, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "만들었어요", "start_ms": 355, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "매일 아침 운동하는 게 힘들지만 뿌듯해요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "매일", "start_ms": 30, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "아침", "start_ms": 100, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "운동하는", "start_ms": 180, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 310, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "힘들지만", "start_ms": 360, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "뿌듯해요", "start_ms": 480, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "친구랑 맛있는 커피 마시러 가요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "친구랑", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "맛있는", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "커피", "start_ms": 270, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "마시러", "start_ms": 350, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "가요", "start_ms": 460, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "이 노래 가사가 정말 마음에 들어요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 35, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "노래", "start_ms": 75, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "가사가", "start_ms": 155, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 265, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "마음에", "start_ms": 345, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "들어요", "start_ms": 445, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "외국어 공부는 꾸준히 하는 게 중요하죠",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "외국어", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "공부는", "start_ms": 160, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "꾸준히", "start_ms": 260, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 360, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 430, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "중요하죠", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "오늘도 수고 많으셨습니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "오늘도", "start_ms": 60, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "수고", "start_ms": 190, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "많으셨습니다", "start_ms": 300, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "맛있는 디저트 먹으니까 행복해요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "맛있는", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "디저트", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "먹으니까", "start_ms": 270, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "행복해요", "start_ms": 410, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "요즘 유행하는 옷 스타일이 뭐예요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "요즘", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "유행하는", "start_ms": 110, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "옷", "start_ms": 240, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "스타일이", "start_ms": 300, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "뭐예요", "start_ms": 420, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "꽃향기가 정말 향긋하고 좋네요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "꽃향기가", "start_ms": 45, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 195, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "향긋하고", "start_ms": 295, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 425, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "내일은 날씨가 맑았으면 좋겠어요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "내일은", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 160, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "맑았으면", "start_ms": 280, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "좋겠어요", "start_ms": 410, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "건강을 위해 비타민을 챙겨 먹어요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "건강을", "start_ms": 35, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "위해", "start_ms": 135, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "비타민을", "start_ms": 215, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "챙겨", "start_ms": 335, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "먹어요", "start_ms": 425, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "심심할 때는 영화를 한 편 봐요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "심심할", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "때는", "start_ms": 160, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "영화를", "start_ms": 250, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 360, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "편", "start_ms": 400, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "봐요", "start_ms": 450, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "부모님께 안부 전화를 드렸어요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "부모님께", "start_ms": 25, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "안부", "start_ms": 165, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "전화를", "start_ms": 255, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "드렸어요", "start_ms": 375, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "강아지랑 산책하는 시간이 즐거워요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "강아지랑", "start_ms": 50, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "산책하는", "start_ms": 180, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "시간이", "start_ms": 300, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "즐거워요", "start_ms": 410, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "여행 가방을 싸고 있는데 설레네요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "여행", "start_ms": 35, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "가방을", "start_ms": 105, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "싸고", "start_ms": 205, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "있는데", "start_ms": 285, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "설레네요", "start_ms": 395, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "여름 휴가 계획을 세우는 중이에요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "여름", "start_ms": 45, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "휴가", "start_ms": 135, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "계획을", "start_ms": 215, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "세우는", "start_ms": 325, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "중이에요", "start_ms": 445, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "컴퓨터 게임 하는 걸 좋아해요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "컴퓨터", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "게임", "start_ms": 150, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 230, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "걸", "start_ms": 300, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "좋아해요", "start_ms": 350, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "오늘 저녁은 간단하게 먹으려고요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 30, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "저녁은", "start_ms": 120, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "간단하게", "start_ms": 230, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "먹으려고요", "start_ms": 360, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "자기 전에 일기를 쓰는 습관이 있어요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "자기", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "전에", "start_ms": 130, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "일기를", "start_ms": 220, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "쓰는", "start_ms": 330, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "습관이", "start_ms": 400, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 500, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "요리하는 걸 좋아해서 자주 해요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "요리하는", "start_ms": 40, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "걸", "start_ms": 170, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "좋아해서", "start_ms": 220, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "자주", "start_ms": 360, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "해요", "start_ms": 450, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "방 정리를 하니까 마음이 편해지네요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "방", "start_ms": 35, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "정리를", "start_ms": 95, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "하니까", "start_ms": 205, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "마음이", "start_ms": 305, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "편해지네요", "start_ms": 415, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "오랜만에 친구랑 수다 떨고 왔어요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "오랜만에", "start_ms": 45, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "친구랑", "start_ms": 175, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "수다", "start_ms": 285, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "떨고", "start_ms": 365, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "왔어요", "start_ms": 435, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "좋아하는 가수의 콘서트에 가고 싶네요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "좋아하는", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "가수의", "start_ms": 170, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "콘서트에", "start_ms": 270, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "가고", "start_ms": 400, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "싶네요", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "매일 물을 2리터씩 마시려고 노력해요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "매일", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "물을", "start_ms": 120, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "2리터씩", "start_ms": 210, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "마시려고", "start_ms": 360, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "노력해요", "start_ms": 490, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "오늘 하늘이 정말 파랗고 예쁘네요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 30, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "하늘이", "start_ms": 120, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 230, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "파랗고", "start_ms": 310, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "예쁘네요", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "운동화를 새로 샀는데 아주 편해요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "운동화를", "start_ms": 50, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "새로", "start_ms": 180, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "샀는데", "start_ms": 270, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 380, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "편해요", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "피곤할 때는 낮잠을 잠깐 자는 게 좋죠",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "피곤할", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "때는", "start_ms": 150, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "낮잠을", "start_ms": 240, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "잠깐", "start_ms": 340, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "자는", "start_ms": 420, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 480, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "좋죠", "start_ms": 520, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "가족들과 맛있는 저녁 먹으러 가요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "가족들과", "start_ms": 35, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "맛있는", "start_ms": 165, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "저녁", "start_ms": 275, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "먹으러", "start_ms": 355, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "가요", "start_ms": 465, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "오늘 하루도 알차게 보내서 기뻐요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 45, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "하루도", "start_ms": 135, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "알차게", "start_ms": 245, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "보내서", "start_ms": 355, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "기뻐요", "start_ms": 465, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "식물을 키우는 게 힐링이 되더라고요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "식물을", "start_ms": 30, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "키우는", "start_ms": 140, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 250, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "힐링이", "start_ms": 290, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "되더라고요", "start_ms": 400, "dur_ms": 170, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "새로운 앱을 다운로드 받았는데 좋아요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "새로운", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "앱을", "start_ms": 150, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "다운로드", "start_ms": 230, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "받았는데", "start_ms": 370, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "좋아요", "start_ms": 500, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "요즘은 건강한 음식을 먹으려고 해요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "요즘은", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "건강한", "start_ms": 140, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "음식을", "start_ms": 250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "먹으려고", "start_ms": 350, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "해요", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "비 오는 날에는 집에서 쉬는 게 최고예요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "비", "start_ms": 25, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "오는", "start_ms": 75, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "날에는", "start_ms": 155, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "집에서", "start_ms": 265, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "쉬는", "start_ms": 365, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 445, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "최고예요", "start_ms": 485, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "자전거 타고 공원 한 바퀴 돌았어요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "자전거", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "타고", "start_ms": 160, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "공원", "start_ms": 250, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 340, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "바퀴", "start_ms": 380, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "돌았어요", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "아침 일찍 일어나니까 상쾌하네요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "아침", "start_ms": 35, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "일찍", "start_ms": 125, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "일어나니까", "start_ms": 215, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "상쾌하네요", "start_ms": 365, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "좋아하는 드라마가 오늘 종영해요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "좋아하는", "start_ms": 45, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "드라마가", "start_ms": 165, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 285, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "종영해요", "start_ms": 375, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "오랜만에 책을 읽으니까 집중이 잘 돼요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "오랜만에", "start_ms": 40, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "책을", "start_ms": 170, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "읽으니까", "start_ms": 250, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "집중이", "start_ms": 360, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 470, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "돼요", "start_ms": 510, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "오늘 하루는 정말 바쁘게 지나갔어요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 30, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "하루는", "start_ms": 120, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 230, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "바쁘게", "start_ms": 310, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "지나갔어요", "start_ms": 410, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "밤공기가 시원해서 창문을 열어뒀어요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "밤공기가", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "시원해서", "start_ms": 190, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "창문을", "start_ms": 300, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "열어뒀어요", "start_ms": 410, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "다양한 경험을 해보는 게 중요하다고 생각해요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "다양한", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "경험을", "start_ms": 150, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "해보는", "start_ms": 260, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 360, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "중요하다고", "start_ms": 400, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "생각해요", "start_ms": 530, "dur_ms": 40, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "꿈을 이루기 위해 노력 중이에요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "꿈을", "start_ms": 25, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "이루기", "start_ms": 105, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "위해", "start_ms": 215, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "노력", "start_ms": 295, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "중이에요", "start_ms": 405, "dur_ms": 170, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "친구 생일 선물을 골랐는데 마음에 들어 했으면 좋겠어요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "친구", "start_ms": 35, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "생일", "start_ms": 105, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "선물을", "start_ms": 185, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "골랐는데", "start_ms": 295, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "마음에", "start_ms": 425, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "들어", "start_ms": 525, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "새로 나온 영화 예고편을 봤는데 흥미롭네요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "새로", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "나온", "start_ms": 130, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "영화", "start_ms": 200, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "예고편을", "start_ms": 290, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "봤는데", "start_ms": 420, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "흥미롭네요", "start_ms": 520, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "주말에 근처 산에 등산 가려고 계획 중이에요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "주말에", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "근처", "start_ms": 140, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "산에", "start_ms": 230, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "등산", "start_ms": 310, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "가려고", "start_ms": 400, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "계획", "start_ms": 500, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "항상 건강하시고 행복하세요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "항상", "start_ms": 30, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "건강하시고", "start_ms": 120, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "행복하세요", "start_ms": 280, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "감사합니다 다음에 또 만나요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "다음에", "start_ms": 250, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 360, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 07:19:07*
