# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=30288  time=04:20:02  uptime=10846s  mem=744MB  system_processes=368"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260404_041837.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 9360ms (file: 754-547-167-130-231-261-230-103-756-321-317-317-130-103-136-621-106-136-571-562-321-103-361-163-310-620-263-106-126-160-613-615-160-625-613-136-123-216-261-260-162-163-312-316-1_W.mp3)
  Segment 2: 10360ms ~ 17164ms (file: 265-263-612-625-265-627-210-561-106-120-263-231-562-120-102-210-126-621-216-103-102-165-320-102-320-316-573-532-203-230-263-120-103-230-725-173-123-625-625-265-265-120-210-256-2_W.mp3)
  Segment 3: 18164ms ~ 18740ms (file: 672-103-216-621-621-126-216-123 201 765-712-136-327-163-167-675-756-765-675-765-657-576-657-675_W.mp3)
  Segment 4: 19740ms ~ 20316ms (file: 617-176-157-153-761-623-756-315 235-123-321-231-751-153-235_W.mp3)
  Segment 5: 21316ms ~ 32620ms (file: 762-672-730-261-236-276-627-736-762-267-726-276-726-327-276-237-732-267-327-103-236-723-107-617-617-167-132-321-756-576-167-167-267-170-137-127-106-130-725-217-173-321-312-371-1_W.mp3)
  Segment 6: 33620ms ~ 34196ms (file: 627-567-576-567-576-751-713 310-123-312-312-263-326-326-236-321-260-263_W.mp3)
  Segment 7: 35196ms ~ 35772ms (file: 326-263-123-362-326-362-361 236-632-261-265-236-261-261-657-675-617-263-267-263-631-126-625-263-236-263-627-267-267-567-657-657-675-632-263-265-263-265-263_W.mp3)
  Segment 8: 36772ms ~ 37348ms (file: 126-132-126-132-571-547-547-574-671-672-567-675-670-627-756-301-103-261-162-621-103-163-136-103-670-602-602-136-630-306-103-512-261-236-263-162-625-316-302-657-615-165-612-312-3_W.mp3)
  Segment 9: 38348ms ~ 38924ms (file: 675-657-657-765-756-765-576-725-726-276-762-276-176-627-126_W.mp3)
  Segment 10: 39924ms ~ 40500ms (file: 236-123-103-301-103-302-203-326-207-206-203-260-206-370-763-675-672-130-310-120-260-312-215-136-312-321-312-654-657-567-576-574-756-765-706-132-320-130-103-310-310-103-103-671-3_V.mp3)
  Segment 11: 41500ms ~ 42076ms (file: 136-621-132-316-136-126-316-136-135-623-210-132-627-657-136-312-137-103-756-136-136-163-163_V.mp3)
  Segment 12: 43076ms ~ 49088ms (file: 236-312-750-276-231-672-276-376-372-370-176-617-627-261-326-263-267-267-671-672-176-321-137-103-371-731-723-137-763-107-170-271-216-720-731-170-675-273-260-261-627-267-762-362-3_W_stt_the.mp3)
  Segment 13: 50088ms ~ 50664ms (file: 613-316-170-137-103-102-653-567-132-231-163-571-236-326-567-657-657-637-362-603-706-376-671-130-120-306-301-310-621-631_W.mp3)
  Segment 14: 51664ms ~ 52240ms (file: 756-726-765-267-627-625-627-627-672-675-657-672-623-326-261-621-623-263-623 576-572-756-675-756-576-567-627-627-675-576-672-672-675-617-673-675-567-675-752-657-627-762-672-762-2_W.mp3)
  Segment 15: 53240ms ~ 53816ms (file: 103-107-215-103-136-156-132-751-216-236_V.mp3)
  Segment 16: 54816ms ~ 55392ms (file: 576-756-657-567-574-574 157-162-120-136-162-136 163-167-617 316-361-316-320-261-236-267-236-267-753-735-731-172-715-376-173-176-756-751-620-623-206-260-203-261-216-675-673-576-5_W.mp3)
  Segment 17: 56392ms ~ 56968ms (file: 126-621-621-617-621-263-216-261-265-274-163-267-236-215-327-713-371-354-264-362-316-632-621-126-312-132-312-316-132-326-216-213-216-126-236-261-316-213-214-213-312-213-261_V.mp3)
  Segment 18: 57968ms ~ 58544ms (file: 625-613-160-163-136-312-231-236-627-263-263-236-236-672-267-263-263-362-123-267-267-627-267-316-675-321 621-623-762-367-673-637-163-672-627-615-612-715-257-176-206-123-613-321-2_W.mp3)
  Segment 19: 59544ms ~ 60120ms (file: 326-267-267-267-236-265-213-326-361-326-326-263-267-267-267-267-236-756-527-267-256-257-256-267-267-623-312-326-627-362-263-612-564-574-562-267-267-627-627-627-265-625-263-236-2_W.mp3)
  Segment 20: 61120ms ~ 61696ms (file: 103-621-762-137-163-136-130 365-675-607-103-631-517-756-715-705-173-130-167-657-617-610-632-671-637-762-623-360-671-163-130-130-516-103-136-136-106-103-106-163-103-160-103-130-3_V.mp3)
  Segment 21: 62696ms ~ 63272ms (file: 130-163-103-206-130-651-163-136-103-163-136-132-123-136-316-213-231-612-576-106-106-316-130 613-163-160-136-106-163-160-103-175-316-103-317-137-126-136-132-123-136-123-542-102-1_V.mp3)
  Segment 22: 64272ms ~ 64848ms (file: 672-627-672-627-263-213-160-675-237-623-617-617-261-657-267-762-726-672-103-671-672-627-675-763-367-673-136-167-612-162-612-612-136-167-127-321-260-263-267-267_W.mp3)
  Segment 23: 65848ms ~ 66424ms (file: 510-510 763-267-725-672-275-271-721-270-725-251-276-267-217-157-251-217-275-162-720-527-265-261 216-162 607-125 167-126 126-103-152_V.mp3)
  Segment 24: 67424ms ~ 68000ms (file: 130-301-102-765-576-675-162-102-132-312-102-310-103-137-103-310 706-726 132-134-130-351-570-503-310-316-310-103-103-702-237-230-203-237-312-103-130-103-316-310-310-203-130-103-1_V.mp3)
  Segment 25: 69000ms ~ 69576ms (file: 216-126-120-103-130-132-753-750-176-765-762-671-167-106-301-321-362-302-320-263-326-623-236-206-263-276-623-236-362-321-130-132-102-106 362-321-627-263-157-321-103-167-326-320-3_W.mp3)
  Segment 26: 70576ms ~ 71152ms (file: 756-765-765-765-765-765-765-675-610-103-536-312 162-163-613-261-106-126-230-130-126-210-162-263-163-106-106-672-126-516-231-320-163-160-607-302-301-132-102-312 316 321-132-261-2_V.mp3)
  Segment 27: 72152ms ~ 72728ms (file: 312-135-103-317-123-136-103-675-103-150-510-567-136-162-103-136-316-103-316-675-103-132-123-103-102-216-103-103-106-671-173-321-260-126-162-130-716-672 527-132 574-576 132 675-2_V.mp3)
  Segment 28: 73728ms ~ 74304ms (file: 672-103-627-106-675-160-126-172 136-670-167-137-761-671-756-160-136-103-106-136-601-167-123-103-136-103-136-562-103-130-130-167-126-130-103-621-612-623-152-631-157 136-102-103-1_V.mp3)
  Segment 29: 75304ms ~ 75880ms (file: 716-761-361-165-136-167-106-163-106-163-136-160-106-756-752-710-176-623-621-625-652-526-572-231-576-751-571-571-571-576-567-567-527-675-657-312_W.mp3)
  Segment 30: 76880ms ~ 77456ms (file: 107-137-103-126-106-162-103-125-167_V.mp3)
  Segment 31: 78456ms ~ 79032ms (file: 125-126-361-672-761-637-761-103-721-137-132-612-163-301-312-321-231-236-621-623-132 136-165-103-126-231-162-312-263-263-321-231-326-236-263-631-162-162-671-316-750_V.mp3)
  Segment 32: 80032ms ~ 85756ms (file: 371-173-762-310-721-107-312-107-167-172-137-136-176-173-173-123-765-172-170-176-712-172-317-103-102-170-127-726-175-751-715-126-261-217-126-123-173-127-126-713-735-273-127-102-1_W.mp3)
  Segment 33: 86756ms ~ 98384ms (file: 276-267-627-123-621-217-123-120-716-176-107-201-756-756-167-132-173-756-715-756-673-261-267-752-102-136-130-620-756-276-756-672-261-217-267-102-263-216-752-260-261-123-102-261-2_W.mp3)
  Segment 34: 99384ms ~ 99960ms (file: 167-167-162-675-362-620-631-316-136-106-157-106-106-120-126-167-156-136-167-105-657-163-130-160-167-371-103-165-516-516-725_V.mp3)
  Segment 35: 100960ms ~ 101536ms (file: 103-761-263-762-132-125-210-120-210-127-156-167-107-317-132-756-657-261_V.mp3)
  Segment 36: 102536ms ~ 103112ms (file: 125-612-162-126-132-130-316-160-167-106-160-650-213-567-126-136-106-561-156-165-103-136-165-160-103-316-316-106-103-216-123-132-610-765-362-172-615-137-103-167-613-136-765-176-7_V.mp3)
  Segment 37: 104112ms ~ 104688ms (file: 103-136-612-617-617-627-162-613-612-672-167-316-126-102-162-160-612-126-261-162-267-267-267-123-163-132-675-675-607-756-261-132-317-413-756-620-321 632-637-627-672-103-132-326-1_V.mp3)
  Segment 38: 105688ms ~ 106264ms (file: 123-132-173-312-375-153-315-375-107-106-107-106-276-253-132-136-126-132-312-127-721-321-123-132-735-127-174-621-130-736-275-567-126-126-106-657-206-123-132-136-362-136-632-263-6_V.mp3)
  Segment 39: 107264ms ~ 107840ms (file: 203-320-203-230-326-213-321-126-316-126-136-136-132-213-213-217-237-237-726-276-237-217-267-276-276-726-762-672 641_W.mp3)
  Segment 40: 108840ms ~ 109416ms (file: 136-316-123 726-765-763-306-316-102-132-263-276-267-263-231-132-672-623-763-376-760 163-621_V.mp3)
  Segment 41: 110416ms ~ 110992ms (file: 231-312-312-127-173-157 126-162-132-156-651-576-165-162-601-630-136-136-103-526-163-316-316-136-162-127-103-103-671-576-317-617-675-173-130-136-576-136-261-263 617-136-621-257-5_V.mp3)
  Segment 42: 111992ms ~ 112568ms (file: 103-310-103-103-103-132-310-310-301-301-310-130-310-103-132-312-216-203-302-301-132-103-312-135-103-316-612-316-310-130_V.mp3)
  Segment 43: 113568ms ~ 114144ms (file: 754-574-753-317-754-756-610-756-756-756-576-574-574-276-267-625-625-652 547-517-135-657-765-657-567-576-756-765-103-264-326-236-765-756-675-675-671-617-612-326-326-326-735-765-7_W.mp3)
  Segment 44: 115144ms ~ 115720ms (file: 507-506-675-203-302-627-360-623-361-763-576-735-327-106-103-103-103-573-106-167-163-162-102-765-765-103-310-136-130-301-312-562-567-625-326-103-301-321-103-231-213-263-260-620-2_W.mp3)
  Segment 45: 116720ms ~ 117296ms (file: 623-176-103-316-103-103-613-610-136-103-136-172-571-130-132-132-103-132-135-751-265-615-103-132-136-136-130-137 547_V.mp3)
  Segment 46: 118296ms ~ 118872ms (file: 213-123-216-213-132-137-137-612-267-276-176-173-103-316-130-132 572-756-730-167-671-617 765-763-765-765-670-132-137-135-137-123-312-130-316-103-562-136-136-710-103-312-365-316-7_V.mp3)
  Segment 47: 119872ms ~ 120448ms (file: 106-765-163-316-351-657-675-765-765-162-316-136-132-163-136_W.mp3)
  Segment 48: 121448ms ~ 122024ms (file: 103-132-214-162-176-607-365-570-312-103-102-103-312-765_V.mp3)
  Segment 49: 123024ms ~ 123600ms (file: 513-103-310-130-102 310-103-123-132-301 135-135-157-135-103-301-103 135-105-130-103-315-301-135-123-130-132-102-132-103 102-130-130-103-132-103-132-135-103-132-312-132-312-153-1_V.mp3)
  Segment 50: 124600ms ~ 125176ms (file: 265 250-132-137-103-315-103-137-132-103-132-175-137-136-106-136-106-135-167-165-106-362-672-316-153-106-621-631-603-362-103-103-103-103-316-167-130-631-130-103-106-613-165-136-6_V.mp3)
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
> **Time**: 2026-04-04 04:21:15
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "천천히 가겠습니다. 저도 가야 돼. 기분 좋다. 다 출금했다.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "천천히", "start_ms": 20, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "가겠습니다", "start_ms": 480, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "저도", "start_ms": 1150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "가야", "start_ms": 1510, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "돼", "start_ms": 1840, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "기분", "start_ms": 2050, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "좋다", "start_ms": 2370, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 2840, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "출금했다", "start_ms": 3030, "dur_ms": 580, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "나도 연말쯤에 사려고. 왜냐면 부동산 가격이 좀 내려갈 것 같아서.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "나도", "start_ms": 30, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "연말쯤에", "start_ms": 360, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "사려고", "start_ms": 890, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "왜냐면", "start_ms": 1400, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "부동산", "start_ms": 1820, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "가격이", "start_ms": 2310, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 2770, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "내려갈", "start_ms": 2990, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 3420, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "같아서", "start_ms": 3580, "dur_ms": 460, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "연말에 이제 급매 나오는 거 잘 생각이고 너도 지금처럼 차",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "연말에", "start_ms": 40, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 430, "dur_ms": 290, "phase_lr_deg": 0.0},
{"word": "급매", "start_ms": 730, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "나오는", "start_ms": 1090, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 1520, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 1680, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "생각이고", "start_ms": 1900, "dur_ms": 510, "phase_lr_deg": 0.0},
{"word": "너도", "start_ms": 2450, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "지금처럼", "start_ms": 2770, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "차", "start_ms": 3300, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "한국 산업 맥락에서 블룸버그나 더 인포메이션 수준의 밀도를 원한다면 하이젠 보고서도 좋다고 생각합니다.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "한국", "start_ms": 50, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "산업", "start_ms": 380, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "맥락에서", "start_ms": 700, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "블룸버그나", "start_ms": 1230, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 1820, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "인포메이션", "start_ms": 2010, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "수준의", "start_ms": 2640, "dur_ms": 390, "phase_lr_deg": 0.0},
{"word": "밀도를", "start_ms": 3040, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "원한다면", "start_ms": 3470, "dur_ms": 510, "phase_lr_deg": 0.0},
{"word": "하이젠", "start_ms": 3990, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "보고서도", "start_ms": 4450, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "좋다고", "start_ms": 4980, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "생각합니다", "start_ms": 5370, "dur_ms": 610, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "상위 1% 연구자들이 직접 쓰는 플랫폼이고 독자가 요약된 해설을",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "상위", "start_ms": 40, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "1%", "start_ms": 370, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "연구자들이", "start_ms": 730, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "직접", "start_ms": 1320, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "쓰는", "start_ms": 1650, "dur_ms": 290, "phase_lr_deg": 0.0},
{"word": "플랫폼이고", "start_ms": 1950, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "독자가", "start_ms": 2540, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "요약된", "start_ms": 2930, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "해설을", "start_ms": 3360, "dur_ms": 410, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "13일에도 SK하이닉스가 대장주로 움직였습니다. 그래서 흐름을 추적하는 거죠. 사상 최고가를 돌파하는 대량 거래 장대 양봉이 나오고 나서 그리고 나",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "13일에도", "start_ms": 30, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "SK하이닉스가", "start_ms": 560, "dur_ms": 780, "phase_lr_deg": 0.0},
{"word": "대장주로", "start_ms": 1350, "dur_ms": 510, "phase_lr_deg": 0.0},
{"word": "움직였습니다", "start_ms": 1870, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 2560, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "흐름을", "start_ms": 2950, "dur_ms": 390, "phase_lr_deg": 0.0},
{"word": "추적하는", "start_ms": 3350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "거죠", "start_ms": 3810, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "사상", "start_ms": 4100, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "최고가를", "start_ms": 4420, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "돌파하는", "start_ms": 4950, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "대량", "start_ms": 5440, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "거래", "start_ms": 5770, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "장대", "start_ms": 6090, "dur_ms": 290, "phase_lr_deg": 0.0},
{"word": "양봉이", "start_ms": 6390, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "나오고", "start_ms": 6810, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "나서", "start_ms": 7200, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 7530, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "나", "start_ms": 7890, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "중식 요리를 몇 가지 정도 이렇게 먹으면 좀 느끼해지기도 하고 하는데 여기 오면 많은 요리를",
"speaker": "speaker_4",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "중식", "start_ms": 50, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "요리를", "start_ms": 380, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "몇", "start_ms": 740, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "가지", "start_ms": 930, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "정도", "start_ms": 1220, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 1540, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "먹으면", "start_ms": 1900, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 2290, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "느끼해지기도", "start_ms": 2510, "dur_ms": 590, "phase_lr_deg": 0.0},
{"word": "하고", "start_ms": 3110, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "하는데", "start_ms": 3440, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "여기", "start_ms": 3830, "dur_ms": 290, "phase_lr_deg": 0.0},
{"word": "오면", "start_ms": 4130, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "많은", "start_ms": 4450, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "요리를", "start_ms": 4780, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "많이 노력을 하신 거 같아요. 근데 또 제가 어떤 아이디어를 가지고 왔을 때",
"speaker": "speaker_5",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "많이", "start_ms": 40, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "노력을", "start_ms": 370, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "하신", "start_ms": 760, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 1080, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 1270, "dur_ms": 390, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 1700, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 2020, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 2240, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 2570, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "아이디어를", "start_ms": 2890, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "가지고", "start_ms": 3420, "dur_ms": 390, "phase_lr_deg": 0.0},
{"word": "왔을", "start_ms": 3820, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 4140, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "많이 마려울 텐데 계단 앞에서 꼼짝도 안 하는 진도개들. 무서워 무서워 계단 무서워. 아이고 참. 결국 10분 실랑이 하다가 제가 안고 계단을 내려옵니다. 평생 1층",
"speaker": "speaker_6",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "많이", "start_ms": 30, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "마려울", "start_ms": 360, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "텐데", "start_ms": 750, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "계단", "start_ms": 1070, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "앞에서", "start_ms": 1400, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "꼼짝도", "start_ms": 1830, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 2290, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 2510, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "진도개들", "start_ms": 2870, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "무서워", "start_ms": 3410, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "무서워", "start_ms": 3800, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "계단", "start_ms": 4190, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "무서워", "start_ms": 4510, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "아이고", "start_ms": 4920, "dur_ms": 390, "phase_lr_deg": 0.0},
{"word": "참", "start_ms": 5320, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "결국", "start_ms": 5630, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "10분", "start_ms": 5990, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "실랑이", "start_ms": 6320, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "하다가", "start_ms": 6740, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 7170, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "안고", "start_ms": 7490, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "계단을", "start_ms": 7820, "dur_ms": 390, "phase_lr_deg": 0.0},
{"word": "내려옵니다", "start_ms": 8220, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "평생", "start_ms": 8810, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "1층", "start_ms": 9170, "dur_ms": 310, "phase_lr_deg": 0.0}
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
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-04 04:21:15*
