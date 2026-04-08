# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=15840  time=08:26:09  uptime=13639s  mem=461MB  system_processes=406"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_082315.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 5508ms (file: 267-265-236-623-762-726-361-106-267-163-517-261-627-120-162-367-765-752-106-263-726-102-613-320-106-102-756-123-327-312-270-273-327-765-236-123-130-371-170-176-267-372-310-237-2_W.mp3)
  Segment 2: 6508ms ~ 13312ms (file: 237-726-267-726-263-753-753-123-126-132-213-231-102-760-213-203-276-705-756-756-213-102-106-753-175-123-762-275-231-370-267-260-201-720-276-267-750-275-267-102-752-237-726-670-7_W.mp3)
  Segment 3: 14312ms ~ 14888ms (file: 265-265-627-726-673-327-263-273-267-670-762-760-265-625-603-620-632-637-362-632-627-673-627-627-276-217-276-167-127-215-265-756-756-725-572-375-627-275-526-657-762-675-627-726-6_V.mp3)
  Segment 4: 15888ms ~ 25752ms (file: 230-271-102-203-270-627-765-263-267-213-210-527-672-726-276-260-230-260-254-312-362-132-312-213-132-132-130-130-173-136-627-236-137-270-136-120-123-120-310-213-153-321-123-213-2_W.mp3)
  Segment 5: 26752ms ~ 34564ms (file: 103-103-167-312-751-103-573-615-621-132-132-103-753-750-756-576-675-756-125-126-120-261-657-275-751-260-726-275-250-216-163-130-610-263-621-231-321-231-260-321-230-627-267-627-2_W.mp3)
  Segment 6: 35564ms ~ 42440ms (file: 123-103-623-572-531-715-175-213-321-213-230-123-625-267-267-103-123-123-216-103-725-132-217-127-267-267-123-310-321-302-263-167-132-132-321-261-162-670-213-273-213-231-320-130-3_W.mp3)
  Segment 7: 43440ms ~ 54276ms (file: 261-321-312-137-174-236-132-123-237-123-132-106-263-723-213-321-312-310-170-103-137-765-267-132-312-106-627-756-316-103-672-263-312-310-301-621-263-132-237-263-132-762-273-361-1_W.mp3)
  Segment 8: 55276ms ~ 63556ms (file: 756-231-210-206-203-103-217-756-210-210-230-263-230-572-103-103-260-230-130-167-102-103-267-236-723-725-301-203-213-210-750-270-213-576-752-103-756-276-752-203-236-752-301-267-2_W.mp3)
  Segment 9: 64556ms ~ 70892ms (file: 312-731-237-276-276-547-574-256-273-356-752-132-726-275-327-107-276-276-217-170-120-107-127-126-216-725-632-310-127-321-321-206-207-250-310-321-321-352-312-312-206-207-206-726-1_W.mp3)
  Segment 10: 71892ms ~ 72468ms (file: 517-517-152-156-157-571-531-315-537-375-153-120-123-513-751-513-375-573-537-127-137-572-172-573-157-752 570-152-571-572-175-175-153-135-517-153-157-371-753-751-751-751-571-751-5_V.mp3)
  Segment 11: 73468ms ~ 74044ms (file: 372-765-576-571-156-257-675-627-504-576-362-576-751-572-506-526-257-526-627-206-763-625-561-560-105_V.mp3)
  Segment 12: 75044ms ~ 75620ms (file: 567-752-567-576-751-156-647-672-175-375-376-716-317-712-176-627-625-617-625-613-672-675-617-672-761-172-167-637-612-762-637-672-627-673-637-637-671-762-372-165-671-756-765-756-7_W.mp3)
  Segment 13: 76620ms ~ 78816ms (file: 631-576-627-673-371-316-310-136-574-567-517-156-157-517-517-561-513-573-756-576-537-256-571-617-316-130-132-130-361-136-165-103 201-267-610-237-732-136-213-726-123-231-216-216-3_V_stt_if.mp3)
  Segment 14: 79816ms ~ 91516ms (file: 203-371-310-231-263-236-123-672-132-326-263-136-376-301-321-361-723-627-231-623-136-132-216-126-310-621-213-216-123-321-123-132-312-301-632-230-236-123-107-605-167-107-130-312-2_W.mp3)
  Segment 15: 92516ms ~ 93092ms (file: 756-126-751-675-627-163-673-167-571-156-136-137-170-376-175-167-126-137-763-761-763-756-756-751-672-167-675-625-621-316-564-365-651-605-163-163-617-615-567-671-612-106-765-362-3_W.mp3)
  Segment 16: 94092ms ~ 94668ms (file: 175-372-531-130-173-631-632-316-163-132-132-517-751-137-712-571-137-157-751-751-132-137-217-712-750-750-173-751-751-701-751-753-715-176-713-701-175-715-721-751-715-537-157-751-7_V.mp3)
  Segment 17: 95668ms ~ 103264ms (file: 312-132-123-127-317-301-231-621-270-301-132-570-257-576-751-132-620-260-210-327-163-623-306-573-573-570-312-231-236-312-730-350-301-703-750-570-302-735-132-361-106-103-103-132-1_V.mp3)
  Segment 18: 104264ms ~ 104840ms (file: 730 130-163-136-165-601-176-617-167-137-726-756-617-723-367-675-765-567-736-103_W.mp3)
  Segment 19: 105840ms ~ 112464ms (file: 756-103-136-765-756-265-267-627-631-671-106-130-231-267-276-172-615-576-267-236-162-675-263-261-260-572-230-256-723-751-267-613-163-571-127-120-321-263-321-765-627-123-263-632-2_W.mp3)
  Segment 20: 113464ms ~ 123760ms (file: 102-312-301-301-312 102-126-132-132-132-123-213-123-124-312-134-201-310-501-520-201-231-302-320-570-574-157-135-103-567-560-103-230-321-302-320-203-520-120-123-510-514-132-130-1_V.mp3)
  Segment 21: 124760ms ~ 125336ms (file: 675-157-157-135-367-635-367-351-361-367-163-671-617-675-675-675-763_W.mp3)
  Segment 22: 126336ms ~ 135588ms (file: 236-716-231-236-213-312-123-126-102-267-123-123-627-627-263-236-231-267-231-312-236-160-162-730-120-216-326-672-756-132-216-321-236-263-261-527-120-627-265-231-213-123-216-257-2_W.mp3)
  Segment 23: 136588ms ~ 137164ms (file: 375-137-176-763-675-736-763-716-162-763-167-617-617-763-752-675-761-617-627-367-637-176-673-760-627-216-253-235-703-126-263-321-312-136-136-316-132-162-576-127-275-163-627-263-3_W.mp3)
  Segment 24: 138164ms ~ 138740ms (file: 631-136-137-176-167-716-136-765-675-675-673-675-673-637-327-371-216-312-320 213-507-310-376-237-672-572-672-576-756-723-562-672-273-257-613-671-621-271-216-257-132-132-132-261-2_W.mp3)
  Segment 25: 139740ms ~ 140316ms (file: 125 765-537-651-236-763-562-673-316-713-675-671-637-362-260-627-760-376-162-206-267-206-216-527-175-173-135-375-137-561-571-375-572-537-517-572-576-576-576-576-576-537-315_W.mp3)
  Segment 26: 141316ms ~ 157407ms (file: 312-326-136-230-231-302-301-231-321-341-345-316 362-326-312-317-316-304-345-312 276-132-316-316-145-123-306-263_V.mp3)
  Segment 27: 158407ms ~ 158983ms (file: 321-312 750-567-750-756-507-572-321-537-157-751-102-571-750 176-175-627-213-263-276-570-216-263-273-236-263-756-217-273-273-602-237-613-672-625-613-617-673-637-671-617-376-367-2_W.mp3)
  Segment 28: 159983ms ~ 160559ms (file: 710-106-715-170-710-170-107-715-175-170-176-715-175 157-107-105-107-715-750-715-750-715-175-710-710-175-170-710-751-710-176-176-137-176-173-175-176-716-173-765-716-175-713-751 7_V.mp3)
  Segment 29: 161559ms ~ 162135ms (file: 250-327-732-206-236-317-572-570-236-263-572-236-236-273-237-361-713-732-762-723-706-236-261-236-321-657-763-726-726-756-571-756-671-627-165-327-623-632-752-571-504-753-327-367-7_W.mp3)
  Segment 30: 163135ms ~ 168967ms (file: 327-317-107-370-370-371-706-306-702-317-372-310-376-713-137-763-574-253-524-325-130-103-267-320-261-276-263-213-132-137-753-572-726-206-765-765-705-276-317-367-276-207-137-163-5_W.mp3)
  Segment 31: 169967ms ~ 170543ms (file: 632-567-576-574-576-753-675-367-316-673-356-356-765-675-675-675-657-167-173-675-761_W.mp3)
  Segment 32: 171543ms ~ 183963ms (file: 672-620-620-574-302-301-306-301-103-103-261-726-275-615-706-765-672-231-514-543-260-261-320-237-321-236-106-213-321-120-130-267-236-263-267-263-132-231-237-201-210-231-245-425-6_W.mp3)
  Segment 33: 184963ms ~ 185539ms (file: 150-315-357-637-356-561-651-516-576-567-576-574-576-573-567-561-657 571_W.mp3)
  Segment 34: 186539ms ~ 187115ms (file: 623-756-127-527-312-705-267-320-520-257-756-367-507-276-572-756-206-726-257-275-236-136-607-613-570 572-261-275-217-267-276-267-572-527-570-756-635-325-316-627-132-162-267-125-6_V.mp3)
  Segment 35: 188115ms ~ 200139ms (file: 751-210-316-263-312-261-103-107-174-102-123 750-172-275-172-712-201-207-276-260-273-236-260-206-263-260-276-675-627-276-216-276-765-275-526-260-120-210-201-276-203-251-321-320-2_W.mp3)
  Segment 36: 201139ms ~ 201715ms (file: 176-175-175-175-176-751-751-176-167-176-175-176-173 135-175-173-175-765-173-167-176-176-175-176-176-167-157-715 103-175-173-317-176-617-176-167-157-157-761-751-175-715-716-617-1_W.mp3)
  Segment 37: 202715ms ~ 211787ms (file: 632-756-632-612-106-754-103-163-321-312-735-102-725-312-130-321-312-321-231-321-231-130-231-123-217-231-127-574-574-107-130-130-320-754-320-301-310-123-617-132-321-375-102-125-1_W.mp3)
  Segment 38: 212787ms ~ 213363ms (file: 317-317-172-271-217-571-157-571-710-710-710-175-170-103-713-137-107-170-173-710-715-157-517-572-752-756-752-765-137-132-327-271-261-321-123-132-172-123-126-137-132-127-756-756-7_W.mp3)
  Segment 39: 214363ms ~ 221527ms (file: 312-301-756-756-172-312-203-725-130-231-675-276-230-231-132-201-273-137-250-275-210-205-103-321-203-213-723-231-103-710-103-257-273-321-231-301-126-756-317-273-547-321-275-207-6_W.mp3)
  Segment 40: 222527ms ~ 223103ms (file: 521-275-162-265-256-251-725-736-752-761-563-627-237-321-572-267-276-321-137-132-137-136-721-301-716-751-756-765-357-362-257-137-312-135-317-317-125-130-576-537-573-573-576-574-5_W.mp3)
  Segment 41: 224103ms ~ 224679ms (file: 756-751-316-312-312-312-316-316-316-316-312-310-310-317-316-310-130-130-130-576-632-317-362-316-367-362-132-361-321-321-321-316-312-312-136-316-376-372-176-317-367-327-362-360-3_W.mp3)
  Segment 42: 225679ms ~ 231187ms (file: 627-320-317-123-123-732-302-312-207-127-231-320-231-726-752-375-537-173-102-126-312-321-130-132-123-102-720-130-123-320-132-130-130-103-506-516-752-207-103-123-107-137-312-230-2_W.mp3)
  Segment 43: 232187ms ~ 238559ms (file: 236-321-627-327-173-175-130-267-132-356-265-672-627-263-713-672-267-216-103-136-361-167-167-167-275-167-570-362-103-135-751-154-320-571-106-175-754-327-130-106-137-170-137-356-5_W.mp3)
  Segment 44: 239559ms ~ 243735ms (file: 321-231-206-263-206-230-217-123-132-132-132-132-312-123-321-126-132-132-231-312-136-132-123-213-123-312-213-123-132-231-321-231-213-213-213-321-321-132-320-320-213-132-321-123-2_W.mp3)
  Segment 45: 244735ms ~ 245311ms (file: 735-621-267-261-576-327-567-573-132-263-321-572-725-507-573-127-175-263-506-276-275-702-763-752-257-275-750-576-562-756-357-267-756-756-502-572-576 675-756-723-367-637-571-763-7_V.mp3)
  Segment 46: 246311ms ~ 246887ms (file: 763-765-675-176-751-675-765-673-765-736-631-716-763-761_W.mp3)
  Segment 47: 247887ms ~ 248463ms (file: 132-375-301-576-574-576-750-756-754-750_W.mp3)
  Segment 48: 249463ms ~ 250039ms (file: 372-673-675-673-673-673-673-673-763-367-617-637 175-132_W.mp3)
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
> **Time**: 2026-04-06 08:28:07
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "이런 시장이었습니다 이런 시장에 잘못 매매하면 굉장히 크게 손실 볼 수도 있는데 지수가 하루에 5퍼센트",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 267,
"audio_type": "speech",
"words": [
{"word": "이런", "start_ms": 267, "dur_ms": 265, "phase_lr_deg": 0.0},
{"word": "시장이었습니다", "start_ms": 532, "dur_ms": 236, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 768, "dur_ms": 623, "phase_lr_deg": 0.0},
{"word": "시장에", "start_ms": 1391, "dur_ms": 762, "phase_lr_deg": 0.0},
{"word": "잘못", "start_ms": 2153, "dur_ms": 726, "phase_lr_deg": 0.0},
{"word": "매매하면", "start_ms": 2879, "dur_ms": 361, "phase_lr_deg": 0.0},
{"word": "굉장히", "start_ms": 3240, "dur_ms": 106, "phase_lr_deg": 0.0},
{"word": "크게", "start_ms": 3346, "dur_ms": 267, "phase_lr_deg": 0.0},
{"word": "손실", "start_ms": 3613, "dur_ms": 163, "phase_lr_deg": 0.0},
{"word": "볼", "start_ms": 3776, "dur_ms": 517, "phase_lr_deg": 0.0},
{"word": "수도", "start_ms": 4293, "dur_ms": 261, "phase_lr_deg": 0.0},
{"word": "있는데", "start_ms": 4554, "dur_ms": 627, "phase_lr_deg": 0.0},
{"word": "지수가", "start_ms": 5181, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "하루에", "start_ms": 5301, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "5퍼센트", "start_ms": 5401, "dur_ms": 107, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "이어지기 때문입니다 결국 대만 방송이 논란 핵심은 하나입니다 한국 방위 산업이 이제 우연히 몇 건 수출한 수준이 아니라 세계 질서 변화 속에서 기회를 산업",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 237,
"audio_type": "speech",
"words": [
{"word": "이어지기", "start_ms": 237, "dur_ms": 726, "phase_lr_deg": 0.0},
{"word": "때문입니다", "start_ms": 963, "dur_ms": 267, "phase_lr_deg": 0.0},
{"word": "결국", "start_ms": 1230, "dur_ms": 726, "phase_lr_deg": 0.0},
{"word": "대만", "start_ms": 1956, "dur_ms": 263, "phase_lr_deg": 0.0},
{"word": "방송이", "start_ms": 2219, "dur_ms": 753, "phase_lr_deg": 0.0},
{"word": "논란", "start_ms": 2972, "dur_ms": 753, "phase_lr_deg": 0.0},
{"word": "핵심은", "start_ms": 3725, "dur_ms": 123, "phase_lr_deg": 0.0},
{"word": "하나입니다", "start_ms": 3848, "dur_ms": 126, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 3974, "dur_ms": 132, "phase_lr_deg": 0.0},
{"word": "방위", "start_ms": 4106, "dur_ms": 213, "phase_lr_deg": 0.0},
{"word": "산업이", "start_ms": 4319, "dur_ms": 231, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 4550, "dur_ms": 102, "phase_lr_deg": 0.0},
{"word": "우연히", "start_ms": 4652, "dur_ms": 760, "phase_lr_deg": 0.0},
{"word": "몇", "start_ms": 5412, "dur_ms": 213, "phase_lr_deg": 0.0},
{"word": "건", "start_ms": 5625, "dur_ms": 203, "phase_lr_deg": 0.0},
{"word": "수출한", "start_ms": 5828, "dur_ms": 276, "phase_lr_deg": 0.0},
{"word": "수준이", "start_ms": 6104, "dur_ms": 705, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 6809, "dur_ms": 756, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 4,
"transcript": "끊이지 않는다고 하죠 그런데 이게 다가 아닙니다 이렇게 한식이 인기를 끌자 돈 냄새를 맡은 중국인들이 한국인 흉내를 내며 한식당을 차리는데 아주 형편없거든요",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 230,
"audio_type": "speech",
"words": [
{"word": "끊이지", "start_ms": 230, "dur_ms": 271, "phase_lr_deg": 0.0},
{"word": "않는다고", "start_ms": 501, "dur_ms": 102, "phase_lr_deg": 0.0},
{"word": "하죠", "start_ms": 603, "dur_ms": 203, "phase_lr_deg": 0.0},
{"word": "그런데", "start_ms": 806, "dur_ms": 270, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 1076, "dur_ms": 627, "phase_lr_deg": 0.0},
{"word": "다가", "start_ms": 1703, "dur_ms": 765, "phase_lr_deg": 0.0},
{"word": "아닙니다", "start_ms": 2468, "dur_ms": 263, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 2731, "dur_ms": 267, "phase_lr_deg": 0.0},
{"word": "한식이", "start_ms": 2998, "dur_ms": 213, "phase_lr_deg": 0.0},
{"word": "인기를", "start_ms": 3211, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "끌자", "start_ms": 3421, "dur_ms": 527, "phase_lr_deg": 0.0},
{"word": "돈", "start_ms": 3948, "dur_ms": 672, "phase_lr_deg": 0.0},
{"word": "냄새를", "start_ms": 4620, "dur_ms": 726, "phase_lr_deg": 0.0},
{"word": "맡은", "start_ms": 5346, "dur_ms": 276, "phase_lr_deg": 0.0},
{"word": "중국인들이", "start_ms": 5622, "dur_ms": 260, "phase_lr_deg": 0.0},
{"word": "한국인", "start_ms": 5882, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "흉내를", "start_ms": 6112, "dur_ms": 260, "phase_lr_deg": 0.0},
{"word": "내며", "start_ms": 6372, "dur_ms": 254, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "여기는 창고나 뭐 열어볼까요? 우와 이게 뭐죠? 이것도 연기네 아니 연기만 두",
"speaker": "speaker_4",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 103,
"audio_type": "speech",
"words": [
{"word": "여기는", "start_ms": 103, "dur_ms": 103, "phase_lr_deg": 0.0},
{"word": "창고나", "start_ms": 206, "dur_ms": 167, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 373, "dur_ms": 312, "phase_lr_deg": 0.0},
{"word": "열어볼까요?", "start_ms": 685, "dur_ms": 751, "phase_lr_deg": 0.0},
{"word": "우와", "start_ms": 1436, "dur_ms": 103, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 1539, "dur_ms": 573, "phase_lr_deg": 0.0},
{"word": "뭐죠?", "start_ms": 2112, "dur_ms": 615, "phase_lr_deg": 0.0},
{"word": "이것도", "start_ms": 2727, "dur_ms": 621, "phase_lr_deg": 0.0},
{"word": "연기네", "start_ms": 3348, "dur_ms": 132, "phase_lr_deg": 0.0},
{"word": "아니", "start_ms": 3480, "dur_ms": 132, "phase_lr_deg": 0.0},
{"word": "연기만", "start_ms": 3612, "dur_ms": 103, "phase_lr_deg": 0.0},
{"word": "두", "start_ms": 3715, "dur_ms": 753, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "미나리가 잘 어울리네 삼겹살이랑 아 그래요? 잘 어울리네 난 미나리 별로 좋아하진 않는",
"speaker": "speaker_5",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 123,
"audio_type": "speech",
"words": [
{"word": "미나리가", "start_ms": 123, "dur_ms": 103, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 226, "dur_ms": 623, "phase_lr_deg": 0.0},
{"word": "어울리네", "start_ms": 849, "dur_ms": 572, "phase_lr_deg": 0.0},
{"word": "삼겹살이랑", "start_ms": 1421, "dur_ms": 531, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 1952, "dur_ms": 715, "phase_lr_deg": 0.0},
{"word": "그래요?", "start_ms": 2667, "dur_ms": 175, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 2842, "dur_ms": 213, "phase_lr_deg": 0.0},
{"word": "어울리네", "start_ms": 3055, "dur_ms": 321, "phase_lr_deg": 0.0},
{"word": "난", "start_ms": 3376, "dur_ms": 213, "phase_lr_deg": 0.0},
{"word": "미나리", "start_ms": 3589, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "별로", "start_ms": 3819, "dur_ms": 123, "phase_lr_deg": 0.0},
{"word": "좋아하진", "start_ms": 3942, "dur_ms": 625, "phase_lr_deg": 0.0},
{"word": "않는", "start_ms": 4567, "dur_ms": 267, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "한국에서도 휴머노이드 로봇을 생산해야 되는 거죠 그런데 아틀라스와 동일한 방식으로는 좀 어렵다라는 겁니다 그래서 2030년 이전까지는 모베드 기반 로봇이나 다리 없는 탁상용 휴머노이드로 대응을 하고 이후에는",
"speaker": "speaker_6",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 261,
"audio_type": "speech",
"words": [
{"word": "한국에서도", "start_ms": 261, "dur_ms": 321, "phase_lr_deg": 0.0},
{"word": "휴머노이드", "start_ms": 582, "dur_ms": 312, "phase_lr_deg": 0.0},
{"word": "로봇을", "start_ms": 894, "dur_ms": 137, "phase_lr_deg": 0.0},
{"word": "생산해야", "start_ms": 1031, "dur_ms": 174, "phase_lr_deg": 0.0},
{"word": "되는", "start_ms": 1205, "dur_ms": 236, "phase_lr_deg": 0.0},
{"word": "거죠", "start_ms": 1441, "dur_ms": 132, "phase_lr_deg": 0.0},
{"word": "그런데", "start_ms": 1573, "dur_ms": 123, "phase_lr_deg": 0.0},
{"word": "아틀라스와", "start_ms": 1696, "dur_ms": 237, "phase_lr_deg": 0.0},
{"word": "동일한", "start_ms": 1933, "dur_ms": 123, "phase_lr_deg": 0.0},
{"word": "방식으로는", "start_ms": 2056, "dur_ms": 132, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 2188, "dur_ms": 106, "phase_lr_deg": 0.0},
{"word": "어렵다라는", "start_ms": 2294, "dur_ms": 263, "phase_lr_deg": 0.0},
{"word": "겁니다", "start_ms": 2557, "dur_ms": 723, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "그는 대만이 국제회의를 계속 보이콧할 경우 글로벌 무대에서 발언권을 더 잃을 수 있다고 우려했다 외교의 본질은 친구를 만드는 것이지 문제를 만드는 것이 아니라며 최근 대만의 접근 방식은 그 반대의",
"speaker": "speaker_7",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 756,
"audio_type": "speech",
"words": [
{"word": "그는", "start_ms": 756, "dur_ms": 231, "phase_lr_deg": 0.0},
{"word": "대만이", "start_ms": 987, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "국제회의를", "start_ms": 1197, "dur_ms": 206, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 1403, "dur_ms": 203, "phase_lr_deg": 0.0},
{"word": "보이콧할", "start_ms": 1606, "dur_ms": 103, "phase_lr_deg": 0.0},
{"word": "경우", "start_ms": 1709, "dur_ms": 217, "phase_lr_deg": 0.0},
{"word": "글로벌", "start_ms": 1926, "dur_ms": 756, "phase_lr_deg": 0.0},
{"word": "무대에서", "start_ms": 2682, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "Okay mostly no I think it's like a soy and mustard",
"speaker": "speaker_8",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 312,
"audio_type": "speech",
"words": [
{"word": "Okay", "start_ms": 312, "dur_ms": 731, "phase_lr_deg": 0.0},
{"word": "mostly", "start_ms": 1043, "dur_ms": 237, "phase_lr_deg": 0.0},
{"word": "no", "start_ms": 1280, "dur_ms": 276, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 1556, "dur_ms": 276, "phase_lr_deg": 0.0},
{"word": "think", "start_ms": 1832, "dur_ms": 547, "phase_lr_deg": 0.0},
{"word": "it's", "start_ms": 2379, "dur_ms": 574, "phase_lr_deg": 0.0},
{"word": "like", "start_ms": 2953, "dur_ms": 256, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 3209, "dur_ms": 273, "phase_lr_deg": 0.0},
{"word": "soy", "start_ms": 3482, "dur_ms": 356, "phase_lr_deg": 0.0},
{"word": "and", "start_ms": 3838, "dur_ms": 752, "phase_lr_deg": 0.0},
{"word": "mustard", "start_ms": 4590, "dur_ms": 132, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "어머 이런 초밥도 만들 수 있네요 감사합니다 아 누를 때 봤는데 아닌데 아 전화 하세요 여보세요 어 한복도 있다 어 그러게 한복도 있네 카리나 하나 더 입어봐야 되나 같이",
"speaker": "speaker_9",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 203,
"audio_type": "speech",
"words": [
{"word": "어머", "start_ms": 203, "dur_ms": 371, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 574, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "초밥도", "start_ms": 884, "dur_ms": 231, "phase_lr_deg": 0.0},
{"word": "만들", "start_ms": 1115, "dur_ms": 263, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 1378, "dur_ms": 236, "phase_lr_deg": 0.0},
{"word": "있네요", "start_ms": 1614, "dur_ms": 123, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 1737, "dur_ms": 672, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "That the AI system is going to behave just like a really trustworthy human personal assistant But is the system real",
"speaker": "speaker_10",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 312,
"audio_type": "speech",
"words": [
{"word": "That", "start_ms": 312, "dur_ms": 132, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 444, "dur_ms": 123, "phase_lr_deg": 0.0},
{"word": "AI", "start_ms": 567, "dur_ms": 127, "phase_lr_deg": 0.0},
{"word": "system", "start_ms": 694, "dur_ms": 317, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 1011, "dur_ms": 301, "phase_lr_deg": 0.0},
{"word": "going", "start_ms": 1312, "dur_ms": 231, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 1543, "dur_ms": 621, "phase_lr_deg": 0.0},
{"word": "behave", "start_ms": 2164, "dur_ms": 270, "phase_lr_deg": 0.0},
{"word": "just", "start_ms": 2434, "dur_ms": 301, "phase_lr_deg": 0.0},
{"word": "like", "start_ms": 2735, "dur_ms": 132, "phase_lr_deg": 0.0}
]
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
"transcript": "22년 8월 이후 최고치를 기록했습니다 갤런당 4달러는 고물가의 심리적 기준으로 인식됩니다 뉴욕에서 MBC 뉴",
"speaker": "speaker_11",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 756,
"audio_type": "speech",
"words": [
{"word": "22년", "start_ms": 756, "dur_ms": 103, "phase_lr_deg": 0.0},
{"word": "8월", "start_ms": 859, "dur_ms": 136, "phase_lr_deg": 0.0},
{"word": "이후", "start_ms": 995, "dur_ms": 765, "phase_lr_deg": 0.0},
{"word": "최고치를", "start_ms": 1760, "dur_ms": 756, "phase_lr_deg": 0.0},
{"word": "기록했습니다", "start_ms": 2516, "dur_ms": 265, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "inside each other At the same time I can get so much done and I can tell you my last startup we had a lot more staff and we got a lot less done than we do right now",
"speaker": "speaker_12",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 102,
"audio_type": "speech",
"words": [
{"word": "inside", "start_ms": 102, "dur_ms": 312, "phase_lr_deg": 0.0},
{"word": "each", "start_ms": 414, "dur_ms": 301, "phase_lr_deg": 0.0},
{"word": "other", "start_ms": 715, "dur_ms": 301, "phase_lr_deg": 0.0},
{"word": "At", "start_ms": 1016, "dur_ms": 312, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 1328, "dur_ms": 102, "phase_lr_deg": 0.0},
{"word": "same", "start_ms": 1430, "dur_ms": 126, "phase_lr_deg": 0.0},
{"word": "time", "start_ms": 1556, "dur_ms": 132, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 1688, "dur_ms": 132, "phase_lr_deg": 0.0}
]
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
"transcript": "요청했다는 보도도 이어지고 있는 상황이죠 이런 상황에서 방공망이 일부 뚫리는 사례까지 나오면서 아이언 돔 신화에도 균열이 생기고 있습니다 이스라엘은 여전히 방공망에는 문제가 없다는 입장을 유",
"speaker": "speaker_13",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 236,
"audio_type": "speech",
"words": [
{"word": "요청했다는", "start_ms": 236, "dur_ms": 716, "phase_lr_deg": 0.0},
{"word": "보도도", "start_ms": 952, "dur_ms": 231, "phase_lr_deg": 0.0},
{"word": "이어지고", "start_ms": 1183, "dur_ms": 236, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 1419, "dur_ms": 213, "phase_lr_deg": 0.0},
{"word": "상황이죠", "start_ms": 1632, "dur_ms": 312, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 1944, "dur_ms": 123, "phase_lr_deg": 0.0},
{"word": "상황에서", "start_ms": 2067, "dur_ms": 126, "phase_lr_deg": 0.0}
]
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
"transcript": "스스로 행동하는 AI가 로봇이라는 몸을 얻으면 세상은 또 한번 달라진",
"speaker": "speaker_14",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 312,
"audio_type": "speech",
"words": [
{"word": "스스로", "start_ms": 312, "dur_ms": 326, "phase_lr_deg": 0.0},
{"word": "행동하는", "start_ms": 638, "dur_ms": 136, "phase_lr_deg": 0.0},
{"word": "AI가", "start_ms": 774, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "로봇이라는", "start_ms": 1004, "dur_ms": 231, "phase_lr_deg": 0.0},
{"word": "몸을", "start_ms": 1235, "dur_ms": 302, "phase_lr_deg": 0.0},
{"word": "얻으면", "start_ms": 1537, "dur_ms": 301, "phase_lr_deg": 0.0},
{"word": "세상은", "start_ms": 1838, "dur_ms": 231, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 2069, "dur_ms": 321, "phase_lr_deg": 0.0},
{"word": "한번", "start_ms": 2390, "dur_ms": 341, "phase_lr_deg": 0.0},
{"word": "달라진", "start_ms": 2731, "dur_ms": 345, "phase_lr_deg": 0.0}
]
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
"transcript": "네 죄송합니다 원 투 쓰리 작년에 미국에서 뵀는데 다시 이렇게 한국에서 뵙게 되니까 너무 반갑습니다 짧은 시간",
"speaker": "speaker_15",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 327,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 327, "dur_ms": 317, "phase_lr_deg": 0.0},
{"word": "죄송합니다", "start_ms": 644, "dur_ms": 107, "phase_lr_deg": 0.0},
{"word": "원", "start_ms": 751, "dur_ms": 370, "phase_lr_deg": 0.0},
{"word": "투", "start_ms": 1121, "dur_ms": 370, "phase_lr_deg": 0.0},
{"word": "쓰리", "start_ms": 1491, "dur_ms": 371, "phase_lr_deg": 0.0},
{"word": "작년에", "start_ms": 1862, "dur_ms": 706, "phase_lr_deg": 0.0}
]
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
"transcript": "solve some of the hardest problems in science The fact that AI can write full pieces of software the fact that soon AI can do more generalized knowledge work But those are all huge",
"speaker": "speaker_16",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 672,
"audio_type": "speech",
"words": [
{"word": "solve", "start_ms": 672, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "some", "start_ms": 1292, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "of", "start_ms": 1912, "dur_ms": 574, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 2486, "dur_ms": 302, "phase_lr_deg": 0.0},
{"word": "hardest", "start_ms": 2788, "dur_ms": 301, "phase_lr_deg": 0.0},
{"word": "problems", "start_ms": 3089, "dur_ms": 306, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 3395, "dur_ms": 301, "phase_lr_deg": 0.0},
{"word": "science", "start_ms": 3696, "dur_ms": 103, "phase_lr_deg": 0.0}
]
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
"transcript": "모습입니다 러시아의 기간 물결이 키이우를 향해 돌진했을 때 유럽 전체는 거대한 공포에 빠졌습니다 특히 러시아에 맞서 최전선에 서 있던 폴란드는 더욱 그랬습니다 자",
"speaker": "speaker_17",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 623,
"audio_type": "speech",
"words": [
{"word": "모습입니다", "start_ms": 623, "dur_ms": 756, "phase_lr_deg": 0.0},
{"word": "러시아의", "start_ms": 1379, "dur_ms": 127, "phase_lr_deg": 0.0},
{"word": "기간", "start_ms": 1506, "dur_ms": 527, "phase_lr_deg": 0.0},
{"word": "물결이", "start_ms": 2033, "dur_ms": 312, "phase_lr_deg": 0.0},
{"word": "키이우를", "start_ms": 2345, "dur_ms": 705, "phase_lr_deg": 0.0},
{"word": "향해", "start_ms": 3050, "dur_ms": 267, "phase_lr_deg": 0.0},
{"word": "돌진했을", "start_ms": 3317, "dur_ms": 320, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "그래가지고 자궁 열려가지고 진통만 오면 애기가 나오고 직전이라는 거예요 그래가지고 막",
"speaker": "speaker_18",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 751,
"audio_type": "speech",
"words": [
{"word": "그래가지고", "start_ms": 751, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "자궁", "start_ms": 961, "dur_ms": 316, "phase_lr_deg": 0.0},
{"word": "열려가지고", "start_ms": 1277, "dur_ms": 263, "phase_lr_deg": 0.0},
{"word": "진통만", "start_ms": 1540, "dur_ms": 312, "phase_lr_deg": 0.0},
{"word": "오면", "start_ms": 1852, "dur_ms": 261, "phase_lr_deg": 0.0},
{"word": "애기가", "start_ms": 2113, "dur_ms": 103, "phase_lr_deg": 0.0},
{"word": "나오고", "start_ms": 2216, "dur_ms": 107, "phase_lr_deg": 0.0}
]
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
"transcript": "장이 쾌적하게 또 온디바이스 AI를 돌릴 수 있기 때문에 그런 관점에서 어 제마프로를 한번 돌려보면서 절대로 오지도 않을 것 같은 온디바이스 AI라고 하는 것이",
"speaker": "speaker_19",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 632,
"audio_type": "speech",
"words": [
{"word": "장이", "start_ms": 632, "dur_ms": 756, "phase_lr_deg": 0.0},
{"word": "쾌적하게", "start_ms": 1388, "dur_ms": 632, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 2020, "dur_ms": 612, "phase_lr_deg": 0.0},
{"word": "온디바이스", "start_ms": 2632, "dur_ms": 106, "phase_lr_deg": 0.0},
{"word": "AI를", "start_ms": 2738, "dur_ms": 754, "phase_lr_deg": 0.0},
{"word": "돌릴", "start_ms": 3492, "dur_ms": 103, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 3595, "dur_ms": 163, "phase_lr_deg": 0.0},
{"word": "있기", "start_ms": 3758, "dur_ms": 321, "phase_lr_deg": 0.0},
{"word": "때문에", "start_ms": 4079, "dur_ms": 312, "phase_lr_deg": 0.0}
]
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
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
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
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 08:28:07*
