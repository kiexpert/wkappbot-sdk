# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=31656  time=08:50:19  uptime=18228s  mem=250MB  system_processes=408"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_084829.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 672 762-672-627-276-675-756-317-573-574-576 576-567-536-357-567 713-713-310-301-763-605-607-607 320-132-123-312-132-721-327-276-273-213-123-765-736-573-567-567-672-236-326-256-7_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 107-107-715-170-701-710-713-715-716-175-715-710 167-175-107-175-157-107-710-170-175-715-715-715-715-175-175-715-175-715-175-175 107-670-751-176-715-107-706-175-716-173-170-175-1_V.mp3)
  Segment 3: 3152ms ~ 10676ms (file: 230-756-120-260-126-260-203-123-130-132-627-627-267-162-213-107-132-760-172-120-301-103-261-267-705-756-756-627-276-102-127-103-762-756-675-216-126-120-210-210-267-672-160-123-6_W.mp3)
  Segment 4: 11676ms ~ 22836ms (file: 102-130-210-210-107-160-260-216-120-106-326-231-231-213-230-574-571-320-106-326-362-675-136-175-517-325-125-260-310-103-213-102-102-203-132-102-132-143-104-210-574-571-625-261-6_W.mp3)
  Segment 5: 23836ms ~ 24412ms (file: 316-312-321-321-231-321-612 516-657-756-157-517-567-371 715-731-352-103-765-351-237-102-701-701-173-130-301-312-321-736-632-325-137-130-106-136-103-136-162-105-510-516-516-516 2_V.mp3)
  Segment 6: 25412ms ~ 25988ms (file: 231-236-576-756-573-372-572-275-216-123-615-617-376-316-613-653-625-327-632-752-237-671-376-765-367-312-367-617-132-271-750-317-526-325-317-321-572-162-736-762-536-526-325-750 5_V.mp3)
  Segment 7: 26988ms ~ 27564ms (file: 715-573-756-573-571 517-517-517-571-517-571-517-571-571 571-571-571-571-576-517-571-517-751-573-715-175-753-756-751-751-571-571-571-571-570-751-751 574-574-571-574-574-574-574-5_W.mp3)
  Segment 8: 28564ms ~ 29140ms (file: 574-576-574-574-574-571-574 573-574-574-574-574-574 574-574 574-574-574-573 576-574-574-571-574-574-571-571-157-510-157-517-517-573-317-503-750-715-150-571-537-571-751 157-573-1_W.mp3)
  Segment 9: 30140ms ~ 37556ms (file: 632-675-103-573-765-276-705-706-267-257-206-752-103-267-126-261-162-130-216-216-132-157-175-735-627-750-170-570-105-210-567-276-756-765-160-136-762-267-176-627-576-671-206-672-7_W.mp3)
  Segment 10: 38556ms ~ 39132ms (file: 570-526-312-236-275-267-675-612-627-263-276-652-263-267-263-236-231-276-216-527-750-763-352-263-726-671-612-632-627-216-267-705-167-527-507-126-325-127 560-502-760-506-756-351-5_V.mp3)
  Segment 11: 40132ms ~ 40708ms (file: 137-135-175-754-751-576-571-574-751-751-175-173-175-715-175-137-175 571-574-756-175-715-157-751-175-157-175-175-175-175-137 135-175-175-716-137-715-175-175-175-157-157-175-157-1_W.mp3)
  Segment 12: 41708ms ~ 42284ms (file: 317-327-572-257-572-267-752-267-752-276-756-765-576-163-517-532-217-576-231-627-573-756-172-276-175-375-136-612-123-160-723-276-627-376-745-257-405-231-752-130-271-672-230-632-5_V.mp3)
  Segment 13: 43284ms ~ 43860ms (file: 573 571-571-571-751-751-570-751-751-751-751-571-751-751-157-715-715-571-571-517-570-571-571-571-571 574-571-574-576-570-157-750-751-175-750-753-715-570-517-517-501-571-750-571-7_V.mp3)
  Segment 14: 44860ms ~ 45436ms (file: 762-623-312-672-672-675-765-756-652-675-576-153-673-657-517-537-375-671-375-103-276-362-365-673-716-573-632-236-672-672-267-576-576-527-361-162-165-657-316-567-563-671-576-567-5_W.mp3)
  Segment 15: 46436ms ~ 56120ms (file: 216-310-753-750-302-735-260-572-203-261-725-263-231-265-526-165-135-263-231-751-576-137-267-321-572-752-230-574-572-301-132-107-165-175-320-574-526-271-103-102-574-573-716-275-7_W.mp3)
  Segment 16: 57120ms ~ 57696ms (file: 735-731-376-735-715-765 570-573-753-751-375-710-103-136-170-173-716-730-710-702-103-760-765-137-306-612-710-761-761-713-731-672-671-167-756-321-651-265-162-576-176-570-312-317-7_V.mp3)
  Segment 17: 58696ms ~ 59272ms (file: 173-170-175-175-715-715-715-715-715-715-751-715-715-715 175-175-157-715-170-710-750-710-105-175-710-701-715-710-175-175-175-715-715-176-175-716-715-751-715-175-756-576-756-576-7_V.mp3)
  Segment 18: 60272ms ~ 60848ms (file: 267-502-672-267-123-256-576-327-621-627-263-236-256-275-261-237-756-752-123-527-325-362-623-253-275-572-256-567-162-760-231-672-672-572-675-756-317-562-253-257-576-527-376-235-2_V.mp3)
  Segment 19: 61848ms ~ 62424ms (file: 276-570-653-267-263-570-362-275-635-326-321-273-167-670-762-576-275-203-671-315-617-637-317-763-617-167-167-761-671-756-362-631-617-673-761-761-137-675-375-312-765-675-765-673-6_W.mp3)
  Segment 20: 63424ms ~ 72316ms (file: 130-310-120-207-312-257-203-102-230-312-123-102-201-103-206-310-102-103-312-106-756-756-120-210-271-130-301-201-320-230-310-572-302-203-273-201-124-310-206-203-765-765-750-201-3_V.mp3)
  Segment 21: 73316ms ~ 73892ms (file: 531-571-751-751-751-517-751-753-756-571-570-751-751-570-571-576-576-571-571-571-570-570-576-571-573-756 517-153-501-751-571-175-576-751-517-751-175-571-751-175-751-751-715-576-7_V.mp3)
  Segment 22: 74892ms ~ 81048ms (file: 570-574-754-375-327-574-754-230-203-257-572-107-265-253-576-752-321-130-375-375-357-325-302-103-102-106-325-130-305-573-576-756-216-620-260-132-312-327-273-160-130-103-326-276-2_W.mp3)
  Segment 23: 82048ms ~ 82624ms (file: 675-754-175-712-547-751-576-764-571-315-253-725-256-253-157-672-257-567-572-157-453-456-657-756-574-351-567-562-756-567-761-165-153-157-157-613-167-375-352-163-125-315-451-156-1_W.mp3)
  Segment 24: 83624ms ~ 84200ms (file: 756-751-571 574-574-573-571-571-571-571-571-571-571-571-571-576-576-576-576-573 574-571-517-571-571-571-574-517-571-571-571-571-576-573 571-571-571-571-571-571-573-750-735-157-3_W.mp3)
  Segment 25: 85200ms ~ 94776ms (file: 267-217-136-316-752-510-130-531-132-310-320-103-103-301-312-137-716-136-136-617-167-670-627-236-136-132-312-260-316-312-231-256-215-570-206-316-120-326-213-236-203-260-201-627-2_W.mp3)
  Segment 26: 95776ms ~ 100852ms (file: 136-237-263-260-567-267-267-610-307-176-132-531-103-167-756-576-675-562-517-105-673-672-706-105-136-607-523-725-637-705-725-623-301-237-573-675-750-672-670-235-267-602-756-302-7_W.mp3)
  Segment 27: 101852ms ~ 102428ms (file: 105-107-150-510-517 105-157-510-517 513-105-105-513-157-351-105-150-510 517-150-510-517-105-135-103-150-105-153-501-105-150-105 103-315-103-105-150-105-157-135-715-236 513-315-5_V.mp3)
  Segment 28: 103428ms ~ 104004ms (file: 726-762-267-726-517-706-231-567-365-635-657-215-756-675-736-536-756-725-372-326-267-275-367-574-567-315-267-507-276-276-257-570-675-576-567-231-627-673-756-576-265-273-716-372-3_V.mp3)
  Segment 29: 105004ms ~ 105580ms (file: 157-756-561-157-156-165-176-156-615-651-512-165-567-657-165-156-152-156-561-576-652-657-567-152-156-260-625-175-135-156-651-165-657-567-756-657-567-576-562-652-516-576-567-576-5_W.mp3)
  Segment 30: 106580ms ~ 120224ms (file: 574-574-351-351-307-607-120-216-203-132-310-312-530-130-136-361-361-360-310-136-130-137-130-754-753-175-157-175-571-137-715-753-735-735-173-753-735-753-754-315-136 137-132-573-6_W.mp3)
  Segment 31: 121224ms ~ 121800ms (file: 672-672-765-756-675-765-657-725-726-675-675-167-175-573-165-156-567-135-571-516-516-576-567-753-751-756-756-761-716-716-716-712-715-573-756-751-576-576-567-156-561-615-576-657-5_W.mp3)
  Segment 32: 122800ms ~ 123376ms (file: 713-710-571-571-570-753-751-751-571-570-752-750 574-571-570-571-571-571-751-570-571-517-571-751-571-573-157-571-751-571-751-751-753-571-517 574-576-576-576-571-576-571-517-513-5_W.mp3)
  Segment 33: 124376ms ~ 133700ms (file: 326-601-762-576-263-673-573-765-763-765-765-176-216-736-261-123-263-625-210-231-271-267-320-756-276-267-263-752-126-213-627-267-273-572-512-216-130-627-132-210-210-236-267-231-2_W.mp3)
  Segment 34: 134700ms ~ 135276ms (file: 705-175-715-753-571-513-517-135-731-135-751-175-573-573-571 571-571-517-157-571-751-175-750-750-153-710-513-517-735-751-571-573-175-157-750-751-753-751-751-571-751-573-751-573 5_V.mp3)
  Segment 35: 136276ms ~ 142972ms (file: 276-632-276-263-127-271-267-276-267-123-321-162-271-276-327-217-327-236-273-267-276-271-276-723-273-732-132-175-132-167-132-106-617-130-103-301-231-103-301-312-174-103-162-164-2_W.mp3)
  Segment 36: 143972ms ~ 144548ms (file: 627-236-276-756-572-576-571-276-265-253-657-570-756-627-675-502-273-507-325-526-326_V.mp3)
  Segment 37: 145548ms ~ 146124ms (file: 657-651-652-126-153-167-152-165-651-576-567-562-256-216-576-567-657-567-752-657-756-651-657-621-265-567-125-165-167-165-657-576-621-651-163-162-136-162-163-162-615-625-126-216-1_W.mp3)
  Segment 38: 147124ms ~ 147700ms (file: 731-176-137-376-672-176-761-376-176-670-712-176-327-173-637-265-762-721-326-260-213-321-205-263-213-752-103-312-756-236-560-123-502-162-236-261-276-267-321-256-213-572-123-567-2_W.mp3)
  Segment 39: 148700ms ~ 149276ms (file: 567-536-261-361-315-312-623-135-760-613-160-601 726-327-356-123-623-231-675-675-576-326-574-123-352-327-215-217-261-627-607-320-236-326-207-235-563-263-521-560-263-237-751-263-2_V.mp3)
  Segment 40: 150276ms ~ 150852ms (file: 571-751-571-751-751-751 574-574-571-571-571-517-571-571-576-573-573-571 571-571-571-571-571-571-571-574 573-571-571-571-571-517-571-571-571 571-571-571-571-751-517-570-571-751-5_W.mp3)
  Segment 41: 151852ms ~ 152428ms (file: 574-576-574-574-574-574-574-574-571-571-574-571-576-571 576-574-576-574-574-574-574-574-574-574-574-574-574-574-574-574-574-574-576 574-574-574-574-574-574-574-574-574-574-574-5_W.mp3)
  Segment 42: 153428ms ~ 162680ms (file: 230-310-310-136-134-736-573-310-321-132-103-317-316-301-321-765-753-756-305-261-173-216-102-203-572-301-310-735-312-752-752-765-301-230-310-256-201-201-572-754-675-675-203-310-2_W.mp3)
  Segment 43: 163680ms ~ 164256ms (file: 576-567-561-753-753-153-671-562-625-765-671-576 351-547-547-547-504-507-570-514-571-563-175-273-756-527-352-357-527-651-635-251-315-314-175-162-132-123-123-236-321-321-315-317-3_V.mp3)
  Segment 44: 165256ms ~ 165832ms (file: 753-573-751-576-756-357-753-657-675-375-763-673-657-653-675-367_W.mp3)
  Segment 45: 166832ms ~ 167408ms (file: 571-570-574-574-574-574 570-570-574-571-574-571-570-570-574-570-570-574-574-574-574 574-571-574-574-574-571-574-571-571-576-576-571-574-571-571-572-571-571-571-571-574-572 574-5_W.mp3)
  Segment 46: 168408ms ~ 174276ms (file: 263-236-267-623-267-627-673-163-321-627-623-263-106-517-123-130-130-136-731-765-675-765-175-371-265-216-326-312-513-576-231-536-576-310-567-102-132-312-321-312-326-136-267-236-2_W.mp3)
  Segment 47: 175276ms ~ 175852ms (file: 631-752-753-230-132-132-570-312-137-756-721-710-317-132-137-723-271-213-175-537 576-132-341-572-327-203-235-127-213-276-273-267-321-570-247-570-302-750-572 574-765-267-237-570-3_V.mp3)
  Segment 48: 176852ms ~ 177428ms (file: 157-137-103-301-576-675-671-675-762-765-765-761_W.mp3)
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
> **Time**: 2026-04-06 08:52:19
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "사라졌답니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "사라졌답니다", "start_ms": 20, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "인허가 비용 이런 거 기본적으로 500에서 1000만 원 정도 이렇게 나오더라고요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "인허가", "start_ms": 10, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "비용", "start_ms": 140, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 230, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 300, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "기본적으로", "start_ms": 350, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "500에서", "start_ms": 510, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "1000만", "start_ms": 620, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "원", "start_ms": 710, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "정도", "start_ms": 760, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 830, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "나오더라고요", "start_ms": 920, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "그럼 그 돈 필요해서 열심히 벌었어요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 10, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 80, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "돈", "start_ms": 130, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "필요해서", "start_ms": 200, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "열심히", "start_ms": 330, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "벌었어요", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "엄청나게 더 업그레이드된 것은 아닙니다",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "엄청나게", "start_ms": 15, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 180, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "업그레이드된", "start_ms": 250, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "것은", "start_ms": 440, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "아닙니다", "start_ms": 530, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "애초에 프리미어리그에서도 손꼽히는 패서였던 손흥민 선수이자 토트넘 역대 최고의 7번",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "애초에", "start_ms": 10, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "프리미어리그에서도", "start_ms": 140, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "손꼽히는", "start_ms": 500, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "패서였던", "start_ms": 710, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "손흥민", "start_ms": 900, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "선수이자", "start_ms": 1060, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "토트넘", "start_ms": 1250, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "역대", "start_ms": 1400, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "최고의", "start_ms": 1510, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "7번", "start_ms": 1640, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "와 이거 조금만 움직여도 머리 부딪히겠네 와 이게 제 키 아니면은 저 부분만 허용하는 걸로 해서 폭을 좁게 했어요 그래야지 공간이 나오거든요",
"speaker": "speaker_4",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "와", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이거", "start_ms": 160, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "조금만", "start_ms": 290, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "움직여도", "start_ms": 480, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "머리", "start_ms": 740, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "부딪히겠네", "start_ms": 900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "와", "start_ms": 1260, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 1370, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "제", "start_ms": 1500, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "키", "start_ms": 1590, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "아니면은", "start_ms": 1700, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "저", "start_ms": 1930, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "부분만", "start_ms": 2020, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "허용하는", "start_ms": 2210, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "걸로", "start_ms": 2470, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "해서", "start_ms": 2630, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "폭을", "start_ms": 2760, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "좁게", "start_ms": 2910, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "했어요", "start_ms": 3080, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "그래야지", "start_ms": 3290, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "공간이", "start_ms": 3520, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "나오거든요", "start_ms": 3710, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "외국 정부들이 늘어나는 데 반발하고 있다",
"speaker": "speaker_5",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "외국", "start_ms": 20, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "정부들이", "start_ms": 110, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "늘어나는", "start_ms": 270, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "데", "start_ms": 400, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "반발하고", "start_ms": 450, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있다", "start_ms": 610, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "그러나 이러한 대응 조치가 실제로 효과를 낼 수 있을지에 대한 의문도 커지고 있다",
"speaker": "speaker_5",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "그러나", "start_ms": 15, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이러한", "start_ms": 125, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "대응", "start_ms": 255, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "조치가", "start_ms": 345, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "실제로", "start_ms": 455, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "효과를", "start_ms": 585, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "낼", "start_ms": 695, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 745, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "있을지에", "start_ms": 785, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "대한", "start_ms": 935, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "의문도", "start_ms": 1025, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "커지고", "start_ms": 1135, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "있다", "start_ms": 1265, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "최근 몇 주 사이 논란은 더욱 확대됐다",
"speaker": "speaker_5",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "최근", "start_ms": 10, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "몇", "start_ms": 100, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "주", "start_ms": 150, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "사이", "start_ms": 200, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "논란은", "start_ms": 270, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "더욱", "start_ms": 380, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "확대됐다", "start_ms": 470, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "한국과 덴마크 그리고 카메룬 정부가",
"speaker": "speaker_5",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "한국과", "start_ms": 20, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "덴마크", "start_ms": 150, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 260, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "카메룬", "start_ms": 350, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "정부가", "start_ms": 460, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "진짜 달달한 감자라고 생각해 그렇지 달달한 감자야 하나도 안 짜고 단맛도 많이 나고 그렇지",
"speaker": "speaker_6",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "달달한", "start_ms": 150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "감자라고", "start_ms": 310, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "생각해", "start_ms": 500, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "그렇지", "start_ms": 660, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "달달한", "start_ms": 790, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "감자야", "start_ms": 950, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "하나도", "start_ms": 1090, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 1220, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "짜고", "start_ms": 1280, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "단맛도", "start_ms": 1370, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 1520, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "나고", "start_ms": 1630, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "그렇지", "start_ms": 1720, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "때문에 50일 넘게 걸립니다 중동 산의 두 배 이상입니다 더 비싼 운임을 들여 가져와도 우리 산업 구조에 잘 안 맞습니다 석유 화학 업계는 끈적끈적한 중동 산 중질유 위주로 정제 나프타와 벙커C유",
"speaker": "speaker_7",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "때문에", "start_ms": 25, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "50일", "start_ms": 155, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "넘게", "start_ms": 265, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "걸립니다", "start_ms": 355, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "중동", "start_ms": 515, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "산의", "start_ms": 625, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "두", "start_ms": 715, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "배", "start_ms": 765, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "이상입니다", "start_ms": 815, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 975, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "비싼", "start_ms": 1025, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "운임을", "start_ms": 1115, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "들여", "start_ms": 1225, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "가져와도", "start_ms": 1315, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "우리", "start_ms": 1475, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "산업", "start_ms": 1565, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "구조에", "start_ms": 1675, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 1805, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 1855, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "맞습니다", "start_ms": 1905, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "석유", "start_ms": 2065, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "화학", "start_ms": 2155, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "업계는", "start_ms": 2245, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "끈적끈적한", "start_ms": 2375, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "중동", "start_ms": 2635, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "산", "start_ms": 2745, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "중질유", "start_ms": 2795, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "위주로", "start_ms": 2955, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "정제", "start_ms": 3085, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "나프타와", "start_ms": 3195, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "벙커C유", "start_ms": 3405, "dur_ms": 200, "phase_lr_deg": 0.0}
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
"transcript": "So, is minari in there? How does it taste? I think just...",
"speaker": "speaker_8",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "So,", "start_ms": 20, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 130, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "minari", "start_ms": 190, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 400, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "there?", "start_ms": 470, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "How", "start_ms": 600, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "does", "start_ms": 690, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "it", "start_ms": 800, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "taste?", "start_ms": 850, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 1010, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "think", "start_ms": 1060, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "just...", "start_ms": 1170, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "She's so lazy. I think that's what he really wants.",
"speaker": "speaker_9",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "She's", "start_ms": 30, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "so", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "lazy.", "start_ms": 250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 410, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "think", "start_ms": 460, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "that's", "start_ms": 570, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "what", "start_ms": 700, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "he", "start_ms": 790, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "really", "start_ms": 860, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "wants.", "start_ms": 990, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "최근 대만 방송을 통해 공개된 한국 방위 산업 관련 소식이 예상보다 훨씬 강한 파장을 일으키면서 대만 사회 전체가 큰 요동을 치고 있기 때문입니다 무엇보다 충격적인",
"speaker": "speaker_10",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "최근", "start_ms": 15, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "대만", "start_ms": 125, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "방송을", "start_ms": 255, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "통해", "start_ms": 415, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "공개된", "start_ms": 525, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 685, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "방위", "start_ms": 795, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "산업", "start_ms": 925, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "관련", "start_ms": 1035, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "소식이", "start_ms": 1165, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "예상보다", "start_ms": 1325, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "훨씬", "start_ms": 1515, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "강한", "start_ms": 1645, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "파장을", "start_ms": 1755, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "일으키면서", "start_ms": 1915, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "대만", "start_ms": 2175, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "사회", "start_ms": 2285, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "전체가", "start_ms": 2415, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 2575, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "요동을", "start_ms": 2635, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "치고", "start_ms": 2795, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있기", "start_ms": 2905, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "때문입니다", "start_ms": 3015, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "무엇보다", "start_ms": 3275, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "충격적인", "start_ms": 3465, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "여기 유명한 데라고 제가 들었어요 어 근데 안에 자리도 있어요? 아네 자리도 있어요 아 그래요? 앉아서 좀만 좀만",
"speaker": "speaker_6",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "여기", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "유명한", "start_ms": 150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "데라고", "start_ms": 310, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 500, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "들었어요", "start_ms": 630, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "어", "start_ms": 840, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 900, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "안에", "start_ms": 1030, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "자리도", "start_ms": 1140, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있어요?", "start_ms": 1300, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "아네", "start_ms": 1490, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "자리도", "start_ms": 1600, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 1760, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 1950, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "그래요?", "start_ms": 2020, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "앉아서", "start_ms": 2180, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "좀만", "start_ms": 2370, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "좀만", "start_ms": 2500, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "직접 만들었답니다 비닐 깔고 그다음 철근 양생하고 이런 모든 과정들을 가족들이 같이 했는데 저희는 이제 인건비가 안 들어가잖아요 그러면 이곳을 지을 때 재료는 아끼지 말자 그래서 강",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "직접", "start_ms": 20, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "만들었답니다", "start_ms": 150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "비닐", "start_ms": 460, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "깔고", "start_ms": 610, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "그다음", "start_ms": 740, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "철근", "start_ms": 900, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "양생하고", "start_ms": 1050, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 1310, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "모든", "start_ms": 1440, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "과정들을", "start_ms": 1600, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "가족들이", "start_ms": 1810, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 2040, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "했는데", "start_ms": 2170, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "저희는", "start_ms": 2360, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 2520, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "인건비가", "start_ms": 2650, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 2910, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "들어가잖아요", "start_ms": 2980, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "그러면", "start_ms": 3270, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이곳을", "start_ms": 3430, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "지을", "start_ms": 3580, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 3690, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "재료는", "start_ms": 3780, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "아끼지", "start_ms": 3940, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "말자", "start_ms": 4130, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 4260, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "강", "start_ms": 4420, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "벌었을 뿐만 아니라 복잡한 지정학적 체스판 위에서 미국이 중동에서 차지하고 있던 무기 시장의 기반까지 흔드는 데 성공했습니다 그렇다면 한국 군수 산업 확대의 야심은 도대체 무엇일까요 한국 군수 산업",
"speaker": "speaker_11",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "벌었을", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "뿐만", "start_ms": 190, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 300, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "복잡한", "start_ms": 430, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "지정학적", "start_ms": 620, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "체스판", "start_ms": 880, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "위에서", "start_ms": 1070, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "미국이", "start_ms": 1230, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "중동에서", "start_ms": 1360, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "차지하고", "start_ms": 1550, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "있던", "start_ms": 1740, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "무기", "start_ms": 1850, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "시장의", "start_ms": 1960, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "기반까지", "start_ms": 2120, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "흔드는", "start_ms": 2350, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "데", "start_ms": 2540, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "성공했습니다", "start_ms": 2610, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그렇다면", "start_ms": 2970, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 3180, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "군수", "start_ms": 3310, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "산업", "start_ms": 3420, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "확대의", "start_ms": 3550, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "야심은", "start_ms": 3710, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "도대체", "start_ms": 3900, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "무엇일까요", "start_ms": 4060, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 4370, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "군수", "start_ms": 4500, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "산업", "start_ms": 4610, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 21,
"transcript": "Ye, mustard, mustard, mustard seeds in there. Oh, that's a grass, that's a grass. Oh, that's the... that's the new thing.",
"speaker": "speaker_8",
"confidence": 0.93,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Ye,", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "mustard,", "start_ms": 130, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "mustard,", "start_ms": 290, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "mustard", "start_ms": 450, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "seeds", "start_ms": 580, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 690, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "there.", "start_ms": 740, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "Oh,", "start_ms": 870, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "that's", "start_ms": 960, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 1070, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "grass,", "start_ms": 1120, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "that's", "start_ms": 1280, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 1390, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "grass.", "start_ms": 1440, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "Oh,", "start_ms": 1600, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "that's", "start_ms": 1690, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "the...", "start_ms": 1800, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "that's", "start_ms": 1930, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 2040, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "new", "start_ms": 2110, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "thing.", "start_ms": 2220, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "이거 생선이다 아 진짜 맛있어요 음 이거 맛있어 진짜 생선 맛 아니에요? 뭔가 달달해",
"speaker": "speaker_6",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "이거", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "생선이다", "start_ms": 140, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 330, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 400, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "맛있어요", "start_ms": 510, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "음", "start_ms": 720, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "이거", "start_ms": 810, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "맛있어", "start_ms": 920, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 1080, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "생선", "start_ms": 1190, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "맛", "start_ms": 1320, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "아니에요?", "start_ms": 1390, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "뭔가", "start_ms": 1580, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "달달해", "start_ms": 1710, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "의원은 대만의 존엄성을 지키는 것은 필요하다면서도 구체적인 상호 대응에서는 예의와 소통의 세부 사항을 신중히 관리해야 한다고 말했다 일부 정치인들은",
"speaker": "speaker_5",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "의원은", "start_ms": 15, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "대만의", "start_ms": 145, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "존엄성을", "start_ms": 255, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "지키는", "start_ms": 445, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "것은", "start_ms": 575, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "필요하다면서도", "start_ms": 665, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "구체적인", "start_ms": 925, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "상호", "start_ms": 1085, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "대응에서는", "start_ms": 1175, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "예의와", "start_ms": 1365, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "소통의", "start_ms": 1495, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "세부", "start_ms": 1605, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "사항을", "start_ms": 1695, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "신중히", "start_ms": 1825, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "관리해야", "start_ms": 1955, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "한다고", "start_ms": 2115, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "말했다", "start_ms": 2225, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "일부", "start_ms": 2355, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "정치인들은", "start_ms": 2445, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "큰 결과를 내놓고 있습니다 UAE 방공망은 2000회가 넘는 공격을 90% 이상 요격해 냈습니다 이 높은 요격률의 중심에는 대한민국의 천궁2가 있습니다",
"speaker": "speaker_12",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "큰", "start_ms": 25, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "결과를", "start_ms": 85, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "내놓고", "start_ms": 215, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 345, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "UAE", "start_ms": 505, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "방공망은", "start_ms": 615, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "2000회가", "start_ms": 805, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "넘는", "start_ms": 1015, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "공격을", "start_ms": 1125, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "90%", "start_ms": 1255, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이상", "start_ms": 1365, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "요격해", "start_ms": 1455, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "냈습니다", "start_ms": 1585, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 1745, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "높은", "start_ms": 1795, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "요격률의", "start_ms": 1885, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "중심에는", "start_ms": 2075, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "대한민국의", "start_ms": 2235, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "천궁2가", "start_ms": 2445, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 2635, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "이러한 열 배 더 재미있게 즐기실 수 있을 거고요 그리고 이런 똥행 축제 자체가 있어요 4월 21일부터 5월 12일까지 더 많이 즐기실 수도 있고 여러분 그리고",
"speaker": "speaker_4",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "이러한", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "열", "start_ms": 180, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "배", "start_ms": 250, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 310, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "재미있게", "start_ms": 380, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "즐기실", "start_ms": 570, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 730, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "있을", "start_ms": 780, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "거고요", "start_ms": 850, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 980, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 1110, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "똥행", "start_ms": 1220, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "축제", "start_ms": 1380, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "자체가", "start_ms": 1510, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 1660, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "4월", "start_ms": 1820, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "21일부터", "start_ms": 1930, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "5월", "start_ms": 2190, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "12일까지", "start_ms": 2300, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 2560, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 2630, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "즐기실", "start_ms": 2740, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "수도", "start_ms": 2900, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "있고", "start_ms": 2990, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "여러분", "start_ms": 3100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 3260, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "되게 되며 방공망이 잇따라 뚫리자 미국이 폴란드에 패트리어트 시스템 한 대와 패트리어트 발사용 미사일을 요청했지만 폴란드 영공을 방어",
"speaker": "speaker_7",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "되게", "start_ms": 20, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "되며", "start_ms": 110, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "방공망이", "start_ms": 200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "잇따라", "start_ms": 360, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "뚫리자", "start_ms": 490, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "미국이", "start_ms": 650, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "폴란드에", "start_ms": 780, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "패트리어트", "start_ms": 940, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "시스템", "start_ms": 1130, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 1280, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "대와", "start_ms": 1330, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "패트리어트", "start_ms": 1420, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "발사용", "start_ms": 1610, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "미사일을", "start_ms": 1770, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "요청했지만", "start_ms": 1930, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "폴란드", "start_ms": 2160, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "영공을", "start_ms": 2290, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "방어", "start_ms": 2420, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "위해 대만이 보이콧한 것 등이 포함된다 그러나 전문가들과 일부 정치인들은 이러한 조치의 실효성에 의문을 제기하고 있다 대부분의 국가들이 외교적으로 베이징을 인정하고 있기 때문이다 이번 논란",
"speaker": "speaker_5",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "위해", "start_ms": 15, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "대만이", "start_ms": 105, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "보이콧한", "start_ms": 215, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 375, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "등이", "start_ms": 425, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "포함된다", "start_ms": 495, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "그러나", "start_ms": 655, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "전문가들과", "start_ms": 765, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "일부", "start_ms": 955, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "정치인들은", "start_ms": 1045, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "이러한", "start_ms": 1235, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "조치의", "start_ms": 1365, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "실효성에", "start_ms": 1475, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "의문을", "start_ms": 1635, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "제기하고", "start_ms": 1745, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있다", "start_ms": 1905, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "대부분의", "start_ms": 1995, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "국가들이", "start_ms": 2155, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "외교적으로", "start_ms": 2315, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "베이징을", "start_ms": 2505, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "인정하고", "start_ms": 2665, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있기", "start_ms": 2825, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "때문이다", "start_ms": 2935, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이번", "start_ms": 3095, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "논란", "start_ms": 3185, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "전문 투자자도 아니고 투자를 공부한 사람이 아니기 때문에 어떤 그런 지표와 어떤 어려운 영어 용어 같은 거 잘 이해하기가 힘들어요 근데 그거를 쉽게 설명해 주고 그 차트를",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "전문", "start_ms": 25, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "투자자도", "start_ms": 135, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "아니고", "start_ms": 295, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "투자를", "start_ms": 425, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "공부한", "start_ms": 555, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "사람이", "start_ms": 715, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "아니기", "start_ms": 845, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "때문에", "start_ms": 975, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 1135, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "그런", "start_ms": 1245, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "지표와", "start_ms": 1335, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 1495, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "어려운", "start_ms": 1605, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "영어", "start_ms": 1735, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "용어", "start_ms": 1845, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "같은", "start_ms": 1955, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 2045, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 2095, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "이해하기가", "start_ms": 2155, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "힘들어요", "start_ms": 2365, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 2555, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "그거를", "start_ms": 2685, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "쉽게", "start_ms": 2845, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "설명해", "start_ms": 2975, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "주고", "start_ms": 3165, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 3275, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "차트를", "start_ms": 3325, "dur_ms": 150, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 08:52:19*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=19672  time=08:50:02  uptime=15419s  mem=176MB  system_processes=408"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_084945.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 751-751-756-576-567-517 574-574-574-574-574-574-574-574-574-571-574-571-574-574-574-574-574-571-574-571-571 574-576-574-574-571-571-751-576-751-753-751-751-753-675-675 574-576-5_W.mp3)
  Segment 2: 1576ms ~ 8848ms (file: 203-312-267-263-261-120-106-320-102-132-576-103-263-130-231-173-103-321-143-130-164-174-203-512-652-165-106-126-127-130-130-107-176-120-273-273-107-132-263-267-310-321-236-263-1_W.mp3)
  Segment 3: 9848ms ~ 10424ms (file: 517 574-571-571-571-571-571-570-517-576-571-157-751-571-570-576-571-751-573 571-517-571-571-571-571-571-751-573-571-751-751-571-750-570-750-570 571-571-574-571-571-571-571-571 5_V.mp3)
  Segment 4: 11424ms ~ 16896ms (file: 103-136-130-102-312-132-230-236-721-273-273-273-276-732-372-723-312-312-317-103-310-310-172-102-326-312-721-312-713-312-312-217-317-231-123-751-517-315-375-132-130-312-172-310-3_W.mp3)
  Segment 5: 17896ms ~ 18472ms (file: 751-752 517-571-510-571-315-173-301-105-157-573-517-153-751-157-753-513-715-513-517 571-517-501-150-170-175-157-753-173-751-731-751-175-753-571-751-571-750-750-571 150-517-150-1_V.mp3)
  Segment 6: 19472ms ~ 30920ms (file: 130-231-236-213-267-321-310-136-267-627-627-321-136-213-627-102-261-627-126-756-106-102-126-236-102-236-320-753-672-627-620-321-672-623-102-175-162-167-231-261-627-627-260-263-1_W.mp3)
  Segment 7: 31920ms ~ 37320ms (file: 176-571-231-276-267-765-257-103-107-507-513-321-267-260-267-230-750-756-765-720-137-106-176-217-327-130-236-261-102-201-627-276-106-617-170-163-320-327-302-312-216-103-126-130-1_W.mp3)
  Segment 8: 38320ms ~ 45448ms (file: 215-205-267-213-102-216-123-321-123-216-120-120-126-263-263-261-130-263-263-206-120-236-106-160-263-321-106-120-120-153-627-257-326-263-267-216-726-267-261-236-723-237-231-765-2_W.mp3)
  Segment 9: 46448ms ~ 54404ms (file: 375-357-317-301-236-267-517-267-312-321-362-632-762-106-107-136-167-312-137-301-301-315-132-132-107-130-257-157-267-360-321-275-317-267-312-723-726-157-263-103-157-312-136-137-1_W.mp3)
  Segment 10: 55404ms ~ 55980ms (file: 705-751-571-570-157-573-571-571-751-751-570-571-751 576-570-573-573-571-751-175-157-571-751-157-570-157-570-570-570-571-751-571-750-751-751 574-576-570-571-576-570-573-571-570-5_W.mp3)
  Segment 11: 56980ms ~ 57556ms (file: 276-702-760-253-267-263-720-623-236-173-276-276-253-752-237-753-125-572-725-375-762-574-516-213-516-235-571-751-235-725-527-326 627-361-236-607-675-675-657-361-167-317-763-371-1_V.mp3)
  Segment 12: 58556ms ~ 59132ms (file: 627-632-361-136-312-132-312-132-136-153-157-167-137-160-135-176-561-567-165-136-153-137-751-156-536-157-671-617-731-673-167-167-167-613-167-163-617-316-613-671-367-716-671-637-6_W.mp3)
  Segment 13: 60132ms ~ 66504ms (file: 130-132-621-260-750-621-620-265-607-602-623-312-672-607-657-273-623-712-172-157-216-627-567-316-765-607-310-203-326-230-276-205-527-310-103-136-316-230-302-213-637-203-521-135-3_V.mp3)
  Segment 14: 67504ms ~ 75028ms (file: 230-756-120-260-126-260-203-123-130-132-627-627-267-162-213-107-132-760-172-120-301-103-261-267-705-756-756-627-276-102-127-103-762-756-675-216-126-120-210-210-267-672-160-123-6_W.mp3)
  Segment 15: 76028ms ~ 76604ms (file: 162-120-126-615-561-612-125-213-165-162-125-126-123-521-152-126-156-156-615-561-162-126-160-615-651-612-615-162-163-167-130-167-156-517-156-157-567-567-675-615-576-567-756-657-6_W.mp3)
  Segment 16: 77604ms ~ 78180ms (file: 157-175-175-175-175-157-175-137-135-312-123-132-132-123-132-132-132-132-132-136-135-135-132-310-132-130-120-126-120-120-152-153-153-175-157-157-173-517-157-175-157-157-175-153-1_V.mp3)
  Segment 17: 79180ms ~ 85408ms (file: 312-216-216-213-123-163-726-327-263-216-710-276-327-362-106-613-621-627-317-237-276-627-163-160-627-620-137-260-726-726-267-267-627-267-627-267-263-132-213-123-312-326-236-632-1_W.mp3)
  Segment 18: 86408ms ~ 92672ms (file: 230-267-230-236-120-302-230-123-321-213-723-761-103-123-752-765-261-203-102-672-762-310-321-132-512-752-320-210-250-631-631-103-206-230-310-617-260-260-175-571-310-230-756-751-6_W.mp3)
  Segment 19: 93672ms ~ 94248ms (file: 157-157-715-715-157-175-135-735-172-175-517-750-750-175-751-571-570-517-570-571-751-750-751-751-517-571-570-571-756 570-570-157-571-751-731-176-517-735-751-375-175-751-751-517-7_V.mp3)
  Segment 20: 95248ms ~ 95824ms (file: 573-517-650-653-132-651-132-362-752-267-765-276-627-756-632-367-367-371-367-376-376-312 316-136-132 316-326-362-365-732-356 316-576-326-671-657-532-216-203-362-267-237-136-316-3_V.mp3)
  Segment 21: 96824ms ~ 103340ms (file: 236-752-132-361-326-102-271-231-123-127-231-237-312-231-230-602-160-167-321-231-301-130-236-261-231-213-321-207-237-207-236-103-231-236-231-763-376-637-160-675-765-326-310-130-3_W.mp3)
  Segment 22: 104340ms ~ 104916ms (file: 213-123-120-123-162-106-106-105-157-150-157-571-517-701-715-705-751-570-120-123-132-120-571-750-751-571-157-751-715-705-750-705-750-570-750 751-157-750-507-751-571-571-750-751-7_V.mp3)
  Segment 23: 105916ms ~ 106492ms (file: 163-136-162-136-613-213-631-376-316-132-130-132-103-170-130-136-137-136-312-316-173-372-736-672-762-716-723-173-371-731-716-731-175-721-271-761-617-613-716-167-761-761-716-762-6_W.mp3)
  Segment 24: 107492ms ~ 108068ms (file: 571-571-157-517-517-157-175-137-357-715-517-576-751-517-756-756-571 570-570-751-571-157-753-517-750-157-517-573-713-751-753-573-517-751-570-570-571-751-576-756-573 570-573-570-7_V.mp3)
  Segment 25: 109068ms ~ 109644ms (file: 672-672-765-756-675-765-657-725-726-675-675-167-175-573-165-156-567-135-571-516-516-576-567-753-751-756-756-761-716-716-716-712-715-573-756-751-576-576-567-156-561-615-576-657-5_W.mp3)
  Segment 26: 110644ms ~ 117340ms (file: 276-632-276-263-127-271-267-276-267-123-321-162-271-276-327-217-327-236-273-267-276-271-276-723-273-732-132-175-132-167-132-106-617-130-103-301-231-103-301-312-174-103-162-164-2_W.mp3)
  Segment 27: 118340ms ~ 118916ms (file: 706-657-567-625-157-301-137-132-213-537-317-173-267-176-750-576-673-270-756-527-503-605-675-762-317-165-672-576-752-257-731-175-275-731-275-325-726-257-754-217-173-562-752-376-7_V.mp3)
  Segment 28: 119916ms ~ 120492ms (file: 657-651-652-126-153-167-152-165-651-576-567-562-256-216-576-567-657-567-752-657-756-651-657-621-265-567-125-165-167-165-657-576-621-651-163-162-136-162-163-162-615-625-126-216-1_W.mp3)
  Segment 29: 121492ms ~ 122068ms (file: 517-517-571-571-572 574-576-574-517-571-517-517-517-750-751-175-751-157-571-517-573-751-571-570-576-570-570-571-570-571-570 571-570-574-571-157-375-157-571-751-753-157-750-570-7_V.mp3)
  Segment 30: 123068ms ~ 123644ms (file: 752-167-612-725-750-130-132-132-675-670-716-371-675-576-756-205-562-312-567-327-137-325-257-132-602-765-653-723-625-726-135-321-765-273-567-126-752-716-235-276-172-506-721-123-6_V.mp3)
  Segment 31: 124644ms ~ 125220ms (file: 103-130-136-236-321-123-126-216-754-623-576-576-627-231-362-574-130-136-576-637-316-106-103-263-316-263-672-674-672-675-627-213-163-627-675-627-126-130-367-627-130-310-621-236-5_W.mp3)
  Segment 32: 126220ms ~ 126796ms (file: 617-567-156-517-567-576-156-167-576-657-126-165-123-276-675-163-356-302-671-617-170-175-571-751-157-175-571-517-751-657-567-567-675-657-567-576-567-156-157-576-765-765-657-576-5_W.mp3)
  Segment 33: 127796ms ~ 128372ms (file: 517-670-213-713-506-750-237-627-271-762-137-276-623-720-521-361-627-167-762-723-517-136-570_V.mp3)
  Segment 34: 129372ms ~ 129948ms (file: 523 652-635 610-613 326-561 632-162-167 567 675-627-621 613-163-163-612-613-615-613-631-613-673 621-315-615 623 627_W.mp3)
  Segment 35: 130948ms ~ 140668ms (file: 263-263-236-103-132-260-326-320-631-326-230-230-302-230-301-103-316-612-163-316-321-132-261-621-126-312-320-230-213 260-260-263-165-163-165-765-310-312-163-106-165-106-130-321-2_W.mp3)
  Segment 36: 141668ms ~ 142244ms (file: 571-574-574-576-576-576-571 574-574-576-574-574-574-574-574-574-571-574-576-574-572-574-574-571-576-576-576-576 510-571-517-517-175-175-157-137-715-573-570-571-170-751-750-751-5_W.mp3)
  Segment 37: 143244ms ~ 143820ms (file: 751-175-715-175-715-751-751-753-751-753 576-576-576-571-571-576-571 574-517-751-715-753-715-751-751-576-571-571-571-570-576-573 574-576-573-570-570 574-571-571-576-576-756-571-5_W.mp3)
  Segment 38: 144820ms ~ 145396ms (file: 713-710-571-571-570-753-751-751-571-570-752-750 574-571-570-571-571-571-751-570-571-517-571-751-571-573-157-571-751-571-751-751-753-571-517 574-576-576-576-571-576-571-517-513-5_W.mp3)
  Segment 39: 146396ms ~ 146972ms (file: 103-103-132-103-153-157-157-157-517-120-132-123-120-120-120-120 571-571-571-574 573-574-571-571-103-103-103-137-103-102-103-102-102-102-126-162-126-132-130-120-102-135-130-135-1_W.mp3)
  Segment 40: 147972ms ~ 158520ms (file: 263-327-372-317-731-237-267-756-267-237-572-310-167-726-317-327-713-130-317-723-273-327-754-317-130-752-276-267-261-276-527-574-137-753-310-103-276-372-123-237-576-537-137-273-3_W.mp3)
  Segment 41: 159520ms ~ 160096ms (file: 627-236-276-756-572-576-571-276-265-253-657-570-756-627-675-502-273-507-325-526-326_V.mp3)
  Segment 42: 161096ms ~ 161672ms (file: 216-216-176-165-163-162-176-167-167-176-172-713-172-317 132-213-127-123-123-123-132-137-137-163 175-715-751-175-715-571-135-517 517-153-157-157-157-153-531-517-157-135-157-153-1_W.mp3)
  Segment 43: 162672ms ~ 173580ms (file: 574-103-216-621-524-235-532-236-312-513-312-260-260-260-267-267-326-103-157-301-310-762-326-765-375-123-216-762-675-617-762-367-730-571-716-317-612-620-357-137-132-132-126-672-6_W.mp3)
  Segment 44: 174580ms ~ 175156ms (file: 736-763-763-763-716-761-756-371-762-763-736-763-763-763-167-756-736-376-367-317-751-157-157-571-125-165-132-137-263-216-271-273-751-157-136-123-765-576-275-623-756-726-527-203-2_V.mp3)
  Segment 45: 176156ms ~ 180512ms (file: 120-120-102-120-201-213-102-102-123-126-120-120-210-213-123-216-213-126-102-201-126-120-120-210-120-210-123-123-102-123-312-321-123-312-310-312-132-123-210-102-210-231-123-203-2_V.mp3)
  Segment 46: 181512ms ~ 182088ms (file: 275-761-725-765-167-657-351-527-567-761-715-675-615-571-567-516-157-536-756-576-751-765-736-163-127-167-136-765-756-765-762-765-376-765-762-765-137-167-612-175-735-765-531-756-3_W.mp3)
  Segment 47: 183088ms ~ 192844ms (file: 132-162-267-312-126-326-765-716-752-326-312-132-106-753-231-261-132-132-120-207-310-130-173-675-756-765-213-173-137-236-236-236-321-321-263-236-752-725-271-175-103-103-731-207-1_W.mp3)
  Segment 48: 193844ms ~ 194420ms (file: 627-762-765-765-760-567-657-657-576-675-756_W.mp3)
  Segment 49: 195420ms ~ 203808ms (file: 107 765-263-237 537-652-103-216-105-501-123-265-231-526-267-507-253-203-263-765-206-230-627-215-136-570-207-312-357-302-157-351-625-502-267-367-123-736-756-506-617-305-106-672-2_V.mp3)
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
> **Time**: 2026-04-06 08:52:25
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "사라졌답니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "사라졌답니다", "start_ms": 20, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "인허가 비용 이런 거 기본적으로 500에서 1000만 원 정도 이렇게 나오더라고요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "인허가", "start_ms": 10, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "비용", "start_ms": 140, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 230, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 300, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "기본적으로", "start_ms": 350, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "500에서", "start_ms": 510, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "1000만", "start_ms": 620, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "원", "start_ms": 710, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "정도", "start_ms": 760, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 830, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "나오더라고요", "start_ms": 920, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "그럼 그 돈 필요해서 열심히 벌었어요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 10, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 80, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "돈", "start_ms": 130, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "필요해서", "start_ms": 200, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "열심히", "start_ms": 330, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "벌었어요", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "엄청나게 더 업그레이드된 것은 아닙니다",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "엄청나게", "start_ms": 15, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 180, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "업그레이드된", "start_ms": 250, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "것은", "start_ms": 440, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "아닙니다", "start_ms": 530, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "애초에 프리미어리그에서도 손꼽히는 패서였던 손흥민 선수이자 토트넘 역대 최고의 7번",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "애초에", "start_ms": 10, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "프리미어리그에서도", "start_ms": 140, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "손꼽히는", "start_ms": 500, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "패서였던", "start_ms": 710, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "손흥민", "start_ms": 900, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "선수이자", "start_ms": 1060, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "토트넘", "start_ms": 1250, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "역대", "start_ms": 1400, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "최고의", "start_ms": 1510, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "7번", "start_ms": 1640, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "와 이거 조금만 움직여도 머리 부딪히겠네 와 이게 제 키 아니면은 저 부분만 허용하는 걸로 해서 폭을 좁게 했어요 그래야지 공간이 나오거든요",
"speaker": "speaker_4",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "와", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이거", "start_ms": 160, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "조금만", "start_ms": 290, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "움직여도", "start_ms": 480, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "머리", "start_ms": 740, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "부딪히겠네", "start_ms": 900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "와", "start_ms": 1260, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 1370, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "제", "start_ms": 1500, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "키", "start_ms": 1590, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "아니면은", "start_ms": 1700, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "저", "start_ms": 1930, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "부분만", "start_ms": 2020, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "허용하는", "start_ms": 2210, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "걸로", "start_ms": 2470, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "해서", "start_ms": 2630, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "폭을", "start_ms": 2760, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "좁게", "start_ms": 2910, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "했어요", "start_ms": 3080, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "그래야지", "start_ms": 3290, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "공간이", "start_ms": 3520, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "나오거든요", "start_ms": 3710, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "외국 정부들이 늘어나는 데 반발하고 있다",
"speaker": "speaker_5",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "외국", "start_ms": 20, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "정부들이", "start_ms": 110, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "늘어나는", "start_ms": 270, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "데", "start_ms": 400, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "반발하고", "start_ms": 450, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있다", "start_ms": 610, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "그러나 이러한 대응 조치가 실제로 효과를 낼 수 있을지에 대한 의문도 커지고 있다",
"speaker": "speaker_5",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "그러나", "start_ms": 15, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이러한", "start_ms": 125, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "대응", "start_ms": 255, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "조치가", "start_ms": 345, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "실제로", "start_ms": 455, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "효과를", "start_ms": 585, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "낼", "start_ms": 695, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 745, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "있을지에", "start_ms": 785, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "대한", "start_ms": 935, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "의문도", "start_ms": 1025, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "커지고", "start_ms": 1135, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "있다", "start_ms": 1265, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "최근 몇 주 사이 논란은 더욱 확대됐다",
"speaker": "speaker_5",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "최근", "start_ms": 10, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "몇", "start_ms": 100, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "주", "start_ms": 150, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "사이", "start_ms": 200, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "논란은", "start_ms": 270, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "더욱", "start_ms": 380, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "확대됐다", "start_ms": 470, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "한국과 덴마크 그리고 카메룬 정부가",
"speaker": "speaker_5",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "한국과", "start_ms": 20, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "덴마크", "start_ms": 150, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 260, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "카메룬", "start_ms": 350, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "정부가", "start_ms": 460, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "진짜 달달한 감자라고 생각해 그렇지 달달한 감자야 하나도 안 짜고 단맛도 많이 나고 그렇지",
"speaker": "speaker_6",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "달달한", "start_ms": 150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "감자라고", "start_ms": 310, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "생각해", "start_ms": 500, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "그렇지", "start_ms": 660, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "달달한", "start_ms": 790, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "감자야", "start_ms": 950, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "하나도", "start_ms": 1090, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 1220, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "짜고", "start_ms": 1280, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "단맛도", "start_ms": 1370, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 1520, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "나고", "start_ms": 1630, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "그렇지", "start_ms": 1720, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "때문에 50일 넘게 걸립니다 중동 산의 두 배 이상입니다 더 비싼 운임을 들여 가져와도 우리 산업 구조에 잘 안 맞습니다 석유 화학 업계는 끈적끈적한 중동 산 중질유 위주로 정제 나프타와 벙커C유",
"speaker": "speaker_7",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "때문에", "start_ms": 25, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "50일", "start_ms": 155, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "넘게", "start_ms": 265, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "걸립니다", "start_ms": 355, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "중동", "start_ms": 515, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "산의", "start_ms": 625, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "두", "start_ms": 715, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "배", "start_ms": 765, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "이상입니다", "start_ms": 815, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 975, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "비싼", "start_ms": 1025, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "운임을", "start_ms": 1115, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "들여", "start_ms": 1225, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "가져와도", "start_ms": 1315, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "우리", "start_ms": 1475, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "산업", "start_ms": 1565, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "구조에", "start_ms": 1675, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 1805, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 1855, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "맞습니다", "start_ms": 1905, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "석유", "start_ms": 2065, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "화학", "start_ms": 2155, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "업계는", "start_ms": 2245, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "끈적끈적한", "start_ms": 2375, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "중동", "start_ms": 2635, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "산", "start_ms": 2745, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "중질유", "start_ms": 2795, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "위주로", "start_ms": 2955, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "정제", "start_ms": 3085, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "나프타와", "start_ms": 3195, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "벙커C유", "start_ms": 3405, "dur_ms": 200, "phase_lr_deg": 0.0}
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
"transcript": "So, is minari in there? How does it taste? I think just...",
"speaker": "speaker_8",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "So,", "start_ms": 20, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 130, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "minari", "start_ms": 190, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 400, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "there?", "start_ms": 470, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "How", "start_ms": 600, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "does", "start_ms": 690, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "it", "start_ms": 800, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "taste?", "start_ms": 850, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 1010, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "think", "start_ms": 1060, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "just...", "start_ms": 1170, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "She's so lazy. I think that's what he really wants.",
"speaker": "speaker_9",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "She's", "start_ms": 30, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "so", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "lazy.", "start_ms": 250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 410, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "think", "start_ms": 460, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "that's", "start_ms": 570, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "what", "start_ms": 700, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "he", "start_ms": 790, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "really", "start_ms": 860, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "wants.", "start_ms": 990, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "최근 대만 방송을 통해 공개된 한국 방위 산업 관련 소식이 예상보다 훨씬 강한 파장을 일으키면서 대만 사회 전체가 큰 요동을 치고 있기 때문입니다 무엇보다 충격적인",
"speaker": "speaker_10",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "최근", "start_ms": 15, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "대만", "start_ms": 125, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "방송을", "start_ms": 255, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "통해", "start_ms": 415, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "공개된", "start_ms": 525, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 685, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "방위", "start_ms": 795, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "산업", "start_ms": 925, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "관련", "start_ms": 1035, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "소식이", "start_ms": 1165, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "예상보다", "start_ms": 1325, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "훨씬", "start_ms": 1515, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "강한", "start_ms": 1645, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "파장을", "start_ms": 1755, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "일으키면서", "start_ms": 1915, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "대만", "start_ms": 2175, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "사회", "start_ms": 2285, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "전체가", "start_ms": 2415, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 2575, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "요동을", "start_ms": 2635, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "치고", "start_ms": 2795, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있기", "start_ms": 2905, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "때문입니다", "start_ms": 3015, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "무엇보다", "start_ms": 3275, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "충격적인", "start_ms": 3465, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "여기 유명한 데라고 제가 들었어요 어 근데 안에 자리도 있어요? 아네 자리도 있어요 아 그래요? 앉아서 좀만 좀만",
"speaker": "speaker_6",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "여기", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "유명한", "start_ms": 150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "데라고", "start_ms": 310, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 500, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "들었어요", "start_ms": 630, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "어", "start_ms": 840, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 900, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "안에", "start_ms": 1030, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "자리도", "start_ms": 1140, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있어요?", "start_ms": 1300, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "아네", "start_ms": 1490, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "자리도", "start_ms": 1600, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 1760, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 1950, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "그래요?", "start_ms": 2020, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "앉아서", "start_ms": 2180, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "좀만", "start_ms": 2370, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "좀만", "start_ms": 2500, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "직접 만들었답니다 비닐 깔고 그다음 철근 양생하고 이런 모든 과정들을 가족들이 같이 했는데 저희는 이제 인건비가 안 들어가잖아요 그러면 이곳을 지을 때 재료는 아끼지 말자 그래서 강",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "직접", "start_ms": 20, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "만들었답니다", "start_ms": 150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "비닐", "start_ms": 460, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "깔고", "start_ms": 610, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "그다음", "start_ms": 740, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "철근", "start_ms": 900, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "양생하고", "start_ms": 1050, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 1310, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "모든", "start_ms": 1440, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "과정들을", "start_ms": 1600, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "가족들이", "start_ms": 1810, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 2040, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "했는데", "start_ms": 2170, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "저희는", "start_ms": 2360, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 2520, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "인건비가", "start_ms": 2650, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 2910, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "들어가잖아요", "start_ms": 2980, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "그러면", "start_ms": 3270, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이곳을", "start_ms": 3430, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "지을", "start_ms": 3580, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 3690, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "재료는", "start_ms": 3780, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "아끼지", "start_ms": 3940, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "말자", "start_ms": 4130, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 4260, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "강", "start_ms": 4420, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "벌었을 뿐만 아니라 복잡한 지정학적 체스판 위에서 미국이 중동에서 차지하고 있던 무기 시장의 기반까지 흔드는 데 성공했습니다 그렇다면 한국 군수 산업 확대의 야심은 도대체 무엇일까요 한국 군수 산업",
"speaker": "speaker_11",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "벌었을", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "뿐만", "start_ms": 190, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 300, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "복잡한", "start_ms": 430, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "지정학적", "start_ms": 620, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "체스판", "start_ms": 880, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "위에서", "start_ms": 1070, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "미국이", "start_ms": 1230, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "중동에서", "start_ms": 1360, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "차지하고", "start_ms": 1550, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "있던", "start_ms": 1740, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "무기", "start_ms": 1850, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "시장의", "start_ms": 1960, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "기반까지", "start_ms": 2120, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "흔드는", "start_ms": 2350, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "데", "start_ms": 2540, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "성공했습니다", "start_ms": 2610, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그렇다면", "start_ms": 2970, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 3180, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "군수", "start_ms": 3310, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "산업", "start_ms": 3420, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "확대의", "start_ms": 3550, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "야심은", "start_ms": 3710, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "도대체", "start_ms": 3900, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "무엇일까요", "start_ms": 4060, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 4370, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "군수", "start_ms": 4500, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "산업", "start_ms": 4610, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 21,
"transcript": "Ye, mustard, mustard, mustard seeds in there. Oh, that's a grass, that's a grass. Oh, that's the... that's the new thing.",
"speaker": "speaker_8",
"confidence": 0.93,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Ye,", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "mustard,", "start_ms": 130, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "mustard,", "start_ms": 290, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "mustard", "start_ms": 450, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "seeds", "start_ms": 580, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 690, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "there.", "start_ms": 740, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "Oh,", "start_ms": 870, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "that's", "start_ms": 960, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 1070, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "grass,", "start_ms": 1120, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "that's", "start_ms": 1280, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 1390, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "grass.", "start_ms": 1440, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "Oh,", "start_ms": 1600, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "that's", "start_ms": 1690, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "the...", "start_ms": 1800, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "that's", "start_ms": 1930, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 2040, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "new", "start_ms": 2110, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "thing.", "start_ms": 2220, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "이거 생선이다 아 진짜 맛있어요 음 이거 맛있어 진짜 생선 맛 아니에요? 뭔가 달달해",
"speaker": "speaker_6",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "이거", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "생선이다", "start_ms": 140, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 330, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 400, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "맛있어요", "start_ms": 510, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "음", "start_ms": 720, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "이거", "start_ms": 810, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "맛있어", "start_ms": 920, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 1080, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "생선", "start_ms": 1190, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "맛", "start_ms": 1320, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "아니에요?", "start_ms": 1390, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "뭔가", "start_ms": 1580, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "달달해", "start_ms": 1710, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "의원은 대만의 존엄성을 지키는 것은 필요하다면서도 구체적인 상호 대응에서는 예의와 소통의 세부 사항을 신중히 관리해야 한다고 말했다 일부 정치인들은",
"speaker": "speaker_5",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "의원은", "start_ms": 15, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "대만의", "start_ms": 145, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "존엄성을", "start_ms": 255, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "지키는", "start_ms": 445, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "것은", "start_ms": 575, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "필요하다면서도", "start_ms": 665, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "구체적인", "start_ms": 925, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "상호", "start_ms": 1085, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "대응에서는", "start_ms": 1175, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "예의와", "start_ms": 1365, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "소통의", "start_ms": 1495, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "세부", "start_ms": 1605, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "사항을", "start_ms": 1695, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "신중히", "start_ms": 1825, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "관리해야", "start_ms": 1955, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "한다고", "start_ms": 2115, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "말했다", "start_ms": 2225, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "일부", "start_ms": 2355, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "정치인들은", "start_ms": 2445, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "큰 결과를 내놓고 있습니다 UAE 방공망은 2000회가 넘는 공격을 90% 이상 요격해 냈습니다 이 높은 요격률의 중심에는 대한민국의 천궁2가 있습니다",
"speaker": "speaker_12",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "큰", "start_ms": 25, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "결과를", "start_ms": 85, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "내놓고", "start_ms": 215, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 345, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "UAE", "start_ms": 505, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "방공망은", "start_ms": 615, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "2000회가", "start_ms": 805, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "넘는", "start_ms": 1015, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "공격을", "start_ms": 1125, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "90%", "start_ms": 1255, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이상", "start_ms": 1365, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "요격해", "start_ms": 1455, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "냈습니다", "start_ms": 1585, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 1745, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "높은", "start_ms": 1795, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "요격률의", "start_ms": 1885, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "중심에는", "start_ms": 2075, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "대한민국의", "start_ms": 2235, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "천궁2가", "start_ms": 2445, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 2635, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "이러한 열 배 더 재미있게 즐기실 수 있을 거고요 그리고 이런 똥행 축제 자체가 있어요 4월 21일부터 5월 12일까지 더 많이 즐기실 수도 있고 여러분 그리고",
"speaker": "speaker_4",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "이러한", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "열", "start_ms": 180, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "배", "start_ms": 250, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 310, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "재미있게", "start_ms": 380, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "즐기실", "start_ms": 570, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 730, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "있을", "start_ms": 780, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "거고요", "start_ms": 850, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 980, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 1110, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "똥행", "start_ms": 1220, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "축제", "start_ms": 1380, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "자체가", "start_ms": 1510, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 1660, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "4월", "start_ms": 1820, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "21일부터", "start_ms": 1930, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "5월", "start_ms": 2190, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "12일까지", "start_ms": 2300, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 2560, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 2630, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "즐기실", "start_ms": 2740, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "수도", "start_ms": 2900, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "있고", "start_ms": 2990, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "여러분", "start_ms": 3100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 3260, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "되게 되며 방공망이 잇따라 뚫리자 미국이 폴란드에 패트리어트 시스템 한 대와 패트리어트 발사용 미사일을 요청했지만 폴란드 영공을 방어",
"speaker": "speaker_7",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "되게", "start_ms": 20, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "되며", "start_ms": 110, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "방공망이", "start_ms": 200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "잇따라", "start_ms": 360, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "뚫리자", "start_ms": 490, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "미국이", "start_ms": 650, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "폴란드에", "start_ms": 780, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "패트리어트", "start_ms": 940, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "시스템", "start_ms": 1130, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 1280, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "대와", "start_ms": 1330, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "패트리어트", "start_ms": 1420, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "발사용", "start_ms": 1610, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "미사일을", "start_ms": 1770, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "요청했지만", "start_ms": 1930, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "폴란드", "start_ms": 2160, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "영공을", "start_ms": 2290, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "방어", "start_ms": 2420, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "위해 대만이 보이콧한 것 등이 포함된다 그러나 전문가들과 일부 정치인들은 이러한 조치의 실효성에 의문을 제기하고 있다 대부분의 국가들이 외교적으로 베이징을 인정하고 있기 때문이다 이번 논란",
"speaker": "speaker_5",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "위해", "start_ms": 15, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "대만이", "start_ms": 105, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "보이콧한", "start_ms": 215, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 375, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "등이", "start_ms": 425, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "포함된다", "start_ms": 495, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "그러나", "start_ms": 655, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "전문가들과", "start_ms": 765, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "일부", "start_ms": 955, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "정치인들은", "start_ms": 1045, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "이러한", "start_ms": 1235, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "조치의", "start_ms": 1365, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "실효성에", "start_ms": 1475, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "의문을", "start_ms": 1635, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "제기하고", "start_ms": 1745, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있다", "start_ms": 1905, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "대부분의", "start_ms": 1995, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "국가들이", "start_ms": 2155, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "외교적으로", "start_ms": 2315, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "베이징을", "start_ms": 2505, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "인정하고", "start_ms": 2665, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있기", "start_ms": 2825, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "때문이다", "start_ms": 2935, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이번", "start_ms": 3095, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "논란", "start_ms": 3185, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "전문 투자자도 아니고 투자를 공부한 사람이 아니기 때문에 어떤 그런 지표와 어떤 어려운 영어 용어 같은 거 잘 이해하기가 힘들어요 근데 그거를 쉽게 설명해 주고 그 차트를",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "전문", "start_ms": 25, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "투자자도", "start_ms": 135, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "아니고", "start_ms": 295, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "투자를", "start_ms": 425, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "공부한", "start_ms": 555, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "사람이", "start_ms": 715, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "아니기", "start_ms": 845, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "때문에", "start_ms": 975, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 1135, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "그런", "start_ms": 1245, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "지표와", "start_ms": 1335, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 1495, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "어려운", "start_ms": 1605, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "영어", "start_ms": 1735, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "용어", "start_ms": 1845, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "같은", "start_ms": 1955, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 2045, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 2095, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "이해하기가", "start_ms": 2155, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "힘들어요", "start_ms": 2365, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 2555, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "그거를", "start_ms": 2685, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "쉽게", "start_ms": 2845, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "설명해", "start_ms": 2975, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "주고", "start_ms": 3165, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 3275, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "차트를", "start_ms": 3325, "dur_ms": 150, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 08:52:25*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=18336  time=08:52:18  uptime=18460s  mem=378MB  system_processes=416"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_084534.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 7128ms (file: 326-620-130-327-103-321-230-132-126-320-260-213-572-126-120-361-132-163-167-316-310-130-167-316-125-102-106-501-326-317-123-201-206-123-712-321-321-265-507-207-216-261-236-107-2_W.mp3)
  Segment 2: 8128ms ~ 16480ms (file: 312-310-312-210-216-317-130-320-312-132-301-321-301-310-103-132-103-203-312-316-321-201-123-206-302-312-210-312-132-312-253-106-312-302-231-317-103-617-130-203-132-263-623-652-1_V.mp3)
  Segment 3: 17480ms ~ 18056ms (file: 317-327-572-257-572-267-752-267-752-276-756-765-576-163-517-532-217-576-231-627-573-756-172-276-175-375-136-612-123-160-723-276-627-376-745-257-405-231-752-130-271-672-230-632-5_V.mp3)
  Segment 4: 19056ms ~ 19632ms (file: 625-625-625-625-625-625-625-265-265-621-261-263-621-216-120_W.mp3)
  Segment 5: 20632ms ~ 21208ms (file: 753-573-751-576-756-357-753-657-675-375-763-673-657-653-675-367_W.mp3)
  Segment 6: 22208ms ~ 29624ms (file: 236-752-123-260-572-150-571-107-265-574-571-574-263-752-752-257-310-574-157-320-574-253-215-572-275-527-103-265-625-260-276-123-721-621-153-756-765-276-267-627-321-316-130-132-2_W.mp3)
  Segment 7: 30624ms ~ 38472ms (file: 231-231-230-126-102-162-210-230-320-210-102-263-206-576-201-230-203-203-230-230-107-230-230-732-230-236-261-620-210-362-162-103-512-315-236-103-167-167-106-102-103-103-120-231-3_W.mp3)
  Segment 8: 39472ms ~ 40048ms (file: 137-135-613-135-120 675-675-675-765-765-765-765-765_W.mp3)
  Segment 9: 41048ms ~ 41624ms (file: 701-705-170-105-710-715-750-752-751-715-130-123-162-160-160-162-106-102 123-132-716-715-175-105-107-175-157-157-751-527-175-175-175-570-751-715-175-715 571-571-715-507-570-751-5_V.mp3)
  Segment 10: 42624ms ~ 43200ms (file: 627-236-276-756-572-576-571-276-265-253-657-570-756-627-675-502-273-507-325-526-326_V.mp3)
  Segment 11: 44200ms ~ 44776ms (file: 103-130-103-103-510-103-105-136-316-136-310-130-103-103-103-130-310-130-130-312-321-312-312-316-316-136-316-163-167-167-613_W.mp3)
  Segment 12: 45776ms ~ 57224ms (file: 130-231-236-213-267-321-310-136-267-627-627-321-136-213-627-102-261-627-126-756-106-102-126-236-102-236-320-753-672-627-620-321-672-623-102-175-162-167-231-261-627-627-260-263-1_W.mp3)
  Segment 13: 58224ms ~ 64740ms (file: 236-752-132-361-326-102-271-231-123-127-231-237-312-231-230-602-160-167-321-231-301-130-236-261-231-213-321-207-237-207-236-103-231-236-231-763-376-637-160-675-765-326-310-130-3_W.mp3)
  Segment 14: 65740ms ~ 66316ms (file: 312-321-326-312-123-302-317-210-173-361-267-172-731-167-107-317-127-317-176-130-360-261-123-102-713-627-132_W.mp3)
  Segment 15: 67316ms ~ 74732ms (file: 632-675-103-573-765-276-705-706-267-257-206-752-103-267-126-261-162-130-216-216-132-157-175-735-627-750-170-570-105-210-567-276-756-765-160-136-762-267-176-627-576-671-206-672-7_W.mp3)
  Segment 16: 75732ms ~ 83076ms (file: 267-267-132-276-217-671-607-760-762-721-172-710-107-723-726-723-317-321-320-320-230-302-307-310-130-132-123-123-102-126-310-301-301-302-321-302-632-306-306-132-316-162-130-310-1_W.mp3)
  Segment 17: 84076ms ~ 84652ms (file: 576-765-312-136-136-316 753-375_W.mp3)
  Segment 18: 85652ms ~ 86228ms (file: 751-751-756-576-567-517 574-574-574-574-574-574-574-574-574-571-574-571-574-574-574-574-574-571-574-571-571 574-576-574-574-571-571-751-576-751-753-751-751-753-675-675 574-576-5_W.mp3)
  Segment 19: 87228ms ~ 87804ms (file: 756-765-157-571-165-135-257-752-756-760 756-127-623-237-372-365-621-756-136-327-756-317-261-132-135-136-253-132-276-627-623-627-573-702-271-326-621-236-621-752-136-321-321-132-3_V.mp3)
  Segment 20: 88804ms ~ 89380ms (file: 751-571-751-751-571-571-573-573-571-576-756-756 570-570-157-571-517-570-157-750-157-750-517-517-570-713-715-571-751-753-735-157-713-715-750-571-570-571-750-570-751-576-576-576-5_V.mp3)
  Segment 21: 90380ms ~ 96608ms (file: 312-216-216-213-123-163-726-327-263-216-710-276-327-362-106-613-621-627-317-237-276-627-163-160-627-620-137-260-726-726-267-267-627-267-627-267-263-132-213-123-312-326-236-632-1_W.mp3)
  Segment 22: 97608ms ~ 107256ms (file: 136-165-137-362-301-103-301-312-130-301-126-162-106-160-610-617-631-176-167-176-361-602-230-267-103-231-230-215-567-130-213-132-321-103-132-301-260-103-230-260-132-321-206-201-1_W.mp3)
  Segment 23: 108256ms ~ 108832ms (file: 574-574-574-574-571-574-574-570-574-570-570-571-574 574-574-574-574-574-574-574-574-574-574-574-574-574-570-574-574-573-570-570-574-574-574 574-574-574-574-574-574-574-574-570 5_W.mp3)
  Segment 24: 109832ms ~ 116240ms (file: 301-230-320-231-321-130-230-102-102-625-126-612-123-326-361-230-310-231-267-136-130-103-312-132-726-132-103-106-106-136-631-130-103-107-301-230-230-320-130-132-102-310-320-312-3_V.mp3)
  Segment 25: 117240ms ~ 124908ms (file: 756-572-310-130-721-726-671-612-267-312-570-750-217-327-207-627-627-752-137-760-276-271-216-731-726-571-726-276-512-617-176-751-106-670-267-572-756-357-351-752-673-265-213-321-5_W.mp3)
  Segment 26: 125908ms ~ 126484ms (file: 657-651-652-126-153-167-152-165-651-576-567-562-256-216-576-567-657-567-752-657-756-651-657-621-265-567-125-165-167-165-657-576-621-651-163-162-136-162-163-162-615-625-126-216-1_W.mp3)
  Segment 27: 127484ms ~ 128060ms (file: 657-675-632-675-657-653-653 165-236-562 216-257-236-275-321-163-237-571-372-137-672-275-576-251-216-256-752-623-326-362-570-325-203-762-127-267-527-572-726-706-267-576-231-312-5_V.mp3)
  Segment 28: 129060ms ~ 129636ms (file: 571-571-570-751-751 574-576-571-570-571-153-175-750-175-517-173-751-753-715-513-531-571-571-571-571 574-517-571-571-571-571-751-570-517-751-513-157-537-750-517-571-571-751 576-5_V.mp3)
  Segment 29: 130636ms ~ 138592ms (file: 375-357-317-301-236-267-517-267-312-321-362-632-762-106-107-136-167-312-137-301-301-315-132-132-107-130-257-157-267-360-321-275-317-267-312-723-726-157-263-103-157-312-136-137-1_W.mp3)
  Segment 30: 139592ms ~ 140168ms (file: 715-753-735-753-157-153-135-157-210-132-325-157-351-361-350-357_W.mp3)
  Segment 31: 141168ms ~ 141744ms (file: 713-710-571-571-570-753-751-751-571-570-752-750 574-571-570-571-571-571-751-570-571-517-571-751-571-573-157-571-751-571-751-751-753-571-517 574-576-576-576-571-576-571-517-513-5_W.mp3)
  Segment 32: 142744ms ~ 143320ms (file: 762-572-126-615-762-675-756-175-127-126-137-723-137-137-537-372-516-316-273-361-657-732-267-576-732-705-576-573-537-632-507-576-567-326-356-570 753-562-216-576-263-326 713-325-2_V.mp3)
  Segment 33: 144320ms ~ 144896ms (file: 705-710-701-710-710-217-107-312-130-103-132-123 120-120-132-132-132-135-173-107-170-170-710-107-170-710-172-210-702-127-170-170-107-102-120-123-126-162-162-162-106-134-103-107-1_V.mp3)
  Segment 34: 145896ms ~ 150756ms (file: 752-761-756-376-765-130-167-160-675-236-321-267-267-261-572-120-210-271-107-163-361-316-613-316-173-163-137-750-106-271-175-174-571-175-321-162-716-756-103-250-103-571-576-765-1_W.mp3)
  Segment 35: 151756ms ~ 152332ms (file: 573-574-574-571-571-572-574-573-574-574-574-576-571-572 576-576-576-572-573-572-571-573-715-571-751-753-571-573-756-517-751-175-576-751-753 574-574-574-574-574-572-574-574-576-5_W.mp3)
  Segment 36: 153332ms ~ 153908ms (file: 316-312-312-325-732-372-371-317-317-301-306-307-136-132-231-321-362-361-312-301-163-136-157-316-630-306-605-652-302-165-301-365-301-163-307 725-752-725-712-271-137-752-752 752-7_V.mp3)
  Segment 37: 154908ms ~ 155484ms (file: 571-570-574-574-574-574 570-570-574-571-574-571-570-570-574-570-570-574-574-574-574 574-571-574-574-574-571-574-571-571-576-576-571-574-571-571-572-571-571-571-571-574-572 574-5_W.mp3)
  Segment 38: 156484ms ~ 163828ms (file: 170-236-762-276-276-263-137-302-203-270-236-206-206-574-527-703-307-276-267-207-132-507-132-756-726-267-267-570-572-260-702-260-206-275-207-127-321-206-260-213-137-237-173-570-2_W.mp3)
  Segment 39: 164828ms ~ 174404ms (file: 267-217-136-316-752-510-130-531-132-310-320-103-103-301-312-137-716-136-136-617-167-670-627-236-136-132-312-260-316-312-231-256-215-570-206-316-120-326-213-236-203-260-201-627-2_W.mp3)
  Segment 40: 175404ms ~ 184296ms (file: 132-721-765-507-627-315-170-375-301-317-302-312-357-352-513-527-230-203-750-752-752-321-132-720-725-320-576-263-752-267-316-621-657-756-756-265-527-236-157-126-157-574-275-267-2_W_stt_some_of_the_threats_from_the_three_of_th.mp3)
  Segment 41: 185296ms ~ 190408ms (file: 216-170-627-216-276-276-267-321-312-321-106-761-627-537-357-267-276-237-623-267-206-276-705-756-276-276-276-162-102-276-106-267-260-260-270-320-276-250-165-672-235-207-120-213-5_W.mp3)
  Segment 42: 191408ms ~ 191984ms (file: 137 671-176-517-367-135-163-523-571-756-756-762-673-760-673-672-765-726-605-637-631 610-302-270-726-673-637-673-167-715-376 176-267-635-523-136 310-130-136-316-136-316-706-372-7_W.mp3)
  Segment 43: 192984ms ~ 193560ms (file: 137-135-175-754-751-576-571-574-751-751-175-173-175-715-175-137-175 571-574-756-175-715-157-751-175-157-175-175-175-175-137 135-175-175-716-137-715-175-175-175-157-157-175-157-1_W.mp3)
  Segment 44: 194560ms ~ 200032ms (file: 362-275-762-563-753-375-132-723-750-375-265-527-210-672-567-162-573-276-657-217-520-123-216-350-205-572-256-156-267-230-213-621-316-276-263-276-267-763-371-735-725-765-763-316-2_W.mp3)
  Segment 45: 201032ms ~ 201608ms (file: 157-756-561-157-156-165-176-156-615-651-512-165-567-657-165-156-152-156-561-576-652-657-567-152-156-260-625-175-135-156-651-165-657-567-756-657-567-576-562-652-516-576-567-576-5_W.mp3)
  Segment 46: 202608ms ~ 203184ms (file: 157-157-517-157-157-517-517-517-576-517-571-576-571-135-315-103-103-135-137-135-137-103-107-175-157-157-175-157-157-156-157-157-157-517-157-571-157-517-103-103-157-157-157-175-1_V.mp3)
  Segment 47: 204184ms ~ 204760ms (file: 103-130-136-236-321-123-126-216-754-623-576-576-627-231-362-574-130-136-576-637-316-106-103-263-316-263-672-674-672-675-627-213-163-627-675-627-126-130-367-627-130-310-621-236-5_W.mp3)
  Segment 48: 205760ms ~ 206336ms (file: 157-137-103-301-576-675-671-675-762-765-765-761_W.mp3)
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
> **Time**: 2026-04-06 08:52:53
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "안녕하세요 만나서 정말 반갑습니다 오늘 기분이 어떠신가요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 320,
"audio_type": "speech",
"words":대답이 중지되었습니다.

---
*Generated by WKAppBot ask gemini — 2026-04-06 08:52:53*
