# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43304  time=05:18:50  uptime=10518s  mem=1620MB  system_processes=442"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260331_051840.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 157-574-705-710-761-567-754-674-706-270-720-756-715-712-175-752-705-672-276-627-375-317-371-371-370-540-541-754 317-312-321-176-761-617-671-236-762-763-217 536-516-501-307-631-2_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 321-326-537-576-567-235-236-263-236-235-623-652-632-362-326-612-165-651-165-615-635-362-362-231-132-132-152-125-173-257-657-367-316-312-312-125-352-526-236-236-231-231-325-236-2_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 756-672-672-160-102-160-210-261-152-162-210-120-162-167-167-106-126-610-610-620-625-260-216-173-257-237-627-567-657-756-513-315-310-325-321-312-312-132-312-137-317-132 657-162 2_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 567-675-675-627-162-657-675-675-756-675-563-756-657-576-675-657-103-301-213-760-601-126-615-216-632-362-261-612-632-623-216-162-136-162-675-160-612-326-360-306-102-132-316-612-6_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 562-326-267-526-657-126-126-765-257-326-236-576-765-527-517-315-104-103-106-165-564-564-516-261-253-256-321-312-731-372-263-627-257-267-627-627-572-276-273-276-257-267-267-627-3_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 162-103-136-126-130-106-671-132-701-316-103-163-137-603-637-163-103-125-213-201-320-123-123-130-162-312-162-176-103-126-162-126-103-163-103-163-167-651-125-152-765-261-263-263-5_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 675-617-136-136-136-103-136-132-206-621-123-263-160-163-167-130-236-176-103-162-106-617-561-137-103-130-673-632-163-160-136-316-103-103-132-132-562-362-236-621-132-103-106-136-1_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 175-735-132-164-163-134-136-431-163-316-136-361-634-671-637-132-163-132-137-173-316-136-672-376-621-672-673-637-627-367-362-126-132-712-671-176-136-162-126-127-102-627-657-612-6_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 216-136-136-213-236-623-326-276-263-312-231-213-213-127-372-357-321-216-127-763-261-267-215-236-136-132-132-621-631-163-576-532-315-531-157-132-312-320-123-127-120-321-312-310-2_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 756 631-613 530-760-136-103-130-132-130-103-102-130-102-103-310-130-103-102_V.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 312-310-263-362-231-136-312-231-263-267-263-213-321-312-316-316-316-213-326-317-312-317-137-317-123-132-312-317-317-173-517-527-673-627-672-574-573-327-215-573-357-326-326-231-3_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 127-132-563-310-102-517-173-617-127-760-762-761-623-620-372-635-576-756-125-125-106-157-135-123-302-302-310-310-620-630-627-732-173-657-105-310-103-715-675-617-620-260-321-123-5_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 316-236-237-257-527-523-520-507-520-540-564-576-576-726-752-574-547-574-756-132-351-351-514-501-503-501-561-561-621-562-516-526-625-617-617-571-675-625-265-625-265-526-561-567-5_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 617-765-765-671-126-765-657-637-136-160-106-106-160-367-361-316-132-312-132-316-103-130-120-360-761-627-670-361-261-361-632-230-310-236-213-320-203-320-206-602-627-612-631-637-6_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 613-160-615-546-547-456-765-657-617-132-316-712-327-672-672-756-571-652-130-132-312-162-672-136-621-361-631-326 613-362-216 675-162-361-361-326 316_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 276-267-236-271-253-527-526-352-257-572-576-125-756-765-263-236-236-362-312-321-312-316-312-260-576-275-726-675-315-562-265-123-325-236-312-321-136-302-102-326-326-236-265-256-2_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 620-102-120-102-231-302-203-205-713-132-765-675-627-627-263-765-760-726-276-765-672-136-261-623-160-103-675-263-301-132-570-657-516-632-675-675-637-136-132-136-132-165-163-132-1_V.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 576-576-567-103-315-153-516-516-156-561-567-561-516-531-135-153-135-153-156-563-315-153-513-153-135-153-156-156-631-316-132-136-316-312-135-316-136-165-537-365-356-635-653-156-1_W.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 261-260-236-236-263-623-316-130-361-326-210-236-320-132-216-162-263-621-136-213-126-627-627-627-263-627-672-623-637-623-632-623-317-316-362-612-613-671-132-132-316-321-162-261-2_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 752-752-260-162-756-127-231-732-627-126-105-172-125-123-125-203-207-207-726-312-120-123-102-130-132 130-762-726-103 576-751 765-376-163-675-765-675-675-765-671-312-106-106-160-1_W.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 321-236-236-326-321-263-263-321-263-132-375-327-237-261 513-571-576-657-652-620-652-657-617-167-574-576-576-267-265-267-263-256-572-132-132-132-236-362-312-316-612-756-267-675-6_W.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 362-201-320 627-672-627-267-612-675-216-126-610_W.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 756-576-567-576-576-657-675-675-657-657-657-675-651-657-651-617-612-652-652-657-657-675-675-576-657-675-652-657-567-672-756-765-761-137-765-756-765-756-756-765-765-765-763-321-6_W.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 167-106-136-136-164-103-132-602-673-126-176-103-671-167-160-621-613-163-670-675-602-615-136-176-136-512-136-320-320-731-162-123-610-617-156-362-163-301-361-310-162-167-310-162-2_V.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 763-736-607-706-765-672-276-301-765-675-765-673 765-760-675-675-672-763-657-652-657-605-671-765-162-162-623-126-103-316-230-260-236-136-106-106-126-312-627-627-260-623-362-267-3_W.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 612-216-123-123-263-362-321-326-321-236-625-623-326-236-326-612-517-136-671-672-671-617-321-316-215-213-130-312-312-625-613-320-627-267-627-267-261-627-627-621-546-625-236-321-7_W.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 632-625-652-367-326-236-263-261-321-361-362-613-263-123-215-136-325-367-612-321-761-765-537-756-652-765-236-532-753-574-574-573-675-637-613-613-610 316-316-613-613-316-571-574-5_W.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 310-321-136-102-103-103-316-126-162-160-671-123-160-612-103-310-165-613-637-162-163-620-675-137-163-162-150-105-105-167-106-103-167-610-162-162-613-765-162-630-617-621-607-107-1_V.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 631-167-615-547-574-573-576-571-157-571-106-106-163-106-201-206-206-310-312-213-523-564-507-265-256-265-652-326-263-326-671-123-321-321-637-601 203-236-123-312-130-132 621-126-6_W.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 173-750-756 127-136 103 126-163-132-136-753-312-632-316-317-123-261-132-316-312-621-326-163-362-367-362-613-163-123 761-671 263-127-213-132-326 756-761-765 160-236-206-362 620-6_W.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 371-302-302-312-637-613-126-124-103-103-320-302-312-301-540-574-506-567-631 762 621-657-126-261-126-617-627-217-765-672-132-312-136-267-312-302-321-370-167-612-162-106-321-167-1_V.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 236-263-261-612-502-502-570-567-137-615-602-103-675-607-630-612-306-163-326-231-312-610-106-126-607-670-162-167-167-163-132-176-671-671-675-160-106-163-716-107-610-617-617-106-7_W.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 567-652-567-653-657-576-567-657-567-567-625-657-657-567-657-567-657-562-657-756-652-567-657-567-657-567-657-657-576-675-576-657-576-657-567-657-657-657-567-765-762-675-673-632_V.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 261-265-261-625-265-523-526-265-256-265-652-542-534-523-521-527-526-527-526-265-257-256-265-256-267-321-312-312-312-327-316-573-571-365-136-651-356-563-652-625-265-265-263-365-3_W.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 607-756-107-615-107-103-126-162-132-106-672-765-173-701-127-167-273-651-261-572-607-206-206-162-136-160-106-106-162-160-617-136-136-610-136-132-160-126-263-230-362-627-167-160-1_V.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 162-127-267-365-561-652-136-317-267-657-572-357-371-135-127-213-312-126-127-127-167-617-261-126-261-267-267-617-126-126-765-267-276-621-126-126-126-751-574-527-261-256-615-657-6_W.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 765-765-756-130-571-517-176-526-562-617-162-130-102-361-165-175-657-675-132-321-310-361-130-103-102-136-126-321-326-361-316-135-136-127-723-216-170-627-612-672-765-675-673-123-2_W.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 362-315-312-312-312-321-263-326-213-231-263-216-216-312-312-312-352-351-162-261-265-261-216-251-625-625-625-265-267-726-267-627-623-261-623-627-617-627-267-623-236-251-652-162-1_W.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 103-106-136-103-610-136-263-267 574_V.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 726-726-167-201-261-207-267-237-275-726-275-275-576-726-752-576-675-263-106-760-725-257-275-136-627-621-320-320-102-106-136-627-326-261-261-267-752-735-732-173-320-312-236-657-3_W.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 302-362-312-163-632-623-362-162-106-103-106-136-162-371-103-316-103-136-162-163-106-231-320-302-302-756-657-267-263-326-231-312-106-103-231-326-321-302-123-106-103-716-317-261-2_V.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 356-361-613-571-157-135-173-132-327-127-371-237-267-253-537-536-235-325-537-375-137-163-317-572-576-576-357-361-137-176-312-237-217-376-237-237-127-271-125-251-157-321-125-213-1_W.mp3)
  Segment 43: 66192ms ~ 66768ms (file: 537-357-107-657-627-362-564-532-524-524-235-321-526-253-267-562-215-216-265-267-276-615-613-523-523-534-235-236-312-542-526-325-523-524-265-216-261-267-276-762-637-362-263-367-6_W.mp3)
  Segment 44: 67768ms ~ 68344ms (file: 756-756-765-765-753-765 321-321-312-123-120-136-162-627-761-316-132-123-126 672-267-326-231-321-310-213-621-672-765-675-765-756 103-163-361-132-167-176-160-713-106-130-103-130-1_W.mp3)
  Segment 45: 69344ms ~ 69920ms (file: 321-302-231-507-750-261-576-657-673-136-613-310-263-236-627-265-526-562-162-312-123-320-310-312-132-237-237 143-705-705-735-703-750-765-675-671 672-602-632-261-306-312-632-620-6_V.mp3)
  Segment 46: 70920ms ~ 71496ms (file: 576-756-672-672-675-765-657-637-127-162-162-163-165-162-163-176-617-617-612-615-675-672-675-765-675-765-765-572-627-672-627-627-617-673-765-716-156-715-716-176-175-175-176-167-1_W.mp3)
  Segment 47: 72496ms ~ 73072ms (file: 612-136-162-136-127-321-126-316-537-351-271-276 316-316-130-136-657-265-267-675-675 625-621-162-163-267_W.mp3)
  Segment 48: 74072ms ~ 74648ms (file: 574-136-163-136-321-236-132-231-321-237-276-273-136-570-527-256-163-162-163-263-263-263-263-136-613-163-163-751-573-753-576-576-756-357-326-236-237-273-237-237-657-657-362-326-3_W.mp3)
  Segment 49: 75648ms ~ 76224ms (file: 623-632-623-627-627-672-627-265-562-625-267-627-627-627-621-216-762-263-261-263-623-627-267-653-312-316-236-631-162-132-326-261-275-267-732-574-253-312-326-236-362-753-376-362-2_W.mp3)
  Segment 50: 77224ms ~ 77800ms (file: 167-132-103-672-672-602-610-217-103-316-132-137-162-130-102-106-652-163-103-106-170-136-132-157-765-167-516-136-103-132-312-310-130-312 165-261-102-130-103-130-675-635-572-263-2_V.mp3)
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
> **Time**: 2026-03-31 05:19:39
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 2,
"transcript": "주말에 시간이 좀 있으세요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "주말에", "start_ms": 45, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "시간이", "start_ms": 185, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 305, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "있으세요?", "start_ms": 365, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "네, 특별한 계획은 없어요. 왜요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": -5.2},
{"word": "특별한", "start_ms": 120, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "계획은", "start_ms": 280, "dur_ms": 110, "phase_lr_deg": -5.3},
{"word": "없어요.", "start_ms": 400, "dur_ms": 90, "phase_lr_deg": -5.1},
{"word": "왜요?", "start_ms": 500, "dur_ms": 60, "phase_lr_deg": -5.2}
]
},
{
"segment": 4,
"transcript": "같이 영화 보러 가실래요?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "같이", "start_ms": 60, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "영화", "start_ms": 180, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "보러", "start_ms": 310, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "가실래요?", "start_ms": 410, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "좋아요! 어떤 영화가 보고 싶으세요?",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "좋아요!", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": -4.8},
{"word": "어떤", "start_ms": 200, "dur_ms": 110, "phase_lr_deg": -4.9},
{"word": "영화가", "start_ms": 320, "dur_ms": 110, "phase_lr_deg": -4.7},
{"word": "보고", "start_ms": 440, "dur_ms": 60, "phase_lr_deg": -4.8},
{"word": "싶으세요?", "start_ms": 510, "dur_ms": 50, "phase_lr_deg": -4.9}
]
},
{
"segment": 6,
"transcript": "요즘 액션 영화가 인기 많더라고요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "요즘", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "액션", "start_ms": 170, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "영화가", "start_ms": 300, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "인기", "start_ms": 420, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "많더라고요.", "start_ms": 520, "dur_ms": 40, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "저도 액션 영화 좋아해요. 언제 만날까요?",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "저도", "start_ms": 35, "dur_ms": 90, "phase_lr_deg": -5.0},
{"word": "액션", "start_ms": 135, "dur_ms": 90, "phase_lr_deg": -5.1},
{"word": "영화", "start_ms": 235, "dur_ms": 70, "phase_lr_deg": -4.9},
{"word": "좋아해요.", "start_ms": 315, "dur_ms": 120, "phase_lr_deg": -5.2},
{"word": "언제", "start_ms": 445, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "만날까요?", "start_ms": 535, "dur_ms": 30, "phase_lr_deg": -5.1}
]
},
{
"segment": 8,
"transcript": "Saturday afternoon is good for me.",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "Saturday", "start_ms": 45, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "afternoon", "start_ms": 235, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 395, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "good", "start_ms": 465, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "for", "start_ms": 535, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "me.", "start_ms": 575, "dur_ms": 0, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "토요일 2시 어때요? 강남역에서 봐요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "토요일", "start_ms": 60, "dur_ms": 110, "phase_lr_deg": -4.5},
{"word": "2시", "start_ms": 180, "dur_ms": 90, "phase_lr_deg": -4.6},
{"word": "어때요?", "start_ms": 280, "dur_ms": 110, "phase_lr_deg": -4.4},
{"word": "강남역에서", "start_ms": 400, "dur_ms": 140, "phase_lr_deg": -4.5},
{"word": "봐요.", "start_ms": 550, "dur_ms": 20, "phase_lr_deg": -4.6}
]
},
{
"segment": 10,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 11,
"transcript": "네, 2시에 늦지 않게 갈게요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 55, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "2시에", "start_ms": 125, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "늦지", "start_ms": 245, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "않게", "start_ms": 335, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "갈게요.", "start_ms": 435, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "혹시 팝콘 좋아하세요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "혹시", "start_ms": 30, "dur_ms": 90, "phase_lr_deg": -5.5},
{"word": "팝콘", "start_ms": 130, "dur_ms": 140, "phase_lr_deg": -5.3},
{"word": "좋아하세요?", "start_ms": 280, "dur_ms": 260, "phase_lr_deg": -5.4}
]
},
{
"segment": 13,
"transcript": "달콤한 팝콘을 좋아해요. 콜라도 마셔야죠.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "달콤한", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "팝콘을", "start_ms": 160, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "좋아해요.", "start_ms": 280, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "콜라도", "start_ms": 410, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "마셔야죠.", "start_ms": 520, "dur_ms": 40, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "Of course! Movie and popcorn go together.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "Of", "start_ms": 20, "dur_ms": 60, "phase_lr_deg": -4.2},
{"word": "course!", "start_ms": 90, "dur_ms": 130, "phase_lr_deg": -4.3},
{"word": "Movie", "start_ms": 230, "dur_ms": 110, "phase_lr_deg": -4.1},
{"word": "and", "start_ms": 350, "dur_ms": 60, "phase_lr_deg": -4.3},
{"word": "popcorn", "start_ms": 420, "dur_ms": 110, "phase_lr_deg": -4.2},
{"word": "go", "start_ms": 540, "dur_ms": 20, "phase_lr_deg": -4.1},
{"word": "together.", "start_ms": 570, "dur_ms": 0, "phase_lr_deg": -4.3}
]
},
{
"segment": 15,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 16,
"transcript": "그날 영화 보고 나서 저녁도 같이 먹을까요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "그날", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "영화", "start_ms": 140, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "보고", "start_ms": 260, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "나서", "start_ms": 350, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "저녁도", "start_ms": 440, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 530, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "먹을까요?", "start_ms": 570, "dur_ms": 0, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "좋아요, 제가 맛집을 하나 알고 있어요.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "좋아요,", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": -4.0},
{"word": "제가", "start_ms": 165, "dur_ms": 90, "phase_lr_deg": -4.1},
{"word": "맛집을", "start_ms": 265, "dur_ms": 110, "phase_lr_deg": -3.9},
{"word": "하나", "start_ms": 385, "dur_ms": 100, "phase_lr_deg": -4.0},
{"word": "알고", "start_ms": 495, "dur_ms": 50, "phase_lr_deg": -4.1},
{"word": "있어요.", "start_ms": 555, "dur_ms": 10, "phase_lr_deg": -3.9}
]
},
{
"segment": 18,
"transcript": "기대되네요. 그럼 토요일에 봬요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "기대되네요.", "start_ms": 60, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "그럼", "start_ms": 280, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "토요일에", "start_ms": 400, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "봬요.", "start_ms": 530, "dur_ms": 40, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "See you soon! Take care.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": -4.6},
{"word": "you", "start_ms": 160, "dur_ms": 60, "phase_lr_deg": -4.7},
{"word": "soon!", "start_ms": 230, "dur_ms": 120, "phase_lr_deg": -4.5},
{"word": "Take", "start_ms": 360, "dur_ms": 110, "phase_lr_deg": -4.6},
{"word": "care.", "start_ms": 480, "dur_ms": 80, "phase_lr_deg": -4.7}
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
"transcript": "안녕하세요, 오늘도 날씨가 좋네요.",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "안녕하세요,", "start_ms": 25, "dur_ms": 210, "phase_lr_deg": 2.5},
{"word": "오늘도", "start_ms": 245, "dur_ms": 110, "phase_lr_deg": 2.6},
{"word": "날씨가", "start_ms": 365, "dur_ms": 120, "phase_lr_deg": 2.4},
{"word": "좋네요.", "start_ms": 495, "dur_ms": 70, "phase_lr_deg": 2.5}
]
},
{
"segment": 22,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 23,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 24,
"transcript": "피아노 소리가 참 듣기 좋네요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "피아노", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "소리가", "start_ms": 230, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "참", "start_ms": 360, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "듣기", "start_ms": 420, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "좋네요.", "start_ms": 510, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "감사합니다. 제가 요즘 연습 중이에요.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "감사합니다.", "start_ms": 50, "dur_ms": 230, "phase_lr_deg": -5.1},
{"word": "제가", "start_ms": 290, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "요즘", "start_ms": 380, "dur_ms": 90, "phase_lr_deg": -5.2},
{"word": "연습", "start_ms": 480, "dur_ms": 60, "phase_lr_deg": -5.1},
{"word": "중이에요.", "start_ms": 550, "dur_ms": 20, "phase_lr_deg": -5.0}
]
},
{
"segment": 26,
"transcript": "How long have you been playing?",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "How", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "long", "start_ms": 120, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "have", "start_ms": 240, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 330, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "been", "start_ms": 420, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "playing?", "start_ms": 510, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "3년 정도 됐어요. 아직 많이 부족해요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "3년", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": -4.8},
{"word": "정도", "start_ms": 165, "dur_ms": 110, "phase_lr_deg": -4.9},
{"word": "됐어요.", "start_ms": 285, "dur_ms": 120, "phase_lr_deg": -4.7},
{"word": "아직", "start_ms": 415, "dur_ms": 80, "phase_lr_deg": -4.8},
{"word": "많이", "start_ms": 505, "dur_ms": 60, "phase_lr_deg": -4.9},
{"word": "부족해요.", "start_ms": 575, "dur_ms": 0, "phase_lr_deg": -4.7}
]
},
{
"segment": 28,
"transcript": "아니에요, 정말 훌륭한 연주였어요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "아니에요,", "start_ms": 65, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 215, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "훌륭한", "start_ms": 335, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "연주였어요.", "start_ms": 475, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "격려해 주셔서 고마워요. 힘이 나네요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "격려해", "start_ms": 35, "dur_ms": 130, "phase_lr_deg": -5.1},
{"word": "주셔서", "start_ms": 175, "dur_ms": 110, "phase_lr_deg": -5.2},
{"word": "고마워요.", "start_ms": 295, "dur_ms": 130, "phase_lr_deg": -5.0},
{"word": "힘이", "start_ms": 435, "dur_ms": 80, "phase_lr_deg": -5.1},
{"word": "나네요.", "start_ms": 525, "dur_ms": 40, "phase_lr_deg": -5.2}
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
"transcript": "다음에 또 연주해 주실 수 있나요?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 200, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "연주해", "start_ms": 270, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "주실", "start_ms": 390, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 480, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "있나요?", "start_ms": 530, "dur_ms": 40, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "네, 언제든지 환영이에요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": -5.5},
{"word": "언제든지", "start_ms": 110, "dur_ms": 210, "phase_lr_deg": -5.4},
{"word": "환영이에요.", "start_ms": 330, "dur_ms": 230, "phase_lr_deg": -5.5}
]
},
{
"segment": 33,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 34,
"transcript": "You are very kind. Thank you so much.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "You", "start_ms": 45, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "are", "start_ms": 115, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "very", "start_ms": 185, "dur_ms": 110, "phase_

---
*Generated by WKAppBot ask gemini — 2026-03-31 05:19:39*
