# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=29532  time=03:16:38  uptime=6292s  mem=1561MB  system_processes=365"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260404_031629.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 106-671-632-620-613-627-267-103-761-567-162-172-107-671-167-162-160-361-176-126-106-167-136-165-671-163-675-631-160-103-162-163-175-172-167 134-106-130-103-167-135-612-106-107-1_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 613-326-217-126-163-167-162-106-312-631-713-613-627-136-571-126-136-126-136-173-136-167-167-302-106-132-607-167-613-671-612-163-621-162-136-103-136-103-106-106-514-312-312-167-1_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 302-372-372-372-735-756-376-753-167-670-672-376-317-317-317-301-371-321-213-312-310-312 561-165-120-132-672-273-352-352-130-123-126-132-123-276-231-312-132-317-261-123-312-623-3_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 607-610-216-620-576-317-103-301-312-326-312-253-251-231-267-260-260-206-203-231-321-320-201-130-230-203-203-231-321-302-136-103-267-236-263-362-321-362-623-675-576-576-765-765 6_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 123-312-312-213-106-106-102-310-301-320-205-326-216-120-301-301-132-103-130-317-130-310-301-231-263-216-103-310-320-312-361-316-360-103-306-130-310-306-302-326-306-302-310-312-1_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 623-623-612-712-367-371-736-723-672-671-106-126-130-357-301-321-132-201-136-163-165-163-103-103-623-574-576-130-130-103-135-103-126-156-613-103-103 106-621-261-176-157-671-162-1_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 123-132-103-153-165-561-562-612-162-623-625-163-136-213_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 123-162-130-132-316-576-567-502-570-605-570-506-576-136-316-102-123-306-123-263-253-235-526-352-341-132-231-213-102 136-132_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 123-132-134-136-134-132-132-136_V.mp3)
  Segment 10: 14184ms ~ 17172ms (file: 106 136-261-673-675-673-673-671-673-763-673-761-762-761-362-627-623-261-576-765-672-627 157-567-576-576-652-567-263-672-637-632-162 321 547-547-567 134-123-165-601-601-607-106-6_W.mp3)
  Segment 11: 18172ms ~ 18748ms (file: 106-153-137-136-173-765-621-532-132-120-312-231-132-163-130-361-136-571-504-513-103-123-103-130-162-263 564-567-127-125-175 167 132-760-672-540-547-547-163-376-376-567-562-231-3_V.mp3)
  Segment 12: 19748ms ~ 20324ms (file: 312-126-216-312-130-123-321-231-123-326-312-162-263-216-136-106-261-731-601-613-672-260-206-260-260-260-260-362-231-612-160-216-126-160-612-362-203-312-671-657-675-675-605-265-6_W.mp3)
  Segment 13: 21324ms ~ 21900ms (file: 751-617-156-102-103-356-175-651-106-136-130-163-103-106-165-132-631-160-163-102-132-103-102-176-130-615_V.mp3)
  Segment 14: 22900ms ~ 23476ms (file: 312-316-216-527-572-756-762-762-762-762-762-672-627-672-756-576-576-765-765-507_W.mp3)
  Segment 15: 24476ms ~ 25052ms (file: 671-601-163-132-176-126-123-213-132-623-136-312-612-130-126-123-263-612-562-756-765-163-123-126-165-206-136-267-765-613-170-163-126-673-136-163-132-136-107-106-216-173-601-136-6_W.mp3)
  Segment 16: 26052ms ~ 26628ms (file: 657-236-612-167-175-510-103-176-312-176-756-761-126-516-152-260-176-162-621-756-263-756-576-576-576-576-567-132-135_W.mp3)
  Segment 17: 27628ms ~ 28204ms (file: 716-136-136-154-163-106-632-103-167-132-123-103-617-620-267-267-163-153-567-135-617-103-135 103-136-103-126-615-236-213-130-176-123-231-321-235-526-576-573-576-572-326-321-132-3_V.mp3)
  Segment 18: 29204ms ~ 29780ms (file: 756-765-761-216-675-760-670-670-706-760-760-670-675-176-724-217-216-635-764-123-167-713-143-312-671-167-136-137-126-756-756-756-756-756-736-627-672-627-123-136-163-325-371-365 6_V.mp3)
  Segment 19: 30780ms ~ 36000ms (file: 213-160-126-542-216-263-261-612-627-162-216-326-261-516-136-236-523-123-217-673-267-572-310-213-276-267-257-260-137-326-621-521-267-267-265-207-126-150-531-263-526-206-261-203-7_W.mp3)
  Segment 20: 37000ms ~ 46576ms (file: 267-267-267-276-267-310-137-732-237-276-102-321-267-132-237-317-321-762-765-270-123-102-126-130-756-726-276-762-672-752-672-137-132-102-726-627-106-132-102-726-167-103-213-120-1_W.mp3)
  Segment 21: 47576ms ~ 48152ms (file: 751-275-672-517-103-163-123-123-627-362-312-207-312-157-570-163-152-137-307-321-321-231-230-235-256-302-327-326-632-635-631-617-675-163-312-132-627-136-231-123-307-312-137-716-1_W.mp3)
  Segment 22: 49152ms ~ 49728ms (file: 157-153-153-153-157-516-156-571-567-537-567-573-576-567-567-516-571-516-657-567-567-571-567-567-567-567-567-516-567-567-567-576-563-567-561-567-576-576-563-576-516-561-576-576-5_W.mp3)
  Segment 23: 50728ms ~ 51304ms (file: 132 312 326-326-301-301-167-163-735-736-376-376-327-103-237-267-231 367-736-136-531-136-321-312-326-312-631-361-136_W.mp3)
  Segment 24: 52304ms ~ 52880ms (file: 712-231-716-765-765-756-765-756-267-126-631-756-715-756-752-756-752-756-762-765-765-752-756-765-576-576_W.mp3)
  Segment 25: 53880ms ~ 54456ms (file: 621-236-132-341 123-132 762-763 326 126_V.mp3)
  Segment 26: 55456ms ~ 56032ms (file: 123-312-217-271-276-217-276-672-267-256-562-652-562-675-625-126-163-126-123-162-162-132-216-213-236-321-325-213-231-321-231-231-237-231-621-267-261-216-267-652-675-672-627-627-6_W.mp3)
  Segment 27: 57032ms ~ 57608ms (file: 725-726-102-576-567-571-574-576-543-573-576-573-150-136-756-750-753-753-137-157-123_W.mp3)
  Segment 28: 58608ms ~ 59184ms (file: 132-103-136-105-123-103-132-106-123-103-162-162 574-576 132-316-301-301-301 136-163-602-761-136-620-261-162-163-160-106-261-612-230-162-152-612-261-132-103-316-671-163-136-132-3_V.mp3)
  Segment 29: 60184ms ~ 60760ms (file: 267-257-674-126-124-127-135-125-132-126-132-175-637-371-713-213-167-175_V.mp3)
  Segment 30: 61760ms ~ 62336ms (file: 107-316-132-103-617-173-670-162-103-136-123-103-143-103-610-106-136-105-162-163-163-103-167-163-163-216-130-103-156-261-612-123-163-103-521-127-125-130-610-106-162-136-106-132-3_V.mp3)
  Segment 31: 63336ms ~ 63912ms (file: 160-623-260-206-106-103-136-306-302-106-102-302-203-230-236-213-203-236-230-632-673-630-206-203-203-620-326-362-326-320-326-320-360-320-302-320-302-320-136-137-162-301-623-260-2_V.mp3)
  Segment 32: 64912ms ~ 65488ms (file: 623-362-657-362-627-675-625-652-627-267-627-627-256-237-132-102-126-762 617_W.mp3)
  Segment 33: 66488ms ~ 67064ms (file: 172-126-312-130-132-103-310-106-106-136-267-715-130-305-136-317-130-132-103-310-123-102-561-571-541-513-162-132-103-162-123-173-160-613-173-103-106-103-163-612-103-312-160-103-1_V.mp3)
  Segment 34: 68064ms ~ 68640ms (file: 163-157-130-167-325-326-132-316-602-216-312-351-765-765-316-361-361-175-361-312-137-162 756 756-756-756-261-263-326-236 132-163-123-103-136-765-106-601-670-160-261-612-103-162-6_W.mp3)
  Segment 35: 69640ms ~ 70216ms (file: 103-132-130-106-136-623-263-126-326-123-612-670-126-621-263-263-621-103-130-312-316-201-203-103-326-316-130-103-137-301-276-176-621-705-507-310-301-136-106-276-163-216-263-203-6_W.mp3)
  Segment 36: 71216ms ~ 76148ms (file: 172-176-127-327-132-371-671-721-751-761-721-716-726-263-167-127-617-612-267-237-367-617-763-632-367-172-712-123-237-261-715-751-752-716-751-756-765-162-173-627-627-267-372-317-7_W.mp3)
  Segment 37: 77148ms ~ 85824ms (file: 106-761-317-312-123-103-126-123-172-103-103-130-103-135-103-261-267-213-102-261-752-130-621-206-261-206-201-261-257-571-752-216-305-265-625-125-216-213-176-675-372-216-251-517-1_W.mp3)
  Segment 38: 86824ms ~ 87400ms (file: 352-327-263-631-576-576-527-625-756-206-312-312-531-263-527-352-756-371-267-670-352-517-321-726-635-576-673-312-123-321-321-165-315-312-123-726-267-256-672-261-706-136-726-562-5_V.mp3)
  Segment 39: 88400ms ~ 88976ms (file: 163-103-103-136-167-103-103-356-576-321-621-162-165-671-130-163-136-103-123-170-136-103-167-137 136-162-362-157 263-312-136-135-651-167-163-516-167-136-361-136-123-516-106-136-1_V.mp3)
  Segment 40: 89976ms ~ 99552ms (file: 756-137-273-126-261-267-172-120-271-231-751-715-714-147-271-127-672-216-362-723-271-130-163-173-713-123-732-672-102-123-137-137-761-735-752-107-132-130-236-213-132-735-276-271-7_W.mp3)
  Segment 41: 100552ms ~ 101128ms (file: 103-103-134-132-123-143-165-136-132-103-175-317-137-120-203-753-576-130-137-137-312-321-137-760-176-173-102-123-176-617-167-106-103-612-163-126 623-162-136-163-167-157-312-763-5_V.mp3)
  Segment 42: 102128ms ~ 102704ms (file: 236-351-312-132-203-237-725-275-725-275-725-207-357-573-576-567-576-673-236-326-307-763-675-127-312-317-107-237-273-270-752-203-712-756-175-571-756-130-327-273-756-105-175-156-1_V.mp3)
  Segment 43: 103704ms ~ 104280ms (file: 163-610-601-162-103-136-167-103-160-106-106-136-167-162-106-152-126-157-163_V.mp3)
  Segment 44: 105280ms ~ 105856ms (file: 106-167-610-316-103-260-621-621-726-132-316-362-231-167-261-126-123-623-217-627-617-136-123-216-627-126-726 567-751-567-576-576-236-263-627-612-361-213-326-136-136-612-627-136_W.mp3)
  Segment 45: 106856ms ~ 114380ms (file: 506-136-534-275-361-703-257-756-765-725-605-715-675-302-276-263-216-175-751-751-756-257-327-120-517-572-257-271-217-273-132-170-130-103-267-261-236-726-276-263-236-326-321-120-1_W.mp3)
  Segment 46: 115380ms ~ 115956ms (file: 603-631-675-675-156-623-615-365-316-631-621-316-162-621-316-671-316-617-613-167-615-163-163-130-132-130-651-657-160-126-321-361-157-361-136-103-136-167-631-132-263-162-261-716-3_W.mp3)
  Segment 47: 116956ms ~ 117532ms (file: 126-632-162-617-136-726-256-263-123-316-132-316-361-632 326-263-267_W.mp3)
  Segment 48: 118532ms ~ 124508ms (file: 327-327-263-301-126-123-213-231-123-312-123-213-231-261-213-216-231-231-321-213-231-321-321-231-236-321-231-321-312-326-321-231-321-213-231-236-231-321-321-310-216-231-231-321-2_W.mp3)
  Segment 49: 125508ms ~ 126084ms (file: 610-361-163-256-126 132-176-175-132-162-136-163-612-162-765-605-316-165-760-132-167-163-316-102-301-123-265-162-102-106-651-163-173-516-126-102-123-132-163-362-672-576-126-162-6_V.mp3)
  Segment 50: 127084ms ~ 127660ms (file: 236-236-620-620-623-260-260-216-216-603-623-620-263-263-260-632-206-620-320-326-230-263-230-263-302-206-320-302-320-236-263-132-260-632-321-231-216-213-123-216-263-602-213-104-1_V.mp3)
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
> **Time**: 2026-04-04 03:17:27
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "로봇 연구 센터를 함께 운영하던 선후배 사이에서",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "로봇", "start_ms": 20, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "연구", "start_ms": 310, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "센터를", "start_ms": 570, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "함께", "start_ms": 930, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "운영하던", "start_ms": 1160, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "선후배", "start_ms": 1610, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "사이에서", "start_ms": 1980, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "지금은 연구 동반자가 돼 한계를",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "지금은", "start_ms": 30, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "연구", "start_ms": 370, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "동반자가", "start_ms": 610, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "돼", "start_ms": 1090, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "한계를", "start_ms": 1220, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "사실 쉽지는 않은데",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "사실", "start_ms": 40, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "쉽지는", "start_ms": 300, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "않은데", "start_ms": 630, "dur_ms": 320, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "몸도 많이 힘들고 쉽지 않지만",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "몸도", "start_ms": 20, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 250, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "힘들고", "start_ms": 510, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "쉽지", "start_ms": 850, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "않지만", "start_ms": 1150, "dur_ms": 380, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "손님들이 오셔서 맛있게 드시고 가시는 모습 보고 또 맛있게 먹었다고 인사하시고 가는 거 보면 그때 보람을 많이 느끼거든요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "손님들이", "start_ms": 50, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "오셔서", "start_ms": 450, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "맛있게", "start_ms": 790, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "드시고", "start_ms": 1150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "가시는", "start_ms": 1520, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "모습", "start_ms": 1850, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "보고", "start_ms": 2090, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 2320, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "맛있게", "start_ms": 2460, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "먹었다고", "start_ms": 2800, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "인사하시고", "start_ms": 3240, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "가는", "start_ms": 3820, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 4080, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "보면", "start_ms": 4210, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "그때", "start_ms": 4440, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "보람을", "start_ms": 4690, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 5050, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "느끼거든요", "start_ms": 5320, "dur_ms": 580, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "그래서 제가 힘 닿는 데까지",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "그래서", "start_ms": 30, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 360, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "힘", "start_ms": 560, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "닿는", "start_ms": 670, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "데까지", "start_ms": 900, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "요즘에는 손님들이 정상적으로 드시면 다 8인분까지 드시는데",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "요즘에는", "start_ms": 60, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "손님들이", "start_ms": 500, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "정상적으로", "start_ms": 900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "드시면", "start_ms": 1370, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 1710, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "8인분까지", "start_ms": 1840, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "드시는데", "start_ms": 2340, "dur_ms": 410, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "오늘의 목표는 성인 남자가 열심히 뛰는 정도의 속도인 초속 5미터",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "오늘의", "start_ms": 40, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "목표는", "start_ms": 370, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "성인", "start_ms": 750, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "남자가", "start_ms": 1010, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "열심히", "start_ms": 1350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "뛰는", "start_ms": 1720, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "정도의", "start_ms": 1950, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "속도인", "start_ms": 2280, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "초속", "start_ms": 2650, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "5미터", "start_ms": 2910, "dur_ms": 380, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "장대양봉을 만들면서 급등을 했었고 가격 조정이 나오다가 다시 올라오고 박스권을 만들고 돌파하면서 급등했습니다 2월 11일",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "장대양봉을", "start_ms": 20, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "만들면서", "start_ms": 520, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "급등을", "start_ms": 960, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "했었고", "start_ms": 1320, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "가격", "start_ms": 1650, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "조정이", "start_ms": 1910, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "나오다가", "start_ms": 2250, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 2650, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "올라오고", "start_ms": 2920, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "박스권을", "start_ms": 3350, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "만들고", "start_ms": 3790, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "돌파하면서", "start_ms": 4130, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "급등했습니다", "start_ms": 4600, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "2월", "start_ms": 5250, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "11일", "start_ms": 5480, "dur_ms": 340, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "이게 모두 내공이 될 수 있잖아 맞아요 어 마치 내가 이렇게 주문을 한 것처럼",
"speaker": "speaker_5",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "이게", "start_ms": 80, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "모두", "start_ms": 280, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "내공이", "start_ms": 540, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "될", "start_ms": 870, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 1000, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "있잖아", "start_ms": 1110, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "맞아요", "start_ms": 1500, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "어", "start_ms": 1850, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "마치", "start_ms": 2010, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "내가", "start_ms": 2270, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 2510, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "주문을", "start_ms": 2850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 3220, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "것처럼", "start_ms": 3350, "dur_ms": 420, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "올라가",
"speaker": "speaker_6",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "올라가", "start_ms": 10, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "(music)",
"speaker": "none",
"confidence": 0,
"language": "none",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-04 03:17:27*
