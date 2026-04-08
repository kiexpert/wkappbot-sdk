# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=39080  time=07:37:06  uptime=14575s  mem=434MB  system_processes=414"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_073653.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 213-143 241-265-276-752-157-372-764-713-351-216-631-367-257-216-241 374-236-617-365-153-257-342-321-321-325-365-312-315-357-375-761-372-632-275-263-215 374-574 137-123-173-312-6_V.mp3)
  Segment 2: 1576ms ~ 8200ms (file: 502-570-631-206-361-753-362-361-316-236-316-230-312-310-231-310-263-271-326-132-316-173-276-103-260-310-315-276-725-201-762-132-301-541-206-201-130-103-260-206-672-207-206-571-2_W.mp3)
  Segment 3: 9200ms ~ 14960ms (file: 103-130-157-175-123-107-125-325-236-726-267-276-105-157-150-105-103-157-103-130-130-106-517-517-310-105-137-305-175-361-123-102-103-267-230-320-236-720-705-236-321-236-261-320-2_W.mp3)
  Segment 4: 15960ms ~ 21324ms (file: 276-206-675-103-637-376-756-725-137-167-175-507-756-756-671-167-174-716-107-172-173-572-375-315-206-705-756-725-726-301-327-137-570-307-523-163-367-136-103-326-267-127-507-207-7_W.mp3)
  Segment 5: 22324ms ~ 30496ms (file: 765-267-167-301-137-267-201-123-162-126-126-132-670-216-137-130-762-720-127-576-156-265-102-137-206_V.mp3)
  Segment 6: 31496ms ~ 32072ms (file: 751-756-675-725-712-261-207-173-672-627-265-276-576-265-713-576-216-213-576-725-637-136-361-574-576-504-567-576-756-756-765-753-736-173-675-617-763_V.mp3)
  Segment 7: 33072ms ~ 33648ms (file: 135-751-316-317-136-317-317-136-731-710-716 763-760-765-753-175-173-375-376-507-675-167-173-761-761-762-761-765-671-617-321-765-172-256-236 612-207-273-271-276-756-576-275-123-3_W.mp3)
  Segment 8: 34648ms ~ 39796ms (file: 230-263-162-206-216-230-123-132-137-173-176-213-231-753-203-203-231-231-321-132-201-203-120-206-327-372-361-163-361-762-256-263-612-231-201-572-136-507-632-356-326-316-752-371-5_W.mp3)
  Segment 9: 40796ms ~ 41372ms (file: 132-215-312-315-752-237-513-632-231-267-576-576-756-276-576-726-576-537-750-571-751-702-520-567-725-657-572-527-317-720-236-517-752 576 631-702-326-376-236-375-576 625-572-570-6_V.mp3)
  Segment 10: 42372ms ~ 42948ms (file: 765-675-635-765-756-576-675-765-756-756-675-375-765-675-756-657-657-756-756-756-567-756-753-576-756-657-576-756-576-547-567-756-753-754-526-365-350-163-261-361-175-235-546-546-5_W.mp3)
  Segment 11: 43948ms ~ 51328ms (file: 106-237-230-126-127-723-231-236-102-752-762-237-103-612-267-102-273-627-237-320-236-130-136-726-236-327-236-326-231-257-103-137-507-573-160-236-273-267-231-612-617-167-136-103-7_W.mp3)
  Segment 12: 52328ms ~ 52904ms (file: 651-576-257-652-571-627-715-136-567-265-157-506-625-512-325-236-752-265-750-527-502-265-751-716-750-576-360-312-763-526-623-567-736-312 125-705-326-562-576-321-572-276 163-756-5_V.mp3)
  Segment 13: 53904ms ~ 60384ms (file: 567-567-376-576-172-257-750-576-560-367-675-760-153-762-765-675-105-321-763-760-312-756-172-267-137-176-215-321-235-235-215-512-527-537-123-123-126-213-312-123-237-127-217-237-2_W.mp3)
  Segment 14: 61384ms ~ 61960ms (file: 576-513-571-512-172-267-216-126-326-326-367-763-761-671-637-673-675-176-671-173-761-175-107-670-621-163-671-617-376-615-610-675-756-627-176-271-632-163-316-123-216-135-123-126-6_W.mp3)
  Segment 15: 62960ms ~ 63536ms (file: 317-317-317-657-673-657-756-673-672_W.mp3)
  Segment 16: 64536ms ~ 65112ms (file: 570-576-510-527-572-527-652-517-157 571-571-513-517-517-574-576-576-571-576-571-751-751-576-517 567-567-576-315-715-571-570-507-703-173-716-310-175-710-130-136-327-372-710-137-3_W.mp3)
  Segment 17: 66112ms ~ 74536ms (file: 213-607-231-263-260-261-103-102-261-120-620-310-123-130-123-267-260-132-602-216-207-120-570-675-267-621-320-760-672-670-126-236-206-103-267-260-260-206-127-302-134-103-206-102-2_W.mp3)
  Segment 18: 75536ms ~ 81836ms (file: 163-256-263-123-103-321-203-172-532-312-312-375-236-351-312-531-317-310-276-317-327-312-713-236-316-276-765-576-502-310-514-517-305-305-165-576-513-265-132-206-260-265-206-325-7_W.mp3)
  Segment 19: 82836ms ~ 83412ms (file: 621-362-312-310-321-310-312-130 621 316-312-316-312-136-316-312-316-312-321-132-316-263-631-163-563-653-653-651-613-126-103-132-130-123-132-321-102-126-312-653_W.mp3)
  Segment 20: 84412ms ~ 84988ms (file: 215-561-726-236-527-265-530-536-125-263-273-256-576-321-673-263-523-532-206-261-137-315-132-756-713-632-362-276-263-523-623-257-762-672-567-256-672-765-123-257-235-267-657-571-7_V.mp3)
  Segment 21: 85988ms ~ 98948ms (file: 307-763-170-130-750-735-732-756-213-273-315-731-312-310-572-765-735-710-715-157-750-752-371-107-715-107-710-751-130-235-752-752-751-175-130-712-275-170-753-570-130-751-573-753-7_W_stt_the.mp3)
  Segment 22: 99948ms ~ 106356ms (file: 674-761-671-137-672-675-237-312-321-176-617-176-276-137-263-236-751-231-312-103-756-750-120-712-127-756-127-136-310-537-176-761-671-206-203-627-672-750-207-230-107-371-163-576-1_W.mp3)
  Segment 23: 107356ms ~ 114412ms (file: 172-576-176-517-163-612-163-156-132-316-315-106-210-103-103-267-301-165-102-765-763-756-167-176-716-103-103-765-675-756-760-765-760-316-167-362-130-576-276-567-163-136-173-265-7_V.mp3)
  Segment 24: 115412ms ~ 115988ms (file: 751-570-576-756-756-765-127-263-375-752-352-215-357-231-756-253-162-627-725-405-275-317-670-672-276-326-263-216-567-607-276-756-765-765-627-216-765-235-657-312-574-576-574-576-3_V.mp3)
  Segment 25: 116988ms ~ 121308ms (file: 267-267-627-672-672-325-321-103-106-617-361-167-160-617-601-176-137-351-235-726-627-672-672-327-326-326-162-362-356-160-756-372-325-327-672-732-312-673-267-267-163-372-165-132-3_W.mp3)
  Segment 26: 122308ms ~ 128068ms (file: 670-316-136 312-617-762-153-312-261 756-627-261-206-206-572-103-263-207-103-217-206-102-326-620-123-201-276-760-172-230-201-267-203-302-720-627-206-236-120-631-216-210-275-231-1_W.mp3)
  Segment 27: 129068ms ~ 145844ms (file: 213-270-321-756-267-526-527-265-236-270-206-321-123-320-263-261-201-213 230-320-230-762-123-103-413-103-325-217-312-530-751-736-715-310-231-203-235-230-103-127-721-312-127-103-7_W.mp3)
  Segment 28: 146844ms ~ 147420ms (file: 751-157-103-123-170-170-157-170-705-752-756-570-175-105-573-572-175-175-175-157-137 571-571-571-571-572-357-105-175-735-173-157-715-715-750-713-570-107-150-175-751-375 572-571-5_V.mp3)
  Segment 29: 148420ms ~ 156700ms (file: 263-752-267-103-716-267-276-123-135-136-106-621-613-106-263-261-236-260-260-362-267-752-765-263-326-312-130-316-136-163-213-267-267-725-312-320-130-756-130-263-263-652-765-763-1_W.mp3)
  Segment 30: 157700ms ~ 158276ms (file: 165-136-165-162-137-132-163-756-765-657-735-752-765-675-765-756-765 175-173-137-137-175-173-172-175-137-175-175-173-175-173-137-752-756-752-576-752-756-752-756 175-517-571-751-7_W.mp3)
  Segment 31: 159276ms ~ 159852ms (file: 567-263-627-572-321-527-236-326-627-652-753-257-362-356-137-627 130-316-351-356-315-573-351-352-356-352-367-326-623_W.mp3)
  Segment 32: 160852ms ~ 161428ms (file: 167-137-137-317-301-316-136-136-136-130-316-130-130-136-170-351-137-351-317-137-763-317-173-137-173-710-713-107-137-173 175-157-153-753-157-752-715-715-751-715-137-137-517-571-1_W.mp3)
  Segment 33: 162428ms ~ 168944ms (file: 351-137-130-103-103-107-307-267-270-675-132-103-620-260-263-631-106-613-615-321-165-160-312-136-106-107-601-163-612-301-654-316-132-725-321-132-130-657-132-132-320-326-213-312-1_W.mp3)
  Segment 34: 169944ms ~ 176316ms (file: 260-761-672-756-203-273-725-267-102-267-126-627-267-176-761-167-106-130-132-267-263-236-132-731-276-276-267-216-132-621-126-230-320-270-257-276-120-123-173-725-276-275-265-276-3_W.mp3)
  Segment 35: 177316ms ~ 177892ms (file: 760-301-756-137-715-163-617-753-173-137-317-167-136-612-136-163-123-365-613-163-316-136-613-163-136-136-163-136-163-163-136-160-163-136-136-765-765-735-705-730-736-761-276-761-1_W.mp3)
  Segment 36: 178892ms ~ 184004ms (file: 231-236-236-236-132-135-132-236-273-276-230-230-123-231-231-230-321-270-132-310-273-210-751-762-267-207-321-231-270-201-231-260-130-236-216-102-103-715-371-310-236-312-321-326-3_W.mp3)
  Segment 37: 185004ms ~ 189684ms (file: 267-265-275-627-236-762-765-276-267-267-301-573-572-571-132-103-263-126-137-237-120-132-735-236-261-237-137-371-720-162-137-107-762-371-572-725-206-726-260-216-623-627-276-312-2_W.mp3)
  Segment 38: 190684ms ~ 191260ms (file: 351-157-567-573-157-657-357-157-567-756-561_W.mp3)
  Segment 39: 192260ms ~ 200576ms (file: 321-371-172-637-357-523-316-325-235-236-231-236-273-237-735-236-312-267-126-102-237-312-263-213-201-103-137-123-132-237-203-326-321-134-176-120-123-510-152-501-310-301-537-327-2_W.mp3)
  Segment 40: 201576ms ~ 202152ms (file: 651-576-712-326-372-365-215-236-326-576-523-351-132-103-125-726-123-257-132-652-652-326-653-206-326-267-236-325-263-567-527-265-352-705 175-216-312-563-321-135-106-162-163-752-2_V.mp3)
  Segment 41: 203152ms ~ 203728ms (file: 103-156-765-167-136-657-657-615-365-165-361-613-135-156-361_W.mp3)
  Segment 42: 204728ms ~ 212756ms (file: 231-316-612-102-102-321-321-230-276-130-127-106-517-561-120-317-137-107-320-306-761-132-126-376-371-213-572-572-350-102-130-316-103-153-357-321-102-312-102-263-261-126-132-275-1_W.mp3)
  Segment 43: 213756ms ~ 214332ms (file: 316-361-361-135-135-301-321-273-527-547-532-315-135-130-130-137-132-120-125-361-236-132-716-721-273-123-361_W.mp3)
  Segment 44: 215332ms ~ 215908ms (file: 705 356-603-137-137-107-712-701-175-750-726-607-310-607-316-163-675-617-630-175-170-106_W.mp3)
  Segment 45: 216908ms ~ 223280ms (file: 270-170-173-321-317-130-176-107-130-137-201-137-321-257-536-167-203-320-375-316-362-307-302-361-672-213-301-321-672-713-301-312-712-361-130-137-137-123-123-120-120-165-316-120-1_V_stt_the_film.mp3)
  Segment 46: 224280ms ~ 224856ms (file: 576-567-165-156-751-567-576-527-567-576-152-156-102-175-135-156-651-165-576-156-256-651-567-576-526-657-216-156-576-567-576-657-756-657-576-567-576-615-260-756-567-156-157-167-1_W.mp3)
  Segment 47: 225856ms ~ 226432ms (file: 631-261-136-715-753-163-103-136-173-752-721-752 310-130-130 625_W.mp3)
  Segment 48: 227432ms ~ 228008ms (file: 725-312-357-261-127-261-231-217-217-756-762-156-135-372-317-302-301-321-725-574-371-327-275-547-567 263-237-267-267-756-723-216-203-137-137-516-326-267-276-361-526-732-321-237-5_V.mp3)
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
> **Time**: 2026-04-06 07:38:02
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "손님의 90%가 프랑스인이었다고 하는데요 매운맛을 줄인 비빔밥, 사계절을 담은 막걸리를 내놓는 등 고급스러움을 강조한 서비스로 현지인들의 발길을",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "손님의", "start_ms": 10, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "90%가", "start_ms": 160, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "프랑스인이었다고", "start_ms": 360, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "하는데요", "start_ms": 510, "dur_ms": 66, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "the opportunities to reinvent how you think about design and create",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "the", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "opportunities", "start_ms": 140, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "절반은 시가보다 좀 추가로 상승했을 때 18% 정도 올랐을 때 전부 다 수익 실현했습니다",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "절반은", "start_ms": 20, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "경쟁은 말이 아니라 숫자와 계약 그리고 실제 납품으로 증명되기 때문입니다",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "경쟁은", "start_ms": 30, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "한복 입기 하는구나 맞아요 한복 입고 전 세계를 다니고 있는데 어 혹시 외국에서 오셨어요? 그렇지 미국 오신 지",
"speaker": "speaker_5",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "한복", "start_ms": 50, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "주도주였기 때문에 저는 매매를 안 했고 찐 대장으로 볼 수 있는 대한광통신을 매매했습니다 디플러스 매매는 이런 강한 주도 테마 대장주가",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "주도주였기", "start_ms": 10, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "할 수 있겠다 라는 용기가 이제 생겨나지",
"speaker": "speaker_6",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "할", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "무려 6미터에 달하는",
"speaker": "speaker_7",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "무려", "start_ms": 40, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "6미터에", "start_ms": 340, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "이 외교 갈등을 촉발했다가 유럽과 아프리카 각국에서 터져 나온 사상 초유의 상황으로 인해 현재 큰 탄식을 터뜨리고 있다며 이를 대대적으로 보도했습니다 대만은 자국을 중국의 일부로 표기하는",
"speaker": "speaker_8",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "대통령은 호르무즈 해협을 언급하다 도련 한국을 겨냥했습니다 let the european countries know let them",
"speaker": "speaker_9",
"confidence": 0.93,
"language": "mixed",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "대통령은", "start_ms": 40, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "그러면 철학적인 사유 그 이전에 많은 위대한 위인들이 했던 고민들 어떤 생각, 경험 그런 것들의 데이터화 이런 것들이 정말 중요하지 않을까 그 기술이 너무 좋아져도 내가 사용할 수 있는",
"speaker": "speaker_10",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "그러면", "start_ms": 50, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "정도로 매매하면서 수익금을 좀 며칠마다 지속적으로 인출하면서 매매했고 제가 입금 출금 금액이 좀 많은 이유는",
"speaker": "speaker_3",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "정도로", "start_ms": 20, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "대출을 해 주는 게 아니라는 걸 그때 처음 알았어요 직장을 가지고 있는 분들은 뭐 좀 많이 해 주더라고요 근데 저희처럼 자영업",
"speaker": "speaker_11",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "대출을", "start_ms": 40, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "이 젬마 모델은 언어 쪽에서 굉장히 잘 지원돼서 특히나 140개 이상의 언어를 지원을 할 수 있어서 주목을 받고",
"speaker": "speaker_12",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "지금 대만 정부 내부 분위기는 그 어느 때보다 무겁게 가라앉아 있습니다 이는 대만 정부가 최근 한국을 상대로 무모한 갈등을 촉발",
"speaker": "speaker_8",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "지금", "start_ms": 50, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "기술을 쓰는 사람에게 달려있다는 뜻이다 AI 시대는 정말 인간이 오히려 어떤 존재인가를 드러내는 것 같아요 여러 이유로 우리의 문제가 뭐였고 결국 그래서 인간은 무슨 존재인가 저는 나의 생각의 주체성이 가장 중요하다고 생각하거든요 좀 자신의 어떤 관점을 드러내는 게 훨씬 더 많아져야",
"speaker": "speaker_10",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "기술을", "start_ms": 10, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "못할 것이라는 전망들이 팽배한 상황에서 이 괴물 같은 우리 KF-21 개발진들은 그랬거나 말거나 원래 예정되어 있던 그 일정에 정확히 맞추어 그리고 초도 비행",
"speaker": "speaker_13",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "못할", "start_ms": 20, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "없어 또니 또니도 엄마 보면은 휠라 깨 줄 거야? 안 해줄 거야? 표정 봐",
"speaker": "speaker_14",
"confidence": 0.91,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "없어", "start_ms": 40, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "업적을 달성해 버렸는데요 이번 경기에서 전반전에만 4개의 어시스트를 기록하며 MLS에서 한 경기 최다 어시스트 기록 2위",
"speaker": "speaker_15",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "업적을", "start_ms": 30, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "출산을 했기 때문에 애를 네 명이나 가진 거지 만약에 제가 이 나이에 결혼하고 첫째 낳았는데 이런 상황이 펼쳐졌다? 둘째는 솔직히",
"speaker": "speaker_16",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "출산을", "start_ms": 20, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "이 펼쳐진 이곳에서 글로벌 기업들은 앞다퉈 새 시대의 개막을 알렸다",
"speaker": "speaker_17",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "맛있어? 왜 이렇게 귀엽게 먹냐 또니 안쪽 볼 좀 봐 앙 왜 안쪽 볼 뽈록해졌어 괜찮아 근데 너는 왜 이렇게 몰래 하는 걸 좋아해",
"speaker": "speaker_14",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "맛있어?", "start_ms": 50, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "배달 두 개 시켜 먹어가지고 나눠 먹고 있고 아니 김치는? 맞아 안 맞아 소피아 맞아 안 맞아 또니 맞아? 아시겠",
"speaker": "speaker_14",
"confidence": 0.91,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "배달", "start_ms": 30, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "그 어느 때보다 드높다 세계 각지에서 모여든",
"speaker": "speaker_17",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "그", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 07:38:02*
