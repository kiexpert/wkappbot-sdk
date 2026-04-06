# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=08:31:36  uptime=25942s  mem=1842MB  system_processes=450"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_083127.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 621-261-267-261-621-621-261-612-261-261-612-261-162-621-126-261-621-621-612-612-162-261-612-621-625-263-362-362-612-362-362-632-623-623-362-623-263-632-261-213-326-521-521-215-2_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 231-237-213-217-216 567-172-715-765 547 573-572-562-167-617-176-671-576-261-567-152-153-106-576-617-230-265-620-260-620-726-203-320-310-130-301-106-102-106-167-132-261-213-327-3_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 312-301-327-302-623-261-102-132-137-201 163-167-617-612-615-562-625-637-267-632-651-673-632-132-316-103-162-710-637-162-102-610-160-136-621-631-632-326-632-236-623-236-267-267-2_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 132-132-132-175-137-134-103-312-301-312 263-263-126-631-326-312-536-361-621-263-261-106-102-130-621-126-132-102-132-172-765-312-613-376-163-261-623-263-201-320-320-567-165-306-3_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 162-126-132-103-312-103-137-132-163-230-132-136-123-162-162-163-106-162-126-312-312-320-235-570-753-752-750-137-312-312-351-543-302-320-320-203-206-540-507-523-235-302-132-163-7_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 163-106-510-510 321-310-301-756 236-302-302-136-163-132-613-231-201-230-302-302-310-301-310-301-310-130-130-137-103-301-361-341-641-413-321-312-301-132-130-103-103-103-103-103-1_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 103-564-612-627-175-362-130-123-321-321-320-230-236-621-162-126-136-135-103-615-670-163-134-106-154-105-162-312-307 132-163-132-175-107-176-172-132-162-623-675-627-623-632-326-3_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 362-263-264-652-572-527-273-725-126-627-265-256-315-317-325-365-541-153-137-137-317-327-275-275-273-327-751-571-751-573-157-125-251-263-257-325-725-276-672-762-674-627-672-762-7_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 624-257-261-123-362-261-276-517-367-361-713-236-314-251-642 326-613 231-317-736_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 267-267-261-623-623-620 321-132-102-103-675-310-136-126-130-236-312-103-103-136-103-130-132-132-657-765-765-762-672-574-576-576-573-567-576-301-367-765-102-765-571-170-170-137-1_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 120-132-106-126-126-631-162-216-123-213-136-762-627-723-123-621-163-136-260-106-216-576-657_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 627-621-213-261-231-512-251-612-627-627-103-267-627-362-236-623-312-263-672-627-672-672-627-276-236-236-541-524-312-132-540-254-236-574-567-576-526-657-267-627-576-526-672-564-5_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 574-537 617-317 531-516 532-325-561-361 527-265 257-267-265-275-265-267-623-623-236-263-326-253-265-260-265-265-625-265-572-527-625-265-632-631-631 257-257-237-627-362-532-321-7_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 675-657-675-675-675-765-672-206-206-602-612-630-360-360-361-357-562-652-372-653-263-265-267-567-576-576-673-657-637-756-567-576-263 625-672-675-176-671-167-671-567-657 267-136-6_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 620-610-102-312-321-302-320-302-621 106-672-612-160-610-130-612-671-760-136-657-615-671-672-617-612-612-620-623-260-203-106-106-612-106-162-651-716-163-126-103-301-231-102-103-1_V.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 136-213-310-123-136-130-621-627-620-162-601-103-132_V.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 127-157-216-127-601-613-167-132-123-603-126-165-102-103-316-106-160-156-163-137-613-125-126-162-106-617-136-107-602-136-675_V.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 754-756-756-756 162-625-276-627-623-657-675-657-576-762-762-326-316-163 165-163-165-106-136-136-132-103-136-317-103-162-165-620-156-135-130-163-316-160-163-103-165-136-130-163-1_V.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 321 632-361 675-312-362 617-613-673-617-613 751-751-576-756-756-756-756-756-576-750 756-576-756-752_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 136-132 621-312 163-675-760-160-631-621-675-607-620 136-317 526-256_V.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 163-106-103-136-103-312-135-203-132-126-130-103-310-123-103-136-610-316-601-107-163-607-651-761-206-206-203-106-132-312-132-602-673-632-103-103-301-103-103-603-367-601-617-617-1_V.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 621-132 261-316-127-576-752-576-576 132-761-276-312-321-231-562-263-261-623-672-267-623-627-265-265-627 625-265-625_W.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 267-613-632-657-627-627-672-675-657-672-627-627-627-627-627-265-256-265-261-265-526-526-256-675-672-627-627-672-564-625-625-627-263-657-675-657-675-657-567-672-627-267-263-162-1_W.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 162-123-103-123-132-103-312-103-312-320-321-236-261-126-231-123-132-103-132-216-123-120-216-103-102_V.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 263-263-263-236-267-236-267-261-267-263-267-267-267-267-236-267-236-236-237-236-237-326-623-236-267-263-267-267-263-267-267-267-267-263-267-263-261-267-263-623-625-267-526-257-2_W.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 756-751-765-765-765-672-765-765-756-673-763-765-760-762-762-672-623-762-216-762-736-651-135-167-103-103-627-617-671-610-167-603-567-612-567-106-136-263-632-103-106-136-261-136-1_W.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 321-162-621-126-162-136 132 672-632-132-132-321-312-324-316-316-132-132-132-164-167-126-312-312-132-276-127-261-321-301-162-621-106-132-132-123-610-162-130-135-315-103-165-176-1_V.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 163-130-136-713-216-162-712-613-106-167-167-176-612-106-236-123-267-260-625-612 132-675-167-130-130-103-125-106-610-165-106-160-136-130-136-216-103-316-326-263-230-326-302 675-7_V.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 574-574-265-265-276-267-267-625-267-256-265-267-265-256-265-267-625-625-265-625-623-623-567-574-574-574-576-267-326-263-623-263-362-621-623-576-567-627-263-623-621-623-623-627-2_W.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 216-526-625-652-265-516-265-261-236-326-326-326-321-236-230-321-321-321-312-321-231-213-325-231-321-312-321-321-231-562-523-651-165-562-561-256-256-253-235-235-235-315-321-325-1_W.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 136-136-136-163-362 136-610-621-612-673-761-765-675-617-675-176 756-765-567-576-756-576-576-574-547-567-576-574-576-657-576-657-652-625-136-103-576-674-675 756-756-625-632-672 7_W.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 130-160-163-162-102-130-106-136-103-106-106-156-632-162-326-261-162-257-567-675-217-310-102-123-261-103-621-625-675-576-162-130-652-361-632-132-316-671-136-162-132-672-567-756-7_V.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 627-657-627-625-576-567-562-265-263-632-631-632-623-632-213-261-312-316-263-236-263-361-623-362-236-653-156-136-567-672-672-672-617-267-261-163-631-163-132-623-132-103-123-130-3_W.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 564-546-106-176-167-167-106-103-706-670-607-756-167-163-176-162-106-162 102-106-263-261-236-627-672-316-536-620-260-126-167-172-176-136-260-260-260-260-263-310-610-657-567-675-1_V.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 675-657-675-652-173-103-753-751-750-765-765-176-670-675-312-120-312-103-267-132-316-136-123 231-526-276-320-132-123-275-762-672-627-237-237-327-106-175-167-162-136-163-162-176-1_V.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 136-103-652-315-562-167-631-130-165-316-326-361-103-103-132-136-716-123-132-136-163-103-237-123-103-106-157-103-261-163-136-103-130-253-657-325-612-316-162-167-610-162-617-271-2_V.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 132-103-163-413-614-132-174 617-672-620-362-360-306-361-321-136-132-103-136-146-123-130 765-765-267-267 631-173-317-132-306-326-203-102-365-236-231-263-126-132-312-320-326-631-3_V.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 672-627 602-670-762-612-136-635-671-670-607 216-132-132_W.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 326-563-213-132-103-103-231-671-106-263-102-602-621-621-160-621-623-620-612-162-760-765-617-672-672-516-106-175-617-103-160-650-167-167-162-561-130-167-136-136-316-130-132-167-1_W.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 503-215-632-610-621-621-216-250-260-203-302-263-326-567 765-675-765-627-627-672-623 670-670-627-602 167-751-627-321-302-321-231-321-231-276-672-371-163-176-371-327-327-632-106-7_W.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 765-765-765-765-756-675-765-576-756-756-657-715-765-627-267-765-675-607-260-260-236-362-130-160-610-601-167-617-716-653-672-627-612-106-167-102-206-263-620-670-657-675-361-316 6_W.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 765-762-236-127-762-726-172-123 753 127-763-762-217-123_W.mp3)
  Segment 43: 66192ms ~ 66768ms (file: 132-310-316-310-136-163-162-612-162-623-612-316-312-132-130-602-613-167-617-610-672-617-361-316-130-576-617-671-163-613-126-132-136-672-675-176-671-612-632-632-163-316-631-136-1_W.mp3)
  Segment 44: 67768ms ~ 68344ms (file: 130-326-126-156-602-672-607-617-261-263-631-765-163-173-615-163-136-136-136-106-136 652-136-127 106-167-136-106-103-176-576-103-130-130-103-612-130-167-132-136-123-120-176-132-1_V.mp3)
  Segment 45: 69344ms ~ 69920ms (file: 316-136-165-136-576-316-130-315-203-275-752-276-276-120-107-316-321-230-203-260-267-253-276-237-236-160-361-361-310-310-361-316-362-263-263-320-236-263-326-236-320-263-360-632-2_V.mp3)
  Segment 46: 70920ms ~ 71496ms (file: 375-763-372-621-126-231-576-567-765-675-275-726-652-562-572-532-132-310-613-362-516-576-576-527-651-576-651-657-675-356-756-756-567-657-635-567-657-567-635-603-160-172-613-635-5_W.mp3)
  Segment 47: 72496ms ~ 73072ms (file: 230-523-106-236-265-265-213-130-310-301-312-230-203-560-620-620-260-206-263-263-261-236-103-210-320-103-310-312-310-103-103-136-106-203-206-207-206-230-130-301-310-362-632-671-1_V.mp3)
  Segment 48: 74072ms ~ 74648ms (file: 657-673-607-123-102-126-367-312-326-601-236-362 167-123-625-625-625-263-623-236-263-267-263_W.mp3)
  Segment 49: 75648ms ~ 76224ms (file: 132 627-312-612-231-123-261-216 610-107-716-615-163-621-137-716-317-127-162-276-176-162-126-167-137-106-103-167-106-137-163-106-103-103-127-627-126-712-236-576-617-567-516_W.mp3)
  Segment 50: 77224ms ~ 77800ms (file: 317-362-631-567-576-651-163-361-263-621-312-316-671-167-612-167-361-316-321-672-612-632-231-213-132-123-132-132-132-316-612-162-216-321 576-576-507-206-361-316-631-576-576-523-7_W.mp3)
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
> **Time**: 2026-04-03 08:32:46
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 2,
"transcript": "주말에 뭐 할 거예요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "주말에", "start_ms": 60, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 220, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 310, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "거예요?", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "친구랑 등산 가기로 했어요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "친구랑", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "등산", "start_ms": 240, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "가기로", "start_ms": 370, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "했어요", "start_ms": 480, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "어느 산으로 가세요?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "어느", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "산으로", "start_ms": 170, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가세요?", "start_ms": 330, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "북한산에 가려고요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "북한산에", "start_ms": 45, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "가려고요", "start_ms": 260, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "와 정말 멋지겠네요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "와", "start_ms": 55, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 120, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "멋지겠네요", "start_ms": 270, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "정상까지 가실 건가요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "정상까지", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "가실", "start_ms": 230, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "건가요?", "start_ms": 340, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 9,
"transcript": "네 백운대까지 도전해 보려고요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 70, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "백운대까지", "start_ms": 140, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "도전해", "start_ms": 330, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "보려고요", "start_ms": 460, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "대단하세요 조심해서 다녀오세요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "대단하세요", "start_ms": 35, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "조심해서", "start_ms": 220, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "다녀오세요", "start_ms": 370, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "날씨가 맑았으면 좋겠네요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "날씨가", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "맑았으면", "start_ms": 180, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "좋겠네요", "start_ms": 340, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "준비물은 다 챙기셨나요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "준비물은", "start_ms": 45, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 230, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "챙기셨나요?", "start_ms": 300, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 14,
"transcript": "네 등산화랑 물도 챙겼어요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 65, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "등산화랑", "start_ms": 130, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "물도", "start_ms": 320, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "챙겼어요", "start_ms": 410, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "도시락도 싸가실 거예요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "도시락도", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "싸가실", "start_ms": 230, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "거예요?", "start_ms": 360, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "아니요 내려와서 먹으려고요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "아니요", "start_ms": 30, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "내려와서", "start_ms": 160, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "먹으려고요", "start_ms": 350, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "산 아래 맛집이 많거든요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "산", "start_ms": 50, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "아래", "start_ms": 120, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "맛집이", "start_ms": 230, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "많거든요", "start_ms": 360, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "부럽네요 저도 가고 싶어요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "부럽네요", "start_ms": 45, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "저도", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "가고", "start_ms": 310, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "싶어요", "start_ms": 420, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 20,
"transcript": "다음에 같이 가요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 35, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 190, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "가요", "start_ms": 300, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "좋은 생각이에요 꼭 불러주세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "생각이에요", "start_ms": 150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "꼭", "start_ms": 310, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "불러주세요", "start_ms": 380, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 23,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 24,
"transcript": "Have a safe trip!",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "Have", "start_ms": 60, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 150, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "safe", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "trip!", "start_ms": 310, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 26,
"transcript": "Thank you, see you next week.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Thank", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "you,", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "see", "start_ms": 250, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 320, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "next", "start_ms": 390, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "week.", "start_ms": 480, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "요즘 건강 관리는 어떻게 하세요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "요즘", "start_ms": 55, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "건강", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "관리는", "start_ms": 250, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 380, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "하세요?", "start_ms": 490, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "매일 아침 조깅을 해요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "매일", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "아침", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "조깅을", "start_ms": 280, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "해요", "start_ms": 430, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 30,
"transcript": "정말 부지런하시네요 얼마나 하세요?",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 70, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "부지런하시네요", "start_ms": 160, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "얼마나", "start_ms": 420, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "하세요?", "start_ms": 530, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 32,
"transcript": "삼십 분 정도 가볍게 뛰어요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "삼십", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "분", "start_ms": 160, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "정도", "start_ms": 230, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "가볍게", "start_ms": 320, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "뛰어요", "start_ms": 450, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 34,
"transcript": "꾸준히 하는 게 제일 중요하죠",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "꾸준히", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 190, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 280, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "제일", "start_ms": 330, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "중요하죠", "start_ms": 440, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "맞아요 몸이 가벼워지는 느낌이에요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "맞아요", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "몸이", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "가벼워지는", "start_ms": 290, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "느낌이에요", "start_ms": 450, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "저도 내일부터 시작해 볼까요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "저도", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "내일부터", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "시작해", "start_ms": 280, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "볼까요?", "start_ms": 410, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "적극 추천합니다 같이 해요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "적극", "start_ms": 35, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "추천합니다", "start_ms": 140, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 330, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "해요", "start_ms": 440, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 39,
"transcript": "내일 아침에 깨워주실 수 있나요?",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "내일", "start_ms": 60, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "아침에", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "깨워주실", "start_ms": 280, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 430, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "있나요?", "start_ms": 480, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "그럼요 일곱 시에 전화할게요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "그럼요", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "일곱", "start_ms": 170, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "시에", "start_ms": 280, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "전화할게요", "start_ms": 370, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "벌써부터 건강해지는 기분이네요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "벌써부터", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "건강해지는", "start_ms": 240, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "기분이네요", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 43,
"transcript": "일찍 자야겠어요 내일 봐요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "일찍", "start_ms": 35, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "자야겠어요", "start_ms": 140, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "내일", "start_ms": 300, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "봐요", "start_ms": 410, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "잘 자요 안녕",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "잘", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "자요", "start_ms": 110, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "안녕", "start_ms": 220, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "그 책 다 읽으셨어요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "그", "start_ms": 40, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "책", "start_ms": 100, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 170, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "읽으셨어요?", "start_ms": 260, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 47,
"transcript": "아직 조금 남았어요 내용이 깊네요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "아직", "start_ms": 55, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 160, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "남았어요", "start_ms": 270, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "내용이", "start_ms": 430, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "깊네요", "start_ms": 520, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 49,
"transcript": "다 읽고 저한테 빌려주실 수 있나요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "다", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "읽고", "start_ms": 110, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "저한테", "start_ms": 220, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "빌려주실", "start_ms": 350, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 500, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "있나요?", "start_ms": 550, "dur_ms": 20, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "그럼요 다 읽고 꼭 가져올게요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "그럼요", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 190, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "읽고", "start_ms": 260, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "꼭", "start_ms": 350, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "가져올게요", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 08:32:46*
