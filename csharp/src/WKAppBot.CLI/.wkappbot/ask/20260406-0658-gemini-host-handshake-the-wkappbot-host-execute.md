# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=30344  time=06:56:47  uptime=11823s  mem=437MB  system_processes=412"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_065555.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 263 273-321-236-126-236-650-263-267-756-567 136-275-327-765-215-203-206-206-326-276-316-275-306-132-321-652-257-257-570-301-136-310-136-312-316-526-367-361-316-315-527-625-675-2_V.mp3)
  Segment 2: 1576ms ~ 11260ms (file: 120-213-261-301-310-310-103-302-730-736-756-103-710-765-765-173-165-217-201-132-132-134-231-132-201-123-237-302-267-267-312-123-231-106-310-321-321-312-312-602-301-210-126-120-3_V.mp3)
  Segment 3: 12260ms ~ 12836ms (file: 576-576-315-651-136-567-576-651-756-576-576-756-567-756-156-156-157-576-517-165-157-156-156-175-157-576-567-516-152-567-567-576-156-176-756-576-657-756-156-152-175-135-135-167-1_W.mp3)
  Segment 4: 13836ms ~ 14412ms (file: 627-671-637-627-621-632-623-623-621-621 263_W.mp3)
  Segment 5: 15412ms ~ 22072ms (file: 267-675-103-631-617-261-612-673-612-160-163-613-316-103-143-310-523-507-751-756-267-132-310-537-631-756-102-361-326-536-321-132-132-673-261-725-756-230-321-756-310-216-106-136-3_W.mp3)
  Segment 6: 23072ms ~ 33224ms (file: 520-260-217-756-213-103-132-136-132-213-230-103-573-532-135-321-123-236-532-230-203-310-362-130-673-630-360-637-673-765-637-136-317-267-267-276-127-276-321-310-201-206-527-154-1_W.mp3)
  Segment 7: 34224ms ~ 34800ms (file: 507-752-573-735-752-326-231-327-726-752-672-126-207-267-317-310-265-203-752-567-236-756-752-623-254-236-725-326-265-257-572-321-263-562-236-265-576-627-256-567-523-356-756-752-2_V.mp3)
  Segment 8: 35800ms ~ 36376ms (file: 537-231-573-675-672-267-123-750-756-132-123-167-321-321-301-312-305-123-721-612-231-726-375-213-357-123-250-362-132-625-263-275-132-523-572-125-216-312-267-251-512-126-573-231-3_V.mp3)
  Segment 9: 37376ms ~ 37952ms (file: 736-106-756-576-756-763-574-351_W.mp3)
  Segment 10: 38952ms ~ 45828ms (file: 127-275-317-317-357-136-173-316-132-103-103-103-130-123-672-157-176-752-671-102-231-267-276-620-752-123-210-123-216-273-270-217-201-276-267-267-216-267-267-210-216-312-136-107-3_W.mp3)
  Segment 11: 46828ms ~ 47404ms (file: 136-756-317-132-756-563-576 761-671-761-671-763-760_W.mp3)
  Segment 12: 48404ms ~ 48980ms (file: 765 216-236-672-671-170-175-351-132-102-167-617-675-627-263-612-316-763-163-135-317-103-120-726-617-627-673-506-210-621-507-672-137-601-371-103-103-213-750-261-316-132-723-361-5_W.mp3)
  Segment 13: 49980ms ~ 50556ms (file: 312-317-362-317-361-316-137-136-316-136-136-317-136-675-675-673-365-657-657-637-631-361-173-637 263-321-263-567-175-765-127-321-502-217-573-125-135-137-127-513-752-573-321-126-1_W.mp3)
  Segment 14: 51556ms ~ 61060ms (file: 130-123-213-312-231-312-312-321-130-130-136-130-312-231-123-326-362-213-321-103-312-231-321-132-312-312-312-312-312-123-130-312-312-132-312-230-237-213-231-270-213-720-720-127-1_W.mp3)
  Segment 15: 62060ms ~ 70016ms (file: 210-312-310-230-120-725-756-752-301-230-571-572-136-130-201-103-321-103-173-137-106-610-675-216-132-120-102-620-263-260-205-201-102-321-210-215-132-102-120-203-207-201-231-375-2_W.mp3)
  Segment 16: 71016ms ~ 77676ms (file: 326-160-106-163-267-716-750-106-321-167-267-126-106-160-175-162-167-160-716-617-761-103-130-263-167-302-263-102-715-570-260-237-172-176-167-750-752-617-751-237-375-102-126-106-1_W.mp3)
  Segment 17: 78676ms ~ 88252ms (file: 216-725-217-132-102-132-275-201-213-572-275-623-130-130-173-172-276-172-210-201-210-217-270-217-261-201-275-675-756-725-765-765-132-137-270-723-276-271-273-270-276-725-270-276-7_W.mp3)
  Segment 18: 89252ms ~ 89828ms (file: 360-631-637-637-163-162-312-130-310 137_W.mp3)
  Segment 19: 90828ms ~ 91404ms (file: 763-167-673-672-623-261-123-137-312-675-137-136-136-167-136-136-163-163-167-175-157-671-173-176-765-217-276-216-501-152-107-167-301-312-132-317-132-137-136-126-165-132-570-675-5_V.mp3)
  Segment 20: 92404ms ~ 99172ms (file: 102-120-275-312-125-570-102-275-273-206-123-571-726-321-213-123-765-206-126-120-123-106-102-752-257-756-506-265-231-627-627-607-267-273-102-132-103-132-213-731-765-754-372-765-2_W.mp3)
  Segment 21: 100172ms ~ 107948ms (file: 756-132-312-317-312-103-231-206-260-201-521-547-572-213-521-572-716-276-357-135-132-207-753-132-231-572-532-325-213-257-236-572-537-152-213-574-162-126-712-103-132-126-321-527-5_W.mp3)
  Segment 22: 108948ms ~ 114240ms (file: 537-153-562-271-271-301-231-175-617-175-326-172-167-306-231-321-726-103-132-102-513-106-201-527-157-312-316-231-207-263-625-206-206-253-103-320-301-763-320-231-276-102-130-326-3_W.mp3)
  Segment 23: 115240ms ~ 124384ms (file: 675-275-265-213-231-254-547-103-143-173-267-276-201-260-720-213-273-256-547-205-103-137-213-103-206-721-102-130-213-312-230-261-213-216-256-253-715-675-576-130-231-132-173-716-1_W.mp3)
  Segment 24: 125384ms ~ 125960ms (file: 176-106-160-176-123-561-752-756-706-716-706-621-271-570-627-540-267-257-351-132-251-261-176-325-725-756-675-267-237-623-203-207-725-237-762-237-321-312-302-265-573-751-567-567-5_V.mp3)
  Segment 25: 126960ms ~ 127536ms (file: 356-351-517-517-537-365-573-573-576-576-576-130-130-567-135-573-576-576-560-351-617-265-756-672-625-612-312-312-713-132-327-376-213-635-163-651-627-576-637-637-761-615-156-536-5_W.mp3)
  Segment 26: 128536ms ~ 135232ms (file: 754-576-627-675-607-657-361-103-561-620-263-267-675-310-765-273-375-357-371-267-312-236-130-756-567-106-127-175-127-170-132-273-276-276-271-371-137-132-310-261-173-320-273-207-2_W.mp3)
  Segment 27: 136232ms ~ 136808ms (file: 157-156-517-157-135-351-571-175-175-157-107-157-157-157-157-157-175-175-156-157-157-517-567-571-576-175-157-107-170-170-157-175-157-157-156-156-157-751-657-517-315-135-153-173-1_V.mp3)
  Segment 28: 137808ms ~ 138384ms (file: 301 523-325-320-173-137-316-237-276-627-612 602-507-371-321-235-367-725-312-750-156-751-520-216-165-576-153-205-231-562-251-251-751-135-657-137-265-752-217-352-236-362-627-231-3_V.mp3)
  Segment 29: 139384ms ~ 139960ms (file: 754-517-367-756-237-217-702-263-537-571-765-762-231-731-237-236-527-607-571-267-267-603-576-576-752-570-574-312-527-671-302-136-160-153-571-756-756-351-317-376-371-371-317-137-1_W.mp3)
  Segment 30: 140960ms ~ 148304ms (file: 126-216-206-760-132-260-103-123-710-320-103-103-153-310-123-206-327-267-206-574-574-312-126-123-103-135-231-217-127-627-126-301-132-607-130-206-261-670-276-102-751-213-103-312-3_W.mp3)
  Segment 31: 149304ms ~ 160248ms (file: 260-210-137-103-132-103-132-317-276-103-301-130-321-103-130-321-236-103-310-137-753-135-132-261-320-137-312-312-317-301-136-107-106-163-361-310-103-320-132-326-326-312-136-301-1_V.mp3)
  Segment 32: 161248ms ~ 170356ms (file: 263-231-126-601-567-756-756-672-756-103-673-326-326-623-326-126-162-201-102-627-765-765-321-627-362-216-260-261-621-120-210-765-236-261-362-213-123-612-132-106-571-574-236-326-3_W.mp3)
  Segment 33: 171356ms ~ 171932ms (file: 213-276-576-573-523-325-572-732-372-573-301-362-215-235-326-326-215-276-531-261-652-761-627-132-620-257-327-574-570-621-537-572-756-765-157-370-276-526-236-536-163-576-632-623-2_V.mp3)
  Segment 34: 172932ms ~ 184776ms (file: 312-321-231-103-231-213-236-132-163-103 306-310-312-231-260-167-312-320-301-237-203-106-216-712-132-312-231-216-726-276-617-231-261-302-206-236-206-302-301-316-130-236-260-126-1_W.mp3)
  Segment 35: 185776ms ~ 186352ms (file: 751-137-136-715-715-173-135-753-315-136-637-136-361-310-136-312_W.mp3)
  Segment 36: 187352ms ~ 187928ms (file: 765-762-327-275-754-265-267-236-627-267-570-705-275-732-756-267-506-562-756-275-756-267-267-750-257-267-627-271-156-657-572-260-612-520-267-275-276-327-257-726-765-765-657-576-6_W.mp3)
  Segment 37: 188928ms ~ 195984ms (file: 726-723-260-756-162-725-327-765-163-726-267-167-107-726-627-107-172-726-236-765-271-725-237-756-276-120-237-123-765-267-267-726-162-265-362-160-167-210-126-726-672-236-263-162-1_W.mp3)
  Segment 38: 196984ms ~ 202636ms (file: 207-725-203-765-672-317-257-132-752-570-267-270-254-120-257-267-273-672-672-317-106-312-237-123-260-267-316-750-253-302-312-236-123-271-576-267-172-370-270-127-271-267-123-372-7_W.mp3)
  Segment 39: 203636ms ~ 204212ms (file: 763-763-765-765-765-756-756-765-756-367-706-375-175-715-167-173-671-175-715-731-763-726-732-715-751-736-765-617-675-761-637-761-713-137-376-372-372-321-372-312-312-327_W.mp3)
  Segment 40: 205212ms ~ 214284ms (file: 237-270-230-325-756-527-260-721-102-163-176-103-301-123-263-263-231-137-137-231-320-537-267-623-236-320-107-137-312-321-327-321-302-302-263-312-236-231-152-320-230-230-231-231-1_W.mp3)
  Segment 41: 215284ms ~ 215860ms (file: 157-175-157-107-103-107-103-135-103-175-137-105-175-157-157-157-157-157-517-157-517-157-157-517-517-517-571-517-571-157-715-573-173-715-135-103-175-157-175-157-157-157-156-157-5_V.mp3)
  Segment 42: 216860ms ~ 217436ms (file: 102-130-312-210-251-310-306-617-125-562-362-376-637-365-651-537-572-356-315-352-523-537-536-321-357-357-763-376-765-763-765-765-673-763-657-367 132-301_W.mp3)
  Segment 43: 218436ms ~ 219012ms (file: 560-567-526-316-263-572-627-263-362-756-527-257-567-516-632-506-635-670-637-263-127-271-503-756-267-276-567-261-253-732-632-627-752-504-702-372-263-362-627-675-571-235-352-152-3_V.mp3)
  Segment 44: 220012ms ~ 220588ms (file: 263-321-312-132-162-256-623-613-162-603-321-721-156-136-312-315-135_W.mp3)
  Segment 45: 221588ms ~ 222164ms (file: 312-531-513-513-351-357-130-561-567-651-365-361-316-317-317-317-316-316-312-312-312-312-123-312-132-310-132-312-130-132-132_W.mp3)
  Segment 46: 223164ms ~ 223740ms (file: 237-756-217-276-217-236-263-607-627-321-231-237-726-265-126-725-752-507 760-276-213-267-576-567-162-632-231-260-263-216-256-261-275-275-263-352-327-351-237-725-627-213-267-123-1_V.mp3)
  Segment 47: 224740ms ~ 225316ms (file: 273-326-672-756-267-570-352-327-572 627 167-567-572-726-762-271-260-672-267-765-257-520-620_V.mp3)
  Segment 48: 226316ms ~ 233192ms (file: 176-621-261-261-756-576-572-357-213-576-672-270-720-257-235-235-524-312-312-673-261-621-325-231-321-257-257-620-105-102-102-106-132-231-213-271-273-276-765-765-176-372-137-531-1_W.mp3)
  Segment 49: 234192ms ~ 242004ms (file: 103-713-103-756-517-574-312-320-103-106-106-103-130-310-312-573-312-312-103-106-321-236-236-632-753-571-167-321-173-106-231-571-276-231-213-267-263-236-103-236-206-310-301-753-2_W.mp3)
  Segment 50: 243004ms ~ 243580ms (file: 570-325-135-173-530-621-217-127-721-371-326-547-127-527-257-251-752-127-627-507-576-507-321-357-265-156-576-712-213-132-675-763-326 765-765-261-371-574-263 765-316-513-273-351-5_V.mp3)
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
> **Time**: 2026-04-06 06:58:16
> **AI**: gemini

