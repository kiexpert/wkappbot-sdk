# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=08:18:51  uptime=25177s  mem=1709MB  system_processes=449"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_081842.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 567 160-316-632-645-614 637-310-657_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 270-207-206-260-271-302-103-132-123-321-207-237-216-752-576-572-260-627-621-103-137-316-216-207-273-201-127-137-176-213-726-231-310-302-203-203-207-201-143-731-173-301-132-301-2_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 547-576-507-507 756-756-756-576-576-756-765_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 216-210-301-216 126-167-617-261-621-627-670-652-167-376-320-302-320-302-312-632-751-235-326-312-765 571-137-137-607-623-602-206-260-132-123-132-176-137-123-135-132-143-706-726-7_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 263-623-632-236 762-762-761-672-762-762_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 576-756-756-675-756-632-153-103-130-136-632-236-721-321-263-130-310-316-216-675-162-672-326-627-612-106-132-236-623-236-763-136-126-763-765-715-107-103-173-760-715-705-617-315-1_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 675-137-312-710-670-321-312-132 672-675-765-760-670-760-760-756-756-765-765-765-765-765-756-765-760-706-163-103-310-132-413-163-106-106-103-310-357-370-361-136-130-623-163-103-1_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 574-574-265-263-267-206-752-275-260-261-106-163-162-617-306-302-206-263-236-326-162-132-136-163-106-762-762-316-301-312-132-136-132-314-631-365-316-672-671-765-627-216-261-210-1_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 163-176-162-162 761-670-761-102-137-103-165-106-176-136-657-167-613-562-136-160-165-632-615-160-613-106-316-103-157-261-617-167-617-167-160-163-312-130-132-672-176-163-613-160-6_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 756-765-765-736 135-756-756-756-756-123 231 175-310-312-130_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 517-152-750-103-321-132-132-203-207-572-173-132-127-137-613-163-507-312-765-376-372-136-103-273-203-352-127-321-327-236-632-623-327-257-312-123-123-102-123-123-213-123-237-321-3_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 263-267-263-627-261-261-263-267-263-627-267-627-627-261-267-263-623-263-623-627-627-625-627-627-627-236-623-326-361-316-236-216-236-263-236-263-236-267-253-523-236-236-261-236-2_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 301-123-627-103 162-310-612-163-657-716-163-327-176-162-163-136-160-167-103-261-201-612-267-201-561-126-672-160-176-607-762-271-173-731-716-716-167-106-617-670-671-670-752-750-6_V.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 267-267-263-263-652-625-627-267-267-256-562-526-162-236-321-321-236-237-236-253-263-321-312-123-567-567-513-563-265-215-625-326-325-236-136-132-316-236-237-273-362-326-532-352-7_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 675-562-657-652-567-625-567-612-562-576-150-567-765-231-613-752-572-726-671-172-371-125-136-132-563-567-257-627-657-576-657-267-102-162-167-106-263-263-362-263 627-610-623 362-6_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 132-106-103-102-260-206-126-162-312-320-321-132-163-607-670-601-267-602-302-312-360-260-263-623-260-260-263-261-203-230-263-236-276-271-271-617-167-106-167-321-321-301-137-136-1_V.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 567-602-261-630-630-672-617-621-612-623-672-672-162-627-651-653-732-376-652 263-260-627-627-617-621-216-621-612-162-615-675-675-612-326-632-632-631-630-670-620-627-620-627-627-6_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 326-302-670-301-362-362-261-132-163-103-136-310-301-163-316-126-312-356-302-320-302-301-316-316-132-130-301-136-103-103-321-231-321-310-135-103-135-132-103-361-301-326-162-263-2_W.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 103-216-102-601-620-260-206-253-251-236-301-367-376-673-760-670-627-260-627-602-135-102-163-103-510-163-327-261-130-621-627-312-132-673-602-607-607-601-617-367-361-754-132-236-2_V.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 230-203-230-320-312-576-706-106-106-162-132-302-236-302-306-263-312-102-326-263-362-123-231-123-132-102-306-360-362-302-260-206-260-263-623-576-235-306-260-623-103-301-351-625-2_W.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 601-167-157-162-671-615-637 136-312-136-620-162-576-576-615-635-106-316-162-136-173-312-672-163-671-621-712-126-132-103-132-103-623-106-162-216-167-132-106-130-103-761-132-176-1_V.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 375-372-326-620-375-375-573-751-175-175-357-573-376-263-623-652-562-361-361-163-152-152-263-253-527-527-524-547-574-253-256-265-276-615-367-264-260-264-625-267-267-236-562-352-2_W.mp3)
  Segment 23: 34672ms ~ 51232ms (file: 571-750-570-750-756-765-725-275-572-726-167-765-152-632-172-167-172-761-257-152-167-103-175-167-134-105-265-125-261-162-751-601-572-126-610-167_V.mp3)
  Segment 24: 52232ms ~ 52808ms (file: 257-352-576-137-103-136-136-760-761-316-103-130-103-102-310-513-163-326-312-320-320-302-320-260-326-326-627-673-362-306-301-306-136-367-362-632-762-362-326-320-302-321-312-312 1_W.mp3)
  Segment 25: 53808ms ~ 54384ms (file: 256-302-320-301-361-213-132-123-103-126-621-263-351-537-357-602-672-627-602-163-136-132-312-531-603-136-132-103-132-126-132-326-320-302-302-103-123-132-106-103-310-163-136-162-1_V.mp3)
  Segment 26: 55384ms ~ 55960ms (file: 105-135-321-265-617-167-362-735-173 130-162-670-173-617-613-167-576-765-761-173-107-765-107-517-125-167-163-126-167-167 136-312-130_W.mp3)
  Segment 27: 56960ms ~ 57536ms (file: 123-312-162-106-260-261-203-201-132-132-107-576-316-123-167-132-103-120-261-206-231-231-307-362-103-103-135-235-325-376-213-123-570-365-132-312-327-317-321-317-302-317-301-123-3_W.mp3)
  Segment 28: 58536ms ~ 59112ms (file: 362-326-236-321-137-317-137-127-276-756-753-725-726-273-721-132-123-127-751-572-752-327-376-546-574-573-325-253-235-235-357-157-315-673-673-673-637-760-376-356-256-362-327-367-3_W.mp3)
  Segment 29: 60112ms ~ 60688ms (file: 172-163-107-132-130-167-176-107-106-106-576-263-172-175-731-607-601-167-103-132-163-136-162-176-103-163-132-175 106-162-136-126-261-172-612-753-165-167-163-103-136-637-103-135-1_V.mp3)
  Segment 30: 61688ms ~ 62264ms (file: 164-136-130-130-103-130-130-762-760-627-312-630-136-167-160-176-317-103-103-130-103-163-136-163-162-136-310-136-316-601-620-671-607-657-160-613-163-167-316-213-162-613-361-613-6_V.mp3)
  Segment 31: 63264ms ~ 63840ms (file: 627-130-136-136-756-310-136-103-653-130-103-103-130-176-316-156-165-564-576-576-150-157-176-547-574-574-756-765-712-172-765-362-206-162-160-765-756-756-657-675-134-134-136-570-7_W.mp3)
  Segment 32: 64840ms ~ 69448ms (file: 310-130-102-231-320-320-752-276-206-206-205-250-206-230-325-372-753-136-130-307-371-236-263-260-260-206-261-206-527-201-312-103-103-570-310-153-306-302-103-310-321-123-263-236-2_V.mp3)
  Segment 33: 70448ms ~ 71024ms (file: 265-261 261 672-672-627-165-130-132-267-260-103-301-503-526-316-627-621-541-132-364-310-137-173-163-103-103-130-310-130-310-316-317-134-137 735-163-621-312-673-326 632-632-607-6_V.mp3)
  Segment 34: 72024ms ~ 72600ms (file: 106-163-316-106-513-150-103-670-670-576-756-326-367-326-362-632-260-602-260-623-130-103-267-263-527-516-217-123-321-542-574-357-310-632-236-260-263-203-203-102-631-132-732-102-1_V.mp3)
  Segment 35: 73600ms ~ 74176ms (file: 572-136-107-136-106-136-103-127-651-162-103-165-167-126-170-107-163-103-136-627-623-621-672-136-163-176-176-103-130-236-236-103-312-103-210-516-623-267-267-162-653-651-167 621-6_V.mp3)
  Segment 36: 75176ms ~ 75752ms (file: 761-671-671-216-132-136-210-671-610-162-627-261-610-612-162-103-160-361 265-263-267-263-263-126-362-263-230-526-625-263-236-623-123-675-126_W.mp3)
  Segment 37: 76752ms ~ 77328ms (file: 136-576-173-231-126-162-136-103-167-316-316-126-163-163-136-103-361-167-316-623-361-136-162-613-316-106-106-130-137-217-160-261-612-316-163-312-261-231-130-257-316-163-136-321-6_V.mp3)
  Segment 38: 78328ms ~ 78904ms (file: 213-132-175-723-276-321-672-136-627-672-267 657-671-672-672-213-132-103-316-623-326 617-160-163-123-312-163-321-132-312-312-321-312-321-321-216-321-236-261-623-261-627-276-276-2_W.mp3)
  Segment 39: 79904ms ~ 80480ms (file: 675-627-106-165-612-160-103-610-163-162-361-176-103-106-623-623-136-136-132-137-103-132-162-671 132-134 576-756-675-765-765-267-267-725-765-762-316-617-316-231-612-625-256 632-2_V.mp3)
  Segment 40: 81480ms ~ 82056ms (file: 736-761-671-176-217-267-627-137-716-265-267-276-267-276-267-263-263-762-675-765-762-267-762-267-176-317-263-236-327-367-376-327-371-762 672-627-627-762-267-267-267-267-524-542-5_W.mp3)
  Segment 41: 83056ms ~ 83632ms (file: 312-163-316-301-132-570-756-307-230-203-206-320-103-136-136-137-206-236-621-176-137-103-301-132-312-203-524-540-543-350-305-320-320-301-103-310-312-132-136-230-320-302-534-352-3_V.mp3)
  Segment 42: 84632ms ~ 85208ms (file: 517-651-160-612-162-103-106-162-610-260-230-203-260-725-731-715-370-375-702-750-753-721-706-726-726-712-375-765-765-672-306-136-312-321-235-537-273-307-750-756-756-576-756-576-7_W.mp3)
  Segment 43: 86208ms ~ 86784ms (file: 612-263-213-451-163-143-175 162-136 162-132-267 167-326-167-130-157-657 136-136-103-106-130-136-106-165-106-160-106-213-132-201-120-102-617-651-126_V.mp3)
  Segment 44: 87784ms ~ 88360ms (file: 136-157-105-326 263 715-316-567-671-516-756-120-135-102-103-103-106-106-160-106-106-160-162-160-167-216-163-751-753-623-162-126-617-615-160-163-163-765-571-576-106-167-160-160-1_W.mp3)
  Segment 45: 89360ms ~ 89936ms (file: 576 362 103-136-163-751-157-672-167-106-152-617-103-103-165-675-361-163-136-103-170-163-165-106-613-106-103-163-106-160-165-136-631-103-103-613-167-716-106-167-125-632-176-162 1_V.mp3)
  Segment 46: 90936ms ~ 91512ms (file: 750-761-761-756-765-756-765-760-762-571-765-765-671-156 172-312 671-167_W.mp3)
  Segment 47: 92512ms ~ 93088ms (file: 321-625-627 621-620-260-621-263-236-136-631-103-176-106-261-230-623-265-362-321-163-632-136-621-617-317-316-601-132-126-301-163-136-132-162-106-136-610-167-136-521-163-136-103-1_V.mp3)
  Segment 48: 94088ms ~ 94664ms (file: 627-276-267-267-672-627-623-623-261-216-261-265-325-132-621-236-632-623-627-621-612-621-267-263-267-627-623-623-162-623-261-623-263-621-126-176-756-672-267-627-627-623-263-623-6_W.mp3)
  Segment 49: 95664ms ~ 96240ms (file: 675 672-627-617-621-672-675-617-167-362-102-103-265-206-267-206-260-160-621-263-260-213-213-261-231 206-206-216-765-175 106-301-213-263-621-753-371-370-627-627-326-310-627-627-6_W.mp3)
  Segment 50: 97240ms ~ 97816ms (file: 123-103-312-753-751-163-103-213-201-231-123-132-312-103-103-132-136-510-150-106-103-136-163-132-136-132-612-576-574-576-657-165-102-312-103-132 132-176-156-507-752-726-321-132-6_V.mp3)
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
> **Time**: 2026-04-03 08:19:47
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "오늘 배운 문법을 활용해서 대화를 완성해 봅시다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "배운", "start_ms": 160, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "문법을", "start_ms": 290, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "활용해서", "start_ms": 430, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "대화를", "start_ms": 590, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "완성해", "start_ms": 720, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "봅시다.", "start_ms": 860, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "저는 한국 음식을 먹는 것을 정말 좋아해요.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 30, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "한국", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "음식을", "start_ms": 280, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "먹는", "start_ms": 430, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "것을", "start_ms": 550, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "정말", "start_ms": 660, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "좋아해요.", "start_ms": 780, "dur_ms": 190, "phase_lr_deg": 5.0}
]
},
{
"segment": 3,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 4,
"transcript": "가장 좋아하는 한국 음식은 무엇인가요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "가장", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "좋아하는", "start_ms": 170, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 310, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "음식은", "start_ms": 430, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "무엇인가요?", "start_ms": 570, "dur_ms": 150, "phase_lr_deg": 0.0}
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
"transcript": "저는 비빔밥을 제일 좋아해요.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "비빔밥을", "start_ms": 150, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "제일", "start_ms": 310, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "좋아해요.", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": 5.0}
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
"transcript": "건강에도 좋고 맛도 훌륭하죠.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "건강에도", "start_ms": 60, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "좋고", "start_ms": 200, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "맛도", "start_ms": 290, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "훌륭하죠.", "start_ms": 410, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "직접 요리해서 드시기도 하나요?",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "직접", "start_ms": 35, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "요리해서", "start_ms": 155, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "드시기도", "start_ms": 305, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "하나요?", "start_ms": 445, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "가끔 집에서 만들어 보는데 좀 어려워요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "가끔", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "집에서", "start_ms": 165, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "만들어", "start_ms": 295, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "보는데", "start_ms": 415, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "좀", "start_ms": 535, "dur_ms": 40, "phase_lr_deg": 5.0},
{"word": "어려워요.", "start_ms": 585, "dur_ms": 140, "phase_lr_deg": 5.0}
]
},
{
"segment": 11,
"transcript": "자주 하다 보면 더 맛있어질 거예요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "자주", "start_ms": 30, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "하다", "start_ms": 150, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "보면", "start_ms": 250, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 370, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "맛있어질", "start_ms": 430, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "거예요.", "start_ms": 590, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
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
"transcript": "오늘 수업도 참 즐거웠습니다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 55, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "수업도", "start_ms": 175, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "참", "start_ms": 305, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "즐거웠습니다.", "start_ms": 385, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "다음에 또 유익한 시간 가져봐요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 25, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 145, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "유익한", "start_ms": 215, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "시간", "start_ms": 345, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "가져봐요.", "start_ms": 465, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 17,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 18,
"transcript": "모두 고생 많으셨습니다. 안녕히 가세요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "모두", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "고생", "start_ms": 160, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "많으셨습니다.", "start_ms": 280, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 500, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "가세요.", "start_ms": 650, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "안녕히 계세요 선생님.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "계세요", "start_ms": 200, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "선생님.", "start_ms": 350, "dur_ms": 150, "phase_lr_deg": 5.0}
]
},
{
"segment": 20,
"transcript": "조심히 들어가고 또 봐요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "조심히", "start_ms": 60, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "들어가고", "start_ms": 210, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 370, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "봐요.", "start_ms": 440, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "바이 바이.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "바이", "start_ms": 35, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "바이.", "start_ms": 205, "dur_ms": 371, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 23,
"transcript": "안녕.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "안녕.", "start_ms": 45, "dur_ms": 531, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 25,
"transcript": "진짜 안녕.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 30, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "안녕.", "start_ms": 180, "dur_ms": 396, "phase_lr_deg": 0.0}
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
"transcript": "다음에 봐요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 50, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "봐요.", "start_ms": 220, "dur_ms": 356, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "행복하세요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "행복하세요.", "start_ms": 40, "dur_ms": 536, "phase_lr_deg": 0.0}
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
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "바이.", "start_ms": 25, "dur_ms": 551, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "진짜 진짜 안녕.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 60, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 210, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "안녕.", "start_ms": 360, "dur_ms": 216, "phase_lr_deg": 0.0}
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
*Generated by WKAppBot ask gemini — 2026-04-03 08:19:47*
