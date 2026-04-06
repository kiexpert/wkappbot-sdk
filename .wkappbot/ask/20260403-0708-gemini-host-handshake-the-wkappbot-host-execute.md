# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=07:07:14  uptime=20880s  mem=1449MB  system_processes=445"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_070706.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 157-105-160-260-312-163-107-167-176-106-162-136-132-103-132-123-126-162-167-167-670-675-756-671-306-602-206-130-657-167-132-103-310-103-103-615-130-601-163-312-356-301-316-135-5_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 106-160-627-765-762-673-176-103-132-762-710-736-316-213-761-750-175-621-126-236-132-231-163-263-752-126-216-127-126-613-103-106-136-167-162-163-160-136-261-132-126-167-612-165-2_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 132-760-236-623-206-267-162-103-106-301-613-206-321-231-316-612-302-316-136-102-162-123-162-312-136-132-176-310-306-637-301-361-612-162-362-632-630-362-263-623-236-261-236-260-2_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 216-263-126-123-126-517-156 163-157-175-175-107-316-312-576-573-137-376_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 321-231-573-753 301-316-132-312-203-326-213-132-132-136-103-123-132-312-321-132-136-213-321-132-206-206-206-127-261-206-250-512-123-126-123-132-301-750-706-103-317-507-570-571-5_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 765-276-206-203-132-163-123-163-126-102-126-130-103-310-231-162-612-126-102-106-163-103-176-130-132-750-750-571-710-765-760-167-106-106 310-635-631-631-310-275-572-756-760-607-6_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 102-103-106-312-132-203-612-310-301-136-621-217-316-576-756-672-612-623-263-231-231-213-261-103-367-326-103-631-301-276-102-132-570-517-567-163-761-326-621-261-231-623-137-613-1_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 316-361-326-263-652-562-562-265-576-567-756-752-527-657-675-627-675-165-534-576-756-621-261-625-652-657-312-321-351-534-574-253-253-231-312-675-362-523-526-526-256-254-325-532-5_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 607-326-301-210 567-657-650-756-765-672-167-716-756-571-756-571-756-756-756-310-123-320-132 125-512-321-126-215-123-213-120-123-120-231-120-312-216-236-327-267-273-273-273-236-2_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 765-756-765-765-756-756-756-756-756 765-765-756-765-752-761-762-751-765 756-756-756-765-765-763 367-312-310-312-132-231-310-672-673-761-763-763-761-765-672-672-671-671-167-163-7_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 316-603-206-310-130-316-132-756-276-627-632-267-276-312-310-130-310-761-713-173-617-671-176-765-602-620-260-260-302-354-356-602-632-623-206-310-103-103-136-103-301-672-276-310-3_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 617-706-760-670-607-602-302-302-321-301-316-312-132-621-163-301-312-562-526-162-602-136-130-136-760-673-132-721-762-607-621-607-267-627-765-675-607-705-760-361-310-136-106-750-7_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 352-530-653-673-623-132-632-675-572-326-571-572-576-571-156-751-751-756-756-576-567-156-156-516-156-315-102-210-231-132-321_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 621 263-236-263-326-265 620-261-576-657-627-672-627 632-362-310-236-261-316-132-312-302-231 627-261-267-372-237-627-260-263-236-623-657-657-765-657-263 163-136 136-136-523-256-2_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 325-215-526-567-567-563-576-563-653-365-652-256-537-576-156-235-567-653-123-120-136-132-130-126-165-135-651-150-135-120-126-132-536-615-136-157-512-571-527-572 103-326-561-576-5_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 206-273-237-710-570-571-375-167-176-673-762-130-310-230-231-750-702-273-263-236-123-132-136-526-625-675-671-675-761-321-132-236-320-307-107-126-570-750-750-631-631-713-710-106-1_V.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 627-627-621-627-162-671-672-627-362-627-162-261-162 657-167-167-167-765-162-316-316 132 617-132-103-612-765-671-675-675-617-765-675-675-675-675-625-263-251-236-123-163-136-130-5_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 120-312-312-316-132-312-123-612-321-312-612-312-362-312-132-123-321-621-621-621-621-621-620-621-620-623-620-624-621-620-623-620-621-620-623-621-623-265-625-620-627-621-621-623-6_W.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 760-567-302-302-326-263-320-321-315-135-136-103-103-576-756-706-632-673-657-632-203-275-273-306-132-375-306-321-716-760-672-132-361-637-132-765-763-103-632-263-236-763-763-103-1_V.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 167-617-126-621-126-162-126-612-163-120_W.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 625-652-657-627-627-602-162-106-106-570-137-301-301-537-574-574-157-106_V.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 162 136-165-361-657-651-675-601-162-106-602-632-126-361-617-621-627-260-362-362-762-627-617-267-627-260-260-261-126-123-162-261-260-260-620-602-765-760-157-157-617-716-562-605-5_W.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 325-362-625-713-627-546-264-217-315-261-127-456-326-276-251-127-321-267-237-374-561-214-367-365-327-217-153-123-326-326-621-326-231-236-123-231-236-263-231-236-216-263-206-236-2_V.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 126-213-103-102-136-312-103-302-203-210-126-206-320-213-132-201-206-260-326-302-536-315-160-631-136-765-675-263-106-261-162-261-316-312-103-136-163-317-103-163-136-103-130-103-3_V.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 316-123-617-136-263-236-362-360 263-106-127-123-137-162-163-615-132-136-260-162-162-617-136-176-176-162-610-671-231-132-216-603-167-316-136-316-136-163-126-106-136-615-623-632-1_V.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 752-275-572-572-576-574-574-576-576-215-257-562-256-527-523-325-351-275-217-215-231-237-236-237-357-325-352-325-327-235-327-237-352-534-523-527-235-236-657-567-527-526-576-567-5_W.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 230-316-203-302-265-165-302-312-321-301-637-123-136-570-715-375-234-312-316-312-310-132-310-301-612-132-132-761-317-371-362-657-673-106-612-607-107-103-673-276-236-761-762-726-1_W.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 601-607-132-167-162-106-163-126-136-756-136-106-103-713-176-765-607-701-675-163-136 537-625-657-136-162-267-310-123-617-514-310-162-106-125-213-206-231-106-103-136-132-132-621-6_V.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 672-627-675-675-672-362-213-372-376-612-672-602-672-260-602-276-263-263-165-617-156-176-176-726-675 574-576-576-675-265-675-625-567-576-657-576-657-576-657-567-576-567-574-756-7_W.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 213-136-132-130-102-132-126-106-561-567-562-567-567-567-576-756-752-762-316-136-163-536-563-657-657-567-576-657-567-657-567-576-567-576-657-657-657-657-657-567-576-756-756-567-5_W.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 231-301-132-631-230-134-132-123-657-750-574-735-376-675-756-527-356-375-357-375-357-627-672-574-756-672-762-762-726-267 756-765-167-617-103-317-317-310 310-310-163-136-621-627-2_W.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 763-761-760-763-760-762-765-765-765-761-671-231-361-312 756-765-675-612-617-167 715-756-765-761-760-761-765-716-756-715-761-617-671-675-765-657-576-765-405-765 765-765-762 520-5_W.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 143-126-316-312-136-675-632-176-103-163-106-627-623 610-637-362-261-132-175-137-267-623-163-216-126-127-102-102-162-126-310-316-103-123-126-312-316-163-760-761-601-602-137-617-1_V.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 765-761-761-763-763-761-672-751-716-576-765-765-762-763-761-716-765-765-763-675-762-756-126_W.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 136-160-612-316-136-160-162-163-310-671-163-173-163-136 762-756-735-675-257-265-126-765-765-765-756-765-765-765-675 621-623-312-715-765-627-672-675-567-765-167-675-612-762-762-2_W.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 657-765-762-762-675-675-675-765-675-765-675-756-756-765-765-756-675-576-576 765-765-765-576-756-756-765-762-675-762-762-762-675-762-763-756-765-765 564-763-765-761-765 675-762 5_W.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 376-762-362 326-326-236-213-261-321 625-267-326-265-657-265-625-652-256-657-526-657-652-657-657-675-627-265-675-652-625-621-132-132-612-165-675-675-765-356-361-657-657-267-263-6_W.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 357-103-103-163-267-623-267-236-263-263-203-237-306-301-310-130-301-163-310-130-620-265-306-130-103-320-361-306-302-302-103-106-163-103-132-162-103-103-136-126-106-601-261-162-1_V.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 316-103-132-650-675-657-576-567-567-572-567-627-675-765-726-756-273-267 351-167-312-132-103-103-163-230-236-261-206-760-107-570-573-136-321-310-132-103-103-102-201-102-210-103-1_V.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 756-756-756-765-765-765-576-576-756-576_W.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 673-623-316-672-162-627-627-621-617-167-163-167-267-132-103-130-102-120-126-132-126-216-126-261-210-236-261-263-163-132-123-210-126-106-310-123-136-216-216-213-123-621-132-612-1_W.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 621-260-547-504-514-514-154-625-237-573-576-574-576-302-302 570-570-157-157-261-123-231-137-103-130-106-163-106-106-607-621-601-160-267-132-302-130-620-137-130-153-135-106-123-1_V.mp3)
  Segment 43: 66192ms ~ 66768ms (file: 162-157-165-105-216-165-621-102-106-102-120-132-623-123-612-312-126-213-103-132-316-123 106 213-725-765-765-721-210-321-236-321-267-632-130-310-312-362-632-362-260-213-321-362-1_V.mp3)
  Segment 44: 67768ms ~ 68344ms (file: 106-176-106-601-126-125-136-106-163-163-612-106-120-125-130-103-712-162-103-137-126-613-163-136-160-136-103-136-103-136-136-163-126-312-572-312-137-612-167-163-126-102-136-103-1_V.mp3)
  Segment 45: 69344ms ~ 69920ms (file: 562-267-672-276-326-675-762-367-576-527-623-236-231-231-675-672-627-267-267-627-672-671-546-654-627-672-672-675-765-672-672-675-654-267-267-231-321-726-267-627-672-546-654-675-3_W.mp3)
  Segment 46: 70920ms ~ 71496ms (file: 126 761-765-761-130-126-712-327-162-265-576-576-576-762-715-576-157-362-167-170-102-163-137-103-316-526-167-103-263-726-132-316-103-651-165-615-617-172-620-172-157-107-127-136-1_V.mp3)
  Segment 47: 72496ms ~ 73072ms (file: 163-163-130-167 160-567-126-160-106-143-170-165-136-261-132-137-176-103-134-123-756-301-172-162-631-623-632-163-623-156-167-316-136-361-132-103-103-136-106-106-167-610-167-130-2_V.mp3)
  Segment 48: 74072ms ~ 74648ms (file: 103-103-103-150-103-320-136-310-320-203-213-132-216-623-231-263-621-623-306-603-631-236-316-631-126-103-236-263-263-732-372-612-632-361-210-102-167-612-632-561-216-216-612-216-2_W.mp3)
  Segment 49: 75648ms ~ 76224ms (file: 756-263-136-362-671-367-213-263-231-265-201-362-576-123-206-263-236-263-136 132-132-136-132 316-671-657-127-123-125-567-673-576-750-165-103 563-675-602-106-610-157-126-175-167-1_W.mp3)
  Segment 50: 77224ms ~ 77800ms (file: 103-106-123-712-173-137-132-134-175-317-103-132-136-136-701-672-376-167-157-103-671-621-627-612-126-570-576-163-301-750-213-136-163-132-301-310-312-132-370-673-137-637-761-173-3_V.mp3)
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
> **Time**: 2026-04-03 07:08:38
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "오늘 인터뷰에 응해주셔서 감사합니다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "인터뷰에", "start_ms": 160, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "응해주셔서", "start_ms": 350, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "감사합니다.", "start_ms": 580, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "천만에요. 저도 이런 기회를 갖게 되어 기쁩니다.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "천만에요.", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "저도", "start_ms": 210, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 320, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "기회를", "start_ms": 440, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "갖게", "start_ms": 590, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "되어", "start_ms": 700, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "기쁩니다.", "start_ms": 800, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "먼저 본인 소개를 부탁드려도 될까요?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "먼저", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "본인", "start_ms": 165, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "소개를", "start_ms": 285, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "부탁드려도", "start_ms": 435, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "될까요?", "start_ms": 645, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "네, 저는 웹 개발자로 일하고 있는 박지민입니다.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 60, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "저는", "start_ms": 150, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "웹", "start_ms": 270, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "개발자로", "start_ms": 340, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "일하고", "start_ms": 500, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 640, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "박지민입니다.", "start_ms": 730, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "개발 일을 시작하신 지는 얼마나 되셨나요?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "개발", "start_ms": 45, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "일을", "start_ms": 185, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "시작하신", "start_ms": 305, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "지는", "start_ms": 495, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "얼마나", "start_ms": 595, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "되셨나요?", "start_ms": 735, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "올해로 벌써 5년 차가 되었습니다.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "올해로", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "벌써", "start_ms": 200, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "5년", "start_ms": 320, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "차가", "start_ms": 430, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "되었습니다.", "start_ms": 530, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "주로 어떤 기술 스택을 사용하시나요?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "주로", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 160, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "기술", "start_ms": 280, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "스택을", "start_ms": 400, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "사용", "start_ms": 550, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "하시나요?", "start_ms": 660, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "주로 리액트와 노드 제이에스를 활용해서 풀스택 개발을 하고 있습니다.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "주로", "start_ms": 55, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "리액트와", "start_ms": 175, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "노드", "start_ms": 365, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "제이에스를", "start_ms": 475, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "활용해서", "start_ms": 685, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "풀스택", "start_ms": 875, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "개발을", "start_ms": 1035, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "하고", "start_ms": 1195, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "있습니다.", "start_ms": 1295, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "개발자로서 가장 보람을 느끼는 순간은 언제인가요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "개발자로서", "start_ms": 110, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 470, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "보람을", "start_ms": 600, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "느끼는", "start_ms": 790, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "순간은", "start_ms": 960, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "언제", "start_ms": 1150, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "인가요?", "start_ms": 1280, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "제가 만든 서비스가 사람들에게 실질적인 도움이 될 때 정말 큰 행복을 느낍니다.",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "제가", "start_ms": 120, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "만든", "start_ms": 280, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "서비스가", "start_ms": 420, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "사람들에게", "start_ms": 680, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "실질적인", "start_ms": 990, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "도움이", "start_ms": 1250, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "될", "start_ms": 1440, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 1530, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 1640, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 1800, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "행복을", "start_ms": 1900, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "느낍니다.", "start_ms": 2090, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "어려운 점은 어떻게 극복하시나요?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "어려운", "start_ms": 45, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "점은", "start_ms": 205, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 325, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "극복", "start_ms": 455, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "하시나요?", "start_ms": 565, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "동료들과 소통하거나 다양한 커뮤니티에서 정보를 얻으며 해결책을 찾으려 노력합니다.",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "동료들과", "start_ms": 110, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "소통하거나", "start_ms": 420, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "다양한", "start_ms": 780, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "커뮤니티에서", "start_ms": 990, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "정보를", "start_ms": 1400, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "얻으며", "start_ms": 1590, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "해결책을", "start_ms": 1750, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "찾으려", "start_ms": 2010, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "노력", "start_ms": 2200, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "합니다.", "start_ms": 2330, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "앞으로의 목표는 무엇인가요?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "앞으로의", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "목표는", "start_ms": 230, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "무엇", "start_ms": 360, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "인가요?", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "더 많은 사용자에게 가치 있는 경험을 선사하는 시니어 개발자가 되고 싶습니다.",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "더", "start_ms": 120, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "많은", "start_ms": 230, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "사용자에게", "start_ms": 390, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "가치", "start_ms": 700, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 860, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "경험을", "start_ms": 990, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "선사하는", "start_ms": 1180, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "시니어", "start_ms": 1440, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "개발자가", "start_ms": 1650, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "되고", "start_ms": 1910, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "싶습니다.", "start_ms": 2040, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "훌륭한 목표네요. 응원하겠습니다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "훌륭한", "start_ms": 40, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "목표네요.", "start_ms": 210, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "응원", "start_ms": 370, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "하겠습니다.", "start_ms": 480, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "감사합니다.",
"speaker": "speaker_2",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "감사", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "합니다.", "start_ms": 190, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "마지막으로 다른 개발자들에게 하고 싶은 말이 있나요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "마지막으로", "start_ms": 60, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "다른", "start_ms": 320, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "개발자들에게", "start_ms": 440, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "하고", "start_ms": 800, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "싶은", "start_ms": 910, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "말이", "start_ms": 1040, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있나요?", "start_ms": 1150, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "계속해서 배우고 공유하는 자세를 잃지 않는 것이 중요하다고 말하고 싶습니다.",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "계속해서", "start_ms": 120, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "배우고", "start_ms": 380, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "공유하는", "start_ms": 590, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "자세를", "start_ms": 850, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "잃지", "start_ms": 1040, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "않는", "start_ms": 1170, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "것이", "start_ms": 1300, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "중요하다고", "start_ms": 1410, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "말하고", "start_ms": 1700, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "싶습니다.", "start_ms": 1890, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "오늘 귀한 시간 내주셔서 다시 한번 감사드립니다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "귀한", "start_ms": 180, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "시간", "start_ms": 300, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "내주셔서", "start_ms": 410, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 620, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "한번", "start_ms": 740, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "감사", "start_ms": 850, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "드립니다.", "start_ms": 960, "dur_ms": 200, "phase_lr_deg": 0.0}
]
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
"transcript": "네, 수고하셨습니다.",
"speaker": "speaker_2",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "수고", "start_ms": 130, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "하셨습니다.", "start_ms": 290, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "한국의 정보 기술 산업은 무척 빠르게 변화합니다. 특히 웹 개발 분야에서는 새로운 프레임워크와 도구들이 쉴 새 없이 등장하죠. 이러한 흐름에 발맞추기 위해 한국의 개발자들은 퇴근 후에도 꾸준히 공부하는 '자기 계발' 열풍이 대단합니다. 스터디 모임이나 기술 블로그 운영이 매우 활발한 것도 이러한 문화를 잘 보여줍니다.",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "한국의", "start_ms": 150, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "정보", "start_ms": 410, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "기술", "start_ms": 600, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "산업은", "start_ms": 790, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "무척", "start_ms": 1050, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "빠르게", "start_ms": 1240, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "변화합니다.", "start_ms": 1470, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "특히", "start_ms": 2000, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "웹", "start_ms": 2210, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "개발", "start_ms": 2300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "분야에서는", "start_ms": 2460, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "새로운", "start_ms": 2870, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "프레임워크와", "start_ms": 3130, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "도구들이", "start_ms": 3590, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "쉴", "start_ms": 3880, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "새", "start_ms": 3990, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "없이", "start_ms": 4100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "등장하죠.", "start_ms": 4260, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이러한", "start_ms": 4800, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "흐름에", "start_ms": 5010, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "발맞추기", "start_ms": 5240, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "위해", "start_ms": 5550, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "한국의", "start_ms": 5710, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "개발자들은", "start_ms": 5920, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "퇴근", "start_ms": 6280, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "후에도", "start_ms": 6470, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "꾸준히", "start_ms": 6680, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "공부하는", "start_ms": 6940, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "'자기", "start_ms": 7250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "계발'", "start_ms": 7410, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "열풍이", "start_ms": 7600, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "대단합니다.", "start_ms": 7830, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "스터디", "start_ms": 8350, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "모임이나", "start_ms": 8610, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "기술", "start_ms": 8920, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "블로그", "start_ms": 9110, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "운영이", "start_ms": 9340, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "매우", "start_ms": 9600, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "활발한", "start_ms": 9760, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "것도", "start_ms": 9990, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이러한", "start_ms": 10150, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "문화를", "start_ms": 10340, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 10570, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "보여줍니다.", "start_ms": 10680, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "오늘 인터뷰가 도움이 되셨기를 바랍니다.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "인터뷰가", "start_ms": 180, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "도움이", "start_ms": 390, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "되셨기를", "start_ms": 550, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "바랍니다.", "start_ms": 740, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "네, 저도 정말 유익한 시간이었습니다.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 45, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "저도", "start_ms": 135, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 245, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "유익한", "start_ms": 375, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "시간이었습니다.", "start_ms": 535, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "다음에 또 뵙겠습니다.",
"speaker": "speaker_1",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 200, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "뵙겠습니다.", "start_ms": 290, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "네, 안녕히 가세요.",
"speaker": "speaker_2",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 35, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 125, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "가세요.", "start_ms": 335, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "조심히 들어가세요.",
"speaker": "speaker_1",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "조심히", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "들어가세요.", "start_ms": 230, "dur_ms": 250, "phase_lr_deg": 0.0}
]
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
"transcript": "오늘 하루도 행복하시길 바랍니다.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "하루도", "start_ms": 180, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "행복하시길", "start_ms": 340, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "바랍니다.", "start_ms": 600, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 32,
"transcript": "이 노래 정말 좋지 않나요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 45, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "노래", "start_ms": 115, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 235, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "좋지", "start_ms": 365, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "않나요?", "start_ms": 475, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "제가 제일 좋아하는 곡이에요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "제가", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "제일", "start_ms": 160, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "좋아하는", "start_ms": 270, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "곡이에요.", "start_ms": 430, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "가사가 정말 감동적이네요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "가사가", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 210, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "감동", "start_ms": 330, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "적이네요.", "start_ms": 440, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "멜로디도 정말 아름답고요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "멜로디도", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 230, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "아름", "start_ms": 360, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "답고요.", "start_ms": 470, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "가끔은 이런 음악이 큰 위로가 되죠.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "가끔은", "start_ms": 55, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 245, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "음악이", "start_ms": 365, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 525, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "위로가", "start_ms": 615, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "되죠.", "start_ms": 765, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "맞아요. 지친 하루를 달래주는 느낌이에요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "맞아요.", "start_ms": 45, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "지친", "start_ms": 185, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "하루를", "start_ms": 305, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "달래주는", "start_ms": 465, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "느낌", "start_ms": 655, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이에요.", "start_ms": 765, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "함께 들어주셔서 감사합니다.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "함께", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "들어주셔서", "start_ms": 170, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "감사합니다.", "start_ms": 380, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "별말씀을요. 저도 덕분에 힐링했는걸요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "별말씀을요.", "start_ms": 50, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "저도", "start_ms": 280, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "덕분에", "start_ms": 400, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "힐링했는걸요.", "start_ms": 550, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 41,
"transcript": "이제 정말 가야겠네요.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 150, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "가야", "start_ms": 270, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "겠네요.", "start_ms": 380, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "조심히 들어가시고 연락 주세요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "조심히", "start_ms": 45, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "들어가시고", "start_ms": 205, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "연락", "start_ms": 395, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "주세요.", "start_ms": 505, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "네, 그럴게요. 안녕히 계세요.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "그럴게요.", "start_ms": 130, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 290, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "계세요.", "start_ms": 480, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "안녕히 가세요.",
"speaker": "speaker_1",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 30, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "가세요.", "start_ms": 240, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 46,
"transcript": "오늘도 수고 많으셨습니다.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "오늘도", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "수고", "start_ms": 210, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "많으셨습니다.", "start_ms": 340, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "내일 또 뵙기를 기대하겠습니다.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "내일", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 170, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "뵙기를", "start_ms": 260, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "기대하겠습니다.", "start_ms": 420, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "안녕히 주무세요.",
"speaker": "speaker_1",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 35, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "주무세요.", "start_ms": 265, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "좋은 꿈 꾸시고요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "꿈", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "꾸시고요.", "start_ms": 250, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "감사합니다.",
"speaker": "speaker_1",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "감사", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "합니다.", "start_ms": 190, "dur_ms": 180, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 07:08:38*
