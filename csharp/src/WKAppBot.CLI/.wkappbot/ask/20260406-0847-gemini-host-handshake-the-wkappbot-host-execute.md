# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=30488  time=08:45:50  uptime=18628s  mem=459MB  system_processes=411"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_084121.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 8352ms (file: 301-263-260-263-216-263-263-213-231-723-321-102-106-103-132-267-162-231-132-267-316-130-107-713-321-173-312-276-613-173-132-231-106-130-263-267-276-267-260-230-236-627-627-231-1_W.mp3)
  Segment 2: 9352ms ~ 9928ms (file: 675-675-762-657-675-762-276-526-720-657-752-256-621-176-762-751-157-715-567-657-564-571-567-725-567-572-572-563-572-652-256-572-574-752-576-576-572-576-347-524-526-542-765-752-5_V.mp3)
  Segment 3: 10928ms ~ 11504ms (file: 574-574-574-574-574-571-574-574-574-574-574-574-574-574-574 574-574-574-576-574-574-574-574-574-574-570-571-571-571-570-571-573-572-571 547-574-574-571-574-571-574-574-574-571-5_W.mp3)
  Segment 4: 12504ms ~ 20639ms (file: 576-756-756-752-750-705-753-735-375-107-756-756-375-130-107-751-103-310-310-675-675-756-170-715-517-715-735-137-316-137-317-731-137-136-137-175-106-132-753-375-756-372-672-673-7_W.mp3)
  Segment 5: 21639ms ~ 22215ms (file: 517-571-517-715-750-715-715-173-750 571-517-751-517-705-153-750-730-751-570-750-175-715-715-571-571-715-571-751-175-751-576-753-573-573 570-571-573-571-573-715-573-175-571-175-5_W.mp3)
  Segment 6: 23215ms ~ 31999ms (file: 106-103-753-230-321-326-301-130-103-321-260-231-236-326-326-321-132-321-130-136-132-203-126-351-102-601-621-237-236-327-723-236-312-321-213-237-213-217-130-107-107-213-127-752-1_W.mp3)
  Segment 7: 32999ms ~ 33575ms (file: 675-675-615-651-165-651-165-651-651-615-615-516-613-765 547-547-547-574-574-574-574_W.mp3)
  Segment 8: 34575ms ~ 35151ms (file: 625-625-625-625-625-625-625-265-265-621-261-263-621-216-120_W.mp3)
  Segment 9: 36151ms ~ 43675ms (file: 230-756-120-260-126-260-203-123-130-132-627-627-267-162-213-107-132-760-172-120-301-103-261-267-705-756-756-627-276-102-127-103-762-756-675-216-126-120-210-210-267-672-160-123-6_W.mp3)
  Segment 10: 44675ms ~ 45251ms (file: 105-105-103-105-153-105-135-103-105-157-610-160-317-176-175-123-216-132-312-162-163-617-617-621-625-615-627-256-217-762-756-715-576-756-176-152-317-316-136-312-136-163-163-165-1_W.mp3)
  Segment 11: 46251ms ~ 53343ms (file: 137-263-231-231-753-276-276-312-327-317-276-572-237-273-267-267-173-371-351-756-657-321-276-237-123-753-371-176-310-361-213-312-276-231-312-237-762-316-317-721-213-130-107-106-5_W.mp3)
  Segment 12: 54343ms ~ 54919ms (file: 253-705-512-627-270-572-326-576-126-623-312-315-132-312-270-273-725-215-273-537-752-572-547-762-706-215-562-267-317-324-723-357-326-523-321-135-163-576-316-163-237-237-237-236-7_V.mp3)
  Segment 13: 55919ms ~ 56495ms (file: 132-132-127-125-132-675-716-736-372-735-751-157-573-753-513-517-573-652-653-165-715-371-571-315-516-156-165-615-531-167-317-175-375-571-732-172-127-167-176-176-617-617-613-671-6_W.mp3)
  Segment 14: 57495ms ~ 62535ms (file: 321-273-257-542-260-276-123-120-210-210-201-271-217-217-123-276-276-726-213-273-273-217-671-231-273-312-751-173-712-713-273-672-273-753-275-752-237-236-127-261-237-231-132-574-3_W.mp3)
  Segment 15: 63535ms ~ 64111ms (file: 152-150-157-157-157-153-157-153-153-517-517-157-517-157-125-103-102-126-126-160-126-120-102-132-123-126-126-126-103-130-105-150-150-157-517-571-517-517-175-157-157-157-517-165-1_V.mp3)
  Segment 16: 65111ms ~ 65687ms (file: 573-573-576-571-574-571-574-571-571-574-574-571_W.mp3)
  Segment 17: 66687ms ~ 74751ms (file: 210-216-754-751-205-201-162-162-176-176-103-321-230-203-320-320-675-230-302-130-316-130-103-106-375-103-136-751-457-317-321-103-132-137-173-136-170-173-310-126-162-621-627-623-1_V.mp3)
  Segment 18: 75751ms ~ 83851ms (file: 216-132-321-236-756-136-102-126-216-721-261-321-375-321-310-326-761-627-213-236-631-160-312-326-132-260-321-236-627-672-756-136-312-261-201-312-260-627-316-236-762-310-310-735-2_W.mp3)
  Segment 19: 84851ms ~ 93671ms (file: 547-657-652-516-761-267-627-754-157-321-207-312-231-165-317-310-315-160-106-170-761-276-260-103-762-231-231-167-576-267-527-230-576-756-760-271-265-207-170-706-703-701-167-106-5_W.mp3)
  Segment 20: 94671ms ~ 95247ms (file: 627-762-765-765-760-567-657-657-576-675-756_W.mp3)
  Segment 21: 96247ms ~ 105859ms (file: 263-321-103-123-130-132-310-623-320-267-637-267-756-130-231-176-617-316-317-137-762-276-752-237-213-107-102-537-317-312-123-326-275-173-751-276-326-267-321-127-321-327-267-276-3_W.mp3)
  Segment 22: 106859ms ~ 107435ms (file: 572-276-670-213-157-765-756-526-257-627-206-570-236-312-267-726-325-236-675-265-235-752-375-325-237-257-256-326-267-572-132-321-376-327-315-135-352_V.mp3)
  Segment 23: 108435ms ~ 120855ms (file: 270-267-752-273-320-130-201-312-267-132-103-136-127-103-107-107-107-705-754-756-754-756-132-321-210-201-103-216-123-213-103-301-231-103-103-102-312-201-542-206-206-206-206-231-3_V.mp3)
  Segment 24: 121855ms ~ 122431ms (file: 327-267-267-275-215-502-326-237-750-236-657-605-267-567-163-705-571-237-567-765-267-671-675-760-725-265-726-751-752-756-725-752-752-706-271-672-576-756-657 256-562-231-257-750-7_V.mp3)
  Segment 25: 123431ms ~ 124007ms (file: 231-275-752-271-321-276-125-257-657-261-315-576-357-537-567-576-754-754-571-756-756-573-576-756-756-756-752-756-376-317-376-731-371-317-371_W.mp3)
  Segment 26: 125007ms ~ 125583ms (file: 763-526-267-263-567-256-270-504-231-623-765-736-672-675-265-627-576-265-602-673-362-527-263-276-657-256-205-627-265-267-265-267-627-315-265-265-327-756-756-765-765-763-765-765-7_V.mp3)
  Segment 27: 126583ms ~ 136339ms (file: 231-231-312-725-265-526-276-725-257-170-317-217-107-627-216-213-120-275-752-726-725-372-132-754-132-574-275-276-276-210-130-132-127-762-572-123-276-275-275-203-210-263-261-216-2_W.mp3)
  Segment 28: 137339ms ~ 137915ms (file: 312-256-702-623-137-327-237-312-173-673-123-572-162-762-351-376-672-207-750-536-527-765-762-276-705-675-627-675-103-576-132-130-310-132-601-576-726-157-315-763-275-725-762-730-1_V.mp3)
  Segment 29: 138915ms ~ 139491ms (file: 762-572-126-615-762-675-756-175-127-126-137-723-137-137-537-372-516-316-273-361-657-732-267-576-732-705-576-573-537-632-507-576-567-326-356-570 753-562-216-576-263-326 713-325-2_V.mp3)
  Segment 30: 140491ms ~ 146179ms (file: 231-203-230-201-321-710-375-713-231-270-260-206-270-137-725-213-172-231-217-260-312-263-120-735-137-612-371-501-270-507-203-120-172-127-170-263-213-372-165-136-127-132-210-132-1_V.mp3)
  Segment 31: 147179ms ~ 147755ms (file: 567-576-267-326-207-236-576-165-267-207-276-765-752-126-125-753-327-570-327-312-672-762-602-527-725-256-265-576-256-310_V.mp3)
  Segment 32: 148755ms ~ 154227ms (file: 362-275-762-563-753-375-132-723-750-375-265-527-210-672-567-162-573-276-657-217-520-123-216-350-205-572-256-156-267-230-213-621-316-276-263-276-267-763-371-735-725-765-763-316-2_W.mp3)
  Segment 33: 155227ms ~ 155803ms (file: 675-763-765-763-765-167-371-317-137-317-137-716-176-761-761-765-763-765-756-765-765-765-765-765-761-765-765-763-713-763-735-715-753-756-765-736-731-756-375-375-371-375-731-751-2_W.mp3)
  Segment 34: 156803ms ~ 157379ms (file: 576-571-574-576-574-573-513-175-574-132-637-765-675-132-173_W.mp3)
  Segment 35: 158379ms ~ 163491ms (file: 216-170-627-216-276-276-267-321-312-321-106-761-627-537-357-267-276-237-623-267-206-276-705-756-276-276-276-162-102-276-106-267-260-260-270-320-276-250-165-672-235-207-120-213-5_W.mp3)
  Segment 36: 164491ms ~ 174067ms (file: 267-217-136-316-752-510-130-531-132-310-320-103-103-301-312-137-716-136-136-617-167-670-627-236-136-132-312-260-316-312-231-256-215-570-206-316-120-326-213-236-203-260-201-627-2_W.mp3)
  Segment 37: 175067ms ~ 175643ms (file: 571-571-571-574-570-571-574-570-571 574-571-574-574-574-574-574-576-571 517-576-571-571-517-517-510-517-157-573-571-571-571-572-175-571-571-571-571-751-756-753-571-573-572 517-5_W.mp3)
  Segment 38: 176643ms ~ 177219ms (file: 765-765-637-715 761-731-763-713-731-716-763-716-763 132-312-310-173-316-301-103-136-106-130-761-760-716-657_W.mp3)
  Segment 39: 178219ms ~ 184447ms (file: 312-216-216-213-123-163-726-327-263-216-710-276-327-362-106-613-621-627-317-237-276-627-163-160-627-620-137-260-726-726-267-267-627-267-627-267-263-132-213-123-312-326-236-632-1_W.mp3)
  Segment 40: 185447ms ~ 186023ms (file: 170-170-170-170-175-176-715-175-715-175-716-715-761 750-157-176-107-715-701-170-170-751-175-175-175-176-175-175-176-175-175-176-715-175-176-175-715 105-175-103-710-170-173-170-1_V.mp3)
  Segment 41: 187023ms ~ 187599ms (file: 631-316-176-736-162-761-763-176-173-321-260-735-356-752-751-671-651-316-316-316-157-137-721-716-617-167-574-276-576-576-125-132-712-132-756-756-103-157-125-273-270-712-725-521-3_W.mp3)
  Segment 42: 188599ms ~ 195943ms (file: 527-521-320-235-257-275-720-752-203-201-752-503-562-107-317-571-571-267-162-704-260-732-213-602-610-105-130-263-132-163-675-657-657-236-752-326-275-257-265-236-321-102-162-136-1_W.mp3)
  Segment 43: 196943ms ~ 197519ms (file: 736-763-763-763-716-761-756-371-762-763-736-763-763-763-167-756-736-376-367-317-751-157-157-571-125-165-132-137-263-216-271-273-751-157-136-123-765-576-275-623-756-726-527-203-2_V.mp3)
  Segment 44: 198519ms ~ 199095ms (file: 571-751-571-751-751-751 574-574-571-571-571-517-571-571-576-573-573-571 571-571-571-571-571-571-571-574 573-571-571-571-571-517-571-571-571 571-571-571-571-751-517-570-571-751-5_W.mp3)
  Segment 45: 200095ms ~ 200671ms (file: 157-517-175-571-517-571-572-130 153-175-513-153-175-751-702-725-703-701-120-173-107-137-132-172-102-130-127-102-135-153-172-371-173-372-731-172-173-132-132-312-371-751-571-157-1_W.mp3)
  Segment 46: 201671ms ~ 202247ms (file: 715-753-735-753-157-153-135-157-210-132-325-157-351-361-350-357_W.mp3)
  Segment 47: 203247ms ~ 216027ms (file: 132-132-123-320-261-201-132-132-103-312-260-230-310-230-310-102-571-312-310-130-320-203-103-321-203-302-103-230-325-130-301-320-102-310-163-263-321-102-102-230-236-320-103-103-3_V.mp3)
  Segment 48: 217027ms ~ 222427ms (file: 176-571-231-276-267-765-257-103-107-507-513-321-267-260-267-230-750-756-765-720-137-106-176-217-327-130-236-261-102-201-627-276-106-617-170-163-320-327-302-312-216-103-126-130-1_W.mp3)
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
> **Time**: 2026-04-06 08:47:40
> **AI**: gemini

