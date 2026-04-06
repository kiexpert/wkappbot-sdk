# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=07:41:48  uptime=22954s  mem=1774MB  system_processes=446"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_074140.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 576-576-576-765-235-312-132-361-362-261-256-526-572-752-276-267-267-265-627-267-267-265-256-257-527-527-276-527-524-524-256-265-256-526-257-321-321-312-327-326-236-261-236-263-5_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 132-153-216-103-132-735-531-351-715-316-312 216-135-276-126_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 617-671-675-671-675-675-672-671-671-671-610-617-167-612-675-637-761-130-130-706-617-673-316-126-315-316-167-126-143-136-215-123-176-164-314_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 167-657-361-136-165-613-162-625-625-623-723-103-671-162-160-162-103-176-103-136-761-672-526-612-613-102-107-167-167-327-316-301-672-163-756-657-260-712-216-671-716-574-105-576-1_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 762-176-762-276-276-276-267-726-726-726-762-726-726-276-206-207-267-276-726-276-765-762-762-726-672-627_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 763-762-762-672-765-657-527-675-364-763-573-576-574-453-576-276-675-672-573-756-754-753-675-673-765-765-675-576-675-675-627-637-362-756-750-750-765-735-706-705-763-706-765-573-5_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 316-326-230-316-126-260-261-102-126-612-162-126-106-162-163-301-265-263-165-162-106-312-620-236-260-260-206-652-627-602-106-603-630-263-156-201-206-206-206-260-267-275-167-130-1_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 715-150-167-621-316-321-120-167-623-316-103-201-103-310-135-103-312-632-312-302-103-123-672-136-136-267-162-675-316-615-651-132-236-216-203-103-326-306-167-312-163-207-356-102-6_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 736 547-574-571-671-671 306-231-302-306-103-132-260-206-206-123-103-631-613-612-613-632-612-617-326-620-603-602-602-621-623-361-673-302-320-231-326-320-132-301-132-321-726-762_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 627-765-675-675-265-612-620-657-657-651-657-657-657-536-617-106-165-106-160-162-123-163-625-163-326-260-562-562-563-562-560-756-617-605-576-576-567-576-652-675-762-265-627-167-6_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 632-623-103-165-162-165-167-132-612-610-160-103-567-672-126-106-132-362-632-326-320-321-236-136-130-136-173-126-123-130-107-106-176-107-107-173-132_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 367-631-671-672-762-672-160-176-761-762-672-162-761-670-760-673_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 765-263-206-216-236-302-203-302-136-217-267-372-123-302-302-320-231-327-652-261-231-206-267-267-236-762-562-206-267-263-231-261-263-210-263-213-267-623-216-231-273-527-162-261-2_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 612-163 657-657 361-163-617-316-167-163-613-160-316-106-362-103-237-132-123-136-671-160-130-103-521-201-631-163-162-106-103-316-162-136-163-103-316-236-136-567-632-126-621-163-1_V.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 506-160-127-163-612-312-132-306-326-302-632-123-321-321-627-657-612-726-270-256-265-236-263-256-526-126-162-163-264-632-261-162-123-126-326-760-167-173-132-316-675-627-672-675-6_V.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 106-623-236 761-716-756-175-371-107-167-167-123-312-162-163-617-210-120-210-102-120-210-210-210-215-213-126-125-152-125-102-120-123-126-267-267-726-623-261 617-165-103-547-574-5_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 617-263-261-103-167-103-173-103-132-301-312-301-317-312_V.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 756-703-317-301-103-103-103-103-103-267-103-316-630-612-260-106-103-136-103-163-106-162-130-360-203-320-132-310-312-132-312-132-312-102-213-302-301-302-310-160-167-165-175-137-3_V.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 126-176-106-756-756-765-136-362-236-632-263-136-103-317-301-301-310-316-362-132-167-630-136-106-756-261-312-132-126-367-327-736-273-372-376_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 237-167-213-621-617-612-126-610-162-621-261-263-756-126-671-671-127-236-320-305-365-620-750-570-532-205-230-253-502-621-671-627-276-675-570-576-765-620-756-362-756-715 564-576-5_W.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 761-172-621-360-367-603-316-310-312-132 160-132-126-602-103_V.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 136-123-176-576-756-156-561-157-715-615-316-312-132-312 162 675-765-213-236 715-751-750-751-325-132-132-103-610-136-103-106-654-106-156-163-103-103-160-162-103-356-136-175-172-1_V.mp3)
  Segment 23: 34672ms ~ 36220ms (file: 263-206-306-261-761-706-276-216-126_W.mp3)
  Segment 24: 37220ms ~ 37796ms (file: 654-641-576-573 135-530-561-361 547 705-765-706-576-570-756-765-675-675-675-167-126-317-162-173-173-126-321-312-310-130-132-162-756-576-756-756 627-672_W.mp3)
  Segment 25: 38796ms ~ 39372ms (file: 672-132-261-216-231-263-216-320-320-675-762-762-273-625-362-631-362-312_V.mp3)
  Segment 26: 40372ms ~ 40948ms (file: 163-136-126-312-361-321-126-637-123-637-361-367-136-627-652-675-267-261-627-756-567-576-576-567-562-657-567-675-576-657-675-765-675-765 126-132-675-312-362-316-317-367-671-673 1_W.mp3)
  Segment 27: 41948ms ~ 42524ms (file: 103-173-103-103-136-165-136-615-731-657-105-165-132-136-163-103-163-130-103-163-137-130-163-160-103-103-126-160-601-163-162-126-602-312-136-621-312_V.mp3)
  Segment 28: 43524ms ~ 44100ms (file: 263-230-320-210-130-160-130-302-301-310-302-320-320-306-302-123-106-162-316-360-312-132-137-312-213-163-361-176-132-136-175-175-750-217-312-312-132-132-132-312-125-132-130-237-2_W.mp3)
  Segment 29: 45100ms ~ 45676ms (file: 562-756-765-756-176-107-102-123-124 504-507-507-507-236-613 132-312-134-132-312-301 602-621-613-612 260-321-230-321-231-302-756-765-765-726-706-273-132-137-127-154-516-265-216-2_V.mp3)
  Segment 30: 46676ms ~ 47252ms (file: 672-671-762-206-206-361-216-326-123-235-260-206-206 207-723-237-723-572-376-176-132-103-301-317-537-357-132-320-230-230-230-206-203-360-312-316-132-213-213-207-203-213-321-320-6_V.mp3)
  Segment 31: 48252ms ~ 48828ms (file: 672-261-263-261-263-263-613-106-632-610-765-276-627-756-726-627-672-610-630-657-765-516-651-130-316-362-106-310-301-316-516-253-561-573-576-573-136-312-620-523-526-526-536-726-2_W.mp3)
  Segment 32: 49828ms ~ 50404ms (file: 136-162 756-756-756-756-751-756-756-756 574-574-574-574-754-754-574-754-756-756-765-756-765-130-126-312 132-103-127-617-136-610-162-132-106-310-165-715-756-721-725-136-123-165-2_W.mp3)
  Segment 33: 51404ms ~ 51980ms (file: 167-361-136-107-107-170-756-163-123-103-106-163-167-367-631-326-162-167-612-103-106-102-132-763-167-107-106-167-365-136-120-713-163-631-106-176-160-716-750-651-713-176-136-175-1_W.mp3)
  Segment 34: 52980ms ~ 53556ms (file: 130-132-152-126-612-126-132-312-130-160-210-102-167-612-167-162-721-136-621-132-316-103-706-726-706-761-163-103-312-163-163-126-152-671-765-613-126-236-120-103-163-160-106-673-1_V.mp3)
  Segment 35: 54556ms ~ 55132ms (file: 263-623-623 621-621-216-261-627-261-261-621-261-132-136-136-301-561-356-167-162-163-172-132-361-130-123-162-261-326-362-671-136-675-135-167-126-163-132-103-163-102-106-136-130-1_W.mp3)
  Segment 36: 56132ms ~ 56708ms (file: 631 672-672-617-675-765-765-672 675-617-573-563-675-235-321-237-237-632-267-127-576-715-761-617-576-576-576-576-576-672-627-267-263-621-132-126-157-672-567-652-627-276-275-326-2_W.mp3)
  Segment 37: 57708ms ~ 58284ms (file: 132-602-602-623-601-106-167-106-106-106-312-130-132-106-162-326-620-263-236-260-206-627-675-675-620-260-623-326-360-167-136-263-675-675-675-267-260-260-106-675-675-607-301-132-1_V.mp3)
  Segment 38: 59284ms ~ 59860ms (file: 567-756-715-213-213-613 527-136-576-256-261-623-261-263-275-372-267-726-265-256-625-652-265-627-654-265-267-623-263-623_W.mp3)
  Segment 39: 60860ms ~ 61436ms (file: 623-216-135-531-510-621-260-362-526-263-263 672-675-637-765-576-567-562-760-765-105-675-672-267-267-276-327-627-762-760-675-762-671-721-612-163-675-756-576-175-165-165-162-136-6_W.mp3)
  Segment 40: 62436ms ~ 63012ms (file: 163-136-132-162-610-167-136-617-106-106-130-613-162-107-167-106-601-316-136-612-106-165-167-126-675-106-710-612-136-103-612-106-601-103-102-162-172-167-671-316-103-106-613-163-6_V.mp3)
  Segment 41: 64012ms ~ 64588ms (file: 367-367-367-630-360-236-263-263-261-261-627-627-627-617-621-162-617-127-316-361-362-317-627-362-327-326-362-361-367-362-361-267-267-267-627-627-672-627-672-627 236-236-263-236-2_W.mp3)
  Segment 42: 65588ms ~ 66164ms (file: 260-236-260-236 320-203 103-126-201-130-310-316-103-103-301-326-216-605-106-103-123-715-765-670-760 567-526-526-526-527-527 263-260-203-260-260-260 260 321-320_V.mp3)
  Segment 43: 67164ms ~ 69072ms (file: 756-705-260-260-106-572-601-160-106-621-103-263-263-132-163-103-106-617-621-203-623-617-106-167-103-106-136-106-120-320-326-302-516-560-765-260-260-120-567-236-260-162-106-102-6_W.mp3)
  Segment 44: 70072ms ~ 70648ms (file: 705-103-136-570-715-761-760-670-672-321-125-132-231-213-126-507-706-170-706-701-167-712-721-751-725-752-735-725-106-136-316-612-312-136-130-756-106-130-130-312-317-710-137-103-1_W.mp3)
  Segment 45: 71648ms ~ 72224ms (file: 672-620-605-562-215-657-756-216-132-165-165-156-153-531-125-526-521-571-563-513-125-152-561-615-537-652-516-613-675-253-536-156-517-156-165-615-165-561-165-132-365-136-156-576-5_W.mp3)
  Segment 46: 73224ms ~ 73800ms (file: 126-102-136-106-106-102-106-106-106-126-162-312-136-160-167-125-316-103-361-361-163-163-230-213-205-524-510-150-251-601-163-316-103-326-617-136-361-231-615-103-637-106-163-517-1_W.mp3)
  Segment 47: 74800ms ~ 75376ms (file: 621-163-103-136-213-162-106-263-132-120-106-136-123-326-163-136 136-213-162 675-163-176-167-612-571-574-705-175-657-756-576-176-106-130-103-136-106-103-316-103-136-132-413-132-2_V.mp3)
  Segment 48: 76376ms ~ 76952ms (file: 162-627-263-236-632-267-256-657-263-627-236-321-260-230-621-602-160-162-163-103-137-103-103-136-312-326-372-162-261-356-130-301-231-726-716-723-672-576-750-753-316-103-106-673-1_V.mp3)
  Segment 49: 77952ms ~ 78528ms (file: 624-163-326-216-231-126-236-267-126-627-617-627-162-126-216-123-632-263-103-603-716-162-103-103-103-124-134-127-617-172-125-613-317-162-105-671-167-120-617-162-134-106-627-172-1_W.mp3)
  Segment 50: 79528ms ~ 80104ms (file: 752-625-675-617 236-231-261-236-236-260-675-657-672-657-652-652-657-562-625-652-250-567-205-206-602-362-672-263-263-206 365-736-312-326-326_W.mp3)
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
> **Time**: 2026-04-03 07:43:08
> **AI**: gemini

