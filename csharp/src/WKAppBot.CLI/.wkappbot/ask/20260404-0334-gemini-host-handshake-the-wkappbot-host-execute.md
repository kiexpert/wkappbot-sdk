# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=34340  time=03:33:58  uptime=9165s  mem=442MB  system_processes=365"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260404_033349.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 126-123-361-312-321-231-152-621-265-567-132-130-103-312-136-103-510-125-160-536-516-520-132-136-132-312-513-574-570-157-102-132-107-512-126-103-103-102-213-216-126-261-574-571-7_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 625-652-650-652-652-562-652-652-567-526 627-627-621-623-623-621-625-625-625-627-267-261-261-621-261-263 620-627-620-623-625-625-621-625-627-621-625-267-627-267-261-261-265-652-6_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 765-625-263-213-143-132-163-106-167-176-126-160 231-312-163-162-623-627-126-123-103-312-276-275-263 516-216-267-326-163-326 130-125-671-126-160-165-106-106-123-103-651-132-613-3_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 176-673-567-136 134-617-135-146-576-163-103-156-261-123-135-167-160-657-167-657-715-516-106-610-165-675-362-136-617-103-317-136-152-251-617-267-657-576-576_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 132-136-136-130-103-137-167-615-136-103-130-315-316-105-135-136-163-316-127-137-176-106-302-317-756-751-576-136-103-135-162-612-167-671_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 163-617-651-136-130-653-103-673-165-102-716-731-123-763-103-623-236-126-126_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 327-357-325-312-123-163-312-316-134-312-132-213-754-576-754-754-372-376-316-326-236-325-257-763-341-321-267-632-624-607-672-607-615 123-241-136-162-123-157-135-157-137-627-672-6_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 102-136-163-172-163-136-103-136-103-136-136-160-106-167-106-312-167_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 132-316-753-320-126-127-106-102-610-602-302-306-362_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 157-135-135-137-132-231-237-325-352-537-573-574-531-351-352-523-352-523-256-513-527-325-235-251-356-325-531-537-574-573-537-513-523-526-521-521-526-526-526-576-256-257-275-251-2_W.mp3)
  Segment 11: 15760ms ~ 24076ms (file: 132-312-321-621-672-267-267-265-207-213-132-217-752-675-160-167-725-210-576-765-617-751-176-137-765-756-765-267-756-753-762-627-267-750-107-102-671-672-617-576-527-276-631-261-2_W.mp3)
  Segment 12: 25076ms ~ 25652ms (file: 321 136-317 756-756-725-762-763-576-571-125-157-173-217-765 675-361-326-365-627-362-756-765-675-765-675-247-132_V.mp3)
  Segment 13: 26652ms ~ 27228ms (file: 627-630-126-275-726-751-726-762-761-716-762-671-726-761-762-267-617-761-712-621-167-612-162-271-267-627-263-213-217-276-231-263-276-213-236-231-261-261-267-263-236-261-627-672-6_W.mp3)
  Segment 14: 28228ms ~ 28804ms (file: 574-671-670-673-670-765-760-726-576-576-752-675-163-216-216-201-213-236-621-620-236-237-236-263-623-162-672-216-160-106-641-364-123-516-136-315-315-314-143-132 231-132-143-123-6_W.mp3)
  Segment 15: 29804ms ~ 30380ms (file: 156-541-540-541-103 713-301-137-316-543-547-132-312-172-260-610-627-672-210-321-136-316-312-312-102-130-103-160-103-547-163-631-517-130-617-106-136-132-413-623-126-102-316-106-3_V.mp3)
  Segment 16: 31380ms ~ 31956ms (file: 756-567-675-657-572-756-675-265-263-625-562-576-576-576-756-576-765-675-675-625-562-675-627-576-627-762-675-762-567-625-576-675-657-527-657-675-657-627-625-672-567-672-672-672-5_W.mp3)
  Segment 17: 32956ms ~ 33532ms (file: 167-163-103-106-167-617-652-326-132-107-130-103-160-132-601-165-103-561-160-163-260-163 124-137-103 615-315-125-126-132-324-617-632-136-627-126-132-130-136-612-612-612-632-162-1_V.mp3)
  Segment 18: 34532ms ~ 35108ms (file: 617-613 126-132-261 267-172-127-726-761-261-135-103-127-102-120-210-106-571-576-576-571-571-756-754-574-574-573-576_W.mp3)
  Segment 19: 36108ms ~ 43812ms (file: 612-162-136-126-132-162-123-126-271-371-371-710-371-130-312-352-350-352-537-153-537-573-754-753-753-357-765-713-765-176-327-275-625-573_W.mp3)
  Segment 20: 44812ms ~ 52372ms (file: 103-263-236-120-126-106-106-253-301-253-572-231-103-263-265-265-625-265-625-267-265-625-267-265-265-265-261-263-263-624-652-625-652-625-625-265 625-263-625-263-263-267-265-265-6_W.mp3)
  Segment 21: 53372ms ~ 53948ms (file: 750-756-570-175-751-756-167-136-137-163-506-570-507-570-267-206-203-263-103-103-312-316-136-312-570-672-762-765-765-706-671-176-162-361-136-103-130-607-607-602-167-136-135-103-1_W.mp3)
  Segment 22: 54948ms ~ 59088ms (file: 673-237-132-137-317-371-327-327-372-321-123-765-327-132-213-236-371-310-132-312-327-236-261-310-326-312-312-312-312-317-312-312-231-263-267-237-320-623-371-137-317-371-321-326-2_W.mp3)
  Segment 23: 60088ms ~ 68223ms (file: 273-765-275-270-123-307-574-310-137-301-132-143-201-237-273-103-132-576-750-372-210-756-103-103-236-201-103-167-307-751-130-310-105-134-623-237-301-302-276-267-276-207-206-301-2_W.mp3)
  Segment 24: 69223ms ~ 69799ms (file: 216-321-310-301-361-360-623-657-617-612-312-132-361-316-316-136-136-103-103-163-103-103-136-163-136-163-162-136-132-504-504-540-167-175-176-163-136-132-143-106-162-163-761-760-7_V.mp3)
  Segment 25: 70799ms ~ 71375ms (file: 671-671-721-725-721-751-721-621-102-167-276-213-572-570-576-570-570-576-576-756-576-756-765-750-576-621-132 107-126 502-126-130-103-132-310-236-362-312-312-310-235-701-371-130-7_W.mp3)
  Segment 26: 72375ms ~ 72951ms (file: 613-216-376-361-136-213-321-321-132-132-167-316-132-132-123-672-362-231-316-670-610-627-261-627-267-627-260-132-136-620-261-362-103-132-120-213-261-312-120-130-312-132-312-316-1_V.mp3)
  Segment 27: 73951ms ~ 79603ms (file: 217-671-671-261-261-671-261-267-267-261-576-517-306-106-107-127-127-175-102-162-213-127-271-627-627-576-574-627-620-627-326-123-571-267-672-627-627-312-231-276-261-542-547-547-5_W.mp3)
  Segment 28: 80603ms ~ 81179ms (file: 163-617-517-657-562-263-267-263 576-756-136-310-301-120-504-507-576-276-231-213-621-362-306-301-316-162-617-671-176-162-134-103-103-103-103 236-261-627-261-216-103-123-127-123-3_V.mp3)
  Segment 29: 82179ms ~ 82755ms (file: 672-672-267-305-325-127-216-267-153-106-156-671-175-175-123-126-713-751-372-270-327-725-270-275_W.mp3)
  Segment 30: 83755ms ~ 84331ms (file: 132-103-106-106-132-652-123-235-627-132-126-657-163-136-132-103-132-162-362-657-231-612-167-621-136-136-617-167-162-132-167-137-123-716-103-603-103-136-162-103-167-613-617-613-1_V.mp3)
  Segment 31: 85331ms ~ 90083ms (file: 267-276-237-216-267-276-723-137-127-273-127-271-172-127-271-312-236-231-321-321-231-276-726-237-217-237-216-726-127-327-327-273-312-372-627-237-137-312-327-672-376-237-326-325-6_W.mp3)
  Segment 32: 91083ms ~ 91659ms (file: 576-361-613-152-176-701-173-107-137-173-103-631-136-103-130-103-130-510-605-620-612-602-301-316-503-627-163-316-103-735-167-175-715-105-316-123-136-167-106-670-160-623-621-621-1_V.mp3)
  Segment 33: 92659ms ~ 93235ms (file: 652-653 607-670-607-607-670-607-670-670-607-670-316-310-310-632-130-362-136-310-312-621-132-146-137-301-301-316-312-321-123-124-173-102-615-671-607-672-267-671-157-316-316-741-2_V.mp3)
  Segment 34: 94235ms ~ 94811ms (file: 301-316-302-260-206-230-237-271-172-726-637-765-657-301-106-320-316-132-310-301-310-120-320-123-102-163-162-361-167-106-216-160-762-267-267-276-216-157-752-571-165-617-761-267-6_W.mp3)
  Segment 35: 95811ms ~ 96387ms (file: 216-210-123-167-136-103-103-156-165-172-127-615-162-102-631-120-263-326-126-612-103-167-106-312-123-130-761-107-157-106-157-610-167-136-620-263-132-756-263-367 312-103-731-103-1_V.mp3)
  Segment 36: 97387ms ~ 97963ms (file: 765-762-625-265-625-261-236-625-652-657-612-627-765-625-675-627-265-625-267-265-627-567-765-675-675-657-670-675-650-675-675-765-763-671-765-675-761-765-675-765-765-726-762-765-6_W.mp3)
  Segment 37: 98963ms ~ 99539ms (file: 201-201-261-206-520-256-256-215-620-260-260-265-502-526-576-657-601-512-562-162 210-263-320-317-732 137-167-103-103-136-132-102-267-762-267-261-103-132-106-567-657-163-135-623-6_W.mp3)
  Segment 38: 100539ms ~ 101115ms (file: 365-356-236-216-312-216-621-362-236-216-621-321-356-126-356-156-132-362-216-312-362-321-526-156-516-216-231-625-362-623-362-263-632-623-627-623-326-263-236-236-263-326-531-352-3_W.mp3)
  Segment 39: 102115ms ~ 102691ms (file: 657-567-650-601-106-167-612-165-617-162-167-162-106-106-602-605-106-162-162-675-574-657-623-675-672-130-261-620-132-210-203-302-320-230-362-632-623-236-261-543-567-312-236-203-1_W.mp3)
  Segment 40: 103691ms ~ 104267ms (file: 756-765-765-756-754-765 675-576-751-136-152-165-671-670-173-160-126 572-574-754-751-571-175-213 213-236_W.mp3)
  Segment 41: 105267ms ~ 105843ms (file: 625-625-623-621-261-621-267-625-627-265-261-261 623-625-625-621-621-627-267-625-621-623-621-623-263-627-261-267-261-261 625-625-623-627-627-621-261-627-627-260-621-625-652-657-6_W.mp3)
  Segment 42: 106843ms ~ 107419ms (file: 213-321-321-321-236-236-107-156-576-574-576-756-576-236-237-236-265-263-236-612-623-263-326-263-265-625-625 265-267-267-326-267-527-576-326-625-256-612-326-263-316-312-132-316-5_W.mp3)
  Segment 43: 108419ms ~ 108995ms (file: 316-130-310-310-312-310-310-307-370-302-301-230-320-321-321-102-230-321-302-325-301-321-312-301-302-301-317-137-317-302-321-326-362-326-306-310-310-103-302-361-302-362-632-361-2_W.mp3)
  Segment 44: 109995ms ~ 110571ms (file: 362-160-612-102-137-103-123-132-126-126-136-103-160-623-163-326-163-136-106-160-731-163-172-261-136-126-601 123-107-130-130-132-175-167-715-162-136-163-651-106-756-517-326-167-2_V.mp3)
  Segment 45: 111571ms ~ 112147ms (file: 175-130 543-547-570-675-632 627-601-312-213-162-123-137-126-317 132-103-261-261-216-123-361-756-675-670-162-103-103-134-301-302-321 105-267-627-627-263-675-765 675-671-167-610-6_V.mp3)
  Segment 46: 113147ms ~ 113723ms (file: 103 103-130-136-103-103-150-103-312-236-751-516-326-103-130-312-130-103-102-132-106-103-167-312-731-103-105 574-574-617-612 107-135-106-106-163-165-136-160-165-136-251-165-156-1_V.mp3)
  Segment 47: 114723ms ~ 115299ms (file: 231-317-635-657-625-315-312-627-263-632-136-657-657-136-362_W.mp3)
  Segment 48: 116299ms ~ 116875ms (file: 103-137-103-762-167-120-301-320-130-312-130-132-130-132-103-163-103-316-173-132-103-163-103-136-137-176-137-130-106-103-316-163-137-310-165-167-167-103-163-103-165-216-167-160-1_V.mp3)
  Segment 49: 117875ms ~ 128243ms (file: 576-102-231-752-275-207-130-132-310-301-130-256-102-213-231-270-301-301-752-752-102-751-756-756-135-257-271-576-271-160-236-216-216-574-210-210-210-312-210-275-265-312-213-752-2_W.mp3)
  Segment 50: 129243ms ~ 129819ms (file: 136-137-517-605-136-170-137-651-716-165-613-136-132-103-136-137 103-671-103-137-137-167-173-167-326-253-126-250-136-153-165-136-136-103-103-153-301-567-132-123-153-756-123-156-6_V.mp3)
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
> **Time**: 2026-04-04 03:34:58
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "안녕하세요 올리버쌤입니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 20, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "올리버쌤입니다", "start_ms": 350, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "미네소타 이사 를 마치고 첫날 아침",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "미네소타", "start_ms": 10, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "이사", "start_ms": 160, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "를", "start_ms": 270, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "마치고", "start_ms": 320, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "첫날", "start_ms": 430, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "아침", "start_ms": 500, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "펑펑 쏟아지는 오십 센티 의 폭설 이",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "펑펑", "start_ms": 15, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "쏟아지는", "start_ms": 120, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "오십", "start_ms": 250, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "센티", "start_ms": 340, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "의", "start_ms": 410, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "폭설", "start_ms": 450, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 520, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "아침 인사 를 하네요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "아침", "start_ms": 20, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "인사", "start_ms": 130, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "를", "start_ms": 260, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "하네요", "start_ms": 310, "dur_ms": 260, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "지혜 의 본질 은",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "지혜", "start_ms": 30, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "의", "start_ms": 160, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "본질", "start_ms": 210, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "은", "start_ms": 370, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "변화 에 적응 하는 것이다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "변화", "start_ms": 10, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "에", "start_ms": 140, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "적응", "start_ms": 200, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 330, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "것이다", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "아 이건 안돼요 버려요 큰일 나요",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "아", "start_ms": 10, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "이건", "start_ms": 70, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "안돼요", "start_ms": 160, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "버려요", "start_ms": 270, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "큰일", "start_ms": 380, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "나요", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "저는 그게 아직 미숙 하니까",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 20, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "그게", "start_ms": 110, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "아직", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "미숙", "start_ms": 310, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "하니까", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "음 월요일 에 하루 참았 잖아요",
"speaker": "speaker_4",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "음", "start_ms": 10, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "월요일", "start_ms": 60, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "에", "start_ms": 210, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "하루", "start_ms": 250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "참았", "start_ms": 360, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "잖아요", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "매매 안하고 너 근데 샀다 팔았다 안했어",
"speaker": "speaker_5",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "매매", "start_ms": 20, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "안하고", "start_ms": 90, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "너", "start_ms": 180, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 230, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "샀다", "start_ms": 300, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "팔았다", "start_ms": 380, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "안했어", "start_ms": 490, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "아 월요일 에는 아예 매매 를 안했는데 안했어 스스 아 한번 매매 를 했 스",
"speaker": "speaker_4",
"confidence": 0.88,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "아", "start_ms": 15, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "월요일", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "에는", "start_ms": 210, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "아예", "start_ms": 280, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "매매", "start_ms": 370, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "를", "start_ms": 440, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "안했는데", "start_ms": 480, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "안했어", "start_ms": 610, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "스스", "start_ms": 700, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 770, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "한번", "start_ms": 820, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "매매", "start_ms": 900, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "를", "start_ms": 970, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "했", "start_ms": 1010, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "스", "start_ms": 1060, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "길 위에서도 걷거나 달릴 수 있는",
"speaker": "speaker_6",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "길", "start_ms": 10, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "위에서도", "start_ms": 60, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "걷거나", "start_ms": 210, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "달릴", "start_ms": 330, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 440, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 490, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "로봇 을 개발 했습니다",
"speaker": "speaker_6",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "로봇", "start_ms": 10, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "을", "start_ms": 120, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "개발", "start_ms": 170, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "했습니다", "start_ms": 290, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "안녕하세요",
"speaker": "speaker_7",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 20, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "미리 준비해 놓으면 맛 이 떨어지는 음식",
"speaker": "speaker_7",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "미리", "start_ms": 10, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "준비해", "start_ms": 100, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "놓으면", "start_ms": 230, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "맛", "start_ms": 320, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 370, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "떨어지는", "start_ms": 410, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "음식", "start_ms": 520, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "(noise)",
"speaker": "null",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 17,
"transcript": "두 개 큰 부담 을 동시에 안고 있다",
"speaker": "speaker_8",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "두", "start_ms": 10, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "개", "start_ms": 60, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 110, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "부담", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "을", "start_ms": 250, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "동시에", "start_ms": 290, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "안고", "start_ms": 410, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "있다", "start_ms": 490, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "하나 는 유럽 동부 에서의 군사적 긴장 이고",
"speaker": "speaker_8",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "하나", "start_ms": 15, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "는", "start_ms": 95, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "유럽", "start_ms": 135, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "동부", "start_ms": 225, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "에서의", "start_ms": 305, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "군사적", "start_ms": 375, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "긴장", "start_ms": 465, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "이고", "start_ms": 545, "dur_ms": 30, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "다른 하나 는 이천이십육년 이월 이십팔일 시작 된 이란 관련 전쟁 이다",
"speaker": "speaker_8",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "다른", "start_ms": 20, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "하나", "start_ms": 130, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "는", "start_ms": 240, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "이천이십육년", "start_ms": 290, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "이월", "start_ms": 900, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이십팔일", "start_ms": 1010, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "시작", "start_ms": 1220, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "된", "start_ms": 1330, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "이란", "start_ms": 1380, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "관련", "start_ms": 1490, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "전쟁", "start_ms": 1600, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이다", "start_ms": 1710, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "프랑스 는 이 전쟁 에 대해 이란 에 책임 이 있다고 보면서도 국제법 을 벗어난 미국 의 군사 행동 에 대해서도 비판 하는 복잡",
"speaker": "speaker_8",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "프랑스", "start_ms": 10, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "는", "start_ms": 170, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 210, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "전쟁", "start_ms": 260, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "에", "start_ms": 370, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "대해", "start_ms": 410, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이란", "start_ms": 520, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "에", "start_ms": 630, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "책임", "start_ms": 670, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 800, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "있다고", "start_ms": 840, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "보면서도", "start_ms": 960, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "국제법", "start_ms": 1120, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "을", "start_ms": 1250, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "벗어난", "start_ms": 1290, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "미국", "start_ms": 1410, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "의", "start_ms": 1520, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "군사", "start_ms": 1560, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "행동", "start_ms": 1670, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "에", "start_ms": 1780, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "대해서도", "start_ms": 1820, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "비판", "start_ms": 1980, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 2100, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "복잡", "start_ms": 2190, "dur_ms": 100, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-04 03:34:58*
