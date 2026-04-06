# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=08:43:17  uptime=26643s  mem=1748MB  system_processes=446"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_084308.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 167-671-672-765-672-612-106-672-263-162-572-106-267-627-671-567-652-107-103-136-134-132-132-765-765-607-205-627-627-237-137-203-230-203-320-302-236-230-203-213-126-132-312-134-1_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 627-167-617-106-167-106-163-103-136-103-105-312-176-176-130-162-612-510-103-160-547-165-602-176-126-103-176-627-620-106-103-136-106-165-651-165-103-312-162-120-103-106-615-165-1_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 574-576-765 627-627 162-106-120-176-150-120-103-670-716-206-670-675-603-621-623-612-612 314-143-312_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 672-627-612-163 321-267-210-120-106-261-120-126-312-567-275-627-723-361-125-321-162-162-316-130-623-365-326-312-123-263-361-263-120-136-613-163-162-167-675-673-627-765-675-671-6_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 153-125-152-125-123-132-312-312-321-321-321-135-312-231-132-213-123-132-132-135-132-516-157-651-165-215-126-125-321-321-321-352-312-321-312-125-253-265-216-215-213-351-325-321-3_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 607-651-106-601-657-620-620-260-206-102-326-216-203-302-230-103-631-607-607-670-607-260-201-652-260-203-203-301-203-635-310-312-302-321-601-603-620-602-621-261-102-102-312-362-6_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 126-570-570-572-657-126-102-621-615-672-613-613-621-602-620-326-263-230-327-267-106-106-123-162-162-261-236-320-675-756-765-657-267-236-263-326-260-627-627-672-625-263-236-263-2_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 570-102-312-216-132-103-301-361-706-761-716-176-607-607-612-631-316-136-312-371-231-371-326-362-670-263-320-267-603-312-670-607-507-625-236-162-103-162-167-601-216-203-231-265-2_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 762-675-576-657-652-365-367-362 543-627-657-652-625-652-627-567-657-675-675-657-657-765-765-372-576-576-576-576-576-576-576-567-576-756-576-574-574-576-574-576-576-567-567-576-5_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 170-713-621-123-132-713-103-136-173-107-163-167-126-132-103-106-175-675-261-362-563-163-765-765-675-361-103-760-176-136-753-761-132-106-165-162-612-165-671-632-623-623-137-163-1_V.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 321-312-231-312-132-152-562-521-265-653-176-312-132-236-123-126-120-132-130-135-156-152-156-216-123-321-321-321-123-123-136-126-136-123-123-123-312-321-316-325-352-325-326-236-2_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 615-261-103-130-106-167-167-106-670-613-610-132-675-106-130-613-136-103-160-132-103-176-130-106-130-152-103-657-136-106-176-103-163-106-136-132-751-176-163-106-165-103-123-310-1_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 130-103-163-103-231-203-263-621-126-130-103-617-607-607-657-627-627-610-310-103-231-132-706-123-761-672-627-750-207-207-270-765-657-602-675-123-102-123-571-751-756-716-137-576-7_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 671-675-765-763-671-675-756-765-176-765-176-167-103-627-627-617-672-613-631-361-612-612-617-613-613-617-613-613-163-617-617-613-612-167-617-612-612-612-612-163-163-162-167-675-6_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 756-756-106-126 715-172-162-167-167-621-621-621-162-642-672-627-627-265-216-162-321-312-103-312-671-621-612-621 672-524-523-672-627-672-627-761 376-317-372-753-570-573-574-570-7_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 627-672-657-675-657-657-615-132-261-623-627-657-526-567-265-652-265-625-257-256-265-256-136-162-256-653-627-625-267-632-652-517-652-526-376-276-265-261-267-263-263-627-526-652-2_V.mp3)
  Segment 17: 25216ms ~ 39832ms (file: 206-260-261-520-574-572-675 765 607-602-260-267-123-126-134-132-143-672-137-362-267-267-627-127-201-167-103-123-137-106-167-162-631-306-160-213-530-573-576-657-612-163-576-573-7_W.mp3)
  Segment 18: 40832ms ~ 41408ms (file: 361-267-621-672-172-765-751-576-657-756-765-765-765-576-672 574_W.mp3)
  Segment 19: 42408ms ~ 42984ms (file: 160-167-627-106-163-615-620-103-263-137-163-103-576-316-103-671-102-316-103-316-541-152-325-136-162-130-136-162-632-316-312-130-106-136-316-167-103-103-137-360-657-316-136-301-6_V.mp3)
  Segment 20: 43984ms ~ 44560ms (file: 321-123-263-231-213-613-602-672-326-276-672-160-130-621-261-312-316-136-316-162-672-627-167-167-610-102-613-163-120-162-163-216-263-162-167-615-670-167-132-201-204-306-321-326-2_W.mp3)
  Segment 21: 45560ms ~ 46136ms (file: 756-132-126-163-361-167-732-103-102-132-531-167-163-136-312-162-106-106-106-160-162-163-716-175-153-635-651-672-627-261-623-236-263-236-230-206-102-760-360-310-651-652-615-310-1_W.mp3)
  Segment 22: 47136ms ~ 47712ms (file: 320-203-236-217-302-236-620-271-132-572-206-260-236-203-207-670-765-675-230-263-263-216-263-670-612-623-136-132-312-132-756-760-136-132-130-130-102-123-132-564-567-561-163-136-1_V.mp3)
  Segment 23: 48712ms ~ 49288ms (file: 362-263-623-362-362-265-625-632-652-635-615-321-316-361-321-321-321-325-321-326-236-231-236-627-672-672-625-652-625-652-652-263-261-261-263-627-627-627-625-625-267-326-540-265-2_W.mp3)
  Segment 24: 50288ms ~ 50864ms (file: 327-253-351-135-315-315-317-315-310-567-267-627-267-267-236-263-321-123-213-123-126-136-163-156-165-326-326-315-312-231-123-312-563-367-567-756-675-625-623-672-657-675-675-635-1_W.mp3)
  Segment 25: 51864ms ~ 52440ms (file: 105-137-132-216 162-126-603-165-256-612-317-671-103-103-617-631-103-276-162-716-160-102-132-103-162-617-136-103-162-163-123-610-612-162-130-163-105-160-106-106-106-512-567-312-7_V.mp3)
  Segment 26: 53440ms ~ 54016ms (file: 315-315-163-167-621-612-172-263-127-617-163-613-637-361-321-317-312-327-321-326-326-612 675-317-165-156-136-316-351-361-367-316-613-632-362-567-576-256-215-132-316-315-513-165-6_W.mp3)
  Segment 27: 55016ms ~ 55592ms (file: 627-627-231-312-310 167-136-216-312-627-126-673-762-725-276-267-265-567-563-623-263-103-326-625-206-231-236-263-627-603-627-620-102-120-162-136-231-130-136-130-132-106-103-132-1_V.mp3)
  Segment 28: 56592ms ~ 57168ms (file: 132-312-132-507-153-301-621-603-321-132-325-620-623-263-231-321-132-670-135-275-365-627-205-275-357-312-321-123-236-370-672-237-302-716-762-167-175-517-351-152-312-302-372-632-6_W.mp3)
  Segment 29: 58168ms ~ 68788ms (file: 321-761-524-751-576-215-571-573-172-572-236_W.mp3)
  Segment 30: 69788ms ~ 70364ms (file: 632-316 610-612-123-120-170-671-615-152-657-675-765-675-657-107-163-136-163-136-310-310-103-675-612_W.mp3)
  Segment 31: 71364ms ~ 71940ms (file: 562-635-156-671-657-167 136-103-162-162-130-210-126-126-612-675-576-526-567-567-672-762-673-126-167-617-567-562-567-265-627-612-652-675-672-672-762-162-615-657-756-765-672-762-6_W.mp3)
  Segment 32: 72940ms ~ 73516ms (file: 376-713-375-316-376-376-317-712-376-312-326-312-362-326-321-312-132-312-213-316-621-326-361-632-632-253-152-573-357-327-532-312-315-257-351-351-253-265-236-267-236-263-236-326-6_W.mp3)
  Segment 33: 74516ms ~ 75092ms (file: 672-675-137-165-615-167-615-567-567-561-657-657-567-567-657-657-657-675-675-657-576-576-652-657-562-572-567-572-567-576-576-576-567-572-536-567-657-567-652-567-576-567-653-657-5_W.mp3)
  Segment 34: 76092ms ~ 76668ms (file: 762 623-675 312-231-316 632-453-314-163-625-321-236-127-103-617-136-136-130-312-132-127-653-167-162-613_W.mp3)
  Segment 35: 77668ms ~ 91240ms (file: 627 103-137-135-157-132-132-621-756-756-756-756-715-760-276-672-567-571-715-103-176-617-176-716-765-167-673-637-672-672-176-623-612-657 657-315-623-571-567-574-105-157-167-137-1_W.mp3)
  Segment 36: 92240ms ~ 92816ms (file: 176-657-672-627-267-672-267-672-627-671-276-627-657-562-567-567-576-365-365-657-567-561-657-567-576-765-573-573-257-675-672-672-576-567-576-576-576-365-653-357-657-567-657-675-6_W.mp3)
  Segment 37: 93816ms ~ 94392ms (file: 620-601-621-613-123-103-132-321-326-321-136-657-706-706-672 326-302-236-231-302 672-206-206-123-310-130-310-273-276-237-623-627-672-267-203-237-703-720-275-203-206-203-302-320-3_V.mp3)
  Segment 38: 95392ms ~ 95968ms (file: 623-625-236-271-201-213-201-217-203-203-203-201-103-532-320-325-236-231-761-316-675-657-572-567-567-563 163 106-162-601-607-762-673_W.mp3)
  Segment 39: 96968ms ~ 97544ms (file: 675-132-132-765-673-756-756-231 126-163-136-130-137-167-376-136 103-236-126-216-263-216-367 103-312-230-132-301-165-621-623-132-716-137-316-123-163 136-673-126-162-675-152-615-3_W.mp3)
  Segment 40: 98544ms ~ 99120ms (file: 123-312-321-302-176-162-135-167-126-576-675-675-361-136-137-132-123-134-750-726-627-163-136-137-163-130-103-106-103-130-301-302-301-302-206-263-203-203-207-302_V.mp3)
  Segment 41: 100120ms ~ 100696ms (file: 765-621-627-623-267-625-201-520-527-137-513-571-301-136-267-267-126-263-671_V.mp3)
  Segment 42: 101696ms ~ 102272ms (file: 267-620-627-672-675-670 617-671-213-123-261-236-261-627-623-231-321-312-236-326-312-231-321-236-312 765 726-657-120-762-716-765-765-762-132-165-517-105-103-132-123-174-572-627-3_W.mp3)
  Segment 43: 103272ms ~ 103848ms (file: 671-672-670-607-610-620-362-263-216-621-162-572-572-571-570-105-137-316-132-103-163-136-106-106-136-701-706-670-310-613-316-631-360-615-127-167-716-701-107-167-706-716-701-610-6_W.mp3)
  Segment 44: 104848ms ~ 105424ms (file: 132 613-163-136-162-167-162-103-316-176-126-162-103-176-163-103-613-126-137-106-162-106-163-176-102-157-136-261-726 105-136_V.mp3)
  Segment 45: 106424ms ~ 107000ms (file: 762-761-761-132 312-316-316-613 765-765-756_W.mp3)
  Segment 46: 108000ms ~ 108576ms (file: 231-213-261-132-126-132-301-167-162-267-627-267-126-130-103-201-136-160-316-317-213-136-132-130-123-126-632-162-321-103-103-351-103-106-176-315-106-163-175-102-612-103-103-136-1_V.mp3)
  Segment 47: 109576ms ~ 110152ms (file: 265-653-267-625-276-326-265-263-263-236-372-326-273-217-675-267-367-126-235-526-672-652-625-623 132-136 657-567 756-756-761-765-675-306-306-362-320-623-621-620-651-652-657-671-6_W.mp3)
  Segment 48: 111152ms ~ 111728ms (file: 637-106-176-107-167-173-170-103-761-136-527-367-162-165-103-761-173-167-671-167-167-160-615-136-136-607-617-160-163-136-152-120-657-137-176-652-632-765-170-126-673-160-163-106-1_V.mp3)
  Segment 49: 112728ms ~ 113304ms (file: 136-316-672-136-261-261-263-263-261-261-260-213-321-213-213-231-210-526-256-265-265-267-261-620-231-574-547-574-357-357-351-356-356-231-547-574-576-756-657-675-637-631-276-621-3_W.mp3)
  Segment 50: 114304ms ~ 114880ms (file: 612-130-621-103-517-652-167-130-106-164-670-617-106-103-127-162-162-756-136-756-765-763-765-756-753-756-765-756-765-753-715-756-175-671-173-105-560-576-156-576-517-516-105-517-3_W.mp3)
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
> **Time**: 2026-04-03 08:44:15
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "다음 주에 여행 계획 있으세요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "다음", "start_ms": 60, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "주에", "start_ms": 170, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "여행", "start_ms": 260, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "계획", "start_ms": 350, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있으세요?", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "제주도로 가족 여행을 가기로 했어요.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "제주도로", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가족", "start_ms": 210, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "여행을", "start_ms": 320, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "가기로", "start_ms": 450, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "했어요.", "start_ms": 540, "dur_ms": 30, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "재미있겠네요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "재미있겠네요.", "start_ms": 40, "dur_ms": 530, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "오랜만에 가는 거라 기대가 많이 돼요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "오랜만에", "start_ms": 45, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가는", "start_ms": 200, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "거라", "start_ms": 290, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "기대가", "start_ms": 380, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 490, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "돼요.", "start_ms": 560, "dur_ms": 10, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "숙소는 어디로 예약하셨어요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "숙소는", "start_ms": 55, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "어디로", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "예약하셨어요?", "start_ms": 290, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "바다가 보이는 호텔로 잡았어요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "바다가", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "보이는", "start_ms": 170, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "호텔로", "start_ms": 290, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "잡았어요.", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 8,
"transcript": "가서 맛있는 것도 많이 드시고 오세요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "가서", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "맛있는", "start_ms": 140, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "것도", "start_ms": 270, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 360, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "드시고", "start_ms": 450, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "오세요.", "start_ms": 540, "dur_ms": 30, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 10,
"transcript": "제주도 흑돼지를 꼭 먹어보려고요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "제주도", "start_ms": 45, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "흑돼지를", "start_ms": 170, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "꼭", "start_ms": 320, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "먹어보려고요.", "start_ms": 390, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "생각만 해도 군침이 도네요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "생각만", "start_ms": 55, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "해도", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "군침이", "start_ms": 310, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "도네요.", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "Where are you planning to go first?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "Where", "start_ms": 70, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "are", "start_ms": 160, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 210, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "planning", "start_ms": 260, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 390, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "go", "start_ms": 440, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "first?", "start_ms": 510, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "We will visit the Manjanggul Cave.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "We", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "will", "start_ms": 110, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "visit", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 290, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "Manjanggul", "start_ms": 340, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "Cave.", "start_ms": 530, "dur_ms": 40, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "거기 동굴 정말 시원하고 멋있죠.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "거기", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "동굴", "start_ms": 150, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 260, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "시원하고", "start_ms": 370, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "멋있죠.", "start_ms": 500, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "렌터카는 미리 빌리셨나요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "렌터카는", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "미리", "start_ms": 240, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "빌리셨나요?", "start_ms": 350, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "네, 공항에서 바로 찾기로 했어요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "공항에서", "start_ms": 110, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 260, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "찾기로", "start_ms": 370, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "했어요.", "start_ms": 500, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 18,
"transcript": "선물 사 오실 건가요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "선물", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "사", "start_ms": 150, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "오실", "start_ms": 200, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "건가요?", "start_ms": 330, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "감귤 초콜릿이라도 좀 사 올게요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "감귤", "start_ms": 55, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "초콜릿이라도", "start_ms": 160, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 370, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "사", "start_ms": 440, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "올게요.", "start_ms": 510, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "고마워요! 여행 잘 다녀오세요.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "고마워요!", "start_ms": 30, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "여행", "start_ms": 220, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 330, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "다녀오세요.", "start_ms": 400, "dur_ms": 170, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 22,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 23,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 24,
"transcript": "안녕히 계세요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "계세요.", "start_ms": 230, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 26,
"transcript": "새 아파트 분위기는 어떤가요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "새", "start_ms": 45, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "아파트", "start_ms": 100, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "분위기는", "start_ms": 230, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "어떤가요?", "start_ms": 380, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 28,
"transcript": "단지 안에 공원도 있고 아주 쾌적해요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "단지", "start_ms": 55, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "안에", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "공원도", "start_ms": 250, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "있고", "start_ms": 380, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 470, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "쾌적해요.", "start_ms": 540, "dur_ms": 30, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 30,
"transcript": "교통은 편리한 편인가요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "교통은", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "편리한", "start_ms": 170, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "편인가요?", "start_ms": 300, "dur_ms": 270, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 32,
"transcript": "지하철역이 바로 앞이라 출퇴근이 편해요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "지하철역이", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 240, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "앞이라", "start_ms": 350, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "출퇴근이", "start_ms": 440, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "편해요.", "start_ms": 550, "dur_ms": 20, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 34,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 35,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 36,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 37,
"transcript": "정말 부럽네요! 나중에 집들이 한 번 하세요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "부럽네요!", "start_ms": 130, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "나중에", "start_ms": 280, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "집들이", "start_ms": 370, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 460, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "번", "start_ms": 510, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "하세요.", "start_ms": 560, "dur_ms": 10, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 39,
"transcript": "정리가 좀 되면 초대할게요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "정리가", "start_ms": 30, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 160, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "되면", "start_ms": 230, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "초대할게요.", "start_ms": 320, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "네, 연락 기다릴게요. 축하드려요!",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "연락", "start_ms": 110, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "기다릴게요.", "start_ms": 200, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "축하드려요!", "start_ms": 390, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 42,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 43,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 44,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 45,
"transcript": "다음에 또 봬요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 190, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "봬요.", "start_ms": 260, "dur_ms": 310, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 47,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 48,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 49,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 50,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 08:44:15*
