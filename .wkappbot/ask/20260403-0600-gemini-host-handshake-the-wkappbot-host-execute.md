# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=05:59:15  uptime=16801s  mem=1614MB  system_processes=446"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_055905.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 620-602-627-620-602-130-310-260-260-620-620-260-260-260 263-621-627-620-620-620-620-620-672-670-130-312-541-105-657-567-267-236-362-263-267-620-602-263-326-263-103-302-263-620-6_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 136-301-610-250-526-502-526-231-132-213-102-213-106-172-267-267-267-762-623-657-651-673-160-167-165-621-165-153-137-531-570-325-102-301-135-103-135-135-132-637-103-137-136-236-3_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 631-367-576-765-675-167-675-657-675-602-612-103-165-165-765-657-671-613-617-352-372-632-627-263-632-657-625-631-130-162-321-132-153-132_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 102-201-206-203-651-162-312-216-132-627-675-627-620-320-132-162-231-216-162-106-541-321-351-567-261-263-236-230-230 176-132-312-130-120-216-260-136-103-120-312-301-301-326-671-1_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 567-657-176-156-571-160-756-657-657-620-620-602-621-216-612-627-620-620-620-672-630-623-613-106-135-256 102-132-103-103-132-153-756-132-126-132-123-107-710-153-157-126-260-260-2_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 176-163-163-760-617-261-263-620-130-126-213-670-106-106-163-162-625-106-267-162-136-312-231-103-123-210-652-760-103-136-162-106-136-317-136-103-103-316-136-136-163-613-675-627-6_W.mp3)
  Segment 7: 9456ms ~ 17952ms (file: 576-572-576-567-572-576-572-576-576-572-572-572-576-576-576-576-576-756-756-657-276-312-321-657-567_W.mp3)
  Segment 8: 18952ms ~ 19528ms (file: 261-231-126-763-725-275-725-275-263-623-267-276-671-267-540-513-321-213-125-273-750-756-765-651-265-213-236-213-261-162-623-267-710-173-210-620-607-235-325-213-132-206-321-213-2_W.mp3)
  Segment 9: 20528ms ~ 21104ms (file: 132-126-132-136-163-136-103-123-126-163-123-201-120_V.mp3)
  Segment 10: 22104ms ~ 22680ms (file: 732-736-675-326-326-321-327-327-273-372-216-562-217-231-321-351-275-236-321-263-231-352-315-271-132-315-534-540-542-540-534-517-516-517-175-175-751-754-675-367-765-532-517-576-6_W.mp3)
  Segment 11: 23680ms ~ 24256ms (file: 320-361-372-362-632-627-632-610-126-617-637-165-613-623-176-672-623-612-106-130-362-623-160-765-617-157-570-576-574-165-125-356-561-167-106-602-162-167-167-130-106-136-106-153-1_W.mp3)
  Segment 12: 25256ms ~ 25832ms (file: 132-321-132-312-132-302-312-103-132-130-301-321-310-132-312-301-123-312-310-213-102-310-132-312-213-312-123-132-312-231-213-231 237-126-267-231-213-123-213-213-123-123_V.mp3)
  Segment 13: 26832ms ~ 27408ms (file: 716-713-362-173-176-172-627-576-571-612-316-137-167-176-167-130-103-103-103-103-103-137-132-713-153-176-316-136-157-157-765-756-706-673-763-672-672-671-126-765-705-701-705-103-1_V.mp3)
  Segment 14: 28408ms ~ 28984ms (file: 264-265-653-326-362-136-236-326-236-657-362-365-326-236-625-562-562-652-625-625-261-263-263-546-546-561-564-657-362-261-263-263-261-263-257-265-213-136-631-637-362-627-675-657-2_W.mp3)
  Segment 15: 29984ms ~ 30560ms (file: 163-653-637-617-321-362-621-621-612-163-132-136-312-326-312-326 326-263-321 326-361 216-623-326-263-267-635-362-526-263_W.mp3)
  Segment 16: 31560ms ~ 32136ms (file: 512-524-572-756-237-310-632-317-317-137-371-637-316-316-361-312-537-325-321-327-576-120-132-321-756-157-132-230-326-312-123-751-567-123-213-165-107-103-763-132 175-316 261-216-3_W.mp3)
  Segment 17: 33136ms ~ 33712ms (file: 312-230-302-321-302-103-103-317-301-301-312-302-203-256-537-375-306-236-320-302-312-310-216-130-321-601-672-623-623-312-362-312-213-102-217-321-153-310-167-160-326-301-173-261 6_W.mp3)
  Segment 18: 34712ms ~ 35288ms (file: 263-236-203-230-320-326-236-260-263-263-236_W.mp3)
  Segment 19: 36288ms ~ 36864ms (file: 567-562-657-625-675-567-756-762-620-326-362-362-653 657-675-672-615-675-132 675-670 103-672-617-201-263-312-301-316-321-306-675-637-136-162-162-163-612-360-365-102-612-106-162-1_W.mp3)
  Segment 20: 37864ms ~ 38440ms (file: 216-360-316-320-321-316-136-316-136-103-123-261-206-206-263-263-502-156-162-237-723-236-301-326-732-321-563-562-326-132-206-260-260-623-627-621-206-260-260-260-206-602-620-620-1_V.mp3)
  Segment 21: 39440ms ~ 40016ms (file: 651-672-316-162-126 671-672-267-627-615-627-567-637-612-672-672-617-607-367-612-631-736-362-236-635-673-126-326-132-162-162-126-126 754-654-165-657-175-162-136-546-176-265-165-6_W.mp3)
  Segment 22: 41016ms ~ 41592ms (file: 130-615-326-261-563-126-163 261-631-316-617-165-763-567-315-103 160-671-106-106-712-160 103-163-163-612-621-103-162-321-126-106-613-136-103-152-175-165-675-123-173-136-615-165-1_V.mp3)
  Segment 23: 42592ms ~ 43168ms (file: 162 213-162-167-157-610-213-673 631-132-172-125-136-134-164_V.mp3)
  Segment 24: 44168ms ~ 44744ms (file: 761-107-103-216-621-672-302-102-127-126-137-213-162-126-361-103-163-103-152-360-301-103-106-160-263-231-106-301-231-132-312-613-106-103-103-236-320-167-310-602-765-637-310-132-7_W.mp3)
  Segment 25: 45744ms ~ 46320ms (file: 162-601-675-163-632-612-130-574-627-612-261-162-625-621-627-261-126-106-106-230-123-123-265-625-102-216-260-610-627-260-261-216-260-627-160-126-725-176-261-167-756-617-657-160-1_V.mp3)
  Segment 26: 47320ms ~ 47896ms (file: 574-574-765-216-126-367-760-760-716-621-623-713-176-216-126-716-126-627-672-627-756-316-165-126-216-130-672-632-367-567-351-156-361-167-167-163-127-167-106-601-613-631-613-132-1_W.mp3)
  Segment 27: 48896ms ~ 49472ms (file: 623-527-657-653-635-562-657-652-657-625-625-652-352-276-672-627-326-625-526-657-675-673-725-675-673-572-567-756-572-352-562-576-675-763-267-562-627-632-326-625-765-726-657-267-6_V.mp3)
  Segment 28: 50472ms ~ 51048ms (file: 756-756-567-765-765-765-675-765-765-675-671-316-615-675-265-612-671-361-316-613-612-612-316-763-713-765-765-765-574-572-576-576-267-657-165-165_W.mp3)
  Segment 29: 52048ms ~ 52624ms (file: 627 157 576-756-765-765-672-527-652-765-675-672-216-123-216-160-602-635-627-657-672-176-756-675_W.mp3)
  Segment 30: 53624ms ~ 54200ms (file: 130-263-623-612-621-136-301-316-627-162-617 607-625-615-615-132-106-106-162-126-173-103-134-132 132_V.mp3)
  Segment 31: 55200ms ~ 55776ms (file: 235-253-526-532-532-523-523-257-527-527-351-135-157-157-256-256-325-321-326-326-357-367-317-316-132-361-351-365-325-361-315-356-365-356-357-356-356-352-356-357-375-356-357-753-3_W.mp3)
  Segment 32: 56776ms ~ 57352ms (file: 675-671-167-632 321-235-257-765-137-612-571-157-136-167-312-321-326-362-316-362-362-136-637-657-671-613-613-631-657-675-675-675-637-631-613-765-761 326-312-236-613-316-316 267-2_W.mp3)
  Segment 33: 58352ms ~ 58928ms (file: 672-765-306-632-103-136-153-513-256-201-276-203-103-130-710-751-765-765-671-627-620-623-260-620-126-231-260-260-260 672-305-370_W.mp3)
  Segment 34: 59928ms ~ 60504ms (file: 263-263-632-632-621-621-621-620-672-672-627-263-326-367-361-267-267-627-627-623-627-267-672-672-326-362-312-213-237-213-213-527-567-652-625-263-672-672-627-267-263-362-263-273-3_W.mp3)
  Segment 35: 61504ms ~ 62080ms (file: 540-562-235-326-236-132-312-325-235-231-236-162-231-231-321-231-516-326-236-216-672-263-263-261-326-627-625-236-231-213-237-217-213-213-231-213-231-213-231-261-265-263-237-327-2_W.mp3)
  Segment 36: 63080ms ~ 63656ms (file: 261-276-236-236-236-623-267-216-123-123-273-312-621-123-632-236-327-327-637-327-237-653-650-361-316-361-123-365-362-362-623-562-361-163-312-327-327-362-326-263-736-362-326-367-2_W.mp3)
  Segment 37: 64656ms ~ 65232ms (file: 163-623-316-312-302-312-362-172-236-732-326-301-613-132-361-312-326-236-263-123-312-321-321-326-321-326-261-136-126-621-620-627-231-326-236-312-132-130-361-602-612-321-263-312-2_W.mp3)
  Segment 38: 66232ms ~ 66808ms (file: 312-231-326 657-623-310-301-321-103-301-132-312 132-261-263-267-261-230-203-627-637-623-136-321-236-362-130-136-231-236-361-612-132-136-236-635-615-361-316-627-362-367-615-625-2_W.mp3)
  Segment 39: 67808ms ~ 68384ms (file: 612-672-673-605-576-756-572-576-751-157-326-765-765-576-567-576-321-576-526-567-657-635-526-652-635-532-576-265-576-567-563-657-657-576-562-562-756-576-657-765-752-756-756-725-5_W.mp3)
  Segment 40: 69384ms ~ 69960ms (file: 562-562-567-562-520-652-560-526-652-652-562-675-675-651-615-675-576-675-517-657-516-561-562-567-576-567-650-506-501-153-607-567-506-657-567-506-561-560-516-650-670-675-657-607-6_W.mp3)
  Segment 41: 70960ms ~ 71536ms (file: 167-610-617-572-357-123-621-316-613-632-623-631-126-637-617-671-376-627-236-637-613-613-361-251-251-325-251-315-132-321-317-213-236-356-513-573-517-513-351-156-625-652-152-325-6_W.mp3)
  Segment 42: 72536ms ~ 73112ms (file: 612-160-106-160-123-103-103-765-317-106-602-621-602-601-613-362-367-765-672-320-235-231-312-162-360-321-362-367-620-263-623-532-534-352-320-325-362 321-312-326-231-321-321-321-7_W.mp3)
  Segment 43: 74112ms ~ 74688ms (file: 236-250-512-567-625-675 617-627-672-762-765-760-706-762-607-607-607-167-106-301-103-673-671-567-567-756-576-576-576-675-657-672-675-762-765-576-756-675-760-306-367-320-627-326-2_W.mp3)
  Segment 44: 75688ms ~ 76264ms (file: 326-236-657-256-627-657-576-562-267-267-236-263-236-621-632-623-263-675-672-627-361 675-637 617-216-137-162-326-362-263-231-265-263-263-263-263-236-526-267-267-672-267-267-263-2_W.mp3)
  Segment 45: 77264ms ~ 77840ms (file: 672-627-312-321-321-312-162-312-216-361-632-321-203-216-126-123-312-136-352-513-132-621-637-365-632-213-326-312-316-216-563-762-265-162-136-162-310-326-256-312-312-106-260-625-3_V.mp3)
  Segment 46: 78840ms ~ 82512ms (file: 326-316-327-176-106-163-103-132-361-260-206-752-206-620-267-267-326-267-103-623-213-241-564-312-152-315-231 132-103-132-163-613-217_V.mp3)
  Segment 47: 83512ms ~ 84088ms (file: 103-136 754-751-123-103-106-136-670-657-675-657-627-652-672-657-761-216-361-102-173-316-126-765-751-761-671-671-671-672-671-126-167-672-657-657-657-165-571-576-765-756-137-312-1_W.mp3)
  Segment 48: 85088ms ~ 85664ms (file: 576-726-762-726-267-265-132-546-574-625-517-156-765-765-765-760-356-137-756-735-132-312-321-623 120-120-157-167-762-267-162-621-167-617-617-621-267-631-312-172-216-236-362-160-1_W.mp3)
  Segment 49: 86664ms ~ 87240ms (file: 320-230-203-230-203-320-203-203-206-203-203-230-230-301-210-231-207-762-670-726-206-236-213-321-320-320-230-203 310-312-316-312-763-756-756-275-270-273-206-167-163-130-302-301-3_V.mp3)
  Segment 50: 88240ms ~ 88816ms (file: 617-167-103-123-601-163-612-203-761-613-602-162-623-612-603-132-102-167-106-136-167-761-621-612-162-167-321-127-175-102-103-136-106-163-137-567-615-175-623-276 167-127-165 162-1_V.mp3)
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
> **Time**: 2026-04-03 06:00:22
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
"transcript": "날씨가 정말 화창하네요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "날씨가", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "화창하네요", "start_ms": 290, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "산책하기 좋은 날씨입니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "산책하기", "start_ms": 35, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "좋은", "start_ms": 195, "dur_ms": 100, "phase_lr_deg": 5.0},
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
*Generated by WKAppBot ask gemini — 2026-04-03 06:00:22*
