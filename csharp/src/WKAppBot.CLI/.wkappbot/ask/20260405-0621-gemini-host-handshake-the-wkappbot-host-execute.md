# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=24268  time=06:20:41  uptime=18658s  mem=482MB  system_processes=355"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_061642.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 157-301-671-352-567-325-752-732-371-260-320-210-321-216-160-152-153-235-325-256-216-162-316-257-256-256-215-265-261-326-312 236-253-562_W.mp3)
  Segment 2: 1576ms ~ 12736ms (file: 574-132-321-231-763-726-257-130-103-267-627-265-123-130-627-627-310-632-130-326-312-236-103-261-130-623-236-715-621-263-136-613-267-267-627-263-236-107-132-376-321-362-732-260-2_W.mp3)
  Segment 3: 13736ms ~ 20288ms (file: 260-267-231-716-267-320-532-267-726-276-267-574-267-267-574-132-130-103-576-213-137-130-236-267-236-762-362-216-102-321-103-132-150-206-216-326-231-572-135-127-321-213-310-375-7_W.mp3)
  Segment 4: 21288ms ~ 26328ms (file: 765-120-126-361-321-236-321-765-265-672-276-726-107-130-316-762-765-237-132-132-276-270-213-275-631-367-256-576-132-321-631-153-632-102-517-176-176-103-103-170-132-127-615-103-1_W.mp3)
  Segment 5: 27328ms ~ 27904ms (file: 571-571-715-756-751-571-157-513-517-571-571-517-571 157-517-175-715-157-157-517-751-715-157-517-751-751-571-751-715-573-751-571-571-751-571-157-157-571-571-576-571-517-153-157-7_V.mp3)
  Segment 6: 28904ms ~ 29480ms (file: 312-137-132 167-762-172-751-157-156_W.mp3)
  Segment 7: 30480ms ~ 34547ms (file: 120-213-162-123-132-162-261-236-216-162-261-273-217-752-712-752-271-712-273-712-267-762-726-276-276-276-271-276-721-762-276-762-762-671-672-762-671-751-762-751-756-752-617-765-7_W.mp3)
  Segment 8: 35547ms ~ 45627ms (file: 237-725-132-360-203-263-123-302-267-260-230-765-267-236-120-627-123-321-103-310-763-130-276-230-276-762-207-236-267-103-321-103-123-213-230-167-102-276-236-230-123-726-301-230-7_W.mp3)
  Segment 9: 46627ms ~ 47203ms (file: 165-125-156-163-103-103-103-103 147-175-137-174-173-174-137-103-103-137-103-137 127-176-165-137-175-173-176-137-153-127-165-165-125-125-123-571-715-723-327-237-321-327-271-271-2_W.mp3)
  Segment 10: 48203ms ~ 48779ms (file: 167-103-627-176-163-136-106-105-123-625-162-162-163-601-261-302-632-103-103-106-103-610-106-162-605-162-263-206-203-206-201-621-216-261-362-602-160-631-216-621-612-674-654-671-1_V.mp3)
  Segment 11: 49779ms ~ 50355ms (file: 576-576-130-175-157-137-312-756-756-756-756-756-573-567-137-316-136-316-315-163-756-756-175-362-312-326-163-135-175-136-263-312-615-156-153-157-516-157-157-157-157-157-175-157-1_W.mp3)
  Segment 12: 51355ms ~ 51931ms (file: 165-163-263-216 602-621-610-175-126-675-106-162-176-162-623-621-615-627-623-602-672-617-103-103-167-761-361-256-135-671-627-126-312-170-103-132-103-613-103-735-675-736-612-267-6_V.mp3)
  Segment 13: 52931ms ~ 61247ms (file: 130-203-130-302-132-315-103-132-107-103-107-617-103-310-103-310-103-215-523-253-102-150-123-570-517-156-152-154-103-102-213-203-132-103-103-103-130-137-123-132-132-134-610-103-1_V.mp3)
  Segment 14: 62247ms ~ 62823ms (file: 135-135-173-173-137-135-315-135-312-312-132-312-132 576-736-576-573-375-735-175-103-135-715-135-537-175-173-137-137-127-543-315-135-315-315-312-132-312-132-312-132-302-576-753-7_V.mp3)
  Segment 15: 63823ms ~ 64399ms (file: 132-317-312-371-176-231-132-173-137-132-315-130-137-173-137-236-267-756-675-163-570-573-276-765-527-136-273-173-625-265-657-657-562-621-672-765-675-751_V.mp3)
  Segment 16: 65399ms ~ 65975ms (file: 103-301-127-176-167-167-103-125-103-127-167-167-617-163-127-130-162-617-126-103-673-167-621-103-610-107-263-216-130-167 106-167-761-126-657-165-106-651-162-106-106-561-627-167-1_V.mp3)
  Segment 17: 66975ms ~ 67551ms (file: 236-326-276-267-672-705-215-325-237-623-507-765-576-526-267-127-352-725-623-236-312-605_V.mp3)
  Segment 18: 68551ms ~ 69127ms (file: 265-132-613 721-152 523 576 627-625-547 501-315-132-132-310-362-367-376-132-136-137-327-301-302-301-310-716-317-137-173-137-136-356-516-617-176-167-167-361-163-631-610-651-612-1_W.mp3)
  Segment 19: 70127ms ~ 77219ms (file: 123-132-231-213-203-132-230-231-231-762-267-312-130-103-103-237-213-237-231-126-163-263-276-762-216-267-130-231-132-230-261-261-261-726-261-326-236-132-267-326-237-236-213-276-2_W.mp3)
  Segment 20: 78219ms ~ 78795ms (file: 617-621-672-263-260-106-103-106-136-106-601-615-106-136-172-165-615-106-617-617-106-167-312-126-176-163-106-136-106-103-263-637-613-163-106-106-612-165-261-176-130-105-175-761-6_V.mp3)
  Segment 21: 79795ms ~ 80371ms (file: 574 672-673-675 765-765-765-765-763-631 160-610-162-260-205 132-120-672-627-621-126-217-231-213-132-361-673-361-361-657-627-562-572-265-271-237-132-163-631-316-236-132-316-123-2_W.mp3)
  Segment 22: 81371ms ~ 81947ms (file: 137-123 130-612-163-675-621-106-106_V.mp3)
  Segment 23: 82947ms ~ 92091ms (file: 130-623-263-216-321-230-231-725-726-237-132-273-312-237-726-206-276-120-320-726-321-310-210-725-321-230-172-103-102-371-725-237-276-102-321-671-761-130-103-762-207-102-132-756-2_W.mp3)
  Segment 24: 93091ms ~ 103999ms (file: 236-102-321-136-320-326-203-263-267-326-321-136-103-275-273-102-302-302-203-132-301-310-130-310-123-216-260-163-102-103-136-320-321-230-206-132-103-103-132-103-103-302-230 132 1_V.mp3)
  Segment 25: 104999ms ~ 116087ms (file: 136-273-237-231-275-274-231-130 235-201-310-321-102-276-627-672-627-672-201-130-367-167-263-754-375-365-260-623-627-627-326-302-326-261-326-326-362-350-365-136-321-135-325-560-5_W.mp3)
  Segment 26: 117087ms ~ 127563ms (file: 267-237-263-237-574-547-762-276-712-716-210-102-106-137-301-731-213-210-231-571-576-675-732-756-756-276-267-230-230-231-273-237-263-273-237-320-203-327-206-130-237-327-327-756-2_W.mp3)
  Segment 27: 128563ms ~ 135043ms (file: 276-213-276-271-276-257-123-217-216-216-267-216-217-675-132-216-273-217-217-275-271-576-257-270-207-231-132-231-213-276-237-275-273-274-237-321-123-231-235-123-123-231-274-273-2_W.mp3)
  Segment 28: 136043ms ~ 136619ms (file: 765-703-316-136-316-361 167-367-613-361-136-317-137-136-137-167-176-731-675-675-657-675-567-365-675-675-765-765-736-675-765_W.mp3)
  Segment 29: 137619ms ~ 138195ms (file: 132-372-716-621-671-617-672-672-316-167-136-317-312-317-317-317-371-371-317-367-362-762-540-532-315-153-541-751-541-513-326-321-534-562-315-312-672-675-673-526-236-231-126-261-5_W.mp3)
  Segment 30: 139195ms ~ 146539ms (file: 672-675-657-320-571-576-130-103-267-756-672-567-103-276-320-327-765-756-276-103-123-723-302-756-543-354-547-271-210-726-754-572-273-276-276-270-574-310-107-517-321-574-756-715-3_W.mp3)
  Segment 31: 147539ms ~ 148115ms (file: 510-517-571-715-510-150-501-570-510-750-571 501-501-517-150-517-507-571-501-510-150-571-150-570-501-501-517-510-517-150-571 510-501-501-517-510-501-510-501-510-570-571-570-507-5_W.mp3)
  Segment 32: 149115ms ~ 149691ms (file: 765-137-165-103-136-316-136-317-130-136-137-613-136-671-376-103-103-134-316-314-635-136-356-123-136-310-317-163-351-536-613-236-216-213-261-216-126-213-126-213-263-612-162-236-6_W.mp3)
  Segment 33: 150691ms ~ 151267ms (file: 715 675-672-672-167-176-261-127-263-312-623-120-231 162-362-102-132-136-136-621-167-160-621-167-162-103-253-302-160-103-106-165-612-103-106-547-163-612-162-136-162-602-632-620-1_V.mp3)
  Segment 34: 152267ms ~ 162311ms (file: 763-320-321-137-613-316-103-310 574-130-276-276-163-213-213-172-261-721-176-721-230-260-102-320-136-541-513-321-102-130-713-103-176-132-107-167-162-610-612-673-132-102-370-716-1_V.mp3)
  Segment 35: 163311ms ~ 172239ms (file: 236-574-231-107-751-756-216-103-103-267-236-132 573-576-675-103-231-267-263-573-107-105-213-263-106-263-216-261-375-137-267-256-576-107-571-132-567-275-256-236-106-756-756-130-2_W.mp3)
  Segment 36: 173239ms ~ 173815ms (file: 103-154-415-104-103 354-345-304-134-314-143-415-154-145-405-451-104-143-145-104-401-431-315-143-105-103-134-134-314 317-140-140-401-140-310-143-415-135-415-150-104-140-105-145-1_V.mp3)
  Segment 37: 174815ms ~ 184787ms (file: 547-102-257-213-123-201-213-132-103-256-123-132-216-260-260-261-102-672-267-203-137-173-103-134-201-260-572-215-206-574-257-206-102-103-672-206-216-760-206-670-627-627-627-257-3_W.mp3)
  Segment 38: 185787ms ~ 186363ms (file: 321 136-276-326-213 574-612-326-312-567-536-601-136-312-136-316-136-260-620-570-506-120-321-132-321-156-165-617-617-631-136-752-756-356-752-357-576-321-632-316-627-325-321-320-3_W.mp3)
  Segment 39: 187363ms ~ 187939ms (file: 263-260-260-260-621-302-261-621-326-236-236-162-321-123-630-316-106-103-203-620-620-602-621-602-103-751-756-570-760-672-260-765-751-713-130-103-103-310-102-312-132-310-260-213-2_V.mp3)
  Segment 40: 188939ms ~ 196247ms (file: 576-574-276-267-627-213-765-267-267-267-267-261-263-263-716-130-316-265-572-321-756-572-576-527-213-132-261-231-103-261-216-132-261-103-723-576-312-216-130-263-265-265-216-261-2_W.mp3)
  Segment 41: 197247ms ~ 197823ms (file: 750-572-251-672-321-270-276-257-750-265-263-527-325-537-756-756-570-175-312-351-317-736-735-617-735-217-257-576 576-562-567-203-326-526-702-570-365-276-726-362-572-657-572-275-5_V.mp3)
  Segment 42: 198823ms ~ 199399ms (file: 612-136-613-123-256-167-102-317-236-610-132-103-123-106-106-623-103-312-610-623-675-615-657-657-312_V.mp3)
  Segment 43: 200399ms ~ 207743ms (file: 672-620-213-675-251-206-762-527-126-210-260-627-310-572-130-312-527-206-257-671-103-103-206-602-201-320-250-542-206-310-301-213-216-123-206-312-627-670-236-230-120-260-103-602-1_V.mp3)
  Segment 44: 208743ms ~ 209319ms (file: 602-620-621-261-621-621-625-625-265-261-261-620-261-261-261-620-265-652-625-625-652-621-625-615-623-621-621-621-620-620-621-260-625-625-261-265-265-261-263-623-621-261-623-625-6_W.mp3)
  Segment 45: 210319ms ~ 218203ms (file: 326-302-270-130-271-735-321-321-130-310-567-130-103-103-273-276-762-276-236-213-316-267-276-267-275-672-765-103-106-136-267-132-762-327-230-321-103-310-276-231-162-326-320-213-2_W.mp3)
  Segment 46: 219203ms ~ 219779ms (file: 576-571-571-517-157-561-751-751-571-175-156-751-751-756-756-756-756-756-756-751-751-756-756-756-756-756-715-105-150-150-175-170-156-756-157-756-175-715-761-675-175-761-756-756-7_W.mp3)
  Segment 47: 220779ms ~ 221355ms (file: 627-571-257-702-321-237-752-275-263-507-570-572-231-213-607-273-715-362-316-312-316-137-317-173-137-173-137-137-167-176-167-137-173-176-173-176-173-312-130-310-130-130-173-310-1_W.mp3)
  Segment 48: 222355ms ~ 222931ms (file: 762-672-713-137-762-612-612-612-671-765-675-765-716-573-576-573-526 527 547-567-256-571-123-657-657-567-750-560-513-213-153-135-756-671-362-326-637-576-576-756-576-567-762-157-1_W.mp3)
  Segment 49: 223931ms ~ 224507ms (file: 501-501-517-510-150-510-150-501-517-510-105-150-105-510-150-510-157-501-571-510 507-570-705-501-570-510-570-510-507-501-510-501-510-501-501-507-510-570-510-570-570_V.mp3)
  Segment 50: 225507ms ~ 226083ms (file: 573-653-326-163-653-725-367-617-513-536-376-137-623-631-627-632-267-263-657-657-657-576-130-357-537-567-576-657-567-315-573-567-576-572-567-571-547-547-572-317-513-576-157-560-5_W.mp3)
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
> **Time**: 2026-04-05 06:21:53
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
"transcript": "안녕하세요 오늘 날씨가 참 좋네요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 150, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 1450, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 2350, "dur_ms": 950, "phase_lr_deg": 0.0},
{"word": "참", "start_ms": 3400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 4000, "dur_ms": 1100, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "맛있는 점심 식사 하셨나요 저는 김치찌개를 먹었습니다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "맛있는", "start_ms": 120, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "점심", "start_ms": 1100, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "식사", "start_ms": 1900, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "하셨나요", "start_ms": 2800, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "저는", "start_ms": 4000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "김치찌개를", "start_ms": 4700, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "먹었습니다", "start_ms": 6000, "dur_ms": 1000, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "Next lesson will be about grammar rules.",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "Next", "start_ms": 200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "lesson", "start_ms": 650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "will", "start_ms": 1200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "be", "start_ms": 1550, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "about", "start_ms": 1850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "grammar", "start_ms": 2400, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "rules.", "start_ms": 3100, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 6,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 7,
"transcript": "오늘 공부할 내용은 동사의 활용입니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 180, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "공부할", "start_ms": 850, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "내용은", "start_ms": 1700, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "동사의", "start_ms": 2600, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "활용입니다", "start_ms": 3400, "dur_ms": 900, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "어제 배운 단어들을 복습해 보겠습니다 기억나시나요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "어제", "start_ms": 140, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "배운", "start_ms": 750, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "단어들을", "start_ms": 1450, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "복습해", "start_ms": 2450, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "보겠습니다", "start_ms": 3400, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "기억나시나요", "start_ms": 4600, "dur_ms": 1200, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 12,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 13,
"transcript": "자 이제 따라 읽어보세요 사과 포도 수박",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "자", "start_ms": 100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "따라", "start_ms": 1100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "읽어보세요", "start_ms": 1800, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "사과", "start_ms": 4000, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "포도", "start_ms": 5500, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "수박", "start_ms": 7000, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 19,
"transcript": "질문 있으신 분은 손을 들어 주세요 아니면 채팅창에 남겨주세요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "질문", "start_ms": 150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "있으신", "start_ms": 700, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "분은", "start_ms": 1400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "손을", "start_ms": 2000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "들어", "start_ms": 2600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 3100, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "아니면", "start_ms": 4000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "채팅창에", "start_ms": 4700, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "남겨주세요", "start_ms": 5700, "dur_ms": 1000, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "오늘 수업은 여기까지입니다 모두 수고하셨습니다 다음 시간에 만나요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 120, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "수업은", "start_ms": 700, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "여기까지입니다", "start_ms": 1500, "dur_ms": 1300, "phase_lr_deg": 0.0},
{"word": "모두", "start_ms": 3000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "수고하셨습니다", "start_ms": 3700, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 5200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "시간에", "start_ms": 5800, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 6600, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "주말 잘 보내시고 맛있는 거 많이 드세요 감기 조심하시고요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "주말", "start_ms": 140, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "보내시고", "start_ms": 1100, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "맛있는", "start_ms": 2200, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 3000, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 3300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "드세요", "start_ms": 3900, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "감기", "start_ms": 5000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "조심하시고요", "start_ms": 5600, "dur_ms": 1200, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "Please make sure to complete the assignment by Friday evening.",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "Please", "start_ms": 180, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "make", "start_ms": 650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "sure", "start_ms": 1000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 1400, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "complete", "start_ms": 1650, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 2300, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "assignment", "start_ms": 2550, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "by", "start_ms": 3500, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "Friday", "start_ms": 3800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "evening.", "start_ms": 4500, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "숙제는 이메일로 보내주시면 됩니다 질문은 언제든지 환영합니다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "숙제는", "start_ms": 130, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "이메일로", "start_ms": 900, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "보내주시면", "start_ms": 1900, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "됩니다", "start_ms": 3000, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "질문은", "start_ms": 4000, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "언제든지", "start_ms": 4800, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "환영합니다", "start_ms": 5800, "dur_ms": 1100, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "감사합니다 그럼 이만 마치겠습니다 안녕히 가세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 100, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "그럼", "start_ms": 1200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이만", "start_ms": 1800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "마치겠습니다", "start_ms": 2400, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 3800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 4500, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "잠시만요 공지사항 하나 더 말씀드릴게요 내일은 휴강입니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "잠시만요", "start_ms": 150, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "공지사항", "start_ms": 1100, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "하나", "start_ms": 2100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 2700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "말씀드릴게요", "start_ms": 3100, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "내일은", "start_ms": 4500, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "휴강입니다", "start_ms": 5300, "dur_ms": 1000, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 32,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "착오 없으시기 바랍니다 다시 한번 말씀드리지만 내일은 수업이 없어요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "착오", "start_ms": 120, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "없으시기", "start_ms": 700, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "바랍니다", "start_ms": 1600, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 2800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "한번", "start_ms": 3300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "말씀드리지만", "start_ms": 3800, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "내일은", "start_ms": 5000, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "수업이", "start_ms": 5800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "없어요", "start_ms": 6500, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "건강 관리 잘 하시고 수요일에 뵙겠습니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "건강", "start_ms": 110, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "관리", "start_ms": 700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 1300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "하시고", "start_ms": 1700, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "수요일에", "start_ms": 2800, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "뵙겠습니다", "start_ms": 3800, "dur_ms": 1100, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 37,
"transcript": "See you all on Wednesday. Have a great day!",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 550, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "all", "start_ms": 800, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "on", "start_ms": 1100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "Wednesday.", "start_ms": 1350, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "Have", "start_ms": 2400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 2750, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "great", "start_ms": 2950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "day!", "start_ms": 3400, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 39,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 40,
"transcript": "아 그리고 잊지 마세요 단어 시험 결과는 내일 문자로 발송됩니다",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 160,
"audio_type": "speech",
"words": [
{"word": "아", "start_ms": 160, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 600, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "잊지", "start_ms": 1300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "마세요", "start_ms": 1800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "단어", "start_ms": 2600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "시험", "start_ms": 3200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "결과는", "start_ms": 3800, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "내일", "start_ms": 4600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "문자로", "start_ms": 5200, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "발송됩니다", "start_ms": 6000, "dur_ms": 1100, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 42,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 43,
"transcript": "확인해보시고 틀린 단어는 세 번씩 써서 제출해주세요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "확인해보시고", "start_ms": 140, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "틀린", "start_ms": 1500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "단어는", "start_ms": 2100, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "세", "start_ms": 2900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "번씩", "start_ms": 3300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "써서", "start_ms": 3900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "제출해주세요", "start_ms": 4500, "dur_ms": 1100, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 45,
"transcript": "그럼 진짜로 마치겠습니다 다들 안녕",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "진짜로", "start_ms": 700, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "마치겠습니다", "start_ms": 1500, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "다들", "start_ms": 2800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "안녕", "start_ms": 3400, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 47,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 48,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 49,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
*Generated by WKAppBot ask gemini — 2026-04-05 06:21:53*
