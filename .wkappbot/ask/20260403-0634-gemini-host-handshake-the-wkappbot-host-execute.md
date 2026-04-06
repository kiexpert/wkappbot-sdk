# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=06:32:55  uptime=18820s  mem=1618MB  system_processes=445"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_063121.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 106-167-106-167-261 126-130-123-516-576-751-163-167-617-167-675_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 132-103 316-136-136-103-162-136-361-617-675-756-765-751-167-657-156-123-130-132-632-326-123-321-253-265-625-675-627-321-231-312-216-267-627-132-321-132-230-631-360-123-102-102-1_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 276-261-263-352-532-563-103-132-612-160-316-513-175-102-564-261-135-231-132-103-130-103-132-136-163-163-106-163-657-512-132-167-130-163-156-106-136-103-156-126-761-163-675-162-6_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 106-136-163-123-260-260-263-617-106-132-123-320-236-370-756-732-620-260-276-326-103-130-310-136-612-106-612-210-610-261-675-627-765-671-617-761-761-672-263-716-612-216-236-137-1_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 312-136-321-103-576-510-106-137-103-731 670-762-620-206-260-260-267-256-572-251-310-302-372-107-130-623-625-632-362-326-326-703-705-705-327-231-321-136_W.mp3)
  Segment 6: 7880ms ~ 18140ms (file: 413-760-670-152-321-103-160-103-371-103-130-163-107-675-261-601-163-106-613-136-132-130-136-756-165-236-176-167-217-103-132-103-134-160-612-127-671-106-163-165-103-165-163-603-1_V.mp3)
  Segment 7: 19140ms ~ 19716ms (file: 756-705-163-103-756-613-301-130-130-157-125-316-673-602-726-705-123-132-631-102-231-162-130-103-306-231-132-301-312-320-302-312-321-302-132-302-236-263-216-231-213-302-320-302-3_V.mp3)
  Segment 8: 20716ms ~ 21292ms (file: 203-320-302-320-630-326-263-203-230-207-103-301 504-103-103-513-321-203-321-310-103-130-310-321-213-312-326-760-706 310-312-602-675-360-301-312-263-316-375-657-675-326-310-302-2_V.mp3)
  Segment 9: 22292ms ~ 22868ms (file: 671-136-316-613-132-672-607-675-675-607-361-631-136-765-617-106-670-623-360-360-326-613-163-613-617-675-167-106-316-367-310-136-103-631-312-361-326-756-763-762-132-165-167-173-1_V.mp3)
  Segment 10: 23868ms ~ 24444ms (file: 312-137-706-765-753-756-276-263-172-627-726-257-765-376-735-763-706-261 731-175-176-176-132-271-725-713-372-137-132-321-307-173-173-132-137-137-163-132-762-267-675-631-316-163-6_V.mp3)
  Segment 11: 25444ms ~ 26020ms (file: 623-162-613-362-261-621-216-132-651-263-326-316-162-612-326-672-613-163-621-612-167-567-576-672-106-206-263-267-652-623-321-316-136-163-107-157-315-216-231-671-763-130-231-136-6_W.mp3)
  Segment 12: 27020ms ~ 27596ms (file: 153-316-160-163-106-132-312 607-670-615-162-163-216-615-561-162-132-157-162 160-671-106-673-103-107-173-106-106-167-160-160-163-136-367-163-160-675-231-612-601-106-162-312-162-7_V.mp3)
  Segment 13: 28596ms ~ 29172ms (file: 627-752-372-527-715-657-576-572-752-756-765-735-725-751-275-637-621-263-162-362-261-216-136-130-362-672-103-137-136-103-132-153-106-136-316-136-136-103-612-725-726-276-216-132-2_W.mp3)
  Segment 14: 30172ms ~ 30748ms (file: 316-136-176-172-132-137-231-237-207-275-205-756-312-132-540-127-321-135-521-152-561-517-157-315-315-135-310-317-723-513-517-547-546-516-123-137-125-120-321-123-213-132-231-213-3_W.mp3)
  Segment 15: 31748ms ~ 32324ms (file: 312-632-621-261-621-261-261-261-267-267-621-623-621-627-627-267-627-627-672-267-267-263-672-673-137-136-103-756-756-765-763-103-106-103-130-160-567-756-765-375-106-160-160-136-1_W.mp3)
  Segment 16: 33324ms ~ 33900ms (file: 167-106-312-613-163-617-167-176-762-132-136-132-312-763-106-103 162-316-301-302-613-163-162-162-106-106-567-567-506-615-167-162-632-263-267-260-260-230-320-162-206-230-102-612-6_V.mp3)
  Segment 17: 34900ms ~ 35476ms (file: 261-263-236-621-657-657-657-132-312-126-312-326-312-726-265-261-156-765-627-672-672-572-562-625-572-357-537-357-576-753-316-135-315-567-657-627-265-265-265-236-326-236-216-126-3_W.mp3)
  Segment 18: 36476ms ~ 37052ms (file: 761-765-756-756-765-761-756-576-756-756-756-761-675-765-762-276-723-312-327-267-276-207-207-273-210-201_W.mp3)
  Segment 19: 38052ms ~ 38628ms (file: 675-627-167-562-527-157-257-251-561-537-352-537-526-567-613-673-263-361-563-521-375-351-517-517-574-537-357-572-625-615-615-627-651-256-567-352-325-325-623-632-562-567-157-627-6_W.mp3)
  Segment 20: 39628ms ~ 40204ms (file: 103-130-103-312-312-267-567-567-237-273-217-231-671-301-137-137-106_V.mp3)
  Segment 21: 41204ms ~ 41780ms (file: 534-534-327-375-325-376-372-375-376-375-165-675-637-316-567-367-371-371-317-371-317-367-136-376-317-316-367-367-357-573-546-547-362-361-137-176-176-176-167-176-173-172-176-176-1_W.mp3)
  Segment 22: 42780ms ~ 43356ms (file: 673-657-623-310-213-321-326-621-263-206-526-625-621-625-623-276-675-567-652-625-657-236 261-261-326-623-321-312-321-312-132-352-201-210-132-623-362-163-312-231-302-132-326-326-6_W.mp3)
  Segment 23: 44356ms ~ 44932ms (file: 136-160-103-130-176-130-163-715-176-561-176-126-621-632-160-106-165-167-567-136-137-103-136-356-175-103-163-132-103-103-571-175-136-715-106-165-157-613-615-216-163-103-167-176-1_V.mp3)
  Segment 24: 45932ms ~ 46508ms (file: 627-217-273-273-267-276-627-267-765-576-615-576-756-567-567-756-756-672-627-637-673-632-267-621-162-574-562-627-621-162-216-260-260-173-751-675-716-371-754-672-726-576-567-576-5_W.mp3)
  Segment 25: 47508ms ~ 48084ms (file: 762-706-627-765-716-627-206-260-206-321-310-756-756-726 765-173-675-675-576-517-513-715 765-765-765 361-162-136-231-132-210-201-130-126 756-756-765-765 146-153-361-132-261-613-2_W.mp3)
  Segment 26: 49084ms ~ 49660ms (file: 132-574-657-613-103-160-236-536-621-613-367-167-167-136-136-153-176-321-625-623-136-103-130-276-652-675-725-756-765-672-167-176-673-613-652-567-576-576-576-567-157-165-750-710-7_W.mp3)
  Segment 27: 50660ms ~ 51236ms (file: 251-532-571-561-516-526-203-263-203-203-206-206-265-206-206-260-263-206-206-725-206-203-263-205-126-203-231-102-213-201-623-625-576-567-257-265-203-236-302-127-163-102-167-106-1_W.mp3)
  Segment 28: 52236ms ~ 52812ms (file: 765-675-672-261-672-672-671-625-617-316-716-657-617-271-765-321-672-673-632-617-612-675-123-620-627-631-576-176-675-673-637-673-612-631-715-752-763-156-657-576-652-621-361-261-5_W.mp3)
  Segment 29: 53812ms ~ 54388ms (file: 312-637-136-132-302-320-236-320-321-316-312-320-301-302-321-312-137-103-107-167-137-137-132-231-203-230-320-302 607-672-627-627 147-627-267-164-132-103-175-230-637-756-576-316-3_V.mp3)
  Segment 30: 55388ms ~ 55964ms (file: 736-671-126-172-617-167-167-163-761-167-136-601-621-621-321-362-631-310-312-132-537-756-725-672-617-672-167-126-162-612-132-375-307-302-327-504-531-316-673-671-361-216-163-307-7_V.mp3)
  Segment 31: 56964ms ~ 57540ms (file: 152 756-756 175-321-123-376-352-365-320-213-127-320-321-312-326-312-213-317-327-317-312-312-301-301-307-371-372-703-207-273-217-271-123-251-235-253-257-257-253-257-213-123-132-1_W.mp3)
  Segment 32: 58540ms ~ 59116ms (file: 103-132-132-132-156-153-321-263-123-326-132-312-106-173-132-317-137-175-107-312-312-136-162-312-213-576-507-576-571-175-103-215-170-163-136-132-103-103-312-165-517-213-203-312-1_W.mp3)
  Segment 33: 60116ms ~ 60692ms (file: 527-567-567-657-325-526-267-236-326-236-261-326-351-352-367-534-537-735-624-547-576-567-567-564-536-563-326-631-632-620-236-276-275-326-327-352-357-357-357-574-357-573-675-765-6_W.mp3)
  Segment 34: 61692ms ~ 62268ms (file: 651-106-673-613-106-301-167-175-325-167-612-167-623-672 127_V.mp3)
  Segment 35: 63268ms ~ 63844ms (file: 621-632-621-627-236-617-632-621-517-765-106-136-671-671-675-167-672-675-621-263-621-265-265-263-236-231-326-267-236-623-621_W.mp3)
  Segment 36: 64844ms ~ 65420ms (file: 236-265-265-213-625-265-657-607-263-236-261-263-162-106-325-320-236-320-263-620-620-263-102-106-602-620-256-567-613-302-261-216-123-126-136-123-103-106-103-126-103-125-162-132-1_V.mp3)
  Segment 37: 66420ms ~ 66996ms (file: 310-103-571-312-310-310-132-130-316-526-531-572-735-103-613-130-316-136-130-103-132-136-103-136-106-126-132-136-162-701-172-576-135-103-136-103-301-103-301-302-302-310-123-735-2_V.mp3)
  Segment 38: 67996ms ~ 68572ms (file: 316-362-321-362-321-261-567-627-657-623-672-623-261-572-562-527-265-627-562-526-652-617-312-236-362-623-627-261-261-267-657-237-261-632-265-632-632-657-631-236-263-367-372-231-3_W.mp3)
  Segment 39: 69572ms ~ 70148ms (file: 167-135-162-160-613-263-621-765-675 716-675-761-675-675-716-132-312-136-612-567-657-316-637-671-627-761-673-260-160-671-621-621-610-206-126-160-261-120-627-617-627-612-126-236-1_W.mp3)
  Segment 40: 71148ms ~ 71724ms (file: 274-312-326-362-362-265-231-326-206-201-203-106-103-106-123-102-602 263-362-213-263-236-321-310-301-136-130-130-310-310-130-103-106-321-326-106-137-231-201-130-103-216-123-130 2_V.mp3)
  Segment 41: 72724ms ~ 73300ms (file: 206-126-763-615-163-617-673-627-612-267-362-612-621-612-163 316-316-136-126-103-716-170-627-276-623-136-367-136-103-106-176-617-163-103-160-136-612-162-103-130-507-103-103-163-3_V.mp3)
  Segment 42: 74300ms ~ 74876ms (file: 103-103-621-632-103-132-106-136-132-623-613-617-213-102 325-326-120-123 675-627-216-263-267-261 756-567-612-672-765-762-762-762-765-765-765-675-765-756-765-765-765-167-163-672-6_W.mp3)
  Segment 43: 75876ms ~ 76452ms (file: 165-103-163-310-136-103-162-106-103-160-612-160-312-106-152-136-612-105-132-163-162-103-610-163-765-126-103-263-126-103-627-621-162-521-362-567-316-160-103-106-160-106-157-106-1_V.mp3)
  Segment 44: 77452ms ~ 78028ms (file: 310-132-137-136-265-165-134-103-312-261-136-103-163-162-167-162-132-261-761-126-103-136-160-176-102-152-576-615-167-167-167-126-162-103-730-721-102-120-216-156-123-675-612-163-7_V.mp3)
  Segment 45: 79028ms ~ 79604ms (file: 206-260-206-321-316-316-162-316-231-620-620-672-765-765-765-206-260-602-627-620-260-260-260-213-326-213-216-310-163-132-312-106-160-213-576-574-216-265-213_W.mp3)
  Segment 46: 80604ms ~ 81180ms (file: 627-276-672-627-675-627-267-672-675-672-627-621-627-675-672-176-672-627-672-657-567-632-672-672-675-627-672-267-652-672-627-716-672-675-653-675-671-675-675-675-675-621-627-627-6_W.mp3)
  Segment 47: 82180ms ~ 82756ms (file: 302-705-705-736-273-372-136-176-175-312-132-157-756-756-310-103-517-150-175-761-716-765-765-765-765-716-130-176-301-103-103-132-367-310-160-612-317-163-176-621-601-310-132-361-3_W.mp3)
  Segment 48: 83756ms ~ 84332ms (file: 236 610-615 627-627-672 621-126-627-176-317_W.mp3)
  Segment 49: 85332ms ~ 85908ms (file: 705-756-675-765-637-675-756-657-675-675-765-756-756-570-534-357-751-613-362-327-236-124-132-567-657-657-567-657-675-756-123-132-651-765-170-270-170-321-132-137-627-627-123-631-6_W.mp3)
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
> **Time**: 2026-04-03 06:34:02
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "안녕하세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "안녕", "start_ms": 40, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "하세요", "start_ms": 260, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "만나서 반갑습니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "만나서", "start_ms": 50, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "반갑", "start_ms": 260, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "습니다", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "오늘 날씨가 어때요?",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 180, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "어때요?", "start_ms": 380, "dur_ms": 170, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "비가 올 것 같아요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "비가", "start_ms": 60, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "올", "start_ms": 200, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 280, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 380, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "우산을 챙기세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "우산을", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "챙기세요", "start_ms": 240, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "한국어 공부는 재미있지만 가끔은 표현이 너무 복잡해서 이해하기 어려울 때가 많아요. 그래도 포기하지 않고 열심히 하려고 노력 중입니다.",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 110, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "공부는", "start_ms": 460, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "재미있지만", "start_ms": 760, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "가끔은", "start_ms": 1260, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "표현이", "start_ms": 1660, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 2010, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "복잡해서", "start_ms": 2260, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이해하기", "start_ms": 2760, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "어려울", "start_ms": 3210, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "때가", "start_ms": 3560, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "많아요.", "start_ms": 3810, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "그래도", "start_ms": 4500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "포기하지", "start_ms": 4850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "않고", "start_ms": 5350, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "열심히", "start_ms": 5600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "하려고", "start_ms": 6000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "노력", "start_ms": 6350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "중입니다.", "start_ms": 6650, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "점심 메뉴 골랐어요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "점심", "start_ms": 45, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "메뉴", "start_ms": 195, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "골랐어요?", "start_ms": 335, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "비빔밥 어때요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "비빔밥", "start_ms": 35, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "어때요?", "start_ms": 275, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "건강에 좋을 것 같아요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "건강에", "start_ms": 40, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "좋을", "start_ms": 250, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 370, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 450, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "그럼 그걸로 해요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "그걸로", "start_ms": 170, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "해요", "start_ms": 350, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "지하철역이 어디예요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "지하철역이", "start_ms": 40, "dur_ms": 260, "phase_lr_deg": 0.0},
{"word": "어디예요?", "start_ms": 300, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "오른쪽으로 가세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "오른쪽으로", "start_ms": 30, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 310, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "길을 잃어버렸어요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "길을", "start_ms": 60, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "잃어버", "start_ms": 200, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "렸어요", "start_ms": 380, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "도와드릴까요?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "도와", "start_ms": 45, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "드릴까요?", "start_ms": 205, "dur_ms": 320, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "감사합니다",
"speaker": "speaker_2",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "감사", "start_ms": 30, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "합니다", "start_ms": 230, "dur_ms": 310, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "천만에요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "천만", "start_ms": 40, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "에요", "start_ms": 250, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "이거 얼마예요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "이거", "start_ms": 35, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "얼마예요?", "start_ms": 175, "dur_ms": 360, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "만 원입니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "만", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "원", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "입니다", "start_ms": 270, "dur_ms": 260, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "조금 비싼 것 같아요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "조금", "start_ms": 50, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "비싼", "start_ms": 180, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 340, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 430, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "깎아 드릴게요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "깎아", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "드릴게요", "start_ms": 240, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "화장실이 어디 있죠?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "화장실이", "start_ms": 30, "dur_ms": 260, "phase_lr_deg": 0.0},
{"word": "어디", "start_ms": 290, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "있죠?", "start_ms": 420, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "계단 옆에 있어요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "계단", "start_ms": 45, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "옆에", "start_ms": 205, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 355, "dur_ms": 170, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "몇 시에 문 닫아요?",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "몇", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "시에", "start_ms": 130, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "문", "start_ms": 230, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "닫아요?", "start_ms": 300, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "열 시에 닫습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "열", "start_ms": 35, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "시에", "start_ms": 135, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "닫습니다", "start_ms": 245, "dur_ms": 290, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "지금 배가 너무 고파요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "지금", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "배가", "start_ms": 180, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 310, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "고파요", "start_ms": 420, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "뭐 좀 먹을까요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "뭐", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 150, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "먹을까요?", "start_ms": 240, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "치킨 시켜 먹어요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "치킨", "start_ms": 30, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "시켜", "start_ms": 230, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "먹어요", "start_ms": 370, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "좋은 생각이에요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "생각", "start_ms": 190, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "이에요", "start_ms": 350, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "영화 보러 갈래요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "영화", "start_ms": 45, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "보러", "start_ms": 225, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "갈래요?", "start_ms": 345, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "무슨 영화 볼까요?",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "무슨", "start_ms": 50, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "영화", "start_ms": 180, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "볼까요?", "start_ms": 340, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "코미디 영화가 좋아요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "코미디", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "영화가", "start_ms": 240, "dur_ms": 170, "phase_lr_deg": 0.0},
{"word": "좋아요", "start_ms": 410, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "저도 코미디 좋아해요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "저도", "start_ms": 45, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "코미디", "start_ms": 175, "dur_ms": 190, "phase_lr_deg": 0.0},
{"word": "좋아해요", "start_ms": 365, "dur_ms": 180, "phase_lr_deg": 0.0}
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
"transcript": "내일 봐요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "내일", "start_ms": 30, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "봐요", "start_ms": 210, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "안녕히 가세요",
"speaker": "speaker_1",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 40, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 260, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "잘 자요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "잘", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "자요", "start_ms": 190, "dur_ms": 320, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "좋은 꿈 꾸세요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 45, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "꿈", "start_ms": 225, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "꾸세요", "start_ms": 345, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "주말에 뭐 할 거예요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "주말에", "start_ms": 50, "dur_ms": 190, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 240, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 330, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "거예요?", "start_ms": 410, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "친구를 만날 거예요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "친구를", "start_ms": 40, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "만날", "start_ms": 250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 400, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "재미있게 노세요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "재미있게", "start_ms": 35, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "노세요", "start_ms": 285, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "고마워요",
"speaker": "speaker_1",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "고마", "start_ms": 30, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "워요", "start_ms": 190, "dur_ms": 320, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "어디에 사세요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "어디에", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "사세요?", "start_ms": 220, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "서울에 살아요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "서울에", "start_ms": 45, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "살아요", "start_ms": 255, "dur_ms": 270, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "직업이 뭐예요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "직업이", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "뭐예요?", "start_ms": 250, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "저는 학생입니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "학생", "start_ms": 180, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "입니다", "start_ms": 360, "dur_ms": 200, "phase_lr_deg": 0.0}
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
"transcript": "전화번호가 어떻게 되세요?",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "전화", "start_ms": 60, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "번호가", "start_ms": 200, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 380, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "되세요?", "start_ms": 530, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "제 번호는요...",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "제", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "번호는요...", "start_ms": 120, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "나중에 연락할게요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "나중에", "start_ms": 35, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "연락", "start_ms": 235, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "할게요", "start_ms": 385, "dur_ms": 170, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 06:34:02*
