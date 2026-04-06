# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=08:34:02  uptime=26087s  mem=1849MB  system_processes=445"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_083352.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 316-306 137-721-271-237-213-301-102-137-136-103-136-134-701-167-103-103-163-670-670-674-671-607-607-607-601-625-136-761-675-765 312-325-143-134-312-312-327 132 627-612 752-765-1_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 102-106-160-357-501-653-360-326-123-167-136 160-160-160-613-163-613-160-163-163-613_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 671-631-106-102-351-136-165-670-621-125-602-162-106-167-361-106-167-102-165-106-621-162-126-103-310-712-107-173-165-162-162-103-715-615-162-167-103-106-136-103-123-163-576-762-1_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 231-312-163-165-601-675-160-623-613-136-715-107-316-103-165-567-176-107-617-561-625-126-123 126-106-564-120-625-162-162-137-135-162-167-627-162-107-301-106-130-671-106-135-165-1_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 165-167-617-561-576-576-351-571-516-165-310-132-172-127-172-167-176-315-315-321-306-265-564-564-127-170-615-167-671-761-671-617-671-376-637-356-126-123-126-213-132-123-213-136-2_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 561-657-675-657-165-162-160-150-165-165-615-617-165-162-765-657-657-657-657-657-651-167-312-123-136-106-106-105-167-156-107-126-156-165-675-613-162-126-365-615-635-657-637-567-3_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 312-675-567-576-564-574-576-312-132-362-263-265-637-621-627-263-625 315-325-132-256-361-367-127-623-630-132-301-312-310-627-627-216-326-621-162-672-167-163-216-162-612-132-312-1_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 601-316-361-316-136-160 576-576-507-576-175_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 103-163-673-267-176-267-361-167-103-213-260-612-326-613-361-130-132-163-136-152-163-132-126-103-103-132-132-136-263-137-165-163-103-132-106-136-316-103-165-165-136-167-103-126-1_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 301-120-302-361-301-326-325-317-103-236-576-756-754-754-607-167-235-215-123-216-316-620-623-326-132-362-136-316-612-136-216-620-602-320-236-672-260-326-206-106-103-613-612-160-1_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 631-316-321-673-167-613-163-176-132-167-627-167-671-136-143-132-617-621-126-132-312-216-605-652-627-264-236-132-135-316-312-413-713-132-461-620-652-615-267-132-123-132-123-132-3_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 753-372-765-762-761-765-761 631-136-612-213-326-236-362-670-672-671-673-613-612-632-316-160-162-126-362-230 765-673-627-126-612 267-726-627-762-673-765-761-763-675-376-267-672-7_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 574-754-754-324-136-216-216-625-317-176-136-163-123 623-637-631-316-312-367-367-602-263-621-126-123-132-126-126-263-621-261-372-765-756-735-756-705-173-123_V.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 765-765-756-713 756 621-761-763-670-612-621-613-103-136-163-163-657-617-706-123-621-162-321-206-120-126-126-162-126-316-624-163-361-120-132-316-102-261-261-162-160-130-162-607-6_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 625-123-102-312-217-235-201-265-201-123-612-203-605-267-275-317-135-103-130-536-672-376-752-312-136-357-102-203-206-103-103-103-103-136-321-203-602-326-162-352-325-357-605-670-3_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 602-206-236-320-623-263-126-260-260-201-163-672-672-260-206-261-263-263-236-263-210-561-316-657-567-657-637-263-623-623-265-263-627-671-163-136-316-751-576-357-316-317-136-103-1_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 627-326-162-165-136-153-632-213-216-176-163-125-156-652 162-516-316-130-172-136-103-106-163-765-167-671-163-163-362-301-632-162-156-136-160-316-132-170-607-675-651-123-231-756-3_V.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 137-132-217-231-327-320-321-321-231-236-302-301_V.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 671-756-765-762-675-762-162-276-312-621-623-615_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 327-642-163-275-126-615-165-136-326-263-132-257-326-125-125-321-267-163-362-275-263-231-265-261-364-214-275-267-154-316-312-631-625-237-326-621-632-612-364-136-154-325-645-135-2_V.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 750-631-261-576-657-657-326-265-675-603-621-236-326-762-621-162-103-103-153-157-167-163-167-612-162-167-167-163-216-627-675-672-670-103-132-104-106-103-706-201-123-102-103-176-1_V.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 672-263-617-271-132-576-576-576-576-507-570-576-576-135-157-351-123-507-572-157-157-157-517-157-152-261-261 160-567-602-167-160-631-761-120-106-206-671-267-673-706-130-615-260-1_W.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 675-627-627-265-576-561-631-527-357-573-513-574-547-576-357-310-527-253-321-316-316-136-153-312-125-126-120-267-261-526-265-276-263-267-627-627-267-265-265-562-572-572-315-135-2_W.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 267-230 126 263-362-321-126-261-162-261-213-621 263-312-156-612-516-627-627-671-761-621-312-362-102-206-267-621-126-602-160-160-321-123-106-175-162 103-162-160-163-136-103-136-1_W.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 123-132-123-263-216-216-623-236-236-236-231-321-326-321-213-213-321-312-213-231-236-231-213-231-231-321-321-321-231-236-231-231-263-236-321-236-231-236-231-231-231-321-231-213-3_W.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 103-103-103-103-103-106-612-162-106-216-621-312-320-301-302-312-321-301-321-301-312-312-134-132-132-312-103-103-152-132-315-132-231-213-156-751-576-135-132-652-624-162-132-203-2_V.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 321-302-301-306-230-675-657-632-631-621-263-103-102-613 306-632-360-326-623-136-163-136-316-136-306-316-361-602-106-310-765-765-320-106-160-106-106-167-312-623-326-325-263-136-3_V.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 576-765-163-671-671-617-632-612-273-367-136-162-126-162-623-612-671-267-621-623-637-176-167-621-623-326-657-657-675-657-675-657-657-567-657 126-632-326-132-156-136-621-326-136-2_W.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 627-672-163-126-623-126-612-567-267 765-617-671-216-231-231-132 571-576-651-137-167-103-765-327-621-213-610-167-106-103-136-103-312-176-162-170-317-617-627-275-761-726-612-162-1_W.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 137-172-106-607-106-617-106-163-213-607-602-627-602-627-267-216-260-620-260-260-106-103-103-103-130-136-310-310-130 162-136-103-132-130-103-675-256-302-263-260-620-602-602-310-1_V.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 627-623-632-203-263-302-625-653-653_W.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 162-170-107-173-617-165-236-613-103-516-167-506-160-671-125-106-103-163-106-132-136-173-710 162-126-216-213-617-612-136-132-610-136-132-673-163-673-716-160-136-137-176-621-123-1_V.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 132-231-236-263-574-756-270-207-206-750 574-574-567-756-765-312-136-123-206-203-263-143-451-321-413-547-540-257-570-756-756-765-675-263-620-540-574-756-576-756-153-320-203-205-2_V.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 531-351-352-137-317-371-173-371-673-312-153-316 321-231-321-523-532-523-325-321-213-572-527-351-132-150-132-126-216-126-132-130-231-327-372-371-327-321-327-231-231-231-231-321-1_W.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 361-136-103-167-132-106-621-176-371-716-103-130-132-105-163-213-132-317-103-675-103-263-132-123-160-136-213-320-132-301-203-561-230-375-325-370-523-562-176-160-162-136-106-163-3_V.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 135-103-312-321-312-132-756-136-126-716-354-132-617-312-237-327-312-130-370-320-206-321-362-321-103-621-302-260-630-321-236-203-302-301-301-302-675-607-263-320-350-753-261-236-1_V.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 617-213-236-630-276-621-601-613-306-236-302-123-260-260-326-320-260-620-620-260-326-263-216-126-123-102-106-103-123-132-162-125-176-621-126-672-627-675-576-620-627-567-576-267-2_W.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 765-657-623-765-657-673-765-675-657-657-576-675-675-675-675-765-756-675-675-765-756-675-657-653-765-675-765-756-765-765-576-765-127-623-761-175-671-761-576-675-675-576-765-765-7_W.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 320-326-320-326-327-326-165-670-621-236-126-102-120-102-123-123-176-167-126-163-372-756-702-270-750-207-312-316-673-627-260-206-203-236-725-612-612-132-162-132-130-735-750-627-6_V.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 130-103-137-756-756-173-137-631-321-301-130-516-106-163-172-261-361-632-607-602-620-620-260-263-162-132-126-136-605-672-615-106-163-361-316-165-167-176-167-167-671-106-165-765-6_V.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 362-362-632-670-732-307-762-321-107-107-612-132-310-372-203-321-236-103-132-106-167-612-261-126 137-756-765-621-106 754-726-312-132-571-531-135-132-132-316-103-102-671-756-567-6_V.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 574-156 316-123-126-671-103-103-103-637-670-103-103-756-576-576-576-750-751-750-765-756-765-754-756-756-756-765 671-675 761-765-617-160-765-170-130-756-761-762-617-637-756-625-5_W.mp3)
  Segment 43: 66192ms ~ 66768ms (file: 127-675-361-612-103-105-105-103-136-756-231-672-136-610-765-167-136-516-175-657-152-106-167-157-261-172-126-136-130-103-752-263_V.mp3)
  Segment 44: 67768ms ~ 68344ms (file: 327-361-623-276-276-261-612-326-673-675-756-627-326-367-321-637-627-167-627-256-675-625-562-675-527-657-657-517-312-126-675-316-613-567-526-576-756-675-162-132-362-157-265-762-6_W.mp3)
  Segment 45: 69344ms ~ 69920ms (file: 602-621-620-132-754-132-312-132-413-132-763-237-327-326-132-312-630-756-627-263-132-617-163-167-267-132-621-316-632-261-765-167-621-130-103-132-170-167-716-312-103-103-751-735-1_W.mp3)
  Segment 46: 70920ms ~ 71496ms (file: 123 137-162-123-312-625-726-672-627-126-103-302-320-231-312-231-201-321-302-302-574-735-302-765-765-276-301-132-632-236-301-675-602-602-623-267-163-236-321-263-762-162-126-162-1_V.mp3)
  Segment 47: 72496ms ~ 73072ms (file: 765-765-765-756-765-765-716-763-763-673-765-673-762-761 726-132-163-136-760-371-613-715-756-710-106-621-316-103-136-107-136-103-263-106-132-136-176-160-103-706-216-137-760-163-1_W.mp3)
  Segment 48: 74072ms ~ 74648ms (file: 760-756-756-761-761-765-763-756-763 567-765-756-572-726-276-763-765-763-760-756-756-756-765-765-137-765-763-762-751-753-175-571-756-765-753-765-762-761_W.mp3)
  Segment 49: 75648ms ~ 76224ms (file: 172-172-173-127-162-137-172-567-157-175-157-157-571-157-571-103-162-362-207-367-362-632-316-261-326-326-321-123-132-136-163-765-561-567-516-576-537-573-537-152-301-136-167-162-1_W.mp3)
  Segment 50: 77224ms ~ 77800ms (file: 672-267-267-627-620 627-267-627-627-617-612-610 507-576-276-267-267_W.mp3)
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
> **Time**: 2026-04-03 08:35:08
> **AI**: gemini

