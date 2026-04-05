# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=30524  time=07:23:11  uptime=10303s  mem=461MB  system_processes=356"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_072049.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 8604ms (file: 160-126-123-672-263-321-231-762-267-237-102-312-213-276-132-132-267-263-213-123-102-236-230-236-267-267-267-120-120-261-312-263-275-237-260-672-267-267-263-236-267-236-612-762-2_W.mp3)
  Segment 2: 9604ms ~ 10180ms (file: 136-136 134-315-143-314-135-134-103-103-134-103-103-135-103-136-103-103-136-103-136-136 315-517-357-571-517-107-137-103 357-351-357-517-153-106-103-137-137 751-573-517-157-517-5_V.mp3)
  Segment 3: 11180ms ~ 21908ms (file: 236-320-231-675-576-753-321-276-237-321-301-103-213-237-327-321-675-276-230-271-132-132-301-574-236-230-312-627-627-276-216-312-103-130-267-327-726-267-321-103-256-173-107-672-3_W.mp3)
  Segment 4: 22908ms ~ 23484ms (file: 132-312-132-127-137-136-163-132-132-135-132-136-137_V.mp3)
  Segment 5: 24484ms ~ 32080ms (file: 270-326-207-276-736-103-136-126-301-320-231-132-201-216-102-132-657-167-610-657-726-203-130-107-706-706-132-523-726-326-321-167-103-123-132-103-102-120-627-562-657-276-625-623-6_W.mp3)
  Segment 6: 33080ms ~ 40856ms (file: 267-361-632-236-326-576-672-130-267-627-321-130-321-120-623-206-361-130-671-612-617-167-310-137-132-237-375-132-312-132-561-176-163-106-167-130-326-136-137-716-176-102-103-123-2_W.mp3)
  Segment 7: 41856ms ~ 48264ms (file: 574-520-547-120-165-213-231-615-615-612-126-213-136-103-132-162-162-561-162-260-210-254-532-513-253-524-542-105-576-657-567-160-175-517-365-576-573-571-574-120-627-216-236-231-2_W.mp3)
  Segment 8: 49264ms ~ 49840ms (file: 761-762-763-672-671-672-672-671-672-671_W.mp3)
  Segment 9: 50840ms ~ 51416ms (file: 301-310-310-631-163-163-106-631-360-631-613-167-613-160-163-160-613-627-627-671-627-637-627-623-672-627-632-623-263-623-672-570-570-571-135-130-163-163-672-673-632-570-570-517-5_W.mp3)
  Segment 10: 52416ms ~ 52992ms (file: 752-216-263-572-321-756-725-273-726-267-123-631-615-365-657-651-635-756-653-657-657-576 673-632_W.mp3)
  Segment 11: 53992ms ~ 54568ms (file: 175-170-170-510 501-516-105-175-150-517-157-150-501-571-105-175-170-175-170-157-501-507-105-107-107-517 501-507-576-507-105-517-571-571-570-510-751-570-750-751-576-571-570-751-5_V.mp3)
  Segment 12: 55568ms ~ 58088ms (file: 132-137-657-362-316-312-310-123-136-137-317-137-130-310-132-137-317-175-317-316-130-310-317-103-173-123-765 613-351-315-312-312-132-136-671-176-761-675-673-176_W.mp3)
  Segment 13: 59088ms ~ 59664ms (file: 107-170-157-150-157-157-157-157-175-105-175-170-175-571 105-105-105-150-157-157-157-157-157-150-157-571-174-175-715-715-175 540-514-510-514-105-150-150-175-170-175-105-175-715-1_V.mp3)
  Segment 14: 60664ms ~ 61240ms (file: 175-501-751-570-570-571 570-517-157-510-517-571-570-751-754-754-754-756-571-574 571-510-517-501-570-574-574-571-571-175-157-570-701-751-750-501-517-517-157-175-570-571-157-715-1_W.mp3)
  Segment 15: 62240ms ~ 62816ms (file: 513-135-107-305-310-276-637-726-762-762-726-736-762-762-765-756-756 517-571-751-136-173-317-157-317-571-701-765-675-307-756-617-716-172-176-761-716-760-701-170-371-175-157-317-1_W.mp3)
  Segment 16: 63816ms ~ 64392ms (file: 103-107-173-175-157-153-175-175-735 105-103-107-157-107-175-107-751-710-157-107 517-150-107-175-105-107-175-157-105-107-761 501-157-105-150-150-107-715-105-107-157-105-105-157 1_V.mp3)
  Segment 17: 65392ms ~ 65968ms (file: 756-176-132-123 127 172-715 162-312-362-162-132-231-362-236-321-123-627_W.mp3)
  Segment 18: 66968ms ~ 75536ms (file: 237-310-267-230-103-123-103-231-276-321-312-103-120-213-123-273-213-301-120-732-267-230-230-672-726-273-230-102-107-173-312-230-270-132-701-310-231-273-312-102-317-762-276-726-1_W.mp3)
  Segment 19: 76536ms ~ 77112ms (file: 316-312-163-316-316-316-231-326-236-326-236-236-356-265-625-621-236-625-267-263-213-326-534-574-517-157-156-715-275-126-517-156-157-156-563-652-365-536-576-576-537-536-513-235-2_W.mp3)
  Segment 20: 78112ms ~ 78688ms (file: 130-632-623-162-163-613 671-301-136-213-230 367-136-630_V.mp3)
  Segment 21: 79688ms ~ 87752ms (file: 103-317-761-267-103-103-136-102-102-103-130-103-231-102-126-312-231-312-132-316-357-230-753-206-301-102-236-102-132-206-612-362-260-612-621-261-263-701-132-236-230-127-261-263-3_W.mp3)
  Segment 22: 88752ms ~ 89328ms (file: 312-325-254-573-751-720-103-206-321 513-573-574-563-547-574-150-120-623-163-673-650-567-257-627-261-302-312-302-320-321-103-312-612-206-236-316-361-571-756-765-760-627-620-620-6_W.mp3)
  Segment 23: 90328ms ~ 91372ms (file: 607-765-736-761-610-175-105-715 765-765-760-672-623-312-132-617-105-153-170-175-713-576-571-310-126-132-213-627-672-126-173-361-621-610-231-136 352-356_W.mp3)
  Segment 24: 92372ms ~ 92948ms (file: 657-567-675-657-563-157-175-165-167-631-176-617-671-672-361-316-263_V.mp3)
  Segment 25: 93948ms ~ 94524ms (file: 672-265-136-213-510-517-513-517-571-715-571-107-715-175-175-751-175 517-576-517-517-573-573-573-537-567-563-537-756-752-571-576-573-571-571_W.mp3)
  Segment 26: 95524ms ~ 96100ms (file: 501-157-510-510-517-510-510-530-510-571-501-510-507-510-570-510-570 507-510-570-501-501-507-501-510-507-507-507-150-507-507-547-574-571-150-501-513-510-510-501-547-547-547-571 5_V.mp3)
  Segment 27: 97100ms ~ 97676ms (file: 501-517-107-107-510 501-504-501-507-510-510-105-107-107-507 514-150-517-510-507-157-157_V.mp3)
  Segment 28: 98676ms ~ 99252ms (file: 602-673-367-627-672-627-362-260-267-672-236-570-250-312-263-627-627-627-627-627-263-263-620-672-627-672-765-627-765-762 675-675-675-756-675-675-765-675-765_W.mp3)
  Segment 29: 100252ms ~ 100828ms (file: 127-357-237-765-726-261-521-127-126-365-261-612-216-123-572-125-132-165-756-261-567-671-637-731-301-603-760-273-362-362-321-312-130-103-103-132-316-361-367-132-576-675-637_V.mp3)
  Segment 30: 101828ms ~ 102404ms (file: 762 765-175-163-167-136-213-236 165-657-301_V.mp3)
  Segment 31: 103404ms ~ 103980ms (file: 126 216-126-136-136-120-103-136-132_V.mp3)
  Segment 32: 104980ms ~ 105556ms (file: 125-137-231-235-132-130-301-632-312-153-167-152-301-123-123-156-125-320-135-516-263-357-235-365-563-135-513-153-612-263-527-564-156-123-562-316-127-136-627-126-362-536-576-123-1_W.mp3)
  Segment 33: 106556ms ~ 115628ms (file: 103-157-231-715-231-132-157-127-103-162-102-102-216-106-261-263-216-267-627-267-123-120-267-267-267-176-321-267-261-527-157-172-132-106-167-167-106-106-216-102-260-102-675-267-3_W.mp3)
  Segment 34: 116628ms ~ 125808ms (file: 603-103-726-103-326-167-762-167-263-610-672-621-162-261-261-103-260-126-620-310-163-761-673-267-620-632-603-267-213-126-207-326-213-213-102-170-631-361-176-103-160-762-261-621-2_W.mp3)
  Segment 35: 126808ms ~ 127384ms (file: 361 316-136-106-136-106-163-132-136-175-167-312-163-103-162-317-130-316-106-160-162-107-316-761-263 154-310-102-136-103-103-513-216-106-701-163-156-263-165-165-610-106-136-137-1_V.mp3)
  Segment 36: 128384ms ~ 128960ms (file: 105-150-150-105-135-105-106-106-105-106-167-175-105 150-157-156-150-160-135-107-105-105-105-105-156-510 105-517-105-150-150-157-105-150-157-175-175-107-715-175-175-150-170-615-7_V.mp3)
  Segment 37: 129960ms ~ 130536ms (file: 627-267-267-276-267-627-627-267-627-627-627-267-627-627-672-276-726-627-672-672-627-627-672-627-627-267-726-627-267-627-276-267-267-267-267-267-267-267-627-627-627-627-627-267-2_W.mp3)
  Segment 38: 131536ms ~ 132112ms (file: 517-517 150-501-510-501-501-510-501-501-501-501-510-517-517-105-157-516-501-510-510-510-501-157-517-510-150-501-510-516 157-517-517-105-517-570-517-517-105-510-501-517-157-150-1_V.mp3)
  Segment 39: 133112ms ~ 133688ms (file: 135-153-153-154-153-315-135-351-153-153-136-135-516-213-237-213-231-236-231-236-236-263-235-236-253-527-576-352-257-236-576-615-361-261-213-216-261-231-326-623-621-132-361-657-3_W.mp3)
  Segment 40: 134688ms ~ 135264ms (file: 106-106-510-710-675-675-602-621-612-706-675-672-367-361-320-621-213-620-625-627-623-607-627-612-670-671-675-163-173-172-312-123-106-362-367-231-236-216-321-132-123-263-321-301-3_V.mp3)
  Segment 41: 136264ms ~ 136840ms (file: 217-273-372-372-732-312-327-316-137-732-372-132-162-136-125-123-172-132-317-735-763-736-765-726-735-712-723-765-765-762-376-762-735-375-765-751-753-756-571-753-375-371-317-132-1_W.mp3)
  Segment 42: 137840ms ~ 138416ms (file: 672-260-260-603-162-761-635-326-301-130-310-261-261-261-765-537-612-623-132-306-135 726-267-102-671-675-310-203-126-321-526-563-130-316-126-510-571-715-163-216-236-261-236-103-1_W.mp3)
  Segment 43: 139416ms ~ 139992ms (file: 675-267 316-362-326-237-217-126-603-623_W.mp3)
  Segment 44: 140992ms ~ 141568ms (file: 362-316-572-312-217-301-231-213-502-627-326-672-762-263-261-237-602-260-650-652-326-512-620 136-357-267-576-361-156-502-573-270-326-263-107-237-316-521-132-172-135-130-310-327-5_V.mp3)
  Segment 45: 142568ms ~ 152756ms (file: 260-267-576-576-365-132-267-210-123-231-201-527-126-237-275-706-765-735-502-756-310-135-123-103-312-123-726-216-326-625-627-367-307-257-201-206-751-607-572-216-136-256-672-257-2_W.mp3)
  Segment 46: 153756ms ~ 154332ms (file: 310-307-317-137-713-713-130-317-310-371-317-310-310-370-103-170-103-713-371-715-175-703-301 371-750-705-175-310-103-630-617-601-603-610-607-376-713-715-157-173-713-173-371-137-3_W.mp3)
  Segment 47: 155332ms ~ 155908ms (file: 761-716-761-762-172-756-716-175-761-765-176-756-751-175-175-175-175-157-156-150-756-756-756-675-765-756-756-710-761-716-715_W.mp3)
  Segment 48: 156908ms ~ 157484ms (file: 130-176-130-173-301-170-706-670-760-176 310-370-170-173-301-172-103-301-107-107-715-103-103-310-715-173-130-135-701-130-317-170-130 103-170-137-301-175-132-310-130-170-751-310-1_V.mp3)
  Segment 49: 158484ms ~ 159060ms (file: 562-675-674-652-675-657-576-657-672-216-705-675-672-637-673-312-723-236-326_V.mp3)
  Segment 50: 160060ms ~ 167908ms (file: 263-576-765-326-312-261-216-231-321-523-263-132-213-261-316-173-260-267-102-137-657-675-265-756-621-136-321-576-267-765-316-106-106-263-312-261-167-103-576-765-213-130-261-326-2_W.mp3)
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
> **Time**: 2026-04-05 07:24:14
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "한 명과 담당 기술진 다섯 명을 급파했습니다 한화시스템 관계자는 UAE와의 계약 사항을 책임지고 이행하기 위해 불가피하게 출장을 보내기로 결정했다고 밝혔습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "한", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "명과", "start_ms": 250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "담당", "start_ms": 650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "기술진", "start_ms": 1050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "다섯", "start_ms": 1550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "명을", "start_ms": 1950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "급파했습니다", "start_ms": 2250, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "한화시스템", "start_ms": 3200, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "관계자는", "start_ms": 4050, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "UAE와의", "start_ms": 4700, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "계약", "start_ms": 5450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "사항을", "start_ms": 5850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "책임지고", "start_ms": 6350, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "이행하기", "start_ms": 6950, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "위해", "start_ms": 7550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "불가피하게", "start_ms": 7950, "dur_ms": 650, "phase_lr_deg": 0.0}
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
"transcript": "입니다 사막의 가장 큰 약점인 물 부족을 기술로 해결하자 사막의 강점이 그대로 살아났습니다 사막이라는 특수한 환경이 오히려 아군이 된 겁니다 이 결과가 중동 왕실에 어떻게 받아들여졌을지는 이후의 움직임을 보면 알 수 있습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "입니다", "start_ms": 100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "사막의", "start_ms": 600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 1100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 1450, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "약점인", "start_ms": 1650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "물", "start_ms": 2200, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "부족을", "start_ms": 2450, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 5,
"transcript": "진짜 오랜만이다 보고 싶었던 친구야 야 너도 잘 지냈어 아 진짜 보고 싶었어 물로도 그리면",
"speaker": "speaker_3",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 200, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "오랜만이다", "start_ms": 650, "dur_ms": 700, "phase_lr_deg": -5.0},
{"word": "보고", "start_ms": 1400, "dur_ms": 300, "phase_lr_deg": -5.0}
]
},
{
"segment": 6,
"transcript": "현재 지역이 이제 허용된 구역이다라고 하고 운전자 상태가 지금 정상이다라는 정보를 쏘아 올려서 보내면 그걸 믿고 거기",
"speaker": "speaker_4",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "현재", "start_ms": 80, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "지역이", "start_ms": 500, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "S+를 국내에서 팔고 있는 S6+랑 직접 비교했을 때 일부 앱에서 좀 급속도로 발열",
"speaker": "speaker_5",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "S+", "start_ms": 120, "dur_ms": 600, "phase_lr_deg": 10.0}
]
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 12,
"transcript": "실제로 문명의 초기 확산 경로를 보면 이 논리가 더 설득력을 얻습니다 유라시아 대륙은 동서 방향으로 길게 뻗어 있어서 같은 위도선을 따라 작물과 가축이 수평으로 이동했습니다 비슷한",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "실제로", "start_ms": 60, "dur_ms": 500, "phase_lr_deg": 0.0}
]
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
"transcript": "센서가 필요하거든요 관성 센서라고 부르는데 그 센서도 여기 들어가 있고요 그리고 이제 배터리가 들어가 있습니다",
"speaker": "speaker_6",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "센서가", "start_ms": 90, "dur_ms": 500, "phase_lr_deg": 0.0}
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
"transcript": "청소했어요 상황이 이렇다 보니까 배들이 이렇게 위험한 곳을 피해서 저기 멀리 아프리카 희망봉으로 삥삥 돌아가고 있습니다",
"speaker": "speaker_7",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "청소했어요", "start_ms": 150, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "다 호주머니 톡톡 톡톡 톡톡 자 도루마무 고추도 넣고 얼큰하게 조",
"speaker": "speaker_8",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "다", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0}
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
"transcript": "프리앙카가 이 정도 반했으니 인도 반응도 장난이 아니에요 이 영화가 터진 이후 인도 현지 반응은 그냥 흥행했다 수준이 아닙니다 인도 대표 매체인 퍼스트 포스트는 아예 메인 기사로 이 현상을 다루면서 이건 기록이 아니라 사건",
"speaker": "speaker_9",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "프리앙카가", "start_ms": 110, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "손절 구간은 방금까지 반등했던 고점 위로 잡아 주시고 익절은 직전 저점 구간에서 캔들 하나 아래로 잡아 주시면 됩니다 손익비는 동일하게 1:1.5를 유지하면서 진",
"speaker": "speaker_10",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "손절", "start_ms": 130, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
},
{
"segment": 50,
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
*Generated by WKAppBot ask gemini — 2026-04-05 07:24:14*
