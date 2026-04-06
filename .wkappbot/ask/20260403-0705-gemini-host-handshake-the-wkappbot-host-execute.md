# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=07:04:01  uptime=20686s  mem=1449MB  system_processes=448"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_070352.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 321-573-574-753-754 523-523-532-356-312-312-316-326-235-362-327-326-326-263-362-627-627-627-267-267-627-672-627-762-672-627-627-276-162-236-612-567-365-236-273-276-326-540-541-5_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 765-657-675-301-301-316-301-106-321-124-123-103-310-501-571-627-103-136-135-153-156-574-750-562-516-261-547-574-756-756-756-765-756-320-306-130-326-756-230-326-502-105-103-130-5_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 310-310-301-301-302-312-312-103-130-103-312-302-302-302-321-302-310-320-203-326-321-103-132-127_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 210-612-167-617-612-126-216-216-756-627-236-237-213-321-310-236-263-263-236-132-123-213-123-267-612-672-672-672-762-672-675-675-657-765-351-312-321-263-126-260-671-675-671-670-6_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 672-267-265-267-267-276-627-267-726-327-637-267-621-627-267-260-627-623-627-263-265-263-261-267-267-236-236-613-265-657-267-263-326-273-736-362-267-156-172-351-253-623-263-326-2_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 132-102-130-102-312-362-132-213-732-623-231-120-162-621-267-231-235-736-637-216-261-216-213-132-132-276-617-627-620-260-260-126-123-203-216-276-576-102-162-621-260-132-652-267-3_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 615-167-610-637-673-316-637 736-675-763 167-617-631-167-672-617-672-637-567-675-675-675-672-625-765-673-175-673-362-362-231-236-263-263-625-625-621-651-625-615-625-621-261-621-1_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 132-136-312-371-216-241-132-613-312-621-657-652-546-564-612-630-632-623-263-623-203-263-236-123-103-132-103-132-123-312-236-675-750-574-705-756-756-507-576 321-327-571-567-105-1_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 652-130-310 576-673-672-710-176-167-316-126-317_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 216-260-267-263-765-765-625-672-627-572-527-267_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 613-130-130-123-503-576-536-547-301-312-573-310-175-302-317-627-657-635-136-162-263-675-267-615-361-567-657-576-657-671-671-763-675 301 321-312-756-256-672-136-167-574-362-103-2_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 123-175-167-167-167-126-163-671-632-671-631-613-610-612-160-132-267-672-127-671-126-216-162-160-610-612-162-627-162-163-132-136-103-132-673 132-123-132-102 673-137-261-126-123-2_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 271-672-160-612-135-132-103-136-106-103-561-672-167-156-106-106-163-136-102-103-126-261-617-126-162-103-132-103-162-106-103-137-130-715-163-167-123-163-106-132-103-123-316-316-1_V.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 510-267-123-127-712-371-127 321-312-321-321-326-231-173-317-613-167-132-312-321-326-126-326 726-276-726-732-746-132-312-132-374-132-361-312-213-132-213-132-461-154_V.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 136-316-126-106-103-672-617-617-617-167-617-163-613-167-162-632-132-621-136-162-163-761-163-607-163-267-652-126-632-163-316-130-106-106-106-631-106-216-103-613-163-524-167-601-6_V.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 574-157-651-675-672-623 136-623-321-726-765-572-670-637-567-735-576-517-537-571 625-625-652-657-610-602-267-276-263-367-362_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 756-301-103-132-765-672-567-675 675 130-103-762-672-601-623-312-126-126-216 362-312-231-123 637-512-625-653-675-267-106-102-265-261-621-162 216-210-521-216-263-625-132-160-162-1_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 173-326-136-301-136-231-573-213-231-205-125-201-132-120-750-132-173-237-123-312-136-301-310-217-103-126-271-231-725-236-237-231-236-302-351-123-132-321-321-503-572-312-132-123-5_V.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 163-162-103-750-137-735-761-357-361-362-756-671-763-103-160-163-613-362-631-601-360-627-620-620-620-623-263-263-362-312-613-631-136-136-106-627-762-672-726-762-726-672-672-627-2_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 576-136-761-176-163-617-615-106-167-627-216-601-163-612-176-106-176-716-617-106-167-615-162-167-175-106-675-617-107-623-261-601-162-102-176-157-612-617-126-612-167-621_V.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 637-627 167-162-163 236-236-213-260-206-230-260-657-567-576-562-756-756-751-357-675-675-675-623-263-361-312-726-627-637-726-170-273-276-173-715-751 765-162-163-162_W.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 163 253-153-132-123-756-765-756-751-126 176 162-176-617-167-261-726 167-617-367 637-760-173 170 617-763-765-675-765-765-765-762-761-713-617-716-167-123-173-173-312-162-132-162-1_W.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 561-356-376-523-523-362-275-276-267-123-132-123 264-261-261-267-276-672-267-542-547-547-547-537-157-156-157-165-135-167-162-576-571-574-517-574-576-521-125-126-621-612-517-571 3_W.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 126-123-103-123-312-130-103-136-173-136-132-126-106-612-567-671-617-106-631-103-107-320-230-126-201-102-103-651-160-206-216-163-102-103-123-137-206-206-160-206-602-602-602-675-6_V.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 601-631-326-312 657-501-326-765-765-165-102-217-167-263-162-210-126-132-103-103-103-106-162-106-106-603-675-162-201-310-316-301-310-310-326-310-362-362-632-236-165-312-106-601-6_V.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 263-231-236-672-327-312-126-213-123-123-231-213-123-132-312-321-321-132-312-132-312-213-321-326-325-536-564-516-657-675-671-672-673-637-631-136-132-321-213-231-321-237-237-237-6_W.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 652-562-352-163-675-136-316-326-327-573-753-756-754-753-751-754-756-372-327-265-265-526-657-365-236-236-567-657-657-570-576-756 576-765-267-132-261-261-236-261-576-675-675-675-2_W.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 175-162-213-376-316-173-716-207-120-257-572-570-756-306-162-726-652-672-126-106-162-157-103-310-136-361-102-127-167-765-756-652-362-312_W.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 276-267-271-127-761-762-362-137-237-312-261-275-231-172-261-627-627-276-621-267-261-672-623-361-213-267-206-260-217-217-276-270-276-762-267-762-762-732-751-317-317-310-137-317-3_W.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 657-160-102-160-106-567-102-103-756-106-167-132-236-263-267-627-623-316-765-627-126-571-205-321-621-623-652-675-672-627-726-762-671-765-267-627-267-256-257-236-652-756-256-326-2_W.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 765-756-765-260-236-263-236-206-260-203-672-163-106-163-316-623-263-261-162-102-167-132-751-706-761-123-631-602-623-176-153-150-576-571-163 123-132-126-570-537-576-256-267-267-2_W.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 326-267-250-260-630-612-260-163-162-103-301-312-130-607-371-130-675-756-103-751-301-130-301-316-317-503-375-360-603-306-326-103-312-321-301-305-132-310-132-132-103-130-302-706-7_V.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 103-103-132-675-607-607-615-361-316-123 657-136-103-136-132-756-756-302-326 126-132-615-621 176-175-103-261-621-136-136-315-316-137-310-673-765-756-726-765-713-715-765-762-726-3_V.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 125-501-546-162 574-574-574-326-761-173-317-173-123-132-103-547-754-765-763-376-574-576-576-162-162-547-576-507_W.mp3)
  Segment 35: 53584ms ~ 55132ms (file: 153-765-102-657-106-631-326-613-167_W.mp3)
  Segment 36: 56132ms ~ 56708ms (file: 670-601-261-126-312-123-321-312-236-263-236-162-312-236-765-763-637-576-657-673-576-567-573-765-571-751-765-726-627-267-216-213-263 627-627-126-163-261-627-160 576-576-547-547-1_W.mp3)
  Segment 37: 57708ms ~ 58284ms (file: 576-756-567-675-567-576-576-675-756-576-657-567-675-675-756-576-567-675-627-623-265-567-657-576-567-576-567-576-756-576-756-567-576-576-567-576-576-652-576-567-572-576-576-567-5_W.mp3)
  Segment 38: 59284ms ~ 59860ms (file: 267-261-623-576-156-153-125-216-162-216-612-162-156-120-326-321-326-312-123-632-126-316-126-136-162-362-621-265-256-256-267-261-265-265-263-236-267-326-362-362-612-136-316-231-1_W.mp3)
  Segment 39: 60860ms ~ 61436ms (file: 106-136-613-162-326-236-203-320-203-623-216-106-301-102-136-163-301-521-103-201-570-571-523-302-206-230-260-320-230-320-137-235-326-302-302-302-362-627-236 267-623 103-136-316-1_W.mp3)
  Segment 40: 62436ms ~ 63012ms (file: 637-623-263-312-136-302-301-136-675-306-316 136-627-672-607-673-623-362-573-537-531-126-627-627-620-263-623-623-602-602-362-236-326-637-602-136-567-356-163-607-361-613-576-706 3_V.mp3)
  Segment 41: 64012ms ~ 64588ms (file: 132-657-765-765-653-675-671-526-260_W.mp3)
  Segment 42: 65588ms ~ 66164ms (file: 312-132-213-532-321-736-765-376-103-137-765-763-137-103-132-136-165-176-671-657-657-316-165-162-321-126-321-327-216-276-527-756-130-326-631-103-106-106-107-167-157-576-756-761-2_W.mp3)
  Segment 43: 67164ms ~ 67740ms (file: 136-132-612-261-623-213-123-321-312-623-362-321-326-265-263-236-263-231-136-103-231 143-736-652 627-627-621-361-362_W.mp3)
  Segment 44: 68740ms ~ 69316ms (file: 567-675-167-167-574-547-517-136-167-362-627-123-321-106-136-136-657-675-603-617-612-651-206-132-173-103-103-163-106-617-621-627-260-623-236-236-230-237-207-207-103-107-710-176-3_V.mp3)
  Segment 45: 70316ms ~ 70892ms (file: 136-167-167-167-617-106-651-167-715-176-671-170-163-713-516-751-173-761-160-632-167-163-603-163-167-316-103-312-130-736-136-106-516-162-163-130-136-650-673-567-172-136-123-367-6_V.mp3)
  Segment 46: 71892ms ~ 72468ms (file: 257-253 516-576-571-310-130-310-321-312-231-210-312-576-576-756-756-756-756-756_W.mp3)
  Segment 47: 73468ms ~ 74044ms (file: 103-103-103-103-163-512-753-561-132-160-607-162-162-106-106-316-137-103-176-103-103-103-652-621-236-310-615-163-765-617-321-126-106-576-632-160-602-130-672-610-163-106-167-617-6_V.mp3)
  Segment 48: 75044ms ~ 75620ms (file: 106-106-106-120-123-312-130-103-136-271-231-103-123-657-607-106-160-320-130-130-123-162-615-136-130-150-213-231-316-130-130-156-651-217-132-312-607-675-601-130-136-316-652-627-1_V.mp3)
  Segment 49: 76620ms ~ 77196ms (file: 632-612-306-613-613-627-672-167-162-162-162-167-213-621-672-627-627-302-120-103-136-706-176-273-715-765-706-176-137-162-267-127-132-321-326-123-137-136-762-672-102-310-312-756-7_W.mp3)
  Segment 50: 78196ms ~ 78772ms (file: 621-627-627-267-261-263-261-263-267-261-267-267-267-261-261-621-716-371-627-263-261-623-236-263-267-127-672-671-672-135-517-513-625-267-236-263-267-267-276-236-267-627-267-267-6_W.mp3)
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
> **Time**: 2026-04-03 07:05:54
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "어서 오세요. 몇 분이세요?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "어서", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "오세요.", "start_ms": 160, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "몇", "start_ms": 310, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "분이세요?", "start_ms": 380, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "두 명입니다. 조용한 자리 있나요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "두", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "명입니다.", "start_ms": 140, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "조용한", "start_ms": 300, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "자리", "start_ms": 440, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "있나요?", "start_ms": 510, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "안쪽 창가 자리가 비어 있습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "안쪽", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "창가", "start_ms": 165, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "자리가", "start_ms": 275, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "비어", "start_ms": 405, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 495, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "네, 거기가 좋겠네요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 45, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "거기가", "start_ms": 135, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "좋겠네요", "start_ms": 275, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "이쪽으로 안내해 드릴게요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "이쪽으로", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "안내해", "start_ms": 200, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "드릴게요", "start_ms": 320, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "메뉴판 좀 주시겠어요?",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "메뉴판", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 180, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "주시겠어요?", "start_ms": 370, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "네, 여기 있습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "여기", "start_ms": 130, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 240, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "주문하시겠어요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "주문", "start_ms": 30, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "하시겠어요?", "start_ms": 200, "dur_ms": 320, "phase_lr_deg": 0.0}
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
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 11,
"transcript": "여기서 가장 인기 있는 메뉴가 뭐예요?",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "여기서", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 170, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "인기", "start_ms": 290, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 420, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "메뉴가", "start_ms": 500, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "뭐예요?", "start_ms": 610, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "해산물 토마토 파스타와 스테이크가 인기가 많습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "해산물", "start_ms": 45, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "토마토", "start_ms": 185, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "파스타와", "start_ms": 335, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "스테이크가", "start_ms": 525, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "인기가", "start_ms": 735, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "많습니다", "start_ms": 855, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "그럼 파스타 하나랑 스테이크 하나 주세요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "파스타", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "하나랑", "start_ms": 280, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "스테이크", "start_ms": 400, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "하나", "start_ms": 550, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 650, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "스테이크 굽기는 어떻게 해 드릴까요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "스테이크", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "굽기는", "start_ms": 200, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 320, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "해", "start_ms": 440, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "드릴까요?", "start_ms": 490, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "미디엄으로 부탁드려요",
"speaker": "speaker_2",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "미디엄으로", "start_ms": 35, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "부탁", "start_ms": 245, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "드려요", "start_ms": 365, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "음료는 어떤 걸로 하시겠어요?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "음료는", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 160, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "걸로", "start_ms": 260, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "하시겠어요?", "start_ms": 350, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "오렌지 에이드 한 잔 주세요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "오렌지", "start_ms": 45, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "에이드", "start_ms": 175, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 295, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "잔", "start_ms": 365, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 445, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "네, 확인해 드릴게요. 파스타 하나, 스테이크 하나, 에이드 한 잔 맞으시죠?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "확인해", "start_ms": 140, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "드릴게요.", "start_ms": 300, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "파스타", "start_ms": 470, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "하나,", "start_ms": 610, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "스테이크", "start_ms": 710, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "하나,", "start_ms": 870, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "에이드", "start_ms": 970, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 1100, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "잔", "start_ms": 1170, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "맞으시죠?", "start_ms": 1250, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "네, 맞아요",
"speaker": "speaker_2",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 35, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "맞아요", "start_ms": 125, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "준비해 드릴게요. 잠시만 기다려 주세요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "준비해", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "드릴게요.", "start_ms": 200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "잠시만", "start_ms": 360, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "기다려", "start_ms": 500, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 620, "dur_ms": 110, "phase_lr_deg": 0.0}
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
"transcript": "식전 빵 나왔습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "식전", "start_ms": 35, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "빵", "start_ms": 175, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "나왔습니다", "start_ms": 295, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "한국의 식당에서는 메인 음식이 나오기 전에 가벼운 식전 빵이나 밑반찬을 제공하는 경우가 많습니다. 이는 손님이 음식을 기다리는 동안 입맛을 돋우기 위한 배려입니다. 특히 한식당의 경우 다양한 김치와 나물 반찬이 무료로 제공되며 리필도 가능한 것이 큰 특징입니다.",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "한국의", "start_ms": 120, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "식당에서는", "start_ms": 430, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "메인", "start_ms": 890, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "음식이", "start_ms": 1100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "나오기", "start_ms": 1460, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "전에", "start_ms": 1770, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "가벼운", "start_ms": 2030, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "식전", "start_ms": 2390, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "빵이나", "start_ms": 2650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "밑반찬을", "start_ms": 3010, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "제공하는", "start_ms": 3470, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "경우가", "start_ms": 3880, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "많습니다.", "start_ms": 4240, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이는", "start_ms": 4700, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "손님이", "start_ms": 4960, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "음식을", "start_ms": 5320, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "기다리는", "start_ms": 5680, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "동안", "start_ms": 6140, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "입맛을", "start_ms": 6400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "돋우기", "start_ms": 6760, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "위한", "start_ms": 7120, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "배려입니다.", "start_ms": 7380, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "특히", "start_ms": 8000, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "한식당의", "start_ms": 8260, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "경우", "start_ms": 8720, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "다양한", "start_ms": 8980, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "김치와", "start_ms": 9340, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "나물", "start_ms": 9750, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "반찬이", "start_ms": 10010, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "무료로", "start_ms": 10370, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "제공되며", "start_ms": 10730, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "리필도", "start_ms": 11190, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "가능한", "start_ms": 11550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "것이", "start_ms": 11910, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 12120, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "특징입니다.", "start_ms": 12280, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "빵이 따끈따끈해서 맛있네요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "빵이", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "따끈따끈해서", "start_ms": 170, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "맛있네요", "start_ms": 480, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "발사믹 소스에 찍어 드세요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "발사믹", "start_ms": 45, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "소스에", "start_ms": 205, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "찍어", "start_ms": 345, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "드세요", "start_ms": 465, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "풍미가 훨씬 좋아지네요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "풍미가", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "훨씬", "start_ms": 200, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "좋아지네요", "start_ms": 320, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "메인 메뉴도 금방 나올 거예요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "메인", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "메뉴도", "start_ms": 170, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "금방", "start_ms": 320, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "나올", "start_ms": 430, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 520, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "에이드 먼저 드릴까요?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "에이드", "start_ms": 45, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "먼저", "start_ms": 205, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "드릴까요?", "start_ms": 345, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "네, 시원하게 부탁드려요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 35, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "시원하게", "start_ms": 125, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "부탁", "start_ms": 315, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "드려요", "start_ms": 435, "dur_ms": 140, "phase_lr_deg": 0.0}
]
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
"transcript": "음식 나왔습니다. 파스타와 스테이크입니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "음식", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "나왔습니다.", "start_ms": 170, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "파스타와", "start_ms": 400, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "스테이크입니다", "start_ms": 590, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "우와, 비주얼이 예술이네요!",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "우와,", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "비주얼이", "start_ms": 180, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "예술이네요!", "start_ms": 370, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "스테이크 소스 부족하면 말씀해 주세요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "스테이크", "start_ms": 45, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "소스", "start_ms": 215, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "부족하면", "start_ms": 335, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "말씀해", "start_ms": 495, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 635, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "네, 감사합니다. 맛있게 먹을게요",
"speaker": "speaker_2",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "감사합니다.", "start_ms": 130, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "맛있게", "start_ms": 340, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "먹을게요", "start_ms": 490, "dur_ms": 180, "phase_lr_deg": 0.0}
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
"transcript": "파스타 면이 정말 쫄깃해요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "파스타", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "면이", "start_ms": 190, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 310, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "쫄깃해요", "start_ms": 420, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "스테이크도 아주 부드럽네요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "스테이크도", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 260, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "부드럽네요", "start_ms": 370, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "둘 다 정말 성공적이에요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "둘", "start_ms": 45, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 125, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 195, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "성공", "start_ms": 325, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "적이에요", "start_ms": 445, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "오늘 여기 오길 정말 잘했어요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "여기", "start_ms": 170, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "오길", "start_ms": 280, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 400, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "잘했어요", "start_ms": 510, "dur_ms": 160, "phase_lr_deg": 0.0}
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
"transcript": "배가 정말 부르네요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "배가", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 165, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "부르네요", "start_ms": 295, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "이제 계산하러 갈까요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "계산하러", "start_ms": 160, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "갈까요?", "start_ms": 320, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 1,
"transcript": "오늘 기분은 어때요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "기분은", "start_ms": 160, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "어때요?", "start_ms": 310, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "날씨가 좋아서 아주 좋아요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "날씨가", "start_ms": 50, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "좋아서", "start_ms": 220, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 350, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "좋아요", "start_ms": 440, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "무슨 좋은 계획이라도 있나요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "무슨", "start_ms": 35, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 145, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "계획이라도", "start_ms": 245, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "있나요?", "start_ms": 435, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "친구들과 한강에 가기로 했어요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "친구들과", "start_ms": 45, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "한강에", "start_ms": 235, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "가기로", "start_ms": 375, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "했어요", "start_ms": 495, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "재미있겠네요!",
"speaker": "speaker_1",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "재미", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있겠네요!", "start_ms": 190, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "한강에서 자전거 탈 거예요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "한강에서", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "자전거", "start_ms": 230, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "탈", "start_ms": 390, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 480, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "바람이 불어서 시원하겠어요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "바람이", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "불어서", "start_ms": 200, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "시원", "start_ms": 340, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "하겠어요", "start_ms": 450, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "네, 벌써 기대돼요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 35, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "벌써", "start_ms": 125, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "기대돼요", "start_ms": 285, "dur_ms": 260, "phase_lr_deg": 0.0}
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
"transcript": "한강에 사람이 많을까요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "한강에", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "사람이", "start_ms": 200, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "많을까요?", "start_ms": 350, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "아마 주말이라 많을 것 같아요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "아마", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "주말이라", "start_ms": 160, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "많을", "start_ms": 330, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 440, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 500, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "주차하기 힘들겠네요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "주차", "start_ms": 45, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "하기", "start_ms": 185, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "힘들", "start_ms": 305, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "겠네요", "start_ms": 415, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "그래서 대중교통을 이용하려고요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "그래서", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "대중교통을", "start_ms": 190, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "이용", "start_ms": 420, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "하려고요", "start_ms": 510, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "현명한 생각이네요!",
"speaker": "speaker_1",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "현명한", "start_ms": 35, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "생각이네요!", "start_ms": 245, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 16,
"transcript": "가서 맛있는 것도 먹나요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "가서", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "맛있는", "start_ms": 160, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "것도", "start_ms": 310, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "먹나요?", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "치킨이랑 라면 먹기로 했어요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "치킨이랑", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "라면", "start_ms": 260, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "먹기로", "start_ms": 400, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "했어요", "start_ms": 520, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "한강 라면이 정말 꿀맛이죠",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "한강", "start_ms": 45, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "라면이", "start_ms": 175, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 335, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "꿀맛", "start_ms": 445, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이죠", "start_ms": 555, "dur_ms": 20, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "맞아요, 밖에서 먹으면 더 맛있어요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "맞아요,", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "밖에서", "start_ms": 190, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "먹으면", "start_ms": 330, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 450, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "맛있어요", "start_ms": 510, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "즐거운 시간 보내고 오세요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "즐거운", "start_ms": 35, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "시간", "start_ms": 205, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "보내고", "start_ms": 325, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "오세요", "start_ms": 455, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "네, 다녀와서 얘기해 줄게요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "다녀와서", "start_ms": 130, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "얘기해", "start_ms": 320, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "줄게요", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "조심히 잘 다녀오세요!",
"speaker": "speaker_2",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "조심히", "start_ms": 45, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 265, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "다녀오세요!", "start_ms": 345, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "요즘 한국 사람들은 한강에서 피크닉을 즐기는 걸 무척 좋아합니다. 텐트나 돗자리를 펴놓고 친구, 연인과 함께 맛있는 배달 음식을 시켜 먹는 문화가 아주 발달해 있어요. 특히 해 질 녘 노을을 보며 먹는 치킨과 맥주는 정말 일품입니다. 기회가 되시면 꼭 한번 경험해 보세요.",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "요즘", "start_ms": 120, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 380, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "사람들은", "start_ms": 640, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "한강에서", "start_ms": 1050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "피크닉을", "start_ms": 1510, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "즐기는", "start_ms": 1970, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "걸", "start_ms": 2330, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "무척", "start_ms": 2440, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "좋아합니다.", "start_ms": 2700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "텐트나", "start_ms": 3300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "돗자리를", "start_ms": 3660, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "펴놓고", "start_ms": 4120, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "친구,", "start_ms": 4480, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "연인과", "start_ms": 4740, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "함께", "start_ms": 5100, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "맛있는", "start_ms": 5360, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "배달", "start_ms": 5720, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "음식을", "start_ms": 6030, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "시켜", "start_ms": 6390, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "먹는", "start_ms": 6650, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "문화가", "start_ms": 6910, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 7270, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "발달해", "start_ms": 7530, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있어요.", "start_ms": 7890, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "특히", "start_ms": 8400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "해", "start_ms": 8710, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "질", "start_ms": 8820, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "녘", "start_ms": 8930, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "노을을", "start_ms": 9040, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "보며", "start_ms": 9450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "먹는", "start_ms": 9760, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "치킨과", "start_ms": 10020, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "맥주는", "start_ms": 10430, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 10840, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "일품입니다.", "start_ms": 11100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "기회가", "start_ms": 11800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "되시면", "start_ms": 12160, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "꼭", "start_ms": 12520, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "한번", "start_ms": 12680, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "경험해", "start_ms": 12940, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "보세요.", "start_ms": 13300, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "한강 가고 싶네요!",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "한강", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "가고", "start_ms": 230, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "싶네요!", "start_ms": 360, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "같이 가요!",
"speaker": "speaker_1",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "같이", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가요!", "start_ms": 190, "dur_ms": 380, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "좋은 생각이에요!",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "생각", "start_ms": 200, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "이에요!", "start_ms": 350, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "주말에 시간 괜찮나요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "주말에", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "시간", "start_ms": 240, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "괜찮나요?", "start_ms": 360, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "토요일 오후가 좋아요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "토요일", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "오후가", "start_ms": 230, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "좋아요", "start_ms": 380, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "그럼 그때 만나요!",
"speaker": "speaker_1",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "그때", "start_ms": 165, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "만나요!", "start_ms": 285, "dur_ms": 290, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "어디서 만날까요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "어디서", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "만날까요?", "start_ms": 210, "dur_ms": 360, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "여의나루역 앞에서 봐요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "여의나루역", "start_ms": 40, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "앞에서", "start_ms": 300, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "봐요", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0}
]
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
"transcript": "알겠어요, 그럼 토요일에 봐요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "알겠어요,", "start_ms": 45, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "그럼", "start_ms": 215, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "토요일에", "start_ms": 335, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "봐요", "start_ms": 495, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "네, 안녕히 가세요",
"speaker": "speaker_1",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 130, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 350, "dur_ms": 220, "phase_lr_deg": 0.0}
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
"transcript": "안녕히 계세요!",
"speaker": "speaker_2",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 35, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "계세요!", "start_ms": 265, "dur_ms": 310, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "다음에 또 봐요",
"speaker": "speaker_1",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 230, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "봐요", "start_ms": 320, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "네, 연락드릴게요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 45, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "연락", "start_ms": 135, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "드릴게요", "start_ms": 255, "dur_ms": 320, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "즐거운 주말 되세요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "즐거운", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "주말", "start_ms": 230, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "되세요", "start_ms": 350, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "민수 씨도요!",
"speaker": "speaker_2",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "민수", "start_ms": 35, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "씨도요!", "start_ms": 185, "dur_ms": 390, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "고마워요!",
"speaker": "speaker_1",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "고마", "start_ms": 30, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "워요!", "start_ms": 220, "dur_ms": 320, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "혹시 캠핑 좋아하세요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "혹시", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "캠핑", "start_ms": 165, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "좋아하세요?", "start_ms": 305, "dur_ms": 270, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "네, 가끔 가요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "가끔", "start_ms": 130, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "가요", "start_ms": 260, "dur_ms": 310, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "어디로 주로 가시나요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "어디로", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "주로", "start_ms": 200, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "가시나요?", "start_ms": 320, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "강원도 쪽으로 자주 가요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "강원도", "start_ms": 45, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "쪽으로", "start_ms": 235, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "자주", "start_ms": 355, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "가요", "start_ms": 445, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "강원도는 바다도 있고 산도 있어서 정말 좋죠. 캠핑하기 딱 좋은 곳 같아요. 장비는 다 챙겨 다니시나요?",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "강원도는", "start_ms": 60, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "바다도", "start_ms": 420, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "있고", "start_ms": 680, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "산도", "start_ms": 840, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있어서", "start_ms": 1000, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 1210, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "좋죠.", "start_ms": 1370, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "캠핑", "start_ms": 1700, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "하기", "start_ms": 1910, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "딱", "start_ms": 2070, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 2180, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "곳", "start_ms": 2340, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "같아요.", "start_ms": 2430, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "장비는", "start_ms": 2800, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 3060, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "챙겨", "start_ms": 3150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "다니", "start_ms": 3310, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "시나요?", "start_ms": 3470, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "네, 차에 항상 실려 있어요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "차에", "start_ms": 130, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "항상", "start_ms": 250, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "실려", "start_ms": 400, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 520, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "와, 부러워요! 저도 언젠가...",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "와,", "start_ms": 55, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "부러워요!", "start_ms": 165, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "저도", "start_ms": 375, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "언젠가...", "start_ms": 465, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "같이 가요, 제가 다 알려드릴게요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "같이", "start_ms": 45, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "가요,", "start_ms": 175, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 295, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 385, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "알려", "start_ms": 435, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "드릴게요", "start_ms": 535, "dur_ms": 40, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "정말요? 기대할게요!",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "정말요?", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "기대", "start_ms": 230, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "할게요!", "start_ms": 350, "dur_ms": 220, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 07:05:54*
