# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=1700  time=13:17:19  uptime=31306s  mem=2188MB  system_processes=359"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_131644.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 751-715 571-157-135-751-571-157-715-571-157-170-157-175-157-750-715-175-715-175-175-751-715-751-751-751-751-715 157-517-173-715-170-127-102-751-175-175-175-751-751-750-175-752-7_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 710-170-715-715-715-175-715-170-170-175-175-175-715 175-517-510-175-175-157-157-175-571-157-751-157-751-157-751-715-175-517-750-710-170-701-751-751-517-571 571-715-715-175_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 175-105-175-170-170-715-175-170-517-175-715-750-751-751-571 107-170-715-750-175-170-175-170-710-170-715 107-175-705-170-175-170-175-751-715-710-170-175-175-751-715-157-715-170-7_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 517-150-157-157-517-157-157-152-157-175-175-175-135-153-137-175-715-157-157 105-157-135-150-153-157-135-150-150-157-150-105-157-132-157-175-175-175-137-137-105-157-173-157 150-1_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 157-517 157-751-715-175-175-170-517-517-517-510-157-175-517-157-150-751-571-175-571-751-157-705-751-715-751-715-751 517-157-157-137-175-715-172-713-173-715-175-715-175-175-153-1_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 157-571-157-175-715-175-175-715-175-157-175-571-715-751-570-157-715-751 157-105-105-150-157-157-157-715-157-175-157-517-517-715-570-751-715-175-170-175-157-512-751-715-701-175-1_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 751-157-517-751-157-752-751-715-715-715-751-715-157-157-175-157 517-571-157-570-571-570-754 715-170-170-170-175-157-175-150-157-175-175-710-170-175-751-715-517-517-173-175-175-1_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 751-175-751-570-751-751 170-157-175-157-175-751-517-571-571-175-751-751-571-753-751-517-571-751-571-570-571-571-570-753-573-571-571-571 517-157-751-517-157-517-751-175-172-751-1_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 753-756-751-751-265-625-621-167-156-175-176-175-175-725-753-752-572-572-572-572-752-752-752-257 175-175-316-153-517-156-517-153-517-571-571-175-517-157-571-751-756-175-753-572-7_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 517-751-705-715-751-517-157-517-751-750-570-751-571-750-715-715-175-751 517-517-715-170-157-157-571-750-750-715-157-157-715-175-715-175-751-751-751-750-751-715-175-751-751-571-7_V.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 105-517-157-751-175-170-715-170-715-751-715-175-157-715-157-751-571-715-715-751-517-710-715-751-751-715-573-751 517-157-751-175-715-175-175-715-157-175-105-517-175-750-175-175-7_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 517-175-175-751-571-175-157-715-751-175-715-175-751-571-715-175-715-150-157-510 157-107-175-170-715-170-157-175-157-715-175-157-175-751-571-157-517-715-175-751-751-751-715-175 1_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 715-715-172-175-175-102-132-107-170-715-135-571-150 107-175-157-157-157-175-157-135-175-157-137-751-517-175-715-710-175-715-157-715-175-175-751-715-173-172-170-730 175-571-157-7_V.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 157-517-571-571 157-157-571-517-751-571-517-571_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 750-751-715-170-135-165-615-167-167-160-176-173-137-163-160-167-175-176-576-153-157-175-167-657-576-135-135-576-573-573-753-316-315-135-157-157-157-165-157-157-175-157-157-156-1_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 517-175-751-175-571-157-175-175-750 715-175-571-170-701-170-175-715-170-751-175-175-715-715-175-705 715-175-751-170-751-715-751-170-751-751-751-715-175-715-751-715-751-157-751-7_V.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 157-571-157-175-517-751-517-175-715-157-157 715-157-751-175-517-157-510-751-157-157-153-107-157-150_V.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 705-715-170-107-710-715-710-710-170-107-170-715-751 175-175-175-157-517-571-157-751-570-570-157-517-751-175-715-751-750-751-571-175-705-175 571-571-571-571-157-570-157-150-705-5_W.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 150-571-150-157-713-751-157-753-751-715-715-751-751-571-750 157-175-157-105-751-715-715-715-751-175-175-715-750-715-751-175-715-715-751-751-571-715-751-715-157-173-170-751-705-1_V.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 107-137-157-157-170-175-170-710-170-701-715-715-710-170-175-715-750 571-571-517-510-157-517-157-175-157-150-157-517-157-751-705-157-571-175-157-715-175-175-751-571-715-157-715-7_V.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 751-175-715-175-175-710-175-175-517-715-715-715-751-751 157-150-157-517-175-175-751-150-135-751-175-150-105-715-175-715-715-715-715-715-570-175-715-715-571-517-751-175 175-157-7_V.mp3)
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
> **Time**: 2026-04-05 13:18:22
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "안녕하세요 반갑습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 157,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 157, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 415, "dur_ms": 161, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "Is it far from here?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 107,
"audio_type": "speech",
"words": [
{"word": "Is", "start_ms": 107, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "it", "start_ms": 190, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "far", "start_ms": 260, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "from", "start_ms": 370, "dur_ms": 90, "phase_lr_deg": 5.0},
{"word": "here", "start_ms": 470, "dur_ms": 106, "phase_lr_deg": 5.0}
]
},
{
"segment": 3,
"transcript": "정말요? 그럼 라떼를 마셔봐야겠네요.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "정말요", "start_ms": 150, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "그럼", "start_ms": 300, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "라떼를", "start_ms": 410, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "마셔봐야겠네요", "start_ms": 570, "dur_ms": 6, "phase_lr_deg": 5.0}
]
},
{
"segment": 4,
"transcript": "커피 한 잔 하실래요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 157,
"audio_type": "speech",
"words": [
{"word": "커피", "start_ms": 157, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "한", "start_ms": 285, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "잔", "start_ms": 370, "dur_ms": 90, "phase_lr_deg": 5.0},
{"word": "하실래요", "start_ms": 470, "dur_ms": 106, "phase_lr_deg": 5.0}
]
},
{
"segment": 5,
"transcript": "오늘 날씨가 정말 좋네요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 157,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 157, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "날씨가", "start_ms": 300, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "정말", "start_ms": 460, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "좋네요", "start_ms": 570, "dur_ms": 6, "phase_lr_deg": 5.0}
]
},
{
"segment": 6,
"transcript": "I heard that place is famous for its latte.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 157,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 157, "dur_ms": 40, "phase_lr_deg": 5.0},
{"word": "heard", "start_ms": 207, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "that", "start_ms": 327, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "place", "start_ms": 417, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "is", "start_ms": 547, "dur_ms": 29, "phase_lr_deg": 5.0}
]
},
{
"segment": 7,
"transcript": "Let's go right now.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 107,
"audio_type": "speech",
"words": [
{"word": "Let's", "start_ms": 107, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "go", "start_ms": 237, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "right", "start_ms": 347, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "now", "start_ms": 467, "dur_ms": 109, "phase_lr_deg": 5.0}
]
},
{
"segment": 8,
"transcript": "네, 지금 바로 가요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 175,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 175, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 265, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 385, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "가요", "start_ms": 495, "dur_ms": 81, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "거기 분위기가 아주 좋다고 하더라고요.",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 175,
"audio_type": "speech",
"words": [
{"word": "거기", "start_ms": 175, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "분위기가", "start_ms": 285, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 455, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "좋다고", "start_ms": 555, "dur_ms": 21, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "오늘 날씨가 정말 좋네요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 157,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 157, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "날씨가", "start_ms": 280, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "정말", "start_ms": 440, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "좋네요", "start_ms": 550, "dur_ms": 26, "phase_lr_deg": 5.0}
]
},
{
"segment": 11,
"transcript": "Yes, it is a beautiful day.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 175,
"audio_type": "speech",
"words": [
{"word": "Yes", "start_ms": 175, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "it", "start_ms": 285, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 345, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 415, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "beautiful", "start_ms": 465, "dur_ms": 111, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "Where should we go?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 157,
"audio_type": "speech",
"words": [
{"word": "Where", "start_ms": 157, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "should", "start_ms": 275, "dur_ms": 90, "phase_lr_deg": 5.0},
{"word": "we", "start_ms": 370, "dur_ms": 70, "phase_lr_deg": 5.0},
{"word": "go", "start_ms": 450, "dur_ms": 126, "phase_lr_deg": 5.0}
]
},
{
"segment": 13,
"transcript": "좋은 생각이에요. 어디로 갈까요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 175,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 175, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "생각이에요", "start_ms": 295, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "어디로", "start_ms": 525, "dur_ms": 51, "phase_lr_deg": 0.0}
]
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
"transcript": "안녕하세요 반갑습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 157,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 157, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 415, "dur_ms": 161, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "근처에 새로 생긴 카페가 있어요.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 157,
"audio_type": "speech",
"words": [
{"word": "근처에", "start_ms": 157, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "새로", "start_ms": 287, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "생긴", "start_ms": 397, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "카페가", "start_ms": 517, "dur_ms": 59, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "정말요? 그럼 라떼를 마셔봐야겠네요.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 157,
"audio_type": "speech",
"words": [
{"word": "정말요", "start_ms": 157, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "그럼", "start_ms": 307, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "라떼를", "start_ms": 417, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "마셔봐야겠네요", "start_ms": 577, "dur_ms": 0, "phase_lr_deg": 5.0}
]
},
{
"segment": 18,
"transcript": "네, 지금 바로 가요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 157,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 157, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 247, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 367, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "가요", "start_ms": 477, "dur_ms": 99, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "정말요? 그럼 라떼를 마셔봐야겠네요.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "정말요", "start_ms": 150, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "그럼", "start_ms": 300, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "라떼를", "start_ms": 410, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "마셔봐야겠네요", "start_ms": 570, "dur_ms": 6, "phase_lr_deg": 5.0}
]
},
{
"segment": 20,
"transcript": "커피 한 잔 하실래요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 157,
"audio_type": "speech",
"words": [
{"word": "커피", "start_ms": 157, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "한", "start_ms": 285, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "잔", "start_ms": 370, "dur_ms": 90, "phase_lr_deg": 5.0},
{"word": "하실래요", "start_ms": 470, "dur_ms": 106, "phase_lr_deg": 5.0}
]
},
{
"segment": 21,
"transcript": "Where should we go?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 157,
"audio_type": "speech",
"words": [
{"word": "Where", "start_ms": 157, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "should", "start_ms": 275, "dur_ms": 90, "phase_lr_deg": 5.0},
{"word": "we", "start_ms": 370, "dur_ms": 70, "phase_lr_deg": 5.0},
{"word": "go", "start_ms": 450, "dur_ms": 126, "phase_lr_deg": 5.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 13:18:22*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=17708  time=13:17:49  uptime=32931s  mem=351MB  system_processes=363"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_131713.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 105-517-157-751-175-170-715-170-715-751-715-175-157-715-157-751-571-715-715-751-517-710-715-751-751-715-573-751 517-157-751-175-715-175-175-715-157-175-105-517-175-750-175-175-7_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 751-157-517-751-157-752-751-715-715-715-751-715-157-157-175-157 517-571-157-570-571-570-754 715-170-170-170-175-157-175-150-157-175-175-710-170-175-751-715-517-517-173-175-175-1_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 517-751-705-715-751-517-157-517-751-750-570-751-571-750-715-715-175-751 517-517-715-170-157-157-571-750-750-715-157-157-715-175-715-175-751-751-751-750-751-715-175-751-751-571-7_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 753-756-751-751-265-625-621-167-156-175-176-175-175-725-753-752-572-572-572-572-752-752-752-257 175-175-316-153-517-156-517-153-517-571-571-175-517-157-571-751-756-175-753-572-7_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 517-150-157-157-517-157-157-152-157-175-175-175-135-153-137-175-715-157-157 105-157-135-150-153-157-135-150-150-157-150-105-157-132-157-175-175-175-137-137-105-157-173-157 150-1_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 750-751-715-170-135-165-615-167-167-160-176-173-137-163-160-167-175-176-576-153-157-175-167-657-576-135-135-576-573-573-753-316-315-135-157-157-157-165-157-157-175-157-157-156-1_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 715-715-172-175-175-102-132-107-170-715-135-571-150 107-175-157-157-157-175-157-135-175-157-137-751-517-175-715-710-175-715-157-715-175-175-751-715-173-172-170-730 175-571-157-7_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 710-170-715-715-715-175-715-170-170-175-175-175-715 175-517-510-175-175-157-157-175-571-157-751-157-751-157-751-715-175-517-750-710-170-701-751-751-517-571 571-715-715-175_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 175-105-175-170-170-715-175-170-517-175-715-750-751-751-571 107-170-715-750-175-170-175-170-710-170-715 107-175-705-170-175-170-175-751-715-710-170-175-175-751-715-157-715-170-7_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 571-157-751-571-157-571-751-570_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 157-571-157-175-517-751-517-175-715-157-157 715-157-751-175-517-157-510-751-157-157-153-107-157-150_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 751-175-715-175-175-710-175-175-517-715-715-715-751-751 157-150-157-517-175-175-751-150-135-751-175-150-105-715-175-715-715-715-715-715-570-175-715-715-571-517-751-175 175-157-7_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 751-175-751-570-751-751 170-157-175-157-175-751-517-571-571-175-751-751-571-753-751-517-571-751-571-570-571-571-570-753-573-571-571-571 517-157-751-517-157-517-751-175-172-751-1_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 107-137-157-157-170-175-170-710-170-701-715-715-710-170-175-715-750 571-571-517-510-157-517-157-175-157-150-157-517-157-751-705-157-571-175-157-715-175-175-751-571-715-157-715-7_V.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 517-175-175-751-571-175-157-715-751-175-715-175-751-571-715-175-715-150-157-510 157-107-175-170-715-170-157-175-157-715-175-157-175-751-571-157-517-715-175-751-751-751-715-175 1_V.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 517-175-751-175-571-157-175-175-750 715-175-571-170-701-170-175-715-170-751-175-175-715-715-175-705 715-175-751-170-751-715-751-170-751-751-751-715-175-715-751-715-751-157-751-7_V.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 157-517 157-751-715-175-175-170-517-517-517-510-157-175-517-157-150-751-571-175-571-751-157-705-751-715-751-715-751 517-157-157-137-175-715-172-713-173-715-175-715-175-175-153-1_V.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 157-517-571-571 157-157-571-517-751-571-517-571_W.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 150-571-150-157-713-751-157-753-751-715-715-751-751-571-750 157-175-157-105-751-715-715-715-751-175-175-715-750-715-751-175-715-715-751-751-571-715-751-715-157-173-170-751-705-1_V.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 705-715-170-107-710-715-710-710-170-107-170-715-751 175-175-175-157-517-571-157-751-570-570-157-517-751-175-715-751-750-751-571-175-705-175 571-571-571-571-157-570-157-150-705-5_W.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 751-715 571-157-135-751-571-157-715-571-157-170-157-175-157-750-715-175-715-175-175-751-715-751-751-751-751-715 157-517-173-715-170-127-102-751-175-175-175-751-751-750-175-752-7_V.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 157-571-157-175-715-175-175-715-175-157-175-571-715-751-570-157-715-751 157-105-105-150-157-157-157-715-157-175-157-517-517-715-570-751-715-175-170-175-157-512-751-715-701-175-1_V.mp3)
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
> **Time**: 2026-04-05 13:18:51
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "오늘 날씨가 정말 좋네요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 105,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 105, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "날씨가", "start_ms": 230, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "정말", "start_ms": 390, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "좋네요", "start_ms": 500, "dur_ms": 76, "phase_lr_deg": 5.0}
]
},
{
"segment": 2,
"transcript": "정말요? 그럼 라떼를 마셔봐야겠네요.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 157,
"audio_type": "speech",
"words": [
{"word": "정말요", "start_ms": 157, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "그럼", "start_ms": 307, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "라떼를", "start_ms": 417, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "마셔봐야겠네요", "start_ms": 577, "dur_ms": 1, "phase_lr_deg": 5.0}
]
},
{
"segment": 3,
"transcript": "I heard that place is famous for its latte.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 157,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 157, "dur_ms": 40, "phase_lr_deg": 5.0},
{"word": "heard", "start_ms": 207, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "that", "start_ms": 327, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "place", "start_ms": 417, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "is", "start_ms": 547, "dur_ms": 29, "phase_lr_deg": 5.0}
]
},
{
"segment": 4,
"transcript": "네, 지금 바로 가요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 175,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 175, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 265, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 385, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "가요", "start_ms": 495, "dur_ms": 81, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "커피 한 잔 하실래요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 157,
"audio_type": "speech",
"words": [
{"word": "커피", "start_ms": 157, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "한", "start_ms": 287, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "잔", "start_ms": 377, "dur_ms": 90, "phase_lr_deg": 5.0},
{"word": "하실래요", "start_ms": 477, "dur_ms": 99, "phase_lr_deg": 5.0}
]
},
{
"segment": 6,
"transcript": "안녕하세요 반갑습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 157,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 157, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 415, "dur_ms": 161, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "Is it far from here?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 107,
"audio_type": "speech",
"words": [
{"word": "Is", "start_ms": 107, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "it", "start_ms": 197, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "far", "start_ms": 267, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "from", "start_ms": 377, "dur_ms": 90, "phase_lr_deg": 5.0},
{"word": "here", "start_ms": 477, "dur_ms": 106, "phase_lr_deg": 5.0}
]
},
{
"segment": 8,
"transcript": "Let's go right now.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 107,
"audio_type": "speech",
"words": [
{"word": "Let's", "start_ms": 107, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "go", "start_ms": 237, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "right", "start_ms": 347, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "now", "start_ms": 467, "dur_ms": 109, "phase_lr_deg": 5.0}
]
},
{
"segment": 9,
"transcript": "Yes, it is a beautiful day.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 175,
"audio_type": "speech",
"words": [
{"word": "Yes", "start_ms": 175, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "it", "start_ms": 285, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 345, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 415, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "beautiful", "start_ms": 465, "dur_ms": 111, "phase_lr_deg": 0.0}
]
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
"transcript": "네, 지금 바로 가요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 157,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 157, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 247, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 367, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "가요", "start_ms": 477, "dur_ms": 99, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "커피 한 잔 하실래요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 157,
"audio_type": "speech",
"words": [
{"word": "커피", "start_ms": 157, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "한", "start_ms": 287, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "잔", "start_ms": 377, "dur_ms": 90, "phase_lr_deg": 5.0},
{"word": "하실래요", "start_ms": 477, "dur_ms": 99, "phase_lr_deg": 5.0}
]
},
{
"segment": 13,
"transcript": "거기 분위기가 아주 좋다고 하더라고요.",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 175,
"audio_type": "speech",
"words": [
{"word": "거기", "start_ms": 175, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "분위기가", "start_ms": 285, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 455, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "좋다고", "start_ms": 555, "dur_ms": 21, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "Where should we go?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 157,
"audio_type": "speech",
"words": [
{"word": "Where", "start_ms": 157, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "should", "start_ms": 275, "dur_ms": 90, "phase_lr_deg": 5.0},
{"word": "we", "start_ms": 370, "dur_ms": 70, "phase_lr_deg": 5.0},
{"word": "go", "start_ms": 450, "dur_ms": 126, "phase_lr_deg": 5.0}
]
},
{
"segment": 15,
"transcript": "정말요? 그럼 라떼를 마셔봐야겠네요.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "정말요", "start_ms": 150, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "그럼", "start_ms": 300, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "라떼를", "start_ms": 410, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "마셔봐야겠네요", "start_ms": 570, "dur_ms": 6, "phase_lr_deg": 5.0}
]
},
{
"segment": 16,
"transcript": "Where should we go?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 157,
"audio_type": "speech",
"words": [
{"word": "Where", "start_ms": 157, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "should", "start_ms": 275, "dur_ms": 90, "phase_lr_deg": 5.0},
{"word": "we", "start_ms": 370, "dur_ms": 70, "phase_lr_deg": 5.0},
{"word": "go", "start_ms": 450, "dur_ms": 126, "phase_lr_deg": 5.0}
]
},
{
"segment": 17,
"transcript": "좋은 생각이에요. 어디로 갈까요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 157,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 157, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "생각이에요", "start_ms": 280, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "어디로", "start_ms": 520, "dur_ms": 56, "phase_lr_deg": 0.0}
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
"transcript": "정말요? 그럼 라떼를 마셔봐야겠네요.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "정말요", "start_ms": 150, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "그럼", "start_ms": 300, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "라떼를", "start_ms": 410, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "마셔봐야겠네요", "start_ms": 570, "dur_ms": 6, "phase_lr_deg": 5.0}
]
},
{
"segment": 20,
"transcript": "근처에 새로 생긴 카페가 있어요.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 157,
"audio_type": "speech",
"words": [
{"word": "근처에", "start_ms": 157, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "새로", "start_ms": 287, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "생긴", "start_ms": 397, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "카페가", "start_ms": 517, "dur_ms": 136, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "안녕하세요 반갑습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 157,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 157, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 415, "dur_ms": 161, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "오늘 날씨가 정말 좋네요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 157,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 157, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "날씨가", "start_ms": 280, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "정말", "start_ms": 440, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "좋네요", "start_ms": 550, "dur_ms": 26, "phase_lr_deg": 5.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 13:18:51*
