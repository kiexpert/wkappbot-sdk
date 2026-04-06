# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43304  time=07:03:30  uptime=16799s  mem=1404MB  system_processes=443"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260331_070319.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 305-103-507-105-531-361-671-672-670-617-671-103-310-301-376-312-316-563-657-657-620-206-260-261-657-657-672-625-657-607-652-625-165-765-576-576-756-276-260-236-206-260-130-310-3_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 547-567 136-127-162 127 765-765-763-765-763-576-756_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 632-316-675-316-672-136-675-675-627-306-326-236-216-213-253-632-260-123-621-126-267-263-265-263-236-263-621_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 126-216-652-765-632-237-362-367-367-267-361-623-236-326-316-615-156-312-361-632-361-621-236-367-261-326-261-321-231-231-326-136-163-316-326-321-362-356-657-572-526-357-573-375-3_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 356-576-236-326-263-236-265-326-625-261-236-356-253-256-326-321-367-546-521-256-625-326-632-621-216-516-163-537-523-576-256-126-326-312-537-532-253-267-625-621-260-376-231-235-5_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 527-157-275-237-756-352-627-376-735-137-130-152-132-132-231-261-527-157-517-513-514-510-153-516-567-567-736-574-530-257-356-532-351-315-352-532-315-325-265-231-326-237-216-236-2_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 206-302-230-206-326-236-230-260-203-203-236-263-236-236-267-213-320-326-326-326-321-326-326-326-326-362-627-627-621-621-632-316-362-360-360-360-365-365-361-365-365-365-316-361-3_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 601-613-126-326-716-132-160-167-632-162-716-130-615-765-167-761-123-106-210-617-165-716-106-136-125-260-526-615-601-126-612-106-615-136-163-103-371-136-617-106-107-157-126-172-1_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 657-267-627-627-276-263-276-267-267-261-261-263-236-263-236-261-236-263-263-236-260-261-261-263-621-261-265-265-216-572-571-256-263-263-263-261-263-236-263-263-236-326-312-231-1_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 357-736-617-671-671-673-671-613-631-637-673-367-237-361-261-361-613-376-361-376-613-637-675-653-156-136-137-137-375-653-623-726-732-752-327-752-731-637-173-163-617-167-631-157-7_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 623-627-673-624-261-263-326 352-231-657-652-625-652-567-576-567-562-256-261-263-623-526-526-256-265-620-526-265-265-672-625-261-620-625-620-627-627-652-564-567-265-265-213-260-6_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 573-576-136-574-756-756-756-750-170-316-736-765-756-752-756-756-753-715-765-765-726-276-271-267-267-263-217-265-265-265-261-267-627-627-267-267-267-762-762-267-231-132-671-361-5_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 120-231-152-132-627-136-672-675-567-765-567-631-316-136-136-167-136-601-132-261-612-126-267-612-267-267-671-267-621-261-267-627-136-236-263-231-236-267-263-621-317-761-376-763-3_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 527-256-126-251-625-267-274-276-267-267-265-257-527-257-256-257-257-574-561-527-576-516-576-165-163-213-231-235-123-312-351-316-613-157-537-135-315-315-315-231-357-315-517-153-5_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 376-216-516-516-571-236-213-671-657-576-172-217-271-271-716-237-217-127-267-217-216-256-175-321-627-165-357-567-157-517-615-521-517-567-517-356-675-365-351-165-327-123-625-751-5_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 126-157-132-103-312-312-136-316-136-167-325-237-361-367-237-367-675-675-675-670-763-675-670-607-765-670-670-675-671-362-312-301-316-361-613-106-316-103-657-163-160-103-163-631-6_V.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 326-236-215-215-312-216-312-123-213-135-312-351-315-357-574-574-574-576-574-351-162-135-371-765-721-517-156-153-162-136-321-312-217-521-135-213-231-257-312-132-375-371-175-715-7_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 356-510-165-613-641 123 413 265-263 672-631-163-132-163-132-124-125-176-136-162-132-163-136-136-132-135-153-136-135-136 621-263-621 651_V.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 765-761-761-761-765-765 762-765-675 763_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 263-126-356-102-621-162-106-163-103-162-621-261-627-620 501-574-506 261-627-263_V.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 631-631-706-167-260-620-260-260-132-321-236-231-312-301-310-301-321-320-213-236-276-206-627-602-601-612-102-120-132-130-130-201-102-123-216-123-162-162-132-236-132-123-136-632-2_W.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 156-521-215-123-125-153-512-251-213-312-321-132-123-563-516-152-265-126-125-152-275-267-213-312-321-312-136-136-136-136-132-351-135-312-132-312-132-132-562-265-256-256-136-136-1_W.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 672-675-176-137-132-213-261-261-621-752-723-123-312-316-123-162-261-627-623-362-623-132-675-671-362-263-361-361-623-672-672-627-631-162-613-312-263-612-627-672-673-673-312-132-6_V.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 620-206-260-623-260-236-623-103-263-670-675-672-765-760-673-206-372-675-756-621-163-621-621-261-623-621-576-756-765-760-261-261-627-652-162-612-106-105-163-136-162-126-162-621-6_W.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 163-123-312-163-316-315-136-163-351-365-265-326-621-576-265-265-126-514-572-537-541-375-367-576-567-651-315-251-512-521-312-263-624-652-625-623-236-263-236-326-231-312-563-567-6_W.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 563-567-517-357-561-651-567-653-657-657-576-567-653-563-536-537-563-576-567-657-567-517-563-365-657-567-652-625-652-652-526-562-657-567-526-561-567-650-506-105-126-126-165-265-6_W.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 627 621-162 546-546-574-564-576-567-576-567-567-564-172_W.mp3)
  Segment 28: 42552ms ~ 45936ms (file: 321 103-123-150-310-213-231-621-301-102-365 251-217-164-326-647-745-136_V.mp3)
  Segment 29: 46936ms ~ 47512ms (file: 326-531-513-531-213-231-215-512-513-253-256-215-216-263-263-267-127-257-576-126-162-627-365-162-216-216-167-172-527-516-157-657-136-163-612-126-361-136-137-537-537-531-574-534-5_W.mp3)
  Segment 30: 48512ms ~ 49088ms (file: 651-657-652-576-657-561-657-567-567-563-563-576-156-517-567-567-567-567-657-567-657-567-567-675-567-567-567-567-567-536-567-567-576-567-567-576-567-576-567-567-567-653-567-567-6_W.mp3)
  Segment 31: 50088ms ~ 50664ms (file: 231-321 513-317-135 132-302-302-301-302 761-765-762-672_W.mp3)
  Segment 32: 51664ms ~ 52240ms (file: 627-601-163-162-160-160-160-106-610-162-603-361-316-657-106-167-163-617-160-617-106-106-106-106-106-167-627-627-627-620-620-547-567-567-657-605-165-617-167-617-671-617-310-361-3_V.mp3)
  Segment 33: 53240ms ~ 53816ms (file: 320-372-703-765-602-206-765-267-706-760-762-370-306-765-765-230-301-756-750-367-752-207-236-230-103-123-613-320-632-632-267-203-320-302-301-306-132-312-316-163-316-306-301-361-3_V.mp3)
  Segment 34: 54816ms ~ 55392ms (file: 574-537 617-317 531-516 532-325-561-361 527-265 257-267-265-275-265-267-623-623-236-263-326-253-265-260-265-265-625-265-572-527-625-265-632-631-631 257-257-237-627-362-532-321-7_W.mp3)
  Segment 35: 56392ms ~ 56968ms (file: 652-256-257-235-532-253-523-527-526-265-265-312-652-652-256-265-625-261-526-521-526-521-526-524-520-523-325-516-567-532-573-572-513-523-523-526-521-570-576-256-261-231-213-327-2_W.mp3)
  Segment 36: 57968ms ~ 58544ms (file: 675-627-627-265-576-561-631-527-357-573-513-574-547-576-357-310-527-253-321-316-316-136-153-312-125-126-120-267-261-526-265-276-263-267-627-627-267-265-265-562-572-572-315-135-2_W.mp3)
  Segment 37: 59544ms ~ 60120ms (file: 762-706-627-765-716-627-206-260-206-321-310-756-756-726 765-173-675-675-576-517-513-715 765-765-765 361-162-136-231-132-210-201-130-126 756-756-765-765 146-153-361-132-261-613-2_W.mp3)
  Segment 38: 61120ms ~ 61696ms (file: 301-120-302-361-301-326-325-317-103-236-576-756-754-754-607-167-235-215-123-216-316-620-623-326-132-362-136-316-612-136-216-620-602-320-236-672-260-326-206-106-103-613-612-160-1_W.mp3)
  Segment 39: 62696ms ~ 63272ms (file: 316-312-163-316-316-316-231-326-236-326-236-236-356-265-625-621-236-625-267-263-213-326-534-574-517-157-156-715-275-126-517-156-157-156-563-652-365-536-576-576-537-536-513-235-2_W.mp3)
  Segment 40: 64272ms ~ 64848ms (file: 752-257-275-725-752-275-725-765-276 615-267-612-312 216-321-162-132-162-623-360-321-123-261-162 574-576-562-567-132-763_W.mp3)
  Segment 41: 65848ms ~ 66424ms (file: 321-231-546-540-542-253-231-231-532-534-571-127-162-514-564-123-157-752-574-351-326-321-352-523-265-132-132-231-312-671-761-673-672-672-526-237-321-261-263-256-265-265-315-327-2_W.mp3)
  Segment 42: 67424ms ~ 68000ms (file: 651-671-765-657 675-651-675-376-162-312-761-176 167-376 627-621-162-162-672-265-120-637-316-321-136 760-726-726-267-267-261-267-627-623-126-362-162-632-263-362-563-627-657-267-2_W.mp3)
  Segment 43: 69000ms ~ 69576ms (file: 105-150-315-651-123-321-216-312-361-231-316-123-613-326-123-132-173-351-173-175-576-571-756-321-317-321-325-312-235-312-326-312-312-235-316-326-312-317-316-123-671-315-571-167-1_W.mp3)
  Segment 44: 70576ms ~ 71152ms (file: 321-326-326-326-231-326-325-312-315-312-327-123-327-235-527-325-321-362-325-321-326-326-362-362-365-326-365-362-326-237-352-532-256-253-235-236-253-235-235-213-321-326-321-320-3_W.mp3)
  Segment 45: 72152ms ~ 72728ms (file: 672-607-765-675-765-765-765-675-657-132-312-213-132-321-132-312-351-213-263-627-321-241-321-321-136-167-106-136-610-623-621-106-136-132-132-213-621-627-670-607-607-620-627-706-7_V.mp3)
  Segment 46: 73728ms ~ 74304ms (file: 627-652-263-672-236-625-675-605-326-235-253-623_W.mp3)
  Segment 47: 75304ms ~ 75880ms (file: 361-763-762-375-756-375-371-372-172-137-375-326-716-217-172-175-176-172-762-175-137-127-165-156-175-657-251-127-257-756-165-175-175-136_W.mp3)
  Segment 48: 76880ms ~ 77456ms (file: 261-261-263-236-263-261-261-263-263-213-231-321-125-136-125-162-615-165-162-321-236-231-321-312-132-312-126-216-316-136-632-163-162-123-126-216-263-236-216-632-675-657-657-657-3_W.mp3)
  Segment 49: 78456ms ~ 79032ms (file: 130-326-123-601-130-706-175-170-263-276 136-631-130-137-136-136-162-625-726-152 107-103-167-136-106-613-612-136-617-316-165-316-615-103-130-157-130-136-107-130-135-213-721-657-3_V.mp3)
  Segment 50: 80032ms ~ 80608ms (file: 365-365-375-132-673-675-675-671-675-670-673-327-203-207-514-310-235-126-213-231-213-216-267-236-321-320-130-127-316-312-231-123-123-167-127-321-321-302-320-524-261-251-536-256-2_W.mp3)
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
> **Time**: 2026-03-31 07:04:23
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "오늘 컨퍼런스 발표 정말 인상적이었어요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "컨퍼런스", "start_ms": 165, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "발표", "start_ms": 315, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 435, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "인상적이었어요.", "start_ms": 525, "dur_ms": 51, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 3,
"transcript": "감사합니다. 준비하느라 고생 좀 했거든요.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "감사합니다.", "start_ms": 30, "dur_ms": 210, "phase_lr_deg": -5.2},
{"word": "준비하느라", "start_ms": 250, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "고생", "start_ms": 410, "dur_ms": 90, "phase_lr_deg": -5.3},
{"word": "좀", "start_ms": 510, "dur_ms": 40, "phase_lr_deg": -5.1},
{"word": "했거든요.", "start_ms": 560, "dur_ms": 16, "phase_lr_deg": -5.2}
]
},
{
"segment": 4,
"transcript": "특히 인공지능 관련 분석이 날카롭더라고요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "특히", "start_ms": 60, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "인공지능", "start_ms": 180, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "관련", "start_ms": 310, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "분석이", "start_ms": 410, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "날카롭더라고요.", "start_ms": 520, "dur_ms": 56, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "데이터를 모으는 데 시간이 꽤 걸렸습니다.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "데이터를", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": -4.8},
{"word": "모으는", "start_ms": 200, "dur_ms": 110, "phase_lr_deg": -4.9},
{"word": "데", "start_ms": 320, "dur_ms": 50, "phase_lr_deg": -4.7},
{"word": "시간이", "start_ms": 380, "dur_ms": 110, "phase_lr_deg": -4.8},
{"word": "꽤", "start_ms": 500, "dur_ms": 40, "phase_lr_deg": -4.9},
{"word": "걸렸습니다.", "start_ms": 550, "dur_ms": 26, "phase_lr_deg": -4.8}
]
},
{
"segment": 6,
"transcript": "Your effort really paid off today.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "Your", "start_ms": 45, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "effort", "start_ms": 135, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "really", "start_ms": 255, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "paid", "start_ms": 375, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "off", "start_ms": 465, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "today.", "start_ms": 535, "dur_ms": 41, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "그렇게 봐주시니 다행이네요. 감사합니다.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "그렇게", "start_ms": 35, "dur_ms": 110, "phase_lr_deg": -5.0},
{"word": "봐주시니", "start_ms": 155, "dur_ms": 130, "phase_lr_deg": -5.1},
{"word": "다행이네요.", "start_ms": 295, "dur_ms": 150, "phase_lr_deg": -4.9},
{"word": "감사합니다.", "start_ms": 455, "dur_ms": 121, "phase_lr_deg": -5.0}
]
},
{
"segment": 8,
"transcript": "다음 프로젝트도 기대가 많이 됩니다.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "다음", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "프로젝트도", "start_ms": 170, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "기대가", "start_ms": 330, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 450, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "됩니다.", "start_ms": 540, "dur_ms": 36, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "부담되지만 더 열심히 해볼게요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "부담되지만", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": -4.5},
{"word": "더", "start_ms": 230, "dur_ms": 60, "phase_lr_deg": -4.6},
{"word": "열심히", "start_ms": 300, "dur_ms": 130, "phase_lr_deg": -4.4},
{"word": "해볼게요.", "start_ms": 440, "dur_ms": 136, "phase_lr_deg": -4.5}
]
},
{
"segment": 10,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 11,
"transcript": "질문 있으신 분 계신가요?",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "질문", "start_ms": 60, "dur_ms": 110, "phase_lr_deg": 2.5},
{"word": "있으신", "start_ms": 180, "dur_ms": 120, "phase_lr_deg": 2.6},
{"word": "분", "start_ms": 310, "dur_ms": 60, "phase_lr_deg": 2.4},
{"word": "계신가요?", "start_ms": 380, "dur_ms": 196, "phase_lr_deg": 2.5}
]
},
{
"segment": 12,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 13,
"transcript": "제가 한 가지만 여쭤봐도 될까요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "제가", "start_ms": 25, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 125, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "가지만", "start_ms": 195, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "여쭤봐도", "start_ms": 315, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "될까요?", "start_ms": 475, "dur_ms": 101, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "네, 얼마든지 말씀하세요.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 50, "dur_ms": 60, "phase_lr_deg": -5.1},
{"word": "얼마든지", "start_ms": 120, "dur_ms": 150, "phase_lr_deg": -5.2},
{"word": "말씀하세요.", "start_ms": 280, "dur_ms": 296, "phase_lr_deg": -5.0}
]
},
{
"segment": 15,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 16,
"transcript": "기술적인 한계는 어떻게 극복하셨나요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "기술적인", "start_ms": 45, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "한계는", "start_ms": 205, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 325, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "극복하셨나요?", "start_ms": 445, "dur_ms": 131, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "팀원들과 밤새 토론하며 해결책을 찾았습니다.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "팀원들과", "start_ms": 35, "dur_ms": 140, "phase_lr_deg": -5.3},
{"word": "밤새", "start_ms": 185, "dur_ms": 90, "phase_lr_deg": -5.4},
{"word": "토론하며", "start_ms": 285, "dur_ms": 130, "phase_lr_deg": -5.2},
{"word": "해결책을", "start_ms": 425, "dur_ms": 110, "phase_lr_deg": -5.3},
{"word": "찾았습니다.", "start_ms": 545, "dur_ms": 31, "phase_lr_deg": -5.4}
]
},
{
"segment": 18,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 19,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 20,
"transcript": "정말 열정이 대단하시네요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 60, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "열정이", "start_ms": 160, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "대단하시네요.", "start_ms": 300, "dur_ms": 276, "phase_lr_deg": 0.0}
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
"transcript": "Teamwork makes the dream work.",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Teamwork", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": -4.2},
{"word": "makes", "start_ms": 230, "dur_ms": 110, "phase_lr_deg": -4.3},
{"word": "the", "start_ms": 350, "dur_ms": 60, "phase_lr_deg": -4.1},
{"word": "dream", "start_ms": 420, "dur_ms": 110, "phase_lr_deg": -4.2},
{"word": "work.", "start_ms": 540, "dur_ms": 36, "phase_lr_deg": -4.3}
]
},
{
"segment": 23,
"transcript": "좋은 답변 감사합니다.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "답변", "start_ms": 170, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "감사합니다.", "start_ms": 300, "dur_ms": 276, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "이제 세션 마무리하겠습니다. 수고하셨습니다.",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 35, "dur_ms": 110, "phase_lr_deg": 2.5},
{"word": "세션", "start_ms": 155, "dur_ms": 110, "phase_lr_deg": 2.6},
{"word": "마무리하겠습니다.", "start_ms": 275, "dur_ms": 210, "phase_lr_deg": 2.4},
{"word": "수고하셨습니다.", "start_ms": 495, "dur_ms": 81, "phase_lr_deg": 2.5}
]
},
{
"segment": 25,
"transcript": "That's all for today. Thank you for attending.",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "That's", "start_ms": 40, "dur_ms": 90, "phase_lr_deg": 2.8},
{"word": "all", "start_ms": 140, "dur_ms": 60, "phase_lr_deg": 2.7},
{"word": "for", "start_ms": 210, "dur_ms": 60, "phase_lr_deg": 2.9},
{"word": "today.", "start_ms": 280, "dur_ms": 110, "phase_lr_deg": 2.8},
{"word": "Thank", "start_ms": 400, "dur_ms": 80, "phase_lr_deg": 2.7},
{"word": "you", "start_ms": 490, "dur_ms": 40, "phase_lr_deg": 2.9},
{"word": "for", "start_ms": 540, "dur_ms": 20, "phase_lr_deg": 2.8},
{"word": "attending.", "start_ms": 570, "dur_ms": 6, "phase_lr_deg": 2.7}
]
},
{
"segment": 26,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 27,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 28,
"transcript": "안녕하세요 처음 뵙겠습니다",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 65, "dur_ms": 220, "phase_lr_deg": 4.5},
{"word": "처음", "start_ms": 305, "dur_ms": 120, "phase_lr_deg": 4.6},
{"word": "뵙겠습니다", "start_ms": 435, "dur_ms": 141, "phase_lr_deg": 4.4}
]
},
{
"segment": 29,
"transcript": "반가워요 저는 지민이라고 해요",
"speaker": "speaker_5",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "반가워요", "start_ms": 45, "dur_ms": 160, "phase_lr_deg": 5.1},
{"word": "저는", "start_ms": 215, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "지민이라고", "start_ms": 335, "dur_ms": 150, "phase_lr_deg": 5.2},
{"word": "해요", "start_ms": 495, "dur_ms": 81, "phase_lr_deg": 5.1}
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
"transcript": "성함이 어떻게 되시나요?",
"speaker": "speaker_5",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "성함이", "start_ms": 35, "dur_ms": 140, "phase_lr_deg": 5.4},
{"word": "어떻게", "start_ms": 185, "dur_ms": 110, "phase_lr_deg": 5.3},
{"word": "되시나요?", "start_ms": 305, "dur_ms": 271, "phase_lr_deg": 5.5}
]
},
{
"segment": 32,
"transcript": "제 이름은 마이클입니다.",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "제", "start_ms": 50, "dur_ms": 60, "phase_lr_deg": 4.1},
{"word": "이름은", "start_ms": 120, "dur_ms": 150, "phase_lr_deg": 4.2},
{"word": "마이클입니다.", "start_ms": 280, "dur_ms": 296, "phase_lr_deg": 4.0}
]
},
{
"segment": 33,
"transcript": "한국어 공부한 지 얼마나 되셨어요?",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 40, "dur_ms": 130, "phase_lr_deg": 5.2},
{"word": "공부한", "start_ms": 180, "dur_ms": 120, "phase_lr_deg": 5.3},
{"word": "지", "start_ms": 310, "dur_ms": 40, "phase_lr_deg": 5.1},
{"word": "얼마나", "start_ms": 360, "dur_ms": 110, "phase_lr_deg": 5.2},
{"word": "되셨어요?", "start_ms": 480, "dur_ms": 96, "phase_lr_deg": 5.3}
]
},
{
"segment": 34,
"transcript": "이제 일 년 정도 되었습니다.",
"speaker": "speaker_4",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 55, "dur_ms": 110, "phase_lr_deg": 4.4},
{"word": "일", "start_ms": 175, "dur_ms": 60, "phase_lr_deg": 4.5},
{"word": "년", "start_ms": 245, "dur_ms": 40, "phase_lr_deg": 4.3},
{"word": "정도", "start_ms": 295, "dur_ms": 110, "phase_lr_deg": 4.4},
{"word": "되었습니다.", "start_ms": 415, "dur_ms": 161, "phase_lr_deg": 4.5}
]
},
{
"segment": 35,
"transcript": "한국말을 참 잘하시네요!",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "한국말을", "start_ms": 30, "dur_ms": 180, "phase_lr_deg": 5.6},
{"word": "참", "start_ms": 220, "dur_ms": 60, "phase_lr_deg": 5.5},
{"word": "잘하시네요!", "start_ms": 290, "dur_ms": 286, "phase_lr_deg": 5.6}
]
},
{
"segment": 36,
"transcript": "아직 멀었습니다 더 열심히 해야죠.",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "아직", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 4.7},
{"word": "멀었습니다", "start_ms": 165, "dur_ms": 180, "phase_lr_deg": 4.8},
{"word": "더", "start_ms": 355, "dur_ms": 60, "phase_lr_deg": 4.6},
{"word": "열심히", "start_ms": 425, "dur_ms": 110, "phase_lr_deg": 4.7},
{"word": "해야죠.", "start_ms": 545, "dur_ms": 31, "phase_lr_deg": 4.8}
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
"transcript": "어떤 계기로 공부를 시작하셨나요?",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "어떤", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 5.1},
{"word": "계기로", "start_ms": 170, "dur_ms": 110, "phase_lr_deg": 5.2},
{"word": "공부를", "start_ms": 290, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "시작하셨나요?", "start_ms": 410, "dur_ms": 166, "phase_lr_deg": 5.1}
]
},
{
"segment": 39,
"transcript": "한국 드라마를 좋아해서 시작하게 됐어요.",
"speaker": "speaker_4",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "한국", "start_ms": 60, "dur_ms": 90, "phase_lr_deg": 4.2},
{"word": "드라마를", "start_ms": 160, "dur_ms": 140, "phase_lr_deg": 4.3},
{"word": "좋아해서", "start_ms": 310, "dur_ms": 110, "phase_lr_deg": 4.1},
{"word": "시작하게", "start_ms": 430, "dur_ms": 110, "phase_lr_deg": 4.2},
{"word": "됐어요.", "start_ms": 550, "dur_ms": 26, "phase_lr_deg": 4.3}
]
},
{
"segment": 40,
"transcript": "K-dramas are very addictive, aren't they?",
"speaker": "speaker_5",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "K-dramas", "start_ms": 35, "dur_ms": 180, "phase_lr_deg": 5.5},
{"word": "are", "start_ms": 225, "dur_ms": 60, "phase_lr_deg": 5.4},
{"word": "very", "start_ms": 295, "dur_ms": 110, "phase_lr_deg": 5.6},
{"word": "addictive,", "start_ms": 415, "dur_ms": 110, "phase_lr_deg": 5.5},
{"word": "aren't", "start_ms": 535, "dur_ms": 30, "phase_lr_deg": 5.4},
{"word": "they?", "start_ms": 575, "dur_ms": 1, "phase_lr_deg": 5.6}
]
},
{

---
*Generated by WKAppBot ask gemini — 2026-03-31 07:04:23*
