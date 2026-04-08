# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=21096  time=07:31:35  uptime=14368s  mem=717MB  system_processes=409"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_072739.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 6084ms (file: 270-213-312-231-132-301-753-106-607-602-260-102-103-173-570-206-206-216-263-217-103-216-213-213-354-721-135-136-107-206-276-132-301-237-312-267-672-670-263-207-267-517-176-152-7_V.mp3)
  Segment 2: 7084ms ~ 13348ms (file: 130-103-756-570-257-301-103-750-715-130-350-753-175-301-103-130-701-571-301-310-135-137-302-570-756-301-136-316-317-312-361-713-351-350-301-350-310-375-310-731-317-137-310-132-3_V.mp3)
  Segment 3: 14348ms ~ 21476ms (file: 312-756-726-326-273-236-276-156-750-327-276-132-312-260-620-206-230-263-756-237-263-321-132-263-765-260-326-635-570-756-236-573-517-756-756-130-132-725-725-175-320-120-267-705-1_W.mp3)
  Segment 4: 22476ms ~ 23052ms (file: 751-715-751-751-571-750-750-756 576-576-756-756-756-705-756-750-756-751-576-567-710-750-750-756-750-705-701-751-715-701-705-701-715-710-701-751-507-576-157-571-573-657-567-576-5_V.mp3)
  Segment 5: 24052ms ~ 33664ms (file: 103-123-301-621-267-675-762-326-267-237-210-132-216-103-672-367-263-617-162-263-236-261-267-260-672-627-765-260-106-210-275-312-132-310-623-160-756-726-267-261-263-312-273-160-6_W.mp3)
  Segment 6: 34664ms ~ 43088ms (file: 602-761-167-617-260-206-520-523-103-153-103-123-126-206-705-170-176-576-256-276-271-157-657-260-102-201-216-102-576-256-260-201-312-123-203-527-625-201-570-574-715-751-375-127-2_V.mp3)
  Segment 7: 44088ms ~ 44664ms (file: 570-275-752-562-652-572-507-123-572-726-167-256-756-576-502-562-567-276-316-310-371-312-310-762-267-526-372-623-527-657-752-627-257-726-706-312-754 371-317-137-317 673-673-231-6_V.mp3)
  Segment 8: 45664ms ~ 46240ms (file: 135 135-162-357-702-310-126-162-610-136-316-316-310-136-310-136-132-136-103-316-130-316-163-136-106-132-162-120-132-312-132-130-135-132-103-517-571-135-157-573-132-157-513-517-1_W.mp3)
  Segment 9: 47240ms ~ 56276ms (file: 263-132-301-301-316-310-310-317-312-276-102-132-126-263-267-261-216-623-670-673-617-102-206-310-310-103-326-276-620-216-173-167-107-136-123-231-306-326-321-320-203-136-267-261-2_W.mp3)
  Segment 10: 57276ms ~ 57852ms (file: 326-267-327-623-267-321-207-215-657-520-275-327-172-657-130-156-576-576-153-612-675-756-657-675-675-756-756_W.mp3)
  Segment 11: 58852ms ~ 64540ms (file: 276-725-567-672-267-765-167-751-107-327-320-756-321-263-756-372-671-172-761-751-754-752-175-167-160-321-756-672-267-312-103-107-103-361-357-176-716-106-175-267-216-601-162-371-7_W.mp3)
  Segment 12: 65540ms ~ 70940ms (file: 207-216-123-267-602-703-263-672-763-267-267-627-357-752-326-263-621-527-257-126-756-273-263-573-207-237-732-237-327-672-760-627-276-572-756-750-170-231-267-267-623-673-256-756-6_V.mp3)
  Segment 13: 71940ms ~ 80904ms (file: 275-607-206-236-320-132-320-310-102-571-127-102-570-375-705-230-206-620-627-620-627-623-206-236-527-230-132-320-263-203-231-720-230-123-317-132-132-102-125-732-235-102-230-725-1_V.mp3)
  Segment 14: 81904ms ~ 82480ms (file: 753-763-756-752-675-517-573-715-657-176-765-756-715-627_W.mp3)
  Segment 15: 83480ms ~ 96836ms (file: 132-732-276-275-236-102-107-237-237-237-725-230-176-275-120-276-572-132-237-762-137-321-301-231-213-237-103-132-756-576-207-753-507-135-132-574-375-137-132-267-102-132-317-325-1_W.mp3)
  Segment 16: 97836ms ~ 103632ms (file: 235-260-527-267-250-547-265-527-103-130-276-236-321-312-675-765-675-576-163-672-625-173-107-176-156-514-207-276-673-725-201-260-526-267-125-275-273-523-137-261-230-765-523-763-3_W.mp3)
  Segment 17: 104632ms ~ 105208ms (file: 137-103-705-730-752-173-175-715-517-573-750-752-715 572-571-571-517-153-571-751-157-153-571-317-371-753-350-701-710-157-731 571-571-571-517-571-517-150-175-715-517-576-175-130-1_V.mp3)
  Segment 18: 106208ms ~ 118808ms (file: 257-267-267-267-257-236-754-267-725-756-276-276-236-754-321-320-237-267-263-231-573-102-130-763-754-267-752-105-672-762-231-327-726-276-756-751-547-231-267-210-547-574-132-231-2_W.mp3)
  Segment 19: 119808ms ~ 120384ms (file: 567-105-675-316-371-317-310-275-321-216-326-621-267-765-256-752-231-267-672-362-367-673-167-173-367-175-765-237-573-752-276-315-672-260-261-623-623-167-632-267-236-261-123-367-2_V.mp3)
  Segment 20: 121384ms ~ 125416ms (file: 326-236-236-537-561-310-312-165-163-163-123-316-316-132-317-321-231-162-136-165-126-162-163-657-610-217-257-756-357-253-271-765-657-675-657-653-657-653-657-657-657-652-657-571-3_W.mp3)
  Segment 21: 126416ms ~ 126992ms (file: 617-132-513-531-517-351-135-356-513-153-561-135-316-137-615-537-574-574-576-546-576-751-571-576 651-561-567_W.mp3)
  Segment 22: 127992ms ~ 128568ms (file: 175-175-176-173-175-176-175-167-176-176-137 175-175-571-751-175-175-157-157-571-157-751-176-175-175-176-175-175-176-176 130-173-175-716-175-137-173-175-173-167-167-176-167-175-1_W.mp3)
  Segment 23: 129568ms ~ 134860ms (file: 372-301-573-720-301-750-756-576-514-751-574-723-321-370-173-576-157-317-132-312-631-675-602-236-602-263-260-137-123-537-571-765-213-756-751-157-752-163-103-207-623-256-102-210-5_W.mp3)
  Segment 24: 135860ms ~ 144464ms (file: 217-123-275-752-275-267-120-127-172-174-176-107-173-163-721-560-670-726-506-607-176-167-176-671-271-237-267-267-312-213-321-103-107-210-203-521-217-275-210-207-201-102-102-103-1_W.mp3)
  Segment 25: 145464ms ~ 153060ms (file: 132-103-320-321-312-231-321-312-301-321-312-103-210-102-312-327-231-103-136-130-370-310-326-301-201-103-312-130-213-231-130-263-213-132-102-312-123-312-123-213-132-312-102-312-2_V.mp3)
  Segment 26: 154060ms ~ 154636ms (file: 371-312-132-167-317-753-716-356-357-571-517-571-137-675-765-376-756-176-137-132-715-617-673-763-673-613-675-672-671-675-176-127-167-716-516-102-326-103-136-136-167-265-132-627-2_W.mp3)
  Segment 27: 155636ms ~ 166796ms (file: 750-370-756-657-175-175-107-735-573-756-753-753-756-756-753-103-317-371-735-751-370-750-753-735-732-327-672-375-735-103-173-317-756-372-735-370-137-751-750-375-753-573-107-573-5_W.mp3)
  Segment 28: 167796ms ~ 168372ms (file: 761-123-327-641-134-135-163-671-123-316-123-167-165-126-167-132-163-136-124-132-263-173-146-123-135-132-136-135-126-125-421-163-132-317-316-312-137-137-123-126-126-126-136-132-1_V.mp3)
  Segment 29: 169372ms ~ 176680ms (file: 267-210-267-230-723-120-267-267-276-261-320-213-213-206-621-126-617-210-610-163-137-263-103-130-572-261-261-715-102-120-130-123-120-130-136-130-132-132-130-130-136-371-317-130-1_W_stt_the_long.mp3)
  Segment 30: 177680ms ~ 178256ms (file: 136-132-135-137-164-312-123-175-136-135-137-136-137-135-132-132-127-132-132-134-137-164-125-164-163-157-127 132-312_V.mp3)
  Segment 31: 179256ms ~ 189084ms (file: 273-275-572-320-231-275-267-260-260-261-263-201-231-321-201-213-236-321-253-521-213-130-236-123-310-102-312-320-103-123-127-176-126-162-123-172-152-267-201-201-132-273-276-276-2_W.mp3)
  Segment 32: 190084ms ~ 199264ms (file: 124-231-213-132-132-102-103-103-102-321-230-231-203-230-213-726-130-321-132-102-230-103-102-130-102-132-103-167-163-574-250-231-540-123-574-562-107-170-231-231-321-120-372-301-3_V.mp3)
  Segment 33: 200264ms ~ 200840ms (file: 176-673-163-735-263-312-576-316-750-316-326-123-263-672-267-275-527-753-132-507-623-260-527-627-506-563-357-637-672-576-632-562-570-502-175-132-731-762-765-527-576-275-276-756-3_V.mp3)
  Segment 34: 201840ms ~ 209940ms (file: 756-752-301-321-310-275-256-756-574-230-712-574-253-756-576-175-265-321-317-106-625-326-267-267-260-132-231-120-231-301-132-106-143-310-261-106-310-321-751-372-753-573-103-321-5_W.mp3)
  Segment 35: 210940ms ~ 216376ms (file: 726-756-276-527-547-273-273-723-123-120-276-236-671-576-326-103-276-230-263-627-257-705-762-762-267-120-261-162-627-107-613-635-376-730-167-106-163-310-613-620-216-752-627-261-6_W.mp3)
  Segment 36: 217376ms ~ 226232ms (file: 627-276-174-103-312-753-213-132-261-216-126-126-123-126-267-261-253-326-236-263-126-106-130-357-731-753-756-357-312-130-517-576-567-576-574-571-573-317-317-357-375-123-132-531-6_W.mp3)
  Segment 37: 227232ms ~ 233460ms (file: 326-372-736-756-571-167-570-326-236-236-230-612-320-236-263-217-671-132-175-263-320-103-756-276-120-231-320-236-123-103-751-236-236-730-752-327-326-317-263-725-765-236-756-273-7_W.mp3)
  Segment 38: 234460ms ~ 235036ms (file: 716-762-716-762-716-731 712-716-173-731-371-371-731-137-736-736-713-317-361-136-615-135-165-651-653-365-156-761-316-312-312-312-130-105-653-167_W.mp3)
  Segment 39: 236036ms ~ 236612ms (file: 276-257-527-574-236-627-267-572-576-705-725-507-572-752-327-625-267-672-670-750-267-725-723-752-657-572-765-671-371-375-175-175_V.mp3)
  Segment 40: 237612ms ~ 245892ms (file: 617-267-276-210-276-263-367-237-102-632-237-620-610-276-725-173-106-126-120-263-127-276-106-107-167-160-765-732-726-270-326-574-765-236-732-275-237-237-132-216-261-167-762-765-7_W.mp3)
  Segment 41: 246892ms ~ 247468ms (file: 231-637-675-765-673-763-376-675-132-126-107-316-163-710-137-130-136-132-715-571-750-752-123-132-716-137-103-130-317-315-136-301-312-301-130-275-127-753-607-265-123-127-751-132-2_V.mp3)
  Segment 42: 248468ms ~ 249044ms (file: 312-136-756-651-675-562-675-651-625-617-657-576-576-635-653-367-567-652-316-675-653-612-765-652-675-653-231-576-567-576-657-651-615-165-657-576-576-675-365-265-567-635-257-675-5_V.mp3)
  Segment 43: 250044ms ~ 254976ms (file: 123-310-713-123-321-103-132-213-123-130-103-130-103-173-132-103-210-213-130-103-172-102-210-120-172-130-103-213-120-123-201-231-132-132-712-123-103-132-310-130-317-103-132-103-1_V.mp3)
  Segment 44: 255976ms ~ 256552ms (file: 375-527-325-321-215-625-726-276-526-576-657-135-753-267-325-621-237-562-275-261-320 301-371-321-312-310-301-135-310-132-731-216-372-325-217-762-275-526-263-537-576-537-256-102-2_W.mp3)
  Segment 45: 257552ms ~ 263420ms (file: 621-126-216-216-326-321-321-261-213-263-216-216-216-120-210-123-127-621-621-261-172-526-312-312-127-715-175-163-756-173-175-176-107-210-263-263-267-267-263-261-263-216-612-216-1_W.mp3)
  Segment 46: 264420ms ~ 264996ms (file: 623-126-613-613-167-136-316-163-136-316-136-163-361-136-136-273-235-362-751-317-371-725-235-132-372-750-706-276-175-527-251-561-372-215-576-176-670-617-576-163-132-267-312-713-7_V.mp3)
  Segment 47: 265996ms ~ 266572ms (file: 763-672-762-762-765-765-675-672-675-765-762-765-765-765-765-762-670_W.mp3)
  Segment 48: 267572ms ~ 268148ms (file: 567-137-132-370-103-302-527-526-125-312-130-132-127-156-301 574-576-576-576-574-576-657-675-657-615-617-167-165-130-675 263-263 167-126-621 321-132-312-531-321-130-132 754-756-6_W.mp3)
  Segment 49: 269148ms ~ 269724ms (file: 612-657-576-571-567-657-657-657-312-312-123-125_W.mp3)
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
> **Time**: 2026-04-06 07:32:23
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답TITLE: Multi-Segment Audio Transcription Analysis FILE_TITLE: study_batch_20260406_072759_transcription
[
{
"segment": 1,
"transcript": "보장 이사회 긴급 회의 소집을 요청했습니다 인도네시아 국방부는 평화 유지군의 안전이 최우선 과제여야 한다며 국제 인도법 준수를 촉구했습니다 UN 사무총장 역시 최근 평화 유지군의 안전과 보안을 위협한 여러",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "보장", "start_ms": 20, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이사회", "start_ms": 350, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "I would be behind the computer I'm surprised I don't have a bad back and I can still see you know because I put in the hours behind the computer I had burnout",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "would", "start_ms": 200, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "I love my life I love what I do and the energy comes from 외도 좀 더 좋은 결과를 냈는데 계좌를 한번 보여드리면 3월에는 10억 정도 그리고 4월 첫째 주에 지금",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "mixed",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "love", "start_ms": 200, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 5,
"transcript": "앉아있다 야 이게 웬 겁니까 이게",
"speaker": "speaker_3",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "앉아있다", "start_ms": 50, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "And I'm happy I love I love this life I'm very happy yeah everything is designed to make our lives better not compete with our lives it should work along",
"speaker": "speaker_4",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "And", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "아 이거 뭐야 다 4,000원이네 어 엄청 잘해 주시네요 시민들 우리 지금 펭귄 마을 왔는데 가리나 자매가 많이",
"speaker": "speaker_5",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "아", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "강하게 움직이는 주도주들 위주로만 공략했습니다 참고로 제가 지금 매매하고 있는 계좌는 예수금을 3억에서 5억 정도",
"speaker": "speaker_6",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "강하게", "start_ms": 40, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "아니 그런데 이게 다 뭐죠 이건 유명 애니메이션의 로봇 같은데 잠수함의",
"speaker": "speaker_7",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "아니", "start_ms": 20, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 11,
"transcript": "I want to see you there with the 미나리",
"speaker": "speaker_8",
"confidence": 0.88,
"language": "mixed",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "이 진짜 인기 아 홍 홍기 이거 뭐지 환기 환기 환기 어 감사합니다 감사합니다 아 그러시구나 바로 아 감사합니다 구독자님",
"speaker": "speaker_9",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "structure build out in human history every technology revolution has made us more productive however these are going to be things that are simply impossible to do before now these",
"speaker": "speaker_10",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "structure", "start_ms": 40, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 15,
"transcript": "I enjoyed more okay this one a little bit less but it's still delicious but then again you know having the sweet out of here was better eating this to take the sweetness out of my mouth plays off each other 비 오는 날은 알밥 알밥 부추",
"speaker": "speaker_11",
"confidence": 0.92,
"language": "mixed",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "능력으로 바꾸는 단계에 들어섰다는 점입니다 빠른 생산과 현실적인 가격 그리고 꾸준한 기술 축적과 수출 전략까지 맞물리면 한국은 앞으로 더 강한 존재감을 보일 가능성",
"speaker": "speaker_12",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "능력으로", "start_ms": 40, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 18,
"transcript": "네 그리고 제가 좋아하는 요 목공은 여기서 같이 작품도 전시해서 이렇게 보고 그래서 요 세 개를 하나의 패키지로 해 가지고 할 수 있도록 그렇게 구상하고",
"speaker": "speaker_13",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "저분 남편이 만든 거라는데 나무 조각",
"speaker": "speaker_14",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "저분", "start_ms": 40, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "트럼프는 주한미군 규모를 늘려 방위비 분담금 증액을 말해왔습니다 we give you free military essentially very low 트럼프가 이번 사안을 관세 후속 협상이나 핵 추진 잠수함 도입과 연관",
"speaker": "speaker_15",
"confidence": 0.93,
"language": "mixed",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "트럼프는", "start_ms": 20, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 22,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 23,
"transcript": "백만 원으로 시작을 하셨다고요 네 저희가 십 년을 계획해서 이 집을 지으려고 시작을 한 겁니다 장고 끝에",
"speaker": "speaker_16",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "백만", "start_ms": 40, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "나가려고 하는 이 전략이 대단히 중요하다고 했죠 그리고 AI 데이터 팩토리도 현대차가 중요한 부분을 생산하려고 하는 거고 자 월드 모델 피지컬 AI 기술은 현대차가 가지고 있지 않죠 보스턴 다이",
"speaker": "speaker_17",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "나가려고", "start_ms": 30, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "왔을 때 원 투 쓰리 쓰리에다가 어서 먹어 뭐 먹을래 먹어 봐 맛있을까 좀 있음 밥 먹어",
"speaker": "speaker_18",
"confidence": 0.91,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "왔을", "start_ms": 50, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 27,
"transcript": "네 그래서 군사비 증액뿐만 아니라 전시 작전권 환수를 통해서 미국의 부담을 줄이고 여 최소한 한반도",
"speaker": "speaker_19",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 29,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 30,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 31,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 32,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 33,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 34,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 35,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 36,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 37,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 38,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 39,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 40,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 41,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 42,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 43,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 44,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 45,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 46,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 47,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 48,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 49,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 07:32:23*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=39080  time=07:31:35  uptime=14243s  mem=427MB  system_processes=409"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_072759.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 157-175-175-752-501-571-107 126-162-160-120-103-321-132-132-312-132-310-130-107-103-125-152-172-175-157-107-175-107-170-107-170-310-735-107-170-175-175-132-132 123-120-132-126-1_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 617-623-623-671-631-612-617-613-617-617-571-576-576-576-761-756-752-765-763-721-765-276-271-275-762-726-725-736-726-132-723-726-721-273-273-726-276-726 765-576-576_V.mp3)
  Segment 3: 3152ms ~ 13916ms (file: 126-231-627-526-267-267-627-236-675-273-527-574-263-263-762-326-123-120-263-267-236-273-326-672-627-267-321-263-236-321-236-132-263-276-612-236-751-326-321-762-267-267-256-236-1_W.mp3)
  Segment 4: 14916ms ~ 28380ms (file: 756-765-576-756-102-230-103-267-260-267-576-310-103-312-321-130-312-571-207-207-230-203-132-206-275-572-302-301-213-310-320-103 103-102-103-301-103-230-310-103-103-302-236-276-2_W.mp3)
  Segment 5: 29380ms ~ 29956ms (file: 507-576-576-752-612-756-567-256 613-163-163-163 671-763-761-761-671-673-763-673-673-761-637-673-671-763-673-671-167-167-156-631-376-763-763-671-673-675-672-635-361-276-637-136-3_W.mp3)
  Segment 6: 30956ms ~ 31532ms (file: 710-715-715-751-715-751-751-751-715-751-715-710-715 763-170-176-607-107-710-751-751-701-715-107-175-715-170-715-751-751-715-175-175-175-710-175-715-750 670-765-715-701-701-706-7_W.mp3)
  Segment 7: 32532ms ~ 38652ms (file: 106-612-216-263-106-132-107-617-176-106-527-102-310-231-216-236-237-260-236-236-321-132-172-671-102-102-236-260-263-132-127-326-236-267-276-723-107-172-756-106-263-167-751-105-1_W.mp3)
  Segment 8: 39652ms ~ 45628ms (file: 527-213-201-123-753-723-275-163-127-123-132-127-123-123-127-721-237-312-102-130-127-137-132-132-172-312-732-132-132-712-721-132-326-312-726-762-361-127-126-127-562-103-217-231-1_W.mp3)
  Segment 9: 46628ms ~ 47204ms (file: 715-731-137-631-752-731-276-756-726-162-706-163-167-516-231-750-521-172-231-273-215-512-216-507-321-625-276-762-256-527-567-157-275-765-236-576-725-721-261-756-321-521-216-352-1_V.mp3)
  Segment 10: 48204ms ~ 58644ms (file: 132-312-154-230-321-203-210-213-602-123-132-312-130-130-203-132-310-132-201-130-102-132 143-102-210-201-102-127-102-135-125-132-102-512-502-123-210-126-120-310-153-213-103-572-1_V.mp3)
  Segment 11: 59644ms ~ 60220ms (file: 756-576-760-670-631-671-367-607-675-672-135-713-265-570-517-623-162-570-701-127-126-132-137-312-230-267-217-237-267-712-357-265-627-560-125-521-367-125-572-613-627-136-257-236-2_V.mp3)
  Segment 12: 61220ms ~ 61796ms (file: 312 567-624-213-321-163-231-137-132-312-130-216-625 701_V.mp3)
  Segment 13: 62796ms ~ 70320ms (file: 176-103-137-137-176-176-267-236-236-320-312-312-132-130-237-327-267-273-206-260-204-210-751-267-206-230-216-261-216-210-210-204-230-231-230-263-265-263-263-236-206-203-260-260-2_W.mp3)
  Segment 14: 71320ms ~ 77692ms (file: 126-671-236-276-276-627-263-623-756-756-752-752-760-160-326-236-572-162-136-267-103-107-671-173-176-675-106-751-752-273-237-310-230-176-267-627-276-267-126-376-726-275-762-106-1_W.mp3)
  Segment 15: 78692ms ~ 79268ms (file: 576-312-546-256-276-125-576-321-612-312-725-316-635-256-672-526-365-257-723-716-726-265-327-207-213-560-621-162-506_V.mp3)
  Segment 16: 80268ms ~ 80844ms (file: 765-531-126-615-675-673-361-673-715-165-165-167-136-315-527-751-127-137-132-760-627-163-160-173 675-265 163-132-312-132-143-602-623-623-321-371-631-361-610-671-716-137-132-126-1_W.mp3)
  Segment 17: 81844ms ~ 89152ms (file: 213-126-217-276-231-123-267-627-267-213-327-103-267-267-672-725-217-104-273-216-201-261-261-213-712-576-123-236-267-276-276-217-123-271-621-263-621-612-623-627-621-261-267-267-2_W.mp3)
  Segment 18: 90152ms ~ 97784ms (file: 102-210-123-175-210-267-237-236-102-103-765-705-751-576-527-526-765-752-257-570-103-130-312-123-206-267-216-203-230-320-207-105-260-267-120-236-537-357-125-102-123-152-206-261-2_V.mp3)
  Segment 19: 98784ms ~ 99360ms (file: 637-167-170-176-317-376-731-317-370-231-321-310-316-310-301-312-132-351-652-301 163-613-312-321-201-261-256 261-263-514-132-263-657-267-236-623-623-571-572-572-510 567-365-306-3_W.mp3)
  Segment 20: 100360ms ~ 100936ms (file: 751-715-765-157-175-176-671-671-615-615-672-637-756-657-632-623-625-623-632-675-367-631-267-671-672-637-623-362-236-365-635-651-625-615-672-267-612-672-651-617-625-632-637-627-6_V.mp3)
  Segment 21: 101936ms ~ 111908ms (file: 325-231-236-752-627-263-536-237-514-312-236-310-756-675-163-103-673-136-316-671-632-367-632-236-126-132-106-564-153-102-125-130-102-163-136-103-326-103-216-765-135-517-507-135-5_W.mp3)
  Segment 22: 112908ms ~ 122880ms (file: 675-102-132-310-103-261-762-672-231-263-230-567-327-237-321-236-136-170-237-236-236-263-236-312-735-104-103-310-130-236-130-136-301 267-726-273-312-312-263-326-261-725-312-360-5_W.mp3)
  Segment 23: 123880ms ~ 124456ms (file: 312-673-675-317-756-576-537-103-105-315-362-631-361-637-136-312-361-631-316-617-136-631-361-316-617-167-367-631-361-361-631-613-163-130-576-574-157-675-765-675-763-675-763-675-7_W.mp3)
  Segment 24: 125456ms ~ 126032ms (file: 316-276-602 263-752-213-236-267-276-257-620-263-453-756-267-162-526-135-267-256-672-527-276-261-506-635-253-362-752-231-576-205-271-267-267-570-537-265-502-263-263-657-257-321-7_V.mp3)
  Segment 25: 127032ms ~ 127608ms (file: 736-310-103-301-172-167-310-137-310-103-302-102-137-501-312-312-312-102-321-256-572-762-326-326-321-135-132-301-310 132-301-132-312 312-124-216-123 621-126-627-136-327-312-213-3_V.mp3)
  Segment 26: 128608ms ~ 129184ms (file: 326-760-762-671-157-125-572-326-752-537-721-327-357-506-317-362-532-357-531-327-453-752-123-267-672-267-236-527-627-725-231-540-652-256-576-570-263-126-261-521-312-267-203 275-1_V.mp3)
  Segment 27: 130184ms ~ 130760ms (file: 175-175-170-170-170-176-137-170-107-170-173-107-170-715 176-160-710-715-175-170-751-710-710-715-716-716-175-170-715-170-176-170-107-170-176-170-107-170-715 571-710-170-175-175-7_V.mp3)
  Segment 28: 131760ms ~ 132336ms (file: 157-157-157-157-517-571-517-571-517-576-571-516-517-175-157-351-157-130-103-157-157-157-157-157-156-516-156-517-516-571-576-751-517-157-137-175-715-175-135-157-135-175-315-130-1_V.mp3)
  Segment 29: 133336ms ~ 133912ms (file: 273-261-263-572-132-503-132-236-752 547-576-567-567-576-657-756-576-574-574-576-574-574-574_W.mp3)
  Segment 30: 134912ms ~ 135488ms (file: 253-725-217-273-726-267-206-173-267-267-237-130-145 570-751-137 316-137-137-132-137-130-352-675-371-617-163-576-257-256-576-526-752-276-123-251-267-527-253-235-762-672-752-625-7_V.mp3)
  Segment 31: 136488ms ~ 146388ms (file: 102-762-637-301-154-103-134-132-312-756-705-134-127-301-301-201-702-762-716-321-312-763-321-754-137-315-123-705-103-576-267-206-263-267-206-236-213-203-751-175-203-203-301-132-7_V.mp3)
  Segment 32: 147388ms ~ 147964ms (file: 735 751-751-756-574-571-157-510-705-751-527-570-570-570-571-517 752-572-572-756-752-752-753-753-753-572-752-752-753-753-573-752-753 752-572-572-572-573-573-752-571-571-751-571-5_W.mp3)
  Segment 33: 148964ms ~ 156704ms (file: 132-103-103-176-103-273-267-762-672-607-302-132-723-765-127-574-267-576-127-172-302-327-132-756-760-273-301-326-102-754-103-213-756-726-175-267-205-216-567-102-123-132-752-123-2_V.mp3)
  Segment 34: 157704ms ~ 158280ms (file: 256-125-156-167-127-167-573-507-507-367-275-235-153-203-326-362-301-216-312 504 756-327 765 320-320-256-723-756-567-567-527-567-572-756-576-572-572-576-756-756-572-576-532-567-5_V.mp3)
  Segment 35: 159280ms ~ 159856ms (file: 517-517-517-517-574-517-517-517 107-130-137-517-157-517-571-571-571-157-571-517-576-517-571-517-517-571-517 173-107-130-157-157-517-517-571-571-517-517-517-571-157-517-517-517-5_V.mp3)
  Segment 36: 160856ms ~ 172196ms (file: 263-206-260-102-132-102-102-741-176-261-231-623-103-316-321-326-623-263-132-132-754-206-230-231-310-267-236-321-312-320-263-103-102-120-203-312-130-103-126-130-130-731-130-217-2_V.mp3)
  Segment 37: 173196ms ~ 182592ms (file: 301-130-726-765-537-130-132-362-231-327-321-103-736-236-513-357-762-762-326-312-103-261-231-123-213-132-213-327-231-276-216-213-312-130-301-176-371-237-762-310-362-103-132-573-2_W.mp3)
  Segment 38: 183592ms ~ 184168ms (file: 527-362-236-650-527-726-275-253-753-261-267-127-527-672-621-762-627-765-572-276-326-312-567-706-236-675-671-316-301_V.mp3)
  Segment 39: 185168ms ~ 185744ms (file: 175-157-157-157-715-753-175-157-756-563-753-517-576-571-576-571-752-576-756-576-576-571-517-517-571-152-135-517-507-135-765-673-765_W.mp3)
  Segment 40: 186744ms ~ 194700ms (file: 213-123-143-103-627-301-103-136-753-753-732-231-231-236-213-756-570-206-301-130-320-325-265-203-320-523-106-750-756-750-205-123-213-213-103-165-153-754-570-753-130-312-137-172-7_W.mp3)
  Segment 41: 195700ms ~ 196276ms (file: 175-135-716-176-761-617-715-175-176-176-176-175-715-176-175-176-176-175-167-176-176-761-761 130-135-153-175-637-716-756-167-716-571-756-715-176-157-176-175-167-175-751-176-671-1_W.mp3)
  Segment 42: 197276ms ~ 197852ms (file: 276-321-752-625-267-702-207-567-602-231-265-752-162-765-625-326-316-257-720-137-176-527-263-136-267-321-216-627-621-263-213-657-572-265-316-706-763-527-352-273-576-572-127-725-2_V.mp3)
  Segment 43: 198852ms ~ 199428ms (file: 135 172-570-753-570-573-537-570-576_W.mp3)
  Segment 44: 200428ms ~ 209212ms (file: 271-132-132-623-236-103-627-765-326-236-231-623-326-236-526-236-130-735-260-620-260-137-231-321-263-756-123-130-132-102-123-321-237-632-236-317-361-136-106-132-102-162-102-236-7_W.mp3)
  Segment 45: 210212ms ~ 210788ms (file: 627-657-617-627-632-612-612-612-671-612-621-621_W.mp3)
  Segment 46: 211788ms ~ 212364ms (file: 651-267-135-132-327-357-206-527-326-527-175-237-213-572-267-762-327-321-213-163-267-263-231-503-362-236-632-235-632-132-623-132-326-601-362-362-135-157-123-312-327-765-317-137-6_W.mp3)
  Segment 47: 213364ms ~ 220420ms (file: 236-326-362-632-537-306-326-326-623-263-320-327-236-236-236-326-623-236-263-326-326-326-352-537-326-623-326-361-130-103-372-132-362-267-572-130-107-263-231-236-162-165-210-231-3_W.mp3)
  Segment 48: 221420ms ~ 221996ms (file: 216-265-265-571-135-253-276-152-256-527-175-751_V.mp3)
  Segment 49: 222996ms ~ 230664ms (file: 216-275-725-752-761-675-765-715-201-201-532-130-201-657-106-261-576-752-206-106-132-207-102-572-236-205-567-526-206-206-201-126-107-207-217-571-206-203-376-260-762-270-102-201-2_W.mp3)
  Segment 50: 231664ms ~ 232240ms (file: 756-756-765-756-765-765-756-756-756-576-576-756-132-130-130-765-761-753-751-756-765-756-753 573-571-571-567-753-317-367-317-315-617-312-132-312-627-312-312-231-275-372-327-235-7_W.mp3)
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
> **Time**: 2026-04-06 07:32:55
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "보장 이사회 긴급 회의 소집을 요청했습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "보장", "start_ms": 10, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이사회", "start_ms": 160, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "긴급", "start_ms": 360, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "회의", "start_ms": 460, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "소집을", "start_ms": 510, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "요청했습니다", "start_ms": 550, "dur_ms": 26, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "인도네시아 국방부는 평화 유지군의 안전이 최우선 과제여야 한다며 국제 인도법 준수를 촉구했습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "인도네시아", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "국방부는", "start_ms": 170, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "평화", "start_ms": 270, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "유지군의", "start_ms": 350, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "안전이", "start_ms": 450, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "최우선", "start_ms": 510, "dur_ms": 66, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "UN 사무총장 역시 최근 평화 유지군의 안전과 보안을 위협한 여러 사건들에 대해 깊은 우려를 표명하며 이스라엘 당국에 사고 재발 방지를 위한 대책 마련을 강력히 요구하고 있는 상황입니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "UN", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "사무총장", "start_ms": 240, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "역시", "start_ms": 640, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "I would be behind the computer I'm surprised I don't have a bad back and I can still see you know because I put in the hours behind the computer I had burnout",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "would", "start_ms": 130, "dur_ms": 200, "phase_lr_deg": 0.0}
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
"transcript": "I love my life I love what I do and the energy comes from",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 20, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "외도 좀 더 좋은 결과를 냈는데 계좌를 한번 보여드리면 3월에는 10억 정도 그리고 4월 첫째 주에 지금",
"speaker": "speaker_3",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "외도", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "앉아있다 야 이게 웬 겁니까 이게",
"speaker": "speaker_4",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "앉아있다", "start_ms": 30, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "And I'm happy I love I love this life I'm very happy yeah",
"speaker": "speaker_5",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "And", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "everything is designed to make our lives better not compete with our lives it should work along",
"speaker": "speaker_5",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "everything", "start_ms": 10, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "아 이거 뭐야 다 4,000원이네 어 엄청 잘해 주시네요 시민들 우리 지금 펭귄 마을 왔는데 가리나 자매가 많이",
"speaker": "speaker_6",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "아", "start_ms": 20, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "강하게 움직이는 주도주들 위주로만 공략했습니다 참고로 제가 지금 매매하고 있는 계좌는 예수금을 3억에서 5억 정도",
"speaker": "speaker_7",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "강하게", "start_ms": 30, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "아니 그런데 이게 다 뭐죠 이건 유명 애니메이션의 로봇 같은데",
"speaker": "speaker_8",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "아니", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "잠수함의",
"speaker": "speaker_8",
"confidence": 0.91,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "잠수함의", "start_ms": 40, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "I want to see you there with the 미나리",
"speaker": "speaker_9",
"confidence": 0.88,
"language": "mixed",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 10, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "이 진짜 인기 아 홍 홍기 이거 뭐지 환기 환기 환기 어 감사합니다 감사합니다 아 그러시구나 바로",
"speaker": "speaker_10",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 60, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "아 감사합니다 구독자님",
"speaker": "speaker_10",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "아", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "structure build out in human history every technology revolution has made us more productive however these are going to be things that are simply impossible to do before now these",
"speaker": "speaker_11",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "structure", "start_ms": 40, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "I enjoyed more okay this one a little bit less but it's still delicious but then again you know having the sweet out of here was better eating this to take the sweetness out of my mouth",
"speaker": "speaker_12",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 20, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "plays off each other 비 오는 날은 알밥 알밥 부추",
"speaker": "speaker_12",
"confidence": 0.93,
"language": "mixed",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "plays", "start_ms": 10, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "능력으로 바꾸는 단계에 들어섰다는 점입니다 빠른 생산과 현실적인 가격 그리고 꾸준한 기술 축적과 수출 전략까지 맞물리면 한국은 앞으로 더 강한 존재감을 보일 가능성",
"speaker": "speaker_13",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "능력으로", "start_ms": 50, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "네 그리고 제가 좋아하는 요 목공은 여기서 같이 작품도 전시해서 이렇게 보고 그래서 요 세 개를 하나의 패키지로 해 가지고 할 수 있도록 그렇게 구상하고",
"speaker": "speaker_14",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "저분 남편이 만든 거라는데 나무 조각",
"speaker": "speaker_15",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "저분", "start_ms": 40, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "트럼프는 주한미군 규모를 늘려 방위비 분담금 증액을 말해왔습니다 we give you free military essentially very low",
"speaker": "speaker_16",
"confidence": 0.92,
"language": "mixed",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "트럼프는", "start_ms": 10, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "트럼프가 이번 사안을 관세 후속 협상이나 핵 추진 잠수함 도입과 연관",
"speaker": "speaker_16",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "트럼프가", "start_ms": 20, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "백만 원으로 시작을 하셨다고요 네 저희가 십 년을 계획해서 이 집을 지으려고 시작을 한 겁니다",
"speaker": "speaker_17",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "백만", "start_ms": 40, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "장고 끝에 나가려고 하는 이 전략이 대단히 중요하다고 했죠 그리고 AI 데이터 팩토리도 현대차가 중요한 부분을 생산하려고 하는 거고 자 월드 모델 피지컬 AI 기술은 현대차가 가지고 있지 않죠 보스턴 다이",
"speaker": "speaker_18",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "장고", "start_ms": 30, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "왔을 때 원 투 쓰리 쓰리에다가 어서 먹어",
"speaker": "speaker_19",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "왔을", "start_ms": 60, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "뭐 먹을래 먹어 봐 맛있을까 좀 있음 밥 먹어",
"speaker": "speaker_19",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "뭐", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "네 그래서 군사비 증액뿐만 아니라 전시 작전권 환수를 통해서 미국의 부담을 줄이고 여 최소한 한반도",
"speaker": "speaker_20",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 32,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 33,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 34,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 35,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 36,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 37,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 38,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 39,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 40,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 41,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 42,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 43,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 44,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 45,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 46,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 47,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 48,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 49,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 50,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 07:32:55*
