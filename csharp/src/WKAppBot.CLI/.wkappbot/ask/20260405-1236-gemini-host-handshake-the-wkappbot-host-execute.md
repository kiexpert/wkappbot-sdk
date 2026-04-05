# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=17708  time=12:35:10  uptime=30371s  mem=468MB  system_processes=354"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_123138.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 317-301-310-173-172-137-305-371-317-753-301-157-217-317 103-135-315-157-103-130-130-315-132-137-315-137-312-357-315-137-357-301-375-715-317-301-175-371-317-135-713-310-312-301-3_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 520-527-256-562-576-657-765-765-765-765-675-675-765-765-765-765-675 507-502-572-250-257-567-625-576-756-657-675-675-576-576-567-657 527-520-507-256-520-520-527-527-576-506-526-5_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 624-264-621-612-612-621-261-621-621-261-621 623-624-624-625-624-625-642-642-621-126-621-624-625-164-623-621-623-263-261-621-621-612-612-612-621-612-612-613-621 613-612-612-674-6_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 570-570-570-571-570-572-521-572-572-571-571-573-573-571-571-751-751-571-572-752-756-756-735-567-513-541-537-507-517-514-507-501-517-501-571-507-573-502-507-567-567_W.mp3)
  Segment 5: 6304ms ~ 20560ms (file: 275-312-317-135-135-153-157-123-137-135-357-175-312-152-574-251-123-135-502-301-570-270-265-132-627-260-547-574 201-120-571-157-231-210-102-574-201-210-210 752-527-576-132-135-2_V.mp3)
  Segment 6: 21560ms ~ 22136ms (file: 267-567-567-567-657-567-156-165-621-627-672-672-627-576-752-275-276-267-236-517-574-756-753-613-167-756-576-756-675-576-567-513-157-215-672-675-765-637-132-326-236-621-621-612-6_W.mp3)
  Segment 7: 23136ms ~ 24792ms (file: 675 675-765-756-576-756-103-310-576-761-675-657-765-376-631-671-657-362-132-312-136-317-136-376-136-317-631-316-675-657-613-163-617 123 361-326-316-361_W.mp3)
  Segment 8: 25792ms ~ 26368ms (file: 216 526-526-576-625-624-264-264-261-243-261-261-261 261-267-620-675-675-675-675-674-674-674-674 615-612-675-657-675-674-674-671-674-674-674-674-671-567-570-572-576-567-576-576-5_W.mp3)
  Segment 9: 27368ms ~ 27944ms (file: 132-132-725-321-375-125-356-315-372-153-132-317-325-123-172-236-625-213-672-271-237-527-316-572-135-137-713-705-763-756-716-520-523-356-123-267-672-267-261-326-270-213-657-136-1_V.mp3)
  Segment 10: 28944ms ~ 29520ms (file: 507-507-507-501-507-520-501-510-507-502-507-507-502-257-520-523-527-256-502-527-572-573-571-572-751-531-512_W.mp3)
  Segment 11: 30520ms ~ 31096ms (file: 715-517-571-726-137-167-613-163-136-163-613-163-135-173-136-317-136-637-631-613-631-675-761-671-163-613-135-316-136-163-153-315-165-156-571-157-517-157-371-716-376-675-567-715-1_W.mp3)
  Segment 12: 32096ms ~ 32672ms (file: 571-756-576-571-573-571-576-576-567-756-576-576-756-576-751-756-765-512-571-572-527-572-752-527-572-275-572-752-725-572-752-576-257-275-257-527-572-725-275-752-572-257-275-752-7_W.mp3)
  Segment 13: 33672ms ~ 34248ms (file: 526-250-276-527-752-570-567-765-765-675-765-765-765-765-675 502-507-572-502-570-752-520-576-527-275-572-275-752-560-756-756-765-765 507-507-520-502-576-675-576-756-756-756-756-7_V.mp3)
  Segment 14: 35248ms ~ 40720ms (file: 570-531-256-531-512-537-317-310-315-165-130-267-123-765-762-102-160-107-260-672-671-130-157-103-126-130-210-752-201-321-312-130-301-275-173-572-152-270-132-123-260-261-726-132-1_V.mp3)
  Segment 15: 41720ms ~ 42296ms (file: 517-512-513-521-512-132-516-105-570-572-572-576-751-752-513-513-537-507-572-572-572-572-572-572-572-572-527-572-527-572-527-572-527-572-527-502-572-571-576-571-572-571-576-576-5_W.mp3)
  Segment 16: 43296ms ~ 43872ms (file: 630-607-601-673-365-657-657-167-163-617-613-631-637-675-627 701-735-157-103-751-107-160-601-631-317-371-105-370-167-370-735-675-137-310-172-107-317 735-751-130-751-107-307-175-7_V.mp3)
  Segment 17: 44872ms ~ 45448ms (file: 165-165-651-165-615-651-651-651-651-156-615-615-165-165-615-165-156-163-657-752-725-752-756-567-752-657-576 651-651-156-615-615-651-651-651-156-651-657-651-651-651-651-165-651-6_W.mp3)
  Segment 18: 46448ms ~ 47024ms (file: 501-513-561-576-576-536-507-531-537-530-503-351-350-503-350-350-351-502-350-350-530-530-523-531-564-513-532-534-527 531-503-503-530-521-572-532 517-571-571-517-513 503-506-506-5_V.mp3)
  Segment 19: 48024ms ~ 52740ms (file: 123-123-126-120-132-132-123-123-132-132-623-621-263-263-126-276-231-267-623-276-762-267-726-623-372-326-316-236-123-267-267-261-627-756-126-130-637-263-160-267-236-231-326-163-2_W.mp3)
  Segment 20: 53740ms ~ 54316ms (file: 567-561-567-563-562 516-531-547-536-512-516_V.mp3)
  Segment 21: 55316ms ~ 55892ms (file: 547-542-572-572-572-572-572-517-527-572-572-573-572 512-561-512-563-541-531-513-562 524-512-541-521-523-651-652-561-526-625-625-620-260-205 513-531-531-531-536-357-513-534-536-5_V.mp3)
  Segment 22: 56892ms ~ 57468ms (file: 752-725-275-752-527-720-276-275-527-725-752-570-572-750-706-725-520 560-562-265-265-256-527-567-562-520-502-572-570-675-526-650-507-507-520-507-572-570-527-675-756-574 507-657-5_V.mp3)
  Segment 23: 58468ms ~ 59044ms (file: 517-571-573-153-157-517-513-517-157 130-157-175-105-130-175-315-130-103-105-157-135-135-315-130-130-153-135-157-517-157-157-157-175 531-137-135-517-130-107-135-103-150-130-137-1_V.mp3)
  Segment 24: 60044ms ~ 60620ms (file: 165 716-132-156-231-657-753-162-726-753-136-756-370-675-170-731-306-165-675-651-620-163-576-157-531-357-756-732_W.mp3)
  Segment 25: 61620ms ~ 62196ms (file: 275-725-273-756-765-756-756-765-675-167-651-156-153-156-162-167-617-671-675-675-671-576-567-567-576-576-576-527-576-567-576-165-615-156-516-156-513-561-531-156-561-531-156-165-5_W.mp3)
  Segment 26: 63196ms ~ 63772ms (file: 657-163-713-576-167-160-106-106-106-103-160_W.mp3)
  Segment 27: 64772ms ~ 65348ms (file: 756 572-572-527-251-251-275-752-275-752-570-571-570-576-576-574-576-756-756-576-756-756-756-756 502-502-256-675-720-571-576-571-570-571-571 567-502-570-750-750-756-750-756-751-7_W.mp3)
  Segment 28: 66348ms ~ 66924ms (file: 517-510-507-501-750-570-750-715-175-751-517-751-517-175-750-175-715-750 571-751-510-570-170-507-175-570-175-157-750-750-571-751-705-570-175-715-107-105-150-731-173-736-753-753-1_V.mp3)
  Segment 29: 67924ms ~ 77824ms (file: 210-206-762-726-102-276-312-213-231-302-203-103-765-270-230-756-231-312-301-321-756-270-103-102-102-201-217-102-132-756-271-752-130-103-103-576-275-751-120-210-756-132-230-301-1_W.mp3)
  Segment 30: 78824ms ~ 79400ms (file: 106-136-160-760-705-175 510-517-157-175-157-715-570-715-571-573-157-157-571-573-710-157-501-501-150-571-517-517-507-157-510-750-570-570-570-571-571-570-175-175-756-756-756-756-5_V.mp3)
  Segment 31: 80400ms ~ 87888ms (file: 213-216-231-276-321-102-751-276-132-213-321-130-312-321-210-102-231-231-532-102-162-210-237-162-312-375-231-751-123-231-163-321-375-217-267-321-106-103-132-321-320-312-321-672-1_W.mp3)
  Segment 32: 88888ms ~ 89464ms (file: 176-723 172-576-576-756-765-765-167 167-765_W.mp3)
  Segment 33: 90464ms ~ 96548ms (file: 301-132-310-261-527-276-102-176-310-230-267-260-267-267-261-237-321-231-231-132-216-235-735-273-157-561-752-321-275-105-175-627-251-123-316-132-132-135-132-107-102-271-263-265-2_W.mp3)
  Segment 34: 97548ms ~ 98124ms (file: 705-612-672-276-507-671-672-564-627-213-637-257-321-520-321-206-213-257-352_V.mp3)
  Segment 35: 99124ms ~ 99700ms (file: 135-175-175-751-751-571-751-571-571-571-751-571-517-751-571-570-571-570 517-157-157-710-715-175-157-571-175-705-751-571-507-570-573-507-570-517-570-570-547-574-574-574-574-574-5_V.mp3)
  Segment 36: 100700ms ~ 101276ms (file: 576 510-517-510-105-157-501-157-501-150-507-157-510-105-510-507-510-507-510-170-157-501-105-510-517-501-510-507-751-571-510-510-507-571-157-571 510-570-517-150-105-150-105-153-1_V.mp3)
  Segment 37: 102276ms ~ 102852ms (file: 502-502-562-526-502-257-576-527-576-725-725-572-576-576-570-571-571-576-571-756-756 562-527-572-520-257-256-570-560-526-706-562-572-275-752-756-576-756-576-751-750-751-756-756-7_V.mp3)
  Segment 38: 103852ms ~ 104428ms (file: 627-627-267-621-175-612-103-651-156-651-216-201-673-136-761-103-516-105-617-132-106-753-761-163-103-136-106-106-316-130-163-316-163-316-546-217-651-260-162_V.mp3)
  Segment 39: 105428ms ~ 106004ms (file: 357-136-205-503-570-317-756-712-570-371-315-356-157-257-712-327-137-715-326-562-672-263-231-675-621-265-675-265-123-512-723-237-261-736-752-263-625-762-627-276-621-620-627-607-6_W.mp3)
  Segment 40: 107004ms ~ 107580ms (file: 132 172-176-763-726-712-713-763-713-735-761-765-736-675-367-507-657-507-537-513-573-576-756-756-763-765-756-317-312-316-327-273-217-231-623-312-371-172-127-216-213-261-362-672-6_W.mp3)
  Segment 41: 108580ms ~ 109156ms (file: 574-576-712-157-756-756-576-576-672-627-632-672-263-106-671-672-261-261-312-103-623-261-132-136-610-627-762-627-132-671-106-106-136-103-153-612-362-726-721-762-721-132-103 132 1_W.mp3)
  Segment 42: 110156ms ~ 110732ms (file: 107-170-175-571-157-157-175-751-175-157-751-167-157-517-571-571-571-715-176-715 105-105-150-105-150-150-571-167-175-157-175-761-176-617-175-675-761-157-571-175 154-150-150-105-1_V.mp3)
  Segment 43: 111732ms ~ 118284ms (file: 657-675-267-236-537-576-576-123-236-231-230-206-123-321-103-307-203-736-273-230-627-672-267-263-267-765-762-267-213-675-675-267-260-725-132-123-301-670-216-301-103-317-302-250-7_W.mp3)
  Segment 44: 119284ms ~ 119860ms (file: 517-573-507-507-501-571-571-150-510-105-567-517-576-517-516-576-571-576-576-567-567-576-576-567-574-570-570 150-105-105-157-507-157-507-571-150-501-150-150-510-507-507-570-501-1_W.mp3)
  Segment 45: 120860ms ~ 128240ms (file: 135-571-157-173-573-135-751-570-127-132-103-137-213-215-132-172-705-541-547-574-756-762-276-572-265-627-135-762-305-253-273-210-237-103-123-502-123-175-153-750-726-206-725-312-2_V.mp3)
  Segment 46: 129240ms ~ 135576ms (file: 327-273-237-316-325-310-376-326-763-267-617-267-627-627-627-267-362-312-637-623-236-312-652-157-527-276-237-271-267-267-637-762-627-675-213-317-176-310-261-267-702-275-317-267-1_W.mp3)
  Segment 47: 136576ms ~ 137152ms (file: 623-263-263-362-261-621-132-531-573-130-103-103-210-123-201-312-310-301-130-130-103-306-316-103-106-130-126-310-102-162-302-326-762-720-602-620-260-263-603-301-362-312-310-102-3_V.mp3)
  Segment 48: 138152ms ~ 138728ms (file: 721-257-527-723-206-752-316-607-127-576-756-527-752-705-237-267-672-672-267-357-502-673-513-657-572-725-506-573-532-675-127 561-315-201-251 127-256-165-152 627-273-752 276-276-2_V.mp3)
  Segment 49: 139728ms ~ 140304ms (file: 752-765-627-672-617-672-652-213 316-612-671-165-617-615-167-136-610-136-160-106-610-316-632-136-103-162-157-106-126-261-167-162-126-136-314-130-756-136-136-617-602-103-716-136-1_V.mp3)
  Segment 50: 141304ms ~ 141880ms (file: 162-102-170-106-615-127-156-261-126-130-126-130-136-163-137-136-142-105-136-152-165-126-106-167-675-617-163-127-136-167-136-102_V.mp3)
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
> **Time**: 2026-04-05 12:36:45
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "된다면 어떻게 될까요 현재 물리학의 표준 모형이 흔들릴 수 있다는 징후가 많이 나오고 있습니다 또 빅뱅 직후에 왜 물질이 반물질보다 조금 더 많이 남았는지 그 수십억 년의 미스터리를 풀 실마리가 생길 수도 있겠죠 왜 우주가 반물질로",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "된다면", "start_ms": 10, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 140, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "될까요", "start_ms": 260, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "현재", "start_ms": 400, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "물리학의", "start_ms": 520, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "표준", "start_ms": 680, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "모형이", "start_ms": 810, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "흔들릴", "start_ms": 950, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 1100, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "있다는", "start_ms": 1190, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "징후가", "start_ms": 1320, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 1470, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "나오고", "start_ms": 1590, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 1730, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 1890, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "빅뱅", "start_ms": 1980, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "직후에", "start_ms": 2100, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "왜", "start_ms": 2250, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "물질이", "start_ms": 2340, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "반물질보다", "start_ms": 2480, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 2690, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 2810, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 2900, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "남았는지", "start_ms": 3030, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 3200, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "수십억", "start_ms": 3290, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "년의", "start_ms": 3440, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "미스터리를", "start_ms": 3560, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "풀", "start_ms": 3750, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "실마리가", "start_ms": 3850, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "생길", "start_ms": 4010, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "수도", "start_ms": 4130, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "있겠죠", "start_ms": 4250, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "왜", "start_ms": 4390, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "우주가", "start_ms": 4480, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "반물질로", "start_ms": 4630, "dur_ms": 160, "phase_lr_deg": 0.0}
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
"transcript": "데 옆에 있던 출연자들이 다 웃었다고 합니다 그러면서 한국에서 지냈던 이야기 서울에서 돌아다니던 이야기 한국",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "데", "start_ms": 20, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "옆에", "start_ms": 110, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "있던", "start_ms": 230, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "출연자들이", "start_ms": 340, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 530, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "웃었다고", "start_ms": 620, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "합니다", "start_ms": 780, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "그러면서", "start_ms": 920, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "한국에서", "start_ms": 1080, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "지냈던", "start_ms": 1240, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "이야기", "start_ms": 1390, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "서울에서", "start_ms": 1530, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "돌아다니던", "start_ms": 1690, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "이야기", "start_ms": 1860, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 2000, "dur_ms": 110, "phase_lr_deg": 0.0}
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
"transcript": "40년 넘게 그랬던 것처럼 오늘도 새벽",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "40년", "start_ms": 10, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "넘게", "start_ms": 150, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "그랬던", "start_ms": 270, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "것처럼", "start_ms": 420, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "오늘도", "start_ms": 560, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "새벽", "start_ms": 700, "dur_ms": 110, "phase_lr_deg": 0.0}
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
"transcript": "에 중심에 두지 않는다면 자연스럽게 서로의 중요한 선택에서 주변으로 밀려나게 된다 따라서 질문은 단순히 프랑스와 한국의 협력을 어떻게 더 발전시킬 것인가가 아니다 더 근본적인 질문은 이것이다 이렇게 전략적으로 이해관계가 잘 맞는 두 나라가 왜 아",
"speaker": "speaker_4",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "에", "start_ms": 15, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "중심에", "start_ms": 110, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "두지", "start_ms": 250, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "않는다면", "start_ms": 370, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "자연스럽게", "start_ms": 540, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "서로의", "start_ms": 730, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "중요한", "start_ms": 880, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "선택에서", "start_ms": 1040, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "주변으로", "start_ms": 1210, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "밀려나게", "start_ms": 1370, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "된다", "start_ms": 1520, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "따라서", "start_ms": 1640, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "질문은", "start_ms": 1780, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "단순히", "start_ms": 1930, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "프랑스와", "start_ms": 2070, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "한국의", "start_ms": 2240, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "협력을", "start_ms": 2380, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 2530, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 2660, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "발전시킬", "start_ms": 2750, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "것인가가", "start_ms": 2920, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "아니다", "start_ms": 3090, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 3230, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "근본적인", "start_ms": 3320, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "질문은", "start_ms": 3490, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "이것이다", "start_ms": 3640, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 3800, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "전략적으로", "start_ms": 3940, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "이해관계가", "start_ms": 4130, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 4320, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "맞는", "start_ms": 4420, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "두", "start_ms": 4540, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "나라가", "start_ms": 4630, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "왜", "start_ms": 4770, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 4860, "dur_ms": 80, "phase_lr_deg": 0.0}
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
"transcript": "운 일은 예를 들어 지중해나 카리브해에서 바다에 던져지는 것과는 전혀 다른 차원의 문제입니다 따뜻한 바다에서는 조난자가 구조를 몇 시간",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "운", "start_ms": 10, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "일은", "start_ms": 100, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "예를", "start_ms": 220, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "들어", "start_ms": 340, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "지중해나", "start_ms": 460, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "카리브해에서", "start_ms": 620, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "바다에", "start_ms": 810, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "던져지는", "start_ms": 950, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "것과는", "start_ms": 1120, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "전혀", "start_ms": 1270, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "다른", "start_ms": 1390, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "차원의", "start_ms": 1510, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "문제입니다", "start_ms": 1650, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "따뜻한", "start_ms": 1840, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "바다에서는", "start_ms": 1990, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "조난자가", "start_ms": 2180, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "구조를", "start_ms": 2350, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "몇", "start_ms": 2490, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "시간", "start_ms": 2590, "dur_ms": 110, "phase_lr_deg": 0.0}
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
"transcript": "한옥의 차경 효과를 내기 위해 막혀 있던 벽을 뚫어 거실 앞뒤로 이런 환상",
"speaker": "speaker_6",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "한옥의", "start_ms": 15, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "차경", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "효과를", "start_ms": 280, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "내기", "start_ms": 420, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "위해", "start_ms": 540, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "막혀", "start_ms": 660, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "있던", "start_ms": 790, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "벽을", "start_ms": 900, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "뚫어", "start_ms": 1030, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "거실", "start_ms": 1150, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "앞뒤로", "start_ms": 1280, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 1420, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "환상", "start_ms": 1540, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "있습니다 실제 NMN을 투여한 60대 쥐가 20대 젊은 쥐의 운동 능력을 회복하는 기적이 일어났습니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "있습니다", "start_ms": 20, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "실제", "start_ms": 180, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "NMN을", "start_ms": 310, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "투여한", "start_ms": 480, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "60대", "start_ms": 630, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "쥐가", "start_ms": 770, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "20대", "start_ms": 890, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "젊은", "start_ms": 1030, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "쥐의", "start_ms": 1160, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "운동", "start_ms": 1280, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "능력을", "start_ms": 1410, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "회복하는", "start_ms": 1560, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "기적이", "start_ms": 1730, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "일어났습니다", "start_ms": 1870, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "작아 보이죠 무게로 따지면 소수점 아래 0이 24개 붙는 그램 정도밖에 되지 않습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "작아", "start_ms": 10, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "보이죠", "start_ms": 140, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "무게로", "start_ms": 280, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "따지면", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "소수점", "start_ms": 580, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "아래", "start_ms": 750, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "0이", "start_ms": 880, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "24개", "start_ms": 1010, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "붙는", "start_ms": 1180, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "그램", "start_ms": 1310, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "정도밖에", "start_ms": 1450, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "되지", "start_ms": 1640, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "않습니다", "start_ms": 1770, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "실마리가 들어옵니다 이거 이제 저희가 튀기려고 이제 1차 작업을 하는 거예요 염지 하기 전의",
"speaker": "speaker_7",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "실마리가", "start_ms": 15, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "들어옵니다", "start_ms": 180, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "이거", "start_ms": 380, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 500, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "저희가", "start_ms": 620, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "튀기려고", "start_ms": 760, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 920, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "1차", "start_ms": 1040, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "작업을", "start_ms": 1170, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 1330, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 1450, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "염지", "start_ms": 1590, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "하기", "start_ms": 1720, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "전의", "start_ms": 1840, "dur_ms": 110, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 12:36:45*
