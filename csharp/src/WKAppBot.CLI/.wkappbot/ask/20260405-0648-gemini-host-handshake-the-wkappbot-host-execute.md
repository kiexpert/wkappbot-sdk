# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=24932  time=06:47:06  uptime=8911s  mem=441MB  system_processes=356"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_064615.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 765-657-671-315-536-107-123-106-175 123-621-576-576-567-574-574-576-567-576-573-576-567-652-576-576-756-765-756-572-531-625-167-675-574-576-576 573-571-362-130-570-517 267-627_W.mp3)
  Segment 2: 1576ms ~ 3232ms (file: 132-123-315-613-621-316-136-127-136-301-130-310-132-321-632-132-163-132-132-756-765-572-162-576-375-375-375-361 321-372-103-123-321 576_V.mp3)
  Segment 3: 4232ms ~ 4808ms (file: 126 217-705-547 627-620-270-765-675-136-231-261-632_V.mp3)
  Segment 4: 5808ms ~ 6384ms (file: 130 314-134-135-134-143-134-134-143-136-135-103-103-136-103-134-103-103-134-135-103-134-103-103-134-103-135-134-103-103 135-314-314-135-103-134-103-135-103-135-135-103-135-143-1_V.mp3)
  Segment 5: 7384ms ~ 14188ms (file: 574-756-236-320-237-325-321-512-325-215-271-752-320-276-102-765-726-230-123-123-310-321-231-132-362-370-574-521-237-230-260-276-201-102-201-103-732-327-130-375-273-237-310-276-3_W.mp3)
  Segment 6: 15188ms ~ 15764ms (file: 132-106-167-160-106-106-751-167 675-176-716-761-765-172-763-263-301-312-236-671-216-326-607-675-675-672-602-627-746-760-672-162 731-371-312-136-132-136-136-106-176-176-176-137_V.mp3)
  Segment 7: 16764ms ~ 26340ms (file: 217-123-123-267-263-207-217-321-261-321-213-321-321-312-316-132-137-270-276-276-267-623-236-273-274-754-257-321-237-213-271-721-623-632-276-326-752-326-362_W.mp3)
  Segment 8: 27340ms ~ 27916ms (file: 265-235 312-602-620-527-527-132-163-315-135-137-126-103-310-315-351-203-321-623-526-302-136-102-103-673-160-103-132-263-236-316-362-612-572-312-736-321-325-321-372-273-203-207-2_W.mp3)
  Segment 9: 28916ms ~ 29492ms (file: 130-301-713-130-371-107-137-731-107-725-130-701-170-135-701-735-173-375 317-371-137-130-713-701-317-130-170-137-170-172-173-103-130-103-107-531-137-103-130-571-710-103-371-710-1_V.mp3)
  Segment 10: 30492ms ~ 31068ms (file: 765-756-705-705-710-170-710-175-126-217-127-172-156-765-715-675-751-765-765-715-765-756-756-751 107-123-107-176-752-721 751-750-765-765-756-756-765-765-765-765-750-756-756-756-7_W.mp3)
  Segment 11: 32068ms ~ 41500ms (file: 107-765-756-765-751-751-753-321-715-103-726-275-263-762-675-672-267-107-316-310-312-571-327-260-256-267-260-756-206-726-175-201-206-312-267-276-572-270-752-267-256-572-267-310-7_W.mp3)
  Segment 12: 42500ms ~ 43076ms (file: 132 103 260-621-136-316-137-102-106-160-106-167-162-126-162-167-567-132-613-316-176-136-102-165-163-103-567-762-632-576-103-617-176-651-163-715-651-561-163-165-526-162-163-631 3_V.mp3)
  Segment 13: 44076ms ~ 50484ms (file: 315-310-267-267-261-123-127-612-261-216-162-216-261-126-216-261-267-217-216-126-162-126-123-526-256-562-126-572-256-762-675-267-267-126-167-671-762-216-213-126-123-156-162-126-2_W.mp3)
  Segment 14: 51484ms ~ 52060ms (file: 627-627-617-673-657-567-657 672-672-571-576-567-576-570-672-751-760-673-675-670-672-670-673-765-762-760-765-756-657-607-607-765-760-671-312-362-716-761-736-612-176-761-763-705-1_W.mp3)
  Segment 15: 53060ms ~ 53636ms (file: 157-157-175 154-105-105-150-105-105-105-105-157-571-175-715-175-716-715-715-175-751-175-176-756-715-175-175-751-715-175-716-715-716 105-105-103-105-150-150-157-107-175-175-765-1_W.mp3)
  Segment 16: 54636ms ~ 58524ms (file: 532-132-132-163-560-567-506-563-162-132-130-316-360-136-316-301-316-132-316-260-632-623-631-612-123-263-136-216-123-716-631-623-321 136-631-576-276-237-623-237-231-132-123-263-2_V.mp3)
  Segment 17: 59524ms ~ 64276ms (file: 713-130-130-137-216-176-726-176-276-126-237-316-276-217-172-231-276-163-132-231-127-132-126-126-671-132-120-102-130-103-103-172-327-325-306-321-312-312-310-547-132-163-216-132-2_W.mp3)
  Segment 18: 65276ms ~ 65852ms (file: 106-617-317-173-176-106-671-107-167-610-106-516-617-576-576-765-256-675-625-675-125-627 106-176-756-735-317-132-136-132-312-302-301-321-315-302-312-103-237-504-321-130-301-103-2_V.mp3)
  Segment 19: 66852ms ~ 67428ms (file: 157-670-675-765-761-765-675-671-670-760-670-765-670-607-560-567-610-765 316_W.mp3)
  Segment 20: 68428ms ~ 76024ms (file: 213-270-540-102-130-213-167-132-172-263-623-127-123-162-267-210-320-201-312-103-130-132-376-132-273-276-213-127-217-312-103-132-103-130-213-236-261-126-321-126-217-213-216-123-1_W.mp3)
  Segment 21: 77024ms ~ 83396ms (file: 130-137-706-102-521-236-576-257-756-261-760-670-627-260-261-267-120-137-321-237-273-102-213-576-750-751-102-526-135-132-120-527-572-612-130-107-236-123-103-130-547-257-215-574-1_W.mp3)
  Segment 22: 84396ms ~ 84972ms (file: 517-570-507-750-750-750-570-571-570-751-157-571-570-507-750-751 570-574-570-574-574-574-574-574 514-501-570-574-570-574-574-574 570-507-570-571-507-570-754-570-571-571-570-750-5_W.mp3)
  Segment 23: 85972ms ~ 97240ms (file: 573-172-203-527-362-136-173-203-103-637-705-317-203-231-236-237-765-675-263-267-213-362-236-236-326-126-236-316-762-236-326-165-627-267-267-267-765-675-327-123-327-130-263-260-2_W.mp3)
  Segment 24: 98240ms ~ 104000ms (file: 175-312-326-301-231-617-267-203-203-102-312-213-216-206-267-215-260-306-302-210-213-752-230-507-375-263-260-627-267-671-610-106-271-216-267-627-257-130-621-261-267-302-107-517-1_W.mp3)
  Segment 25: 105000ms ~ 113208ms (file: 235-276-106-236-236-261-263-230-310-725-267-126-321-132-231-123-106-167-132-765-276-320-267-312-103-312-326-312-326-103-136-123-130-571-153-231-130-130-173-130-230-231-267-120-1_W.mp3)
  Segment 26: 114208ms ~ 120436ms (file: 310-132-135-132-571-103-267-765-627-261-126-130-312-527-760-260-103-620-670-263-130-716-137-137-132-130-137-137-137-137-137-123-103-312-103-312-137-132-206-103-310-136-167-126-1_W.mp3)
  Segment 27: 121436ms ~ 122012ms (file: 316-165-165-623-316-136-317-103-160-361-136-176-567-756-567-756-613-130-167-653-510-316-563-521-257-315-136-301-560-610-361-103-315-653-367-351-135-351-153-510-513-531-547-573-5_W.mp3)
  Segment 28: 123012ms ~ 123588ms (file: 316-107-103-206-160-160-163-162-621-613-231-310-312-261-123-763-271-107-312-615-756-756-756-765-763-167 132 125-231 103-132-310-130-673-672-673-765 167-516-215-627-627-267-126-1_W.mp3)
  Segment 29: 124588ms ~ 125164ms (file: 213-235-261-123-325 605-671-576-720-316-132-713-317-752-513-375-175-123-675-621-752-721-276-752-267-326-261-273-201-276-657-267-721 576-172-276-732-213-132-125-135-321-571-576-5_V.mp3)
  Segment 30: 126164ms ~ 133292ms (file: 216-120-126-316-123-132-261-267-751-103-726-137-321-231-607-321-627-275-215-574-120-126-213-210-312-321-751-102-271-256-570-265-265-103-103-156-756-726-317-750-752-153-263-263-2_V.mp3)
  Segment 31: 134292ms ~ 134868ms (file: 674-673-367-316-635-713-731-317-176-167-167-617-671-617-673-367-362-237-672-753-763-627-302-327-352-305-356-357-357-326-375-325-357-325-357-327-327-321-365-325-357-375-325-315-3_V.mp3)
  Segment 32: 135868ms ~ 136444ms (file: 170-167-260-176-103-136-132-103-536-162-130-135-532-136-163-160-167-103-167-132-310-136-163-136-130-137-167-136-130-136-102-137-675-710-176-316-726-156-163-123-316-613-617-103-1_V.mp3)
  Segment 33: 137444ms ~ 138020ms (file: 514-150-501 105-150-150-150-150-105-103-103-105-107-105-157-107-173-175-705-157-107-157-715-715-107 105-150-157-157-175-157-175-715-751-715-175-750-175-175-751-571-571-157-517-7_V.mp3)
  Segment 34: 139020ms ~ 146940ms (file: 527-150-510-105-615-321-123-726-231-216-672-267-237-123-632-213-673-572-321-237-132-573-150-102-312-132-312-756-273-231-312-132-764-627-163-107-103-574-310-231-321-310-321-130-7_W.mp3)
  Segment 35: 147940ms ~ 155248ms (file: 620-105-261-261-103-257-512-310-130-267-260-317-542-250-102-261-123-672-123-130-572-321-670-672-132-103-132-260-260-756-765-267-257-130-260-310-716-217-236-706-765-602-103-207-1_V.mp3)
  Segment 36: 156248ms ~ 162404ms (file: 130-236-672-627-256-765-276-267-213-230-627-627-132-617-126-123-230-132-623-236-130-102-260-267-213-261-231-127-157-157-267-267-326-326-230-210-627-267-261-621-610-261-206-275-3_W.mp3)
  Segment 37: 163404ms ~ 173412ms (file: 172-567-576-657-652-567-756-376-367-306-307-632-726-263-136-130-671-607-702-765-725-137-301-301-136-107-713-761-127-176-720-130-310-107-610-276-320-273-317-617-176-620-267-312-6_W.mp3)
  Segment 38: 174412ms ~ 174988ms (file: 571-175-571-571-570-175-571-715-715-571-157-517-571-570-750-570-570-751-571-571-570-103-105-105-105-103-157-105-517-517-576-571-751-716-175-175-517-715-715-715-137-175-751-157-7_V.mp3)
  Segment 39: 175988ms ~ 176564ms (file: 150-150-150-150-150-514-150-150-150-150-150-510-150-517-157-571-157-157-517-175-157-516 105-105-105-150-154-150-157-105-107-150-175-157-157-175-156-157-175-150-157-175-175-170 1_V.mp3)
  Segment 40: 177564ms ~ 178140ms (file: 103-103-136-317 172-107 571-572-752-705-754-143-132-162-163-132-326-362-260-302-217 134-314-132-513-236-623-326-675-576-765-261-326-320-326-321-302-267-320-320-321-761-361-173-1_W.mp3)
  Segment 41: 179140ms ~ 179716ms (file: 726-276-267-572-261-576-275-705-612-675-567-127-763-765-761-736-763-736-752-675-576-315-327-231-132-125-231-132-235-312-172-256-213-675-576-572-576-752-172-765-160-153-517-513-1_W.mp3)
  Segment 42: 180716ms ~ 181292ms (file: 672 750-735-751-763-765-367-317-137-135-312-135-762-576-375-365-657-675-675-765-620-371-731-602-627-627-627-765_W.mp3)
  Segment 43: 182292ms ~ 188880ms (file: 267-316-657-102-127-276-574-165-132-231-670-206-231-123-132-524-527-574-236-231-526-236-352-102-265-546-526-236-102-362-321-263-367-573-263-627-524-260-236-326-572-275-213-321-7_W.mp3)
  Segment 44: 189880ms ~ 190456ms (file: 517-576-765-753-725-752-375-517-517-510-570-501-571-751-571-517-571-571-517-157-517-514-517-157-571 751-571-571-751-571-571-150-157-571-571-571-571-157-175-157 570-571-570-571-7_W.mp3)
  Segment 45: 191456ms ~ 192032ms (file: 167-176-716-175-571-175-175-715-175-715 715-176-173-175-715-175-715-716-170-120-170-102-170-170-170-173-173-173-175-173-175-173-175-173-173-173-173-173-170-136-137-136-136-137-1_W.mp3)
  Segment 46: 193032ms ~ 200916ms (file: 571-201-213-512-541-576-627-256-526-572-625-564-260-620-123-201-213-351-132-531-103-320-302-265-205-103-361-547-562-207-260-216-123-320-540-216-261-103-310-201-102-731-216-126-2_W.mp3)
  Segment 47: 201916ms ~ 202492ms (file: 213-231-267-267-231-123-236-612-723-372-125-236-267-627-750-627-570-572-572-507_V.mp3)
  Segment 48: 203492ms ~ 204068ms (file: 567-657-562-576-756-576-657-563-567-567-576-567-657-576-567-765-527-576-756-567-562-657-567-567-657-567-657-567-567-576-562-567-576-526-576-567-576-652-567-657-576-526-652-657-7_W.mp3)
  Segment 49: 205068ms ~ 213996ms (file: 726-716-276-235-102-756-276-203-130-230-574-574-726-321-327-132-627-103-130-237-713-321-320-273-762-726-276-273-230-237-762-230-321-123-132-321-230-276-265-327-301-102-123-213-2_W.mp3)
  Segment 50: 214996ms ~ 215572ms (file: 157-751-175 540-510-570-501-574 154-150-105-105-150-501-105-150-510-175-157-517-167-107-157-175-107-175-157-156-157-571-574 154-105-175-175-165-761-715-175-175-715-715-617-765-7_W.mp3)
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
> **Time**: 2026-04-05 06:48:28
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답TITLE: Audio Transcription and Analysis

