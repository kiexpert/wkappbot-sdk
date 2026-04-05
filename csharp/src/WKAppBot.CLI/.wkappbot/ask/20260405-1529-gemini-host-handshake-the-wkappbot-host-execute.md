# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=1700  time=15:28:19  uptime=39166s  mem=1114MB  system_processes=357"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_152806.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 716-317-617-175-173-167-176-175-173-176-173-176-167-361-163-613-137-123-157-157-132-125-126-125-165-127-132-136-315-135-137-135-157-357-135-312-153-135-157-156-156-517-156-153-1_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 541-547-547-547-537-537-574-537 571-157-517-517-157-517-517-531-517-517-513-517-531-153 135-153-157-153-153-153-125-125-651-612-162-163-165-163-156-136-135 516-156-156-150-165-1_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 315-315-751-157-317-573-357-351-175-153-156-157-156-517-156-157-156-165-137-157-175-317-135-367-137-735-175 176-376 173-173-751-571-576-571-517-517-517-571-571-571-756-574-576-1_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 152-157-130-572-573-305-571-517-527-510-510-157-150-510 130-510-501-105-150-150-501-153-715-105-510-517-150-501-157-175-715-517-571-517-150-510-105-150-507-107-517 150-157-105-5_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 510-157-105-105-501-510-517-150-501-510-510-570-501-150 517-105-150-517-571-157-150-157-105-153-501-135-517-105-570-105-150-150-150-510-150-157-517-150-513-501-510-571 150-105-1_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 153-157-517 570-517-510-510-157-157-517-517-517-517-510-571-517-517-571-510-501-571-517-517-574-532 510-510-153-150-153-510-510-510-513-517-510-510-507-517-512-521-510-510-510-5_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 512-132-135-135-157-175-175-173-132-135-132-123-135-157-157-135-135-153-152-521-516 150-510-510-157-150-153-153-510-513-153-135-150-153-150-105-150-152-150-513-150-153-153-153-5_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 157-571-572-571-571-531-512-153-510-510-150-510-510-501_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 567-576-513-153-537-675-571-675-167-157-675-157-615-761-375-751-156-657-567-576-573-576-576-673-576-756-576-567-561-657-567-567-576-657-576-574-574-517-753-671-362-123-106-310-1_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 165-135-751-315-157-517-375-371-375-715-375 751-715-375-371-715-317 173-751-153-537-130-527-153-571-517-157-517-517-527-512-152-561-576-521-157-517-130-153 514-150-501-517-517-5_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 150-150-510-517-150-130-513-510-157-157-573-157-150-715-315-517-150-517-157-150-150-157-153-150-315-157-157-517-571 576-510-510-130-517-157-153-150-153-135-517-173-153-150-517-1_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 675-675-675-675-675-156-156-176-175-617-167-615-176 615-165-165-163-165-167-167-617-167-167 761-671-671-765-671-671-761-671-716-671-167-617-167-167 671-672-671-672-167_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 571-537-571-571-153-357-752-753-751-571-573-573-574-573-572-571-517-153-157-156-516-156_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 157-156-157-156-167-156-165-167-165-156-165-157-156-573-751-573-756-751-751-756-576-175 150-517-157-157-157-571-516-517-571-571-571-751-751-157-756-175-175-713-576-170-157-157-6_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 165 163-137-176-175-175-135-135-173-157-157-157-157-157-157-153-157-157-135-105-176-175-176-107-103-130-513-150-512-513-153-517-571-537-513-513-517-517-571-571-571-157-751-751-1_W.mp3)
  Segment 16: 23640ms ~ 28176ms (file: 150-510-135-751-501-501-507-510-501-103-135-315 123-510-105-253-205-621-123-157-105-623-267-261-312-201-150-517-517-157-510-571-571-510-150-105-157-510-501-513-507-507-510-150-1_W.mp3)
  Segment 17: 29176ms ~ 29752ms (file: 150-135-761-671-762-765-657-615-571-517-157-157-157-715-751-571-751-715-157-135-751-571-573-571-573-576-576-576-150-153-153-125-153-135-150-105-105-150-157-137-132-102-351-105-1_W.mp3)
  Segment 18: 30752ms ~ 31328ms (file: 521-175-152-135-152-165-136-167-163-163-163 617-167-167-671-675-167-671-617-167-617-167-617-163-617_W.mp3)
  Segment 19: 32328ms ~ 32904ms (file: 150-150-530-165-135-130-153-135-153_W.mp3)
  Segment 20: 33904ms ~ 34480ms (file: 547-541-513-135-153-150-513-125-135-125-126-152-127-217-162-172-125-175-156-123-153-135-751-153-513-156-516-532-561_W.mp3)
  Segment 21: 35480ms ~ 36056ms (file: 675-651-675-765-765-761-753-756-751-576-756-751-756-571 105-105-107-150-105-357-103-170-107-107-175-176-617-671-617-657-671-671-671-675-675-617-671-672-675-675-672 157-571-157-1_W.mp3)
  Segment 22: 37056ms ~ 37632ms (file: 571-571-571-157-175-175-175-157-715-675-651-675-672-657-675-675-675-657-675 574-507-517-157-157-150-150-105-150-517-517-157-513-517-513-571-510-510-157-150-150-501-105_W.mp3)
  Segment 23: 38632ms ~ 39208ms (file: 765 163-165-167-163-167 163-163-163-613-136-163-163-136-163-136-163-165-163-165-617-163 153-510-150-153-150-150-571-537-573-153-537-157-513-517-537-315_W.mp3)
  Segment 24: 40208ms ~ 40784ms (file: 715-756-675-567-573-357-354-501-105-105-102-235-235-235-231-321-750-756-753-762-602-657-671 657-163-520-103_W.mp3)
  Segment 25: 41784ms ~ 42360ms (file: 573-576-576-312-513-576-576-573-567-356-536-367-567-576-356-571-567-567-573-567-567-546-573-562-517-576-657-567-675-756-625-576-675-136-576-576-576-576-576-562-576-132-675-375-6_W.mp3)
  Segment 26: 43360ms ~ 43936ms (file: 615-167-163-137-613-167-167-617-136-317-165-617-136-156-163-631_W.mp3)
  Segment 27: 44936ms ~ 45512ms (file: 517-517-512-512-510-150-510-150-510-150-153-510-517-510-513-537 510-517-517-571-571-571-571-517-157-751-157-571-571-571-573-517-510-251-150-152-571-175-517-157-150-105-512-507 1_V.mp3)
  Segment 28: 46512ms ~ 47088ms (file: 517-571-517-157-517-150-571-157-153-153-157-125-153-527 153-150-153-510-510-217-173-513-517-150-510-150-152-130-510-157-751-507-570-570-517-510-517-153-105-510 150-150-150-150-1_V.mp3)
  Segment 29: 48088ms ~ 48664ms (file: 510-510-150-510-510-510-150-153-513-510-510-153-510-150-510-105-153-105-510-516-513-153-157-571 510-501-501-510-153-513-510-503-153-150-510-510-150-510-531-153-150-135-150-173-1_V.mp3)
  Segment 30: 49664ms ~ 50240ms (file: 754-574-517-547 715-735-715-716-761-716-167-167-617-613-163-167-167-617-671-617 175-175-176-175-176-176-135-175-167-173-165-175-176-175-715-175-751-517-715-175-157-751-157-751-5_W.mp3)
  Segment 31: 51240ms ~ 51816ms (file: 157-157-752-701-507 105-157-175-150-157-175-157-150-157-107-157-137-157-175-157 571-157-105-150-150-157-157-501-105-107-150-157-507-715-157-510-175-507-570-501-150-507-507-510-5_V.mp3)
  Segment 32: 52816ms ~ 53392ms (file: 510-153-517-130-123-125-153-132-135-135-135-357-315-375-137-176-156-156-157-157-165-157-156-153-135-165-156-631-320-320-326-306-316-751-751-761-756-751-756-571-756-751-756-756-7_W.mp3)
  Segment 33: 54392ms ~ 54968ms (file: 510-571-501-510-517-510-517-517-510-517 513-517-510-510-512-517-510-513-510-510-510-510-517-157-517-513-517-512-512-537-507 153-153-153-153-150-132-153-157-157-135-513-713-173-7_V.mp3)
  Segment 34: 55968ms ~ 56544ms (file: 517-135-315-612-316-136-135-521-125-231-132-123-321-213-325-253-253 135-316-153-153-135-135-132-136-135-125-125-125-152-125-125-156-153-157-153-135-136-132-153-315-135-132-132-7_W.mp3)
  Segment 35: 57544ms ~ 58120ms (file: 510-150-510-501-501-507-501-510-150-501-153-150-105-150-510-501-150-507-501-510-513-513-351-153-150-150-501-105 105-510-150-150-105-510 507-501-105-150 510-150-150-157-510-157-5_V.mp3)
  Segment 36: 59120ms ~ 59696ms (file: 150-150-150-105-157-731-572-216-726-176-521-125-132-762-762-167-751-517-756-571-157-715-135-175-175-173-167-357-627-275-172-173-175-175-175-175-571-751-571-576-756-576-571-751-7_W.mp3)
  Segment 37: 60696ms ~ 61272ms (file: 157-513-157-157-157-517-157-157-517-157-571_W.mp3)
  Segment 38: 62272ms ~ 62848ms (file: 105-150-510-512-517-157-157-517-517-517-517-517-157-517-153-157-153-157-153_W.mp3)
  Segment 39: 63848ms ~ 64424ms (file: 310-301-361-123-213-231-130-103-216-513-517-123-123-263-635-726-761-176-317-312-132-301-326-632-576-765-605-706-650 675-103-256-236-162 762-237-217-721-217-231-123-213-217-213-6_W.mp3)
  Segment 40: 65424ms ~ 66000ms (file: 517-501-150-157-517-501-517-507-570_W.mp3)
  Segment 41: 67000ms ~ 67576ms (file: 671-153-157-157-571-153-153-157-532 514-153-517-157-517-157-157-153-157-153-150-153-157-510-517-513-153-517-157-517 154-154-153-513-514-514-517-513-517-157-157-517-514-157-510-5_W.mp3)
  Segment 42: 68576ms ~ 69152ms (file: 157-156-513-576-517-153-157-175-157-571-175-756-571-175-157-751-135-132-137-163-137-172-237-137-175-137-172-127-175-317-175-135-157-175-715-125-152-135-125-157-127-125-216-267-2_W.mp3)
  Segment 43: 70152ms ~ 70728ms (file: 132-132-157-157-176-157-175-315-125-135-312-132-231-132-231 175-135-157-173-137-137_W.mp3)
  Segment 44: 71728ms ~ 72304ms (file: 150-153-105-105-130-150-103-105-150-130-105-105-130-103-513-301-120-105-130-350-135-152-105 105-513-150-570-510-517-157-157-517-531-517-517-517-157-150-153-152-153-150-153-175-1_V.mp3)
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
> **Time**: 2026-04-05 15:29:25
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 2,
"transcript": "포커스 양보가 필요합니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "포커스", "start_ms": 10, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "양보가", "start_ms": 170, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "필요합니다", "start_ms": 360, "dur_ms": 200, "phase_lr_deg": 0.0}
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
"transcript": "다음 단계를 확인해 주세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "다음", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "단계를", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "확인해", "start_ms": 280, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 400, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "연습을 시작하도록 하겠습니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "연습을", "start_ms": 30, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "시작하도록", "start_ms": 160, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "하겠습니다", "start_ms": 370, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "준비가 되셨나요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "준비가", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "되셨나요", "start_ms": 200, "dur_ms": 320, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "천천히 따라해 보세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "천천히", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "따라해", "start_ms": 200, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "보세요", "start_ms": 330, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "아주 잘하고 있습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "아주", "start_ms": 20, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "잘하고", "start_ms": 130, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 280, "dur_ms": 250, "phase_lr_deg": 0.0}
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
"transcript": "목소리를 조금 더 높여볼까요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "목소리를", "start_ms": 60, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 250, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 340, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "높여볼까요", "start_ms": 400, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "발음에 주의해 주세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "발음에", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "주의해", "start_ms": 200, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 330, "dur_ms": 200, "phase_lr_deg": 0.0}
]
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
"transcript": "리듬에 맞춰서 불러보세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "리듬에", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "맞춰서", "start_ms": 160, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "불러보세요", "start_ms": 310, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "(mixed)",
"speaker": "speaker_1",
"confidence": 0.85,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "mixed",
"words": [
{"word": "반복", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "학습이", "start_ms": 160, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "필수입니다", "start_ms": 290, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "거의 다 끝났습니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "거의", "start_ms": 25, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 115, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "끝났습니다", "start_ms": 180, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "수고하셨습니다 오늘 수업 끝",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "수고하셨습니다", "start_ms": 120, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 380, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "수업", "start_ms": 490, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "끝", "start_ms": 580, "dur_ms": 100, "phase_lr_deg": 0.0}
]
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
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 19,
"transcript": "잠시만요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "잠시만요", "start_ms": 40, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 21,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 22,
"transcript": "다시 한 번 들어보세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "다시", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 140, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "번", "start_ms": 190, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "들어보세요", "start_ms": 260, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "이 부분은 조금 어렵죠",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 50, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "부분은", "start_ms": 110, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 240, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "어렵죠", "start_ms": 350, "dur_ms": 180, "phase_lr_deg": 0.0}
]
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
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
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
"transcript": "질문 있으시면 말씀해 주세요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "질문", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있으시면", "start_ms": 150, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "말씀해", "start_ms": 300, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 420, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "좋은 하루 보내세요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "하루", "start_ms": 140, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "보내세요", "start_ms": 260, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "안녕히 가세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 20, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 210, "dur_ms": 330, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "(mixed)",
"speaker": "speaker_2",
"confidence": 0.8,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "mixed",
"words": [
{"word": "감사합니다", "start_ms": 40, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "고생 많으셨습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "고생", "start_ms": 25, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "많으셨습니다", "start_ms": 135, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 33,
"transcript": "녹음을 종료합니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "녹음을", "start_ms": 40, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "종료합니다", "start_ms": 180, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 35,
"transcript": "정말 훌륭한 실력이네요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "훌륭한", "start_ms": 140, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "실력이네요", "start_ms": 290, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "(mixed)",
"speaker": "speaker_1",
"confidence": 0.75,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "mixed",
"words": [
{"word": "박수", "start_ms": 50, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "오늘의 학습 포인트입니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "오늘의", "start_ms": 20, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "학습", "start_ms": 140, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "포인트입니다", "start_ms": 240, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "잘 듣고 큰 소리로 말해보세요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "잘", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "듣고", "start_ms": 110, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 210, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "소리로", "start_ms": 280, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "말해보세요", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": 0.0}
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
"transcript": "잊지 말고 복습하세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "잊지", "start_ms": 30, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "말고", "start_ms": 130, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "복습하세요", "start_ms": 230, "dur_ms": 320, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "내일 이 시간에 뵙겠습니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "내일", "start_ms": 25, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 120, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "시간에", "start_ms": 170, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "뵙겠습니다", "start_ms": 300, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 43,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 44,
"transcript": "수업을 모두 마칩니다",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "수업을", "start_ms": 100, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "모두", "start_ms": 250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "마칩니다", "start_ms": 360, "dur_ms": 180, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 15:29:25*
