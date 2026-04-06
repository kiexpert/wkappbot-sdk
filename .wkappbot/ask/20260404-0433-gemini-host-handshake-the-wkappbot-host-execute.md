# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=29532  time=04:32:12  uptime=10827s  mem=1340MB  system_processes=364"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260404_043203.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 261-263-621-176-267-671-132-623-317-217-153-216-213-321-675-765-326-672-267-576-576-375-732 132-541-312-726-725-725-725-725-271-721-637-716-316 106-160-176-561-137-231-230-236-2_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 167-160-167-167-316-103-610-106-106-162-126-136-132-167-130-163-160-126-176-165-657-162-672-165-256-165-126-617-675-163-106-576-576-671-261-671-632-603-156-160-106-123-102-216-1_V.mp3)
  Segment 3: 3152ms ~ 8120ms (file: 321-321-321-321-237-321-231-231-231-201-213-231-213-752-752-236-236-263-230-327-302-321-302-320-231-231-321-321-321-327-321-326-236-231-321-237-231-231-237-321-237-213-231-321-1_W.mp3)
  Segment 4: 9120ms ~ 13548ms (file: 713-735-127-123-572-253-132-230-120-132-321-350-305-327-267-310-130-726-725-175-172-170-137-751-170-173-375-176-731-713-137-271-172-312-321-726-237-321-123-312-317-173-276-267-2_W.mp3)
  Segment 5: 14548ms ~ 15124ms (file: 617-163-163-260-620-620-206-206-621-163-103-201-230-206-261-672-206-321-102-236-506-526-612-756-175-162-126-162-167-163-106-260-213-362-231-360-106-167-612-621-612-201-312-205-1_V.mp3)
  Segment 6: 16124ms ~ 16700ms (file: 103-531 316-273-130-157-157-712-175-715-175-175-517-571-571-576-576-576-576-576 132-130-132-216-231-213-267-263-657-625-132-132-610-136-657-163-507-612-326-361-136-103-103-635-1_V.mp3)
  Segment 7: 17700ms ~ 18276ms (file: 130-103-310-175-301-310-317-103-310-107-103-317-307-302-357 315-137-317-132-137-317-175-130-130-301-310-132-103-135-173-157-137-310-315-310-107-301-137-103-130-705 137-132-103-3_V.mp3)
  Segment 8: 19276ms ~ 19852ms (file: 576-756-576-576-576-567-567-567-657-567-567-576-576-576-675-657-675-675-675-657-576-576-576-576-576-576-567-657-576-567-756-576-567-576-576-576-567-576-576-576-567 651-567-576-5_W.mp3)
  Segment 9: 20852ms ~ 21428ms (file: 650-605-650-605-650-620-625 652-652-657-657-657-657-675-657-675-657-652-625-652-652-562-652 625-625-625-652-652-652-657-652-652-625 652-657-625-657-567-657-652-625-652-625 652-6_W.mp3)
  Segment 10: 22428ms ~ 23004ms (file: 163-167-631-106-103-136-167-236-163-312-617_V.mp3)
  Segment 11: 24004ms ~ 32968ms (file: 106-672-621-752-765-372-130-765-367-362-372-753-172-103-765-765-763-751-105-261-627-267-263-276-267-716-170-130-106-761-132-102-103-321-321-756-673-627-621-127-167-263-765-263-3_W.mp3)
  Segment 12: 33968ms ~ 40412ms (file: 135-165-253-627-631 137-735 126-132-130-120-123-125 513 312-132 132-132-217-162-132-127-126-132-123-162 127 126 176 315-125_V.mp3)
  Segment 13: 41412ms ~ 41988ms (file: 136-136-361-136-167-716-716-176-576-576-315-623-627-627-672-627-672-657-567-561-576-567-576-576-357-576-567-317-136-167-163-167-362-267-627-623-621-623-367-632-361-160-136-130-1_W.mp3)
  Segment 14: 42988ms ~ 43564ms (file: 103-163-106-106-620-623-206-607-620-620-623-301 176-126-321-316-263-136-501-315-326-316-126-126-103-102-103-103-517-176-103-136-756-230-230-123-107-160-103-106-103-163-613-167-1_V.mp3)
  Segment 15: 44564ms ~ 53492ms (file: 137-145-574-547-574-540-754-276-276-267-267-276-765-267-267-130-310-542-103-312-201-213-130-726-712-617-102-310-751-120-176-107-270-276-756-621-573-103-107-710-715-316-637-631-6_W.mp3)
  Segment 16: 54492ms ~ 55068ms (file: 316-310-765-673-361-231-361-132-312-631-236-365-325-312-316-753-573-572-576-123-132-146-134-132-163_W.mp3)
  Segment 17: 56068ms ~ 56644ms (file: 625-562-701-172-751-251-306-326-203-302-302-302-301-310-310-301-301-132-130-310-123-203-213-216-312-301-172 130-321-312-123-126-326-321-540-253-263-574-310-301-103-103-627-627-6_V.mp3)
  Segment 18: 57644ms ~ 58220ms (file: 126-120-361-132-137-751-137-165-136-103-123-163-162_V.mp3)
  Segment 19: 59220ms ~ 59796ms (file: 136-136-312-132-216-267-163-172-136-106-102-106-607-623-670-610-601-761-261-326-265-362-167-716-160-561-361-172-671-617-163-106-103-136-162-163-136-175-561-175-127-103-623-103-6_V.mp3)
  Segment 20: 60796ms ~ 61372ms (file: 574-576-574-572-754-754-754-754-754-750-754-754-754-754-561-517-567-543-514-351-543-541-547-547-547-574-574-574-574-574-574-574-756-576-754-574-756-750-754-754-751-754-574-572-5_W.mp3)
  Segment 21: 62372ms ~ 80480ms (file: 103 103-106-165-103-135-137-527-562-763-176 102-671-163-136-132-170-312-617-751-136-716-165-671-103-137-176-153-103-761-167-630-137-126-760-761-167-716 132-103-106-126-127-160-1_V.mp3)
  Segment 22: 81480ms ~ 92064ms (file: 103-360-207-276-270-273-762-206-267-267-106-362-137-167-167-160-167-176-231-326-126-216-176-163-316-736-672-721-716-102-163-126-123-123-753-731-713-731-176-361-627-672-763-257-6_W.mp3)
  Segment 23: 93064ms ~ 93640ms (file: 312-310-123-213-126-123-126-123-203-263-263-267-540-540 576-576 306-302-267-263-326-162-321-312-321-312-312-320-263-236-260-263-627-267-260-261-206-230-123-326-321-302-261-301-3_V.mp3)
  Segment 24: 94640ms ~ 95216ms (file: 276-261-213-106-172-126-761-130-271-267-607-621-672-672-260-206-203-210-102-126-672-672-620-547-206-206-216-602-201-163-310-163-361-601-613-631-126-367-206-320-620-260-650-256-2_W.mp3)
  Segment 25: 96216ms ~ 96792ms (file: 537-531-517-517-357-251-523-527-523-567-267-657-652-561-657-635-315-356-365-321-126-126-162-123-617-136-256-257-125-631-567-576-527-571-315-356-276-276-267-675-265-567-615-675-6_W.mp3)
  Segment 26: 97792ms ~ 98368ms (file: 602-620-610-621-261-123-261-126-162_W.mp3)
  Segment 27: 99368ms ~ 99944ms (file: 657-567-675-657-567-753-762-275-275-275-276-752-675-637-136_W.mp3)
  Segment 28: 100944ms ~ 101520ms (file: 123-756-760-620-126-136-132-516-672-276-675-675-675-675-765-675-763-163_W.mp3)
  Segment 29: 102520ms ~ 103096ms (file: 574-574-574-574-574-570-574-576-576-576-576-576-754-754-751-754-574-574 574-574-574-574-547-547-574-574-570-574-574-572-576-754-754-754-754-754-751-754-754-514 574-574-574-547-5_W.mp3)
  Segment 30: 104096ms ~ 104672ms (file: 623 167-162-623-602-612-362-576 126-617-207-675-123-132-137-153-135-125-215-132-132-132-132-126-132-136-163-136-137-123-132-176-613-621-163 152-175-261-312-731-132-671 675_V.mp3)
  Segment 31: 105672ms ~ 106248ms (file: 216-231-615-167-136-163-162-613-167-561-316-361-162-162-612-167-261-162-316-671-167-317-351-652-672-267-261-163-312-376-137-317-316-162-617-136-136-316-617-613-163-563-326-312-3_W.mp3)
  Segment 32: 107248ms ~ 107824ms (file: 103-103-130 136-123-163 132-103-103-130_V.mp3)
  Segment 33: 108824ms ~ 109400ms (file: 256-526-276-765-257-265-762-627-263-263-213-623-623-263-376-312-761-167-136-106_W.mp3)
  Segment 34: 110400ms ~ 110976ms (file: 132-750-751-752-216-132-162 627-260-206-672-547-573-702-576-576-756-752-276-726-756-576-756-756-765-756-576-756-726-762-765-765-263-237-752-756-576-576-527-275-670-670-672-627-2_W.mp3)
  Segment 35: 111976ms ~ 112552ms (file: 123-103-163-132-136-136-276-512-612-102-106-657-102-106-176-216-671-102-612-130-162-103-731-136-261-167-160-160-163-623-126-620-236-213-261-320-123-167-213 162-326-132-631-213-2_V.mp3)
  Segment 36: 113552ms ~ 114128ms (file: 123-263-326-617-103-176-106-160-136-106-617-562-302-213-123-321-165-126-160-621-132-136-106-176-157-165-231-321-310-130-301-103-107-617-613-160-637-162-165-102-175-176-617-160-2_V.mp3)
  Segment 37: 115128ms ~ 115704ms (file: 762-526-752-107-603-126-631-217-132-136-315-231-267-321-320-327-231-376-372-321-106-163-135-136-136-321-132-317-576-176-576-153-365-237-302-137-132-123-206-715-715-571-132-137-6_W.mp3)
  Segment 38: 116704ms ~ 117280ms (file: 312-137-173-137-137-130-173-137-375-372 135-135-315-105-150-157-153-137-315-103-317-301-107-103-317-371-317-137-173-375-137-135-153-715-703-735 135-157-135-351-130-137-130-132-3_V.mp3)
  Segment 39: 118280ms ~ 118856ms (file: 576-576-570-571-725-672-627 625-627_W.mp3)
  Segment 40: 119856ms ~ 125508ms (file: 301-310-103-750-276-765-627-267-102-167-120-312-326-376-376-136-713-137-576-760-107-576-576-751-753-756-735-765-765-173-167-765-715-276-267-657-176-367-375-376-367-365-357-375-2_W.mp3)
  Segment 41: 126508ms ~ 127084ms (file: 302-103-136-126-163-136-103-162-673-126-261-637-632-130-310-136-301-312-162-617-607-672-625 617-615-165-615-651-165-167-132-762-620-160-167-613-371-610-135-613-621-672-675-673-1_V.mp3)
  Segment 42: 128084ms ~ 128660ms (file: 672-163-675-627-652-263-263-632-627-627-236-263-326-267-263-263-627-261-316-263-267-236 761-671_W.mp3)
  Segment 43: 129660ms ~ 130236ms (file: 312-132-321-312-263-521-527-513-325-321-312-213-162-132-231-132-216-321-132-132-231-321-321-126-312-152-215-253-132-216-123-126-216-321-132-312-132-123-321-326-132-216-123-126-3_W.mp3)
  Segment 44: 131236ms ~ 136996ms (file: 107-621-260-312-213-276-310-312-756-260-316-213-123-107-312-370-726-213-725-170-107-137-572-235-562-123-231-213-231-507-732-231-136-130-312-253-302-312-756-137-532-132-317-316-3_V.mp3)
  Segment 45: 137996ms ~ 138572ms (file: 163-160-537-136-657-176-637-312-167-130-132-617-163-162-176-126 673-316-103-124-136-672-106-167-652-136-312-106-675-610-167-162-610-651-167-176-162-132-103-136-671-132-261-613-1_V.mp3)
  Segment 46: 139572ms ~ 140148ms (file: 126-136-103-103-173-103-362-130-103-167-163-106-162-567-362-567-176-671-623-216_V.mp3)
  Segment 47: 141148ms ~ 141724ms (file: 567-576-576-576-576-576 567-576-576-567-570-576-567-576-576-576-567-576-576-576 657-576-567-576-576-576-576-576-576-567-567-576-576-576-567-576-756-576-567-576-567 657-567-576-5_V.mp3)
  Segment 48: 142724ms ~ 145568ms (file: 312-236-306-761-367-165-136-165-615-361-165-136-123-627-610-162-635-175-765-675-160-165-132-623-105-231-162-163-102-612-603-310-213-132-120-637_W.mp3)
  Segment 49: 146568ms ~ 147144ms (file: 670-637-637-765-726-762-210 627-625-672-567-567-576-657-632-625-612-267-627-627-627-157-263-326-263-216 623-163-623-102-126-126-261-261-123 267-265-273-627-206-250-762-267-362-2_W.mp3)
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
> **Time**: 2026-04-04 04:33:35
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "안녕하세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 110, "dur_ms": 410, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "반갑습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 95,
"audio_type": "speech",
"words": [
{"word": "반갑습니다", "start_ms": 95, "dur_ms": 430, "phase_lr_deg": 5.0}
]
},
{
"segment": 3,
"transcript": "오늘 날씨가 정말 좋네요 그쵸",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 140, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 610, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 1250, "dur_ms": 490, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 1820, "dur_ms": 510, "phase_lr_deg": 0.0},
{"word": "그쵸", "start_ms": 2400, "dur_ms": 380, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "네 맞아요 나들이 가기 딱 좋은 날이에요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 120, "dur_ms": 310, "phase_lr_deg": -2.0},
{"word": "맞아요", "start_ms": 510, "dur_ms": 480, "phase_lr_deg": -2.0},
{"word": "나들이", "start_ms": 1080, "dur_ms": 620, "phase_lr_deg": -2.0},
{"word": "가기", "start_ms": 1780, "dur_ms": 410, "phase_lr_deg": -2.0},
{"word": "딱", "start_ms": 2250, "dur_ms": 280, "phase_lr_deg": -2.0},
{"word": "좋은", "start_ms": 2600, "dur_ms": 390, "phase_lr_deg": -2.0},
{"word": "날이에요", "start_ms": 3050, "dur_ms": 640, "phase_lr_deg": -2.0}
]
},
{
"segment": 5,
"transcript": "점심 뭐 먹을까요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "점심", "start_ms": 80, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 530, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "먹을까요", "start_ms": 780, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "비빔밥 어때요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 105,
"audio_type": "speech",
"words": [
{"word": "비빔밥", "start_ms": 105, "dur_ms": 510, "phase_lr_deg": 4.0},
{"word": "어때요", "start_ms": 670, "dur_ms": 420, "phase_lr_deg": 4.0}
]
},
{
"segment": 7,
"transcript": "좋은 생각이에요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 90, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "생각이에요", "start_ms": 510, "dur_ms": 620, "phase_lr_deg": 0.0}
]
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
"transcript": "맛있겠다",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 115,
"audio_type": "speech",
"words": [
{"word": "맛있겠다", "start_ms": 115, "dur_ms": 440, "phase_lr_deg": 2.0}
]
},
{
"segment": 11,
"transcript": "근처에 유명한 맛집이 있다고 들었어요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "근처에", "start_ms": 130, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "유명한", "start_ms": 650, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "맛집이", "start_ms": 1220, "dur_ms": 490, "phase_lr_deg": 0.0},
{"word": "있다고", "start_ms": 1780, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "들었어요", "start_ms": 2310, "dur_ms": 610, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "진짜요? 그럼 거기로 가요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "진짜요?", "start_ms": 150, "dur_ms": 520, "phase_lr_deg": -3.0},
{"word": "그럼", "start_ms": 780, "dur_ms": 380, "phase_lr_deg": -3.0},
{"word": "거기로", "start_ms": 1220, "dur_ms": 490, "phase_lr_deg": -3.0},
{"word": "가요", "start_ms": 1780, "dur_ms": 350, "phase_lr_deg": -3.0}
]
},
{
"segment": 13,
"transcript": "웨이팅이 길면 어쩌죠",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "웨이팅이", "start_ms": 110, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "길면", "start_ms": 750, "dur_ms": 390, "phase_lr_deg": 0.0},
{"word": "어쩌죠", "start_ms": 1210, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "지금 가면 괜찮을 거예요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 125,
"audio_type": "speech",
"words": [
{"word": "지금", "start_ms": 125, "dur_ms": 390, "phase_lr_deg": 1.0},
{"word": "가면", "start_ms": 580, "dur_ms": 380, "phase_lr_deg": 1.0},
{"word": "괜찮을", "start_ms": 1020, "dur_ms": 480, "phase_lr_deg": 1.0},
{"word": "거예요", "start_ms": 1560, "dur_ms": 450, "phase_lr_deg": 1.0}
]
},
{
"segment": 15,
"transcript": "Excuse me, where is the subway station?",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 160,
"audio_type": "speech",
"words": [
{"word": "Excuse", "start_ms": 160, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "me,", "start_ms": 610, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "where", "start_ms": 950, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 1310, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 1520, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "subway", "start_ms": 1780, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "station?", "start_ms": 2350, "dur_ms": 640, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "저기 사거리에서 우회전하세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "저기", "start_ms": 90, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "사거리에서", "start_ms": 490, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "우회전하세요", "start_ms": 1260, "dur_ms": 810, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "감사합니다 좋은 하루 되세요",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 110, "dur_ms": 710, "phase_lr_deg": -10.0},
{"word": "좋은", "start_ms": 890, "dur_ms": 350, "phase_lr_deg": -10.0},
{"word": "하루", "start_ms": 1310, "dur_ms": 390, "phase_lr_deg": -10.0},
{"word": "되세요", "start_ms": 1750, "dur_ms": 480, "phase_lr_deg": -10.0}
]
},
{
"segment": 18,
"transcript": "천만에요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "천만에요", "start_ms": 100, "dur_ms": 470, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "외국인들이 정말 많아졌네요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 115,
"audio_type": "speech",
"words": [
{"word": "외국인들이", "start_ms": 115, "dur_ms": 780, "phase_lr_deg": 5.0},
{"word": "정말", "start_ms": 950, "dur_ms": 420, "phase_lr_deg": 5.0},
{"word": "많아졌네요", "start_ms": 1420, "dur_ms": 680, "phase_lr_deg": 5.0}
]
},
{
"segment": 20,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 21,
"transcript": "관광지라 그런가 봐요. 우리도 빨리 가죠",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "관광지라", "start_ms": 140, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "그런가", "start_ms": 810, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "봐요.", "start_ms": 1280, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "우리도", "start_ms": 1720, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "빨리", "start_ms": 2210, "dur_ms": 390, "phase_lr_deg": 0.0},
{"word": "가죠", "start_ms": 2650, "dur_ms": 320, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "배고파 죽겠어요. 도착하려면 멀었나요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "배고파", "start_ms": 120, "dur_ms": 510, "phase_lr_deg": 2.0},
{"word": "죽겠어요.", "start_ms": 680, "dur_ms": 580, "phase_lr_deg": 2.0},
{"word": "도착하려면", "start_ms": 1350, "dur_ms": 720, "phase_lr_deg": 2.0},
{"word": "멀었나요?", "start_ms": 2120, "dur_ms": 610, "phase_lr_deg": 2.0}
]
},
{
"segment": 23,
"transcript": "다 왔어요. 저기 간판 보이시죠?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 105,
"audio_type": "speech",
"words": [
{"word": "다", "start_ms": 105, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "왔어요.", "start_ms": 320, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "저기", "start_ms": 820, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "간판", "start_ms": 1250, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "보이시죠?", "start_ms": 1710, "dur_ms": 620, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "사람들이 꽤 많네요. 그래도 금방 빠질 것 같아요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "사람들이", "start_ms": 130, "dur_ms": 620, "phase_lr_deg": -1.0},
{"word": "꽤", "start_ms": 810, "dur_ms": 210, "phase_lr_deg": -1.0},
{"word": "많네요.", "start_ms": 1080, "dur_ms": 490, "phase_lr_deg": -1.0},
{"word": "그래도", "start_ms": 1650, "dur_ms": 480, "phase_lr_deg": -1.0},
{"word": "금방", "start_ms": 2180, "dur_ms": 410, "phase_lr_deg": -1.0},
{"word": "빠질", "start_ms": 2650, "dur_ms": 390, "phase_lr_deg": -1.0},
{"word": "것", "start_ms": 3100, "dur_ms": 180, "phase_lr_deg": -1.0},
{"word": "같아요", "start_ms": 3320, "dur_ms": 450, "phase_lr_deg": -1.0}
]
},
{
"segment": 25,
"transcript": "메뉴판 좀 볼까요? 비빔밥 종류가 많네요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 145,
"audio_type": "speech",
"words": [
{"word": "메뉴판", "start_ms": 145, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 710, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "볼까요?", "start_ms": 980, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "비빔밥", "start_ms": 1520, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "종류가", "start_ms": 2150, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "많네요", "start_ms": 2720, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "전 육회비빔밥이요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "전", "start_ms": 90, "dur_ms": 210, "phase_lr_deg": 3.0},
{"word": "육회비빔밥이요", "start_ms": 350, "dur_ms": 920, "phase_lr_deg": 3.0}
]
},
{
"segment": 27,
"transcript": "전 돌솥비빔밥으로 할게요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 105,
"audio_type": "speech",
"words": [
{"word": "전", "start_ms": 105, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "돌솥비빔밥으로", "start_ms": 380, "dur_ms": 880, "phase_lr_deg": 0.0},
{"word": "할게요", "start_ms": 1310, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "여기요 주문할게요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "여기요", "start_ms": 120, "dur_ms": 420, "phase_lr_deg": 2.0},
{"word": "주문할게요", "start_ms": 590, "dur_ms": 680, "phase_lr_deg": 2.0}
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
"transcript": "음식 나왔습니다 맛있게 드세요",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "음식", "start_ms": 150, "dur_ms": 380, "phase_lr_deg": -15.0},
{"word": "나왔습니다", "start_ms": 580, "dur_ms": 650, "phase_lr_deg": -15.0},
{"word": "맛있게", "start_ms": 1310, "dur_ms": 480, "phase_lr_deg": -15.0},
{"word": "드세요", "start_ms": 1850, "dur_ms": 420, "phase_lr_deg": -15.0}
]
},
{
"segment": 31,
"transcript": "우와 비주얼 대박이네요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "우와", "start_ms": 110, "dur_ms": 350, "phase_lr_deg": 4.0},
{"word": "비주얼", "start_ms": 520, "dur_ms": 480, "phase_lr_deg": 4.0},
{"word": "대박이네요", "start_ms": 1050, "dur_ms": 650, "phase_lr_deg": 4.0}
]
},
{
"segment": 32,
"transcript": "잘 먹겠습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 95,
"audio_type": "speech",
"words": [
{"word": "잘", "start_ms": 95, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "먹겠습니다", "start_ms": 350, "dur_ms": 680, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "역시 맛집이네요 진짜 맛있어요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "역시", "start_ms": 120, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "맛집이네요", "start_ms": 550, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 1220, "dur_ms": 390, "phase_lr_deg": 0.0},
{"word": "맛있어요", "start_ms": 1650, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 35,
"transcript": "고추장이 좀 매운데 그래도 중독성 있어요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 135,
"audio_type": "speech",
"words": [
{"word": "고추장이", "start_ms": 135, "dur_ms": 610, "phase_lr_deg": 2.0},
{"word": "좀", "start_ms": 780, "dur_ms": 210, "phase_lr_deg": 2.0},
{"word": "매운데", "start_ms": 1050, "dur_ms": 480, "phase_lr_deg": 2.0},
{"word": "그래도", "start_ms": 1620, "dur_ms": 450, "phase_lr_deg": 2.0},
{"word": "중독성", "start_ms": 2150, "dur_ms": 580, "phase_lr_deg": 2.0},
{"word": "있어요", "start_ms": 2780, "dur_ms": 420, "phase_lr_deg": 2.0}
]
},
{
"segment": 36,
"transcript": "다 먹고 카페 갈까요?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "다", "start_ms": 100, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "먹고", "start_ms": 320, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "카페", "start_ms": 710, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "갈까요?", "start_ms": 1120, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "좋죠 근처에 예쁜 카페 봐둔 곳 있어요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 125,
"audio_type": "speech",
"words": [
{"word": "좋죠", "start_ms": 125, "dur_ms": 350, "phase_lr_deg": 1.0},
{"word": "근처에", "start_ms": 520, "dur_ms": 480, "phase_lr_deg": 1.0},
{"word": "예쁜", "start_ms": 1050, "dur_ms": 390, "phase_lr_deg": 1.0},
{"word": "카페", "start_ms": 1480, "dur_ms": 380, "phase_lr_deg": 1.0},
{"word": "봐둔", "start_ms": 1920, "dur_ms": 410, "phase_lr_deg": 1.0},
{"word": "곳", "start_ms": 2380, "dur_ms": 180, "phase_lr_deg": 1.0},
{"word": "있어요", "start_ms": 2610, "dur_ms": 420, "phase_lr_deg": 1.0}
]
},
{
"segment": 38,
"transcript": "오 어디요? 궁금하네요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 95,
"audio_type": "speech",
"words": [
{"word": "오", "start_ms": 95, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "어디요?", "start_ms": 350, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "궁금하네요", "start_ms": 820, "dur_ms": 650, "phase_lr_deg": 0.0}
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
"transcript": "한옥 카페인데 분위기가 아주 고즈넉해요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "한옥", "start_ms": 150, "dur_ms": 390, "phase_lr_deg": 1.0},
{"word": "카페인데", "start_ms": 580, "dur_ms": 610, "phase_lr_deg": 1.0},
{"word": "분위기가", "start_ms": 1250, "dur_ms": 620, "phase_lr_deg": 1.0},
{"word": "아주", "start_ms": 1920, "dur_ms": 380, "phase_lr_deg": 1.0},
{"word": "고즈넉해요", "start_ms": 2350, "dur_ms": 780, "phase_lr_deg": 1.0}
]
},
{
"segment": 41,
"transcript": "와 가보고 싶어요. 사진 잘 나오겠는데요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "와", "start_ms": 130, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "가보고", "start_ms": 420, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "싶어요.", "start_ms": 950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "사진", "start_ms": 1520, "dur_ms": 390, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 1950, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "나오겠는데요?", "start_ms": 2210, "dur_ms": 750, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "그럼요. 제가 인생샷 찍어드릴게요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 115,
"audio_type": "speech",
"words": [
{"word": "그럼요.", "start_ms": 115, "dur_ms": 480, "phase_lr_deg": 2.0},
{"word": "제가", "start_ms": 650, "dur_ms": 380, "phase_lr_deg": 2.0},
{"word": "인생샷", "start_ms": 1080, "dur_ms": 520, "phase_lr_deg": 2.0},
{"word": "찍어드릴게요", "start_ms": 1650, "dur_ms": 710, "phase_lr_deg": 2.0}
]
},
{
"segment": 43,
"transcript": "믿음직스럽네요. 기대하겠습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "믿음직스럽네요.", "start_ms": 100, "dur_ms": 820, "phase_lr_deg": 0.0},
{"word": "기대하겠습니다", "start_ms": 980, "dur_ms": 750, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "계산은 제가 할게요. 맛있게 먹었으니까요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "계산은", "start_ms": 140, "dur_ms": 480, "phase_lr_deg": 1.0},
{"word": "제가", "start_ms": 650, "dur_ms": 320, "phase_lr_deg": 1.0},
{"word": "할게요.", "start_ms": 1020, "dur_ms": 390, "phase_lr_deg": 1.0},
{"word": "맛있게", "start_ms": 1550, "dur_ms": 450, "phase_lr_deg": 1.0},
{"word": "먹었으니까요", "start_ms": 2050, "dur_ms": 680, "phase_lr_deg": 1.0}
]
},
{
"segment": 45,
"transcript": "아니에요 제가 낼게요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "아니에요", "start_ms": 110, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 610, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "낼게요", "start_ms": 980, "dur_ms": 420, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "커피는 그럼 제가 살게요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 125,
"audio_type": "speech",
"words": [
{"word": "커피는", "start_ms": 125, "dur_ms": 410, "phase_lr_deg": 2.0},
{"word": "그럼", "start_ms": 580, "dur_ms": 350, "phase_lr_deg": 2.0},
{"word": "제가", "start_ms": 980, "dur_ms": 310, "phase_lr_deg": 2.0},
{"word": "살게요", "start_ms": 1350, "dur_ms": 420, "phase_lr_deg": 2.0}
]
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
"transcript": "좋아요. 그럼 그렇게 하죠",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "좋아요.", "start_ms": 110, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "그럼", "start_ms": 580, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "그렇게", "start_ms": 950, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "하죠", "start_ms": 1410, "dur_ms": 380, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "즐거운 주말이네요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 135,
"audio_type": "speech",
"words": [
{"word": "즐거운", "start_ms": 135, "dur_ms": 480, "phase_lr_deg": 1.0},
{"word": "주말이네요", "start_ms": 650, "dur_ms": 620, "phase_lr_deg": 1.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-04 04:33:35*
