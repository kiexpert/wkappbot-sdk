# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=04:35:11  uptime=11757s  mem=1543MB  system_processes=448"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_043414.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 625-106-103-103-316-302-320-256-321-756-765-627-261-103-137-103-362-230-561-265-123-621-620-620-362-231-321-312-132-123-216-123-102-213-126-213-312-271-267-672-631-362-520-651-6_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 567-176-761-607-673-756-756-362-367-132-163-671-163-123-163-312-103-167-617-163-103-317-176-126-102-160-130-103-103-136-130-126 651-163-132-106-621-165-106-756-765-761-710-106-1_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 106 637-672 103-312-132-136-126-132-137 267 126-106-102-106-105-103-103 153-132-264-132-312 365-312 316-312-751-753_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 156 130-102-120-176-726-136-361-267-316-671-672-612-621-617-163-271-137-130-106-761-376-673-765-765-675-637-367-361-756-765-762-675-672-763-673-136-623-607-672-672-602-601-607-6_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 326-326-125-326-302-360-362-130-132-210-260-126-106-165-165-165-165-132-326-321-103-132-672-627-672-620-602-612-213-705-765-375-310-130-132-576-657-567-657-657-321-570-567-231-3_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 215-103-316-361-301-136-130-351-675-675-302-302-637-132-376-756-730-135-756-765-516-301-361-136-750-152-620-260-310-106-726-167-301-210-261-162-617-167-176-671-163-102-260-120-1_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 623-603-132-163-136-162-136-103-130-126-675-136-136-213-132-301-136-126-712-162-621-612-160-301-627-236-263-610-672-657-612-136-162-206-103-375-615-567-562-321-765-675-273-261-7_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 731-765-765-753 762-721-163-136 103_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 627-765-127-126-612-163-162-163-126-126-162-162-162-163-136 123_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 231-236-231-216-213-321-231-321-321-231-321-321-321-231-321-321-321-326-321-312-231-236-231-213-216-231-321-231-132-123-213-312-231-231-123-312-213-213-216-123-231-213-321-213-3_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 675-103-610-167-561-106-103-671-167-613-163-106-316-103-163-126_V.mp3)
  Segment 12: 17336ms ~ 29180ms (file: 103-516-132-103-613-167-165-106-130-576-176-130-136-103-136-167-167-103-103-163-163-136-160-615-163-136-316-103 136-167-562-102-175-167-103-165-160-136-176-167-627-625-231-310-3_V.mp3)
  Segment 13: 30180ms ~ 30756ms (file: 374-265-352-315-367-267-237-134-325-321-231-351-765-164-315-312-162-123_V.mp3)
  Segment 14: 31756ms ~ 32332ms (file: 102-261-106-123-103-125-163-132-135 621-126-132-125-136-176_V.mp3)
  Segment 15: 33332ms ~ 33908ms (file: 310-103-301-310-263-230-623-216-312-230-312-130-132-102-103-301-301-302-130-103-103-103-162-326-321-306-132-301-312-631-765-756-360-261-361-360-302-132-310-315-301-673-673-360-2_W.mp3)
  Segment 16: 34908ms ~ 35484ms (file: 216-162-136-132-136-301-130-603-267-623-276-627-562-652-652-162-312 670-671-160-231-120-627-612-267-120-162-106-621-627-627-621-231-321-612-621-267-627-712-762-316-261-132-621-7_W.mp3)
  Segment 17: 36484ms ~ 37060ms (file: 627-267-326-106-103-631-163 675-675-601-612-136-102-130-602-162-126-216-312-123-621-612-102-261-130-316-123-617-361-736-620-263-206-620-263-261-263-602-601-261-761-162-612-621-6_W.mp3)
  Segment 18: 38060ms ~ 38636ms (file: 126-136-263-126-631-132-312-132-312-574-574-574-705-316-132-316-106-612-210-321-615-260-620-602-610-612-160-756-756-160-621-261-260-236-136-371-163-750-321-613-103-302-231-203-2_V.mp3)
  Segment 19: 39636ms ~ 40212ms (file: 621-627-126-163-126-675-657-675-627-267-627-627-162-136-625-672-632-632-316-132-136-130 627-620-612-321-106 312 675-612-263-263-216-672-627-632 321-310-130 157 312-326-612-362-6_W.mp3)
  Segment 20: 41212ms ~ 41788ms (file: 762-762-761-762-761-765-765-716-671-167-167-675-216-230-236-312-327-210-613-127-167-567-657-567-657-652-567-576-526-567-572-567-576-576-216-620 136-321-137-163-103-136-376-361-5_W.mp3)
  Segment 21: 42788ms ~ 43364ms (file: 256-216-236-312-267-652-206-265-260-231-321-123-320-306-132-315-312-315-231-237-267-267-635-637-635-631-231-265-257-257-265-265-267-265-276-237-256-267-267-632-527-574-573-754-3_W.mp3)
  Segment 22: 44364ms ~ 44940ms (file: 356-653-562-316-362-362-256-503-265-236-260-562-576-321-362-263-567-357-652-625-612-632-356-365-365-326-573-317-312 157-261-756-657-675-672-135-362-361_W.mp3)
  Segment 23: 45940ms ~ 46516ms (file: 312-321-130-120-103-132-103-167-567-756-576-576-173-632-267-326-236-365-326-326-162-316-613-167-615-675-615-671-361-617-617-671-610-167-671-162-312-123-231-136-230-236-216-312-3_W.mp3)
  Segment 24: 47516ms ~ 48092ms (file: 236-132-627-672-612-621-126-167-167-106-612-617-762-761-706-726-607-672-762-753-736-136-176-137-163-631-167-736-362-316-361-152-163-167-317-163-103-301-136-103-673-602-163-167-6_V.mp3)
  Segment 25: 49092ms ~ 49668ms (file: 627-267 621-132-153-103-564-230-567-316-103-136-103-136-130-130-106-316-765-106-316-651-162-671-163-316-263-123-213-132-672-762-762 132-162-162-163-143-132-142-132-123-124-102-1_V.mp3)
  Segment 26: 50668ms ~ 51244ms (file: 623-261-261-756-765-267-576-571-562_W.mp3)
  Segment 27: 52244ms ~ 52820ms (file: 637-602-610-160-617-306-136-163-132-137-132-260-236-326-326-130-621-671-617-162-102-132-136-201-236-201-203-203-102-126-163-321-756-620-260-263-261-162-216-732-106-132-203-236-2_V.mp3)
  Segment 28: 53820ms ~ 54396ms (file: 672-763-736 316-675-617-627-267-627-216-216-261-123-126_W.mp3)
  Segment 29: 55396ms ~ 55972ms (file: 612-261-627-261 267-627-627-267-623-627-627-236-265-362-562-267-267-263-261-621_W.mp3)
  Segment 30: 56972ms ~ 57548ms (file: 136-165-136-163-106-765-103-130-136-613-130-731-137-130-163-162-120-265-263-261-267-632-263-621-632-753 103-620-136-130-617-172-126-103-103-130-103-756-263-612-136-126-176-617-1_V.mp3)
  Segment 31: 58548ms ~ 59124ms (file: 301-312-321-132-123-153-125-310-205-135-310-516-572-216-126-130-201-120-213-120-132-321-327-735-312-136-103-163-163-716-167-321-310-163-160-310-310-571-162-135-126-132-312-136-1_W.mp3)
  Segment 32: 60124ms ~ 60700ms (file: 574-567-567-657-321-123-376 261-632-623_W.mp3)
  Segment 33: 61700ms ~ 62276ms (file: 627 123-132-123-132-162-126-132-136-165 765-762-672-267-263-216-261-321-132-120-102-276-267-265_W.mp3)
  Segment 34: 63276ms ~ 63852ms (file: 163-321-361 315-312-136-103 621-321_V.mp3)
  Segment 35: 64852ms ~ 65428ms (file: 310-657-136-302-316-136-160-106-103-103-136-132-165-132-106-136-361-617-136 137-145-173-174-135-265-136-621-136-613-136-167-160-612-132-132-103-132-136-126-621-263-216-261-210-1_V.mp3)
  Segment 36: 66428ms ~ 67004ms (file: 261-263-216-213-236-231-213-216-213-326-326-362-326-236-326-267-236-263-231-321-316-521-312-132-132-163-162-126-123-132-136-162-167-162-623-236-213-263-326-312-126-521-527-213-3_W.mp3)
  Segment 37: 68004ms ~ 68580ms (file: 162-167-136-762 672-326-263-236-263-326-362-753-675-275 267-163-216-126-162-326-267-657-576-765-765-675-610-673-672-367-675-765 132-216-261-231-236-631-167-621-263-261 756-765-6_W.mp3)
  Segment 38: 69580ms ~ 70156ms (file: 316-310-317-260-236-312-132-102-320-230-621-623-263-612-106-163-613-563-601-123-612-760-765-127-612-130-206-210-621-236-320-302-310-754-754-675-756-271-102-136-620-206-263-261-6_W.mp3)
  Segment 39: 71156ms ~ 71732ms (file: 672-672-267 675-672-675-672-167-162-132-613-631-136-163-162-261-670-672-675-672-160-167-617-176-671-617-613-612-263-263-302-326-316-317-301-301-361-321-260-762-760-762-617-103-3_V.mp3)
  Segment 40: 72732ms ~ 73308ms (file: 763-672-763-675-367-736-637-673-765-765-675-765-765-673-637-160-361-613-617-106-132-612-617-176-736-673-761-671-671-637-672-362-672-671-637-617-672-672-672-261-617-612-671-623-2_W.mp3)
  Segment 41: 74308ms ~ 74884ms (file: 321-231-321-123-126-162-132-571-574-306-362-627-260-620-602-160-601-620-126-162-617-617-167-617-623-162-104-317-137-102-201-123-103-123-132-362-325-132-162-761-760-671-761-671-3_V.mp3)
  Segment 42: 75884ms ~ 76460ms (file: 620-623-627-627-276-123-103-103-102-167-127-136-163-106-103-310-123-301-103-136-136-361-762-326-627-627-627-672-627-263-201-263-745-742-351-132-547-567-657-174-175-173-143-301-3_V.mp3)
  Segment 43: 77460ms ~ 78036ms (file: 261-106-106-316-103-302-130-612-510-105-130-250-651-512-125-126-103-213-136-102-312-123-312-162-132-316-326-321-312-312-132-312-316-321-132-136-172-125-132-103-103-103-301-206-2_V.mp3)
  Segment 44: 79036ms ~ 79612ms (file: 106 235-257-326 326-741-217-312 574-572-765-576-567-576-756-657-756-765-670-671-126-263-326-312-356-716-623-167-326-736 231-162-236_W.mp3)
  Segment 45: 80612ms ~ 81188ms (file: 672-670-675-756-675-576-756-756-216-316-163-123-765-672-160-706-731-316-167-136-137-136-136-103-301-136-561-312-621-316-132-312-310-176-126-731-103-106_V.mp3)
  Segment 46: 82188ms ~ 82764ms (file: 137-176-620-617 130-103-103-130-136-103-136-316-127-653-176-165 163-103-103-167-163-103-160-136-167-135-132-160-103-103-360-176-132-106-102-172-106-136-103-106-167-103-160-574-2_V.mp3)
  Segment 47: 83764ms ~ 84340ms (file: 607-627-607-236-261-261-617-360-261-102-103-106-160-172-130-103-156-136-103-576-167-160-173-316-137-236 657-162-167-371-132-316-103-103-576-576-572-257-275-176-163-136-267-610-1_W.mp3)
  Segment 48: 85340ms ~ 85916ms (file: 570-570-106-167 217-127-271-215-312-213-132-321-135-215-213-521-235-312-326-625-263-673-167-673-312-315-136-317-312-136-132-316-123-236-231-123-123-216-231-326-623-263-213-632-2_W.mp3)
  Segment 49: 86916ms ~ 87492ms (file: 267-267-627 637-753-263-267-675-635-627-671-672-706-236-236-716-765-756-750-765-567-375-326-325-632-372-173 163-627-627 267-627 162-162 132-143-147 267-672-627-267-627-164-162 6_W.mp3)
  Segment 50: 88492ms ~ 89068ms (file: 263-326-623-612-672-627-627-136-132-217-125-217-217-162-163-163-623 157-367-763-567-753-156-215-162-657-620-267-173-170-516-267-136-167-173-102-163-603-213 261-136-163-107-167-1_W.mp3)
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
> **Time**: 2026-04-03 04:36:32
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "안녕하세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "안녕", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "하세요", "start_ms": 250, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "반갑습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "반갑", "start_ms": 40, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "습니다", "start_ms": 290, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "오늘 날씨가 좋네요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 60, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 210, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 390, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "공부하기 좋은 날입니다",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "공부하기", "start_ms": 45, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 265, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "날입니다", "start_ms": 385, "dur_ms": 170, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "함께 시작해볼까요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "함께", "start_ms": 55, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "시작해", "start_ms": 215, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "볼까요", "start_ms": 395, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "첫 번째 문장입니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "첫", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "번째", "start_ms": 130, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "문장입니다", "start_ms": 270, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "천천히 따라 읽어보세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "천천히", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "따라", "start_ms": 220, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "읽어보세요", "start_ms": 360, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "잘 하셨습니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "잘", "start_ms": 70, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "하셨습니다", "start_ms": 190, "dur_ms": 310, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "다시 한번 해볼까요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "다시", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "한번", "start_ms": 190, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "해볼까요", "start_ms": 340, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 11,
"transcript": "이번에는 조금 더 빠르게",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "이번에는", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 220, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 340, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "빠르게", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "발음에 주의하면서 연습해 보세요. 한국어는 억양보다 명확한 발음이 중요합니다.",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "발음에", "start_ms": 80, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "주의하면서", "start_ms": 280, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "연습해", "start_ms": 630, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "보세요", "start_ms": 810, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "한국어는", "start_ms": 1000, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "억양보다", "start_ms": 1250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "명확한", "start_ms": 1550, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "발음이", "start_ms": 1770, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "중요합니다", "start_ms": 1950, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "Excellent progress so far.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Excellent", "start_ms": 50, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "progress", "start_ms": 270, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "so", "start_ms": 450, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "far", "start_ms": 550, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "계속해서 다음 단계로",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "계속해서", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 240, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "단계로", "start_ms": 360, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "단어의 의미를 생각하며 읽어주세요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "단어의", "start_ms": 60, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "의미를", "start_ms": 210, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "생각하며", "start_ms": 350, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "읽어주세요", "start_ms": 530, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "집중해서 듣는 것이 가장 중요합니다",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "집중해서", "start_ms": 45, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "듣는", "start_ms": 245, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "것이", "start_ms": 345, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 465, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "중요합니다", "start_ms": 595, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "매일 꾸준히 하는 연습이 실력을 만듭니다",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "매일", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "꾸준히", "start_ms": 170, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 320, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "연습이", "start_ms": 420, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "실력을", "start_ms": 560, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "만듭니다", "start_ms": 700, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "포기하지 말고 끝까지 화이팅하세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "포기하지", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "말고", "start_ms": 220, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "끝까지", "start_ms": 320, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "화이팅하세요", "start_ms": 460, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "거의 다 왔습니다 조금만 더 힘내세요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "거의", "start_ms": 55, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 155, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "왔습니다", "start_ms": 205, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "조금만", "start_ms": 355, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 495, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "힘내세요", "start_ms": 555, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "마지막까지 집중해 주셔서 감사합니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "마지막까지", "start_ms": 40, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "집중해", "start_ms": 260, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "주셔서", "start_ms": 410, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 540, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "수고하셨습니다. 다음 시간에 만나요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "수고하셨습니다", "start_ms": 50, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 350, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "시간에", "start_ms": 450, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 570, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "Happy learning journey to you.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "Happy", "start_ms": 60, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "learning", "start_ms": 210, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "journey", "start_ms": 390, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 570, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 650, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "안녕히 가세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 220, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "Have a wonderful day ahead.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "Have", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 145, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "wonderful", "start_ms": 195, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "day", "start_ms": 415, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "ahead", "start_ms": 515, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "See you soon in the next lesson.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 150, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "soon", "start_ms": 250, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 370, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 450, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "next", "start_ms": 530, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "lesson", "start_ms": 670, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 27,
"transcript": "복습은 가장 좋은 공부 방법입니다.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "복습은", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 220, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 340, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "공부", "start_ms": 440, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "방법입니다", "start_ms": 560, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "잊지 말고 꼭 하세요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "잊지", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "말고", "start_ms": 170, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "꼭", "start_ms": 270, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "하세요", "start_ms": 350, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "잘 할 수 있을 거예요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "잘", "start_ms": 45, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 125, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 205, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "있을", "start_ms": 265, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 365, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "스스로를 믿고 계속 노력하세요. 여러분의 노력을 응원합니다.",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "스스로를", "start_ms": 60, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "믿고", "start_ms": 260, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 380, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "노력하세요", "start_ms": 500, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "여러분의", "start_ms": 750, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "노력을", "start_ms": 950, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "응원합니다", "start_ms": 1130, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "Keep going and never look back.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Keep", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "going", "start_ms": 170, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "and", "start_ms": 320, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "never", "start_ms": 400, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "look", "start_ms": 530, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "back", "start_ms": 630, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "거의 끝났습니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "거의", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "끝났습니다", "start_ms": 160, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "마지막 한 걸음만 더",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "마지막", "start_ms": 55, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 205, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "걸음만", "start_ms": 285, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 435, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "완벽하게 해내셨어요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "완벽하게", "start_ms": 40, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "해내셨어요", "start_ms": 260, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "오늘의 연습은 여기까지입니다. 정말 대단해요.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "오늘의", "start_ms": 60, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "연습은", "start_ms": 240, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "여기까지입니다", "start_ms": 390, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 740, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "대단해요", "start_ms": 860, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "기초가 탄탄해야 실력이 늘어납니다",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "기초가", "start_ms": 45, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "탄탄해야", "start_ms": 195, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "실력이", "start_ms": 375, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "늘어납니다", "start_ms": 515, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "반복 학습을 생활화하세요. 그것이 비결입니다.",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "반복", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "학습을", "start_ms": 170, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "생활화하세요", "start_ms": 310, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "그것이", "start_ms": 610, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "비결입니다", "start_ms": 760, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "언어는 공부가 아니라 훈련입니다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "언어는", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "공부가", "start_ms": 190, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 330, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "훈련입니다", "start_ms": 450, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "꾸준함이 천재를 이깁니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "꾸준함이", "start_ms": 55, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "천재를", "start_ms": 255, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이깁니다", "start_ms": 405, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "포기하지 않는 마음이 가장 소중합니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "포기하지", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "않는", "start_ms": 220, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "마음이", "start_ms": 320, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 460, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "소중합니다", "start_ms": 580, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "자신감을 가지고 큰 소리로 말해보세요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "자신감을", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "가지고", "start_ms": 250, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 390, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "소리로", "start_ms": 470, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "말해보세요", "start_ms": 620, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "틀려도 괜찮습니다. 시도하는 자체가 중요해요.",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "틀려도", "start_ms": 60, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "괜찮습니다", "start_ms": 210, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "시도하는", "start_ms": 460, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "자체가", "start_ms": 640, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "중요해요", "start_ms": 780, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "당신의 꿈을 위해 오늘 한 걸음 더 나아갔습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "당신의", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "꿈을", "start_ms": 190, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "위해", "start_ms": 310, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 410, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 530, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "걸음", "start_ms": 610, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 730, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "나아갔습니다", "start_ms": 810, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "You are doing an amazing job.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "You", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "are", "start_ms": 145, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "doing", "start_ms": 225, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "an", "start_ms": 375, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "amazing", "start_ms": 455, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "job", "start_ms": 675, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "항상 당신의 열정을 응원할게요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "항상", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "당신의", "start_ms": 200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "열정을", "start_ms": 350, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "응원할게요", "start_ms": 530, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "오늘 하루도 수고 많으셨습니다. 안녕히 가세요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "하루도", "start_ms": 160, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "수고", "start_ms": 310, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "많으셨습니다", "start_ms": 430, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 730, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 910, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "See you again in the next class.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 55, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 155, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "again", "start_ms": 235, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 385, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 465, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "next", "start_ms": 545, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "class", "start_ms": 685, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "Goodbye and take care.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Goodbye", "start_ms": 40, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "and", "start_ms": 260, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "take", "start_ms": 360, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "care", "start_ms": 480, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "다음에 또 뵙겠습니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 230, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "뵙겠습니다", "start_ms": 330, "dur_ms": 280, "phase_lr_deg": 0.0}
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
*Generated by WKAppBot ask gemini — 2026-04-03 04:36:32*
