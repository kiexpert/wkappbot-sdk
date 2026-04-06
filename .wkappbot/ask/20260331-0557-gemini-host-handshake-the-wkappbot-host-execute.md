# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43304  time=05:56:26  uptime=12775s  mem=1141MB  system_processes=440"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260331_055617.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 765-763-675-765-657-657-576-756-752 672-623-613-165 621-167-672-675-637-120-362-123-236-762-210-627-263-236-213-103-136-126-270-130-103-103-102-216-137-162-136-103-312-132-301-6_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 762-672 765-760-703-137-756-756-761-175-756-761-672-671-671-612-715-765-130-751-176-173-752-756-167 316 317-321_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 260-172-162 103-107-167-163-163-172-106-103-312-130-136-136-103-106-136-160-163-632-726-316-136-103-136-617-716-163-321-263-136-160-613-617-163-326-623-167-160-136-132-136-103-6_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 236-312-260-206-602-627-602-216-162-620-627-650-571-750-752-576-576-570-567-620-607-673-103-361-130-136-132-123-103-103-132-236-210-260-302-762-627-627-620-631-617-126-621-130-3_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 321-136-167-163-316-316-156-652-265-612-612-163-164-162-621-621-162-162-621-162-621-652-365-361-316-316-631-326-365-516-514-514-512-514-216-562-526-562-612-621-613-615-615-563-5_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 163-126-321-106-627-613-621 613-361-132-612-102-136-615-127-316-103-716-136-316-325-167-126 675-675-167-163-103-160-167-657-106-610-162-167-136-176-162-103-106-216-103-561-126-1_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 613-312-130-231-126-362-126-671-362-310-132-316-132-613-632-263-326-316-102-301-136-216-623-136-123 623-756-765-526-502_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 627-261-267 263-231-261-263-637-156-617-361-106-162-216-123-210-126 267-623_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 673-316-673-162-326-621 671-627 361-316-321-263-672-612-672-621-617-167-761-761-761-617-167-621-613-621-612-312_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 132-623-627-632-652-621-326-632-763-762-367-362-312 621 627-526-326-236-326-320-632-362-132-136-123-312 362-216 132-326-130-302-632-362-362-672-726-263-627-362-612-213-261-136-1_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 617-172-163-673-671-167-163-651-173-165-157-715-753-735-713-735-731-376-637-753-673-763-673-613-517-735-732-673-625-362-631-631-271-256-217-157-570-537-725-736-275-371-753-152-7_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 627-267-762-706-701-570-601-162-620-620-163-603-706-206-203-362-321-301-106-163-165-106-106-105-751-760-762-126-103-517-157-231-765-765-167-157 765-765-675-672-362-576-263-206-2_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 253-326-231-312-321-321-325-326-312-231-231-236-263-263-237-217-216-235-213-235-123-215-215-216-123-253-216-326-215-523-251-235-235-253-215-235-625-263-256-326-362-362-362-326-3_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 617-621-701-163-612-126-132-162-162-160-671-631-576-576-574-567-673-607-670-132-132-163-765-760-765-675-765-103-716-617-671-173-107-176-713-107-176-137-765-765-765-765-132-672-3_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 263-267-276-267-726-376-356-357-735-736-367-367-376-573-572-357-537-536-537-356-365-375-576-356-652-675-672-652-265-567-567-654-657-654-657-617-674-654-654-654-654-651-635-613-6_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 263-326-316-356-173-326-326-123-361-572-362-571-537-753-325-312-652-576-615-265-125-153-263-236-216-132-326-213-312-236-321-312-132-362-231-321-172-732-231-326-321-251-251-236-2_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 576-574-546-573-564-547-564-547-546-567-210-612-263-312-130-310-120-231-132-123-210-321-312-103-132-310-310-130-102-673-637-601-316-102-310-103-163-136-306-310-103-756-367-310-1_V.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 326-312-126-123-326-130-120-123-103-132-312-623-263-123-302-132-123-326-132-123_V.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 627-267-267-267-267-267-627-625-672-762 135-352-521-261-236-615-367-672-316-326-325-213-132-312-163-316-312-130-236-236-260-326-261-216-231-163-617-623-267-267-267-627-326-321-3_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 576-627-236-126-213-236-736-627-316-365-637-637-672-672-627-263-207-617-261-326-237-327-263-310-103-130-321-321-302-360-361-263-123-261-612-263-620-132-632-623-620-670-627-620-2_W.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 672 275-236-236-326-237-617 217-312-316-167-567-236-135-176-361-236-672_V.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 321-162-621-126-162-136 132 672-632-132-132-321-312-324-316-316-132-132-132-164-167-126-312-312-132-276-127-261-321-301-162-621-106-132-132-123-610-162-130-135-315-103-165-176-1_V.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 136-376-163-102-156-106-310-165-571-175-153 625-312-162-162-670-651-130-160-672-261-261-132-653-657-761-657-657-657-657-657-570-617-163-103-321-756-713 603-162-167-673-163-103-1_V.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 163-132-310-210-103-130-103-176-310-123-103-132-127-162-132-301-312 103 576-567 567-165-625-672-672-574-657-652-175-537-607-612-103-567-605-361-162-126-231-621-132-130-160-120-1_W.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 236-623-361-576-576-576-576-371-531-513-531-315-235-325-321-637-327-276-723-317-376-371-163-316-231-123-123-231-236-231-210-201-210-254-152-251-173-371-321-231-213-213-123-271-3_W.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 312-621-612-162-126-106-621-612-620-361-136-562-216-162-102-106-137-756-765-672-610-627-102-623-106-132-316-132-152-310-167-250-561-576-524-542-574-572-621-621 756-756-756-126_W.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 657-657-657-675-527-156-562-576-657-316-163-127-132-612-125-175-632-367-361-315-356-365-315-356-376-316-376-315-657-361-675-316-167-176-176-137-576-175-716-617-136-615-167-176-1_W.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 302-301-362-657-762-267-102-765-270-276-301-360-310-301-361-621-203-310-310-310-103-132-652-625 276-321-573-350-301-103-132-136-163-162-136-623-627-176-137-312-302-230-230-203-3_V.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 206-326-632-106-610-631-302-370-613-167-236-236-601-325-316-201-306-310-312-213-206-570-357-372-367-136-126-176-103-716-752-725-721-321-321-132-315-316-316-312-231-156-350-567-5_W.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 567-576-567-567-657-657-657-657-610-612-625-627-765-675-763-672-526-513-162-126-126-216-126-126 102-120-102-102_W.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 157-301-671-352-567-325-752-732-371-260-320-210-321-216-160-152-153-235-325-256-216-162-316-257-256-256-215-265-261-326-312 236-253-562_W.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 567-532-563-516-576-576-561-516-156-516-561-651-163-163-156-137-517-561-517-157-153-517-576-516-576-571-517-517-571-576-561-156-163-167-576-517-536-156-532-516-516-157-571-765-5_W.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 761-701-716-716-761-276-621-625-756-756-576-574-574-574-574-574-132-123-123-321-312-134-621-362-267-263-261-627-607-762 312-316-132-123-132-321-107-170-173-107-132-675-726-617-6_V.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 621-612-126-615-160-671-763-130-132-672-617-126-132-261-132-321-362-236-126-261-576-312-362-163-163-136-163-631-132-136-316-136-613-201-106-132-301-301-310-103-107-107-137-103-1_W.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 237-276-236-231-362-276-271-217-123-271-127-127-123-132-217-217-265-165-261-236-231-216-321-321-132-231-237-326-236-271-273-723-327-721-276-213-321-276-237-271-236-237-237-276-2_W.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 173-103-106-576-215-671 547-574-574-573-576 276-263-263-236-263-276-726-726-762-732-723-762-756-765-716-756-756-756-756-312-320-130-312-301-567-267-201-721-372-126-267-620-217-5_W.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 267-230 126 263-362-321-126-261-162-261-213-621 263-312-156-612-516-627-627-671-761-621-312-362-102-206-267-621-126-602-160-160-321-123-106-175-162 103-162-160-163-136-103-136-1_W.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 123-236-270-207-276-321-567-567-567-106-326-301-132-620-206-265-620-675-627-260-263-620-672-672-612-132-103-301-103-316-123-260-612-167-120-162-136-630-630-360-630-603-607-620-6_V.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 765-765-675-765-673-765-675-765-765-765-673-675-765-675-673-765-607-675-673-675-675-675-672-675-765-675_W.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 527-253-527-562-265-265-261-267-256-236-271-213-210-201-537-571-573-537-574-576-537-532-321-623-567-576-567-257-527-321-312-327-321-312-321-312-271-213-321-317-273-571-573-573-5_W.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 672-627-673-627-672-672-627-637-632-672-632-367-672-672-673-672-672-672-673-672-672-672-627-672-672-765-672-673-672-316-672-672-672-672-627-601-362-623-632-632-673-672-675-672-6_W.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 326 103-621-263-620-163-106-136-167-316-126-103-316-602-360-316-512-526-126-231-102-103-103-637-136-601-130-103-103-163-623-163-162-312-132-103-123-106-106-160-136-130-135-561-6_V.mp3)
  Segment 43: 66192ms ~ 66768ms (file: 123-132-231-312-102-126-102-102-132-106-132-201-123-103-163-126-613-163-160-601-136-136-132-162-216-623-106-132-213-216-103-102-132-102-310-103-316-136-136-316_V.mp3)
  Segment 44: 67768ms ~ 68344ms (file: 632-362-163-631-326-136-312-236-162-672-671-673-762-761-716-671-672-765-762-675-765-176-617-167-163-603-126-574-507-675-126-167-762-762-672-165-751-650-615-506-301-306-316-260-2_W.mp3)
  Segment 45: 69344ms ~ 69920ms (file: 765-675-267-263-567-756-567-675-675-716-567-675-657-573-756-652-627-613-621-572-562-325-657-126-216-621-372-325-675-132-126-312-123-576-576-572-576-567-756-657-517-175-136-652-5_W.mp3)
  Segment 46: 70920ms ~ 71496ms (file: 627-653-657-567-367-137-137-136-136-173-137-263-236-321-136-315-356-256-532-576-576-175-716-137-157-165-267-627-627-627-627-261-267-621-267-256-567-567-526-567-526-567-673-361-3_W.mp3)
  Segment 47: 72496ms ~ 73072ms (file: 657-625-627-672-762-672-267-267-627-621-127-123-231-321-265-261-236-236-216-213-216-265-251-217-275-761-673-627-132-265-216-217-210-517-532-157-213-123-125-671-671-675-567-536-6_W.mp3)
  Segment 48: 74072ms ~ 74648ms (file: 261-126-106-601-510-310-132-136-173-175-617-162-510-157-712-310-130-162-216-123-136-126-126-321-621-621-217-217-260-312-123-261-126-312-162-126-263-162-362-136-321-617-162-126-6_W.mp3)
  Segment 49: 75648ms ~ 76224ms (file: 132-127-126-263-236-216-267-267-263-216-231-261-236-261-261-261-216-261-265-261-216-216-213-213-216-126-263-261-261-267-216-267-261-216-267-276-267-276-267-267-265-267-267-267-2_W.mp3)
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
> **Time**: 2026-03-31 05:57:17
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "오늘 인터뷰 준비는 많이 하셨나요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "인터뷰", "start_ms": 165, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "준비는", "start_ms": 325, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 485, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "하셨나요?", "start_ms": 535, "dur_ms": 41, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "네, 긴장되지만 최선을 다해 준비했습니다.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 30, "dur_ms": 60, "phase_lr_deg": -5.2},
{"word": "긴장되지만", "start_ms": 100, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "최선을", "start_ms": 260, "dur_ms": 110, "phase_lr_deg": -5.3},
{"word": "다해", "start_ms": 380, "dur_ms": 90, "phase_lr_deg": -5.1},
{"word": "준비했습니다.", "start_ms": 480, "dur_ms": 96, "phase_lr_deg": -5.2}
]
},
{
"segment": 3,
"transcript": "편안하게 마음먹으세요. 잘 하실 거예요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "편안하게", "start_ms": 60, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "마음먹으세요.", "start_ms": 200, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 390, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "하실", "start_ms": 460, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "거예요.", "start_ms": 520, "dur_ms": 56, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "I hope I don't make any mistakes.",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 40, "dur_ms": 40, "phase_lr_deg": -4.8},
{"word": "hope", "start_ms": 90, "dur_ms": 90, "phase_lr_deg": -4.9},
{"word": "I", "start_ms": 190, "dur_ms": 40, "phase_lr_deg": -4.7},
{"word": "don't", "start_ms": 240, "dur_ms": 80, "phase_lr_deg": -4.8},
{"word": "make", "start_ms": 330, "dur_ms": 90, "phase_lr_deg": -4.9},
{"word": "any", "start_ms": 430, "dur_ms": 60, "phase_lr_deg": -4.7},
{"word": "mistakes.", "start_ms": 500, "dur_ms": 76, "phase_lr_deg": -4.8}
]
},
{
"segment": 5,
"transcript": "실수해도 괜찮아요. 진정성이 중요하니까요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "실수해도", "start_ms": 50, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "괜찮아요.", "start_ms": 190, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "진정성이", "start_ms": 310, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "중요하니까요.", "start_ms": 460, "dur_ms": 116, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "좋은 말씀 감사합니다. 자신감이 생기네요.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 35, "dur_ms": 90, "phase_lr_deg": -5.0},
{"word": "말씀", "start_ms": 135, "dur_ms": 100, "phase_lr_deg": -5.1},
{"word": "감사합니다.", "start_ms": 245, "dur_ms": 150, "phase_lr_deg": -4.9},
{"word": "자신감이", "start_ms": 405, "dur_ms": 110, "phase_lr_deg": -5.2},
{"word": "생기네요.", "start_ms": 525, "dur_ms": 51, "phase_lr_deg": -5.0}
]
},
{
"segment": 7,
"transcript": "곧 인터뷰 시작입니다. 들어가 볼까요?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "곧", "start_ms": 60, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "인터뷰", "start_ms": 130, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "시작입니다.", "start_ms": 260, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "들어가", "start_ms": 410, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "볼까요?", "start_ms": 520, "dur_ms": 56, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "Yes, I'm ready. Let's go.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Yes,", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": -4.5},
{"word": "I'm", "start_ms": 110, "dur_ms": 60, "phase_lr_deg": -4.6},
{"word": "ready.", "start_ms": 180, "dur_ms": 110, "phase_lr_deg": -4.4},
{"word": "Let's", "start_ms": 300, "dur_ms": 110, "phase_lr_deg": -4.5},
{"word": "go.", "start_ms": 420, "dur_ms": 156, "phase_lr_deg": -4.6}
]
},
{
"segment": 9,
"transcript": "긴장 풀고 평소처럼만 하세요.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "긴장", "start_ms": 55, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "풀고", "start_ms": 155, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "평소처럼만", "start_ms": 275, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "하세요.", "start_ms": 465, "dur_ms": 111, "phase_lr_deg": 0.0}
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
"transcript": "생각보다 분위기가 부드러웠어요.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "생각보다", "start_ms": 30, "dur_ms": 130, "phase_lr_deg": -5.4},
{"word": "분위기가", "start_ms": 170, "dur_ms": 130, "phase_lr_deg": -5.3},
{"word": "부드러웠어요.", "start_ms": 310, "dur_ms": 266, "phase_lr_deg": -5.5}
]
},
{
"segment": 12,
"transcript": "거봐요, 제가 잘할 거라고 했잖아요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "거봐요,", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 170, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "잘할", "start_ms": 260, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "거라고", "start_ms": 380, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "했잖아요.", "start_ms": 480, "dur_ms": 96, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "결과가 어떻게 나올지 궁금하네요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "결과가", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": -5.1},
{"word": "어떻게", "start_ms": 170, "dur_ms": 110, "phase_lr_deg": -5.2},
{"word": "나올지", "start_ms": 290, "dur_ms": 110, "phase_lr_deg": -5.0},
{"word": "궁금하네요.", "start_ms": 410, "dur_ms": 166, "phase_lr_deg": -5.1}
]
},
{
"segment": 14,
"transcript": "최선을 다했으니 좋은 결과 있을 거예요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "최선을", "start_ms": 25, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "다했으니", "start_ms": 145, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 285, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "결과", "start_ms": 385, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "있을", "start_ms": 485, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "거예요.", "start_ms": 545, "dur_ms": 31, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "Everything will be fine. Don't worry.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "Everything", "start_ms": 45, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "will", "start_ms": 235, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "be", "start_ms": 325, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "fine.", "start_ms": 395, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "Don't", "start_ms": 515, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "worry.", "start_ms": 565, "dur_ms": 11, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "네, 홀가분하네요. 이제 밥 먹으러 가요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 60, "dur_ms": 60, "phase_lr_deg": -4.2},
{"word": "홀가분하네요.", "start_ms": 130, "dur_ms": 180, "phase_lr_deg": -4.3},
{"word": "이제", "start_ms": 320, "dur_ms": 90, "phase_lr_deg": -4.1},
{"word": "밥", "start_ms": 420, "dur_ms": 60, "phase_lr_deg": -4.2},
{"word": "먹으러", "start_ms": 490, "dur_ms": 60, "phase_lr_deg": -4.3},
{"word": "가요.", "start_ms": 560, "dur_ms": 16, "phase_lr_deg": -4.2}
]
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
"transcript": "점심 메뉴는 정하셨나요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "점심", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "메뉴는", "start_ms": 170, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "정하셨나요?", "start_ms": 290, "dur_ms": 286, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "비빔밥 어때요? 근처에 맛집이 있어요.",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "비빔밥", "start_ms": 35, "dur_ms": 130, "phase_lr_deg": 2.5},
{"word": "어때요?", "start_ms": 175, "dur_ms": 110, "phase_lr_deg": 2.6},
{"word": "근처에", "start_ms": 295, "dur_ms": 110, "phase_lr_deg": 2.4},
{"word": "맛집이", "start_ms": 415, "dur_ms": 110, "phase_lr_deg": 2.5},
{"word": "있어요.", "start_ms": 535, "dur_ms": 41, "phase_lr_deg": 2.4}
]
},
{
"segment": 20,
"transcript": "Sounds good. Healthy choice for today.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Sounds", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "good.", "start_ms": 160, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "Healthy", "start_ms": 260, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "choice", "start_ms": 380, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "for", "start_ms": 490, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "today.", "start_ms": 550, "dur_ms": 26, "phase_lr_deg": 0.0}
]
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
"transcript": "여기 비빔밥은 채소가 정말 신선해요.",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "여기", "start_ms": 60, "dur_ms": 90, "phase_lr_deg": 2.8},
{"word": "비빔밥은", "start_ms": 160, "dur_ms": 130, "phase_lr_deg": 2.7},
{"word": "채소가", "start_ms": 300, "dur_ms": 110, "phase_lr_deg": 2.9},
{"word": "정말", "start_ms": 420, "dur_ms": 90, "phase_lr_deg": 2.8},
{"word": "신선해요.", "start_ms": 520, "dur_ms": 56, "phase_lr_deg": 2.9}
]
},
{
"segment": 23,
"transcript": "고소한 참기름 냄새도 너무 좋네요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "고소한", "start_ms": 35, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "참기름", "start_ms": 155, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "냄새도", "start_ms": 285, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 405, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "좋네요.", "start_ms": 505, "dur_ms": 71, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "I'll add more red chili paste.",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "I'll", "start_ms": 45, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "add", "start_ms": 135, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "more", "start_ms": 255, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "red", "start_ms": 355, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "chili", "start_ms": 425, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "paste.", "start_ms": 515, "dur_ms": 61, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "너무 매우면 속 쓰릴 수도 있어요.",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "너무", "start_ms": 30, "dur_ms": 90, "phase_lr_deg": 3.0},
{"word": "매우면", "start_ms": 130, "dur_ms": 110, "phase_lr_deg": 3.1},
{"word": "속", "start_ms": 250, "dur_ms": 60, "phase_lr_deg": 2.9},
{"word": "쓰릴", "start_ms": 320, "dur_ms": 90, "phase_lr_deg": 3.0},
{"word": "수도", "start_ms": 420, "dur_ms": 70, "phase_lr_deg": 3.1},
{"word": "있어요.", "start_ms": 500, "dur_ms": 76, "phase_lr_deg": 2.9}
]
},
{
"segment": 26,
"transcript": "걱정 마세요. 전 매운 거 잘 먹거든요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "걱정", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "마세요.", "start_ms": 160, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "전", "start_ms": 280, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "매운", "start_ms": 350, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 440, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 490, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "먹거든요.", "start_ms": 550, "dur_ms": 26, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "다 드셨으면 시원한 차 한잔할까요?",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "다", "start_ms": 40, "dur_ms": 40, "phase_lr_deg": 4.5},
{"word": "드셨으면", "start_ms": 90, "dur_ms": 140, "phase_lr_deg": 4.6},
{"word": "시원한", "start_ms": 240, "dur_ms": 120, "phase_lr_deg": 4.4},
{"word": "차", "start_ms": 370, "dur_ms": 60, "phase_lr_deg": 4.5},
{"word": "한잔할까요?", "start_ms": 440, "dur_ms": 136, "phase_lr_deg": 4.6}
]
},
{
"segment": 28,
"transcript": "좋아요. 근처에 분위기 좋은 카페 알아요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "좋아요.", "start_ms": 60, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "근처에", "start_ms": 180, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "분위기", "start_ms": 300, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 420, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "카페", "start_ms": 510, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "알아요.", "start_ms": 575, "dur_ms": 1, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "I love the coffee there. Let's go.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 35, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "love", "start_ms": 105, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 225, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "coffee", "start_ms": 275, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "there.", "start_ms": 395, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "Let's", "start_ms": 515, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "go.", "start_ms": 575, "dur_ms": 1, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 31,
"transcript": "벌써 퇴근할 시간이네요. 수고하셨습니다.",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "벌써", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 5.1},
{"word": "퇴근할", "start_ms": 170, "dur_ms": 120, "phase_lr_deg": 5.2},
{"word": "시간이네요.", "start_ms": 300, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "수고하셨습니다.", "start_ms": 460, "dur_ms": 116, "phase_lr_deg": 5.1}
]
},
{
"segment": 32,
"transcript": "네, 다들 조심히 들어가세요. 내일 봬요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 45, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "다들", "start_ms": 115, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "조심히", "start_ms": 215, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "들어가세요.", "start_ms": 335, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "내일", "start_ms": 475, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "봬요.", "start_ms": 565, "dur_ms": 11, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "Have a nice evening. See you tomorrow.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Have", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 160, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "nice", "start_ms": 210, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "evening.", "start_ms": 330, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "See", "start_ms": 450, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 520, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "tomorrow.", "start_ms": 570, "dur_ms": 6, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "안녕히 계세요!",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 65, "dur_ms": 230, "phase_lr_deg": 5.4},
{"word": "계세요!", "start_ms": 305, "dur_ms": 271, "phase_lr_deg": 5.5}
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
"transcript": "오늘 회의 자료는 다 준비됐나요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "회의", "start_ms": 170, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "자료는", "start_ms": 300, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 420, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "준비됐나요?", "start_ms": 470, "dur_ms": 106, "phase_lr_deg": 0.0}
]
},
{
"segment":

---
*Generated by WKAppBot ask gemini — 2026-03-31 05:57:17*
