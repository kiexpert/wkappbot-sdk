# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=28596  time=08:31:32  uptime=16775s  mem=451MB  system_processes=407"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_083119.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 756 571-571-571-751-571-571-570-517-571-751-576-574-574-574-574-574 574-570-570-517-574-574-574-574-574-571-571-157-574-574-574-574-574-571-517 571-570-571-517-517-571-517-517-5_W.mp3)
  Segment 2: 1576ms ~ 8272ms (file: 103-157-321-320-263-527-265-627-351-103-316-123-514-301-675-103-123-326-217-321-617-725-175-310-163-107-317-310-125-106-130-106-130-167-156-357-750-321-103-157-137-317-301-137-1_W.mp3)
  Segment 3: 9272ms ~ 9848ms (file: 702-157-713 571-571-571-574-570-571-570-576-571-571-751-517-157-157-150-103-571-523-753-751-157-710-173-317-215-137 573-571-572-571-150-170-157-571-571-150-713-576-103-710-725-5_W.mp3)
  Segment 4: 10848ms ~ 17904ms (file: 316-310-132-137-135-156-107-103-321-527-413-752-321-312-301-256-125-120-123-265-312-206-126-302-130-675-263-310-103-301-237-325-567-657-501-573-765-217-321-236-237-321-201-267-2_W.mp3)
  Segment 5: 18904ms ~ 29992ms (file: 763-210-752-725-756-276-260-216-267-276-753-176-132-132-213-310-310-732-765-203-765-132-672-267-754-301-310-123-301-132-130-203-723-201-206-130-271-201-765-312-167-132-301-752-7_W.mp3)
  Segment 6: 30992ms ~ 31568ms (file: 573 576-576-574-574-574-574-574-574-576-574-574-570-576 570-574 574-576-571-574-574-574-576-570-570-570-570-570-570-574-576-571 571-574-574-574-574-574-574-574-570-574-574 570-5_W.mp3)
  Segment 7: 32568ms ~ 33144ms (file: 152-156-175-135-315-135-312-132-136-135-130-103-301-315-135-315-103-103-150-135-130-310-130-105-153-157-150-150-516-157-156-517-157-157-517-157-157-156-167-156-130-135-130-130-1_V.mp3)
  Segment 8: 34144ms ~ 34720ms (file: 627-751-621-136-576-657-236-753-362-316-561-615-750 756-326-632-267-312-507-325-567-503-206-231-621-123-672-621-601-602-675-267-762-326-672-602-576-756-527-562-507-576-627-267-1_V.mp3)
  Segment 9: 35720ms ~ 36296ms (file: 126-621-175-316-213-731-271-127-154-167-213-203-206-627-675-567-637-310-316-315-130-315-312-576-736-675-671-163-627-367_V.mp3)
  Segment 10: 37296ms ~ 45288ms (file: 127-157-271-371-217-236-301-231-326-267-127-201-102-321-320-103-261-756-206-103-106-103-236-102-136-132-756-201-261-203-102-103-120-230-126-263-236-321-231-107-620-726-231-130-2_W.mp3)
  Segment 11: 46288ms ~ 46864ms (file: 571-157-571-573-571-351-157-571-517-175-571-153-571-175-517-715-157-517-537-517-572-517-751 571-571-517-157-156-517-573-571-157-751-571-157-175-517-157-157-175-175-517-571-157-5_V.mp3)
  Segment 12: 47864ms ~ 48440ms (file: 621-612-671-165-617-617-675-276-765-765-362-612-657-175-675-675-672-267-726-176-726-261-126-326-721-756-153-217-352-326-271-672-765-765-762-675-627-362-762-267-127-162-136-126-7_V.mp3)
  Segment 13: 49440ms ~ 50016ms (file: 672-361-316-163-107-106-673-326-623-632-367-673-376-632-367-637-367-736-763-376-736-367-736-376-132 751-231-326-263-623-627-312-351-167-371-521-312-752-457-213_W.mp3)
  Segment 14: 51016ms ~ 51592ms (file: 213-203-263-261-762-267-237-657-671-136-361-760-267 162-316-315-130-321 132-321-573-715-765-672-132-275-237-235-725-617-265-621-136-361-351-163-137-316-163-136-137-257-275-136-7_W.mp3)
  Segment 15: 52592ms ~ 53168ms (file: 150-153-510-517-517-517 517-517-570-153-510-513-510-571-153-517-517 510-571-157-507-753-510-150-510-150-153-517-510-510-517-570-571-573 153-531-153-517-510-501-517-501-157-517-5_W.mp3)
  Segment 16: 54168ms ~ 54744ms (file: 574-571-576-571-573 571-571-570-571-571-750-571-517-756 576-574-574-571-576-571-571-571-157-571-157-570-573-571 157-571-574-574-574-574-571-570-571-574-576-756 574-573-574-574-5_W.mp3)
  Segment 17: 55744ms ~ 64492ms (file: 310-673-576-351-753-375-753-361-137-572-572-652-276-732-715-301-136-236-172-103-721-132-312-130-632-731-523-627-756-103-106-310-567-241-574-130-301-320-231-312-321-130-750-137-1_W.mp3)
  Segment 18: 65492ms ~ 66068ms (file: 572-136-235-725-762-256-213-267-312-207-276-136-651-132-570-362-507-372-652-507-576-276-267-506-163-725-326-123-372-371-275-570-756-725-752-327-563-576 236-162-526-705-257-576-2_V.mp3)
  Segment 19: 67068ms ~ 76176ms (file: 361-375-321-321-316-362-201-721-102-230-321-236-231-321-756-120-213-132-132-612-123-527-132-271-326-327-372-362-237-725-572-237-276-231-321-321-236-260-320-103-756-310-132-132-5_W.mp3)
  Segment 20: 77176ms ~ 77752ms (file: 751-750-750-756-765-756-756-756-576-765-675-675-657-675-567-756-576-756-576-573-576-576-756-576-576-576-756-576-576-756-567-756-571-657-265-576-752-316-316-361-136-316-316-312-5_W.mp3)
  Segment 21: 78752ms ~ 79328ms (file: 130-751-751-710-103-130-317-317-361 573-175-137-731-317-753-175-576-175-713-756-760-730-716-710-670-601-312-267-326-705-157-176-571-763-723-361-270-263-657-236-276-267-326-267-2_W.mp3)
  Segment 22: 80328ms ~ 88104ms (file: 132-167-126-137-312-321-310-130-265-260-312-301-172-325-132-230-207-327-270-730-725-723-675-706-756-750-756-750-756-602-620-236-320-732-573-273-263-236-753-312-213-623-761-627-2_W.mp3)
  Segment 23: 89104ms ~ 97384ms (file: 267-372-263-210-627-263-126-236-231-236-231-102-143-236-637-321-316-765-627-627-765-670-602-162-106-152-625-103-103-103-231-123-216-326-137-167-263-263-320-316-312-164-263-326-3_W.mp3)
  Segment 24: 98384ms ~ 98960ms (file: 710-701-701-715-750-710-170-705-705-710-701-715-705-701-710-715-701-710-701-701-705-705-751 175-716-705-175-761-107-170-701-705-701-705-710-710-170-175-715-715-751-715-715-715-1_V.mp3)
  Segment 25: 99960ms ~ 100536ms (file: 157-731-175-571-175-570-751-753-570-750-751-715-517-573-153-715 571-571-510-537-517-507-175-130-715-137-715-750-157-750-715-750-750-715-571-150-537-570 175-170-170-751-170-750-1_V.mp3)
  Segment 26: 101536ms ~ 107980ms (file: 327-310-267-267-107-310-176-167-576-103-762-672-627-267-130-236-327-237-132-106-106-617-276-107-107-170-751-273-320-176-751-301-371-136-765-236-571-106-273-327-167-671-576-103-3_W.mp3)
  Segment 27: 108980ms ~ 116360ms (file: 263-160-326-763-567-372-237-675-623-160-762-276-276-237-632-601-267-610-120-203-103-327-216-106-617-312-163-756-765-267-276-371-132-267-236-236-237-237-132-327-176-765-267-213-2_W.mp3)
  Segment 28: 117360ms ~ 124308ms (file: 163-172-137-106-130-316-751-136-103-237-237-103-175-107-612-617-371-321-627-267-236-123-123-170-637-132-163-163-631-237-267-173-573-175-310-160-175-176-163-132-675-306-163-173-1_W.mp3)
  Segment 29: 125308ms ~ 125884ms (file: 753-527-756-756-752-732-751-571-576-753-571-756-576-572-567-756-576-756-576-572 751-576-573-567-576-576 570-570-571 574-574-574-574-574-574-574-574-571-571-157 573-571-574-574-5_W.mp3)
  Segment 30: 126884ms ~ 127460ms (file: 624-237 265-327-361-235 652-241-321-231-754-541-326-673-357-154-365-145-312-132-612-627-751-632-123-631-123-245-234-741-325-731-251-647-173-153-127-173-253-246-256-326-154-132-3_V.mp3)
  Segment 31: 128460ms ~ 129036ms (file: 175-173-573-571-571-751 576-517-570-135-750-175-157-715-751-157-573-517-715-750-751-571-517-753-715-576 574-574-576-571-570-753-573-571-751-753-157-570-751-571-571-751-573-576-5_W.mp3)
  Segment 32: 130036ms ~ 130612ms (file: 137-173-716-761-713-763-673-763-371-672-613-130-310-132-130-103-130-312-137-326-123-623-624_W.mp3)
  Segment 33: 131612ms ~ 132188ms (file: 571-517-571-573 574-574-571-574-517-517-157-517-350-573-537-137-571-517-751-571-751 570-571-517-751-751-175-571-571-570-571-157-573-150-570-537-175-157 570-517-571-173-513-157-5_V.mp3)
  Segment 34: 133188ms ~ 138948ms (file: 361-361-672-127-762-276-627-675-276-236-216-201-236-267-263-267-721-756-672-267-372-213-160-627-627-703-361-563-236-623-672-527-762-276-671-276-267-612-206-267-263-263-231-756-2_W.mp3)
  Segment 35: 139948ms ~ 146320ms (file: 725-603-607-627-726-267-103-132-312-276-267-260-321-712-573-125-123-126-765-213-172-721-217-712-172-257-136-517-207-372-123-271-276-210-231-752-726-207-627-270-206-715-175-270-7_W.mp3)
  Segment 36: 147320ms ~ 147896ms (file: 576-157-150-156-157-615-571-156-105-175-756-157-517-715-751-765-165-175-751-705-107-175-356-156-157-517-571-567-657-567-567-675-675-567-576-576-675-157-571-576-756-756-576-576-5_W.mp3)
  Segment 37: 148896ms ~ 149472ms (file: 216-532-672-657-215-576-125-753-130-236-237-317-715-756-125-125-172-312-705-751-602-625-725-612-752-137-237-657-751-271-621-261-576-735-750-756-751-517-261-267-701-123-137-321-7_V.mp3)
  Segment 38: 150472ms ~ 156916ms (file: 312-370-617-317-617-107-710-106-103-176-106-267-321-326-310-106-107-320-263-765-756-725-750-175-715-175-571-726-276-275-170-107-176-167-310-320-736-376-756-175-106-237-267-132-7_W.mp3)
  Segment 39: 157916ms ~ 158492ms (file: 137-576-571-316-137-376-765-263-672-231-267-237-236-265-231-567-507-675-607-352-527-236-176-107-357-312 723-123-503-312-507-132-231-723-527-273-756-125-167-631-271-723-623-321-2_V.mp3)
  Segment 40: 159492ms ~ 169212ms (file: 715-237-103-103-201-321-672-231-103-132-103-203-320-302-754-102-230-574-203-230-203-320-230-203-236-756-720-213-750-301-756-756-137-206-657-657-237-217-201-230-203-106-753-754-1_W.mp3)
  Segment 41: 170212ms ~ 170788ms (file: 312-756-276-276-362-253-260-321-236-574-523-275-531-267-567-547-675-530-312-534-570 237-527-127-732-325-215-572-326-235-705-753-327-326-753-576-750-375-750-273-327-752-362-523-2_V.mp3)
  Segment 42: 171788ms ~ 178772ms (file: 236-206-760-265-231-756-263-627-627-627-265-320-103-752-276-267-752-725-754-207-216-602-267-263-230-263-753-576-526-206-735-270-230-230-316-230-230-263-320-321-672-236-310-236-3_W_stt_the_interest_is_your_commitment_to_peace.mp3)
  Segment 43: 179772ms ~ 180348ms (file: 160-620-216-216-126-261-107-123-153-123-321-132-312-312-273-261-751-725-123-321-513-572-126-621-261-267-567-623-265-625-216-720-712-620-251-362-507-623-726-217-126-260-275-726-5_V.mp3)
  Segment 44: 181348ms ~ 181924ms (file: 132-126-132-136-126-312-231-132-321-371-715-753-675-637-375-567-135-375-173-130-731-753-567-563-756-756-567-567-567-576-765-657-365-670-675-574-547-567-576-675-132-623-136-316-5_W.mp3)
  Segment 45: 182924ms ~ 191744ms (file: 310-130-132-127-562-521-135-621-265-527-215-273-321-107-312-231-126-236-236-236-236-206-216-236-216-735-216-124-230-214-263-627-267-527-517-153-231-237-236-321-231-321-103-574-1_W.mp3)
  Segment 46: 192744ms ~ 193320ms (file: 675-675-657-651-631-167-617-167-673-617-671-671-617-675-675-765-675-675-765-765-765-765-675-765-675-765-765-675-765-675-675-675-675-765-675-576-657-651-651-652-657-657-615-657-1_W.mp3)
  Segment 47: 194320ms ~ 194896ms (file: 574-547-574-547-574-574-574-574_W.mp3)
  Segment 48: 195896ms ~ 196472ms (file: 517-571-571-571-574-574-574-574-571-574-571-571-570-570-570-571-571-574-576 571-571-574-574-574-571-517-571-570-571-571-371-157-507-571-573-571-571-517 574-517-571-570-571-517-1_W.mp3)
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
> **Time**: 2026-04-06 08:32:50
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
"transcript": "But it's hard, right? 소리 좀 들어봐. 와 진짜 맛있겠다.",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "mixed",
"silence_before_ms": 103,
"audio_type": "speech",
"words": [
{"word": "But", "start_ms": 103, "dur_ms": 157, "phase_lr_deg": 0.0},
{"word": "it's", "start_ms": 260, "dur_ms": 321, "phase_lr_deg": 0.0},
{"word": "hard,", "start_ms": 581, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "right?", "start_ms": 901, "dur_ms": 263, "phase_lr_deg": 0.0},
{"word": "소리", "start_ms": 1691, "dur_ms": 527, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 2218, "dur_ms": 265, "phase_lr_deg": 0.0},
{"word": "들어봐.", "start_ms": 2483, "dur_ms": 627, "phase_lr_deg": 0.0},
{"word": "와", "start_ms": 3461, "dur_ms": 351, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 3812, "dur_ms": 103, "phase_lr_deg": 0.0},
{"word": "맛있겠다.", "start_ms": 3915, "dur_ms": 316, "phase_lr_deg": 0.0}
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
"transcript": "하는 부분들 패키징에서 뭔가 좀 지원을 하는 부분들이 있었고 지표도 기본적으로 메모리 로컬리티가 유니파이드해서 굉장히 좋은 편이라서 이번에 이런 연산 연산 같은 트랜스포머의 KV 캐시",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 316,
"audio_type": "speech",
"words": [
{"word": "하는", "start_ms": 316, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "부분들", "start_ms": 626, "dur_ms": 132, "phase_lr_deg": 0.0},
{"word": "패키징에서", "start_ms": 758, "dur_ms": 137, "phase_lr_deg": 0.0},
{"word": "뭔가", "start_ms": 895, "dur_ms": 135, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 1030, "dur_ms": 156, "phase_lr_deg": 0.0},
{"word": "지원을", "start_ms": 1186, "dur_ms": 107, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 1293, "dur_ms": 103, "phase_lr_deg": 0.0},
{"word": "부분들이", "start_ms": 1396, "dur_ms": 321, "phase_lr_deg": 0.0},
{"word": "있었고", "start_ms": 1717, "dur_ms": 527, "phase_lr_deg": 0.0},
{"word": "지표도", "start_ms": 2244, "dur_ms": 413, "phase_lr_deg": 0.0},
{"word": "기본적으로", "start_ms": 2657, "dur_ms": 752, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "음~ 아 근데 진짜 맛있어. 맛있지? 이거 진짜 맛있는데? 나 맛집이다 맛집이다. 근데 진짜 추천 추천. 아 이것도 1,000원이야? 이것도 1,000원이야?",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 763,
"audio_type": "speech",
"words": [
{"word": "음~", "start_ms": 763, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 973, "dur_ms": 752, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 1725, "dur_ms": 725, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 2450, "dur_ms": 756, "phase_lr_deg": 0.0},
{"word": "맛있어.", "start_ms": 3206, "dur_ms": 276, "phase_lr_deg": 0.0},
{"word": "맛있지?", "start_ms": 3482, "dur_ms": 260, "phase_lr_deg": 0.0},
{"word": "이거", "start_ms": 3742, "dur_ms": 216, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 3958, "dur_ms": 267, "phase_lr_deg": 0.0},
{"word": "맛있는데?", "start_ms": 4225, "dur_ms": 276, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "떡갈비도 유명하고 뭐 양동시장의 통닭도 유명하고 유명한 게 되게 많아요. 우리 오늘은 양동시장 갈 건데. 아 바로 그 말이 나왔네요.",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 627,
"audio_type": "speech",
"words": [
{"word": "떡갈비도", "start_ms": 627, "dur_ms": 751, "phase_lr_deg": 0.0},
{"word": "유명하고", "start_ms": 1378, "dur_ms": 621, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 1999, "dur_ms": 136, "phase_lr_deg": 0.0},
{"word": "양동시장의", "start_ms": 2135, "dur_ms": 576, "phase_lr_deg": 0.0},
{"word": "통닭도", "start_ms": 2711, "dur_ms": 657, "phase_lr_deg": 0.0},
{"word": "유명하고", "start_ms": 3368, "dur_ms": 236, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 10,
"transcript": "활용하고 있는 셈입니다. 다시 돌아보면 유럽을 휩쓴 K2 전차와 K9 자주포, 중동을 놀라게 한 천궁 방공 미사일, 그리고 동남아 진출을 눈앞에 둔 KF-21 전투기까지 한국 군수 산업이 전 세계에서 동시에 공세를 펼치며 수천억 규모의 주문을 따낼 수 있었던 이유는 앞서",
"speaker": "speaker_5",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 127,
"audio_type": "speech",
"words": [
{"word": "활용하고", "start_ms": 127, "dur_ms": 157, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 284, "dur_ms": 271, "phase_lr_deg": 0.0},
{"word": "셈입니다.", "start_ms": 555, "dur_ms": 371, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 926, "dur_ms": 217, "phase_lr_deg": 0.0},
{"word": "돌아보면", "start_ms": 1143, "dur_ms": 236, "phase_lr_deg": 0.0}
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
"transcript": "다음 날도 상한가 그리고 또 그 다음 날에 24%까지 상승하면서 한 달쯤 전에 광동제약 대장주로 상승했고 기도 없었",
"speaker": "speaker_6",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 621,
"audio_type": "speech",
"words": [
{"word": "다음", "start_ms": 621, "dur_ms": 612, "phase_lr_deg": 0.0},
{"word": "날도", "start_ms": 1233, "dur_ms": 671, "phase_lr_deg": 0.0},
{"word": "상한가", "start_ms": 1904, "dur_ms": 165, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 2069, "dur_ms": 617, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 2686, "dur_ms": 617, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 3303, "dur_ms": 675, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "지 에이전트가 알아서 처리한다.",
"speaker": "speaker_7",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 672,
"audio_type": "speech",
"words": [
{"word": "지", "start_ms": 672, "dur_ms": 361, "phase_lr_deg": 0.0},
{"word": "에이전트가", "start_ms": 1033, "dur_ms": 316, "phase_lr_deg": 0.0},
{"word": "알아서", "start_ms": 1349, "dur_ms": 163, "phase_lr_deg": 0.0},
{"word": "처리한다.", "start_ms": 1512, "dur_ms": 107, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "What makes us humans humans, right? That's that's my biggest worry is it's not so much a worry, I think we will figure it out but it creates discomfort in the process as we re-defining",
"speaker": "speaker_8",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 213,
"audio_type": "speech",
"words": [
{"word": "What", "start_ms": 213, "dur_ms": 203, "phase_lr_deg": 0.0},
{"word": "makes", "start_ms": 416, "dur_ms": 263, "phase_lr_deg": 0.0},
{"word": "us", "start_ms": 679, "dur_ms": 261, "phase_lr_deg": 0.0},
{"word": "humans", "start_ms": 940, "dur_ms": 762, "phase_lr_deg": 0.0}
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
"transcript": "미국과의 안보 관계 사이에서 절묘한 줄타기를 하고 있습니다. 이런 상황에서 대만 문제는 부차적인 외교 사안일 수밖에 없습니다. 결국 이러한 국제 정세를 무시하고 대만 정부가 강경 대응을 반복",
"speaker": "speaker_9",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 310,
"audio_type": "speech",
"words": [
{"word": "미국과의", "start_ms": 310, "dur_ms": 673, "phase_lr_deg": 0.0},
{"word": "안보", "start_ms": 983, "dur_ms": 576, "phase_lr_deg": 0.0},
{"word": "관계", "start_ms": 1559, "dur_ms": 351, "phase_lr_deg": 0.0},
{"word": "사이에서", "start_ms": 1910, "dur_ms": 753, "phase_lr_deg": 0.0},
{"word": "절묘한", "start_ms": 2663, "dur_ms": 375, "phase_lr_deg": 0.0},
{"word": "줄타기를", "start_ms": 3038, "dur_ms": 753, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "어 진짜 맛있어! 맞아 이거 진짜... 아 닭발! 닭발 맞아! 그렇지 닭발! 닭발 진짜 서울에서 안 나오는데... 맞아 여기 진짜 통닭 해가지고 진짜 다 나와. 여기 닭발 튀김도 나와요. 어~ 엄청 많다. 어~ 맞아 맞아. 여기 1번 통닭이거든요.",
"speaker": "speaker_3",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 572,
"audio_type": "speech",
"words": [
{"word": "어", "start_ms": 572, "dur_ms": 136, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 708, "dur_ms": 235, "phase_lr_deg": 0.0},
{"word": "맛있어!", "start_ms": 943, "dur_ms": 725, "phase_lr_deg": 0.0},
{"word": "맞아", "start_ms": 1668, "dur_ms": 762, "phase_lr_deg": 0.0},
{"word": "이거", "start_ms": 2430, "dur_ms": 256, "phase_lr_deg": 0.0},
{"word": "진짜...", "start_ms": 2686, "dur_ms": 213, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "직접 했고요 집 짓기에 필요한 자격증도 미리 준비했다지요. 포크레인 하루 빌리면 30~40만 원입니다. 직접 학원에 등록을 해서 교육을 받고 제가",
"speaker": "speaker_10",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 361,
"audio_type": "speech",
"words": [
{"word": "직접", "start_ms": 361, "dur_ms": 375, "phase_lr_deg": 0.0},
{"word": "했고요", "start_ms": 736, "dur_ms": 321, "phase_lr_deg": 0.0},
{"word": "집", "start_ms": 1057, "dur_ms": 321, "phase_lr_deg": 0.0},
{"word": "짓기에", "start_ms": 1378, "dur_ms": 316, "phase_lr_deg": 0.0},
{"word": "필요한", "start_ms": 1694, "dur_ms": 362, "phase_lr_deg": 0.0},
{"word": "자격증도", "start_ms": 2056, "dur_ms": 201, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "어그로는 저리 가라라고 할 정도로 지저분하고 조잡한 타블로이드성 헤드라인만 뽑아내는데 특히나 국내 축구 기사는 클릭도 하기 싫을 정도로 혐오스럽습",
"speaker": "speaker_11",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 216,
"audio_type": "speech",
"words": [
{"word": "어그로는", "start_ms": 216, "dur_ms": 532, "phase_lr_deg": 0.0},
{"word": "저리", "start_ms": 748, "dur_ms": 672, "phase_lr_deg": 0.0},
{"word": "가라라고", "start_ms": 1420, "dur_ms": 657, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 2077, "dur_ms": 215, "phase_lr_deg": 0.0},
{"word": "정도로", "start_ms": 2292, "dur_ms": 576, "phase_lr_deg": 0.0}
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
"transcript": "했었던 거죠. 그게 현대차였다는 겁니다. 구글 소프트뱅크가 차례로 인수했다가 다시 매각한 것도 이런 문제와 무관하지가 않은데 그런데 최근에는 약간 바뀌고 있죠. 현대차가 아틀라스를 일정 궤도에 올",
"speaker": "speaker_12",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 132,
"audio_type": "speech",
"words": [
{"word": "했었던", "start_ms": 132, "dur_ms": 167, "phase_lr_deg": 0.0},
{"word": "거죠.", "start_ms": 299, "dur_ms": 126, "phase_lr_deg": 0.0},
{"word": "그게", "start_ms": 425, "dur_ms": 137, "phase_lr_deg": 0.0},
{"word": "현대차였다는", "start_ms": 562, "dur_ms": 312, "phase_lr_deg": 0.0},
{"word": "겁니다.", "start_ms": 874, "dur_ms": 321, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "억 원을 치면 구매했고 그 뒤를 이어 2024년 9월에는 이라크도 28억 달러를 내놓고 한 번에 천궁-II 8개 포대를 사들였습니다. 한국의 방공 미사일은 이제 중동 국가들이 미국에 대한 극단적인 의존에서 벗어나는 데 가장 적합한 대안이 된 셈입니다. 한국은",
"speaker": "speaker_5",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 267,
"audio_type": "speech",
"words": [
{"word": "억", "start_ms": 267, "dur_ms": 372, "phase_lr_deg": 0.0},
{"word": "원을", "start_ms": 639, "dur_ms": 263, "phase_lr_deg": 0.0},
{"word": "치면", "start_ms": 902, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "구매했고", "start_ms": 1112, "dur_ms": 627, "phase_lr_deg": 0.0}
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
"transcript": "봐야 하는 기사는 이후 20분에는 손흥민으로부터 완벽한 패스가 부황가의 아름다운 칩 슛으로 연결되었다는 것이었는데요 그런데 그렇게 리드를 늘린",
"speaker": "speaker_13",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 327,
"audio_type": "speech",
"words": [
{"word": "봐야", "start_ms": 327, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 637, "dur_ms": 267, "phase_lr_deg": 0.0},
{"word": "기사는", "start_ms": 904, "dur_ms": 267, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "판단되고 있다는 점입니다. 이와 관련해 홍콩의 유력 매체인 사우스차이나모닝포스트는 대만 정부가 자국의 외교 현실을 간과한 채 한국을 상대로 상식 밖",
"speaker": "speaker_14",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 263,
"audio_type": "speech",
"words": [
{"word": "판단되고", "start_ms": 263, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "있다는", "start_ms": 423, "dur_ms": 326, "phase_lr_deg": 0.0},
{"word": "점입니다.", "start_ms": 749, "dur_ms": 763, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "어 해야지 해야지! 아이고 뭐야 결혼한 거 맞아? 아 방금 비즈니스 같다.",
"speaker": "speaker_3",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 163,
"audio_type": "speech",
"words": [
{"word": "어", "start_ms": 163, "dur_ms": 172, "phase_lr_deg": 0.0},
{"word": "해야지", "start_ms": 335, "dur_ms": 137, "phase_lr_deg": 0.0},
{"word": "해야지!", "start_ms": 472, "dur_ms": 106, "phase_lr_deg": 0.0}
]
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
"transcript": "에서 다음 날인 D+2일차 아침에 갭 상승했을 때 이렇게 수익 실현해서 추가로 1억 이상 수익을",
"speaker": "speaker_6",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 137,
"audio_type": "speech",
"words": [
{"word": "에서", "start_ms": 137, "dur_ms": 173, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 310, "dur_ms": 716, "phase_lr_deg": 0.0},
{"word": "날인", "start_ms": 1026, "dur_ms": 761, "phase_lr_deg": 0.0}
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
"transcript": "언트의 등장은 금융 시장에도 엄청난 파장을 몰고 왔다. 소프트웨어 주가가 일제히 떨어지",
"speaker": "speaker_15",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 361,
"audio_type": "speech",
"words": [
{"word": "언트의", "start_ms": 361, "dur_ms": 361, "phase_lr_deg": 0.0},
{"word": "등장은", "start_ms": 722, "dur_ms": 672, "phase_lr_deg": 0.0},
{"word": "금융", "start_ms": 1394, "dur_ms": 127, "phase_lr_deg": 0.0},
{"word": "시장에도", "start_ms": 1521, "dur_ms": 762, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "습니다. 그뿐만 아니라 완전히 한국식으로 꾸며진 막걸리 집까지 우후죽순으로 생기며 한국 사람들이 즐기는 메뉴를 거의 실시간으로 같이 즐기는 문화까",
"speaker": "speaker_16",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 725,
"audio_type": "speech",
"words": [
{"word": "습니다.", "start_ms": 725, "dur_ms": 603, "phase_lr_deg": 0.0},
{"word": "그뿐만", "start_ms": 1328, "dur_ms": 607, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 1935, "dur_ms": 627, "phase_lr_deg": 0.0},
{"word": "완전히", "start_ms": 2562, "dur_ms": 726, "phase_lr_deg": 0.0}
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
"transcript": "보여드리면 15% 갭 상승했을 때 시가의 절반은 수익 실현해 줬고 나머지",
"speaker": "speaker_6",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 216,
"audio_type": "speech",
"words": [
{"word": "보여드리면", "start_ms": 216, "dur_ms": 532, "phase_lr_deg": 0.0},
{"word": "15%", "start_ms": 748, "dur_ms": 672, "phase_lr_deg": 0.0},
{"word": "갭", "start_ms": 1420, "dur_ms": 657, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "농지 투기 근절 의지를 더욱 분명히 했습니다. 네, 점프 대박입니다. 관련해 이 특사경 관련 문제를 최초 보도해 해결해서 검찰",
"speaker": "speaker_17",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 312,
"audio_type": "speech",
"words": [
{"word": "농지", "start_ms": 312, "dur_ms": 756, "phase_lr_deg": 0.0},
{"word": "투기", "start_ms": 1068, "dur_ms": 276, "phase_lr_deg": 0.0},
{"word": "근절", "start_ms": 1344, "dur_ms": 276, "phase_lr_deg": 0.0}
]
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
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 08:32:50*
