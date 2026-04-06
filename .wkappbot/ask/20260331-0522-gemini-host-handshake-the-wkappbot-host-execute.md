# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43304  time=05:21:15  uptime=10663s  mem=1531MB  system_processes=441"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260331_052106.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 167-126-162-621-612-260-216-623-617-617-163-130-167-167-136-567-160-623-126-613-130-163-361-671-165-617-106-571-316-617-162-312-176-162-761-106 610-615-106-137-103-216-521-260 1_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 162-326-236-176-127-137-132-172-163-136-136-203-132-123-132-751-765-765-760-672-627-267-261-750-705-570-603-203-512-215-136-162-236-126-705-627-672-310-103-132-102-126-103-132-6_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 123-312-321 317-231 761-765-362-267-267-672-716-736-762-720-276-627-726-726-762-675-657-657-205-257-762-706-276-726-765-762-576-576-675-756-765-752-765-765-765-756-762-536-132-1_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 103-136-574-136-106-136-361-137-631-361-103-136-216-162-160-163-612-136-167-136-130-163-517-362-621-163-316-103-136-715-137-576-130-167-136-103-362-136-637-312-163-103-163-136-1_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 361-362-362-362-361-326-362-361-361-136-130-132-123-132-103-132-162-267-167-756-675-675-672-163-136-175-163-310-316-302-361-103-103-230-210-203-213-102-103-301-136-132-706-756-7_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 325-352-325-235-327-253-532-524-532-523-357-327-372-321-376-263-267-627-273-723-273-237-236-217-216-326-236-213-231-321-327-312-217-213-237-217-213-213-132-213-210-231-321-215-2_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 537-352-352-537-573-574-571-562-561-561-156-567-563-576-235-321-321-312-675-617-167-265-572-572-576-567-562-672-627-632-236-625-261-627-621-267-267-261-632-621-267-263-261-263-2_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 263-762-260-103-103-102-671-613-167-130-163-167-107-136-126-162-127-126-167-130-162-163-130-562-165-716-625-310-136-621-621-172-127-167-163-103-167-103-136-136-671-167-167-176-1_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 651-265-213-527-256-125-275-261-351-156-365-516-567-576-756-652-560-567-562-765-760-750-756-706-756-765-502-572-657-650-327-673-375-357-715-265-275-267-236-235-237-123-173-172-5_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 620-260-672-675-765-675-670-671-617-670-765-670-670-106-103-136-103-137-701-173-107-170-106-136-320-320-302-260-621-312-130-310-132-631-612-507-765-756-567-567-671-327-627-630-7_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 312-321-261-321-263-236-621-326-623-613-326-312-316-571-576-567-576-571-576-361-321-316-312-132-316-231-321-316-132-312-312-312-321-312-316-312-312-312-321-321-321-312-312-132-3_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 107-103-574-574-320-103-310-312-123-103-620-236-326-320-132-312-301-736-702-320-130-302-573-753-132-213-321-231-312-206-216-123-316-504-574-570 602-263-162-540-517-321-302-301-1_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 627-621-621-267-625-526-561-127-265-625-157-156-716-574-537-375-165-621-126-163-136-657-627-362-672-564-523-526-524-523-132-263-523-265-265-562-235-215-621-267-267-271-615-625-3_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 263-102 672-672-627-627-627-231-726-260-673-673-607-627-620-620-627-163-621 627-623-172-137-106-136-175-106-103-351-153-103-136-103-261-216-103-321-235-236-623-372-735-671-763-3_V.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 132-103-136-163-163-623-362-621-106-132-310-163-326-123-126-103-310-310-360-316-762-762-762-621-260-267-265-163-163-316-651-675-236-102-103-102-132-312-301 263-612-613-132-102-1_V.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 326-563-213-132-103-103-231-671-106-263-102-602-621-621-160-621-623-620-612-162-760-765-617-672-672-516-106-175-617-103-160-650-167-167-162-561-130-167-136-136-316-130-132-167-1_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 316-106-103-123-130-627-671-106-126-260-621-632-761-765-756-765-765-765-312-312-316-316-163-167-367-631-627-361-627-260 310-137-361-162-103-731-612-213-362-627-672-267-632-132-3_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 567-267-261-620-613 103-507-265-672-673-672 675-574-576 576-136-637-267-267-102-230-120-203-625-627-601-672-672-623-631-623-673-673-267-652-365-625-326-326-326-261 617-162-571-5_W.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 103-135 627-312-103-132-310-310-312-360-620-603-620-602-602-236-530-302-302-632-603-623-263-530-302-302-326-250-526-527-532-231-213-201-231-576-756-765-576-567-560-236-203-754-7_V.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 367-316-231-213-213-231-235-136-213-213-213-213-531-651-625-526-675-675-507-632-327-376-371-372-376-365-326-376-736-162-163-163-613-163-516-517-153-156-514-257-325-267-321-327-3_W.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 236-132-126-136-132-376-326-360-106-136-316-103-102-103-132-176-103-136-103-136-106-103-302-261-623-301-675-675-672-263-321-326-675-607-675-621-310-310-301-513-517-135-302-326-3_V.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 567-576-567-567-657-657-657-657-610-612-625-627-765-675-763-672-526-513-162-126-126-216-126-126 102-120-102-102_W.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 623-627-261-623-216-261-213-632-213_W.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 267-612-163-132-756-163-126-547-756-576-657-576-673-756-713-726-163-762-673-763-673-617-136-103-670-103-130-361-203-671-612-610-136-317-126-715-167-130-160-125-162-136-126-103-1_W.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 127-162-126-103-136-602-263-253-506-136-162-106-102-126-316-326-203-132-312-301-132-312-132-103-132-123-132-312-132-137 623-263-206-723-623-613-163-310-301-602-310-301-301-710-1_V.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 126-132-103-132-103-312 132-132-132-103_V.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 216-527-756-762-765-752-572-567-215-123-213-123-217-172-571-132-216-127-512-567-547-273-237-231-627-756-675-765-675-327-326-215-231-632-213-531-752-367-736-275-253-123-213-123-1_W.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 617-213-621-127-217-627-261-102-627-621-317-132-136-163-627-627_W.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 123-712-261-103-102-127-102-130-132-120-175-172-103-132-103-162-672-162-621-612-316-136-160-136-312-317-137-137-136-672-613-167-136-316-103 167-675-657-612-673-671-675-673_V.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 103-103-103-130-671-136-320-236-602-167-120-160-637-165-120-137-675-672-271-527-712-251-321-236-675-312-562-621-126-126-312-316-160-132-761-126-176-162-135-623-602-620-106-130 1_V.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 215-203-523-352-123-265-215-517-752-732-703-275-275-275-725-524-547-540-530-250-203-236-231-321-206-231-260-213-216-213-231-325-321-123-231-312-235-325-321-547-543-547-574-257-2_W.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 267-263-267-123-321-132-213-132-263-236-321-312 132 321-312-213_W.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 625-106-103-103-316-302-320-256-321-756-765-627-261-103-137-103-362-230-561-265-123-621-620-620-362-231-321-312-132-123-216-123-102-213-126-213-312-271-267-672-631-362-520-651-6_V.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 612-126-602-627-620-230-621-621-132-107-710-173-627-623-621-162-610-106-126-560-103-163-103-153-107-167-132-243-231-236-261-621-621-621-627-371-130-143-541-317-316-317-317-316-1_V.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 625-540-513-312-316-136-136-653-567-536-635-327-327-372-573-652-567-563-562-567-156-517-657-327-236-213-153-125-156-165-356-576-576-752-571-570-257-327-236-572-327-265-623-231-3_W.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 163-137-132-103-671-103-160-126-126-123-126-325-627-126-167-106-163-316-135-162-361-306-310-312-136-132-103-136-103-310-130-103-123-312-130-123-132-106-316-136-521-306-136-103-1_V.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 236-267-163-273-275-657-576-675-752-653-652-257-627-574-576-756-673-671-761-761-672-327-276-267-632-672-652-576-167-567-756-561-157-751-157-123-563-137-132-132-317-130-130-213-2_W.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 123-213-125-321-231-321-126-312-312-125-143-321-123-237-326-162-326-321-321-327-275-324-231-316-231-321-236-236-216-267-213-236-231-213-123-236-231-321-123-126-216-214-213-127-2_V.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 136-162-172-137-613-217-123-257-265-276-671-271-271-276-123-136-163-631-167-537-356-317-351-357-317-371-371-137-321-231-735-571-574-547-753-375-376-367-731-176-327-327-317-132-3_W.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 103-312-361-631-361-316-362-260-206-620-206-206-327-327-257-762-763-760 765-675-756-765-765-267-267-206-620-675-675-612-163-360-632-263-263-236-265-256-621 763-763-670-763-670-1_W.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 263-263-261-627-267-263-326-265-573-576-756-765-671-612-162-236-236-652-635-135-756-375-357-576-765-236-263-260-123-267-576-756-756-317-321-312-321-536-356-650-650-651-574-576-5_W.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 230-203-302-301-103-130-103-130-132-132-136-132-316-317-163-136-136-134-103-132-103-103-103-103-103-103-137-132-153-153-165-176-163-163-160-123-216-162-132 132-103 132-103 621-6_V.mp3)
  Segment 43: 66192ms ~ 66768ms (file: 657-610-564-560-576-612-672-672-762 765-765-753-765-716-163-132-312-105-167-716-163-756-631-132-301-162-127-267-631-162-765-671-103-136-103-653-315-163-564-316-126-620-562-657-1_V.mp3)
  Segment 44: 67768ms ~ 68344ms (file: 160-103-130-132-120-632-621-621-621-160-621-261-632-623 670-670-674-670-674-671-617-107-713-103-167-637-167-107-134-315-132-167 203-206-206-312-312-302-312-602-602-620-620-627-1_W.mp3)
  Segment 45: 69344ms ~ 69920ms (file: 176-167-173-174-132-175-576-751-657-627-613-160-163-136-601-675-106-620-260-320-326-263-263-320-302-321-132-312-132-106-263-203-621-106-601-675-302-720-672-267-260-206-750-607-3_V.mp3)
  Segment 46: 70920ms ~ 71496ms (file: 751-154-105-310-106-631-132-360-603-650-130-163-230-216-261-326-326-210-235-163-126-136-126-102-135-312-103-514-670-103-301-705-750-307-302-302-236-326-320-625-203-263-623-263-6_V.mp3)
  Segment 47: 72496ms ~ 73072ms (file: 126-316-103-136-326-213-120-130-671-176-103-263-216-261-765-132-263-120-130-136-176-231-216-361-163-103-126-103-132-312-126-612-216-160-302-326-301-310-310-367-103-163-605-173-1_V.mp3)
  Segment 48: 74072ms ~ 74648ms (file: 562-561 756-756 132-126-231-210-216-623 627-672-627-672 627-627-672-267-263-263-362-326-263-231-327-765-762-316-632-631-631-326-213-312-132-132 762-576-572-574-547-574_W.mp3)
  Segment 49: 75648ms ~ 76224ms (file: 631-632-632-361-672-627-672-671-631-326-326-362-362-536-267-621-312-163 163-612-126-267-317-163-126-621-625-657-576-657-675-672-627-130-162-736-123-123-367-567-567-657-675-657-7_W.mp3)
  Segment 50: 77224ms ~ 77800ms (file: 231-312-321-325-325-315-132-352-537-527-351-527-357-176-361-675-671-165-562-253-265-631-213-327-762-276-213-123-253-526-532-352-573-357-357-257-375-361-137-136-126-152-351-125-1_W.mp3)
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
> **Time**: 2026-03-31 05:22:07
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "오늘 회의 준비는 다 끝났나요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "회의", "start_ms": 175, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "준비는", "start_ms": 325, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 485, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "끝났나요?", "start_ms": 535, "dur_ms": 41, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "네, 자료 출력만 하면 됩니다.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": -5.2},
{"word": "자료", "start_ms": 120, "dur_ms": 130, "phase_lr_deg": -5.0},
{"word": "출력만", "start_ms": 260, "dur_ms": 160, "phase_lr_deg": -5.3},
{"word": "하면", "start_ms": 430, "dur_ms": 90, "phase_lr_deg": -5.1},
{"word": "됩니다.", "start_ms": 530, "dur_ms": 46, "phase_lr_deg": -5.2}
]
},
{
"segment": 3,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 4,
"transcript": "발표 자료에 오타는 없는지 확인했나요?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "발표", "start_ms": 60, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "자료에", "start_ms": 180, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "오타는", "start_ms": 310, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "없는지", "start_ms": 410, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "확인했나요?", "start_ms": 520, "dur_ms": 56, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "세 번 정도 꼼꼼히 읽어봤습니다.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "세", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": -4.8},
{"word": "번", "start_ms": 110, "dur_ms": 60, "phase_lr_deg": -4.9},
{"word": "정도", "start_ms": 180, "dur_ms": 90, "phase_lr_deg": -4.7},
{"word": "꼼꼼히", "start_ms": 280, "dur_ms": 140, "phase_lr_deg": -4.8},
{"word": "읽어봤습니다.", "start_ms": 430, "dur_ms": 146, "phase_lr_deg": -4.9}
]
},
{
"segment": 6,
"transcript": "I'll double check the final version.",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "I'll", "start_ms": 45, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "double", "start_ms": 135, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "check", "start_ms": 265, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 385, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "final", "start_ms": 455, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "version.", "start_ms": 555, "dur_ms": 21, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "네, 여기 파일 보내드렸습니다.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 35, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "여기", "start_ms": 125, "dur_ms": 110, "phase_lr_deg": -5.1},
{"word": "파일", "start_ms": 245, "dur_ms": 130, "phase_lr_deg": -4.9},
{"word": "보내드렸습니다.", "start_ms": 385, "dur_ms": 191, "phase_lr_deg": -5.2}
]
},
{
"segment": 8,
"transcript": "확인했어요. 이제 회의실로 갈까요?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "확인했어요.", "start_ms": 50, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 270, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "회의실로", "start_ms": 370, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "갈까요?", "start_ms": 520, "dur_ms": 56, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "잠시만요, 노트북 챙겨야 해요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "잠시만요,", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": -4.6},
{"word": "노트북", "start_ms": 230, "dur_ms": 140, "phase_lr_deg": -4.7},
{"word": "챙겨야", "start_ms": 380, "dur_ms": 120, "phase_lr_deg": -4.5},
{"word": "해요.", "start_ms": 510, "dur_ms": 66, "phase_lr_deg": -4.6}
]
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
"transcript": "아, 알겠습니다. 천천히 오세요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "아,", "start_ms": 60, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "알겠습니다.", "start_ms": 110, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "천천히", "start_ms": 300, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "오세요.", "start_ms": 450, "dur_ms": 126, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "기다려 주셔서 감사합니다.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "기다려", "start_ms": 25, "dur_ms": 160, "phase_lr_deg": -5.5},
{"word": "주셔서", "start_ms": 195, "dur_ms": 140, "phase_lr_deg": -5.3},
{"word": "감사합니다.", "start_ms": 345, "dur_ms": 231, "phase_lr_deg": -5.4}
]
},
{
"segment": 13,
"transcript": "회의가 생각보다 길어지네요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "회의가", "start_ms": 45, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "생각보다", "start_ms": 185, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "길어지네요.", "start_ms": 335, "dur_ms": 241, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "질문이 많아서 그런 것 같아요.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "질문이", "start_ms": 30, "dur_ms": 120, "phase_lr_deg": -4.2},
{"word": "많아서", "start_ms": 160, "dur_ms": 110, "phase_lr_deg": -4.3},
{"word": "그런", "start_ms": 280, "dur_ms": 80, "phase_lr_deg": -4.1},
{"word": "것", "start_ms": 370, "dur_ms": 50, "phase_lr_deg": -4.3},
{"word": "같아요.", "start_ms": 430, "dur_ms": 146, "phase_lr_deg": -4.2}
]
},
{
"segment": 15,
"transcript": "Almost finished now. Let's focus.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Almost", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "finished", "start_ms": 200, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "now.", "start_ms": 340, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "Let's", "start_ms": 450, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "focus.", "start_ms": 540, "dur_ms": 36, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "네, 마지막까지 힘냅시다.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": -5.1},
{"word": "마지막까지", "start_ms": 110, "dur_ms": 210, "phase_lr_deg": -5.0},
{"word": "힘냅시다.", "start_ms": 330, "dur_ms": 246, "phase_lr_deg": -5.2}
]
},
{
"segment": 17,
"transcript": "드디어 끝났네요. 수고하셨습니다.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "드디어", "start_ms": 60, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "끝났네요.", "start_ms": 200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "수고하셨습니다.", "start_ms": 360, "dur_ms": 216, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "다들 고생 많으셨어요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "다들", "start_ms": 30, "dur_ms": 110, "phase_lr_deg": -4.6},
{"word": "고생", "start_ms": 150, "dur_ms": 130, "phase_lr_deg": -4.7},
{"word": "많으셨어요.", "start_ms": 290, "dur_ms": 286, "phase_lr_deg": -4.5}
]
},
{
"segment": 19,
"transcript": "오늘 저녁에 회식 있는 거 아시죠?",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 2.5},
{"word": "저녁에", "start_ms": 165, "dur_ms": 140, "phase_lr_deg": 2.6},
{"word": "회식", "start_ms": 315, "dur_ms": 110, "phase_lr_deg": 2.4},
{"word": "있는", "start_ms": 435, "dur_ms": 70, "phase_lr_deg": 2.5},
{"word": "거", "start_ms": 515, "dur_ms": 30, "phase_lr_deg": 2.6},
{"word": "아시죠?", "start_ms": 555, "dur_ms": 21, "phase_lr_deg": 2.4}
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
"transcript": "네, 당연하죠. 어디서 모이나요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 50, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "당연하죠.", "start_ms": 120, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "어디서", "start_ms": 310, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "모이나요?", "start_ms": 450, "dur_ms": 126, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "회사 건너편 고깃집입니다.",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "회사", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 2.8},
{"word": "건너편", "start_ms": 160, "dur_ms": 140, "phase_lr_deg": 2.7},
{"word": "고깃집입니다.", "start_ms": 310, "dur_ms": 266, "phase_lr_deg": 2.9}
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
"transcript": "I'll be there on time.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "I'll", "start_ms": 35, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "be", "start_ms": 125, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "there", "start_ms": 195, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "on", "start_ms": 315, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "time.", "start_ms": 405, "dur_ms": 171, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "맛있게 많이 드시고 푹 쉽시다.",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "맛있게", "start_ms": 60, "dur_ms": 140, "phase_lr_deg": 3.0},
{"word": "많이", "start_ms": 210, "dur_ms": 90, "phase_lr_deg": 3.1},
{"word": "드시고", "start_ms": 310, "dur_ms": 110, "phase_lr_deg": 2.9},
{"word": "푹", "start_ms": 430, "dur_ms": 40, "phase_lr_deg": 3.0},
{"word": "쉽시다.", "start_ms": 480, "dur_ms": 96, "phase_lr_deg": 3.1}
]
},
{
"segment": 26,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 27,
"transcript": "주말에는 뭐 하실 예정인가요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "주말에는", "start_ms": 45, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 195, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "하실", "start_ms": 265, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "예정인가요?", "start_ms": 385, "dur_ms": 191, "phase_lr_deg": 0.0}
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
"transcript": "저는 가족들과 등산 가려고요.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": -5.1},
{"word": "가족들과", "start_ms": 170, "dur_ms": 150, "phase_lr_deg": -5.2},
{"word": "등산", "start_ms": 330, "dur_ms": 90, "phase_lr_deg": -5.0},
{"word": "가려고요.", "start_ms": 430, "dur_ms": 146, "phase_lr_deg": -5.1}
]
},
{
"segment": 30,
"transcript": "등산하기 참 좋은 계절이죠.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "등산하기", "start_ms": 35, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "참", "start_ms": 225, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 295, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "계절이죠.", "start_ms": 395, "dur_ms": 181, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "I hope the weather is clear.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": -5.4},
{"word": "hope", "start_ms": 110, "dur_ms": 130, "phase_lr_deg": -5.3},
{"word": "the", "start_ms": 250, "dur_ms": 60, "phase_lr_deg": -5.5},
{"word": "weather", "start_ms": 320, "dur_ms": 150, "phase_lr_deg": -5.4},
{"word": "is", "start_ms": 480, "dur_ms": 60, "phase_lr_deg": -5.3},
{"word": "clear.", "start_ms": 550, "dur_ms": 26, "phase_lr_deg": -5.5}
]
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
"transcript": "일기예보 보니까 맑다고 하네요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "일기예보", "start_ms": 60, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "보니까", "start_ms": 250, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "맑다고", "start_ms": 370, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "하네요.", "start_ms": 490, "dur_ms": 86, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "다행이네요. 안전하게 다녀오세요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "다행이네요.", "start_ms": 30, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "안전하게", "start_ms": 250, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "다녀오세요.", "start_ms": 440, "dur_ms": 136, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 36,
"transcript": "네, 월요일에 밝은 모습으로 봐요.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 45, "dur_ms": 60, "phase_lr_deg": -5.1},
{"word": "월요일에", "start_ms": 115, "dur_ms": 150, "phase_lr_deg": -5.2},
{"word": "밝은", "start_ms": 275, "dur_ms": 110, "phase_lr_deg": -5.0},
{"word": "모습으로", "start_ms": 395, "dur_ms": 130, "phase_lr_deg": -5.1},
{"word": "봐요.", "start_ms": 535, "dur_ms": 41, "phase_lr_deg": -5.2}
]
},
{
"segment": 37,
"transcript": "Have a wonderful weekend everyone.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Have", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 160, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "wonderful", "start_ms": 210, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "weekend", "start_ms": 400, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "everyone.", "start_ms": 520, "dur_ms": 56, "phase_lr_deg": 0.0}
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
"transcript": "감사합니다. 안녕히 계세요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "감사합니다.", "start_ms": 50, "dur_ms": 230, "phase_lr_deg": -5.4},
{"word": "안녕히", "start_ms": 290, "dur_ms": 180, "phase_lr_deg": -5.3},
{"word": "계세요.", "start_ms": 480, "dur_ms": 96, "phase_lr_deg": -5.5}
]
},
{
"segment": 40,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 41,
"transcript": "다들 조심히 들어가세요.",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "다들", "start_ms": 65, "dur_ms": 130, "phase_lr_deg": 2.5},
{"word": "조심히", "start_ms": 205, "dur_ms": 180, "phase_lr_deg": 2.6},
{"word": "들어가세요.", "start_ms": 395, "dur_ms": 181, "phase_lr_deg": 2.4}
]
},
{
"segment": 42,
"transcript": "See you on Monday morning.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 35, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word

---
*Generated by WKAppBot ask gemini — 2026-03-31 05:22:07*
