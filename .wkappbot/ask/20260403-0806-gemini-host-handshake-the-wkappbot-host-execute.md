# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=08:05:19  uptime=24364s  mem=1566MB  system_processes=447"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_080509.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 162-135-123-132-126-321-132-126-162-126-176-765-267-136-267-123-621-126-102-132-137-102-312-130-106-126-106-671-760-761-107-617-107-761-761-617-762-602-361-362-635-671-576-675-7_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 750-137-361-627-206-136-153-132-613-675-356-657-653-576-675-657-765-567-675-305-132-137-620-362-136-160-750-705-756-230-231-276-267-321-136-321-137-216-213-754-574-301-213-320-6_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 326-267-263-236-263-236-263-267-263-217 231-236-657-625-625_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 132-176-173-312-132-132-312-176-207-132-120-165-103-123-126-627-675-605-675-167-176-170-157-150-103-517-156-316-612-132-210-321-132-321-325-231-235-215-710-302-132-316-230-372-2_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 351-503-502-125-312-267-627-276-235-235-235-265-203-312-721-213-206-102-120-213-175-153-132-302-231-132-126-103-263-213-320-237-572-576-572-527-321-321-321-627-756-732-725-762-7_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 316-316-163-326-316-216-130-673-623-106 301 301-321-613-132-173-132-321-301-321-301-301-312-136 675-176-671-761-176-326-136-316-361 576-567-657-216-562-162-362-165-103-312-136-1_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 320-302-326-203-206-203 203-102-106-163-310-310-301-310-137-136-103-321-310-751-217-321-230-130-102-103-206-205-236-231-106-160-261-320-237-102-621-103-576-231-172-103-103-132-1_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 632-103-137-103-123-617-136-315-132-163-163-106-163-153-103-103-612-365-631-136-163-136-631-160-756-672-612-163-126-612_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 751-765-736 216-615-120-162-105-657-627-163-103-103-206-260-265-123-213-267-562-256-265-105-163-167-321-263-206-265-206-236-602-163-301-326-230-320-320-312-130-321-231-275-325-3_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 613-653-136-613-312-136-163-216-632-162-123-103-136-126-136-103-651-132-136-516-321-260-273-236-106-103-172-136-613-103-615-132-130-103-617-123-213-163-261-573-123-176 756-176-1_V.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 150-765-623-162-103-132-136-567-617-165-103-361-107-615-316-136-107-165-176-163-263-602-136-136-163-163-106-256-761-160-162-153-136-132-103-263-574-136-631-176_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 267-627-627-126-160-120-602-632-231 316-267-675-673-617-361_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 302-326-530-105-135-136-673-601-610-163-613-106-601-601-607-607-670-675-607-607-607-617-106-671-615-106-620-260-137-136-156-105-156-510-506-315-310-302-103-102-167-716-167-106-1_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 754-574-570-574-572-320-236-620-623-162-236-231-627-705-574-572-302-302-732-754-756-307-320-203-275-572-574-106-326-602-627-617-750-325-517-135-103-123-261-672-672-623-362-136-1_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 230-627-263-765-176-176-123-123-132-601-607-726-765-756-357-371-230-625-132-123-765-261-761-216-203-127-623-752-102-127-172-756-763-765-276-326-203-136-765-362-507-203-207-723-2_V.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 632-637-612-163-162-162-136-312_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 765-756-765-752-273-756-752-572-571-572-571-756-265-725-526-207-625-657-657-657-627-102-165-105-521-257-572-576-567-756-671-623-375-367-267-627 716-615-617-261-612-361-136-136-3_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 312-326-312 316-312-327-321-327-312-315-531-503-530-512-537-570-574-507-572-571-267-623-652-576-756-567-672-263-132-136-136-165-517-567-576-570-507-576-364 103-603-623-160-631-2_V.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 130-312-632-163-310-675-671-362-301-316-103-312-160-361-103-162-163-675-672-762-617-162-136-571-517-156-371-261-361-123-216-162-316-312-623-126-301-316-312-321-617-627-603-601-6_V.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 357-153-312-312 267-267-256-265-265-265-265-261-267-267-627-267-267-267-267-526-527-572-531-125-132-312-123 123-231-231-123-132-132-312-571-576-567-351-236-231-123-312-132-132-1_W.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 501-526-326-132-132-163-132-652-675-675-756-526-256-176-657-762-206-320-167-756-756-213-106-103-103-631-617-756-167-103-756-175-657-613-756-750-750-137-167-136-316-750-750-301-3_V.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 675-765-765-756-675-106-361-263-260-673-756-765-726-267-670-672-672-627-765-675-675-673-637-367-267-672-267-302-326-267-326-705-765-762-675-675-576-567-567-576-316-763-375-751-1_W.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 653-603-631-136-163-617-670-601-603-501-576-576-526-621-632-621-632-162-126-631-263-261-621-312-236-623-263-302-320-320-326-627-627-672-627-162-136-623-627-167-176-123-217-620-2_W.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 123-216-213-261-261-263-523-527-572-562-657-162-631-162-216-263-126-267-623-326-367-637-361-613-621-263-623-623-632-621-621-621-216-263-261-267-267-627-623-263-263-267-621-627-2_W.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 103-163-102-106-106-320-302-236-612-610-163-102-132-310-103-213-163-126-216-201-123-261-602-302-321-310-301-103-106-163-601-610-130-136-310-163-623-602-236-763-675-672-602-760-1_V.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 165-172-106-137-107-635-175-126 135-163-106-671-106-675-103-612-136-126-736-160-106-612-132-621-106-106-163-167-130-160-103-136-751-613-167-162-136-136-601-167-261-601-103-162-1_V.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 312-301-560-360-632-302 571-612-162_V.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 613-617-617-627-617-167-753-137-315-135-315-312-315-132-513-130-103-132-312-136-263-632-163-261-231-632-261-136-236-321-132-326-623-136-612-263-672-632-621-162-170-107 260-623-3_W.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 126-761-132-361-130-103-576-136-160-316-136-136-316-316-103-160-160-561-312-162-260-162-132-136-610-163-167-167-163-106-136-263-623 621-312-567-132-132 213-126-213-126-210-126-2_V.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 136-312-130-612-625-617-576-157-510-361-162-162-507-370-103-103-137-316-136-103-601-157-326-320-230-532-507-657-132-162-213-126-132-106-154-136-136-317-136-163-160-612-103-301-3_V.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 136-625-217-106-130-126-160-156-631-316-162-261-310-230-310-312-273-237-632-276-267-123-621-123-271-750-275-172-176-762-762-761-762-761-675-706-701-761-761-162-126-127-126-726-7_W.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 672-672-673-671 620-623-625-231-103-103-136-162-621-620-602-623-620-206-231-263-261-206-203-203-206-123-102-103-621-620-602-620-260-602-260-620-620-620-621-763-764 670-607-670-6_W.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 726-327-725 327-726-715-132-123-623-302-132-210-163-132-132-123-201-672-237-610-326-132-132-123-172-125-157-135-137-103-137-173-123-302-231-237-327-327-753-123-102-215-103-103-1_W.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 132-137-103-132-126-210-231-263-231-235-201-237-321-275-756-612-576-567-761-106-516-617-136-103-137-312-371-163-103-130-162-671-130-301-320-302-675-736-763-310-103-310-130-163-1_V.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 601-160-162-162-705-162-176-613-106-102-617-310-613-627-316-610-765-675-615-167-167-632-162-312-261-621-302-312-123-312-760-720-267-260-620-236-267-603-672-267-612-632-617-162-1_W.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 162-623-213-216-213-301-650-572-167-362-132-103-301-130-103-136-310-130-301-315-103-137-103-130-302-367-307-321-362-132-123-162-102-160-132-123-132-132-263-261-123-621-162-162-1_V.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 371-137-365-365-543-540-534-354-310-276-726-762-312-136-367-317-123-126-276-263-237-263-632-265-254-621-235-275-231-316-317-327-315-321-237-723-237-271-271-237-265-514-534 160-3_W.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 103-657 610-671-617-672-756-765-576-657-657-576-576-756-657-567-567-576-567-576-657-765-761-765-756-675-675-657-675-675-675-657-756-657-567-567-576-657-567-756-765-765-765-765-5_W.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 103-132-231-213-237-276-765-765 567-635-632-620-362-310-301-132-312-301-213-231-672-672-623-603-630-620-620-612 321-123-263-623-103-230-236-203-201-126-621-617-106-302-675-567-5_V.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 612-160-106-731 620-260-260-206-263-321-312-316-547-504-130-310-315-310-320-320-236-236-263-375-726-672-516-601-160-160-623-621-316-162-316-621-631-216-320-236-321-162-126-362-1_V.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 756-765-765-726-762-765-762-712-762-726-726-726-762-726-726-217-321-523-231-321-231-276-231-235-256-263-132-263-627_W.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 302-756-310-301-302-103-136-315-135-613-623-136-153-371-163-735-325-231-236-325-321-235-230-301-231-267-263-236-326-312-213-217-201-230-265-231-253-320-231-632-162-316-267-672-6_W.mp3)
  Segment 43: 66192ms ~ 66768ms (file: 657-167-631-136-361-312-132-123-103-653-613-136-106-326-367-516-137-137-167-236-167-627-671-160-106-176-631-106-631-126-165-162-376-213-705-163-106-106-106-312-316-176-576-612-6_V.mp3)
  Segment 44: 67768ms ~ 68344ms (file: 162-657-651 671-130-103-205-517-106-756-265-123-623-612-123-263-302-213-610-167-162-567-136-610-316-167-176-165-106-546-621-612-162-316-162-103-301-167-631-361-651-162-651-162-6_V.mp3)
  Segment 45: 69344ms ~ 69920ms (file: 576-756-576-506-765-576-657-617-607-760-675-657-657-675-607-607-631-136-310-316-310 263-216-657-321-136-312-132-136-312-310-671-567-567-657-516-150-126-310-563-567-567-567-576-5_W.mp3)
  Segment 46: 70920ms ~ 71496ms (file: 162-260-236-203-312-657-675-263-302-302-320-310-302-301-310-310-576 231-206-206-263-261-312-316-301_V.mp3)
  Segment 47: 72496ms ~ 73072ms (file: 267-265-260-263-726-762-267-376-261-267-263-672-617-613-126-321-206-132-126-316-231-123-723-267-263-236-236-236-732-732-231-312-132-126-263-623-572-623-627-627-136-136-613-162-3_W.mp3)
  Segment 48: 74072ms ~ 74648ms (file: 657-123-126-210-132-623-203-673-203-321-312-132-162-163-132-153-102-136-362-612-620-620-213-132-312-132-132-312-317-376-130-315-371-173-713-750-572-325-623-320-132-312-620-235-3_W.mp3)
  Segment 49: 75648ms ~ 76224ms (file: 167-160-765-167-106-136-321-130-546-126-162-167-106-125-657-136-163-716-106-106-172-106-103-167-167-103-510-127-167-136-175-103-103-652-765-103-167-136-564-123-156-175-362-136-1_V.mp3)
  Segment 50: 77224ms ~ 77800ms (file: 763-137-716-731-602-627-632-672-627-627-637-361-632-361-627-237_W.mp3)
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
> **Time**: 2026-04-03 08:06:15
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "오늘 배운 내용을 최종 복습해 보겠습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "배운", "start_ms": 160, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "내용을", "start_ms": 290, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "최종", "start_ms": 440, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "복습해", "start_ms": 530, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "보겠습니다", "start_ms": 670, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "한국어 문법은 알면 알수록 재미있어요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 30, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "문법은", "start_ms": 180, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "알면", "start_ms": 320, "dur_ms": 90, "phase_lr_deg": 5.0},
{"word": "알수록", "start_ms": 420, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "재미있어요", "start_ms": 540, "dur_ms": 210, "phase_lr_deg": 5.0}
]
},
{
"segment": 3,
"transcript": "다음에도 즐겁게 공부해 봐요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "다음에도", "start_ms": 55, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "즐겁게", "start_ms": 215, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "공부해", "start_ms": 355, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "봐요", "start_ms": 475, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "네 선생님 수고 많으셨습니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 20, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "선생님", "start_ms": 90, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "수고", "start_ms": 240, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "많으셨습니다", "start_ms": 350, "dur_ms": 220, "phase_lr_deg": 5.0}
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
"transcript": "안녕히 가시고 다음에 또 봬요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "가시고", "start_ms": 190, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "다음에", "start_ms": 330, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 450, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "봬요", "start_ms": 510, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "건강하게 잘 지내고 계세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "건강하게", "start_ms": 60, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 220, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "지내고", "start_ms": 290, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "계세요", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 9,
"transcript": "바이 바이 수고했어",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "바이", "start_ms": 35, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "바이", "start_ms": 155, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "수고했어", "start_ms": 275, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "정말 고마워요 안녕",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "고마워요", "start_ms": 165, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "안녕", "start_ms": 385, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "열심히 하는 모습이 보기 좋아요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "열심히", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 200, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "모습이", "start_ms": 300, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "보기", "start_ms": 430, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "좋아요", "start_ms": 520, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "항상 긍정적인 마음으로 임하세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "항상", "start_ms": 30, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "긍정적인", "start_ms": 150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "마음으로", "start_ms": 310, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "임하세요", "start_ms": 460, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 14,
"transcript": "네 알겠습니다 감사합니다 선생님",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 40, "dur_ms": 50, "phase_lr_deg": 5.0},
{"word": "알겠습니다", "start_ms": 100, "dur_ms": 160, "phase_lr_deg": 5.0},
{"word": "감사합니다", "start_ms": 270, "dur_ms": 210, "phase_lr_deg": 5.0},
{"word": "선생님", "start_ms": 490, "dur_ms": 140, "phase_lr_deg": 5.0}
]
},
{
"segment": 15,
"transcript": "모두 평안한 하루 보내세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "모두", "start_ms": 55, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "평안한", "start_ms": 175, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "하루", "start_ms": 315, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "보내세요", "start_ms": 435, "dur_ms": 150, "phase_lr_deg": 0.0}
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
"transcript": "안녕히 가세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "가세요", "start_ms": 190, "dur_ms": 380, "phase_lr_deg": 5.0}
]
},
{
"segment": 18,
"transcript": "조금 이따가 다시 뵙겠습니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "조금", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "이따가", "start_ms": 165, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 305, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "뵙겠습니다", "start_ms": 415, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "바이 바이",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "바이", "start_ms": 40, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "바이", "start_ms": 210, "dur_ms": 370, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 21,
"transcript": "감사합니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 60, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "안녕히 가세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 35, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 195, "dur_ms": 380, "phase_lr_deg": 0.0}
]
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
"transcript": "안녕",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "안녕", "start_ms": 25, "dur_ms": 530, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 26,
"transcript": "작별",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "작별", "start_ms": 50, "dur_ms": 520, "phase_lr_deg": 0.0}
]
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
"transcript": "바이",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "바이", "start_ms": 30, "dur_ms": 540, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "진짜 안녕",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "안녕", "start_ms": 200, "dur_ms": 370, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 31,
"transcript": "수고",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "수고", "start_ms": 60, "dur_ms": 510, "phase_lr_deg": 0.0}
]
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
"transcript": "고생",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "고생", "start_ms": 45, "dur_ms": 530, "phase_lr_deg": 0.0}
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
"transcript": "안녕",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "안녕", "start_ms": 25, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "바이",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "바이", "start_ms": 50, "dur_ms": 520, "phase_lr_deg": 0.0}
]
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
"transcript": "안녕히",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 40, "dur_ms": 530, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "잘 가",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "잘", "start_ms": 30, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "가", "start_ms": 150, "dur_ms": 420, "phase_lr_deg": 0.0}
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
"transcript": "바이",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "바이", "start_ms": 45, "dur_ms": 530, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "안녕",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "안녕", "start_ms": 55, "dur_ms": 520, "phase_lr_deg": 0.0}
]
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
"transcript": "끝",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "끝", "start_ms": 30, "dur_ms": 540, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "수고",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "수고", "start_ms": 40, "dur_ms": 530, "phase_lr_deg": 0.0}
]
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
"transcript": "진짜 안녕",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 60, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "안녕", "start_ms": 210, "dur_ms": 360, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 08:06:15*
