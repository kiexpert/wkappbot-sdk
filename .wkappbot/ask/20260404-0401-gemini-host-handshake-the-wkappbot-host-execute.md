# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=10512  time=04:00:35  uptime=9368s  mem=596MB  system_processes=366"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260404_040026.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 163-167-627-621-576-576-156-267-620-260-673-312-206-602-136-216-612-267-162-163-167-136-615-635-362-762-632-623-326-362-316-167-316-517-136-136-106-136-126-103-162-176-570-176-1_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 106-136-163-631-261-713-672-163-623-163 132-106-103-163-610-176-564-670-163-712 321-106-132-632-361-617-163-163-163-136-567-652-576 103-106-136-163-160-103-541-376-176-157-176-1_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 370-713-763-620-631-103-103-310-123-231-623-302-370-713-105-130-613-326-206-136-163-167-163-163-106-167-103-326-126-306-312-312-713-162-632-627-620 310-326-362-765-672-135-126-1_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 317-163-136-173-512-213-615-102-160-163-126-620-136-132-106-103-576-156-106-613-136-657-625-163-657-625-126-136-132-123-256-136-163-103-176-136-103-765-136-673-167-160-152-176-1_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 126-375-267-263-260-627-627-160-162-601-617-601-601-654-165-236-320-326-652-610-610-652-547-567-316-160-621-620-620-602-625-106-162-170-675-675-765-675-326-326-326-623-316-302-3_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 761-765-172-137-641-312 672-765-761-137-135-574-547-527-132-504-574-572 752-675-576-576-560-562-507-106-167-106-106-106-316-106-756-576-756-765-576-526-537-517-613-213-261-247-6_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 621-261-621-625-625-265-265-621-265-621-625-652-526-562-652-652-562-652-652-652-625 621-625-625-652-256-625-625-657-625-653 625-265-625-625-651-625-625-652-260-621-621-625-623-6_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 645-607-675 613-623-132-160-136-670-627-572-752-312-312-762-206-207-267-216-126-123-201-123-670-206-206-230-302-203-671-675-760-763 765-672 672-623-756-765-326-236-203-206-203 1_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 103-103-762-162-301-136-162-130-103-167-103-103-136-103-107-165-106-106-156-652-126-106-160-317-163-136-126-103-146-361-126-132-136-125-163-610-761-167-107-103-236-137 761-673-7_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 613-613-612-162 761-167-756 675-657-675-613-657-657 675-675-216-123-316-261-761-716-176-362-632-132-721-765-176-756-756-765-627-217-261-213-216-210-120-123-162-621-321-627-236_W.mp3)
  Segment 11: 15760ms ~ 20440ms (file: 625-623-326-756-103-571-574-627-132-627-756-756-761-765-217-762-726-176-127-276-273-712-726-721-726-726-761-761-276-726-721-271-612-261-123-312-312-137-127-672-261-167-267-276-3_W.mp3)
  Segment 12: 21440ms ~ 22016ms (file: 321-316-713 753-571-736-312-362-236-632-362-312-312 162 361-136-326-125-103-263-562-601-610-103-136-103-136-163-137-106-671-564-612-615-136-106-163-163-106-160-156-136-132-162-1_V.mp3)
  Segment 13: 23016ms ~ 23592ms (file: 537-576-570-765-205-675-605-607-650-607 675_W.mp3)
  Segment 14: 24592ms ~ 25168ms (file: 162-137-106-136-103-105-617-130-136-130-123-126-126-361-136-267-263-132-173-726 103-106-103-216 103-576-163-130-162 627-167-316-163-167-567-271-601-103-103-136-103-617-103-136-1_V.mp3)
  Segment 15: 26168ms ~ 26744ms (file: 621-267-621-267-261-261-621-261 623-620-620-620-621-621-625-627-621-627-623-623-623-263-261-263-261-261-623-265-562-652-652-562-562-562-526-562-652 623-620-623-620-621-623-627-2_W.mp3)
  Segment 16: 27744ms ~ 28320ms (file: 135-613-631-163-163-103-162-610-167-165 657-612-603-607-561-623-107-564-132-612-175-162-137-162-136-613-602-175-136-163-165-312-167-163-130-103-130-316-106-617-316-103-137-160-1_V.mp3)
  Segment 17: 29320ms ~ 29896ms (file: 576-756-756-756-752-756-756-576-756-576-576-756 657-576-576-576-574-765-754-752-756-765-756-752-756-576-576-576-756-576-756-752-576-756-752-576 576-574-574-576-754-754-756-756-5_W.mp3)
  Segment 18: 30896ms ~ 31472ms (file: 103-136-306-126-136-132-132-751-753-576-752-275-123-215_W.mp3)
  Segment 19: 32472ms ~ 33048ms (file: 136-163-135-130-130-160-165-106-102-135-103-103-356-312-103-316-103-107-316-263-267-327-127-237-765-273-372-327-732-316-276-765-735-762-765-765-756-517-713-357-571-571-326-302-3_V.mp3)
  Segment 20: 34048ms ~ 34624ms (file: 574-576-576-326-576-576-675 672-657 765-765-763-756-675-725-765-761-106-716-361-765-765-675-671-671 765-765-765-765-765-760-765-765-765-765-671-765-760-567-567-165-756-576-265-2_W.mp3)
  Segment 21: 35624ms ~ 36200ms (file: 132-163-107-137-610-621-162-621-163-106-315-632-165-160-132-103-103-167-106-567-136-160-176-132-176-623-612-320-176-172-103-123-167 176_V.mp3)
  Segment 22: 37200ms ~ 37776ms (file: 102-312-321 236-261-672-273-753-263-267-267-267-260-260-621 627-627-671-627-623-326-321-613_W.mp3)
  Segment 23: 38776ms ~ 48892ms (file: 765-756-756-765 716-167-130-107-276-236-263-236-173-267-216-231-326-267-672-756-726-270-675-107-210-267-267-312-267-321-142-123-170-267-765-726-765-765-752-231-163-310-312-267-1_W.mp3)
  Segment 24: 49892ms ~ 50468ms (file: 540-326-102-136-130-106-162-106-675-672-260-260-765-762-206-265-520-162-106-106-103_V.mp3)
  Segment 25: 51468ms ~ 52044ms (file: 163-236 152-124-136-672-603-160-321-136-132-103-126-160-103-126-103-103-564-123-312-316-126-132-312-213-126-132-103-136-103-362-516-136-136-615-136_V.mp3)
  Segment 26: 53044ms ~ 53620ms (file: 150-167-136-671-163-342-310-315-537_V.mp3)
  Segment 27: 54620ms ~ 55196ms (file: 615-623-162-630-623-267-620-136-132-130-103-132-160-135-163-564-315-210-156-516_V.mp3)
  Segment 28: 56196ms ~ 56772ms (file: 625-625-625-652-625-652-652-625-625-625-625-625-625-625-625-652 620-625-605-602-605-650-625-652-625-652-605-620-620-605-650-652-625-620-625-625-650-657-675-607-675-675 620-605-6_W.mp3)
  Segment 29: 57772ms ~ 58348ms (file: 213-260-627-602-601-602-627-760-370-103-716-702-320-231-130-756-765-675-675-206-260-206-206-107-107-765-765-371-756-765-765-715-765-675-762-312-316-263-631-127-726-761-716-217-2_W.mp3)
  Segment 30: 59348ms ~ 59924ms (file: 623-675-675-761-756-675-675-675-165-165-321-231-132-170-167-106-261-126-236_W.mp3)
  Segment 31: 60924ms ~ 61500ms (file: 627-267-672-267-267-326-236-321-106-103-103-136-310-103-302-203-102-213-306-301-163-301-310-206-260-206-206-260-236-104-102-137-163-326-132-123-132-263-260-326-321-621-362-267-1_V.mp3)
  Segment 32: 62500ms ~ 63184ms (file: 107-103-516-107-261-216-120-126-126-176_W.mp3)
  Segment 33: 64184ms ~ 64760ms (file: 123-260-756-275-602-231-236-325-231-316-302-103-627-263-261-123-302-213-321-326-321-302-302-317-237-236-237-320-230-326-326-231-312-120-312-236-123-312-631-125-261-236-130-326-3_V.mp3)
  Segment 34: 65760ms ~ 76524ms (file: 130-231-216-120-102-237-257-120-213-103-124-320-230-256-514-103-157-215-210-235-231-321-231-326-123-413-123-531-257-207-102-574-105-165-237-752-105-276-132-102-215-105-302-321-2_W.mp3)
  Segment 35: 77524ms ~ 78100ms (file: 620-260-260-170-671-671-162-137-701-751-312-310-136-103-326-306-627-620-620-620-160-103-613-617-167-136-102-620-263-263-362-601-623-620-263-301-162-106-613-106-132-671-176-107-6_W.mp3)
  Segment 36: 79100ms ~ 79676ms (file: 672-672-627-627-627-627-261-621-621-627-167-162-206-210-126 316-136-316-123-132-136-156-157-571-576-715-756-673-376-231-231-231-231-236-231-321-321-231-231-231-312-216-312-130-5_W.mp3)
  Segment 37: 80676ms ~ 81252ms (file: 167-163-136-310-103-301-306-236-103-671-316-326-103-103 130-134-627-163-164-136-316-627-617-267-725-671 713-716-761-761-716-176-716-713-716-731-176 132-176-173-176-716-371-371-1_V.mp3)
  Segment 38: 82252ms ~ 82828ms (file: 756-716 103-163-136-613-156-162 132-106-136-671-132-130-103-623-176-157-103-132-103-316-163-132-160-672-127-123-136-132-106-103-106-130-163-651-103-136-153-210-157-162_V.mp3)
  Segment 39: 83828ms ~ 84404ms (file: 612-163-621-273-231-326-236-326-312-321-216-612-267-231-635-632 567-576-623-627 325 627-627-260-263-672-675-623-126-621-216 513-413-103-103-103-136-130-641-627-237-126-623-162-6_W.mp3)
  Segment 40: 85404ms ~ 85980ms (file: 163-136-172-123-261 126-172-216-612_V.mp3)
  Segment 41: 86980ms ~ 87556ms (file: 361-365-561-326-126-312-756-756-765-670-630-310-316-735-756-761-750-675-176-103-103-103-103-130-156-567-156-165-567-567-756-567-576-625-652-675-657-567-567-657-675-231-263-621-6_W.mp3)
  Segment 42: 88556ms ~ 89132ms (file: 652-672-672-627-167-316-167-163-173-763-312-130 231-132-103-102-134-103-132-126-123-136 260-362-203-315-132-312-316-624-465-261-627-602-627-130-103-130-316-126-321-631-326-362-3_V.mp3)
  Segment 43: 90132ms ~ 90708ms (file: 360-301-360-602-267-126-162-126-756-761-602-106-261-601-316-362-362-602-302-236-632-760-167-607-136-301-306-361-367-362_V.mp3)
  Segment 44: 91708ms ~ 92284ms (file: 571-275-321-302-210-132-310-312-201-352-326-316-132-301-312-321-132-710-176-316-360-752-607-263-625-652-267-267-263-326-362-263-361_W.mp3)
  Segment 45: 93284ms ~ 93860ms (file: 126-675-657-102-201-213-123-123-617-631-671-126-520-607-657-135-726-261-102-651-207-263-625-261-623-265-230-163-127-152-175-130-627-132-312-136-216-631-132-106-716-761-627-172-1_W.mp3)
  Segment 46: 94860ms ~ 95436ms (file: 765-762-267-620-260-260-260-765-765-765-617-302-321-302-756-761-756-756 310-756-751-750 765-675-670-751-756-756-756-763-765-756-753-765-765-762-761-762-103-103-163-637-310-301-1_W.mp3)
  Segment 47: 96436ms ~ 108208ms (file: 375-267-102-310-321-130-312-620-301-130-672-267-126-106-167-103-765-260-260-260-320-130-231-132-103-761-267-236-321-326-312-321-310-173-713-765-276-176-130-312-317-725-723-310-7_W.mp3)
  Segment 48: 109208ms ~ 109784ms (file: 567-576-632-652-625 163-671-316-576-605 152 532 625-620-765-167 576-571_W.mp3)
  Segment 49: 110784ms ~ 111360ms (file: 162-136-716-130-607-162-123-612-130-103-132-316-136-610-103-173-675-316-136-132-130-312-132-167-167-672-627-627-153-135-213-312-321-316-672-627-601-617-613-312-672-765-613-635-6_W.mp3)
  Segment 50: 112360ms ~ 120424ms (file: 210-130-102-123-134-210-203-213-574-572-620-720-102-130-301-712-276-370-273-310-735-756-675-172-103-763-675-132-310-375-230-231-372-103-312-267-230-132-756-576-276-576-320-230-1_W.mp3)
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
> **Time**: 2026-04-04 04:01:48
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "그리고 첫 손님이 들어오셨습니다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "그리고", "start_ms": 20, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "첫", "start_ms": 90, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "손님이", "start_ms": 130, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "들어오셨습니다.", "start_ms": 220, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 3,
"transcript": "정말 큰 충격을 받았던 그때 머릿속에 맴돌았던 내용을 대충 정리하면 조금만 더 일찍 수강을 들었다면 지금까지의 손실",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 30, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 80, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "충격을", "start_ms": 110, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "받았던", "start_ms": 170, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "그때", "start_ms": 220, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "머릿속에", "start_ms": 260, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "맴돌았던", "start_ms": 320, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "내용을", "start_ms": 390, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "대충", "start_ms": 440, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "정리하면", "start_ms": 480, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "조금만", "start_ms": 540, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 590, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "일찍", "start_ms": 620, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "수강을", "start_ms": 660, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "들었다면", "start_ms": 710, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "지금까지의", "start_ms": 780, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "손실", "start_ms": 860, "dur_ms": 40, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 5,
"transcript": "박사의 지위가 향후 5년 내에 굉장히 위험해질 것으로 판단하고 있는데요.",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "박사의", "start_ms": 40, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "지위가", "start_ms": 90, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "향후", "start_ms": 140, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "5년", "start_ms": 180, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "내에", "start_ms": 220, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "굉장히", "start_ms": 260, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "위험해질", "start_ms": 320, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "것으로", "start_ms": 390, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "판단하고", "start_ms": 440, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "있는데요.", "start_ms": 500, "dur_ms": 40, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "수만 줄의 코드를 짜고 하이퍼파라미터를 튜닝하면서 벤치마크 1, 2%를 올리기 위해서 밤을 새우는 노동은 더 이상 인간의 몫이 아니니까요.",
"speaker": "speaker_3",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "수만", "start_ms": 20, "dur_ms": 35, "phase_lr_deg": 0.0},
{"word": "줄의", "start_ms": 65, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "코드를", "start_ms": 105, "dur_ms": 45, "phase_lr_deg": 0.0},
{"word": "짜고", "start_ms": 160, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "하이퍼파라미터를", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "튜닝하면서", "start_ms": 310, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "벤치마크", "start_ms": 390, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "1,", "start_ms": 460, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "2%를", "start_ms": 490, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "올리기", "start_ms": 540, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "위해서", "start_ms": 600, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "밤을", "start_ms": 650, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "새우는", "start_ms": 690, "dur_ms": 45, "phase_lr_deg": 0.0},
{"word": "노동은", "start_ms": 745, "dur_ms": 45, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 800, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "이상", "start_ms": 830, "dur_ms": 35, "phase_lr_deg": 0.0},
{"word": "인간의", "start_ms": 875, "dur_ms": 45, "phase_lr_deg": 0.0},
{"word": "몫이", "start_ms": 930, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "아니니까요.", "start_ms": 970, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "이제 전기를 먹고사는 값싼 토큰과 에이전트",
"speaker": "speaker_3",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 30, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "전기를", "start_ms": 70, "dur_ms": 45, "phase_lr_deg": 0.0},
{"word": "먹고사는", "start_ms": 125, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "값싼", "start_ms": 195, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "토큰과", "start_ms": 245, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "에이전트", "start_ms": 305, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 9,
"transcript": "있는 거 아닌가요? 아닙니다. 이건 떨어지는 게 아니라 에너지를 모으는 구간입니다.",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "있는", "start_ms": 20, "dur_ms": 35, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 65, "dur_ms": 15, "phase_lr_deg": 0.0},
{"word": "아닌가요?", "start_ms": 90, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "아닙니다.", "start_ms": 150, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "이건", "start_ms": 220, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "떨어지는", "start_ms": 260, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 330, "dur_ms": 15, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 355, "dur_ms": 45, "phase_lr_deg": 0.0},
{"word": "에너지를", "start_ms": 410, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "모으는", "start_ms": 480, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "구간입니다.", "start_ms": 540, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "사상 최고가를 돌파하는 강한 상승 파동이 나왔고 그 상승",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "사상", "start_ms": 30, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "최고가를", "start_ms": 80, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "돌파하는", "start_ms": 160, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "강한", "start_ms": 230, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "상승", "start_ms": 280, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "파동이", "start_ms": 330, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "나왔고", "start_ms": 390, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 450, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "상승", "start_ms": 480, "dur_ms": 40, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 12,
"transcript": "발휘 업체는 어디에나 있기 때문입니다. 진정으로 한국 무기가 동남아시아와 심지어 중앙아시아에서까지 폭발적인 인기를 얻은 이유는 실질적인 극한의 가성비 덕분이었습니다. 예전까지 중동의 부유한",
"speaker": "speaker_4",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "발휘", "start_ms": 30, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "업체는", "start_ms": 80, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "어디에나", "start_ms": 140, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "있기", "start_ms": 210, "dur_ms": 35, "phase_lr_deg": 0.0},
{"word": "때문입니다.", "start_ms": 255, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "진정으로", "start_ms": 340, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 410, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "무기가", "start_ms": 460, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "동남아시아와", "start_ms": 520, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "심지어", "start_ms": 630, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "중앙아시아에서까지", "start_ms": 690, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "폭발적인", "start_ms": 820, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "인기를", "start_ms": 900, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "얻은", "start_ms": 960, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "이유는", "start_ms": 1010, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "실질적인", "start_ms": 1070, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "극한의", "start_ms": 1150, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "가성비", "start_ms": 1210, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "덕분이었습니다.", "start_ms": 1270, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "예전까지", "start_ms": 1370, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "중동의", "start_ms": 1450, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "부유한", "start_ms": 1510, "dur_ms": 60, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-04 04:01:48*
