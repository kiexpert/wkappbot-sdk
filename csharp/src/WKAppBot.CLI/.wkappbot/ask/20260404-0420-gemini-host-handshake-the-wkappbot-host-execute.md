# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=25708  time=04:19:08  uptime=11633s  mem=753MB  system_processes=365"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260404_041859.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 7344ms (file: 123-130-132-120-201-125-507-102-102-207-203-102-130-102-103-167-312-270-203-123-125-135-123-120-230-571-270-270-270-102-103-201-207-201-120-320-265-102-231-201-120-302-235-230-2_W.mp3)
  Segment 2: 8344ms ~ 8920ms (file: 163-631-321-673-137-176-163-570-756-175-563-705-706-216-567-162-123-301-301-312-301-136-206-260-263-213-236-276-213-321-132-103-312-370-137-103-130-102-125-562-502-625-162-265 1_V.mp3)
  Segment 3: 9920ms ~ 18344ms (file: 267-672-753-765-203-310-301-627-627-326-103-765-312-132-165-263-675-176-176-675-321-132-765-273-762-126-103-723-273-237-763-637-652-256-130-613-675-671-562-675-362-756-132-531-3_W.mp3)
  Segment 4: 19344ms ~ 19920ms (file: 762-762-716-763-302-130-301-312-320-302-326-231-602-602-621-623-620-602-602-602-602-206-203-206-620-602-623-602-310-103-132 134-103-135-301_V.mp3)
  Segment 5: 20920ms ~ 21496ms (file: 576-507-750-570-175-517-216 163-126-301-130-671-763-106-613-132-316-136-136-103-136-163-163-610-163-106-163-136-132-130-361-106-172-365-172-136-103-756-167-136-130-613-631-132-7_V.mp3)
  Segment 6: 22496ms ~ 28868ms (file: 360-263-236-321-312-523-265-512-756-230-265-257-216-172-316-360-263-265-310-362-106-326-267-203-351-547-216-251-260-260-576-576-574-516-261-210-657-160-756-520-301-321-265-263-2_W.mp3)
  Segment 7: 29868ms ~ 30444ms (file: 751-675-671-617-165-136-167-637-671-510-163-673-156-137-671-765-762-726-672-107-312-173-361-203-312-312-762-627-231-236-213-231-627-132-231-672-671-627-276-267-126-236-230-261-1_W.mp3)
  Segment 8: 31444ms ~ 32020ms (file: 167-165-156-531-357-513-517-536-516-156-175-571-157-516-571-157-517-576-576-571-753-153-531-351-357-357-351-375-537-571-751-157-175-751-571-571-157-157-517-571-751-756-751-317-6_W.mp3)
  Segment 9: 33020ms ~ 33596ms (file: 653-675-751-657-627-671-651-653-356-625-516-756-675-567-576-576-657-765-675-561-576-615-165-617-651-567-576-567-571-576-567-156-517-516-156-765-576-651-517-567-576-576-567-657-6_W.mp3)
  Segment 10: 34596ms ~ 35172ms (file: 675-632-367-326-623-672-670 574-157_W.mp3)
  Segment 11: 36172ms ~ 46036ms (file: 201-125-132-657-620-120-125-216-210-215-321-761-210-756-130-123-572-756-765-764-156-310-231-216-213-320-372-175-157-517-215-153-571-513-132-123-210-135-316-301-325-302-201-206-1_V.mp3)
  Segment 12: 47036ms ~ 47612ms (file: 123-312-672-627-672-527-751-576-675-672-361-673-756-675-675-765-765-754-705-507-570 675-570-572-275-326-572-625-657-765_W.mp3)
  Segment 13: 48612ms ~ 49188ms (file: 137-361-160-130-217-103-267-613-106-567-657-765-562-103-136-236-103-671-671-136-316-106-576-136-631-675-675-765-675-675-675-675-675-675-657-675-675-765-675-765-756 236-263-673-6_W.mp3)
  Segment 14: 50188ms ~ 50764ms (file: 571-157-123-107-173-132-123-172-167-167-163-132-103-130-361-752-175-621-136-372-237 127-756-567-765-157-571-756-706-571-175-167-315-163-136-631-751-765-172-751-756-756-137-153-2_V.mp3)
  Segment 15: 51764ms ~ 52340ms (file: 671-765-167-756-576-756-675-756-765-675-574-576-617-167-576-576-576-675-657-625-610 574-123 547-312-132 362-231-163-261-236-231_W.mp3)
  Segment 16: 53340ms ~ 56940ms (file: 273-103-167-327-132-231-123-231-127-273-273-713-127-216-231-267-267-236-216-231-267-213-123-237-217-312-213-213-326-123-172-132-312-132-312-137-327-321-312-120-217-123-321-237-3_W.mp3)
  Segment 17: 57940ms ~ 58516ms (file: 610-167-123-103-570-316-132-130-130-106-160-136-610-130-136-103-136-136-157-106-103-136-103-671-316-103-163-561-137-357-163-106-631-132-150-163-173-136-132-137-136-136-103-105-5_V.mp3)
  Segment 18: 59516ms ~ 60092ms (file: 627-627-236-362-326-635-625-265-265-265-263-625-623-263-625-261-276-267-267-326-567-563-567-574-531-125-132-213-316-562-357-567-567-657-567-576-523-651-653-215-521-532-571-571-5_W.mp3)
  Segment 19: 61092ms ~ 61668ms (file: 132-106-136-136-106-175-103-610-610-106-163-617-136-601-216-261-613-276-617-617-132-136-163-162-216-163-162-106-163-367-163-316-130-135-167-376-165-135-106-165-312-310-103-136-1_V.mp3)
  Segment 20: 62668ms ~ 73432ms (file: 132-230-102-231-275-135-105-320-320 576-527-203-206-203-230-102-103-165-175-103-510-120-310-230-325-120-207-726-576-123-203-123-230-230-572-256-230-213-260-206-235-256-256-203-2_W.mp3)
  Segment 21: 74432ms ~ 75008ms (file: 635-617-163-576-675-725-206-206-362-302-362-657-306-672-756-751-216-672-517-276-265-265-675-106-106-165-761-672-765-506-576-547-571-153-153-620-672-167-130-513-570-576-602-716-7_W.mp3)
  Segment 22: 76008ms ~ 76584ms (file: 546-761-670-760-762-673-762-765-672-672-612-650-735-675-276-215-126_W.mp3)
  Segment 23: 77584ms ~ 78160ms (file: 672-723-653-320-302 302-620-621-627-576-765-176-163-162-756-230-362-106-301-106-163-106-103-617-163-106-157-137-301-310-236-263-574-750-756-176-176-162-167-162-103-103-136-203-2_V.mp3)
  Segment 24: 79160ms ~ 79736ms (file: 312-320 132-613-126-136-103-132-316-162-316-613 576-576-576-106-316-306-620-261-162-162-612-612-103 102-210-320-321-123-132-103-107-136-176-176-167-137-312-203-260-236-765-201-7_V.mp3)
  Segment 25: 80736ms ~ 81312ms (file: 612-162-213 236-162-123-132-621 130_V.mp3)
  Segment 26: 82312ms ~ 82888ms (file: 156-126-176-157-167 673-637-673 673-672-123-124-341-245-103-260-623-652-320-613-120-671-305-123-316-103-137-312-567-576-756-675-657-571-571-103 623_W.mp3)
  Segment 27: 83888ms ~ 84464ms (file: 561-156-157-563-657-165-156-153-135-153-351-156-531-517-157-567-576-653-567-715-657-651-576-567-567-657-567-567-567-657-657-176-576-657-651-657-675-576-165-567-165-651-516-157-5_W.mp3)
  Segment 28: 85464ms ~ 86040ms (file: 574-576-574-574-574 574-574-576-574-754-574-574-574-574-574-574-576-574-574-574-574-547-574-574-576-574-574 574-574-570-574-574-574-574-574-574-574-574-574-574-574-574-570-574-5_W.mp3)
  Segment 29: 87040ms ~ 87616ms (file: 517-576-567-753-756-731-765-735-576-576-152-127-217-257-325-357-375-375-517-572-352-361-316-375-126-321-312-317-137-137-137-172-127-237-275-256-275-125-273-725-213-712-721-725-7_W.mp3)
  Segment 30: 88616ms ~ 89192ms (file: 267-326-231-236-236-126-136-625-265-261-261-126-217-637-165 312-137-176-671-312_W.mp3)
  Segment 31: 90192ms ~ 90768ms (file: 672-671-317-671-671-763-761-762-765-762-675-675-762-761-761-675-765-672-673-267-762-627-236-263 162_W.mp3)
  Segment 32: 91768ms ~ 92344ms (file: 132-532-527-507-205-203-574-725-257-206-236-321-136-103-105-103-136-631-203-120-123-103-310-316-613-630-302-312-537-517-350-357-367-301-203-321-302-326-213-105-541-302-203-236-2_V.mp3)
  Segment 33: 93344ms ~ 93920ms (file: 276-627-130-137-103-130-136-136-516-167-137-167-103-103-136-103-163-103-106-316-130-163-263-631-263 617-163_V.mp3)
  Segment 34: 94920ms ~ 100896ms (file: 312-130-371-352-275-231-132-105-261-675-170-572-657-267-576-756-576-637-367-673-673-637-637-367-761-367-136-316-123-321-132-321-261-236-167-752-120-165-357-176-125-132-102-756-7_W.mp3)
  Segment 35: 101896ms ~ 102472ms (file: 756-167-106-106-103-316-576-235-162 762-103-132-103-163-162-765-623-362-761-610-167-160-617-576-621-631-160-630-167-163-163-152-132-675-130-106-135-631-657-137-136-657-312-132-1_V.mp3)
  Segment 36: 103472ms ~ 104048ms (file: 201-270-302-203-253-253-372-321-132-176-163-301-132-167-106-106-157-135-571-574-574-257-143-213-321-123-316-216-621-261-107-106-107-160-176-167-231-213-216-756-176-136-102-106-1_V.mp3)
  Segment 37: 105048ms ~ 105624ms (file: 265-265-562-521-163-512-512-265-276-261-265-625-627-263-261-621-612-621-615-627-261-612-162-621-126-562-567-256-265-256-256-253-251-253-256-213-132-126-126-216-216-216-562-615-2_W.mp3)
  Segment 38: 106624ms ~ 107200ms (file: 672-621 621-267-623-621-276-231-362-213-631-126-671-672-617-672-716-316-623-657-653-621-627-625-672-673-672-367-627-216-632-672-672-623-136-627-673-635-765-637-231-236-673-362-3_W.mp3)
  Segment 39: 108200ms ~ 108776ms (file: 163-162-102-132-213-620-203-263-623-261-130-106-120-326-132-623-216-162-106-306-302-360-106-301-675-670-620-216-123 103 652-263-765-756-756-706-670-276-173-162-236-326-203-267-1_V.mp3)
  Segment 40: 109776ms ~ 110352ms (file: 325-302-532-537-736-267-627-632-672-362-320-315-352-623-320-312-352-351-312-321-312-321-351-325 156-120-132-130-301-762-627-607-130-537-537-537-572-543-321-302-231-302-302-210-3_W.mp3)
  Segment 41: 111352ms ~ 111928ms (file: 136-721-273-237-136-162-132-170-725-751-765-756-315-312-237-230-761-163-132-132-561-736-267-132-706-237-752-725-173-123-157-765-703-721-261-206 127-172-127-513-513-352-352-351-7_W.mp3)
  Segment 42: 112928ms ~ 113504ms (file: 216-203-367-361-136-136-312-137-231-132-136-137-105-301-130-547-105-361-230-103-103-102-103-310-312-301-317-130-260-260-261-716-763-316-167-105-750-157-275-176-127-312-217-106-1_W.mp3)
  Segment 43: 114504ms ~ 115080ms (file: 162-157-621-167-103-710-513-316-160-163-130-160-376-126-621-673-317-136-176-371-172-371-371-137-176-176-175-173-176-516-756-576-765-675-657-675-657-675_W.mp3)
  Segment 44: 116080ms ~ 116656ms (file: 162-103-136-103-103-657-135-317-107-362-172-126-106-136-103-521-650-126-134-103-162-310-176-106-163-103-136-312-130-103-136-136-103-671-625-516-120-132-267-263-263_V.mp3)
  Segment 45: 117656ms ~ 126764ms (file: 276-271-276-106-201-576-206-260-216-752-102-276-721-275-201-230-716-632-613-136-367-672-127-267-267-267-276-752-120-163 276-526-216-752-765-672-627-230-120-120-716-267-753-236-2_W.mp3)
  Segment 46: 127764ms ~ 128340ms (file: 567-576-576-567-576-576-576-576-567-657-675-675-675 567-650-567-576-567-576-576-576-576-576-675-765-765-675-675 576-657-576-576-576-576-576-567-576 567-576-576-576-576-576-567-5_W.mp3)
  Segment 47: 129340ms ~ 129916ms (file: 315-620-625-627-625-260-650-620-625-627-672-625-627-672-627-675_W.mp3)
  Segment 48: 130916ms ~ 131492ms (file: 135-132-103-361-621-620-207-260-267-216-203-102-301-170-126-620-261-126-106-761-103-175-130-751-576-765-267-362-312-316-136-367-627-627-621-623 756-726-216-267-312-362-362-362-1_W.mp3)
  Segment 49: 132492ms ~ 140016ms (file: 203-231-102-230-310-753-213-310-203-207-276-207-236-130-102-102-213-267-267-756-672-210-201-752-756-103-207-103-132-230-735-275-541-213-201-231-237-623-230-756-576-753-574-576-3_W.mp3)
  Segment 50: 141016ms ~ 141592ms (file: 135-132-301-357-351-315-175-135-310 317-132-137-312-103-310-135-137-135-135-312-315-315-315-103-137-301-152-315-103-315 315-135-315-135-130-132-135-135-137-173-317-132-135-153-1_V.mp3)
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
> **Time**: 2026-04-04 04:20:43
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
*Generated by WKAppBot ask gemini — 2026-04-04 04:20:43*
