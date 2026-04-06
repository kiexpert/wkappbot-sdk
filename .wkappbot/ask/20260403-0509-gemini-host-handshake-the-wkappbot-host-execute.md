# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=05:08:27  uptime=13752s  mem=1354MB  system_processes=447"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_050817.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 312-312-130-302-203-236-301-543-536-302-215-132-132-637-756-376-627-132-106-136-167-756-765-760-753-756 132-132-627-627-762-736-607-126-657-765-132 576-576-657-675-756-157-135-1_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 213-132-267-326-102-213-326-623-263-623-623-362-602-312-231-103-617-106-163-213-203-102-160-103-160-103-516-321-312-216-326-317-312-326-326-126-621-263-132-106-103-623-612-632-6_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 617-617-617-167-673-671-167-617-617-167_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 123-312-123-321-261-126-321-326-231-321-523-265-321-265-235-361-231-326-321-312-321-231-321-321-312-312-321-521-526-325-326-325-352-321-321-312-135-132-231-132-253-152-321-231-1_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 351-316-351-263-267-267-263-236-236-263-326-236-267-267-627-267-627-356-543-547-357-573-574-547-534-536-352-536-531-351-513-357-351-357-356-350-356-356-356-356-365-356-265-625_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 125-135-530-653-230-302-372-126-123-762-720-726-276-237-273-237-230-237-321-201-321-201-103-230-326-623-230-263-263-623-230-326-302-302-320-321-326-362-362-362-362-320-302-302-3_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 153-263-267-263-762-376-302 627-672 576-576-751-167-576-657-672 675-672 123-216-362-126-261-126-362 137-321-627-362-136-163-162-672-627-621-627-216-210-617-613-612-617-167-162-1_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 217-263-263-260-263-263-236-263-267-267-627-627-263-236-321-267-612-362-263-261-267-261-267-623-627-621-162-312-316-312-362-216-261-625-263-217-621-261-267-267-261-267-265-263-6_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 310-312-103-103-135-571-576-103-103-627-625-103-361-163-236-302-302-375-675-301-136-132-312-316-143-103-103-203-206-213-132-103-134-103-263-267-206-206-267-627-627-602-627-672-6_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 756-751-156-172-136-576-570-576-765-623-620-627-657-361-615-750-670-607-760-176-167-173-567-105-175-715-167-175-107 762-672-673-367-637-362-134-132-314-132-312-672-261-216-123-5_V.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 201-103-312-620-261-216-620-602-756-760-164-230-217-510-571-256-235-236-206-602-106-230-120-765-273-762-765-756-756-753-231-236-216-716-751-765-765-672-762-761-761-761-237-623-6_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 617-617-765-620-627-620-602-320-301-316-203-602-260-162-163-106-103-106-710-623-260-201-126-120-126-162-310-130-132-136-123-572-570-253-276-237-236-203-132-321-305-705-175-137-1_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 736-617-103-672-621-327-623-320-320-364-362-301-302-672-671-675-672-617-256-526-632-231-326-321-672-602-610-162-126-203-620-632-362-103-360-320-235-125-316-302-673-206-612-126-1_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 301-132-123-312-123-132-137-136-132-312-173-372-312-703-627-620-312-321-321-321-316-136-315-315-317-137 516-507-765-376-312-106-106-316-312-316-316-316-315-136-163-310-312-132-1_V.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 762-763-756-756-765 756 756-756-765-765-675-762-763-765-765-765-576-567-567-756-756-756-765-756-756-765-765-765-761-716-756-765-675-672-712-123 574-657-672-672-760-706 627-675-5_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 163-106-136-163-136-106-617-613-631-163-126-261-321-301-312-132-316-706-756-267-231-132-163-301-306-361-376-632-632-617-615-673-762 610-106-103-312-162-261-675-764-306-316-137-1_V.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 513-351-351-157-351-153-513-153-157-153-513-517-135-157-153-150-135-157-513-517-156-165-156-501-521-512-651-653-163-163-310-316-316-315-315-130-136-132-312-261-365-516-512-567-6_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 136-236-176-217 361-672-162-163-261-163-623-160-106-103-132-103-105-136-617-103-123-316-361-136-136-103-175-574-213-605-136-162-617-602-165-165-106-136-103-731-157-756-756 756-7_V.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 756-617-675-601-672-725-716-731-173_V.mp3)
  Segment 20: 29944ms ~ 31852ms (file: 176 175 756-675-675-751-756-756-765-136-627-163-162-213 657-625-326-621-627-627-612-123 132-132-321-257-217-236-273-617-126 672-672-675-632-306-632-672_W.mp3)
  Segment 21: 32852ms ~ 33428ms (file: 321-132-302-312-132-623-620-260-260-260-236-231-130-321-362-301-130-317-765-230-206-230-162-612-612-612-632-612-623-602-612-612-162-612-162-103-301-136-321-562-563-612-627-672-1_W.mp3)
  Segment 22: 34428ms ~ 35004ms (file: 731-701-713-173-610-106-167-163-631-312-103-136-103-130-617-607-625-620-760-706-670-751-715-706-760-761-706-760-107-621-621-361-263-236-162-130-130-130-316-621-316-362-362-316-3_W.mp3)
  Segment 23: 36004ms ~ 36580ms (file: 367-762-762-120-132-236-321-213-326-263-231-326-312 267-263-263-263-236-276-263-236-261-267-265-261-263-526-325-632-106-362-752-236-621-236-216-261-236-627-672-261-276-567 576-6_W.mp3)
  Segment 24: 37580ms ~ 38156ms (file: 106-160-103-310-132-175-612-301-136-103-163-136-107-137-756-705-103-103-103-132-103-130-276-312-306-362-326-203-230-675-167-106-132-106-267-120-620-201-132-526-625-263-136-173-2_V.mp3)
  Segment 25: 39156ms ~ 39732ms (file: 516-573-675-752-576-570-357-376-750-573-567-657-675-627-672-673-736-726-672-567-567-351-315-135-167-361-163-163-267-627-261-261-263-632-615-536-135-316-321-326-136-613-162-316-3_W.mp3)
  Segment 26: 40732ms ~ 41308ms (file: 132-102-123-132-103-156-132-615-607-670-130-716-167-756-617-601-162-671-103-130-316-132-321-321-316-321-310-301-361-367-673-302-367-320-236-675-607-670-675-627-126-123-126-263-2_W.mp3)
  Segment 27: 42308ms ~ 42884ms (file: 207-572-572-165-751-576-567-567-576-567-235-365-567-756-165-176-617-102-206-207-210-132-103-105-105-167-105-160-652-620-675-657-657-136-135-310-305-173-170-102-105-106-135-157-5_W.mp3)
  Segment 28: 43884ms ~ 44460ms (file: 653-562-637-576-576-562-567-562-536-657-632-625-516-563-652-625-562-576-657-576-652-657-657-257-652-265-671-762-563-365-625-672-325-562-267-261-657-265-172-625-623-371-567-652-2_V.mp3)
  Segment 29: 45460ms ~ 48196ms (file: 126 106-136-132-172-612-175-103-617-715-621-162-163-175-176-162-102-126-103-167-172-127-651-162-571-362-176 103-125-612-712-160-105-103-175-123-310-301-123-175-316-617-103-106-1_V.mp3)
  Segment 30: 49196ms ~ 49772ms (file: 301-320-162-163-126-123-231-103-103-316-136-103-167-612-765-176-167-167-103-167-731-106-127-713-721-617-163-130-301-306-160-130-136-163-163-106-162-163-103-103-321-316-302-103-1_V.mp3)
  Segment 31: 50772ms ~ 51348ms (file: 510-657-506-756-613-203-312-231-302-230-302-132-614-132-103-302-320-302-120-130-301-103-167-103-132-103-561-520-215-504-541-351-130-302-230-320-301-361-301-136-103-301-130-136-1_V.mp3)
  Segment 32: 52348ms ~ 52924ms (file: 615-126-670-162-103-103-106-136-136-132-103-132-163-143-132-103-165-201-103-126-126-162-126-517-760-615-163-163-163-546-126-130-617-627-713-613-712-613-607-361-632-617-631-653-1_V.mp3)
  Segment 33: 53924ms ~ 54500ms (file: 576-260-623-627-627-756-765-576-157-751-107-670-657-561-376-360-361-321-301-176-167-167-172-367-132-263-263-321 610-106-103-106-107-137-261-206-302-602-350-567-526-203-256-103-3_W.mp3)
  Segment 34: 55500ms ~ 56076ms (file: 267-627-267-136-362-236-236-263-263-361-263-326-321-326-673-361-167-547-231-123-367-361-367-136-675-657-312-372-276-231-327-524-543-265-627-564-652-627-326-321-765-765-762-672-6_W.mp3)
  Segment 35: 57076ms ~ 57652ms (file: 260-261-162-106-670-607-673-163-172-137-371-317-301-751-670-765-136-671-761-670-136-106-167-607-613-607-567-567-763-167-203-560-457-567-123-315-316-236-132-172-231-236-263-103-7_W.mp3)
  Segment 36: 58652ms ~ 59228ms (file: 673-672-163-163-132-316-103 103-136-103 670-670-670 672-672-672-762-130-310-130-310-203-263-206-236-102-163-103-103-162-136-301-103-310-103-316-312-206-206-236-132-321-301-312-1_V.mp3)
  Segment 37: 60228ms ~ 60804ms (file: 651-561-513-561-562-567-561-651-651-567-136-136-136-163-165-156-156-165-165-615-165-152-521-153-123-132-160-106-356-165-106-106-650-650-560-650-561-165-501-150-102-103-130-130-1_W.mp3)
  Segment 38: 61804ms ~ 62380ms (file: 152-135 263-756-765-716-167-671-765-672-612-765-263-216-265-326-132-612-167-275-265-316-163-351-103-602-675-316-132-163-103-360-621-136-312-620-216-321-276-132-127-361-163-106-1_W.mp3)
  Segment 39: 63380ms ~ 63956ms (file: 367-316-231-213-213-231-235-136-213-213-213-213-531-651-625-526-675-675-507-632-327-376-371-372-376-365-326-376-736-162-163-163-613-163-516-517-153-156-514-257-325-267-321-327-3_W.mp3)
  Segment 40: 64956ms ~ 65532ms (file: 326-261-267-265-256-532-652-654-654-562-523-561-567-312-312-132-326-312-321-236-132-132-521-531-215-312-321-321-321-236-213-231-132-126-132-351-357-572-572-567-276-127-265-260-2_W.mp3)
  Segment 41: 66532ms ~ 67108ms (file: 103-130-120-713-372-123-106-302-326-620-263-126-162-301-320-751-126-123-260-260-216-261-206-263-320-306-236-672-672-176-106-670-637-670-163-612-673-163-163-601-621-612-167-516-6_V.mp3)
  Segment 42: 68108ms ~ 68684ms (file: 631-632-632-361-672-627-672-671-631-326-326-362-362-536-267-621-312-163 163-612-126-267-317-163-126-621-625-657-576-657-675-672-627-130-162-736-123-123-367-567-567-657-675-657-7_W.mp3)
  Segment 43: 69684ms ~ 70260ms (file: 601-306-301-163-130-136-136-126-612-675-162-137-317-237-237-132-613-163-164-123-260-261-612-261-621 260-206 271_W.mp3)
  Segment 44: 71260ms ~ 71836ms (file: 751-751-715-715-176-137-761 617-163-123-216-621-210-126-102-612-621-276-132-750-726-762-267-236-621 170-610 163-617-361-163_W.mp3)
  Segment 45: 72836ms ~ 73412ms (file: 261-621-261-362-265-267-754-365-376-361-127-306-165-167-162-136-610-501-576-567-725-162-617-631-567-657-567-736-106-610-160-762-672-107-157-130-137-675-567-367-574_W.mp3)
  Segment 46: 74412ms ~ 74988ms (file: 761-301-310-602-263-632-236-130-301-316-206-206-206-206-206-203-231-236-320-302-320-123-106-162-657-610-301-302-750-750-236-206-263-253_V.mp3)
  Segment 47: 75988ms ~ 76564ms (file: 657-675 162-132-216-321-613-623-627-612-162-613-623-612-621-312 567-657 627 136-136_W.mp3)
  Segment 48: 77564ms ~ 78140ms (file: 726-276-231-276-216-267-263 216-261 612-672-612-625-612-721-236-236-236-263-276-327-107-176-130-173-132-106-107-103-163-106-137-362-761-301-673-106-701-701-170-107-612-567-263-1_W.mp3)
  Segment 49: 79140ms ~ 79716ms (file: 135-103-103-106-163-615-103-106-106-132-106-167-601-610-617-162-216-627-216-216-260-216-162-106-126-163-126-176-173-106-617-102-103-136-103-106-160-613-106-103-615-216-165-102-1_V.mp3)
  Segment 50: 80716ms ~ 81292ms (file: 672 672-762-263-627 623-672-623-623-623 675-627-627_W.mp3)
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
> **Time**: 2026-04-03 05:09:40
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "오늘의 명언을 들려드릴게요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "오늘의", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "명언을", "start_ms": 200, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "들려드릴게요", "start_ms": 390, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "성공은 최종적인 것이 아닙니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "성공은", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "최종적인", "start_ms": 165, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "것이", "start_ms": 325, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "아닙니다", "start_ms": 435, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 4,
"transcript": "Success is not final.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "Success", "start_ms": 45, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 255, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "not", "start_ms": 345, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "final.", "start_ms": 455, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "실패 또한 치명적인 것이 아닙니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "실패", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "또한", "start_ms": 180, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "치명적인", "start_ms": 310, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "것이", "start_ms": 500, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "아닙니다", "start_ms": 590, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "Failure is not fatal.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Failure", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 230, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "not", "start_ms": 320, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "fatal.", "start_ms": 430, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "중요한 것은 계속해 나가는 용기입니다",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "중요한", "start_ms": 55, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "것은", "start_ms": 215, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "계속해", "start_ms": 325, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "나가는", "start_ms": 455, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "용기입니다", "start_ms": 585, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "It is the courage to continue that counts.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "It", "start_ms": 45, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 105, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 165, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "courage", "start_ms": 255, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 415, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "continue", "start_ms": 505, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "that", "start_ms": 695, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "counts.", "start_ms": 785, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "윈스턴 처칠의 명언이었습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "윈스턴", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "처칠의", "start_ms": 200, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "명언이었습니다", "start_ms": 330, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "이 말이 여러분에게 힘이 되었기를 바라요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 60, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "말이", "start_ms": 120, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "여러분에게", "start_ms": 230, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "힘이", "start_ms": 420, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "되었기를", "start_ms": 530, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "바라요", "start_ms": 660, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "I hope these words gave you strength.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 50, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "hope", "start_ms": 110, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "these", "start_ms": 220, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "words", "start_ms": 330, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "gave", "start_ms": 460, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 570, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "strength.", "start_ms": 660, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "포기하고 싶을 때 이 말을 떠올려보세요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "포기하고", "start_ms": 65, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "싶을", "start_ms": 255, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 365, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 455, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "말을", "start_ms": 515, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "떠올려보세요", "start_ms": 625, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "당신은 생각보다 훨씬 강한 사람입니다",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "당신은", "start_ms": 70, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "생각보다", "start_ms": 230, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "훨씬", "start_ms": 390, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "강한", "start_ms": 520, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "사람입니다", "start_ms": 630, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "You are much stronger than you think.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "You", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "are", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "much", "start_ms": 250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "stronger", "start_ms": 360, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "than", "start_ms": 550, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 640, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "think.", "start_ms": 730, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 16,
"transcript": "계속해서 노력하는 당신이 아름답습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "계속해서", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "노력하는", "start_ms": 230, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "당신이", "start_ms": 390, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "아름답습니다", "start_ms": 520, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "(instrument)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 18,
"transcript": "작은 실천이 큰 변화를 만듭니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "작은", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "실천이", "start_ms": 140, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 270, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "변화를", "start_ms": 360, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "만듭니다", "start_ms": 490, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "Small actions make big changes.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "Small", "start_ms": 45, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "actions", "start_ms": 175, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "make", "start_ms": 335, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "big", "start_ms": 425, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "changes.", "start_ms": 535, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 21,
"transcript": "오늘도 수고 많으셨습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "오늘도", "start_ms": 30, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "수고", "start_ms": 160, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "많으셨습니다", "start_ms": 270, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "Good job today.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "Good", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "job", "start_ms": 165, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "today.", "start_ms": 275, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "내일은 더 좋은 소식으로 만나요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "내일은", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 210, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 270, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "소식으로", "start_ms": 380, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 540, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "See you tomorrow with better news.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 55, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 165, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "tomorrow", "start_ms": 255, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "with", "start_ms": 415, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "better", "start_ms": 505, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "news.", "start_ms": 635, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "안녕히 가세요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 30, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 220, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "Goodbye.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Goodbye.", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 28,
"transcript": "항상 응원합니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "항상", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "응원합니다", "start_ms": 165, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "Always rooting for you.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Always", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "rooting", "start_ms": 200, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "for", "start_ms": 330, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "you.", "start_ms": 420, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "(instrument)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 31,
"transcript": "고맙습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "고맙습니다", "start_ms": 30, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "Thank you.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "Thank", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "you.", "start_ms": 165, "dur_ms": 100, "phase_lr_deg": 0.0}
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
"transcript": "다음에 또 만나요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 35, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 195, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 305, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "See you again soon.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 150, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "again", "start_ms": 240, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "soon.", "start_ms": 370, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 37,
"transcript": "오늘의 공부는 여기까지입니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "오늘의", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "공부는", "start_ms": 200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "여기까지입니다", "start_ms": 360, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "That's all for today's lesson.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "That's", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "all", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "for", "start_ms": 250, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "today's", "start_ms": 340, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "lesson.", "start_ms": 500, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "수고 많으셨습니다. 안녕히 계세요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "수고", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "많으셨습니다.", "start_ms": 150, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 410, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "계세요.", "start_ms": 570, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "You've done a great job. Goodbye.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "You've", "start_ms": 55, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "done", "start_ms": 165, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 255, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "great", "start_ms": 315, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "job.", "start_ms": 445, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "Goodbye.", "start_ms": 555, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 42,
"transcript": "언제나 여러분을 응원합니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "언제나", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "여러분들을", "start_ms": 200, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "응원합니다", "start_ms": 390, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "We are always rooting for you.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "We", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "are", "start_ms": 140, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "always", "start_ms": 230, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "rooting", "start_ms": 390, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "for", "start_ms": 520, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "you.", "start_ms": 610, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "함께해주셔서 감사합니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "함께해주셔서", "start_ms": 30, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 290, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "Thank you for joining us.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Thank", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 170, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "for", "start_ms": 260, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "joining", "start_ms": 350, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "us.", "start_ms": 510, "dur_ms": 100, "phase_lr_deg": 0.0}
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
"transcript": "다음에 또 뵙겠습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 35, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 195, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "뵙겠습니다", "start_ms": 305, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "I'll see you again.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "I'll", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "see", "start_ms": 130, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 220, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "again.", "start_ms": 310, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "고맙습니다. 안녕히 가세요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "고맙습니다.", "start_ms": 45, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 255, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가세요.", "start_ms": 415, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 05:09:40*
