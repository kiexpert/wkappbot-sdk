# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=39828  time=08:17:08  uptime=10255s  mem=431MB  system_processes=409"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_081457.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 3096ms (file: 567-750-315-576-762-572-657-520-263-762-527-502-531-675-632 756-524-236-572-576-257-352-321-571-521-372-237-132-276-752-574-752-236-267-572-156_V.mp3)
  Segment 2: 4096ms ~ 4672ms (file: 152-157 157-153-136-351-153-170-207-132-135-135-135-315-361-361-237-275-271-237-236-327-376-273-137-367-372-726-375-376-517-572-571-735-135-305-175-172-576-715-751-175-572-751-5_W.mp3)
  Segment 3: 5672ms ~ 12044ms (file: 216-275-267-261-267-627-132-130-132-210-572-276-267-276-715-756-276-267-231-130-752-372-572-132-270-270-170-107-216-123-126-276-127-213-126-216-123-267-267-276-126-275-271-123-1_W.mp3)
  Segment 4: 13044ms ~ 21504ms (file: 327-763-647-327-312-102-123-715-137-236-761-735-753-102-710-501-751-713-172-123-132-172-123-123-102-130-270-213-123-106-312-316-136-137-107-130-206-201-230-230-413-102-102-702-2_W.mp3)
  Segment 5: 22504ms ~ 30028ms (file: 106-261-627-261-201-306-316-103-103-321-105-130-163-261-267-316-130-162-167-270-621-267-702-107-103-210-107-103-106-706-716-173-725-137-574-137-720-231-107-153-213-326-162-613-3_W.mp3)
  Segment 6: 31028ms ~ 35564ms (file: 326-312-321-312-321-321-312-236-132-123-213-310-173-162-132-136-173-312-123-132-132-213-123-132-132-132-326-123-126-132-123-132-127-126-137-213-327-231-123-231-213-231-213-236-3_W.mp3)
  Segment 7: 36564ms ~ 37140ms (file: 175-571-751-571-517-175-517-571-531 570-574-576-574-574-574-576-574-574-576-572 574-574-571-517-574-574-574-574-574 574-571-571-571-571-571-517-571-715-571-517-571-157-571-571-5_W.mp3)
  Segment 8: 38140ms ~ 45124ms (file: 321-130-107-102-213-207-210-216-230-302-126-312-170-321-167-176-167-136-625-167-103-130-106-321-103-137-316-103-231-132-103-701-261-236-375-130-231-210-261-716-103-136-203-132-7_W.mp3)
  Segment 9: 46124ms ~ 46700ms (file: 517-576-517-576-157-153-513-157-107-103-157-157-157-157-156-157-157-157-157-517-157-517-517-516-756-716-751-135-317-105-105-103-107-135-175-175-175-157-175-157-156-157-157-175-5_V.mp3)
  Segment 10: 47700ms ~ 48276ms (file: 715-751 571-750-570-517-571-751-570-170-175-571-170-170-731-751-175-731-351-175-705-701-170-751-710-135-173-701-170-157-715-571-750-175 751-715-750-703-170-715-175-107-130-107-1_V.mp3)
  Segment 11: 49276ms ~ 49852ms (file: 752-176-612-612-672-762-362-562-315-562-761-267-570-123-263-652-756-621-570-235-167-625-627-625-256-317-716-572-756-572-713-267-326-752-572-756-706-627-135-371-637-367-371-367-3_V.mp3)
  Segment 12: 50852ms ~ 51428ms (file: 157-175-157-175-157-157-517-156-157-571-516-567-571-516-517-175-571-137-135-175-103-105-137-135-103-157-513-105-130-157-157-157-157-571-157-175-157-517-157-157-751-175 103-105-1_V.mp3)
  Segment 13: 52428ms ~ 53004ms (file: 175-316-672-625-326-523-574-574-574-750-631-361-137-136-137-136-316-136_W.mp3)
  Segment 14: 54004ms ~ 54580ms (file: 263-627-267-267-261-261-102-127-172-271-127-127-217-126-621-621-261-621_W.mp3)
  Segment 15: 55580ms ~ 67424ms (file: 312-132-136-163-276-765-672-627-162-567-265-267-267-132-231-263-102-231-321-103-213-123-231-736-103-260-260-236-326-136-267 267-314-267-310-301-312-134-701-132-267-132-132-136-2_W.mp3)
  Segment 16: 68424ms ~ 69000ms (file: 126-261-263-725-265-327-325-207-236-372-732-507-352-572-215-672-326-570-123-705-265-576-672-217-360-621-621-153-765-631-675-521-706-723-571-765-754-574-576-670-570-731-136-516-6_V.mp3)
  Segment 17: 70000ms ~ 70576ms (file: 621-316-312-517-153-316-153-321-136-315-517-136-103-165-107-265-136-132-320-531-541-312-310-261-672-267-216-361-627-102-102-627-574-576-625-627-267-154-215-547-263-105-156-103-2_W.mp3)
  Segment 18: 71576ms ~ 72152ms (file: 135-576-156-176-165-567-653-517-753-317-756-130-310-617-167-673-671-673-675-675-165-167-102-157-756-357-576-130-153-156-173-731-167-761-570-216-351-265-621-125-231-216-176-570-1_W.mp3)
  Segment 19: 73152ms ~ 73728ms (file: 257-725-257-570-567-756-756-571-516-615-106-750-507-657-567-562-756-567-576-652-657-156-156-576-567-156-652-516-156-516-517-657-567-567-152-156-576-576-156-516-156-571-657-567-1_W.mp3)
  Segment 20: 74728ms ~ 85456ms (file: 650-216-236-230-203-301-310-302-312-312-132-136-170-712-173-123-231-261-216-216-173-136-367-130-103-123-160-230-302-361-306-103-136-107-612-360-306-357-261-136-326-261-260-263-3_W.mp3)
  Segment 21: 86456ms ~ 92612ms (file: 103-132-163-612-236-756-132-671-672-621-267-572-276-130-623-367-362-632-623-576-625-601-163-312-127-106-230-201-102-135-265-261-672-617-237-253-612-136-160-612-167-206-326-261-2_W.mp3)
  Segment 22: 93612ms ~ 94188ms (file: 132 312-236-326-362-362-361-365-673-567-675-637-623_W.mp3)
  Segment 23: 95188ms ~ 97528ms (file: 261-152 760-106-132-317-261 157-526-236-150-570-571-576-753-651-175-571-517-561-567-601-567-157-576_W.mp3)
  Segment 24: 98528ms ~ 103784ms (file: 361-361-316-316-136-103-136-312-130-310-673-367-731-367-716-763-216-312-137-320-130-320-103-302-301-213-316-132-106-107-326-723-137-130-136-230-276-320-107-231-132-301-102-301-1_V.mp3)
  Segment 25: 104784ms ~ 111984ms (file: 124-120-261-103-160-751-751-716-765-514-627-761-572-230-106-715-160-174-136-756-750-312-174-176-130-107-617-762-103-572-267-327-136-236-103-163-756-237-230-756-106-671-571-275-2_W.mp3)
  Segment 26: 112984ms ~ 113560ms (file: 731-736-375-731-321-675-631-316-317-137-173-317-137-135-310-361-317 756-750-213-756-326-132-265-726-256-126-620 762-326-216-672-520-235-352-235-625-321-207-632-570-327-256-267-5_W.mp3)
  Segment 27: 114560ms ~ 115136ms (file: 325-265-672-265-726-275-725-527-265-672-765-275-762-276-275-726-372-275-275-237-213-372-726-267-675-237-527-267-752-275-672-276-276-236-265-263-216-261-263-263-267-265-275-275-2_W.mp3)
  Segment 28: 116136ms ~ 116712ms (file: 765-765-762-765-751-756-712-765-756-753-576-576-756-756-756-756-752-725-752-756-572-725-751-752-756-752-756-750-275-132-572-756-752-752-572-756-756-573-576-576-567-675-567-756-7_W.mp3)
  Segment 29: 117712ms ~ 125128ms (file: 762-326-761-672-607-106-130-765-671-231-230-213-162-123-361-631-163-361-267-162-163-107-310-672-102-621-267-376-326-756-735-706-176-736-321-321-237-326-362-321-573-576-672-726-1_W.mp3)
  Segment 30: 126128ms ~ 126704ms (file: 573-574-576-756-136-136-631-637-361-136-163-316-361-567-657-567-576-567-567-567-651-765-671-756-576-567-675-631-163-165-163-652-765-576-765-652-576-635-561-675-615-715-162-176-1_W.mp3)
  Segment 31: 127704ms ~ 134724ms (file: 317-123-312-130-361-625-120-201-231-574-574-573-573-576-137-103-150-153-301-541-513-574-157-357-136-321-237-231-213-236-136-136-321-236-216-137-627-263-263-713-157-157-175-153-3_W.mp3)
  Segment 32: 135724ms ~ 136300ms (file: 725-675-675 167-317-137-372-301-132-312-162-260-716-312-127-162-126-507-236-257-312-123-257-752-261-137-326-302-271-173-312-317-576-317-635-103-263-576-625-570-753-625-752-276-1_V.mp3)
  Segment 33: 137300ms ~ 137876ms (file: 216-102-756-753-751-756-751-751-736-317-157-602-317-371-317-310-713-162-726-762-175-752-157-156-136-135-257-123-215-123-125-135-216-315-275-126-217-102-132-627-612-261-263-316-3_V.mp3)
  Segment 34: 138876ms ~ 145572ms (file: 213-576-627-107-312-132-132-310-132-310-312-107-267-720-735-172-706_W.mp3)
  Segment 35: 146572ms ~ 147148ms (file: 715-576-751-750-756-751-571-571-576-751-175-751-756-751 576-571-576-570-570-576-750-571-571-570-750-751-571-175-571-175-157-751-751-571-751-576-756-571-751-751-750-701-715-751-7_V.mp3)
  Segment 36: 148148ms ~ 154160ms (file: 267-263-120-127-167-726-732-756-273-263-267-726-170-703-326-301-312-107-756-236-326-127-172-321-726-236-120-103-160-257-130-170-160-276-276-127-627-736-123-231-102-217-725-756-7_W.mp3)
  Segment 37: 155160ms ~ 162864ms (file: 236-627-236-267-627-765-736-310-627-263-236-102-162-120-267-267-162-106-106-316-713-312-263-103-136-136-103-263-213-130-213-260-321-321-163-103-130-526-102-123-102-130-321-127-7_V.mp3)
  Segment 38: 163864ms ~ 164440ms (file: 571-571-510-571-571-576-571-574-517-157-175-170-751-571-576-571 571-576-751-517-571-156-150-570-571-517-750-570-751-571-750-571-751-751-571-751-570-576-571 517-571-751-715-571-7_V.mp3)
  Segment 39: 165440ms ~ 166016ms (file: 316-317-136-316-361-361-316-756-620 103-132-173-173-312 126-576-157-571-573-357-657-753-754-753-756-176-731-267-312-103-163-132-136-132-162-167-132-165-123-576-136-325-320-312-5_W.mp3)
  Segment 40: 167016ms ~ 167592ms (file: 502-527-253-305-362-570-735-253-756_W.mp3)
  Segment 41: 168592ms ~ 169168ms (file: 751-751 571-571-571-571-517-175-501-157-715-517-751-715-753-571 571-574-571-574-571-517-571-751-517-157-753-175-517-570_W.mp3)
  Segment 42: 170168ms ~ 170744ms (file: 136-320-362-231 531-351 736-670-316-310_W.mp3)
  Segment 43: 171744ms ~ 176928ms (file: 276-673-130-326-763-367-753-672-726-326-263-273-260-250-527-315-236-236-103-123-570-205-203-123-175-276-275-260-627-120-103-571-570-157-316-326-752-276-570-162-132-320-230-203-2_W.mp3)
  Segment 44: 177928ms ~ 186676ms (file: 312-321-137-102-320-321-537-320-312-230-132-160-321-302-230-236-230-230-136-162-106-167-312-231-320-130-321-352-236-321-321-671-761-103-321-376-372-376-167-136-273-752-107-130-2_W.mp3)
  Segment 45: 187676ms ~ 188252ms (file: 516-632-162-657-537-537-123-573-361-615-657-351-561-756-572-615-517-537-751-513-673-627-756-135-135-136-567-651-567-567-756-567-567-657-567-567-760-176-571-675-105-137-576-576-5_W.mp3)
  Segment 46: 189252ms ~ 189828ms (file: 514-541-165-516-156-516-567-156-157-613-612_W.mp3)
  Segment 47: 190828ms ~ 191404ms (file: 574-574-574-574-574-574-517-571-571-571 573-574-574-574-574-574-574-571-517-571-573-573-574-574-571-572-571-574-571-150-573-572 574-574-574-547-574-574-574-571-574-574-574-574 5_W.mp3)
  Segment 48: 192404ms ~ 192980ms (file: 574-574-574-574-574-517-517-351-157 517-513-571-571-153-357-513-153 517-571-576-751-517-571-517-517-517-573-571-570-571-157-573 571-570-570-571-375-517-753-573-571-153-573-153-1_W.mp3)
  Segment 49: 193980ms ~ 194556ms (file: 506-563-576-621-276-236-236-325-456-327-620-126-267-321-317-657-572-507-257-357-750-765-657-605-132-572-576-275-537-327-263-132-752-752-570-273-527-527-567-752-721-267-265-627-7_V.mp3)
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
> **Time**: 2026-04-06 08:18:05
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "안녕하세요 반갑습니다 오늘 날씨가 참 좋네요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 150, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 1050, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 1900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 2250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "참", "start_ms": 2700, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 2850, "dur_ms": 246, "phase_lr_deg": 0.0}
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
"transcript": "외국어 공부를 할 때 가장 중요한 것은 꾸준함이라고 생각합니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 210,
"audio_type": "speech",
"words": [
{"word": "외국어", "start_ms": 210, "dur_ms": 500, "phase_lr_deg": -2.0},
{"word": "공부를", "start_ms": 750, "dur_ms": 450, "phase_lr_deg": -1.5},
{"word": "할", "start_ms": 1250, "dur_ms": 150, "phase_lr_deg": -1.0},
{"word": "때", "start_ms": 1450, "dur_ms": 200, "phase_lr_deg": -1.0},
{"word": "가장", "start_ms": 1700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "중요한", "start_ms": 2150, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "것은", "start_ms": 2750, "dur_ms": 350, "phase_lr_deg": 0.5},
{"word": "꾸준함이라고", "start_ms": 3200, "dur_ms": 900, "phase_lr_deg": 1.0},
{"word": "생각합니다", "start_ms": 4200, "dur_ms": 850, "phase_lr_deg": 1.5}
]
},
{
"segment": 4,
"transcript": "Everything is possible if you try your best every single day",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "Everything", "start_ms": 180, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 800, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "possible", "start_ms": 1050, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "if", "start_ms": 1650, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 1850, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "try", "start_ms": 2100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "your", "start_ms": 2450, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "best", "start_ms": 2750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "every", "start_ms": 3200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "single", "start_ms": 3600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "day", "start_ms": 4100, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "일본어로는 하じめまして라고 인사하죠 만나서 반가워요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 300,
"audio_type": "speech",
"words": [
{"word": "일본어로는", "start_ms": 300, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "はじめまして라고", "start_ms": 1100, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "인사하죠", "start_ms": 2400, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "만나서", "start_ms": 3100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "반가워요", "start_ms": 3700, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "노래방에서 부르기 좋은 노래를 추천해 드릴게요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "노래방에서", "start_ms": 120, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "부르기", "start_ms": 850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 1350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "노래를", "start_ms": 1700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "추천해", "start_ms": 2200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "드릴게요", "start_ms": 2750, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 8,
"transcript": "음악과 함께 배우는 언어는 훨씬 더 기억에 오래 남습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "음악과", "start_ms": 250, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "함께", "start_ms": 800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "배우는", "start_ms": 1250, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "언어는", "start_ms": 1800, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "훨씬", "start_ms": 2400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 2850, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "기억에", "start_ms": 3100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "오래", "start_ms": 3650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "남습니다", "start_ms": 4100, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 12,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 13,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "자 이제 본격적으로 발음 연습을 시작해 볼까요? 따라해 보세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "자", "start_ms": 150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "본격적으로", "start_ms": 750, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "발음", "start_ms": 1600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "연습을", "start_ms": 2050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "시작해", "start_ms": 2600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "볼까요?", "start_ms": 3150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "따라해", "start_ms": 3800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "보세요", "start_ms": 4350, "dur_ms": 600, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 18,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 19,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 20,
"transcript": "가나다라마바사 아자차카타파하 아주 잘 하셨습니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "가나다라마바사", "start_ms": 100, "dur_ms": 1500, "phase_lr_deg": 0.0},
{"word": "아자차카타파하", "start_ms": 1700, "dur_ms": 1500, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 3300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 3750, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "하셨습니다", "start_ms": 4000, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "반복 학습만이 실력을 향상시키는 지름길입니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "반복", "start_ms": 140, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "학습만이", "start_ms": 600, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "실력을", "start_ms": 1350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "향상시키는", "start_ms": 1900, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "지름길입니다", "start_ms": 2750, "dur_ms": 900, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 24,
"transcript": "틀려도 괜찮으니 자신 있게 큰 목소리로 읽어보세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "틀려도", "start_ms": 200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "괜찮으니", "start_ms": 750, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "자신", "start_ms": 1400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있게", "start_ms": 1800, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 2150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "목소리로", "start_ms": 2400, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "읽어보세요", "start_ms": 3100, "dur_ms": 750, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "오늘 배운 내용을 복습하는 시간을 가져봅시다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 160,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 160, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "배운", "start_ms": 550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "내용을", "start_ms": 1000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "복습하는", "start_ms": 1550, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "시간을", "start_ms": 2250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "가져봅시다", "start_ms": 2750, "dur_ms": 750, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 28,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 29,
"transcript": "수고하셨습니다 다음 시간에 또 만나요 안녕",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "수고하셨습니다", "start_ms": 200, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 1200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "시간에", "start_ms": 1600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 2100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 2350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "안녕", "start_ms": 3000, "dur_ms": 400, "phase_lr_deg": 0.0}
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
"transcript": "Please remember to practice every day for better results",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "Please", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "remember", "start_ms": 600, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 1200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "practice", "start_ms": 1400, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "every", "start_ms": 2050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "day", "start_ms": 2450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "for", "start_ms": 2800, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "better", "start_ms": 3050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "results", "start_ms": 3500, "dur_ms": 600, "phase_lr_deg": 0.0}
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
"transcript": "See you soon!",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 100, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 400, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "soon!", "start_ms": 650, "dur_ms": 400, "phase_lr_deg": 0.0}
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
"transcript": "마지막으로 한 번 더 복습해 보겠습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "마지막으로", "start_ms": 120, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 900, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "번", "start_ms": 1150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 1400, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "복습해", "start_ms": 1650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "보겠습니다", "start_ms": 2200, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "여러분 모두 정말 잘하셨어요 훌륭합니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "여러분", "start_ms": 150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "모두", "start_ms": 650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 1050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "잘하셨어요", "start_ms": 1500, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "훌륭합니다", "start_ms": 2300, "dur_ms": 800, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "Have a wonderful day everyone Goodbye",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "Have", "start_ms": 180, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 500, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "wonderful", "start_ms": 650, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "day", "start_ms": 1300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "everyone", "start_ms": 1700, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "Goodbye", "start_ms": 2400, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "즐거운 하루 되세요 다음에 또 만나요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "즐거운", "start_ms": 140, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "하루", "start_ms": 650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "되세요", "start_ms": 1050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "다음에", "start_ms": 1600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 2050, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 2300, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 46,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 47,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
},
{
"segment": 49,
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
*Generated by WKAppBot ask gemini — 2026-04-06 08:18:05*
