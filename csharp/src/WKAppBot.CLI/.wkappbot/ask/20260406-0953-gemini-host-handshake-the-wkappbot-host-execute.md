# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=42684  time=09:53:12  uptime=20810s  mem=134MB  system_processes=409"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_095301.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 157-105-105-157-751-107-105-517-105-571-105-157-517-105-501 150-510-150-105-501-105-150-510-501-105-517-157-157-157-517-105-751-107-105-105-517-715-175-715 571-157-157-105-715-5_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 105-150-510-105-517-175-175-105 510-514-541-541-514-154-150-150-510-105-157-105-150-150-105-157-170-103 501-510-150-154-154-150-150-150-157-105-150-150-150-513-157-150-105-103_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 107-710-105-107-150-107-107-103-175-137-175-173-107-715-157-137-173-103-756 517-105-150-157-157-107-105-105-107-105-107-105-107-107-107-107-103-105-153-531 105-105-517-150-150-1_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 105-153-153-105-103-105-103-105-153-105 150-157-157-105-105-105-153-157-105-105-105-175-105-157 105-153-105-105-105-150-510-157-150-175-105-501-107-570-571-105-105-150 510-541-5_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 153-135-153-153-105-153-157-157-105-105-157-105-105-135-105_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 105-510-517-571-107-157-157-150-150-152-105-105-351-367-106-310-315-315-135-153-135-153-153-317-315-357-317-316-132-162-163-162-132-123-123-321-123-321-123-125-213-235-312-325-3_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 150-510-510-751-175-150-517-150-514-514-510-517-157-157-510 514-150-510-157-105-517 150-154-541-105-150-105-150-105-105-150-531-501-105-517-157-517-517-501-571-105-157-517-157-1_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 756-753-517-157-756-317-173-173-751-135-153-513-163-162-261-216-316-167-612-617-162-627-617-372-132-163-137-613-756-752-756-752-756-671-675 572-517-570-571-517-571-507-157-507-7_W.mp3)
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
> **Time**: 2026-04-06 09:53:55
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "안녕하세요 만나서 반갑습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 35, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "만나서", "start_ms": 255, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 405, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "오늘 공부할 내용은 무엇인가요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 42,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 42, "dur_ms": 105, "phase_lr_deg": 0.0},
{"word": "공부할", "start_ms": 155, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "내용은", "start_ms": 295, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "무엇인가요", "start_ms": 425, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 4,
"transcript": "한국어 발음을 연습해 봅시다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "발음을", "start_ms": 200, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "연습해", "start_ms": 330, "dur_ms": 115, "phase_lr_deg": 5.0},
{"word": "봅시다", "start_ms": 455, "dur_ms": 110, "phase_lr_deg": 5.0}
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
"transcript": "천천히 따라 읽어주세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "천천히", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "따라", "start_ms": 190, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "읽어주세요", "start_ms": 310, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "Great job everyone",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "Great", "start_ms": 60, "dur_ms": 120, "phase_lr_deg": -10.0},
{"word": "job", "start_ms": 190, "dur_ms": 100, "phase_lr_deg": -10.0},
{"word": "everyone", "start_ms": 300, "dur_ms": 250, "phase_lr_deg": -10.0}
]
},
{
"segment": 8,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 09:53:55*
