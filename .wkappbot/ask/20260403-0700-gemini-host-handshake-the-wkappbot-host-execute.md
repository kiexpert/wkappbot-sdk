# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=06:59:34  uptime=20419s  mem=1690MB  system_processes=448"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_065925.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 632-106-316-106-136-752-671-215-612-762-725-216-261-231-132-301-236-312-261-602-126-265-652-672-762-675-165-560-610-160-106-103-571-152-306-601-162-136-750-750-172-102-103-132-1_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 316-612-136-160-106-136-576-612-612-162-163-103-136-126-312-162-167-631-716-612-160-631-637-612-316-163-617-136-706-162-106-176-237-162-162-312-102-263-621-230-621-216-163-102-7_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 625-312-361-173-321-213-623-326-273-632-602-657-657_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 136-156-163-516-256-652-615-165-216-126-216-126-132-165-126-517-571-521-312-321-361-317-617-621-165-625-652-652-256-675-625-265-652-526-231-237-263-236-263-362-362-632-325-325-3_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 675-675-675 175-106-514-130-671-132-103-103-160-106-103-316-103-752-261-267-216-312-261-621-261-120-120-130-236-231-321-236-236-270-267-267-265-236-326-320-320-327-326-236-236-2_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 617-316-526-312-136-130-617-130-273-562-132 601-103-162 126-126-367-231-617-610-756 126-621-672-176-160-657-160-136-261-163-756-175-267-672-261-765-673-160-526-315-160-163-136-1_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 136-132-763-106-106-106-610-326-612-627-316-316-563-526-176-362-735-103-103-106-160-165-103-176-602-103-102-160-167-167-163-527-105-675-623-163-106-670-657-617-130-362-130-132-1_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 516-572-267-261-106-106-137-167-617-163-103-106-173-301-310-310-310-372-637-362-623-361-631-321-362-136-362-126-621-260-216-103-321-312-176-156-756-651-673-672-673-671-671-632-6_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 567-516-357-356-351-156-356-752-563-156-516-351-135-137-351-357-576-615-576-563-576-536-516-567-561-567-156-165-567-657-567-576-375-567-576-657-367-635-657-567-567-675-576-375-5_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 675-567-675-267-625-725-652-517-261-623-632-621-612-653-752-657-657-527-562-325-276-635-657-657-357-672-652-657-625-216-263-576-576-657-672-657-675-657-567-576-756-675-165-567-5_V.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 236-256-524-526-256-265-625-526-326-321-326-321-321-523-537-567-315-135-136-573-352-576-261-263-213-231-210-261-265-256-256-572-317-136-160-125-160-514-536-657-536-576-163-621-2_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 725-675-762-276-765-265-265-256-562-265-571-610-160-261-612-612-160-621-612-601-612-601-612-610-610-316-132-156-657-672-631-361-126-162-103-261-236-126-123-574-576 617 627-216-1_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 265-672-623-256-175-260-326-672-260-103-163-162-216-127-236-261-265-621-263-672-623-627-263-621 175-163-160-162-103-132-105-103 103-137 167-637_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 103-136-136-132-267-302-732-203-203-201-213-261-103-132-103-103-103-130-312-132-312-621-102-613-326-312-310-206-261-263-623-670-621-361-120-230-276-312-103-123-126-612-627-612-3_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 672-627-261-763-657-657-265-762-672-627-632-567-256-263-576-567-126-261-130-162-326-126-312-136-132-672-765-705-631-625-650-562-105-126-157-135-136-623-673-617-236-316-162-102-6_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 103-136-547-162-567-103-136-136-106-130-756-167-126-310-163-136-516-576-632-367-657-651-501-507-135-157-130-103-657-625-157-651-615-126-567-162-651 162-136-106-613-160-103-130-1_V.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 231-521-512-215-261-126-231-236-256-526-625-632-321-231-213-236-321-321-236-261-263-261-267-261-261-526-567-265-236-231-236-321-231-321-326-132-135-531-531-635-623-362-362-362-3_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 130-216-602-620-261-627-260-765-675-657-310-316-716-106-106-360-536-536-137-301-132-126-123-162-310-130-103-310-130-310-130-103-107-137-137-756-610-176-136-126-210-106-102-301-3_V.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 126-321-213-230-321-361-306-632-163-103-106-162-627-163-106-361-623-265-627-726-217-326-216-361-362-136-613-125-675-160-621-615-167-562-130-732-123-312-137-135-312-123-156-613-6_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 620-623-316-310-321-321-321-103-162-312-103-136-132-162-103-126-210-130-165-651-576-165-612-367-160-310-103-132-103-316-312-130-132-621-231-103-627-623-106-163-716-361-103-102-1_V.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 574-576-567-657-653-657-605-620-617-621-621-126 547-576-576-156-571_W.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 310-103-135-136-103-312-612-672-621-621-207-267-602-607-261-103-126-126-132-103-126-136-326-261 132-132-621-750-574_V.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 756-756-756-672-765-675-765-765-136-176 167 136-136_W.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 103-675-675-675-675-627-627 612-162-610-167-316 632-361-671-627-621-612-627-632-627-632-672-627-621-627-672-627-672-260-627-627-613-623-126-657-621-672-267-627-672-623-627-612-3_W.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 613-130-162-103-105-301-302-312-310-301-302-231-302-312-132-123-150-125-123-132-206-206-263-260-712-310-106-765-762-276-206-260-267-260-526-520-326-610-632-206-203-206-206-263-6_W.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 517-163-165-162-301-175-310-301-320-321-301-132-750-375-307-123-103-574-157-136-163-136-167-136-167-163-103-163-167-162-132-176-312-265-102-206-203-162-126-612-162-126-136-103-1_V.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 163-174-172-176-107-316-301-307-312-320-302-301-312-302-321-320-320 754-765-103-312-310-307-312-302-203-203-275-527-572-572-205-726-276-270-620-203-162-760-637-362-312-537-530-6_W.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 167-567-163-103-316-301-316-136-130-610-312-103-162-160-163-102-106-736-615-153-521-136-136-103-136-137-126-761-621-163-162-134-132-106-165-107-615-675-617-103-617-623-236-106-1_V.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 326-316-251-516-352-265-625-236-621-261-216-261-625-265-621-615-632-632-132-213-236-215-263-263-261-206-574-506-526-132-123-123-126-216-312-312-132-513-541-351-321-236-312-136-5_W.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 312-310-312-301-215-123-312-206-102-162-164-106-162-102-123-102 625-265-236-263-547-263-632-325-162-310-160-103-103-163-103-163-160-312-106-231-210-263-236-263-236-132-621-627-6_V.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 132-157-372-132-312-362-576-675-317-312-316-213-103-301-136-123-136-231-201-123-213-126-310-301-310-302-236-301-357-321-301-301-103-132-103-321-132-127-132-310-132-501-612-602-2_W.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 652-316-162-657-630-230-670-175-167-761-263-263-542-564-236-540-256-235-236-253-621-624-621-261-263-756 257-275-253-257-250-254-240-201-173-713-701-547-657-372-326-317-316-316-3_W.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 261-162-126-617-671-627-162-617-675-627-165-756-671-176-765-132-361-672-167-162-167-715-176-617-172-765-162-613-761-760-167-162-376-713-136-576-263-216-613-163-362-263-621-162-1_W.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 367-562-562-653-365-657-567-675-657-657-756-756-756-675-675-765-762-673-627-675 562-576-576-675-267-267-627-623-162-756-321-302-213-213-326-326-310-673-762-362-632-627-617-306-1_W.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 103-132-136-732-752-576-675-316-267-312-132-756-725-310-213-312-301-316-605-507-756-756-316-231-675-670-621 160-160-130-612-621-623-623-623-623 361-276-672-623-625-312-136-316-2_W.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 602-625-126-123-102-123-132-103-310-236-320-765-675-670-601-607-627-602-760-320-302-320-320-320-320-360-136-260-263-263-320-723-752-206-207-276-162-201-132-312-230-236-305-567-5_W.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 317-310-310-136-163-130-301-103-103-107-615-126-132-267-321-546-547-762-761_V.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 263-726-726-627-675-613-607-160-136-216-627-263-261-261-362-651-123-216-152-627-627-615-162-613-167-613-612-236-361-236-631-617-617-651-615-675-675-752-765 675-765-670-361-261-2_W.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 123-326-301-102-132-123-321-302-321-301-132-134-132-706-716-671-612-510-105-103-136-160-103-106-103-157-751-750-753-576-756-657-176-316-236-236-320-301-312-316-612-312-301-260-2_V.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 620-625-157-751-156-571 163-263-623-621-675-671 574-574-753-756 716-765-756-756-136-675-163-103-312-130-653-316-617-106-621-612-630-602-627-362-576-756-136-106-671-716-301-167-1_W.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 103-162-106-216-236-210-120-102-126-163-167-176-106-605-675-276-215-206-162-750-760-706-673-627-627-267-173-172-260-726-276-267-267-267 612-612-631-167-675-167-162-657-625-762-1_V.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 650-125-561-301-310-130-231-132-675-675-576-210-203-576-752-312-310-317-132-567-657-306-360-136-316-631-213-613-167-263-267-213-136-326-123-132-132-132-315-103-103-136-106-275-1_V.mp3)
  Segment 43: 66192ms ~ 66768ms (file: 317-310-102-103-123-326-326-326-231-236-263-263-620-206 263-236-236-203-326-203-260-620 652-236-260-263 602-620-260_V.mp3)
  Segment 44: 67768ms ~ 68344ms (file: 106-167-321-160-715-765-175-106-136-106-507-617-316-516-546-567-576-576-564-605-762-576-765-765-762-310-601-137-361-367-173-136-237-326-103-675-175-716-103-132-621-263-162-163-1_V.mp3)
  Segment 45: 69344ms ~ 69920ms (file: 627-672-623-136-312-321-132-673-627-236-231-136-136-132-316-136-136-136-132-316-627-621-617-671-267-236-231-312-236-321-132-316-361-316-132-312-316-361-316-136-361-256-275-621-6_W.mp3)
  Segment 46: 70920ms ~ 71496ms (file: 230-216-625-627-632-567-617-627-621-132-675-567-562-627-631-261-621-651-567-657-567-160-316-621-267-261-260-320-657-657-162-136-621-627-621-361-632-361-610-615-653-612-623-627-1_W.mp3)
  Segment 47: 72496ms ~ 73072ms (file: 765-567-765-617-675-675-675-567-567-657-675-567-657-657-657-657-567-657-635-356-653-652-567-657-561-625-615-675-315-625-512-256-531-257-563-576-573-537-356-561-567-657-567-567-5_W.mp3)
  Segment 48: 74072ms ~ 74648ms (file: 627-652-672-617-637-572-672-106-106-150-160-617-601-617-160-657-652-632-603-362-603-613-361-617-106-106-150-201-260-620-267-675-675-675-675-237-265-625-263-263-765-267-320-310-1_V.mp3)
  Segment 49: 75648ms ~ 76224ms (file: 316-631-136-103-154-162-103-231-210-123_V.mp3)
  Segment 50: 77224ms ~ 77800ms (file: 617-617-361-167-612-617-671-361-163-623-162-361-613-713-162-162-162-627-162-216-612-162 617-321 671-716-176_W.mp3)
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
> **Time**: 2026-04-03 07:00:48
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "오늘 점심은 뭐 먹을까요?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "점심은", "start_ms": 160, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 310, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "먹을까요?", "start_ms": 380, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "근처에 새로 생긴 파스타집 어때요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "근처에", "start_ms": 50, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "새로", "start_ms": 190, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "생긴", "start_ms": 310, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "파스타집", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "어때요?", "start_ms": 580, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "거기 맛있다고 들었어요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "거기", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "맛있다고", "start_ms": 165, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "들었어요", "start_ms": 355, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "그럼 거기로 가보죠",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "거기로", "start_ms": 155, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "가보죠", "start_ms": 295, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "예약이 필요할까요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "예약이", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "필요", "start_ms": 200, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "할까요?", "start_ms": 320, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "지금 전화해 볼게요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "지금", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "전화해", "start_ms": 180, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "볼게요", "start_ms": 370, "dur_ms": 150, "phase_lr_deg": 0.0}
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
"transcript": "다행히 자리가 있대요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "다행히", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "자리가", "start_ms": 190, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "있대요", "start_ms": 330, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "그럼 지금 바로 출발해요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 35, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 155, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 265, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "출발해요", "start_ms": 385, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "네, 차 키 챙겨 올게요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 45, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "차", "start_ms": 135, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "키", "start_ms": 205, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "챙겨", "start_ms": 275, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "올게요", "start_ms": 405, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "가는 길에 편의점 좀 들를 수 있을까요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "가는", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "길에", "start_ms": 160, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "편의점", "start_ms": 280, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 440, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "들를", "start_ms": 520, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 610, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "있을까요?", "start_ms": 660, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "네, 필요한 거 있으세요?",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "필요한", "start_ms": 130, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 280, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "있으세요?", "start_ms": 350, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "생수 한 병 사야 해서요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "생수", "start_ms": 45, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 185, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "병", "start_ms": 265, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "사야", "start_ms": 355, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "해서요", "start_ms": 475, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "알겠어요. 저 앞 사거리에 있어요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "알겠어요.", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "저", "start_ms": 210, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "앞", "start_ms": 280, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "사거리에", "start_ms": 360, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 530, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 16,
"transcript": "식당 인테리어가 정말 깔끔하네요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "식당", "start_ms": 40, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "인테리어가", "start_ms": 180, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 390, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "깔끔하네요", "start_ms": 510, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "분위기가 좋아서 데이트 코스로도 인기 많대요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "분위기가", "start_ms": 55, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "좋아서", "start_ms": 225, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "데이트", "start_ms": 345, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "코스로도", "start_ms": 475, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "인기", "start_ms": 625, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "많대요", "start_ms": 725, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "메뉴판 좀 볼까요?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "메뉴판", "start_ms": 40, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 210, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "볼까요?", "start_ms": 290, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "저는 해산물 크림 파스타 먹을래요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "해산물", "start_ms": 155, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "크림", "start_ms": 305, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "파스타", "start_ms": 425, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "먹을래요", "start_ms": 565, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "그럼 저는 토마토 미트 소스로 할게요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "저는", "start_ms": 160, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "토마토", "start_ms": 270, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "미트", "start_ms": 410, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "소스로", "start_ms": 520, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "할게요", "start_ms": 650, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 22,
"transcript": "음식 나왔습니다. 맛있게 드세요",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "음식", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "나왔습니다.", "start_ms": 170, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "맛있게", "start_ms": 380, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "드세요", "start_ms": 530, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "우와, 정말 맛있어 보여요!",
"speaker": "speaker_2",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "우와,", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 165, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "맛있어", "start_ms": 285, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "보여요!", "start_ms": 435, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "소스가 진하고 해산물도 싱싱하네요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "소스가", "start_ms": 45, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "진하고", "start_ms": 195, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "해산물도", "start_ms": 335, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "싱싱하네요", "start_ms": 495, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "여기 오길 정말 잘한 것 같아요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "여기", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "오길", "start_ms": 150, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 270, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "잘한", "start_ms": 380, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 500, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 560, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "맞아요, 다음에 또 오고 싶을 정도예요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "맞아요,", "start_ms": 50, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "다음에", "start_ms": 190, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 340, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "오고", "start_ms": 410, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "싶을", "start_ms": 500, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "정도예요", "start_ms": 590, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 28,
"transcript": "배가 정말 부르네요. 기분 좋은 점심이었어요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "배가", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 150, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "부르네요.", "start_ms": 270, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "기분", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 580, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "점심", "start_ms": 680, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "이었어요", "start_ms": 800, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "계산은 제가 할게요",
"speaker": "speaker_1",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "계산은", "start_ms": 35, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 225, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "할게요", "start_ms": 335, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "아니에요, 오늘은 제가 사기로 했잖아요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "아니에요,", "start_ms": 45, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "오늘은", "start_ms": 205, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 345, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "사기로", "start_ms": 445, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "했잖아요", "start_ms": 565, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "아, 그랬나요? 그럼 다음엔 제가 꼭 낼게요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "아,", "start_ms": 40, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "그랬나요?", "start_ms": 100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "그럼", "start_ms": 260, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "다음엔", "start_ms": 370, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 510, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "꼭", "start_ms": 620, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "낼게요", "start_ms": 690, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "약속 잊지 마세요!",
"speaker": "speaker_2",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "약속", "start_ms": 35, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "잊지", "start_ms": 225, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "마세요!", "start_ms": 355, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "이제 다시 사무실로 가야겠네요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 150, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "사무실로", "start_ms": 270, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "가야", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "겠네요", "start_ms": 580, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "오후 업무도 힘내세요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "오후", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "업무도", "start_ms": 155, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "힘내세요", "start_ms": 295, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "민희 씨도요. 연락할게요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "민희", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "씨도요.", "start_ms": 170, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "연락", "start_ms": 320, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "할게요", "start_ms": 450, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "조심히 가세요",
"speaker": "speaker_2",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "조심히", "start_ms": 35, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 255, "dur_ms": 280, "phase_lr_deg": 0.0}
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
"transcript": "주말엔 보통 뭐 하고 보내세요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "주말엔", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "보통", "start_ms": 240, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 370, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "하고", "start_ms": 450, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "보내세요?", "start_ms": 560, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "밀린 잠을 자거나 넷플릭스를 봐요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "밀린", "start_ms": 60, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "잠을", "start_ms": 180, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "자거나", "start_ms": 310, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "넷플릭스를", "start_ms": 470, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "봐요", "start_ms": 760, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "추천할 만한 시리즈 있어요?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "추천할", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "만한", "start_ms": 230, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "시리즈", "start_ms": 360, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "있어요?", "start_ms": 530, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "요즘 새로 나온 스릴러물이 재밌더라고요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "요즘", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "새로", "start_ms": 180, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "나온", "start_ms": 300, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "스릴러물이", "start_ms": 410, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "재밌더라고요", "start_ms": 660, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "저도 스릴러 좋아하는데 꼭 볼게요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "저도", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "스릴러", "start_ms": 165, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "좋아하는데", "start_ms": 315, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "꼭", "start_ms": 545, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "볼게요", "start_ms": 635, "dur_ms": 130, "phase_lr_deg": 0.0}
]
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
"transcript": "주말에 약속 없으시면 같이 운동하실래요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "주말에", "start_ms": 55, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "약속", "start_ms": 225, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "없으시면", "start_ms": 355, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 545, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "운동", "start_ms": 665, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "하실래요?", "start_ms": 775, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "등산이나 자전거 타는 거 어떠세요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "등산이나", "start_ms": 40, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "자전거", "start_ms": 270, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "타는", "start_ms": 430, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 550, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "어떠세요?", "start_ms": 620, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "등산 좋죠! 공기도 맑고 상쾌할 것 같아요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "등산", "start_ms": 50, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "좋죠!", "start_ms": 190, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "공기도", "start_ms": 350, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "맑고", "start_ms": 520, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "상쾌할", "start_ms": 650, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 810, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 870, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "그럼 일요일 오전에 갈까요?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "일요일", "start_ms": 160, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "오전에", "start_ms": 330, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "갈까요?", "start_ms": 490, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "네, 아침 9시에 입구에서 봐요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 45, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "아침", "start_ms": 135, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "9시에", "start_ms": 265, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "입구에서", "start_ms": 415, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "봐요", "start_ms": 605, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "알겠어요, 그때 봐요!",
"speaker": "speaker_1",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "알겠어요,", "start_ms": 35, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "그때", "start_ms": 255, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "봐요!", "start_ms": 375, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "안녕히 가세요",
"speaker": "speaker_2",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 40, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 270, "dur_ms": 300, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 07:00:48*
