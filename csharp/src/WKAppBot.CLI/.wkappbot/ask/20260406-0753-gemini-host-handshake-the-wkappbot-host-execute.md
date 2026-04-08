# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=23176  time=07:51:13  uptime=14541s  mem=418MB  system_processes=410"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_075057.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 5148ms (file: 170-751-671-310-260-132-103-257-231-524-527-132-321-230-321-702-765-107-752-527-257-542-527-132-762-206-201-206-201-260-362-261-326-706-715-370-725-756-571-715-735-756-756-752-5_W.mp3)
  Segment 2: 6148ms ~ 6724ms (file: 576-571-571-571-517 517-574-571-571-571-571-510-571_W.mp3)
  Segment 3: 7724ms ~ 8300ms (file: 236-157-725-153-326-572-213-125-671-763-726-306-162-570-152-376-713-576-507-756-725-726-705 315-562-312-537-570_V.mp3)
  Segment 4: 9300ms ~ 9876ms (file: 762-253-507-705-312-672 765-520-123-251-705-127-137-265-756-705-672-527-251-215-362-162-267-613-506-625-312-167-576-765-507-570-326-531-526-765-132-267-762-620-623-213-325-213-5_V.mp3)
  Segment 5: 10876ms ~ 11452ms (file: 513-153-157-513-153-517-517-517 153-153-513-513-153-150-513-150-157-157-517-517-517-572-571-572-573_W.mp3)
  Segment 6: 12452ms ~ 20264ms (file: 123-627-627-756-235-237-132-267-612-623-123-213-721-752-236-126-231-102-136-103-231-236-132-231-213-765-576-102-102-236-612-761-715-160-672-103-236-231-231-235-236-271-137-216-3_W.mp3)
  Segment 7: 21264ms ~ 31056ms (file: 712-327-723-231-123-723-213-213-127-327-231-310-103-213-130-270-526-312-103-257-216-261-261-720-201-107-236-260-620-206-206-267-267-260-762-230-236-260-627-301-321-271-623-162-3_W.mp3)
  Segment 8: 32056ms ~ 32632ms (file: 257-725-275-235-560-267-213-126-157-563-367-675-617-376-675-367-673-657-675-574-547_W.mp3)
  Segment 9: 33632ms ~ 34208ms (file: 573-576-572-576-576-571-576-573-572-573-572-571-573-752 576-573-576-576-576-571-576-572-572-571-175-571-157-175-517-751-317-573-513-571-157-157-751-305-375-175 713-712-715-751-3_W.mp3)
  Segment 10: 35208ms ~ 42084ms (file: 276-627-672-276-267-213-137-130-316-312-102-301-310-301-316-203-231-275-127-132-235-123-321-375-573-752-572-157-107-213-231-273-271-273-263-237-236-576-172-572-215-137-130-271-1_W.mp3)
  Segment 11: 43084ms ~ 43660ms (file: 627-623-672-627-672-627-632-627-652-256-627 657-652-652-625-763-672-672-627-625-672-625-267-567-657-657 765-657-675-625_W.mp3)
  Segment 12: 44660ms ~ 50816ms (file: 201-201-261-261-207-201-260-671-176-214-201-275-217-217-231-237-267-276-217-203-201-230-210-267-213-213-103-230-103-123-320-130-316-132-123-201-201-210-103-130-132-102-321-317-1_W.mp3)
  Segment 13: 51816ms ~ 52392ms (file: 321-123-167-312-325-231-206-275-126-263-627-237-315-263-165-576-261-721-756-671-275-261-327-351-256-726-752-267-317-307-735_V.mp3)
  Segment 14: 53392ms ~ 53968ms (file: 153-103-517-531-517-105-105-103-175-150-153 157-153-135-153-513-513-150-512-312-175-157-157-175-157-137-315-312-523-571-571-517_W.mp3)
  Segment 15: 54968ms ~ 55544ms (file: 750-756-753-753-735 735-753-735-735-750-756-756-375-735-312-130-765-761-130-135 163 275_W.mp3)
  Segment 16: 56544ms ~ 65364ms (file: 167-123-765-326-103-207-726-132-207-126-312-312-302-507-206-356-320-310-132-120-203-230-236-523-102-125-567-231-236-106-310-320-165-105-107-635-306-103-230-623-657-320-312-230-2_W.mp3)
  Segment 17: 66364ms ~ 66940ms (file: 137-103-107-103-157-157-157-157-157-517-517-517-571-517-571-571-715-751-705-705-753-715-705-107-517-157-157-517-517-571-517-756-751-571-157-150-157-157-517-517-571-571-517-567-5_V.mp3)
  Segment 18: 67940ms ~ 74168ms (file: 123-673-267-231-721-762-271-102-712-761-172-123-267-263-236-327-573-257-236-213-271-217-216-213-216-210-206-127-270-720-217-173-572-730-130-321-273-276-123-120-137-312-217-127-1_W.mp3)
  Segment 19: 75168ms ~ 81684ms (file: 260-261-312-123-310-327-130-571-107-157-312-302-312-137-107-132-321-132-312-107-136-173-231-213-271-103-321-301-632-103-130-312-735-237-120-103-301-107-105-736-230-207-271-132-1_V_stt_for_those_of_us.mp3)
  Segment 20: 82684ms ~ 83260ms (file: 715-710-715-751-701-105-157-175-170-175-132-312-132-126-123-213-120-132-126-162-162-102-751-751-517-157-157-105-105-715-705-705-753-701-750-710-120-132-210-120-120-507-571-750-7_W.mp3)
  Segment 21: 84260ms ~ 84836ms (file: 506-756-756-576-567-765-765-675-675-615-675-617-657-671-637-761-673-675-176-673-617-763-123-316-673-756-376-703-376-671-316-651-617-623-270-123-625-217-623-312-312-153-136-567-2_W.mp3)
  Segment 22: 85836ms ~ 86412ms (file: 574-361-316-361-613-631-613-361-637-163-631-316-673-675-673-675-675-671-671-617-675-615-156-132-317-132 231-321-123-302-326-362-356-365-631-623-326-306-361-635-675-635-675-765-1_W.mp3)
  Segment 23: 87412ms ~ 87988ms (file: 513-571-175-157-157 153-517-157-753-517-571-517-157-507-153-153-317-753-152-173-507-521-715-715-105-501-751-103-710-137-105-571 501-570-571-153-153-157-105-510-517-517-105-510-5_V.mp3)
  Segment 24: 88988ms ~ 96980ms (file: 317-301-231-132-312-237-103-213-102-321-750-207-320-761-321-270-276-732-210-725-123-327-231-312-731-327-237-310-510-321-201-230-132-127-126-312-103-321-310-102-312-317-137-136-1_V.mp3)
  Segment 25: 97980ms ~ 98556ms (file: 132 102-120-126-127-157-153-517-105-107-701-715-703-710-105-170-705-751-105-701-705-751-705-710-175-710-170-175-715-127-213-123-120-120-120-126-162-162-160-162-106-705-705-571-7_V.mp3)
  Segment 26: 99556ms ~ 106864ms (file: 213-621-321-263-162-261-216-120-162-572-251-132-267-256-302-263-726-162-130-617-627-265-267-206-276-762-263-310-312-762-276-216-267-257-321-213-102-102-620-231-263-123-302-130-1_W.mp3)
  Segment 27: 107864ms ~ 108440ms (file: 123-126 136-621-625-627-756-706 261-520-135-627-621-261-132-325-317-325-127-731-376-372-275-271-713-523-263-312-126-157_V.mp3)
  Segment 28: 109440ms ~ 110016ms (file: 136-765-756-371-673-735-736-673-756-736-376-735-765-756_W.mp3)
  Segment 29: 111016ms ~ 111592ms (file: 563-576-756-573-325-263-326-576-576-756-756-572-372-576-756-576-175-751-651-654-512-451-562-562-512-567-765-752-762-765-752-751-576-756-756-574-574-576-573-574-754-547-564-572-5_W.mp3)
  Segment 30: 112592ms ~ 113168ms (file: 574-574-574-571-517-571-571-574-574-574-574 574-574-574-574-574-574-574-574-574-571-517-157-531-157 574-574-157-517-517-571-571-571-571-157-573-571 517-571-517-571-571-571-571-5_W.mp3)
  Segment 31: 114168ms ~ 114744ms (file: 574-571-263-167-216-231-261-576-152-716-657-275-725-206-271-216-261-126-251-215-127-231-130-261-123-562-217-132-321-321-213-217-123-125-215-570-751-271-562-175_V.mp3)
  Segment 32: 115744ms ~ 116320ms (file: 672-357-753-175-175-765-765-107-537-352-652 672_W.mp3)
  Segment 33: 117320ms ~ 117896ms (file: 576-567-573-576-567-756-673-675-765-765-756-756-765-361-617-762-713-156-276-712-721-756-751-752-765-765-752-725-752-715-627-765-763-713-361-637-372-317-136-761-167-163-675-756-7_V.mp3)
  Segment 34: 118896ms ~ 127320ms (file: 260-321-310-301-312-132-136-103-320-103-103-134-312-143-170-103-130-231-103-130-103-152-312-135-215-163-163-163-160-632-136-236-132-120-276-673-712-732-732-732-103-102-106-135-1_V.mp3)
  Segment 35: 128320ms ~ 136672ms (file: 572-253-275-203-572-570-602-265-260-326-671-273-203-320-103-750-576-576-627-231-310-317-231-231-127-321-263-632-502-206-126-263-257-231-261-326-312-326-576-627-125-236-523-362-5_W.mp3)
  Segment 36: 137672ms ~ 138248ms (file: 641-163 163-173-617-625 621-627-652-103-163-163-217-326-756-675-602-613-106-607-602-632-613-621-132 301_V.mp3)
  Segment 37: 139248ms ~ 149688ms (file: 136-137-137-123-230-162-236-102-176-260-261-301-260-163-261-312-310-106-201-263-260-260-143-132-130-310-201-236-261-132-260-326-163-132-310-103-154-501-567-165-103-106-156-106-5_W.mp3)
  Segment 38: 150688ms ~ 159220ms (file: 276-263-210-236-327-537-721-173-120-127-267-326-362-372-671-103-231-267-263-351-275-276-267-267-236-623-103-310-310-753-756-326-106-617-103-236-327-167-316-312-362-263-231-230-2_W.mp3)
  Segment 39: 160220ms ~ 160796ms (file: 167-165-265-650-163-102-126-160-651-125-120-120-126-567-651-107-107-123-107-107-150-170-170-157-175-716-715-715-715-175-715-165-576-576-165-567-657-156-165-657-571-157-657-576-5_W.mp3)
  Segment 40: 161796ms ~ 162372ms (file: 571-752-571-751-571-750-572-573-751-756 576-756-572-756-571-571-574-571-576-751-576-573-570-751-571-572-572-572-572 751-751-753-750-753-751-750 572-752-576-753-751-571-572-752-7_W.mp3)
  Segment 41: 163372ms ~ 163948ms (file: 756-576-756-562-576-562-652-526-532-537-564-257-576-526-572-523-532-516-653-526-562-657-562-526-526-527-563-562-532-562-521-352-536-562-526-563-526-563-527-536-562-652-657-526-5_V.mp3)
  Segment 42: 164948ms ~ 173372ms (file: 302-276-310-732-123-103-167-107-163-263-726-312-236-276-237-276-762-725-720-321-273-176-107-132-537-103-230-236-675-675-106-261-267-726-706-716-327-327-320-273-236-607-607-261-6_W.mp3)
  Segment 43: 174372ms ~ 174948ms (file: 173-176-170-173-173-137-175-137 105-150-571-571-751-173-175-731-173-173-173 157-150-517-157-175-517-571-751-157-751-751-571-571 571-517-571-501-571-570-571-517_W.mp3)
  Segment 44: 175948ms ~ 176524ms (file: 571-517-107-751-173-375-573-375-753-157-157-570-175-570-175-720-751-712-750-127-752-713-713-716-710-752 751-715-175-175-756-751_V.mp3)
  Segment 45: 177524ms ~ 186884ms (file: 236-267-257-263-267-263-321-237-310-130-132-102-753-231-321-102-231-236-213-213-130-103-127-574-502-263-325-235-231-230-230-260-312-263-256-562-256-267-260-263-321-263-236-231-3_W.mp3)
  Segment 46: 187884ms ~ 195552ms (file: 630-326-263-216-712-570-673-327-126-173-237-321-321-326-326-203-203-230-263-217-760-206-276-726-320-267-267-267-320-372-310-236-236-103-267-236-230-276-327-573-523-123-173-217-1_W.mp3)
  Segment 47: 196552ms ~ 206164ms (file: 132-267-261-103-763-572-132-517-135-260-752-756-103-203-675-236-210-312-310-201-203-310-107-157-176-106-167-672-765-132-617-156-573-726-102-230-725-701-726-756-175-162-761-276-2_W.mp3)
  Segment 48: 207164ms ~ 207740ms (file: 751-571 517-571-571-571-571-570-157-571-750-107-751-715-105-750-315-715-750-756 517-571-574-572-574-574-571-571-571-571-571-157-175-157-570-175-573-571-517-751-753-751-175-573 5_W.mp3)
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
> **Time**: 2026-04-06 07:53:18
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "한마디로 스스로 판단하고 스스로 행동하는 AI다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "한마디로", "start_ms": 100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "스스로", "start_ms": 750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "판단하고", "start_ms": 1300, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "스스로", "start_ms": 2000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "행동하는", "start_ms": 2550, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "AI다", "start_ms": 3200, "dur_ms": 500, "phase_lr_deg": 0.0}
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
"transcript": "이스라엘은 장기전을 대비해 무기를 아껴 쓰는 방향으로 전략을 바꿀 수밖에 없었던 것입니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "이스라엘은", "start_ms": 50, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "장기전을", "start_ms": 700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "대비해", "start_ms": 1250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "무기를", "start_ms": 1700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아껴", "start_ms": 2150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "쓰는", "start_ms": 2500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "방향으로", "start_ms": 2850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "전략을", "start_ms": 3400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "바꿀", "start_ms": 3850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "수밖에", "start_ms": 4200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "없었던", "start_ms": 4650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "것입니다", "start_ms": 5100, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "문제는 그 사이에서 방어 공백이 발생하고 있다는 점입니다 결국 지금",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "문제는", "start_ms": 40, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 480, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "사이에서", "start_ms": 600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "방어", "start_ms": 1150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "공백이", "start_ms": 1550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "발생하고", "start_ms": 2050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "있다는", "start_ms": 2600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "점입니다", "start_ms": 3050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "결국", "start_ms": 3600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 4000, "dur_ms": 300, "phase_lr_deg": 0.0}
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
"transcript": "십 년 전 오늘 바로 여기서 세계는 인간과 AI의 대결을 지켜봤다",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "십", "start_ms": 80, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "년", "start_ms": 250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "전", "start_ms": 450, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 1100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "여기서", "start_ms": 1450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "세계는", "start_ms": 2000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "인간과", "start_ms": 2500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "AI의", "start_ms": 3000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "대결을", "start_ms": 3450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "지켜봤다", "start_ms": 3900, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "십 년 전에는 이세돌 교수님과 AI의 대결이",
"speaker": "speaker_4",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "십", "start_ms": 60, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "년", "start_ms": 250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "전에는", "start_ms": 450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이세돌", "start_ms": 900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "교수님과", "start_ms": 1400, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "AI의", "start_ms": 2050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "대결이", "start_ms": 2500, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 9,
"transcript": "입니다 저 푸른 초원에 그림 같은 집은 어쩌면 우리의 낡은 고정관념",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "입니다", "start_ms": 40, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "저", "start_ms": 380, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "푸른", "start_ms": 550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "초원에", "start_ms": 950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "그림", "start_ms": 1450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "같은", "start_ms": 1850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "집은", "start_ms": 2250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "어쩌면", "start_ms": 2650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "우리의", "start_ms": 3150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "낡은", "start_ms": 3600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "고정관념", "start_ms": 4000, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "아니는가 아이 그러니까 내가 뭐라 그랬어 내버려두라고 했잖아 잔소리",
"speaker": "mixed_speakers",
"confidence": 0.90,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "아니는가", "start_ms": 50, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "아이", "start_ms": 700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "그러니까", "start_ms": 1050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "내가", "start_ms": 1600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "뭐라", "start_ms": 1950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "그랬어", "start_ms": 2300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "내버려두라고", "start_ms": 2750, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "했잖아", "start_ms": 3500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "잔소리", "start_ms": 4000, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "가 그래서 오늘은 이 특사경에 대해서 조금 좀 살펴보려고 합니다 제가 갖고 있는 특사경에 대한 인식 특사경 수사주의 권한이 왜 문제인지 그리고 삭제했으니까",
"speaker": "speaker_5",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "가", "start_ms": 100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "오늘은", "start_ms": 750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 1200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "특사경에", "start_ms": 1400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "대해서", "start_ms": 1950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 2400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 2750, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "살펴보려고", "start_ms": 2950, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "합니다", "start_ms": 3600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 4000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "갖고", "start_ms": 4350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 4700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "특사경에", "start_ms": 5050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "대한", "start_ms": 5600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "인식", "start_ms": 5950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "특사경", "start_ms": 6350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "수사주의", "start_ms": 6850, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "권한이", "start_ms": 7450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "왜", "start_ms": 7950, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "문제인지", "start_ms": 8200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 8750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "삭제했으니까", "start_ms": 9200, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "올라프 씨가 집에 없습니다 한 일주일 됐습니다 지금 당연히 없지요 엄마 병원에 있지 아빠 혼자서 자고 있는데 아저씨",
"speaker": "mixed_speakers",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "올라프", "start_ms": 50, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "씨가", "start_ms": 500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "집에", "start_ms": 850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "없습니다", "start_ms": 1200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 1750, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "일주일", "start_ms": 2000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "됐습니다", "start_ms": 2450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 3000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "당연히", "start_ms": 3350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "없지요", "start_ms": 3850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "엄마", "start_ms": 4300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "병원에", "start_ms": 4650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "있지", "start_ms": 5100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "아빠", "start_ms": 5450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "혼자서", "start_ms": 5800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "자고", "start_ms": 6300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "있는데", "start_ms": 6650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아저씨", "start_ms": 7100, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "엄두가 안 났을 것 같습니다 무슨 그래가지고 지금 와이프가 너무 오래 있고 애들도 못 보고 계속 영상 통화하고 그러는데 오늘",
"speaker": "speaker_6",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "엄두가", "start_ms": 80, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 530, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "났을", "start_ms": 730, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 1080, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "같습니다", "start_ms": 1280, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "무슨", "start_ms": 1850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "그래가지고", "start_ms": 2200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 2850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "와이프가", "start_ms": 3200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 3750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "오래", "start_ms": 4100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "있고", "start_ms": 4450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "애들도", "start_ms": 4800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "못", "start_ms": 5250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "보고", "start_ms": 5450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 5800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "영상", "start_ms": 6200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "통화하고", "start_ms": 6600, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "그러는데", "start_ms": 7250, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 7800, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "안경 안경 안 썼네 아 애들이 아빠 보고 싶다고 해가지고 데려왔지 내가 아 진짜 몸 좀 어때",
"speaker": "mixed_speakers",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "안경", "start_ms": 50, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "안경", "start_ms": 400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 750, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "썼네", "start_ms": 950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 1300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "애들이", "start_ms": 1500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아빠", "start_ms": 1950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "보고", "start_ms": 2300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "싶다고", "start_ms": 2650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "해가지고", "start_ms": 3150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "데려왔지", "start_ms": 3700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "내가", "start_ms": 4250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 4600, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 4800, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "몸", "start_ms": 5150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 5400, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "어때", "start_ms": 5600, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "아니라 드론과 순항 미사일 탄도 미사일을 동시에 투입하는 방식으로 이스라엘을 공격하고 있는데요 여기에 다탄두 방식까지 결합해 하나의 목표물에 여러",
"speaker": "speaker_7",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "아니라", "start_ms": 40, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "드론과", "start_ms": 480, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "순항", "start_ms": 930, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "미사일", "start_ms": 1330, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "탄도", "start_ms": 1830, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "미사일을", "start_ms": 2230, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "동시에", "start_ms": 2780, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "투입하는", "start_ms": 3280, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "방식으로", "start_ms": 3830, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이스라엘을", "start_ms": 4380, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "공격하고", "start_ms": 5030, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "있는데요", "start_ms": 5580, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "여기에", "start_ms": 6130, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "다탄두", "start_ms": 6580, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "방식까지", "start_ms": 7080, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "결합해", "start_ms": 7630, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "하나의", "start_ms": 8130, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "목표물에", "start_ms": 8580, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "여러", "start_ms": 9180, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "이거를 좀 중목 구조의 느낌이 나도록 어떻게 표현할 수 있을까 해가지고 이 각관들을 나무로 감싼 겁니다 아 그러니까 이제 경량 철골 구조",
"speaker": "mixed_speakers",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "이거를", "start_ms": 100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 550, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "중목", "start_ms": 750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "구조의", "start_ms": 1100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "느낌이", "start_ms": 1550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "나도록", "start_ms": 2000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 2450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "표현할", "start_ms": 2850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 3300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있을까", "start_ms": 3500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "해가지고", "start_ms": 3950, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 4500, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "각관들을", "start_ms": 4700, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "나무로", "start_ms": 5300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "감싼", "start_ms": 5750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "겁니다", "start_ms": 6200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 6650, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "그러니까", "start_ms": 6850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 7350, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "경량", "start_ms": 7650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "철골", "start_ms": 8050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "구조", "start_ms": 8450, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "yesterday don't think you can help us you don't do that we're going to remember 하지만 이런 협박은 더 이상 유럽에 통하지 않고 있습니다 This is not our war and we will not",
"speaker": "mixed_speakers",
"confidence": 0.92,
"language": "en",
"silence_before_ms": 40,
"audio_type": "mixed",
"words": [
{"word": "yesterday", "start_ms": 40, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "don't", "start_ms": 700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "think", "start_ms": 1050, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 1400, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "can", "start_ms": 1650, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "help", "start_ms": 1950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "us", "start_ms": 2300, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 2600, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "don't", "start_ms": 2850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "do", "start_ms": 3200, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "that", "start_ms": 3450, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "we're", "start_ms": 3750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "going", "start_ms": 4100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 4450, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "remember", "start_ms": 4700, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "하지만", "start_ms": 5400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 5900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "협박은", "start_ms": 6300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 6800, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "이상", "start_ms": 7050, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "유럽에", "start_ms": 7400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "통하지", "start_ms": 7850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "않고", "start_ms": 8300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 8650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "This", "start_ms": 9200, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 9500, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "not", "start_ms": 9700, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "our", "start_ms": 10000, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "war", "start_ms": 10250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "and", "start_ms": 10600, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "we", "start_ms": 10850, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "will", "start_ms": 11050, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "not", "start_ms": 11300, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "그런데도 여기에 굉장히 돈이 될 만한 사건들이 꽤 있습니다 우리가 모두 기억하는 사건 쿠팡 퇴직금 미지급 사건 아시죠 그 문지석 검사와 그 부천지청장 그 누구죠 어미준 이 사건",
"speaker": "speaker_5",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "그런데도", "start_ms": 60, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "여기에", "start_ms": 550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "굉장히", "start_ms": 950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "돈이", "start_ms": 1400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "될", "start_ms": 1750, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "만한", "start_ms": 2000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "사건들이", "start_ms": 2350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "꽤", "start_ms": 2900, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 3150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "우리가", "start_ms": 3650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "모두", "start_ms": 4050, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "기억하는", "start_ms": 4400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "사건", "start_ms": 4900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "쿠팡", "start_ms": 5250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "퇴직금", "start_ms": 5650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "미지급", "start_ms": 6150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "사건", "start_ms": 6600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "아시죠", "start_ms": 6950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 7400, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "문지석", "start_ms": 7550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "검사와", "start_ms": 8000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 8450, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "부천지청장", "start_ms": 8600, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 9350, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "누구죠", "start_ms": 9500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "어미준", "start_ms": 9950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 10400, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "사건", "start_ms": 10550, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "AFM을 이용해서 다수 보게 되면은 뭔가 AI용으로 온디바이스 AI로 굉장히 빨리 돌릴 수 있다라는 거죠 그래서 젠마 4가 나왔기 때문에 요런 것들을 지금 빨리 돌려보고 싶어서 제가 직접 돌려보고 말씀을 드리는 건데 M5 프로 맥북에서",
"speaker": "speaker_8",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "AFM을", "start_ms": 50, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이용해서", "start_ms": 600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "다수", "start_ms": 1100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "보게", "start_ms": 1500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "되면은", "start_ms": 1850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "뭔가", "start_ms": 2300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "AI용으로", "start_ms": 2650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "온디바이스", "start_ms": 3200, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "AI로", "start_ms": 3800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "굉장히", "start_ms": 4250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "빨리", "start_ms": 4700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "돌릴", "start_ms": 5050, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 5400, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있다라는", "start_ms": 5600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "거죠", "start_ms": 6150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 6500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "젠마", "start_ms": 6950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "4가", "start_ms": 7300, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "나왔기", "start_ms": 7600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "때문에", "start_ms": 8050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "요런", "start_ms": 8450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "것들을", "start_ms": 8800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 9250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "빨리", "start_ms": 9600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "돌려보고", "start_ms": 9950, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "싶어서", "start_ms": 10500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 10950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "직접", "start_ms": 11300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "돌려보고", "start_ms": 11650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "말씀을", "start_ms": 12200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "드리는", "start_ms": 12650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "건데", "start_ms": 13100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "M5", "start_ms": 13450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "프로", "start_ms": 13800, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "맥북에서", "start_ms": 14150, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "선물을 드려도 될까요 어 선물요 좋은데요 선물 너무 좋죠 선물 너무 좋아 진행해 줘 오마이갓 오마이갓 찾으러 가자",
"speaker": "mixed_speakers",
"confidence": 0.91,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "선물을", "start_ms": 60, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "드려도", "start_ms": 500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "될까요", "start_ms": 950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "어", "start_ms": 1350, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "선물요", "start_ms": 1550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "좋은데요", "start_ms": 2000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "선물", "start_ms": 2500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 2850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "좋죠", "start_ms": 3200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "선물", "start_ms": 3550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 3900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "좋아", "start_ms": 4250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "진행해", "start_ms": 4600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "줘", "start_ms": 5050, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "오마이갓", "start_ms": 5250, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "오마이갓", "start_ms": 5800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "찾으러", "start_ms": 6350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "가자", "start_ms": 6850, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "비즈니스랍니다 이 어느 책에서인가 들었는데 일주일 중에 하루를 주어졌다면 진짜 그 시간을 투자해서 네가 하고 싶은 게 뭐냐 그게 너의 취미이자 네가 진짜 마음속에 가지고 있는 하고 싶은 일",
"speaker": "speaker_9",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "비즈니스랍니다", "start_ms": 80, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 830, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "어느", "start_ms": 1030, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "책에서인가", "start_ms": 1380, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "들었는데", "start_ms": 1980, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "일주일", "start_ms": 2480, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "중에", "start_ms": 2980, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "하루를", "start_ms": 3330, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "주어졌다면", "start_ms": 3780, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 4430, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 4780, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "시간을", "start_ms": 4930, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "투자해서", "start_ms": 5380, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "네가", "start_ms": 5930, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "하고", "start_ms": 6280, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "싶은", "start_ms": 6630, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 6980, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "뭐냐", "start_ms": 7180, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그게", "start_ms": 7580, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "너의", "start_ms": 7930, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "취미이자", "start_ms": 8280, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "네가", "start_ms": 8830, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 9180, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "마음속에", "start_ms": 9530, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "가지고", "start_ms": 10080, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 10530, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "하고", "start_ms": 10880, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "싶은", "start_ms": 11230, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "일", "start_ms": 11580, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "Subject You redeemed from last night Okay Yeah so you get what you get a pass Okay You know one of my all time favorites",
"speaker": "mixed_speakers",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Subject", "start_ms": 50, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "You", "start_ms": 600, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "redeemed", "start_ms": 850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "from", "start_ms": 1400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "last", "start_ms": 1750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "night", "start_ms": 2100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "Okay", "start_ms": 2550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "Yeah", "start_ms": 2900, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "so", "start_ms": 3200, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 3450, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "get", "start_ms": 3700, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "what", "start_ms": 4000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 4350, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "get", "start_ms": 4600, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 4900, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "pass", "start_ms": 5050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "Okay", "start_ms": 5450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "You", "start_ms": 5800, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "know", "start_ms": 6050, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "one", "start_ms": 6350, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "of", "start_ms": 6600, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "my", "start_ms": 6800, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "all", "start_ms": 7050, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "time", "start_ms": 7300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "favorites", "start_ms": 7650, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "받은 것도 아니고 이제 와서 그게 그렇게 인도네시아 마음대로 될 확률은 극히 희박해 보입니다 게다가 인도네시아는 앞서도 언급한 미국으로부터 십팔조 원을 주고 F-15 EX를 도입하려던 계획도 무산",
"speaker": "speaker_7",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "받은", "start_ms": 40, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "것도", "start_ms": 450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "아니고", "start_ms": 800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 1250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "와서", "start_ms": 1600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "그게", "start_ms": 1950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "그렇게", "start_ms": 2300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "인도네시아", "start_ms": 2750, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "마음대로", "start_ms": 3400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "될", "start_ms": 3950, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "확률은", "start_ms": 4200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "극히", "start_ms": 4650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "희박해", "start_ms": 5050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "보입니다", "start_ms": 5550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "게다가", "start_ms": 6100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "인도네시아는", "start_ms": 6550, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "앞서도", "start_ms": 7300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "언급한", "start_ms": 7750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "미국으로부터", "start_ms": 8200, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "십팔조", "start_ms": 8950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "원을", "start_ms": 9400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "주고", "start_ms": 9750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "F-15", "start_ms": 10100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "EX를", "start_ms": 10650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "도입하려던", "start_ms": 11100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "계획도", "start_ms": 11750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "무산", "start_ms": 12250, "dur_ms": 350, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 07:53:18*