---

## Response

AnalysisAnalysisGemini의 응답[
{
"segment": 1,
"transcript": "오늘 정말 바쁜 하루였어요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 170, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "바쁜", "start_ms": 300, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "하루였어요.", "start_ms": 410, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "I can see that. You look tired.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 120, "dur_ms": 40, "phase_lr_deg": -5.0},
{"word": "can", "start_ms": 170, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "see", "start_ms": 240, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "that.", "start_ms": 330, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "You", "start_ms": 410, "dur_ms": 50, "phase_lr_deg": -5.0},
{"word": "look", "start_ms": 470, "dur_ms": 70, "phase_lr_deg": -5.0},
{"word": "tired.", "start_ms": 550, "dur_ms": 20, "phase_lr_deg": -5.0}
]
},
{
"segment": 3,
"transcript": "네, 퇴근하고 바로 눕고 싶네요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "퇴근하고", "start_ms": 110, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 270, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "눕고", "start_ms": 380, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "싶네요.", "start_ms": 480, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "Hang in there, the weekend is coming.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "Hang", "start_ms": 60, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "in", "start_ms": 150, "dur_ms": 40, "phase_lr_deg": -5.0},
{"word": "there,", "start_ms": 200, "dur_ms": 90, "phase_lr_deg": -5.0},
{"word": "the", "start_ms": 300, "dur_ms": 30, "phase_lr_deg": -5.0},
{"word": "weekend", "start_ms": 340, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "is", "start_ms": 470, "dur_ms": 40, "phase_lr_deg": -5.0},
{"word": "coming.", "start_ms": 520, "dur_ms": 50, "phase_lr_deg": -5.0}
]
},
{
"segment": 5,
"transcript": "주말만 기다리고 있어요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "주말만", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "기다리고", "start_ms": 210, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "있어요.", "start_ms": 400, "dur_ms": 170, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "Any special plans for the weekend?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Any", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "special", "start_ms": 110, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "plans", "start_ms": 220, "dur_ms": 90, "phase_lr_deg": -5.0},
{"word": "for", "start_ms": 320, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "the", "start_ms": 390, "dur_ms": 30, "phase_lr_deg": -5.0},
{"word": "weekend?", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": -5.0}
]
},
{
"segment": 7,
"transcript": "그냥 집에서 푹 쉬려고요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "그냥", "start_ms": 55, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "집에서", "start_ms": 175, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "푹", "start_ms": 325, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "쉬려고요.", "start_ms": 395, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "That's the best way to recharge.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "That's", "start_ms": 35, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "the", "start_ms": 145, "dur_ms": 40, "phase_lr_deg": -5.0},
{"word": "best", "start_ms": 195, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "way", "start_ms": 285, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "to", "start_ms": 355, "dur_ms": 40, "phase_lr_deg": -5.0},
{"word": "recharge.", "start_ms": 405, "dur_ms": 170, "phase_lr_deg": -5.0}
]
},
{
"segment": 9,
"transcript": "동감이에요. 아무것도 안 하고 싶어요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "동감이에요.", "start_ms": 45, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "아무것도", "start_ms": 235, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 395, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "하고", "start_ms": 445, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "싶어요.", "start_ms": 525, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "Do you want to grab coffee later?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Do", "start_ms": 40, "dur_ms": 40, "phase_lr_deg": -5.0},
{"word": "you", "start_ms": 90, "dur_ms": 50, "phase_lr_deg": -5.0},
{"word": "want", "start_ms": 150, "dur_ms": 70, "phase_lr_deg": -5.0},
{"word": "to", "start_ms": 230, "dur_ms": 40, "phase_lr_deg": -5.0},
{"word": "grab", "start_ms": 280, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "coffee", "start_ms": 370, "dur_ms": 110, "phase_lr_deg": -5.0},
{"word": "later?", "start_ms": 490, "dur_ms": 80, "phase_lr_deg": -5.0}
]
},
{
"segment": 11,
"transcript": "커피 좋죠! 기분 전환이 될 것 같아요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "커피", "start_ms": 60, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "좋죠!", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "기분", "start_ms": 250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "전환이", "start_ms": 360, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "될", "start_ms": 480, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 530, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "같아요.", "start_ms": 560, "dur_ms": 10, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "(music)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 13,
"transcript": "이 노래 제목이 뭐예요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 50, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "노래", "start_ms": 110, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "제목이", "start_ms": 230, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "뭐예요?", "start_ms": 390, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "It's a new song by my favorite artist.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "It's", "start_ms": 120, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "a", "start_ms": 190, "dur_ms": 20, "phase_lr_deg": -5.0},
{"word": "new", "start_ms": 220, "dur_ms": 70, "phase_lr_deg": -5.0},
{"word": "song", "start_ms": 300, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "by", "start_ms": 390, "dur_ms": 40, "phase_lr_deg": -5.0},
{"word": "my", "start_ms": 440, "dur_ms": 50, "phase_lr_deg": -5.0},
{"word": "favorite", "start_ms": 500, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "artist.", "start_ms": 570, "dur_ms": 6, "phase_lr_deg": -5.0}
]
},
{
"segment": 15,
"transcript": "목소리가 아주 매력적이네요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "목소리가", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 230, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "매력적이네요.", "start_ms": 350, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "I'm glad you like it.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "I'm", "start_ms": 35, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "glad", "start_ms": 125, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "you", "start_ms": 235, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "like", "start_ms": 305, "dur_ms": 110, "phase_lr_deg": -5.0},
{"word": "it.", "start_ms": 425, "dur_ms": 150, "phase_lr_deg": -5.0}
]
},
{
"segment": 17,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 18,
"transcript": "오늘 커피는 제가 살게요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 60, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "커피는", "start_ms": 180, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 320, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "살게요.", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "No, let me get this one.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "No,", "start_ms": 45, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "let", "start_ms": 135, "dur_ms": 70, "phase_lr_deg": -5.0},
{"word": "me", "start_ms": 215, "dur_ms": 50, "phase_lr_deg": -5.0},
{"word": "get", "start_ms": 275, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "this", "start_ms": 365, "dur_ms": 70, "phase_lr_deg": -5.0},
{"word": "one.", "start_ms": 445, "dur_ms": 130, "phase_lr_deg": -5.0}
]
},
{
"segment": 20,
"transcript": "그럼 다음에는 꼭 제가 살게요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "다음에는", "start_ms": 170, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "꼭", "start_ms": 310, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 380, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "살게요.", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "It's a deal.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "It's", "start_ms": 100, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "a", "start_ms": 230, "dur_ms": 40, "phase_lr_deg": -5.0},
{"word": "deal.", "start_ms": 280, "dur_ms": 290, "phase_lr_deg": -5.0}
]
},
{
"segment": 22,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 23,
"transcript": "벌써 시간이 이렇게 됐네요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "벌써", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "시간이", "start_ms": 150, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 290, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "됐네요.", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "Time flies when you're having fun.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "Time", "start_ms": 35, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "flies", "start_ms": 145, "dur_ms": 110, "phase_lr_deg": -5.0},
{"word": "when", "start_ms": 265, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "you're", "start_ms": 335, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "having", "start_ms": 425, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "fun.", "start_ms": 535, "dur_ms": 40, "phase_lr_deg": -5.0}
]
},
{
"segment": 25,
"transcript": "그러게요. 이제 들어가야겠어요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "그러게요.", "start_ms": 60, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 210, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "들어가야겠어요.", "start_ms": 320, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "Okay, take care on your way back.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "Okay,", "start_ms": 45, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "take", "start_ms": 135, "dur_ms": 70, "phase_lr_deg": -5.0},
{"word": "care", "start_ms": 215, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "on", "start_ms": 285, "dur_ms": 40, "phase_lr_deg": -5.0},
{"word": "your", "start_ms": 335, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "way", "start_ms": 405, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "back.", "start_ms": 475, "dur_ms": 100, "phase_lr_deg": -5.0}
]
},
{
"segment": 27,
"transcript": "감사합니다. 내일 봬요!",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "감사합니다.", "start_ms": 40, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "내일", "start_ms": 260, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "봬요!", "start_ms": 380, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "See you tomorrow.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 110, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "you", "start_ms": 240, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "tomorrow.", "start_ms": 350, "dur_ms": 220, "phase_lr_deg": -5.0}
]
},
{
"segment": 29,
"transcript": "잘 가요!",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "잘", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "가요!", "start_ms": 140, "dur_ms": 430, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "Goodbye!",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "Goodbye!", "start_ms": 120, "dur_ms": 450, "phase_lr_deg": -5.0}
]
},
{
"segment": 31,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 32,
"transcript": "날씨가 갑자기 쌀쌀해졌네요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "날씨가", "start_ms": 60, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "갑자기", "start_ms": 210, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "쌀쌀해졌네요.", "start_ms": 340, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "It feels like autumn is here.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "It", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "feels", "start_ms": 110, "dur_ms": 110, "phase_lr_deg": -5.0},
{"word": "like", "start_ms": 230, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "autumn", "start_ms": 320, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "is", "start_ms": 450, "dur_ms": 40, "phase_lr_deg": -5.0},
{"word": "here.", "start_ms": 500, "dur_ms": 70, "phase_lr_deg": -5.0}
]
},
{
"segment": 34,
"transcript": "맞아요. 옷을 따뜻하게 입어야겠어요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "맞아요.", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "옷을", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "따뜻하게", "start_ms": 290, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "입어야겠어요.", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "Don't catch a cold.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "Don't", "start_ms": 60, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "catch", "start_ms": 170, "dur_ms": 130, "phase_lr_deg": -5.0},
{"word": "a", "start_ms": 310, "dur_ms": 40, "phase_lr_deg": -5.0},
{"word": "cold.", "start_ms": 360, "dur_ms": 210, "phase_lr_deg": -5.0}
]
},
{
"segment": 36,
"transcript": "(music)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 37,
"transcript": "벌써 단풍이 들기 시작했네요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "벌써", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "단풍이", "start_ms": 165, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "들기", "start_ms": 305, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "시작했네요.", "start_ms": 405, "dur_ms": 170, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "The colors are beautiful.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "The", "start_ms": 120, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "colors", "start_ms": 190, "dur_ms": 130, "phase_lr_deg": -5.0},
{"word": "are", "start_ms": 330, "dur_ms": 50, "phase_lr_deg": -5.0},
{"word": "beautiful.", "start_ms": 390, "dur_ms": 180, "phase_lr_deg": -5.0}
]
},
{
"segment": 39,
"transcript": "산행하기 딱 좋은 날씨예요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "산행하기", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "딱", "start_ms": 230, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 290, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "날씨예요.", "start_ms": 390, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "I love hiking in this weather.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 35, "dur_ms": 40, "phase_lr_deg": -5.0},
{"word": "love", "start_ms": 85, "dur_ms": 90, "phase_lr_deg": -5.0},
{"word": "hiking", "start_ms": 185, "dur_ms": 130, "phase_lr_deg": -5.0},
{"word": "in", "start_ms": 325, "dur_ms": 50, "phase_lr_deg": -5.0},
{"word": "this", "start_ms": 385, "dur_ms": 70, "phase_lr_deg": -5.0},
{"word": "weather.", "start_ms": 465, "dur_ms": 110, "phase_lr_deg": -5.0}
]
},
{
"segment": 41,
"transcript": "가까운 산에 한 번 가볼까요?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "가까운", "start_ms": 60, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "산에", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 310, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "번", "start_ms": 360, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "가볼까요?", "start_ms": 410, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "Sounds like a plan.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Sounds", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "like", "start_ms": 180, "dur_ms": 90, "phase_lr_deg": -5.0},
{"word": "a", "start_ms": 280, "dur_ms": 30, "phase_lr_deg": -5.0},
{"word": "plan.", "start_ms": 320, "dur_ms": 250, "phase_lr_deg": -5.0}
]
},
{
"segment": 43,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 44,
"transcript": "준비물은 제가 챙길게요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "준비물은", "start_ms": 45, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 235, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "챙길게요.", "start_ms": 355, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "Then I'll bring some snacks.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Then", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "I'll", "start_ms": 130, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "bring", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "some", "start_ms": 310, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "snacks.", "start_ms": 380, "dur_ms": 190, "phase_lr_deg": -5.0}
]
},
{
"segment": 46,
"transcript": "완벽해요! 벌써 기대되네요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "완벽해요!", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "벌써", "start_ms": 240, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "기대되네요.", "start_ms": 360, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "Me too. Let's make it a great trip.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "Me", "start_ms": 35, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "too.", "start_ms": 105, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "Let's", "start_ms": 195, "dur_ms": 90, "phase_lr_deg": -5.0},
{"word": "make", "start_ms": 295, "dur_ms": 70, "phase_lr_deg": -5.0},
{"word": "it", "start_ms": 375, "dur_ms": 30, "phase_lr_deg": -5.0},
{"word": "a", "start_ms": 415, "dur_ms": 20, "phase_lr_deg": -5.0},
{"word": "great", "start_ms": 445, "dur_ms": 90, "phase_lr_deg": -5.0},
{"word": "trip.", "start_ms": 545, "dur_ms": 30, "phase_lr_deg": -5.0}
]
},
{
"segment": 48,
"transcript": "그럼 주말에 봬요!",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 60, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "주말에", "start_ms": 160, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "봬요!", "start_ms": 280, "dur_ms": 290, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "See you then.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 100, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "you", "start_ms": 230, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "then.", "start_ms": 340, "dur_ms": 230, "phase_lr_deg": -5.0}
]
},
{
"segment": 50,
"transcript": "조심히 가세요!",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "조심히", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "가세요!", "

---
*Generated by WKAppBot ask gemini — 2026-04-03 07:43:08*
