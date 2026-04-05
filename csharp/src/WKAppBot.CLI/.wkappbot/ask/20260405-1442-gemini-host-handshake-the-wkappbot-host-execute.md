# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=24432  time=14:40:30  uptime=45300s  mem=458MB  system_processes=354"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_143954.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 153-715-175-105-150-105-105-157-510-157-751 157-157-501-157-715-157-107-157-175-175-175-157-531-517-571-751-175-175-715-175-751-517-570-715 751-175-157-517-751-157-175-715-170-1_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 517-517-517-150-570-715-175-751-175-715-175-507-570-571-150-150-157-153-517-501-507-105-510-501-501 153-501-150-571-517-157-135-150-153-175-157-751-715-751-510-517-571-150-157-5_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 517-571-571 175-107-157-517-571-137-157-157-157-501-170-157-517-517-571-571-517-157-157-157-157-157-157-571 170-571-157-157-175-157-157-150-571-107-150-157-501-517-517-175-517-1_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 751-157-715-751-751-571-756-751-751-367-157-753-756-735-756-753 157-715-751-751-750-175-715-751-571-751-715-175-715-751-715-173-315-761-623-657-756-573 751-157-175-570-751-715-7_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 517-150-150-150-157-150-157-517-571-175-157-571-517-157-517-517-570 510-150-510-157-150-157-150-157-517-517-157-150-157-157-175-715-175-571-751-157 517-513-517-510-157-150-510-1_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 572-523-527-572-572-571-532-572-571-527-527-725-723 571-751-571-571-571-715-175-157-157-517-751-571-175-715-175-517-175-751-571-571-751 751-571-157-571-570-517-751-571-175-750-7_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 157-157-175-157-517-157-751-517-157-517-571-715-751-150-715-175-571-751-715-517-570-715-751-517-751-571 157-517-517-153-517-157-157-751-571-517-157-570-715-570-751-175-517-751-7_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 574-574-574-573-574-574-573-574-574 574-547-574-574-574-574-574-574-574-574-574-574-574 547-547-574-574-574-574-574-574-547-547-574-574-574 130-574-571-571-570-571-570-750-517-5_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 576-657-257-267-627 574-571-574-570-517-571-751-715-715-750-576-570-570_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 567-657-576-657-657-567 571-570-571-570-571-750-751-157-751-751-751-710-175-715-175-751-715-175-710-751-175-751-571 574-574-574-571-517-570-571-517-574-571-570-571-570-574 571-5_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 751-574-574-574-574-574-754-754-571-756-754-574-574 574-574-574-570-574-574-571-574-754-571-571-571-574-571-751-751-751-751-571-573 574-574-574-574-574-574-517-517-517-517-517-5_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 574-751-751-751-571-571-754-751-715-175-751-715-715-715-751-751-715-715-751-753-751 571-751-571-571-571-571-751-571-157-751-751-751-753-751-157-571-756-753-571-571-756-571-751 5_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 157-715-715-751 571-571-750-751-571-175-715-157-751-751-715-175-173-175-751-715-751-751-571-571-751-751-576-571-573 571-571-751-751-751-517-751-517-571-715-175-571-571-751-751-7_W.mp3)
  Segment 14: 20488ms ~ 22108ms (file: 576-576-573-571 574-574-574-571-571-574-751-715-751-751-751-751-751-571-571-571-571-571-751-571-751 517-175-715-751-715-517-751-715-571-753-576-573-576-576-756-576-573-753-573-5_W.mp3)
  Segment 15: 23108ms ~ 23684ms (file: 517-157-157-517-157-157-157-571-571-517-517-517-571-157-571-157-571-571-751-517-751-571 157-157-517-517-157-517-571-517-571-517-175-715-517-517-571-150-517-751-517-571-507-570-5_V.mp3)
  Segment 16: 24684ms ~ 25260ms (file: 157-175-157-175-157-715-571-751-750-571-157-571-517-571-517-571 517-571-517-517-157-157-175-157-157-570-175-517-571-517-507-571-756-571-570-570 574-574-571-715-175-170-175-173-1_V.mp3)
  Segment 17: 26260ms ~ 26836ms (file: 715-715-751-715-175-175-175-751-175-571-157-576-576-756-576 571-175-175-715-517-571-571-756-576-576-675-756-675-567-756-765-765-675-675-675-672-672 173-715-175-175-751-175-715-7_V.mp3)
  Segment 18: 27836ms ~ 28412ms (file: 751-715-751-571-517-570-517-157-571-570 571-157-510-157-517-157-517-517-157-150-517-157-175-517-571-570-571-571-571 517-150-150-157-150-517-150-153-157-517-157-157-571-157-751-5_V.mp3)
  Segment 19: 29412ms ~ 29988ms (file: 157-715-570-571-574-574-571 175-715-751-157-715-175-715-751-157-517-751-754-751-517-715-157-175-715-175-751-571-751-715-753-715-751-574-751 571-574-574-574-157-715-751-715-751-7_W.mp3)
  Segment 20: 30988ms ~ 31564ms (file: 751-571-571-571-750 571-570-574-570-570-571-571-751-517-751-574-571-751-175-751-571-751-157-751-751-751-751-715-751-751-715-751-751-751 571-574-571-754-157-751-715-571-571-750-7_W.mp3)
  Segment 21: 32564ms ~ 33140ms (file: 175-375-175-157-751-753-751-571-571-570-571-571-573-573-571-571-571-571-572-572-571-574-572-574-574-572-756 570-571-175-715-715-715-157-715-571-751-571-751-715-751-751-715-715-7_V.mp3)
  Segment 22: 34140ms ~ 34716ms (file: 750-517-517-715-157-571-751-157-517-501-573-501 571-510-157-150-153-157-175-507-750-157-710-715-705-517 751-507-570-157-150-510-501-105-571-517-157-517-517-571-517-157-517-517-5_V.mp3)
  Segment 23: 35716ms ~ 36292ms (file: 574-547-574-571-574-574-574-517-574-576-571-574-571-715-571-571-157-175-175-157-751-170-150-107-170-150-105-105-107-175-157-750-751 574-574-574-574-571-574-571-517-570-571-157-5_W.mp3)
  Segment 24: 37292ms ~ 37868ms (file: 517-751-175-751-710-751-175-710-715 507-570-570-571-751-751-571-715-705-571-750-571-751-751-571 571-751-750-751-715-751-753-751-571-571-571-576-573 547-547-547-517-750-750-157-7_W.mp3)
  Segment 25: 38868ms ~ 39444ms (file: 175-157-751-571-501-570-170-715 510-157-157-150-517-135-175-175-157-175-715-157-710-175-750-517-751-751-501-157-571-570-570-750 157-513-517-517-507-150-157-175-157-150-157-175-5_V.mp3)
  Segment 26: 40444ms ~ 41020ms (file: 571-574-574-752-572-527 571-175-751-715-715-573-576-576-572-573-573-572-573-573-573-576-573-576-576-753-573-751-573-572-576-753-573 751-570-751-710-751-715-175-715-175-175-715-1_W.mp3)
  Segment 27: 42020ms ~ 42596ms (file: 751-570-750-751-715-570-751-570-750-571-574-517-175-751-517-571-517-571-517-571-517-571-751-754-751 571-751-570-751-571-571-571-751-754-751-571-571-751-571-751-571-715-571-571-5_W.mp3)
  Segment 28: 43596ms ~ 44172ms (file: 715-751-751-751-754-751-571 574-571-517-571-574-574-574-570-574-570-750-157-715-175-175-751-751-715-715-175-751-571-715-157-574-756-576 751-574-571-517-571-750-175-715-751-571-7_W.mp3)
  Segment 29: 45172ms ~ 45748ms (file: 571-157-571-571-571-571-157-175-715-175-715-715-751-175-571-756-752-571-751-751-571-357-573-756-753 571-571-751-751-751-751-571-751-751-715-751-751-571-715-175-715-751-751-175-1_W.mp3)
  Segment 30: 46748ms ~ 47324ms (file: 157-715-175-715-751-517-751-751-157-751-571-751-571-571-572-527 750-571-517-571-571-517-571-175-571-517-571-574-754-574 574-571-571-754-754-574-751-751-751-751-571-751-751-571-7_W.mp3)
  Segment 31: 48324ms ~ 48900ms (file: 157-157-150-571-175-750-175-701-710-571-705-715-750-750-750-750-751 571-157-157-517-157-517-157-157-571-571-571-571-517-571-571-570-571-157-517-570-751 517-150-157-517-517-157-1_W.mp3)
  Segment 32: 49900ms ~ 50476ms (file: 571-157-501-150-510-510-517-105-530-571-157-571-517-157-751-571-517-157-517-517-517-157-571-571-517-571-157-570 157-715-170-715-175-715-710-751-715-751-751-756-753-756-765-765-7_V.mp3)
  Segment 33: 51476ms ~ 52052ms (file: 315-351-765-325-567-562-567-576-267 751-571-751-715-571-175-751-756-756-376-712-273-627-567-251-215-621-627-627-627-672-571-537-751-715-157-751-571-751-715-715-751-175-175-157-7_W.mp3)
  Segment 34: 53052ms ~ 53628ms (file: 371-317-135-152-652-657-561-512 671-632-627-621-617-617-167-167-612-627-762 135-326-263-627-627-627-623-672-621-367 173-173-175-135-175-713-173-175-173-173-751-756-137-175 571-5_W.mp3)
  Segment 35: 54628ms ~ 55204ms (file: 756-716-756-765-753-762-751-715-715-716-715-715-716-715-175_W.mp3)
  Segment 36: 56204ms ~ 56780ms (file: 571-571-571-751-715-175-571-517-175-571-517-571-517-157-510-157-150-571-517-510-517-571 571-517-751-715-715-517-175-157-571-170-150-175-570-571-517-175-157-715-175-751-175-157-5_W.mp3)
  Segment 37: 57780ms ~ 58356ms (file: 105-571-501-571-517-517-570-570-150 150-150-501-150-510-157-150-517-157-150-157-517-157-510-157-517 510-510-510-517-513-150-517-150-153-513-150-150-510-510-150-150-150-150-150-5_W.mp3)
  Segment 38: 59356ms ~ 59932ms (file: 157-157-157-571-157-175-571-175-150-157-157-571-751-571-751-750-571-571-517 510-150-157-150-157-517-157-517-571-157-715-175-751-175-715-175-715-750-517-571-570-750-571 150-510-1_V.mp3)
  Segment 39: 60932ms ~ 61508ms (file: 517-751-157-150-715-157-751-570-750 571-751-715-571-150-715-715-751-715-715-175-751-157-170-751-710-175-715-710-715-751-175-751-751-571 574-574-547-574-547-574-574-751-571-571-7_V.mp3)
  Segment 40: 62508ms ~ 63084ms (file: 612-362-612-362-361-361-613-631-316-231 546-564-653-516-623-365-325-365-361-326-316-136-315-231-136-326-162-137-127-316 547-547-570-571-751-570-570-751-571-751-750-570-571 570-5_W.mp3)
  Segment 41: 64084ms ~ 64660ms (file: 574-574-574 570-574-574-574-574-574 517-571-157-571-751-571-574-574-754-754-574-754-754 632-362-163-316-173-175-137-135-157 574-571-574-751-751-751-750-751-571-756-572-572-752 7_W.mp3)
  Segment 42: 65660ms ~ 66236ms (file: 715-573-571-571-571 751-751-175-517-715-751-751-571-571-751-571-571-517-571-715-157-157-517-571-571-571-571-572-572-576-756-175 571-517-175-157-751-175-715-751-751-751-157-517-5_V.mp3)
  Segment 43: 67236ms ~ 67812ms (file: 175-175-517-517-157 574-547-574-574-571-574-574-574 574-574-574-574-574-574-572 516-754-571-751-571-175-571-571-751-175-517-751-571-517-571-751-517-751-751-751-571-751-751-751-7_W.mp3)
  Segment 44: 68812ms ~ 69388ms (file: 175-175-175-571-517-571-571-574-571 570-571-751-571-751-157-751-507-715-715-157-170-715-751-170-175-705-175-751-157-561-653-350-326-356-605-652-652-567 571-570-751-157-157-571-5_V.mp3)
  Segment 45: 70388ms ~ 70964ms (file: 571-715 150-157-150-157-175-175-157-715-175-170-175-750-175-175-715-173-157-531-513-157-130-510-137-105-105-153-517-150-375 517-157-517-175-751-157-175-157-751-157-751-715-175-1_V.mp3)
  Segment 46: 71964ms ~ 75456ms (file: 751-756-753-123-125-501-521-235-206-602-213-132-123-103-301-621-260-267-612-123-574-175-170-715-170-710-705-175-715-175-175-751-175-751-715-276-751-751-752-756-157-756-756 571-7_W.mp3)
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
> **Time**: 2026-04-05 14:42:43
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
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
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 14:42:43*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=6944  time=14:40:30  uptime=46639s  mem=409MB  system_processes=354"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_144006.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 751-750-157-571-751 570-750-751-571-571-751-570-517-570-715-751-150-157-175-715-175-175-715-570 517-571-571-175-571-157-751-751-715-571-751-571-751-170-701-175-175-157-710-157-7_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 574-574-574-574-574-574-574-574-574-574-574-574-574-574-574-571-574 157-510-571-574-571-574-574-751-574-574-574-157-571-570-571-751-751-175-157-157 574-574-517-517-571-547-574-5_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 751-715-751-751-754-751-754-751-571-751-754-571-751-571-751-571-574-571-751-754-751-571-754-574-751-754-751 754-574-751-571-751-751-715-715-715-751-751-715-756-756-756-751-751-7_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 517-570-751-517-571-570-517-570-501-510-571 157-571-517-157-517-150-510-571-510-705-750-175-715-715-175-571-157-517-517-157-517-157-517 157-571-157-170-157-150-150-517-570-751-7_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 571-157-571-571-571-571-157-175-715-175-715-715-751-175-571-756-752-571-751-751-571-357-573-756-753 571-571-751-751-751-751-571-751-751-715-751-751-571-715-175-715-751-751-175-1_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 150-105-150-501-501-510-150-510-571 157-517-517-517-570-751-157-175-157-170-175-751-150-157-157-175-175-105-150-150-153-107-105-157-150-510-517 517-157-517-157-175-170-150-715-1_V.mp3)
  Segment 7: 9456ms ~ 10392ms (file: 570-510-157-157-157-157-517-157-751-517-751-710-157-517-105-517-715-175-751-750-710-751-157-715-750-751 157-153-157-153-157-517-173-157-715-153-715-750-750-751-710-751-701-507-7_V.mp3)
  Segment 8: 11392ms ~ 11968ms (file: 571-571-751-715-517-715-517-751-715-517-715-751-571-751-157 157-157-150-157-157-510-150-150-157-157-517-150-517-157-157-157-157-517-571-571-517-517-571-517-157-571-517-571-570 1_V.mp3)
  Segment 9: 12968ms ~ 13544ms (file: 570-570-570-571-517-751-715-710-750-175-710-175-176-715-715-175-705-751-715-570-571-751-715-751-571-751-571 750-571-750-175-170-175-715-715-715-751-175-750-751-571-571-571-576-5_V.mp3)
  Segment 10: 14544ms ~ 15120ms (file: 571-517-715-571-570-570-571-571-754-571-751-571-750 574-574-157-510-150-157-510-150-510-157-150-157-517-157-517-150-157-510-157-507-517-175-157-157-157-150-751 571-517-150-571-1_V.mp3)
  Segment 11: 16120ms ~ 16696ms (file: 517-157-150-157-715-175-150-715-157-570-517-157-157-510-517-517-517-157 150-157-150-153-157-150-175-157-150-150-150-510-105-105-105-157-157-573-510-570-571-510 517-517-517-715-5_W.mp3)
  Segment 12: 17696ms ~ 18272ms (file: 765-271-175-751-173-751-762-712 715-750-175-715-175-715-175-751-715-715-713-715-715-715-715-751-751-731-715-517-751-715-751-715-157-751-571-752-756-576-756-753 157-571-715-751-7_W.mp3)
  Segment 13: 19272ms ~ 19848ms (file: 574-574-574-754-751-751-756-751-571-574-751 751-715-715-751-715-175-175-175-576-753-375-573-751-571-752-576-751-571-571-576-756-756-567-756 571-751-715-571-715-715-751-751-751-7_V.mp3)
  Segment 14: 20848ms ~ 21424ms (file: 571-715 150-157-150-157-175-175-157-715-175-170-175-750-175-175-715-173-157-531-513-157-130-510-137-105-105-153-517-150-375 517-157-517-175-751-157-175-157-751-157-751-715-175-1_V.mp3)
  Segment 15: 22424ms ~ 23000ms (file: 574-540-571-570-574-574-574-574-571-157-517-371-375-317-351-315-150-315-105-125-135-235-235-512-537-751 570-517-715-157-175-170-710-175-710-715-157-175-157-315-315-310-310-130-1_V.mp3)
  Segment 16: 24000ms ~ 24576ms (file: 751 570-517-150-157-157-157-175-517-157-175-751-157-571-751-517-157-571-517-571-751-517-574-571-571-571-570 517-157-150-157-510-150-517-157-157-510-715-517-751-517-517-157-751-5_V.mp3)
  Segment 17: 25576ms ~ 26152ms (file: 517-510-510-517-570-517-517-571-571-517-157-517 570-570-517-570-157-517-150-150-501-157-150-150-507-510-501-510-510-570-501-517 150-517-175-517-157-157-517-751-170-510-157-571-1_V.mp3)
  Segment 18: 27152ms ~ 27728ms (file: 517-157 157-157-517-571-517-517-157-517-157-157-517-150-517-510-175-157-175-157-571-175-517-570-510-517-517-571-571-571-571-517 571-517-157-157-157-751-510-157-517-571-517-517-5_V.mp3)
  Segment 19: 28728ms ~ 29304ms (file: 175-715-571-157-175-750-175-517-571-175-157-750-175-175-571-574-571-571 517-751-715-751-157-157-170-157-157-517-571-517-570-157-517-571-571-517-571-157-517-157-517-570-517-517-5_V.mp3)
  Segment 20: 30304ms ~ 30880ms (file: 572-523-527-572-572-571-532-572-571-527-527-725-723 571-751-571-571-571-715-175-157-157-517-751-571-175-715-175-517-175-751-571-571-751 751-571-157-571-570-517-751-571-175-750-7_W.mp3)
  Segment 21: 31880ms ~ 32456ms (file: 571-574-751-571-751-157-157-157-175-751-571-507-571 612-621-623-612-621-612-612-612-612-612-162-612-621-612-612-625-625-621 750-751-751-751-751-571-576-571-571-571-750-571-157-7_W.mp3)
  Segment 22: 33456ms ~ 34032ms (file: 175-175-175-751-715-751-175-751-715-751-715-751-751 751-751-751-751-751-751-751-751-751-751-751-715-715-175-715-715-175-715-715-751-750 315-175-715-173-751-751-750-715-751-715-1_V.mp3)
  Segment 23: 35032ms ~ 35608ms (file: 571-571-751-751-751-571-571-571-574-571-574-571 571-571-571-574-574-517-571-571-751-517-157-571-517-517-150-517_W.mp3)
  Segment 24: 36608ms ~ 37184ms (file: 571-570-574-574-574-571-574-571-517-574-571-517-571-570-571-571-517-571-571-574-574-574-574-574 571-574-571-574-571-571-517-517-157-157-517-574-571-573-573-754-574-574-574-517-5_W.mp3)
  Segment 25: 38184ms ~ 38760ms (file: 175-715-713-170-710-715-751-175-751-107-715-175-175-637-756 751-571-517-751-570-173-751-175-750-710-705-170-701-571-675-623-652-567-657-657-657-657-576-567-756 571-571-571-571-7_V.mp3)
  Segment 26: 39760ms ~ 40336ms (file: 175-571-570-751-751-715-751-715-751-750-756-751-753-576-571-576-576-571-571 574-574-574-574-574-574-574-574-574-754-756-751-574-571-751_W.mp3)
  Segment 27: 41336ms ~ 41912ms (file: 175-571-570-571 570-574-574-574-574-574-570-571-574-571-751-751-571-751-750-750-750-756 574-574-574-574_W.mp3)
  Segment 28: 42912ms ~ 43488ms (file: 751-716-756-756-756-571-571-751-157-751-715-574-752 175-175-137-531-751-571-173-751-715-175-751-175-751-715-751-751-751-751-751-751-715-715-715-715-715-751-753-756-571-571-571-5_W.mp3)
  Segment 29: 44488ms ~ 45064ms (file: 507-150-150-571-751 715-517-157-571-175-517-715-510-517-751-175-175-175-715-571-715-751-751-157-517-571-571-571-570-571-517 157-157-150-157-150-571-157-571-157-571-157-571-517-5_V.mp3)
  Segment 30: 46064ms ~ 46640ms (file: 175-157-175-517-157-175-571-751-175-715-507-571-510-571-157-517-751-150-175-750-175-571-517-751 517-157-157-157-157-157-517-157-715-157-751-175-571-571-157-570-750-517-157-571-7_V.mp3)
  Segment 31: 47640ms ~ 48216ms (file: 157-150-157-150-175-175-157-175-157-715-715-750-751-175-571-751-517-750-175-750-751-501-751 157-510-517-517-157-157-157-153-157-517-510-175-715-751-751-105-175-710-751-517-150-5_V.mp3)
  Segment 32: 49216ms ~ 49792ms (file: 175-517-715-175-175-751-157 574-571-157-150-157-510-517-150-157-150-157-517-150-510-517-157-751-157-750-751-751-710-157-570-751 517-150-517-150-510-150-153-517-157-105-157-175-1_V.mp3)
  Segment 33: 50792ms ~ 51368ms (file: 756-712-725-715-751-571-751-715-751-753-751 574-574-754-752-754-571-751-175-715-751-715-170-715-175-715-715-751-715-713-751-751-175-715-715-715-715-750 157-170-715-751-715-751-7_W.mp3)
  Segment 34: 52368ms ~ 52944ms (file: 175-375-175-157-751-753-751-571-571-570-571-571-573-573-571-571-571-571-572-572-571-574-572-574-574-572-756 570-571-175-715-715-715-157-715-571-751-571-751-715-751-751-715-715-7_V.mp3)
  Segment 35: 53944ms ~ 54520ms (file: 571-352 571-715-751-175-571-571-571-571-571-715-715-517-175-571-570-570-750-750-570-752-570-752-752-267-657-657-657-561-561-516-517 157-751-175-751-715-175-715-157-175-751-175-1_W.mp3)
  Segment 36: 55520ms ~ 56096ms (file: 570-571-751-751-751-571-751-750-571-715-715-170-751-701-170-710-710-710-137-570-157-517 571-750-751-715-175-715-751-750-175-157-175-715-750-751-715-170-175-710-175-710-715-170-7_V.mp3)
  Segment 37: 57096ms ~ 57672ms (file: 157-150-175-517-517-157-517-517-751-751-571-751-750-751-571-751-751-715-751-175-751-571-751-571-751-571-751-751 571-574-571-574-574-547-571-574-570-574-574-574-571-571-574-751-5_W.mp3)
  Segment 38: 58672ms ~ 59248ms (file: 715-751-751-751-754-751-571 574-571-517-571-574-574-574-570-574-570-750-157-715-175-175-751-751-715-715-175-751-571-715-157-574-756-576 751-574-571-517-571-750-175-715-751-571-7_W.mp3)
  Segment 39: 60248ms ~ 60824ms (file: 715-573-571-571-571 751-751-175-517-715-751-751-571-571-751-571-571-517-571-715-157-157-517-571-571-571-571-572-572-576-756-175 571-517-175-157-751-175-715-751-751-751-157-517-5_V.mp3)
  Segment 40: 61824ms ~ 62400ms (file: 175-753-756-715-751-175-157-571-571-715-157-170-715-175-371-713-163-137-631-627 671-312-621-625-627-627-627-621-612-672-627-165-162-615-657-567-751-657-576-576-756-572-157-517-5_W.mp3)
  Segment 41: 63400ms ~ 63976ms (file: 625-657-567-652-652-576-756 574-570-571-751-571-571-751-715-751-715-751-750-571-751-570-751-715-751-570-751-570-750 571-750-570-570-571-517-751-570-157-571-574-571-570-751-570-5_W.mp3)
  Segment 42: 64976ms ~ 65552ms (file: 567-576-571-567-576-576-263-275-726-271-716 571-751-571-715-750-571-576-265-651-657-625-167-137-726-752-572-715-175-507-570-510-516-571 157-571-751-571-571-571-571-157-751-175-1_W.mp3)
  Segment 43: 66552ms ~ 67128ms (file: 715-751-751-754-751 571-750-571-751-571-751-571 547 574-547-547-547-547-574-574-547-514-514-517-571-517-547-574-571-754-751-571-571 574-574-547-574-574-574-570-570-571-751-571-5_W.mp3)
  Segment 44: 68128ms ~ 68704ms (file: 157-715-175-715-751-517-751-751-157-751-571-751-571-571-572-527 750-571-517-571-571-517-571-175-571-517-571-574-754-574 574-571-571-754-754-574-751-751-751-751-571-751-751-571-7_W.mp3)
  Segment 45: 69704ms ~ 70280ms (file: 751-715-756-173-175-173-153-157-571_W.mp3)
  Segment 46: 71280ms ~ 71856ms (file: 751-751 574-574-574-574-571-574-571-574-574-547-574-574-574-574-574-574-574-574-574 574-574-574-574-574-574-574-517-571-571-517-157-571-571-571-751-571-751-517-571-571 574-574-5_W.mp3)
  Segment 47: 72856ms ~ 73432ms (file: 736-756-756-752 362-657-652-167-135-135-157-716-175-175-756-752-751-756-756-752-753-751-756-751-751-563-562-516-657-167-167-126-123-137-317-132-137-316-163-316-765-715-753-753-7_W.mp3)
  Segment 48: 74432ms ~ 75008ms (file: 753-573-573-753 751-571-751-157-751-175-715-517-715-175-751-571-571-571-571-574-517-573-576-571-573-573-576-357-753 570-571-754-751-751-751-750-751-715-715-715-715-175-517-571-7_W.mp3)
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
> **Time**: 2026-04-05 14:42:43
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
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
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 14:42:43*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=30524  time=14:40:30  uptime=36542s  mem=415MB  system_processes=354"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_143951.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 576-657-257-267-627 574-571-574-570-517-571-751-715-715-750-576-570-570_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 175-157-175-517-157-175-571-751-175-715-507-571-510-571-157-517-751-150-175-750-175-571-517-751 517-157-157-157-157-157-517-157-715-157-751-175-571-571-157-570-750-517-157-571-7_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 517-157-150-157-715-175-150-715-157-570-517-157-157-510-517-517-517-157 150-157-150-153-157-150-175-157-150-150-150-510-105-105-105-157-157-573-510-570-571-510 517-517-517-715-5_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 571-751-574-574-574-574-574-754-574-754 751-571-571-571-574-751-751-751-751-175-751-571-157-751-754-571-751-754-754-754-751-571-750-756 750-751-574-571-571-571-571-754-751-751-7_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 175-751-715-517-571-517-517-157-751-571-175-571-517-157-510-150-157-130-310-315-510-735 574-574-574-570-574-571-517-751-571-574-175-751-157-751-175-715-751-715-157-751-570-517-1_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 157-150-157-150-175-175-157-175-157-715-715-750-751-175-571-751-517-750-175-750-751-501-751 157-510-517-517-157-157-157-153-157-517-510-175-715-751-751-105-175-710-751-517-150-5_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 715-715-751-715-175-175-175-751-175-571-157-576-576-756-576 571-175-175-715-517-571-571-756-576-576-675-756-675-567-756-765-765-675-675-675-672-672 173-715-175-175-751-175-715-7_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 715-765-761-715-357-750-571 150-175-751-175-710-153-710-157-157-175-157-175-715-715-175-715-751-157-517-175-751 510-175-157-715-175-571-715-175-571-571-571-157-751-715-715-571-1_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 175-753-756-715-751-175-157-571-571-715-157-170-715-175-371-713-163-137-631-627 671-312-621-625-627-627-627-621-612-672-627-165-162-615-657-567-751-657-576-576-756-572-157-517-5_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 162-162-671-162-267-621-627-756-756-753-756-756-570-751-517-571-571-751-175-570-175-175-751-157-751-576-756-765-567-567-567-576-576-576-571-756-756-751-571-571-571-571-570 507-5_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 625-657-567-652-652-576-756 574-570-571-751-571-571-751-715-751-715-751-750-571-751-570-751-715-751-570-751-570-750 571-750-570-570-571-517-751-570-157-571-574-571-570-751-570-5_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 570-751-571-751-571-175-571-751-751-751-751-751-157-751-175-157-571-751-571-571_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 175-175-517-517-157 574-547-574-574-571-574-574-574 574-574-574-574-574-574-572 516-754-571-751-571-175-571-571-751-175-517-751-571-517-571-751-517-751-751-751-571-751-751-751-7_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 175-751-715-571-157-571-157-571-571-517-571-571-751-571-571-571-715-571-751-571-576 571-751-175-571-175-715-175-751-574-715-571-571-571-517-571-517-571-571-751-756-175-715-751-5_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 150-105-150-501-501-510-150-510-571 157-517-517-517-570-751-157-175-157-170-175-751-150-157-157-175-175-105-150-150-153-107-105-157-150-510-517 517-157-517-157-175-170-150-715-1_V.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 751 570-517-150-157-157-157-175-517-157-175-751-157-571-751-517-157-571-517-571-751-517-574-571-571-571-570 517-157-150-157-510-150-517-157-157-510-715-517-751-517-517-157-751-5_V.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 574-574-574-754-751-751-756-751-571-574-751 751-715-715-751-715-175-175-175-576-753-375-573-751-571-752-576-751-571-571-576-756-756-567-756 571-751-715-571-715-715-751-751-751-7_V.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 150-153-150-157-517-150-510-571-571-517-571-571-571-510-517-571 751-570-571-517-175-157-157-157-175-157-571-517-501-157-105-157-501-150-517 157-157-510-157-152-570-157-175-157-1_V.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 715-751-715-721-372-172-751-751-765 175-751-170-175-715-751-751-175-715-175-713-750-731-713-375-367-576-715-715-752-176-576-752-735-375-657-756 103-157-715-175-517-571-750-751-5_V.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 571-573 571-510-517-517-517-517-571-570-571-571-751-517-517-571-517-517-157-571-157-571-157-517-157-517-157-157-157-157-571-571 570-517-150-517-501-571-175-571-517-517-157-157-5_V.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 751-157-715-751-751-571-756-751-751-367-157-753-756-735-756-753 157-715-751-751-750-175-715-751-571-751-715-175-715-751-715-173-315-761-623-657-756-573 751-157-175-570-751-715-7_V.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 175-715-751-175-751-175-173-175-731-136-673-627-761 754-570-571-517-751-571-571-517-157-751-571-571-571-574-571-571-571-574-571-571-571 751-571-157-751-715-137-751-157-175-715-1_W.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 157-157-175-157-517-157-751-517-157-517-571-715-751-150-715-175-571-751-715-517-570-715-751-517-751-571 157-517-517-153-517-157-157-751-571-517-157-570-715-570-751-175-517-751-7_V.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 751-715-751-751-751-175-751-751-573-216-762-671-571 175-571-751-175-715-751-175-175-137-715-173-715-715-175-715-175-157-157-175-175-175-751-517-753-653 105-571-175-710-701-715-1_W.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 751-571-571-571-750 571-570-574-570-570-571-571-751-517-751-574-571-751-175-751-571-751-157-751-751-751-751-715-751-751-715-751-751-751 571-574-571-754-157-751-715-571-571-750-7_W.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 517-150-150-150-157-150-157-517-571-175-157-571-517-157-517-517-570 510-150-510-157-150-157-150-157-517-517-157-150-157-157-175-715-175-571-751-157 517-513-517-510-157-150-510-1_W.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 175-571-570-751-751-715-751-715-751-750-756-751-753-576-571-576-576-571-571 574-574-574-574-574-574-574-574-574-754-756-751-574-571-751_W.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 715-175-157-175-157-501-150-150-157-103-105-150 157-150-715-157-150-173-175-157-175-157-175-715-175-715-751-715-157-570-715-107-710-107-130-105-150-501-571 135-157-175-107-170-1_W.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 175-517-715-175-175-751-157 574-571-157-150-157-510-517-150-157-150-157-517-150-510-517-157-751-157-750-751-751-710-157-570-751 517-150-517-150-510-150-153-517-157-105-157-175-1_V.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 571-571-571-751-517-175-715-157-751-751-715-571-751-715-751-137-170_V.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 517-571-574-570-571-175-751-751-715-170-751-715-715-571-751-571-751-571-517-571-571-751-571-571 547-571-570-570-570-570-751-715-710-715-175-710-710-751-715-715-715-571-571-751-7_W.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 371-357-175-751 153-510-175-517-157-157-150-175-157-157-175-751-175-175-175-351-751-175-175-173-157-175-715-157-157-517 150-510-157-150-157-715-105-175-135-750-150-715-137-173-1_V.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 571-715 150-157-150-157-175-175-157-715-175-170-175-750-175-175-715-173-157-531-513-157-130-510-137-105-105-153-517-150-375 517-157-517-175-751-157-175-157-751-157-751-715-175-1_V.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 571-571-574 574-574-574-754-574-571-574-571 574-754-754-574-751-754-574-574-571-574-574-574-574-574-571-574-574-574-571-574-754-754-574 574-574-571-574-574-574-574-574-571-571-5_W.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 570-571-751-157-175-517-103-157-170-107-175-617-673-375-753 571-570-570-570-570-573-571-571-570-570-571-574-571-574-574-576-571-157-371-571-157 571-570-573-570-574-574-574-574-7_W.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 571-574 571-574-574-574-571-157-571-574-175-175-751-715-517-517-571-571-574-517-574-517-571-574-571 751-574-571-175-715-751-157-517-751-175-157-571-157-157-175-517-517-571-571-5_W.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 574-754-754-754-751-751-715-751-751-751-751-751-751-751-751 574-574-751-751-715-751-751-751-571-571-571-751-753-571-751-571-571-571-571 574-574-571-754-754-751-751-715-751-751-7_W.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 571-571-751-751-751-571-571-571-574-571-574-571 571-571-571-574-574-517-571-571-751-517-157-571-517-517-150-517_W.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 672-652 613-627-267-627-627-621 571-517-157-571-751-715-175-571-170-715-175-710-175-170-715-710-751-175-710-170-150-175-157-157-576-517-536-365-563-563-356-563-753 571-751-750-7_V.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 715-715-715-713-715-713-175-715-105-170-103-173-157-517-150-157 517-750-751-130-157-175-175-157-175-715-537-175-571-175-157-175-175-157-153-150-175-105-710-150-157 150-517-157-1_V.mp3)
  Segment 41: 63040ms ~ 67216ms (file: 315-315 157-571-157-105-170-157-153-571-713-157-175-175-715-175-517-157-157-572-750-571-120-132-310-627-260-103-132 517-571-153-153-157-153-175-157-571-157-175-135-175-107-507-7_V.mp3)
  Segment 42: 68216ms ~ 68792ms (file: 571-571-576-576-576-573-756-573-756 571-751-571-571-751-715-751-715-175-175-751-571-751-175-715-157-751-571-571-571-753-571-751-751-571-571-574-574-576-756-571 571-571-571-157-7_W.mp3)
  Segment 43: 69792ms ~ 70368ms (file: 571-570-510-570-571-571-576-571-571-517 510-157-157-157-517-175-150-157-157-175-157-571-175-507-571-517-571-150-517-571-517-507-157-751 517-517-571-517-517-517-150-157-150-517-5_V.mp3)
  Segment 44: 71368ms ~ 71944ms (file: 736-756-756-752 362-657-652-167-135-135-157-716-175-175-756-752-751-756-756-752-753-751-756-751-751-563-562-516-657-167-167-126-123-137-317-132-137-316-163-316-765-715-753-753-7_W.mp3)
  Segment 45: 72944ms ~ 73520ms (file: 751-715-751-751-754-751-754-751-571-751-754-571-751-571-751-571-574-571-751-754-751-571-754-574-751-754-751 754-574-751-571-751-751-715-715-715-751-751-715-756-756-756-751-751-7_W.mp3)
  Segment 46: 74520ms ~ 75096ms (file: 567-657-576-657-657-567 571-570-571-570-571-750-751-157-751-751-751-710-175-715-175-751-715-175-710-751-175-751-571 574-574-574-571-517-570-571-517-574-571-570-571-570-574 571-5_W.mp3)
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
> **Time**: 2026-04-05 14:42:43
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
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
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 14:42:43*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=11864  time=14:40:53  uptime=38674s  mem=299MB  system_processes=354"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_144035.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 105-571-501-571-517-517-570-570-150 150-150-501-150-510-157-150-517-157-150-157-517-157-510-157-517 510-510-510-517-513-150-517-150-153-513-150-150-510-510-150-150-150-150-150-5_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 751-571-571-571-750 571-570-574-570-570-571-571-751-517-751-574-571-751-175-751-571-751-157-751-751-751-751-715-751-751-715-751-751-751 571-574-571-754-157-751-715-571-571-750-7_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 751-735-175-715-657-672-751 170-175-715-751-751-175-751-751-715-157-715-715-571-751-513-675-756-573-765-175-756-751-751 170-571-701-751-571-751-715-751-751-751-715-715-751-715-7_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 517-571-574-570-571-175-751-751-715-170-751-715-715-571-751-571-751-571-517-571-571-751-571-571 547-571-570-570-570-570-751-715-710-715-175-710-710-751-715-715-715-571-571-751-7_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 570-574-574-751-754-751-754-715-751-756-571-574-571-751-571-574-574-574-574-754-574-574_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 571-157-501-150-510-510-517-105-530-571-157-571-517-157-751-571-517-157-517-517-517-157-571-571-517-571-157-570 157-715-170-715-175-715-710-751-715-751-751-756-753-756-765-765-7_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 175-571-570-751-751-715-751-715-751-750-756-751-753-576-571-576-576-571-571 574-574-574-574-574-574-574-574-574-754-756-751-574-571-751_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 751-571-751-157-715-173-571-751-571-672-756-765-765 574-750-574-517-751-571-571-571-715-751-751-175-157-157-175-751-715-175-517-751-715-751-751-570 571-517-517-157-750-175-157-1_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 162-162-671-162-267-621-627-756-756-753-756-756-570-751-517-571-571-751-175-570-175-175-751-157-751-576-756-765-567-567-567-576-576-576-571-756-756-751-571-571-571-571-570 507-5_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 571-571-571-751-517-175-715-157-751-751-715-571-751-715-751-137-170_V.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 150-157-715-157-571-570-570-507-750-751-517-570-517-507-570-571-571 157-517-157-517-150-571-157-517-157-715-157-517-517-175-751-715-751-157-175-571-751-751-701-507-571-157 157-5_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 175-517-571 105-157-105-175-157-105-170-107-170-130-175-153-105-137-137-137-713-571 105-157-150-153-157-715-150-157-750-715-175-173-175-175-157-175-751-571-175-175-157-751 157-1_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 574-754-754-754-751-751-715-751-751-751-751-751-751-751-751 574-574-751-751-715-751-751-751-571-571-571-751-753-571-751-571-571-571-571 574-574-571-754-754-751-751-715-751-751-7_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 371-317-135-152-652-657-561-512 671-632-627-621-617-617-167-167-612-627-762 135-326-263-627-627-627-623-672-621-367 173-173-175-135-175-713-173-175-173-173-751-756-137-175 571-5_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 175-715-713-170-710-715-751-175-751-107-715-175-175-637-756 751-571-517-751-570-173-751-175-750-710-705-170-701-571-675-623-652-567-657-657-657-657-576-567-756 571-571-571-571-7_V.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 315-351-765-325-567-562-567-576-267 751-571-751-715-571-175-751-756-756-376-712-273-627-567-251-215-621-627-627-627-672-571-537-751-715-157-751-571-751-715-715-751-175-175-157-7_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 751-517-751-517-175-751-751-175-751-754-751-571-571-754-571-571-574 574-574-571-571-571-571-574-751-751-751-751-571-571-571-574-571-571-751-751 574-574-574-517-517-754-754-751-7_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 157-157-175-157-517-157-751-517-157-517-571-715-751-150-715-175-571-751-715-517-570-715-751-517-751-571 157-517-517-153-517-157-157-751-571-517-157-570-715-570-751-175-517-751-7_V.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 571-571-571-751-715-175-571-517-175-571-517-571-517-157-510-157-150-571-517-510-517-571 571-517-751-715-715-517-175-157-571-170-150-175-570-571-517-175-157-715-175-751-175-157-5_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 574-574-574-574-574-574-574-574-574-574-574-574-574-574-574-571-574 157-510-571-574-571-574-574-751-574-574-574-157-571-570-571-751-751-175-157-157 574-574-517-517-571-547-574-5_W.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 517-570-751-517-571-570-517-570-501-510-571 157-571-517-157-517-150-510-571-510-705-750-175-715-715-175-571-157-517-517-157-517-157-517 157-571-157-170-157-150-150-517-570-751-7_V.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 574-574-574 574-574-574-754-574-574-571-574-571-715-571-751-571-751-751-751-756-756-751 574-574-574-571-754-571-574-751-574-751-754-754-574 574-574-574-574-571-751-754-571-751-7_W.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 135-571-571-175-173-175-563-571-572-527-572-576-573-573-573-573-571-576-572-572-571-576-576-576-751-257-753 517-175-571-751-751-751-571-751-715-715-715-753-175-715-715-751-175-7_V.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 571-751 157-157-175-157-175-157-715-175-751-715-751-715-175-715-571-175-751-175-753-715-157-150-317-510-150-157-751-751 571-517-510-157-150-175-135-170-175-175-175-175-715-175-1_V.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 672-321-157 751-576-157-751-715-715-715-751-715-751-715-751-756-570-751-571-751-756-756-756-576-756-576-576-756 571-571-751-157-750-751-710-715-175-175-715-750-750-750-571-751-7_W.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 567-756-671-675-657-651-625 715-751-751-571-175-751-173-715-571-571-574-751-574-571-754-756-576-576-756-756-576 751-570-750-571-715-751-715-170-751-576-576-751-715-571-710-751-7_W.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 175-175-175-571-517-571-571-574-571 570-571-751-571-751-157-751-507-715-715-157-170-715-751-170-175-705-175-751-157-561-653-350-326-356-605-652-652-567 571-570-751-157-157-571-5_V.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 572-572 751-571-175-751-571-751-715-175-751-754-571-571-571-571-571-571-573-571-573-570-570-570-570-571-572-571-753 571-571-574-571-751-751-715-751-754-571-751-715-751-715-157-5_W.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 756-712-725-715-751-571-751-715-751-753-751 574-574-754-752-754-571-751-175-715-751-715-170-715-175-715-715-751-715-713-751-751-175-715-715-715-715-750 157-170-715-751-715-751-7_W.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 751-570-574-574-571-715-157-715-751-751 574-754-571-574-571-751-571-751-571-175-571-570-751-751-753-751-715-751-750-751-751 570-571-751-570-571-571-751-751-517-750-751-715-751-1_W.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 567-576-571-567-576-576-263-275-726-271-716 571-751-571-715-750-571-576-265-651-657-625-167-137-726-752-572-715-175-507-570-510-516-571 157-571-751-571-571-571-571-157-751-175-1_W.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 751-751-715-751-715-751-751-751-753-571-576-756-756 571-570-571-570-510-750-751-715-751-571-517-170-170-710-175-107-710-170-175-750-750-750-750-751-750-715-175-750-751-751 517-5_V.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 547-540-547-574-150-517-517-517-571-517-517-571-571 571-571-157-517-157-517-671-671-612-617 571-571-571-750-175-750-751-175-751-715-751-571-150-750-751-571-517 517-573-751-751-5_V.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 371-357-175-751 153-510-175-517-157-157-150-175-157-157-175-751-175-175-175-351-751-175-175-173-157-175-715-157-157-517 150-510-157-150-157-715-105-175-135-750-150-715-137-173-1_V.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 571 570-571-751-571-750-751-751-574-751-571-751-756-756-751-571-574-574-574-574-574-574-574 574-570-574-574-574-751-750-570-571-570-754-754-750-517-574-751-571-751-751-571-751-7_W.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 570-751-751-751-751-571-574-574-571-574-571-574-574-574-574 574-570-750-571-517-571-574-571-751-753-751-751-751-751-751-175-175-571-576-576-576-573-576-576-752-576-756-573 574-5_W.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 507-150-150-571-751 715-517-157-571-175-517-715-510-517-751-175-175-175-715-571-715-751-751-157-517-571-571-571-570-571-517 157-157-150-157-150-571-157-571-157-571-157-571-517-5_V.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 175-751-751-715-751-751-635-516-372-275-267-527-723-627-762-725-237-756 751-715-751-173-715-715-751-715-751-175-751-751-751-367-756-567-576-756-175-735-756-653-675-672-576-756-7_V.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 571-570-510-570-571-571-576-571-571-517 510-157-157-157-517-175-150-157-157-175-157-571-175-507-571-517-571-150-517-571-517-507-157-751 517-517-571-517-517-517-150-157-150-517-5_V.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 517-157-157-517-157-157-157-571-571-517-517-517-571-157-571-157-571-571-751-517-751-571 157-157-517-517-157-517-571-517-571-517-175-715-517-517-571-150-517-751-517-571-507-570-5_V.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 517-507-571-571-751-571-517-517-517-715 574-574-571-517-157-157-571-751-750-570-571-517-571-570-571 574-574-571-571-571-570-510-571-571-570-571-570-517-517-571-571-571-571-571 5_V.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 751-715-756-173-175-173-153-157-571_W.mp3)
  Segment 43: 66192ms ~ 66768ms (file: 672-652 613-627-267-627-627-621 571-517-157-571-751-715-175-571-170-715-175-710-175-170-715-710-751-175-710-170-150-175-157-157-576-517-536-365-563-563-356-563-753 571-751-750-7_V.mp3)
  Segment 44: 67768ms ~ 68344ms (file: 571-751-751-571-751-751-751 574-574-574-571-754-571-574-570-571-751-571-574-751-751-751-175-715-751-751-751-751-175-751-175-571-571-751-754-751-571 574-754-574-574-574-574-754-5_W.mp3)
  Segment 45: 69344ms ~ 69920ms (file: 517-150-150-150-157-150-157-517-571-175-157-571-517-157-517-517-570 510-150-510-157-150-157-150-157-517-517-157-150-157-157-175-715-175-571-751-157 517-513-517-510-157-150-510-1_W.mp3)
  Segment 46: 70920ms ~ 71496ms (file: 157-175-731-175-175-710-175-571-756-750-756-574-756-756-574-576 574-571-751-751-715-517-753-175-135-170-175-175-157-175-175-175-751-715-571-175-571-751-571-751-754 574-517-715-5_W.mp3)
  Segment 47: 72496ms ~ 73072ms (file: 751-751-751-751-751-751-571-571-571-754 754-570-751-751-751-754-751-751-754-754-751-571-574-574-574-574-754-751-751-751-751-754-754-754-754-574-754-754 750-175-175-751-751-751-7_W.mp3)
  Segment 48: 74072ms ~ 74648ms (file: 175-571-175-751-751-517-571-175-571-573-571-517-517-571-571-574-571-571-517-571-517-571-571-517 751-175-751-173-721-731-173-761-176-376-736-371-713-716-713-371-731-173-173 173-1_W.mp3)
  Segment 49: 75648ms ~ 76224ms (file: 175-710-751-571-751-751-715-751-715-710-750-715-765-735-705-752-751-570-571-571-573-570-570-576 571-751-715-750-751-710-170-751-135-375-713-375-175-570-571-750-753-751-576-165_V.mp3)
  Segment 50: 77224ms ~ 77800ms (file: 751-574-574-574-574-574-754-754-571-756-754-574-574 574-574-574-570-574-574-571-574-754-571-571-571-574-571-751-751-751-751-571-573 574-574-574-574-574-574-517-517-517-517-517-5_W.mp3)
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
> **Time**: 2026-04-05 14:42:43
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
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
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 14:42:43*
