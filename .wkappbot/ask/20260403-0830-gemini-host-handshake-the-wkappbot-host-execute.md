# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=08:29:29  uptime=25815s  mem=1841MB  system_processes=446"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_082919.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 671-675-675-657-765-675-672-675-765-675-675-675-675-657-657-657-627-672-275-725-637-673-714-375-713_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 217-126-215-165-165-312-127-153-125-312-136-163-123-631-321 317-617-537-547-534-135-137-173-173 327-327-132-736-376-137-137-631-136-316-136-357-371-372-371-371-372-315-371-317-5_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 765-762-726-765-763-317-657-623-310-136-162-316-613-623-263-132-216-213-657-675-672-267_W.mp3)
  Segment 4: 4728ms ~ 6636ms (file: 126-123-576-576-576-576-576-576-576-576-576-765-570-132_W.mp3)
  Segment 5: 7636ms ~ 8212ms (file: 167-157-256-123-136-572-160-163-206-602-627-612-621-216-153-103-176-652-162-210-176-162-612-601-162-167-103-107-671-627-602-613-106-156-261-562-102-136-162-137-512-514-612-175-1_V.mp3)
  Segment 6: 9212ms ~ 9788ms (file: 201-627-602-632-167-136-103-136-123-103-136-750-705-576-756-103-136-670-607-620-206-206-620-627 136-316-723-570-574-547-306-163-162-163-213-136-137-603-360-630-613-615-165-106-1_V.mp3)
  Segment 7: 10788ms ~ 11364ms (file: 260-203-230-320-302-320-302-123-312-302-306-301-312-136-627-765-632-623-236-316-263-623-263-621-627-627-627 675-312-317-715-756-276-270-103-106-160-631-136-301-301-576-620-260-3_V.mp3)
  Segment 8: 12364ms ~ 12940ms (file: 231-216-321-235-256-654-163-103-103-132-316-163-103-230-632-136-162-137-126-310-132-125-167-176-107-175-103-163-271-120-751-612 132-657-651-163-176-127-167-123-160-136-162-216-1_V.mp3)
  Segment 9: 13940ms ~ 14516ms (file: 160-136-163-103-130-106-352-157-651-613-263 132-126-157_V.mp3)
  Segment 10: 15516ms ~ 16092ms (file: 103-621-263-321-672-362-627-627-672-672-617-617-136-160-106-136-106-316-623-716-765-130-617-265-712-627-103-163-316-760-103-130-103-157-123-126-615-130-136-103-123-312-130-301-3_V.mp3)
  Segment 11: 17092ms ~ 17668ms (file: 537-572-120-123-251-217-527-253-713-261-103-652-657-675-361-637-367-106-135-167-176-760-765-672-176-251-263-312-372-637-763-327-176 574-574-574-574-576-576-576_W.mp3)
  Segment 12: 18668ms ~ 19244ms (file: 106-103-750-756-617-617-617-176-136-675-706-670-167-136-106-167-176-576-567-213-203-312-301-310-316-516-637-136-306-302-236-312-106-601-560-160-136-310-310-130-365-301-103-132-1_W.mp3)
  Segment 13: 20244ms ~ 20820ms (file: 672-607-201-102-103-517-103-637-106-103_W.mp3)
  Segment 14: 21820ms ~ 22396ms (file: 576-567-576-567-567-567-576-576-576-567 576-576-567-576-657-576-657-657-567-653-652-263-652-576-572-752-567-526-123-213-132-576-567 135-103_W.mp3)
  Segment 15: 23396ms ~ 23972ms (file: 321-132-567-567-567-657-574-657-576-567-567-756-673-765-612-367-657-576-567-576-765-675-654-675-576-756-576-576-576-652-576-756-576-673-576-627-137-362 762_W.mp3)
  Segment 16: 24972ms ~ 25548ms (file: 326-267-620-562-265-265-256-162-106 617-617-265-265-263-263-263-236-267-362-136-162-321-265-352-625-265-567-567-562-567-356-561-657-657-627-657-657-627-652-567-635-576-672-267-6_W.mp3)
  Segment 17: 26548ms ~ 27124ms (file: 630-602-162-613-132-620-260-632-623-620-136-163-136-157-135-106-136-103-312-167-136-316-163-176-103-501-172 123-125-237-623-126-601-103-123-136-136-653-612-130-167-103-167-762-1_V.mp3)
  Segment 18: 28124ms ~ 28700ms (file: 657-756-567-651-126-261-162-103-310-301-130-312-132-612-162-132-320-360-302-631-630-361-601-130-160-130-316-136-163-103-672-657-657-761 657-657-567-567-567-567-657-602-657-657-6_W.mp3)
  Segment 19: 29700ms ~ 30276ms (file: 167-176-162-176-162-132-361-765-136-206-206-206-260-201-126-216-261-312-132-316-327-623-132-316-316-136-103-103-132-316-312-231-230-320-302-312-321-576-175-756-132-312 621-627-2_V.mp3)
  Segment 20: 31276ms ~ 31852ms (file: 675-673-754-756-765-735-765-761-763-672-675-765-763-765-761-761-756-574-547-547-576-756-765-765-576-126-162-132-765-765-567-762-765-765-765-716-763-736-670-617-265-263-216-261-7_W.mp3)
  Segment 21: 32852ms ~ 33428ms (file: 627-762-261-261-652-652-621-762-672-761-763-762-765-675-625 756-756-756 126-163-621-756-576-562-152-256-162-310-130-321-231-263-260-263-132-130-132-103-132-162-261-261-261-360-3_V.mp3)
  Segment 22: 34428ms ~ 35004ms (file: 106-167-165-102-167-126-326-302-310-301-130-261-123-216-312-230-326-236-230-203-236-320-320-320-326-162-612-602-627-126-362-263-123-321-173-621-762-307-103-136-163-627-267-627-2_V.mp3)
  Segment 23: 36004ms ~ 36580ms (file: 103-103-130-301-103-312-134-312-317-316-137-132-103-103-106-132-103-132-162-136-103-612-162-316-230-302-136-315-301-137-217-106-136-167-106-163-162-162-132-123-216-167-102-571-1_V.mp3)
  Segment 24: 37580ms ~ 38156ms (file: 231-627-625-576-756-765-267-623-236-632-321-326-361-362-236-236-326-567-657-362-326-312-132-715-756-756-762-276-236-326-572-756-376-372-321-321-756-765-672-627-267-263-257-572-5_W.mp3)
  Segment 25: 39156ms ~ 39732ms (file: 176-261-621-754-751-715-715-310-130-103-130-132-312-631-312-130-312-612-637-260-263-310-623-623-761-761-103-670-213-162-103-316-163-132-231 263-632-362-612-623-263-316-623-617-6_W.mp3)
  Segment 26: 40732ms ~ 41308ms (file: 326-263-236-326-261 762-327-267-367-762-612-265-567-567-652-263-627-267-265-657-627-623-625-623-276-627-675-236-637-231-623-623-236-613-123-236-623-261-623-627-321-123-312-362-3_W.mp3)
  Segment 27: 42308ms ~ 42884ms (file: 124 135-132-137 237-367-123-132-120-102_V.mp3)
  Segment 28: 43884ms ~ 44460ms (file: 175-175-135-153-143-132-163-574-576-574-574-576-574-156-176-137-574-507-574-137-312-136-130-136-103-163-136-134-157-517-301-312-301-312-125-302-320-130-362-236-362-326-362-306-3_V.mp3)
  Segment 29: 45460ms ~ 46036ms (file: 657-631-613-136-137-150-631-712-132-106-617-163-160-162-136-103-132-102-103-103-127-172-103-136_V.mp3)
  Segment 30: 47036ms ~ 47612ms (file: 301-160-160-160-120-162-126-361-102-102-162-153-175-620-620-631-657-567-610-650-106-716-167-176-517-537-306-637-762-263-236-231-103-130-367-361-162-136-163-173-617-302-301-361-3_W.mp3)
  Segment 31: 48612ms ~ 49188ms (file: 173-103-301-136-103-106-675-617-167-236-206-260-260-672-632-326-263-276-523-576-576-562-756-276-372-672-623-623-670-613-306-672-316-316-306-375-236-230-657-257-162-623-136-162-3_W.mp3)
  Segment 32: 50188ms ~ 50764ms (file: 315-103-276-275-273-325-532-215-132-125-132-213-132-251-652-207-572-572-725-761-612-201-573-756-321-273-756-167-706-230-321-132-572-123-312-231-231-627-123-152-107-750-175-375-3_W.mp3)
  Segment 33: 51764ms ~ 52340ms (file: 637-672-307-623-136-670-602-627-612-163-367-367-613-361-367-316-627-167-632-612-672-321-312-613-162-625-756-765-765-105-150-536-130-130-136-317-136-316-316 657-236-231-126-362-6_W.mp3)
  Segment 34: 53340ms ~ 53916ms (file: 627-632 675-675-306-613 762-637-263 627-672_W.mp3)
  Segment 35: 54916ms ~ 55492ms (file: 123-136-167 365 132-123-163-613-761-123-137-132-134-672-126-312-132 162 163_V.mp3)
  Segment 36: 56492ms ~ 57068ms (file: 236-231-236-263-261-236-163-216-136-612-651-130-652-612-620-613-160-130-167-315-136-612-216-162-620-103-561-176-106-103-562-135-105-137-762-716-601-630-103-517-123-637-137-170-1_W.mp3)
  Segment 37: 58068ms ~ 58644ms (file: 567-567-130-320-302-103-162-230-236-512-150-231-216-625-260-206-213-102-301-301-136-153-315-130-320-321-310-301-260-206-302-102-253-620-623-135-103 567-567_V.mp3)
  Segment 38: 59644ms ~ 60220ms (file: 657-567-576-562-567-657-576-567-657-765-756-725-576-725-657-576-567-756-576-675-657-652-675-657-567-567-675-657-652-675-562-576-567-576-567-657-162-162-261-265-126-165-135-756-6_W.mp3)
  Segment 39: 61220ms ~ 61796ms (file: 627-620-620-627-620-230-362-765-672-103-103-102-132-310-761-172-103-136-612-136-136-103-102-230-570-317-317-125-276-125-317-321-725-106-123-102-320-231-326-302-123-120-312-310-7_W.mp3)
  Segment 40: 62796ms ~ 63372ms (file: 167-167-123-137-712-301-632-612-613-106-761-156-612 546-612-625-167-312-314-321-257-156-162-312-132-231-154-167-671-613-617-601-106-671-627-657-765-756-765-670-670-765-756-756-7_V.mp3)
  Segment 41: 64372ms ~ 64948ms (file: 163-613-163-132-136-162-132-162 761-162-216-167-106-136-156-615-160-167-107-312-132-130-761-106-163-136-136-160-216-106-163-637-160-671-675-617-603-710-203-126-163-126-576-365-1_V.mp3)
  Segment 42: 65948ms ~ 66524ms (file: 163-130-130-103-160-516-102-103-613-627-132-314-132-765_V.mp3)
  Segment 43: 67524ms ~ 68100ms (file: 510-501-517-507-501-517-251-620-602-520-652-175-531-567-165-163-123-132-136-621-207-210-260-231-103-176-165-175-167-176-167-176-106-163-106-263-361-162-672-730-312-360-367-762-1_V.mp3)
  Segment 44: 69100ms ~ 69676ms (file: 726-162-361-106-120-310-301-301-301-307-317 672 150-351-571-507-576 162-136-362-623-102-632-601 671-671-756-562-657-567-576-567-657-612-261-123-651-567-562-652-652-657-216-316-1_W.mp3)
  Segment 45: 70676ms ~ 71252ms (file: 721-723-273-732-732-726-721-627-327-723-673 671-167-716-137-173-130-756-567-675-761-324-153-132-261-265-265-276-512-572-125-526-572-572-576-625-527-752-725-761-752-573-762-762-7_W.mp3)
  Segment 46: 72252ms ~ 72828ms (file: 132-156-320-703-312-123-321-321-321-123-132-123-135-135-451-756-627_W.mp3)
  Segment 47: 73828ms ~ 74404ms (file: 326-531-513-531-213-231-215-512-513-253-256-215-216-263-263-267-127-257-576-126-162-627-365-162-216-216-167-172-527-516-157-657-136-163-612-126-361-136-137-537-537-531-574-534-5_W.mp3)
  Segment 48: 75404ms ~ 75980ms (file: 265-253-125-267-132-132-625-627-617-603-362-623-620-620-265-261-261-263-231-123-273-572-321-567-267-236-320-210-173-103-631-103-567-150-620-260-320-321-312-132-302-503-756-372-3_W.mp3)
  Segment 49: 76980ms ~ 77556ms (file: 123-103-102-103-106-136-173-637-306-261-163-602-621-260-132-106-162-103-705-136-360-631-632-162-136-163-126-320-203-320-176-617-632-612-601-236-621-675-617-612-621-612-567-203-3_V.mp3)
  Segment 50: 78556ms ~ 79132ms (file: 760-706-726-735-275-127 321-361-613-631-617-613-103-136-612-316-162-163-167-163-301-132-136-136-361-564-627-632-617-672-632-621-617-617-617-361-630-130-167-160-317-310-306-632-6_W.mp3)
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
> **Time**: 2026-04-03 08:30:36
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 2,
"transcript": "이거 얼마예요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "이거", "start_ms": 80, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "얼마예요?", "start_ms": 250, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "삼만 원입니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "삼만", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "원입니다", "start_ms": 240, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 5,
"transcript": "조금만 깎아주세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "조금만", "start_ms": 60, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "깎아주세요", "start_ms": 260, "dur_ms": 260, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "그럼 이만 오천 원에 드릴게요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이만", "start_ms": 150, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "오천", "start_ms": 240, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "원에", "start_ms": 330, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "드릴게요", "start_ms": 400, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "네 감사합니다 여기 있어요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 50, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 120, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "여기", "start_ms": 310, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 420, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "거스름돈 여기 있습니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "거스름돈", "start_ms": 45, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "여기", "start_ms": 260, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 370, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "수고하세요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "수고하세요", "start_ms": 30, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "또 오세요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "또", "start_ms": 35, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "오세요", "start_ms": 150, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "Can I help you with anything?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "Can", "start_ms": 70, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 160, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "help", "start_ms": 210, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 320, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "with", "start_ms": 390, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "anything?", "start_ms": 480, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "I'm looking for a gift.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "I'm", "start_ms": 55, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "looking", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "for", "start_ms": 280, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 350, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "gift.", "start_ms": 400, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
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
"transcript": "누구를 위한 선물인가요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "누구를", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "위한", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "선물인가요?", "start_ms": 310, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "부모님께 드릴 거예요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "부모님께", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "드릴", "start_ms": 240, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 370, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "이 차 세트는 어떠세요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 45, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "차", "start_ms": 100, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "세트는", "start_ms": 170, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "어떠세요?", "start_ms": 330, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "포장도 예쁘고 좋네요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "포장도", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "예쁘고", "start_ms": 200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 360, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "포장해 드릴까요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "포장해", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "드릴까요?", "start_ms": 230, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 21,
"transcript": "네 예쁘게 부탁드려요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 60, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "예쁘게", "start_ms": 130, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "부탁드려요", "start_ms": 280, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "배달도 가능한가요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "배달도", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "가능한가요?", "start_ms": 240, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "네 주소를 알려주세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "주소를", "start_ms": 110, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "알려주세요", "start_ms": 270, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 25,
"transcript": "서울시 강남구입니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "서울시", "start_ms": 45, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "강남구입니다", "start_ms": 210, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "내일까지 배송해 드릴게요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "내일까지", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "배송해", "start_ms": 240, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "드릴게요", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": 0.0}
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
"transcript": "언제쯤 도착할까요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "언제쯤", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "도착할까요?", "start_ms": 200, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "오전 열 시쯤 도착할 거예요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "오전", "start_ms": 55, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "열", "start_ms": 140, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "시쯤", "start_ms": 210, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "도착할", "start_ms": 300, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 410, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "알겠습니다 감사합니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "알겠습니다", "start_ms": 35, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 250, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "신용카드 결제 되나요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "신용카드", "start_ms": 45, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "결제", "start_ms": 240, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "되나요?", "start_ms": 350, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "네 가능합니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 30, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "가능합니다", "start_ms": 100, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 34,
"transcript": "영수증 드릴까요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "영수증", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "드릴까요?", "start_ms": 200, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "네 부탁드려요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 35, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "부탁드려요", "start_ms": 100, "dur_ms": 350, "phase_lr_deg": 0.0}
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
"transcript": "오늘도 행복한 하루 되세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "오늘도", "start_ms": 45, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "행복한", "start_ms": 180, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "하루", "start_ms": 310, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "되세요", "start_ms": 420, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 39,
"transcript": "새로 나온 메뉴가 있나요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "새로", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "나온", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "메뉴가", "start_ms": 250, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "있나요?", "start_ms": 380, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "딸기 케이크가 새로 나왔어요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "딸기", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "케이크가", "start_ms": 150, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "새로", "start_ms": 300, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "나왔어요", "start_ms": 410, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "그거 하나 주세요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "그거", "start_ms": 35, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "하나", "start_ms": 140, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 250, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "포장해 드릴까요? 먹고 가실 건가요?",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "포장해", "start_ms": 60, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "드릴까요?", "start_ms": 190, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "먹고", "start_ms": 320, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "가실", "start_ms": 410, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "건가요?", "start_ms": 500, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "먹고 갈게요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "먹고", "start_ms": 35, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "갈게요", "start_ms": 140, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 45,
"transcript": "커피도 한 잔 드릴까요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "커피도", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 190, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "잔", "start_ms": 260, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "드릴까요?", "start_ms": 350, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 47,
"transcript": "아이스 아메리카노 주세요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "아이스", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "아메리카노", "start_ms": 160, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 370, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "진동벨 울리면 받으러 오세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "진동벨", "start_ms": 45, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "울리면", "start_ms": 210, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "받으러", "start_ms": 320, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "오세요", "start_ms": 410, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "자리가 넓고 쾌적하네요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "자리가", "start_ms": 55, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "넓고", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "쾌적하네요", "start_ms": 290, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "편하신 자리에 앉으세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "편하신", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "자리에", "start_ms": 190, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "앉으세요", "start_ms": 320, "dur_ms": 200, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 08:30:36*
