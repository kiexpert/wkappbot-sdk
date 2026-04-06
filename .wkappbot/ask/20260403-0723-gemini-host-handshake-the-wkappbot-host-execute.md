# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=07:22:20  uptime=21785s  mem=1571MB  system_processes=448"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_072210.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 107-103-132-136-162-136-302-320-231-302-236-230-267-260-725-657-675-627-756-657-765-675-176-620-612-106-765-376-367-326-326-372-730-736-267-625-267-317-751-705-657-675-671-102-1_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 526-623-261-263-320-320-302-320-326-312-302-320-130-302-231-321-326-321-231-231-657-312-130-547-572 326-326-316-326-236-312-321-615-652-132-312-216-261-213-162-163-231-320-126-6_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 675-675-130-362-132-132-176-163-106-106-123-261-620-627-602-607-602-627-670-620-627-620-265-310-320-132-325-352-235-203-203-203-206-206-213-123-103-136-574-351-563-305-230-203-2_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 213 257-215-127-327-321-123-132-321-321-321-237-327-375-327-325-327-321-236-326-326-321-375-723-362-327-326-573-537-537-573-572-537-356-625-235-312-235-361-326-352-315-315-721-3_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 356-315-316-657-265-236-236-125-157-153-514-531-514-152-531-512-541-543-540-351-135-153-257-574-574-573-572-532-265-231-362-312-361-362-326-362-362-653-652-263-236-231-263-215-5_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 617-673-621-625-267-162-163 765-673-713_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 612-612-621-612-167-107-167-163-167-143-124-317-617-103-103-102-174-136-163-312-137-132-136-162-753-713-765-761-673-176 137 763-765-107-705-675-701-167-162-136-132-132-671-617-1_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 265-257 267-256-267-627-267-263-263 576-657-267-267-267-267-267 627_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 106-106-106-617-136-301-312-327-372-301-756-301-706-317-317-132-315-351-315-312 326-203-320-763-765-760-672-362-631-135-136-206-206-206_V.mp3)
  Segment 10: 14184ms ~ 14868ms (file: 206-320-236-723-725-732-750-701-725-703-573-106-157-176-160-510-547-547-517-516-632-106-675-615-106-756-516-132-167-102-631-176-765-167-160-103-231-132 123-312 123-132-627-632-7_W.mp3)
  Segment 11: 15868ms ~ 16444ms (file: 267-263-267-263-263-326-263-236-163-701-762-762-672-627-312-750-756-627-236-306-203-230-137-607-631-106-136-310-135-570-735-176-173-127-612-103-103-326-623-623-621-540-156-156-1_V.mp3)
  Segment 12: 17444ms ~ 18020ms (file: 103-602-132-163-132-136-102-106-167-637-672-176-672-267-627-602-162-130-103-132-160-162-106-761-326-263-726-617-762-762-761-657-675-672-103-152-126-657-326_V.mp3)
  Segment 13: 19020ms ~ 19596ms (file: 175-172-312-102-267-267-123-103-103-103-136-137-136-132-107-107-107-103_V.mp3)
  Segment 14: 20596ms ~ 21172ms (file: 132-103-613-162-132-136-317-162-163-260-162-162-136-263-627-623-162-103-106-167-126-237-167-106-136-360-103-623-103-106-362-125-136-175-715-162-167-136-157-162-126-103-176-163-6_V.mp3)
  Segment 15: 22172ms ~ 22748ms (file: 612-103-310-137-107-137-107-367-105-103-163-167-231-561-321-316-130-321-130-103-162-352-601-671-671-163-137-671-602-672-632_V.mp3)
  Segment 16: 23748ms ~ 24324ms (file: 132-103-106-103-143-103-132-103-136-163-514-153-635-162-213-456-726-261-726-265-627-627-625-632-627-672-327-162-310-103-132-132-576-756-756-765-756-307-603-361-630-631-136-670-1_V.mp3)
  Segment 17: 25324ms ~ 25900ms (file: 136 106-316-301-327-310-671-167-310-260-632-236-302-302-326-361-362-612-671-537 172-126-162-103-301-302-307-132-123-102-132-126-126-162-123-103-173-670-306-103-162-372-124 750-5_V.mp3)
  Segment 18: 26900ms ~ 27476ms (file: 612-126-102-126-106-312-301-137-136-163-103-203-203-263-261-216-321-103-613-206-302-351-350-163-103-612-306-216-301-705-362-630-361-136-307-321-326-327-132-301-132-136-613-260-6_V.mp3)
  Segment 19: 28476ms ~ 29052ms (file: 567-576-576-357-756-365-765-132-132-561-627-632-267-326-625-236-627-356-657-652-563-652-671-256-561-652-623-315-351-327-325-623-653-652-625-652-627-562-657-652-536-657-576-562-6_V.mp3)
  Segment 20: 30052ms ~ 30628ms (file: 167 672-623-637-621-617-612-163-613-765-762-765-675-763-672-673-627-617-617-675-675-675-627-623-765-675-762-765-765-672-675-672-627-621-627-672-675-671-672-627-275-675-756-765 5_W.mp3)
  Segment 21: 31628ms ~ 32204ms (file: 675-265-567-657-652-632-326-263-261-613-625-261-253-652-576-625-675-623-625-173-361-623-625-623-621-263-263-213-321-132-231-132-132-103 310 362-123-213-132-236-621-120-301-312-1_W.mp3)
  Segment 22: 33204ms ~ 33780ms (file: 153-574-507-561-273-765-756-576-103-310-301-132-123-314-673-706-621-213-326-321-367-750-370-320-321-302-310-301-310-537-310-301-203-230-362-236-216-360-320-320-756-762-726-320-3_V.mp3)
  Segment 23: 34780ms ~ 35356ms (file: 302-310-136-310-756-370-316-261-267-270-307-762-302-251-123-132-321-236-130-132-312-312-326-365-310-321-301-530-301-302-301-513-203-312-206-264-320-320-503-510-312-203-201-302-6_W.mp3)
  Segment 24: 36356ms ~ 36932ms (file: 765-765 562-267-263-672-627-627-153-576-537-576-657-762-632-326-267-321-206-213-130-103-176-106-176-617-167-651-756-236-617-167-126-136-136-623-621-567-517-156-517-651-567-567-5_W.mp3)
  Segment 25: 37932ms ~ 38508ms (file: 572-652-567-563-657-625-625-627-726-672-623-162-123-162-156-132-651-165-657-670-761-165 216-365-617-157-206-260-263 260-627-627-675-627 267-621-635-675-612-621-671-617-361-176-1_W.mp3)
  Segment 26: 39508ms ~ 40084ms (file: 675-176-162-712-260-263-617-675-754-756-765-756-756-756-756-750-754-752-754-143-132_W.mp3)
  Segment 27: 41084ms ~ 41660ms (file: 521-523-315-136-312-136-135-136-126-123-526-521-523-215-312-216-201-201-513-152-521-135-251-312-326-263-216-123-321-231-123-216-216-263-216-312-316-312-136-310-136-514-526-572-5_W.mp3)
  Segment 28: 42660ms ~ 43236ms (file: 206-260-206-206-206-263-627-765-756-570-547-547-504-154-321-301-321-541-513-571-753-543-547-547-134-312-315-310-130-103-134-103-312-136-761-716-671-623-621-612-123-310-320-132-1_V.mp3)
  Segment 29: 44236ms ~ 44812ms (file: 210-102-163-106-601-261-260-260-103-132-103-136-106-106-261-326-362-302-312-301-370-602-306-230-236-365-206-236-260-620-620-620-102-106-675-673-617-301-301-630-132-312-260-621-2_V.mp3)
  Segment 30: 45812ms ~ 46388ms (file: 316-136-162-267-216-362-263-623-621-126-261-263-326-263-231-621-162-162-172-216-276-326-326-236-326-326-316-312-326-316-362-362-312-136-312-321-326-326-573-756-735-317-321-321-3_W.mp3)
  Segment 31: 47388ms ~ 47964ms (file: 326-312-327-367 321-321-267-132-267-623-623_W.mp3)
  Segment 32: 48964ms ~ 49540ms (file: 136-167-126-136 761-163-637-657-725-765-756-675-756-576-756-752-135-751-716-751-103-612-127-267-710-715-137-167-316-317-126-156-105-756-675-567-657-576-567-631-516-516-765-765-7_W.mp3)
  Segment 33: 50540ms ~ 51116ms (file: 312-312-627-357-623-261-623-256-657-365-567-576-572-635-537-657-657-562-567-657-576-675-657-657-576-657-567-675-657-756-735-752-675-576-765-657-756-567-657-657-576-356-567-765-5_W.mp3)
  Segment 34: 52116ms ~ 52692ms (file: 132-312-316-301-132-231-327-236-230-236-163-157-751-601-613-316-632-165-671-130-706-637-320-263-123-312-320-302-230-574-503-135-503-305-576-620-260-206-602-173-263-236-752-276-3_W.mp3)
  Segment 35: 53692ms ~ 54268ms (file: 167 165-173-136-106-132-632-162-613-103-106-310-713-103-160-613-162-261-130-162-623-236-267-162-160-167-601-130-136-106-162-612-162-361-103-160-236-103-103-106-726-763-132-162-3_V.mp3)
  Segment 36: 55268ms ~ 55844ms (file: 136-726-671-670-231-206-162-106-103-162-607-671-162-631-102-106-630-601-206-602-260-326-362-106-103-167-216-236-206-205-570-120-103-123-126-103-136-132-321-132-612-261-326-201-1_V.mp3)
  Segment 37: 56844ms ~ 57420ms (file: 672-627-160-275-675-605-637-671-762_W.mp3)
  Segment 38: 58420ms ~ 58996ms (file: 716-167-167-576-567-657-672-627-627-653-612-617-756-361-657-675-671-673-657-516-672-673-621-231-263-321-103-132-756-750-705-754-765-605-135-301-570-312-236-302-302-312-673-312-1_W.mp3)
  Segment 39: 59996ms ~ 60572ms (file: 263-276-726-712-712-175-127-712-712-721-172 267-256-725-257-276-725-725-726-725-726-627-762-276-276-725-572-627-627-265-756-567-715-617-756-752-756-675-526-752-756-752-256_W.mp3)
  Segment 40: 61572ms ~ 62148ms (file: 576-574-567-671-765-675-751-671 103_W.mp3)
  Segment 41: 63148ms ~ 63724ms (file: 756-756-756-765-165-706-573-671-576-567-513-135-165-570-503-105-506-130-514-567-167-103-163-276-361-362-176-320-176-301-136-106-671-103-136-106-106-126-160-327-762-162-673-103-1_V.mp3)
  Segment 42: 64724ms ~ 65300ms (file: 635-627-126-301-631-631-316-361-507-520-507-570-506 672-602-602-627-627-625-102-103-134-143-705-750-136-137-132-136-574-570-136-106-162-136-265-261-762-267-267-726-267-612-162-1_V.mp3)
  Segment 43: 66300ms ~ 66876ms (file: 576-576-623-362-672-267-675-326-632-123-132-316-132-621-326-261-627-627-326-672-673 316-631-672-267-765-765-716-176-276-612-765-617-367-167-617-762-765-765-167-167-617-167-371-6_W.mp3)
  Segment 44: 67876ms ~ 68452ms (file: 102-132-312-160-761-256-625-136-103-106-517-751-126-615-162-120-132-136-231-236-132-176-132-162-321-123-123-136-231-123-123-562-256-572-123-136-126-136-312-136-136-316-162-216-3_W.mp3)
  Segment 45: 69452ms ~ 70028ms (file: 167-103-130-103-136-132-143 103-310-362-620-632-160-130-106-617-675-107-103-675-670-301-301-130-103-312-106-103-136-136-106-510-627-620-260-163-136-106-671-675-627-167-106-107-1_V.mp3)
  Segment 46: 71028ms ~ 71604ms (file: 675-613-160-160-162-312-162-657-651-561-650-765-167-150-162-132-621-601-267-261-627-167-627-621-673-625-613-627-672-260-620-621-623-263-267-621-725-765-762-675-765-263-627-367-3_W.mp3)
  Segment 47: 72604ms ~ 73180ms (file: 162-102-103-153-201-231-126-124-132-546-576-263-260-620-504-135-105-576-627-132-156-102-514-103-312-320-231-321-302 276-132-172-602-657-163-175-176-675-167-627-756-625-627-362-1_W.mp3)
  Segment 48: 74180ms ~ 74756ms (file: 162-165-103-163-163-106-106-156-127-672-136-671-103-163-106-105-123-673-130-103-513-132-103-103-103-143-165-352-172-136 160-716-610-106-103-106-167-103-136-167-153-675-176-632-1_V.mp3)
  Segment 49: 75756ms ~ 76332ms (file: 326-632-632-625-265-627-623-261-126-123-126-203-231-312-320-235-352-532-127-132-123-201-231-132-605-657-670-612-106-106-167-162-326-326-235-216-623-623-652-236-231-236-236-567-7_W.mp3)
  Segment 50: 77332ms ~ 77908ms (file: 163-167-316-163-631-567-106-267-216-163-120-156-160-725-106-103-617-163-160-160-162-163-620-162-106-136-165-163-657-160-602-162-276-162-617-612-167-670-106-263-162-675-765-617-1_V.mp3)
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
> **Time**: 2026-04-03 07:23:37
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "오늘 아침은 평소보다 일찍 일어났어요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 40, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "아침은", "start_ms": 130, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "평소보다", "start_ms": 230, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "일찍", "start_ms": 350, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "일어났어요", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "창문을 여니 시원한 바람이 들어오네요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "창문을", "start_ms": 35, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "여니", "start_ms": 145, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "시원한", "start_ms": 235, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "바람이", "start_ms": 335, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "들어오네요", "start_ms": 435, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "따뜻한 커피 한 잔 마시며 시작해요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "따뜻한", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "커피", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 240, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "잔", "start_ms": 280, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "마시며", "start_ms": 340, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "시작해요", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "오늘 할 일을 메모지에 적어봅니다",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 25, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 115, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "일을", "start_ms": 175, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "메모지에", "start_ms": 255, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "적어봅니다", "start_ms": 375, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "작은 목표부터 차근차근 이뤄봐요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "작은", "start_ms": 45, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "목표부터", "start_ms": 135, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "차근차근", "start_ms": 255, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "이뤄봐요", "start_ms": 385, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "점심엔 친구와 맛집에 가기로 했어요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "점심엔", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "친구와", "start_ms": 130, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "맛집에", "start_ms": 240, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "가기로", "start_ms": 340, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "했어요", "start_ms": 450, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "어떤 음식을 먹을지 고민되네요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "어떤", "start_ms": 40, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "음식을", "start_ms": 130, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "먹을지", "start_ms": 240, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "고민되네요", "start_ms": 340, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "오후에는 도서관에서 책을 빌릴 거예요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "오후에는", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "도서관에서", "start_ms": 155, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "책을", "start_ms": 295, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "빌릴", "start_ms": 375, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 455, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "읽고 싶었던 소설이 마침 있기를 바라요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "읽고", "start_ms": 50, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "싶었던", "start_ms": 120, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "소설이", "start_ms": 220, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "마침", "start_ms": 330, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "있기를", "start_ms": 420, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "바라요", "start_ms": 520, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "저녁엔 가벼운 산책으로 마무리할래요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "저녁엔", "start_ms": 60, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "가벼운", "start_ms": 170, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "산책으로", "start_ms": 270, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "마무리할래요", "start_ms": 400, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "노을 지는 풍경이 참 아름다울 것 같아요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "노을", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "지는", "start_ms": 120, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "풍경이", "start_ms": 210, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "참", "start_ms": 320, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "아름다울", "start_ms": 360, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 480, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 520, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "오늘 하루도 감사한 마음으로 보내요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 30, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "하루도", "start_ms": 120, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "감사한", "start_ms": 230, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "마음으로", "start_ms": 330, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "보내요", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "모두가 행복한 금요일이 되길 바랍니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "모두가", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "행복한", "start_ms": 155, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "금요일이", "start_ms": 255, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "되길", "start_ms": 375, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "바랍니다", "start_ms": 455, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "웃음이 끊이지 않는 하루 보내세요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "웃음이", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "끊이지", "start_ms": 160, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "않는", "start_ms": 260, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "하루", "start_ms": 340, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "보내세요", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "작은 것에도 감사함을 느껴보세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "작은", "start_ms": 35, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "것에도", "start_ms": 125, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "감사함을", "start_ms": 225, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "느껴보세요", "start_ms": 345, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "그러면 더 큰 행복이 찾아올 거예요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "그러면", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 150, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 190, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "행복이", "start_ms": 230, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "찾아올", "start_ms": 330, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "항상 긍정적인 마음가짐을 가져요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "항상", "start_ms": 25, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "긍정적인", "start_ms": 115, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "마음가짐을", "start_ms": 245, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가져요", "start_ms": 395, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "오늘도 화이팅 넘치는 하루 되세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "오늘도", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "화이팅", "start_ms": 160, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "넘치는", "start_ms": 270, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "하루", "start_ms": 370, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "되세요", "start_ms": 450, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "함께라서 더 즐거운 시간입니다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "함께라서", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 155, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "즐거운", "start_ms": 195, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "시간입니다", "start_ms": 295, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "감사의 인사를 전하며 인사드려요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "감사의", "start_ms": 45, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "인사를", "start_ms": 135, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "전하며", "start_ms": 235, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "인사드려요", "start_ms": 345, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "언제나 당신의 꿈을 응원할게요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "언제나", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "당신의", "start_ms": 150, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "꿈을", "start_ms": 240, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "응원할게요", "start_ms": 320, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "포기하지 말고 끝까지 달려봐요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "포기하지", "start_ms": 30, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "말고", "start_ms": 140, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "끝까지", "start_ms": 220, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "달려봐요", "start_ms": 330, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "빛나는 미래가 기다리고 있을 거예요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "빛나는", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "미래가", "start_ms": 160, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "기다리고", "start_ms": 260, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "있을", "start_ms": 380, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "스스로를 믿고 당당하게 걸어가요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "스스로를", "start_ms": 40, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "믿고", "start_ms": 170, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "당당하게", "start_ms": 250, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "걸어가요", "start_ms": 380, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "당신은 충분히 잘 해내고 있습니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "당신은", "start_ms": 35, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "충분히", "start_ms": 135, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 245, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "해내고", "start_ms": 285, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 385, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "오늘의 노력이 내일의 열매가 될 거예요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "오늘의", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "노력이", "start_ms": 145, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "내일의", "start_ms": 255, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "열매가", "start_ms": 355, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "될", "start_ms": 465, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 505, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "힘들 땐 잠시 쉬어가는 것도 방법이죠",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "힘들", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "땐", "start_ms": 130, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "잠시", "start_ms": 170, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "쉬어가는", "start_ms": 250, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "것도", "start_ms": 380, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "방법이죠", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "다시 일어설 힘을 얻을 수 있으니까요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "다시", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "일어설", "start_ms": 120, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "힘을", "start_ms": 230, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "얻을", "start_ms": 310, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 390, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "있으니까요", "start_ms": 430, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "함께 걷는 이 길이 즐거웠으면 해요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "함께", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "걷는", "start_ms": 110, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 190, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "길이", "start_ms": 230, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "즐거웠으면", "start_ms": 310, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "해요", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "서로에게 따뜻한 위로가 되어주기로 해요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "서로에게", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "따뜻한", "start_ms": 170, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "위로가", "start_ms": 280, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "되어주기로", "start_ms": 390, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "해요", "start_ms": 540, "dur_ms": 30, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "오늘도 참 고생 많으셨습니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "오늘도", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "참", "start_ms": 150, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "고생", "start_ms": 190, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "많으셨습니다", "start_ms": 280, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "편안한 밤 보내시고 좋은 꿈 꾸세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "편안한", "start_ms": 35, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "밤", "start_ms": 145, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "보내시고", "start_ms": 205, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 335, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "꿈", "start_ms": 415, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "꾸세요", "start_ms": 475, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "내일은 더 밝은 태양이 뜰 거예요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "내일은", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 155, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "밝은", "start_ms": 195, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "태양이", "start_ms": 295, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "뜰", "start_ms": 405, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 465, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "기대 가득한 마음으로 잠자리에 들어요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "기대", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "가득한", "start_ms": 110, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "마음으로", "start_ms": 220, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "잠자리에", "start_ms": 330, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "들어요", "start_ms": 450, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "항상 곁에서 응원하고 있음을 기억하세요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "항상", "start_ms": 50, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "곁에서", "start_ms": 140, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "응원하고", "start_ms": 240, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "있음을", "start_ms": 360, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "기억하세요", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "당신은 세상에서 가장 소중한 사람입니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "당신은", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "세상에서", "start_ms": 140, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 260, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "소중한", "start_ms": 340, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "사람입니다", "start_ms": 450, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "스스로를 아껴주고 사랑해 주세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "스스로를", "start_ms": 25, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "아껴주고", "start_ms": 145, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "사랑해", "start_ms": 255, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 365, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "당신의 앞날에 축복이 가득하길 빌게요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "당신의", "start_ms": 50, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "앞날에", "start_ms": 140, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "축복이", "start_ms": 240, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "가득하길", "start_ms": 350, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "빌게요", "start_ms": 480, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "오늘도 고마웠습니다 안녕히 주무세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "오늘도", "start_ms": 35, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "고마웠습니다", "start_ms": 145, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 345, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "주무세요", "start_ms": 445, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "감사합니다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 45, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "다음에 또 만나요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 150, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 210, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "행복한 꿈 꾸시길",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "행복한", "start_ms": 30, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "꿈", "start_ms": 140, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "꾸시길", "start_ms": 200, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "굿나잇",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "굿나잇", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "사랑합니다",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "사랑합니다", "start_ms": 40, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "축복합니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "축복합니다", "start_ms": 25, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "내일 봐요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "내일", "start_ms": 35, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "봐요", "start_ms": 135, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "안녕",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "안녕", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "바이바이",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "바이바이", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "좋은 아침",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 30, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "아침", "start_ms": 120, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "벌써 아침이네요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "벌써", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "아침이네요", "start_ms": 160, "dur_ms": 150, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 07:23:37*
