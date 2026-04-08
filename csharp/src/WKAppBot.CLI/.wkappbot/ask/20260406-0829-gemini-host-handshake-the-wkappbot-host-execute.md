# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=23176  time=08:26:48  uptime=16676s  mem=429MB  system_processes=406"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_082249.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 276-267-762-327-752-326-372-265-754-527-527-620-576-741-754-237-573-762-607-560-567-567_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 367-173-673-763-765-675-762-675-673-761-765-763-671-763-762-627-763-675-765-675-675-130-130-673-672-673-765-763-765-673-671-672-673-672-763-675-671-673-671-716-167-671-675-637 5_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 175-753-571-571-571-571-570-570-570-571-570-570-573-576-576 576-751-571-570-576-571-573-571-570-570-571-573-571-571-751-750-571-537 573-573-576-572-572-574-576-571-571-751-571-5_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 715-715-175-701-710-170-173-170-107-701 715-106-715-710-750-701-710-715-710-176-750-701-715-751-751-710-705 617-617-157-157-157-107-701-107-571-170-710-751-107-157-107-751-701-7_V.mp3)
  Segment 5: 6304ms ~ 13108ms (file: 103-167-613-103-103-103-106-106-571-136-130-267-267-726-576-627-627-206-260-310-637-675-316-765-756-762-637-103-713-176-213-321-257-132-206-130-316-613-671-163-367-715-301-312-3_W.mp3)
  Segment 6: 14108ms ~ 14684ms (file: 256-160-105-210-215-103-130-130-130-527-562-205-654-562-265-625-657-576-651-156-165-567-136-125-165-573-235-502-126-527-153-165-156-135-165-106-165-125-120-165-106-106-106-657-5_W.mp3)
  Segment 7: 15684ms ~ 24000ms (file: 103-210-620-123-213-123-231-261-267-261-261-267-756-267-123-127-673-762-756-762-261-123-130-263-765-260-106-261-231-120-132-102-236-130-203-623-102-231-321-103-102-756-130-257-3_W.mp3)
  Segment 8: 25000ms ~ 33964ms (file: 130-103-103-574-571-321-276-236-321-753-756-756-570-752-756-230-132-301-143-163-162-213-756-320-370-135-637-756-573-310-312-357-206-572-754-260-620-621-612-236-276-623-263-127-2_W.mp3)
  Segment 9: 34964ms ~ 44036ms (file: 126-102-210-120-126-276-726-206-103-213-203-102-213-261-236-263-261-236-213-321-231-260-206-310-261-154-321-132-317-137-123-321-316-123-132-132-316-312-154-135-103-175-731-102-7_W.mp3)
  Segment 10: 45036ms ~ 45612ms (file: 521-275-162-265-256-251-725-736-752-761-563-627-237-321-572-267-276-321-137-132-137-136-721-301-716-751-756-765-357-362-257-137-312-135-317-317-125-130-576-537-573-573-576-574-5_W.mp3)
  Segment 11: 46612ms ~ 47188ms (file: 507-261-607-367-756-276-261-263-132-312-263-325-576-352-326-132-356-316-312-371-371-316-312-130-301-237-657-537-357-573-574-756-574-576-576-137-375_W.mp3)
  Segment 12: 48188ms ~ 56180ms (file: 312-231-527-527-567-236-376-102-236-576-256-162-126-362-326-623-326-136-105-263-326-621-523-623-231-576-570-123-756-267-123-301-261-267-231-263-326-321-321-236-532-571-231-263-7_W.mp3)
  Segment 13: 57180ms ~ 63948ms (file: 657-675-602-605-601-751-761-236-217-671-271-273-176-173-160-267-237-327-756-756-573-361-762-712-176-103-137-327-576-576-617-716-106-130-103-176-106-710-167-301-327-372-130-672-6_W.mp3)
  Segment 14: 64948ms ~ 65524ms (file: 150-315-357-637-356-561-651-516-576-567-576-574-576-573-567-561-657 571_W.mp3)
  Segment 15: 66524ms ~ 75848ms (file: 763-726-762-726-765-721-276-762-726-732-763-726-732-762-326-574-271-132-103-106-726-713-103-372-321-310-201-312-107-103-102-102-103-132-103-143-756-562-103-207-206-627-320-230-7_W.mp3)
  Segment 16: 76848ms ~ 83940ms (file: 270-102-275-230-276-276-752-725-672-302-547-315-237-271-107-217-207-547-157-267-216-206-261-320-276-236-201-542-321-103-261-257-213-213-210-267-127-276-726-726-276-276-206-542-2_W.mp3)
  Segment 17: 84940ms ~ 85516ms (file: 163-615-135-135-317-132-312-263-352-235-237-312_W.mp3)
  Segment 18: 86516ms ~ 87092ms (file: 765-756-765-375-657-165-167-165-675-156-675-675-756-765-675-756-751-672-675-765-657-576-567-571-561-567-576-715-751-175-576-156-156-165-156-165-167-126-126-123-756-756-756-765-7_W.mp3)
  Segment 19: 88092ms ~ 88668ms (file: 175-716-715-376-361-763-736-761-761-716-765-761_W.mp3)
  Segment 20: 89668ms ~ 90244ms (file: 574-576-576-572-574-574-574-574-574-574-571 576-576-576-517-571-517-510-510-571-751-517-571-751-571-715-517-750-571-571-571-572-576-571-751-750 571-571-571-571-750-507-570-570-5_W.mp3)
  Segment 21: 91244ms ~ 95528ms (file: 571-517-325-157-507-576-567-504-251-251-320-203-310-327-276-672-321-231-527-361-573-573-573-352-253-527-235-725-527-201-527-152-270-275-752-765-230-510-206-517-130-103-130-123-1_W.mp3)
  Segment 22: 96528ms ~ 97104ms (file: 756-576-573-725-130-317-312-310-736-163-613-605-672-621-750-617-750-756-102-123-750-217-637-672-216-513-275-261-570-213-536-132-132-213-132-172-312-356-275-276-217-176-157-206-5_V.mp3)
  Segment 23: 98104ms ~ 98680ms (file: 672 567-567 312-316-310-762-163-137-126-321-236-275-253-657-560-561-567-567 627-623-716-362-576-125-627-765 756-576-756-675-756-675-675-761-761-675-761-671-651-567-576-672-653-5_W.mp3)
  Segment 24: 99680ms ~ 108500ms (file: 617-617-103-163-326-236-523-574-576-632-351-537-672-267-236-263-321-257-267-267-627-267-573-106-310-137-103-130-267-537-756-267-670-627-102-162-627-173-316-276-371-103-672-236-2_W.mp3)
  Segment 25: 109500ms ~ 115152ms (file: 267-103-231-236-263-267-267-267-267-263-237-126-276-267-267-261-267-261-267-265-257-205-267-267-672-267-263-236-576-103-576-216-132-267-267-123-260-130-106-752-265-162-756-716-3_W.mp3)
  Segment 26: 116152ms ~ 116728ms (file: 126-217-125-125-132-132-136-132-132-123-132-123-132-163-162-163-162-157-153-150-175-571-715-176-157-571-157-175-157-517-175-157-751-571-753-531-571-571-175-123-126-162-126-123-1_W.mp3)
  Segment 27: 117728ms ~ 123632ms (file: 217-213-132-123-231-102-103-103-310-130-130-327-231-327-371-320-321-357-320-206-206-130-107-326-236-326-320-326-236-326-236-203-123-316-160-126-123-216-132-126-216-126-236-312-3_W.mp3)
  Segment 28: 124632ms ~ 126468ms (file: 106-506-365-631-361-371-362-361-723-317-716-712-271-672-567-176-162-217-213-312-316-263-362-326-136-613-361-316-631-316-637-631-360-601-602-130-106-306-176-167-371-137-631-716-6_W.mp3)
  Segment 29: 127468ms ~ 128044ms (file: 765-736-763-765-765-756-267-754-132-573-127-206-215-765-267-231-213-257-217-162_V.mp3)
  Segment 30: 129044ms ~ 129620ms (file: 327-310-310-307-317-371-137-301-765-107-173-170-751-132-756-725-617-157-761-716_W.mp3)
  Segment 31: 130620ms ~ 131196ms (file: 276-527 520-625-752-236-153-572-725-571-705-126-536-152-570-136-137-615-231-267-506-325-570-562-315-671-315-570-321-273-732-507-103-763-317-173-651-273-273-705-375-572-756-275-2_V.mp3)
  Segment 32: 132196ms ~ 132772ms (file: 157-153-153 105-517-571-150 574-576-571-574-572-574-574-574-574-574-574-571-517-150-150-517-175-713-317-715 105-715-175-705-701-725-703-705-701-107-715-175-715-715-731-175-710-7_V.mp3)
  Segment 33: 133772ms ~ 134348ms (file: 710-106-715-170-710-170-107-715-175-170-176-715-175 157-107-105-107-715-750-715-750-715-175-710-710-175-170-710-751-710-176-176-137-176-173-175-176-716-173-765-716-175-713-751 7_V.mp3)
  Segment 34: 135348ms ~ 135924ms (file: 175-175-751-571-157-175-157-157-137-167-176-176-176-167-175-175 135-107-157-175-175-571-517-571-517-157-175-157-176-165-157-157-175-157-167 105-150-157-175-173-175-157-175-517-1_W.mp3)
  Segment 35: 136924ms ~ 137500ms (file: 170-701-713-371-317-372-371-173-713-735-576-753-765-753-675 157-157 574-574-574-574-574-574-571-571-571-571-756-571-752-571-751-756-751 571-573-571-576-751-572-756-753-517-501-7_V.mp3)
  Segment 36: 138500ms ~ 139076ms (file: 567-576-715-173-571-375-571-715-170-705-105-170-715-705-317-751-751-753-756-576 756-571-153-756-375-762-576-731-751-715-576-573-713-710-715-756-751-570-756-576-715-751-571-576-5_V.mp3)
  Segment 37: 140076ms ~ 140652ms (file: 625-135-152-167-163-613-165-367-312-612-632-673-126-670-612-316-675-562-176-315-765-130-576-751-651-123-752-236-326-163-716-765-637-651-653-265-657-675-516-165-132-153-615-765-3_V.mp3)
  Segment 38: 141652ms ~ 142228ms (file: 153-653-136-165-576-760-761-710-760-761-736-736-716-751-517-672-705-617-165-135-625-523-725-165-756-731-253-235-175-610-632-653-132-267-312-504-215-231-123-576-652-632-623-572_W.mp3)
  Segment 39: 143228ms ~ 143804ms (file: 261-750-276-570-276-273-627-176-560-256-326-253-675-726-612-567_V.mp3)
  Segment 40: 144804ms ~ 145380ms (file: 625-657-526-562-265-365-265-625-627-625-672-256-657-562-657-657-756-657-657-625-627-672-675-637-673-367-673-673-637-736-763-657-671-736-765-763-673-675-765-625-671-627-762-612-6_W.mp3)
  Segment 41: 146380ms ~ 152788ms (file: 756-576-167-107-761-675-302-321-235-275-576-573-350-325-756-753-357-312-137-132-136-137-137-137-137-173-713-713-317-136-263-321-751-130-136-123-132-267-123-310-312-563-312-762-5_W.mp3)
  Segment 42: 153788ms ~ 161384ms (file: 102-612-623-751-126-175-351-627-623-160-260-627-132-601-612-352-206-236-237-236-507-257-236-627-230-261-625-267-602-321-207-625-263-103-231-203-213-231-132-532-265-213-263-623-3_W.mp3)
  Segment 43: 162384ms ~ 162960ms (file: 657-657-675-675-765-136-137-316-316-361-756-765-736-613-136 637-316-362-153-632-613 136-163-136 674-675 673-635-132-317-672-631-362-321-361-316-132-312-132-136-132-132-312-132-1_W.mp3)
  Segment 44: 163960ms ~ 164536ms (file: 231-267-326-251-231-203-326-236-321-576-526-162-576-572-267-567-562-267-627-657-652-315-637-267-672-517-256-263-257-570-532-753-275-576-762-367-327-576-750-132-276-726-205-216-2_V.mp3)
  Segment 45: 165536ms ~ 170432ms (file: 213-276-120-216-102-263-273-132-103-231-132-123-213-127-102-173-120-213-275-267-216-213-127-271-351-261-312-213-127-263-213-126-271-126-216-123-316-132-123-123-132-123-275-321-1_W.mp3)
  Segment 46: 171432ms ~ 182088ms (file: 203-260-206-102-103-310-103-103-124-230-623-102-620-260-230-236-260-103-230-105-310-132-326-310-132-103-315-576-672-261-260-230-231-123-231-320-231-236-257-210-130-326-362-216-1_W.mp3)
  Segment 47: 183088ms ~ 190900ms (file: 613-617-617-163-613-631-671-361-761-263-321-532-567-132-172-621-102-130-312-132-237-123-102-263-231-216-235-621-263-265-263-326-236-236-236-267-627-756-326-103-310-712-312-120-1_W.mp3)
  Segment 48: 191900ms ~ 192476ms (file: 706-716-715-175-105-170-710-710-137-751-710-170-175-170-710-175-751-715-175-173-715-175-170-715-750 756-601-167-170-710-716-173-170-107-715-170-715-175-175-173-175-713-175-175-1_V.mp3)
  Segment 49: 193476ms ~ 194052ms (file: 571-571-573-572-573-517-157-157-137-175-175-175 571-513-517-571-510-157-107-105-173-153-105-710-152-517-517-153-150-105-105-103-103-157-170-150-150-103-751-175-513 571-573-517-5_V.mp3)
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
> **Time**: 2026-04-06 08:29:27
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 2,
"transcript": "업무나 또는 뭐 사업이나 에이전트식 AI가 도움 없이는 아마 어떤 사회 활동 교육 활동 경제 활동",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 165,
"audio_type": "speech",
"words": [
{"word": "업무나", "start_ms": 165, "dur_ms": 376, "phase_lr_deg": 0.0},
{"word": "또는", "start_ms": 541, "dur_ms": 375, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 916, "dur_ms": 763, "phase_lr_deg": 0.0},
{"word": "사업이나", "start_ms": 1679, "dur_ms": 637, "phase_lr_deg": 0.0},
{"word": "에이전트식", "start_ms": 2316, "dur_ms": 375, "phase_lr_deg": 0.0},
{"word": "AI가", "start_ms": 2691, "dur_ms": 137, "phase_lr_deg": 0.0},
{"word": "도움", "start_ms": 2828, "dur_ms": 167, "phase_lr_deg": 0.0},
{"word": "없이는", "start_ms": 2995, "dur_ms": 573, "phase_lr_deg": 0.0},
{"word": "아마", "start_ms": 3568, "dur_ms": 371, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 3939, "dur_ms": 765, "phase_lr_deg": 0.0},
{"word": "사회", "start_ms": 4704, "dur_ms": 137, "phase_lr_deg": 0.0},
{"word": "활동", "start_ms": 4841, "dur_ms": 153, "phase_lr_deg": 0.0},
{"word": "교육", "start_ms": 4994, "dur_ms": 137, "phase_lr_deg": 0.0},
{"word": "활동", "start_ms": 5131, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "경제", "start_ms": 5231, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "활동", "start_ms": 5331, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "완성된 휴머노이드는 지금은 그렇게 힘이 없지만 나중에 완성된 휴머노이드가 나온다 그러면 엄청난 힘을 발휘할 겁니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 507,
"audio_type": "speech",
"words": [
{"word": "완성된", "start_ms": 507, "dur_ms": 261, "phase_lr_deg": 0.0},
{"word": "휴머노이드는", "start_ms": 768, "dur_ms": 607, "phase_lr_deg": 0.0},
{"word": "지금은", "start_ms": 1375, "dur_ms": 367, "phase_lr_deg": 0.0},
{"word": "그렇게", "start_ms": 1742, "dur_ms": 756, "phase_lr_deg": 0.0},
{"word": "힘이", "start_ms": 2498, "dur_ms": 276, "phase_lr_deg": 0.0},
{"word": "없지만", "start_ms": 2774, "dur_ms": 261, "phase_lr_deg": 0.0},
{"word": "나중에", "start_ms": 3035, "dur_ms": 263, "phase_lr_deg": 0.0},
{"word": "완성된", "start_ms": 3298, "dur_ms": 132, "phase_lr_deg": 0.0},
{"word": "휴머노이드가", "start_ms": 3430, "dur_ms": 312, "phase_lr_deg": 0.0},
{"word": "나온다", "start_ms": 3742, "dur_ms": 263, "phase_lr_deg": 0.0},
{"word": "그러면", "start_ms": 4005, "dur_ms": 325, "phase_lr_deg": 0.0},
{"word": "엄청난", "start_ms": 4330, "dur_ms": 576, "phase_lr_deg": 0.0},
{"word": "힘을", "start_ms": 4906, "dur_ms": 352, "phase_lr_deg": 0.0},
{"word": "발휘할", "start_ms": 5258, "dur_ms": 326, "phase_lr_deg": 0.0},
{"word": "겁니다", "start_ms": 5584, "dur_ms": 132, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "가격도 엄청 비쌀 거고요 그런 상황을 지금부터 염두에",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 652,
"audio_type": "speech",
"words": [
{"word": "가격도", "start_ms": 652, "dur_ms": 576, "phase_lr_deg": 0.0},
{"word": "엄청", "start_ms": 1228, "dur_ms": 263, "phase_lr_deg": 0.0},
{"word": "비쌀", "start_ms": 1491, "dur_ms": 126, "phase_lr_deg": 0.0},
{"word": "거고요", "start_ms": 1617, "dur_ms": 526, "phase_lr_deg": 0.0},
{"word": "그런", "start_ms": 2143, "dur_ms": 675, "phase_lr_deg": 0.0},
{"word": "상황을", "start_ms": 2818, "dur_ms": 362, "phase_lr_deg": 0.0},
{"word": "지금부터", "start_ms": 3180, "dur_ms": 231, "phase_lr_deg": 0.0},
{"word": "염두에", "start_ms": 3411, "dur_ms": 764, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "부담금을 납부하지 않고 있었는데 나중에는 그걸 식용유로 대신 주면 어떡하냐고 묘한 딜을 걸어왔고 또 그 와중에 인도네시아에서 파견한 기술자가 USB에 KF21 관련 기밀을 훔쳐",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 216,
"audio_type": "speech",
"words": [
{"word": "부담금을", "start_ms": 216, "dur_ms": 760, "phase_lr_deg": 0.0},
{"word": "납부하지", "start_ms": 976, "dur_ms": 760, "phase_lr_deg": 0.0},
{"word": "않고", "start_ms": 1736, "dur_ms": 672, "phase_lr_deg": 0.0},
{"word": "있었는데", "start_ms": 2408, "dur_ms": 203, "phase_lr_deg": 0.0},
{"word": "나중에는", "start_ms": 2611, "dur_ms": 201, "phase_lr_deg": 0.0},
{"word": "그걸", "start_ms": 2812, "dur_ms": 216, "phase_lr_deg": 0.0},
{"word": "식용유로", "start_ms": 3028, "dur_ms": 270, "phase_lr_deg": 0.0},
{"word": "대신", "start_ms": 3298, "dur_ms": 273, "phase_lr_deg": 0.0},
{"word": "주면", "start_ms": 3571, "dur_ms": 206, "phase_lr_deg": 0.0},
{"word": "어떡하냐고", "start_ms": 3777, "dur_ms": 201, "phase_lr_deg": 0.0},
{"word": "묘한", "start_ms": 3978, "dur_ms": 765, "phase_lr_deg": 0.0},
{"word": "딜을", "start_ms": 4743, "dur_ms": 301, "phase_lr_deg": 0.0},
{"word": "걸어왔고", "start_ms": 5044, "dur_ms": 136, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "먹었잖아 그치 그때도 그랬지 그때 그랬지 그렇지 짠 진짜 너무 많이 야 어때 근데",
"speaker": "speaker_4",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 257,
"audio_type": "speech",
"words": [
{"word": "먹었잖아", "start_ms": 257, "dur_ms": 752, "phase_lr_deg": 0.0},
{"word": "그치", "start_ms": 1009, "dur_ms": 123, "phase_lr_deg": 0.0},
{"word": "그때도", "start_ms": 1132, "dur_ms": 260, "phase_lr_deg": 0.0},
{"word": "그랬지", "start_ms": 1392, "dur_ms": 371, "phase_lr_deg": 0.0},
{"word": "그때", "start_ms": 1763, "dur_ms": 670, "phase_lr_deg": 0.0},
{"word": "그랬지", "start_ms": 2433, "dur_ms": 627, "phase_lr_deg": 0.0},
{"word": "그렇지", "start_ms": 3060, "dur_ms": 276, "phase_lr_deg": 0.0},
{"word": "짠", "start_ms": 3336, "dur_ms": 206, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "있습니다 이란의 최대 석유 수출 거점인 하르그섬에 유도 미사일을 추가 배치하고 해안선에 기뢰를 매설한 것으로 알려졌습니다 일각에서는 이란이 섬 지하",
"speaker": "speaker_5",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 132,
"audio_type": "speech",
"words": [
{"word": "있습니다", "start_ms": 132, "dur_ms": 273, "phase_lr_deg": 0.0},
{"word": "이란의", "start_ms": 405, "dur_ms": 267, "phase_lr_deg": 0.0},
{"word": "최대", "start_ms": 672, "dur_ms": 267, "phase_lr_deg": 0.0},
{"word": "석유", "start_ms": 939, "dur_ms": 267, "phase_lr_deg": 0.0},
{"word": "수출", "start_ms": 1206, "dur_ms": 103, "phase_lr_deg": 0.0},
{"word": "거점인", "start_ms": 1309, "dur_ms": 107, "phase_lr_deg": 0.0},
{"word": "하르그섬에", "start_ms": 1416, "dur_ms": 570, "phase_lr_deg": 0.0},
{"word": "유도", "start_ms": 1986, "dur_ms": 256, "phase_lr_deg": 0.0},
{"word": "미사일을", "start_ms": 2242, "dur_ms": 263, "phase_lr_deg": 0.0},
{"word": "추가", "start_ms": 2505, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "배치하고", "start_ms": 2635, "dur_ms": 162, "phase_lr_deg": 0.0}
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
"transcript": "소수 중에 강했던 종목 같은 경우는 시장이 하락을 멈추거나 약간만 반등하려고 하더라도 급등하는 경향이 있어서 오히려 시장",
"speaker": "speaker_6",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 263,
"audio_type": "speech",
"words": [
{"word": "소수", "start_ms": 263, "dur_ms": 236, "phase_lr_deg": 0.0},
{"word": "중에", "start_ms": 499, "dur_ms": 236, "phase_lr_deg": 0.0},
{"word": "강했던", "start_ms": 735, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "종목", "start_ms": 1055, "dur_ms": 321, "phase_lr_deg": 0.0},
{"word": "같은", "start_ms": 1376, "dur_ms": 231, "phase_lr_deg": 0.0},
{"word": "경우는", "start_ms": 1607, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "시장이", "start_ms": 1837, "dur_ms": 231, "phase_lr_deg": 0.0},
{"word": "하락을", "start_ms": 2068, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "멈추거나", "start_ms": 2298, "dur_ms": 213, "phase_lr_deg": 0.0},
{"word": "약간만", "start_ms": 2511, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "반등하려고", "start_ms": 2741, "dur_ms": 203, "phase_lr_deg": 0.0},
{"word": "하더라도", "start_ms": 2944, "dur_ms": 230, "phase_lr_deg": 0.0}
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
"transcript": "한 3년 동안 난방 제품을 찾아봤던 것 같아요 그래서 찾은 게 이제 탄소 난방인데 쉽게 얘기하면 필름이에요 전기 필름 음 어저께 얘기만 들어봤는데 그 우리",
"speaker": "speaker_7",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 207,
"audio_type": "speech",
"words": [
{"word": "한", "start_ms": 207, "dur_ms": 203, "phase_lr_deg": 0.0},
{"word": "3년", "start_ms": 410, "dur_ms": 127, "phase_lr_deg": 0.0},
{"word": "동안", "start_ms": 537, "dur_ms": 102, "phase_lr_deg": 0.0},
{"word": "난방", "start_ms": 639, "dur_ms": 630, "phase_lr_deg": 0.0},
{"word": "제품을", "start_ms": 1269, "dur_ms": 132, "phase_lr_deg": 0.0},
{"word": "찾아봤던", "start_ms": 1401, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 1531, "dur_ms": 126, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 1657, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 1777, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "찾은", "start_ms": 2097, "dur_ms": 203, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 2300, "dur_ms": 620, "phase_lr_deg": 0.0}
]
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
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 15,
"transcript": "한 3년 동안 난방 제품을 찾아봤던 것 같아요 그래서 찾은 게 이제 탄소 난방인데 쉽게 얘기하면 필름이에요 전기 필름 음 어저께 얘기만 들어봤는데 그 우리",
"speaker": "speaker_7",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 106,
"audio_type": "speech",
"words": [
{"word": "한", "start_ms": 106, "dur_ms": 267, "phase_lr_deg": 0.0},
{"word": "3년", "start_ms": 373, "dur_ms": 267, "phase_lr_deg": 0.0},
{"word": "동안", "start_ms": 640, "dur_ms": 273, "phase_lr_deg": 0.0},
{"word": "난방", "start_ms": 913, "dur_ms": 276, "phase_lr_deg": 0.0},
{"word": "제품을", "start_ms": 1189, "dur_ms": 276, "phase_lr_deg": 0.0},
{"word": "찾아봤던", "start_ms": 1465, "dur_ms": 237, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 1702, "dur_ms": 102, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 1804, "dur_ms": 367, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 2171, "dur_ms": 537, "phase_lr_deg": 0.0},
{"word": "찾은", "start_ms": 2708, "dur_ms": 167, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 2875, "dur_ms": 617, "phase_lr_deg": 0.0}
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
"transcript": "28500명 수준인 주한미군을 부풀리고 북한의 위협까지",
"speaker": "speaker_8",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 213,
"audio_type": "speech",
"words": [
{"word": "28500명", "start_ms": 213, "dur_ms": 524, "phase_lr_deg": 0.0},
{"word": "수준인", "start_ms": 737, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "주한미군을", "start_ms": 967, "dur_ms": 725, "phase_lr_deg": 0.0},
{"word": "부풀리고", "start_ms": 1692, "dur_ms": 273, "phase_lr_deg": 0.0},
{"word": "북한의", "start_ms": 1965, "dur_ms": 276, "phase_lr_deg": 0.0},
{"word": "위협까지", "start_ms": 2241, "dur_ms": 267, "phase_lr_deg": 0.0}
]
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
"transcript": "하기 위한 것으로 이 정부에 힘이 없다고 거부했습니다 스페인은 이란 공격용 미국 항공기의 영공 사용을 승인하지 않겠다고 했고 프랑스 역시 이스라",
"speaker": "speaker_9",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 570,
"audio_type": "speech",
"words": [
{"word": "하기", "start_ms": 570, "dur_ms": 362, "phase_lr_deg": 0.0},
{"word": "위한", "start_ms": 932, "dur_ms": 705, "phase_lr_deg": 0.0},
{"word": "것으로", "start_ms": 1637, "dur_ms": 261, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 1898, "dur_ms": 702, "phase_lr_deg": 0.0},
{"word": "정부에", "start_ms": 2600, "dur_ms": 672, "phase_lr_deg": 0.0},
{"word": "힘이", "start_ms": 3272, "dur_ms": 216, "phase_lr_deg": 0.0},
{"word": "없다고", "start_ms": 3488, "dur_ms": 265, "phase_lr_deg": 0.0},
{"word": "거부했습니다", "start_ms": 3753, "dur_ms": 627, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "지는 겁니다 수사권을 송두리째 뺏겼으니까 이 위기를 타개할 방책으로 특사경의 수사 지휘 권한을 관심 있게 들여다보고 있다 라는 취재를 통해서 이 문제를 제기했던 겁니다 그냥 제 머릿속에서 뇌피셜로 전해드렸던 게 아니라 직접 수사",
"speaker": "speaker_10",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 750,
"audio_type": "speech",
"words": [
{"word": "지는", "start_ms": 750, "dur_ms": 710, "phase_lr_deg": 0.0},
{"word": "겁니다", "start_ms": 1460, "dur_ms": 760, "phase_lr_deg": 0.0},
{"word": "수사권을", "start_ms": 2220, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "송두리째", "start_ms": 2970, "dur_ms": 751, "phase_lr_deg": 0.0},
{"word": "뺏겼으니까", "start_ms": 3721, "dur_ms": 573, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 4294, "dur_ms": 352, "phase_lr_deg": 0.0},
{"word": "위기를", "start_ms": 4646, "dur_ms": 375, "phase_lr_deg": 0.0},
{"word": "타개할", "start_ms": 5021, "dur_ms": 657, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "눈치 챘다고? 어 야 이게 뭐야 아 진짜 골고루 하느라 고생 많다",
"speaker": "speaker_11",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 751,
"audio_type": "speech",
"words": [
{"word": "눈치", "start_ms": 751, "dur_ms": 327, "phase_lr_deg": 0.0},
{"word": "챘다고?", "start_ms": 1078, "dur_ms": 273, "phase_lr_deg": 0.0},
{"word": "어", "start_ms": 1351, "dur_ms": 123, "phase_lr_deg": 0.0},
{"word": "야", "start_ms": 1474, "dur_ms": 275, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 1749, "dur_ms": 276, "phase_lr_deg": 0.0},
{"word": "뭐야", "start_ms": 2025, "dur_ms": 132, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 2157, "dur_ms": 326, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 2483, "dur_ms": 312, "phase_lr_deg": 0.0},
{"word": "골고루", "start_ms": 2795, "dur_ms": 123, "phase_lr_deg": 0.0},
{"word": "하느라", "start_ms": 2918, "dur_ms": 123, "phase_lr_deg": 0.0},
{"word": "고생", "start_ms": 3041, "dur_ms": 125, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "눈치 챘다고? 어 야 이게 뭐야 아 진짜 골고루 하느라 고생 많다",
"speaker": "speaker_11",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 721,
"audio_type": "speech",
"words": [
{"word": "눈치", "start_ms": 721, "dur_ms": 275, "phase_lr_deg": 0.0},
{"word": "챘다고?", "start_ms": 996, "dur_ms": 301, "phase_lr_deg": 0.0},
{"word": "어", "start_ms": 1297, "dur_ms": 102, "phase_lr_deg": 0.0},
{"word": "야", "start_ms": 1399, "dur_ms": 526, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 1925, "dur_ms": 301, "phase_lr_deg": 0.0},
{"word": "뭐야", "start_ms": 2226, "dur_ms": 123, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 2349, "dur_ms": 670, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 3019, "dur_ms": 327, "phase_lr_deg": 0.0},
{"word": "골고루", "start_ms": 3346, "dur_ms": 637, "phase_lr_deg": 0.0},
{"word": "하느라", "start_ms": 3983, "dur_ms": 567, "phase_lr_deg": 0.0},
{"word": "고생", "start_ms": 4550, "dur_ms": 651, "phase_lr_deg": 0.0}
]
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
"transcript": "28500명 수준인 주한미군을 부풀리고 북한의 위협까지",
"speaker": "speaker_8",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 103,
"audio_type": "speech",
"words": [
{"word": "28500명", "start_ms": 103, "dur_ms": 203, "phase_lr_deg": 0.0},
{"word": "수준인", "start_ms": 306, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "주한미군을", "start_ms": 516, "dur_ms": 132, "phase_lr_deg": 0.0},
{"word": "부풀리고", "start_ms": 648, "dur_ms": 357, "phase_lr_deg": 0.0},
{"word": "북한의", "start_ms": 1005, "dur_ms": 276, "phase_lr_deg": 0.0},
{"word": "위협까지", "start_ms": 1281, "dur_ms": 271, "phase_lr_deg": 0.0}
]
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
"transcript": "업무나 또는 뭐 사업이나 에이전트식 AI가 도움 없이는 아마 어떤 사회 활동 교육 활동 경제 활동",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 312,
"audio_type": "speech",
"words": [
{"word": "업무나", "start_ms": 312, "dur_ms": 326, "phase_lr_deg": 0.0},
{"word": "또는", "start_ms": 638, "dur_ms": 136, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 774, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "사업이나", "start_ms": 1004, "dur_ms": 231, "phase_lr_deg": 0.0},
{"word": "에이전트식", "start_ms": 1235, "dur_ms": 302, "phase_lr_deg": 0.0},
{"word": "AI가", "start_ms": 1537, "dur_ms": 301, "phase_lr_deg": 0.0},
{"word": "도움", "start_ms": 1838, "dur_ms": 231, "phase_lr_deg": 0.0},
{"word": "없이는", "start_ms": 2069, "dur_ms": 321, "phase_lr_deg": 0.0},
{"word": "아마", "start_ms": 2390, "dur_ms": 341, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 2731, "dur_ms": 345, "phase_lr_deg": 0.0},
{"word": "사회", "start_ms": 3076, "dur_ms": 316, "phase_lr_deg": 0.0},
{"word": "활동", "start_ms": 3392, "dur_ms": 326, "phase_lr_deg": 0.0},
{"word": "교육", "start_ms": 3718, "dur_ms": 312, "phase_lr_deg": 0.0},
{"word": "활동", "start_ms": 4030, "dur_ms": 317, "phase_lr_deg": 0.0},
{"word": "경제", "start_ms": 4347, "dur_ms": 316, "phase_lr_deg": 0.0},
{"word": "활동", "start_ms": 4663, "dur_ms": 304, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "완성된 휴머노이드는 지금은 그렇게 힘이 없지만 나중에 완성된 휴머노이드가 나온다 그러면 엄청난 힘을 발휘할 겁니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 236,
"audio_type": "speech",
"words": [
{"word": "완성된", "start_ms": 236, "dur_ms": 716, "phase_lr_deg": 0.0},
{"word": "휴머노이드는", "start_ms": 952, "dur_ms": 231, "phase_lr_deg": 0.0},
{"word": "지금은", "start_ms": 1183, "dur_ms": 236, "phase_lr_deg": 0.0},
{"word": "그렇게", "start_ms": 1419, "dur_ms": 213, "phase_lr_deg": 0.0},
{"word": "힘이", "start_ms": 1632, "dur_ms": 312, "phase_lr_deg": 0.0},
{"word": "없지만", "start_ms": 1944, "dur_ms": 123, "phase_lr_deg": 0.0},
{"word": "나중에", "start_ms": 2067, "dur_ms": 126, "phase_lr_deg": 0.0},
{"word": "완성된", "start_ms": 2193, "dur_ms": 102, "phase_lr_deg": 0.0},
{"word": "휴머노이드가", "start_ms": 2295, "dur_ms": 267, "phase_lr_deg": 0.0},
{"word": "나온다", "start_ms": 2562, "dur_ms": 123, "phase_lr_deg": 0.0},
{"word": "그러면", "start_ms": 2685, "dur_ms": 123, "phase_lr_deg": 0.0},
{"word": "엄청난", "start_ms": 2808, "dur_ms": 627, "phase_lr_deg": 0.0},
{"word": "힘을", "start_ms": 3435, "dur_ms": 627, "phase_lr_deg": 0.0},
{"word": "발휘할", "start_ms": 4062, "dur_ms": 263, "phase_lr_deg": 0.0},
{"word": "겁니다", "start_ms": 4325, "dur_ms": 236, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "부담금을 납부하지 않고 있었는데 나중에는 그걸 식용유로 대신 주면 어떡하냐고 묘한 딜을 걸어왔고 또 그 와중에 인도네시아에서 파견한 기술자가 USB에 KF21 관련 기밀을 훔쳐",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 316,
"audio_type": "speech",
"words": [
{"word": "부담금을", "start_ms": 316, "dur_ms": 102, "phase_lr_deg": 0.0},
{"word": "납부하지", "start_ms": 418, "dur_ms": 143, "phase_lr_deg": 0.0},
{"word": "않고", "start_ms": 561, "dur_ms": 203, "phase_lr_deg": 0.0},
{"word": "있었는데", "start_ms": 764, "dur_ms": 756, "phase_lr_deg": 0.0},
{"word": "나중에는", "start_ms": 1520, "dur_ms": 172, "phase_lr_deg": 0.0},
{"word": "그걸", "start_ms": 1692, "dur_ms": 173, "phase_lr_deg": 0.0},
{"word": "식용유로", "start_ms": 1865, "dur_ms": 132, "phase_lr_deg": 0.0},
{"word": "대신", "start_ms": 1997, "dur_ms": 132, "phase_lr_deg": 0.0},
{"word": "주면", "start_ms": 2129, "dur_ms": 307, "phase_lr_deg": 0.0},
{"word": "어떡하냐고", "start_ms": 2436, "dur_ms": 301, "phase_lr_deg": 0.0},
{"word": "묘한", "start_ms": 2737, "dur_ms": 302, "phase_lr_deg": 0.0},
{"word": "딜을", "start_ms": 3039, "dur_ms": 637, "phase_lr_deg": 0.0},
{"word": "걸어왔고", "start_ms": 3676, "dur_ms": 261, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "먹었잖아 그치 그때도 그랬지 그때 그랬지 그렇지 짠 진짜 너무 많이 야 어때 근데",
"speaker": "speaker_4",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 320,
"audio_type": "speech",
"words": [
{"word": "먹었잖아", "start_ms": 320, "dur_ms": 267, "phase_lr_deg": 0.0},
{"word": "그치", "start_ms": 587, "dur_ms": 267, "phase_lr_deg": 0.0},
{"word": "그때도", "start_ms": 854, "dur_ms": 267, "phase_lr_deg": 0.0},
{"word": "그랬지", "start_ms": 1121, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "그때", "start_ms": 1441, "dur_ms": 270, "phase_lr_deg": 0.0},
{"word": "그랬지", "start_ms": 1711, "dur_ms": 735, "phase_lr_deg": 0.0},
{"word": "그렇지", "start_ms": 2446, "dur_ms": 173, "phase_lr_deg": 0.0},
{"word": "짠", "start_ms": 2619, "dur_ms": 754, "phase_lr_deg": 0.0}
]
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
"transcript": "있습니다 이란의 최대 석유 수출 거점인 하르그섬에 유도 미사일을 추가 배치하고 해안선에 기뢰를 매설한 것으로 알려졌습니다 일각에서는 이란이 섬 지하",
"speaker": "speaker_5",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 527,
"audio_type": "speech",
"words": [
{"word": "있습니다", "start_ms": 527, "dur_ms": 103, "phase_lr_deg": 0.0},
{"word": "이란의", "start_ms": 630, "dur_ms": 627, "phase_lr_deg": 0.0},
{"word": "최대", "start_ms": 1257, "dur_ms": 627, "phase_lr_deg": 0.0},
{"word": "석유", "start_ms": 1884, "dur_ms": 213, "phase_lr_deg": 0.0},
{"word": "수출", "start_ms": 2097, "dur_ms": 263, "phase_lr_deg": 0.0},
{"word": "거점인", "start_ms": 2360, "dur_ms": 213, "phase_lr_deg": 0.0},
{"word": "하르그섬에", "start_ms": 2573, "dur_ms": 217, "phase_lr_deg": 0.0},
{"word": "유도", "start_ms": 2790, "dur_ms": 762, "phase_lr_deg": 0.0},
{"word": "미사일을", "start_ms": 3552, "dur_ms": 672, "phase_lr_deg": 0.0},
{"word": "추가", "start_ms": 4224, "dur_ms": 612, "phase_lr_deg": 0.0},
{"word": "배치하고", "start_ms": 4836, "dur_ms": 130, "phase_lr_deg": 0.0}
]
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
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 08:29:27*
