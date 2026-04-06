# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=08:40:44  uptime=26489s  mem=1852MB  system_processes=446"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_084035.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 176-713-317-173-103-102-176-326-302-310-321-351-253-123-126-612-312-126-632-205-312-310-312-317-176-173-102-263-170-617-612-106-136-360-216-162-162-175-756-567-576-572-576-263-1_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 672-632 612-361-136-316-206-261-210-120-201-230-203-203-201-203-261-123-210-263-316-326-623-301-210-261-216-201-321-201-201-210-263-230_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 132-136-132-263-612-260-103-132-132-132-103-130_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 162-176-167-106-576-756-765-617-167-106-156-612-635-310-103-103-136-136-103-106-106-106-136-765-617-653-176-103-103-107-137-173-175-170-173-137-160-610-152-576-510-615-621-132-3_V.mp3)
  Segment 5: 6304ms ~ 18364ms (file: 261-106-126-167-162-163-617-613-103-136-267-162-176-310-136-163-107-165-762-760-167-167-106-163-751-631-753-132-167-137-160-103-136-103-216-617-163-102-136-136-136-136-136-761-6_V.mp3)
  Segment 6: 19364ms ~ 19940ms (file: 231-231-542-356-316-316-312-321-236-562-265-627-562-562-752-276-152-315-312-326-263-562-542-251-263-263-257-276-721-564-657-567-563-325-326-236-536-512-215-542-572-562-546-540-5_W.mp3)
  Segment 7: 20940ms ~ 21516ms (file: 213-627-206-620-276-756-276-307-631-261-216-137-103-136-136-136-132-137-135-376-172-137-132-136-163-126-103-675-316-132-134-231-312-132-132-321-132-621-623-324-321-341-132-312-3_V.mp3)
  Segment 8: 22516ms ~ 23092ms (file: 362-265-237-326-571-135-361-671-163-162-213-321-216-127-123-236-237-256-257-123-213-213-123-362-126-236-126-126-132-213-123-136-216-126-162-123-236-126-632-365-326-326-327-236-1_W.mp3)
  Segment 9: 24092ms ~ 24668ms (file: 320-310-312-321-132-132-231-312-213-132-132_W.mp3)
  Segment 10: 25668ms ~ 26244ms (file: 263-236-623-132-126-526-621-123 167-271_V.mp3)
  Segment 11: 27244ms ~ 27820ms (file: 657-376-327-625-167-167-130-132-163-631-132-123-607-362-602-367-316-312-312-103-103-301-136-312-306-603-607-103-132-573-567-163-317-136-132-672-623-620-302-760-675-316-631-617-1_V.mp3)
  Segment 12: 28820ms ~ 29396ms (file: 235-254-253-236-265-235-265-251-253-502-273-327-327-325-352-257-257-215-250-201-210-236-261-215-527-254-253-625-635-532-735-375-570-317-371-317-317-137-316-326-127-167-136-365-3_W.mp3)
  Segment 13: 30396ms ~ 30972ms (file: 312-103-213-203-362-312-310-302-631-302-361-621-321-203-102-627-601-617-621-627-263-206-206-206-120-216-216-126-126-312-123-326-671-617-651-762-765-762-726-132-763-756-765-756-7_W.mp3)
  Segment 14: 31972ms ~ 32548ms (file: 312-127 263-612 136-103-136-162-136-675-316-162-162-167-106-163-623-103-167-163-162-610-160-160-360-176-126-167-137-130-136-167-167-136-160-163-176-102-126-163-216-136-610-126-1_V.mp3)
  Segment 15: 33548ms ~ 34124ms (file: 546-576-136-316-137-103-160-136-167-136-106-132-103-162-152-103-132-106-127-162-167-106-167-103-132-615-175-172-176-175-103-136-167-617-163-176-167-751-167-761-103-613-613-163-6_V.mp3)
  Segment 16: 35124ms ~ 35700ms (file: 652-670-615-635-560-573-157-103-312-132-621-236-126-162-571 623-603-631-361-326-263-326 102-103-106-102-103-136-163-136-103-621-625-316-625-301-263-601-612-601-657-657-610-601-6_V.mp3)
  Segment 17: 36700ms ~ 37276ms (file: 627-672-627-672-675-672-627-671-176-672-672-672-671-136-263-210-203-206-206-206-260-710-176-106-765-173-367-106-126-106-102-123-130-123-201-236-231 573-756-756-672-765-762 627-6_W.mp3)
  Segment 18: 38276ms ~ 38852ms (file: 576-574-576-752-762-672-657-657-675-620-632-306-672-136-567 546-564-362-546-576-576 617-765-765-136-316-361 276-260-162-172 160-126-103-163-106_W.mp3)
  Segment 19: 39852ms ~ 40428ms (file: 540-542-574-570-357-357-357-305-312-167-716-716-716-517-256-267-265-267-276-267-276-351-357-357-357-317-153 367-367-352-653 517 132-513-132_W.mp3)
  Segment 20: 41428ms ~ 42004ms (file: 513-471-271-263-256-317-172-163-132 752-765-756-217-756_V.mp3)
  Segment 21: 43004ms ~ 43580ms (file: 376-612-612-765-756-765-275-627-703-312-267-256-576-576-765-570-574-576-756-760-762-706-762-762-765-675-162-216-362-263-527-756-237-627-623-170-170-675-260-236-723-270-273-723-7_W.mp3)
  Segment 22: 44580ms ~ 45156ms (file: 231-312-326-362-362-623-627-623-301-123-651-765-620-123-217-271-173-132-231-201-203-153-375-325-302-236-263-675-216-325-537-527-576-172-175-172-126-103-136-173-136-756-765-726-1_V.mp3)
  Segment 23: 46156ms ~ 46732ms (file: 165-367-136-217-312-172 451-317 671-671-627-172-627_V.mp3)
  Segment 24: 47732ms ~ 48308ms (file: 106-301-612-302-302-203-130-132 312_V.mp3)
  Segment 25: 49308ms ~ 49884ms (file: 521-625-153-627-123-765-236-132-123-102-102-126-163-126-102-103-125-162-102-602-260-576-576-573-135-103-103-156-312-236-263-625-126-163-103-132-632-613-126-102-675-675-670-617-6_W.mp3)
  Segment 26: 50884ms ~ 51460ms (file: 106-103-132-673-317-103-163-167-632-137-132-176-106-130-601-625-106-571-617-167-106-675-672-106-607-621-652-106-103-103-162-126-106-160-163-162-620-167-123-560-162-172-612-103-1_V.mp3)
  Segment 27: 52460ms ~ 53036ms (file: 623-267-631-625-326-621-216-132-731-167-136-135-136-125-163-135-617-137-167-615-106-137-106_W.mp3)
  Segment 28: 54036ms ~ 54612ms (file: 132-345-136-125-635-173-316-312-315-314-146-641-153-671-621-375-132-713-143-103-137-125-165-126-132-173-150-135-137-263-106-612-172-160-126-125-162-167-623-617-163-103-160-615-1_V.mp3)
  Segment 29: 55612ms ~ 56188ms (file: 621-136-163-760-671-162-136-301-106-130-671-103-130-103-103-302-325-123-103-603-630-312-136-765-267-602-625-620-620-160-621-602-362-762-760-671-261-261-263-216-621-162-612-613-2_V.mp3)
  Segment 30: 57188ms ~ 57764ms (file: 576-725-276-327-126-176-127-176-312-267-627-623-627-267-265-265-627-261-237-123-617-362-573-574-573-576-567-567-362-316-312-526-652-567-765-632-316-631-167-136-126-316-263-621-2_W.mp3)
  Segment 31: 58764ms ~ 59340ms (file: 367-326-237-275-725-276-762-576-756-576-173-617-671-617-231-213-236-236-251-256-652-263-526-561-516-615-362-215-162-126-162-576-567-576-657-165-651-157-573-576-572-532-216-261-1_W.mp3)
  Segment 32: 60340ms ~ 60916ms (file: 671-760 657-265 362-175-103-713-732-103-106-617-103-137-130-176-123-136-103-136-106-123-506-567-127-123-203-615-621-651-567-567-261-263-362-162-621-762-621-613-213 216-156-106-1_W.mp3)
  Segment 33: 61916ms ~ 62492ms (file: 261-213-203-320-321-132-103-136-167-103-103-102-106-216-206-602-632-316_V.mp3)
  Segment 34: 63492ms ~ 64068ms (file: 517-167-675-103-106-132-625-162-613-612-160-167-160-603-163-137-125-613-167-127-613-610-602-607-564-576-672-106-106-106-650-175-106-107-103-172-106-103-160-360-610-325-126-136-1_W.mp3)
  Segment 35: 65068ms ~ 65644ms (file: 607-617-310-103-136-367-617-102-671-760-376-165-765-106-103-375-103-136-516-163-561-156-162-524-617-167-130-130-103-130-130-167-175-102-671-716-653-715-623-126 216-126_V.mp3)
  Segment 36: 66644ms ~ 67220ms (file: 637-675-160-361-163-137-163-367-617-162-106-316-106-103-130-206-715-631-106-613-632-362-162-162-132-136-765-103 576-627-651-167_W.mp3)
  Segment 37: 68220ms ~ 68796ms (file: 602 657-675-607-652-750 160-263-620-362-256-265-306-230-320-213-267-263-267-623-236-126-230-627-765-675-625-601-601-106-103-312-315-123-132-261-231-201-263-201-602-602-602-602-6_W.mp3)
  Segment 38: 69796ms ~ 70372ms (file: 316-127-163-216-362-167-106-103-103-106-106-103-102-103-132-123-127-132-213-312-413-176-123-176-136-132-103-132-123-123-175-143-132_V.mp3)
  Segment 39: 71372ms ~ 71948ms (file: 160-102-103-132-617-136-657-165-126-275-275-521 765-765-765-167-102-675-170-160-650-216-130-735-167-310-106-761-635-672-162-160-163-106-167-617-106-176-106-167-163-126-261-236-1_W.mp3)
  Segment 40: 72948ms ~ 73524ms (file: 567-152-160-167-162-106-165-105-165-356-167-132-136-107-136-167-657-175-126-675-167-103-163-106-163-103-130-165-167-167-610-136-167-167-165-167-627-162-167-317-316-312-167-176-6_V.mp3)
  Segment 41: 74524ms ~ 75100ms (file: 670-674-163 576-756-765-671-736-651-361-671-671-167-167-617-176-165-136-163-637-672-716-176-605-163-671-617-163-675-675-617-672-672-165-176-675-162-321-671-167-617-376-367-327-6_W.mp3)
  Segment 42: 76100ms ~ 76676ms (file: 573-572-302-231-132-312-132-136-203-230-321-231-132-320-236-302-312-103-132-206-230-103-203-320-320-130-163-103-103-321-321-103-213-312-213-203-203-103-301-501-136 206-260-627-2_V.mp3)
  Segment 43: 77676ms ~ 78252ms (file: 136-132 371 761-760-107-756-751-106-162-102-126-126-126-132-634-312-621-126-236-623-321-162-176-162-103-217-650-567-562-163-165-160-672 261-236-623-632-130_V.mp3)
  Segment 44: 79252ms ~ 79828ms (file: 673-312-761-167-675-617 761-163-362-106-612-106-106-671-107-106-162-312-671-167-103-160-312-176-160-650-167-176-601-671-103-561-176-162-670-612-162-106-163-163-617-106-160-163-1_V.mp3)
  Segment 45: 80828ms ~ 81404ms (file: 260-260-260-206-607-620-602-260-230-320-302-103-106-206-260-126-201-106-162-103-206-326-203-302-621-625-652-601-601-612-612-167-250-305-106-103-571-756-261-326-130-261-230-267-3_V.mp3)
  Segment 46: 82404ms ~ 82980ms (file: 623-617-265-267-632-632-136-263-621-263-236 627-237-321-765-631-617-172 123-321-260-136-312 627-326-376_W.mp3)
  Segment 47: 83980ms ~ 84556ms (file: 612-130-167-163-136-130-310-136-361-310-136-216-167-137-156_V.mp3)
  Segment 48: 85556ms ~ 93944ms (file: 134-106-163-623-671-132-167-612-167-132-312-713-570-132-134-103-106-316-160-613-106-136-162-106-571-167-163-176-317-103-130-136-136-126-613-160-203-623-326-102-106-613-163-136-3_V.mp3)
  Segment 49: 94944ms ~ 95520ms (file: 137-173-123-267-762-230-321-230-325-237-127-315-256-103-312-132-123-172-716-136-312-163-123-265-102-203-230-302-312-301-320-302-312-307-257-532-574-572-302-725-257-236-132-312-1_V.mp3)
  Segment 50: 96520ms ~ 97096ms (file: 627-267-612-306-321-206-302-301-136-106-517-165-163 267-206-267-260-261-206-620-627-620-620-627-601-620-602-103-310-326-310-501-571-576-571-570-671-267-623-621-602-206-103-362-6_V.mp3)
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
> **Time**: 2026-04-03 08:41:44
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "다음 주에 여행 계획 있으세요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "다음", "start_ms": 60, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "주에", "start_ms": 170, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "여행", "start_ms": 260, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "계획", "start_ms": 350, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있으세요?", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "제주도로 가족 여행을 가기로 했어요.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "제주도로", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가족", "start_ms": 210, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "여행을", "start_ms": 320, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "가기로", "start_ms": 450, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "했어요.", "start_ms": 540, "dur_ms": 30, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "재미있겠네요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "재미있겠네요.", "start_ms": 40, "dur_ms": 530, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "오랜만에 가는 거라 기대가 많이 돼요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "오랜만에", "start_ms": 45, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가는", "start_ms": 200, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "거라", "start_ms": 290, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "기대가", "start_ms": 380, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 490, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "돼요.", "start_ms": 560, "dur_ms": 10, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "숙소는 어디로 예약하셨어요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "숙소는", "start_ms": 55, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "어디로", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "예약하셨어요?", "start_ms": 290, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "바다가 보이는 호텔로 잡았어요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "바다가", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "보이는", "start_ms": 170, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "호텔로", "start_ms": 290, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "잡았어요.", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 8,
"transcript": "가서 맛있는 것도 많이 드시고 오세요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "가서", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "맛있는", "start_ms": 140, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "것도", "start_ms": 270, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 360, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "드시고", "start_ms": 450, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "오세요.", "start_ms": 540, "dur_ms": 30, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 10,
"transcript": "제주도 흑돼지를 꼭 먹어보려고요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "제주도", "start_ms": 45, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "흑돼지를", "start_ms": 170, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "꼭", "start_ms": 320, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "먹어보려고요.", "start_ms": 390, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "생각만 해도 군침이 도네요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "생각만", "start_ms": 55, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "해도", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "군침이", "start_ms": 310, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "도네요.", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "Where are you planning to go first?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "Where", "start_ms": 70, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "are", "start_ms": 160, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 210, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "planning", "start_ms": 260, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 390, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "go", "start_ms": 440, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "first?", "start_ms": 510, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "We will visit the Manjanggul Cave.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "We", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "will", "start_ms": 110, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "visit", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 290, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "Manjanggul", "start_ms": 340, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "Cave.", "start_ms": 530, "dur_ms": 40, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "거기 동굴 정말 시원하고 멋있죠.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "거기", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "동굴", "start_ms": 150, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 260, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "시원하고", "start_ms": 370, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "멋있죠.", "start_ms": 500, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "렌터카는 미리 빌리셨나요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "렌터카는", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "미리", "start_ms": 240, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "빌리셨나요?", "start_ms": 350, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "네, 공항에서 바로 찾기로 했어요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "공항에서", "start_ms": 110, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 260, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "찾기로", "start_ms": 370, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "했어요.", "start_ms": 500, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 18,
"transcript": "선물 사 오실 건가요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "선물", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "사", "start_ms": 150, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "오실", "start_ms": 200, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "건가요?", "start_ms": 330, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "감귤 초콜릿이라도 좀 사 올게요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "감귤", "start_ms": 55, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "초콜릿이라도", "start_ms": 160, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 370, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "사", "start_ms": 440, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "올게요.", "start_ms": 510, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "고마워요! 여행 잘 다녀오세요.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "고마워요!", "start_ms": 30, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "여행", "start_ms": 220, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 330, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "다녀오세요.", "start_ms": 400, "dur_ms": 170, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 22,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 23,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 24,
"transcript": "안녕히 계세요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "계세요.", "start_ms": 230, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 26,
"transcript": "새 아파트 분위기는 어떤가요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "새", "start_ms": 45, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "아파트", "start_ms": 100, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "분위기는", "start_ms": 230, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "어떤가요?", "start_ms": 380, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 28,
"transcript": "단지 안에 공원도 있고 아주 쾌적해요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "단지", "start_ms": 55, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "안에", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "공원도", "start_ms": 250, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "있고", "start_ms": 380, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 470, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "쾌적해요.", "start_ms": 540, "dur_ms": 30, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 30,
"transcript": "교통은 편리한 편인가요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "교통은", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "편리한", "start_ms": 170, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "편인가요?", "start_ms": 300, "dur_ms": 270, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 32,
"transcript": "지하철역이 바로 앞이라 출퇴근이 편해요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "지하철역이", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 240, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "앞이라", "start_ms": 350, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "출퇴근이", "start_ms": 440, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "편해요.", "start_ms": 550, "dur_ms": 20, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 34,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 35,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 36,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 37,
"transcript": "정말 부럽네요! 나중에 집들이 한 번 하세요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "부럽네요!", "start_ms": 130, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "나중에", "start_ms": 280, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "집들이", "start_ms": 370, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 460, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "번", "start_ms": 510, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "하세요.", "start_ms": 560, "dur_ms": 10, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 39,
"transcript": "정리가 좀 되면 초대할게요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "정리가", "start_ms": 30, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 160, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "되면", "start_ms": 230, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "초대할게요.", "start_ms": 320, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "네, 연락 기다릴게요. 축하드려요!",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "연락", "start_ms": 110, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "기다릴게요.", "start_ms": 200, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "축하드려요!", "start_ms": 390, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 44,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 45,
"transcript": "다음에 또 봬요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 190, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "봬요.", "start_ms": 260, "dur_ms": 310, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 47,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 48,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 49,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
*Generated by WKAppBot ask gemini — 2026-04-03 08:41:44*