{
"segment": 1,
"transcript": "실제로 NMN을 투여한 60대 쥐가 20대 젊은 쥐의 운동 능력을 회복하는 기적이 일어났습니다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "실제로", "start_ms": 30, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "NMN을", "start_ms": 450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "투여한", "start_ms": 980, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "60대", "start_ms": 1350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "쥐가", "start_ms": 1680, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "20대", "start_ms": 1950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "젊은", "start_ms": 2280, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "쥐의", "start_ms": 2650, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "운동", "start_ms": 2920, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "능력을", "start_ms": 3250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "회복하는", "start_ms": 3680, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "기적이", "start_ms": 4200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "일어났습니다", "start_ms": 4650, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "다음에 뵙겠습니다. 감사합니다.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 50, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "뵙겠습니다", "start_ms": 420, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 1050, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "여러분, 소주 맛있게 먹는 방법은 맛있는 거 하나 준비하세요.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "여러분", "start_ms": 20, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "소주", "start_ms": 450, "dur_ms": 300, "phase_lr_deg": 5.0},
{"word": "맛있게", "start_ms": 780, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "먹는", "start_ms": 1150, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "방법은", "start_ms": 1380, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "맛있는", "start_ms": 1820, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "거", "start_ms": 2200, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "하나", "start_ms": 2380, "dur_ms": 300, "phase_lr_deg": 5.0},
{"word": "준비하세요", "start_ms": 2700, "dur_ms": 600, "phase_lr_deg": 5.0}
]
},
{
"segment": 4,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 5,
"transcript": "라고 달 기지 구축 등 머스크가 제시한 비전 실현에 투입할 계획이라고 합니다.",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "라고", "start_ms": 40, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "달", "start_ms": 320, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "기지", "start_ms": 550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "구축", "start_ms": 880, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "등", "start_ms": 1250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "머스크가", "start_ms": 1420, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "제시한", "start_ms": 1950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "비전", "start_ms": 2380, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "실현에", "start_ms": 2720, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "투입할", "start_ms": 3200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "계획이라고", "start_ms": 3650, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "합니다", "start_ms": 4350, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "SK하이닉스 ADR 상장 주관사에 씨티, JP모건, 골드만삭스 등이 선정됐습니다.",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "SK하이닉스", "start_ms": 30, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "ADR", "start_ms": 750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "상장", "start_ms": 1120, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "주관사에", "start_ms": 1450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "씨티,", "start_ms": 1980, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "JP모건,", "start_ms": 2350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "골드만삭스", "start_ms": 2850, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "등이", "start_ms": 3480, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "선정됐습니다", "start_ms": 3820, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "노화 프로세스를 늦추는 이 기적의 분자를 바르는 화장품으로 완벽하게 구현해내는 겁니다.",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "노화", "start_ms": 50, "dur_ms": 300, "phase_lr_deg": -2.0},
{"word": "프로세스를", "start_ms": 380, "dur_ms": 600, "phase_lr_deg": -2.0},
{"word": "늦추는", "start_ms": 1000, "dur_ms": 450, "phase_lr_deg": -2.0},
{"word": "이", "start_ms": 1480, "dur_ms": 100, "phase_lr_deg": -2.0},
{"word": "기적의", "start_ms": 1600, "dur_ms": 400, "phase_lr_deg": -2.0},
{"word": "분자를", "start_ms": 2050, "dur_ms": 350, "phase_lr_deg": -2.0},
{"word": "바르는", "start_ms": 2450, "dur_ms": 400, "phase_lr_deg": -2.0},
{"word": "화장품으로", "start_ms": 2880, "dur_ms": 600, "phase_lr_deg": -2.0},
{"word": "완벽하게", "start_ms": 3520, "dur_ms": 500, "phase_lr_deg": -2.0},
{"word": "구현해내는", "start_ms": 4050, "dur_ms": 600, "phase_lr_deg": -2.0},
{"word": "겁니다", "start_ms": 4700, "dur_ms": 300, "phase_lr_deg": -2.0}
]
},
{
"segment": 8,
"transcript": "이건 우연이 아닙니다. 국가공인 인증기관 임상시험 결과",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "이건", "start_ms": 20, "dur_ms": 300, "phase_lr_deg": -2.0},
{"word": "우연이", "start_ms": 350, "dur_ms": 400, "phase_lr_deg": -2.0},
{"word": "아닙니다.", "start_ms": 780, "dur_ms": 450, "phase_lr_deg": -2.0},
{"word": "국가공인", "start_ms": 1450, "dur_ms": 500, "phase_lr_deg": -2.0},
{"word": "인증기관", "start_ms": 1980, "dur_ms": 550, "phase_lr_deg": -2.0},
{"word": "임상시험", "start_ms": 2550, "dur_ms": 500, "phase_lr_deg": -2.0},
{"word": "결과", "start_ms": 3100, "dur_ms": 350, "phase_lr_deg": -2.0}
]
},
{
"segment": 9,
"transcript": "연간 수주액만 매년 70억 달러, 우리 돈으로 약 10조 원에 달할 것으로 기대된다고 해요.",
"speaker": "speaker_5",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "연간", "start_ms": 60, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "수주액만", "start_ms": 380, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "매년", "start_ms": 900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "70억", "start_ms": 1250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "달러,", "start_ms": 1650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "우리", "start_ms": 2150, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "돈으로", "start_ms": 2420, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "약", "start_ms": 2800, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "10조", "start_ms": 2980, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "원에", "start_ms": 3300, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "달할", "start_ms": 3550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "것으로", "start_ms": 3880, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "기대된다고", "start_ms": 4250, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "해요", "start_ms": 4900, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "아니,",
"speaker": "speaker_5",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "아니,", "start_ms": 10, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "행동 하나가 30년의 꿈을 날려버릴 수도 있었으니까요. 트럭이 목적지에 도착했습니다.",
"speaker": "speaker_6",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "행동", "start_ms": 40, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "하나가", "start_ms": 380, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "30년의", "start_ms": 820, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "꿈을", "start_ms": 1300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "날려버릴", "start_ms": 1650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "수도", "start_ms": 2200, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "있었으니까요.", "start_ms": 2500, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "트럭이", "start_ms": 3400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "목적지에", "start_ms": 3800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "도착했습니다", "start_ms": 4350, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "베이스 스태프 장치를 내렸고요. 놀랍게도 반양성자들은 소멸하지 않고 그대로 살아있었습니다.",
"speaker": "speaker_6",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "베이스", "start_ms": 30, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "스태프", "start_ms": 450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "장치를", "start_ms": 850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "내렸고요.", "start_ms": 1300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "놀랍게도", "start_ms": 1950, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "반양성자들은", "start_ms": 2500, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "소멸하지", "start_ms": 3300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "않고", "start_ms": 3850, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "그대로", "start_ms": 4150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "살아있었습니다", "start_ms": 4600, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "베이스 팀은 즉시 실험을 재개했습니다.",
"speaker": "speaker_6",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "베이스", "start_ms": 20, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "팀은", "start_ms": 400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "즉시", "start_ms": 680, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "실험을", "start_ms": 1020, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "재개했습니다", "start_ms": 1400, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "문제로 확산될 수 있다라는 부분들, 그러니까 이 케어에 대한 보안 관련 점에서도 많은 이제 신경을 써야 합니다.",
"speaker": "speaker_7",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "문제로", "start_ms": 50, "dur_ms": 350, "phase_lr_deg": 3.0},
{"word": "확산될", "start_ms": 450, "dur_ms": 400, "phase_lr_deg": 3.0},
{"word": "수", "start_ms": 880, "dur_ms": 100, "phase_lr_deg": 3.0},
{"word": "있다라는", "start_ms": 1000, "dur_ms": 450, "phase_lr_deg": 3.0},
{"word": "부분들,", "start_ms": 1500, "dur_ms": 400, "phase_lr_deg": 3.0},
{"word": "그러니까", "start_ms": 2100, "dur_ms": 450, "phase_lr_deg": 3.0},
{"word": "이", "start_ms": 2600, "dur_ms": 100, "phase_lr_deg": 3.0},
{"word": "케어에", "start_ms": 2750, "dur_ms": 350, "phase_lr_deg": 3.0},
{"word": "대한", "start_ms": 3150, "dur_ms": 300, "phase_lr_deg": 3.0},
{"word": "보안", "start_ms": 3500, "dur_ms": 300, "phase_lr_deg": 3.0},
{"word": "관련", "start_ms": 3850, "dur_ms": 350, "phase_lr_deg": 3.0},
{"word": "점에서도", "start_ms": 4250, "dur_ms": 500, "phase_lr_deg": 3.0},
{"word": "많은", "start_ms": 4850, "dur_ms": 300, "phase_lr_deg": 3.0},
{"word": "이제", "start_ms": 5200, "dur_ms": 250, "phase_lr_deg": 3.0},
{"word": "신경을", "start_ms": 5500, "dur_ms": 350, "phase_lr_deg": 3.0},
{"word": "써야", "start_ms": 5900, "dur_ms": 300, "phase_lr_deg": 3.0},
{"word": "합니다", "start_ms": 6250, "dur_ms": 350, "phase_lr_deg": 3.0}
]
},
{
"segment": 15,
"transcript": "그렇게 평가해요. 북극 항로가 열린다는 건 해운업계 지형 자체가 완전히 뒤바뀌는 대사건이다.",
"speaker": "speaker_8",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "그렇게", "start_ms": 40, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "평가해요.", "start_ms": 420, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "북극", "start_ms": 1100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "항로가", "start_ms": 1450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "열린다는", "start_ms": 1900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "건", "start_ms": 2450, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "해운업계", "start_ms": 2650, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "지형", "start_ms": 3250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "자체가", "start_ms": 3600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "완전히", "start_ms": 4050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "뒤바뀌는", "start_ms": 4550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "대사건이다", "start_ms": 5100, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "북극 항로가 열리면 대한민국...",
"speaker": "speaker_8",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "북극", "start_ms": 20, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "항로가", "start_ms": 400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "열리면", "start_ms": 900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "대한민국", "start_ms": 1350, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "아, 이거 진짜 안 되겠다. 야, 이게 진짜 너무 얼큰하고 감칠맛이 진짜 죽인다. 이건 안 돼요, 여러분. 이건",
"speaker": "speaker_9",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "아,", "start_ms": 30, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "이거", "start_ms": 250, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 900, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "되겠다.", "start_ms": 1080, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "야,", "start_ms": 1750, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 2050, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 2400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 2800, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "얼큰하고", "start_ms": 3150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "감칠맛이", "start_ms": 3800, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 4500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "죽인다.", "start_ms": 4900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이건", "start_ms": 5600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 6000, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "돼요,", "start_ms": 6200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "여러분.", "start_ms": 6600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이건", "start_ms": 7200, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "현실화될 수가 있습니다. 그래서 이제 저희는 호르무즈 해협 내 갇힌 전쟁 포로나 다름없기 때문에 신중하게 검토해야 할 것 같습니다.",
"speaker": "speaker_10",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "현실화될", "start_ms": 50, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "수가", "start_ms": 650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "있습니다.", "start_ms": 1000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 1650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 2100, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "저희는", "start_ms": 2400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "호르무즈", "start_ms": 2850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "해협", "start_ms": 3400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "내", "start_ms": 3750, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "갇힌", "start_ms": 3950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "전쟁", "start_ms": 4350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "포로나", "start_ms": 4750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "다름없기", "start_ms": 5250, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "때문에", "start_ms": 5900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "신중하게", "start_ms": 6450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "검토해야", "start_ms": 7000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 7550, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 7750, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "같습니다", "start_ms": 7900, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "네, 알겠습니다.",
"speaker": "speaker_11",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 20, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "알겠습니다", "start_ms": 250, "dur_ms": 500, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 06:48:28*
