# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=29532  time=03:38:37  uptime=7612s  mem=2281MB  system_processes=366"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260404_033828.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 275-672-236-276-275-263-670-562-572-765-756-750-657-673-573-237-547-725-321-532-576-327-267-567-576-531_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 301-310-130-312-631 162-132-316-306-632-630-620-261-163-574-573-754-102-231-637-160-123-261-203-126-103-310_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 167-631-612-623-612-103-301-306-631-230 130-270-260-620-260-627-260-206-130-312-120-153-106-103-136-316-103-130-210-501-541-537-357-573-567-675-537-356-537-567-576-567-537-536-5_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 620-620 627-620-621-620-620 621-621-625-632_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 756-567-576-657-567-657-576-765-657-675-567-657-576-675-176-562-756-576-657-576-675-657-675-672-613-672-675-765-756-657-675-567-657-756-316-163-617-765-563-765-657-625-627-672-1_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 130-103-163-621-203-206-276-106-132-130-312-136-136-103-103-103-132-631-167-302-106-670-201-102-102-102-106-126-106-126-312-120-102-102-103-126-102-132-517-170-165-176-657-360-1_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 526-527-762-657-527-572-721-237-217-627-167-273-276-723-672-325-157-317-317-173-317-137-327-372-372-372-327-327-312-231-527-325-236-621-216-213-231-215-231-267-327-213-215-512-5_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 137-617-315-137-207-120-260-260-260-654-516-302-203-102-326-203-236-621-267-362-672-261-163-301-106-103-301-306-130-130-132-301-130-136-603-367-163-673-613-136-162-176-617-106-2_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 163-612-261-261-623-362-765-756-761-632-372-173-573-576-750-135-301-130-162-132-316-312-362-236-321-632-623-231-672-627-672-136-163-132-312-675-672-107-167-103-132-316-312-513-1_V.mp3)
  Segment 10: 14184ms ~ 22428ms (file: 201-103-756-752-175-176-103-276-137-751-752-173-103-310-601-765-132-107-103-756-271-302-103-321-275-273-310-103-103-754-270-173-713-723-250-751-752-102-310-321-132-107-236-230-3_W.mp3)
  Segment 11: 23428ms ~ 24004ms (file: 123-132-750-765-765-706-761-713-132-163-301-310-130-137-173-312-261-632-675-765-706-617-176-672-675-675-675-670-106-106-574-756-706-206-206-263-203-602-130-103-130-106-103-106-1_W.mp3)
  Segment 12: 25004ms ~ 25580ms (file: 163-612-621-621-617-673-675-261-160-107-361-612-612-621-672-675-167-610-176-163-316-165-210-167-162-716-761-175-107-260-120-561-167-102-167 735-317 716-762-312-132-231-132-163-6_V.mp3)
  Segment 13: 26580ms ~ 27156ms (file: 167-176 312-132-163-612-106 761-627-760-765-672-726 756-715-752-756-751-715-312-756-765-736-321-263-621-163 765-765-576-576-571-765-765-756-761-761-765-765-617-761-106-617-512-2_W.mp3)
  Segment 14: 28156ms ~ 28732ms (file: 152-670-672-672-162-621-362-672-126-631 135-106-375-165-156-103-106-106-136-163-167-762-632-162-602-103-160-136-106-103-561-160-163-126-165-136-165-136-132-103-165-675-623-362-1_V.mp3)
  Segment 15: 29732ms ~ 30308ms (file: 216-107-130-137-156-756-612-165-162-124-103-136-136-103-165-165-753-165-163-130-103-672-612-216-120-612-615-136-301-132-710-176-103-136-103-160-163-670-163-670-627-623-602-267-1_V.mp3)
  Segment 16: 31308ms ~ 31884ms (file: 561-507-507-360-570-750-735-753-730-370-103-132-312-203-203-206-621-621 675-765 627-267-267-312-326-320-321-652-627-236-263-263-263-267-263-267-236-261 263_W.mp3)
  Segment 17: 32884ms ~ 33460ms (file: 756-567-657-675-157-716-567-751-103-123-132-157-562-261-621-103-103-162_V.mp3)
  Segment 18: 34460ms ~ 35036ms (file: 136-167-136-132-126-132-163-136-162-102-123-301-130_V.mp3)
  Segment 19: 36036ms ~ 36612ms (file: 310-103-312-230-103-210-123-216-231-102-130-123-120-103-130-103-130-103-123-267-321-216-602-162-130-671-623-625-576-567-576 621-162-216-652-326-321-263-120-263-206-103-176-176-2_W.mp3)
  Segment 20: 37612ms ~ 44884ms (file: 120-213-120-102-123-321-163-321-231-260-123-102-210-103-563-120-572-572-120-213-567-205-102-102-576-317-671-527-253-320-213-201-301-136-312-576-576-270-235-571-175-235-302-206-3_V.mp3)
  Segment 21: 45884ms ~ 49808ms (file: 712-176-172-216-170-170-712-176-716-612-716-716-107-176-170-716-107-716-176-103-103-716-710-120-627-132-213-173-130-120-132-137-162-216-210-201-127-173-176-176-172-173-163-167-7_W.mp3)
  Segment 22: 50808ms ~ 51384ms (file: 567-163-321-213-231-301-627-320-326-136-163-361-620-620-620-602-623-602-620-610-601-167-613-163-601-167-106-163-106-106-106-310-301-326-310-136-103-136-675-765-613-671-671-620-2_V.mp3)
  Segment 23: 52384ms ~ 52960ms (file: 126-524-574-530-236-261-206-263-206-326-320 362-136-310-602-361-302-321-213-236-612-261-123-620-623-103-306-301-162-103-107-765-760-672-576-506-572-206-263-123-123-132-310-130-3_W.mp3)
  Segment 24: 53960ms ~ 54536ms (file: 103-126-765-120-632-137-176-173-136-321-127-167-136-137-130-513-106-132-164-163-175-263-261-756 610-167-103-176-132-130-103-163-103-317-103-263-263-236-756-765 765-765-617-176-1_V.mp3)
  Segment 25: 55536ms ~ 56112ms (file: 206-263-175-357-537-160-673-163-605-106-106-106-310-136-310-361-630-106-136-103-106-136-103-167-136-316-103-102-106-132-765-631-130-301-102-236-326-236-672-623-276-632-103-312-1_V.mp3)
  Segment 26: 57112ms ~ 57688ms (file: 510-105-125-136-136-617-617-675-613-126-163 632-175-157-137-613-136-136-651-103-621-126 623 126-136-136-316-130-136-137-137-102-316-136-162-106-163-103-103-136-102-130-126-123-1_V.mp3)
  Segment 27: 58688ms ~ 70064ms (file: 302-307-302-302-362-237-321-301-206-567-253-201-261-216-102-123-572-105-132-301-321-321-123-123-312-321-102-103-134-312-132-236-231-216-231-671-312-273-352-321-103-132-321-231-3_W.mp3)
  Segment 28: 71064ms ~ 71640ms (file: 310-130-136-570-570-537-354-506-156-103-160-603-160-610-106-103-627-627-206-203-321-321-320-302-320-236-136-103-103-106-316-306-103-231-103-103-137-316-310-321-316-103-230-230-2_W.mp3)
  Segment 29: 72640ms ~ 73216ms (file: 617-160-123-130-136-103-137-176-163-126-623-652-316-652-675-162-761-160-136-103-154-165-632-652-136-173-132-106-106-312-326-162-163-130-132-126-657 106-160-675-162-763-167-106-1_V.mp3)
  Segment 30: 74216ms ~ 74792ms (file: 103-103-172-361-162-612-106-136-136-103-103-153-136-136-136-213-316-130-103-561-132-163-103-106-126-163-103-517-103-613-632-136-132-132-103-301-723-103-765-751-176-165_V.mp3)
  Segment 31: 75792ms ~ 76368ms (file: 657-517-567-617-156-176-105-607-615-657-657-765-315-157-516-571-126-216-657-165-672-615-671-160-567-651-657-576-567-657-657-576-756-756-576-567-756-576-527-567-576-761-576-361-7_W.mp3)
  Segment 32: 77368ms ~ 77944ms (file: 632-326-361-632-236-136-136-136-753-216-623-627-570-672-273-137-657-756-375-301-132-321-574-547-652-256-657-576-617 175-106-123-136-617-621-612-106-570-756-756-623-130-103-261-6_W.mp3)
  Segment 33: 78944ms ~ 79520ms (file: 321-132-306-162-126-163-602-103-132-136-130-310-135-106-103-675 657-615-106-103-102-103 176_V.mp3)
  Segment 34: 80520ms ~ 81096ms (file: 176-167-137-102-106-176-175-156-157-165-517-173-123-257-715-571-576-570-175-756-175-137-716-175-127-173-217-275-765-567-576-576-756-675-576-765-762-576-567-765-675-657-652-652-5_W.mp3)
  Segment 35: 82096ms ~ 90736ms (file: 732-107-107-750-137-751-215-132-103-123-213-726-210-327-754-136-547-130-103-103-103-107-751-301-231-756-756-270-231-723-756-130-320-547-253-102-275-752-210-207-572-623-765-256-2_W.mp3)
  Segment 36: 91736ms ~ 92312ms (file: 607-673-540-543-540 132 103-103_V.mp3)
  Segment 37: 93312ms ~ 93888ms (file: 175-601-602-672-607-672-106-103-130-136-163-173-612-103-103-651-126-251-723-107-136-510-165-102-160-736-136-317-310-167-176-137-130-132-361-126-673-567-576-675-321-130-507-564-7_W.mp3)
  Segment 38: 94888ms ~ 95464ms (file: 160-165-106-576-705-107-613-132-176-167-165-106-765-617-671-160-103-103-657-617-631-576-173-103-103-106-106-567-657-156-765-167-617-103-103-132-130-136-103-123-103-163-210-170-7_V.mp3)
  Segment 39: 96464ms ~ 97040ms (file: 621-625-627-261-621-625-627-310-312-321-216-213-263-231-236-326 257_W.mp3)
  Segment 40: 98040ms ~ 98616ms (file: 167 376-630-765-756-367-671-675-750-675_W.mp3)
  Segment 41: 99616ms ~ 100192ms (file: 754-756-517-157-103-132-123-136-120-106-162-176-103-175-167-316-102-321-126-135-617-710-765-216-276-276-762-267-326-512-261-216-261-716-126-162-216-326-136-126-162-163-607-607-3_V.mp3)
  Segment 42: 101192ms ~ 101768ms (file: 306-130-620-103-103-102-132-102-310-310-312-130-320-316-162-310-316-126-310-130-310-320-301-136-312-162-670-675-672-627-672-632-120-602-627-236-620-607-637-260-607-617-320-321-1_W.mp3)
  Segment 43: 102768ms ~ 103344ms (file: 361-361-167-106-136-637-316-617-160-136-671-612-632-163-130-361 672-670_V.mp3)
  Segment 44: 104344ms ~ 104920ms (file: 567-567-576-576-567-576-756-576-567-576-576-567-567-576-567-657-567-576-572-752-576-572-756-765-657-576-756-562-576-572-257-576-567-567-675-675-765-756-675-675-265-657-567-765-6_W.mp3)
  Segment 45: 105920ms ~ 106496ms (file: 316-136-136-316-103-316-162-136-136-103-316-312 136-301-103-312-761-130-123-135-156-612-123-620-132-103-361-316-761-260-613-632-367-136-106-615-156-136-173-136-106-176-312-263-1_V.mp3)
  Segment 46: 107496ms ~ 108072ms (file: 327-326-627-267-263-302-302-237-675-716-703-762-260-763-761-106-136-165-176-317-362-637-675-126-631-176-763-672-673-176-652-672-627-567-625-276-165-132-162-621-572-723-726-675-7_W.mp3)
  Segment 47: 109072ms ~ 109648ms (file: 576-576-516-561-574-567-573-573-567-675-576-576-567-567-576-657-657-675-675-536-563-576-165-561-657-136-167_W.mp3)
  Segment 48: 110648ms ~ 111224ms (file: 576-675-670-167-137-176-176-716-615-617-765-617-367-637-376-160-671-176-671-736-617-761-136-316-612-631-617-673-675-673-367-637-627-631-615-567-627-765-671-567-651-156-236-762-5_W.mp3)
  Segment 49: 112224ms ~ 112800ms (file: 561-162-312-261-231-321-236-756-765-760-675-762-627-267-267-576-756-675-765-762-627-261-510-756-657-607-675-607-632-672-673-672-120-163-617-637-765-657-675-672-627-670-635-625-6_V.mp3)
  Segment 50: 113800ms ~ 114376ms (file: 162-352-573-571-576-574-571-312-312-120-317-571-537-127-261-256-524-521-236-325-256-236-652-326-236-321-315-513-541-524-527-265-652-621-263-236-236-576-675-627-216-123-527-547-5_W.mp3)
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
> **Time**: 2026-04-04 03:39:38
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "기법은 없다 이러고 마시거든요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "기법은", "start_ms": 20, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "없다", "start_ms": 210, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "이러고", "start_ms": 340, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "마시거든요", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "동주야 계좌 관리가 생명이다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "동주야", "start_ms": 30, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "계좌", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "관리가", "start_ms": 310, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "생명이다", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "그러면 이제 거기에서 제가 그거에 대한",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "그러면", "start_ms": 10, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "거기에서", "start_ms": 250, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 380, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "그거에", "start_ms": 470, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "대한", "start_ms": 540, "dur_ms": 30, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "말씀을",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "말씀을", "start_ms": 50, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "해주신 거를 고민을 하는 거죠 너가",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "해주신", "start_ms": 15, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "거를", "start_ms": 170, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "고민을", "start_ms": 280, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 410, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "거죠", "start_ms": 500, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "너가", "start_ms": 550, "dur_ms": 20, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "월급을 많이 받을 확률이 높습니다",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "월급을", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 200, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "받을", "start_ms": 290, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "확률이", "start_ms": 390, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "높습니다", "start_ms": 500, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "그런데 더 충격적인 점은 이제",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "그런데", "start_ms": 25, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 160, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "충격적인", "start_ms": 230, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "점은", "start_ms": 420, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 510, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "또 논문을 중요시하는 것은 시대",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "또", "start_ms": 10, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "논문을", "start_ms": 80, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "중요시하는", "start_ms": 220, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "것은", "start_ms": 410, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "시대", "start_ms": 500, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "착오적이라는 이야기가 나오고",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "착오적이라는", "start_ms": 15, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "이야기가", "start_ms": 210, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "나오고", "start_ms": 350, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "있다는 겁니다 실제로 (BGM)",
"speaker": "speaker_3",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "mixed",
"words": [
{"word": "있다는", "start_ms": 20, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "겁니다", "start_ms": 170, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "실제로", "start_ms": 290, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 12,
"transcript": "다 나오고 신부님 나오셔서 소식을",
"speaker": "speaker_4",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "다", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "나오고", "start_ms": 120, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "신부님", "start_ms": 240, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "나오셔서", "start_ms": 350, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "소식을", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "전해주시는데요",
"speaker": "speaker_4",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "전해주시는데요", "start_ms": 20, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "(현장 음성 - 인도네시아)",
"speaker": "speaker_5",
"confidence": 0.85,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "인도네시아", "start_ms": 100, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "김치를 가져오셔서",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "김치를", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "가져오셔서", "start_ms": 250, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "제가 기다리다 안 나오셔서 먼저",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "제가", "start_ms": 15, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "기다리다", "start_ms": 120, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 270, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "나오셔서", "start_ms": 340, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "먼저", "start_ms": 480, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "왔거든요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "왔거든요", "start_ms": 10, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "원래 옛날 관점으로",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "원래", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "옛날", "start_ms": 180, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "관점으로", "start_ms": 310, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "매매를 했으면 그날 아마 풀베팅을",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "매매를", "start_ms": 10, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "했으면", "start_ms": 160, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "그날", "start_ms": 290, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "아마", "start_ms": 400, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "풀베팅을", "start_ms": 490, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "해서 다음날 장렬히 전사했을 거예요 근데 이제 형님은 이제 비중 조절을 잘하시는데",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "해서", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "다음날", "start_ms": 140, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "장렬히", "start_ms": 270, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "전사했을", "start_ms": 380, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 540, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 630, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 710, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "형님은", "start_ms": 780, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 880, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "비중", "start_ms": 950, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "조절을", "start_ms": 1040, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "잘하시는데", "start_ms": 1150, "dur_ms": 120, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 23,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 28,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 29,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 36,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 39,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 40,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 41,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 42,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 50,
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
*Generated by WKAppBot ask gemini — 2026-04-04 03:39:38*
