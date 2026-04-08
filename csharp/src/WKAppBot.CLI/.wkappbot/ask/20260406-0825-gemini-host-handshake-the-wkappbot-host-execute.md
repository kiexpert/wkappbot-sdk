# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=40368  time=08:23:19  uptime=10269s  mem=440MB  system_processes=408"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_082013.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 6012ms (file: 135-312-103-310-231-315-361-312-132-123-126-275-321-312-213-231-236-276-270-312-762-267-260-527-574-207-256-120-752-753-327-230-752-756-752-756-325-573-201-231-725-752-120-312-5_W_stt_the_thing_that_is_now_that_the_chances_o.mp3)
  Segment 2: 7012ms ~ 16048ms (file: 236-267-263-751-261-263-213-320-321-310-326-326-627-263-216-163-132-231-261-321-167-130-316-302-367-205-175-137-103-103-103-301-301-130-130-316-367-263-625-260-267-672-276-102-1_W.mp3)
  Segment 3: 17048ms ~ 26120ms (file: 632-756-632-612-106-754-103-163-321-312-735-102-725-312-130-321-312-321-231-321-231-130-231-123-217-231-127-574-574-107-130-130-320-754-320-301-310-123-617-132-321-375-102-125-1_W.mp3)
  Segment 4: 27120ms ~ 33492ms (file: 174-321-231-312-132-130-103-137-175-231-103-312-201-302-320-207-267-267-547-210-127-126-126-120-126-216-216-201-120-120-126-126-613-162-162-126-120-321-236-127-123-126-126-276-3_W.mp3)
  Segment 5: 34492ms ~ 35068ms (file: 761-765-360-312-136-301-136-132-163-576-507-670-306-765-657-675-657-673-671-163-753-576-573-573-756-573_W.mp3)
  Segment 6: 36068ms ~ 46364ms (file: 752-321-312-263-326-261-123-213-126-263-601-627-627-716-725-754-213-327-102-627-267-263-627-623-267-126-103-162-216-361-132-237-753-231-263-612-601-574-574-312-120-571-130-160-1_W.mp3)
  Segment 7: 47364ms ~ 47940ms (file: 763-132-527-725-137-123-205-231-127-632-132-316-217-127-732-576-217-156-123-216-172-213-570-125-163-132-312-136-123-156-361-312-537-536-571-512-531-136-135-136-126-765-675_V.mp3)
  Segment 8: 48940ms ~ 49516ms (file: 316-716-765-751-732-716-571-576-752-765-175-167-167-167-265-627-627-625-256-652-657-672-672-756-726-637-765-652-672-675-761 675-756-715-176-172-126-106-615-610-761 623-613-165_W.mp3)
  Segment 9: 50516ms ~ 64844ms (file: 765-627-351-716-176-761-621-216-207-312-103-617-675-651-651-167-156-576-657-567-657-675-137-317-137-675-132-705-312-651-567-657-547-537-573-576-573-567-573-537-312-567-576-576-5_W.mp3)
  Segment 10: 65844ms ~ 66420ms (file: 175-175-175-157-517 175-175-175-715-175-715-175-175-157-175 576-751-576-751-175-157-175-175-175-175 137-715-175-175-175-175-157-571-157-175-157-175 135-175-571-751-756-571-715-1_V.mp3)
  Segment 11: 67420ms ~ 73684ms (file: 175-671-623-673-576-537-513-320-210-203-105-135-517-574-157-310-231-752-230-106-130-573-305-103-130-513-516-321-267-263-325-316-765-576-504-120-231-532-236-257-527-572-574-260-2_W.mp3)
  Segment 12: 74684ms ~ 75260ms (file: 671-176-765-756-751-716-756-756-756-576-576-576-756-751-754-754-753-751-751-756-751-756-751-756-751-756_W.mp3)
  Segment 13: 76260ms ~ 76836ms (file: 170-701-713-371-317-372-371-173-713-735-576-753-765-753-675 157-157 574-574-574-574-574-574-571-571-571-571-756-571-752-571-751-756-751 571-573-571-576-751-572-756-753-517-501-7_V.mp3)
  Segment 14: 77836ms ~ 78412ms (file: 765-637-627-137-376-176-715-716-736-715-763-176-756-765-756-675-761-765-675-765-756-675-765-765-765-765-736-675-675-765-765-675-765-675-167-130-103-175-167-175-176-175-162-316-7_W.mp3)
  Segment 15: 79412ms ~ 85820ms (file: 130-123-327-102-712-127-715-572-132-137-132-237-216-270-217-163-176-316-631-361-312-320-602-236-270-321-102-137-201-230-263-120-216-261-213-236-130-103-126-236-260-261-312-123-3_W.mp3)
  Segment 16: 86820ms ~ 95604ms (file: 120-705-170-103-103-231-230-261-201-210-612-762-265-103-106-103-320-326-326-203-201-230-572-103-107-263-132-103-276-320-752-132-206-260-206-675-301-213-203-102-103-176-106-132-1_W.mp3)
  Segment 17: 96604ms ~ 109024ms (file: 326-265-265-237-327-263-130-753-263-351-261-570-572-325-375-576-625-451-574-265-574-357-675-617-276-726-735-752-675-601-312-526-637-132 617-310_V.mp3)
  Segment 18: 110024ms ~ 110600ms (file: 731-735-731-736-763-712-756-167-173-715-751-175-157-371-136-315-573-576-571-516-521-153-517-571-517-105-132-236-736-173-761-673-173-137-176-576-761-570-617-675-165-671-103-132-6_W.mp3)
  Segment 19: 111600ms ~ 112176ms (file: 167-673-765-756-765-167-175-176-375-763-531-613-361-362_W.mp3)
  Segment 20: 113176ms ~ 119620ms (file: 361-710-103-576-571-153-137-567-603-567-105-165-617-705-576-507-750-570-571-135-756-750-754-756-216-312-735-276-327-375-726-137-726-715-271-276-321-721-275-267-271-725-152-276-2_W.mp3)
  Segment 21: 120620ms ~ 121196ms (file: 267-627-726-327-537-357 375-361-537-327-312-725-123-572-163-312-613-523-570-163-751-312-361-137-321-132-652-126-527-625-165-572_V.mp3)
  Segment 22: 122196ms ~ 122772ms (file: 750-236-503-571-265-326-602-206-562-103-236-613_V.mp3)
  Segment 23: 123772ms ~ 130288ms (file: 216-106-106-130-316-320-231-213-312-301-123-130-103-172-132-123-103-123-130-103-130-130-213-362-316-136-136-321-237-217-206-230-213-237-236-130-107-107-106-301-312-103-176-136-3_W.mp3)
  Segment 24: 131288ms ~ 131864ms (file: 761-176-617-716-617 756-173-361-675-716-175_W.mp3)
  Segment 25: 132864ms ~ 140388ms (file: 263-236-236-320-321-231-230-231-230-213-230-203-230-230-201-203-230-302-302-275-275-130-312-270-321-123-207-573-572-170-213-275-257-321-321-201-230-231-102-102-213-213-261-213-2_W.mp3)
  Segment 26: 141388ms ~ 141964ms (file: 317-317-317-317-137-312-316-132-310-132 571-175-576-675 351-756-312-173-570-261-216-721-127-137-153-157-527-235-326-125-721-206-156-723-326-321-273-123-507-321-502-573-720-267-3_V.mp3)
  Segment 27: 142964ms ~ 144800ms (file: 106-506-365-631-361-371-362-361-723-317-716-712-271-672-567-176-162-217-213-312-316-263-362-326-136-613-361-316-631-316-637-631-360-601-602-130-106-306-176-167-371-137-631-716-6_W.mp3)
  Segment 28: 145800ms ~ 146376ms (file: 153-350-736 167-106-675-567-156-561-516-657-650-567-756-570-506-540-571-567-576-576-657-547-567-653-671-157-567-675-765-567-527-675-547-675-567-762-162-132-312-127-567-351-261-2_W.mp3)
  Segment 29: 147376ms ~ 154180ms (file: 103-167-613-103-103-103-106-106-571-136-130-267-267-726-576-627-627-206-260-310-637-675-316-765-756-762-637-103-713-176-213-321-257-132-206-130-316-613-671-163-367-715-301-312-3_W.mp3)
  Segment 30: 155180ms ~ 155756ms (file: 576-576-576-571-571-571-175-705-175-715-753-517-715-513-517-753-571-753-570-570-571 576-574-571-503-570-357-157-175-157-753-750-570-750-751-570-150-570-570 574-574-574-571-574-5_W.mp3)
  Segment 31: 156756ms ~ 161400ms (file: 213-102-201-130-130-132-120-102-120-210-230-203-107-103-102-123-130-102-231-210-103-132-130-371-301-102-170-201-213-123-213-210-213-201-120-130-130-103-712-102-123-320-130-120-2_V.mp3)
  Segment 32: 162400ms ~ 162976ms (file: 372-765-576-571-156-257-675-627-504-576-362-576-751-572-506-526-257-526-627-206-763-625-561-560-105_V.mp3)
  Segment 33: 163976ms ~ 164552ms (file: 756 571-576 547-756-576-576-675-576-576-613-351_W.mp3)
  Segment 34: 165552ms ~ 166128ms (file: 765-756-765-375-657-165-167-165-675-156-675-675-756-765-675-756-751-672-675-765-657-576-567-571-561-567-576-715-751-175-576-156-156-165-156-165-167-126-126-123-756-756-756-765-7_W.mp3)
  Segment 35: 167128ms ~ 174724ms (file: 102-320-310-106-132-132-103-106-301-570-573-230-230-750-270-231-126-752-725-752-574-203-310-302-102-312-321-130-725-273-230-130-310-753-670-210-102-201-102-201-201-321-207-103-7_V.mp3)
  Segment 36: 175724ms ~ 176300ms (file: 375-137-176-763-675-736-763-716-162-763-167-617-617-763-752-675-761-617-627-367-637-176-673-760-627-216-253-235-703-126-263-321-312-136-136-316-132-162-576-127-275-163-627-263-3_W.mp3)
  Segment 37: 177300ms ~ 177876ms (file: 517-571-105-105-150-310 517-517-153-137-157-517-513-175-517-571-571-152-517-752-175-105-517-153-513-175-571-157-107-507-571-150-157 517-510-150-103-130-735-503-517-521-571-167-1_V.mp3)
  Segment 38: 178876ms ~ 179452ms (file: 207-127-756-263-562-572-312-257-235-570-123-572-237-517-317-172-570-312-237-216-317-275-716-527-265-756-726-275-672-573-576-672-306-170-576-756-671-167-763-132-123_V.mp3)
  Segment 39: 180452ms ~ 181028ms (file: 507-126-517-573-753-251-125-517-756-352-570-136-176-357-523-517-672-625-267-256-625-327-623-765-316-327-675-572-326-132-651-123-762-657-570-273-327-256-726-273-316-536-261-532-5_V.mp3)
  Segment 40: 182028ms ~ 189768ms (file: 167-174-136-136-137-176-210-213-132-207-260-210-260-620-260-576-571-157-130-305-375-317-360-310-317-136-312-123-213-302-612-153-532-621-572-752-137-735-362-763-715-765-132-107-1_W.mp3)
  Segment 41: 190768ms ~ 197248ms (file: 632-267-137-103-107-701-301-302-173-176-107-607-263-670-163-267-267-317-726-756-756-726-725-276-267-316-574-756-310-753-756-230-627-260-270-756-372-237-320-756-176-260-267-326-1_W.mp3)
  Segment 42: 198248ms ~ 198824ms (file: 321-250-267-325-762-176-327-720-362-325-231-750-756-760-627-562-725-570-326-576-723-732-172-136-317-312-312-236-326-175-135-317-573-765-351-315_V.mp3)
  Segment 43: 199824ms ~ 209256ms (file: 102-130-571-715-102-231-102-275-712-103-257-260-752-275-230-276-312-752-217-173-127-231-713-217-765-106-102-170-751-753-753-213-201-216-627-762-276-207-206-267-132-103-210-123-1_W.mp3)
  Segment 44: 210256ms ~ 210832ms (file: 167-761-765-765-765-765-176-671-176-617 576-573-576-576-715-756-576-756-751-675-371-173-170-751-170-150-510-170-175-170-507-157-671-716-167_V.mp3)
  Segment 45: 211832ms ~ 212408ms (file: 574-574-574-570-574-570-571 574-574-571-574-574-574-574-574-574-574-574-574-574-574-574-574-571 571-574-574-574-574-574-574-571-574-574-571-571 574-576-571-574-572-574-574-574-5_W.mp3)
  Segment 46: 213408ms ~ 218808ms (file: 735-327-371-136-617-260-130-132-310-312-310-310-136-130-103-130-103-103-175-130-302-301-136-130-621-263-326-321-723-371-231-230-126-176-107-273-236-236-123-260-265-213-261-216-2_W.mp3)
  Segment 47: 219808ms ~ 224704ms (file: 716-176-107-167-105-106-173-127-312-102-103-213-123-103-102-103-102-123-102-123-103-132-103-213-102-123-102-130-132-102-130-312-132-132-132-120-123-132-120-160-574-561-312-107-3_V.mp3)
  Segment 48: 225704ms ~ 226280ms (file: 756 573-572-751-756-570-576-617-670-675-670-627-172-261-502-523-236-267-236-762-276-157-265-271-720-261-321-235-215-162-312-217-231-235-275-137-671-572-752-312-271-507-756-756-2_V.mp3)
  Segment 49: 227280ms ~ 237144ms (file: 230-271-102-203-270-627-765-263-267-213-210-527-672-726-276-260-230-260-254-312-362-132-312-213-132-132-130-130-173-136-627-236-137-270-136-120-123-120-310-213-153-321-123-213-2_W.mp3)
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
> **Time**: 2026-04-06 08:25:41
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "What happened here? If you get this now, the chances of your success is exponentially higher. It really",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "What", "start_ms": 50, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "happened", "start_ms": 350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "here?", "start_ms": 800, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "If", "start_ms": 1200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 1400, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "get", "start_ms": 1650, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "this", "start_ms": 1950, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "now,", "start_ms": 2200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 2600, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "chances", "start_ms": 2800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "of", "start_ms": 3250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "your", "start_ms": 3450, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "success", "start_ms": 3700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 4250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "exponentially", "start_ms": 4450, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "higher.", "start_ms": 5300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "It", "start_ms": 5800, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "really", "start_ms": 5950, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "굳이 특사경을 통해서 할 필요가 없죠. 그런데 이젠 공수처, 중수처로 나눠지는 거는 일찌감치 이재명 정부 출범하면서부터 결정됐던 거고 이거는 검사들 입장에선 뭐냐",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "굳이", "start_ms": 100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "특사경을", "start_ms": 450, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "통해서", "start_ms": 1100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 1550, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "필요가", "start_ms": 1750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "없죠.", "start_ms": 2200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그런데", "start_ms": 2800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이젠", "start_ms": 3250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "공수처,", "start_ms": 3600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "중수처로", "start_ms": 4150, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "나눠지는", "start_ms": 4750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "거는", "start_ms": 5300, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "일찌감치", "start_ms": 5600, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "이재명", "start_ms": 6200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "정부", "start_ms": 6700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "출범하면서부터", "start_ms": 7100, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "결정됐던", "start_ms": 7950, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "거고", "start_ms": 8500, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "이거는", "start_ms": 8800, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "검사들", "start_ms": 9150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "입장에선", "start_ms": 9600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "뭐냐", "start_ms": 10150, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "solve some of the hardest problems in science. The fact that AI can write full pieces of software, the fact that soon AI can do more generalized knowledge work, those are all huge.",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "solve", "start_ms": 50, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "some", "start_ms": 400, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "of", "start_ms": 650, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 800, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "hardest", "start_ms": 1000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "problems", "start_ms": 1500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 1950, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "science.", "start_ms": 2150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "The", "start_ms": 2800, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "fact", "start_ms": 3000, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "that", "start_ms": 3300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "AI", "start_ms": 3500, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "can", "start_ms": 3750, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "write", "start_ms": 4000, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "full", "start_ms": 4300, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "pieces", "start_ms": 4550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "of", "start_ms": 4950, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "software,", "start_ms": 5100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 5700, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "fact", "start_ms": 5900, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "that", "start_ms": 6200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "soon", "start_ms": 6400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "AI", "start_ms": 6750, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "can", "start_ms": 7000, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "do", "start_ms": 7250, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "more", "start_ms": 7500, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "generalized", "start_ms": 7800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "knowledge", "start_ms": 8450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "work,", "start_ms": 8900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "those", "start_ms": 9300, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "are", "start_ms": 9600, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "all", "start_ms": 9800, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "huge.", "start_ms": 10100, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "수입을 만들어서 팔아왔던 소프트웨어 기업들의 주가는 일제히 급락했습니다.",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "수입을", "start_ms": 80, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "만들어서", "start_ms": 600, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "팔아왔던", "start_ms": 1200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "소프트웨어", "start_ms": 1850, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "기업들의", "start_ms": 2600, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "주가는", "start_ms": 3250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "일제히", "start_ms": 3750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "급락했습니다.", "start_ms": 4300, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 6,
"transcript": "안을 산다고 운을 띄웠습니다. 그러면서 미국 석유를 사거나 뒤늦은 용기를 내 호르무즈 해협에 가서 가져가라고 했습니다. 이어 당신들은 스스로 싸우는 법을 배워야 한다, 미국은 더 이상 당신들",
"speaker": "speaker_5",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "안을", "start_ms": 120, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "산다고", "start_ms": 450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "운을", "start_ms": 950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "띄웠습니다.", "start_ms": 1300, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "그러면서", "start_ms": 2100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "미국", "start_ms": 2650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "석유를", "start_ms": 3050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "사거나", "start_ms": 3550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "뒤늦은", "start_ms": 4050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "용기를", "start_ms": 4550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "내", "start_ms": 5000, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "호르무즈", "start_ms": 5200, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "해협에", "start_ms": 5800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "가서", "start_ms": 6300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "가져가라고", "start_ms": 6700, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "했습니다.", "start_ms": 7400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이어", "start_ms": 8100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "당신들은", "start_ms": 8450, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "스스로", "start_ms": 9050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "싸우는", "start_ms": 9550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "법을", "start_ms": 10000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "배워야", "start_ms": 10350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "한다,", "start_ms": 10850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "미국은", "start_ms": 11350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 11850, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이상", "start_ms": 12050, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "당신들", "start_ms": 12400, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "뭐 해줘 부탁할 게 많지만 AI는 엄청 많은 텍스트, 외부 웹페이지 등의 다양한 정보의 유입으로 인해서 취업",
"speaker": "speaker_6",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "뭐", "start_ms": 150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "해줘", "start_ms": 400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "부탁할", "start_ms": 800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 1300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "많지만", "start_ms": 1500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "AI는", "start_ms": 2100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "엄청", "start_ms": 2600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "많은", "start_ms": 3000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "텍스트,", "start_ms": 3400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "외부", "start_ms": 4000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "웹페이지", "start_ms": 4400, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "등의", "start_ms": 5000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "다양한", "start_ms": 5350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "정보의", "start_ms": 5850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "유입으로", "start_ms": 6350, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "인해서", "start_ms": 6950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "취업", "start_ms": 7450, "dur_ms": 400, "phase_lr_deg": 0.0}
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
},
{
"segment": 11,
"transcript": "나 뭐 좋아. 몇 년 만났어요? 6년. 6년 거의 7년이야. 아냐 6년 만나고 결혼했다고. 아",
"speaker": "mixed_speakers",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "나", "start_ms": 100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "좋아.", "start_ms": 500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "몇", "start_ms": 900, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "년", "start_ms": 1100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "만났어요?", "start_ms": 1300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "6년.", "start_ms": 1900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "6년", "start_ms": 2300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "거의", "start_ms": 2650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "7년이야.", "start_ms": 3000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "아냐", "start_ms": 3600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "6년", "start_ms": 3950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "만나고", "start_ms": 4300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "결혼했다고.", "start_ms": 4800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 5500, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 13,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 14,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 15,
"transcript": "지금 맞는데 이 마감을 이제 목재로 해서 맛이 이제 종목 구조처럼 보이게 했다. 네 네. 그거죠. 어 그렇구나. 요즘 느낌 훨씬 따뜻해",
"speaker": "mixed_speakers",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "지금", "start_ms": 100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "맞는데", "start_ms": 450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 900, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "마감을", "start_ms": 1050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 1500, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "목재로", "start_ms": 1800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "해서", "start_ms": 2250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "맛이", "start_ms": 2600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 2950, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "종목", "start_ms": 3250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "구조처럼", "start_ms": 3650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "보이게", "start_ms": 4200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "했다.", "start_ms": 4650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "네", "start_ms": 5100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "네.", "start_ms": 5300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "그거죠.", "start_ms": 5550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "어", "start_ms": 6000, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "그렇구나.", "start_ms": 6200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "요즘", "start_ms": 6800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "느낌", "start_ms": 7200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "훨씬", "start_ms": 7550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "따뜻해", "start_ms": 7950, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "태어났습니다.",
"speaker": "speaker_7",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "태어났습니다.", "start_ms": 100, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "준비됐어? 물론이죠 사실 저도 엄청 기대하고 있었어요.",
"speaker": "mixed_speakers",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "준비됐어?", "start_ms": 120, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "물론이죠", "start_ms": 1000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "사실", "start_ms": 1700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "저도", "start_ms": 2150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "엄청", "start_ms": 2550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "기대하고", "start_ms": 3000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "있었어요.", "start_ms": 3650, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 19,
"transcript": "김문종 씨가 A",
"speaker": "speaker_8",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "김문종", "start_ms": 80, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "씨가", "start_ms": 550, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "A", "start_ms": 850, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "와. 하셔도 돼요? 나중에 내 생각해서 맛있게 먹어요. 감사합니다. 김치찌개 시",
"speaker": "mixed_speakers",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "와.", "start_ms": 100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "하셔도", "start_ms": 450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "돼요?", "start_ms": 900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "나중에", "start_ms": 1500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "내", "start_ms": 2000, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "생각해서", "start_ms": 2200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "맛있게", "start_ms": 2750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "먹어요.", "start_ms": 3250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "감사합니다.", "start_ms": 3800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "김치찌개", "start_ms": 4500, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "시", "start_ms": 5100, "dur_ms": 150, "phase_lr_deg": 0.0}
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
"transcript": "추운 겨울에 따뜻한 대피처가 되어준다는 데요. 음 트리 하우스도 아",
"speaker": "speaker_9",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "추운", "start_ms": 120, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "겨울에", "start_ms": 500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "따뜻한", "start_ms": 950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "대피처가", "start_ms": 1450, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "되어준다는", "start_ms": 2050, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "데", "start_ms": 2750, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "요.", "start_ms": 2950, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "음", "start_ms": 3400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "트리", "start_ms": 3700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "하우스도", "start_ms": 4050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 4600, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "업무나 또는 뭐 사업이나 에이전트 AI가 도움 없이는 아마 어떤 사회 활동, 교육 활동, 경제 활동",
"speaker": "speaker_10",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "업무나", "start_ms": 100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "또는", "start_ms": 600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 1000, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "사업이나", "start_ms": 1200, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "에이전트", "start_ms": 1800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "AI가", "start_ms": 2350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "도움", "start_ms": 2850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "없이는", "start_ms": 3250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "아마", "start_ms": 3750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 4150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "사회", "start_ms": 4550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "활동,", "start_ms": 4950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "교육", "start_ms": 5450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "활동,", "start_ms": 5850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "경제", "start_ms": 6350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "활동", "start_ms": 6750, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 25,
"transcript": "기자 서류와 거주 허가증을 비롯해 국제 회의 등록 시스템 등 공식 시스템에서 대만을 표기하는 방식을 변경한 사실이 확인되면서다. 이에 대해 타이베이는 상징적 조치와 실질적 조치를 병행하는",
"speaker": "speaker_11",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "기자", "start_ms": 120, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "서류와", "start_ms": 500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "거주", "start_ms": 1000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "허가증을", "start_ms": 1400, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "비롯해", "start_ms": 2000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "국제", "start_ms": 2500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "회의", "start_ms": 2900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "등록", "start_ms": 3300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "시스템", "start_ms": 3700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "등", "start_ms": 4200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "공식", "start_ms": 4400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "시스템에서", "start_ms": 4800, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "대만을", "start_ms": 5500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "표기하는", "start_ms": 6000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "방식을", "start_ms": 6550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "변경한", "start_ms": 7050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "사실이", "start_ms": 7550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "확인되면서다.", "start_ms": 8050, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "이에", "start_ms": 8900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "대해", "start_ms": 9250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "타이베이는", "start_ms": 9650, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "상징적", "start_ms": 10300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "조치와", "start_ms": 10800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "실질적", "start_ms": 11300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "조치를", "start_ms": 11800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "병행하는", "start_ms": 12250, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "이거 의뢰 들어온 게 있는데 음 요거를 해야 되거든. 그래서 요거 맞춤 선반으로 제작해 달라고 하는데",
"speaker": "speaker_12",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "이거", "start_ms": 100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "의뢰", "start_ms": 450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "들어온", "start_ms": 850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 1300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있는데", "start_ms": 1500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "음", "start_ms": 2000, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "요거를", "start_ms": 2300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "해야", "start_ms": 2750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "되거든.", "start_ms": 3150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 3650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "요거", "start_ms": 4100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "맞춤", "start_ms": 4450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "선반으로", "start_ms": 4850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "제작해", "start_ms": 5400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "달라고", "start_ms": 5900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "하는데", "start_ms": 6400, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "집도 함께 지",
"speaker": "speaker_13",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "집도", "start_ms": 80, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "함께", "start_ms": 450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "지", "start_ms": 900, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "군사력을 과소평가했다가 어려움이 부딪히자 휴전을 제안했다고 주장했는데요. 미국이 쿠웨이트 부비안 섬에 있는 미군 군사 창",
"speaker": "speaker_14",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "군사력을", "start_ms": 120, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "과소평가했다가", "start_ms": 650, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "어려움이", "start_ms": 1500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "부딪히자", "start_ms": 2050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "휴전을", "start_ms": 2600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "제안했다고", "start_ms": 3100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "주장했는데요.", "start_ms": 3750, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "미국이", "start_ms": 4550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "쿠웨이트", "start_ms": 5050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "부비안", "start_ms": 5600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "섬에", "start_ms": 6100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 6450, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "미군", "start_ms": 6750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "군사", "start_ms": 7150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "창", "start_ms": 7550, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "불렀으면 집 한 채 뚝딱 지을 시간이었죠. 그 바닥만 있었어요. 근데 되게 행복한 거예요. 아 여기 오면 아이들에게 각각 방을 줄 수가 있잖아요.",
"speaker": "mixed_speakers",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "불렀으면", "start_ms": 100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "집", "start_ms": 650, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 850, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "채", "start_ms": 1050, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "뚝딱", "start_ms": 1250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "지을", "start_ms": 1650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "시간이었죠.", "start_ms": 2000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 2700, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "바닥만", "start_ms": 2900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "있었어요.", "start_ms": 3400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 4000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "되게", "start_ms": 4350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "행복한", "start_ms": 4700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "거예요.", "start_ms": 5200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 5700, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "여기", "start_ms": 5900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "오면", "start_ms": 6300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "아이들에게", "start_ms": 6650, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "각각", "start_ms": 7300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "방을", "start_ms": 7700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "줄", "start_ms": 8050, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "수가", "start_ms": 8250, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "있잖아요.", "start_ms": 8550, "dur_ms": 500, "phase_lr_deg": 0.0}
]
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
"transcript": "지금껏 보지 못한 새로운 AI의",
"speaker": "speaker_15",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "지금껏", "start_ms": 100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "보지", "start_ms": 600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "못한", "start_ms": 950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "새로운", "start_ms": 1300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "AI의", "start_ms": 1800, "dur_ms": 350, "phase_lr_deg": 0.0}
]
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
"transcript": "믿지 않는다고 하죠. 그런데 이게 다가 아닙니다. 이렇게 한식의 인기를 끌자 돈 냄새를 맡은 중국인들이 한국인 흉내를 내며 한식당을 차리는데 아주 형편없거든요. 한국 교포들도 골머리를 앓고 있다고 하죠. 그런데",
"speaker": "speaker_16",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "믿지", "start_ms": 120, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "않는다고", "start_ms": 500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "하죠.", "start_ms": 1000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그런데", "start_ms": 1450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 1900, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "다가", "start_ms": 2200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "아닙니다.", "start_ms": 2550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 3150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "한식의", "start_ms": 3600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "인기를", "start_ms": 4100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "끌자", "start_ms": 4550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "돈", "start_ms": 4950, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "냄새를", "start_ms": 5200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "맡은", "start_ms": 5700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "중국인들이", "start_ms": 6100, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "한국인", "start_ms": 6800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "흉내를", "start_ms": 7300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "내며", "start_ms": 7800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "한식당을", "start_ms": 8200, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "차리는데", "start_ms": 8800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 9350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "형편없거든요.", "start_ms": 9700, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 10600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "교포들도", "start_ms": 11000, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "골머리를", "start_ms": 11600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "앓고", "start_ms": 12150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있다고", "start_ms": 12550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "하죠.", "start_ms": 13000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그런데", "start_ms": 13450, "dur_ms": 400, "phase_lr_deg": 0.0}
]
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
"transcript": "이런 시장이었습니다. 이런 시장이 잘못 매매하면 굉장히 크게 손해 볼 수도 있는데 지수가 하루에 5퍼센트",
"speaker": "speaker_17",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "이런", "start_ms": 100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "시장이었습니다.", "start_ms": 450, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 1250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "시장이", "start_ms": 1600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "잘못", "start_ms": 2050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "매매하면", "start_ms": 2450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "굉장히", "start_ms": 3000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "크게", "start_ms": 3500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "손해", "start_ms": 3900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "볼", "start_ms": 4300, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "수도", "start_ms": 4550, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "있는데", "start_ms": 4850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "지수가", "start_ms": 5350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "하루에", "start_ms": 5850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "5퍼센트", "start_ms": 6300, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "이어지기 때문입니다. 결국 대만 방송이 논란의 핵심은 하나입니다. 한국 방위 산업이 이제 우연히 몇 건 수출한 수준이 아니라 세계 질서 변화 속에서 기회를 산업화",
"speaker": "speaker_18",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "이어지기", "start_ms": 120, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "때문입니다.", "start_ms": 650, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "결국", "start_ms": 1350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "대만", "start_ms": 1750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "방송이", "start_ms": 2150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "논란의", "start_ms": 2650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "핵심은", "start_ms": 3150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "하나입니다.", "start_ms": 3650, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 4400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "방위", "start_ms": 4800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "산업이", "start_ms": 5200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 5700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "우연히", "start_ms": 6050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "몇", "start_ms": 6550, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "건", "start_ms": 6800, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "수출한", "start_ms": 7000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "수준이", "start_ms": 7500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 8000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "세계", "start_ms": 8450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "질서", "start_ms": 8850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "변화", "start_ms": 9250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "속에서", "start_ms": 9650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "기회를", "start_ms": 10150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "산업화", "start_ms": 10650, "dur_ms": 500, "phase_lr_deg": 0.0}
]
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
"transcript": "믿지 않는다고 하죠. 그런데 이게 다가 아닙니다. 이렇게 한식의 인기를 끌자 돈 냄새를 맡은 중국인들이 한국인 흉내를 내며 한식당을 차리는데 아주 형편없거든요. 한국 교포들도 골머리를 앓고 있다고 하죠. 그런데",
"speaker": "speaker_16",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "믿지", "start_ms": 120, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "않는다고", "start_ms": 500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "하죠.", "start_ms": 1000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그런데", "start_ms": 1450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 1900, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "다가", "start_ms": 2200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "아닙니다.", "start_ms": 2550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 3150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "한식의", "start_ms": 3600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "인기를", "start_ms": 4100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "끌자", "start_ms": 4550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "돈", "start_ms": 4950, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "냄새를", "start_ms": 5200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "맡은", "start_ms": 5700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "중국인들이", "start_ms": 6100, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "한국인", "start_ms": 6800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "흉내를", "start_ms": 7300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "내며", "start_ms": 7800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "한식당을", "start_ms": 8200, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "차리는데", "start_ms": 8800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 9350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "형편없거든요.", "start_ms": 9700, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 10600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "교포들도", "start_ms": 11000, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "골머리를", "start_ms": 11600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "앓고", "start_ms": 12150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있다고", "start_ms": 12550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "하죠.", "start_ms": 13000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그런데", "start_ms": 13450, "dur_ms": 400, "phase_lr_deg": 0.0}
]
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
"transcript": "미나리가 잘 어울리네 바베큐랑. 어때요 잘 어울리네. 나도 미나리 절로 좋아하진 않",
"speaker": "mixed_speakers",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "미나리가", "start_ms": 100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 600, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "어울리네", "start_ms": 850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "바베큐랑.", "start_ms": 1350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "어때요", "start_ms": 2000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 2400, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "어울리네.", "start_ms": 2650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "나도", "start_ms": 3200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "미나리", "start_ms": 3550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "절로", "start_ms": 4000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "좋아하진", "start_ms": 4400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "않", "start_ms": 4950, "dur_ms": 150, "phase_lr_deg": 0.0}
]
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
"transcript": "한국에서도 휴머노이드 로봇을 생산해야 하는 거죠. 그런데 아틀라스와 동일한 방식으로는 좀 어렵다라는 겁니다. 그래서 2030년 이전까지는 모베드 기반 로봇이나 다리 없는 탁상용 휴머노이드로 대응을 하고 이후에는",
"speaker": "speaker_19",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "한국에서도", "start_ms": 120, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "휴머노이드", "start_ms": 750, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "로봇을", "start_ms": 1400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "생산해야", "start_ms": 1900, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 2500, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "거죠.", "start_ms": 2800, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "그런데", "start_ms": 3300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아틀라스와", "start_ms": 3750, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "동일한", "start_ms": 4450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "방식으로는", "start_ms": 4950, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 5650, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "어렵다라는", "start_ms": 5900, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "겁니다.", "start_ms": 6550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 7100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "2030년", "start_ms": 7600, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "이전까지는", "start_ms": 8250, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "모베드", "start_ms": 8900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "기반", "start_ms": 9400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "로봇이나", "start_ms": 9800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "다리", "start_ms": 10350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "없는", "start_ms": 10700, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "탁상용", "start_ms": 11000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "휴머노이드로", "start_ms": 11500, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "대응을", "start_ms": 12300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "하고", "start_ms": 12800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이후에는", "start_ms": 13200, "dur_ms": 500, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 08:25:41*
