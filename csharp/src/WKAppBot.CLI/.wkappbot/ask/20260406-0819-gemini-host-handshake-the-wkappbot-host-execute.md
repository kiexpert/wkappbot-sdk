# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=36536  time=08:17:57  uptime=8558s  mem=389MB  system_processes=409"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_081711.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 135-107-713-751-763-761-756-763-765-735-172-573-756-576-576-675-671-761-617-601-760-761-756-657-107-162-316-137-376-362-126-132-213-126-752-617-167-167-167-135-716-710-170-731-1_V.mp3)
  Segment 2: 1576ms ~ 4672ms (file: 567-750-315-576-762-572-657-520-263-762-527-502-531-675-632 756-524-236-572-576-257-352-321-571-521-372-237-132-276-752-574-752-236-267-572-156_V.mp3)
  Segment 3: 5672ms ~ 13772ms (file: 206-120-201-201-123-102-317-175-231-206-132-576-750-627-756-213-621-570-520-201-201-207-276-260-267-210-207-273-725-217-752-612-627-231-237-130-312-620-260-620-627-620-173-267-2_W.mp3)
  Segment 4: 14772ms ~ 21828ms (file: 231-126-163-230-203-103-312-324-316-631-623-627-102-231-301-765-320-321-103-320-302-301-321-321-203-230-103-103-127-217-217-312-312-320-321-301-357-305-350-302-301-130-310-137-3_V.mp3)
  Segment 5: 22828ms ~ 23404ms (file: 506-563-576-621-276-236-236-325-456-327-620-126-267-321-317-657-572-507-257-357-750-765-657-605-132-572-576-275-537-327-263-132-752-752-570-273-527-527-567-752-721-267-265-627-7_V.mp3)
  Segment 6: 24404ms ~ 24980ms (file: 756-751-750-175-715-753-751-175-715-751-751-750 617-612-617-671-107-175-107-701-170-176-716-710-750-750-715-751-175-107-170-715-107-170-750-701-750 167-671-706-705-706-701-750-7_V.mp3)
  Segment 7: 25980ms ~ 26556ms (file: 574-547-574-574-517-157-157-571-571_W.mp3)
  Segment 8: 27556ms ~ 33784ms (file: 756-102-312-276-132-320-317-206-735-756-627-257-206-263-123-607-260-267-263-167-627-302-507-132-265-103-236-506-206-321-203-230-752-756-273-270-756-275-276-213-213-761-213-231-2_W.mp3)
  Segment 9: 34784ms ~ 35360ms (file: 512-157-752 572-572-571-751-571-751-571-715-756-752 150-510-517-157-571-150-157-571-157-175-503-315-157-571-715-170-571-753-175-175-135-317-130-712-371-375-371-710-713-357-756-7_V.mp3)
  Segment 10: 36360ms ~ 36936ms (file: 173-673-365-367-326-365-360-360-672-367-301-362-652-360-362-375-351-352-325-321_V.mp3)
  Segment 11: 37936ms ~ 38512ms (file: 514-541-165-516-156-516-567-156-157-613-612_W.mp3)
  Segment 12: 39512ms ~ 48764ms (file: 130-312-132-206-103-143-130-123-126-216-210-301-321-327-102-213-135-213-230-302-320-321-130-230-231-230-123-261-756-765-260-206-206-321-106-167-106-106-201-130-730-627-261-576-2_W.mp3)
  Segment 13: 49764ms ~ 50340ms (file: 675-672-762-763-574-574-510-571-657-675-576-175-156-165-617-156-123 612-627-672-672-672_W.mp3)
  Segment 14: 51340ms ~ 59728ms (file: 271-231-174-130-237-572-210-230-371-321-132-752-275-137-132-721-715-201-217-102-132-103-107-756-716-312-154-312-167-156-517-574-574-571-574-265-254-321-210-236-236-162-132-103-1_W.mp3)
  Segment 15: 60728ms ~ 69476ms (file: 310-673-576-351-753-375-753-361-137-572-572-652-276-732-715-301-136-236-172-103-721-132-312-130-632-731-523-627-756-103-106-310-567-241-574-130-301-320-231-312-321-130-750-137-1_W.mp3)
  Segment 16: 70476ms ~ 76848ms (file: 174-321-231-312-132-130-103-137-175-231-103-312-201-302-320-207-267-267-547-210-127-126-126-120-126-216-216-201-120-120-126-126-613-162-162-126-120-321-236-127-123-126-126-276-3_W.mp3)
  Segment 17: 77848ms ~ 84328ms (file: 260-261-132-210-267-301-321-263-312-213-260-267-206-316-310-361-301-236-263-263-237-237-316-307-601-165-517-375-236-763-260-762-716-107-106-107-157-352-176-107-137-216-731-710-2_W.mp3)
  Segment 18: 85328ms ~ 93608ms (file: 267-372-263-210-627-263-126-236-231-236-231-102-143-236-637-321-316-765-627-627-765-670-602-162-106-152-625-103-103-103-231-123-216-326-137-167-263-263-320-316-312-164-263-326-3_W.mp3)
  Segment 19: 94608ms ~ 95184ms (file: 751-571-571-751 571-571-571-571-573-571-571-571-571-571-517-571-571-571-574-574-571-574-574-517 574-574-574-574-574-574-574-574-574-574-571-571-574 570-573 571-547-547 574-574-5_W.mp3)
  Segment 20: 96184ms ~ 96760ms (file: 675-673-673-675-765-675-765-657 675 756-751-756_W.mp3)
  Segment 21: 97760ms ~ 98336ms (file: 123 107-175-107-105-105 135-301-317-713-716-761-765-137-176-763-712-176-765-765-752-675-671-671-675-675-713-673-716-673-736-163-712-167-157-163-163-173-376-672-765-762-736-675-3_W.mp3)
  Segment 22: 99336ms ~ 99912ms (file: 510-517-571-571-157-517-175-157-751-570-751-517-750-753-750-750-576-571 571-570-510-571-571-157-751-750-150-157-571-751-751-751-570-571-175 574-574-574-574-574-574-574-574-570-5_W.mp3)
  Segment 23: 100912ms ~ 101488ms (file: 132-123-120-102-130-312-123-132-132-123-102-103-103-130-102-103-102-105-120-125-125-152-517-517-517-152-157-153-152-135-132-162-126-126-162-123-213-123-120-132-120-123-120-120-1_V.mp3)
  Segment 24: 102488ms ~ 107420ms (file: 362-723-301-127-206-102-263-267-265-230-173-756-705-607-176-705-320-321-203-230-236-235-570-106-127-271-352-372-236-237-267-267-275-756-756-206-756-132-105-360-531-231-271-756-2_W.mp3)
  Segment 25: 108420ms ~ 108996ms (file: 620-736-736-716-715-716-761-176-176-671-163 765-763-761-716_W.mp3)
  Segment 26: 109996ms ~ 117448ms (file: 302-273-156-137-107-175-712-627-327-137-132-210-201-320-301-312-576-627-130-302-320-201-302-231-236-203-206-207-312-132-317-326-320-301-210-213-132-310-260-123-132-321-130-176-5_V.mp3)
  Segment 27: 118448ms ~ 127520ms (file: 632-756-632-612-106-754-103-163-321-312-735-102-725-312-130-321-312-321-231-321-231-130-231-123-217-231-127-574-574-107-130-130-320-754-320-301-310-123-617-132-321-375-102-125-1_W.mp3)
  Segment 28: 128520ms ~ 136764ms (file: 103-321-130-103-103-130-317-756-321-103-256-527-207-203-235-670-267-765-762-210-527-103-571-230-301-301-721-130-756-576-273-134-310-321-123-316-137-310-301-103-210-201-107-576-5_W.mp3)
  Segment 29: 137764ms ~ 138340ms (file: 376-736-315-123-673-765-675-675-675-675-765-756-756-753-751-375-625-612-136-136-316-136-136-136-136-675-675-675-657-761-675-671-173-673_W.mp3)
  Segment 30: 139340ms ~ 147440ms (file: 750-106-253-263-257-350-321-237-752-316-361-637-357-537-567-302-103-310-136-132-325-631-657-265-325-301-326-325-235-263 357-130-236-326-263-357-537-326-273-263-231-132-135-157-2_W.mp3)
  Segment 31: 148440ms ~ 149016ms (file: 157-517-175-571-517-571-572-130 153-175-513-153-175-751-702-725-703-701-120-173-107-137-132-172-102-130-127-102-135-153-172-371-173-372-731-172-173-132-132-312-371-751-571-157-1_W.mp3)
  Segment 32: 150016ms ~ 150592ms (file: 715-751 571-750-570-517-571-751-570-170-175-571-170-170-731-751-175-731-351-175-705-701-170-751-710-135-173-701-170-157-715-571-750-175 751-715-750-703-170-715-175-107-130-107-1_V.mp3)
  Segment 33: 151592ms ~ 158936ms (file: 527-521-320-235-257-275-720-752-203-201-752-503-562-107-317-571-571-267-162-704-260-732-213-602-610-105-130-263-132-163-675-657-657-236-752-326-275-257-265-236-321-102-162-136-1_W.mp3)
  Segment 34: 159936ms ~ 160512ms (file: 726-276-273-752-235 716-713-176-761-163-176-167-173-317-173-261-203-153-107-173-137-136-136-132-130-130-103-106-132-543-574-574-574_W.mp3)
  Segment 35: 161512ms ~ 169900ms (file: 170-371-725-275-125-103-275-271-236-137-713-763-632-132-275-765-167-106-102-127-617-612-321-321-132-132-137-736-703-163-103-130-236-253-257-132-136-632-726-217-271-263-102-132-1_W.mp3)
  Segment 36: 170900ms ~ 171476ms (file: 267-123-570-157-502-571-217-567-135-712-514-572-175-173-576-126-756-257-275-627-571-750-175-137-705-167-572-715-367-726-275-752-754-230-372-751-357-576-276-517-321-572-213-276-2_V.mp3)
  Segment 37: 172476ms ~ 182628ms (file: 132-273-267-267-267-103-107-570-256-263-130-162-260-761-130-267-612-130-216-260-216-167-267-371-710-735-103-752-213-305-165-120-160-160-713-130-126-172-176-317-167-261-601-231-2_W.mp3)
  Segment 38: 183628ms ~ 184204ms (file: 706-716-715-175-105-170-710-710-137-751-710-170-175-170-710-175-751-715-175-173-715-175-170-715-750 756-601-167-170-710-716-173-170-107-715-170-715-175-175-173-175-713-175-175-1_V.mp3)
  Segment 39: 185204ms ~ 185780ms (file: 617-567-156-517-567-576-156-167-576-657-126-165-123-276-675-163-356-302-671-617-170-175-571-751-157-175-571-517-751-657-567-567-675-657-567-576-567-156-157-576-765-765-657-576-5_W.mp3)
  Segment 40: 186780ms ~ 187356ms (file: 357-317-315-305-317-310-130-132-170-673-367-371-367-237-637-372-637-276-623-631-632-263-376-317-617-627-276-673-673-376-135-675-756 753-756-753-753-765_W.mp3)
  Segment 41: 188356ms ~ 188932ms (file: 157-153-157-157-150-105-157-157-157-153-150-715-135-715-310-153-571-170-106-735-673-675-673-657-375-675-136-137-130-135-135-657-536-573-513-572-513-135-157-153-152-271-125-175-1_W.mp3)
  Segment 42: 189932ms ~ 199292ms (file: 102-103-172-176-607-263-201-216-102-312-271-207-237-103-167-162-623-216-123-132-207-270-107-132-312-143-132-271-621-627-673-362-312-123-731-376-130-537-752-370-137-143-102-175-1_W.mp3)
  Segment 43: 200292ms ~ 207384ms (file: 270-102-275-230-276-276-752-725-672-302-547-315-237-271-107-217-207-547-157-267-216-206-261-320-276-236-201-542-321-103-261-257-213-213-210-267-127-276-726-726-276-276-206-542-2_W.mp3)
  Segment 44: 208384ms ~ 208960ms (file: 625-625-625-625-625-625-625-265-265-621-261-263-621-216-120_W.mp3)
  Segment 45: 209960ms ~ 220004ms (file: 157-102-756-213-627-260-263-210-206-756-270-276-132-260-103-320-310-310-312-102-201-756-754-675-321-236-103-143-261-123-210-752-107-130-210-571-571-206-573-130-132-163-210-321-3_W.mp3)
  Segment 46: 221004ms ~ 228312ms (file: 132-103-723-276-130-123-627-623-127-210-215-162-271-213-627-576-213-213-237-106-312-263-103-513-510-312-572-213-236-627-352-132-316-216-216-120-263-260-321-360-213-217-271-273-2_W.mp3)
  Segment 47: 229312ms ~ 234784ms (file: 103-136-130-102-312-132-230-236-721-273-273-273-276-732-372-723-312-312-317-103-310-310-172-102-326-312-721-312-713-312-312-217-317-231-123-751-517-315-375-132-130-312-172-310-3_W.mp3)
  Segment 48: 235784ms ~ 245828ms (file: 132-720-370-237-203-273-705-750-103-136-573-153-135-213-321-130-103-321-317-310-264-261-213-321-573-530-270-721-701-570-375-236-675-321-236-235-203-327-237-237-710-107-153-367-3_W.mp3)
  Segment 49: 246828ms ~ 247404ms (file: 205-215-752-562-567-570-532-762-752-576-627-675-576-326-367-506-135-276-375-576-574-627-572-623-573-235-537-270-675-173-153-267-756-361-275-567-526-265-564-657-261-351-572-265-7_V.mp3)
  Segment 50: 248404ms ~ 248980ms (file: 652-576-263-126-526-675-362-231-764-527-652-305-276-623-361-631-635-651-653-316-631-163-137-132-761-367-763-765 756-753-257-516-514-157_W.mp3)
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
> **Time**: 2026-04-06 08:19:10
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "이거 짓는 데는 얼마 들었을까요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "이거", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "짓는", "start_ms": 170, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "데는", "start_ms": 270, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "얼마", "start_ms": 370, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "들었을까요?", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "대략 2억 정도. 정리해 놓은 거로는 1억 6천까지는 정리가 돼 있는데 그 이후...",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "대략", "start_ms": 50, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "2억", "start_ms": 300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "정도.", "start_ms": 600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "정리해", "start_ms": 1100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "놓은", "start_ms": 1450, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "거로는", "start_ms": 1600, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "1억", "start_ms": 1850, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "6천까지는", "start_ms": 2050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "정리가", "start_ms": 2450, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "돼", "start_ms": 2700, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있는데", "start_ms": 2800, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 3000, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "이후", "start_ms": 3050, "dur_ms": 46, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "(music)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 4,
"transcript": "Okay, that's it. Okay. Rise and shine.",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "Okay,", "start_ms": 200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "that's", "start_ms": 800, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "it.", "start_ms": 1100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "Okay.", "start_ms": 2500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "Rise", "start_ms": 5800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "and", "start_ms": 6200, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "shine.", "start_ms": 6400, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "램이 48기가거든요. 요즘 온디바이스 AI로 돌릴 때 가장 많이 쓰는 디바이스 중 하나가 또 이런 맥 제품들이기 때문에 요걸로 실제로 테스트...",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "램이", "start_ms": 30, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "48기가거든요.", "start_ms": 330, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "무엇보다 중요했던 것은 손흥민과 부항가 교우가 경기 감각을 되찾은 것이라며 그들은 올 시즌 초반보다 더 많은 득점 기회를 만들어낼 수...",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "무엇보다", "start_ms": 50, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "중요했던", "start_ms": 450, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 8,
"transcript": "이게 마냥 좋은 건지, 꼭 그렇지는 않거든요. 특사경이라는 게 굉장히 역사가 다른 수사 기관에 비해서는 짧고 독특한 제도이기 때문에 대한민국에서는 이게 100대 0의 게임이 아닙니다. 검사의 수사지휘 권한을 삭제함으로써 나타날 수 있는 어떤 그 문제점들이 분명히 있습니다. 특사경 제도...",
"speaker": "speaker_6",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "이게", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "마냥", "start_ms": 190, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 390, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "건지,", "start_ms": 590, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 10,
"transcript": "국무부, 핵심 정보기관들이 판단하는 거죠. 승인 조건은 사실상 핵심은 기술 이전 제한, 미국 내 생산 유지.",
"speaker": "speaker_7",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "국무부,", "start_ms": 30, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "핵심", "start_ms": 380, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "(music)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 12,
"transcript": "아 그러지 않고? 엄청 셌어요. 여기 먹어야 했나? 아 그래요. 식초랑 설탕을 절대 안 써요. 아 그래요? 알겠습니다. 감사합니다.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "아", "start_ms": 100, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "그러지", "start_ms": 200, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "않고?", "start_ms": 400, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 14,
"transcript": "분담금을 납부하지 않고 있었는데 나중에는 그걸 식용유로 대신 주면 어떻겠냐고 묘한 딜을 걸어왔고요. 또 그 와중에 인도네시아에서 파견한 기술자가 USB에 KF-21 관련 기밀을 훔쳐...",
"speaker": "speaker_7",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "분담금을", "start_ms": 40, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "납부하지", "start_ms": 440, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "사라졌답니다. 인허가 비용 이런 거 기본적으로 500에서 1000만 원 정도 이렇게 나오더라고요. 그럼 그 돈 필요해서 열심히 벌었어요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "사라졌답니다.", "start_ms": 50, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "인허가", "start_ms": 1800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "비용", "start_ms": 2200, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "I'm a little bit on the same side as Papa. Too much of the pickled radish or something. Oh, okay. But I did like it. Of course the ddukbokki is always a favorite.",
"speaker": "speaker_8",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "I'm", "start_ms": 150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 300, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "little", "start_ms": 400, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "I liked this a lot. And I think it's...",
"speaker": "speaker_9",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "liked", "start_ms": 250, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "this", "start_ms": 450, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "어 진짜? 맞네. 맞네. 아이고 아이고. 근데 그거 어떻게 영국에서 알게 됐어요 하리나 씨는?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "어", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "진짜?", "start_ms": 150, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": "20",
"transcript": "(noise)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 21,
"transcript": "면회할 수 있는 시간도 정해져 있거든요. 보호자가 또 옆에 있을 수가 없고 항상 딱 시간이 정해져 있고. 그래서 이제 와이프는 그 안에서 지금 언제 수술할지도 모르고 지금...",
"speaker": "speaker_10",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "면회할", "start_ms": 50, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 400, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 500, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 23,
"transcript": "보세 보세요. 아 무서워 비켜. 다 팔라고. 제가 갖고 있는 거야. 또 이거 먹는 건데. 뭐 먹고 싶은데?",
"speaker": "speaker_11",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "보세", "start_ms": 30, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "보세요.", "start_ms": 230, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "삼각형처럼 된 거지? 자 이거 한번 물어봐야 되겠네. 계세요. 계십니까? 어 안녕하세요. 아 반갑습니다. 안녕하세요.",
"speaker": "speaker_12",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "삼각형처럼", "start_ms": 40, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "된", "start_ms": 540, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "거지?", "start_ms": 640, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 26,
"transcript": "사이 저 사이 BTS에요. 왜냐면 저도 강남스타일 저도 들어봤어 사이. 저도 싸이 싸이 싸이 들어봤는데 근데 관심 생긴 거...",
"speaker": "speaker_13",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "사이", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "저", "start_ms": 250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "사이", "start_ms": 350, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "BTS에요.", "start_ms": 550, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "치킨도 먹고. 우와 너무 괜찮은데? 그래서 되게 큰 축제예요. 그때는 축제 기간 동안에는 맥주가 단돈 천 원 천 원. 천 원? 천 원? 일 유로보다 저렴해요 진짜로.",
"speaker": "speaker_14",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "치킨도", "start_ms": 40, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "먹고.", "start_ms": 440, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "우와", "start_ms": 1000, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 1200, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "괜찮은데?", "start_ms": 1450, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "미국과의 안보 관계 사이에서 절묘한 줄타기를 하고 있습니다. 이런 상황에서 대만 문제는 부차적인 외교 사안일 수밖에 없습니다. 결국 이러한 국제 정세를 무시하고 대만 정부가 강경 대응을 반복...",
"speaker": "speaker_15",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "미국과의", "start_ms": 30, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "안보", "start_ms": 430, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 30,
"transcript": "모습 중에 강했던 종목 같은 경우는 시장이 하락을 멈추거나 약간만 반등하려고 하더라도 급등하는 경향이 있어서 오히려 시장...",
"speaker": "speaker_16",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "모습", "start_ms": 40, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "중에", "start_ms": 340, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 32,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 33,
"transcript": "정도로 누가 봐도 지금 인도네시아는 너무도 급하게 이 KF-21 관련 계약을 하씨 이전 수준으로 되돌리고 싶어 하는 것 같은 모습을 보이고 있는데 하지만...",
"speaker": "speaker_15",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "정도로", "start_ms": 50, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "누가", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 35,
"transcript": "이렇게 낼 수 있었습니다. 그래서 제가 하는 이런 D+ 주도주 매매 같은 경우는 시장이 횡보하는 상황이나 또 상승하는 상황...",
"speaker": "speaker_16",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "이렇게", "start_ms": 40, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "낼", "start_ms": 340, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 37,
"transcript": "터가 중요한데 목요일에 10시에 트럼프 연설 이후로 지수가 굉장히 가파르게 하락했습니다. 대한광통신과 코스피 지수...",
"speaker": "speaker_16",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "터가", "start_ms": 40, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "중요한데", "start_ms": 290, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 39,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 40,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 41,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 42,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 43,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 44,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 45,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 46,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 47,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 48,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 49,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 50,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 08:19:10*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=15840  time=08:19:10  uptime=13221s  mem=454MB  system_processes=413"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_081513.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 6804ms (file: 537-103-213-130-312-301-321-132-130-162-137-756-231-210-206-752-132-537-517-231-267-763-310-321-123-102-326-236-276-752-237-213-756-320-756-176-107-715-201-216-237-321-753-576-2_W.mp3)
  Segment 2: 7804ms ~ 8380ms (file: 675-657-357-517-516-517-751-571-576-517-153-163-316-163-361-137-163-637-731-317-713-316-637-163-163 657 352-321-132-102-576-356-635-361-317-317-317_W.mp3)
  Segment 3: 9380ms ~ 9956ms (file: 107-107-715-170-701-710-713-715-716-175-715-710 167-175-107-175-157-107-710-170-175-715-715-715-715-175-175-715-175-715-175-175 107-670-751-176-715-107-706-175-716-173-170-175-1_V.mp3)
  Segment 4: 10956ms ~ 19416ms (file: 327-763-647-327-312-102-123-715-137-236-761-735-753-102-710-501-751-713-172-123-132-172-123-123-102-130-270-213-123-106-312-316-136-137-107-130-206-201-230-230-413-102-102-702-2_W.mp3)
  Segment 5: 20416ms ~ 31576ms (file: 102-130-210-210-107-160-260-216-120-106-326-231-231-213-230-574-571-320-106-326-362-675-136-175-517-325-125-260-310-103-213-102-102-203-132-102-132-143-104-210-574-571-625-261-6_W.mp3)
  Segment 6: 32576ms ~ 33152ms (file: 736-763-763-763-716-761-756-371-762-763-736-763-763-763-167-756-736-376-367-317-751-157-157-571-125-165-132-137-263-216-271-273-751-157-136-123-765-576-275-623-756-726-527-203-2_V.mp3)
  Segment 7: 34152ms ~ 34728ms (file: 135-312-310-132-130-316-176-167-153-367-316-507-506-315-157-315-301-317-310-317-312-312-136-132-136-312-132-136-312-317-132-137-317-317-137-317-132-132-574-375-751-573-135-130-3_W.mp3)
  Segment 8: 35728ms ~ 36304ms (file: 627-236-276-756-572-576-571-276-265-253-657-570-756-627-675-502-273-507-325-526-326_V.mp3)
  Segment 9: 37304ms ~ 41660ms (file: 120-120-102-120-201-213-102-102-123-126-120-120-210-213-123-216-213-126-102-201-126-120-120-210-120-210-123-123-102-123-312-321-123-312-310-312-132-123-210-102-210-231-123-203-2_V.mp3)
  Segment 10: 42660ms ~ 43236ms (file: 617-326-752-732-362-570-275-726-271-320-362-372-523-103-237-507-315-326-623-312-321-632-236-237-362-263-325-752-257-572-752-216-736-267-326-627-326-624-576-627-527-657-325-750-6_V.mp3)
  Segment 11: 44236ms ~ 44812ms (file: 547-675-156-102-253-312-315-352-257-132-310-320-103-103-630-362-160-132-173-163-106-265-167-176-361-163-631-613-613-367-673-576-576-672-672-657-756-263-657-672-623-672-627-132-3_W.mp3)
  Segment 12: 45812ms ~ 55856ms (file: 157-102-756-213-627-260-263-210-206-756-270-276-132-260-103-320-310-310-312-102-201-756-754-675-321-236-103-143-261-123-210-752-107-130-210-571-571-206-573-130-132-163-210-321-3_W.mp3)
  Segment 13: 56856ms ~ 57432ms (file: 567-752-567-576-751-156-647-672-175-375-376-716-317-712-176-627-625-617-625-613-672-675-617-672-761-172-167-637-612-762-637-672-627-673-637-637-671-762-372-165-671-756-765-756-7_W.mp3)
  Segment 14: 58432ms ~ 65488ms (file: 316-310-132-137-135-156-107-103-321-527-413-752-321-312-301-256-125-120-123-265-312-206-126-302-130-675-263-310-103-301-237-325-567-657-501-573-765-217-321-236-237-321-201-267-2_W.mp3)
  Segment 15: 66488ms ~ 67064ms (file: 130-130-103-130-130-651-576-265-206-215-657-562-627-650-567-675-657-516-615-156-216-765-657-136-106-103-130-165-576-125-120-126-357-135-163-136-132-160-130-162-612-120-201-165-1_W.mp3)
  Segment 16: 68064ms ~ 68640ms (file: 157-571-571-751-571-715-517-715-517-157-517-571-571-571 571-570-570-571-571-150-571-517-571-157-507-157-751-715-715-751-571-751-157-571-517-157-571-570-750-576-571-751-576-751-5_V.mp3)
  Segment 17: 69640ms ~ 76264ms (file: 163-231-321-236-231-103-163-237-751-571-351-106-120-201-260-612-162-167-216-213-627-376-312-310-263-310-672-627-620-326-325-367-106-761-671-163-106-620-310-637-275-765-376-310-1_W.mp3)
  Segment 18: 77264ms ~ 77840ms (file: 267-503-312-567-362-315-357-317-315-750_W.mp3)
  Segment 19: 78840ms ~ 84672ms (file: 172-206-271-103-173-765-301-103-310-130-103-526-276-201-270-276-203-275-750-270-267-271-130-327-317-136-103-103-231-120-756-725-130-301-623-706-256-567-315-310-256-276-301-573-6_W.mp3)
  Segment 20: 85672ms ~ 86248ms (file: 576-157-150-156-157-615-571-156-105-175-756-157-517-715-751-765-165-175-751-705-107-175-356-156-157-517-571-567-657-567-567-675-675-567-576-576-675-157-571-576-756-756-576-576-5_W.mp3)
  Segment 21: 87248ms ~ 98408ms (file: 372-574-527-256-576-761-630-273-236-576-573-173-317-276-327-321-273-236-230-326-263-173-571-236-326-302-237-127-137-103-327-315-321-163-231-325-130-574-301-351-320-173-236-752-3_W.mp3)
  Segment 22: 99408ms ~ 99984ms (file: 152-156-175-135-315-135-312-132-136-135-130-103-301-315-135-315-103-103-150-135-130-310-130-105-153-157-150-150-516-157-156-517-157-157-517-157-157-156-167-156-130-135-130-130-1_V.mp3)
  Segment 23: 100984ms ~ 104260ms (file: 351-576-261-563-657-632-613-216-315-132-312-123-123-132-123-316-136-315-563-536-315-135-315-365-316_W.mp3)
  Segment 24: 105260ms ~ 105836ms (file: 574-571-572-574-574-574-573-573-572-574-576-573-576 561-517-571-517-517-576-513-157-753-573-153-571-573-576-571-715-571-571-751-571-751-753-751-571-751-756 571-572-576-572-572-5_W.mp3)
  Segment 25: 106836ms ~ 107412ms (file: 517-571-105-105-150-310 517-517-153-137-157-517-513-175-517-571-571-152-517-752-175-105-517-153-513-175-571-157-107-507-571-150-157 517-510-150-103-130-735-503-517-521-571-167-1_V.mp3)
  Segment 26: 108412ms ~ 115108ms (file: 213-576-627-107-312-132-132-310-132-310-312-107-267-720-735-172-706_W.mp3)
  Segment 27: 116108ms ~ 116684ms (file: 517-506-517-157-710-123-132-312-132-712-715-176-675-167-367-671-753-371-153-316-165-675-617-763-163-312-126-167-107-130-163-716-173-716-375-237-371-637-375-327-326-376-327-326-3_W.mp3)
  Segment 28: 117684ms ~ 118260ms (file: 517-571-751-750-351-713-750-753-730-713-701-103-102-762-172-756-701-753-507-706-270-107-721 576-571-753-571-751-507-170-735-735-573-751-701-572-570-570-750-753-571-756-571-576-5_V.mp3)
  Segment 29: 119260ms ~ 125416ms (file: 276-725-276-310-263-130-760-176-106-361-267-276-756-752-176-106-237-273-237-130-107-316-267-267-126-102-102-712-103-231-276-260-263-107-612-750-617-167-276-752-276-326-106-176-7_W.mp3)
  Segment 30: 126416ms ~ 130520ms (file: 231-371-765-736-731-762-761-765-763-675-763-276-623-617-762-762-763-765-726-763-765-762-276-263-762-762-763-762-765-762-672-762-267-762-763-762-761-762-725-236-321-231-203-236-2_W.mp3)
  Segment 31: 131520ms ~ 132096ms (file: 157-157-157-157-152-152-513-157-152-132-120-120-103-102-102-126-162-162-106-153-510-157-571-153-157-571-175-175-175-157-175-751-157-175-571-751-571-571-175-175-571-157-715-715-1_V.mp3)
  Segment 32: 133096ms ~ 133672ms (file: 537-573-315-351 150-135-531-513-517-153-517-573-571-570-571-571-571-751-751-571-623 571-574-571-517-517-571-571-573-574-574-571 574-574-574-517-517-571-574 576-576-576-753-571-5_W.mp3)
  Segment 33: 134672ms ~ 142844ms (file: 103-260-620-312-602-210-103-260-230-203-270-275-106-206-607-276-130-675-237-203-273-123-260-302-260-206-726-276-320-760-260-762-754-130-627-612-102-310-231-327-123-201-271-765-2_W.mp3)
  Segment 34: 143844ms ~ 148956ms (file: 216-170-627-216-276-276-267-321-312-321-106-761-627-537-357-267-276-237-623-267-206-276-705-756-276-276-276-162-102-276-106-267-260-260-270-320-276-250-165-672-235-207-120-213-5_W.mp3)
  Segment 35: 149956ms ~ 154888ms (file: 362-723-301-127-206-102-263-267-265-230-173-756-705-607-176-705-320-321-203-230-236-235-570-106-127-271-352-372-236-237-267-267-275-756-756-206-756-132-105-360-531-231-271-756-2_W.mp3)
  Segment 36: 155888ms ~ 161720ms (file: 712-371-106-312-327-317-132-130-170-231-275-237-317-103-176-320-301-763-310-130-135-107-715-162-213-201-137-130-760-217-152-207-312-170-231-267-312-102-127-765-312-231-576-327-1_V.mp3)
  Segment 37: 162720ms ~ 163296ms (file: 601-657-735-756-176-602-617-160-573-137-251-327-271-276-162-217-231-712-703-372-123-132-301-132-123-573-312-723-352-267-725-352-257-132-536-217-137-752-672-372-705-657-213-567-1_W.mp3)
  Segment 38: 164296ms ~ 170740ms (file: 236-765-267-236-576-321-267-751-106-571-267-103-316-315-713-176-106-751-231-231-321-375-756-123-167-750-754-716-750-312-123-210-572-752-107-762-230-237-130-237-326-327-750-715-5_W.mp3)
  Segment 39: 171740ms ~ 172316ms (file: 123-361-175-763-715-137-701-571-576-165-571-103-507-351-612-123-176-716-710-715-751-715_W.mp3)
  Segment 40: 173316ms ~ 181128ms (file: 103-103-167-312-751-103-573-615-621-132-132-103-753-750-756-576-675-756-125-126-120-261-657-275-751-260-726-275-250-216-163-130-610-263-621-231-321-231-260-321-230-627-267-627-2_W.mp3)
  Segment 41: 182128ms ~ 187060ms (file: 726-276-107-610-126-132-203-627-162-105-754-372-570-572-765-307-712-267-163-302-310-120-712-102-276-567-213-126-231-321-145-752-206-102-217-267-231-713-123-132-301-137-123-317-1_V.mp3)
  Segment 42: 188060ms ~ 188636ms (file: 571-571-317-132-317-657-263-765-765-671-317_W.mp3)
  Segment 43: 189636ms ~ 190212ms (file: 751-517-751-571-751-571-751-571-157-571-570-751-750-571-571-517-751-175-571-107-517-715-751-715-750 571-571-750-570-750-153-713-175-751-705-150-517-157-751-107-175-750-705-107-5_V.mp3)
  Segment 44: 191212ms ~ 191788ms (file: 560-567-256-206-276-612-540-572-276-625-760-507-237-657-256-627-537-275-263-267-132-350-326-507-312-617-260-256-576-573-312-706-632-725-653-621-735-762-263-576-275-507-576-750 5_V.mp3)
  Segment 45: 192788ms ~ 193364ms (file: 376-736-315-123-673-765-675-675-675-675-765-756-756-753-751-375-625-612-136-136-316-136-136-136-136-675-675-675-657-761-675-671-173-673_W.mp3)
  Segment 46: 194364ms ~ 194940ms (file: 751-570-750-705-705-750-750-750-735-705-750-715-750-715-705-750-705-750 123-163-132-130-130-135-175-157-571-135-170-157-105-175-571-713-173-130-731-713-710-713-713-107 126-120-1_V.mp3)
  Segment 47: 195940ms ~ 204436ms (file: 260-213-123-231-217-267-217-230-273-217-216-203-270-726-762-270-203-231-267-130-213-301-762-310-230-213-276-267-207-230-752-260-267-276-267-271-267-201-175-310-213-267-726-267-3_W.mp3)
  Segment 48: 205436ms ~ 206012ms (file: 516-632-162-657-537-537-123-573-361-615-657-351-561-756-572-615-517-537-751-513-673-627-756-135-135-136-567-651-567-567-756-567-567-657-567-567-760-176-571-675-105-137-576-576-5_W.mp3)
  Segment 49: 207012ms ~ 216444ms (file: 102-130-571-715-102-231-102-275-712-103-257-260-752-275-230-276-312-752-217-173-127-231-713-217-765-106-102-170-751-753-753-213-201-216-627-762-276-207-206-267-132-103-210-123-1_W.mp3)
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
> **Time**: 2026-04-06 08:19:50
> **AI**: gemini

---

---
*Generated by WKAppBot ask gemini — 2026-04-06 08:19:50*
