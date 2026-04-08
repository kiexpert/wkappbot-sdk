# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=28596  time=09:23:33  uptime=19895s  mem=184MB  system_processes=406"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_092322.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 573-573-576-572-517-517-351-531-573-153-153-517-531-351-315_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 157-517-157-175-571-105-175-170-175-173-716 150-157-137-127-176-157-157-157-516-510-517 107-571-156-157-157-107-175-750-750-701-105 501-150-571-512-517-150-517_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 153-135 132-315-137-312-135-137-312_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 153-105-105-510-150-517-157-105-157-157-571-510-157-517-510-105-517-510-150-570-105-105-175 105-105-105-150-105-105-105-150-150-105-157-150-105-107-105-105-150-157 105-517-105-1_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 514-517-513-153-153-150 510-571-507-574-517-513-517-751-157-517-517-751-751-370-513-537-735-751-173-715-375-713-137 236-132-136-136-163-136-176-735-716-763-176-176-175-756-567-5_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 701-176-175-710-107-175-710-170-175-715-170-170-715-175-762-106-610-230_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 524-534-574-576-576-547-574-514-572-573-571-573-571-572-571-574-570-571 570-571-517-510-510-517_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 105-105-105-507-510-517 157-150-150-150-150-517-150-517-570-150-150_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 175-157 574-570-570-570-570-570-570-571-571-571-571-571-571-570-576-572_W.mp3)
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
> **Time**: 2026-04-06 09:24:17
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": [
{
"word": "(music)",
"start_ms": 0,
"dur_ms": 576,
"phase_lr_deg": 0.0
}
]
},
{
"segment": 2,
"transcript": "제 이름은 지민입니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{
"word": "제",
"start_ms": 50,
"dur_ms": 110,
"phase_lr_deg": 0.0
},
{
"word": "이름은",
"start_ms": 165,
"dur_ms": 205,
"phase_lr_deg": 0.0
},
{
"word": "지민입니다",
"start_ms": 380,
"dur_ms": 196,
"phase_lr_deg": 0.0
}
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
"words": [
{
"word": "(noise)",
"start_ms": 0,
"dur_ms": 576,
"phase_lr_deg": 0.0
}
]
},
{
"segment": 4,
"transcript": "안녕하세요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{
"word": "안녕하세요",
"start_ms": 45,
"dur_ms": 531,
"phase_lr_deg": 0.0
}
]
},
{
"segment": 5,
"transcript": "도와주셔서 감사합니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{
"word": "도와주셔서",
"start_ms": 40,
"dur_ms": 290,
"phase_lr_deg": 0.0
},
{
"word": "감사합니다",
"start_ms": 335,
"dur_ms": 241,
"phase_lr_deg": 0.0
}
]
},
{
"segment": 6,
"transcript": "어디에서 오셨어요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 42,
"audio_type": "speech",
"words": [
{
"word": "어디에서",
"start_ms": 42,
"dur_ms": 240,
"phase_lr_deg": -3.5
},
{
"word": "오셨어요?",
"start_ms": 285,
"dur_ms": 291,
"phase_lr_deg": -3.5
}
]
},
{
"segment": 7,
"transcript": "그것은 사과입니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{
"word": "그것은",
"start_ms": 55,
"dur_ms": 190,
"phase_lr_deg": 0.0
},
{
"word": "사과입니다",
"start_ms": 245,
"dur_ms": 331,
"phase_lr_deg": 0.0
}
]
},
{
"segment": 8,
"transcript": "만나서 반가워요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{
"word": "만나서",
"start_ms": 40,
"dur_ms": 225,
"phase_lr_deg": 0.0
},
{
"word": "반가워요",
"start_ms": 265,
"dur_ms": 311,
"phase_lr_deg": 0.0
}
]
},
{
"segment": 9,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": [
{
"word": "(instrument)",
"start_ms": 0,
"dur_ms": 576,
"phase_lr_deg": 0.0
}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 09:24:17*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=18336  time=09:24:21  uptime=20383s  mem=436MB  system_processes=406"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_092404.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 514-517-513-153-153-150 510-571-507-574-517-513-517-751-157-517-517-751-751-370-513-537-735-751-173-715-375-713-137 236-132-136-136-163-136-176-735-716-763-176-176-175-756-567-5_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 175-157 574-570-570-570-570-570-570-571-571-571-571-571-571-570-576-572_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 153-135 132-315-137-312-135-137-312_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 701-176-175-710-107-175-710-170-175-715-170-170-715-175-762-106-610-230_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 105-105-105-507-510-517 157-150-150-150-150-517-150-517-570-150-150_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 573-573-576-572-517-517-351-531-573-153-153-517-531-351-315_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 524-534-574-576-576-547-574-514-572-573-571-573-571-572-571-574-570-571 570-571-517-510-510-517_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 157-517-157-175-571-105-175-170-175-173-716 150-157-137-127-176-157-157-157-516-510-517 107-571-156-157-157-107-175-750-750-701-105 501-150-571-512-517-150-517_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 517-537-513 576-513-517-153-350-513-751-613-123-257-572-651-751-765-753-752-735-673-257-521-253-527-517-572-513-715-716-765-716-713-175-173-753-756-753-576-756-765-752-571-752-7_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 756-765-756-751-731-175-731-715-716-765-576-675-756-765-765-153-315-135-137-317-135-517-135-135-175-715-125-217-317-125-137-153-135-135 571-571-517-517-576-517-531-513-153-571-5_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 153-105-105-510-150-517-157-105-157-157-571-510-157-517-510-105-517-510-150-570-105-105-175 105-105-105-150-105-105-105-150-150-105-157-150-105-107-105-105-150-157 105-517-105-1_V.mp3)
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
> **Time**: 2026-04-06 09:24:47
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "도와주셔서 감사합니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 38,
"audio_type": "speech",
"words": [
{"word": "도와주셔서", "start_ms": 38, "dur_ms": 290, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 333, "dur_ms": 243, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": [
{"word": "(instrument)", "start_ms": 0, "dur_ms": 576, "phase_lr_deg": 0.0}
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
"words": [
{"word": "(noise)", "start_ms": 0, "dur_ms": 576, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "어디에서 오셨어요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 42,
"audio_type": "speech",
"words": [
{"word": "어디에서", "start_ms": 42, "dur_ms": 240, "phase_lr_deg": -3.5},
{"word": "오셨어요?", "start_ms": 282, "dur_ms": 294, "phase_lr_deg": -3.5}
]
},
{
"segment": 5,
"transcript": "만나서 반가워요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "만나서", "start_ms": 40, "dur_ms": 225, "phase_lr_deg": 0.0},
{"word": "반가워요", "start_ms": 265, "dur_ms": 311, "phase_lr_deg": 0.0}
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
"words": [
{"word": "(music)", "start_ms": 0, "dur_ms": 576, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "그것은 사과입니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "그것은", "start_ms": 55, "dur_ms": 190, "phase_lr_deg": 0.0},
{"word": "사과입니다", "start_ms": 245, "dur_ms": 331, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "제 이름은 지민입니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "제", "start_ms": 50, "dur_ms": 115, "phase_lr_deg": 0.0},
{"word": "이름은", "start_ms": 165, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "지민입니다", "start_ms": 375, "dur_ms": 201, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "한국어를 공부하고 있어요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 52,
"audio_type": "speech",
"words": [
{"word": "한국어를", "start_ms": 52, "dur_ms": 215, "phase_lr_deg": 0.0},
{"word": "공부하고", "start_ms": 267, "dur_ms": 195, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 462, "dur_ms": 114, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "저는 미국에서 왔어요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 65, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "미국에서", "start_ms": 225, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "왔어요", "start_ms": 445, "dur_ms": 131, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "안녕하세요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 45, "dur_ms": 531, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 09:24:47*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=31656  time=09:24:22  uptime=20271s  mem=428MB  system_processes=406"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_092357.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 153-105-105-510-150-517-157-105-157-157-571-510-157-517-510-105-517-510-150-570-105-105-175 105-105-105-150-105-105-105-150-150-105-157-150-105-107-105-105-150-157 105-517-105-1_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 524-534-574-576-576-547-574-514-572-573-571-573-571-572-571-574-570-571 570-571-517-510-510-517_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 514-517-513-153-153-150 510-571-507-574-517-513-517-751-157-517-517-751-751-370-513-537-735-751-173-715-375-713-137 236-132-136-136-163-136-176-735-716-763-176-176-175-756-567-5_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 701-176-175-710-107-175-710-170-175-715-170-170-715-175-762-106-610-230_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 175-157 574-570-570-570-570-570-570-571-571-571-571-571-571-570-576-572_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 157-517-157-175-571-105-175-170-175-173-716 150-157-137-127-176-157-157-157-516-510-517 107-571-156-157-157-107-175-750-750-701-105 501-150-571-512-517-150-517_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 573-573-576-572-517-517-351-531-573-153-153-517-531-351-315_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 517-537-513 576-513-517-153-350-513-751-613-123-257-572-651-751-765-753-752-735-673-257-521-253-527-517-572-513-715-716-765-716-713-175-173-753-756-753-576-756-765-752-571-752-7_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 153-135 132-315-137-312-135-137-312_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 105-105-105-507-510-517 157-150-150-150-150-517-150-517-570-150-150_W.mp3)
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
> **Time**: 2026-04-06 09:24:59
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "안녕하세요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 45, "dur_ms": 531, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "그것은 사과입니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "그것은", "start_ms": 55, "dur_ms": 190, "phase_lr_deg": 0.0},
{"word": "사과입니다", "start_ms": 245, "dur_ms": 331, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "도와주셔서 감사합니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "도와주셔서", "start_ms": 40, "dur_ms": 290, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 335, "dur_ms": 241, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "어디에서 오셨어요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 42,
"audio_type": "speech",
"words": [
{"word": "어디에서", "start_ms": 42, "dur_ms": 240, "phase_lr_deg": -3.5},
{"word": "오셨어요?", "start_ms": 285, "dur_ms": 291, "phase_lr_deg": -3.5}
]
},
{
"segment": 5,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": [
{"word": "(instrument)", "start_ms": 0, "dur_ms": 576, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "제 이름은 지민입니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "제", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "이름은", "start_ms": 165, "dur_ms": 205, "phase_lr_deg": 0.0},
{"word": "지민입니다", "start_ms": 380, "dur_ms": 196, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": [
{"word": "(music)", "start_ms": 0, "dur_ms": 576, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "한국어를 공부하고 있어요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 52,
"audio_type": "speech",
"words": [
{"word": "한국어를", "start_ms": 52, "dur_ms": 215, "phase_lr_deg": 0.0},
{"word": "공부하고", "start_ms": 267, "dur_ms": 195, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 462, "dur_ms": 114, "phase_lr_deg": 0.0}
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
"words": [
{"word": "(noise)", "start_ms": 0, "dur_ms": 576, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "만나서 반가워요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "만나서", "start_ms": 40, "dur_ms": 225, "phase_lr_deg": 0.0},
{"word": "반가워요", "start_ms": 265, "dur_ms": 311, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 09:24:59*
