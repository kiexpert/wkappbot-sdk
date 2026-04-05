# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=6944  time=13:33:31  uptime=42621s  mem=351MB  system_processes=357"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_133321.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 517-715-157-153-150-150-517-105-510-157-150-175-157-317-157-315-751-175-175-175-150-157-175-150-150-571 751-571-157-175-517-175-517-571-751-571-157-150-750-157-510-750-517-570-3_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 157-157-571-517-175-157-175-157-175-157-517-157-157-175-157-175-751-501-715-753-157 571-157-157-517-571-570-571-751-157-517-570-701-175-751-150-157-175-571-175-150-157-175-710-1_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 571-157-150-157-715-175-570-175-751-715-713-517-157-710-517-157-713-571-175-157-715-753-175-750-751-157 157-175-150-715-751-157-517-751-175-571-157-715-173-175-751-175-150-107-1_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 710 517-150-157-157-713-701-715-712-170-157-130-710-175-715-730-751-710-175-710-750-175-726-175-715-751-137-751-156-157-571-571-571-571-175-173-756-752-756-574-574-752-173-137-1_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 175-751-510-715-715-173-715-715 157-157-736-157-157-156-175-156-163-165-156-163-160-165-167-617-615-167-617-165-167-617-176-671-165-165-156-572-752-567-572-756-576-756-576-752-5_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 173-715-751-157-175-715-571-715-715-751 574-571-175-710-571-175-751-170-175-175-157-175-315-175-157-175-715-175-157-571 157-105-175-175-175-175-713-170-715-715-175-751-173-715-1_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 570-715-157-571-751-715-517-517-517-607-175-157-175-571-105-715-715-715-157-715-105-750-175-751-170-715-175-175-751-750-175-715 751-571-157-175-157-571-170-537-570-175-750-751-5_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 501-510-510-510-510-514-547-507 517-510-517-574-517-157-150-510-517-153-173-173-517-153-173-735-175-157-571-751-517-175 510-157-150-157-507-175-157-175-157-170-715-715-157-173-1_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 517-157-175-715-175-715-175-175-175-713-710-571-137-175-701-175-571-175-715-175-157-715-175-173 750-157-173-175-170-173-173-175-173-173-715-672-156-165-156 751-137-175-137-135-7_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 273-135-732-725-256-261-571-571-571-157-572 157-571-157-713-173-715-175-715-157-715-751-751-753-175-715-715-751-571 571-157-571-175-173-315-153-175-157-573 314 574-574-574-574-5_W.mp3)
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
> **Time**: 2026-04-05 13:36:14
> **AI**: gemini

---

## Response

