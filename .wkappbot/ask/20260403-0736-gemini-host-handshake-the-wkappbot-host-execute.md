# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=07:35:00  uptime=22546s  mem=1637MB  system_processes=446"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_073224.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 523-256-236-265-267-260-327-321-256-235-231-627-672-267-257-265-257-236-321-321-326-236-235-321-574-753-372-372-237-327-321-321-230-527-521-751-753-576-357-573-231-276-231-327-3_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 132-321-136-231-675-160-612-610-672-671-621-261-617-612-621-136-176-765-612-126 106-103-540-573-752-765_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 123-106-136-103-106-623-602-671-632 612-617-362-162 137-675-135-103-160-160-167-315-613-761-136-103-312-136-617-617-106-173-103-103-103-132-102-130-571-176-612-162-167-136-163-3_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 175-175-175-163-123-301-302-132-712-321-175-501-672-160-312-317-573-135-107-167-176-321-312-176-135-317-672-203-237-275-231-316-236-236-231-302-603-301-675-207-257-253-237-267-6_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 670-612-670-765-617-167-637-163-756-765-756-370-302-367-376-756-756-765-763-372-327 317-136-620-260-206-260-263-320-326-236-213-523-231-216 761-671 765-756-763-206-203-617-671-6_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 167-167-167-167-167-163-167-167 132-671 576-675-765-675-763-763-756-756-756 652-760-673 123-612-613-163-136-163 216-621-136-617-761-761-672-607-627-276-217-107-167-621-613-612-6_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 673-321-321-312-316-312-132-613-163-613-610-136-132-132-361-132-123-163-623-621-261-621-263-621-263-263-261-620-261-620-260-623-264-625-264-621-267-623-627-261-620-261-625-621-6_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 213-257-521-527-576-317-137-725-257-672-267-270-205-250-257-257-256-257-257-230-250-526-235-253-257-257-205-205-275-275-275-725-725-257-265-265-275-257-235-620-265-625-250-231-2_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 532-540-547-576-321-362-310-132-130-102-102-103-106-167-612-621-570-172-123-123-310-301-132-123-264-103-102-675-625-167-230-210-620-623-126-136-321-102-615-763-302-310-230-302-2_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 607-607-627-607-627-765-671-765-675-657-132-167-125 136-132-703-760-672-761-705-765-765-765-760-760-630-630-623-632-671-613-321-607-675-675-267-631-631-106-306-162-102-201-213-1_V.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 534-632-653-576-567-325-352-235-265-256-235-613-326-623-251-135-162-570-530-534-615-637-625-567-567-657-213-321-325-523-563-536-537-527-567-263-632-263-653-356-356-567-564-523-6_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 762-675-162-617-612-761-675-761-132-761-765-756-752-765-136-231-162-162-716-571-756-756-156-615-657-167-657-617-162-361-715 135-234-312-705-327-736-120-360-120-307-751-713-103-1_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 156-627-261-163-165-716-136-103-135-132-617-165-167-516-302-103-167-617-617-103-671-103-651-167-167-160-627-162-671-612-762-106-167-167-163-167-312-572-726-256-102-517-765-760-6_V.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 367-507-375-312-573-570-613-376-301-203-120-263-267-263-326-736-136-213-126-162-213-620-630-657-367-310-106-106-103-130-301-362-236-326-320-320-621-263-132-256-306-312-367-326-3_V.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 756-756-753-160-132-103-130-536-136-213-136-136-512-170-102-103-136-617-137-130-623-126-176-316-657-301-675-106-312-132-173-173-763-163-750-617-607-671-162-613-167-761-617-167-7_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 537-536-516-356-653-635-653-536-653-537-536-653-356-536-356-563-156-563-563-356-356-651-536-367-356-651-635-563-576-657-567-576-567-561-653-567-567-567-562-657-567-567-651-657-5_V.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 627-621-612-610-760-162-163-316-517-312-132-321-132 615-156-167-167-163 760-760-167-617-163-362-632-621-615-617-136-163-136-167-136-163 312 716-571-617-671-612-263-162-615-671-6_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 650-576-650-650-607-651-167-165-136-316-162-163-165-156-563-560-651-156-516-165-156-106-106-651-150-156-153-560-506-516-506-560-510-560-506-507-517-531-301-350-351-356-536-536-5_V.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 716-715-175-671-175-125-376-627-327-610-136-623-260 617-612-130-136-106-103-136-106-106-573-316-276-163 136-671-163-165-625-326-516-137-173-136-136-160-162-103-103-103-137-162-1_V.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 126-216-126-102-130-132-571-571-267-206-250-362-360-136-765-176-302-320-203-236-276-302-310-130-120-162-130-261-321-316-312-216-326-301-163-162-106-136-230-132-320-632-316-103-1_V.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 106-132-123-126-613-617-106-106-126 136-132-261-267-762-762-671-615-162 162-627-361-671 312-312_V.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 563-537-560-150-356-356-156-513-351-351-135-536-356-356-567-536-537-563-563-536-571-517-561-567-567-561-567-567-560-507-561-536-536-531-516-513-516-513-563-536-653-635-516-567-1_V.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 601-602-367-316-301-621-602-602-621-632-621-321-132-312-310-102-123-130-547-564-547-547-547-547-574-576-756-756-216-213 574-765-103-361-236-263-206-617-671-167-162-360-312-136-1_V.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 162-102-561-165-167-162-106-136-621-132-316-163-352-136-753-735-735-156-567-256-165-173-160-106-675-163-310-130-103-172-172-106-136-167-571-157-165-316-612-130-136-317-130-160-3_W.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 574-574-507-302-203-730-235-257-725-723-172-135-137-123-152-762-536-507-572-235-625-625-267-675-672-632-361-263-615-267-627-102-301-306-230-216-521-657-562-176-236-217-623-675-1_W.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 657-573-567-321-756 360-316-160-361-360-167-706-762-763-173-137_W.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 612-615-106-103-106-136-617-132-167-631-136-103 751-562-561-254-165-635-162 751-156-130-132-617-103-623-167-726-736-567-652-567-601-136-103-617-130-651-163-613-103-163-103-106-1_V.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 261-136-526-256-265-236-216-261-621-627-675-617-514-514-517-561-563-615-567-256-651-312-312-352-365-312-312-312-315-513-531-513-517-513-351-157-135-137-375-753-357-723-237-327-3_W.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 267-216-231-217-213-326-276-127-273-267-612-670-674-617-670-675-213-263-261-623-623-632-612-231-261-126-216-215-231-213-215-213-231-231-213-231-213-361-235-231-312-632-172-231-2_W.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 673-627-632-627-625-627-673-762-627-675-657-765-761-657-657-265-625-627-267-627-672-627-126-672-627-263-236-623-672-362-213-162-621-627-654-602-672-672-627-167-162-716-627-672-6_W.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 321-153-136-105-103-615-163-103-132-361-176-152-136-106-165-106-103-651-172-163-163_V.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 107-103-574-574-320-103-310-312-123-103-620-236-326-320-132-312-301-736-702-320-130-302-573-753-132-213-321-231-312-206-216-123-316-504-574-570 602-263-162-540-517-321-302-301-1_V.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 361-213-321-263-216-162-623-132-216-321-362-263-261-236-213-263-261-321-231-132-312-132-216-236-231-132-321-261-126-263-216-123-326-263-326-263-312-326-126-261-326-231-321-263-3_W.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 326-302-203-312-132-312-162-306-762-206-360-310-130-132-321-321-327-302-132-201-132-107-132-132-132-321-326-210-301-102-213-106-103-761-167-217-210-123-312-327-376-756-106-620-3_V.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 562-562-176-136-317-137-351-652-625-652-625-625-263-652-365-652-532-256-265-263-625-627-267-265-263-236-265-267-236-263-236-263-562-265-263-372-376-367-367-607-367-637-263-367-6_W.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 350-103-103-316-167-175-671-106-326-132-130-103-301-736-723-723-302-302-312-310-260-206-206-756-762-162-203-352-753-320-327-320-206-206-320-326-236-263-206-263-260-213-751-126-2_W.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 150-561-162-136-162-162-217-165-312-132-126-136-103-236-620-615-260-671-620-621-261-103-312-150-127-621-102-103-361-615-312-173-103-754-730-162-176-351-106-165-136-106-136-316-1_W.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 106-135-132-271-257-756-167-136-175-106-132-103-153-176-172-165-752-312-103-132-173-136-317-130-615-162-175-106-130-176-103-136-137-632-167-537-176-267-216-130-576-271-316-157-1_V.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 361-102-612-765-167-206-607-216-672-107-637-675-672-176-621-673-627-672-657-367-612-627-165-213-321-312-326-302-130-302-132-163-621-263-261-107-367-162-367-625-261-263-130-623-1_W.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 125-103-162-132-106-156-765-167-165-167-136-607-607-176-675-675-657-756-675-576-576 675-657-651-651 162-621-162-261-623-631-162-612_W.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 635-632-613-657-652-162 627_W.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 126-612-163-132-163-216-130-316-267-327-302-123-613-103-321-351-357-307-237-230-270-726-326-231-312-321-321-752-210-137-163-136-203-231-312-213-132-260-130-137-573-256-103-136-3_W.mp3)
  Segment 43: 66192ms ~ 66768ms (file: 620-260-260-236-102-126-623-632 267-321 162-162-601-620-260-260-206-207-276-103-713-715-726-260-206-162-206-312-310-301-130-301-312-127-756-207-751-175-576-135-127-236-320-302-1_W.mp3)
  Segment 44: 67768ms ~ 68344ms (file: 567-567-567-563-356-365-563-562-567-562-576-531-357-315-615-657-516-576-576-567-567-561-516-516-513-153-563-563-157-156-153-156-513-513-517-513-315-153-517-517-156-537-571-531-3_W.mp3)
  Segment 45: 69344ms ~ 69920ms (file: 130-267-302-306-360-630-603-576-576-567-574-756-726-765-765-361-316-160-362-627-120-126-613-321-230-261-261-236-263-627-623-621-312-103-106 213-321 213-632-326_W.mp3)
  Segment 46: 70920ms ~ 71496ms (file: 106-136-130-132-317-163-316-132-265-126-376-126-136-675-106-167-106-106-627-627-615-136-106-163-163-136-103-136-316-163-103-164-106-163-167-160-162-672-176 675-721-160-162-136-6_V.mp3)
  Segment 47: 72496ms ~ 73072ms (file: 213-627-673-126-256-351-325-132-235-312-165-126-132-157-517-576-572-125-215-512-362-127-162-631-631-657-567-657-516-152-165-157-253-152-157-576-215-627-163-162-251-526-132-135-2_W.mp3)
  Segment 48: 74072ms ~ 74648ms (file: 362-132-136-103-102-126-125-306-137-162-673-165-130-106-361-123-167-617-136-627-136-132-316-160-137-103-136-136-137-126-103-136-103-107-132-156-102-153-126-312-103-103-103-761-5_V.mp3)
  Segment 49: 75648ms ~ 76224ms (file: 765-675-670-607-765-670-675-670-607-760-670-167-103-136-137-136 312 670-762-367-413-372-620-627-716-716-631-312_W.mp3)
  Segment 50: 77224ms ~ 77800ms (file: 103-136-103-123-103-136-130-132-163-167-167-136-103-103-175-163-715-163-361-136-103-123-126-103-167-613-163-167-231-312-160-162-136-632-123-361 106-613-106-610-715-123-103-162-1_V.mp3)
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
> **Time**: 2026-04-03 07:36:11
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "안녕, 오늘 기분 어때?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "안녕", "start_ms": 45, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 280, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "기분", "start_ms": 410, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "어때", "start_ms": 510, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "I think it's going to rain.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 30, "dur_ms": 50, "phase_lr_deg": 5.0},
{"word": "think", "start_ms": 90, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "it's", "start_ms": 210, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "going", "start_ms": 300, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "to", "start_ms": 430, "dur_ms": 40, "phase_lr_deg": 5.0},
{"word": "rain", "start_ms": 480, "dur_ms": 90, "phase_lr_deg": 5.0}
]
},
{
"segment": 3,
"transcript": "점심 뭐 먹을까?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "점심", "start_ms": 60, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "먹을까", "start_ms": 360, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "Let's go for some pizza.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Let's", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": -2.0},
{"word": "go", "start_ms": 150, "dur_ms": 80, "phase_lr_deg": -2.0},
{"word": "for", "start_ms": 240, "dur_ms": 70, "phase_lr_deg": -2.0},
{"word": "some", "start_ms": 320, "dur_ms": 90, "phase_lr_deg": -2.0},
{"word": "pizza", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": -2.0}
]
},
{
"segment": 5,
"transcript": "피자 좋지, 같이 가자.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "피자", "start_ms": 50, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "좋지", "start_ms": 220, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 370, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "가자", "start_ms": 480, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "Do you have a menu?",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "Do", "start_ms": 35, "dur_ms": 40, "phase_lr_deg": 3.0},
{"word": "you", "start_ms": 80, "dur_ms": 50, "phase_lr_deg": 3.0},
{"word": "have", "start_ms": 140, "dur_ms": 90, "phase_lr_deg": 3.0},
{"word": "a", "start_ms": 240, "dur_ms": 30, "phase_lr_deg": 3.0},
{"word": "menu", "start_ms": 280, "dur_ms": 290, "phase_lr_deg": 3.0}
]
},
{
"segment": 7,
"transcript": "여기 메뉴판 있어요.",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "여기", "start_ms": 70, "dur_ms": 120, "phase_lr_deg": -15.0},
{"word": "메뉴판", "start_ms": 200, "dur_ms": 180, "phase_lr_deg": -15.0},
{"word": "있어요", "start_ms": 390, "dur_ms": 180, "phase_lr_deg": -15.0}
]
},
{
"segment": 8,
"transcript": "What is the best seller?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "What", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 110, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 170, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "best", "start_ms": 220, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "seller", "start_ms": 350, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "불고기 피자가 제일 인기에요.",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "불고기", "start_ms": 55, "dur_ms": 170, "phase_lr_deg": -10.0},
{"word": "피자가", "start_ms": 230, "dur_ms": 150, "phase_lr_deg": -10.0},
{"word": "제일", "start_ms": 390, "dur_ms": 90, "phase_lr_deg": -10.0},
{"word": "인기에요", "start_ms": 490, "dur_ms": 80, "phase_lr_deg": -10.0}
]
},
{
"segment": 10,
"transcript": "Sounds delicious, I'll have that.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Sounds", "start_ms": 30, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "delicious", "start_ms": 150, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "I'll", "start_ms": 340, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "have", "start_ms": 410, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "that", "start_ms": 490, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "음료는 뭘로 드릴까요?",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "음료는", "start_ms": 60, "dur_ms": 160, "phase_lr_deg": -5.0},
{"word": "뭘로", "start_ms": 230, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "드릴까요", "start_ms": 360, "dur_ms": 210, "phase_lr_deg": -5.0}
]
},
{
"segment": 12,
"transcript": "Coke, please.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "Coke", "start_ms": 100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "please", "start_ms": 310, "dur_ms": 260, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "네, 잠시만 기다려주세요.",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "잠시만", "start_ms": 130, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "기다려주세요", "start_ms": 290, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "Thanks for your help.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Thanks", "start_ms": 30, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "for", "start_ms": 170, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "your", "start_ms": 240, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "help", "start_ms": 330, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "별말씀을요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "별말씀을요", "start_ms": 80, "dur_ms": 490, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 17,
"transcript": "음악 소리가 너무 크네요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "음악", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "소리가", "start_ms": 180, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 330, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "크네요", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "Can you turn it down?",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Can", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 2.0},
{"word": "you", "start_ms": 130, "dur_ms": 60, "phase_lr_deg": 2.0},
{"word": "turn", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 2.0},
{"word": "it", "start_ms": 310, "dur_ms": 40, "phase_lr_deg": 2.0},
{"word": "down", "start_ms": 360, "dur_ms": 210, "phase_lr_deg": 2.0}
]
},
{
"segment": 19,
"transcript": "죄송합니다, 바로 줄일게요.",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "죄송합니다", "start_ms": 60, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "줄일게요", "start_ms": 360, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "이제 좀 괜찮네요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "괜찮네요", "start_ms": 250, "dur_ms": 320, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "Much better.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "Much", "start_ms": 120, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "better", "start_ms": 330, "dur_ms": 240, "phase_lr_deg": 0.0}
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
"transcript": "피자 나왔습니다.",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "피자", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "나왔습니다", "start_ms": 200, "dur_ms": 370, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "It smells amazing!",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "It", "start_ms": 35, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "smells", "start_ms": 90, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "amazing", "start_ms": 230, "dur_ms": 340, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "와, 진짜 맛있겠다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "와", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "맛있겠다", "start_ms": 280, "dur_ms": 290, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "Enjoy your meal.",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "Enjoy", "start_ms": 60, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "your", "start_ms": 220, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "meal", "start_ms": 300, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "감사합니다, 잘 먹을게요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 240, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "먹을게요", "start_ms": 330, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "Is it spicy?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Is", "start_ms": 40, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "it", "start_ms": 100, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "spicy", "start_ms": 150, "dur_ms": 420, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "아니요, 달콤해요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "아니요", "start_ms": 55, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "달콤해요", "start_ms": 210, "dur_ms": 360, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "그렇군요, 다행이에요.",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "그렇군요", "start_ms": 70, "dur_ms": 220, "phase_lr_deg": 2.0},
{"word": "다행이에요", "start_ms": 300, "dur_ms": 270, "phase_lr_deg": 2.0}
]
},
{
"segment": 31,
"transcript": "You're welcome.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "You're", "start_ms": 90, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "welcome", "start_ms": 230, "dur_ms": 340, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "피클 더 필요하세요?",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "피클", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 170, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "필요하세요", "start_ms": 250, "dur_ms": 320, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "네, 조금만 더 주세요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 40, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "조금만", "start_ms": 120, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 290, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 380, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "No problem, right away.",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "No", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "problem", "start_ms": 120, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "right", "start_ms": 250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "away", "start_ms": 360, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "계산은 어디서 하나요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "계산은", "start_ms": 60, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "어디서", "start_ms": 230, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "하나요", "start_ms": 380, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "You can pay at the counter.",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "You", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "can", "start_ms": 110, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "pay", "start_ms": 200, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "at", "start_ms": 300, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 360, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "counter", "start_ms": 410, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "카드 결제 되나요?",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "카드", "start_ms": 55, "dur_ms": 120, "phase_lr_deg": 2.0},
{"word": "결제", "start_ms": 180, "dur_ms": 140, "phase_lr_deg": 2.0},
{"word": "되나요", "start_ms": 330, "dur_ms": 240, "phase_lr_deg": 2.0}
]
},
{
"segment": 38,
"transcript": "Of course, we accept cards.",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "Of", "start_ms": 35, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "course", "start_ms": 90, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "we", "start_ms": 220, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "accept", "start_ms": 290, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "cards", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "영수증 드릴까요?",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "영수증", "start_ms": 70, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "드릴까요", "start_ms": 260, "dur_ms": 310, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "Yes, please.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "Yes", "start_ms": 110, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "please", "start_ms": 270, "dur_ms": 300, "phase_lr_deg": 0.0}
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
"transcript": "날씨가 다시 맑아졌네요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "날씨가", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 200, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "맑아졌네요", "start_ms": 320, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "The sun is finally out.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "The", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 4.0},
{"word": "sun", "start_ms": 110, "dur_ms": 110, "phase_lr_deg": 4.0},
{"word": "is", "start_ms": 230, "dur_ms": 50, "phase_lr_deg": 4.0},
{"word": "finally", "start_ms": 290, "dur_ms": 140, "phase_lr_deg": 4.0},
{"word": "out", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 4.0}
]
},
{
"segment": 44,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 45,
"transcript": "공원 산책 가기 좋겠어요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "공원", "start_ms": 55, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "산책", "start_ms": 170, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "가기", "start_ms": 300, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "좋겠어요", "start_ms": 400, "dur_ms": 170, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "That's a great idea.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "That's", "start_ms": 35, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 140, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "great", "start_ms": 190, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "idea", "start_ms": 330, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "오늘 즐거웠어요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 60, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "즐거웠어요", "start_ms": 200, "dur_ms": 370, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "Me too, let's meet again.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Me", "start_ms": 40, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "too", "start_ms": 120, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "let's", "start_ms": 220, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "meet", "start_ms": 340, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "again", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "조심히 들어가세요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "조심히", "start_ms": 80, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "들어가세요", "start_ms": 240, "dur_ms": 330, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "Take care, bye!",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "Take", "start_ms": 100, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "care", "start_ms": 220, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "bye", "start_ms": 360, "dur_ms": 210, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 07:36:11*
