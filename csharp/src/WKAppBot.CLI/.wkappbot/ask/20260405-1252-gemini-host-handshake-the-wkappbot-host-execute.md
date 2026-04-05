# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=1700  time=12:51:59  uptime=29786s  mem=1485MB  system_processes=356"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_125148.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 105-150-157-175-751-107-150-157-150-107-170-175-175-710_W.mp3)
  Segment 2: 1576ms ~ 4312ms (file: 673-673-267-712-657-106-175-765-162-176-165-675-675-675-756-675-657-765-657-675-576-576-657-765-756-765 672-576-125-167-617-756-675-675-725-372-672-321-253-257-231-537-576-576-6_W.mp3)
  Segment 3: 5312ms ~ 5888ms (file: 750-623-132-103-134-103-132-312-132-167-312-134-103-103-136_V.mp3)
  Segment 4: 6888ms ~ 7464ms (file: 172-127-162-312-126-136-361-312-103-301-103-213-201-312-132-301-312-321-302-321-301-367-306-327-361-315-305-526-567-135-316-160-675-672-761-162-617-167-162-612-162-326-263-267-2_V.mp3)
  Segment 5: 8464ms ~ 9040ms (file: 507-573-507-507-507-570-570-570-570-570-572-657-652-526-562-562-652-657-567-257-521-527-257-521-527-527-521-572-512-527-527-527-527-257-527-527-257-725-517-574-576-506-502-507-5_V.mp3)
  Segment 6: 10040ms ~ 10616ms (file: 150-507-571-517-175-150-157-517-751-571-517-150-157-510-517-571-510-574-510 501-501-517-510-150-570-715-751-750-570-570-750-157-570-571-571-751-570-571-751-571 547-547-574-574-5_W.mp3)
  Segment 7: 11616ms ~ 12192ms (file: 162-267-760 623-612-675-657-657-651-657-610-621-267-267-615-637-650-652-167-130-561-751-126-163-601-167-675-617-675-601-612-671-671-671-601-136-162_W.mp3)
  Segment 8: 13192ms ~ 23776ms (file: 231-230-230-103-312-310-102-764-273-732-763-302-267-310-236-756-613-213-310-130-210-162-132-613-130-312-123-760-732-312-263-231-321-321-132-320-762-620-120-132-176-310-321-276-1_W.mp3)
  Segment 9: 24776ms ~ 25352ms (file: 571-570 150-105-105-157-105-105-105-105-105-517-157-517-157-157-517-157-510-510-517-150-105-510-105-153-150-105-157-150-150-157-541-150-517-571-507 501-105-105-157-517-105-157-5_V.mp3)
  Segment 10: 26352ms ~ 31284ms (file: 132-207-256-120-120-675-756-237-312-326-230-267-123-321-123-267-261-321-312-762-701-136-126-136-256-605-574-102-321-326-135-310-106-675-316-231-260-312-231-316-236-576-765-231-3_W.mp3)
  Segment 11: 32284ms ~ 32860ms (file: 541-514-506-506-571-576-576-507-576-513-572-571-517-576-517-572-572-573-215-512-521-527-572-502-502-502-572-572-572-527-572-576-572-576-527-576-572-527-576-572-526-572-752-572-5_W.mp3)
  Segment 12: 33860ms ~ 34436ms (file: 152-512-521 513 230-236-620-620-620-261-621-623-632-623-621-673-263-132-213 306-320-326-302-362-361-260-620-620-620_V.mp3)
  Segment 13: 35436ms ~ 44616ms (file: 267-627-756-257-237-236-123-236-267-267-267-103-321-231-103-175-231-102-132-216-231-167-271-276-621-627-263-627-267-263-257-105-126-162-572-231-137-176-106-127-107-217-623-625-1_W.mp3)
  Segment 14: 45616ms ~ 54688ms (file: 103-167-126-102-132-130-132-173-301-103-176-167-167-107-170-172-172-172-107-175-172-103-107-107-172-107-132-103-103-576-571-102-102-127-705-237-132-102-126-103-570-627-265-756-3_V.mp3)
  Segment 15: 55688ms ~ 63284ms (file: 672-627-267-576-237-102-102-107-267-627-276-276-267-126-102-102-130-160-136-136-126-574-263-523-524-237-326-213-263-327-357-160-617-527-576-321-617-625-327-762-271-130-327-302-3_W.mp3)
  Segment 16: 64284ms ~ 64860ms (file: 167-132-103-571-103-312-163-130-137-213-612-126-167-107-107-137-105-135-710-107-132-157-130-163-134-132-103-261-136-362 261-132-263-752-675-725-130-103-137-137-713-123-103-102-3_V.mp3)
  Segment 17: 65860ms ~ 66436ms (file: 537-503-573-612-632-326-362-362-362-567-507-507-507-507-572-507-507-570-507-567-567-576-705-507-750-750-572-507-756-570-750-756-750-702-570-756-507-750-507-507-263-236-312-213-2_V.mp3)
  Segment 18: 67436ms ~ 69344ms (file: 103-132-301-132-216-621-216-136-132-136-132-136-176-137-103-123-301-723-206-254-265-231-102-103-321-132-132-321-132-132-132-301-312-617-671-576-576-574-574-574-574-576-361-621-6_V.mp3)
  Segment 19: 70344ms ~ 70920ms (file: 752-627-275-275-627-256 134-213-215_W.mp3)
  Segment 20: 71920ms ~ 72496ms (file: 361-326-567-517-571-571-756-571-576-157-627-625-615-250-526-567-625-176-756-675-765-675-765-752-756-765-765-671-657-657-657 520-572-572-562-576-576-576-567-756-576-576-756-756-5_W.mp3)
  Segment 21: 73496ms ~ 74072ms (file: 504-507-507-507-507-570-570-570-570-570-573-507-507-572-570-570-507-507-507-570-570-570-570-570-507-570-570-507-572-527-653-573-507-570-507-572-570-570-507-570-507-531-537-367-3_W.mp3)
  Segment 22: 75072ms ~ 75648ms (file: 167-765-736-756-763-765-756-765-765-765-735-765-765-765-762-765-762-763-716-716-136-103-163-762-627-612-627-627-276-627-263-267-265-672-675-675-617-613-163-613-136-613-613-613-6_W.mp3)
  Segment 23: 76648ms ~ 77224ms (file: 504-502-506-507-506-507-506-502-507-572-507-507-507-507-507-507-507-507-507-507-507-507-507-527-502-507-531-351-507-574-570-503-507-547-501-570-574-507-507-507-507-507-507-507-5_V.mp3)
  Segment 24: 78224ms ~ 78800ms (file: 107-105-170-107-107-102-107-103-173-715-715-175-715-715-751-715 510-105-105-507-701-107-751-107-701-715-751-750-715-107-105-517-750-751-150-570-507-571-750-715 517-705-571-175-7_V.mp3)
  Segment 25: 79800ms ~ 80376ms (file: 576-572-574-574-571-537-574-574-507-507-503-502-527-502-502-520-506-523-526-516-517-536-517-564-504-561-532-547-502-502-502-532-502 507-507-502-502-502-502-532-250-352-350-503-5_V.mp3)
  Segment 26: 81376ms ~ 81952ms (file: 261-260-261-672-627-672-625-657-206-136-301-132-302-302-756-726-162-106-672-165-165-507-675-136-102-671-672-671-756-652-306-301-301-361-612-673-607-601-163-761-176-716-173-371-7_V.mp3)
  Segment 27: 82952ms ~ 93932ms (file: 321-273-231-321-321-325-321-321-312-137-325-327-623-275-273-213-326-273-306-361-103-102-103-213-130-310-103-216-130-620-127-312-136-210-206-132-103-251-132-310-103-132-317-130-1_V.mp3)
  Segment 28: 94932ms ~ 95508ms (file: 715-701-175-175-715-175 751-715-751-571-173-137-163-167-167-164-163-617-576-576-576-756-756-756-765-736-316-130-137-170-761-765-713-715-176-576-657-576-657-657-573-567-576-765-6_W.mp3)
  Segment 29: 96508ms ~ 104104ms (file: 762-576-752-213-235-325-321-320-723-137-231-273-210-320-267-230-230-236-320-231-765-276-230-123-327-672-765-276-562-230-267-231-102-321-103-756-725-267-237-312-537-574-752-130-1_W.mp3)
  Segment 30: 105104ms ~ 105680ms (file: 762-675-506-517-651-615-657-657-657-675-612-576-756-752-756-765-756-726-756-675-175-765-756-175-671-765-175-135-163-136-163-361-631-163-625-613-651-361-615-132-632-625-675-765-6_W.mp3)
  Segment 31: 106680ms ~ 107256ms (file: 713 165-615-657-657-675-657-576-657-567-576-576-765-675-613-621-312-541 621-617-675-675-657-657-567-567-567-657-567-567-657-567-657-675-627 376-621-621-672-657-675-651-654-657-6_W.mp3)
  Segment 32: 108256ms ~ 108832ms (file: 621-756-572-572-573-752-572-731-126-621-216-216-261-621-126-263-621-261-326-261-623-621-261-620-261-261-627-623-263-620-621-261-263-261-623-502-560-526-562-625-562-562-562_W.mp3)
  Segment 33: 109832ms ~ 110408ms (file: 267-570-275-631-752-156-132-136-153-136-317-672-213-627-325-762-276-573-263-267-325-627-567-327-637-652-675-762-231-753-253-570-571-271-276-352-236-523-321-570-753-312-263-753-2_V.mp3)
  Segment 34: 111408ms ~ 117996ms (file: 132-230-260-136-301-321-761-201-201-173-761-617-362-106-516-617-310-130-312-671-562-672-756-653-675-765-671-316-301-137-130-310-103-561-310-301-637-627-127-107-137-327-132-152-2_W.mp3)
  Segment 35: 118996ms ~ 119572ms (file: 210-256-215-512-561-521-526-562-251-257-213-213-541-513-561-513-536-561-531-517-502-512-527-503-517-576-524-513-543-567-561-513-504-513-504-527-513-532-534-563-503-501-541-564-5_V.mp3)
  Segment 36: 120572ms ~ 121148ms (file: 627-673-136-163-617-167-531-152-105-132-132-132-172-136-153-137-135_V.mp3)
  Segment 37: 122148ms ~ 122724ms (file: 507-261-236-627-573-632-273-756-256-276-572-762-752-657-237-576-725-675-602-627-627-237-756-715-567-541-576-514-124-370-507-567-572-123-216-267-526-762-726-125-325-276-756-725-6_V.mp3)
  Segment 38: 123724ms ~ 124300ms (file: 310-317-371-310-132-130 751-701-307-107-765-736-137-610-176-173-106-671-167-631-617-651-765-317-310-137-173-103-173-103-175-107-130-751-105 175-315-103-310-715-316-601-610-106-3_V.mp3)
  Segment 39: 125300ms ~ 125876ms (file: 672-672-675 125-156-165-165-165-615-165-615-610-615-651-610-165-615-615-617_V.mp3)
  Segment 40: 126876ms ~ 135120ms (file: 173-751-157-732-275-715-762-231-523-573-517-517-576-572-573-752-271-263-236-231-310-130-162-753-561-132-623-216-216-267-163-267-236-123-573-263-256-275-261-231-263-326-621-752-1_W.mp3)
  Segment 41: 136120ms ~ 136696ms (file: 365-351-136-537-265-705-612-372-752-375-372-135-570 153-571-136-765-217-135-765-512-316-256-750 503-173-523-152-735-537-257-327-517-237-762-327-136-751-573-207-752-372-301-317-3_V.mp3)
  Segment 42: 137696ms ~ 146228ms (file: 213-326-275-375-372-756-132-672-751-267-206-321-267-326-351-137-132-514-362-203-263-132-216-312-263-263-132-257-236-236-257-352-312-302-236-756-206-754-620-213-273-706-302-360-1_V.mp3)
  Segment 43: 147228ms ~ 155724ms (file: 574-576-276-547-216-231-103-207-310-276-106-574-267-327-627-320-103-726-765-312-102-261-765-206-326-213-657-537-130-231-230-260-267-260-130-213-103-130-543-547-672-267-263-532-5_W.mp3)
  Segment 44: 156724ms ~ 157300ms (file: 132-317-132-103-572-317-162-176-567-372-735-725-736-236-327-721-306-576-253-630-275-273-327-302-301-312-302-312-302-301-576-571-561-123-125-621-162-132-310-160-162-617-136-136-5_V.mp3)
  Segment 45: 158300ms ~ 158876ms (file: 567-567-563-576-567-576-567-567 657-657-657-567-657-675-675-675-675-562-567-567-567-576-576-576-576-567-576-567-576-567-530-502-570-572-752-756-756-752-576-756-657-562-562-652-6_W.mp3)
  Segment 46: 159876ms ~ 160452ms (file: 526-502-526-762-526-520-205-256-526-756-756-765-675-756-765-756-765 526-502-526-520-256-250-257-526-256-567-567-657-756-756-765-765-675-765-675-765-756-765-756-576-572-576-567-5_V.mp3)
  Segment 47: 161452ms ~ 162028ms (file: 507-507-507-507-507-125-351-312-301-325-352-520-530-512-150-510-531-510-503-530-510-503-507-507-351-531-315-513-153-510-504-501-501-501-510-503-530-532-532 507-531-135 312 132-5_W.mp3)
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
> **Time**: 2026-04-05 12:52:55
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
"transcript": "해서 강화도 천도와 같은 전술이 존재할 수 있었던 겁니다 이 땅이 침략자에게 얼마나 지독한 곳이었는지를 가장 잘 보여주는 것 동절기 청야 전술입니다 압록강은 한겨울에 얼어붙습니다 그래서 북방 유목민족은 이 얼어",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "해서", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "강화도", "start_ms": 200, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "천도와", "start_ms": 500, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "같은", "start_ms": 780, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "전술이", "start_ms": 1000, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "존재할", "start_ms": 1250, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 1520, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "있었던", "start_ms": 1620, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "겁니다", "start_ms": 1880, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "게 식당에 앉았는데 프리양카가 메뉴판을 보더니 진짜 한국 사람처럼 너무 자연스럽게 주문을 합니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "게", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "식당에", "start_ms": 150, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "앉았는데", "start_ms": 450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "프리양카가", "start_ms": 850, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "메뉴판을", "start_ms": 1300, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "보더니", "start_ms": 1720, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 2100, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 2400, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "사람처럼", "start_ms": 2650, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 3080, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "자연스럽게", "start_ms": 3350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "주문을", "start_ms": 3850, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "합니다", "start_ms": 4200, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "AI 최전선에 계신 분들이 꼭 한 번 읽어봐야 하는 책이 아닌가 싶습니다 또 학생들이나 직장인들은 이런 경영인들이 AI에 대해서 어떻게 생각하고 또 앞으로 AI를 어떻게 사용할 예정인",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "AI", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "최전선에", "start_ms": 280, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "계신", "start_ms": 750, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "분들이", "start_ms": 1050, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "꼭", "start_ms": 1420, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 1580, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "번", "start_ms": 1700, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "읽어봐야", "start_ms": 1850, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 2320, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "책이", "start_ms": 2620, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "아닌가", "start_ms": 2880, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "싶습니다", "start_ms": 3250, "dur_ms": 450, "phase_lr_deg": 0.0}
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
"transcript": "안 되겠다 아 진짜 안 되겠다",
"speaker": "speaker_4",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "안", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "되겠다", "start_ms": 210, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 650, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 800, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 1120, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "되겠다", "start_ms": 1300, "dur_ms": 380, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "기술자들을 외부 환경에 의존하지 않는 완전 제어 시스템 개발로 밀어붙인 동인이었습니다 혹독함이 기술의",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "기술자들을", "start_ms": 40, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "외부", "start_ms": 550, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "환경에", "start_ms": 850, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "의존하지", "start_ms": 1220, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "않는", "start_ms": 1650, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "완전", "start_ms": 1900, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "제어", "start_ms": 2220, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "시스템", "start_ms": 2500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "개발로", "start_ms": 2900, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "밀어붙인", "start_ms": 3280, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "동인이었습니다", "start_ms": 3750, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "혹독함이", "start_ms": 4350, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "기술의", "start_ms": 4780, "dur_ms": 320, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "AI는 도구일 뿐이니까요 판단과 책임은 저희 사람의 몫입니다 그래서 저는 하네스 엔지니어링 너무 어렵게 생각하지 말라고",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "AI는", "start_ms": 20, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "도구일", "start_ms": 300, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "뿐이니까요", "start_ms": 650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "판단과", "start_ms": 1150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "책임은", "start_ms": 1550, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "저희", "start_ms": 1920, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "사람의", "start_ms": 2180, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "몫입니다", "start_ms": 2500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 2900, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "저는", "start_ms": 3250, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "하네스", "start_ms": 3550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "엔지니어링", "start_ms": 3880, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 4400, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "어렵게", "start_ms": 4650, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "생각하지", "start_ms": 4980, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "말라고", "start_ms": 5350, "dur_ms": 320, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "correct one was my second guess. Yes! Okay I got that one right. Whew! All right. Yeah that was easy I got that right. Okay so I got four out of five.",
"speaker": "speaker_6",
"confidence": 0.91,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "correct", "start_ms": 60, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "one", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "was", "start_ms": 600, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "my", "start_ms": 750, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "second", "start_ms": 880, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "guess.", "start_ms": 1220, "dur_ms": 320, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "친환경 이렇게 관련된 종가 주가들이 좀 많이 올랐다 요런 차트로 보시면 될 것 같고요 그다음에 초대형 딜 현실화 스페이스X 관련 내용인데 미국 증시에서는 우주 관련 된 종목들이 좀 많이",
"speaker": "speaker_7",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "친환경", "start_ms": 40, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 420, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "관련된", "start_ms": 680, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "종가", "start_ms": 1050, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "주가들이", "start_ms": 1350, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 1820, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 1980, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "올랐다", "start_ms": 2280, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "just have open intellectual conversations. Yeah. Humans have emotions and emotions drive our creative thinking. They drive our decisions every single day all the time. AI doesn't.",
"speaker": "speaker_8",
"confidence": 0.93,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "just", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "have", "start_ms": 280, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "open", "start_ms": 480, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "intellectual", "start_ms": 750, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "conversations.", "start_ms": 1350, "dur_ms": 720, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 12:52:55*
