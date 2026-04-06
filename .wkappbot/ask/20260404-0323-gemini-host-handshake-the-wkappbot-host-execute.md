# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=17688  time=03:20:49  uptime=7332s  mem=129MB  system_processes=364"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260404_032040.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 267-621 361 123-102-120-267-267-276-625-657-267-267-567-657-267-263-263-263-261-261-263-231-612-623-613-162-621-163-216-126-123-136-126-162-627-612-621-612-213-627-621-216-621-6_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 162-167-176-137-576-715-317-162-326-762-765-765-765-765-765-765_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 637-165-501-103-316-126-321-162-607-267-273_W.mp3)
  Segment 4: 4728ms ~ 12684ms (file: 216-261-216-627-627-132-357-273-735-312-504-102-723-263-620-532-236-276-541-756-765-765-217-267-263-561-572-705-756-756-325-320-570-102-123-103-571-257-562-270-276-230-203-123-3_W.mp3)
  Segment 5: 13684ms ~ 14260ms (file: 321-123-302-132-312-632-326-301-312-312-163-216-126-310-103-163-316-136-132-103-267-627-751-175-715-173-165-132-132-136-103-106-107-136-132-361-320-361-320_V.mp3)
  Segment 6: 15260ms ~ 15836ms (file: 267-726-276-627-621-672-567-567-267-267-276-276-267-267-126-216-123-215-216-231-213-123-126-561-256-213-213-321-321-213-267-627-267-627-267-562-675-671-617-613-216-126-126-265-2_W.mp3)
  Segment 7: 16836ms ~ 17412ms (file: 263-326-263-623-627-327-267-623-231-312-326-261-263-321-632-675-756-167-716-672-361-136-756-567-561-675-673-761-613-106-657-671-762-713-671-627-675-673-762-673-761-760-671-671-6_W.mp3)
  Segment 8: 18412ms ~ 18988ms (file: 106-136-163-160-125-126-623-260-312-132-316-136-173-106-106-106-132-163-126-162 123-103-657-163-123-162-613-162-126-321-136-316-132-231-136-263-216-160-263-236-162-627-610-126-6_V.mp3)
  Segment 9: 19988ms ~ 20564ms (file: 301-306-132-103-136-103-136-613-106-765-765-760-756-756-172-705-613-120-132-102-706-765-765-751-312-106-130-103-236-230-310-103-103-167-173-136-260-260-260-230-273-120-103-750-1_V.mp3)
  Segment 10: 21564ms ~ 22140ms (file: 627-654-563-635-236-261-576-635-106-163-106-106-162-107-103-156-176-312-132-102-623-621-301-261-267-627-650-563-576-567-576-567-105-567-567-765-657-574-567-567-627-165-310-301-3_W.mp3)
  Segment 11: 23140ms ~ 23716ms (file: 135-102-317-160-136-106-706-136-136-132-563-613-102-675-316-106-136-103-136-136-362-751-136-163-103-130-106-103-312-703-136-103-130-517-762-175-165-136-103-106-316-137-316-605-1_V.mp3)
  Segment 12: 24716ms ~ 25292ms (file: 236-236-263-326-326-236-326-263-256-235-253-265-362-326-623-325-325-623-625-326-263-623-362-365-326-316-321-326-265-263-265-236-263-263-267-263-263-362-263-267-261-267-261-263-2_W.mp3)
  Segment 13: 26292ms ~ 26868ms (file: 236-162-261-136-163-621-623-103-132-716-617-617-106-103-132-162-562-102-765-321-132-312-167-576-312-372-173-126-675-316-362-625-673-162-236-136-672-627-267-623-275-627-267-263-6_W.mp3)
  Segment 14: 27868ms ~ 28444ms (file: 136-103-137-132-103-765-173-167-103 167_V.mp3)
  Segment 15: 29444ms ~ 30020ms (file: 165-657-625-621-631-670 765-671-167-716 175 765-675-120-361-312-126_W.mp3)
  Segment 16: 31020ms ~ 31596ms (file: 623-103-136-103-163-756-601-157-136-612-675-130-132-615-136-160-132-103-160-103-610-165-106-126-156-125-623-106-163-615-136-137-106-172-736-316-136-352-103-176-103_V.mp3)
  Segment 17: 32596ms ~ 33172ms (file: 362 103-136-163-136-167-103-136-107-163-106-761-126-103-136-132-137-156-163-165-165-203-103-106-167-136-610-173-105-107-136-107-132-103-130-156-125-106-136-103-631-136-671-613-1_V.mp3)
  Segment 18: 34172ms ~ 34748ms (file: 612-126-627-126-627-623-327-231-326-236-321-627-673-653-601-167-165 136-623-632-754-365-632-637-163-327-312-137-671-176-670-601-651-107-670-163-132-237-613-612-136-163-376-126-1_W.mp3)
  Segment 19: 35748ms ~ 36324ms (file: 760-326-162 123 675-103-136-103-130-130-316-631-103-567-516-132-103-103-103-132-167-106-163-137-152-167-136-167-167-136-160-136-160-156-127-103-106-671-671-172 261-126-601-715-1_V.mp3)
  Segment 20: 37324ms ~ 37900ms (file: 765-675-765-765-673-765-765-672-126-573-175-627-627-672-675-652-132-103-136-136-163-316-103-136-136-103-136-106-316-126-167-301-130-103-136-301-103-137-316-134-130-517-653-163-7_V.mp3)
  Segment 21: 38900ms ~ 39476ms (file: 637-627 571-574-756-103-312-132-617-675-651-615-601-675-657-652 576-576-567 765-765-756-675-765-576 162-621-162-162-127-162-163-176-167-167-167-613-716-176-276-126-132-127-312-1_W.mp3)
  Segment 22: 40476ms ~ 41052ms (file: 132-235-325-321-351-326-316-231-326-321-361-237-362-376-376-625-675-267-621-326-213-263-125-123-123-103-321-326-321-312-231-237-237-237 231 321-136-610-162-612-106-103-156-321-1_W.mp3)
  Segment 23: 42052ms ~ 42628ms (file: 514-627-123-261-256-657-261-106-132-136-132-146-162-162-172-126-751-756 103-103-653-165-163-106-162-176-136-106-167-106-132-137-164-103-165-316-103-106-106-561-615-163-170-136-1_V.mp3)
  Segment 24: 43628ms ~ 44204ms (file: 672-627-627-762-123-132 135-126-231-123-312-263-127_W.mp3)
  Segment 25: 45204ms ~ 45780ms (file: 371-241-126-136-132-671-271-627 145-123-261-132-752-312-132-176-531-317-312-537-312-136-176-314-106-621-631-763-675-162-135-136-163-213 612-216-235-162-163-136-176-170-172-361-3_V.mp3)
  Segment 26: 46780ms ~ 47356ms (file: 143-671-102-637-657-167-306-163-132-103-163-103-162-102-136-612-163-132-217-162-136-617-312-103-726-172-126 157-716-103-613-136-213-103-156-125-617-132-625-310-163-106-103-167-1_V.mp3)
  Segment 27: 48356ms ~ 48932ms (file: 203-207-237-132-123-236-273-327-753-756-756-736-263-263-260-321-261-216-310-267-627-126-213-312-372-236-736-673-302-172-176-617-315-352-267-235-362-103-137-107-137-312-763-276-2_W.mp3)
  Segment 28: 49932ms ~ 50508ms (file: 173-126-261-316-167-167-574-371-326-316-317-137-361-617-612-623-362-367-671-561-576-567-135-137-137-172-123-261-263-236-126-162-126-216-231-261-621-265-621-625-651-627-625-625-3_W.mp3)
  Segment 29: 51508ms ~ 52084ms (file: 136-103-517-361-165-135-136-137-132-617-316-310-163-316-615-136-136-126-103-130-103-321-361-136-176-617-136-103-136-136-312-103-103-163 176-137-617-176_V.mp3)
  Segment 30: 53084ms ~ 59096ms (file: 265-256-106-163-260-752-572-203-230-521-576-260-123-627-263-267-260-542-265-627-673-754-263-623-163-162-620-261-257-267-576-527-167-361-126-261-672-675-637-236-620-156-162-531-1_W.mp3)
  Segment 31: 60096ms ~ 60672ms (file: 102-103-130-132-102-213-362-126-120-375-531-120-162-613-163-361-617-361-310-361-312_W.mp3)
  Segment 32: 61672ms ~ 62248ms (file: 350-320-362-326-326-236-326-206-203-230-263-627-361-123-310-213-231-132-102-103-103-301 621-326-132-236-321-321-312-321-312-317-317-316-132-150-510-517-675-657-627-623-163-627-2_W.mp3)
  Segment 33: 63248ms ~ 63824ms (file: 502-756-321-206-203-502-123-102-106-765-706-726-267-361-203-216-132-163-312-310-306-362-306-362-763-706-760-765-765-107-103-173-206-207-236-362-703-306-306-301-302-321-103-301-5_V.mp3)
  Segment 34: 64824ms ~ 71412ms (file: 321-572-120-103-312-231-231-123-261-231-236-263-213-261-213-265-120-257-102-213-236-326-260-653-102-236-126-206-217-102-253-102-201-756-235-321-103-167-165-162-162-236-263-267-2_W.mp3)
  Segment 35: 72412ms ~ 72988ms (file: 271-217-237-123-576-617-127-567-256 130-132-123-316-613-213-623-627-162-623-231-132-267 316-132-136-130-136-163-103-132-103-103-132-126-120-132-631-316-627-637-236-267-263-267-6_W.mp3)
  Segment 36: 73988ms ~ 82448ms (file: 103-627-103-103-176-612-157-127-162-163-260-126-260-672-627-167-176-317-320-123-620-621-326-206-260-623-236-670-263-261-627-267-261-612-613-576-756-216-567-126-267-210-231-267-6_W.mp3)
  Segment 37: 83448ms ~ 90972ms (file: 203-213-231-102-103-201-123-102-123-510-510-152-231-103-517-120-132-132-210-127-257-130-237-230-321-120-123-213-321-102-102-120-102-756-527-257-231-102-102-102-513-125-201-320-1_V.mp3)
  Segment 38: 91972ms ~ 92548ms (file: 310-316-301-302-613-136-126-103-132-103-132 710-157-671-136-103-106-532-362-367 621-260 132-103-631-216-103-136-301-130-310-703-736-670-201-205-201-210-265-672-632-571-576-351-3_V.mp3)
  Segment 39: 93548ms ~ 94124ms (file: 136-126-132-106-167-102-123-160-315-153-362-631-136-173-103-132-652-136-126-130-167-103-132-106-136-135-751-574-751-152-165-132-165-150-103-132-103-103-571-132-561-103-130-105-1_V.mp3)
  Segment 40: 95124ms ~ 95700ms (file: 163-136-106-126-106-136-317-163-361-265-235-163 162-130-132-312_V.mp3)
  Segment 41: 96700ms ~ 97276ms (file: 630-627-672-316-135-546-132-163-176-123-103-317-130-103-136-163-173-103-163-106-106-163-106-305-165-156-136-103-132-136-613-106-615-162-267-316-137-132-176-517-160-126-102-103-1_V.mp3)
  Segment 42: 98276ms ~ 98852ms (file: 167-623 627-632-625-612-160-126-176-312-326-362-261-267-673-627-162-621-631-612-632-613-167-627-621-621-612-621-612-651-361-160-167-167-617-167-613-162-637-613-621-261-612-621-6_W.mp3)
  Segment 43: 99852ms ~ 100428ms (file: 756-267-267-527-736-725-507-321-326-503-231-163-672-605-267-132-527-326-675-312-752-573-745-574-125-571-573-531-237-673-753-326-576-132-671-136-137-317-310-137-317-361-136-316-3_W.mp3)
  Segment 44: 101428ms ~ 107980ms (file: 312-312-213-132-132-123-123-153-321-132-135-123-103-132-123-130-120-132-321-213-132-132-321-123-120-102-132-123-132-132-312-132-623-123-516-675-672-657-216-321-132-231-103-130-3_V.mp3)
  Segment 45: 108980ms ~ 109556ms (file: 167-167-106-106-210-257-572-576-652-265-267-765-726-670-672 302-302-306-302-360-326-301-320-613-106-106-230-260-672-670-627-260-130-270-623-103-106-106-671-137 267-273-276-671-6_W.mp3)
  Segment 46: 110556ms ~ 111132ms (file: 132-134-163-103-106-163-310-601-602-603-130-310-102-312-301-301-672-713-621-132-173-715-103-132-125-164-103-103-716-263-126-562-106-317-167-136-652-176-135-165-367-105-157-357-1_V.mp3)
  Segment 47: 112132ms ~ 112708ms (file: 160-163-163-261-203-321-621-261-670-301-132-301-132-316-107-137-276-362-132-107-130-301-163-160-621-627-260-670-670-603-602-213-267-620-260-276-260-627-263-260-620-260-576-143-1_W.mp3)
  Segment 48: 113708ms ~ 114284ms (file: 762-705-573-174-312-713-132-576-305-306-135-374-321-167-617-641 136-176-103-136-136-136 136-132-136-631-671-106-607-670-162-173-132-163-216-162-137-567-657-273-372-750 132-137-1_V.mp3)
  Segment 49: 115284ms ~ 123924ms (file: 752-132-130-103-260-627-261-103-260-216-206-213-103-210-160-574-103-261-257-103-321-320-267-675-765-367-103-134-231-312-301-236-273-573-107-316-263-627-130-172-236-231-326-267-6_W.mp3)
  Segment 50: 124924ms ~ 125500ms (file: 106-105-562-327-320-237-321-327-321-310-312-627-621-132-176-371-371-123-137-135-231-317-573-102-126-326-103-265-362-176-137-573-231-317-230-203-263-237-230-321-312-312-312-136-1_W.mp3)
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
> **Time**: 2026-04-04 03:23:12
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "받아들였습니다 강태양 소년단의 이번 앨범 아리랑은 처음부터 끝까지 한국적인 정서를 품고 있습니다 하나의 팝 앨범이자 동시에 한국 문화에 대한 거대한 레퍼런스처럼 느껴질 정도인데요 전통 민요 아리랑의 선",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "받아들였습니다", "start_ms": 20, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "강태양", "start_ms": 720, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "소년단의", "start_ms": 1100, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "이번", "start_ms": 1550, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "앨범", "start_ms": 1780, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "아리랑은", "start_ms": 2050, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "처음부터", "start_ms": 2450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "끝까지", "start_ms": 2820, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "한국적인", "start_ms": 3170, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "정서를", "start_ms": 3650, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "품고", "start_ms": 3980, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 4220, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "하나의", "start_ms": 4600, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "팝", "start_ms": 4900, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "앨범이자", "start_ms": 5030, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "동시에", "start_ms": 5410, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 5750, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "문화에", "start_ms": 5980, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "대한", "start_ms": 6320, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "거대한", "start_ms": 6570, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "레퍼런스처럼", "start_ms": 6910, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "느껴질", "start_ms": 7520, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "정도인데요", "start_ms": 7860, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "전통", "start_ms": 8340, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "민요", "start_ms": 8600, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "아리랑의", "start_ms": 8840, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "선", "start_ms": 9250, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "법입니다 전업 투자자들이 하는 빠른 호흡의 매매가 아니더라도 주식을 할 수 있",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "법입니다", "start_ms": 20, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "전업", "start_ms": 400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "투자자들이", "start_ms": 680, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 1180, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "빠른", "start_ms": 1420, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "호흡의", "start_ms": 1690, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "매매가", "start_ms": 2030, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "아니더라도", "start_ms": 2380, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "주식을", "start_ms": 2860, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 3210, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 3350, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "있", "start_ms": 3470, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "는 종가 매매와 시가 매매 그리고 확률 높은 스케줄 트레이딩 등을 알차게 준비했습니다 자세한 내용은 고정",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "는", "start_ms": 30, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "종가", "start_ms": 170, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "매매와", "start_ms": 440, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "시가", "start_ms": 820, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "매매", "start_ms": 1090, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 1370, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "확률", "start_ms": 1720, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "높은", "start_ms": 1980, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "스케줄", "start_ms": 2250, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "트레이딩", "start_ms": 2670, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "등을", "start_ms": 3120, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "알차게", "start_ms": 3380, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "준비했습니다", "start_ms": 3750, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "자세한", "start_ms": 4450, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "내용은", "start_ms": 4830, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "고정", "start_ms": 5170, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "빵 먹으러 갈까 그 와중에도 아침 식사를 챙겨주겠다고 빵을 반으로 자르더니 동생에게 줄 접시를 잡고 인사를 와서 제대로",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "빵", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "먹으러", "start_ms": 190, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "갈까", "start_ms": 490, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 750, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "와중에도", "start_ms": 860, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "아침", "start_ms": 1230, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "식사를", "start_ms": 1460, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "챙겨주겠다고", "start_ms": 1790, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "빵을", "start_ms": 2460, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "반으로", "start_ms": 2720, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "자르더니", "start_ms": 3060, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "동생에게", "start_ms": 3460, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "줄", "start_ms": 3900, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "접시를", "start_ms": 4030, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "잡고", "start_ms": 4400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "인사를", "start_ms": 4670, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "와서", "start_ms": 5010, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "제대로", "start_ms": 5250, "dur_ms": 380, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "부탁드린다고 했는데 들어갔어요 이번에 1월달에 들어가지 아 아 예 예 감사합니다 언니 그 계산서 꼭 끊어주",
"speaker": "speaker_4",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "부탁드린다고", "start_ms": 30, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "했는데", "start_ms": 610, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "들어갔어요", "start_ms": 940, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이번에", "start_ms": 1420, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "1월달에", "start_ms": 1750, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "들어가지", "start_ms": 2190, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 2600, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 2710, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "예", "start_ms": 2820, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "예", "start_ms": 2950, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 3080, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "언니", "start_ms": 3620, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 3850, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "계산서", "start_ms": 3960, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "꼭", "start_ms": 4320, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "끊어주", "start_ms": 4460, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "린지 하지만 목이 메는 퍽퍽한 맛빵에 스카이는 짜증",
"speaker": "speaker_5",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "린지", "start_ms": 20, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "하지만", "start_ms": 250, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "목이", "start_ms": 590, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "메는", "start_ms": 830, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "퍽퍽한", "start_ms": 1090, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "맛빵에", "start_ms": 1490, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "스카이는", "start_ms": 1860, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "짜증", "start_ms": 2290, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "난 한국의 군수 산업 역량이 실제로는 대만이 감히 엄두조차 낼 수 없는 전혀 다른 차원으로 올라섰다며 대만 매체가 이를 대대적으로 극찬하고 나서면서 연일 한국을 상대로 날선 발등을 유발해 와 왔",
"speaker": "speaker_6",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "난", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "한국의", "start_ms": 170, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "군수", "start_ms": 500, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "산업", "start_ms": 760, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "역량이", "start_ms": 1030, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "실제로는", "start_ms": 1390, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "대만이", "start_ms": 1860, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "감히", "start_ms": 2200, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "엄두조차", "start_ms": 2440, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "낼", "start_ms": 2840, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 2970, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "없는", "start_ms": 3080, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "전혀", "start_ms": 3340, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "다른", "start_ms": 3590, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "차원으로", "start_ms": 3830, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "올라섰다며", "start_ms": 4200, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "대만", "start_ms": 4770, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "매체가", "start_ms": 5030, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "이를", "start_ms": 5360, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "대대적으로", "start_ms": 5590, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "극찬하고", "start_ms": 6190, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "나서면서", "start_ms": 6630, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "연일", "start_ms": 7130, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "한국을", "start_ms": 7390, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "상대로", "start_ms": 7760, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "날선", "start_ms": 8100, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "발등을", "start_ms": 8360, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "유발해", "start_ms": 8720, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "와", "start_ms": 9090, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "왔", "start_ms": 9220, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "레이더를 장착했고 적외선 탐색 및 추적 시스템과 통합 전자전 시스템까지 갖추고 있습니다 또한 동력 측면에서는 한국의 한화에어로스페이스가 미국 제너럴 일렉트릭과 이미 계약을 맺고 미국 본토에서 F414 엔진 40기를 생",
"speaker": "speaker_6",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "레이더를", "start_ms": 50, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "장착했고", "start_ms": 450, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "적외선", "start_ms": 890, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "탐색", "start_ms": 1260, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "및", "start_ms": 1560, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "추적", "start_ms": 1690, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "시스템과", "start_ms": 1950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "통합", "start_ms": 2420, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "전자전", "start_ms": 2670, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "시스템까지", "start_ms": 3040, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "갖추고", "start_ms": 3580, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 3940, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "또한", "start_ms": 4410, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "동력", "start_ms": 4670, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "측면에서는", "start_ms": 4940, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "한국의", "start_ms": 5440, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "한화에어로스페이스가", "start_ms": 5770, "dur_ms": 950, "phase_lr_deg": 0.0},
{"word": "미국", "start_ms": 6740, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "제너럴", "start_ms": 6970, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "일렉트릭과", "start_ms": 7310, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이미", "start_ms": 7780, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "계약을", "start_ms": 8010, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "맺고", "start_ms": 8370, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "미국", "start_ms": 8640, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "본토에서", "start_ms": 8880, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "F414", "start_ms": 9320, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "엔진", "start_ms": 9720, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "40기를", "start_ms": 9980, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "생", "start_ms": 10350, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "얘기 꺼내기가 어려웠을 것 같아요 근데 선배님이 아니 아니 너 하고 싶은 거 한번 다 해 라고 이제 선배님이 리액션 해주시니까 제",
"speaker": "speaker_7",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "얘기", "start_ms": 30, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "꺼내기가", "start_ms": 260, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "어려웠을", "start_ms": 630, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 1030, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 1160, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 1490, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "선배님이", "start_ms": 1730, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "아니", "start_ms": 2100, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "아니", "start_ms": 2300, "dur_ms": 170, "phase_lr_deg": 0.0},
{"word": "너", "start_ms": 2490, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "하고", "start_ms": 2620, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "싶은", "start_ms": 2850, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 3110, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "한번", "start_ms": 3240, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 3490, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "해", "start_ms": 3630, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "라고", "start_ms": 3800, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 4040, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "선배님이", "start_ms": 4240, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "리액션", "start_ms": 4610, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "해주시니까", "start_ms": 5010, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "제", "start_ms": 5550, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "방향을 못 잡고 있고요 그러면 시스템이 바뀌는 속도랑 내가 바뀌는 속도 중에서 어느 쪽이 더 빠를까요 스티븐 호킹은 이런 말을 하게 되었습니다 지성의",
"speaker": "speaker_8",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "방향을", "start_ms": 60, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "못", "start_ms": 390, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "잡고", "start_ms": 520, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "있고요", "start_ms": 780, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "그러면", "start_ms": 1120, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "시스템이", "start_ms": 1420, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "바뀌는", "start_ms": 1850, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "속도랑", "start_ms": 2190, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "내가", "start_ms": 2590, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "바뀌는", "start_ms": 2820, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "속도", "start_ms": 3160, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "중에서", "start_ms": 3420, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "어느", "start_ms": 3790, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "쪽이", "start_ms": 4020, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 4260, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "빠를까요", "start_ms": 4390, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "스티븐", "start_ms": 4860, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "호킹은", "start_ms": 5220, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 5590, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "말을", "start_ms": 5830, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "하게", "start_ms": 6090, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "되었습니다", "start_ms": 6320, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "지성의", "start_ms": 6890, "dur_ms": 310, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "듯한 남쪽 텍사스 시골 개들에게는 미네소타의 매운맛이 너무 강력했나 봅니다 눈물이 안 나고 모",
"speaker": "speaker_9",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "듯한", "start_ms": 20, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "남쪽", "start_ms": 280, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "텍사스", "start_ms": 580, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "시골", "start_ms": 950, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "개들에게는", "start_ms": 1280, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "미네소타의", "start_ms": 1820, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "매운맛이", "start_ms": 2320, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 2760, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "강력했나", "start_ms": 2990, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "봅니다", "start_ms": 3390, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "눈물이", "start_ms": 3730, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 4090, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "나고", "start_ms": 4230, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "모", "start_ms": 4490, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "가족들하고 오면 더 좋겠다 어디를 좀 부탁하지 민정이는 아버지하고 오기로 했고요 네 알겠습니다 예 감사합니다 가세요 어디 가시",
"speaker": "speaker_4",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "가족들하고", "start_ms": 50, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "오면", "start_ms": 550, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 810, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "좋겠다", "start_ms": 940, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "어디를", "start_ms": 1270, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 1610, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "부탁하지", "start_ms": 1750, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "민정이는", "start_ms": 2180, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "아버지하고", "start_ms": 2620, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "오기로", "start_ms": 3090, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "했고요", "start_ms": 3450, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "네", "start_ms": 3790, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "알겠습니다", "start_ms": 3920, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "예", "start_ms": 4490, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 4620, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 5160, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "어디", "start_ms": 5490, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "가시", "start_ms": 5750, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "면서 싼 데가 있고 어떤 데는 공산품이 같은 제품인데도 싼 데가 있고",
"speaker": "speaker_10",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "면서", "start_ms": 30, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "싼", "start_ms": 260, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "데가", "start_ms": 400, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "있고", "start_ms": 660, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 900, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "데는", "start_ms": 1150, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "공산품이", "start_ms": 1410, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "같은", "start_ms": 1810, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "제품인데도", "start_ms": 2070, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "싼", "start_ms": 2570, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "데가", "start_ms": 2710, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "있고", "start_ms": 2970, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "그래서 이제 고단한 몸을 이끌고 여러 곳을 돌아다니며 장을 보",
"speaker": "speaker_10",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "그래서", "start_ms": 20, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 320, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "고단한", "start_ms": 550, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "몸을", "start_ms": 910, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "이끌고", "start_ms": 1180, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "여러", "start_ms": 1510, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "곳을", "start_ms": 1770, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "돌아다니며", "start_ms": 2010, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "장을", "start_ms": 2510, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "보", "start_ms": 2770, "dur_ms": 110, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-04 03:23:12*
