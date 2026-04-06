# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=07:28:55  uptime=22180s  mem=1595MB  system_processes=445"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_072844.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 231-123-102-176-163-176-120-106-263-627-270 167-135-107-172-620-175-163-103-136-130-310-106-107-106-136-152-721-652-165-103-761-136-163-130-103-167-136-673-615-361-160-615-162-6_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 132-103-132-136-601-612-632-671-103-163-301-136-136-175-163-136-103-103-167-601-607-602-612-316-163-163-167-103-167-106-176-126-176-627-607-675-630-361-302-217-312-132-312-132-1_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 106-167-160-167-106-360-167-163-106-765-163-160-160-106-675-617-617-106-162-160-162-102-103-167-167-167-103-136-103-130-103-135-571-170-106-613-361-301-106-136-716-103-301-157-1_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 132-310-301-271-321-106-607-653-627-267-216-126-671-752-615-675-276-137-136-756-571-627-207-762-762-627-627-267-267 657-267-263-261-236-123-312-123 123-132-154-632-162-173-127-1_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 301-103-162-106-136-301-301-106-136-107-213-627-612-162-613-301-136-763-673-106-106-162-602-162-132-124-143-142-132-314-673-762-176-670-670-670-672-315-132-132-756-576-651-162-1_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 231-162-612-132-162 106-103-173-103-103-362-137-162-162-213-163-130-167-601-610-160-671-612-607-621-261-601-106-103-103-613-106-103-135-126 357-751-107-106-527-137-612 760-136-1_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 102-123-267-753-375-325-275-257-523-257-276-236-327-326-352-325-136-132-132-306-263-267-567-632-306-301-362-362-632-306-312-106-310-260-260-623-675-620-602-623-102-103-306-301-3_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 751-756-756-576-564-576-576-574-576-572-567-751-765-675-621-136-136-163-574-547-574-576_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 651-163-126-176-132-167-132-126-162-627-163-316-301-571-107-317-136-170-670-617-103-176-316-167-301-167-517-136-132-160-613-571-765-751-175-103-236-263-123-302-132-765-765-307-3_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 132-516-136-651-312-671-671-163-136-137-132-321-612-176-132-716-132-103-173-213-231-670-576-637-675-673 761-675-765-761-672-672-762 617-631-631-632-627-276-267-672-672-367-361_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 267-623-620-123-210-126-132-130-162-316-361-320-162-162-126-173-176-106-621-126-123-132-123-126-123-126-312-123-201-126-312-123-102-261-261-130-103-312-263-362-132-165-152-136-6_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 310-307-736-756-376-137-312-750-375-761-167-167-302-236-263-216-265-302-310-106-103-123-312-321-260-602-621-612-617-310-165-321-310-104-132-265-261-103-132-625-264-670-265-163-1_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 710-317-163-607-765-372-203-206-132-103-310-301-310-106-162-620-623-260-267-620-236-320-621-103-310-310-103-301-103-310-301-301-310-130-310-310-621-623-602-637-320-301-760-306-3_V.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 176-136-137-136-175-175-102-132-156-135-152_V.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 132-263-623-632-265-267-625-612-652-301-132-325-312-136-301-137-302-307-365-537-576-375-132-302-307-375-756-570-756-756-732-675-675-367-321-315-156-613-176-617-231-167-671-136-7_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 132-267-612-602-167-316-632-260-263-620-562-126-362-136-173-132-103-160-160-163-516-162-162-163-163-103-137-103-657-106-107-316-160-163-152-612-167-165-163-613-216-265-765-106-6_V.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 326-632-576-317-137-321-231-326-257-275-273-312-567-567-756-756-267-236-756-756-756-756-756-756-756-765-756-735-326-312-316-651-564-576-756-167-167-317-321-312-316-263-267-276 2_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 175-137-106-167-301-132-132-163-137-271-263-167-132-136-167-136-623-361 267 132-263-120-623 756-763-756-675-675 136-612-630-675-753-715-657-716-725-567-567-237-670-567-576-136-6_V.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 106-106 265-126-265-675-526-652-265-625-625-265-261-263-625-756-675-672-675-576-657-567-657-657-675-672-726-267-625-627-612-256-265-326-263-263-673-652-657-762-265-576-672-617-6_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 361-316-136-263-623-263-236-123-132-103 562-652-236-236-236-263-236-263-263-531-567-523-321-132-310-103-521-103-126-615-136-316-103-136-123-136-751-701-315-132-312-132-316-103-1_W.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 126-162-516-106-167-167-167-310-175-326-236-267-267-256-256-203-270-276-217-312-521-512-256-215-261-123-321-231-276-372-231-237-327-321-574_W.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 134-103-132-103-671-162-167-106-103-103-623-316-617-631-617-617-612-617-671-675-617-671-617-765-657-675-675-673-673-316-316-326-321-362-236-263-627-672-162-613-120-312-136-132-1_W.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 102-173-672 103-163-136-103-106-610-671-106-316-103-125-167-612-103-136-167-712-623-675-625-261-167-157-162-263-632-621-236-123-143 267-267_V.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 103-137-136-136-612-617-162-126-601-612-617-631-627-672-637-132-103-103-103-103-312-136-613-507-516-162-312-132-126-612-162-540-504-651-765-675-675-756-715-173-123-203-123 267-2_V.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 103-571-106-176-106-671-132-167-132-162-103-126-103-136-513-172-671-167-621-321-236-231-312-213-321-325-327-567-572-623-621-526-567-756-657-753_W.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 167-132-167-167-105-103-106-106-163-176-316-130-154-127-305-176-126-106-167-106-615-163-167-162-103-301-617-612 134-103-160-107-132-671-163-316-675-163-607-163-617-165-132-517-1_V.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 312-213-362-132-132-623-263-126-163-216-231-162-126-126-107-120-125-120-106-102-126-125-210-162-126-312-216-765-321-315-132-136 165-106-103-612 162-716-137-127-132-132-312-162-1_V.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 301-320-302-321-735-765-765-760-573-523-530-327-312-137-136-103-132-752-752-156-265-253-320-312-102-312-120-301-173-132-301-312-132-310-310-310-306-362-306-312-132-123-513-302-5_V.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 672-765-267-621-267-623-261-267-201-612-624-213-236-261-276-321-613-736-103-271-317-371-167-163-165-106-716-167-163-160-631-673-613-167-167-605-501-506-153-631-302-301-360-362-6_V.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 610-167-132 145-376-761-137-126-176-136 573 126-136-162-103-163-371-576-351-765-627_V.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 572-526-623-263-623-261-756-675-567-756 765-672-671-754-756-751-165-103-103-167-136-136-132-103-136-136-106-136-163-103-163-726-762-260-316-627-263-516-167-612-613-130-103-132-1_W.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 672-657-675-675-504-605-360-367-763-302-301-130-106-163-162-167-507-157-105-172-132-601-106-326-132-310-321-362-123-536-531-103-716-603-607-167-106-321-326-206-126-136-754-756-7_W.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 163-132 123-127-132-167-675-673-275-132-312-132-167-261-263-236-216-213-132-126-132-216-621-267-261-120-130-103-130-261-160 567-173-621-236-263-236-617-136-132-132-162-567-163-1_W.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 627-627-612-627-621-236-756-756-103-132-301-132_W.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 167-126-103-106-103-617-106-163-106-517-567-106-317-675-167-106-517-167-613-167-106-613-163-130-106-316-163-137-130-623-136-602-610-132-103-103-103-163-123-167-163-163-160-163-1_V.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 627-367-132-316-361 631-325-321-361-316-361-301-136-610-136-132-126-132-103-137-132 567-167-162-312 617-723_V.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 725-675-136-162-132-136-163-132-316-130-302-301-163-132-120-132-163-621-627-126-261-750-701-576-756-756-765 170-316-761-176-206-102-260-261-765-675-671-607-605-607-675-167-627-6_W.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 271-276-276-540-547-315-351-351-351-513-315-351-513-157-751-753-215-251-253-254-253-352-351-524-251-251-156-531-351-351-513-315-537-351-135-357-315-531-510-153-512-253-135-251-1_W.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 136 163-167-671-136-103-130-617-136-167-125-132-103-172-126-103-132-136-163-671-103-165-160-176-163-107-162-162-162-712-127-107-106-103-106-167-326-175-126 567-165-617-601-627-7_V.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 713-103-621-610-631-613-671-671-375-310-610-632-621-621-601-630-362-236-362-362-376-361-761-301-310-103-312-613-613-103-312-302-301-312-315-603-541-531-312-301-301-730-317-751-1_V.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 107-132-137-130-173-137-173-132-132-132-126-136-657-653-302-130-310-216-302-316-312-132-137-123-312-302-673-207-203-206-236-231-230-321-301-260-260-210-301-170-107-571-357-105-1_V.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 263-326-623-627-623-260-265-256-632-260-635 236-263-126-162-261-132-326-361-361-621-316-621-632-126-361-627-675-632-637-623-160-163-201-132 162-576-756-267-231-261 560-376-570-5_W.mp3)
  Segment 43: 66192ms ~ 66768ms (file: 617 103-167-162-516-135-163-103-175 160-673-176-160-761-617-172-126-134-160-651-162-310-126-173-175-617-172-126-127-137-615-103-103-106-365-172-106-175-576-126-263-612-762-716-1_V.mp3)
  Segment 44: 67768ms ~ 68344ms (file: 670-671-671-103-132-163-163-160-613-163-103-126-126-130-120-132-162-160-103-103-130 362 173-756-657-576-675-756-752-576-176-671-617-607-106-671-165-167-765-756-163-167-132-126-6_W.mp3)
  Segment 45: 69344ms ~ 69920ms (file: 672-321-627-261-621-765-167-276-620-672-627-675-372-627-126-215-654-574-162-136-123-612-206-620-621-216-213-326-326-236-261-120-132-231-231-763-276-672-217-317-127-103-137-162-1_V.mp3)
  Segment 46: 70920ms ~ 71496ms (file: 136-136-167-132-123-102-103-126-132-136-320-230-321-312-312-103-160-206-230-210-312-326-213-320 762-672_V.mp3)
  Segment 47: 72496ms ~ 73072ms (file: 173-372-375-367-376-175-375-317-375-375-317-375-570-157-157-517-351-352-352-315-315-352-351-235-574-753-357-135-526-527-256-572-527-257-257-256-250-205-250-267-627-267-627-267-2_W.mp3)
  Segment 48: 74072ms ~ 74648ms (file: 756-576-756-572-312 671-567-517-571-760-230-312-231-625 126 672 675-762-672-267-263-623-263-162-623-672-621-261 762-761-623-162-612-362-631-607-236-263-206-362-623-276-623-213-6_W.mp3)
  Segment 49: 75648ms ~ 76224ms (file: 623-267-267-267-326-376-367-756-102-762-236-672-762-276-617-627-263-261-612-621-576-752-756-576-236-263-562-523-326-326-263-263-526-562-567-625-372-216-263-627-623-216-627-136-5_W.mp3)
  Segment 50: 77224ms ~ 77800ms (file: 567-567-136 126-105-613-172-316-163-120-613-107-167-512-172-310-576-176-561-652-615-623-321-132-126-162-162-137-106-106-107-103-157-107-163-162-102-160-163-612-263-326-613-136-1_W.mp3)
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
> **Time**: 2026-04-03 07:30:17
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "오늘 점심은 뭘 먹을지 아직 못 정했어요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 40, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "점심은", "start_ms": 130, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "뭘", "start_ms": 230, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "먹을지", "start_ms": 280, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "아직", "start_ms": 390, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "못", "start_ms": 470, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "정했어요", "start_ms": 510, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "날씨가 좋아서 밖에서 먹고 싶네요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "날씨가", "start_ms": 35, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "좋아서", "start_ms": 145, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "밖에서", "start_ms": 235, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "먹고", "start_ms": 335, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "싶네요", "start_ms": 415, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "근처에 새로 생긴 일식집 어때요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "근처에", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "새로", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "생긴", "start_ms": 240, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "일식집", "start_ms": 310, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "어때요", "start_ms": 430, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "거기 초밥이 정말 맛있다고 들었어요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "거기", "start_ms": 25, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "초밥이", "start_ms": 115, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 215, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "맛있다고", "start_ms": 295, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "들었어요", "start_ms": 415, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "좋은 생각이네요 지금 바로 가요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 45, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "생각이네요", "start_ms": 125, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 255, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 335, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "가요", "start_ms": 405, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "웨이팅이 길지 않았으면 좋겠네요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "웨이팅이", "start_ms": 30, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "길지", "start_ms": 150, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "않았으면", "start_ms": 230, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "좋겠네요", "start_ms": 340, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "이번 주말에는 영화 한 편 보려고요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "이번", "start_ms": 40, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "주말에는", "start_ms": 110, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "영화", "start_ms": 230, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 310, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "편", "start_ms": 350, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "보려고요", "start_ms": 400, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "요즘 재미있는 영화가 많이 개봉했더라고요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "요즘", "start_ms": 35, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "재미있는", "start_ms": 115, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "영화가", "start_ms": 225, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 325, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "개봉했더라고요", "start_ms": 405, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "액션 영화나 코미디 영화 좋아하세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "액션", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "영화나", "start_ms": 130, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "코미디", "start_ms": 230, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "영화", "start_ms": 330, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "좋아하세요", "start_ms": 410, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "저는 장르 가리지 않고 다 좋아해요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 60, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "장르", "start_ms": 140, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "가리지", "start_ms": 230, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "않고", "start_ms": 330, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 410, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "좋아해요", "start_ms": 450, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "그럼 같이 예매할까요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 120, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "예매할까요", "start_ms": 210, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "네 좋아요 시간 맞춰봐요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 30, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "좋아요", "start_ms": 70, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "시간", "start_ms": 180, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "맞춰봐요", "start_ms": 270, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "오늘 공부는 여기까지 할게요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 45, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "공부는", "start_ms": 135, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "여기까지", "start_ms": 235, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "할게요", "start_ms": 345, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "수고 많으셨습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "수고", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "많으셨습니다", "start_ms": 160, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "다음 시간에 다시 만나요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "다음", "start_ms": 35, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "시간에", "start_ms": 115, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 215, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 305, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "안녕히 가세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "조심히 들어가세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "조심히", "start_ms": 25, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "들어가세요", "start_ms": 145, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "감사합니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 50, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "네 다음에 봬요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 35, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "다음에", "start_ms": 75, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "봬요", "start_ms": 175, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "좋은 하루 되세요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 45, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "하루", "start_ms": 135, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "되세요", "start_ms": 215, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "오늘 아침은 평소보다 일찍 일어났어요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 40, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "아침은", "start_ms": 130, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "평소보다", "start_ms": 230, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "일찍", "start_ms": 350, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "일어났어요", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "공기가 맑아서 기분이 상쾌하네요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "공기가", "start_ms": 30, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "맑아서", "start_ms": 140, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "기분이", "start_ms": 230, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "상쾌하네요", "start_ms": 330, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "가벼운 스트레칭으로 하루를 시작해요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "가벼운", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "스트레칭으로", "start_ms": 160, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "하루를", "start_ms": 300, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "시작해요", "start_ms": 400, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "따뜻한 차 한 잔 마시니 몸이 풀리네요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "따뜻한", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "차", "start_ms": 150, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 190, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "잔", "start_ms": 230, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "마시니", "start_ms": 290, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "몸이", "start_ms": 390, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "풀리네요", "start_ms": 470, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "오늘 할 일을 차근차근 계획해 봅니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 35, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 125, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "일을", "start_ms": 185, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "차근차근", "start_ms": 265, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "계획해", "start_ms": 385, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "봅니다", "start_ms": 495, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "중요한 업무부터 먼저 처리해야겠어요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "중요한", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "업무부터", "start_ms": 155, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "먼저", "start_ms": 275, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "처리해야겠어요", "start_ms": 355, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "집중해서 일하면 금방 끝날 거예요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "집중해서", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "일하면", "start_ms": 170, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "금방", "start_ms": 270, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "끝날", "start_ms": 350, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 440, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "동료들과 협력하며 즐겁게 일해요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "동료들과", "start_ms": 40, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "협력하며", "start_ms": 170, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "즐겁게", "start_ms": 290, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "일해요", "start_ms": 390, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "어려운 일도 함께라면 해낼 수 있어요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "어려운", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "일도", "start_ms": 130, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "함께라면", "start_ms": 210, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "해낼", "start_ms": 330, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 410, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 450, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "오늘 하루도 모두 파이팅합시다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "하루도", "start_ms": 130, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "모두", "start_ms": 240, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "파이팅합시다", "start_ms": 320, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "언제나 당신을 응원하고 있어요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "언제나", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "당신을", "start_ms": 150, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "응원하고", "start_ms": 250, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 380, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "힘든 일이 있어도 포기하지 말아요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "힘든", "start_ms": 35, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "일이", "start_ms": 125, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "있어도", "start_ms": 205, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "포기하지", "start_ms": 305, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "말아요", "start_ms": 425, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "당신은 충분히 잘 해낼 자격이 있어요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "당신은", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "충분히", "start_ms": 145, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 255, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "해낼", "start_ms": 295, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "자격이", "start_ms": 375, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 485, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "밝은 미래가 기다리고 있을 거예요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "밝은", "start_ms": 30, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "미래가", "start_ms": 120, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "기다리고", "start_ms": 220, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "있을", "start_ms": 340, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 420, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "스스로를 믿고 당당하게 나아가세요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "스스로를", "start_ms": 50, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "믿고", "start_ms": 180, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "당당하게", "start_ms": 260, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "나아가세요", "start_ms": 390, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "작은 행복들이 모여 큰 기쁨이 됩니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "작은", "start_ms": 40, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "행복들이", "start_ms": 130, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "모여", "start_ms": 250, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 320, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "기쁨이", "start_ms": 360, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "됩니다", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "감사하는 마음으로 하루를 채워가요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "감사하는", "start_ms": 25, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "마음으로", "start_ms": 145, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "하루를", "start_ms": 265, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "채워가요", "start_ms": 365, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "주변 사람들에게 따뜻한 인사를 건네보세요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "주변", "start_ms": 50, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "사람들에게", "start_ms": 140, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "따뜻한", "start_ms": 270, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "인사를", "start_ms": 380, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "건네보세요", "start_ms": 480, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "서로 웃으며 인사하는 것만으로도 행복해져요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "서로", "start_ms": 35, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "웃으며", "start_ms": 115, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "인사하는", "start_ms": 215, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "것만으로도", "start_ms": 335, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "행복해져요", "start_ms": 485, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "오늘도 당신은 정말 멋진 사람입니다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "오늘도", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "당신은", "start_ms": 155, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 255, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "멋진", "start_ms": 335, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "사람입니다", "start_ms": 425, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "항상 건강하시고 평안하시길 빌게요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "항상", "start_ms": 40, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "건강하시고", "start_ms": 130, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "평안하시길", "start_ms": 290, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "빌게요", "start_ms": 440, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "감사합니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 30, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "다음에 또 만나요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 160, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 220, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "안녕히 계세요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "계세요", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "바이바이",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "바이바이", "start_ms": 25, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "굿나잇",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "굿나잇", "start_ms": 35, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "행복하세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "행복하세요", "start_ms": 50, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "사랑합니다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "사랑합니다", "start_ms": 40, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "축복합니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "축복합니다", "start_ms": 30, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "화이팅",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "화이팅", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 07:30:17*
