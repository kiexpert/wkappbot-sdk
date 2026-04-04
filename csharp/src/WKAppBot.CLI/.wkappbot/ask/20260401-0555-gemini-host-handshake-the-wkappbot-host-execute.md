# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=14524  time=05:53:41  uptime=7811s  mem=2228MB  system_processes=860"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260401_055331.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 612-672-136-312-765-716-167-216-613-167-671-765-236-127-120-302-362-316-301-632-126-123-132-123-162-132-126-651-621-163-136-102-103-123-301-130-310-103-136-310-632-602-562-357-3_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 230-102-312-123-621-163-136-316-312-312-236-602-620-621-236-235-301-203-326-321-320-301-321-370-316-213-261-206-102-103-123-163-263-312-130-301-321-230-230-302-321-236-574-256-3_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 576 231-321-261-231-261-126-216-175-617 672-162-765-675-675-756-167-261-261-162-615-126-672-762-762-607-765-672-723-765-765-621-627-263-312-627-621-176-167-671-602-627-632-623-2_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 627-106-173-613-206-123-123-312-103-263-601-263-126-216-263-156-621-376-126-103-103-657-362-312-310-103-106-162-263-163-564-326-165-312-106-316-106-136-561-572-162-760-167-106-1_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 567-567-652-576-756-576-756-625-275-725-576-527-576-572-574-576-312-312-312-231-351-351-215-516-256-321-321-321-362-321-312-321-321-315-312-321-312-315-315-315-321-325-321-321-3_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 572-572-537-517-523-561-651-365-573-352-235-216-162-567-564-564-652-256-623-623-625-632-257-652-265-526-256-257-325-540-152-137-371-137-271-271-267-275-231-567-567-564-536-576-5_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 276-267-623 263-632-263-263-267-623-263_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 657-317-615-716-173-126-263-362-312-361-312-632-657-326-625-613-263-623-621-613-361-163-621-163-316-362-163-652-613-167-360-621-652-362-310-632-312-136-253-362-103-612-316-163-6_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 567 263-261-263-267-263-362-612-672-623 261-623-213-321 263-261-312-261-236-621-216-327-267-621-326-326-236-263-621-362-361-632-623-263-326-321-263-130-301-310-306-602-361-507-3_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 267-261-236-326-216-562-652-615-657-675-736-637-236-267-236-576-275-236-236-265-765-726-732-136-167-136-132-126-567-567-756-657-267-621-657-567-652-267-261-576-756-213-312-132-3_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 156-517-163-157-165-517-365-153-165-136-135-361-153-516-567-135-132-125-235-236-231-265-256-671-156-175-156-356-165-351-751-561-576-571-567-567-516-165-615-165-613-567-615-571-7_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 312-213-132-132-526-571-135-231-123-132-136-126-132-136-316-132-312-132-132-315-153-135-126-162-126-136-132-123-132-136-132-136-135-517-157-156-157-127-316-173-136-132-326-312-3_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 627-627-627-236-367-637-637-365-635-635-635-635-635-635-635-365-365-365-365-365-762-672-672-672-673-637-573-574-576-752-765-672 547-574-576-576-546-540-540-547-574-753-175-301-3_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 651-164-106-130-163-621-362-573-576-576-763-675-763-765-216-761-562-631-106-103-675-632-162-160-136-103-126-561-512-106-163-103-106-621-176-620-621-621-612-132-276-176-751-716-1_V.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 261-361-631-613-130-326-316-621-263-620-306-275-652-627-265-623-312-631-230-216-623-230-216-162-263-362-261-762-136-213-276-123-167-136-631-602-160-316-301-156-125-326-326-236-3_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 605-612-132-162-623-236-106-103-103-136-105-105-107-135-106-103-301-130-103-261-213-120-123-103-102-106-103-103-203-231-321-356-506-123-103-321-316-562-261-712-312-103-316-316-1_V.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 130-132-160-365-136-321-361-671-167-162-120-316-167-516-107-167-106-167-167-106-631-167-561-201-561-621-160-167-106-617-163-106-160-106-106-136-106-165-671-167-716-106-176_V.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 106-165-123-105-136-103-136-306-306-103-632-627-670-672-657-615-106-103-105-531-360-302-312-276-576-621-621-106-106-301-156-103-130-360-136-371-362-132-362-130-157-136-137-276-2_V.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 651-615-132-103-123-132-617-560-165-156-130-627-130-103-123-130-130-657-326-231-361-301-316-506-675-162-103-136-103-103-132-601-627-136-126-765-167-206-167-126-163-162-103-106-1_V.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 673-672-675-276-567-675-675-675-675 765-765-725 163-132-120-576-705-173-213-132-106-123-236-263-136-123-301-126-316-301-362-136-160-316-356-532-312-123-132-126-236-607-123-216-3_W.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 237-103-132-102-126-136-126-163-167-601-126-120-621-261-267-260-167-301-302-736-601-621-106-136-163-106-167-167-123-127-126-162-612-621-126-162-102-321-136-103-103-163-106-163-1_V.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 732-163-762-627-671-271-675-367-316-631-163-631-672-612-617-163-617-671-617-316-756-752-752-725-752-725-752-756-752-752-756-752-765-762-621-123-576-257-253-215-213-271-213-216-2_W.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 132-167-162-130-136-316-610-165-165-130-376-306-317-103-301-602-623-362-130-136-316-103-356-162-130-103-123-176-276-725-521-165-126-132-610-657-657-562-567-567-652-312-136-162 2_V.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 102-130-675-135-176-675-106-103-106-617-132-602-617-127-165-607-167-106-605-167-136-163-160-167-106-103-167-160-103-621-623-621-315 167-317-163-106-167-153-367-672-637-325-123-6_V.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 315-253-217-275-271-675-762-723-367-756-761-763-562-136-367-357-126-152-563-526-163-153-152-153-231-362-321-126-165-167-163-165-613-163-365-615-361-165-367-365-637-361-136-167-5_W.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 621 145-103-316-130-613-103-231-372-126-136-153-136-126-612-651-631-603-263-673 162-612-102-162-103-136-167-615-760-130-610-106-136-103-671-132-103-162-165-120-617-165-103-136-1_V.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 635-617-675-657-653-160-276-675-672-261-631-625-625-627-735-762-672-126-136-562-352-572-356-135-765-657-675-657-613-623-173-607-206-102-126-102-361-163-261-261-312-231-132-102-2_W.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 612 263-206-230-236-163-163_W.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 651-652-620-123-106-102-213-231-301-306-123-136-132-103-132-675-675-620-260-756-701-715-107-170-671-617-607-301-302-206-206-203-320-623-261-301-123-132-106-103-106-136-203-320-6_V.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 302-302-136-570-165-316-103-206-261-103-236-326-320-230-620-230-263-206-230-236-160-167-623-261-361-670-360-316-326-316-103-136-310-136-574-675-670-716-621-310-312-610-260-260-6_V.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 536-653-653-163-156-567-561-365-715-517-561-561-562-512-651-651-576-561-567-651-631-562-563-561-652-657-651-635-365-356-652-561-652-156-567-657-651-657-563-531-567-657-567-567-7_W.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 210-321-173-763-736-275-521-572-572-275-276-257-257-267-256-325-257-527-257-125-215-256-216-326-352-352-352-325-253-257-257-205-205-250-205-532-756-257-256-257-250-253-524-540-5_W.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 103-316-301-316-316-315-314-324-567-214-325-321-637-361 675-675-673-617-621-132 172-102-137-106_V.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 275-275-526-652-506-506-657-675-607 635-673_W.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 147-123-106-103-136-135-560-103-567-652-123-635-637-136-106-136-165-165-561-325-163-136-132-103-627-261-167-136-163-612-613-103-132-621-267-651-136-136-130-136-130-103-103-153-1_V.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 517-537-523-531-315-327-362-326-632-517-532-547-251-574-574-571-570-532-532-267-623-236-236-361-561-532-317-216-327-325-315-325-315-623-362-362-632-367-637-627-632-672-267-267-2_W.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 132-617-617-675-576-675-765-675-765-517-675-765-761-167-657 627-627-627-672-162-217-172-761-172-726-617-712-726-721-761-726-761-762-726-761-762-761-716-761-762-752-752-765-756-7_W.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 632-632-657-361-362 276-623-236-675-627-263-326-623-326-632-627-627-362-321 672-672-267-627-623-625-267-632-627-267-765-675-576-756-760 765-762-762-726-267-657-267_W.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 136-176-310-136 236-321-632-631-326-327-763-576 326-236-263-263-236-263-162-627-623-625-267-620-167-162-130-301-103-312-126-126-106-617-610-617-106-163-130-167-107-153-576-105-6_V.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 176-162-106-132-153-130-135-576-576-756-756-576-576-756-567-576-756-670-567-576-567-612-517-571-517-576-317-537-540-763-576-576-762-675-546-574-576-756-617-136-546-547-574-574-5_W.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 236-631-236-540-514-103-137-705-107-135-163-136-173-132-135-612-671-126-103-163-617-152-612-261-617-106-106-165-132-316-136-165-137-326-261-613-136-136-126-765-562-627-613-612-7_W.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 372-672-261-712-723-216-326-627-162-160-103-216-136-261-160-126-361-216-103-621-106-167-620-623-216-163-103-631-136-765-567-126-201-132-136-317-103-152-617-163-132-132-106-136-1_V.mp3)
  Segment 43: 66192ms ~ 66768ms (file: 103-765-765-716-163-361-123-163-631-136-621-621-136-136-106-675-126-163-136-176-132-652-316-163-167-567-167-621 617-106-103-132-316-613-136-123-102-321-612-126-136-106-136-167-1_V.mp3)
  Segment 44: 67768ms ~ 68344ms (file: 132-316-617-106-106-103-106-167-106-601-610-601-612-613-312-136-362-670-601-627-136-130-130-612-163-106-106-143-132 162-163-103-310-132-134-103-130 623-263-627-607-620-625-126-1_V.mp3)
  Segment 45: 69344ms ~ 69920ms (file: 601-136-670-316-316-623-163-165-362-567-312-163-617-136-631-576-265-516-136-163-176-123-312-137-103-135-163-756-135-136-107-617-261-216-120-162-132-216-561-615-137-173 263-657-6_V.mp3)
  Segment 46: 70920ms ~ 71496ms (file: 670-670-627-672-231-570-571-203-750-675-675-726-217-526-671-673-607-672-236-306-360-302-326-306-263-263-573-206-263-206-106-132-621-310-310-310-123-103-102-132-361-132-136-126-1_V.mp3)
  Segment 47: 72496ms ~ 73072ms (file: 321-312-326-132-672-672-625-675-162-671-362-123-763-657-362-136-576-576-570-672-627-672-267-137-312-132-571-573-310-576-567-365-375-657-657-726-657-670-763-132-312-132-312-213-2_W.mp3)
  Segment 48: 74072ms ~ 74648ms (file: 316-312-310-301-102-765-312-230-236-231-213-216-236-762-760-301-102-162-106-103-106-263-231-216-263-120-162-213-263-312-160-106-163-201-310-301-301-326-320-326-213-201-301-302-3_W.mp3)
  Segment 49: 75648ms ~ 76224ms (file: 570-756-507-756-750-570-507-267-276-576-657-706-670-362-675-623-621-672-617 637-163-362-613-236-621-632-132-321-316-136-632-265-267-675-623-326-167-672-621-623-657-612-651-631-6_W.mp3)
  Segment 50: 77224ms ~ 77800ms (file: 163-123-167-163-162-105-612-136-613-103-102-136-157-127-365-316-163-561-123-351-310-163-163-103-136-103-632-620-612-106-136-103-136-103-135-165-136-617-163-167-316-356-712-136-1_V.mp3)
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
> **Time**: 2026-04-01 05:55:13
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "주말에 시간이 어떻게 되나요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "주말에", "start_ms": 110, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "시간이", "start_ms": 250, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 370, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "되나요?", "start_ms": 480, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "What is your schedule for the weekend?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 95,
"audio_type": "speech",
"words": [
{"word": "What", "start_ms": 95, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 175, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "your", "start_ms": 225, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "schedule", "start_ms": 295, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "for", "start_ms": 425, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 485, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "weekend?", "start_ms": 535, "dur_ms": 35, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "토요일 오후에 영화를 보러 가요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "토요일", "start_ms": 120, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "오후에", "start_ms": 270, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "영화를", "start_ms": 390, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "보러", "start_ms": 500, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "가요.", "start_ms": 560, "dur_ms": 15, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "I am going to see a movie on Saturday afternoon.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 105,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 105, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "am", "start_ms": 155, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "going", "start_ms": 205, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 285, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "see", "start_ms": 325, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 395, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "movie", "start_ms": 425, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "on", "start_ms": 495, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "Saturday", "start_ms": 535, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "afternoon.", "start_ms": 570, "dur_ms": 5, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "일요일에는 무엇을 하나요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "일요일에는", "start_ms": 140, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "무엇을", "start_ms": 310, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "하나요?", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "What do you do on Sunday?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "What", "start_ms": 110, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "do", "start_ms": 200, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 270, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "do", "start_ms": 330, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "on", "start_ms": 410, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "Sunday?", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "집에서 푹 쉴 거예요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 135,
"audio_type": "speech",
"words": [
{"word": "집에서", "start_ms": 135, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "푹", "start_ms": 285, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "쉴", "start_ms": 355, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "거예요.", "start_ms": 435, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "I will take a good rest at home.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 115,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 115, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "will", "start_ms": 175, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "take", "start_ms": 245, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 325, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "good", "start_ms": 365, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "rest", "start_ms": 435, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "at", "start_ms": 515, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "home.", "start_ms": 565, "dur_ms": 10, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "좋은 계획이네요! 월요일에 봐요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 125,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 125, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "계획이네요!", "start_ms": 215, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "월요일에", "start_ms": 375, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "봐요.", "start_ms": 505, "dur_ms": 65, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "That's a good plan! See you on Monday.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "That's", "start_ms": 100, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 200, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "good", "start_ms": 250, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "plan!", "start_ms": 340, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "See", "start_ms": 430, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 510, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "on", "start_ms": 570, "dur_ms": 1, "phase_lr_deg": 0.0},
{"word": "Monday.", "start_ms": 575, "dur_ms": 1, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "네, 즐거운 주말 보내세요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 130, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "즐거운", "start_ms": 210, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "주말", "start_ms": 330, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "보내세요.", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "Yes, have a nice weekend.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 105,
"audio_type": "speech",
"words": [
{"word": "Yes,", "start_ms": 105, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "have", "start_ms": 195, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 305, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "nice", "start_ms": 355, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "weekend.", "start_ms": 455, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "감사합니다. 안녕히 가세요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "감사합니다.", "start_ms": 140, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 310, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "가세요.", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "Thank you. Goodbye.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "Thank", "start_ms": 110, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "you.", "start_ms": 230, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "Goodbye.", "start_ms": 320, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "다음 주에 또 만나요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "다음", "start_ms": 130, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "주에", "start_ms": 250, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 350, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "만나요.", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "See you again next week.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 100, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 200, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "again", "start_ms": 270, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "next", "start_ms": 390, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "week.", "start_ms": 500, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "공부가 잘 되어가나요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 145,
"audio_type": "speech",
"words": [
{"word": "공부가", "start_ms": 145, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 295, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "되어가나요?", "start_ms": 365, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "Is your study going well?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "Is", "start_ms": 110, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "your", "start_ms": 170, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "study", "start_ms": 250, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "going", "start_ms": 370, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "well?", "start_ms": 480, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "네, 하지만 조금 어려워요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 120, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "하지만", "start_ms": 200, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 340, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "어려워요.", "start_ms": 450, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "Yes, but it's a bit difficult.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 95,
"audio_type": "speech",
"words": [
{"word": "Yes,", "start_ms": 95, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "but", "start_ms": 185, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "it's", "start_ms": 275, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 355, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "bit", "start_ms": 395, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "difficult.", "start_ms": 475, "dur_ms": 95, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "어떤 부분이 어려운가요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 135,
"audio_type": "speech",
"words": [
{"word": "어떤", "start_ms": 135, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "부분이", "start_ms": 255, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "어려운가요?", "start_ms": 385, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "Which part is difficult?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "Which", "start_ms": 110, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "part", "start_ms": 230, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 340, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "difficult?", "start_ms": 410, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "단어 외우는 게 힘들어요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "단어", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "외우는", "start_ms": 280, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 400, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "힘들어요.", "start_ms": 450, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "It's hard to memorize words.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 115,
"audio_type": "speech",
"words": [
{"word": "It's", "start_ms": 115, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "hard", "start_ms": 205, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 315, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "memorize", "start_ms": 375, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "words.", "start_ms": 515, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "포기하지 말고 계속해 보세요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "포기하지", "start_ms": 120, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "말고", "start_ms": 280, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "계속해", "start_ms": 380, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "보세요.", "start_ms": 510, "dur_ms": 65, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "Don't give up and keep going.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 95,
"audio_type": "speech",
"words": [
{"word": "Don't", "start_ms": 95, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "give", "start_ms": 195, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "up", "start_ms": 285, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "and", "start_ms": 355, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "keep", "start_ms": 415, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "going.", "start_ms": 505, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "네, 노력하겠습니다. 도와주셔서 감사해요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 135,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 135, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "노력하겠습니다.", "start_ms": 205, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "도와주셔서", "start_ms": 375, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "감사해요.", "start_ms": 515, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "Yes, I'll try. Thank you for your help.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 105,
"audio_type": "speech",
"words": [
{"word": "Yes,", "start_ms": 105, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "I'll", "start_ms": 185, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "try.", "start_ms": 255, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "Thank", "start_ms": 345, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 455, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "for", "start_ms": 505, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "your", "start_ms": 555, "dur_ms": 20, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "천만예요. 언제든 물어보세요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "천만예요.", "start_ms": 140, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "언제든", "start_ms": 300, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "물어보세요.", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "You're welcome. Feel free to ask anytime.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "You're", "start_ms": 110, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "welcome.", "start_ms": 210, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "Feel", "start_ms": 330, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "free", "start_ms": 410, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 490, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "ask", "start_ms": 530, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "anytime.", "start_ms": 575, "dur_ms": 1, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "질문이 하나 더 있어요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "질문이", "start_ms": 130, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "하나", "start_ms": 270, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 380, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "있어요.", "start_ms": 430, "dur_ms": 145, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "I have one more question.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 100, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "have", "start_ms": 170, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "one", "start_ms": 260, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "more", "start_ms": 340, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "question.", "start_ms": 440, "dur_ms": 135, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "무엇인지 말씀해 보세요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "무엇인지", "start_ms": 120, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "말씀해", "start_ms": 280, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "보세요.", "start_ms": 410, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "Please tell me what it is.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "Please", "start_ms": 90, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "tell", "start_ms": 200, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "me", "start_ms": 290, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "what", "start_ms": 350, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "it", "start_ms": 430, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "is.", "start_ms": 480, "dur_ms": 95, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "이 문장을 어떻게 읽나요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 135,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 135, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "문장을", "start_ms": 185, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 325, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "읽나요?", "start_ms": 445, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "How do you read this sentence?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "How", "start_ms": 110, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "do", "start_ms": 200, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 270, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "read", "start_ms": 330, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "this", "start_ms": 430, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "sentence?", "start_ms": 500, "dur_ms": 75, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "천천히 따라 읽어 보세요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "천천히", "start_ms": 120, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "따라", "start_ms": 280, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "읽어", "start_ms": 400, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "보세요.", "start_ms": 490, "dur_ms": 85, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "Read it slowly after me.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 95,
"audio_type": "speech",
"words": [
{"word": "Read", "start_ms": 95, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "it", "start_ms": 195, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "slowly", "start_ms": 245, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "after", "start_ms": 365, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "me.", "start_ms": 475, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "발음이 정말 좋으시네요!",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "발음이", "start_ms": 130, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 280, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "좋으시네요!", "start_ms": 390, "dur_ms": 185, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "Your pronunciation is very good!",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "Your", "start_ms": 100, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "pronunciation", "start_ms": 180, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 390, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "very", "start_ms": 450, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "good!", "start_ms": 530, "dur_ms": 45, "phase_lr_deg": 0.0}
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
"transcript": "칭찬해 주셔서 고마워요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "칭찬해", "start_ms": 140, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "주셔서", "start_ms": 280, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "고마워요.", "start_ms": 400, "dur_ms": 175, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "Thank you for the compliment.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "Thank", "start_ms": 110, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 220, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "for", "start_ms": 270, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 330, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "compliment.", "start_ms": 380, "dur_ms": 195, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "다음에 또 봐요. 안녕히 계세요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 130, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 260, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "봐요.", "start_ms": 330, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 430, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "계세요.", "start_ms": 550, "dur_ms": 25, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "See you again next time. Goodbye.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 100, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 180, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "again", "start_ms": 240, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "next", "start_ms": 340, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "time.", "start_ms": 430, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "Goodbye.", "start_ms": 510, "dur_ms": 65, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "네, 잘 가요!",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 150, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 260, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "가요!", "start_ms": 380, "dur_ms": 195, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "Yes, take care!",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "Yes,", "start_ms": 120, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "take", "start_ms": 240, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "care!", "start_ms": 380, "dur_ms": 195, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "즐거운 공부 하세요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "즐거운", "start_ms": 140, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "공부", "start_ms": 300, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "하세요.", "start_ms": 420, "dur_ms": 155, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "Have a nice study.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "Have", "start_ms": 130, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 250, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "nice", "start_ms": 300, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "study.", "start_ms": 420, "dur_ms": 155, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "나중에 만나요!",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "나중에", "start_ms": 150, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "만나요!", "start_ms": 290, "dur_ms": 285, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-01 05:55:13*
