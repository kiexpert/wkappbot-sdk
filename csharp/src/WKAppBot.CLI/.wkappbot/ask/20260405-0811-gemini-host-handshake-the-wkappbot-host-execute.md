# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=30524  time=08:09:35  uptime=13087s  mem=370MB  system_processes=353"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_080850.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 135-301-351-301-317-135-315-157-137-103-157-135-301-135-301 137-315-312-135-137-315-135-153-135-317-153-132-312-317-315-103-137-315-317-301 321-137-315-312-132-301-135-315-310-1_V.mp3)
  Segment 2: 1576ms ~ 9388ms (file: 162-137-167-120-236-132-276-726-103-316-310-132-710-132-312-310-175-162-216-276-317-237-103-132-127-715-230-132-635-362-316-236-312-132-176-750-572-275-203-103-752-257-302-327-2_V.mp3)
  Segment 3: 10388ms ~ 10964ms (file: 105-160-102-263-216-216-231-231-253-321-123-125-125-123-125-231-512-571-513-516-561-512-576-564-574-576-576-157-157-157-517-576-517-571-571-517-510-507-562-537-357-537-521-376-6_W.mp3)
  Segment 4: 11964ms ~ 12540ms (file: 716-761-763-756-357-762-735-567-725-672-712-761-173-763-726-731-753-627-756-715-672-762-763-175-157-157-516-152-165-165-562-156-152-156-756-765-756-765-756-751-765-715-756-756-7_W.mp3)
  Segment 5: 13540ms ~ 19012ms (file: 273-213-127-327-132-371-321-132-762-231-162-312-273-236-123-236-326-312-237-321-231-132-127-312-321-123-321-137-251-271-312-237-312-123-216-231-371-321-312-325-132-137-310-103-3_W.mp3)
  Segment 6: 20012ms ~ 32396ms (file: 135-132-267-756-670-213-123-170-761-106-627-261-367-132-213-236-326-216-623-106-103-321-167-126-136-126-216-326-231-320-132-176-172-172-260-315-123-126-120-102-130-136-620-267-6_W.mp3)
  Segment 7: 33396ms ~ 33972ms (file: 753-715-713-753-753-715-710-175-173-753-137-751-175-137-157-137-175-173-175-170-713-751 756-751-576-576-517-751-321-137-371-137-571-576-713-137-137-761-736-713-576-130-103-751-5_W.mp3)
  Segment 8: 34972ms ~ 35548ms (file: 103-273-275-123-321-123-125-756-756-230-231-325-213-125-137-132-321-213-123-102-132-105-137-570-321-137-103-321-123-136-672-123-163-312-672-675-123-132-132-132-716-761-761-712-1_V.mp3)
  Segment 9: 36548ms ~ 37124ms (file: 136-106-106-160-316-756-576-756-103-561-567-567-576-567-560-651-563-365-756-765-765-675-765-756-576-561-563-137-163-136-136-163-136-756-765-761-763-765-726-765-763-762-673-765-7_W.mp3)
  Segment 10: 38124ms ~ 42588ms (file: 627-627-627-267-627-627-267-126-672-162-621-261-263-726-762-627-267-261-621-261-216-261-176-621-216-610-267-267-623-627-627-261-612-721-672-162-160-162-672-621-726-276-726-672-6_W.mp3)
  Segment 11: 43588ms ~ 44164ms (file: 130-301-317-130-301-317-130-173-301-310-312-137-317-103-103-130-135-103-173-307 310-310-310-153-137-317-137-137-310-317-137-173-317-137-137-173-317-123-310-137-130-103-172-372-3_V.mp3)
  Segment 12: 45164ms ~ 45740ms (file: 547-574-574-571-750-507-751-705-175-157-517-157-157-756-705-750-710-715-157-157-170 571-571-571-517-751-570-570-576-576-517-507-751-517-571-107-507-517-571-750-571-507-570 170-5_V.mp3)
  Segment 13: 46740ms ~ 47316ms (file: 517-751-751-571-750-570-571-570-570-507-571-570-517-571-501-571-157-751-751-753-756-751-675-751-751-576 510-571-571-571-571-571-571-571-517-751-750-571-751-751-175-751 510-571-5_W.mp3)
  Segment 14: 48316ms ~ 48892ms (file: 715-705-750-715-750-751-750-751-715-751-750-751-570-751-175-570 501-510-150-150-517-571-157-750-751-175-750-751-705-750-751-715-750-705-756-750-750-756-705-750 157-570-570-157-1_V.mp3)
  Segment 15: 49892ms ~ 50468ms (file: 571-570-571-576-573-571-576-571-576-576-571-571-570-571-517-531-517-517-753-561-517-576-576-571-570-517-517-157-571-517-571-517 150-157-756-571-751-570-501-571-576-571-751-571-5_V.mp3)
  Segment 16: 51468ms ~ 52044ms (file: 627-312 237-173-576-750-267-572-325-312-267-273-327-215 163-367-765-731-567-153-156-136-106-167-316-316-371-130-316-137-176-137-163-173-163-367-627-623-162-216-216-362-316_W.mp3)
  Segment 17: 53044ms ~ 56536ms (file: 157-175-153-130 123-102-130-203-260-260-201-132-236-627-127-260-103-130-531-510-513-103-175-130-137-137-137-713-175-137-130-310-135-130-175-105 351-135-153-517-351-571-510-570-1_V.mp3)
  Segment 18: 57536ms ~ 58112ms (file: 315-731-703-370-130-137-371 307-371-317-103-307-637-376-317-137-310-173-310-310-130-310-307-130-703-731-137-317-307-301-307-731-317-371-137-713-735-370-753 701-703-371-103-301-3_V.mp3)
  Segment 19: 59112ms ~ 59688ms (file: 136-231-213-321-216-120-162-105-126-102-160-136-261-327-102-103-123-123-125-217-705-726-312-271-276-376-326-413-671-621-163-136-173-321-103-102-132 143-176-123-216-712-701-103-1_V.mp3)
  Segment 20: 60688ms ~ 61264ms (file: 105-105-105-105-157-175-175-170-157-175-157-157-157-175-105-157-170-154 103-105-105-105-150-157-107-170-107-105-173-175-157-156-157-157-175-175 135-150-157-150-510-510-150-175-7_V.mp3)
  Segment 21: 62264ms ~ 62840ms (file: 362-372-327-217-327-137-312-741-571-574 641-316-213-312-134-351-352-132-213-152-356-513-541-513-521-513 235-273-237-302-321-327-321_V.mp3)
  Segment 22: 63840ms ~ 64416ms (file: 570-570-570-705-231-203-206-203-621-261-263-312-123-260-620-627-106-213-126-302-263-231-132-123-132-301-312-132-301-321-231-236-263-231-312-352-531-563-751-573-750-705-571-750-1_V.mp3)
  Segment 23: 65416ms ~ 65992ms (file: 157-751-754-751-715-751-751-715-175-715-715-715-751-751-715-715-713-715-713-753 517-571-571-157-175-173-157-175-175-715-172-137-715-170-176-175-173-713-173-175-715-713-710-173-7_W.mp3)
  Segment 24: 66992ms ~ 72176ms (file: 127-273-230-321-732-327-735-576-546-725-301-163-132-237-150-153-132-206-765-672-312-275-206-620-562-765-271-507-267-756-675-627-326-720-617-723-167-267-753-175-327-520-103-231-5_V.mp3)
  Segment 25: 73176ms ~ 73752ms (file: 103-132-103-103-137-126-671-726-137-172-120-230-167-103-137-517-135-135-103-167-153-163-316-132-160 146-625-103-126-305-362-312-301-103-132-106-165-103-103-176-102-132-126-143-1_V.mp3)
  Segment 26: 74752ms ~ 75328ms (file: 162-173-236-206-203-320-230-326-230-203-276-126-120-103-102-132-615-106-167-261-206-320-260-102-162-132-750-756-307-371-576-675-106-675-167-163-167-230-260-206-260-672-176-617-6_V.mp3)
  Segment 27: 76328ms ~ 76904ms (file: 317-170-317 130-173-731-173-170-103-137-301-317-102-103-317-137-103-317-130-302-137-310-130-312-130-135-137-130-370-721-107-103 173-315-730-730-310-507-617-671-316-716-176-301-1_V.mp3)
  Segment 28: 77904ms ~ 78480ms (file: 167-657-615-617-671-167-132-123-126-106-162-167-167-163-167-165-167-615-621-617-657-657-567-165-167-615-165-165-617-167-617-167-165-657-165-615-516-651-617-651-156-567-675-756-5_W.mp3)
  Segment 29: 79480ms ~ 80056ms (file: 631-162-612 152-162 230-236-210-201-326_W.mp3)
  Segment 30: 81056ms ~ 86852ms (file: 230-263-327-237-327-762-275-213-273-726-120-672-276-126-617-276-263-756-756-237-203-230-276-275-172-576-301-267-627-123-123-127-276-230-205-102-720-120-203-276-267-267-216-267-1_W.mp3)
  Segment 31: 87852ms ~ 88428ms (file: 761-671-152-315-762-726-762-726-732-326-712-765-765 105-123-267 153-163-136-213-103-105-132-107-623-621-601-365-165-651 612-167-143-163-136-136-136-516-627-103-130-103-157-671-1_V.mp3)
  Segment 32: 89428ms ~ 95872ms (file: 307-230-257-260-260-637-367-367-102-126-106-260-173-210-612-673-762-106-136-326-623-276-710-761-361-316-267-602-370-206-320-230-326-267-672-672-276-627-263-326-167-137-207-327-2_W.mp3)
  Segment 33: 96872ms ~ 102524ms (file: 132-756-325-132-107-107-237-317-107-735-132-321-236-123-751-756-276-754-267-230-276-562-574-715-107-574-723-672-317-376-371-752-275-273-372-731-372-376-765-276-726-372-172-371-3_W.mp3)
  Segment 34: 103524ms ~ 104100ms (file: 756-725-635-750-326-257-235-637-726-256-752-257-752-756-572-526-325-132-162-372-163-235-236-325-627-275-327-132-205-726-236-267-635-756-275-132-510-316-312-162-217-216-143-536-5_V.mp3)
  Segment 35: 105100ms ~ 105676ms (file: 316-621-261-623 136-103-103-103-361-206-312-361-103-132-106-136-123-215-125-627-632-263-602-632-612-321-637-671-316-162-123-102-132-317-137-160-253-136-610-617-267-173-213-103-7_V.mp3)
  Segment 36: 106676ms ~ 107252ms (file: 756-756-756-574-574-574-571-517-756-327-276-263-612-623-167-237-571-152-157 765-326-267-527-152-267-273-125-312-752-735-213-257-720-725-235-123-705-276-231-276-625-527-256-175-1_V.mp3)
  Segment 37: 108252ms ~ 108828ms (file: 756-765-765-765-756-756-756-765-756-765-756-507-517-157-570-157-157-175-157-570-175-751-617-756-765-762-765-675-765-675-751-765-761-765-765-751-756-675-765-756-765-756-756-517-5_W.mp3)
  Segment 38: 109828ms ~ 118828ms (file: 213-517-573-510-321-103-103-103-315-130-137-357-351-130-315-137-750-357-357-617-301-135-175-157-362-576-716-657-713-173-156-176-673-136-216-127-102-127-302-172-350-153-135-105-2_W.mp3)
  Segment 39: 119828ms ~ 120404ms (file: 713-621-326-257-571-127-560-526-763-162-627-632-765-623-527-561 261-132-630-316-362-123-362-632-536-132-261-213-527-152-627-270-236-162-132-327-513-576-507-265-754-163-601 127-1_V.mp3)
  Segment 40: 121404ms ~ 126948ms (file: 301-547-756-756-756-312-270-372-765-615-362-534-354-356-325-103-213-172-371-312-361-132-275-657-657-135-120-517-162-217-120-230-310-715-617-123-576-573-576-135-725-753-315-135-5_W.mp3)
  Segment 41: 127948ms ~ 128524ms (file: 715-715-175-715-175-175-715-715-715-751-715-751-751-715-715 157-150-715-751-756-751-751-756-756-756-751-715-751-756-751 501-510-150-150-510-157-157-751-175-173-175-175-173-175-1_W.mp3)
  Segment 42: 129524ms ~ 130100ms (file: 126 213-162-631-130-163-167-651-156-217-162-631-167-316-160-163-103-136-136-610-106-106-156-106-165-163-103-312-627-617-167-137-236-612-761-123-102-102-106-103-263-621 612-103-1_V.mp3)
  Segment 43: 131100ms ~ 131676ms (file: 413-213-123-172-671-701-731-576-765-136-135-572-256-516-132-523-571-217-275-312-257-215-217-127-576-175-173-125-205-276-715-567-627-745-571-157-547-275-657-576-273-236-236-576-1_V.mp3)
  Segment 44: 132676ms ~ 133252ms (file: 751-753-672-527-765-572-153-371-625-351-312-675-173-367-721-123-512-752-715-215-301-316-753-137-573-132-137-321-135-271-517-315-756_V.mp3)
  Segment 45: 134252ms ~ 140948ms (file: 625-326-136-126-126-276-236-132-326-136-162-103-123-102-102-120-123-132-123-752-327-625-261-562-152-362-312-132-321-132-237-231-236-213-317-132-713-216-627-267-574-276-763-216-2_W.mp3)
  Segment 46: 141948ms ~ 142524ms (file: 312-132-375-637-375-351-367-103-573-576-570-576-572-137-156-176-136-103-163-136-576-576-576_W.mp3)
  Segment 47: 143524ms ~ 144100ms (file: 750-750-750-750-154-105-105-501-150-105-157-175-156-170-156-157-175-571-751-175-571-175-157-175-175 540-105-105-150-501-157-715-567-157-167-751-175-176-751-176-176-567-617-651-1_V.mp3)
  Segment 48: 145100ms ~ 151400ms (file: 263-120-321-130-231-321-103-102-102-751-127-130-213-756-231-527-257-235-231-326-231-316-176-236-572-267-263-627-263-725-231-310-326-735-102-160-613-316-326-312-316-316-123-216-3_W.mp3)
  Segment 49: 152400ms ~ 152976ms (file: 106-162-160-136-152-105-675-132-103-376-132-163-103-132-153-167-612-126-137-103-137-601-162-176-726-172-627-671-162-617-612-631-613-610-612-613-163-106-103-106-103-153-160-136-5_V.mp3)
  Segment 50: 153976ms ~ 158260ms (file: 326-213-352-276-710-607-236 276-267-276-736-507-721-257-572-517-267-675-216-756-765-752-576-751-237-372-257-257-257-231-507-753-576-165-673-236-705-756-765-765-753-237-327-570_V.mp3)
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
> **Time**: 2026-04-05 08:11:02
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
"transcript": "안녕하세요 오늘 날씨가 참 좋네요 그쵸",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 150, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 1450, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 2100, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "참", "start_ms": 3000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 3500, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "그쵸", "start_ms": 4600, "dur_ms": 500, "phase_lr_deg": 0.0}
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
"transcript": "외국어 공부하는 게 생각보다 쉽지 않더라고요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "외국어", "start_ms": 200, "dur_ms": 800, "phase_lr_deg": -5.0},
{"word": "공부하는", "start_ms": 1100, "dur_ms": 1000, "phase_lr_deg": -5.0},
{"word": "게", "start_ms": 2200, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "생각보다", "start_ms": 2600, "dur_ms": 900, "phase_lr_deg": -5.0},
{"word": "쉽지", "start_ms": 3600, "dur_ms": 600, "phase_lr_deg": -5.0},
{"word": "않더라고요", "start_ms": 4300, "dur_ms": 1100, "phase_lr_deg": -5.0}
]
},
{
"segment": 6,
"transcript": "그래도 꾸준히 하다 보면 언젠가는 자연스럽게 말할 수 있는 날이 오겠죠 파이팅",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "그래도", "start_ms": 180, "dur_ms": 700, "phase_lr_deg": -5.0},
{"word": "꾸준히", "start_ms": 950, "dur_ms": 800, "phase_lr_deg": -5.0},
{"word": "하다", "start_ms": 1800, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "보면", "start_ms": 2350, "dur_ms": 600, "phase_lr_deg": -5.0},
{"word": "언젠가는", "start_ms": 3100, "dur_ms": 1100, "phase_lr_deg": -5.0},
{"word": "자연스럽게", "start_ms": 4300, "dur_ms": 1300, "phase_lr_deg": -5.0},
{"word": "말할", "start_ms": 5700, "dur_ms": 600, "phase_lr_deg": -5.0},
{"word": "수", "start_ms": 6400, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "있는", "start_ms": 6700, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "날이", "start_ms": 7200, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "오겠죠", "start_ms": 7800, "dur_ms": 900, "phase_lr_deg": -5.0},
{"word": "파이팅", "start_ms": 9500, "dur_ms": 800, "phase_lr_deg": -5.0}
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
"transcript": "Can you help me with this sentence please?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 300,
"audio_type": "speech",
"words": [
{"word": "Can", "start_ms": 300, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 580, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "help", "start_ms": 820, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "me", "start_ms": 1250, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "with", "start_ms": 1500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "this", "start_ms": 1850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "sentence", "start_ms": 2250, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "please?", "start_ms": 3100, "dur_ms": 600, "phase_lr_deg": 0.0}
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
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 14,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 15,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
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
"transcript": "네 물론이죠 어떤 부분이 어려우신가요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 250, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "물론이죠", "start_ms": 600, "dur_ms": 900, "phase_lr_deg": -5.0},
{"word": "어떤", "start_ms": 1600, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "부분이", "start_ms": 2150, "dur_ms": 600, "phase_lr_deg": -5.0},
{"word": "어려우신가요", "start_ms": 2800, "dur_ms": 1100, "phase_lr_deg": -5.0}
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
"transcript": "이 단어의 발음이 조금 헷갈려요 다시 한번 들려주실래요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 200, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "단어의", "start_ms": 450, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "발음이", "start_ms": 1100, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 1850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "헷갈려요", "start_ms": 2400, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 3400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "한번", "start_ms": 3950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "들려주실래요", "start_ms": 4400, "dur_ms": 1000, "phase_lr_deg": 0.0}
]
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
"transcript": "천천히 발음해 드릴게요 잘 들어보세요 자 시작합니다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 220,
"audio_type": "speech",
"words": [
{"word": "천천히", "start_ms": 220, "dur_ms": 800, "phase_lr_deg": -5.0},
{"word": "발음해", "start_ms": 1100, "dur_ms": 700, "phase_lr_deg": -5.0},
{"word": "드릴게요", "start_ms": 1850, "dur_ms": 800, "phase_lr_deg": -5.0},
{"word": "잘", "start_ms": 2800, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "들어보세요", "start_ms": 3200, "dur_ms": 900, "phase_lr_deg": -5.0},
{"word": "자", "start_ms": 4300, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "시작합니다", "start_ms": 4600, "dur_ms": 1100, "phase_lr_deg": -5.0}
]
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
"transcript": "카라오케 시스템으로 공부하니까 훨씬 재밌는 것 같아요 지루하지도 않고요",
"speaker": "speaker_1",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "카라오케", "start_ms": 150, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "시스템으로", "start_ms": 1200, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "공부하니까", "start_ms": 2400, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "훨씬", "start_ms": 3700, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "재밌는", "start_ms": 4400, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 5200, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 5500, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "지루하지도", "start_ms": 6400, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "않고요", "start_ms": 7400, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "맞아요 노래 가사를 따라 부르면서 익히면 기억에도 더 오래 남죠",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "맞아요", "start_ms": 200, "dur_ms": 700, "phase_lr_deg": -5.0},
{"word": "노래", "start_ms": 1000, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "가사를", "start_ms": 1600, "dur_ms": 700, "phase_lr_deg": -5.0},
{"word": "따라", "start_ms": 2400, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "부르면서", "start_ms": 3000, "dur_ms": 900, "phase_lr_deg": -5.0},
{"word": "익히면", "start_ms": 4000, "dur_ms": 700, "phase_lr_deg": -5.0},
{"word": "기억에도", "start_ms": 4800, "dur_ms": 800, "phase_lr_deg": -5.0},
{"word": "더", "start_ms": 5700, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "오래", "start_ms": 6000, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "남죠", "start_ms": 6600, "dur_ms": 600, "phase_lr_deg": -5.0}
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
"transcript": "일본어 가사도 한 번 도전해 볼까요 조금 어려울 수도 있지만요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "일본어", "start_ms": 250, "dur_ms": 800, "phase_lr_deg": -5.0},
{"word": "가사도", "start_ms": 1100, "dur_ms": 700, "phase_lr_deg": -5.0},
{"word": "한", "start_ms": 1900, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "번", "start_ms": 2150, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "도전해", "start_ms": 2400, "dur_ms": 800, "phase_lr_deg": -5.0},
{"word": "볼까요", "start_ms": 3300, "dur_ms": 700, "phase_lr_deg": -5.0},
{"word": "조금", "start_ms": 4200, "dur_ms": 600, "phase_lr_deg": -5.0},
{"word": "어려울", "start_ms": 4900, "dur_ms": 700, "phase_lr_deg": -5.0},
{"word": "수도", "start_ms": 5700, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "있지만요", "start_ms": 6200, "dur_ms": 1000, "phase_lr_deg": -5.0}
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
"transcript": "좋아요 도전해보고 싶어요 어려운 만큼 보람도 있겠죠",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "좋아요", "start_ms": 180, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "도전해보고", "start_ms": 1000, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "싶어요", "start_ms": 2200, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "어려운", "start_ms": 3200, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "만큼", "start_ms": 4100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "보람도", "start_ms": 4700, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "있겠죠", "start_ms": 5500, "dur_ms": 900, "phase_lr_deg": 0.0}
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
"transcript": "그럼 다음 문장으로 넘어가 볼게요 준비되셨나요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 200, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "다음", "start_ms": 800, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "문장으로", "start_ms": 1400, "dur_ms": 900, "phase_lr_deg": -5.0},
{"word": "넘어가", "start_ms": 2400, "dur_ms": 700, "phase_lr_deg": -5.0},
{"word": "볼게요", "start_ms": 3200, "dur_ms": 800, "phase_lr_deg": -5.0},
{"word": "준비되셨나요", "start_ms": 4200, "dur_ms": 1200, "phase_lr_deg": -5.0}
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
"transcript": "네 준비됐습니다 빨리 시작해 주세요 기대돼요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "준비됐습니다", "start_ms": 550, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "빨리", "start_ms": 1800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "시작해", "start_ms": 2500, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 3300, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "기대돼요", "start_ms": 4300, "dur_ms": 1000, "phase_lr_deg": 0.0}
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
"transcript": "좋습니다 이번 문장은 조금 기니까 집중해서 들어보세요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "좋습니다", "start_ms": 200, "dur_ms": 800, "phase_lr_deg": -5.0},
{"word": "이번", "start_ms": 1100, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "문장은", "start_ms": 1700, "dur_ms": 700, "phase_lr_deg": -5.0},
{"word": "조금", "start_ms": 2500, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "기니까", "start_ms": 3100, "dur_ms": 700, "phase_lr_deg": -5.0},
{"word": "집중해서", "start_ms": 3900, "dur_ms": 900, "phase_lr_deg": -5.0},
{"word": "들어보세요", "start_ms": 4900, "dur_ms": 1100, "phase_lr_deg": -5.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 08:11:02*
