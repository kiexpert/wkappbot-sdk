# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=14524  time=04:50:39  uptime=4029s  mem=2523MB  system_processes=730"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260401_044819.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 567-540-547-257-517-572-725-275-257-567-253-275-751-576-572-570-574-754-523-253-250_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 256-231-236-263-267-651-621-625-621-265-356-263-261-126-123-216-316-136-672-251-265-526-625-312-513-513-253-632-235-265-625-256-615-216-126-627-672-612-615-652-672-672-627-564-5_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 103-136-136-132-106-162-136-156-761-165-136-160-160-103-136-103-156-123-106-312-316-163-136-136-671-130-162-136-106-167-601-136-130-160-156-103-106-165-621-610-167-130-126-130-3_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 103-132-607-601-317-301-302-371-317-317-136-301-316-361-107-106-617-610-160-613-617-167-316-136-163-314-613-316-316-623-362-362-361-520-672-627-673-627-673-607-602-620-607-672-6_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 162-106-731-130-716-610-326-261-206-102-103-312-625-617-103-103-261-103-163-157-167-167-163-103-136-175-163-106-162-103-136-103-127-162-107-106-136-106-103-106-103-103-762-761-1_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 253-265-623 524-652-756-765-760-576-103-321-163-316-126-362-162-316-316-317-361-361-136-362-103-136-123-216-167-617-167-367-567-576-576-576-675-627-103-370-701-371-320-143-167-7_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 756-715-765-760-760-762-756-756-756-763-763-765-765-706-765-573-765-675-671-765-657-765-672-675-657-671-670-673-674-765-761-765-761-675-672-106-106-103-162-765-765-761-761-756-7_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 547-312-756-731-360-260-620-162-602-607-672-612-123-236-762-627-672-263-265-625-267-263-675-160-413-213-576-576-756-570-750-705-754-751-753-765-765-627-163-576-756-756-275-627-1_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 637-156-362-632-256-627-267-652-567-356-365-657-627-652-625-625-652-756-635-627-617-675-617-675-631-653-367-367-637-673-367-567-675-675-657-617-615-615-675-367-367-167-637-167-6_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 613-350-135-130-106-176-163-163-602-261-123-162-136-326-326-360-326-120-312-312-301-107-163-103-301-657-206-203-542-540-547-547-260-236-132 312-310-301-302-302-302-310-301-301-2_V.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 136-754-136-103-130-132-163-132-627-361-567-527-316-213-627-267-213-132-231-302-236-602-630-136-326-102-103-310-213-103-750-603-603-630-103-657-503-312-103-106-106-132-612-216-5_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 152-136-617-610-136-132-132-162-216-631-326-362-612-613-136-316-526-362-321-321-231-316-367-326-326-326-326-236-351-356-316-326-362-574-265-625-326-653-561-563-632-362-326-136-1_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 176-157-156-157-156-516-156-157-123-156-165-136-135-137-137-132-125-125-135-135-135-315-135-315-135-135-351-357-537-153-135-213-132-156-132-156-153-156-153-153-135-512-125-132-1_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 762-657-567-572-571-567-351-136-135-153-567-537-572-256-265-572-526-256-562-562-257-571-576-576-257-267-257-657-265-651-523-571-576-572-574-574-576-574-627-527-576-576-756-351-3_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 123-106-165-620-162-126-126-216-162-160_V.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 623-137-362-267-267-627-627-627-236-267-267-267-265-625-627-236-652-321-621-265-236-267-267-276-574-574-576-576-574-576-562-543-517-152-751-236-367-362-326-132-532-572-325-327-3_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 301-312-326-316-320-263-326-265-621_V.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 123-326-167-617-123-160-106-361-126-130-230-236-263-236-162-103-162-103-160-213-162-301-120-160-613-167-136-103-715-716-160-136-627-621 103-136 162-761-103-106-631-167-103-612-1_V.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 620-623-621-752-750-126-103-301-137-573-107-172-167-260-260-206-756-750-765-276-750-756-672-627-627-623-173-132-310-312-302-173-217-103-267-620-261-316-301-301 631-576-754-162-6_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 261-216-216-216-216-216-210-216-263-261-215-523-526-216-261-126-162-126-123-312-132-213-123-123-213-123-213-231-523-563-572-265-213-126-126-213-216-210-213-312-321-231-321-231-2_W.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 756-765-762-267-267-620-617-167-106-312-670-672-625-123-612-602-672-163-176-176-301-316-301-301-761-637-627-162-136-216-301-301-631-612-612-312-136-103-103-103-231-236-231-257-1_V.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 321-632-306-210-231-236-321-567-536-261-263-261-230-321-236-102-326-631-312-321-312-321-326-321-263-321-132-213-321-123-310-106-261-260-123-213-126-216-120-672-627-367-627-236-2_W.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 173-712-126-716-167 263-612-621-216-671-167-361-103-617-617-602-362-163-103-521-210-362-103-576-136-132-627-176-103-163-132-136-162-132-103-167-620 671-675-675_W.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 102-612-657-561-617-326-167-652 132-671-106-613-137-567-167-152-106-167-156-576-132-130_V.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 130-163-136-103-165-362-263-761-106-127-564-256-136-615-160-160-627-612_V.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 321-312-312-672-617-361-361-136-130-532-523-632-321-537 312-315-631-657-672-657-675-613-675-675-675-675-631 312_W.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 120-130-576-756-136-637-536-316-315 570-623-620-325-567-135-675-627-163-376-617 326-236-203 302-320-326-326-126-123 571-765-617-623-263-670-762-762-760-762-760-765-765-765-230-2_V.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 532-236-236-326-532-325-362-372-326-326-361-137-136-623-267-263-231-123-216-362-321-231-236-576-671-671-236-261-236-321-132-371-267-231-236-625-623-263-326-213-256-562-526-526-5_W.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 627-627-625-627-625-625-625-627-625-627-625-627-625-627-562-652-326-623-623-261-216-216-162-261-325-635-613-132-316-261-621-352-672-762-672-627-362-263-326-362-327-273-256-267-7_W.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 130-132-136-136-126-132-103-103-126_V.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 271-136-231-176-760-103-136-176-167-106-163-103-716-162-631-126-671-163-167-137-103-150-261-126-137-130-106-163-103-150-176-612-631-316-312-136-106-320-136-160-163-123-326-231-1_V.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 367-726-756-231-237-126-361-375-157-106-325-325-312-567-726-276-276-271-125-261-253-126-361-261-632-632-312-317-136-137-173 275-257-752-726-726-765-762-725-213-521-275-265-126-2_W.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 576-567-576-576-576-567-567-576-576-567-657-576-576-576-572-576-567-567-576-576-567-657-512-751-576-576-576-765-627-621-653-657-675-265-576-576-567-576-567-576-756-567 130-312-5_W.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 362-165-312-103-761-701-621-165-651-103-761-673-107-106-127-216-625-751-137-103-157-621-175-216-137-103-316-163-617-176-136-601-163-135-560-675-371-136-132-754-765-706-132-321-2_V.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 567-756-357-167-167-675-675-675-657-651-176-163-163-106-167-675-657-671-136-123-136-132-132-312-316-135-136-567-567-326-362-265-632-765-753-756-613-672-612-160-106-167-106-302-3_W.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 132-106-306-715-765-725-270-276-206-213-162-126-351-203-263-132-162-136-163-602-106-136-201-120-102-163-136-123-617-106-605-163-310-103-213-312-130-310-310-316 130-201-103-102-1_W.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 671-163-126-612-160-162-106-163-312-123-103-106-301-563-312-136-631-362-617-673-367-136-260-126-126-675-162-163-162-630-601-360-603-163-612-165-657-651-362-672-617-612-162-165-2_V.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 106-164 137 765-675-671-671-672-673-765-657-567-675-361-326-621-625-102-263-136-261-623-267-672-623-721-625-673_W.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 216-175-145-631-275-145-564-124-513-621-376-365-327-624-267-273-321-367-231-267-243-321-213-216-265-256-172-267-321-216-234-231-216-237-216-265-237-324-326-362-236-237-261-326-3_V.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 315-312-205-312 163-321 276-253-102-371-321-630-326-236-237-327-635-365-317-316-231-176-173-103-165-135-136-376-361-612-361-163-361-136-135-321-206-602-620-260-627-623-213-132-2_W.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 126-326-132-526-126-162-706-756-761 132-123-102-102-103-123-610-617-671-672-613-103-107-623-162-106-103-103-162-621-103-132-103-102-102-760-765-673-216-321-621-672-672-603-260-5_V.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 627-167-672-153-162-671-263-231-123-765-576-673-631-602-126-651-623-362-260 756-317-761-167-615-126-617-625-623-130-167-623-263-263-621-627-762-726-276-261-162-316-213-210-216-2_W.mp3)
  Segment 43: 66192ms ~ 66768ms (file: 623-602-263-623-260-216-672-612-103-136-567-567-627-326-326-310-103-132-153-130-103-136-107-163-106-106-162-103-102-106-103-627-106-136-130-310-315-531-251-276-316-167-361-301-1_V.mp3)
  Segment 44: 67768ms ~ 68344ms (file: 621-627-632-621-613-360-751-316-231-126-162-310-120-203-302-370-321-203-672-320-302-312-301-123-362-763-132-601-136-132-310-362-267-230-126-321-102-213-123-267-670-670-361-136 1_W.mp3)
  Segment 45: 69344ms ~ 69920ms (file: 162-103-130-601-631-612-163-106-162-123-123-132-530-235-126-302-326-261-132-325-657-362-316-326-203-230-301-320-302-132-102-413-132-312-134-413-326-236-310-316-103-102-136-130-1_V.mp3)
  Segment 46: 70920ms ~ 71496ms (file: 130-134-136-317-130-721-126-106-162-176-576-617-165-165-130-671-126-173-607-302-102-657 752-623-126-261 765-312-312-762-172-716-172-723-271-127-172-216-213-572-652-231-132-231-7_W.mp3)
  Segment 47: 72496ms ~ 73072ms (file: 106-175-735-361-631-631-316-312-106-176-756-106-102-103-175-301-306-635-761-165-617-106-623-316-716-106-175-517-651-765-162-675-167-613-715-621-126-657-517-123-106-615-137-261-1_V.mp3)
  Segment 48: 74072ms ~ 74648ms (file: 136-360-312-251-132-163-126-310-167-130-130-103-132-106-160-612-715-130-132-136-652-162-106-750-126-231-156-561-703-702-275-237-723-723-712-726-132 756-613-316-367-163-167-163-1_V.mp3)
  Segment 49: 75648ms ~ 76224ms (file: 150-156-153-156-270-213-312-132-312-162-136-312-631-567-567-563-230-320-321-321-132-230-320-162-136-123-275-257-572-215-162-136-321-302-302-350-532-573-537-137-670-163-126-163-1_W.mp3)
  Segment 50: 77224ms ~ 77800ms (file: 132-632-267-263-261-261-256-627-657-132-126-126-132-123-132-137-352-312-371-673-671-671-671-675-670-675-564-652-652-617-617-321-231-132-231-321-327-132-230-217-762-761-672-762-6_W.mp3)
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
> **Time**: 2026-04-01 04:51:56
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "안녕하세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "안녕", "start_ms": 40, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "하세요", "start_ms": 260, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "만나서 반가워요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "만나서", "start_ms": 30, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "반가워요", "start_ms": 270, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "오늘 날씨가 좋네요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 230, "dur_ms": 170, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "점심 뭐 먹을까요?",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "점심", "start_ms": 60, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 220, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "먹을까요?", "start_ms": 320, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "비빔밥 어때요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "비빔밥", "start_ms": 45, "dur_ms": 260, "phase_lr_deg": 0.0},
{"word": "어때요?", "start_ms": 305, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "좋은 생각이에요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 35, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "생각이에요", "start_ms": 235, "dur_ms": 300, "phase_lr_deg": 0.0}
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
"transcript": "영어로 대화해 볼까요?",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "영어로", "start_ms": 55, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "대화해", "start_ms": 235, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "볼까요?", "start_ms": 395, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "Sure, that sounds great.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Sure,", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "that", "start_ms": 190, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "sounds", "start_ms": 290, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "great.", "start_ms": 430, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "What is your favorite food?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "What", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 130, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "your", "start_ms": 210, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "favorite", "start_ms": 300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "food?", "start_ms": 450, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "I love Korean barbecue.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "love", "start_ms": 130, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "Korean", "start_ms": 250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "barbecue.", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "그거 정말 맛있죠",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "그거", "start_ms": 60, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 200, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "맛있죠", "start_ms": 360, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 14,
"transcript": "음악 소리가 들리네요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "음악", "start_ms": 40, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "소리가", "start_ms": 200, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "들리네요", "start_ms": 380, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "네, 카페 배경음이에요",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "카페", "start_ms": 150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "배경음이에요", "start_ms": 300, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "분위기가 아주 좋네요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "분위기가", "start_ms": 35, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 255, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 375, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "동감이에요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "동감", "start_ms": 40, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "이에요", "start_ms": 280, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "이제 공부를 시작할까요?",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 55, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "공부를", "start_ms": 195, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "시작할까요?", "start_ms": 375, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "좋아요, 책을 펴세요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "좋아요,", "start_ms": 45, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "책을", "start_ms": 225, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "펴세요", "start_ms": 375, "dur_ms": 170, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "몇 페이지인가요?",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "몇", "start_ms": 30, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "페이지", "start_ms": 150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "인가요?", "start_ms": 350, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "사십 이 페이지입니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "사십", "start_ms": 50, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 210, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "페이지", "start_ms": 310, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "입니다", "start_ms": 460, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "네, 찾았습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "찾았습니다", "start_ms": 160, "dur_ms": 380, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "문장을 따라 읽으세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "문장을", "start_ms": 60, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "따라", "start_ms": 240, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "읽으세요", "start_ms": 380, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "천천히 해볼게요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "천천히", "start_ms": 50, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "해볼게요", "start_ms": 290, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "발음이 아주 좋으세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "발음이", "start_ms": 35, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 235, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "좋으세요", "start_ms": 355, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "감사합니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "감사", "start_ms": 30, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "합니다", "start_ms": 270, "dur_ms": 260, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "더 연습해 볼까요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "더", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "연습해", "start_ms": 140, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "볼까요?", "start_ms": 360, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "네, 계속 해주세요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 55, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 175, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "해주세요", "start_ms": 335, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "이 문장은 어때요?",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 45, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "문장은", "start_ms": 125, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "어때요?", "start_ms": 325, "dur_ms": 180, "phase_lr_deg": 0.0}
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
"transcript": "조금 어렵지만 해볼게요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "조금", "start_ms": 50, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "어렵지만", "start_ms": 210, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "해볼게요", "start_ms": 390, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "아주 잘 하셨어요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "아주", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 155, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "하셨어요", "start_ms": 255, "dur_ms": 260, "phase_lr_deg": 0.0}
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
"transcript": "이제 다음 단어입니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 180, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "단어입니다", "start_ms": 340, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "이건 어떻게 읽나요?",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "이건", "start_ms": 60, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 220, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "읽나요?", "start_ms": 360, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "학교 라고 읽습니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "학교", "start_ms": 30, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "라고", "start_ms": 230, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "읽습니다", "start_ms": 350, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "학교, 맞나요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "학교,", "start_ms": 40, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "맞나요?", "start_ms": 260, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "네, 완벽해요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "완벽해요", "start_ms": 170, "dur_ms": 380, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "다른 단어도 해봐요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "다른", "start_ms": 55, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "단어도", "start_ms": 195, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "해봐요", "start_ms": 375, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "이건 사과입니다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "이건", "start_ms": 45, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "사과", "start_ms": 185, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "입니다", "start_ms": 385, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "사과, 맛있겠네요",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "사과,", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "맛있겠네요", "start_ms": 250, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "정말 그래요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 35, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "그래요", "start_ms": 255, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "벌써 시간이 다 됐네요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "벌써", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "시간이", "start_ms": 180, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 340, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "됐네요", "start_ms": 400, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "수고하셨습니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "수고", "start_ms": 30, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "하셨습니다", "start_ms": 270, "dur_ms": 260, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "다음에 또 봐요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 240, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "봐요", "start_ms": 340, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "네, 안녕히 계세요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 60, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 160, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "계세요", "start_ms": 360, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "안녕히 가세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 50, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 290, "dur_ms": 250, "phase_lr_deg": 0.0}
]
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
"transcript": "복습 잊지 마세요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "복습", "start_ms": 55, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "잊지", "start_ms": 255, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "마세요", "start_ms": 375, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "네, 알겠습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 30, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "알겠습니다", "start_ms": 150, "dur_ms": 380, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-01 04:51:56*
