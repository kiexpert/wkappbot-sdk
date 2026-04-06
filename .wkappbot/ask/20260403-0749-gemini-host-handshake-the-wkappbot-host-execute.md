# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=07:48:36  uptime=23361s  mem=1776MB  system_processes=453"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_074826.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 162-326-132-623-623-312-162-603-326-213 325-547-315-134-132-621-613-316-162-136-132-103-103-107-672-672-672-627-267-621-261-260-631-623-527-623-623-231-326-312 321-612_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 132-136-157-154 617-316-562-361-627 627-612_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 602-612-231-362-657-657-310-132-670-675-762-670-107-107-301-136-103-517-576-756-136-312-203-320-132-136-312-261-621-312-213-231-302-132-540-574-312-620-621-627-623-621-136-162-2_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 267-276-765-316-106-310-206-132-103-501-506-106-132-273-321-320-263-321-736-320-230-326-160-326-236-613-613-163-612-612-517-501-125-213-510-106-106-106-362-631-106-326-320-351-1_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 321-312-260-203-320-236-320-236-321-602-620-602-620-627-263-230-302-123-126-571-657-756-601-601-106-163-206-206-231-362-531-657-316-603-327-536-531-567-576-756-265-165-362-574-5_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 213-162-167-106-162-163-316-130-231-253-206-260-206-263-203-132-230-236-302-320-236-371-301-302-326-573-750-102-123-612-132-310-312-310-103-132-103-123-312-236-260-206-621-236-2_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 106-103-163-236-176-126-136-132-102-102-136-167-163-160-103-103 103-130_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 567-132-321-213-756-526-657-657-726-576-657-265-173-576-756-675 574-632-261-623-621-765-760-763-705-532-376-162-631-562-567-625-620-612-673-162-716-761-126-765-601-672-672-672-6_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 567-317-170 671-126-167-716-201-271-260-263-206-103-632 162-167-567-546-574 326 162-261-632-514-517-675-361-231-324-675-136-167-615-163-612-673-621-657-716-631-630-620-106-135-1_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 563-615-163-163-631-261-312-163-136-123-130-137-367-360-657-675-675-673-765-265-263-132-316-317-317-163-106-605-231-321-321-527-526-567-572-756 157-170-564-627-103-617-603-162-6_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 263-206-267-620-620-602-206-620 625-623-231-263-536-623-631-312-513-516-632-621-637-162-236-230-160-165-167-236-610-617-761-671-260-602-601-163-160-106-106-106-612-613-106-160-6_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 675-765-672-632-136-213-130-756-756-765-763-672-621-632 130-160-617-317-137-136 623-326-263-123-362-231-312_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 756-756-765-672-627-632-361-376-675-376-316-306-163-106-103-136-316-136-132-206-206-261-126-760-761-736-317 132-136-123-312 231 165 671-607-672-612-647-162-617-672-620 613-134-1_V.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 162-103-261-236-261-302-261-510-576-576-675-672-632-623-326-320-230-132-320-320-231-320-236-263-236-761-310-301-130-103-513-310-614-203-230-326-617-125-715-526-137-637-362-362 1_V.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 137-267-206-261-216-607-672-607-206-263-167-137-273-163-106-106-103-137-310-307-617-613-631-613-312-571-320-320-260-230-236-236-236-214-126-217-236-217-263-123-107-103-627-620-7_V.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 756-675-765-765-103-136-163-137-607-617-601-612-705-607-672-675-671-672-163-162-576-756-675-675-675-670-106-601-620-623-672-367-362-762-301-610-163-167-137-302-263-571-754-731-3_V.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 574-574-765-763-756-765-765-715-710-631-761-731-136-102-167-163-126-103-612-617-130-167-106-163-760-120-167-102-102-106-132 260 621-715-173-103-562-620 103 130-713-132-153-130-1_V.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 765 675-672-612-361-623-162-621-216-162_W.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 762-627-672-623-310-306-670-601-361-162-613-362-306-103-312 267-267-621-162_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 103-130-310-267-623 672-672-672-627-623-236-623 675-576-657-675-672_W.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 132-126-321-132-123-143-132-134-165-137-132-312-134-413-475-413-132-143-413-627-751-264 132-312-132-214 706-351-760-371-137-132-312-671-621-267-326-261-132-573-507-321-231-236-3_V.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 627-267-762-706-701-570-601-162-620-620-163-603-706-206-203-362-321-301-106-163-165-106-106-105-751-760-762-126-103-517-157-231-765-765-167-157 765-765-675-672-362-576-263-206-2_W.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 302-230-236-623-320-321-370-321-237-310-213-327-320-326-326-231-326-362-362-632-632-627-623-632-136-316-316-302-301-103-316-316-163-103-132-126-123-763-612-236-162-126-612-162-2_W.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 672-672-126-162-176-167-657-756-675-167-621-621-136-631-162-263-612-671-136-310-162-316-765-761-765-756-625-657-657-625-326-163-567-651-621-567-657-675-637-617-617-635-627-637-6_W.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 765-765-765 103-136-607-267-167-176-172-162-612-126-612-652-132-167-106-106-106-367-102-132 301-316-576-156-167-126-675-567-567-765-576-160-361-362-321-260-267-362-631-136-130-6_W.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 371-312-126-120-206-201-201-123-216-132-136-263-261-132-126-102-123-132-321_V.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 162-165-670-625-627 267-267-263-123 613-317_W.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 251-517-562-561-651-625-265-265-265-627-276-267-276-216-537-512-572-352-574-576-537-570-275-325-265-236-261-326-213-326-321-126-312-321-231-127-213-321-231-123-253-527-512-257-2_W.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 763-756-567-576-257-527-156-567-653-657-671-765-756-571-756-671-657-751-765-675-657-576-725-652-652-156-576-516-523-256-562-256-165-651-165-165-216-576-156-567-657-156-563-567-6_W.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 361-301-326-321-306-360-263-623-312-612-564-504-546-650-613-623-627-567-163-621-602-162-103-103-126-261-621-206-602-621-302-620-213-263-261-621-162-103-126-316-132-153-135-672-6_V.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 675-760-162-135-137-571-576-567-760-617-106-103-620-207-603-162-106-617-601-627-260-216-213-103-163-165-672-567-576-567-163-156-305-375-537-536-567-657-657-657-123-130-317-317-6_W.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 163-567-176-127-162-103-103-123-301-123-163-132-136-167-165-150-567-315-310-103-150-136-316-136-136-143-763-132-570-526-265-256-206-123-310-163-136-106-106-321-302-715 537-627-3_V.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 123-132-312-302-307-530-351-632-312-731-756-706-127-570-507-574-154-132 136-103-132-631-613-163-130-361-167-627-206-320-230-267-213-675-672-735-765-756-765-763-312-310-130-756-6_W.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 132-627-602-623-675-173-163-165-573-321-132-301-361-203-672-126-657-256-235-326-267-765-132-135-652-517-362-312-574-510-175-317-572-537-607-573-765-302-325-327-237-302-312-157-2_W.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 136-612-316-317-175-103-163-105-567-371-762-153-706-173-102-706-136-361-123-125-362-671-126-215-316-613-163-106-613-617-103-130-127-160-613-106-167-136-160-761-106-107-126-176-1_V.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 357-315-175-165-156-537-357-574-540-254-527-571-251-125-126-162-123-152-154-514-156-573-315-175-175-175-175-517-706-765-176-716-761-170-715-751-235-253-215-231-123-312-137-716-1_W.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 267-260-261-657-260-632-623-235 136-132-316-316-326-263-321-321-312-213-321-310-301-617-627-640-657-637-362-362-612-613-261-132-216-312-162-316-657-671-312-627-627-631-162-671-5_W.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 671 132-263-162-213-312-123 602-267 103-301-163_W.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 263-263-261-627-267-263-326-265-573-576-756-765-671-612-162-236-236-652-635-135-756-375-357-576-765-236-263-260-123-267-576-756-756-317-321-312-321-536-356-650-650-651-574-576-5_W.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 302-321-301-132-673-637-603-612-263-310-102-132-103-132-216-631-102-312-132-156-613-106-612-612-602-627-362-361-103-103-123-126-216-623-216-231-231-123-321-312-132-327-216-621-2_V.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 261-123-136-126-136-126-162 627-326-623-632-765-765-761-761-763-762-762-623-312 163-263_W.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 765-670-617 267-237-236 574 261 132-132-126_W.mp3)
  Segment 43: 66192ms ~ 66768ms (file: 326-325-325-316-351-315-723-375-316-362-326-321-321-361-316-216-231-236-263-236-236-236-236-236-231-236-326-321-321-237-230-320-230-320-230-230-203-520-520-572-157-137-127-534-5_W.mp3)
  Segment 44: 67768ms ~ 68344ms (file: 153-537 103-320-102-162-617-671-617-167-165-657-321-320-321-102-130-103-103-154-123-754-765-627-627-236-320-236-302-260-620-260-620-752-206-756-352-265-267-273-726-705-275-627-7_W.mp3)
  Segment 45: 69344ms ~ 69920ms (file: 547-574-547-547-546-564-547-310-312-312-312-312-321-312-231_W.mp3)
  Segment 46: 70920ms ~ 71496ms (file: 107 106-106-316-671-260-263-260-260-206 540-574-736-726-617-160-162-326-361-321-163-362-306-360-362-261-132-132-103-310-231-312-320-230-230-623-326-163-216-132-136-103-106-130-3_W.mp3)
  Segment 47: 72496ms ~ 73072ms (file: 106-165-162-132-761-321-126-132-123-310-132-103-103-103-103-623-230-130-173-103-517-306-136-152-137-103-106-136-136-136-617-602-625-605-106-167-260-413-132 103-103-106-167-106-3_V.mp3)
  Segment 48: 74072ms ~ 74648ms (file: 672-672-671-637-263-627-627-213-123-631-136-613-612-167-672-672-731-675-672-236-267-756-756-765-657-362-621 572-172-627-627-621-216_W.mp3)
  Segment 49: 75648ms ~ 76224ms (file: 325-256-652-652-652-263-216-261-216-261-276-126-561-562-526-532-325-251-627-675-627-623-162-132-321-263-216-162-271-217-321-312-321-132-312-312-326-263-267-627-372-327-265-276-2_W.mp3)
  Segment 50: 77224ms ~ 77800ms (file: 672-623 160-103-316-136-571-570-657-163-173-301-136-612-610-621-271-621-126-601-612-625-603-615-603-162-103-163-136-156-136-106-106-152-653-617-162-136-103-163-162-167-256-613-6_V.mp3)
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
> **Time**: 2026-04-03 07:49:51
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "오늘 저녁에 뭐 할 거예요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "저녁에", "start_ms": 160, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 300, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 370, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "거예요?", "start_ms": 440, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "친구들과 영화 보러 가기로 했어요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "친구들과", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "영화", "start_ms": 210, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "보러", "start_ms": 330, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "가기로", "start_ms": 430, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "했어요", "start_ms": 540, "dur_ms": 36, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "재미있겠네요 어떤 영화인데요?",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "재미있겠네요", "start_ms": 45, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 240, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "영화인데요?", "start_ms": 350, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "최근에 개봉한 액션 영화예요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "최근에", "start_ms": 60, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "개봉한", "start_ms": 180, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "액션", "start_ms": 310, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "영화예요", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "I love action movies as well.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 35, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "love", "start_ms": 95, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "action", "start_ms": 195, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "movies", "start_ms": 315, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "as", "start_ms": 425, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "well.", "start_ms": 495, "dur_ms": 81, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "그럼 다음에 같이 보러 가요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 50, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "다음에", "start_ms": 150, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 270, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "보러", "start_ms": 360, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "가요", "start_ms": 460, "dur_ms": 116, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 8,
"transcript": "좋은 생각이에요 언제 갈까요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 40, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "생각이에요", "start_ms": 140, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "언제", "start_ms": 300, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "갈까요?", "start_ms": 410, "dur_ms": 166, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "다음 주말 어때요? 시간 비워둘게요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "다음", "start_ms": 55, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "주말", "start_ms": 145, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "어때요?", "start_ms": 255, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "시간", "start_ms": 365, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "비워둘게요", "start_ms": 465, "dur_ms": 111, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "네 좋아요 제가 연락할게요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 45, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "좋아요", "start_ms": 105, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 215, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "연락할게요", "start_ms": 305, "dur_ms": 226, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "팝콘도 꼭 사 먹어야 해요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "팝콘도", "start_ms": 60, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "꼭", "start_ms": 190, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "사", "start_ms": 270, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "먹어야", "start_ms": 330, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "해요", "start_ms": 450, "dur_ms": 126, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "당연하죠 전 캐러멜 맛을 좋아해요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "당연하죠", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "전", "start_ms": 200, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "캐러멜", "start_ms": 270, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "맛을", "start_ms": 390, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "좋아해요", "start_ms": 470, "dur_ms": 106, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "저랑 취향이 비슷하시네요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "저랑", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "취향이", "start_ms": 130, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "비슷하시네요", "start_ms": 260, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "That's great, we can share it.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "That's", "start_ms": 35, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "great,", "start_ms": 135, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "we", "start_ms": 235, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "can", "start_ms": 285, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "share", "start_ms": 345, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "it.", "start_ms": 475, "dur_ms": 101, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "벌써부터 다음 주가 기다려져요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "벌써부터", "start_ms": 45, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 195, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "주가", "start_ms": 295, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "기다려져요", "start_ms": 385, "dur_ms": 191, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "저도요 이번 주는 빨리 가겠네요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "저도요", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이번", "start_ms": 150, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "주는", "start_ms": 250, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "빨리", "start_ms": 340, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "가겠네요", "start_ms": 440, "dur_ms": 136, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "영화 보고 나서 맛있는 것도 먹어요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "영화", "start_ms": 50, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "보고", "start_ms": 150, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "나서", "start_ms": 240, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "맛있는", "start_ms": 330, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "것도", "start_ms": 450, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "먹어요", "start_ms": 540, "dur_ms": 36, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "치킨 어때요? 영화엔 치킨이죠",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "치킨", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "어때요?", "start_ms": 155, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "영화엔", "start_ms": 275, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "치킨이죠", "start_ms": 405, "dur_ms": 171, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "완벽한 계획이에요 기대되네요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "완벽한", "start_ms": 60, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "계획이에요", "start_ms": 200, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "기대되네요", "start_ms": 350, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "그럼 오늘 저녁 친구들과 잘 놀아요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 130, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "저녁", "start_ms": 230, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "친구들과", "start_ms": 330, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 470, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "놀아요", "start_ms": 530, "dur_ms": 46, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "Have a wonderful evening!",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "Have", "start_ms": 35, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 135, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "wonderful", "start_ms": 175, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "evening!", "start_ms": 325, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "고마워요 당신도 좋은 시간 보내요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "고마워요", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "당신도", "start_ms": 160, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 280, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "시간", "start_ms": 370, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "보내요", "start_ms": 470, "dur_ms": 106, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "내일 또 이야기 나눠요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "내일", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 150, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "이야기", "start_ms": 210, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "나눠요", "start_ms": 330, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "네 그럴게요 잘 자요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 45, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "그럴게요", "start_ms": 105, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 245, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "자요", "start_ms": 315, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "Good night and see you tomorrow.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Good", "start_ms": 30, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "night", "start_ms": 130, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "and", "start_ms": 220, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "see", "start_ms": 270, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 360, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "tomorrow.", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "안녕히 계세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "계세요", "start_ms": 165, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 28,
"transcript": "언제나 건강 유의하세요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "언제나", "start_ms": 60, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "건강", "start_ms": 180, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "유의하세요", "start_ms": 280, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "고마워요 당신도요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "고마워요", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "당신도요", "start_ms": 160, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "I'll be rooting for you.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "I'll", "start_ms": 35, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "be", "start_ms": 105, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "rooting", "start_ms": 155, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "for", "start_ms": 295, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "you.", "start_ms": 365, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "정말 든든하네요 감사합니다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 45, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "든든하네요", "start_ms": 145, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 295, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "그럼 이제 진짜 안녕히",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 130, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 210, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 310, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "Bye bye for now!",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "Bye", "start_ms": 35, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "bye", "start_ms": 125, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "for", "start_ms": 215, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "now!", "start_ms": 275, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "내일 봐요 안녕",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "내일", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "봐요", "start_ms": 155, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "안녕", "start_ms": 255, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "오늘도 수고 많으셨습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "오늘도", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "수고", "start_ms": 170, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "많으셨습니다", "start_ms": 260, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "감사합니다 수고하셨어요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 40, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "수고하셨어요", "start_ms": 210, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "평안한 밤 되시길 바랍니다",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "평안한", "start_ms": 55, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "밤", "start_ms": 175, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "되시길", "start_ms": 235, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "바랍니다", "start_ms": 335, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "좋은 꿈 꾸세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 45, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "꿈", "start_ms": 145, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "꾸세요", "start_ms": 205, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "See you in my dreams, maybe!",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 35, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 105, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 165, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "my", "start_ms": 215, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "dreams,", "start_ms": 275, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "maybe!", "start_ms": 395, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "그럴지도 모르겠네요 안녕히",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "그럴지도", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "모르겠네요", "start_ms": 170, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 310, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "진짜로 인사를 몇 번이나 하는지",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "진짜로", "start_ms": 60, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "인사를", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "몇", "start_ms": 290, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "번이나", "start_ms": 350, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "하는지", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "정이 많아서 그런가 봐요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "정이", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "많아서", "start_ms": 130, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "그런가", "start_ms": 250, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "봐요", "start_ms": 350, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "It shows we have a lot to talk about.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "It", "start_ms": 35, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "shows", "start_ms": 85, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "we", "start_ms": 185, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "have", "start_ms": 235, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 295, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "lot", "start_ms": 335, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 405, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "talk", "start_ms": 455, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "about.", "start_ms": 555, "dur_ms": 121, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "맞아요 할 말이 끊이질 않네요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "맞아요", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 165, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "말이", "start_ms": 225, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "끊이질", "start_ms": 315, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "않네요", "start_ms": 445, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "이제 정말 끝낼게요 잘 자요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 130, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "끝낼게요", "start_ms": 230, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 370, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "자요", "start_ms": 440, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "Sleep tight, my friend.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "Sleep", "start_ms": 35, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "tight,", "start_ms": 135, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "my", "start_ms": 235, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "friend.", "start_ms": 295, "dur_ms": 121, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "네 안녕히 주무세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 50, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 110, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "주무세요", "start_ms": 230, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "안녕!",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "안녕!", "start_ms": 45, "dur_ms": 121, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "끝!",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "끝!", "start_ms": 100, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 07:49:51*
