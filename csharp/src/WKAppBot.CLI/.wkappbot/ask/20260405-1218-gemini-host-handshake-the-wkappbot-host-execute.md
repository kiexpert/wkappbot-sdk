# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=17708  time=12:17:55  uptime=29336s  mem=495MB  system_processes=356"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_121530.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 760-725-261-713-725-271-572-725-570-265-372-705-351-275-372-572-761-573-576-132-627-765-753-125 570-275-271-760-132 576-572-512-261-756-325-231-576-526-574-752-256-502-257-572-2_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 150-135-134-103-315-354-435-143-134-130-314-134-314-134-103-103-143-314-134-103-314-103-134-134-134-301-314-301 401-401-130-310-135-103-301-103-134-130-301-143-143-134-104-103-1_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 523-756-754-756-754-754-754-756-750-576-501-503-502-502-501-506-501-501-506-502-501-503-501-503-561-502-527-527-507-562-562-520-250-270-756-572-762-756-756-576-756-756-756-756-7_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 134-103-134-146-143-130-103-165 137-751-571-513-574-315-135-315-315-135-317-315-315-317-157-571-573-571-571-571-574-574-574 103-103-103-103-134-103-137-173-135-176-103-107-134-1_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 371-137-301-312-103-301-103-103-310-107-172-713-103-713 317-137-103-315-173-103-103-301-130-137-301-310-130-107-130-103-103-130-130-130-130-310-130-173-310-103-172-310 153-103-3_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 572-367-576-752-732-372-570-216-375-735-573-702-570-752-765-726-672-670-136-362-276-237-572-675-765-275-625-702-321-312-301-132-316-167-736-136-153-567-571-657-657-675-765-653-6_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 372-327-316-216-312-326-206-671-631-326-672-263-672-627-672-672-627-627-627-162-621-621-162-162-620-765-761-617-671-176-362-671-671-361-136-613-612-631-632-163-361-176-163-617-6_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 715-103-123-612-615-657-615-657-617-157-156-157-157-571-157-637-617-671-673-673-376-763-367-376-763-762-673-763-765-316-132-120-160-617-615-671-675-615-165-567-175-156-175-157-7_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 317-103-301-317-176-316-317-132-130-301-130-310-136-130-301-310-731-107-173-317-137-135 751-703-730-760-736-517-573-306-736-163-670-607-607-607-763-371-312-731-170-731-301-705-7_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 162-213-260-137-132-316-671-762-672-627-670-762-763-576-675-672-671-267-263-236-261-216-136-123-372-673 132-362 623-623-306-103-136-103-310-310-162-167-627-267-567-571-106-167-5_W.mp3)
  Segment 11: 15760ms ~ 24904ms (file: 632-257-237-102-123-102-216-230-230-203-615-256-321-123-103-237-132-120-213-312-103-216-123-135-231-623-765-376-236-236-267-123-120-267-103-312-136-216-315-103-132-210-120-132-1_V.mp3)
  Segment 12: 25904ms ~ 26480ms (file: 157-150-105-107-517-517-715-571-751-175-715-157-715-175-571 105-501-150-150-170-710-701-170-701-710-170-751-710-751-750-170-751-170-570-710-750-175-173 105-510-105-571-570-107-1_V.mp3)
  Segment 13: 27480ms ~ 28056ms (file: 216-726-271-236-507-312-760-567-306-135-316-165-160-301_V.mp3)
  Segment 14: 29056ms ~ 29632ms (file: 750-751-751-751-751-751-751-751-751 501-105-105-103-105-105-517-756-751-756-751-751-751-715-751-751-715-751-715-751 150-150-517-105-517-517-501-507-750-751-751-751-751-715-751-7_W.mp3)
  Segment 15: 30632ms ~ 31208ms (file: 157-105-157-571-150-501-507-157-150-510-150-510-571-510-150-150-507-510-150-510-510-105-516-150-501-175-157-571-510 150-157-153-507-507-501-501-510-501-150-510-105-501-150-517-5_V.mp3)
  Segment 16: 32208ms ~ 32784ms (file: 567-561-567-657-657-567-567-615-756-571-576-765-576-531-153-561-561-165-156-567-516-357-563-653-657-651-561-567-576-375-357-357-567-516-657-576-651-657-657-657-576-651-657-657-5_W.mp3)
  Segment 17: 33784ms ~ 42532ms (file: 263-132-321-102-123-102-360-301-132-120-320-132-103-137-216-301-154-153-751-203-312-503-103-513-574-162-126-136-175-126-123-123-126-130-132-127-132-127-123-123-257-216-721-302-3_W.mp3)
  Segment 18: 43532ms ~ 52496ms (file: 321-175-261-327-157-320-321-627-237-213-132-576-572-572-267-230-150-103-130-315-615-361-162-136-613-316-136-103-130-351-210-520-123-231-127-603-706-107-750-713-103-106-170-310-1_W.mp3)
  Segment 19: 53496ms ~ 54072ms (file: 765-507-256-261-275-256-275-765-765-275-256-527_W.mp3)
  Segment 20: 55072ms ~ 55648ms (file: 167-617 165-165-156-156-615-156-165-516-165-165-156-165-561-516-135-561-165-156-671-615-671-167-671-621 756-572-576-576-572-576_W.mp3)
  Segment 21: 56648ms ~ 57224ms (file: 601-167-176-603-621-621-623-620-276-623-627-136-106-136-106-167-106-106-163-103-106-106-160-651-165-625-167-106-136-136-103-135-613-613-103-173-612-615-576-651-167-173-612-360-3_V.mp3)
  Segment 22: 58224ms ~ 58800ms (file: 135-310-103-135-137-315-135-103-301-315-132-135-315-135-305-150 153-135-130-315-103-137-135-135-135-137-315-315-103-310-135-130-312-103-137-135-135-153-315-315-137-305 157-135-1_V.mp3)
  Segment 23: 59800ms ~ 60376ms (file: 574-513-725-256-750-675-671-507-317-253-231-235-572-652-267-275-236-760-670-372-627-267-607-762-623-275-576-213-257-572-752-607-765-572-527-520-203-327-521-132-372-752-321-527-2_V.mp3)
  Segment 24: 61376ms ~ 61952ms (file: 756-751-756-157-157-576-567 756-120-762-237-237-672-672-672_W.mp3)
  Segment 25: 62952ms ~ 74436ms (file: 237-206-623-216-627-172-132-206-167-762-267-627-627-267-627-120-237-537-527-276-103-102-276-320-752-230-213-765-267-230-312-163-137-263-673-217-123-312-361-652-756-132-162-167-6_W.mp3)
  Segment 26: 75436ms ~ 76012ms (file: 156-105-105-150 105-150-105-106-103-135-105-157-156-657-567-165-610-167-671 105-105-156-510-150-157-156-156-150-160-156-156-150-150-510 156-150-156-150-156-150-157-501-516-156-1_V.mp3)
  Segment 27: 77012ms ~ 77588ms (file: 321-532-512-573-576-576-567-562-257-657_W.mp3)
  Segment 28: 78588ms ~ 86256ms (file: 203-103-132-312-310-130-103-310-312-310-310-267-572-203-756-756-237-236-310-230-230-102-107-310-503-321-230-206-501-530-350-250-205-260-102-201-527-572-205-230-203-321-257-201-1_W.mp3)
  Segment 29: 87256ms ~ 87832ms (file: 321-623-576-762-726-762-763-763-726-712-671-627-761-716-312 716-673-765-762-761-701-716-765-762-217-612-275-632-276-376-152-216-570-317-215-312-203-217-756-625-217-576-567-735-3_V.mp3)
  Segment 30: 88832ms ~ 89408ms (file: 507-507-507-572-572-752-572-572-527-527-527-572-573-725-725-271-576-756-756-576-571-576-571-756-576-756-756-751-756-715_W.mp3)
  Segment 31: 90408ms ~ 90984ms (file: 576-576-567-576-576-576-571-517-571-571-517-157-517-571-576-576-657-652-165-567-657-516-561-156-653-651-657-650-657-756-571-571-571-751-756-657-176-657-261-310-103-321 672-671-6_W.mp3)
  Segment 32: 91984ms ~ 101056ms (file: 210-241-236-216-267-210-206-260-256-574-302-231-201-302-201-213-120-123-102-124-132-127-127-312-213-216-310-301-310-321-320-752-130-213-713-275-103-132-160-362-136-301-725-276-2_V.mp3)
  Segment 33: 102056ms ~ 107024ms (file: 310-215-176-312-132-123-123-123-132-123-213-312-123-127-256-321-236-263-632-120-236-361-367-670-613-361-367-673-210-260-310-103-206-206-572-570-750-753-107-627-756-756-576-571-7_W.mp3)
  Segment 34: 108024ms ~ 108600ms (file: 157-173-715-751-175 527-527-527 157-150-157-152-153-157-175-157-157-157-512 157-157-153-153-157-150-170-107-713-173-170-712-170-751-750-751-751-751_V.mp3)
  Segment 35: 109600ms ~ 110176ms (file: 175-735-765-716-157-756-756-756-576-756-751-756 517-105-150-153-153-751-756-576-756 574-517-516-157-175-157-517-157-105-105-517-157-153-105-135-751-715-751-153-157-156-516-576-5_W.mp3)
  Segment 36: 111176ms ~ 111752ms (file: 251-250-257 256-253-205-205-205-257-257-520-527-257-250-205-527-257-250-520-250-502-520-205-250-502-250-507 235-235-236-213-257 205-201-205-206-206 527-270-275-256-257-257-527-2_V.mp3)
  Segment 37: 112752ms ~ 113328ms (file: 576-571-576-576-576-756-576-571 507-175-157-713-170-157-175-715-170-170-175-157-751-751-715-756-576-576-576-756-576-576-576-571 701-157-570-175-715-175-170-715-573-571 574-574-5_W.mp3)
  Segment 38: 114328ms ~ 126676ms (file: 201-267-623-376-263-326-753-127-213-216-574-120-203-263-260-263-130-712-765-257-267-312-213-301-450-103-236-652-576-672-260-261-576-275-201-760-203-260-103-201-620-621-260-215-6_W.mp3)
  Segment 39: 127676ms ~ 131960ms (file: 132-103-317 160-126-130-123-521-521-236-602-216-162-310-162-126-216-130-312-361-352-156-315-156-165-165-756-536-635-621 651-156-156-156-651-615-165-615-561-651-651-156-651-561-6_W.mp3)
  Segment 40: 132960ms ~ 139296ms (file: 763-631-167-106-672-627-627-237-627-267-261-261-627-621-627-672-267-672-321-261-260-260-103-736-267-620-206-126-276-251-312-312-362-312-670-103-102-120-126-761-126-276-267-106-3_W.mp3)
  Segment 41: 140296ms ~ 140872ms (file: 657-657-657-672-623-637-675-326 163-362 756-657-627-136_W.mp3)
  Segment 42: 141872ms ~ 149108ms (file: 102-132-261-310-231-261-130-601-167-706-657-650-615-567-574-130-756-236-123-263-236-230-321-321-321-632-310-132-213-321-123-672-236-632-765-267-625-257-256-267-162-326-621-260-2_W.mp3)
  Segment 43: 150108ms ~ 150684ms (file: 765-760-765-765-710-765-761-763-716-716-672-752-750-756_W.mp3)
  Segment 44: 151684ms ~ 152260ms (file: 756-567-765-576-756-576-576-675-675-657-675-675 607-576-576-576-576-576-576-576-576-576-576-576-567-576-567-756-576-576-576-756 653-567-576-576-576-576-576-576-576-756-576-576-5_W.mp3)
  Segment 45: 153260ms ~ 153836ms (file: 503-531-516-516-531-512-567-531-512-502-516 527-532-526-527-502-507-520-250-750-527-257-725-257-572-572-572-256-526-257-502-527-521-256-562-652-567-657-561-567-567-567-567-567-5_W.mp3)
  Segment 46: 154836ms ~ 155412ms (file: 631-103-136-615-102-163-103-316-673-607-501-136-571-126-136-173-251-126-103-106-163-153-137-612-162-627-162-617-672-276-267-236-213-267-612-613-621-132-136-165-613-163-623-603-3_V.mp3)
  Segment 47: 156412ms ~ 163396ms (file: 127-536-163-726-503-570-673-176-102-107-160-625-601-231-623-130-126-120-127-137-362-123-103-103-136-612-216-102-627-627-673-672-623-260-620-620-136-126-123-210-231-216-126-217-2_W.mp3)
  Segment 48: 164396ms ~ 164972ms (file: 371-672-756-572-725-275-237-762-321-320-351-541-213-310-310-543-547-541-547-307-532-572-567-125-175-135-137-513-672-162-132-123-623-265-321-320-136-217-106-574-574-546-312-263-5_W.mp3)
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
> **Time**: 2026-04-05 12:18:58
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "거칠게 말해서 껌값이 되면서 무너지고 있는 박사 학위에 대한 이야기입니다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "거칠게", "start_ms": 20, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "말해서", "start_ms": 135, "dur_ms": 105, "phase_lr_deg": 0.0},
{"word": "껌값이", "start_ms": 250, "dur_ms": 95, "phase_lr_deg": 0.0},
{"word": "되면서", "start_ms": 350, "dur_ms": 85, "phase_lr_deg": 0.0},
{"word": "무너지고", "start_ms": 440, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 525, "dur_ms": 51, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "본론으로 들어가기 전에 한 가지 강조드리고 싶습니다.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "본론으로", "start_ms": 10, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "들어가기", "start_ms": 170, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "전에", "start_ms": 320, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 415, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "가지", "start_ms": 460, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "강조드리고", "start_ms": 525, "dur_ms": 51, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 4,
"transcript": "수제비까지 넣어 속이 든든한 우럭 매운탕입니다.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "수제비까지", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "넣어", "start_ms": 190, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "속이", "start_ms": 280, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "든든한", "start_ms": 380, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "우럭", "start_ms": 490, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "매운탕입니다", "start_ms": 555, "dur_ms": 21, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 6,
"transcript": "인데요 말 그대로 공중에 가둬 넣는 함정인 거죠 여기에 고진공 환경도 필요합니다 공기 분자 하나가 날아와서 닿아도 소멸하거든요 당연히",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "인데요", "start_ms": 15, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "말", "start_ms": 80, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "그대로", "start_ms": 115, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "공중에", "start_ms": 180, "dur_ms": 55, "phase_lr_deg": 0.0},
{"word": "가둬", "start_ms": 240, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "넣는", "start_ms": 285, "dur_ms": 35, "phase_lr_deg": 0.0},
{"word": "함정인", "start_ms": 325, "dur_ms": 65, "phase_lr_deg": 0.0},
{"word": "거죠", "start_ms": 395, "dur_ms": 35, "phase_lr_deg": 0.0},
{"word": "여기에", "start_ms": 435, "dur_ms": 45, "phase_lr_deg": 0.0},
{"word": "고진공", "start_ms": 485, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "환경도", "start_ms": 550, "dur_ms": 26, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 8,
"transcript": "함정이 주목을 받았는데 이분이 원래 테슬라 안드로이드 프로젝트의 창립자라고도 합니다 설치를 한 오 분 정도만 그냥 투자를 하면 된다라고 하거든요",
"speaker": "speaker_4",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "함정이", "start_ms": 25, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "주목을", "start_ms": 90, "dur_ms": 55, "phase_lr_deg": 0.0},
{"word": "받았는데", "start_ms": 150, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "이분이", "start_ms": 225, "dur_ms": 55, "phase_lr_deg": 0.0},
{"word": "원래", "start_ms": 285, "dur_ms": 45, "phase_lr_deg": 0.0},
{"word": "테슬라", "start_ms": 335, "dur_ms": 65, "phase_lr_deg": 0.0},
{"word": "안드로이드", "start_ms": 405, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "프로젝트의", "start_ms": 520, "dur_ms": 56, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "더 웃긴 거는 스마트 서먼 있잖아요",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "더", "start_ms": 10, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "웃긴", "start_ms": 55, "dur_ms": 55, "phase_lr_deg": 0.0},
{"word": "거는", "start_ms": 115, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "스마트", "start_ms": 180, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "서먼", "start_ms": 265, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "있잖아요", "start_ms": 340, "dur_ms": 236, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 11,
"transcript": "올해 살아남은 사람들이 결국 이거는 승리자잖아요 어떻게 보면 시장에서 퇴출 안 되는 것만 하더라도요 그래서 아까 말씀하신 부분이 우리가 미리 어떻게 시나리오를 세워서 이렇게 이렇게 대비하자 이게 안 돼요 그래서",
"speaker": "speaker_5",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "올해", "start_ms": 20, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "살아남은", "start_ms": 150, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "사람들이", "start_ms": 300, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "결국", "start_ms": 490, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이거는", "start_ms": 650, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "승리자잖아요", "start_ms": 790, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "이런 게 인테리어 철학이랍니다",
"speaker": "speaker_6",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "이런", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 150, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "인테리어", "start_ms": 210, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "철학이랍니다", "start_ms": 370, "dur_ms": 206, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 14,
"transcript": "아까 봤지만은 주방을 거실과 분리해 따로 둔 거는 좀",
"speaker": "speaker_6",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "아까", "start_ms": 15, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "봤지만은", "start_ms": 100, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "주방을", "start_ms": 210, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "거실과", "start_ms": 330, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "분리해", "start_ms": 430, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "따로", "start_ms": 510, "dur_ms": 66, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 16,
"transcript": "들이 완전히 들썩이기 시작했어요 배경은 일본 신주쿠 가부키초 신주쿠 중앙 경찰서의 신입",
"speaker": "speaker_7",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "들이", "start_ms": 20, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "완전히", "start_ms": 100, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "들썩이기", "start_ms": 200, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "시작했어요", "start_ms": 330, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "배경은", "start_ms": 490, "dur_ms": 86, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "welcome Kimchi starts from Korea again so very very happy",
"speaker": "speaker_8",
"confidence": 0.92,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "welcome", "start_ms": 50, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "Kimchi", "start_ms": 500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "starts", "start_ms": 1050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "from", "start_ms": 1500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "Korea", "start_ms": 1850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "again", "start_ms": 2300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "so", "start_ms": 2800, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "very", "start_ms": 3100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "very", "start_ms": 3500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "happy", "start_ms": 3900, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "한류 열풍이 불면서 한식의 관심도 높아진 요즘 이곳 네와리를 찾는 외국인들에게도 김장은 한국 여행 버킷 리스트 중",
"speaker": "speaker_9",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "한류", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "열풍이", "start_ms": 200, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "불면서", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "한식의", "start_ms": 580, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 20,
"transcript": "포커스 양보가 필요합니다",
"speaker": "speaker_10",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "포커스", "start_ms": 10, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "양보가", "start_ms": 140, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "필요합니다", "start_ms": 260, "dur_ms": 316, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "토론의 식비 아이 드리겠습니다 기름이랑 이렇게 고추장으로 만들어요 뭐 솔직히 고추장만 있으면 여러분 다른 양념 필요",
"speaker": "speaker_11",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "토론의", "start_ms": 25, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "식비", "start_ms": 130, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "아이", "start_ms": 220, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "드리겠습니다", "start_ms": 280, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "연간으로 보면 기가 가지는 안 될 것으로 봅니다 스타십은 아직 대량 생산 초기 단계입니다 이천이십육년은 이후에 수십 배 수준으로 이제 시작",
"speaker": "speaker_12",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "연간으로", "start_ms": 30, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "보면", "start_ms": 150, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "기가", "start_ms": 230, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "가지는", "start_ms": 300, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "버섯도 들어갔고 좋 자 좋다 이때 딱 세게만 딱 기다리면 돼요 비주얼이 장난 아니죠",
"speaker": "speaker_13",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "버섯도", "start_ms": 15, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "들어갔고", "start_ms": 120, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "좋", "start_ms": 250, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "자", "start_ms": 300, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "좋다", "start_ms": 350, "dur_ms": 80, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 12:18:58*
