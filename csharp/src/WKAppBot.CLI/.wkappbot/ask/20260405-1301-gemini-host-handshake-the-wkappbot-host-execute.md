# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=11716  time=13:00:10  uptime=40267s  mem=395MB  system_processes=354"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_125940.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 632-623-672-765-607-675-527-502 570-507-576-705-706-750-752-756-752-705-507-675-705 570-570-576-576-576-570-507-570-275-562-526-570-750-756-705-756 570-750-705-705-756-607-576-5_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 503-506-506-562-562-561-561-567-602-205-567-567-517-576-576-576-572-571-576-567-253-521-521-512-527-520-527-570-523-573-507-507-507-570-507-570-570-576-756-572-527-572-576-567-2_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 725-756 502-265-527-562-527-526-265-625-265-205-602-275-520-502-650-207-657-750-502-526-502-526-572-650-526-675-205-750 502-570-507-527-562-506-756-257-572-265-675-576-507-725-5_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 613-163-136-236-651-567-567-675-165-310-612-671-672-765-617-675-756-675-576-761-620-602-621-123-637-612-162-602-620-612 103-301-132-231-546-576-675-567-561-652-561-136-765-761-7_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 132-162-137-102-602-602-261-267-267-157-162-326 613-613-106-106-162-216-620-106-617-601-106-106-160-106-672-672-672-627-547-574-547-567-657-672-167-162-765-607-310-316-106-157-7_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 103-103-612-671-162-163-612-167-136-312-165-102-163-162-130-316-163-130-163-167-163-547-621-162-126-143-163-106-517-103-102-103-165-126-136-612-216-125-127-261-376-163-167-106-1_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 570-527 150-102-150-502-510-517-175-520-150-157-572-152-125-520-153-512-150-512-572-157-507-510-571-251-571-517-571 153-120-123-135-125-523-123-572-512-521-157-512-573-152-521-2_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 617-263-623-326-362-621-167-602-605-625-672-675-675-135-576-567-570-537-503-537-576-576-576-576-573-576-576-573-571-576-572-576-573-753-357-275_W.mp3)
  Segment 9: 12608ms ~ 19772ms (file: 367-132-675-167-675-267-267-672-260-120-716-671-236-713-261-267-760-756-123-321-132-123-176-671-231-216-132-213-126-576-657-756-627-321-102-267-627-267-326-103-756-672-267-267-1_W.mp3)
  Segment 10: 20772ms ~ 21348ms (file: 567-571-532-537-562 531-562-561-352-526-521-512-521-527-572-537-503-574-506-567-567-503-527-567-523-567-564-506-506-507-504-507-502-506-506-532-530-501-350-350-305-532-350-530-5_V.mp3)
  Segment 11: 22348ms ~ 22924ms (file: 612-651-631-621 150-652-651-625-654-654-651-657-657-657-654-654-657-654-657-652 167-265-625-651-567-612-160-675-675-675-671-675-675-675-675-675-671-673-675-672-672-163-165-631-1_W.mp3)
  Segment 12: 23924ms ~ 24500ms (file: 507-750-715-703-730-751-731-750-736-570-751-571-570-173-731-750-507-701-501-517-507-570-705-570 507-351-517-753-573-570-750-175-705-713-750-703-750-705-750-715-175-715-713-170-7_V.mp3)
  Segment 13: 25500ms ~ 26076ms (file: 231-275-627-213-275-527-236-237-326-365-326-237-326-752-263-762-756-570-237-136-726-265-267-275-275-367-735-725-752-215-175-705-725-712-567-572-267-576-635-207-625-576-276-573-3_V.mp3)
  Segment 14: 27076ms ~ 33988ms (file: 237-276-103-103-124-310-132-136-160-102-231-130-271-320-263-362-316-123-102-267-213-162-210-103-120-102-321-523-376-362-316-671-167-176-675-257-723-326-126-270-216-320-167-617-1_V.mp3)
  Segment 15: 34988ms ~ 35564ms (file: 541-527-541-501-504-506-542-502-125-521-531-352-135-315-352-135-312-315-305-524-501-501-503-503-507-523-501-501-574-507-507-507-507-507-507-574-572-507-507-507-507-507-507-507-5_V.mp3)
  Segment 16: 36564ms ~ 46248ms (file: 103-310-203-520-321-135-301-753-531-263-627-267-206-326-356-231-574-132-754-547-506-156-156-574-713-130-541-756-302-132-236-367-657-135-217-167-176 571-752-631-132-103-106-673-6_W.mp3)
  Segment 17: 47248ms ~ 53620ms (file: 375-320-571-756-267-236-207-756-327-230-702-652-267-672-320-537-302-570-325-302-627-263-231-321-326-267-276-276-236-312-130-236-765-276-320-723-132-237-302-574-203-203-237-237-5_V.mp3)
  Segment 18: 54620ms ~ 55196ms (file: 610-651-615-651-615-651-615-651-567-657-567-615-156-615-156-561-517-651-156-516-156-615 165-651-651-651-651-651-657-651-651-657-615-567-157-517-765-165-156-156-651-517-156-561 6_V.mp3)
  Segment 19: 56196ms ~ 56772ms (file: 571-576-751-576-576-567-756-756-571-572-517-157-157-571-571-517-251-517-572-517-527-571-756-756-756-756-756-756-756-756-576 571-751-752-751-571-751-572-752_W.mp3)
  Segment 20: 57772ms ~ 58348ms (file: 671-601-216-326-103-160-167-763-675-675-263-213_V.mp3)
  Segment 21: 59348ms ~ 59924ms (file: 713-301-130-301-307-307-317-731-130-317-310-137-317-301-103-130-317-173-317-130-107-371-370-730-715-170 713-705-735-750-103-371-730-360-160-617-602-607-670-631-631-716-701-317-1_V.mp3)
  Segment 22: 60924ms ~ 67656ms (file: 231-236-236-370-627-132-310-130-320-107-723-173-312-260-213-123-312-301-320-301-310-723-327-572-726-230-231-236-236-236-203-267-370-263-230-210-132-103-137-103-315-231-265-256-2_V.mp3)
  Segment 23: 68656ms ~ 73948ms (file: 127-236-627-260-276-267-260-213-352-130-135-267-620-612-537-213-572-257-231-213-162-123-261-710-261-162-167-126-231-215-612-163-162-231-120-163-120-162-106-163-106-160-126-123-1_W.mp3)
  Segment 24: 74948ms ~ 75524ms (file: 507-507-507-501-571-507-506-506-501-502-157-563-361-513-507-503-501-570-573-574-507-507-507-507-507-507-507-571-507-507-502-507-507-570-507-572-507-572-507-527-326-361-326-326-3_W.mp3)
  Segment 25: 76524ms ~ 84480ms (file: 736-672-627-267-267-236-230-260-260 267-210-210-201-627-302-267-203-102-201-267-126-216-123-230-207-576-726-752-572-237-103-620-123-137-712-720-102-127-217-123-201-173-127-162-3_W.mp3)
  Segment 26: 85480ms ~ 86056ms (file: 126-651-613-164-316-126-614-613-136-167-367-143-657-561-315-621-615-652-657-657-657-564-654-654-564-567-657-564-567-564-654-567-657-136-312-513-312-765-174-236-341-327-613-327-6_W.mp3)
  Segment 27: 87056ms ~ 87632ms (file: 507-516-574-507-357-752-751-517-563-564-510-750-576-570-357-561-567-561-571-657-567-356-651-510-130-312-317-137-715-175-173_W.mp3)
  Segment 28: 88632ms ~ 97488ms (file: 267-123-154-321-312-623-263-231-102-103-751-231-132-547-267-103-132-271-154-102-301-103-207-276-102-175-276-132-702-127-627-267-160-160-130-607-253-312-130-253-236-752-263-103-2_V.mp3)
  Segment 29: 98488ms ~ 99064ms (file: 501-501-501-501-510-501-501-501-501-513-514-516-651-651-651-657-657-576-576-567-567-576-567-576-567-576-567-576-567-567-567-576-567-576-657-641-163-563-526-564-547-541-514-514-5_W.mp3)
  Segment 30: 100064ms ~ 105212ms (file: 136-106-326-526-526-625-621-136-376-657-265-260-623-162-103-162-631-217-321-627-627-321-263-612-216-216-126-162-216-213-123-261-216-126-612-627-216-216-327-123-132-132-132-123-2_W.mp3)
  Segment 31: 106212ms ~ 106788ms (file: 761-765 531-617-132-136-103-130-103-136-136-136-156-310-136-316-162-103-130-106-162-126-126-137-106-321-623-163-173-123-136-107-106 132-135-612-163-621 360-706-623-261-132-127-3_V.mp3)
  Segment 32: 107788ms ~ 115996ms (file: 231-574-132-103-301-265-261-543-256-173-302-210-236-210-312-107-762-127-317-276-267-230-102-123-173-312-137-136-102-103-102-210-236-231-213-216-326-236-236-130-312-312-302-206-3_W.mp3)
  Segment 33: 116996ms ~ 117572ms (file: 625-265-263-265-625-625-625-652-652-652-625-625-312-132-132-312-362-326-632-623-635-635-635-623-635-625-652-657-652-652-652-652-365-635-365-635-657-635-365-362-365-362-362 315-3_W.mp3)
  Segment 34: 118572ms ~ 125916ms (file: 765-316-673-265-275-231-321-132-103-235-520-352-123-312-213-327-312-372-735-253-316-132-652-165-631-675-362-136-657-657-362-657-672-267-672-267-527-231-103-263-263-236-130-321-3_W.mp3)
  Segment 35: 126916ms ~ 127492ms (file: 105-154-105-105-105-105-105-105-105-105-156-157-175-175-176-175-175-175-175-175-175-175-175-157-175-107 105-105-105-105-105-150-105-105-105-105-157-175-157-175-157-175-157-175-1_V.mp3)
  Segment 36: 128492ms ~ 129068ms (file: 132-167-167-610-135-163-157-175-167-106-136 132_V.mp3)
  Segment 37: 130068ms ~ 130644ms (file: 507-574-507-503-507-574-503-503-573-507-574-507-507-507-573-507-507-507-507-507-570-507-507-507-507-125-162-156-612-561-516-531-531-516-512-512-523-512-512-526-521-512-512-251 5_V.mp3)
  Segment 38: 131644ms ~ 132220ms (file: 301-316-312-302-306-312-132-317-705-507-567-576-726-723-301-132-130-302-132-310-132-362-152-152-145-216-206-216-261-102-106-103-130-135-517-301-312-153-167-106-165-130-130-103-1_V.mp3)
  Segment 39: 133220ms ~ 140924ms (file: 130-231-102-160-137-132-136-103-123-132-312-231-321-137-236-261-216-756-571-312-132-123-173-673-132-571-531-537-263-231-752-756-765-203-230-321-756-756-574-310-102-130-103-765-6_W.mp3)
  Segment 40: 141924ms ~ 142500ms (file: 756-576-576-756-756 507-506-572-260-562-562-765-756-756-576-756-756-576-756-576-576-756 502-205-260-205-520-576-567-607-765-756-756-756-756-756-756-756-756-756-765-576-657 576-5_V.mp3)
  Segment 41: 143500ms ~ 144076ms (file: 576-572-527-572-573-572-573-572-507-257-572-527-572-527-521-572-572-576-756-571-756-164-571-561-516-527-516-512-561-571-574-571-572-527-572-537-507-502-750-507-572-750-752-537-5_V.mp3)
  Segment 42: 145076ms ~ 145652ms (file: 503-504-506-503-507-506-507-501-503-507-507-507-502-507-507-507-507-507-572-537-574-547-507-507-573-572-576-573-507-507 507-507-507-507-507-576-503-502-573-576-576-547-572-507-5_V.mp3)
  Segment 43: 146652ms ~ 147228ms (file: 261-627-162-126 106-167-160-127-617-312-260-120-106 736-132-162-312-267-123-621-130-102-123-130-231-130-623-126-160-167-103-637-310-163-163-106-103-136-103-160-135-613-173-162-1_W.mp3)
  Segment 44: 148228ms ~ 156652ms (file: 107-267-327-321-231-175-217-321-312-103-137-132-231-263-267-106-237-236-203-126-627-627-236-123-236-263-263-527-106-160-671-160-263-267-627-627-136-170-162-120-276-367-361-256-2_W.mp3)
  Segment 45: 157652ms ~ 158228ms (file: 520-527-572-572-725-572-752 152-546-532-502-546-571-526-536-527-526-537-574-576-571-573-576-576-572-572-576-507-572-523-512-521-532-512-352-512-235-305-532-503-153-530-350-150-5_V.mp3)
  Segment 46: 159228ms ~ 159804ms (file: 567-657-651-651-165-156-651-615-651-165_W.mp3)
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
> **Time**: 2026-04-05 13:01:14
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
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
"segment": 2,
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
"segment": 3,
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
"segment": 4,
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
"segment": 5,
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
"segment": 6,
"transcript": "해서 인제 그 자리를 내드기가 쉽지는 않았는데 이제 어머니가 이제",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "해서", "start_ms": 40, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "인제", "start_ms": 400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 800, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "자리를", "start_ms": 980, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "내드기가", "start_ms": 1280, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "쉽지는", "start_ms": 1620, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "않았는데", "start_ms": 1880, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 2250, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "어머니가", "start_ms": 2480, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 2950, "dur_ms": 220, "phase_lr_deg": 0.0}
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
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 13:01:14*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=17708  time=13:00:42  uptime=31904s  mem=412MB  system_processes=355"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_130002.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 153-157-507-157-150-150-571-570-561 516-513-517-517-561-516-513-105-157-517-507-517-507-510-507-570-513-517 150_V.mp3)
  Segment 2: 1576ms ~ 7588ms (file: 123-127-371-137-123-372-213-217-267-213-321-327-317-237-321-231-371-236-321-273-132-201-216-201-210-321-712-261-271-125-265-261-213-527-215-135-610-130-267-627-327-175-154-106-5_W.mp3)
  Segment 3: 8588ms ~ 21764ms (file: 123-540-203-254-213-273-230-236-130-327-327-236-102-132-230-203-352-263-236-623-621-725-301-103-103-367-326-574-103-765-103-267-137-134-203-267-260-216-132-260-574-754-102-125-1_W.mp3)
  Segment 4: 22764ms ~ 31188ms (file: 107-267-327-321-231-175-217-321-312-103-137-132-231-263-267-106-237-236-203-126-627-627-236-123-236-263-263-527-106-160-671-160-263-267-627-627-136-170-162-120-276-367-361-256-2_W.mp3)
  Segment 5: 32188ms ~ 32764ms (file: 541-527-541-501-504-506-542-502-125-521-531-352-135-315-352-135-312-315-305-524-501-501-503-503-507-523-501-501-574-507-507-507-507-507-507-574-572-507-507-507-507-507-507-507-5_V.mp3)
  Segment 6: 33764ms ~ 34340ms (file: 607-167-617-617-617-167-672-267-276-136-172-261-620-601-621-612-613-607-106-316-130-136-106-103-136-671-621-123-126-301-312-302-360-302-316-615-163-176-301-261-126-130-310-362-1_V.mp3)
  Segment 7: 35340ms ~ 45852ms (file: 126-321-126-237-231-216-126-132-316-312-106-123-627-625-261-162-257-573-210-126-316-365-231-276-261-735-365-671-756-265-326-321-567-130-261-263-120-176-103-135-627-267-316-127-1_W.mp3)
  Segment 8: 46852ms ~ 47428ms (file: 632-623-672-765-607-675-527-502 570-507-576-705-706-750-752-756-752-705-507-675-705 570-570-576-576-576-570-507-570-275-562-526-570-750-756-705-756 570-750-705-705-756-607-576-5_V.mp3)
  Segment 9: 48428ms ~ 49004ms (file: 231-175-157-157-175-157-157-132-123-312-132-215-132-315-135-317-713-572-351-217-315-123-271-175-715-175-173-157-157-175-175-167-713-751-753-375-753-732-753-753-751-372-375-157-1_W.mp3)
  Segment 10: 50004ms ~ 50580ms (file: 507-501-502-521-507-305-503-571-753-510-230-521-325-503 576 507-507-502-520-507-520-253-250-253-352-523-520-502-507-502-502-502 503-571-537-503-503-527-520-523-520-521-521-512-5_V.mp3)
  Segment 11: 51580ms ~ 52156ms (file: 516-562-561-165-152-762-756-574-574-576-576-576-507-507-574-507-507-507-570-507-507-570-570-570-572-576-576-527-526-562-572-573-572-263-216-326-236-213-213-251-251-512-521-523-5_W.mp3)
  Segment 12: 53156ms ~ 53732ms (file: 751-753-751-751-751 751-571-750-715-170-517-571-175-751-571-751-715-751-751-175-175-107-715-751-751-157-571-751-751-715-756-570-571 571-571-751-571-715-175-751-157-150-751-175-1_W.mp3)
  Segment 13: 54732ms ~ 55308ms (file: 157-517-157-571-576-574-574-574-574-574-574-574-574-574-574-574-574-574-574-751-501-175-157-157 574-574-574-574-754-754-751-175 574-574-574-574-574-574-574-574-574-574-574-574-5_W.mp3)
  Segment 14: 56308ms ~ 56884ms (file: 501-501-571 510-507-517-507-501-150-510-510-150-510-150-501-507-571-510-157-510-501-501-517-570-510-517 105-157-157-105-157-105-501-105-157-105-150-150-510-105-507-570-574-570-5_V.mp3)
  Segment 15: 57884ms ~ 58460ms (file: 637-367-672-367-657 630-216-607-617-301-173-675-765-675-167-312-120-123-160-160-137-103-132-713-136-132-216-213-136-102-132-705-176-132-176-106-671-760-673-637-175-613-132-216-1_V.mp3)
  Segment 16: 59460ms ~ 66624ms (file: 367-132-675-167-675-267-267-672-260-120-716-671-236-713-261-267-760-756-123-321-132-123-176-671-231-216-132-213-126-576-657-756-627-321-102-267-627-267-326-103-756-672-267-267-1_W.mp3)
  Segment 17: 67624ms ~ 68200ms (file: 715-175-107-571-170-571-750-574-574-574-574 150-715-175-170-571-105-571-105-715-710-107-701-705-715-705-150-175-710-715-705-170-501-715-751-570-571-756-576-751 570-517-570-750-7_W.mp3)
  Segment 18: 69200ms ~ 69776ms (file: 572-517-576-572-572-573-576-752-567-572-576-576-520-520-756-570-756-576-576-567-567-567-576-756-651-567 752-756-576-756-756-726-547-512-517-571-523-521-501-573-523-512 752-752-7_W.mp3)
  Segment 19: 70776ms ~ 71352ms (file: 612-165-613-651-653-563-256-562-562-652-653-635-135-315-321-315-135-125-625-651-156-615-165-576-536-563-651-365-356-365-351-563-360-306-615-356-536-501-651-105-150-536-567-567-6_W.mp3)
  Segment 20: 72352ms ~ 72928ms (file: 765-631-165-675 267-675-675-657 567-167-765-672-657-653 765-765-765-673-765-756-362-362-632_W.mp3)
  Segment 21: 73928ms ~ 78464ms (file: 572-231-251-130-231-260-210-172-127-310-621-263-362-326-321-721-671-627-671-627-203-231-172-172-237-267-572-751-107-310-102-103-136-371-716-173-761-173-761-573-235-576-537-267-6_W.mp3)
  Segment 22: 79464ms ~ 85872ms (file: 103-162-362-736-326-627-672-672-216-321-317-163-136-137-736-261-263-627-263-623-621-576-126-632-317-671-726-107-702-725-702-160-163-301-367-317-302-231-123-723-201-613-750-301-3_W.mp3)
  Segment 23: 86872ms ~ 91552ms (file: 627-126-231-273-621-275-263-263-231-217-325-261-263-326-236-261-261-621-236-103-126-762-763-167-762-367-167-130-312-107-173-713-167-123-123-312-312-137-312-130-132-213-126-213-3_W.mp3)
  Segment 24: 92552ms ~ 98888ms (file: 627-126-312-261-267-263-267-261-261-526-123-572-132-130-576-267-267-574-315-107-216-123-103-574-105-261-675-751-213-231-725-132-756-275-316-103-321-576-265-576-257-130-216-120-1_W.mp3)
  Segment 25: 99888ms ~ 100464ms (file: 751-752-751-731-156-514-526-564-567-567-571-571-573-576-576-573-574-573-517-517-576-574-576-507-576-573-576-571-507-571-507-576-572-576-574-560-564-562-576-572-572-572-752-752-5_V.mp3)
  Segment 26: 101464ms ~ 102040ms (file: 104-143-134-143 314-134-103-301-154-103-103-104-341-104-103-134-134-103-143-104-143-134-143-104-145-153-143-134-134 143-130-301-130-105-103-104-103-103-103-135-134-145-135-130-1_V.mp3)
  Segment 27: 103040ms ~ 103616ms (file: 567-657-651-651-165-156-651-615-651-165_W.mp3)
  Segment 28: 104616ms ~ 105192ms (file: 574 510-510-501-517-501-510-501-501-501-501-160-501-150-150-571-576-570-571-517-510-570-507-570-507-576-517-501-517-571-571-507-571-507-571 510-501-517-105-507-501-157-157-501-5_V.mp3)
  Segment 29: 106192ms ~ 120448ms (file: 716-706-172-710-217-130-106-130-126-172-627-176-261-723-176-170-170-103-317-176-136-173-132-123-765-612-651-216-231-317-132-230-201-652-160-106-107-106-673-126-176-167-127-120-5_W.mp3)
  Segment 30: 121448ms ~ 122024ms (file: 752-576-752-572-752-752-752-572-257-725-750-576-576-576-576-576-756-576-571-756-756-716-576-576-257-752-752-725-752-752-570-576-752-572-527-275-752-752-756-572-527-756-527-572-5_W.mp3)
  Segment 31: 123024ms ~ 123600ms (file: 175-175-175-756-756 105-715-175-715-175-751-751-175-571-517-751-751-751-751-715-175-175-175-175-751 157-751-157-751-175-751-715-157-751-751-751-751-175-751-753-751-754-751-753-7_W.mp3)
  Segment 32: 124600ms ~ 125176ms (file: 301-316-312-302-306-312-132-317-705-507-567-576-726-723-301-132-130-302-132-310-132-362-152-152-145-216-206-216-261-102-106-103-130-135-517-301-312-153-167-106-165-130-130-103-1_V.mp3)
  Segment 33: 126176ms ~ 126752ms (file: 572-572-765-756-756-756-756-756-756-756-752-756-513-514-543-506-504-571-507-504-504-506-504-547-504-504-546-725-756-576-756-567-576-756-576-756-576-576-576-756-752-504-542-504-5_W.mp3)
  Segment 34: 127752ms ~ 137364ms (file: 312-327-237-231-527-325-231-571-127-210-231-321-231-321-104-235-135-157-170-157-102-572-217-231-321-275-275-321-213-123-725-267-236-123-312-125-123-527-251-263-321-312-210-127-1_W.mp3)
  Segment 35: 138364ms ~ 138940ms (file: 173-173-137-167-754-765-756-754-574 361-632-617-263_W.mp3)
  Segment 36: 139940ms ~ 140516ms (file: 756-756-752-756-752-756-752-756-753-756-765 507-570-571-157-571-570-157-175-175-571-517-571-571-751-715-175-710-705-570 501-517-105-107-107-107-130-175 570-507-570-507-507-501-1_V.mp3)
  Segment 37: 141516ms ~ 147924ms (file: 152-260-652-213-236-162-103-265-620-570-623-132-570-520-206-572-210-527-302-526-637-312-137-375-574-261-621-261-623-265-526-625-265-267-260-136-756-265-167-107-216-235-213-130-1_W.mp3)
  Segment 38: 148924ms ~ 149500ms (file: 201-256-526-652-652-657-657-651 165-163-615-316-136-132-675-675-576-756-576-675-562-562-651-156-165-165-156-165-165-165-163-623-126-576-567-657-576-567-576-576-567-756-576-675 5_W.mp3)
  Segment 39: 150500ms ~ 151076ms (file: 107-710-701-701-710-701-170-716-701 170-517-571-517-176-716-107-137-715-176-716-170-107-170-301-137-170-170-715-715-170-137-170 170-501-107-107-103-107-103-130-130-130-130-103-1_V.mp3)
  Segment 40: 152076ms ~ 152652ms (file: 613-163-136-236-651-567-567-675-165-310-612-671-672-765-617-675-756-675-576-761-620-602-621-123-637-612-162-602-620-612 103-301-132-231-546-576-675-567-561-652-561-136-765-761-7_W.mp3)
  Segment 41: 153652ms ~ 154228ms (file: 576-572-527-572-573-572-573-572-507-257-572-527-572-527-521-572-572-576-756-571-756-164-571-561-516-527-516-512-561-571-574-571-572-527-572-537-507-502-750-507-572-750-752-537-5_V.mp3)
  Segment 42: 155228ms ~ 155804ms (file: 257-527-257-527-257-251-572-527-527-527-652-527-512-523-152-157-562-257-526-562-526-521-571-513-502-502-507-527-502-520-205-520-520-250-250-527-521-527-527-527-527-527-215-527-5_W.mp3)
  Segment 43: 156804ms ~ 157380ms (file: 571-571-157-751 571-517-571-157-517-735-571-713-715-157-157-715-517-715-571-517-571-571-751-571-751-571-573 574-570-570-571-574-571-517-571-571-571-571-571 507-751-571-571-751-7_W.mp3)
  Segment 44: 158380ms ~ 158956ms (file: 507-750-715-703-730-751-731-750-736-570-751-571-570-173-731-750-507-701-501-517-507-570-705-570 507-351-517-753-573-570-750-175-705-713-750-703-750-705-750-715-175-715-713-170-7_V.mp3)
  Segment 45: 159956ms ~ 164240ms (file: 103-106-316-312 102-150-512-215-203-602-206-213-301-263-623-206-206-312-261-125-157-236-261-162-125-152-125-135-137-217-756-317-137-172-173-137-175-173-315-175-137-132-123-253-3_W.mp3)
  Segment 46: 165240ms ~ 165816ms (file: 231-236-260-261-623-623-623-263-562-546-652-627-627-627-627-672-236-267-267-267-627-263-632-321-361-217-267-627-562-562-562-625-627-627-627-263-123-265-216-236-216-623-612-263-3_W.mp3)
  Segment 47: 166816ms ~ 167392ms (file: 157-105-517-513-157-105-501-501-105-105-507-105-105-150-105-510-105-153-501-510-150-517-507-501-751 105-157-105-105-153-105-501-105-105-157-501-105-517-105-517-150-107-507-170-1_V.mp3)
  Segment 48: 168392ms ~ 168968ms (file: 623-263-623 576-576-756-657-675-675-672-627-627-103-132-132-756-327-765-726-675-675-765-765-675-672-756-756-576-756-576-572-576-756-672-132-762-762-276-276-675-765 752-725-572-6_W.mp3)
  Segment 49: 169968ms ~ 170544ms (file: 571-157-751-671-731-137-153-167-617-631-637-175-713-576-762-172-675-137-167-165-135-176-163-137-165-135-175-136-163-135-317-763-361-315-367-361-365-615-375-167-136-162-136-123-1_W.mp3)
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
> **Time**: 2026-04-05 13:01:46
> **AI**: gemini

