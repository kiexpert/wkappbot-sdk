# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=06:14:49  uptime=17735s  mem=1503MB  system_processes=445"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_061439.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 263-132-103-637-627-316-132-657-316 675-672-621-672-673-672-637 631-617-547-547-257-123-301-320 751-756-126-265-236-263-263-236-103-261-126-651-631-156-160-751-165-657-165-612-3_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 126-106-162-617-715-102-106-127-751-602-602-261-106-163-162-163-617-617-574-576-576-576-103-675-576-576-761-175-176-137-132-675-670-760-276-207-276-207-206-672-672-671-612-143-7_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 651-673-157-573-675-657-652-567-257-265-657-523-652-567-652-657-156-576-562-627-265-652-576-576-657-567-657-756-657-265-562-562-657-657-567-572-673-627-625-675-675-652-267-632-3_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 617-132-675-367-637-362-612-675-675-615-613-637-615-625-156-157-632-160-261-607-167-167-531-157-167-231-716-627-675-567-765-756-765-756-123-123-215-572-156-156-312-657-675-627-6_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 103-132-165-103-163-361-167-163-103-657-126-172-162-671-176-621-213-120-312-237-231-137-507-137-172-132-413-143-132-312_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 132-136-102-751-627-672-627-670-276-627-526-567-167-132-321-312-136-103-105-103-162-157-156-265-260-236-120-132-162-231-310-621-263-712-351-167-136-263-263-760-760-376-716-317-1_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 305-175-216-306-765-756-675-672-307-306-361-236-321-103-756 762-765-132-130 657-627-125-127-261-751 231-132-617-567-167-176-576-705-156-617-123-576-103-136-132-203-302-103-123-1_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 163-163-167-675-675-763-120-126-126-162-106-306-620-260-263-230-605-576-752-302-103-351-635-756-675-675-670-267-126-167-231-316-162-607-632-673-173-306-361-601-673-162-627-206-1_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 562-256-632-621-512-625-652-625-675-675-675-627-627-617-627-326-236-261-263-265-236-231-261 613-631-612_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 163-175-160-621-126-103-670-136-321-106-516-675-671-321-617-623-315-163-103-576-123-126-261-162-621-254-234-765-675-123-623-576-576-765-760 106-107-132 132-302-571-567-507-657-6_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 756-657-672-270-301-312-673-312-103-103-316-651-753-751-756-175-375-675-675-637-576-567 657-321 671-601-617-167-163-162-136-316-160-106-106-612-126-106-750-750-175-160-106-103-1_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 657-620-236-301-751-572-531-157-571-105-165-106-167-701-106-162-163-301-103-120-672-671-167-106-261-201-267-762-670-610-163-136-162-126-671-162-106-106-106-510-574-320-230-203-2_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 765-136-321-325-162-145-315-104-167-571-206-206-206 103-103-103-152-576-571-527-216-102-126-162-231-213-136-316-352-521-651-763-370-367-306-316-361-310-103-316-160-103-103-261-7_V.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 123-201 302-236 123-231-216-216-203-310-310-231-126-312-301-130-617-627-267-261-267-617-621-326-625-263-213-216-213-231-213-261-621-261-263-621-261-126-162-132 576-756-756-751-6_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 162-612-210-102-310-132-621-675-625-657-216-267-261-623-672-657-602-657-672-632-612-621-612-162-615-620-675-672-672-623-765-605-652-126-271-276-675 216-236-103-136-103-103-136-1_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 610-607-607-671-162-126-612-632-627-163-306-621-237-326-132-126-132-123-601-163-130-675-371-326-623-627-623-631-136-163-136-163-136-321-657-625-230-320-206-263-213-236-362-507-5_V.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 136-106-103-107-163-627-627-316-103-103-103-102-136-137-103-143-754-765-602-260-230-203-203-263-176-157-136-137-163-162-617-542-547-570-507-574-756-574-574-570-547-547-570-750-7_V.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 260-260-206-206-203-206-206-321-301-567-576-521-320-302-301-163-136-162-106-137-102-260-201-132-213-206-130-236-236-532-236-206-201-261-206-102-321-301-132-103-312-132-326-203-6_V.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 123-237-235-105-312-162-316-206-236-321-132-126-175-765-321-236-623-163-127-123-123-316-327-312-321-263-267-730-753-327-123-201-213-312-231-123-570-756-750-756-675-137-173-132-3_V.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 567-563-316-321-527-576-563-631-361-621-362-632-672-527-572-621-126-163-136-312-132-321-301-103-162-316-361-316-126-312-132-302-163-321-132-321-136-102-321-132-123-312-123-123-3_V.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 312-215-213-237-325-237-327-312-231-231-152-231-216-213-325-216-621-617-672-267-267-236-265-231-321-321-671-271-612-764-326-321-236-263-621-362-351-312-213-312-321-216-213-625-6_W.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 163-612 326-236-267-261-231 126-276 601-613-621 103-612-362-120-657-651-675-103-175-163-167-162 167-103-106-165-106-103_V.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 273-237-276-206-207-236-253-652-362-365-263-623-627-132-172-173-126-126-213-327-251-572-527-572-576-574-537-526-531-351-356-576-567-357-132-132-157-165-165-165-163-315-513-576-2_W.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 163-130-316-612-176-163-103-136-547-176-617-617-163-162-106-162-167-107-132-756-702-321-173-106-103-106-617-130-136-130-103-165-516-371-712-561-621-615-165-617-613-756-762-103-1_V.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 673-627-617 627-672-621 672-627-672-672-627-367 672-627-627_W.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 762-675-762-673-657-675-765-765-672-267-675-762-671-156-672-632-627-365-756-675-627-612-607-627 761-617-765-761-671-675-765-671-617-671-617-716-761-317-627-263-613-162-763-763-7_W.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 371-132-362-314-146-261-317-162-251-321-164-241-367-254-613-362-236-216-267-162-263-327-637-276-236-124-173_V.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 160-103-130-132-120-632-621-621-621-160-621-261-632-623 670-670-674-670-674-671-617-107-713-103-167-637-167-107-134-315-132-167 203-206-206-312-312-302-312-602-602-620-620-627-1_W.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 657-675-576-573-765-763-760 126-231-136-162-136-130 263-236-236-326-321 201-162-162-672-763_W.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 621-623-267-263-627-267-263-523-527-265-261-126-236-326 526-267-265-362-543-547-547-547-547-350-351-351-574-235-263-756-567-675-632-567-567-657-567-657 325-325-265-375-326-261-5_W.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 162-103-132-167-152-163-617-765-625-612-167-623-617-103-137-163-136-173-103-103-106-106-136-162-627-632-623-160-316-162-163-610-126-103-136-153-162-165-163-165-125-651-157-135-1_V.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 125-127-132-736-763-765-167-176-126-126_V.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 561-136-103-103-160-163-160-134-103-134-136-107-137-162-263-213 216-213-126-236-126-102-103-236-162-612-130-103-162-103-106-162-576-635-106-716-102-312-631-163-617-106-103-162-1_V.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 631-163-163-321-312-361-236-316-612-621-612-162-621-261-623-621-621-623-623-620-627-624-627-625-620-263-621-621-621-621-261-623-627-620-620-620-621-620-624-627-617-163-613-132-3_W.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 261-612-206-123-261-316 602-610-620-607-675-167-621-167-162-106-263-632-216-126-130-302-201-321-627-213-167-625-627-631-162-160-627-325-132-132-106-312-130-127-120-106-160-130-1_V.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 603-602-627-765-312-160-316-612-763-632-631-316-361-361-632-316-301-162-163-162-106-106-267-320-302-136-326-326-153-657-765-567-765-765-675-637-361-532-321-613-612-602-612-261-6_V.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 126-312-123-715-107-175-751-751-317-371-317-136-317-137-713-317-715-715-175-176-132-715-375-312-376-367-613-361-132-276-750-756-761-176-567-351-351-162-267-627-216-132-127-312-3_V.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 275-726-540-574-573-537-253-215-162-126-526-325-325-215-213-351-523-516-574-537-351-573-651-625-635-365-653-567-356-325-216-637-537-527-562-532-253-573-627-537-536-536-136-756-6_W.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 675-623-617-726-526-567-572-517-137-167-132_W.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 320-261-321-321-612-671-621-136-126-312-362-613-621-312-136-130-136-236-213-316-612-162-605-657-612-123-126-163-612-602-160-136-132-103-130 132-103-132-130-130_W.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 132-123-103-103-103-132-316-126-316-130-130-310-103_V.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 765-765-760-706-705-765-621-617-163-136-637-632-750-765-760-675-675-514-514-527-170-157-132-130-162-160-236-216-573-574-135-135-153 136-130-107-176-165-163-564-562-154-673-103-1_W.mp3)
  Segment 43: 66192ms ~ 66768ms (file: 623-756-567 162-134-132-310-163-136-132-123_V.mp3)
  Segment 44: 67768ms ~ 68344ms (file: 236-236-236-236-263-572-256-527-524-574-540-524-620-620-627-620-627-267-267-267-267-265-257-526-356-326-356-362-632-623-263-726-267-216-261-265-251-251-514-517-152-527-153-517-5_W.mp3)
  Segment 45: 69344ms ~ 69920ms (file: 156-176-107-167-160-716-607-165-162-160-675-106-103-167-167-216-567-652-132-316-136-136-106-163-132-612-167-176-610-162-312-160-132-126-301-167-675-671-106-132-167-631-675-163-1_V.mp3)
  Segment 46: 70920ms ~ 71496ms (file: 574-576-527-752-527-673-675-375-715-576-235-531-537-517-135-130-315-351-312-132-136-135-261-361-316-167-163-316-756-576-574-531-574-571-521-514-256-652-576-375-623-263-263-623-6_W.mp3)
  Segment 47: 72496ms ~ 73072ms (file: 602-762-602-765-617-167-725-102-132-136-153-176-167-165-756-106-106-316-310-273-623-360-362-236-326-310-263-231-672-621-260-260-206-321-103-260-260-260-620-620-261-126-216-164-6_V.mp3)
  Segment 48: 74072ms ~ 74648ms (file: 326-103-172-237-106-316-160-163-316-615-136-271-126-312-162-105-106-167-213-127 167-163-513-176-317-106-165-632-172-126-103-176-106-617-601-136-621-621-136-103-130-623-160-153-1_V.mp3)
  Segment 49: 75648ms ~ 76224ms (file: 625-263-267-206-263-623-672-672-362-306-360-360-136-136-310-130-301-103-136-163-163-316-627-162-173-751-725-320-362-302-123-317-753-753-703-267-231-321-263-312-163 302-627-612-1_W.mp3)
  Segment 50: 77224ms ~ 77800ms (file: 562-341-123-762-341-261-162-172-634-136-136-103-103-103-637-632-326-621-627-756-765-706-705-756-301-367-361-123-103-103-173-127-103-136-103-163-362-301-612-672-137-123-130-310-5_V.mp3)
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
> **Time**: 2026-04-03 06:16:04
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "오늘 점심은 뭐 먹을까요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 60, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "점심은", "start_ms": 180, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 320, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "먹을까요?", "start_ms": 380, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "따뜻한 국물이 있는 게 좋겠어요.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "따뜻한", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "국물이", "start_ms": 200, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "있는", "start_ms": 330, "dur_ms": 70, "phase_lr_deg": 5.0},
{"word": "게", "start_ms": 410, "dur_ms": 40, "phase_lr_deg": 5.0},
{"word": "좋겠어요.", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 5.0}
]
},
{
"segment": 3,
"transcript": "(music)",
"speaker": "null",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 4,
"transcript": "그럼 칼국수 먹으러 갈까요?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 40, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "칼국수", "start_ms": 140, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "먹으러", "start_ms": 300, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "갈까요?", "start_ms": 420, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "That sounds like a great idea.",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "That", "start_ms": 30, "dur_ms": 60, "phase_lr_deg": -2.0},
{"word": "sounds", "start_ms": 100, "dur_ms": 90, "phase_lr_deg": -2.0},
{"word": "like", "start_ms": 200, "dur_ms": 50, "phase_lr_deg": -2.0},
{"word": "a", "start_ms": 260, "dur_ms": 30, "phase_lr_deg": -2.0},
{"word": "great", "start_ms": 300, "dur_ms": 100, "phase_lr_deg": -2.0},
{"word": "idea.", "start_ms": 410, "dur_ms": 150, "phase_lr_deg": -2.0}
]
},
{
"segment": 6,
"transcript": "근처에 맛있는 집이 있어요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "근처에", "start_ms": 70, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "맛있는", "start_ms": 210, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "집이", "start_ms": 360, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "있어요.", "start_ms": 450, "dur_ms": 110, "phase_lr_deg": 5.0}
]
},
{
"segment": 7,
"transcript": "웨이팅이 조금 길 수도 있는데 괜찮으세요?",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "웨이팅이", "start_ms": 45, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "조금", "start_ms": 195, "dur_ms": 90, "phase_lr_deg": 5.0},
{"word": "길", "start_ms": 295, "dur_ms": 50, "phase_lr_deg": 5.0},
{"word": "수도", "start_ms": 355, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "있는데", "start_ms": 425, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "괜찮으세요?", "start_ms": 515, "dur_ms": 50, "phase_lr_deg": 5.0}
]
},
{
"segment": 8,
"transcript": "배가 많이 고픈데 금방 빠지겠죠?",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "배가", "start_ms": 55, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 155, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "고픈데", "start_ms": 245, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "금방", "start_ms": 355, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "빠지겠죠?", "start_ms": 455, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "I'm starving. I hope the line isn't too long.",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "I'm", "start_ms": 40, "dur_ms": 40, "phase_lr_deg": -2.0},
{"word": "starving.", "start_ms": 90, "dur_ms": 110, "phase_lr_deg": -2.0},
{"word": "I", "start_ms": 210, "dur_ms": 40, "phase_lr_deg": -2.0},
{"word": "hope", "start_ms": 260, "dur_ms": 80, "phase_lr_deg": -2.0},
{"word": "the", "start_ms": 350, "dur_ms": 40, "phase_lr_deg": -2.0},
{"word": "line", "start_ms": 400, "dur_ms": 60, "phase_lr_deg": -2.0},
{"word": "isn't", "start_ms": 470, "dur_ms": 50, "phase_lr_deg": -2.0},
{"word": "too", "start_ms": 530, "dur_ms": 30, "phase_lr_deg": -2.0},
{"word": "long.", "start_ms": 570, "dur_ms": 40, "phase_lr_deg": -2.0}
]
},
{
"segment": 10,
"transcript": "금방 자리가 날 거예요. 가봅시다.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "금방", "start_ms": 65, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "자리가", "start_ms": 175, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "날", "start_ms": 305, "dur_ms": 40, "phase_lr_deg": 5.0},
{"word": "거예요.", "start_ms": 355, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "가봅시다.", "start_ms": 465, "dur_ms": 100, "phase_lr_deg": 5.0}
]
},
{
"segment": 11,
"transcript": "여기 정말 사람 많네요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "여기", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 140, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "사람", "start_ms": 250, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "많네요.", "start_ms": 350, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "메뉴판 좀 보여주세요.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "메뉴판", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "좀", "start_ms": 200, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "보여주세요.", "start_ms": 270, "dur_ms": 290, "phase_lr_deg": 5.0}
]
},
{
"segment": 13,
"transcript": "(noise)",
"speaker": "null",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 14,
"transcript": "바지락 칼국수 삼 인분 주세요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "바지락", "start_ms": 60, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "칼국수", "start_ms": 210, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "삼", "start_ms": 350, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "인분", "start_ms": 400, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "주세요.", "start_ms": 480, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "만두도 한 접시 추가할까요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "만두도", "start_ms": 55, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "한", "start_ms": 185, "dur_ms": 50, "phase_lr_deg": 5.0},
{"word": "접시", "start_ms": 245, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "추가할까요?", "start_ms": 335, "dur_ms": 230, "phase_lr_deg": 5.0}
]
},
{
"segment": 16,
"transcript": "Yes, please. I love dumplings.",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Yes,", "start_ms": 40, "dur_ms": 70, "phase_lr_deg": -2.0},
{"word": "please.", "start_ms": 120, "dur_ms": 90, "phase_lr_deg": -2.0},
{"word": "I", "start_ms": 220, "dur_ms": 40, "phase_lr_deg": -2.0},
{"word": "love", "start_ms": 270, "dur_ms": 80, "phase_lr_deg": -2.0},
{"word": "dumplings.", "start_ms": 360, "dur_ms": 190, "phase_lr_deg": -2.0}
]
},
{
"segment": 17,
"transcript": "만두는 김치랑 고기 중에 고를 수 있네요.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "만두는", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "김치랑", "start_ms": 180, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "고기", "start_ms": 300, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "중에", "start_ms": 390, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "고를", "start_ms": 460, "dur_ms": 50, "phase_lr_deg": 5.0},
{"word": "수", "start_ms": 520, "dur_ms": 30, "phase_lr_deg": 5.0},
{"word": "있네요.", "start_ms": 560, "dur_ms": 10, "phase_lr_deg": 5.0}
]
},
{
"segment": 18,
"transcript": "반반으로 주문 가능한지 물어볼게요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "반반으로", "start_ms": 60, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "주문", "start_ms": 210, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "가능한지", "start_ms": 300, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "물어볼게요.", "start_ms": 420, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "와 드디어 나왔어요. 김이 모락모락 나네요.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "와", "start_ms": 45, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "드디어", "start_ms": 115, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "나왔어요.", "start_ms": 225, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "김이", "start_ms": 345, "dur_ms": 70, "phase_lr_deg": 5.0},
{"word": "모락모락", "start_ms": 425, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "나네요.", "start_ms": 555, "dur_ms": 10, "phase_lr_deg": 5.0}
]
},
{
"segment": 20,
"transcript": "뜨거우니까 천천히 드세요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "뜨거우니까", "start_ms": 80, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "천천히", "start_ms": 270, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "드세요.", "start_ms": 400, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "It looks delicious. I'll dig in.",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "It", "start_ms": 40, "dur_ms": 40, "phase_lr_deg": -2.0},
{"word": "looks", "start_ms": 90, "dur_ms": 90, "phase_lr_deg": -2.0},
{"word": "delicious.", "start_ms": 190, "dur_ms": 150, "phase_lr_deg": -2.0},
{"word": "I'll", "start_ms": 350, "dur_ms": 60, "phase_lr_deg": -2.0},
{"word": "dig", "start_ms": 420, "dur_ms": 80, "phase_lr_deg": -2.0},
{"word": "in.", "start_ms": 510, "dur_ms": 50, "phase_lr_deg": -2.0}
]
},
{
"segment": 22,
"transcript": "국물 맛이 정말 시원하네요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "국물", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "맛이", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 250, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "시원하네요.", "start_ms": 370, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "면발도 아주 쫄깃해요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "면발도", "start_ms": 65, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "아주", "start_ms": 195, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "쫄깃해요.", "start_ms": 285, "dur_ms": 270, "phase_lr_deg": 5.0}
]
},
{
"segment": 24,
"transcript": "여기 김치가 특히 맛있지 않나요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "여기", "start_ms": 55, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "김치가", "start_ms": 135, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "특히", "start_ms": 245, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "맛있지", "start_ms": 345, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "않나요?", "start_ms": 445, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "(noise)",
"speaker": "null",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 26,
"transcript": "맞아요. 칼국수 집은 김치가 생명이죠.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "맞아요.", "start_ms": 40, "dur_ms": 90, "phase_lr_deg": 5.0},
{"word": "칼국수", "start_ms": 140, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "집은", "start_ms": 280, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "김치가", "start_ms": 370, "dur_ms": 90, "phase_lr_deg": 5.0},
{"word": "생명이죠.", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 5.0}
]
},
{
"segment": 27,
"transcript": "I need more kimchi. Can we get a refill?",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 35, "dur_ms": 30, "phase_lr_deg": -2.0},
{"word": "need", "start_ms": 75, "dur_ms": 60, "phase_lr_deg": -2.0},
{"word": "more", "start_ms": 145, "dur_ms": 50, "phase_lr_deg": -2.0},
{"word": "kimchi.", "start_ms": 205, "dur_ms": 120, "phase_lr_deg": -2.0},
{"word": "Can", "start_ms": 335, "dur_ms": 50, "phase_lr_deg": -2.0},
{"word": "we", "start_ms": 395, "dur_ms": 40, "phase_lr_deg": -2.0},
{"word": "get", "start_ms": 445, "dur_ms": 40, "phase_lr_deg": -2.0},
{"word": "a", "start_ms": 495, "dur_ms": 20, "phase_lr_deg": -2.0},
{"word": "refill?", "start_ms": 525, "dur_ms": 50, "phase_lr_deg": -2.0}
]
},
{
"segment": 28,
"transcript": "셀프 코너에서 가져오면 될 거예요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "셀프", "start_ms": 60, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "코너에서", "start_ms": 170, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "가져오면", "start_ms": 320, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "될", "start_ms": 460, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "거예요.", "start_ms": 510, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "제가 갔다 올게요. 필요한 거 더 있으세요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "제가", "start_ms": 55, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "갔다", "start_ms": 165, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "올게요.", "start_ms": 245, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "필요한", "start_ms": 365, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 455, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 495, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "있으세요?", "start_ms": 545, "dur_ms": 30, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "단무지도 좀 가져다 주세요.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 75,
"audio_type": "speech",
"words": [
{"word": "단무지도", "start_ms": 75, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "좀", "start_ms": 225, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "가져다", "start_ms": 295, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "주세요.", "start_ms": 405, "dur_ms": 160, "phase_lr_deg": 5.0}
]
},
{
"segment": 31,
"transcript": "(noise)",
"speaker": "null",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 32,
"transcript": "자, 여기 있습니다. 맛있게 드세요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "자,", "start_ms": 50, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "여기", "start_ms": 120, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있습니다.", "start_ms": 230, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "맛있게", "start_ms": 360, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "드세요.", "start_ms": 480, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "만두 소가 아주 꽉 찼네요. 진짜 맛있어요.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "만두", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "소가", "start_ms": 130, "dur_ms": 70, "phase_lr_deg": 5.0},
{"word": "아주", "start_ms": 210, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "꽉", "start_ms": 300, "dur_ms": 50, "phase_lr_deg": 5.0},
{"word": "찼네요.", "start_ms": 360, "dur_ms": 90, "phase_lr_deg": 5.0},
{"word": "진짜", "start_ms": 460, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "맛있어요.", "start_ms": 530, "dur_ms": 40, "phase_lr_deg": 5.0}
]
},
{
"segment": 34,
"transcript": "This dumpling is amazing. The skin is so thin.",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "This", "start_ms": 60, "dur_ms": 60, "phase_lr_deg": -2.0},
{"word": "dumpling", "start_ms": 130, "dur_ms": 120, "phase_lr_deg": -2.0},
{"word": "is", "start_ms": 260, "dur_ms": 40, "phase_lr_deg": -2.0},
{"word": "amazing.", "start_ms": 310, "dur_ms": 140, "phase_lr_deg": -2.0},
{"word": "The", "start_ms": 460, "dur_ms": 40, "phase_lr_deg": -2.0},
{"word": "skin", "start_ms": 510, "dur_ms": 60, "phase_lr_deg": -2.0},
{"word": "is", "start_ms": 570, "dur_ms": 0, "phase_lr_deg": -2.0}
]
},
{
"segment": 35,
"transcript": "배가 정말 부르네요. 기분 좋은 배부름이에요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "배가", "start_ms": 45, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 135, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "부르네요.", "start_ms": 235, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "기분", "start_ms": 355, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 435, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "배부름이에요.", "start_ms": 515, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "디저트로 아이스크림 어떠세요?",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "디저트로", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "아이스크림", "start_ms": 210, "dur_ms": 180, "phase_lr_deg": 5.0},
{"word": "어떠세요?", "start_ms": 400, "dur_ms": 170, "phase_lr_deg": 5.0}
]
},
{
"segment": 37,
"transcript": "좋죠! 근처 편의점에 들렀다 가요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "좋죠!", "start_ms": 65, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "근처", "start_ms": 175, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "편의점에", "start_ms": 285, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "들렀다", "start_ms": 425, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "가요.", "start_ms": 535, "dur_ms": 40, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "Ice cream sounds perfect right now.",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Ice", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": -2.0},
{"word": "cream", "start_ms": 110, "dur_ms": 90, "phase_lr_deg": -2.0},
{"word": "sounds", "start_ms": 210, "dur_ms": 90, "phase_lr_deg": -2.0},
{"word": "perfect", "start_ms": 310, "dur_ms": 120, "phase_lr_deg": -2.0},
{"word": "right", "start_ms": 440, "dur_ms": 70, "phase_lr_deg": -2.0},
{"word": "now.", "start_ms": 520, "dur_ms": 50, "phase_lr_deg": -2.0}
]
},
{
"segment": 39,
"transcript": "어떤 맛으로 고를까요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "어떤", "start_ms": 55, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "맛으로", "start_ms": 165, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "고를까요?", "start_ms": 285, "dur_ms": 290, "phase_lr_deg": 5.0}
]
},
{
"segment": 40,
"transcript": "전 초코 맛이요. 당신은요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "전", "start_ms": 60, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "초코", "start_ms": 130, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "맛이요.", "start_ms": 240, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "당신은요?", "start_ms": 360, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "(noise)",
"speaker": "null",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 42,
"transcript": "저는 상큼한 딸기 맛으로 할게요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 45, "dur_ms": 90, "phase_lr_deg": 5.0},
{"word": "상큼한", "start_ms": 145, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "딸기", "start_ms": 265, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "맛으로", "start_ms": 375, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "할게요.", "start_ms": 495, "dur_ms": 80, "phase_lr_deg": 5.0}
]
},
{
"segment": 43,
"transcript": "(noise)",
"speaker": "null",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 44,
"transcript": "오늘 점심 정말 완벽했네요. 고마워요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 70, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "점심", "start_ms": 180, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 280, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "완벽했네요.", "start_ms": 390, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "고마워요.", "start_ms": 510, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "저도요. 다음에 또 맛있는 거 먹으러 가요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "저도요.", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "다음에", "start_ms": 170, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "또", "start_ms": 290, "dur_ms": 40, "phase_lr_deg": 5.0},
{"word": "맛있는", "start_ms": 340, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "거", "start_ms": 450, "dur_ms": 30, "phase_lr_deg": 5.0},
{"word": "먹으러", "start_ms": 490, "dur_ms": 70, "phase_lr_deg": 5.0},
{"word": "가요.", "start_ms": 570, "dur_ms": 6, "phase_lr_deg": 5.0}
]
},
{
"segment": 46,
"transcript": "Let's do this again soon.",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Let's", "start_ms": 40, "dur_ms": 70, "phase_lr_deg": -2.0},
{"word": "do", "start_ms": 120, "dur_ms": 40, "phase_lr_deg": -2.0},
{"word": "this", "start_ms": 170, "dur_ms": 50, "phase_lr_deg": -2.0},
{"word": "again", "start_ms": 230, "dur_ms": 100, "phase_lr_deg": -2.0},
{"word": "soon.", "start_ms": 340, "dur_ms": 230, "phase_lr_deg": -2.0}
]
},
{
"segment": 47,
"transcript": "네, 오후 업무도 화이팅 하세요!",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 60, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "오후", "start_ms": 120, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "업무도", "start_ms": 230, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "화이팅", "start_ms": 360, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "하세요!", "start_ms": 490, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "You too! Have a productive afternoon.",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "You", "start_ms": 35, "dur_ms": 50, "phase_lr_deg": -2.0},
{"word": "too!", "start_ms": 95, "dur_ms": 70, "phase_lr_deg": -2.0},
{"word": "Have", "start_ms": 175, "dur_ms": 60, "phase_lr_deg": -2.0},
{"word": "a", "start_ms": 245, "dur_ms": 30, "phase_lr_deg": -2.0},
{"word": "productive", "start_ms": 285, "dur_ms": 140, "phase_lr_deg": -2.0},
{"word": "afternoon.", "start_ms": 435, "dur_ms": 140, "phase_lr_deg": -2.0}
]
},
{
"segment": 49,
"transcript": "안녕히 가세요!",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 80, "dur_ms": 180, "phase_lr_deg": 5.0},
{"word": "가세요!", "start_ms": 270, "dur_ms": 310, "phase_lr_deg": 5.0}
]
},
{
"segment": 50,
"transcript": "조심히 들어가세요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "조심히", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "들어가세요.", "start_ms": 200, "dur_ms": 370, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 06:16:04*
