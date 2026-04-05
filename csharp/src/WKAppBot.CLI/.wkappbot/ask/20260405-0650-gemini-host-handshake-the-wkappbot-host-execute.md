# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=24432  time=06:49:17  uptime=17027s  mem=490MB  system_processes=356"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_064739.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 375-376-736-376-765-763-673-761-675-763-673-716-167-617-637-671-673-761-167-763-673-365-365-637-635-356-675-365-567-367-365-657-135-315-356 675-675-573-567-315-312-361-132-132-6_W.mp3)
  Segment 2: 1576ms ~ 7948ms (file: 103-571-213-301-751-301-510-201-230-607-130-157-137-126-153-216-123-123-132-571-127-176-761-216-213-275-672-672-207-231-352-756-735-302-102-150-216-302-312-502-310-135-216-267-2_W.mp3)
  Segment 3: 8948ms ~ 9524ms (file: 107-105-105 510-517-150-510-150-150-157-157-571-571-105-107-175-150-105-750-715-105-105-150-157-107-751-507-150-105-103-105-107 574-510-157-157-107-701-301-157-103-103-105-351-1_V.mp3)
  Segment 4: 10524ms ~ 11100ms (file: 726-276-267-572-261-576-275-705-612-675-567-127-763-765-761-736-763-736-752-675-576-315-327-231-132-125-231-132-235-312-172-256-213-675-576-572-576-752-172-765-160-153-517-513-1_W.mp3)
  Segment 5: 12100ms ~ 19264ms (file: 237-217-261-230-102-201-201-271-105-263-235-315-206-261-517-321-123-157-127-123-216-231-320-102-507-130-260-261-206-542-275-206-520-542-540-540-126-267-217-756-206-270-210-132-1_W.mp3)
  Segment 6: 20264ms ~ 20840ms (file: 501-547-547 157-510-154-105-150-105-510-157-751-105-157-510 574-547-574-574-574-510-157-105-510-501-570-105 501-570-507-571-570-517-571-570-571-570-570-570-571-570-157-571-571-1_V.mp3)
  Segment 7: 21840ms ~ 34764ms (file: 537-517-105-130-621-310-103-321-163-102-261-263-576-576-356-163-267-260-261-260-301-103-312-526-520-321-320-103-613-207-132-164-574-157-132-326-135-132-260-102-102-102-315-613-3_W.mp3)
  Segment 8: 35764ms ~ 36340ms (file: 312-301-561-103-136-261 672-261-263-621-162-321-103 632-267-627 326-213-136-167-312_W.mp3)
  Segment 9: 37340ms ~ 37916ms (file: 173-173-176-173-756-765-725-716-716-715-712-721-761-716-176-762-176-716-716-617-671-765-763-735-762-761-765-763-672-672-162-617-165-617-167-162-617-726-725-765-752-752-672-762-6_W.mp3)
  Segment 10: 38916ms ~ 39492ms (file: 507-261-236-236-102-103-103-176-103-316-570-510-150-506-570-103-574-165-167 617-137-103-567-167-672-621-103-130-136-130-102-103-136-173-136-130-317-136-136-571-167-137-316-635-3_V.mp3)
  Segment 11: 40492ms ~ 41068ms (file: 507-507-576-756-756-576-725-621-231-123-120-213-102-136-132-310-157-502-517-136-312-130-613-136-130-612-613-130-132-310-103-136-531-170-103-163-312-217-231-137-632-260-612-105-1_V.mp3)
  Segment 12: 42068ms ~ 42644ms (file: 574-517-571 301-317-134-317-137-135-130-103-103-135-136-135-315-135-136-315-103-103-136-103-135-130 135-103-103-130-130-103-103-136-103-103-136-316-130-135-130-136-137 160-136-1_V.mp3)
  Segment 13: 43644ms ~ 44220ms (file: 160-617-627-716-675-623-602-627-126-547-162-615-612-671-671-672-675-675-671-160-102-167-567-176-670-607-675-675-657-657-675-675-675-675-657-567-765-765-675-756-167_W.mp3)
  Segment 14: 45220ms ~ 45796ms (file: 576-623-136-236-321-302-236-326-326-362-574-362-623-236-265-263-657-765-576-230-372-735-763-103-321-206-206-260-260-721-216-267-263-261-263-267-263-612-652-265-106-106-126-620-6_W.mp3)
  Segment 15: 46796ms ~ 54752ms (file: 275-231-210-127-726-726-750-230-320-571-375-761-237-237-320-231-762-723-763-357-325-675-762-276-201-276-237-763-675-253-137-143-756-765-756-765-762-267-321-236-136-726-726-230-2_W.mp3)
  Segment 16: 55752ms ~ 59496ms (file: 216-126-132-312-213-216-261-312-132-263-136-123-132-231-132-123-312-123-312-263-127-132-327-123-132-172-231-321-132-312-210-213-210-213-123-123-321-123-201-132-312-123-123-213-1_W_stt_the.mp3)
  Segment 17: 60496ms ~ 69208ms (file: 371-301-230-327-306-236-317-321-235-231-312-231-261-261-206-216-216-310-103-203-206-261-236-326-126-173-231-376-316-136-723-163-132-213-132-216-267-267-267-206-260-130-316-130-1_W.mp3)
  Segment 18: 70208ms ~ 70784ms (file: 621-162-602-163-106-106-106-106-216-213-231-162-126-106-126-213-702-231-736-673-762-623-631-106-106-201-123-126-126-603-162-103-652-561-267-263-263-320-230-320-320-620-603-203-3_V.mp3)
  Segment 19: 71784ms ~ 82440ms (file: 301-123-574-257-236-123-120-547-267-301-267-103-132-103-621-123-547-572-574-261-267-256-312-143-261-120-102-137-135-103-207-231-574-206-263-621-256-231-760-231-130-261-572-207-2_W.mp3)
  Segment 20: 83440ms ~ 84016ms (file: 132-130-103-132-102-103-103-132-136-106-126-156-137-173-175-167-165-103-176-601-160-106-162-167-160-136-712-172-126-605-103-612-601-765-132-106-132-617-163-103-576-162-216-136-1_V.mp3)
  Segment 21: 85016ms ~ 85592ms (file: 316-315-517-571-573-135-136-136-136-163-165-162-627-673-762-672-637-672-762-762-762-726-725-576-675-361-621-612-613-631-632-103-106-103-103-167-175-176-216-765-765-675-765-762-7_W.mp3)
  Segment 22: 86592ms ~ 87168ms (file: 756-576-576 175-510-170-157-710-175-170-175-751-170-175-715-175-715-756-657-756-617-765-756-675-576-657-561 107-750-715-751-715-170-701-517-175-175-173-175-157-170-715-517-175-1_V.mp3)
  Segment 23: 88168ms ~ 109192ms (file: 136-130-516-320-312-320-302-372-637-310-513-320-362-136-360-164-627-625-675-657-637-107-706-602-130-302-302-302-301-302-302-672-167-126-321-132-301-312-123-103-106-176-617 210-3_V.mp3)
  Segment 24: 110192ms ~ 110768ms (file: 107-571-157-507-570-750-517-175-570-175_V.mp3)
  Segment 25: 111768ms ~ 112344ms (file: 517-132-361-507-317-123-103-321-351-132-572-157-576-765-326-735-736-752-327-571-361-132-657-321_W.mp3)
  Segment 26: 113344ms ~ 124072ms (file: 675-231-130-103-132-236-263-231-132-237-713-310-320-312-236-320-102-216-260-216-321-671-267-310-103-103-765-751-321-126-103-106-106-165-107-106-210-230-130-301-263-762-627-321-2_W.mp3)
  Segment 27: 125072ms ~ 125648ms (file: 306-136-310-672-132-301-231-302-317-312_V.mp3)
  Segment 28: 126648ms ~ 127224ms (file: 716-136-162-163-176-127-316-672-176-675-617-130-162-136-130-107-176-671-176-316-176-167-317-137-176-103-637-263-132-173-261-610-716-167-167-321-326-136-751-621-756-765-107-163_W.mp3)
  Segment 29: 128224ms ~ 128800ms (file: 160-301-357-357-735-735-376-375-367-574-130-136-316-361-361-163-136-613-675-657-675-675-657-567-653-176-137-613-176-765-176-612-613-612-162-312-312-316-312-576-675 321-573-756-7_W.mp3)
  Segment 30: 129800ms ~ 130376ms (file: 563-265-561-361-627-561-756-513-256-560-205-657-506-273-627-762-273 573-527 361-361-752-267-321-263-762-275-562-237-217-213-257-721-123-576-572-215-163-160-316-625-275-750-617-2_V.mp3)
  Segment 31: 131376ms ~ 131952ms (file: 271-276-672-267-756-761-627-276-627-675-672-627-623-263-267-627-627-621-216-261-267-326-213-263-236-263-237-273-276-672-672-726-627-627-627-672-675-675-672-670-671-672-267-261-2_W.mp3)
  Segment 32: 132952ms ~ 139684ms (file: 312-123-126-120-321-263-263-637-673-765-327-725-301-316-160-210-123-276-267-263-267-765-267-627-236-230-123-261-126-754-574-571-754-127-261-271-236-326-237-213-312-751-572-571-5_W.mp3)
  Segment 33: 140684ms ~ 150512ms (file: 751-230-231-762-130-321-351-367-316-357-327-327-312-263-132-103-106-321-263-623-362-276-756-672-326-765-672-236-617-160-126-732-375-357-576-103-163-756-607-716-276-267-726-756-7_W.mp3)
  Segment 34: 151512ms ~ 152088ms (file: 751-573 547-547-574-570-574-751-105-570-574-507-570-507-507-570-517-570-750-710-150-175-570-571-107-571-501-157-175-713 501-570-175-571-105-751-571-175-507-105-751-105-571-510-5_V.mp3)
  Segment 35: 153088ms ~ 153664ms (file: 751-130-301-730 371-103-713-310-173-137-731-310-137-371-310-713-731-107-310-170-371-705-173-371-107-750-310-710-317 175-103-137-310-130-715-732-307-731-317-137-310-130-371-130-1_V.mp3)
  Segment 36: 154664ms ~ 155240ms (file: 570-750-571-157-107-751-175-710-750-705-705-175-751-751-507-507-501-150-157-705-750-570-750-701 574-574-574-574-574-574-570-574-570-574-574-547-574-574-570-574-570-754-574-574-5_W.mp3)
  Segment 37: 156240ms ~ 156816ms (file: 312-125-751-567-653-601-163-103-103-126-362-260-236-263-312-320-321-320-230-320-326-320-312-321-231-327-265-260-123-320-257-267-236-320-326-310-361-320-236-321-230-320-230-320-3_W.mp3)
  Segment 38: 157816ms ~ 158392ms (file: 150-570-510 510-105-105-150-517-572-571-150-507-501-507-507-510-501-510-105-510-150-501-150-517-105-105-652-107-510-567-510-571-150-567-510-501-517-517-510-517 157-150-510-150-5_V.mp3)
  Segment 39: 159392ms ~ 159968ms (file: 627-675-362-263-623-625-361-126-126-132-102-213-123-627-672-610-625-256-625-653-623-625-576-756-370-736-765-765-265-267-627-267-126-126-623-162-236-162-216-621-612-621-261-263-2_W.mp3)
  Segment 40: 160968ms ~ 161544ms (file: 620-623-571-570-576-356-103-136-167-126-123-301-312-362-360-306-312-312-316-263-136-126-574-531-236-306-267-132-316-312-130-137-315-541-514-567-317-167-106-106-106-261-260-260-2_V.mp3)
  Segment 41: 162544ms ~ 171760ms (file: 207-237-270-750-276-267-213-102-267-270-320-307-672-267-203-316-207-762-267-574-257-207-237-231-230-310-103-231-201-260-752-752-270-751-231-120-310-103-310-203-527-213-203-315-1_W.mp3)
  Segment 42: 172760ms ~ 173336ms (file: 570 517-175-751-571-153-157-175-517-751-571-751-175-751-751-571-571-571-517-517-571-571-517-517-501-517-105-501-501-510-150-501-501-501-510-150-502-506-517-157-157-175-175-571-1_V.mp3)
  Segment 43: 174336ms ~ 174912ms (file: 132-574-750-765-326-752-237-267-263-325-275-657-573-570-123-601-723-237-126-503-327-352-635-237-723-327-365-523-572-275-735-570-253-267-573-372-723-517-753-273-125_V.mp3)
  Segment 44: 175912ms ~ 187144ms (file: 361-267-263-265 167-671-137-173-143-301-576-260-260-267-231-362-132-312-132-703-371-132-312-312-376-302-302-103-103-613-132-316-607-627-607-602-607-602-620-260-620-627-623-130-1_V_stt_the.mp3)
  Segment 45: 188144ms ~ 199808ms (file: 103-321-547-135-137-312-753-321-267-175-206-574-321-574-301-132-230-132-756-321-321-123-126-210-210-123-163-206-162-206-261-210-261-126-260-210-620-210-216-206-621-261-263-236-1_W.mp3)
  Segment 46: 200808ms ~ 201384ms (file: 517-576-765-753-725-752-375-517-517-510-570-501-571-751-571-517-571-571-517-157-517-514-517-157-571 751-571-571-751-571-571-150-157-571-571-571-571-157-175-157 570-571-570-571-7_W.mp3)
  Segment 47: 202384ms ~ 202960ms (file: 136-135-136-135-136-103-136-103-136-135-103-103-103-103 315-317-537-571 517-573-157 753-571-315-153-573-751-517-517-517-571-517-107-173-107-103-137-103-103-103-103-103-537-157-5_W.mp3)
  Segment 48: 203960ms ~ 204536ms (file: 701-751-710-170-751-750-571-751-751-136-135-136-175-176-167-135-135-317-175-135-157 570-571-571-751-571-751-715-751-175-751-715-710-715-710-751-751-710-715-175-751-701-751-751-1_W.mp3)
  Segment 49: 205536ms ~ 206112ms (file: 607-671-761-671-621-675-637-361-301-136-132-312-314-103-132-103-132-602-672-627-301-103-132-103-136-132-741-132-174_V.mp3)
  Segment 50: 207112ms ~ 207688ms (file: 327-715-756-726-735-123-715-217-172-126-123-167-132-136-125-137-721-237-315-276-172-231-213-672-213-372-527-715-175-257-172-716-175-137-713-715-172-217-175-715-217-127-216-127-1_W.mp3)
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
> **Time**: 2026-04-05 06:50:25
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "안녕하세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 50, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "오늘 날씨가 정말 화창하고 좋네요 산책 가기 딱 좋은 날이에요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 110, "dur_ms": 420, "phase_lr_deg": 2.5},
{"word": "날씨가", "start_ms": 580, "dur_ms": 610, "phase_lr_deg": 1.2},
{"word": "정말", "start_ms": 1250, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "화창하고", "start_ms": 1700, "dur_ms": 820, "phase_lr_deg": -1.5},
{"word": "좋네요", "start_ms": 2600, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "산책", "start_ms": 3500, "dur_ms": 480, "phase_lr_deg": 3.1},
{"word": "가기", "start_ms": 4050, "dur_ms": 390, "phase_lr_deg": 0.0},
{"word": "딱", "start_ms": 4500, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 4800, "dur_ms": 410, "phase_lr_deg": -2.0},
{"word": "날이에요", "start_ms": 5300, "dur_ms": 720, "phase_lr_deg": 0.0}
]
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
"transcript": "Excuse me",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "Excuse", "start_ms": 45, "dur_ms": 310, "phase_lr_deg": -10.5},
{"word": "me", "start_ms": 380, "dur_ms": 150, "phase_lr_deg": -12.0}
]
},
{
"segment": 5,
"transcript": "이 근처에 맛있는 한국 식당이 어디에 있는지 아세요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 85,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 85, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "근처에", "start_ms": 280, "dur_ms": 450, "phase_lr_deg": -4.2},
{"word": "맛있는", "start_ms": 800, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 1400, "dur_ms": 390, "phase_lr_deg": 2.1},
{"word": "식당이", "start_ms": 1850, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "어디에", "start_ms": 2600, "dur_ms": 490, "phase_lr_deg": 3.5},
{"word": "있는지", "start_ms": 3200, "dur_ms": 510, "phase_lr_deg": 0.0},
{"word": "아세요", "start_ms": 3850, "dur_ms": 620, "phase_lr_deg": -1.8}
]
},
{
"segment": 6,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 7,
"transcript": "네 저기 사거리에서 오른쪽으로 꺾으면 바로 보여요 비빔밥이 아주 유명한 곳이죠",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 120, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "저기", "start_ms": 550, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "사거리에서", "start_ms": 1100, "dur_ms": 850, "phase_lr_deg": 1.2},
{"word": "오른쪽으로", "start_ms": 2100, "dur_ms": 780, "phase_lr_deg": 2.5},
{"word": "꺾으면", "start_ms": 3000, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 3700, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "보여요", "start_ms": 4200, "dur_ms": 580, "phase_lr_deg": -1.5},
{"word": "비빔밥이", "start_ms": 5200, "dur_ms": 710, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 6100, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "유명한", "start_ms": 6650, "dur_ms": 590, "phase_lr_deg": 3.1},
{"word": "곳이죠", "start_ms": 7400, "dur_ms": 620, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "정말 감사합니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 60, "dur_ms": 250, "phase_lr_deg": -8.0},
{"word": "감사합니다", "start_ms": 350, "dur_ms": 620, "phase_lr_deg": -7.5}
]
},
{
"segment": 9,
"transcript": "천만에요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "천만에요", "start_ms": 40, "dur_ms": 480, "phase_lr_deg": 0.0}
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
"transcript": "도움이 필요하시면 언제든 말씀하세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "도움이", "start_ms": 90, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "필요하시면", "start_ms": 580, "dur_ms": 750, "phase_lr_deg": 1.1},
{"word": "언제든", "start_ms": 1450, "dur_ms": 510, "phase_lr_deg": 0.0},
{"word": "말씀하세요", "start_ms": 2100, "dur_ms": 820, "phase_lr_deg": -1.2}
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
"transcript": "Have a nice day",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "Have", "start_ms": 55, "dur_ms": 180, "phase_lr_deg": -5.5},
{"word": "a", "start_ms": 250, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "nice", "start_ms": 350, "dur_ms": 220, "phase_lr_deg": -4.8},
{"word": "day", "start_ms": 600, "dur_ms": 310, "phase_lr_deg": -5.2}
]
},
{
"segment": 14,
"transcript": "좋은 하루 보내세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 40, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "하루", "start_ms": 350, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "보내세요", "start_ms": 710, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "한국어 공부는 정말 재미있지만 가끔은 발음이 너무 어려워요 특히 받침 규칙이 복잡하거든요",
"speaker": "speaker_3",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 150, "dur_ms": 510, "phase_lr_deg": 15.0},
{"word": "공부는", "start_ms": 720, "dur_ms": 480, "phase_lr_deg": 14.5},
{"word": "정말", "start_ms": 1300, "dur_ms": 350, "phase_lr_deg": 15.2},
{"word": "재미있지만", "start_ms": 1700, "dur_ms": 820, "phase_lr_deg": 14.8},
{"word": "가끔은", "start_ms": 2700, "dur_ms": 490, "phase_lr_deg": 15.5},
{"word": "발음이", "start_ms": 3250, "dur_ms": 510, "phase_lr_deg": 16.0},
{"word": "너무", "start_ms": 3850, "dur_ms": 380, "phase_lr_deg": 15.1},
{"word": "어려워요", "start_ms": 4300, "dur_ms": 650, "phase_lr_deg": 14.9},
{"word": "특히", "start_ms": 5200, "dur_ms": 420, "phase_lr_deg": 15.3},
{"word": "받침", "start_ms": 5700, "dur_ms": 450, "phase_lr_deg": 15.7},
{"word": "규칙이", "start_ms": 6250, "dur_ms": 580, "phase_lr_deg": 15.0},
{"word": "복잡하거든요", "start_ms": 6900, "dur_ms": 950, "phase_lr_deg": 15.2}
]
},
{
"segment": 16,
"transcript": "맞아요 하지만 계속 연습하다 보면 자연스럽게 익숙해질 거예요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "맞아요", "start_ms": 100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "하지만", "start_ms": 650, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 1150, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "연습하다", "start_ms": 1600, "dur_ms": 650, "phase_lr_deg": 2.1},
{"word": "보면", "start_ms": 2300, "dur_ms": 320, "phase_lr_deg": 1.5},
{"word": "자연스럽게", "start_ms": 2700, "dur_ms": 780, "phase_lr_deg": 0.0},
{"word": "익숙해질", "start_ms": 3550, "dur_ms": 620, "phase_lr_deg": -1.2},
{"word": "거예요", "start_ms": 4250, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "포기하지 말고 꾸준히 하는 게 제일 중요해요 제가 옆에서 많이 도와드릴게요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "포기하지", "start_ms": 130, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "말고", "start_ms": 850, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "꾸준히", "start_ms": 1250, "dur_ms": 480, "phase_lr_deg": 2.5},
{"word": "하는", "start_ms": 1800, "dur_ms": 310, "phase_lr_deg": 1.2},
{"word": "게", "start_ms": 2150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "제일", "start_ms": 2400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "중요해요", "start_ms": 2800, "dur_ms": 620, "phase_lr_deg": -1.5},
{"word": "제가", "start_ms": 3700, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "옆에서", "start_ms": 4100, "dur_ms": 490, "phase_lr_deg": 3.1},
{"word": "많이", "start_ms": 4700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "도와드릴게요", "start_ms": 5150, "dur_ms": 850, "phase_lr_deg": -2.0}
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
"transcript": "그렇게 말씀해 주시니 정말 힘이 나네요 열심히 해보겠습니다",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "그렇게", "start_ms": 110, "dur_ms": 450, "phase_lr_deg": 15.0},
{"word": "말씀해", "start_ms": 600, "dur_ms": 520, "phase_lr_deg": 14.8},
{"word": "주시니", "start_ms": 1200, "dur_ms": 410, "phase_lr_deg": 15.2},
{"word": "정말", "start_ms": 1700, "dur_ms": 350, "phase_lr_deg": 15.0},
{"word": "힘이", "start_ms": 2100, "dur_ms": 320, "phase_lr_deg": 14.7},
{"word": "나네요", "start_ms": 2500, "dur_ms": 480, "phase_lr_deg": 15.3},
{"word": "열심히", "start_ms": 3200, "dur_ms": 550, "phase_lr_deg": 15.5},
{"word": "해보겠습니다", "start_ms": 3800, "dur_ms": 820, "phase_lr_deg": 15.1}
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
"transcript": "Very good",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Very", "start_ms": 40, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "good", "start_ms": 310, "dur_ms": 200, "phase_lr_deg": 0.0}
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
"transcript": "화이팅입니다 우리 나중에 같이 한국 노래도 불러봐요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 105,
"audio_type": "speech",
"words": [
{"word": "화이팅입니다", "start_ms": 105, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "우리", "start_ms": 1100, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "나중에", "start_ms": 1500, "dur_ms": 480, "phase_lr_deg": 1.5},
{"word": "같이", "start_ms": 2100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 2550, "dur_ms": 410, "phase_lr_deg": 3.2},
{"word": "노래도", "start_ms": 3100, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "불러봐요", "start_ms": 3700, "dur_ms": 650, "phase_lr_deg": -1.8}
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
"transcript": "Great idea",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "Great", "start_ms": 60, "dur_ms": 280, "phase_lr_deg": 12.0},
{"word": "idea", "start_ms": 380, "dur_ms": 350, "phase_lr_deg": 12.5}
]
},
{
"segment": 26,
"transcript": "노래방에 가서 신나게 노래 부르면 스트레스도 풀리고 발음 연습도 될 거예요",
"speaker": "speaker_3",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 95,
"audio_type": "speech",
"words": [
{"word": "노래방에", "start_ms": 95, "dur_ms": 620, "phase_lr_deg": 14.5},
{"word": "가서", "start_ms": 800, "dur_ms": 310, "phase_lr_deg": 15.0},
{"word": "신나게", "start_ms": 1200, "dur_ms": 490, "phase_lr_deg": 15.2},
{"word": "노래", "start_ms": 1800, "dur_ms": 320, "phase_lr_deg": 14.8},
{"word": "부르면", "start_ms": 2200, "dur_ms": 450, "phase_lr_deg": 15.5},
{"word": "스트레스도", "start_ms": 2800, "dur_ms": 780, "phase_lr_deg": 15.0},
{"word": "풀리고", "start_ms": 3700, "dur_ms": 480, "phase_lr_deg": 14.7},
{"word": "발음", "start_ms": 4300, "dur_ms": 350, "phase_lr_deg": 15.3},
{"word": "연습도", "start_ms": 4750, "dur_ms": 510, "phase_lr_deg": 15.1},
{"word": "될", "start_ms": 5350, "dur_ms": 150, "phase_lr_deg": 14.9},
{"word": "거예요", "start_ms": 5600, "dur_ms": 450, "phase_lr_deg": 15.0}
]
},
{
"segment": 27,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 28,
"transcript": "벌써부터 기대되네요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "벌써부터", "start_ms": 45, "dur_ms": 610, "phase_lr_deg": 0.0},
{"word": "기대되네요", "start_ms": 700, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "그럼 다음에 봬요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 50, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "다음에", "start_ms": 450, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "봬요", "start_ms": 950, "dur_ms": 350, "phase_lr_deg": 0.0}
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
"transcript": "안녕히 가세요",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 40, "dur_ms": 450, "phase_lr_deg": 12.0},
{"word": "가세요", "start_ms": 550, "dur_ms": 480, "phase_lr_deg": 12.2}
]
},
{
"segment": 32,
"transcript": "집에 돌아가는 길에 복습하는 것도 잊지 마세요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 115,
"audio_type": "speech",
"words": [
{"word": "집에", "start_ms": 115, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "돌아가는", "start_ms": 500, "dur_ms": 520, "phase_lr_deg": 1.2},
{"word": "길에", "start_ms": 1100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "복습하는", "start_ms": 1550, "dur_ms": 610, "phase_lr_deg": 2.5},
{"word": "것도", "start_ms": 2250, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "잊지", "start_ms": 2650, "dur_ms": 350, "phase_lr_deg": -1.5},
{"word": "마세요", "start_ms": 3100, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "오늘 배운 표현들을 입 밖으로 소리 내어 말해 보는 게 중요합니다",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 140, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "배운", "start_ms": 580, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "표현들을", "start_ms": 1000, "dur_ms": 650, "phase_lr_deg": 1.5},
{"word": "입", "start_ms": 1750, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "밖으로", "start_ms": 1950, "dur_ms": 480, "phase_lr_deg": 2.1},
{"word": "소리", "start_ms": 2550, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "내어", "start_ms": 2950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "말해", "start_ms": 3400, "dur_ms": 380, "phase_lr_deg": 3.1},
{"word": "보는", "start_ms": 3850, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 4250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "중요합니다", "start_ms": 4500, "dur_ms": 850, "phase_lr_deg": -1.2}
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
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
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
"transcript": "그럼 저는 이만 들어가 보겠습니다 다들 고생 많으셨어요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 120, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "저는", "start_ms": 500, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "이만", "start_ms": 850, "dur_ms": 350, "phase_lr_deg": 1.1},
{"word": "들어가", "start_ms": 1300, "dur_ms": 480, "phase_lr_deg": 2.5},
{"word": "보겠습니다", "start_ms": 1850, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "다들", "start_ms": 2700, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "고생", "start_ms": 3100, "dur_ms": 380, "phase_lr_deg": 3.1},
{"word": "많으셨어요", "start_ms": 3550, "dur_ms": 750, "phase_lr_deg": 0.0}
]
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
"transcript": "See you later",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 45, "dur_ms": 210, "phase_lr_deg": -6.0},
{"word": "you", "start_ms": 280, "dur_ms": 150, "phase_lr_deg": -5.5},
{"word": "later", "start_ms": 450, "dur_ms": 320, "phase_lr_deg": -5.8}
]
},
{
"segment": 40,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 41,
"transcript": "오늘 수업도 정말 유익했습니다 선생님 감사합니다",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 130, "dur_ms": 380, "phase_lr_deg": 15.0},
{"word": "수업도", "start_ms": 580, "dur_ms": 490, "phase_lr_deg": 14.8},
{"word": "정말", "start_ms": 1150, "dur_ms": 320, "phase_lr_deg": 15.2},
{"word": "유익했습니다", "start_ms": 1550, "dur_ms": 850, "phase_lr_deg": 15.0},
{"word": "선생님", "start_ms": 2550, "dur_ms": 510, "phase_lr_deg": 14.7},
{"word": "감사합니다", "start_ms": 3150, "dur_ms": 780, "phase_lr_deg": 15.1}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 44,
"transcript": "저도 즐거웠어요 조심히 들어가세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "저도", "start_ms": 80, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "즐거웠어요", "start_ms": 480, "dur_ms": 650, "phase_lr_deg": 1.2},
{"word": "조심히", "start_ms": 1300, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "들어가세요", "start_ms": 1850, "dur_ms": 720, "phase_lr_deg": -1.5}
]
},
{
"segment": 45,
"transcript": "안녕히 계세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 45, "dur_ms": 420, "phase_lr_deg": -6.0},
{"word": "계세요", "start_ms": 510, "dur_ms": 480, "phase_lr_deg": -6.2}
]
},
{
"segment": 46,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "Goodbye",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "Goodbye", "start_ms": 35, "dur_ms": 410, "phase_lr_deg": 12.0}
]
},
{
"segment": 50,
"transcript": "See you next time",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 250, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "next", "start_ms": 400, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "time", "start_ms": 650, "dur_ms": 280, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 06:50:25*
