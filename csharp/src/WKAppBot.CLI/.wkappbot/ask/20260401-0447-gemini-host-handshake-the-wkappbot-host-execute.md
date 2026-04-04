# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=14524  time=04:45:52  uptime=3742s  mem=2521MB  system_processes=721"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260401_044542.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 103-136-162-102 103-126-213-271-721-137-162-157-276-627-623-610-106-103-321-132-103-136-167-136-675-675-106-761-731-326-132-162-106-103-135-136-132-103-173-106-132-160-136-103-1_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 370-732-673-675-736-106-103-163-574-570-760-610-163-162-567-756-236-162-106-170-107-106-106-167-163-620-316-106-357-721-765-765-762-672-675-670-260-203-302-456-675-657-263-230-6_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 123-612-617-563-312-352-627-671-623-632-631-673-621-167-167-162-613-617-362-637-637-631-362-362-321-162-136-163-361-361-136-316-136-136-316-315-316-153-153-135-123-527-572-571-6_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 136-162-361-163-321-260-263-321-231-321-302-321-302-316-163-632-136-136-136-162-163-130-127-132-162-260-620-673-126-320-302-302-301-130-301-310-301-317-312-135-365-136-120-136-1_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 574-135-756-756-765-760-765-762-762-162-715-713-761-763-761-625-762-716 675-765 762-762-156-517 106-132-761-176-671-672-267-623-627-672-672-675-576-607-107-623-132-216-173-231-1_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 314-132-120-312-132-216-132-230-231-213-132-126-102-126-126-132-123-312-126-216-132-123-120-132-312_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 567-567-536-513-135-356-536-651-563-653-567-563-567-653-567-657-561-165-153-165-165-527-516-521-567-572-127-135-125-571-563-652-652-576-357-356-567-576-652-351-162-671-617-567-5_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 562-165-136-102-601-103-106-670-162-132-103-103-615-106-106-160-613-160-621-156-106-106-163-621-326-236-627-156-167-136-163-136-167-617-162-103-167-617-671-621-136-176-103-671-1_V.mp3)
  Segment 9: 12608ms ~ 15056ms (file: 321-132-102-120-201-150-701-102-301-715-175-175-126-257-257-357-315-312-312-213-513-312-312-321-130-315-317-317-317-321-213-216 235-312-317-137-123-137 276-627-675-657-675-567-1_W.mp3)
  Segment 10: 16056ms ~ 16632ms (file: 617-130-263-103-162-670-162-130-103-103-136-136-132-163-167-136-132-172-756-135-123-167-126-176-162-167-163-167-160-613-136-602-251-160-162-167-136-103-165-162-136-137-310-136-6_V.mp3)
  Segment 11: 17632ms ~ 18208ms (file: 726-257-627-627-627-623-267-657-267-623-652-256-675-675-672-657-267-236-652-652-672-627-632-752-572-526-652-675-762-675-672-265-263-263-627-265-607-620-267-217-267-326-326-132-1_W.mp3)
  Segment 12: 19208ms ~ 23060ms (file: 527-567-621-631-615-631-126-136-162-210-127-103-162-163-175-165-167-160-765-756-753-762 263-236-261-267-265-361-362-321-236-231-230-235-623-132-231 765-765-765_W.mp3)
  Segment 13: 24060ms ~ 24636ms (file: 156-165-615-516-165-167-671-167-167-671-675-765-675-675-576-657-675-672-623-637-617-276-362-261-265-675-765-576-574-576-576-567-576-567-576-576-567-675-765-756-576-625-265-265-2_W.mp3)
  Segment 14: 25636ms ~ 26212ms (file: 162-617-167-107-631-106-106-621-153-607-126-612-106-612-106-615-136-132-103-162-231-216-236-613-162-631-106-167-617-162-620-130-326-652-302-103-261-265-621-162 672 263-610-265-2_V.mp3)
  Segment 15: 27212ms ~ 27788ms (file: 620-156-163-162-103-132-315-310-102-236-716-316-613-126-651-103-136-136-106-201-671-261-256-231-236-321-623-152_V.mp3)
  Segment 16: 28788ms ~ 29364ms (file: 263-326-316-356-173-326-326-123-361-572-362-571-537-753-325-312-652-576-615-265-125-153-263-236-216-132-326-213-312-236-321-312-132-362-231-321-172-732-231-326-321-251-251-236-2_W.mp3)
  Segment 17: 30364ms ~ 30940ms (file: 613-671-765-675-263-657-657-167-564-567-623-627-675-657-672-657-765-316-621-621-361-671-561-657-675-261-372-267-306-167-657-627-765-672-672-163-132-361-136-103-136-130-132-130-1_W.mp3)
  Segment 18: 31940ms ~ 32516ms (file: 263-375-265-652-621 652-615-263-236-123-610-631-612-160-630-361-130-715-371-651-137-176-623-360-260-206-620-620-620-260-263-132-236-263-102-612-623-160-167-106-102-103-260-216-1_W.mp3)
  Segment 19: 33516ms ~ 34092ms (file: 672-670-176-517-152-107-107-607-265-172-176-123-612 612-213 752-153-321-312-537-127-162 132-213-756-261-736 617-617-132-173-136 615 316_V.mp3)
  Segment 20: 35092ms ~ 35668ms (file: 761-176-376-376-367-301-756-756-765-756-765-672-673-376-765-675-617-163-136-162-163-612-167-621-672-657-675-673-260-236-657-653-103-103-137-162-164-135-162-132-103-536-325-312-3_W.mp3)
  Segment 21: 36668ms ~ 37244ms (file: 571-571-571-213-261-265-267-265-267-625-625-652-657-265-263-567-576-527-517-517-152-235-265-267-267-627-267-263-623-265-265-267-625-265-260-627-267-267-627-627-652-526-576-567-6_W.mp3)
  Segment 22: 38244ms ~ 38820ms (file: 621-572-752-725-712-273-236-231-326-321-321-362-326-326-231-261-261-623-562-521-126-321-362-325-263-652-352-523-236-321-231-231-236-326-320-325-352-312-352-326-265-621-562-752-2_W.mp3)
  Segment 23: 39820ms ~ 40396ms (file: 263-267-256-251-235-251-256-257-526-324-612-312-263-236-267-275-213-123-321-261-236-652-651-315-516-537-251-532-537-573-312-312-123-321-213-321-312-316-236-321-321-237-261-231-3_W.mp3)
  Segment 24: 41396ms ~ 41972ms (file: 163-610-136-132-102-167-510-652-132-126-136-163-670-123-307-167-162-102-123-132-321-132-132-130-132-231-670-672-316-261-632-321-321-167-163-160-617-163-167-162-201-106-102-102-6_V.mp3)
  Segment 25: 42972ms ~ 43548ms (file: 316-123-103-132-126-162-613-106-160-136-103-165-102-132-106-103-163-106-163-126-265-361-163-103-103-136-513-106-103-132-165-615-103-157-367-103-136-167-160-106-361-316-163-136-1_V.mp3)
  Segment 26: 44548ms ~ 45124ms (file: 132-612-132-132-263-206-621-263-127 103-631-167-167-160-165-276-672-261-623-627-267-325-162-126-106-150-132-136-317-106-301-136-271-132-163-103-126-276-102-671-163-136 325-271-1_V.mp3)
  Segment 27: 46124ms ~ 46700ms (file: 327-372-375-372-375-372-326-623-365-567-365-635-356-635-365-635-365-356-356-726-672-672-627-627-627-236-326-326-263-236-362-326-326-263-236-362-632-362-326-236-236-236-627-627-2_W.mp3)
  Segment 28: 47700ms ~ 48276ms (file: 617-316-102-165-671-265-102-617-267-106-213-627-263-126-621-607-261-167-167-132-615-165-106-132-617-176-167-157-261-176-157 136 765 627_W.mp3)
  Segment 29: 49276ms ~ 49852ms (file: 167-136-103-567-351-612-123-136-316-316-163-765-137-160-123-301-107-136-132-126-263-176-136-312-137-136-130-136-561-263-261-213-136-576-321-176-631-103-317-132-170-106-163-562-6_V.mp3)
  Segment 30: 50852ms ~ 51428ms (file: 163 756-627-675-671-675-670 637-567 756-132-623-603-731-763-675-672-163_W.mp3)
  Segment 31: 52428ms ~ 53004ms (file: 236-231-236-231-213-231-321-213-321-136-632-126-217-751-217-327-321-123-123-123-326-213-261-312-123-326-236-132-231-572-756-275-632-216-163-263-216-261-261-612-216-162-123-261-1_W.mp3)
  Segment 32: 54004ms ~ 54580ms (file: 657-637-732-326-326-675-753-561-176-176-103-610-567-637-675-361-136-362-601-627-623-523-175-176-312-132-316-176-103-126-617-136-163-657-657-615-167-321-132-361-613-316-216-312-3_W.mp3)
  Segment 33: 55580ms ~ 56156ms (file: 576-507-570-756-107-136-132-176-615-167-756-670-675-675-607-730-671-625-625-625-132-174-256-236-651-136-106-103-103-130-316-657-302-301-312-132-127-267-103-213-326-263-206-123-6_V.mp3)
  Segment 34: 57156ms ~ 57732ms (file: 103-106-162-617-265-126-576-236-256 516-135-516-621-265-267-625-560-657-547-572-576-574-716-761-132-761-712-176-716-761-756-573-735-237-751-130 310-163-671-130-367-103-213-132-6_W.mp3)
  Segment 35: 58732ms ~ 59308ms (file: 267-263-267-123-321-132-213-132-263-236-321-312 132 321-312-213_W.mp3)
  Segment 36: 60308ms ~ 61568ms (file: 162-160-176-106-130-130-106-163-103-103-137-163-306-367-136-160-632-361-367-107-362-765-106-675-763-103-673-160-136-130-603-510-156-132-126-216-167-567-756-236-257-627-321 567 1_W.mp3)
  Segment 37: 62568ms ~ 63144ms (file: 163-137-132-103-671-103-160-126-126-123-126-325-627-126-167-106-163-316-135-162-361-306-310-312-136-132-103-136-103-310-130-103-123-312-130-123-132-106-316-136-521-306-136-103-1_V.mp3)
  Segment 38: 64144ms ~ 64720ms (file: 130-326-610-612-635-621-157-316-126-127-612-673-671-671-675-671-163-612-617-267-632-623-635-621-637-652-652-657-251-675-726-162-267-356-123-216-127-615-615-163-102-156-562-715-3_W.mp3)
  Segment 39: 65720ms ~ 66296ms (file: 162-136-574-574-574-574-572 765-675-302-321-231-302-372-753-570-574-750-750 756-725_W.mp3)
  Segment 40: 67296ms ~ 67872ms (file: 413-175-173-314-670-617-167-162-163-162-167-132 637-213-125-145-124-163-175-132-176-312-127-413-143-162-127-623 627 264-267-627-621-261-260-621-236-603-670-672-627-612-627-672-6_V.mp3)
  Segment 41: 68872ms ~ 69448ms (file: 762-763-763-765-671-751-173-167-756-675-671-673-761-672-673-671-627-621-621-756-763-763-671-672-672-675-672-761-763-763-762-762-675-671-762-763-673-607-672-756-765-763-672-763-6_W.mp3)
  Segment 42: 70448ms ~ 71024ms (file: 167-106-106-106-612-162-621-756-756-756-206-260-620-302-326-203-302-302-367-103-106-160-316-316-162-126-672-675-627-672-167-163-170-756-751-730-760-672 672-672-672-620-603-306-1_W.mp3)
  Segment 43: 72024ms ~ 72600ms (file: 573-657-675-675-673-263-162-261-561-657-675-627-263-261-236-236-237-623-263-267-326-231-321-213-231-672-627-673-672-627-267-267-672-675-675-675-671-671-617-162-612-213-372-371-1_W.mp3)
  Segment 44: 73600ms ~ 74176ms (file: 756-675-652-627-627-627-635-567-576-567-657-675-675-756-621-612-321 765-762-762-765-762-761-267-327-621-302-263-362-265-361-126-623-632-631-326-321-326-327-163-120-312-162-326-1_W.mp3)
  Segment 45: 75176ms ~ 75752ms (file: 761-613-675-675-657-716-756-576-152-576-571-567-652-567-576-621-623-621-613-610-612-163-613-631-613-367-621-675-637-672-623-362-652-621-132-310-301-102-612-320-263-231-132-316-2_W.mp3)
  Segment 46: 76752ms ~ 77328ms (file: 123-213-103-312-136-132-103-301-103-163-136-130-310-103-130-310-103-130 607-167-162-617-106-167 162 674-627-627-167-163-671-267-216-103-317-637-136-306-362-136-126-136-132-302-3_V.mp3)
  Segment 47: 78328ms ~ 78904ms (file: 302-302-320-362-372-713-237 102 302-320-263-230-203-230-260-263-570-574-163-163-163-126-102-130-310-130-103-103-231-360-103-301-130-310-623-326-123-312-706-136-326-321-326-602-2_V.mp3)
  Segment 48: 79904ms ~ 80480ms (file: 156-103-102-510-513-123-152-132-123-126-127-213-231-132-103-130-326-236-265-570-371-317-312-321-123-312-361-637-136-361-162-123-132-263-375-760-136-163-136-310-135 327-103-607-6_V.mp3)
  Segment 49: 81480ms ~ 82056ms (file: 513-261-671-765-765-706-756-107-150-103-105-103-160-106-126-130-132-103-103-106-103-163-162-136-106-103-157-165-103-137-136-567-126-126-562-103-613-607-312-132-106-761-125-102-1_V.mp3)
  Segment 50: 83056ms ~ 83632ms (file: 574-571-573-754-750-571-162-126-106-163-613-362 670-607-312-167-103-167-670-675-765-706-706-761-760-760-103-106-106-103-610-106-102-106-160-765-750-675-617-106-627-260-261-623-2_V.mp3)
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
> **Time**: 2026-04-01 04:47:21
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "오늘의 학습을 시작해 볼까요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "오늘의", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "학습을", "start_ms": 160, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "시작해", "start_ms": 290, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "볼까요", "start_ms": 430, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "준비되셨으면 말씀해 주세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "준비되셨으면", "start_ms": 50, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "말씀해", "start_ms": 280, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 410, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "네 준비됐습니다 시작하죠",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 30, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "준비됐습니다", "start_ms": 100, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "시작하죠", "start_ms": 340, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "먼저 자기소개부터 해볼까요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "먼저", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "자기소개부터", "start_ms": 155, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "해볼까요", "start_ms": 375, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "이름과 직업을 말씀해 보세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "이름과", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "직업을", "start_ms": 165, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "말씀해", "start_ms": 295, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "보세요", "start_ms": 425, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "제 이름은 김철수입니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "제", "start_ms": 60, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "이름은", "start_ms": 130, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "김철수입니다", "start_ms": 270, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "저는 현재 대학생이에요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "현재", "start_ms": 150, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "대학생이에요", "start_ms": 270, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "만나서 정말 반갑습니다 철수 씨",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "만나서", "start_ms": 30, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 150, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 240, "dur_ms": 190, "phase_lr_deg": 0.0},
{"word": "철수", "start_ms": 440, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "씨", "start_ms": 520, "dur_ms": 20, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "전공은 무엇을 공부하고 있나요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "전공은", "start_ms": 120, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "무엇을", "start_ms": 270, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "공부하고", "start_ms": 400, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "있나요", "start_ms": 570, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "저는 컴퓨터 공학을 전공해요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "컴퓨터", "start_ms": 150, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "공학을", "start_ms": 270, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "전공해요", "start_ms": 400, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "아주 유망한 분야를 공부하시네요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "아주", "start_ms": 35, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "유망한", "start_ms": 125, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "분야를", "start_ms": 265, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "공부하시네요", "start_ms": 385, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 13,
"transcript": "코딩하는 것이 재미있으신가요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "코딩하는", "start_ms": 45, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "것이", "start_ms": 235, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "재미있으신가요", "start_ms": 335, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "가끔 어렵지만 문제를 해결할 때 즐거워요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "가끔", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "어렵지만", "start_ms": 140, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "문제를", "start_ms": 300, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "해결할", "start_ms": 420, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 540, "dur_ms": 30, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "그 마음이 가장 중요하죠",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "그", "start_ms": 40, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "마음이", "start_ms": 100, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 220, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "중요하죠", "start_ms": 320, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "그럼 이제 실용적인 대화를 연습해 봅시다",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 35, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 145, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "실용적인", "start_ms": 265, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "대화를", "start_ms": 425, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "연습해", "start_ms": 535, "dur_ms": 40, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "식당에서 주문하는 상황이에요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "식당에서", "start_ms": 50, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "주문하는", "start_ms": 220, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "상황이에요", "start_ms": 370, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "먼저 메뉴판 좀 주시겠어요라고 해보세요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "먼저", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "메뉴판", "start_ms": 140, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 260, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "주시겠어요라고", "start_ms": 320, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "해보세요", "start_ms": 530, "dur_ms": 40, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "저기요 메뉴판 좀 주시겠어요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "저기요", "start_ms": 40, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "메뉴판", "start_ms": 180, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 300, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "주시겠어요", "start_ms": 360, "dur_ms": 170, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "네 여기 있습니다 무엇을 드릴까요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 35, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "여기", "start_ms": 105, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 195, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "무엇을", "start_ms": 325, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "드릴까요", "start_ms": 445, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "불고기 2인분하고 공깃밥 두 개 주세요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "불고기", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "2인분하고", "start_ms": 165, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "공깃밥", "start_ms": 315, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "두", "start_ms": 435, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "개", "start_ms": 495, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 555, "dur_ms": 20, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "완벽합니다 주문을 아주 잘하시네요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "완벽합니다", "start_ms": 30, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "주문을", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 310, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "잘하시네요", "start_ms": 410, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "숫자 표현도 정확하게 사용하셨어요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "숫자", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "표현도", "start_ms": 150, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "정확하게", "start_ms": 270, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "사용하셨어요", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "한국어에서 숫자가 좀 헷갈려요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "한국어에서", "start_ms": 55, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "숫자가", "start_ms": 245, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 365, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "헷갈려요", "start_ms": 435, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "하나 둘과 일 이 삼의 차이를 아시나요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "하나", "start_ms": 40, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "둘과", "start_ms": 140, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "일", "start_ms": 250, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 300, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "삼의", "start_ms": 350, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "차이를", "start_ms": 440, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "아시나요", "start_ms": 550, "dur_ms": 20, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "개수를 셀 때는 고유어 숫자를 써야 하죠",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "개수를", "start_ms": 30, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "셀", "start_ms": 150, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "때는", "start_ms": 210, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "고유어", "start_ms": 300, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "숫자를", "start_ms": 410, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "써야", "start_ms": 530, "dur_ms": 40, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "네 맞습니다 아주 정확한 답변이에요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 45, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "맞습니다", "start_ms": 115, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 265, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "정확한", "start_ms": 355, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "답변이에요", "start_ms": 485, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "그럼 이제 물건의 가격을 묻는 연습을 하죠",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 35, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 145, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "물건의", "start_ms": 265, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "가격을", "start_ms": 375, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "묻는", "start_ms": 495, "dur_ms": 70, "phase_lr_deg": 0.0}
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
"transcript": "이 옷은 얼마인가요라고 물어보세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "옷은", "start_ms": 110, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "얼마인가요라고", "start_ms": 210, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "물어보세요", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "사장님 이 티셔츠 얼마예요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "사장님", "start_ms": 30, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 160, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "티셔츠", "start_ms": 230, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "얼마예요", "start_ms": 390, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "이건 2만 5천 원입니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "이건", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "2만", "start_ms": 160, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "5천", "start_ms": 270, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "원입니다", "start_ms": 380, "dur_ms": 170, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "조금 비싼데 깎아주실 수 있나요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "조금", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "비싼데", "start_ms": 160, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "깎아주실", "start_ms": 300, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 450, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "있나요", "start_ms": 510, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "와 흥정까지 하시네요 대단해요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "와", "start_ms": 30, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "흥정까지", "start_ms": 100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "하시네요", "start_ms": 260, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "대단해요", "start_ms": 410, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "그럼 2만 원에 드릴게요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 45, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "2만", "start_ms": 145, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "원에", "start_ms": 255, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "드릴게요", "start_ms": 355, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "정말 감사합니다 사장님 최고예요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 160, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "사장님", "start_ms": 350, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "최고예요", "start_ms": 480, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "이렇게 상황극을 하니 훨씬 기억에 잘 남죠",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "이렇게", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "상황극을", "start_ms": 160, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "하니", "start_ms": 300, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "훨씬", "start_ms": 400, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "기억에", "start_ms": 490, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "네 훨씬 재미있고 머리에 쏙쏙 들어와요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 35, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "훨씬", "start_ms": 105, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "재미있고", "start_ms": 215, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "머리에", "start_ms": 365, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "쏙쏙", "start_ms": 485, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "(music)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 40,
"transcript": "자 이제 마지막으로 오늘 소감을 말해 볼까요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "자", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 110, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "마지막으로", "start_ms": 220, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 370, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "소감을", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "오늘 수업은 정말 최고였어요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "수업은", "start_ms": 140, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 270, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "최고였어요", "start_ms": 380, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "저도 철수 씨와 함께해서 즐거웠습니다",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "저도", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "철수", "start_ms": 160, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "씨와", "start_ms": 270, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "함께해서", "start_ms": 380, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "즐거웠습니다", "start_ms": 520, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "다음 수업에서도 웃는 얼굴로 만나요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "다음", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "수업에서도", "start_ms": 150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "웃는", "start_ms": 310, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "얼굴로", "start_ms": 420, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 540, "dur_ms": 30, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "네 선생님 안녕히 가세요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 35, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "선생님", "start_ms": 105, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 255, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 415, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "잘 가요 철수 씨",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "잘", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "가요", "start_ms": 130, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "철수", "start_ms": 240, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "씨", "start_ms": 350, "dur_ms": 30, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 47,
"transcript": "오늘 배운 내용을 꼭 복습하세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "배운", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "내용을", "start_ms": 250, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "꼭", "start_ms": 380, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "복습하세요", "start_ms": 450, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "복습만이 실력을 키우는 길입니다",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "복습만이", "start_ms": 30, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "실력을", "start_ms": 200, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "키우는", "start_ms": 340, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "길입니다", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "건강 관리 잘하시고 다음에 봐요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "건강", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "관리", "start_ms": 155, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "잘하시고", "start_ms": 255, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "다음에", "start_ms": 415, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "봐요", "start_ms": 525, "dur_ms": 40, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "그럼 진짜 마칠게요 안녕",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 150, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "마칠게요", "start_ms": 270, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "안녕", "start_ms": 410, "dur_ms": 160, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-01 04:47:21*
