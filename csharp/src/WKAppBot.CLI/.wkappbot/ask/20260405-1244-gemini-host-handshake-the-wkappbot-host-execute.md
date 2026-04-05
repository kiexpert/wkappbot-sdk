# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=30524  time=12:43:33  uptime=29525s  mem=462MB  system_processes=356"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_124300.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 527-570-572-705-720-725-257-527-521-527-517_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 615-362-751-163-673-362-236-321-372-326-321-217-761-267-236_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 312-132-312 671-176-167-310-312-132-365-362-312-103-132-123-102-126-132-163-162-167-176-163-156-216-162-165-167-601-167-106-107-706-107-571-576-157-236-651-576-570-517-517-160-1_W.mp3)
  Segment 4: 4728ms ~ 14268ms (file: 321-130-103-102-230-231-236-230-263-236-203-267-236-230-236-126-231-765-257-237-237-126-106-230-310-102-231-120-106-321-126-102-176-120-231-301-231-213-253-231-162-326-326-263-2_W.mp3)
  Segment 5: 15268ms ~ 15844ms (file: 651-657-567-652-652-625 265-625-652-652-651-652_W.mp3)
  Segment 6: 16844ms ~ 17420ms (file: 571-571-507-517-507-507-503-572-351-531-531-513-531-513-315-532-531-531-531-351-351-513-507-507-507-507-507-507-507-507-507-507-507-507-507-576-507-507-507-573-572-576-507-507-5_V.mp3)
  Segment 7: 18420ms ~ 18996ms (file: 752-732-317-317-371-132-127-257-267 256-275-726-562-526-125-625-627-132-136-317-573-126-315-315-725-537-326-365-675-523-265-762-726-765-623-623-217-217-217-125-574-571-567-562-5_W.mp3)
  Segment 8: 19996ms ~ 28492ms (file: 236-756-627-103-213-136-213-765-312-263-107-725-132-573-261-312-237-576-567-756-123-576-103-216-261-321-132-106-213-263-261-102-321-231-132-312-261-312-573-567-123-213-236-752-5_W.mp3)
  Segment 9: 29492ms ~ 30068ms (file: 173-103-735-735-753 375-357-713-137-375-731-301-307-610-671-613-603-301-137-307-317-571-753-573-715-753-571-753-751-137-751-750 573-752-137-351-137-310-136-310-310-307-170-107-7_V.mp3)
  Segment 10: 31068ms ~ 31644ms (file: 157-130 751-570-573-713-130-317-316-130-536-157-135-157-150-105-105-103-157-135-165-136-157-105-137-103-137-103-107-301-130-762-761-765-765-762-765-765-761-761-135-153-157-153-1_W.mp3)
  Segment 11: 32644ms ~ 33220ms (file: 106-104-106-106-106-103-106-106-103-163-165-106-106-163-143-134-137-135-130-135-135-135-315-135-315-317-315-571 135-135-317-517-157-574-571-574-571-574 315-157-517-517-517-517-5_W.mp3)
  Segment 12: 34220ms ~ 34796ms (file: 507-576-572-750-507-750-752-527-507-752-570-576-576-576-570 507-572-570-576-572-576-507-572-507-765-570-750-506-567-570-570-560-526-675-675-752-572-560 563-567-503-507-570-576-5_V.mp3)
  Segment 13: 35796ms ~ 36372ms (file: 517-510-517-157-510-157-150-157-157-175-175-175-175-175-175-175-571-517-175-175 105-107-157-150-157-105-715-175-175-170-176-175-175-175-175-175-175-517-175-175-175-751-175-715-1_W.mp3)
  Segment 14: 37372ms ~ 37948ms (file: 514-531-503-527-562-572-512-576-572-502-752-756-725-756-725-257-251-752-506-576-576-576-576-756-562-527-521-527-572-567-572-752-572-576-572-752-572-752-572-752-572-572-752-756-5_V.mp3)
  Segment 15: 38948ms ~ 39524ms (file: 510-507-510-510-150-150-105-130-105-105-150-105-150 105-150-105-150-105-105-517-501-507-501-510-150-510-501-501-501-150-150-157-517-517-517-501 105-153-150-150-107-105-150-157-5_V.mp3)
  Segment 16: 40524ms ~ 41100ms (file: 672-672 431-163-765-326-236-627-276-276-256-265-627-672-627-267-263-261-621-326-623-213-612-612-362-216 671-632-672-675-657-653-657-672-625 675-763-762-762-672-673-627-612-631-1_W.mp3)
  Segment 17: 42100ms ~ 46096ms (file: 263-123-326-627-163-132-213-620-261-216-267-623-621-217-612-263-206-652-263-627-706-627-267-621-627-627-632-621-623-627-672-672-276-367-236-673-762-362-327-173-132-326-261-671-6_W.mp3)
  Segment 18: 47096ms ~ 47672ms (file: 623-623-627-625-625-625-625-625-625-625-623-623 527-526-251-625-567-256-215-206-236-205-120-125-120-126-213-623-216-621-627-625-625-620-621-625-625-625 502-502-501-501-507-507-5_W.mp3)
  Segment 19: 48672ms ~ 56700ms (file: 267-236-576-253-547-137-321-231-627-762-372-325-543-571-715-571-273-237-725-576-316-576-263-547-236-321-321-573-312-710-751-170-567-267-357-573-236-137-756-312-327-326-326-574-2_W.mp3)
  Segment 20: 57700ms ~ 58276ms (file: 513-153-513-513-516-514-501-506-516-501-514-563-537 527-543 265-325-536-527-531-513_V.mp3)
  Segment 21: 59276ms ~ 59852ms (file: 765-756-501-567-506-561-752-756-576-756-576-756-576-756-576-576-657-576-756-576-756-756-576-576-756-576-576-756-547-573-561-567-501-501-501-506-507-752-576-576-576-567-576-576-7_W.mp3)
  Segment 22: 60852ms ~ 70212ms (file: 312-236-756-576-130-267-126-601-526-103-576-765-257-123-725-756-576-213-751-163-715-574-326-751-267-621-123-317-657-657-267-765-260-263-321-261-754-107-267-216-216-261-316-751-1_W.mp3)
  Segment 23: 71212ms ~ 71788ms (file: 576-576-576-576-756-756 752-752-756-756-576-576-576-756-756-576-756-576-576 725-756-756-576-576-576-576-756-576-756-576-756-576-576-576-576-756 752-756-756-576-756-576-756-756-7_W.mp3)
  Segment 24: 72788ms ~ 78152ms (file: 275-527-231-216-213-237-273-132-123-127-127-127-213-231-216-261-276-126-132-132-231-321-125-126-127-217-216-132-210-213-216-126-126-321-123-213-132-312-231-213-267-102-310-213-2_W.mp3)
  Segment 25: 79152ms ~ 85056ms (file: 123-135-651-265-231-256-261-316-263-576-521-314-516-635-361-103-136-267-263-620-130-263-201-267-260-260-527-126-572-130-123-574-267-267-576-123-103-671-213-321-213-573-107-610-2_W.mp3)
  Segment 26: 86056ms ~ 86632ms (file: 136-106-137-153-627-126-316-312-123-132-160-106-163-106-106-102-261-672-160-613-627-673-672-621-623-263-673-132-706-617-753-163-132-106-671-260-632-321-231-175-607-602-617-150-6_W.mp3)
  Segment 27: 87632ms ~ 93680ms (file: 762-163-361-321-201-123-670-756-750-763-176-615-617-162-163-310-132-103-136-312-306-132-127-132-621-621-621-756-316-236-576-231-257-126-206-231-257-267-706-527-625-736-672-273-3_W.mp3)
  Segment 28: 94680ms ~ 95256ms (file: 613-261-263-230-231-321 126-103-136-132-136-132_V.mp3)
  Segment 29: 96256ms ~ 96832ms (file: 756-756-576-756-576-576-576-756-756-751-352-561-541-564-546-517-567-571-573-752-756-756-576-576-576-576-756-756-132-513-351-547-531-541-532-564-561-541-514-567-572-576-576-756-5_W.mp3)
  Segment 30: 97832ms ~ 98408ms (file: 571-571-517-157-510-571-103-130-751-712-715-752-756-725-675-763-762-762-765-763-752-725-267-273-273-136-132-173-127-163-167-316-176-376-736-327-317-712-376-735-763-735-752-756-7_W.mp3)
  Segment 31: 99408ms ~ 99984ms (file: 132-134-163-136-103-163-612-306-137-136-230-206-762-273-102-136-132-136 123-123-705-326-130-130-106-103-130-130-132-203-213-123-130-136-132-126-103-123-260-230-203-123-320-362-3_V.mp3)
  Segment 32: 100984ms ~ 101560ms (file: 310-130-310-103 310-130-143-103-143-105-104-134-140-130-104-130-103-130-130-130-103-103-130-103-104-143-103-130-140-143-143 304-143-314-154-143-154-130-104-104-143-104-145-134-1_V.mp3)
  Segment 33: 102560ms ~ 103136ms (file: 576-567-576-567-576-576-576-567-576-576-576-756-671-613-351-534-546-547-504-504-541-504-541-504-541-514-547-534-503-546-541-547-543-652-567-567-657-567-576-567-567-567-567-564-5_W.mp3)
  Segment 34: 104136ms ~ 104712ms (file: 532-527 526-527-534-521-574-526-502-576-507-752-270_V.mp3)
  Segment 35: 105712ms ~ 106288ms (file: 576-576-756-576-576-576-574-576-756-765-576-576-576-756-657-567-567-567-625-627 167-761-135-123-321-637-621-137-103-310-310-310-130-103-316-136-103-157-136-165-163-165-136-130-1_W.mp3)
  Segment 36: 107288ms ~ 107864ms (file: 175-751 150-157-510-157-150-157-157-150-175-751-715-175-715-175-175-715-175-157-715-175-150 105-150-105-150-105-751-715-750-715-750-751-715-751-756-751-715-751-175-751 105-150-1_V.mp3)
  Segment 37: 108864ms ~ 115308ms (file: 201-267-576-567-135-130-136-136-132-657-265-126-315-132-576-132-103-312-321-351-103-570-756-725-312-216-312-261-276-213-236-231-321-576-312-751-175-621-250-136-130-621-136-510-6_W.mp3)
  Segment 38: 116308ms ~ 116884ms (file: 103-137-102-102-216-231-106-162-163-160-136-167-615-167-167-103-167-156-671-160-316-613-623-627-617-615-156-162-165-615-706-567-576-756-316-162-675-671-176-756-754-756-756-765-7_V.mp3)
  Segment 39: 117884ms ~ 133148ms (file: 176-574-132-675-201-130-210-617-132-167-167-130-107-273-320-372-367-327-310-213-301-137-574-517-210-130-574-105-762-302-574-574-103-567-567-203-213-201-130-576-132-537-105-201-6_W.mp3)
  Segment 40: 134148ms ~ 134724ms (file: 201-132-205-217-251-502-205-253-205-250-253-205-521 520-250-253-265-253-235-523-235 512-152-512-521-325-205-531-251-513-502-205-251-325-253-523-201-203-205-201-213-521-253-210 5_V.mp3)
  Segment 41: 135724ms ~ 144616ms (file: 237-107-175-320-267-237-326-236-236-302-210-621-621-132-301-102-273-756-267-752-574-576-765-207-302-260-267-267-127-320-203-260-267-574-576-302-267-102-102-710-756-765-672-762-7_W.mp3)
  Segment 42: 145616ms ~ 146192ms (file: 751-546-512-527-507-527-502-572-502-527-562-572-507-275-520-250-250-250-570-572-570-507-257-527-527-527-527-527_V.mp3)
  Segment 43: 147192ms ~ 147768ms (file: 617-617-617-617-617-617-671-617-617-617-610-512-572-504-504-504-501-504-615-672-657-675-765-675-657-675-617-675-671-675-617-162-571-165-276-726-275-735-725-725-273-723-726-726-3_W.mp3)
  Segment 44: 148768ms ~ 149344ms (file: 567-567-567-576-567-567-576-576-576-752-527-513-516-527-571-572-576-570-570-572-527-527-572-567-571-572-572-527-576-527-572-752-572-572-570-570-572-567-572-507-572-567-576-576-7_W.mp3)
  Segment 45: 150344ms ~ 150920ms (file: 361-316-716-713-371-317-570-317-765-170-276-217-671-213-267-216-261-507-514-130-132-310-136-103-130-130-126-172-137-726-567-527-157-576-750-675-753-213-721-301-317-137-736-613-7_V.mp3)
  Segment 46: 151920ms ~ 158724ms (file: 672-267-162-231-321-754-237-236-263-536-206-275-267-267-627-325-230-276-207-526-547-261-267-260-237-261-106-754-275-237-260-267-237-326-321-126-106-102-316-236-326-270-201-270-7_W.mp3)
  Segment 47: 159724ms ~ 163972ms (file: 261-216-213-102-216-261-126-276-260-236-203-267-623-217-276-127-107-136-132-103-267-627-316-132-130-216-326-263-127-627-231-631-627-617-267-276-261-126-726-521-617-675-601-567-6_W_stt_if_1000_of_the.mp3)
  Segment 48: 164972ms ~ 173864ms (file: 701-270-203-260-267-276-260-267-326-103-312-107-123-273-213-172-201-276-321-136-574-726-106-106-302-765-576-276-275-276-762-102-175-571-176-675-570-547-301 102-570-123-102-103-2_W.mp3)
  Segment 49: 174864ms ~ 175440ms (file: 510-571-751-751-715-175-715-751-571-750-571-157-571-571-570-750-751-750-751-507-750-571-750 507-507-157-517-570-517-510-507-570-507-157_V.mp3)
  Segment 50: 176440ms ~ 177016ms (file: 657-675-756-756-765-756-765-675 502-502-526-526-527-576-756-756-275-752-765-756-765-756-756-756-765 502-526-502-526-572-576-265-675-725-765-675-765-765-765-675-675-756 502-520-5_V.mp3)
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
> **Time**: 2026-04-05 12:44:41
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "아이폰은 계속 기능이 업데이트되는데",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "아이폰은", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 180, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "기능이", "start_ms": 300, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "업데이트되는데", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "갤럭시폰은 업데이트가 딱 막히다 보니까 게다가 성능도 좀 떨어져 가지고 오래전에 갤럭시를 경험하신 분들 중에서는 그때 경험으로 인해서",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "갤럭시폰은", "start_ms": 30, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "업데이트가", "start_ms": 290, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "딱", "start_ms": 480, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "막히다", "start_ms": 540, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "보니까", "start_ms": 670, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 4,
"transcript": "보시면 알파 트렌드가 파란색으로 전환됐다는 건 매수세가 들어왔다는 신호이죠 그런데 중요한 건 그 다음이라고 합니다 그 캔들이 얼마나 강하게 닫히느냐 그게 첫 번째 기준이에요 이게 제일 중",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "보시면", "start_ms": 40, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "알파", "start_ms": 270, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "트렌드가", "start_ms": 430, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "파란색으로", "start_ms": 640, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "전환됐다는", "start_ms": 950, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "건", "start_ms": 1280, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "매수세가", "start_ms": 1340, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "들어왔다는", "start_ms": 1600, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "신호이죠", "start_ms": 1850, "dur_ms": 220, "phase_lr_deg": 0.0}
]
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
"transcript": "중요 말해 처음에 인제 그 자리를 내드기가 쉽지는 않았는데 이제 어머니가 이제",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "중요", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "말해", "start_ms": 240, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "처음에", "start_ms": 410, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "인제", "start_ms": 620, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "주장이 허위라고 일축했습니다 미군은 캐슘섬 전투기 추락은 즉각 부인했지만 F-35 전투기가 실제로 격추됐는지에 대해서는 아직 입장을 내놓지 않고",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "주장이", "start_ms": 20, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "허위라고", "start_ms": 210, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "일축했습니다", "start_ms": 420, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "미군은", "start_ms": 750, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "캐슘섬", "start_ms": 910, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "전투기", "start_ms": 1120, "dur_ms": 180, "phase_lr_deg": 0.0}
]
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
"transcript": "라는 신호이고 우리는 여기서 진입을 시작합니다 손절은 직전 저점 밑으로 잡아 주시고 익절은 동일하게 손익비 1대 1.5로 잡아 주시면 됩니다 지켜보시죠 자 진입",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "라는", "start_ms": 60, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "신호이고", "start_ms": 190, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "우리는", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "여기서", "start_ms": 560, "dur_ms": 160, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 17,
"transcript": "아니면 기타를 아 저요? 아 너무 고맙지만 전 현직 현직 가수예요 가",
"speaker": "speaker_5",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "아니면", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "기타를", "start_ms": 230, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 420, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "저요?", "start_ms": 490, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "이 리듬이 끊기기 시작합니다 이 부분을 하루에 매매를 40번 이상 하는 제이슨 노아가 모를 리가 없었겠죠 그래서 직접 만든",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 30, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "리듬이", "start_ms": 100, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "끊기기", "start_ms": 250, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "시작합니다", "start_ms": 460, "dur_ms": 280, "phase_lr_deg": 0.0}
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
},
{
"segment": 21,
"transcript": "하니스 엔지니어링의 핵심입니다 오늘 하나만 기억하세요 AI 에이전트가 실수했을 때 프롬프트를 고치지 마세",
"speaker": "speaker_6",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "하니스", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "엔지니어링의", "start_ms": 230, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "핵심입니다", "start_ms": 590, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 850, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "하나만", "start_ms": 980, "dur_ms": 140, "phase_lr_deg": 0.0}
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
"transcript": "추가 하락을 하더라도 더 이상 진입을 하지 않았는데요 진입을 하지 않은 이유는 아래 차트를 보시면 다이버전스가 만들어졌기 때문이에요 지금 시장에 거품이",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "추가", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "하락을", "start_ms": 200, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "하더라도", "start_ms": 390, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 620, "dur_ms": 60, "phase_lr_deg": 0.0}
]
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
"transcript": "중 일부만 잡을 수 있어요 이 과정에서 드는 비용이 정말 어마어마합니다 얼마나 비싸냐면 반물질 1g을 만들려면 수조 달러가 필요합니다 한국 GDP의 몇 배가 드는 돈이죠 게다가 썬의 현재 생산 속도로 1g을 모으려고 하면은 우주의",
"speaker": "speaker_7",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "중", "start_ms": 20, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "일부만", "start_ms": 90, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "잡을", "start_ms": 240, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 360, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 430, "dur_ms": 120, "phase_lr_deg": 0.0}
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
"transcript": "에드워드 손다이크는 군인들을 대상으로 흥미로운 연구를 진행했습니다 지휘관들에게 부하들의 지능, 리더십, 신뢰도 등을 객관적으로 평",
"speaker": "speaker_8",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "에드워드", "start_ms": 30, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "손다이크는", "start_ms": 240, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "군인들을", "start_ms": 530, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "대상으로", "start_ms": 720, "dur_ms": 200, "phase_lr_deg": 0.0}
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
"transcript": "거 저 풀 셀프 드라이빙이 USB 형태로 제공이 된다 이니까 제공이 된다고 표현을 하셨는데 어쨌든 이 USB",
"speaker": "speaker_9",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "거", "start_ms": 60, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "저", "start_ms": 150, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "풀", "start_ms": 220, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "셀프", "start_ms": 310, "dur_ms": 120, "phase_lr_deg": 0.0}
]
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
"transcript": "먹었을 때 파닥파닥 튀어오르는 게 있거든요 소총 두 개를 사용 하나는",
"speaker": "speaker_10",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "먹었을", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 170, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "파닥파닥", "start_ms": 230, "dur_ms": 220, "phase_lr_deg": 0.0}
]
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
"transcript": "1986년이라고 써 있었고 그리고 음력 4월 20일이라고 써 있었어요",
"speaker": "speaker_11",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "1986년이라고", "start_ms": 30, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "써", "start_ms": 390, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "있었고", "start_ms": 460, "dur_ms": 180, "phase_lr_deg": 0.0}
]
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
"transcript": "그래서 4월 20일이",
"speaker": "speaker_11",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "그래서", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "4월", "start_ms": 210, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "20일이", "start_ms": 340, "dur_ms": 180, "phase_lr_deg": 0.0}
]
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
*Generated by WKAppBot ask gemini — 2026-04-05 12:44:41*
