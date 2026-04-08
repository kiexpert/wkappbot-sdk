# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=31656  time=07:11:29  uptime=12298s  mem=441MB  system_processes=419"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_070813.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 7884ms (file: 130-213-312-231-620-103-102-321-203-260-327-753-765-672-237-237-321-132-130-123-130-124-726-273-236-163-136-312-576-675-236-627-267-621-312-213-273-362-670-310-123-362-130-721-5_W.mp3)
  Segment 2: 8884ms ~ 9460ms (file: 570-576 301-103-312-310-130-132 576-615-125_V.mp3)
  Segment 3: 10460ms ~ 11036ms (file: 132-632-310-730-630-317-176-317-716-173-301-132-365-632-236-731-735-731-376 576-713-751-716-671-176-176-716-107-176-170-572-760-126-261-267-263-235-216-726-526-256-276-372-705-3_W.mp3)
  Segment 4: 12036ms ~ 12612ms (file: 756-765-751 574-573-135-105-574 572-574-547-571-574-573-531-173-170-173-137_W.mp3)
  Segment 5: 13612ms ~ 14188ms (file: 574-576-752-576-627 157-123-325-237-761-275-571-152-536-735-315-271-265-625-675-267-275-312-371-213-275-537-153-312-520-213-135-236-706-203-750-276-623-302-236-263-237-257-625-7_V.mp3)
  Segment 6: 15188ms ~ 15764ms (file: 672-607-632-263-236-123-675-625-627-756-261-257-236-263-562-672-237-326-623-256-361-275-673-627-623-526-261-275-325-627-765-217-620-253-570-736-570-165-235-567-617-267-572-625-6_V.mp3)
  Segment 7: 16764ms ~ 17340ms (file: 261-275-257-276-567-726-562-752-526-263-235-725-572-267-261-263-250-273-125-267-726-267-526-276-752-325-261-126-271-730-312-236-352-123-357-351-537-576-673-763-763-753-375-356-3_V.mp3)
  Segment 8: 18340ms ~ 24712ms (file: 236-503-310-316-623-236-267-261-571-257-231-371-625-251-162-312-126-132-706-126-132-312-316-160-265-756-321-321-730-623-206-310-120-130-136-135-102-301-312-312-237-175-517-627-3_W.mp3)
  Segment 9: 25712ms ~ 26288ms (file: 725-371-761-712-715-761-726-726-726-726-267-726-762-276-762-672-672-761-267-263-271-321-312-231-126-721-637-671-627-762-671-726-726-672-762-372-627-672-672-762-726-762-762-762-7_V.mp3)
  Segment 10: 27288ms ~ 39132ms (file: 251-213-216-720-217-127-573-705-375-753-256-107-130-103-103-706-127-123-362-103-103-216-132-103-123-123-102-102-102-107-310-712-701-730-765-310-270-756-172-137-320-713-103-271-1_V.mp3)
  Segment 11: 40132ms ~ 40708ms (file: 716-327-506-321-236-621-371-271-507-172-753-726-371-237-607-263-507-763-267-270-267-127-275-573-267-207-726-756-623-270-672-627-726-546-156-320-263-367-672-671-106-275-352-675-5_V.mp3)
  Segment 12: 41708ms ~ 42284ms (file: 157-156-516-167-750-723-253-236-570-123-173-253-751-132-751-125-231-507-237-326-276-167-621-103-123-261-102-321-547-527-721-152-207-765_V.mp3)
  Segment 13: 43284ms ~ 49836ms (file: 316-326-316-136-576-576-753-175-165-315-305-637-321-103-321-103-574-326-310-632-236-302-302-376-236-260-301-312-306-360-631-613-316-130-236-263-263-265-175-157-610-621-216-320-3_W.mp3)
  Segment 14: 50836ms ~ 51412ms (file: 367-312-270-263-507-317-130-271-135-312-512-273-265-207-726-325-657-236-263-216-235-132-675-625-573-321-354-627-572-267-632-507-276-621-726-173-657-765-267-312-576 315-130-301-6_W.mp3)
  Segment 15: 52412ms ~ 64256ms (file: 236-312-273-136-130-716-326-123 760-712-273-725-721-230-103-123-671-756-237-102-302-103-217-321-172-215-102-127-310-320-327-657-160-136-312-132-102-135-230-672-126-120-103-103-7_W.mp3)
  Segment 16: 65256ms ~ 65832ms (file: 103-135-156-170-175-175-175-153-125-157-150-103-153-150-157-753-150-170-160-625-651-165-675-672-765-536-371-172-152-156-173-716-176-752-571-507-150-105-713-107-317-137-315-317-3_V.mp3)
  Segment 17: 66832ms ~ 78208ms (file: 312-230-173-107-132-547-320-103-316-315-231-267-573-257-276-527-275-263-120-267-203-267-263-231-267-230-267-276-675-267-321-321-321-276-210-267-230-230-213-547-524-572-321-310-3_W.mp3)
  Segment 18: 79208ms ~ 79784ms (file: 756-136-567-627-275-567-215-267-260-627-750-726-570-267-162-567-756-620-103-136-157-126-217-267-251-173-217-127-123-507-735-263-126-312-123-657-562-127-236-132-216-132-201-312-2_V.mp3)
  Segment 19: 80784ms ~ 81360ms (file: 612-162-316-263-560-517-256-261-236-625-271-635-675-725-235-572 765-672-675-256-506-627-623-136-316-136 276-321-273-236-526-235-326-236-326_V.mp3)
  Segment 20: 82360ms ~ 89092ms (file: 675-130-136-130-320-756-726-236-752-752-230-310-103-321-123-216-237-716-276-276-201-207-276-172-170-765-765-201-721-176-725-267-273-206-706-726-170-715-756-726-176-571-170-715-1_W.mp3)
  Segment 21: 90092ms ~ 90668ms (file: 107-130-316-756-672-723-317-735-370-735-721-137-317-317-310-231-731-123-127-123-173-137-317-137-135-375-753-375-735-735-753-753-730-271 756-756-762-672-312-150-130-137-130 573-3_W.mp3)
  Segment 22: 91668ms ~ 97824ms (file: 267-627-260-576-276-267-130-317-310-176-673-201-126-267-267-267-231-263-310-751-765-267-257-576-275-372-762-107-103-267-267-267-276-725-106-216-172-765-231-627-320-627-236-310-7_W.mp3)
  Segment 23: 98824ms ~ 106888ms (file: 231-301-267-201-367-371-316-317-173-310-376-310-103-136-103-267-267-203-237-327-320-126-102-103-267-263-310-137-132-316-320-132-123-372-326-236-302-132-312-326-312-132-312-107-1_W.mp3)
  Segment 24: 107888ms ~ 118868ms (file: 517-146-163-627-620-762-612-617-132-736-316-612-602-360-603-162-103-261-670-132-137-301-103-132-132-320-203-230-260-627-267-273-502-571-751-320-517-756-507-132-135-315-136-312-3_W.mp3)
  Segment 25: 119868ms ~ 135924ms (file: 163-312-317-310-105-102-127-216-213-132-123-216-126-132-123-126-127-123-275-213-126-127-213-126-132-312-127-162-172-712-267-562-756-572 231-276-207-207 203-263-206-236-756 702-7_W.mp3)
  Segment 26: 136924ms ~ 137500ms (file: 752-236-536-261-371-132-765-756-576-756-756-574-745-257-261-265-512-175-276-623-572-752-567-572-570-321-752 507-725-267-217-762-257-512-571-621-231-165-236-273-631-627-732-263-6_V.mp3)
  Segment 27: 138500ms ~ 139076ms (file: 715-715-176-675-576-576-567-576-756-756-756-754-576-576-567-576-574-753-756-576-756-576-517-675-765-756-567-571-675-567-657-576-567-573-675-763-657-765-756-527-547-576-571-576-5_W.mp3)
  Segment 28: 140076ms ~ 149004ms (file: 635-236-271-132-316-371-327-135-317-631-315-356-536-536-531-356-537-574-547-574-537-230-130-167-132-312-231-321-320-132-167-317-137-317-301-320-320-310-301-310-301-103-413-132-1_V.mp3)
  Segment 29: 150004ms ~ 150580ms (file: 756-756-576-567-567-567-765-672-675-675-673-673-675-763-765-673-763-675-672-672-672-673-673-672-367-673-637-362-376_W.mp3)
  Segment 30: 151580ms ~ 163352ms (file: 136-216-312-326-132-230-120-231-320-213-120-213-263-213-206-103-102-103-176-613-260-621-107-134-602-260-625-265-576-276-276-130-310-103-315-103-103-275-672-263-216-623-261-267-2_W.mp3)
  Segment 31: 164352ms ~ 164928ms (file: 137-312-761-765-205-127-731-127-725-267-217-317-362-125-153-173-317-507-721-257-527-637-762-326-316-572-213-623-527-705-763-371-375-375-375-751-351-753-357-547-574-756-574-573-5_W.mp3)
  Segment 32: 165928ms ~ 166504ms (file: 132-105-571-517-570-637-312-103-613-153-153-321-513-316-310-315-136-136-136-675-321-263-716-172-167-612-216-621-231-123-132-126-312-123-132-216-123-127-163-123-132-137-316-173-7_W.mp3)
  Segment 33: 167504ms ~ 168080ms (file: 752-276-276-267-267-207-236-263-576-267-267-627-361-670-627-672-672-673-672-603-630-362-362-316-236-706-672-275-257-756-571-216-725-257-572-326-256-762-735-765-632-517-163-135-2_V.mp3)
  Segment 34: 169080ms ~ 174480ms (file: 312-372-362-267-752-576-176-570-571-157-312-752-275-276-273-572-103-103-267-236-267-276-136-105-265-572-120-327-371-375-176-160-571-103-153-267-126-106-517-106-175-567-165-571-5_W.mp3)
  Segment 35: 175480ms ~ 176056ms (file: 574-537-715-157-316-314-132 176-176-765-765_W.mp3)
  Segment 36: 177056ms ~ 183392ms (file: 357-617-167-127-671-756-257-725-230-237-627-321-352-316-103-267-263-310-236-136-617-613-673-267-320-237-312-263-321-612-753-725-752-715-751-671-163-176-167-317-236-326-162-372-2_W.mp3)
  Segment 37: 184392ms ~ 191880ms (file: 123-162-163-312-136-136-136-126-120-120-120-132-123-132-231-231-123-261-213-126-132-213-230-237-312-312-103-164-765-236-267-326-310-753-756-375-102-106-130-754-257-753-671-132-3_W_stt_off_the_world_as_we_can_best_allies_and.mp3)
  Segment 38: 192880ms ~ 193456ms (file: 706-736-517 572-756-576-574-765-675 675-752-756-750-317-517_W.mp3)
  Segment 39: 194456ms ~ 195032ms (file: 325-725-175-621-271-162-326-754-236-750-256-267-321-275-321-132-312-762-570-572-625-312-627-570-567-756-572-215-372-312-576-312-506-236-675-362-162-572-132-127-623-526-213-706-2_V.mp3)
  Segment 40: 196032ms ~ 204420ms (file: 126-102-103-761-615-106-107-765-675-265-206-216-726-276-751-725-217-572-725-217-302-230-230-206-206-201-260-203-320-231-261-765-765-201-657-654-756-167-230-201-106-132-103-103-1_W.mp3)
  Segment 41: 205420ms ~ 205996ms (file: 213-123-132-132-312-123-120-132-312-132-123-132-135-135-750-517-150-175-107-175-170-175-713-170-715-705-753-701-756-507-751-705-750-751-107-120-120-132-312-132-126-160-130-123-2_W.mp3)
  Segment 42: 206996ms ~ 207572ms (file: 231-123-352-627-231-236-725-263-602-705-321-762-261-672-537-507_V.mp3)
  Segment 43: 208572ms ~ 217464ms (file: 136-132-123-231-130-176-167-267-106-132-217-236-236-327-312-574-514-150-210-102-765-756-756-756-765-675-160-607-215-612-132-130-271-102-216-126-216-260-216-260-206-123-132-102-7_W.mp3)
  Segment 44: 218464ms ~ 219040ms (file: 152-157-157-157-576-571-576-547-765-567_W.mp3)
  Segment 45: 220040ms ~ 220616ms (file: 675-537-657-756-516-576-576-576-561-572-516-361-315-326-326-576-573-563-574-506-572-352-351-302-126-123-126-130 632-123-137-531-106-103-106-621-175-572-756-513-756-576-375-571-5_W.mp3)
  Segment 46: 221616ms ~ 228024ms (file: 261-320-762-132-236-563-310-527-206-321-216-625-312-263-123-216-263-627-236-312-106-136-316-531-106-213-130-102-106-267-675-302-230-326-362-326-263-231-213-126-106-260-625-256-2_V.mp3)
  Segment 47: 229024ms ~ 237592ms (file: 175 702-720-310-176-267-756-675-607-136-103-310-310-312-135-301-651-602-763-150-236-762-157-157-135-165-360-105-657-326-756-106-103-137-307-301-301-632-203-201-203-237-236-230-2_W_stt_the.mp3)
  Segment 48: 238592ms ~ 239168ms (file: 612-756-573-160-367-372-361-316-316-137-167-163-671-176-167-136-637-316-317-310-752-132-321-326-231-135-512-203-123-312-531-735-705-267-502-312-752-726-127-237-261-326-126-752-1_V.mp3)
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
> **Time**: 2026-04-06 07:12:46
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "안녕하세요 만나서 정말 반갑습니다 오늘 날씨가 참 좋네요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 130, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "만나서", "start_ms": 1050, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 1700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 2200, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 3500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 4000, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "참", "start_ms": 4650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 5000, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 3,
"transcript": "Great to see you",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "Great", "start_ms": 110, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "to", "start_ms": 370, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "see", "start_ms": 480, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "you", "start_ms": 640, "dur_ms": 200, "phase_lr_deg": -5.0}
]
},
{
"segment": 4,
"transcript": "이것은 한국어 문장입니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "이것은", "start_ms": 80, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "한국어", "start_ms": 600, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "문장입니다", "start_ms": 1250, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "Let's study together",
"speaker": "speaker_3",
"confidence": 0.92,
"language": "en",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "Let's", "start_ms": 150, "dur_ms": 300, "phase_lr_deg": 10.0},
{"word": "study", "start_ms": 460, "dur_ms": 400, "phase_lr_deg": 10.0},
{"word": "together", "start_ms": 880, "dur_ms": 550, "phase_lr_deg": 10.0}
]
},
{
"segment": 6,
"transcript": "언어 학습은 정말 재미있어요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "언어", "start_ms": 120, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "학습은", "start_ms": 550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 1100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "재미있어요", "start_ms": 1550, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "How about a song?",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "How", "start_ms": 90, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "about", "start_ms": 300, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "a", "start_ms": 610, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "song?", "start_ms": 720, "dur_ms": 400, "phase_lr_deg": -5.0}
]
},
{
"segment": 8,
"transcript": "노래방에서 노래를 불러봐요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "노래방에서", "start_ms": 140, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "노래를", "start_ms": 1100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "불러봐요", "start_ms": 1750, "dur_ms": 750, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 10,
"transcript": "반짝반짝 작은 별 아름답게 비치네",
"speaker": "speaker_4",
"confidence": 0.91,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "mixed",
"words": [
{"word": "반짝반짝", "start_ms": 200, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "작은", "start_ms": 1500, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "별", "start_ms": 2200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "아름답게", "start_ms": 3000, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "비치네", "start_ms": 4200, "dur_ms": 900, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "Sing along with me",
"speaker": "speaker_3",
"confidence": 0.93,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "Sing", "start_ms": 100, "dur_ms": 300, "phase_lr_deg": 10.0},
{"word": "along", "start_ms": 420, "dur_ms": 400, "phase_lr_deg": 10.0},
{"word": "with", "start_ms": 850, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "me", "start_ms": 1070, "dur_ms": 300, "phase_lr_deg": 10.0}
]
},
{
"segment": 12,
"transcript": "발음 연습을 시작합니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "발음", "start_ms": 150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "연습을", "start_ms": 650, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "시작합니다", "start_ms": 1250, "dur_ms": 900, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "Please repeat after the audio",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "Please", "start_ms": 120, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "repeat", "start_ms": 550, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "after", "start_ms": 1100, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "the", "start_ms": 1480, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "audio", "start_ms": 1650, "dur_ms": 500, "phase_lr_deg": -5.0}
]
},
{
"segment": 14,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 15,
"transcript": "사과 배 포도 딸기",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "사과", "start_ms": 180, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "배", "start_ms": 1000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "포도", "start_ms": 1600, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "딸기", "start_ms": 2400, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "Apple pear grape strawberry",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "Apple", "start_ms": 110, "dur_ms": 450, "phase_lr_deg": -5.0},
{"word": "pear", "start_ms": 700, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "grape", "start_ms": 1200, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "strawberry", "start_ms": 1800, "dur_ms": 700, "phase_lr_deg": -5.0}
]
},
{
"segment": 17,
"transcript": "잘 하셨습니다 다음으로 넘어갈까요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "잘", "start_ms": 130, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "하셨습니다", "start_ms": 400, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "다음으로", "start_ms": 1500, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "넘어갈까요", "start_ms": 2200, "dur_ms": 850, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "Next lesson is about colors",
"speaker": "speaker_3",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 95,
"audio_type": "speech",
"words": [
{"word": "Next", "start_ms": 95, "dur_ms": 300, "phase_lr_deg": 10.0},
{"word": "lesson", "start_ms": 420, "dur_ms": 450, "phase_lr_deg": 10.0},
{"word": "is", "start_ms": 900, "dur_ms": 150, "phase_lr_deg": 10.0},
{"word": "about", "start_ms": 1100, "dur_ms": 350, "phase_lr_deg": 10.0},
{"word": "colors", "start_ms": 1500, "dur_ms": 500, "phase_lr_deg": 10.0}
]
},
{
"segment": 19,
"transcript": "빨간색 파란색 노란색 초록색",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "빨간색", "start_ms": 120, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "파란색", "start_ms": 950, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "노란색", "start_ms": 1800, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "초록색", "start_ms": 2650, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "Red blue yellow green",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "Red", "start_ms": 100, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "blue", "start_ms": 600, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "yellow", "start_ms": 1150, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "green", "start_ms": 1750, "dur_ms": 500, "phase_lr_deg": -5.0}
]
},
{
"segment": 21,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 22,
"transcript": "이제 문장을 만들어 봅시다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 140, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "문장을", "start_ms": 600, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "만들어", "start_ms": 1250, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "봅시다", "start_ms": 1900, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "I like the color red",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 110, "dur_ms": 150, "phase_lr_deg": 10.0},
{"word": "like", "start_ms": 280, "dur_ms": 250, "phase_lr_deg": 10.0},
{"word": "the", "start_ms": 550, "dur_ms": 100, "phase_lr_deg": 10.0},
{"word": "color", "start_ms": 680, "dur_ms": 400, "phase_lr_deg": 10.0},
{"word": "red", "start_ms": 1150, "dur_ms": 400, "phase_lr_deg": 10.0}
]
},
{
"segment": 24,
"transcript": "저는 빨간색을 좋아해요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 130, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "빨간색을", "start_ms": 650, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "좋아해요", "start_ms": 1450, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "What is your favorite color?",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "What", "start_ms": 120, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "is", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "your", "start_ms": 580, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "favorite", "start_ms": 800, "dur_ms": 550, "phase_lr_deg": -5.0},
{"word": "color?", "start_ms": 1400, "dur_ms": 450, "phase_lr_deg": -5.0}
]
},
{
"segment": 26,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 27,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 28,
"transcript": "오늘 수업은 여기서 마치겠습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "수업은", "start_ms": 600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "여기서", "start_ms": 1200, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "마치겠습니다", "start_ms": 1850, "dur_ms": 1000, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 30,
"transcript": "수고하셨습니다 다음에 또 만나요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "수고하셨습니다", "start_ms": 140, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "다음에", "start_ms": 1500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 2050, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 2300, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "Goodbye everyone",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "Goodbye", "start_ms": 90, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "everyone", "start_ms": 650, "dur_ms": 600, "phase_lr_deg": -5.0}
]
},
{
"segment": 32,
"transcript": "See you next time",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 110, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "you", "start_ms": 350, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "next", "start_ms": 600, "dur_ms": 350, "phase_lr_deg": 10.0},
{"word": "time", "start_ms": 1000, "dur_ms": 400, "phase_lr_deg": 10.0}
]
},
{
"segment": 33,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 34,
"transcript": "마지막 확인 질문입니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "마지막", "start_ms": 130, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "확인", "start_ms": 700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "질문입니다", "start_ms": 1200, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "Ready to finish?",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "Ready", "start_ms": 80, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "to", "start_ms": 500, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "finish?", "start_ms": 700, "dur_ms": 500, "phase_lr_deg": -5.0}
]
},
{
"segment": 36,
"transcript": "네 수고 많으셨습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 120, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "수고", "start_ms": 550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "많으셨습니다", "start_ms": 1050, "dur_ms": 1000, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "the world as we can best allies and",
"speaker": "speaker_3",
"confidence": 0.88,
"language": "en",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "the", "start_ms": 150, "dur_ms": 150, "phase_lr_deg": 10.0},
{"word": "world", "start_ms": 350, "dur_ms": 400, "phase_lr_deg": 10.0},
{"word": "as", "start_ms": 800, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "we", "start_ms": 1050, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "can", "start_ms": 1300, "dur_ms": 300, "phase_lr_deg": 10.0},
{"word": "best", "start_ms": 1700, "dur_ms": 400, "phase_lr_deg": 10.0},
{"word": "allies", "start_ms": 2200, "dur_ms": 550, "phase_lr_deg": 10.0},
{"word": "and", "start_ms": 2800, "dur_ms": 300, "phase_lr_deg": 10.0}
]
},
{
"segment": 38,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 39,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 40,
"transcript": "프로그램을 종료합니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "프로그램을", "start_ms": 140, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "종료합니다", "start_ms": 1100, "dur_ms": 900, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "System shutdown complete",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "System", "start_ms": 100, "dur_ms": 450, "phase_lr_deg": -5.0},
{"word": "shutdown", "start_ms": 600, "dur_ms": 600, "phase_lr_deg": -5.0},
{"word": "complete", "start_ms": 1250, "dur_ms": 700, "phase_lr_deg": -5.0}
]
},
{
"segment": 42,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 43,
"transcript": "안녕히 가세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 120, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 800, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 45,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 46,
"transcript": "감사합니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 100, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "the",
"speaker": "speaker_3",
"confidence": 0.90,
"language": "en",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "the", "start_ms": 200, "dur_ms": 250, "phase_lr_deg": 10.0}
]
},
{
"segment": 48,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 07:12:46*
