# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=17628  time=06:53:52  uptime=18045s  mem=461MB  system_processes=353"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_065305.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 175-751-705-751-750-715-750-750-751-751-751-750-751-750-750-751-750-751-576 517-571-705-751-507-175-710-175-173-715-751-175-751-175-715-751-715-751-705-715-751-751-756-750-751 1_V.mp3)
  Segment 2: 1576ms ~ 6760ms (file: 231-320-123-513-320-231-231-321-321-326-312-312-371-612-103-125-216-123-213-615-231-216-120-126-310-310-136-361-736-763-367-621-263-267-273-317-302-302-321-132-321-321-352-735-3_W.mp3)
  Segment 3: 7760ms ~ 8336ms (file: 567-571-150-570-501 503-570-517-105-157-157-517-157-105-105-501-501-501-507-501-510-513-501-150-510-150-510-576-576-571-507-570-517 507-570-507-571-517-501-501-510-150-501-517-5_V.mp3)
  Segment 4: 9336ms ~ 14808ms (file: 213-173-513-102-361-537-576-260-567-576-621-627-265-574-236-153-237-267-572-132-312-326-576-576-260-213-235-756-256-320-265-576-302-321-263-570-236-156-123-321-504-506-201-312-1_W.mp3)
  Segment 5: 15808ms ~ 23764ms (file: 260-267-160-102-276-612-260-260-260-260-312-102-216-102-302-712-102-321-320-236-256-206-102-203-260-230-213-765-260-312-130-201-260-260-213-261-623-621-123-201-130-326-213-254-3_W.mp3)
  Segment 6: 24764ms ~ 25340ms (file: 517-574-156-567-537-576-567-574-574-673 132_W.mp3)
  Segment 7: 26340ms ~ 26916ms (file: 605-650-657-650-657-605-612-612-617-672-675-675-672-672 605-650-650-650-650-657-650-605-650-672-672-672-672-176 650-650-605-650-650-650-657-650-657-657-650-657-672-670-670-675-6_W.mp3)
  Segment 8: 27916ms ~ 28492ms (file: 316-267-706-326-561-203-752-316-175-327-126-163-760-375-572-316-523-671-532-627-750-627-632-301-361-612-176-317-136-637-137-315-103-310-316-736-613-376-670-761-137-371-167-361_W.mp3)
  Segment 9: 29492ms ~ 38996ms (file: 103-726-765-762-276-107-765-672-137-106-230-231-320-327-372-263-276-267-237-201-126-201-270-720-127-207-267-107-103-203-103-237-321-231-672-276-172-102-762-762-275-273-302-756-2_W.mp3)
  Segment 10: 39996ms ~ 40572ms (file: 675-571-576-675-675-657-675-172-765-765-675-567-617-657-312-132-136-672-321-362-162-163-675-126-657-167-217-130-713-172-216-627-631-126-612-625-615-623-632-672-362-612-167-316-6_W.mp3)
  Segment 11: 41572ms ~ 42148ms (file: 632-126-216-231-126-132-125-216-216-261-265-256-276-267-265-256-265-265-265-265-321-321-325-235-237-725-237-574-574-576-576-576-574-547-514-153-317-371-317-317-567-567-657-751-3_W.mp3)
  Segment 12: 43148ms ~ 43724ms (file: 170-710-170-157-751-157-571-165-157-675-751-176-715-675-175-576-157-716-756-761-756-715 157-105-105-105-107-105-105-175-175-157-105-170-710-710-712-710-170-170-157-571-150 157-1_V.mp3)
  Segment 13: 44724ms ~ 52356ms (file: 617-756-765-102-132-120-176-360-321-237-572-752-132-672-672-106-132-372-271-756-103-237-237-207-273-102-236-230-302-320-762-762-320-327-752-321-760-123-627-120-107-302-276-765-5_W.mp3)
  Segment 14: 53356ms ~ 53932ms (file: 130-341-143-103-130-143-130-103-104-103-103-134-104-314-134-143-134-103-143-134-134-103 301-310-134-103-314-103-103-103-143-135-134-130-103-143-104-105-143-134 301-341-103-317-3_V.mp3)
  Segment 15: 54932ms ~ 55508ms (file: 570-571-571-571-751-571-570-571-574-571-570-573-573-570-570-153-157-153-153-517-517-105-513-517-157-517-517-175-157-137-715-137-751-175-715-571-715-576-175-715-751-751-751-751-7_V.mp3)
  Segment 16: 56508ms ~ 61584ms (file: 167-627-125-576-236-126-157-765-126-206-321-230-602-120-102-312-261-106-167-270-251-175-217-263-256-574-312-260-317-570-301-371-321-231-321-321-321-312-130-310-130-132-102-152-1_V.mp3)
  Segment 17: 62584ms ~ 63160ms (file: 627-632-623-135-103 672 103 106-106-102-610-612-106-276-672-627-136-620-627_W.mp3)
  Segment 18: 64160ms ~ 64736ms (file: 263-163-572 261-326-672-673-637-631-136-637-671-671-671-637-673-763-761-671-673-671-671-671-761-761-736-135-573-527-312-103-170-132-215-103-231-263-235-136-132-230-132-321-123-1_W.mp3)
  Segment 19: 65736ms ~ 77868ms (file: 267-107-201-263-261-203-230-236-213-321-210-260-571-627-213-261-123-267-123-312-231-103-103-206-230-201-261-120-132-310-130-130-321-126-162-260-610-136-103-230-163-216-102-320-2_W.mp3)
  Segment 20: 78868ms ~ 84664ms (file: 231-201-102-231-207-210-715-126-675-231-103-216-162-756-310-316-136-765-267-573-105-231-257-165-261-207-231-302-203-765-275-207-102-130-237-756-507-257-726-267-706-263-756-132-7_V.mp3)
  Segment 21: 85664ms ~ 98048ms (file: 102-135-143-165-167-163-162-321-145-201-102-203-153-320-132-230-135-203-230-623-230-672-201-123-213-327-120-320-123-125-103-263-627-263-203-153-143-126-157-124-260-627-127-103-2_V.mp3)
  Segment 22: 99048ms ~ 110424ms (file: 267-267-213-132-760-672-672-620-572-210-754-231-756-276-123-136-105-267-261-216-317-301-126-320-260-716-765-672-631-301-102-172-507-123-321-237-537-163-153-574-756-762-574-312-7_W.mp3)
  Segment 23: 111424ms ~ 116644ms (file: 620-270-217-130-725-705-106-217-206-206-120-321-213-261-627-132-103-207-216-201-260-620-267-201-213-263-126-756-572-321-752-572-216-261-312-213-203-137-103-312-512-321-210-201-2_V.mp3)
  Segment 24: 117644ms ~ 118220ms (file: 302-302-301-571-751-176-316-601-167-617-106-216-602-165-126-102-123-130-312-163-136-123-762-136-165-106-316-576-517-157-107-312-103-137-167-143-312-613-103-107-176-301-301-301-1_V.mp3)
  Segment 25: 119220ms ~ 119796ms (file: 576-567-652-576-562-657-560-657-562-576-650-562-657-627-657-567-526-502-560-567-652 657-657-312-312-132-132-576-567-576-765-572-576-675-567-576-561-517-631-260-623-206-652-632-2_W.mp3)
  Segment 26: 120796ms ~ 121372ms (file: 621-576-365-675-167-567-657-735-567-675-357-563-513-561-150-516-576-572-513-526-257-265-627-276-652-675-765-612-610-756-571-756-765-716-761-175-715-651-576-657-675-516-765-612-6_W.mp3)
  Segment 27: 122372ms ~ 130364ms (file: 230-231-210-312-210-216-120-102-123-213-231-263-213-206-103-213-235-235-321-123-123-321-231-206-236-216-106-106-106-102-130-230-236-263-203-326-123-132-231-216-573-574-206-230-3_W.mp3)
  Segment 28: 131364ms ~ 131940ms (file: 613-136-312-156-136-103-135-106-165-153-610-765-312_V.mp3)
  Segment 29: 132940ms ~ 133516ms (file: 513-517-517-517-517-157-150-571-573-570-517-570-507 156-517-157-135-152-517-571-513-157-157-157-517-517-571-517-571-570-157-517-517-507-507-570-507-570-576-570 510-517-517-571-5_V.mp3)
  Segment 30: 134516ms ~ 135092ms (file: 216-627-627-627-621-627-627-672-762-672-617-672-627-627-620-623-625-652-627-627-267-627-627-217-627_W.mp3)
  Segment 31: 136092ms ~ 149700ms (file: 756-574-765-715-756-574-257 130-726-705-135-574-526-765-756-574-103-132-120-571-756-752-413-756-123-130-130-306-573-752-103-512-317-130-132-310-107-201-123-130-102-574-517-136-3_W.mp3)
  Segment 32: 150700ms ~ 151276ms (file: 173-317-103-103-137 135-103-301-315-103-137-132-301-130-137-317-137-310-130-132-310-310-301-130-130-317-310-137-370-107-103-130 130-130-103-310-130-103-103-317-310-103-301-310-1_V.mp3)
  Segment 33: 152276ms ~ 152852ms (file: 540-103-261-172-176-106-123-165-610-735-165-213-103-761-130-356-136-132-631-163-103-103-106-103-130-136-326-516-162-126-163-562-157-316-136-136-163-103-153-136-103-134-130-103-3_V.mp3)
  Segment 34: 153852ms ~ 164328ms (file: 657-625-267-267-756-754-231-312-102-163-263-321-621-263-672-627-213-130-132-310-130-132-130-120-716-765-267-236-756-765-267-231-230-230-132-167-162-162-216-126-123-132-102-103-2_W.mp3)
  Segment 35: 165328ms ~ 165904ms (file: 275-625-275-571-751-725-127-751-750-756-576-135-256-267-705-617-725-715-571-756-542-207-726-765-326-237-123-450-265-132-276-237-570-236-132-572-273-762-572-312-271-236-267-162-5_V.mp3)
  Segment 36: 166904ms ~ 175580ms (file: 231-203-127-260-210-326-120-127-301-601-263-106-230-237-265-230-236-213-362-106-176-103-123-130-276-756-267-267-261-123-320-167-270-753-712-756-123-327-726-327-273-207-203-107-2_W.mp3)
  Segment 37: 176580ms ~ 177156ms (file: 105-150-510-571-514-510-105-150-150-105-501 105-150-517-105-157-715-170 105-105-105-105-105-153-105-157-105-107-105-150-175-105-157-175-107 105-150-150-510-510-157-517-105-107-1_V.mp3)
  Segment 38: 178156ms ~ 178732ms (file: 150-105-150-105-510-150-510-150-150-501-517-105-150-150-510-150-150-501-150-103-153-501-570-571 105-510-150-105-501-501-150-175-150-501-501-150-150-150-150-150-135-153-153-517-5_V.mp3)
  Segment 39: 179732ms ~ 180308ms (file: 132-132-103-123-312 567-650-670-650-560-167-165-652-312-631-671-631-631-613-167-613_V.mp3)
  Segment 40: 181308ms ~ 191496ms (file: 260-267-576-576-365-132-267-210-123-231-201-527-126-237-275-706-765-735-502-756-310-135-123-103-312-123-726-216-326-625-627-367-307-257-201-206-751-607-572-216-136-256-672-257-2_W.mp3)
  Segment 41: 192496ms ~ 199768ms (file: 761-620-231-123-312-213-301-574-107-130-103-231-102-675-627-102-103-712-310-201-612-310-217-213-231-123-302-102-715-276-103-751-103-157-254-267-157-672-652-137-132-312-762-312-5_V.mp3)
  Segment 42: 200768ms ~ 205952ms (file: 260-162-267-230-320-123-302-216-261-236-213-756-365-310-130-326-312-230-230-301-103-103-213-263-231-236-263-236-573-312-253-267-267-263-576-576-261-267-623-321-321-301-136-301-6_W.mp3)
  Segment 43: 206952ms ~ 207528ms (file: 132-572-576-675-265-756-627-265-263-263-263-216-527-572-256-136-516-652-576-576-567-627 361-621-631-321-261-612-621-163 312-632-163-167-761-167-617-617-132 756-657-657-623-263-2_W.mp3)
  Segment 44: 208528ms ~ 215260ms (file: 127-721-621-216-162-610-230-203-612-172-623-613-361-267-130-612-317-631-103-236-216-261-326-123-201-263-213-312-103-236-203-267-132-126-362-216-627-127-120-126-310-301-103-612-6_W.mp3)
  Segment 45: 216260ms ~ 223784ms (file: 601-576-321-217-756-321-253-231-130-231-321-321-571-130-260-132-102-123-213-132-321-761-267-213-213-237-231-512-236-132-312-312-752-312-321-267-527-157-160-231-731-375-102-527-2_W.mp3)
  Segment 46: 224784ms ~ 225360ms (file: 152-706-671-301 216-237-675-507-571-571-756-627-765-316-260-132-712 163-126-706-372-507-175-152-753-570 126-206-326-621-725-126-257-251-136-752-523-257-236-326-236-627-275-265-7_V.mp3)
  Segment 47: 226360ms ~ 226936ms (file: 263-106-106-106-612-761-612-621-236-261-316-260-321-302-106-326-203-230-253-250-105-132-750-576-150-156-756-167-670-705-167-167-623-260-236-675-623-267-627-263-136-126-765-576-5_W.mp3)
  Segment 48: 227936ms ~ 234848ms (file: 261-216-217-267-263-231-123-361-265-267-132-317-671-761-712-236-306-217-267-257-627-126-127-276-123-123-123-312-132-237-136-137-135-132-327-312-312-312-316-321-231-321-120-162-3_W.mp3)
  Segment 49: 235848ms ~ 236424ms (file: 306-537-230-326-360-320-260-630-602-312-326-106-162-106-162-106-612-623-163-160-610-106-160-620-126-612-263-312-312-326-362-623-126-613-621 216_V.mp3)
  Segment 50: 237424ms ~ 238000ms (file: 507-570-570-107-510-150-107-157-175-705-570-574-574-570 571-751-570-501-150-571-510-150-107-175-517-701-150-570-175-175-175-751-571-571-574-751 570-150-751-150-507-751-107-571-7_V.mp3)
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
> **Time**: 2026-04-05 06:55:20
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 2,
"transcript": "안녕하세요 오늘 날씨가 참 좋네요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 150, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 1050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 1500, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "참", "start_ms": 2200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 2600, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 4,
"transcript": "한국어 공부는 정말 재미있어요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 200, "dur_ms": 700, "phase_lr_deg": 5.0},
{"word": "공부는", "start_ms": 1000, "dur_ms": 650, "phase_lr_deg": 5.0},
{"word": "정말", "start_ms": 1800, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "재미있어요", "start_ms": 2400, "dur_ms": 900, "phase_lr_deg": 5.0}
]
},
{
"segment": 5,
"transcript": "How are you doing lately? I hope everything is fine.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "How", "start_ms": 180, "dur_ms": 250, "phase_lr_deg": -2.0},
{"word": "are", "start_ms": 450, "dur_ms": 150, "phase_lr_deg": -2.0},
{"word": "you", "start_ms": 650, "dur_ms": 200, "phase_lr_deg": -2.0},
{"word": "doing", "start_ms": 900, "dur_ms": 350, "phase_lr_deg": -2.0},
{"word": "lately?", ""start_ms": 1300, "dur_ms": 500, "phase_lr_deg": -2.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 9,
"transcript": "요즘 바빠서 연락을 자주 못 했네요 미안해요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 300,
"audio_type": "speech",
"words": [
{"word": "요즘", "start_ms": 300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "바빠서", "start_ms": 900, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "연락을", "start_ms": 1600, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "자주", "start_ms": 2300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "못", "start_ms": 2800, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "했네요", "start_ms": 3100, "dur_ms": 600, "phase_lr_deg": 0.0}
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
"transcript": "Please tell me more about your hobby. I am curious.",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "Please", "start_ms": 250, "dur_ms": 400, "phase_lr_deg": 3.0},
{"word": "tell", "start_ms": 700, "dur_ms": 300, "phase_lr_deg": 3.0},
{"word": "me", "start_ms": 1050, "dur_ms": 200, "phase_lr_deg": 3.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 16,
"transcript": "제 취미는 영화 감상과 음악 듣기입니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "제", "start_ms": 120, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "취미는", "start_ms": 400, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "영화", "start_ms": 1100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "감상과", "start_ms": 1700, "dur_ms": 700, "phase_lr_deg": 0.0}
]
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
"transcript": "That sounds like a lot of fun. I also like music.",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "That", "start_ms": 200, "dur_ms": 300, "phase_lr_deg": 4.0},
{"word": "sounds", "start_ms": 550, "dur_ms": 400, "phase_lr_deg": 4.0}
]
},
{
"segment": 20,
"transcript": "어떤 장르의 음악을 가장 좋아하세요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "어떤", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "장르의", "start_ms": 600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "음악을", "start_ms": 1200, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "저는 팝 음악과 재즈를 즐겨 듣는 편이에요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 200, "dur_ms": 400, "phase_lr_deg": 2.0},
{"word": "팝", "start_ms": 700, "dur_ms": 300, "phase_lr_deg": 2.0},
{"word": "음악과", "start_ms": 1100, "dur_ms": 600, "phase_lr_deg": 2.0}
]
},
{
"segment": 22,
"transcript": "Jazz is very relaxing after a long day of work.",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 300,
"audio_type": "speech",
"words": [
{"word": "Jazz", "start_ms": 300, "dur_ms": 450, "phase_lr_deg": -1.0}
]
},
{
"segment": 23,
"transcript": "맞아요 특히 비 오는 날에 들으면 정말 좋아요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "맞아요", "start_ms": 100, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 25,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "혹시 일본 노래도 자주 들으시나요?",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 220,
"audio_type": "speech",
"words": [
{"word": "혹시", "start_ms": 220, "dur_ms": 400, "phase_lr_deg": 5.0}
]
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
"transcript": "가끔 시티팝 장르의 노래를 찾아 듣곤 해요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "가끔", "start_ms": 140, "dur_ms": 400, "phase_lr_deg": 0.0}
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
"transcript": "City pop has a very unique vibe from the 80s.",
"speaker": "speaker_2",
"confidence": 0.93,
"language": "en",
"silence_before_ms": 280,
"audio_type": "speech",
"words": [
{"word": "City", "start_ms": 280, "dur_ms": 300, "phase_lr_deg": 2.0}
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
"transcript": "그 시대 특유의 세련된 분위기가 매력적이죠",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 160,
"audio_type": "speech",
"words": [
{"word": "그", "start_ms": 160, "dur_ms": 150, "phase_lr_deg": 0.0}
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
"transcript": "다음에 기회가 되면 추천 리스트를 보내드릴게요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 210,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 210, "dur_ms": 500, "phase_lr_deg": 4.0}
]
},
{
"segment": 41,
"transcript": "정말요? 기대하고 있을게요 감사합니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "정말요?", "start_ms": 120, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "You're welcome. Have a wonderful evening ahead.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 240,
"audio_type": "speech",
"words": [
{"word": "You're", "start_ms": 240, "dur_ms": 300, "phase_lr_deg": 2.0}
]
},
{
"segment": 43,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 44,
"transcript": "네 좋은 저녁 되세요 다음에 또 이야기해요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 150, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "Bye for now, looking forward to our next talk.",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "Bye", "start_ms": 200, "dur_ms": 300, "phase_lr_deg": 3.0}
]
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
"transcript": "Let's keep practicing Korean together every day.",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 300,
"audio_type": "speech",
"words": [
{"word": "Let's", "start_ms": 300, "dur_ms": 350, "phase_lr_deg": 0.0}
]
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
},
{
"segment": 50,
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
*Generated by WKAppBot ask gemini — 2026-04-05 06:55:20*
