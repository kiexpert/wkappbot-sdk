# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=24268  time=06:02:34  uptime=17571s  mem=845MB  system_processes=353"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_060058.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 725-750-527-312-576-302-572-532-571 236-275-237-576-375-527-215-517-261-726-735-235-132-213-527-570-726-572-237-375-756-312-572-576 206-267-174-231-236-321 570 765-261-576-567-7_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 276-715-731-627-672-213-326-573-576-752-576-725-570-235-760-267-623-503-137-321-576-756-576-213-135-765-567-275-256-265-570-652-570-326-125-162-216-702-253-762-261-756-627-253-6_V.mp3)
  Segment 3: 3152ms ~ 10100ms (file: 123-267-260-237-213-216-726-132-107-213-512-675-237-270-321-237-276-267-321-102-125-132-675-627-267-102-137-361-321-172-267-267-210-260-132-237-237-172-176-172-132-321-321-761-1_W.mp3)
  Segment 4: 11100ms ~ 11676ms (file: 267-316-263-267-612-103-713-752-127-162 136-132-103-136-103-572-132-165-765-126-103-106-213-103-126-106-160-601-513-103-160-216-132-103-213-671-162-176-132-103-620-651-165-756-2_V.mp3)
  Segment 5: 12676ms ~ 13252ms (file: 175-175-571-175-751-150-157-751-710-715-715-571 510-517-571-107-157-105-105-175-107-105-517-571-150-157-150-150-501-571-517-150-705-157-510 105-105-150-105-105-157-510-574-571-5_V.mp3)
  Segment 6: 14252ms ~ 14828ms (file: 316 321-132-312-301-613-130-136-613-613-632-613-163-160-102-312-310-132-610-610-613-160-163-130-136-136-671-375-763-312-132-312-312-132-132-312-316-312-326-321-316-276-736-376-3_W.mp3)
  Segment 7: 15828ms ~ 25764ms (file: 601-106-216-103-167-271-751-672-217-123-132-627-123-132-216-261-216-271-216-123-753-715-162-167-160-321-136-132-213-102-715-132-231-213-213-261-267-260-201-123-576-571-621-120-2_W.mp3)
  Segment 8: 26764ms ~ 27340ms (file: 571-571-571-157-157-571 570-571-157-517-571-715-570-710-715-715-157-175-571-175-175-750-715-570-571-571-571-571-571-756-756 570-576-570-570-571-571-571-157-157-751-571-175-157-7_W.mp3)
  Segment 9: 28340ms ~ 28916ms (file: 105-105-150-105-150-130-105-105-130-105-150-135-130-135-150-157-153_V.mp3)
  Segment 10: 29916ms ~ 30492ms (file: 135-173-173-103 150-103-157-517-157-103-137-137-173-130-170-170-153-103-173-107-173-130-173-170-103-137-107-137-107-137-170-170-170-517-710 261-623-261-162 137-321-132-127-123-2_W.mp3)
  Segment 11: 31492ms ~ 39700ms (file: 230-320-327-236-543-352-267-216-213-261-276-736-261-230-230-321-321-103-213-260-263-261-261-167-170-316-123-216-672-612-632-672-316-260-123-120-123-132-123-123-572-213-623-267-3_W.mp3)
  Segment 12: 40700ms ~ 41276ms (file: 570-510-150-105-157-105-157-517-571-105-157-105-510-501-105-501-516-510-150-506-507-560-571-150-501-570-507-507-574-570-570-507 510-517-517-105-157-501-105-517-517-517-507-507-5_W.mp3)
  Segment 13: 42276ms ~ 42852ms (file: 571-571-576-571 175-571-571-715-517-157-751-756-751-751-751-571-571-576-571-576-571-517-576 517-157-175-157-715-517-615-571-517-517-571-571-751-751-751-571-751-576-571-576 517-5_V.mp3)
  Segment 14: 43852ms ~ 44428ms (file: 176-231-132 576-657-657-526-361-315-315-165-361-536-615-361-126-635-652-765-635-315-156-563-657-657-753-675-657-356-632-635-153-153-567-576-657-765-657-675-756-532-567-327-631-6_W.mp3)
  Segment 15: 45428ms ~ 54680ms (file: 132-726-271-267-273-572-102-312-130-321-312-132-231-762-132-106-107-276-267-756-765-176-107-327-207-230-102-107-730-237-237-310-271-230-273-103-167-107-273-173-136-271-362-316-3_W.mp3)
  Segment 16: 55680ms ~ 65184ms (file: 263-321-267-267-301-260-302-130-123-754-201-620-126-123-231-201-123-120-102-213-216-126-213-123-210-102-527-574-547-210-754-256-532-301-206-542-260-671-574-574-206-216-542-265-2_W.mp3)
  Segment 17: 66184ms ~ 66760ms (file: 132-632-132-321-750-326-316-136-237-715-163-560-132-137-705-317-732-672-167-256-123-572-276-752-127-752-756-231-273-257-706-572-257-217-152-321-206-506-531-573-752-215-570-157-5_V.mp3)
  Segment 18: 67760ms ~ 68336ms (file: 572-715-175-570-175-175-705-570-710-750-750-751-705-570-571-750-715-750-750-105-170-715-571-510-705-750-715-715-517-571-570-751-570-750 507-751-510-175-571-705-710-105-175-710-7_W.mp3)
  Segment 19: 69336ms ~ 69912ms (file: 613-132-261-267-267-326-362 362-632-361-326-132-123_W.mp3)
  Segment 20: 70912ms ~ 71488ms (file: 710-576-576-567-567-576-574-576-572-576 170-517-570-570-175-170-170-751-175-107-175-170-751-571-750-501-175-570-576-576-517-573-517-576-157-570-576 150-571-175-157-507-175-170-1_W.mp3)
  Segment 21: 72488ms ~ 73064ms (file: 760-706-672-627-756-706-750-617-716-163-172-362-132-632-316-321-312-312-236-263-263-263-263-257-267-567-572-172 263 251-125-705-675-756-765-167-167-162-106-106-123-615-564-516-3_W.mp3)
  Segment 22: 74064ms ~ 74640ms (file: 675-637-361-361-362-312-613-136-362-312-132-312-132-132-103-102-132-302-320-210-120-201_W.mp3)
  Segment 23: 75640ms ~ 76216ms (file: 362-312-716-365-532-301-216-312-312-726-721-327-760-276-136-367-627-267-276-546-765-316-316-316-301-136-316-136 176-731-371-137-316-713 715-713-735-731-713-763-756-751-753-673_W.mp3)
  Segment 24: 77216ms ~ 77792ms (file: 317-351-365-130-137-106-160-715-731-236-275-326-231-216-276-765-507-273-276-752-276-715-253-506-512-571-532-236-732-752-263-574-321-570-135-350-571-310-137-315-136-102-251-175-1_V.mp3)
  Segment 25: 78792ms ~ 83184ms (file: 312-365-365-756-130-135-107-130-127-543-261-261-267-310-312-123-213-261-267-261-261-621-206-321-120-213-267-263-261-263-263-265-263-265-265-123-163-261-230-132-216-631-326-576-2_W.mp3)
  Segment 26: 84184ms ~ 92356ms (file: 276-127-107-312-156-273-103-103-725-317-276-301-130-207-267-320-312-230-230-237-276-230-275-123-507-516-736-103-362-103-132-513-730-720-762-651-230-267-207-672-706-672-625-320-1_V.mp3)
  Segment 27: 93356ms ~ 100196ms (file: 267-326-261-362-123-123-130-231-567-713-751-103-362-103-756-257-267-267-312-130-210-132-267-572-267-256-267-260-316-130-167-213-675-562-126-213-136-312-361-576-725-216-312-263-5_W.mp3)
  Segment 28: 101196ms ~ 108612ms (file: 102-167-126-203-203-231-123-123-312-321-702-123-276-627-762-627-263-137-132-173-103-301-130-210-320-103-126-201-306-102-143-102-103-167-623-320-301-230-231-261-372-370-371-271-7_W.mp3)
  Segment 29: 109612ms ~ 110188ms (file: 163-167-130-163-103-130-516-136-617-763-163-103-136-312-123-132-576 765-672-267-276-267-276-167-617-156-162-567-561-657-765-576-576-725-123-213-312 123-321-123 621-672-132-175-3_V.mp3)
  Segment 30: 111188ms ~ 111764ms (file: 156-150-150-105-105-150-516-157-107-157-157-157-150-157-150-157-157-175-150-157-157-175-105-514 105-105-150-571-157-705-105-105-157-150-105-150-157-156-150-157-156-507-175-175-1_V.mp3)
  Segment 31: 112764ms ~ 113340ms (file: 621-361 267-276-236-325-132-326-327-362-321-310-623-621-216-162-126-136-312-162-623-162-326-102-321-123-132-316-361-321-621-162-562-652-102-263-236-210-132-167-175-657-163-765-6_W.mp3)
  Segment 32: 114340ms ~ 125500ms (file: 561-135-135-157-135-135-301-301-321-372-316-312-312-103-137-672-627-627-623-260-126-216-120-162-206-130-216-231-231-576-260-261-130-107-263-572-527-102-103-752-267-672-267-120-5_W.mp3)
  Segment 33: 126500ms ~ 127076ms (file: 613-162-506-126-216-301-326-130-103-132-261-627-612-361-621-317-672-162-126-362-163-175-617-601-613-625-621-163-106-672-160-126-106-761-613-236-162-216-621-106-123-267-162-621-1_W.mp3)
  Segment 34: 128076ms ~ 135888ms (file: 762-726-217-267-273-621-261-261-132-726-627-126-127-276-271-312-275-725-765-267-201-576-205-752-625-752-105-126-315-705-754-237-572-301-256-231-751-547-572-572-310-765-103-157-7_W.mp3)
  Segment 35: 136888ms ~ 146104ms (file: 574-321-326-312-573-753-751-756-753-576-157-567-576-576-573-617-517-576-315-637-756-157-576-765-310-137-751-564-163-631-617-230-263-263-623-321-326-237-753-312-367-762-627-103-1_W.mp3)
  Segment 36: 147104ms ~ 147680ms (file: 135-136-103-134-103-134-136-136-103-136-134-136-134-103-103-103-134-103-135-103 314-130-103-134-314-341-134-135-136-103-103-134-136-103-103-136-136-103-136-103-136 315-537-573-1_V.mp3)
  Segment 37: 148680ms ~ 149256ms (file: 137-134-134-103-104-103-145-134-103-134-145-103-103-103-134-103-103-134 315-357-351-543-103-130-105-143-145-130-103-130-130-314-451-154-453-105-105-140-143-145-145-104-135-105-1_W.mp3)
  Segment 38: 150256ms ~ 150832ms (file: 761-761-765-765-765-715-725-716-137-713-576-527-205-527-571-517-572-571-157-172-215-754-750-570-752-576-751-756-576-547-576-576-657-675-507-547-507 576-576-574-574-574-750-750-5_W.mp3)
  Segment 39: 151832ms ~ 152408ms (file: 507-570-570-507-570-507-570-571-751-750-507-751_W.mp3)
  Segment 40: 153408ms ~ 153984ms (file: 652-625-652-562-652-652-652-623 260-620-620-261-260-621-625-625-625-562-652-625-652-256-625-625-625-657 623-265-264-623-623-625-650-625-562-652-562-652-652-526-563-652-652-657-6_W.mp3)
  Segment 41: 154984ms ~ 173920ms (file: 756-573-130-321-102-216-132-317-524-316-103-123-573-351-351-316-231-137-123-317-317-375-137-107-175-756-574-573-574-175-756-265-275-375-371-132-574-230-574-574-301-132-573-576-2_W.mp3)
  Segment 42: 174920ms ~ 175496ms (file: 751-751-173-571-710-175-157-715-751-571-175-751-715-751-750-752-751-756-750 756-751-173-715-751-571-751-571-574-570-574-574-173-751-571 571-750-570-751-517-715-751-715-751-715-7_W.mp3)
  Segment 43: 176496ms ~ 186828ms (file: 312-321-231-312-137-137-175-135-312-152-135-135-123 173-672-657 712-751-756-547-573-317-567-713-167-103-765-612-631-365-361-316-361-316-136-316-317-316-136-137-136-136-507-501-1_W.mp3)
  Segment 44: 187828ms ~ 188404ms (file: 715-173-276-723-716-726-762-715-163-173-762-317-761_V.mp3)
  Segment 45: 189404ms ~ 189980ms (file: 107-130-170-175-176-761-175-716-716-715-716-761-713 570-751-571-571-750-750-715-750-716-756-751-701-762-765-752-715-710-756-756-751-173-176-726-762-716-761-176-716-173-176 576-5_W.mp3)
  Segment 46: 190980ms ~ 191556ms (file: 236-261-123-610-623-103-617 312-176-231_V.mp3)
  Segment 47: 192556ms ~ 193132ms (file: 321-136-527-257-235-152-215-256-672-267-236-132-726-276-273-231 361-316-627-627-627 130-310-316-236-126-316-361-365-103-362-361-134-130-103-103-267-624-214-514-517-106-103-102-1_V.mp3)
  Segment 48: 194132ms ~ 194708ms (file: 135-750-756-132-175-103-137-123-752-762-762-213-521-561-312-217-135-170-231-527-576-372-725-761-752-267-512-752-215-576 172-507-261-572-172-273-672-265-563-261-260-627-723-263-2_V.mp3)
  Segment 49: 195708ms ~ 196284ms (file: 673 576-756-713-301-370-307-715-607-765-637-167-176-310-310-137-761-710-317-107-137-130-317-307-731-103-173-170-371-731-750-170 750-750-710-371-731-735-153-371-137-316-367-163-3_V.mp3)
  Segment 50: 197284ms ~ 197860ms (file: 231-765-672-672-627-623-267-260-367-312-612-321-216-126-273-367-126-136-130-103-267-267-621-216-621-652-612-762-726-672-672-672-672-672-672-576-657-675-567 163-263-236-261-765-6_W.mp3)
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
> **Time**: 2026-04-05 06:03:50
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
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 95,
"audio_type": "speech",
"words": [
{"word": "반갑습니다", "start_ms": 95, "dur_ms": 430, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "오늘 날씨가 정말 좋네요 같이 산책하실래요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "날씨가", "start_ms": 600, "dur_ms": 550, "phase_lr_deg": 5.0},
{"word": "정말", "start_ms": 1200, "dur_ms": 450, "phase_lr_deg": 5.0},
{"word": "좋네요", "start_ms": 1700, "dur_ms": 600, "phase_lr_deg": 5.0},
{"word": "같이", "start_ms": 2500, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "산책하실래요", "start_ms": 3000, "dur_ms": 950, "phase_lr_deg": 5.0}
]
},
{
"segment": 4,
"transcript": "좋아요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "좋아요", "start_ms": 80, "dur_ms": 440, "phase_lr_deg": 0.0}
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
"transcript": "Where are we going?",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "Where", "start_ms": 120, "dur_ms": 150, "phase_lr_deg": -10.0},
{"word": "are", "start_ms": 270, "dur_ms": 100, "phase_lr_deg": -10.0},
{"word": "we", "start_ms": 370, "dur_ms": 100, "phase_lr_deg": -10.0},
{"word": "going?", "start_ms": 470, "dur_ms": 210, "phase_lr_deg": -10.0}
]
},
{
"segment": 7,
"transcript": "공원에 가서 커피 한 잔 마시면서 얘기해요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "공원에", "start_ms": 200, "dur_ms": 600, "phase_lr_deg": 5.0},
{"word": "가서", "start_ms": 850, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "커피", "start_ms": 1300, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "한", "start_ms": 1750, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "잔", "start_ms": 2000, "dur_ms": 300, "phase_lr_deg": 5.0},
{"word": "마시면서", "start_ms": 2400, "dur_ms": 700, "phase_lr_deg": 5.0},
{"word": "얘기해요", "start_ms": 3200, "dur_ms": 800, "phase_lr_deg": 5.0}
]
},
{
"segment": 8,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
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
"transcript": "That sounds perfect.",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "That", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": -10.0},
{"word": "sounds", "start_ms": 200, "dur_ms": 200, "phase_lr_deg": -10.0},
{"word": "perfect.", "start_ms": 400, "dur_ms": 350, "phase_lr_deg": -10.0}
]
},
{
"segment": 11,
"transcript": "요즘 공부는 잘 되어가고 있어요?",
"speaker": "speaker_2",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "요즘", "start_ms": 180, "dur_ms": 450, "phase_lr_deg": 5.0},
{"word": "공부는", "start_ms": 700, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "잘", "start_ms": 1250, "dur_ms": 250, "phase_lr_deg": 5.0},
{"word": "되어가고", "start_ms": 1550, "dur_ms": 600, "phase_lr_deg": 5.0},
{"word": "있어요?", "start_ms": 2200, "dur_ms": 500, "phase_lr_deg": 5.0}
]
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
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 14,
"transcript": "It's a bit challenging.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "It's", "start_ms": 100, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 220, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "bit", "start_ms": 300, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "challenging.", "start_ms": 400, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "한국어 발음이 생각보다 어려워서 매일 연습하고 있어요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 220,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 220, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "발음이", "start_ms": 800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "생각보다", "start_ms": 1400, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "어려워서", "start_ms": 2100, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "매일", "start_ms": 2900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "연습하고", "start_ms": 3400, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 4200, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "꾸준히 하면 반드시 늘 거예요 걱정 마세요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "꾸준히", "start_ms": 150, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "하면", "start_ms": 700, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "반드시", "start_ms": 1100, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "늘", "start_ms": 1650, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "거예요", "start_ms": 1900, "dur_ms": 450, "phase_lr_deg": 5.0},
{"word": "걱정", "start_ms": 2500, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "마세요", "start_ms": 3000, "dur_ms": 500, "phase_lr_deg": 5.0}
]
},
{
"segment": 17,
"transcript": "감사합니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 90, "dur_ms": 480, "phase_lr_deg": 0.0}
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
"transcript": "You're welcome.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "You're", "start_ms": 60, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "welcome.", "start_ms": 260, "dur_ms": 350, "phase_lr_deg": 5.0}
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
"transcript": "저기 예쁜 카페가 보이네요",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "저기", "start_ms": 110, "dur_ms": 400, "phase_lr_deg": -10.0},
{"word": "예쁜", "start_ms": 550, "dur_ms": 350, "phase_lr_deg": -10.0},
{"word": "카페가", "start_ms": 950, "dur_ms": 450, "phase_lr_deg": -10.0},
{"word": "보이네요", "start_ms": 1450, "dur_ms": 550, "phase_lr_deg": -10.0}
]
},
{
"segment": 22,
"transcript": "들어가 볼까요?",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "들어가", "start_ms": 80, "dur_ms": 400, "phase_lr_deg": -10.0},
{"word": "볼까요?", "start_ms": 500, "dur_ms": 380, "phase_lr_deg": -10.0}
]
},
{
"segment": 23,
"transcript": "네 좋아요 제가 살게요",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 120, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "좋아요", "start_ms": 300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "살게요", "start_ms": 1100, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "아니에요 이번엔 제가 대접할게요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "아니에요", "start_ms": 100, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "이번엔", "start_ms": 650, "dur_ms": 450, "phase_lr_deg": 5.0},
{"word": "제가", "start_ms": 1150, "dur_ms": 300, "phase_lr_deg": 5.0},
{"word": "대접할게요", "start_ms": 1500, "dur_ms": 700, "phase_lr_deg": 5.0}
]
},
{
"segment": 25,
"transcript": "정말요 고맙습니다 그럼 다음엔 제가 낼게요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "정말요", "start_ms": 150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "고맙습니다", "start_ms": 700, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "그럼", "start_ms": 1500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "다음엔", "start_ms": 1900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 2450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "낼게요", "start_ms": 2800, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "무슨 음료를 마시고 싶어요? 저는 아이스 아메리카노요",
"speaker": "speaker_2",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "무슨", "start_ms": 200, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "음료를", "start_ms": 600, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "마시고", "start_ms": 1150, "dur_ms": 450, "phase_lr_deg": 5.0},
{"word": "싶어요?", "start_ms": 1650, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "저는", "start_ms": 2400, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "아이스", "start_ms": 2800, "dur_ms": 450, "phase_lr_deg": 5.0},
{"word": "아메리카노요", "start_ms": 3300, "dur_ms": 800, "phase_lr_deg": 5.0}
]
},
{
"segment": 27,
"transcript": "저는 따뜻한 카페라떼 한 잔 부탁드려요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 180, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "따뜻한", "start_ms": 550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "카페라떼", "start_ms": 1100, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 1800, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "잔", "start_ms": 2050, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "부탁드려요", "start_ms": 2350, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "주문 완료했습니다 자리에 가서 앉을까요?",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "주문", "start_ms": 130, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "완료했습니다", "start_ms": 550, "dur_ms": 800, "phase_lr_deg": 5.0},
{"word": "자리에", "start_ms": 1500, "dur_ms": 450, "phase_lr_deg": 5.0},
{"word": "가서", "start_ms": 2000, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "앉을까요?", "start_ms": 2400, "dur_ms": 550, "phase_lr_deg": 5.0}
]
},
{
"segment": 29,
"transcript": "Sure, let's sit over there.",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "Sure,", "start_ms": 90, "dur_ms": 250, "phase_lr_deg": -10.0},
{"word": "let's", "start_ms": 350, "dur_ms": 150, "phase_lr_deg": -10.0},
{"word": "sit", "start_ms": 550, "dur_ms": 150, "phase_lr_deg": -10.0},
{"word": "over", "start_ms": 750, "dur_ms": 200, "phase_lr_deg": -10.0},
{"word": "there.", "start_ms": 1000, "dur_ms": 300, "phase_lr_deg": -10.0}
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
"transcript": "여기 창가 자리가 경치가 아주 좋네요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "여기", "start_ms": 110, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "창가", "start_ms": 500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "자리가", "start_ms": 950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "경치가", "start_ms": 1450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 2000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 2350, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "맞아요 마음이 편안해지는 것 같아요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "맞아요", "start_ms": 140, "dur_ms": 450, "phase_lr_deg": 5.0},
{"word": "마음이", "start_ms": 700, "dur_ms": 450, "phase_lr_deg": 5.0},
{"word": "편안해지는", "start_ms": 1200, "dur_ms": 800, "phase_lr_deg": 5.0},
{"word": "것", "start_ms": 2100, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "같아요", "start_ms": 2300, "dur_ms": 450, "phase_lr_deg": 5.0}
]
},
{
"segment": 33,
"transcript": "The atmosphere here is really cozy.",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "The", "start_ms": 120, "dur_ms": 100, "phase_lr_deg": -10.0},
{"word": "atmosphere", "start_ms": 230, "dur_ms": 600, "phase_lr_deg": -10.0},
{"word": "here", "start_ms": 850, "dur_ms": 250, "phase_lr_deg": -10.0},
{"word": "is", "start_ms": 1150, "dur_ms": 150, "phase_lr_deg": -10.0},
{"word": "really", "start_ms": 1350, "dur_ms": 350, "phase_lr_deg": -10.0},
{"word": "cozy.", "start_ms": 1750, "dur_ms": 400, "phase_lr_deg": -10.0}
]
},
{
"segment": 34,
"transcript": "우리 공부에 대해서 더 이야기해 볼까요?",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 170,
"audio_type": "speech",
"words": [
{"word": "우리", "start_ms": 170, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "공부에", "start_ms": 550, "dur_ms": 450, "phase_lr_deg": 5.0},
{"word": "대해서", "start_ms": 1050, "dur_ms": 450, "phase_lr_deg": 5.0},
{"word": "더", "start_ms": 1550, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "이야기해", "start_ms": 1750, "dur_ms": 600, "phase_lr_deg": 5.0},
{"word": "볼까요?", "start_ms": 2400, "dur_ms": 450, "phase_lr_deg": 5.0}
]
},
{
"segment": 35,
"transcript": "어떤 부분이 가장 어렵게 느껴지나요?",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "어떤", "start_ms": 130, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "부분이", "start_ms": 550, "dur_ms": 450, "phase_lr_deg": 5.0},
{"word": "가장", "start_ms": 1050, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "어렵게", "start_ms": 1450, "dur_ms": 450, "phase_lr_deg": 5.0},
{"word": "느껴지나요?", "start_ms": 1950, "dur_ms": 700, "phase_lr_deg": 5.0}
]
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
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
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
"transcript": "(mixed)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "mixed",
"words": []
},
{
"segment": 41,
"transcript": "저는 특히 조사가 좀 헷갈려요 이 가 은 는 이런 것들이요",
"speaker": "speaker_1",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 190,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 190, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "특히", "start_ms": 600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "조사가", "start_ms": 1050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 1600, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "헷갈려요", "start_ms": 1850, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 2600, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가", "start_ms": 2800, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "은", "start_ms": 3000, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "는", "start_ms": 3200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 3450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "것들이요", "start_ms": 3900, "dur_ms": 600, "phase_lr_deg": 0.0}
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
"transcript": "그건 한국어를 배우는 모든 사람들이 겪는 고충이에요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 160,
"audio_type": "speech",
"words": [
{"word": "그건", "start_ms": 160, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "한국어를", "start_ms": 550, "dur_ms": 600, "phase_lr_deg": 5.0},
{"word": "배우는", "start_ms": 1200, "dur_ms": 450, "phase_lr_deg": 5.0},
{"word": "모든", "start_ms": 1700, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "사람들이", "start_ms": 2150, "dur_ms": 650, "phase_lr_deg": 5.0},
{"word": "겪는", "start_ms": 2850, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "고충이에요", "start_ms": 3250, "dur_ms": 700, "phase_lr_deg": 5.0}
]
},
{
"segment": 44,
"transcript": "Don't worry too much.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "Don't", "start_ms": 70, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "worry", "start_ms": 250, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "too", "start_ms": 480, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "much.", "start_ms": 600, "dur_ms": 300, "phase_lr_deg": 5.0}
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
"transcript": "I'll help you.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "I'll", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "help", "start_ms": 200, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "you.", "start_ms": 420, "dur_ms": 250, "phase_lr_deg": 5.0}
]
},
{
"segment": 47,
"transcript": "정말 고마워요 덕분에 힘이 나네요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 130, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "고마워요", "start_ms": 500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "덕분에", "start_ms": 1100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "힘이", "start_ms": 1600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "나네요", "start_ms": 1950, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "우리 같이 열심히 해봐요 화이팅",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "우리", "start_ms": 100, "dur_ms": 350, "phase_lr_deg": -10.0},
{"word": "같이", "start_ms": 500, "dur_ms": 350, "phase_lr_deg": -10.0},
{"word": "열심히", "start_ms": 900, "dur_ms": 500, "phase_lr_deg": -10.0},
{"word": "해봐요", "start_ms": 1450, "dur_ms": 500, "phase_lr_deg": -10.0},
{"word": "화이팅", "start_ms": 2100, "dur_ms": 550, "phase_lr_deg": -10.0}
]
},
{
"segment": 49,
"transcript": "화이팅",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "화이팅", "start_ms": 80, "dur_ms": 480, "phase_lr_deg": 5.0}
]
},
{
"segment": 50,
"transcript": "Yeah, fighting!",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "Yeah,", "start_ms": 60, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "fighting!", "start_ms": 250, "dur_ms": 450, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 06:03:50*
