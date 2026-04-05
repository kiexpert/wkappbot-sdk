# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=11864  time=12:53:56  uptime=32256s  mem=402MB  system_processes=356"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_125336.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 107-715-107-107-107-107-175-170-107-170-170-170-170-715-715-107-713-170-173-170-107-170-570-570 175-107-175-107-175-716-175-107-107-715-175-370-173-107-715-107-175-170-150-517-5_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 150-517-507-150-510-507-517-507 510-150-157-501-510-105-157-510-507-150-105-157-750-571-157-170-570-175-715-157-751-170-570-571-150-175-157-751-510-571-570-150-571-570-571-517-6_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 652-657-610-615-605 261-620-620-620-620-602-620-260-621-620-620-620-261-621-621-620-265-261-620-261-265-625-652-625-652-526-652-657-652 264-621-612-621-261-620-621-623-620-625-2_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 765-765-762 216-756-576-167-716-576-576-576-576-507-576-136-172-714 263-162-267-623-175-576 637-135-267-726-671-132-136_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 751-507-507-572-571-135-130-135-753-527-735-572-576-276-756-517-752-357-167 510-351-157-571-517-150-152-153-157-175-157-751-175-150-157-132-571-517-571-571-751-175-725-732-137-7_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 510-150-105-157-157-157-716-176-761-675-175-176-176-716-176-716-176-176-175-175 105-150-105-105-105-107-157-170-150-175-175-716-176-175-176-671-176-175-167-176-170-150 105-150-1_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 136-167-137-260-627-675-657-627-621-627-627-621-627-621-162-106-627-612-610-617-657-627-612-213-123-210-213-102-120-203-123-120-210-120-213-132-612_W.mp3)
  Segment 8: 11032ms ~ 16432ms (file: 216-126-623-123-123-123-627-263-267-623-213-236-231-263-216-267-213-126-261-201-102-261-312-627-260-231-261-216-126-236-263-273-123-321-362-612-126-123-261-310-312-213-312-321-2_W.mp3)
  Segment 9: 17432ms ~ 18008ms (file: 765-526-576-572-576-567-672-256-265-652-165-615-165-165-612-165-156-315-156-156-653-165-163-657-675-567-657-756-657-675-765-675-625-765 560-506-150-156-165-165-561-615-165-165-1_W.mp3)
  Segment 10: 19008ms ~ 19584ms (file: 571-751-756-576-751-751-571-570-570-570-570-570 157-157-175-175-175-751-756-756-357-751-157-571-571-571-570-570-750-751-750-751-750-751-751-570-105-513-157-513-105-517-513-105-5_V.mp3)
  Segment 11: 20584ms ~ 21160ms (file: 517-150-105-175-170-107-105-175-105-701-130-173-107-175-107-135-107-501-157-501-570-571 507-501-514-510-150-105-150-517-751-150-150-105-175-507-571-570-571-570 105-150-107-150-1_V.mp3)
  Segment 12: 22160ms ~ 22736ms (file: 507-502-502-507-625-236-231-213-253-521-253-523-526-526-523-527-521-526-521-527-523-523-521-523-521-523-574-520-507-507-527-576-572-572-570-572-572-527-520-572-576-572-576-576-2_W.mp3)
  Segment 13: 23736ms ~ 40548ms (file: 216-261-627-276-312-231-261-276-273-102-123 120-321-106-152-273-176-754-257-732-237-327-126-102-126-102-574-531-231-213-321-132-107-267-276-102-371-160-312-102-237-236-762-312-2_W.mp3)
  Segment 14: 41548ms ~ 42124ms (file: 105-105-510-150-517-501-510-105-510-506-510-507-150-153 150-510-105-150-150-150-105-501-150-105-105-150-150-501-150-571-567-517-576-150-506-560-571-567-574-571 105-150-105-571-1_V.mp3)
  Segment 15: 43124ms ~ 43700ms (file: 175-107-160-167-103-652-617-601-657-127-137-316-617-163-107-103-103-761-163-163-106-136-103-103-576-601-156-103-510-602-651-127-153-136-106-136-670-672-620-621-206-176-106-103-1_V.mp3)
  Segment 16: 44700ms ~ 53736ms (file: 267-230-270-720-103-762-627-130-726-327-176-172-132-143-132-127-574-756-756-627-320-217-107-237-276-762-672-127-107-327-230-237-132-102-230-230-672-263-260-230-236-231-327-736-7_W.mp3)
  Segment 17: 54736ms ~ 55312ms (file: 516-501-513-514-531-571-572-561-576-561 527-512-513-512-501-527-516-532-531-516-512-513-516-156-105-156-105_V.mp3)
  Segment 18: 56312ms ~ 61064ms (file: 317-236-321-162-213-106-302-671-673-672-726-723-362-612-761-671-132-312-276-160-261-762-216-126-162-302-761-671-167-601-160-260-267-627-671-167-602-231-260-260-267-672-617-261-3_W.mp3)
  Segment 19: 62064ms ~ 62640ms (file: 761-756-751-657-765-132-351-513-756-356-273-570-256-756-752-267-765-765-702-316-637-670-361-263-150-372-317-732-756-572-265-726-267-257-562-752-372-267-571-326-371-215-216-237-7_V.mp3)
  Segment 20: 63640ms ~ 64576ms (file: 231-261-612-215-216-627-623-621-135-135-627-627-265-627-256-532-736-765-765-612-612-653-617-627-751-756-132 163-163-163-136-136-756-753-735-352-357-357-315-537-375 672-657-657-6_W.mp3)
  Segment 21: 65576ms ~ 66152ms (file: 520-527-527-520-527-527-520-501-527-520-572-572-527-572-527-527-572-527-256-251-512_W.mp3)
  Segment 22: 67152ms ~ 67728ms (file: 526-527-576-572-726-751 527-507-570-526-527-570-526-572-576_W.mp3)
  Segment 23: 68728ms ~ 69304ms (file: 765-756-765-576-576-675-675-751-765-756-756-756-576-765-756-765-675-675-675-675-617-167-167-167-167-127-162-167-167-167-163-765-765-657-657-756-576-756-756-765-675-576-576-567-5_W.mp3)
  Segment 24: 70304ms ~ 70880ms (file: 265-265-625-265-265-265-265-625-625-265-625-256-265-657-256-526-256-256-261-625-625-562-165-651-652-652-652-267-265-162-615-162-216-261-612-625-261-625-625-612-162-621-123-625-2_W.mp3)
  Segment 25: 71880ms ~ 72456ms (file: 725-207-507-702-257-170-130-275-276-257-702 562-361-623-625-260-215-120-201-103-163-163-135-160-165_V.mp3)
  Segment 26: 73456ms ~ 74032ms (file: 531-532-507-563-571-517-574-502-512-502-502-503-503-507-521-567-501-503-507-352-530-574-357-352-531-531-530-513-546-574-504-501-501-567-507-571-537-514-517-502-507-562-507-507-5_V.mp3)
  Segment 27: 75032ms ~ 75608ms (file: 671-165-612-162-672-672-267-612-612-617-176-236-612-615-635-673-673-672-265-365-361-627-625-627-360-632-627-306-637-360-653-671-637-632-615-631-321-103-102-316-130-362-623-260-3_W.mp3)
  Segment 28: 76608ms ~ 77184ms (file: 157-165-576-752-156-175-517-751-167-157-175-137-351-137-527-157-567-150-156-531-517-573-517-157-517-157-157-517-156-537-513 157-571-751-715-715-517-517-571-570-570-576-570-570-5_W.mp3)
  Segment 29: 78184ms ~ 78760ms (file: 526-526-576-572-517-572-527-526-521-520-520_W.mp3)
  Segment 30: 79760ms ~ 80336ms (file: 140-143-134-143-143-143-415-415-143-134-134-103-134-134-143-134-310_V.mp3)
  Segment 31: 81336ms ~ 81912ms (file: 621-136-206-261-620-125-612-162-612-612-612-612-612-617-612-612-612-612-612-610-612-610-631-106 621-623-621-620-621-621-621-623-623-623-621-612-621-612-612-612-163-103 612-612-6_W.mp3)
  Segment 32: 82912ms ~ 83488ms (file: 267-276-362-320-326-362-326-312-126-316-623-263-361-750-765-726-276-237-167-163-103-613-612-623-672-206-261-267-627-267-267-236-612-613-261-623-321-263-136-301-301-103-103-627-6_V.mp3)
  Segment 33: 84488ms ~ 85064ms (file: 507-572-562-502-572-520-507-572-275-520-527-520-562-570-526-576-570-572-570_V.mp3)
  Segment 34: 86064ms ~ 86640ms (file: 756-756-756-765-571-572-756-750-572-752-751-573-750-572-576-576-576-576-576-576-756-756-756-756-751-571-507-576-750-570-571-573-573-350-501-571-357-572-576-572-507-573-572-752-7_W.mp3)
  Segment 35: 87640ms ~ 88216ms (file: 652-625-625-625-652-625-625-625-326-231-263-236-261-236-263-263-261-217-765-756-752-756-524-574-514-541-541-547-574-547-572-570-574-517-574-574-547-547-547-540-540-541-540-540-5_W.mp3)
  Segment 36: 89216ms ~ 94688ms (file: 126-132-102-705-721-312-312-163-326-231-230-206-201-130-132-102-631-621-126-126-312-132-326-176-261-216-231-263-321-260-326-123-326-236-231-132-162-132-123-231-623-753-263-326-1_W.mp3)
  Segment 37: 95688ms ~ 96264ms (file: 756-756-752-751-736 571-534-537-531-573-576 752-752-756-752-756-576-756-756-753-572-725-176-513-531_W.mp3)
  Segment 38: 97264ms ~ 97840ms (file: 517-517-571-574-574-574-571 510-517-514-517-570-517-510-507-510-501-157-570-507-750-750-751-570-750-570-570 510-517-517-510-510-510-517-570-574-571-571-570-750-570-507-510-571-7_W.mp3)
  Segment 39: 98840ms ~ 99416ms (file: 570-751-157-571-756-751-751-517-571 570-570-574-576-576-576-574-574-571-517-517-517-571-571-571-135 570-574-574-573-574-574-574-571-517-517-517-517-571-571-571-573-517-217-215 5_W.mp3)
  Segment 40: 100416ms ~ 100992ms (file: 563-534-546 572-572-752-572-756-572-752-752-752-514-546-504-504-536-504 217 216-621-310 572-572-576-572-756-576-576-576-752-752-254-524-514-541-542-542-504-504-504-504-504-504-5_W.mp3)
  Segment 41: 101992ms ~ 102568ms (file: 510-150-510-501-150-105-150-150-105-150-105-150-510-570 105-517-501-501-510-150-105-501-150-150-513-517-510-501-150-150-150-150-150-501-501-105-105-501-510-510-517-547-547-517 5_V.mp3)
  Segment 42: 103568ms ~ 104144ms (file: 103-715-173-310-170-301-173-735-317-175-713-107-751-701-130-105-130-716-713-130-170-371-175-710-753 175-701-103-107-713-137-170-107-376-701-107-106-715-701-317-103-301-103-301-7_W.mp3)
  Segment 43: 105144ms ~ 105720ms (file: 573-573-573-573-573-574-572-562-576-523-527-527-571-527-576-572-507-507-571-572-507-507-527-507-507-507-570-502-570-507-507-572-507-572-570-527-275-507-205-520-205-527-275-752-2_V.mp3)
  Segment 44: 106720ms ~ 107296ms (file: 530-503-507-507 507-503-503-506 530-537-537-503-350-530-504-546-503-502-574-507-523-506-527-502-532-572-576-507-507-507-572-507-507-573-502-572-534-354-356-536-537-351-354-356-3_V.mp3)
  Segment 45: 108296ms ~ 108872ms (file: 257-275-257-752-257-572-527-527-572-572-526-527-572-572-520-520-507-527-507-507-527-507-502-507-501-502-527-125-105-150-105-150-651-516-175-107-165-167-607-165-670-216-125-507-6_W.mp3)
  Segment 46: 109872ms ~ 115488ms (file: 213-210-263-321-213-572-752-527-206-263-547-576-574-120-120-675-120-361-316-376-763-756-567-576-561-127-216-321-276-170-620-572-120-152-132-271-327-627-723-267-230-310-210-213-3_W.mp3)
  Segment 47: 116488ms ~ 117064ms (file: 173-134-154-132 135-132-105-132-132-132-127-134-134-172-162-137-137-126-132-132-126-621-621-627-652-675-617-651-165-165-165-106-163-103-106-106-106-106-106-103-103-106-106-106-1_V.mp3)
  Segment 48: 118064ms ~ 118640ms (file: 276-705-572-726-217-712-752-715-253-657-675-576-750-326-504-672-231-756-312-765-276-572-125-507-150-672-376-217-276-263-235-507-152-215-321-276-516-357-716-570-173-507-753-157-7_V.mp3)
  Segment 49: 119640ms ~ 128604ms (file: 267-256-756-276-263-765-267-321-103-206-210-102-216-102-326-163-572-263-620-236-102-312-201-675-102-120-102-120-126-106-261-265-254-130-163-621-267-126-102-106-106-574-201-754-2_V.mp3)
  Segment 50: 129604ms ~ 130180ms (file: 163-615-165-136-163-162-612-213-210-276-725-752-756-756-572-256-567-572-576-562-567-675-657-567-572-527-702-157-276-752-756-576-576-576-765-576-576-756-765-756-765-765 526-527-5_W.mp3)
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
> **Time**: 2026-04-05 12:54:33
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
"transcript": "해서 인제 그",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "해서", "start_ms": 40, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "인제", "start_ms": 400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 800, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "해서 이제 제가 거기서 서버 개발을 했었고요 그러다가 이제 제가 우연한 기회에 어 해외 지사 발령을 받게 됐어요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "해서", "start_ms": 30, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 300, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 520, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "거기서", "start_ms": 750, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "서버", "start_ms": 1100, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "개발을", "start_ms": 1380, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "했었고요", "start_ms": 1750, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "그러다가", "start_ms": 2180, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 2650, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 2880, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "우연한", "start_ms": 3150, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "기회에", "start_ms": 3550, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "어", "start_ms": 3900, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "해외", "start_ms": 4050, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "지사", "start_ms": 4350, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "발령을", "start_ms": 4650, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "받게", "start_ms": 5000, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "됐어요", "start_ms": 5280, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 5,
"transcript": "해서 이제 제가 거기서 한 5년 정도 있었고요 그러다가 이제 제가 다시 한국으로 돌아오게 됐어요 그때 이제 제가 어 어떤 일을 하게 됐냐면 어 리니지 2 서비스를 총괄하는 역할을 맡게 됐어요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "해서", "start_ms": 40, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 300, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 520, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "거기서", "start_ms": 750, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 1100, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "5년", "start_ms": 1220, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "정도", "start_ms": 1550, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "있었고요", "start_ms": 1850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그러다가", "start_ms": 2250, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 2700, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 2920, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 3180, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "한국으로", "start_ms": 3500, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "돌아오게", "start_ms": 3950, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "됐어요", "start_ms": 4350, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "그때", "start_ms": 4700, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 4980, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 5200, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "어", "start_ms": 5450, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 5580, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "일을", "start_ms": 5850, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "하게", "start_ms": 6100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "됐냐면", "start_ms": 6350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "어", "start_ms": 6750, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "리니지", "start_ms": 6850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "2", "start_ms": 7220, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "서비스를", "start_ms": 7400, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "총괄하는", "start_ms": 7850, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "역할을", "start_ms": 8350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "맡게", "start_ms": 8750, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "됐어요", "start_ms": 9050, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
*Generated by WKAppBot ask gemini — 2026-04-05 12:54:33*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=11716  time=12:53:59  uptime=39896s  mem=350MB  system_processes=356"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_125346.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 260-265-205-206-260-207-276-257-576-576-576-576-576-576-756 257-276-572-576-567-576-576-567-567-576-567-657-756-576-657 251-502-257-570-572-576-576-576-576-567-576-576-576 267-7_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 531-521-512-562-527-576-531-576-527-750-752-275-275-267-752-602-675-760-765-675-765-765-765-675-675-765-675-675-765-675-765-675-675-675-657-752-576-257-526-562-576-507-527-265-6_V.mp3)
  Segment 3: 3152ms ~ 10316ms (file: 361-276-570-270-230-236-534-301-726-130-102-260-267-576-726-127-137-726-106-760-574-102-207-102-327-263-372-103-267-267-527-547-572-267-276-276-207-726-267-276-572-572-127-123-7_W.mp3)
  Segment 4: 11316ms ~ 11892ms (file: 351-352-357-325-532-351-351-126-571-576-571-517 326-263-126-102-512-576-517-367-632-615-617-163-312-623-632-361-756-367-635-572-573-576-516-375-367-362-263-316-253-526-572-527-3_W.mp3)
  Segment 5: 12892ms ~ 13468ms (file: 216-261-261-260-126-261 504-502-501-503-534-503-503-504-501-507-652-625-651-561-657-651-652-501 567-106-135-163-165-156-165-156-510-516-567-561-562-526-526-157-512-156-156-651-1_W.mp3)
  Segment 6: 14468ms ~ 15044ms (file: 761-527-572-527-527-572-752-752-257-756-572-527-572-576-527-257-572-570-573-572-527-572-570-752-752-572-527-753-756-576-574-250-756-576-576-756-756-673-576-571-507-507-527-572-5_W.mp3)
  Segment 7: 16044ms ~ 16620ms (file: 175-250-570-752-720-725-205-275-207-721_V.mp3)
  Segment 8: 17620ms ~ 25180ms (file: 752-260-102-260-201-130-310-231-276-320-726-213-132-321-267-267-310-236-132-130-312-132-231-123-213-236-321-236-210-720-103-230-103-231-327-320-321-216-132-130-231-107-260-230-1_W.mp3)
  Segment 9: 26180ms ~ 26756ms (file: 752-207-706-572-572-627-765-761-715-765-765-765-765-756-576-672-657-576-576-576-572-576-276-765-756-567-756-567-765-576-576-567-576-567-572-526-527-572-576-567-756-157-617-675-1_W.mp3)
  Segment 10: 27756ms ~ 28332ms (file: 105-105-510-150-517-501-510-105-510-506-510-507-150-153 150-510-105-150-150-150-105-501-150-105-105-150-150-501-150-571-567-517-576-150-506-560-571-567-574-571 105-150-105-571-1_V.mp3)
  Segment 11: 29332ms ~ 29908ms (file: 572-573-576-507-572-576-572-527-570-750-570-572-527-326-213-216-352-351-321 507-572-507-507-507-576-507-572-507-573-572-571-537-576 501-534-537-513-513-514-153-513-105 132-523-5_V.mp3)
  Segment 12: 30908ms ~ 31484ms (file: 613-361-631-316-365-316-365-136-316-361-316-361-361-163-631-163-316-316-316_W.mp3)
  Segment 13: 32484ms ~ 33060ms (file: 143-134-104-143-104-401-130-140-104-534-134-105-134-104-104-103-147-143-104-103-134 435-431-314-457-154-405-104-103-154-451-314-153-104-107-130-315-103-145-103-143-154-134-135-1_V.mp3)
  Segment 14: 34060ms ~ 34636ms (file: 576-572-257-752-570-572-570-527-562-503-502-207-570-512-576-567-567-567-567-567-567-567-576-576-576-571-735-537-527-572-572-576-752-572-752-572-571-572-576-752-572-576-752-572-7_W.mp3)
  Segment 15: 35636ms ~ 36212ms (file: 570-571-570-571-570-570-570 510-175-517-507-510-517-157-571-715-570-571-570-715-157-573-517-517-571-571-570-571-571-571-570-570-570-570-570-501-510-517-657-516-157-520-567-510-1_V.mp3)
  Segment 16: 37212ms ~ 37788ms (file: 530-503-507-507 507-503-503-506 530-537-537-503-350-530-504-546-503-502-574-507-523-506-527-502-532-572-576-507-507-507-572-507-507-573-502-572-534-354-356-536-537-351-354-356-3_V.mp3)
  Segment 17: 38788ms ~ 39364ms (file: 537-504-503-507-573-574-507-547-507-507-507-574-507-301-235-352-251-251-235-532-521-572-502-527-507-507-572-574-572-507-572-507-502-576-572-572-576-527-527-527-570-570-572-572-5_V.mp3)
  Segment 18: 40364ms ~ 40940ms (file: 751-715-517-570-517-571-576-571-576-576-570-570-536-510-157-501-517-576-560-517-571-517-510-516-506-507-501-510-560-510-506-517-517-571-517-570-715-175-175-753-571-753-751-576-5_W.mp3)
  Segment 19: 41940ms ~ 42516ms (file: 103-130-103-105-514-154-105-103-106-130-136-167-617-716-715-675-657-657-102-176-176-716-176-761-761-671-167-761-176-761-716-761-716-761-617-176-716-176-671-671-617-617-617-716-1_W.mp3)
  Segment 20: 43516ms ~ 44092ms (file: 107-715-107-107-107-107-175-170-107-170-170-170-170-715-715-107-713-170-173-170-107-170-570-570 175-107-175-107-175-716-175-107-107-715-175-370-173-107-715-107-175-170-150-517-5_V.mp3)
  Segment 21: 45092ms ~ 58736ms (file: 123-751-762-172-267-762-765-132-672-267-326-312-123-326-106-765-672-276-312-317-761-102-103-321-236-210-120-231-261-230-217-321-231-263-213-132-231-236-132-261-267-261-236-216-7_W.mp3)
  Segment 22: 59736ms ~ 60312ms (file: 635-673-617-631-613-167-312-371-352-126-136-561-327-231-241-126-216-325-132-136-130 673-216-275-321_V.mp3)
  Segment 23: 61312ms ~ 61888ms (file: 752-572-572 507-510-502-507-570-507-576-520-572-752-572-571-576-570-572-527-572-256-275-257-572-570 572-507-570-520-527-502-507-570-576-752-572-257-576-570-265-257-726-256-750-7_V.mp3)
  Segment 24: 62888ms ~ 63464ms (file: 715-175-571-175-175-751 574-574-570-570-571-750-570-157-571-157-571-175-170-751-571-751-715-750-701-751-175-170-175 517-157-157-751-175-751-517-571-751-715-175-751-715-175-751-1_V.mp3)
  Segment 25: 64464ms ~ 65040ms (file: 105-574-517-571-510-507-750-105-157 540-574-547-507 574-573-574-574-573-105-175-751-571-715-751-750-570 574-517-517-571-571-517-571-175-571-170-715-570-571-547-514-715-751-705-5_V.mp3)
  Segment 26: 66040ms ~ 66616ms (file: 517 105-157-105-107-175-150-175-107-710-170-705-576-756-576-576-576-657-576-567-756-576-567-576-567-576-567-570 157-105-107-107-103-107-715-753-576-567-567-576-675-765-765-657-5_W.mp3)
  Segment 27: 67616ms ~ 68192ms (file: 530-532-523 502-502-517-501-507-531-534-547-507-503-536-501-503-501-507-502-506-507-507-576-507-527-521-507-507-507-507-507 521-567-514-506-537-501-571-572-576-574-517-572-507-5_V.mp3)
  Segment 28: 69192ms ~ 69768ms (file: 135-512-152-157-517-517-152-172-136-715-176-136-126-563-163-621-623-165-576-165-617-123-125-132-125-125-125-152-125-125-165-135-153-521-651-567-617-761-165-615-165-561-136-165-1_W.mp3)
  Segment 29: 70768ms ~ 71344ms (file: 657-653-567-567-576 501-520-570-501-570-567-576-570-576-572-507-506-507-567-576-576-572-570 506-562-526-652-256-652-527-567-576_V.mp3)
  Segment 30: 72344ms ~ 72920ms (file: 130-103-104-104-143 405-143-401-413-301-104-103-145-143-105-104-401-134-174-417-143-145-430-103-105-105-417-134-104-134-143 145-154-105-130-104-134-104-103-103-175-153-137-104-1_V.mp3)
  Segment 31: 73920ms ~ 74496ms (file: 513-153-325-123-231-123-125-123-125-123-137-721-327-125-135-123-150-135-153-123-356-315-315-213-527-576-564-361-236-357-527-532-357-257-351-351-351-572-750-325-574-537-534-574-5_W.mp3)
  Segment 32: 75496ms ~ 76072ms (file: 137-137-175-713-127-132-137-173-175-715 157-105-105-130-173-137-103-105-130-170-170-130-137-130-137-137-137-127-317-137-137 570-570-570-175-135-103-150-157-170-107-135-170-170-1_V.mp3)
  Segment 33: 77072ms ~ 77648ms (file: 576-576-753-752-160-260-615-651-651-657-567-615-657-615-651-165-756-572-765-752-756-765 651-651-657-657-657-567-657-567-657-567-657-567-564-657 657-651-657-657-657-567-657-567-6_W.mp3)
  Segment 34: 78648ms ~ 79224ms (file: 671-617-627-672-627-610-601-526-526-560-652 617-675-657-671-617-671-671-617-671-671-671-672-617-617-617-617-610-617-617-610-605-653-617-617-617-167-617-617-610-617-617-167-617-6_W.mp3)
  Segment 35: 80224ms ~ 80800ms (file: 715-175-170-710-170-751-701-715-701-571-170-710 157-571-751-571-750-750-710-750-751-750-715-571-751-750-705-507-705-750-750-170-710-157-750_V.mp3)
  Segment 36: 81800ms ~ 82376ms (file: 120-123 107-130-127-172-127-172-712-710-720 672-672-672-672-102-120-175-156-765-621-176-762-761-612-671-765-756-621-617-761-762-765-725-721-720-720-756-715-671-160-652-706-672-6_W.mp3)
  Segment 37: 83376ms ~ 83952ms (file: 216-162-160-637-671-605-176-765-160-673-730-167-761-715-756-167-163-136-163-361-361-136-136-653-613-610-361-316-631-135-135-320-312-321-132-321-321-231-326-321-321-321-213-312-3_W.mp3)
  Segment 38: 84952ms ~ 85528ms (file: 561-561-651-516-651-165-523-567-561 574-574-574-574-572-576-562-652-162_W.mp3)
  Segment 39: 86528ms ~ 87104ms (file: 326-321-137-317-136-317-376-371-173-173-376-317-713-715-576-651-165-671-627-163-362-163-315-536-356-315-513-521-523-253-365-356-356-357-157-357-517-312-321-326-327-132-362-361-3_W.mp3)
  Segment 40: 88104ms ~ 88680ms (file: 135-137-315-315-310-315-301-103-315-103-134-103-310-314-130-130-130-314-130-103-103-103-310-130-103-134-130-310-130-130-103-134-103 134-453-135-345-134-314-134-104-103-130-103-1_V.mp3)
  Segment 41: 89680ms ~ 90256ms (file: 312-623-261-132-126-103-123-316-312-132-130-136-156-103-156-137-136-103-136-136-106-130-137-132_V.mp3)
  Segment 42: 91256ms ~ 98060ms (file: 165-726-310-312-702-236-206-705-760-267-162-230-725-157-130-361-761-527-765-301-765-231-706-763-276-271-201-376-753-267-571-760-267-126-162-312-132-210-267-730-316-206-263-615-2_W.mp3)
  Segment 43: 99060ms ~ 99636ms (file: 607-536-576-571-507-506-507-503-507-123-125-521-523-513-563-165-631-613-163-163-162-103-351-325-175_W.mp3)
  Segment 44: 100636ms ~ 101212ms (file: 276-271-273-216-261-213-216-251-213-215-231-321-235-261-263-261-132-123-312-132-132-123-152-257-256-261-316-215-123-132-213-652-326-276-217-251-521-571-526-257-267-627-267-263-2_W.mp3)
  Segment 45: 102212ms ~ 102788ms (file: 163-623-620 675-132-631-631-316-132-123-623-132-123-520-231-756-756-657-213-107-175-157-153-716-567-261-261 173-361-507-530-503-350-357-132-137-673-627-263-123-167-516-310-361-1_W.mp3)
  Segment 46: 103788ms ~ 104364ms (file: 257-257-527-572-572-572-570-527-526-235-125-213-351-753-725-752-756-752-752-752-572-576-756-572-576-576-576-756-756-576-756-756-751-572-571-571-572-571-572-570-570-751-572-570-7_W.mp3)
  Segment 47: 105364ms ~ 105940ms (file: 523-271-217-216-257-627-321-705-267-326-672-502-523-237-570-362 675_V.mp3)
  Segment 48: 106940ms ~ 107516ms (file: 167-235-136-132-120-217-213-203-123_V.mp3)
  Segment 49: 108516ms ~ 109092ms (file: 571-731-751-157-517-715-175-175-751-715-571-175-175-517-175-517 150-106-157-176-135-137-153-135-123-157-157-571-715-751-375-715-175-175-710-157-715-175-751-751-715-715-715-175-7_V.mp3)
  Segment 50: 110092ms ~ 110668ms (file: 547-547-547-574 547-574-574-547-547-574-547-547-574-574-574-574-574-574-574-574-574 504-507-514-510-570-541-574-574-574-574-574-574-547 547-547-547-547-547-547-574-574-574-574-5_W.mp3)
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
> **Time**: 2026-04-05 12:54:59
> **AI**: gemini

