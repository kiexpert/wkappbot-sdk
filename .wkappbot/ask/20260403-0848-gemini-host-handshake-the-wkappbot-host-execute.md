# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=08:46:49  uptime=26854s  mem=1496MB  system_processes=445"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_084515.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 617-632-362-352-326-326-321-163-610-163-136-165-516-107-160-162-651-672-265-167-361-106-160-306-136-103-167-206-176-612-136-163-106-176-167-657-617-160-167-106-163-326-312-301-1_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 657-635-657-652-536-567-357-153-615-652-365-536-675-637-362-657-657-635-756-653-635-365-365-357-156-517-567-567-567-576-657-536-576-573-537-567-351-173-315-153-375-531-513-135-5_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 670-170-167-713-613-217-263-213-136-632-621-706-170-102-172-130-761-327-573-750-750-735-175-132-103-103-132-103-314-132-312-132-623-326-263-632-620-263-163-716-671-671-672-167-1_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 256-653-265-162-136-103-132-103-156-157-167-167-162-162-620-126-361-106-167-105-136-132-136-137-165-173-510-107-526-137-276-765-763-765-165-617-160-103-103-106-103-103-765-317-1_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 612-162-123-132-126-136-132-123-176-762-216_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 321-132-213-123-632-236-231-326-236-321-216-316-123-132-231-570-570-753-157-137-507-570-571-173-103-103-132-701-130-107-173-175-105-715-103-137-154-750-125-167-162-517-153-105-5_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 627-627-675-627-627-627-632-627-623-613-521-157-132-103-132-103-321-162-641-613-631-613-620-620-620-620-263-263-260-256-756-765-672-706-316-354-504-543-136-163-136-163-231-203-2_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 230-236-321 532-512-135-125-153-513-514-154-514-321-213 153-231-326-132-213-312-576-526-132-231-326-231-321-213 261-326-132-213 163-102-210-136-326-321-106-312-216-236-213-130-3_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 163-165 623 671-627-612-627 162-756-756-765-765-762-765-675-567-657-607-653-517-561-561-312-615-613-362-367-657-106-126-162-625-236_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 756-756-756-132-136 765-765-762-672-627-263 136-162_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 547-103-103-103-103-103-672-261-627-267-203-207-236-301-302-320-301-632-673-627-260-260-610-761-671-617-163-167-261-263-260-203-236-263-301-302-306-230-320-201-260-726-236-263-2_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 176-761-120-751-167-715-761-716-761-672-172-706-126-675-756-706-765-672-706-765-617-230-752-751-716-612-607-362-762-761-701-716-721-162-102-103-102-576-753-230-320-370 756-723-5_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 123-372-320-263-206-230-260-236-302-216-320-302-320-231-321-160-612-607-167-106-103-672-136-316-176-136-162-136-173-167-136-176-107-176-260-612-671-163-136-103-317-123-120-321-3_V.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 263-206-263-206-620-106-120-201-302-312-310-312-206-524-325-302-572-260-260-627-620-267-260-320-132-136-301-301-307-302-301-236-302-130-130-173-710-137-103-162-621-263-326-267-6_V.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 631-261-612-130-126-617-162-617-160-615-361-261-631-602-103-103-106-152-126-153-510-160-162-167-106-103-610-103-137-623-167-136-615-132-123-153-106-126-163-206-106-516-163-516-1_V.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 672-657-257-567-627-672-671-673-671-672-761-612-127-126-216-102-217-213-261-215-136-261-621-261-267-267-627-672-675-571-627-672-672-765-763-761-762-617-127-132-123-265-217-261-3_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 136-130-120-312-123-263-260-261-132-132-123-126-103-316-301-623-260-260-620-160-613-130-103-130-671-617-326-236-136-617-175-106-167-106-312-123-341-167-613-167-163-103 163-136-6_V.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 216-213-617-162-163-136-213-321-123-231-216-126 627-265-326-275-256-236-265-326-261-261-261-265 163-260-267-267-267-627-267-672-627-672-673-670-672-623-362-360-630-613-635-657-6_W.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 516-106-613-130-612-106-361-162-623-216-167-612-143 162-261-261-162-765-765-623-310-260-316-126-167-216-763-625-672-263-236-621-126-162-162-132-126-261-162-162-123-163-162-216-6_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 135-351-576-576-576-576-567-576-756-657-567-576-657-657-567-675-576-576-576-576-576-576-576-576-574-561-567-576-567-576-576-576-576-756-653-576-576-576-576-560-105-607-607-173-3_W.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 130-571-173-137-316-137-125-326-260-167-703-167-132-567-130-625-136-167-167-103-160-137-675-701-106-163-123-765-763-610-760-761-765-672-361-106-167-716-316-671-103-512-326-561-6_V.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 316-321-326-620-320-360-326-651-765-632-236-312-320-302-326-603-602-605-607-260-620-620-620-361-301-560-632-756-163-102-126-102-120-625-673-306-623-236-260-310-621-612-612-320-3_V.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 617-310-130-106-106-765-137-716-623-365-162-167-367-132-167-103-106-136-163-103-136-106-106-136-236-216-310-163-235-653-573-132-763-167-621-367-167_V.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 671-162-106-123-715-207-126-136-103-136-126-126-216-261-321-326-321-701-137-106-613-602-206-260-264-254-267-206-263-261-312-731-705-765-705-765-617-302-203-301_V.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 657-715-651-675-561-165-162-163-316-136-136-615-675-361-165-135-167-162-165-162-365-756-672-765-675-657-675-527-756-156-163-612-132-312-163-162-675-165-657-675-671-671-675-765-6_W.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 654-261-267-265-625-526-546-514-315-312-315-512-256-265-623-623-625-365-362-534-534-562-326-326-267-263-623-620-652-657-625-627-657-601-540-562-265-362-652-261-267-267-526-514-5_W.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 267-326-263-316-126-316 261-260-260-263-652_W.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 102-102-751-715-765-160-627-673-607-267-627-267-260-623-623-267-672-765-672-675-670-675-126-163-625-263-312-231-206-126-761-761-627-267-625-627-652-103-136-507-130-175-107-132-7_W.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 106-162-126-123-126-130-103-103-302-123-103-310-321-650-520-162-162-162-263-326-362-102-163-162-312-301-261-621-231-130-310-627-672-620-672-136-162-137-136-132_V.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 201-235-203-230-207-237-327-327-302-320-320-302-321-326-326-326-326-265-321-215-132-123-207-206-132-213-231-720-321-302-231-675-762-236-567-257-213-250-275-260-261-620-260-261-2_W.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 567-567-567-657-576-561-657-305-321-132-132-163-172-635-137-156-356-536-573-571-571-517-751-536-571-576-517-157-156-157-157-156-561-510-153-165-153-156-156-153-536-536-516-156-1_W.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 136 216-132-316-312-236-261-261-376-375-753-123-736-263-125-632-632-326-126-267-326-672-627-627-627-362-671-613-236-627-263-672-627-671-267-160-567-675-675-675-627-261-231-326-1_W.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 371-567-631-106-612-623-612-162-136-123-617-165-126-103-136-163-167-163-167-163-146-103-132-106-167-562-157-106-106-103-516-637-671-617-160-136-261-167-613 106-657-316-106-132-1_V.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 732-361-631-316-126-671-263-253-653-756-627-657 762-765-756-326-352-305-103-130-103-310-213-312-130-130-103-103-136-312-130-103-301-123-103-732-376-316-302-231_V.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 310-103-103-301-312-132-701-570-517-507 176-571-321-203-750-705-132-162-132-501-570-507-750-103-316-316-672-760-371-173-504-547-574-153 672-176 751-765-765-763-756-765-756-657-2_W.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 165-705-705-765-537-103-231-231-203-302-753-754-756-103-156-216-107-260-620-623-761-673-310-132-310-213-230-103-132-103-130-136 302-312-302-301-132-527-312-302-163-613-106-103-1_V.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 231-125-630-762-367-320-321-726-103-132-137-130-236-321-236-537-316-316-321-263-125-215-326-265-312-216-706-752-756-730-153-571-165-625-312-132-103-362-176-136-361-125-176-167-5_W.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 627-675-675-627-627-627-216-675-670-763-107-103-165-602-627-602-607-602-602-760-765-765-675-675-617-167-167-162-163-712-276-236-631-362-162-167-167-617-206-602-260-621-162-617-6_V.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 236-621-261-263-627-261-230-320-623-260-206-302-206-230-302-302-302-620-620-632-302-321-137-127-106-210-203-302-301-312-132-670-607-670-627-675-670-267-570-570-326-320-673-326-3_V.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 674-617-671-316-362-325-123-126-132-231-213-213-132-123-231-321-132-132-213-123-132-213-123-213-352-327-123-123-126-213-621-167-216-316-632-761-261-312-231-217-123-372-321-263-3_W.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 361-136 321-213-302-216-312-320-362-623_V.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 132-570-310-613-310-132-312-132-602-625-602-163-106-176-173-163-367-602-672-263-120-103-103-134-103-136-123-167-617-607-620-623-632-136-106-167-163-620-602-106-106-163-163-630-6_V.mp3)
  Segment 43: 66192ms ~ 66768ms (file: 672-627-675-675-765-765-762-762-762-765-765-762-762-762-762-726-726-726-267-762-765-725-576-567-567-756-756-564-671-630-163-615-612-261-671-672-607-312 637-136-673-137-362-637-1_W.mp3)
  Segment 44: 67768ms ~ 68344ms (file: 651-310-130-316-760-765-730-163-216-617-106-317-103-106-167-103-107-176-167-103-130-106-201-213-123-132-301-631-302-321-103-312-231-203-302-302-132-162-106-136-105-105-236-132-1_V.mp3)
  Segment 45: 69344ms ~ 69920ms (file: 362-106-312-163-567-657-762-576-132-176-631-675-361-367-173-756-367-316-176-672-752-756-754-750-751 657-725 615 571-751-326-236-326-362-576-756-576-576-576-675-675-567 612-623-2_W.mp3)
  Segment 46: 70920ms ~ 71496ms (file: 632-162-621 163-137 725-723-132-762-507-150-301-103-130-310-120-612-621-160-136-217-126-103-531-375-352-136-103-173-163-617-103-132-130-310-617-610-106-130-106-106-103-160-730-6_V.mp3)
  Segment 47: 72496ms ~ 73072ms (file: 132-136-103-106-106-162-126-167-136-165-106-167-310-106-106-103-126-172 126-106-167-160-106-106-165-671-106-617-136-106-610-150-610-136-156-123-265-162_V.mp3)
  Segment 48: 74072ms ~ 74648ms (file: 103-310-630-601-361-106-210-162-361-613-164-106-132-136-361-106-610-632-620-167-160-106-106-361-321-302-163-135-143-104 132-123-106 203-203-326-263-203-620-260-360-260-260-201-2_V.mp3)
  Segment 49: 75648ms ~ 76224ms (file: 516-567-672-627-263-261-657-756-127-130-130-716-316-310-301-321-203-260-206-203-260-362-312-765-263-236-326-236-230-230-526-256-162-162-756-756-765-301-102-312-362-360-362-301-3_W.mp3)
  Segment 50: 77224ms ~ 77800ms (file: 620-206-627-623-312-130-753-750-675-275-270-672-163-106-613-130-103-720-756-765-671-163-106-130-310-136-301-312-732-231-216-231-263-301-321-726-726-276-267-261-206-206-206-267-2_V.mp3)
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
> **Time**: 2026-04-03 08:48:00
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "안녕하세요 만나서 반갑습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 40, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "만나서", "start_ms": 300, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "오늘 날씨가 정말 좋네요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 170, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 320, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "점심 메뉴는 무엇인가요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "점심", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": 2.5},
{"word": "메뉴는", "start_ms": 165, "dur_ms": 150, "phase_lr_deg": 2.5},
{"word": "무엇인가요", "start_ms": 330, "dur_ms": 240, "phase_lr_deg": 2.5}
]
},
{
"segment": 4,
"transcript": "저는 비빔밥을 좋아합니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": 2.5},
{"word": "비빔밥을", "start_ms": 155, "dur_ms": 220, "phase_lr_deg": 2.5},
{"word": "좋아합니다", "start_ms": 385, "dur_ms": 190, "phase_lr_deg": 2.5}
]
},
{
"segment": 5,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 6,
"transcript": "한국어 공부는 재미있어요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 60, "dur_ms": 180, "phase_lr_deg": 2.5},
{"word": "공부는", "start_ms": 250, "dur_ms": 130, "phase_lr_deg": 2.5},
{"word": "재미있어요", "start_ms": 390, "dur_ms": 180, "phase_lr_deg": 2.5}
]
},
{
"segment": 7,
"transcript": "열심히 연습해 보세요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "열심히", "start_ms": 55, "dur_ms": 160, "phase_lr_deg": 2.5},
{"word": "연습해", "start_ms": 225, "dur_ms": 140, "phase_lr_deg": 2.5},
{"word": "보세요", "start_ms": 375, "dur_ms": 180, "phase_lr_deg": 2.5}
]
},
{
"segment": 8,
"transcript": "어디에서 오셨나요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "어디에서", "start_ms": 40, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "오셨나요", "start_ms": 280, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "저는 미국에서 왔습니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "미국에서", "start_ms": 160, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "왔습니다", "start_ms": 380, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "반가워요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "반가워요", "start_ms": 120, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "도움이 필요하시면 말씀하세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "도움이", "start_ms": 45, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "필요하시면", "start_ms": 185, "dur_ms": 190, "phase_lr_deg": 0.0},
{"word": "말씀하세요", "start_ms": 385, "dur_ms": 185, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "언제든지 환영합니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "언제든지", "start_ms": 55, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "환영합니다", "start_ms": 280, "dur_ms": 290, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "이것은 무엇입니까",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "이것은", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 2.5},
{"word": "무엇입니까", "start_ms": 200, "dur_ms": 330, "phase_lr_deg": 2.5}
]
},
{
"segment": 14,
"transcript": "그것은 책입니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "그것은", "start_ms": 35, "dur_ms": 160, "phase_lr_deg": 2.5},
{"word": "책입니다", "start_ms": 210, "dur_ms": 330, "phase_lr_deg": 2.5}
]
},
{
"segment": 15,
"transcript": "가족은 몇 명인가요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "가족은", "start_ms": 45, "dur_ms": 180, "phase_lr_deg": 2.5},
{"word": "몇", "start_ms": 235, "dur_ms": 80, "phase_lr_deg": 2.5},
{"word": "명인가요", "start_ms": 325, "dur_ms": 240, "phase_lr_deg": 2.5}
]
},
{
"segment": 16,
"transcript": "부모님과 동생이 있습니다",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "부모님과", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "동생이", "start_ms": 260, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 410, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "주말에 보통 뭐 하세요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "주말에", "start_ms": 60, "dur_ms": 140, "phase_lr_deg": 2.5},
{"word": "보통", "start_ms": 210, "dur_ms": 110, "phase_lr_deg": 2.5},
{"word": "뭐", "start_ms": 330, "dur_ms": 70, "phase_lr_deg": 2.5},
{"word": "하세요", "start_ms": 410, "dur_ms": 150, "phase_lr_deg": 2.5}
]
},
{
"segment": 18,
"transcript": "영화 보는 것을 좋아해요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "영화", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "보는", "start_ms": 170, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "것을", "start_ms": 290, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "좋아해요", "start_ms": 400, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "어떤 영화를 좋아하세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "어떤", "start_ms": 45, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "영화를", "start_ms": 175, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "좋아하세요", "start_ms": 345, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 21,
"transcript": "액션 영화를 아주 좋아합니다",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "액션", "start_ms": 55, "dur_ms": 140, "phase_lr_deg": 2.5},
{"word": "영화를", "start_ms": 205, "dur_ms": 130, "phase_lr_deg": 2.5},
{"word": "아주", "start_ms": 345, "dur_ms": 90, "phase_lr_deg": 2.5},
{"word": "좋아합니다", "start_ms": 445, "dur_ms": 120, "phase_lr_deg": 2.5}
]
},
{
"segment": 22,
"transcript": "한국 음식이 입에 맞으세요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "한국", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 2.5},
{"word": "음식이", "start_ms": 170, "dur_ms": 130, "phase_lr_deg": 2.5},
{"word": "입에", "start_ms": 310, "dur_ms": 100, "phase_lr_deg": 2.5},
{"word": "맞으세요", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 2.5}
]
},
{
"segment": 23,
"transcript": "네 맵지만 맛있어요",
"speaker": "speaker_2",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 70, "dur_ms": 100, "phase_lr_deg": 2.5},
{"word": "맵지만", "start_ms": 180, "dur_ms": 150, "phase_lr_deg": 2.5},
{"word": "맛있어요", "start_ms": 340, "dur_ms": 200, "phase_lr_deg": 2.5}
]
},
{
"segment": 24,
"transcript": "김치를 드셔 보셨나요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "김치를", "start_ms": 45, "dur_ms": 160, "phase_lr_deg": 2.5},
{"word": "드셔", "start_ms": 215, "dur_ms": 130, "phase_lr_deg": 2.5},
{"word": "보셨나요", "start_ms": 355, "dur_ms": 210, "phase_lr_deg": 2.5}
]
},
{
"segment": 25,
"transcript": "네 처음에는 매웠어요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 60, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "처음에는", "start_ms": 170, "dur_ms": 170, "phase_lr_deg": 0.0},
{"word": "매웠어요", "start_ms": 350, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "지금은 아주 잘 먹습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "지금은", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 190, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 290, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "먹습니다", "start_ms": 380, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "다행이네요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "다행이네요", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "취미가 무엇인가요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "취미가", "start_ms": 60, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "무엇인가요", "start_ms": 250, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "음악 듣는 것을 좋아해요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "음악", "start_ms": 55, "dur_ms": 130, "phase_lr_deg": 2.5},
{"word": "듣는", "start_ms": 195, "dur_ms": 110, "phase_lr_deg": 2.5},
{"word": "것을", "start_ms": 315, "dur_ms": 90, "phase_lr_deg": 2.5},
{"word": "좋아해요", "start_ms": 415, "dur_ms": 150, "phase_lr_deg": 2.5}
]
},
{
"segment": 30,
"transcript": "어떤 음악을 좋아하세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "어떤", "start_ms": 45, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "음악을", "start_ms": 175, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "좋아하세요", "start_ms": 345, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 32,
"transcript": "케이팝을 아주 좋아합니다",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "케이팝을", "start_ms": 65, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 255, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "좋아합니다", "start_ms": 365, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "가장 좋아하는 가수는 누구인가요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "가장", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 2.5},
{"word": "좋아하는", "start_ms": 160, "dur_ms": 150, "phase_lr_deg": 2.5},
{"word": "가수는", "start_ms": 320, "dur_ms": 140, "phase_lr_deg": 2.5},
{"word": "누구인가요", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 2.5}
]
},
{
"segment": 34,
"transcript": "저는 방탄소년단을 좋아합니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": 2.5},
{"word": "방탄소년단을", "start_ms": 155, "dur_ms": 230, "phase_lr_deg": 2.5},
{"word": "좋아합니다", "start_ms": 395, "dur_ms": 170, "phase_lr_deg": 2.5}
]
},
{
"segment": 35,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 36,
"transcript": "그들의 노래는 정말 멋져요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "그들의", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 2.5},
{"word": "노래는", "start_ms": 180, "dur_ms": 130, "phase_lr_deg": 2.5},
{"word": "정말", "start_ms": 320, "dur_ms": 100, "phase_lr_deg": 2.5},
{"word": "멋져요", "start_ms": 430, "dur_ms": 130, "phase_lr_deg": 2.5}
]
},
{
"segment": 37,
"transcript": "맞아요 퍼포먼스도 대단하죠",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "맞아요", "start_ms": 60, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "퍼포먼스도", "start_ms": 210, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "대단하죠", "start_ms": 400, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "어제는 무엇을 하셨나요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "어제는", "start_ms": 55, "dur_ms": 140, "phase_lr_deg": 2.5},
{"word": "무엇을", "start_ms": 205, "dur_ms": 130, "phase_lr_deg": 2.5},
{"word": "하셨나요", "start_ms": 345, "dur_ms": 220, "phase_lr_deg": 2.5}
]
},
{
"segment": 39,
"transcript": "친구들과 도서관에 갔어요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "친구들과", "start_ms": 60, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "도서관에", "start_ms": 250, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "갔어요", "start_ms": 420, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "시험 공부를 하러 갔나요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "시험", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "공부를", "start_ms": 155, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "하러", "start_ms": 295, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "갔나요", "start_ms": 405, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "네 시험이 곧 있어요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 80, "dur_ms": 110, "phase_lr_deg": 2.5},
{"word": "시험이", "start_ms": 200, "dur_ms": 140, "phase_lr_deg": 2.5},
{"word": "곧", "start_ms": 350, "dur_ms": 80, "phase_lr_deg": 2.5},
{"word": "있어요", "start_ms": 440, "dur_ms": 120, "phase_lr_deg": 2.5}
]
},
{
"segment": 42,
"transcript": "공부하느라 힘들겠네요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "공부하느라", "start_ms": 55, "dur_ms": 210, "phase_lr_deg": 2.5},
{"word": "힘들겠네요", "start_ms": 275, "dur_ms": 280, "phase_lr_deg": 2.5}
]
},
{
"segment": 43,
"transcript": "조금 힘들지만 괜찮아요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "조금", "start_ms": 60, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "힘들지만", "start_ms": 190, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "괜찮아요", "start_ms": 380, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "화이팅 하세요 응원합니다",
"speaker": "speaker_2",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "화이팅", "start_ms": 50, "dur_ms": 160, "phase_lr_deg": 2.5},
{"word": "하세요", "start_ms": 220, "dur_ms": 130, "phase_lr_deg": 2.5},
{"word": "응원합니다", "start_ms": 360, "dur_ms": 200, "phase_lr_deg": 2.5}
]
},
{
"segment": 45,
"transcript": "고맙습니다 좋은 하루 되세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "고맙습니다", "start_ms": 40, "dur_ms": 190, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 240, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "하루", "start_ms": 350, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "되세요", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "오늘 저녁에 시간 있으세요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 60, "dur_ms": 100, "phase_lr_deg": 2.5},
{"word": "저녁에", "start_ms": 170, "dur_ms": 130, "phase_lr_deg": 2.5},
{"word": "시간", "start_ms": 310, "dur_ms": 100, "phase_lr_deg": 2.5},
{"word": "있으세요", "start_ms": 420, "dur_ms": 140, "phase_lr_deg": 2.5}
]
},
{
"segment": 47,
"transcript": "네 특별한 약속은 없어요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": 2.5},
{"word": "특별한", "start_ms": 155, "dur_ms": 150, "phase_lr_deg": 2.5},
{"word": "약속은", "start_ms": 315, "dur_ms": 120, "phase_lr_deg": 2.5},
{"word": "없어요", "start_ms": 445, "dur_ms": 120, "phase_lr_deg": 2.5}
]
},
{
"segment": 48,
"transcript": "같이 커피 한 잔 할까요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "같이", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 2.5},
{"word": "커피", "start_ms": 170, "dur_ms": 110, "phase_lr_deg": 2.5},
{"word": "한", "start_ms": 290, "dur_ms": 70, "phase_lr_deg": 2.5},
{"word": "잔", "start_ms": 370, "dur_ms": 80, "phase_lr_deg": 2.5},
{"word": "할까요", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 2.5}
]
},
{
"segment": 49,
"transcript": "좋아요 여섯 시에 만나요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "좋아요", "start_ms": 65, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "여섯", "start_ms": 215, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "시에", "start_ms": 335, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 435, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "그때 뵙겠습니다 안녕히 계세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "그때", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "뵙겠습니다", "start_ms": 160, "dur_ms": 190, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 360, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "계세요", "start_ms": 490, "dur_ms": 130, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 08:48:00*
