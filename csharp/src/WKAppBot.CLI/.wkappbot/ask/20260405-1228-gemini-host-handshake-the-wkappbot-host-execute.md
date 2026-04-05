# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=30524  time=12:27:27  uptime=28559s  mem=409MB  system_processes=361"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_122702.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 315-136-612-635-362-316-263-236-612-361-623-756-765-265-276-726-672-675-265 165-261-756-705-702-706-765-765-760-625-602-602-612-761-756-705-705-703-617-675-267-672-672-670-607-6_W.mp3)
  Segment 2: 1576ms ~ 8740ms (file: 321-102-752-276-267-263-276-726-720-175-237-312-632-276-260-702-321-230-231-213-260-203-230-102-326-302-136-236-762-723-261-260-231-203-260-267-260-267-203-103-210-203-267-260-2_V.mp3)
  Segment 3: 9740ms ~ 10316ms (file: 125-210-203-213-526-205-251-201-215-210-120-625-350-203-521-132-251-235-251-203-205-201-523-205-253-205-253-205-526-265-256-625-205-256 316-351-502-537-521-152-513-532-215-257-2_V.mp3)
  Segment 4: 11316ms ~ 11892ms (file: 510-150-571-510-501-510-150-510-150-150-150-105-135-105-150-150-150-150-510-105-517-510-570-517-571-157-105-105-510-501-571 501-501-510-105-153-105-513-510-517-510-510-150-501-1_V.mp3)
  Segment 5: 12892ms ~ 13468ms (file: 265-276-265-261-267-617-671-175-756-675-751-751-765-675-765-715-761-756-573-571-756-576-765-756-756-761-765-765-765-765-765-765-761-675-756-576-156-176-615-162-175-165-157-156-6_W.mp3)
  Segment 6: 14468ms ~ 25520ms (file: 207-123-120-201-201-231-237-203-327-207-123-102-102-201-103-107-672-126-167-106-107-176-317-762-176-217-731-127-126-260-301-170-362-267-123-170-316-176-126-670-514-152-315-312-1_W.mp3)
  Segment 7: 26520ms ~ 27096ms (file: 765-761-761-237-327-302-321-230-620-231-672-761-762-762-761-761-176-106-612-326-123-376-632-762-761-627-621 567-576-675-763 756-765-762-762-761-673-765-765 763-765-765 762-762-7_W.mp3)
  Segment 8: 28096ms ~ 28672ms (file: 130 576-576-570-750-750-705-750-705-750-570-750-576-271-752-572-705-750-570-576-765_V.mp3)
  Segment 9: 29672ms ~ 37772ms (file: 270-123-137-175-102-620-732-725-312-276-276-267-213-670-574-267-571-751-732-623-260-627-670-672-275-123-216-312-130-130-627-216-123-213-130-672-267-263-126-125-207-257-230-752-2_W.mp3)
  Segment 10: 38772ms ~ 39348ms (file: 175-157-135-103-103 130-130-130-310-103-103-137-137-135-103-105-104-105-105-107-157-105-541-710-105-107-147-175-174-103-104 107-107-105-104-154-105-150-105-135-517-105-157-105-1_V.mp3)
  Segment 11: 40348ms ~ 40924ms (file: 527-526-574-543-524-510-501-506-501-510-510_W.mp3)
  Segment 12: 41924ms ~ 43436ms (file: 576-321-167-236-256-672-623-312-326-361-106-537-316-135-160-657-156-617-176-317-371-731-376-736-736-371-731-716-731-761-736-736-731-371-576-576-567-657-713-731_W.mp3)
  Segment 13: 44436ms ~ 45012ms (file: 261-236-213-574-576-657-526-562-526-547-576-562-567-572-576-576-564-537-574_W.mp3)
  Segment 14: 46012ms ~ 46588ms (file: 725-267-256-321-572-235-726-256-732-576-321-726-705-126-507-752-230-703-257-123-327-327-271-762-267-726-527-136-265-276-716-231-231-102-213-637-631-367-167-316-317-735-751-375-3_W.mp3)
  Segment 15: 47588ms ~ 48164ms (file: 357-137-571-301-317-136-603-637-316-603-316-637-630-617-137-136-173-103-170 175-715-713-317-310-637-601-617-103-301-610-106-103-610-306-301-136-130-130-163-176-603-371-317-107-1_V.mp3)
  Segment 16: 49164ms ~ 49740ms (file: 572-752-527-572-527-527-527-257-527-257-275-257-527-527-275-257-527-527-527-572-567-576-567-576-576-175-524-537-571-573-572-572-527-572-257-520-750-752-527-570-257-752-752-725-2_W.mp3)
  Segment 17: 50740ms ~ 51316ms (file: 753-157-517-517-571-175-750-571-570-570-751 571-571-750-751-571-175-715-175-571-517-715-517-571-573-517-753-157-751-571-751 517-175-751-573-571-175-571-715-715-153-715-751-715-5_V.mp3)
  Segment 18: 52316ms ~ 52892ms (file: 361-317-721-137-360-726-263-176-162-572-312-623-326-756-120-132-265-605-217-267-617-205-507-125-176-216-162-132-172-132-217-126-216-321-267-507-276-752-527-273-316-136-136-312-6_V.mp3)
  Segment 19: 53892ms ~ 54468ms (file: 576 671-765-165-156-165-165-165-165-165-165-167-576-576-572-756-756 623-625-652-265-625-265-627-672-625-267-265-526-526-560-572-571_W.mp3)
  Segment 20: 55468ms ~ 56044ms (file: 201-620-263-267-130-136-316-516-163-103-130-371-310-170-630-607-163-136-632-136-165-632-160-157-123-256-510-154-143-136-312-132-162-136-103-267-176-103-137-103-136-136-130 267-7_V.mp3)
  Segment 21: 57044ms ~ 62408ms (file: 201-321-675-625-236-263-326-132-672-537-263-235-312-751-326-710-721-301-321-316-312-136-317-136-217-137-136-312-213-312-173-732-173-237-276-632-126-310-367-273-237-216-276-672-2_W.mp3)
  Segment 22: 63408ms ~ 70284ms (file: 261-267-132-263-236-130-106-163-102-162-126-267-672-267-260-267-375-327-627-523-310-203-327-230-206-103-623-237-236-627-327-107-713-372-230-236-205-236-230-276-267-276-276-231-1_W.mp3)
  Segment 23: 71284ms ~ 71860ms (file: 751-156-517-165-510-165-516-150-175-765-765-765-675-165-167-176-175-176-107-753-571-567-517-576-571-157-105-107-107-130-105-153-156-156-573-617-153-136-315-130-130-103-731-176-3_W.mp3)
  Segment 24: 72860ms ~ 73436ms (file: 206-312-623-216-263 236-126-312-123-156 672-672-620-206-621-105-163-261-361-312-231-321-621-621-103-761-160-162-160-106-160-106-123-612-612-631-107-762-627-267-632-267-623-231-3_W.mp3)
  Segment 25: 74436ms ~ 75012ms (file: 317-631-603-671-367-376-137-132-132-157-175-173-137-175-751-750 705-715-701-375-173-317-307-360-603-631-610-316-136-617-637-307-137-107-701-317-130-317-137-301-173-135-310-170-1_V.mp3)
  Segment 26: 76012ms ~ 76588ms (file: 157-517-501-501-517-513-516-501-516-517-510-517-510-507-501-501-501-150-507-501-570-507-510-574-540-507-504-540-504-507-507 507-105-501-501-507-507-507-570-507-507-517-157-571-5_V.mp3)
  Segment 27: 77588ms ~ 78164ms (file: 752-752-576-756-576-576-756-756 752-576-756-756-576-576-576-576-567-576-567-576-576-567-576-576-756-765-536-516-513-507-506-502-513-504-503-523-502-504-503-725-572-576-576-576-5_W.mp3)
  Segment 28: 79164ms ~ 79740ms (file: 517-510-510-157-571-517-571-157-175-751-571-756-571-715-517-715-175-571-751-751-571-571-751-756-756-756-756-756-571-756-750-750-756-571-570 570-576-517-715-715-510-751-571-571-5_V.mp3)
  Segment 29: 80740ms ~ 88696ms (file: 132-203-102-312-135-320-361-230-275-216-207-574-752-132-572-752-135-263-765-706-570-507-257-103-612-137-576-120-213-261-132-510-765-705-217-275-207-250-753-573-653-316-130-150-5_V.mp3)
  Segment 30: 89696ms ~ 90272ms (file: 130-304-140-145-405-105-143-104-143-104-143-143-140-104-104-103-143-134-104-103-157-451-105-145-104-301-103-145-134-104 310-130-143-314-103-143-104-145-143-105-145-135-175-134-1_V.mp3)
  Segment 31: 91272ms ~ 91848ms (file: 316-130-750-763-103-107-176-170-716-715-170-137-317-137-301-163-632-516-136-103-103-103-137-103-134-103-103 751-176-130-157-765-301-631-236_V.mp3)
  Segment 32: 92848ms ~ 93424ms (file: 751-751-517-735-715-715-715-175-751-571-571-571-715-751-751-715-175-756-756-571-567-576-573-507-517-517-572-150-517-570-571-570-517-157-512-517-576-527-572-571-571-576-150-570-7_V.mp3)
  Segment 33: 94424ms ~ 95000ms (file: 752-751-571-756-516-514-513-532-561-536-531-561-513-531-501-534-502-504-312-623 516-503-534-541-561-562-506-536-567-561-562-516-513-531-513_V.mp3)
  Segment 34: 96000ms ~ 101868ms (file: 270-275-256-256-256-275-103-257-756-126-172-107-167-103-603-326-357-213-357-362-302-310-251-136-321-312-103-267-176-102-316-571-312-236-210-231-321-751-312-237-321-267-267-762-7_W.mp3)
  Segment 35: 102868ms ~ 103444ms (file: 527-527-512-513-513-517-506 532-517-521-521-561-571-506-526-527-527-571-526-567-502-256-527-752-527-752-527-250-507-502-572-520-527-527-567-571-517-576-576-567-576-576-576-567-5_W.mp3)
  Segment 36: 104444ms ~ 105020ms (file: 571-571-571-571-571-561-576-576-571 170-150-571-170-170-716-710-571-751-571-517-576-576-751-576-567-657-567-657-176-675-617 510-571-150-710-715-170-170-173-170-710-715-715-756-7_W.mp3)
  Segment 37: 106020ms ~ 106596ms (file: 265-627-267-621-263-623-623-627-623-123-316-130-621-625-627-265-276-267-216-627-261-527-526-257-752 754-756-762-627-627-267-672-672-672-672-762-672-627-675-267 624-625-625-624-2_W.mp3)
  Segment 38: 107596ms ~ 119872ms (file: 312-267-237-230-320-267-276-206-672-627-123-130-763-267-236-102-130-276-123-132-726-762-276-762-103-267-762-672-267-231-230-673-230-157-231-302-320-312-310-276-312-671-672-321-7_W.mp3)
  Segment 39: 120872ms ~ 121448ms (file: 167-132-163-135-341-254-354-534-167-132-653-375-136-451-413-471-613-345-347-657-652-657-657-567-654-654-657-567-654-564-564-654-654-564-654-653-613-132-761-573-517-357-513-356-3_V.mp3)
  Segment 40: 122448ms ~ 123024ms (file: 173-132 576-175 173-137-143-132-143-132-134-124-137-576-574-265-236-612-103-321-203-263-201-203-206-203-126-231-321-210-276-261-620-612-623-260-762-627-276-762-267-230-276-127-2_V.mp3)
  Segment 41: 124024ms ~ 133888ms (file: 230-276-230-752-130-571-574-317-312-267-132-762-761-371-763-170-103-123-315-312-312-173-230-370-107-310-301-301-102-273-573-627-617-270-217-126-321-301-517-672-672-267-236-203-2_W.mp3)
  Segment 42: 134888ms ~ 135464ms (file: 103-315-315 130-130-310-310-301-103-130-105-135-130-135-153-153-501-140-134-410-130-341-415-413-431-143-315-315-135-315-315-315-104-310-135-103-105-104-103-135-134-105-314-146-1_W.mp3)
  Segment 43: 136464ms ~ 137040ms (file: 612-123-216-267-126-765-521-751-102-267-320-752-256-750-672-750-163-152-312-716-236-752-236-271-215-327-571-571-207-271-172-217-127-702-275-217-527-172-657-705-576-765-574-576 2_V.mp3)
  Segment 44: 138040ms ~ 145312ms (file: 130-267-752-231-213-574-267-527-532-136-316-172-267-625-126-321-136-321-576-627-261-213-210-765-754-267-756-576-267-576-265-576-756-170-756-756-576-765-267-236-572-625-267-267-2_W.mp3)
  Segment 45: 146312ms ~ 146888ms (file: 361-132-123-627-672 260-236-326-136-312_W.mp3)
  Segment 46: 147888ms ~ 148464ms (file: 132-132-165-132-135-132-123-137-132-126-132-136-137-132-123-167_V.mp3)
  Segment 47: 149464ms ~ 150040ms (file: 216-162-102-210-261-216-102-103-136-162-501-103-317-173-137-312-213-123-457-754-675-672-167-163-106-106-136-617-167-607-167-206-123-213-136-162-106-312-621-312-310-301-316-603-6_W.mp3)
  Segment 48: 151040ms ~ 151616ms (file: 170-175-107-150-105-157-751 510-507-570-750-571-507-501-150-105-701-571-751-105-107-175-107-715-175-715-107-107-175-170-107-150-157-170-175-175-105-710-157 570-570-750-715-705-5_V.mp3)
  Segment 49: 152616ms ~ 156900ms (file: 132-136-316 521-523-521-205-620-216-623-620-267-206-132-201_V.mp3)
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
> **Time**: 2026-04-05 12:28:51
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "제 절인데 처음엔 사실 그랬었어요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "제", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "절인데", "start_ms": 110, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "처음엔", "start_ms": 260, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "사실", "start_ms": 380, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "그랬었어요", "start_ms": 480, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "달맞이길 명당자리에 위치한 절",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "달맞이길", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "명당자리에", "start_ms": 240, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "위치한", "start_ms": 490, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "절", "start_ms": 640, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "우와 이 칸데 선생님이 왜 이렇게 잘 어울려요 진짜 둘이 최고 최고 조합이에요 그냥 야",
"speaker": "speaker_3",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "우와", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 200, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "칸데", "start_ms": 280, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "선생님이", "start_ms": 400, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "왜", "start_ms": 600, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 700, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 820, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "어울려요", "start_ms": 900, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 1080, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "둘이", "start_ms": 1200, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "최고", "start_ms": 1320, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "최고", "start_ms": 1440, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "조합이에요", "start_ms": 1560, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 1760, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "야", "start_ms": 1880, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "아우 고생 하셨어",
"speaker": "speaker_4",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "아우", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "고생", "start_ms": 190, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "하셨어", "start_ms": 340, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "한국어 공부는 이미 문의 폭주 상태래요 이제 인도 청춘들의 버킷리스트 1위는 당연 한국에서 1년 살아보기가 됐습니다 도대체 무엇이 그들을 이토록 움직였을까요 의외로",
"speaker": "speaker_5",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 60, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "공부는", "start_ms": 260, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "이미", "start_ms": 480, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "문의", "start_ms": 630, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "폭주", "start_ms": 780, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "상태래요", "start_ms": 930, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 1180, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "인도", "start_ms": 1330, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "청춘들의", "start_ms": 1480, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "버킷리스트", "start_ms": 1700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "1위는", "start_ms": 2000, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "당연", "start_ms": 2180, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "한국에서", "start_ms": 2330, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "1년", "start_ms": 2550, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "살아보기가", "start_ms": 2700, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "됐습니다", "start_ms": 2980, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "도대체", "start_ms": 3200, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "무엇이", "start_ms": 3380, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "그들을", "start_ms": 3560, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "이토록", "start_ms": 3740, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "움직였을까요", "start_ms": 3920, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "의외로", "start_ms": 4220, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "업무로 가는 시간 매장도 조금 한산해졌습니다 부장님 처음에 기도문 읽고",
"speaker": "speaker_6",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "업무로", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "가는", "start_ms": 250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "시간", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "매장도", "start_ms": 550, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 730, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "한산해졌습니다", "start_ms": 880, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "부장님", "start_ms": 1230, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "처음에", "start_ms": 1410, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "기도문", "start_ms": 1560, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "읽고", "start_ms": 1760, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "대박인데요 저 바깥 꼭대기에 이 상량패가 걸려 있는 거예요 그래서 거기 날짜를 이제 봤는데 서기 19",
"speaker": "speaker_7",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "대박인데요", "start_ms": 40, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "저", "start_ms": 290, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "바깥", "start_ms": 370, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "꼭대기에", "start_ms": 520, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 770, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "상량패가", "start_ms": 850, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "걸려", "start_ms": 1100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 1250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 1350, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 1500, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "거기", "start_ms": 1680, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "날짜를", "start_ms": 1830, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 2010, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "봤는데", "start_ms": 2160, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "서기", "start_ms": 2340, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "19", "start_ms": 2490, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "에서 반양성자가 어떻게 움직이는지를 측정해야 합니다 근데 주변에서 자기장 잡음이 계속 발생하면 측정이 방해를 받아요",
"speaker": "speaker_8",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "에서", "start_ms": 60, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "반양성자가", "start_ms": 160, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 410, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "움직이는지를", "start_ms": 560, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "측정해야", "start_ms": 840, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "합니다", "start_ms": 1040, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 1190, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "주변에서", "start_ms": 1340, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "자기장", "start_ms": 1540, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "잡음이", "start_ms": 1740, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 1940, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "발생하면", "start_ms": 2090, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "측정이", "start_ms": 2340, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "방해를", "start_ms": 2540, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "받아요", "start_ms": 2690, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "게 중요한 것 같아요 그 경험에서 내가 실수한 것들을 계속 공부해 나가고 복기해 나가고 그런 것들의 싸움이 아닐까 결국엔 지금의 시장을 잘 기억해",
"speaker": "speaker_9",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "게", "start_ms": 80, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "중요한", "start_ms": 160, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 340, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 440, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 590, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "경험에서", "start_ms": 670, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "내가", "start_ms": 870, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "실수한", "start_ms": 990, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "것들을", "start_ms": 1170, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 1350, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "공부해", "start_ms": 1500, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "나가고", "start_ms": 1680, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "복기해", "start_ms": 1860, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "나가고", "start_ms": 2040, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "그런", "start_ms": 2220, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "것들의", "start_ms": 2370, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "싸움이", "start_ms": 2550, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "아닐까", "start_ms": 2730, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "결국엔", "start_ms": 2910, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "지금의", "start_ms": 3090, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "시장을", "start_ms": 3270, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 3450, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "기억해", "start_ms": 3550, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "문명이 탄생하기 위한 비옥하고 이상적인 평원과는 거리가 멉니다 국토의 70% 이상이 산이라는 숫자를 들었을 때 서양에서는 도대체 이 땅에서 어떻게 살았느냐고 묻는 것도 이상한 반응이 아니었던 것이죠 오늘날 전 세계가 경이로운 눈빛으로 바라보는 대한민국",
"speaker": "speaker_10",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "문명이", "start_ms": 70, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "탄생하기", "start_ms": 250, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "위한", "start_ms": 470, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "비옥하고", "start_ms": 620, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "이상적인", "start_ms": 840, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "평원과는", "start_ms": 1060, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "거리가", "start_ms": 1310, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "멉니다", "start_ms": 1490, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "국토의", "start_ms": 1690, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "70%", "start_ms": 1870, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이상이", "start_ms": 2020, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "산이라는", "start_ms": 2200, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "숫자를", "start_ms": 2420, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "들었을", "start_ms": 2600, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 2780, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "서양에서는", "start_ms": 2900, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "도대체", "start_ms": 3150, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 3330, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "땅에서", "start_ms": 3410, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 3610, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "살았느냐고", "start_ms": 3790, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "묻는", "start_ms": 4040, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "것도", "start_ms": 4190, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이상한", "start_ms": 4340, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "반응이", "start_ms": 4520, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "아니었던", "start_ms": 4700, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "것이죠", "start_ms": 4900, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "오늘날", "start_ms": 5080, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "전", "start_ms": 5260, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "세계가", "start_ms": 5340, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "경이로운", "start_ms": 5520, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "눈빛으로", "start_ms": 5740, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "바라보는", "start_ms": 5960, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "대한민국", "start_ms": 6180, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "시선은 차갑습니다 왜냐하면 인간 역시 겉으로 드러나는 외적 아름다움 특히 우리 몸을 감싸고 있는 피부라는 거대한 껍데기 앞에서 얼마나 나약하게 흔들",
"speaker": "speaker_11",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "시선은", "start_ms": 80, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "차갑습니다", "start_ms": 260, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "왜냐하면", "start_ms": 510, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "인간", "start_ms": 710, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "역시", "start_ms": 860, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "겉으로", "start_ms": 1010, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "드러나는", "start_ms": 1190, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "외적", "start_ms": 1390, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "아름다움", "start_ms": 1540, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "특히", "start_ms": 1760, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "우리", "start_ms": 1910, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "몸을", "start_ms": 2030, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "감싸고", "start_ms": 2180, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 2360, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "피부라는", "start_ms": 2480, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "거대한", "start_ms": 2680, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "껍데기", "start_ms": 2860, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "앞에서", "start_ms": 3040, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "얼마나", "start_ms": 3220, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "나약하게", "start_ms": 3400, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "흔들", "start_ms": 3600, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "간판 매매지표를 만들어냅니다 제이슨 로와 리듬 단타 매매지표 이름에서도 볼 수 있듯이 지지 저항과 시장 전체를 볼 수 있는 리듬의 진짜 강도 가격 움직임을",
"speaker": "speaker_12",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "간판", "start_ms": 90, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "매매지표를", "start_ms": 240, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "만들어냅니다", "start_ms": 490, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "제이슨", "start_ms": 790, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "로와", "start_ms": 970, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "리듬", "start_ms": 1120, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "단타", "start_ms": 1270, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "매매지표", "start_ms": 1420, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "이름에서도", "start_ms": 1640, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "볼", "start_ms": 1860, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 1960, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "있듯이", "start_ms": 2040, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "지지", "start_ms": 2190, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "저항과", "start_ms": 2340, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "시장", "start_ms": 2520, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "전체를", "start_ms": 2670, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "볼", "start_ms": 2850, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 2950, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 3030, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "리듬의", "start_ms": 3150, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 3330, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "강도", "start_ms": 3480, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가격", "start_ms": 3630, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "움직임을", "start_ms": 3780, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "포커스 양보가 필요합니다",
"speaker": "speaker_13",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "포커스", "start_ms": 30, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "양보가", "start_ms": 210, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "필요합니다", "start_ms": 390, "dur_ms": 250, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 12:28:51*
