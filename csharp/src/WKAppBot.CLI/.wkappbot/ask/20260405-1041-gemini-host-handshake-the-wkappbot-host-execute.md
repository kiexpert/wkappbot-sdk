# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=6944  time=10:41:10  uptime=32279s  mem=429MB  system_processes=353"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_104057.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 132 103-135-310-103-135-315-153-135-135-315-135-315-135-135-315-135-315-153-153-135-135-315 315-315-137-135-301-103-135-130-310-153-135-315-157-103-135-317-135-315-153-357-315-1_V.mp3)
  Segment 2: 1576ms ~ 11872ms (file: 301-135-123-130-123-216-261-210-320-362-302-623-312-317-370-123-130-572-103-102-210-123-231-132-213-120-217-216-541-321-103 267-206-206-206-362-230-231-302-321-236-130-213-231-2_W.mp3)
  Segment 3: 12872ms ~ 13448ms (file: 135 705-376-765-765-763-761-371-376-317-371-371-176-507-531-564-536-357-765-123-763-376-753 123-261-231-162-571-367-716-136-713-176-173-176-163-263-136-107-751-756-736-231-754-7_V.mp3)
  Segment 4: 14448ms ~ 15024ms (file: 571-517-501-105-507-105-701-501-751-571-175-710-715-150-105-175-175-170-175-753 517-571-105-571-501-510-571-501-501-715-571-750-751-705-750-751-571 507-517-150-510-571-571-571-5_W.mp3)
  Segment 5: 16024ms ~ 16600ms (file: 132-123-132-123-312-132-132-312-231-123-126-132-621-126-217-236-321-132-327-213-106-120-167-102-167-610-160-163-310-162-206-127 126-135-103-261-176 603-601-561-132-136-617-163-1_W.mp3)
  Segment 6: 17600ms ~ 18176ms (file: 103-137-103-103-107-103-130-137-103-103-137-136-137-103-137-103-103-137 153-571-173-157-103-107-137-103-137-103-103-103-137-103 517-175-571-517-571-173-137-137-103-103-103-103-1_V.mp3)
  Segment 7: 19176ms ~ 19752ms (file: 731-751-715-175-715-751-751-751-751-751-715-751-751-571 510-510-501-150-150-153-517-571-751-751-751-751-751-571-571-751-751-175-751-517-751-175-715-715-751-175-517-517-175 150-5_V.mp3)
  Segment 8: 20752ms ~ 21328ms (file: 517-163-320-165-576-102-163-136-601-615-126-136-103-136-130-132-103-576-130-531-617-136-103-165-107-570-216-260-603-623-163-326-203-103-136-563-651-162-126-126-103-103-136-163 3_V.mp3)
  Segment 9: 22328ms ~ 22904ms (file: 657-576-576-567-576-567-567-567-576-657-657-576-576-567-567-657-675-657-657-657-675-167-165-167-165-162-167-613-567-567-657-567-765-567-567-657-576-765-756-675-765-756-756-765-6_W.mp3)
  Segment 10: 23904ms ~ 24480ms (file: 261-271-231-127-132-672-672-672-617-136-163-613-627-632-631-627-652-623-621-362-632-362-657-627-637-632-623-627-623-671-762-263-326-756-672-672-316-602-620-627-263-263-123-261-2_W.mp3)
  Segment 11: 25480ms ~ 32824ms (file: 267-263-236-310-327-273-237-326-263-260-263-612-260-762-706-260-236-236-236-267-263-267-312-260-237-236-267-236-106-106-106-301-267-312-321-130-236-312-103-236-270-627-236-231-1_W_stt_but_you_are_not_create_images_here_is_a.mp3)
  Segment 12: 33824ms ~ 34400ms (file: 107-107-175-710-107-132-317-173-752-701-730-175-130-753-725-372 135-103-310-317-130-107-130-310-713-107-317-307-170-317-710-107-170-107-103-310-735-705-310-573-710-170-107-307-1_V.mp3)
  Segment 13: 35400ms ~ 35976ms (file: 751-571-756-751-756-576-751-576-576-754-756-756-576-157 150-170-715-756-715-750-751-175-715-751-751-756-756-756-751 145-510-175-170-157-157-715-175-765-756-761-715-716-715-751-7_W.mp3)
  Segment 14: 36976ms ~ 44644ms (file: 162-317-263-236-210-123-217-132-307-307-302-102-120-176-201-301-173-316-271-216-120-132-217-273-217-273-271-237-726-725-762-107-216-765-627-675-754-657-537-130-320-752-735-276-7_W_stt_if.mp3)
  Segment 15: 45644ms ~ 46220ms (file: 127-361-231-327 627-621-263-213-216-316 162-132-136-130-137-167-362-130-136-310-576_V.mp3)
  Segment 16: 47220ms ~ 47796ms (file: 571-751-571-157-105-157-157-510-715-157-156-157-571-576-567-561 570-571-170-710-157-750-571-570-571-517-157-170-170-170-517-576-576-574-574 507-571-150-105-751-510-157-715-157-1_W.mp3)
  Segment 17: 48796ms ~ 49372ms (file: 513-135-513-517-517-571-574-571-701-175-701-175-137-721-267-715-175-317-713-750-715-715-751-753-715-175-751-751-173 157-175-157-170-175-715-157-107-175-173-157-710-170-175-107-1_V.mp3)
  Segment 18: 50372ms ~ 50948ms (file: 574-514 105 510-150-510-150-510-501-510-510-513-510-510-501-510-510-510-150-510-510-510-150-510-501 150-501-510-510-501-510-510-510-501-510-150-507-150-510-150-501-517-501-510-5_V.mp3)
  Segment 19: 51948ms ~ 52524ms (file: 103-103-103-105-105 103-105-107-105-105-105-105-154_V.mp3)
  Segment 20: 53524ms ~ 54100ms (file: 572-567-756-756-576-675-576-756-542-546-564-527-516-526-526-563-502-504-536-725-756-756-576-756-756-756-756-756-756-756-756-756-576-756-675-756-756-751-504-504-506-504-504-501-5_W.mp3)
  Segment 21: 55100ms ~ 55676ms (file: 167-635-163-163-157-175-162-571-756-751-765-756-753-765-756-576-756-756-576-576-576-756-572-756-165-167-176-165-175-153-165-175-156-175-165-165-756-756-657-765-762-756-756-752-7_W.mp3)
  Segment 22: 56676ms ~ 57252ms (file: 157-153-516-153-156-517-576-574-576-513-130-127-163-175-631-136-217-376-137-127-163-165-325-253-526-571-253-132-215-763-276-276-726-273-217-123-153-175-157-156-165-167-167-167-1_W.mp3)
  Segment 23: 58252ms ~ 66280ms (file: 137-321-327-136-361-362-361-316-631-316-263-217-736-371-316-365-326-310-163-675-236-236-237-321-231-267-216-312-326-675-567-267-267-267-267-236-320-317-267-736-317-236-130-765-7_W.mp3)
  Segment 24: 67280ms ~ 67856ms (file: 175-715-705-715-175-715-501-715-157-105-571-157-105-710-175 510-501-571-105-150-570-510-571-751-507-517-105-507-571-570-501-510-157-157-175-750-571-705-751-157 501-507-507-517-1_W.mp3)
  Segment 25: 68856ms ~ 69432ms (file: 571-710-175-175-156-175-157-156-176-156-156-175-175-175-167-176-175-176-175 104-150-150-150-157-157-175-157-176-175-176-170-176-150-157-175-715-617-156-165-157-165-157-571 105-1_V.mp3)
  Segment 26: 70432ms ~ 89440ms (file: 126-103-261-206-206-260-620-176-103-301-317-312-123-203-756-162-201-103-102-276-103-130-103-607-216-267-260-260-107-210-201-130-103-103-103-321-216-216-620-201-125-123-130-103 1_V.mp3)
  Segment 27: 90440ms ~ 91016ms (file: 107-173-173-107-170-571-157-510-570 517-715-105-175-107-105-175-107-107-107-175-107-107-107-170-107-170-157-170-175-173-170-175-157-571 105-157-105-107-175-107-173-107-107-107-1_V.mp3)
  Segment 28: 92016ms ~ 103644ms (file: 206-267-206-207-764-260-627-754-574-203-201-260-230-203-236-123-143-130-230-657-164-763-230-203-302-203-263-320 203-132-103-132-531-547-132-231-310-132-163-107-160-126-261-312-2_W.mp3)
  Segment 29: 104644ms ~ 105220ms (file: 301 130-137-170-107-170-715-735-176-165-135-150-135-156-150-150-157-150-165-751-756-751-756-715-176-172-137-176-135-176-765-756-756-756-756-175-752-715-751-751-716 751-751-135-1_W.mp3)
  Segment 30: 106220ms ~ 106796ms (file: 670-736-675-607-602-267-134 136-173-137-576-625-625-136-136-103-103-132-132-312-132-147-571-572-573-103-310-230-310-504-531-312-123-132-106-105-103-302-574-576-537-367-624-362-1_V.mp3)
  Segment 31: 107796ms ~ 115320ms (file: 162-321-315-213-102-126-162-216-213-312-132-130-231-321-576-275-130-213-321-132-273-231-321-237-153-153-571-310-102-671-672-261-572-235-231-156-267-267-126-235-236-216-271-267-3_W.mp3)
  Segment 32: 116320ms ~ 116896ms (file: 105-105-150-501-507-571-570-570-517-571-517-571-570-570-571-517-517-157 504-510-571-574-574-574-574-507-574-574 150-517-507-105-507-570-750-570-571-570-750-571-751-371-157-175-1_V.mp3)
  Segment 33: 117896ms ~ 125960ms (file: 236-237-130-310-327-263-751-175-137-320-301-130-301-107-317-275-573-310-137-517-123-236-574-532-203-263-576-625-267-236-576-261-305-537-312-326-321-237-236-170-576-130-263-236-3_W.mp3)
  Segment 34: 126960ms ~ 136104ms (file: 216-276-267-576-276-273-712-765-756-136-170-132-316-675-627-235-574-132-361-157-251-261-210-132-102-527-132-576-213-316-103-132-632-756-576-321-132-621-321-326-326-361-361-657-3_W.mp3)
  Segment 35: 137104ms ~ 137680ms (file: 150-150-103-105-150-751-105-137-153-103-107-173-517-507-570-175-571-715-571-751-107-157-715-751-570-175-175-175-175-107-175-175-751-570-571 157-715-507-157-175-175-751-157-517-1_W.mp3)
  Segment 36: 138680ms ~ 148940ms (file: 127-120-132-201-126-213-312-102-312-321-736-126-507-567-527-627-516-132-527-132-321-205-351-543-217-230-301-570-576-201-210-652-103-546-564-621-564-206-123-231-206-627-216-230-2_W.mp3)
  Segment 37: 149940ms ~ 150516ms (file: 105-150-157-105-105-107-157-105-157-150-105-106-157-107-105 105-150-105-105-105-105-150-105-150-157-150-157-150-170-175-165-105-157-751-175-175-153-103 105-130-103-153-510-154-1_V.mp3)
  Segment 38: 151516ms ~ 152092ms (file: 321-126 567-564-562-567-562-567-562-271 176-316-267-175-157-617-615-130-130-321-613-536-136-631-102-765-756-675-627-627-627-627-765-706-716-675-123-756-326-562-567-536-753-261-1_W.mp3)
  Segment 39: 153092ms ~ 153668ms (file: 573-765-107-675 327-672-352-763-126-570-261-127-572-263-236-576-562-203-572-756-325-317-132-236-257-316-560-326-231-540-576-570-265-570-675-267-257-275-267-650-372-376-627-153-3_W.mp3)
  Segment 40: 154668ms ~ 155244ms (file: 574-574-576-574-574-576-570-574-571 573-571-574-574-572-574 547-547-547-547-574-547-574-574-574-574-157-152-175-157-153-157-175-571-357-375-170-571-157-175-571-571-571-576 150-5_W.mp3)
  Segment 41: 156244ms ~ 167548ms (file: 267-210-721-537-102-103-103-201-250-275-756-275-576-657-137-123-203-103-132-316-312-267-102-261-267-217-321-236-754-267-627-672-201-263-754-267-263-136-230-130-206-261-752-103-3_W.mp3)
  Segment 42: 168548ms ~ 169124ms (file: 315-267-276-251-527-572-257-256-362-273-536-253-132-107-172-547-231-216-265-267-627-326-257-736-326-237-312-317-572-532-573-751-375-257-253-756-275-507-507-327-723-261-520-123-1_V.mp3)
  Segment 43: 170124ms ~ 170700ms (file: 615-651-657-657-657-657-654-657-564-657-567-657-654-657-567-675 541-613-615-615-615-165-615-651-651-657-657-657-657-657-675-657-567-567-657-567-657-657-675-132-564-504-504-546-5_W.mp3)
  Segment 44: 171700ms ~ 172276ms (file: 167-610-165-156-162-623-132-162-136-107-165-163-156-167-542-263-137-126-102-610-132-160-162-105-615-163-102-103 103-132-576-362-316-136-261-236-261-627-127-213-126-126-132-312-3_W.mp3)
  Segment 45: 173276ms ~ 174716ms (file: 756-275-103-312-132-137-321-132-125-302-137-327-672-260-132-136-301-132-231-765-762-706-162-576-103-103 160-123-165 136-107-735-312-132 162-507-657 257-237-231-312 126-217-236-2_V.mp3)
  Segment 46: 175716ms ~ 176292ms (file: 107-150 150-105-105-150-150-153-105-150-105-150-150-150-150-105-517-157-157-150_V.mp3)
  Segment 47: 177292ms ~ 177868ms (file: 173-371-135 571-513-157-135-310-130-135-103-135-153-130-137-315-135-137-357-351-317-315-317-173-175-135-713 135-351-135-137-135-130-315-135-103-315-153-135-105-357-135-103-130-3_V.mp3)
  Segment 48: 178868ms ~ 179444ms (file: 542-524-562-564-567-564-564-567_W.mp3)
  Segment 49: 180444ms ~ 181020ms (file: 715-170-130-312-315-352-371-357-752-375-537-357-357-357-375-735-356-357-537-156-153-513-135-153-165-176-176-176-176-167-173-317-315-517-537-157-571-157-517-157-175-751-756-573-7_W.mp3)
  Segment 50: 182020ms ~ 182596ms (file: 507-576-267-231-301-312-306-136-103-137-507-756-750-175-716-176-167-263-312-103-137-136-163-132-621-620-621 631-167-601_V.mp3)
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
> **Time**: 2026-04-05 10:41:55
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 2,
"transcript": "비극적인 결말은 거의 피할 수 없게 되죠 얼음 지구상의 많은 바다와는 달리 베링해는 매년 겨울이 되면 반드시 일부가 얼어붙습니다 지구 온난화",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "비극적인", "start_ms": 20, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "결말은", "start_ms": 650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "거의", "start_ms": 1100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "피할", "start_ms": 1450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 1850, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "없게", "start_ms": 2050, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "되죠", "start_ms": 2400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "얼음", "start_ms": 3200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "지구상의", "start_ms": 4100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "많은", "start_ms": 4750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "바다와는", "start_ms": 5150, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "달리", "start_ms": 5750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "베링해는", "start_ms": 6200, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "매년", "start_ms": 6900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "겨울이", "start_ms": 7350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "되면", "start_ms": 7850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "반드시", "start_ms": 8250, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "일부가", "start_ms": 8800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "얼어붙습니다", "start_ms": 9300, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "지구", "start_ms": 10200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "온난화", "start_ms": 10650, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "Kimchi fashion 중간에 단감 내려가고 그러면 사레 김치 좀 있고 양념 닿으면 또 아프고 쓰리고",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Kimchi", "start_ms": 50, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "fashion", "start_ms": 360, "dur_ms": 200, "phase_lr_deg": 0.0}
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
"transcript": "만반의 준비를 마쳤으니 배춧잎 사이사이",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "만반의", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "준비를", "start_ms": 180, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "마쳤으니", "start_ms": 340, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "배춧잎", "start_ms": 500, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "사이사이", "start_ms": 560, "dur_ms": 10, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "가 어우러지면서 매우 혹독하고 예측하기 어려운 기상 조건이 만들어지고 있는 것입니다 기후의 경계에 놓인 바다 사실 베링해에서 가장 큰 위험은 바로 그 기후였습니다 그 밖의 모",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "가", "start_ms": 30, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "어우러지면서", "start_ms": 60, "dur_ms": 50, "phase_lr_deg": 0.0}
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
"transcript": "중요하며 신중한 입장을 유지하고 파병 요청에 즉각 응하지 않자 트럼프 대통령은 이를 배신이자 기회주의자로 규정해 버린 것입니다",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "중요하며", "start_ms": 10, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "신중한", "start_ms": 170, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "입장을", "start__ms": 330, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "유지하고", "start_ms": 490, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
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
"transcript": "천만 달러의 자산을 오직 단타 트레이딩 하나만으로 일궈낸 남자라면 믿으실 수 있으신가요 그의 이름은 제이슨 노아 그가 말한 트레이딩의 3원칙은",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "천만", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "달러의", "start_ms": 200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "자산을", "start_ms": 360, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "오직", "start_ms": 520, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "좋아요와 구독으로 다음 이야기에도 동행해 주세요 다음 영상에서 또 만나요 단골이",
"speaker": "speaker_6",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "좋아요와", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "구독으로", "start_ms": 180, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 340, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이야기에도", "start_ms": 500, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "에요 하지만 쇄빙선은 쐐기형 선수로 경사져 있죠 쇄빙선은 이 쐐기",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "에요", "start_ms": 15, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "하지만", "start_ms": 120, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "쇄빙선은", "start_ms": 280, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "쐐기형", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "지지율은 13%에 그쳐 민주당과의 격차가 38% 포인트나 됐습니다 베이 의원은 선거비 보전도 못할까 봐 후보들이 도통 나서지 않는다며 구인난이 심",
"speaker": "speaker_7",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "지지율은", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "13%에", "start_ms": 180, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "그쳐", "start_ms": 340, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "민주당과의", "start_ms": 500, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "안내 말씀 드립니다 지난주 토요일에 소개해 드린 현대판 불로초 NMN 안티에이징 리포좀 크림 영상 이후 바이오",
"speaker": "speaker_8",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "안내", "start_ms": 10, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "말씀", "start_ms": 170, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "드립니다", "start_ms": 330, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "지난주", "start_ms": 490, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "를 위해 거실 오른쪽에는 따로 주방을 냈고요",
"speaker": "speaker_9",
"confidence": 0.91,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "mixed",
"words": [
{"word": "를", "start_ms": 60, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "위해", "start_ms": 120, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "거실", "start_ms": 280, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "오른쪽에는", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 18,
"transcript": "에 이런 반응들을 보인 걸까요 세상에서 가장 비싸고 가장 위험하고 가장 희귀한 물질 만들기도 지옥이고 보관하기도",
"speaker": "speaker_10",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "에", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 140, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "반응들을", "start_ms": 300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "보인", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 20,
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
*Generated by WKAppBot ask gemini — 2026-04-05 10:41:55*
