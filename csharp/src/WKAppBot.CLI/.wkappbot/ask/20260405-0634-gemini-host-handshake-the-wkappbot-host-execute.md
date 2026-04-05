# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=6944  time=06:33:13  uptime=17402s  mem=332MB  system_processes=351"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_063300.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 267-127-152-176-163-123-163-317-125 671-513-125-103-132-145-162_V.mp3)
  Segment 2: 1576ms ~ 3196ms (file: 705-375-157-157-715-715-157-105-107-107-175-170-175-105-130-103-130-573 157-137-517-517-531-170-150-351-531-750-357-357-351-135-153-137-137-130-130-135-157-157 317-312-130-351-1_V.mp3)
  Segment 3: 4196ms ~ 9524ms (file: 201-217-203-127-230-452-257-321-302-321-263-231-326-201-123-321-260-230-302-270-167-106-167-106-106-201-203-231-321-236-270-256-203-217-217-210-203-120-276-120-271-213-123-102-2_W.mp3)
  Segment 4: 10524ms ~ 11100ms (file: 675-657-163-130-213-216-312-126-172-175-613-130-103-506-567-657-573-137-123-617-675-735-650-657-367-750-635-732-607-176-276-273-102-167-267-517-126-103-163-136-163-136-103-310-6_V.mp3)
  Segment 5: 12100ms ~ 12676ms (file: 261-264-206-206-206-206-206-203-321-312-301-301-301-530-315-301-302-321-756-756-765-762-627-263-326-125-625-513-617-167-163-132-136-132-326-315-231-132-316-132-321-261-312-132-3_V.mp3)
  Segment 6: 13676ms ~ 20876ms (file: 267-127-167-317-120-216-572-576-276-760-761-263-103-173-167-576-267-267-762-762-267-123-207-206-132-574-267-203-310-713-276-267-672-267-756-302-276-276-673-137-713-310-236-173-3_W.mp3)
  Segment 7: 21876ms ~ 34692ms (file: 310-130-572-203-132-315-310-213-123-130-103-130-752-103-130-103-132-153-103-104-103-130-763-130-132-302-130-325-160-651-765-567-754-132-130-517-201-230-103-356-130-130-310-576-2_W.mp3)
  Segment 8: 35692ms ~ 36268ms (file: 547-576-564-546-546-562-602-625 206-206-567-503-574-267-263-623-567-310-130-310-310-670-623-607-627-705-705-132-176-137-173-137-160-106-136-613-602-601-316-130-103-613-310-610-1_V.mp3)
  Segment 9: 37268ms ~ 37844ms (file: 312-326-275-276-237-217-752-725-752-671-672-632-263-263-263-576-725-752-267-263-265-267-267 276-261-621-267-261-136-107-103-136-316-316-137-314-637-671-675-765-765-765-675-670-6_W.mp3)
  Segment 10: 38844ms ~ 46620ms (file: 132-210-310-310-312-136-137-317-136-317-316-317-135-371-137-317-715-175-753-153-756-317-357-231-514-130-132-312-132-130-310-132-132-132-120-210-132-136-130-137-130-103-136-106-2_W.mp3)
  Segment 11: 47620ms ~ 53992ms (file: 765-726-276-756-627-260-132-127-120-106-673-674-627-123-265-236-267-263-126-570-756-265-627-237-756-267-517-172-257-267-326-302-320-230-260-267-320-132-132-301-715-526-102-102-1_W.mp3)
  Segment 12: 54992ms ~ 55568ms (file: 231-321-230-260-672-130-136-137-167-162-175-176-165-176-167-160-125-132-136-316-612-162-216-126-576-756-756-751-671-321-175-261-312-136-316-175-137-137-137-130-513-536-312-236-3_W.mp3)
  Segment 13: 56568ms ~ 65532ms (file: 231-260-236-263-267-627-547-231-107-126-263-263-103-162-516-310-130-302-725-576-627-167-312-263-627-130-754-367-374-316-531-310-543-231-236-130-765-627-261-123-263-621-162-321-1_W.mp3)
  Segment 14: 66532ms ~ 67108ms (file: 517-576-517-572-157-517-153-153-317-521-157-572-571-157-573-731-571-570-175-571-517-517-576-571-576-576-576-576-517-570-530-506-570-517-507-507-517-570-510-570-501-510-576-560-5_V.mp3)
  Segment 15: 68108ms ~ 68684ms (file: 167-123-103-103-175-175-504-162-507-547-621-103-153-160-130-163-103-162-132-514-162-261-103-103-103-102-132-103-132-261-176-612-671-621-613-610-102-162-312-312-657-316-561-251-5_V.mp3)
  Segment 16: 69684ms ~ 79980ms (file: 673-230-302-175-371-537-321-132-571-230-260-236-213-135-312-675-263-326-576-752-570-572-573-357-765-507-547-562-612-236-705-231-726-527-236-263-627-726-627-165-637-125-712-765-2_V.mp3)
  Segment 17: 80980ms ~ 81556ms (file: 750-750-750-750-750-750-750-756-750-750-750-750-754-750-750-750-130-153-105-105-105-150-150-157-165-157-517-157-157-157-157-516-157-517-157-175 105-105-105-150-150-157-103-107-1_V.mp3)
  Segment 18: 82556ms ~ 93824ms (file: 723-172-236-231-236-230-310 263-236-321-213-231-260-102-276-276-120-103-276-267-276-267-723-173-103-103-276-167-327-107-276-273-137-103-120-317-270-260-275-265-756-273-213-102-1_W.mp3)
  Segment 19: 94824ms ~ 95400ms (file: 261-267-263-310-361-263-210-621-672-627-260-162-613-170-760-170-103-310-312-130-130-607-162-126-627-670-620-256-562-102-162-230-132-102-201-201-260-216-261-206-203-130-132-132-1_V.mp3)
  Segment 20: 96400ms ~ 96976ms (file: 167-132-620-672-672-672-176-162-367-136-157-571-573-574-573-756 103-126-162-627-167-160-631-751-176-613-130-237-167-160-671-162-154-157-137-571-175-736-132-761-167-106-137-163-1_W.mp3)
  Segment 21: 97976ms ~ 104024ms (file: 216-325-120-531-267-237-120-231-213-213-621-726-237-257-265-267-371-160-652-652-236-276-765-753-675-576-576-756-321-705-705-207-103-301-312-103-137-317-267-320-132-130-567-571-2_W.mp3)
  Segment 22: 105024ms ~ 105600ms (file: 571-573 517-125-512-150 650-132-651 657-107-706-132-306-261-621-625-357-257-672-327-312-675-351-576-563-527-567-365-136-106-362-162-367-312-126-136-653-163-261-210-321-367-362-2_W.mp3)
  Segment 23: 106600ms ~ 107176ms (file: 132-103-756-167-103-137-106-105-163-175-562-517-135-675-571-157-156-571-137-106-765-136-135-130-162-103-106-516-107-137-102-162-126-102-263-675-675-657-167-601-163-136 231-235-3_V.mp3)
  Segment 24: 108176ms ~ 112136ms (file: 301-306-723-627-625-267-671-327-532-206-752-275-627-132-321-103-672-271-725-267-127-276-756-273-123-123-175-126-271-712-715-327-216-217-260-312-123-756-715-176-123-761-762-760-7_W.mp3)
  Segment 25: 113136ms ~ 113712ms (file: 517-570-570-574-574-574-574-574 547-570-507-570-574-574-754-751-754-574-547-754-754-571-754-574-751 574-574-574-570-570-751-570-574-754-571-570-574-574-574-574-751-157-150 510-5_W.mp3)
  Segment 26: 114712ms ~ 115288ms (file: 675-765-760-567-765-675-675-675-765-675-765-756-765-716-761-765-675-765-675-765-675-765-675-671-765-675-765-756-576-576-756-576-675-671-673-675-617-675-765-675-657-675-765-675-7_W.mp3)
  Segment 27: 116288ms ~ 133172ms (file: 102-237-312-312-237-231 673-137-176-267-132-321 637-761-617-263-120-210-217-103-163-231-130-321-231-574-253-271-321-310-231-327-130-267-267-231-231-231-102-106-321-231-123-123-1_W.mp3)
  Segment 28: 134172ms ~ 134748ms (file: 672-675 675-675-675-765-576-675-761-316-106-107-170-137-173-132-127-126-765-261-263-213-231-237-623-132-123-163-105-132-103-756-567-271-267-312-502-215-612-216-570-235-572-523-5_V.mp3)
  Segment 29: 135748ms ~ 146044ms (file: 574-260-261-540-102-120-201-310-130-765-230-231-201-547-657-547-567-213-102-103-263-206-201-120-102-203-756-765-206-210-230-514-256-207-574-547-574-327-574-571-547-547-132-257-2_W.mp3)
  Segment 30: 147044ms ~ 147620ms (file: 571-517-157-751-753-751-570-673-675_V.mp3)
  Segment 31: 148620ms ~ 149196ms (file: 715-620-621-271-107-706-103-657 316-236-136_W.mp3)
  Segment 32: 150196ms ~ 150772ms (file: 657-765-675 170-710-170-507-107-170-571-157-571-175-170-170-170-710-107-710-170-710-170-715-571-756-570-567-756-571 107-571-571-751-574-576-574-576-574-576-574-574-576-576-576-5_W.mp3)
  Segment 33: 151772ms ~ 159872ms (file: 547-627-103-165-167-751-132-576-321-716-231-316-237-237-576-130-132-375-127-172-231-261-260-216-271-213-321-236-231-321-123-231-321-231-237-532-106-321-753-312-132-273-752-216-2_W.mp3)
  Segment 34: 160872ms ~ 161448ms (file: 175-130-175-170-715-170-175-175-715-175-715-713-571-175_V.mp3)
  Segment 35: 162448ms ~ 163024ms (file: 167-301-170-301-137-102-136-315-135-135-317-132-137 751-715-705-107-705-175-357-137-317-137-137-103-312-607-761-361-167-136-673-671-613-632-136-136 703-103-137-317-130-310-317-3_V.mp3)
  Segment 36: 164024ms ~ 181556ms (file: 316-312-312-316-136-312-316-316-132-132-312-163-206 137 106-136-103-106-173-103-312-102-103-312-143-306-102-102-230-302-164-106-103-320-102-203-301-263-762-132-312-261-521-357-1_V.mp3)
  Segment 37: 182556ms ~ 183132ms (file: 631-632-637-673-307-735-167-163-652-175-172-132-167-321-312-203-176-132-103-132-321-326-623-230-132-123-106-162-105-132-135-103-103-106-576-507-316-123-357-132-316-326-236-326-3_V.mp3)
  Segment 38: 184132ms ~ 206416ms (file: 675-671-132-362-103-572-132-120-132-103-103-572-173-132-130 321-312-210-260-276-206-231-123-103-571-216-132-574-527-261-134-231-103-510-210-103-103-210-321_V.mp3)
  Segment 39: 207416ms ~ 207992ms (file: 576-576-715-751-617-157-756-657-517-576-671-517-107-107-107-157-617-701-170-175-701-107-751-175-165-165-130-156-153-153-576-517-571-751-756-657-675-175-675-576-175-157-135-167-1_W.mp3)
  Segment 40: 208992ms ~ 214896ms (file: 213-213-231-102-260-761-175-721-301-312-615-231-365-752-571-574-576-752-573-317-176-675-173-317-613-327-123-372-261-267-620-613-126-165-265-162-215-125-576-170-127-120-120-216-1_V.mp3)
  Segment 41: 215896ms ~ 216472ms (file: 150-150-105-150-175-154 105-103-105-105-105-150-165-156-156-157-150-150-150-105-517-157-517-156-157-105-104 105-105-150-105-150-105-150-156-106-150-167-175-157-165-175-157-517-1_V.mp3)
  Segment 42: 217472ms ~ 223988ms (file: 206-217-176-765-756-167-715-716-106-726-103-716-705-765-103-302-627-627-276-637-137-726-706-672-765-672-310-312-750-207-273-276-267-173-106-162-523-371-762-721-572-325-725-725-7_W.mp3)
  Segment 43: 224988ms ~ 225564ms (file: 105-157-175-130 571-571-157-517-571-517-517-710-157-105-513-507-173-173-170-137-175-175-135-170-157-150-103-135-173-130-107 517-501-175-157-175-105-150-173-371-173-713-137-713-1_V.mp3)
  Segment 44: 226564ms ~ 232252ms (file: 201-103-125-257-102-237-504-541-270-127-701-120-107-310-321-726-720-310-107-231-231-230-326-537-201-201-527-127-257-203-125-705-167-137-102-132-267-602-321-310-315-103-761-523-2_V.mp3)
  Segment 45: 233252ms ~ 233828ms (file: 637-136-103-631-136-137-173-163-175-126-130-132-617-107-621-157-137-136-103-164-165-163-617-106-105-106-612-106-175-162-312-167-106-103-137-132-173-620-176-162-106-136-106-167-6_V.mp3)
  Segment 46: 234828ms ~ 235404ms (file: 675-632-236-263-621-132-162-612-267-126-126-367-361-362-620-126-213-201-312-132-163-136-731-213-236-126-261-261-263-261-236-261-627-617-612-276-267-263-260-270-206-621-261-231-2_W.mp3)
  Segment 47: 236404ms ~ 236980ms (file: 263-236-236-361-613-361-136-162-136-621-576-576-261-623_W.mp3)
  Segment 48: 237980ms ~ 238556ms (file: 150-105-105-103-105-105-157-105-105-105-105-150-501-510-571 105-510-150-510-501-150-750-571-570-570-501-571-570-157-157-150-105-105-154-510-105-501-510-510-517-571 105-510-150-5_V.mp3)
  Segment 49: 239556ms ~ 240132ms (file: 150-501-510-517-510-571-570-517-105-751-571-570-574-573 571-150-105-157-105-157-517-517-517-507-517-150-105-501-513-501-571-570-507-507-576-507-570-507-576-574-507-570 510-157-1_V.mp3)
  Segment 50: 241132ms ~ 241708ms (file: 132-137 321-136-162-623-726-637-756-572-576-576-576-576-756-675-657-132-103-135-132-103-103-312-163-103-165-613-136-162-126 106-162-561-315-106-136-751-167-160-136-132-106-541-1_W.mp3)
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
> **Time**: 2026-04-05 06:34:13
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "知恵が一番、知恵さが一番だな",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ja",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "知恵が", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "一番、", "start_ms": 170, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "知恵さが", "start_ms": 290, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "一番だな", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "바닥 타일 하나도 오염을 최소화할 수",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "바닥", "start_ms": 40, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "타일", "start_ms": 290, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "하나도", "start_ms": 490, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "오염을", "start_ms": 790, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "최소화할", "start_ms": 1140, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 1440, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "어머님 생신이 4월 22일입니다 그래서 제가 아주 애기 때부터 지역 번호 쓸 때 집 전화번호도 891에 뒷자리가 4",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "어머님", "start_ms": 60, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "생신이", "start_ms": 410, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "4월", "start_ms": 810, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "22일입니다", "start_ms": 1110, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 1710, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 2110, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "인가 생각이 되는데요 우주에서 가장 다루기 어려운 물질을 담아 트럭이 달리는 동안 그 조용한 행렬을 지켜보며 눈물을 흘린 사람들이 있었습니다",
"speaker": "speaker_4",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "인가", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "생각이", "start_ms": 200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "되는데요", "start_ms": 500, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "30년을 기다린 사람들이었겠죠 반물질 연구는 이제 더 이상 실험실",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "30년을", "start_ms": 40, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "기다린", "start_ms": 440, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "조상들을 어떤 기준으로 세울 것인가를 기준으로 세울 때 저희는 얼굴을 기준으로 했습니다 왜냐하면 이 얼굴을 보면 표정이 다 담겨 있어요 뱀을 먹는 괴수도 보면 입이 이렇게",
"speaker": "speaker_5",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "조상들을", "start_ms": 30, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 430, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "진로를 바꾼 건 할머니 때문이었습니다 할머니가 같이 하자고",
"speaker": "speaker_6",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "mixed",
"words": [
{"word": "진로를", "start_ms": 50, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "바꾼", "start_ms": 400, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "건", "start_ms": 700, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "할머니", "start_ms": 850, "dur_ms": 450, "phase_lr_deg": -5.0},
{"word": "때문이었습니다", "start_ms": 1300, "dur_ms": 700, "phase_lr_deg": -5.0}
]
},
{
"segment": 8,
"transcript": "있겠다는 나름 계산된 포석입니다 안보 다음은 반드시 돈입니다 순서는 항상 같습니다 피는 우리가 흘리고 돈도 우리가 내라는 겁니다 우리는 조만간 트럼프 행정부가 호르무즈",
"speaker": "speaker_7",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "있겠다는", "start_ms": 40, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "You're in trouble man, you're in trouble cause you're not gonna adapt",
"speaker": "speaker_8",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "You're", "start_ms": 80, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 280, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "trouble", "start_ms": 430, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "en",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 11,
"transcript": "Okay, I'm gonna take these",
"speaker": "speaker_9",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "Okay,", "start_ms": 100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "I'm", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "gonna", "start_ms": 550, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "때문에 내가 더 잘해가지고 부모님도 호강시켜드리면 효심도 되실 거고요 자 그럼 어떻게 이겨내요 저는 나한테 한마디를 해줄 수 있다라고 한다면은 잘하고 있다 이런 것도 모든 것도 과정이다라고 얘기해주고 싶어요 제가 방향성이 맞다면 제 앞",
"speaker": "speaker_10",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "때문에", "start_ms": 40, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "어부지득을 누리고 세계 최고 수준의 극지 연구 능력을 갖추고 기후 변화에도 대",
"speaker": "speaker_11",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "어부지득을", "start_ms": 50, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "So I never saw the strategies",
"speaker": "speaker_12",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "So", "start_ms": 120, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 220, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "현장에서 난리가 날 텐데 저렇게 해도 되나 싶었거든요 그리고 이번 사태가 약간 그 반증이지 않을까 싶습니다 그리고 기업이 크면 클수록 한번 내뱉은 말에 좀 책임을 져야 된다는 생각이 강한지 말을 잘 바꾸는 일이 없는데 그러다 보니까 이 사태가 그냥 어 제가 잘못 말했어요 업데이트 해줄게요라고",
"speaker": "speaker_13",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "현장에서", "start_ms": 60, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "현지 사람들과 깊은 교류를 나누며 살아가는 지금이 진정한 여행이라고 생각했다는 이들",
"speaker": "speaker_14",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "mixed",
"words": [
{"word": "현지", "start_ms": 40, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "I'm here, this is last summer when I was here and they took us to",
"speaker": "speaker_15",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "I'm", "start_ms": 100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "here,", "start_ms": 250, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "상업 어업 자체도 매우 위험한 직업이지만 그중에서도 베링해의 게잡이는 사망률 면에서 단연 두드러집니다 이처럼 베링해는 모든 장비가 완비된 현대식",
"speaker": "speaker_16",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "상업", "start_ms": 35, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "축소되는 게 아닙니다 상향 이동되는 겁니다 즉 직접 공을 차는 선수에서 전술을 짜고 팀을 운영하는 감독으로 한 단계 올라가는 겁니다 여러분의 프로젝트에 하네스가 있나요 클라우드 닷 MD든 프리커밋 훅이든 아키텍처든 테스트든 어떤 형태로든 AI를 통제하는 시스템이 있으시다면 댓글로 한번 알려주세요",
"speaker": "speaker_17",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "축소되는", "start_ms": 50, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "우리 우주가 반물질 대신 우리를 택했는지 또 왜 아무것도 없는 대신 무언가가 이 우주에 존재하고 있는지 오늘의 이야기는 여기까지입니다 앞으로도 더 재미있는 소식으로 찾아뵙도록 하겠습니다 지금까지 안될과학 항성이었습니다 감사합니다",
"speaker": "speaker_4",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "우리", "start_ms": 40, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "뭐야 이거 만두?",
"speaker": "speaker_18",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "뭐야", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이거", "start_ms": 170, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "만두?", "start_ms": 320, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "요즘은 그",
"speaker": "speaker_19",
"confidence": 0.90,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "요즘은", "start_ms": 30, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 280, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "요 이평선 자리에서 지지 상승이 나왔고 아침 고점을 돌파하고 그 아침 고점",
"speaker": "speaker_20",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "요", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "만드는 겁니다 이것은 허영심의 문제가 아닙니다 현대 사회에서 매끄럽고 건강한 피부라는 것은 곧 철저한 자기 관리와 건강",
"speaker": "speaker_21",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "만드는", "start_ms": 45, "dur_ms": 300, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 06:34:13*
