# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=09:08:34  uptime=28160s  mem=1781MB  system_processes=449"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_090700.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 173 631-612-361-160-167 601-160-136-160-163-160-130-163-103-103_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 263-263-263-326-236-261-261-261-263-261-236_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 576-157-567-567-651 576-561-567-567-105-103-576-132-125-163-310-103-126-130-316-127-321-301-103-123-132-132-321-130-312-132-130-372-126-165-136-103-301-160-132-310-132-136-130-6_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 263-261-316-136-123-213-123-126-316-132-132-312-312-326 263-632-751-756-756-570-576-752-631-165-136-163-160-123-126-106-123-163-123-132-173-137-163-613-564-163-167-567-206-206-1_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 320-320-206-126-123-103-106-106-210-103-132-312-130-301-326-231-132-310-103-231-310-130-130-310-236-132-120-103-130-360-103-106-103-106-163-312-106-106-167-162-607-601-251-125-2_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 601-136-132-310-103-765-765-756-765-675-657-567-675-765-675-675-657-675-567-126-126-316-753-310-106-153-751-765-706-723-675-765-671-175-716-170-137-103-103-263-213-136-620-263-2_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 607-316-102-312-360-216-102-103-301-310-132-753-623-627-623-627-673-637-621-621-612-627-620-672-627-160-627-612-136-602-612-601-627-607-136-762-672-607-130-130-310-310-310-136-3_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 756-756-657-316-231-136-316-265-213-230-123-102 215-567-675-617-160-162-163-610-263-123-120-103-170-712-267 315-136-135-103 310-130-106-315-103-132-576-567-576-567-567-157-103-1_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 260-263-235-236-312-136-103-132-301-326-236-106-163-123-312-132-312-765-106-216-201-216-310-103-163-103-120-160-310-163-167-132-321-320-120-563-163-176-136-102-316-136-102-136-6_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 132-263-257-215-572-106-137-675-106-607-162-103-163-162-120-120-760-103-675-516-153-637-761-136-137-103-627-106-136-765-103-136-136-316-631-675-162-103-672-627-127-103-132-103-1_V.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 136-136-163-172 136-175-172-127-126-136-130-136-172-176-576-672-213-162-127-126-756-136-167-621-126-126-126-106-165-167-761-670-103-103-765-672-623-267-310_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 123-761-716-176-756-675-153-670-132-163-132-162-236-312-213-126-216-123 671-675-106-671-617-132-103-167-130-103-126-172-260-620-260-672-103-312-103-160-103-162-675-173-126-103-1_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 617-160-601-765-617-627-632-263-326-623-265-126-576-675-671-167-162-312-123-132-310-106-216-216-261-326-162 574-574-576-574-574 127-163-167 617-617-367_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 526-361-103-751-760-257-261-160-106-516-123-675-706-256-126-106-102-123-132-132-103_V.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 621-160-612-613-371-216-103-163-103-275-632-316-216-126-106-130-106-671-136-103-560-135-162-621-103-160-162-160-162-602-162-612-103-162-106-671-613-103-542-127-256-602-651-130-1_V.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 576 367-301-321-326-310-301-306-302-306-306_V.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 103-103-165-163-236-326-320-236-260-210-312-103-102-130-103-130-530-310-103-136-326-312-601-351-312-310-260-367-153-327-732-107-360-327-306-136-320-236-206-302-312-130-130-310-3_V.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 162-572-527-261-261-567-657 316-312 610-672-671-267-312-123-213-132-361-623-162-631-312-312-162-621-623-632-613-162-631-612-623-613-162-163-612-631-162-361-623-623-623-632-623-6_W.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 263-261-267-263-623-263-267-263-263-267-263-263-236-236-321-236-237-236-230-237-231-237-213-321 652-267-326-275-725-725-725-732-256-261-127-217-327-312-317-372-237-315-317-325-7_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 376-216-516-516-571-236-213-671-657-576-172-217-271-271-716-237-217-127-267-217-216-256-175-321-627-165-357-567-157-517-615-521-517-567-517-356-675-365-351-165-327-123-625-751-5_W.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 371-132-607-136 752-267-261-631-316-613-106-163-615-326-320-126-130-130-160-157-103-362-612-206-107-106-163-167-103-132-163-367-163-756-261_V.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 613-136-671-675-625-623-263-261-263-623-163-132-103-167-136-361-106-761-136-156-316-627-136-301-163-163-216-621-621-623-162-160-132-621-172-163-103-103-163-106-106-106-136-156-6_V.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 236-236-375-675-763-132-236-236-236-123-132 576-321-367-132-321-732-372-726-267-327-675-675-637-673-376-573-573-572-573-547-574-576-613-613-163-163-136-153-513-514-571-517-571-7_W.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 106-132-163-627-362-231-120-302-263-236-203-231-675-627-627-267-126-213-623-253-216-132-326-127-126-621-312-231-326-312-126-236-231-623-625-236-321-623-621-256-527-657-256-613-1_V.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 167-631-103-617-307-126-260-627-162-163-317-106-316-103-512-157-213-162_V.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 167-163-106-132-136-761-106-756-617-165-652-672-612-671-612-506-160-106-507-175-576-156-106-605-612-136-167-160-103-130-623-103-167-103-136-263-320-360-361-612-316-321-630-167-1_V.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 310-567-621-326-102-527-675-756-762-617-163-136-163-130-136-675-310-312-631-130-320-326-206-206-206-602-627-654-506-605-605-236-526-574-351-316-756-735-106-136-132-320-230-524-5_W.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 103-163-132-627-617-612-130-136-107-567-625-165-136-567-620-163-167-617-160-103-167-103-106-761 164-627-762-176-163-162-163-103-136-617-136-106-136-654-652-671-670-103-106-163-1_V.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 617-631-671-165-160-136-162-316-316-361-321-312-312-136-362-316-136-321-316-621-632-631-617-672-627-671-675-670-631-613-130-321-321-132-637-657-257 163-106-136-312 167-631 361-1_W.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 102-217-716-753-763-731-103-312-167-750-312-317-137-135-261-103-106-125-137-162-730-621-602-610-310-120-162-160-623-163-123-671-137-136-103-103-132-167-163-615-103-617-675-567-1_V.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 534-354-352-273-574-325-236-625-623-625-325-236-532-527-357-356-356-352-536-523-530-256-527-256-256-563-352-265-326-235-523-526-150-517-517-531-576-562-576-567-527-756-576-256-6_W.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 206-253-523-150-560-657-216-261-216-102-126-216-261-267-217-504-560-560-567-756-756-750-375-560-362-306-627-670-602-107-602-612 576-756-756 362-125-627-627-623-612-602-621-263-1_V.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 610 126-312-157-173-627-637-675-167-610-620 576-765-371_W.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 126-163-136-163-316-132-156-314-361-316-361-341-413-613-213-413-312-321-631-670-657-607-675-756-756-765-756-765-675-765-576-756-735-317-163-376-756-765-261-621-126-236-231-123-2_V.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 106-103-762-126-126-372-372-310-312-132-102-201-206-206-206-267-216-267-230-210-321-320-302-302-231-321-321-312-216-123-602-326-523-206-306-302-326-302-230-132-312-213-320-302-2_W.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 163-103-157-157-163-613-136-103-106-316-657-136-312-103-106-167-103-103-675-567-231-176_V.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 106-310-301-130-301-301-132 765-670 236-236-261-576-213-167-610-165-163-162-156-260-126 765-675-675-762-130-621-163-167-617-607-610-106-162-312-136-103-136-216-316-176-106-136-5_W.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 253-203-213-231-216-126-312-361-230-236-250-203-206-621-621-261-260-236-263-316-362-326-126-102-132-126-103-132-702-706-726-627-672-167-163-312-531-312-132-312-612-263-203-102-1_V.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 136-130 237-132-132-103-302-731-271-635-617-162-176-162-127-217-136-167-162-106-765-761 760-672-672 106-136-263-102-132-167-167-261-132-162 103-130-103-135-103-103-132-123-127-1_V.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 267-276-163-157-517-123-137-713-163-132-175-516-672-312-170-751-756-756-576-175-173-751-753 163-635-750-536-756-765-703-670-752-765-673-617-670-617-673-621-765-602-672-372-167-6_W.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 726-637-623-103-136-167-136-103-137-360-302-326-327-361-327-376-673 574-573-715-310-103-103-136-612-612-162-261-621-612-621-574-574-570-570-265-623-130-130-103 301-301-312-132-7_V.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 102-106-163-170-517-136-627-162-162-163-754-106-103-160-156-156-132-165-106-167-301-130-136-137-126-132-765-175-106-107-163-167-137-162-106-736-172-167-103-107-165-167-165-612-6_V.mp3)
  Segment 43: 66192ms ~ 66768ms (file: 765-762-713-713-716-731-713-736-761-762-763-756-756-754-753-741-176-617-367-103-126-126-236-206-230-306-237-123-163-102-267-532-761-267-261-765-602-627-267-163-175-576-756-176-1_V.mp3)
  Segment 44: 67768ms ~ 68344ms (file: 302-312-106-156-310-530-260-206-713-102-103-137-320-130-103-103-105-301-570-574-157-206-210-301-316-301-326-312-203-206-206-201-206-203-126-130-132-123-760-103-130-316-150-527-1_V.mp3)
  Segment 45: 69344ms ~ 69920ms (file: 106-136-106-163-103-516-136-310 712-653-136-163-132-621-612-103-136-103-152-176-215 103-617-167-103-251-103-712-615-627-165-106-162_V.mp3)
  Segment 46: 70920ms ~ 71496ms (file: 615-165-106-103-175-103-617-760-576-213-176-126-167-176-615-162-123-124-162-176-136-607-136-167-103-132-625-567-265-765-756-756-756-756 620-672-260-623-612-632-564-236-617-361-1_V.mp3)
  Segment 47: 72496ms ~ 73072ms (file: 163-213-761-752-130-106 107-106-176-716 675-627-316 103-531-561-236-236-236-203-102-162-163-321-162-632-162-752-256-215-571-673-672-675-316-162-136-132-613-632-612-612-627-603-6_W.mp3)
  Segment 48: 74072ms ~ 74648ms (file: 216-263-312-612-136-167-136-132-152-617-162-136-130-167-136-137-621-167-162-106-316-316-176-106-132-176-162-103-162-176-103-103-162-625-136-136-136-136-316-130-132-610-132-106-1_V.mp3)
  Segment 49: 75648ms ~ 76224ms (file: 106-163-136-326-206-327-753-706-126-106-162 260-260-617 260-260-260-627-762-765-413-162-136-103-601-620-603-357-537-576-576-657-356-327-231-632-261-617-627-672-607-103-613-610-6_W.mp3)
  Segment 50: 77224ms ~ 77800ms (file: 657-715-657-574-576-572-567-157-153-157-571-576-157-675-715-576-517 574-574-132-103-360-571-231-572-570-765-675-625-617-765-567-623-670-106-627-305-602-602-612-160-106-631-306-5_W.mp3)
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
> **Time**: 2026-04-03 09:09:52
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
{"word": "안녕", "start_ms": 40, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "하세요", "start_ms": 290, "dur_ms": 286, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "반갑습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "반갑", "start_ms": 30, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "습니다", "start_ms": 270, "dur_ms": 306, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "오늘 날씨가 정말 좋네요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 170, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 320, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 440, "dur_ms": 136, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "공부하기 딱 좋은 날이에요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "공부", "start_ms": 25, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "하기", "start_ms": 155, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "딱", "start_ms": 255, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 335, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "날이에요", "start_ms": 445, "dur_ms": 131, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "한국어 공부는 재미있나요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 60, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "공부는", "start_ms": 200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "재미", "start_ms": 350, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있나요", "start_ms": 450, "dur_ms": 126, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "네 아주 흥미로워요",
"speaker": "speaker_2",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 45, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 125, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "흥미", "start_ms": 255, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "로워요", "start_ms": 405, "dur_ms": 171, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "단어 외우기가 조금 힘들어요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "단어", "start_ms": 35, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "외우기가", "start_ms": 145, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 305, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "힘들어요", "start_ms": 415, "dur_ms": 161, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "반복해서 들으면 익숙해질 거예요",
"speaker": "speaker_2",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "반복해서", "start_ms": 20, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "들으면", "start_ms": 160, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "익숙", "start_ms": 280, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "해질", "start_ms": 380, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 470, "dur_ms": 106, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "포기하지 말고 계속하세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "포기", "start_ms": 55, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "하지", "start_ms": 155, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "말고", "start_ms": 245, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 345, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "하세요", "start_ms": 455, "dur_ms": 121, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "응원해주셔서 감사합니다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "응원", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "해주셔서", "start_ms": 150, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "감사", "start_ms": 330, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "합니다", "start_ms": 450, "dur_ms": 126, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "천천히 말해주실 수 있나요",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "천천히", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "말해", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "주실", "start_ms": 280, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 370, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "있나요", "start_ms": 430, "dur_ms": 146, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "네 알겠습니다 다시 말할게요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 45, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "알겠습니다", "start_ms": 115, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 295, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "말할게요", "start_ms": 395, "dur_ms": 181, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "이 문장은 무슨 뜻이에요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 50, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "문장은", "start_ms": 110, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "무슨", "start_ms": 250, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "뜻이에요", "start_ms": 360, "dur_ms": 216, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "그건 비밀이라는 뜻입니다",
"speaker": "speaker_2",
"confidence": 0.91,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "그건", "start_ms": 35, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "비밀", "start_ms": 135, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "이라는", "start_ms": 255, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "뜻입니다", "start_ms": 365, "dur_ms": 211, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "농담이에요 웃어보세요",
"speaker": "speaker_2",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "농담", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "이에요", "start_ms": 150, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "웃어", "start_ms": 260, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "보세요", "start_ms": 380, "dur_ms": 196, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 17,
"transcript": "하하하 정말 재미있네요",
"speaker": "speaker_1",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "하하하", "start_ms": 45, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 225, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "재미", "start_ms": 355, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있네요", "start_ms": 455, "dur_ms": 121, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "이제 다음 단계로 넘어가요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 25, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 125, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "단계로", "start_ms": 235, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "넘어", "start_ms": 375, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "가요", "start_ms": 465, "dur_ms": 111, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "질문이 있으면 언제든 하세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "질문이", "start_ms": 50, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "있으면", "start_ms": 180, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "언제든", "start_ms": 300, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "하세요", "start_ms": 430, "dur_ms": 146, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "이해가 아주 잘 돼요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "이해가", "start_ms": 35, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 175, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 295, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "돼요", "start_ms": 375, "dur_ms": 201, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "그럼 다행이네요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 60, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "다행", "start_ms": 180, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이네요", "start_ms": 330, "dur_ms": 246, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "복습도 잊지 마시고요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "복습도", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "잊지", "start_ms": 190, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "마시", "start_ms": 300, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "고요", "start_ms": 400, "dur_ms": 176, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "오늘 배운 건 꼭 기억할게요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 30, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "배운", "start_ms": 140, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "건", "start_ms": 250, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "꼭", "start_ms": 310, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "기억", "start_ms": 380, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "할게요", "start_ms": 490, "dur_ms": 86, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "훌륭한 태도입니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "훌륭한", "start_ms": 45, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "태도", "start_ms": 205, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "입니다", "start_ms": 345, "dur_ms": 231, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "내일 또 만나요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "내일", "start_ms": 50, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 280, "dur_ms": 296, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "안녕히 가세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 40, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 280, "dur_ms": 296, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "잘 가요 친구",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "잘", "start_ms": 35, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "가요", "start_ms": 135, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "친구", "start_ms": 285, "dur_ms": 291, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "즐거운 주말 보내세요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "즐거운", "start_ms": 25, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "주말", "start_ms": 165, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "보내세요", "start_ms": 295, "dur_ms": 281, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "당신도요 고마워요",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "당신", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "도요", "start_ms": 170, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "고마", "start_ms": 280, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "워요", "start_ms": 390, "dur_ms": 186, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "별말씀을요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "별말씀", "start_ms": 40, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "을요", "start_ms": 280, "dur_ms": 296, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "Good job everyone",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Good", "start_ms": 30, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "job", "start_ms": 150, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "everyone", "start_ms": 250, "dur_ms": 326, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "See you next time",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 145, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "next", "start_ms": 225, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "time", "start_ms": 345, "dur_ms": 231, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 34,
"transcript": "자 다시 시작해볼까요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "자", "start_ms": 35, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 95, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "시작해", "start_ms": 205, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "볼까요", "start_ms": 345, "dur_ms": 231, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "준비되었습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "준비", "start_ms": 25, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "되었습니다", "start_ms": 205, "dur_ms": 371, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "집중해서 들어보세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "집중", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "해서", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "들어", "start_ms": 300, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "보세요", "start_ms": 400, "dur_ms": 176, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "이 소리는 무엇일까요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "소리는", "start_ms": 100, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "무엇", "start_ms": 240, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "일까요", "start_ms": 350, "dur_ms": 226, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "새가 우는 소리 같아요",
"speaker": "speaker_1",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "새가", "start_ms": 30, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "우는", "start_ms": 140, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "소리", "start_ms": 250, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 360, "dur_ms": 216, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "정답입니다 아주 잘했어요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "정답", "start_ms": 45, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "입니다", "start_ms": 175, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 285, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "잘했어요", "start_ms": 385, "dur_ms": 191, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "다음 문제를 드릴게요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "다음", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "문제를", "start_ms": 160, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "드릴게요", "start_ms": 300, "dur_ms": 276, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "준비되셨나요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "준비", "start_ms": 35, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "되셨나요", "start_ms": 215, "dur_ms": 361, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "네 어서 주세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 20, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "어서", "start_ms": 100, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 230, "dur_ms": 346, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "이게 마지막 문제입니다",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "이게", "start_ms": 55, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "마지막", "start_ms": 155, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "문제", "start_ms": 295, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "입니다", "start_ms": 405, "dur_ms": 171, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "조금 어려울 수도 있어요",
"speaker": "speaker_1",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "조금", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "어려울", "start_ms": 150, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "수도", "start_ms": 280, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 370, "dur_ms": 206, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "자신감을 가지세요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "자신감을", "start_ms": 30, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "가지세요", "start_ms": 210, "dur_ms": 366, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "할 수 있습니다 화이팅",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "할", "start_ms": 45, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 105, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 165, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "화이팅", "start_ms": 315, "dur_ms": 261, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "정말 고마워요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "고마워요", "start_ms": 200, "dur_ms": 376, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "다음에 또 봐요 안녕",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 35, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 165, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "봐요", "start_ms": 265, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "안녕", "start_ms": 415, "dur_ms": 161, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "안녕히 계세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 25, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "계세요", "start_ms": 265, "dur_ms": 311, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "수고하셨습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "수고", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "하셨습니다", "start_ms": 220, "dur_ms": 356, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 09:09:52*
