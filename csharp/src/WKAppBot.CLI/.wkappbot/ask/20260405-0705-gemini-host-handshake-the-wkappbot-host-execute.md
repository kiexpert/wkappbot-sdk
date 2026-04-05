# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=24432  time=07:03:21  uptime=17872s  mem=479MB  system_processes=352"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_065708.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 4680ms (file: 217-276-176-726-271-527-167-520-216-267-567-627-375-315-750-236-231-627-710-312-326-267-213-503-627-326-317-567-752-560-652-625-652-752-267-265-506-506-267-602-627-167-623-206-3_W_stt_along.mp3)
  Segment 2: 5680ms ~ 6256ms (file: 571-573-765-715-157-175-107-170-107-701-107-751-170-107-750-570-571-107-105-105-175-750-576-726-627-726-762-762-672-276-762-716-736-271-726-713-763-756-725-762-517-571-750-751-5_V.mp3)
  Segment 3: 7256ms ~ 15140ms (file: 130-276-310-572-507-203-732-136-261-267-103-103-163-236-756-132-576-261-216-261-216-103-501-507-607-301-175-607-316-756-267-265-564-152-175-126-217-261-752-132-261-271-607-207-6_W.mp3)
  Segment 4: 16140ms ~ 16716ms (file: 150-105-157-513-517-150-510-715-507-150-507-570-507-510-510-517-157-517-517-547-541-547-571-574-547-574-507-574-571 507-507-751-157-571-150-517-517-510-510-150-510-501-510-517-1_W.mp3)
  Segment 5: 17716ms ~ 18292ms (file: 137-351-371-173 135-157-135-103-135-105-713-153-315-135-317-135-357-310-315-301-317-301-371-751-715-357-175-731-137-173-273-317-107-103 130-135-315-135-135-135-103-135-132-137-1_V.mp3)
  Segment 6: 19292ms ~ 23612ms (file: 236-210-126-321-213-231-302-120-126-102-213-123-263-261-263-326-236-263-126-231-216-263-621-236-632-213-621-621-216-261-213-231-261-216-216-657-261-216-216-160-216-162-621-632-1_W.mp3)
  Segment 7: 24612ms ~ 25188ms (file: 130-103-310-312-137-103-310-753-753-107-137-175-307-130-713 103-317-137-130-137-310-130-103-137-103-137-103-103-307-137-317-751-130-312-307-103-753-103-371-317-701 132-310-371-1_V.mp3)
  Segment 8: 26188ms ~ 26764ms (file: 103-301-317-103-312-317-301-310-310-713-173-130-317-315-173-137-317 371-310-715-137-317-306-367-317-637-310-301-371-372-132-301-176-736-735-317-371-310-317-307-137-310-130 307-7_V.mp3)
  Segment 9: 27764ms ~ 28340ms (file: 132-136 132-326-326-567-130-750-135-132-537-132-163-120_V.mp3)
  Segment 10: 29340ms ~ 29916ms (file: 675-760-706-620-362-203-137-620-675-602-762-310-302-362-325-326-326-632-762-760-673-360-627-612-623-627-627_W.mp3)
  Segment 11: 30916ms ~ 31492ms (file: 573-501-576-574-356-517-570-175-517-567-576-165-516-637-517-517-537-536-573-576-574-576-536-136-135-123 574-576-572-625-563-576-576-753-576-576-576-567-537-253-576-572-567 576-5_W.mp3)
  Segment 12: 32492ms ~ 40808ms (file: 271-271-172-173-754-750-237-275-537-132-137-273-136-176-710-715-127-217-210-627-162-130-173-173-173-175-137-172-710-172-701-710-714-271-751-752-756-726-725-756-170-257-276-120-1_W.mp3)
  Segment 13: 41808ms ~ 47028ms (file: 162-752-154-152-175-213-103-203-130-312-206-236-103-731-312-123-627-267-213-267-762-756-721-263-230-301-523-320-725-203-231-327-267-205-726-203-106-127-572-237-632-203-132-321-7_V.mp3)
  Segment 14: 48028ms ~ 60124ms (file: 756-276-132-321-267-216-231-132-267-267-237-237-130-316-326-136-756-754-120-132-267-276-230-260-230-230-237-230-231-231-217-231-231-276-237-102-231-120-612-231-273-267-216-120-1_W.mp3)
  Segment 15: 61124ms ~ 70232ms (file: 267-547-123-203-201-627-542-260-132-123-402-576-547-356-765-213-132-213-137-574-231-206-260-627-267-265-302-102-231-103-321-574-527-126-123-201-206-207-231-756-756-267-120-576-1_W.mp3)
  Segment 16: 71232ms ~ 71808ms (file: 362-321-172-102-123-124-150-120-120-120-120-102_V.mp3)
  Segment 17: 72808ms ~ 73384ms (file: 501-510-150-510-517-501-501-501-105-501 514-105-150-510-501-501-501-501-153-150-510-150-510-105-501-501-510-153-105-501-510-501-570-571 150-150-510-150-510-150-150-510-157-150-5_V.mp3)
  Segment 18: 74384ms ~ 74960ms (file: 127-672-162-172-137-136-317-671-176-176-167-167-163-167-176-172-712-672-712-276-762-720-725-705-765-760-760-765-752-750-750-726-267-276-267-267-276-762-765-765-276-273-726-276-7_W.mp3)
  Segment 19: 75960ms ~ 86436ms (file: 103-310-752-103-102-213-201-120-231-312-130-130-162-136-672-267-263-130-237-276-276-130-762-267-276-267-163-756-275-302-230-275-726-765-102-173-267-267-231-132-132-276-236-267-1_W.mp3)
  Segment 20: 87436ms ~ 88012ms (file: 627 621-162 546-546-574-564-576-567-576-567-567-564-172_W.mp3)
  Segment 21: 89012ms ~ 89588ms (file: 150-150-150-105-150-105-105-105 105-105-503-103-103-103 105-150-150-510-150-153-156-150-153-150-105-150_V.mp3)
  Segment 22: 90588ms ~ 99048ms (file: 213-162-217-132-120-213-321-213-216-213-231-216-263-213-310-301-231-752-216-230-120-271-123-213-236-231-213-263-231-320-574-236-103-261-320-276-107-132-201-267-320-756-726-752-7_W.mp3)
  Segment 23: 100048ms ~ 100624ms (file: 517-651-576-576-517-507-576-571-531-571-753-753-725-572-253-517-517-532-527-571-157-527-517-571-157-571-751-517-751-175-715-571-517-756-157-571-571-517-571-517-571-571-157-576-7_V.mp3)
  Segment 24: 101624ms ~ 102200ms (file: 731-371-657-570-370-765-310-301-310-317-736-761-715-713-715-176-317-103-576-752-273-126-570-576-765-713-507-123-301-310-301-132-132-132-752-123-327-127-173-307-716-507-127-217-7_W.mp3)
  Segment 25: 103200ms ~ 103776ms (file: 231-312-123-627-325-321 136-257-215-203-136-653-756-576-127-201-163-326-260-312-631-316-316-167-613-615_V.mp3)
  Segment 26: 104776ms ~ 113596ms (file: 573-753-256-136-236-163-162-670-203-526-236-276-302-530-527-537-327-270-157-752-157-267-123-103-571-312-351-321-157-165-576-573-201-105-231-130-132-753-560-723-263-130-203-237-5_V.mp3)
  Segment 27: 114596ms ~ 115172ms (file: 517-571-517-576-751-571-571-571-571-576-571-576-567-517-576-572-517-571-756-517-513-517-157-153-517-570-156-571-135-517-573-517-516-157-751-756-576-571-751-571-150-517-157-175-7_W.mp3)
  Segment 28: 116172ms ~ 122436ms (file: 672-671-130-236-120-675-751-261-261-231-312-130-726-236-726-761-102-132-213-130-230-103-210-132-213-162-321-623-732-130-672-627-261-712-123-213-201-216-130-312-236-321-236-123-6_W.mp3)
  Segment 29: 123436ms ~ 124012ms (file: 316-137-716-173-317-321-132-132-563-165-156-156-310-316-370-753-756-706-675-137-167-371-765-176-173-176-173-175-312-312-156-156-153-567-570-573-537-130-173-317-137-173-513-354-3_V.mp3)
  Segment 30: 125012ms ~ 125588ms (file: 312-312-301-130-301-137-130-137-312-375-317-357-312-137-130-310-135-135-135 130-103-103-103-321-137-103-135-317-321-315-137-132-132-173-317-137-132-103-132-132-130-137-301-310-3_V.mp3)
  Segment 31: 126588ms ~ 127164ms (file: 150-501-153-150-510-510-150-510-571-570-517-571 507-750-501-150-105-517-157-105-507-510-150-501-510-150-501-510-105-510-570-510-150-507-516-510-571-510-507-571-571 517-513-573-5_W.mp3)
  Segment 32: 128164ms ~ 128740ms (file: 301-310-301-375-730-370-160-162-621-260-623-136-316-203-236-532-527-574-572-125-621-107-106-201-206-312-206-603-136-301-567-765-260-326-236-102-103-160-126-123-160-261-210-625-2_W.mp3)
  Segment 33: 129740ms ~ 130316ms (file: 637-361-362-673-673-637-637-673-673-367-673-637-367-627 302-625-632-163-376-316-316-571-312-317-135-316-302-301-362-321-157-715-167-176-175-172-357-157-352-103-163-302-302 106-6_V.mp3)
  Segment 34: 131316ms ~ 131892ms (file: 510-510-501-501-150-510-501-510-150-510-501-510-501-510-501-507-501-501-514-574-514-157-510-157-571-571-517 547-514-150-510-510-510-510-510-150-157-510-507-540-570-570-517-517-5_V.mp3)
  Segment 35: 132892ms ~ 147364ms (file: 143 123-672-267-236-572-532-103-265-627-216-236-326-321-763-762-672-267-263-213-230-230-123-130-765-236-103-135-102-267-260-321-102-130-236-231-130-216-132-103-574-527-762-260-3_W.mp3)
  Segment 36: 148364ms ~ 148940ms (file: 526-253 367-362-703-763-376-762-705-706-725-576-375-765-615-367-672-672-263 165-672-627_V.mp3)
  Segment 37: 149940ms ~ 150516ms (file: 517-513-571-517-573-571-517-517-517-517-517-517-517-570 751-753-715-751_W.mp3)
  Segment 38: 151516ms ~ 156880ms (file: 130-627-267-267-267-627-263-621-120-567-260-120-102-126-127-612-260-670-627-267-162-123-201-120-312-216-267-672-201-206-263-620-520-267-162-167-120-302-231-627-326-267-123-103-1_W.mp3)
  Segment 39: 157880ms ~ 158456ms (file: 163-162-613-623-623-617-627-621-625-623-625-621-613-612-621-361-213-621-162-613-612-621-621-613-612-621-612-613-162-263-362-631-163-613-612-621-612-612-612-615-612-615-612-615-6_W.mp3)
  Segment 40: 159456ms ~ 160032ms (file: 672-621-620-160-630-765-672-672-675-672-627-267-762-276_W.mp3)
  Segment 41: 161032ms ~ 161608ms (file: 156-361-136-132-621-301-132-302-267-260-236-321-106-206 173-361-761-167-671-615-106-103-263-213-312-762-270-270-261-260-620-627-607-621-627-627-637-260-263-762-621-602-612-623-1_V.mp3)
  Segment 42: 162608ms ~ 168224ms (file: 103-310-312-130-123-312-321-231-123-120-132-120-321-132-123-312-312-120-132-312-132-132-132-310-312-132-312-312-132-310-312-320-321-312-321-320-320-216-367-276-132-321-326-132-3_V.mp3)
  Segment 43: 169224ms ~ 179880ms (file: 263-120-231-320-235-261-320-120-675-102-572-321-231-326-236-320-236-263-267-612-123-230-260-236-123-236-321-261-321-267-231-132-130-236-236-267-230-756-213-627-672-236-231-632-3_W.mp3)
  Segment 44: 180880ms ~ 189808ms (file: 302-540-103-310-107-136-163-216-260-507-103-123-136-321-103-327-312-137-103-750-216-275-527-275-173-132-201-210-123-132-310-256-725-216-236-312-137-713-215-103-132-312-574-317-1_V.mp3)
  Segment 45: 190808ms ~ 191384ms (file: 160-136-103-106-136-103-130-136-103-136-103-136-103-310-136-103-103-136-103-135-163-103-103-105-103-136_V.mp3)
  Segment 46: 192384ms ~ 192960ms (file: 156-567-516-651-561-156-165-675-165-156-615-631-765-713-362 607-507-324-213-123-627_W.mp3)
  Segment 47: 193960ms ~ 194536ms (file: 137-103-136-130-361-516-574-103-627-162-103-132-216-163-130-316-132-536-657-165-103-103-103-136-163-564-206-157-103-103-576-163-107-176-316-130-106-137-312-316-136-163-103-103-1_V.mp3)
  Segment 48: 195536ms ~ 196112ms (file: 625-532-351-532-503-520-502-503-502-521 301-312-102-316-107-136-625-713-321-137-326-613-362 316-301-312-302-631-310-301-124-132-103-310-301-130-310-134-312-217-132-361-603_V.mp3)
  Segment 49: 197112ms ~ 197688ms (file: 102-160-607-601-126-103-102-130-102-126-126-231-130-130-136-231-176-617-123-670-761-316-136-163-517-365-761-126-136-136-316-165-756-637-613-163-106-617-361-263-621-325-132-726-7_V.mp3)
  Segment 50: 198688ms ~ 199264ms (file: 715-751-750-751-715-751-751-571-751-751-751-517-756-570-576_W.mp3)
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
> **Time**: 2026-04-05 07:05:24
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "이메일 주소로 제보해 주시면 소중히 제작하겠습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "이메일", "start_ms": 150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "주소로", "start_ms": 650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "제보해", "start_ms": 1200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "주시면", "start_ms": 1650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "소중히", "start_ms": 2200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "제작하겠습니다", "start_ms": 2650, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 3,
"transcript": "약간 공포감 때문에",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "약간", "start_ms": 100, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "공포감", "start_ms": 400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "때문에", "start_ms": 800, "dur_ms": 300, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 6,
"transcript": "지금 제 채널 댓글에서도 아주 그냥 난리가 나가지고 막말로 이렇게 삼성 좀 줘패달라는 얘기까지도 나오고요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "지금", "start_ms": 80, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "제", "start_ms": 400, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "채널", "start_ms": 550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "댓글에서도", "start_ms": 950, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 1550, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 1850, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "난리가", "start_ms": 2100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "나가지고", "start_ms": 2500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "막말로", "start_ms": 3100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 3550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "삼성", "start_ms": 3900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 4300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "줘패달라는", "start_ms": 4500, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "얘기까지도", "start_ms": 5100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "나오고요", "start_ms": 5700, "dur_ms": 450, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 9,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 12,
"transcript": "거들떠보지도 않았죠 심지어 굶주린 개미 떼가 다가와 수컷들의 몸을 뜯어먹으며 사냥을",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "거들떠보지도", "start_ms": 120, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "않았죠", "start_ms": 850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "심지어", "start_ms": 1500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "굶주린", "start_ms": 2000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "개미", "start_ms": 2450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "떼가", "start_ms": 2800, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "다가와", "start_ms": 3050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "수컷들의", "start_ms": 3500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "몸을", "start_ms": 4050, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "뜯어먹으며", "start_ms": 4350, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "사냥을", "start_ms": 5000, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "해수부는 선원들 안전과 보급 상황을 확인하고 마지막 한 명이 내릴 때까지 최선을 다하겠다고 밝혔습니다 MBC 뉴스 김민형입니다",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "해수부는", "start_ms": 50, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "선원들", "start_ms": 550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "안전과", "start_ms": 950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "보급", "start_ms": 1400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "상황을", "start_ms": 1700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "확인하고", "start_ms": 2100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "마지막", "start_ms": 2650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 3100, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "명이", "start_ms": 3250, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "내릴", "start_ms": 3500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "때까지", "start_ms": 3850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "최선을", "start_ms": 4300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "다하겠다고", "start_ms": 4700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "밝혔습니다", "start_ms": 5250, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "MBC", "start_ms": 5900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "뉴스", "start_ms": 6250, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "김민형입니다", "start_ms": 6550, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "지금 몇 명의 선원이 고립돼 있는 겁니까",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "지금", "start_ms": 100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "몇", "start_ms": 450, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "명의", "start_ms": 600, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "선원이", "start_ms": 850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "고립돼", "start_ms": 1250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 1650, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "겁니까", "start_ms": 1900, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 16,
"transcript": "대륙의 모양 하나가 문명의 속도를 결정했다는 뜻입니다 이쯤 되면 문명의 탄생은 사람의 문제가 아니라 넓은 평야 큰 강 온화한 기후라는 지형의 문제라는 결론에",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "대륙의", "start_ms": 120, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "모양", "start_ms": 550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "하나가", "start_ms": 900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "문명의", "start_ms": 1350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "속도를", "start_ms": 1800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "결정했다는", "start_ms": 2250, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "뜻입니다", "start_ms": 2900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이쯤", "start_ms": 3500, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "되면", "start_ms": 3800, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "문명의", "start_ms": 4150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "탄생은", "start_ms": 4600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "사람의", "start_ms": 5100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "문제가", "start_ms": 5550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 6000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "넓은", "start_ms": 6400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "평야", "start_ms": 6750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 7150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "강", "start_ms": 7350, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "온화한", "start_ms": 7600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "기후라는", "start_ms": 8050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "지형의", "start_ms": 8600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "문제라는", "start_ms": 9050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "결론에", "start_ms": 9600, "dur_ms": 400, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 19,
"transcript": "에스플러스를 국내에서 팔고 있는 에스식스 플러스랑 직접 비교했을 때 일부 앱에서 좀 급속도로 발열",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "에스플러스를", "start_ms": 80, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "국내에서", "start_ms": 750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "팔고", "start_ms": 1250, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 1550, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "에스식스", "start_ms": 1800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "플러스랑", "start_ms": 2300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "직접", "start_ms": 2850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "비교했을", "start_ms": 3200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 3750, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "일부", "start_ms": 3950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "앱에서", "start_ms": 4300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 4700, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "급속도로", "start_ms": 4900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "발열", "start_ms": 5450, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "속도를 철저히 저당잡혀 살아가는지 여실히 깨닫게 되니까요 1915년 제1차 세계 대전 당시 심리학자 에드워",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "속도를", "start_ms": 120, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "철저히", "start_ms": 550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "저당잡혀", "start_ms": 1050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "살아가는지", "start_ms": 1600, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "여실히", "start_ms": 2250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "깨닫게", "start_ms": 2750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "되니까요", "start_ms": 3200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "1915년", "start_ms": 3850, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "제1차", "start_ms": 4700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "세계", "start_ms": 5200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "대전", "start_ms": 5600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "당시", "start_ms": 6000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "심리학자", "start_ms": 6450, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "에드워", "start_ms": 7050, "dur_ms": 450, "phase_lr_deg": 0.0}
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
"transcript": "아 지금 뛰시는 거예요 예 얼마 뛰시는데요",
"speaker": "speaker_5",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "아", "start_ms": 100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "뛰시는", "start_ms": 650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 1100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "예", "start_ms": 1500, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "얼마", "start_ms": 1750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "뛰시는데요", "start_ms": 2100, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "무기가 생기지 않을까 근데 내가 어떤 종목을 1년 2년 계속 지켜보고 매매를 하다 보면은 그 종목만의 특유의 리듬이 몸에 익어 네 말로 설명하기 되게 어려운데 어떻게 보면 감각적인 부분인 것 같아요 아 이",
"speaker": "speaker_6",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "무기가", "start_ms": 80, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "생기지", "start_ms": 450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "않을까", "start_ms": 900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 1300, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "내가", "start_ms": 1600, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 1900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "종목을", "start_ms": 2250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "1년", "start_ms": 2700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "2년", "start_ms": 3100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 3500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "지켜보고", "start_ms": 3900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "매매를", "start_ms": 4450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "하다", "start_ms": 4900, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "보면은", "start_ms": 5200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 5650, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "종목만의", "start_ms": 5800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "특유의", "start_ms": 6350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "리듬이", "start_ms": 6800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "몸에", "start_ms": 7250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "익어", "start_ms": 7600, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "네", "start_ms": 7900, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "말로", "start_ms": 8100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "설명하기", "start_ms": 8450, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "되게", "start_ms": 9050, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "어려운데", "start_ms": 9350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 9900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "보면", "start_ms": 10350, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "감각적인", "start_ms": 10650, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "부분인", "start_ms": 11250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 11700, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 11900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 12400, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 12600, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "강화되면서 안보 위협이 커졌다 북한은 러시아의 무기와 병력을 제공했고 그 대가로 상당한 외화를 확보한 것으로 추정된다 이는 북한의 군사력을 강화시키고 제재 효과를 약화시키는 결과를 낳",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "강화되면서", "start_ms": 50, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "안보", "start_ms": 700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "위협이", "start_ms": 1100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "커졌다", "start_ms": 1550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "북한은", "start_ms": 2100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "러시아의", "start_ms": 2600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "무기와", "start_ms": 3150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "병력을", "start_ms": 3650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "제공했고", "start_ms": 4150, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 4750, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "대가로", "start_ms": 4900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "상당한", "start_ms": 5350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "외화를", "start_ms": 5850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "확보한", "start_ms": 6350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "것으로", "start_ms": 6850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "추정된다", "start_ms": 7250, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "이는", "start_ms": 7950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "북한의", "start_ms": 8300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "군사력을", "start_ms": 8750, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "강화시키고", "start_ms": 9350, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "제재", "start_ms": 10100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "효과를", "start_ms": 10500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "약화시키는", "start_ms": 11000, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "결과를", "start_ms": 11800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "낳", "start_ms": 12300, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "있는 링크나 댓글에 있는 링크로 들어오셔서 간단한 양식과 숫자 8번 또는 8번 지표라고 적어 주시면",
"speaker": "speaker_6",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "있는", "start_ms": 100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "링크나", "start_ms": 350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "댓글에", "start_ms": 800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 1250, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "링크로", "start_ms": 1500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "들어오셔서", "start_ms": 2000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "간단한", "start_ms": 2650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "양식과", "start_ms": 3150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "숫자", "start_ms": 3650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "8번", "start_ms": 4050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "또는", "start_ms": 4450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "8번", "start_ms": 4800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "지표라고", "start_ms": 5200, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "적어", "start_ms": 5800, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "주시면", "start_ms": 6150, "dur_ms": 450, "phase_lr_deg": 0.0}
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
"transcript": "거대한 타이타닉도 얼음에 부딪히자마자 반으로 갈라졌는데 쇄빙선은 도대체 어떻게 얼음을 부수면서 앞으로 나가는 거죠",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "거대한", "start_ms": 100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "타이타닉도", "start_ms": 600, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "얼음에", "start_ms": 1250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "부딪히자마자", "start_ms": 1700, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "반으로", "start_ms": 2550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "갈라졌는데", "start_ms": 3000, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "쇄빙선은", "start_ms": 3700, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "도대체", "start_ms": 4300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 4750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "얼음을", "start_ms": 5200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "부수면서", "start_ms": 5650, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "앞으로", "start_ms": 6250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "나가는", "start_ms": 6700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "거죠", "start_ms": 7150, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "쇄빙선의 비밀은 바로 이 앞머리 선수에",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "쇄빙선의", "start_ms": 120, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "비밀은", "start_ms": 650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 1150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 1500, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "앞머리", "start_ms": 1700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "선수에", "start_ms": 2200, "dur_ms": 450, "phase_lr_deg": 0.0}
]
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
"transcript": "면서 소통이란 이런 거다라는 걸 조금 보여줬으면 좋겠습니다 요즘 스마트폰은요 진짜 성능이 옛날보다 많이 상향 평준화가 되었지만 가격이 진짜",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "면서", "start_ms": 80, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "소통이란", "start_ms": 350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 900, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "거다라는", "start_ms": 1200, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "걸", "start_ms": 1800, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 2000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "보여줬으면", "start_ms": 2350, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "좋겠습니다", "start_ms": 3050, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "요즘", "start_ms": 3700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "스마트폰은요", "start_ms": 4100, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 4900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "성능이", "start_ms": 5300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "옛날보다", "start_ms": 5800, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 6400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "상향", "start_ms": 6750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "평준화가", "start_ms": 7150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "되었지만", "start_ms": 7800, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "가격이", "start_ms": 8400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 8850, "dur_ms": 350, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 07:05:24*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=11716  time=07:04:07  uptime=18904s  mem=450MB  system_processes=353"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_065938.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 6228ms (file: 136-310-316-132-126-167-106-201-302-213-230-216-201-103-312-310-301-132-526-325-103-354-201-103-325-136-201-132-102-123-231-236-231-123-540-312-126-236-213-123-132-103-302-321-2_V.mp3)
  Segment 2: 7228ms ~ 7804ms (file: 621-576-576-726-267-576-263-312-357-132-213-217-725-752-573-572-312-132-273-275-275-325-132 576-726-267-126-316-231-537-107-567-267-310-102-103-725-275-271-716-175-715-312-137-5_W.mp3)
  Segment 3: 8804ms ~ 9380ms (file: 167-123-126-136-137-132-134-132-167_V.mp3)
  Segment 4: 10380ms ~ 10956ms (file: 306-602-362-623-621-623-620-620-267-620-620-623-632-361-316_W.mp3)
  Segment 5: 11956ms ~ 12532ms (file: 573-751-715-751-157-751-576-571-157-157-157-517-517-571-576_V.mp3)
  Segment 6: 13532ms ~ 21128ms (file: 267-765-275-267-312-753-106-106-231-236-231-263-263-231-102-321-271-256-132-162-165-612-162-756-756-265-326-362-675-625-321-316-612-162-102-231-321-213-273-206-236-260-267-130-5_W_stt_those_commands_all_the_time_we_took_a_to.mp3)
  Segment 7: 22128ms ~ 22704ms (file: 576-576-267-206-267-216-237-321-507-135-103-132-162-103-754-756-751-301-103-107-107-103-175-132-574-574-317-571-501-103-164-174-672-741-765-617-173-671-671-207-276-103-316-136-3_V.mp3)
  Segment 8: 23704ms ~ 24280ms (file: 201-201-120-210-120-206-213-213-123-213-126-231-216-210-102-210-210-216-120-213-210-210-210-210-257-213-201-120-213-263-236-261-260-216-120-216-210-126-216-216-251-256-210-216-2_W.mp3)
  Segment 9: 25280ms ~ 25856ms (file: 325-175-654 367-361-617-236-652 162-132-310 125-326-531-571 715-765-765-671-675-713-761-176-671-637-763-671-673-136-163-361-673-367-735-635-365-376-357-351-325-675-517-514-532-1_V.mp3)
  Segment 10: 26856ms ~ 27432ms (file: 165-613-765-605-136-613-136-103-576-306-657-156-163-163-167-615-126-167-613-106-517-730-127-106-103-136-716-671-216-267-136-106-136-103-160-136-165-173-106-123-103-136-256-102-1_V.mp3)
  Segment 11: 28432ms ~ 29008ms (file: 132-316-230-261-163-631-673-376-612-106-137-136-162-167-136-106-107-526-163-576-627-672-172-130-136-536-103-621-162-316-610-162-106-162-160-123-106-102-132-136-320-613-167 614-5_V.mp3)
  Segment 12: 30008ms ~ 39404ms (file: 103-267-627-627-267-236-231-130-312-217-710-756-756-752-275-135-576-756-571-231-231-267-267-672-267-236-617-132-103-267-120-210-206-237-130-574-576-576-726-726-132-105-103-207-2_W.mp3)
  Segment 13: 40404ms ~ 47496ms (file: 310-201-570-576-756-206-207-205-267-207-526-237-207-316-127-132-213-752-756-126-124-206-231-123-310-750-231-750-103-312-523-760-362-132-572-317-230-207-725-127-205-132-270-267-2_V_stt_as_the_dons_a_student_in_a_gala_for_the.mp3)
  Segment 14: 48496ms ~ 49072ms (file: 256-156-152-527-675-576-526-567-576-765-567-756-572-756 316-132-574-562-675-576-567-673-567-576-567 137-136-213-631-156-106-176-316-516-126-215-135-130-103-103-163-136-123-175-6_W.mp3)
  Segment 15: 50072ms ~ 50648ms (file: 134-104-105-104-134-104-103-104-451-174-145-104-175-104-145-103-375-371-413-341-435-405-451-401-410-354-310-401-405-410-104-104-140-451-104-140-134 375-415-470-345-413-314-341-1_V.mp3)
  Segment 16: 51648ms ~ 52224ms (file: 621-261-627-672 312-136-623-623-623-627-627-627-625-326-623-627-321-326-763-672-627-271-321-261-637-716-126-126-312-316-637-617-261-267-236-675-356-672-673 612-213 675 675-672-6_W.mp3)
  Segment 17: 53224ms ~ 53800ms (file: 153-537-530-501-571-570-517-570-572-517-510-517-570-517-570-513-517-570-571-517-517-570-517-751-157-157-175-715-173-570-751-751-756-576-571-517-571-517-576-517-571-571-576-576-5_W.mp3)
  Segment 18: 54800ms ~ 55376ms (file: 750-570-574-574-107-701-570-574-574-574-571-571 570-517-157-571-517-105-710-751-517-107-107-750-574-715-517-107-701-570 574-570-571-507-105-507-157-510-150-175-105-105-150-107-1_V.mp3)
  Segment 19: 56376ms ~ 56952ms (file: 175-157-135-136-137-163-167-163-167-135-351-137-136-316-156-173-170-103-150-153-135-671-163-673-673-176-371-137-731-715-713-137 517-567-751-761-765-517-517-571-715-517-571-517-5_W.mp3)
  Segment 20: 57952ms ~ 65512ms (file: 721-327-273-602-217-102-320-327-237-136-137-203-172-321-321-153-370-135-157-127-271-201-201-276-207-276-265-752-750-127-102-752-175-521-230-723-107-527-275-231-205-627-205-312-1_V.mp3)
  Segment 21: 66512ms ~ 67088ms (file: 157-150-510-150-157-150-157-150-510-150-507-517-150-510-501-517-157-175-751-750-754-754-751-754-754-754-754-754-754-754-754-754-754-105-150-157-150-157-105-157-150-105-150-150-1_W.mp3)
  Segment 22: 68088ms ~ 68664ms (file: 135-236-562-136-621-312-316-326-123-312-612-621-613-216-623-627-627-256-627-612-657-102-136-132-210-672-672-623-162-621-162-126-236-261-615-620-623-265-236-260-260-261-260-263-6_W.mp3)
  Segment 23: 69664ms ~ 70240ms (file: 135-657-163-615-165-125-617-631-632-263-625-316-236-326-315-567-576-567-574-547-574-567-657-627-657-637-673-675-267-754-725-750-547-357-357-540-357-357-357-574-576-572-235-537-5_W.mp3)
  Segment 24: 71240ms ~ 71816ms (file: 301-613-260-351-150-756-765-571-571-137-576-576-507-163-652-263-231-706-761-132-317-157-316-103-130-167-163-137-136-316-176-132-163-321-132-130-623-263-267-762-670-301-560-574-5_V.mp3)
  Segment 25: 72816ms ~ 73392ms (file: 726-623-317-367-103-325-763-132 625-132 327-312-632_V.mp3)
  Segment 26: 74392ms ~ 74968ms (file: 754-157-751 105-105-105-105-150-517-105-157-157-175-150-517-157-175-571-754-517-751 154-105-150-175-157-157-576-576-715-176-150-105-175-157-175-175-175-157-153-157-157-157-751-1_W.mp3)
  Segment 27: 75968ms ~ 82376ms (file: 102-320-321-213-267-231-132-261-120-123-312-302-312-321-312-123-231-237-123-201-321-236-236-260-120-310-126-160-132-130-326-213-321-321-236-623-267-260-206-216-126-126-167-162-6_W.mp3)
  Segment 28: 83376ms ~ 93996ms (file: 123-137-617-573-571-627-620-617-750-725-137-132-526-132-762-756-762-271-723-157-107-137-132-273-236-570-732-254-537-507-257-231-543-573-574-267-257-203-203-130-716-756-760-765-7_V.mp3)
  Segment 29: 94996ms ~ 103600ms (file: 165-267-521-756-206-230-602-572-576-627-627-261-105-125-132-572-265-132-576-607-267-157-201-261-260-607-756-127-135-573-125-307-625-120-627-570-263-607-623-312-524-675-567-572-5_V.mp3)
  Segment 30: 104600ms ~ 111512ms (file: 163-165-136-130-270-210-217-570-105-167-206-201-265-627-120-102-163-263-326-213-312-176-137-106-163-167-706-617-320-312-231-312-321-312-132-137-123-102-137-753-716-167-107-731-7_W_stt_the_fees_alone_from_the_command_of_a_van.mp3)
  Segment 31: 112512ms ~ 113088ms (file: 315-135-135-103-315-315-351-315-137-317-135-175-531-310 137-310-312-132-135-152-315-315-137-135-317-132-317-135-317-175-135-315-137-317-531-317-321-137-105-517 135-130-135-135-1_V.mp3)
  Segment 32: 114088ms ~ 114664ms (file: 106-162-672-621-623-306-735-537-362-306-362-261-173-601-162-613-675-670-672-670-675-765-167-106-321-231-103-132-321-302-132-763-637-631-106-103-301-103-312-316 134-312_V.mp3)
  Segment 33: 115664ms ~ 126212ms (file: 361-105-751-657-637-675-653-573-527-576-567-165-175-715-137-123-103-102-213-527-570-135-301-267-267-132-103-132-103-723-732-302-312-321-320-321-304-654-312-132-261-132-127-123-1_W.mp3)
  Segment 34: 127212ms ~ 127788ms (file: 763-637-613-103-567-562-617-617-126-162-260-236-215-231-312-210-267-162-231-132-273-201-167-176-607-612-106-103-136-673-302-106-136-627-123-162-360-103-106-106-607-260-216-106-5_W.mp3)
  Segment 35: 128788ms ~ 129364ms (file: 612-601-103-310-312 132-167-103 132 637 617-673-765-170-102-126 132-136-132-132_V.mp3)
  Segment 36: 130364ms ~ 142280ms (file: 302-310-130-310-751-213-130-237-756-167-120-751-165-310-213-203-213-102-230-275-752-726-761-231-213-132-136-713-107-102-130-752-752-132-210-203-320-231-725-123-207-231-123-130-2_W.mp3)
  Segment 37: 143280ms ~ 143856ms (file: 175-170-135-170-107-107-170-107-170-173-173-107-710-130-170-137-175-172-172-103-107-170-170-150-107-107-107-170-107-170-170-137-507 175-715-715-175-175-137-710-751-751-715-107-3_V.mp3)
  Segment 38: 144856ms ~ 145432ms (file: 751-756-756-756-756-751-571-517-570-507-715-715-157-107-710-175-157-175-750-570-571-750-571-571-574-574-574-574-574-574-574-105-175 571-750-570-574-574-574-574-574-574-574-751-5_W.mp3)
  Segment 39: 146432ms ~ 147008ms (file: 103-312-103-312-106-107-103-123-102-231-210-126-326-753-376-301-316-302-301-327-725-217-301-130-136-162-163-136-362-632-236-230-203-326-514-574-157-517-165-103-602-206-260-260-2_W.mp3)
  Segment 40: 148008ms ~ 154920ms (file: 261-216-217-267-263-231-123-361-265-267-132-317-671-761-712-236-306-217-267-257-627-126-127-276-123-123-123-312-132-237-136-137-135-132-327-312-312-312-316-321-231-321-120-162-3_W.mp3)
  Segment 41: 155920ms ~ 156496ms (file: 756-765-703-730-206-206-126-321-201-206-132-130-635-136-326-230-570-257-230-301-310-316-136-103-136-106-751-705-720-207-270-123-213-216-126-230-762-672-632-632-320-320-312-312-3_V.mp3)
  Segment 42: 157496ms ~ 158072ms (file: 657 173-175-137-731-103-701-715-107-107-712-715-701-130-107-710-310-371-130-715-710-713 317-301-137-715-137-371-173-710-127-371-137-715-735-317-175-173-371-317-175-735-751-130-1_V.mp3)
  Segment 43: 159072ms ~ 159648ms (file: 132-612-657-156-572-562-516-163-136-130-361-301-163-162-261-123-216-103-162-132-102-132-103-231-130_V.mp3)
  Segment 44: 160648ms ~ 161224ms (file: 206-206-206-267-126-312-123-132-103-102-123-321 273-312_V.mp3)
  Segment 45: 162224ms ~ 162800ms (file: 571-517-576-576-261-176-132-120-763-652-165-163-162-675-756-657-675-651-651-163_W.mp3)
  Segment 46: 163800ms ~ 164376ms (file: 132-312-312-126 126-316-103-321-236-321-130-312-321-127-263-236-263-263 132_W.mp3)
  Segment 47: 165376ms ~ 165952ms (file: 672-621-620-160-630-765-672-672-675-672-627-267-762-276_W.mp3)
  Segment 48: 166952ms ~ 177500ms (file: 203-375-752-312-231-260-260-576-267-612-120-163-260-231-261-261-210-261-260-216-102-216-132-261-267-216-216-261-572-265-213-263-762-526-265-263-261-267-276-213-206-546-260-260-2_W_stt_they.mp3)
  Segment 49: 178500ms ~ 179076ms (file: 547-547-547-514-541-517 504-507-504-540-504-514-154-105-547-574-547-547-547 540-570-507-574-570-507-501-504-504-547-571-105-157-501-504 507-504-504-547-547-541-547-547-541-510-5_W.mp3)
  Segment 50: 180076ms ~ 180652ms (file: 105-310-130-135-157-153 507-153-510-530-137-507-573-135-135-107-130-130-103-130-137-175 130-135-150-175-150-351-517-157-517-157-573-153-130-310-310-130-315-315-135-130-130-130-1_V.mp3)
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
> **Time**: 2026-04-05 07:05:24
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "이메일 주소로 제보해 주시면 소중히 제작하겠습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "이메일", "start_ms": 150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "주소로", "start_ms": 650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "제보해", "start_ms": 1200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "주시면", "start_ms": 1650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "소중히", "start_ms": 2200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "제작하겠습니다", "start_ms": 2650, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 3,
"transcript": "약간 공포감 때문에",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "약간", "start_ms": 100, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "공포감", "start_ms": 400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "때문에", "start_ms": 800, "dur_ms": 300, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 6,
"transcript": "지금 제 채널 댓글에서도 아주 그냥 난리가 나가지고 막말로 이렇게 삼성 좀 줘패달라는 얘기까지도 나오고요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "지금", "start_ms": 80, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "제", "start_ms": 400, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "채널", "start_ms": 550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "댓글에서도", "start_ms": 950, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 1550, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 1850, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "난리가", "start_ms": 2100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "나가지고", "start_ms": 2500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "막말로", "start_ms": 3100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 3550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "삼성", "start_ms": 3900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 4300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "줘패달라는", "start_ms": 4500, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "얘기까지도", "start_ms": 5100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "나오고요", "start_ms": 5700, "dur_ms": 450, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 9,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 12,
"transcript": "거들떠보지도 않았죠 심지어 굶주린 개미 떼가 다가와 수컷들의 몸을 뜯어먹으며 사냥을",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "거들떠보지도", "start_ms": 120, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "않았죠", "start_ms": 850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "심지어", "start_ms": 1500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "굶주린", "start_ms": 2000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "개미", "start_ms": 2450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "떼가", "start_ms": 2800, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "다가와", "start_ms": 3050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "수컷들의", "start_ms": 3500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "몸을", "start_ms": 4050, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "뜯어먹으며", "start_ms": 4350, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "사냥을", "start_ms": 5000, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "해수부는 선원들 안전과 보급 상황을 확인하고 마지막 한 명이 내릴 때까지 최선을 다하겠다고 밝혔습니다 MBC 뉴스 김민형입니다",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "해수부는", "start_ms": 50, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "선원들", "start_ms": 550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "안전과", "start_ms": 950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "보급", "start_ms": 1400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "상황을", "start_ms": 1700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "확인하고", "start_ms": 2100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "마지막", "start_ms": 2650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 3100, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "명이", "start_ms": 3250, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "내릴", "start_ms": 3500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "때까지", "start_ms": 3850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "최선을", "start_ms": 4300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "다하겠다고", "start_ms": 4700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "밝혔습니다", "start_ms": 5250, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "MBC", "start_ms": 5900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "뉴스", "start_ms": 6250, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "김민형입니다", "start_ms": 6550, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "지금 몇 명의 선원이 고립돼 있는 겁니까",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "지금", "start_ms": 100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "몇", "start_ms": 450, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "명의", "start_ms": 600, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "선원이", "start_ms": 850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "고립돼", "start_ms": 1250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 1650, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "겁니까", "start_ms": 1900, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 16,
"transcript": "대륙의 모양 하나가 문명의 속도를 결정했다는 뜻입니다 이쯤 되면 문명의 탄생은 사람의 문제가 아니라 넓은 평야 큰 강 온화한 기후라는 지형의 문제라는 결론에",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "대륙의", "start_ms": 120, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "모양", "start_ms": 550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "하나가", "start_ms": 900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "문명의", "start_ms": 1350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "속도를", "start_ms": 1800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "결정했다는", "start_ms": 2250, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "뜻입니다", "start_ms": 2900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이쯤", "start_ms": 3500, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "되면", "start_ms": 3800, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "문명의", "start_ms": 4150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "탄생은", "start_ms": 4600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "사람의", "start_ms": 5100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "문제가", "start_ms": 5550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 6000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "넓은", "start_ms": 6400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "평야", "start_ms": 6750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 7150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "강", "start_ms": 7350, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "온화한", "start_ms": 7600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "기후라는", "start_ms": 8050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "지형의", "start_ms": 8600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "문제라는", "start_ms": 9050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "결론에", "start_ms": 9600, "dur_ms": 400, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 19,
"transcript": "에스플러스를 국내에서 팔고 있는 에스식스 플러스랑 직접 비교했을 때 일부 앱에서 좀 급속도로 발열",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "에스플러스를", "start_ms": 80, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "국내에서", "start_ms": 750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "팔고", "start_ms": 1250, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 1550, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "에스식스", "start_ms": 1800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "플러스랑", "start_ms": 2300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "직접", "start_ms": 2850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "비교했을", "start_ms": 3200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 3750, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "일부", "start_ms": 3950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "앱에서", "start_ms": 4300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 4700, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "급속도로", "start_ms": 4900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "발열", "start_ms": 5450, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "속도를 철저히 저당잡혀 살아가는지 여실히 깨닫게 되니까요 1915년 제1차 세계 대전 당시 심리학자 에드워",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "속도를", "start_ms": 120, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "철저히", "start_ms": 550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "저당잡혀", "start_ms": 1050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "살아가는지", "start_ms": 1600, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "여실히", "start_ms": 2250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "깨닫게", "start_ms": 2750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "되니까요", "start_ms": 3200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "1915년", "start_ms": 3850, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "제1차", "start_ms": 4700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "세계", "start_ms": 5200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "대전", "start_ms": 5600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "당시", "start_ms": 6000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "심리학자", "start_ms": 6450, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "에드워", "start_ms": 7050, "dur_ms": 450, "phase_lr_deg": 0.0}
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
"transcript": "아 지금 뛰시는 거예요 예 얼마 뛰시는데요",
"speaker": "speaker_5",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "아", "start_ms": 100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "뛰시는", "start_ms": 650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 1100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "예", "start_ms": 1500, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "얼마", "start_ms": 1750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "뛰시는데요", "start_ms": 2100, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "무기가 생기지 않을까 근데 내가 어떤 종목을 1년 2년 계속 지켜보고 매매를 하다 보면은 그 종목만의 특유의 리듬이 몸에 익어 네 말로 설명하기 되게 어려운데 어떻게 보면 감각적인 부분인 것 같아요 아 이",
"speaker": "speaker_6",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "무기가", "start_ms": 80, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "생기지", "start_ms": 450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "않을까", "start_ms": 900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 1300, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "내가", "start_ms": 1600, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 1900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "종목을", "start_ms": 2250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "1년", "start_ms": 2700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "2년", "start_ms": 3100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 3500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "지켜보고", "start_ms": 3900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "매매를", "start_ms": 4450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "하다", "start_ms": 4900, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "보면은", "start_ms": 5200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 5650, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "종목만의", "start_ms": 5800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "특유의", "start_ms": 6350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "리듬이", "start_ms": 6800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "몸에", "start_ms": 7250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "익어", "start_ms": 7600, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "네", "start_ms": 7900, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "말로", "start_ms": 8100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "설명하기", "start_ms": 8450, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "되게", "start_ms": 9050, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "어려운데", "start_ms": 9350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 9900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "보면", "start_ms": 10350, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "감각적인", "start_ms": 10650, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "부분인", "start_ms": 11250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 11700, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 11900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 12400, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 12600, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "강화되면서 안보 위협이 커졌다 북한은 러시아의 무기와 병력을 제공했고 그 대가로 상당한 외화를 확보한 것으로 추정된다 이는 북한의 군사력을 강화시키고 제재 효과를 약화시키는 결과를 낳",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "강화되면서", "start_ms": 50, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "안보", "start_ms": 700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "위협이", "start_ms": 1100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "커졌다", "start_ms": 1550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "북한은", "start_ms": 2100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "러시아의", "start_ms": 2600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "무기와", "start_ms": 3150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "병력을", "start_ms": 3650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "제공했고", "start_ms": 4150, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 4750, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "대가로", "start_ms": 4900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "상당한", "start_ms": 5350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "외화를", "start_ms": 5850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "확보한", "start_ms": 6350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "것으로", "start_ms": 6850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "추정된다", "start_ms": 7250, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "이는", "start_ms": 7950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "북한의", "start_ms": 8300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "군사력을", "start_ms": 8750, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "강화시키고", "start_ms": 9350, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "제재", "start_ms": 10100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "효과를", "start_ms": 10500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "약화시키는", "start_ms": 11000, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "결과를", "start_ms": 11800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "낳", "start_ms": 12300, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "있는 링크나 댓글에 있는 링크로 들어오셔서 간단한 양식과 숫자 8번 또는 8번 지표라고 적어 주시면",
"speaker": "speaker_6",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "있는", "start_ms": 100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "링크나", "start_ms": 350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "댓글에", "start_ms": 800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 1250, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "링크로", "start_ms": 1500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "들어오셔서", "start_ms": 2000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "간단한", "start_ms": 2650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "양식과", "start_ms": 3150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "숫자", "start_ms": 3650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "8번", "start_ms": 4050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "또는", "start_ms": 4450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "8번", "start_ms": 4800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "지표라고", "start_ms": 5200, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "적어", "start_ms": 5800, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "주시면", "start_ms": 6150, "dur_ms": 450, "phase_lr_deg": 0.0}
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
"transcript": "거대한 타이타닉도 얼음에 부딪히자마자 반으로 갈라졌는데 쇄빙선은 도대체 어떻게 얼음을 부수면서 앞으로 나가는 거죠",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "거대한", "start_ms": 100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "타이타닉도", "start_ms": 600, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "얼음에", "start_ms": 1250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "부딪히자마자", "start_ms": 1700, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "반으로", "start_ms": 2550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "갈라졌는데", "start_ms": 3000, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "쇄빙선은", "start_ms": 3700, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "도대체", "start_ms": 4300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 4750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "얼음을", "start_ms": 5200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "부수면서", "start_ms": 5650, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "앞으로", "start_ms": 6250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "나가는", "start_ms": 6700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "거죠", "start_ms": 7150, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "쇄빙선의 비밀은 바로 이 앞머리 선수에",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "쇄빙선의", "start_ms": 120, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "비밀은", "start_ms": 650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 1150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 1500, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "앞머리", "start_ms": 1700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "선수에", "start_ms": 2200, "dur_ms": 450, "phase_lr_deg": 0.0}
]
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
"transcript": "면서 소통이란 이런 거다라는 걸 조금 보여줬으면 좋겠습니다 요즘 스마트폰은요 진짜 성능이 옛날보다 많이 상향 평준화가 되었지만 가격이 진짜",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "면서", "start_ms": 80, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "소통이란", "start_ms": 350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 900, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "거다라는", "start_ms": 1200, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "걸", "start_ms": 1800, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 2000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "보여줬으면", "start_ms": 2350, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "좋겠습니다", "start_ms": 3050, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "요즘", "start_ms": 3700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "스마트폰은요", "start_ms": 4100, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 4900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "성능이", "start_ms": 5300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "옛날보다", "start_ms": 5800, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 6400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "상향", "start_ms": 6750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "평준화가", "start_ms": 7150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "되었지만", "start_ms": 7800, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "가격이", "start_ms": 8400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 8850, "dur_ms": 350, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 07:05:24*
