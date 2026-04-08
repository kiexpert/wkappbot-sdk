# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=27304  time=08:34:39  uptime=14456s  mem=255MB  system_processes=409"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_083243.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 713-735-756-571-375-753-571-750-750-517-570-570-756-756 574-574-574-574-574-574-547-547-574-573 571-574-574-573 572-571-574-574-574-574-571 547-574-574-574-574-574-574-576-574-5_W.mp3)
  Segment 2: 1576ms ~ 7300ms (file: 726-726-715-752-725-723-321-210-217-751-127-613-201-602-627-137-301-165-235-307-132-123-123-210-270-312-230-320-256-103-321-120-215-132-102-210-213-231-237-327-327-715-312-132-5_W.mp3)
  Segment 3: 8300ms ~ 8876ms (file: 150-570-153-105-150-105-157-157-715-175-571-153-175-573-751-157-712 571-517-517-571-571-571-571-513-150-105-153-157-571 576-157-517-157-517-517-157-135-751-175-157-175-572-715-5_W.mp3)
  Segment 4: 9876ms ~ 16932ms (file: 316-310-132-137-135-156-107-103-321-527-413-752-321-312-301-256-125-120-123-265-312-206-126-302-130-675-263-310-103-301-237-325-567-657-501-573-765-217-321-236-237-321-201-267-2_W.mp3)
  Segment 5: 17932ms ~ 18508ms (file: 573-576-153-351-157-517-157-517-576-567-165-651-567-562-521-152-163-120-162-510-150-561-516-157-175-157-175-751-157-157-157-517-571-571-175-517-657-576-156-615-162-576-510-157-5_V.mp3)
  Segment 6: 19508ms ~ 20084ms (file: 627-627-316-316-136-316-361-316-136-312-136-361-316-752-173-162-576-157-125-761-271-125-162-625-125-612-175-176-175-156-167-175-157-137-751-725-152-167-751_W.mp3)
  Segment 7: 21084ms ~ 27996ms (file: 312-120-107-102-267-617-603-267-276-107-736-123-231-361-362-216-164-326-570-761-120-312-326-273-106-126-673-620-120-316-632-765-123-310-302-130-107-102-675-276-267-627-702-163-7_W.mp3)
  Segment 8: 28996ms ~ 29572ms (file: 152-156-175-135-315-135-312-132-136-135-130-103-301-315-135-315-103-103-150-135-130-310-130-105-153-157-150-150-516-157-156-517-157-157-517-157-157-156-167-156-130-135-130-130-1_V.mp3)
  Segment 9: 30572ms ~ 31148ms (file: 175-173-573-571-571-751 576-517-570-135-750-175-157-715-751-157-573-517-715-750-751-571-517-753-715-576 574-574-576-571-570-753-573-571-751-753-157-570-751-571-571-751-573-576-5_W.mp3)
  Segment 10: 32148ms ~ 43704ms (file: 321-163-176-301-321-756-573-612-206-230-675-120-230-320-753-173-153-541-130-163-263-276-123-301-130-130-310-573-765-620-260-752-756-276-753-132-130-571-230-203-207-231-756-754-7_W.mp3)
  Segment 11: 44704ms ~ 45280ms (file: 620-736-736-716-715-716-761-176-176-671-163 765-763-761-716_W.mp3)
  Segment 12: 46280ms ~ 46856ms (file: 756-507-671-157-165-561-170-157-673-673-671-167-675-671-761-623-567-657-536-736-167-167-136-167-176-671-376-672-673-675-765-765-675-673_W.mp3)
  Segment 13: 47856ms ~ 48432ms (file: 125-652-123-625-672-625-236-635-126-762-675-672-563-675-657-627-257-526-526-236-657-756-576-675-572-632-675-765-657-765-625-257-625-256-635-625-326-537-543-546-540-504-504-514-6_W.mp3)
  Segment 14: 49432ms ~ 56524ms (file: 617-236-237-251-617-617-276-236-752-574-267-103-627-260-163-716-167-201-236-572-573-163-267-362-107-175-106-167-136-136-756-267-267-160-132-326-327-261-726-107-276-517-106-163-3_W.mp3)
  Segment 15: 57524ms ~ 64400ms (file: 136-627-107-302-230-302-267-607-574-547-576-537-576-312-675-302-362-301-263-306-132-173-713-317-130-132-102-102-162-231-263-132-130-316-371-316-106-230-132-312-320-320-312-103-3_W.mp3)
  Segment 16: 65400ms ~ 65976ms (file: 547-547-132-654-537-153-635-132-302-753-703-102-312-301-132-102 653 756-267 175 165-362 657_V.mp3)
  Segment 17: 66976ms ~ 67552ms (file: 765-763-671-765-756-263-263-263-623-623-632-632-657-675-675-623-756-753-735-571-517-576-573-576-567-312-653-671-637-673-367-617-613-637-607-630-167-607-571-756-751-756-375-132-6_W.mp3)
  Segment 18: 68552ms ~ 69128ms (file: 130-751-756-751-103-130-316-657-123-761-312-273-621-765-765-675-765-765-765-675-765-576-765 136-271-175-173-137-102-175-531-237-136-612-730-761-317-132-136-310-137-316-312-317-5_W.mp3)
  Segment 19: 70128ms ~ 76788ms (file: 216-263-213-102-213-203-120-217-251-260-321-512-123-312-231-132-210-216-213-123-521-251-213-210-123-210-213-213-210-120-102-213-123-123-132-132-132-312-312-312-123-132-132-102-1_W.mp3)
  Segment 20: 77788ms ~ 78364ms (file: 573 576-576-574-574-574-574-574-574-576-574-574-570-576 570-574 574-576-571-574-574-574-576-570-570-570-570-570-570-574-576-571 571-574-574-574-574-574-574-574-570-574-574 570-5_W.mp3)
  Segment 21: 79364ms ~ 85592ms (file: 261-132-762-752-216-267-572-752-572-267-267-627-260-213-267-725-267-754-574-257-672-167-765-760-574-570-574-260-201-275-107-716-726-672-261-627-267-103-136-206-520-574-137-173-2_W.mp3)
  Segment 22: 86592ms ~ 93936ms (file: 271-267-702-371-130-307-276-270-320-130-103-150-137-123-310-130-123-103-375-130-310-573-310-167-137-310-231-152-123-132-127-275-217-127-217-120-271-216-721-201-123-271-217-276-1_W.mp3)
  Segment 23: 94936ms ~ 95512ms (file: 601-657-735-756-176-602-617-160-573-137-251-327-271-276-162-217-231-712-703-372-123-132-301-132-123-573-312-723-352-267-725-352-257-132-536-217-137-752-672-372-705-657-213-567-1_W.mp3)
  Segment 24: 96512ms ~ 97088ms (file: 316-610-136-130-571-576-567-156-571-651-516-157-657-657-106-157-135-136-317-136-156-257-367-137-123-372-370-317-310-351-317-310 136-130-103-106 351-315 751-127-213-235-312-512-1_W.mp3)
  Segment 25: 98088ms ~ 98664ms (file: 574-574 157-751-576 570-571 572-572-574-512-507-751-751-576-317-573-751-751-715-715-751-573-751-571-571-570 576-576-576-576-576-576-571-756-516-571-751-571-573-756-751-751-571-5_W.mp3)
  Segment 26: 99664ms ~ 100240ms (file: 532-521-753 157-526-352-176-156-157-157-650-157-157-156-315-351-652-635-756-576-756-756-671-657-657-567-576-157-510-756-517-675-765-735-756-675-627-237-326-256 312-763-326-567-5_W.mp3)
  Segment 27: 101240ms ~ 101816ms (file: 716-751-150-175-136-136-163-163-163-163-163-175-317-137 361-532-312-132-132-132-137-137-137-137-135-137-137-761-176-765-761-716-736-175-715-715-513-517-571-715-710-173-137-132-1_W.mp3)
  Segment 28: 102816ms ~ 103392ms (file: 132-123-132-136-163-136-107-175-107-751-705-715-170-170-107-715-751-701-175-170-157-132-126-126-130-123-132-123-132-120-130-173-135-135-175-751-715-107-701-570-751-705-701-571-1_W.mp3)
  Segment 29: 104392ms ~ 104968ms (file: 753-756-756-756-765-751-756-753-765-735-531-753-657-657-756-753-756-735-576-576-573-576-753-756-672-756-756-137-317-317-317-375-756-735-135-351-573-753-317_W.mp3)
  Segment 30: 105968ms ~ 106544ms (file: 175-751-175-710-157-175-571-517 571-571-517-157-517-571-517-571-751-576-571-517-751-175-517-571-751-715-157 576-570-751-571-517-571-571-571-571-570-157-571-751-571-715-157-571-7_V.mp3)
  Segment 31: 107544ms ~ 108120ms (file: 517-571-571-573-571-573-571-571 571-570-753-751-571-137-317-357-573-571-751-571-751-751-571-517-571-571-570-570-571 571-175-751-751-713-715-751-571-571-570-571-570 570-571-571-5_V.mp3)
  Segment 32: 109120ms ~ 109696ms (file: 756-765-756-765-756-753-715-715-756-752-671-251-263-673-637-637-367-631-316-317-153-316-716-752-172-617-127-327-317-216-123-306-620-306-306-362-613-316-310-316-376-716-637-753-1_W.mp3)
  Segment 33: 110696ms ~ 119480ms (file: 321-524-103-263-620-267-625-627-265-105-132-236-263-136-201-203-326-132-103-163-203-321-165-573-326-630-631-263-261-263-627-267-326-312-162-267-267-267-102-621-235-236-362-572-2_W.mp3)
  Segment 34: 120480ms ~ 126564ms (file: 320-270-230-230-132-210-312-301-237-312-316-163-312-230-236-230-371-103-217-621-213-326-301-301-163-103-312-137-103-132-637-103-267-362-103-203-206-362-321-360-316-312-237-236-7_W.mp3)
  Segment 35: 127564ms ~ 136636ms (file: 236-260-260-132-136-203-103-136-260-102-103-260-153-312-132-103-137-132-760-317-301-316-302-103-301-312-106-312-106-103-132-103-173-175-135-326-320-260-136-321-231-103-132-132-2_V.mp3)
  Segment 36: 137636ms ~ 146996ms (file: 123-253-527-267-312-571-132-514-207-261-120-572-502-206-230-150-213-261-132-321-570-215-213-103-123-312-312-132-321-620-136-107-135-317-102-276-520-250-754-260-267-576-207-132-1_V.mp3)
  Segment 37: 147996ms ~ 155844ms (file: 231-372-163-103-102-230-231-273-203-730-726-276-206-276-672-213-271-213-132-270-270-321-321-236-263-321-230-103-106-103-753-106-310-271-213-216-126-132-164-106-137-765-706-120-1_W.mp3)
  Segment 38: 156844ms ~ 157420ms (file: 136-317-132-132-173-317-136-316-317-716-317-316-317-317-317-361-132-137-317-173-137-130-137-132-135-137-173-371-135-235-312-172-126-175-167-175-173-176-176-137-127-153-175-751-7_W.mp3)
  Segment 39: 158420ms ~ 164720ms (file: 507-201-302-602-672-207-231-317-325-132-627-625-570-321-265-547-306-362-267-265-206-260-213-627-312-317-126-271-126-612-627-261-357-752-231-620-265-756-102-310-261-504-206-625-3_W.mp3)
  Segment 40: 165720ms ~ 166296ms (file: 726-763-672-761-673-617-167-635-136-756-276-317-716-173-317-762-675-672-736-763-675-675-763-762-715-762-671-736-672-712-176-612-621-617-612-176-672-716-637-627-635-631-761-672-6_W.mp3)
  Segment 41: 167296ms ~ 173128ms (file: 172-671-761-376-271-327-273-372-312-712-317-103-217-750-670-620-126-721-765-672-671-312-301-372-362-326-163-132-376-370-320-307-106-673-306-103-236-203-132-123-261-260-267-210-2_W.mp3)
  Segment 42: 174128ms ~ 174704ms (file: 715-573 573-574-573-571-571-573-571-571-570-571-571-715 574-574-573-574-571-574-574-574-574-574-571-571-571-571-574-576-571 573-571-571-571-573-574-574-574-571-574-574-573-574-5_W.mp3)
  Segment 43: 175704ms ~ 183120ms (file: 371-307-301-103-130-310-623-213-237-102-127-712-107-175-173-276-217-546-130-310-301-130-267-263-130-215-236-263-320-263-127-235-103-130-253-267-157-127-267-302-107-275-620-267-2_W.mp3)
  Segment 44: 184120ms ~ 184696ms (file: 135-312-310-132-130-316-176-167-153-367-316-507-506-315-157-315-301-317-310-317-312-312-136-132-136-312-132-136-312-317-132-137-317-317-137-317-132-132-574-375-751-573-135-130-3_W.mp3)
  Segment 45: 185696ms ~ 193328ms (file: 263-762-756-753-106-107-765-765-762-236-261-312-716-316-132-216-752-357-107-765-310-623-103-751-301-267-614-130-631-103-132-132-102-153-136-126-136-137-103-103-361-163-361-172-6_W.mp3)
  Segment 46: 194328ms ~ 203940ms (file: 760-136-216-107-675-106-201-203-103-230-102-260-120-102-135-132-162-106-130-231-321-230-230-320-106-126-106-102-267-256-216-312-321-210-123-106-206-201-130-235-312-102-312-321-3_V.mp3)
  Segment 47: 204940ms ~ 205516ms (file: 153-153-135-715-150-517-571 517-517_W.mp3)
  Segment 48: 206516ms ~ 213608ms (file: 103-715-562-625-526-250-325-103-236-301-213-375-357-327-526-103-236-267-752-203-276-260-310-321-632-675-756-275-265-275-625-130-231-320-351-506-752-256-326-132-231-261-310-321-2_W.mp3)
  Segment 49: 214608ms ~ 215184ms (file: 571-571-576-571 571-517-715-573-571-571-517-751-753-571-751-570-753-571-570-571-570-571-571-573-571-571-573 574-571-571-507-715-517-571-751-375-571-573-715-751-751-751-715-751-7_V.mp3)
  Segment 50: 216184ms ~ 222880ms (file: 106-167-106-175-576-130-267-102-263-236-756-725-236-753-137-132-103-263-263-126-321-261-123-167-756-765-756-517-106-753-106-267-137-103-176-237-123-103-103-527-267-327-130-263-1_W.mp3)
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
> **Time**: 2026-04-06 08:36:38
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
{"word": "안녕하세요", "start_ms": 150, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 1400, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 2100, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "참", "start_ms": 3000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 3500, "dur_ms": 900, "phase_lr_deg": 0.0}
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
"transcript": "공부하기 딱 좋은 날씨인 것 같아요 그쵸",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "공부하기", "start_ms": 200, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "딱", "start_ms": 1400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 1800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "날씨인", "start_ms": 2400, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 3300, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 3600, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "그쵸", "start_ms": 4500, "dur_ms": 600, "phase_lr_deg": 0.0}
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
"transcript": "일본어로 인사는 오하이오 고자이마스라고 해요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "일본어로", "start_ms": 180, "dur_ms": 900, "phase_lr_deg": 5.0},
{"word": "인사는", "start_ms": 1150, "dur_ms": 700, "phase_lr_deg": 5.0},
{"word": "오하이오", "start_ms": 2000, "dur_ms": 1000, "phase_lr_deg": 5.0},
{"word": "고자이마스라고", "start_ms": 3100, "dur_ms": 1400, "phase_lr_deg": 5.0},
{"word": "해요", "start_ms": 4600, "dur_ms": 500, "phase_lr_deg": 5.0}
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
"transcript": "영어로 아침 인사는 Good morning입니다 다들 아시죠",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "영어로", "start_ms": 250, "dur_ms": 800, "phase_lr_deg": 5.0},
{"word": "아침", "start_ms": 1100, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "인사는", "start_ms": 1700, "dur_ms": 700, "phase_lr_deg": 5.0},
{"word": "Good", "start_ms": 2500, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "morning입니다", "start_ms": 3000, "dur_ms": 1200, "phase_lr_deg": 5.0},
{"word": "다들", "start_ms": 4500, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "아시죠", "start_ms": 5000, "dur_ms": 600, "phase_lr_deg": 5.0}
]
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
"transcript": "자 이제 본격적으로 가사 연습을 시작해볼까요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "자", "start_ms": 120, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "본격적으로", "start_ms": 1000, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "가사", "start_ms": 2300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "연습을", "start_ms": 2900, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "시작해볼까요", "start_ms": 3700, "dur_ms": 1200, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "첫 번째 문장은 내가 제일 잘나가 입니다",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 300,
"audio_type": "speech",
"words": [
{"word": "첫", "start_ms": 300, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "번째", "start_ms": 600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "문장은", "start_ms": 1100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "내가", "start_ms": 1800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "제일", "start_ms": 2400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "잘나가", "start_ms": 2900, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "입니다", "start_ms": 3700, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
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
"transcript": "발음할 때 혀의 위치에 주의해서 따라해 보세요",
"speaker": "speaker_2",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "발음할", "start_ms": 200, "dur_ms": 800, "phase_lr_deg": 5.0},
{"word": "때", "start_ms": 1100, "dur_ms": 300, "phase_lr_deg": 5.0},
{"word": "혀의", "start_ms": 1500, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "위치에", "start_ms": 2000, "dur_ms": 700, "phase_lr_deg": 5.0},
{"word": "주의해서", "start_ms": 2800, "dur_ms": 1000, "phase_lr_deg": 5.0},
{"word": "따라해", "start_ms": 3900, "dur_ms": 600, "phase_lr_deg": 5.0},
{"word": "보세요", "start_ms": 4600, "dur_ms": 600, "phase_lr_deg": 5.0}
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
"transcript": "아주 잘하고 있어요 조금 더 크게 말해볼까요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "아주", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "잘하고", "start_ms": 650, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 1350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 2000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 2600, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "크게", "start_ms": 2900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "말해볼까요", "start_ms": 3400, "dur_ms": 1100, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "자신감을 가지고 한 번 더 반복해 보겠습니다",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "자신감을", "start_ms": 200, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "가지고", "start_ms": 1200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 1900, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "번", "start_ms": 2200, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 2500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "반복해", "start_ms": 2900, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "보겠습니다", "start_ms": 3700, "dur_ms": 1200, "phase_lr_deg": 0.0}
]
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
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 31,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
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
"transcript": "이번에는 속도를 조금 높여서 연습해 볼게요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "이번에는", "start_ms": 140, "dur_ms": 800, "phase_lr_deg": 5.0},
{"word": "속도를", "start_ms": 1000, "dur_ms": 600, "phase_lr_deg": 5.0},
{"word": "조금", "start_ms": 1700, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "높여서", "start_ms": 2300, "dur_ms": 700, "phase_lr_deg": 5.0},
{"word": "연습해", "start_ms": 3100, "dur_ms": 600, "phase_lr_deg": 5.0},
{"word": "볼게요", "start_ms": 3800, "dur_ms": 600, "phase_lr_deg": 5.0}
]
},
{
"segment": 34,
"transcript": "어려워도 포기하지 말고 끝까지 화이팅",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "어려워도", "start_ms": 180, "dur_ms": 900, "phase_lr_deg": 5.0},
{"word": "포기하지", "start_ms": 1150, "dur_ms": 800, "phase_lr_deg": 5.0},
{"word": "말고", "start_ms": 2050, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "끝까지", "start_ms": 2650, "dur_ms": 700, "phase_lr_deg": 5.0},
{"word": "화이팅", "start_ms": 3500, "dur_ms": 800, "phase_lr_deg": 5.0}
]
},
{
"segment": 35,
"transcript": "잘하고 있어요 거의 다 왔습니다 조금만 더 힘내세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 220,
"audio_type": "speech",
"words": [
{"word": "잘하고", "start_ms": 220, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "거의", "start_ms": 1600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 2100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "왔습니다", "start_ms": 2400, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "조금만", "start_ms": 3400, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 4100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "힘내세요", "start_ms": 4400, "dur_ms": 900, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "마지막 구절은 우리 모두 함께 노래해요 입니다",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "마지막", "start_ms": 250, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "구절은", "start_ms": 1000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "우리", "start_ms": 1800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "모두", "start_ms": 2300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "함께", "start_ms": 2800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "노래해요", "start_ms": 3400, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "입니다", "start_ms": 4300, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "자 다같이 크게 외쳐볼까요 하나 둘 셋",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "자", "start_ms": 100, "dur_ms": 300, "phase_lr_deg": 5.0},
{"word": "다같이", "start_ms": 500, "dur_ms": 600, "phase_lr_deg": 5.0},
{"word": "크게", "start_ms": 1200, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "외쳐볼까요", "start_ms": 1700, "dur_ms": 1100, "phase_lr_deg": 5.0},
{"word": "하나", "start_ms": 3000, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "둘", "start_ms": 3600, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "셋", "start_ms": 4100, "dur_ms": 500, "phase_lr_deg": 5.0}
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
"transcript": "정말 훌륭해요 오늘 수업은 여기서 마치겠습니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "훌륭해요", "start_ms": 750, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 1700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "수업은", "start_ms": 2200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "여기서", "start_ms": 2900, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "마치겠습니다", "start_ms": 3600, "dur_ms": 1200, "phase_lr_deg": 0.0}
]
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
"transcript": "수고하셨습니다 다음 시간에 또 만나요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "수고하셨습니다", "start_ms": 120, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 1500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "시간에", "start_ms": 2000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 2700, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 3000, "dur_ms": 600, "phase_lr_deg": 0.0}
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
"transcript": "잊지 말고 복습하는 것 꼭 기억하세요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "잊지", "start_ms": 180, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "말고", "start_ms": 650, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "복습하는", "start_ms": 1200, "dur_ms": 800, "phase_lr_deg": 5.0},
{"word": "것", "start_ms": 2100, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "꼭", "start_ms": 2400, "dur_ms": 300, "phase_lr_deg": 5.0},
{"word": "기억하세요", "start_ms": 2800, "dur_ms": 900, "phase_lr_deg": 5.0}
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
"transcript": "안녕히 계세요 바이바이",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "계세요", "start_ms": 800, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "바이바이", "start_ms": 1600, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
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
"transcript": "마지막으로 한 번 더 들어볼까요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "마지막으로", "start_ms": 200, "dur_ms": 1000, "phase_lr_deg": 5.0},
{"word": "한", "start_ms": 1300, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "번", "start_ms": 1600, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "더", "start_ms": 1900, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "들어볼까요", "start_ms": 2200, "dur_ms": 900, "phase_lr_deg": 5.0}
]
},
{
"segment": 49,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 50,
"transcript": "감사합니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 100, "dur_ms": 1000, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 08:36:38*
