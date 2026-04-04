# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=16288  time=03:47:27  uptime=11457s  mem=737MB  system_processes=364"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260404_034657.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 672-627-627-620-132-567 310-136 764 761-761-576-576-576-675-675-765-546-671_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 162-163-136-312-231-130-130-135-716-163-312-301-302-132-301-103-127-203-231-132-570-126-176-570-107-137-310-216-136-657-567-561-503-301-316-132-137-132-162-261-261-326-216-162-1_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 136-156-136-136-317-103-103-613-130-106-160-167-137-103-137-326-130-127-123-176-176-132-103-103-103-167-317-106-103-315-156-601-103-310-136-601-603-163-175-516-106-103-103-260-1_V.mp3)
  Segment 4: 4728ms ~ 13836ms (file: 756-103-751-102-627-123-312-756-756-510-260-756-107-756-627-106-106-103-163-167-106-637-267-260-302-257-673-756-572-561-123-103-132-675-267-627-627-625-756-675-267-576-137-276-1_W.mp3)
  Segment 5: 14836ms ~ 15412ms (file: 672-627-726-627-127-136-103-107-136_V.mp3)
  Segment 6: 16412ms ~ 16988ms (file: 163-321-567-137-132-162 576-675-657-576-567-576-567-756-756-765-765-756-765-576-576-576-675-657-675-756-576-567-576-576-576-576-675-765-765-765-657-675-567-576-657-576-675-675-6_W.mp3)
  Segment 7: 17988ms ~ 18564ms (file: 765-617-162-130-751-137-561-137-317-163-561-617-106-167-670-617-617 675-567-657 576-256-752-367-675-672-312-326-362-623-627-627-102-163-102-765-756-756-762-632-602-756-652-765-7_W.mp3)
  Segment 8: 19564ms ~ 20140ms (file: 156-136-137-132-103-103-134-531-512-106-165-136-163-120-136-103-103-673-167-136-132-103-106-413-312-136-621-621-620 251-135-126-673-173-136-136-103-136-106-123-213-126-130-163-2_V.mp3)
  Segment 9: 21140ms ~ 21716ms (file: 602-627-632-362-706-237-570-357-315 132-231-326-301-103-316-136-321-237-237-217-302-310-301-312-317-370-307-371-317-213-312-163-162-136-123-162-162-132-163-106-106-261-231-765-7_V.mp3)
  Segment 10: 22716ms ~ 23292ms (file: 761-637-765-632-756-765-762-763-126-617-631-705-750-751-756-705-750-756-571-507-750-671-756-756-761-136-213-260-321-312-261-162-612-762-621-760-716-123-167-326-312-231-651-510-1_W.mp3)
  Segment 11: 24292ms ~ 24868ms (file: 167-136-671-106-136-163-617-621-617-162-136-625-127-126-162-125-176-103-136-160-162-160-671-617-613-103-103-273-130-136-621-165-126-167-136-163-136-621-631-361-106-157-126-167-6_V.mp3)
  Segment 12: 25868ms ~ 26444ms (file: 106-160-601-163-613-163-621-263-621-627-261-162-103-316-317-617-103-106-176-570-756-167-106-617-167-106-562-162-706-760-703-103-103-103-136-567-567-650-167-130-160-601-320-310-1_V.mp3)
  Segment 13: 27444ms ~ 34860ms (file: 173-716-713-137-731-371-312-320-312-123-631-103-102-312-130-203-132-321-312-120-321-302-310-127-137-210-302-321-103-132-301-310-310-130-310-210-123-123-123-320-231-321-310-231-1_W.mp3)
  Segment 14: 35860ms ~ 36436ms (file: 326-236-326-632-236-326-236-326-362-362-623-260-326-236-326-362-632-362-632-362-362-632-263-236-326-326-627-263-326-236-326-362-362-637-632-635-362-361-365-635-365-635-635-635-6_W.mp3)
  Segment 15: 37436ms ~ 38012ms (file: 126-163-156-237-132-126-132-301-310-162-163-106-310-506-516-312-213-231-126-263-216-261-216-217-216-216-261-267-231-261-126-617-167-561-561-165-617-651-657-657-651-567_W.mp3)
  Segment 16: 39012ms ~ 39588ms (file: 163-126-326-312-321-163-123-316-136-105-165-216-163-137-316-132-132 312-132-132-143-574-756-675-132-312 310-413-431-361-132-627-267-137-254-124-615-123-132-312-103-132-310-301-3_V.mp3)
  Segment 17: 40588ms ~ 41164ms (file: 316-103-132-162-103-132-103-163-136-132-206-102-310-132-106-673-613-163-130-613-106-167-163-132-216-102-623-730-673-613-312-103-612-637-630-316-361-312-310-267-216-126-623-326-3_V.mp3)
  Segment 18: 42164ms ~ 42740ms (file: 156-153-150-367-765-670-237-760-576-576-675-765-237-207-376-562-752-765-276-527-673-137-137-672-267-573-375-276-263-756-236-325-726-143-374-327-376-132 213-154-310-302-316-310-3_W.mp3)
  Segment 19: 43740ms ~ 44316ms (file: 136 321-136-361-623-326-326-362-156-123 632-361-163-312-317-103-103-276-263-136-621-126-213 124-163-135-132-312-753-574-571-751-761-765-763-761-756-756-571-326-507-532-564-627-6_V.mp3)
  Segment 20: 45316ms ~ 45892ms (file: 310-132-321-132-216-123-213-312-132-312-321-136-237-260-216-137-765-267-675-672-672-126-310-627-261-162-756-621-261-627-267-103-120-230-123-231_W.mp3)
  Segment 21: 46892ms ~ 47468ms (file: 132-756-750-705-345-327-312-675-627-607-627-624-265-312-617-613-126-167-627-316-167-126-126-162-162-316-301 675-657-165-615-167-612-726-617-632-163-312-103-103-715-137-576-705-1_V.mp3)
  Segment 22: 48468ms ~ 53472ms (file: 326-312-241-154-143-103-231-312-102-103-130-612-413-102-103-123-123-215-136-216-623-213-672-261-267-263-312-136-123-126-617-672-673-675-631-317-635-635-361-326-312-673-627-653-6_W_stt_the.mp3)
  Segment 23: 54472ms ~ 55048ms (file: 216-712-172-136-163-230-213-310-123-312-301-213-602-230-301-103-163-613-671-672-310-135-201-312-675-260-302-103-136-157-301-301-103-130-103-136-103-130-103-301-231-130-132-136-1_V.mp3)
  Segment 24: 56048ms ~ 56624ms (file: 103-163-610-130-103-103-103-671-761-263-160-175-160-103-316-163-167-137-163-132-130-103-132-103-130-317-167-316-136-103-103-541-517-250-157-251-157-130-260-165-132-136-103-136-1_V.mp3)
  Segment 25: 57624ms ~ 58200ms (file: 617-103-163-312-312-123-176-607-126-132-103-521-130-126-130-762-567-260-625-356-103-657-106-136-106-136-160-623-615-125-607-106-126-162-163-136-167-162-321-176-162-306-103-106-6_V.mp3)
  Segment 26: 59200ms ~ 59776ms (file: 362-635-632-160-765-136-326-167-163-163-126-162-126-103-130-312-163-310-302-310-162-176-716-675-617-631-137-705-716-756-567-576-675-167-571-507-675-106-167-162-167-610-162-106-1_V.mp3)
  Segment 27: 60776ms ~ 61352ms (file: 571-573-137-713-136-315-716-376-765-567-675-763-175-765-671-176-175-153-165-165-156-561-576-651-657-162-617-612-627-156-165-357-763-371-731-367-713-173-731-731-675-765-736-375-7_W.mp3)
  Segment 28: 62352ms ~ 62928ms (file: 130-103-301-302-302-103-136-672-260-321-216-103-301-103-132-321-352-123-103-123-231-260-310-312-627-623-120-627-260-627-261-206-236-130-652-203-231-253-253-523-315-130-132-134-1_V.mp3)
  Segment 29: 63928ms ~ 64504ms (file: 123-125-571-576-516-571-517-516-576-512-165-263-165-156-157-312-123-216-236-123-163-127-162-123-321-132-312-327-312-312-132-312-316-163-136-132-123-132-316-136-132-136-132-126-2_W.mp3)
  Segment 30: 65504ms ~ 66080ms (file: 163 102-102-321-310-123-216-210-103-107-132-132-123-213 631-175-517-175-751-751-756-574-267-126-163 231-167-671 675-765-675-675 162-162 621-627-627-627-263-623-621-627-627-623-6_W.mp3)
  Segment 31: 67080ms ~ 67656ms (file: 371-103-136-276-172-751-103-103-103-123-635-216-167-126-136-675-103-357-127-162-126-713-106-163-103-130-130-103-105-132-156-106-765_V.mp3)
  Segment 32: 68656ms ~ 69232ms (file: 103-167-103-136-257-516-627 623-623-123-103-671-361-106-576-107-136-671-160-653-163-162-136-176-106-160-163-167-137-136-103-317-671-516-621-163-156-103-623-627 106-126-632-163-3_V.mp3)
  Segment 33: 70232ms ~ 70808ms (file: 612-631-672-316-326-362-163-126-216-231-261-213-103-132-601-612-620-612-362-632-132-103-103-621-627-632-623-613-672-126-210-130-136-627-627-672-631-231-137-173-617-106-627-367-2_V.mp3)
  Segment 34: 71808ms ~ 72384ms (file: 126-165-163-671-165-103-106-716-617-615-162-106-601-126-106-136-107-160-160-167-102-602-126-160-567-167-362-162-126-236-251-162-603-132-267-362-632-621-612 625-106-561-726-162-1_V.mp3)
  Segment 35: 73384ms ~ 81700ms (file: 312-163-123-310-312-136-325-521-320-236-103-351-263-236-206-301-163-261-632-312-132-316-325-103-261-267-735-130-517-321-201-230-123-306-320-260-267-627-201-572-162-267-203-210-5_W.mp3)
  Segment 36: 82700ms ~ 83276ms (file: 134-132-310-301-163-132-123 631-136 756_V.mp3)
  Segment 37: 84276ms ~ 84852ms (file: 765-376-603-163-106-160-107-601-160-103-160-610-612-167-163-761-617-167-162 675-657-120-106_W.mp3)
  Segment 38: 85852ms ~ 86428ms (file: 176-172-312-132-132-134-132-123-134-132-134-134-135-231-163-621-206-261-162-136-163-103-130-136-167-610-261-621-617-162-261-167-126-326-167-167-130-601-167-136 136-206-176-165-1_V.mp3)
  Segment 39: 87428ms ~ 88004ms (file: 627-216-602-260-625-762-621-623-623-621-620-362-620-132-130-312-136_V.mp3)
  Segment 40: 89004ms ~ 89580ms (file: 326-230-230-325-325-235-253-236-263-261-231-236-231-321-230-256-253-523-562-256-263-235-315-321-352-532-523-524-253-325-325-237-325-256-362-627-362-625-623-321-235-236-230-320-5_W.mp3)
  Segment 41: 90580ms ~ 91156ms (file: 256-261-267-627-267-276-672-367-361-263-623-312-632-546-531-156-156-163-316-312-231-542-524-352-352-263-263-265-236-236-231-126-261-213-263-216-236-263-236-261-526-562-652-625-2_W.mp3)
  Segment 42: 92156ms ~ 92732ms (file: 160-136-301-765-267-605-620-263-623-672-167-106-163-103-321-203-206-213 672-625-267-625-261-210-102-210-126-102-321-312-301-326-316-230-362-261-263-621-261-260-230-236-236-236-2_W.mp3)
  Segment 43: 93732ms ~ 94308ms (file: 621-162-106 267-607-621-627-263-102-201-361-603-607-607-672-672-620-263-360-362-103-106-160-610-612-612-106-102-327-136-103-103-574-754-756-371-361-307-307-762-612-620-163-103-3_V.mp3)
  Segment 44: 95308ms ~ 95884ms (file: 263-261-267-321-136-651-507-573-675-102-156-163-176-137-312-360-623-237-132-163-167-107-301-137-750-754-375-705-756-270-207-203-123-267-103-136-136-132-315-571-751-754-750-750-5_V.mp3)
  Segment 45: 96884ms ~ 97460ms (file: 567 567-576-576-657-567-657-653-675-652-653 132-132-506-560-650-625-507-657-572-567-507-256-576-216-103 103_V.mp3)
  Segment 46: 98460ms ~ 99036ms (file: 316-136-765-163-576-735-132 137-103-165-103-316-103-136-130-107-136-167-103-103-103-103-132-132-103-165-126_V.mp3)
  Segment 47: 100036ms ~ 100612ms (file: 560-506-657-567-627 605-605-605-567-605-567-657-567-567-650-657-567-506-567-567-567-657-652 605-650-605-650-560-650-657-652-567-567-657 650-560-605-506-657-650-605-650-657-567-6_V.mp3)
  Segment 48: 101612ms ~ 102188ms (file: 162-123-362-123 173 562-167-163 167-761 571-326-263-261-672-163-123-321-312-563-231-326-172-327-162-762-637-163-675-576-617-673-675-673-627-621-263-261-251-673-312-321-321-321 7_W.mp3)
  Segment 49: 103188ms ~ 110316ms (file: 723-731-127-317-123-130-312-321-123-357-531-321-574-230-132-132-123-102-230-235-132-751-570-576-105-102-203-257-750-251-102-130-217-671-206-302-130-302-302-321-320-320-102-102-1_V.mp3)
  Segment 50: 111316ms ~ 111892ms (file: 362-167-625-326-362-361-761-265-237-164-163-231-615-617-325-157-713 675-376-167-143-257-374-325-571-275-132-156-651-132-275-251-274-451-324-374-713-471-356-371-132-127-153_V.mp3)
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
> **Time**: 2026-04-04 03:49:04
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "안녕하세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "안녕", "start_ms": 50, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "하세요", "start_ms": 300, "dur_ms": 226, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "만나서 반갑습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "만나서", "start_ms": 40, "dur_ms": 240, "phase_lr_deg": 2.5},
{"word": "반갑습니다", "start_ms": 280, "dur_ms": 296, "phase_lr_deg": 2.5}
]
},
{
"segment": 3,
"transcript": "오늘 날씨가 정말 좋네요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 35, "dur_ms": 150, "phase_lr_deg": 1.0},
{"word": "날씨가", "start_ms": 185, "dur_ms": 180, "phase_lr_deg": 1.0},
{"word": "정말", "start_ms": 365, "dur_ms": 110, "phase_lr_deg": 1.0},
{"word": "좋네요", "start_ms": 475, "dur_ms": 101, "phase_lr_deg": 1.0}
]
},
{
"segment": 4,
"transcript": "한국어 공부를 시작한 지 얼마나 되셨나요? 저는 작년부터 배우기 시작했어요.",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 60, "dur_ms": 400, "phase_lr_deg": -1.5},
{"word": "공부를", "start_ms": 460, "dur_ms": 350, "phase_lr_deg": -1.5},
{"word": "시작한", "start_ms": 810, "dur_ms": 320, "phase_lr_deg": -1.5},
{"word": "지", "start_ms": 1130, "dur_ms": 100, "phase_lr_deg": -1.5},
{"word": "얼마나", "start_ms": 1230, "dur_ms": 380, "phase_lr_deg": -1.5},
{"word": "되셨나요?", "start_ms": 1610, "dur_ms": 450, "phase_lr_deg": -1.5},
{"word": "저는", "start_ms": 2200, "dur_ms": 300, "phase_lr_deg": -1.5},
{"word": "작년부터", "start_ms": 2500, "dur_ms": 500, "phase_lr_deg": -1.5},
{"word": "배우기", "start_ms": 3000, "dur_ms": 400, "phase_lr_deg": -1.5},
{"word": "시작했어요.", "start_ms": 3400, "dur_ms": 5708, "phase_lr_deg": -1.5}
]
},
{
"segment": 5,
"transcript": "네 맞아요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "맞아요", "start_ms": 170, "dur_ms": 406, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 7,
"transcript": "Great to meet you.",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "Great", "start_ms": 45, "dur_ms": 180, "phase_lr_deg": 5.0},
{"word": "to", "start_ms": 225, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "meet", "start_ms": 325, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "you.", "start_ms": 475, "dur_ms": 101, "phase_lr_deg": 5.0}
]
},
{
"segment": 8,
"transcript": "이것도 한번 읽어보세요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "이것도", "start_ms": 30, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "한번", "start_ms": 210, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "읽어보세요", "start_ms": 330, "dur_ms": 246, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "어려운 단어가 꽤 많네요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "어려운", "start_ms": 55, "dur_ms": 200, "phase_lr_deg": -2.0},
{"word": "단어가", "start_ms": 255, "dur_ms": 150, "phase_lr_deg": -2.0},
{"word": "꽤", "start_ms": 405, "dur_ms": 80, "phase_lr_deg": -2.0},
{"word": "많네요", "start_ms": 485, "dur_ms": 91, "phase_lr_deg": -2.0}
]
},
{
"segment": 10,
"transcript": "천천히 하면 다 할 수 있어요",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "천천히", "start_ms": 40, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "하면", "start_ms": 260, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 360, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 420, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 480, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 520, "dur_ms": 56, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "발음이 아주 좋으시네요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "발음이", "start_ms": 25, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 225, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "좋으시네요", "start_ms": 345, "dur_ms": 231, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "감사합니다 연습 많이 했어요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 35, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "연습", "start_ms": 285, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 385, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "했어요", "start_ms": 465, "dur_ms": 111, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "자, 다음 문장으로 넘어가 볼까요? 이번에는 조금 더 긴 문장입니다.",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "자,", "start_ms": 70, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 220, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "문장으로", "start_ms": 420, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "넘어가", "start_ms": 770, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "볼까요?", "start_ms": 1020, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이번에는", "start_ms": 1500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 1850, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 2050, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "긴", "start_ms": 2150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "문장입니다.", "start_ms": 2300, "dur_ms": 5116, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 15,
"transcript": "어떤 문장인가요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "어떤", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "문장인가요?", "start_ms": 240, "dur_ms": 336, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "배가 고파서 식당에 가고 싶어요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "배가", "start_ms": 30, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "고파서", "start_ms": 150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "식당에", "start_ms": 300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가고", "start_ms": 450, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "싶어요", "start_ms": 510, "dur_ms": 66, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "좋은 문장이네요 메뉴는요?",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 45, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "문장이네요", "start_ms": 165, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "메뉴는요?", "start_ms": 365, "dur_ms": 211, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "비빔밥을 제일 좋아합니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "비빔밥을", "start_ms": 50, "dur_ms": 250, "phase_lr_deg": -3.0},
{"word": "제일", "start_ms": 300, "dur_ms": 120, "phase_lr_deg": -3.0},
{"word": "좋아합니다", "start_ms": 420, "dur_ms": 156, "phase_lr_deg": -3.0}
]
},
{
"segment": 19,
"transcript": "저도 비빔밥 정말 좋아해요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "저도", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "비빔밥", "start_ms": 130, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 330, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "좋아해요", "start_ms": 450, "dur_ms": 126, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "매운 음식을 잘 드시나 봐요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "매운", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "음식을", "start_ms": 160, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 310, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "드시나", "start_ms": 370, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "봐요", "start_ms": 490, "dur_ms": 86, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "네 조금 맵지만 맛있어요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 35, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 115, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "맵지만", "start_ms": 235, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "맛있어요", "start_ms": 415, "dur_ms": 161, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "한국 음식 중에서 삼겹살도 유명하죠. 나중에 꼭 같이 먹으러 가요.",
"speaker": "speaker_1",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "한국", "start_ms": 65, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "음식", "start_ms": 315, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "중에서", "start_ms": 515, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "삼겹살도", "start_ms": 815, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "유명하죠.", "start_ms": 1265, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "나중에", "start_ms": 1800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "꼭", "start_ms": 2150, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 2250, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "먹으러", "start_ms": 2450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "가요.", "start_ms": 2750, "dur_ms": 2254, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "좋아요 기대할게요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "좋아요", "start_ms": 40, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "기대할게요", "start_ms": 260, "dur_ms": 316, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "이제 다음 단계를 해볼까요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 155, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "단계를", "start_ms": 275, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "해볼까요", "start_ms": 425, "dur_ms": 151, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "숫자 읽기 연습을 해봅시다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "숫자", "start_ms": 45, "dur_ms": 150, "phase_lr_deg": 1.5},
{"word": "읽기", "start_ms": 195, "dur_ms": 100, "phase_lr_deg": 1.5},
{"word": "연습을", "start_ms": 295, "dur_ms": 150, "phase_lr_deg": 1.5},
{"word": "해봅시다", "start_ms": 445, "dur_ms": 131, "phase_lr_deg": 1.5}
]
},
{
"segment": 26,
"transcript": "일 이 삼 사 오 육",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "일", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 110, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "삼", "start_ms": 190, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "사", "start_ms": 270, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "오", "start_ms": 350, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "육", "start_ms": 430, "dur_ms": 146, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 28,
"transcript": "아주 잘 하셨어요 완벽해요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "아주", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 155, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "하셨어요", "start_ms": 235, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "완벽해요", "start_ms": 415, "dur_ms": 161, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 30,
"transcript": "하나 둘 셋 넷 다섯",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "하나", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "둘", "start_ms": 145, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "셋", "start_ms": 225, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "넷", "start_ms": 305, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "다섯", "start_ms": 385, "dur_ms": 191, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "순 우리말 숫자도 잘 아시네요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "순", "start_ms": 50, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "우리말", "start_ms": 110, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "숫자도", "start_ms": 260, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 410, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "아시네요", "start_ms": 470, "dur_ms": 106, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "이건 조금 헷갈릴 때가 있어요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "이건", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 160, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "헷갈릴", "start_ms": 260, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "때가", "start_ms": 410, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 490, "dur_ms": 86, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "괜찮아요 계속 연습하면 익숙해질 거예요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "괜찮아요", "start_ms": 55, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 205, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "연습하면", "start_ms": 305, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "익숙해질", "start_ms": 485, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 635, "dur_ms": 141, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "응원해 주셔서 감사합니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "응원해", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "주셔서", "start_ms": 180, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 330, "dur_ms": 246, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "자, 그럼 이제 마지막으로 오늘 배운 내용을 복습해 볼까요? 하나씩 차근차근 해봐요.",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 75,
"audio_type": "speech",
"words": [
{"word": "자,", "start_ms": 75, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "그럼", "start_ms": 225, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 375, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "마지막으로", "start_ms": 495, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 845, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "배운", "start_ms": 995, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "내용을", "start_ms": 1115, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "복습해", "start_ms": 1315, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "볼까요?", "start_ms": 1615, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "하나씩", "start_ms": 2200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "차근차근", "start_ms": 2500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "해봐요.", "start_ms": 2900, "dur_ms": 5416, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "준비됐습니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "준비", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "됐습니다", "start_ms": 240, "dur_ms": 336, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "화이팅 하세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "화이팅", "start_ms": 35, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "하세요", "start_ms": 285, "dur_ms": 291, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "열심히 하겠습니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "열심히", "start_ms": 45, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "하겠습니다", "start_ms": 265, "dur_ms": 311, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "오늘 수업은 여기까지입니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "수업은", "start_ms": 170, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "여기까지", "start_ms": 320, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "입니다", "start_ms": 470, "dur_ms": 106, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 41,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 42,
"transcript": "수고하셨습니다 선생님",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "수고", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "하셨습니다", "start_ms": 160, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "선생님", "start_ms": 380, "dur_ms": 196, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "다음에 또 만나요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 35, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 215, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 315, "dur_ms": 261, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "좋은 하루 보내세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 45, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "하루", "start_ms": 195, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "보내세요", "start_ms": 345, "dur_ms": 231, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 46,
"transcript": "안녕히 계세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 30, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "계세요", "start_ms": 280, "dur_ms": 296, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 48,
"transcript": "Goodbye everyone.",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Goodbye", "start_ms": 40, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "everyone.", "start_ms": 290, "dur_ms": 286, "phase_lr_deg": -5.0}
]
},
{
"segment": 49,
"transcript": "끝까지 들어주셔서 감사합니다. 다음에 더 유익한 내용으로 찾아뵙겠습니다.",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "끝까지", "start_ms": 60, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "들어주셔서", "start_ms": 360, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "감사합니다.", "start_ms": 760, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "다음에", "start_ms": 1500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 1800, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "유익한", "start_ms": 1900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "내용으로", "start_ms": 2250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "찾아뵙겠습니다.", "start_ms": 2700, "dur_ms": 4428, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "감사합니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "감사", "start_ms": 30, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "합니다", "start_ms": 280, "dur_ms": 296, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-04 03:49:04*
