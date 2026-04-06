# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=07:58:48  uptime=23974s  mem=1511MB  system_processes=447"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_075839.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 312-276-326-641-261-621-312-751-167-321-321-301-302-132-361-376-316-713-162-213-143-613 713-176-612-312-362-136-163-632-136-613-136-361-301-152-612-175-126-163-163-132-316-136-6_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 625-625-625-625-625-261-261-267-261-217-267-126-126-261-216-621-162-612-267-267-216-261-261-126-216-612-126-612-213-312-236-213-126-312-351-537-356-316-315-362-613-263-635-637-6_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 273-312-132-375-203-203-203-236-203-203-135-315-315-231-320-236-623-632-653-235-230-302-325-312-136-312-326-302-103-157-572-675-672-613-136-312-302-321-132-237-236-326-326-361-3_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 132-316-106-126-123-162-621-123 631-132-103-136-103-153-531-712-167-736-316-163-163-130-163-163-510-316-617-316-137-103-130-103-103-516-103-125-132-617-103-136-103-645-671-163-1_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 126-132-163-132-163 672-612-126-163-162-621-236-623-261-261-162-231-621-621-326-132-623-126-126-123-261-316-612-162-162-213-632-162-310-130-106-612-617-103-102-130-130-136-612-1_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 612-231-125-625-625-261-236-261-326-263-260-216-217-261-321-362-316-612-316-136-123-130-160-165-123-213-216-136-136-136-136-612-362-163-123-610-632-216-160-136-376-103-163-106-1_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 103-130-103 310-603-175-761-102-130-361-574-571-362-301 132 547-576-163-312-567-571-576-573-125-327-675-675-657-103-136-123 136-163-613-567-567-623-321-312-132-130-103-137-132-2_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 106-136-126-162-160-106-106-160-216-213-536-576-351-365-653-136-106-302-302-230-321-301-362-360-203-206-206-326-302-302-103-103-261-216-217-637-261-167-174-103-103-136-103-312-1_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 760-675-163-162-106-103-301-302_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 632-631-637-627-567-657-657-165-105-103-103-130-631-301-612-260-602-752-672-670-672-670-675-675-623-126-261-236-267-726-273-320-327-657-163-136-167-167-123-163-153-163-675-163-3_V.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 312-136-136-675-637 756-672 765-562-756-103-102-132-635-623 125 267-263_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 267-632-637-721-675-672-625-176-576-265-625-265-625-265-267-261-671-236-725-123-615-675-652-637-657-652-325-321-761-671-132-156-613-671-672-267-672-761-673-176-367-631 631-617-6_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 106-163-316-162-163-321-126-163-136-103-163-103-610-106-136-160-167-106-126-617-126-132-163-103-106-163-162-162-163-157-132-210-132-621_V.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 130-136-537-527-265-632-325-263-236-237-326-321-216-532-321-365-627-267-625-657-657-567-563-675-635-547-567-576-576-527-527-327-573-356-762-672-625-256-756-562-256-265-237-235-2_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 765-764-765-765-576-756-756-672-675-672-157-153-675-670-176-735-761-670-170-756-765-765-167-670-756-750-613-165-163-671-163-765-762-756-756-752-367-136-576-576-157-715-173-103-3_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 302 312-361-301-670-607-761-671-613-136 267-627-267 675-675 105-561-650-156-615-675-625-267-136-631-617-160-610-160-165-615-637-627-162-316-136-162-621-167-167-761-167_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 617-316-162-167-631-302-321-137-135-750-705-765-132-754-761-267-132-721-315-312-134-132-613-106-136-132-143-136-137-132-173-132-123-103-167-167-106-167-321-302-321-317-236-236-7_V.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 310-361-362-130-103-106-132-163-103-376-607-263-625-621-321 263-263-754-576-756-753-231-756-756-316-613-612-621-726-217-102-731-765-176-765-712-637-761-302-627-316-631-631-312-1_W.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 625-621-213-263-263-236-261-123-231-263-265-673-672-612-632-631-163-752-367-672-265-675-765-756-765-761-671-672-267-267-627-265-267-263_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 103-102 561-175-176-516 130-306-136-136-546-625-673-127-130-136-106-126-156-317-130-103-136-175-136-136-132-103-103-756-137-327-605-617-160-612-136-103-162-617-137-137-106-326-1_V.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 123-136-213-673-172-120-162-216-206-201-126-126-123-203-231-321-173-132-756-745-754 560-567-567-657-615-165-106-176-106-106-327-106-167-756-765-675-670-623-263-326-206-207-206-1_V.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 215-261-265-216-216-213-251-652-615-620-261-261-126-621-621-526-235-365-265-216-126-236-157-153-513-153-537-576-156-153-531-521-510-210-215-213-216-351-327-217-123-216-514-574-5_W.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 176-765-260-230-312-132-720-756-750-756-360-130-675-657-621-163-167-136-163-136-613-126-316-753-576-162-610-675-607-621-162-162-761-675-657-675-617-625-675-761 165-617-675-671-6_W.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 260-203-120-102-123-126-163-126-306-103-106-301-132-301-627-126-203-201-201-216-126-620-260-260-263-326-302-320-326-302-320-230-302-301-625-265-130-621-631-176-103-132-263-206-2_V.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 612-623-621-316-321-765-376-210-216-301-316-631-671-326-312-310-326-301 513-607-607-607-617-361-631-132-102-162-163-146-145-164-146-143-164-675-675-675-672-602-612-163-132-670-6_V.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 623-632-236-632-263-623-260-263-623-623-263-623-623-632-625-623-632-263-623-623-261-623-362-623 576-527-265-267-256-256-325-236-263-263-216-265-256-256-257-267-236-321-327-327-1_W.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 163-136-371-627-267-672-671-675-607-601-617-617-601-671-610-671-675-362-617-671-756-765-762-672-675-765-764-672-762-316-632-562-516-362-136-576-765 163-612-237-216-213-207-324-3_W.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 127-123-671-106-615-130-175-165-716-176-167-102-163-273-103-120-210_V.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 326-752-326-203-326-632-632-623-625-632-621-361-326-326-321-312-216-103-126-123-312-310-310-321-312-327-320-320-635-312-132-576-576-305-375-371-372-317-371-623-621-362-321-107-1_W.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 362-316-136-103-103-163-163-613-103-653-657-163-153-124-170-167-567-657-567-567-321-126-176-576-576-206-106-106-106-106-107-657-160-617-163-301-301-623-623-206-236-206-657-675-6_V.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 123 623-627-613-265-620-627-623-126-136-316-162-362-613-712-123-103-126-560-651-605-673-672-632-123-613-613-612-163-762-632-623-162-123-613-162-316-756-673-672-762-763 236-231-2_W.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 316-216-263-162-216-213-312-102-216-162-176-526-126-167 321-132 132-106-361-312-123-236-613-263-326_W.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 571-765-756-265-256-256-263-756-725-267-276-267-276-273-367-312-310-301-210-201-210-102_W.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 132-312-321-132-574-574-574-106-103-620-627-623-362-130-130-102-320-302-306-306-107-176-710-716-610-306-530-157-357-135-103-103-103-103-132-103-137-136-103-715-713-301-310-316-5_V.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 326-260-123-621-106-167-501-170-127-127-126-726-617-523-126-156-516-163-765-176-106-103-136-156-123-162-160-103-163-316-176-162-617-617-106-162-106-162-157-176-125-167-162-162-1_W.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 315-173-132-136-126-137-135-175-561-635-153-561-615-571-357-567-715-657-365-135-563-675-156-657-516-567-165-751-536-135-157-165-175-153-156-715-751-175-157-175-651-567-156-657-5_W.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 527-261-261-267-276-236-321-261-263-326-236-253-236-263-132-173-167-126-216-173-713-176-176-731-375-372-327-237-257-257-265-265-265-256-275-257-276-261-263-216-162-125-523-265-3_W.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 312-216-130-102-126-162-301-613-136-132-312-132-310-123-263-201-213-602 362-326-326-236-362-765-376-632-623-623-326-673-362-265-265-627 123 623-263-735_W.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 236-326-320-301-213-136-132-132-120-103-302-203-310-123-305-203-216-362-765-675-672-760-263-360-610-316-312-620-612-631-361-316-263-260-267-627-620-607-162-103-130-103-132-302-3_V.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 123-126-102-103-103-136-615-657-167-612-160-106-157-153-105-162-163-130-163-175-175-561-123-126-126-576-567-617-162 263-102-312-231-231-526-627-206-632-213-203-123-132-136-106-1_W.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 713-172-102-216-213-126-137-320-321-523-231-132-531-657-530-512-125-123-321-132-321-302-672-207-276-524-572-621-326-327-273-275-207-572-572-750-257-203-273-213-731-756-572-257-2_W.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 312-302-301-312-302-302-321-132-310-361-627-756-726-627-532-317-701-713-103-167-136-132-326-621-627-632-675-167-617-132-625-361-132-657-562-675-261-263-263-236-256-236-576-567-5_W.mp3)
  Segment 43: 66192ms ~ 66768ms (file: 103-321-132-365-136-123-126-671-762-231-326-132-621-623-261-623-236-216-765-756-657-675-675-657-635-573-561-517-576-652-162-657-570-562-576-756-576-562-512-756-675-627-562-627-5_W.mp3)
  Segment 44: 67768ms ~ 68344ms (file: 627-576-574-567-105-170-701-715-751-163-172-715-756-675-675-765-756-175-176-132-715-756-756-756-576-756-756-675-760-675-671-176-162-103-601-137-673-637-706-713-675-673-316-301-3_W.mp3)
  Segment 45: 69344ms ~ 69920ms (file: 132-103-102-213-231-312-103-135-531-537-107-103-103-132-310-103-103-103-136-132-134-132-702-203-307-532-230-203-302-567-367-632-631-612-507-570-173-174-123-231-321-617-576-725-2_V.mp3)
  Segment 46: 70920ms ~ 71496ms (file: 731-725-276-375-123-537-135-156-156-156-132-631-136-576-157-513-135-216-635-512-523-543-513-510-534-256-536-652-625-650-657-560-506-653-265-265-265-265-265-263-623-632-623-620-5_W.mp3)
  Segment 47: 72496ms ~ 73072ms (file: 601-163-103-132-617-163-612-136-175-631-167-130-103-673-316-167-167-671-751-167-106-621-602-617-106-160-617-612-561-132-106-103-156-160-162-765-176-615-610-132-106-135-103-137-1_V.mp3)
  Segment 48: 74072ms ~ 74648ms (file: 613-136-132-632-621-627-162-162-631-132-623-627-621-621-261-675-673-671-672-312-627-162-613-321-372-326-316-362-136-261-321-632-672-627-632-627-627-673-612-632-261-672-657-675-6_W.mp3)
  Segment 49: 75648ms ~ 76224ms (file: 136-136-136-126-261-201-216-103-126-102-132-126-167-136-172-132-312-132-103_V.mp3)
  Segment 50: 77224ms ~ 77800ms (file: 312-132-672-671 132-130 317 657-162-756-576-675-672-627-761-657-672-567-627-610-162-762-627-672-675-673-671-621-621-612-106-613-362-620-623-765-671-623-672-612-167-627-627-632-6_W.mp3)
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
> **Time**: 2026-04-03 07:59:48
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "오늘 정말 수고 많으셨습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 160, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "수고", "start_ms": 290, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "많으셨습니다", "start_ms": 400, "dur_ms": 170, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "별말씀을요 저도 즐거웠어요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "별말씀을요", "start_ms": 30, "dur_ms": 160, "phase_lr_deg": 5.0},
{"word": "저도", "start_ms": 200, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "즐거웠어요", "start_ms": 320, "dur_ms": 250, "phase_lr_deg": 5.0}
]
},
{
"segment": 3,
"transcript": "다음 수업 때 웃으며 만나요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "다음", "start_ms": 55, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "수업", "start_ms": 175, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 275, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "웃으며", "start_ms": 335, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 465, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "네 안녕히 가세요 선생님",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 20, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "안녕히", "start_ms": 90, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "가세요", "start_ms": 240, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "선생님", "start_ms": 400, "dur_ms": 170, "phase_lr_deg": 5.0}
]
},
{
"segment": 5,
"transcript": "조심히 들어가고 푹 쉬세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "조심히", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "들어가고", "start_ms": 190, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "푹", "start_ms": 350, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "쉬세요", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "오늘 공부한 것도 잊지 마세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 35, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "공부한", "start_ms": 155, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "것도", "start_ms": 285, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "잊지", "start_ms": 385, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "마세요", "start_ms": 495, "dur_ms": 75, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "항상 응원하고 있을게요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "항상", "start_ms": 60, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "응원하고", "start_ms": 180, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "있을게요", "start_ms": 330, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "감사합니다 힘이 나네요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 20, "dur_ms": 240, "phase_lr_deg": 5.0},
{"word": "힘이", "start_ms": 270, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "나네요", "start_ms": 390, "dur_ms": 180, "phase_lr_deg": 5.0}
]
},
{
"segment": 9,
"transcript": "굿바이",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "굿바이", "start_ms": 80, "dur_ms": 490, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 11,
"transcript": "마지막으로 질문 하나만 더요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "마지막으로", "start_ms": 50, "dur_ms": 160, "phase_lr_deg": 5.0},
{"word": "질문", "start_ms": 220, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "하나만", "start_ms": 330, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "더요", "start_ms": 450, "dur_ms": 120, "phase_lr_deg": 5.0}
]
},
{
"segment": 12,
"transcript": "어떤 게 궁금하신가요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "어떤", "start_ms": 25, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 145, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "궁금하신가요", "start_ms": 205, "dur_ms": 360, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "다음 수업 준비물은 뭔가요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "다음", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "수업", "start_ms": 165, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "준비물은", "start_ms": 275, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "인가요", "start_ms": 425, "dur_ms": 140, "phase_lr_deg": 5.0}
]
},
{
"segment": 14,
"transcript": "교재와 필기도구만 챙겨오세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "교재와", "start_ms": 35, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "필기도구만", "start_ms": 175, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "챙겨오세요", "start_ms": 395, "dur_ms": 170, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 16,
"transcript": "네 알겠습니다 감사합니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 60, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "알겠습니다", "start_ms": 130, "dur_ms": 210, "phase_lr_deg": 5.0},
{"word": "감사합니다", "start_ms": 350, "dur_ms": 220, "phase_lr_deg": 5.0}
]
},
{
"segment": 17,
"transcript": "네 안녕히 가세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 30, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 260, "dur_ms": 310, "phase_lr_deg": 0.0}
]
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
"transcript": "오늘 하루도 행복하시길",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "하루도", "start_ms": 160, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "행복하시길", "start_ms": 290, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "당신도요 좋은 꿈 꾸세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "당신도요", "start_ms": 50, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 220, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "꿈", "start_ms": 330, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "꾸세요", "start_ms": 400, "dur_ms": 170, "phase_lr_deg": 0.0}
]
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
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 23,
"transcript": "안녕",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "안녕", "start_ms": 60, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "바이 바이",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "바이", "start_ms": 35, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "바이", "start_ms": 205, "dur_ms": 370, "phase_lr_deg": 0.0}
]
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
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 27,
"transcript": "다음에 또 봐요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 45, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 215, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "봐요", "start_ms": 325, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "나중에 연락할게요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "나중에", "start_ms": 30, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "연락할게요", "start_ms": 200, "dur_ms": 370, "phase_lr_deg": 0.0}
]
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
"transcript": "기다릴게요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "기다릴게요", "start_ms": 60, "dur_ms": 510, "phase_lr_deg": 5.0}
]
},
{
"segment": 31,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
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
"transcript": "벌써 시간이 이렇게 됐네요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "벌써", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "시간이", "start_ms": 170, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "이렇게", "start_ms": 310, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "됐네요", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 5.0}
]
},
{
"segment": 34,
"transcript": "그러게요 금방 지나갔어요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "그러게요", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "금방", "start_ms": 190, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "지나갔어요", "start_ms": 320, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "오늘도 많이 배우고 갑니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "오늘도", "start_ms": 20, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "많이", "start_ms": 170, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "배우고", "start_ms": 280, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "갑니다", "start_ms": 410, "dur_ms": 160, "phase_lr_deg": 5.0}
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
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 38,
"transcript": "도움이 됐다니 다행이에요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "도움이", "start_ms": 55, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "됐다니", "start_ms": 195, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "다행이에요", "start_ms": 325, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "그럼 이제 정말 안녕",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 35, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 145, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 245, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "안녕", "start_ms": 365, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "안녕히 가세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 25, "dur_ms": 160, "phase_lr_deg": 5.0},
{"word": "가세요", "start_ms": 195, "dur_ms": 380, "phase_lr_deg": 5.0}
]
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
"transcript": "행복한 저녁 보내세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "행복한", "start_ms": 45, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "저녁", "start_ms": 195, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "보내세요", "start_ms": 325, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "당신도요 수요일에 봐요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "당신도요", "start_ms": 30, "dur_ms": 160, "phase_lr_deg": 5.0},
{"word": "수요일에", "start_ms": 200, "dur_ms": 180, "phase_lr_deg": 5.0},
{"word": "봐요", "start_ms": 390, "dur_ms": 180, "phase_lr_deg": 5.0}
]
},
{
"segment": 45,
"transcript": "조심히 들어가세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "조심히", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "들어가세요", "start_ms": 210, "dur_ms": 360, "phase_lr_deg": 0.0}
]
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
"transcript": "나중에 봬요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "나중에", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "봬요", "start_ms": 200, "dur_ms": 370, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 49,
"transcript": "진짜 안녕",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 25, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "안녕", "start_ms": 175, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "바이 바이",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "바이", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "바이", "start_ms": 200, "dur_ms": 370, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 07:59:48*
