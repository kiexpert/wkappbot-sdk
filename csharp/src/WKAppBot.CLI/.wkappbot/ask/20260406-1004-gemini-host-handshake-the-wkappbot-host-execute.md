# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=27304  time=10:03:48  uptime=19804s  mem=426MB  system_processes=416"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_100337.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 152-156-513-156-517-156-135-153-513-153-153_V.mp3)
  Segment 2: 1576ms ~ 7984ms (file: 576-576-753-751-571-576-576-756-751-756-176-167-175-106-165-157-105-160-105-165-327-317-325-317-567-735-756-573-157-756-753-103-106-105-175-175-157-571-756-571-756-536-576 756-7_W.mp3)
  Segment 3: 8984ms ~ 9560ms (file: 127-215-157-312-312-325-312-231-352-325-357-537-532-352-354-351-150-150-135-150-150-513-150-152-105-150 105-105-105-517-105-150-517-150-105-105-501-105-105-105-150-513-105-153-1_V.mp3)
  Segment 4: 10560ms ~ 11136ms (file: 317-517-517-517-517-571-517-571-576-103-135-570-576-153-513-571-756-571-573-571 105-510-510-517-510-150-105-150-150-510-150-150-501-150-501-501-156_W.mp3)
  Segment 5: 12136ms ~ 12712ms (file: 156-160-165-165-165-165-165-572-572-572-572-572-572-573-572-572-573-572-572-572-576-175-123-126-107-156-163-160-163-150-160-130-135-130-375-571-573-572-576-576-572-574-572-572-5_W.mp3)
  Segment 6: 13712ms ~ 14288ms (file: 103-103-325-235-125-315-325-215-351-352-352-531-537-523-523-523-527-532-512-352-123-132-125-125-157-136-156-165-163-163-167-165-165-156-165-651-165-175-156-153-157-153-517-157-5_W.mp3)
  Segment 7: 15288ms ~ 15864ms (file: 514-105-156-501-150-150-516-156-105-156-105-501-156-513-105-105-105-156-105-156-105-153 105-105-517-154-150-514-153-156-105-153-153-152-105-105-154-514-517-153-105-105-156-105-5_V.mp3)
  Segment 8: 16864ms ~ 17440ms (file: 501-153-157-517-516-105-157-157-517-315-573-576-517-157-130-135-152-125-102-213-210-120-102-150-135-120-132-132-127-173-137-175-123-132-125-132-173-352-537-573-537-573-537-537-3_W.mp3)
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
> **Time**: 2026-04-06 10:04:28
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "반갑습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "반갑습니다", "start_ms": 35, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 3,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 4,
"transcript": "따라해 보세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "따라해", "start_ms": 40, "dur_ms": 210, "phase_lr_deg": 5.0},
{"word": "보세요", "start_ms": 260, "dur_ms": 240, "phase_lr_deg": 5.0}
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
"transcript": "Excellent work today",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "Excellent", "start_ms": 70, "dur_ms": 220, "phase_lr_deg": -10.0},
{"word": "work", "start_ms": 300, "dur_ms": 110, "phase_lr_deg": -10.0},
{"word": "today", "start_ms": 420, "dur_ms": 130, "phase_lr_deg": -10.0}
]
},
{
"segment": 7,
"transcript": "내일 만나요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "내일", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 250, "dur_ms": 280, "phase_lr_deg": 0.0}
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
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 10:04:28*
