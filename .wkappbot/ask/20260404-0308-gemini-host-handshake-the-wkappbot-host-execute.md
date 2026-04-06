# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=29532  time=03:07:44  uptime=5759s  mem=997MB  system_processes=364"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260404_030646.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 726-236-762-672-672-672-627-267-627-623 765-675-675_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 627-267-263-267-327-321-237-317-723-352-251-627-671-273-217-126-215-571-517-137-721-173-376-372-315-371-356-135-173-357-351-537-576-572-357-537-537-357-567-572-576-576-756-260-2_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 357-136-106-601-165-316-136-106-562-126-507_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 103-130-103-136-103-617-517-716-163-137-561-617-617-602-261-137-123-167-106-715-756-107-167-132-107-103-172-610-167-106-103-163-132-132-136-162-163-670-136-103-576-175-103-106-5_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 201-675-672-106-301-702-260-230-701-106-106-136-162-160-601-617-603-603-631-613-613-106-162-203-230-230-203-671-126-307-305-203-103-302-507-106-102-231-216-102-103-132-106-320-3_V.mp3)
  Segment 6: 7880ms ~ 11012ms (file: 526-312 312-126-236-126-123-126-162-126 621-602_V.mp3)
  Segment 7: 12012ms ~ 12588ms (file: 265-256-523-256-256-256-253-526-326-236-326-263-623-623-265-625-265-265-625-260-265-625-627-627-265-263-236-126-213-263-623-526-256-236-263-236-263-526-526-265-236-236-265-625-2_W.mp3)
  Segment 8: 13588ms ~ 14164ms (file: 621-103-103-103-173-157-362-123-167-132-103-136-576-123-617-132-103-130-651-126-132-123-320-176-123-107-103-317-127-176-103-167-106-136-106-673-156-126-301-167-106-136-157-657-5_V.mp3)
  Segment 9: 15164ms ~ 15740ms (file: 321-175-216-103-103-362-236-231-301-231-310-103-312-132-132-126-123-123-216-312-361-236-260-206-206-201-217-237-102-627-670-607-623-136-637-132-657-517-135-537-751-765-132-132-1_W.mp3)
  Segment 10: 16740ms ~ 17316ms (file: 236-362-263-236-263-326-263-237-236-326-236-326-263-532-235-326-236-326-236-230-230-206-502-567-256-261-136-123-534-574-501-253-632-362-621-261-621-613-263-126-263-632-306-302-3_W.mp3)
  Segment 11: 18316ms ~ 18892ms (file: 571-765-367-623-206-260-203-510-715-720-716-570-705-310-301-103-302-231-302-326-310-637-103-361-605-130-563-301-132-301-236-236-570-203-230-103-230-361-301-103-130-103-130-132-7_W.mp3)
  Segment 12: 19892ms ~ 26624ms (file: 267-267-267-267-264-726-623-362-572-750-130-230-260-267-265-236-213-132-103-237-732-103-260-267-236-260-263-267-627-620-120-217-210-261-132-103-106-162-103-725-106-217-132-123-2_W.mp3)
  Segment 13: 27624ms ~ 28200ms (file: 362-316-130-213-132-310-130-103 136-103-163-610-310-546-163-165-163-103-163-326-216-130-301-102-362-132-612-106-150-160-302-130-162-312-651-176-612-162-163-103-612-106-136-102-1_V.mp3)
  Segment 14: 29200ms ~ 29776ms (file: 726-137-132-762-765-765-570-574-571-153-514-546-560-756-765-716-176-136-137-136-361-230-231-132-163-103-103-172-670-617-163-130-615-167-163-132-105-672-136-162-176-610-163-136-1_V.mp3)
  Segment 15: 30776ms ~ 31352ms (file: 153 136 136-163-137-136-761-617-136-716-301-103-617-106-316-103-136-136-130-516-106-153-176-163-163-136-536-132-165-312-617-107-136-136-130-132-103-510-231-513-132-130-316-106-3_V.mp3)
  Segment 16: 32352ms ~ 32928ms (file: 617-371-713-671-675-362-263-261-126-231-326-367-762-261-152-152-217-231-321-321-623-716-673-637-672-672-673-671-670-674-670-674-672-263-261-632-672-672-670-671-674-637-637-671-6_W.mp3)
  Segment 17: 33928ms ~ 34504ms (file: 675-576-675 675-671-765-576-657-671-621-167-726-762-267-625-576-756-675-671-267-276-627-562-215-263-162-213-236-236-260-106-627-672-623-261-216-671-617-576-125-362-132-613-671-7_W.mp3)
  Segment 18: 35504ms ~ 36080ms (file: 657-675-567-657-572-567-561-561-516-561-651-163-156-165-165-651-625-615-651-517-651-652-562-652-562-657-576-653-651-652-615-610-165-651-651-653-657-652-567-576-516-652-657-675-6_W.mp3)
  Segment 19: 37080ms ~ 37656ms (file: 236-321-326-321-130-316-321-312-321-312-316-326-326-321-326-321-360-321-257-276-256-257-206-625-756-576-567-567-657-567-657-657-675-762-675-657-652-576-136-136-135-132-315-531-5_W.mp3)
  Segment 20: 38656ms ~ 39232ms (file: 217-106-163-126 317-123-312-132-231-162-136_V.mp3)
  Segment 21: 40232ms ~ 40808ms (file: 126-623-621-713-317-172-761-160-103-130-123-163-627-137-103-132-365-103-136_W.mp3)
  Segment 22: 41808ms ~ 43248ms (file: 136 312 132-712-527-572-567-316-316-613-362-163-167-612-675-675-675-632-637-627-261-621-621-362-617-261-631-607-263-610-165-162-216_W.mp3)
  Segment 23: 44248ms ~ 44824ms (file: 675-327-123-127-217-312 256-136-362-317-132-325-165-126-235-120-163-163-163-613-236-627-302-573-376-136-321-132-132-302-213-613-574-576-320-235-257-236-263-321-312-132-320-325-6_V.mp3)
  Segment 24: 45824ms ~ 58352ms (file: 213-271-276-213-120-132-312-203-210-231-271-230-231-201-230-572-763-312-132-312-273-203-672-765-752-257-217-302-231-275-751-132-210-752-270-310-230-312-132-321-102-120-201-130-2_W.mp3)
  Segment 25: 59352ms ~ 59928ms (file: 130-103-312-213-670-607-607-607-671-672-206-326-162-103-316-260-621-675-326-236-623-610-362-162-310-302-132-213-136-103-631-623-602-632-620-701-167-176-107-107-617-167-103-326-6_V.mp3)
  Segment 26: 60928ms ~ 61504ms (file: 623-260-620-625-620-625-625-265-625-265-265-625-620-625-263-627-625-267-265-265-267-265-267-261-265-267-267-260-261-267-620-620-625-620-627-621-625-627-261-625-625-625-261-267-6_W.mp3)
  Segment 27: 62504ms ~ 63080ms (file: 157-765-675-672-675-765-671-765-672-672-763-671-765-675-675-672-621-163-162-106-102-163-126-627 326-623-231-326-612-263-263 167-627-261-623-621 213-231-216-237-236-126-213-216-1_W.mp3)
  Segment 28: 64080ms ~ 64656ms (file: 103-617-613-163-617-637-621-610-123-103-263-610-630-167-106-316-160-265-102-130-103-167-167-132-154-175-561-163-163-103-312-173-137-173-103-106-152-136-167-617-632-163-617-627-1_V.mp3)
  Segment 29: 65656ms ~ 72532ms (file: 260-621-617-627-321-321-316-123-276-612-312-615-162-356-136-102-136-623-265-126-236-625-216-132-321-261-257-260-261-216-261-316-136-316-130-316-623-623-621-672-610-310-123-261-2_W.mp3)
  Segment 30: 73532ms ~ 74108ms (file: 623-627-672-627 261-362-621-627-263-623-312-623-213-623-162-126-136-236-301-132-136-162-213-321-123-613-120-231 321-132-631-216-612-162-621-123-612-123-312-120-130-216-263-631-1_W.mp3)
  Segment 31: 75108ms ~ 75684ms (file: 132-167-103-106-106-136-103-103-136-103-167-623-126-162-716-132-163-276-136-153-132-163-312-167-162-671-167-106-671-163-136-671-106-167-130-263-671-165-137-136-106-103-130-102-7_V.mp3)
  Segment 32: 76684ms ~ 77260ms (file: 613-617-760-176-176-176-175-175-167-675-132-517-102-123-132 132-236-362_V.mp3)
  Segment 33: 78260ms ~ 78836ms (file: 105-150-562-631-106-163-106-706-607-675-167-615-617-627-136-106-103-106-106-106-761-765-675-765-607-672-675-670-672-160-107-136-317-312-173-617-126-132-312-167-361-731-763-206-3_W.mp3)
  Segment 34: 79836ms ~ 89556ms (file: 627-103-201-207-270-574-514-143-754-312-302-267-207-276-267-137-573-107-132-713-765-720-132-103-571-756-175-137-163-756-203-201-327-103-721-510-105-130-103-756-726-270-276-276-5_W.mp3)
  Segment 35: 90556ms ~ 91132ms (file: 267-623-762-261-627-263-236-106-570-130-132-213-102-126-316-106-123 612-103-132-316-103-132-123-102-123-136-103-130-316-312-132-310-163-125-106-316-103-671-175-106-321-135-132-1_V.mp3)
  Segment 36: 92132ms ~ 92708ms (file: 127-765-672-601-603-163-756-675-670-602-102-126-320-302-230-230-625-265-132-236-612-632-302-320-316 132-261-102-103 126-602-621-612-162-130-103-103-123-172-124-213-123-103-301-3_V.mp3)
  Segment 37: 93708ms ~ 102708ms (file: 312-130-123-135-713-153-315-251-267-126-756-176-267-627-765-235-237-213-523-265-715-637-261-206-102-132-326-134-627-620-620-763-725-572-130-730-310-130-153-750-507-261-206-213-2_W.mp3)
  Segment 38: 103708ms ~ 104284ms (file: 630-106-132-213-341-413-132-143-312-132-143-136-173_V.mp3)
  Segment 39: 105284ms ~ 105860ms (file: 136-576-123-162-106-132-601-163-617-176-126-162-623-576-756-657-576-517-576-136-154 632-621 173-612 126-716-167-321-162_V.mp3)
  Segment 40: 106860ms ~ 114852ms (file: 267-267-267-216-261-217-267-273-317-216-271-263-271-137-317-312-316-312-213-320-130-231-312-132-312-321-231-312-132-210-102-310-103-102-321-167-231-231-321-312-312-321-316-624-6_W.mp3)
  Segment 41: 115852ms ~ 122188ms (file: 103-276-265-136-301-231-753-157-312-176-107-267-627-672-762-751-571-217-106-267-275-275-273-276-617-127-327-231-236-263-267-236-217-326-231-237-263-231-261-123-213-312-213-321-2_W.mp3)
  Segment 42: 123188ms ~ 136904ms (file: 163-321-612-120-231-267-126-231-167-103-102-261-321-130-276-106-167-106-276-216-213-216-273-752-126-765-765-162-107-725-106-167-106-765-320-765-751-326-321-726-267-123-321-213-6_W.mp3)
  Segment 43: 137904ms ~ 138480ms (file: 102-261-263-617-613-167-160-103-106-123-321-132-102-301-103-317-136-576-267-206-752-751-701-267-276-210-520-752-210-126-167-176-765-756-312 576-576-576-517-573-537 630-360-263-3_W.mp3)
  Segment 44: 139480ms ~ 140056ms (file: 652-362-621-326-752-563-572-657-765-765-576-376-756-376-376-576-675-673-531-632-751-675-317-165-137-562-675-756-627-652-625-752-572-752-675-726-705-576-625-173_W.mp3)
  Segment 45: 141056ms ~ 141632ms (file: 162-136-126-102-130-132-312-130-162-136-136-316-123-163-617-163-613-136-317-106-362-310-312-531-136-761-671-637-765-617-671-617-162-162-163-163-163-632-162-312-132-136-136-631-6_W.mp3)
  Segment 46: 142632ms ~ 143208ms (file: 126-126-162-124-236-621-126-312-216-321-312-236-132-601-106-302-163-526-302-320-130-310-320-310-231-123-123-123-326-326-230-562-310-310-127-130-107-132-715-317-301-210-321-372-3_W.mp3)
  Segment 47: 144208ms ~ 144784ms (file: 576-571-126-362-263-376-176-716-176-761-137-730-731-176-132-137-162-123-162-103-651-610-156-361-137-103-125-120-132-123-132-126-136-103-213-132-126-137-213-312-216-175-123-106-5_W.mp3)
  Segment 48: 145784ms ~ 146360ms (file: 103-136-106-302-617-160-130-312-123-136-167-103-526-103-631-136-132 132-103-132-136-136-132-103-103-103-136-137-163-103-175-617-106-106-103-170-163-167-156-602-165-175-106-613-1_V.mp3)
  Segment 49: 147360ms ~ 147936ms (file: 261-620-102-130-102-136-137-123-126-130-321-103-130-132-123-130-310-103-173-753-106-162-315-130-321-132-321-310-103-317-132-731-731-617-360-675-621-106-617-601-316-136-612-102-6_V.mp3)
  Segment 50: 148936ms ~ 149512ms (file: 756-753 675-675-675-672-547-576-675-675-672-765-672-362-103-173-765-760-376-674-675-670-546-576-302-320-320-320-123-132-301_W.mp3)
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
> **Time**: 2026-04-04 03:08:48
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "얘기 꺼내기가",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "얘기", "start_ms": 10, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "꺼내기가", "start_ms": 260, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "어려웠을 것 같아요 근데 선배님이 아니 아니 너 하고 싶은 거 한번 다 해",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "어려웠을", "start_ms": 20, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 350, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 440, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 750, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "선배님이", "start_ms": 980, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "아니", "start_ms": 1350, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "아니", "start_ms": 1540, "dur_ms": 170, "phase_lr_deg": 0.0},
{"word": "너", "start_ms": 1720, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "하고", "start_ms": 1820, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "싶은", "start_ms": 2010, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 2220, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "한번", "start_ms": 2320, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 2570, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "해", "start_ms": 2690, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "그대로 다급해졌습니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "그대로", "start_ms": 10, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "다급해졌습니다", "start_ms": 300, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "그들은 대량의 첨단 중장갑 무기를 서둘러 들여와 무장해야 했습니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "그들은", "start_ms": 50, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "대량의", "start_ms": 380, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "첨단", "start_ms": 720, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "중장갑", "start_ms": 1010, "dur_ms": 390, "phase_lr_deg": 0.0},
{"word": "무기를", "start_ms": 1420, "dur_ms": 330, "phase_lr_deg": 0.0},
{"word": "서둘러", "start_ms": 1780, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "들여와", "start_ms": 2150, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "무장해야", "start_ms": 2550, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "했습니다", "start_ms": 2980, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "다시는 누군가에게 전격적인 폴란드 침공을 당하지 않기 위해서였습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "다시는", "start_ms": 40, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "누군가에게", "start_ms": 410, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "전격적인", "start_ms": 920, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "폴란드", "start_ms": 1390, "dur_ms": 360, "phase_lr_deg": 0.0},
{"word": "침공을", "start_ms": 1760, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "당하지", "start_ms": 2160, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "않기", "start_ms": 2520, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "위해서였습니다", "start_ms": 2780, "dur_ms": 680, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "그래서 폴란드는 막대한 돈다발을 들고 유럽의 전통적인 군수 산업 거인들인 독일과 프랑스의 문을 두드렸습니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "그래서", "start_ms": 60, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "폴란드는", "start_ms": 400, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "막대한", "start_ms": 830, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "돈다발을", "start_ms": 1230, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "들고", "start_ms": 1670, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "유럽의", "start_ms": 1920, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "전통적인", "start_ms": 2250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "군수", "start_ms": 2720, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "산업", "start_ms": 2970, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "거인들인", "start_ms": 3240, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "독일과", "start_ms": 3680, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "프랑스의", "start_ms": 4040, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "문을", "start_ms": 4440, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "두드렸습니다", "start_ms": 4680, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "그리고 독일의 레오파르트 전차와",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "그리고", "start_ms": 20, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "독일의", "start_ms": 320, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "레오파르트", "start_ms": 680, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "전차와", "start_ms": 1180, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "결정되었습니다 극장에서 입소문이 타기 전에 상영이 조기 종료되며 장기 흥행의 가능성 자체가 잘려나간 셈이었죠",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "결정되었습니다", "start_ms": 40, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "극장에서", "start_ms": 850, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "입소문이", "start_ms": 1290, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "타기", "start_ms": 1690, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "전에", "start_ms": 1920, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "상영이", "start_ms": 2170, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "조기", "start_ms": 2510, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "종료되며", "start_ms": 2770, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "장기", "start_ms": 3240, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "흥행의", "start_ms": 3490, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "가능성", "start_ms": 3820, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "자체가", "start_ms": 4180, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "잘려나간", "start_ms": 4520, "dur_ms": 390, "phase_lr_deg": 0.0},
{"word": "셈이었죠", "start_ms": 4930, "dur_ms": 420, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "딱 흥행 실패",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "딱", "start_ms": 10, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "흥행", "start_ms": 150, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "실패", "start_ms": 380, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "있었기 때문에 무조건 소액으로 나의 매매 디테일만 생각을 하시면 이게 나가서 10만원 벌긴 너무 어렵잖아요",
"speaker": "speaker_4",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "있었기", "start_ms": 30, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "때문에", "start_ms": 370, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "무조건", "start_ms": 750, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "소액으로", "start_ms": 1110, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "나의", "start_ms": 1540, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "매매", "start_ms": 1770, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "디테일만", "start_ms": 2040, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "생각을", "start_ms": 2480, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "하시면", "start_ms": 2820, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 3210, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "나가서", "start_ms": 3410, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "10만원", "start_ms": 3750, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "벌긴", "start_ms": 4180, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 4480, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "어렵잖아요", "start_ms": 4710, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "며칠 전에도 앤서랑 형님이랑 같이 쿠팡 알바를 제가 촬영 감독을 했는데",
"speaker": "speaker_4",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "며칠", "start_ms": 40, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "전에도", "start_ms": 270, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "앤서랑", "start_ms": 610, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "형님이랑", "start_ms": 980, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 1380, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "쿠팡", "start_ms": 1620, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "알바를", "start_ms": 1890, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 2250, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "촬영", "start_ms": 2490, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "감독을", "start_ms": 2750, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "했는데", "start_ms": 3090, "dur_ms": 360, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "하고 급등을 하는 모습이죠 이 구조를 미리 체크한 사람은 수익이 나는 거고 못 본 사람은 구경만 하는 겁니다 이 차이가",
"speaker": "speaker_5",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "하고", "start_ms": 50, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "급등을", "start_ms": 280, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 640, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "모습이죠", "start_ms": 890, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 1450, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "구조를", "start_ms": 1560, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "미리", "start_ms": 1910, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "체크한", "start_ms": 2170, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "사람은", "start_ms": 2550, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "수익이", "start_ms": 2980, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "나는", "start_ms": 3320, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "거고", "start_ms": 3580, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "못", "start_ms": 3850, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "본", "start_ms": 3980, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "사람은", "start_ms": 4120, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "구경만", "start_ms": 4500, "dur_ms": 360, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 4880, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "겁니다", "start_ms": 5140, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 5620, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "차이가", "start_ms": 5750, "dur_ms": 320, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "가지수가 진짜 엄청 많은데 다 먹을 수 있을까 너무 좋아요",
"speaker": "speaker_6",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "가지수가", "start_ms": 30, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 440, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "엄청", "start_ms": 710, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "많은데", "start_ms": 980, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 1350, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "먹을", "start_ms": 1480, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 1720, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "있을까", "start_ms": 1840, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 2150, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "좋아요", "start_ms": 2420, "dur_ms": 320, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "안녕하세요 저는 무한리필 한식 뷔페를 운영하고 있는 박준형입니다",
"speaker": "speaker_7",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 20, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "저는", "start_ms": 550, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "무한리필", "start_ms": 820, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "한식", "start_ms": 1250, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "뷔페를", "start_ms": 1550, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "운영하고", "start_ms": 1890, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 2290, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "박준형입니다", "start_ms": 2520, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "하고 있습니다 아 예 이제 시장 번호가 끝났고요 이제 가게로 가면 됩니다",
"speaker": "speaker_7",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "하고", "start_ms": 10, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 750, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "예", "start_ms": 860, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 1020, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "시장", "start_ms": 1250, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "번호가", "start_ms": 1520, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "끝났고요", "start_ms": 1860, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 2280, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "가게로", "start_ms": 2520, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "가면", "start_ms": 2860, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "됩니다", "start_ms": 3090, "dur_ms": 320, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "과정의 변형 형태로 나온 겁니다 그렇게 에너지를 모으고 나서 오늘 강하게 상승을 한 것이죠 여기서 정말 중요한 팁을 알려드리겠습니다 강한 테마의 주도",
"speaker": "speaker_5",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "과정의", "start_ms": 40, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "변형", "start_ms": 380, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "형태로", "start_ms": 650, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "나온", "start_ms": 1020, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "겁니다", "start_ms": 1260, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "그렇게", "start_ms": 1720, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "에너지를", "start_ms": 2050, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "모으고", "start_ms": 2450, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "나서", "start_ms": 2790, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 3050, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "강하게", "start_ms": 3320, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "상승을", "start_ms": 3660, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 4030, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "것이죠", "start_ms": 4160, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "여기서", "start_ms": 4650, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 4980, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "중요한", "start_ms": 5240, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "팁을", "start_ms": 5580, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "알려드리겠습니다", "start_ms": 5810, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "강한", "start_ms": 6720, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "테마의", "start_ms": 6980, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "주도", "start_ms": 7320, "dur_ms": 280, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-04 03:08:48*
