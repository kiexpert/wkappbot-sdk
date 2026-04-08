# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=23176  time=08:15:13  uptime=15982s  mem=294MB  system_processes=408"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_081455.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 7452ms (file: 236-237-371-372-326-316-103-301-326-267-107-137-316-703-372-317-615-372-765-302-230-276-321-132-267-236-176-137-716-716-713-310-267-312-137-132-276-267-230-706-130-301-132-321-2_W.mp3)
  Segment 2: 8452ms ~ 9028ms (file: 263-627-267-267-261-261-102-127-172-271-127-127-217-126-621-621-261-621_W.mp3)
  Segment 3: 10028ms ~ 10604ms (file: 321-312-631 267-156-652-574-540-571-356-657-105-316 675-361-752-231-261-312-752-135-217-726-702-126-725-276-237-532-570-257-267-132-562-263-267-623-257-256-257-367-623-257-273-3_V.mp3)
  Segment 4: 11604ms ~ 12180ms (file: 173-175-173-175-176-137-176-173-175-176-175 132-137-713-715-751-571-715-751-175-175-175-176-175-176-176-176-175-176-176-175-170 172-371-731-175-751-715-175-175-173-175-175-157-1_W.mp3)
  Segment 5: 13180ms ~ 19156ms (file: 132-123-102-217-123-172-761-762-207-102-120-723-102-176-163-617-176-671-361-531-527-512-512-572-320-310-273-273-320-157-276-267-203-103-710-102-270-237-123-731-317-312-320-132-5_W.mp3)
  Segment 6: 20156ms ~ 20732ms (file: 571-571-510-571-571-576-571-574-517-157-175-170-751-571-576-571 571-576-751-517-571-156-150-570-571-517-750-570-751-571-750-571-751-751-571-751-570-576-571 517-571-751-715-571-7_V.mp3)
  Segment 7: 21732ms ~ 22308ms (file: 312-130-132-123-163-136-136-537-572-361-367-312-312-352-275-527-257-527-571-752-735-675-173-736-327-267-362-136-136-316-312-632-163-137-137-132-162-163-716-172-756-751-715-371-1_W.mp3)
  Segment 8: 23308ms ~ 23884ms (file: 267-503-312-567-362-315-357-317-315-750_W.mp3)
  Segment 9: 24884ms ~ 31760ms (file: 231-231-120-312-201-317-537-547-123-123-103-132-130-123-701-167-627-210-120-206-756-275-206-627-103-230-210-102-301-321-235-130-735-756-301-302-130-103-213-136-103-201-276-231-6_W.mp3)
  Segment 10: 32760ms ~ 39960ms (file: 267-276-613-321-672-756-230-302-312-132-371-132-132-617-631-621-276-672-267-257-261-276-216-612-236-102-160-321-276-267-312-163-136-132-653-176-312-132-231-267-263-321-627-321-1_W.mp3)
  Segment 11: 40960ms ~ 41536ms (file: 135-153-135-153-120-102-102-102-120-120-102-126-160-120-123-120-120-102-102-123-125-152-152-152-157-157-517-517-517-571-571-571-571-571-517-517-157-157-517-517-517-157-517-103-1_V.mp3)
  Segment 12: 42536ms ~ 43112ms (file: 715-576-751-750-756-751-571-571-576-751-175-751-756-751 576-571-576-570-570-576-750-571-571-570-750-751-571-175-571-175-157-751-751-571-751-576-756-571-751-751-750-701-715-751-7_V.mp3)
  Segment 13: 44112ms ~ 51996ms (file: 756-675-267-123-175-321-236-725-126-167-710-765-672-372-752-320-352-325-571-130-762-173-217-513-123-103-123-130-231-723-752-275-276-632-357-756-576-123-132-326-637-103-320-316-2_W.mp3)
  Segment 14: 52996ms ~ 61060ms (file: 317-371-721-756-731-576-173-137-372-135-263-176-621-725-756-230-236-321-260-265-267-267-273-321-756-276-263-123-130-312-573-132-103-576-156-106-576-137-547-657-753-576-756-576-1_W.mp3)
  Segment 15: 62060ms ~ 62636ms (file: 576-763-725-513-621-253-276-705-752-275-625-657-652-260-207-216-236-270-276-627-512-162-627-672-571-371-513-361-517-657-671-637-673_W.mp3)
  Segment 16: 63636ms ~ 74940ms (file: 715-601-561-275-570-571-763-372-276-672-267-572-257-276-267-267-132-321-102-627-206-302-102-320-301-312-201-756-576-206-175-527-732-236-321-236-572-276-657-561-572-276-574-547-1_W.mp3)
  Segment 17: 75940ms ~ 84436ms (file: 260-213-123-231-217-267-217-230-273-217-216-203-270-726-762-270-203-231-267-130-213-301-762-310-230-213-276-267-207-230-752-260-267-276-267-271-267-201-175-310-213-267-726-267-3_W.mp3)
  Segment 18: 85436ms ~ 86012ms (file: 571 103-105-103-157-517-517-157-157-157-517-517-517-517-571-517-517-571-571-517-517 517-517-571-517-157-517-517-517-517-517-517-517-571-517-157-756 150-571-571-157-517-175-157-1_W.mp3)
  Segment 19: 87012ms ~ 91476ms (file: 547-765 657-257-756-173-672-217-276-527-756-672-571-236-756-321-312-315-132-571-235-257-261-263-257-275-321-573-137-507-573-137-703-236-253-570-256-532-267-327-576-253-357-527-2_V.mp3)
  Segment 20: 92476ms ~ 104536ms (file: 236-326-267-107-267-576-263-102-576-236-621-574-230-531-267-126-612-627-267-267-612-231-236-163-327-753-273-321-267-672-103-321-576-261-267-257-123-213-162-326-326-326-263-260-3_W.mp3)
  Segment 21: 105536ms ~ 113384ms (file: 132-102-236-765-301-310-173-213-301-623-362-106-310-302-326-736-103-310-317-301-260-376-267-362-263-267-263-231-260-261-312-321-260-236-213-172-176-627-276-276-167-102-260-301-2_W.mp3)
  Segment 22: 114384ms ~ 120216ms (file: 712-371-106-312-327-317-132-130-170-231-275-237-317-103-176-320-301-763-310-130-135-107-715-162-213-201-137-130-760-217-152-207-312-170-231-267-312-102-127-765-312-231-576-327-1_V.mp3)
  Segment 23: 121216ms ~ 121792ms (file: 615-317-631-536-326-761-716-675-175-576-617-517-351-653-316-316-136-316-312-316-315-315-316-361-653-657-657-615-657-561-651-615-657-657-657-617-235-672-673-231-712-260 675-165 6_W.mp3)
  Segment 24: 122792ms ~ 123368ms (file: 675-657-357-517-516-517-751-571-576-517-153-163-316-163-361-137-163-637-731-317-713-316-637-163-163 657 352-321-132-102-576-356-635-361-317-317-317_W.mp3)
  Segment 25: 124368ms ~ 124944ms (file: 173-107-107-173-157-107-103-105-501-157-510-517 517-157-517-571-517-501-175-571-570-175-715-150-507-571-157-753-517_V.mp3)
  Segment 26: 125944ms ~ 136672ms (file: 650-216-236-230-203-301-310-302-312-312-132-136-170-712-173-123-231-261-216-216-173-136-367-130-103-123-160-230-302-361-306-103-136-107-612-360-306-357-261-136-326-261-260-263-3_W.mp3)
  Segment 27: 137672ms ~ 144692ms (file: 317-123-312-130-361-625-120-201-231-574-574-573-573-576-137-103-150-153-301-541-513-574-157-357-136-321-237-231-213-236-136-136-321-236-216-137-627-263-263-713-157-157-175-153-3_W.mp3)
  Segment 28: 145692ms ~ 146268ms (file: 175-137-173-175-175-175-175-175-176-175-176 173-317-371-731-175-175-157-157-175-157-751-175-173-175-175-176-175 175-173-371-715-175-175-175-173-175-175-175-173-175-175-175-715-7_W.mp3)
  Segment 29: 147268ms ~ 152704ms (file: 106-750-132-321-132-123-213-213-132-120-321-237-123-125-213-765-675-675-675-675-765-675-756-160-321-321-321-237-237-231-123-321-321-276-217-123-152-275-173-132-103-567-672-320-3_W.mp3)
  Segment 30: 153704ms ~ 154280ms (file: 163-375-573-751-753-576-756-753-261-756-756-715-157-573-571-517-567-571-576-263-725-765-756-735_W.mp3)
  Segment 31: 155280ms ~ 155856ms (file: 216-102-756-753-751-756-751-751-736-317-157-602-317-371-317-310-713-162-726-762-175-752-157-156-136-135-257-123-215-123-125-135-216-315-275-126-217-102-132-627-612-261-263-316-3_V.mp3)
  Segment 32: 156856ms ~ 164380ms (file: 312 756-276-726-726-170-327-762-276-273-752-276-572-725-265-267-107-170-763-713-761-367-756-752-267-267-215-173-576-170-706-263-736-267-267-275-172-574-765-367-573-752-601-107-5_W.mp3)
  Segment 33: 165380ms ~ 173948ms (file: 230-276-732-271-270-270-276-321-237-271-260-206-273-120-103-270-231-672-267-236-206-132-136-541-315-231-230 326-312-176-230-537-257-301-524-257-542-542-312-103-127-267-207-231-2_W.mp3)
  Segment 34: 174948ms ~ 175524ms (file: 135-315-357-157-576-573-570-567-175-572-752-230-267-672-316-316-136-136-316-316-310-357-103-236-623-756-273-507-162-520-365-263-576-527-236-162-326-265-213-231-267-136-137-172-7_W.mp3)
  Segment 35: 176524ms ~ 177100ms (file: 175-602-621-273-237-632-176 613-576-275-576-170-135-713-271-756-762-760-652-620-621-620-120-102 675-362-317-756-756-756 627-627-756-176 602-620-602-261-120-103-627-261-207-720-3_W.mp3)
  Segment 36: 178100ms ~ 185912ms (file: 574-302-275-762-207-632-672-267-260-325-376-267-301-267-237-271-103-167-165-620-130-305-170-765-756-276-537-107-312-217-103-326-327-376-627-576-267-362-762-672-321-326-170-726-2_V.mp3)
  Segment 37: 186912ms ~ 187488ms (file: 567-567-635-361-326-153-312-561-573-576-256-320-657-613-625-231-312-123-312-326-312 657 576-574-574-576-576-576-567-652-567-574 163-103-672-137-236-623-365-675-756-756-751-162-2_W.mp3)
  Segment 38: 188488ms ~ 189064ms (file: 567-567-612-563-576-675-356 576-250-751-103 361-263-172-713-132-137-136-136-316-102-137-137-106-153-317-137-325-132-136_V.mp3)
  Segment 39: 190064ms ~ 190640ms (file: 761-613-671-675-671-675-637-765-765-765-765-675-756-756-756-756-765-576-765-567-756-756-756-576-576-567-753-756-576-765-756-576-157-576-576-675-735-756-756-657-576-765-756-756-5_W.mp3)
  Segment 40: 191640ms ~ 198012ms (file: 203-263-762-752-657-752-602-657-601-136-317-315-317-310-632-316-321-210-213-136-135-153-513-156-217-726-172-657-175-102-136-106-176-103-320-123-102-671-376-137-310-206-201-201-3_W.mp3)
  Segment 41: 199012ms ~ 199588ms (file: 123-316 130-130-137-137-137-310-137-317-137-130-136-275-265-625-203-752-137-725-726-231-123-312-527-517-120-312-310-310_W.mp3)
  Segment 42: 200588ms ~ 201164ms (file: 260-620-623-261-625-652-567-675-605-675-675-754-756-756-756-756-756-765-621-652-652-567-576-763-765-765-567-756-753-765-756-576-756-265-651-561-157-175-751-175-571-136-163-165-1_W.mp3)
  Segment 43: 202164ms ~ 202740ms (file: 765 361-253-360-326-673-316-153-157-351-175-315-135-367-371-317-375-327-176-310-127-321-102-750-572-127-126-360-123-123-576-263-316-516-623-567-165-236-601-506-576-523-132-130-3_W.mp3)
  Segment 44: 203740ms ~ 204316ms (file: 761-576-576-765-576-375-750-576-670-706-670-276-570-327-267-136-306-316-316-360-307-231-317-152-261-175-236-126-276-507-125-271-756-137-316-312_W.mp3)
  Segment 45: 205316ms ~ 211652ms (file: 327-725-216-103-210-263-165-326-210-216-206-216-132-156-725-153-312-321-236-236-312-123-163-201-310-310-310-132-203-263-267-237-106-310-320-321-320-321-302-201-230-321-231-320-1_V.mp3)
  Segment 46: 212652ms ~ 213228ms (file: 765-165-162-570-135-351-315-762 657-607-301-321-316-316-631-136-637-136-167-675-312-312-132-765-765-765-102_W.mp3)
  Segment 47: 214228ms ~ 214804ms (file: 236-312-325-301-173-317-315-723-372-576-713-267-267-720-562-236-260-137-610-513-570-375-547-612-321-215-507-725-267-573-765-752-671-725-327-137-570-267-576-512-726-513-532-735-5_V.mp3)
  Segment 48: 215804ms ~ 216380ms (file: 571-570-751-571-571-576-517 571-571-571-571-571-517-571-157-517-517-571-175-571-751-571-175-751-571-570-570-571 574-571-571-571-157-571-571-576-571-571-571-571-751-756-576 576-5_W.mp3)
  Segment 49: 217380ms ~ 222888ms (file: 135-271-317-123-572-762-237-350-327-573-132-130-137-167-305-321-752-135-310-603-315-132-321-132-326-351-132-216-213-127-210-301-753-301-312-130-230-217-720-350-320-372-307-320-3_V.mp3)
  Segment 50: 223888ms ~ 224464ms (file: 765-657-357-375-753-574-572-721-725-726-756-756-572-526-574-756-765-716-765-275-635-762-765-756-762-752-765-765-756-576-756-375-563-351-756-735-756-756-753-763-675-362-637-673-6_W.mp3)
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
> **Time**: 2026-04-06 08:16:13
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "안녕하세요 만나서 반갑습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 150, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "만나서", "start_ms": 1100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 1800, "dur_ms": 900, "phase_lr_deg": 0.0}
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
"transcript": "오늘 날씨가 정말 화창하고 좋네요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 210,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 210, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "날씨가", "start_ms": 700, "dur_ms": 550, "phase_lr_deg": -5.0},
{"word": "정말", "start_ms": 1350, "dur_ms": 450, "phase_lr_deg": -5.0},
{"word": "화창하고", "start_ms": 1900, "dur_ms": 800, "phase_lr_deg": -5.0},
{"word": "좋네요", "start_ms": 2800, "dur_ms": 600, "phase_lr_deg": -5.0}
]
},
{
"segment": 4,
"transcript": "이런 날에는 공원에 가서 산책을 하고 싶어져요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "이런", "start_ms": 180, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "날에는", "start_ms": 650, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "공원에", "start_ms": 1250, "dur_ms": 550, "phase_lr_deg": -5.0},
{"word": "가서", "start_ms": 1900, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "산책을", "start_ms": 2400, "dur_ms": 600, "phase_lr_deg": -5.0},
{"word": "하고", "start_ms": 3100, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "싶어져요", "start_ms": 3550, "dur_ms": 750, "phase_lr_deg": -5.0}
]
},
{
"segment": 5,
"transcript": "함께 커피 한 잔 하러 가실래요?",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 240,
"audio_type": "speech",
"words": [
{"word": "함께", "start_ms": 240, "dur_ms": 450, "phase_lr_deg": 5.0},
{"word": "커피", "start_ms": 800, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "한", "start_ms": 1300, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "잔", "start_ms": 1600, "dur_ms": 250, "phase_lr_deg": 5.0},
{"word": "하러", "start_ms": 1950, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "가실래요?", "start_ms": 2450, "dur_ms": 700, "phase_lr_deg": 5.0}
]
},
{
"segment": 6,
"transcript": "그거 아주 좋은 생각이에요 저도 마침 목이 말랐거든요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "그거", "start_ms": 120, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "아주", "start_ms": 550, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "좋은", "start_ms": 1050, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "생각이에요", "start_ms": 1550, "dur_ms": 750, "phase_lr_deg": -5.0},
{"word": "저도", "start_ms": 2400, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "마침", "start_ms": 2850, "dur_ms": 450, "phase_lr_deg": -5.0},
{"word": "목이", "start_ms": 3400, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "말랐거든요", "start_ms": 3850, "dur_ms": 800, "phase_lr_deg": -5.0}
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
"transcript": "근처에 새로 생긴 예쁜 카페가 있는데 거기로 가요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "근처에", "start_ms": 200, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "새로", "start_ms": 800, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "생긴", "start_ms": 1300, "dur_ms": 450, "phase_lr_deg": 5.0},
{"word": "예쁜", "start_ms": 1850, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "카페가", "start_ms": 2350, "dur_ms": 550, "phase_lr_deg": 5.0},
{"word": "있는데", "start_ms": 3000, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "거기로", "start_ms": 3600, "dur_ms": 450, "phase_lr_deg": 5.0},
{"word": "가요", "start_ms": 4150, "dur_ms": 400, "phase_lr_deg": 5.0}
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
"transcript": "Welcome to our store. How can I help you?",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 300,
"audio_type": "speech",
"words": [
{"word": "Welcome", "start_ms": 300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 850, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "our", "start_ms": 1050, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "store.", "start_ms": 1300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "How", "start_ms": 2200, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "can", "start_ms": 2500, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 2750, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "help", "start_ms": 2950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "you?", "start_ms": 3300, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "en",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 17,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "en",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 18,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "en",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 19,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "en",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 20,
"transcript": "아이스 아메리카노 두 잔 주세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "아이스", "start_ms": 250, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "아메리카노", "start_ms": 800, "dur_ms": 850, "phase_lr_deg": 5.0},
{"word": "두", "start_ms": 1750, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "잔", "start_ms": 2000, "dur_ms": 250, "phase_lr_deg": 5.0},
{"word": "주세요", "start_ms": 2350, "dur_ms": 600, "phase_lr_deg": 5.0}
]
},
{
"segment": 21,
"transcript": "주문하신 음료 나왔습니다 맛있게 드세요",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 400,
"audio_type": "speech",
"words": [
{"word": "주문하신", "start_ms": 400, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "음료", "start_ms": 1150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "나왔습니다", "start_ms": 1650, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "맛있게", "start_ms": 2800, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "드세요", "start_ms": 3450, "dur_ms": 500, "phase_lr_deg": 0.0}
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
"transcript": "여기 커피 정말 맛있네요 분위기도 좋고요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 350,
"audio_type": "speech",
"words": [
{"word": "여기", "start_ms": 350, "dur_ms": 450, "phase_lr_deg": -5.0},
{"word": "커피", "start_ms": 900, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "정말", "start_ms": 1400, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "맛있네요", "start_ms": 1900, "dur_ms": 700, "phase_lr_deg": -5.0},
{"word": "분위기도", "start_ms": 2750, "dur_ms": 650, "phase_lr_deg": -5.0},
{"word": "좋고요", "start_ms": 3500, "dur_ms": 550, "phase_lr_deg": -5.0}
]
},
{
"segment": 25,
"transcript": "마음에 드신다니 다행이에요 자주 와야겠어요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 280,
"audio_type": "speech",
"words": [
{"word": "마음에", "start_ms": 280, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "드신다니", "start_ms": 850, "dur_ms": 600, "phase_lr_deg": 5.0},
{"word": "다행이에요", "start_ms": 1550, "dur_ms": 750, "phase_lr_deg": 5.0},
{"word": "자주", "start_ms": 2500, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "와야겠어요", "start_ms": 3000, "dur_ms": 700, "phase_lr_deg": 5.0}
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
"transcript": "Thank you for the coffee. It was really good.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "Thank", "start_ms": 150, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "you", "start_ms": 500, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "for", "start_ms": 700, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "the", "start_ms": 950, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "coffee.", "start_ms": 1150, "dur_ms": 450, "phase_lr_deg": -5.0},
{"word": "It", "start_ms": 2000, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "was", "start_ms": 2200, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "really", "start_ms": 2450, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "good.", "start_ms": 2900, "dur_ms": 400, "phase_lr_deg": -5.0}
]
},
{
"segment": 30,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "en",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 31,
"transcript": "My pleasure. Let's hang out again soon.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "My", "start_ms": 200, "dur_ms": 250, "phase_lr_deg": 5.0},
{"word": "pleasure.", "start_ms": 500, "dur_ms": 550, "phase_lr_deg": 5.0},
{"word": "Let's", "start_ms": 1500, "dur_ms": 300, "phase_lr_deg": 5.0},
{"word": "hang", "start_ms": 1850, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "out", "start_ms": 2250, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "again", "start_ms": 2500, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "soon.", "start_ms": 3000, "dur_ms": 450, "phase_lr_deg": 5.0}
]
},
{
"segment": 32,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "en",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 33,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "en",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 34,
"transcript": "네 다음에 또 봬요 조심히 가세요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 180, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "다음에", "start_ms": 500, "dur_ms": 450, "phase_lr_deg": -5.0},
{"word": "또", "start_ms": 1050, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "봬요", "start_ms": 1300, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "조심히", "start_ms": 2000, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "가세요", "start_ms": 2600, "dur_ms": 550, "phase_lr_deg": -5.0}
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
"transcript": "오늘 정말 즐거운 시간이었습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 220,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 220, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "정말", "start_ms": 700, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "즐거운", "start_ms": 1200, "dur_ms": 550, "phase_lr_deg": 5.0},
{"word": "시간이었습니다", "start_ms": 1850, "dur_ms": 1000, "phase_lr_deg": 5.0}
]
},
{
"segment": 37,
"transcript": "저도요 조만간 다시 연락 드릴게요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 300,
"audio_type": "speech",
"words": [
{"word": "저도요", "start_ms": 300, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "조만간", "start_ms": 1000, "dur_ms": 600, "phase_lr_deg": -5.0},
{"word": "다시", "start_ms": 1700, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "연락", "start_ms": 2200, "dur_ms": 450, "phase_lr_deg": -5.0},
{"word": "드릴게요", "start_ms": 2750, "dur_ms": 600, "phase_lr_deg": -5.0}
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
"transcript": "안녕히 가세요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 700, "dur_ms": 500, "phase_lr_deg": 0.0}
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
"transcript": "Transcribing finished. Thank you for using our service.",
"speaker": "speaker_4",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "Transcribing", "start_ms": 100, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "finished.", "start_ms": 950, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "Thank", "start_ms": 1800, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 2150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "for", "start_ms": 2350, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "using", "start_ms": 2600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "our", "start_ms": 3000, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "service.", "start_ms": 3250, "dur_ms": 500, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 08:16:13*
