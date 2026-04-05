# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=17628  time=13:21:32  uptime=41305s  mem=285MB  system_processes=355"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_132117.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 157-571-175-517-750-751-715-175-751-173-751-175-157_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 170-170-170-170-175-715-170-137-157-175-715-751-175-751-517-157-751-715-571-715-157 517-157-105-150-715-170-751-157-175-153-157-157-175-170-710-175-170-175-157-157-175-715-175-7_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 573-576-573-576-756-576-573-756-752 157-153-513-157-715-157-157-157-157-751-157-157-571-157-150-170-571-517-715-175-157-751-751-175 153-571-157-157-175-157-517-157-517-751-175-7_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 150-153-150-501-150-150-130-153-173-517-153-157-150_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 176-137-173-176-756-756-756-576-657-756-576-756-576-752-756-756-576-517-517-157-157-137-712-176-617-617-157-571-176-716-167-675-671-671-672-675-657-675-657-576-576-576-765-576-5_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 574-574-574-574-574-574-571_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 517-570-157-571-170-157-751-517-751-751_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 165-612-160-163-162-167-162-167-617-167-675-621-675-657 105-105-173-107-103-170-103-105-175-105-157-105-150-157-175-175-135-157-517-157-175-150-571_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 751-751-175-157-715-157-571-157-125-235 571-751-751-751-170-175-751-715-710-715-751-175-752-750-705-705-715-175-710-571-715-715-705-751 150-517-507-175-751-157-751-517-157-571-7_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 574-574-574-574-574-574-574-574-574-571-571-574-754-574-574-574-574-574_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 571-571-570-715-517-751-517-157-571-571-750-571-615-672-612-167-137-157-517-573-157-517-537-571-576-756-537-573-576-756-753-753-752-756-753-157-175-173-715-576-751-157-175-751-7_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 571-571-573-573-173-170-170-175-157-157-130-657-576-512-253-256-576-257-576-157-135-571-657-715-571-537 175-175-176-761-675-617-175-172-751-175-712-517-167-617-165-167-167-617-1_W.mp3)
  Segment 13: 18912ms ~ 23160ms (file: 105-315-351-751-175-157-371-705-173 123-105-130-721-175-712-731-271-106-213-175-127-175-130-107-105-750-127-107-751-157-751-571-571-750-751-715-571 175-170-175-710-571-150-571-1_V.mp3)
  Segment 14: 24160ms ~ 24736ms (file: 547-574-574-574-574-574-574-571-751-571-571-571-750_W.mp3)
  Segment 15: 25736ms ~ 26312ms (file: 157-751-750 517-175-105-751-750-751-157-175-175-175-751-175-571-510-715-517-175-751-571-751-750-751 571-157-157-175-107-175-715-715-715-701-715-751-517-751-750-752-175-715-750-1_V.mp3)
  Segment 16: 27312ms ~ 27888ms (file: 752-751-751-571-751-751-752-751_W.mp3)
  Segment 17: 28888ms ~ 29464ms (file: 571-751-175-751-157-715-715-751-715-715-157-510-751-175-751-571-751-175 517-570-751-157-175-175-157-715-105-715-571-751-715-751-157-715-751-715-570-715-571 571-175-570-574-574-5_V.mp3)
  Segment 18: 30464ms ~ 31040ms (file: 517-517-571-150-157-150-510-153-573-517-157-571-517-571-571-571-175-570-153-571-751-715-157 157-510-715-175-715-175-750-751-157-715-517-157-715-157-753-715-157-175-715-175-751-7_W.mp3)
  Segment 19: 32040ms ~ 32616ms (file: 751-175-517-571-517-157-571-715-170-176-765-765-576-756-756-517-571-715-175-751-756-572-576-574-576-572-572-576-576-576-567-756-756-765-761-156-761-615-165-165-163-175-165-615-6_W.mp3)
  Segment 20: 33616ms ~ 34192ms (file: 517-751-157-571-572-152-571-175-572-216-576-576-576-576-725-217-721-715-175-175-172-127-152-123-125-137-137-137-371-721-273-572-576-756-572-576-573-572-573-576-576-576-573-573-5_W.mp3)
  Segment 21: 35192ms ~ 35768ms (file: 715-715-175-157-751-170-715-751-715-715-751-571-157-713-751-751-715-751-750-751-715-573-517-571-571-571-576-754-572 752-726-276-267-762-675-765-675-671-576-752-572-572-752 107-5_W.mp3)
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
> **Time**: 2026-04-05 13:22:16
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
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 40, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "만나서 반가워요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "만나서", "start_ms": 30, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "반가워요", "start_ms": 290, "dur_ms": 280, "phase_lr_deg": -5.0}
]
},
{
"segment": 3,
"transcript": "오늘 날씨가 정말 좋네요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "날씨가", "start_ms": 180, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "정말", "start_ms": 340, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "좋네요", "start_ms": 450, "dur_ms": 120, "phase_lr_deg": -5.0}
]
},
{
"segment": 4,
"transcript": "Yes, it is beautiful",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "Yes,", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "it", "start_ms": 180, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 270, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "beautiful", "start_ms": 380, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "커피 한 잔 하실래요?",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "커피", "start_ms": 60, "dur_ms": 140, "phase_lr_deg": -5.0},
{"word": "한", "start_ms": 210, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "잔", "start_ms": 300, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "하실래요?", "start_ms": 410, "dur_ms": 160, "phase_lr_deg": -5.0}
]
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
"transcript": "좋은 생각이에요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 45, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "생각이에요", "start_ms": 260, "dur_ms": 310, "phase_lr_deg": -5.0}
]
},
{
"segment": 8,
"transcript": "I would love some coffee",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "would", "start_ms": 120, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "love", "start_ms": 230, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "some", "start_ms": 360, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "coffee", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "어떤 커피를 좋아하세요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "어떤", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "커피를", "start_ms": 170, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "좋아하세요?", "start_ms": 330, "dur_ms": 240, "phase_lr_deg": -5.0}
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
"transcript": "저는 아이스 아메리카노요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "아이스", "start_ms": 160, "dur_ms": 140, "phase_lr_deg": -5.0},
{"word": "아메리카노요", "start_ms": 310, "dur_ms": 260, "phase_lr_deg": -5.0}
]
},
{
"segment": 12,
"transcript": "저도 같은 걸로 할게요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "저도", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": -5.0},
{"word": "같은", "start_ms": 160, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "걸로", "start_ms": 290, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "할게요", "start_ms": 400, "dur_ms": 170, "phase_lr_deg": -5.0}
]
},
{
"segment": 13,
"transcript": "Okay, two iced americanos then. 주문 도와드릴게요.",
"speaker": "speaker_1",
"confidence": 0.93,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "Okay,", "start_ms": 100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "two", "start_ms": 280, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "iced", "start_ms": 410, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "americanos", "start_ms": 560, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "then.", "start_ms": 880, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "주문", "start_ms": 1100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "도와드릴게요.", "start_ms": 1350, "dur_ms": 400, "phase_lr_deg": 0.0}
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
"transcript": "잠시만 기다려 주세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "잠시만", "start_ms": 30, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "기다려", "start_ms": 240, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": 0.0}
]
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
"transcript": "여기 주문하신 커피 나왔습니다",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "여기", "start_ms": 20, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "주문하신", "start_ms": 130, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "커피", "start_ms": 320, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "나왔습니다", "start_ms": 450, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "감사합니다. 잘 마실게요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "감사합니다.", "start_ms": 40, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "잘", "start_ms": 300, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "마실게요.", "start_ms": 390, "dur_ms": 180, "phase_lr_deg": -5.0}
]
},
{
"segment": 19,
"transcript": "즐거운 시간 보내세요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "즐거운", "start_ms": 55, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "시간", "start_ms": 250, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "보내세요", "start_ms": 380, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "네, 고마워요. 다음에 또 봐요.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 35, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "고마워요.", "start_ms": 130, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "다음에", "start_ms": 290, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "또", "start_ms": 420, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "봐요.", "start_ms": 490, "dur_ms": 80, "phase_lr_deg": -5.0}
]
},
{
"segment": 21,
"transcript": "See you later!",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "later!", "start_ms": 280, "dur_ms": 250, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 13:22:16*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=17708  time=13:21:56  uptime=33177s  mem=487MB  system_processes=354"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_132028.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 752-751-751-571-751-751-752-751_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 573-576-573-576-756-576-573-756-752 157-153-513-157-715-157-157-157-157-751-157-157-571-157-150-170-571-517-715-175-157-751-751-175 153-571-157-157-175-157-517-157-517-751-175-7_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 715-715-175-157-751-170-715-751-715-715-751-571-157-713-751-751-715-751-750-751-715-573-517-571-571-571-576-754-572 752-726-276-267-762-675-765-675-671-576-752-572-572-752 107-5_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 517-517-571-150-157-150-510-153-573-517-157-571-517-571-571-571-175-570-153-571-751-715-157 157-510-715-175-715-175-750-751-157-715-517-157-715-157-753-715-157-175-715-175-751-7_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 574-574-574-574-574-574-571_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 157-571-175-517-750-751-715-175-751-173-751-175-157_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 751-751-175-157-715-157-571-157-125-235 571-751-751-751-170-175-751-715-710-715-751-175-752-750-705-705-715-175-710-571-715-715-705-751 150-517-507-175-751-157-751-517-157-571-7_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 571-571-570-715-517-751-517-157-571-571-750-571-615-672-612-167-137-157-517-573-157-517-537-571-576-756-537-573-576-756-753-753-752-756-753-157-175-173-715-576-751-157-175-751-7_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 571-571-573-573-173-170-170-175-157-157-130-657-576-512-253-256-576-257-576-157-135-571-657-715-571-537 175-175-176-761-675-617-175-172-751-175-712-517-167-617-165-167-167-617-1_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 547-574-574-574-574-574-574-571-751-571-571-571-750_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 170-170-170-170-175-715-170-137-157-175-715-751-175-751-517-157-751-715-571-715-157 517-157-105-150-715-170-751-157-175-153-157-157-175-170-710-175-170-175-157-157-175-715-175-7_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 517-570-157-571-170-157-751-517-751-751_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 157-751-750 517-175-105-751-750-751-157-175-175-175-751-175-571-510-715-517-175-751-571-751-750-751 571-157-157-175-107-175-715-715-715-701-715-751-517-751-750-752-175-715-750-1_V.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 574-574-574-574-574-574-574-574-574-571-571-574-754-574-574-574-574-574_W.mp3)
  Segment 15: 22064ms ~ 26312ms (file: 105-315-351-751-175-157-371-705-173 123-105-130-721-175-712-731-271-106-213-175-127-175-130-107-105-750-127-107-751-157-751-571-571-750-751-715-571 175-170-175-710-571-150-571-1_V.mp3)
  Segment 16: 27312ms ~ 27888ms (file: 571-751-175-751-157-715-715-751-715-715-157-510-751-175-751-571-751-175 517-570-751-157-175-175-157-715-105-715-571-751-715-751-157-715-751-715-570-715-571 571-175-570-574-574-5_V.mp3)
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
> **Time**: 2026-04-05 13:22:48
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
"transcript": "오늘 날씨가 정말 좋네요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 45, "dur_ms": 130, "phase_lr_deg": -5.0},
{"word": "날씨가", "start_ms": 185, "dur_ms": 160, "phase_lr_deg": -5.0},
{"word": "정말", "start_ms": 355, "dur_ms": 110, "phase_lr_deg": -5.0},
{"word": "좋네요", "start_ms": 475, "dur_ms": 90, "phase_lr_deg": -5.0}
]
},
{
"segment": 3,
"transcript": "See you later!",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 30, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 150, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "later!", "start_ms": 290, "dur_ms": 260, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "감사합니다 잘 마실게요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 50, "dur_ms": 240, "phase_lr_deg": -5.0},
{"word": "잘", "start_ms": 310, "dur_ms": 90, "phase_lr_deg": -5.0},
{"word": "마실게요", "start_ms": 410, "dur_ms": 150, "phase_lr_deg": -5.0}
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
"transcript": "안녕하세요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 25, "dur_ms": 530, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "어떤 커피를 좋아하세요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "어떤", "start_ms": 40, "dur_ms": 130, "phase_lr_deg": -5.0},
{"word": "커피를", "start_ms": 180, "dur_ms": 160, "phase_lr_deg": -5.0},
{"word": "좋아하세요?", "start_ms": 350, "dur_ms": 210, "phase_lr_deg": -5.0}
]
},
{
"segment": 8,
"transcript": "저는 아이스 아메리카노요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 60, "dur_ms": 110, "phase_lr_deg": -5.0},
{"word": "아이스", "start_ms": 180, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "아메리카노요", "start_ms": 340, "dur_ms": 220, "phase_lr_deg": -5.0}
]
},
{
"segment": 9,
"transcript": "저도 같은 걸로 할게요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "저도", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "같은", "start_ms": 165, "dur_ms": 130, "phase_lr_deg": -5.0},
{"word": "걸로", "start_ms": 305, "dur_ms": 110, "phase_lr_deg": -5.0},
{"word": "할게요", "start_ms": 425, "dur_ms": 140, "phase_lr_deg": -5.0}
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
"transcript": "만나서 반가워요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "만나서", "start_ms": 40, "dur_ms": 260, "phase_lr_deg": -5.0},
{"word": "반가워요", "start_ms": 310, "dur_ms": 250, "phase_lr_deg": -5.0}
]
},
{
"segment": 12,
"transcript": "좋은 생각이에요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 30, "dur_ms": 210, "phase_lr_deg": -5.0},
{"word": "생각이에요", "start_ms": 250, "dur_ms": 310, "phase_lr_deg": -5.0}
]
},
{
"segment": 13,
"transcript": "잠시만 기다려 주세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "잠시만", "start_ms": 45, "dur_ms": 190, "phase_lr_deg": 0.0},
{"word": "기다려", "start_ms": 245, "dur_ms": 170, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 425, "dur_ms": 140, "phase_lr_deg": 0.0}
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
"transcript": "Okay, two iced americanos then. 주문 도와드릴게요.",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "Okay,", "start_ms": 110, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "two", "start_ms": 290, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "iced", "start_ms": 430, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "americanos", "start_ms": 590, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "then.", "start_ms": 920, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "주문", "start_ms": 1150, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "도와드릴게요.", "start_ms": 1400, "dur_ms": 420, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "여기 주문하신 커피 나왔습니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "여기", "start_ms": 30, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "주문하신", "start_ms": 150, "dur_ms": 190, "phase_lr_deg": 0.0},
{"word": "커피", "start_ms": 350, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "나왔습니다", "start_ms": 490, "dur_ms": 110, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 13:22:48*
