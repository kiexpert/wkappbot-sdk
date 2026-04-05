# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=24932  time=13:14:09  uptime=32134s  mem=424MB  system_processes=353"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_131249.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 157-715-571-576-753-573-573-537-753-752-573-756-536-572-751-753-753-751-573-753-573-753-753-135-137-137-172-170-170-107-617-716-176-617-167-175-157-751-571-573-357-563-573-573-5_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 570-150-175-107-175-510-170-170-157-170-571-751-175-715-571-175-715-175-571-517 175 157-157-175-105-175-157-751-157-105-175-157-157-507-501-517-157-715-570-175 157-150-150-157-1_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 175-571-157-510-157-175-157-157-157-150-175-175-157-175-157-713-517-715_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 107-170-170-750-715-175-175 175-175-715-170-170-107-107-731-175-170-175-135-170-157-175-175-175-175-173-173-175-175-715-175-715 574-571-517-571-751-157-571-157-571-751-571-517-1_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 752-576-576 157-173-107-317-173-137-132-170-172-175-175-175-157-173-175-715-175-712-175-715-175-715-157-175-157-175 175-157-715-157-175-153-157-175-175-517-175-173-157-715-175-1_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 150-510-517-157-751-517-517 150-153-150-517-157-751-150-751-170-751-157-570-517-150-517-150-517-157-175-157-571 150-157-153-510-150-150-105-150-510-150-150-157-157-150-157-157-5_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 713-170-715-172-715-715-153-321-135-361-352-312-312-536-135-157-571-175-157-157-156-157-156-576-152-175-315-362-567-531-365-352-571-753-753-572-752-571-157-756-756-756-517-175-1_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 175-175-715-751-157-571-157-175-157-751-715-157 105-150-107-105-157-150-175-715-157-517-175-715-157-175-517-150-175-715-715-175-157-715-175-157-751 105-150-105-105-175-157-157-1_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 157-157-157-517-157-517-157-517-157-517_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 157-170-175-517-517-157-157-571-157-715-751-715-752-715 571-157-153-157-175-175-175-175-175-175-157-157-715-751-571-751-517-750-750-175-105-173 571-517_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 517-137-715-751-715-175 574-571-570-510-157-517-157-517-571-751-571-517-157-571-517-175-175-157_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 536-675-657-567-657-571-752-751-165-167-176-157-156-516-156-156-753-765-157-657-573-756-653-576-173-157-107-175-175-157-715-157-571-571-517-157-157-175-157-751-571-761-657-657-5_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 517-157-153-105-150-157-157-170-170-175-150-175-175-157-157-175-517-175-157-750-157-175-751-157-175-157 150-175-150-157-105-150-150-157-175-175-175-175-175-751-751-175-175-157-1_V.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 123-130-170-172-173-137-765-167-172-176-175-175-175-175-172-176-172-172-716-257-512-576-751-576-573-176-275-527-172-127-216-271-176-725-572-157-571-517-157-135-156-615-135-156-1_W.mp3)
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
> **Time**: 2026-04-05 13:15:01
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "거기 분위기가 아주 좋다고 하더라고요.",
"speaker": "speaker_1",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 135,
"audio_type": "speech",
"words": [
{"word": "거기", "start_ms": 135, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "분위기가", "start_ms": 245, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 415, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "좋다고", "start_ms": 515, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "하더라고요", "start_ms": 665, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "정말요? 그럼 라떼를 마셔봐야겠네요.",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 107,
"audio_type": "speech",
"words": [
{"word": "정말요", "start_ms": 107, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "그럼", "start_ms": 257, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "라떼를", "start_ms": 367, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "마셔봐야겠네요", "start_ms": 527, "dur_ms": 280, "phase_lr_deg": 5.0}
]
},
{
"segment": 3,
"transcript": "오늘 날씨가 정말 좋네요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 175,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 175, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "날씨가", "start_ms": 300, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "정말", "start_ms": 460, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "좋네요", "start_ms": 570, "dur_ms": 140, "phase_lr_deg": 5.0}
]
},
{
"segment": 4,
"transcript": "Let's go right now.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 107,
"audio_type": "speech",
"words": [
{"word": "Let's", "start_ms": 107, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "go", "start_ms": 237, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "right", "start_ms": 347, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "now", "start_ms": 467, "dur_ms": 130, "phase_lr_deg": 5.0}
]
},
{
"segment": 5,
"transcript": "I heard that place is famous for its latte.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 157,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 157, "dur_ms": 40, "phase_lr_deg": 5.0},
{"word": "heard", "start_ms": 207, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "that", "start_ms": 327, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "place", "start_ms": 417, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "is", "start_ms": 547, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "famous", "start_ms": 617, "dur_ms": 180, "phase_lr_deg": 5.0},
{"word": "for", "start_ms": 807, "dur_ms": 70, "phase_lr_deg": 5.0},
{"word": "its", "start_ms": 887, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "latte", "start_ms": 957, "dur_ms": 150, "phase_lr_deg": 5.0}
]
},
{
"segment": 6,
"transcript": "커피 한 잔 하실래요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "커피", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "한", "start_ms": 280, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "잔", "start_ms": 370, "dur_ms": 90, "phase_lr_deg": 5.0},
{"word": "하실래요", "start_ms": 470, "dur_ms": 200, "phase_lr_deg": 5.0}
]
},
{
"segment": 7,
"transcript": "좋은 생각이에요. 어디로 갈까요?",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 170,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 170, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "생각이에요", "start_ms": 290, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "어디로", "start_ms": 520, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "갈까요", "start_ms": 680, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "Where should we go?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 105,
"audio_type": "speech",
"words": [
{"word": "Where", "start_ms": 105, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "should", "start_ms": 220, "dur_ms": 90, "phase_lr_deg": 5.0},
{"word": "we", "start_ms": 320, "dur_ms": 70, "phase_lr_deg": 5.0},
{"word": "go", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": 5.0}
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
"transcript": "안녕하세요 반갑습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 157,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 157, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 415, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "네, 지금 바로 가요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 137,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 137, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 227, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 347, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "가요", "start_ms": 457, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "근처에 새로 생긴 카페가 있어요.",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 165,
"audio_type": "speech",
"words": [
{"word": "근처에", "start_ms": 165, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "새로", "start_ms": 305, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "생긴", "start_ms": 425, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "카페가", "start_ms": 555, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 715, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "커피 한 잔 하실래요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "커피", "start_ms": 150, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "한", "start_ms": 290, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "잔", "start_ms": 380, "dur_ms": 90, "phase_lr_deg": 5.0},
{"word": "하실래요", "start_ms": 480, "dur_ms": 210, "phase_lr_deg": 5.0}
]
},
{
"segment": 14,
"transcript": "Yes, it is a beautiful day.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 123,
"audio_type": "speech",
"words": [
{"word": "Yes", "start_ms": 123, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "it", "start_ms": 230, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 290, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 360, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "beautiful", "start_ms": 410, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "day", "start_ms": 620, "dur_ms": 100, "phase_lr_deg": 0.0}
]
}
]대답이 중지되었습니다.

---
*Generated by WKAppBot ask gemini — 2026-04-05 13:15:01*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=30524  time=13:12:28  uptime=31260s  mem=437MB  system_processes=354"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_131217.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 175-571-157-510-157-175-157-157-157-150-175-175-157-175-157-713-517-715_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 107-170-170-750-715-175-175 175-175-715-170-170-107-107-731-175-170-175-135-170-157-175-175-175-175-173-173-175-175-715-175-715 574-571-517-571-751-157-571-157-571-751-571-517-1_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 157-157-157-517-157-517-157-517-157-517_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 123-130-170-172-173-137-765-167-172-176-175-175-175-175-172-176-172-172-716-257-512-576-751-576-573-176-275-527-172-127-216-271-176-725-572-157-571-517-157-135-156-615-135-156-1_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 157-170-175-517-517-157-157-571-157-715-751-715-752-715 571-157-153-157-175-175-175-175-175-175-157-157-715-751-571-751-517-750-750-175-105-173 571-517_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 150-510-517-157-751-517-517 150-153-150-517-157-751-150-751-170-751-157-570-517-150-517-150-517-157-175-157-571 150-157-153-510-150-150-105-150-510-150-150-157-157-150-157-157-5_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 752-576-576 157-173-107-317-173-137-132-170-172-175-175-175-157-173-175-715-175-712-175-715-175-715-157-175-157-175 175-157-715-157-175-153-157-175-175-517-175-173-157-715-175-1_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 517-137-715-751-715-175 574-571-570-510-157-517-157-517-571-751-571-517-157-571-517-175-175-157_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 536-675-657-567-657-571-752-751-165-167-176-157-156-516-156-156-753-765-157-657-573-756-653-576-173-157-107-175-175-157-715-157-571-571-517-157-157-175-157-751-571-761-657-657-5_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 157-715-571-576-753-573-573-537-753-752-573-756-536-572-751-753-753-751-573-753-573-753-753-135-137-137-172-170-170-107-617-716-176-617-167-175-157-751-571-573-357-563-573-573-5_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 713-170-715-172-715-715-153-321-135-361-352-312-312-536-135-157-571-175-157-157-156-157-156-576-152-175-315-362-567-531-365-352-571-753-753-572-752-571-157-756-756-756-517-175-1_W.mp3)
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
> **Time**: 2026-04-05 13:15:01
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "거기 분위기가 아주 좋다고 하더라고요.",
"speaker": "speaker_1",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 135,
"audio_type": "speech",
"words": [
{"word": "거기", "start_ms": 135, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "분위기가", "start_ms": 245, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 415, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "좋다고", "start_ms": 515, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "하더라고요", "start_ms": 665, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "정말요? 그럼 라떼를 마셔봐야겠네요.",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 107,
"audio_type": "speech",
"words": [
{"word": "정말요", "start_ms": 107, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "그럼", "start_ms": 257, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "라떼를", "start_ms": 367, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "마셔봐야겠네요", "start_ms": 527, "dur_ms": 280, "phase_lr_deg": 5.0}
]
},
{
"segment": 3,
"transcript": "오늘 날씨가 정말 좋네요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 175,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 175, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "날씨가", "start_ms": 300, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "정말", "start_ms": 460, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "좋네요", "start_ms": 570, "dur_ms": 140, "phase_lr_deg": 5.0}
]
},
{
"segment": 4,
"transcript": "Let's go right now.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 107,
"audio_type": "speech",
"words": [
{"word": "Let's", "start_ms": 107, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "go", "start_ms": 237, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "right", "start_ms": 347, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "now", "start_ms": 467, "dur_ms": 130, "phase_lr_deg": 5.0}
]
},
{
"segment": 5,
"transcript": "I heard that place is famous for its latte.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 157,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 157, "dur_ms": 40, "phase_lr_deg": 5.0},
{"word": "heard", "start_ms": 207, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "that", "start_ms": 327, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "place", "start_ms": 417, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "is", "start_ms": 547, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "famous", "start_ms": 617, "dur_ms": 180, "phase_lr_deg": 5.0},
{"word": "for", "start_ms": 807, "dur_ms": 70, "phase_lr_deg": 5.0},
{"word": "its", "start_ms": 887, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "latte", "start_ms": 957, "dur_ms": 150, "phase_lr_deg": 5.0}
]
},
{
"segment": 6,
"transcript": "커피 한 잔 하실래요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "커피", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "한", "start_ms": 280, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "잔", "start_ms": 370, "dur_ms": 90, "phase_lr_deg": 5.0},
{"word": "하실래요", "start_ms": 470, "dur_ms": 200, "phase_lr_deg": 5.0}
]
},
{
"segment": 7,
"transcript": "좋은 생각이에요. 어디로 갈까요?",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 170,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 170, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "생각이에요", "start_ms": 290, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "어디로", "start_ms": 520, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "갈까요", "start_ms": 680, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "Where should we go?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 105,
"audio_type": "speech",
"words": [
{"word": "Where", "start_ms": 105, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "should", "start_ms": 220, "dur_ms": 90, "phase_lr_deg": 5.0},
{"word": "we", "start_ms": 320, "dur_ms": 70, "phase_lr_deg": 5.0},
{"word": "go", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": 5.0}
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
"transcript": "안녕하세요 반갑습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 157,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 157, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 415, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "네, 지금 바로 가요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 137,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 137, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 227, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 347, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "가요", "start_ms": 457, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "근처에 새로 생긴 카페가 있어요.",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 165,
"audio_type": "speech",
"words": [
{"word": "근처에", "start_ms": 165, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "새로", "start_ms": 305, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "생긴", "start_ms": 425, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "카페가", "start_ms": 555, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 715, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "커피 한 잔 하실래요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "커피", "start_ms": 150, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "한", "start_ms": 290, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "잔", "start_ms": 380, "dur_ms": 90, "phase_lr_deg": 5.0},
{"word": "하실래요", "start_ms": 480, "dur_ms": 210, "phase_lr_deg": 5.0}
]
},
{
"segment": 14,
"transcript": "Yes, it is a beautiful day.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 123,
"audio_type": "speech",
"words": [
{"word": "Yes", "start_ms": 123, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "it", "start_ms": 230, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 290, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 360, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "beautiful", "start_ms": 410, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "day", "start_ms": 620, "dur_ms": 100, "phase_lr_deg": 0.0}
]
}
]대답이 중지되었습니다.

---
*Generated by WKAppBot ask gemini — 2026-04-05 13:15:01*
