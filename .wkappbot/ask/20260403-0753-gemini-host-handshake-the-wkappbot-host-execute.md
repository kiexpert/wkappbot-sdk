# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=07:52:34  uptime=23599s  mem=1708MB  system_processes=447"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_075033.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 163-103-163-103-163-153-136-160-106-620-167-123-132-123-162-102-103-617-152-176-130-160-167-672-367-175-152-136-261-217-635 573_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 216-103-765-103-156-675-165-617-106-106-216-613-102-617-103-107-176-176-103-167-132-103-103-167-167-163-617-167-167-617-167-165-130-163-607-162-671-136-167-106-612-126-162-164-1_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 267-367-271-325-467-321-132-451-167-736-132-316-127-376-275-126-614-362-326-631-376-357-326-317-612-263-261-326-327-231-362-126-231-132-321-267-326-362-241-236-275-216-235-321-3_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 670-673-312 103-123 162-132 671 176-671-136-163-762-762-731_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 316-361-327-315-351-165-263-251-231-253-125-261-152-215-356-567-567-236-123-173-321-312-317-135-271-123-172-721-271-721-217-126-256-235-265-273-265-236-236-321-321-326-321-316-3_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 132-132-317-126-361-632-136-621-602-613-764-760-756-657-635-761-623-627-623-261-126-167-621-607-612-312-120-621-621-216-123-321-361-123-263-321-103-103-130-306-361-613-132-231-2_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 102 102-106-106-106-574-547 356-560-520-507-361-631-216-216-236-675-672-567-576-765-765 627-627-765-270-672-136-103-130-603-162-612-631-162-201-203-132-261-213-216-102-526-562-6_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 301-765-760-627-623-632-103-132-316-632-310-132-106-607-162-163-102-160-612-621-301-321-236-312-130-361-236-326-312-103-132-136-507-571-675-610-621-627-672-623-623-136-167-162-2_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 261-102-163-317-136-163-567-136-361-163-162-360-162-671-165-105-103-103-167-176-165-163-137-106-367-651-167-106-136-162-163-102 627-627-312-132-657-172 627-312-263-631-631-627-6_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 716-306-310-612-716-712-716 267 752-276-267-261-267-276-276-267-261-627 106-651-675-670-657-167-657 617-672-317-173-713-671-753-567-567-576-672-627-627-627_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 162-136-312-312-213-263-267-163-312-613-316-361-317-130-310-130-312-132-761-326-312-263-631-217-671-761-612-612-320-123-102-130-210-123-103-103 627-136-126-267-263-236-321-623-2_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 762-576-576-657-675-657-576-657-675-657-576-675-675-765-657-756-675-657-657-653-675-567-657-625-263-627-261-623-625-652-675-365-623-632-652-236-627-265-267-265-276-652-675-675-7_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 267-627 627-267-162-162 172-136-637-167_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 361-107-175-326-102-135-612-607-167-103-135-106-136-103-165-165-361-136-167-167-162-123-132-103-316-105-613-136-103-163-106-172-612-316-132-201-102-156-163-160-103-136-716-163-1_V.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 132-321-136-301-312-130-123 210-102-102-132-316-564-564-657_V.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 203-261-256-726-132-123-123-213-217-237-231-137-231-271-706-267-207-752-567-701-715-107-230-231-261-572-573-321-123-715-752-657-132-132-136-132-126-237-765-725-326-637-230-207-2_V.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 670-673-631-215-153-627-627-672-670-316-312-316-361-317-312-301-130-163-613-316-310-135-132-312-132-326-137-162-617-617-627-316-301-672-672-607-602-236-576-675-672-672-756 762-6_V.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 312-106-163-132-103-167-516-150-731-136-617-136-176-751-127-567-617-156-103-165-120-716-576-613-516-103-103-103-361-162-126-152-106-631-163-627-106-261-126-617-173-103-167-136-6_V.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 657-567-165-567-567-173-501-567-675-567-165-152-120-127-576-576-765-576-351-567-576-157-571-576-517-561-675-675-160-173-106-312-312-163-107-103-516-603-612-621-612-610-106-651-1_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 310-361-163-316-126-312-310-301-310-130-123-312-310-301-231-261-230-132-301-301-301-361-312-132-361-613-607-160-362-306-601-602-607 320-137-103-317-310-130-567-657-675-567-576-6_V.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 675-672-675-672-672-672-627-627-627-672-627-672-765-672-671-673-672-637-236-627-267-362-670-726-367-672-763-760-716-736-673-762-671-672-672-763-736-673-765-637-671-637 376-372_W.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 312-317-617-765-631-625-326-352-756-567-756-326-763-627-621-123-637-576-267-265-236-627-265-625-265-623-265-261-571-576-576-567-625-627-267-625-675-625-675-652-265-162-561-652-6_W.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 312-132-123-132-102-123-123-103-123-316-103-312_V.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 103-132-103 672-627 123-301-310-312_V.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 756-765-736-765-761-761-671-763-675-675-576-105 632-267-263-672-362-236-260-230-261-762-273-321-260-326-206-602-621-756-756-723-320-723-731-637-627-726-271-231-236-260-203-206-2_W.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 572-175-157-175-157-517-567-157-517-157-517-150-157-501-567-571-572-572-625-236-152-135-123-153-536-563-365-362-361-263-570-576-627-562-576-576-256-265-263-265-267-267-265-265-2_W.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 574-576-576-765-576-576-567-675-576-576-567-576-576-567-657-567-576-567-576-657-675-657-651-627-627-672-671-657-675-657-576-517_W.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 756-756-762-276-106-167-106-610-576-532-263-620-326-302-132-326-352-352-352-106-621-263-623-326-321-750-765-376-170-613-612-106-106-136-130-260-263-206-617-607-106-617-167-106-6_V.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 320-320-320-132-102-321-362-201-261-162-260-103-130-102-130-310-136-213-621-301-621-613-631-130-312-301-263-231-312-123-130-310-312-302-136-316-321-302-130-230-201-306-312-132-3_V.mp3)
  Segment 30: 45704ms ~ 55784ms (file: 726-726-726-762-762-762-763 301-632-306-376-716-236-126-123-126-126-126-126_W.mp3)
  Segment 31: 56784ms ~ 57360ms (file: 657-136-167-106-106-756-162-632-671-617-126-103-136-143-135-601-137-103-103-623-163_V.mp3)
  Segment 32: 58360ms ~ 58936ms (file: 163-103-106-132-160-136-163-103-310-103-310-103-103-102-102-102-601-106-130-106-106-106-103-106-163-136-163-326-610-672-627-620-602-260-321-567-561-620-162-617-627-672-625-607-1_V.mp3)
  Segment 33: 59936ms ~ 60512ms (file: 275-301-312-103-301-103-302-301-302-213-230-231-302-360-103-137-312-132-531-573-576-752-765-756-567-167-576-574-157-167-167-167-710-176-672-217-761-170-761-173-172-137-126-312-2_W.mp3)
  Segment 34: 61512ms ~ 62088ms (file: 106-765-302-302-320-106-164-136-134-163-132 106-615-561-651-132-657-567-605-675-617-671-162-130-316-326-301-613-106-106-162-756-576-573-753-756-753-756-675-130-103-126-261-123-1_V.mp3)
  Segment 35: 63088ms ~ 63664ms (file: 672-572-657-657-675-567-167-156-536-536-510-156-652-156-567-574-576-675-167-675-576-657-536-572-576-576-516-573-561-567-150-653-103-652-567-657-765-635-650-561-765-763-617-617-6_W.mp3)
  Segment 36: 64664ms ~ 65240ms (file: 276-275-132-357-321-132-132-316-136-137-130-103-130-130-517-576-574-576-150-103-106-103-615-163-137-421-137-132-136-167-132-136 321-271-174-312-123-267-260-627-261-163-130 231-1_V.mp3)
  Segment 37: 66240ms ~ 66816ms (file: 631-312-132-312-130-310-136-631-621-132-612-312-136-231-621-263-621-261-263-621-261-620-621-620-620-621-621-620-623-620-623-620-621-621-621-261-264-261-261-263-627-620-625-621-2_W.mp3)
  Segment 38: 67816ms ~ 68392ms (file: 672-576-756-572-756-572-576-547_W.mp3)
  Segment 39: 69392ms ~ 69968ms (file: 635-623-672-162-127-160-261-621-162-263-263-236-120-263-321-126-263-267-236-236-267-106-167-160-176-715-162-206-175-160-260-263-201-612-361-123-236-103-526-265 167-136-106-751-1_V.mp3)
  Segment 40: 70968ms ~ 71544ms (file: 675-762-675-130-136 263-231-216-132-216-236-263-236-216-263-216 712-716-761-261-162-167-216-612-236-261-627-126-621-126-627-613-671-136 726-261-621-623-621-621 130-632-261-126-6_W.mp3)
  Segment 41: 72544ms ~ 73120ms (file: 574-576-567-516-573-571-576-613-670-163-106-106-126-162-102-136-671-163-162-160-162-162-162-612-621-627-673-652-617-361-760-136_W.mp3)
  Segment 42: 74120ms ~ 74696ms (file: 321-312-321-123-103-231-231-124-613-312-132-312-312-312-321-137-321-132-263-301-301-157-705-206-302-306-310-310-301-310-310-136-301-276-736-326-605-625-267-261-627-621-627-627-5_V.mp3)
  Segment 43: 75696ms ~ 76272ms (file: 675-765-672-675-567-562-765-672-756-657-657-675-675-671-671-726-375-735-670-672-672-607-607-607-103-607-657-672-103-632-623-567-572-316-176-102-361-370-302-672-602-621-761-675-7_W.mp3)
  Segment 44: 77272ms ~ 77848ms (file: 206 216-321-621 675-136-756-710-172-632-162-123-160-671-610-621-103-136-654-103-125-167-102-162-163-571-765-310-167-167-163-135-136-130-103-165-163-163-176-546-675-627 751-163-6_V.mp3)
  Segment 45: 78848ms ~ 79424ms (file: 167-120-761-103-136-167 675-617-162-156-602-103-103-103-137-103-165-362-613-167-672-103-136-163-751-157-615-163-163-136-130-165-136-157-136-725-137-167-163-136-126-173-175-173-6_V.mp3)
  Segment 46: 80424ms ~ 81000ms (file: 103-301-132-312-213-165-613-106-613-321-302-320-302-413-163-132-165-106-675-760-670-672-302-236-326-263-102-130-136-601-613-173-231-210-627-236-326-321-102-162-102-103-103-120-2_V.mp3)
  Segment 47: 82000ms ~ 82576ms (file: 302-206-312-231-312-316-261-713-571-103-135-103-127-167-167-316-103-103-167-132-103-507-157-267-165-631-615-103-671-617-103-275-162-617-601-625-612-157-671-136-167-167-136-136-1_V.mp3)
  Segment 48: 83576ms ~ 84152ms (file: 735-267-265-265-623-273-276-276-273-270-236-267-260-237-267-263-267-236-263-267-267-263-267-267-276-261-276-261-216-261-216-216-273-263-263-267-267-263-326-256-157-567-657-625-2_W.mp3)
  Segment 49: 85152ms ~ 85728ms (file: 723-301-136-102-546-564-326-620-236-206-260-267-203-326-260-602-620-675-675-765-732-725-301-103-102-107-132-541-574-150-136-136-102-136-612-165-630-675 276-163-137-136-317 620-2_W.mp3)
  Segment 50: 86728ms ~ 87304ms (file: 176-761-761-752-675 165-103-547-516-627-162-216 132-213-327-172-107-130-367-312-567-601-621-102-136-163-102-316-136-524-130-316-175-105-167-360-103-162-617-601-167-316-103-612-1_V.mp3)
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
> **Time**: 2026-04-03 07:53:48
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
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 45, "dur_ms": 320, "phase_lr_deg": 0.0},
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
{"word": "오늘", "start_ms": 30, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 150, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 300, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "점심 메뉴는 무엇이 좋을까요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "점심", "start_ms": 55, "dur_ms": 115, "phase_lr_deg": 5.0},
{"word": "메뉴는", "start_ms": 180, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "무엇이", "start_ms": 320, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "좋을까요", "start_ms": 450, "dur_ms": 120, "phase_lr_deg": 5.0}
]
},
{
"segment": 4,
"transcript": "비빔밥이나 불고기 어때요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "비빔밥이나", "start_ms": 20, "dur_ms": 240, "phase_lr_deg": 5.0},
{"word": "불고기", "start_ms": 270, "dur_ms": 160, "phase_lr_deg": 5.0},
{"word": "어때요", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 5.0}
]
},
{
"segment": 5,
"transcript": "한국어 공부는 재미있어요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "공보는", "start_ms": 200, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "재미있어요", "start_ms": 350, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "열심히 하면 잘할 수 있습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "열심히", "start_ms": 35, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "하면", "start_ms": 205, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "잘할", "start_ms": 315, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 415, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 465, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "지금 몇 시인지 아세요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "지금", "start_ms": 60, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "몇", "start_ms": 190, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "시인지", "start_ms": 260, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "아세요", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "오전 열한 시 반입니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "오전", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": -5.0},
{"word": "열한", "start_ms": 165, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "시", "start_ms": 295, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "반입니다", "start_ms": 385, "dur_ms": 190, "phase_lr_deg": -5.0}
]
},
{
"segment": 9,
"transcript": "주말에 무엇을 하실 거예요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "주말에", "start_ms": 30, "dur_ms": 140, "phase_lr_deg": -5.0},
{"word": "무엇을", "start_ms": 180, "dur_ms": 130, "phase_lr_deg": -5.0},
{"word": "하실", "start_ms": 320, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "거예요", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": -5.0}
]
},
{
"segment": 10,
"transcript": "친구와 영화를 보러 가요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "친구와", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "영화를", "start_ms": 210, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "보러", "start_ms": 350, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "가요", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "어떤 영화가 요즘 인기가 많죠",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "어떤", "start_ms": 25, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "영화가", "start_ms": 145, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "요즘", "start_ms": 295, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "인기가", "start_ms": 405, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "많죠", "start_ms": 535, "dur_ms": 40, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "액션 영화가 흥행 중이에요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "액션", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "영화가", "start_ms": 170, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "흥행", "start_ms": 320, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "중이에요", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "즐거운 시간 보내세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "즐거운", "start_ms": 80, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "시간", "start_ms": 250, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "보내세요", "start_ms": 380, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "네 감사합니다 당신도요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 35, "dur_ms": 50, "phase_lr_deg": -5.0},
{"word": "감사합니다", "start_ms": 100, "dur_ms": 240, "phase_lr_deg": -5.0},
{"word": "당신도요", "start_ms": 360, "dur_ms": 210, "phase_lr_deg": -5.0}
]
},
{
"segment": 15,
"transcript": "도움이 필요하면 말씀하세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "도움이", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "필요하면", "start_ms": 170, "dur_ms": 180, "phase_lr_deg": -5.0},
{"word": "말씀하세요", "start_ms": 360, "dur_ms": 210, "phase_lr_deg": -5.0}
]
},
{
"segment": 16,
"transcript": "여기서 지하철역이 먼가요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "여기서", "start_ms": 65, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "지하철역이", "start_ms": 205, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "먼가요", "start_ms": 435, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "걸어서 오 분 정도 걸려요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "걸어서", "start_ms": 30, "dur_ms": 140, "phase_lr_deg": -5.0},
{"word": "오", "start_ms": 180, "dur_ms": 50, "phase_lr_deg": -5.0},
{"word": "분", "start_ms": 240, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "정도", "start_ms": 310, "dur_ms": 110, "phase_lr_deg": -5.0},
{"word": "걸려요", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": -5.0}
]
},
{
"segment": 18,
"transcript": "생각보다 아주 가깝네요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "생각보다", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": -5.0},
{"word": "아주", "start_ms": 240, "dur_ms": 110, "phase_lr_deg": -5.0},
{"word": "가깝네요", "start_ms": 360, "dur_ms": 210, "phase_lr_deg": -5.0}
]
},
{
"segment": 19,
"transcript": "길 찾기가 어렵지 않을 거예요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "길", "start_ms": 45, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "찾기가", "start_ms": 95, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "어렵지", "start_ms": 245, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "않을", "start_ms": 385, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 475, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "친절하게 알려주셔서 고마워요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "친절하게", "start_ms": 20, "dur_ms": 160, "phase_lr_deg": -5.0},
{"word": "알려주셔서", "start_ms": 190, "dur_ms": 210, "phase_lr_deg": -5.0},
{"word": "고마워요", "start_ms": 410, "dur_ms": 160, "phase_lr_deg": -5.0}
]
},
{
"segment": 21,
"transcript": "천만에요 조심히 가세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "천만에요", "start_ms": 55, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "조심히", "start_ms": 245, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 395, "dur_ms": 170, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "다음에 또 뵙겠습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 35, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 175, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "뵙겠습니다", "start_ms": 245, "dur_ms": 320, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "네 안녕히 계세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 60, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "안녕히", "start_ms": 130, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "계세요", "start_ms": 290, "dur_ms": 180, "phase_lr_deg": -5.0}
]
},
{
"segment": 24,
"transcript": "좋은 하루 되세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "하루", "start_ms": 170, "dur_ms": 140, "phase_lr_deg": -5.0},
{"word": "되세요", "start_ms": 320, "dur_ms": 250, "phase_lr_deg": -5.0}
]
},
{
"segment": 25,
"transcript": "커피 한 잔 하실래요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "커피", "start_ms": 70, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 200, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "잔", "start_ms": 260, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "하실래요", "start_ms": 350, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "좋아요 카페로 갈까요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "좋아요", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "카페로", "start_ms": 190, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "갈까요", "start_ms": 360, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "저는 아이스 아메리카노요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "아이스", "start_ms": 165, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "아메리카노요", "start_ms": 315, "dur_ms": 260, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "저는 따뜻한 라떼 마실게요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 55, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "따뜻한", "start_ms": 165, "dur_ms": 160, "phase_lr_deg": -5.0},
{"word": "라떼", "start_ms": 335, "dur_ms": 110, "phase_lr_deg": -5.0},
{"word": "마실게요", "start_ms": 455, "dur_ms": 120, "phase_lr_deg": -5.0}
]
},
{
"segment": 29,
"transcript": "여기 분위기가 참 아늑하네요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "여기", "start_ms": 25, "dur_ms": 110, "phase_lr_deg": -5.0},
{"word": "분위기가", "start_ms": 145, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "참", "start_ms": 305, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "아늑하네요", "start_ms": 375, "dur_ms": 200, "phase_lr_deg": -5.0}
]
},
{
"segment": 30,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 31,
"transcript": "음악 소리도 너무 좋아요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "음악", "start_ms": 60, "dur_ms": 110, "phase_lr_deg": -5.0},
{"word": "소리도", "start_ms": 180, "dur_ms": 130, "phase_lr_deg": -5.0},
{"word": "너무", "start_ms": 320, "dur_ms": 90, "phase_lr_deg": -5.0},
{"word": "좋아요", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": -5.0}
]
},
{
"segment": 32,
"transcript": "공부하기에 딱 좋은 장소예요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "공부하기에", "start_ms": 35, "dur_ms": 210, "phase_lr_deg": -5.0},
{"word": "딱", "start_ms": 255, "dur_ms": 40, "phase_lr_deg": -5.0},
{"word": "좋은", "start_ms": 305, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "장소예요", "start_ms": 415, "dur_ms": 160, "phase_lr_deg": -5.0}
]
},
{
"segment": 33,
"transcript": "다음에 같이 공부하러 와요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 200, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "공부하러", "start_ms": 320, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "와요", "start_ms": 490, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "그거 정말 좋은 생각이네요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "그거", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "정말", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "좋은", "start_ms": 280, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "생각이네요", "start_ms": 390, "dur_ms": 180, "phase_lr_deg": -5.0}
]
},
{
"segment": 35,
"transcript": "언제가 시간이 제일 괜찮으세요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "언제가", "start_ms": 70, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "시간이", "start_ms": 200, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "제일", "start_ms": 340, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "괜찮으세요", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "저는 평일 오후가 한가해요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "평일", "start_ms": 140, "dur_ms": 110, "phase_lr_deg": -5.0},
{"word": "오후가", "start_ms": 260, "dur_ms": 140, "phase_lr_deg": -5.0},
{"word": "한가해요", "start_ms": 410, "dur_ms": 160, "phase_lr_deg": -5.0}
]
},
{
"segment": 37,
"transcript": "그럼 수요일 오후에 만나요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 55, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "수요일", "start_ms": 145, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "오후에", "start_ms": 285, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 415, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "약속 장소는 여기로 하죠",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "약속", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "장소는", "start_ms": 150, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "여기로", "start_ms": 290, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "하죠", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "알겠어요 그때 뵙겠습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "알겠어요", "start_ms": 45, "dur_ms": 160, "phase_lr_deg": -5.0},
{"word": "그때", "start_ms": 215, "dur_ms": 110, "phase_lr_deg": -5.0},
{"word": "뵙겠습니다", "start_ms": 335, "dur_ms": 240, "phase_lr_deg": -5.0}
]
},
{
"segment": 40,
"transcript": "오늘 만나서 즐거웠습니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "만나서", "start_ms": 170, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "즐거웠습니다", "start_ms": 310, "dur_ms": 260, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "저도요 조심히 들어가세요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "저도요", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "조심히", "start_ms": 165, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "들어가세요", "start_ms": 315, "dur_ms": 260, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "수고 많으셨습니다 안녕히 가세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "수고", "start_ms": 25, "dur_ms": 90, "phase_lr_deg": -5.0},
{"word": "많으셨습니다", "start_ms": 125, "dur_ms": 210, "phase_lr_deg": -5.0},
{"word": "안녕히", "start_ms": 345, "dur_ms": 140, "phase_lr_deg": -5.0},
{"word": "가세요", "start_ms": 495, "dur_ms": 80, "phase_lr_deg": -5.0}
]
},
{
"segment": 43,
"transcript": "조만간 다시 연락드릴게요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "조만간", "start_ms": 45, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 195, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "연락드릴게요", "start_ms": 315, "dur_ms": 260, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "네 기다리고 있겠습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 30, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "기다리고", "start_ms": 100, "dur_ms": 180, "phase_lr_deg": -5.0},
{"word": "있겠습니다", "start_ms": 290, "dur_ms": 280, "phase_lr_deg": -5.0}
]
},
{
"segment": 45,
"transcript": "항상 건강하시고 행복하세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "항상", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "건강하시고", "start_ms": 180, "dur_ms": 190, "phase_lr_deg": -5.0},
{"word": "행복하세요", "start_ms": 380, "dur_ms": 190, "phase_lr_deg": -5.0}
]
},
{
"segment": 46,
"transcript": "당신도 모든 일이 잘되길 바라요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "당신도", "start_ms": 40, "dur_ms": 130, "phase_lr_deg": -5.0},
{"word": "모든", "start_ms": 180, "dur_ms": 90, "phase_lr_deg": -5.0},
{"word": "일이", "start_ms": 280, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "잘되길", "start_ms": 370, "dur_ms": 110, "phase_lr_deg": -5.0},
{"word": "바라요", "start_ms": 490, "dur_ms": 80, "phase_lr_deg": -5.0}
]
},
{
"segment": 47,
"transcript": "감동이네요 감사합니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "감동이네요", "start_ms": 60, "dur_ms": 180, "phase_lr_deg": -5.0},
{"word": "감사합니다", "start_ms": 250, "dur_ms": 320, "phase_lr_deg": -5.0}
]
},
{
"segment": 48,
"transcript": "그럼 진짜 가볼게요 안녕",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 35, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 145, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "가볼게요", "start_ms": 265, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "안녕", "start_ms": 435, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "잘 가요 수요일에 봐요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "잘", "start_ms": 25, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "가요", "start_ms": 85, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "수요일에", "start_ms": 205, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "봐요", "start_ms": 395, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "바이 바이",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "바이", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "바이", "start_ms": 210, "dur_ms": 360, "phase_lr_deg": -5.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 07:53:48*
