# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=10512  time=04:17:45  uptime=10397s  mem=605MB  system_processes=365"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260404_041720.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 627-567-576-570-576-576-370-675 310-136-136-163-156-165-153-132-103-301-102-361-310-106-315-132-316-316-671-670-631-601-572-376-307-302-317-130-103-310-657-652-627-602-623-672-1_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 102-612-765-675-671 674-756-160-132-601-167-162-132-312-261-236-263-321-216-261-231-263-263-267-267-263-263-236-263-236-267-263-237-237-236-273-210-627-260-260-267-260-236-231-2_W.mp3)
  Segment 3: 3152ms ~ 13160ms (file: 301-672-106-571-157-157-754-765-167-756-236-276-726-756-273-237-260-257-574-537-675-310-756-307-675-371-756-102-567-203-276-270-270-276-267-206-154-105-756-756-276-716-176-132-1_W.mp3)
  Segment 4: 14160ms ~ 14736ms (file: 257-267-627-672-267-372-256-572-526-572-576-567-567-657-625-756-756-675-756-627-756-765-576-567-567-652-675-657-657-627-756-756-765-625-675-267-675-276-627-765 163-163 136-162-6_W.mp3)
  Segment 5: 15736ms ~ 16312ms (file: 136-163-106-123-163-106-137-137-206-167-516-576-216-261-261-236-265-256-617 162-123 123 761-567-236-126-712 362-361-672-637-361-326-136-106-106-613-163-671-106-126-102-625-613-1_W.mp3)
  Segment 6: 17312ms ~ 24800ms (file: 137-672-627-627-136-612-617-162-316-612-106-216-123-163-621-610-623-137-301-160-106-263-312-130-136-123-263-167-163-123-130-123-617-163-312-126-623-103-316-613-312-132-132-162-6_V.mp3)
  Segment 7: 25800ms ~ 31056ms (file: 617-172-367-762-672-167-276-621-127-107-167-102-176-132-267-217-216-213-213-271-216-123-213-231-213-271-213-231-123-213-123-132-321-123-512-216-261-251-217-207-216-620-217-123-1_W_stt_the_plan.mp3)
  Segment 8: 32056ms ~ 32632ms (file: 302-321-103-627-267-126-627-126-105-157-135-103-507-627-162-261-302-320-327-302-326-762-670-627-326-302-301-106-163-130-165-312-631-532-561-726-621-675-736-623-627-621-230-763-3_V.mp3)
  Segment 9: 33632ms ~ 34208ms (file: 126-106-172-617-176-627-623-623-621-136-143-175-360-176-126 103-136-103-126 512-152-163-136 163-765-127-165-516-632-103-136 130-571-136-231-136-163-136-326-103-623-362-301-132-1_V.mp3)
  Segment 10: 35208ms ~ 35784ms (file: 206-260-261-621-206-312-263-216-210-236-106-260-261-620-620-621-620-126-261-126-236-312-362-260-206-216-126-162-612-612-216-126-162-610-201-162-620-162-612-160-162-102-610-106-1_W.mp3)
  Segment 11: 36784ms ~ 41644ms (file: 132-126 576-576-612-126 167-175-103 163-132-136_V.mp3)
  Segment 12: 42644ms ~ 43220ms (file: 163-132-613-517-627-623-312-301-236-302-623-261-756-765-672-675-620-321-312-260-206-203-207-367-623-260-672-167-136-136-602-627-621-630-635-362-130-106-103-260-231-362-326-623-6_W.mp3)
  Segment 13: 44220ms ~ 44796ms (file: 576 103-132-503-165-312-326-132-136-136-103-635-132-136-603-103-160-165 413-136-103-136-167-163-316-103-561-567-106-162-163-106-165-710-165-163-153-160-103-176-135-136-126-103-5_V.mp3)
  Segment 14: 45796ms ~ 46372ms (file: 672-621-716-236-623-627-621-621-612-132-106-103-162-613-316-513-216-312-130-134-136-163-130-567-176-312-163-576-167-136-135-130-132-103-136-103-136-106-167-167 135-713-136-103-1_V.mp3)
  Segment 15: 47372ms ~ 47948ms (file: 672 576-627 675-625-715-173 362-612-763-672-765-671 361_W.mp3)
  Segment 16: 48948ms ~ 49524ms (file: 312 601-675-637-367-657-637-673-621-657-671-637-613-136-637-671-673-361-312-136-631-163-576-576-375-765-367-637-635-637-652-536-657-621-276-762-765-745-157-607-173-162-163-137-2_W.mp3)
  Segment 17: 50524ms ~ 51100ms (file: 267-263-236-213-627-652-607-620-627-620-265-625-256-265-123-312-132-132-267-267-516-106-326-203-362-267-413-213-213-231-312-763-760-726-632-302-310-301-302-326-320-326-635-307-6_W.mp3)
  Segment 18: 52100ms ~ 58220ms (file: 201-201-136-160-163-312-326-310-375-716-103-132-162-132-123-273-751-312-256-517-527-351-207-271-176-327-253-132-501-301-750-572-765-120-312-267-261-752-270-315-231-527-317-573-1_V.mp3)
  Segment 19: 59220ms ~ 59796ms (file: 103-106-106-162-615-756-617-651 516-213-126-123-126-261-267-213-621-267-276-136-137-106-103-261-103-120-103-130-165-615-621-617-106-627-261-162-126-675-162-603-167-613-617-163-1_V.mp3)
  Segment 20: 60796ms ~ 61372ms (file: 302-301-362-657-762-267-102-765-270-276-301-360-310-301-361-621-203-310-310-310-103-132-652-625 276-321-573-350-301-103-132-136-163-162-136-623-627-176-137-312-302-230-230-203-3_V.mp3)
  Segment 21: 62372ms ~ 67412ms (file: 132-136-132-132-312-132-213-312-312-321-132-312-123-132-123-123-130-132-123-132-103-123-123-127-123-213-123-123-123-127-127-123-123-132-127-127-120-126-136-123-312-312-132-312-3_W.mp3)
  Segment 22: 68412ms ~ 68988ms (file: 132-635-165-136-103-102-132-136-130-376-106-136-156-627-137-170-756-576-567-657-567-657-726-657-657-506-675-760-726-721-267-236 765-217-165-613-265_W.mp3)
  Segment 23: 69988ms ~ 70564ms (file: 135-312-310-130-315 137-135-132-123-135-315-135-135-157-135-137-310-135-132-130-103-315-137-103-315-132-315-305-152 175-103-132-315-135-103-137-135-310-315-315-103-312-103-153-3_V.mp3)
  Segment 24: 71564ms ~ 72140ms (file: 201-263-236-231-320-236-320-302-321-310-312-312-320-312-132-762-317-231-576-156-567-765-673-167-126-627-310-130-375-237-523-750-312-231-321-230-312-321-362-362-310-326-321-326-6_W.mp3)
  Segment 25: 73140ms ~ 73716ms (file: 123-312-167-230-273-231-213-210-123-213-127-120-631-123-123-160-126-173-617-167-176-160_W.mp3)
  Segment 26: 74716ms ~ 75292ms (file: 302-230-267-570-135-510-162-163-162-162-107-102-132-103-312-315-105-103-310-310-153-352-321-326-632-635-637-623-632-623-375-567-752-312-260-132-123-103-312-207-721-207-236-230-3_W.mp3)
  Segment 27: 76292ms ~ 79064ms (file: 576-514-560-567-567-756-573-567-651-657-574-574-657-156-567-563-203-102-516-261-261-257-637-217-572-576-572-567-657-576-567-574-574-576-574-617-567-576-526-132-765-726-567-675-6_W.mp3)
  Segment 28: 80064ms ~ 88632ms (file: 130-751-175-137-103-657-103-167-726-267-103-163-576-260-267-236-260-623-625-756-265-576-756-725-273-267-360-265-310-230-753-752-301-260-260-754-312-362-571-321-320-310-137-137-1_W.mp3)
  Segment 29: 89632ms ~ 90208ms (file: 645-267-206-316-571-527-627-267-627-627-107-137-136-130-317-705-375-162-132-136-103-137-170-107-137-136-123-162-621-216-613-627-162-163-317-621-670-627-621-267-231-302-362-136-1_V.mp3)
  Segment 30: 91208ms ~ 91784ms (file: 261-265-261-263-231-321-136-132-316-132-135-135-316-316-326-321-231-326-236-362-106-621-162-123-162-123-513-150-512-136-637-627-263-126-123-126-126-156-512-165-325-321-320-316-1_W.mp3)
  Segment 31: 92784ms ~ 93360ms (file: 103-163-136-162-132-163-610-167-613-562-612-126-102-163-136-102-176-175-716-630-276-132 612-261-612-216 120-126-103-136-162-627-627-106-126-126-162-123-162-163-136-103-157-612-3_V.mp3)
  Segment 32: 94360ms ~ 94936ms (file: 576-574-576 632-261-623-267-265-261-726-276-762-635-632-621-263-126-612-630-301-105-375-216-602-627-623-607-627-201-210-260-263-261-236-623-256-265-213-362-327-326-703-312-362-2_W.mp3)
  Segment 33: 95936ms ~ 96512ms (file: 275-231-130-132-172-217-136-165-106-103-163-756-103-165-150-514-514-165-603-362-136-103-136-103-312-103-120-123-576-576-576-574-574 102-103-136-123-260-206-103-106-601-106-130-1_V.mp3)
  Segment 34: 97512ms ~ 98088ms (file: 265-327-263-362-376-672-675-527-172-756_W.mp3)
  Segment 35: 99088ms ~ 105352ms (file: 201-207-623-237-726-236-321-213-321-326-236-103-216-263-136-310-263-326-236-623-137-173-157-321-230-103-165-326-627-361-321-230-273-672-672-627-756-163-167-756-756-106-105-670-1_W.mp3)
  Segment 36: 106352ms ~ 106928ms (file: 103-653-170-310-130-263-620-136-132-103-103-163-316-136-130-160-670_V.mp3)
  Segment 37: 107928ms ~ 108504ms (file: 261-135-267-236-132-103-310-132-136-132-103-312-312-670-670-675-765-675-570-675-675-765-167-106-610-106-136-316-136-613-163-167-612-102-206-206-206-562-276-260-261-612-162-167-7_V.mp3)
  Segment 38: 109504ms ~ 110080ms (file: 617-675-675-675-267-672-675-672-567-675-675-675-675-765-576-762-672-526-527-265-236-261-765-617-675-756-657-762-302-367-675-672-627-675-573-132-523-725-672-310-103-675-672-136 2_W.mp3)
  Segment 39: 111080ms ~ 111656ms (file: 136-312-517-157-510-135-163-361-263-263 320-326-137-126-231-312-152-321-135-623-362-132-132-132-163-163-321-265-136-312-632-132-362-326-321-362-321-236-263-325-321-326-635-673-2_W.mp3)
  Segment 40: 112656ms ~ 123636ms (file: 756-276-127-267-326-573-756-230-136-263-120-135-105-123-170-167-130-127-632-361-312-371-312-321-371-102-132-213-321-672-261-267-627-276-217-261-162-126-176-267-175-576-315-163-1_W.mp3)
  Segment 41: 124636ms ~ 132412ms (file: 126-265-213-236-123-106-260-120-120-361-163-312-132-215-236-120-162-126-231-263-236-260-261-601-126-261-267-261-261-132-137-130-120-102-321-273-123-203-201-203-107-153-126-260-1_W.mp3)
  Segment 42: 133412ms ~ 133988ms (file: 617-760-310-371-137-163-137-136-137-167-163-617-317-316-317-371-216-316-267-612-671-627-672-607-627-671-136-130-103-132-132-351-106-153-137-672-312 213-236-362-341 163-126-312-1_V.mp3)
  Segment 43: 134988ms ~ 135564ms (file: 312-631-641-241-130-310-103-301-132-136-312-123-130-213-132-132-103-312-123-301-312-263-206-206-620-602-607-602-162-506-503-325-302-103-310-132-103-621-136-362-130-103-103-103-1_V.mp3)
  Segment 44: 136564ms ~ 137140ms (file: 102-213-321 123-132-312-103-102-103-312-301-126-106-106-103-106-163-213-136-103-126-120-301-130-321-130-137-312-123-576-576-132-312 310-130-306-301-136-130-103-375-621-675-673-1_V.mp3)
  Segment 45: 138140ms ~ 138716ms (file: 541-136-632-631-620-267-103-153-130-261-156-206-627-615-617-105-563-130-103-150-167-106-103-103-130-132-351-315-136-103-103-701-165-132-130 627-267-675-206-136-627-163-136-567-1_V.mp3)
  Segment 46: 139716ms ~ 140292ms (file: 156-567-576-517-576-263-627-765-761-175-576-675-675-617-672-762-675-672-673-632-576-716-316-673-267-632-637-657-617-162-126-312-312-631-572-576-752-652-361-236-621-623-613-672-6_W.mp3)
  Segment 47: 141292ms ~ 141868ms (file: 106-106 765-765-167-163-765-760-675-132-153-153-163-106-310-301-103-765-765-137-301-763-103-135-123-102-103-132-362-206-260-620-260-236-216-263-130-106-310-132-210-312-306-320-3_V.mp3)
  Segment 48: 142868ms ~ 143444ms (file: 765-163-132 261-236-231-103-130-260-103-130-321-301-136-317-312-753-163-415-623-172-621-263-231 261 136-132-134-103 310-132-716-751 632-302-302-637-103-103-627-263-675-675-675-6_V.mp3)
  Segment 49: 144444ms ~ 145020ms (file: 106-126-126-163-106-106-163-126-106-126-123_V.mp3)
  Segment 50: 146020ms ~ 146596ms (file: 657-257-657-576-756-675-761-756-675-276-265-573-216-756-721-571-357_W.mp3)
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
> **Time**: 2026-04-04 04:18:38
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "미국이 과거에 매달리는 동안 미국과 중국은 세상을 바꾸는 기업을 세우고 매출과 일자리를 폭발시키고 있습니다 박사 학위의 가치가 이제 월 3만 원짜리",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "미국이", "start_ms": 20, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "과거에", "start_ms": 450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "매달리는", "start_ms": 930, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "동안", "start_ms": 1450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "미국과", "start_ms": 1800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "중국은", "start_ms": 2250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "세상을", "start_ms": 2750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "바꾸는", "start_ms": 3200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "기업을", "start_ms": 3650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "세우고", "start_ms": 4100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "매출과", "start_ms": 4600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "일자리를", "start_ms": 5100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "폭발시키고", "start_ms": 5650, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 6400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "박사", "start_ms": 6900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "학위의", "start_ms": 7300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "가치가", "start_ms": 7750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 8200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "월", "start_ms": 8550, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "3만", "start_ms": 8800, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "원짜리", "start_ms": 9150, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "생각을 늘 하였고 꾸준한 수익을 낼 수 있는 가능성이 있지 않을까 생각하여 신청하게 되었는데 정말",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "생각을", "start_ms": 40, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "늘", "start_ms": 480, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "하였고", "start_ms": 720, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "꾸준한", "start_ms": 1250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "수익을", "start_ms": 1750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "낼", "start_ms": 2200, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 2450, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 2650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "가능성이", "start_ms": 3000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "있지", "start_ms": 3550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "않을까", "start_ms": 3900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "생각하여", "start_ms": 4350, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "신청하게", "start_ms": 4950, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "되었는데", "start_ms": 5500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 6050, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "표현되는 게 그렇게 많지가 않거든요 진짜 많이 준비를 해오세요 신동엽 씨는 진짜",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "표현되는", "start_ms": 30, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "게", "start_ms": 550, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "그렇게", "start_ms": 750, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "많지가", "start_ms": 1200, "dur_ms": 450, "phase_lr_deg": -5.0},
{"word": "않거든요", "start_ms": 1700, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "진짜", "start_ms": 2250, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "많이", "start_ms": 2700, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "준비를", "start_ms": 3100, "dur_ms": 450, "phase_lr_deg": -5.0},
{"word": "해오세요", "start_ms": 3600, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "신동엽", "start_ms": 4150, "dur_ms": 450, "phase_lr_deg": -5.0},
{"word": "씨는", "start_ms": 4650, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "진짜", "start_ms": 5000, "dur_ms": 400, "phase_lr_deg": -5.0}
]
},
{
"segment": 4,
"transcript": "어느 정도 수준의 전투기일까요 한국 공군의 위치 설정은 매우 분명합니다 이 기체는 F-16과 F-35 사이에 놓인 4.5세대 전투기 혹은 준 5세대 전투기로 불립니다 절대적인 스텔스 성능과",
"speaker": "speaker_4",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "어느", "start_ms": 50, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "정도", "start_ms": 380, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "수준의", "start_ms": 720, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "전투기일까요", "start_ms": 1150, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 1900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "공군의", "start_ms": 2350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "위치", "start_ms": 2800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "설정은", "start_ms": 3200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "매우", "start_ms": 3700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "분명합니다", "start_ms": 4050, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 4750, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "기체는", "start_ms": 4950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "F-16과", "start_ms": 5450, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "F-35", "start_ms": 6100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "사이에", "start_ms": 6750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "놓인", "start_ms": 7200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "4.5세대", "start_ms": 7600, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "전투기", "start_ms": 8300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "혹은", "start_ms": 8800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "준", "start_ms": 9200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "5세대", "start_ms": 9400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "전투기로", "start_ms": 9900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "불립니다", "start_ms": 10450, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "절대적인", "start_ms": 11050, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "스텔스", "start_ms": 11650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "성능과", "start_ms": 12150, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "출금해 네가 6,000으로도 2억 넘게 벌었는데 더 하면 뭐 하냐 그리고 너 같은 스타일은 미수 풀이라서 무조건 출금해야 돼 출금해서 통장",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "출금해", "start_ms": 25, "dur_ms": 450, "phase_lr_deg": 10.0},
{"word": "네가", "start_ms": 550, "dur_ms": 300, "phase_lr_deg": 10.0},
{"word": "6,000으로도", "start_ms": 900, "dur_ms": 700, "phase_lr_deg": 10.0},
{"word": "2억", "start_ms": 1650, "dur_ms": 300, "phase_lr_deg": 10.0},
{"word": "넘게", "start_ms": 2000, "dur_ms": 350, "phase_lr_deg": 10.0},
{"word": "벌었는데", "start_ms": 2400, "dur_ms": 550, "phase_lr_deg": 10.0},
{"word": "더", "start_ms": 3000, "dur_ms": 150, "phase_lr_deg": 10.0},
{"word": "하면", "start_ms": 3200, "dur_ms": 300, "phase_lr_deg": 10.0},
{"word": "뭐", "start_ms": 3550, "dur_ms": 150, "phase_lr_deg": 10.0},
{"word": "하냐", "start_ms": 3750, "dur_ms": 350, "phase_lr_deg": 10.0},
{"word": "그리고", "start_ms": 4150, "dur_ms": 400, "phase_lr_deg": 10.0},
{"word": "너", "start_ms": 4600, "dur_ms": 150, "phase_lr_deg": 10.0},
{"word": "같은", "start_ms": 4800, "dur_ms": 350, "phase_lr_deg": 10.0},
{"word": "스타일은", "start_ms": 5200, "dur_ms": 500, "phase_lr_deg": 10.0},
{"word": "미수", "start_ms": 5750, "dur_ms": 350, "phase_lr_deg": 10.0},
{"word": "풀이라서", "start_ms": 6150, "dur_ms": 550, "phase_lr_deg": 10.0},
{"word": "무조건", "start_ms": 6750, "dur_ms": 450, "phase_lr_deg": 10.0},
{"word": "출금해야", "start_ms": 7250, "dur_ms": 550, "phase_lr_deg": 10.0},
{"word": "돼", "start_ms": 7850, "dur_ms": 250, "phase_lr_deg": 10.0},
{"word": "출금해서", "start_ms": 8150, "dur_ms": 550, "phase_lr_deg": 10.0},
{"word": "통장", "start_ms": 8750, "dur_ms": 400, "phase_lr_deg": 10.0}
]
},
{
"segment": 6,
"transcript": "each other We completely missed each other at the airport We completely missed each other at the airport",
"speaker": "speaker_6",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "each", "start_ms": 120, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "other", "start_ms": 400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "We", "start_ms": 750, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "completely", "start_ms": 1000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "missed", "start_ms": 1650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "each", "start_ms": 2100, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "other", "start_ms": 2400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "at", "start_ms": 2750, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 2950, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "airport", "start_ms": 3150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "We", "start_ms": 4500, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "completely", "start_ms": 4750, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "missed", "start_ms": 5400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "each", "start_ms": 5850, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "other", "start_ms": 6150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "at", "start_ms": 6500, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 6700, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "airport", "start_ms": 6900, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "한국항공우주산업의 대형 공장은 축구장 세 개에 맞먹는 규모를 자랑하며 KF-21과 FA-50 그리고 T-50 등의 기종을 동시에 전력 생산하고 있습니다 생산 능력은 놀라울 정도입니다 이들은 올해 27대의 항공기를 인도할 계획이며 내년",
"speaker": "speaker_7",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "한국항공우주산업의", "start_ms": 50, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "대형", "start_ms": 1100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "공장은", "start_ms": 1550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "축구장", "start_ms": 2100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "세", "start_ms": 2600, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "개에", "start_ms": 2850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "맞먹는", "start_ms": 3200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "규모를", "start_ms": 3700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "자랑하며", "start_ms": 4200, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "KF-21과", "start_ms": 4800, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "FA-50", "start_ms": 5550, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 6200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "T-50", "start_ms": 6700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "등의", "start_ms": 7250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "기종을", "start_ms": 7600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "동시에", "start_ms": 8100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "전력", "start_ms": 8600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "생산하고", "start_ms": 9000, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 9600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "생산", "start_ms": 10100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "능력은", "start_ms": 10550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "놀라울", "start_ms": 11050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "정도입니다", "start_ms": 11500, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "이들은", "start_ms": 12150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "올해", "start_ms": 12650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "27대의", "start_ms": 13050, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "항공기를", "start_ms": 13650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "인도할", "start_ms": 14200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "계획이며", "start_ms": 14700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "내년", "start_ms": 15250, "dur_ms": 350, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-04 04:18:38*
