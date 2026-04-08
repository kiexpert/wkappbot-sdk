# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=30488  time=08:43:23  uptime=18481s  mem=145MB  system_processes=407"}],"isError":false}}]
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
> **Time**: 2026-04-06 08:45:28
> **AI**: gemini

---

## Response

AnalysisAnalysisGemini의 응답TITLE: Audio Transcription & Analysis

{
"segment": 1,
"transcript": "AI 역량이 경쟁력을 좌우하는 국면으로 이동할 가능성이 크다는 거죠",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "AI", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "역량이", "start_ms": 600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "경쟁력을", "start_ms": 1150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "좌우하는", "start_ms": 1800, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "국면으로", "start_ms": 2500, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "이동할", "start_ms": 3150, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "가능성이", "start_ms": 3750, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "크다는", "start_ms": 4450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "거죠", "start_ms": 4950, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "로봇의 하드웨어 형태나 기구 설계보다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "로봇의", "start_ms": 100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "하드웨어", "start_ms": 600, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "형태나", "start_ms": 1200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "기구", "start_ms": 1700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "설계보다", "start_ms": 2100, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "현실 세계를 이해하고 학습하며 적응할 수 있는가",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "현실", "start_ms": 80, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "세계를", "start_ms": 530, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이해하고", "start_ms": 1030, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "학습하며", "start_ms": 1680, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "적응할", "start_ms": 2380, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 2930, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있는가", "start_ms": 3130, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "전문 투자자도 아니고 투자를 공부한 사람이 아니기 때문에 어떤 그런 지표와 어려운 영어 용어 같은 거 잘 이해하기가 힘들어요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "전문", "start_ms": 200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "투자자도", "start_ms": 600, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "아니고", "start_ms": 1200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "투자를", "start_ms": 1700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "공부한", "start_ms": 2150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "사람이", "start_ms": 2650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아니기", "start_ms": 3100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "때문에", "start_ms": 3600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 4100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그런", "start_ms": 4500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "지표와", "start_ms": 4850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "어려운", "start_ms": 5350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "영어", "start_ms": 5800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "용어", "start_ms": 6200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "같은", "start_ms": 6600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 6950, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 7150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "이해하기가", "start_ms": 7400, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "힘들어요", "start_ms": 8050, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "근데 그거를 쉽게 설명해주고 그 차트를",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "근데", "start_ms": 50, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "그거를", "start_ms": 400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "쉽게", "start_ms": 850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "설명해주고", "start_ms": 1250, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 2000, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "차트를", "start_ms": 2200, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "끈기가 진짜 대단하시다 누가 이기나 보자 하는 세화라 네가 이기나 내가 이기나 보자 뭐 거의 그 수준인 거 같은데요",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "끈기가", "start_ms": 150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "대단하시다", "start_ms": 1050, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "누가", "start_ms": 1900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이기나", "start_ms": 2250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "보자", "start_ms": 2750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 3150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "세화라", "start_ms": 3500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "네가", "start_ms": 4050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이기나", "start_ms": 4450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "내가", "start_ms": 4950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이기나", "start_ms": 5350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "보자", "start_ms": 5850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 6250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "거의", "start_ms": 6450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 6850, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "수준인", "start_ms": 7050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 7550, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "같은데요", "start_ms": 7750, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "주변에서 집 짓다 망했다는 소리",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "주변에서", "start_ms": 120, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "집", "start_ms": 670, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "짓다", "start_ms": 920, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "망했다는", "start_ms": 1320, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "소리", "start_ms": 1970, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "(music)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 9,
"transcript": "최근 대만 방송을 통해 공개된 한국 방위산업 관련 소식이 예상보다 훨씬 강한 파장을 일으키면서 대만 사회 전체가 큰 요동을 치고 있기 때문입니다 무엇보다 충격",
"speaker": "speaker_5",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "최근", "start_ms": 180, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "대만", "start_ms": 630, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "방송을", "start_ms": 1030, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "통해", "start_ms": 1580, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "공개된", "start_ms": 1980, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 2480, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "방위산업", "start_ms": 2880, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "관련", "start_ms": 3580, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "소식이", "start_ms": 4030, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "예상보다", "start_ms": 4530, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "훨씬", "start_ms": 5180, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "강한", "start_ms": 5630, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "파장을", "start_ms": 6030, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "일으키면서", "start_ms": 6530, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "대만", "start_ms": 7330, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "사회", "start_ms": 7730, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "전체가", "start_ms": 8130, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 8630, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "요동을", "start_ms": 8880, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "치고", "start_ms": 9380, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있기", "start_ms": 9780, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "때문입니다", "start_ms": 10180, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "무엇보다", "start_ms": 10980, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "충격", "start_ms": 11630, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "대비할 수 있다 우려까지 나옵니다 아주 오래된 사드 같은 방공 요격 미사일 일부가 차출되는 정황이 포착되는 등 이란 전쟁으로 주한미군의 전략적 유연성",
"speaker": "speaker_6",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "대비할", "start_ms": 120, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 620, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있다", "start_ms": 820, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "우려까지", "start_ms": 1220, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "나옵니다", "start_ms": 1870, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 2500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "오래된", "start_ms": 2900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "사드", "start_ms": 3450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "같은", "start_ms": 3850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "방공", "start_ms": 4300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "요격", "start_ms": 4700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "미사일", "start_ms": 5100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "일부가", "start_ms": 5650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "차출되는", "start_ms": 6150, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "정황이", "start_ms": 6850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "포착되는", "start_ms": 7400, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "등", "start_ms": 8100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "이란", "start_ms": 8350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "전쟁으로", "start_ms": 8750, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "주한미군의", "start_ms": 9450, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "전략적", "start_ms": 10250, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "유연성", "start_ms": 10900, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "(music)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 12,
"transcript": "먼저 말을 걸어오는 AI",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "먼저", "start_ms": 100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "말을", "start_ms": 550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "걸어오는", "start_ms": 900, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "AI", "start_ms": 1500, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "치킨도 먹고 우와 너무 괜찮은데 그래서 되게 큰 축제예요 그때는 축제 기간 동안은 맥주가 단돈 천 원에서 이천 원 천 원 응 천 원 일 유로보다 적은 거지 진짜 응",
"speaker": "mixed_speakers",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "치킨도", "start_ms": 150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "먹고", "start_ms": 650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "우와", "start_ms": 1050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 1450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "괜찮은데", "start_ms": 1850, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 2550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "되게", "start_ms": 3050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 3450, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "축제예요", "start_ms": 3700, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "그때는", "start_ms": 4300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "축제", "start_ms": 4800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "기간", "start_ms": 5200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "동안은", "start_ms": 5600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "맥주가", "start_ms": 6100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "단돈", "start_ms": 6650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "천", "start_ms": 7050, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "원에서", "start_ms": 7300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이천", "start_ms": 7800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "원", "start_ms": 8200, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "천", "start_ms": 8450, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "원", "start_ms": 8700, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "응", "start_ms": 8950, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "천", "start_ms": 9150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "원", "start_ms": 9400, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "일", "start_ms": 9650, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "유로보다", "start_ms": 9850, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "적은", "start_ms": 10550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "거지", "start_ms": 10950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 11350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "응", "start_ms": 11750, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "현대차가 인수했을까요 그 당시 보면 보스턴 다이내믹스를 보던 기업들이 엄청 유명한 기업들이었죠 근데 갑자기 현대차가 인수했다라는 겁니다 미국 정부에서 봤을 때도 현대차",
"speaker": "speaker_7",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "현대차가", "start_ms": 120, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "인수했을까요", "start_ms": 670, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 1470, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "당시", "start_ms": 1670, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "보면", "start_ms": 2070, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "보스턴", "start_ms": 2470, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "다이내믹스를", "start_ms": 2970, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "보던", "start_ms": 3820, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "기업들이", "start_ms": 4220, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "엄청", "start_ms": 4820, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "유명한", "start_ms": 5220, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "기업들이었죠", "start_ms": 5720, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 6570, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "갑자기", "start_ms": 6970, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "현대차가", "start_ms": 7470, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "인수했다라는", "start_ms": 8070, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "겁니다", "start_ms": 8870, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "미국", "start_ms": 9320, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "정부에서", "start_ms": 9720, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "봤을", "start_ms": 10320, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "때도", "start_ms": 10720, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "현대차", "start_ms": 11070, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "비춰야 할까 AI 너머에 있는 인간 우리 자신을 들여다볼 때다",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "비춰야", "start_ms": 150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "할까", "start_ms": 650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "AI", "start_ms": 1100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "너머에", "start_ms": 1550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 2050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "인간", "start_ms": 2450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "우리", "start_ms": 2900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "자신을", "start_ms": 3300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "들여다볼", "start_ms": 3800, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "때다", "start_ms": 4400, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 17,
"transcript": "이란이 타국 항구로 인도적 물품 특히 생필품과 사료 등을 싣고 오는 선박의 호르무즈 해협 통과를 허용했다는 공문이 이란 통신사가 공개했습니다 아라그치 외교",
"speaker": "speaker_8",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "이란이", "start_ms": 180, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "타국", "start_ms": 680, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "항구로", "start_ms": 1080, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "인도적", "start_ms": 1580, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "물품", "start_ms": 2080, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "특히", "start_ms": 2480, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "생필품과", "start_ms": 2930, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "사료", "start_ms": 3580, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "등을", "start_ms": 3980, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "싣고", "start_ms": 4380, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "오는", "start_ms": 4780, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "선박의", "start_ms": 5130, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "호르무즈", "start_ms": 5630, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "해협", "start_ms": 6230, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "통과를", "start_ms": 6630, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "허용했다는", "start_ms": 7130, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "공문이", "start_ms": 7880, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이란", "start_ms": 8380, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "통신사가", "start_ms": 8780, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "공개했습니다", "start_ms": 9380, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "아라그치", "start_ms": 10180, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "외교", "start_ms": 10780, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "직접 만들었답니다 비닐 깔고 그다음 철근 양생하고 이런 모든 과정들을 가족들이 같이 했는데 저희는 이제 인건비가 안 들어가잖아요 그러면 이곳을 지을 때 재료는 아끼지 말자 그래서",
"speaker": "mixed_speakers",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "직접", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "만들었답니다", "start_ms": 600, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "비닐", "start_ms": 1400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "깔고", "start_ms": 1800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그다음", "start_ms": 2200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "철근", "start_ms": 2700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "양생하고", "start_ms": 3100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 3750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "모든", "start_ms": 4150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "과정들을", "start_ms": 4550, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "가족들이", "start_ms": 5200, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 5800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "했는데", "start_ms": 6200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "저희는", "start_ms": 6700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 7200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "인건비가", "start_ms": 7550, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 8150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "들어가잖아요", "start_ms": 8350, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "그러면", "start_ms": 9150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이곳을", "start_ms": 9650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "지을", "start_ms": 10150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 10550, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "재료는", "start_ms": 10750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "아끼지", "start_ms": 11250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "말자", "start_ms": 11750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 12150, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "60평 대저택 부모님들에게 방 하나씩 주고도 남을 정도로 널찍합니다 중문부터가 이 집의 진짜 모습",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "60평", "start_ms": 120, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "대저택", "start_ms": 570, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "부모님들에게", "start_ms": 1120, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "방", "start_ms": 1970, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "하나씩", "start_ms": 2220, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "주고도", "start_ms": 2770, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "남을", "start_ms": 3270, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "정도로", "start_ms": 3670, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "널찍합니다", "start_ms": 4170, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "중문부터가", "start_ms": 4970, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 5720, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "집의", "start_ms": 5920, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 6270, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "모습", "start_ms": 6670, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "(music)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 21,
"transcript": "150평이나 되는데 100만 원밖에 없었다니 용감한 건가요 무모한 건가요",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "150평이나", "start_ms": 100, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "되는데", "start_ms": 850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "100만", "start_ms": 1350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "원밖에", "start_ms": 1800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "없었다니", "start_ms": 2300, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "용감한", "start_ms": 3000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "건가요", "start_ms": 3500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "무모한", "start_ms": 3950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "건가요", "start_ms": 4450, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "결혼식을 올렸습니다",
"speaker": "speaker_4",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "결혼식을", "start_ms": 50, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "올렸습니다", "start_ms": 650, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "(music)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 24,
"transcript": "확정되었습니다 그래서 이번 승리는 LAFC의 서부 리그 1위 자리를 더욱 견고히 다졌는데 이번 경기",
"speaker": "speaker_4",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "확정되었습니다", "start_ms": 120, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 1020, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이번", "start_ms": 1520, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "승리는", "start_ms": 1920, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "LAFC의", "start_ms": 2420, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "서부", "start_ms": 3070, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "리그", "start_ms": 3470, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "1위", "start_ms": 3870, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "자리를", "start_ms": 4220, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "더욱", "start_ms": 4720, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "견고히", "start_ms": 5120, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "다졌는데", "start_ms": 5620, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "이번", "start_ms": 6320, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "경기", "start_ms": 6720, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "그 건축비는 얼마 들었을까요 대략 2억 정도 정리해 놓은 걸로는 1억 6천까지는 정리가 돼 있는데 그 이후",
"speaker": "mixed_speakers",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "그", "start_ms": 150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "건축비는", "start_ms": 350, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "얼마", "start_ms": 950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "들었을까요", "start_ms": 1350, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "대략", "start_ms": 2100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "2억", "start_ms": 2550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "정도", "start_ms": 2950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "정리해", "start_ms": 3350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "놓은", "start_ms": 3850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "걸로는", "start_ms": 4250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "1억", "start_ms": 4750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "6천까지는", "start_ms": 5150, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "정리가", "start_ms": 5900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "돼", "start_ms": 6400, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "있는데", "start_ms": 6650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 7150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이후", "start_ms": 7350, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "언급을 한 것이었는데요 우리가 알던 손흥민 선수의 인성과 인터뷰 스킬 및 예의를 감안한다면 이 정도 대답은 극단적인",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "언급을", "start_ms": 120, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 620, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "것이었는데요", "start_ms": 870, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "우리가", "start_ms": 1670, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "알던", "start_ms": 2120, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "손흥민", "start_ms": 2520, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "선수의", "start_ms": 3020, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "인성과", "start_ms": 3520, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "인터뷰", "start_ms": 4020, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "스킬", "start_ms": 4520, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "및", "start_ms": 4920, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "예의를", "start_ms": 5120, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "감안한다면", "start_ms": 5620, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 6420, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "정도", "start_ms": 6620, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "대답은", "start_ms": 7020, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "극단적인", "start_ms": 7520, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "오늘도 잘 먹었습니다 Good morning Chef as you know most of my favorite restaurants",
"speaker": "mixed_speakers",
"confidence": 0.93,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "오늘도", "start_ms": 100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 600, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "먹었습니다", "start_ms": 850, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "Good", "start_ms": 1600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "morning", "start_ms": 2000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "Chef", "start_ms": 2500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "as", "start_ms": 3000, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 3250, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "know", "start_ms": 3500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "most", "start_ms": 3950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "of", "start_ms": 4400, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "my", "start_ms": 4600, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "favorite", "start_ms": 4850, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "restaurants", "start_ms": 5500, "dur_ms": 750, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "관련해서는 한 요 얘기를 좀 해보도록 하겠습니다 일단 제가 특사경 수사지휘 권한을 왜 지적을 했느냐라는 것부터 설명을 드릴게요 특사경 수사지휘 권한은 사실 중요하지 않았습니다 검찰 입장에서 볼 때는 검찰이 특사경과 짬짬이를 해서 사건을 만들거나 특사경 수사 영역에",
"speaker": "speaker_9",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "관련해서는", "start_ms": 180, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 880, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "요", "start_ms": 1130, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "얘기를", "start_ms": 1330, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 1780, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "해보도록", "start_ms": 2030, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "하겠습니다", "start_ms": 2630, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "일단", "start_ms": 3330, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 3730, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "특사경", "start_ms": 4130, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "수사지휘", "start_ms": 4630, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "권한을", "start_ms": 5280, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "왜", "start_ms": 5780, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "지적을", "start_ms": 6030, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "했느냐라는", "start_ms": 6530, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "것부터", "start_ms": 7330, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "설명을", "start_ms": 7830, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "드릴게요", "start_ms": 8330, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "특사경", "start_ms": 8930, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "수사지휘", "start_ms": 9430, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "권한은", "start_ms": 10080, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "사실", "start_ms": 10580, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "중요하지", "start_ms": 10980, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "않았습니다", "start_ms": 11580, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "검찰", "start_ms": 12280, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "입장에서", "start_ms": 12680, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "볼", "start_ms": 13280, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "때는", "start_ms": 13530, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "검찰이", "start_ms": 13880, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "특사경과", "start_ms": 14380, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "짬짬이를", "start_ms": 15030, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "해서", "start_ms": 15680, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "사건을", "start_ms": 16080, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "만들거나", "start_ms": 16580, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "특사경", "start_ms": 17230, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "수사", "start_ms": 17730, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "영역에", "start_ms": 18130, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "수익을 냈고 그리고 디플러스 일을 저해 시장 급락에도 오히려 좀 강세를 보였던 종목이라서 오버나이트",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "수익을", "start_ms": 120, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "냈고", "start_ms": 620, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 1020, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "디플러스", "start_ms": 1470, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "일을", "start_ms": 2070, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "저해", "start_ms": 2420, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "시장", "start_ms": 2820, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word":

---
*Generated by WKAppBot ask gemini — 2026-04-06 08:45:28*