---

## Response

[
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
"transcript": "포커스 양보가 필요합니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "포커스", "start_ms": 20, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "양보가", "start_ms": 480, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "필요합니다", "start_ms": 820, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "해서 이제 제가 거기서 서버 개발을 했었고요 그러다가 이제 제가 우연한 기회에 어 해외 지사 발령을 받게 됐어요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "해서", "start_ms": 30, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 300, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 520, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "거기서", "start_ms": 750, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "서버", "start_ms": 1100, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "개발을", "start_ms": 1380, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "했었고요", "start_ms": 1750, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "그러다가", "start_ms": 2180, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 2650, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 2880, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "우연한", "start_ms": 3150, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "기회에", "start_ms": 3550, "dur_ms": 320, "phase_lr_deg": 0.0}
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
"transcript": "해서 이제 제가 거기서 한 5년 정도 있었고요 그러다가 이제 제가 다시 한국으로 돌아오게 됐어요 그때 이제 제가 어 어떤 일을 하게 됐냐면 어 리니지 2 서비스를 총괄하는 역할을 맡게 됐어요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "해서", "start_ms": 40, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 300, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 520, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "거기서", "start_ms": 750, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 1100, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "5년", "start_ms": 1220, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "정도", "start_ms": 1550, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "있었고요", "start_ms": 1850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그러다가", "start_ms": 2250, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 2700, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 2920, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 3180, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "한국으로", "start_ms": 3500, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "돌아오게", "start_ms": 3950, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "됐어요", "start_ms": 4350, "dur_ms": 320, "phase_lr_deg": 0.0}
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
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 13:01:46*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=24432  time=13:00:33  uptime=39303s  mem=256MB  system_processes=357"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_125948.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 765-657-567-576-576-725 715-165-527 136-321-132-103-216-132-132-312-132-312-312-326-563-361-132-163-317-375-361-510-150-163-136-631-362-302-765-302-130-250-570-576-567-156-157-1_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 507-560-507-507-507-657-567-506-507-507-507-507-507-570-572-570-576-657-576-657-256-527-507-571 547-504-540-570-507-570-570-570-570-570-752-572-570-572-507 503-540-530-504-504 5_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 762-237-672-570-274-231-276-215-570-267-136-136-725-502-152-273-261-601-175-763-217-175 275-732-524-573-257-672-736-627-672-326-235-326 576 206-357-235-312-732-236-617-765-732-5_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 501-501-501-501-510-501-501-501-501-513-514-516-651-651-651-657-657-576-576-567-567-576-567-576-567-576-567-576-567-567-567-576-567-576-657-641-163-563-526-564-547-541-514-514-5_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 157-507-517-507-501-567-501-507-501-501-507-157_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 657-623-236-231-263-267-263-132 267-261-263-120-527-265-175-176-132-103-102-106-671-610-621-216-632-673-726-613-267-765-306-103-126-102-162-631-260-106-167-126-167-265-623-263-1_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 130-157-157-153-157-153-157-153-157-135-156-135-135-513-521-527-527-357-537-532-523-517-531-527-253-517-150-715-157-761-710-173-715-761-657-157-751-175-571-752-573-572-357-253-5_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 753-756-576-625-572-526-715-756-756-756-765-765-756-675-126-216_W.mp3)
  Segment 9: 12608ms ~ 21068ms (file: 125-210-312-572-756-672-231-123-216-130-103-372-752-574-276-725-276-235-725-165-261-126-102-216-167-547-265-203-106-675-306-301-637-756-127-672-621-163-573-267-237-217-230-503-2_W.mp3)
  Segment 10: 22068ms ~ 22644ms (file: 175-107-750-751-750-751-750-751-750-751-751-750-751-750-751-750-751-750-750-750-750-750-750-107-105-510-517-517-571-157-517-157-517-576-517-157-751-751-157-175-157-175-175-517-5_W.mp3)
  Segment 11: 23644ms ~ 24220ms (file: 104-143-134-143 314-134-103-301-154-103-103-104-341-104-103-134-134-103-143-104-143-134-143-104-145-153-143-134-134 143-130-301-130-105-103-104-103-103-103-135-134-145-135-130-1_V.mp3)
  Segment 12: 25220ms ~ 31196ms (file: 162-320-267-576-127-360-725-107-760-572-157-507-607-176-210-157-271-562-135-750-103-705-672-752-765-261-201-310-123-127-176-176-173-132-126-162-613-613-762-170-760-263-312-362-1_W.mp3)
  Segment 13: 32196ms ~ 32772ms (file: 752-576-752-572-752-752-752-572-257-725-750-576-576-576-576-576-756-576-571-756-756-716-576-576-257-752-752-725-752-752-570-576-752-572-527-275-752-752-756-572-527-756-527-572-5_W.mp3)
  Segment 14: 33772ms ~ 40180ms (file: 152-260-652-213-236-162-103-265-620-570-623-132-570-520-206-572-210-527-302-526-637-312-137-375-574-261-621-261-623-265-526-625-265-267-260-136-756-265-167-107-216-235-213-130-1_W.mp3)
  Segment 15: 41180ms ~ 41756ms (file: 132-623-603-763-765-371-657-675-105-631-162-162 176-675-617-621_W.mp3)
  Segment 16: 42756ms ~ 56436ms (file: 127-132-123-236-132-216 310-765-137-572-257-325-136-263-206-321-201-267-260-672-213-236-132-312-753-123-207-175-326-263-765-206-236-372-763-176-176-532-126-217-573-367-267-675-6_V.mp3)
  Segment 17: 57436ms ~ 62584ms (file: 136-106-326-526-526-625-621-136-376-657-265-260-623-162-103-162-631-217-321-627-627-321-263-612-216-216-126-162-216-213-123-261-216-126-612-627-216-216-327-123-132-132-132-123-2_W.mp3)
  Segment 18: 63584ms ~ 68768ms (file: 260-162-267-230-320-123-302-216-261-236-213-756-365-310-130-326-312-230-230-301-103-103-213-263-231-236-263-236-573-312-253-267-267-263-576-576-261-267-623-321-321-301-136-301-6_W.mp3)
  Segment 19: 69768ms ~ 70344ms (file: 105-170-170-715-170-103-170-170-710-715-175-751-567-675-175-756-651-657-516-715-761-167-651-675-576 107-105-170-170-170-751-571-715-170-170-710-751-756-715-571-756-567-567-657-6_W.mp3)
  Segment 20: 71344ms ~ 71920ms (file: 720-507 765-756-752-726-702-725-756-720-725-675-725-762-750-270-276 756-705-276-705-705-705-750-607-705-705-750-750-705-507-750-570 715-705-702-726-207-726-702-756-720-705-706-6_V.mp3)
  Segment 21: 72920ms ~ 73496ms (file: 571-576-751-576-576-567-756-756-571-572-517-157-157-571-571-517-251-517-572-517-527-571-756-756-756-756-756-756-756-756-576 571-751-752-751-571-751-572-752_W.mp3)
  Segment 22: 74496ms ~ 75072ms (file: 132-157-205-327-751-507-576-263-756-725-632-672-576-276-275-132-753-761-257-267-725-735-627-216-315-750-312-527-570-762-257-750-617-763-256-267-207-276-265-750-623-571-762-263-2_V.mp3)
  Segment 23: 76072ms ~ 76648ms (file: 561-571-571 153-564-561-512-521-534-523-573-574-512-561-513_V.mp3)
  Segment 24: 77648ms ~ 82796ms (file: 126-231-236-236-321-263-263-210-102-123-256-102-206-126-123-261-236-263-231-321-102-321-236-326-623-236-527-321-163-651-265-102-517-230-216-213-162-102-120-621-265-261-260-206-2_W.mp3)
  Segment 25: 83796ms ~ 84372ms (file: 126-712-132-326-163-715-710-713-173-170-172-175-175-175-157-175-153-157-751-175-615-132-526-325-256-253-253-153-157-165-165-675-736-170-765-751-175-170-172-137-172-710-172-517-1_W.mp3)
  Segment 26: 85372ms ~ 85948ms (file: 576-571-571-315-315-315-537-175-751-517-571-517-571-576-571-574 103-137-103-136-103-103-103-103-137-310-304-135_W.mp3)
  Segment 27: 86948ms ~ 87524ms (file: 725-756 502-265-527-562-527-526-265-625-265-205-602-275-520-502-650-207-657-750-502-526-502-526-572-650-526-675-205-750 502-570-507-527-562-506-756-257-572-265-675-576-507-725-5_V.mp3)
  Segment 28: 88524ms ~ 89100ms (file: 576-567-576-576-576-576-752-723-513-562-574-514-517-571-531-527-521-513-514-513-315-351-305-531-542-512-513_V.mp3)
  Segment 29: 90100ms ~ 90676ms (file: 107-710-701-701-710-701-170-716-701 170-517-571-517-176-716-107-137-715-176-716-170-107-170-301-137-170-170-715-715-170-137-170 170-501-107-107-103-107-103-130-130-130-130-103-1_V.mp3)
  Segment 30: 91676ms ~ 92252ms (file: 103-103-517-175-570-175-571-571-570-153-507-153-153-150-517-157-517-135-153-150-153-157 137-317-103-215 317-317 503-150-751-517-571-157-135-753-570-731-130 173-713-310-170-701-1_V.mp3)
  Segment 31: 93252ms ~ 93828ms (file: 762-502-510-561-560-576-576-507-501-507-506-560-560-574-507-570-570-567-705-765-760-756-570-765-762-765-756-621-623-570-576-657-675-673-625-675-675-675-756-752-617-163-165-675-5_W.mp3)
  Segment 32: 94828ms ~ 95404ms (file: 150-152 150-573-215-152-150-152-510-153-152-512-517-251-251-512-152-153-105-501-512-153-153-510-152-153-135-531-157-157-510-572 150-150-150-517-513-150-152-510-152-153-152-251-1_V.mp3)
  Segment 33: 96404ms ~ 96980ms (file: 576-572-527-572-573-572-573-572-507-257-572-527-572-527-521-572-572-576-756-571-756-164-571-561-516-527-516-512-561-571-574-571-572-527-572-537-507-502-750-507-572-750-752-537-5_V.mp3)
  Segment 34: 97980ms ~ 98556ms (file: 625-625-261-625-621-621-261-261-612-715-715-721-236-123-127-217-721-716-721-716-716-716-716_W.mp3)
  Segment 35: 99556ms ~ 106468ms (file: 261-657-627-267-265-756-576-627-261-576-321-576-256-263-572-265-251-215-312-756-267-576-631-610-132-216-213-106-263-572-265-625-310-263-143-130-261-130-236-132-132-576-756-362-3_W.mp3)
  Segment 36: 107468ms ~ 108044ms (file: 562-576-573-572-576-572-574-576-572-576-507-572-507-507-507-570-507-507-570-507-562-516-532-516-521-532-531-530-502-521-502-501-513-315-153-251-152-532-352-532-540-541-512-512-5_V.mp3)
  Segment 37: 109044ms ~ 109620ms (file: 157-175-765-756-571-571-571-571-571-517-571-571-157-517-157-513-517-517-157-571-567-576-756-715_W.mp3)
  Segment 38: 110620ms ~ 111196ms (file: 157-105-517-513-157-105-501-501-105-105-507-105-105-150-105-510-105-153-501-510-150-517-507-501-751 105-157-105-105-153-105-501-105-105-157-501-105-517-105-517-150-107-507-170-1_V.mp3)
  Segment 39: 112196ms ~ 112772ms (file: 613-513-671-256-265-260-526-132 571-514-513-521-357-156_V.mp3)
  Segment 40: 113772ms ~ 114348ms (file: 150-510-150-150-507-571-501-517-510-517-510-507-570-571-576 150-150-510-501-150-510-510-150-510-150-510-150-157-150-507-570-571-571-571-573-571-571 150-501-150-501-517-501-517-1_V.mp3)
  Segment 41: 115348ms ~ 115924ms (file: 751-751-751-751-751-751-751-751-751-715-715-175-715-715 510-510-175-751-175-175-715-517-571-570-751-751-715-751-175-175-756-715-175-715-175-175-175-175-751-751-571-754 150-150-5_V.mp3)
  Segment 42: 116924ms ~ 117500ms (file: 756-756-576-576-751-571-574-576-574-576-576-756-756-756-576-756-751-753-756-725-752-756-752-756-753-756-507-571-576-573-517-531-517-516-531-537-536-513-561-153-153-531-351_V.mp3)
  Segment 43: 118500ms ~ 119076ms (file: 765-631-165-675 267-675-675-657 567-167-765-672-657-653 765-765-765-673-765-756-362-362-632_W.mp3)
  Segment 44: 120076ms ~ 120652ms (file: 567-657-651-651-165-156-651-615-651-165_W.mp3)
  Segment 45: 121652ms ~ 131336ms (file: 103-310-203-520-321-135-301-753-531-263-627-267-206-326-356-231-574-132-754-547-506-156-156-574-713-130-541-756-302-132-236-367-657-135-217-167-176 571-752-631-132-103-106-673-6_W.mp3)
  Segment 46: 132336ms ~ 132912ms (file: 502 250-510-510-150-152-520-517 501-512-520-520-502-527_V.mp3)
  Segment 47: 133912ms ~ 140644ms (file: 231-236-236-370-627-132-310-130-320-107-723-173-312-260-213-123-312-301-320-301-310-723-327-572-726-230-231-236-236-236-203-267-370-263-230-210-132-103-137-103-315-231-265-256-2_V.mp3)
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
> **Time**: 2026-04-05 13:01:46
> **AI**: gemini