---

## Response

AnalysisAnalysisGemini의 응답[
{
"segment": 1,
"transcript": "셀카 좋아해요 어 뭐야 뭐야 엄청 왔는데 광주에서 진짜 인기가 많네",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 210,
"audio_type": "speech",
"words": [
{"word": "셀카", "start_ms": 210, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "좋아해요", "start_ms": 850, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "어", "start_ms": 2100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "뭐야", "start_ms": 2450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "뭐야", "start_ms": 2900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "엄청", "start_ms": 3400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "왔는데", "start_ms": 3950, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "광주에서", "start_ms": 4800, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 5800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "인기가", "start_ms": 6350, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "많네", "start_ms": 7100, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 3,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 4,
"transcript": "역시 부자네요 고마워요 고마워요 야 이거 오 야 야 머리 이거 얘네가 산 거랑 똑같은 양이야",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "역시", "start_ms": 150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "부자네요", "start_ms": 700, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "고마워요", "start_ms": 1600, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "고마워요", "start_ms": 2400, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "야", "start_ms": 3300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이거", "start_ms": 3650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "오", "start_ms": 4100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "야", "start_ms": 4450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "야", "start_ms": 4800, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "머리", "start_ms": 5200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이거", "start_ms": 5750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "얘네가", "start_ms": 6300, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "산", "start_ms": 6950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "거랑", "start_ms": 7300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "똑같은", "start_ms": 7800, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "양이야", "start_ms": 8550, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 6,
"transcript": "아 너무 아이고 아이고 너무 감사하게 생각에 있어서 그리 만족스럽지 못할 것 같습니다 대신 앞서도 말했듯 이제 드디어 우리 KF-21의 개발이 완료되었고 양산을 시작했기 때문에",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "아", "start_ms": 120, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아이고", "start_ms": 900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "아이고", "start_ms": 1450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 2000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "감사하게", "start_ms": 2500, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "생각에", "start_ms": 3300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "있어서", "start_ms": 3850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "그리", "start_ms": 4400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "만족스럽지", "start_ms": 4800, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "못할", "start_ms": 5650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 6100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "같습니다", "start_ms": 6350, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "대신", "start_ms": 7100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "앞서도", "start_ms": 7550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "말했듯", "start_ms": 8100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 8700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "드디어", "start_ms": 9150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "우리", "start_ms": 9700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "KF-21의", "start_ms": 10100, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "개발이", "start_ms": 11050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "완료되었고", "start_ms": 11600, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "양산을", "start_ms": 12450, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "시작했기", "start_ms": 13050, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "때문에", "start_ms": 13750, "dur_ms": 550, "phase_lr_deg": 0.0}
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
"transcript": "와 이거 조금만 움직여도 머리 부딪히겠네 와 이게 제 키 아니면은 저 부분만 사용하는 걸로 해서 폭을 좁게 했어요 그래야지 공간이",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 230,
"audio_type": "speech",
"words": [
{"word": "와", "start_ms": 230, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이거", "start_ms": 580, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "조금만", "start_ms": 1030, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "움직여도", "start_ms": 1630, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "머리", "start_ms": 2380, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "부딪히겠네", "start_ms": 2880, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "와", "start_ms": 3780, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 4130, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "제", "start_ms": 4580, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "키", "start_ms": 4880, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "아니면은", "start_ms": 5180, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "저", "start_ms": 5880, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "부분만", "start_ms": 6230, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "사용하는", "start_ms": 6830, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "걸로", "start_ms": 7530, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "해서", "start_ms": 7980, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "폭을", "start_ms": 8480, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "좁게", "start_ms": 8980, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "했어요", "start_ms": 9480, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "그래야지", "start_ms": 10080, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "공간이", "start_ms": 10830, "dur_ms": 550, "phase_lr_deg": 0.0}
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
"transcript": "기술도 약합니다 그래서 엔비디아 구글 딥마인드 등이 들어오는 거죠 서로 얻을 건 얻으면 되는 겁니다 이런 관점에서 테슬라 옵티머스는 한국 및 중국 생산이 가능할까요 이 부분도 상당히 애매합니다 이게 어떻게 될 건지 테슬라의 전략으로 보면",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "기술도", "start_ms": 130, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "약합니다", "start_ms": 680, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 1380, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "엔비디아", "start_ms": 1930, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "구글", "start_ms": 2630, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "딥마인드", "start_ms": 3080, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "등이", "start_ms": 3880, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "들어오는", "start_ms": 4280, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "거죠", "start_ms": 4880, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "서로", "start_ms": 5230, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "얻을", "start_ms": 5680, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "건", "start_ms": 6080, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "얻으면", "start_ms": 6330, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "되는", "start_ms": 6830, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "겁니다", "start_ms": 7230, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 7730, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "관점에서", "start_ms": 8130, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "테슬라", "start_ms": 8880, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "옵티머스는", "start_ms": 9530, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 10430, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "및", "start_ms": 10880, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "중국", "start_ms": 11130, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "생산이", "start_ms": 11580, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "가능할까요", "start_ms": 12180, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 12980, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "부분도", "start_ms": 13230, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "상당히", "start_ms": 13780, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "애매합니다", "start_ms": 14380, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 15130, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 15580, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "될", "start_ms": 16130, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "건지", "start_ms": 16430, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "테슬라의", "start_ms": 16880, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "전략으로", "start_ms": 17630, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "보면", "start_ms": 18330, "dur_ms": 450, "phase_lr_deg": 0.0}
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
"transcript": "맛있는 것도 많이 먹고 그리고 옛날 마을도 구경할 수 있으니까 근데 아까 봤는데 아이 교신은 많아요 오늘 우리 그냥 이만큼",
"speaker": "speaker_5",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "맛있는", "start_ms": 250, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "것도", "start_ms": 850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 1300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "먹고", "start_ms": 1800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 2300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "옛날", "start_ms": 2850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "마을도", "start_ms": 3350, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "구경할", "start_ms": 3950, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 4550, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "있으니까", "start_ms": 4800, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 5550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아까", "start_ms": 6000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "봤는데", "start_ms": 6500, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "아이", "start_ms": 7150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "교신은", "start_ms": 7550, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "많아요", "start_ms": 8250, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 8850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "우리", "start_ms": 9350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 9800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이만큼", "start_ms": 10300, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 16,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 17,
"transcript": "가진 실질적인 유일한 압박 카드인 만큼 쉽게 놓지 않을 것으로 미국 정부 당국도 전망하고 있습니다 네 그런데 이란 관영 매체",
"speaker": "speaker_6",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "가진", "start_ms": 180, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "실질적인", "start_ms": 630, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "유일한", "start_ms": 1430, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "압박", "start_ms": 2030, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "카드인", "start_ms": 2480, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "만큼", "start_ms": 3080, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "쉽게", "start_ms": 3530, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "놓지", "start_ms": 4030, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "않을", "start_ms": 4480, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "것으로", "start_ms": 4880, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "미국", "start_ms": 5430, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "정부", "start_ms": 5880, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "당국도", "start_ms": 6330, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "전망하고", "start_ms": 6980, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 7780, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "네", "start_ms": 8480, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "그런데", "start_ms": 8780, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이란", "start_ms": 9330, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "관영", "start_ms": 9780, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "매체", "start_ms": 10230, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "언급을 한 것이었는데요 우리가 알던 손흥민 선수의 인성과 인터뷰 스킬 및 예의를 감안한다면 이 정도 대답은 극단적인",
"speaker": "speaker_7",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "언급을", "start_ms": 140, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 690, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "것이었는데요", "start_ms": 940, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "우리가", "start_ms": 1840, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "알던", "start_ms": 2390, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "손흥민", "start_ms": 2840, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "선수의", "start_ms": 3490, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "인성과", "start_ms": 4040, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "인터뷰", "start_ms": 4690, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "스킬", "start_ms": 5390, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "및", "start_ms": 5840, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "예의를", "start_ms": 6140, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "감안한다면", "start_ms": 6690, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 7590, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "정도", "start_ms": 7840, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "대답은", "start_ms": 8290, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "극단적인", "start_ms": 8940, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "고맙고 미안하고 막 이러죠 우리가 집을 지으면 정말 단열에 신경을 쓰고 창문도 그렇게 큰 거 필요 없다 통창 이런 거 나는 필요 없다 그러면서 이제 하나하나 이렇게 그냥 언제",
"speaker": "speaker_8",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "고맙고", "start_ms": 200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "미안하고", "start_ms": 850, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "막", "start_ms": 1600, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "이러죠", "start_ms": 1900, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "우리가", "start_ms": 2500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "집을", "start_ms": 3050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "지으면", "start_ms": 3500, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 4100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "단열에", "start_ms": 4600, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "신경을", "start_ms": 5250, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "쓰고", "start_ms": 5950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "창문도", "start_ms": 6400, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "그렇게", "start_ms": 7050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 7600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 7950, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "필요", "start_ms": 8200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "없다", "start_ms": 8650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "통창", "start_ms": 9150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 9700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 10150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "나는", "start_ms": 10400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "필요", "start_ms": 10850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "없다", "start_ms": 11300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "그러면서", "start_ms": 11800, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 12600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "하나하나", "start_ms": 13050, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 13900, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 14550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "언제", "start_ms": 15050, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 21,
"transcript": "유명하다고 들었는데 근데 안에 자리도 있어요 아 당연히 자리 있죠 아 그래요 앉아서 어 좀만 더 좀만 더",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "유명하다고", "start_ms": 250, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "들었는데", "start_ms": 1150, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 1950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "안에", "start_ms": 2400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "자리도", "start_ms": 2900, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 3550, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 4150, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "당연히", "start_ms": 4450, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "자리", "start_ms": 5100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "있죠", "start_ms": 5550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 6000, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "그래요", "start_ms": 6300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "앉아서", "start_ms": 6850, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "어", "start_ms": 7550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "좀만", "start_ms": 7900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 8400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "좀만", "start_ms": 8750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 9250, "dur_ms": 350, "phase_lr_deg": 0.0}
]
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
"transcript": "생각하지 않아 업무에 지장이 없다고 보도했습니다 지금까지 오만 무스카트에서 전해드렸습니다 이란 관영 매체는 미국이 우방국을 통해 48",
"speaker": "speaker_6",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 220,
"audio_type": "speech",
"words": [
{"word": "생각하지", "start_ms": 220, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "않아", "start_ms": 970, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "업무에", "start_ms": 1420, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "지장이", "start_ms": 2020, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "없다고", "start_ms": 2620, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "보도했습니다", "start_ms": 3220, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "지금까지", "start_ms": 4120, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "오만", "start_ms": 4870, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "무스카트에서", "start_ms": 5320, "dur_ms": 950, "phase_lr_deg": 0.0},
{"word": "전해드렸습니다", "start_ms": 6320, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "이란", "start_ms": 7370, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "관영", "start_ms": 7870, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "매체는", "start_ms": 8370, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "미국이", "start_ms": 8970, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "우방국을", "start_ms": 9570, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "통해", "start_ms": 10370, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "48", "start_ms": 10870, "dur_ms": 600, "phase_lr_deg": 0.0}
]
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
"transcript": "이거 생선이다 어 맛있다 음 음 이거 맛있어 진짜 생선 아니에요 그래 응 뭔가 달달해",
"speaker": "speaker_5",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 280,
"audio_type": "speech",
"words": [
{"word": "이거", "start_ms": 280, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "생선이다", "start_ms": 730, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "어", "start_ms": 1530, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "맛있다", "start_ms": 1830, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "음", "start_ms": 2480, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "음", "start_ms": 2830, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이거", "start_ms": 3180, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "맛있어", "start_ms": 3630, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 4280, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "생선", "start_ms": 4830, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "아니에요", "start_ms": 5330, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "그래", "start_ms": 6080, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "응", "start_ms": 6580, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "뭔가", "start_ms": 6880, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "달달해", "start_ms": 7380, "dur_ms": 650, "phase_lr_deg": 0.0}
]
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
"transcript": "지도자로 선출된 뒤 종적을 감춰 폭격으로 큰 부상을 입고 러시아에서 수술까지 받았다는 소문이 돌아",
"speaker": "speaker_6",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "지도자로", "start_ms": 150, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "선출된", "start_ms": 900, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "뒤", "start_ms": 1500, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "종적을", "start_ms": 1800, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "감춰", "start_ms": 2400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "폭격으로", "start_ms": 2900, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 3600, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "부상을", "start_ms": 3900, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "입고", "start_ms": 4500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "러시아에서", "start_ms": 4950, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "수술까지", "start_ms": 5850, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "받았다는", "start_ms": 6650, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "소문이", "start_ms": 7400, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "돌아", "start_ms": 8000, "dur_ms": 450, "phase_lr_deg": 0.0}
]
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
"transcript": "말씀드린 문제들이 불 보듯 뻔하기 때문에 10년 전 일본이 그랬던 것처럼 인증 표시제를 통해 품질을 관리해야 한다는 목소리가 높아졌던 건데요 실제로 몇 년 전엔 125",
"speaker": "speaker_7",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "말씀드린", "start_ms": 180, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "문제들이", "start_ms": 880, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "불", "start_ms": 1580, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "보듯", "start_ms": 1830, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "뻔하기", "start_ms": 2280, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "때문에", "start_ms": 2880, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "10년", "start_ms": 3430, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "전", "start_ms": 3880, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "일본이", "start_ms": 4180, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "그랬던", "start_ms": 4730, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "것처럼", "start_ms": 5280, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "인증", "start_ms": 5880, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "표시제를", "start_ms": 6330, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "통해", "start_ms": 7080, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "품질을", "start_ms": 7530, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "관리해야", "start_ms": 8130, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "한다는", "start_ms": 8880, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "목소리가", "start_ms": 9480, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "높아졌던", "start_ms": 10180, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "건데요", "start_ms": 10930, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "실제로", "start_ms": 11530, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "몇", "start_ms": 12180, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "년", "start_ms": 12480, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "전엔", "start_ms": 12730, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "125", "start_ms": 13230, "dur_ms": 700, "phase_lr_deg": 0.0}
]
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
"transcript": "그 건축비는 얼마 들었을까요 대략 2억 정도 정리해 놓은 걸로는 1억 6천까지는 정리돼 있는데 그 이후",
"speaker": "speaker_8",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "그", "start_ms": 200, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "건축비는", "start_ms": 450, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "얼마", "start_ms": 1200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "들었을까요", "start_ms": 1700, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "대략", "start_ms": 2600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "2억", "start_ms": 3150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "정도", "start_ms": 3650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "정리해", "start_ms": 4200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "놓은", "start_ms": 4850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "걸로는", "start_ms": 5300, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "1억", "start_ms": 5900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "6천까지는", "start_ms": 6350, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "정리돼", "start_ms": 7300, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "있는데", "start_ms": 8000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 8650, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "이후", "start_ms": 8900, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "But the threats from North Korea and China increasing this posture of a consistent policy of deterrence is very important To that end your commitment to",
"speaker": "speaker_9",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "But", "start_ms": 120, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "threats", "start_ms": 600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "from", "start_ms": 1150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "North", "start_ms": 1500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "Korea", "start_ms": 1950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "and", "start_ms": 2450, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "China", "start_ms": 2750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "increasing", "start_ms": 3300, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "this", "start_ms": 4100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "posture", "start_ms": 4450, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "of", "start_ms": 5050, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 5300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "consistent", "start_ms": 5500, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "policy", "start_ms": 6250, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "of", "start_ms": 6800, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "deterrence", "start_ms": 7050, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 7900, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "very", "start_ms": 8150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "important", "start_ms": 8600, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "To", "start_ms": 9350, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "that", "start_ms": 9600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"

---
*Generated by WKAppBot ask gemini — 2026-04-06 08:47:40*