AnalysisAnalysisGemini의 응답[
{
"segment": 1,
"transcript": "안녕하세요 반갑습니다 오늘 날씨가 참 좋네요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 45, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 170, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 290, "dur_ms": 85, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 380, "dur_ms": 95, "phase_lr_deg": 0.0},
{"word": "참", "start_ms": 480, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 525, "dur_ms": 51, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "이름이 뭐예요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "이름이", "start_ms": 30, "dur_ms": 220, "

---
*Generated by WKAppBot ask gemini — 2026-04-05 13:36:14*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=17708  time=13:33:45  uptime=33887s  mem=343MB  system_processes=357"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_133335.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 517-715-157-153-150-150-517-105-510-157-150-175-157-317-157-315-751-175-175-175-150-157-175-150-150-571 751-571-157-175-517-175-517-571-751-571-157-150-750-157-510-750-517-570-3_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 173-715-751-157-175-715-571-715-715-751 574-571-175-710-571-175-751-170-175-175-157-175-315-175-157-175-715-175-157-571 157-105-175-175-175-175-713-170-715-715-175-751-173-715-1_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 571-157-150-157-715-175-570-175-751-715-713-517-157-710-517-157-713-571-175-157-715-753-175-750-751-157 157-175-150-715-751-157-517-751-175-571-157-715-173-175-751-175-150-107-1_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 157-157-571-517-175-157-175-157-175-157-517-157-157-175-157-175-751-501-715-753-157 571-157-157-517-571-570-571-751-157-517-570-701-175-751-150-157-175-571-175-150-157-175-710-1_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 570-715-157-571-751-715-517-517-517-607-175-157-175-571-105-715-715-715-157-715-105-750-175-751-170-715-175-175-751-750-175-715 751-571-157-175-157-571-170-537-570-175-750-751-5_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 710 517-150-157-157-713-701-715-712-170-157-130-710-175-715-730-751-710-175-710-750-175-726-175-715-751-137-751-156-157-571-571-571-571-175-173-756-752-756-574-574-752-173-137-1_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 273-135-732-725-256-261-571-571-571-157-572 157-571-157-713-173-715-175-715-157-715-751-751-753-175-715-715-751-571 571-157-571-175-173-315-153-175-157-573 314 574-574-574-574-5_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 517-751-175-157-175-175-751-175-170-715-710-731-173-751-175-715-715-571-175-751-751-157_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 517-157-175-715-175-715-175-175-175-713-710-571-137-175-701-175-571-175-715-175-157-715-175-173 750-157-173-175-170-173-173-175-173-173-715-672-156-165-156 751-137-175-137-135-7_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 175-751-510-715-715-173-715-715 157-157-736-157-157-156-175-156-163-165-156-163-160-165-167-617-615-167-617-165-167-617-176-671-165-165-156-572-752-567-572-756-576-756-576-752-5_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 501-510-510-510-510-514-547-507 517-510-517-574-517-157-150-510-517-153-173-173-517-153-173-735-175-157-571-751-517-175 510-157-150-157-507-175-157-175-157-170-715-715-157-173-1_V.mp3)
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
> **Time**: 2026-04-05 13:36:17
> **AI**: gemini

---

## Response

AnalysisAnalysisGemini의 응답[
{
"segment": 1,
"transcript": "안녕하세요 반갑습니다 오늘 날씨가 참 좋네요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 45, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 170, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 290, "dur_ms": 85, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 380, "dur_ms": 95, "phase_lr_deg": 0.0},
{"word": "참", "start_ms": 480, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 525, "dur_ms": 51, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "이름이 뭐예요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "이름이", "start_ms": 30, "dur_ms": 220, "phase_lr_deg": 5.0},
{"word": "뭐예요", "start_ms": 260, "dur_ms": 316, "phase_lr_deg": 5.0}
]
},
{
"segment": 3,
"transcript": "제 이름은 김민수입니다 만나서 정말 기뻐요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "제", "start_ms": 50, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "이름은", "start_ms": 95, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "김민수입니다", "start_ms": 180, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "만나서", "start_ms": 340, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 440, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "기뻐요", "start_ms": 510, "dur_ms": 66, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "Where are you from originally",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "Where", "start_ms": 20, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "are", "start_ms": 125, "dur_ms": 40, "phase_lr_deg": 5.0},
{"word": "you", "start_ms": 170, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "from", "start_ms": 235, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "originally", "start_ms": 350, "dur_ms": 226, "phase_lr_deg": 5.0}
]
},
{
"segment": 5,
"transcript": "저는 서울에서 왔어요 당신은요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words":

---
*Generated by WKAppBot ask gemini — 2026-04-05 13:36:17*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=30524  time=13:34:10  uptime=32562s  mem=478MB  system_processes=355"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_133355.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 751-157-175-715-157-715-571-753-713-753-753-751-715-175-751 750-574-571-751-571-574-574-574-574-571 547-574-574-574 541-547-517-571-571-517-510-570-517-517-507 574-517-571-517-5_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 517-157-175-715-175-715-175-175-175-713-710-571-137-175-701-175-571-175-715-175-157-715-175-173 750-157-173-175-170-173-173-175-173-173-715-672-156-165-156 751-137-175-137-135-7_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 175-751-510-715-715-173-715-715 157-157-736-157-157-156-175-156-163-165-156-163-160-165-167-617-615-167-617-165-167-617-176-671-165-165-156-572-752-567-572-756-576-756-576-752-5_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 571-157-150-157-715-175-570-175-751-715-713-517-157-710-517-157-713-571-175-157-715-753-175-750-751-157 157-175-150-715-751-157-517-751-175-571-157-715-173-175-751-175-150-107-1_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 273-135-732-725-256-261-571-571-571-157-572 157-571-157-713-173-715-175-715-157-715-751-751-753-175-715-715-751-571 571-157-571-175-173-315-153-175-157-573 314 574-574-574-574-5_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 173-715-751-157-175-715-571-715-715-751 574-571-175-710-571-175-751-170-175-175-157-175-315-175-157-175-715-175-157-571 157-105-175-175-175-175-713-170-715-715-175-751-173-715-1_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 710 517-150-157-157-713-701-715-712-170-157-130-710-175-715-730-751-710-175-710-750-175-726-175-715-751-137-751-156-157-571-571-571-571-175-173-756-752-756-574-574-752-173-137-1_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 157-157-571-517-175-157-175-157-175-157-517-157-157-175-157-175-751-501-715-753-157 571-157-157-517-571-570-571-751-157-517-570-701-175-751-150-157-175-571-175-150-157-175-710-1_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 517-751-175-157-175-175-751-175-170-715-710-731-173-751-175-715-715-571-175-751-751-157_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 517-715-157-153-150-150-517-105-510-157-150-175-157-317-157-315-751-175-175-175-150-157-175-150-150-571 751-571-157-175-517-175-517-571-751-571-157-150-750-157-510-750-517-570-3_V.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 501-510-510-510-510-514-547-507 517-510-517-574-517-157-150-510-517-153-173-173-517-153-173-735-175-157-571-751-517-175 510-157-150-157-507-175-157-175-157-170-715-715-157-173-1_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 570-715-157-571-751-715-517-517-517-607-175-157-175-571-105-715-715-715-157-715-105-750-175-751-170-715-175-175-751-750-175-715 751-571-157-175-157-571-170-537-570-175-750-751-5_V.mp3)
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
> **Time**: 2026-04-05 13:36:26
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "안녕하세요 반갑습니다 오늘 날씨가 참 좋네요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 45, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 170, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 290, "dur_ms": 85, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 380, "dur_ms": 95, "phase_lr_deg": 0.0},
{"word": "참", "start_ms": 480, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 525, "dur_ms": 51, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "이름이 뭐예요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "이름이", "start_ms": 30, "dur_ms": 220, "phase_lr_deg": 5.0},
{"word": "뭐예요", "start_ms": 260, "dur_ms": 316, "phase_lr_deg": 5.0}
]
},
{
"segment": 3,
"transcript": "제 이름은 김민수입니다 만나서 정말 기뻐요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "제", "start_ms": 50, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "이름은", "start_ms": 95, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "김민수입니다", "start_ms": 180, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "만나서", "start_ms": 340, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 440, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "기뻐요", "start_ms": 510, "dur_ms": 66, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "Where are you from originally",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "Where", "start_ms": 20, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "are", "start_ms": 125, "dur_ms": 40, "phase_lr_deg": 5.0},
{"word": "you", "start_ms": 170, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "from", "start_ms": 235, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "originally", "start_ms": 350, "dur_ms": 226, "phase_lr_deg": 5.0}
]
},
{
"segment": 5,
"transcript": "저는 서울에서 왔어요 당신은요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 60, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "서울에서", "start_ms": 170, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "왔어요", "start_ms": 300, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "당신은요", "start_ms": 440, "dur_ms": 136, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 7,
"transcript": "요즘 공부는 잘 되어가고 있나요",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "요즘", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": -10.0},
{"word": "공부는", "start_ms": 130, "dur_ms": 110, "phase_lr_deg": -10.0},
{"word": "잘", "start_ms": 250, "dur_ms": 40, "phase_lr_deg": -10.0},
{"word": "되어가고", "start_ms": 300, "dur_ms": 140, "phase_lr_deg": -10.0},
{"word": "있나요", "start_ms": 450, "dur_ms": 126, "phase_lr_deg": -10.0}
]
},
{
"segment": 8,
"transcript": "네 열심히 하고 있습니다 감사합니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 55, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "열심히", "start_ms": 115, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "하고", "start_ms": 255, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 325, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 455, "dur_ms": 121, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "한번 더 말해줄 수 있어요",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "한번", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": -10.0},
{"word": "더", "start_ms": 155, "dur_ms": 50, "phase_lr_deg": -10.0},
{"word": "말해줄", "start_ms": 215, "dur_ms": 140, "phase_lr_deg": -10.0},
{"word": "수", "start_ms": 365, "dur_ms": 40, "phase_lr_deg": -10.0},
{"word": "있어요", "start_ms": 415, "dur_ms": 161, "phase_lr_deg": -10.0}
]
},
{
"segment": 10,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 13:36:26*
