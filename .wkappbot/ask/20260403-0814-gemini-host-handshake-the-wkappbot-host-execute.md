# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=08:13:27  uptime=24852s  mem=1813MB  system_processes=445"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_081317.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 267-261-261-623-263-623-263-362-632-236-362-632-361-326-623-326-362-627-362-236-326-362-361-136-326-362-231-326-361-263-316-326-236-326-326-312-326-316-321-326-231-321-573-352-3_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 136-167-162-712-173-136-106-671-167-126-620-623-620-617 136-163-136-106-163-236-612-602-102-167-106-163-103-136-103-163-160-106-106-105-613-632-162-716-103-132-102_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 316-631-156-651-135-615-615-365-651-651-615-651-657-652-635-165-562-536-651-675-651-561-653-651-561-156-315-653-653-615-156-615-156-615-165-635-651-651-156-615-651-615-651-631-5_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 503-543-301-310-736-713-137-163-137-163-127-167-106-715-163-165-126-127-627-561-571-526-106-261-127 103-127-167-127-620-260-206-310-302-325-521-615-623-601-312-301-132-627-621-6_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 750-765-671-162-162-157-103-130-136-317-361-367-637-543-574-541-326-230-236-230-230-236-302-235-523-503-601-165-657-167-361-176-367-316-163-176-617-217-126-136-762-752-756-762-7_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 235-350-356-534-315-357-527-560-653-516-135-125-156-154-153-351-153-351-153-165-154-165-615-657-574-657-571-165-315-135-154-150-165-517-357-365-357-351-315-507-350-351-356-357 7_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 613-612-607-106-615-132-123-137-103-103-316-136-132-136-132-306-205-205-257-567-615-576-576-123-132-326-312-301-301-310-317-137-132-312-103-670-136-307-736-371-132-216-621-620-2_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 317-132-103-103-130-106-612-362-136-316-130-132-310-103-136-132-316-126-312-136-126-601-162-136-106-103_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 563-657-516-561-567-567-576-657-567-537-675-356-657-516-356-567-537-537-573-376-657-657-573-513-572-357-356-351-531-513-513-561-563-536-537-657-635-567-536-563-536-563-653-567-5_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 356-362-326-231-537-362-312-312-325-132-326-235-532-532-573-527-302-753-753-302-312-137-317-175-132-123-132-321-312-203-123-270-123-312-327-357-351-537-302-103-123-231-302-321-7_V.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 267-206 523-321-302-237-203-302-756-123-230-123-756-237-327-237-301-203-321-302-237-327-310-103-132-627-267-231-216-620-627-263-132-326-326-231-721-617-132-102-123-231-126-102-1_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 301-310-310-310-103-132-132-231-163-301-301-312-301-130-130-103-103-130-103-103-103 675-672-672-302-123-302-132-261 627-627-623-632-627-627-762-321-103-103-103-106-163-136-103-1_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 123-123-125-301-102-123-617-136-102-106-132-120-132-132-601-320-310-610-106-106-316-312-132-126-302-316-672-657-627-263-261-132-106-163-162-563-163-301-301-362-312-302-672-620-2_V.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 103-136-321-136-136-263-316-126 132-106-130-106-167-160-617-317-617-103-126-123-671-627-713-167-132-123-130-206-231-326-312-263 165-756-756-136-721-167-176-620-672 103-176-103-1_V.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 705-715-715-753-217-102-210-162-106-102-136-123-310-130-157-756-263-302-302-265-230-213-213-362-670-213-261-103-123-162-106-167-106-132-130-321-136-163-106-321-301-231-132-162-6_V.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 213-362-362-623-316-361-637-362-263-267-627-127-136-673-657-675-671-670-206-206-312-136-621-316-216-230-201-102-213-602-620-260-213-162-574-574-576-456-546-765-671-671-167-657-1_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 253-326-231-312-321-321-325-326-312-231-231-236-263-263-237-217-216-235-213-235-123-215-215-216-123-253-216-326-215-523-251-235-235-253-215-235-625-263-256-326-362-362-362-326-3_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 132-130-106-126-621-216-612-103-132-261-206-206-263-102-136-263-216-627-216-231-576-657-132-325-235-160-675-765-651-516-162-106-106-652-621-627-263-326-567-657-132-103-602-623-3_V.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 106-601-263-321-302-302-623-326-320-632-310-163-320-306-365-316-672-367-576-756-752-602-620-310-123-213-236-207-203-203-127-312-316-126-703-270-172-103-715-765-706-237-106-106-1_V.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 627-601-617-762-162-263-103-136-651-163-103-312-536-761-632-367-103-132-613-136-763-362-672-163-106-136-130-136-106-102-617-361-627-260-267-106-617-103-136-106-162-106-613-136-6_V.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 302-367-657-126-132-135-136-126-126-516-326-567-576-765-756-567-765-576-657-567-567-576-567-576-756-561-567-657-657-675-675-675-675 576-576-576-567-567-576-675-657-576-567-567-5_W.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 267-627-627-627-672-672-627-276-261-675-726-627-627-621-672-762-672-671-620-762-672-627-672-627-637-267-261-236-726-267-362-163-310-675-765-675-675-726-267-267-326-213-231-263-3_W.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 103-675-637-675-267-132 163-312-165-673-671-162-136-106-132-125-317-157-130-310-136-136-657-605-617-136-157-105-167-701-567-263-265-652-657-652_W.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 153-765-657-567-513-567-651-651-567-657-675-657-156-651-671-576-756-573-573-576-567-567-652-653-567-657-561-567-653-576-657-635-537-573-657-762-537-372-261-132-156-576-576-567-5_W.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 235-136-261 236-632-162-162-167-163-162-362-263-657-675-132-123-512-732-670-760-263-236 625-136 123-312-132-236-120-327-237-263-236-675-672-623-635-652-675-657-675-162-263-236-6_W.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 751-130-367-132-310-301-675-167-176-167-617-167-103-176-731-671-617-761-317-132-312-203-132-301-312-132-132-103-153-312-135-312-312-315-315-106-132-167-327-123-126-126-576-574-7_W.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 362-365-326-267-526-256-675-567-576-572-265-265-563-512-561-516-156-157-751-265-253-235-132-125-517-165-531-532-517-537-576-215-315-126-236-326-235-376-317-371-321-132-135-362-1_W.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 637-162-362-136 576-756-576-675-675-165-615-106-106-516-657-652-316-617-615-106-617-163-176-167-716-736-705-750-713-173-165-675-671-602-627-612-613-672-612-627-602-672-675-657-6_W.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 143-164-107-130-130-130-103-130-310-130-137-136-372-617-631-326-317-361-637-731-716-726-627-635-163-621-123-126-106-601-106-136-672-631-136-631-106-130-103-160-103-126-102-126-1_W.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 257-257-253-256-532-534-513-357-537-541-514-351-351-352-315-315-315-315-531-315-352-523-532-510-510-561-572-516-526-536-531-527-572-576-615-513-561-517-157-514-510-514-514-571-5_W.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 162-310-163-106-610-617-613-167-160-162-236-623-263-263-621-137-103-107-173-217-127-127-501-152-675-627-610-670-602-267-206-103-126-103-163-123-106-124-621-267-107-671-126-325-3_V.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 615-126-106-613-126-103-163-765-167-261-615-126-260-632-620-263-327-216-126-132-621-361-136-103-132-613-132-163-136-316-106-123-216-671-260-130-103-106-163-103-167-123-524-215-1_V.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 670-763-276-267-362-106-136-637-763-736-765-763-675-763-675-765-675-765-675-307-376-360-370-376-765-756-756-765-706-675-607-602-612-163-610-371-367-367-361-360-316-637-275-362-6_W.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 263-310-763-265-623-601-627-627-632-623-574-570-176-130-310-756-756-712-627-763-267-126-236-571-572-574-576-546-576-574-547-574-261-260-526-560-203-302-316-231-260-263-260-206-6_W.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 103-312-123-102-574-756-756-756-657-675-627-301-203-230-103-312-316-130-301-510-310-130-310-350-301-103-132-602-602-361-316-310-301-657-563-615-613-675-675-167-103-132-302-201-3_W.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 672-136-136-376-316-103-716-713-372-672-163_W.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 136-623-675-760-765-762 107-126-267-615-167-621-612-671-162-621-256-230-237-230-231-231-760_V.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 136-130-130-613-312-657-657-753-576-567-765-752-632-625-752-672-756-210-261-163-126-712-702-316-137-756-135-756-675-127-567-136-126-130-157-652-576-165-576-756-107-670-617-671-7_W.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 162-126-103-136-231-123 621 134 103-127 132-135_V.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 162-312-312-310-130-310-175-137-321-130-126-132-312-206-236-623-362-620-312-320-320-361-130-413-132-103-130 132-316-310-316-102-103-103-102-123-103-130-106-160-106-173-130-103-2_V.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 367-657-216-271-267-310-130-321-130-130-136-302-203-302-327-321-162-620-631-301-123-312-106-130-627-206-130-613-106-106-162-103-136-103-132-162-617-610-601-765-657-675-671-627-2_V.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 163-130-103-103-103-163-126-167-163-136-103-163-651-136-103-673-176 106-176-136-106-615-103-152-167-127-103-176-173-617-176-761-106-156-362-167-163-126-162-617-136-173-263-175-2_V.mp3)
  Segment 43: 66192ms ~ 66768ms (file: 365-365-362-316-165-165-653-156-351-513-315-163-316-361-167-176-612-637-362-637-726-376-567-567-567-563-657-653-657-165-165-615-516-165-265-216-165-615-165-635-365-365-635-365-5_W.mp3)
  Segment 44: 67768ms ~ 68344ms (file: 167-126-126-316-126-162-613-631-361-361-136-136-132-324-614-126-132-167-312-126-613-165-136-132-163-710-316-327-513-316-216-162 321-123-672-236-145-316-175-126-675-756-617-126-1_V.mp3)
  Segment 45: 69344ms ~ 69920ms (file: 657-102-716-120-217-521-213-126-765-216-627-276-725-271-263-613-621-163-126-361-371-375-753-326-203-207-216-276-162-261-723-276-762-160-162-170-135-306-316-132-123-263-172-527-5_W.mp3)
  Segment 46: 70920ms ~ 71496ms (file: 163-612-613-163-613-130-163-160-160-103-130-102-613-130-103-726-627-267-267-162-103-301-301-371-736-705-576-602-260-206-267-206-263-206-263-267-627-620-260-320-312-201-231-637-3_V.mp3)
  Segment 47: 72496ms ~ 73072ms (file: 637-631-675-657-637-623-601-160-136-360-326-261-617-671-312-316-321-763-672-672-632-621-362-623-673-316-356-362-623-631-312-163-136-362-672-361-156-657-316-126-316-362-362-263-6_W.mp3)
  Segment 48: 74072ms ~ 74648ms (file: 316-301-360-326-632-620-602-120-130-601-267-327-236-132-213-310-206-260-265-502-521-623-126-540-510-102-612-623-236-302-361-632-162-136-132-321-765-267-162-163-670-670-607-761-6_W.mp3)
  Segment 49: 75648ms ~ 76224ms (file: 170-160-103-216-560-106-106-106-167-672-231-261-765-517-756-356-652-106-167-103-156-713-572-160-561-671-106-167-761-103-103-167-132-163-617-165-162-712-167 263-376-256-160-130-1_W.mp3)
  Segment 50: 77224ms ~ 77800ms (file: 317-132-103-136-623-360-321-627-106-167-106-132-675-267-206-136-103-561-576-567-615-163-317-136-132-103-105-103-107-167-621-173-174-650-756-675-765-675-761-167-610-160-613-163-1_V.mp3)
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
> **Time**: 2026-04-03 08:14:24
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "오늘 배운 문법을 활용해서 문장을 만들어 볼까요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "배운", "start_ms": 160, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "문법을", "start_ms": 290, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "활용해서", "start_ms": 430, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "문장을", "start_ms": 590, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "만들어", "start_ms": 720, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "볼까요?", "start_ms": 850, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "저는 한국 노래를 듣는 것을 정말 좋아해요.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 30, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "한국", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "노래를", "start_ms": 280, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "듣는", "start_ms": 430, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "것을", "start_ms": 550, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "정말", "start_ms": 660, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "좋아해요.", "start_ms": 780, "dur_ms": 190, "phase_lr_deg": 5.0}
]
},
{
"segment": 3,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 4,
"transcript": "어떤 가수를 가장 좋아하시나요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "어떤", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "가수를", "start_ms": 170, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 310, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "좋아하시나요?", "start_ms": 430, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 6,
"transcript": "저는 방탄소년단의 노래를 자주 들어요.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "방탄소년단의", "start_ms": 150, "dur_ms": 250, "phase_lr_deg": 5.0},
{"word": "노래를", "start_ms": 410, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "자주", "start_ms": 540, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "들어요.", "start_ms": 660, "dur_ms": 140, "phase_lr_deg": 5.0}
]
},
{
"segment": 7,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 8,
"transcript": "가사가 참 아름답고 위로가 되죠.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "가사가", "start_ms": 60, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "참", "start_ms": 200, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "아름답고", "start_ms": 280, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "위로가", "start_ms": 470, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "되죠.", "start_ms": 610, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "노래를 따라 부르면서 한국어를 익혀요.",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "노래를", "start_ms": 35, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "따라", "start_ms": 175, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "부르면서", "start_ms": 295, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "한국어를", "start_ms": 455, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "익혀요.", "start_ms": 605, "dur_ms": 130, "phase_lr_deg": 5.0}
]
},
{
"segment": 10,
"transcript": "정말 훌륭한 학습 방법이네요. 계속해 보세요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "훌륭한", "start_ms": 165, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "학습", "start_ms": 305, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "방법이네요.", "start_ms": 435, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "계속해", "start_ms": 625, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "보세요.", "start_ms": 755, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 12,
"transcript": "선생님 덕분에 한국어에 더 자신이 생겼어요.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "선생님", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "덕분에", "start_ms": 190, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "한국어에", "start_ms": 330, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "더", "start_ms": 490, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "자신이", "start_ms": 560, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "생겼어요.", "start_ms": 690, "dur_ms": 150, "phase_lr_deg": 5.0}
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
"transcript": "도움이 되어 기쁩니다. 오늘도 수고 많으셨어요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "도움이", "start_ms": 55, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "되어", "start_ms": 185, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "기쁩니다.", "start_ms": 295, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "오늘도", "start_ms": 445, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "수고", "start_ms": 585, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "많으셨어요.", "start_ms": 705, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "네 선생님 안녕히 계세요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 25, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "선생님", "start_ms": 95, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "안녕히", "start_ms": 245, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "계세요.", "start_ms": 405, "dur_ms": 140, "phase_lr_deg": 5.0}
]
},
{
"segment": 16,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 17,
"transcript": "안녕히 가시고 다음에 또 봐요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "가시고", "start_ms": 190, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "다음에", "start_ms": 330, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 450, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "봐요.", "start_ms": 520, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "조심히 들어가세요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "조심히", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "들어가세요.", "start_ms": 200, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "바이 바이.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "바이", "start_ms": 60, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "바이.", "start_ms": 230, "dur_ms": 340, "phase_lr_deg": 0.0}
]
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
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 22,
"transcript": "오늘 수업도 정말 보람찼습니다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "수업도", "start_ms": 160, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 290, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "보람찼습니다.", "start_ms": 410, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "공부하시느라 고생 많으셨어요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "공부하시느라", "start_ms": 30, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "고생", "start_ms": 250, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "많으셨어요.", "start_ms": 380, "dur_ms": 220, "phase_lr_deg": 0.0}
]
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
"transcript": "푹 쉬시고 다음에 봬요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "푹", "start_ms": 45, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "쉬시고", "start_ms": 125, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "다음에", "start_ms": 245, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "봬요.", "start_ms": 385, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "안녕히 가세요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 35, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "가세요.", "start_ms": 185, "dur_ms": 350, "phase_lr_deg": 5.0}
]
},
{
"segment": 27,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 28,
"transcript": "진짜 안녕.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 20, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "안녕.", "start_ms": 160, "dur_ms": 380, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "바이 바이.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "바이", "start_ms": 50, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "바이.", "start_ms": 220, "dur_ms": 340, "phase_lr_deg": 0.0}
]
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
"transcript": "수고.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "수고.", "start_ms": 60, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "고생.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "고생.", "start_ms": 40, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "안녕.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "안녕.", "start_ms": 25, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 35,
"transcript": "바이.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "바이.", "start_ms": 45, "dur_ms": 500, "phase_lr_deg": 0.0}
]
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
"transcript": "진짜 끝.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 30, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "끝.", "start_ms": 180, "dur_ms": 370, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 39,
"transcript": "안녕.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "안녕.", "start_ms": 55, "dur_ms": 490, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "수고.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "수고.", "start_ms": 35, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "바이.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "바이.", "start_ms": 25, "dur_ms": 530, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 43,
"transcript": "진짜 진짜 끝.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 190, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "끝.", "start_ms": 340, "dur_ms": 210, "phase_lr_deg": 0.0}
]
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
"transcript": "바이 바이.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "바이", "start_ms": 60, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "바이.", "start_ms": 230, "dur_ms": 330, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 47,
"transcript": "안녕.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "안녕.", "start_ms": 40, "dur_ms": 520, "phase_lr_deg": 0.0}
]
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
"transcript": "수고.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "수고.", "start_ms": 50, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "진짜 안녕.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "안녕.", "start_ms": 190, "dur_ms": 370, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 08:14:24*
