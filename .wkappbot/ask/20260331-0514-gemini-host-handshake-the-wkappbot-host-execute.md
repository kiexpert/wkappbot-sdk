# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43304  time=05:13:10  uptime=10178s  mem=1631MB  system_processes=440"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260331_051300.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 673-672-673-567-576-567-576 675-762-765_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 312-132-765-756-756-756-260-236-263-564-516-136-103-260-260-120-126-236-517-137-375-713-305-651-162-106-136-163-176-610-351-561-657-365-136-136-167-127-165-137-160-672-103-126-1_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 132-301-627-621-613-751-576-576-103-621-106-316-136-312-632-261 751-106-615-107-162-623-673-103-163-312-132-137-512-562-163-107-132-103-103-103-162-163-261-163-613-160-613-163-6_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 621-632-261-265-263-756-756-276-261-236-321-236-236-263-231-236-263-627 627 326-236-236-263 261-261-216-276-762-762-672-762-260-236-326-321-326-326-302-326-312-321-326-621-612 3_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 106-163-106-136-163-164-163-106-301-316-206-206-216 106 765-675-672-765-103-167-107-167-167-160-176-106-671-716-671-762-673-762-761-760-601-673-763-675-673-765-673-673-673-673-3_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 123-312-103-310-102-132-310-321-130-130-130-103-316-216-260-236-312-213-231-213_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 132-103-107-106-102-103-136-126-136-376-162-136-130-216-267-672 672-671-617-612_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 620-620-620-620-602-620-602-620-620-620-263-601-610-610-621-632-362-326-106-106-162-670-675-607-602-607-106-672-627-620-627-620-260-106-106-106-136-206-206-207-230-203-203 261-2_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 537-752-752-574-365-157-362-623-563-564-564-256-216-526-527-752-536-576-526-265-652-627-627-267-267-267-762-215-237-547-562-652-637-762-153-310-316-612-625-564-251-267-261-275-2_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 627-627-627-261-675-613-136-507-576-135-617-136-167-162-130-160-167-136-713-130-123-623-126-206-261-302-263-103-136-613-157-263-160-136-103-576-103-716 167-610-167-106-510-567-6_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 302-302-375-735-732 263-623-126-236-261-263-263-263-236-263-236 672-367-263-632-237-367 762-762-267-263-236-263-310_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 762-672-276-267-276-263-276-263-267-236-267-276-627-263-261-263-623 574-547_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 103-672-612-136-103-571-572-561-167-103-326-102-163-136-601-136-263-632-316-126-136-136-132-263-263_V.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 623-621-130-135-612-162-612-165-164-162-623-765-675-123-627_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 672-672-627-627-621-672-261-627-623-621-617-612-261-162_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 136-123-271-321-165-625-362-267-261-217-361-623-617-312_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 540-547-352-312-213-213-521-532-325-236-231-261-326-326-327-321-231-302-320-527-325-235-230-206-253-326-236-265-267-265-236-236-362-631-236-263-236-326-253-523-256-325-362-361-3_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 263-231-623-762-726-267-237-627-623-263-263-621-627-263-627-672-267-721-126-236-326-273-621-602-326-627-312-231-132-623-263-267-623-762-267-267-632-623-627-326-263-216-672-627-6_W.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 576-362-612-537-321-632-762-672-612-516-615-576-753-576-576-672-627-576-576-547-567-576-576-657-567-657-627-627-672-765-675-547-576-574-576-576 762-672-762-756-567-756-765-756-7_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 356-352-365-357-536-567-561-516-567-516-567-567-756-567-567-567-567-657-567-567-567-567-567-517-567-567-576-615-165-561-756-657-561-567-321-123-572-562-576-576-567-567-516-157-6_W.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 134 675-621-610-617-612-613-612-136-671-164-264-167 421 267-627-765-675-651-167-167-126-267-617-316-361-310-621-312-236-263-276-376 623-263-627-263-637-675-621-632-613-621-632-1_W.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 236-321-365-365-516-256-513-516-512-526-152-153-125-165-156-150-153-516-513-153-153-165-163-165-156-156-156-165-156-153-153-513-512-153-150-516-561-513-125-152-513-510-527-526-3_W.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 210-231-132-123-125-132 652-361-175-751-741-256-652-265-256-257-572-527-250-765-256-576-576-574-547-237-172-127-724-371-170-751-731-371-713-730-762-764-670-627-627-672-625-675-6_W.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 657-765-657-567-657-567-567-657-567-657-617-567-576-576-657 657-756-657-574-567-567-576-576-567-567-576-567-576-567-567-567-576-576-567-567-675-675-621-627-156-567-516-570-756-5_W.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 621-162-136-126-312-162-162-163-136-316-132-126-216-623-362-132-312-321-312-325-321-132-536-512-531-351-315-352-532-352-321-156-315-231-312-631-317-312-312-317-326-312-317-315-3_W.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 263-265-256-261-216-263-361-316-326-326-362-236-273-726-276-271-276-237-267-235-265-265-263-263-263-261-267-256-265-625-265-625-615-256-521-325-312-132-126-327-261-231-312-713-3_W.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 762-625-276-627-625-627-567-675-672-675-756-673-672-267-675-657-567-653-657-652-267-672-563-632-627-672-672-675-675-627-765-257-625-567-657-762-725-675-576-567-657-765-672-672-6_W.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 627-765-756-756-756-765-765-675-657 574-576 567-576-675-756 657-632-623-267-216-267-675-657-637-236-321-651-576-762-623-267-326-673-765-637-321-327-163-362-317-765-672-657-126-5_W.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 675-675-765-123-312-312-635 162-316-132-236-627-167_W.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 165-126-132-126-102-103-136-312-123-132-132-136_V.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 631-263-620-162-602-506-570-623-260-126-106-103-162-103-132 574-574-576 135-156-176-620-620-670-267-627-176-571-156-157-165-103-103-567-576-357-310-705-675-167-617-671-163-160-6_V.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 263-621-627-627-621-261-261-216-126-132-312-132-312-316-215-215-261-261-623-216-132-173-167-316-130-315-310-167-163-173-163-175-175-157-137-137-127-163-130-160-173-716-137 325-2_W.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 765-567-567-756-716-761-672-674-137-316-126-213-261-236-231-216-261-263-621-261-261-256-267-261-627-627-672-267-261-623-236-263-265-632-627-267-621-612-625-623-621-625-654-625-3_W.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 173-751-325-235-625-357-326-165-176-236-267-265-527-576-574-532-576-572-256-752-175-173-173-735-567-267-173-261-367-372-504-357-326-357-375-357-756-527-502-537-507-357-573-537-3_W.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 670-263-136-516-512-165-163-136 216 672-762 627-627-263 756-576-756-756-756-756-576-751 672-621-102-762-765 213-216 547_W.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 627-652-652-365-627-632-361-163-356-256-267-267-267-265-263-261-615-265-265-316-367-357-356-376-637-312-567-526-536-367-361-613-367-635-623-627-627-263-261-236-316-316-316-213-2_W.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 162-632-605-326-123-163-613-630-236-320-326-603-576-257-523-312-130-301-102-360-362-326-320-126-162-612-162-621-612-162-216-126-162-620-621-103-103-312-230-213-201-132-237-217-3_V.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 675-657-657-635-657-653-536-563-356-651-651-567-657-657-653-317-157-567-537-517-657-567-657-567-576-567-572-652-563-631-637-561-657-567-561-563-567-576-567-567-357-617-563-637-6_W.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 163-231-127-103-132-103-301-310-301-132-312-326-261-326-613-672-316-103 103-267-206-263-532-321-132-136-301-301-312-367-136-127-167 132-321-302-320-203-326-123-103-132-136-103-1_V.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 756-756-765-763-542-526-526-526-540-531-574-571-571-576-310-310-130-547-574-576-574-265-267-326-236-632-231-256-572-576-756-576-765-153-312-105-130 265-231-752-572-627-726-627-2_W.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 102-675-631-675-672-672-167-162-106-137-631-671-167-763-673-163-631-164 756_W.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 213-132-261-276-267-621-326-263 235-235-321-321-321-321-231-321-321-321-305-327-352-375-537-573-576-572-576-536-537-315-351-351-523-516-352-356-326-325-362-325-352-352-357-315-3_W.mp3)
  Segment 43: 66192ms ~ 66768ms (file: 543-521-125-317-375-325-327-237-237-372-375-275-573-753-574-375-235-275-725-675-674-672-627-726-726-275-547-574-576-137-315-135-127-136-371-726-726-231-265-325-312-315-312-237-1_W.mp3)
  Segment 44: 67768ms ~ 68344ms (file: 163-173-263-213-316-623-367-267-617-136-216-126-136-203-571-516-675-756-627-367-613-367-623-263-261-657-527-765-567-657-675-657-652-567-135-576-576-572-156-567-567-567-567-576-5_W.mp3)
  Segment 45: 69344ms ~ 69920ms (file: 236-413-132-504-627-265-231-120-513-537-352-621-371-210-652-521-132-625-657-562-672-671-132-625-265-162-237-201-125-132-562-265-625-625-620-132-215-657-267-653-621-562-327-617-7_W.mp3)
  Segment 46: 70920ms ~ 71496ms (file: 765-765-761-765-765-765-675-765-675-756-765-761-761-761-176-631-162-103-163-317-160-160-126-275-617-167-762-312-617-103-126-317-130-621-130-132-532-162-106-163-361-103-163-136-1_W.mp3)
  Segment 47: 72496ms ~ 73072ms (file: 761-137-103-523-675-136-260-162-103-102-162-160-136-762-106-103-136-514-123-672-132-162-623-103-163-136-167-102-613-163-165-325-163-716-301-136-106-163-613-517-126-261-362-173-1_V.mp3)
  Segment 48: 74072ms ~ 74648ms (file: 267-261-215-261-251-632-613-312-316-361-623-632-236-316-613-672-621-261-361-563-367-356-635-361-617-162-167-561-516-135-236-612-621-671-675-564-635-562-265-621-612-652-512-517-5_W.mp3)
  Segment 49: 75648ms ~ 76224ms (file: 736-672-657-236-623-632-312 562-652-625-602-123-651-562-573-674-624-123-261-260-576-576-576-567-567-567-567-657-567-576-756-756-675-675-765-675-765-167-761-672-761-762-672-675-1_W.mp3)
  Segment 50: 77224ms ~ 77800ms (file: 621-625-153-167-163-160-163-613-106-136-106-136-160-130-163-163-163-612-256-126-172-136-162-625-103-136-132-130-651-136-613-136-130-163-715-123-103-316-561-632-163-312-106-163-1_V.mp3)
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
> **Time**: 2026-03-31 05:14:10
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
"transcript": "실례지만 길 좀 물어볼게요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "실례지만", "start_ms": 45, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "길", "start_ms": 235, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 325, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "물어볼게요", "start_ms": 395, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "네 말씀하세요 어디 찾으세요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": -5.2},
{"word": "말씀하세요", "start_ms": 120, "dur_ms": 210, "phase_lr_deg": -5.0},
{"word": "어디", "start_ms": 340, "dur_ms": 90, "phase_lr_deg": -5.3},
{"word": "찾으세요?", "start_ms": 440, "dur_ms": 100, "phase_lr_deg": -5.1}
]
},
{
"segment": 4,
"transcript": "이 근처에 은행이 있나요?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 60, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "근처에", "start_ms": 130, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "은행이", "start_ms": 280, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "있나요?", "start_ms": 410, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "직진해서 신호등을 건너세요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "직진해서", "start_ms": 40, "dur_ms": 210, "phase_lr_deg": -4.8},
{"word": "신호등을", "start_ms": 260, "dur_ms": 180, "phase_lr_deg": -4.9},
{"word": "건너세요", "start_ms": 450, "dur_ms": 110, "phase_lr_deg": -4.7}
]
},
{
"segment": 6,
"transcript": "얼마나 걸릴까요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "얼마나", "start_ms": 50, "dur_ms": 190, "phase_lr_deg": 0.0},
{"word": "걸릴까요?", "start_ms": 250, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "걸어서 5분 정도 걸려요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "걸어서", "start_ms": 35, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "5분", "start_ms": 195, "dur_ms": 120, "phase_lr_deg": -5.1},
{"word": "정도", "start_ms": 325, "dur_ms": 110, "phase_lr_deg": -4.9},
{"word": "걸려요", "start_ms": 445, "dur_ms": 100, "phase_lr_deg": -5.2}
]
},
{
"segment": 8,
"transcript": "Is there a subway station near here?",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "Is", "start_ms": 45, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "there", "start_ms": 135, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 225, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "subway", "start_ms": 275, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "station", "start_ms": 405, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "near", "start_ms": 515, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "here?", "start_ms": 575, "dur_ms": 0, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "네 바로 저기에 있어요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 60, "dur_ms": 110, "phase_lr_deg": -4.5},
{"word": "바로", "start_ms": 180, "dur_ms": 120, "phase_lr_deg": -4.6},
{"word": "저기에", "start_ms": 310, "dur_ms": 130, "phase_lr_deg": -4.4},
{"word": "있어요", "start_ms": 450, "dur_ms": 90, "phase_lr_deg": -4.5}
]
},
{
"segment": 10,
"transcript": "도와주셔서 감사합니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "도와주셔서", "start_ms": 30, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 360, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "천만에요 조심히 가세요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "천만에요", "start_ms": 55, "dur_ms": 230, "phase_lr_deg": -5.5},
{"word": "조심히", "start_ms": 295, "dur_ms": 140, "phase_lr_deg": -5.3},
{"word": "가세요", "start_ms": 445, "dur_ms": 100, "phase_lr_deg": -5.4}
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
"transcript": "배가 고프네요 점심 먹을까요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "배가", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "고프네요", "start_ms": 160, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "점심", "start_ms": 300, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "먹을까요?", "start_ms": 420, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "좋아요 무엇을 먹고 싶어요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "좋아요", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": -4.2},
{"word": "무엇을", "start_ms": 180, "dur_ms": 130, "phase_lr_deg": -4.3},
{"word": "먹고", "start_ms": 320, "dur_ms": 90, "phase_lr_deg": -4.1},
{"word": "싶어요?", "start_ms": 420, "dur_ms": 120, "phase_lr_deg": -4.3}
]
},
{
"segment": 15,
"transcript": "냉면 어때요? 오늘 덥잖아요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "냉면", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "어때요?", "start_ms": 200, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 320, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "덥잖아요", "start_ms": 420, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "That's a great idea!",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "That's", "start_ms": 35, "dur_ms": 110, "phase_lr_deg": -3.5},
{"word": "a", "start_ms": 155, "dur_ms": 40, "phase_lr_deg": -3.4},
{"word": "great", "start_ms": 205, "dur_ms": 130, "phase_lr_deg": -3.6},
{"word": "idea!", "start_ms": 345, "dur_ms": 180, "phase_lr_deg": -3.5}
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
"transcript": "이 집 냉면이 정말 맛있네요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 45, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "집", "start_ms": 115, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "냉면이", "start_ms": 195, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 355, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "맛있네요", "start_ms": 455, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "국물이 아주 시원해요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "국물이", "start_ms": 30, "dur_ms": 170, "phase_lr_deg": -4.0},
{"word": "아주", "start_ms": 210, "dur_ms": 110, "phase_lr_deg": -4.1},
{"word": "시원해요", "start_ms": 330, "dur_ms": 210, "phase_lr_deg": -3.9}
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
"transcript": "식후에 커피 마시러 갈까요?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "식후에", "start_ms": 60, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "커피", "start_ms": 220, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "마시러", "start_ms": 320, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "갈까요?", "start_ms": 440, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "좋아요 제가 커피 살게요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "좋아요", "start_ms": 40, "dur_ms": 130, "phase_lr_deg": -4.6},
{"word": "제가", "start_ms": 180, "dur_ms": 110, "phase_lr_deg": -4.7},
{"word": "커피", "start_ms": 300, "dur_ms": 90, "phase_lr_deg": -4.5},
{"word": "살게요", "start_ms": 400, "dur_ms": 140, "phase_lr_deg": -4.6}
]
},
{
"segment": 23,
"transcript": "아니에요 아까 길 가르쳐 주셨잖아요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "아니에요", "start_ms": 25, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "아까", "start_ms": 195, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "길", "start_ms": 295, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "가르쳐", "start_ms": 355, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "주셨잖아요", "start_ms": 475, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 25,
"transcript": "그럼 같이 반반씩 낼까요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": -5.2},
{"word": "같이", "start_ms": 160, "dur_ms": 90, "phase_lr_deg": -5.3},
{"word": "반반씩", "start_ms": 260, "dur_ms": 140, "phase_lr_deg": -5.1},
{"word": "낼까요?", "start_ms": 410, "dur_ms": 120, "phase_lr_deg": -5.2}
]
},
{
"segment": 26,
"transcript": "더치페이 하자는 말씀이죠?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "더치페이", "start_ms": 55, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "하자는", "start_ms": 275, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "말씀이죠?", "start_ms": 405, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "Yes, that's what I mean.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Yes,", "start_ms": 30, "dur_ms": 110, "phase_lr_deg": -3.8},
{"word": "that's", "start_ms": 150, "dur_ms": 110, "phase_lr_deg": -3.7},
{"word": "what", "start_ms": 270, "dur_ms": 80, "phase_lr_deg": -3.9},
{"word": "I", "start_ms": 360, "dur_ms": 60, "phase_lr_deg": -3.8},
{"word": "mean.", "start_ms": 430, "dur_ms": 130, "phase_lr_deg": -4.0}
]
},
{
"segment": 28,
"transcript": "그래요 그렇게 해요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "그래요", "start_ms": 45, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "그렇게", "start_ms": 185, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "해요", "start_ms": 305, "dur_ms": 240, "phase_lr_deg": 0.0}
]
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
"transcript": "여기 아메리카노 두 잔 주세요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "여기", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": -4.2},
{"word": "아메리카노", "start_ms": 180, "dur_ms": 180, "phase_lr_deg": -4.3},
{"word": "두", "start_ms": 370, "dur_ms": 50, "phase_lr_deg": -4.1},
{"word": "잔", "start_ms": 430, "dur_ms": 60, "phase_lr_deg": -4.2},
{"word": "주세요", "start_ms": 500, "dur_ms": 60, "phase_lr_deg": -4.3}
]
},
{
"segment": 31,
"transcript": "따뜻한 거 드릴까요 차가운 거 드릴까요?",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "따뜻한", "start_ms": 20, "dur_ms": 130, "phase_lr_deg": 2.5},
{"word": "거", "start_ms": 160, "dur_ms": 40, "phase_lr_deg": 2.6},
{"word": "드릴까요", "start_ms": 210, "dur_ms": 110, "phase_lr_deg": 2.4},
{"word": "차가운", "start_ms": 330, "dur_ms": 110, "phase_lr_deg": 2.5},
{"word": "거", "start_ms": 450, "dur_ms": 40, "phase_lr_deg": 2.6},
{"word": "드릴까요?", "start_ms": 500, "dur_ms": 60, "phase_lr_deg": 2.7}
]
},
{
"segment": 32,
"transcript": "아이스로 두 잔 주세요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "아이스로", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "두", "start_ms": 230, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "잔", "start_ms": 300, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 370, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "잠시만 기다려 주세요",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "잠시만", "start_ms": 35, "dur_ms": 210, "phase_lr_deg": 2.8},
{"word": "기다려", "start_ms": 255, "dur_ms": 130, "phase_lr_deg": 2.7},
{"word": "주세요", "start_ms": 395, "dur_ms": 160, "phase_lr_deg": 2.9}
]
},
{
"segment": 34,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 35,
"transcript": "커피 나왔습니다 맛있게 드세요",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "커피", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 3.0},
{"word": "나왔습니다", "start_ms": 170, "dur_ms": 180, "phase_lr_deg": 3.1},
{"word": "맛있게", "start_ms": 360, "dur_ms": 110, "phase_lr_deg": 2.9},
{"word": "드세요", "start_ms": 480, "dur_ms": 80, "phase_lr_deg": 3.0}
]
},
{
"segment": 36,
"transcript": "감사합니다 수고하세요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 30, "dur_ms": 210, "phase_lr_deg": -4.8},
{"word": "수고하세요", "start_ms": 250, "dur_ms": 300, "phase_lr_deg": -4.9}
]
},
{
"segment": 37,
"transcript": "커피 향이 참 좋네요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "커피", "start_ms": 60, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "향이", "start_ms": 180, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "참", "start_ms": 310, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 390, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "맞아요 마음이 편안해져요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "맞아요", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": -4.5},
{"word": "마음이", "start_ms": 190, "dur_ms": 130, "phase_lr_deg": -4.6},
{"word": "편안해져요", "start_ms": 330, "dur_ms": 220, "phase_lr_deg": -4.4}
]
},
{
"segment": 39,
"transcript": "주말에는 보통 뭐 하세요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "주말에는", "start_ms": 25, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "보통", "start_ms": 215, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 335, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "하세요?", "start_ms": 415, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "I usually go hiking.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": -3.2},
{"word": "usually", "start_ms": 140, "dur_ms": 140, "phase_lr_deg": -3.1},
{"word": "go", "start_ms": 290, "dur_ms": 110, "phase_lr_deg": -3.3},
{"word": "hiking.", "start_ms": 410, "dur_ms": 150, "phase_lr_deg": -3.4}
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
"transcript": "우와, 건강에 정말 좋겠어요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "우와,", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "건강에", "start_ms": 160, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 290, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "좋겠어요", "start_ms": 390, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "다음에 같이 갈까요?",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 60, "dur_ms": 160, "phase_lr_deg": -5.1},
{"word": "같이", "start_ms": 230, "dur_ms": 110, "phase_lr_deg": -5.2},
{"word": "갈까요?", "start_ms": 350, "dur_ms": 210, "phase_lr_deg": -5.0}
]
},
{
"segment": 44,
"transcript": "네 좋아요 하지만 저는 초보예요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 30, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "좋아요", "start_ms": 100, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "하지만", "start_ms": 220, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "저는", "start_ms": 340, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "초보예요", "start_ms": 430, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "걱정 마세요 쉬운 코스로 가요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "걱정", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": -4.8},
{"word": "마세요", "start_ms": 165, "dur_ms": 110, "phase_lr_deg": -4.9},
{"word": "쉬운", "start_ms": 285, "dur_ms": 80, "phase_lr_deg": -4.7},
{"word": "코스로", "start_ms": 375, "dur_ms": 110, "phase_lr_deg": -4.8},
{"word": "가요", "start_ms": 495, "dur_ms": 60, "phase_lr_deg": -4.9}
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
"transcript": "그럼 이번 주 일요일 어때요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 35, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "이번", "start_ms": 135, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "주", "start_ms": 235, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "일요일", "start_ms": 305, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "어때요?", "start_ms": 445, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "일요일 오전 9시에 만날까요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "일요일", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": -5.1},
{"word": "오전", "start_ms": 200, "dur_ms": 110, "phase_lr_deg": -5.2},
{"word": "9시에", "start_ms": 320, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "만날까요?", "start_ms": 450, "dur_ms": 110, "phase_lr_deg": -5.1}
]
},
{
"segment": 49,
"transcript": "좋습니다 아침 일찍 봐요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "좋습니다", "start_ms": 65, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "아침", "start_ms": 255, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "일찍", "start_ms": 375, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "봐요", "start_ms": 475, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "네, 그때 뵙겠습니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": -5.5},
{"word": "그때", "start_ms": 130, "dur_ms": 110, "phase_lr_deg": -5.4},
{"word": "뵙겠습니다", "start_ms": 250, "dur_ms": 300, "phase_lr_deg": -5.5}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-03-31 05:14:10*
