# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43304  time=05:42:38  uptime=11946s  mem=1107MB  system_processes=437"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260331_054228.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 762-765-672-673-672-623-627-672-267-612-216-621-263-326-237-672-627-762-672-627-276-762-276-672-675-671-672-627-627-263-312-327-371-317-317-326-672-632-627-627-627-261-627-617-6_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 106-516-576-162-163-162-163-106-106-103-167-316-651-165-756-635-103-156-126-132-103-126-130-136-143-103-163-103-163-613-326-263-160-512-316-235_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 231-312-135-231-126-261-263-261-261-263-231-231-534-315-325-321-321-315-265-326-321-326-541-263-326-261-263-231-261-263-236-312-326-362-265-526-532-312-321-261-524-253-236-321-2_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 132-136-163-612-132-316-617-613-317-316-631-162-106-136-315-612-156-567-260-125-136-162-267-765 312-132-765-675-675-675-675 130-136-756-756-756-756-316-361-312-312-310_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 570-167-162-123-612-671-167-361-316-351-135-132-765-672-163-312-631-613-567-635-163-621-671-617-167-167-162-126-160-362-106 136-574-547-574-756-765-136-103-613-236-312-130-302-1_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 321-321-132-312-312-376-327-321-213-236-236-231-235-210-325-123-321-213-265-216-236-215-231-321-132-312-356-567-725-267-723-732-723-726-375-573-576-576-675-657-657-362-326-312-3_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 523-651-261-623-261-256-123-130-103-612-103-610-123-612-321-163-576-576-574-754-576-571-574-756-253-623-215-150-231-162-632-267-623-632_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 263-326-316-564-356-231-236 261-621-326-261-231-612-563-310-231-263-261-623-231-321-126-312-216 267-326-236 612 567-567-320-321-162-103-610-627-267-267-621-236 321-263-326-132-1_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 574-574-504 576-576-567-650-651-657-310-130-316-576-621-236-162-162-632-627-612-372-160-230-163 176 361-623-316-361-312-671-167-126-756-752-765-716-765-765-765-765-765-716-761-7_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 623-253-253-527-235-365-276-326-263-362-362-253-573-157-351-315-317-372-263-532-572-537-153-351-531-571-573-571-521-157-573-571-537-537-357-735-571-517-573-157-137-517-571-753-3_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 612-675-106-671-167-165-106-152-163-623-163-563-621-136-136-361-103-136-716-167-163-126-103-106-136-163-167-201-167 160-106-106-316-106-316-136-106-125-162-261-136-106-621-176-1_V.mp3)
  Segment 12: 17336ms ~ 23348ms (file: 132-316-365-136-621-167-675-672-312-623 627_W_stt_the_the.mp3)
  Segment 13: 24348ms ~ 24924ms (file: 756-765-756-576-675-263-612-657-601-715-162-621-301-261-163-106-731-312-762-627-612-263-620-627-623-103-162-106-106-167-160-103-106-316-621-126-137-216-715-263 321-123-126 612-6_V.mp3)
  Segment 14: 25924ms ~ 26500ms (file: 612-613-617-567-756-675-267-632-613-162-163-132-132-132-675-576-260-621-106-132-102-321-123 203-620-260-260-623-263-160-106-106-170-765-762-765-672 751-756-267-762-675-673-127-7_W.mp3)
  Segment 15: 27500ms ~ 28076ms (file: 531-531-513-532-536-531-351-513-513-153-135-531-351-513-517-315-135-517-537-537-531-576-571-573-531-513-517-517-517-531-153-157-175-531-351-137-135-157-137-153-135-153-135-132-1_W.mp3)
  Segment 16: 29076ms ~ 29652ms (file: 231-231-321-213-213-231-326-362-321-375-236-231-321-236-231-321-236-326-326-351-321-235-231-231-361-316-236-326-261-361-312-213-235-213-213-261-627-267-256-236-265-213-231-326-3_W.mp3)
  Segment 17: 30652ms ~ 31228ms (file: 625-267-675-621-267-752-573-765-756-576-573-536-537-576-576-576-361-160-136-136-106-162-671-613-671_W.mp3)
  Segment 18: 32228ms ~ 32804ms (file: 567-765-315-536-162-627-617-671-623 762-627-675-263-261-216 673-627 637-123-167-167-672-326-612-362-627-326 321-150-105-617-627 132 103-236-236-326-326-326-326-236-321-236-162-1_W.mp3)
  Segment 19: 33804ms ~ 34380ms (file: 265-265-256-256-527-267-263-263-265-562-526-265-265-256-256-326-326-321-321-231-231-523-532-573-574-536-326-312-563-527-352-325-352-237-263-263-236-632-260-265-235-362-635-136-1_W.mp3)
  Segment 20: 35380ms ~ 35956ms (file: 612-162-627-623-163-316-176-103-106-105-167-106-613-175-167-130-173-106 716-516-716-162-136-617-103-136-160-316-163-103-613-163-163-106-106-106-103-132-132-613-136-735-103-617-1_V.mp3)
  Segment 21: 36956ms ~ 37532ms (file: 527-132-317-316-312-216-360-263-236-217-237-263-231-130-325-236-312-213-123-213-123-267-216-301-251-562-625-125-271-127-723-172-721-217-317-317-137-573-157-562-257-572-537-572-5_W.mp3)
  Segment 22: 38532ms ~ 39108ms (file: 263-263-261-267-267-261-627-267-261-265-265-261-265-625-263-256-263-263-263-236-263-263-261-236-263-621-267-623-621-261-623-623-267-263-263-263-326-261-623-263-265-267-623-263-2_W.mp3)
  Segment 23: 40108ms ~ 40684ms (file: 237-267-603-756-751-765-756-726-726-237-756-372-372-362-375-573-326-623-672-672-216-627-326-327-263-625-623-236-253-752-167-167-321-321-326-236-321-267-267-627-267-327-543-547-5_W.mp3)
  Segment 24: 41684ms ~ 42260ms (file: 756-765-756-175-574-576-571-763-765 361-123-136-567-651-675-756_W.mp3)
  Segment 25: 43260ms ~ 43836ms (file: 136-612-620-326-621-671-506-571-103-103-103-106-260-261-657-162-267-236-360-163-617-236-673-176-623-123-763-561-601-362-756-765-672-672-167-107-617-163-163-162-627-672-627-507-5_W.mp3)
  Segment 26: 44836ms ~ 45412ms (file: 621-623-621-361-136-312-326-361-713-132-671-176-567-156-576-546-765-765-657-657-675-576-107 213-612-631-162-612-136-167-136-163-625-623-163 167-617-623-613-617-615-617-671-361-1_W.mp3)
  Segment 27: 46412ms ~ 46988ms (file: 675-672-602-675-765-263-263-157 326-635-672-367-650-567-756-273-607-527-567-576_V.mp3)
  Segment 28: 47988ms ~ 51768ms (file: 132-132-103-132-163-631-123-123-136-174-132-312-103-132 132-123-576_V.mp3)
  Segment 29: 52768ms ~ 53344ms (file: 263-130-320-132-301 206-203-261-320-230-127 321-123-263-316 301-310 625-267-627-632-627-765-752-765-162-102-123-120-130-123-261-627-132-620-301-301-162-317-267-627-627-672-627-1_V.mp3)
  Segment 30: 54344ms ~ 54920ms (file: 265-571-175-571-536-375-357-163-163-137-567-675-362-631-546-536-526-524-532-132-263-523-625-265-564-526-162-625-627-276-276-625-361-356-532-523-532-543-235-231-321-541-256-256-5_W.mp3)
  Segment 31: 55920ms ~ 56496ms (file: 165-167-163-126-526-361-615-126-103-126-650-163-617-136-316-362-103-316-316-163-136-572-236-263_V.mp3)
  Segment 32: 57496ms ~ 58072ms (file: 610-106-617-167-137-172-162-162-627-621-706-675-576-657-567-674-671-103 263-236-263-263-132-102-130-715-175-703-270-160-123-136-765-167-601-163-163-623-163-163-321-632-267-103-7_W.mp3)
  Segment 33: 59072ms ~ 59648ms (file: 167-126-162-621-612-260-216-623-617-617-163-130-167-167-136-567-160-623-126-613-130-163-361-671-165-617-106-571-316-617-162-312-176-162-761-106 610-615-106-137-103-216-521-260 1_V.mp3)
  Segment 34: 60648ms ~ 61224ms (file: 316-162-362-610-201-130-301-260-132-106-620-207-761-326-306-263-617-136-103-130-516-513-613-136-632-123-103-132-137-156-132-362-106-162-136-163-312-312-532-670-625-675-612-123-1_V.mp3)
  Segment 35: 62224ms ~ 62800ms (file: 657-675-765-675-765-576-756-765-765-756-576-750-576-756-576-750-756-574-574-571-571-573-576-576-567-576-576-574-547-576-657-756-576-756-756-576-756-756-576-576-675-756-567-574-7_W.mp3)
  Segment 36: 63800ms ~ 64376ms (file: 301-312-362-306-302-260-260-326-320-320-302-103-136-310-306-632-623-362-132-302-321-301-130-632-102-106-137-103-571-167-617-671-617-617-617-671-607-607-610-617-601-601-106-137-1_V.mp3)
  Segment 37: 65376ms ~ 65952ms (file: 125-265-613-136-612-671-613-603-136-167-160-162-106-127-617-136-162-620-621-627-160-601-316-263-126-126-236-162-261-163-270-621-167-106-130-136-765-176-263-621-126-162-613-167-1_V.mp3)
  Segment 38: 66952ms ~ 67528ms (file: 321-315-357-357-375-350-537-521-267-627-653-256-625-675-127-163-513-157-156-563-567-352-213-263-321-126-326-316-516-351-531-351-365-350-310-316-367-325-362-623-376-637-321-361-3_W.mp3)
  Segment 39: 68528ms ~ 69104ms (file: 506-263-120-261-321-263-103-136-132-163-617-136-132-617-162-361-637-631-312-603-362-216-326-623-132-316-163-671-326-320-316-312-236-136-316-612-123-213-312-301-103-316-126-130-1_V.mp3)
  Segment 40: 70104ms ~ 70680ms (file: 623-261-263-267-261-623-263-263-623-263-621-621-217-623-627-267-627-627-267-627-276-276-267-267-267-276-267-267-267-726-267-273-276-726-236-237-267-627-267-276-267-267-267-263-2_W.mp3)
  Segment 41: 71680ms ~ 72256ms (file: 215-135-132-135-315-135-512-152-213-135-315-325-165-351-153-352-153-213-231-263-216-315-315-315-351-316-312-136-132-523-526-523-524-254-354-534-537-357-513-531-576-635-653-635-6_W.mp3)
  Segment 42: 73256ms ~ 73832ms (file: 673-762-637 671-637-625-163-126-132-123-162-126-132-716-672-726-237-632-267-160-672-672-672-617-671-672-756-752-762-672-672 213-627-672-671-672-632-623-263-267-276-267-326_W.mp3)
  Segment 43: 74832ms ~ 75408ms (file: 312-567-513-517-103-137-105-160-531-137-126-613-126-160-163-671-106-153-106-162-617-601-103-106-130-136-612-163-163-623 103-106-167-137-317-612-601-263-761-106-163-167-612-725-7_V.mp3)
  Segment 44: 76408ms ~ 76984ms (file: 137-137-136-162-310-301-316-132-321-105-201-132-130-103-103-103-103-103-137-750-173-107-143-156-157-156-165-162-173-103-143-260-260-260-105-170-107-107-126-201-216-106-126-162-1_V.mp3)
  Segment 45: 77984ms ~ 78560ms (file: 563-106-136-170-670-160-163-316-106-107-136-106-132-137-126-130-136-541-165-165-136-106-103-170-163-136-130-136-613-136-716-376-167-760-136-576-165-132-132-316-765-302-763-376-6_V.mp3)
  Segment 46: 79560ms ~ 80136ms (file: 317-316-312-267-267-276-267-267-276-273-276-267-267-236-260-267-236-267-256-523-526-256-257-256-265-576-572-257-251-263-231-216-126-253-352-321-327-523-326-316-135-312-132-316-2_W.mp3)
  Segment 47: 81136ms ~ 81712ms (file: 536-321 263-623-267-261-310-236-326-123-216-601-612-136-216-362-163-126-126-132-271-321-321 623-627-632 672-762-752-352-312-627-625-672-326-321-261-132-361-623-267-321-316-132-3_W.mp3)
  Segment 48: 82712ms ~ 83288ms (file: 263-623-623-532-165-561-571-537-257-625-627-362-361-362-653-652-651-540-574-325-236-263-623-263-263-562-267-236-260-326-623-632-623-163-236-263-236-236-216-612-162-216-621-620-2_W.mp3)
  Segment 49: 84288ms ~ 84864ms (file: 137-106-163-136-301-106-163-103-617-316-136-163-167-163-106-670-163-163-725-103-160-173-627-621-621_V.mp3)
  Segment 50: 85864ms ~ 86440ms (file: 754-765-320-320-103-310-751-576-576-106-167-106-106-201-517-160-106-310-312-627-621-163-612-613-106-106-106-106-162-126-312-321-620-623-623-260-326-362-603-136-107-760-671-154-1_V.mp3)
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
> **Time**: 2026-03-31 05:43:27
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "오늘 점심 메뉴는 정하셨나요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "점심", "start_ms": 165, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "메뉴는", "start_ms": 295, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "정하셨나요?", "start_ms": 435, "dur_ms": 141, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "비빔밥 어때요? 건강에도 좋잖아요.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "비빔밥", "start_ms": 30, "dur_ms": 180, "phase_lr_deg": -5.2},
{"word": "어때요?", "start_ms": 220, "dur_ms": 110, "phase_lr_deg": -5.0},
{"word": "건강에도", "start_ms": 340, "dur_ms": 120, "phase_lr_deg": -5.3},
{"word": "좋잖아요.", "start_ms": 470, "dur_ms": 106, "phase_lr_deg": -5.2}
]
},
{
"segment": 3,
"transcript": "좋은 생각이에요! 저도 찬성입니다.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 60, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "생각이에요!", "start_ms": 180, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "저도", "start_ms": 370, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "찬성입니다.", "start_ms": 460, "dur_ms": 116, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "I'll lead the way to a good restaurant.",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "I'll", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": -4.8},
{"word": "lead", "start_ms": 130, "dur_ms": 110, "phase_lr_deg": -4.9},
{"word": "the", "start_ms": 250, "dur_ms": 60, "phase_lr_deg": -4.7},
{"word": "way", "start_ms": 320, "dur_ms": 110, "phase_lr_deg": -4.8},
{"word": "to", "start_ms": 440, "dur_ms": 60, "phase_lr_deg": -4.9},
{"word": "a", "start_ms": 510, "dur_ms": 20, "phase_lr_deg": -4.7},
{"word": "good", "start_ms": 530, "dur_ms": 46, "phase_lr_deg": -4.8}
]
},
{
"segment": 5,
"transcript": "여기 정말 사람이 많네요. 맛집인가 봐요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "여기", "start_ms": 50, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 150, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "사람이", "start_ms": 270, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "많네요.", "start_ms": 390, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "맛집인가", "start_ms": 510, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "봐요.", "start_ms": 560, "dur_ms": 16, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "재료가 신선해서 항상 붐비는 곳이에요.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "재료가", "start_ms": 35, "dur_ms": 130, "phase_lr_deg": -5.0},
{"word": "신선해서", "start_ms": 175, "dur_ms": 140, "phase_lr_deg": -5.1},
{"word": "항상", "start_ms": 325, "dur_ms": 90, "phase_lr_deg": -4.9},
{"word": "붐비는", "start_ms": 425, "dur_ms": 110, "phase_lr_deg": -5.2},
{"word": "곳이에요.", "start_ms": 545, "dur_ms": 31, "phase_lr_deg": -5.0}
]
},
{
"segment": 7,
"transcript": "비빔밥 고추장이 아주 맵지는 않죠?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "비빔밥", "start_ms": 60, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "고추장이", "start_ms": 200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 360, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "맵지는", "start_ms": 460, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "않죠?", "start_ms": 560, "dur_ms": 16, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "살짝 매콤한 정도라 괜찮을 거예요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "살짝", "start_ms": 40, "dur_ms": 90, "phase_lr_deg": -4.5},
{"word": "매콤한", "start_ms": 140, "dur_ms": 130, "phase_lr_deg": -4.6},
{"word": "정도라", "start_ms": 280, "dur_ms": 120, "phase_lr_deg": -4.4},
{"word": "괜찮을", "start_ms": 410, "dur_ms": 110, "phase_lr_deg": -4.5},
{"word": "거예요.", "start_ms": 530, "dur_ms": 46, "phase_lr_deg": -4.6}
]
},
{
"segment": 9,
"transcript": "Delicious! The flavor is perfect.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "Delicious!", "start_ms": 55, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "The", "start_ms": 245, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "flavor", "start_ms": 315, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 455, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "perfect.", "start_ms": 525, "dur_ms": 51, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "참기름 향이 정말 고소하네요.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "참기름", "start_ms": 30, "dur_ms": 130, "phase_lr_deg": -5.4},
{"word": "향이", "start_ms": 170, "dur_ms": 110, "phase_lr_deg": -5.3},
{"word": "정말", "start_ms": 290, "dur_ms": 110, "phase_lr_deg": -5.5},
{"word": "고소하네요.", "start_ms": 410, "dur_ms": 166, "phase_lr_deg": -5.4}
]
},
{
"segment": 11,
"transcript": "여기 반찬도 깔끔하고 맛있어요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "여기", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "반찬도", "start_ms": 160, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "깔끔하고", "start_ms": 290, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "맛있어요.", "start_ms": 450, "dur_ms": 126, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 13,
"transcript": "식후에 시원한 차 한잔 마실까요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "식후에", "start_ms": 40, "dur_ms": 130, "phase_lr_deg": -4.2},
{"word": "시원한", "start_ms": 180, "dur_ms": 140, "phase_lr_deg": -4.3},
{"word": "차", "start_ms": 330, "dur_ms": 60, "phase_lr_deg": -4.1},
{"word": "한잔", "start_ms": 400, "dur_ms": 110, "phase_lr_deg": -4.2},
{"word": "마실까요?", "start_ms": 520, "dur_ms": 56, "phase_lr_deg": -4.3}
]
},
{
"segment": 14,
"transcript": "좋아요, 제가 아는 예쁜 카페가 있어요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "좋아요,", "start_ms": 25, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 175, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "아는", "start_ms": 275, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "예쁜", "start_ms": 395, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "카페가", "start_ms": 515, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "있어요.", "start_ms": 570, "dur_ms": 6, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "Perfect! I need some caffeine now.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "Perfect!", "start_ms": 45, "dur_ms": 140, "phase_lr_deg": -5.1},
{"word": "I", "start_ms": 195, "dur_ms": 60, "phase_lr_deg": -5.2},
{"word": "need", "start_ms": 265, "dur_ms": 110, "phase_lr_deg": -5.0},
{"word": "some", "start_ms": 385, "dur_ms": 80, "phase_lr_deg": -5.1},
{"word": "caffeine", "start_ms": 475, "dur_ms": 101, "phase_lr_deg": -5.2}
]
},
{
"segment": 16,
"transcript": "가서 시원한 아이스 아메리카노 마셔요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "가서", "start_ms": 60, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "시원한", "start_ms": 160, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "아이스", "start_ms": 310, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "아메리카노", "start_ms": 430, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "마셔요.", "start_ms": 560, "dur_ms": 16, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "오늘 점심 정말 만족스럽네요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": -5.5},
{"word": "점심", "start_ms": 160, "dur_ms": 120, "phase_lr_deg": -5.4},
{"word": "정말", "start_ms": 290, "dur_ms": 110, "phase_lr_deg": -5.6},
{"word": "만족스럽네요.", "start_ms": 410, "dur_ms": 166, "phase_lr_deg": -5.5}
]
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
"transcript": "다음에 또 오고 싶을 정도예요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 35, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 185, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "오고", "start_ms": 255, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "싶을", "start_ms": 375, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "정도예요.", "start_ms": 495, "dur_ms": 81, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "Me too. Best bibimbap in town.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Me", "start_ms": 50, "dur_ms": 60, "phase_lr_deg": -4.2},
{"word": "too.", "start_ms": 120, "dur_ms": 80, "phase_lr_deg": -4.3},
{"word": "Best", "start_ms": 210, "dur_ms": 110, "phase_lr_deg": -4.1},
{"word": "bibimbap", "start_ms": 330, "dur_ms": 150, "phase_lr_deg": -4.2},
{"word": "in", "start_ms": 490, "dur_ms": 40, "phase_lr_deg": -4.3},
{"word": "town.", "start_ms": 540, "dur_ms": 36, "phase_lr_deg": -4.2}
]
},
{
"segment": 21,
"transcript": "이제 다시 업무 복귀해야겠네요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 165, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "업무", "start_ms": 285, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "복귀해야겠네요.", "start_ms": 415, "dur_ms": 161, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "네, 오후 시간도 힘내봅시다!",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 30, "dur_ms": 60, "phase_lr_deg": -5.1},
{"word": "오후", "start_ms": 100, "dur_ms": 110, "phase_lr_deg": -5.2},
{"word": "시간도", "start_ms": 220, "dur_ms": 130, "phase_lr_deg": -5.0},
{"word": "힘내봅시다!", "start_ms": 360, "dur_ms": 216, "phase_lr_deg": -5.1}
]
},
{
"segment": 23,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 24,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 25,
"transcript": "Let's grab a coffee before the meeting.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Let's", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "grab", "start_ms": 160, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 290, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "coffee", "start_ms": 340, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "before", "start_ms": 480, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 550, "dur_ms": 10, "phase_lr_deg": 0.0},
{"word": "meeting.", "start_ms": 570, "dur_ms": 6, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "그럴까요? 잠도 깨고 좋겠네요.",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "그럴까요?", "start_ms": 60, "dur_ms": 180, "phase_lr_deg": 2.5},
{"word": "잠도", "start_ms": 250, "dur_ms": 110, "phase_lr_deg": 2.6},
{"word": "깨고", "start_ms": 370, "dur_ms": 110, "phase_lr_deg": 2.4},
{"word": "좋겠네요.", "start_ms": 490, "dur_ms": 86, "phase_lr_deg": 2.5}
]
},
{
"segment": 27,
"transcript": "여기 아메리카노 두 잔 주문이요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "여기", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "아메리카노", "start_ms": 170, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "두", "start_ms": 360, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "잔", "start_ms": 430, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "주문이요.", "start_ms": 500, "dur_ms": 76, "phase_lr_deg": 0.0}
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
"transcript": "원두는 어떤 걸로 드릴까요?",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "원두는", "start_ms": 35, "dur_ms": 150, "phase_lr_deg": 4.1},
{"word": "어떤", "start_ms": 195, "dur_ms": 110, "phase_lr_deg": 4.2},
{"word": "걸로", "start_ms": 315, "dur_ms": 110, "phase_lr_deg": 4.0},
{"word": "드릴까요?", "start_ms": 435, "dur_ms": 141, "phase_lr_deg": 4.1}
]
},
{
"segment": 30,
"transcript": "산미가 적고 고소한 맛으로 주세요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "산미가", "start_ms": 50, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "적고", "start_ms": 190, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "고소한", "start_ms": 310, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "맛으로", "start_ms": 450, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "주세요.", "start_ms": 550, "dur_ms": 26, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "I'll have the same thing, please.",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "I'll", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 2.8},
{"word": "have", "start_ms": 130, "dur_ms": 110, "phase_lr_deg": 2.7},
{"word": "the", "start_ms": 250, "dur_ms": 60, "phase_lr_deg": 2.9},
{"word": "same", "start_ms": 320, "dur_ms": 110, "phase_lr_deg": 2.8},
{"word": "thing,", "start_ms": 440, "dur_ms": 60, "phase_lr_deg": 2.7},
{"word": "please.", "start_ms": 510, "dur_ms": 66, "phase_lr_deg": 2.9}
]
},
{
"segment": 32,
"transcript": "네, 진동벨 울리면 가지러 와주세요.",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 30, "dur_ms": 60, "phase_lr_deg": 4.4},
{"word": "진동벨", "start_ms": 100, "dur_ms": 130, "phase_lr_deg": 4.5},
{"word": "울리면", "start_ms": 240, "dur_ms": 120, "phase_lr_deg": 4.3},
{"word": "가지러", "start_ms": 370, "dur_ms": 110, "phase_lr_deg": 4.4},
{"word": "와주세요.", "start_ms": 490, "dur_ms": 86, "phase_lr_deg": 4.5}
]
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
"transcript": "여기 커피 나왔습니다. 맛있게 드세요.",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "여기", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 4.7},
{"word": "커피", "start_ms": 170, "dur_ms": 110, "phase_lr_deg": 4.8},
{"word": "나왔습니다.", "start_ms": 290, "dur_ms": 150, "phase_lr_deg": 4.6},
{"word": "맛있게", "start_ms": 450, "dur_ms": 110, "phase_lr_deg": 4.7

---
*Generated by WKAppBot ask gemini — 2026-03-31 05:43:27*