---

## Response

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
{"word": "날씨가", "start_ms": 1500, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "참", "start_ms": 2100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 2450, "dur_ms": 600, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 5,
"transcript": "공부하기 딱 좋은 시간이에요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "공부하기", "start_ms": 200, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "딱", "start_ms": 950, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 1250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "시간이에요", "start_ms": 1700, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "한국어 발음을 연습할 때는 천천히 따라 하는 것이 중요합니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 180, "dur_ms": 600, "phase_lr_deg": 5.0},
{"word": "발음을", "start_ms": 850, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "연습할", "start_ms": 1400, "dur_ms": 550, "phase_lr_deg": 5.0},
{"word": "때는", "start_ms": 2000, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "천천히", "start_ms": 2500, "dur_ms": 700, "phase_lr_deg": 5.0},
{"word": "따라", "start_ms": 3300, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "하는", "start_ms": 3750, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "것이", "start_ms": 4150, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "중요합니다", "start_ms": 4600, "dur_ms": 900, "phase_lr_deg": 5.0}
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
"transcript": "Let's try some English words too.",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "Let's", "start_ms": 100, "dur_ms": 300, "phase_lr_deg": 5.0},
{"word": "try", "start_ms": 450, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "some", "start_ms": 850, "dur_ms": 250, "phase_lr_deg": 5.0},
{"word": "English", "start_ms": 1150, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "words", "start_ms": 1700, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "too.", "start_ms": 2150, "dur_ms": 300, "phase_lr_deg": 5.0}
]
},
{
"segment": 11,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "en",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 12,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
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
"transcript": "이 문장은 조금 더 길게 말해 보겠습니다",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 220,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 220, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "문장은", "start_ms": 420, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 1100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 1550, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "길게", "start_ms": 1800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "말해", "start_ms": 2300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "보겠습니다", "start_ms": 2750, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "받침 발음에 주의하면서 들어보세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "받침", "start_ms": 140, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "발음에", "start_ms": 700, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "주의하면서", "start_ms": 1300, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "들어보세요", "start_ms": 2200, "dur_ms": 750, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "목소리의 톤과 억양을 따라하면 효과적입니다",
"speaker": "speaker_2",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "목소리의", "start_ms": 250, "dur_ms": 650, "phase_lr_deg": 5.0},
{"word": "톤과", "start_ms": 950, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "억양을", "start_ms": 1400, "dur_ms": 550, "phase_lr_deg": 5.0},
{"word": "따라하면", "start_ms": 2000, "dur_ms": 700, "phase_lr_deg": 5.0},
{"word": "효과적입니다", "start_ms": 2750, "dur_ms": 950, "phase_lr_deg": 5.0}
]
},
{
"segment": 17,
"transcript": "자신감을 가지고 크게 소리 내어 읽어보세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "자신감을", "start_ms": 110, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "가지고", "start_ms": 850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "크게", "start_ms": 1350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "소리", "start_ms": 1800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "내어", "start_ms": 2200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "읽어보세요", "start_ms": 2550, "dur_ms": 800, "phase_lr_deg": 0.0}
]
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
"transcript": "매일 꾸준히 연습하는 것이 가장 중요합니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "매일", "start_ms": 130, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "꾸준히", "start_ms": 580, "dur_ms": 550, "phase_lr_deg": 5.0},
{"word": "연습하는", "start_ms": 1180, "dur_ms": 700, "phase_lr_deg": 5.0},
{"word": "것이", "start_ms": 1930, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "가장", "start_ms": 2330, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "중요합니다", "start_ms": 2780, "dur_ms": 900, "phase_lr_deg": 5.0}
]
},
{
"segment": 21,
"transcript": "여러분도 할 수 있어요 포기하지 마세요",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 160,
"audio_type": "speech",
"words": [
{"word": "여러분도", "start_ms": 160, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 850, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 1100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 1350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "포기하지", "start_ms": 2000, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "마세요", "start_ms": 2750, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "자 이제 다음 단계로 넘어가 볼까요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 190,
"audio_type": "speech",
"words": [
{"word": "자", "start_ms": 190, "dur_ms": 250, "phase_lr_deg": 5.0},
{"word": "이제", "start_ms": 490, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "다음", "start_ms": 890, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "단계로", "start_ms": 1340, "dur_ms": 550, "phase_lr_deg": 5.0},
{"word": "넘어가", "start_ms": 1940, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "볼까요", "start_ms": 2490, "dur_ms": 500, "phase_lr_deg": 5.0}
]
},
{
"segment": 23,
"transcript": "반복해서 듣는 것이 듣기 실력 향상에 도움이 됩니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "반복해서", "start_ms": 120, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "듣는", "start_ms": 920, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "것이", "start_ms": 1370, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "듣기", "start_ms": 1770, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "실력", "start_ms": 2220, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "향상에", "start_ms": 2670, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "도움이", "start_ms": 3320, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "됩니다", "start_ms": 3870, "dur_ms": 600, "phase_lr_deg": 0.0}
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
"transcript": "화이팅입니다 여러분 끝까지 응원할게요",
"speaker": "speaker_2",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "화이팅입니다", "start_ms": 200, "dur_ms": 900, "phase_lr_deg": 5.0},
{"word": "여러분", "start_ms": 1150, "dur_ms": 600, "phase_lr_deg": 5.0},
{"word": "끝까지", "start_ms": 1800, "dur_ms": 650, "phase_lr_deg": 5.0},
{"word": "응원할게요", "start_ms": 2500, "dur_ms": 850, "phase_lr_deg": 5.0}
]
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
"transcript": "질문이 있다면 언제든 물어보세요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "질문이", "start_ms": 150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "있다면", "start_ms": 700, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "언제든", "start_ms": 1300, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "물어보세요", "start_ms": 1900, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "오늘 수업은 여기서 마치겠습니다 수고하셨습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "수업은", "start_ms": 550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "여기서", "start_ms": 1100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "마치겠습니다", "start_ms": 1650, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "수고하셨습니다", "start_ms": 2600, "dur_ms": 1000, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "Great job everyone, see you next time.",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "Great", "start_ms": 140, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "job", "start_ms": 550, "dur_ms": 300, "phase_lr_deg": 5.0},
{"word": "everyone,", "start_ms": 900, "dur_ms": 600, "phase_lr_deg": 5.0},
{"word": "see", "start_ms": 1600, "dur_ms": 250, "phase_lr_deg": 5.0},
{"word": "you", "start_ms": 1900, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "next", "start_ms": 2150, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "time.", "start_ms": 2550, "dur_ms": 400, "phase_lr_deg": 5.0}
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
"transcript": "내일도 같은 시간에 만나요 안녕히 계세요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "내일도", "start_ms": 180, "dur_ms": 550, "phase_lr_deg": 5.0},
{"word": "같은", "start_ms": 780, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "시간에", "start_ms": 1230, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "만나요", "start_ms": 1780, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "안녕히", "start_ms": 2330, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "계세요", "start_ms": 2880, "dur_ms": 600, "phase_lr_deg": 5.0}
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
"transcript": "Keep practicing until you feel comfortable.",
"speaker": "speaker_2",
"confidence": 0.93,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "Keep", "start_ms": 120, "dur_ms": 300, "phase_lr_deg": 5.0},
{"word": "practicing", "start_ms": 470, "dur_ms": 750, "phase_lr_deg": 5.0},
{"word": "until", "start_ms": 1270, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "you", "start_ms": 1720, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "feel", "start_ms": 1970, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "comfortable.", "start_ms": 2370, "dur_ms": 800, "phase_lr_deg": 5.0}
]
},
{
"segment": 38,
"transcript": "다음에 또 유익한 내용으로 찾아오겠습니다",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 110, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 610, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "유익한", "start_ms": 860, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "내용으로", "start_ms": 1460, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "찾아오겠습니다", "start_ms": 2160, "dur_ms": 1000, "phase_lr_deg": 0.0}
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
"transcript": "마지막으로 한 번 더 읽어볼까요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "마지막으로", "start_ms": 200, "dur_ms": 750, "phase_lr_deg": 5.0},
{"word": "한", "start_ms": 1000, "dur_ms": 250, "phase_lr_deg": 5.0},
{"word": "번", "start_ms": 1300, "dur_ms": 250, "phase_lr_deg": 5.0},
{"word": "더", "start_ms": 1600, "dur_ms": 250, "phase_lr_deg": 5.0},
{"word": "읽어볼까요", "start_ms": 1900, "dur_ms": 700, "phase_lr_deg": 5.0}
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
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
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
"transcript": "학습을 완료했습니다 수고 많으셨습니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "학습을", "start_ms": 140, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "완료했습니다", "start_ms": 800, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "수고", "start_ms": 1700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "많으셨습니다", "start_ms": 2100, "dur_ms": 900, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "안녕히 가세요 또 만나요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 110, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "가세요", "start_ms": 650, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "또", "start_ms": 1200, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "만나요", "start_ms": 1450, "dur_ms": 500, "phase_lr_deg": 5.0}
]
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
*Generated by WKAppBot ask gemini — 2026-04-06 06:58:16*
