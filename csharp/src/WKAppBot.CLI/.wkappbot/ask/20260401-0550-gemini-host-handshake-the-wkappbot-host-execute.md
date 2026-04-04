# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=14524  time=05:49:19  uptime=7549s  mem=2318MB  system_processes=847"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260401_054909.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 315-531-516-531-315-315-316-156-351-561-517-576-567-657-567-657-653-675-765-675-367-567-653-657-567-657-657-657-657-675-765-675-657-736-356-167-156-517-156-157-156-513-135-153-5_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 136-631-263-231-162-612-627-163-761-761-751-175-107-107-175-176-107-107 157-103-137-106-310-760-701-706-362-623-627-326-316-367-362-623-605-165-765-176-301-731-132-123-601-132-3_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 326-120-312-672-671-617 756-756-716-761-761-712-756-173-175-623-617-106-175-127-632-136-672-561-160-217-106-542-132-172-105-165-206-167-176-176-106-671-671-106-163-137-106-251-5_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 627-267-762-236-263-621-263-612-627-627-627-623-623-267-627-236-623-263-267-623-623-623-623-632-627-627-627_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 352-372-306-312-301-372-627-326-732-273-670-670-326-213-126-103-312-301-302-301-312-316-102-120-160-613-106-130-130-130-302-103-130-310-130-137-132-675-613-130-317-103-163-703-1_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 652-675-106-102-123-126-601-132-657-750-672-627-316-261-235-532-261-362-760-672-267-261-216-310-310-263-236-163-675-631-523-561-320-320-163-103-165-230-216-572-670-206-627-207-6_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 312-263-326-321-673-762-327-132-310-130-321-213-576-725-310-302-302-301-761-260-261-152-215-625-321-625-572-301-302-326-302-360-302-132-106-362-632-316-657-765-267-602-213-301-3_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 621-623-627-672 623-632-327-327-237-362-263 637-360-160-301_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 132-657-657-657 162-361-132-316-130-320-163-316-162-136-617-765-765-712-701-756-751-273-157-312-176-267-316-310-163-312-125-567-567-367-631-162-167-312-136-361-623-627-671-603-6_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 657-567-576-652-367-736-236-326-263-625-362-517-652-312_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 317-316-312-316-236 673-316-617-613-136-312-236-126-136-103-310-306-360-301-106-136-162-106-136-162-675-607-167-167-672-316-162-103-601-326-362-263-106-130-132-103-310-103-301-6_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 103-103-123-132-137-671-627-602-302-103-167-167-103-163-657-627-267-260-627-620-602-607-163-103-517-716-163-310 132-167-103-310-672-213-167-256-276-207-203-143-132-312-301-301-3_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 372-312-137-341-764-376-375-671-547-321 362-132-314-312-312-362-326-276-627-276-275-765-752-726-623-315-316-316-123-132-126-321-213-315-570-513-153-371-315-132-621-143-412-613-1_V.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 176-167-132-361-136-162 327-213-352-763-276-726-627-276-267-267-302-172-216-312_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 310-310-136-312-675-675-160-625-312-261-627-263-326-621-263-261 756-576-576-576-574-157-157 756-765-765-765-706-572-571 576-756-756-751_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 132-136-123-136-612-617-136-176-165-162-167-165-165-165-615-165-165-167-165-167-615-617-162-765-675-672-612-612-615-167-163-165-156-162-165-165-162-162-165-675-167-165-651-671-7_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 615-657-657-165-167-312-137-167-163-135-132-316-137-261-752-275-276-231-213-723-236-267-275-276-275-726-675-751_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 675-321-231-126-136-162-136-106-132-265-623-162-301-574-753-753-504-547-547-621-267-263-236-260-206-203-263-602-260-572-312-316-132-312-316-230-203-201-321-302-320-362-756-760-7_V.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 267-621-267-621-627-627-627-672-672-267-267-263-263-267-261-267-261-231-162-216-752-237-327-712-721-137-231-231-132-716-361-326-237-321-362-316-563-531-632-261-265-561-256-621-6_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 627-632 136-163-106-315-735-123-167-103-316-103-137-163-136-136-136-617-106-163-136-103-167-136-206-631-615-163-672-162-312-610-136-106-617-132-163-130-362-160-163-103-136-651-6_V.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 657-567-576-576-576-756-567-576-576-567-576-576-574-567-576-567-517-657-756-567-567-576-576-675-165-615-601-560-675-675-651-756-621-625-256-256-105-160-105-106-130-630-632-607-1_W.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 531-317-317-163-317-173-315-351-361-371-367-367-635-567-657-367-316 213-312-326-173-362-367-326-362-365-326-327-325-235-362-325-326-325-326-325-312-316-315-375-317-351-321-371-3_W.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 132-136-361-326-216-213-136-132-362-621-136-162-612-162-132-123-312-132-326-321-316-312-312-312-132-162-136-132-312-312-135-136-213-625-325-176-123-123-312-136-132-137-316-310-1_W.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 762 136-713-167-103-176-170-136-612-136-106-130-673-163-136-103-761-167-612-316-617-106-136-567-103-163-106-170-763-130-160-617-167-176-126-157-137-126-130-103-103-123-321-132-1_V.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 167-106-136-130-617-267-621-621-617-206-630-612-326-236-621-750-756-765-102-132-507-576-765-236-231-263-260-267-216-326-267-267-263-267-267-265-267-267 302-236-362-617-623-261-6_W.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 236-206-320-230-231-362-326-106-106-103-132-160-615-130-671-517-312-123-623-621-627-671-672-671-671-617-673-765-167-617-651-567-657-751-675-657-675-672-672 267-657-163-126-231-1_W.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 312-612-102-576-567-321-163-106-160-607-620-621-106-136-206-601-602-632-613-617-601-617-612-362-316-360-206-206-206-236-206-236-263-126-301-132-130-103-163-613-617-614-106-316-3_V.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 602-260-206-260-207-206-207-271-132-712-102-713-170-576-576-517-321-162-132-317-573-157-751-153-156-312-126-132-356-160-675-167-126-162-132-167-132-216 675-670-103-127-321-123-1_W.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 136-132-670-672-675-765-576-675-372-567-751-165-102-316_V.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 123-216-236-312-312-126-216-132-321-321-612-162-123-163-316-623-156-261-152-675-617-613-672-163-172-123-351-510-561-613-162-103-167-126-675 127 672-612-132-164-260-206-216-213-5_W.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 326-623-362-365-321-326-326-321-123-216-326-263-352-531-352-251-257-327-325-267-267-326-267-265-526-527-527-527-235-236-213-326-231-231-231-325-132-215-132-216-326-263-263-236-2_W.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 167-163-103-162-136-106-153-135-165-165-160-671-321-623-610-160-160-106-162-231-263-126-651-156-167-106-106-103-512-657-216-167-615-165-162-165-162-132-106-163-126-106-176-167-1_V.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 573-576-106-103-310-163-206-216-260-126-236-160-602-106-672-602-162-261-301-103-163-103-130-723-320-612-230-260-236-206-236-136-630-632-603-672-760-730-106-607-103-362-106-102-1_W.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 326-321-623-621-132-326-231-213-216-326-231-231-263-261-216-263-213-231-236-321-321-632-267-627-276-267-265-263-132-326-621-261-236 132-361 623-675-657-657-627-627-621-263-213-6_W.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 574-572-547-253-325-351 165-536-152-625-675-265-261-631-312-261-165-365-352-652-362-623-162-162-621-617-165-561-576-765-670-671-716-176-175-175-135-763-371-372-671-652-312-132-3_W.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 627-261-267 263-231-261-263-637-156-617-361-106-162-216-123-210-126 267-623_W.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 201-621-160-107-106-163-271-213-162-312-621-126-123-163-321-316-231-210-162-571-576-627-267-267-265-672-276-157-765-765-756-675-627-106-574-576-517-615-167-601-306-132-317-517-5_W.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 271 132-675-136-517-176-621-612-167-267-162-316-106-103-126-130-162-213-312-157-163-132-601-261-103-160-160-167-513-756-167-765-105-125-136-103-160-136-136-157-621-653-615-615-1_V.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 267-263-267-623-263-623-326-326-263-627-263-263-267-627-627-210-261-213-216-215-531-312-513-315-312-315-316-325-365-361-312-316-361-263-236-263-263-263-265-263-362-326-365-267-3_W.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 261-326-163-126-163-160-163-631-316-120-612-103-103-516-213-265-126-162-103-103-103-137-126-103-103-163-126-162-163-136-162-103 162-613-126-326-361-132-136-167-167-613 675-163-2_V.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 621 756-765-756-671-167-126-632-367-163-163-713-360-751-716 675-132-253-602-136-103-123-716-516-165-613-103-150-213-130-316-163-106-123-125-756-630-261-632-231-261-231-213-261-2_W.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 574-257-276-236-237-261-273-276-625-265-267-265-267-261-623-236-326-263-256-263-623-623-260-261-162-136-312-321-167-167-167-756-135-675-673-617-617-617-671-574-756-317-326-674-6_W.mp3)
  Segment 43: 66192ms ~ 66768ms (file: 570-517-162-716-756-756-765-760 756-756-756-765-672-607-576-572-756-765-652-365-657-652-675-637-613-765-671-672-762-765_W.mp3)
  Segment 44: 67768ms ~ 68344ms (file: 132-302-362-765-267-612-162-103-123-320-627-206-230-320-132-123-621-106-123-102-237-732-750-723-260-206-206-206-321-206-602-762-360-360-613-130-316-231-126-130-302-301-320-632-2_W.mp3)
  Segment 45: 69344ms ~ 69920ms (file: 123-136-267-321-216-126-765-567-562-203-725-627-627-132-752-567-163-765-627-675-132-137-612-103-671-102-620-261-263-162-201-261-612-621-610-103-130-136-613-136-136-625-617-106-1_W.mp3)
  Segment 46: 70920ms ~ 71496ms (file: 375-567-751-756-715-537-571-576-756-627-637-675-567-673-751-576-765-126-572-756-756-276-576-576-765-756-756-625-675-637-612-613-621-167-365-635-671-167-137-375-317-316-317-127-5_W.mp3)
  Segment 47: 72496ms ~ 73072ms (file: 312-213-125-321-213-123-302-312-103-203-312-132-612-160-103-612-123-162-132-106-236-123_V.mp3)
  Segment 48: 74072ms ~ 74648ms (file: 362-312-231-123 362-632-612-362-631-627-627-672-675-326-625-261-627-627-263-321-213-372-675-652-657-576-316-123-316-316-312-317-176-153-657-136-312-361-570-175-107-176-516-571-7_W.mp3)
  Segment 49: 75648ms ~ 76224ms (file: 267-617-627-267-621-362-267-267-627-627-652-675-372-362-637-173-176-137-176-176-361-172-763-276-762-672-670-637-675-673-613-621-627-265-627-672-672-637-627-627-361-627-267-623-6_W.mp3)
  Segment 50: 77224ms ~ 77800ms (file: 130-310-132-136 627-362 103-625-163-671 627-627-126-163-261-657-176-621 176-617-107-567-576-167-271-512-167-103-130-163-163-265-126-163-136-617-620-173-106-125-162-163-162-612-2_V.mp3)
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
> **Time**: 2026-04-01 05:50:55
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "방학 때 무엇을 할 계획인가요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 115,
"audio_type": "speech",
"words": [
{"word": "방학", "start_ms": 115, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 225, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "무엇을", "start_ms": 275, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 395, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "계획인가요?", "start_ms": 455, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "What are your plans for the vacation?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "What", "start_ms": 90, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "are", "start_ms": 170, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "your", "start_ms": 230, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "plans", "start_ms": 300, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "for", "start_ms": 410, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 470, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "vacation?", "start_ms": 520, "dur_ms": 55, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "제주도에 여행을 가려고 해요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "제주도에", "start_ms": 130, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "여행을", "start_ms": 300, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "가려고", "start_ms": 430, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "해요.", "start_ms": 530, "dur_ms": 45, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "I'm going on a trip to Jeju Island.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "I'm", "start_ms": 100, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "going", "start_ms": 170, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "on", "start_ms": 270, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 330, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "trip", "start_ms": 370, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 460, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "Jeju", "start_ms": 510, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "Island.", "start_ms": 565, "dur_ms": 10, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "누구와 함께 가나요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "누구와", "start_ms": 140, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "함께", "start_ms": 280, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "가나요?", "start_ms": 390, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "Who are you going with?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "Who", "start_ms": 110, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "are", "start_ms": 200, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 260, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "going", "start_ms": 330, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "with?", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "가족들과 함께 가요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 135,
"audio_type": "speech",
"words": [
{"word": "가족들과", "start_ms": 135, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "함께", "start_ms": 305, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "가요.", "start_ms": 425, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "I'm going with my family.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 115,
"audio_type": "speech",
"words": [
{"word": "I'm", "start_ms": 115, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "going", "start_ms": 195, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "with", "start_ms": 295, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "my", "start_ms": 375, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "family.", "start_ms": 445, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "제주도에서 무엇을 할 건가요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 125,
"audio_type": "speech",
"words": [
{"word": "제주도에서", "start_ms": 125, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "무엇을", "start_ms": 315, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 425, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "건가요?", "start_ms": 475, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "What will you do in Jeju Island?",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "What", "start_ms": 100, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "will", "start_ms": 200, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 280, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "do", "start_ms": 350, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 420, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "Jeju", "start_ms": 470, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "Island?", "start_ms": 530, "dur_ms": 45, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "바다를 구경하고 맛있는 것을 먹을 거예요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "바다를", "start_ms": 130, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "구경하고", "start_ms": 260, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "맛있는", "start_ms": 380, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "것을", "start_ms": 490, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "먹을", "start_ms": 540, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "거예요.", "start_ms": 575, "dur_ms": 1, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "I will see the ocean and eat delicious food.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 105,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 105, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "will", "start_ms": 165, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "see", "start_ms": 235, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 315, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "ocean", "start_ms": 365, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "and", "start_ms": 465, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "eat", "start_ms": 525, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "delicious", "start_ms": 570, "dur_ms": 5, "phase_lr_deg": 0.0},
{"word": "food.", "start_ms": 576, "dur_ms": 0, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "얼마나 머물 건가요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "얼마나", "start_ms": 140, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "머물", "start_ms": 280, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "건가요?", "start_ms": 400, "dur_ms": 170, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "How long will you stay?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "How", "start_ms": 110, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "long", "start_ms": 210, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "will", "start_ms": 320, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 380, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "stay?", "start_ms": 450, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "3박 4일 동안 있을 거예요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "3박", "start_ms": 130, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "4일", "start_ms": 250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "동안", "start_ms": 360, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "있을", "start_ms": 450, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "거예요.", "start_ms": 520, "dur_ms": 55, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "I will be there for four days and three nights.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 100, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "will", "start_ms": 160, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "be", "start_ms": 230, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "there", "start_ms": 280, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "for", "start_ms": 360, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "four", "start_ms": 410, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "days", "start_ms": 480, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "and", "start_ms": 540, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "three", "start_ms": 571, "dur_ms": 4, "phase_lr_deg": 0.0},
{"word": "nights.", "start_ms": 576, "dur_ms": 0, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "숙소는 어디인가요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 145,
"audio_type": "speech",
"words": [
{"word": "숙소는", "start_ms": 145, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "어디인가요?", "start_ms": 305, "dur_ms": 270, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "Where is the accommodation?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "Where", "start_ms": 110, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 210, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 270, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "accommodation?", "start_ms": 320, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "해변 근처 호텔을 예약했어요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "해변", "start_ms": 120, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "근처", "start_ms": 230, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "호텔을", "start_ms": 330, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "예약했어요.", "start_ms": 450, "dur_ms": 125, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "I booked a hotel near the beach.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 95,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 95, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "booked", "start_ms": 155, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 265, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "hotel", "start_ms": 305, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "near", "start_ms": 425, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 505, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "beach.", "start_ms": 555, "dur_ms": 20, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "재미있겠네요! 부러워요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 135,
"audio_type": "speech",
"words": [
{"word": "재미있겠네요!", "start_ms": 135, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "부러워요.", "start_ms": 405, "dur_ms": 170, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "That sounds fun! I'm jealous.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "That", "start_ms": 110, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "sounds", "start_ms": 190, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "fun!", "start_ms": 290, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "I'm", "start_ms": 390, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "jealous.", "start_ms": 470, "dur_ms": 105, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "다음에 같이 가요.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 310, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "가요.", "start_ms": 430, "dur_ms": 145, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "Let's go together next time.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 115,
"audio_type": "speech",
"words": [
{"word": "Let's", "start_ms": 115, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "go", "start_ms": 215, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "together", "start_ms": 285, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "next", "start_ms": 425, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "time.", "start_ms": 515, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "좋은 생각이에요. 꼭 가요!",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 120, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "생각이에요.", "start_ms": 220, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "꼭", "start_ms": 380, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "가요!", "start_ms": 450, "dur_ms": 125, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "That's a good idea. Let's definitely go!",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 95,
"audio_type": "speech",
"words": [
{"word": "That's", "start_ms": 95, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 185, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "good", "start_ms": 235, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "idea.", "start_ms": 335, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "Let's", "start_ms": 445, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "definitely", "start_ms": 515, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "go!", "start_ms": 576, "dur_ms": 0, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "사진 많이 찍어 오세요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 135,
"audio_type": "speech",
"words": [
{"word": "사진", "start_ms": 135, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 265, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "찍어", "start_ms": 375, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "오세요.", "start_ms": 475, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "Please take lots of pictures.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 105,
"audio_type": "speech",
"words": [
{"word": "Please", "start_ms": 105, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "take", "start_ms": 215, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "lots", "start_ms": 305, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "of", "start_ms": 385, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "pictures.", "start_ms": 445, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "네, 그럴게요. 여행 잘 다녀올게요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 140, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "그럴게요.", "start_ms": 210, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "여행", "start_ms": 330, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 430, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "다녀올게요.", "start_ms": 490, "dur_ms": 85, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "Yes, I will. I'll have a great trip.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "Yes,", "start_ms": 110, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 190, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "will.", "start_ms": 250, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "I'll", "start_ms": 340, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "have", "start_ms": 410, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 480, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "great", "start_ms": 520, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "trip.", "start_ms": 575, "dur_ms": 1, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "선물도 잊지 마세요!",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 125,
"audio_type": "speech",
"words": [
{"word": "선물도", "start_ms": 125, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "잊지", "start_ms": 285, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "마세요!", "start_ms": 405, "dur_ms": 170, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "Don't forget the souvenir!",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "Don't", "start_ms": 100, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "forget", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 310, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "souvenir!", "start_ms": 360, "dur_ms": 215, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "알겠어요. 맛있는 선물 사 올게요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "알겠어요.", "start_ms": 130, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "맛있는", "start_ms": 270, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "선물", "start_ms": 390, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "사", "start_ms": 490, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "올게요.", "start_ms": 530, "dur_ms": 45, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "I'll get it. I'll buy a delicious souvenir.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "I'll", "start_ms": 110, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "get", "start_ms": 190, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "it.", "start_ms": 260, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "I'll", "start_ms": 320, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "buy", "start_ms": 390, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 460, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "delicious", "start_ms": 500, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "souvenir.", "start_ms": 575, "dur_ms": 1, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "안녕히 다녀오세요!",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 140, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "다녀오세요!", "start_ms": 280, "dur_ms": 295, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "Have a safe trip!",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "Have", "start_ms": 120, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 220, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "safe", "start_ms": 270, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "trip!", "start_ms": 380, "dur_ms": 195, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "네, 감사합니다. 나중에 봐요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 130, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "감사합니다.", "start_ms": 200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "나중에", "start_ms": 360, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "봐요.", "start_ms": 480, "dur_ms": 95, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "Yes, thank you. See you later.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "Yes,", "start_ms": 100, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "thank", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "you.", "start_ms": 290, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "See", "start_ms": 360, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 460, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "later.", "start_ms": 530, "dur_ms": 45, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "잘 가요!",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "잘", "start_ms": 150, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "가요!", "start_ms": 270, "dur_ms": 305, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "Goodbye!",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "Goodbye!", "start_ms": 120, "dur_ms": 455, "phase_lr_deg": 0.0}
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
"transcript": "오늘 저녁에 시간 있나요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 135,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 135, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "저녁에", "start_ms": 235, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "시간", "start_ms": 355, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "있나요?", "start_ms": 445, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "Are you free this evening?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 105,
"audio_type": "speech",
"words": [
{"word": "Are", "start_ms": 105, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 185, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "free", "start_ms": 255, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "this", "start_ms": 375, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "evening?", "start_ms": 445, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "네, 무슨 일인가요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 140, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "무슨", "start_ms": 210, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "일인가요?", "start_ms": 310, "dur_ms": 265, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "Yes, what is it?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "Yes,", "start_ms": 110, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "what", "start_ms": 190, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 310, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "it?", "start_ms": 370, "dur_ms": 205, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "영화 보러 가실래요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "영화", "start_ms": 130, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "보러", "start_ms": 250, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "가실래요?", "start_ms": 350, "dur_ms": 225, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "Would you like to go to the movies?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "Would", "start_ms": 100, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 200, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "like", "start_ms": 270, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 360, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "go", "start_ms": 410, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 470, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 510, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "movies?", "start_ms": 550, "dur_ms": 25, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "좋아요, 몇 시에 만날까요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "좋아요,", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "몇", "start_ms": 280, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "시에", "start_ms": 350, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "만날까요?", "start_ms": 440, "dur_ms": 135, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "Sounds good, what time should we meet?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "Sounds", "start_ms": 120, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "good,", "start_ms": 220, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "what", "start_ms": 300, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "time", "start_ms": 420, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "should", "start_ms": 500, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "we", "start_ms": 570, "dur_ms": 5, "phase_lr_deg": 0.0},
{"word": "meet?", "start_ms": 576, "dur_ms": 0, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "7시에 극장 앞에서 만나요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "7시에", "start_ms": 140, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "극장", "start_ms": 260, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "앞에서", "start_ms": 360, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "만나요.", "start_ms": 470, "dur_ms": 105, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-01 05:50:55*
