# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=14716  time=03:41:12  uptime=11628s  mem=136MB  system_processes=363"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260404_034026.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 231-213-132 362-326-621-216-162-316-132-123-312-623-621-612-631-326-312-612-132_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 367-613-602-316-763-310-716-162-761-756-765-132-132-350-326-326-326-367-673-673-675-673-312-130-130-620-574-576-576-134-201-632-602-672-672-617-670-760-603 672_W.mp3)
  Segment 3: 3152ms ~ 9416ms (file: 120-261-267-216-267-276-726-576-265-620-206-260-267-726-235-517-750-675-725-562-123-126-316-207-216-607-267-267-267-236-507-576-570-756-352-257-176-726-760-271-127-762-273-753-2_W.mp3)
  Segment 4: 10416ms ~ 10992ms (file: 175-165-675-674-706-705-175-132-175-765-760-761-675-617-564-546-547-547-627-267-265-265-357-637-326-241-203-261-123-236-203-236-725-763-753-576-127-172-310-217-507-576-576-507-5_V.mp3)
  Segment 5: 11992ms ~ 15916ms (file: 327-327-372-376-326-671-267-207-207-726-725-201-201-207-270-260-271-273-276-376-273-763-723-732-137-371-752-312-132-267-127-231-627-273-712-712-721-276-756-765-763-175-371-756-7_W.mp3)
  Segment 6: 16916ms ~ 17492ms (file: 261-261-263 231-126-263-326-321-312-236-362-627-132-321-627-632-672-673-132-263-263-213-526-627-672-261-627-275-652-362-621-360-257-217-361-312-672 617-627-617-610-216-621-672-6_W.mp3)
  Segment 7: 18492ms ~ 19068ms (file: 162-612-361-625-756-263-236-126-326-213-236-276-263-237-576-753-365-367_W.mp3)
  Segment 8: 20068ms ~ 20644ms (file: 761-765-617-162-126-326 126-231-236-321-236-216_W.mp3)
  Segment 9: 21644ms ~ 22220ms (file: 672-763-627-672-123-136-126-310-726-125-126-210-216_W.mp3)
  Segment 10: 23220ms ~ 23796ms (file: 306-376-237-102-216-132-312-602-612-670-706-675-763-106-132-316-165-621-672-670-612-601-613-163-163-236-236-213-231-267-276-276-327-126-263-130-162-123-320-627-623-126-261-216-1_V.mp3)
  Segment 11: 24796ms ~ 25372ms (file: 316-623 106-301-673-762-576-762-607-760-107-173-317-671-613-136-163-167-123-132-761-170-716-106-671-607-567-672-206-203-102-320-130-320-312-310-102-123-632-536-357-650-106-103-1_W.mp3)
  Segment 12: 26372ms ~ 26948ms (file: 657-567-617-160-103-236-132-136-631-261-613-130-627-312-672-621-623-675-675-167-176-136-123-312-120-132-754-756-765-672-762-176-107-601-621-750-570-570-620-103-103-136-103-326-3_W.mp3)
  Segment 13: 27948ms ~ 28524ms (file: 236-620-260-620-620-675-657-672-671-267-132-361-620-672-206-763-716-706-670-672-162-612-762-162-612-631-361-132-157-312-213-163-130-126-123-123-127_W.mp3)
  Segment 14: 29524ms ~ 30100ms (file: 725 172-150-567-576-572-576-365-567-572-752-754-176-376-613-326-236-361-765-765-765-765-760-321-751-136-756 725_W.mp3)
  Segment 15: 31100ms ~ 31676ms (file: 160-615-136-136-165-137-136-136-312-167-316-103-315-103-136-163-316-106-130-103-103-310-165-132-130-162-371-162-103-170-167-137-163-536-135-167-130-165-165-671-167-601-136-130-1_V.mp3)
  Segment 16: 32676ms ~ 40488ms (file: 672-107-203-627-216-257-201-132-126-231-671-675-167-613-163-165-106-130-167-176-130-173-103-106-176-167-160-320-670-310-130-210-301-321-103-203-627-276-267-362-103-103-316-163-3_W.mp3)
  Segment 17: 41488ms ~ 42064ms (file: 675-675-657-136-130-176-325-375-136-132-172-132-312-132-310-547-576-567-675-675-675-672-675-627-136-132-123-213-623-162-631-637-657-567-572-576-753-715-127-123-132-136-132-126-2_W.mp3)
  Segment 18: 43064ms ~ 43640ms (file: 135-760-376-317-361-316-130-261-106-167-362-365-256-130-136-132-206-230-261-321-136-312-163-716-107-102-127-103-103-132-163-612-126-612-621-671-136-132-762-627-267-163-153-607-6_V.mp3)
  Segment 19: 44640ms ~ 45216ms (file: 165-132-136-136-167-130-106-216-267-132-130-136-135-132-103-615-136-103-106-756-312-167-162-316-130-103-263-367-152-142 327 162-167-231-625-652-576-571_V.mp3)
  Segment 20: 46216ms ~ 46792ms (file: 723 762-672-162-312-136-316-135-132-315-143-142-163-132-312-132_V.mp3)
  Segment 21: 47792ms ~ 48368ms (file: 627-165-136-163-132-653-103-103-106-165-617-167-176-167-103-137-516-126-327-763-726-765-762-675-765-674-756-752-765-725 162-516-263-162-106-103-132-672-671-671-670-673-360-237-1_W.mp3)
  Segment 22: 49368ms ~ 49944ms (file: 756-671-751-765-367-312-360-362-261 257-617-102-176-136-175-657-162-162-617-102-167-103-136 567-165-610-103-130-103-716-106-176-562-102-103-163-156-621-102-632-231-652-261-256-1_W.mp3)
  Segment 23: 50944ms ~ 51520ms (file: 260-260-265-265-267-630-627-657-760-627-160-163-103-163-162-612-163-301-316-301-302-302 540-543-756-675 750-574-754-130-130-310-130-302-320-103-132-106-103-132-163-136-312-316-1_W.mp3)
  Segment 24: 52520ms ~ 53096ms (file: 167-123-132-261-163-367-163-216-316 106-610-136-130-102-136-130-153-316-136-137-316-310-106-160-135-136-167-106_W.mp3)
  Segment 25: 54096ms ~ 54672ms (file: 301-302-320-302-301-317 276-251-175-312-321-132-231-206-206-267-702-132-176-176-321-765-760-763-756-732-567-561-675-167-167-173-612-162-163-132-612-657-651 130-132-123-134-657-1_W.mp3)
  Segment 26: 55672ms ~ 56248ms (file: 612-260-163-162-617-761-675-673-675-167-231-123-132-176-103-165-316-165-675-601-601-163-617-316-172-167-107-130-310-130-130-103-715-213-172-126-137-316-126-106-617-106-106-106-1_W.mp3)
  Segment 27: 57248ms ~ 57824ms (file: 574-756 163-106-160-617-716-106-167-175-352-162-126-167-106-516-623-162-612-136-106-103-176-152-725-675-230-237-512-235-312-103-576-675-130-302-562-576-526-562-567-657-526-236-2_W.mp3)
  Segment 28: 58824ms ~ 63540ms (file: 576-756-765-167-267-173-176-362-721-726-276-267-627-216-236-673-236-312-130-765-321-123-671-217-217-210-321-326-136-130-120-210-210-172-271-132-123-136-326-312-132-231-237-361-3_W.mp3)
  Segment 29: 64540ms ~ 65116ms (file: 621-265-650-602-627-162-126-162-103-765-657-726-762-672-671-765-761-102-631-163-236-230-236-263-326-316-312-652-356-230-130-132-360-603-675-603-672-162-617-613-126-106-716-162-2_W.mp3)
  Segment 30: 66116ms ~ 66692ms (file: 302-302-203-231-203-320-310-326-312-136-103-130-321-320-302-716-302-132-136-312-301-321-321-302-312-123-362-321-630-312-321-326-362-326-327-312-103-512-321-230-320-310-301-302-2_W.mp3)
  Segment 31: 67692ms ~ 68268ms (file: 657-657-657-657-165-156-567-651-561-156-615-765-657-567-715-156-536-672-651-576-625-165-651-652-165-165-165-612-165-165-156-657-756-751-715-760-617-163-163-167-167-167-162-165-6_W.mp3)
  Segment 32: 69268ms ~ 69844ms (file: 132-106-103-514-150-136-261-625-231-316-517-517-571-165-562-503-576-315-120-126-261-637-365-316-136-123-216-657-563-602-210-126-657-625-627-326-356-753-657-637-312-326-612-562-6_W.mp3)
  Segment 33: 70844ms ~ 71420ms (file: 321-675-756-325-576-576-576-136-173-176-176-136-167-126-132 675-716-716 612-672_W.mp3)
  Segment 34: 72420ms ~ 72996ms (file: 671-512-261-263-621-123-132-301-103-132-132-263-765-756-317-163-571-657-126-260-132-706-761-762-760-627-126-216-716-756-263-261 657-564-567_W.mp3)
  Segment 35: 73996ms ~ 74572ms (file: 137-307-367-260-206-206-320-547-574-230-305-301-576-270-320-302-317-570-675-103-163-160-617-175-571 672-126-132-162-123-213-176-137-132-302-235-135-723-317-123-132-123-126-163-1_V.mp3)
  Segment 36: 75572ms ~ 76148ms (file: 623-632-716-627-607-106-102-103-123-176-316-765-675-675-607-132-176-236-153-125-132-143-165-153-135-613-623-206-512 627-267-267-621-725-176-602-637-672-673-617-761-317-672-231-6_W.mp3)
  Segment 37: 77148ms ~ 77724ms (file: 316-103-103-231-132-130-120-310-320-230-230-320-302-302-731-731-316-306-103-137-301-610-301-236-321-230 312-230-307-130-176-160-160-627-106-517-167-106-601-106-172-567-162-136-1_V.mp3)
  Segment 38: 78724ms ~ 79300ms (file: 123-761-126-106-103-136-103-167-163-105-165-103-167-716-136-136-157-136-167-163-103-167 610-103-132-167-167-602-165-617-103 512-175-162-152-132-103-713-106-126-675-165-612-136-1_V.mp3)
  Segment 39: 80300ms ~ 80876ms (file: 231-263-236-263-627-263-126-312-301-762-672-672-576-756-213-321-257-236-621-312-173-132-132-362-236-263-326-213-365-653-261-162-216-106-106-103-316-671-623-362-310-301-130-230-1_W.mp3)
  Segment 40: 81876ms ~ 82452ms (file: 236-261-613-567-165 103 312-713-574-276-374-374-731-513-132-213-654-756-132-163 750-723-371-103-136-132-137-315-216-362-126-716-761-367-641-321 310-312-316 127-672-765-176-107-6_V.mp3)
  Segment 41: 83452ms ~ 84028ms (file: 673-163 612-765-267-263-627-562-526-265-637-765-657-672-671-672-627-627-617-276-267 620-160-750-705-163-762-765-675-670-756-756-753-670-760-132-756-754 756-756-756-762-267_W.mp3)
  Segment 42: 85028ms ~ 85604ms (file: 672-627-627-627-263-623-627-621-623-163_W.mp3)
  Segment 43: 86604ms ~ 87180ms (file: 657-567-567-502-576-527-672-236-627-605-375-321-632-623-675 276-206-203-261-607-671-132-316-612-136-132-134-126-167-134-612-130-672-316-326-301-312-130-163-106-106-326-312-126-2_W.mp3)
  Segment 44: 88180ms ~ 88756ms (file: 206-102-710-510-756-526-567-657-301-301-306-675-607-613-165-106-126-102-361-632-316-162-163-162-261-623-267-603-621-160-326-302-126-160-260-326-312-312-376-321-106-516-230-230-1_W.mp3)
  Segment 45: 89756ms ~ 90332ms (file: 102-613-612-765-765-675-761-160-201-623-560-260-210-612-621-627-265-621-672-130-310-130-571-721-275-326-310-310-103-263-213-627-672-632-162-627-532-652-263-265-267-675-652-625-2_W.mp3)
  Segment 46: 91332ms ~ 101664ms (file: 321-321-312-310-263-321-130-231-106-103-326-231-267-231-312-267-123-753-103-107-230-732-753-756-267-312-756-765-672-276-752-132-130-267-267-123-102-130-637-136-176-102-765-126-1_W.mp3)
  Segment 47: 102664ms ~ 103240ms (file: 576-126 631-130-612-136-136-631-301-563-561-631-301-132-312-136-163-531-316-361-367-326-631-361-362-367-316-312-316-316-326-361-516-135-316-316-317-315-317-317-357-317-317-175 6_W.mp3)
  Segment 48: 104240ms ~ 104816ms (file: 257-625-263-206-621-216-261-670-765-627-236-316-312-261-607-306-103-136-103-102-132-312-301-132-132-132-123-263-236-263-230-263-261-216-126-231-163-132-316-132-136-301-310-136-1_V.mp3)
  Segment 49: 105816ms ~ 106392ms (file: 102-130-103-102-103-102-120-102-120-120-123-102-123-213-102-102-216-103 143-170-176-612-321-362-106-261-712 627-621-632-765-176 103-316-106-621-263-362-632-316-162-103-136-156-3_V.mp3)
  Segment 50: 107392ms ~ 107968ms (file: 165-132-103-507-715-175-137-136-132-143-132-134-137-136-173-570-507-576-175-176-167-132-132-176-301-132-103-160-216-516-502-506-651-621-506-570-615-106-106-126-126-163-312-123-6_V.mp3)
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
> **Time**: 2026-04-04 03:42:11
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "현실은 50센티의 눈밭에 오케까지",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "현실은", "start_ms": 20, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "50센티의", "start_ms": 140, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "눈밭에", "start_ms": 300, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "오케까지", "start_ms": 400, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "푹푹 빠지는 강제 눈 수영입니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "푹푹", "start_ms": 15, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "빠지는", "start_ms": 110, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "강제", "start_ms": 220, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "눈", "start_ms": 310, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "수영입니다", "start_ms": 360, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "표현되는 게 많지가 않거든요 진짜 많이 준비를 해보세요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "표현되는", "start_ms": 30, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 180, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "많지가", "start_ms": 230, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "않거든요", "start_ms": 340, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 470, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 560, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "준비를", "start_ms": 640, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "해보세요", "start_ms": 750, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "기계 부품을 가공할 수 있는 뭐",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "기계", "start_ms": 10, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "부품을", "start_ms": 100, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "가공할", "start_ms": 210, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 330, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 370, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 440, "dur_ms": 40, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "그런 것을 찾아야 되겠다 그래서 청계천을 무조건 갔고요 다들 안 해주시려고 하죠 예 그러다가 이제",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "그런", "start_ms": 20, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "것을", "start_ms": 90, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "찾아야", "start_ms": 150, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "되겠다", "start_ms": 250, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 340, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "청계천을", "start_ms": 420, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "무조건", "start_ms": 550, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "갔고요", "start_ms": 640, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "다들", "start_ms": 720, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 790, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "해주시려고", "start_ms": 830, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "하죠", "start_ms": 950, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "예", "start_ms": 1000, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "그러다가", "start_ms": 1040, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 1140, "dur_ms": 40, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 7,
"transcript": "공산품을 주로 보는 거고요 이게 나가게 되면 농산품 수산",
"speaker": "speaker_4",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "공산품을", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "주로", "start_ms": 170, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "보는", "start_ms": 240, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "거고요", "start_ms": 320, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 390, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "나가게", "start_ms": 450, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "되면", "start_ms": 530, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "농산품", "start_ms": 580, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "수산", "start_ms": 680, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "맞는 말입니다 근데 그걸로 매매하면 항상 늦습니다 시장은 항상 유보다 먼저 자리를 만들어",
"speaker": "speaker_5",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "맞는", "start_ms": 25, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "말입니다", "start_ms": 100, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 220, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "그걸로", "start_ms": 290, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "매매하면", "start_ms": 370, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "항상", "start_ms": 500, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "늦습니다", "start_ms": 590, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "시장은", "start_ms": 700, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "항상", "start_ms": 800, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "유보다", "start_ms": 880, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "먼저", "start_ms": 980, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "자리를", "start_ms": 1070, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "만들어", "start_ms": 1170, "dur_ms": 80, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
*Generated by WKAppBot ask gemini — 2026-04-04 03:42:11*
