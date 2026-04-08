# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=30488  time=07:43:05  uptime=14863s  mem=935MB  system_processes=410"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_074201.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 9432ms (file: 312-763-312-102-752-201-130-310-130-136-312-132-216-123-312-103-102-130-157-275-213-132-321-130-670-320-213-143-203-310-752-752-163-207-237-257-201-370-702-103-310-310-260-102-1_W.mp3)
  Segment 2: 10432ms ~ 17956ms (file: 312 756-276-726-726-170-327-762-276-273-752-276-572-725-265-267-107-170-763-713-761-367-756-752-267-267-215-173-576-170-706-263-736-267-267-275-172-574-765-367-573-752-601-107-5_W.mp3)
  Segment 3: 18956ms ~ 19532ms (file: 321-267-326-726-256-572-127-756-362-572-236-236-270-725-267-752_V.mp3)
  Segment 4: 20532ms ~ 21108ms (file: 136-176-125-123-137-715-751-710-375-507-107-175-316-756-756-705-576-765-576-307-301-307-617-310-106-301-136-103-307-103-670-657-675-570-572-231-216-372-502-562-765-267-573-127-2_V.mp3)
  Segment 5: 22108ms ~ 22684ms (file: 675-765-376-617-637-631-637-673-671-637-361-637-316-673-361-315-517-576-574-571-574-576-735-517-675-761-276-716-175-765-617_W.mp3)
  Segment 6: 23684ms ~ 24260ms (file: 750-751-170-751-150-160-163-130-167-615-120-165-165-106-615-567-126-165-126-106-107-156-150-102-156-576-657-576-150-617-165-160-175-657-165-162-756-576-571-531-576-302-302-325-7_W.mp3)
  Segment 7: 25260ms ~ 25836ms (file: 327-371-136-137-137-173-137-173-175-175-175-172-137-132-371-317-231-127_V.mp3)
  Segment 8: 26836ms ~ 27412ms (file: 176-173-716-175-157-751-715 135-130-130-157-175-157-175-157-157-517-157-517-571-756-576-675-751-576-657-175-176-751-751-716 103-130-130-137-175-157-175-157-167-751-567-671-167-1_V.mp3)
  Segment 9: 28412ms ~ 28988ms (file: 623-756-127-527-312-705-267-320-520-257-756-367-507-276-572-756-206-726-257-275-236-136-607-613-570 572-261-275-217-267-276-267-572-527-570-756-635-325-316-627-132-162-267-125-6_V.mp3)
  Segment 10: 29988ms ~ 30564ms (file: 254-132-127-273 316-316-163-231-216-276-132-137-562-576-567-562-725-725-627_W.mp3)
  Segment 11: 31564ms ~ 40348ms (file: 106-103-753-230-321-326-301-130-103-321-260-231-236-326-326-321-132-321-130-136-132-203-126-351-102-601-621-237-236-327-723-236-312-321-213-237-213-217-130-107-107-213-127-752-1_W.mp3)
  Segment 12: 41348ms ~ 49160ms (file: 103-103-167-312-751-103-573-615-621-132-132-103-753-750-756-576-675-756-125-126-120-261-657-275-751-260-726-275-250-216-163-130-610-263-621-231-321-231-260-321-230-627-267-627-2_W.mp3)
  Segment 13: 50160ms ~ 55092ms (file: 362-723-301-127-206-102-263-267-265-230-173-756-705-607-176-705-320-321-203-230-236-235-570-106-127-271-352-372-236-237-267-267-275-756-756-206-756-132-105-360-531-231-271-756-2_W.mp3)
  Segment 14: 56092ms ~ 60880ms (file: 763-617-637-613-637-132-143-213-570-574-154-103-143-751-567-571-513-267-276-201-267-267-267-325-103-756-275-320-103-203-132-231-621-672-361-623-613-617-675-673-763-765-563-670-6_W.mp3)
  Segment 15: 61880ms ~ 62456ms (file: 267-267-652-236-315-132-652-527-762-615-576-273-732-573-362-627-327-231-126-265-256-571-501-123-705-271-765-256-625-237-752-205-752-756-236-671-753-765-761-765-673-765-673-765-7_W.mp3)
  Segment 16: 63456ms ~ 69072ms (file: 312-132-123-210-752-237-237-216-126-132-263-236-123-326-263-627-321-203-120-760-762-571-127-267-627-276-236-236-607-627-230-257-130-267-213-231-321-312-321-312-531-130-102-123-3_W.mp3)
  Segment 17: 70072ms ~ 70648ms (file: 263-230-210-123-613-136-132-601-106-123-261-567-162-657-657-231-361-362-621-261-263-210_W.mp3)
  Segment 18: 71648ms ~ 72224ms (file: 170-705-150-175-715-750-710-705-751-105-570-751 574-574-574-571-576-576-571_W.mp3)
  Segment 19: 73224ms ~ 73800ms (file: 753-715-756-736-716-172-175-176-751-157-157-751-572-751-572-375-753-736-521-157-751-751-571-573-576-157-157-751-756-756-750-574-754-572-750-756-756-571-752-725-675-763-576-750-7_W.mp3)
  Segment 20: 74800ms ~ 75376ms (file: 135-136-135-315-135-132-132-135-132-132-132-135-135-135-253-315-351-132-315-321-312-357-156-317-137-136-165-163-106-137-316-163-136-176-167-162-127-136-126-316-136-573-153-173-1_W.mp3)
  Segment 21: 76376ms ~ 76952ms (file: 756-567-261-547-627-527-263-275-236-276-762-265-276-762-326-570-235-231-215-275-251-236-567-627-602-675-765-765-756-756-756-756-576-135-567-576-756-567-567-576-576-576-576-576-5_W.mp3)
  Segment 22: 77952ms ~ 78528ms (file: 716-715-716-715-716-751-715-716-716 761-607-751-105-715-107-170-715-751-710-715-751-756-765-715-761-715-716-701 715-617-106-751-705-715-107-710-170-715-175-175-176-715-176-716-7_V.mp3)
  Segment 23: 79528ms ~ 85036ms (file: 135-271-317-123-572-762-237-350-327-573-132-130-137-167-305-321-752-135-310-603-315-132-321-132-326-351-132-216-213-127-210-301-753-301-312-130-230-217-720-350-320-372-307-320-3_V.mp3)
  Segment 24: 86036ms ~ 86612ms (file: 675-157-157-135-367-635-367-351-361-367-163-671-617-675-675-675-763_W.mp3)
  Segment 25: 87612ms ~ 88188ms (file: 103-103-103-103-132-126-103-310-132-103-135-103-135-712-361-172-127-576-132-261-325-167-762-367-657-623-257-627-137-750 574-216-527-251-617-126-276-621-756-561-576-126-127-617-6_V.mp3)
  Segment 26: 89188ms ~ 95272ms (file: 320-270-230-230-132-210-312-301-237-312-316-163-312-230-236-230-371-103-217-621-213-326-301-301-163-103-312-137-103-132-637-103-267-362-103-203-206-362-321-360-316-312-237-236-7_W.mp3)
  Segment 27: 96272ms ~ 96848ms (file: 317-316-632-675-657-657-160-106-635-516-675-547-574-567-576-574-756-756-574-576-754-152-253 567-156-263-567-610-165-310-132-123-126-516-362-365-352-756-103-536-165-517-765-576-6_W.mp3)
  Segment 28: 97848ms ~ 105372ms (file: 263-236-236-320-321-231-230-231-230-213-230-203-230-230-201-203-230-302-302-275-275-130-312-270-321-123-207-573-572-170-213-275-257-321-321-201-230-231-102-102-213-213-261-213-2_W.mp3)
  Segment 29: 106372ms ~ 118396ms (file: 751-210-316-263-312-261-103-107-174-102-123 750-172-275-172-712-201-207-276-260-273-236-260-206-263-260-276-675-627-276-216-276-765-275-526-260-120-210-201-276-203-251-321-320-2_W.mp3)
  Segment 30: 119396ms ~ 128720ms (file: 301-230-562-230-320-320-263-602-263-132-230-621-104-310-231-301-310-103-230-236-230-230-206-321-320-230-103-301-132-302-320-321-321-230-320-267-230-203-230-206-527-236-231-263-2_W_stt_every_security_assessment_of_routed_by_s.mp3)
  Segment 31: 129720ms ~ 140808ms (file: 763-210-752-725-756-276-260-216-267-276-753-176-132-132-213-310-310-732-765-203-765-132-672-267-754-301-310-123-301-132-130-203-723-201-206-130-271-201-765-312-167-132-301-752-7_W.mp3)
  Segment 32: 141808ms ~ 142384ms (file: 165-167-157-157-756-751 103-130-130-170-137-175-157-175-517-715-571-751-157-157-175-157-157-571-715-135-157-105-175-751-157-175-175-157-157-157-175-571-157-156-157-175-157-751 1_W.mp3)
  Segment 33: 143384ms ~ 151664ms (file: 267-372-263-210-627-263-126-236-231-236-231-102-143-236-637-321-316-765-627-627-765-670-602-162-106-152-625-103-103-103-231-123-216-326-137-167-263-263-320-316-312-164-263-326-3_W.mp3)
  Segment 34: 152664ms ~ 153240ms (file: 567-563-576-567-576-576-567-576-321-651-652-572-567-367-756-765-576-536-356-365-657-576-765-756-573-567-567-356-567-752-315-371-751-527-526-567-567-257-526-263-145-316-576-576-5_W.mp3)
  Segment 35: 154240ms ~ 160504ms (file: 236-273-120-762-276-267-267-267-120-213-713-752-752-275-617-203-207-276-216-126-216-263-721-132-172-267-712-725-237-267-756-762-276-276-672-627-106-752-276-267-236-312-675-231-3_W.mp3)
  Segment 36: 161504ms ~ 162080ms (file: 576-576-576-574-574-157-153-315-735-175-672-163-136-316-136-136-132-175-321-123-132-123-130-130-130-513-156-512-153-156-156-153-135-153-315-153-125-135-135-325-135-152_W.mp3)
  Segment 37: 163080ms ~ 169380ms (file: 361-132-302-320-627-627-267-730-301-163-267-627-627-260-260-260-672-162-132-564-206-762-627-130-137-217-123-123-120-103-137-372-213-102-372-216-613-267-316-726-123-316-312-231-7_W.mp3)
  Segment 38: 170380ms ~ 183160ms (file: 132-132-123-320-261-201-132-132-103-312-260-230-310-230-310-102-571-312-310-130-320-203-103-321-203-302-103-230-325-130-301-320-102-310-163-263-321-102-102-230-236-320-103-103-3_V.mp3)
  Segment 39: 184160ms ~ 192224ms (file: 210-216-754-751-205-201-162-162-176-176-103-321-230-203-320-320-675-230-302-130-316-130-103-106-375-103-136-751-457-317-321-103-132-137-173-136-170-173-310-126-162-621-627-623-1_V.mp3)
  Segment 40: 193224ms ~ 204780ms (file: 275-123-167-276-201-521-231-236-327-106-571-120-713-132-125-236-235-231-123-351-235-102-623-236-267-762-231-321-132-231-123-231-263-230-312-132-231-312-132-157-213-236-312-213-1_W.mp3)
  Segment 41: 205780ms ~ 214564ms (file: 216-276-207-170-751-107-162-102-102-132-175-107-107-175-175-173-132-203-312-132-103-103-132-716-106-175-765-134-103-312-130-173-276-137-203-301-230-260-321-132-210-231-231-312-1_V.mp3)
  Segment 42: 215564ms ~ 223484ms (file: 610-237-267-106-601-306-103-271-273-236-637-130-103-310-103-163-160-613-276-627-160-716-201-273-307-215-216-216-217-237-251-253-215-213-231-213-216-213-217-201-215-257-265-216-2_W.mp3)
  Segment 43: 224484ms ~ 225060ms (file: 765-736-765-620-716-716-176-761-736-765-765-761-713-736-713-753-316-756-627-752-576-756-672-613-137-301-326-317-301-130-735-312-137-127-756-325-237-672-312-256-620-316-137-321-5_W.mp3)
  Segment 44: 226060ms ~ 226636ms (file: 157-125-152-125-152-125-157-105-107-107-105-105-157-715-103-176-105-106-160-106 132-132-753-570-571-571-517-517-157-105-107-175-127-175-705-710-751-571-570-571-510-751 571-571-5_V.mp3)
  Segment 45: 227636ms ~ 233180ms (file: 123-312-107-103-270-237-705-106-620-107-273-250-675-627-672-573-573-257-231-261-352-517-237-276-263-327-507-237-273-750-301-130-372-537-107-167-357-765-375-137-107-362-570-375-2_W.mp3)
  Segment 46: 234180ms ~ 247104ms (file: 120-756-130-130-213-103-130-216-765-571-130-312-201-210-102-102-130-312-103-310-675-751-132-310-321-751-120-520-321-132-130-132-210-257-257-261-175-205-275-751-762-103-130-203-2_W.mp3)
  Segment 47: 248104ms ~ 260524ms (file: 270-267-752-273-320-130-201-312-267-132-103-136-127-103-107-107-107-705-754-756-754-756-132-321-210-201-103-216-123-213-103-301-231-103-103-102-312-201-542-206-206-206-206-231-3_V.mp3)
  Segment 48: 261524ms ~ 262100ms (file: 750-236-503-571-265-326-602-206-562-103-236-613_V.mp3)
  Segment 49: 263100ms ~ 263676ms (file: 735-753-753-735-316-315-321-321-132-765 130_W.mp3)
  Segment 50: 264676ms ~ 274864ms (file: 372-326-317-753-267-627-537-327-153-136-172-376-317-726-263-317-237-327-137-316-160-326-372-175-715-261-627-756-672-762-267-326-327-263-132-321-267-213-623-237-326-371-132-327-3_W.mp3)
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
> **Time**: 2026-04-06 07:46:09
> **AI**: gemini

