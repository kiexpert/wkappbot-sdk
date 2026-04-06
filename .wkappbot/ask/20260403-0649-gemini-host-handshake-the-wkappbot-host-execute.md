# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=06:48:22  uptime=19747s  mem=1605MB  system_processes=447"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_064812.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 623-627-263-652-516-574-517-536-153-213-263-261-263-210-123-657-625-623-263-260-263-576-652-276-627-672-125-627-625-261-132-753-716-271-675-657-652-650-625-625-625-627-620-263-2_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 657-756-567-657-657-517-576-567-567-675-576-571-216-106-517-570-571-675-265-136-621-201-103-321-652-132-103-106-106-105-132-761-175-126-132-327-321-132-103-135-173-705-617-567-1_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 123-165-125-576-123-132-615-132-132-312-132-132-675-657-564-654-632-657-627-265-625-567-576-567-576-576-564-657-675-567-165-163-167-136-135-156-156-516-176-176-137-153-105-652-2_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 623-132-562-567-162-125-312-617-103-160-631-312-213-137-136-316-613-362-160-316-321-306-316-613-136-312-316-652-306-160-130-132-320-163-106-623-160-362-362-673-612-602-617-163-7_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 175-162-613-275-276-257-275-317-375-675-623-326-312-675-706-670-735-327-237-137-136-321-132-123-102-132-126-172-263-276-765-132-206-235-123-236-312-321-720-203-137-137-102-263-2_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 263-263-326-231-263-236-326-623-623-162-632-362-623-675-673-762-527-576-257-276-632-637-356-126-213-216-213-762-672-621-263-567-256-672-627-623-261-632-216-123-213-123-621-132-2_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 326-726-263-267-263-267-267-267-627-267-267-267-627-267-267-623-236-362-326-623-236-361-613-367-367-361-367-361-367-361-367-361-316-367-371-317-371-367-317-317-534-351-625-321-2_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 132-137-103-136-132-103-132-136_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 570-153-573-572 756 756-731-731 612-162_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 567-157-106-167-165-176-106-516-156-576-562 675-705-310-132-136-613-163-501-540-514-514-162-167-302-301-106-672-263-627-136-136-576-567-615-162-176-236-261-106-201-123-132-135-3_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 162-621-162-106-617-760-103-103-163-316-126-132-216-137-162-103-162-103-123-675-361-123-613-165-312-162-163-136-103-103-623-163-160-167-163-167-132-102-132-107-126-106-167-136-1_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 326-536-365-326-532-352-231-263-263-236-261-263-236-126-321-216-265-572-567-236-236-235-265-526-310-310-326-543-534-352-236-263-267-263-263-236-260-263-231-213-321-325-531-512-2_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 637-562-657-576-107-615-132-360-312-562-540-576-350-501-543-625-263-126-261-312-236-312-362-517-263-623-103-316-317-137-103-130-162-216-126-263-126-326-231-310-310-132-312-102-3_V.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 513-135-315-531-351-357-517-513-157-156-517-571-157-157-157-572-517-517-517-572-157-576-517-517-571-517-576-517-516-157-153-135-153-513-153-135-371-357-537-351-357-351-531-531-3_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 576-751-675-567-576-765-765-765-167-136-136-103-561-206-216-206-206-206-126-326-236-260-620-267-260-261-321-326-231-261-617-673-754-173-317-130-103-106-567-650-630-263-230-326-2_V.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 671-627-263-326-321-312-756 621-617-567-675 143-132 657 562-132-135-671-106-132-672-607-316-162-637-127 123-312-123-132-123-136-162-627-132 126-632-316-312-163-126-136-562-675-6_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 312-267-627-236-236-572-132-705 132-103-761-165-125-325-623-120-216-762-165-615-617-615-610_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 657-621 620-160-612-613 132-103-316-312-136-103-136-132-762-672-762-163-103-312-763-675-376-675-625-657-632_W.mp3)
  Segment 19: 28368ms ~ 42300ms (file: 120-726-167-163-761-163-160-325-163-216-206-312-216-620-162-162-326-631-671-603-206-206-270-201-126-145-105-130-176-671-710-617-251-267-206-267-761-617-103-165-173-617-201-160-1_V.mp3)
  Segment 20: 43300ms ~ 43876ms (file: 576-521-321-352-235-675-672-675-631 612-316-216-312-321-231-632-237-237-237 761-316-136-654-176-106-657-572-576-312-267-765-756-756-576-756-756-576-752-267-126-127-671-167-176-1_W.mp3)
  Segment 21: 44876ms ~ 45452ms (file: 301-530-106-175-162-165-167-153-103-765-632-136-137-136-136-135-361-163-301-103-132-163-623-162_V.mp3)
  Segment 22: 46452ms ~ 47028ms (file: 260-261-261-621-621-623-562-652-625-547-562-265-267-236-261-123-267-236-235-236-263-326-317-135-165-265-125-213-216-135-316-135-256-261-261-126-263-153-135-256-265-216-261-136-3_W.mp3)
  Segment 23: 48028ms ~ 48604ms (file: 617-162-162-102-123-756-765-705-765-361-126-321-132-301-617 163-610-750-560-605-567-156-156-362-263-136-132-163-136-163-763-160-603-136-301-136-301-162-615-130-321-213-103-102-6_W.mp3)
  Segment 24: 49604ms ~ 50180ms (file: 167-576-756-765-765-675-576-576-567-756-756-267-612-675-576-576-160-312-160-761-765-765-671-765-763-675-160-106-701-107-137-106 576-672-362 612-163-167-162-160-216-216-210-106-6_W.mp3)
  Segment 25: 51180ms ~ 51756ms (file: 136-132-103-103-365-361-632-301-132-513-153-123-326-136-136-631-301-231-103-103-102-120-103-130-130-103-132-137-167-316-132-163-567-165-160-107-716-617-672-632-617-671-765-176-6_V.mp3)
  Segment 26: 52756ms ~ 53332ms (file: 176-231-623-321-203-672-652-302-136-352-312-361-126-625-312-103-130-132-312-675-312-302-316-302-130-106-126-230-302-302-562-567-675-137-162-175-675-756-265-136-132-102-307-301-1_W.mp3)
  Segment 27: 54332ms ~ 54908ms (file: 321-321-162-130-312-132-123-327-213-123-162-126-126-261-261-231-302-302-321-163-162-163-671-203-320-301-302-321-321-132-321-312-621-260-623-631-130-312-632-231-316-132-103-120-6_V.mp3)
  Segment 28: 55908ms ~ 56484ms (file: 736 762-762-265-263 623-621-574 321-326-321-267-213-627-162-236-216-261-321-261-231-627-263-276-273-235-325-265-762-762-761-326-216-132-126-163-316-312-301-132-301-130-103-132-1_W.mp3)
  Segment 29: 57484ms ~ 58060ms (file: 276-275-235-257-276-273-273-352-362-327-352-236-623-236-230-623-263-201-256-263-263-265-260-263-236-263-260-260-256-236-265-261-271-261-263-260-261-267-265-256-253-215-263-267-2_W.mp3)
  Segment 30: 59060ms ~ 59636ms (file: 675-675-617-672-627-623-261-263 627-627_W.mp3)
  Segment 31: 60636ms ~ 61212ms (file: 715-705-763-632-761-107-105-162-612-106-106-106-126-162-316-162-105-120-216-165-106-167-162-126-106-107-163-132-123-657-754-753-153-173-137-165-163-761-706-760-756-670-675-675-6_V.mp3)
  Segment 32: 62212ms ~ 62788ms (file: 650-167-103-231-612-130-615-716-613-527-657-326-162-275-621-612-163-160-106-167-163-176-157-126-176-167-176-716-170-103-163-617-136-160-765-756-170-615-163-160-617-167-613-126-1_V.mp3)
  Segment 33: 63788ms ~ 64364ms (file: 576-756-756-136-167-765-756-672-760-162-136-361-123-216-126-216-263-261-263-326-623-621-261-126-562-263-632-263-627-263-675-576-574-126-176-167-167-627-612-602-162-672-602-206-2_V.mp3)
  Segment 34: 65364ms ~ 65940ms (file: 267-627-106-612-612-162-612-326-231-326-203-321-320-126-657-657-657-675-657-627-621-756-675-576-612-627-675-657-657-567-572-726-675-765-167-675-267-627-627-673-103-130-231-326-1_W.mp3)
  Segment 35: 66940ms ~ 67516ms (file: 127-726-176-621-163-613-617-765-167-106-163-152-136-176-157-163-165-162-136-761-103-213-162-106-103-605-165-136-130-365-107-167-165-231-621-165-315-126 143-132_V.mp3)
  Segment 36: 68516ms ~ 69092ms (file: 103-320-312-623-106-136-173-367-312-103-102-106-301-152-103-715-754-756-725-726-205-752-750-175-620-103-260-260-126-172-126-312-103-310-123-751-576-627-625-152-123-571-573-567-3_W.mp3)
  Segment 37: 70092ms ~ 70668ms (file: 627-641-143-176-231-127-361-671-671-657-615-647-613-136-531-136-321-136-231-213-126-602-752-756-756-760-763-176-175-756-765-765-675-672-672-601-613-623-675-367-163-132-631-136-2_W.mp3)
  Segment 38: 71668ms ~ 72244ms (file: 670-675-670-672-754-756-756 310-176-617-637-762-672-765-765-765-672-672-672-627-627-260 657-675-613-632-316-576-175-173-317-763-673-763 756-675-675-625-716-672-316-321-537-567-6_W.mp3)
  Segment 39: 73244ms ~ 73820ms (file: 672-263-261-261-236-132-102-102-316-531-573-537-573-365-362-547-576-537-547-256-273-231-312-543-216-265-625-625-267-625-625-260-627-623-362-261-326-362-263-627-267-260-326-132-3_W.mp3)
  Segment 40: 74820ms ~ 76728ms (file: 216-176-612-172-756-756-756 236-326-672 725-267-326-162-167-671-123-712-321-312-213-316-310-312-103-310-301-203-301-213-206-123-102-173-102-176-167-612-257-627-126-572-102 574-5_W.mp3)
  Segment 41: 77728ms ~ 78304ms (file: 130-165-165-623-612-613-601-160-635-163-716-751-162-175-167-765-316-107-106-167-106-316-716-136-106-607-176-613-613-136-351-162-613-167-165-103-165-167-123-260-163-623-157-612-1_V.mp3)
  Segment 42: 79304ms ~ 79880ms (file: 576-675 765-765 576-165-152-176-216-251-627-367-673-607-261-265-625-136-136-163-316-170-176-176-176-610-632-630-621-163-106-136-167-762-672-672-675 621-623-126-215-237-326_W.mp3)
  Segment 43: 80880ms ~ 81456ms (file: 321-312-310-312-312-312-136-130-361-362-206-132-213-123-123-125-152-162-213-156-162-167-761-617-621-326-236-276-261-267-623-610-631-572-612-126-130-231-132-162-621-123-716-716-5_W.mp3)
  Segment 44: 82456ms ~ 83032ms (file: 136-167-162-162-162-127-127-172-137-127-132-173-176-172-120-312-213-263-123-206-275-527-265-625-256-265-267-263-623-754-756-621-312-172-157-576-752-762-726-762-765-762-106 657-5_W.mp3)
  Segment 45: 84032ms ~ 84608ms (file: 361-613-261-260-126-123-316-376-261-236-263-602-316-163-261-216-123-201-627-672-672-162 165-136-162-167-162-126-216-136-267-532-267-265-217 102 106-167-621-612-261-106-102-106-6_V.mp3)
  Segment 46: 85608ms ~ 86184ms (file: 316-163-136-132-163-316-321-312-143-132-134-132-613-312-136-601-167-163-136-312 132-172-172-127-127-761-752-706-706-716-765 263-267-267-263-267-263-263-263-261-574-267-267-267-2_V.mp3)
  Segment 47: 87184ms ~ 87760ms (file: 257-205-203-203-203-263-236-263-236-236-263-237-231-237-273-215-325-325-315-312-623-623-621-263_W.mp3)
  Segment 48: 88760ms ~ 89336ms (file: 657-567-567-657-651-657-561-567-567-537-561-567-765-657-765-675-675-567-561-567-167-576-567-156-156-157-157-567-517-756-651-576-561-765-657-567-571-675-657-617-567-657-567-756-6_V.mp3)
  Segment 49: 90336ms ~ 90912ms (file: 126-126-216-276-627-136-623-263-564-576-675-267-672-672-763-671-617-176-671-761-761-675-326-637-162-673-362-632-536-563-362-263-267-276-267-326-652-672-267-267-256-562-362-632-2_W.mp3)
  Segment 50: 91912ms ~ 92488ms (file: 574-576-567-567-617-106-106-106-136-103-162-163-126-162-123-103-123-624-623-632-647 176-157-671-671-127-576 136-132_V.mp3)
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
> **Time**: 2026-04-03 06:49:37
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "어제 영화 봤어요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "어제", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "영화", "start_ms": 170, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "봤어요?", "start_ms": 330, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "아니요, 너무 바빠서 못 봤어요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "아니요,", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 160, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "바빠서", "start_ms": 280, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "못", "start_ms": 420, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "봤어요", "start_ms": 490, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "무슨 일 때문에 그렇게 바빴어요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "무슨", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "일", "start_ms": 140, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "때문에", "start_ms": 210, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "그렇게", "start_ms": 330, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "바빴어요?", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "회사 보고서 제출일이었거든요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "회사", "start_ms": 60, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "보고서", "start_ms": 180, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "제출일", "start_ms": 330, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "이었거든요", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "그거 참 힘들었겠네요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "그거", "start_ms": 45, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "참", "start_ms": 145, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "힘들었", "start_ms": 225, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "겠네요", "start_ms": 385, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "네, 밤을 거의 꼬박 새웠어요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "밤을", "start_ms": 130, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "거의", "start_ms": 250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "꼬박", "start_ms": 360, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "새웠어요", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "오늘은 좀 쉬는 게 어때요?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "오늘은", "start_ms": 35, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 195, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "쉬는", "start_ms": 285, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 395, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "어때요?", "start_ms": 455, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "그래야겠어요",
"speaker": "speaker_2",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "그래야", "start_ms": 30, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "겠어요", "start_ms": 240, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 10,
"transcript": "이번 주말 계획 있어요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "이번", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "주말", "start_ms": 170, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "계획", "start_ms": 290, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "있어요?", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "친구들과 등산 가기로 했어요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "친구들과", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "등산", "start_ms": 230, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "가기로", "start_ms": 360, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "했어요", "start_ms": 480, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "어느 산으로 가세요?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "어느", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "산으로", "start_ms": 165, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "가세요?", "start_ms": 315, "dur_ms": 260, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "북한산에 가려고요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "북한산에", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "가려고요", "start_ms": 240, "dur_ms": 330, "phase_lr_deg": 0.0}
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
"transcript": "날씨가 좋았으면 좋겠네요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "날씨가", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "좋았으면", "start_ms": 200, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "좋겠네요", "start_ms": 390, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "그러게요, 비만 안 오면 좋겠어요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "그러게요,", "start_ms": 55, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "비만", "start_ms": 215, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 335, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "오면", "start_ms": 405, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "좋겠어요", "start_ms": 505, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "산행 조심해서 잘 다녀오세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "산행", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "조심해서", "start_ms": 170, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 330, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "다녀오세요", "start_ms": 400, "dur_ms": 170, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "네, 고마워요. 월요일에 봐요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 45, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "고마워요.", "start_ms": 135, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "월요일에", "start_ms": 295, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "봐요", "start_ms": 455, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "한국의 가을은 정말 아름다운 것 같아요. 산들이 울긋불긋 단풍으로 물드는 모습이 무척 인상적입니다. 특히 북한산은 서울에서 가깝기도 하고 풍경이 아주 좋아서 자주 찾는 편이에요. 나중에 시간이 되시면 저희와 함께 가보시는 건 어떨까요?",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "한국의", "start_ms": 120, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "가을은", "start_ms": 430, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 790, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "아름다운", "start_ms": 1050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 1460, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "같아요.", "start_ms": 1570, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "산들이", "start_ms": 2100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "울긋불긋", "start_ms": 2460, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "단풍으로", "start_ms": 2970, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "물드는", "start_ms": 3430, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "모습이", "start_ms": 3840, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "무척", "start_ms": 4200, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "인상적", "start_ms": 4460, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "입니다.", "start_ms": 4870, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "특히", "start_ms": 5400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "북한산은", "start_ms": 5660, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "서울에서", "start_ms": 6170, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "가깝기도", "start_ms": 6630, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "하고", "start_ms": 7090, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "풍경이", "start_ms": 7350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 7710, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "좋아서", "start_ms": 7970, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "자주", "start_ms": 8330, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "찾는", "start_ms": 8590, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "편이에요.", "start_ms": 8900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "나중에", "start_ms": 9600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "시간이", "start_ms": 10010, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "되시면", "start_ms": 10370, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "저희와", "start_ms": 10780, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "함께", "start_ms": 11140, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "가보시는", "start_ms": 11450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "건", "start_ms": 11910, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "어떨까요?", "start_ms": 12020, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "제안해주셔서 감사해요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "제안해", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "주셔서", "start_ms": 250, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "감사해요", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "언제든지 환영이에요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "언제", "start_ms": 35, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "든지", "start_ms": 195, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "환영", "start_ms": 315, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "이에요", "start_ms": 455, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "벌써 점심시간이네요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "벌써", "start_ms": 45, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "점심", "start_ms": 185, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "시간", "start_ms": 305, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이네요", "start_ms": 465, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "뭐 먹고 싶은 거 있어요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "뭐", "start_ms": 50, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "먹고", "start_ms": 120, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "싶은", "start_ms": 240, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 350, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "있어요?", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "날씨도 쌀쌀한데 칼국수 어때요?",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "날씨도", "start_ms": 60, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "쌀쌀한데", "start_ms": 200, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "칼국수", "start_ms": 390, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "어때요?", "start_ms": 530, "dur_ms": 40, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "완전 좋은데요! 가요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "완전", "start_ms": 35, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "좋은데요!", "start_ms": 185, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "가요", "start_ms": 395, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "여기 정말 맛있어 보여요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "여기", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 170, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "맛있어", "start_ms": 290, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "보여요", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "자주 오는 맛집이에요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "자주", "start_ms": 45, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "오는", "start_ms": 175, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "맛집", "start_ms": 285, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "이에요", "start_ms": 425, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "국물 맛이 끝내주네요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "국물", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "맛이", "start_ms": 200, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "끝내주네요", "start_ms": 330, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "김치랑 같이 드셔보세요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "김치랑", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 230, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "드셔", "start_ms": 350, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "보세요", "start_ms": 480, "dur_ms": 90, "phase_lr_deg": 0.0}
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
"transcript": "배가 정말 부르네요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "배가", "start_ms": 35, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 155, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "부르네요", "start_ms": 305, "dur_ms": 270, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "후식으로 커피 마실까요?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "후식으로", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "커피", "start_ms": 250, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "마실까요?", "start_ms": 390, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "좋아요, 제가 살게요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "좋아요,", "start_ms": 45, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 205, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "살게요", "start_ms": 345, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "아니에요, 오늘은 제가 낼게요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "아니에요,", "start_ms": 50, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "오늘은", "start_ms": 220, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 370, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "낼게요", "start_ms": 490, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "그럼 다음에는 꼭 제가 낼게요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "다음에는", "start_ms": 150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "꼭", "start_ms": 310, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 390, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "낼게요", "start_ms": 500, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "약속한 거예요! 가시죠",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "약속한", "start_ms": 35, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "거예요!", "start_ms": 225, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가시죠", "start_ms": 385, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "이 카페 인테리어 예쁘네요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 45, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "카페", "start_ms": 105, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "인테리어", "start_ms": 225, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "예쁘네요", "start_ms": 415, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "여기가 요즘 핫하대요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "여기가", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "요즘", "start_ms": 190, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "핫하대요", "start_ms": 310, "dur_ms": 260, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "사람들이 많은 이유가 있네요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "사람들이", "start_ms": 55, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "많은", "start_ms": 245, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "이유가", "start_ms": 375, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "있네요", "start_ms": 495, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "아이스 아메리카노 두 잔 나왔습니다",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "아이스", "start_ms": 60, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "아메리카노", "start_ms": 320, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "두", "start_ms": 730, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "잔", "start_ms": 840, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "나왔습니다", "start_ms": 950, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "시원하고 좋네요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "시원하고", "start_ms": 35, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 295, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "남은 하루도 힘내세요!",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "남은", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "하루도", "start_ms": 170, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "힘내세요!", "start_ms": 330, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "민수 씨도요! 월요일에 봬요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "민수", "start_ms": 45, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "씨도요!", "start_ms": 175, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "월요일에", "start_ms": 325, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "봬요", "start_ms": 485, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "안녕히 가세요",
"speaker": "speaker_1",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 40, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 270, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "조심히 들어가세요",
"speaker": "speaker_2",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "조심히", "start_ms": 50, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "들어가세요", "start_ms": 290, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 47,
"transcript": "전화하는 중인가요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "전화하는", "start_ms": 40, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "중인가요?", "start_ms": 260, "dur_ms": 310, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 49,
"transcript": "음악이 참 좋네요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "음악이", "start_ms": 55, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "참", "start_ms": 245, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 325, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "마음에 드신다니 다행이에요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "마음에", "start_ms": 45, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "드신다니", "start_ms": 215, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "다행", "start_ms": 405, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "이에요", "start_ms": 505, "dur_ms": 70, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 06:49:37*
