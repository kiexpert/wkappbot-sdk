# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=34340  time=04:13:16  uptime=11523s  mem=448MB  system_processes=363"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260404_041307.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 750 132-137-136-367-675-602-136-612-103-167-130-651-163-132-167-163-362-106-136-106-136-136-317-103 675-671-136-261-163-306-617-675-163-613-671-617-613-106-576-125-517_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 156-103-123-102-163-163-715-316-162-163-132-671-163-106-617-610-163-106-167-206-156-103-716-136-621-167-310-672-631-163-362-236-261-261 675-130-163-657_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 175-640-670-107-170-103-157-157-603-620-615-236-263-263-627-267-672-627-132-451-514-534-537-631-367-105-547-317-230-203-213-261-236-176-763-574-157-126-602-620-621-547-574-571-5_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 574-261-621-103-103-106-132-103-317-163-157-132-136-167-613-562-162-132-165-167-103-103-132-103-312-132-567-132-376-621-163-126-103-315-103-103-106-261-615-602-301-176-163-103-1_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 361-635-573-507-352-235-326-237-723-372-725_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 610-610-601-613-612-675-670-367-361-130-312-302-260-261-236-120-103-103-167-173-106-230-230-130-326-263-123-126-132-312-637-675-563-601-632-631-631-621-651-136-315-125-623-367-3_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 103-160-610-132-103-564-132-163-612-312-675-167-750-756-123-561-627-136-103-106-103-567-163-130-136-136-316-103-102-670-613-623-137-671-102-106-163-136-137-562-132-501-635-167-1_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 672-106-120-206-206-326-103-316-123-576-301-301-136-736-751-157-176-167-756-720-726-103-132-163-136-621-260-206-260-567-705-750-576-754-307-326-320-102-612-162-163-613-136-123-5_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 615-621-621-627-657-672-627-627 136-132-612-621 657 126-612-132-162-621-261-126-316-625-621-627-236-213-621-102-312-312_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 652-653-675-653-657-625-361-631-236-256-215-162-106-751-267-625-623-265-216-627-263-652-657-652-567-675-126-123-251-123-312-316-102-631-672-673 316-173-317 625-617-675-657 361-3_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 675-765-731-107-270-261-136-716-715-756-162-160-315-107-130-263-132-137-156-132-103-163-163-564-623-716-123-163-612-103-103-570-132-163-132-132-130-157-163-167-261-627-162-650-1_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 132-136-310-103-362-310-316-301-302-203-231-261-517-675-326-132-321-326-621-231-675-126-132-136-123-132-312-132-501-537-567-623-302-760-621-627-213-132-103-130-276-216-126-136-6_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 236-623-567-765-765-765-675-265-326-216-362-632-201-326-236-265-321-263-326-236-263-236-236-213-236-236 263-236-126-236-326-326-321-213-321-123-632-362-236-652-326-267-623-162 2_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 675-672-176-162 321-230-251-316-564-506-627-602-601-301-136-132-136-106-601-163-620-602-637-612-631-132-106-312-216-120-623-316-301-756-621-306-236-273-260-206-207-260-213-602-6_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 617-760-265-254-132-127-163-125-312 132-123-136-167-136-103 301-312-607-673-631-163-136-302-123-132-132-136-503-573-546-547-504-352-547-547-572-756-127-576-176-165-175-173-175-7_V.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 620-260-627-602-620-602-620-620-620 602-602-620-620-267-602-672-602-625-602-625-603-672 123-260-267-260-260-725-620-627-627-632-675-627-657-625-627-650-620-670-670-602-625-621-6_W.mp3)
  Segment 17: 25216ms ~ 35584ms (file: 261-216-236-210-261-213-623-263-136-106-167-127-123 267-621-260-231-751-267-216-263-216-321-312-267-213-260-260-263-136-163-160-106-160-163-136-361-316-316-236-316-136-231-312-1_W.mp3)
  Segment 18: 36584ms ~ 37160ms (file: 576-576-576-576-576-576-756-765-576-576 576-576-576-576-574-756-576-576-576-576-576-576-576-576-576-576-567 576-567-576-576-576-576-576-576-576-567-576-576-576-576 576-576-576-5_W.mp3)
  Segment 19: 38160ms ~ 38736ms (file: 132-217-231-207-275-317-315-315-310-136-754-754-756-765-765-137-173-751-137-103-316-106-106-103-165-631-302-301-136-136-506-176-167-601-751-132-236-725-372-137-103-132-765-261-6_W.mp3)
  Segment 20: 39736ms ~ 40312ms (file: 627-267-627-631 765-576-752-756-765-715-167-176-567-561-105-150_W.mp3)
  Segment 21: 41312ms ~ 41888ms (file: 263-263-310-103-126-132-756-751-705-167-310-360-316 617-617-132-321-503-576-576-657-605-601-260-260-625-276-206-203-312-320-752-752-103-516-156-576-753-576-576-567-765-756-316-3_W.mp3)
  Segment 22: 42888ms ~ 43464ms (file: 175-716-721-715-712-716-731-731-672-723-721-712-712-132-103-317-617-316-675-132-263-137-316-103-126-612-602-612-621-621-763-263-623-621-671-163-103-102-123-125-136-132-136-361-1_V.mp3)
  Segment 23: 44464ms ~ 45040ms (file: 265-627-265-265-625-625-625-267-261-267-263-261-620-620-621-625-621-625-625-625-260-620-625-625-265-625-625-625-265-625-265-625-625-627-267-261-263-261-261-263-620-620-620-620-6_W.mp3)
  Segment 24: 46040ms ~ 56156ms (file: 206-261-301-103-670-102-301-756-567-206-751-137-576-102-130-327-312-273-312-103-230-236-270-756-756-756-267-562-756-756-206-237-231-103-310-207-231-273-130-312-132-310-137-130-3_W.mp3)
  Segment 25: 57156ms ~ 57732ms (file: 153 123-123-163-136-103-301-103-136-367-765-167-754-756-572-670-103-103-136-163-105-107-601-167-623-120-236-176-601-613-163-103-103-216-216-123-263-206-312-301-312-316-136-301-3_V.mp3)
  Segment 26: 58732ms ~ 59308ms (file: 103-715-375-371-712-723-321-127-307-360-106-673-362-317-673-106-175-765-675-675-675-675-675-675-675-675-675-765-675-617_W.mp3)
  Segment 27: 60308ms ~ 60884ms (file: 136-136-136-531-136-130-106-136-316-263-263-623 362-162-613-103-357-105-157-137-163-132-103-163-617-167-167-316-103-136-306-136-136-153-136-165-136-613-136-361-106-136-136-103-1_V.mp3)
  Segment 28: 61884ms ~ 62460ms (file: 625-765-652-563-576-567-675-625-672-236-627-275-267-625-216-276-256-765-572-576-572-716-623-123-136-672-126-623-326-231-263-761-623-361-576-576-657-631-657-651-257-765-675-756-6_W.mp3)
  Segment 29: 63460ms ~ 64036ms (file: 675-627-276-627-652-625-675-621-267-516-175-762-167-175-657-672-657-102-132-216-263-216-213-132-671-176-617-362-267-672-362-625-632-652-652-653-103-756-752-657-675-752-675-675-6_W.mp3)
  Segment 30: 65036ms ~ 65612ms (file: 136-137-316-516-162-236-123-126-136-613-216-602-162-165-601-130-617-136-130-675-165-316-103-167-167-617-103-132-103-610-136-301-672-627-261_V.mp3)
  Segment 31: 66612ms ~ 67188ms (file: 371-173-375-731-137-135-107-130 317-537-175-153-317-135-317-137-137-301-317-301-317-753-317-173-137-137-103-137-310-103-375-317-301-713 103-137-351-301-315-715-317-137-317-573-1_V.mp3)
  Segment 32: 68188ms ~ 68764ms (file: 605-605-650-605-602-605-602-620-672 607-607-605-602-657-605-671-605-672-672-675-672-675-673-672 602-603-605-620-601-650-650-605-650-605-601-672-762-675-762 601-610-602-610-605-6_W.mp3)
  Segment 33: 69764ms ~ 70340ms (file: 576-627-236-126-213-236-736-627-316-365-637-637-672-672-627-263-207-617-261-326-237-327-263-310-103-130-321-321-302-360-361-263-123-261-612-263-620-132-632-623-620-670-627-620-2_W.mp3)
  Segment 34: 71340ms ~ 71916ms (file: 576-257-625-267-326-321-623-675-627-612_W.mp3)
  Segment 35: 72916ms ~ 73492ms (file: 671-617-617-162-613-613-612-167-136-671-610-102-102-102-236-260-312-312-310-310-621-236-621-160-167-672-617-612_W.mp3)
  Segment 36: 74492ms ~ 75068ms (file: 102-102 721-571-716-701-157-210-123-120_V.mp3)
  Segment 37: 76068ms ~ 76644ms (file: 321-360-301-672-627-623-237-217-216-652-632-312-612-136-316-672-213-132-751-574-753-574-752-627-627-267-672-206-260-162-103-132-230-230-603-162-230-206-231-213-102-132-102-102-6_W.mp3)
  Segment 38: 77644ms ~ 78220ms (file: 576-675-765-756-756-675-657-765-567-576-567-675-765-765-756-567-576-567-567-567-657-726-672-657-267-562-675-756-675-756-752-675-756-576 567-765-657-765-765-763-576-756-567-765-6_W.mp3)
  Segment 39: 79220ms ~ 79796ms (file: 236-260-260-261-632-261-263-627-763-673-612-260-236-267-265-625-265-265-265-265-625-627-265-263-261-267-261-652-261-625-216-265-267-267-267-267-263-267-260-216-106-162-621-147-1_W.mp3)
  Segment 40: 80796ms ~ 81372ms (file: 673-627-612-126-316-102-136-160-132-160-562-102-176-261-103-623-132-671-123-216-621-216-216_W.mp3)
  Segment 41: 82372ms ~ 82948ms (file: 136 132 261-216-312-327 752-725-726-725-752-752-572-752-752-752-572-752-257-726-276-275-726-726-275-276-276-276-267-276-657-267 607-607-706-627-164-672-167-217-612-162-321-751-5_W.mp3)
  Segment 42: 83948ms ~ 84524ms (file: 627-263-607-260-261-620-126-106-620-260-260-205-162-106-162-167-162-250-610-261-206-613-301-301-160-562-562-760-126-106-265-167-136-137-132-672-621-620-620-621-136-153-105-601-1_W.mp3)
  Segment 43: 85524ms ~ 86100ms (file: 675-756-657-675-567-675-576-657-675-765-765-657-765-657-675-675-657-657-765-672-765-675-675-657-675-576-567-675-567-675-625-376-675-675-765-576-675-756-567-756-756-765-567-765-7_W.mp3)
  Segment 44: 87100ms ~ 87676ms (file: 132-627-617-601-136-163-132-123-123-126-321-132-103-132-136-132-167-765 267_V.mp3)
  Segment 45: 88676ms ~ 89252ms (file: 601-162-705-375-137-136-765-260-627-762-756-572-510-105-315-570-576-716-261-126-762-173-162-172-763-726-672-576-167-620 627-602-657-216-263-236-627-210-216-201-102-201-216-162-6_W.mp3)
  Segment 46: 90252ms ~ 90828ms (file: 132-312-231 673-173-731-637-162-367-765-326-376-617-127-316-102-136-132-103-612-370-316-103-103-561-167-163-135-351-127-761-176-163-615-136-613-126-103-231-106-637-263-362-621 2_V.mp3)
  Segment 47: 91828ms ~ 92404ms (file: 315-163-163-301-136-106-136-756-756-765-675-675-673-675-261-126-123-765-765-760-765-132-760-765-763-617-675-571-657-314-413-657-675-675-657-625-620-625-620-762-276-765-716-713-7_V.mp3)
  Segment 48: 93404ms ~ 93980ms (file: 756-316-163-156-136-351-132-130-135-103-716-312-316-103-136-103-103-613-316-106-137-613-362-103-163-520-153-615-173-312-107 162-106-123-137-103-136-561-160-105-531-160-106-136-1_V.mp3)
  Segment 49: 94980ms ~ 95556ms (file: 675-671-617 756-657-756-152-136-136-513-106-106-671-103-106-627-627-163-167-106 213-260-120-213-126-621-326-103-126-756-627-760-672-671-103-136 613-165-127-263-126-231-160-160-5_V.mp3)
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
> **Time**: 2026-04-04 04:14:57
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "서둘러서 가자 이러다 늦겠어",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "서둘러서", "start_ms": 20, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "가자", "start_ms": 210, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "이러다", "start_ms": 330, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "늦겠어", "start_ms": 450, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 3,
"transcript": "오후 뉴스가 걱정되지만 지금 당장 출발하지 않으면 안 되는 상황입니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "오후", "start_ms": 10, "dur_ms": 40, "phase_lr_deg": 5.0},
{"word": "뉴스가", "start_ms": 60, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "걱정되지만", "start_ms": 130, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "지금", "start_ms": 220, "dur_ms": 40, "phase_lr_deg": 5.0},
{"word": "당장", "start_ms": 270, "dur_ms": 40, "phase_lr_deg": 5.0},
{"word": "출발하지", "start_ms": 320, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "않으면", "start_ms": 410, "dur_ms": 50, "phase_lr_deg": 5.0},
{"word": "안", "start_ms": 470, "dur_ms": 20, "phase_lr_deg": 5.0},
{"word": "되는", "start_ms": 500, "dur_ms": 30, "phase_lr_deg": 5.0},
{"word": "상황입니다", "start_ms": 540, "dur_ms": 30, "phase_lr_deg": 5.0}
]
},
{
"segment": 4,
"transcript": "다행히 시동은 걸렸어 조심스럽게 차로 움직여",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "다행히", "start_ms": 15, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "시동은", "start_ms": 85, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "걸렸어", "start_ms": 170, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "조심스럽게", "start_ms": 260, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "차로", "start_ms": 370, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "움직여", "start_ms": 440, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 6,
"transcript": "나 보기에 네가 빨리 집도 사고 싶고 마음이 급했어",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "나", "start_ms": 20, "dur_ms": 30, "phase_lr_deg": -5.0},
{"word": "보기에", "start_ms": 60, "dur_ms": 50, "phase_lr_deg": -5.0},
{"word": "네가", "start_ms": 120, "dur_ms": 40, "phase_lr_deg": -5.0},
{"word": "빨리", "start_ms": 170, "dur_ms": 40, "phase_lr_deg": -5.0},
{"word": "집도", "start_ms": 220, "dur_ms": 50, "phase_lr_deg": -5.0},
{"word": "사고", "start_ms": 280, "dur_ms": 40, "phase_lr_deg": -5.0},
{"word": "싶고", "start_ms": 330, "dur_ms": 50, "phase_lr_deg": -5.0},
{"word": "마음이", "start_ms": 390, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "급했어", "start_ms": 460, "dur_ms": 100, "phase_lr_deg": -5.0}
]
},
{
"segment": 7,
"transcript": "제가 그래도 나이가 꽤 있잖아요",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "제가", "start_ms": 30, "dur_ms": 60, "phase_lr_deg": 10.0},
{"word": "그래도", "start_ms": 100, "dur_ms": 70, "phase_lr_deg": 10.0},
{"word": "나이가", "start_ms": 180, "dur_ms": 80, "phase_lr_deg": 10.0},
{"word": "꽤", "start_ms": 270, "dur_ms": 40, "phase_lr_deg": 10.0},
{"word": "있잖아요", "start_ms": 320, "dur_ms": 240, "phase_lr_deg": 10.0}
]
},
{
"segment": 8,
"transcript": "영포티지 느릿느릿한 이거",
"speaker": "speaker_3",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "영포티지", "start_ms": 10, "dur_ms": 180, "phase_lr_deg": -5.0},
{"word": "느릿느릿한", "start_ms": 200, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "이거", "start_ms": 360, "dur_ms": 200, "phase_lr_deg": -5.0}
]
},
{
"segment": 9,
"transcript": "이제 사실인데 이게 다 그럴 텐데 나이가 아무래도 있으면 좀 더",
"speaker": "speaker_4",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 20, "dur_ms": 40, "phase_lr_deg": 10.0},
{"word": "사실인데", "start_ms": 70, "dur_ms": 60, "phase_lr_deg": 10.0},
{"word": "이게", "start_ms": 140, "dur_ms": 40, "phase_lr_deg": 10.0},
{"word": "다", "start_ms": 190, "dur_ms": 20, "phase_lr_deg": 10.0},
{"word": "그럴", "start_ms": 220, "dur_ms": 40, "phase_lr_deg": 10.0},
{"word": "텐데", "start_ms": 270, "dur_ms": 40, "phase_lr_deg": 10.0},
{"word": "나이가", "start_ms": 320, "dur_ms": 50, "phase_lr_deg": 10.0},
{"word": "아무래도", "start_ms": 380, "dur_ms": 60, "phase_lr_deg": 10.0},
{"word": "있으면", "start_ms": 450, "dur_ms": 50, "phase_lr_deg": 10.0},
{"word": "좀", "start_ms": 510, "dur_ms": 30, "phase_lr_deg": 10.0},
{"word": "더", "start_ms": 550, "dur_ms": 20, "phase_lr_deg": 10.0}
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
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 12,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 13,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 14,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 15,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 16,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 19,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 20,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 40,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-04 04:14:57*
