# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=25708  time=03:48:32  uptime=9796s  mem=739MB  system_processes=365"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260404_034748.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 10620ms (file: 106-132-261-267-263-263-671-613-136-613-103-137-317-371-173-736-762-763-732-723-713-713-763-173-107-107-672-762-763-761-173-126-231-210-310-760-143-123-124-123-173-761-120-620-7_W.mp3)
  Segment 2: 11620ms ~ 12196ms (file: 165-175-163-137-103-675-361-617-612-162-163-132-126-167-176-162-103-263-621-162-621-123-136-613-135-103-152-123-562-103-103-103-261-106-103-106-176-623-617-617-312-612-136 132-3_V.mp3)
  Segment 3: 13196ms ~ 13772ms (file: 167-751-705-706-654-654-602-623-326-236-162-751-675-103-163-613-507-576-574-756-170-750-107-106-103-547-134-756-567-657-627-612-627-312-301-301-302-756-765-567-623-320-362-306-3_W.mp3)
  Segment 4: 14772ms ~ 15348ms (file: 163-612-162-627-613-367-613-163-637-631-316 162-132-132-574-574-267-267-765-762-675-361-326-130-103-136-312-132-321-132-756-762-167-612-413-206-263-623-236-635-136-361-136-132-6_W.mp3)
  Segment 5: 16348ms ~ 16924ms (file: 546-651-375-625-136-156-167-103-163-167-316-103-103-103-301-132-103-315-132-103-107-137-316-132-106-137-103-136-103-132-103-576-316-106-361-103-132-312-103-132-103-130-367-136-1_V.mp3)
  Segment 6: 17924ms ~ 18500ms (file: 627-712-217-127-130-132-716-170-316-176-107-157-276-312-271-276-237-326-623-132-126-126-213-275-127-312-123-132-136-126-132-103-310-130-132-312-361-301-267-713-312-216-102 627-6_W.mp3)
  Segment 7: 19500ms ~ 20076ms (file: 567-652-517-657-657-167-675-615-657-657-657-165-651-650-657-562-657-605-103-162-651-150-106-106-160-106-106-610-105-103-106-105-160-615-156-165-651-561-561-651-205-265-651-160-1_V.mp3)
  Segment 8: 21076ms ~ 21652ms (file: 756-751-751-765-762-106 157-163-126-213-216-236-621-612-136-163_W.mp3)
  Segment 9: 22652ms ~ 23228ms (file: 312-103-103-132-103-132-675-130-130-103-102-106-106-107-103-167-106-103-130-130-310-160-612-612-652-621-615-625-671-576-765-617-617-612-156-675-567-657-675-167 312-314-312-341-6_V.mp3)
  Segment 10: 24228ms ~ 24804ms (file: 756-756-765-123-103-765-765-760-756-753-260-267-621-201-207-201-201-132-123-126-136-123-132-123-162-164-134-104 174-267-236-203-361-136-613-106-136-132-271-705-750-735-726-106-1_V.mp3)
  Segment 11: 25804ms ~ 26380ms (file: 302-236-563-567-523-301-310-310-106-176-106-163-163-137-136-301-516-572-632-162-613-316-302-326-602-261-625-167-106-132-103-301-132-136-316-361-103-130-321-301-130-312-132-321-2_V.mp3)
  Segment 12: 27380ms ~ 27956ms (file: 157-517 571-570-257 267-137-137-132_W.mp3)
  Segment 13: 28956ms ~ 29532ms (file: 613 765-126-267-263-263-326-261-236-263-267-263-267-263-623-627-623-236-172-261-261-321-213-213-162-167-176-716-163-106-175-165-136-136-312-132-312-136-623-316-627-637-162-612-1_W.mp3)
  Segment 14: 30532ms ~ 31108ms (file: 152-612-750-756-713-130-310-312-132-135-607-316-526-723-302-302-302-302-326-136-123-163-317-312-735-731-316-176-210-102-126-132-576-756-750-705-321-103-103-106-136-261-261-237-1_V.mp3)
  Segment 15: 32108ms ~ 32684ms (file: 627-265-326-623-361-236-126-637-316-167-762-675-627-672-765-256-765-765-723-276-735-572-572-567-675-672-756-625-765-657-726-765-263-756-675-657-756-652-675-652-625-675-167-176-6_W.mp3)
  Segment 16: 33684ms ~ 34260ms (file: 756-576-574-537-352-352-325-326-321-326-231-321-237-235-263-265-251-215-521-521-256-251-524-253-524-523-254-254-257-325-523-567-526-321-213-231-623-627-672-623-257-213-216-123-1_W.mp3)
  Segment 17: 35260ms ~ 35836ms (file: 312-625-265-625-650-625-625-652-625-562-657-625_W.mp3)
  Segment 18: 36836ms ~ 37412ms (file: 103-163-617-163-675-613-176-162-106-357-175-607-167-651-607-621-621-362-260_V.mp3)
  Segment 19: 38412ms ~ 38988ms (file: 675-627-657-756-765-765-652-675_W.mp3)
  Segment 20: 39988ms ~ 40564ms (file: 231-312-237-235-213-217-236-632-623-623-265-625-625-213-231-253-312-213-261-321-325-213-216-326-123-351-135-213-231-213-126-231-213-352-312-231-213-657-651-650-675-615-623-623-6_W.mp3)
  Segment 21: 41564ms ~ 42140ms (file: 567-657-657-630-657-612-623-635-675-236-657-675-651-213-312-562-625-312-213-130-132-106-617-617-136-761-612-321-362-513-316-130-103-613-601-602-760-671-106-613-163-617-162-170-5_W.mp3)
  Segment 22: 43140ms ~ 43716ms (file: 670-637-361-312-206-203-207-251-527-675-765-371-362-367-637-360-631-213-231-216-176-672-316-236-321-167-176-316-216-321-312-136-172-172-312-320-312-531-351-351-362-653-562-362-3_W.mp3)
  Segment 23: 44716ms ~ 50980ms (file: 216-263-201-263-263-261-206-236-263-327-203-230-562-352-326-107-312-120-132-102-120-321-231-574-235-236-216-126-267-263-263-126-216-260-216-120-206-203-206-263-260-210-261-231-1_W.mp3)
  Segment 24: 51980ms ~ 52556ms (file: 163-540-512-152-216-261-265-216-621-562-561-156-165-651-657-657-615-167-163-160-163-260-126-256-215-265-236-162-312-132-321-125-215-265-265-132-361-312-236-132-132-132-315-125-1_W.mp3)
  Segment 25: 53556ms ~ 54132ms (file: 761-657-162-567-576-576-675-103-612-612-261-123-102-312-132-147-136-132-130-576-576-576-576-106-613-136-316-163-617-271-216-612-321-324-671-750-765-576-756-657-657-675-765-765-7_W.mp3)
  Segment 26: 55132ms ~ 55708ms (file: 621-132 267-103-130-167-130-102-261-236-632-130-316-275-257-275-103-126-316-312-263-160-136-136-103-213-516-103-510-517-160-103-162-106-136-165-103-612-126-106-107-103-257-571-1_V.mp3)
  Segment 27: 56708ms ~ 57284ms (file: 123-317-570-123-134-135-132-321-301-301-301-136-312-632-621-261-756-756-576-576-312-316-136-620-126-132-326-236-501-507-150-130-103-132-136-517-567-153-315-157-163-261-613-216-1_V.mp3)
  Segment 28: 58284ms ~ 58860ms (file: 317-136-103-136-137-137-137-316-361-136-103-132-615-316-176-136-136-361-201-163-163-167-136-617-136-617-156-130-316-312-103-136-103-567-312-132-103-135-567-263-267-620_V.mp3)
  Segment 29: 59860ms ~ 60436ms (file: 547-547-517-576-157-176-176-517-157-125-571-257-571-157-517-526-625-625-261-153-617-562-265-657-265-326-531-513-637-163-263-315-132-375-173-375-137-316-312-317-351-357-537-537-5_W.mp3)
  Segment 30: 61436ms ~ 73028ms (file: 103-230-302-236-203-132-120-672-267-267-310-620-672-674-201-201-203-261-267-132-206-126-267-137-137-310-231-206-123-306-627-267-267-120-761-617-276-301-103-103-260-260-672-261-2_W.mp3)
  Segment 31: 74028ms ~ 80688ms (file: 765-175-657-756-735-176-106-317-715-721-312-107-617-370-371-143-341-163-132-236-726-752-321-103-317-103-310-172-103-762-672-762-762-326-715-637-457-103-310-372-273-237-631-361-7_W.mp3)
  Segment 32: 81688ms ~ 87952ms (file: 260-163-263-263-612-321-136-316-162-216-216-216-130-106-102-172-137-132-103-137-723-137-175-172-175-132-352-327-372-152-176-107-607-176-537-263-203-526-572-276-263-236-326-120-1_V.mp3)
  Segment 33: 88952ms ~ 89528ms (file: 312-136-631-321-326-326-236-132-613-362-326-136-326-160-620-136-136-176-623-623-306-163-607-621-316-365-625-562-630-362-306-306-302-620-367-631-326-362-612-632-321-260-261-236-2_W.mp3)
  Segment 34: 90528ms ~ 96252ms (file: 253-102-263-201-261-263-263-260-263-263-527-102-265-236-216-102-102-572-361-106-260-203-523-527-652-157-157-572-527-257-251-102-201-230-302-231-320-326-235-231-231-263-206-620-2_W.mp3)
  Segment 35: 97252ms ~ 97828ms (file: 136-312-136-675-672-627-265-632-132-351-523-352-520-527-137-103-103-236-321-315-312-316-316-156-153-513-132-132-123-237-261-271-217-167-157-673-367-123-627-126-261-321-326-236-5_W.mp3)
  Segment 36: 98828ms ~ 99404ms (file: 160-167-617-167-167-176-574-576-576-756-725-765-576-756-765-756-756-754-753-756_W.mp3)
  Segment 37: 100404ms ~ 100980ms (file: 376-326-237-367-130-213-326-361-312-132-310-130-132-130-132-132-756-576-574-163-106-167-165-132-670-675-675-765-765-675-765-675-675-615-163-103-675-675-671-617-312-132-310-361-7_W.mp3)
  Segment 38: 101980ms ~ 102556ms (file: 627-623-625-625-265-265-261-621-625-625-652-625-625-265 620-621-621-621-623-621-261-261-612-623-263-621-623-621-265-625-625-652-625-657-652-620 623-263-260-621-620-625-621-265-6_W.mp3)
  Segment 39: 103556ms ~ 114680ms (file: 276-672-612-260-201-132-267-271-170-107-123-301-206-103-132-621-267-261-126-102-260-621-130-217-237-102-260-261-120-310-321-102-210-210-216-367-237-276-576-260-310-130-123-231-2_W.mp3)
  Segment 40: 115680ms ~ 116256ms (file: 637-163-236-362-216-261-623-621-621-621-621-623-213-132-312-316-361-132-632-321_W.mp3)
  Segment 41: 117256ms ~ 117832ms (file: 136-361-314-312 271-134-351-312-103-103-362-124-123-135-126-132-164-627-671-164-167-607-317-173 754-716-371-124-326-371-607-376-264-617-637-613-163-756-756-765-756-354-471-143 1_V.mp3)
  Segment 42: 118832ms ~ 119408ms (file: 576-567-567-516-156-652-657-652-657-623-765-726-326-261-216-261-163-176-617-276-216-162-271-617-271-210-123-163-710-217_W.mp3)
  Segment 43: 120408ms ~ 120984ms (file: 136-316-603-167-136-103-163-132-160-103-162-612 163-156-165-136-316-361-632-601-316-136-650-136-130-236-203-102-302-326-103-312-361-136-310-216-261-103-216-263-513-375-603-261-1_V.mp3)
  Segment 44: 121984ms ~ 122560ms (file: 607-670-675-675-672-367-327-326-325-326-236-236-326-265-362-361 217-231-627-265-267-572-507-672-627-567-574-567-657-365-362-670-760-762-762-162-671-613-127-132-316-670-675-607-6_V.mp3)
  Segment 45: 123560ms ~ 124136ms (file: 165-175-167-106-135-103-135-157-160-617-731-632-617-162-132-715-137-127-261-621-130-756-576-675_V.mp3)
  Segment 46: 125136ms ~ 134028ms (file: 263-327-235-321-572-230-130-532-521-257-120-126-256-205-235-523-250-201-162-573-157-125-231-257-123-120-213-325-574-123-257-257-572-230-257-321-103-132-257-103-123-302-573-231-1_W.mp3)
  Segment 47: 135028ms ~ 139528ms (file: 216-216-123-213-213-123-231-236-231-231-230-236-213-216-231-213-231-321-321-312-321-231-217-213-217-213-231-231-213-326-321-326-213-231-216-213-213-126-123-123-123-213-321-213-2_W.mp3)
  Segment 48: 140528ms ~ 141104ms (file: 632-623-627-263-265-263-236-263-263-263-236-362-236-635-613-612-627-675-627-623-362-362-617-631-163-612-627-362-657-657-567-576-567-756-675-657-546-625-362-716-136-671 751-675-7_W.mp3)
  Segment 49: 142104ms ~ 142680ms (file: 103-136-627-621-316-103-130-672-620-617-165 163-376-316-123-137-163-516-215-567-136-130-706-760-672-362-672-675-673-756-756 675-675-617-627-625-632-576-567-756-576-756-572-567-6_W.mp3)
  Segment 50: 143680ms ~ 144256ms (file: 126-106-106-136-132-675-132-103-136-123-132-365-176-216-162-361-136-153-103-562-162-216-173-267-617-160-103-103-621-167-132-562-653-136-172-136-162-621 102 123-106-132-613-103-1_V.mp3)
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
