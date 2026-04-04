# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=14524  time=04:32:52  uptime=2963s  mem=2117MB  system_processes=696"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260401_043156.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 130-216-261-620-302-360-302-301-713-751-375-137-260-260-260-627-625-256-256-215-256-256-527-253-526-103-106-126-260-302-612-301-132-760-306-130-361-106-106-103-103-136-132-134-1_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 263-627 132-654-251-605-675-126-136-132-167-103-106-103-613-103-615-167-612-162-263 167-657-156-617-312-326-361-173-136-103-163-610-136-316-160-215-302-756-136-136-136-126-203-6_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 103-103-720-167-156-106-263-170-162-132-231-320-230-102-123-130-321-123-213-301-103-130-135-570-572-206-260-206-302-326-620-203-102-106-517-150-130-103-316-136-103-103-213-206-2_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 312-576-572-136-153-612-162-761-127-537-261-132-137-312-627-263-267-627-137-613-126-312-163-103-263-213-132-132-103-102-132-361-360-163-610-312-132-150-136-362-301-163-103-236-6_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 623-162-176-213-206-261-263-726-576-576-765-672-617-261-601-627-657-671-672-672-675-675-106-761-517-765-715-765-621-163-561-105-574-501-571-756-657-213-106-157-657-675-627-506-6_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 132-130-132-631-623-123-265-267-621-107-103-106-163-132-126-261-627-216-167-137-715-267-617-671-125-612-167-136-103-163-167-165-160-106-106-130-103-167-517-103-132-671-106-167-1_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 126-213-301-316-361-763-276-106-162-267-672-670-627-267-320-326-126-237-627-672-167-107-176-107-765-705-750-713-230-320-302-203-103-103-326-762-170-615-320-320-570-576-576-576-5_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 216-216-216-326-321-236-216-216-213-213-217-216-216-261-263-236-236-236-263-263-326-236-263-326-362-361-326-326-326-231-326-326-236-261-261-236-231-213-132-213-321-217-271-217-2_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 612-623-625-263-321-623-263-671-167-671 627-356-576-571-576-571-756-765-761-672-176-137-571-671-317-173-216 136-165-136-123-156-160-136-130-103-106-561-165-516-162-517-751-765-1_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 365-365-362-634 657-651-652-257-257-574-521-762-361-173-376-716-762-376-371-376-167-712-762-712-175-312-217-723-172-217-267-362-132-127-517-517-527-352-514-537-541-540 321-236-6_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 612-602-531-531-517-543-263-235-305-507-527-256-132-126-136-321-251-326-231-617-651-162-325-236-326-712-127-127-132-631-361-526-513-574-152-537-163-136-160-361-756-627 107-106-1_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 671-103-136-103-675-627-162-126-163-167-106-136-163-162-132-103-132-123-126-512-675-176-162-130-132-103-132-162-635-267_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 236-321-362-326-625-325-576-356-625-357-615-627-263-261-623-607-362-657-235-326-567-326-231-312-261-215-652-543-537-574-571-567-216-156-756-574-135-213-267-327-312-167-631-352-3_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 136-162-103-103-136-103-162-612-612-613-621-613-162-260-206-627-236-132 320-203-503-753-756-571-574-570-756-617-132-276-620-620-260-623-106-163-612-617-672-671-671-167-103-216-1_V.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 367-367-357-357-357-357-375-362-165-315-351-357-351-321-312-123-162-162-126-126-216-167-163-326-613-526-357-537-365-123-326-325-637-613-163-156-512-512-571-567-153-216-513-517-1_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 162-162-621-321-302-301-301-612-162-130-263-236-320-612-123-326-301-132-637-163-163-731-621-672-612-607-601-672-620-620-120-130-120-312-210-103-263-327-675-267-613-670-672-213-6_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 213-320-302-321-231-236-132-312-260-620-203-203-160-621-216-201-216-206-620-602-263-261-263-657-601-615-106-162-612-162-106-162-106-162-106-167-674-602-620-621-765-763-613-102-1_V.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 621-627-627-623-632-265-625-613-632-627-632-673-672-672-627-673-627-261-267-256-215-215-215-524-625-231-321-231-217-231-127-327-275-237-231-213-213-217-237-362-763-163-136-361-2_W.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 102-316-632-613-670-671-613-632-613-126-574-574-571-132-132-126-132-162-123-102-136-216-127-126-162-126-123-126-126-162-120-210_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 576-567-567-576-576-576-576-567-576-576-567-567-576-567-576-567-526-576-567-576-567-576-567-576-576-567-567-576-567-576-567-576-567-567-576-576-756-567-567-576-576-567-576-572-5_W.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 320-236-231-132-310-130-123-263-216-326_W.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 231-361-675-316-103-316-163-130-103-175-175-160-103-176-163-167-136-162-103-320-162-576-126-103-167-163-301-103-103-760-537-173-167-561-135-153-162-163_V.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 576-567-756-765-265-256-265-265-265-256-265-265-265-261-621-265-216-261-132-576_W.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 263-625-236-627-652-265-265 163-103-106-162-103-536-671-617-162-126-263-263-126-261-103-160-136-136-136-162-106-136-103-103-103-175-107-562-612-612-610-671-167-106-160-103-136-1_V.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 567-162-623-621-160-165-651-167-175-317-156-517-156-157-162-162-267-726-726-726-762-726-672-762-725-765-762-276-267-276-236-627-576-615-134-617-107-103-567-527-276-162-167-617 5_W.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 235-236-236-326-237-352-534-537-537-325-356-352-325-321-327-326-326-321-320-235-256-257-265-312-231-625-263-625-263-236-230-256-276-267-263-265-265-263-263-263-260-157-157-547-7_W.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 352-657-637-523-253-257-237-327-312-132-315-136-316-316-361-326-256-532-256-263-263-263-256-532-537-325-235-326-362-357-352-352-321-213-321-321-213-237-236-236-326-321-312-321-1_W.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 275-572-751-673-235-256-572-523-573-573-253-257-574-520-352-251-537-527-254 576-751-563-571-574 657-567-567-571-561-547-536-536-534-354-354-351-354-312-325-365-165-132-361-316-3_W.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 765-657-567-657-657-651-136-316-102-103-103-162-136-617-301_W.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 176-610-123-312-126-301-103-316-765-106-107-130-106-617-617-162-103-136-132-103-106-621-103-103-136-514-162-210-265-612-136-106-613-612-136-126-312-163-160-136-267-632 765 167-1_W.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 103-163-163-132-167-765-163-106-610-165-136-106-613-167-103-167-163-103-702-615-670-567-136-761-216-162-231-167-106-167-103-164-136-572-513-162-216-126-103-163-316-162-103-657-1_V.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 302-306-362-312-316-310-231-210-253-231-213-251-216-321-213-137-103-103-106-103_W.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 236-213-236-672-754-726-162-106-162-756-672-627-267-236-326-620-263-201-102-167-326-310-130-361-105-106-167-617-163-167-162-621-623 675-670-673-326-302-672-675-672-607-612-621-2_W.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 621-261-263-672-672-625-267-627-672-762-627-627-574-576-275-726-732-372-763-627-672-762-576-576-576-756-756-273-237-713-672-627-675-316-361-632-367-635-627-672-130-651-651-326-6_W.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 260-263-126-127-623-263-631-602-103-671-167-160-136-106-312-561-136-167-132-610-162-103-615-132-103-612-102-671-167-132-103-137-163-106-132-106-310-316-163-367-156-162-106-162-1_V.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 261-302-321-261-613-126-165-637-136-603-106-130-126-123-130-106-103-567-762-625-160-260-162-261-126-163-106-106-165-103-136-162-213-102-715-103-103-136-163-136-361-132-135-173-3_V.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 165-617-603-263-630-631-136-160-260-623-623-610-160-310-361-106-106-106-106-136-103-136-136-103-312-301-413-312-756-756-103-132-136-103-316-675-567-561-301-126-612-163-631-620-6_V.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 106-167-167-567-563-617-310-163-103-675-106-615-613-612-167-106-106-156-130-651-561-301-136-617-236-623-136-657-316-617-167-160-160-103-623-256-203-236-216-576-672-127 132 136-1_V.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 563-675-657-576-576-657-675-653-576-657-657-657-567-567-617-561-567-576-156-567-561-561-652-567-651-567-567-657-567-561-567-576-567-576-567-576-567-576-567-576-576-567-567-576-5_W.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 165-123-126-632-612-126-163-321-632-263-136-652-652-675-615-361-136-321-132-213-326-321-321-327-315-136-312-273-237-273-637-326-326-326-321-326-317-372-327-367-176-213-126-123-1_W.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 537 103-153-610-167-617-163-136-137-103-163-316-103-130-162-102-107-760-631-176-761-302-132-130-102-106-136-103-163-316-276-130-137-132-621-106-206-762-762-672-260-160-106-106-1_V.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 526-562-652-265-256-267-263-321-261-261-627-625-627-276-267-263-261-216-260-567-265-265-267-260-527-572-357-576-523-562-125-312-326-236-321-321-236-625-326-623-621-267-123-231-3_W.mp3)
  Segment 43: 66192ms ~ 66768ms (file: 306-103-132-103-102-132-126-123-321-562-103-306-765-765-132-132-501-150-135-370-173-132-234-315-230-632-231-326-321-301-302-321-302-621-613-136-276-176-271-206-162-126-152-162-6_V.mp3)
  Segment 44: 67768ms ~ 68344ms (file: 132-103-163-627-167-263-617-612-106-610-163-167-162-207-612-162-620-625-106-617-106-613-162-723-126-132-130-167-617-160-136-132-103-615-671-765-765-176-716-167-615-326-213-125-6_V.mp3)
  Segment 45: 69344ms ~ 69920ms (file: 132-627-602-261-206-106-103-136-651-321-260-561-632-613-163-765-673-167-106-613-615-631-613-612-612-617-631-162-107-312-316-671-321-273-273-230-326-120-132-157-561-310-321-637-6_V.mp3)
  Segment 46: 70920ms ~ 71496ms (file: 132-103-173-106-367-763-206-321-162-130-120-216-261-265-620-126-310-216-231-132-136-316-576-571-263-236-267-236-132-136-362-621-263-236-261-213-326-263-260-213-627-267-316-310 1_W.mp3)
  Segment 47: 72496ms ~ 73072ms (file: 612-123-167-103-106-261-126-316-301-612-623 574-530-302-302-236-203-230-235-523-102-106-162-126-162-163-756-765-613-620-612-361-316-136-103-132-123-167-203-527-301-361-261-103-3_V.mp3)
  Segment 48: 74072ms ~ 74648ms (file: 167-106-102-167-167-103 547-567-573-751-675-672-673-675-671-576-573-540-564-657-675-165-517-571-753-627-672-326-765-672-673-617-601-261-261 312-672-670-670-627-673-165-673-615-1_W.mp3)
  Segment 49: 75648ms ~ 76224ms (file: 136-163-765-627-627-136-312-213-162-602-621-362-627-136-106-136-601-621-602-261-206-260-620-260-201-516-162-320-302-130-301-316-136-160-136-163-621-316-673-260-251-206-103-765-1_V.mp3)
  Segment 50: 77224ms ~ 77800ms (file: 317-315-365-315-513-503-315-105-135-310-130-301-130-312-135-356-365-317-156-517-156-135-153-315-371-361-137-316-125-153-135-135-517-351-315-165-136-153-316-365-561-365-563-153-1_W.mp3)
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
> **Time**: 2026-04-01 04:34:13
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "안녕하세요 반갑습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 40, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 380, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "오늘 날씨가 정말 좋네요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 30, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 160, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 320, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "공부하기 딱 좋은 날씨예요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "공부하기", "start_ms": 50, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "딱", "start_ms": 270, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 360, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "날씨예요", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "한국어 공부는 재미있나요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 20, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "공부는", "start_ms": 210, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "재미있나요", "start_ms": 360, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "조금 어렵지만 보람차요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "조금", "start_ms": 60, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "어렵지만", "start_ms": 200, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "보람차요", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "매일 꾸준히 하는 게 중요해요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "매일", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "꾸준히", "start_ms": 160, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 310, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 400, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "중요해요", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "포기하지 말고 힘내세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "포기하지", "start_ms": 35, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "말고", "start_ms": 245, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "힘내세요", "start_ms": 365, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "언제든지 물어보세요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "언제든지", "start_ms": 50, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "물어보세요", "start_ms": 290, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "친절하게 설명해 드릴게요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "친절하게", "start_ms": 40, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "설명해", "start_ms": 260, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "드릴게요", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "감사합니다 많은 도움이 돼요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 30, "dur_ms": 260, "phase_lr_deg": 0.0},
{"word": "많은", "start_ms": 300, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "도움이", "start_ms": 410, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "돼요", "start_ms": 530, "dur_ms": 40, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "자 이제 다음 문장을 볼까요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "자", "start_ms": 45, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 115, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 225, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "문장을", "start_ms": 335, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "볼까요", "start_ms": 485, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "이 단어는 어떻게 읽나요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 20, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "단어는", "start_ms": 90, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 250, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "읽나요", "start_ms": 400, "dur_ms": 170, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "천천히 따라 해 보세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "천천히", "start_ms": 50, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "따라", "start_ms": 270, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "해", "start_ms": 380, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "보세요", "start_ms": 450, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "발음이 아주 정확하시네요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "발음이", "start_ms": 40, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 210, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "정확하시네요", "start_ms": 330, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "칭찬해 주시니 기뻐요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "칭찬해", "start_ms": 60, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "주시니", "start_ms": 250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "기뻐요", "start_ms": 410, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "더 열심히 연습할게요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "더", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "열심히", "start_ms": 120, "dur_ms": 190, "phase_lr_deg": 0.0},
{"word": "연습할게요", "start_ms": 320, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "금방 익숙해질 거예요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "금방", "start_ms": 45, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "익숙해질", "start_ms": 195, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 425, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "자신감을 가지고 말해 보세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "자신감을", "start_ms": 35, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "가지고", "start_ms": 225, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "말해", "start_ms": 375, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "보세요", "start_ms": 465, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "실수해도 괜찮으니까요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "실수해도", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "괜찮으니까요", "start_ms": 260, "dur_ms": 310, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 21,
"transcript": "네 알겠습니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 120, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "알겠습니다", "start_ms": 230, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "질문 하나 더 해도 될까요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "질문", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "하나", "start_ms": 170, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 290, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "해도", "start_ms": 360, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "될까요", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "그럼요 무엇이든 물어보세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "그럼요", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "무엇이든", "start_ms": 190, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "물어보세요", "start_ms": 380, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "이 문법이 이해가 안 돼요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 55, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "문법이", "start_ms": 125, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이해가", "start_ms": 285, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 425, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "돼요", "start_ms": 495, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "예문을 보면서 설명해 줄게요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "예문을", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "보면서", "start_ms": 190, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "설명해", "start_ms": 330, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "줄게요", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "아 이제 이해가 가네요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "아", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 120, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "이해가", "start_ms": 240, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "가네요", "start_ms": 380, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "다행이네요 다음으로 넘어갈까요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "다행이네요", "start_ms": 45, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "다음으로", "start_ms": 235, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "넘어갈까요", "start_ms": 385, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "오늘 수업은 여기까지입니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 30, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "수업은", "start_ms": 150, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "여기까지입니다", "start_ms": 300, "dur_ms": 270, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "수고 많으셨습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "수고", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "많으셨습니다", "start_ms": 180, "dur_ms": 340, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "선생님도 수고하셨습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "선생님도", "start_ms": 40, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "수고하셨습니다", "start_ms": 260, "dur_ms": 310, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "다음 시간에 또 만나요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "다음", "start_ms": 35, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "시간에", "start_ms": 155, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 295, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 365, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "안녕히 계세요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 150, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "계세요", "start_ms": 340, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "네 안녕히 가세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 40, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 120, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 310, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "(music)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 35,
"transcript": "복습 잊지 마시고요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "복습", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "잊지", "start_ms": 180, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "마시고요", "start_ms": 300, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "숙제는 이메일로 보내주세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "숙제는", "start_ms": 30, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "이메일로", "start_ms": 200, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "보내주세요", "start_ms": 390, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "확인 후에 답장 드릴게요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "확인", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "후에", "start_ms": 165, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "답장", "start_ms": 265, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "드릴게요", "start_ms": 395, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "그럼 즐거운 하루 보내세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 35, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "즐거운", "start_ms": 155, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "하루", "start_ms": 305, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "보내세요", "start_ms": 425, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 40,
"transcript": "건강 조심하시고요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "건강", "start_ms": 60, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "조심하시고요", "start_ms": 210, "dur_ms": 310, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "웃는 얼굴로 다시 봐요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "웃는", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "얼굴로", "start_ms": 170, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 330, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "봐요", "start_ms": 450, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "매일 조금씩 나아질 거예요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "매일", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "조금씩", "start_ms": 170, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "나아질", "start_ms": 330, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 480, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "꾸준함이 가장 큰 무기예요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "꾸준함이", "start_ms": 30, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 220, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 340, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "무기예요", "start_ms": 410, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "스스로를 믿고 계속하세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "스스로를", "start_ms": 45, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "믿고", "start_ms": 235, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "계속하세요", "start_ms": 355, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "어려운 건 언제든 도와줄게요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "어려운", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "건", "start_ms": 200, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "언제든", "start_ms": 260, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "도와줄게요", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "오늘 정말 잘하셨어요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 35, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 155, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "잘하셨어요", "start_ms": 285, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "우리 함께 성장해 봐요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "우리", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "함께", "start_ms": 170, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "성장해", "start_ms": 290, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "봐요", "start_ms": 450, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "작은 습관이 변화를 만들어요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "작은", "start_ms": 30, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "습관이", "start_ms": 160, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "변화를", "start_ms": 310, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "만들어요", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "오늘도 수고 많았습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "오늘도", "start_ms": 45, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "수고", "start_ms": 195, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "많았습니다", "start_ms": 315, "dur_ms": 260, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "그럼 이제 마칠게요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 160, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "마칠게요", "start_ms": 280, "dur_ms": 240, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-01 04:34:13*
