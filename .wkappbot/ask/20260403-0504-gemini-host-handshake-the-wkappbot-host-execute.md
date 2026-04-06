# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=05:03:38  uptime=13463s  mem=1620MB  system_processes=447"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_050328.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 132-130-106-102-756-562-152 103-675-167-326-632-163-106-106-126-132-617-263-672-126-103-130-126-126-765-361-163-163-163-316-103-310-610-630-103-130-103-301-103-316-617-751-601-6_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 672-760-703-720-623 750-760-762-760 103 261 163-126-136-162-605-671-167-750-753 130-103-103-132-567-574-576-576-576-756-136-213-627-263-673 756-765-726-570-574-573-754-203-162-1_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 612-705-760-673-623-672-613-361-301-601-367-316-361-236-310-302-302-263-302-574-570-572 341-574-753-310-301-301-123-132-136-132-143-321-231-302-123-756-765-765-736-132-312-312-3_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 361-132-132-160-716-167-136-627-317-613-136-132_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 260-236-326-103-312-123-301-320-302-203-236-261-261-260-260-620-760-672-120-130-106-613 657-675-672-623-632-260-627-623-260-620-603-236-756-132-150-574-570-574-754-574-765-657-6_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 627-673-637-765-756-567-765-761-761-675-175-137-567-756-756-576-570-312 321-312-316-672-672-673_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 162-321-132-321-236-261-213-231-231-213-213-231-621-623-261-754-657-631-162-163-160-136-652-625-625 263-621-651-756-657-675-672-621-621-623-621-625-261-265-261-263-236-631-267-2_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 675-625-625-625-615-564-563-356-256-356-536-356-537-574-574-536-531-532-561-527-536-527-257-257-275-752-257-257-175-532-157-157-157-517-715-713-715-715-715-175-175-175-531-516-5_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 267-263-162-163-103-136-621-103-136-673-123-153-136-312-302-103-162-106-376-162-621-612-163-165-157-157-152-670-160-163-163-137-132-154-132-103-132-103-132-103-175-160-716-175-1_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 137-106-231-102-631-126-671-312-316-361-106-316-103-103-103-135-102-107-165-231-163-136-136-103-106-136-162-312-163-103-165-136-103-613-136-167-137-157-162-106-136-106-136-107-5_V.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 301-360-132-612-621-360-230-102-605-320-326-736-706-302-132-103-312-163-102-132-136-167-162-261-260-620-260-105-602-130-316-632-612-602-621-321-312-321-263-361-361-312-163-132-1_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 765-756-756-751-173-617-670-267-607-201-163-102-120-137-623-635-261-231-260-163-136-103-136-306-160-617-301-132-753-163-130-160-267 126_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 132 312-316-751-326-361-132-123 627-620-362-176 675 613-631-162-136-316-136-673-631-163-103-612-621-261-216-260-231-312-103-123-103-103-132-136-302-672-670-301-231-312-236-620-6_V.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 127-123-321-312-123-132-261-213-260-260-261-213-210-260-326-132-126-312-230-130_V.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 261-672-526-265-256-256-265-623-613-651-563-562-526-532-532-251-163-126-167-216-623-621-326-162-136-136-162-132-617-215-514-541-534-153-672-726-267-672-726-627-273-361-623-524-5_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 213-756-756-760-672-675-561-361-675-607-623-216-126-756-206-230-136-316-163-136-103-132-316-312-326-153-206-261-206-621-162-172-126-102-162-102-120-152-526-625-562-752-756-567-5_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 672-726-263-267-675-675-675-576-756-762-675-652-627-627 762-675-621-612-612-126-621-612-162-163-106-613-316-610-163-637-317-132-316-613-607-103-632-270-203-310-132-103-136-612-3_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 207-270-230-201-547-574-574-574-754 765-675-627-762-621-620-670-625-625-672-267-672-672-672-637-672-675-675-657-625-625-675-671-132-312-167-132-165-275-267-276-261-162-263-326-3_W.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 572-572-657-627-572-576-762-675-675 153-513-312-136-613-574-567-657-625-627-627-103-130-106-103-162-512-250-210-123-210-106-103-103-102-102-103-103-103-103-162-130-310-130-623-6_V.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 167-371-657-163-261-167-612-623-627-261-263 132-751-762-162-165-163-136-103-271-216-617-165-617-163-103-572-172-137-316-160-103-312-136-132-351-541-765-351-267-167-160-103-106-1_V.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 627-546-567-316-574-576-567-312-162-267-507-657-506-675-657-506-672-671_W.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 164-176-645-631-274-321-623-321-326-237-267-371-271-741-327-267-312-264-236-347-314-267-675-213-276-265-316-167-261-627-147-612-235-273-213-265-321-327-251-254-215-312-261-324-1_V.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 670-612-620-106-167-316-106-162-162-763-103-136-163-615-672-176-167-157-615-130-615-126-170-765-127-720-102-562-526-532-506-156-106-106-132-310-163-165-136-167-126-260-167-615-1_V.mp3)
  Segment 24: 36248ms ~ 46544ms (file: 167-761-607-175-103-106-103-123-176-167-316-106-103-176-126-136-576-576-126-574-217-107-762-672-762-675-765-675-675-765-675-675-765-675_W.mp3)
  Segment 25: 47544ms ~ 48120ms (file: 316-631-317-635-265-167-576-576-567-567-765-120-136-326-231-526-562-567-576-265-562-326-526-267-623-632 236-231-130-103-315-156-576-675 576-567-263-563-675-567-563-561-621-625-6_W.mp3)
  Segment 26: 49120ms ~ 49696ms (file: 316-321-132-321-103-136-103-135-103-103-106-163-316-602-167-532-506-532-123-163-127-103-136-162-136-165-162-163-132-136-215-162-165-130-106-103-761-236-120-102-163-163-175-165-1_V.mp3)
  Segment 27: 50696ms ~ 51272ms (file: 367-312-615-123-257-627-523-562-532-523-132-152-127-537-273-127-352-572-352-356-567-567-576-576-675-657-356-635-627-627-627-263-263-263-263-267-267-265-126-236-321-213-321-351-3_W.mp3)
  Segment 28: 52272ms ~ 52848ms (file: 136-261-236-263-132-261-631-630-136-136-312-126 671-671-761 176-176-162-126 617-612-613-263 162-613-136-167-125-651-507-156-561-517-673-126-623-617-612-167-621-651-162-671-176-6_W.mp3)
  Segment 29: 53848ms ~ 54424ms (file: 163-167-106-137-216-236-302-301-132-106-130-361-675-613-361-136-105-130-312-123-132-103-103-312-206-213-123-172-126-123-312-132-136-570-562-132 532-102-326-302-675-617-126-320-2_W.mp3)
  Segment 30: 55424ms ~ 56000ms (file: 106-310-362-302-176-163-123-160-216-103-310-136-136-316-136-321-321-261-216-263-236-312-657-260-162-103-136-132-103-106-103-631-316-321-216-236 672-672 675-672-675-672-675-627-6_W.mp3)
  Segment 31: 57000ms ~ 57576ms (file: 162-106-106-106-160-674 137-312-103-103-167-267_V.mp3)
  Segment 32: 58576ms ~ 59152ms (file: 136-106-167-512-176-617-316-316-167-135-162-106-165-132-630-106-625-526-630-176-162-160-120-163-376-103-162-765-167-671-103-163-163-132-130-126-130-162-126-106-627-126-263-137-1_V.mp3)
  Segment 33: 60152ms ~ 60728ms (file: 213-216-123-162-261-326-256-567-265-231-132-271-652-621-267-627-627-267-263-612-213_W.mp3)
  Segment 34: 61728ms ~ 62304ms (file: 257-236-123-574-123-231-312-527-576-236_W.mp3)
  Segment 35: 63304ms ~ 63880ms (file: 301-230-301-310-316-132-123-103-301-316-123-310-176-501-520-134-621-623-162-162 156-106-617-617-126-312-615-613-163-157-756-675-517-670-107-160-106-612-103-316-631-621-106-123-1_V.mp3)
  Segment 36: 64880ms ~ 65456ms (file: 321-231-236-326-325-562-516-356-563-576-157-136-136-312-352-627-672-627-637-326-312-123-132-217-627-256-256-257-215-327-276-216-321-326-231-132-132-256-321-326-321-513-135-315-2_W.mp3)
  Segment 37: 66456ms ~ 67032ms (file: 236-231-526-526-256-236-301-357-356-320-231-316-103-132-103-130-310-267-623-672-306-613-106-106-132-163-126-562-567-132-123-132-126-573-570-576-567-250-256-205-357-301-310-567-7_W.mp3)
  Segment 38: 68032ms ~ 68608ms (file: 612-631-132-750-105-650-567-612-165-123-162-163-612-130-573-136-103-136-123-165-163-136-132-103-163-167-102-671-167-167-106-173-152-175-362-617 167-261-621-623-617-617-103-517-7_W.mp3)
  Segment 39: 69608ms ~ 70184ms (file: 657-657-756-756-564-750-175-705-107-610-673-607-765-756-765-751-765-765-670-607-765-571-765-756-762-765-267-756-756-763-735-625-367-375-672-312-301-136-103-612-162-236 613-617-1_W.mp3)
  Segment 40: 71184ms ~ 71760ms (file: 123-213-162-103-163-103-213-132-136-321-316-163-612-162-167-176-261-236-361-136-123-350-316-132-316-367-561-165-615-675-620-620-567-650-615-352-315-352-507-570-574-576-567-576-3_V.mp3)
  Segment 41: 72760ms ~ 73336ms (file: 167-675-671-670-267-574-574-576-576-675-672-576-576-567-657-574-756-576-756-765-576-576-632-362-756-756-571-576-765-765-126-106-163-621-631-126-120-617-136-130-130-123-231-132-3_W.mp3)
  Segment 42: 74336ms ~ 74912ms (file: 106-601-167-617-576-576-507-670-765-702-756-657-765-657-675-617-623-627-762-762-762-762-167-761-712-762-123-102-123-163-156-362-316-162-125-132-670-167-167-152-156-175-670-136-6_W.mp3)
  Segment 43: 75912ms ~ 76488ms (file: 362-361-312-316-615-657-563-765-671-167-607-517-625-612-601-175-136-127-137-562-253-376-103-317-160-316-106-136-312-321-132-317-132-134-103-137-103-126-132-162-103-156-321-213-2_V.mp3)
  Segment 44: 77488ms ~ 78064ms (file: 625-623-126-361-102-310-530-570-620-621-261-132-106-163-632-130-620-602-605-607-602-632-263-126-175-236-263-167-167-130-136-547-572-260-260-260-206-260-260 260-206-752-725-761-7_W.mp3)
  Segment 45: 79064ms ~ 79640ms (file: 671-136-156-106-615-165-176-167-621-602-631-106-612-167-106-126-103-623-316-136-162-615-162-615-756-175-103-326-163-167-103-567-263-612-136-263-621-613-103-106-103-132-103-103-1_V.mp3)
  Segment 46: 80640ms ~ 81216ms (file: 312-752-627-237-326-301-312-312-570-756-756-735-371-763-367-376-763-574-574 321-231-217-231-270-527-572-571-541-541-541-542-256-251-216-267-267-621-621-627-627-632-326-376-276-2_W.mp3)
  Segment 47: 82216ms ~ 82792ms (file: 132-675-237-236-267-327-326-675-362-132-326-670-306-136-103-132-106-136-167-162-620-260-162-102-312-326-617-621-613-607-103-162-623-267-263-163-716-631-316-310-625-321-763-765-6_W.mp3)
  Segment 48: 83792ms ~ 84368ms (file: 126-517-756-321-103-103-103-136-136-106-130-615-136-103-165-160-513-312-136-637-216-310-173-310-102-672-627-632-263-263-103-134-567-152-176-137-362-612-603-632-173-167-106-160-6_W.mp3)
  Segment 49: 85368ms ~ 85944ms (file: 570-153-157-231-123-123-321-301 261-213-132-103-213-261-326-163-102-163-103-301-306-730-126-236-267-627 574-567-561-267-612-507-567-567-567-576-572-520-265-312-231-326-236-312-2_W.mp3)
  Segment 50: 86944ms ~ 89248ms (file: 176-726-716-176-671-762-726-673-726-276-276-120-312-107-307-173-762-735-765-123-763-761-765-765-765-672-672-761-764-716-671-617-362-326-760-710-716-756-765-765-763-312-321-613-6_W_stt_the_didn't.mp3)
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
> **Time**: 2026-04-03 05:04:59
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "오늘의 날씨를 알려드릴게요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "오늘의", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "날씨를", "start_ms": 200, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "알려드릴게요", "start_ms": 390, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "현재 서울은 아주 맑습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "현재", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "서울은", "start_ms": 165, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 325, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "맑습니다", "start_ms": 435, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "기온은 영상 십팔도입니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "기온은", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "영상", "start_ms": 210, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "십팔도입니다", "start_ms": 340, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "미세먼지 농도는 낮아요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "미세먼지", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "농도는", "start_ms": 250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "낮아요", "start_ms": 410, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "산책하기 참 좋은 날씨네요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "산책하기", "start_ms": 60, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "참", "start_ms": 270, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 360, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "날씨네요", "start_ms": 470, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "The weather is perfect for a walk.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "The", "start_ms": 45, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "weather", "start_ms": 135, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 295, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "perfect", "start_ms": 385, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "for", "start_ms": 575, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 665, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "walk.", "start_ms": 725, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "하지만 오후에는 비 소식이 있어요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "하지만", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "오후에는", "start_ms": 210, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "비", "start_ms": 400, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "소식이", "start_ms": 490, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 650, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 9,
"transcript": "외출하실 때 우산을 챙기세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "외출하실", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 230, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "우산을", "start_ms": 320, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "챙기세요", "start_ms": 480, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "Don't forget to take an umbrella.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "Don't", "start_ms": 55, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "forget", "start_ms": 185, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 345, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "take", "start_ms": 435, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "an", "start_ms": 565, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "umbrella.", "start_ms": 655, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "강수 확률은 육십 퍼센트입니다",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "강수", "start_ms": 60, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "확률은", "start_ms": 190, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "육십", "start_ms": 350, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "퍼센트입니다", "start_ms": 460, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "The chance of rain is sixty percent.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "The", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "chance", "start_ms": 140, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "of", "start_ms": 300, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "rain", "start_ms": 390, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 520, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "sixty", "start_ms": 610, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "percent.", "start_ms": 760, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "저녁부터는 기온이 크게 떨어질 거예요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "저녁부터는", "start_ms": 70, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "기온이", "start_ms": 300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "크게", "start_ms": 460, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "떨어질", "start_ms": 570, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 720, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "감기 조심하시기 바랍니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "감기", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "조심하시기", "start_ms": 170, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "바랍니다", "start_ms": 430, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "Be careful not to catch a cold.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Be", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "careful", "start_ms": 140, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "not", "start_ms": 330, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 440, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "catch", "start_ms": 530, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 660, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "cold.", "start_ms": 720, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "내일은 하루 종일 흐릴 예정입니다",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "내일은", "start_ms": 60, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "하루", "start_ms": 220, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "종일", "start_ms": 330, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "흐릴", "start_ms": 440, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "예정입니다", "start_ms": 570, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "It's going to be cloudy all day tomorrow.",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "It's", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "going", "start_ms": 160, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 290, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "be", "start_ms": 380, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "cloudy", "start_ms": 470, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "all", "start_ms": 660, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "day", "start_ms": 750, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "tomorrow.", "start_ms": 860, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "이상으로 날씨 정보를 마칩니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "이상으로", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "날씨", "start_ms": 250, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "정보를", "start_ms": 380, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "마칩니다", "start_ms": 540, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "행복한 하루 보내세요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "행복한", "start_ms": 35, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "하루", "start_ms": 195, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "보내세요", "start_ms": 325, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "Have a wonderful day.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "Have", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 155, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "wonderful", "start_ms": 215, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "day.", "start_ms": 445, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 22,
"transcript": "다음은 교통 정보입니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "다음은", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "교통", "start_ms": 170, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "정보입니다", "start_ms": 300, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "출근길 정체가 심해지고 있어요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "출근길", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "정체가", "start_ms": 210, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "심해지고", "start_ms": 360, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 550, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "특히 강변북로는 매우 혼잡합니다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "특히", "start_ms": 60, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "강변북로는", "start_ms": 170, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "매우", "start_ms": 430, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "혼잡합니다", "start_ms": 540, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "가급적 대중교통을 이용해 주세요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "가급적", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "대중교통을", "start_ms": 210, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "이용해", "start_ms": 440, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 600, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "Please use public transportation if possible.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "Please", "start_ms": 45, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "use", "start_ms": 175, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "public", "start_ms": 285, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "transportation", "start_ms": 435, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "if", "start_ms": 745, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "possible.", "start_ms": 835, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "안전 운전하시기 바랍니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "안전", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "운전하시기", "start_ms": 165, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "바랍니다", "start_ms": 395, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "Drive safely.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Drive", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "safely.", "start_ms": 200, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "감사합니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 30, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "Thank you.",
"speaker": "speaker_2",
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
"segment": 31,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 32,
"transcript": "이제 음악을 감상해 보세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "음악을", "start_ms": 180, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "감상해", "start_ms": 340, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "보세요", "start_ms": 530, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "Now, please enjoy the music.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "Now,", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "please", "start_ms": 155, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "enjoy", "start_ms": 285, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 445, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "music.", "start_ms": 535, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 35,
"transcript": "어떠셨나요? 정말 아름다운 선율이죠.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "어떠셨나요?", "start_ms": 60, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 350, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "아름다운", "start_ms": 480, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "선율이죠.", "start_ms": 690, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "How was it? It's a truly beautiful melody.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "How", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "was", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "it?", "start_ms": 250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "It's", "start_ms": 380, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 470, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "truly", "start_ms": 530, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "beautiful", "start_ms": 660, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "melody.", "start_ms": 850, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "이 곡은 위로가 필요한 분들을 위해 만들어졌어요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 70, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "곡은", "start_ms": 130, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "위로가", "start_ms": 240, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "필요한", "start_ms": 400, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "분들을", "start_ms": 550, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "위해", "start_ms": 710, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "만들어졌어요", "start_ms": 820, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "This song was made for those who need comfort.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "This", "start_ms": 60, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "song", "start_ms": 190, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "was", "start_ms": 340, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "made", "start_ms": 430, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "for", "start_ms": 560, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "those", "start_ms": 650, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "who", "start_ms": 800, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "need", "start_ms": 890, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "comfort.", "start_ms": 1020, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "여러분의 지친 마음이 조금이나마 풀렸기를 바라요",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "여러분의", "start_ms": 80, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "지친", "start_ms": 270, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "마음이", "start_ms": 380, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "조금이나마", "start_ms": 530, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "풀렸기를", "start_ms": 740, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "바라요", "start_ms": 930, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "I hope your tired heart was relieved even a little.",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 70, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "hope", "start_ms": 130, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "your", "start_ms": 240, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "tired", "start_ms": 330, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "heart", "start_ms": 460, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "was", "start_ms": 590, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "relieved", "start_ms": 680, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "even", "start_ms": 870, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 980, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "little.", "start_ms": 1040, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "오늘의 방송을 마칩니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "오늘의", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "방송을", "start_ms": 210, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "마칩니다", "start_ms": 360, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "함께해 주셔서 감사합니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "함께해", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "주셔서", "start_ms": 200, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 330, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "Thank you for being with us.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Thank", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 180, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "for", "start_ms": 270, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "being", "start_ms": 360, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "with", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "us.", "start_ms": 580, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "안녕히 계세요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 35, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "계세요", "start_ms": 225, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "Goodbye.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Goodbye.", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 47,
"transcript": "언제나 여러분의 곁에 있겠습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "언제나", "start_ms": 60, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "여러분의", "start_ms": 220, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "곁에", "start_ms": 410, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있겠습니다", "start_ms": 520, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "We will always be by your side.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "We", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "will", "start_ms": 140, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "always", "start_ms": 230, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "be", "start_ms": 390, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "by", "start_ms": 480, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "your", "start_ms": 570, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "side.", "start_ms": 660, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "고맙습니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "고맙습니다", "start_ms": 30, "dur_ms": 250, "phase_lr_deg": 0.0}
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
*Generated by WKAppBot ask gemini — 2026-04-03 05:04:59*
