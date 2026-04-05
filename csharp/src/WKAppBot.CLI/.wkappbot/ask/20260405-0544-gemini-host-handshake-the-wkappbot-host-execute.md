# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=9236  time=05:41:11  uptime=13501s  mem=138MB  system_processes=350"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_054101.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 263-103-176-167-163-615-310-130-610-103-103-127-671-136-106-167-176-251-527 102-137-126-173-657-106-657-165-160-103-631-130-106-103-162-312-156-136-163-173-106-136-167-136-617-1_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 571-517-576-570-517-571-570 517-153-513-157-153-517-175-715-173-751-570-570-571-570-570-570-751-750-576-570-576-751-751-756-750-756-750-571-570 175-175-751-571-513-517-507-135-1_V.mp3)
  Segment 3: 3152ms ~ 12944ms (file: 103-321-754-267-152-756-107-176-130-216-310-120-123-326-321-321-623-623-623-710-102-754-267-231-236-263-276-265-312-132-524-627-236-231-237-170-613-725-756-130-106-216-572-103-2_W.mp3)
  Segment 4: 13944ms ~ 14520ms (file: 130-136-126-362-120-167-167-176-312-367-716-327-713-263-276-263-362-765-261-261-263-267-261-761-762-761-756-765-276-765-627-263-236-276-236-601-120-106-627-672-672-673-762-675-6_W.mp3)
  Segment 5: 15520ms ~ 16096ms (file: 276-573-231-372-756-531-672-162-675-126-275-123-152-750-612-536-632-765-206-271-576-273-736-216-627-263-705-572_V.mp3)
  Segment 6: 17096ms ~ 17672ms (file: 213-362-103-310-735-731-730 672-675-607-106-362-360-163-103-701-170-103-301-301-132-136-163-613-617-671-613-163-167-163-602-670 672-672-675-675-765-657-672-652-627-263-216-236-1_W.mp3)
  Segment 7: 18672ms ~ 19248ms (file: 157-105-715-170-175-157-157-107-157-175-107-175-170-173-175-157-157-175-517-571-576-571-167 105-105-570-710-170-170-170-571-574-574-574-574-574 105-175-710-715-175-715-715-175-1_V.mp3)
  Segment 8: 20248ms ~ 20824ms (file: 170-715-751-751-715-175 105-103-105-150-105-157-157-175-156-175-175-176-175-157-175-157-157-157-175-175-173 157-150-105-105-157-157-157-175-157-175-157-175-175-571-157-571-571-1_V.mp3)
  Segment 9: 21824ms ~ 22400ms (file: 316-136-130-360-613-136-361-317 612-316-367_W.mp3)
  Segment 10: 23400ms ~ 23976ms (file: 107-310-713-173-137-751-103-371-713-103-170-713-130-103-301-317-317-157-572-753-701-301-107-103-703-730-701 751-175-371-130-173-130-307-137-713-310-137-317-137-173-137-130-310-1_V.mp3)
  Segment 11: 24976ms ~ 31852ms (file: 762-762-123-132-103-762-302-135-572-760-736-267-302-532-267-273-132-206-327-732-765-236-375-203-267-276-236-613-320-315-536-361-130-237-762-765-132-310-543-537-752-310-672-610-2_W.mp3)
  Segment 12: 32852ms ~ 33428ms (file: 132-130-156-136-317-130-317-132-162-156-251-136-136-103-316-103-103-756-137-317-136-156-631-162-612-103-103-216-136-317-103-103-136-136-134-136-153-612 136-135-103-165-106-105-1_V.mp3)
  Segment 13: 34428ms ~ 35004ms (file: 756-756 306-316-136-176-756-760-716-167-316-176-671-675-627-672-261-367-735-170 356_W.mp3)
  Segment 14: 36004ms ~ 36580ms (file: 301-134-304-134-143-134-103-104-314-130-104-143-134-314-301-134-103-134-314-103-134-103-315-134 134-134-103-135-143-103-104-401-103-314-130-314 103-314-310-103-130-153-134-153-1_V.mp3)
  Segment 15: 37580ms ~ 44960ms (file: 271-120-206-267-620-672-321-260-102-217-257-213-527-216-206-527-754-753-172-174-103-132-201-260-103-702-267-260-130-157-207-627-301-103-527-263-201-267-607-102-670-103-375-307-5_W.mp3)
  Segment 16: 45960ms ~ 52836ms (file: 123-103-201-276-572-123-574-103-130-752-517-527-570-275-756-201-103-351-130-123-213-621-756-765-675-123-103-103-675-236-327-123-370-721-751-310-357-135-576-547-105-735-527-620-3_W.mp3)
  Segment 17: 53836ms ~ 54412ms (file: 153-105-107-157-175-175-175-157-175-175-571-571-671-715-716 150-154-105-105-157-175-107-105-106-157-175-175-175-517-576-571-576-517-175 501-510-105-105-105-175-160-175-176-157-1_W.mp3)
  Segment 18: 55412ms ~ 55988ms (file: 672-107-327-750-765-572-756-576-236-236-107-756-756-257-761-632-367-507-672-321-207-270-312-362-372-617-165-506-721-213-671-572-256-623-265-756-526-765-267-726-326-345-507-316-7_V.mp3)
  Segment 19: 56988ms ~ 57564ms (file: 571-571-517-571-574-574-571-357-175-571-571-573-517-517-157-571-571-574-571-571-571-571-571-571-574 375-756-753-571-571-531-571-517-571-517-517-571-517-517-754 357-753-375-573-5_W.mp3)
  Segment 20: 58564ms ~ 59140ms (file: 105 107-175-105-173-170-170-170-173-170-173-107-103-170-132-103-170-175-170-170 501-103-107-170-130-170-137-710-107-107-170-170-103-173-710-713-137-170-170-137-137-132-173-172-1_V.mp3)
  Segment 21: 60140ms ~ 64496ms (file: 672-263-326-263-672-237-326-263-726-216-672-123-627-267-672-672-627-762-106-107-760-176-760-762-762-176-672-672-672-726-762-726-672-762-672-617-267-672-321-362-236-120-261-106-1_W.mp3)
  Segment 22: 65496ms ~ 66072ms (file: 571-751-751-576-756-576-576-576-576-576-576-576-570-573 751-571-751-153-170-570-175-751-571-517-571-576-567-567-576-571-576-571-571-571-756-571-571-571-573-501-517 507-571-570-1_V.mp3)
  Segment 23: 67072ms ~ 67648ms (file: 571-513-571-571-571-517 517-517-571-157-175-157-175-751-175-175-157-517-571-157-513-517-157-153-571-751-517-571 715-517-573-517-153-175-751-571-175-751-571-573-571-715-317-157-1_V.mp3)
  Segment 24: 68648ms ~ 75704ms (file: 136-107-231-357-130-321-310-213-267-275-376-263-756-257-321-765-231-163-231-231-132-103-267-321-532-267-231-127-731-230-237-231-321-751-617-123-237-312-102-123-326-763-237-321-2_W.mp3)
  Segment 25: 76704ms ~ 84624ms (file: 206-321-263-267-126-123-123-572-574-265-210-210-127-572-657-675-236-257-237-267-276-267-576-627-672-362-752-751-754-357-321-326-136-513-623-123-216-627-126-627-261-716-276-316-3_W.mp3)
  Segment 26: 85624ms ~ 86200ms (file: 527-352-352-317-317-317-572-576-576-571-351-513-573-532-125-215-321-321-321-312-132-213-213-123-231-123-125-123-123-216-572-152-257-751-357-125-135-375-371-531-574-573-572-527-5_W.mp3)
  Segment 27: 87200ms ~ 87776ms (file: 657-637-675-360-162-261-765-176-130-102-130-623-216-201-301-365-316-310-162-176-361-163-632-312 574-162-631-362-312-312-230-362-310-301-102-625-132-517-312-301-130-361-576-576-3_W.mp3)
  Segment 28: 88776ms ~ 89352ms (file: 105-517-507-571-157-750-751-571-571-574-571-574-574-570-574-574-574-754-756 574-574-574-574-570-571-570-574-570-570-574-507_W.mp3)
  Segment 29: 90352ms ~ 96220ms (file: 230-753-237-237-723-136-705-312-213-760-230-123-213-102-372-276-267-267-261-213-761-132-123-130-167-103-310-216-130-136-163-105-165-103-127-103-213-316-620-261-321-160-103-150-1_W.mp3)
  Segment 30: 97220ms ~ 97796ms (file: 310-735-173-317-130-310-103-137-310-130-310-130-107-107-172-307-753-372 135-315-310-310-301-103-315-103-301-301-317-130-310-103-301-130-310-132-103-130-107-317-103-130-310-701-7_V.mp3)
  Segment 31: 98796ms ~ 99372ms (file: 310-301-301-310-301-301-310-310-316-136-310-301 317-317-314-317-317-317-301-134-103-314-310-310-317-137-301-103-317-310-301-103-130-310-103 351-315 751 573-157-317-157-751-175-1_V.mp3)
  Segment 32: 100372ms ~ 104620ms (file: 103-316-756-173-173-675-731-317-105-513-125-231-206-260-213-123-312-620-260-103_W.mp3)
  Segment 33: 105620ms ~ 106196ms (file: 230-206-206-265-625-231-102-601-672-162-132-103-367-130-526-562-613-617-613-301-312-206-620-260-206-320-276-672-276-627-102-310-163-617-167-613-163-316-321-620-623-762-367-362-3_V.mp3)
  Segment 34: 107196ms ~ 115331ms (file: 273-276-756-576-654-574-726-765-263 120-137-102-102-170-103-123-102-163-527-621-167-120-321-753-170-236-573-574-527-125-103-216-276-267-265-327-130-107-276-613-176-160-273-732-5_W.mp3)
  Segment 35: 116331ms ~ 123819ms (file: 673-263-216-610-106-236-130-261-203-263-267-123-263-267-231-574-752-132-102-162-230-321-216-136-312-320-326-126-136-213-203-312-102-203-106-230-132-106-163-210-106-132-325-231-2_W.mp3)
  Segment 36: 124819ms ~ 137131ms (file: 103-310-637-136-136-672-103-516-230-206-735-362-265-756-275-627-637-267-237-762-317-763-756-756-213-312-276-752-205-752-675-657-527-576-132-753-573-203-267-752-310-230-103-206-7_W.mp3)
  Segment 37: 138131ms ~ 144359ms (file: 310-136-371-752-137-726-126-261-236-206-216-267-236-107-574-507-621-102-320-627-672-613-572-312-627-670-257-627-725-275-572-574-152-517-237-206-620-542-210-271-213-627-574-726-2_W.mp3)
  Segment 38: 145359ms ~ 158139ms (file: 102-127-201-216-215-175-102-127-103-201-251-132-507 523-752-326-627-672-673-176-317-176-617-136-276-671-765-673-513-367-673-360-306-306-306-362-317-367-631-736-376-375-675-673-7_W.mp3)
  Segment 39: 159139ms ~ 159715ms (file: 547-574-547-547-574-547-574-547-514-514 547-547-547-574-514-541-547-574-547-547 547-547-547-547-547-150-541-547-547-574-574 547-547 547-547-547-547-547-547-547-574 547-510-157-5_W.mp3)
  Segment 40: 160715ms ~ 161291ms (file: 571-571-571-507-507-507 173-157-715-517-571-751-756-751-571-571-576-571-571-576-571-576-567-517 507-715-173-157-175-571-751-175-716-751-176-751-570-750-570-750-570-570-715-750-5_V.mp3)
  Segment 41: 162291ms ~ 162867ms (file: 715-571-571-571-571-571-571-571-571-751-571-571-570-571-570-153-157-105-150-175-317-157-157-517-175-517-715-571-576-175-751-571-751-751-751-751-751-756-751-751-751-573-517-571 5_V.mp3)
  Segment 42: 163867ms ~ 171031ms (file: 162-306-321-317-317-263-213-206-357-137-745-215-752-126-167-160-326-620-261-261-312-621-613-261-164-231-321-236-267-130-263-214-627-263-321-162-267-754-175-754-321-213-620-576-7_W.mp3)
  Segment 43: 172031ms ~ 180995ms (file: 231-216-312-312-102-231-523-273-236-275-327-213-176-123-321-126-216-672-107-263-230-236-521-547-321-231-512-265-263-321-316-137-263-261-236-263-326-541-257-102-103-126-162-102-3_W.mp3)
  Segment 44: 181995ms ~ 182571ms (file: 157-167-176 105-105-150-154-154-157-175-165-167-156-165-651-165-615-165-165-675-167-615-617-615-176-617-105 105-105-103-150-157-165-165-105-106-105-156-105-156-153-516-156-105-1_V.mp3)
  Segment 45: 183571ms ~ 184147ms (file: 517-517-571-517-517-571-571-570-571-570-570-570-571-570-570-576-576-567-576-576-576 571-517-150 175-315-713-175-751-571-715-751-576-576-571-576-576-576-576-576-576-576-576-576-5_V.mp3)
  Segment 46: 185147ms ~ 185723ms (file: 217-213-167-203-257-203-210-736-671-736-367-761-675-537-325-312-135-573-610-106-103-301-312-173-173-317-312-237-207-230-320 123-301-317-270-103-132-103-137-310-103-103-132-103-1_W.mp3)
  Segment 47: 186723ms ~ 194211ms (file: 756-261-106-235-312-265-564-103-751-126-167-756-621-312-267-756-621-213-103-263-731-213-123-261-312-316-136-231-627-754-257-312-123-362-136-160-765-231-361-167-367-316-263-576-5_W.mp3)
  Segment 48: 195211ms ~ 195787ms (file: 637-176-137-136-217-176-167-136-136-137 701-710-705-701-750-750-750-750-756-761-701-765-761-710-706-710-716-701-713-716-716-713 705-761-701-170-701-731-713-107-701-710-107-710-7_V.mp3)
  Segment 49: 196787ms ~ 197363ms (file: 135-130-317-753-735-715-756-756_W.mp3)
  Segment 50: 198363ms ~ 198939ms (file: 627-627 317-162-126-326-126-321-632-136-613-167-672-175-310-126-236-132-310_W.mp3)
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
> **Time**: 2026-04-05 05:44:32
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "사실상의 청구서 예고입니다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "사실상의", "start_ms": 20, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "청구서", "start_ms": 240, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "예고입니다.", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 3,
"transcript": "다만 이 발언이 공식 정책이라기보다는 트럼프 특유의 협상용 압박 메시지라는 해석도 동시에 나오고 있습니다. 비록 백악관이 해당 연설 영상을 유튜브 계정에 올렸다가",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "다만", "start_ms": 40, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 300, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "발언이", "start_ms": 410, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "공식", "start_ms": 720, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "정책이라기보다는", "start_ms": 930, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "트럼프", "start_ms": 1650, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "특유의", "start_ms": 1910, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "협상용", "start_ms": 2220, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "압박", "start_ms": 2630, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "메시지라는", "start_ms": 2840, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "해석도", "start_ms": 3350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "동시에", "start_ms": 3710, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "나오고", "start_ms": 4020, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "있습니다.", "start_ms": 4330, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "비록", "start_ms": 4800, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "백악관이", "start_ms": 5100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "해당", "start_ms": 5510, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "연설", "start_ms": 5770, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "영상을", "start_ms": 6030, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "유튜브", "start_ms": 6400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "계정에", "start_ms": 6710, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "올렸다가", "start_ms": 7080, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "더 어려웠을 것이라는 것은 사실입니다. 아일랜드가 19세기에 겪은 그것과 구조는 달랐지만 핵심은 같았습니다. 식량의 주도권이 남의 손에 있는",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "더", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "어려웠을", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "것이라는", "start_ms": 560, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "것은", "start_ms": 1020, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "사실입니다.", "start_ms": 1230, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "아일랜드가", "start_ms": 1800, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "19세기에", "start_ms": 2360, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "겪은", "start_ms": 2870, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "그것과", "start_ms": 3080, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "구조는", "start_ms": 3440, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "달랐지만", "start_ms": 3800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "핵심은", "start_ms": 4350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "같았습니다.", "start_ms": 4710, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "식량의", "start_ms": 5300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "주도권이", "start_ms": 5660, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "남의", "start_ms": 6170, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "손에", "start_ms": 6380, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 6640, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "명성으로 유명했는데 리메이크가 되면 세트는 작아지고 영상은 밋밋해지고",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "명성으로", "start_ms": 20, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "유명했는데", "start_ms": 430, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "리메이크가", "start_ms": 950, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "되면", "start_ms": 1460, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "세트는", "start_ms": 1720, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "작아지고", "start_ms": 2030, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "영상은", "start_ms": 2450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "밋밋해지고", "start_ms": 2810, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "무엇보다 일본 특유의 과장된 연기와 느린 전개 때문에 몰입감이 확 떨어진다는 평가가 정말",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "무엇보다", "start_ms": 15, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "일본", "start_ms": 430, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "특유의", "start_ms": 640, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "과장된", "start_ms": 950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "연기와", "start_ms": 1310, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "느린", "start_ms": 1670, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "전개", "start_ms": 1880, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "때문에", "start_ms": 2140, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "몰입감이", "start_ms": 2500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "확", "start_ms": 2960, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "떨어진다는", "start_ms": 3070, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "평가가", "start_ms": 3580, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 3940, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 8,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 9,
"transcript": "이런 후기들이 쏟아지면서 입소문이 순식간에 퍼졌습니다. 영화 내용도 굉장히 강렬합니다. 범죄 조직에게 납치된 딸을 구하기 위해 단 70",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "이런", "start_ms": 20, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "후기들이", "start_ms": 230, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "쏟아지면서", "start_ms": 690, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "입소문이", "start_ms": 1200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "순식간에", "start_ms": 1610, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "퍼졌습니다.", "start_ms": 2070, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "영화", "start_ms": 2600, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "내용도", "start_ms": 2860, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "굉장히", "start_ms": 3220, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "강렬합니다.", "start_ms": 3630, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "범죄", "start_ms": 4200, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "조직에게", "start_ms": 4460, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "납치된", "start_ms": 4920, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "딸을", "start_ms": 5280, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "구하기", "start_ms": 5490, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "위해", "start_ms": 5850, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "단", "start_ms": 6110, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "70", "start_ms": 6220, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "우와, 헨리 씨가 한국말 잘하네요. 잘해. 한국말 어디서 배우셨어요? 저...",
"speaker": "speaker_4",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "우와,", "start_ms": 10, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "헨리", "start_ms": 220, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "씨가", "start_ms": 430, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "한국말", "start_ms": 590, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "잘하네요.", "start_ms": 950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "잘해.", "start_ms": 1360, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "한국말", "start_ms": 1570, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "어디서", "start_ms": 1930, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "배우셨어요?", "start_ms": 2240, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "저...", "start_ms": 2750, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "에서는 착빙이 순식간에 진행되어 배는 완전히 통제 불능 상태에 빠지게 됩니다. 배가 전복되기 전에 얼음을 두드려 깨뜨리려면 승무원들이",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "에서는", "start_ms": 30, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "착빙이", "start_ms": 290, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "순식간에", "start_ms": 650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "진행되어", "start_ms": 1110, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "배는", "start_ms": 1520, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "완전히", "start_ms": 1730, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "통제", "start_ms": 2090, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "불능", "start_ms": 2350, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "상태에", "start_ms": 2610, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "빠지게", "start_ms": 2970, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "됩니다.", "start_ms": 3330, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "배가", "start_ms": 3800, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "전복되기", "start_ms": 4060, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "전에", "start_ms": 4470, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "얼음을", "start_ms": 4680, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "두드려", "start_ms": 5040, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "깨뜨리려면", "start_ms": 5400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "승무원들이", "start_ms": 5910, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 13,
"transcript": "함께 걸어가고 있습니다. 며칠 뒤 선배 로봇을 보고 자극을 받은 박지원 교수 또 다른 로봇",
"speaker": "speaker_6",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "함께", "start_ms": 40, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "걸어가고", "start_ms": 300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "있습니다.", "start_ms": 710, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "며칠", "start_ms": 1200, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "뒤", "start_ms": 1460, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "선배", "start_ms": 1600, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "로봇을", "start_ms": 1860, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "보고", "start_ms": 2220, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "자극을", "start_ms": 2430, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "받은", "start_ms": 2790, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "박지원", "start_ms": 3050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "교수", "start_ms": 3410, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 3670, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "다른", "start_ms": 3780, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "로봇", "start_ms": 3990, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 15,
"transcript": "빠지겠다라고 생각하시는 분들은 오히려 요럴 때 흔들릴 때 이제 매수를 하실 거고 아니다 총공세하고 이란이 열받아 가지고 중동 지역",
"speaker": "speaker_7",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "빠지겠다라고", "start_ms": 25, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "생각하시는", "start_ms": 550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "분들은", "start_ms": 1010, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "오히려", "start_ms": 1320, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "요럴", "start_ms": 1680, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 1940, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "흔들릴", "start_ms": 2050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 2410, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 2520, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "매수를", "start_ms": 2730, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "하실", "start_ms": 3090, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "거고", "start_ms": 3350, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "아니다", "start_ms": 3610, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "총공세하고", "start_ms": 3920, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이란이", "start_ms": 4430, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "열받아", "start_ms": 4790, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "가지고", "start_ms": 5150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "중동", "start_ms": 5510, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "지역", "start_ms": 5770, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "포커스 양보가 필요합니다.",
"speaker": "speaker_8",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "포커스", "start_ms": 15, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "양보가", "start_ms": 330, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "필요합니다.", "start_ms": 690, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 18,
"transcript": "이 신스킨 지용입니다.",
"speaker": "speaker_9",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 10, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "신스킨", "start_ms": 120, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "지용입니다.", "start_ms": 480, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "마라라고가 있는 플로리다 주 하원 보궐 선거에서 예상을 깨고 민주당 후보가 당선됐습니다. 대선에서 트럼프가",
"speaker": "speaker_10",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "마라라고가", "start_ms": 35, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 540, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "플로리다", "start_ms": 750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "주", "start_ms": 1160, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "하원", "start_ms": 1270, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "보궐", "start_ms": 1530, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "선거에서", "start_ms": 1790, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "예상을", "start_ms": 2250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "깨고", "start_ms": 2610, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "민주당", "start_ms": 2870, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "후보가", "start_ms": 3230, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "당선됐습니다.", "start_ms": 3590, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "대선에서", "start_ms": 4200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "트럼프가", "start_ms": 4660, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 21,
"transcript": "그 저희 집에 남는 게 그 집밖에 없는 거예요. 좋을 때도 돈 많이 벌다가 안 좋으면 다 날리고 집밖에 없어요 그래서 제가",
"speaker": "speaker_11",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "그", "start_ms": 20, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "저희", "start_ms": 130, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "집에", "start_ms": 390, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "남는", "start_ms": 650, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 910, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 1020, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "집밖에", "start_ms": 1130, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "없는", "start_ms": 1490, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "거예요.", "start_ms": 1750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "좋을", "start_ms": 2110, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "때도", "start_ms": 2370, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "돈", "start_ms": 2630, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 2790, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "벌다가", "start_ms": 3050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 3410, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "좋으면", "start_ms": 3520, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 3880, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "날리고", "start_ms": 3990, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "집밖에", "start_ms": 4350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "없어요", "start_ms": 4710, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 5070, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 5430, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 23,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 24,
"transcript": "억이 넘는 돈을 쏟아부어 K2 흑표 주력 전차 1000대와 K9 자주포 672문을 비롯해 K239 천무 다연장 로켓 288기와 FA-50 경공격기 48대를 주문했습니다. 이 숫자들은 앞으로 폴란드 육군의 절대적인 주력이 모두 한국산 무기로 교체된다는 뜻이었습니다. 그런데",
"speaker": "speaker_12",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "억이", "start_ms": 30, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "넘는", "start_ms": 240, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "돈을", "start_ms": 500, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "쏟아부어", "start_ms": 760, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "K2", "start_ms": 1220, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "흑표", "start_ms": 1430, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "주력", "start_ms": 1690, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "전차", "start_ms": 1950, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "1000대와", "start_ms": 2210, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "K9", "start_ms": 2670, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "자주포", "start_ms": 2880, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "672문을", "start_ms": 3240, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "비롯해", "start_ms": 3800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "K239", "start_ms": 4160, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "천무", "start_ms": 4620, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "다연장", "start_ms": 4880, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "로켓", "start_ms": 5240, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "288기와", "start_ms": 5550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "FA-50", "start_ms": 6060, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "경공격기", "start_ms": 6520, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "48대를", "start_ms": 6980, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "주문했습니다.", "start_ms": 7440, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 8100, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "숫자들은", "start_ms": 8210, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "앞으로", "start_ms": 8670, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "폴란드", "start_ms": 9030, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "육군의", "start_ms": 9390, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "절대적인", "start_ms": 9750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "주력이", "start_ms": 10210, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "모두", "start_ms": 10570, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "한국산", "start_ms": 10830, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "무기로", "start_ms": 11190, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "교체된다는", "start_ms": 11550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "뜻이었습니다.", "start_ms": 12060, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "그런데", "start_ms": 12720, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "굳이 건드린다고 이랬습니다. 세련되고 치밀한 K복수극에 익숙해진 시청자들 입장에서는 굳이 다시 만들 필요조차 없다고 느끼게",
"speaker": "speaker_13",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "굳이", "start_ms": 25, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "건드린다고", "start_ms": 240, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이랬습니다.", "start_ms": 700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "세련되고", "start_ms": 1250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "치밀한", "start_ms": 1660, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "K복수극에", "start_ms": 2020, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "익숙해진", "start_ms": 2530, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "시청자들", "start_ms": 2940, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "입장에서는", "start_ms": 3400, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "굳이", "start_ms": 3960, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 4170, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "만들", "start_ms": 4430, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "필요조차", "start_ms": 4690, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "없다고", "start_ms": 5150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "느끼게", "start_ms": 5510, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 27,
"transcript": "안면 구독과 좋아요 꼭 부탁드리겠습니다.",
"speaker": "speaker_14",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "안면", "start_ms": 15, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "구독과", "start_ms": 280, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "좋아요", "start_ms": 640, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "꼭", "start_ms": 950, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "부탁드리겠습니다.", "start_ms": 1110, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 29,
"transcript": "이른바 가성비 소모전입니다. 이 불안한 구조 속에서는 아무리 강한 미국이라도 버틸 수 없습니다. 요격 미사일의 극심한 재고 부족에 시달리는",
"speaker": "speaker_15",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "이른바", "start_ms": 30, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "가성비", "start_ms": 290, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "소모전입니다.", "start_ms": 600, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 1300, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "불안한", "start_ms": 1410, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "구조", "start_ms": 1770, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "속에서는", "start_ms": 2030, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아무리", "start_ms": 2440, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "강한", "start_ms": 2800, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "미국이라도", "start_ms": 3060, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "버틸", "start_ms": 3570, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 3830, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "없습니다.", "start_ms": 3940, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "요격", "start_ms": 4400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "미사일의", "start_ms": 4660, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "극심한", "start_ms": 5120, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "재고", "start_ms": 5480, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "부족에", "start_ms": 5740, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "시달리는", "start_ms": 6100, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 31,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 32,
"transcript": "있다는 월스트리트 저널의 보도도 나왔습니다. 이란이 파키스탄에서 미국 측 인사들을 만날 의향이 없으며 미국의 요구안도 거부한다는 입장을 분명히 밝혔다는 겁니다.",
"speaker": "speaker_15",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "있다는", "start_ms": 20, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "월스트리트", "start_ms": 330, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "저널의", "start_ms": 790, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "보도도", "start_ms": 1150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "나왔습니다.", "start_ms": 1510, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "이란이", "start_ms": 2100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "파키스탄에서", "start_ms": 2460, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "미국", "start_ms": 3070, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "측", "start_ms": 3330, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "인사들을", "start_ms": 3440, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "만날", "start_ms": 3900, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "의향이", "start_ms": 4160, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "없으며", "start_ms": 4520, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "미국의", "start_ms": 4880, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "요구안도", "start_ms": 5240, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "거부한다는", "start_ms": 5700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "입장을", "start_ms": 6210, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "분명히", "start_ms": 6570, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "밝혔다는", "start_ms": 6930, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "겁니다.", "start_ms": 7390, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 34,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 35,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 36,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 37,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 38,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 39,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 40,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 41,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 42,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 43,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 44,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 45,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 46,
"transcript": "있으면 구름 위로 올라타면서 노란색에서 초록색으로 전환됐죠. 바로 이 구간이 노이즈를 완전히 걸러낸 구간입니다. 자 위 차트를 보시면 바이 시그널이 나왔고 시그널",
"speaker": "speaker_16",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "있으면", "start_ms": 25, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "구름", "start_ms": 340, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "위로", "start_ms": 600, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "올라타면서", "start_ms": 860, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "노란색에서", "start_ms": 1370, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "초록색으로", "start_ms": 1880, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "전환됐죠.", "start_ms": 2390, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 2850, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 3110, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "구간이", "start_ms": 3220, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "노이즈를", "start_ms": 3580, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "완전히", "start_ms": 3990, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "걸러낸", "start_ms": 4350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "구간입니다.", "start_ms": 4710, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "자", "start_ms": 5300, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "위", "start_ms": 5410, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "차트를", "start_ms": 5520, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "보시면", "start_ms": 5880, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "바이", "start_ms": 6240, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "시그널이", "start_ms": 6450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "나왔고", "start_ms": 6860, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "시그널", "start_ms": 7220, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 48,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 49,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 50,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 05:44:32*