---

## Response

{
"segment": 1,
"transcript": "안녕하세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 105,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 105, "dur_ms": 420, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "만나서 반갑습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "만나서", "start_ms": 80, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 300, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "오늘 날씨가 정말 화창하고 좋네요 산책하기 딱 좋은 날씨예요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 150, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 410, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 720, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "화창하고", "start_ms": 1010, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 1470, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "산책하기", "start_ms": 2100, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "딱", "start_ms": 2590, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 2750, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "날씨예요", "start_ms": 2980, "dur_ms": 410, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "Yes, it is a beautiful day for a walk in the park.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "Yes,", "start_ms": 120, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "it", "start_ms": 310, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 420, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 550, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "beautiful", "start_ms": 640, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "day", "start_ms": 1000, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "for", "start_ms": 1210, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 1370, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "walk", "start_ms": 1460, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 1710, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 1840, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "park.", "start_ms": 1950, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "어디로 갈까요?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "어디로", "start_ms": 90, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "갈까요?", "start_ms": 340, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 7,
"transcript": "근처에 있는 남산 타워나 한강 공원 어때요? 둘 다 경치가 아주 좋거든요.",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "근처에", "start_ms": 180, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 510, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "남산", "start_ms": 700, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "타워나", "start_ms": 960, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "한강", "start_ms": 1280, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "공원", "start_ms": 1530, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "어때요?", "start_ms": 1760, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "둘", "start_ms": 2300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 2460, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "경치가", "start_ms": 2590, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 2940, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "좋거든요.", "start_ms": 3160, "dur_ms": 420, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "That sounds like a great idea. I would prefer to go to the river side.",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "That", "start_ms": 200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "sounds", "start_ms": 360, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "like", "start_ms": 590, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 740, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "great", "start_ms": 830, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "idea.", "start_ms": 1040, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 1500, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "would", "start_ms": 1610, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "prefer", "start_ms": 1800, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 2110, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "go", "start_ms": 2240, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 2410, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 2530, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "river", "start_ms": 2640, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "side.", "start_ms": 2870, "dur_ms": 260, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "좋아요 출발합시다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "좋아요", "start_ms": 110, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "출발합시다", "start_ms": 340, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 11,
"transcript": "가기 전에 마실 것 좀 살까요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "가기", "start_ms": 130, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "전에", "start_ms": 320, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "마실", "start_ms": 480, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 650, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 760, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "살까요?", "start_ms": 880, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "Sure, I'll get some coffee. What about you?",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 115,
"audio_type": "speech",
"words": [
{"word": "Sure,", "start_ms": 115, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "I'll", "start_ms": 325, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "get", "start_ms": 455, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "some", "start_ms": 605, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "coffee.", "start_ms": 775, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "What", "start_ms": 1200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "about", "start_ms": 1360, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "you?", "start_ms": 1550, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "저는 아이스티로 할게요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 95,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 95, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "아이스티로", "start_ms": 265, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "할게요", "start_ms": 585, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "Great. Let's find a cafe nearby.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "Great.", "start_ms": 110, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "Let's", "start_ms": 330, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "find", "start_ms": 520, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 690, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "cafe", "start_ms": 780, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "nearby.", "start_ms": 1010, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "저기 모퉁이에 예쁜 카페가 있네요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "저기", "start_ms": 140, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "모퉁이에", "start_ms": 300, "dur_ms": 260, "phase_lr_deg": 0.0},
{"word": "예쁜", "start_ms": 570, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "카페가", "start_ms": 760, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "있네요", "start_ms": 980, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "Let's go inside.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 125,
"audio_type": "speech",
"words": [
{"word": "Let's", "start_ms": 125, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "go", "start_ms": 295, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "inside.", "start_ms": 425, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "카페 분위기가 너무 좋네요. 아늑하고 조용해서 공부하기에도 좋을 것 같아요.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 220,
"audio_type": "speech",
"words": [
{"word": "카페", "start_ms": 220, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "분위기가", "start_ms": 440, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 770, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "좋네요.", "start_ms": 960, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "아늑하고", "start_ms": 1400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "조용해서", "start_ms": 1760, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "공부하기에도", "start_ms": 2080, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "좋을", "start_ms": 2510, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 2700, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "같아요.", "start_ms": 2810, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "I agree. But we are here to relax today, not to study.",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "agree.", "start_ms": 360, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "But", "start_ms": 750, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "we", "start_ms": 910, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "are", "start_ms": 1040, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "here", "start_ms": 1160, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 1330, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "relax", "start_ms": 1440, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "today,", "start_ms": 1730, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "not", "start_ms": 2100, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 2250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "study.", "start_ms": 2360, "dur_ms": 310, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "맞아요 휴식이 필요하죠",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "맞아요", "start_ms": 100, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "휴식이", "start_ms": 290, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "필요하죠", "start_ms": 500, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "요즘 업무가 너무 많아서 많이 피곤했거든요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "요즘", "start_ms": 150, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "업무가", "start_ms": 340, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 560, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "많아서", "start_ms": 720, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 950, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "피곤했거든요.", "start_ms": 1120, "dur_ms": 420, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "You deserve a break. Take it easy today.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "You", "start_ms": 140, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "deserve", "start_ms": 250, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 500, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "break.", "start_ms": 590, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "Take", "start_ms": 950, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "it", "start_ms": 1120, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "easy", "start_ms": 1210, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "today.", "start_ms": 1400, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 23,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 24,
"transcript": "음료 나왔습니다",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "음료", "start_ms": 100, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "나왔습니다", "start_ms": 290, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "Thank you very much.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "Thank", "start_ms": 90, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "very", "start_ms": 360, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "much.", "start_ms": 530, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "고맙습니다. 이제 강변으로 나가볼까요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "고맙습니다.", "start_ms": 120, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 550, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "강변으로", "start_ms": 710, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "나가볼까요?", "start_ms": 960, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "바람도 시원하고 정말 좋네요. 사람이 꽤 많지만 여유로운 느낌이에요.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 160,
"audio_type": "speech",
"words": [
{"word": "바람도", "start_ms": 160, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "시원하고", "start_ms": 390, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 680, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "좋네요.", "start_ms": 900, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "사람이", "start_ms": 1300, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "꽤", "start_ms": 1530, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "많지만", "start_ms": 1640, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "여유로운", "start_ms": 1900, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "느낌이에요.", "start_ms": 2190, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "People in this city really love the river. It's like an oasis in the concrete jungle.",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 210,
"audio_type": "speech",
"words": [
{"word": "People", "start_ms": 210, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 400, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "this", "start_ms": 510, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "city", "start_ms": 640, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "really", "start_ms": 800, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "love", "start_ms": 990, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 1160, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "river.", "start_ms": 1270, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "It's", "start_ms": 1700, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "like", "start_ms": 1830, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "an", "start_ms": 1980, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "oasis", "start_ms": 2090, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 2380, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 2500, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "concrete", "start_ms": 2610, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "jungle.", "start_ms": 2920, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "콘크리트 정글이라는 말이 딱 맞네요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "콘크리트", "start_ms": 110, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "정글이라는", "start_ms": 370, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "말이", "start_ms": 700, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "딱", "start_ms": 860, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "맞네요.", "start_ms": 970, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 31,
"transcript": "저기 보세요 노을이 지기 시작했어요. 하늘 색깔이 정말 예술이네요.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 170,
"audio_type": "speech",
"words": [
{"word": "저기", "start_ms": 170, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "보세요", "start_ms": 340, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "노을이", "start_ms": 650, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "지기", "start_ms": 870, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "시작했어요.", "start_ms": 1030, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "하늘", "start_ms": 1500, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "색깔이", "start_ms": 1690, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 1910, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "예술이네요.", "start_ms": 2100, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "It's breathtaking. I'm so glad we came here.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "It's", "start_ms": 130, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "breathtaking.", "start_ms": 260, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "I'm", "start_ms": 800, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "so", "start_ms": 910, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "glad", "start_ms": 1060, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "we", "start_ms": 1230, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "came", "start_ms": 1340, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "here.", "start_ms": 1530, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "덕분에 저도 힐링이 되는 것 같아요. 같이 와줘서 고마워요.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "덕분에", "start_ms": 180, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "저도", "start_ms": 430, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "힐링이", "start_ms": 620, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "되는", "start_ms": 870, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 1030, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "같아요.", "start_ms": 1140, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 1550, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "와줘서", "start_ms": 1720, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "고마워요.", "start_ms": 1970, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "My pleasure. We should do this more often.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "My", "start_ms": 140, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "pleasure.", "start_ms": 270, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "We", "start_ms": 750, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "should", "start_ms": 860, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "do", "start_ms": 1030, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "this", "start_ms": 1160, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "more", "start_ms": 1310, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "often.", "start_ms": 1480, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 38,
"transcript": "다음에는 다른 공원도 가봐요. 서울에는 예쁜 곳이 정말 많거든요.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "다음에는", "start_ms": 200, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "다른", "start_ms": 490, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "공원도", "start_ms": 680, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "가봐요.", "start_ms": 930, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "서울에는", "start_ms": 1300, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "예쁜", "start_ms": 1590, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "곳이", "start_ms": 1760, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 1920, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "많거든요.", "start_ms": 2110, "dur_ms": 340, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 40,
"transcript": "That's a plan. I'm looking forward to it.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "That's", "start_ms": 110, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 280, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "plan.", "start_ms": 370, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "I'm", "start_ms": 750, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "looking", "start_ms": 860, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "forward", "start_ms": 1050, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 1280, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "it.", "start_ms": 1390, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 42,
"transcript": "어느새 어두워졌네요. 이제 저녁 먹으러 갈까요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 160,
"audio_type": "speech",
"words": [
{"word": "어느새", "start_ms": 160, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "어두워졌네요.", "start_ms": 420, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 900, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "저녁", "start_ms": 1060, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "먹으러", "start_ms": 1250, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "갈까요?", "start_ms": 1470, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "Yes, I'm getting hungry. What do you want to eat?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "Yes,", "start_ms": 120, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "I'm", "start_ms": 280, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "getting", "start_ms": 390, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "hungry.", "start_ms": 580, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "What", "start_ms": 1000, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "do", "start_ms": 1150, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 1260, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "want", "start_ms": 1370, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 1530, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "eat?", "start_ms": 1640, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "음... 치맥 어때요? 한강에서는 치맥이 최고죠.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 190,
"audio_type": "speech",
"words": [
{"word": "음...", "start_ms": 190, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "치맥", "start_ms": 550, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "어때요?", "start_ms": 770, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "한강에서는", "start_ms": 1200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "치맥이", "start_ms": 1560, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "최고죠.", "start_ms": 1790, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "That sounds perfect! Let's order some chicken.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "That", "start_ms": 100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "sounds", "start_ms": 260, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "perfect!", "start_ms": 450, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "Let's", "start_ms": 850, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "order", "start_ms": 1020, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "some", "start_ms": 1210, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "chicken.", "start_ms": 1360, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "제가 주문할게요. 금방 올 거예요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "제가", "start_ms": 90, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "주문할게요.", "start_ms": 250, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "금방", "start_ms": 650, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "올", "start_ms": 840, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "거예요.", "start_ms": 950, "dur_

---
*Generated by WKAppBot ask gemini — 2026-04-06 07:46:09*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=30344  time=07:44:57  uptime=14713s  mem=978MB  system_processes=411"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_074333.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 132-236-167-163-617-320 657-657-657-657-657-567_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 657-132-136 571-361-163-136-613-167-756-652-276-276-725-712-627-617-576-754-326-162-702-517-567-516-125-723-325-106-163-725-136-157-126-570-236-762-705-216-132-167-712-513-751-5_V.mp3)
  Segment 3: 3152ms ~ 14708ms (file: 275-123-167-276-201-521-231-236-327-106-571-120-713-132-125-236-235-231-123-351-235-102-623-236-267-762-231-321-132-231-123-231-263-230-312-132-231-312-132-157-213-236-312-213-1_W.mp3)
  Segment 4: 15708ms ~ 21468ms (file: 572-627-265-267-276-756-756-716-763-312-623-567-576-275-236-327-573-517-157-571-157-531-756-751-326-613-132-123-135-310-517-173-130-327-726-602-632-620-602-610-612-620-632-625-2_W.mp3)
  Segment 5: 22468ms ~ 23044ms (file: 361 623-132-375-307 672 570 735-671-657-615-126_V.mp3)
  Segment 6: 24044ms ~ 24620ms (file: 756-175-175-756-715-170-705-751-571-756-756-751-715-751-751-751-751-751-756-756 157-175-571-175-571-750-571-751-715-751-751-753-751-756-573-750-751-570-571-517-175-751-751-571-7_V.mp3)
  Segment 7: 25620ms ~ 33288ms (file: 152-102-120-723-572-321-230-235-123-201-213-123-213-312-130-103-201-213-120-102-103-123-213-310-312-301-217-231-201-103-236-213-123-123-276-263-271-372-231-231-201-216-231-230-3_V.mp3)
  Segment 8: 34288ms ~ 41632ms (file: 216-217-312-231-321-327-273-237-231-231-231-217-237-213-213-231-213-210-276-263-312-132-127-751-572-173-102-172-231-213-130-712-675-102-267-302-102-123-327-123-216-231-263-213-2_W.mp3)
  Segment 9: 42632ms ~ 43208ms (file: 254-132-127-273 316-316-163-231-216-276-132-137-562-576-567-562-725-725-627_W.mp3)
  Segment 10: 44208ms ~ 44784ms (file: 765-107-175-107-175-107-216-751-130_V.mp3)
  Segment 11: 45784ms ~ 46360ms (file: 275-165-152-632-527-503-521-236-726-631-267-327-502-627-136-135-316-153-135-537-573-753-765-576-517-315 136-103-136-103-163-167-163-167-160-375-571-573-753-576-753-376-316-763-7_W.mp3)
  Segment 12: 47360ms ~ 47936ms (file: 657-572-607-561-532-312-506-316-205-235-173-135-651-317-136-605-653-613-567-576-316-103-361-361-570-450-763-507-567-365-506-635-672-527-675-123-127-507-215-507-357-251-576-627-2_W.mp3)
  Segment 13: 48936ms ~ 49512ms (file: 567-576-576-576 574-572-517 572-571-574-570-517-570_W.mp3)
  Segment 14: 50512ms ~ 51088ms (file: 672 372-716-370-765-365-361-173-137-506-367-361-367-360-316-376-376-317-376-361-317-301-316-301-301-301-312-765-360-307-367-312-357-361-362-613-623-613-126-136-163-136-610-136-3_W.mp3)
  Segment 15: 52088ms ~ 52664ms (file: 621-352-316-312-573-357-537-257-723-632-576-726-760-130-321-312-370-256-207-162-352-576-537-316-356-756-360-675-657-301-325-754_V.mp3)
  Segment 16: 53664ms ~ 54240ms (file: 137-561-576-567-617-671-617-671-675-672-671-167-753-135-206-173-765-736-170-756-571-763-572-213-576-126-163-123-132-531-752-751-132-136-137-527-173-765-175-127-627-276-751-216-5_V.mp3)
  Segment 17: 55240ms ~ 63304ms (file: 257-302-735-276-206-765-230-760-265-267-231-312-312-712-127-173-725-756-263-251-275-160-617-160-765-675-321-123-231-572-756-576-125-621-105-312-317-103-132-127-612-162-216-263-1_W.mp3)
  Segment 18: 64304ms ~ 71576ms (file: 765-713-715-574-715-162-627-267-256-326-312-312-136-312-136-130-361-763-567-572-657-756-625-610-321-631-263-326-276-312-310-376-576-103-102-132-126-103-132-213-326-132-132-276-3_W.mp3)
  Segment 19: 72576ms ~ 73152ms (file: 547-574-571-574-513-517-513-571-576-517-572-570-573-537-175-756-157-157_W.mp3)
  Segment 20: 74152ms ~ 82504ms (file: 126-321-576-126-321-236-326-235-547-572-162-165-547-574-160-316-173-327-236_W.mp3)
  Segment 21: 83504ms ~ 84080ms (file: 103-132-761-372-637-625-132-132-130 570-527-251-756-706-175 124 136-173-170-176-762-163 361-132-123-627-721-723-271-732-715-572-260-260-216-261-213-672-326-312-321-731-613-236-6_V.mp3)
  Segment 22: 85080ms ~ 85656ms (file: 574-574-574-571 175-157-157-517-573 571-517-517-157-571-517-570-570-517-571-571-517-574-517-574-574-517-571-571-153-571 570-570-570-517-571-517-157-157-157-157-157-157-157-517-5_W.mp3)
  Segment 23: 86656ms ~ 87232ms (file: 527-572-756-576-135-735-170-715-701-175-705-705-710-157-157 170-107-107-170-170-173-170-701-710-710-715-701-705 571-750-705-751-701-750-750-710-107-570-751-701-170-705-751-701-7_V.mp3)
  Segment 24: 88232ms ~ 88808ms (file: 321-261-132-567-671-167-567-576-763 130-675-650-675-765-657 135-106_W.mp3)
  Segment 25: 89808ms ~ 90384ms (file: 673-736 157-150-571-571-175-107-170-105-160-175-571-175-156-751-157-715-176-157-175-167-163-317-173-317-317_V.mp3)
  Segment 26: 91384ms ~ 91960ms (file: 756-567-570-216-176-261-671-276-132-305-367-136-267-265-367-306-316-326-362-312-257-625-627-572-267-253-321-273-267-273-237-672-267-123-753-361-563-136-173-571-765-231-762-756-7_V.mp3)
  Segment 27: 92960ms ~ 99692ms (file: 210-210-102-160-162-320-302-320-132-201-216-126-203-103-102-126-103-312-312-132-320-312-320-231-230-132-132-123-132-310-230-102-126-301-203-213-230-230-231-103-130-102-132-302-3_W.mp3)
  Segment 28: 100692ms ~ 108540ms (file: 102-261-216-230-216-326-102-201-627-672-120-210-206-276-207-217-321-361-316-136-130-301-107-276-321-312-261-130-321-306-106-132-206-216-261-302-123-320-203-143-620-752-260-260-1_W.mp3)
  Segment 29: 109540ms ~ 110116ms (file: 275-312-725-271-257-265-257-215-725-570-152-276-570-321-715-251-570_V.mp3)
  Segment 30: 111116ms ~ 111692ms (file: 574-574-572-570-571-576-576-571-576-573-715-751-517-175-571-571-513-513-573-751-570-571-571-573 574-574-574-574-574-574-574-574-571-751-571-572-517-517-574-572-574_W.mp3)
  Segment 31: 112692ms ~ 123600ms (file: 230-230-231-321-130-103-132-103-123-231-261-127-123-321-230-230-213-310-103-301-320-230-231-321-213-230-312-741-725-172-237-320-236-130-312-236-326-312-267-267-120-123-312-325-1_W.mp3)
  Segment 32: 124600ms ~ 125176ms (file: 675-675-675-673-675-671-675-673-671-673-673_W.mp3)
  Segment 33: 126176ms ~ 126752ms (file: 371-371-317-317-371-137-371-503-571-573-576-576-756-573-571-570 765_W.mp3)
  Segment 34: 127752ms ~ 128328ms (file: 750-756-132-213-361-125-725-235-217-567-752-153 172-725-276-756-216-321-572-265-562-256-725-572-765-625-267-562-352-263-256-253-721-276-573-327-527-312-257-675-125-725-236-723-5_V.mp3)
  Segment 35: 129328ms ~ 141208ms (file: 103-107-105-105-157-574-574-574-751-275-263-326-267-362-261-273-263-217-137-237-107-103-130-276-276-276-276-276-275-132-263-217-267-317-132-270-267-213-173-107-132-752-217-213-2_W.mp3)
  Segment 36: 142208ms ~ 150668ms (file: 672-365-361-356-326-316-327-361-261-231-103 367-317-723-273-153-172-362-103-537-375-315 752-236-256-312-213-107-173-167-673-312-267-765-716-276-672-372-367-627-637-216-176-712-7_W.mp3)
  Segment 37: 151668ms ~ 152244ms (file: 176-701 756-107-701-613-173-170-157-710-716-176-170-710-170-176-715-710-706-176-173-715-176-175-710 107-170-107-710-175-173-175-175-170-710-170-713-716-170-175-715-761-170-170-1_V.mp3)
  Segment 38: 153244ms ~ 161379ms (file: 310-316-276-267-123-310-176-103-107-106-107-103-316-351-106-167-321-327-107-130-137-103-103-726-756-725-527-537-165-310-302-320-320-312-531-726-170-167-213-126-102-672-312-276-7_W.mp3)
  Segment 39: 162379ms ~ 162955ms (file: 576-763-765-765-675-765-675-673-672-673-673-675-673-763-765_W.mp3)
  Segment 40: 163955ms ~ 164531ms (file: 273-216-326-261-267-276-657-316-576-316-312-237-765-136-537-321-576-612-123-126-213-325-573-257-376-612-627-137-132-571-135-103-125-526-671-136-267-236-576-257-576-675-231-672-2_V.mp3)
  Segment 41: 165531ms ~ 166107ms (file: 657-675-675-765-756-574-657-576-576-567-576-574-547-576 567 672-672-627-627-671-361-621-216-367-536-672-163-365-267-620-621-216-210-312-326-376-376-137-316-316-136-316-361-316 6_W.mp3)
  Segment 42: 167107ms ~ 176539ms (file: 765-672-267-263-312-672-236-103-236-765-237-326-753-136-230-213-236-263-267-321-627-261-267-762-123-213-763-173-326-765-267-756-267-267-213-321-675-657-765-263-123-143-163-231-3_W.mp3)
  Segment 43: 177539ms ~ 183155ms (file: 752-752-176-152-126-205-215-723-235-235-213-235-325-231-273-726-321-765-725-102-123-123-321-167-152-175-176-127-621-267-721-721-302-765-320-236-123-321-632-623-263-217-275-725-2_W.mp3)
  Segment 44: 184155ms ~ 184731ms (file: 573-571-312-357-726-763-163-765-671-673-367-637-671-613-635-617-671-167-625-132-765-270-627-360-306-326-367-367-326-376-627-672-627-657-765-672-367 321-705-362-607-263-326 547 5_W.mp3)
  Segment 45: 185731ms ~ 186307ms (file: 126-512-162-305-326-527-361-276-576-276-726-312-705-275-237-576-257-627-762-560-273-257-157-617-562-165_V.mp3)
  Segment 46: 187307ms ~ 187883ms (file: 265-570-326-527-136-312-307-137-176-613-362-107-376-671-657-137-675-652-617-631-367-136 716-716-716-176_W.mp3)
  Segment 47: 188883ms ~ 189459ms (file: 623-236-231-675-510-312-231 672-516-630-625-657-675-123_W.mp3)
  Segment 48: 190459ms ~ 191035ms (file: 576-375-537-320-310-137-310-136-136-316-132_W.mp3)
  Segment 49: 192035ms ~ 195275ms (file: 123-103-512-512-235-206-602-206-213-123-312-623-276-260-132_V.mp3)
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
> **Time**: 2026-04-06 07:46:27
> **AI**: gemini

---

## Response

{
"segment": 1,
"transcript": "안녕하세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 105,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 105, "dur_ms": 420, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "만나서 반갑습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "만나서", "start_ms": 80, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 300, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "오늘 날씨가 정말 화창하고 좋네요 산책하기 딱 좋은 날씨예요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 150, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 410, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 720, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "화창하고", "start_ms": 1010, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 1470, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "산책하기", "start_ms": 2100, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "딱", "start_ms": 2590, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 2750, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "날씨예요", "start_ms": 2980, "dur_ms": 410, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "Yes, it is a beautiful day for a walk in the park.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "Yes,", "start_ms": 120, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "it", "start_ms": 310, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 420, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 550, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "beautiful", "start_ms": 640, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "day", "start_ms": 1000, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "for", "start_ms": 1210, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 1370, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "walk", "start_ms": 1460, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 1710, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 1840, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "park.", "start_ms": 1950, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "어디로 갈까요?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "어디로", "start_ms": 90, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "갈까요?", "start_ms": 340, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 7,
"transcript": "근처에 있는 남산 타워나 한강 공원 어때요? 둘 다 경치가 아주 좋거든요.",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "근처에", "start_ms": 180, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 510, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "남산", "start_ms": 700, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "타워나", "start_ms": 960, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "한강", "start_ms": 1280, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "공원", "start_ms": 1530, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "어때요?", "start_ms": 1760, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "둘", "start_ms": 2300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 2460, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "경치가", "start_ms": 2590, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 2940, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "좋거든요.", "start_ms": 3160, "dur_ms": 420, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "That sounds like a great idea. I would prefer to go to the river side.",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "That", "start_ms": 200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "sounds", "start_ms": 360, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "like", "start_ms": 590, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 740, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "great", "start_ms": 830, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "idea.", "start_ms": 1040, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 1500, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "would", "start_ms": 1610, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "prefer", "start_ms": 1800, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 2110, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "go", "start_ms": 2240, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 2410, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 2530, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "river", "start_ms": 2640, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "side.", "start_ms": 2870, "dur_ms": 260, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "좋아요 출발합시다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "좋아요", "start_ms": 110, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "출발합시다", "start_ms": 340, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 11,
"transcript": "가기 전에 마실 것 좀 살까요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "가기", "start_ms": 130, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "전에", "start_ms": 320, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "마실", "start_ms": 480, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 650, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 760, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "살까요?", "start_ms": 880, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "Sure, I'll get some coffee. What about you?",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 115,
"audio_type": "speech",
"words": [
{"word": "Sure,", "start_ms": 115, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "I'll", "start_ms": 325, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "get", "start_ms": 455, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "some", "start_ms": 605, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "coffee.", "start_ms": 775, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "What", "start_ms": 1200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "about", "start_ms": 1360, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "you?", "start_ms": 1550, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "저는 아이스티로 할게요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 95,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 95, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "아이스티로", "start_ms": 265, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "할게요", "start_ms": 585, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "Great. Let's find a cafe nearby.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "Great.", "start_ms": 110, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "Let's", "start_ms": 330, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "find", "start_ms": 520, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 690, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "cafe", "start_ms": 780, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "nearby.", "start_ms": 1010, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "저기 모퉁이에 예쁜 카페가 있네요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "저기", "start_ms": 140, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "모퉁이에", "start_ms": 300, "dur_ms": 260, "phase_lr_deg": 0.0},
{"word": "예쁜", "start_ms": 570, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "카페가", "start_ms": 760, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "있네요", "start_ms": 980, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "Let's go inside.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 125,
"audio_type": "speech",
"words": [
{"word": "Let's", "start_ms": 125, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "go", "start_ms": 295, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "inside.", "start_ms": 425, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "카페 분위기가 너무 좋네요. 아늑하고 조용해서 공부하기에도 좋을 것 같아요.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 220,
"audio_type": "speech",
"words": [
{"word": "카페", "start_ms": 220, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "분위기가", "start_ms": 440, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 770, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "좋네요.", "start_ms": 960, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "아늑하고", "start_ms": 1400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "조용해서", "start_ms": 1760, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "공부하기에도", "start_ms": 2080, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "좋을", "start_ms": 2510, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 2700, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "같아요.", "start_ms": 2810, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "I agree. But we are here to relax today, not to study.",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "agree.", "start_ms": 360, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "But", "start_ms": 750, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "we", "start_ms": 910, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "are", "start_ms": 1040, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "here", "start_ms": 1160, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 1330, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "relax", "start_ms": 1440, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "today,", "start_ms": 1730, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "not", "start_ms": 2100, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 2250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "study.", "start_ms": 2360, "dur_ms": 310, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "맞아요 휴식이 필요하죠",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "맞아요", "start_ms": 100, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "휴식이", "start_ms": 290, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "필요하죠", "start_ms": 500, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "요즘 업무가 너무 많아서 많이 피곤했거든요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "요즘", "start_ms": 150, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "업무가", "start_ms": 340, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 560, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "많아서", "start_ms": 720, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 950, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "피곤했거든요.", "start_ms": 1120, "dur_ms": 420, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "You deserve a break. Take it easy today.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "You", "start_ms": 140, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "deserve", "start_ms": 250, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 500, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "break.", "start_ms": 590, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "Take", "start_ms": 950, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "it", "start_ms": 1120, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "easy", "start_ms": 1210, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "today.", "start_ms": 1400, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 23,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 24,
"transcript": "음료 나왔습니다",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "음료", "start_ms": 100, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "나왔습니다", "start_ms": 290, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "Thank you very much.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "Thank", "start_ms": 90, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "very", "start_ms": 360, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "much.", "start_ms": 530, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "고맙습니다. 이제 강변으로 나가볼까요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "고맙습니다.", "start_ms": 120, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 550, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "강변으로", "start_ms": 710, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "나가볼까요?", "start_ms": 960, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "바람도 시원하고 정말 좋네요. 사람이 꽤 많지만 여유로운 느낌이에요.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 160,
"audio_type": "speech",
"words": [
{"word": "바람도", "start_ms": 160, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "시원하고", "start_ms": 390, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 680, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "좋네요.", "start_ms": 900, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "사람이", "start_ms": 1300, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "꽤", "start_ms": 1530, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "많지만", "start_ms": 1640, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "여유로운", "start_ms": 1900, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "느낌이에요.", "start_ms": 2190, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "People in this city really love the river. It's like an oasis in the concrete jungle.",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 210,
"audio_type": "speech",
"words": [
{"word": "People", "start_ms": 210, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 400, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "this", "start_ms": 510, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "city", "start_ms": 640, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "really", "start_ms": 800, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "love", "start_ms": 990, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 1160, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "river.", "start_ms": 1270, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "It's", "start_ms": 1700, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "like", "start_ms": 1830, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "an", "start_ms": 1980, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "oasis", "start_ms": 2090, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 2380, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 2500, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "concrete", "start_ms": 2610, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "jungle.", "start_ms": 2920, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "콘크리트 정글이라는 말이 딱 맞네요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "콘크리트", "start_ms": 110, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "정글이라는", "start_ms": 370, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "말이", "start_ms": 700, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "딱", "start_ms": 860, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "맞네요.", "start_ms": 970, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 31,
"transcript": "저기 보세요 노을이 지기 시작했어요. 하늘 색깔이 정말 예술이네요.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 170,
"audio_type": "speech",
"words": [
{"word": "저기", "start_ms": 170, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "보세요", "start_ms": 340, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "노을이", "start_ms": 650, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "지기", "start_ms": 870, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "시작했어요.", "start_ms": 1030, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "하늘", "start_ms": 1500, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "색깔이", "start_ms": 1690, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 1910, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "예술이네요.", "start_ms": 2100, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "It's breathtaking. I'm so glad we came here.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "It's", "start_ms": 130, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "breathtaking.", "start_ms": 260, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "I'm", "start_ms": 800, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "so", "start_ms": 910, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "glad", "start_ms": 1060, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "we", "start_ms": 1230, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "came", "start_ms": 1340, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "here.", "start_ms": 1530, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "덕분에 저도 힐링이 되는 것 같아요. 같이 와줘서 고마워요.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "덕분에", "start_ms": 180, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "저도", "start_ms": 430, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "힐링이", "start_ms": 620, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "되는", "start_ms": 870, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 1030, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "같아요.", "start_ms": 1140, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 1550, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "와줘서", "start_ms": 1720, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "고마워요.", "start_ms": 1970, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "My pleasure. We should do this more often.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "My", "start_ms": 140, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "pleasure.", "start_ms": 270, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "We", "start_ms": 750, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "should", "start_ms": 860, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "do", "start_ms": 1030, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "this", "start_ms": 1160, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "more", "start_ms": 1310, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "often.", "start_ms": 1480, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 38,
"transcript": "다음에는 다른 공원도 가봐요. 서울에는 예쁜 곳이 정말 많거든요.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "다음에는", "start_ms": 200, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "다른", "start_ms": 490, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "공원도", "start_ms": 680, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "가봐요.", "start_ms": 930, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "서울에는", "start_ms": 1300, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "예쁜", "start_ms": 1590, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "곳이", "start_ms": 1760, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 1920, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "많거든요.", "start_ms": 2110, "dur_ms": 340, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 40,
"transcript": "That's a plan. I'm looking forward to it.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "That's", "start_ms": 110, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 280, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "plan.", "start_ms": 370, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "I'm", "start_ms": 750, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "looking", "start_ms": 860, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "forward", "start_ms": 1050, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 1280, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "it.", "start_ms": 1390, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 42,
"transcript": "어느새 어두워졌네요. 이제 저녁 먹으러 갈까요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 160,
"audio_type": "speech",
"words": [
{"word": "어느새", "start_ms": 160, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "어두워졌네요.", "start_ms": 420, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 900, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "저녁", "start_ms": 1060, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "먹으러", "start_ms": 1250, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "갈까요?", "start_ms": 1470, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "Yes, I'm getting hungry. What do you want to eat?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "Yes,", "start_ms": 120, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "I'm", "start_ms": 280, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "getting", "start_ms": 390, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "hungry.", "start_ms": 580, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "What", "start_ms": 1000, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "do", "start_ms": 1150, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 1260, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "want", "start_ms": 1370, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 1530, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "eat?", "start_ms": 1640, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "음... 치맥 어때요? 한강에서는 치맥이 최고죠.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 190,
"audio_type": "speech",
"words": [
{"word": "음...", "start_ms": 190, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "치맥", "start_ms": 550, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "어때요?", "start_ms": 770, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "한강에서는", "start_ms": 1200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "치맥이", "start_ms": 1560, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "최고죠.", "start_ms": 1790, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "That sounds perfect! Let's order some chicken.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "That", "start_ms": 100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "sounds", "start_ms": 260, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "perfect!", "start_ms": 450, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "Let's", "start_ms": 850, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "order", "start_ms": 1020, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "some", "start_ms": 1210, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "chicken.", "start_ms": 1360, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "제가 주문할게요. 금방 올 거예요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "제가", "start_ms": 90, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "주문할게요.", "start_ms": 250, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "금방", "start_ms": 650, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "올", "start_ms": 840, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "거예요.", "start_ms": 950, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "Can't wait for the food.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 105,
"audio_type": "speech",
"words": [
{"word": "Can't", "start_ms": 105, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "wait", "start_ms": 295, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "for", "start_ms": 465, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 585, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "food.", "start_ms": 695, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "오늘 하루 정말 즐거웠어요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 110, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "하루", "start_ms": 280, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 440, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "즐거웠어요.", "start_ms": 630, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "Me too. Best day ever!",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 115,
"audio_type": "speech",
"words": [
{"word": "Me", "start_ms": 115, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "too.", "start_ms": 245, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "Best", "start_ms": 500, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "day", "start_ms": 690, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "ever!", "start_ms": 860, "dur_ms": 220, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 07:46:27*
