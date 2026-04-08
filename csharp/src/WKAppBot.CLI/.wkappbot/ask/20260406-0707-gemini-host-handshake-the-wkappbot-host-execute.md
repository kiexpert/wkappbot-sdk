# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=28596  time=07:05:13  uptime=11595s  mem=441MB  system_processes=412"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_070458.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 756-765-756-756-756-763-631-176-715-736-627-203-316-705-712-137-317-503-167-527-213-236-752-125-162-726 752-316-517-132-375-572-705-572-625-152-132-753-765-762-705-265-526-527-2_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 316-362-362-316-316-654-567-576-547-576-567 156-310-130-310-310-657-756-537-567-576-753-632 675-567-570-527-132-103-102-301-375-735-327-316-136-136-163-163-123-103-132-176 167-6_V.mp3)
  Segment 3: 3152ms ~ 9560ms (file: 102-263-167-751-123-312-261-136-136-143-103-267-167-617-106-321-326-103-132-765-267-526-567-263-107-370-731-137-130-175-567-573-312-236-326-163-106-130-103-103-201-102-106-132-1_V.mp3)
  Segment 4: 10560ms ~ 11136ms (file: 165-750-126-756-713-675-675-736-576-576-357-315-327-317-312-316-362-361-765-765-765-763_W.mp3)
  Segment 5: 12136ms ~ 12712ms (file: 675-671-613-137-316-613-673-671-671-627-675-765-675-762-762-765-132-135-132-572-506-150-105-152-506-571-653-103-532-765-162-125-521-576-365-135-135-673-136-367-765-763-670-673-7_W.mp3)
  Segment 6: 13712ms ~ 14288ms (file: 570-571-571-571-571-576-574-576-571-571-571-571-571-571-570 571-571-751-571-570-571-570-570-751-751-571-571-571-570-571-751-175 751-571-571-571-571-750-750-571-571-750-517-571_W.mp3)
  Segment 7: 15288ms ~ 22020ms (file: 230-206-213-310-320-573-231-321-123-123-320-312-132-130-320-120-123-120-123-627-261-260-216-102-106-126-120-102-120-213-267-261-136-107-321-230-230-231-570-103-136-207-102-320-1_V.mp3)
  Segment 8: 23020ms ~ 32668ms (file: 103-310-756-312-765-213-230-213-321-360-230-310-102-230-130-132-765-103-206-756-107-103-132-203-230-305-765-756-276-270-136-132-756-720-230-310-275-352-312-130-130-316-163-673-3_W.mp3)
  Segment 9: 33668ms ~ 34244ms (file: 271-765-326-502-127-762-306-653-217-273-562-302-627-567-135-573-507-765-621-753-726-375-376-570-725-750-365-763-265-625-276-352-725-321-207-273-576 572-573-756-726-123-752-127-7_V.mp3)
  Segment 10: 35244ms ~ 46188ms (file: 574-106-126-206-627-203-620-102-120-123-126-627-263-712-267-120-207-102-260-213-136-623-756-260-207-201-267-123-126-570-120-754-120-574-132-132-102-231-574-627-120-120-627-627-2_W.mp3)
  Segment 11: 47188ms ~ 53524ms (file: 756-650-735-237-756-106-672-765-754-135-120-106-132-103-213-213-217-230-576-576-231-150-213-705-172-103-275-752-752-721-753-570-762-210-765-672-725-761-725-103-321-203-302-176-7_V.mp3)
  Segment 12: 54524ms ~ 64208ms (file: 132-574-756-574-574-537-752-172-537-762-502-625-345-537-702-276-413-754-507-573-571-526-705-673-760-576-750-625-765-560-576-231-235-312-301-576-653-367-162-132-176-231-713 123-1_V.mp3)
  Segment 13: 65208ms ~ 65784ms (file: 107-710-170-710-710-701-750 671-617-671-675-675-657-756-571-751-710-175-170-710-710-751 657-175-576-567-567-756-701-710-715-715-751-175-705-701-710-170-705-701-751-750 675-567-7_V.mp3)
  Segment 14: 66784ms ~ 71968ms (file: 275-576-103-130-130-576-206-570-276-276-725-321-237-321-302-507-627-620-276-230-203-123-120-315-127-372-230-236-263-327-301-230-765-326-570-576-257-541-517-263-236-375-657-726-2_V.mp3)
  Segment 15: 72968ms ~ 89672ms (file: 257-123-137-132-137-173-715-137-213-213-217-276-216 213-216-267-236-216-263-267-263-261-213-231-230-301-213-312-312-372-317-130-137-106-102-312-236-263-631-312-230-103-573-573-1_W.mp3)
  Segment 16: 90672ms ~ 91248ms (file: 261-163-361-346-167-624-325-351-413-326-371-132_V.mp3)
  Segment 17: 92248ms ~ 92824ms (file: 235-123-267-276-750-672-267-265-237-526-276-275-356-257-715-156-627-576-567-657-576-537-351-517-576-576-576-765-576-756-657-756-765-756_W.mp3)
  Segment 18: 93824ms ~ 101420ms (file: 130-315-267-206-230-317-312-206-576-270-257-206-217-276-260-130-753-260-260-540-207-206-527-267-762-206-267-762-270-540-267-752-236-216-572-725-236-713-132-753-275-103-137-301-2_W.mp3)
  Segment 19: 102420ms ~ 102996ms (file: 275-327-217-506-276-751-753-532-762-576-765-753-572-752-752-671-135-175-136-176-263-752-673-173-751-576-750 270-231-276-752-715-127-257-257-273-562-136-576 561-361-135-276-261-2_V.mp3)
  Segment 20: 103996ms ~ 104572ms (file: 356-567-316-675-261-537-312-326-627-362-762-617-627-362-216-631-325-135-130-572-136-167-326-236-561-657-623-326-317-235-316-576-526-123-132-560-576-567-236-576 756-567-576-576-5_W.mp3)
  Segment 21: 105572ms ~ 106148ms (file: 103-540-543-132-136-312-136-763-716-763-173-163-137-326-216-675-136-723-675-763-761 627 176-376-173-137-176_W.mp3)
  Segment 22: 107148ms ~ 112764ms (file: 321-321-132-123-213-210-213-130-312-321-230-203-620-102-230-617-613-712-752-173-301-630-263-312-123-102-136-120-726-267-572-157-231-312-106-312-362-327-307-106-206-275-572-231-1_W.mp3)
  Segment 23: 113764ms ~ 124168ms (file: 263-326-326-326-627-672-162-136-136-276-310-312-326-263-326-236-326-361-130-326-123-132-123-206-210-216-162-102-261-216-120-263-263-216-263-103-130-157-103-132-231-167-312-367-2_W.mp3)
  Segment 24: 125168ms ~ 133340ms (file: 107-735-312-256-567-375-137-213-253-675-367-637-705-576-235-573-265-361-751-312-762-573-312-317-103-301-301-573-301-573-267-263-267-627-203-312-236-265-310-361-725-312-136-123-7_W.mp3)
  Segment 25: 134340ms ~ 139920ms (file: 176-735-576-203-320-123-310-132-123-263-120-213-756-576-326-371-317-750-570-753-750-753-173-321-162-321-310-326-601-621-165-312-362-106-210-263-206-260-627-106-102-326-731-320-2_V.mp3)
  Segment 26: 140920ms ~ 141496ms (file: 632-231-326-376-507-325-325-571-576-514-257-315-326-625-617-756-256-760-276-627-576-567-276-267-216-267-362-567-527-576-725-267-675-625-260-275-356-605-672-267-302-213-675-326-3_V.mp3)
  Segment 27: 142496ms ~ 143072ms (file: 536-567-563-516-352-153-756-576-576-576-357-576-751-135-156-156-657-615-652-615-675-635-673-673-671-672-675-675-675-675-672-672-672-671-672-637-672-673-627-673-627-627-623-672-6_W.mp3)
  Segment 28: 144072ms ~ 151524ms (file: 310-103-310-236-276-301-750-107-260-216-310-310-132-312-531-301-106-316-537-623-213-672-213-237-310-365-302-130-627-321-307-312-216-736-637-675-236-217-627-326-175-310-170-137-5_W.mp3)
  Segment 29: 152524ms ~ 153100ms (file: 675-723-365-506-237-756-205-106-301-263-675-357-265-267-237-275-537-265-267-627-625-236-265-504-564-256-576-257-506-352-753-316-316-321-135-102-635-751-237-756-275-576-513-531-6_V.mp3)
  Segment 30: 154100ms ~ 154676ms (file: 751-126-106-752-216-267-570-107-170-371-170 367-136-715-136-126-176-715-172-316-132-132-317-517-571_V.mp3)
  Segment 31: 155676ms ~ 166476ms (file: 310-130-523-541-620-623-510-130-321-236-236-762-256-256-206-326-320-102-210-231-236-276-675-130-321-231-263-326-130-302-102-267-326-326-236-263-261-260-205-524-521-231 103-123-7_W.mp3)
  Segment 32: 167476ms ~ 176692ms (file: 132-210-320-271-237-216-130-123-316-326-316-126-376-371-371-375-375-763-367-736-721-362-153-153-132-321-754-576-275-206-267-452-203-320-321-260-207-217-712-732-123-162-206-564-2_W.mp3)
  Segment 33: 177692ms ~ 178268ms (file: 623-276-263-570-625-325-725-237-560-265-263-261-263-675-761-537-213-261-135-570-325-527-752-275-576-273-576-107-153-137-123-312-173-201-357-762-627-271-267-572-723-753-750-217-7_V.mp3)
  Segment 34: 179268ms ~ 189924ms (file: 231-715-136-201-216-203-102-236-203-321-260-320-316-306-160-513-267-207-130-163-321-260-123-217-320-753-276-750-102-312-130-103-103-263-261-571-267-217-132-106-306-302-123-126-2_V.mp3)
  Segment 35: 190924ms ~ 191500ms (file: 765-176-736-307-136-715-716-716-710-701-160-176-175-276-276-726-750-710-706-760-705-761-701-265-507-752-310-237-127-175-527-571-567-752-527-752-572-572-574-576-526-567-526-562-5_W.mp3)
  Segment 36: 192500ms ~ 201644ms (file: 201-216-120-203-576-372-765-103-203-201-216-102-120-126-576-572-260-231-206-257-210-560-206-261-260-206-231-523-265-261-231-123-167-237-316-627-265-236-572-725-627-627-621-267-7_W.mp3)
  Segment 37: 202644ms ~ 212580ms (file: 316-312-310-361-316-376-263-136-231-573-572-357-275-325-312-320-103-263-267-267-267-754-527-253-572-574-321-312-176-326-576-576-103-371-327-573-572-137-107-136-312-310-137-236-2_W.mp3)
  Segment 38: 213580ms ~ 223768ms (file: 231-130-310-167-103-310-231-230-756-103-103-301-107-102-102-120-120-275-275-273-761-263-326-230-123-236-123-231-237-320-320-572-125-106-160-106-231-572-260-230-321-213-216-236-2_W.mp3)
  Segment 39: 224768ms ~ 235100ms (file: 564-673-230-132-103-137-163-317-137-132-671-132-570-751-756-571-537-713-756-157-175-105-165-603-653-130-751-173-310-175-672-751-107-756-750-756-310-750-756-657-103-756-750-756-3_V.mp3)
  Segment 40: 236100ms ~ 236676ms (file: 715-765-317-376-321-317-317-137-307-301-326-316-362-361-637-631_W.mp3)
  Segment 41: 237676ms ~ 238252ms (file: 157-157-175-157-157-157-157-157-157-157-571-517-571-157-157-157-517-157-176-571-157 571-517-517-517-157-157-157-157-165-715-167-173-175-175-176-175-167-157 135-103-130-175-175-1_V.mp3)
  Segment 42: 239252ms ~ 239828ms (file: 317-321-265-135-132-715-351-273-321-365-526-312-631-326-325-321-320-302-623-176-312-351-527-527-362-317-532-132-321-351-561-756-156-310-762-675-652-573-567-513-137-571-372-136-6_W.mp3)
  Segment 43: 240828ms ~ 241404ms (file: 157-157-157-157-157-571-517-517-751-137-105-137-103-105-175-157-517-157-157-517-517-571-517-517-517-571-351-103-157-157-157-157-157-157-157-157-517-157-567-157-517-175 157-157-5_W.mp3)
  Segment 44: 242404ms ~ 252556ms (file: 267-670-576-675-135 672-356-570-501-512-327-132-173-125-750-132-213-706-627-576-352-527-216-621-156-352-765-236-320-765-236-672-321-627-327-532-135-365-752-573 752-753-537-136-7_V.mp3)
  Segment 45: 253556ms ~ 254132ms (file: 567-756 275-702-625-273-217-257-257-752-237-570-263-326-256-756-716-753-752-236-261-265-267-752-567-756-132 517-756-721-206-276-726-276-657-267-376-726-201-570-157-576-750 675-2_V.mp3)
  Segment 46: 255132ms ~ 255708ms (file: 103-103-576-257-261-632-765-601-126-610-162-130-601-312-672-267-125-562-213-627-637-216-321-576-216-236-602-325-723-261-231-132-126-267-123-567-327-725-123-570-756-260-273-756-7_V.mp3)
  Segment 47: 256708ms ~ 257284ms (file: 372-213-316-751-763-765-157-570-705-573-570-753_W.mp3)
  Segment 48: 258284ms ~ 258860ms (file: 310-351-517-736-132-510-130-130-135-156-163-136-751-763-560-612-726-126-137-132-216-253-213-216-216-570-351-126-213-136-671-316-267-206-127-203-267-213-276-263-765-763-321-567-1_V.mp3)
  Segment 49: 259860ms ~ 260436ms (file: 134-761 137-136-123-321-126-135-137-130-132-132-132-103-132-301-103-132-137-137-132-132-132-103-314-136-137-132-136-136-135-132-132-136-136-123-136-136-137-162-137-105-103-132-1_V.mp3)
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
> **Time**: 2026-04-06 07:07:06
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "이런 모습을 보니 구글이나 오픈 AI 같은 로봇에 다시 뛰어들려고 합니다. 그런데 하드웨어라기보다 로봇 월드 모델",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "이런", "start_ms": 110, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "모습을", "start_ms": 450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "보니", "start_ms": 880, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "구글이나", "start_ms": 1250, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "오픈", "start_ms": 1950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "AI", "start_ms": 2350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "같은", "start_ms": 2700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "로봇에", "start_ms": 3100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 3600, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "뛰어들려고", "start_ms": 4050, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "합니다.", "start_ms": 4700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "그런데", "start_ms": 5200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "하드웨어라기보다", "start_ms": 5600, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "로봇", "start_ms": 6450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "월드", "start_ms": 6800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "모델", "start_ms": 7200, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "반갑습니다 용강입니다",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "반갑습니다", "start_ms": 50, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "용강입니다", "start_ms": 650, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "아이 반갑습니다",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "아이", "start_ms": 30, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 300, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 7,
"transcript": "아 이 모형을 보니까 이 앞부분에 이 창문이 동그라미잖아요 맞습니다 오",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "아", "start_ms": 80, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "모형을", "start_ms": 380, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "보니까", "start_ms": 800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 1200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "앞부분에", "start_ms": 1350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 1850, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "창문이", "start_ms": 2000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "동그라미잖아요", "start_ms": 2450, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "맞습니다", "start_ms": 3200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "오", "start_ms": 3700, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "를 급하게 설치하면서 중국과의 관계를 단절했다며 그 결과는 대만으로부터의 이익은 전혀 없고 중국으로부터는 큰 손해만 입었다고 말했다. 리투아니아는 2022년 사실상의 대만 대사관 역할을 하는 대표부 설립을 허용한 이후 중국과의",
"speaker": "speaker_4",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "를", "start_ms": 150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "급하게", "start_ms": 350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "설치하면서", "start_ms": 800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "중국과의", "start_ms": 1450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "관계를", "start_ms": 2000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "단절했다며", "start_ms": 2450, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 3150, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "결과는", "start_ms": 3300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "대만으로부터의", "start_ms": 3800, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "이익은", "start_ms": 4600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "전혀", "start_ms": 5050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "없고", "start_ms": 5450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "중국으로부터는", "start_ms": 5800, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 6600, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "손해만", "start_ms": 6800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "입었다고", "start_ms": 7300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "말했다.", "start_ms": 7850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "리투아니아는", "start_ms": 8350, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "2022년", "start_ms": 9150, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 10,
"transcript": "결정적인 부분은 그동안 대만 사회가 가볍게 보거나 제한적으로만 평가했던 한국의 군사 산업 역량이 실제로는 대만이 감히 엄두조차 낼 수 없는 전혀 다른 차원으로 올라섰다며 대만 매체가 이를 대대적으로 극찬하고 나서면서 연일 한국을 상대로 날선",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "결정적인", "start_ms": 120, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "부분은", "start_ms": 700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "그동안", "start_ms": 1150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "대만", "start_ms": 1600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "사회가", "start_ms": 1950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "가볍게", "start_ms": 2400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "보거나", "start_ms": 2900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "제한적으로만", "start_ms": 3350, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "평가했던", "start_ms": 4150, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "한국의", "start_ms": 4750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "군사", "start_ms": 5200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "산업", "start_ms": 5550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "역량이", "start_ms": 5900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "실제로는", "start_ms": 6400, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "대만이", "start_ms": 7000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "감히", "start_ms": 7450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "엄두조차", "start_ms": 7800, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "낼", "start_ms": 8400, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 8600, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "없는", "start_ms": 8800, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "전혀", "start_ms": 9150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "다른", "start_ms": 9550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "차원으로", "start_ms": 9900, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "연결 브라켓을 하면 그냥 드릴 하나면 조립할 수 있겠다라고 생각을 했는데 브라켓 가격이",
"speaker": "speaker_5",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "연결", "start_ms": 90, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "브라켓을", "start_ms": 480, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "하면", "start_ms": 1050, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 1400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "드릴", "start_ms": 1800, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "하나면", "start_ms": 2150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "조립할", "start_ms": 2650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 3150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있겠다라고", "start_ms": 3350, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "생각을", "start_ms": 4050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "했는데", "start_ms": 4500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "브라켓", "start_ms": 4950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "가격이", "start_ms": 5400, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "포커스 양보가 (music)",
"speaker": "speaker_6",
"confidence": 0.85,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "mixed",
"words": [
{"word": "포커스", "start_ms": 100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "양보가", "start_ms": 650, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "en",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 14,
"transcript": "I'm a seafood expert. Surgeon, of course. Roll of it.",
"speaker": "speaker_7",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "I'm", "start_ms": 150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 300, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "seafood", "start_ms": 450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "expert.", "start_ms": 950, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "Surgeon,", "start_ms": 1600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "of", "start_ms": 2150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "course.", "start_ms": 2350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "Roll", "start_ms": 2900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "of", "start_ms": 3250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "it.", "start_ms": 3450, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "김치찌개 예 오 오 음 오 음 Do I want scoop with us um gim on it",
"speaker": "speaker_8",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "김치찌개", "start_ms": 200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "예", "start_ms": 850, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "오", "start_ms": 1200, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "오", "start_ms": 1600, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "음", "start_ms": 2000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "오", "start_ms": 2500, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "음", "start_ms": 3000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "Do", "start_ms": 3500, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 3700, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "want", "start_ms": 3850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "scoop", "start_ms": 4200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "with", "start_ms": 4650, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "us", "start_ms": 4950, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "um", "start_ms": 5200, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "gim", "start_ms": 5450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "on", "start_ms": 5800, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "it", "start_ms": 6050, "dur_ms": 200, "phase_lr_deg": 0.0}
]
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 18,
"transcript": "개에 불과했던 파리 식당은 현재 300여 곳이 넘는데 새로 생긴 175개 중 80에서 90%가 왕서방이 운영하는 곳들이라고 하죠 기존에 운영하",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "개에", "start_ms": 110, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "불과했던", "start_ms": 400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "파리", "start_ms": 950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "식당은", "start_ms": 1300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "현재", "start_ms": 1850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "300여", "start_ms": 2250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "곳이", "start_ms": 2750, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "넘는데", "start_ms": 3050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "새로", "start_ms": 3550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "생긴", "start_ms": 3900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "175개", "start_ms": 4300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "중", "start_ms": 4850, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "80에서", "start_ms": 5050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "90%가", "start_ms": 5600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "왕서방이", "start_ms": 6150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "운영하는", "start_ms": 6700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "곳들이라고", "start_ms": 7200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "하죠", "start_ms": 7850, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 20,
"transcript": "Yes. Oh. Sorry to know. Yes. No, I didn't do anything. It's just skill. It's hot. It's hot. Breathing",
"speaker": "speaker_9",
"confidence": 0.91,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "Yes.", "start_ms": 120, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "Oh.", "start_ms": 450, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "Sorry", "start_ms": 700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 1050, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "know.", "start_ms": 1250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "Yes.", "start_ms": 1700, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "No,", "start_ms": 2000, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 2250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "didn't", "start_ms": 2400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "do", "start_ms": 2750, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "anything.", "start_ms": 2950, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "It's", "start_ms": 3500, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "just", "start_ms": 3750, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "skill.", "start_ms": 4050, "dur_ms": 400, "phase_lr_deg": 0.0}
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
"transcript": "자율주행 기기 결정에 휴머노이드라면 팔 다리가 중요한데 난 다리 부분은 발동형 휴머노이드 개념으로 개발한다고 했죠. 사족 보행도 발동 달린 거 휠 베이스 휴머노이드 로봇을 완성한다는 개념 범용",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "자율주행", "start_ms": 130, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "기기", "start_ms": 780, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "결정에", "start_ms": 1150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "휴머노이드라면", "start_ms": 1650, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "팔", "start_ms": 2500, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "다리가", "start_ms": 2750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "중요한데", "start_ms": 3200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "난", "start_ms": 3750, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "다리", "start_ms": 4000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "부분은", "start_ms": 4350, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "근데 그 사람이 파파이스 치킨을 너무 좋아한 거예요 한국에서 적응하기 힘든데 근데 이걸 딱 이 양동 통닭을 만난 거예요 그때 스토리가 있죠",
"speaker": "speaker_10",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "근데", "start_ms": 100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 450, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "사람이", "start_ms": 600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "파파이스", "start_ms": 1050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "치킨을", "start_ms": 1600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 2050, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "좋아한", "start_ms": 2400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 2850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "한국에서", "start_ms": 3300, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "적응하기", "start_ms": 3900, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "Jeff. You know what? Jeff is talking. Right. Jeff is talking.",
"speaker": "speaker_11",
"confidence": 0.93,
"language": "en",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "Jeff.", "start_ms": 150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "You", "start_ms": 600, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "know", "start_ms": 800, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "what?", "start_ms": 1050, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "Jeff", "start_ms": 1400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 1750, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "talking.", "start_ms": 1950, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "아니면 아예 반대 엄마는 정말 좀 마시고 반대로 가면 되고 여튼",
"speaker": "speaker_12",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "아니면", "start_ms": 100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "아예", "start_ms": 500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "반대", "start_ms": 850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "엄마는", "start_ms": 1250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 1700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 2050, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "마시고", "start_ms": 2300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "반대로", "start_ms": 2800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "가면", "start_ms": 3300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "되고", "start_ms": 3700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "여튼", "start_ms": 4100, "dur_ms": 300, "phase_lr_deg": 0.0}
]
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
"transcript": "With a little bit of champion of sizzle sound. Similar to what would you do? It's really good in together. Yes. Mm. Ah. At the butcher. Mm. And then",
"speaker": "speaker_13",
"confidence": 0.90,
"language": "en",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "With", "start_ms": 150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 400, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "little", "start_ms": 550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "bit", "start_ms": 900, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "of", "start_ms": 1150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "champion", "start_ms": 1350, "dur_ms": 500, "phase_lr_deg": 0.0}
]
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
"transcript": "아무도 없는 사무실에 에이전트만 남아 일을 하는 풍경은 이제 곧 현실이 된다.",
"speaker": "speaker_14",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "아무도", "start_ms": 120, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "없는", "start_ms": 650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "사무실에", "start_ms": 1000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "에이전트만", "start_ms": 1650, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "남아", "start_ms": 2350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "일을", "start_ms": 2750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 3100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "풍경은", "start_ms": 3450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 4000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "곧", "start_ms": 4350, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "현실이", "start_ms": 4600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "된다.", "start_ms": 5100, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "왔습니다. 제가 황조대 살고 있는 거 밥 맛있는 거 근데 혼자 다니면 좀 심심할 것 같아서 여기 친구도 왔어요",
"speaker": "speaker_12",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "왔습니다.", "start_ms": 100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "황조대", "start_ms": 1100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "살고", "start_ms": 1650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 2050, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 2400, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "안녕하세요 네 안녕하세요",
"speaker": "speaker_15",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 50, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "도시 여자 아내를 위해 근처 아파트에 살면서 남편만 매일 아침 이곳으로 출근을 한다는데요",
"speaker": "speaker_16",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "도시", "start_ms": 110, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "여자", "start_ms": 500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "아내를", "start_ms": 900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "위해", "start_ms": 1400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "근처", "start_ms": 1800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "아파트에", "start_ms": 2200, "dur_ms": 500, "phase_lr_deg": 0.0}
]
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
"transcript": "의존도는 현재 약 16% 정도 하지만 무작정 더 늘리긴 쉽지 않습니다. 일단 미국은 중동보다 훨씬 멉니다 좁은 운하를 통과 못 하는 대형 유조선은 아프리카 희망봉을 돌아오기 때문에",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "의존도는", "start_ms": 120, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "현재", "start_ms": 650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "약", "start_ms": 1100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "16% 정도", "start_ms": 1350, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "엄마 나오면 나와야 돼 알았지 얘들이 이제 몰래 하는 유튜브를 하도 많이 찍으니까 전문가 전문가 이제",
"speaker": "speaker_12",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "엄마", "start_ms": 100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "나오면", "start_ms": 450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "나와야", "start_ms": 900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "돼", "start_ms": 1350, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "알았지", "start_ms": 1600, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "이제는 저처럼 평범한 일반인도 뭔가 만들어 볼 수 있지 않을까라는 기대감이 좀 들기 시작하는 것 같습니다 요즘 네",
"speaker": "speaker_17",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "이제는", "start_ms": 130, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "저처럼", "start_ms": 650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "평범한", "start_ms": 1150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "일반인도", "start_ms": 1650, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "뭔가", "start_ms": 2300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "만들어", "start_ms": 2700, "dur_ms": 450, "phase_lr_deg": 0.0}
]
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 07:07:06*
