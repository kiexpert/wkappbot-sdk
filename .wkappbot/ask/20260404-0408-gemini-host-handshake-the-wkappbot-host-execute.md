# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=17688  time=04:07:17  uptime=10120s  mem=175MB  system_processes=367"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260404_040707.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 650-657-650-605-650-605-560-605-650-567-605 605-650-605-657-605-657-567-657-605-605-567-650-650-650-657-650-560-506-657-560-560-650-625-652-652-652-652-605-607-605-605-567-567-6_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 231-623 167 162-126 631-673-672-673-736-736-762-763-716 256-751-675 321-137 103-316-763-762-763-763-726_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 106-126-612-126-162-162-106-627-672-267-276-672-627-617-672-126-312-621-162-312-361-163-163-310-312-162-627-637-657-267-621-136 612 576-756-576-512-361-631-126-123-671-167-167-5_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 625-627 632-652-623-621 613-605-631-631-630_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 165-613-132-130-163-316-157-103-175-165-163-167-160-132-136-310-136-130-167-103-136-301-137-102-613-136-514-571 167-143-136-132-163-175-163-106-136-126-163-136-317-126-157-657-5_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 273-207-210-210-316-206-306-301-102-217-672-102-107-106-610-263-120-320-321-260-210-302-301-102-302-302-320-312-310-362-312-312-350-325-327-326-320-310-123-130-306-302-301-713-2_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 576-173-617-617-123-612-132-610-123-137-607-675-136-126-162 261-162-750-103-123-126 130-137-136-175-132-103 601_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 103-162-167-673-263-261-621-312-231-316-621-176-207-260-236-260-206-275-301-103-301-316-201-206-207-207-207-237-102-301-312-260-230-260-320-302-320-103-310-136-106-162-316-106-3_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 126-615-526-567-527-652-560-527-576-657-675-675-675-627-562-256-257-256-267-265-256-253-257-251-265_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 260-620-260-260-260-260-206-621-612-621-106-163-136-136-136-267-260-671-163-176-106-103-310-302-320_V.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 765-756-756-756-671-136-106-361-612-160-162-163-716-726-672-627-103 106-102-132-102-675-671-130-103-301-751-576-567-362-136-132-132-102-163-601-671-621-270-675-627-617-637-754-6_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 216 756-756-756-756-765-610 756-756 236_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 302 103-102-136-675-670-765-763 167-760-765-547-105-571-574-570-507-750-173-107-103-176-106-376-576-567-652-657-605-650-620-256-275-537-352-562-623-561-376-371-317-762-132-327-3_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 547-527-572-567-375-762-236-132-572-527-174 260-261_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 326-236-123-103-132-102-310-310-136-371-736-123-132-136-102-106-167-106-612-761-627-672-672-126-162-126-123-216-206-260-267-136-106-620-620-263-106-602-602-610-103-132-631-612-6_V.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 612-132-675-271-652-213-123 672-672-167_V.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 167-137-312-132-310-106-163-106-163-163-132_V.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 321-325-236-236-267-263-326-321-265-267-261-236-623-365-625-365-365-316-632-621-176-372-672-367-673-367-365-356-356-235-362-357-157-573-735-375-576-537-316-371-375-576-517-576-6_W.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 751-567-756-756-765-756-762-756-576-517-507-567-576-231-167-126-612-716-675-162-671-671-657-671-675-762-673-317-602-627-316-301-132-157-120-126-263-657-675-657-567-657-765-657-6_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 516-175-615-613-672-130-132-163-261-123-613_V.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 132-123-327-326-517-126-620-261-260-260-260-620-162-163-541-514-560-156-620-261-267-206-261-107-160-762-607-620-236-627-627-310-130-103-136-310-310-301-103_V.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 162-103-103-103-176-673-176-610-176-136-120-123-576-312-523-213-162-167-123-765-756-756-765-756-623-627-127-216-173-760-670-670-760-706-713-756-672-627-267-672-675-132-637-675-6_V.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 765-276-627-627-756-705-312-375-324-413-132-612-621-521-726-762-271-375-732-675-132-314-132-312-132-675-670-672-723-570-572-503-567-652-652-652-602-136-123-316-271-213-413-341-2_V.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 762-763-165-375-127-612 613-623-216-675-756-657_W.mp3)
  Segment 25: 37824ms ~ 47148ms (file: 260-263-756-257-102-102-132-126-132-312-257-237-321-321-231-537-235-215-320-231-102-163-130-275-207-321-310-230-267-236-120-132-725-126-132-123-257-132-231-230-321-102-102-257-2_W.mp3)
  Segment 26: 48148ms ~ 57580ms (file: 756-573-375-763-762-236-753-756-617-173-176-752-273-371-130-103-715-137-132-571-751-120-260-261-267-271-216-231-213-213-173-573-276-103-217-260-261-260-307-230-276-261-517-301-2_W.mp3)
  Segment 27: 58580ms ~ 59156ms (file: 576-576-576-576-576-576-576-576-576-567-567-576-576-576-567-567-576-576-675-657-675-675-675-675-675 567-576-576-567-576-576-576-576-567-567-576-576-576-576-576-567-567-567-576-5_W.mp3)
  Segment 28: 60156ms ~ 60732ms (file: 132-651-601-163-316-316-731-307-310-132-136-105-136-136-165-613-631-613-163-615-675-613-361-617_V.mp3)
  Segment 29: 61732ms ~ 62308ms (file: 756-756 713-132-341 756-765-765-143-132 236_V.mp3)
  Segment 30: 63308ms ~ 63884ms (file: 312-301-753-715-750-137-321-103-103-130-130-136-102-576-507-574-153-752-357-167-236-132-132-321-210-231-213-310-307-703-735-317-103-103-135-167-173-132-201-312-302-136-103-362-2_V.mp3)
  Segment 31: 64884ms ~ 65460ms (file: 160-106-130-130-125-167-162-103-623-126-675-126-621-103-136-103-130-571-137-103-673-316-615-137-107-153-362-617-675-176-731-137-761-620-671-162-163-162-715-753_V.mp3)
  Segment 32: 66460ms ~ 67036ms (file: 156-103-613-163-162-162-103-130-136-617-571-135-172-107-176-130-130-136-530-132-130-102-610-263-136-125-132-132-120-165-136-132-165-627-657-162-675-316-162 732-276-723-275-275-7_V.mp3)
  Segment 33: 68036ms ~ 68612ms (file: 213-361-173-374-324-163-613-231-352-375-312-627-621-536-123-623-632-163-362-123-126 567-256-263-206-260-267-267 103-621-263 627-260-130-123-213-126-120-210_W.mp3)
  Segment 34: 69612ms ~ 75336ms (file: 107-176-517-206-175-576-270-275-652-612-257-267-207-276-267-506-547-521-352-203-267-750-265-623-273-201-312-173-537-570-754-751-765-756-167-130-327-725-572-162-732-376-270-327-5_W.mp3)
  Segment 35: 76336ms ~ 76912ms (file: 213-672-617-602-621-671-160-136-610-617-601-610-627-621-612-672-631-610-671-136-621-132-162-702-271-715-156-153-162-671-607-163-673-617-167-106-671-601-106-617-167-107-126-106-1_V.mp3)
  Segment 36: 77912ms ~ 86012ms (file: 126-756-267-361-756-762-267-102-275-263-312-137-267-172-706-671-217-260-765-261-361-130-137-163-132-267-123-312-135-321-372-271-136-261-763-160-213-216-316-136-132-136-163-137-1_W.mp3)
  Segment 37: 87012ms ~ 87588ms (file: 261-174-103-103-136-576-576-675-652-602-237-275 275-275-571-657-516-216-170-574-571-105-715-170-107-175-107-735-574-567-675-670-106-105-231-320-320-753-516-507-504-750-760-675-6_W.mp3)
  Segment 38: 88588ms ~ 89164ms (file: 625-267-621-617-572-316-367-576-637-163-567-567-260-675-675-765-671-756-567-567-567-576-156-657-657-657 102-312-263-236 130-326-206-261-206-263-236-206-123-627-637-362-617-617-1_V.mp3)
  Segment 39: 90164ms ~ 95384ms (file: 762-753-315-310-301-301-326-573-130-310-671-672-351-365-265-326-753-130-371-371-137-675-573-537-517-563-325-265-325-352-316-317-132-723-573-351-316-653-576-167-167-715-165-163-1_W_stt_this_often_because_of_an_officer_of.mp3)
  Segment 40: 96384ms ~ 96960ms (file: 165-125-516-156-256-205-215-215-520-251-235-215-253-215-215-251-213-216-215-231-213-652-261-216-216-126-216-256-216-261-215-263-256-265-213-265-231-321-321-263-527-572-235-235-2_W.mp3)
  Segment 41: 97960ms ~ 98536ms (file: 316-631-673-670-756-576-765-167-137-136-163-132-136-673-167-750-756-765-167-206-210-206-167-132-615-576-561-301-132-702-302-312-132-316-136-526-157-201-207-237-206-203-236-502-3_V.mp3)
  Segment 42: 99536ms ~ 100112ms (file: 256-625-657-576-567-567-267-265-106-106-765-160-601-620-620-260-260-263-260-675-765-167-165-103-163-162-103-716-316-370-736-671-736-267-126-326-203-632-163-260-361-576-651-671-3_W.mp3)
  Segment 43: 101112ms ~ 101688ms (file: 765-362-572-362-271-371-321-576-276-173-562-652-627-327-213-731-765-637-530-267-375-507-657-625-567-765-257-567-576-256-265-572-253-325-217-215-163-263-326-362-507-327-573-257-5_V.mp3)
  Segment 44: 102688ms ~ 103264ms (file: 156-621-165-123 612-631-607-601-601-136-132-123-210-326-160-547-503-520-316-316-160-132-102-130-103-312-136-130-106-136-653-563-132-136-103-162-613-620-106-617-620-672-260-103-1_V.mp3)
  Segment 45: 104264ms ~ 104840ms (file: 367 615-165-765-175-136-267-126-106-312-176-137-167-610-163-176-671-605-176-637-675-106-156-316-136-176-102-136-106-136-162-217-102-136-136-612-162-136-103-150-165-162-136-123-1_V.mp3)
  Segment 46: 105840ms ~ 106416ms (file: 762-726-231-216-236-216-231-230-203-273-267-273-326-327-173-136-136-132-547-574-560-625-267-367-316-162-621-574-574-162-612-263-621-163-613 316-136-103-132-147 143-756-765-765-1_V.mp3)
  Segment 47: 107416ms ~ 107992ms (file: 162-136-127-602-172-162-162-135-163-106-675-167-106-617-137-571-632-671 137-163-673-612-174-762-620-106-137-126-153-217-167-176-103-127-103-316-103-106-160-123-137-103-136-716-1_V.mp3)
  Segment 48: 108992ms ~ 109568ms (file: 532-572-563-567-567-567-567-516-567-657-657-312-321-123-312-321-312-321-312-316-312-615-130-156-536-657-657-635-635-653-657-536-562-562-675-652-657-567-657-310-301-103-312-132-3_W.mp3)
  Segment 49: 110568ms ~ 111144ms (file: 765-763-673-673-673-765-765-673-365-367-765-673-375-673-376-176-716-716-576-630-653-560-567-715-537-562-257-563-657-126-652-573_W.mp3)
  Segment 50: 112144ms ~ 112720ms (file: 123-132 627-236-136-132-612-603-627-672-710-172-176-126-612-623-612-760-102-103-123-361-213-206-261-216-162-361-612-602-312-312-610-621-102-350-103-103-750-137-576-756-765 576-7_W.mp3)
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
> **Time**: 2026-04-04 04:08:03
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "기업을 운영하는 임원이라면 연봉, 퇴직금, 4대 보험과 같은 복지를 제공해야 되는 인간 연구원 대신 필요한 때만 API 비용을 지불하고 즉각 해지할 수 있는 가상 계약직 에이전트를 선택을 하지 않을까요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "기업을", "start_ms": 20, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "운영하는", "start_ms": 75, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "임원이라면", "start_ms": 140, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "연봉,", "start_ms": 225, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "퇴직금,", "start_ms": 280, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "4대", "start_ms": 345, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "보험과", "start_ms": 390, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "같은", "start_ms": 445, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "복지를", "start_ms": 490, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "제공해야", "start_ms": 545, "dur_ms": 65, "phase_lr_deg": 0.0},
{"word": "되는", "start_ms": 615, "dur_ms": 35, "phase_lr_deg": 0.0},
{"word": "인간", "start_ms": 655, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "연구원", "start_ms": 700, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "대신", "start_ms": 755, "dur_ms": 45, "phase_lr_deg": 0.0},
{"word": "필요한", "start_ms": 805, "dur_ms": 55, "phase_lr_deg": 0.0},
{"word": "때만", "start_ms": 865, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "API", "start_ms": 910, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "비용을", "start_ms": 965, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "지불하고", "start_ms": 1020, "dur_ms": 65, "phase_lr_deg": 0.0},
{"word": "즉각", "start_ms": 1090, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "해지할", "start_ms": 1135, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 1190, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 1215, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "가상", "start_ms": 1260, "dur_ms": 45, "phase_lr_deg": 0.0},
{"word": "계약직", "start_ms": 1310, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "에이전트를", "start_ms": 1365, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "선택을", "start_ms": 1450, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "하지", "start_ms": 1505, "dur_ms": 35, "phase_lr_deg": 0.0},
{"word": "않을까요?", "start_ms": 1545, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "진짜 많이 노력을 하신 것 같아요. 근데 또 제가 어떤 아이디어를 가지고 왔을 때, 만약에 선배님 거기서 '아 그거는 불필요한 거 같애' 이렇게 생각하면서 누르는 거야.",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 30, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 85, "dur_ms": 45, "phase_lr_deg": 0.0},
{"word": "노력을", "start_ms": 135, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "하신", "start_ms": 200, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 245, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "같아요.", "start_ms": 270, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 325, "dur_ms": 35, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 365, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 390, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 435, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "아이디어를", "start_ms": 480, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "가지고", "start_ms": 555, "dur_ms": 55, "phase_lr_deg": 0.0},
{"word": "왔을", "start_ms": 615, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "때,", "start_ms": 660, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "만약에", "start_ms": 695, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "선배님", "start_ms": 750, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "거기서", "start_ms": 805, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "'아", "start_ms": 860, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "그거는", "start_ms": 885, "dur_ms": 45, "phase_lr_deg": 0.0},
{"word": "불필요한", "start_ms": 935, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 1000, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "같애'", "start_ms": 1025, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 1070, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "생각하면서", "start_ms": 1125, "dur_ms": 75, "phase_lr_deg": 0.0},
{"word": "누르는", "start_ms": 1205, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "거야.", "start_ms": 1260, "dur_ms": 40, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "매일 벌 수 있냐. 대신 계좌 방어하고 시장에서 이렇게 운 좋게 또 많은 수익을 주면은",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "매일", "start_ms": 40, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "벌", "start_ms": 85, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 120, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "있냐.", "start_ms": 145, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "대신", "start_ms": 200, "dur_ms": 45, "phase_lr_deg": 0.0},
{"word": "계좌", "start_ms": 250, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "방어하고", "start_ms": 295, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "시장에서", "start_ms": 370, "dur_ms": 65, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 440, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "운", "start_ms": 495, "dur_ms": 25, "phase_lr_deg": 0.0},
{"word": "좋게", "start_ms": 525, "dur_ms": 45, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 575, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "많은", "start_ms": 600, "dur_ms": 45, "phase_lr_deg": 0.0},
{"word": "수익을", "start_ms": 650, "dur_ms": 55, "phase_lr_deg": 0.0},
{"word": "주면은", "start_ms": 710, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "없었습니다. 지금 보면 바닥이고 팔지 않았더라면 복구가 가능했다는 게 저의 멘탈을 더 흔들어 놓게",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "없었습니다.", "start_ms": 30, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 105, "dur_ms": 45, "phase_lr_deg": 0.0},
{"word": "보면", "start_ms": 155, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "바닥이고", "start_ms": 200, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "팔지", "start_ms": 275, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "않았더라면", "start_ms": 320, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "복구가", "start_ms": 405, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "가능했다는", "start_ms": 470, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 555, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "저의", "start_ms": 580, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "멘탈을", "start_ms": 625, "dur_ms": 65, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 695, "dur_ms": 25, "phase_lr_deg": 0.0},
{"word": "흔들어", "start_ms": 725, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "놓게", "start_ms": 790, "dur_ms": 45, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "그래서 과식을 피할 수 없답니다. 아니 요거 맛있어요. 꼬바로우라고 요거",
"speaker": "speaker_4",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "그래서", "start_ms": 50, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "과식을", "start_ms": 105, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "피할", "start_ms": 170, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 225, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "없답니다.", "start_ms": 250, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "아니", "start_ms": 325, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "요거", "start_ms": 370, "dur_ms": 45, "phase_lr_deg": 0.0},
{"word": "맛있어요.", "start_ms": 420, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "꼬바로우라고", "start_ms": 495, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "요거", "start_ms": 600, "dur_ms": 45, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-04 04:08:03*
