# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=28596  time=07:25:02  uptime=12785s  mem=405MB  system_processes=408"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_072429.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 9324ms (file: 230-310-230-321-103-132-132-163-103-106-106-601-102-126-310-231-231-123-103-102-203-206-574-754-206-106-321-213-102-320-263-261-213-126-210-231-231-102-321-132-106-106-102-231-1_V.mp3)
  Segment 2: 10324ms ~ 17488ms (file: 276-752-625-270-320-260-156-156-260-540-320-457-256-657-276-107-267-756-203-310-210-201-720-260-326-326-302-203-541-540-103-103-103-541-510-154-267-260-726-106-317-301-103-120-3_V.mp3)
  Segment 3: 18488ms ~ 19064ms (file: 756-715-751-751-170-157-175-756-750-756-750-750-751-756-713-750-751-750-576 103-105-510-751-130-170-570-135-751-701-715-756-751-753-751 517-571-751-175-517-175-710-713-751-710-1_V.mp3)
  Segment 4: 20064ms ~ 35148ms (file: 312-732-317-173-172-716-172-107-107-710-751-725-107-173-750-107-716-750-705-173-175-132-310-760-716-107-753-103-750-723-612-375-723-107-107-725-730-710-270-703-713-671-103-103-1_W.mp3)
  Segment 5: 36148ms ~ 36724ms (file: 570-617-765-173-765-163-175-137-231-126_W.mp3)
  Segment 6: 37724ms ~ 44312ms (file: 316-572-571-756-257-210-570-572-103-527-572-132-570-756-275-526-257-672-276-762-267-547-527-251-263-203-312-253-326-263-235-217-312-574-623-152-502-261-276-206-267-263-527-102-1_V.mp3)
  Segment 7: 45312ms ~ 51000ms (file: 621-316-562-167-137-726-576-706-267-267-267-267-267-206-273-127-206-672-312-321-260-520-137-106-312-265-265-321-362-571-153-172-127-163-721-602-273-627-726-106-751-317-207-237-2_V.mp3)
  Segment 8: 52000ms ~ 58948ms (file: 103-103-317-621-276-271-756-106-621-267-107-107-756-127-301-760-176-170-617-216-275-106-175-706-175-207-621-706-623-235-175-267-765-107-712-756-570-172-162-107-750-237-765-576-2_W.mp3)
  Segment 9: 59948ms ~ 60524ms (file: 321-316 652-657-675 267-261-326-632-576-302-236-316-130-326-236-326-326-367-637-670-230-302-130-673-765-567-675-675-361-352-376-235-315-130-362-321-352-317-326-321-753-756-576-5_W.mp3)
  Segment 10: 61524ms ~ 62100ms (file: 560-326-275-527-213-721-271-326-532 356-675-673-756-175-635-527-752-216-570-123-762-215-613-475-362-562-637-273-726-572-632-705-213-507-753-547-135-301-370-326-137-572-276-672-7_V.mp3)
  Segment 11: 63100ms ~ 63676ms (file: 762-762-762-761-763-762-672-625-702-312-132-231-321_W.mp3)
  Segment 12: 64676ms ~ 74144ms (file: 130-751-621-263-765-103-167-176-236-103-230-126-723-756-103-267-263-263-120-267-752-162-261-750-672-102-231-132-132-132-765-102-627-267-132-143-173-260-260-725-675-132-236-263-2_W.mp3)
  Segment 13: 75144ms ~ 75720ms (file: 317 357-756-517-576-576-312-312-132-165-517-163-657-526-625-531-513-352-573-617-675-365-315-537-751-675-761-136-236-326-125-615-365-356-167-132-514-125-731-615-316-163-165-562-1_W.mp3)
  Segment 14: 76720ms ~ 77296ms (file: 651-625-372-765-256-576-136-507-362-613-327-672-617-123-312-362-213-672-267_V.mp3)
  Segment 15: 78296ms ~ 81680ms (file: 753-735-756-576-735 673 731-175-756-576-573-573-357-756-657-576-576-756-567-316-371-317-315-317 362-736-261-201-726-627-320-253-315-172-751-751-750-751-106-130-132-317-137-263-1_W.mp3)
  Segment 16: 82680ms ~ 83256ms (file: 572-367-205-275-621-615-670-175-576-570-213-351-573-750-753-756-756-176-375-731-175-135-153-573-573-573-135-157-571-571-361-316-361-631-361-361-316-316-136-136-137-637-361-613-6_W.mp3)
  Segment 17: 84256ms ~ 84832ms (file: 312-132-312-103-103-102-103-103-102-107-137 576-612-162-132-167-167-176-163-176-127-167-173-176-173-716-671-376-570-657-756-756-756-675-762-267-650-756-762-162-136-132-163-163-1_W.mp3)
  Segment 18: 85832ms ~ 93536ms (file: 736-273-203-201-130-201-103-310-130-750-510-267-273-170-102-103-102-752-527-276-216-103-231-276-207-201-213-103-102-253-267-230-103-321-207-547-572-257-752-217-627-762-160-607-3_W.mp3)
  Segment 19: 94536ms ~ 100764ms (file: 206-312-627-152-132-275-570-265-167-163-126-315-627-206-352-267-620-301-762-236-175-750-203-273-312-102-213-572-125-206-123-356-321-132-503-736-231-751-312-126-302-513-371-316-3_V.mp3)
  Segment 20: 101764ms ~ 108496ms (file: 167-367-321-236-213-203-302-756-765-756-127-132-237-267-263-267-127-312-260-172-102-107-103-261-263-312-130-574-176-135-207-726-267-657-106-765-367-375-726-756-276-127-716-217-7_W.mp3)
  Segment 21: 109496ms ~ 110072ms (file: 310-362-316-312-312-136-130-731-375-730-376-317-721-312 713-376-317-370-307-263-206-350-761-657-167-175-120-132-572-672-210-172-162-136 745-327-203-236-562-367-312-523-273-576-5_W.mp3)
  Segment 22: 111072ms ~ 111648ms (file: 176-175-176-175 103-173-157-751-571-175-175-173-175-176-175-176-176-157-176-137 130-150-153-157-175-175-165-176-175-517-713-167-175-175-175-617-176-176-176-176-176-176-176-175 1_W.mp3)
  Segment 23: 112648ms ~ 113224ms (file: 537-365-672-215-327-130-312-632-302-312-267-326-536-206-216-627-326-261-321-263-607-623-276-265-526-627-573-627-675-607-507-572-620-672-750-576-567_V.mp3)
  Segment 24: 114224ms ~ 114800ms (file: 157-276-756-257-627-765-213-267-357-675-517-312-367-265-765-326-756-652-752-203-236-526-675 675-325-752-237-231-231-132-263 231-217-723-257-273-576-276-362-501-253-327-753-375-7_V.mp3)
  Segment 25: 115800ms ~ 116376ms (file: 134 156-625-763-673-132-367-351-362-326-321_W.mp3)
  Segment 26: 117376ms ~ 117952ms (file: 136-361-316-361-316-621-231-765-671-763-162-672-675-127-274-312-371-236-316-365-123-136-132-132-123-164-615-312-314-135-314-143-136-163-132-135-143-652-765-762-736-345-635-374-1_V.mp3)
  Segment 27: 118952ms ~ 119528ms (file: 326-216-270-326-203-613-621-217-612-761-712-163-570-321-675-752-263-765-623-267-327-271-567-326-756-756-367-765-570-326-726-627-627-576-567-725-732-756-627-506-364-573-216-502-7_V.mp3)
  Segment 28: 120528ms ~ 128160ms (file: 120-210-206-206-260-236-237-261-236-235-267-265-102-321-260-230-260-103-316-216-206-102-123-203-301-263-132-103-312-130-312-302-310-621-213-216-123-103-103-261-320-132-203-612-2_W.mp3)
  Segment 29: 129160ms ~ 129736ms (file: 162-160-107-175-105-157-701-170-721-710-127-123-321-132-132-132-312-123-132-312-132-130-132-123-132-175-710-571-751-751-703-705-107-750-750-701-750-750-751-750-705-750-750-751-7_V.mp3)
  Segment 30: 130736ms ~ 136748ms (file: 210-126-132-731-217-217-712-731-175-261-257-321-130-130-123-231-276-325-275-265-230-120-321-236-123-126-320-312-123-321-103-132-125-215-310-621-631-132-163-156-132-120-120-126-2_W.mp3)
  Segment 31: 137748ms ~ 146172ms (file: 752-103-321-753-106-310-725-130-102-312-320-310-230-230-750-270-210-267-756-275-725-576-210-201-321-103-627-120-236-123-206-257-207-231-163-752-213-231-201-675-725-132-127-120-7_W.mp3)
  Segment 32: 147172ms ~ 155380ms (file: 675-126-765-765-726-276-702-765-106-716-654-617-607-576-276-276-273-173-103-702-102-276-627-765-267-765-267-207-756-273-267-756-105-215-167-267-106-176-627-267-237-276-260-123-1_W.mp3)
  Segment 33: 156380ms ~ 162068ms (file: 375-236-267-753-267-256-752-267-176-132-731-160-230-260-576-107-167-576-167-302-230-237-102-612-371-350-317-107-571-230-260-106-317-237-732-167-617-573-106-302-750-302-260-752-2_W.mp3)
  Segment 34: 163068ms ~ 169872ms (file: 162-156-675-263-273-126-210-763-560-135-310-123-206-310-301-730-623-756-207-210-206-321-526-206-267-276-130-263-720-750-210-256-267-260-213-504-231-326-265-576-503-576-267-237-2_W.mp3)
  Segment 35: 170872ms ~ 171448ms (file: 570-571-517-570 573-573-571-751-570-507-750-750-175-571-570-573-571-572-570-517-570-572-517 573-756-750-570-570-750-570-570-570-750-570 751-175-751-157-570-751-517-715-751-750-5_W.mp3)
  Segment 36: 172448ms ~ 173024ms (file: 576-267-631-562-672-273-260-127-571-271-756-267 261-527_W.mp3)
  Segment 37: 174024ms ~ 174600ms (file: 657-637-673-637-631-613-631-361-136-631-361-613-361-376-163-163-671-165-657-516-751-574-574-651-637-365-613-631-163-731-173-715-157-153-173-175 547_W.mp3)
  Segment 38: 175600ms ~ 180964ms (file: 321-312-230-263-267-263-320-231-130-132-132-321-726-267-302-321-326-321-267-320-302-321-376-367-732-631-706-327-237-103-260-321-231-154-273-317-607-310-315-756-573-237-307-672-7_W.mp3)
  Segment 39: 181964ms ~ 190352ms (file: 265-627-510-526-257-163-236-162-276-316-310-312-136-106-176-310-107-103-672-672-267-572-653-715-103-736-261-267-573-532-102-150-361-671-231-361-163-765-627-627-132-573-726-310-2_W.mp3)
  Segment 40: 191352ms ~ 196572ms (file: 162-213-672-627-120-216-210-621-102-261-236-236-372-217-236-321-367-376-103-756-751-576-627-261-123-136-761-673-765-637-735-315-132-172-132-371-130-310-312-327-321-375-312-132-1_W.mp3)
  Segment 41: 197572ms ~ 198148ms (file: 715 701-157-715-501-175-571-715-710-710-170-715-710-173-175-715-716-175-716-175-715-176-760-715-761-715-751 170-150-107-705-751-710-170-751-710-175-170-715-170-175-170-175-715-1_V.mp3)
  Segment 42: 199148ms ~ 199724ms (file: 250-132-756-276-236-752-705-621-576-132-675-637-527-572-167-251-620-352-157-312-576-351-603-527-236-627-652-273-762-520-132-612-527-526-257-570-721-765-376-631-176-361-671-163-6_W.mp3)
  Segment 43: 200724ms ~ 201300ms (file: 765-316-132-712-130-130-123-312-316-105-310-752-576-273-257-317-576-157-125-762-237-132-701-137-132-172-625-213-321-153-657-317 126-261-321-715-573-312-231-135-261-725-213-231-6_V.mp3)
  Segment 44: 202300ms ~ 202876ms (file: 624-136-316-765-132-316-621-657-167-613-163-132-312-167-162-571-157-135 573-312-317-361-351-576-753-576-576-567-576-576-756-702-652-123-672-317-637-607-576-710-173-371-713-761-7_W.mp3)
  Segment 45: 203876ms ~ 204452ms (file: 213-675-726-327-506-507-317-672-571-325-627-267-572-362-572-507-132-607-632-236-762-376-765-763-631-167-617-167-175-136-517-316-136-516-156-321-623-517-316-316-362-310-312-137-1_W.mp3)
  Segment 46: 205452ms ~ 206028ms (file: 150-514-153-103-135-135-137-176-765-576-160-175-570-107-756-716-137-157-312-257-721-316-176-123-175-165-137-126-765-716-721-312-702-316-751-267-126-623-621-571-153-157-570-571 2_V.mp3)
  Segment 47: 207028ms ~ 207604ms (file: 576-576-576-352-301-132-275-356-357-567-576-576-576-613-267-275-721-657-567-657-637-576-756-567-576-356-307-576-260-162-763-756-574-756-756-726-756-735-657-321-607-657-756-574-5_W.mp3)
  Segment 48: 208604ms ~ 209180ms (file: 502-576-752-570-327-231-572-265-325-725-236-257-725-273-261-576-205-672-567-573-276-507-672-752-276-762-235-127-506-276-751-736-312-375-132-547-132-527-253-705-251-627-527-572-2_V.mp3)
  Segment 49: 210180ms ~ 210756ms (file: 673-761-675-651-160-731-625-701-675-673-627 354 652-672-576-652-652-617-130-751-651-652-137-612-617-620-675-562 265-231-315-315_W.mp3)
  Segment 50: 211756ms ~ 217228ms (file: 276-102-260-320-312-103-206-276-602-216-207-267-725-621-207-312-137-731-206-541-574-253-217-260-215-102-120-260-201-751-607-201-216-301-175-317-632-260-132-153-135-305-716-350-5_V.mp3)
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
> **Time**: 2026-04-06 07:26:21
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "이년 걸렸습니다 와 어 정말요 판넬도 이게 쉽지가 않은데",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "이년", "start_ms": 20, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "걸렸습니다", "start_ms": 450, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "와", "start_ms": 1100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "어", "start_ms": 1350, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "정말요", "start_ms": 1550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "판넬도", "start_ms": 2050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 2500, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "쉽지가", "start_ms": 2800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "않은데", "start_ms": 3300, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "그 유 선생님한테 많이 배웠습니다 어 유요 아 그분 저도 배워요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "그", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "유", "start_ms": 250, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "선생님한테", "start_ms": 500, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 1150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "배웠습니다", "start_ms": 1500, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "어", "start_ms": 2150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "유요", "start_ms": 2350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 2750, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "그분", "start_ms": 2950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "저도", "start_ms": 3300, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "배워요", "start_ms": 3600, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "디지털 유목민의 성지 인도네시아 발리는 한발 앞서 멈춰온 미래를 경험하고 있다",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "디지털", "start_ms": 30, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "유목민의", "start_ms": 500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "성지", "start_ms": 1050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "인도네시아", "start_ms": 1450, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "발리는", "start_ms": 2200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "한발", "start_ms": 2700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "앞서", "start_ms": 3100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "멈춰온", "start_ms": 3450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "미래를", "start_ms": 3950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "경험하고", "start_ms": 4400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "있다", "start_ms": 4950, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "내가 아이디어 떠올랐을 때 바로 한번 해 봐 되니 이것만 물어보면 되는 거죠 뭐 이렇게 하면 어떨까의 물음표였다면 이제는 이거 한번 해 봐 해 보자 가 되는 거 같아요",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "내가", "start_ms": 50, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "아이디어", "start_ms": 350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "떠올랐을", "start_ms": 900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 1450, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 1650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "한번", "start_ms": 2000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "해", "start_ms": 2350, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "봐", "start_ms": 2550, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "되니", "start_ms": 2800, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이것만", "start_ms": 3150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "물어보면", "start_ms": 3600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "되는", "start_ms": 4100, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "거죠", "start_ms": 4400, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "가상 실험에서 시스템을 교체하려고 하자 AI는 날 없애지 말라고",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "가상", "start_ms": 40, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "실험에서", "start_ms": 450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "시스템을", "start_ms": 1000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "교체하려고", "start_ms": 1500, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "하자", "start_ms": 2100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "AI는", "start_ms": 2450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "날", "start_ms": 2900, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "없애지", "start_ms": 3100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "말라고", "start_ms": 3550, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "또다시 리그 최강의 흥부 듀오를 활성화시킨 산토스 감독은 인정받을 만했는데요 말도 안 되는 득",
"speaker": "speaker_6",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "또다시", "start_ms": 20, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "리그", "start_ms": 450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "최강의", "start_ms": 800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "흥부", "start_ms": 1300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "듀오를", "start_ms": 1650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "활성화시킨", "start_ms": 2100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "산토스", "start_ms": 2750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "감독은", "start_ms": 3200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "인정받을", "start_ms": 3650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "만했는데요", "start_ms": 4200, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "모즈타바 하메네이가 건재하다는 보도도 나왔습니다 러시아 국영 매체는 모즈타바가 러시아에 몇 시간도 체류한 적이 없고 부상도 심",
"speaker": "speaker_7",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "모즈타바", "start_ms": 30, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "하메네이가", "start_ms": 550, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "건재하다는", "start_ms": 1200, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "보도도", "start_ms": 1800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "나왔습니다", "start_ms": 2250, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "러시아", "start_ms": 2900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "국영", "start_ms": 3350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "매체는", "start_ms": 3750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "모즈타바가", "start_ms": 4200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "러시아에", "start_ms": 4850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "몇", "start_ms": 5400, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "시간도", "start_ms": 5650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "체류한", "start_ms": 6100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "적이", "start_ms": 6550, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "없고", "start_ms": 6850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "부상도", "start_ms": 7250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "심", "start_ms": 7700, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "유사한 로봇을 꼭 개발해야 된다는 거죠 한국에서 생산할 수 있는 방식으로 여기서 기술적인 차별화는 더해야 되겠죠 만약 이 부분이 미국하고 협의될 수 있는 부분인가도 고민을 해야 될 것 같고 이런 고민들이 어느 정도",
"speaker": "speaker_8",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "유사한", "start_ms": 20, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "로봇을", "start_ms": 450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "꼭", "start_ms": 900, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "개발해야", "start_ms": 1150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "된다는", "start_ms": 1700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "거죠", "start_ms": 2150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "한국에서", "start_ms": 2500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "생산할", "start_ms": 3050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 3550, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 3750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "방식으로", "start_ms": 4100, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "Nice to meet you. 반갑습니다. Welcome. Nice to meet you. I'm John.",
"speaker": "speaker_9",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "Nice", "start_ms": 100, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "meet", "start_ms": 600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "you.", "start_ms": 950, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "다른 사람도 아닌 프랑스 사람들이 나서서 중국인들이 운영하는 한식당을 적발하고 렛데루를 달아놓는 서비스를 만들었다고 하는데요 시작은 중국인이 운영한 한식당에서 찍힌 몰래카메라",
"speaker": "speaker_10",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "다른", "start_ms": 50, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "사람도", "start_ms": 400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아닌", "start_ms": 850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "프랑스", "start_ms": 1200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "사람들이", "start_ms": 1700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "나서서", "start_ms": 2250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "중국인들이", "start_ms": 2700, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "운영하는", "start_ms": 3350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "한식당을", "start_ms": 3900, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "적발하고", "start_ms": 4500, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "렛데루를", "start_ms": 5100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "달아놓는", "start_ms": 5650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "서비스를", "start_ms": 6200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "만들었다고", "start_ms": 6750, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "하는데요", "start_ms": 7450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "시작은", "start_ms": 8000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "중국인이", "start_ms": 8500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "운영한", "start_ms": 9050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "한식당에서", "start_ms": 9550, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "찍힌", "start_ms": 10250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "몰래카메라", "start_ms": 10650, "dur_ms": 700, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 07:26:21*
