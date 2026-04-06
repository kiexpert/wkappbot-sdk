# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=06:45:46  uptime=19592s  mem=1712MB  system_processes=445"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_064537.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 756-617-756-765-675-621-263-105-105-126-236-152-176-361-162-602-621 126-106-567-570-130-162-167-156-612-106-163-217-620-615-123-163-103-130-651-516-561-765-671-165-103-316-162-1_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 576-107-172-162-602-620-613-601-127-610-617-621-601-216-126-620-621-601-103-170-102-162-132-160-162-132-102-106-135-167-157-103-162-612-106-167-315-130-136-712-143-157-103-163-1_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 102-162-670-627-263-162-132-123-312-126-136-156-102-136-120-165-123-603-103-610-316-106-163-136-106-621-160-610-167-136-567-123-321-612-162-301-362-103-103-673-316-620-621-106-1_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 615-157-371-574-572-672-765-512-517-562-256-567-357-671-657-576-751-175-275-271-752-573-657-175-615-657-657-153-651-516-216-352-357-365-763-623-256-235-216-265-625-261-765-675-6_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 163-312-326-213-326-263-201-106-163-102-623-567-235-130-513-130-670-267-231-235-265-126-263-261-265-625-625-612-361-631-132-613-610-623-620-612-162-167-167-107-126-127-162-162-6_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 102-312-503-573-537-574-574-573-576-627-312-312-632-612-306-310-310-301-320-362-302-302-320-301-302-301-316-763-716-163-750-750-236-320-153-130-120-150-514-135-312-352-206-260-2_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 263-267-267-261-264-263-267-627-627-263-267-267-265-526-625-362-367-263-623-627-625-576-265-627-263-261-267-267-267-627-627-261-261-632-620-627-260 213-261-621-261-264-261-264 2_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 316-127-210-120 206-732-371-173-103-312-326-327-265-312-132-312-102-102-102-102-102-301-310-123-126-216-132-236-203-203-310-301-312-156-617-612-130-316-501-510-157-315-206-301-3_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 725-765-735-756-765-763-753-567-657-657-657-136-132-627-632-102-136-612 567-576-756-567-623-261 132-137-651-175-167-175-162-132-167-167-360-163-136-651-163-726-762-132-761-167-1_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 567-657-567-576-312-132-213-132-310-567-657-657-657-650-576-657-657-621-362-126-126-612-136-631-571-576-765-175-107-103-672-672-670-607-670-607-576-576-567-570-756-765-560-657-1_V.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 261-216-261-216-261-126-216-261-216-126-126-621-261-621-261-126-126-216-261-123-312-321-321-132-132-132-157-576-153-153-135-135-123-137-126-312-513-153-215-231-132-312-136-157-1_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 263-126-521-362-261-206-126-320-265-103-106-102-126-106-726-562-102-316-325-132-106-301-312-136-625-261-263-216-123-132-302-312-751-765-670-756-756-756-675-675-765-627-320-310-1_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 671-762-761-276-126-162-310-130-136-137-106-132-156-120-210-216-127-251-276-176-162-672-163-136-263-201-216-127-210-167-216-621-260-632-213-216-216-215-215-617-613-132-562-316-1_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 230-623-326-672-671-672 627-627-623-261 514-517-501 263-670 675-672-617-627-627-612-627-657-657-670-672-176-710-601-617_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 162-627-267-120-267-267-267-267-261-267-267-263-576-576-312-130-103-103-136-103-132-103-312-270-260-617-672-671-671-765-627-672-672-321-132-102-761-627-612-621-267-602-261-261-2_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 163-136-312-103-136-561-361-275-562-106-631-725-216 162-163-712-162-162-126-103-102-526-132-576-362-123-630-603-162-132-123-103-316-631-106-103-160-176-137-253-125-316-632-130-1_V.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 167-576-167-760-376-307-367-260-124-143_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 526-126-120-125-201-132-216-203-230-320-230-236-320-320-320-236-325-320-310-312-537-270-320-103-301-132-231-302-320-320-130-312-106-162-106-301-321-321-320-213-623-320-130-326-3_V.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 102-126-106-106-103-130-265-672-230-321-132-163-136-123-136-123-106-136-163-602-236-675-260-620-326-236-301-106-136-301-530-531-103-103-157-107-173-135-320-123-103-130-312-362-3_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 235-310-302-321-352-543-574-567-576-175-357-231-503-543-514-153-325-563-130-621-107-163-321-321-325-321-320-310-372-137-321-312-321-547-547-547-607-672-267-305-235-673-236-231-3_W.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 547-310-315 153-613-317-137-136-126-136-176-316-132-167-162-106-612-163-312-132-163-576-576-567-657-567-657-576-657-576-567-216-256-765-675-576 576-567-567-567-657-756-675-653-5_W.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 213-123-126-123-257-265-263-162-261-216-275-572-571-256-216-251-652-562-256-275-157-325-362-263-236-312-237-276-321-321-320-236-236-267-267-263-263-263-263-524-547-540-156-135-2_W.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 630-312-231-174-207-734-730-724-273-270-316-235-257-526-567-651-275-536-267-361-315-316-612-321-126-162-162-621-312-123-263-132-153-137-165-162-623-312-621-326-362-312-653-526-5_W.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 760-237-213-231-216-325-123-326-213-261-261-362-231-321-674-765-156-236-236-236-216-540-527-257-250-265-237-257-361-321-213-312-216-312-213-231-123-302-361-731-760-612-263-267-2_W.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 576-576-576-576-756-756-162-163-132-162-703-713-671-710-756-256-216-162-103-765-103-760-160-760-763-137-136-360-136-127-137-137-123-761-367-173-601-540-326-725-765-752-576-765-7_V.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 756-706-123-126-761-315-104-165-163-156-670-637-316-362-132-103-312-625-123-675-675-760-176-167-136-175-602-612-601-602-162-301-637-136-137-167-163-613-132-621-632-263-613-651-6_V.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 754-576-576-756-310-765-576-657-573-357-310-306-316-130-301-357-376-306-310-351-301-310-103-321-301-103-316-367-316-163-570-750-103-312-123-126-754-754-703-103-132-301-130-301-3_V.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 620-326-362-310-321-201-521-756-607-216-362-672-671-632-673-216-627-132-126-123-612-627-621-162-162-621-632-372-627-621-321-132-361-317-327-317-631-263-672-326-362-612-672-621-6_W.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 621-162 632-623-627-672-627-620-627-627-672-673-675-675-627_W.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 321-312-126-165-623-326-312-316-132-136-162 625-236-612-231-236-627-162-136-213-263-612_W.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 763 102-103-657-652-627 136-132-130-632-136-326-672-562-265-627-627-627-657-657-675-675-657-567-657-657-675-367-652-167-675-167-167-267-621-126-132-125-156-263-216-126-627-612-7_W.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 263-231-160-210-136-160-321-621-625-623-167-175-153-167-615-675-765-756-765-617 607-103-136-103-162-163-631-136-312-671-617-675-627-675-625-627-612-106-132-301-136-103-106-136-1_W.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 627-532-574-504-547-574-504-547 753-751 543-534-547-761-716-673-726-762-627-672-263-632-627-673-156-127-123-167-162-163-103-317-312-172-126-301-312-621-130-132-102-103-105-103-1_W.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 126-126-761-567-657-567-765-106-126-106-103-326-756-107-167-327-203-203-320-326-263-321-320-326-306-361-316-103-627-263-162-106-321-132-531-315 564-321-260-206-261-261-126-320-2_V.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 231-132-123-213-126-123-132-132-316-312-132-321-216-523-576-756-216-126-312-320-203-213-123-312-130-213-213-152-263-267-216-267-265-267-267-263-251-213-312-126-571-576-576-765-6_W.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 625-576-356-651-567-125-152-532-315-317-312-123-126-102-156-315-567-657-527-153-153-576-571-576-573-563-256-265-625-261-326-526-635-652-356-136-512-735-561-157-751-175-576-765-5_W.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 106-137-126-162-217-271-602-620-260-260-206-230-123-312-756-620-612-162-132-321-312-132-267-127-123-132-123-312-103-123-162-124-261-261-126-102-106-107-123-213-201-213-231-321-6_V.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 216-265-276-276-216-267-267-276-276-267-276-276-267-271-126-263-237-271-216-267-236-261-267-621-261-126-321-216-216-216-326-275-657-673-367-167-321-752-156-617-123-163-312-125-1_W.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 576-265-267-576 672-567-103-132-605-126-120-120-103-103-675-671-132-361-163-756-765-721-657-623-267-576-765-206-201-103-310-302-260-123-126-162-102-652-672-216-652-675-260-260-6_V.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 263-136-263-267-327-162-123-216-126-126-261-261-213-136-162-163-326-261-216-621-162-136-623-627-621-612-627-621-621-621-612-136-126-126 136 125-163-132-672-765-765-765-765-716-1_W.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 362-361 762-726-765-756-765-762-756-753-316-316-327-326-652-235-261-321-567-312-132-263-263-526-326-326-267-672-326-236-263-256-213-321-236-236-326-261-236-263-263-623-326 312-1_W.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 652-621-162-361-627-261-263-261-627-612-312-231-623-632-623-216-316-315-317-326-623-126-316-126-621-617-316-162-637-672-576-675-672-672-162-612-623-632-651-675-361-613-362-673-6_W.mp3)
  Segment 43: 66192ms ~ 66768ms (file: 627-612-361-623-526-615-675-627-657-623-725-371-752-675-675-765-672-657-653-627-672-672-657-652-621-672-621-261-267-623-627-267-621-261-267-276-627-672-627-261-627-267-627-267-6_W.mp3)
  Segment 44: 67768ms ~ 68344ms (file: 673-670-627-267-627-627-620-260-263-230-236-623-260-620-260-236-203-751-761-760-603-316-136-316-136-106-132-137-510-310-215-120-123-216-203-312-312-132-130-312-130-213-310-310-1_W.mp3)
  Segment 45: 69344ms ~ 69920ms (file: 261-627-572-507-135-136-132-312-627-263-236-236-672-362-162-627-612-762-176-263-362-602-627-206-237-267-627-172-103-301-320-230-231-413 413-123_W.mp3)
  Segment 46: 70920ms ~ 71496ms (file: 106-162-310-132-130-132-103-136-312-103-132-136-132-617-506-514-154-106-163-132-173-167-106-267-267-206-206-507-251-316-136-301-316-312-326-312-136-103-312-132-163-763-750-173-1_V.mp3)
  Segment 47: 72496ms ~ 73072ms (file: 172-132-137-132-132-123-132-132-132-132-137-134-123-123-132-132-163_V.mp3)
  Segment 48: 74072ms ~ 74648ms (file: 132-123-132-123-321-312-163-316-136-130-106 167 613-613-326-231-213-132-126-126-136-136-136-136-137-136-127-174-132-173-173-173-173-137-137-135-137-137-137-132-317-136-132 123-2_W.mp3)
  Segment 49: 75648ms ~ 76224ms (file: 631-620-627-627-627-672-617-627-627-627-620-263-263-267 765-607-627-263-623-167-627-612-162-306-632-263-326-260-670-675-675-670-567-163-163-167-612-163-136-312-260-126-601-160 7_V.mp3)
  Segment 50: 77224ms ~ 77800ms (file: 106-123-761-136-137-127-546-162-562-136-106-107-103-106-103-163-106-106-512-135-162-621-106-610-167-671-163-167-612-106-615-163-617-123-761-126-136-751-316-120-612-613-106-263-1_V.mp3)
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
> **Time**: 2026-04-03 06:46:59
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "자기소개 좀 해 주시겠어요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "자기소개", "start_ms": 40, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 270, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "해", "start_ms": 360, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "주시겠어요?", "start_ms": 430, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "제 이름은 김민수입니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "제", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "이름은", "start_ms": 140, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "김민수", "start_ms": 300, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "입니다", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "만나서 정말 반갑습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "만나서", "start_ms": 35, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 225, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "반갑", "start_ms": 355, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "습니다", "start_ms": 485, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "요즘 어떻게 지내세요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "요즘", "start_ms": 45, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 205, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "지내세요?", "start_ms": 355, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "취미가 무엇인가요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "취미가", "start_ms": 30, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "무엇인가요?", "start_ms": 240, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "영화를 보는 것을 좋아해요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "영화를", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "보는", "start_ms": 230, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "것을", "start_ms": 360, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "좋아해요", "start_ms": 480, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "가장 좋아하는 음식이 뭐예요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "가장", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "좋아하는", "start_ms": 160, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "음식이", "start_ms": 310, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "뭐예요?", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "저는 불고기를 아주 좋아합니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "불고기를", "start_ms": 170, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 360, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "좋아합니다", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "가족 관계는 어떻게 되세요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "가족", "start_ms": 60, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "관계는", "start_ms": 190, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 340, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "되세요?", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "부모님과 여동생 한 명이 있어요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "부모님과", "start_ms": 35, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "여동생", "start_ms": 225, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 375, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "명이", "start_ms": 435, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 505, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "어디에 사세요?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "어디에", "start_ms": 45, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "사세요?", "start_ms": 235, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "서울 관악구에 살고 있습니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "서울", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "관악구에", "start_ms": 150, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "살고", "start_ms": 340, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "직업이 무엇인가요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "직업이", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "무엇인가요?", "start_ms": 240, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "저는 대학생입니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 30, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "대학생", "start_ms": 180, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "입니다", "start_ms": 370, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "전공은 무엇을 공부하세요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "전공은", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "무엇을", "start_ms": 230, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "공부하세요?", "start_ms": 360, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "컴퓨터 공학을 공부하고 있어요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "컴퓨터", "start_ms": 55, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "공학을", "start_ms": 225, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "공부하고", "start_ms": 375, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 505, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 18,
"transcript": "어려운 점은 없나요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "어려운", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "점은", "start_ms": 200, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "없나요?", "start_ms": 330, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "가끔 과제가 너무 많아서 힘들어요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "가끔", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "과제가", "start_ms": 160, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 310, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "많아서", "start_ms": 400, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "힘들어요", "start_ms": 520, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "그래도 힘내세요! 응원할게요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "그래도", "start_ms": 35, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "힘내세요!", "start_ms": 185, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "응원", "start_ms": 375, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "할게요", "start_ms": 485, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "감사합니다. 저도 열심히 할게요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "감사합니다.", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "저도", "start_ms": 250, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "열심히", "start_ms": 340, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "할게요", "start_ms": 480, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "주말에는 보통 뭐 하세요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "주말에는", "start_ms": 45, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "보통", "start_ms": 235, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 355, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "하세요?", "start_ms": 425, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "주로 친구들을 만나서 놀아요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "주로", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "친구들을", "start_ms": 170, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "만나서", "start_ms": 360, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "놀아요", "start_ms": 500, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "친구들과 어디에 가세요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "친구들과", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "어디에", "start_ms": 230, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "가세요?", "start_ms": 380, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "강남이나 홍대 근처에 자주 가요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "강남이나", "start_ms": 45, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "홍대", "start_ms": 235, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "근처에", "start_ms": 355, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "자주", "start_ms": 485, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "가요", "start_ms": 555, "dur_ms": 20, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "거기서 무엇을 하고 노나요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "거기서", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "무엇을", "start_ms": 200, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "하고", "start_ms": 330, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "노나요?", "start_ms": 450, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "맛있는 것을 먹고 카페에 가요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "맛있는", "start_ms": 40, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "것을", "start_ms": 210, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "먹고", "start_ms": 330, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "카페에", "start_ms": 440, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "가요", "start_ms": 560, "dur_ms": 10, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "재미있는 시간이 되겠네요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "재미있는", "start_ms": 45, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "시간이", "start_ms": 235, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "되겠네요", "start_ms": 395, "dur_ms": 170, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 30,
"transcript": "혹시 운동하는 것 좋아하세요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "혹시", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "운동하는", "start_ms": 160, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 350, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "좋아하세요?", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "네, 저는 수영을 좋아해요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "저는", "start_ms": 140, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "수영을", "start_ms": 260, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "좋아해요", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "수영을 얼마나 자주 하세요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "수영을", "start_ms": 40, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "얼마나", "start_ms": 210, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "자주", "start_ms": 360, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "하세요?", "start_ms": 450, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "일주일에 세 번 정도 가요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "일주일에", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "세", "start_ms": 240, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "번", "start_ms": 330, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "정도", "start_ms": 410, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "가요", "start_ms": 510, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "정말 대단하시네요!",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 35, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "대단하시네요!", "start_ms": 205, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "건강을 위해서 꾸준히 하려고요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "건강을", "start_ms": 40, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "위해서", "start_ms": 210, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "꾸준히", "start_ms": 360, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "하려고요", "start_ms": 490, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "저도 수영을 배워보고 싶어요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "저도", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "수영을", "start_ms": 160, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "배워보고", "start_ms": 330, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "싶어요", "start_ms": 490, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "나중에 기회가 되면 같이 가요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "나중에", "start_ms": 45, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "기회가", "start_ms": 195, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "되면", "start_ms": 325, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 445, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "가요", "start_ms": 525, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "네, 꼭 그러고 싶네요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "꼭", "start_ms": 130, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "그러고", "start_ms": 230, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "싶네요", "start_ms": 380, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "오늘 대화 즐거웠습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 55, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "대화", "start_ms": 195, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "즐거웠", "start_ms": 345, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "습니다", "start_ms": 505, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "저도요. 다음에 또 봐요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "저도요.", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "다음에", "start_ms": 230, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 380, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "봐요", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "네, 조심히 가세요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 35, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "조심히", "start_ms": 125, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 335, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "안녕히 계세요",
"speaker": "speaker_2",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 45, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "계세요", "start_ms": 275, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 44,
"transcript": "벌써 시간이 이렇게 됐네요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "벌써", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "시간이", "start_ms": 200, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 350, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "됐네요", "start_ms": 490, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "이제 슬슬 가봐야겠어요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "슬슬", "start_ms": 170, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "가봐야", "start_ms": 300, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "겠어요", "start_ms": 450, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "오늘 정말 즐거웠습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 55, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 195, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "즐거웠", "start_ms": 325, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "습니다", "start_ms": 485, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "연락처 좀 알 수 있을까요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "연락처", "start_ms": 60, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 250, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "알", "start_ms": 330, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 380, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "있을까요?", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "네, 제 번호는 공일공...",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "제", "start_ms": 130, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "번호는", "start_ms": 200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "공일공...", "start_ms": 360, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "다음에 꼭 다시 봬요!",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 45, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "꼭", "start_ms": 195, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 285, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "봬요!", "start_ms": 405, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "네, 제가 연락드릴게요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 130, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "연락", "start_ms": 250, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "드릴게요", "start_ms": 380, "dur_ms": 190, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 06:46:59*
