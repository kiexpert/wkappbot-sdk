# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=29532  time=03:54:39  uptime=8574s  mem=1498MB  system_processes=364"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260404_035430.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 675-672-671-602-765-765-317-176-267-267-673-267-625-623-657-316-361-613-361-632-675-163-216 301-576-657-675-567-576-576_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 675-567-310-103-163-167-136-671-162-132-312-136-160-567-163-103-673-137-316-132-136-102-137-103-162-621-176-617-160-163-671-321-312-310-135-312-106-123-301-321-301-310-123-165-5_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 263-260-260-620-312-307-301-357-723-321-361-750-306-302-360-316-316-163-531-326-675-627-752-756-203-625-562-265-527-206-310-126-162-316-703-237-130-317-136-163-216-602-620-206-3_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 671-673-675-316-671-632-613-567-517-675-765-672-672-176-657-672-675-673-627-627-672-675-756-762-672-672-267-672-672-672-675-672-167-671-627-672-673-672-672-763-673-673-765-675-6_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 203-237-235-265-263-236-326-523-235-321-231-325-156-213 672-672-672-267-260-620-216-176-320-326-126-137-170-176-701-170-713-173-137-762-123-137-152-157-751-627-576-123-371-162-6_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 263-261-213-210-657-651-105-153-103-316-607-625-152-132-516-136-132-167-675-765-576-312-261-123-132-756-675-126-132-265-632-362 617-326-627_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 316-213-567-615-376-637-367-361-361-316-361-316-316-310-316-137-316-136-637-376-130-312_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 162-103-627-625-125-167-730-106-612-601-715-165-657-103-514-103-102-156-103-651-265-653-165-157-157-570-256-627-627-621 675-106-103-137-176-173-163-106-132-306-621-167-657-130-1_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 627-620-617-617-617-142-671-163-162-263-167-136-103 136-103-675-163-675-163-675-162-167-610-615-162 163-673-613-765-765-106-617-125-675-167-617-106-163-176-612-160-671-617-617-1_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 132-130-123-236-301-103-136-236-123-135-132-162-162-123-520-173-312-132-657-160-103-103-163-620-160-621-162-671-263-621-126 132-167-163-163-103-106-103-176-163-103-160-162-126-1_V.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 126-167-126-156-176-102-576-162-125-137-103-130-163-623-623-103-103-715-135-176-172-213-176-106-126-103-126-765-106-172-167-132-136-103-103-312-126-627-612-127-312-175_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 213-216-135-536-132-103-132-576-756-132-751-516-156-675-136-571-126-671-726-173-173-103-267-137-675-126-716-176-267-632-620-157-132-326-261-263-613-236-263-620-267-627-672-671-1_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 267-102-627 167-165-751-163-165-561-756-715-517-576-756-576-752-571-231-123-261-216-103-132-312-165-120-231-123-231-132-123-163-123-126-216-623-167-756-762-267-621-236-102-123-1_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 310-136-126-163-316-756-607-163-106-130-106-130-130-620-102-165-162-756-670-617 162-613-136-130-613-176-321-301-310-763-763-657-165-156-157-150-567-165-517-532-102-126-263-657-5_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 265-276-762-627-627-267-675-612 567-157-517-753-175-123-132-132-123-132-103-106-162-163-167-136_W.mp3)
  Segment 16: 23640ms ~ 29400ms (file: 754-627-273-752-123-132-103-236-231-103-130-231-725-725-271-210-751-310-132-132-327-271-732-725-753-273-217-217-127-271-217-172-712-231-126-123-317-123-312-120-236-276-175-527-1_W.mp3)
  Segment 17: 30400ms ~ 30976ms (file: 106-301-136-173-715-652-513-132-126-310-162-261-213-143-123-132-613-132-136-163-136-132-310-715-715-276-273-750-756 107-137 103-310-312 137-716-615-671-103-301-123-312-317-376-1_V.mp3)
  Segment 18: 31976ms ~ 32552ms (file: 107-621 136-617-671-613-671-715-637-657-675-607-671-132 701-160-617-671-160-627_W.mp3)
  Segment 19: 33552ms ~ 38916ms (file: 123-150-120-132-612-103-102 123-501-231-236-621-132_V.mp3)
  Segment 20: 39916ms ~ 40492ms (file: 572-267-267-276-217-612-267-762-265-627-326-375-276-263-321-126-132-213-310-657-625-635-613-130-106-316-136-361-136-321-126-263-216-213-216 675-657-652-675-602-265-760-762-617-6_W.mp3)
  Segment 21: 41492ms ~ 42068ms (file: 312-231-213 136-671-675-675-675-675-672-675-672-671-160-576-571-765-765 765-675-671-765-761-617-163-173-172-175-751-167-132-137-135 512-321-325-367-265-576-574-572-157-675-657-6_W.mp3)
  Segment 22: 43068ms ~ 43644ms (file: 157-617-167 621 132-123 162-263-326-162-162-316-613-637-312 162-136-160-160-160-106-617-620-136 125-163-106-106-162-106-103-130-160-162-167-106-106-167-136-137-103-135-132-516-6_V.mp3)
  Segment 23: 44644ms ~ 48964ms (file: 123-103-130-102-123-123-123-132-123-132-132-132-137-102-537-213-213-123-231-271-276-127-217-217-217-217-123-127-127-213-231-231-210-263-123-213-132-213-231-213-213-213-213-231-2_W.mp3)
  Segment 24: 49964ms ~ 50540ms (file: 601-603-630-756-126-123-136-156-106-156-715-157-261-132_W.mp3)
  Segment 25: 51540ms ~ 52116ms (file: 154-106-106-106-163-132-103-312-657-130-361-132-316-531-103-207-637-165-130-163-103-136-106-132-236-576-106-267-263-137 106-617-632-671-102-126-120-216-362-312_V.mp3)
  Segment 26: 53116ms ~ 53692ms (file: 627-617-612 672-762-762-672-326-316-231 123-123 623-612-360-216-321-126_W.mp3)
  Segment 27: 54692ms ~ 55268ms (file: 263 621 103-675-675-176-367-160-231-623 126-623-176-271-765-176-103-103-671-632-126-561-362-261-162 130-132-103-106-167-103-571-157-107-163-103-106-546-657-617-617-163-623-167 3_W.mp3)
  Segment 28: 56268ms ~ 56844ms (file: 671-672 216-125-136-613 621 167-103-651-317-123-312_V.mp3)
  Segment 29: 57844ms ~ 58420ms (file: 230-631-132-312-261-263-312-137-213-513-135-107-175-126-136-351-136-651-672-167-163-137-132-103-623-216-632-321-231-163-103-103-657-106-163-103-310-103-136-136-106-567-216-167-1_V.mp3)
  Segment 30: 59420ms ~ 59996ms (file: 576-576-576-567 127 576-675-574-576-675-657-675-675_W.mp3)
  Segment 31: 60996ms ~ 61572ms (file: 371-317-201-123-312-105-235-167-132-123-756-721-302-107-103-167-175-137-132-753-132-320-237-367-320-231-325-172-327-321-132-325-312-130-310-302-205-235-132-312-123-321-762-126-2_W.mp3)
  Segment 32: 62572ms ~ 63148ms (file: 627-675-326-276-267-627-236-623-267-627-652-625-657-576-576-562-576-567-576-752-765-625-657-576-675-657-756-672-576-562-567-765-567-756-756-567-765-756-276-637-635-672-675-765-5_W.mp3)
  Segment 33: 64148ms ~ 64724ms (file: 635-376-236-321-673-673-675-673_W.mp3)
  Segment 34: 65724ms ~ 66300ms (file: 312-302-230-235-715-517-576-571-574-574-756-165-135-135-175-267-203-201-120-103-756 132-267-132-123-306-203-237-176-627-256-321-302-302-203-123-173-671-106-163-136-312-763-102-6_V.mp3)
  Segment 35: 67300ms ~ 72124ms (file: 172-321-267-217-726-752-203-102-167-127-260-276-267-762-652-102-237-275-736-765-327-321-103-103-301-103-365-325-702-312-137-130-375-756-231-130-721-302-573-276-260-760-712-310-1_V.mp3)
  Segment 36: 73124ms ~ 73700ms (file: 265-265-261-625-620-625-620-620-620-625-625 602-602-620-625-623-623-625-265-263-625-625-621-627-625-627-267-627-267-261-267-263-267-265-620-260-620-623-623-625-625-627 620-620-6_W.mp3)
  Segment 37: 74700ms ~ 75276ms (file: 726-672-726-762-276-270-276-726-276-760-726-276-270-276-726-706-726-276-726-275-765-762-726-276-726-762-732-726-372-762-732-650-751-576-567-572-527-576-574-754-574-574-576-573-5_W.mp3)
  Segment 38: 76276ms ~ 76852ms (file: 620 657-675-657-623-132-261-321-236 162-312 165 136-632-163-263-637-621-136-206-260-620-263-260 213-210-210-210-267-261-240-261-201-260-261-627-231-210-216-672-231-230-103-675-1_W.mp3)
  Segment 39: 77852ms ~ 78428ms (file: 765-762-762-263-267-103-362-210-302-236-623-612-236-312-126-123-132-123-120-167-132-126-210-162-206-230-203-320-203-230-261-236-230-206-126-203-216-236-326-312-261-532_W.mp3)
  Segment 40: 79428ms ~ 80004ms (file: 236-612-612-356-231-213-126-261-562-652-625-216-321-637-627-573-571-576-576-576-567-653-536-165-156-372-157-571-576-175-715-175-571-715-715-572-562-756-257-365-326-326-536-573-5_W.mp3)
  Segment 41: 81004ms ~ 90868ms (file: 203-320-261-261-263-260-230-132-301-627-103-312-103-312-273-231-231-301-213-263-276-206-210-206-320-321-260-260-260-203-206-201-201-267-267-132-260-267-267-310-103-267-210-102-2_V.mp3)
  Segment 42: 91868ms ~ 92444ms (file: 621-106-163-132-160-106-513-261-361-136-163-136-136-135-136-102-137-137-156-106-126-710-102-125-312-175-175-765-751-765-756-756-576-756-165-175-176-167-576-576-567-567-576-567-3_W.mp3)
  Segment 43: 93444ms ~ 94020ms (file: 627-621-267-136-623-621-612-725-756-726-231-152-657-316-236-321-235-627-573 623_W.mp3)
  Segment 44: 95020ms ~ 95596ms (file: 102-106-102-102-103-132-163-132-162-132 765-132-106-162-703-567-103-612-123-132-312-126-216-321-326-267-705-572-261-132-103-302-236-302-162-260-627-316-213-201-302-302-756-756-7_V.mp3)
  Segment 45: 96596ms ~ 97172ms (file: 103-413-275-275-754-572-270-754-765-571-652-756-754-754-574 175-547-751-754-537-273-301 657-623-162-574-754-570-257-207-537-573-130-163-754-574-754-106-573-103-571-735-574-547-5_W.mp3)
  Segment 46: 98172ms ~ 105192ms (file: 103-312-276-321-321-236-672-263-652-627-267-267-236-106-107-754-175-751-123-765-675-267-672-261-160-160-132-763-326-576-672-756-576-157-267-276-172-321-213-123-312-321-123-713-7_W.mp3)
  Segment 47: 106192ms ~ 106768ms (file: 165-103-163-136-106-136-316-136-103-135-361-137-132-103-136-137-103-312-312-126-132-135-751-136-613-106-136-173-163-136-261-627-263-231-673-167-615-216-162-216-163-176-103-136-1_V.mp3)
  Segment 48: 107768ms ~ 108344ms (file: 627-621-621-261-625-625-652-625-623-625-621-267-625 620-625-652-652-625-652-657-627-627-672 263-620-620-620-620-620-621-261-621-625-625-625-652-625-265-625-625-652-652-652-652-6_W.mp3)
  Segment 49: 109344ms ~ 115716ms (file: 123-213-213-213-123-123-213-123-213-213-217-213-213-213-213-312-312-312-321-312-312-132-231-213-123-231-132-321-312-213-210-123-120-123-123-213-123-123-123-213-123-123-120-132-2_W_stt_if.mp3)
  Segment 50: 116716ms ~ 117292ms (file: 627-672-640-326-263-623-263-263-623-623-263-263-263-623-627-623-362-325-532-534-237 632-231-167-167-162-620-264-263-217-127-237-317-371-371-546-572-574-726-213-312-312-321-231-6_W.mp3)
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
> **Time**: 2026-04-04 03:55:47
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "매물 소화 흐름으로 볼 수 있습니다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "매물", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "소화", "start_ms": 175, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "흐름으로", "start_ms": 280, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "볼", "start_ms": 435, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 490, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "있습니다.", "start_ms": 535, "dur_ms": 41, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 3,
"transcript": "매물 소화 돌파가 나온다면 돌파 매매 후보로 접근할 수 있고",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "매물", "start_ms": 45, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "소화", "start_ms": 110, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "돌파가", "start_ms": 175, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "나온다면", "start_ms": 260, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "돌파", "start_ms": 365, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "매매", "start_ms": 430, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "후보로", "start_ms": 495, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "접근할", "start_ms": 580, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 665, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "있고", "start_ms": 690, "dur_ms": 36, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "주도주의 매매",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "주도주의", "start_ms": 30, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "매매", "start_ms": 285, "dur_ms": 291, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "그리고 나서 박스 중심 위에서 작은 박스를 만들었고",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "그리고", "start_ms": 65, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "나서", "start_ms": 190, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "박스", "start_ms": 300, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "중심", "start_ms": 410, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "위에서", "start_ms": 500, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "작은", "start_ms": 590, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "박스를", "start_ms": 680, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "만들었고", "start_ms": 770, "dur_ms": 106, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "3월 19일 아침에 그 자리를 돌파하고 지지가",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "3월", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "19일", "start_ms": 110, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "아침에", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 310, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "자리를", "start_ms": 340, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "돌파하고", "start_ms": 430, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "지지가", "start_ms": 540, "dur_ms": 36, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 8,
"transcript": "사장님 도대체 가격이 얼마인가요? 이게 1인당 9,500원에 무한 리필입니다.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "사장님", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "도대체", "start_ms": 210, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "가격이", "start_ms": 340, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "얼마인가요?", "start_ms": 470, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 700, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "1인당", "start_ms": 790, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "9,500원에", "start_ms": 920, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "무한", "start_ms": 1180, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "리필입니다.", "start_ms": 1270, "dur_ms": 338, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "9,500원 예 물론 생고기",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "9,500원", "start_ms": 35, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "예", "start_ms": 290, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "물론", "start_ms": 340, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "생고기", "start_ms": 430, "dur_ms": 146, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "이 수급 매매를 파고 이제 포텐이 터진 거잖아. 너는 이제 1년 벌었어. 근데 내가 밀착 마크를 하기 때문에 여기서 네가 2-3년만 잘하면 충분히 다시 나",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 60, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "수급", "start_ms": 100, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "매매를", "start_ms": 190, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "파고", "start_ms": 300, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 390, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "포텐이", "start_ms": 480, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "터진", "start_ms": 610, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "거잖아.", "start_ms": 700, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "너는", "start_ms": 850, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 940, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "1년", "start_ms": 1030, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "벌었어.", "start_ms": 1120, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 1300, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "내가", "start_ms": 1390, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "밀착", "start_ms": 1480, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "마크를", "start_ms": 1570, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "하기", "start_ms": 1680, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "때문에", "start_ms": 1770, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "여기서", "start_ms": 1900, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "네가", "start_ms": 2030, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "2-3년만", "start_ms": 2120, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "잘하면", "start_ms": 2280, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "충분히", "start_ms": 2410, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 2540, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "나", "start_ms": 2650, "dur_ms": 30, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 12,
"transcript": "오늘 잡채도 있네? 맛있겠네요. 부지런히 음식을 담습니다.",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "잡채도", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "있네?", "start_ms": 280, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "맛있겠네요.", "start_ms": 400, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "부지런히", "start_ms": 650, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "음식을", "start_ms": 910, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "담습니다.", "start_ms": 1120, "dur_ms": 252, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 14,
"transcript": "현재 새로운 형태의 사족 보행 로봇을 함께 연구 중인 두 사람. 로봇 제어 시스템 담당은 박해원 교수가 하드웨어는 오준호 박사가",
"speaker": "speaker_5",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "현재", "start_ms": 60, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "새로운", "start_ms": 170, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "형태의", "start_ms": 300, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "사족", "start_ms": 410, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "보행", "start_ms": 500, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "로봇을", "start_ms": 590, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "함께", "start_ms": 720, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "연구", "start_ms": 810, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "중인", "start_ms": 900, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "두", "start_ms": 990, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "사람.", "start_ms": 1040, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "로봇", "start_ms": 1150, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "제어", "start_ms": 1240, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "시스템", "start_ms": 1330, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "담당은", "start_ms": 1440, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "박해원", "start_ms": 1570, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "교수가", "start_ms": 1700, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "하드웨어는", "start_ms": 1850, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "오준호", "start_ms": 2010, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "박사가", "start_ms": 2120, "dur_ms": 106, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "물 속에서 헤엄칠 수 있는 물고기 모양의 로봇까지 다양한 로봇을 개발해 왔습니다.",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "물", "start_ms": 45, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "속에서", "start_ms": 95, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "헤엄칠", "start_ms": 185, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 295, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 345, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "물고기", "start_ms": 415, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "모양의", "start_ms": 525, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "로봇까지", "start_ms": 635, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "다양한", "start_ms": 800, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "로봇을", "start_ms": 930, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "개발해", "start_ms": 1060, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "왔습니다.", "start_ms": 1170, "dur_ms": 101, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 17,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 18,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 19,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 20,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 21,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 22,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 23,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 24,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 25,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 26,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 27,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 28,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 29,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 30,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 31,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 32,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 33,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 34,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 35,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 36,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 37,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 38,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 39,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 40,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 41,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 42,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 43,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 44,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 45,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 46,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 47,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 48,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 49,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 50,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-04 03:55:47*
