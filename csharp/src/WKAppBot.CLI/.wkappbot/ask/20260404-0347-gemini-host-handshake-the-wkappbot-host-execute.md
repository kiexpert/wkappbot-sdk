# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=34276  time=03:44:57  uptime=10422s  mem=740MB  system_processes=364"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260404_034448.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 615-567-163-123-160-103-163-126-671-135-102-156-103-136-103-167-615-761-163-276_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 621-106-103-103-132-137-316-316-136-135-103-132-317-675-672-756-627-607-765-617-670 214 316 312 607-627-756-756-705-756-721-576-365-357-351-317-327-325-213-371-235-156-105-135-3_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 321 176-136-310 706 574-576-576-756-756-756-123-126-571-136-632-261-326-302-326-576-756-765-765-762-126-231-321_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 610-167-123-103-570-316-132-130-130-106-160-136-610-130-136-103-136-136-157-106-103-136-103-671-316-103-163-561-137-357-163-106-631-132-150-163-173-136-132-137-136-136-103-105-5_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 632-123-261-213-312-132-120-102-210-256-123-361-316-165-623-612-130-103-103-132-103-610-612-176-136-163-136-167-610-316-176-615-156-675-675-671-675-627-160-603-673-106-674-106-6_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 620-362-230-320-230-102-132-102-162-612-612-620-260-102-610-613-130-326-135-315-315-321-123-103-120-201-102-123-132-312-236-102-301-123-502-123-132-103-351-630-103-630-360-316-1_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 517-136-102-756-261-675-765-617-136-362-160-617-132-605-163-756-627-715-571-754_V.mp3)
  Segment 8: 11032ms ~ 19384ms (file: 310-103-312-132-310-273-765-126-231-572-312-213-231-723-172-572-130-310-752-276-321-731-123-312-237-312-201-765-672-756-317-765-765-765-701-761-763-765-753-310-120-514-107-102-1_W.mp3)
  Segment 9: 20384ms ~ 20960ms (file: 765-756-576-576-756-756-756-705-756-312-132-135-572-136-327-103-236-261-163-163-103-106-160-103-103-136-103-106-103-152-123-571-107-167-564-276-135-316-162-103-135-120-517-162-1_V.mp3)
  Segment 10: 21960ms ~ 22536ms (file: 540-326-102-136-130-106-162-106-675-672-260-260-765-762-206-265-520-162-106-106-103_V.mp3)
  Segment 11: 23536ms ~ 31600ms (file: 271-231-160-213-261-120-267-236-132-751-176-754-756-672-762-756-675-162-163-136-136-216-621-675-267-263-621-613-613-617-621-672-627-627-765-756-567-536-673-261-132-167-571-126-2_W.mp3)
  Segment 12: 32600ms ~ 33176ms (file: 251-267-261-627-627-276-263-321-261-263-256-216-216-213-216-126-216-126-612-361-261-236-213-216-321-123-132-123-326-236-231-261-267-265-627-326-213-231-312-132-312-213-321-261-2_W.mp3)
  Segment 13: 34176ms ~ 34752ms (file: 316-130-750-763-103-107-176-170-716-715-170-137-317-137-301-163-632-516-136-103-103-103-137-103-134-103-103 751-176-130-157-765-301-631-236_V.mp3)
  Segment 14: 35752ms ~ 36328ms (file: 506-154-152-672-612-612-617-621-671-621-263-260-206-562-216-132-620-620-627-627-312-321-316-316-301-316-362-607-627-267-206-263-203-203-231-103-136-136-132-136-132 756-753-765-6_W.mp3)
  Segment 15: 37328ms ~ 37904ms (file: 607-671-761-671-621-675-637-361-301-136-132-312-314-103-132-103-132-602-672-627-301-103-132-103-136-132-741-132-174_V.mp3)
  Segment 16: 38904ms ~ 39480ms (file: 130-310-326-216-625-267-672-167-632-627-675-572-637-627-627-672-623-367-632-635-632-635-612-362-632-326 123-213-263-265-635-652-617-652-756-627-675-652-623-632-627-627-625-267-6_W.mp3)
  Segment 17: 40480ms ~ 41056ms (file: 160-615-601-510-570-360-306-236-302-720-763-371-367-631-753-710-317-617-762-376-620-632-236-326-230-120-231-321-302-320-132-362-230-236-130-127-765-675-627-672-326-236-630-106-6_V.mp3)
  Segment 18: 42056ms ~ 42632ms (file: 607-603-613-761-763 132-126-312-751-754-750-137-301-136-103-107-301-305-502-275-312-132-136-123-106-163-103-574-570-175-132-136-316-612-316-176-160-167-136-167-106-507-547-367-6_V.mp3)
  Segment 19: 43632ms ~ 44208ms (file: 132-635-165-136-103-102-132-136-130-376-106-136-156-627-137-170-756-576-567-657-567-657-726-657-657-506-675-760-726-721-267-236 765-217-165-613-265_W.mp3)
  Segment 20: 45208ms ~ 45784ms (file: 132-103-103-167-136-167-260-326-320-102-136-127-301-130-627-160-361-503-562-625-615-102-675-675-765-751-671-310-360-207-702-621-672-670-613-163-625-567-567-567-657-576-567-576-6_W.mp3)
  Segment 21: 46784ms ~ 47360ms (file: 236-261-123-610-623-103-617 312-176-231_V.mp3)
  Segment 22: 48360ms ~ 48936ms (file: 261-271-231-127-132-672-672-672-617-136-163-613-627-632-631-627-652-623-621-362-632-362-657-627-637-632-623-627-623-671-762-263-326-756-672-672-316-602-620-627-263-263-123-261-2_W.mp3)
  Segment 23: 49936ms ~ 50512ms (file: 123-621-527-162-135-126-521-136-312-321-236-326-236-532-526-521-267-256-152-125-165-156-651-126-153-165-163-136-126-123-126-126-136-132-123-153-513-751-617-621-162-261-621-627-3_W.mp3)
  Segment 24: 51512ms ~ 52088ms (file: 103-162-105-103-103-756-276-236-213-132-126-301-326-362-326-301-310-302-576-657-625-260-263-321-302-310-362-301-302-321-302-321-763-673-163-103-106-106-106-756-362-312-103-613-1_V.mp3)
  Segment 25: 53088ms ~ 53664ms (file: 231-236-216-572-547-547-536-253-326-301-310-320-120-310-367-630-316-564-510-321-206-236-726-761-627-260-260-206-310-570-576-576-576-756-576-657-547-576-576-570-675-620-562-560-5_W.mp3)
  Segment 26: 54664ms ~ 55240ms (file: 761-623-263-263-236-263-362-327-672-627-627-617-513-513-615-621-263-236-623-267-623-672-263-267-267-267-627-627-267-267-627-627-627-672-627-267-263-263-267-260-267-267-627-267-2_W.mp3)
  Segment 27: 56240ms ~ 56816ms (file: 267-263-236-213-627-652-607-620-627-620-265-625-256-265-123-312-132-132-267-267-516-106-326-203-362-267-413-213-213-231-312-763-760-726-632-302-310-301-302-326-320-326-635-307-6_W.mp3)
  Segment 28: 57816ms ~ 58392ms (file: 163-326-316-316-137-517-317-137-571-576-127-630-163-726-257-125-216-315-132-321-126-231-261-613-526-125-672-152-657-756-572-657 231-235-256-625-261-137-376-673-705-375-621-571-5_V.mp3)
  Segment 29: 59392ms ~ 69580ms (file: 106-236-103-132-137-753-765-763-237-273-270-547-230-237-756-756-613-231-327-762-301-320-276-276-756-270-207-547-570-751-103-574-107-312-103-103-175-175-317-732-726-726-276-207-2_W.mp3)
  Segment 30: 70580ms ~ 71156ms (file: 671-165-612-162-672-672-267-612-612-617-176-236-612-615-635-673-673-672-265-365-361-627-625-627-360-632-627-306-637-360-653-671-637-632-615-631-321-103-102-316-130-362-623-260-3_W.mp3)
  Segment 31: 72156ms ~ 72732ms (file: 301-613-260-351-150-756-765-571-571-137-576-576-507-163-652-263-231-706-761-132-317-157-316-103-130-167-163-137-136-316-176-132-163-321-132-130-623-263-267-762-670-301-560-574-5_V.mp3)
  Segment 32: 73732ms ~ 74308ms (file: 753-251-327-256-231-251-213-562-627-123-132-207-207-207-263-236_W.mp3)
  Segment 33: 75308ms ~ 75884ms (file: 571-275-321-302-210-132-310-312-201-352-326-316-132-301-312-321-132-710-176-316-360-752-607-263-625-652-267-267-263-326-362-263-361_W.mp3)
  Segment 34: 76884ms ~ 77460ms (file: 163-621-672-627-317-576-576-576-267-257-521-152-135-301-132-574-573-752-136-167-162-574-570-237-267-267-231-236-263-132-103-513-517-753-751-712-213-231-321-213-213-574-574-754-7_V.mp3)
  Segment 35: 78460ms ~ 79036ms (file: 617-367-675-673-762-763-276-765-762 621-172-713-276-627-126-263-127-312-136-132-326-176-123-132-761-167-176-132-713-326-327-716-176-637-615-176-617-672-613-670-160 136-752-765-1_W.mp3)
  Segment 36: 80036ms ~ 80612ms (file: 260-203-230-302-321-130-132-620-126-162-362-312-362-736-632-306-316-750-371-765-627-367-630-302-675-632-206-562-360-630-620-267-750-675-725-726-306-236-263-310-675-670-765-706-7_W.mp3)
  Segment 37: 81612ms ~ 82188ms (file: 162-102-170-106-615-127-156-261-126-130-126-130-136-163-137-136-142-105-136-152-165-126-106-167-675-617-163-127-136-167-136-102_V.mp3)
  Segment 38: 83188ms ~ 92296ms (file: 756-103-751-102-627-123-312-756-756-510-260-756-107-756-627-106-106-103-163-167-106-637-267-260-302-257-673-756-572-561-123-103-132-675-267-627-627-625-756-675-267-576-137-276-1_W.mp3)
  Segment 39: 93296ms ~ 93872ms (file: 317-136-103-136-137-137-137-316-361-136-103-132-615-316-176-136-136-361-201-163-163-167-136-617-136-617-156-130-316-312-103-136-103-567-312-132-103-135-567-263-267-620_V.mp3)
  Segment 40: 94872ms ~ 95448ms (file: 130-136-163-130-132-137-162-137-315-756-615-705-102-132-136-136-150-150-103-156-167-167-162-756-516-752-576-756-715-726-716-713-762-671-760-756-761-736-627-106-167-132 612-613-7_W.mp3)
  Segment 41: 96448ms ~ 97024ms (file: 301-570-571-157-103-132-316-136-351-102-102-136-135-517-570-753-765-176-106-103-162-103-124-756-756-732-371-570-571-574-574-576-756-750-173-103-135-750-312-213-321-312-136-103-1_V.mp3)
  Segment 42: 98024ms ~ 98600ms (file: 163-276-762-276-672-672-673-637-625-627-625-657-625-657-652-657-652-652-625-625-317-207-107-103-301-726-725-217-316-231-312-132-213-263-213-261-231-574-756-765-675-673-167-213-2_W.mp3)
  Segment 43: 99600ms ~ 100176ms (file: 672-267-653-756-136-750-275-260-526-576-725-702-206-527-162-132-670-765-670-106-263-301-507-176-165-547-710-670-671-625-613-621-162-103-103-103-173-123-132-103-176-132-631-360-1_V.mp3)
  Segment 44: 101176ms ~ 101752ms (file: 163-163-136-127-605-765-167-103-167-137-103-137-163-130-301-132-170-103-163-630-162-156-103-132-160-136-106-103-103-312-106-176-167-137-160-126-176-106-127-103-106-126-136-106-1_V.mp3)
  Segment 45: 102752ms ~ 103328ms (file: 132-132-312 210-260-216-261-263-631-312-130-102-103-102-263-321-136-106-103-103_V.mp3)
  Segment 46: 104328ms ~ 104904ms (file: 765-765-761-751-751-765-751-756-756-765-576-576-576-576-576-571-576-657-615-675-760-176-751-716-176-172-762-716-672-761-617_W.mp3)
  Segment 47: 105904ms ~ 111880ms (file: 160-613-213-321-132-126-130-276-236-320-321-371-162-120-123-213-312-316-312-321-102-137-723-723-103-102-123-263-236-765-657-216-752-312-106-103-173-321-321-231-321-132-312-130-1_W.mp3)
  Segment 48: 112880ms ~ 113456ms (file: 213-310-126-102-102-165-571-756-103-315-215-765-752-670-761-716-756-765-765-765-765-675-765-176-610-170-760-673-673-671-617-673-657-623-265-627-652-761-756-627-362-372-216-517-5_V.mp3)
  Segment 49: 114456ms ~ 115032ms (file: 263-236-261-762-267-627-236-136-132-134-531-537-132-173-136-576-765-760-624-613-762-713-127-152-257-312-301-132-126-371-103-261-672-602-326-507-572-572-735-705-701-237-230-256-2_V.mp3)
  Segment 50: 116032ms ~ 116608ms (file: 103-106-106-152-132-106-632-176-162 537-163-176-176-716_V.mp3)
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
> **Time**: 2026-04-04 03:47:17
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "원래 옛날 관점으로 매매를 했으면 그날 아마 풀베팅을",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "원래", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "옛날", "start_ms": 180, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "관점으로", "start_ms": 310, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "매매를", "start_ms": 480, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "했으면", "start_ms": 630, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "그날", "start_ms": 760, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "아마", "start_ms": 870, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "풀베팅을", "start_ms": 960, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "해서 다음날 장렬히 전사했을 거예요 근데 이제 형님은 이제 비중 조절을 잘하시는데",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "해서", "start_ms": 30, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "다음날", "start_ms": 160, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "장렬히", "start_ms": 310, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "전사했을", "start_ms": 470, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 660, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 810, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 930, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "형님은", "start_ms": 1040, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 1180, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "비중", "start_ms": 1290, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "조절을", "start_ms": 1420, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "잘하시는데", "start_ms": 1580, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "있을 때 내가 이제 비중 베팅을 하기 위해서 참고 있는 거다 근데 시나리오대로 하셨고 오늘 이제 말씀하신 대로 이제 일당 1억을 찍으셔 가지고 딱 원하시는 대로 다 됐잖아요 음 계획대로 기분 좋아",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "있을", "start_ms": 15, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "내가", "start_ms": 250, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 370, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "비중", "start_ms": 480, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "베팅을", "start_ms": 610, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "하기", "start_ms": 770, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "위해서", "start_ms": 890, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "참고", "start_ms": 1030, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 1150, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "거다", "start_ms": 1250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 1360, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "시나리오대로", "start_ms": 1480, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "하셨고", "start_ms": 1710, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 1870, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 1990, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "말씀하신", "start_ms": 2090, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "대로", "start_ms": 2280, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 2390, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "일당", "start_ms": 2490, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "1억을", "start_ms": 2620, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "찍으셔", "start_ms": 2770, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "가지고", "start_ms": 2940, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "딱", "start_ms": 3090, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "원하시는", "start_ms": 3170, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "대로", "start_ms": 3330, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 3440, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "됐잖아요", "start_ms": 3510, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "음", "start_ms": 3680, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "계획대로", "start_ms": 3760, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "기분", "start_ms": 3950, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "좋아", "start_ms": 4070, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 5,
"transcript": "저희는 대규모 다리 건설에 적용되는 레고형 조립식 강화 강철 교각 블록 이런 것들을 개발하면서 박사 학위를 이제 취득을 했습니다",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "저희는", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "대규모", "start_ms": 180, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "다리", "start_ms": 330, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "건설에", "start_ms": 450, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "적용되는", "start_ms": 620, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "레고형", "start_ms": 810, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "조립식", "start_ms": 950, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "강화", "start_ms": 1100, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "강철", "start_ms": 1220, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "교각", "start_ms": 1350, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "블록", "start_ms": 1470, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 1600, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "것들을", "start_ms": 1710, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "개발하면서", "start_ms": 1840, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "박사", "start_ms": 2050, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "학위를", "start_ms": 2170, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 2310, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "취득을", "start_ms": 2410, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "했습니다", "start_ms": 2560, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "이제 논문에서 산업 결과로 평가의 기준을 바꾸겠다는 겁니다 일부 사람들은 요즘 실리콘밸리에서",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "논문에서", "start_ms": 140, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "산업", "start_ms": 280, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "결과로", "start_ms": 400, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "평가의", "start_ms": 530, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "기준을", "start_ms": 650, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "바꾸겠다는", "start_ms": 780, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "겁니다", "start_ms": 950, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "일부", "start_ms": 1070, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "사람들은", "start_ms": 1180, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "요즘", "start_ms": 1330, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "실리콘밸리에서", "start_ms": 1450, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "박기원 교수를 빛내주",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "박기원", "start_ms": 20, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "교수를", "start_ms": 210, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "빛내주", "start_ms": 370, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "의 경우 곧 급등이 나올 가능성이 높습니다 그래서 박스권 하단 부근에서 지지를 확인하면서 매수하거나 박스",
"speaker": "speaker_5",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "의", "start_ms": 30, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "경우", "start_ms": 80, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "곧", "start_ms": 210, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "급등이", "start_ms": 270, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "나올", "start_ms": 420, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "가능성이", "start_ms": 530, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "높습니다", "start_ms": 720, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 880, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "박스권", "start_ms": 1010, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "하단", "start_ms": 1160, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "부근에서", "start_ms": 1280, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "지지를", "start_ms": 1450, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "확인하면서", "start_ms": 1590, "dur_ms": 190, "phase_lr_deg": 0.0},
{"word": "매수하거나", "start_ms": 1790, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "박스", "start_ms": 2000, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 10,
"transcript": "1위를 가진 그룹이 되었는데요 이 기록으로 BTS는 빌보드 역사에서 비틀즈 슈프림스 비지스 롤링스톤스에 이어 다섯 번째로 1위 곡을 많이 보유한 그룹이 되었다고 합니다 여기에 더해",
"speaker": "speaker_6",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "1위를", "start_ms": 15, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "가진", "start_ms": 140, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "그룹이", "start_ms": 250, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "되었는데요", "start_ms": 390, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 580, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "기록으로", "start_ms": 620, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "BTS는", "start_ms": 780, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "빌보드", "start_ms": 950, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "역사에서", "start_ms": 1090, "dur_ms": 170, "phase_lr_deg": 0.0},
{"word": "비틀즈", "start_ms": 1270, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "슈프림스", "start_ms": 1430, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "비지스", "start_ms": 1600, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "롤링스톤스에", "start_ms": 1750, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "이어", "start_ms": 1980, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "다섯", "start_ms": 2070, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "번째로", "start_ms": 2170, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "1위", "start_ms": 2300, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "곡을", "start_ms": 2390, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 2490, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "보유한", "start_ms": 2600, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "그룹이", "start_ms": 2730, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "되었다고", "start_ms": 2870, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "합니다", "start_ms": 3040, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "여기에", "start_ms": 3160, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "더해", "start_ms": 3270, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "했다 범종 성덕대왕신종의 종소리를 샘플링한 트랙까지 이걸 들은 전 세계 팬들은 단지 노래가 좋다에서 멈추지 않고 여기에 나오는 종소리는 뭐지 여기에 나오는 전통음이 너무 좋다 한번 찾아볼까",
"speaker": "speaker_6",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "했다", "start_ms": 20, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "범종", "start_ms": 130, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "성덕대왕신종의", "start_ms": 260, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "종소리를", "start_ms": 570, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "샘플링한", "start_ms": 760, "dur_ms": 190, "phase_lr_deg": 0.0},
{"word": "트랙까지", "start_ms": 960, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "이걸", "start_ms": 1150, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "들은", "start_ms": 1270, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "전", "start_ms": 1380, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "세계", "start_ms": 1440, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "팬들은", "start_ms": 1560, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "단지", "start_ms": 1710, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "노래가", "start_ms": 1810, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "좋다에서", "start_ms": 1940, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "멈추지", "start_ms": 2130, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "않고", "start_ms": 2290, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "여기에", "start_ms": 2400, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "나오는", "start_ms": 2520, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "종소리는", "start_ms": 2650, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "뭐지", "start_ms": 2820, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "여기에", "start_ms": 2920, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "나오는", "start_ms": 3040, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "전통음이", "start_ms": 3170, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 3360, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "좋다", "start_ms": 3460, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "한번", "start_ms": 3570, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "찾아볼까", "start_ms": 3690, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "이거도시의 찬진짜 많이 노력을 하신 것 같아요 근데 또 제가 어떤 아이디어를 가지고 왔을 때 만약에 선배님",
"speaker": "speaker_7",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "이거도시의", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "찬진짜", "start_ms": 190, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 320, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "노력을", "start_ms": 420, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "하신", "start_ms": 540, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 650, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 700, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 820, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 910, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 970, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 1060, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "아이디어를", "start_ms": 1160, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가지고", "start_ms": 1320, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "왔을", "start_ms": 1430, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 1530, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "만약에", "start_ms": 1580, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "선배님", "start_ms": 1710, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "입니다 이 움직임 뉴스 보고는 절대 못 잡습니다 오늘은 왜 올랐는지가 아니라",
"speaker": "speaker_8",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "입니다", "start_ms": 25, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 140, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "움직임", "start_ms": 200, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "뉴스", "start_ms": 330, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "보고는", "start_ms": 430, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "절대", "start_ms": 550, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "못", "start_ms": 660, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "잡습니다", "start_ms": 720, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "오늘은", "start_ms": 860, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "왜", "start_ms": 990, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "올랐는지가", "start_ms": 1050, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 1220, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "(BGM) 로봇의 자세와 움직임을 제어하는",
"speaker": "speaker_9",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "mixed",
"words": [
{"word": "로봇의", "start_ms": 100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "자세와", "start_ms": 260, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "움직임을", "start_ms": 400, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "제어하는", "start_ms": 550, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "움직이기 전 준비 구간을 인지하는 것입니다 주도주가 매물 소화 박스권 형성 눌림 구간 등을 거치며 에너지를 모으는 흐름이",
"speaker": "speaker_10",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "움직이기", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "전", "start_ms": 180, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "준비", "start_ms": 250, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "구간을", "start_ms": 380, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "인지하는", "start_ms": 520, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "것입니다", "start_ms": 690, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "주도주가", "start_ms": 840, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "매물", "start_ms": 1000, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "소화", "start_ms": 1120, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "박스권", "start_ms": 1230, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "형성", "start_ms": 1370, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "눌림", "start_ms": 1500, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "구간", "start_ms": 1620, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "등을", "start_ms": 1730, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "거치며", "start_ms": 1820, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "에너지를", "start_ms": 1980, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "모으는", "start_ms": 2150, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "흐름이", "start_ms": 2280, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 17,
"transcript": "저희 로봇 같은 경우는 관절이 이제 12개인 그런 로봇인데요 그러면 그 각 관절마다 모터가 다 달려 있기 때문에 12개의 모터를 이 로봇이 원하",
"speaker": "speaker_11",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "저희", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "로봇", "start_ms": 140, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "같은", "start_ms": 270, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "경우는", "start_ms": 380, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "관절이", "start_ms": 520, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 670, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "12개인", "start_ms": 760, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "그런", "start_ms": 920, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "로봇인데요", "start_ms": 1030, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "그러면", "start_ms": 1220, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 1340, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "각", "start_ms": 1390, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "관절마다", "start_ms": 1450, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "모터가", "start_ms": 1620, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 1760, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "달려", "start_ms": 1820, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있기", "start_ms": 1930, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "때문에", "start_ms": 2030, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "12개의", "start_ms": 2160, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "모터를", "start_ms": 2310, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 2450, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "로봇이", "start_ms": 2500, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "원하", "start_ms": 2640, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "PCI 오버레이와 스모크 테스트",
"speaker": "speaker_12",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "PCI", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "오버레이와", "start_ms": 200, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "스모크", "start_ms": 430, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "테스트", "start_ms": 620, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "식당 안으로 들어가 봤는데요 안에는 줄이 더 깁니다 도대체 어떤 식당",
"speaker": "speaker_13",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "식당", "start_ms": 20, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "안으로", "start_ms": 160, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "들어가", "start_ms": 310, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "봤는데요", "start_ms": 440, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "안에는", "start_ms": 600, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "줄이", "start_ms": 740, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 840, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "깁니다", "start_ms": 900, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "도대체", "start_ms": 1020, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 1170, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "식당", "start_ms": 1280, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "네 단숨에 달려가 뜨거운 가슴에 푹 안길 수 있는 그렇게 보고 싶고 그리워하던 할머니입니다 (울음소리) 사랑해요 할아버지 저도 있어요 그동안 많이 컸죠",
"speaker": "speaker_14",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "mixed",
"words": [
{"word": "네", "start_ms": 15, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "단숨에", "start_ms": 100, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "달려가", "start_ms": 270, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "뜨거운", "start_ms": 420, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "가슴에", "start_ms": 560, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "푹", "start_ms": 720, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "안길", "start_ms": 790, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 910, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 960, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "그렇게", "start_ms": 1050, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "보고", "start_ms": 1190, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "싶고", "start_ms": 1300, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "그리워하던", "start_ms": 1420, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "할머니입니다", "start_ms": 1610, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "사랑해요", "start_ms": 2000, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "할아버지", "start_ms": 2250, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "저도", "start_ms": 2450, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 2570, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "그동안", "start_ms": 2720, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 2880, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "컸죠", "start_ms": 3010, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "기법은 없다 이러고 마시거든요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "기법은", "start_ms": 20, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "없다", "start_ms": 210, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "이러고", "start_ms": 340, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "마시거든요", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "동주야 계좌 관리가 생명이다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "동주야", "start_ms": 30, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "계좌", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "관리가", "start_ms": 310, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "생명이다", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "그러면 이제 거기에서 제가 그거에 대한",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "그러면", "start_ms": 10, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "거기에서", "start_ms": 250, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 380, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "그거에", "start_ms": 470, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "대한", "start_ms": 540, "dur_ms": 30, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "말씀을",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "말씀을", "start_ms": 50, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "해주신 거를 고민을 하는 거죠 너가",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "해주신", "start_ms": 15, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "거를", "start_ms": 170, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "고민을", "start_ms": 280, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 410, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "거죠", "start_ms": 500, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "너가", "start_ms": 550, "dur_ms": 20, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "월급을 많이 받을 확률이 높습니다",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "월급을", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 200, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "받을", "start_ms": 290, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "확률이", "start_ms": 390, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "높습니다", "start_ms": 500, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "그런데 더 충격적인 점은 이제",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "그런데", "start_ms": 25, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 160, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "충격적인", "start_ms": 230, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "점은", "start_ms": 420, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 510, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "또 논문을 중요시하는 것은 시대",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "또", "start_ms": 10, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "논문을", "start_ms": 80, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "중요시하는", "start_ms": 220, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "것은", "start_ms": 410, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "시대", "start_ms": 500, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "착오적이라는 이야기가 나오고",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "착오적이라는", "start_ms": 15, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "이야기가", "start_ms": 210, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "나오고", "start_ms": 350, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "있다는 겁니다 실제로 (BGM)",
"speaker": "speaker_3",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "mixed",
"words": [
{"word": "있다는", "start_ms": 20, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "겁니다", "start_ms": 170, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "실제로", "start_ms": 290, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 32,
"transcript": "다 나오고 신부님 나오셔서 소식을",
"speaker": "speaker_4",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "다", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "나오고", "start_ms": 120, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "신부님", "start_ms": 240, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "나오셔서", "start_ms": 350, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "소식을", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "전해주시는데요",
"speaker": "speaker_4",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "전해주시는데요", "start_ms": 20, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "(현장 음성 - 인도네시아)",
"speaker": "speaker_5",
"confidence": 0.85,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "인도네시아", "start_ms": 100, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "김치를 가져오셔서",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "김치를", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "가져오셔서", "start_ms": 250, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "제가 기다리다 안 나오셔서 먼저",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "제가", "start_ms": 15, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "기다리다", "start_ms": 120, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 270, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "나오셔서", "start_ms": 340, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "먼저", "start_ms": 480, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "왔거든요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "왔거든요", "start_ms": 10, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "이 아이로스 학회는 보통 한 논문 4000편 정도가 제출이 되고",
"speaker": "speaker_6",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 20, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "아이로스", "start_ms": 70, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "학회는", "start_ms": 180, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "보통", "start_ms": 260, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 320, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "논문", "start_ms": 360, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "4000편", "start_ms": 410, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "정도가", "start_ms": 490, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "제출이", "start_ms": 530, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "되고", "start_ms": 560, "dur_ms": 16, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "그중에서 한 반 정도가 이제 억셉을 받고요 1000",
"speaker": "speaker_6",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "그중에서", "start_ms": 30, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 150, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "반", "start_ms": 200, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "정도가", "start_ms": 260, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 340, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "억셉을", "start_ms": 390, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "받고요", "start_ms": 480, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "1000", "start_ms": 550, "dur_ms": 26, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 41,
"transcript": "지금처럼 유튜브를 운영하고 사업을 만들고 좋은 기회들은 가능성 없었을 거예요 현재 저를 평가하는 기준은 학위가 아니라 지금까지 제가 만들어온 결과물들이기 때문입니다 옛날",
"speaker": "speaker_7",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "지금처럼", "start_ms": 10, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "유튜브를", "start_ms": 400, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "운영하고", "start_ms": 820, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "사업을", "start_ms": 1210, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "만들고", "start_ms": 1570, "dur_ms": 360, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 1970, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "기회들은", "start_ms": 2290, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "가능성", "start_ms": 2680, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "없었을", "start_ms": 3040, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 3380, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "현재", "start_ms": 3770, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "저를", "start_ms": 4090, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "평가하는", "start_ms": 4380, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "기준은", "start_ms": 4740, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "학위가", "start_ms": 5060, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 5400, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "지금까지", "start_ms": 5720, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 6100, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "만들어온", "start_ms": 6360, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "결과물들이기", "start_ms": 6750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "때문입니다", "start_ms": 7140, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "옛날", "start_ms": 7530, "dur_ms": 22, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "생명을 구할 수 있어 다행히 하늘이 무너지라는 법은 없나 봅니다 폭설이 잦은 지역인 만큼 익숙한 듯 제설차가 나타나서 도로를 청소해 줍니다 제설",
"speaker": "speaker_8",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "생명을", "start_ms": 20, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "구할", "start_ms": 60, "dur_ms": 25, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 90, "dur_ms": 15, "phase_lr_deg": 0.0},
{"word": "있어", "start_ms": 110, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "다행히", "start_ms": 140, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "하늘이", "start_ms": 180, "dur_ms": 35, "phase_lr_deg": 0.0},
{"word": "무너지라는", "start_ms": 220, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "법은", "start_ms": 270, "dur_ms": 25, "phase_lr_deg": 0.0},
{"word": "없나", "start_ms": 300, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "봅니다", "start_ms": 330, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "폭설이", "start_ms": 370, "dur_ms": 35, "phase_lr_deg": 0.0},
{"word": "잦은", "start_ms": 410, "dur_ms": 25, "phase_lr_deg": 0.0},
{"word": "지역인", "start_ms": 440, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "만큼", "start_ms": 480, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "익숙한", "start_ms": 510, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "듯", "start_ms": 545, "dur_ms": 10, "phase_lr_deg": 0.0},
{"word": "제설차가", "start_ms": 560, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "나타나서", "start_ms": 610, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "도로를", "start_ms": 660, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "청소해", "start_ms": 700, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "줍니다", "start_ms": 750, "dur_ms": 25, "phase_lr_deg": 0.0},
{"word": "제설", "start_ms": 780, "dur_ms": 20, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 44,
"transcript": "현실은 50센티의 눈밭에 오케까지",
"speaker": "speaker_9",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "현실은", "start_ms": 20, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "50센티의", "start_ms": 140, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "눈밭에", "start_ms": 300, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "오케까지", "start_ms": 400, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "푹푹 빠지는 강제 눈 수영입니다",
"speaker": "speaker_9",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "푹푹", "start_ms": 15, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "빠지는", "start_ms": 110, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "강제", "start_ms": 220, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "눈", "start_ms": 310, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "수영입니다", "start_ms": 360, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "표현되는 게 많지가 않거든요 진짜 많이 준비를 해보세요",
"speaker": "speaker_10",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "표현되는", "start_ms": 30, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 180, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "많지가", "start_ms": 230, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "않거든요", "start_ms": 340, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 470, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 560, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "준비를", "start_ms": 640, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "해보세요", "start_ms": 750, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "기계 부품을 가공할 수 있는 뭐",
"speaker": "speaker_11",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "기계", "start_ms": 10, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "부품을", "start_ms": 100, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "가공할", "start_ms": 210, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 330, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 370, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 440, "dur_ms": 40, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "그런 것을 찾아야 되겠다 그래서 청계천을 무조건 갔고요 다들 안 해주시려고 하죠 예 그러다가 이제",
"speaker": "speaker_11",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "그런", "start_ms": 20, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "것을", "start_ms": 90, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "찾아야", "start_ms": 150, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "되겠다", "start_ms": 250, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 340, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "청계천을", "start_ms": 420, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "무조건", "start_ms": 550, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "갔고요", "start_ms": 640, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "다들", "start_ms": 720, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 790, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "해주시려고", "start_ms": 830, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "하죠", "start_ms": 950, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "예", "start_ms": 1000, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "그러다가", "start_ms": 1040, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 1140, "dur_ms": 40, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "공산품을 주로 보는 거고요 이게 나가게 되면 농산품 수산",
"speaker": "speaker_12",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "공산품을", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "주로", "start_ms": 170, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "보는", "start_ms": 240, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "거고요", "start_ms": 320, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 390, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "나가게", "start_ms": 450, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "되면", "start_ms": 530, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "농산품", "start_ms": 580, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "수산", "start_ms": 680, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "맞는 말입니다 근데 그걸로 매매하면 항상 늦습니다 시장은 항상 유보다 먼저 자리를 만들어",
"speaker": "speaker_13",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "맞는", "start_ms": 25, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "말입니다", "start_ms": 100, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 220, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "그걸로", "start_ms": 290, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "매매하면", "start_ms": 370, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "항상", "start_ms": 500, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "늦습니다", "start_ms": 590, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "시장은", "start_ms": 700, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "항상", "start_ms": 800, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "유보다", "start_ms": 880, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "먼저", "start_ms": 980, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "자리를

---
*Generated by WKAppBot ask gemini — 2026-04-04 03:47:17*
