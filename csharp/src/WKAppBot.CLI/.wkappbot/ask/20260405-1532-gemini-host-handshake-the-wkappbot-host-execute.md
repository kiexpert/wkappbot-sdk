# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=30524  time=15:31:27  uptime=39599s  mem=343MB  system_processes=355"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_153116.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 716-135-125-125-172-126-127-163-162-621-617-723-726-237-127 716-715-761-761-716-761-716-756-715 136-130-326-130-136-132-125-132-123-123-123-162-317-102-621-106-103-102-210-132-5_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 510-150-150-157-150-517-150-150-153-152-153-132-315-135-130-132-175-107-175-157 510-150-153-153-150-150-153-150-157-150-150-135-150-153-105-150-153-153-152-157 573-507-510-510-5_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 170-517-517-571-517-175-157-157-160-517-513-517-572-517-576-576-567-567-567-567-165-615-617-657-657-275-672-657-627-637-127-126-127-156-165-165-136-165-165-137-130-156-561-651-1_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 501-510-501-507 150-105-150-105-150-150-501-514-514 150-150-150-501-150-510-517-150-157-570 510-150-510_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 153-154-105-157-105-157-150-105-150-570-103-150-105-150-153-503-153-150-510-510-517-153_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 167-167-167-175-165-167-716-175-716-715-761-675-716-715 510-105-501-507-574-574-504-103-105 547-531_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 150-105-150-105-501-157-510-501-105-150-150-105-501-517-150-510-150-150-513-501-501-510-501-510-150-510-501 105-105-150-105-150-150-571-510-510-175-175-157-517-510-501-507-570-5_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 130-103-105-103-103-130-103-123-130-130-103-130-130-130-150-675-715-671-651-651-167-517-753-157-751-317-173-751-317-751-754-574-573-573-573 517-150-150-517-150-517-517-157-157-1_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 326-317-173-376-375-157-517-310-310-635-163-675-156-612-723-315-136-175-137-103-132-132-310-312-321-312-321-327-571-571-752-517-576-657-571-573-510-567-157-136-536-271-271-271-2_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 517-150-571-507-517-153-153-150-153-571-572-536 517-716-762-276-726-726-721-271-271-726-271-732-762-257-571-572-752-752-753-371-137-176-716-176-167-175-175-165-175-135-135-756-7_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 510-510-153-510-150-517-510-510-531-563-516-517-517-156-135-165-156-560-156-510-510-150-516-561-156-513-516 150-150-135-153-513-153-531-150-510-150-150-510-150-150-513-135-150-1_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 153-325-315-352-315-352-315-253-375-351-375-172-172-127-172-137-175-175-172-172-172-735-175-715-753-173-735-317-375-735-715-137-136-137-136-132-163-175-135-157-175_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 251-521-513-251-576-567-531-176-136-167-136-167-136-163-136-751-756-751-753-735-751 167-167-163-163-167-167-163-167-167 167-173-137-736-317-175-716-175-617-167-163-157-165-615-1_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 157-150-315-513-157-150-153-513-150-513-517-513 510-510-571-570-517-510-135-513-135-150-150-153-501-517-517-105-510-517-510-150-571-513-157-325-351 517-543-504-510-150-510-514-5_V.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 617-671-167-167-167-671-671-675-564_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 135-150-153-517-150-150-510-513-150-105-510-157-150-510-150-510-157-150-510-150-150 513-103-135-150-150-105-570-105-153-507-510-150-510-150-531-152-150-510-510-150-153-135-152-1_V.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 150-571-571-517-150-150-750-507-507-105-105-501-150 501-501-517-510-570-517 157-715-175-150-150-517-501-507-150-501-501-150-105-103-510-501-507-105-150-157-150-510-157-526 501-1_V.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 576-576-756-576-752-562-576-567-167-163-163-163-731-726-712-723-726-726-726-721-726-726-765-762-765-752-712-163-167-163-163-163-162-136-162-163-167-613-617-167-672-612-671-615-6_W.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 752-751-721-765 517 105-107-150-105-510-105-175-157-175-170-157-150-105-715-715-715-107-571-157-751-715-157-517-175-157-157-510-517-571 105-105-107-150-501-150-710-175-170-375-1_V.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 135-153-135-531-315-157-715-163-156-157-156-157-153-135-157-135-136-357-351-357-351-157-317-753-153-153-753-157-375-513-157-153-153-153-135-135-153-357-257-357-513-531-351-357-3_W.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 501-504-504-541-574-571-571-510-517-153-152-531-153-153-153-153-154-153-153-153-153-153-153-153 153-135-157-153-152-513-153-315-153-157-513-127-175-157-751-315-157 517-153-152-1_W.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 510-517-501 105-150-105-510-105-150-150-105-105-150-105-150-153-517-513-150-150-150-150-513-105-510-153-510-510-150-153 135-103-315-510-150-153-150-105-150-150-130-150-510-510-5_W.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 150-150-150-513-510-517-150-105-130-103-103-105-105-103-105-130-130-130-512-153-135 765-173-756-175-157-156-657-671-615-671-167-176-750-752-752-573-751-725-571-571-573-375-732-7_W.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 571-541-547-517-517-517 150-510-150-510-157-157-150-157-157-154-157-153-152 510-517-571-517-175-175-517-157-152-135-150-130-103-102-310-130-672-175-675-765-175-157-165-165-165-1_W.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 150-157-517-510-157-150-150-150-150-510-157-510-150-510-513-150-150-510 514-150-175-150-150-150-157-517-517-517-510-513-105-150-153-510-513-510-150-157-150-517-510-153-510-510-1_V.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 173-137 173-156-157-165-167-157-157-176-157 156-156-157-157-517-517-517-517-513-517-153-513-153-153-153_W.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 253-152-321-315-357-132-132-365-132-136-216 153-153-170-715-517-153-571-517-571-517-157-571-157-531-351-157-135-153-517-157-157-571-510-150 150-510-150-157-105-103-135-135-153-1_W.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 152-510-152-150 537-751-752-725-762-762-765-617-371-671-371-176-176-713-756-751-751-715-753-756-753-172-712-173-176-176-167 176-173-176-167-176 167-167-173-176-376-675-376-715 1_W.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 327-301-567-516-215-537 173-135-135-105-153-126-152-132-103-136-163-167-531-315-102 132-321-623-325-625-263-513-156-317-567-526-561-573-567-571-376-312-162-127-362-315-571-576-3_W.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 716-167-716-675 167-176-176-761-176-157-175-175-175-157-157 157-751-571-175-517-517-517-157-153-157-153 517-571-517-510-157-517-153-153-157-517-517-510-517-510-150-517-351_W.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 107-137-175-716-763-716-715-167-572-517-572-517-571-571-571-574-571-573-576-671-167-165-165-156-175-167-165-167-165-167-157-135-153-123-153-135-315-153-317-175-135-135-153-156-1_W.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 504-504-504-510-504-510-510-514-501-150-501_W.mp3)
  Segment 33: 50432ms ~ 54716ms (file: 103-103-316-312 123-123-102-103-203-620-260-210-132-301-260-103-132_V.mp3)
  Segment 34: 55716ms ~ 56292ms (file: 154-105-501-514-510-514-510-513-157-513-512-517-572-574-547 510-501-510-510-510-510-517-570-547-571-517-510-520-514-514-513-510-513-513-517-513 150-517-540-504-510-157-571-517-5_V.mp3)
  Segment 35: 57292ms ~ 57868ms (file: 312-130-132-136-310-312-130-265-267-672-567-576-756-657-657-675-675-576-675-671-176-576-567-675-756-756-756-756-761-167-165-671-657-765-671-675-175-675-576-756-657-756-657-265-7_W.mp3)
  Segment 36: 58868ms ~ 63044ms (file: 314-316 126-103-236-102-132-123-103-301-216_V.mp3)
  Segment 37: 64044ms ~ 68580ms (file: 547-517-574-514-150-513-153-135-153-153-531-513-157-152-130-103-312-762-672-167-167-167-167-163-176-137-167-136-107-103-130-132-512-521-236-206-620-213-123-623-260-260-613-123 1_W.mp3)
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
> **Time**: 2026-04-05 15:32:31
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
"transcript": "잠시만 기다려 주십시오",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "잠시만", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "기다려", "start_ms": 170, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "주십시오", "start_ms": 330, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "이제 시작해도 될까요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "시작해도", "start_ms": 140, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "될까요", "start_ms": 330, "dur_ms": 210, "phase_lr_deg": 0.0}
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
"transcript": "네 준비가 되었습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 20, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "준비가", "start_ms": 110, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "되었습니다", "start_ms": 270, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 9,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 10,
"transcript": "목소리가 아주 잘 들리네요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "목소리가", "start_ms": 45, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 235, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 325, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "들리네요", "start_ms": 395, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "마이크 테스트 중입니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "마이크", "start_ms": 25, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "테스트", "start_ms": 175, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "중입니다", "start_ms": 345, "dur_ms": 200, "phase_lr_deg": 0.0}
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
"transcript": "(mixed)",
"speaker": "speaker_2",
"confidence": 0.85,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "mixed",
"words": [
{"word": "하나", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "둘", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "셋", "start_ms": 290, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "볼륨을 조금만 높여볼까요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "볼륨을", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "조금만", "start_ms": 165, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "높여볼까요", "start_ms": 305, "dur_ms": 230, "phase_lr_deg": 0.0}
]
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
"transcript": "이제 완벽하게 들립니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "완벽하게", "start_ms": 140, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "들립니다", "start_ms": 310, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "녹음을 시작하도록 하겠습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "녹음을", "start_ms": 20, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "시작하도록", "start_ms": 160, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "하겠습니다", "start_ms": 380, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 19,
"transcript": "첫 번째 문장을 읽어주세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "첫", "start_ms": 50, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "번째", "start_ms": 120, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "문장을", "start_ms": 230, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "읽어주세요", "start_ms": 380, "dur_ms": 170, "phase_lr_deg": 0.0}
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
"transcript": "한국어 공부는 즐겁습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "공부는", "start_ms": 200, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "즐겁습니다", "start_ms": 330, "dur_ms": 210, "phase_lr_deg": 0.0}
]
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
"transcript": "(mixed)",
"speaker": "speaker_2",
"confidence": 0.7,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "mixed",
"words": [
{"word": "참", "start_ms": 100, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "잘하셨어요", "start_ms": 190, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "발음이 정말 정확하시네요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "발음이", "start_ms": 30, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 160, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "정확하시네요", "start_ms": 270, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "감사합니다 더 열심히 할게요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 20, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 230, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "열심히", "start_ms": 300, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "할게요", "start_ms": 450, "dur_ms": 110, "phase_lr_deg": 0.0}
]
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
"transcript": "다음 문장으로 넘어가겠습니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "다음", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "문장으로", "start_ms": 155, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "넘어가겠습니다", "start_ms": 315, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 29,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
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
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 32,
"transcript": "오늘 수업은 여기까지입니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "수업은", "start_ms": 140, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "여기까지입니다", "start_ms": 290, "dur_ms": 270, "phase_lr_deg": 0.0}
]
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
"transcript": "모두 고생 많으셨습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "모두", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "고생", "start_ms": 160, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "많으셨습니다", "start_ms": 290, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
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
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 38,
"transcript": "안녕히 가세요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 20, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 210, "dur_ms": 350, "phase_lr_deg": 0.0}
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
"transcript": "내일 또 만나요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "내일", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 150, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 220, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "감사합니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 30, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "(mixed)",
"speaker": "speaker_2",
"confidence": 0.75,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "mixed",
"words": [
{"word": "좋은", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "하루", "start_ms": 160, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "보내세요", "start_ms": 280, "dur_ms": 250, "phase_lr_deg": 0.0}
]
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
"transcript": "녹음을 종료합니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "녹음을", "start_ms": 100, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "종료합니다", "start_ms": 250, "dur_ms": 300, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 15:32:31*
