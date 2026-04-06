# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=08:15:30  uptime=24976s  mem=1690MB  system_processes=447"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_081520.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 126-521-623 765-627-163-203-610-316-320-756-756-627-321-132-265-263-623-263-267 657-576-562-652-613-312-302-301-312-320-326-306-321-213-312-321-320-310-213-216-236-623-673-267-2_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 756-765-576-567-653-573-547-574-574-567-756-367-523-562-537-325-321-362-315-256-257-230-236-235-250-253-235-532-352-253-567-357-607-532-235-265-263-260-236-260-206-236-362-362-3_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 312-627-231-201-103-316-106-103-132-320-302-312-756-675-320-302-130-301-103-310-301-136-163-163-103-106-576-576-670-610-106-137-132-231-213-652-623-160-103-301-213-132-231-216-2_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 726-732-276-237-267-527-256-162-123-163-267-625-326-213-576-217-263-267-625-726-127-263-612-267-261-263-263-312-612-123-216-216-126-216-261-627-261-216-216-261-216-132-236-261-3_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 756-756 631-126-637-361-756-751-765-756-756-756-765-765-716-765-765-751-175-715-276-621-623-123-132-312 160-136-102-526-126-102-126-103-132-106-150-561-261-162-103-160-162-671-6_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 301-315-315-163-310-617-317-321-321-321-312-312-132-540-761-765-761-617-612-627-754-106-126-162-201-231-125-231-312-320-261-263-623-201-213-236-236-123-621-623-261-621-267-261-2_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 263-256-625-641-671-641-306-623-263-206-260-260-260-162-106-163-206-621-126-607-672-621-672-675-765-675-672-760-610-601-601-602-620-620-761-675-671-675-106-612 621-260-261-256-2_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 516-312-315-103-106-162-132-130-316-103-574-125-632-360-312-360-617-103-132-103-103-617-103-312-613-123-106-103-126-162-165-136-671-136-106-106-106-561-710-657-263-106-106-756 1_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 675-367-630-321-301-132-576-657 132-132-705-175-173-312-312-321-376-756-675-317-576-753-507-176-762-715-167-735-576-567-567-576-576-576-156-132-517-576-652-356-567-317-316-301-1_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 260-267-620-106-106-103-617-617-160-106-160-106-130-136-167-176-176-761-765-167-765-730-617-627-675-132-620-612-163-106-312-103-316-103-103-103-102-103-103-136-132-163-267-267-2_V.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 635-126-106-176-103-312-103-613-136-312-103-160-132-361-126-123-267-236-102-163-615-137-106-103-106-163-167-615-160-631-137-617-106-106-103-130-162-651-623-613-106-123-306-716-1_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 130-132-317-130-137-103-103-130-160-130-267-267-103 132-123-130-310-130-130 312-316-301-316-163-160-162-236-206-321 312-316-136-310-130-312-163-136-163-136-103-103-316-130-103-1_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 612-312-623-632-361-312-623-231-612-765-672-672-612-165-627-623-623-621-263-321-327-163-136-163-167-213-215-251-213-352-362-367-367-263-316-362-365-531-563-365-326-312-231-261-2_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 312-236-326-263-261 670-761-716-756-163-765-675-765-671-761-167-621-612-612-167-163-316-136-316-136-312-216-576-536-573-672-561-576-126-162 761 763 163-631-163-163-715-371-756-6_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 567-126-162-576-576-576-576-567-576-567-576-567-576-567-567-567-576-576-567-567-756-576-263-103-263-126-576_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 760-263-623-132-312-132-231-103-301-362-632-756-765 263-123-756-572-756-675_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 326-321-324-254-527-325-731-317-137-175-157-175-125-167-132-263-627-617-627-362 106-163-236-671-576-132-135-130-123-130-267-263-267-312-126-126-716-762-765-765-706-760-170-672-3_V.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 756-765-126-210-123-236 657-657 312 135-625-321-623-623-657-652-567-567-657-617-601-625-671-657-163-215-613-657-657-617-617-631-657-632-362-632-120-362-261-320 170-516-136-617-6_W.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 236-765-726-670-765-705-761-657-670-765-670-576-756-765-756-705-705-750-163-716-637-637-637-367-765-736-762-736-762-762-762-763-763-716-672-671-761-672-762-763-127-716-762-762-7_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 631-136-621-236-527-547-574-513-521-562-625-625-627-310-301-531-547-547-572-235-235-260-230-206-627-627-273-216-126-210-132-312-316-216-231-167-136-135-574-517-567-265-574-571-5_W.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 362-312-321-312-362-236-362 326-237-327-123-651-167-175-157-574-574-576-576-576_W.mp3)
  Segment 22: 33096ms ~ 40296ms (file: 574-567-765-756-756-765-752-675-762-160-126-567-675-672_W.mp3)
  Segment 23: 41296ms ~ 41872ms (file: 235-257-325-307-260-236-753-623-672-675-675-167-673-603-326-637-150-132-312-371-302-132-260-562-231-123-102-352-153-135-670-123-715-761-103-103-173-261-162-136-103-301-103-126-1_W.mp3)
  Segment 24: 42872ms ~ 43448ms (file: 627-621-326-263-236-126-321-321 623-263-236-506-625-567-265-267-621-260-236-103-130-310-103-310-310-103-756-761-756-765-675-671-675-621 726-627-267-260-267-263-620-632 362-231-3_W.mp3)
  Segment 25: 44448ms ~ 45024ms (file: 602-135-315-532-321-321-321-327-326-267-213-320-263-123-231-132-263-321-132-132-132-163-260-236-206-230-123-103-312-123-165-576-507-361-163-156-316-671-231-320-352-135-312-135-1_V.mp3)
  Segment 26: 46024ms ~ 46600ms (file: 301-120-263-206-651-260-206-621-765-307-312-260-326-376-237-132-132-317-362-260-260-260-621-106-632-261-263-203-267-301-103-132-103-132-312-310-735-173-137-172-132-123-162-754-1_V.mp3)
  Segment 27: 47600ms ~ 48176ms (file: 265-672-365-372 103-132-301-306-132-126_V.mp3)
  Segment 28: 49176ms ~ 49752ms (file: 631-623-326-316-321-321-351-536-562-256-257-625-256-126-231-612-236-326-263-326-231-316-361-316-362-312-351-315-162-156-326-357-325-321-231-352-357-516-563-315-517-537-352-572-5_W.mp3)
  Segment 29: 50752ms ~ 51328ms (file: 632-136-302-130-263-620-203-325-620-623-627-672 710-123 130-130-120_W.mp3)
  Segment 30: 52328ms ~ 52904ms (file: 547-765-176-312-705-756-765-706-675-673-326-376-341 675-627-627-260 671-160-631-637-612-164-132-371-634-672-312-361-316 123-312-132-312-320-321-127-326-617-613-763-610-321-126-1_V.mp3)
  Segment 31: 53904ms ~ 54480ms (file: 326-267-765-756-672-632-602-620-163-102-162-320-321-763-753-731_V.mp3)
  Segment 32: 55480ms ~ 56056ms (file: 306-136-317-136-136-672-726-123-263-237-213-213-261-267-672-762-765-760-567-657-657-567-657-607-163-321-320-320-320-623-576-130-103-103-103-163-361-765-764-143-674-621-261-126-1_W.mp3)
  Segment 33: 57056ms ~ 57632ms (file: 361-312-362-326-361-321-312-362-316-361-362-361-362-632-326-321-621-316-361-236-362-132-321-321-312-132-312-132-312-316-312-312-316-312-132-312-312-132-312-316-312-316-136-316-1_W.mp3)
  Segment 34: 58632ms ~ 59208ms (file: 624-261-213 326-623-672-123-613-617-130-563-351-625-612-316-672-160-136-627-360-613-160-130-163-103-103-132-632-216-163-172-103-130-103-103-163-761-126-103-126-132_W.mp3)
  Segment 35: 60208ms ~ 60784ms (file: 162-321-261-623-612-631-106-123-617-136-316-162-521-126-517-671-312-316-163-613-165-103-263-136-310-567-132-126 103-675-632-163-567-163-612-136-163-103-617-671-163-160-167-107-1_V.mp3)
  Segment 36: 61784ms ~ 62360ms (file: 627-267-627-236-216-627-756-163-123-216-132-675-672-627-672-675_W.mp3)
  Segment 37: 63360ms ~ 63936ms (file: 306-306-632-356-261-132-306-326-321-326-503-365-375-267-260-206-261-106-261-260-206-260-163-316-632-267-574-754-763-763-107-137-103-613-267-672-567-572-106-173-136-132-675-672-6_V.mp3)
  Segment 38: 64936ms ~ 65512ms (file: 271-217-213-321-123-512-217-123-127-273-217-127-172-127-172 574-756-732-321-321-321-321-231-321-321-153-725-267-235-123-321-123-123-123-126-123-126-153-157-157-571-537-157-573-7_W.mp3)
  Segment 39: 66512ms ~ 67592ms (file: 756-675-672-673-675-675 675-576-765-765-765-675-675-672-672-765-672-765-762-672-675-167-613-576-576-672-562-576-576-752 612-362 613-263-261-672-621-632-631-672-671-631-765-627-7_W.mp3)
  Segment 40: 68592ms ~ 69168ms (file: 216-621-263-216-162-236-263-261-301-576-637-316-326-316-236-310-130-321-316-132-261-320-132-231-321-132-102-106-162-123-236-613-106-620-623-203-320-263-623-260-206-102-230-320-3_V.mp3)
  Segment 41: 70168ms ~ 70744ms (file: 706-764-670-762-670-620-620-672-267-203-210-326-370-321-231-201-320-236-126-120-176-126-760-607-620-267-316-376-301-765-706-756-765-765-761-762-627-260-320-236-320-307-706-736-7_W.mp3)
  Segment 42: 71744ms ~ 72320ms (file: 326-325-327-302-367-326-302-326-627-627-206-203-312-301-301-206-260-206-620-267-130-310-136-576-326-361-316-326-321-136-612-617-261-163-203-136-157-136-103-530-316-627-267-302-2_V.mp3)
  Segment 43: 73320ms ~ 73896ms (file: 301-203-301-316-301-501-531-321-130-136-306-307-371-136-163-136-103-706-756-765-132-306-136-135-165-126-136-613-106-316-156-267-765-165-517-165-103-123-607 167-167-760-706-762-7_W.mp3)
  Segment 44: 74896ms ~ 75472ms (file: 132-132-371-731-136-516-567-602-607-602-607-671-673-106-231-103-672-627-602-612-162-103-136-132-106-627-123-203-201-206-261-630-316-207-302-320-321-263-637-352-627-621-623-321-3_W.mp3)
  Segment 45: 76472ms ~ 77048ms (file: 627-263-216-621-136-621-162-126-132-652-126-761-752-167-165-752-160-623-267-621-126-162-216-612-276-216-216-602-623-106-362-160-157-312-362-176-136-103-314-136-572-657-576-617-1_V.mp3)
  Segment 46: 78048ms ~ 78624ms (file: 106-237-621-621-126-136-103-521-362-567-106-103-236-231-312-627-620-617-631-260-260-620-620-602-361-136-163-136-106-143-360-603-607-607-461-206-524-602-621-603-160-163-163-672-7_V.mp3)
  Segment 47: 79624ms ~ 80200ms (file: 726-217-127-312-106-126-127-172-126-132-103-617-623-623-163 627-716-627-621-612-620-601-106-103-106-765-136-365-175-126-137-163-176-671-671-106-106-620-167-762 102-103-165-106-2_V.mp3)
  Segment 48: 81200ms ~ 81776ms (file: 672-302-312-132-123-103-301-103-132-312-301-106-103-621-206-261-261-617-136-103-320-320-302-326-302-320-321-103-103-136-106-316-260-617-163-765-760-160-167-103-320-230-210-621-6_W.mp3)
  Segment 49: 82776ms ~ 83352ms (file: 617-316-316-320-320-320-321-130-316-301-310-310-301-103-310-103-130-302-320-236-230-320-320-352-532-537-502-302-302-312-132-316-132-103-106-103-103-602-627-360-302-130-310-130-1_W.mp3)
  Segment 50: 84352ms ~ 84928ms (file: 163-312-316-312-173-137-173-132-132-136-301-312-320-371-321-315-312-301-312-321-302-352-356-326-123-210-123-670-132-302-235-562-625-756-576-576-162_W.mp3)
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
> **Time**: 2026-04-03 08:16:27
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "오늘 배운 내용을 바탕으로 대화를 나눠봅시다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "배운", "start_ms": 160, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "내용을", "start_ms": 290, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "바탕으로", "start_ms": 430, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "대화를", "start_ms": 590, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "나눠봅시다.", "start_ms": 720, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "저는 한국 문화를 더 깊이 이해하고 싶어요.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 30, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "한국", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "문화를", "start_ms": 280, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "더", "start_ms": 430, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "깊이", "start_ms": 500, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "이해하고", "start_ms": 620, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "싶어요.", "start_ms": 780, "dur_ms": 180, "phase_lr_deg": 5.0}
]
},
{
"segment": 3,
"transcript": "문화와 언어는 뗄 수 없는 관계죠.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "문화와", "start_ms": 55, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "언어는", "start_ms": 195, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "뗄", "start_ms": 325, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 375, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "없는", "start_ms": 415, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "관계죠.", "start_ms": 515, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "한국 전통 의상인 한복에 대해 알고 계신가요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "한국", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "전통", "start_ms": 165, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "의상인", "start_ms": 295, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "한복에", "start_ms": 435, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "대해", "start_ms": 585, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "알고", "start_ms": 705, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "계신가요?", "start_ms": 815, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 6,
"transcript": "네 한복은 색깔이 아주 곱고 아름다워요.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 35, "dur_ms": 50, "phase_lr_deg": 5.0},
{"word": "한복은", "start_ms": 95, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "색깔이", "start_ms": 245, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "아주", "start_ms": 385, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "곱고", "start_ms": 495, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "아름다워요.", "start_ms": 615, "dur_ms": 200, "phase_lr_deg": 5.0}
]
},
{
"segment": 7,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 8,
"transcript": "특별한 날에 입는 옷이라는 것도 알아요.",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "특별한", "start_ms": 40, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "날에", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "입는", "start_ms": 290, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "옷이라는", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "것도", "start_ms": 560, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "알아요.", "start_ms": 650, "dur_ms": 140, "phase_lr_deg": 5.0}
]
},
{
"segment": 9,
"transcript": "맞아요 명절이나 결혼식 때 주로 입죠.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "맞아요", "start_ms": 25, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "명절이나", "start_ms": 155, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "결혼식", "start_ms": 315, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 455, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "주로", "start_ms": 515, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "입죠.", "start_ms": 635, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "기회가 되면 직접 한번 입어보고 싶네요.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "기회가", "start_ms": 50, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "되면", "start_ms": 190, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "직접", "start_ms": 300, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "한번", "start_ms": 420, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "입어보고", "start_ms": 540, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "싶네요.", "start_ms": 690, "dur_ms": 150, "phase_lr_deg": 5.0}
]
},
{
"segment": 11,
"transcript": "나중에 한국에 오시면 꼭 체험해 보세요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "나중에", "start_ms": 35, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "한국에", "start_ms": 175, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "오시면", "start_ms": 305, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "꼭", "start_ms": 445, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "체험해", "start_ms": 515, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "보세요.", "start_ms": 665, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "네 꼭 그렇게 하고 싶어요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 60, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "꼭", "start_ms": 130, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "그렇게", "start_ms": 200, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "하고", "start_ms": 340, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "싶어요.", "start_ms": 450, "dur_ms": 150, "phase_lr_deg": 5.0}
]
},
{
"segment": 13,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 14,
"transcript": "오늘 수업도 정말 보람 있었네요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "수업도", "start_ms": 160, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 300, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "보람", "start_ms": 420, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "있었네요.", "start_ms": 520, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "질문 있으시면 언제든 편하게 물어보세요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "질문", "start_ms": 25, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "있으시면", "start_ms": 145, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "언제든", "start_ms": 295, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "편하게", "start_ms": 435, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "물어보세요.", "start_ms": 585, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 17,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 18,
"transcript": "오늘 배운 내용 꼭 복습해 보세요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "배운", "start_ms": 165, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "내용", "start_ms": 295, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "꼭", "start_ms": 415, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "복습해", "start_ms": 485, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "보세요.", "start_ms": 635, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "네 알겠습니다 감사합니다.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 30, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "알겠습니다", "start_ms": 100, "dur_ms": 180, "phase_lr_deg": 5.0},
{"word": "감사합니다.", "start_ms": 290, "dur_ms": 210, "phase_lr_deg": 5.0}
]
},
{
"segment": 20,
"transcript": "다음 수업 때 뵙겠습니다.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "다음", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "수업", "start_ms": 170, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "때", "start_ms": 280, "dur_ms": 50, "phase_lr_deg": 5.0},
{"word": "뵙겠습니다.", "start_ms": 340, "dur_ms": 240, "phase_lr_deg": 5.0}
]
},
{
"segment": 21,
"transcript": "안녕히 가세요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 60, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가세요.", "start_ms": 220, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 23,
"transcript": "바이 바이.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "바이", "start_ms": 40, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "바이.", "start_ms": 210, "dur_ms": 340, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "고생하셨습니다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "고생하셨습니다.", "start_ms": 35, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "진짜 안녕.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 45, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "안녕.", "start_ms": 185, "dur_ms": 391, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 27,
"transcript": "다음에 또 봐요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 30, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 200, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "봐요.", "start_ms": 290, "dur_ms": 286, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "행복하세요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "행복하세요.", "start_ms": 50, "dur_ms": 526, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 30,
"transcript": "바이.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "바이.", "start_ms": 60, "dur_ms": 516, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "진짜 진짜 안녕.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 190, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "안녕.", "start_ms": 340, "dur_ms": 236, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 33,
"transcript": "수고.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "수고.", "start_ms": 25, "dur_ms": 551, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "고생.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "고생.", "start_ms": 55, "dur_ms": 521, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 36,
"transcript": "안녕히.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "안녕히.", "start_ms": 35, "dur_ms": 541, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 38,
"transcript": "잘 가.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "잘", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "가.", "start_ms": 165, "dur_ms": 411, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "끝.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "끝.", "start_ms": 30, "dur_ms": 546, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 41,
"transcript": "진짜 끝.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "끝.", "start_ms": 200, "dur_ms": 376, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "바이.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "바이.", "start_ms": 40, "dur_ms": 536, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "안녕.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "안녕.", "start_ms": 60, "dur_ms": 516, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 45,
"transcript": "수고.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "수고.", "start_ms": 35, "dur_ms": 541, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "고생.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "고생.", "start_ms": 45, "dur_ms": 531, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 48,
"transcript": "바이 바이.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "바이", "start_ms": 25, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "바이.", "start_ms": 195, "dur_ms": 351, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 50,
"transcript": "진짜 끝.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "끝.", "start_ms": 190, "dur_ms": 386, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 08:16:27*
