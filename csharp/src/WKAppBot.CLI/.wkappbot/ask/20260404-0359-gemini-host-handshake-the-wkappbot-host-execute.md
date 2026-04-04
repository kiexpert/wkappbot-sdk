# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=16288  time=03:58:21  uptime=12112s  mem=723MB  system_processes=363"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260404_035800.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 512-362 316-136-310-310-136-310-361-312-217-237 765-706-103-130-103-103-132-312-106-170-106-103-103-320-312-537-132-102-132-103-126-601-506-301-157-501-657-675-123-127-576-570-5_V.mp3)
  Segment 2: 1576ms ~ 7300ms (file: 267-623-236-362-267-167-160-317-627-627-765-213-627-627-267-306-361-613-160-672-230-315-316-271-236-621-361-126-106-103-512-162-162-102-106-105-310-123-571-236-260-237-761-203-2_V.mp3)
  Segment 3: 8300ms ~ 8876ms (file: 102-160-106-176-126-132-567-572-517-567-576-576-756-751-176-671-176-712-162-123-103-361-206-173-123 574-576 217_W.mp3)
  Segment 4: 9876ms ~ 10452ms (file: 136-361-130-163-103-136-103-567-316-130-136-103-137-103-163-103-316-316-136-163-136-316-136-103-176-126-567-362-517-132-132-316-312-316-312-136-162-126_V.mp3)
  Segment 5: 11452ms ~ 12028ms (file: 236-675-176-213 612-165-106-103-167-103-136-103-160-612-102-621-312-156-136-612-126-652-601-263-612-562-173-271-367-167-106-136-176-163-163-163-163-137-106-316-170-160-162-163-1_V.mp3)
  Segment 6: 13028ms ~ 13604ms (file: 627-301-310-301-310-132-103-601-150-570-157-105-175-132-761-712-163-102-167-137-316-610-103-164-157-623-163-106-167-106-103-137-160-103-610-670-651-612-617-260-260-123-610-612-6_V.mp3)
  Segment 7: 14604ms ~ 15180ms (file: 675-367-762-637-672-673-637-765-671-672-627-673-673-673-675-672-673-673-673-376-673-673-762-675-675-637-675-675-765-672-673 762-672-672-675_W.mp3)
  Segment 8: 16180ms ~ 16756ms (file: 465-756-671-620-601-627-672-260-620-261-621-261-620-206-621-260-216-261-620-620-260-621-602-216-261-216-620-206-316-260-620-206-602-627-625-260-260-621-621-620-602-627-267-362-2_W.mp3)
  Segment 9: 17756ms ~ 18332ms (file: 162-136-103-541-215-123-103-167-106-103-123-132-263-561-152-175-173-672-527-516-167-102-123_V.mp3)
  Segment 10: 19332ms ~ 19908ms (file: 362-321-327-321-327-327 162-162-321-620 162-567-135-213-512-523-136-136 103-312-103-176-167-107-263-361-236-602-601-236 167-176-672-576-657-576-263-527-536-316-326-632-567-657-6_W.mp3)
  Segment 11: 20908ms ~ 21484ms (file: 136-617-612-126-625-261-260-623-612-162-106-671-675-567-103-152-103-103-136-316-315-576-316-367-173-103-316-536-715-762-173-631-103-106-136-103-673-316-316-165-103-351-103-761-3_V.mp3)
  Segment 12: 22484ms ~ 23060ms (file: 312-123-621-621-623-263-621-621-621-263-624-620-621-621-624-623-264-623-625-627-620-627-625-265-624-627-621-261-261-623-620-623-621-621-623-264-627-625-621-625-264-267-623-623-6_W.mp3)
  Segment 13: 24060ms ~ 24636ms (file: 361-316-235-652-623-216-236-352-352-625-215-236-362-652-657-657-657-657-657-657-265-265-235-326-325-256-352-235-321-327-732-327-231-312-327-376-372-327-375-752-752-275-275-275-2_W.mp3)
  Segment 14: 25636ms ~ 35860ms (file: 136-613-136-136-413-617-134-230-270-310-231-627-627-276-275-257-260-753-756-163-260-376-261-321-130-203-103-103-317-103-317-260-270-260-321-725-260-210-103-621-302-203-756-137-1_W.mp3)
  Segment 15: 36860ms ~ 37436ms (file: 621-620-620-625-621-625-627-267-267-621-263-261-261-261-620-621-620-621-620-620 620-601-625-627-625-621-267-267-267-627-261-265-267-265-265-627-261-267-261-261-261-263-672-672-5_W.mp3)
  Segment 16: 38436ms ~ 39012ms (file: 156 176-267 601-601-612-306-621-106-103-103-132-103-567-132-165-367-106-130-137-132-137-136-132-137-163-150-671-153-106-103-106-103-562-162-163-137-136-103-316-103-130-216-135-1_V.mp3)
  Segment 17: 40012ms ~ 40588ms (file: 576 321-236-263-102-617-167-106-657-612-106-132-267-617-167-312-103-163-130-163-163-103-651-765-716-105-160-612-165-612-103-173-106-671-103-136-160-167-627-671-602_V.mp3)
  Segment 18: 41588ms ~ 42164ms (file: 327-267-627-231-267-276-267-267-267-267-265-527-523-524-537-537-531-315-327-361-216-524-527-524-256-261-236-213-231-276-261-216-627-627-267-267-236-267-263-267-256-253-236-362-6_W.mp3)
  Segment 19: 43164ms ~ 43740ms (file: 574-576-756-765-617-362-365-763-671-756-721-216-162-106-310-316-301-301-130-132-103-132-201-760-602-504-157-153-312-132-362-623-612-206-260-206-231-103-136-132-312-670-761-726-3_V.mp3)
  Segment 20: 44740ms ~ 45316ms (file: 216-561-716-136-162-163-613-362 326-263-267_V.mp3)
  Segment 21: 46316ms ~ 46892ms (file: 451-517-167-762-157 657-765-765-761-162-312-123-132-263-361 765-765 756-756-130-761-103-103-103-163-105-136-103-312-103-135-153-316-103-136-103-153-136-316-632-715-163-761-136-1_V.mp3)
  Segment 22: 47892ms ~ 48468ms (file: 301-361-316-132-705-750-137-107-107-601-672-651-507-567-563-357-761-106-136-136-316-316-130-310-102-312-302-206-123-675-302-162-176-716-312-302-301-132-236-526-572-310-130-130-1_V.mp3)
  Segment 23: 49468ms ~ 50044ms (file: 362-670-365-136-163 657 732-213-376-362-602-576-703-675-537-506-657-156-716-236-237-236-267-263-263-260_V.mp3)
  Segment 24: 51044ms ~ 51620ms (file: 132-312-301-103-316-136-103-136-103-316-103-130-130-132-102-136-103-136-106-653-326-365-123-316-130-163-130-163-136-310-126-163-260-173-162-617-163-103-162-102-301-132-130-103-3_V.mp3)
  Segment 25: 52620ms ~ 58884ms (file: 132-215-123-251-275-123-102-127-123-175-571-135-572-257-231-320-230-752-572-120-213-230-103-201-207-260-260-125-213-125-105-103-321-236-260-276-260-527-572-201-126-102-132-302-7_V.mp3)
  Segment 26: 59884ms ~ 60460ms (file: 710-176-176-701-107-107-716-612-126-176-761-176-132-765-765-765-756-765-756-176-163-603-163-103-106-574-162-213-130-106-617-612-103-167-360-102-103-312-106-167-176-167-623-631-6_V.mp3)
  Segment 27: 61460ms ~ 62036ms (file: 627-632-302-302-160-620-603-705-173-672-106-136-136-301-651-136-103-162-163-607-620-602-167-106-206-670-301-206-203-231-621-632-632-162-201-326-765-671-546-567-675-765-265-206-6_V.mp3)
  Segment 28: 63036ms ~ 63612ms (file: 132-160-163-126-136-162-170-671-163-765-765-675-675-670-670_V.mp3)
  Segment 29: 64612ms ~ 65188ms (file: 127-103-750-576-316-163-106-265-576-756-612-361-132-312-102-123-132-132-203-273-167-106-160-716-167-207-620-362-326-103-167-132-106-123-631-620-320-321-321-136-765-216-106-102-1_W.mp3)
  Segment 30: 66188ms ~ 66764ms (file: 163-312-136-316-301-312-312-310-136-163-136 567-136-103-612-136-167-106-136-601-103-621-162-103-160-267-126-132-136-316-567-123-316-617-612-162-361-316-130-103-136-675-132-163-1_V.mp3)
  Segment 31: 67764ms ~ 68340ms (file: 167-301-623-163-130-103-103-163-130-130-103-571-102-631-362-362-632-136-136-361-361-130-136-312-136-163-102-103-301-130-162-621-106-312-163-106-162-361-152-231-176-132-130-103-6_V.mp3)
  Segment 32: 69340ms ~ 69916ms (file: 620-627-620-627-602-627-620-602-675-763-263-236-567-102-361-135-130-326-236-231-236-623-623-302-302-265-263-657-260-627-231-627-620-103-316-162-130-103-621-615 563-256-326-321 2_W.mp3)
  Segment 33: 70916ms ~ 71492ms (file: 605-650-650-657-605-657-605-670-607-675-675-675-672-670 610-605-605-605-607-605-607-670-607-605-627-670-672-675-672 603-650-650-601-603-605-620-607-602-605-602-602-603-621-612-6_W.mp3)
  Segment 34: 72492ms ~ 73068ms (file: 521-126-265-236-231-321-621-631-267-762-574-570-574-572-751-172-675-163-167-136-316-316-362-705-672-675-672-627-617-671-610-617-607 627 617-671-672-216-236-623-361-173-612-162 2_V.mp3)
  Segment 35: 74068ms ~ 74644ms (file: 367-126 126-547-126-156-132-162-163-103-123-213-106-132-217-136-301-301-132-327-145-123-167-163-103-103-163-163-103-560-152-612-123-623-120 136-105-627-162-163-103-137-103-153-1_V.mp3)
  Segment 36: 75644ms ~ 76220ms (file: 637-673-627-276-672-261-623-263-365-136 574-765_W.mp3)
  Segment 37: 77220ms ~ 77796ms (file: 103-236-206-527-725-750-207-547-657-620-372-756-627-620-326-263-267-260-260-206-261-213-321-321-572-302-320-327-250-265-372-753-532-362-327-312-203-230-203-302-326-321-231-326-6_W.mp3)
  Segment 38: 78796ms ~ 79372ms (file: 750-753-751-130-123-574-567-657-607-310-136-316-360-301-103-106-102-103-103-106-130-163-136-163-137-167-316-136-103-314-312-317-361-612-206-106-263-312-103-310-136-316-312-126-3_V.mp3)
  Segment 39: 80372ms ~ 80948ms (file: 126 236-160-106-650-136-160-316-103-107-136-136-547-621-130-130-163-130-601-136-362-163-601-670-670 236-213-236-136-132-316-103-103-317-316-310-132-167-103-136-136-321-136-136-1_V.mp3)
  Segment 40: 81948ms ~ 82524ms (file: 265-265-621-263-265-261-621-267-263-621-621-625-612-265-652-652-562-562-652-651-653-625 621-623-625-623-206-620-620-265-625-625-621-265-264-261-264-625-265-625-265-625-652-256-5_W.mp3)
  Segment 41: 83524ms ~ 84100ms (file: 302-312-132-103-106-106-617-167-617-320-632-230-236-320-230-213-132-106-103-136-170-107-130-132-603-621-610-302-507-567-150-310-206-206-260-263-236-203-260-263-203-230-203-206 7_W.mp3)
  Segment 42: 85100ms ~ 85676ms (file: 675-675-657-756-756-572-652-537-567-163-132-231-265-617-657-657-567-576-567-567-130-132-106-365-630-361-136-167-103-160-136-312-631-310-136-103-163-153-136-715-136-105-617-316-1_W.mp3)
  Segment 43: 86676ms ~ 87252ms (file: 275 574-547-567-576-103 176-176 317-372-302-132-107-273-207-107-301-620-302-625-652_W.mp3)
  Segment 44: 88252ms ~ 88828ms (file: 612-163-310-136-361-163-617-136-514-136-162-123-162-560-561-153-106-612-627-103-621-312-361-613-163-162-136-673-617-631-362-316-617-312-631-362-362-657-765-675-765-761-765-657-6_W.mp3)
  Segment 45: 89828ms ~ 90404ms (file: 610-617-326-612-627-672-102-126-103-162-612-136-621-316-106-216-316-163-103-103-130-123-103-103-276-723-702-601-302-162-610-321-321-102-103-103-136-127-361-602-621-260-610-612-6_V.mp3)
  Segment 46: 91404ms ~ 91980ms (file: 623-263-261-261-623-621-265-261-263-216-263-261-265-265-263-261-263-263-612-612-621-261-621-627-215-261-261-267-261-261-265-265-265-261-267-267-261-236-213-216-132-267-216-236-1_W.mp3)
  Segment 47: 92980ms ~ 93556ms (file: 574-576-567-576-567-756-567-657-657-675-576-275-267-526-657-567-615-651-657-561-567-765-657-567-657-576-567-576-576-576-132 160-163-216-210-213-234-240-672-267-627-621 132-627-2_W.mp3)
  Segment 48: 94556ms ~ 95132ms (file: 321-132-576-316-672-216-126-163-316-673-163-160-136-132-126-564-136-163-167-613-163-160-103-107-103-167-612-136-672-623-321-361-103-547-132-130-726-326-123-106-675-613-615-163-1_V.mp3)
  Segment 49: 96132ms ~ 96708ms (file: 761-763-760 672 267-261 321 132-613-165-621-562-216-753-276-265-561 132-103-103-512-137-712-175-613-103-102-103-167-107-136-167-103-136-106-316-136-130-310-123-132-350-763-124-3_V.mp3)
  Segment 50: 97708ms ~ 98284ms (file: 132-132-136-136-574-574 162 321 673-671-617-263-162-106-601-670-617-610-671-675-761-761-736-761-263-260-172-712-630-627-162-106-176-756-317-376-673-163-106-610-130-312-631-361-1_W.mp3)
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
> **Time**: 2026-04-04 03:59:10
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "실제로 저희가 이 로봇이 사용하게 될 환경이 이렇게 완벽하지 않을 가능성이 있기 때문에 저희가 이런 데에서 실험도 진행",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "실제로", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "저희가", "start_ms": 185, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 310, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "로봇이", "start_ms": 335, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "사용하게", "start_ms": 420, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "될", "start_ms": 525, "dur_ms": 51, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "월화수목금 모아가려 그랬는데 다행히 운이 좋게 밤에 트럼프가 이제 종전 얘기를 하겠다 이란과 협상하겠다 호르무즈 해협 개방 안 하고 그냥 종식하겠다 그래서 미장이 급등해서 아침에 욕심 없이",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "월화수목금", "start_ms": 60, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "모아가려", "start_ms": 465, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "그랬는데", "start_ms": 720, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "다행히", "start_ms": 1050, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "운이", "start_ms": 1300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "좋게", "start_ms": 1450, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "밤에", "start_ms": 1700, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "트럼프가", "start_ms": 1950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 2300, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "종전", "start_ms": 2550, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "얘기를", "start_ms": 2850, "dur_ms": 2874, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "전 세계적으로 박사 PhD라는 지성의 상징이 월 3만 원짜리 AI 구독제보다도 못한 취급을 받고 있는 건데요 오늘은",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "전", "start_ms": 45, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "세계적으로", "start_ms": 90, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "박사", "start_ms": 250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "PhD라는", "start_ms": 360, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "지성의", "start_ms": 515, "dur_ms": 61, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 5,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 6,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 7,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 8,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 9,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 10,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 11,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 12,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 13,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 14,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 15,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 16,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 17,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 18,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 19,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 20,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 21,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 22,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 23,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 24,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 25,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 26,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 27,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 28,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 29,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 30,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 31,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 32,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 33,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 34,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 35,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 36,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 37,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 38,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 39,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 40,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 41,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 42,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 43,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 44,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 45,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 46,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 47,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 48,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 49,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 50,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-04 03:59:10*