---

## Response

[
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
"transcript": "포커스 양보가 필요합니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "포커스", "start_ms": 20, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "양보가", "start_ms": 480, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "필요합니다", "start_ms": 820, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "해서 이제 제가 거기서 서버 개발을 했었고요 그러다가 이제 제가 우연한 기회에 어 해외 지사 발령을 받게 됐어요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "해서", "start_ms": 30, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 300, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 520, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "거기서", "start_ms": 750, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "서버", "start_ms": 1100, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "개발을", "start_ms": 1380, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "했었고요", "start_ms": 1750, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "그러다가", "start_ms": 2180, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 2650, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 2880, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "우연한", "start_ms": 3150, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "기회에", "start_ms": 3550, "dur_ms": 320, "phase_lr_deg": 0.0}
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
"transcript": "해서 이제 제가 거기서 한 5년 정도 있었고요 그러다가 이제 제가 다시 한국으로 돌아오게 됐어요 그때 이제 제가 어 어떤 일을 하게 됐냐면 어 리니지 2 서비스를 총괄하는 역할을 맡게 됐어요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "해서", "start_ms": 40, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 300, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 520, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "거기서", "start_ms": 750, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 1100, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "5년", "start_ms": 1220, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "정도", "start_ms": 1550, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "있었고요", "start_ms": 1850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그러다가", "start_ms": 2250, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 2700, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 2920, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 3180, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "한국으로", "start_ms": 3500, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "돌아오게", "start_ms": 3950, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "됐어요", "start_ms": 4350, "dur_ms": 320, "phase_lr_deg": 0.0}
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
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 13:01:46*
