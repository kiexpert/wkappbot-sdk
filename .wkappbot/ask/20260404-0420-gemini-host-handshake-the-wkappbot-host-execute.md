# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=17688  time=04:18:56  uptime=10819s  mem=179MB  system_processes=372"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260404_041737.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 263-623-263-263-263 263-230 576-572 765-765-756-765-675-765-362-621 765-672-627-672 751-176-160-610 601-610-603-716-671-613-165-367-136 103-612-163-103-103-103-106-106-163-106-1_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 627-163-637-531-517-531-154-132-103-132-507-504-507-507-504-132-576-257-356-256-357-265-325-632-675-625-265-652-362-625-652-652-625-653-365-356-625-362-362-567-574-356-576-375-3_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 236-362-526-652-563-652-267-325-635-253-235-360-326-326-362-306-623-263-321-263-627-762-763-763-627-231-203-236-203-260-672-672-670-673-361-206-620-621-231-120-761-176-163-570-7_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 617-123-176-765-765-617-761-762-671-675-172-762-670-762-601-621-671-675-671-672-675-675-206-623-625-615-657-260-675-527-571-576-756-752-765-765-761-765-762-576-567-527-675 654-6_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 532-627-627-630-306-307-562-536-127-710-706-621-735-531-571-251-265-267-276-267 263-236-326-326_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 160-621-620-263-320-302-230-106-625-675-631-312-301-302-132-326-726-203-326-132-106-130-103-312-316-321-236-603-103-136-103-657-627-260-602 261 265-231-236-753-376-316_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 213-123-216-213-132-137-137-612-267-276-176-173-103-316-130-132 572-756-730-167-671-617 765-763-765-765-670-132-137-135-137-123-312-130-316-103-562-136-136-710-103-312-365-316-7_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 547-576-574 576-574-574-574-576-574-574-574-574-574-572-574-576-570-574-574-574-574-574 576-571-576-574-576-574-574-574-574-574-574-574-571-570-574-570-574-574-576 574-574-574-5_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 162-160-103-136-126-312-136-361-130-176-136-103-130-103-517-516-672-103-316-361-163-106-163-103-103-132-761-176-164-103-103-136-153-136-176-160-136-103-163-765-263-132-126 136 1_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 132-132-613-263-362-365-765-576-753-572-752-103 106-103-103-136-132-103-152-716-132-130-531-163-136-135-137-130-136-621-136-673-126-103-137-176-301-130-132-106-765-167-156-102-1_V.mp3)
  Segment 11: 15760ms ~ 22672ms (file: 126-162-263-625-627-265-265-265-265-210-213-236-230-203-230-624-260-536-132-320-256-321-231-236-267-236-236-321-120-231-263-265-260-261-265-265-625-263-120-261-103-102-213-126-1_W.mp3)
  Segment 12: 23672ms ~ 24248ms (file: 306-103-301-132-132-160-106-150-103-216-271-312-201-230-132-213-267-607-627-620-267-672-607-547-675-276-276-627-167-301-312-103-627-102-310-310-310-106-517-756-753-507-570-320-2_W.mp3)
  Segment 13: 25248ms ~ 25824ms (file: 650-657-657 650-651-650-650-605-605-652-650-652-605-601-620 605-605-605-650-605-605-605-605-650-605-601 650-650-560-605-605-650-650-650-650-650-650-650 605-650-605-650-605-650-6_W.mp3)
  Segment 14: 26824ms ~ 35248ms (file: 312-173-120-137-236-326-312-213-321-123-172-765-367-301-310-751-276-236-267-236-275-752-267-317-137-127-752-621-632-751-753-321-725-312-231-756-765-751-107-725-231-136-231-236-3_W.mp3)
  Segment 15: 36248ms ~ 36824ms (file: 756-765-765-765-765-765-765-675-610-103-536-312 162-163-613-261-106-126-230-130-126-210-162-263-163-106-106-672-126-516-231-320-163-160-607-302-301-132-102-312 316 321-132-261-2_V.mp3)
  Segment 16: 37824ms ~ 38400ms (file: 627-267 210-230-231-216-216-267-231-617-261 312 267-267 261-627-267_W.mp3)
  Segment 17: 39400ms ~ 39976ms (file: 675-672-631 136-103-103-175-106-351-621-123-162-762-672-136 213 716-163-602-162-126-106-260-367-317-627-632-236-136-163-106-610-163-136-163-610-160-361-165-173-162-130-160-261-6_W.mp3)
  Segment 18: 40976ms ~ 41552ms (file: 753-570-157-517-254-254-362-261-263-216-237-236-327-275-257-235-265-275-276-253-237-237-237-254-524-254-265-265-265-236-326-326-326-263-236-326-231-216-230-326-236-236-264-230-2_W.mp3)
  Segment 19: 42552ms ~ 43128ms (file: 316-132-623-312-362-632-312-361 671-765 231-236-231-612-216-261 132-216-103-613-760-367-370-623-230-260-570-576-765-675-675-756-176-762-602-601-607-672-617-126-162-317-750-761-6_W.mp3)
  Segment 20: 44128ms ~ 49096ms (file: 763-276-726-720-706-725-271-275-276-276-736-726-765-762-732-317-372-574-752-317-316-267-710-712-756-756-756-765-576-135-375-756-756-765-756-762-726-726-732-271-574-572-273-732-7_W.mp3)
  Segment 21: 50096ms ~ 50672ms (file: 756-627-236-326 672-362-623-326-326-361 751-126-162-167-631-263-602-132-261-103-136-106-130-136-516-103-106-103-106-132-130-163-103-657-516-130-310-316-132-136-765-136-657-103-3_V.mp3)
  Segment 22: 51672ms ~ 52248ms (file: 574-571-571-576-576-576-305-310-315-652-105-135-125-132-312-132-302-130-571-572-573-571-573-567-125-756-751-160-365-365-136-167-615-157-576-756-705-301-103-135-135-312 157-136_W.mp3)
  Segment 23: 53248ms ~ 53824ms (file: 613-607-167-671-310-106-106-501-135-163-132-236-362-230-301-310-310-137-130-317-130-103-137-173-167-106-103-106-136-167-617-167-167-236-206-267-261-126-513-102-715-562-613-612-1_V.mp3)
  Segment 24: 54824ms ~ 55400ms (file: 326-362-176-136-156-210-165-132-136-103-612-136-701-137-107-712-731-137-617-627-613-617-672-137-132-136-137-136-135-156-103-106-103-631-163-350-130-132-165-316-751-136-162-103-1_V.mp3)
  Segment 25: 56400ms ~ 56976ms (file: 157-156-136-136-546-167-163-136-130-130-103-106-517-163-136-136-106-263-623-103-136-136-160-130-136-136-163-163-316-103-136-612-136-103-173-145-623-206-132-302-176-621-163-176-1_V.mp3)
  Segment 26: 57976ms ~ 66220ms (file: 230-102-132-102-751-765-627-102-207-310-321-132-123-127-201-201-572-257-253-230-102-231-672-267-627-572-210-120-203-230-752-725-716-132-130-701-725-712-130-231-203-210-753-752-1_W.mp3)
  Segment 27: 67220ms ~ 67796ms (file: 517-153-315-130-317-175-137-315-375-731-301-175-137-751-157-105-517-107-157-175-310-137-731-371 513-135-315-137-135-310-103-137-175-130-301-317-372-107-137-107-175-713-731-137-7_V.mp3)
  Segment 28: 68796ms ~ 69372ms (file: 765-756-765-576-657-765-765-765-576-752-756-574-750-672-167-612-762-267-625-621 130-301 621-620-621-675 671-617-716-631 675-762 362 627-723-623-726-762-126-216 672-762 761-756-2_W.mp3)
  Segment 29: 70372ms ~ 70948ms (file: 526-627-267-256-263-362-136-316-362-361-631-631-362-657-326-326-365-621-617-356-567-365-153-673-635-367-567-567-536-567-357-635-564-574-256-657-675-716-612-365-632-263-623-263-2_W.mp3)
  Segment 30: 71948ms ~ 72524ms (file: 617-612 612-361-632-631-637-736-632-631-632-165-173-137-671-621-623-632-612-671-761-617-631-621-625-627-671-673-316-316-163-136-623-627-263-326-673-672-617-712-617-213-726-267-6_W.mp3)
  Segment 31: 73524ms ~ 74100ms (file: 574-567-123-572-537-267-237-562-273-172-507-235-576-276-756-257-753-572-267-673-632-265-276-275 752-275-726-756-362-672-657-751-576-265-752-257-572-217-267-126-261-321-172-623-5_W.mp3)
  Segment 32: 75100ms ~ 75676ms (file: 675-675-765-675-657-657-657-567-675-567-576-765-657-567-657-657-567-657-576-657-567-657-567-657-567-657-563-657-365-615-657-765-675-765-576-675-567-657-765-517-576-567-765-572-6_W.mp3)
  Segment 33: 76676ms ~ 77252ms (file: 307-312-316-301-132-106-163-106-107-106-167-167-267 670-607-602-621-625-265-260-162-316-675-675-675-675-602-623-607-625-210-312-321-302-102-132-136-126-132-312-306-163-301-162-5_V.mp3)
  Segment 34: 78252ms ~ 78828ms (file: 705-230-302-302-230-362-132-231-251-301-305-567-236-263-652-627-652-627-371-136-516-312-302-310-103-130-165-103-657-362-657-162-673-672-167-635-657-765-657-675-675-132-627-267-6_W.mp3)
  Segment 35: 79828ms ~ 80404ms (file: 216-216-217-216-130 167-672-136-371-367-176-167-671-176-715-672-623-673-627-631-367-627-632-672-675-765-675-765-675-263-327-672-120-607-617-260-675-675-736-132-362-360-361-657-6_W.mp3)
  Segment 36: 81404ms ~ 87920ms (file: 756-576-152-103-273-206-620-627-132-362-571-576-157-105-620-162-261-261-502-156-671-675-167-163-136-137-165-517-570-130-320-627-756-103-130-731-107-153-132-165-564-156-725-162-3_V.mp3)
  Segment 37: 88920ms ~ 89496ms (file: 321-321-321-321-326-263-326-675-623-632-361-362-230-263-623-263-526-235-231-613-627-625-620-623-620-263-623-263-263-256-524-625-627-627-623-263-267-267-627-562-562-536-137-317-1_W.mp3)
  Segment 38: 90496ms ~ 91072ms (file: 627-162-261-621-162-163-261-263-213-213-216-263-123 567-103-310_W.mp3)
  Segment 39: 92072ms ~ 92648ms (file: 135-310-175-351-301 135-132-135-135-130-153-135-153-157-135-135-531-135-317-351-537-173-731-103-103-137-317-103 312-132-301-135-135-103-130-132-135-103-137-153-315-103-103-315-3_V.mp3)
  Segment 40: 93648ms ~ 94224ms (file: 103-132-103-132-173-103-107-176-105-137-103-763-760-761-760-312-321-617-163-637-657-657-625-675-273-162-102-215-216-167-162-721-167-526-156-167-326-316-361-362-162-561-652-132-1_V.mp3)
  Segment 41: 95224ms ~ 95800ms (file: 123-130-216-276-621-206-136-160-106-710-163-256-567-763-670-620-260-260-326-230-302-320-310-321-126-162-126-163-321-132-360-316-567-516-317-501-301-756-754-705 163-670-613-623-6_W.mp3)
  Segment 42: 96800ms ~ 97376ms (file: 267-627-621-126-136-236-321-163-621-261-263-623-261-263-136-621 102-162-671-163-123-137-102-210-312-316-637-725-736-576-105-162-216-130-213-321-317-123-132-321-132-316-162-506-5_W.mp3)
  Segment 43: 98376ms ~ 98952ms (file: 135-135-315-317-315-135-103-315-301-135-317-573-135-301-357-153-517-573-310 317-103-135-317-103-315-135-103-315-137-312-135-103-315-135-175-153-315-751-315-571-537-307-301 135-1_V.mp3)
  Segment 44: 99952ms ~ 100528ms (file: 103-576-610-564-756-540-541-501-647-501-567-574-576-572-754-765-627-675-675-672-126-576-563-527-546-576-574-574 312-231-617-361-563 136-123-127_W.mp3)
  Segment 45: 101528ms ~ 126620ms (file: 260-261-573-257-752-267-230-120 267-236-236-236-263-230 260-267-267-267-260-260-267-276-206-263-236-260-260-260-203-231-523 136 376-375-763-753-765-675-763-736-753-376-317-736-7_W.mp3)
  Segment 46: 127620ms ~ 128196ms (file: 163-132-137-516-136-671-610-130-136-163-103-136-130-106-136-130-130-130-610-165-103-103-167-627-623-672-632-625-236-362-623-621-632-637-623-632-312 362-637-617-136-163-123-673-6_W.mp3)
  Segment 47: 129196ms ~ 129772ms (file: 761-760 623-612-627-627-612-621-612-126-216-103-627-627-176-675-672-632 106-132-103-316-162-362-316-136-136-312-136-216-136-103-130-567-675-627-756-765-652-675-627 321 135-576-5_W.mp3)
  Segment 48: 130772ms ~ 131348ms (file: 671-672-672-576-567-301-301 576 103-130-103-132-632-632-623-261-236-213-123-132-267-621-627-132-312-301-103-321-130 263-261-136-765-567-536-576-576-752-527-165-672-162-613-321_W.mp3)
  Segment 49: 132348ms ~ 132924ms (file: 216-316-126-326-613-256-123-231-231-132-123-263-312-213-765-265-657-625-657-156-567-263-623 607-627-627-627-627-576-160-126-263-173-172 103-136-163-136-623-613-126-627-263-627 6_W.mp3)
  Segment 50: 133924ms ~ 144436ms (file: 536-637-320-126-536-237-321-270-260-267-672-165-362-637-326-130-260-267-206-752-207-276-756-765-756-752-271-176-732-736-627-756-260-736-752-275-276-356-301-213-126-321-321-236-2_W.mp3)
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
> **Time**: 2026-04-04 04:20:44
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "우리가 흔히 과거에 매달리는 동안 미국과 중국은 세상을 바꾸는 기업을 세우고 매출과 일자리를 폭발시키고 있습니다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "우리가", "start_ms": 20, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "흔히", "start_ms": 480, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "과거에", "start_ms": 810, "dur_ms": 510, "phase_lr_deg": 0.0},
{"word": "매달리는", "start_ms": 1330, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "동안", "start_ms": 1960, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "미국과", "start_ms": 2350, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "중국은", "start_ms": 2940, "dur_ms": 590, "phase_lr_deg": 0.0},
{"word": "세상을", "start_ms": 3540, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "바꾸는", "start_ms": 4030, "dur_ms": 470, "phase_lr_deg": 0.0},
{"word": "기업을", "start_ms": 4510, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "세우고", "start_ms": 5040, "dur_ms": 530, "phase_lr_deg": 0.0},
{"word": "매출과", "start_ms": 5580, "dur_ms": 610, "phase_lr_deg": 0.0},
{"word": "일자리를", "start_ms": 6200, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "폭발시키고", "start_ms": 6630, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 7050, "dur_ms": 290, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "박사 학위의 가치가 이제 월 300만 원짜리 리더십 교육으로 대체되는 세상입니다.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "박사", "start_ms": 10, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "학위의", "start_ms": 370, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "가치가", "start_ms": 800, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 1290, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "월", "start_ms": 1610, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "300만", "start_ms": 1830, "dur_ms": 490, "phase_lr_deg": 0.0},
{"word": "원짜리", "start_ms": 2330, "dur_ms": 440, "phase_lr_deg": 0.0},
{"word": "리더십", "start_ms": 2780, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "교육으로", "start_ms": 3270, "dur_ms": 590, "phase_lr_deg": 0.0},
{"word": "대체되는", "start_ms": 3870, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "세상입니다", "start_ms": 4400, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "어떻게 하면 부의 추월차선에 올라탈 수 있을까 하는 생각을 늘 하였고 꾸준한 수익을 낼 수 있는 가능성이 있지 않을까 생각하여 신청하게 되었는데 정말",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "어떻게", "start_ms": 40, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "하면", "start_ms": 430, "dur_ms": 290, "phase_lr_deg": 0.0},
{"word": "부의", "start_ms": 730, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "추월차선에", "start_ms": 1050, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "올라탈", "start_ms": 1740, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 2200, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "있을까", "start_ms": 2390, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 2810, "dur_ms": 290, "phase_lr_deg": 0.0},
{"word": "생각을", "start_ms": 3110, "dur_ms": 390, "phase_lr_deg": 0.0},
{"word": "늘", "start_ms": 3510, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "하였고", "start_ms": 3800, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "꾸준한", "start_ms": 4290, "dur_ms": 460, "phase_lr_deg": 0.0},
{"word": "수익을", "start_ms": 4760, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "낼", "start_ms": 5250, "dur_ms": 190, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 5450, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 5640, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "가능성이", "start_ms": 5930, "dur_ms": 590, "phase_lr_deg": 0.0},
{"word": "있지", "start_ms": 6530, "dur_ms": 290, "phase_lr_deg": 0.0},
{"word": "않을까", "start_ms": 6830, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "생각하여", "start_ms": 7220, "dur_ms": 560, "phase_lr_deg": 0.0},
{"word": "신청하게", "start_ms": 7790, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "되었는데", "start_ms": 8380, "dur_ms": 560, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 8950, "dur_ms": 470, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "우리가 꿈꾸는 게 실현되는 게 그렇게 많지가 않거든요. 진짜 많이 준비를 해오세요. 한 번의 기회가 참 진짜",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "우리가", "start_ms": 30, "dur_ms": 390, "phase_lr_deg": 0.0},
{"word": "꿈꾸는", "start_ms": 430, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 920, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "실현되는", "start_ms": 1110, "dur_ms": 590, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 1710, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "그렇게", "start_ms": 1900, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "많지가", "start_ms": 2290, "dur_ms": 490, "phase_lr_deg": 0.0},
{"word": "않거든요", "start_ms": 2790, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 3380, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 3770, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "준비를", "start_ms": 4160, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "해오세요", "start_ms": 4650, "dur_ms": 570, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 5230, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "번의", "start_ms": 5420, "dur_ms": 290, "phase_lr_deg": 0.0},
{"word": "기회가", "start_ms": 5720, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "참", "start_ms": 6210, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 6500, "dur_ms": 380, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "KF-21은 어느 정도 수준의 전투기일까요? 한국 공군의 위치 설정은 매우 분명합니다. 이 기체는 F-16과 F-35 사이에 놓인 4.5세대 전투기, 혹은 준 5세대 전투기로 불립니다. 절대적인 스텔스 성능과",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "KF-21은", "start_ms": 15, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "어느", "start_ms": 710, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "정도", "start_ms": 1000, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "수준의", "start_ms": 1390, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "전투기일까요", "start_ms": 1880, "dur_ms": 780, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 2680, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "공군의", "start_ms": 3070, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "위치", "start_ms": 3560, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "설정은", "start_ms": 3950, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "매우", "start_ms": 4540, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "분명합니다", "start_ms": 4930, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 5620, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "기체는", "start_ms": 5810, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "F-16과", "start_ms": 6300, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "F-35", "start_ms": 7000, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "사이에", "start_ms": 7690, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "놓인", "start_ms": 8180, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "4.5세대", "start_ms": 8570, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "전투기", "start_ms": 9260, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "혹은", "start_ms": 9850, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "준", "start_ms": 10240, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "5세대", "start_ms": 10430, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "전투기로", "start_ms": 11020, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "불립니다", "start_ms": 11610, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "절대적인", "start_ms": 12200, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "스텔스", "start_ms": 12890, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "성능과", "start_ms": 13380, "dur_ms": 580, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "출금해 네가 6000으로도 2억 넘게 버는데 더 하면 뭐 하냐 그리고 너 같은 스타일은 미수 풀이라서 무조건 출금해야 돼 출금해서 통장",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "출금해", "start_ms": 35, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "네가", "start_ms": 530, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "6000으로도", "start_ms": 820, "dur_ms": 780, "phase_lr_deg": 0.0},
{"word": "2억", "start_ms": 1610, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "넘게", "start_ms": 1900, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "버는데", "start_ms": 2290, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 2780, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "하면", "start_ms": 2970, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 3360, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "하냐", "start_ms": 3550, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 3940, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "너", "start_ms": 4330, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "같은", "start_ms": 4520, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "스타일은", "start_ms": 4810, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "미수", "start_ms": 5400, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "풀이라서", "start_ms": 5690, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "무조건", "start_ms": 6280, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "출금해야", "start_ms": 6770, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "돼", "start_ms": 7360, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "출금해서", "start_ms": 7550, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "통장", "start_ms": 8140, "dur_ms": 380, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "We completely missed each other at the airport.",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "We", "start_ms": 110, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "completely", "start_ms": 300, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "missed", "start_ms": 890, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "each", "start_ms": 1280, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "other", "start_ms": 1570, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "at", "start_ms": 1960, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 2150, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "airport", "start_ms": 2340, "dur_ms": 580, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 9,
"transcript": "경남 사천의 한국항공우주산업의 대형 공장은 축구장 3개에 맞먹는 규모를 자랑하며 KF-21과 FA-50 그리고 T-50 등의 기종을 동시에 전력 생산하고 있습니다. 생산 능력은 놀라울 정도입니다. 이들은 올해 27대의 항공기를 인도할 계획이며 내년",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "경남", "start_ms": 15, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "사천의", "start_ms": 310, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "한국항공우주산업의", "start_ms": 700, "dur_ms": 1180, "phase_lr_deg": 0.0},
{"word": "대형", "start_ms": 1890, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "공장은", "start_ms": 2280, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "축구장", "start_ms": 2770, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "3개에", "start_ms": 3260, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "맞먹는", "start_ms": 3750, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "규모를", "start_ms": 4240, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "자랑하며", "start_ms": 4730, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "KF-21과", "start_ms": 5320, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "FA-50", "start_ms": 6010, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 6700, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "T-50", "start_ms": 7090, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "등의", "start_ms": 7680, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "기종을", "start_ms": 7970, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "동시에", "start_ms": 8460, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "전력", "start_ms": 8950, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "생산하고", "start_ms": 9340, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 9930, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "생산", "start_ms": 10520, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "능력은", "start_ms": 10910, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "놀라울", "start_ms": 11400, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "정도입니다", "start_ms": 11890, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "이들은", "start_ms": 12580, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "올해", "start_ms": 13070, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "27대의", "start_ms": 13460, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "항공기를", "start_ms": 14050, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "인도할", "start_ms": 14640, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "계획이며", "start_ms": 15130, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "내년", "start_ms": 15720, "dur_ms": 380, "phase_lr_deg": 0.0}
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
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-04 04:20:44*
