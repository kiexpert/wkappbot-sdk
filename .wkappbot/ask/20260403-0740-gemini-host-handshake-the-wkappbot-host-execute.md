# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=07:38:51  uptime=22776s  mem=1696MB  system_processes=446"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_073841.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 153-103-230-132-653-576-235-276-513-571-132-325-367-135-136-315-356-165-375-752-703-570-570-157-132-170-237-137-127-123-310-207-321-312-310-732-237-273-276-203-130-302-320-230-3_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 263-265-265-236-321-276-527-574-235-235-326-352-563 215-157-572-521-235-672-617-176-627-167-176-715-753-165-126-615-157-361-567-576-562-256-265-672-367-236-260-263-236-231-236-1_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 673-672 617-672-672-612 163-176-761-263-326_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 612-617-136-513-567-617-106-103-765-167-651-657-631-615-167-160-103-675-715-157-567-576 567-573_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 625-625-630-103-103-301-627-627-627-136-106-103-132-103-103-136-320-326-623-610-160-613-132-102-130-103-160-106-162-167-106-301-136-103-312-106-672-605-610-160-267-621-612-621-1_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 132-236-572-175-163 167-165-162-602-612-165-312-162-715-137-617-160-612-612-136-310-621-136-632-136-103-675-610-136-132-103-613-126-652-621-617-176-367-751-216-263-671 365-361-3_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 213-126-103-126-102-163-263-321-132-623-627-320-132-136-167-163-136-213-230-207-267-206-207-206-206-302-326-302-203-320-321-203-263-216-103-123-205-725-763-302-327-312-301-302-3_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 310-103-316-630-306-360-263-263-326-320-236-206-230-516-306-260-126-502-206-203-302-130-301-103-132-302-203-607-627-160-617-570-651-167-765-765-576-573-560-763-126-301-376-312-3_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 321-312-672-632 230-231-326-231-236 103-132-312-136-632-213-123-213-236-260-236-231-213-231-524-520-250-132-162-361-621-213-612-765-617-236-236-123-123-657-275-623-326-321-326-2_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 136-126-106-613-162-167-103-107-167-163-137-163-315-367-156-152-615-756-156-615-102-103-137-152-163-517-106-137-103-132-176-167-156-103-157-632-176-312-167-673-756-756-162-761-1_V.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 265-321-263-632-263-265-263-625-265-261-261-627-267-265-213-736-127-267-627-623-362-261-326-326-236-623-623-623-362-261-627-267-627-627-652-652-365-576-275-235-263-267-236-312-2_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 103-521-362-162-126-176-163-631-132-136-106-613-160-136-726-160-107-671-567-126-103-671-361-103-167-106-136-103-362-231-156-162-167-167-103-136-136-103-162-514-163-623-163-162-6_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 132-132-657-671-675-567-652-163-156-361-627-627-623-357-371-761-756-156-567-136-167-617-652-657-657-726-176-127-627-126-175-617-316-712-167-261-567-651-106-165 162-627-176-162-1_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 510-120-326-310-136-263-263-236-261-236-627-671-265-625-672-612-672-273-360-560-751-537-652-652-635-623-627-625-621-625-265-657-652-375-165-765-763-276-126-672-206-217-276-213-1_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 102-120-251-106-236-306-301-203-132-301-103-623-260-672-162-106-321-260-103-301-320-261-610-135-312-310-601-216-310-136-163-103-103-136-103-156-627-136-106-310-106-103-610-103-1_V.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 261-320-236-356-621-263-720-756-765-672-627-123-102-123-612-621-502-612-176-107-765-675-671-103-135-135-160-231-360-301-103-362-321-306-103-316-132-627-612-301-326-302-753-576-3_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 172-176-615 132 612-627-621-136-132-160-136-106-617-167-106-172-163-162-621-261-316-136-567-675-162-672-102-612-231-163-132-136-132-672-103-106-163-136-271-617 165-163-136-162-6_V.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 756-326-320-301-206-206-206-237-231-231-213-261-526-612-631-367-367-720-370-573-630-750-103-310-136-136-107-167-106-136-310-326-132-230-203-203-320-236-237-327-627-607-627-206-2_V.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 236-612-216-261-236-123-213-120-163-162-123-132 237-236-231-265 132-132 167_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 302-302-103-310-310-312-327-312-123-317-302-327-732-756-713-276-157-136-176-126-125-315-135-173-620-236-321-132-501-514-541-547-107-231-312-302-137-310-570-126-751-310-132-703-7_W.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 126-267-613-162-265-206-736-735-750-312-132-261-602-264-260-265-502-250-103-130-306-316-316-361-103-301-316-312-310-123-136-271-237-312-213-326-536-572-671-206-103-136-103-103-1_V.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 623-612-126-126-107-670-607-160-162-167-756-632-316-106-163-167-126-216-176-130-317-310-163-106-136-301-316-136-572-703-712-152-651-261_V.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 163-317-230-632-623-612-106-132-163-103-213-136-106-162-167-163-176-310-763-260-236-263-176-571-173-107-103-372-230-672-203-230-230-326-123-132-263 136-136-132-263-312-162-136-1_W.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 620-260-126-231-263-106-103-201-162-162-102-106-106-176-516-657-206-230-320-352-362-130-103-132-103-132-627-602-621-132-134-135-136-162-602 576-576-576-623-602-260-602-560-624-5_V.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 206-203-123-120-103-126-132-312-136-120-730-723-216-267-126-162-123-136-123-316-136-136-203-320-321-132-312-316-312-236-263-216-726-306-206-261-652-260-620-265-162-750-705-672-3_V.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 321-123-126-614-136-671-637-306-237-560 756-765-736-672-637-763-637-672-260-621-653-563-163-236-617-657-267-675-672-756-673-607-362-607-635-625-623-623-672-607-765-672-627-762-2_W.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 261-321-327-672-627-637-173-716 132-367-263-236-326-362-236-216-361-265-267-265-527-527-352-253-265-265-261-265-265-312-126-213-125-236-215-562-572-257-512-257-326-123-126-236-2_W.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 106-165-267-172-127-167-167-312-317-160-136-316-157-162-152-163-163-136-213-651-612-263-132-571-310-132-103-620-163-301-231-126-310-136-120-261-126-126-132-103-167-163-167-165-1_V.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 206-203-203-231-236-132-206-260-620-260-312-136-312-362-301-132-312 567-267-672-361-260-310-610-610-613-362-316-106-613-574-671-317-623-267-216-263-236-632-256-165-250-120-326-2_W.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 605-367-176-163-167-106-103-107-617-176-160-236-132-612-163-216-213-136-163-712-160-167-125-162-127-132-103 135-362-162-632-160 761-136-106-561-610-167-360-637-260-620-617-362 6_V.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 163-103-613-136-103-163-103-136-301-176-326-620_V.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 256-203-267-203-236-320-206-260-206-260-602-206-206-236-261-520-301-132-136-123-162-167-102-326-310-570-752-765-625-672-102-237-527-137-123-526-521-216-120-231-103-126-123-216-2_W.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 675-270-312-310-302-320-326-132-315-162-261-132-123-167-263-236-267-263-736-765-721-613-136-163-632-612-357-513-534-312-134-216-236-761-163-630-106-103-163-765-156-361-306-136-1_V.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 576-627-765-675-675-652-657-675-657-675-765-756-675-657-130-321-312 567-576-567-576-576-657-756-576-567-561-567-657-576-576-567-576-567-675-652-657-576-572-567-567-576-657-567-5_W.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 136-106-236-612-326-216-512-127-127-106-632-136-361-132-623-103-106-213-261-162-136-103-130-615-103-130-103-167-136-132-271-612-126-163-136-103-167-561-612-167-160-163-613-103-1_V.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 125-326-167-312-325-256-527-672-652-657-567-567-572-756-256-265-567-625-652-567-657-652-657-576-765-576-765-765-637-637-673-675-627-576-657-765-265-267-527-567-657-657-756-657-5_W.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 762-126-136-316-231-236-263-236-573-576-265-625_W.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 260-206-206-203-160-621-621-621-612-627-621-206-263-230-321-576-756-765-675-623-627-316-106-375-562-673-627-627-263-620-260-206-201-126-102-126-370-176-136-103-612-163-103-163-1_V.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 765-160-165-106-130-106-261-176-167-632-120-130-130 231-167 261-326 312 153 130-217-231_V.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 631-160-310-126-103-157-175-271-167-102-132-507-567-756-657-536-136-132-153-163-413-107-617-167-136-132-123-267-276-261-162-310-632-321-102-130-162-312-163-102-312-162-163-163-1_V.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 326-157-576-571-571-126-137-675 615_V.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 521-127-507-126-132-103-132-103-132-312-132-261-236-261-123-160-267-267-132-163-312-136-167-652-106-136-136-316-256-672-156-650-106-325-367-612-136-756-752-263-526-657-617-610-1_V.mp3)
  Segment 43: 66192ms ~ 66768ms (file: 136-601-320-301-302-307-123-103-132-106-162-126-163-136-132-302-231-360-123-102-126-132-162-213-312-123-213-123-103-321-327-756-762-126-163-213-201-752-726-672-627-620-263-261-6_V.mp3)
  Segment 44: 67768ms ~ 68344ms (file: 756 574 136-132 765-756-765-750 753-756-756-765-765_W.mp3)
  Segment 45: 69344ms ~ 69920ms (file: 547-567 136-127-162 127 765-765-763-765-763-576-756_W.mp3)
  Segment 46: 70920ms ~ 71496ms (file: 107-175-210-206-547-570-126-312-103-132-123-175-705-756-275-312-302-173-175-137-504-162-132-176-123-301-571-157-157-154-132-103-176-136-132-216-173-610-316-756-754-576-601-126-1_V.mp3)
  Segment 47: 72496ms ~ 73072ms (file: 521-132-316-317-167-631-310-132-316-352-136-301-362-320-203-703-716-165-106-106-103-106-160-106-301-103-301-132-103-130-750-716-576-176-716-317-321-326-325-713-371-726-506-513-6_W.mp3)
  Segment 48: 74072ms ~ 74648ms (file: 761-765-652-675-765 163 236-263-261-267-623-621-657-627-726-627-236-625-652-756-675-765-652-627-621-623-267-635-261-267-617-675-672-627-671-617-162-627-263-265-263-263-673-256-6_W.mp3)
  Segment 49: 75648ms ~ 76224ms (file: 574-261-123-562-564-527-675-132-762-275-126-672-137-217-316-631-215-312-236-523-267-361-136-136-103-350-351-135-130-137-617-157-103-103-107-170-170-170-176-137-132 137-312-317-3_W.mp3)
  Segment 50: 77224ms ~ 77800ms (file: 130-312-361-621-106-102-103-203-231-174-713-741-675-167-671-137-176-301-103-174-571-203-175-276-132-301-260-620-326-320-230-302-301-163-106-137-136-301-213-321-263-620-362-574-7_V.mp3)
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
> **Time**: 2026-04-03 07:40:20
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
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 60, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "점심은", "start_ms": 180, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 320, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "먹을까요?", "start_ms": 380, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "I feel like having some pasta.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 40, "dur_ms": 50, "phase_lr_deg": -5.0},
{"word": "feel", "start_ms": 100, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "like", "start_ms": 210, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "having", "start_ms": 300, "dur_ms": 110, "phase_lr_deg": -5.0},
{"word": "some", "start_ms": 420, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "pasta.", "start_ms": 490, "dur_ms": 80, "phase_lr_deg": -5.0}
]
},
{
"segment": 3,
"transcript": "파스타 좋네요. 근처에 맛있는 곳 있나요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "파스타", "start_ms": 50, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "좋네요.", "start_ms": 190, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "근처에", "start_ms": 310, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "맛있는", "start_ms": 420, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "곳", "start_ms": 510, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "있나요?", "start_ms": 550, "dur_ms": 20, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "There is a new Italian place around the corner.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "There", "start_ms": 45, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "is", "start_ms": 110, "dur_ms": 40, "phase_lr_deg": -5.0},
{"word": "a", "start_ms": 160, "dur_ms": 20, "phase_lr_deg": -5.0},
{"word": "new", "start_ms": 190, "dur_ms": 70, "phase_lr_deg": -5.0},
{"word": "Italian", "start_ms": 270, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "place", "start_ms": 400, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "around", "start_ms": 490, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "the", "start_ms": 560, "dur_ms": 10, "phase_lr_deg": -5.0},
{"word": "corner.", "start_ms": 580, "dur_ms": -4, "phase_lr_deg": -5.0}
]
},
{
"segment": 5,
"transcript": "아, 거기 새로 생겼나 보네요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "아,", "start_ms": 70, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "거기", "start_ms": 160, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "새로", "start_ms": 270, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "생겼나", "start_ms": 370, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "보네요.", "start_ms": 500, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "Let's check it out.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Let's", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "check", "start_ms": 160, "dur_ms": 110, "phase_lr_deg": -5.0},
{"word": "it", "start_ms": 280, "dur_ms": 40, "phase_lr_deg": -5.0},
{"word": "out.", "start_ms": 330, "dur_ms": 240, "phase_lr_deg": -5.0}
]
},
{
"segment": 7,
"transcript": "네, 가 봅시다. 지금 가면 자리 있겠죠?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "가", "start_ms": 110, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "봅시다.", "start_ms": 160, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 280, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "가면", "start_ms": 380, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "자리", "start_ms": 480, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "있겠죠?", "start_ms": 570, "dur_ms": 6, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "It's still early, so it should be fine.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "It's", "start_ms": 30, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "still", "start_ms": 100, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "early,", "start_ms": 210, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "so", "start_ms": 340, "dur_ms": 40, "phase_lr_deg": -5.0},
{"word": "it", "start_ms": 390, "dur_ms": 40, "phase_lr_deg": -5.0},
{"word": "should", "start_ms": 440, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "be", "start_ms": 530, "dur_ms": 40, "phase_lr_deg": -5.0},
{"word": "fine.", "start_ms": 580, "dur_ms": -4, "phase_lr_deg": -5.0}
]
},
{
"segment": 9,
"transcript": "다행이네요. 빨리 가요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "다행이네요.", "start_ms": 60, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "빨리", "start_ms": 280, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "가요.", "start_ms": 400, "dur_ms": 170, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "What's your favorite type of pasta?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "What's", "start_ms": 40, "dur_ms": 70, "phase_lr_deg": -5.0},
{"word": "your", "start_ms": 120, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "favorite", "start_ms": 190, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "type", "start_ms": 320, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "of", "start_ms": 410, "dur_ms": 40, "phase_lr_deg": -5.0},
{"word": "pasta?", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": -5.0}
]
},
{
"segment": 11,
"transcript": "저는 크림 파스타를 제일 좋아해요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "크림", "start_ms": 170, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "파스타를", "start_ms": 270, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "제일", "start_ms": 420, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "좋아해요.", "start_ms": 510, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "I prefer tomato based sauce.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 55, "dur_ms": 50, "phase_lr_deg": -5.0},
{"word": "prefer", "start_ms": 115, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "tomato", "start_ms": 245, "dur_ms": 130, "phase_lr_deg": -5.0},
{"word": "based", "start_ms": 385, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "sauce.", "start_ms": 475, "dur_ms": 100, "phase_lr_deg": -5.0}
]
},
{
"segment": 13,
"transcript": "그렇군요. 그럼 메뉴 두 개 시켜서 나눠 먹을까요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "그렇군요.", "start_ms": 40, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "그럼", "start_ms": 180, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "메뉴", "start_ms": 270, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "두", "start_ms": 360, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "개", "start_ms": 410, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "시켜서", "start_ms": 450, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "나눠", "start_ms": 560, "dur_ms": 10, "phase_lr_deg": 0.0},
{"word": "먹을까요?", "start_ms": 580, "dur_ms": -4, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "That's a great idea! Let's do that.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "That's", "start_ms": 35, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "a", "start_ms": 145, "dur_ms": 30, "phase_lr_deg": -5.0},
{"word": "great", "start_ms": 185, "dur_ms": 90, "phase_lr_deg": -5.0},
{"word": "idea!", "start_ms": 285, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "Let's", "start_ms": 415, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "do", "start_ms": 505, "dur_ms": 40, "phase_lr_deg": -5.0},
{"word": "that.", "start_ms": 555, "dur_ms": 20, "phase_lr_deg": -5.0}
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
"transcript": "가게 분위기가 정말 좋네요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "가게", "start_ms": 100, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "분위기가", "start_ms": 220, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 380, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "좋네요.", "start_ms": 500, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "Yeah, I like the lighting here.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "Yeah,", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "I", "start_ms": 155, "dur_ms": 40, "phase_lr_deg": -5.0},
{"word": "like", "start_ms": 205, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "the", "start_ms": 295, "dur_ms": 40, "phase_lr_deg": -5.0},
{"word": "lighting", "start_ms": 345, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "here.", "start_ms": 505, "dur_ms": 70, "phase_lr_deg": -5.0}
]
},
{
"segment": 18,
"transcript": "메뉴판 좀 볼까요? 여기 해산물 파스타도 있네요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "메뉴판", "start_ms": 30, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 160, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "볼까요?", "start_ms": 210, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "여기", "start_ms": 330, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "해산물", "start_ms": 420, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "파스타도", "start_ms": 530, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "있네요.", "start_ms": 580, "dur_ms": -4, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "Oh, that sounds good too.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Oh,", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "that", "start_ms": 140, "dur_ms": 90, "phase_lr_deg": -5.0},
{"word": "sounds", "start_ms": 240, "dur_ms": 110, "phase_lr_deg": -5.0},
{"word": "good", "start_ms": 360, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "too.", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": -5.0}
]
},
{
"segment": 20,
"transcript": "결정하기 힘드네요. 다 맛있어 보여요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "결정하기", "start_ms": 60, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "힘드네요.", "start_ms": 250, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 390, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "맛있어", "start_ms": 440, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "보여요.", "start_ms": 540, "dur_ms": 30, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "Let's just pick two and share.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Let's", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "just", "start_ms": 150, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "pick", "start_ms": 240, "dur_ms": 90, "phase_lr_deg": -5.0},
{"word": "two", "start_ms": 340, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "and", "start_ms": 450, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "share.", "start_ms": 520, "dur_ms": 50, "phase_lr_deg": -5.0}
]
},
{
"segment": 22,
"transcript": "좋아요. 저는 까르보나라로 할게요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "좋아요.", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "저는", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "까르보나라로", "start_ms": 290, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "할게요.", "start_ms": 480, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "And I'll take the seafood tomato pasta.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "And", "start_ms": 35, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "I'll", "start_ms": 105, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "take", "start_ms": 175, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "the", "start_ms": 265, "dur_ms": 40, "phase_lr_deg": -5.0},
{"word": "seafood", "start_ms": 315, "dur_ms": 110, "phase_lr_deg": -5.0},
{"word": "tomato", "start_ms": 435, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "pasta.", "start_ms": 545, "dur_ms": 30, "phase_lr_deg": -5.0}
]
},
{
"segment": 24,
"transcript": "완벽해요. 음료는 뭐 마실까요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "완벽해요.", "start_ms": 45, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "음료는", "start_ms": 240, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 380, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "마실까요?", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "Just some water for me.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "Just", "start_ms": 60, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "some", "start_ms": 170, "dur_ms": 70, "phase_lr_deg": -5.0},
{"word": "water", "start_ms": 250, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "for", "start_ms": 380, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "me.", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": -5.0}
]
},
{
"segment": 26,
"transcript": "저도 물이면 충분해요. 주문할게요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "저도", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "물이면", "start_ms": 150, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "충분해요.", "start_ms": 270, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "주문할게요.", "start_ms": 410, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "Wait, let's add some garlic bread.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Wait,", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": -5.0},
{"word": "let's", "start_ms": 170, "dur_ms": 90, "phase_lr_deg": -5.0},
{"word": "add", "start_ms": 270, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "some", "start_ms": 360, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "garlic", "start_ms": 430, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "bread.", "start_ms": 540, "dur_ms": 30, "phase_lr_deg": -5.0}
]
},
{
"segment": 28,
"transcript": "오, 마늘빵 좋죠! 그것도 같이 주문할게요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "오,", "start_ms": 30, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "마늘빵", "start_ms": 110, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "좋죠!", "start_ms": 250, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "그것도", "start_ms": 340, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 460, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "주문할게요.", "start_ms": 540, "dur_ms": 30, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "I can't wait to taste it.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 60, "dur_ms": 50, "phase_lr_deg": -5.0},
{"word": "can't", "start_ms": 120, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "wait", "start_ms": 230, "dur_ms": 110, "phase_lr_deg": -5.0},
{"word": "to", "start_ms": 350, "dur_ms": 40, "phase_lr_deg": -5.0},
{"word": "taste", "start_ms": 400, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "it.", "start_ms": 530, "dur_ms": 40, "phase_lr_deg": -5.0}
]
},
{
"segment": 30,
"transcript": "저도요. 금방 나오겠죠?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "저도요.", "start_ms": 40, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "금방", "start_ms": 180, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "나오겠죠?", "start_ms": 300, "dur_ms": 270, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "Probably in 10 minutes.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "Probably", "start_ms": 120, "dur_ms": 180, "phase_lr_deg": -5.0},
{"word": "in", "start_ms": 310, "dur_ms": 40, "phase_lr_deg": -5.0},
{"word": "10", "start_ms": 360, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "minutes.", "start_ms": 450, "dur_ms": 120, "phase_lr_deg": -5.0}
]
},
{
"segment": 32,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 33,
"transcript": "음식 나왔습니다. 맛있게 드세요.",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "음식", "start_ms": 50, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "나왔습니다.", "start_ms": 190, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "맛있게", "start_ms": 380, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "드세요.", "start_ms": 500, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 35,
"transcript": "우와, 정말 맛있어 보이네요!",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "우와,", "start_ms": 60, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 180, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "맛있어", "start_ms": 300, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "보이네요!", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 37,
"transcript": "It looks amazing.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "It", "start_ms": 55, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "looks", "start_ms": 125, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "amazing.", "start_ms": 255, "dur_ms": 320, "phase_lr_deg": -5.0}
]
},
{
"segment": 38,
"transcript": "먼저 마늘빵부터 먹어 볼까요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "먼저", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "마늘빵부터", "start_ms": 160, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "먹어", "start_ms": 390, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "볼까요?", "start_ms": 480, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "Sure, it smells so garlicky.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "Sure,", "start_ms": 35, "dur_ms": 110, "phase_lr_deg": -5.0},
{"word": "it", "start_ms": 155, "dur_ms": 40, "phase_lr_deg": -5.0},
{"word": "smells", "start_ms": 205, "dur_ms": 130, "phase_lr_deg": -5.0},
{"word": "so", "start_ms": 345, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "garlicky.", "start_ms": 435, "dur_ms": 140, "phase_lr_deg": -5.0}
]
},
{
"segment": 40,
"transcript": "바삭하고 맛있네요. 까르보나라도 먹어 봐요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "바삭하고", "start_ms": 45, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "맛있네요.", "start_ms": 215, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "까르보나라도", "start_ms": 365, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "먹어", "start_ms": 505, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "봐요.", "start_ms": 575, "dur_ms": 1, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "Oh, this is very creamy.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "Oh,", "start_ms": 60, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "this", "start_ms": 150, "dur_ms": 90, "phase_lr_deg": -5.0},
{"word": "is", "start_ms": 250, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "very", "start_ms": 320, "dur_ms": 110, "phase_lr_deg": -5.0},
{"word": "creamy.", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": -5.0}
]
},
{
"segment": 42,
"transcript": "입맛에 맞으셔서 다행이에요. 해산물 파스타는 어때요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "입맛에", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "맞으셔서", "start_ms": 160, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "다행이에요.", "start_ms": 310, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "해산물", "start_ms": 450, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "파스타는", "start_ms": 540, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "어때요?", "start_ms": 580, "dur_ms": -4, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "It's slightly spicy, but I like it.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "It's", "start_ms": 35, "dur_ms": 70, "phase_lr_deg": -5.0},
{"word": "slightly", "start_ms": 115, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "spicy,", "start_ms": 245, "dur_ms": 130, "phase_lr_deg": -5.0},
{"word": "but", "start_ms": 385, "dur_ms": 70, "phase_lr_deg": -5.0},
{"word": "I", "start_ms": 465, "dur_ms": 40, "phase_lr_deg": -5.0},
{"word": "like", "start_ms": 515, "dur_ms": 50, "phase_lr_deg": -5.0},
{"word": "it.", "start_ms": 575, "dur_ms": 1, "phase_lr_deg": -5.0}
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
"transcript": "저도 한 입 먹어 봐도 될까요?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "저도", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 160, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "입", "start_ms": 210, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "먹어", "start_ms": 270, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "봐도", "start_ms": 370, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "될까요?", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "Of course, help yourself.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "Of", "start_ms": 45, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "course,", "start_ms": 115, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "help", "start_ms": 245, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "yourself.", "start_ms": 355, "dur_ms": 220, "phase_lr_deg": -5.0}
]
},
{
"segment": 47,
"transcript": "와, 진짜 맛있네요. 여기 오길 잘했어요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "와,", "start_ms": 60, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 150, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "맛있네요.", "start_ms": 260, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "여기", "start_ms": 410, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "오길", "start_ms": 490, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "잘했어요.", "start_ms": 560, "dur_ms": 10, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "I'm so glad we came here too.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "I'm", "start_ms": 35, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "so", "start_ms": 125, "dur_ms": 70, "phase_lr_deg": -5.0},
{"word": "glad", "start_ms": 205, "dur_ms": 110, "phase_lr_deg": -5.0},
{"word": "we", "start_ms": 325, "dur_ms": 50, "phase_lr_deg": -5.0},
{"word": "came", "start_ms": 385, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "here", "start_ms": 495, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "too.", "start_ms": 565, "dur_ms": 10, "phase_lr_deg": -5.0}
]
},
{
"segment": 49,
"transcript": "다음에 또 와요. 그때는 다른 메뉴 먹어 봐요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 170, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "와요.", "start_ms": 230, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "그때는", "start_ms": 320, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "다른", "start_ms": 430, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "메뉴", "start_ms": 500, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "먹어", "start_ms": 560, "dur_ms": 10, "phase_lr_deg": 0.0},
{"word": "봐요.", "start_ms": 580, "dur_ms": -4, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "Definitely! Looking forward to it.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "Definitely!", "start_ms": 120, "dur_ms": 210, "phase_lr_deg": -5.0},
{"word": "Looking", "start_ms": 340, "dur_ms": 130, "phase_lr_deg": -5.0},
{"word": "forward", "start_ms": 480, "dur_ms": 90, "phase_lr_deg": -5.0},
{"word": "to", "start_ms": 580, "dur_ms": -4, "phase_lr_deg": -5.0},
{"word": "it.", "start_ms": 580, "dur_ms": -4, "phase_lr_deg": -5.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 07:40:20*
