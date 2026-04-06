# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=04:44:14  uptime=12299s  mem=1637MB  system_processes=444"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_044404.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 675-675-675-657-671-617-276-657-765-756-576-756-576-576-576-756-756-576-256-526-657-567-567-526-532-567-576-567-526-527-567-572-576-756-752_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 126-321 107-127-261-267 671-670 123-103-103-310-672-673 267 261-621-621-623-620 621 620-627 136-132-261-623-762-675_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 316-236-263-231-132-312-132-312-761-367-231-316-321-162-613-613-326 132-312 163-206-136-132-513-315-312-276-231-623-160-261-612-612-260-627-620-620-623-602-621-160-107-107-107-1_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 612-562-623 623-162-632-615-673-163-132-312-102-162-132-263-652-132-125-162-162-176-163-103-103-106-165-602-617-162-657-617-672-612-612-672-673-263-103-136-137-516-163-216-267-2_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 326-106-103-106-162-203-327-103-301-302-301-302-326-312-301-130-312-305-260-237-312-320-320-321-302-320-316-362-126-201-301-132-756-567-562-576-675-657-570-326-136-312-132-315-3_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 162-136-310-671-765-765 376-763-732-376-736-637-763 157-175-570-576-132 371-132-316-132_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 362-312-312-231-312-321-236-362-632-653-756-657-675-657-765-675-675-675-765-136-136-617-162-716-123-670-765 137_W.mp3)
  Segment 8: 11032ms ~ 19744ms (file: 673-267-627-672-260-267-637-672 673-672-672-672-672-672-673-762-761_W.mp3)
  Segment 9: 20744ms ~ 21320ms (file: 627-627-632 163-162 756-576-567-567-657-657-567-562-165-261-163-175-675-652-165-567-567-675-627-623-326-613-657-762-672-671-163-607-136-361-130-132-103-615-106-735-760-601-671-6_W.mp3)
  Segment 10: 22320ms ~ 22896ms (file: 206-326-260-620-260-206-602-627-623-362-360-236-320-310-132-123-236-613-312-637-362-123-321-326-132-216-321-162-163-716-136-310-132-103-162-613-106-106-106-163-136-360-612-123-3_V.mp3)
  Segment 11: 23896ms ~ 33580ms (file: 106-132-106-167-126-162-751-136-103-130-103-617-167-145-103-317-106-165-136-761-756-263-267-576-567-547_V.mp3)
  Segment 12: 34580ms ~ 35156ms (file: 106-103-173-605-126-130-103-136-673-306-167-567-127-136-153-312-271-725-726-765-756-675 127_W.mp3)
  Segment 13: 36156ms ~ 36732ms (file: 725-571-725-275-765-765-675-756-715-756-517-752-715-172-752-752-762-572-765-756-751-756-725-715_W.mp3)
  Segment 14: 37732ms ~ 44356ms (file: 326-367-671-175-751-271-631-567-576_W.mp3)
  Segment 15: 45356ms ~ 45932ms (file: 136 652-105-603-301-612-126-126-106-120-126 103-675-617-572-756-752-572-567-375 716-165-156_V.mp3)
  Segment 16: 46932ms ~ 47508ms (file: 567-657-675 613-213-612-102-670-671-670 261-320-572-541-531-167-162-165-126-103-163-162-136-136-103-317-623-163-103-162-123-107-126-176-613-167-103-620-765-162-610-601-160-163-1_V.mp3)
  Segment 17: 48508ms ~ 56140ms (file: 267-160-165-167-124-103-106-514-632-165-162-137-130-312-107-103-137-163-160-103-103-610-103-103-106-132-165-130-103-136-160-163-130-136-652-163-316-136-103-160-653-162-106-172-5_V.mp3)
  Segment 18: 57140ms ~ 57716ms (file: 103-103-103-130-671-136-320-236-602-167-120-160-637-165-120-137-675-672-271-527-712-251-321-236-675-312-562-621-126-126-312-316-160-132-761-126-176-162-135-623-602-620-106-130 1_V.mp3)
  Segment 19: 58716ms ~ 59292ms (file: 136-765-367-623-163-362-326-263-267-623-376-316-316-216-312-132-136-321-236-736-367-130-103-601-106-163-132-103-103-136-631-163-163-321-561-530-574-576-305-325-321-316-617-617-6_V.mp3)
  Segment 20: 60292ms ~ 60868ms (file: 652-657-625-627-567-625-675-156-165-617-651-657-576-165-167-165-675-176-621-576-612-165-612-160-167-163-362-367-672-756-165-573-756-263-263-263-236-263-263-261 675-765-567-675-6_W.mp3)
  Segment 21: 61868ms ~ 62444ms (file: 567-357-157-537-257-235-135-315-521-256-215-265-263-326-326-235-325-356-534-574-352-235-326-321-265-356-672-216-526-653-357-312-315-316-537-573-574-527-236-216-256-571-574-572-5_W.mp3)
  Segment 22: 63444ms ~ 64020ms (file: 106-316-637-631-567-612-106-167-162-165-103-136-103-762-567-625 637-657-657-617-617-675-316-576-576-576-576-572-756-157-325-157-251-215-125-267-235-257-257-325-236-216-265-213-3_W.mp3)
  Segment 23: 65020ms ~ 65596ms (file: 132-132-236-316-103-163-163-216-163-165-126-106-621-613-132-162-103-156-672-637-263-613_V.mp3)
  Segment 24: 66596ms ~ 67172ms (file: 517-176-130-126-617-710-126-601-607-610-672-103-106-106-103-316_V.mp3)
  Segment 25: 68172ms ~ 68748ms (file: 237-276-236-231-362-276-271-217-123-271-127-127-123-132-217-217-265-165-261-236-231-216-321-321-132-231-237-326-236-271-273-723-327-721-276-213-321-276-237-271-236-237-237-276-2_W.mp3)
  Segment 26: 69748ms ~ 70324ms (file: 170-132-321-120-765-567-561-567-576-365-356-572-637-326-103-123-617 712-716-760-570-213-261-613-103-135-126-321-534-150-325-130-632-673_W.mp3)
  Segment 27: 71324ms ~ 71900ms (file: 156-106-136-134-217-127-312-316-160-106-103-132-671-613-132-765-167-136-670-103-176-623 132-613_V.mp3)
  Segment 28: 72900ms ~ 73476ms (file: 267-267-236-236-236-326-236-612-261-156-123-361-301-361-236-162-173-726-126-261-176-627-132-175-716-162-627-157-765-157-762-765 127-765-675-627-765-176-652-576-567-567-617-176-1_W.mp3)
  Segment 29: 74476ms ~ 75052ms (file: 601-762-132-137-132-135-312-105-751-753-167-312-321-102-316-153-231-261-132-136-125-136-162-136-216-162-316-126-176-106-106-263-621-623-132-312-132 106-167-162-162-765-107-267-6_V.mp3)
  Segment 30: 76052ms ~ 76628ms (file: 567-657-167-106-310-657-657-267-263-326-263-213-132-263-261-236-263-326-612-613-612-625-526-620-672-617-162-657-657-562-576-567-567-650-325-317-317-312-317-756-735-675-652-567-7_W.mp3)
  Segment 31: 77628ms ~ 78204ms (file: 126-526-261-167 176-673-736-761-675-765-765-756-765-756-751-765-716-765-765-765-765-765-267-236-326-312-251-315-325-213-231-235-215-251-213 263-261-316-132-157-103-513-136-126-2_W.mp3)
  Segment 32: 79204ms ~ 79780ms (file: 371-137-136-130-136-316-137-753-672-672-612-675-620-627 165-576-760-657-657-367-306-263-326-302-130-132-601-631-106-162-621-613-162-620-206-726-376-306-316-360-163-206-260-260-2_V.mp3)
  Segment 33: 80780ms ~ 81356ms (file: 625-615-612-136-167-103-167-601-106-165-106-106-106-105-152-615-627-162-103-103-136-103-312-167-567-136-160-106-167-103-136-176-671-106-106-137-621 123-216-102-210-123-213-210-2_V.mp3)
  Segment 34: 82356ms ~ 82932ms (file: 163-165-167-167-615-136-673-617-165-657-617-615-167-167-163-167-635-516-612-765-163-135-135-103-132-167-163-576-675-162-165-361-163-167-167-275-612-126-162-361-132-316-312-136 1_W.mp3)
  Segment 35: 83932ms ~ 84508ms (file: 762-672-726-267-276-230-230-236-523-527-256-625-652-623-612-623-623-203 263-260-620-206-206-542-524-502-361-310-301-301-203-203-213-132-127-132-312-136-312_W.mp3)
  Segment 36: 85508ms ~ 86084ms (file: 316-163-326-301-263-103-130-310-201-136-571-513-612-123-213-103-103-103 162-301-132-136-361-163-213-612-627-620-632-651-652-260-263-263-312-302-302-103-301-103-130-103-132-162-1_V.mp3)
  Segment 37: 87084ms ~ 87660ms (file: 312-132-316-316-260-236-321-213-306-312-601-612-631-132-123-362-321-132-321-231-321-263-631-621-216-213-326-162-216-123-310-132-163-361-316-362-617-632-123-126-312-312-362-612-1_W.mp3)
  Segment 38: 88660ms ~ 89236ms (file: 236-236-526-275-527-265-326-261-213-263-526-256-567-576-356-352-326-321-326-126-361-632-261-263-267-267-267-263-362-362-623-260-561-523-501-256-527-625-267-265-267-263-625-570-5_W.mp3)
  Segment 39: 90236ms ~ 90812ms (file: 526-235-236-263-235-362-263-265-361-263-261-236-260-625-623-536-356-357-635-653-526-652-631-623-326-326-632-623-367-361-316-136-136-613-657-356-361-631-632-136-364-365-361-365-3_W.mp3)
  Segment 40: 91812ms ~ 92388ms (file: 236-326-136-325-327-237-263-621-267-621-547-312-216-231-261-231-162-312_W.mp3)
  Segment 41: 93388ms ~ 94432ms (file: 261-326-321-132-367-106-103-176-175-263-236-230-320-321-125-263-621-632-103-316-312-132-126-106-316-261-601-617-136-132-321-136-612-576-761-765-126-716-172-765-675-657-762-672-7_V.mp3)
  Segment 42: 95432ms ~ 96008ms (file: 657-756-567-576-657-574-576-576-567-675-756-576-756-576-576-576-756-275-576-756-657-576-756-175-651-576-576-576-675-765-132-310-173-103-130-310-130-623-362-263-765-765-756-675-7_W.mp3)
  Segment 43: 97008ms ~ 97584ms (file: 612-361-316-321-123-132-312-132 362-301-312_V.mp3)
  Segment 44: 98584ms ~ 99160ms (file: 512-103-130-613-167-657-136-163-136-673-617-657-236-261 576-576_V.mp3)
  Segment 45: 100160ms ~ 100736ms (file: 165-562-653-567-157-156-167-123-132-135-513-235-153-315-156-163-625-675-571-325-321-123-516-567-152-162-623-613-617-165-567-652-657-650-513-153-152-315-351-257-165-623-671-536-2_W.mp3)
  Segment 46: 101736ms ~ 102312ms (file: 321-361-671-621-613-675-617-675-672-675-632_W.mp3)
  Segment 47: 103312ms ~ 103888ms (file: 132-124-265-267-203-615-176-132-102-312-163-612 675-237-231-236-756 762-671_V.mp3)
  Segment 48: 104888ms ~ 105464ms (file: 613-621-631-132-627-627-231-671-672-627-362-672-673-627-267-267-162-217-612-673-362-617-765-362-673-237-617-267-326-627-627-612-627-263-216-672-657-761-673-672-163-672-672-632-6_W.mp3)
  Segment 49: 106464ms ~ 107040ms (file: 265-617-162-571-103-106-136-351-167-103-371_V.mp3)
  Segment 50: 108040ms ~ 108616ms (file: 534-574-571-756-275-672-627-762-670-120-213-637-673-672-637-675-716-672-627-267-612-621 160-167-162-167-765-765-765-756_W.mp3)
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
> **Time**: 2026-04-03 04:45:37
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 2,
"transcript": "오늘 공부할 단어입니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "공부할", "start_ms": 200, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "단어입니다", "start_ms": 390, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "첫 번째 단어는 학교입니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "첫", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "번째", "start_ms": 140, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "단어는", "start_ms": 290, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "학교입니다", "start_ms": 450, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "학교에 가서 공부를 해요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "학교에", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가서", "start_ms": 210, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "공부를", "start_ms": 340, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "해요", "start_ms": 490, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "School. I study at school.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "School.", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 250, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "study", "start_ms": 310, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "at", "start_ms": 440, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "school.", "start_ms": 530, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "두 번째 단어는 선생님입니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "두", "start_ms": 35, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "번째", "start_ms": 125, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "단어는", "start_ms": 285, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "선생님입니다", "start_ms": 435, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "선생님께서 친절하게 가르쳐 주십니다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "선생님께서", "start_ms": 60, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "친절하게", "start_ms": 320, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "가르쳐", "start_ms": 530, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "주십니다", "start_ms": 690, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "Teacher. The teacher teaches kindly.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "Teacher.", "start_ms": 45, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "The", "start_ms": 275, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "teacher", "start_ms": 365, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "teaches", "start_ms": 555, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "kindly.", "start_ms": 725, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "세 번째 단어는 친구입니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "세", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "번째", "start_ms": 120, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "단어는", "start_ms": 270, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "친구입니다", "start_ms": 430, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "친구와 함께 놀이터에서 놀아요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "친구와", "start_ms": 55, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "함께", "start_ms": 205, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "놀이터에서", "start_ms": 335, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "놀아요", "start_ms": 595, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "Friend. I play with my friend at the playground.",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Friend.", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 240, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "play", "start_ms": 300, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "with", "start_ms": 410, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "my", "start_ms": 500, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "friend", "start_ms": 590, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "at", "start_ms": 740, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 830, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "playground.", "start_ms": 920, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "네 번째 단어는 사과입니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 35, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "번째", "start_ms": 125, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "단어는", "start_ms": 275, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "사과입니다", "start_ms": 435, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "사과가 아주 빨갛고 맛있어요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "사과가", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "빨갛고", "start_ms": 310, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "맛있어요", "start_ms": 500, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "Apple. The apple is very red and delicious.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Apple.", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "The", "start_ms": 240, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "apple", "start_ms": 330, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 480, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "very", "start_ms": 570, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "red", "start_ms": 700, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "and", "start_ms": 810, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "delicious.", "start_ms": 900, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "다섯 번째 단어는 바다입니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "다섯", "start_ms": 30, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "번째", "start_ms": 160, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "단어는", "start_ms": 310, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "바다입니다", "start_ms": 470, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "여름에는 바다에 가서 수영을 해요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "여름에는", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "바다에", "start_ms": 240, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가서", "start_ms": 400, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "수영을", "start_ms": 530, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "해요", "start_ms": 680, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "Sea. I swim in the sea in summer.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "Sea.", "start_ms": 45, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 205, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "swim", "start_ms": 265, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 395, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 485, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "sea", "start_ms": 575, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 685, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "summer.", "start_ms": 775, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "여섯 번째 단어는 하늘입니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "여섯", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "번째", "start_ms": 170, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "단어는", "start_ms": 320, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "하늘입니다", "start_ms": 480, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "오늘 하늘이 정말 파랗고 맑아요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "하늘이", "start_ms": 170, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 330, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "파랗고", "start_ms": 440, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "맑아요", "start_ms": 600, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "Sky. Today the sky is very blue and clear.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Sky.", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "Today", "start_ms": 210, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 400, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "sky", "start_ms": 490, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 620, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "very", "start_ms": 710, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "blue", "start_ms": 840, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "and", "start_ms": 950, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "clear.", "start_ms": 1040, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "일곱 번째 단어는 책입니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "일곱", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "번째", "start_ms": 165, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "단어는", "start_ms": 315, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "책입니다", "start_ms": 475, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "매일 저녁에 재미있는 책을 읽어요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "매일", "start_ms": 55, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "저녁에", "start_ms": 165, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "재미있는", "start_ms": 325, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "책을", "start_ms": 535, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "읽어요", "start_ms": 665, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "Book. I read an interesting book every evening.",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Book.", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 200, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "read", "start_ms": 260, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "an", "start_ms": 370, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "interesting", "start_ms": 460, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "book", "start_ms": 720, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "every", "start_ms": 850, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "evening.", "start_ms": 1000, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "여덟 번째 단어는 노래입니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "여덟", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "번째", "start_ms": 165, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "단어는", "start_ms": 315, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "노래입니다", "start_ms": 475, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "신나는 노래를 들으면 기분이 좋아져요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "신나는", "start_ms": 45, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "노래를", "start_ms": 235, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "들으면", "start_ms": 395, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "기분이", "start_ms": 545, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "좋아져요", "start_ms": 705, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "Song. Listening to an exciting song makes me feel good.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Song.", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "Listening", "start_ms": 240, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 470, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "an", "start_ms": 560, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "exciting", "start_ms": 650, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "song", "start_ms": 860, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "makes", "start_ms": 990, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "me", "start_ms": 1120, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "feel", "start_ms": 1210, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "good.", "start_ms": 1320, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "아홉 번째 단어는 꽃입니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "아홉", "start_ms": 30, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "번째", "start_ms": 160, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "단어는", "start_ms": 310, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "꽃입니다", "start_ms": 470, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "봄이 오면 예쁜 꽃들이 많이 피어요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "봄이", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "오면", "start_ms": 160, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "예쁜", "start_ms": 290, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "꽃들이", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 580, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "피어요", "start_ms": 690, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "Flower. When spring comes, many pretty flowers bloom.",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Flower.", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "When", "start_ms": 250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "spring", "start_ms": 360, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "comes,", "start_ms": 520, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "many", "start_ms": 670, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "pretty", "start_ms": 800, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "flowers", "start_ms": 950, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "bloom.", "start_ms": 1140, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "마지막 단어는 가족입니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "마지막", "start_ms": 35, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "단어는", "start_ms": 195, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가족입니다", "start_ms": 355, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "우리 가족은 모두 서로 사랑해요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "우리", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "가족은", "start_ms": 155, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "모두", "start_ms": 315, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "서로", "start_ms": 425, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "사랑해요", "start_ms": 555, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "Family. All of our family members love each other.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Family.", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "All", "start_ms": 260, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "of", "start_ms": 350, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "our", "start_ms": 410, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "family", "start_ms": 500, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "members", "start_ms": 650, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "love", "start_ms": 840, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "each", "start_ms": 950, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "other.", "start_ms": 1060, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "오늘 공부하느라 정말 고생 많으셨습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "공부하느라", "start_ms": 170, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 400, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "고생", "start_ms": 510, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "많으셨습니다", "start_ms": 640, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "다음 시간에 더 유익한 내용으로 만나요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "다음", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "시간에", "start_ms": 155, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 285, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "유익한", "start_ms": 345, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "내용으로", "start_ms": 505, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 695, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "여러분들의 꿈을 언제나 응원합니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "여러분들의", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "꿈을", "start_ms": 260, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "언제나", "start_ms": 390, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "응원합니다", "start_ms": 550, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "Great job today! Let's see you again next time.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Great", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "job", "start_ms": 170, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "today!", "start_ms": 280, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "Let's", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "see", "start_ms": 580, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 670, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "again", "start_ms": 760, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "next", "start_ms": 890, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "time.", "start_ms": 1000, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "꾸준히 복습하는 것 잊지 마세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "꾸준히", "start_ms": 45, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "복습하는", "start_ms": 205, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 395, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "잊지", "start_ms": 455, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "마세요", "start_ms": 565, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "노력은 결코 배신하지 않습니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "노력은", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "결코", "start_ms": 200, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "배신하지", "start_ms": 330, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "않습니다", "start_ms": 540, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "여러분의 열정이 정말 아름답습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "여러분의", "start_ms": 55, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "열정이", "start_ms": 245, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 405, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "아름답습니다", "start_ms": 515, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "안녕히 가세요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 230, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "See you again. Goodbye!",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "again.", "start_ms": 250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "Goodbye!", "start_ms": 410, "dur_ms": 200, "phase_lr_deg": 0.0}
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
"transcript": "열심히 공부합시다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "열심히", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "공부합시다", "start_ms": 230, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "오늘도 화이팅",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "오늘도", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "화이팅", "start_ms": 210, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "꿈은 이루어집니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "꿈은", "start_ms": 55, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "이루어집니다", "start_ms": 185, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "포기하지 마세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "포기하지", "start_ms": 45, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "마세요", "start_ms": 235, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "다시 시작해봐요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "다시", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "시작해봐요", "start_ms": 170, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "힘내세요 여러분",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "힘내세요", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "여러분", "start_ms": 260, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "잘 하고 있어요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "잘", "start_ms": 45, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "하고", "start_ms": 135, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 245, "dur_ms": 150, "phase_lr_deg": 0.0}
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
*Generated by WKAppBot ask gemini — 2026-04-03 04:45:37*
