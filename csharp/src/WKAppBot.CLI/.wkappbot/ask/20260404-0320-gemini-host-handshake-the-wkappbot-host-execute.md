# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=34340  time=03:19:36  uptime=8303s  mem=130MB  system_processes=364"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260404_031925.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 613-362-672-102-123 261-263-237-263-627 162-657-163-672-623 761-673-671-163-136-625-632-267-762-623-675-675-765-132-137-167-136-612-601-612-136-162-165-672-627-627-627-672-672-7_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 572-573-572-527-753-572-532-352-321-312-312-120-132-137-152-572-725-732-257-752-215-271-253-215-217-210-217-271-231-213-236-235-236-321-231-235-257-254-527-257-235-253-253-325-2_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 175-216-120-213 756-765-756-753-756-765-765-106-625-136-657-657-167-107-574-756-107-671-163-612-623-132-137-103 173-176-631-167-321-103-132-106-106-372-327 617 165-671-106-613-1_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 312-321-301-621-621-230-260-256-206-203-302-301-201-621-126-671-136-176-162-103-132-123-301-103-203-206-302-132-312-107-132-301-130-301-260-260-260-210-136_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 726-672-672-675-672-621-265-216-231-612-610-627-312-216-321-126-267-261-216-261-236-132-623-621-136-126-132-123-132-267-263-321-261-621-213-167-176-756-761_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 153-136-316-130-137-317-570-675-163-173-106-176-167-106-317_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 716-756-675-315-361-657-576-571-175-125-157-132-375-167-537-517-571-157-571-156-756-716-765-673-367-761-167-167-136-176-716-176-761-517-317-176-167-176-167-173-316-315-316-136-1_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 237 123-601-610-312-632-362-612 102 167-671-106-106 137_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 312-103-103-137-175-103-123-107-320-265-321-120-123-132-130-103-132-215-123-165-164-164-123-175-153-175-132-132-137-176-167-167-321-216-132-310-301-302-215-203-136-106-623-623-1_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 367-603-637-613-132-617-312-206-216-102_V.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 132-162-157-237-320-230-123-230-320-302-103-162-103-203-201-260-620-260-607-670-602-762-126-126-230-320-132-310-316-167-103-302-103-310-362-306-706-576-657-163-321-206-263-203-6_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 310-320-360-310-130-103-103-136-316-301-130-310-312-310-130-130-132-613-136-103-312-320-321-213-321-207-276-203-310-130-310-310-130-103-102-136-136-310-360-103-301-310-130-312-1_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 673-672-763-673-761-762-672-670-672-673-670-675-673-376 126-574-576-576-153-130-136-312-751-705-103-312-132-623-632-267-263-236-265-213-271-621-617-276-267-162-672-627-267-765-6_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 130-536-136-103-315-103-102-361-106-103-136-163-132-316-103-136-263 136-136-103-163-130-136-103-135-312-132-103-312-156-302-135-623-136-103-106-352-106-167-165-136-136-103-136-5_V.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 560-127-263-103-761-762-736-716-163-531-765-763-706-173-675-167-617-610-610-762-760-765-765-762 623-163-362-671-601-756-160-130-163-103-136-612-125-561-132-231-167-126-163-631-1_V.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 673-672-576-567-657-567-356-357-367-365-537-236-231-132-130-103-326-326-321-236-326-312-763-657-560-563-356-356-376-310-130-130-623-627-621-261-621-130-312-130-103-231-216-310-3_V.mp3)
  Segment 17: 25216ms ~ 34216ms (file: 617-103-317-731-617-310-320-357-206-362-102-103-756-216-765-316-365-605-130-103-105-135-153-367-235-635-265-310-320-321-130-207-605-257-256-236-756-316-376-376-623-263-327-563-6_W.mp3)
  Segment 18: 35216ms ~ 35792ms (file: 130-320-132-316-172-130-261-621 210-201-103-301_W.mp3)
  Segment 19: 36792ms ~ 37368ms (file: 267-217-326-132-760-170-576-763-176-760-760-167-376-730-312-103-107-173-257-270-326-327-107-103-671-306-107-106-675-273-126-621-216-120-126-326-327-326-231-365-672-267-126-130-7_W.mp3)
  Segment 20: 38368ms ~ 38944ms (file: 265-752-527-562 567-576-576-567-567-567-657-576-576-567-567-576-567-657-567-657-576-765-675-576-756-576-576-756-576-567-657-657-756-657-675-657-567-576-576-567-657-657-567-576-1_W.mp3)
  Segment 21: 39944ms ~ 40520ms (file: 261-621-574-573-632-261-162-263-263-167-706-267-261-310-103-162-361-123-762-267-726-267-627-627-612-162-613-602-621-126-621-216-236-213-627-261-672-637-316-761-763-316-107-106-6_W.mp3)
  Segment 22: 41520ms ~ 42096ms (file: 127-163-163-621-627-230-230-132-216-213-302-617-127-130-150-316-136-103-602-136-163-136-130-632-132-517-126-103-631-136-136-103-130-106-103-103-516-706-165-617-137-103-136-651-1_V.mp3)
  Segment 23: 43096ms ~ 43672ms (file: 263-163-601-613-603-362-623-136-103-136-163-136-316 763-765-765-756-765-756-765-765-751-156-675-765-206-360-260-627-672-670-637-675-136-312-102-260-617-613-316-301-301-130-103-1_V.mp3)
  Segment 24: 44672ms ~ 56732ms (file: 215-123-207-102-310-130-263-231-107-320-236-267-276-267-267-123-620-130-103-267-263-320-260-326-326-123-310-230-263-231-213-106-321-362-632-621-103-103-231-123-267-267-312-130-1_V.mp3)
  Segment 25: 57732ms ~ 58308ms (file: 316-176-670-326-761-126-136-672-617-731-176-167-703-103-106-163-103-132-167-301-761-763-107-167-160-163-617-163-613-617-160-106-716-326-236-623-621-132 756-763-756-756-675-637-7_W.mp3)
  Segment 26: 59308ms ~ 69136ms (file: 713-765-756-735-705-765-671-632-126-613-532-267-627-267-267-627-261-547-260-102-316-514-532-267-612-267-572-261-320-547-754-216 267-176-216-765-261-261-753-574-201-210-267-326-2_W.mp3)
  Segment 27: 70136ms ~ 70712ms (file: 563-507-376-130-310-312-310-123-261-321-135-103-103-301-301-132-316-136-301-103-310-136_V.mp3)
  Segment 28: 71712ms ~ 72288ms (file: 213-261-321-231-203-216-321-130-201-203-231-326-261-367-362-213-132 752-752-705-321-216-765-576-570-135-162-750-326-216-231-621-627-320-236-263-321-231-123-613-145-126-132-132-1_V.mp3)
  Segment 29: 73288ms ~ 73864ms (file: 372-327-103-132-136 602-316-613-103-132-163-623-103-103-612-621-165-210-126-612-617-163-601-613-612-216-126-621-162-361-627-761-671-612 627-620-756-765-367-756-762-763-675-617-2_W.mp3)
  Segment 30: 74864ms ~ 75440ms (file: 103 765-756-765-752-765 562-562-657-762-567-567-576-576-765-576-576-675-563-356-576-637-367-623-627-627-602-362-637-267 103-567-625-260-267-206-201-573-572-756 106-167-103_W.mp3)
  Segment 31: 76440ms ~ 77016ms (file: 612-123-103-132 136-136-612-162-127-125 130-103-301-103-302-137-103-310-301-316-136-132-213-301-103-103-103-765-176-175-123 627-627-162-162-765-756-735-234-241-123-671-671-103-1_V.mp3)
  Segment 32: 78016ms ~ 78592ms (file: 163-136-137-165-136-167-103-167-130-106-106-106-135-173-310-675-123-167-162-176-167-106-107-167-136-512-127-126-362-106-103-165-105-326-710-175-173-716-561-576-754-675-617-675-6_V.mp3)
  Segment 33: 79592ms ~ 80168ms (file: 627-236-362-326-263-652-652-653-675-625-256-256 326-751-756-761-176 362-612-673-162-627-627-162-162-162-621-267-763-132-157-175-751-756-756-765-576-675-675 126-612-623-612-621-6_W.mp3)
  Segment 34: 81168ms ~ 91932ms (file: 267 723-236-123-312-257-273-275-271-213-102-123-103-270-203-103-130-765-756-672-267-756-275-627-267-725-276-267-267-231-136-721-763-362-260-263-261-765-230-276-126-765-276-567-6_W.mp3)
  Segment 35: 92932ms ~ 105784ms (file: 672-267-263-612-267-263-263-236-362-253-625-265-263-621-612-126-165_W.mp3)
  Segment 36: 106784ms ~ 107360ms (file: 103-162-526-567-632-362-623-612-627-621-632-316-312-136-625-625-621-236-263-321-326-123-621-627-631-621-132-623-623-623-263-236-613-657-613-136-136-652-627-263-623-162-236-362-6_W.mp3)
  Segment 37: 108360ms ~ 108936ms (file: 627-267 670-765-761-657-623-267-526-256-527 672-362_W.mp3)
  Segment 38: 109936ms ~ 110512ms (file: 106-263-326-236-603-621-261-326-567-675 301 564 567-567-506-657-675-623-612-103 103-263-261-213-123-132-102-361-163-164-165-641-645-671-132-163 162-236-526-265-562-236-263-231-2_W.mp3)
  Segment 39: 111512ms ~ 116588ms (file: 260-627-576-617-267-206-206-260-620-263-260-627-570-570-267-267-236-627-260-756-562-612-726-760-620-621-627-762-162-621-612-723-672-620-763-637-260-261-672-761-762-762-607-572-7_W.mp3)
  Segment 40: 117588ms ~ 118164ms (file: 673 162-610-673-105-157-106-157-157-571-571-136-102-136-630-657-612-162-163-562-716-631-126-143-136-103-136-615-103-103-316-103-106-127-160-726-132-174-317-103-136-136-675-617-1_V.mp3)
  Segment 41: 119164ms ~ 119740ms (file: 361-613 132-263-263-263-623-263-260_W.mp3)
  Segment 42: 120740ms ~ 129056ms (file: 317-136-216-231-236-132-573-137-207-136-137-130-126-132-576-357-315-572-735-123-267-263-237-130-103-107-127-315-572-367-537-705-517-126-217-675-126-106-231-627-267-130-176-651-1_V.mp3)
  Segment 43: 130056ms ~ 130632ms (file: 567-675-761-762-261-216-136-126-236-316-273-623-621-613-735-367-657-163-675-736-627-675-765-672-627-672-627-627-675-765-652-562-672-726-657-167-657-176-657-613-623-317-635-631-3_W.mp3)
  Segment 44: 131632ms ~ 132208ms (file: 134-103-571-165-106-103-621-623-631-106-517-210-102-106-621-106-162-103-156-176-163-106-610-173 167-102-132-163-165-317-103-165-615-106-136-167-162-126-106-326-103-102-167-136-1_V.mp3)
  Segment 45: 133208ms ~ 133784ms (file: 173-657-756-765-756-765-675-715-675-657-765-762-172-761-671-765-765-675-675-675-765-765-756-765-765-671-715-576-756-567-657-571-756-657-576-657-650-631-306-106-301-756-750-706-6_W.mp3)
  Segment 46: 134784ms ~ 135360ms (file: 130-106-163-103-103-136-176-317-176-163-103-137-167-160-102-103-631-136-326-165-157-132-162-106-136-163-163-135-163-621-236-162-165-162-106-167-167-612-163-167-316-130-136-657-5_V.mp3)
  Segment 47: 136360ms ~ 136936ms (file: 567-564-547-605-620-206-310-103-130-132-103-201-126-576-536-302-310-302-103-103-107-103-140-105 576-310-517-123-213-527-750-203-206-275-527-501-157-125-315-210-213-301-213-510-3_V.mp3)
  Segment 48: 137936ms ~ 138512ms (file: 360-306-136-316-106-610-306-103-136-361-316-160-302-312-103-130-160-361-103-312-167-162-230-132-621-123-312-621-362-120-132-103-130-312-130-165-103-361-130-615-136-321-321-316-1_W.mp3)
  Segment 49: 139512ms ~ 140088ms (file: 263-263-263-236-627-267-263-326-321-326-621-263-261-310-632-613-167-162-623-162-123-172-612-167-162-612-162-162 162-162-157_W.mp3)
  Segment 50: 141088ms ~ 141664ms (file: 317-736-652-376-657-567-752-573-357_W.mp3)
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
> **Time**: 2026-04-04 03:20:34
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "아니 월요일 날 종배 했는데 아침에 5~6% 갭하락 하는 거야",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "아니", "start_ms": 10, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "월요일", "start_ms": 200, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "날", "start_ms": 530, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "종배", "start_ms": 650, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "했는데", "start_ms": 940, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "아침에", "start_ms": 1300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "5~6%", "start_ms": 1660, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "갭하락", "start_ms": 2090, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 2480, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "거야", "start_ms": 2700, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "근데 나는 내가 내프콘에 저번주부터 계속 월 화 수 목 금 지수 관련주 모아가라고 계속 글도 쓰고 특강을 했거든",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "근데", "start_ms": 20, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "나는", "start_ms": 270, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "내가", "start_ms": 560, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "내프콘에", "start_ms": 790, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "저번주부터", "start_ms": 1220, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 1780, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "월", "start_ms": 2100, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "화", "start_ms": 2230, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 2350, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "목", "start_ms": 2470, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "금", "start_ms": 2590, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "지수", "start_ms": 2720, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "관련주", "start_ms": 2970, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "모아가라고", "start_ms": 3360, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 3890, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "글도", "start_ms": 4210, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "쓰고", "start_ms": 4460, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "특강을", "start_ms": 4700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "했거든", "start_ms": 5060, "dur_ms": 380, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "그날 대부분의 사람들은 광화문 공연 사진부터 보기 시작했습니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "그날", "start_ms": 30, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "대부분의", "start_ms": 250, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "사람들은", "start_ms": 680, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "광화문", "start_ms": 1070, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "공연", "start_ms": 1430, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "사진부터", "start_ms": 1680, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "보기", "start_ms": 2100, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "시작했습니다", "start_ms": 2330, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "드론과 높은 건물 위에서 찍힌 사진 속 광화문은 탁 트인 넓은 공간에 팬들이 여러 구역으로 나뉘어 서 있는 모습이었습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "드론과", "start_ms": 40, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "높은", "start_ms": 400, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "건물", "start_ms": 650, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "위에서", "start_ms": 940, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "찍힌", "start_ms": 1270, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "사진", "start_ms": 1560, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "속", "start_ms": 1800, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "광화문은", "start_ms": 1900, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "탁", "start_ms": 2330, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "트인", "start_ms": 2460, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "넓은", "start_ms": 2710, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "공간에", "start_ms": 2940, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "팬들이", "start_ms": 3300, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "여러", "start_ms": 3650, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "구역으로", "start_ms": 3900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "나뉘어", "start_ms": 4360, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "서", "start_ms": 4690, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 4810, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "모습이었습니다", "start_ms": 5030, "dur_ms": 680, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "언론이 컴백 전부터 사상 최대 인파 광화문이 보라색 바다로 물든다 라고",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "언론이", "start_ms": 30, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "컴백", "start_ms": 350, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "전부터", "start_ms": 640, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "사상", "start_ms": 1030, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "최대", "start_ms": 1270, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "인파", "start_ms": 1530, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "광화문이", "start_ms": 1820, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "보라색", "start_ms": 2280, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "바다로", "start_ms": 2640, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "물든다", "start_ms": 2990, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "라고", "start_ms": 3350, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "꿈꾸던 로봇을 만들기 위해 머릿속엔 온통 로봇으로 가득 차 있었습니다",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "꿈꾸던", "start_ms": 50, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "로봇을", "start_ms": 380, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "만들기", "start_ms": 740, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "위해", "start_ms": 1070, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "머릿속엔", "start_ms": 1290, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "온통", "start_ms": 1710, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "로봇으로", "start_ms": 1960, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "가득", "start_ms": 2390, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "차", "start_ms": 2650, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "있었습니다", "start_ms": 2780, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "결국 박혜원 교수가 만들어낸 대한민국 로봇 휴보는 모두 그의",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "결국", "start_ms": 40, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "박혜원", "start_ms": 330, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "교수가", "start_ms": 680, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "만들어낸", "start_ms": 1010, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "대한민국", "start_ms": 1470, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "로봇", "start_ms": 1900, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "휴보는", "start_ms": 2190, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "모두", "start_ms": 2550, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "그의", "start_ms": 2810, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "빵 먹으러 갈까 언제 먹을까",
"speaker": "speaker_4",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "빵", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "먹으러", "start_ms": 190, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "갈까", "start_ms": 510, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "언제", "start_ms": 760, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "먹을까", "start_ms": 980, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "그 와중에도 아침 식사를 챙겨주겠다고 빵을 반으로 자르더니 동생에게 줄 접시를 잡고",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "그", "start_ms": 20, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "와중에도", "start_ms": 110, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "아침", "start_ms": 500, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "식사를", "start_ms": 750, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "챙겨주겠다고", "start_ms": 1080, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "빵을", "start_ms": 1710, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "반으로", "start_ms": 1970, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "자르더니", "start_ms": 2320, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "동생에게", "start_ms": 2740, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "줄", "start_ms": 3170, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "접시를", "start_ms": 3300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "잡고", "start_ms": 3660, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "인사를 와서 제대로",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "인사를", "start_ms": 10, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "와서", "start_ms": 340, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "제대로", "start_ms": 600, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "그의 옆에는 늘 로봇이 함께 했는데요",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "그의", "start_ms": 20, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "옆에는", "start_ms": 240, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "늘", "start_ms": 570, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "로봇이", "start_ms": 690, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "함께", "start_ms": 1040, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "했는데요", "start_ms": 1300, "dur_ms": 380, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "제가 항상 로봇",
"speaker": "speaker_5",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "제가", "start_ms": 10, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "항상", "start_ms": 230, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "로봇", "start_ms": 480, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "눌렸는데 또 올라가고 눌렸는데 또 올라가고 또 여기도 이렇게 눌렸죠 아래꼬리 길게 달았는데요 눌렸는데 또 올라갔습니다",
"speaker": "speaker_6",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "눌렸는데", "start_ms": 50, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 440, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "올라가고", "start_ms": 570, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "눌렸는데", "start_ms": 1030, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 1420, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "올라가고", "start_ms": 1550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 2010, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "여기도", "start_ms": 2140, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 2470, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "눌렸죠", "start_ms": 2790, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "아래꼬리", "start_ms": 3140, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "길게", "start_ms": 3530, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "달았는데요", "start_ms": 3780, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "눌렸는데", "start_ms": 4310, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 4700, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "올라갔습니다", "start_ms": 4830, "dur_ms": 650, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-04 03:20:34*
