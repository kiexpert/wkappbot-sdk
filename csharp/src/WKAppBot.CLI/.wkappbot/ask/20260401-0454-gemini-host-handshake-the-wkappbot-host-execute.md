# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=14524  time=04:53:14  uptime=4184s  mem=1970MB  system_processes=737"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260401_045304.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 765-673 671-765-725-725-752-275-765-725 137 213-612-612-672-637-621-125-152-312-203-210-321-203-213-321-253-627-621 236-632-160-706-105-561-106-612-765-167-615-321-576-610-610-6_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 261-261-657-612-275-326-367-257-765-376-675-235-326-326-231-126-612-312-516-531-536-543-657-536-651-361-657-657-356-547-546-540-352-536-571-571-514-547-574-354-354-351-354-365-3_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 326-102-106-620-136-106-210-102-132-136-163-613-617-362-623-263-672-765-675-632-632-316-613-361-601-316-132-625-756-765-106-301-136-135-317-105-517-726-217-231-130-326-267-132-3_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 123-163-126-316-132-103-136-163-126-162-103-106-102-132-106-103-102-103-132-312-126-103-102-102-132 632-621_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 376-601-103-132-106-132 156-267-123-163-675-621-362-156-713-236-172-106-576-136-615-126-103-163-103-512-123-260-217-103-216-361-675-235-136-312 716_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 265-132-123-627-267-261-263-756-273-216_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 761-701-673-107-372-321-132-765-765-675-617-123-127-103-156-125-135-132-126-601-320-632-361-613-136-167-136-672-765-315-315-362-327-235-376-571-752-125-231-125-135-612-316-316-3_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 610-106-160-107-107-613-731-106-607-620-602-620-675-765-765-206-203-302-630-103-106 207-206-576-756-627-602-267-263-632-301 173-132-167-765-174-175-126-675-765-675-671-163-613-1_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 236-263-236-267-257-253-725-572-523-325-327-327-235-231-321-132-321-312-152-273-237-623-267-276-237-275-217-732-537-265-275-257-275-752-572-576-576-756-725-275-572-576-574-572-6_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 103-136-103-103-106-163-310-130-132-316-103-170-103-167-315-103-163-130-361-137-160-261-316-106-103-751-375-350-375-375-143-132 620-602-326-106-130-623-362-362-163-136-167-670-6_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 374 621-623-627-570-157-617-267-625-625-632-312-567-762-765-675-276-617-267-267-750-302-317-753-673-627-607-672-637-673-603-627-265-267-236_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 675-526-306-576-321-613-675-107-150-153-107-126-125-675-675-657-231-236-123-612-153-130-102-760-160-127-126-132-132-132-126-132-136-103-132-102-160-621-163-601-102-576-762-126-1_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 132-756-570-310-302-312-321-327-273-321-231-230-236-231-321-573-574-576-754-576-567-657-657-236-236-236-517-576-567-547-547-543-564-547-312-321-231-261-326-362-236-132-134-320-2_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 136-261-361-130-136-163-167_V.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 123-236-263-265-672-627-236-651-637-627-637-362-312-326-231-126-326-236-231-236-263-216-321-326-132-213-213-326-236-216-231-236-326-321-236-326-623-623-261-236-231-231-213-236-3_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 623-267-102-213-130-132-361-312-135-315-312-231-231-130-213-326-623-362-276-637-236-213-263-627-126-310-321-576-706-620-627-620-620-267-216-513-765-751-673-263-326-236-576-572-7_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 130-302-310-206-201-365-567-132-106-367-316-362-361-675-617-136-167-136-765-675-620-610-632-657-657-567-675-167-162-167-765-657-576-765-756-675-657-576-756-765-576-675-675 136-3_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 126-132-123-102-132-360-106-123-327-623-130-136-137-623-362-132-127-130-607-137-572-136-103-176-765-167-136-610-106-103-156-120-103-163-263-260-261-123-673-706 216 167-103-102-1_V.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 132-132 672-763-671-671-617-163-164-367-302-162-623-621-610-632-216-263-632-613-621-621-623-672-672-267-672-627-620-627-627-627-625-627-627-672-267-267-623-623-672-672-267-632-6_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 103-163-623-316-157-126-137-123-132-671-103-162-136-103-136-103-106-106-103-106-610-126-316-167-216-106-173-132-621-167-167-103-316-103-321-102-613-610-671_V.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 602-601-601-601-360-301-301-236-326-356-532-657-756-732-610-620-762-672-672-263-126-126-162-106-106-106-126-132-106-106-163-162-162-126-263-627-625-267-265-216-123-102-103-130-3_W.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 765-765-765-726-627-675-672-627-263-315-567-356-261-312-260-206-263-316-132-301-301-306-103-310-103-261-362-261-231-162-106-751-765-372-301-103-132-132-206-260-201-231-135-135-1_V.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 126-157-132-103-312-312-136-316-136-167-325-237-361-367-237-367-675-675-675-670-763-675-670-607-765-670-670-675-671-362-312-301-316-361-613-106-316-103-657-163-160-103-163-631-6_V.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 627-267-267-267-267-267-627-625-672-762 135-352-521-261-236-615-367-672-316-326-325-213-132-312-163-316-312-130-236-236-260-326-261-216-231-163-617-623-267-267-267-627-326-321-3_W.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 654-267-561-126-126-103-103-312-617-167-102-103-136-106-103-310-167-261-162-126-162-162-103-610-316-310-261-312-726-627-621-102-615-167-132-625-136-617-167-162-132-613-156-132-1_V.mp3)
  Segment 26: 39400ms ~ 41380ms (file: 162-627-621-216-612-321-231-261-132-216-213-312-261-631-362-362-132-316-672-675-726-672-261-163-236-126-162-612-126-106-106-123-126 607-671-675-132-136-756-756-156-150-132-103 2_W.mp3)
  Segment 27: 42380ms ~ 42956ms (file: 627-612-621 175-726-175-562-172-137-326-306-306-362-230-236 756-765-765-765-670-672-576-756-756-750-136 760-675-765-675-672-260-620-762-167-261-261-260-376-267-132-236-261-716-1_W.mp3)
  Segment 28: 43956ms ~ 44532ms (file: 123-103-316-130-321-132-301-312-132-162-627-701-721-172-701-762-316-612 261-123 162-673-103-230-216-106-160-216-123-621-167-126-162 126-317-162-120-103-136-126-103-123-317-102-1_V.mp3)
  Segment 29: 45532ms ~ 46108ms (file: 312-103-327-627-603-216-326-613-560-671-132-612-671-160-154-527-162-261-165-160-215-102 576-574-271-236-612-261-216-621-562-526-125-123-321-213-260-620-632-236-102-126-163-136-1_V.mp3)
  Segment 30: 47108ms ~ 47684ms (file: 310-130-321-310-361-321-132 267-267-517_W.mp3)
  Segment 31: 48684ms ~ 49260ms (file: 267-267-637-367-736-763-763-376-576-567-567-567-567-756-265-261-261-216-201-257-321-316-316-123-162-213-213-123-127-126-216-126-126-123-127-126-123-152-516-215-312-327-312-315-5_W.mp3)
  Segment 32: 50260ms ~ 50836ms (file: 162-621-260-206-752-627-273-136-105-305-503-302-206-250-231-362-230-230-725-327-106-106-613-567-765-103-310-760-136-105-230-302-320-320-320-230-560-570-503-623_W.mp3)
  Segment 33: 51836ms ~ 52412ms (file: 163-136-126-261-216-103-102-213-162-213-123-216-236-123-136-126-132-102-130-120-126-321-123-312_V.mp3)
  Segment 34: 53412ms ~ 53988ms (file: 567-531 130-613-162-765-675-672-675-637-672-670-675-762-627-765-675-706-670-670-670-620-672-607-627-602-623-672-267-126-126-620-612-623-261-613_W.mp3)
  Segment 35: 54988ms ~ 55564ms (file: 756-765-756-756-765-756-765-765-765-762-762-765-762-716-716-276-276-725-276-612-236-123-123-206-762-567-526-572-572 613-156-671-627 127-167-671-631-317-236-612-216 103-136-103-1_W.mp3)
  Segment 36: 56564ms ~ 57140ms (file: 315-203-260-136-163-106-310-102-106-612-106-612-316-601-612-631-612-326-263-250-267-263-326-360-361-326-230-263-103-106-326-312-130-301-130-312-310-103-103-621-261-621-253-326-3_V.mp3)
  Segment 37: 58140ms ~ 58716ms (file: 715-471-312-143-123-175-312 751-572-623-627-672-163-316-316-235-241-627-627-623-672-613-263-130-627-620-261-260-621-106-615-321-326-321-231-362-361-617-162-136-315-312-301-236-1_V.mp3)
  Segment 38: 59716ms ~ 60292ms (file: 176-617-765-761-670-621-613-613-601-106-316-715-316-627-106-756-756-576-176-567-136-716-671-761-103-132-163-756-165-126-165-562-562-526-625-256-263-213-321-675-675-276-267-267-6_W.mp3)
  Segment 39: 61292ms ~ 61868ms (file: 123-261-203-206-206-263-213-206 263-260-260-310-301-103-103-132-103-123-103-130-103-103-130-130-132-103-136-132 106-106-106-160-103-106-163-106-762-762_V.mp3)
  Segment 40: 62868ms ~ 63444ms (file: 263-361-623-213-621-126-126-126-257-267-326-326-236-236-236-326-312-623-361-163-631-312-621-312-326-362-327-574-756-735-317-321-326-326-326-321-321-326-213-213-123-236-213-236-2_W.mp3)
  Segment 41: 64444ms ~ 65020ms (file: 362-632-123-132-123-715-132-136-572-652-567-615-106-213-576-756-231-103-132-263-673 263-261-172-627-127-165-160-123-176-627-136-351-135-167-176-607-723-127-317-135-137-361-361-1_W.mp3)
  Segment 42: 66020ms ~ 66596ms (file: 627-623-627-623-625-621-612-623-265-625-627-267-261-216-612-623-615-657-675-657-625-675-625-632-625-635-672-675-672-671-752-672-763-672-576-675-657-625-627-625-762-652-672-625-6_W.mp3)
  Segment 43: 67596ms ~ 69612ms (file: 763-627-756-576-567-123-125-657-561-563-657-675-675-102-120-657-675-576-657-657-157-263-632-275-165-753-317-317-376-756-705-756-751-715-371-631-136-156-517-157-157-615-165-617-6_W_stt_the_love_the.mp3)
  Segment 44: 70612ms ~ 71188ms (file: 657-657-671-607-671-627-627-271-576-567-175-261 672-726-612-362 123-132-126-162-623-623-216-356-321-231-102-203-213-123-217-623-627-675-761-672-267-765-671-675-651-601-261-621-1_V.mp3)
  Segment 45: 72188ms ~ 72764ms (file: 750-132-103-715-107-371-531-351-163-106-136-130-310-103-620-123-103-306-136-302-126-163-167-613-163-607-326-360-306-631-361-673-367-365-317-231-130-132-130-132-312-715-760-167-6_V.mp3)
  Segment 46: 73764ms ~ 74340ms (file: 765-260-620-627-620-620-267-260 301-375-731-715-137-625-621-316-650-651-603-260-206-132-103-106-621-160-103-765-672-762-163-107-176-167-671-671-671-617-621-671-162-102-103-623-2_V.mp3)
  Segment 47: 75340ms ~ 75916ms (file: 163-612-765-605-765-627-617-621-675-312-312-302-375-765-576-620-102-123-627-263-126-316-123-126-671-617-672-672-706-203-623 617-627-623-316-613-163-310-301-126 675-672-630-360-6_W.mp3)
  Segment 48: 76916ms ~ 77492ms (file: 207-267-260-260-260-620-267-607-617-750-756-675-567-513-706-103-136-102-162-761-620-103-310-170-103-136-503-370-753-761-673-761-765-765 236-520-547-301-301-132-102-126-162-106-1_W.mp3)
  Segment 49: 78492ms ~ 79068ms (file: 672 275-236-236-326-237-617 217-312-316-167-567-236-135-176-361-236-672_V.mp3)
  Segment 50: 80068ms ~ 80644ms (file: 126-130-162-163-162-160-160-163-126-276-612-162-130-160-315-160-156-103-103-763-130-136-136-130-130-263-263-261-123-102-517-132 162-361-675-675-316-621 675-627 326-321 673-672-1_V.mp3)
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
> **Time**: 2026-04-01 04:54:30
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "오늘 배울 표현은 쇼핑할 때 유용한 문장들입니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 45, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "배울", "start_ms": 170, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "표현은", "start_ms": 285, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "쇼핑할", "start_ms": 430, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "이거 얼마예요?",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "이거", "start_ms": 30, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "얼마예요?", "start_ms": 220, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "How much is this?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "How", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "much", "start_ms": 160, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 290, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "this?", "start_ms": 380, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "좀 깎아 주실 수 있나요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "좀", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "깎아", "start_ms": 150, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "주실", "start_ms": 300, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 410, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "있나요?", "start_ms": 460, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "Can I get a discount?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "Can", "start_ms": 35, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 125, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "get", "start_ms": 195, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 305, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "discount?", "start_ms": 355, "dur_ms": 180, "phase_lr_deg": 0.0}
]
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
"transcript": "다른 색상도 있나요?",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "다른", "start_ms": 45, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "색상도", "start_ms": 175, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "있나요?", "start_ms": 345, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "Do you have this in other colors?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Do", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 110, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "have", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "this", "start_ms": 310, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 10,
"transcript": "입어 봐도 될까요?",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "입어", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "봐도", "start_ms": 200, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "될까요?", "start_ms": 330, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "May I try this on?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "May", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 130, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "try", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "this", "start_ms": 310, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "on?", "start_ms": 400, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "탈의실은 어디인가요?",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "탈의실은", "start_ms": 35, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "어디인가요?", "start_ms": 265, "dur_ms": 260, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "Where is the fitting room?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "Where", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 165, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 255, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "fitting", "start_ms": 325, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "room?", "start_ms": 465, "dur_ms": 100, "phase_lr_deg": 0.0}
]
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
"transcript": "이걸로 할게요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "이걸로", "start_ms": 30, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "할게요", "start_ms": 220, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "I'll take this one.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "I'll", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "take", "start_ms": 140, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "this", "start_ms": 260, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "one.", "start_ms": 360, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "카드 결제 가능한가요?",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "카드", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "결제", "start_ms": 190, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "가능한가요?", "start_ms": 320, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "Do you accept credit cards?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "Do", "start_ms": 45, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 115, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "accept", "start_ms": 205, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "credit", "start_ms": 335, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "cards?", "start_ms": 445, "dur_ms": 110, "phase_lr_deg": 0.0}
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
"transcript": "영수증 주시겠어요?",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "영수증", "start_ms": 30, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "주시겠어요?", "start_ms": 240, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "Could I have a receipt, please?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Could", "start_ms": 40, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 140, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "have", "start_ms": 210, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 300, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "receipt,", "start_ms": 350, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "봉투 필요하세요?",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "봉투", "start_ms": 35, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "필요하세요?", "start_ms": 205, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "Would you like a bag?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Would", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "like", "start_ms": 250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 360, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "bag?", "start_ms": 410, "dur_ms": 120, "phase_lr_deg": 0.0}
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
"transcript": "네, 봉투 하나 주세요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "봉투", "start_ms": 120, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "하나", "start_ms": 250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 360, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "Yes, please. One bag.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Yes,", "start_ms": 40, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "please.", "start_ms": 140, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "One", "start_ms": 280, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "bag.", "start_ms": 370, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "환불하고 싶어요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "환불하고", "start_ms": 35, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "싶어요", "start_ms": 285, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "I'd like a refund.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "I'd", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "like", "start_ms": 140, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 250, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "refund.", "start_ms": 300, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "교환 가능한가요?",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "교환", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "가능한가요?", "start_ms": 170, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "Can I exchange this?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "Can", "start_ms": 45, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 135, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "exchange", "start_ms": 205, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "this?", "start_ms": 365, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 32,
"transcript": "좋은 하루 보내세요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 30, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "하루", "start_ms": 180, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "보내세요", "start_ms": 350, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "Have a nice day!",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Have", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 130, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "nice", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "day!", "start_ms": 290, "dur_ms": 140, "phase_lr_deg": 0.0}
]
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
"transcript": "이제 복습해 보겠습니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "복습해", "start_ms": 165, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "보겠습니다", "start_ms": 355, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "따라해 보세요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "따라해", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "보세요", "start_ms": 240, "dur_ms": 260, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "이거 얼마예요?",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "이거", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "얼마예요?", "start_ms": 190, "dur_ms": 320, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "How much is this?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "How", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "much", "start_ms": 155, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 285, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "this?", "start_ms": 375, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "좀 깎아 주세요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "좀", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "깎아", "start_ms": 140, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 310, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "Can I get a discount?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Can", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 130, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "get", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 310, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "discount?", "start_ms": 360, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "다른 색은 없나요?",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "다른", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "색은", "start_ms": 165, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "없나요?", "start_ms": 315, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "Any other colors?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Any", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "other", "start_ms": 160, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "colors?", "start_ms": 280, "dur_ms": 160, "phase_lr_deg": 0.0}
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
"transcript": "이거 입어 봐도 될까요?",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "이거", "start_ms": 30, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "입어", "start_ms": 180, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "봐도", "start_ms": 310, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "될까요?", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "Can I try this on?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Can", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 130, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "try", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "this", "start_ms": 310, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "on?", "start_ms": 400, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "탈의실은 저기 있습니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "탈의실은", "start_ms": 35, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "저기", "start_ms": 265, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 415, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "The fitting room is over there.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "The", "start_ms": 45, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "fitting", "start_ms": 115, "dur_ms": 140, "phase_lr_deg": 0.0}
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
"transcript": "오늘 고생 많으셨습니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 30, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "고생", "start_ms": 160, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "많으셨습니다", "start_ms": 310, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "수고하셨습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "수고", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "하셨습니다", "start_ms": 250, "dur_ms": 280, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-01 04:54:30*
