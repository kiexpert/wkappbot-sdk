# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=34276  time=03:50:45  uptime=10770s  mem=668MB  system_processes=367"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260404_035003.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 760-670-670-621-627-672-672-276-276 726-725-276-726-276-267-762-267-267-627_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 657-657-657-567-675-657-657-567-657-756-576-765-576-657-657-567-657-567-675-675-576-657-576-567-567-576-567-657-576-675-657-657-756-657-567-576-657-756-657-576-765-657-765-765-7_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 675-657-167-367-106-610-126-672-176-601-173-675-615-610-630-671-675-675-605-657-756-765-576-756-765-756-675-675-675-765-675-765-576-567-567-567-567-657-571-165-673-136-136-132-1_W.mp3)
  Segment 4: 4728ms ~ 12468ms (file: 312-137-316-137-103-613-613-361-163-160-671-136-167-613-671-763-672-712-267-327-130-567-567-576-574-547-547-574-321-123-152-765-167-310-167-276-327-103-236-671-217-326-732-765-7_W.mp3)
  Segment 5: 13468ms ~ 14044ms (file: 675-760-760-756-765-765-765-765 134-153 631-607-607-176-173-310-132-312-162-132-163-123-132 137-132-126-132-621-162-123-136-132-310-130-103-263-236-675-675-675-672-675 231-130-3_V.mp3)
  Segment 6: 15044ms ~ 15620ms (file: 613-126-567-576-675-615-675-615-765-617-672-675-672-637-367-675-615-613-675-567-765-675-367-576-574-576_W.mp3)
  Segment 7: 16620ms ~ 17196ms (file: 130-163-136-165-132-136-163-216-102-106-136-137-710-106-672-136-106-130-130-310-103-103-316-130-103-103-163-153-635-175-615-367-620-670 675-762-675-657-675-657-672-136-371-732_V.mp3)
  Segment 8: 18196ms ~ 18772ms (file: 136-123-136-316-312-570-573-507-157-137-103-103-107-132-675-617-601-167-162-106-106-103-126-213-136-612-162-752-751-617-613-163-163-617-671-126-163-673-672-621-613-132-756-765 6_V.mp3)
  Segment 9: 19772ms ~ 20348ms (file: 657-106-106-120-136-162-126-162-612-206-103-302-301-302-103-301-316-136-102-310-103-263-675-675-605-360-162-261-621-361-312-310-132-231-263-621-127-172-163-167-106-164-706-716-7_V.mp3)
  Segment 10: 21348ms ~ 21924ms (file: 750-571-547-750-137 615-361-610-731-326-756-751-756-756-756-756-657-765-675-173-103-136-132-567-675_W.mp3)
  Segment 11: 22924ms ~ 23500ms (file: 267-362-621-126-103-175-167-163-132-571-567-670-570-570-175-175-174-127-150-576-675-176-175-750-574-326-317-134-574-574-153 175-321-362-106-132-163-160-163-136-613-160-621-231-2_V.mp3)
  Segment 12: 24500ms ~ 33068ms (file: 136-103-231-326-236-102-120-103-367-731-231-231-527-210-203-765-136-123-216-106-756-726-123-321-327-103-236-326-132-312-236-132-726-167-765-635-271-130-320-756-216-236-752-237-3_W.mp3)
  Segment 13: 34068ms ~ 40980ms (file: 127-126-123-126-716-130-327-312-213-321-217-123-316-365-316-231-217-321-217-216-126-612-231-137-213-312-312-316-163-371-263-326-231-273-312-362-623-137-512-532-351-312-735-123-2_W.mp3)
  Segment 14: 41980ms ~ 42556ms (file: 132-124-132-143-132-153-124-132-135 675-267-267-126-167-621-316-213-231-673-607-320-230-301-312-132-107-310-102-230-321-321-537-574-503-306-527-102-123-132-306-260-603-605-502-5_V.mp3)
  Segment 15: 43556ms ~ 44132ms (file: 675-765-765-672-657-672-675-672-675-675-672 675-762-756-376-365-765-637-675-763-673-231-632-763-673-671-675-672-675-723-205-120-231-312-612-102-106-126-561-132-132-130-132-310-1_W.mp3)
  Segment 16: 45132ms ~ 53232ms (file: 567-756-657-576-736-327-207-206-175-103-672-576-267-756-276-261-725-751-715-106-167-106-107-761-672-657-620-756-705-710-301-106-267-260-260-132-130-756-105-325-752-571-573-135-1_W.mp3)
  Segment 17: 54232ms ~ 54808ms (file: 754-107-106-153-106-103-103-163-316-623-103-123-136-107-163-130-651-106-103-106-103-103-106-163-107-103-362-167-106-376-103-130-765-617-130-103-136-163-160-136-163-612-176-126-1_V.mp3)
  Segment 18: 55808ms ~ 56384ms (file: 615-617-615-617-652-615-675-657-651-167-163-176-716-261-126-157-573-107-103-270-203-230-230-230-326-236-231-132-163-301-136-167_W.mp3)
  Segment 19: 57384ms ~ 57960ms (file: 570-312-230-360-607-306-310-203-312-230-326-302-312-320-230-320-326-236-326-320-206-261-607-123-136-320-236-216-625-260-260-320-526-503-302-203-206-236-203-263-620-206-263-526-6_W.mp3)
  Segment 20: 58960ms ~ 59536ms (file: 163-216 312-130-312-301-310-157-120 675-612-236-275-627-671-132 326-130-130_V.mp3)
  Segment 21: 60536ms ~ 61112ms (file: 201-203-651-310-312-203-103-312-260-301-135-107-673-130-103-136-163-136-103-546-652-756-623-136-103-132-134-312-134-143-263-236-260-206-102-621-310-302-351-103-150-203-160-160-2_V.mp3)
  Segment 22: 62112ms ~ 62688ms (file: 517-576-576-162-617-106-162-162-165-751-756-675-572-567-362-627-756-572-574-567-567-762-756-765-756-576-756-756-627-612-632-362-637-126-132-627-231-576-673-675-715-357-123-132-2_W.mp3)
  Segment 23: 63688ms ~ 64264ms (file: 137-261-132-103-137-136-103-170-167-673-261-623_V.mp3)
  Segment 24: 65264ms ~ 65840ms (file: 671-632-627-107-102-136-132-130-312-312-130-302-321-312-103-136-721-210-213 321-132-627-627 672-562-356-536-571-536-236-621-216-217-261-325-523-265-351-365-325-312-157-231-632-6_W.mp3)
  Segment 25: 66840ms ~ 67416ms (file: 516-103-356-361-136-162-163-106-630-731-136-103-132-126-162-163-361-103-103-163-362-765-613-160-671_V.mp3)
  Segment 26: 68416ms ~ 68992ms (file: 623-321-162-362-231-236-231-236-321-231-320-673-672-672-627-627-625-631-612-163-361-675-675-673-623-162-162-625-652 263-263-263-263-263-261-726-672_W.mp3)
  Segment 27: 69992ms ~ 70568ms (file: 173-326-632 312-162-321 261-263-206-203-103-103-136-567-536-316-216-132-315-136-103-163-162-136-602-106-160-163-162-301-123-320-302-326-302-632-362-310-260-362-562-617-127-103-1_V.mp3)
  Segment 28: 71568ms ~ 72144ms (file: 673-156-576-572-367-612-671-167-612-162-136-632-365-136-312-261-627-621-623-123-312-362-163-163-165-615-123-123 564-560-612-627-260-620 126-137-263-165-123-136-160-312-123-620-2_W.mp3)
  Segment 29: 73144ms ~ 73720ms (file: 173-213-126-132-123-567-652-617-261-576-752-267-126-672-621-756-672-576-573-756-576-312-315 236-267-632-521-506-672 602-613-651-267-672-632-136-327-362-632-176-167-136-632-306-6_W.mp3)
  Segment 30: 74720ms ~ 75296ms (file: 217-705-716-672-607-620-261-126-103-175-213-312-574-574-103-203-627-620-261-623-627-267-620-623-630-230-413-672-765-176-136-130-103-137-317-301-301-302-301-213-617-162-613-623-1_V.mp3)
  Segment 31: 76296ms ~ 83100ms (file: 130-725-276-316-315-302-301-312-367-376-136-752-312-321-370-372-367-762-276-237-210-362-703-302-320-263-263-236-326-726-276-267-756-216-123-623-263-762-267-213-130-571-571-756-7_W_stt_if_if_if.mp3)
  Segment 32: 84100ms ~ 84676ms (file: 301-360-631-126-123-326-623-103-136-102-312-123-675-136-132-635-576-136-561-152-315-175-103-125-316-132-130-163-605-156-103-263-163-102-673-136-103-106-356-165-316-103-165-613-1_V.mp3)
  Segment 33: 85676ms ~ 86252ms (file: 627-610-163-132-136-136-123-106-120-123-213-132-321-106-130-132-106-102-162-316-162-162-756-726-261-612-261-321-132-312-321-352-321-312-352-301-320 236-213-201-321-136-132-657-6_W.mp3)
  Segment 34: 87252ms ~ 87828ms (file: 167-107-263-263-372-726-276-275-627-623-574-574-574-574-620-620-623-546-574-620-260-267-762-702-176-106-163-310-103-136-301-310-103-130-103-136-761-310-316-671-760-362-360-301-3_W.mp3)
  Segment 35: 88828ms ~ 89404ms (file: 167-130-103-167-516-106-130-163-103-103-136-107-650-163-160-136-167-162-130-103-176-136-756-617-156-162-136-165-165-617-615-105-132-103-671-326-162-136-132-103-103-132-103-132-1_V.mp3)
  Segment 36: 90404ms ~ 90980ms (file: 132-103-103-167-136-167-260-326-320-102-136-127-301-130-627-160-361-503-562-625-615-102-675-675-765-751-671-310-360-207-702-621-672-670-613-163-625-567-567-567-657-576-567-576-6_W.mp3)
  Segment 37: 91980ms ~ 92556ms (file: 263-106-106-263-620-267-627-620-260 267-627-623-123-261-261-627_V.mp3)
  Segment 38: 93556ms ~ 94132ms (file: 126-267-570-756-576-756-576-570-523-542-572-576-507-570-756-756-517-576-150-621-574-574-576-537-571-715-571-756-756-574-576-567-716-576-576-752-176-573-276-756-762-736-376-762-7_W.mp3)
  Segment 39: 95132ms ~ 95708ms (file: 263-263-236-153-132-102-123-124-136-167-132-153-123-176-176-132-621-261-236-321-321-617-617-362-312-316-716-130-103-513-315-306-621-206-126-102-103-130-321-301-172-612-621-167-3_V.mp3)
  Segment 40: 96708ms ~ 104916ms (file: 276-752-672-263-672-372-273-576-756-762-261-256-260-263-103-126-260-652-271-126-765-630-260-620-163-534-261-513-672-627-326-675-547-251-250-265-256-261-620-217-213-571-574-571-2_W.mp3)
  Segment 41: 105916ms ~ 106492ms (file: 576-567-567-516-156-652-657-652-657-623-765-726-326-261-216-261-163-176-617-276-216-162-271-617-271-210-123-163-710-217_W.mp3)
  Segment 42: 107492ms ~ 108068ms (file: 105 312 321-136-132-362-106-103_V.mp3)
  Segment 43: 109068ms ~ 109644ms (file: 623-623-267-263-267-267-632-362-326-263-326-326-321-321-213-263-236-236-263-627-624-267-267-621-261-236-326-236-267-627-263-627-623-267-627-256-263-627-672-627-672-627-567-576-7_W.mp3)
  Segment 44: 110644ms ~ 111220ms (file: 576 627-261-136-726-762-726-762-762 612-132 376 263_W.mp3)
  Segment 45: 112220ms ~ 112796ms (file: 167-675-652-631-136-163-316-261-162-361-126-167-165-651-615-126-162-102 123-123-126-120-216-321-123-216-123-132-106-126-162-126-162-612-126-162-132-130-125-103-520-312-103-163-3_W.mp3)
  Segment 46: 113796ms ~ 118764ms (file: 627-602-602-267-260-623-627-206-632-267-623-321-620-107-627-256-203-216-106-265-265-507-260-602-620-625-261-135-261-267-361-136-136-230-260-560-621-621-267-206-627-106-136-267-6_W.mp3)
  Segment 47: 119764ms ~ 120340ms (file: 651-657-367-753-136-136-162-135 675-671-675-607-675-657-123-136-126-136-567-657-657-672-672-631-621-362-632-627-623-621-632-132-632-675-762-675-267-627-267-627 652-716-756-567-7_W.mp3)
  Segment 48: 121340ms ~ 121916ms (file: 762-765-762-160-362-637-321-236-236-263-236-236-263-102-615-103-165-761-361-362-167-163-610-236-263-621-567-321-132-267-267-527-136-160-176-136-612-632-106-136-136-613-651-167-1_W.mp3)
  Segment 49: 122916ms ~ 123492ms (file: 261 361-132-173-132-123-127-123-231-201-123-413-132-134-132-362-143 132-123-143-132-130-130-130-213-237-231-237-203-267-236-236-203-321-132-123-312-326-206-237-132-216-576-627-6_V.mp3)
  Segment 50: 124492ms ~ 125068ms (file: 753-312-652-312-312-132-321-352-317-137-136-132-523-136-132-375-537-752-132-317-312-671-675-326-163-670-620-260-526-524-502-312-657-752-567-675-765-765-571-627-260-672-263-136-6_W.mp3)
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
> **Time**: 2026-04-04 03:52:13
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "매물 소화 흐름으로 볼 수 있습니다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "매물", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "소화", "start_ms": 175, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "흐름으로", "start_ms": 280, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "볼", "start_ms": 435, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 490, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "있습니다.", "start_ms": 535, "dur_ms": 41, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 3,
"transcript": "매물 소화 돌파가 나온다면 돌파 매매 후보로 접근할 수 있고",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "매물", "start_ms": 45, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "소화", "start_ms": 110, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "돌파가", "start_ms": 175, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "나온다면", "start_ms": 260, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "돌파", "start_ms": 365, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "매매", "start_ms": 430, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "후보로", "start_ms": 495, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "접근할", "start_ms": 580, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 665, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "있고", "start_ms": 690, "dur_ms": 36, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "주도주의 매매",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "주도주의", "start_ms": 30, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "매매", "start_ms": 285, "dur_ms": 291, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "그리고 나서 박스 중심 위에서 작은 박스를 만들었고",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "그리고", "start_ms": 65, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "나서", "start_ms": 190, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "박스", "start_ms": 300, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "중심", "start_ms": 410, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "위에서", "start_ms": 500, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "작은", "start_ms": 590, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "박스를", "start_ms": 680, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "만들었고", "start_ms": 770, "dur_ms": 106, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "3월 19일 아침에 그 자리를 돌파하고 지지가",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "3월", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "19일", "start_ms": 110, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "아침에", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 310, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "자리를", "start_ms": 340, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "돌파하고", "start_ms": 430, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "지지가", "start_ms": 540, "dur_ms": 36, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 8,
"transcript": "사장님 도대체 가격이 얼마인가요? 이게 1인당 9,500원에 무한 리필입니다.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "사장님", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "도대체", "start_ms": 210, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "가격이", "start_ms": 340, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "얼마인가요?", "start_ms": 470, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 700, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "1인당", "start_ms": 790, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "9,500원에", "start_ms": 920, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "무한", "start_ms": 1180, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "리필입니다.", "start_ms": 1270, "dur_ms": 338, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "9,500원 예 물론 생고기",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "9,500원", "start_ms": 35, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "예", "start_ms": 290, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "물론", "start_ms": 340, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "생고기", "start_ms": 430, "dur_ms": 146, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "이 수급 매매를 파고 이제 포텐이 터진 거잖아. 너는 이제 1년 벌었어. 근데 내가 밀착 마크를 하기 때문에 여기서 네가 2-3년만 잘하면 충분히 다시 나",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 60, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "수급", "start_ms": 100, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "매매를", "start_ms": 190, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "파고", "start_ms": 300, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 390, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "포텐이", "start_ms": 480, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "터진", "start_ms": 610, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "거잖아.", "start_ms": 700, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "너는", "start_ms": 850, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 940, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "1년", "start_ms": 1030, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "벌었어.", "start_ms": 1120, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 1300, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "내가", "start_ms": 1390, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "밀착", "start_ms": 1480, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "마크를", "start_ms": 1570, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "하기", "start_ms": 1680, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "때문에", "start_ms": 1770, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "여기서", "start_ms": 1900, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "네가", "start_ms": 2030, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "2-3년만", "start_ms": 2120, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "잘하면", "start_ms": 2280, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "충분히", "start_ms": 2410, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 2540, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "나", "start_ms": 2650, "dur_ms": 30, "phase_lr_deg": 0.0}
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
"transcript": "오늘 잡채도 있네? 맛있겠네요. 부지런히 음식을 담습니다.",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "잡채도", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "있네?", "start_ms": 280, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "맛있겠네요.", "start_ms": 400, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "부지런히", "start_ms": 650, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "음식을", "start_ms": 910, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "담습니다.", "start_ms": 1120, "dur_ms": 252, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 14,
"transcript": "현대 새로운 형태의 사족 보행 로봇을 함께 연구 중인 두 사람. 로봇 제어 시스템 담당은 박혜원 교수가 하드웨어는 오준호 박사가",
"speaker": "speaker_5",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "현대", "start_ms": 60, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "새로운", "start_ms": 170, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "형태의", "start_ms": 300, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "사족", "start_ms": 410, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "보행", "start_ms": 500, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "로봇을", "start_ms": 590, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "함께", "start_ms": 720, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "연구", "start_ms": 810, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "중인", "start_ms": 900, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "두", "start_ms": 990, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "사람.", "start_ms": 1040, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "로봇", "start_ms": 1150, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "제어", "start_ms": 1240, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "시스템", "start_ms": 1330, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "담당은", "start_ms": 1440, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "박혜원", "start_ms": 1570, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "교수가", "start_ms": 1700, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "하드웨어는", "start_ms": 1850, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "오준호", "start_ms": 2010, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "박사가", "start_ms": 2120, "dur_ms": 106, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "물 속에서 헤엄칠 수 있는 물고기 모양의 로봇까지 다양한 로봇을 개발해 왔습니다.",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "물", "start_ms": 45, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "속에서", "start_ms": 95, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "헤엄칠", "start_ms": 185, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 295, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 345, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "물고기", "start_ms": 415, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "모양의", "start_ms": 525, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "로봇까지", "start_ms": 635, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "다양한", "start_ms": 800, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "로봇을", "start_ms": 930, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "개발해", "start_ms": 1060, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "왔습니다.", "start_ms": 1170, "dur_ms": 101, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 17,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 18,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 19,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 20,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 21,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 22,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 23,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 24,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 25,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 26,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 27,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 28,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 29,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 30,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 31,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 32,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 33,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 34,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 35,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 36,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 37,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 38,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 39,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 40,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 41,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 42,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 43,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 44,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 45,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 46,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 47,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 48,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 49,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 50,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-04 03:52:13*