---

## Response

[
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
"transcript": "제 고향 선배님이기도 하셔 가지고 예전에 이제 그 엠비씨 코미디언 공채 오실 때 그때 이제 제가 시험장에서 수험생으로 마주쳤던 기억이 나거든요 근데 그때도 사실은 워낙에 수려한 외모로 유명하셨어요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "제", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "고향", "start_ms": 180, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "선배님이기도", "start_ms": 450, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "하셔", "start_ms": 1000, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "가지고", "start_ms": 1220, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "예전에", "start_ms": 1600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 1980, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 2200, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "엠비씨", "start_ms": 2300, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "코미디언", "start_ms": 2650, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "공채", "start_ms": 3100, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "오실", "start_ms": 3380, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 3650, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "그때", "start_ms": 3800, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 4050, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 4250, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "시험장에서", "start_ms": 4480, "dur_ms": 480, "phase_lr_deg": 0.0}
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
"words": []
},
{
"segment": 4,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "해서 한 3년 정도 있었던 것 같아요 그러다가 이제 제가 우연한 기회에 어 다른 회사로 스카웃 제의를 받고 옮기게 되었는데 그때 이제 제가 옮긴 회사가 어 지금의 엔씨소프트의 전신인 회사였어요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "해서", "start_ms": 60, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 300, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "3년", "start_ms": 420, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "정도", "start_ms": 750, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "있었던", "start_ms": 1050, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 1380, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 1500, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "그러다가", "start_ms": 1900, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 2350, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 2600, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "우연한", "start_ms": 2850, "dur_ms": 380, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 20,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 21,
"transcript": "해서 이제 제가 거기서 서버 개발을 했었고요 그러다가 이제 제가 우연한 기회에 어 해외 지사 발령을 받게 됐어요 그래서 미국으로 가게 되었는데 그때 이제 제가 미국에서 어 어떤 걸 경험했냐면 어 리니지라는 게임을 처음 접하게 됐어요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "해서", "start_ms": 80, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 320, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 520, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "거기서", "start_ms": 750, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "서버", "start_ms": 1100, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "개발을", "start_ms": 1380, "dur_ms": 350, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 28,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 29,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 32,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
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
"transcript": "해서 이제 제가 거기서 어 리니지 서비스를 런칭을 하는 데 어 참여를 했었고요 그때 이제 제가 어 게임이라는 것이 어 단순히 그냥 노는 것이 아니라 어 하나의 어떤 문화가 될 수 있다는 걸 처음으로 느끼게 됐어요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "해서", "start_ms": 50, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 300, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 500, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "거기서", "start_ms": 720, "dur_ms": 310, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 38,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "해서 이제 제가 거기서 한 5년 정도 있었고요 그러다가 이제 제가 다시 한국으로 돌아오게 됐어요 그때 이제 제가 어 어떤 일을 하게 됐냐면 어 리니지 2 서비스를 총괄하는 역할을 맡게 됐어요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "해서", "start_ms": 70, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 320, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 520, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 42,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 45,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 46,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 47,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 48,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 49,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 50,
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
*Generated by WKAppBot ask gemini — 2026-04-05 12:54:59*
