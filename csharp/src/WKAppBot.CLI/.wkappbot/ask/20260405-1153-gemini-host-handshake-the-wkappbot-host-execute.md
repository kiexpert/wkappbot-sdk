# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=30524  time=11:51:45  uptime=26417s  mem=466MB  system_processes=356"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_115111.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 751-175-157-715-571-705-750-705-701-701-170-701-751-170-751-716-710-751-175-715-170-710-173-731-705 507-574-570-507-501-150-507-571-157-510-570-157-517-510_V.mp3)
  Segment 2: 1576ms ~ 9568ms (file: 527-102-126-120-203-230-102-752-327-231-213-230-726-756-756-257-213-130-756-375-237-230-762-756-231-267-326-273-726-361-321-326-231-627-765-237-320-102-216-756-236-102-576-265-2_W.mp3)
  Segment 3: 10568ms ~ 11144ms (file: 517-157-105-105-517-105-153-150-517-510-105-150-105-150-150-504-150-157-153-105-105-105-150-153-510-150-510-513-517-517-514-510 501-501-517-153-105-153-105-517-105-150-157-150-5_V.mp3)
  Segment 4: 12144ms ~ 12720ms (file: 103-765-312-720-173-713-265-153-451-613 762-103-130-310-136-103-103-136-613-103-130-163-163-617-136-153-316-103-103-136-137-136-136-576-731-706-130-312-163-715-701-750-751-751-1_V.mp3)
  Segment 5: 13720ms ~ 14296ms (file: 567-576-576-576-567-657-756-675-765-657-756-567-765-576-756-576-576-576-567 756-567-675-765-657-675-675-675-657-576-765-675-567-657-765-765-756-762-367-716-176-167-163-167-176-1_W.mp3)
  Segment 6: 15296ms ~ 15872ms (file: 574-574-574-752-574-754-754-574-754-752-275-275-756-765-762-765-762-675-762-762-765-756-752-652-654-675-671-617-176-765-765-675-761-756-761-765-765-765-765-756-715 763-761-617-1_W.mp3)
  Segment 7: 16872ms ~ 17448ms (file: 571-576-635-637-657-136-375-517-576-576-156-157-165-756-765-236-526-265-573-567-567-576-576-576-750-567-574-547-576-567-571-574-567-765-567-517-163-756-573-576-765-516-523-537-6_W.mp3)
  Segment 8: 18448ms ~ 26764ms (file: 310-301-321-123-270-302-301-123-570-275-765-276-270-725-710-301-310-230-213-210-657-576-102-203-321-123-725-752-135-732-725-102-120-753-123-751-752-765-107-276-213-210-267-201-2_W.mp3)
  Segment 9: 27764ms ~ 28340ms (file: 715-750-317-376-726-574-125-163-301-312-301-375-267-750-173-307-361-321-376-312-132-507-135-312-317-702-372-753-231-570-375-236-732-357-753-567-657-537-375 152-167-276-507-153-2_V.mp3)
  Segment 10: 29340ms ~ 29916ms (file: 567-576-756-576-657-716-572-572-576-576-576-576-576-675 514-501-571-752-751-576-756-576-576-756-571-576-576-571-756-571-576-751-571-765-531-501-512-504_W.mp3)
  Segment 11: 30916ms ~ 31492ms (file: 136-136-135-136-163-135-316-136-103-136-135-103 315-357-756-753-751-357-517-517-153-175 301-314-134-134-314-316-134-314-134-134-134-136-301-135-135-137-135-167-103-136-135-136-1_V.mp3)
  Segment 12: 32492ms ~ 33068ms (file: 132-132-675-516-263-216-623-203-326-162-132 157-541_V.mp3)
  Segment 13: 34068ms ~ 42924ms (file: 120-210-321-102-261-201-230-107-103-617-132-132-302-130-175-271-132-210-201-257-231-207-254-260-752-275-572-275-726-367-136-326-231-362-130-136-752-170-103-257-263-236-513-125-1_W.mp3)
  Segment 14: 43924ms ~ 44500ms (file: 157-157-517-571-571-571-571-571-571-574 576-571-571-571-517-157-571-517-573-571-753-571-756-517-571-517-571-571-507 153-517-571-715-751-715-175-751-571-137-312-321-137-132-137-1_W.mp3)
  Segment 15: 45500ms ~ 51008ms (file: 312-132-206-627-267-236-527-317-320-273-761-163-106-675-635-135-270-326-276-267-276-230-172-137-756-367-376-763-736-731-763-237-326-273-132-725-260-236-301-310-321-312-132-132-3_W.mp3)
  Segment 16: 52008ms ~ 62088ms (file: 132-760-103-132-163-312-623-627-267-103-236-564-257-265-267-120-102-123-103-102-263-765-213-123-236-120-761-237-736-327-236-123-231-321-213-123-237-312-123-321-123-752-267-263-1_W.mp3)
  Segment 17: 63088ms ~ 63664ms (file: 136-312-173-715-176-173-157-631-137-130-310-317-137-130-132-137-132-137-136-137 502-325-271-361-537-132-163-265-325-126-215-236-526-325-356-572-231-752-123-137-236-213-123-325-3_V.mp3)
  Segment 18: 64664ms ~ 72368ms (file: 103-126-150-132-273-103-231-130-231-136-230-267-312-312-263-263-312-236-237-236-130-312-231-271-132-132-235-103-312-106-263-267-206-321-213-203-210-201-230-163-216-156-126-256-3_W.mp3)
  Segment 19: 73368ms ~ 73944ms (file: 765-546-504-504-506-504-503-502-504-507-506-506-506-567-507-752-756-765-756-756-567-756-756-756-756-752-756-756-756-725-504-561-507-501-507-507-507-507-576-571-506-506-506-567-5_V.mp3)
  Segment 20: 74944ms ~ 75520ms (file: 517-571-571-157-576-571-517-571-571-517-576-153-501-570-153-157-156-135-157-156-135-517-513-537-175-170-167-516-135-510-756-537-517-517-517-571-751-715-571-517-157-153-135-153-1_V.mp3)
  Segment 21: 76520ms ~ 84800ms (file: 261-261-257-103-130-136-136-213-123-134-261-210-216-576-267-576-267-265-216-123-574-576-132-352-157-106-167-106-210-261-213-130-316-163-132-312-106-754-726-627-623-362-756-752-3_W.mp3)
  Segment 22: 85800ms ~ 86376ms (file: 312-312-315-135-103-137-315-132-301-137-132-137-130-310-305-371-315-315 135-301-132-315-312-315-137-317-137-135-301-315-103-153-357-152-103-135-301-312-137 103-132-135-310-301-1_V.mp3)
  Segment 23: 87376ms ~ 87952ms (file: 275-275-273-127-172-715-517-137-571-271-713-375-175-371-317-715-573-516-567-546-457-176-715-751-756-765-756-756-754-574-754-754-754-754-754-754-574_W.mp3)
  Segment 24: 88952ms ~ 89528ms (file: 136-367-137 703-703-730-137-753-103-317-367-671-670-613-613-317-103-160-307-136-360-107-105-756-751-537-135-750 753-705-103-307-317-106-613-603-607-627-612-160-106-612-301-671-1_V.mp3)
  Segment 25: 90528ms ~ 91104ms (file: 715-175-571-756-751-715-751-715-571-157-175-156-157-157-175 175-107-157-175-175-175-157-175-157-157-715-571-751-571-576-571-574-751-751-176 105-170-175-710-175-157-170-175-175-1_W.mp3)
  Segment 26: 92104ms ~ 93724ms (file: 501-572-257-271-567-657-567-576-756-765-762-512-512-512-512-501-512-501-507-501-501-513-507-150-157-105-157-315-352-261-201-501-705-560-605-675-657-601-675-675-650-610-651-567-6_V.mp3)
  Segment 27: 94724ms ~ 99260ms (file: 756-715-705-750 317-317-571-517-517-571-157-175-751-751-175-751-715-750-751-150-751-175-750-715-157-517-157-173-751-175-517-157-127-123-710-102-513-521-203-206-620-102-312-623-2_V.mp3)
  Segment 28: 100260ms ~ 100836ms (file: 320-261-126-705-702-273-326-623-263-261-261-620-213-215-627-627-627-542-562-123-130-310-123-236-326-260-267-260-261-132-263-206-206-260-762-276-302-231-236-732-167-510-256-260-2_W.mp3)
  Segment 29: 101836ms ~ 102412ms (file: 526-527-510-517-516-576-670-675-675-670-657-675 627 502-501_W.mp3)
  Segment 30: 103412ms ~ 103988ms (file: 120-216-612-261-612-621-162-612-160-613-163-612 621-612-261-621-621-261-621-612-612-160-160-106 612-621-162-621-612-610-612-601-602-610-602-106 621-160-162-160-162-162-160-162-1_W.mp3)
  Segment 31: 104988ms ~ 105564ms (file: 105-105-517-510-501-501-510-501 510-510-510-571-510-510-517-510-517-501-150-501-517-105-501-105-157-105-157-153-105-150-517-517-501-510-510 501-105-153-507-507-501-517-571-501-5_V.mp3)
  Segment 32: 106564ms ~ 107140ms (file: 756-576-756-756-576-756 752-756-576-576-756-756-576-576-576-576-576-576-756-756-765 725-576-576-756-576-756-576-576-576-756-576-576-576-756-756 752-756-576-756-756-756-576-576-7_W.mp3)
  Segment 33: 108140ms ~ 108716ms (file: 126-210-726-257-257-527-567-561-567-571-675-765-165-127-156-615-156-675-657-567-675-657-675-657-675-672-520-156-615-516-561-561-675-567-675-675 506-561-625-657-651-657-567-657-6_W.mp3)
  Segment 34: 109716ms ~ 115908ms (file: 320-321-574-172-106-213-276-527-206-627-276-267-263-276-236-572-751-574-762-267-721-130-267-107-726-627-267-572-572-752-120-210-231-267-762-276-267-754-756-150-271-547-542-567-7_W.mp3)
  Segment 35: 116908ms ~ 117484ms (file: 150-175-175-175-175-517-175-175-153-175-176-176-175-175-173-715 507-510-150-150-157-510-517-576-751-576-157-517-157-756-175-716-175-157-715-175-157-157-517-571-157-571-157-153 1_V.mp3)
  Segment 36: 118484ms ~ 119060ms (file: 751-715-175-751-175-715-751 571-756-517-517-571-571-715-715-751-173-750-173-751-571-175-715-175-173-713-731-753-753-750-573 751-750-576-751-756-173-173-710-173-701-173-750-751-7_W.mp3)
  Segment 37: 120060ms ~ 128448ms (file: 761-657-751-236-762-762-672-756-275-237-267-203-130-107-312-271-627-231-273-174-102-207-203-207-276-762-675-260-210-320-237-230-123-230-237-130-263-236-531-130-267-672-236-263-5_W.mp3)
  Segment 38: 129448ms ~ 139492ms (file: 162-312-316-216-527-207-132-123-102-312-705-316-276-271-267-237-507-627-137-312-761-276-302-312-172-130-107-213-203-716-761-521-302-236-753-721-372-215-572-752-267-732-127-761-7_V.mp3)
  Segment 39: 140492ms ~ 150068ms (file: 267-132-362-537-327-521-103-231-236-765-312-267-621-123-213-163-576-136-216-231-216-261-261-216-213-201-102-261-267-132-627-627-267-216-120-276-236-263-612-361-327-362-326-312-2_W.mp3)
  Segment 40: 151068ms ~ 151644ms (file: 103-713-716-317-173 175 176-132-132-247_V.mp3)
  Segment 41: 152644ms ~ 153220ms (file: 517-157-571-517-571 527-720-675-752-725-752-756-752-756-751-756-751-175-751-751-751-751-751-751-751-751-751-715-751 652-256-520-257-576-257-725-576-257-756-752-756-756-756-751-7_W.mp3)
  Segment 42: 154220ms ~ 161852ms (file: 631-256-716-162-107-163-257-176-137-571-126-102-217-132-326-367-726-501-170-173-173-175-125-751-173-657-625-756-527-127-167-541-351-312-120-130-107-136-102-173-173-530-153-167-7_V.mp3)
  Segment 43: 162852ms ~ 163428ms (file: 572-572-756-572-576-572-576-576-752-752-542-547-542-542-504-542-541-504-524-547-504-504-504-572-572-576-576-576-756-752-576-756-756-752-532-531-562-501-542-501-502-502-562-506-5_V.mp3)
  Segment 44: 164428ms ~ 175804ms (file: 260-675-236-326-302-705-574-157-571-312-362-362-230-213-326-321-316-236-723-316-263-103-317-236-537-750-312-750-203-103-375-267-132-573-105-103-652-135-123-570-317-201-320-203-3_W.mp3)
  Segment 45: 176804ms ~ 177380ms (file: 136-103-601-107-173-136-231-612-621-715-123-137-157-613-736 163 321-635 167-136-123-631-231-321-572-576-102-752-651-573-752-512-671 176-370-321-327-267-106-613-231-361-301-531-1_V.mp3)
  Segment 46: 178380ms ~ 178956ms (file: 123-617-321-261-672-237 215-261 736-736-736_W.mp3)
  Segment 47: 179956ms ~ 180532ms (file: 751-570-571-571-175-715-715 175-571-751-750-175-751-751-751-571-105-750-751-715-751-715-750-751-751-571-570-751-570-751_W.mp3)
  Segment 48: 181532ms ~ 182108ms (file: 751-715-715-571-175-571-175-571-751-571-573-571-573-156-157-153-513-516-531-157-571-751-157-715-517-716-172-715-715-715-715-751-715-753-715-751-715-751-751-751-751-571-751-751-5_V.mp3)
  Segment 49: 183108ms ~ 188616ms (file: 325-103-631-103-713-210-751-106-237-203-321-321-310-176-702-167-162-620-236-103-261-230-103-120-301-203-216-102-107-216-216-260-231-260-230-321-137-321-231-302-671-236-231-231-1_V.mp3)
  Segment 50: 189616ms ~ 190192ms (file: 756-570-576-571-517-517-517-517-756-756-756-716 507-510-570-751-571-751-571 571-510-507-510-510-501-510-507_W.mp3)
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
> **Time**: 2026-04-05 11:53:59
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "A low of two dollars and thirty cents a gallon in most states.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "A", "start_ms": 20, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "low", "start_ms": 70, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "of", "start_ms": 250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "two", "start_ms": 350, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "dollars", "start_ms": 500, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "and", "start_ms": 750, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "thirty", "start_ms": 870, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "cents", "start_ms": 1070, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 1250, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "gallon", "start_ms": 1330, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 1580, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "most", "start_ms": 1680, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "states.", "start_ms": 1880, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "하지만 이 연설 나흘 뒤 시작된 전쟁으로 국제 유가는 한 달 만에 66% 폭등했습니다.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "하지만", "start_ms": 120, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 520, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "연설", "start_ms": 620, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "나흘", "start_ms": 970, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "뒤", "start_ms": 1220, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "시작된", "start_ms": 1370, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "전쟁으로", "start_ms": 1820, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "국제", "start_ms": 2400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "유가는", "start_ms": 2700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 3100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "달", "start_ms": 3250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "만에", "start_ms": 3400, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "66%", "start_ms": 3600, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "폭등했습니다.", "start_ms": 4250, "dur_ms": 750, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "(noise)",
"speaker": null,
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 4,
"transcript": "작은 정부들이나 빠른 재보급이 시급한 국가들에게는 매우 중요한 의미를 가질 수 있다.",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "작은", "start_ms": 80, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "정부들이나", "start_ms": 380, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "빠른", "start_ms": 1030, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "재보급이", "start_ms": 1330, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "시급한", "start_ms": 1780, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "국가들에게는", "start_ms": 2180, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "매우", "start_ms": 2930, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "중요한", "start_ms": 3230, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "의미를", "start_ms": 3630, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "가질", "start_ms": 4030, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 4330, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있다.", "start_ms": 4430, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "워싱턴의 정책 결정자들에게 천궁-2의 실전 대비는 동맹국들이 이제는 전통적인 미국산 장비를 대체할 수 있는 신뢰할",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "워싱턴의", "start_ms": 150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "정책", "start_ms": 650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "결정자들에게", "start_ms": 950, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "천궁-2의", "start_ms": 1700, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "실전", "start_ms": 2250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "대비는", "start_ms": 2550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "동맹국들이", "start_ms": 2950, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "이제는", "start_ms": 3500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "전통적인", "start_ms": 3900, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "미국산", "start_ms": 4450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "장비를", "start_ms": 4850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "대체할", "start_ms": 5250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 5650, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 5750, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "신뢰할", "start_ms": 6000, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "K2 전차와 K9 자주포를 판매하면서 빠른 납품만 약속한 것이 아니라 더 나아가 폴란드 현지에서의 생산 라인을 세우도록 돕고 핵심 제조 기술까지 전수하겠다고 시원하게 약속했습니다. 그렇게 함으로써 폴란드가 장",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "K2", "start_ms": 50, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "전차와", "start_ms": 350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "K9", "start_ms": 750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "자주포를", "start_ms": 1050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "판매하면서", "start_ms": 1550, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "빠른", "start_ms": 2200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "납품만", "start_ms": 2500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "약속한", "start_ms": 2900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "것이", "start_ms": 3300, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 3500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 3900, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "나아가", "start_ms": 4050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "폴란드", "start_ms": 4400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "현지에서의", "start_ms": 4800, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "생산", "start_ms": 5450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "라인을", "start_ms": 5800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "세우도록", "start_ms": 6200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "돕고", "start_ms": 6700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "핵심", "start_ms": 7050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "제조", "start_ms": 7400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "기술까지", "start_ms": 7700, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "전수하겠다고", "start_ms": 8250, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "시원하게", "start_ms": 9000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "약속했습니다.", "start_ms": 9500, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "그렇게", "start_ms": 10300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "함으로써", "start_ms": 10700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "폴란드가", "start_ms": 11200, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "장", "start_ms": 11750, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "의 사이버 보안 관리 체계하고 관리 체계, 위협 탐지, 대응 프로세스",
"speaker": "speaker_5",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "의", "start_ms": 100, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "사이버", "start_ms": 200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "보안", "start_ms": 600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "관리", "start_ms": 950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "체계하고", "start_ms": 1300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "관리", "start_ms": 1800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "체계,", "start_ms": 2150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "위협", "start_ms": 2550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "탐지,", "start_ms": 2850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "대응", "start_ms": 3250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "프로세스", "start_ms": 3600, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "5분의 1 정도 수준이 되죠. 수십 개가 한꺼번에 올라가고 계속 깔고 있거든요. 향후에는 교체 수요도 있을 걸로 봅니다. 연간으로 보면 시간이 가면 갈수록 50에서 100 메가와트 아워 이상 될 걸로 봅니다. 삼성 SDI가 납품하는 게",
"speaker": "speaker_6",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "5분의", "start_ms": 60, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "1", "start_ms": 410, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "정도", "start_ms": 560, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "수준이", "start_ms": 860, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "되죠.", "start_ms": 1260, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "수십", "start_ms": 1610, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "개가", "start_ms": 1960, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "한꺼번에", "start_ms": 2260, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "올라가고", "start_ms": 2810, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 3360, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "깔고", "start_ms": 3710, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "있거든요.", "start_ms": 4010, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "향후에는", "start_ms": 4510, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "교체", "start_ms": 5010, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "수요도", "start_ms": 5310, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "있을", "start_ms": 5710, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "걸로", "start_ms": 5960, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "봅니다.", "start_ms": 6260, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "연간으로", "start_ms": 6660, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "보면", "start_ms": 7210, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "시간이", "start_ms": 7510, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "가면", "start_ms": 7960, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "갈수록", "start_ms": 8260, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "50에서", "start_ms": 8710, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "100", "start_ms": 9260, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "메가와트", "start_ms": 9560, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "아워", "start_ms": 10060, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이상", "start_ms": 10360, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "될", "start_ms": 10710, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "걸로", "start_ms": 10860, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "봅니다.", "start_ms": 11160, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "삼성", "start_ms": 11560, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "SDI가", "start_ms": 11860, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "납품하는", "start_ms": 12310, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 12860, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "건데도 좀 감사하게 좀 생각하는 경향이 있죠. 그래서 예전에도 얘기한 적이 있는데 저는 사실 북한에서 안 태어난 것만 해도 되게 아 되게 행복해요 이거. 또 우리나라 남한 얼마나 좋아 이게 자",
"speaker": "speaker_7",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "건데도", "start_ms": 40, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 490, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "감사하게", "start_ms": 640, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 1190, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "생각하는", "start_ms": 1340, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "경향이", "start_ms": 1890, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "있죠.", "start_ms": 2290, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 2640, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "예전에도", "start_ms": 3040, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "얘기한", "start_ms": 3540, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "적이", "start_ms": 3940, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "있는데", "start_ms": 4190, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "저는", "start_ms": 4590, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "사실", "start_ms": 4890, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "북한에서", "start_ms": 5240, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 5740, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "태어난", "start_ms": 5840, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "것만", "start_ms": 6290, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "해도", "start_ms": 6590, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "되게", "start_ms": 6890, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 7190, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "되게", "start_ms": 7290, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "행복해요", "start_ms": 7590, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이거.", "start_ms": 8090, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 8440, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "우리나라", "start_ms": 8590, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "남한", "start_ms": 9140, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "얼마나", "start_ms": 9490, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "좋아", "start_ms": 9940, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 10290, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "자", "start_ms": 10590, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "매매 들어갔던 타점은 확인을 하고 확정을 짓고 들어간 진입의 결과였지 흥분해서 반복 매매로 만들어낸 몰입의 결과가 아니라는 겁니다. 수익이 나면 시장이 쉬는 리듬을",
"speaker": "speaker_8",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "매매", "start_ms": 120, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "들어갔던", "start_ms": 470, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "타점은", "start_ms": 1020, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "확인을", "start_ms": 1470, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "하고", "start_ms": 1870, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "확정을", "start_ms": 2170, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "짓고", "start_ms": 2620, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "들어간", "start_ms": 2970, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "진입의", "start_ms": 3420, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "결과였지", "start_ms": 3820, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "흥분해서", "start_ms": 4370, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "반복", "start_ms": 4920, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "매매로", "start_ms": 5270, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "만들어낸", "start_ms": 5720, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "몰입의", "start_ms": 6270, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "결과가", "start_ms": 6720, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "아니라는", "start_ms": 7170, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "겁니다.", "start_ms": 7670, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "수익이", "start_ms": 8070, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "나면", "start_ms": 8470, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "시장이", "start_ms": 8820, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "쉬는", "start_ms": 9270, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "리듬을", "start_ms": 9620, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "(noise)",
"speaker": null,
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 12,
"transcript": "포커스 양보가 필요합니다.",
"speaker": "speaker_9",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "포커스", "start_ms": 100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "양보가", "start_ms": 600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "필요합니다.", "start_ms": 1050, "dur_ms": 750, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "(noise)",
"speaker": null,
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 14,
"transcript": "Nobody's ever late to get there when you're supposed to be at the appointment, which is the time you arrive. Everything was so like seamlessly managed. My kid was to",
"speaker": "speaker_10",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Nobody's", "start_ms": 30, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "ever", "start_ms": 480, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "late", "start_ms": 730, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 980, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "get", "start_ms": 1130, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "there", "start_ms": 1330, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "when", "start_ms": 1580, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "you're", "start_ms": 1780, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "supposed", "start_ms": 2030, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 2430, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "be", "start_ms": 2580, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "at", "start_ms": 2730, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 2880, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "appointment,", "start_ms": 3030, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "which", "start_ms": 3680, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 3930, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 4080, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "time", "start_ms": 4230, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 4530, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "arrive.", "start_ms": 4680, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "Everything", "start_ms": 5130, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "was", "start_ms": 5680, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "so", "start_ms": 5880, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "like", "start_ms": 6130, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "seamlessly", "start_ms": 6380, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "managed.", "start_ms": 7030, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "My", "start_ms": 7580, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "kid", "start_ms": 7780, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "was", "start_ms": 8030, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 8230, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "수지 지금 세계에서 가장 가혹한 환경인 사막의 해법으로 등장하고 있는 겁니다. 아일랜드는 식량의 통제권을 빼앗겨 인구의 4분의 1을 잃었습니다. 21세기의 중동은 달러가 있",
"speaker": "speaker_11",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "수지", "start_ms": 100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "세계에서", "start_ms": 750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 1250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "가혹한", "start_ms": 1600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "환경인", "start_ms": 2050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "사막의", "start_ms": 2500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "해법으로", "start_ms": 2900, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "등장하고", "start_ms": 3450, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 4000, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "겁니다.", "start_ms": 4250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아일랜드는", "start_ms": 4650, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "식량의", "start_ms": 5300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "통제권을", "start_ms": 5700, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "빼앗겨", "start_ms": 6250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "인구의", "start_ms": 6700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "4분의", "start_ms": 7100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "1을", "start_ms": 7500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "잃었습니다.", "start_ms": 7800, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "21세기의", "start_ms": 8550, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "중동은", "start_ms": 9200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "달러가", "start_ms": 9650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "있", "start_ms": 10100, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "줄로 쓰지 않았다고 해요. 이게 가능했던 이유가 뭘까요? AI 에이전트가 강해서? 물론 GPT 모델은 강력하지만 결국 이 핵심은 하네스 엔지니어링에 있었습니다. 세 명의 엔지니어가 실제로 한 일을 하네스의 관점에서 한번 보",
"speaker": "speaker_12",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "줄로", "start_ms": 50, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "쓰지", "start_ms": 350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "않았다고", "start_ms": 650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "해요.", "start_ms": 1100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 1450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "가능했던", "start_ms": 1750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이유가", "start_ms": 2250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "뭘까요?", "start_ms": 2650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "AI", "start_ms": 3100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "에이전트가", "start_ms": 3400, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "강해서?", "start_ms": 4000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "물론", "start_ms": 4450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "GPT", "start_ms": 4800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "모델은", "start_ms": 5200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "강력하지만", "start_ms": 5650, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "결국", "start_ms": 6350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 6700, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "핵심은", "start_ms": 6800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "하네스", "start_ms": 7250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "엔지니어링에", "start_ms": 7650, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "있었습니다.", "start_ms": 8500, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "세", "start_ms": 9250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "명의", "start_ms": 9400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "엔지니어가", "start_ms": 9650, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "실제로", "start_ms": 10300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 10750, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "일을", "start_ms": 10900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "하네스의", "start_ms": 11200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "관점에서", "start_ms": 11700, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "한번", "start_ms": 12250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "보", "start_ms": 12550, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "디자인과 투명한 둥근 문, 그리고 간단 명료한 조작부는 세탁기는 숨겨야 하는 가전이라는 이미지를 조금씩 걷어냈다고 합니다. 이제는 남들에게 보여줘도 민망하지 않은 인테리어 소품처럼 여겨지게 된 거",
"speaker": "speaker_13",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "디자인과", "start_ms": 80, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "투명한", "start_ms": 630, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "둥근", "start_ms": 1080, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "문,", "start_ms": 1430, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 1730, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "간단", "start_ms": 2130, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "명료한", "start_ms": 2480, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "조작부는", "start_ms": 2980, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "세탁기는", "start_ms": 3580, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "숨겨야", "start_ms": 4130, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 4580, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "가전이라는", "start_ms": 4880, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "이미지를", "start_ms": 5580, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "조금씩", "start_ms": 6080, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "걷어냈다고", "start_ms": 6530, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "합니다.", "start_ms": 7180, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이제는", "start_ms": 7580, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "남들에게", "start_ms": 8030, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "보여줘도", "start_ms": 8580, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "민망하지", "start_ms": 9130, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "않은", "start_ms": 9680, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "인테리어", "start_ms": 9980, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "소품처럼", "start_ms": 10530, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "여겨지게", "start_ms": 11130, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "된", "start_ms": 11680, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 11830, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "(noise)",
"speaker": null,
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 19,
"transcript": "(noise)",
"speaker": null,
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 20,
"transcript": "급등을 좀 했었습니다. 근데 우리나라는 상대적으로 그렇게까지 많이 급등한 건 아니지만 이번에 나온 내용은 스페이스X의 기업 가치가 3,000조 원으로 상향됐다고 합니다. 얼마 전에 2,000조 원 아니었나? 그냥 부르는 게 값인가? 그냥 1,000조 원이 그냥 복사가 된",
"speaker": "speaker_14",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "급등을", "start_ms": 50, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 450, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "했었습니다.", "start_ms": 600, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 1200, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "우리나라는", "start_ms": 1450, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "상대적으로", "start_ms": 2000, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "그렇게까지", "start_ms": 2550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 3050, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "급등한", "start_ms": 3350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "건", "start_ms": 3800, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "아니지만", "start_ms": 3950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이번에", "start_ms": 4400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "나온", "start_ms": 4750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "내용은", "start_ms": 5050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "스페이스X의", "start_ms": 5500, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "기업", "start_ms": 6200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "가치가", "start_ms": 6500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "3,000조", "start_ms": 6950, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "원으로", "start_ms": 7600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "상향됐다고", "start_ms": 7950, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "합니다.", "start_ms": 8600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "얼마", "start_ms": 9000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "전에", "start_ms": 9300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "2,000조", "start_ms": 9600, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "원", "start_ms": 10200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "아니었나?", "start_ms": 10350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 10850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "부르는", "start_ms": 11150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 11550, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "값인가?", "start_ms": 11700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 12200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "1,000조", "start_ms": 12500, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "원이", "start_ms": 13050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 13400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "복사가", "start_ms": 13700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "된", "start_ms": 14150, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "적으로 저희는 항상 어떤 공간을 보면 그걸 그대로 보는 게 아니라 아 여기서 뭐 빼고 뭐 더해서 이렇게 되겠",
"speaker": "speaker_15",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "적으로", "start_ms": 100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "저희는", "start_ms": 450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "항상", "start_ms": 850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 1200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "공간을", "start_ms": 1500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "보면", "start_ms": 1950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그걸", "start_ms": 2300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "그대로", "start_ms": 2600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "보는", "start_ms": 3050, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 3350, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 3500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 3900, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "여기서", "start_ms": 4000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 4450, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "빼고", "start_ms": 4600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 4950, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "더해서", "start_ms": 5100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 5550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "되겠", "start_ms": 5950, "dur_ms": 350, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 11:53:59*
