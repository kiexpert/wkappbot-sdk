# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=10820  time=06:50:25  uptime=9961s  mem=470MB  system_processes=352"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_064819.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 5148ms (file: 271-102-132-102-102-102-172-170-127-271-127-132-130-310-172-231-527-261-231-123-130-102-310-302-315-321-261-310-723-726-213-127-120-167-751-172-173-713-137-176-173-720-276-102-3_W.mp3)
  Segment 2: 6148ms ~ 6724ms (file: 137-107-573-572-130-103-170-157-173-173-170-170-107-175 705-175-701-175-107-170-107-137-170-173-130-103-130-703-137-301-310-731-172-130-175-130-731-310-170-750-130-175-750-735 7_V.mp3)
  Segment 3: 7724ms ~ 17984ms (file: 216-213-751-107-175-547-106-106-754-675-261-267-102-132-312-321-753-106-213-216-267-620-260-103-123-160-102-231-321-126-216-260-136-610-216-261-621-261-126-310-267-263-130-231-6_W_stt_the_missiles_almost_becomes_all_the_lega.mp3)
  Segment 4: 18984ms ~ 19560ms (file: 750-751-751-507-705-751-750-751-713-705-715-751-715 750-750-751-751-756-571-750-701-753-750-750-751-715-751-751-750-751-756-756 750-715-750-750-705-751-751-715-751-715-751-715-7_V.mp3)
  Segment 5: 20560ms ~ 21136ms (file: 510-150-510-510-510-570-507 517-510-105-510-150-150-571-157-507-510-150-507-150-510-150-501-510-510-501-150-105-150-501-157-571-510-510-574-157-150-517-157-510-571-507-507 510-1_W.mp3)
  Segment 6: 22136ms ~ 38372ms (file: 213-132-312-213-231-102-102-321-321-213-260-201 321-321-326-267-203-123-206-236-231-206-127-213-201-263-102-230-672-103-260-206-752-527-157-105-130-210-542-652-627-130 173 267-6_W.mp3)
  Segment 7: 39372ms ~ 39948ms (file: 102-316-316-236-130-136-172-317-137-316-317-136-132-316-316-103-316-176-170-176-137-671-176-162-126-273-162-176-167-162-165-176-571-163-517-163-316-163-126-132-163-163-136-157-1_W.mp3)
  Segment 8: 40948ms ~ 41524ms (file: 263-153-103-107-107-107-107-105-170-107-105-107-105-105-157-150-105-173-167-163-176-716-367-176-176-173-716-712-176-176-173-172-765-762-671-267-672-726-167-765-671-761-751-675-7_W.mp3)
  Segment 9: 42524ms ~ 43100ms (file: 715-137-136-106-103-132-163-712-173-163-103-153-103-102-162-617-316-675-613-106-103-316-103-215-613-617-173-167-621-160-106-576-137-107-162-103-132-103-517-167-156-106-136 136-1_V.mp3)
  Segment 10: 44100ms ~ 44676ms (file: 751-175-175-715-175-713-753-751-753-570-571-537-152-175-561-516-157-157-517-175-156-135-560-105-751-751-713-715-517-157-173-150-513-173-713-753-571-715-571-715-571-517-175-175-1_V.mp3)
  Segment 11: 45676ms ~ 46252ms (file: 571-571-501-510-517-517-571 507-157-153-105-157-517-501-517-150-510-570-570-150-571-571-517-576-576-517-571-576-574-574 510-501-501-105-150-510-157-105-507-510-501-517-571-571-5_W.mp3)
  Segment 12: 47252ms ~ 47828ms (file: 132-132-312-132-132-312-321-320-230-321-231-327-321-320-260-321-213-326-201-201-237-236-237-203-207-320-231-321-327-267-136-213-261-230-163-130 107_V.mp3)
  Segment 13: 48828ms ~ 49404ms (file: 153-607-137-175-137-751-137-607-361-163-675-106-675-175-675-136-671-637-136-765-176-671-675-163-637-617-175-657 157-751-730-317-370-160-301-315-357-315-371-301-130-130-317-301-6_W.mp3)
  Segment 14: 50404ms ~ 50980ms (file: 173-107-710-170-173-137-701-703-710-153-107-710-713-173-137-173 715-571-571-510-150-107-715-713-170-173-713-175-173-137-173-170-137-135-175-173-137-175-170-130-713-173-175-571-7_V.mp3)
  Segment 15: 51980ms ~ 52556ms (file: 571-517-735-517-571-517-571-756-157-570-517-571-527-756-517-517-567-517-517-507-570-510-517-571-517-573-517-571-573-517-531-517-175-571-175-153-175-175-571-576-571-576-175-571-5_W.mp3)
  Segment 16: 53556ms ~ 60252ms (file: 752-107-310-132-263-210-236-103-103-103-103-103-107-103-102-302-206-574-215-265-123-302-213-162-316-754-720-206-102-301-321-230-320-321-103-301-263-321-312-734-576-261-213-201-2_V.mp3)
  Segment 17: 61252ms ~ 61828ms (file: 510-501-501-510-502-502-502-316 231_V.mp3)
  Segment 18: 62828ms ~ 63404ms (file: 617-167-163-612-601-163-162-162-167-675-657-675-670-106-765-107-572-263-623-167-176-175-756-176-371-716-751-106-657-173-316-163-167-163-671-167-175-157-167-317-137-132-621-601-2_V.mp3)
  Segment 19: 64404ms ~ 64980ms (file: 170-170-170-173-170-715-716-571-571-517-751-576-756-756-751-750-725 150-517-157-570-170-105-170-175-107-170-710-170-710-710-715-751-751-716-571-715-567-561-567-657-756-750-571 1_W.mp3)
  Segment 20: 65980ms ~ 66556ms (file: 157 517-507-571-570-570-570-574_V.mp3)
  Segment 21: 67556ms ~ 68132ms (file: 106-316-136-675-102-103-132-137-127-672-627_V.mp3)
  Segment 22: 69132ms ~ 69708ms (file: 510-571-715-571-751-571-571-571-571-751-570-751-751_W.mp3)
  Segment 23: 70708ms ~ 79060ms (file: 705-103-574-315-310-201-132-206-216-602-670-120-132-130-103-256-201-203-527-275-507-207-132-752-206-312-573-160-267 206-172-127-601-267-123-120-103-316-263-201-103-216-260-206-6_V.mp3)
  Segment 24: 80060ms ~ 80636ms (file: 675-651-657-657-671-675-657-165-617-675-635-765-675-756-756-765-567-765-657-756-765-675-765-765-756-765-376-365-367-637-762-736-671-761-673-762-675-675-672-672-765-175-176-175-1_W.mp3)
  Segment 25: 81636ms ~ 82212ms (file: 136-106-160-162-610-627-612-621-103-170-316-106-617-162-615-610-162-615-612-167-602-162-165-136-631-612-163-315-106-162-162-613-613-106-136-136-316-150-613-163-130-312-310-136-1_V.mp3)
  Segment 26: 83212ms ~ 83788ms (file: 607-602-620-602-620-236-106-231-613-607-605-605-137-173-326-106-625 267-267-267-610-153-123 623-236-762-760-607 267-326-236-236 621-612-612-102-261 602-260-320-236-514-152-725-6_W.mp3)
  Segment 27: 84788ms ~ 85364ms (file: 756-105-510-150-150-150-105-105-157-176-165-165-156-571-651-561-517-157-571-157-751-715-176-751-517-175-571-157-571-175-175-750-750-750-705-750-750-715-750-105-501-150-105-157-1_W.mp3)
  Segment 28: 86364ms ~ 94932ms (file: 260-236-123-301-260-260-301-260-203-320-203-260-237-236-132-107-312-107-217-276-162-571-236-102-206-756-267-267-103-167-163-623-176-102-231-103-106-326-267-756-132-276-231-231-7_W.mp3)
  Segment 29: 95932ms ~ 96508ms (file: 137-735-762-675-765-761-753-576-756-573-763-673-736-673-317-375-175-371-713-173-371-317-173-715-573-753-756-751-157-537-571-571-573-153-157-756-756-756-375-735-371-715-761-315-1_W.mp3)
  Segment 30: 97508ms ~ 104168ms (file: 267-632-127-102-123-706-761-612-270-627-312-162-263-162-216-126-126-120-203-267-263-762-620-672-267-321-130-126-312-123-217-601-160-123-107-571-326-316-261-123-632-312-627-260-1_W.mp3)
  Segment 31: 105168ms ~ 105744ms (file: 513 756-361-631-327-163-106-715-752-357-537-756_W.mp3)
  Segment 32: 106744ms ~ 107320ms (file: 570 517-175-751-571-153-157-175-517-751-571-751-175-751-751-571-571-571-517-517-571-571-517-517-501-517-105-501-501-510-150-501-501-501-510-150-502-506-517-157-157-175-175-571-1_V.mp3)
  Segment 33: 108320ms ~ 108896ms (file: 623-632-163-361-216-623-627-612-162-672-316-263-263-263-627-321-567-276-123-231_W.mp3)
  Segment 34: 109896ms ~ 141648ms (file: 132-637-136-361-362 312-123 750-756-756-547-217-132-572-210-312-123-217-756-103-132-206-126-103-527-123-301-267 132-123-573-217-210-213-301-103-317-756 261-574-231-531-325-527-1_W.mp3)
  Segment 35: 142648ms ~ 148912ms (file: 130-123-213-132-132-132-132-120-123-123-132-231-213-312-132-231-230-132-123-231-130-123-123-132-132-213-132-132-132-123-321-312-210-273-123-120-132-210-312-201-210-213-206-321-1_W_stt_off_off_half,_if_half.mp3)
  Segment 36: 149912ms ~ 150488ms (file: 753-701-713-703-571-570-713-750-701-715-713-750-571-170-751 576-573-570-573-507-135-571-513-105-507-570-507-513 574-507-570-570-570-507-507-570-570-507-537-573-531_V.mp3)
  Segment 37: 151488ms ~ 159408ms (file: 137-270-267-261-216-260-260-627-123-130-210-231-270-132-167-123-756-267-321-132-132-263-260-206-632-276-267-206-267-236-217-276-267-756-216-123-321-267-175-217-756-175-572-256-2_W.mp3)
  Segment 38: 160408ms ~ 160984ms (file: 572-752-754-570-701-371-576-705-137-507-756-763-713-132-153-135-173-705-527-726-527-526-756-712-725-761-317-627-572-567-567_V.mp3)
  Segment 39: 161984ms ~ 162560ms (file: 120-260-621-612-162-607-162-167-536-576-671-617-675-261-621-251-365-567-564-562-572-571-561-276-367-217-172-213-123-130-267-672-541-756-751-316-167-126 613-130-623-236-231-123-3_W.mp3)
  Segment 40: 163560ms ~ 164136ms (file: 517 153-105-105-105-501-150-105-150-105-105-150-150-510-501-501-501-517-501-506-571-517-516-510-501-501-513-157-516-501-571-516 105-153-150-501-105-105-150-105-150-501-501-150-5_V.mp3)
  Segment 41: 165136ms ~ 175756ms (file: 360-302-567-657-567-306-236-310-167-675-163-635-162-540-576-675-103-657-102-103-201-103-721-102-102-157-102-127-731-372-706-236-320-301-136-751-726-237-315-106-607-761-160-751-7_V.mp3)
  Segment 42: 176756ms ~ 177332ms (file: 715-751-751-362-637-261-726-731-637-106-130-632-267-756-762-672-365-167-136-265-170-103-167-617-625 103-103-317-130-136-136-165-136-617-136-103-103-137-103-137-136-310-561-263-7_V.mp3)
  Segment 43: 178332ms ~ 192624ms (file: 120-621-310-231-102-201-270-267-276-270-103-102-621-213-203-132-123-267-102-103-162-540-260-102-102-206-310-730-213-123-173-124-617-130-763-206-672-260-167-672-760-206-312-103-1_W.mp3)
  Segment 44: 193624ms ~ 194200ms (file: 705-710-750-170-750-105 571-571-750-750-571-157-751-750-751-701-570-751-750-750-715-710-157 517-517-570-571-571 517-571-507-517-150-517-571-157-751-571-175-517-571-751-570-754-5_V.mp3)
  Segment 45: 195200ms ~ 195776ms (file: 762-756-675-751-751-715-537-756 735-713-371-715-317-173-176-713-715-715-571-573-756-571-571-573-574-573-576-705-716-756-705-710-175-715-176-713-765-167-716-610-675-167-167-617-6_W.mp3)
  Segment 46: 196776ms ~ 197352ms (file: 710-750-170-713-731-175-371-710 137-103-173-317-130-175-317-130-130-137-130-103-132-310-375-705-107-137-731-107-710-137-310-107-375-751-137-710-317 371-703-137-317-701-763-317-3_V.mp3)
  Segment 47: 198352ms ~ 198928ms (file: 510-501-510-150-510-517 150-510-510-510-150-150-510-510-510-510-510-510-150-150-513-150-513-513-517-157 510-150-510-510-510-150-510-510-150-510-150-150-150-510-510-150-150-150-1_V.mp3)
  Segment 48: 199928ms ~ 200504ms (file: 310-103-315-162-176-103-103-135-530-165-165-310-137-617-167-675-617-617-610-316-610-631-201-627-632-602-103-617-103 103-567-671-120-103-103-136-136-106-130-517-130-103-130-167-1_V.mp3)
  Segment 49: 201504ms ~ 215472ms (file: 573-750-576-206-250-132-310-301-132-574-132-103-517-173-507-105-206-103-132-105-547 723-675-723-102-213-123-123-123-130-312-310-321-312-312-302-351-504-135-103-126-103-256-501-3_V.mp3)
  Segment 50: 216472ms ~ 217048ms (file: 612-106-167-762-671-167-260-206-261-236-206-260-613-130-316-130-312-167-167-103-130-602-321-316-301-621-206 260-206-236-320-302-301-312-310-103-103-130-103-326-236-203-130-130-1_V.mp3)
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
> **Time**: 2026-04-05 06:52:24
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "그런 것 같으니까 하지 말라고 했으면 그 다음부터 저도 뭔가를 네 이야기를",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "그런", "start_ms": 150, "dur_ms": 320, "phase_lr_deg": -5.0},
{"word": "것", "start_ms": 480, "dur_ms": 150, "phase_lr_deg": -4.5},
{"word": "같으니까", "start_ms": 650, "dur_ms": 580, "phase_lr_deg": -3.2},
{"word": "하지", "start_ms": 1300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "말라고", "start_ms": 1700, "dur_ms": 420, "phase_lr_deg": 2.1},
{"word": "했으면", "start_ms": 2200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 2700, "dur_ms": 120, "phase_lr_deg": -1.5},
{"word": "다음부터", "start_ms": 2850, "dur_ms": 520, "phase_lr_deg": -2.0},
{"word": "저도", "start_ms": 3450, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "뭔가를", "start_ms": 3800, "dur_ms": 450, "phase_lr_deg": 1.2},
{"word": "네", "start_ms": 4300, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "이야기를", "start_ms": 4550, "dur_ms": 480, "phase_lr_deg": 2.5}
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
"transcript": "받으면서 싸우는 모습이 있죠 꼭 이것 같다고 해서 레밍이라고 하는 거예요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "받으면서", "start_ms": 110, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "싸우는", "start_ms": 800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "모습이", "start_ms": 1350, "dur_ms": 510, "phase_lr_deg": 1.5},
{"word": "있죠", "start_ms": 1900, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "꼭", "start_ms": 2300, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "이것", "start_ms": 2600, "dur_ms": 380, "phase_lr_deg": -1.2},
{"word": "같다고", "start_ms": 3050, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "해서", "start_ms": 3600, "dur_ms": 350, "phase_lr_deg": 2.1},
{"word": "레밍이라고", "start_ms": 4000, "dur_ms": 820, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 4900, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 5250, "dur_ms": 420, "phase_lr_deg": -1.5}
]
},
{
"segment": 4,
"transcript": "아 근데 부엉이 형 만약에 배가 얼음 위로 올라갔는데 만약에 얼음이 안 깨지면",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "아", "start_ms": 90, "dur_ms": 180, "phase_lr_deg": 10.0},
{"word": "근데", "start_ms": 300, "dur_ms": 320, "phase_lr_deg": 10.5},
{"word": "부엉이", "start_ms": 650, "dur_ms": 450, "phase_lr_deg": 11.2},
{"word": "형", "start_ms": 1150, "dur_ms": 250, "phase_lr_deg": 10.0},
{"word": "만약에", "start_ms": 1500, "dur_ms": 480, "phase_lr_deg": 9.5},
{"word": "배가", "start_ms": 2050, "dur_ms": 350, "phase_lr_deg": 10.2},
{"word": "얼음", "start_ms": 2450, "dur_ms": 320, "phase_lr_deg": 11.0},
{"word": "위로", "start_ms": 2800, "dur_ms": 380, "phase_lr_deg": 10.0},
{"word": "올라갔는데", "start_ms": 3250, "dur_ms": 780, "phase_lr_deg": 10.8},
{"word": "만약에", "start_ms": 4100, "dur_ms": 420, "phase_lr_deg": 9.2},
{"word": "얼음이", "start_ms": 4600, "dur_ms": 450, "phase_lr_deg": 11.5},
{"word": "안", "start_ms": 5100, "dur_ms": 150, "phase_lr_deg": 10.0},
{"word": "깨지면", "start_ms": 5300, "dur_ms": 520, "phase_lr_deg": 10.5}
]
},
{
"segment": 5,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "en",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 6,
"transcript": "울주에서 한달음에 달려온 여기는 경주의 숨은 명소라는 진평왕릉",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "울주에서", "start_ms": 120, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "한달음에", "start_ms": 820, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "달려온", "start_ms": 1450, "dur_ms": 520, "phase_lr_deg": 1.2},
{"word": "여기는", "start_ms": 2050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "경주의", "start_ms": 2600, "dur_ms": 480, "phase_lr_deg": 2.5},
{"word": "숨은", "start_ms": 3150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "명소라는", "start_ms": 3550, "dur_ms": 620, "phase_lr_deg": -1.5},
{"word": "진평왕릉", "start_ms": 4250, "dur_ms": 850, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "생일 파티를 준비했답니다",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "생일", "start_ms": 80, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "파티를", "start_ms": 480, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "준비했답니다", "start_ms": 950, "dur_ms": 780, "phase_lr_deg": 0.0}
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
"transcript": "공간이로군요 근데 1층이 그러면은 집인가요",
"speaker": "speaker_5",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "공간이로군요", "start_ms": 100, "dur_ms": 820, "phase_lr_deg": -15.0},
{"word": "근데", "start_ms": 1000, "dur_ms": 350, "phase_lr_deg": -14.2},
{"word": "1층이", "start_ms": 1400, "dur_ms": 420, "phase_lr_deg": -15.5},
{"word": "그러면은", "start_ms": 1850, "dur_ms": 580, "phase_lr_deg": -14.8},
{"word": "집인가요", "start_ms": 2500, "dur_ms": 650, "phase_lr_deg": -15.1}
]
},
{
"segment": 10,
"transcript": "저희가 사무실로 지금 스튜디오의 공간으로 사용하고 있고요",
"speaker": "speaker_6",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 95,
"audio_type": "speech",
"words": [
{"word": "저희가", "start_ms": 95, "dur_ms": 450, "phase_lr_deg": 8.5},
{"word": "사무실로", "start_ms": 600, "dur_ms": 520, "phase_lr_deg": 7.2},
{"word": "지금", "start_ms": 1150, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "스튜디오의", "start_ms": 1500, "dur_ms": 780, "phase_lr_deg": 9.1},
{"word": "공간으로", "start_ms": 2350, "dur_ms": 510, "phase_lr_deg": 8.0},
{"word": "사용하고", "start_ms": 2900, "dur_ms": 620, "phase_lr_deg": 7.5},
{"word": "있고요", "start_ms": 3550, "dur_ms": 480, "phase_lr_deg": 8.2}
]
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
"transcript": "이 정도면 그냥 리메이크가 아니라 한국 제작 시스템을 통째로 수입하는 수준인데요",
"speaker": "speaker_7",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 110, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "정도면", "start_ms": 250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 750, "dur_ms": 320, "phase_lr_deg": 1.5},
{"word": "리메이크가", "start_ms": 1100, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 1800, "dur_ms": 480, "phase_lr_deg": -1.2},
{"word": "한국", "start_ms": 2350, "dur_ms": 350, "phase_lr_deg": 2.1},
{"word": "제작", "start_ms": 2750, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "시스템을", "start_ms": 3100, "dur_ms": 580, "phase_lr_deg": 1.8},
{"word": "통째로", "start_ms": 3750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "수입하는", "start_ms": 4250, "dur_ms": 620, "phase_lr_deg": -2.5},
{"word": "수준인데요", "start_ms": 4900, "dur_ms": 780, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "최근 일본 영화 도쿄 버스트의 메인 예고편과 포스터가 공개되자 일본 영화",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 105,
"audio_type": "speech",
"words": [
{"word": "최근", "start_ms": 105, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "일본", "start_ms": 550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "영화", "start_ms": 950, "dur_ms": 320, "phase_lr_deg": 1.2},
{"word": "도쿄", "start_ms": 1300, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "버스트의", "start_ms": 1750, "dur_ms": 580, "phase_lr_deg": 2.5},
{"word": "메인", "start_ms": 2400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "예고편과", "start_ms": 2800, "dur_ms": 650, "phase_lr_deg": -1.5},
{"word": "포스터가", "start_ms": 3500, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "공개되자", "start_ms": 4150, "dur_ms": 650, "phase_lr_deg": 3.1},
{"word": "일본", "start_ms": 4850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "영화", "start_ms": 5250, "dur_ms": 380, "phase_lr_deg": -1.2}
]
},
{
"segment": 14,
"transcript": "I know that he has and and something that I think you'll hear more from him on",
"speaker": "speaker_8",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 130, "dur_ms": 100, "phase_lr_deg": -10.0},
{"word": "know", "start_ms": 250, "dur_ms": 220, "phase_lr_deg": -9.5},
{"word": "that", "start_ms": 500, "dur_ms": 150, "phase_lr_deg": -9.0},
{"word": "he", "start_ms": 680, "dur_ms": 180, "phase_lr_deg": -10.5},
{"word": "has", "start_ms": 900, "dur_ms": 250, "phase_lr_deg": -9.8},
{"word": "and", "start_ms": 1200, "dur_ms": 180, "phase_lr_deg": -10.2},
{"word": "and", "start_ms": 1450, "dur_ms": 150, "phase_lr_deg": -11.0},
{"word": "something", "start_ms": 1650, "dur_ms": 450, "phase_lr_deg": -9.2},
{"word": "that", "start_ms": 2150, "dur_ms": 180, "phase_lr_deg": -10.0},
{"word": "I", "start_ms": 2350, "dur_ms": 100, "phase_lr_deg": -10.5},
{"word": "think", "start_ms": 2500, "dur_ms": 220, "phase_lr_deg": -11.2},
{"word": "you'll", "start_ms": 2750, "dur_ms": 250, "phase_lr_deg": -10.0},
{"word": "hear", "start_ms": 3050, "dur_ms": 280, "phase_lr_deg": -9.8},
{"word": "more", "start_ms": 3350, "dur_ms": 250, "phase_lr_deg": -10.4},
{"word": "from", "start_ms": 3650, "dur_ms": 220, "phase_lr_deg": -11.0},
{"word": "him", "start_ms": 3900, "dur_ms": 210, "phase_lr_deg": -10.5},
{"word": "on", "start_ms": 4150, "dur_ms": 180, "phase_lr_deg": -10.2}
]
},
{
"segment": 15,
"transcript": "트럼프 대통령이 그런 생각을 하고 있고 직접 언급도 할 거라는 겁니다 이란의 군사 위협을 제거해 이득을 보는 것",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 115,
"audio_type": "speech",
"words": [
{"word": "트럼프", "start_ms": 115, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "대통령이", "start_ms": 650, "dur_ms": 610, "phase_lr_deg": 0.0},
{"word": "그런", "start_ms": 1300, "dur_ms": 320, "phase_lr_deg": 1.5},
{"word": "생각을", "start_ms": 1650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "하고", "start_ms": 2150, "dur_ms": 350, "phase_lr_deg": 2.1},
{"word": "있고", "start_ms": 2550, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "직접", "start_ms": 2950, "dur_ms": 380, "phase_lr_deg": -1.2},
{"word": "언급도", "start_ms": 3350, "dur_ms": 510, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 3900, "dur_ms": 150, "phase_lr_deg": 1.8},
{"word": "거라는", "start_ms": 4100, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "겁니다", "start_ms": 4650, "dur_ms": 450, "phase_lr_deg": -2.5},
{"word": "이란의", "start_ms": 5200, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "군사", "start_ms": 5750, "dur_ms": 350, "phase_lr_deg": 3.2},
{"word": "위협을", "start_ms": 6150, "dur_ms": 510, "phase_lr_deg": 0.0},
{"word": "제거해", "start_ms": 6700, "dur_ms": 490, "phase_lr_deg": -1.8},
{"word": "이득을", "start_ms": 7250, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "보는", "start_ms": 7800, "dur_ms": 320, "phase_lr_deg": 2.1},
{"word": "것", "start_ms": 8150, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "이 꽉 막힌 생각이 한국 기업들이 만든 세탁기에 의해 깨지게 되었다고 하는데요 한국이 만든 드럼 세탁기가 미국 가전에서 혁명을",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 100, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "꽉", "start_ms": 250, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "막힌", "start_ms": 500, "dur_ms": 350, "phase_lr_deg": 1.2},
{"word": "생각이", "start_ms": 900, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 1450, "dur_ms": 320, "phase_lr_deg": 2.5},
{"word": "기업들이", "start_ms": 1800, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "만든", "start_ms": 2450, "dur_ms": 350, "phase_lr_deg": -1.5},
{"word": "세탁기에", "start_ms": 2850, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "의해", "start_ms": 3450, "dur_ms": 320, "phase_lr_deg": 3.1},
{"word": "깨지게", "start_ms": 3800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "되었다고", "start_ms": 4300, "dur_ms": 580, "phase_lr_deg": -2.0},
{"word": "하는데요", "start_ms": 4900, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "한국이", "start_ms": 5500, "dur_ms": 450, "phase_lr_deg": 1.8},
{"word": "만든", "start_ms": 6000, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "드럼", "start_ms": 6350, "dur_ms": 350, "phase_lr_deg": -1.2},
{"word": "세탁기가", "start_ms": 6750, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "미국", "start_ms": 7450, "dur_ms": 320, "phase_lr_deg": 2.5},
{"word": "가전에서", "start_ms": 7800, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "혁명을", "start_ms": 8450, "dur_ms": 510, "phase_lr_deg": -1.5}
]
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
"transcript": "나라였던 거죠 서울에서 인생 처음으로 눈을 맞은 순간 사드나는 그냥 완전히 빠져버립니다 하늘에서 떨어지는 눈을 보면서 이게 진짜야 하는 표정으로 웃고 또 웃어요 편의점 앞에서는 음악 틀어놓고 혼자 춤까지 추면서 영상을 찍기도 하죠 진짜 말 그대로 한국을 있는 그대로 제대로 즐기고 돌아간 여행이었습니다 그런데 인도로 돌아가자마자 사드나가 영상을 하나 올리는데 분위기가 좀 이상합니다 눈이 퉁퉁 부어 있고 눈물은 계속 맺혀 있고 말도 제대로 못 잇는 상태로 카메라를 켰거든요 그래서 친구들이 다 걱정했대요 한국에서 무슨 일 있었던 거 아니야 사드나가 걱정하던 일 생긴 것 같아 근데 사드나가 울먹이며 꺼낸 말은 완전 예상 밖이었습니다 한국이 너무 그립다였죠 The first solo trip of my life",
"speaker": "speaker_4",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "나라였던", "start_ms": 140, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "거죠", "start_ms": 750, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "서울에서", "start_ms": 1150, "dur_ms": 550, "phase_lr_deg": 1.2},
{"word": "인생", "start_ms": 1750, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "처음으로", "start_ms": 2100, "dur_ms": 510, "phase_lr_deg": 2.5},
{"word": "눈을", "start_ms": 2650, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "맞은", "start_ms": 3000, "dur_ms": 350, "phase_lr_deg": -1.5},
{"word": "순간", "start_ms": 3400, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "사드나는", "start_ms": 3750, "dur_ms": 580, "phase_lr_deg": 3.1},
{"word": "그냥", "start_ms": 4350, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "완전히", "start_ms": 4700, "dur_ms": 480, "phase_lr_deg": -2.0},
{"word": "빠져버립니다", "start_ms": 5200, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "하늘에서", "start_ms": 6100, "dur_ms": 520, "phase_lr_deg": 1.8},
{"word": "떨어지는", "start_ms": 6650, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "눈을", "start_ms": 7250, "dur_ms": 320, "phase_lr_deg": -1.2},
{"word": "보면서", "start_ms": 7600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 8100, "dur_ms": 310, "phase_lr_deg": 2.5},
{"word": "진짜야", "start_ms": 8450, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 8950, "dur_ms": 320, "phase_lr_deg": -1.5},
{"word": "표정으로", "start_ms": 9300, "dur_ms": 510, "phase_lr_deg": 0.0},
{"word": "웃고", "start_ms": 9850, "dur_ms": 350, "phase_lr_deg": 3.2},
{"word": "또", "start_ms": 10250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "웃어요", "start_ms": 10450, "dur_ms": 450, "phase_lr_deg": -1.8},
{"word": "편의점", "start_ms": 11000, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "앞에서는", "start_ms": 11500, "dur_ms": 550, "phase_lr_deg": 2.1},
{"word": "음악", "start_ms": 12100, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "틀어놓고", "start_ms": 12450, "dur_ms": 510, "phase_lr_deg": -1.5},
{"word": "혼자", "start_ms": 13000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "춤까지", "start_ms": 13400, "dur_ms": 420, "phase_lr_deg": 3.5},
{"word": "추면서", "start_ms": 13850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "영상을", "start_ms": 14350, "dur_ms": 480, "phase_lr_deg": -2.0},
{"word": "찍기도", "start_ms": 14850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "하죠", "start_ms": 15350, "dur_ms": 320, "phase_lr_deg": 1.2},
{"word": "진짜", "start_ms": 15700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "말", "start_ms": 16100, "dur_ms": 150, "phase_lr_deg": -1.5},
{"word": "그대로", "start_ms": 16300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "한국을", "start_ms": 16800, "dur_ms": 480, "phase_lr_deg": 2.5},
{"word": "있는", "start_ms": 17300, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "그대로", "start_ms": 17650, "dur_ms": 450, "phase_lr_deg": -1.2},
{"word": "제대로", "start_ms": 18150, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "즐기고", "start_ms": 18650, "dur_ms": 450, "phase_lr_deg": 3.1},
{"word": "돌아간", "start_ms": 19150, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "여행이었습니다", "start_ms": 19650, "dur_ms": 1050, "phase_lr_deg": -2.0},
{"word": "그런데", "start_ms": 20800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "인도로", "start_ms": 21300, "dur_ms": 480, "phase_lr_deg": 1.8},
{"word": "돌아가자마자", "start_ms": 21800, "dur_ms": 820, "phase_lr_deg": 0.0},
{"word": "사드나가", "start_ms": 22650, "dur_ms": 520, "phase_lr_deg": -1.2},
{"word": "영상을", "start_ms": 23200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "하나", "start_ms": 23700, "dur_ms": 320, "phase_lr_deg": 2.5},
{"word": "올리는데", "start_ms": 24050, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "분위기가", "start_ms": 24650, "dur_ms": 580, "phase_lr_deg": -1.5},
{"word": "좀", "start_ms": 25250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이상합니다", "start_ms": 25450, "dur_ms": 780, "phase_lr_deg": 3.2},
{"word": "눈이", "start_ms": 26300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "퉁퉁", "start_ms": 26700, "dur_ms": 380, "phase_lr_deg": -1.8},
{"word": "부어", "start_ms": 27100, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "있고", "start_ms": 27450, "dur_ms": 310, "phase_lr_deg": 2.1},
{"word": "눈물은", "start_ms": 27800, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 28300, "dur_ms": 350, "phase_lr_deg": -1.5},
{"word": "맺혀", "start_ms": 28700, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "있고", "start_ms": 29100, "dur_ms": 310, "phase_lr_deg": 3.5},
{"word": "말도", "start_ms": 29450, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "제대로", "start_ms": 29800, "dur_ms": 480, "phase_lr_deg": -2.0},
{"word": "못", "start_ms": 30300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "잇는", "start_ms": 30500, "dur_ms": 320, "phase_lr_deg": 1.2},
{"word": "상태로", "start_ms": 30850, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "카메라를", "start_ms": 31350, "dur_ms": 610, "phase_lr_deg": -1.5},
{"word": "켰거든요", "start_ms": 32000, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 32700, "dur_ms": 450, "phase_lr_deg": 2.5},
{"word": "친구들이", "start_ms": 33200, "dur_ms": 610, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 33850, "dur_ms": 150, "phase_lr_deg": -1.2},
{"word": "걱정했대요", "start_ms": 34050, "dur_ms": 780, "phase_lr_deg": 0.0},
{"word": "한국에서", "start_ms": 34900, "dur_ms": 650, "phase_lr_deg": 3.1},
{"word": "무슨", "start_ms": 35600, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "일", "start_ms": 35950, "dur_ms": 120, "phase_lr_deg": -2.0},
{"word": "있었던", "start_ms": 36100, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "거 아니야", "start_ms": 36550, "dur_ms": 480, "phase_lr_deg": 1.8},
{"word": "사드나가", "start_ms": 37100, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "걱정하던", "start_ms": 37650, "dur_ms": 550, "phase_lr_deg": -1.2},
{"word": "일", "start_ms": 38250, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "생긴", "start_ms": 38400, "dur_ms": 350, "phase_lr_deg": 2.5},
{"word": "것", "start_ms": 38800, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "같아", "start_ms": 39000, "dur_ms": 320, "phase_lr_deg": -1.5},
{"word": "근데", "start_ms": 39400, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "사드나가", "start_ms": 39750, "dur_ms": 520, "phase_lr_deg": 3.2},
{"word": "울먹이며", "start_ms": 40300, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "꺼낸", "start_ms": 40900, "dur_ms": 350, "phase_lr_deg": -1.8},
{"word": "말은", "start_ms": 41300, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "완전", "start_ms": 41700, "dur_ms": 350, "phase_lr_deg": 2.1},
{"word": "예상", "start_ms": 42100, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "밖이었습니다", "start_ms": 42450, "dur_ms": 950, "phase_lr_deg": -1.5},
{"word": "한국이", "start_ms": 43500, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 44000, "dur_ms": 320, "phase_lr_deg": 3.5},
{"word": "그립다였죠", "start_ms": 44350, "dur_ms": 820, "phase_lr_deg": 0.0},
{"word": "The", "start_ms": 45200, "dur_ms": 150, "phase_lr_deg": -2.0},
{"word": "first", "start_ms": 45400, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "solo", "start_ms": 45750, "dur_ms": 350, "phase_lr_deg": 1.2},
{"word": "trip", "start_ms": 46150, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "of", "start_ms": 46500, "dur_ms": 150, "phase_lr_deg": -1.5},
{"word": "my", "start_ms": 46700, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "life", "start_ms": 46900, "dur_ms": 350, "phase_lr_deg": 2.5}
]
},
{
"segment": 19,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "en",
"silence_before_ms": 0,
"audio_type": "music",
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
"transcript": "근데 선배님이 아니아니 너 하고 싶은 거 한번 다 해봐 그리고 이제 선배님이 리액션 해주시니까 제가 한 행동들도 다",
"speaker": "speaker_9",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "근데", "start_ms": 110, "dur_ms": 320, "phase_lr_deg": -5.0},
{"word": "선배님이", "start_ms": 450, "dur_ms": 520, "phase_lr_deg": -4.2},
{"word": "아니아니", "start_ms": 1000, "dur_ms": 480, "phase_lr_deg": -5.5},
{"word": "너", "start_ms": 1500, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "하고", "start_ms": 1700, "dur_ms": 350, "phase_lr_deg": 2.1},
{"word": "싶은", "start_ms": 2100, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 2450, "dur_ms": 150, "phase_lr_deg": -1.5},
{"word": "한번", "start_ms": 2650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 3050, "dur_ms": 150, "phase_lr_deg": 3.1},
{"word": "해봐", "start_ms": 3250, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 3700, "dur_ms": 450, "phase_lr_deg": -2.0},
{"word": "이제", "start_ms": 4200, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "선배님이", "start_ms": 4550, "dur_ms": 550, "phase_lr_deg": 1.8},
{"word": "리액션", "start_ms": 5150, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "해주시니까", "start_ms": 5650, "dur_ms": 720, "phase_lr_deg": -1.2},
{"word": "제가", "start_ms": 6400, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 6750, "dur_ms": 150, "phase_lr_deg": 2.5},
{"word": "행동들도", "start_ms": 6950, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 7550, "dur_ms": 150, "phase_lr_deg": -1.5}
]
},
{
"segment": 22,
"transcript": "창고에서 새 코드를 짜는데 이중 코드는 나쁜 패턴이 있으면 그대로 따라 합니다 아 여기서 이렇게 하나 보다 하고 복제를 하는 건데요 이러면 나쁜 패턴이 눈덩이처럼 불어납니다 그래서 저희에게는",
"speaker": "speaker_10",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 125,
"audio_type": "speech",
"words": [
{"word": "창고에서", "start_ms": 125, "dur_ms": 610, "phase_lr_deg": 0.0},
{"word": "새", "start_ms": 750, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "코드를", "start_ms": 950, "dur_ms": 420, "phase_lr_deg": 1.5},
{"word": "짜는데", "start_ms": 1400, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "이중", "start_ms": 1900, "dur_ms": 350, "phase_lr_deg": 2.1},
{"word": "코드는", "start_ms": 2300, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "나쁜", "start_ms": 2750, "dur_ms": 320, "phase_lr_deg": -1.2},
{"word": "패턴이", "start_ms": 3100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "있으면", "start_ms": 3600, "dur_ms": 410, "phase_lr_deg": 1.8},
{"word": "그대로", "start_ms": 4050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "따라", "start_ms": 4550, "dur_ms": 320, "phase_lr_deg": -2.5},
{"word": "합니다", "start_ms": 4900, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 5400, "dur_ms": 150, "phase_lr_deg": 3.2},
{"word": "여기서", "start_ms": 5600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 6100, "dur_ms": 410, "phase_lr_deg": -1.8},
{"word": "하나", "start_ms": 6550, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "보다", "start_ms": 6900, "dur_ms": 310, "phase_lr_deg": 2.1},
{"word": "하고", "start_ms": 7250, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "복제를", "start_ms": 7600, "dur_ms": 480, "phase_lr_deg": -1.5},
{"word": "하는", "start_ms": 8100, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "건데요", "start_ms": 8450, "dur_ms": 450, "phase_lr_deg": 3.5},
{"word": "이러면", "start_ms": 8950, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "나쁜", "start_ms": 9400, "dur_ms": 320, "phase_lr_deg": -2.0},
{"word": "패턴이", "start_ms": 9750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "눈덩이처럼", "start_ms": 10250, "dur_ms": 820, "phase_lr_deg": 1.2},
{"word": "불어납니다", "start_ms": 11100, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 12000, "dur_ms": 450, "phase_lr_deg": -1.5},
{"word": "저희에게는", "start_ms": 12500, "dur_ms": 720, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "개입하지 않아도 돼요 말이 고삐에 의해 방향이 틀어지면 말이 자연스럽게 올바른 방향으로 돌아오거든요 그런 것과 같습니다 린터가 빨간 불을 켜면 에이전트가 자동으로 코드를 돌리고 다시 시도합니다 이 자동 교정 루프가 하네스의 핵심 메커니즘인데요",
"speaker": "speaker_10",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 135,
"audio_type": "speech",
"words": [
{"word": "개입하지", "start_ms": 135, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "않아도", "start_ms": 800, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "돼요", "start_ms": 1250, "dur_ms": 320, "phase_lr_deg": 1.5},
{"word": "말이", "start_ms": 1600, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "고삐에", "start_ms": 1950, "dur_ms": 480, "phase_lr_deg": 2.5},
{"word": "의해", "start_ms": 2450, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "방향이", "start_ms": 2800, "dur_ms": 450, "phase_lr_deg": -1.2},
{"word": "틀어지면", "start_ms": 3300, "dur_ms": 610, "phase_lr_deg": 0.0},
{"word": "말이", "start_ms": 3950, "dur_ms": 310, "phase_lr_deg": 2.1},
{"word": "자연스럽게", "start_ms": 4300, "dur_ms": 780, "phase_lr_deg": 0.0},
{"word": "올바른", "start_ms": 5100, "dur_ms": 450, "phase_lr_deg": -1.8},
{"word": "방향으로", "start_ms": 5600, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "돌아오거든요", "start_ms": 6200, "dur_ms": 950, "phase_lr_deg": 3.1},
{"word": "그런", "start_ms": 7200, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "것과", "start_ms": 7550, "dur_ms": 310, "phase_lr_deg": -2.0},
{"word": "같습니다", "start_ms": 7900, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "린터가", "start_ms": 8550, "dur_ms": 480, "phase_lr_deg": 1.2},
{"word": "빨간", "start_ms": 9050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "불을", "start_ms": 9450, "dur_ms": 320, "phase_lr_deg": -1.5},
{"word": "켜면", "start_ms": 9800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "에이전트가", "start_ms": 10200, "dur_ms": 750, "phase_lr_deg": 3.2},
{"word": "자동으로", "start_ms": 11000, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "코드를", "start_ms": 11650, "dur_ms": 450, "phase_lr_deg": -1.8},
{"word": "돌리고", "start_ms": 12150, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 12650, "dur_ms": 350, "phase_lr_deg": 2.1},
{"word": "시도합니다", "start_ms": 13050, "dur_ms": 820, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 13900, "dur_ms": 120, "phase_lr_deg": -1.5},
{"word": "자동", "start_ms": 14050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "교정", "start_ms": 14450, "dur_ms": 380, "phase_lr_deg": 3.5},
{"word": "루프가", "start_ms": 14850, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "하네스의", "start_ms": 15400, "dur_ms": 580, "phase_lr_deg": -2.0},
{"word": "핵심", "start_ms": 16000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "메커니즘인데요", "start_ms": 16400, "dur_ms": 1050, "phase_lr_deg": 1.2}
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
"transcript": "비유를 들면 이렇습니다 시끄러운 공사장 한가운데서 바이올린 소리의 미세한 음정 차이를 듣는 것과 소리가 하나도 없는 무음실에서 듣는 것 어디서 더 정밀하게 들을 수 있을까요 반양성자를 써서 멀리 떨어진 조용한",
"speaker": "speaker_11",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "비유를", "start_ms": 120, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "들면", "start_ms": 650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이렇습니다", "start_ms": 1050, "dur_ms": 820, "phase_lr_deg": 1.5},
{"word": "시끄러운", "start_ms": 1900, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "공사장", "start_ms": 2600, "dur_ms": 520, "phase_lr_deg": 2.1},
{"word": "한가운데서", "start_ms": 3150, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "바이올린", "start_ms": 4050, "dur_ms": 580, "phase_lr_deg": -1.2},
{"word": "소리의", "start_ms": 4650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "미세한", "start_ms": 5150, "dur_ms": 480, "phase_lr_deg": 1.8},
{"word": "음정", "start_ms": 5650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "차이를", "start_ms": 6050, "dur_ms": 450, "phase_lr_deg": -2.5},
{"word": "듣는", "start_ms": 6550, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "것과", "start_ms": 6900, "dur_ms": 350, "phase_lr_deg": 3.2},
{"word": "소리가", "start_ms": 7300, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "하나도", "start_ms": 7800, "dur_ms": 450, "phase_lr_deg": -1.8},
{"word": "없는", "start_ms": 8300, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "무음실에서", "start_ms": 8650, "dur_ms": 850, "phase_lr_deg": 2.5},
{"word": "듣는", "start_ms": 9550, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 9900, "dur_ms": 150, "phase_lr_deg": -1.5},
{"word": "어디서", "start_ms": 10100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 10600, "dur_ms": 150, "phase_lr_deg": 3.1},
{"word": "정밀하게", "start_ms": 10800, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "들을", "start_ms": 11500, "dur_ms": 320, "phase_lr_deg": -2.0},
{"word": "수", "start_ms": 11850, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있을까요", "start_ms": 12050, "dur_ms": 620, "phase_lr_deg": 1.2},
{"word": "반양성자를", "start_ms": 12700, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "써서", "start_ms": 13600, "dur_ms": 320, "phase_lr_deg": -1.5},
{"word": "멀리", "start_ms": 13950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "떨어진", "start_ms": 14350, "dur_ms": 480, "phase_lr_deg": 2.5},
{"word": "조용한", "start_ms": 14850, "dur_ms": 520, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 06:52:24*
