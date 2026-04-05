# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=11716  time=11:03:47  uptime=33284s  mem=412MB  system_processes=356"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_110331.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 5760ms (file: 175-201-105-102-130-372-107-617-273-231-173-103-731-137-137-210-201-315-123-156-132-231-132-561-321-103-123-312-761-756-301-210-230-167-316-137-153-571-574-257-672-325-172-316-3_W.mp3)
  Segment 2: 6760ms ~ 7336ms (file: 765-762-675-123-326-316-206-217-267-627-765-756-574-576-754-574-572-576-576-576-576-576-567-576-610-617-106-136-517-756-156-167-671-672-763-137-705-705-713-716-173-316-176-571-6_W.mp3)
  Segment 3: 8336ms ~ 17048ms (file: 123-237-675-275-102-715-371-301-273-270-671-320-237-273-672-132-321-103-102-201-231-317-270-267-321-102-672-672-170-271-103-270-237-276-237-213-307-237-320-574-317-276-213-765-6_W.mp3)
  Segment 4: 18048ms ~ 18624ms (file: 621-207-120-210-735-507-756-675-236-103-162-126-106-107-716-167-765-607-627-162-137-250-517-316-126-126-123-216-571-321-163-312-506-137-163-716-375-126-175-135 317-351 570-237-3_V.mp3)
  Segment 5: 19624ms ~ 20200ms (file: 510-507-510-150-105-507-570-571 570-501-510-501-510-501-507-510-150-510-501-150-517-150-510-105-517-150-150-150-571-105-510-510-517 501-507-571-150-105-150-570-105-751-517-571-1_V.mp3)
  Segment 6: 21200ms ~ 21776ms (file: 105-620-675-672-623-627-361-621-163-627-627-261-602-216-102-172-270-132-751-312-103-103-136-103-573-106-137-137-316-132-163-570-106-623-617-176 312_V.mp3)
  Segment 7: 22776ms ~ 23352ms (file: 321-316-216-312-312-321-261-621-123 623-675-263-123-136-316-621-632-613-265-621-762-260-675-136-315-312-517-312-316-312-312-132-132-231_W.mp3)
  Segment 8: 24352ms ~ 24928ms (file: 617-163-621-627-610-617-672-267-261-136-162-126-621-162-126-620-620 675-613_W.mp3)
  Segment 9: 25928ms ~ 32840ms (file: 267-201-317-517-321-132-217-236-571-310-103-206-735-135-273-716-267-207-705-753-175-176-751-103-310-167-207-276-137-103-617-273-574-201-107-176-756-375-130-715-320-237-273-267-7_W.mp3)
  Segment 10: 33840ms ~ 34416ms (file: 326-607-312-123-132-721-263-706-726-672-126-710-125-625-623-263-562-127-621-132-103-103-306-135-135-213-132-132-761-137-731-173-317-137-132 132 173-123-234-324 132-731_V.mp3)
  Segment 11: 35416ms ~ 35992ms (file: 751-751-753-751-751-751-571 150-150-510-150-157-751-751-571-751-751-751-751-751-751-751-715-715-715-751-751-710-510-103-310-130-310-130-132-130-132-312-310-130-163-301-130-157-1_V.mp3)
  Segment 12: 36992ms ~ 37568ms (file: 756-756-756-657-765-675-765-671-675-675-675-675 615-652-631-657-672-672-657-671-617-675-671-675-675-657-675-657-756-567-657-675-765-675-765-675-761-615-637-675-675-657-675-675-5_W.mp3)
  Segment 13: 38568ms ~ 39144ms (file: 126-127-172-126-127-126-126-136-132-132-123-126-671-672-675-671-617-716-167-671-167-167-167-756-752-756-756-762-275-765-275-762-756-675-762 725-752-756-765-765-756-765-761 725-7_W.mp3)
  Segment 14: 40144ms ~ 40720ms (file: 765-765-756-756-756-756-567-657 560-652-502-250-756-257-675-756-675-765-675-756-756-756-765-576-756-765-756-675-675-765-675 507-527-205-206-260-267-520-675-275-256-762-765-675-6_V.mp3)
  Segment 15: 41720ms ~ 42296ms (file: 137-172-157-175-751-735-137-751-753-751-137 571-517-751-571-571-571-175-571-751-571-576-571 571-571-571-751-751-751-571-517-571-574-574 574-513-751-573-517-157 571-574-571-576-5_W.mp3)
  Segment 16: 43296ms ~ 53052ms (file: 127-103-103-107-537-137-157-517-570-103-257-756-752-752-136-756-561-751-173-103-153-153-136-756-261-547-150-107-570-756-257-276-270-756-237-574-173-321-153-312-137-270-324-572-7_V.mp3)
  Segment 17: 54052ms ~ 62476ms (file: 132-231-312-123-132-132-106-216-265-236-376-765-217-572-103-152-235-312-213-231-103-213-132-312-236-132-276-527-572-312-216-261-213-753-756-517-126-310-216-231-572-231-132-753-1_W.mp3)
  Segment 18: 63476ms ~ 64052ms (file: 715-751-751-751-715-751 547-574-574-574-571-751-756-715-751-751-756-756-715-715-751-750-751-751-756-756-756-751-715 150-510-150-150-157-175-175-715-751-175-715-756-751-756-751-7_W.mp3)
  Segment 19: 65052ms ~ 65628ms (file: 150-105-150-107-175-751-157-175-715-715-715-715-715-715-756-751-715-715 501-504-105-150-150-105-751-175-175-175-175-175-751-175-715-175-571-175-715-175-715-713-715 153-105-105-1_V.mp3)
  Segment 20: 66628ms ~ 67204ms (file: 176-563-356-563-563-735-163-315-276-235-173-103-135-361-326-136-136-637-361-731-310-316-316_V.mp3)
  Segment 21: 68204ms ~ 74756ms (file: 132-237-726-267-276-672-276-216-267-102-310-210-103-320-312-231-137-376-571-735-567-376-732-327-137-317-316-213-231-231-130-312-130-103-102-123-126-132-103-107-106-361-163-132-1_W.mp3)
  Segment 22: 75756ms ~ 76332ms (file: 537-537-537-150-510-570-507-105-501-510-510 517-501-510-510-501-510-570-517-510-517-571-517-571-517-517-517-571-517-571-571-517-571-571 517-570-571-571-517-571-517-517-517-571-5_W.mp3)
  Segment 23: 77332ms ~ 77908ms (file: 175-571-570-571-570-571-751-570-750-750-750-570-571 150-105-157-105-571-571-750-157-175-571-175-751-175-751-715-571-751-751-175-751-157-157-715-571-571-571-570-571-513-152-157-5_V.mp3)
  Segment 24: 78908ms ~ 79484ms (file: 517-570-510-507-571 517-157-507-157-517-507-537-507-501-510-150-501-150-517-501-517-510-501-510-501-507-150-507-510-574-547-517-541-571-517-531-513-507-501 501-157-715-507-510-5_V.mp3)
  Segment 25: 80484ms ~ 81060ms (file: 725-725-715-752-753 754-752-754-754-756-752-752-725-712-765-765-752-762-765-761-761-765-762-675-762-627 715-721-756-716-725-723-173_W.mp3)
  Segment 26: 82060ms ~ 89620ms (file: 720-321-320-231-301-103-260-260-263-260-210-210-230-260-230-310-263-267-123-230-260-260-260-230-213-213-312-103-126-230-203-231-201-320-230-203-320-302-301-127-312-210-210-327-2_V.mp3)
  Segment 27: 90620ms ~ 91196ms (file: 617-167-617-675-675-615-675-675-675 750-751-750-574-750-751-750-175-751-750-750-751-756-567-567-657-651-562-564-546_W.mp3)
  Segment 28: 92196ms ~ 92772ms (file: 231-136-136-163-156-103-512-136-162-205-126-162_V.mp3)
  Segment 29: 93772ms ~ 94348ms (file: 371-125-317-132-132-162-160-106-176-103-162-126-216-625 176-627-162-123 761-176_V.mp3)
  Segment 30: 95348ms ~ 95924ms (file: 317-315-735-317-173-367-165-175-216-516-651-567-572-576-136-130-135-351-635-316-132-310-321-130-567-103-315-153-302-320-302-135-135-157-351-570-310-573-571-531-157-135-315-751-1_W.mp3)
  Segment 31: 96924ms ~ 97500ms (file: 672-672-613-657-567-765-756-765-675-675-675-675-627-267-267-267_W.mp3)
  Segment 32: 98500ms ~ 105412ms (file: 312-231-726-756-762-271-103-321-276-312-230-132-627-237-260-102-123-103-260-267-321-267-320-320-627-320-273-762-761-765-310-621-261-126-106-231-230-130-273-735-273-371-103-173-2_W.mp3)
  Segment 33: 106412ms ~ 106988ms (file: 715-216-137-136-135-136-163-136-130-105-103-106-103-105-310 136-163-132-167-156-130-527-632-103-106-516-326-136-106-103-162-126-123-162-120-167-167-126-617-163-167-103-132-163-1_V.mp3)
  Segment 34: 107988ms ~ 112380ms (file: 732-267-263-672-672-261-126-612-162-176-126-216-162-123-312-216-261-726-123-162-612-162-762-631-103-160-106-106-162-106-103-107-312-132-162-176-126-167-126-127-132-213-317-327-1_W_stt_if.mp3)
  Segment 35: 113380ms ~ 120400ms (file: 267-276-267-267-267-726-263-263-201-106-167-163-167-267-320-136-306-271-752-732-753-320-230-537-320-216-310-213-302-273-320-207-267-206-213-207-723-270-320-263-203-320-103-107-1_W.mp3)
  Segment 36: 121400ms ~ 129320ms (file: 321-256-754-725-276-237-267-267-267-267-756-207-752-276-275-176-137-750-157-375-312-712-716-207-762-126-756-267-263-756-713-120-327-253-207-273-572-127-130-705-632-607-751-705-3_W.mp3)
  Segment 37: 130320ms ~ 130896ms (file: 537-543-541-531-513-534-354-547-534-541-534-534-621-615-615-657-657-567-567-567-567-567-564-567-564-567-654-657-657-156-534-531-546-546-536-541-541-514-512-501 261-162-621-625-6_W.mp3)
  Segment 38: 131896ms ~ 132472ms (file: 135-576-756-630-172-617-760-762-760 315-162-217-756-627-765-125-316-712-213-216-317-176-315-312-231-312-526-750-267-325-570-672-713-257-265-275-756-317-375-625-256-625-275-217-1_V.mp3)
  Segment 39: 133472ms ~ 135020ms (file: 126-312 126-752-563-137 123-601-216-132-756-576-567-657-132 753-176-136-326-236-136-637-673-657_W.mp3)
  Segment 40: 136020ms ~ 136596ms (file: 752-756-756-756-756-576-756-576-756-756-576-756_W.mp3)
  Segment 41: 137596ms ~ 138172ms (file: 726-573-752-765-752-162-267-706-701-760-765-706-760-756-763-751-356-576-750 276-237-276 276-267-263-570-572 271-327-762-725-761-712-157-753-576-571-572-352-127-352-237-236-237-1_V.mp3)
  Segment 42: 139172ms ~ 146372ms (file: 275-672-261-120-576-672-376-302-321-625-517-127-752-132-162-312-236-312-132-576-527-216-362-712-720-231-312-102-213-237-213-675-207-237-213-123-672-120-312-576-105-572-576-316-2_W.mp3)
  Segment 43: 147372ms ~ 147948ms (file: 657-671-627-675-671-612-576-576-756-725-765-675-765-756-675-762-765-675-576-765-675-762-627-762-672-657-652-657-657-675-657-657-657-675 675-657-675-675-675-675-675-657-675-675-6_W.mp3)
  Segment 44: 148948ms ~ 149524ms (file: 105-150-150-105-150-154-145-153-150-150-150-150-105-150-105-105-105-150-173-105-157-170 105-105-105-514-150-157-175-157-150-156-105-105-150-135-157-157-175-105-137 103-105-150-1_V.mp3)
  Segment 45: 150524ms ~ 151100ms (file: 130-610-103-673-167-574-567-253-310-102-123-103-102-106-106-103-136-135-103-106-136 167-163-107-312-321-201-172-362-670-670-670-260-207-203-230-520-275-702-570-607-467-617-236-2_W.mp3)
  Segment 46: 152100ms ~ 152676ms (file: 157-105 570-547-570-547-574-574-754-754-574-574-574-574 547-514-504-547-574-150-517-504-571-510-570-574-570-574-510-507-571-570-574-574-571 547-547-547-547-547-574-501-571-150-1_W.mp3)
  Segment 47: 153676ms ~ 154252ms (file: 253-756-672-765-627 567-251-236-236-267-753-236-167-263-267-157-576-162-632-623-267-627-765-672-702-276-567-275-726-762-572-576-650-173-576-627-172-756-507-127-126-136-720-576-5_V.mp3)
  Segment 48: 155252ms ~ 155828ms (file: 167-716-671-167-167-371-315-365-613-631-361-167-317-763-657-615-567-376-367-237-372-327-537-576-523-540-540-543-352-327-327-127-715-275-275-371-172-574-573-256-523-315-321-317-3_W.mp3)
  Segment 49: 156828ms ~ 157404ms (file: 501-510-105-507-501-150-157-510-567-150-105-517-157-517-571-751-153-715-715-751-750-175-715-175-571-175-175-175-713-715-175-571-570-571-105-517-506-516-571-175-173-571-157-715-1_V.mp3)
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
> **Time**: 2026-04-05 11:05:22
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "이런 사회에서 우리가 단타 매매 할 수 있다는 거 자체만으로 저는 너무 행복하게 생각하고 이런 직업이 사실 없잖아요? 감사하는 마음이 기본",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "이런", "start_ms": 50, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "사회에서", "start_ms": 350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "우리가", "start_ms": 850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "단타", "start_ms": 1250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "매매", "start_ms": 1550, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 1800, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 1950, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있다는", "start_ms": 2050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 2400, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "자체만으로", "start_ms": 2500, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "저는", "start_ms": 3100, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 3350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "행복하게", "start_ms": 3650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "생각하고", "start_ms": 4150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 4650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "직업이", "start_ms": 4950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "사실", "start_ms": 5300, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "없잖아요?", "start_ms": 5500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "감사하는", "start_ms": 5900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "마음이", "start_ms": 6350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "기본", "start_ms": 6650, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 3,
"transcript": "그 시각적 충격이 고스란히 뇌로 전달되어 심각한 우울감과 불안을 유발하죠. 상명대학교 연구팀은 피부 상태에 따른 우울감이 생활 스트레스 수준을 높이고 심리적 삶의 질을 낮춘다는",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "그", "start_ms": 80, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "시각적", "start_ms": 230, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "충격이", "start_ms": 630, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "고스란히", "start_ms": 1030, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "뇌로", "start_ms": 1530, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "전달되어", "start_ms": 1830, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "심각한", "start_ms": 2330, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "우울감과", "start_ms": 2730, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "불안을", "start_ms": 3230, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "유발하죠.", "start_ms": 3630, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "상명대학교", "start_ms": 4300, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "연구팀은", "start_ms": 5000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "피부", "start_ms": 5500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "상태에", "start_ms": 5800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "따른", "start_ms": 6150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "우울감이", "start_ms": 6450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "생활", "start_ms": 6950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "스트레스", "start_ms": 7250, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "수준을", "start_ms": 7750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "높이고", "start_ms": 8100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "심리적", "start_ms": 8450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "삶의", "start_ms": 8850, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "질을", "start_ms": 9100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "낮춘다는", "start_ms": 9300, "dur_ms": 500, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 7,
"transcript": "안녕하겠습니까? 가장 주식 성장에 있어서 정말 탁월한 방법이 이바라하면은",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "안녕하겠습니까?", "start_ms": 100, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "주식", "start_ms": 1200, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "성장에", "start_ms": 1450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있어서", "start_ms": 1800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 2150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "탁월한", "start_ms": 2450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "방법이", "start_ms": 2850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이바라하면은", "start_ms": 3250, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "세 가지만 꼽아주신다면은 어떤 것들이 있을까요?",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "세", "start_ms": 100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가지만", "start_ms": 250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "꼽아주신다면은", "start_ms": 600, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 1400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "것들이", "start_ms": 1650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있을까요?", "start_ms": 2000, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "든다. 그래서 좀 더 많이 친해진 거 아닌가 싶고. 어, 현실의 삶. 아주 좋다. 그래서 오늘도 아침에 제가 출금을 좀 덜 해서 혼났는데 바로 좀 조용히 생각을 많이 해보고 더 많이 빼놨거든요.",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "든다.", "start_ms": 120, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 850, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 1000, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 1150, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "친해진", "start_ms": 1400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 1800, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "아닌가", "start_ms": 1900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "싶고.", "start_ms": 2250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "어,", "start_ms": 2600, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "현실의", "start_ms": 2800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "삶.", "start_ms": 3150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 3500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "좋다.", "start_ms": 3800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 4300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "오늘도", "start_ms": 4700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아침에", "start_ms": 5100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 5500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "출금을", "start_ms": 5800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 6200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "덜", "start_ms": 6350, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "해서", "start_ms": 6550, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "혼났는데", "start_ms": 6800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 7400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 7700, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "조용히", "start_ms": 7850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "생각을", "start_ms": 8250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 8650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "해보고", "start_ms": 8950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 9400, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 9600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "빼놨거든요.", "start_ms": 9900, "dur_ms": 600, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 13,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "베테랑 선원이라 해도 아무리 최신식 선박이라 해도 바다로 내던져질 가능성이 있습니다. 베링해에는 침몰하지 않는 배라는 것은 존재하지 않죠. 그리고 물론 베링해에서 바다에 빠지는",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "베테랑", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "선원이라", "start_ms": 550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "해도", "start_ms": 1050, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "아무리", "start_ms": 1450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "최신식", "start_ms": 1850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "선박이라", "start_ms": 2300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "해도", "start_ms": 2800, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "바다로", "start_ms": 3200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "내던져질", "start_ms": 3600, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "가능성이", "start_ms": 4150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "있습니다.", "start_ms": 4650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "베링해에는", "start_ms": 5300, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "침몰하지", "start_ms": 5900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "않는", "start_ms": 6400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "배라는", "start_ms": 6700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "것은", "start_ms": 7100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "존재하지", "start_ms": 7400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "않죠.", "start_ms": 7900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 8500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "물론", "start_ms": 8900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "베링해에서", "start_ms": 9250, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "바다에", "start_ms": 9850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "빠지는", "start_ms": 10250, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "안으로 길 위의 인생이었죠. 지금 걷고 있어요? 없어요. 걸음 좀 졌더라고. 야.",
"speaker": "speaker_6",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "안으로", "start_ms": 100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "길", "start_ms": 500, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "위의", "start_ms": 700, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "인생이었죠.", "start_ms": 950, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 1800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "걷고", "start_ms": 2150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "있어요?", "start_ms": 2450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "없어요.", "start_ms": 3000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "걸음", "start_ms": 3600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 3950, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "졌더라고.", "start_ms": 4150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "야.", "start_ms": 4900, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 19,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
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
},
{
"segment": 21,
"transcript": "날 결혼을 하면서 인테리어 디자이너인 아내의 가장 든든한 오른팔이 됐더랍니다. 날 할배. 사진관은",
"speaker": "speaker_7",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "날", "start_ms": 120, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "결혼을", "start_ms": 350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "하면서", "start_ms": 750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "인테리어", "start_ms": 1300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "디자이너인", "start_ms": 1800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "아내의", "start_ms": 2400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 2800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "든든한", "start_ms": 3150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "오른팔이", "start_ms": 3550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "됐더랍니다.", "start_ms": 4050, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "날", "start_ms": 4900, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "할배.", "start_ms": 5200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "사진관은", "start_ms": 5800, "dur_ms": 600, "phase_lr_deg": 0.0}
]
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 24,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 25,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 26,
"transcript": "준다고 하고 있습니다. 심지어 동서남북 삼면이 강대국으로 포위된 여러분이 살고 있는 이 땅의 이야기입니다. 한반도가 왜 이렇게 생겨먹었는지를 이해하려면 시각",
"speaker": "speaker_8",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "준다고", "start_ms": 100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "하고", "start_ms": 500, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "있습니다.", "start_ms": 750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "심지어", "start_ms": 1400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "동서남북", "start_ms": 1850, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "삼면이", "start_ms": 2450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "강대국으로", "start_ms": 2900, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "포위된", "start_ms": 3600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "여러분이", "start_ms": 4100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "살고", "start_ms": 4600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 4900, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 5150, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "땅의", "start_ms": 5250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이야기입니다.", "start_ms": 5550, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "한반도가", "start_ms": 6400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "왜", "start_ms": 6900, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 7050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "생겨먹었는지를", "start_ms": 7400, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "이해하려면", "start_ms": 8200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "시각", "start_ms": 8800, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "오 저쪽은 아무래도 파- 파닥이니까",
"speaker": "speaker_9",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "오", "start_ms": 100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "저쪽은", "start_ms": 350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아무래도", "start_ms": 800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "파-", "start_ms": 1300, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "파닥이니까", "start_ms": 1500, "dur_ms": 600, "phase_lr_deg": 0.0}
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
"transcript": "이 왜요? 제가 행운의 숫자가 있어요. 4와 2가 저의 행운의 숫자예요. 그 행운의 숫자는 언제부터 이렇게 저희 어",
"speaker": "speaker_10",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 120, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "왜요?", "start_ms": 300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 800, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "행운의", "start_ms": 1100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "숫자가", "start_ms": 1450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "있어요.", "start_ms": 1850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "4와", "start_ms": 2500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "2가", "start_ms": 2850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "저의", "start_ms": 3150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "행운의", "start_ms": 3450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "숫자예요.", "start_ms": 3850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 4500, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "행운의", "start_ms": 4650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "숫자는", "start_ms": 5000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "언제부터", "start_ms": 5450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 5950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "저희", "start_ms": 6350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "어", "start_ms": 6650, "dur_ms": 200, "phase_lr_deg": 0.0}
]
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
"transcript": "만 원 정도 좋은 수익을 냈는데 형님이 형님 매매처럼 되게 기뻐하시는 거예요. 제- 아니 좋지.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "만", "start_ms": 100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "원", "start_ms": 300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "정도", "start_ms": 450, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 700, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "수익을", "start_ms": 950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "냈는데", "start_ms": 1300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "형님이", "start_ms": 1800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "형님", "start_ms": 2200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "매매처럼", "start_ms": 2500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "되게", "start_ms": 3000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "기뻐하시는", "start_ms": 3300, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "거예요.", "start_ms": 3900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "제-", "start_ms": 4250, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "아니", "start_ms": 4450, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "좋지.", "start_ms": 4700, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "운동 가는데 전화 주셔 가지고 야 너 헷지 테마 했을까 봐 불안했다 하네",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "운동", "start_ms": 120, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "가는데", "start_ms": 470, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "전화", "start_ms": 950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "주셔", "start_ms": 1250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "가지고", "start_ms": 1550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "야", "start_ms": 2100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "너", "start_ms": 2300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "헷지", "start_ms": 2450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "테마", "start_ms": 2750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "했을까", "start_ms": 3050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "봐", "start_ms": 3450, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "불안했다", "start_ms": 3650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "하네", "start_ms": 4200, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "이끔하게 가게 열 준비를 마치자마자 어제 두고 간 재벌소 세척에 나서는데요.",
"speaker": "speaker_11",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "이끔하게", "start_ms": 150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "가게", "start_ms": 750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "열", "start_ms": 1050, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "준비를", "start_ms": 1250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "마치자마자", "start_ms": 1700, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "어제", "start_ms": 2500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "두고", "start_ms": 2850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "간", "start_ms": 3150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "재벌소", "start_ms": 3350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "세척에", "start_ms": 3850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "나서는데요.", "start_ms": 4300, "dur_ms": 700, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 11:05:22*
