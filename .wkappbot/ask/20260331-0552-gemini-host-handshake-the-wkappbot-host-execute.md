# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43304  time=05:51:45  uptime=12494s  mem=1597MB  system_processes=440"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260331_055136.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 102-312-321 236-261-672-273-753-263-267-267-267-260-260-621 627-627-671-627-623-326-321-613_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 621-320-301-320-320-627-360-321-231-230-321-301-203-320-327-321-312-312-137-317-137-103-135-132-675-576-370-357-170-765-136-310-756-765-637-306-136-167-670-706-176-154-145-760-6_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 136-163-106-163-613-120-152-102-156-165-610-163-106-103-132-173-102-162-613-716-103-163-130-106-103-136-172-127-615-516 103-167-126-167-561-615-310-316-705-137-316-623-206-130-2_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 621 263-236-263-326-265 620-261-576-657-627-672-627 632-362-310-236-261-316-132-312-302-231 627-261-267-372-237-627-260-263-236-623-657-657-765-657-263 163-136 136-136-523-256-2_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 631-621-123 762-765-765-321-765-617-136-106-123-106-136-106-163-163-160-130-136-136-106-516-132-205-103-103-163-103-136-623-613-630-612-263-326-176-167-136-167-103-167-163-103-3_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 103-132-302-312-567-537-231-657-657-617-276-627-563-306-201-126-160-106-160-310-103-102-216-102-601-621-623-163-574-576-567-572-576-567-576-635-127-357-567-765-357-675-756-325-2_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 726-137-132-762-765-765-570-574-571-153-514-546-560-756-765-716-176-136-137-136-361-230-231-132-163-103-103-172-670-617-163-130-615-167-163-132-105-672-136-162-176-610-163-136-1_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 623-162-613-362-261-621-216-132-651-263-326-316-162-612-326-672-613-163-621-612-167-567-576-672-106-206-263-267-652-623-321-316-136-163-107-157-315-216-231-671-763-130-231-136-6_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 276-261-621-326-236-672-612-641-312-134-132-734-312-132-315-413-562-576-136-673-673-613-413-276-627-623-312-316-316-671-173-172-176-173-137-132-173-132-143-132-143-172-134-132-1_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 367-673-627-672-367-267-312-362-623-637-362-637-632-623-673-263-627-672-627-637-367-672-672-671-672-267-726-627-672-267-261-267-672-672-621-627-627-672-267-672-761-672-672-672-6_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 123-132-126-132 716-176-176-761 126 126-135-130-176-126-130-312-263-135-306-362-216-162-126-103-132-163-165-136-541-715-156-372-160-317-137-103-156-103-316-136-612-675-162-167-1_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 765-671-623-632-103-123-312-103-136-137-310-136-132-103-130-236-132-320-102-103-102-132-103-103-132-103-675-261-163-312-126-312-761-716-621-312-126-123-236 760-761-765-760-760-7_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 623-267-267-267-326-376-367-756-102-762-236-672-762-276-617-627-263-261-612-621-576-752-756-576-236-263-562-523-326-326-263-263-526-562-567-625-372-216-263-627-623-216-627-136-5_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 374 621-623-627-570-157-617-267-625-625-632-312-567-762-765-675-276-617-267-267-750-302-317-753-673-627-607-672-637-673-603-627-265-267-236_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 357-372-376-372-327-376-576-576-627-726-756-126-362-362-367-361-637-673-627-631-361-627-267-627-627-367-672-354-354-510-517-351-315-523-106-105-561-165-612-651-162-136-316-312-3_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 165-136-176-103-163-103-130-130-130-103-267_V.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 176-765-763-765-756-675-657-675-675-671-136 765-675 357-315_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 517-167-675-103-106-132-625-162-613-612-160-167-160-603-163-137-125-613-167-127-613-610-602-607-564-576-672-106-106-106-650-175-106-107-103-172-106-103-160-360-610-325-126-136-1_W.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 571-576-567-517-517-517-576-513-516-563-536-567-576-576-536-517-576-573-576-567-567-573-537-356-357-536-536-537-357-536-576-576-573-576-576-753-376-357-576-356-357-357-356-563-5_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 236-321-312-213-321-312-213-321-326-321-362-316-361-216-257-253-527-257-752-752-572-573-752-725-327-521-235-267-256-352-215-217-135-510-531-520-527-520 321-352-321-275-527-573-5_W.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 106-136-136-615-167-103-136-163-316-103-163-657-162-106-173-103-136-105-136-137-106-621-126_V.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 260-236-260-236 320-203 103-126-201-130-310-316-103-103-301-326-216-605-106-103-123-715-765-670-760 567-526-526-526-527-527 263-260-203-260-260-260 260 321-320_V.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 134-316-261-623-603-136-126-106-130-102-102-106-610-765-672-761-762-167-162-675-167-167-675-672-137-613-607-167-316-126-617-601-106-627-607-610-126-132-102-163-136-654-316-365-2_V.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 613-612-631-653-607-610-610-160-106-610-160-601-160-615-615-163-625-306-162-165-165-651-615-165-615-165-165-163-165-615-156-615-135-615-165-615-165-610-610-613-605-652-650-670-5_W.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 621-652-612-602-621-612-106-162-160-162-651-567-136-137-326-103-321-310-753-162-273-160-617-162-312-756-627-765-615-162-620-621-621-620-106-106-167-601-651-607-601-167-106-176-1_V.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 267-675-763-126-627-261-173-716-632-316-361-267-261-126-312-316-675-672-672-265-625-263-625-625-612-267-623-621-623-263-362-361-316-531-261 531-537-357-357 567-657-627-265-625-6_W.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 167-610-617-572-357-123-621-316-613-632-623-631-126-637-617-671-376-627-236-637-613-613-361-251-251-325-251-315-132-321-317-213-236-356-513-573-517-513-351-156-625-652-152-325-6_W.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 312-132-216-231-236-236-126-136-176-102-162-316-301-163-321-136-623-613-261-613-621-167-162-716-167-127-217-361-361-132-316-172-132-152-301-132-103-126-276-267-321-261-123-106-1_V.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 752-756-765-765-765-765-765-765-765-765-765-763-576-756-756-765-765-762-675-671-574-571-576-576-576 756-765 765-762-716-751 567-576-573-754-756-765-756-765-765-675-670-765-765-7_W.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 136-306-136-103-102-160-316-163-103-361-362-302-672-627-760-627-601-320-163-103-130-201-320-203-231-725-320-103-301-326-302-132-316-136-103-103-103-701-136-103-316-705-705-736-3_W.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 167-175-165-165-317-312-357-351-357-365-365-365-375-356_W.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 312-671-176-132-367-132-572-160-263-102-126-102-236-106-103-150-623-613-627-163-267-267-762-726-267-267-126-321-106-621-657-167-612-627-126-621-607-627-267-653-325-316-612-106-7_W.mp3)
  Segment 33: 50432ms ~ 51980ms (file: 163-615-657-163-650-106-673-106-316-547-265-632-571-163-137-102-536-175-675-165-615-601-106-756-561-170-160-176-675-106-106-156-165-617-106-156-102-561-362-162-527-536-657-106-1_V.mp3)
  Segment 34: 52980ms ~ 53556ms (file: 675-326-126-602-167-163-132-136-316-231-126-762-623-316-613-106-167-157-523-163-132-106-617-163-631-316-613-167-136-675-136-163-163-613-103-163-103-160-165-625-175-136-321-261 1_V.mp3)
  Segment 35: 54556ms ~ 55132ms (file: 125-162-603-617-765-632-263-361-130-162-156-631-137 165-167-162-160-103-567-605-716-621-752-601-620 601-631-610-167-136-103-132-564-312-126-167_V.mp3)
  Segment 36: 56132ms ~ 56708ms (file: 573-574-527-162-236-236-631-672-637-527-517-517-672-540-571-572-257-257-751-275-270-257-527-537-165-312-357-357-537-537-531-567-326-236-623-362-631-325-352-532-321-573-576-576-5_W.mp3)
  Segment 37: 57708ms ~ 58284ms (file: 670-627-602-607-610-103-106-306-630-306-260-621-103-106-201-106-106-160-236-316-106-130-310-306-607-602-607-162-136-302-320-130-130-576-675-371-173-367-167-316-103-326-203-106-1_V.mp3)
  Segment 38: 59284ms ~ 59860ms (file: 765-230-326-163-106-163-126-162-103-136-163-602-620-602-762-672-620-260-263-310-136-160-310-310-301-263-650-567-657-657-360-301-306-316-132-123-236-230-320-132-612-320-574-537-5_W.mp3)
  Segment 39: 60860ms ~ 61436ms (file: 105-135-321-265-617-167-362-735-173 130-162-670-173-617-613-167-576-765-761-173-107-765-107-517-125-167-163-126-167-167 136-312-130_W.mp3)
  Segment 40: 62436ms ~ 63012ms (file: 123 615-620-173-176 612 136 160-123-312-675-675 576-657-756-756-756-756-756-756-765-765-765-765-761_W.mp3)
  Segment 41: 64012ms ~ 64588ms (file: 307-320-302-302-312-316-612-162-610-627-627-631-306-530-253-250-253-325-267-106-103-623-260-621-126-102-132-167-765-632-236-261-126-136-163-302-527-130-216-132-301-302-320-362-2_V.mp3)
  Segment 42: 65588ms ~ 66164ms (file: 127-251-256-126-126-216-261-267-263-265-623-267-623-216-216-216-612-617-276-617-627-615-672-721-762-167-761-526-572-576-126-537-512-567-275-721-175-267-621-261-623-263-623-621-2_W.mp3)
  Segment 43: 67164ms ~ 67740ms (file: 157 132-321-321-263-752-173-137-132-126-162-132-163-137-706-673-631-316-362-236-362-237-213-321-216-216-326-321-321-623-326-321-162-316-136-162-132-126-132-675-765-672-620-672-1_V.mp3)
  Segment 44: 68740ms ~ 69316ms (file: 756-675-765-673-765 756-761-760-761-176-710-671-261-627-160-132_W.mp3)
  Segment 45: 70316ms ~ 70892ms (file: 763-765-675-617-167-613-103-107-105-126-137-132-613-103-160-103-163-651-165-165-136-167-612-162-167-630-167-632-672-132-671-251-764-671-136-136-103-106-372-167-162-106-106-136-1_V.mp3)
  Segment 46: 71892ms ~ 72468ms (file: 312-127 263-612 136-103-136-162-136-675-316-162-162-167-106-163-623-103-167-163-162-610-160-160-360-176-126-167-137-130-136-167-167-136-160-163-176-102-126-163-216-136-610-126-1_V.mp3)
  Segment 47: 73468ms ~ 74044ms (file: 561-516-165-165-615-167-165-165-165-153-135-136-165-135-135-136-136-136-135-136-165-163-165-516-615-635-625-652-657-657-537-651-615-613-516-560-650-675-560-165-615-651-657-567-5_W.mp3)
  Segment 48: 75044ms ~ 75620ms (file: 567-567-567-657-576-561-657-305-321-132-132-163-172-635-137-156-356-536-573-571-571-517-751-536-571-576-517-157-156-157-157-156-561-510-153-165-153-156-156-153-536-536-516-156-1_W.mp3)
  Segment 49: 76620ms ~ 77196ms (file: 657-657-653-365-163-657-657-563-576-537-567-567-576-576-576-561-563-537-576-657-576-576-167-517-571-753-517-576-576-576-576-576-517-567-567-567-657-567-567-517-516-567-567-576-5_W.mp3)
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
> **Time**: 2026-03-31 05:52:44
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "오늘 컨디션은 좀 어떠세요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "컨디션은", "start_ms": 165, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 315, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "어떠세요?", "start_ms": 385, "dur_ms": 191, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "잠을 잘 못 자서 조금 피곤하네요.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "잠을", "start_ms": 30, "dur_ms": 90, "phase_lr_deg": -5.2},
{"word": "잘", "start_ms": 130, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "못", "start_ms": 200, "dur_ms": 50, "phase_lr_deg": -5.3},
{"word": "자서", "start_ms": 260, "dur_ms": 100, "phase_lr_deg": -5.1},
{"word": "조금", "start_ms": 370, "dur_ms": 90, "phase_lr_deg": -5.2},
{"word": "피곤하네요.", "start_ms": 470, "dur_ms": 106, "phase_lr_deg": -5.2}
]
},
{
"segment": 3,
"transcript": "비타민이라도 좀 챙겨 드셔야겠어요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "비타민이라도", "start_ms": 55, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 245, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "챙겨", "start_ms": 315, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "드셔야겠어요.", "start_ms": 415, "dur_ms": 161, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "그래야겠어요. 커피도 한잔 마시고요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "그래야겠어요.", "start_ms": 40, "dur_ms": 210, "phase_lr_deg": -4.8},
{"word": "커피도", "start_ms": 260, "dur_ms": 120, "phase_lr_deg": -4.9},
{"word": "한잔", "start_ms": 390, "dur_ms": 110, "phase_lr_deg": -4.7},
{"word": "마시고요.", "start_ms": 510, "dur_ms": 66, "phase_lr_deg": -4.8}
]
},
{
"segment": 5,
"transcript": "Did you finish the report for tomorrow?",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "Did", "start_ms": 45, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 115, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "finish", "start_ms": 175, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 295, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "report", "start_ms": 345, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "for", "start_ms": 475, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "tomorrow?", "start_ms": 535, "dur_ms": 41, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "거의 다 했어요. 마지막 검토 중입니다.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "거의", "start_ms": 35, "dur_ms": 90, "phase_lr_deg": -5.0},
{"word": "다", "start_ms": 135, "dur_ms": 50, "phase_lr_deg": -5.1},
{"word": "했어요.", "start_ms": 195, "dur_ms": 110, "phase_lr_deg": -4.9},
{"word": "마지막", "start_ms": 315, "dur_ms": 110, "phase_lr_deg": -5.2},
{"word": "검토", "start_ms": 435, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "중입니다.", "start_ms": 525, "dur_ms": 51, "phase_lr_deg": -5.1}
]
},
{
"segment": 7,
"transcript": "고생 많으셨어요. 조금만 더 힘내세요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "고생", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "많으셨어요.", "start_ms": 170, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "조금만", "start_ms": 360, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 480, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "힘내세요.", "start_ms": 530, "dur_ms": 46, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "감사합니다. 얼른 끝내고 쉬고 싶네요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "감사합니다.", "start_ms": 40, "dur_ms": 160, "phase_lr_deg": -4.5},
{"word": "얼른", "start_ms": 210, "dur_ms": 110, "phase_lr_deg": -4.6},
{"word": "끝내고", "start_ms": 330, "dur_ms": 130, "phase_lr_deg": -4.4},
{"word": "쉬고", "start_ms": 470, "dur_ms": 60, "phase_lr_deg": -4.5},
{"word": "싶네요.", "start_ms": 540, "dur_ms": 36, "phase_lr_deg": -4.6}
]
},
{
"segment": 9,
"transcript": "오늘 저녁에 맛있는 거 먹으러 가요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 55, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "저녁에", "start_ms": 175, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "맛있는", "start_ms": 305, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 445, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "먹으러", "start_ms": 495, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "가요.", "start_ms": 575, "dur_ms": 1, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "그럴까요? 메뉴 추천해주세요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "그럴까요?", "start_ms": 30, "dur_ms": 180, "phase_lr_deg": -5.4},
{"word": "메뉴", "start_ms": 220, "dur_ms": 120, "phase_lr_deg": -5.3},
{"word": "추천해주세요.", "start_ms": 350, "dur_ms": 226, "phase_lr_deg": -5.5}
]
},
{
"segment": 11,
"transcript": "How about some grilled pork belly?",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "How", "start_ms": 45, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "about", "start_ms": 135, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "some", "start_ms": 255, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "grilled", "start_ms": 345, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "pork", "start_ms": 475, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "belly?", "start_ms": 545, "dur_ms": 31, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "좋아요! 삼겹살은 언제나 옳죠.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "좋아요!", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": -5.1},
{"word": "삼겹살은", "start_ms": 200, "dur_ms": 180, "phase_lr_deg": -5.2},
{"word": "언제나", "start_ms": 390, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "옳죠.", "start_ms": 520, "dur_ms": 56, "phase_lr_deg": -5.1}
]
},
{
"segment": 13,
"transcript": "그럼 7시에 회사 앞에서 봬요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "7시에", "start_ms": 170, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "회사", "start_ms": 300, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "앞에서", "start_ms": 400, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "봬요.", "start_ms": 530, "dur_ms": 46, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "네, 이따 뵙겠습니다.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 35, "dur_ms": 60, "phase_lr_deg": -4.2},
{"word": "이따", "start_ms": 105, "dur_ms": 150, "phase_lr_deg": -4.3},
{"word": "뵙겠습니다.", "start_ms": 265, "dur_ms": 311, "phase_lr_deg": -4.2}
]
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
"transcript": "실례합니다. 혹시 이 근처에 우체국이 있나요?",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "실례합니다.", "start_ms": 60, "dur_ms": 180, "phase_lr_deg": 2.5},
{"word": "혹시", "start_ms": 250, "dur_ms": 110, "phase_lr_deg": 2.6},
{"word": "이", "start_ms": 370, "dur_ms": 40, "phase_lr_deg": 2.4},
{"word": "근처에", "start_ms": 420, "dur_ms": 90, "phase_lr_deg": 2.5},
{"word": "우체국이", "start_ms": 520, "dur_ms": 50, "phase_lr_deg": 2.6},
{"word": "있나요?", "start_ms": 570, "dur_ms": 6, "phase_lr_deg": 2.4}
]
},
{
"segment": 17,
"transcript": "저기 사거리에서 왼쪽으로 꺾으면 바로 보여요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "저기", "start_ms": 40, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "사거리에서", "start_ms": 140, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "왼쪽으로", "start_ms": 300, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "꺾으면", "start_ms": 440, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 550, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "보여요.", "start_ms": 570, "dur_ms": 6, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "Is it within walking distance?",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Is", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": 2.8},
{"word": "it", "start_ms": 140, "dur_ms": 40, "phase_lr_deg": 2.7},
{"word": "within", "start_ms": 190, "dur_ms": 130, "phase_lr_deg": 2.9},
{"word": "walking", "start_ms": 330, "dur_ms": 110, "phase_lr_deg": 2.8},
{"word": "distance?", "start_ms": 450, "dur_ms": 126, "phase_lr_deg": 2.9}
]
},
{
"segment": 19,
"transcript": "네, 걸어서 5분 정도면 충분합니다.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 30, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "걸어서", "start_ms": 100, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "5분", "start_ms": 240, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "정도면", "start_ms": 340, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "충분합니다.", "start_ms": 460, "dur_ms": 116, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "친절하게 알려주셔서 감사합니다.",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "친절하게", "start_ms": 65, "dur_ms": 180, "phase_lr_deg": 3.0},
{"word": "알려주셔서", "start_ms": 255, "dur_ms": 180, "phase_lr_deg": 3.1},
{"word": "감사합니다.", "start_ms": 445, "dur_ms": 131, "phase_lr_deg": 2.9}
]
},
{
"segment": 21,
"transcript": "아니에요. 조심히 가세요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "아니에요.", "start_ms": 45, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "조심히", "start_ms": 215, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "가세요.", "start_ms": 365, "dur_ms": 211, "phase_lr_deg": 0.0}
]
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
"transcript": "어제 본 영화는 어땠어?",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "어제", "start_ms": 35, "dur_ms": 110, "phase_lr_deg": 4.5},
{"word": "본", "start_ms": 155, "dur_ms": 60, "phase_lr_deg": 4.6},
{"word": "영화는", "start_ms": 225, "dur_ms": 130, "phase_lr_deg": 4.4},
{"word": "어땠어?", "start_ms": 365, "dur_ms": 211, "phase_lr_deg": 4.5}
]
},
{
"segment": 24,
"transcript": "정말 감동적이었어. 너도 꼭 봐.",
"speaker": "speaker_5",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 5.1},
{"word": "감동적이었어.", "start_ms": 180, "dur_ms": 220, "phase_lr_deg": 5.2},
{"word": "너도", "start_ms": 410, "dur_ms": 90, "phase_lr_deg": 5.0},
{"word": "꼭", "start_ms": 510, "dur_ms": 40, "phase_lr_deg": 5.1},
{"word": "봐.", "start_ms": 560, "dur_ms": 16, "phase_lr_deg": 5.2}
]
},
{
"segment": 25,
"transcript": "The acting was truly impressive.",
"speaker": "speaker_5",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "The", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 5.5},
{"word": "acting", "start_ms": 110, "dur_ms": 110, "phase_lr_deg": 5.4},
{"word": "was", "start_ms": 230, "dur_ms": 60, "phase_lr_deg": 5.6},
{"word": "truly", "start_ms": 300, "dur_ms": 110, "phase_lr_deg": 5.5},
{"word": "impressive.", "start_ms": 420, "dur_ms": 156, "phase_lr_deg": 5.4}
]
},
{
"segment": 26,
"transcript": "결말이 좀 슬프지 않았어?",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "결말이", "start_ms": 60, "dur_ms": 130, "phase_lr_deg": 4.2},
{"word": "좀", "start_ms": 200, "dur_ms": 60, "phase_lr_deg": 4.3},
{"word": "슬프지", "start_ms": 270, "dur_ms": 110, "phase_lr_deg": 4.1},
{"word": "않았어?", "start_ms": 390, "dur_ms": 186, "phase_lr_deg": 4.2}
]
},
{
"segment": 27,
"transcript": "눈물이 멈추지 않을 정도였어.",
"speaker": "speaker_5",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "눈물이", "start_ms": 35, "dur_ms": 130, "phase_lr_deg": 5.3},
{"word": "멈추지", "start_ms": 175, "dur_ms": 110, "phase_lr_deg": 5.4},
{"word": "않을", "start_ms": 295, "dur_ms": 90, "phase_lr_deg": 5.2},
{"word": "정도였어.", "start_ms": 395, "dur_ms": 181, "phase_lr_deg": 5.3}
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
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 30,
"transcript": "기타 소리가 참 경쾌하네요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "기타", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "소리가", "start_ms": 170, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "참", "start_ms": 290, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "경쾌하네요.", "start_ms": 360, "dur_ms": 216, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 32,
"transcript": "감사합니다. 독학으로 배우고 있어요.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "감사합니다.", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": -5.1},
{"word": "독학으로", "start_ms": 230, "dur_ms": 130, "phase_lr_deg": -5.0},
{"word": "배우고", "start_ms": 370, "dur_ms": 110, "phase_lr_deg": -5.2},
{"word": "있어요.", "start_ms": 490, "dur_ms": 86, "phase_lr_deg": -5.1}
]
},
{
"segment": 33,
"transcript": "You have a great talent for music.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "You", "start_ms": 45, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "have", "start_ms": 115, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 205, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "great", "start_ms": 255, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "talent", "start_ms": 375, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "for", "start_ms": 495, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "music.", "start_ms": 545, "dur_ms": 31, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "과찬이세요. 아직 연습이 많이 필요해요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "과찬이세요.", "start_ms": 60, "dur_ms": 150, "phase_lr_deg": -5.5},
{"word": "아직", "start_ms": 220, "dur_ms": 90, "phase_lr_deg": -5.3},
{"word": "연습이", "start_ms": 320, "dur_ms": 110, "phase_lr_deg": -5.4},
{"word": "많이", "start_ms": 440, "dur_ms": 80, "phase_lr_deg": -5.3},
{"word": "필요해요.", "start_ms": 530, "dur_ms": 46, "phase_lr_deg": -5.5}
]
},
{
"segment": 35,
"transcript": "매일 꾸준히 하시는 모습이 멋져요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "매일", "start_ms": 35, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "꾸준히", "start_ms": 135, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "하시는", "start_ms": 255, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "모습이", "start_ms": 375, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "멋져요.", "start_ms": 495, "dur_ms": 81, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "즐거워서 하는 일이라 힘들지 않아요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "즐거워서", "start_ms": 50, "dur_ms": 130, "phase_lr_deg": -4.2},
{"word": "하는", "start_ms": 190, "dur_ms": 90, "phase_lr_deg": -4.3},
{"word": "일이라", "start_ms": 290, "dur_ms": 110, "phase_lr_deg": -4.1},
{"word": "힘들지", "start_ms": 410, "dur_ms": 110, "phase_lr_deg": -4.2},
{"word": "않아요.", "start_ms": 530, "dur_ms": 46, "phase_lr_deg": -4.3}
]
},
{
"segment": 37,
"transcript": "Passion is the best teacher.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Passion", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 190, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 260, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "best", "start_ms": 310, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "teacher.", "start_ms": 430, "dur_ms": 146, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "맞는 말씀이에요. 더 노력할게요.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "맞는", "start_ms": 60, "dur_ms": 90, "phase_lr_deg": -5.1},
{"word": "말씀이에요.", "start_ms": 160, "dur_ms": 150, "phase_lr_deg": -5.2},
{"word": "더", "start_ms": 320, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "노력할게요.", "start_ms": 390, "dur_ms": 186, "phase_lr_deg": -5.1}
]
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
"transcript": "나중에 작은 연주회라도 열어주세요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "나중에", "start_ms": 50, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "작은", "start_ms": 190, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "연주회라도", "start_ms": 290, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "열어주세요.", "start_ms": 450, "dur_ms": 126, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "부끄럽지만 약속할게요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "부끄럽지만", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": -4.6},
{"word": "약속할게요.", "start_ms": 230, "dur_ms": 346, "phase_lr_deg": -4.7}
]
},
{
"segment": 42,
"transcript": "I'll be looking forward to that day.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "I'll", "start_ms": 45, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "be", "start_ms": 135, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "looking", "start_ms": 205, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "forward", "start_ms": 325, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 445, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "that", "start_ms": 495, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "day.", "start_ms": 565, "dur_ms": 11, "phase_lr_deg": 0.0}

---
*Generated by WKAppBot ask gemini — 2026-03-31 05:52:44*
