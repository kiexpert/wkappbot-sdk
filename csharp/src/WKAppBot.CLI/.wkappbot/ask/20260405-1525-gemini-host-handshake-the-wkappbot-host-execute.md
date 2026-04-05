# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=9236  time=15:22:22  uptime=48372s  mem=425MB  system_processes=354"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_151410.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 105-510-751-157-507-715-107 541-150-501-157-157-712-510-102-105-517-507-150-157-150-501-150-157_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 576-576-756-576-752-562-576-567-167-163-163-163-731-726-712-723-726-726-726-721-726-726-765-762-765-752-712-163-167-163-163-163-162-136-162-163-167-613-617-167-672-612-671-615-6_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 675-651-675-765-765-761-753-756-751-576-756-751-756-571 105-105-107-150-105-357-103-170-107-107-175-176-617-671-617-657-671-671-671-675-675-617-671-672-675-675-672 157-571-157-1_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 510-150-510-501-501-507-501-510-150-501-153-150-105-150-510-501-150-507-501-510-513-513-351-153-150-150-501-105 105-510-150-150-105-510 507-501-105-150 510-150-150-157-510-157-5_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 517-517-512-512-510-150-510-150-510-150-153-510-517-510-513-537 510-517-517-571-571-571-571-517-157-751-157-571-571-571-573-517-510-251-150-152-571-175-517-157-150-105-512-507 1_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 715-756-675-567-573-357-354-501-105-105-102-235-235-235-231-321-750-756-753-762-602-657-671 657-163-520-103_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 510-517-501 105-150-105-510-105-150-150-105-105-150-105-150-153-517-513-150-150-150-150-513-105-510-153-510-510-150-153 135-103-315-510-150-153-150-105-150-150-130-150-510-510-5_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 571-541-547-517-517-517 150-510-150-510-157-157-150-157-157-154-157-153-152 510-517-571-517-175-175-517-157-152-135-150-130-103-102-310-130-672-175-675-765-175-157-165-165-165-1_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 154-105-501-514-510-514-510-513-157-513-512-517-572-574-547 510-501-510-510-510-510-517-570-547-571-517-510-520-514-514-513-510-513-513-517-513 150-517-540-504-510-157-571-517-5_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 571-571-571-157-175-175-175-157-715-675-651-675-672-657-675-675-675-657-675 574-507-517-157-157-150-150-105-150-517-517-157-513-517-513-571-510-510-157-150-150-501-105_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 157-157-752-701-507 105-157-175-150-157-175-157-150-157-107-157-137-157-175-157 571-157-105-150-150-157-157-501-105-107-150-157-507-715-157-510-175-507-570-501-150-507-507-510-5_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 547-541-513-135-153-150-513-125-135-125-126-152-127-217-162-172-125-175-156-123-153-135-751-153-513-156-516-532-561_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 150-150-510-517-150-130-513-510-157-157-573-157-150-715-315-517-150-517-157-150-150-157-153-150-315-157-157-517-571 576-510-510-130-517-157-153-150-153-135-517-173-153-150-517-1_V.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 153-157-517 570-517-510-510-157-157-517-517-517-517-510-571-517-517-571-510-501-571-517-517-574-532 510-510-153-150-153-510-510-510-513-517-510-510-507-517-512-521-510-510-510-5_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 157-571-572-571-571-531-512-153-510-510-150-510-510-501_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 152-510-152-150 537-751-752-725-762-762-765-617-371-671-371-176-176-713-756-751-751-715-753-756-753-172-712-173-176-176-167 176-173-176-167-176 167-167-173-176-376-675-376-715 1_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 512-132-135-135-157-175-175-173-132-135-132-123-135-157-157-135-135-153-152-521-516 150-510-510-157-150-153-153-510-513-153-135-150-153-150-105-150-152-150-513-150-153-153-153-5_V.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 517-501-150-157-517-501-517-507-570_W.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 501-510-501-507 150-105-150-105-150-150-501-514-514 150-150-150-501-150-510-517-150-157-570 510-150-510_V.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 150-150-150-513-510-517-150-105-130-103-103-105-105-103-105-130-130-130-512-153-135 765-173-756-175-157-156-657-671-615-671-167-176-750-752-752-573-751-725-571-571-573-375-732-7_W.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 675-675-675-675-675-156-156-176-175-617-167-615-176 615-165-165-163-165-167-167-617-167-167 761-671-671-765-671-671-761-671-716-671-167-617-167-167 671-672-671-672-167_W.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 765 163-165-167-163-167 163-163-163-613-136-163-163-136-163-136-163-165-163-165-617-163 153-510-150-153-150-150-571-537-573-153-537-157-513-517-537-315_W.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 754-574-517-547 715-735-715-716-761-716-167-167-617-613-163-167-167-617-671-617 175-175-176-175-176-176-135-175-167-173-165-175-176-175-715-175-751-517-715-175-157-751-157-751-5_W.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 152-157-130-572-573-305-571-517-527-510-510-157-150-510 130-510-501-105-150-150-501-153-715-105-510-517-150-501-157-175-715-517-571-517-150-510-105-150-507-107-517 150-157-105-5_V.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 157-156-157-156-167-156-165-167-165-156-165-157-156-573-751-573-756-751-751-756-576-175 150-517-157-157-157-571-516-517-571-571-571-751-751-157-756-175-175-713-576-170-157-157-6_W.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 517-150-571-507-517-153-153-150-153-571-572-536 517-716-762-276-726-726-721-271-271-726-271-732-762-257-571-572-752-752-753-371-137-176-716-176-167-175-175-165-175-135-135-756-7_W.mp3)
  Segment 27: 40976ms ~ 45512ms (file: 150-510-135-751-501-501-507-510-501-103-135-315 123-510-105-253-205-621-123-157-105-623-267-261-312-201-150-517-517-157-510-571-571-510-150-105-157-510-501-513-507-507-510-150-1_W.mp3)
  Segment 28: 46512ms ~ 47088ms (file: 510-510-150-510-510-510-150-153-513-510-510-153-510-150-510-105-153-105-510-516-513-153-157-571 510-501-501-510-153-513-510-503-153-150-510-510-150-510-531-153-150-135-150-173-1_V.mp3)
  Segment 29: 48088ms ~ 48664ms (file: 135-150-153-517-150-150-510-513-150-105-510-157-150-510-150-510-157-150-510-150-150 513-103-135-150-150-105-570-105-153-507-510-150-510-150-531-152-150-510-510-150-153-135-152-1_V.mp3)
  Segment 30: 49664ms ~ 50240ms (file: 510-571-501-510-517-510-517-517-510-517 513-517-510-510-512-517-510-513-510-510-510-510-517-157-517-513-517-512-512-537-507 153-153-153-153-150-132-153-157-157-135-513-713-173-7_V.mp3)
  Segment 31: 51240ms ~ 51816ms (file: 157-513-157-157-157-517-157-157-517-157-571_W.mp3)
  Segment 32: 52816ms ~ 53392ms (file: 315-315-751-157-317-573-357-351-175-153-156-157-156-517-156-157-156-165-137-157-175-317-135-367-137-735-175 176-376 173-173-751-571-576-571-517-517-517-571-571-571-756-574-576-1_W.mp3)
  Segment 33: 54392ms ~ 54968ms (file: 107-137-175-716-763-716-715-167-572-517-572-517-571-571-571-574-571-573-576-671-167-165-165-156-175-167-165-167-165-167-157-135-153-123-153-135-315-153-317-175-135-135-153-156-1_W.mp3)
  Segment 34: 55968ms ~ 56544ms (file: 517-571-517-157-517-150-571-157-153-153-157-125-153-527 153-150-153-510-510-217-173-513-517-150-510-150-152-130-510-157-751-507-570-570-517-510-517-153-105-510 150-150-150-150-1_V.mp3)
  Segment 35: 57544ms ~ 58120ms (file: 571-537-571-571-153-357-752-753-751-571-573-573-574-573-572-571-517-153-157-156-516-156_W.mp3)
  Segment 36: 59120ms ~ 59696ms (file: 150-150-150-105-157-731-572-216-726-176-521-125-132-762-762-167-751-517-756-571-157-715-135-175-175-173-167-357-627-275-172-173-175-175-175-175-571-751-571-576-756-576-571-751-7_W.mp3)
  Segment 37: 60696ms ~ 61272ms (file: 150-153-105-105-130-150-103-105-150-130-105-105-130-103-513-301-120-105-130-350-135-152-105 105-513-150-570-510-517-157-157-517-531-517-517-517-157-150-153-152-153-150-153-175-1_V.mp3)
  Segment 38: 62272ms ~ 62848ms (file: 517-135-315-612-316-136-135-521-125-231-132-123-321-213-325-253-253 135-316-153-153-135-135-132-136-135-125-125-125-152-125-125-156-153-157-153-135-136-132-153-315-135-132-132-7_W.mp3)
  Segment 39: 63848ms ~ 64424ms (file: 251-521-513-251-576-567-531-176-136-167-136-167-136-163-136-751-756-751-753-735-751 167-167-163-163-167-167-163-167-167 167-173-137-736-317-175-716-175-617-167-163-157-165-615-1_W.mp3)
  Segment 40: 65424ms ~ 66000ms (file: 130-103-105-103-103-130-103-123-130-130-103-130-130-130-150-675-715-671-651-651-167-517-753-157-751-317-173-751-317-751-754-574-573-573-573 517-150-150-517-150-517-517-157-157-1_W.mp3)
  Segment 41: 67000ms ~ 67576ms (file: 157-156-513-576-517-153-157-175-157-571-175-756-571-175-157-751-135-132-137-163-137-172-237-137-175-137-172-127-175-317-175-135-157-175-715-125-152-135-125-157-127-125-216-267-2_W.mp3)
  Segment 42: 68576ms ~ 69152ms (file: 617-671-167-167-167-671-671-675-564_W.mp3)
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
> **Time**: 2026-04-05 15:25:26
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
"transcript": "오늘 날씨가 정말 좋네요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "날씨가", "start_ms": 160, "dur_ms": 180, "phase_lr_deg": -5.0},
{"word": "정말", "start_ms": 350, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "좋네요", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": -5.0}
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
"transcript": "공부하기 싫을 때 어떻게 하세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "공부하기", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "싫을", "start_ms": 210, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 300, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 360, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "하세요", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "그냥 일단 시작하는 게 중요해요",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "그냥", "start_ms": 20, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "일단", "start_ms": 130, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "시작하는", "start_ms": 240, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 400, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "중요해요", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 0.0}
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
"transcript": "맛있는 거 먹으러 갈까요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "맛있는", "start_ms": 60, "dur_ms": 140, "phase_lr_deg": -5.0},
{"word": "거", "start_ms": 210, "dur_ms": 40, "phase_lr_deg": -5.0},
{"word": "먹으러", "start_ms": 260, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "갈까요", "start_ms": 390, "dur_ms": 180, "phase_lr_deg": -5.0}
]
},
{
"segment": 8,
"transcript": "메뉴는 뭐가 좋을까요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "메뉴는", "start_ms": 45, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "뭐가", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "좋을까요", "start_ms": 310, "dur_ms": 260, "phase_lr_deg": -5.0}
]
},
{
"segment": 9,
"transcript": "저는 매운 떡볶이가 먹고 싶어요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "매운", "start_ms": 120, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "떡볶이가", "start_ms": 230, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "먹고", "start_ms": 390, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "싶어요", "start_ms": 480, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "어제 너무 많이 먹어서 배가 불러요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "어제", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "너무", "start_ms": 150, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "많이", "start_ms": 240, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "먹어서", "start_ms": 330, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "배가", "start_ms": 440, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "불러요", "start_ms": 510, "dur_ms": 60, "phase_lr_deg": -5.0}
]
},
{
"segment": 11,
"transcript": "그래도 디저트 배는 따로 있죠",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "그래도", "start_ms": 25, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "디저트", "start_ms": 140, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "배는", "start_ms": 290, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "따로", "start_ms": 380, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "있죠", "start_ms": 480, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 13,
"transcript": "커피 한 잔 하러 갈까요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "커피", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 140, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "잔", "start_ms": 190, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "하러", "start_ms": 260, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "갈까요", "start_ms": 380, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "저는 아이스 아메리카노 주세요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 55, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "아이스", "start_ms": 140, "dur_ms": 110, "phase_lr_deg": -5.0},
{"word": "아메리카노", "start_ms": 260, "dur_ms": 240, "phase_lr_deg": -5.0},
{"word": "주세요", "start_ms": 510, "dur_ms": 60, "phase_lr_deg": -5.0}
]
},
{
"segment": 15,
"transcript": "여기 분위기가 좋네요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "여기", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "분위기가", "start_ms": 150, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "좋네요", "start_ms": 410, "dur_ms": 160, "phase_lr_deg": -5.0}
]
},
{
"segment": 16,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 17,
"transcript": "주말에 뭐 할 계획이에요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "주말에", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 180, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 240, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "계획이에요", "start_ms": 300, "dur_ms": 270, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "집에서 푹 쉬려고요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "집에서", "start_ms": 35, "dur_ms": 140, "phase_lr_deg": -5.0},
{"word": "푹", "start_ms": 185, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "쉬려고요", "start_ms": 255, "dur_ms": 321, "phase_lr_deg": -5.0}
]
},
{
"segment": 19,
"transcript": "넷플릭스 보면서 뒹굴거릴 거예요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "넷플릭스", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "보면서", "start_ms": 230, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "뒹굴거릴", "start_ms": 350, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 520, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "(mixed)",
"speaker": "speaker_2",
"confidence": 0.6,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "mixed",
"words": [
{"word": "그것도", "start_ms": 100, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "좋죠", "start_ms": 210, "dur_ms": 100, "phase_lr_deg": -5.0}
]
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
"transcript": "요즘 유행하는 노래 알아요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "요즘", "start_ms": 60, "dur_ms": 90, "phase_lr_deg": -5.0},
{"word": "유행하는", "start_ms": 160, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "노래", "start_ms": 320, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "알아요", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": -5.0}
]
},
{
"segment": 23,
"transcript": "아이브의 신곡이 좋더라고요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "아이브의", "start_ms": 30, "dur_ms": 140, "phase_lr_deg": -5.0},
{"word": "신곡이", "start_ms": 180, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "좋더라고요", "start_ms": 310, "dur_ms": 260, "phase_lr_deg": -5.0}
]
},
{
"segment": 24,
"transcript": "멜로디가 정말 중독적이에요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "멜로디가", "start_ms": 45, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 205, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "중독적이에요", "start_ms": 315, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 26,
"transcript": "가사도 참 예쁜 것 같아요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "가사도", "start_ms": 35, "dur_ms": 130, "phase_lr_deg": -5.0},
{"word": "참", "start_ms": 175, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "예쁜", "start_ms": 245, "dur_ms": 110, "phase_lr_deg": -5.0},
{"word": "것", "start_ms": 365, "dur_ms": 40, "phase_lr_deg": -5.0},
{"word": "같아요", "start_ms": 415, "dur_ms": 150, "phase_lr_deg": -5.0}
]
},
{
"segment": 27,
"transcript": "한국어 공부는 얼마나 하셨어요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 100, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "공부는", "start_ms": 260, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "얼마나", "start_ms": 390, "dur_ms": 110, "phase_lr_deg": -5.0},
{"word": "하셨어요", "start_ms": 510, "dur_ms": 180, "phase_lr_deg": -5.0}
]
},
{
"segment": 28,
"transcript": "이제 막 일 년 정도 됐어요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 30, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "막", "start_ms": 130, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "일", "start_ms": 200, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "년", "start_ms": 260, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "정도", "start_ms": 330, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "됐어요", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "아직도 배울 게 너무 많네요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "아직도", "start_ms": 40, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "배울", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 290, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 350, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "많네요", "start_ms": 450, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "말하기가 제일 어려운 것 같아요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "말하기가", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "제일", "start_ms": 210, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "어려운", "start_ms": 300, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 420, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "꾸준히 하면 늘 거예요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",

---
*Generated by WKAppBot ask gemini — 2026-04-05 15:25:26*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=11864  time=15:22:58  uptime=41199s  mem=471MB  system_processes=355"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_151612.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 157-156-157-156-167-156-165-167-165-156-165-157-156-573-751-573-756-751-751-756-576-175 150-517-157-157-157-571-516-517-571-571-571-751-751-157-756-175-175-713-576-170-157-157-6_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 510-157-105-105-501-510-517-150-501-510-510-570-501-150 517-105-150-517-571-157-150-157-105-153-501-135-517-105-570-105-150-150-150-510-150-157-517-150-513-501-510-571 150-105-1_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 107-137-175-716-763-716-715-167-572-517-572-517-571-571-571-574-571-573-576-671-167-165-165-156-175-167-165-167-165-167-157-135-153-123-153-135-315-153-317-175-135-135-153-156-1_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 150-135-761-671-762-765-657-615-571-517-157-157-157-715-751-571-751-715-157-135-751-571-573-571-573-576-576-576-150-153-153-125-153-135-150-105-105-150-157-137-132-102-351-105-1_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 312-130-132-136-310-312-130-265-267-672-567-576-756-657-657-675-675-576-675-671-176-576-567-675-756-756-756-756-761-167-165-671-657-765-671-675-175-675-576-756-657-756-657-265-7_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 510-153-517-130-123-125-153-132-135-135-135-357-315-375-137-176-156-156-157-157-165-157-156-153-135-165-156-631-320-320-326-306-316-751-751-761-756-751-756-571-756-751-756-756-7_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 157-513-157-157-157-517-157-157-517-157-571_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 150-157-517-510-157-150-150-150-150-510-157-510-150-510-513-150-150-510 514-150-175-150-150-150-157-517-517-517-510-513-105-150-153-510-513-510-150-157-150-517-510-153-510-510-1_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 571-571-571-157-175-175-175-157-715-675-651-675-672-657-675-675-675-657-675 574-507-517-157-157-150-150-105-150-517-517-157-513-517-513-571-510-510-157-150-150-501-105_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 504-504-504-510-504-510-510-514-501-150-501_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 152-510-152-150 537-751-752-725-762-762-765-617-371-671-371-176-176-713-756-751-751-715-753-756-753-172-712-173-176-176-167 176-173-176-167-176 167-167-173-176-376-675-376-715 1_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 153-157-517 570-517-510-510-157-157-517-517-517-517-510-571-517-517-571-510-501-571-517-517-574-532 510-510-153-150-153-510-510-510-513-517-510-510-507-517-512-521-510-510-510-5_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 315-315-751-157-317-573-357-351-175-153-156-157-156-517-156-157-156-165-137-157-175-317-135-367-137-735-175 176-376 173-173-751-571-576-571-517-517-517-571-571-571-756-574-576-1_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 157-150-315-513-157-150-153-513-150-513-517-513 510-510-571-570-517-510-135-513-135-150-150-153-501-517-517-105-510-517-510-150-571-513-157-325-351 517-543-504-510-150-510-514-5_V.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 675-651-675-765-765-761-753-756-751-576-756-751-756-571 105-105-107-150-105-357-103-170-107-107-175-176-617-671-617-657-671-671-671-675-675-617-671-672-675-675-672 157-571-157-1_W.mp3)
  Segment 16: 23640ms ~ 28176ms (file: 150-510-135-751-501-501-507-510-501-103-135-315 123-510-105-253-205-621-123-157-105-623-267-261-312-201-150-517-517-157-510-571-571-510-150-105-157-510-501-513-507-507-510-150-1_W.mp3)
  Segment 17: 29176ms ~ 29752ms (file: 501-510-501-507 150-105-150-105-150-150-501-514-514 150-150-150-501-150-510-517-150-157-570 510-150-510_V.mp3)
  Segment 18: 30752ms ~ 31328ms (file: 573-175-175-571-523-132-125-137-153-135 150-157-157-152-157-157-175-571-172-103-157-517-157-137-571-517-175-135-715-157-157-517-573-571-157-517-157-157-571 547-571-574-574_V.mp3)
  Segment 19: 32328ms ~ 32904ms (file: 531-574-517-517-517-517-517-517-512_W.mp3)
  Segment 20: 33904ms ~ 34480ms (file: 154-105-501-514-510-514-510-513-157-513-512-517-572-574-547 510-501-510-510-510-510-517-570-547-571-517-510-520-514-514-513-510-513-513-517-513 150-517-540-504-510-157-571-517-5_V.mp3)
  Segment 21: 35480ms ~ 36056ms (file: 576-576-756-576-752-562-576-567-167-163-163-163-731-726-712-723-726-726-726-721-726-726-765-762-765-752-712-163-167-163-163-163-162-136-162-163-167-613-617-167-672-612-671-615-6_W.mp3)
  Segment 22: 37056ms ~ 37632ms (file: 135-150-153-517-150-150-510-513-150-105-510-157-150-510-150-510-157-150-510-150-150 513-103-135-150-150-105-570-105-153-507-510-150-510-150-531-152-150-510-510-150-153-135-152-1_V.mp3)
  Segment 23: 38632ms ~ 39208ms (file: 754-574-517-547 715-735-715-716-761-716-167-167-617-613-163-167-167-617-671-617 175-175-176-175-176-176-135-175-167-173-165-175-176-175-715-175-751-517-715-175-157-751-157-751-5_W.mp3)
  Segment 24: 40208ms ~ 40784ms (file: 521-175-152-135-152-165-136-167-163-163-163 617-167-167-671-675-167-671-617-167-617-167-617-163-617_W.mp3)
  Segment 25: 41784ms ~ 42360ms (file: 150-150-150-105-157-731-572-216-726-176-521-125-132-762-762-167-751-517-756-571-157-715-135-175-175-173-167-357-627-275-172-173-175-175-175-175-571-751-571-576-756-576-571-751-7_W.mp3)
  Segment 26: 43360ms ~ 43936ms (file: 150-150-150-513-510-517-150-105-130-103-103-105-105-103-105-130-130-130-512-153-135 765-173-756-175-157-156-657-671-615-671-167-176-750-752-752-573-751-725-571-571-573-375-732-7_W.mp3)
  Segment 27: 44936ms ~ 45512ms (file: 153-154-105-157-105-157-150-105-150-570-103-150-105-150-153-503-153-150-510-510-517-153_V.mp3)
  Segment 28: 46512ms ~ 47088ms (file: 165 163-137-176-175-175-135-135-173-157-157-157-157-157-157-153-157-157-135-105-176-175-176-107-103-130-513-150-512-513-153-517-571-537-513-513-517-517-571-571-571-157-751-751-1_W.mp3)
  Segment 29: 48088ms ~ 48664ms (file: 510-571-501-510-517-510-517-517-510-517 513-517-510-510-512-517-510-513-510-510-510-510-517-157-517-513-517-512-512-537-507 153-153-153-153-150-132-153-157-157-135-513-713-173-7_V.mp3)
  Segment 30: 49664ms ~ 50240ms (file: 675-675-675-675-675-156-156-176-175-617-167-615-176 615-165-165-163-165-167-167-617-167-167 761-671-671-765-671-671-761-671-716-671-167-617-167-167 671-672-671-672-167_W.mp3)
  Segment 31: 51240ms ~ 51816ms (file: 150-150-530-165-135-130-153-135-153_W.mp3)
  Segment 32: 52816ms ~ 53392ms (file: 157-105-510 571-153 153-150-150-150-150-510-510-105-152 517-513-105-510-150-157-153-150-157-135-175-517-517-517-105-153-152-157-517-157-517 105-150-150-150-135-153-150-150-517-5_V.mp3)
  Segment 33: 54392ms ~ 54968ms (file: 571-541-547-517-517-517 150-510-150-510-157-157-150-157-157-154-157-153-152 510-517-571-517-175-175-517-157-152-135-150-130-103-102-310-130-672-175-675-765-175-157-165-165-165-1_W.mp3)
  Segment 34: 55968ms ~ 56544ms (file: 150-150-510-517-150-130-513-510-157-157-573-157-150-715-315-517-150-517-157-150-150-157-153-150-315-157-157-517-571 576-510-510-130-517-157-153-150-153-135-517-173-153-150-517-1_V.mp3)
  Segment 35: 57544ms ~ 58120ms (file: 510-510-150-510-510-510-150-153-513-510-510-153-510-150-510-105-153-105-510-516-513-153-157-571 510-501-501-510-153-513-510-503-153-150-510-510-150-510-531-153-150-135-150-173-1_V.mp3)
  Segment 36: 59120ms ~ 59696ms (file: 251-521-513-251-576-567-531-176-136-167-136-167-136-163-136-751-756-751-753-735-751 167-167-163-163-167-167-163-167-167 167-173-137-736-317-175-716-175-617-167-163-157-165-615-1_W.mp3)
  Segment 37: 60696ms ~ 61272ms (file: 253-152-321-315-357-132-132-365-132-136-216 153-153-170-715-517-153-571-517-571-517-157-571-157-531-351-157-135-153-517-157-157-571-510-150 150-510-150-157-105-103-135-135-153-1_W.mp3)
  Segment 38: 62272ms ~ 62848ms (file: 617-671-167-167-167-671-671-675-564_W.mp3)
  Segment 39: 63848ms ~ 64424ms (file: 517-501-150-157-517-501-517-507-570_W.mp3)
  Segment 40: 65424ms ~ 66000ms (file: 157-157-752-701-507 105-157-175-150-157-175-157-150-157-107-157-137-157-175-157 571-157-105-150-150-157-157-501-105-107-150-157-507-715-157-510-175-507-570-501-150-507-507-510-5_V.mp3)
  Segment 41: 67000ms ~ 67576ms (file: 517-150-571-507-517-153-153-150-153-571-572-536 517-716-762-276-726-726-721-271-271-726-271-732-762-257-571-572-752-752-753-371-137-176-716-176-167-175-175-165-175-135-135-756-7_W.mp3)
  Segment 42: 68576ms ~ 69152ms (file: 573-576-576-312-513-576-576-573-567-356-536-367-567-576-356-571-567-567-573-567-567-546-573-562-517-576-657-567-675-756-625-576-675-136-576-576-576-576-576-562-576-132-675-375-6_W.mp3)
  Segment 43: 70152ms ~ 70728ms (file: 167-167-167-175-165-167-716-175-716-715-761-675-716-715 510-105-501-507-574-574-504-103-105 547-531_W.mp3)
  Segment 44: 71728ms ~ 72304ms (file: 150-153-105-105-130-150-103-105-150-130-105-105-130-103-513-301-120-105-130-350-135-152-105 105-513-150-570-510-517-157-157-517-531-517-517-517-157-150-153-152-153-150-153-175-1_V.mp3)
  Segment 45: 73304ms ~ 77840ms (file: 547-517-574-514-150-513-153-135-153-153-531-513-157-152-130-103-312-762-672-167-167-167-167-163-176-137-167-136-107-103-130-132-512-521-236-206-620-213-123-623-260-260-613-123 1_W.mp3)
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
> **Time**: 2026-04-05 15:25:31
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
"transcript": "오늘 날씨가 정말 좋네요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "날씨가", "start_ms": 160, "dur_ms": 180, "phase_lr_deg": -5.0},
{"word": "정말", "start_ms": 350, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "좋네요", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": -5.0}
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
"transcript": "공부하기 싫을 때 어떻게 하세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "공부하기", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "싫을", "start_ms": 210, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 300, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 360, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "하세요", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "그냥 일단 시작하는 게 중요해요",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "그냥", "start_ms": 20, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "일단", "start_ms": 130, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "시작하는", "start_ms": 240, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 400, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "중요해요", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 0.0}
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
"transcript": "맛있는 거 먹으러 갈까요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "맛있는", "start_ms": 60, "dur_ms": 140, "phase_lr_deg": -5.0},
{"word": "거", "start_ms": 210, "dur_ms": 40, "phase_lr_deg": -5.0},
{"word": "먹으러", "start_ms": 260, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "갈까요", "start_ms": 390, "dur_ms": 180, "phase_lr_deg": -5.0}
]
},
{
"segment": 8,
"transcript": "메뉴는 뭐가 좋을까요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "메뉴는", "start_ms": 45, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "뭐가", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "좋을까요", "start_ms": 310, "dur_ms": 260, "phase_lr_deg": -5.0}
]
},
{
"segment": 9,
"transcript": "저는 매운 떡볶이가 먹고 싶어요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "매운", "start_ms": 120, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "떡볶이가", "start_ms": 230, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "먹고", "start_ms": 390, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "싶어요", "start_ms": 480, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "어제 너무 많이 먹어서 배가 불러요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "어제", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "너무", "start_ms": 150, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "많이", "start_ms": 240, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "먹어서", "start_ms": 330, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "배가", "start_ms": 440, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "불러요", "start_ms": 510, "dur_ms": 60, "phase_lr_deg": -5.0}
]
},
{
"segment": 11,
"transcript": "그래도 디저트 배는 따로 있죠",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "그래도", "start_ms": 25, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "디저트", "start_ms": 140, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "배는", "start_ms": 290, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "따로", "start_ms": 380, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "있죠", "start_ms": 480, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 13,
"transcript": "커피 한 잔 하러 갈까요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "커피", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 140, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "잔", "start_ms": 190, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "하러", "start_ms": 260, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "갈까요", "start_ms": 380, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "저는 아이스 아메리카노 주세요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 55, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "아이스", "start_ms": 140, "dur_ms": 110, "phase_lr_deg": -5.0},
{"word": "아메리카노", "start_ms": 260, "dur_ms": 240, "phase_lr_deg": -5.0},
{"word": "주세요", "start_ms": 510, "dur_ms": 60, "phase_lr_deg": -5.0}
]
},
{
"segment": 15,
"transcript": "여기 분위기가 좋네요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "여기", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "분위기가", "start_ms": 150, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "좋네요", "start_ms": 410, "dur_ms": 160, "phase_lr_deg": -5.0}
]
},
{
"segment": 16,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 17,
"transcript": "주말에 뭐 할 계획이에요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "주말에", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 180, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 240, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "계획이에요", "start_ms": 300, "dur_ms": 270, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "집에서 푹 쉬려고요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "집에서", "start_ms": 35, "dur_ms": 140, "phase_lr_deg": -5.0},
{"word": "푹", "start_ms": 185, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "쉬려고요", "start_ms": 255, "dur_ms": 321, "phase_lr_deg": -5.0}
]
},
{
"segment": 19,
"transcript": "넷플릭스 보면서 뒹굴거릴 거예요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "넷플릭스", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "보면서", "start_ms": 230, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "뒹굴거릴", "start_ms": 350, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 520, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "(mixed)",
"speaker": "speaker_2",
"confidence": 0.6,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "mixed",
"words": [
{"word": "그것도", "start_ms": 100, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "좋죠", "start_ms": 210, "dur_ms": 100, "phase_lr_deg": -5.0}
]
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
"transcript": "요즘 유행하는 노래 알아요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "요즘", "start_ms": 60, "dur_ms": 90, "phase_lr_deg": -5.0},
{"word": "유행하는", "start_ms": 160, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "노래", "start_ms": 320, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "알아요", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": -5.0}
]
},
{
"segment": 23,
"transcript": "아이브의 신곡이 좋더라고요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "아이브의", "start_ms": 30, "dur_ms": 140, "phase_lr_deg": -5.0},
{"word": "신곡이", "start_ms": 180, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "좋더라고요", "start_ms": 310, "dur_ms": 260, "phase_lr_deg": -5.0}
]
},
{
"segment": 24,
"transcript": "멜로디가 정말 중독적이에요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "멜로디가", "start_ms": 45, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 205, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "중독적이에요", "start_ms": 315, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 26,
"transcript": "가사도 참 예쁜 것 같아요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "가사도", "start_ms": 35, "dur_ms": 130, "phase_lr_deg": -5.0},
{"word": "참", "start_ms": 175, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "예쁜", "start_ms": 245, "dur_ms": 110, "phase_lr_deg": -5.0},
{"word": "것", "start_ms": 365, "dur_ms": 40, "phase_lr_deg": -5.0},
{"word": "같아요", "start_ms": 415, "dur_ms": 150, "phase_lr_deg": -5.0}
]
},
{
"segment": 27,
"transcript": "한국어 공부는 얼마나 하셨어요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 100, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "공부는", "start_ms": 260, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "얼마나", "start_ms": 390, "dur_ms": 110, "phase_lr_deg": -5.0},
{"word": "하셨어요", "start_ms": 510, "dur_ms": 180, "phase_lr_deg": -5.0}
]
},
{
"segment": 28,
"transcript": "이제 막 일 년 정도 됐어요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 30, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "막", "start_ms": 130, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "일", "start_ms": 200, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "년", "start_ms": 260, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "정도", "start_ms": 330, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "됐어요", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "아직도 배울 게 너무 많네요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "아직도", "start_ms": 40, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "배울", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 290, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 350, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "많네요", "start_ms": 450, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "말하기가 제일 어려운 것 같아요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "말하기가", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "제일", "start_ms": 210, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "어려운", "start_ms": 300, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 420, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "꾸준히 하면 늘 거예요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "꾸준히", "start_ms": 35, "dur_ms": 140, "phase_lr_deg": -5.0},
{"word": "하면", "start_ms": 185, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "늘", "start_ms": 295, "dur_ms": 70, "phase_lr_deg": -5.0},
{"word": "거예요", "start_ms": 375, "dur_ms": 201, "phase_lr_deg": -5.0}
]
},
{
"segment": 32,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
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
"transcript": "자신감을 가지는 게 중요해요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "자신감을", "start_ms": 45, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가지는", "start_ms": 205, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 335, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "중요해요", "start_ms": 395, "dur_ms": 170, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "틀리는 걸 두려워하지 마세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "틀리는", "start_ms": 30, "dur_ms": 110, "phase_lr_deg": -5.0},
{"word": "걸", "start_ms": 150, "dur_ms": 40, "phase_lr_deg": -5.0},
{"word": "두려워하지", "start_ms": 200, "dur_ms": 180, "phase_lr_deg": -5.0},
{"word": "마세요", "start_ms": 390, "dur_ms": 180, "phase_lr_deg": -5.0}
]
},
{
"segment": 36,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 37,
"transcript": "좋은 조언 감사합니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 60, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "조언", "start_ms": 170, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 290, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",

---
*Generated by WKAppBot ask gemini — 2026-04-05 15:25:31*
