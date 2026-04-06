# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=39828  time=08:24:28  uptime=10695s  mem=459MB  system_processes=412"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_082041.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 132-102-103-130-130-137-317-123-137-132-132 501-510-150-153-153-150-135-103-150-153-125-157-751-103-137-172-137-312 150-501-150-150-105-351-130-150-153-305-105-352-103-107-172-1_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 165-376-375-763-637-375-137-167-573-371-765 137-153-137_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 507-261-607-367-756-276-261-263-132-312-263-325-576-352-326-132-356-316-312-371-371-316-312-130-301-237-657-537-357-573-574-756-574-576-576-137-375_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 652-576-263-126-526-675-362-231-764-527-652-305-276-623-361-631-635-651-653-316-631-163-137-132-761-367-763-765 756-753-257-516-514-157_W.mp3)
  Segment 5: 6304ms ~ 12568ms (file: 216-760-760-672-203-201-216-270-273-206-201-765-301-136-260-210-261-627-126-302-132-206-301-263-602-620-620-620-756-752-206-217-260-756-267-216-123-312-310-570-137-213-126-260-6_W.mp3)
  Segment 6: 13568ms ~ 22496ms (file: 257-752-123-260-371-670-627-276-206-103-321-670-203-260-623-207-217-103-756-576-602-765-756-106-123-670-672-213-267-627-260-372-216-702-574-670-201-672-213-260-312-276-760-756-2_W.mp3)
  Segment 7: 23496ms ~ 33648ms (file: 132-273-267-267-267-103-107-570-256-263-130-162-260-761-130-267-612-130-216-260-216-167-267-371-710-735-103-752-213-305-165-120-160-160-713-130-126-172-176-317-167-261-601-231-2_W.mp3)
  Segment 8: 34648ms ~ 35224ms (file: 675-172-756-617-362-301-321-213-632-623-623-654-675-376-135-631-367-736-623-637-657-567-657-576-657-576-567-513-517-561-567-637-765-765-567-651-567-675-106-567-617-657-160-612-2_W.mp3)
  Segment 9: 36224ms ~ 43748ms (file: 263-236-236-320-321-231-230-231-230-213-230-203-230-230-201-203-230-302-302-275-275-130-312-270-321-123-207-573-572-170-213-275-257-321-321-201-230-231-102-102-213-213-261-213-2_W.mp3)
  Segment 10: 44748ms ~ 45324ms (file: 675-456-725-256-576-756-576-752-725-725-765-756-762-267-275-267-276-162-267-257-257-256-572-576-756-756-576-756-752-572-657-256-562-576-756-572-756-735-765-752-765-756-672-675-6_V.mp3)
  Segment 11: 46324ms ~ 53560ms (file: 207-203-127-102-630-132-130-126-120-320-203-620-260-132-720-726-236-172-574-301-276-671-130-267-760-216-547-670-260-231-312-236-607-617-620-672-260-123-103-263-210-756-105-103-7_W.mp3)
  Segment 12: 54560ms ~ 55136ms (file: 317-317-172-271-217-571-157-571-710-710-710-175-170-103-713-137-107-170-173-710-715-157-517-572-752-756-752-765-137-132-327-271-261-321-123-132-172-123-126-137-132-127-756-756-7_W.mp3)
  Segment 13: 56136ms ~ 56712ms (file: 751-751-751-705-756-765 715-175-751-751-570_W.mp3)
  Segment 14: 57712ms ~ 58288ms (file: 132-276-320-762-625-263-627-267-623-627-765-173-315-623-762-163-123-672-362-362-362-632-627-623-672-672-672-261-213-213-267-627-263-623-672-263-627-267-627-236-627-672-632-621-1_W.mp3)
  Segment 15: 59288ms ~ 65588ms (file: 106-267-267-273-276-276-237-102-367-537-167-617-752-617-753-203-312-130-231-230-617-237-267-627-263-623-752-276-160-103-317-237-273-362-316-167-627-267-230-275-367-267-672-267-2_W.mp3)
  Segment 16: 66588ms ~ 67164ms (file: 517-506-517-157-710-123-132-312-132-712-715-176-675-167-367-671-753-371-153-316-165-675-617-763-163-312-126-167-107-130-163-716-173-716-375-237-371-637-375-327-326-376-327-326-3_W.mp3)
  Segment 17: 68164ms ~ 68740ms (file: 567-752-567-576-751-156-647-672-175-375-376-716-317-712-176-627-625-617-625-613-672-675-617-672-761-172-167-637-612-762-637-672-627-673-637-637-671-762-372-165-671-756-765-756-7_W.mp3)
  Segment 18: 69740ms ~ 76544ms (file: 213-524-230-725-273-276-267-501-517-731-107-103-120-176-123-706-601-621-201-125-615-617-613-617-625-612-610-627-160-106-103-120-103-123-361-671-761-760-765-321-362-632-263-762-1_W.mp3)
  Segment 19: 77544ms ~ 78120ms (file: 175-753-571-571-571-571-570-570-570-571-570-570-573-576-576 576-751-571-570-576-571-573-571-570-570-571-573-571-571-751-750-571-537 573-573-576-572-572-574-576-571-571-751-571-5_V.mp3)
  Segment 20: 79120ms ~ 79696ms (file: 570-362-705-261-702-672-216-265-627-673-136-316-317-631-361-317-316-371-361 321-362-632-623-631-576-153-157-317-716-623-325-216-513-720-723-713 765-167-163-753-761 753-671-563-7_W.mp3)
  Segment 21: 80696ms ~ 81272ms (file: 750-710-760-750-751-573-352-375-657-652-625-326-621-627-263-312-613-631-316-136-136-136-312-130-162-136-162-260 132-136-132-165 710-362-312-372-307-735-307-317-136-317-312-160-6_W.mp3)
  Segment 22: 82272ms ~ 91560ms (file: 751-327-273-123-275-276-132-326-312-123-123-125-312-317-160-162-163-106-321-637-167-130-160-576-261-260-236-231-273-231-627-263-762-267-236-210-217-217-230-726-236-627-261-756-2_W.mp3)
  Segment 23: 92560ms ~ 100695ms (file: 721-275-301-102-526-301-123-670-327-637-567-651-576-127-103-321-372-310-623-231-271-321-203-362-132-312-672-132-106-302-302-615-637-613-160-631-206-620-167-230-312-163-173-602-6_W.mp3)
  Segment 24: 101695ms ~ 108715ms (file: 213-326-627-213-120-261-620-612-312-126-236-261-123-321-261-261-136-136-170-317-260-260-261-261-713-107-213-210-176-310-261-261-261-154-326-217-267-123-132-312-135-310-613-617-6_W.mp3)
  Segment 25: 109715ms ~ 110291ms (file: 517-574-574-573-574-574-571-571-517-517-571-571-571-157 571-571-571-571-517-572-571-175-751-175-170-175-750-157-105-170-175-571-150-751 570-157-510-517-150-517-715-157-150-157-7_W.mp3)
  Segment 26: 111291ms ~ 111867ms (file: 172-713-710-173-317-317-731-375-573-576-107-750-172-756-765-673-765-765-376-751-731-756-716_W.mp3)
  Segment 27: 112867ms ~ 113443ms (file: 103 265 517-162-514-175-753-756-572-135-317-321-136-310-315-756-236-536-507-157-517-546-152-637-360-312-326-356-310-310-305-706-173-675-367-367-653-756-673-675-376-715-765-625-3_W.mp3)
  Segment 28: 114443ms ~ 115019ms (file: 571-517-153 571-571-547-571-517-517-157-517-510-517-510 571-571-517-510-150-157-150-157-150-157-157-157-510-517-571-576-537 574-571-517-571-572-157-153-517-517-571-571-317-573-5_W.mp3)
  Segment 29: 116019ms ~ 116595ms (file: 576-571-157-157-105-510 574-571-517-517-571-571-570-517-571-570-571-571 157-571-517-570-517-571-571-571-570-571-576-571 570-571-570-576-756-570-571-571-751-571-571-571-750-751-7_W.mp3)
  Segment 30: 117595ms ~ 118171ms (file: 756 571-576 547-756-576-576-675-576-576-613-351_W.mp3)
  Segment 31: 119171ms ~ 119747ms (file: 750 752-362-316-316-362-672-672-560-507-263-765-361-261-126-672-752-237-627-321-265-361-705-312-327-657-751-276-720-752-267-361-361-301-361-750-261-265-165-750-632-762-705-367-3_V.mp3)
  Segment 32: 120747ms ~ 121323ms (file: 103-103-103-103-132-126-103-310-132-103-135-103-135-712-361-172-127-576-132-261-325-167-762-367-657-623-257-627-137-750 574-216-527-251-617-126-276-621-756-561-576-126-127-617-6_V.mp3)
  Segment 33: 122323ms ~ 122899ms (file: 357-317-315-305-317-310-130-132-170-673-367-371-367-237-637-372-637-276-623-631-632-263-376-317-617-627-276-673-673-376-135-675-756 753-756-753-753-765_W.mp3)
  Segment 34: 123899ms ~ 132395ms (file: 103-203-210-132-357-276-271-316-260-267-207-207-627-267-602-123-237-217-765-627-627-260-103-102-120-517-260-203-260-126-276-542-201-762-316-201-236-216-547-514-271-572-126-574-1_W.mp3)
  Segment 35: 133395ms ~ 133971ms (file: 236-367-307-367-367-761-367-527-325-702-627-732-735-132-635-532-756-672-276-507-753-236-705-572-261-316-672-751-670-271-207-326-257-675-275-127-713-572-751-507-513-135-207-275-2_V.mp3)
  Segment 36: 134971ms ~ 135547ms (file: 571 571-571-571-571-571-571-573-573-571-573-517-576-576 574-576-576-571-576-571-571-571-571-571-571-571-571-571-571 574-571-576-574-571-576-571-517-517-517-571-517-571-570 571-5_W.mp3)
  Segment 37: 136547ms ~ 137123ms (file: 761-623-127-761-765-537-735-726-217-327-123-125-217-216-573-751-723-712-216-721-756-315-712-512-273-271-217-571-721-326-527-715-257-216-125-753-506-267-267-273-213-126-756-162-6_V.mp3)
  Segment 38: 138123ms ~ 142659ms (file: 563-132 612-176-627-126-672-362-617-132-612-312-312 367-162-675-675-163-103-312 132-102-152-231-206-602-213-132-301-627-260-260-130_W_stt_the.mp3)
  Segment 39: 143659ms ~ 144235ms (file: 567-627-627-762-276-276-261-326-732-236-762-132-607-760-765-756-574-726_V.mp3)
  Segment 40: 145235ms ~ 150023ms (file: 763-617-637-613-637-132-143-213-570-574-154-103-143-751-567-571-513-267-276-201-267-267-267-325-103-756-275-320-103-203-132-231-621-672-361-623-613-617-675-673-763-765-563-670-6_W.mp3)
  Segment 41: 151023ms ~ 167114ms (file: 312-326-136-230-231-302-301-231-321-341-345-316 362-326-312-317-316-304-345-312 276-132-316-316-145-123-306-263_V.mp3)
  Segment 42: 168114ms ~ 177366ms (file: 236-716-231-236-213-312-123-126-102-267-123-123-627-627-263-236-231-267-231-312-236-160-162-730-120-216-326-672-756-132-216-321-236-263-261-527-120-627-265-231-213-123-216-257-2_W.mp3)
  Segment 43: 178366ms ~ 188770ms (file: 316-102-143-203-756-172-173-132-132-307-301-302-637-261-716-713-162-231-621-167-706-756-276-762-203-675-762-103-731-317-735-237-267-263-762-267-736-107-273-273-231-261-126-701-3_W.mp3)
  Segment 44: 189770ms ~ 198554ms (file: 320-267-267-267-320-270-735-173-754-756-230-216-726-312-752-672-701-712-701-710-107-137-132-302-317-263-276-267-267-270-210-321-320-230-231-231-320-213-310-312-310-132-132-321-1_W.mp3)
  Segment 45: 199554ms ~ 200130ms (file: 715-716-175-715-715-716-716-715-716-715 607-176-107-701-175-710-107-170-715-715-176-710-715-751-715-751-715-716-715-716 671-175-751-701-715-701-715-173-716-716-713-731-713-731-7_V.mp3)
  Segment 46: 201130ms ~ 209770ms (file: 527-103-627-627-213-263-213-217-762-672-612-130-216-261-267-627-231-102-261-312-321-132-250-230-126-203-261-102-103-132-102-317-107-231-102-172-102-130-537-253-120-216-213-236-1_W.mp3)
  Segment 47: 210770ms ~ 211346ms (file: 215-752-576-162-721-153-236-573-572-216-527-231-261-267-756-523-621-256-527-172-257-567-657-657-603-236 765-756-576-756-756-756-756-576-163-357-361-361-316-365-135-136-315-135-3_W.mp3)
  Segment 48: 212346ms ~ 212922ms (file: 157-517-517-150-501-150-570-517 574-574-574-574-571-574-574-574-574-574-571-570-175-570-571-517-153-157-517-715-750-751-750-571-573 571-175-517-517-157-571-157-153-517-573-570-1_V.mp3)
  Segment 49: 213922ms ~ 214498ms (file: 574-570-571 571-574-573-574-574-574-570-570-570-570-571-571-571-571-517-507-137-370-157-701-570 571-570-571-157-573-157-573-105-107-150-517-571-517-751-513-135-710-175-507-175-5_V.mp3)
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
> **Time**: 2026-04-06 08:28:06
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
*Generated by WKAppBot ask gemini — 2026-04-06 08:28:06*
