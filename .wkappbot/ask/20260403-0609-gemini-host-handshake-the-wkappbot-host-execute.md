# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=06:07:52  uptime=17318s  mem=1395MB  system_processes=448"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_060742.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 601-261-263-260-126-263-671-176-176-173-176-167-132-136-126-621-136-312-625-632-362-321-263-265-652-675-126-625-627-625-260-627-570-576-574-756-375-132-102-132-620-136-106-301-3_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 132-103-316-162-612-162-301-312-572-752-106-163-627-632-163-612 613-574-510-156-236-540-105-620-675-675-607-130-321-613-623-612-617-765-756-675-673-672-672-672-671-217-107-135-1_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 570-756-675-765-103-132-137-561-506-106-503-521-605-203-201-321-302-312-570-106-751-302-312-302-301-230-230-203-203-236-321-276-213-267-256-527-720-213-310-203-213-231-502-570-2_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 561-163-106-316-103-103-163-136-136-167-106-601-601-621-163-130-163-261-213-127-361-612-263-657-651-613-362-613-610-631-357-567-563-516-310-162-126-130-103-130-516-615-675-150-7_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 316-301-130-123-321-162-123-132-316-106-106-136-163-136-132-126-632-312-326-231-310-620-160-632-621-213-217-137-103 162-102-216-126-102-103-213-123-123-126-216-312-126-213_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 103-105-105-210-103-136-106-106-136-103-162-512-621-163-163-267-206-162-257-162-126-701-751-167-671-163-163-130-167-613-167-132-132-316-160-132-162-127-623-102-213-672-126-163-2_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 623-126-263-162-367-617-162-123-672-367-576-576-657-671-657-627-576-672-672-765-672-627-652-605-627-620-617-672-276-625-765-652-657-657-567-617-657-657-657-765-567-657-567-657-7_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 160-603-136-162-103-132-106-613-136 576-671-671-163-106-675-672-132-260-120-106-160-367-673-627-672-675-765-672-576-765 625-256-756-657-675-657-651-756-657-567-756-653-103-106-5_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 132-123-163-132-312-312-321-613-632-672-630-367-627-203-320-716-765-672-237-731-706-137-301-203-203-762-762-160-143-326-321-302-760-175-572-325-237-231-312-302-230-302-321-321-1_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 231-521-312-132-123-321-312-317-153-175-762-765-567-256-236-321-312-135-317-312-315-132-123-123-213-123-132-123-216-217-267-126-213-126-216-217-210-271-572-257-257-256-502-315-3_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 765-675-761-725-753-751-620-260-260-620-620-612-620-602-302-163-167-167-106-301-103-310-310-132-123-132-260-620-106-162-612-167-761-627-102-120-203-762-301-237-235-172-206-210-2_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 163-163-106-103-132-312-130-735-175-765-316-761-602-106-612-671-615-602-623-362-136-126-102-136-132-136-150-103-103-150-571-705-763-601-601-176-631-160-167-653-627-620-726-632-4_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 675-635-356-576-562-576-531-567-572-561-526-657-756-651-653-652-156-316-165-615-657-657-156-561-637-567-567-657-156-570-571-675-675-175-675-576-567-571-756-756-675-165-562-612-6_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 607-627-267-507-671-607-610-671-321-176-132-672-672-310-310-103-103-310-103-103-105-136 310-312-261-602-261-267-620-260-602-162-310-310-130-162 126-126-136-106-516-103-106-650-1_V.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 620-612-672-163-316-752-754-756-756-576-567-576-613-536-316-132-302-130-571-756-372-316 672-672-263-263-263-326 630-163-126-517-103-130-136-137-107-103 321-156-132_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 312-316-316-362-236-362-625-236-263-263-236-263-326-236-627-236-236-267-263-362-236-326-236-263-163-316-321-123-236-213-312-132-216-231-231-236-273-726-276-267-263-621-263-625-6_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 103-103-136-103-136-103-510-356-652-705-173-103-615-637-167-361-126-136-106-130-176-603-762-617-675-621-631-621-163-326-312-236-102_V.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 132-136-621-231-123-267-160-163-601-627-670-601-527-265-362-126-617-136-276-267-123-312-310-132-136-163-167-126-123-602-206-106-620-602-601-621-627-126-160-261-260-263-761-310-1_V.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 267-627-216-762-325-267-627-267-627-627-627-670-672-637-627-326-621-671-673-670-617-176-672-362-672-672-627-267-267-267-263-267-267-267-263-267-236-236-762-623-623-632-637-136-6_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 123-126-170-310-302-203-602-657-765-602-672-612-605-601-635-106-103-132-123-236-617-135 631-631-621-621-126-132-321-106-163-126-120-163-136-362-130-132-136-162-230-671-671-617-6_V.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 567-657 571-132-312-163-361-321-231-362-263-321-213-231-236-263-263-231-263-326_W.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 315-315-135-135-357-537-357-372-325-372-362-361-136-316-316-135-132-312-136-136-163-165-165-135-165-165-165-157-157-163-167-125-163-173-165-156-157-156-215-213-256-265-132-153-5_W.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 231-326-542-564-315-315-562-675-627-564-564-576-756-752-541-315-317-312-512-546-512-256-261-256-625-564-657-652-657-567-563-357-236-543-513-512-521-573-756-756-540-154-135-316-3_W.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 163-165-123-625-265-523-657-653-657-562-653-651-657-615-651-657-615-165-765-162-265-625-265-652-625-625-325-251-125-215-216 623-627-612-123-132-263-637-617-612_W.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 321-216-231-372-327-207-207-213-213-123-321-321-326-325-312-316-316-316-167-716-175-157-152-132-213-537-527-732-132-315-536-301-231-163-362-326-176-106-176-301-130-173-713-173-1_V.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 756-275-752-576-576-670-670-756-621-723-726-762-763-762-761-176-612-713-765-317-756-176-762-756-756-765-176-765-761-761-761-765-716-756-751-173-726-317-716-761-760-765-716-671-3_W.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 617-164-132-612-163 251-756-756-762-267-206-765-765-706-763-176-174-132-163-237-167-617-613-163-106-167-134 576-756-765-207-207-206-276-136-167-173-216-231-123-136-163-316-321-2_V.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 176-721-136-751-617-163-657-567-571-752-726-731-701-162-126-752-715-716-176-175-751-517-571-157-517-573-106-176-103-126-136-160-621-630-103-136-653-136-136-361-136-276-162-136-1_W.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 137-126-216 765-637-213-637 153-137-271-136-263-613-367_W.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 361-312-167-675-612-106-754-756-754-754-162-206-573-527-620-206-260-620-206-236-135-672-657-627-675-627-267-206-756-752-173 765-670-176-710-751-750-317-132-261-136 132-312-123-1_W.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 261-263-326-236-263-326-231 321-326-236-623-326-132-321-326-263-326-627-627-756-765-765-765-756 216-127-721 765-316-310-312-362-326-261-675-367-361-630-302-632-602-263-206-203-3_W.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 610-657-137-132-316-621-630-123-167-165-106-760-167-637-106-670-103-106-103-160-167-103-316-173-562-617-172-175-160-761-671_V.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 631-176 672-765-763-632-376-736-762-276-762-267-763-713-762-732-276-675-237-726-723-370-763-762-276-273-765-675-671-672-762-726-267-627-752-756-750-720-271-670-652-367-261-137-3_W.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 716-163-310-576-567-675-657-567-675-675-671-163-136-675-576-675-672-672-672-762-671-761-675-675-675-675-675-762-765-765-765 675-762-726-627-267-276-276-263-572-175-237-672-376-6_W.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 106-162-103-163-176-206-162-130-136-716-167-632-103-301-312-302-156-163-314-765-675-167-176-617-167-162-310-316-136-361-301-607-160-130-715-132-167-160-375-260-623-301-132-103 6_V.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 637-326-362-732-632-672-632-361-621-316-361-617-361-326-261-623-263-361-623-632-326-267-627-623-672-672-367-623-627-763-672-362-673-376-627-637-627-672-672-672-671-762-761-672-6_W.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 765 756-756-765-756-763-765-760-761 756-165-176 756-756-705-751-715 136-612-675-637-267-623-625-132-716-672-627-675-675-765-756-570 156-673-176-765-765-761-762-267-167-602-361-3_W.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 132-132-103-103-754-762-675-310-103-103-132-301-103-132-103-103-103-103-130-321-213-315-310-675-675-675-756-576-672-627-602-675-657-675-673-176-673-637-623-106-612-612-631-637-6_W.mp3)
  Segment 39: 59888ms ~ 61796ms (file: 132-312-123-216-102-163-136-263-132-160-132-576-756-576-576-261-236-617-263 657-276-725-721-273-607-670-163-103-136-132 615-162 126_W.mp3)
  Segment 40: 62796ms ~ 63372ms (file: 672-726-637-263-675-672-673-631-623-326-236-623-623 621-162_W.mp3)
  Segment 41: 64372ms ~ 64948ms (file: 103-312-603-130-103-315-716-567-657-163-316-132-163-507-307-371-301_V.mp3)
  Segment 42: 65948ms ~ 66524ms (file: 165-165-132-163-167-136-632-136-163-106-136-132-761-167-617-361-561-126-136-136-621-132-136-136-361-106-132-126-136-652-627-765-670-163-136-615-567-230-126-163-627-126-613-130-2_W.mp3)
  Segment 43: 67524ms ~ 68100ms (file: 106-102-623-362-236-216-160-163-106-216-106-675-176-210-206-102-206-276-301-130-312-765-627-627-620-627-620-326-316-321-326-312-132-623-106-167-163-617-576-610-756-263-257-620-6_W.mp3)
  Segment 44: 69100ms ~ 69676ms (file: 206-613 362-321-301-753-756-670-760-367-167-136-632-206-263-260-620-260-621-201-201-206-201-206-261-260-210 136-126-312-136-123-136-136-301 607-103-362-612-306-612-103-163-310-1_V.mp3)
  Segment 45: 70676ms ~ 71252ms (file: 263-263-620-263-312-310-103-301-103-137-321-130-321-132-130-310-531-102-126-106-103-102-213-126-216-102-261-765-637-132-160-160-162-106-163-136-162-102-126-136-261-613-621-631-2_V.mp3)
  Segment 46: 72252ms ~ 72828ms (file: 167-136-103-103-130-130-103-132-763-752-672-301-102-301-670-726-576-567-567-637-103 320-260-326-213-320-301-301-513-315-302-753-326-306-153-306-126-326-160-612-574-574-267-236 3_W.mp3)
  Segment 47: 73828ms ~ 74404ms (file: 613 106-103-106-106-103-163-103-136-103-162-261-123_V.mp3)
  Segment 48: 75404ms ~ 75980ms (file: 316-137-162-125-312-126-261 102 361-314-135-134-126-621_V.mp3)
  Segment 49: 76980ms ~ 77556ms (file: 675-163-167-167-612-172-156-103-103-163-127-160-316-163-526-167 103-132-160-162-165-106-163-516-261-167 310-175-761-316-103-103-751-612-167-623-126-361-612-726-236-620-602 132-1_V.mp3)
  Segment 50: 78556ms ~ 79132ms (file: 106-362-167-136-756-706-670-263-627-621-621-671-230-236-623-670-675-672-637-316-136-316-316-316-613-261-621-362-675-675-670-670-167-167-136-320-130-167-312-206-620-162-610-602-1_V.mp3)
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
> **Time**: 2026-04-03 06:09:01
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "안녕하세요 반갑습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 40, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 280, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "오늘 날씨가 정말 화창하네요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 180, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 340, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "화창하네요", "start_ms": 450, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "산책하기 좋은 날씨입니다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "산책하기", "start_ms": 35, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "좋은", "start_ms": 185, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "날씨입니다", "start_ms": 305, "dur_ms": 200, "phase_lr_deg": 5.0}
]
},
{
"segment": 4,
"transcript": "커피 한 잔 할까요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "커피", "start_ms": 60, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 170, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "잔", "start_ms": 240, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "할까요", "start_ms": 320, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "네 근처 카페로 가요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 45, "dur_ms": 70, "phase_lr_deg": 5.0},
{"word": "근처", "start_ms": 125, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "카페로", "start_ms": 255, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "가요", "start_ms": 395, "dur_ms": 100, "phase_lr_deg": 5.0}
]
},
{
"segment": 6,
"transcript": "아이스 아메리카노 주세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "아이스", "start_ms": 55, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "아메리카노", "start_ms": 205, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 435, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "(music)",
"speaker": "null",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 8,
"transcript": "오늘 기분은 어떠세요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "기분은", "start_ms": 180, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "어떠세요", "start_ms": 330, "dur_ms": 180, "phase_lr_deg": 5.0}
]
},
{
"segment": 9,
"transcript": "아주 상쾌하고 좋네요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "아주", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "상쾌하고", "start_ms": 150, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 340, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "What are your plans for tomorrow?",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "What", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": -2.0},
{"word": "are", "start_ms": 120, "dur_ms": 40, "phase_lr_deg": -2.0},
{"word": "your", "start_ms": 170, "dur_ms": 60, "phase_lr_deg": -2.0},
{"word": "plans", "start_ms": 240, "dur_ms": 110, "phase_lr_deg": -2.0},
{"word": "for", "start_ms": 360, "dur_ms": 50, "phase_lr_deg": -2.0},
{"word": "tomorrow?", "start_ms": 420, "dur_ms": 130, "phase_lr_deg": -2.0}
]
},
{
"segment": 11,
"transcript": "친구를 만나기로 했어요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "친구를", "start_ms": 70, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "만나기로", "start_ms": 220, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "했어요", "start_ms": 410, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "즐거운 시간 보내세요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "즐거운", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "시간", "start_ms": 210, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "보내세요", "start_ms": 330, "dur_ms": 200, "phase_lr_deg": 5.0}
]
},
{
"segment": 13,
"transcript": "감사합니다 당신도요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 45, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "당신도요", "start_ms": 275, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "주말에 영화 볼까요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "주말에", "start_ms": 65, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "영화", "start_ms": 205, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "볼까요", "start_ms": 335, "dur_ms": 150, "phase_lr_deg": 5.0}
]
},
{
"segment": 15,
"transcript": "네 어떤 영화가 좋을까요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 55, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 125, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "영화가", "start_ms": 255, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "좋을까요", "start_ms": 395, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "액션 영화는 어떠세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "액션", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "영화는", "start_ms": 170, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "어떠세요", "start_ms": 320, "dur_ms": 180, "phase_lr_deg": 5.0}
]
},
{
"segment": 17,
"transcript": "좋아요 저도 액션을 좋아해요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "좋아요", "start_ms": 70, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "저도", "start_ms": 200, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "액션을", "start_ms": 290, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "좋아해요", "start_ms": 440, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "그럼 일요일에 만나요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "일요일에", "start_ms": 155, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "만나요", "start_ms": 315, "dur_ms": 180, "phase_lr_deg": 5.0}
]
},
{
"segment": 19,
"transcript": "I'll see you then",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "I'll", "start_ms": 35, "dur_ms": 50, "phase_lr_deg": -2.0},
{"word": "see", "start_ms": 95, "dur_ms": 70, "phase_lr_deg": -2.0},
{"word": "you", "start_ms": 175, "dur_ms": 60, "phase_lr_deg": -2.0},
{"word": "then", "start_ms": 245, "dur_ms": 100, "phase_lr_deg": -2.0}
]
},
{
"segment": 20,
"transcript": "알겠습니다 이따 봐요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "알겠습니다", "start_ms": 60, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "이따", "start_ms": 270, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "봐요", "start_ms": 380, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "점심 메뉴는 정하셨나요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "점심", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "메뉴는", "start_ms": 180, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "정하셨나요", "start_ms": 330, "dur_ms": 200, "phase_lr_deg": 5.0}
]
},
{
"segment": 22,
"transcript": "비빔밥 어떠세요 맛있어요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "비빔밥", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "어떠세요", "start_ms": 200, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "맛있어요", "start_ms": 350, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "네 저도 비빔밥 좋아해요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 55, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "저도", "start_ms": 125, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "비빔밥", "start_ms": 215, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "좋아해요", "start_ms": 365, "dur_ms": 150, "phase_lr_deg": 5.0}
]
},
{
"segment": 24,
"transcript": "그럼 식당으로 갑시다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "식당으로", "start_ms": 155, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "갑시다", "start_ms": 325, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "Let's go have lunch",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Let's", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": -2.0},
{"word": "go", "start_ms": 120, "dur_ms": 50, "phase_lr_deg": -2.0},
{"word": "have", "start_ms": 180, "dur_ms": 70, "phase_lr_deg": -2.0},
{"word": "lunch", "start_ms": 260, "dur_ms": 130, "phase_lr_deg": -2.0}
]
},
{
"segment": 26,
"transcript": "맛있게 드셨나요 배불러요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "맛있게", "start_ms": 65, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "드셨나요", "start_ms": 215, "dur_ms": 160, "phase_lr_deg": 5.0},
{"word": "배불러요", "start_ms": 385, "dur_ms": 140, "phase_lr_deg": 5.0}
]
},
{
"segment": 27,
"transcript": "네 아주 든든하네요 하하",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 55, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 125, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "든든하네요", "start_ms": 235, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "하하", "start_ms": 445, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "소화도 시킬 겸 걸어요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "소화도", "start_ms": 40, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "시킬", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "겸", "start_ms": 290, "dur_ms": 50, "phase_lr_deg": 5.0},
{"word": "걸어요", "start_ms": 350, "dur_ms": 180, "phase_lr_deg": 5.0}
]
},
{
"segment": 29,
"transcript": "It's good to walk after eating",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "It's", "start_ms": 25, "dur_ms": 60, "phase_lr_deg": -2.0},
{"word": "good", "start_ms": 95, "dur_ms": 80, "phase_lr_deg": -2.0},
{"word": "to", "start_ms": 185, "dur_ms": 40, "phase_lr_deg": -2.0},
{"word": "walk", "start_ms": 235, "dur_ms": 100, "phase_lr_deg": -2.0},
{"word": "after", "start_ms": 345, "dur_ms": 110, "phase_lr_deg": -2.0},
{"word": "eating", "start_ms": 465, "dur_ms": 90, "phase_lr_deg": -2.0}
]
},
{
"segment": 30,
"transcript": "오늘 정말 즐거웠습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 60, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 190, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "즐거웠습니다", "start_ms": 300, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "다음에 또 만나요 안녕",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "또", "start_ms": 200, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "만나요", "start_ms": 270, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "안녕", "start_ms": 400, "dur_ms": 130, "phase_lr_deg": 5.0}
]
},
{
"segment": 32,
"transcript": "조심히 들어가세요 바이",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "조심히", "start_ms": 45, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "들어가세요", "start_ms": 195, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "바이", "start_ms": 425, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "See you again soon",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 30, "dur_ms": 60, "phase_lr_deg": -2.0},
{"word": "you", "start_ms": 100, "dur_ms": 50, "phase_lr_deg": -2.0},
{"word": "again", "start_ms": 160, "dur_ms": 100, "phase_lr_deg": -2.0},
{"word": "soon", "start_ms": 270, "dur_ms": 150, "phase_lr_deg": -2.0}
]
},
{
"segment": 34,
"transcript": "안녕히 가세요 화이팅",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 65, "dur_ms": 160, "phase_lr_deg": 5.0},
{"word": "가세요", "start_ms": 235, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "화이팅", "start_ms": 385, "dur_ms": 130, "phase_lr_deg": 5.0}
]
},
{
"segment": 35,
"transcript": "Have a wonderful evening",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "Have", "start_ms": 55, "dur_ms": 100, "phase_lr_deg": -2.0},
{"word": "a", "start_ms": 165, "dur_ms": 30, "phase_lr_deg": -2.0},
{"word": "wonderful", "start_ms": 205, "dur_ms": 150, "phase_lr_deg": -2.0},
{"word": "evening", "start_ms": 365, "dur_ms": 140, "phase_lr_deg": -2.0}
]
},
{
"segment": 36,
"transcript": "저도 들어가 볼게요 슝",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "저도", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "들어가", "start_ms": 150, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "볼게요", "start_ms": 270, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "슝", "start_ms": 420, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "오늘 수고 많으셨어요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 70, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "수고", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "많으셨어요", "start_ms": 310, "dur_ms": 200, "phase_lr_deg": 5.0}
]
},
{
"segment": 38,
"transcript": "내일도 힘내봅시다 얍",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "내일도", "start_ms": 45, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "힘내봅시다", "start_ms": 195, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "얍", "start_ms": 405, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "Good night everyone",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "Good", "start_ms": 35, "dur_ms": 80, "phase_lr_deg": -2.0},
{"word": "night", "start_ms": 125, "dur_ms": 100, "phase_lr_deg": -2.0},
{"word": "everyone", "start_ms": 235, "dur_ms": 220, "phase_lr_deg": -2.0}
]
},
{
"segment": 40,
"transcript": "안녕히 주무세요 꿀잠",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 60, "dur_ms": 160, "phase_lr_deg": 5.0},
{"word": "주무세요", "start_ms": 230, "dur_ms": 180, "phase_lr_deg": 5.0},
{"word": "꿀잠", "start_ms": 420, "dur_ms": 120, "phase_lr_deg": 5.0}
]
},
{
"segment": 41,
"transcript": "내일 만나요 안녕",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "내일", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 160, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "안녕", "start_ms": 320, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "Sweet dreams to you",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Sweet", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": -2.0},
{"word": "dreams", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": -2.0},
{"word": "to", "start_ms": 280, "dur_ms": 60, "phase_lr_deg": -2.0},
{"word": "you", "start_ms": 350, "dur_ms": 100, "phase_lr_deg": -2.0}
]
},
{
"segment": 43,
"transcript": "드디어 금요일입니다 예이",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "드디어", "start_ms": 70, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "금요일입니다", "start_ms": 200, "dur_ms": 220, "phase_lr_deg": 5.0},
{"word": "예이", "start_ms": 430, "dur_ms": 100, "phase_lr_deg": 5.0}
]
},
{
"segment": 44,
"transcript": "불금을 즐겨봅시다 앗싸",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "불금을", "start_ms": 45, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "즐겨봅시다", "start_ms": 195, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "앗싸", "start_ms": 405, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "Happy Friday everyone",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Happy", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": -2.0},
{"word": "Friday", "start_ms": 140, "dur_ms": 120, "phase_lr_deg": -2.0},
{"word": "everyone", "start_ms": 270, "dur_ms": 200, "phase_lr_deg": -2.0}
]
},
{
"segment": 46,
"transcript": "한 주 동안 고생했어요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "한", "start_ms": 65, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "주", "start_ms": 135, "dur_ms": 70, "phase_lr_deg": 5.0},
{"word": "동안", "start_ms": 215, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "고생했어요", "start_ms": 325, "dur_ms": 180, "phase_lr_deg": 5.0}
]
},
{
"segment": 47,
"transcript": "주말 푹 쉬세요 안녕",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "주말", "start_ms": 55, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "푹", "start_ms": 165, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "쉬세요", "start_ms": 235, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "안녕", "start_ms": 395, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "Enjoy your weekend guys",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Enjoy", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": -2.0},
{"word": "your", "start_ms": 150, "dur_ms": 60, "phase_lr_deg": -2.0},
{"word": "weekend", "start_ms": 220, "dur_ms": 140, "phase_lr_deg": -2.0},
{"word": "guys", "start_ms": 370, "dur_ms": 130, "phase_lr_deg": -2.0}
]
},
{
"segment": 49,
"transcript": "월요일에 다시 만나요 흑",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "월요일에", "start_ms": 60, "dur_ms": 160, "phase_lr_deg": 5.0},
{"word": "다시", "start_ms": 230, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "만나요", "start_ms": 340, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "흑", "start_ms": 490, "dur_ms": 50, "phase_lr_deg": 5.0}
]
},
{
"segment": 50,
"transcript": "(noise)",
"speaker": "null",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 06:09:01*
