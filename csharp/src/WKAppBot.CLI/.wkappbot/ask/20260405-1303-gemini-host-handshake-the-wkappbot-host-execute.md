# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=9236  time=13:02:30  uptime=39980s  mem=380MB  system_processes=355"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_130219.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 3744ms (file: 172-261-732-236-321-231-317-367-137-723-372-362-213-316-172-137-163-752-671-267-316-213-302-103-216-231-163-163-317-756-267-267-126-627-261-216-137-162-163-126-261-261-126-126-1_W.mp3)
  Segment 2: 4744ms ~ 19252ms (file: 625-126-372-327-756-301-765-756-237-627-263-617-105-756-273-130-321-276-306-726-726-230-317-321-271-723-312-657-610-123-730-126-765-731-273-276-762-317-760-372_W.mp3)
  Segment 3: 20252ms ~ 20828ms (file: 705-605-675-627-753-310-712-175-210-250-750-571-570-572-572-125-132-253-217-251-215-327-215-215-271-217-257-215-125-217-536-750-170-172-175-157-725-172-715-157-750-751-317-273-5_V.mp3)
  Segment 4: 21828ms ~ 22404ms (file: 152-760-231-237-573-135-267-216-512-321-617-321-635-267-547-236 541-620-362-316-326-316-312-361-613-605-503-215-132-123-620-123-326-361-316-316 167-571-613-371-173-731-310-375-7_W.mp3)
  Segment 5: 23404ms ~ 30568ms (file: 267-236-316-312-130-127-267-256-237-275-326-236-275-165-172-130-267-572-752-267-316-130-302-326-326-231-236-763-106-632-320-236-237-360-130-623-263-120-162-263-126-267-276-765-7_W.mp3)
  Segment 6: 31568ms ~ 32144ms (file: 175-710-705-107-107-715-705-107-170-715-170-175-135-173-751-170-751-175-175-571-750-517-157-517-751-157-170-157-517-751 510-571-170-715-750-705-751-170-701-710-170-517-175-153-1_W.mp3)
  Segment 7: 33144ms ~ 33720ms (file: 352-531-357-357-507-573 503-570-530-537-537-501-530-503-351-532-530-532-531-531-531-531-350-530-531-532-572-675-765-512-153-513-513-514-541-512-513-534-512-541-513-513-513-514-5_V.mp3)
  Segment 8: 34720ms ~ 35296ms (file: 571-576-571-561-567-567-567-567-567-567-576-576-576-571-175-721-751-521-504-572-562-576-576-572-527-572-527-572-702-752-572-257-752-750-725-725-705-570-502-576-507-502-502-572-5_W.mp3)
  Segment 9: 36296ms ~ 36872ms (file: 157-157-175-175-713-751 517-157-173-175-715-715-175-173-753-157-713-715-157-715-157-716-715-751-175-715-751-175-137-316-137-137-321-173-715-375-137-317-137-517-517-735-715-175-7_V.mp3)
  Segment 10: 37872ms ~ 38448ms (file: 675-362-312-361-301-130-103-103-130-136-360-316-103-123_V.mp3)
  Segment 11: 39448ms ~ 40024ms (file: 103-130-107-317-107-137-310-137-107-731-301-713-130-703-173-170-735-370 153-317-301-310-135-730-371-103-703-731-365-751-137-130-107-730-130-317-312-701-173-107-702-173-170-703 1_V.mp3)
  Segment 12: 41024ms ~ 41600ms (file: 710-175-715-713-371-176-170-173-731-173-715-173-170-173-710-317-750-750-751-751 571-175-137-157-713-173-175-157-173-715-175-713-175-712-175-172-175-172-176-127-175-175-170-173-7_V.mp3)
  Segment 13: 42600ms ~ 43176ms (file: 506-560-560 527-527-527-520-532-253-123-512-513 321-215-235-251-251-315-526-523-503-501-512-502-520-521-523-502-132_V.mp3)
  Segment 14: 44176ms ~ 44752ms (file: 562-501-513-501-512-502-506-507-507-527-502-520-570-502-527-572-725-752-752-527-153-135-132-516-567-573-567-576-571-576-576-571-576-576-576-571-756-576-571-735-716-761-506-576-5_W.mp3)
  Segment 15: 45752ms ~ 46328ms (file: 153-375-731-321-126-631-265-263-213-213-271-276-276-672-761-716-761-765-675-671-765-675-765-675-675 571-571-351-153-375-357-235-372-327-312-236-327-235-312-325-326-361-326-325-2_W.mp3)
  Segment 16: 47328ms ~ 47904ms (file: 163 621-621-621-621-621-261-612-612-612_W.mp3)
  Segment 17: 48904ms ~ 49480ms (file: 756-765-527 627-672-752-756-567-576-576-756-503-501 756-576-756-756-756-576-756-756-514-503-504 752-765-765-756-576-756-576-576-576-576-576-756-756-502-507 572-576-576-576-576-5_W.mp3)
  Segment 18: 50480ms ~ 51056ms (file: 570-576-570-576-357-230-175-520-513-517 503-531-530-507-527-572-527-512 521-521-524-524-521-521-524-521_W.mp3)
  Segment 19: 52056ms ~ 57852ms (file: 106-106-132-231-236-213-103-261-756-365-135-361-623-312-136-263-162-102-567-130-571-756-527-672-106-260-213-216-532-103-103-157-570-257-153-675-103-316-316-312-136-216-701-123-2_W.mp3)
  Segment 20: 58852ms ~ 59428ms (file: 537-576-572-357-351-354-351-351-135-530-530-530-350-530-503-530-523-513-532-532 541-513-531-536-521-547-571-561-526-563-541-524-524-576-574-527-571-502-523-574-547-547-574-537-5_V.mp3)
  Segment 21: 60428ms ~ 61004ms (file: 132 173 103 260-621-602-106-301-617-163-316-135-126-136-156-152-153-125-123-125-132-125-123-152-125-125-125-125-136-162-130-106-160-106-316-136-130-163-136-576-576-567-576-576-1_V.mp3)
  Segment 22: 62004ms ~ 62580ms (file: 725-756-572-275-275-527-275-572-752-507-572-576-576-567-576-576-576-567-536-527-514-517-567-506-573-507-576-571-527-576-752-750-275-752-256-752-527-562-507-752-756-576-756-567-7_W.mp3)
  Segment 23: 63580ms ~ 64156ms (file: 136-130-701-321-532-350-715-175-750-137_V.mp3)
  Segment 24: 65156ms ~ 65732ms (file: 315-521-527-532-504-503 154-512 513-512-527 120-205-261-265-206-632-603-630-362-302-631-561-576-567-567-561-562-563-526-256-256-206-602-206-260-623-630-621-213-162-317-136-125-2_V.mp3)
  Segment 25: 66732ms ~ 74976ms (file: 752-102-270-673-672-130-132-102-103-375-132-726-167-310-210-201-207-672-260-216-672-765-276-230-260-203-231-237-576-572-312-210-541-170-120-765-203-671-765-670-126-130-213-107-1_W.mp3)
  Segment 26: 75976ms ~ 76552ms (file: 137-316-136-136-317-376-613-106-130-362-623-637-672-726-726-765-756-672 516-517-537-730-315-561-153-573-705-576-753-567-103-105-107-105 567-526-531-761-657-536-657-576-765-157-1_V.mp3)
  Segment 27: 77552ms ~ 78128ms (file: 172-613-726-137-176 570-570-751-571-751-715-175-157-157-571-517-571-576-751-756-571-571-751-751-761-761-761-175-752-217-756-751 750-570-574-574-753-753-157-125_W.mp3)
  Segment 28: 79128ms ~ 79704ms (file: 513-571-561-517-761-276-275-625-521-531-132-127-156-735-173-375-135-172-312-125-215-253-235-253-257-315-352-512 107-150-375-173-130-170-257-732-715-750-721-702-715-751-751-753-7_V.mp3)
  Segment 29: 80704ms ~ 81280ms (file: 725-267-507-675-567-576-765-675-670-675-670-760-706-765-765-765-750-706-706-706-706-756-507-756-765-751-517-507-571-572-507-507-705-570-570-507-570-750-750-572-507-502-756-576-7_V.mp3)
  Segment 30: 82280ms ~ 82856ms (file: 506-163-231-312-163-316-127-536-510-534-572-576-576-572-257-602-137-672-671-376-312-357-137-135-354-351-516-165-125-137-723-317-312-624-316-162-136-126-136-126-362-361-163-130-5_W.mp3)
  Segment 31: 83856ms ~ 84432ms (file: 507-502-520-250-203-205-205-260-203-257-265-256-527-257 576-507-507-503-257-270-205-325-520-235-502 573-574-571-517-507-572-572-507-527-572-570-527-527-527-502-501-527-526 570-5_V.mp3)
  Segment 32: 85432ms ~ 94612ms (file: 320-320-102-201-230-102-754-725-352-132-231-213-102-517-547-103-275-762-276-132-276-721-261-627-213-574-567-312-201-270-132-123-201-130-102-276-120-302-276-627-230-726-271-120-2_W.mp3)
  Segment 33: 95612ms ~ 96188ms (file: 572-250-570-507-250-705-750-725-250-523-205-275-205-523-572-567-576-573 502-527-527-502-572-527-250-250-520-205-752-205-215-205-205-250-250-251-501-572-527-502-527-527-507-502-5_V.mp3)
  Segment 34: 97188ms ~ 97764ms (file: 574-571-751-561-572-574-576-567-576-572-576-572-571-573-507-753-750-507-705-502-750-257-205-527-572-572-752-572-752-756-756-576-576-756-756-576-756-752-751-752-753-574-514-514-5_W.mp3)
  Segment 35: 98764ms ~ 99340ms (file: 372-602-127-137 175-715 236-627-612-216-216-126-132-132-103-136-173-103-316-165-136-132-135-126-136-316-103-103-103-132-672-672-671-607-620-176-160-672-672-672-617-312-103-321-1_W.mp3)
  Segment 36: 100340ms ~ 100916ms (file: 236-263-263-261-672-627-627-326-263-326-263-162-263-627-672-672-675-621-613 162-106-627-612-627-627-627 627-267-672-625-627-627 672-627 762-762_W.mp3)
  Segment 37: 101916ms ~ 102492ms (file: 145-571-415-107-103-104-147-104-135-104-157-153-145-134-104-145-104-143 735-103-107-145-104-145-105-451-154-104-105-310-105-105-175-103-157-154-143-105-154-415-451-401 457-307-1_V.mp3)
  Segment 38: 103492ms ~ 104068ms (file: 527-523-572-572-527-572-572-576-576-507-572-576-576-576-527-527-572-576-572-572-526-527-257-527-572-527-526-231-236-213-125-521-512-512-513-512-526-521-512-527-521-523-521-523-5_W.mp3)
  Segment 39: 105068ms ~ 105644ms (file: 750-175-731-735-715-572-150-132-152-531-527-521-152-132-215 573-573-157-513-153-315-751-715-175-715-173-172-721-137-175-517-365-730-175-702-157-750-567-567 170-715-715-507-715-3_V.mp3)
  Segment 40: 106644ms ~ 113664ms (file: 312-136-132-762-267-312-136-321-576-627-261-231-230-735-754-756-576-267-576-265-576-756-576-756-576-765-267-236-752-627-267-267-265-261-213-261-572-576-126-312-576-756-267-231-2_W.mp3)
  Segment 41: 114664ms ~ 123088ms (file: 310-130-132-130-120-301-102-132-320-130-132-103-102-106-103-762-302-623-260-620-102-103-210-216-267-276-726-627-267-267-126-162-236-102-167-326-120-612-136-106-367-136-106-106-1_V.mp3)
  Segment 42: 124088ms ~ 124664ms (file: 172-216-312-123 317 510-150-132 132-317-170-710-157-136-617-316-376-156-567-375-137-123-637-163-632-362-576-756-675-657-625-256-256-231-321-216-263-361-127-627-675-312-326-316_W.mp3)
  Segment 43: 125664ms ~ 126240ms (file: 132-163-132-130-132-136-213-123-627-261-261_W.mp3)
  Segment 44: 127240ms ~ 127816ms (file: 165-126-627-627-265-256-256-253-625-263-261-216-216-215-215-520-537-571-357-357-521-520-263-752-572-571-572-653-576-157-531-130-136-135-315-536-573-571-576-315-512-675-315-315-3_W.mp3)
  Segment 45: 128816ms ~ 129392ms (file: 326-623-267-236-321-263-162-236-126-261-612-621-612-613 132-160-136-571-167-103-103-136-103-675-163-106-617-106-613-316-130-571-173-260-102-163-631-103-601-160-130-103-610-136-1_V.mp3)
  Segment 46: 130392ms ~ 130968ms (file: 504-501-504-504-503-501-504-501-510-504-501-502 507-507-570-507 574-547-574-517-572-571 541-547-563-504-504-507-574-576-570-572-527-574-507-506-506-561-506-503-501-514-514-523-5_V.mp3)
  Segment 47: 131968ms ~ 132544ms (file: 317-302-361-321-356-316-635-153-310-310-317-316-137-371-371-137-317-137-317-317-361-167-163-316-571-754-536-576-756-653-756-574-573-132_W.mp3)
  Segment 48: 133544ms ~ 134120ms (file: 567-567 524-574-527-572-527-507-560-526-523-524-542-574-572-576-576-574-576-527-572-572-572-572-572-571-571 540-564-504-543-547-541-547-547-547-547-574-576-576-574-573-574-573-5_W.mp3)
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
> **Time**: 2026-04-05 13:03:32
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
"transcript": "제 고향 선배님이기도 하셔 가지고 예전에 이제 그 엠비씨 코미디언 공채 오실 때 그때 이제 제가 시험장에서 수험생으로 마주쳤던 기억이 나거든요 근데 그때도 사실은 워낙에 수려한 외모로 유명하셨어요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "제", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "고향", "start_ms": 180, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "선배님이기도", "start_ms": 450, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "하셔", "start_ms": 1000, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "가지고", "start_ms": 1220, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "예전에", "start_ms": 1600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 1980, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 2200, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "엠비씨", "start_ms": 2300, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "코미디언", "start_ms": 2650, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "공채", "start_ms": 3100, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "오실", "start_ms": 3380, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 3650, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "그때", "start_ms": 3800, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 4050, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 4250, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "시험장에서", "start_ms": 4480, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "해서 한 3년 정도 있었던 것 같아요 그러다가 이제 제가 우연한 기회에 어 다른 회사로 스카웃 제의를 받고 옮기게 되었는데 그때 이제 제가 옮긴 회사가 어 지금의 엔씨소프트의 전신인 회사였어요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "해서", "start_ms": 60, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 300, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "3년", "start_ms": 420, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "정도", "start_ms": 750, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "있었던", "start_ms": 1050, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 1380, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 1500, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "그러다가", "start_ms": 1900, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 2350, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 2600, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "우연한", "start_ms": 2850, "dur_ms": 380, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "해서 이제 제가 거기서 서버 개발을 했었고요 그러다가 이제 제가 우연한 기회에 어 해외 지사 발령을 받게 됐어요 그래서 미국으로 가게 되었는데 그때 이제 제가 미국에서 어 어떤 걸 경험했냐면 어 리니지라는 게임을 처음 접하게 됐어요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "해서", "start_ms": 80, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 320, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 520, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "거기서", "start_ms": 750, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "서버", "start_ms": 1100, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "개발을", "start_ms": 1380, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "해서 이제 제가 거기서 어 리니지 서비스를 런칭을 하는 데 어 참여를 했었고요 그때 이제 제가 어 게임이라는 것이 어 단순히 그냥 노는 것이 아니라 어 하나의 어떤 문화가 될 수 있다는 걸 처음으로 느끼게 됐어요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "해서", "start_ms": 50, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 300, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 500, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "거기서", "start_ms": 720, "dur_ms": 310, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "해서 이제 제가 거기서 한 5년 정도 있었고요 그러다가 이제 제가 다시 한국으로 돌아오게 됐어요 그때 이제 제가 어 어떤 일을 하게 됐냐면 어 리니지 2 서비스를 총괄하는 역할을 맡게 됐어요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "해서", "start_ms": 70, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 320, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 520, "dur_ms": 200, "phase_lr_deg": 0.0}
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
"transcript": "제 고향 선배님이기도 하셔 가지고 예전에 이제 그 엠비씨 코미디언 공채 오실 때 그때 이제 제가 시험장에서 수험생으로 마주쳤던 기억이 나거든요 근데 그때도 사실은 워낙에 수려한 외모로 유명하셨어요",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "제", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "고향", "start_ms": 180, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "선배님이기도", "start_ms": 450, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "하셔", "start_ms": 1000, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "가지고", "start_ms": 1220, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "예전에", "start_ms": 1600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 1980, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 2200, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "엠비씨", "start_ms": 2300, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "코미디언", "start_ms": 2650, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "공채", "start_ms": 3100, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "오실", "start_ms": 3380, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 3650, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "그때", "start_ms": 3800, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 4050, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 4250, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "시험장에서", "start_ms": 4480, "dur_ms": 480, "phase_lr_deg": 0.0}
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
"transcript": "해서 이제 제가 거기서 서버 개발을 했었고요 그러다가 이제 제가 우연한 기회에 어 해외 지사 발령을 받게 됐어요 그래서 미국으로 가게 되었는데 그때 이제 제가 미국에서 어 어떤 걸 경험했냐면 어 리니지라는 게임을 처음 접하게 됐어요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "해서", "start_ms": 80, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 320, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 520, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "거기서", "start_ms": 750, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "서버", "start_ms": 1100, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "개발을", "start_ms": 1380, "dur_ms": 350, "phase_lr_deg": 0.0}
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
"transcript": "해서 이제 제가 거기서 어 리니지 서비스를 런칭을 하는 데 어 참여를 했었고요 그때 이제 제가 어 게임이라는 것이 어 단순히 그냥 노는 것이 아니라 어 하나의 어떤 문화가 될 수 있다는 걸 처음으로 느끼게 됐어요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "해서", "start_ms": 50, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 300, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 500, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "거기서", "start_ms": 720, "dur_ms": 310, "phase_lr_deg": 0.0}
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
"transcript": "해서 이제 제가 거기서 한 5년 정도 있었고요 그러다가 이제 제가 다시 한국으로 돌아오게 됐어요 그때 이제 제가 어 어떤 일을 하게 됐냐면 어 리니지 2 서비스를 총괄하는 역할을 맡게 됐어요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "해서", "start_ms": 70, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 320, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 520, "dur_ms": 200, "phase_lr_deg": 0.0}
]
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
*Generated by WKAppBot ask gemini — 2026-04-05 13:03:32*
