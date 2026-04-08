# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=31656  time=07:26:53  uptime=13222s  mem=350MB  system_processes=408"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_072640.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 6624ms (file: 615-160-751-627-321-321-231-236-275-230-132-173-672-236-132-217-301-132-231-210-206-260-327-754-273-310-310-230-203-206-203-230-213-123-206-301-320-126-162-273-231-102-321-735-2_W.mp3)
  Segment 2: 7624ms ~ 16984ms (file: 310-237-132 357-571-153-132-365-360-361-372-357-236-132-106-143-316-312-326-132-163-136-361-765-267-736-120-201-263-230-132-123-135-325-257-621-210-210-765-762-321-136-657-613-1_W.mp3)
  Segment 3: 17984ms ~ 18560ms (file: 623-361-162-367-235-631-376-752-627-137-753-761-763-376-736-763-162-257-723-352-123-175-675-356-536-570-631-325-732-317-312-573-526-372-573-673-627-632-362-371-756-132-263-362-3_W.mp3)
  Segment 4: 19560ms ~ 20136ms (file: 136-135-135-130-173-317-173-317-731-371-371-713-715-731-173-173-713-173-173-137-510-153-731-175-571-136-157-375-127-175-671-651-561-517-561-615-567-561-571-672-617-317-136-137-1_W.mp3)
  Segment 5: 21136ms ~ 21712ms (file: 136-301-135-165-563-567-536-357-657-567-567-263-361-365-316-365 574-132-132-163-136-760-231-315_W.mp3)
  Segment 6: 22712ms ~ 23288ms (file: 574-675-756-576-576-576-576-751-751-271-327-263-236-326-261-216-263-236-263-236-627-672-675-627-623-637-651-325-362-265-527-372-352-572-573-574-526-256-167-317-367-673-675-765-7_W.mp3)
  Segment 7: 24288ms ~ 24864ms (file: 756-753-176-753-761-567-765-675-165-675-763-167-761-671-315-137-310-135-135-165-571-531-576-675-547-516-735-120-560-765-756-675-627-765-576-756-356 765-672-756-537-753-321-137-1_W.mp3)
  Segment 8: 25864ms ~ 26440ms (file: 326-760-762-671-157-125-572-326-752-537-721-327-357-506-317-362-532-357-531-327-453-752-123-267-672-267-236-527-627-725-231-540-652-256-576-570-263-126-261-521-312-267-203 275-1_V.mp3)
  Segment 9: 27440ms ~ 28016ms (file: 540-237-756-576-672 504-657-715-217-527-531-251-572-671-572-576-756-572-276-756-705-175-316-125-756-326-310-312-326-657-752-756-375-236-231-326-752-572-531-312-537-362-301-317-3_V.mp3)
  Segment 10: 29016ms ~ 29592ms (file: 157-175-157-156-157-157-156-157-517-157-517-571-517-157-571-517-157-571-135-153-135-153-135-153-135-513-130-130-175-157-175-157-157-175-157-175-576-561-567-517-571-517-157-153-3_V.mp3)
  Segment 11: 30592ms ~ 37504ms (file: 123-217-102-213-574-251-236-236-263-275-123-601-273-216-102-120-123-623-132-236-321-273-102-216-213-203-231-573-235-231-213-321-572-125-201-203-132-107-715-175-103-132-103-132-2_W.mp3)
  Segment 12: 38504ms ~ 44156ms (file: 765-672-276-267-260-621-231-312-325-236-126-137-327-267-231-120-267-167-705-756-267-276-267-267-160-627-267-176-321-756-675-657-617-216-126-102-750-316-126-213-762-726-216-267-1_W.mp3)
  Segment 13: 45156ms ~ 45732ms (file: 102-671-761-376-763-761-736-136-163-751-376-543-375-125-132-132-316-531-615-132-132-562-576-156-316-136-361-106-103-160-163-102-312-132-103-132-123-132-103-106-160-160-160-615-1_W.mp3)
  Segment 14: 46732ms ~ 53824ms (file: 321-752-152-705-236-627-206-102-162-751-176-137-273-230-752-261-236-267-627-236-627-103-312-301-531-507-257-263-263-310-275-213-260-302-725-576-613-123-230-237-672-321-152-203-5_W.mp3)
  Segment 15: 54824ms ~ 55400ms (file: 507-312-732-256-213-132-502-231-762-625-673-520-216-627-357-726-625-213-526-265-527-356-257-127-537-726-602-213-372-570-312-506-653-570-126-260-206-321-236-516-263-673-675-360-6_W.mp3)
  Segment 16: 56400ms ~ 60576ms (file: 271-310-206-123-231-302-572-352-276-312-130-312-213-712-731-762-754-756-751-157-517-517-517-570-570-570-172-273-127-701-725-710-271-217-731-317-170-103-103-170-130-175-103-153-1_V.mp3)
  Segment 17: 61576ms ~ 66652ms (file: 130-201-230-715-137-617-756-715-756-107-107-321-120-213-123-231-231-123-213-126-162-172-167-571-210-123-312-315-302-301-321-531-576-173-206-302-120-176-173-257-175-126-312-175-1_W.mp3)
  Segment 18: 67652ms ~ 76112ms (file: 721-372-126-210-260-206-123-136-106-237-216-750-302-307-307-301-132-106-310-163-103-106-106-172-231-125-123-103-130-316-136-130-321-132-312-132-123-321-123-231-275-123-326-320-3_W.mp3)
  Segment 19: 77112ms ~ 86544ms (file: 621-302-312-203-752-107-102-123-230-301-752-157-103-301-752-130-235-310-130-316-103-231-301-130-103-320-231-765-103-106-276-130-103-276-271-130-206-204-203-270-210-207-120-574-1_W.mp3)
  Segment 20: 87544ms ~ 88120ms (file: 176-103-236-672-321-361-301-123-132-623-756-675-576-675-756-107-137-675-325-527-502_W.mp3)
  Segment 21: 89120ms ~ 89696ms (file: 237-725-235-237-312-127-721-706-632-521-602-672-574-357-750-756-507-365-316-361-751-765-375_W.mp3)
  Segment 22: 90696ms ~ 98292ms (file: 267-231-263-123-310-326-136-361-326-261-235-325-236-356-201-327-267-752-267-123-572-574-265-267-627-267-236-237-321-123-102-753-130-132-120-236-567-132-160-657-265-263-236-206-1_W.mp3)
  Segment 23: 99292ms ~ 108796ms (file: 176-167-673-132-136-236-320-301-236-103-310-326-321-310-326-237-316-361-132-310-672-276-267-267-230-236-163-103-163-123-326-230-203-320-302-236-263-236-132-213-230-237-213-312-1_W.mp3)
  Segment 24: 109796ms ~ 110372ms (file: 176-710-170-715-176-170-713-175-175 105-175-107-175-170-107-715-705-175-715-715-715-751-715-715-751-715-751-751-715-710 167-170-710-136-137-107-175-170-715-173-715-760-715-715-7_V.mp3)
  Segment 25: 111372ms ~ 111948ms (file: 265-731-236-265-213-256-315-126-537-713-137-167-317-361-763-673-763-672-763-376-731-136-753-321-365-751-615-506-135-135-570-576-576-657-675-635-637-163-367-316-136-367-316-367-3_W.mp3)
  Segment 26: 112948ms ~ 113524ms (file: 136-316-136-613-165-165-312 623-265-261-132-257-673-123-256-276-751-523-321-231-320-752-162-263-312-231-572-635-316-351-136 756-576-756-367 570-574-574_V.mp3)
  Segment 27: 114524ms ~ 115100ms (file: 713-765-617-612-163-637-361-173-367-637-371-572-507-756-367-376-371-731-301-321-725-503-312-357-257-723-236-312-752-613-376-163-251-126-321-231-521-126-206-127-132_W.mp3)
  Segment 28: 116100ms ~ 125064ms (file: 752-275-751-175-107-312-107-301-302-230-701-756-236-753-735-310-750-317-750-756-130-103-312-132-301-321-276-230-320-301-103-132-102-123-132-102-716-672-621-213-210-216-210-216-1_W.mp3)
  Segment 29: 126064ms ~ 126640ms (file: 176-715 130-715-157-571-751-751-576-715-751-715-175-176-175-165-175-176-715-175-176-175-175-765-716 150-175-715-175-157-175-175-175-713-176-175-157-716-175-175-715-175-175-175-1_W.mp3)
  Segment 30: 127640ms ~ 133076ms (file: 102-126-237-126-217-106-106-102-103-135-315-136-132-175-310-326-102-372-103-132-320-271-172-127-172-132-102-175-127-162-273-321-213-132-231-325-231-103-130-312-312-317-231-312-3_V.mp3)
  Segment 31: 134076ms ~ 134652ms (file: 120-132-316-765-756-615-652-675-657-567-756-206-362-375-301-310-132-130-123-231-321-312-132-137-136-316-632-132-534-213-671-361-574-537-317-130-312 130-103-310-132 574-507-720-7_W.mp3)
  Segment 32: 135652ms ~ 136228ms (file: 567-726-705-257-507-267-257-507-523-132-572-673-103-326-230-236-527-216-752-725-257-236-275-257-753-265-517-571-756-137-572-571-507-573-526-752-357-567-567-231-756-576-571-317-1_V.mp3)
  Segment 33: 137228ms ~ 142664ms (file: 132-107-165-675-267-267-672-627-762-657-762-257-572-627-657-672-675-675-675-652-672-276-765-572-765-672-576-576-157-576-562-572-675-251-265-267-265-627-276-267-236-625-675-132-1_W.mp3)
  Segment 34: 143664ms ~ 144240ms (file: 625-120-367-317-273-132-576-237-312-236-261-276-267-321-165-273-735-705-371-725-726-357-237-162-762-723-265-132-670-317-327-753-753-325-532-235-257-316-126-507-231-725-265-513-3_V.mp3)
  Segment 35: 145240ms ~ 145816ms (file: 756-136-317-310-317-350-163-312-316-315-765-736-137-713-316-361-361-316-136-310-213-312-326-136-136-736-675-765-736-765-547-564-236-623-723-157-572-576-567-567-756-672-672-672-2_W.mp3)
  Segment 36: 146816ms ~ 147392ms (file: 105-765-560 132-726-325-136-216-203-506-275-571-267-526-256-236-527-326-750-725-527-326-235-536-705-207-723-163-570-627-265-576-326-725-237-357-627-507-256-572-627-267-502-257-5_V.mp3)
  Segment 37: 148392ms ~ 152856ms (file: 216-217-132-176-126-206-103-312-326-136-126-213-167-362-201-627-237-320-562-723-230-326-534-510-275-130-317-130-312-546-206-126-672-671-627-316-362-365-615-261-253-317-203-263-3_W.mp3)
  Segment 38: 153856ms ~ 154432ms (file: 715-756-715-715-713-715-715-175-175-175-175-175-175-715-176-576-156-120-516-765-576-167-576-657-657-156-572-657-576-157-157-756-567-576-657-176-516-576-576-756-710-710-170-701-7_W.mp3)
  Segment 39: 155432ms ~ 163172ms (file: 263-206-261-261-263-203-230-176-675-674-167-267-130-326-216-132-106-310-310-103-267-763-267-276-132-320-625-572-236-236-263-137-132-301-321-236-267-267-312-310-163-261-267-236-2_W.mp3)
  Segment 40: 164172ms ~ 164748ms (file: 537-312-132 165-716-713-765-765-675-673-675-672-763-765-675-752-571-710-176-167-160-162-163-610-627-651-167-372-275-752-316-673-731-705-126-136-163-513-162-617-574-567-175-571-3_W.mp3)
  Segment 41: 165748ms ~ 172120ms (file: 103-210-206-206-756-572-207-207-175-103-201-260-265-250-547-527-260-206-103-230-321-123-617-175-250-260-201-102-124-201-125-123-302-102-102-123-103-162-103-103-103-103-103-103-1_V.mp3)
  Segment 42: 173120ms ~ 178232ms (file: 301-571-176-167-160-651-567-265-621-671-106-107-751-567-756-137-502-256-301-326-570-756-750-627-275-135-672-671-136-327-310-317-760-267-275-310-236-261-756-627-257-532-562-267-3_W.mp3)
  Segment 43: 179232ms ~ 186720ms (file: 230-317-753-237-231-235-257-532-365-265-127-237-263-762-263-132-510-120-236-236-123-267-317-213-627-627-620-103-106-576-102-267-126-236-123-672-162-612-132-213-231-621-216-312-2_W.mp3)
  Segment 44: 187720ms ~ 196792ms (file: 752-203-231-320-230-237-572-572-103-103-176-320-103-137-137-321-230-321-320-132-127-321-523-235 267-263-261-260-621-216-261-260-260-261-612-321-260-206-260-327-752-102-216-132-2_W.mp3)
  Segment 45: 197792ms ~ 207368ms (file: 317-273-123-267-716-217-751-107-175-751-107-103-306-310-327-107-310-173-132-753-753-103-310-715-753-276-173-710-612-307-710-710-107 132-513-517-501-501-150-570-570-517-157-570-5_W.mp3)
  Segment 46: 208368ms ~ 208944ms (file: 130-135-135-135-135-517-157-156_W.mp3)
  Segment 47: 209944ms ~ 215056ms (file: 103-321-523-537-130-267-230-503-570-517-127-715-675-207-270-752-253-237-203-132-103-132-126-301-306-217-321-201-102-126-132-136-315-130-132-312-316-310-136-310-130-176-205-206-3_V.mp3)
  Segment 48: 216056ms ~ 216632ms (file: 132-136 765-671-756-765-671-571-574-574 537-537-576-675-763-736-761-172-576-537-713-103-673-160-715-761-675-572-120-564-316-376-157-263-231-237-312-132-136-136-136-160-327-572-7_W.mp3)
  Segment 49: 217632ms ~ 218208ms (file: 170-750 576-175-105-517-157-715-710-701-715-701-751-705-751-715-701-710-715-170-710-750 765-576-567-157-175-751-107-170-710-107-715-175-750-175-107-175-107-701-170-175-576 715-1_V.mp3)
  Segment 50: 219208ms ~ 219784ms (file: 315-357-317-123-376-356-765-367-576-537-653-657-356-573-567-735-573-357-765-317-137-173-136-371-317-136-316-617-617-361-570-720-637-316-136-130-672-321-571-312-167-132-715-132-3_W.mp3)
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
> **Time**: 2026-04-06 07:28:10
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "Do you know about kale? You know kale? Kale. Kale in the sea. Ah yeah. Everything old kale is dried kale. Yeah, and think about",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Do", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "know", "start_ms": 280, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "about", "start_ms": 480, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "kale?", "start_ms": 780, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "You", "start_ms": 1200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "know", "start_ms": 1350, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "kale?", "start_ms": 1550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "Kale.", "start_ms": 1900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "Kale", "start_ms": 2350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 2650, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 2800, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "sea.", "start_ms": 2950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "Ah", "start_ms": 3400, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "yeah.", "start_ms": 3600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "Everything", "start_ms": 4100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "old", "start_ms": 4600, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "kale", "start_ms": 4850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 5150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "dried", "start_ms": 5300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "kale.", "start_ms": 5700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "Yeah,", "start_ms": 6150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "and", "start_ms": 6350, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "think", "start_ms": 6450, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "about", "start_ms": 6650, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "국방 산업 기반까지 함께 세워주겠다는 이런 진정성은 국방 자립을 꿈꾸는 어느 나라에게나 거부하기 어려운 유혹일 수밖에 없습니다 그리고 그 최종 목표에 대해서는 대만 매체 자유시보의 보도에 따르면 한국 정부가 이미 2030",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "국방", "start_ms": 20, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "산업", "start_ms": 450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "기반까지", "start_ms": 900, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "함께", "start_ms": 1550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "세워주겠다는", "start_ms": 2000, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 2750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "진정성은", "start_ms": 3150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "국방", "start_ms": 3800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "자립을", "start_ms": 4250, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "꿈꾸는", "start_ms": 4800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "어느", "start_ms": 5300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "나라에게나", "start_ms": 5650, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "거부하기", "start_ms": 6400, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "어려운", "start_ms": 7050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "유혹일", "start_ms": 7550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "수밖에", "start_ms": 8050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "없습니다", "start_ms": 8550, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 9150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 9650, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "최종", "start_ms": 9850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "목표에", "start_ms": 10300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "대해서는", "start_ms": 10800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "대만", "start_ms": 11450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "매체", "start_ms": 11850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "자유시보의", "start_ms": 12250, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "보도에", "start_ms": 12950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "따르면", "start_ms": 13450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 13950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "정부가", "start_ms": 14350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이미", "start_ms": 14850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "2030", "start_ms": 15250, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "course, and Johnny's, you know, Johnny's. It's basically lemonade with the shrimp. Yeah. Right, right, right. Move on from that. I'm very excited. Maybe first bite, just see, oh",
"speaker": "speaker_3",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "course,", "start_ms": 50, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "and", "start_ms": 600, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "Johnny's,", "start_ms": 800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 1350, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "know,", "start_ms": 1550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "Johnny's.", "start_ms": 1900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "It's", "start_ms": 2450, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "basically", "start_ms": 2750, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "lemonade", "start_ms": 3400, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "with", "start_ms": 4000, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 4250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "shrimp.", "start_ms": 4450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "Yeah.", "start_ms": 5000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "Right,", "start_ms": 5350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "right,", "start_ms": 5700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "right.", "start_ms": 6050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "Move", "start_ms": 6500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "on", "start_ms": 6850, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "from", "start_ms": 7050, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "that.", "start_ms": 7350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "I'm", "start_ms": 7700, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "very", "start_ms": 7950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "excited.", "start_ms": 8300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "Maybe", "start_ms": 8850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "first", "start_ms": 9300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "bite,", "start_ms": 9650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "just", "start_ms": 10050, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "see,", "start_ms": 10400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "oh", "start_ms": 10750, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "위에 올랐을 뿐만 아니라 이번 시즌 시작 후 1골 10도움을 기록하며 고작 열 경기 만에 공격 포인트 두 자",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "위에", "start_ms": 40, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "올랐을", "start_ms": 350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "뿐만", "start_ms": 850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 1250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이번", "start_ms": 1750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "시즌", "start_ms": 2100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "시작", "start_ms": 2500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "후", "start_ms": 2900, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "1골", "start_ms": 3150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "10도움을", "start_ms": 3600, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "기록하며", "start_ms": 4300, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "고작", "start_ms": 4950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "열", "start_ms": 5350, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "경기", "start_ms": 5600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "만에", "start_ms": 6000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "공격", "start_ms": 6350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "포인트", "start_ms": 6750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "두", "start_ms": 7300, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "자", "start_ms": 7550, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "제법과 주권을 이유로 선을 긋자 트럼프 대통령은 계산기를 꺼내 들었습니다 매년 수천억 달러를 나토에 지출하고 있다며",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "제법과", "start_ms": 20, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "주권을", "start_ms": 500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이유로", "start_ms": 1000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "선을", "start_ms": 1500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "긋자", "start_ms": 1850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "트럼프", "start_ms": 2250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "대통령은", "start_ms": 2750, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "계산기를", "start_ms": 3400, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "꺼내", "start_ms": 4000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "들었습니다", "start_ms": 4350, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "매년", "start_ms": 5100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "수천억", "start_ms": 5500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "달러를", "start_ms": 6000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "나토에", "start_ms": 6500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "지출하고", "start_ms": 6950, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "있다며", "start_ms": 7550, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "다음에 반영할 줄도 안다",
"speaker": "speaker_6",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 50, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "반영할", "start_ms": 500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "줄도", "start_ms": 1000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "안다", "start_ms": 1350, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "금액 더 많은 금액을 결제하도록 유도할 수 있다는 얘기",
"speaker": "speaker_6",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "금액", "start_ms": 30, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 350, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "많은", "start_ms": 550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "금액을", "start_ms": 950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "결제하도록", "start_ms": 1450, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "유도할", "start_ms": 2150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 2650, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있다는", "start_ms": 2850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "얘기", "start_ms": 3300, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "또니 배고파요 어 왔다 왔다 왔다 왔다 오늘 점심은 돈가스입니다",
"speaker": "speaker_7",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "또니", "start_ms": 100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "배고파요", "start_ms": 450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "어", "start_ms": 1000, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "왔다", "start_ms": 1200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "왔다", "start_ms": 1550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "왔다", "start_ms": 1900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "왔다", "start_ms": 2250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 2600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "점심은", "start_ms": 2950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "돈가스입니다", "start_ms": 3450, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "에게 전격적인 폴란드 침공을 당하지 않기 위해서였습니다 그래서 폴란드는 막대한 돈다발을 들고 유럽의 전통적인 군수 산업 거인들인 독일과 프랑스의 문을 두드렸습니다 그리고 독일의 레오파르트 전차와 PZH 2000 자주포를 대량",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "에게", "start_ms": 30, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "전격적인", "start_ms": 300, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "폴란드", "start_ms": 900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "침공을", "start_ms": 1400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "당하지", "start_ms": 1950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "않기", "start_ms": 2450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "위해서였습니다", "start_ms": 2850, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 3700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "폴란드는", "start_ms": 4200, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "막대한", "start_ms": 4800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "돈다발을", "start_ms": 5300, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "들고", "start_ms": 5950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "유럽의", "start_ms": 6350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "전통적인", "start_ms": 6800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "군수", "start_ms": 7450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "산업", "start_ms": 7850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "거인들인", "start_ms": 8250, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "독일과", "start_ms": 8850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "프랑스의", "start_ms": 9350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "문을", "start_ms": 9900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "두드렸습니다", "start_ms": 10250, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 11050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "독일의", "start_ms": 11550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "레오파르트", "start_ms": 12000, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "전차와", "start_ms": 12750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "PZH", "start_ms": 13300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "2000", "start_ms": 13800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "자주포를", "start_ms": 14250, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "대량", "start_ms": 14900, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "에 방대한 터널망을 구축했을 가능성까지 제기하고 있습니다 이란 본토의 경비 태세도 한층 강화돼 주요 도시 길목마다 새로운 검문소가 설치됐",
"speaker": "speaker_8",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "에", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "방대한", "start_ms": 250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "터널망을", "start_ms": 750, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "구축했을", "start_ms": 1400, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "가능성까지", "start_ms": 2000, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "제기하고", "start_ms": 2800, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 3400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이란", "start_ms": 3950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "본토의", "start_ms": 4350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "경비", "start_ms": 4850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "태세도", "start_ms": 5250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "한층", "start_ms": 5750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "강화돼", "start_ms": 6150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "주요", "start_ms": 6650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "도시", "start_ms": 7050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "길목마다", "start_ms": 7450, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "새로운", "start_ms": 8100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "검문소가", "start_ms": 8600, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "설치됐", "start_ms": 9200, "dur_ms": 450, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 07:28:10*
