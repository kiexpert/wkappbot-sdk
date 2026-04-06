# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=07:25:39  uptime=21985s  mem=1585MB  system_processes=448"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_072530.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 231-213 537-357-632-627-672-316-263-236-576-276-263-672-263-216-162-126-261-567-562-652-132-136-312-316-103-130-103 236-127_W.mp3)
  Segment 2: 1576ms ~ 2440ms (file: 716-167-136-132-573-756-271-132-723-302-321-320-312-312-307-236-320-321-327-326-652-625-365-326-132-132-132-231-627-637-675-265 130-136-135-715-103-217-271-102-167-612-132-102-3_W.mp3)
  Segment 3: 3440ms ~ 4016ms (file: 132-162-136-132-163-613-621-612-230-260-216-206-216-203-236-216-260-123-620-602-126-130-102-312-123-216_V.mp3)
  Segment 4: 5016ms ~ 5592ms (file: 317-312-103-361-316-136-301-312-136-610-213-163-123-132-312-132-301-320-721-376-137-176-132-136-136-317-136-576-675-321-301-136-136-136-106-132-213-132-123-130-263-136-123-103-3_V.mp3)
  Segment 5: 6592ms ~ 7168ms (file: 316-132-132-132-132-316-167-162-136-132-123-153-137-132-317-146-124-162-162-127-317-316-371-152-132-321-615-316-326-136-164-123-143-167-312-164-132-137-613-413-173-413-132-613 4_V.mp3)
  Segment 6: 8168ms ~ 8744ms (file: 310-135-153-125-317-507-260-123-126-127-157-752-207-310-370-107-712-762-173-325-301-231-316-302-301-235-230-275-231-320-302-302-231-362-320-362-632-362-321-312-312-263-237-230-2_V.mp3)
  Segment 7: 9744ms ~ 10320ms (file: 756-756-736-765-763-167-127-657-576-567-576-301-321-301-321-123-123-603-160-136-672-761-367-617-617-130-701-176 162-160-623-316-136-130-301-106-103-751-675-260-256-524-230-326-5_W.mp3)
  Segment 8: 11320ms ~ 11896ms (file: 163-137-176-103-236-561-675-362-632-260-627-136-316-160-132-136-136-163-106-360-167-162-671-136-761-167-613-217-261-612-106-163-160-632-167-105-103-671-162 130-310-310-130-310-1_V.mp3)
  Segment 9: 12896ms ~ 13472ms (file: 236-362-236-236-763-627-763-762-671-765-267-762-762-567-761-602-263-267-612-612-621-620-120-326-263 107-236-261-365-657-765-561-671-765-576-546-570-576-576-605-675-675-672-617-6_W.mp3)
  Segment 10: 14472ms ~ 15048ms (file: 205-502-547-527-257-103-312-310-310-263-623-236-203-203-102-620-621-216-302-312-132 260-625-276-267-206-260-236-235-320-320-162-206-260-267-206-576-657-576-267-267-132-570-267-2_W.mp3)
  Segment 11: 16048ms ~ 16624ms (file: 361-314-312-136-137-103-713-317-317-317-376-371-376-314-132-312-750-756-756-765-765-762-302-230-203-231-230-320-327-130-103-316-137-317-163-312-302-301-263-237-362-132-613-132-5_V.mp3)
  Segment 12: 17624ms ~ 18200ms (file: 360-601-612-275-276-263-630-602-260-260-620-306-106-136-316-162-123-130-312-216-126-201-623-620-620-620-260-267-236-263-621-162-603-106-610-206-261-632-623-263-657-657-675-675-6_V.mp3)
  Segment 13: 19200ms ~ 19776ms (file: 612-612-632-312-103-137-136-310-130-106-126-163-102-103-103-136-316-613-103-106-106-176-762-173-310-163-102-153-540-546-362-326-123-301-321-602-302-312-123-130-132-103-673-670-2_V.mp3)
  Segment 14: 20776ms ~ 21352ms (file: 627-621-267-136-136-137-167-617-675-675-672-761-237-637-316-361-673-630-673-670-710-617-367-376-756-765-673-675-765-673-675-657-736-627-765-657-632-756-576-576-567-576-576-657-6_W.mp3)
  Segment 15: 22352ms ~ 22928ms (file: 163-130-632-162-610-215-167-163-610-613-276-623-132-130-726-132-136-102-120-126-137-163-130-103-367-216-120-123-120-103-136-621-132-107-103-167-160-163-167-130-310-156-126-176-1_V.mp3)
  Segment 16: 23928ms ~ 24504ms (file: 124-134-312-350-543-301-130-103-137-306-306-632-157-167-132-310-310-637-302-320-132-263-123-103-310-310-301-316-163-302-260-103 351-137-317-132-163-136-302-132-103-136-136-316-1_V.mp3)
  Segment 17: 25504ms ~ 26080ms (file: 302-302-316-136-132-312-301-301-326-306-130-136-103-103-160-301-316-301-301-312-301-132-316-130-310-162-126-310-302-132-126-132-123-236-310-306-326-213-126-236-236-263-256-516-5_V.mp3)
  Segment 18: 27080ms ~ 27656ms (file: 302-106-321-132-103-132-601-610-613-612 756-756-657-260-263-326-123-312-503-350-315-503-607-760-203-236-752-572-305-206-231-163-132-765-765-760-765-657-756-607-362-752-574-572-5_W.mp3)
  Segment 19: 28656ms ~ 29232ms (file: 103-123-167-216-162 163-103-675-106-126-567-712-215-136-136-176-127-156-106-107-106-163-162-163-176-761-263-136-162-612-765-621-102-106-316-126-162-312-136-103-172-103-165-160-1_V.mp3)
  Segment 20: 30232ms ~ 30808ms (file: 130-123-103-143-163-136-137-136-136-160-132-103-135-106-160-651-130-623-103-103-610-136-103-103-632-136-136-123-165-135-136-216-230_V.mp3)
  Segment 21: 31808ms ~ 32384ms (file: 612-160-621-612-156-546-126-651-365-162-652-562-162-632-236-632 627-263-623-132-132-261-623-263-620-627-312-120-312-132-123-362-132-610-136-672-675-672-672-627-675-657-567-263-1_W.mp3)
  Segment 22: 33384ms ~ 33960ms (file: 671-762-763-763-761-763-732-736-756-751 176-623-613-127-756-765-752-763-761-763-765-756-756-756-761-762-627-625-761-762-765-763-731-175-627-621-762-267-162-206 675-675-672-625-6_W.mp3)
  Segment 23: 34960ms ~ 35536ms (file: 526-562-526-256-261-631-162-632-162-163-167-615-651-526-562-567-263-672-623-126-316-651-675-526-625-623-362-326-236-652-623-126-316-316-135-315-321-231-523-253-256-265-325-325-1_W.mp3)
  Segment 24: 36536ms ~ 37112ms (file: 276-726-762-762-762-762-763-725-732-172-102-231-261-123-213-231_V.mp3)
  Segment 25: 38112ms ~ 38688ms (file: 361-136-620-163-103-106-316-716-765-160-673-231-613-765-263 675_V.mp3)
  Segment 26: 39688ms ~ 40264ms (file: 136-103-136-765-763-763 361-130-103-573-571-175-173-172-127-162-261-236-312-570-750-263-256-263-267-627 671-167-671-617-162-132 106-163-103-103-136 267-162-162-167-126-750-574-7_W.mp3)
  Segment 27: 41264ms ~ 41840ms (file: 675-625-261-361 153-135-326-231-316-165-214-527-172-275-216-217-217-312-371-524-352-312-641_V.mp3)
  Segment 28: 42840ms ~ 43416ms (file: 756-672-765-627-726-620-672-127-762-761-764-671-132-312-413-312-132-106-162-124-163-136-137-136-136-162-132-162-162-756-716-627-361-132-103-163-136-123-132-134-753-751 163-367-6_V.mp3)
  Segment 29: 44416ms ~ 44992ms (file: 536-361-132-567-261-603-601-621-231-125-235-152-301-673-267-751-657-763 162-216-102-201-261-167-321-123-653-163-136-621-120-651-123-513-163-126-176-132-162-213-216-623-621-261-6_W.mp3)
  Segment 30: 45992ms ~ 46568ms (file: 267-620-627-267-276-263-723-706-752-673-326-612-602-620-510-103-132-102-103-123-162-540-501-176-167-167-310-316-136-613-103-301-132-103-326-230-213-625-275-651-165-360-230-263-3_V.mp3)
  Segment 31: 47568ms ~ 48144ms (file: 615-176-716-176-316-263-261-306-132-316-165-106-102-103-167-136-612-126-106-160-132-736-301-136-153-617-310-301-301-301-301-301-301-631-163-361-160-607-607-276-327-132-126-625-1_V.mp3)
  Segment 32: 49144ms ~ 49720ms (file: 265-267-326-107-106-265-621-361-671-217-312-231-320-326-261-312-102-106-120-312-130-263-120-573-610-261-231-126-316-136-316-103-136-762-670-670-130-123-126-236-216_W.mp3)
  Segment 33: 50720ms ~ 51296ms (file: 537-231-203-320-320-301-310-130-130-130-310-130-360-316-302-310-326-360-302-320-203-236-236-320-310-130-361-236-230-203-320-231-321-321-132-312-361-320-312-301-236-326-163-162-3_V.mp3)
  Segment 34: 52296ms ~ 52872ms (file: 137-163-567-132-103-612-613-130-136-167-136-103-136-613-172-163-713-163-103-130-137-316-107-103-136-163-103-136-127-216-175-671-621-106-261-617-136-103-136-103-106 251-261-165-2_V.mp3)
  Segment 35: 53872ms ~ 54448ms (file: 231-213-312-312-315-153-537-527-576-536-352-351-236-235-263-267-267-216-231-312-317-312-321-321-231-231-312-127-321-126-127-265-265-265-265-275-675-267-237-263-326-261-263-216-6_W.mp3)
  Segment 36: 55448ms ~ 69452ms (file: 103-736-176-617-135-615-756-163 176-317-617-106-103-103-137-107-107-713-137-130-376-137-617-132-602-157-126-103-671-316-107 136-105-106-716-610-162-165-130-675-615-130-163-136-1_V.mp3)
  Segment 37: 70452ms ~ 71028ms (file: 136-617 167-162-132-103-162-216-712-316-126 106-615-165-160-513-167-612-136-157-130-123-612-316-165-652-165 367-362-263-752-126-123-516-175-612-627-261-162-162-256-136-163-316-1_W.mp3)
  Segment 38: 72028ms ~ 72604ms (file: 203-157-231-306-126-261-301-610-206-120-672-236-763-765-726-706-160-170-107-106-216-106-621-106-163-162-312-102-120-301-617-617-106-106-213-216-126-760-612-276-127-123-126-123-1_V.mp3)
  Segment 39: 73604ms ~ 74180ms (file: 167-602-765-657-230-130-302-312-163-103-312-132-623-162-632-236-301-102-261-312-301-163-106-163-206-261-126-261-172-201-213-230-132-102-301-260-620-126-132-326-102-136-750-731-1_V.mp3)
  Segment 40: 75180ms ~ 75756ms (file: 756-175-163-621-750-320-301-130-301-123-263-261-231-273-263-173-132-163-132-671-617-167-176-130-136-361-302-360-130-102-123-120-362-236-326-263-320-310-167-163-105-753-132-316-2_V.mp3)
  Segment 41: 76756ms ~ 77332ms (file: 132-312-175-265-263-216-261-263-260-265-231-567-356-253-325-320-754-576-563-356-365-317-536-653-326-527-532-536-361-531-135-351-536-576-316-162-621-621-617-671-763-637-675-637-5_W.mp3)
  Segment 42: 78332ms ~ 78908ms (file: 162-163-356-163-136-167-163-106-617-165-163-106-163-612-162-571-156-623-756_V.mp3)
  Segment 43: 79908ms ~ 80484ms (file: 135-637-175-321-627-316 571-756-756-753-756 657-760-673-631-675-762-672-160-136-162-612-673-167-672-672-763-673-516-516-567-756-675-657-675-657-675-657-657-657-675-675-675-567-7_W.mp3)
  Segment 44: 81484ms ~ 82060ms (file: 620-261-126 132 260-263-620-136-102-130-132 206-261 173-132-312-203-302-302 136-163-162-657-657-675 213-123-132-136-103-106-107-103-137-126-216-612-621-216-621-621-261-213-361-1_V.mp3)
  Segment 45: 83060ms ~ 83636ms (file: 136-570-103-103-175-516-132-132-216-176-167-167-136-106-136-162-103-167-136-175-601-167-130-165-561-162-176-167-106-765-362-670-103-136-103-376-103-175-673-361-136-316-136-165-6_V.mp3)
  Segment 46: 84636ms ~ 85212ms (file: 651-605-160-601-620-106-612-612-106-601-106-160-601-612-617-160-160-601-106-613-326-132-321-602-603-603-603-602-620-261-361-123-106-162-261-206-206-673-106-261-103-236-320-632-6_W.mp3)
  Segment 47: 86212ms ~ 86788ms (file: 613-126-261-132-602-627-316-106-627-670-162-162-160-136-613-136-103-136-673-106-136-136-671-601-617-162-761-103-167-106-103-165-162-167-123-106-175-160-612-162-126-316-761-567-1_V.mp3)
  Segment 48: 87788ms ~ 88364ms (file: 106-136-170-135-137-612-165-163-612-621-612-136-612-102-576-175-267-165-621-620-136-130-103 726-167-621-162-621-160-103-231-216-567-120-132-103-310-103-517-576-175-162-236-176-1_V.mp3)
  Segment 49: 89364ms ~ 89940ms (file: 312-132-103-135-350-203-130-130-132-610-216-627-607-627-123-216-250-563 632-160 547-547-327-623-230-326-632-672 236-321-130-130-103-132-105-302-130-103-130-123-236-652-267-267-2_W.mp3)
  Segment 50: 90940ms ~ 91516ms (file: 613-672-617-672-625-627-672-367-261 576-567-576 261_W.mp3)
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
> **Time**: 2026-04-03 07:26:58
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "오늘 아침은 정말 공기가 맑네요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 40, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "아침은", "start_ms": 130, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 230, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "공기가", "start_ms": 310, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "맑네요", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "새로 산 커피 머신으로 커피를 내려 마셨어요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "새로", "start_ms": 35, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "산", "start_ms": 115, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "커피", "start_ms": 175, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "머신으로", "start_ms": 255, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "커피를", "start_ms": 375, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "내려", "start_ms": 475, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "마셨어요", "start_ms": 555, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "창밖으로 보이는 나무들이 푸르러요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "창밖으로", "start_ms": 50, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "보이는", "start_ms": 180, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "나무들이", "start_ms": 290, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "푸르러요", "start_ms": 410, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "주말에는 근처 공원으로 산책을 가볼까 해요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "주말에는", "start_ms": 25, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "근처", "start_ms": 135, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "공원으로", "start_ms": 215, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "산책을", "start_ms": 315, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "가볼까", "start_ms": 405, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "해요", "start_ms": 495, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "책장에 꽂혀 있는 책들을 정리해야겠어요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "책장에", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "꽂혀", "start_ms": 155, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 235, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "책들을", "start_ms": 305, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "정리해야겠어요", "start_ms": 415, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "요즘 새로 배운 요리 레시피가 마음에 들어요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "요즘", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "새로", "start_ms": 110, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "배운", "start_ms": 190, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "요리", "start_ms": 260, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "레시피가", "start_ms": 340, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "마음에", "start_ms": 460, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "들어요", "start_ms": 560, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "친구들과 저녁 약속이 있어 기대되네요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "친구들과", "start_ms": 40, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "저녁", "start_ms": 170, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "약속이", "start_ms": 250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있어", "start_ms": 350, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "기대되네요", "start_ms": 430, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "어제보다 날씨가 조금 쌀쌀해진 것 같아요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "어제보다", "start_ms": 35, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 145, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 245, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "쌀쌀해진", "start_ms": 325, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 445, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 485, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "외출할 때 얇은 겉옷을 챙겨야겠네요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "외출할", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 150, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "얇은", "start_ms": 190, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "겉옷을", "start_ms": 270, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "챙겨야겠네요", "start_ms": 380, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "오늘 하루도 알차게 보내봅시다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 60, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "하루도", "start_ms": 150, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "알차게", "start_ms": 260, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "보내봅시다", "start_ms": 370, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "편안한 음악을 들으며 쉬고 싶어요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "편안한", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "음악을", "start_ms": 150, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "들으며", "start_ms": 260, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "쉬고", "start_ms": 360, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "싶어요", "start_ms": 430, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "창가에 앉아 책을 읽는 시간이 소중해요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "창가에", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "앉아", "start_ms": 130, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "책을", "start_ms": 210, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "읽는", "start_ms": 290, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "시간이", "start_ms": 380, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "소중해요", "start_ms": 490, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "부모님께 안부 전화를 드려야겠어요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "부모님께", "start_ms": 45, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "안부", "start_ms": 175, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "전화를", "start_ms": 265, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "드려야겠어요", "start_ms": 375, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "따뜻한 차 한 잔이 생각나는 밤이네요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "따뜻한", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "차", "start_ms": 160, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 200, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "잔이", "start_ms": 240, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "생각나는", "start_ms": 320, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "밤이네요", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "오늘 하루도 참 수고 많으셨습니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 35, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "하루도", "start_ms": 125, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "참", "start_ms": 235, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "수고", "start_ms": 275, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "많으셨습니다", "start_ms": 355, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "내일은 더 좋은 일들이 생길 거예요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "내일은", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 150, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 190, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "일들이", "start_ms": 270, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "생길", "start_ms": 380, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 460, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "편안하게 푹 자고 기분 좋게 일어나요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "편안하게", "start_ms": 25, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "푹", "start_ms": 155, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "자고", "start_ms": 195, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "기분", "start_ms": 275, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "좋게", "start_ms": 355, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "일어나요", "start_ms": 435, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "항상 건강하시고 행복하세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "항상", "start_ms": 50, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "건강하시고", "start_ms": 140, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "행복하세요", "start_ms": 300, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "언제나 응원하겠습니다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "언제나", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "응원하겠습니다", "start_ms": 155, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "감사합니다 안녕히 계세요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 45, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 225, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "계세요", "start_ms": 335, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "좋은 아침입니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 40, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "아침입니다", "start_ms": 130, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "오늘 날씨가 참 화창하네요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 30, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 120, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "참", "start_ms": 230, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "화창하네요", "start_ms": 270, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "이런 날엔 나들이 가기 딱이죠",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "이런", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "날엔", "start_ms": 130, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "나들이", "start_ms": 210, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "가기", "start_ms": 320, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "딱이죠", "start_ms": 400, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "어디로 가면 좋을까요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "어디로", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "가면", "start_ms": 150, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "좋을까요", "start_ms": 240, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "가까운 공원이나 강변 어때요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "가까운", "start_ms": 35, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "공원이나", "start_ms": 135, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "강변", "start_ms": 265, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "어때요", "start_ms": 345, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "좋은 생각이에요 도시락 싸서 가요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 45, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "생각이에요", "start_ms": 135, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "도시락", "start_ms": 285, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "싸서", "start_ms": 395, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "가요", "start_ms": 475, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "비빔밥이나 샌드위치 괜찮겠네요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "비빔밥이나", "start_ms": 50, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "샌드위치", "start_ms": 180, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "괜찮겠네요", "start_ms": 300, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "저는 과일도 좀 챙길게요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "과일도", "start_ms": 120, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 210, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "챙길게요", "start_ms": 250, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "벌써부터 설레네요 빨리 가고 싶어요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "벌써부터", "start_ms": 30, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "설레네요", "start_ms": 150, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "빨리", "start_ms": 260, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "가고", "start_ms": 350, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "싶어요", "start_ms": 420, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "그럼 준비해서 한 시간 뒤에 만나요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "준비해서", "start_ms": 130, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 250, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "시간", "start_ms": 290, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "뒤에", "start_ms": 370, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 440, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "네 알겠습니다 그때 봬요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 40, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "알겠습니다", "start_ms": 80, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "그때", "start_ms": 230, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "봬요", "start_ms": 310, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "요즘 공부하는 거 어때요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "요즘", "start_ms": 35, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "공부하는", "start_ms": 125, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 245, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "어때요", "start_ms": 285, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "조금 힘들지만 재미있어요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "조금", "start_ms": 45, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "힘들지만", "start_ms": 125, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "재미있어요", "start_ms": 235, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "새로운 걸 배우는 게 보람차요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "새로운", "start_ms": 30, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "걸", "start_ms": 140, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "배우는", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 280, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "보람차요", "start_ms": 320, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "꾸준히 하다 보면 실력이 늘 거예요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "꾸준히", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "하다", "start_ms": 150, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "보면", "start_ms": 230, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "실력이", "start_ms": 310, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "늘", "start_ms": 410, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 450, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "네 응원해 주셔서 감사합니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 40, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "응원해", "start_ms": 80, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "주셔서", "start_ms": 190, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 290, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "서로 도와가며 열심히 해봐요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "서로", "start_ms": 25, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "도와가며", "start_ms": 105, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "열심히", "start_ms": 225, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "해봐요", "start_ms": 335, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "오늘 점심은 뭐 먹을까요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 50, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "점심은", "start_ms": 140, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 240, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "먹을까요", "start_ms": 280, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "오랜만에 파스타 어때요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "오랜만에", "start_ms": 35, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "파스타", "start_ms": 165, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "어때요", "start_ms": 275, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "좋아요 크림 파스타가 당기네요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "좋아요", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "크림", "start_ms": 155, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "파스타가", "start_ms": 235, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "당기네요", "start_ms": 365, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "저는 토마토 파스타로 할게요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "토마토", "start_ms": 120, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "파스타로", "start_ms": 220, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "할게요", "start_ms": 350, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "그럼 근처 맛집으로 가요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "근처", "start_ms": 110, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "맛집으로", "start_ms": 190, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "가요", "start_ms": 300, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "벌써 배가 고파지네요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "벌써", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "배가", "start_ms": 130, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "고파지네요", "start_ms": 200, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "맛있게 먹고 힘내서 오후도 파이팅해요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "맛있게", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "먹고", "start_ms": 150, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "힘내서", "start_ms": 230, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "오후도", "start_ms": 330, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "파이팅해요", "start_ms": 440, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "네 맛점하세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 25, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "맛점하세요", "start_ms": 65, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "감사합니다",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 35, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "즐거운 주말 보내세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "즐거운", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "주말", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "보내세요", "start_ms": 240, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "가족들과 행복한 시간 되시길",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "가족들과", "start_ms": 40, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "행복한", "start_ms": 170, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "시간", "start_ms": 270, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "되시길", "start_ms": 350, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "언제나 건강이 최고입니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "언제나", "start_ms": 30, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "건강이", "start_ms": 140, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "최고입니다", "start_ms": 240, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "안녕히 계세요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "계세요", "start_ms": 160, "dur_ms": 120, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 07:26:58*
