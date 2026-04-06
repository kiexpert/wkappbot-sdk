# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=08:27:37  uptime=25702s  mem=1837MB  system_processes=447"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_082600.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 176-107-103-176-106-617-136-312-132-136-621-271-162-137-167-107-761-167-567-107-106-361-615-165-163-163-630-162-103-765-175-127-512-167-162-623-167_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 136-562-106-621-620-637 361-263 675 126-620-620-620-620-260-620-620-567-576-672-672-167-167-160-136-576-567-576-765-657-756-765-675-675-675-657-652-765-765-672-627-765 413-132-6_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 126-312-762-637-763 310-362-361-316_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 163-126-321-106-627-613-621 613-361-132-612-102-136-615-127-316-103-716-136-316-325-167-126 675-675-167-163-103-160-167-657-106-610-162-167-136-176-162-103-106-216-103-561-126-1_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 231-316-167-162-120-312-163-132-270-127-132-163-126-167-123-176-157-136-123-261-362-316-613-132-126-312-162-321-136-213-621-213-312-126-132-571-156-126-617-102-120-123-132-361-3_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 765 715-123-267-765-675-627-627-672-567-267-326 756-570 765-627-672-603-316-126 302-213 236-273-237-715-761-761-162-612-623-302-530-637-573-753-723 312-601-547-675-267-276-207-5_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 751-132-621-123 765-236-132-624-107 162-310-137-163-107-136-163-103-316-130-130-103-612-162-601-716-165-765-136-517-726-167-163-612-621-617-617-106-103-132-156-316-106-162-167-1_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 615-361-216-132 134-136-132-126-617-162-126-623-162-126-132-126-615-657 716_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 625-132-237-625-756-765-761-621-362-263-213-623-236-623-267-265-625-621-562-632-652-623-361-627-657-632-263-652-365-325-563-236-736-326-236-263-162-230-326-672-675-765-761-563-6_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 312-376-762-571-356-316-136-315-132-315-326-324-316-362-321-135-361-326 623_V.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 632-261 310-312-321-130-316-621-612-152-126-160-612-160-106-167-103-615-106-157-176-610-671-673-617-613-160-103-160-657-167-103-127-176-162-621-163-103-106-106-132-612-326-312-1_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 216-631-123-623-362-360-326 675-672-672-756-751-705-312-132-167-217-124 162-163-627-620-620-602-306-132-136-103-301-570-103-127-602-162-102-310-103-132-532-547-510-105-106-160-3_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 672-607-632-362-326-126-306-103-713-103-106-130-260-301-607-620-602-136-621-617-275-627-670-672-760-167-162-615-670-765-675-675-675-617-675-765-760-706-176-671-607-617-167 125-7_V.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 130-321-132-126 567-567-564-574-547-571-547-567-574_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 132-231-231-261-263-267-267-261-265-135-513-571-517-756-675-675-132-261-576-576-716-612-165-756-327-237-231-235-321-235-263-321-213-132-361-123-157-571-157-312-123-175-756-765-7_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 132-120-102-126-163-103-201-203-236-103-123-103-102-103-126-167-675-765-607-163-632-126-132-320-203-231-203-123-132-201-210-216-253-261-321-312-321-302-607-670-607-273-602-627-2_V.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 670-716-310-162-326-130-620-576-103-376-302-103-617-157-256-260-103-160-162-123-312-312-126-627-102-120-321-162-217-312-536-635-612-315-103-602-610-610-103-126-162-612-160-601 1_V.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 102-321-302-310 627-627-756-756-701-106-130-103-301-312-310-236-543-570-572-623-362-362-316-310-103-173-132-167-613-312-321-213-123-103-132-162-612-310-130-310-130-163-162-106-1_V.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 756-576-567-625-263-325-312-315-537-571-153-571-574-572-576-132-235-325-235-236-325-235-501-526-526-540-513-621-261-517-260-265-621-260-620-602-564-267-267-265-263-236-623-623-6_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 765-761-762 765-716-736-231-236-625-627-362-136-263-216-627-267-625-126-672-267-627-236-567-627-621-267-263-621-236-326 103-106-103-316-136_W.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 263-231-231-213-163-210-123-231-263-263-216-621-672-136-132-312-612-261-263-103-132-213-210-263-130-103-153-326-165 106-137-621-103-103-132-601-602-765-671-632-621-612-601-176-1_V.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 562-625-260-275-273-123-132-172-671-671-623-360-136-103-106-301-130-543-163-106-106-167-316-301-320-236-320-123-132-106-236-206-167-627-631-310-302-623-371-130-306-167-102-132-1_V.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 260-261-621-263-327-236-504-260-237-320-301-136-163-103-612-126-163-362-302-302-310-312-302-203-316-301-574-576-567-506-576-627-106-160-160-160-106-136-106-106-326-326-326-230-3_W.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 132-167-135-137-162-102-103-103-106-106 136-136-106-601-163-165-103-610-672-623-231-231-301-106-126-326-132-103-160-612-106-136-127-123-103-126-176-521-103-162-132-163-620-162-1_V.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 265-261-612-216-627-261-562-562-325-361-213-316-123-132-521-512-521-562-216-126-126-126-162-261-126-216-326-621-162-126-216-126-612-216-162-216-623-163-361-612-162-621-612-265-2_W.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 260-260-216 361-316-201-216-126-213-123-102-162-126-136-132-201-213-120-231-132-102-123-102-130-301-103-310-321-160-136-312-123-103-310-103-231-102-130-132 261-236 321-362-312 3_V.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 376-123-103-103-102-130-103-132-531-236-301-375-753-573-756-176 675-670-672 752-574-756-756-705-675-675-670_W.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 103-132-312-321-312-316-530-567-576-317-132-570-752-351-315-357-315-103-321-123-574-175-132-134-307-103-321-321-302-103-132-756-567-302-320-132-312-103-137-123-102-570-723-213-2_V.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 312-102-267-261-621-163-320-302-312-310-312-132-130-130-103-103-136-103-107-103-103-173-107-137 361-136-672-617-162-613-631-123-267-326-126-136-163_W.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 107-617-125-106-163-136-103-132-167-126-106-106-137-106-160-165-167-512-612 132-126-367-167-623-123-621-103-126-106-162-107-123-620-561-105-173-106-167-103-172-162-103-103-132-7_V.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 160-135-165-106-136-126-135-731-751-761-715-716-712-762-726-761-765-761-716-765-762-726-721-726-716-672-275-276-271-271-217-275-276-271-276-275-231-162-123-576-574_W.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 107 136-613-163-613-163 615-165-136-176-167-162-106-162-670-176-162-136-123-157-176-602-612-605-601-167-106-103-165-176-265-167-162-613-130-163-615-136-137-106-162-321-136-106-6_V.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 765-165-167-150-675-165-127-362-167-126-627-657-615 715-617-136-162-106-163-136-106-163-170-136-103-167-631-351-536-156-651-602-123-137-163-163-103-652-130-160-612-163-130-162-3_V.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 267-263-236-321-236-321-321-231-123-231-231-236-236-236-216-263-167-136-106-162-162-162-103-160-136-103-103-136-310-716-127-136-137-317-316-156-165-103-136-156-103-610-136-610-1_V.mp3)
  Segment 35: 53584ms ~ 55024ms (file: 761-361-125-362-132-132-130-137-103-107-107-107-173-651-103-362-123-126-213-321-302-103-321-163-301-312-137-276-672-672-675-672-672-672-671-761-612-627-671-126-251-216-276-127-1_V.mp3)
  Segment 36: 56024ms ~ 56600ms (file: 165-267-762-257-156-620-623-625-657-657-517-576-567-657-657-167-160-167-103-106-627-632-710-716-761-762-761-167-362-675-762-276-265-165-675-561-163-572-671-675-562-675-657-613-6_W.mp3)
  Segment 37: 57600ms ~ 58176ms (file: 617-645 765-312-576-102-612-103-123-310 321-123-162-316-201-126-162 160-716-163-103-137-134-103-103-165-713-165-132-103-631-106-132-103-132-103-143-106-132-103-107-132-103-136-1_V.mp3)
  Segment 38: 59176ms ~ 59752ms (file: 561-136-135-162-167-601-162-621-163-602-620-620-627-620-620-610-612-106-106-160-106-635-163-167-137-713-137-275-753-735-176-106-167-167-106 175-705-706-163-165-130-102-103-165-6_W.mp3)
  Segment 39: 60752ms ~ 61328ms (file: 362 123-715-157-761-126-163-132-103-107-156-163-157-256-165-136-165-106-106-136-167-136_V.mp3)
  Segment 40: 62328ms ~ 62904ms (file: 123-203-230-216-201-216-162-106-321-123-102-126-106-136-136-316-136-217-201-231-230-201-206-265-567-576-576-576-567-206-260-627-623-216-302 612-106-601-601-163-160-106-601-612-6_V.mp3)
  Segment 41: 63904ms ~ 64480ms (file: 576-576-576-567-567-576-567-576-567-576-567-576-567-576-567-576-567-576-567-576-567-567-576-576-576-576-576-576-576-576-567-576-576-567-576-567-576-576-567-576-576-567-576-576-5_W.mp3)
  Segment 42: 65480ms ~ 66056ms (file: 321-154-756-675-675-162-103-126-216-260-203-236-231-203-216-156-162-136-103-163-167-106-162-106-162-106-657-675-602-207-725-120-126-123-567-162-163-103-103-103-103-134-103-513-5_V.mp3)
  Segment 43: 67056ms ~ 67632ms (file: 153-167 163-762-307-307-103-127-102-137-103-137-136 372-327-372-132-132-163-132-576-375-372 673-762-763-762-672-671-675-672-237-176-762-765-670-617-312-162-167-135-236-231-132-3_V.mp3)
  Segment 44: 68632ms ~ 69208ms (file: 365-352-367-675-576-623-623-267-267-236-267-267-256-326-362-326-236-362-326-236-326-627-236-326-362-263-263-672-237-312-123-132-215-125-132-173-352-351-351-315-361-365-317-376-3_W.mp3)
  Segment 45: 70208ms ~ 70784ms (file: 326-312-361-321-132-632-326-621-631-316-361-657-635-657-672-657-652-657-675-672-671-610-167-160-623-675-675-675-657-637-765-675-657-136-316-632-620-621-261-627 267-362-162-631-6_W.mp3)
  Segment 46: 71784ms ~ 72360ms (file: 574-570-123-602-602-106-103-163-162-261-162-621-657-162-312-316-136-312-123-206-320-135-314-326-203-236-230-230-236-206-230-236-312-326-672-627-132-135-132-312-504-672-601-602-5_V.mp3)
  Segment 47: 73360ms ~ 73936ms (file: 765 765-756-756-765-572-576-756-576 261-675-613-136-621-612-617-627 675-765-762-762-765-765-762-762-765-765-763-762-763 576-576-576-576 657-576-760-765-765-765-760-715-716-756-7_W.mp3)
  Segment 48: 74936ms ~ 75512ms (file: 756-756-576-576-675-652-652-650-657-567-652-625-625-126-231-135-103-103 672-671-716-761-716-602-263-216-120-507-512-532-251-125-135-103-106-657-106-236-170-176-657-136-126-612-7_W.mp3)
  Segment 49: 76512ms ~ 77088ms (file: 317-260-260-103-132-106-123-106-713-162-137-132-132-710-175-107-127-172-716-756-760-167-167-176-173-612-172-317-751-175-675-265-267-362-362-231-132-123-206-260-127-203-132-162-1_V.mp3)
  Segment 50: 78088ms ~ 78664ms (file: 526-657-127-103-123-106-316-136-216-267-652 756-765-675 617_W.mp3)
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
> **Time**: 2026-04-03 08:28:49
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "안녕하세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 40, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "반갑습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "반갑습니다", "start_ms": 50, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "식사하셨어요?",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "식사하셨어요?", "start_ms": 30, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "오늘 날씨가 정말 좋네요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 60, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 190, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 340, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 450, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "어디 가시는 길인가요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "어디", "start_ms": 45, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가시는", "start_ms": 200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "길인가요?", "start_ms": 360, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "공부하러 도서관에 가요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "공부하러", "start_ms": 55, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "도서관에", "start_ms": 240, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "가요", "start_ms": 430, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "시험 기간이라 바쁘시겠어요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "시험", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "기간이라", "start_ms": 170, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "바쁘시겠어요", "start_ms": 340, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "네 조금 힘들긴 하네요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 70, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 160, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "힘들긴", "start_ms": 290, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "하네요", "start_ms": 440, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "그래도 화이팅 하세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "그래도", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "화이팅", "start_ms": 210, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "하세요", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "감사합니다 좋은 하루 되세요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 230, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "하루", "start_ms": 340, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "되세요", "start_ms": 450, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "What are you studying now?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "What", "start_ms": 60, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "are", "start_ms": 170, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 240, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "studying", "start_ms": 330, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "now?", "start_ms": 490, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "I am learning Korean language.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 50, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "am", "start_ms": 110, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "learning", "start_ms": 180, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "Korean", "start_ms": 330, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "language.", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "한국어 공부는 재미있나요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "공부는", "start_ms": 200, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "재미있나요?", "start_ms": 350, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 15,
"transcript": "조금 어렵지만 보람차요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "조금", "start_ms": 60, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "어렵지만", "start_ms": 180, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "보람차요", "start_ms": 370, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "열심히 하시는 모습이 보기 좋아요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "열심히", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "하시는", "start_ms": 190, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "모습이", "start_ms": 310, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "보기", "start_ms": 420, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "좋아요", "start_ms": 500, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "응원해 주셔서 기뻐요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "응원해", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "주셔서", "start_ms": 210, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "기뻐요", "start_ms": 360, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "다음에 또 만나요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 200, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 290, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "잘 가세요 조심하시고요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "잘", "start_ms": 55, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 140, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "조심하시고요", "start_ms": 270, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "안녕히 계세요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 35, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "계세요", "start_ms": 220, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "점심 메뉴는 정하셨나요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "점심", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "메뉴는", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "정하셨나요?", "start_ms": 280, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "아니요 아직 고민 중이에요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "아니요", "start_ms": 65, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "아직", "start_ms": 190, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "고민", "start_ms": 300, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "중이에요", "start_ms": 410, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "근처에 맛있는 비빔밥 집이 있어요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "근처에", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "맛있는", "start_ms": 170, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "비빔밥", "start_ms": 300, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "집이", "start_ms": 420, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 500, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "정말요? 추천해 주세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "정말요?", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "추천해", "start_ms": 240, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "여기 지도로 위치 보내드릴게요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "여기", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "지도로", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "위치", "start_ms": 280, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "보내드릴게요", "start_ms": 390, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "와 정말 친절하시네요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "와", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 110, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "친절하시네요", "start_ms": 240, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 28,
"transcript": "별말씀을요 맛있게 드세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "별말씀을요", "start_ms": 60, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "맛있게", "start_ms": 270, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "드세요", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "네 덕분에 든든하게 먹겠어요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "덕분에", "start_ms": 140, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "든든하게", "start_ms": 270, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "먹겠어요", "start_ms": 430, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "즐거운 주말 보내세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "즐거운", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "주말", "start_ms": 190, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "보내세요", "start_ms": 320, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 32,
"transcript": "오늘 저녁에 시간 괜찮으세요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "저녁에", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "시간", "start_ms": 280, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "괜찮으세요?", "start_ms": 390, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "영화 보러 갈까요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "영화", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "보러", "start_ms": 200, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "갈까요?", "start_ms": 330, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "좋아요 어떤 영화 볼까요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "좋아요", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 200, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "영화", "start_ms": 320, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "볼까요?", "start_ms": 430, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "액션 영화는 어때요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "액션", "start_ms": 45, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "영화는", "start_ms": 200, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "어때요?", "start_ms": 350, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 37,
"transcript": "저는 코미디 영화가 더 좋아요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 60, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "코미디", "start_ms": 170, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "영화가", "start_ms": 330, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 460, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "좋아요", "start_ms": 520, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "그럼 코미디 영화로 예매할게요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "코미디", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "영화로", "start_ms": 280, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "예매할게요", "start_ms": 410, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "고마워요 저녁에 봬요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "고마워요", "start_ms": 35, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "저녁에", "start_ms": 190, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "봬요", "start_ms": 340, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "이따가 연락드릴게요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "이따가", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "연락드릴게요", "start_ms": 230, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 42,
"transcript": "비가 올 것 같아요 우산 챙기셨나요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "비가", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "올", "start_ms": 140, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 210, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 260, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "우산", "start_ms": 370, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "챙기셨나요?", "start_ms": 480, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "깜빡하고 안 가져왔어요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "깜빡하고", "start_ms": 60, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 250, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "가져왔어요", "start_ms": 320, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "제 우산 같이 써요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "제", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "우산", "start_ms": 110, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 240, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "써요", "start_ms": 350, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "정말 고마워요 덕분에 살았네요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 35, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "고마워요", "start_ms": 140, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "덕분에", "start_ms": 290, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "살았네요", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "이제 역까지 같이 가요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 45, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "역까지", "start_ms": 170, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 330, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "가요", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 48,
"transcript": "조심해서 걸으세요 바닥이 미끄러워요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "조심해서", "start_ms": 65, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "걸으세요", "start_ms": 210, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "바닥이", "start_ms": 340, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "미끄러워요", "start_ms": 450, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "네 천천히 가요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "천천히", "start_ms": 130, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "가요", "start_ms": 340, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "다 왔네요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "다", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "왔네요", "start_ms": 140, "dur_ms": 400, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 08:28:49*
