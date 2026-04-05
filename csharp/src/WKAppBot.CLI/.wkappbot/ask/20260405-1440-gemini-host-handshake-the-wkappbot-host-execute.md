# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=24268  time=14:39:39  uptime=48596s  mem=385MB  system_processes=353"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_143928.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 571-571-751-751-751-571-571-571-574-571-574-571 571-571-571-574-574-517-571-571-751-517-157-571-517-517-150-517_W.mp3)
  Segment 2: 1576ms ~ 3196ms (file: 576-576-573-571 574-574-574-571-571-574-751-715-751-751-751-751-751-571-571-571-571-571-751-571-751 517-175-715-751-715-517-751-715-571-753-576-573-576-576-756-576-573-753-573-5_W.mp3)
  Segment 3: 4196ms ~ 4772ms (file: 754-751-751-751-751-751-751-751-751-751-571-751-754-751-751 574-517-571-574-751-751-751-715-715-576-576-752-756-756-576-756 574-574-574-574-754-754-754-750-751-751-751-751-175-1_W.mp3)
  Segment 4: 5772ms ~ 6348ms (file: 517-510-510-517-570-517-517-571-571-517-157-517 570-570-517-570-157-517-150-150-501-157-150-150-507-510-501-510-510-570-501-517 150-517-175-517-157-157-517-751-170-510-157-571-1_V.mp3)
  Segment 5: 7348ms ~ 7924ms (file: 157-157-517-517-107-750-157-150-157-570-571 150-153-150-510-150-571-517-715-157-175-175-571-157-751-517-570-571-571-571-517 574-157-150-150-150-157-150-157-571-751-175-157-751-1_W.mp3)
  Segment 6: 8924ms ~ 9500ms (file: 754-751-751 574-574-571-574-574-571-751-571-754-751-754-751-751-754-571-574-571-574-571-571-751-175-571-517-571-571-574-754-571 570-574-571-751-715-571-751-715-751-715-715-175-7_W.mp3)
  Segment 7: 10500ms ~ 11076ms (file: 175-175-517-517-157 574-547-574-574-571-574-574-574 574-574-574-574-574-574-572 516-754-571-751-571-175-571-571-751-175-517-751-571-517-571-751-517-751-751-751-571-751-751-751-7_W.mp3)
  Segment 8: 12076ms ~ 12652ms (file: 157-175-751-175-715-715-517-175-175-715-751-175-751-715-751-175-751-175 175-170-175-157-175-175-517-751-173-750-175-157-715-175-751-157-157-175-173-715-715-715-157-715-751-751-7_V.mp3)
  Segment 9: 13652ms ~ 14228ms (file: 517-570-751-517-571-570-517-570-501-510-571 157-571-517-157-517-150-510-571-510-705-750-175-715-715-175-571-157-517-517-157-517-157-517 157-571-157-170-157-150-150-517-570-751-7_V.mp3)
  Segment 10: 15228ms ~ 15804ms (file: 571-157-501-150-510-510-517-105-530-571-157-571-517-157-751-571-517-157-517-517-517-157-571-571-517-571-157-570 157-715-170-715-175-715-710-751-715-751-751-756-753-756-765-765-7_V.mp3)
  Segment 11: 16804ms ~ 17380ms (file: 570-570-570-571-517-751-715-710-750-175-710-175-176-715-715-175-705-751-715-570-571-751-715-751-571-751-571 750-571-750-175-170-175-715-715-715-751-175-750-751-571-571-571-576-5_V.mp3)
  Segment 12: 18380ms ~ 18956ms (file: 517-571-571-517 510-517-157-517-157-153-135-157-157-150-157-150-501-157-150-150-150-175-571-157-157-157-751-157-517-507-157-517-510 517-517-517-157-150-175-510-517-157-571-750-7_V.mp3)
  Segment 13: 19956ms ~ 20532ms (file: 751-715-751-570 754-754-574-574-574-574-574-574-574-574-574-574-574-574-574-574-574 570-751-571-574-754-574-571-751-751-571-571-517-751-750-571-571-571-571-571-754-573_W.mp3)
  Segment 14: 21532ms ~ 22108ms (file: 571-574-574-574-574-574-574-574-574-756-571-574-574-571-574-574-574-574-574 574-751-750-751-715-715-175-175-571-751-175-517-517-571-751-751-157-571-517-571-571-175-571-571-571-1_W.mp3)
  Segment 15: 23108ms ~ 23684ms (file: 157-175-731-175-175-710-175-571-756-750-756-574-756-756-574-576 574-571-751-751-715-517-753-175-135-170-175-175-157-175-175-175-751-715-571-175-571-751-571-751-754 574-517-715-5_W.mp3)
  Segment 16: 24684ms ~ 25260ms (file: 571-751-571-576-751-751-751-573-571-751-571-157-571-157-571-157-571-517-571-571-517-157-571-517-517-175-170-715-710-175-571-175-157-157-157-175-135-157-157-175-157-157-752-750-7_W.mp3)
  Segment 17: 26260ms ~ 26836ms (file: 150-507-517-517 175-107-705-170-175-150-157-175-157-175-175-175-153-175-157-751-175-715-157-571-517-157-571-571 574-574-547-510-510-501-150-153-150-510-510-157-517-517-510-517-1_V.mp3)
  Segment 18: 27836ms ~ 28412ms (file: 753-572 570-574-574-751-570-751-751-751-715-754-751-751-715-715-751-751-175-175-756-571-576-576-572-576-572-576-576-753 570-517-715-157-751-715-750-715-710-175-175-751-574-756-5_W.mp3)
  Segment 19: 29412ms ~ 29988ms (file: 571-751-751-571-751-751-751 574-574-574-571-754-571-574-570-571-751-571-574-751-751-751-175-715-751-751-751-751-175-751-175-571-571-751-754-751-571 574-754-574-574-574-574-754-5_W.mp3)
  Segment 20: 30988ms ~ 31564ms (file: 150-157-517-150-157-157-517-517-571-517-517-517-517-571-570-517-175-715-571-570-150-751 574-517-157-105-150-507-510-510-150-517-501-571-517-157-751-751-157-751-175-175-175-715-7_W.mp3)
  Segment 21: 32564ms ~ 33140ms (file: 130-510-153-573-153-510-501-517-512-527-521-502-752-756 751-157-517-157-751-715-517-710-715-571-571-517-175-157-751-175-715-571-517-157-571-153-135-135-310-150-103-105-103-105-5_W.mp3)
  Segment 22: 34140ms ~ 34716ms (file: 576-657-257-267-627 574-571-574-570-517-571-751-715-715-750-576-570-570_W.mp3)
  Segment 23: 35716ms ~ 36652ms (file: 570-510-157-157-157-157-517-157-751-517-751-710-157-517-105-517-715-175-751-750-710-751-157-715-750-751 157-153-157-153-157-517-173-157-715-153-715-750-750-751-710-751-701-507-7_V.mp3)
  Segment 24: 37652ms ~ 38228ms (file: 572-572 751-571-175-751-571-751-715-175-751-754-571-571-571-571-571-571-573-571-573-570-570-570-570-571-572-571-753 571-571-574-571-751-751-715-751-754-571-751-715-751-715-157-5_W.mp3)
  Segment 25: 39228ms ~ 39804ms (file: 574-574-574-574-574-574-574-574 754-574-574-751-754-571-751-754-571-754-574-571-574-754-756-756-574-754 574-574-574-574-571-751-571-751-571-751-571-751-751-715-751-751-571-715-7_W.mp3)
  Segment 26: 40804ms ~ 41380ms (file: 157 157-571-157-157-157-576-517-517-157-175-150-157-517-150-150-517-150-510-150-571 513-510-150-157-153-157-517-157-175-571-157-157-571-517-715-157-715-517-571-570-570 153-150-5_W.mp3)
  Segment 27: 42380ms ~ 46556ms (file: 315-315 157-571-157-105-170-157-153-571-713-157-175-175-715-175-517-157-157-572-750-571-120-132-310-627-260-103-132 517-571-153-153-157-153-175-157-571-157-175-135-175-107-507-7_V.mp3)
  Segment 28: 47556ms ~ 48132ms (file: 571-517-571-571-510-571-517-571-570-517-571 517-517-157-510-517-157-571-517-571-517-517-571-157-175-751-175-715-157-175-157-510-751-571-570-571 157-517-157-150-175-157-170-157-7_V.mp3)
  Segment 29: 49132ms ~ 49708ms (file: 105-571-501-571-517-517-570-570-150 150-150-501-150-510-157-150-517-157-150-157-517-157-510-157-517 510-510-510-517-513-150-517-150-153-513-150-150-510-510-150-150-150-150-150-5_W.mp3)
  Segment 30: 50708ms ~ 51284ms (file: 574-751-751-751-571-571-754-751-715-175-751-715-715-715-751-751-715-715-751-753-751 571-751-571-571-571-571-751-571-157-751-751-751-753-751-157-571-756-753-571-571-756-571-751 5_W.mp3)
  Segment 31: 52284ms ~ 52860ms (file: 751-571-751-751-571-750-157-571-571-751-175-715-751-571-751-576-753-571-573-576-573-573 571-574-574-571-571-571-571-751-751-715-751-751-751-715-751-751-751-715-751-517-574-751-5_V.mp3)
  Segment 32: 53860ms ~ 54436ms (file: 327-137 571-571-715-157-751-571-571-571-517-571-571-571-571-517-571-571-571-571-571-571 574-574-574-574-157-574-574-571-574-571-574-571-574-574-571-571-754 574-574-574-574-547-5_W.mp3)
  Segment 33: 55436ms ~ 56012ms (file: 751 570-517-150-157-157-157-175-517-157-175-751-157-571-751-517-157-571-517-571-751-517-574-571-571-571-570 517-157-150-157-510-150-517-157-157-510-715-517-751-517-517-157-751-5_V.mp3)
  Segment 34: 57012ms ~ 57588ms (file: 571-751-157-517-571-175-751-574-715-751-735-751-765-715-751-576-751-756-756 571-571-715-750-751-574-751-715-715-715-751-175-715-175-751-751-715-175-751-715-715-175-715-137-756-5_W.mp3)
  Segment 35: 58588ms ~ 59164ms (file: 753-752-752-750-751 672-627-621-627-671-267-237-261-267-236-632-621 713-715-175-175-173-175-175-175-761-175-751-715-175-765-765-756-756-756-756-756-756-756-756-756 570-570-570-7_W.mp3)
  Segment 36: 60164ms ~ 60740ms (file: 175-375-175-157-751-753-751-571-571-570-571-571-573-573-571-571-571-571-572-572-571-574-572-574-574-572-756 570-571-175-715-715-715-157-715-571-751-571-751-715-751-751-715-715-7_V.mp3)
  Segment 37: 61740ms ~ 62316ms (file: 517-157-150-157-715-175-150-715-157-570-517-157-157-510-517-517-517-157 150-157-150-153-157-150-175-157-150-150-150-510-105-105-105-157-157-573-510-570-571-510 517-517-517-715-5_W.mp3)
  Segment 38: 63316ms ~ 63892ms (file: 765-271-175-751-173-751-762-712 715-750-175-715-175-715-175-751-715-715-713-715-715-715-715-751-751-731-715-517-751-715-751-715-157-751-571-752-756-576-756-753 157-571-715-751-7_W.mp3)
  Segment 39: 64892ms ~ 65468ms (file: 371-357-175-751 153-510-175-517-157-157-150-175-157-157-175-751-175-175-175-351-751-175-175-173-157-175-715-157-157-517 150-510-157-150-157-715-105-175-135-750-150-715-137-173-1_V.mp3)
  Segment 40: 66468ms ~ 67044ms (file: 567-657-576-657-657-567 571-570-571-570-571-750-751-157-751-751-751-710-175-715-175-751-715-175-710-751-175-751-571 574-574-574-571-517-570-571-517-574-571-570-571-570-574 571-5_W.mp3)
  Segment 41: 68044ms ~ 68620ms (file: 751-751 574-574-574-574-571-574-571-574-574-547-574-574-574-574-574-574-574-574-574 574-574-574-574-574-574-574-517-571-571-517-157-571-571-571-751-571-751-517-571-571 574-574-5_W.mp3)
  Segment 42: 69620ms ~ 70196ms (file: 157-571-150-750-510-517-175-571-157-571-157-507-517-570-571-517-571-570 157-157-517-517-157-157-105-157-517-715-750-157-517-571-715-571-751-517-517-510-571 157-157-157-157-517-1_V.mp3)
  Segment 43: 71196ms ~ 71772ms (file: 517-517-571-571-571-571-751-571-157 510-517-510-510-517-571-150-157-157-517-571-751-571-751-571-571-157 517-751-571-517-570-157-150-157-105-157-517-517-517-571-571-517-571-510-5_V.mp3)
  Segment 44: 72772ms ~ 73348ms (file: 571-571-571-517-751-751 570-175-157-175-175-157-175-157-175-175-157-157-751-517-157-571-157-751-571-571-571-157-571-751 571-715-157-157-750-517-175-157-175-157-173-715-157-175-1_V.mp3)
  Segment 45: 74348ms ~ 74924ms (file: 715-175-751-751-571-751-576-576-572 571-751-571-571-571-751-715-571-751-751-571-751-571-157-571-751-715-751-751-571-571-751-571-751 574-574-574-570-751-751-751-571-517-751-751-7_W.mp3)
  Segment 46: 75924ms ~ 76500ms (file: 517-157 157-157-517-571-517-517-157-517-157-157-517-150-517-510-175-157-175-157-571-175-517-570-510-517-517-571-571-571-571-517 571-517-157-157-157-751-510-157-517-571-517-517-5_V.mp3)
  Segment 47: 77500ms ~ 78076ms (file: 153-715-175-105-150-105-105-157-510-157-751 157-157-501-157-715-157-107-157-175-175-175-157-531-517-571-751-175-175-715-175-751-517-570-715 751-175-157-517-751-157-175-715-170-1_W.mp3)
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
> **Time**: 2026-04-05 14:40:52
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
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 14:40:52*
