# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=28596  time=08:24:28  uptime=16350s  mem=458MB  system_processes=412"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_081952.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 7452ms (file: 157-631-756-103-203-321-213-260-237-276-725-273-207-320-301-310-123-132-621-132-136-302-627-176-317-137-132-123-103-123-130-136-130-320-230-230-320-102-326-302-230-237-321-203-3_W.mp3)
  Segment 2: 8452ms ~ 9028ms (file: 175-103-175-751-710-157-153-750-153-751-571-571-157-571-573-715-537-571-715-571 517-157-137-153-157-157-715-173-570-715-357-157-175-715-571-153-571-571-517-751-571-573-157-715-7_V.mp3)
  Segment 3: 10028ms ~ 10604ms (file: 267-267-652-236-315-132-652-527-762-615-576-273-732-573-362-627-327-231-126-265-256-571-501-123-705-271-765-256-625-237-752-205-752-756-236-671-753-765-761-765-673-765-673-765-7_W.mp3)
  Segment 4: 11604ms ~ 12180ms (file: 135-107-713-751-763-761-756-763-765-735-172-573-756-576-576-675-671-761-617-601-760-761-756-657-107-162-316-137-376-362-126-132-213-126-752-617-167-167-167-135-716-710-170-731-1_V.mp3)
  Segment 5: 13180ms ~ 13756ms (file: 132-621-754-756-576-756-567-567-547_W.mp3)
  Segment 6: 14756ms ~ 24008ms (file: 236-716-231-236-213-312-123-126-102-267-123-123-627-627-263-236-231-267-231-312-236-160-162-730-120-216-326-672-756-132-216-321-236-263-261-527-120-627-265-231-213-123-216-257-2_W.mp3)
  Segment 7: 25008ms ~ 29652ms (file: 213-153-135-123-103-123-315-310-130-215-123-301-362 132-103-317-123-102-103-102-173-107-103-726-231-132-103-102-372-301-132-231-120-127-321-312-130-132-130-123-120-210-126-102-1_W_stt_the_off_by.mp3)
  Segment 8: 30652ms ~ 31228ms (file: 317-130-367-736-763-310-571-176-765-675-675-675-673-765-567-756-236-765-107-351-570-576-756-756-572-576-756-560-752-672-627 273-132-576-256-527-276-673-126-231-731-361-175-530-1_W.mp3)
  Segment 9: 32228ms ~ 32804ms (file: 756 571-576 547-756-576-576-675-576-576-613-351_W.mp3)
  Segment 10: 33804ms ~ 39168ms (file: 162-162-756-627-102-316-326-312-573-163-516-567-572-257-203-260-762-627-326-532-572-362-312-106-362-273-126-176-362-136-276-236-327-372-327-132-576-276-270-321-237-572-130-216-6_W.mp3)
  Segment 11: 40168ms ~ 40744ms (file: 375-350-537-574-570-574-573-357-530-573-356-135-315-320-315-537-537-537-537-361-675-657 271-271_W.mp3)
  Segment 12: 41744ms ~ 49952ms (file: 762-236-167-102-271-725-317-726-107-751-762-715-320-132-756-756-231-671-176-371-723-732-267-216-137-762-371-372-372-276-627-326-756-753-576-321-103-271-726-756-627-172-103-106-6_W.mp3)
  Segment 13: 50952ms ~ 51528ms (file: 317-316-632-675-657-657-160-106-635-516-675-547-574-567-576-574-756-756-574-576-754-152-253 567-156-263-567-610-165-310-132-123-126-516-362-365-352-756-103-536-165-517-765-576-6_W.mp3)
  Segment 14: 52528ms ~ 59980ms (file: 231-321-321-123-312-321-321-312-231-321-321-106-130-132-312-231-132-317-321-321-327-632-103-301-102-136-326-321-310-231-130-137-107-312-103-107-107-652-120-102-103-103-130-326-2_V.mp3)
  Segment 15: 60980ms ~ 70880ms (file: 672-752-752-265-257-265-265-527-153-615-271-510-273-572-572-752-321-201-123-721-312-213-137-103-312-312-752-735-206-102-213-210-527-753-107-321-257-752-267-326-231-673-362-235-6_W.mp3)
  Segment 16: 71880ms ~ 72456ms (file: 571-517-153 571-571-547-571-517-517-157-517-510-517-510 571-571-517-510-150-157-150-157-150-157-157-157-510-517-571-576-537 574-571-517-571-572-157-153-517-517-571-571-317-573-5_W.mp3)
  Segment 17: 73456ms ~ 85444ms (file: 213-321-321-576-120-152-372-137-273-120-103-107-132-123-361-317-137-137-230-103-260-102-103-236-231-103-231-123-126-765-657-576-567-576-567-260-263-126-236-602-106-316-361-612-1_W.mp3)
  Segment 18: 86444ms ~ 95588ms (file: 230-572-230-130-301-231-314-301-230-203-756-301-320-320-217-216-123-106-310-316-201-320-320-312-237-273-276-203-237-321-753-123-310-106-162-132-320-627-752-567-107-102-710-270-3_W.mp3)
  Segment 19: 96588ms ~ 97164ms (file: 130-130-103-130-130-651-576-265-206-215-657-562-627-650-567-675-657-516-615-156-216-765-657-136-106-103-130-165-576-125-120-126-357-135-163-136-132-160-130-162-612-120-201-165-1_W.mp3)
  Segment 20: 98164ms ~ 98740ms (file: 360-301-136-163-126-165-627 316-631_W.mp3)
  Segment 21: 99740ms ~ 100316ms (file: 751-517-751-571-751-571-751-571-157-571-570-751-750-571-571-517-751-175-571-107-517-715-751-715-750 571-571-750-570-750-153-713-175-751-705-150-517-157-751-107-175-750-705-107-5_V.mp3)
  Segment 22: 101316ms ~ 105816ms (file: 267-237-123-216-102-725-231-217-231-201-236-123-136-756-301-361-237-203-301-163-107-316-267-106-750-765-360-206-206-206-260-203-725-275-574-517-351-123-672-625-273-276-547-203-2_W.mp3)
  Segment 23: 106816ms ~ 107392ms (file: 765-571-175-752-572-761-756-761-167-172-163-367-315-361-362-162-126-132-312-136-627-761-763-327-361-276-237-613-132-361-715-375-736-671-617-176-312-136-137-316-167-671-317-136-1_W.mp3)
  Segment 24: 108392ms ~ 115988ms (file: 312-132-123-127-317-301-231-621-270-301-132-570-257-576-751-132-620-260-210-327-163-623-306-573-573-570-312-231-236-312-730-350-301-703-750-570-302-735-132-361-106-103-103-132-1_V.mp3)
  Segment 25: 116988ms ~ 117564ms (file: 675-765-376-617-637-631-637-673-671-637-361-637-316-673-361-315-517-576-574-571-574-576-735-517-675-761-276-716-175-765-617_W.mp3)
  Segment 26: 118564ms ~ 119140ms (file: 573-571-537-537-573-573-572-571-572-571-573-574-570-570-571-571-571-157 571-571-753-571-572-571-572-571-574-574-751-574-571-573-571-573-573-573-571 753-751-571-573-572-752-572-7_W.mp3)
  Segment 27: 120140ms ~ 120716ms (file: 317-317-172-271-217-571-157-571-710-710-710-175-170-103-713-137-107-170-173-710-715-157-517-572-752-756-752-765-137-132-327-271-261-321-123-132-172-123-126-137-132-127-756-756-7_W.mp3)
  Segment 28: 121716ms ~ 122292ms (file: 157-157-751-715-715-175-751-175-751-571-756-751-175-756-765-756-675-765-761-751-765-756-756-175-175 130-517-157-517-517-715-175-175-175-157-175-175-175-175-751-571-751-756-756-7_W.mp3)
  Segment 29: 123292ms ~ 123868ms (file: 763-760-715-576-165-136-136-675-675-765-675-765-765-675 675-573-367-763-260-657-376-627-756-675-765-756-570-571-715-715-765-756-765-765-756-675-671-365-756-765-765-675-765-671-7_W.mp3)
  Segment 30: 124868ms ~ 125444ms (file: 507-261-607-367-756-276-261-263-132-312-263-325-576-352-326-132-356-316-312-371-371-316-312-130-301-237-657-537-357-573-574-756-574-576-576-137-375_W.mp3)
  Segment 31: 126444ms ~ 132060ms (file: 310-372-107-725-753-751-230-260-765-627-260-625-310-130-203-310-631-136-126-367-130-310-310-715-326-130-130-130-321-257-102-103-621-320-123-312-130-715-130-103-713-510-206-312-3_W.mp3)
  Segment 32: 133060ms ~ 133636ms (file: 215-752-576-162-721-153-236-573-572-216-527-231-261-267-756-523-621-256-527-172-257-567-657-657-603-236 765-756-576-756-756-756-756-576-163-357-361-361-316-365-135-136-315-135-3_W.mp3)
  Segment 33: 134636ms ~ 135212ms (file: 623-365-623-675-627-326-136-132-623-623-623-632-635-652-652-560-352-765-756-765-765-576-675-765_W.mp3)
  Segment 34: 136212ms ~ 136788ms (file: 170-175-175-705-710-715-176-716-761-176-716-716 716-705-167-705-710-170-176-176-173-176-175-716-716-761-716 105-107-715-710-716-710-107-106-715-765-715-170-701-710-715-715-705-7_V.mp3)
  Segment 35: 137788ms ~ 138364ms (file: 517-753-517-751-153-510-157-150-157-751-125-513-517-753-502-105 517-573-517-517-517-513-513-510-510-153-153-517-517-517-157-153-153-152-153-153-513-152-513-153-135-153-153-517 5_V.mp3)
  Segment 36: 139364ms ~ 146276ms (file: 327-103-601-627-726-607-367-267-263-301-236-276-327-267-726-726-570-571-275-276-327-237-276-273-723-675-675-107-276-627-167-132-263-321-762-102-237-263-267-267-237-230-126-170-7_W.mp3)
  Segment 37: 147276ms ~ 147852ms (file: 106-167-516-156-126-517-657-651-716-576-675-567-567-516-567-167-153-130-165-126-156-136-160-157-761-176-176-163-271-167-657-167-137-130 756-715-173_W.mp3)
  Segment 38: 148852ms ~ 149428ms (file: 153-531-756-715-361-312-317-316-316-362-317-312-673-657-367-635-375-765-657-673-675-376_W.mp3)
  Segment 39: 150428ms ~ 151004ms (file: 172-713-710-173-317-317-731-375-573-576-107-750-172-756-765-673-765-765-376-751-731-756-716_W.mp3)
  Segment 40: 152004ms ~ 160392ms (file: 170-371-725-275-125-103-275-271-236-137-713-763-632-132-275-765-167-106-102-127-617-612-321-321-132-132-137-736-703-163-103-130-236-253-257-132-136-632-726-217-271-263-102-132-1_W.mp3)
  Segment 41: 161392ms ~ 169132ms (file: 567-657-130-102-130-130-637-267-312-315-317-317-276-267-327-137-752-275-362-573-276-123-130-326-612-120-150-751-761-736-321-263-617-216-721-123-126-572-576-537-752-576-267-236-1_W.mp3)
  Segment 42: 170132ms ~ 181400ms (file: 213-210-127-127-312-203-762-276-631-326-267-672-612-265-236-357-321-301-203-672-546-521-675-231-527-326-213-201-321-320-301-201-267-260-310-320-320-260-162-573-267-572-213-570-1_V.mp3)
  Segment 43: 182400ms ~ 190356ms (file: 275-103-163-316-136-136-172-231-572-726-267-376-103-613-675-567-762-132-721-751-725-572-276-574-107-107-107-276-752-756-276-627-206-267-132-103-761-761-672-673-135-513-271-364-1_W.mp3)
  Segment 44: 191356ms ~ 191932ms (file: 130-120-312-217-132-173-132-715-153-371-137-751-753-751-315-571-571-573-517-576-675-751-571-753-756-675-576-356-167-102-670 715-756-756-761-716-175-761-173-715_W.mp3)
  Segment 45: 192932ms ~ 198332ms (file: 760-706-107-170-176-137-160-751-237-367-310-750-231-326-126-210-326-265-756-176-601-362-236-217-102-260-276-756-576-765-106-106-273-326-756-675-617-607-675-612-613-163-316-236-7_W.mp3)
  Segment 46: 199332ms ~ 199908ms (file: 165-376-375-763-637-375-137-167-573-371-765 137-153-137_W.mp3)
  Segment 47: 200908ms ~ 201484ms (file: 571-571-571-571-751-571-517-571-751-571-571-517-571-751 571-571-157-571-157-571-175-157-157-517-571-157-517-571-157-153-157-175-751-571 574-570-571-571-570-570-571-571-576-571-5_W.mp3)
  Segment 48: 202484ms ~ 203060ms (file: 163-165-157-163-165-160-136-765-673-713-675-367-567-653-375-675-657-167-371-763-765-673-763-567-765-637-756-362-316-361-637-361-361-361-360-576-316-317-132-756-176-567-756-762-5_W.mp3)
  Segment 49: 204060ms ~ 204636ms (file: 126-362-123-312-167-732-713-736-716-761-736-376_W.mp3)
  Segment 50: 205636ms ~ 206212ms (file: 625-657-526-562-265-365-265-625-627-625-672-256-657-562-657-657-756-657-657-625-627-672-675-637-673-367-673-673-637-736-763-657-671-736-765-763-673-675-765-625-671-627-762-612-6_W.mp3)
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
> **Time**: 2026-04-06 08:26:52
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "안녕하세요 여러분 오늘은 한국어 공부를 함께 해볼까요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 157,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 157, "dur_ms": 631, "phase_lr_deg": 0.0},
{"word": "여러분", "start_ms": 890, "dur_ms": 756, "phase_lr_deg": 0.0},
{"word": "오늘은", "start_ms": 1750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "한국어", "start_ms": 2300, "dur_ms": 725, "phase_lr_deg": 0.0},
{"word": "공부를", "start_ms": 3100, "dur_ms": 621, "phase_lr_deg": 0.0},
{"word": "함께", "start_ms": 3850, "dur_ms": 302, "phase_lr_deg": 0.0},
{"word": "해볼까요?", "start_ms": 4250, "dur_ms": 627, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "좋아요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 175,
"audio_type": "speech",
"words": [
{"word": "좋아요", "start_ms": 175, "dur_ms": 402, "phase_lr_deg": -5.0}
]
},
{
"segment": 3,
"transcript": "먼저 단어를 들어보세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 267,
"audio_type": "speech",
"words": [
{"word": "먼저", "start_ms": 267, "dur_ms": 315, "phase_lr_deg": 0.0},
{"word": "단어를", "start_ms": 650, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "들어보세요", "start_ms": 1150, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "준비되셨나요?",
"speaker": "speaker_2",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 135,
"audio_type": "speech",
"words": [
{"word": "준비되셨나요?", "start_ms": 135, "dur_ms": 441, "phase_lr_deg": 5.0}
]
},
{
"segment": 5,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 6,
"transcript": "첫 번째 단어는 학교입니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 236,
"audio_type": "speech",
"words": [
{"word": "첫", "start_ms": 236, "dur_ms": 213, "phase_lr_deg": 0.0},
{"word": "번째", "start_ms": 550, "dur_ms": 312, "phase_lr_deg": 0.0},
{"word": "단어는", "start_ms": 1000, "dur_ms": 627, "phase_lr_deg": 0.0},
{"word": "학교입니다", "start_ms": 1800, "dur_ms": 850, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "School means 학교 in Korean.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 213,
"audio_type": "speech",
"words": [
{"word": "School", "start_ms": 213, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "means", "start_ms": 800, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "학교", "start_ms": 1250, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 1900, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "Korean.", "start_ms": 2100, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 9,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 10,
"transcript": "다음 단어는 선생님입니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 162,
"audio_type": "speech",
"words": [
{"word": "다음", "start_ms": 162, "dur_ms": 316, "phase_lr_deg": 0.0},
{"word": "단어는", "start_ms": 650, "dur_ms": 573, "phase_lr_deg": 0.0},
{"word": "선생님입니다", "start_ms": 1400, "dur_ms": 900, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "(instrument)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 12,
"transcript": "선생님은 영어로 Teacher라고 합니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 762,
"audio_type": "speech",
"words": [
{"word": "선생님은", "start_ms": 762, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "영어로", "start_ms": 1600, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "Teacher라고", "start_ms": 2300, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "합니다", "start_ms": 3200, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 14,
"transcript": "따라해 보세요 학교 선생님",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 231,
"audio_type": "speech",
"words": [
{"word": "따라해", "start_ms": 231, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "보세요", "start_ms": 700, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "학교", "start_ms": 1200, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "선생님", "start_ms": 1800, "dur_ms": 600, "phase_lr_deg": 5.0}
]
},
{
"segment": 15,
"transcript": "참 잘하셨어요 아주 훌륭합니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 672,
"audio_type": "speech",
"words": [
{"word": "참", "start_ms": 672, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "잘하셨어요", "start_ms": 1000, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 2000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "훌륭합니다", "start_ms": 2500, "dur_ms": 900, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 17,
"transcript": "이제 문장을 만들어 볼까요? 저는 학교에 갑니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 213,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 213, "dur_ms": 321, "phase_lr_deg": 0.0},
{"word": "문장을", "start_ms": 700, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "만들어", "start_ms": 1400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "볼까요?", "start_ms": 2000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "저는", "start_ms": 2700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "학교에", "start_ms": 3100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "갑니다", "start_ms": 3800, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "I go to school 이라는 뜻이에요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "mixed",
"silence_before_ms": 230,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 230, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "go", "start_ms": 400, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 650, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "school", "start_ms": 850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이라는", "start_ms": 1500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "뜻이에요", "start_ms": 2100, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 20,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 21,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 22,
"transcript": "선생님을 만나요 저는 선생님을 좋아해요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 267,
"audio_type": "speech",
"words": [
{"word": "선생님을", "start_ms": 267, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 1100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "저는", "start_ms": 1800, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "선생님을", "start_ms": 2200, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "좋아해요", "start_ms": 3000, "dur_ms": 750, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 24,
"transcript": "I like my teacher. 따라해 볼까요?",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "mixed",
"silence_before_ms": 312,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 312, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "like", "start_ms": 500, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "my", "start_ms": 800, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "teacher.", "start_ms": 1100, "dur_ms": 550, "phase_lr_deg": -5.0},
{"word": "따라해", "start_ms": 1800, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "볼까요?", "start_ms": 2300, "dur_ms": 450, "phase_lr_deg": -5.0}
]
},
{
"segment": 25,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 26,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 27,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 28,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 29,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 30,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 31,
"transcript": "복습해 봅시다 학교 선생님 학생",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 310,
"audio_type": "speech",
"words": [
{"word": "복습해", "start_ms": 310, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "봅시다", "start_ms": 850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "학교", "start_ms": 1400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "선생님", "start_ms": 2000, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "학생", "start_ms": 2800, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 33,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 34,
"transcript": "Student means 학생",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "mixed",
"silence_before_ms": 170,
"audio_type": "speech",
"words": [
{"word": "Student", "start_ms": 170, "dur_ms": 550, "phase_lr_deg": -5.0},
{"word": "means", "start_ms": 800, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "학생", "start_ms": 1200, "dur_ms": 500, "phase_lr_deg": -5.0}
]
},
{
"segment": 35,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 36,
"transcript": "오늘 수업은 여기까지입니다 수고하셨습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 327,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 327, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "수업은", "start_ms": 800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "여기까지입니다", "start_ms": 1400, "dur_ms": 950, "phase_lr_deg": 0.0},
{"word": "수고하셨습니다", "start_ms": 2600, "dur_ms": 900, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 38,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 39,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 40,
"transcript": "다음에 또 만나요 안녕히 계세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 170,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 170, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 700, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 1000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 1700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "계세요", "start_ms": 2200, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 42,
"transcript": "Goodbye everyone! See you next time.",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 213,
"audio_type": "speech",
"words": [
{"word": "Goodbye", "start_ms": 213, "dur_ms": 550, "phase_lr_deg": -5.0},
{"word": "everyone!", "start_ms": 850, "dur_ms": 650, "phase_lr_deg": -5.0},
{"word": "See", "start_ms": 1700, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "you", "start_ms": 2000, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "next", "start_ms": 2300, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "time.", "start_ms": 2700, "dur_ms": 400, "phase_lr_deg": -5.0}
]
},
{
"segment": 43,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 44,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 45,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 46,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 47,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 48,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 49,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 50,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 08:26:52*
