# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=14524  time=06:00:27  uptime=8218s  mem=2043MB  system_processes=869"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260401_060017.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 612-163-106-632-312-216-261-136-132-637-136-765-703-167-103-106-136-106-760-137-657-652-167-136-162-106-125-326-672-136-107-106-106-102-103-106-135-567-106-163-316-160-107-102-1_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 276-276-321-263-326-136-326-531-362-236-263-263-236-312-231-261-126-516-325-263-321-327-237-217-231-321-621-627-136-327-273-267-321-132-123-356-631-361-316-165-165-651-617-326-3_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 567-261-261-326-627-627-657-627-627-675-267-627-726-627-267-623-162-623-231-236-263-236-206-260-621-321-260-260-261-256-623-617-362-547-567-756-576-527-352-527-637-236-132-120-2_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 106-163-163-167-105-157-136-103-103-163-124-132-605-137-160-106-136-103-136-130-103-132-103-125-167-263-216 362-632_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 126-765-627-752-725-612-256 134-765-675-675-765-670-765 671-670-672-765-735-167-136-165-157-632-631-312-326-326-326 765-762-765-765-762 627-321-265-263-176-126-612-120-162-236-2_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 516-156-135-531-156-153-156-513-165-175-153-516-516-516-156-156-631-316-156-157-132-153-125-216-152-152-156-132-152-325-352-135-152-157-561-562-567-516-516-563-352-316-657-153-1_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 672-761-756-765-672 763-760-706-750-706-765-675-750-761-765-761-765-756-716-705-136-715-705-760-760-167-761-765-265-172-761-756-170-675-756-736-136-132-513-103-103-675-672-675-6_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 623-263-236-236-627-261-321-123-627-627-362-316-362-627-623-263-236-267-267-627-627-623-316-163-263-261-261-231-263-263-321-326-637-367-136-167-561-541-213-123-371-316-317-137-6_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 310-317-301-301-326-267-260-260-206-203-230-231-127-216-312-230-237-203-762-720-712-701-127-103-132-213-201-203-206-263-261-230-231-350-163-162-106-230-230-230-206-231-263-267-2_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 103-103-103-106-136-123-103-123-106-103-312-132-130-132-132-302-213-501-531-765-617-617-170-576-162-106-126-106-106-206-270-276-721-276-120-210-123-130-106-106-163-710-706-160-6_V.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 170-106-130-176-653-756-763-153-675-735-317-675-137-136-572-527-576-547-576-562-123-132-162-613-126-123-123-123-216-120 675_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 726-173-130-106-175-173-715-176-716-170-765-176-761-675-706-765-706-172-761-175-671-617-671-167-167-657-671-107-167-301-713-517-175-130-103-321-320-130-105-310-130-103-130-607 1_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 261-103-162-261-165-165-261-236-267-613-362-621-126-162-163-612-261-126-621-605-657-362-651-762-675-627-236-103-167-103-675-167-671-176-603-165-762-167-617-671-652-576-561-157-1_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 623-625-623-623-257-216-612-236-236-572-657-276-316-326 312 321-236-263-301-301 605-621-623_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 127-716-706-320-231 213-103-106-136-263-132 547-543-547-504-504-507-504-507-547-574-574-574-574-574-573-175-127-132-106-675-675-630-620-107-126-103-631-610-103-103-167-136-132-1_V.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 103-123-765-136-230-102-260-260-103-132-103-135-106-136-106-607-360-760-706-760-706-132-312-103-574-547-547-260-206-206-547-547-567 103-301-130-310-136-312-263-310-103-503-570-1_V.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 130-136-617-765-167-205-167-302-160-136-136-106-263-260-671-106-136-163-132-761-613-612-712_V.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 760-576-750-132-162-102-132 316-320-675-672-765-143-136 132-671-670-765-631-136-310-310-312-301-360-107-761-150-267-120-132-261-162-167-657-615-754-765-172-617-612-674 754-754-7_V.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 102-102-312-160-216-160-261-130-312-163-301-132-612-321-572-623-312-103-316-301-130-103-310-301-302-361-326-632-302-362-567-567-617-106-163-506-602-261-657-672-670-670-627-612-6_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 213-231-213-137-231-321-325-312-321-536-540-547-516-657-765-675-123-231-237-271-273-675-673-671-167-162-617-637-316-157-156-157-175-157-517-512-213-321-231-123-127-132-213-132-3_W.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 302-301-136-632-362-362-675-603-361-306-301-236-162-132-312-132-106-160-106-106-160-106-163-612-617-167-617-651-615-621-612-106-310-316-310-316-103-263-620-163-627-675-167-620-6_V.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 163-106-130-310-130-310-107-106-106-167-675-675-675-576-675-172-267-132-126-126-260-231-103-617-615-671-716-167-167-163-167-713-173-167-610-163-310-120-130-236-123-102-120-123-7_W.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 627-612-623-163-130-321-132 123-312-132-312-310-132-123_V.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 316-103-132-765-617-657-326-167-132-126-612-621 103-760-317-761-602-123-167-136-710-162-756-162-103-102-106-617-105-163-136-106-103-150-105-123-617-671-261-627-163-561-215-671-1_V.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 265-765-765-762-760-672-154-756-106-326-231-163-173-132-617-673-671-617-106-310-310-716-107-103-136-103-132-173-657-620-627-160-106-126 167-106-162-761-167-162-671-132-103-163-1_V.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 731-267-236-263-326-261-321-326-216-316-167-236-673-612-623-631-162_W.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 132-103-137-613-156-362-627-615-752-671-120-162-106-163-162-610-627-160-160-160-102-156-675-106-263-236-657-572-526-256-526-527-257-527-257-257-257-526-257 132_W.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 135-357-375-375-375-736-376-765-763-376-326-237-327-326-362-126-316-236-321-362-236-237-231-321-321-231-321-321-261-231-231-236-213-527-327-375-357-372-253-327-326-327-321-326-3_W.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 627-621-620-613-217-216-726-716-267-172-721-710-761-726-371-172-627-762-726-726-672-726_W.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 312-301-312-341-413-625-602-602-706-756-367-217-271-271-107-756-765-756-705-753-765-761-706-507-570-576-175-731-762-576-765-176-671-621-612-576-517-756-751-753-162-127-132-547-5_V.mp3)
  Segment 31: 47280ms ~ 48504ms (file: 216-625-657-652-632-163-635-651-635-136-316-162-135-652-213-563-365-132-162-126-136-620-106-213-216-261-175-126-527-571-675-251-753-172-516-103-123-107-620-216-106-136-132-765-6_W.mp3)
  Segment 32: 49504ms ~ 50080ms (file: 307-176-507-576-136-165-617-316-132-132-135-132-216-123-132-213-132-160-610-310-362-126-167-701-571-136-103-213-361-162-130-261-176-670-362-672-715-132-312-167-601-631-316-164-2_W.mp3)
  Segment 33: 51080ms ~ 51656ms (file: 672-637-720-675-716-517-172-152-175-751-752-657-652-730-657-562-576-630-723-601-736-716-716-627-625-263-623-637-637-613-316-136-163-761-765-765-657-237-671-637-627-175-761-763-7_W.mp3)
  Segment 34: 52656ms ~ 53232ms (file: 726-765-754-135-153-312-132-316-120-301-320-263-230-623-206-562-125-156-301-326-620-326-612-301-316-613-136-160-106-162-167-760-263-326-103-102-361-160-320-136-601-756-675-263-6_V.mp3)
  Segment 35: 54232ms ~ 54808ms (file: 156-651-561-165-156-156-156-651-567-512-567-657-657-651-657-657-657-653-652-576-561-165-175-165-156-165-156-561-165-156-561-165-165-156-516-651-156-651-657-610-105-106-605-165-1_W.mp3)
  Segment 36: 55808ms ~ 56384ms (file: 657-625-627-672-762-672-267-267-627-621-127-123-231-321-265-261-236-236-216-213-216-265-251-217-275-761-673-627-132-265-216-217-210-517-532-157-213-123-125-671-671-675-567-536-6_W.mp3)
  Segment 37: 57384ms ~ 57960ms (file: 103-103-167-561-672-176-162-123-132-123-136-103-163-617-617-130-160-123-130-175-175-102-671 163-617-167-132-176-106-103-162-103-163-651-216 175-607-620_V.mp3)
  Segment 38: 58960ms ~ 59536ms (file: 256-321-351-321-356-356-365-376-735-371-657-157-563-576-567-371-163-316-652-652-267-627-625-625-265-652-567-657-365-376-365-357-371-367-371-376-176-367-317-153-156-315-517-571-2_W.mp3)
  Segment 39: 60536ms ~ 61112ms (file: 761-675-671-716-167-106-617-671-617-675-617-276-107-160-106-106-176-172-126-216-162-126-132-213 261-216-623-574-576-567-675-605-162-756-527-625-615-625-167-162-106-106-103-106-1_W.mp3)
  Segment 40: 62112ms ~ 62688ms (file: 576-623-263-263-267-267-265-236_W.mp3)
  Segment 41: 63688ms ~ 64264ms (file: 672-607-167-716-375-312-136-132-106-620-751 163 672-657-657-763-301-163-631-675-213-165-675-671-617-162-651-675-675-675-657-756-765-567-567-576-165-756-576-576-657-567-756-567-5_W.mp3)
  Segment 42: 65264ms ~ 65840ms (file: 562-625-265-625-627-267-627-265-265-621-261-627-623-267-621-321-623-623-362-362-563-315-135-163-137-136-126-316-316-231-321-236-231-236-263-263-265-236-231-236-326-261-532-236-2_W.mp3)
  Segment 43: 66840ms ~ 67416ms (file: 672-672 362-213-132-321-213-312-126-316-103_V.mp3)
  Segment 44: 68416ms ~ 68992ms (file: 675-765-576-547-503-312-216-201-605-657-605-670-567-576-576-567-576-675-675-675-756-275-207-670-670-671-675-540-506-256-351-132-132-127-716-765-321-712-712-671-167-136-162-123-1_W.mp3)
  Segment 45: 69992ms ~ 70568ms (file: 260-361-612-316-136-263-321-316-312-613-362-163-172-216-124-216-132-103-132-570 574-576-105-103-132-370-375-173-162-132-103-301-312-627-236-236-276-675-675-672-753-756-364-127-1_V.mp3)
  Segment 46: 71568ms ~ 72144ms (file: 163-106-106-620-623-120-126-123-236-130-103-201-126-103-132-174 632-320-321-620-267-263-326-236-326-123-206-620-321-253-526-350-326-306-610-361-302-306-310-301-601-610-632-623-2_V.mp3)
  Segment 47: 73144ms ~ 73720ms (file: 675-136-612-617-617-621-613-613-632-615-167-617-167-675-673-375-675-162-165-612-617-621 641-326-613-621-263-236-132-165-173-751-367-673-672-620-627-602-326-263-362-362-163-326-1_W.mp3)
  Segment 48: 74720ms ~ 75296ms (file: 725-357-750-574-574-574-540-574-540-501-574-576-574-527-574-567-540-504-501 315-315-756-637-627-627-623 756-576-527-752-562-352-657-716-526-256-231-213-521-574-574-576-657-657-6_W.mp3)
  Segment 49: 76296ms ~ 76872ms (file: 106-623-621-372-637-125-103-316-103-103-621-613-163-601-167-136-123-163-216-612-136-163-316-126-123-325-175-326-613-132-321-160-162-167-136-130-102-106-165-136-106-160-612-162-1_V.mp3)
  Segment 50: 77872ms ~ 78448ms (file: 362-326-351-352-326-327-326-135-574-537-163-316-321-261-612-235-327-362-623-236-326-362-623-206-256-567-321-326-237-230-365-362-326-360-136-574-547-623-135-163-230-270-214-216-2_W.mp3)
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
> **Time**: 2026-04-01 06:01:58
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "오늘 기분이 어떠세요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 115,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 115, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "기분이", "start_ms": 255, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "어떠세요?", "start_ms": 405, "dur_ms": 165, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "How are you feeling today?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "How", "start_ms": 90, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "are", "start_ms": 170, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 230, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "feeling", "start_ms": 300, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "today?", "start_ms": 420, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "정말 행복해요. 좋은 일이 있거든요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 130, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "행복해요.", "start_ms": 240, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 395, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "일이", "start_ms": 465, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "있거든요.", "start_ms": 525, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "I'm really happy. Something good happened.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "I'm", "start_ms": 100, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "really", "start_ms": 170, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "happy.", "start_ms": 260, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "Something", "start_ms": 385, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "good", "start_ms": 495, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "happened.", "start_ms": 555, "dur_ms": 20, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "축하드려요! 무슨 일인지 궁금하네요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "축하드려요!", "start_ms": 140, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "무슨", "start_ms": 350, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "일인지", "start_ms": 440, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "궁금하네요.", "start_ms": 540, "dur_ms": 35, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "Congratulations! I'm curious what it is.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 115,
"audio_type": "speech",
"words": [
{"word": "Congratulations!", "start_ms": 115, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "I'm", "start_ms": 375, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "curious", "start_ms": 435, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "what", "start_ms": 525, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "it", "start_ms": 570, "dur_ms": 5, "phase_lr_deg": 0.0},
{"word": "is.", "start_ms": 576, "dur_ms": 0, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "장학금을 받게 되었어요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "장학금을", "start_ms": 130, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "받게", "start_ms": 320, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "되었어요.", "start_ms": 440, "dur_ms": 135, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "I got a scholarship.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 120, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "got", "start_ms": 180, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 270, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "scholarship.", "start_ms": 310, "dur_ms": 260, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "우와, 대단하시네요! 정말 자랑스러워요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 135,
"audio_type": "speech",
"words": [
{"word": "우와,", "start_ms": 135, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "대단하시네요!", "start_ms": 215, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 385, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "자랑스러워요.", "start_ms": 465, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "Wow, that's amazing! I'm so proud of you.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 105,
"audio_type": "speech",
"words": [
{"word": "Wow,", "start_ms": 105, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "that's", "start_ms": 195, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "amazing!", "start_ms": 265, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "I'm", "start_ms": 385, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "so", "start_ms": 445, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "proud", "start_ms": 495, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "of", "start_ms": 555, "dur_ms": 15, "phase_lr_deg": 0.0},
{"word": "you.", "start_ms": 575, "dur_ms": 1, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "감사합니다. 다 친구들 덕분이에요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "감사합니다.", "start_ms": 150, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 300, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "친구들", "start_ms": 350, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "덕분이에요.", "start_ms": 470, "dur_ms": 105, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "Thank you. It's all thanks to my friends.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "Thank", "start_ms": 120, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "you.", "start_ms": 220, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "It's", "start_ms": 290, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "all", "start_ms": 360, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "thanks", "start_ms": 420, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 500, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "my", "start_ms": 540, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "friends.", "start_ms": 575, "dur_ms": 1, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "언제 한턱 내실 건가요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "언제", "start_ms": 140, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "한턱", "start_ms": 250, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "내실", "start_ms": 370, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "건가요?", "start_ms": 470, "dur_ms": 105, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "When are you going to treat us?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "When", "start_ms": 110, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "are", "start_ms": 200, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 260, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "going", "start_ms": 320, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 400, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "treat", "start_ms": 440, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "us?", "start_ms": 530, "dur_ms": 45, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "내일 점심에 맛있는 거 먹으러 가요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 125,
"audio_type": "speech",
"words": [
{"word": "내일", "start_ms": 125, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "점심에", "start_ms": 225, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "맛있는", "start_ms": 335, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 455, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "먹으러", "start_ms": 495, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "가요.", "start_ms": 570, "dur_ms": 5, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "Let's go eat something delicious for lunch tomorrow.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "Let's", "start_ms": 100, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "go", "start_ms": 180, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "eat", "start_ms": 230, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "something", "start_ms": 290, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "delicious", "start_ms": 390, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "for", "start_ms": 490, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "lunch", "start_ms": 530, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "tomorrow.", "start_ms": 575, "dur_ms": 1, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "좋아요! 기대할게요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 145,
"audio_type": "speech",
"words": [
{"word": "좋아요!", "start_ms": 145, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "기대할게요.", "start_ms": 305, "dur_ms": 270, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "Great! I'll look forward to it.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "Great!", "start_ms": 110, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "I'll", "start_ms": 220, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "look", "start_ms": 290, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "forward", "start_ms": 370, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 480, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "it.", "start_ms": 530, "dur_ms": 45, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "메뉴는 무엇이 좋을까요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "메뉴는", "start_ms": 130, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "무엇이", "start_ms": 250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "좋을까요?", "start_ms": 360, "dur_ms": 215, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "What menu would be good?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "What", "start_ms": 100, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "menu", "start_ms": 190, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "would", "start_ms": 300, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "be", "start_ms": 380, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "good?", "start_ms": 430, "dur_ms": 145, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "비빔밥 어때요? 건강에도 좋잖아요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "비빔밥", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "어때요?", "start_ms": 280, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "건강에도", "start_ms": 390, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "좋잖아요.", "start_ms": 500, "dur_ms": 75, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "How about Bibimbap? It's good for health.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "How", "start_ms": 120, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "about", "start_ms": 200, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "Bibimbap?", "start_ms": 290, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "It's", "start_ms": 430, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "good", "start_ms": 500, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "for", "start_ms": 550, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "health.", "start_ms": 576, "dur_ms": 0, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "좋아요, 제가 아는 맛집이 있어요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "좋아요,", "start_ms": 140, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 260, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "아는", "start_ms": 330, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "맛집이", "start_ms": 410, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있어요.", "start_ms": 520, "dur_ms": 55, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "Great, I know a good restaurant.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "Great,", "start_ms": 110, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 210, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "know", "start_ms": 260, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 340, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "good", "start_ms": 380, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "restaurant.", "start_ms": 460, "dur_ms": 115, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "그럼 내일 12시에 정문에서 봐요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 130, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "내일", "start_ms": 210, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "12시에", "start_ms": 310, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "정문에서", "start_ms": 420, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "봐요.", "start_ms": 530, "dur_ms": 45, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "Then see you at 12 at the main gate tomorrow.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 105,
"audio_type": "speech",
"words": [
{"word": "Then", "start_ms": 105, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "see", "start_ms": 185, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 255, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "at", "start_ms": 315, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "12", "start_ms": 355, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "at", "start_ms": 425, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 465, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "main", "start_ms": 505, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "gate", "start_ms": 555, "dur_ms": 15, "phase_lr_deg": 0.0},
{"word": "tomorrow.", "start_ms": 576, "dur_ms": 0, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "네, 늦지 않게 갈게요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 140, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "늦지", "start_ms": 210, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "않게", "start_ms": 320, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "갈게요.", "start_ms": 420, "dur_ms": 155, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "Okay, I'll be there on time.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 115,
"audio_type": "speech",
"words": [
{"word": "Okay,", "start_ms": 115, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "I'll", "start_ms": 205, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "be", "start_ms": 275, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "there", "start_ms": 325, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "on", "start_ms": 405, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "time.", "start_ms": 465, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 30,
"transcript": "내일 만나요! 안녕.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "내일", "start_ms": 150, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "만나요!", "start_ms": 270, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "안녕.", "start_ms": 410, "dur_ms": 165, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "See you tomorrow! Bye.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 120, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 210, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "tomorrow!", "start_ms": 280, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "Bye.", "start_ms": 440, "dur_ms": 135, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "기다리는 동안 책을 읽을까요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "기다리는", "start_ms": 140, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "동안", "start_ms": 300, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "책을", "start_ms": 410, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "읽을까요?", "start_ms": 510, "dur_ms": 65, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "Shall we read a book while waiting?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "Shall", "start_ms": 110, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "we", "start_ms": 200, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "read", "start_ms": 270, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 370, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "book", "start_ms": 410, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "while", "start_ms": 500, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "waiting?", "start_ms": 570, "dur_ms": 5, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "네, 도서관에 재미있는 책이 많아요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 130, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "도서관에", "start_ms": 200, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "재미있는", "start_ms": 340, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "책이", "start_ms": 470, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "많아요.", "start_ms": 540, "dur_ms": 35, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "Yes, there are many interesting books in the library.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 105,
"audio_type": "speech",
"words": [
{"word": "Yes,", "start_ms": 105, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "there", "start_ms": 185, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "are", "start_ms": 255, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "many", "start_ms": 305, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "interesting", "start_ms": 385, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "books", "start_ms": 505, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 570, "dur_ms": 5, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 576, "dur_ms": 0, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "어떤 종류의 책을 좋아하세요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 145,
"audio_type": "speech",
"words": [
{"word": "어떤", "start_ms": 145, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "종류의", "start_ms": 255, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "책을", "start_ms": 375, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "좋아하세요?", "start_ms": 475, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "What kind of books do you like?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "What", "start_ms": 110, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "kind", "start_ms": 195, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "of", "start_ms": 295, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "books", "start_ms": 345, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "do", "start_ms": 445, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 505, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "like?", "start_ms": 555, "dur_ms": 20, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "저는 소설책을 즐겨 읽어요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 130, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "소설책을", "start_ms": 250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "즐겨", "start_ms": 410, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "읽어요.", "start_ms": 510, "dur_ms": 65, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "I enjoy reading novels.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 115,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 115, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "enjoy", "start_ms": 185, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "reading", "start_ms": 305, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "novels.", "start_ms": 445, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "소설은 상상력을 풍부하게 해주죠.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "소설은", "start_ms": 140, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "상상력을", "start_ms": 270, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "풍부하게", "start_ms": 410, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "해주죠.", "start_ms": 530, "dur_ms": 45, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "Novels enrich your imagination.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "Novels", "start_ms": 120, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "enrich", "start_ms": 280, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "your", "start_ms": 400, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "imagination.", "start_ms": 470, "dur_ms": 105, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "맞아요. 여행가는 기분이 들기도 해요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "맞아요.", "start_ms": 150, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "여행가는", "start_ms": 260, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "기분이", "start_ms": 400, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "들기도", "start_ms": 500, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "해요.", "start_ms": 571, "dur_ms": 4, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "True. It feels like traveling sometimes.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "True.", "start_ms": 110, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "It", "start_ms": 220, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "feels", "start_ms": 270, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "like", "start_ms": 390, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "traveling", "start_ms": 460, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "sometimes.", "start_ms": 560, "dur_ms": 15, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "추천해 줄 만한 책이 있나요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "추천해", "start_ms": 130, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "줄", "start_ms": 260, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "만한", "start_ms": 310, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "책이", "start_ms": 410, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "있나요?", "start_ms": 510, "dur_ms": 65, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "Do you have any books to recommend?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "Do", "start_ms": 100, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 180, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "have", "start_ms": 250, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "any", "start_ms": 350, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "books", "start_ms": 430, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 530, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "recommend?", "start_ms": 570, "dur_ms": 5, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "이 책 한번 읽어 보세요. 정말 재미있어요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 140, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "책", "start_ms": 190, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "한번", "start_ms": 260, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "읽어", "start_ms": 360, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "보세요.", "start_ms": 450, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 550, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "재미있어요.", "start_ms": 571, "dur_ms": 4, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "Try reading this book. It's really fun.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "Try", "start_ms": 110, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "reading", "start_ms": 200, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "this", "start_ms": 320, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "book.", "start_ms": 390, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "It's", "start_ms": 480, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "really", "start_ms": 540, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "fun.", "start_ms": 571, "dur_ms": 4, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 49,
"transcript": "고마워요. 다 읽고 돌려줄게요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "고마워요.", "start_ms": 130, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 250, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "읽고", "start_ms": 300, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "돌려줄게요.", "start_ms": 400, "dur_ms": 175, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "Thank you. I'll return it after I finish reading.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 115,
"audio_type": "speech",
"words": [
{"word": "Thank", "start_ms": 115, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "you.", "start_ms": 215, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "I'll", "start_ms": 285, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "return", "start_ms": 355, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "it", "start_ms": 455, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "after", "start_ms": 505, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 571, "dur_ms": 4, "phase_lr_deg": 0.0},
{"word": "finish", "start_ms": 576, "dur_ms": 0, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-01 06:01:58*
