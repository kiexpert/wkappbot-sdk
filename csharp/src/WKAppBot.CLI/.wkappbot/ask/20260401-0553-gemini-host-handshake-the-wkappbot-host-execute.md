# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=14524  time=05:51:38  uptime=7689s  mem=2396MB  system_processes=853"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260401_055129.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 730-763-736-760-763-675-672-760-762-706-765-760-762-670-672-673-673-564-574 631-637_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 756-570-762-672-362-362-312-326-321-362-136-760-672-675-607-603-637-706-317-103-132-123-260-213-203-326-126-123-326-627-174-173-123-503-573-137-301-132-236-216-132-102-130-132-1_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 216-321-576-261-613-632-236-627-625-672-625-276-276-236-621-362 301-237-236_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 613-621-612-623-571-752-163-160-130-137-103-157-165-163-316-103-326-136-162 672-127-627-627-612-630-613-306-103-132-302-301-362-123-160-103-103-103-163-176-765-156-136-125-106-3_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 276-726-265-237-327-623-123-162-162-617-162-237-237-276-236-231-217-276-276-270-706-723-276-237-236-263-236-263-260-206-276-207-372-321-137-312-321-321-132-132-312-213-231-237-1_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 357-357-357-573-573-357-157-571-357-573-251-265-236-217-237-317-257-235-235-237-257-417-276-671-752-175-261-236-276-257-752-715 361-326-527-752-275-725-276-276-672-653-567-257-6_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 761-671-271-726-726-276-723-276-276-712-762-761-726-726-672-276-167-162-167-126-617-126-172-120-132-216-210-126-216-210-210-216-236-261-263-231_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 623-162-312-231-203 310-326-123-513-510_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 130-301-316-106-162-613-132-123-103-132-163-160-617-137-675-567-760-167-675-675-765-570-657-306-257-263-623-632-126-135-136-167-106-130-321-230-236-236-260-523-210-203-320-620-6_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 765-657-507-107-157-615-567-657-617-173-106-167-106-106-107-176-756-163-164-754-206-527-534-163-632-672-632-637-231-362-623-631-672-627-632-623-267-627-132-362-236-263-326-627-2_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 620-201-206-206-261-102-125-562-126-261-173-216-261-216-137-173-761-206-236-320-326-236-136-167-751-760-576-675-657-625-167-612-106-570-751-765-160-762-627-765-756-765-675-136-3_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 621-176-361-301-301-301-317-312-312-563-126-673-136-136-571-316-163-103-132-165-165-162-132-103-176-106-130-165-326-135-167-136-136-163-167-167-106-106-103-103-615-671-751-615-2_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 731-713-765-723-136-132-103-103-620-547-571-507-756-756-756-751-576-657-576-356-136-310-675-670-360-103 136-132-103-160-601-617-160-176-307-167-103-136-135-126-136-736-706-326-3_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 267-267-267-162-123-312-312 125-162-135-326-136-163-163-162-103-602-671-627-132-607-670-103-173-165_V.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 567-765-621-657-716-175-675-612-136-671-617-671-632-236-123-263-216-123-627-163-261-321-613-671-672-162-316-167-617-567-265-627-672-621-567-657-621-126_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 675 765-762-675-675-675-756-576-576-574-574-574-574-576-132-216-632-765-736-607-672-617-673-657-657-573-576-567-136-365-316-163 572-756-623_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 132-136-326-312-123-123-123-120-123-213-216-123-217-132-123-132-312-167-136-132-167-612-167-756-137-125-123-123-271-217-217-213-312-217-257-127-172-312-123-127-137-310-120-132 1_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 263-206-236-602-657-103-710-167-160-231-301-230-263-236-206-102-236-320-302-361-213-102-102-261-263-326-621-236-261-326-231-102-120-123-120-106-162-632-162-102-362-306-163-213-3_V.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 372-567-635-163-162-362-376-736-372-372-574-573-653-763-371-321-213-261-127-715-731-751-752-725-376-756-137-123-136-567-576-567-567-576-574-567-657-627-657-627-236-632-367-627-6_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 623-621-750-754-735-213-316-765-765-765-715-716-723-637-236-367-623-672-365 574-574-357-321-315-312-317-316-361-316-312-136-316-413-561-657-617-127-142 275-327-376-716-175-716-1_V.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 652-167-103-261-136-103-163-106-752-162-612-162-326-613 136-134-136-312-136-671-167-176-103-132-603-602-103-126-136-136-160-613-326-102-167-316-163-136-136-106-756-657-136-615-1_V.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 657-675-675-673-675-675-765-765-657-756-675-675-765-675-176-127-617-673-617-627-362-623-632-632-617-613-612_W.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 615-576-327-657-627-675-627-167-675-675-236-267-627-627-675-756-276-236-136-123-617-163-632-671-623-623-672-123-316-162-162-162-613-615-763-657-765-675-657-567-172-715-735-765-2_W.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 632-716-617-301-130-673-761-712-107-130-321-237-127-136-106-170-761-706-170-610-107-103-631-165-136-637-136-316-103-576-103 267-136-350-672-103-316-123-162-651-621-361-675-132-1_W.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 263-326-621-612-672-263-236-672-631-263-361 263-263_W.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 136-716 103-312-675-672-623-621-231-132-165-157-672-256-652-256-652-620-612-126_W.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 612-236 123-265-756-301-361-103-103-103-301-361-312-675-361-163-316-136-301-362-761-726-627-630-310-302-301-301-132-103-231-206-350-357-702-360-263-627-632-136-613-657-261-362-5_W.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 106-362-602-316-263-126-261-621-632-267-123-263-620-620-620-602-612-120-160-206-130-261-162-602-621-236-216-163-760-126-267-612-601-706-576-150-765-617-617-612-621-620-620-160-1_V.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 103-302-213-302-312-302-321-361-631-613-163-613-613-106-132-120-321-231-321-612-671-627-106-103-670-672-607-607-750-726-376-760-267-263 731-170-623-321-213-216-326-321 671-670-6_W.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 106-103 132-106-167-103-607-672-627-602-672-617-602-162-127-672-603-507-563-136-620-201-132-206-260-670-670-610-623-260-206-620-162-162-302-206-563-326-326-132-302-310 612-162-3_V.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 621-765-716-167-106-165-560-106-672-172-612-761-136-563-156-657-103-106-143-152-756-215-312-635-126-216-106-136-102-123-136-263-231-621-126-263-162-126-126-136-706-123-251-160-1_V.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 620-256-567-567-657-672-627-107-301-127-167-130-130-163-176-614-106-167-167-167-167-756-657-362-632-236-306-260-216-216-576-657-675-263-623-106-106-136-103-103-106-173-127-103-7_V.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 356-537-102-310-132-130-123-351-531-365-567-563-652-657-567-567-561-536-536-365-567-567-567-567-657-561-657-567-657-651-156-516-657-657-567-503-305-653-506-567-563-563-135-153-5_W.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 132-103-163-162-136-301-103-231-302-130-103-260-576-576-321-327-236-725-276-360-320-130-103-130-130-547-574-574-130 106-670-310-167-106-167-106-317-130-167-107-307-231-206-260-2_W.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 236-326-256-236-263-236-326-652-263-267-632-625-276-632-263-623-263-263-267-263-236-637-602-620-132-362-236-326-132-621 760-167-617-267-613-160-106-136-132-163-167-165-361-103-1_W.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 163-623-367-163-267-237-236-231-725-625-625-631-321-362-562-523-612-163-213-621-321-326-236-326-321-326-326-326-236-163-236-326-326-123-120-231-126-231-236-152-527-756-276-637-2_W.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 625-156-362-130-132-130-106-613-160-167-216-156-130-163-162-132-620-162-136-163-260-156-163-130-103-107-106-103-103-106-136-260-162-562-261-267-672-276-267-132-143-136-132-175-1_V.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 267-216-137-167-371-137-671-765-735-753-754-756-754-103 231-671-632-617-130-103-263-263-231-236-231-213-263-213-527-137-107-103-136-106-163-675-675-620-621-265-267-260-201-213-2_W.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 316-132 103-167-165-167-163-167-167 251-256-257-672-521-627-361-132-162-176-126-126-625-256-253-216-617-576-576-627-672-675-312-301-630-632-623-632-613-612-637-631-612-621-610-1_V.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 320-302-103-132 532-603-631-603-261-230-562-123-173-263-326-207-207-207-315-623-302-310-623-263-620-320-230-267-203-263-620-520-253-132-736-762-162-102-621-621-601-172-751-263-2_W.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 623-613-627-632-326-627-627-627-627-672-675-672-765-726-672-326-762-765-765-627-627-627-621 312-125-134-173-132-632-361-362-613-216-236-623 675-765-675-765-627-672-632-326-326-3_W.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 163-136-312-321-132-316-136-132-561-671-610-103-103-107-125-671-627-627-236-326-132-613-132-162-136-213-316-132-163-106-156-671-675-176-136-231-167-165-765-103-165-326-163-601-1_V.mp3)
  Segment 43: 66192ms ~ 66768ms (file: 320-632-162-126-162-623-621-671-130-132-123-160-130-263-612-612-126-130-106-162-120-231-623-123-130-316-725-621-165-630-210-612-136-312-260-672-260-120-123-715-123-612-610-236-3_V.mp3)
  Segment 44: 67768ms ~ 68344ms (file: 516-576-652-576-571-615-617-156-521-567-156-561-567-167-651-361-531-675-675-367-576-657-537-675-765-576-753-571-563-153-156-157-516-156-135-156-165-516-513-517-517-156-156-517-5_W.mp3)
  Segment 45: 69344ms ~ 69920ms (file: 132-275-612-671-136-132-134-132-162-613-603-617-765-761-657-607-567-312-273-215-123-216-306-236-261 236-625-652-652-612-316-623 132-167-607-316-765-610-316-163-103-562-165-162-1_W.mp3)
  Segment 46: 70920ms ~ 71496ms (file: 160-136-236-132 765-675-267-312-132-103 576-765 567-567-657-756-763-163_W.mp3)
  Segment 47: 72496ms ~ 73072ms (file: 372-136-316-631-316-136-310-362-316-612-106-132-172-126-216-126-237-235-137-325-527-125-561-576-561-567-657-567-562-156-652-562-652-512-156-167-106-120-263 657-165 516-657-312-3_W.mp3)
  Segment 48: 74072ms ~ 74648ms (file: 671-672-671-671-671-671-610-160-706-637-706-361-673-376-760-316-316-132-312-316-321-160-136-106-675-675-672-625-621-615-126-756-103-103-103-103-135-102 127-103-103-615-163-165-1_V.mp3)
  Segment 49: 75648ms ~ 76224ms (file: 326-316-312-321-326-316-321 367-312-361-267_W.mp3)
  Segment 50: 77224ms ~ 77800ms (file: 657-760-675-706-765-675-306-603-360-316-136-316-136-361-163-632-321-612-136-163-621-651-607-607-167-167-756-675-675-260-263-236 756-670-672-670-756-657-675-765-756-310-312-317-7_V.mp3)
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
> **Time**: 2026-04-01 05:53:13
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "어떤 운동을 좋아하시나요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "어떤", "start_ms": 110, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "운동을", "start_ms": 220, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "좋아하시나요?", "start_ms": 360, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "What kind of sports do you like?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 95,
"audio_type": "speech",
"words": [
{"word": "What", "start_ms": 95, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "kind", "start_ms": 185, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "of", "start_ms": 285, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "sports", "start_ms": 335, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "do", "start_ms": 435, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 495, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "like?", "start_ms": 545, "dur_ms": 25, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "저는 축구를 좋아해요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 125,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 125, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "축구를", "start_ms": 245, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "좋아해요.", "start_ms": 405, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "I like soccer.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 105,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 105, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "like", "start_ms": 195, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "soccer.", "start_ms": 325, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "축구 경기를 자주 보나요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 135,
"audio_type": "speech",
"words": [
{"word": "축구", "start_ms": 135, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "경기를", "start_ms": 255, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "자주", "start_ms": 385, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "보나요?", "start_ms": 485, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "Do you watch soccer matches often?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "Do", "start_ms": 110, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 180, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "watch", "start_ms": 240, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "soccer", "start_ms": 350, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "matches", "start_ms": 450, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "often?", "start_ms": 530, "dur_ms": 40, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "네, 주말마다 챙겨 봐요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 140, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "주말마다", "start_ms": 210, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "챙겨", "start_ms": 370, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "봐요.", "start_ms": 480, "dur_ms": 85, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "Yes, I watch them every weekend.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "Yes,", "start_ms": 120, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 210, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "watch", "start_ms": 280, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "them", "start_ms": 380, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "every", "start_ms": 440, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "weekend.", "start_ms": 520, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "직접 하는 것도 좋아하나요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "직접", "start_ms": 130, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 250, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "것도", "start_ms": 350, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "좋아하나요?", "start_ms": 440, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "Do you also like playing it yourself?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 115,
"audio_type": "speech",
"words": [
{"word": "Do", "start_ms": 115, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 185, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "also", "start_ms": 245, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "like", "start_ms": 335, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "playing", "start_ms": 415, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "it", "start_ms": 515, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "yourself?", "start_ms": 565, "dur_ms": 10, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "네, 친구들과 가끔 축구를 해요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 145,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 145, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "친구들과", "start_ms": 215, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "가끔", "start_ms": 355, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "축구를", "start_ms": 455, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "해요.", "start_ms": 545, "dur_ms": 30, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "Yes, I sometimes play soccer with my friends.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "Yes,", "start_ms": 120, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 210, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "sometimes", "start_ms": 270, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "play", "start_ms": 390, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "soccer", "start_ms": 470, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "with", "start_ms": 540, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "my", "start_ms": 571, "dur_ms": 4, "phase_lr_deg": 0.0},
{"word": "friends.", "start_ms": 576, "dur_ms": 0, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "다른 운동은요?",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 160,
"audio_type": "speech",
"words": [
{"word": "다른", "start_ms": 160, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "운동은요?", "start_ms": 280, "dur_ms": 285, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "What about other sports?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 125,
"audio_type": "speech",
"words": [
{"word": "What", "start_ms": 125, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "about", "start_ms": 225, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "other", "start_ms": 335, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "sports?", "start_ms": 435, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "수영도 조금 할 줄 알아요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "수영도", "start_ms": 140, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 300, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 400, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "줄", "start_ms": 460, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "알아요.", "start_ms": 510, "dur_ms": 55, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "I can swim a little too.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 110, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "can", "start_ms": 180, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "swim", "start_ms": 280, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 400, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "little", "start_ms": 450, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "too.", "start_ms": 550, "dur_ms": 20, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "수영을 언제 배웠나요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "수영을", "start_ms": 150, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "언제", "start_ms": 300, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "배웠나요?", "start_ms": 420, "dur_ms": 145, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "When did you learn to swim?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "When", "start_ms": 120, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "did", "start_ms": 230, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 310, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "learn", "start_ms": 380, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 480, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "swim?", "start_ms": 530, "dur_ms": 40, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "어렸을 때 배웠어요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "어렸을", "start_ms": 140, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 300, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "배웠어요.", "start_ms": 370, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "I learned it when I was a child.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 110, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "learned", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "it", "start_ms": 290, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "when", "start_ms": 350, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 430, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "was", "start_ms": 480, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 540, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "child.", "start_ms": 571, "dur_ms": 4, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "지금도 수영장에 자주 가나요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 135,
"audio_type": "speech",
"words": [
{"word": "지금도", "start_ms": 135, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "수영장에", "start_ms": 285, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "자주", "start_ms": 415, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "가나요?", "start_ms": 505, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "Do you still go to the swimming pool often?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 115,
"audio_type": "speech",
"words": [
{"word": "Do", "start_ms": 115, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 185, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "still", "start_ms": 245, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "go", "start_ms": 335, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 405, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 455, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "swimming", "start_ms": 505, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "pool", "start_ms": 570, "dur_ms": 5, "phase_lr_deg": 0.0},
{"word": "often?", "start_ms": 576, "dur_ms": 0, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "아니요, 요즘은 바빠서 잘 못 가요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "아니요,", "start_ms": 140, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "요즘은", "start_ms": 240, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "바빠서", "start_ms": 360, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 470, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "못", "start_ms": 520, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "가요.", "start_ms": 560, "dur_ms": 10, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "No, I'm too busy to go these days.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "No,", "start_ms": 120, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "I'm", "start_ms": 210, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "too", "start_ms": 280, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "busy", "start_ms": 350, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 450, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "go", "start_ms": 500, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "these", "start_ms": 550, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "days.", "start_ms": 571, "dur_ms": 4, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "운동하는 시간을 늘려보세요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "운동하는", "start_ms": 150, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "시간을", "start_ms": 290, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "늘려보세요.", "start_ms": 420, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "Try to increase your exercise time.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "Try", "start_ms": 110, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 190, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "increase", "start_ms": 240, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "your", "start_ms": 360, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "exercise", "start_ms": 440, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "time.", "start_ms": 540, "dur_ms": 30, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "네, 그래야겠어요. 건강이 중요하니까요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 140, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "그래야겠어요.", "start_ms": 210, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "건강이", "start_ms": 370, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "중요하니까요.", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "Yes, I should. Because health is important.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "Yes,", "start_ms": 120, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 210, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "should.", "start_ms": 270, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "Because", "start_ms": 370, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "health", "start_ms": 470, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 540, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "important.", "start_ms": 571, "dur_ms": 4, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "산책은 어때요? 매일 할 수 있잖아요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "산책은", "start_ms": 130, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "어때요?", "start_ms": 240, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "매일", "start_ms": 340, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 430, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 480, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "있잖아요.", "start_ms": 520, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "How about taking a walk? You can do it every day.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "How", "start_ms": 110, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "about", "start_ms": 190, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "taking", "start_ms": 280, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 390, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "walk?", "start_ms": 430, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "You", "start_ms": 530, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "can", "start_ms": 570, "dur_ms": 5, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "좋은 생각이에요! 오늘부터 시작할게요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 140, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "생각이에요!", "start_ms": 250, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "오늘부터", "start_ms": 390, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "시작할게요.", "start_ms": 510, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "Great idea! I'll start today.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 115,
"audio_type": "speech",
"words": [
{"word": "Great", "start_ms": 115, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "idea!", "start_ms": 205, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "I'll", "start_ms": 305, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "start", "start_ms": 375, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "today.", "start_ms": 495, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "꾸준히 하는 게 제일 중요해요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "꾸준히", "start_ms": 130, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 260, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 350, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "제일", "start_ms": 400, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "중요해요.", "start_ms": 500, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "Consistency is the most important thing.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "Consistency", "start_ms": 100, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 290, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 350, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "most", "start_ms": 400, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "important", "start_ms": 490, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "thing.", "start_ms": 571, "dur_ms": 4, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "맞아요. 같이 힘내봐요!",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "맞아요.", "start_ms": 150, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 270, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "힘내봐요!", "start_ms": 370, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "Right. Let's cheer up together!",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "Right.", "start_ms": 120, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "Let's", "start_ms": 230, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "cheer", "start_ms": 330, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "up", "start_ms": 430, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "together!", "start_ms": 500, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "함께 운동하면 더 즐거울 거예요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "함께", "start_ms": 140, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "운동하면", "start_ms": 260, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 400, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "즐거울", "start_ms": 450, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "거예요.", "start_ms": 540, "dur_ms": 30, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "It will be more fun to exercise together.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "It", "start_ms": 110, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "will", "start_ms": 160, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "be", "start_ms": 220, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "more", "start_ms": 270, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "fun", "start_ms": 350, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 420, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "exercise", "start_ms": 470, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "together.", "start_ms": 571, "dur_ms": 4, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "내일 아침에 같이 공원 가실래요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "내일", "start_ms": 130, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "아침에", "start_ms": 240, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 360, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "공원", "start_ms": 450, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "가실래요?", "start_ms": 540, "dur_ms": 30, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "Would you like to go to the park together tomorrow morning?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 105,
"audio_type": "speech",
"words": [
{"word": "Would", "start_ms": 105, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 195, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "like", "start_ms": 255, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 335, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "go", "start_ms": 385, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 445, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 485, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "park", "start_ms": 525, "dur_ms": 40, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 42,
"transcript": "좋아요. 7시에 만나요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 145,
"audio_type": "speech",
"words": [
{"word": "좋아요.", "start_ms": 145, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "7시에", "start_ms": 285, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "만나요.", "start_ms": 445, "dur_ms": 125, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "Sounds good. Let's meet at seven.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "Sounds", "start_ms": 120, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "good.", "start_ms": 230, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "Let's", "start_ms": 330, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "meet", "start_ms": 430, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "at", "start_ms": 520, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "seven.", "start_ms": 571, "dur_ms": 4, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "그때 봐요. 좋은 저녁 보내세요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "그때", "start_ms": 130, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "봐요.", "start_ms": 230, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 340, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "저녁", "start_ms": 430, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "보내세요.", "start_ms": 520, "dur_ms": 55, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "See you then. Have a good evening.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 110, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 200, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "then.", "start_ms": 270, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "Have", "start_ms": 370, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 460, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "good", "start_ms": 500, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "evening.", "start_ms": 571, "dur_ms": 4, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "내일 봐요!",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "내일", "start_ms": 150, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "봐요!", "start_ms": 270, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "See you tomorrow!",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 120, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 220, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "tomorrow!", "start_ms": 290, "dur_ms": 285, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "안녕히 계세요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 140, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "계세요.", "start_ms": 300, "dur_ms": 270, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "Goodbye.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "Goodbye.", "start_ms": 130, "dur_ms": 440, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "잘 가요!",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "잘", "start_ms": 150, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "가요!", "start_ms": 270, "dur_ms": 300, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-01 05:53:13*
