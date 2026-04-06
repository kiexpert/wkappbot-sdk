# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=29532  time=03:49:04  uptime=8238s  mem=2211MB  system_processes=364"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260404_034516.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 325 754-756-675-267-207-276-502-520-574-572 326-236-203-123-162-756-756-756-765-765-762-607-306-136-765-705-657-672-627-623-306-310-312 621-236-160-652-625-621-627-620-652-562-6_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 123-137 126 263-265-124-126-123-136-136-163-362-316-136-317-167-176-621-621-623-316-362-136-126-132-162-126-123-136-132-162-132-316-132-136-316-132-126-163-765-673-136-163-136-2_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 621-627-762-765-762-765-765-712-716-172 751-765 571-163-375-102-136-103-317-516-632-516-156-675-167-561-567-576-236-765-765-130-136-136-316-136-123-167-756-216-106-312-132-136-1_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 765-761 756-576-576-576-162-326-273-276-237-130-167-162-132-132-132-263-375-713 765-756-753-756-752-765-765-765 765-730-761-216-216-765-765-760-706-765-765-765-716-710-701-765-7_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 163-216 312-130-312-301-310-157-120 675-612-236-275-627-671-132 326-130-130_V.mp3)
  Segment 6: 7880ms ~ 13748ms (file: 321-627-103-102-203-123-126-102-765-107-120-361-316-317-130-162-301-321-310-130-362-631-165-165-165-310-316-136-106-517-672-167-657-603-576-657-365-652-657-675-765-756-326-275-6_W.mp3)
  Segment 7: 14748ms ~ 15324ms (file: 136-156-106-136-361-106-361-265-601-132-576-126-163-671-657-276-627-103-760-102-103-103-136-103-316-756-157-123-106-103-316-103-301-132-316-105-761-137-236-162-213-271-275-231-1_V.mp3)
  Segment 8: 16324ms ~ 16900ms (file: 173-107-136-574-301-302-302-574-725-756-621-152-576-531-507-105-105-541-507-576-574-574-546-573-527-573-375-537-537-137-517-165-125-153-657-675-706-760-763-560-562-207-206-261-1_W.mp3)
  Segment 9: 17900ms ~ 25928ms (file: 317-120-231-206-627-267-672-762-260-261-206-132-123-627-602-260-261-627-102-132-271-267-756-276-267-750-176-267-206-123-260-326-267-267-257-102-672-675-312-103-267-276-236-276-2_W.mp3)
  Segment 10: 26928ms ~ 27504ms (file: 106-236-120-321-123-132-106-312-130-103-103-137-167-312-106-160-163-126-163-361-136-106-103-673-301-130-120-263-123-362-132-123-312-362 106-103-106-103-130-103-106-163-132-136-2_V.mp3)
  Segment 11: 28504ms ~ 29080ms (file: 627-261-621-261-621-261-263-236-312-321_W.mp3)
  Segment 12: 30080ms ~ 34328ms (file: 236-236-231-231-351-534-537-275-273-720-723-726-702-702-270-273-276-276-276-203-207-201-260-213-201-210-207-207-230-273-230-210-237-237-213-327-201-210-203-230-237-231-210-237-2_V.mp3)
  Segment 13: 35328ms ~ 35904ms (file: 301-132-301-310-102-130-162-130-143-132-261-201-201-217-201-217-123-123-312 627-632-762-132-261-231-236-237-236-601-326-102-163-670-675-670-607 132-143-103-143-134-132-143-135-1_V.mp3)
  Segment 14: 36904ms ~ 43204ms (file: 270-726-267-157-627-756-706-632-136-132-765-236-123-132-130-206-231-103-132-327-301-267-236-267-216-261-267-217-203-207-103-675-106-162-672-260-263-760-102-126-617-173-167-106-1_W.mp3)
  Segment 15: 44204ms ~ 44780ms (file: 163-132-164-163-613-162-106-615-567-576-756-657-765-367-765-376-761-576-576-625-176-756-765-574-567-765-657-675-756-765-756-673-675-627-321-275-675-765-576-567-765-537-537-534-1_W.mp3)
  Segment 16: 45780ms ~ 46356ms (file: 127-312-620-602-627-627-206-620-620-260-675-623-260-260-326-362-310-316-163-670-260-206-567-267 167-260-326-230-213-301-670-706-132-312-615-260-167-106-103-136-103-163-216-102-1_V.mp3)
  Segment 17: 47356ms ~ 47932ms (file: 236-517-726-172-762-726-761-712-103-130-136-536-316-651-137-317-317-106-103-316-103-175-135-132-136-103-651-632-132-312-132-136-317-130-546-156-516 103-102-157-153-103-536-317-5_V.mp3)
  Segment 18: 48932ms ~ 49508ms (file: 216-236-126-216 716-672-156-627-672-627-670-762-670-627-216-106-162-762-126-132-132-123-102-130-126-136-231-263-627-623 267-267-267 361-623-673-617 657-657 657-653-547-370-130-6_W.mp3)
  Segment 19: 50508ms ~ 51084ms (file: 627-625-675-256-652-657-625-216-267_W.mp3)
  Segment 20: 52084ms ~ 58492ms (file: 231-236-762-267-206-213-231-263-236-213-126-123-201-217-276-276-213-237-231-237-237-327-376-312-321-132-126-231-132-321-132-231-276-231-213-231-231-231-103-132-210-231-231-130-1_W.mp3)
  Segment 21: 59492ms ~ 60068ms (file: 275-267-620-625-261-621-267-176-167-672-217-670-605-126-127-617-167-672-162-671-672-762-672-675-672-675-126-602-162-167-675-107-670-671-657-675-672-675-675-672-765-672-672-672-6_W.mp3)
  Segment 22: 61068ms ~ 61644ms (file: 501-102-176-125-652-136-316-102-130-132-652-236-673-326-123-271-310-137-130-317-327-571-752-317-132-236-162-136-312-312-136-130-126-123-316-316-573-576-573-765-267-215-235-263-1_V.mp3)
  Segment 23: 62644ms ~ 63220ms (file: 206-260-762-326-231-321-306-260-206-267-201-126-301-312-301-136-301-236-320-301-103-301-302-632-263-321-312-103-132-302-123-206-236-260-203-576-276-572-672-756-765-236-167-107-7_W.mp3)
  Segment 24: 64220ms ~ 69836ms (file: 514-542-261-130-162-621-517-572-261-106-213-627-625-627-157-261-267-261-275-126-231-261-352-765-123-137-136-136-176-216-216-207-267-267-216-571-571-260-162-261-231-150-210-621-2_W.mp3)
  Segment 25: 70836ms ~ 71412ms (file: 602-672-672-627-231-352-176-706-132-103-362-106-673-602-162-136-103-312-317-136-137-310 103-132-203-321-231-273-231-123-132-132-132-175-163-312-305-756-576-350-321-312-302-367-5_V.mp3)
  Segment 26: 72412ms ~ 72988ms (file: 163-160-103-123-167-162-106-167-136-102-103-675-130-315-103-312-173-321-162-176-216-136-567-136-312-613-130-103-137-136-516-657-156-236 716-762-263-162 176-170-136-716-167-176-6_V.mp3)
  Segment 27: 73988ms ~ 74564ms (file: 230-231-327-675-627-263-267-312-312-326-613-135-361_W.mp3)
  Segment 28: 75564ms ~ 84924ms (file: 623-627-102-327-216-316-672-627-576-265-260-275-267-261-267-261-267-103-106-130-316-756-762-216-756-632-163-613-163-631-163-361-160-160-126-276-612-162-276-712-276-376-175-267-7_W.mp3)
  Segment 29: 85924ms ~ 86500ms (file: 176-716-126-176-176-163-261-621-261-263-136-312-321-130-172-160-167-157-167-316-123-163-617-167-163-617-601-560-670-706-675-675-765-760-675-765-762-672-627-263-672-132-236-162-1_W.mp3)
  Segment 30: 87500ms ~ 88076ms (file: 602-236-230-206-206-312-361-621-263-612-312-176-102-362-310-326-321-253-260-236-260-210-320-312-567-562-102-203-260-206-156-136-657-107-261-162-123-612-312-132-136-567-675-316-3_V.mp3)
  Segment 31: 89076ms ~ 89652ms (file: 541-561-736-126-126-672-610-263-316-236-625-235-267-627-267-672-267-562-620-627-623-267-627-267-567-265-263-267-263-276-267-672 132-170_W.mp3)
  Segment 32: 90652ms ~ 91228ms (file: 321-302-306-321-320-310-231-206-547-546-627-627-236-230-253-326-301-310-321-123-136-106-516-265-236-236-203-230-263-206-316-130-236-542-526-203-320-302-305-573-576-357-253-327-3_W.mp3)
  Segment 33: 92228ms ~ 92804ms (file: 326-230-230-325-325-235-253-236-263-261-231-236-231-321-230-256-253-523-562-256-263-235-315-321-352-532-523-524-253-325-325-237-325-256-362-627-362-625-623-321-235-236-230-320-5_W.mp3)
  Segment 34: 93804ms ~ 94380ms (file: 675-176-517-123-237-627-572-376-261-267-267-621-132-312-216-261-312-126-103-312-163-765-761-607-672-106-172-361-130-130-103-103-132-106-105-610-163-160-216-672-132-572-372-751-7_V.mp3)
  Segment 35: 95380ms ~ 95956ms (file: 163-167-106-103-162-612-256-576-652-572-536-157-516-157-751-316-165-176-167-157-175-510-561-576-576-576-567-562-567-752-675-756-574-576-756-576-675-176-176-136-176-176-175-172-1_W.mp3)
  Segment 36: 96956ms ~ 97532ms (file: 675-607-163-605-167-613-136-103-610-613-361 132-172-127-137-312-153-251-651-562-675-562-576-623-630 607-630-620-261-123-137-612-106-136-621-631-670-637-172-361-675-631-671-613-1_W.mp3)
  Segment 37: 98532ms ~ 99108ms (file: 361-231-321-326-326-236-236-362-326-362-326-213-321-263-326-126-326-236-261-326-132-213-236-231-326-312-213-231-567-132-136-130-312-213-312-236-132-312-102-312-312-673-623-326-3_V.mp3)
  Segment 38: 100108ms ~ 100684ms (file: 165-615-136-627-130-201-136-316-163-160-136-163-106-136-135-576-561-130-132-137-710-137-103-103-103-103 672-671 167-163 621-103-136-103-172-136-103-137-316-136-132-317-130-136-1_V.mp3)
  Segment 39: 101684ms ~ 102260ms (file: 312-132-163-672-132-132-321-103-312-263-603-312-302-312-301-203-305-573-103-150-103-657-136-103-103-103-312-302-263-267-206-302-310-132-176-317-321-132-321-301-203-260-265-352-3_W.mp3)
  Segment 40: 103260ms ~ 113772ms (file: 230-103-103-213-102-231-523-156-102-216-236-216-103-721-270-130-230-130-237-237-752-253-217-207-206-267-236-213-213-231-120-213-235-230-102-123-130-302-201-120-130-103-357-230-2_V.mp3)
  Segment 41: 114772ms ~ 115348ms (file: 576-547-574-574-573-574-576-576-576-576-574-576-574-576-567-576-574-576-576-574-576-756-576-567-576-574-562-574-576-576-576-576-576-547-574-547-576-754-574-574-574-537-574-537_W.mp3)
  Segment 42: 116348ms ~ 116924ms (file: 203-206-256-260-321-126-132-310-301-306-306-320-230-320-607-607-310-547-570-253-276-206-216-103-302-306-320-360-302-203-206-320-310-312-103-301-136-150-132-316-321-302-310-103-6_V.mp3)
  Segment 43: 117924ms ~ 118500ms (file: 627-620-710-270-276-270-201 267-267-267-210-216-126-261-320-230-320-320-301-123-162-261-260-206-126-216-130-301-130-103-316-136-126-163-321-301-160-306-136-260-326-302-321-301-7_V.mp3)
  Segment 44: 119500ms ~ 120076ms (file: 756-756-710-731-120-130-130-103-106-120-162-132-326-103-216-620-162-107-126-627-162-102-103-102-106-130-106-136-103-103-301-310-130-153-103-105-105-103-103-520-524-527-520-502_W.mp3)
  Segment 45: 121076ms ~ 121652ms (file: 163-316-326-735-372-302-306-231-321-301-302-703-673-136-312-106-130-301-316-371-163-763-376-136-301-301-316-736-106-570-361-310-136-316-103-310-103-317-106-163-701-137-612-126-2_W.mp3)
  Segment 46: 122652ms ~ 123228ms (file: 632-162-136-162-132-312-123-621-126-632-263-261-136-120-102-126-175-231-162-210-201-210-213-206-261-236-231-231-312-302-106-162-130-316-126-231-236-261-260-236-310-310-623-362-1_V.mp3)
  Segment 47: 124228ms ~ 124804ms (file: 231-326-132-361-130-126-312-126-132-132-310-132-671-617-267-162-362-326-361-320-632-627-607-627-574-562-216-130-130-102-613-765 756-675-103-132-301-310-321-136-167 123-103-103-1_W.mp3)
  Segment 48: 125804ms ~ 126380ms (file: 750-627-573-560 675-671-627 765-763-756-576-576-572-752-576-760-673-670-206-231_W.mp3)
  Segment 49: 127380ms ~ 136668ms (file: 312-627-361-352-532-203-132-231-237-206-210-301-132-203-632-130-263-136-163-527-132-231-263-231-167-756-765-271-720-263-625-673-316-326-765-157-235-102-102-236-312-625-265-130-7_W.mp3)
  Segment 50: 137668ms ~ 142780ms (file: 173-107-107-732-123-263-261-162-260-130-613-312-136-715-316-316-316-631-136-316-136-613-106-612-126-612-120-613-627-672-623-612-672-276-301-562-263-236-136-176-271-301-310-312-2_V.mp3)
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
> **Time**: 2026-04-04 03:50:51
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 2,
"transcript": "안녕하세요 여러분 오늘 날씨가 참 좋네요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 45, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "여러분", "start_ms": 200, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 325, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 410, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "참", "start_ms": 515, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 550, "dur_ms": 26, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "만나서 정말 반갑습니다 제 이름은 민수예요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "만나서", "start_ms": 30, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "제", "start_ms": 410, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "이름은", "start_ms": 445, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "민수예요", "start_ms": 530, "dur_ms": 46, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 5,
"transcript": "Hello everyone. Welcome to the class.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "Hello", "start_ms": 60, "dur_ms": 120, "phase_lr_deg": 2.5},
{"word": "everyone.", "start_ms": 190, "dur_ms": 150, "phase_lr_deg": 2.5},
{"word": "Welcome", "start_ms": 350, "dur_ms": 100, "phase_lr_deg": 2.5},
{"word": "to", "start_ms": 460, "dur_ms": 40, "phase_lr_deg": 2.5},
{"word": "the", "start_ms": 510, "dur_ms": 30, "phase_lr_deg": 2.5},
{"word": "class.", "start_ms": 550, "dur_ms": 26, "phase_lr_deg": 2.5}
]
},
{
"segment": 6,
"transcript": "자 이제 한국어 단어를 하나씩 배워볼까요? 준비되셨나요?",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "자", "start_ms": 80, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 250, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "한국어", "start_ms": 470, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "단어를", "start_ms": 900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "하나씩", "start_ms": 1280, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "배워볼까요?", "start_ms": 1720, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "준비되셨나요?", "start_ms": 2400, "dur_ms": 3468, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "네 선생님 기대됩니다",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 35, "dur_ms": 100, "phase_lr_deg": -1.5},
{"word": "선생님", "start_ms": 145, "dur_ms": 200, "phase_lr_deg": -1.5},
{"word": "기대됩니다", "start_ms": 360, "dur_ms": 216, "phase_lr_deg": -1.5}
]
},
{
"segment": 8,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 9,
"transcript": "첫 번째 단어는 사과입니다 따라 읽어보세요 사과",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "첫", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "번째", "start_ms": 160, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "단어는", "start_ms": 320, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "사과입니다", "start_ms": 540, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "따라", "start_ms": 950, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "읽어보세요", "start_ms": 1170, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "사과", "start_ms": 1600, "dur_ms": 6428, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "사과 발음이 맞나요?",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "사과", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": -1.0},
{"word": "발음이", "start_ms": 260, "dur_ms": 180, "phase_lr_deg": -1.0},
{"word": "맞나요?", "start_ms": 460, "dur_ms": 116, "phase_lr_deg": -1.0}
]
},
{
"segment": 11,
"transcript": "네 완벽해요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 25, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "완벽해요", "start_ms": 140, "dur_ms": 436, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "두 번째 단어는 학교입니다 여러분은 학교에 가는 것을 좋아하나요?",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "두", "start_ms": 70, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "번째", "start_ms": 180, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "단어는", "start_ms": 350, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "학교입니다", "start_ms": 580, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "여러분은", "start_ms": 1100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "학교에", "start_ms": 1480, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "가는", "start_ms": 1800, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "것을", "start_ms": 1970, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "좋아하나요?", "start_ms": 2150, "dur_ms": 2098, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "네 친구들을 만날 수 있어서 좋아요",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": -1.0},
{"word": "친구들을", "start_ms": 130, "dur_ms": 200, "phase_lr_deg": -1.0},
{"word": "만날", "start_ms": 340, "dur_ms": 120, "phase_lr_deg": -1.0},
{"word": "수", "start_ms": 470, "dur_ms": 30, "phase_lr_deg": -1.0},
{"word": "있어서", "start_ms": 510, "dur_ms": 40, "phase_lr_deg": -1.0},
{"word": "좋아요", "start_ms": 560, "dur_ms": 16, "phase_lr_deg": -1.0}
]
},
{
"segment": 14,
"transcript": "그렇군요 아주 좋은 답변입니다 다음은 문장 연습을 해볼게요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "그렇군요", "start_ms": 60, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 580, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "답변입니다", "start_ms": 720, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "다음은", "start_ms": 1200, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "문장", "start_ms": 1480, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "연습을", "start_ms": 1700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "해볼게요", "start_ms": 2050, "dur_ms": 4254, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 16,
"transcript": "저는 오늘 아침에 빵을 먹었습니다",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 170, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "아침에", "start_ms": 265, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "빵을", "start_ms": 400, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "먹었습니다", "start_ms": 495, "dur_ms": 81, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "빵 말고 다른 것은 안 먹었나요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "빵", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "말고", "start_ms": 110, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "다른", "start_ms": 210, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "것은", "start_ms": 325, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 420, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "먹었나요?", "start_ms": 470, "dur_ms": 106, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "우유도 같이 마셨어요",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "우유도", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 195, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "마셨어요", "start_ms": 330, "dur_ms": 246, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "Good job! You are doing great.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Good", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 2.0},
{"word": "job!", "start_ms": 160, "dur_ms": 120, "phase_lr_deg": 2.0},
{"word": "You", "start_ms": 300, "dur_ms": 60, "phase_lr_deg": 2.0},
{"word": "are", "start_ms": 370, "dur_ms": 40, "phase_lr_deg": 2.0},
{"word": "doing", "start_ms": 420, "dur_ms": 100, "phase_lr_deg": 2.0},
{"word": "great.", "start_ms": 530, "dur_ms": 46, "phase_lr_deg": 2.0}
]
},
{
"segment": 20,
"transcript": "감사합니다 선생님 더 열심히 공부하겠습니다 오늘 수업은 언제 끝나나요?",
"speaker": "speaker_3",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 75,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 75, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "선생님", "start_ms": 450, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 720, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "열심히", "start_ms": 850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "공부하겠습니다", "start_ms": 1200, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 2000, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "수업은", "start_ms": 2300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "언제", "start_ms": 2700, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "끝나나요?", "start_ms": 3000, "dur_ms": 3408, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 22,
"transcript": "곧 끝날 거예요 마지막으로 숫자 읽기 연습을 해봅시다",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "곧", "start_ms": 65, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "끝날", "start_ms": 180, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 350, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "마지막으로", "start_ms": 550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "숫자", "start_ms": 950, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "읽기", "start_ms": 1170, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "연습을", "start_ms": 1380, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "해봅시다", "start_ms": 1650, "dur_ms": 4728, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "일 이 삼 사 오 육 칠 팔 구 십",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "일", "start_ms": 45, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 85, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "삼", "start_ms": 125, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "사", "start_ms": 175, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "오", "start_ms": 225, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "육", "start_ms": 275, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "칠", "start_ms": 325, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "팔", "start_ms": 375, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "구", "start_ms": 425, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "십", "start_ms": 475, "dur_ms": 101, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 25,
"transcript": "완벽합니다 이제 수업을 마칠게요 수고하셨습니다",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "완벽합니다", "start_ms": 55, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 320, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "수업을", "start_ms": 485, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "마칠게요", "start_ms": 700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "수고하셨습니다", "start_ms": 1050, "dur_ms": 4526, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "감사합니다 안녕히 계세요 선생님",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 30, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 295, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "계세요", "start_ms": 430, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "선생님", "start_ms": 550, "dur_ms": 26, "phase_lr_deg": 0.0}
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
"transcript": "See you next time. Goodbye!",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 45, "dur_ms": 80, "phase_lr_deg": 2.0},
{"word": "you", "start_ms": 135, "dur_ms": 60, "phase_lr_deg": 2.0},
{"word": "next", "start_ms": 210, "dur_ms": 100, "phase_lr_deg": 2.0},
{"word": "time.", "start_ms": 325, "dur_ms": 120, "phase_lr_deg": 2.0},
{"word": "Goodbye!", "start_ms": 460, "dur_ms": 116, "phase_lr_deg": 2.0}
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
"transcript": "다음에 또 만나요 안녕히 가세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 35, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 200, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 465, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 550, "dur_ms": 26, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 32,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 33,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 34,
"transcript": "마지막 확인 질문 하나 할게요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "마지막", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "확인", "start_ms": 210, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "질문", "start_ms": 350, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "하나", "start_ms": 485, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "할게요", "start_ms": 550, "dur_ms": 26, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 36,
"transcript": "무엇이 궁금하신가요?",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "무엇이", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "궁금하신가요?", "start_ms": 255, "dur_ms": 321, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "오늘 배운 단어 세 개만 말해 보세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "배운", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "단어", "start_ms": 260, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "세", "start_ms": 375, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "개만", "start_ms": 430, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "말해", "start_ms": 510, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "보세요", "start_ms": 560, "dur_ms": 16, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "사과 학교 빵입니다 맞죠?",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "사과", "start_ms": 30, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "학교", "start_ms": 165, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "빵입니다", "start_ms": 280, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "맞죠?", "start_ms": 475, "dur_ms": 101, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "네 아주 정확해요 이제 진짜 마칠게요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 35, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 125, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "정확해요", "start_ms": 240, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 445, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 530, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "마칠게요", "start_ms": 575, "dur_ms": 1, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
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
"transcript": "그럼 진짜로 가보겠습니다",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "진짜로", "start_ms": 180, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "가보겠습니다", "start_ms": 400, "dur_ms": 176, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "네 안녕히 가세요 조심히 들어가세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 295, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "조심히", "start_ms": 410, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "들어가세요", "start_ms": 525, "dur_ms": 51, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 49,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 50,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-04 03:50:51*