---

## Response

AnalysisAnalysisGemini의 응답[
{
"segment": 1,
"transcript": "이 노래 제목이 뭐예요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 60, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "노래", "start_ms": 130, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "제목이", "start_ms": 260, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "뭐예요?", "start_ms": 370, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 3,
"transcript": "최근에 유행하는 곡이에요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "최근에", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "유행하는", "start_ms": 210, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "곡이에요", "start_ms": 360, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "멜로디가 정말 중독성 있네요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "멜로디가", "start_ms": 45, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 230, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "중독성", "start_ms": 340, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "있네요", "start_ms": 470, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "가사도 한번 찾아보세요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "가사도", "start_ms": 55, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "한번", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "찾아보세요", "start_ms": 290, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "(mixed)",
"speaker": "speaker_2",
"confidence": 0.85,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "mixed",
"words": [
{"word": "가사가", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "참", "start_ms": 170, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "서정적이에요", "start_ms": 240, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "어떤 가수가 불렀나요?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "어떤", "start_ms": 35, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "가수가", "start_ms": 140, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "불렀나요?", "start_ms": 270, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 9,
"transcript": "신인 그룹인데 목소리가 좋아요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "신인", "start_ms": 60, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "그룹인데", "start_ms": 170, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "목소리가", "start_ms": 320, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "좋아요", "start_ms": 450, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "나중에 콘서트도 가보고 싶네요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "나중에", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "콘서트도", "start_ms": 170, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가보고", "start_ms": 330, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "싶네요", "start_ms": 440, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "라이브 실력이 대단하다고 해요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "라이브", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "실력이", "start_ms": 180, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "대단하다고", "start_ms": 300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "해요", "start_ms": 460, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "Would you like to listen to it together?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "Would", "start_ms": 70, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 160, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "like", "start_ms": 210, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 280, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "listen", "start_ms": 330, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 440, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "it", "start_ms": 490, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "together?", "start_ms": 540, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "Sure, that sounds great.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Sure,", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "that", "start_ms": 150, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "sounds", "start_ms": 240, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "great.", "start_ms": 350, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 15,
"transcript": "기분이 울적할 때 들으면 좋겠어요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "기분이", "start_ms": 45, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "울적할", "start_ms": 170, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 280, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "들으면", "start_ms": 350, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "좋겠어요", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "위로가 되는 노래예요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "위로가", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "되는", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "노래예요", "start_ms": 250, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "다른 추천곡도 있나요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "다른", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "추천곡도", "start_ms": 150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있나요?", "start_ms": 310, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "이 가수의 데뷔곡도 들어보세요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 50, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "가수의", "start_ms": 100, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "데뷔곡도", "start_ms": 210, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "들어보세요", "start_ms": 360, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 20,
"transcript": "제목이 어떻게 되나요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "제목이", "start_ms": 45, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "되나요?", "start_ms": 290, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "첫눈이라는 곡이에요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "첫눈이라는", "start_ms": 30, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "곡이에요", "start_ms": 240, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "(mixed)",
"speaker": "speaker_2",
"confidence": 0.88,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "mixed",
"words": [
{"word": "겨울에", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "듣기", "start_ms": 170, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "딱", "start_ms": 260, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "좋아요", "start_ms": 330, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "당장 리스트에 추가해야겠어요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "당장", "start_ms": 55, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "리스트에", "start_ms": 160, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "추가해야겠어요", "start_ms": 310, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 25,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 26,
"transcript": "요즘 유행하는 패션이 뭐예요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "요즘", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "유행하는", "start_ms": 150, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "패션이", "start_ms": 300, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "뭐예요?", "start_ms": 430, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "레트로 스타일이 다시 인기예요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "레트로", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "스타일이", "start_ms": 180, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 330, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "인기예요", "start_ms": 440, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "아 구제 옷 같은 느낌인가요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "아", "start_ms": 45, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "구제", "start_ms": 100, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "옷", "start_ms": 190, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "같은", "start_ms": 260, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "느낌인가요?", "start_ms": 350, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "네 통 넓은 바지가 유행이죠",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 35, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "통", "start_ms": 110, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "넓은", "start_ms": 180, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "바지가", "start_ms": 270, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "유행이죠", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 31,
"transcript": "저도 하나 사야겠어요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "저도", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "하나", "start_ms": 150, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "사야겠어요", "start_ms": 260, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 33,
"transcript": "동대문 시장에 가면 많아요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "동대문", "start_ms": 60, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "시장에", "start_ms": 210, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "가면", "start_ms": 340, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "많아요", "start_ms": 450, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "언제 한번 같이 가실래요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "언제", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "한번", "start_ms": 150, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 260, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "가실래요?", "start_ms": 350, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "좋아요 주말에 시간 어떠세요?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "좋아요", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "주말에", "start_ms": 160, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "시간", "start_ms": 290, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "어떠세요?", "start_ms": 380, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "토요일 오후 두 시쯤 어때요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "토요일", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "오후", "start_ms": 170, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "두", "start_ms": 280, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "시쯤", "start_ms": 350, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "어때요?", "start_ms": 440, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "네 그때 뵙겠습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 30, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "그때", "start_ms": 100, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "뵙겠습니다", "start_ms": 210, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 39,
"transcript": "I'm looking forward to it.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "I'm", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "looking", "start_ms": 140, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "forward", "start_ms": 270, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 380, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "it.", "start_ms": 430, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "Me too, see you then.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Me", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "too,", "start_ms": 110, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "see", "start_ms": 200, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 290, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "then.", "start_ms": 360, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "그 영화 예고편 보셨어요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "그", "start_ms": 45, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "영화", "start_ms": 100, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "예고편", "start_ms": 230, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "보셨어요?", "start_ms": 380, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 43,
"transcript": "네 아주 스릴 넘치더라고요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 55, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 120, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "스릴", "start_ms": 230, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "넘치더라고요", "start_ms": 360, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "개봉하면 같이 보러 갈까요?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "개봉하면", "start_ms": 35, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 220, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "보러", "start_ms": 330, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "갈까요?", "start_ms": 420, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 46,
"transcript": "좋아요 벌써부터 기대되네요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "좋아요", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "벌써부터", "start_ms": 170, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "기대되네요", "start_ms": 360, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 48,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 49,
"transcript": "팝콘도 큰 걸로 먹어요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "팝콘도", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 200, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "걸로", "start_ms": 270, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "먹어요", "start_ms": 360, "dur_ms": 150, "phase_lr_deg": 0.0}
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
*Generated by WKAppBot ask gemini — 2026-04-03 08:35:08*
