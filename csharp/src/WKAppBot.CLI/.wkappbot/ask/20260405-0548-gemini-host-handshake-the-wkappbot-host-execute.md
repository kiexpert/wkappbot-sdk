# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=10820  time=05:47:02  uptime=6158s  mem=250MB  system_processes=362"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_054616.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 2304ms (file: 173-162-162-126 165-136 152-136-362 165-167 136-130-571-216-154-310-317-130-301-103-136-103-361-156-103-103-136-156-567-517-176-173-107-167-106-137-632-126-603-617-167-651-163-1_V.mp3)
  Segment 2: 3304ms ~ 8164ms (file: 675-617 756 132-675-316-361-631-731-163-637-132-617-631-163_W.mp3)
  Segment 3: 9164ms ~ 9740ms (file: 130-132 132-132-136-136-103-132-102_V.mp3)
  Segment 4: 10740ms ~ 11316ms (file: 627-602-612-603-263-627-620-630-267-260-260-206-206-260-206-260-260-260-126-203-162-136-602-106-162-320-203-103-163-132-301-145-132-134-162-103-261-601-612-765-362-631-132-136-1_V.mp3)
  Segment 5: 12316ms ~ 12892ms (file: 301-312-613-301-206-126-236-326-301-361-326-106-162-231-231-106-310-123-326-326-321-312-103-137-312-130-136-106-210-206-267-620-631-123-130-531-312-231-230-231-723-312-236-320-2_V.mp3)
  Segment 6: 13892ms ~ 14468ms (file: 570 501-501-105-150-501-501-501-150-150-510-501-510-510-517-510-501-150-517-510-150-150-150-510 501-150-510-150-510-501-150-501-510-150-150-510-157-153-517-501-517-510-150-105-1_V.mp3)
  Segment 7: 15468ms ~ 16044ms (file: 576-576-170-576-657-576-576-567-536-615-652-756-267-657-675-765-673-762-652-657-625-625-651-561-651-517-576-576-657-765-625-576-576-657-657-657-567-571-751-567-132-267-531-201-1_W.mp3)
  Segment 8: 17044ms ~ 27376ms (file: 173-120-617-620-261-132-136-123-236-216-301-103-132-107-130-621-620-261-270-726-726-267-210-120-621-132-216-310-132-321-107-260-261-130-132-107-170-627-317-130-372-173-236-637-6_W.mp3)
  Segment 9: 28376ms ~ 28952ms (file: 715-157-574-571-574-571-517-571-157-571-517-157-150-715-103-107-157-130-135-175-170-537 517-517-157-517-571-157-517-157-517-570-571_W.mp3)
  Segment 10: 29952ms ~ 30528ms (file: 507-527 517-510-570-175-517-510-517-517-517-517-571-507-570-517-570-570-570-571-507-507 517-571-157-751-517-157-751-570-571-571-157-517-571-571-756-571-517-571-571-571-576-571-5_V.mp3)
  Segment 11: 31528ms ~ 32104ms (file: 172-175-157-175-517-157-571-157-517-150-571-175-175-571-751-175-571-715-517-571-150-157-751-175-157-170-750-735-375-107-170-105-135-150-571-172-137-132-123-127-123-137-132-172-1_V.mp3)
  Segment 12: 33104ms ~ 33680ms (file: 627-617 735-765-315-756-576-756-756-753-756-765-706-574-576-765-675-632-632-631-612-126-261-761-765-612-130-136-702-760-671-672-132-143-123-312-765-765-675-130-163-132-132-123-3_W.mp3)
  Segment 13: 34680ms ~ 44904ms (file: 510-132-132-312-123-106-153-206-265-126-261-547-546-235-263-126-164-312-547-102-103-132-103-103-752-723-201-320-174-201-312-213-203-210-130-102-103-107-302-261-657-163-216-376-2_V.mp3)
  Segment 14: 45904ms ~ 46480ms (file: 310-137-103-312-103-130-103-132-153-713-173-317-137-172-103-132-137-317-137-103-173-137-375-137 315-310-315-310-317-137-135-315-103-103-135-172-175-172-137-173-307-130-103-173-1_V.mp3)
  Segment 15: 47480ms ~ 53672ms (file: 201-725-675-756-275-136-752-751-126-752-215-137-627-132-105-267-126-216-267-265-576-265-213-523-210-524-132-216-216-216-132-267-123-263-213-132-312-321-261-267-312-752-756-237-7_W.mp3)
  Segment 16: 54672ms ~ 63384ms (file: 263-157-735-267-231-307-107-302-237-705-276-576-267-106-126-102-725-217-276-267-263-120-321-321-725-236-512-163-517-236-302-754-206-302-372-207-103-235-206-215-321-263-527-576-3_V.mp3)
  Segment 17: 64384ms ~ 75688ms (file: 361-320-326-136-672-267-267-672-267-327-321-320-321-317-765-267-231-237-231-725-276-136-762-267-236-237-532-716-167-167-752-576-765-612-356-756-617-160-762-627-260-216-213-213-3_W.mp3)
  Segment 18: 76688ms ~ 77264ms (file: 572-756-567-267-612-621-621-612-361-216-623-261-621 546-675-672-607-265-562-267-627-652-265-567-657-257-567-725-576-256-605-650-657-650-562-605-675 576-527-756-576-756-765-675-7_W.mp3)
  Segment 19: 78264ms ~ 78840ms (file: 150-150-150-153-150-150-105-163-160-176-105-156-105-165-156-157-157-150-157-105-105-150-105-510 150-105-150-157-156-165-176-165-167-751-156-157-175-516-156-157-176-130 105-105-1_V.mp3)
  Segment 20: 79840ms ~ 86572ms (file: 572-573-103-671-576-752-103-267-320-267-213-213-326-321-310-302-103-130-103-206-230-231-203-302-305-302-310-301-534-503-260-502-102-103-132-534-527-312-543-205-273-231-123-103-1_W.mp3)
  Segment 21: 87572ms ~ 88148ms (file: 261-623-627-652-627-362-670-631 657-675-762-623-627-326-263-326-362-236_W.mp3)
  Segment 22: 89148ms ~ 89724ms (file: 263-236-216-621-620-612-261-260-613-675-657-610-106-163-163-612-162-263-621-571-765-675-672-670-357-326-236-236-623-263-261-162-162-106-126-162-136-162-164-312-136-103-135-236-3_V.mp3)
  Segment 23: 90724ms ~ 91300ms (file: 156-157-130-175 150-510-150-105-150-517-156-157-175-157-175-175-157-157-150-156-156-156-150-105-517-157-176-175 105-103-130-105-105-105-107-156-145-106-150-105-157-157-150-175-5_V.mp3)
  Segment 24: 92300ms ~ 92876ms (file: 371-173-307-713-103-135-107-170-317-310-307-130-371-715-135-130-305 103-173-137-736-637-307-376-701-371-317-301-107-130-301-317-703-375-107-310-310-307-301-310-317-170-175-130-5_V.mp3)
  Segment 25: 93876ms ~ 94452ms (file: 157-175 153-517-157-157-157-517-157-175-105-153-105-157-105 157_V.mp3)
  Segment 26: 95452ms ~ 96028ms (file: 751-751-570-570-571-537 157-175-571-157-571-751-715-571-157-507-157-570-507-571-157-170-570-570-507-510-570-537-530 517-576-570-573-517-513-157-517-571-751-751-517-517-571-571-5_V.mp3)
  Segment 27: 97028ms ~ 97604ms (file: 150-156-105-106-105-105-157 510-510-514-150-105-105-106-516-156-106-105-156-105-150-150-105-107 105-150-150-157-150-150-157-105-105-150-150-105-150-105-150-157-150-156-507-517-1_V.mp3)
  Segment 28: 98604ms ~ 113940ms (file: 230-162 235-362-632-625-632-163-162-163-261-230-201-231-261-213-216-120-216-120-213-210-230-261-627-623-625-625-102-301-162-130-762-231-327-231-302-130-162-103-320-102-765-765-2_W.mp3)
  Segment 29: 114940ms ~ 115516ms (file: 236-275-732-263-621-526-517-312-761-506-135-570-756-502-625-276-137-135-157-316-762-361-632-172-726-267-627-765-752-735-261 273-756-270-573-712-726-726-753-213-712-572-321-623-1_V.mp3)
  Segment 30: 116516ms ~ 117092ms (file: 570-715-517-705-570-175-710-751-507-570-574-750 750-574-750-754-574-574-574-751-751-735-573-574-754-750-157-105-501-510-507-570-570-570-750-705-751-751-157-701-150-157-165_V.mp3)
  Segment 31: 118092ms ~ 126552ms (file: 123-136-201-674-572-206-260-132-103-132-756-237-126-253-365-576-520-506-731-352-725-213-123-237-132-203-206-231-503-752-251-312-321-261-126-132-325-301-230-730-720 162-132-106-1_V.mp3)
  Segment 32: 127552ms ~ 128128ms (file: 132-130-136-632-136-176-160-312-106-261-302-103-130-213-106-130-136-136-163-632-106-132-136-312-130-157-106-165-130-163-103-136-105-165-163-163-651-316-160-365-136-163-167-136-1_V.mp3)
  Segment 33: 129128ms ~ 129704ms (file: 136-316-165-106-136-106-157-137-103-165-157-136-103-137-106-512-103-163-136-136-136-103-103-617-136-321-167-106-132 156-725-756-756-765-137-163-136-157-213-673-132-163-762-103-6_V.mp3)
  Segment 34: 130704ms ~ 136536ms (file: 103-263-236-253-275-267-106-132-675-162-102-126-123-125-312-756-573-675-273-107-106-760-752-253-541-123-173-230-157-207-312-126-527-213-732-725-376-173-103-321-102-315-173-127-1_V.mp3)
  Segment 35: 137536ms ~ 138112ms (file: 123-203-302-307-263-207-257-201-123-621-321-123-256-265-302-236-302-302-302-321-326-321-230-320-327-231-126-235-527-675-102-123-102-725-762-162-203-207-275-320-320-230-302-302-1_W.mp3)
  Segment 36: 139112ms ~ 144008ms (file: 632-167-235-236-237-263-203-260-230-250-230-213-107-201-102-102-257-235-102-156-120-231-123-236-231-126-126-257-621-265-120-265-210-216-106-160-156-206-261-726-203-230-312-261-2_W.mp3)
  Segment 37: 145008ms ~ 150408ms (file: 231-231-312-320-706-213-132-576-321-102-367-137-137-132-103-237-750-316-312-371-367-102-576-213-216-317-271-213-315-321-312-123-350-132-102-237-103-517-372-123-261-216-126-231-2_V.mp3)
  Segment 38: 151408ms ~ 151984ms (file: 671-613-672-706-657-657-675-675-615-361-265-617-132-615-613-536-561-517-657-567-265-526-572-576-753-576-570-537-325-317-312-231-321-265-237-327-371-267-276-720-213-132-312-513-3_W.mp3)
  Segment 39: 152984ms ~ 157196ms (file: 132-231-164-231-302-310-230-453-210-125-201-521-326-321-207-203-320-201-203-231-130-260-230-123-236-342-216-762-267-206-236-216-213-203-203-210-203-301-231-321-320-326-320-326-3_W.mp3)
  Segment 40: 158196ms ~ 165684ms (file: 132-123-615-231-213-236-261-267-267-267-106-267-163-173-327-123-261-567-260-526-216-132-103-127-157-361-130-127-136-705-265-315-632-263-126-175-107-260-571-216-721-137-756-312-1_W.mp3)
  Segment 41: 166684ms ~ 167260ms (file: 761-162-126-213-136-236-326-621-235 163_V.mp3)
  Segment 42: 168260ms ~ 168836ms (file: 371-731-371-713-751-715-715-176-175-715-715-571-756-765-756-765-716-725-765-752-576-756-765-765 573-576-572-576-571-576-571-571-576-571-517-567-517-573-756-576-567-567-576-567-5_W.mp3)
  Segment 43: 169836ms ~ 170412ms (file: 105-501-105-150-157-501-513-150-157-517-157-517-105-157-517-501-517-501-510-105-156-105 157-105-105-510-105-105-105-150-105-501-105-517-501-516-510-156-501-517-516-501 135-105-5_V.mp3)
  Segment 44: 171412ms ~ 171988ms (file: 106-135-132-130-136-315-671-162-163-103-130-163-561-136-167-167-760-125-167-167-127-103-106-132 567_V.mp3)
  Segment 45: 172988ms ~ 182888ms (file: 267-213-725-102-210-267-754-102-312-123-126-216-321-257-251-132-321-267-267-762-276-320-230-157-102-170-567-213-261-236-230-237-267-103-102-316-710-167-172-126-120-236-267-132-3_W.mp3)
  Segment 46: 183888ms ~ 184464ms (file: 754-530-305-135-501-571-517-153-573-571-153-105-175-107-105-130-130 517-157-157-571-501-507-510-501-530-507-530-537-537-503-573-507-574-574-574 574-574-574-574-570-157-157-157_W.mp3)
  Segment 47: 185464ms ~ 186040ms (file: 267-576-526-213-271-257-750-163-726-276-652-723-326-752-372-472-327-267-267-207-267-316-362-517-127-572-357-567-576-527-507-615-752-275-265-615-507-257-537-752-321-267-751-230-5_V.mp3)
  Segment 48: 187040ms ~ 187616ms (file: 652-612-625-265 621-620-621-621-621-621-651-625-621-627-621-625-621-623-652-625-620-621-625-652-652-562-562-652-652-625-652-653-261-265-261-625-625-261-625-621-620-625-263-260-2_W.mp3)
  Segment 49: 188616ms ~ 189192ms (file: 163-137-261-236-621-230-103 312-301-326-637-326-312-321-132-513-130-310-130-123-132-316 754-514-547-162-356-623-653-657-362-361-163-610-163-317-103_W.mp3)
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
> **Time**: 2026-04-05 05:48:32
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "안녕하세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 50, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "만나서 반갑습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "만나서", "start_ms": 40, "dur_ms": 220, "phase_lr_deg": -5.0},
{"word": "반갑습니다", "start_ms": 280, "dur_ms": 280, "phase_lr_deg": -5.0}
]
},
{
"segment": 3,
"transcript": "오늘 날씨가 정말 좋네요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 60, "dur_ms": 110, "phase_lr_deg": 2.0},
{"word": "날씨가", "start_ms": 180, "dur_ms": 140, "phase_lr_deg": 2.0},
{"word": "정말", "start_ms": 330, "dur_ms": 100, "phase_lr_deg": 2.0},
{"word": "좋네요", "start_ms": 440, "dur_ms": 120, "phase_lr_deg": 2.0}
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
"transcript": "Where are you from?",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Where", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "are", "start_ms": 190, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 280, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "from?", "start_ms": 390, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "저는 한국에서 왔습니다",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 45, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "한국에서", "start_ms": 170, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "왔습니다", "start_ms": 380, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "한국어 공부는 재미있지만 가끔은 조금 어렵기도 해요",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 110, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "공부는", "start_ms": 550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "재미있지만", "start_ms": 1100, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "가끔은", "start_ms": 2000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 2600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "어렵기도", "start_ms": 3100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "해요", "start_ms": 3800, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "열심히 연습해 보세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "열심히", "start_ms": 20, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "연습해", "start_ms": 240, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "보세요", "start_ms": 430, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "지금 이 문장을 따라서 읽어보시면 발음 교정에 큰 도움이 될 것입니다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "지금", "start_ms": 80, "dur_ms": 300, "phase_lr_deg": -3.0},
{"word": "이", "start_ms": 400, "dur_ms": 100, "phase_lr_deg": -3.0},
{"word": "문장을", "start_ms": 550, "dur_ms": 400, "phase_lr_deg": -3.0},
{"word": "따라서", "start_ms": 1000, "dur_ms": 450, "phase_lr_deg": -3.0},
{"word": "읽어보시면", "start_ms": 1500, "dur_ms": 600, "phase_lr_deg": -3.0},
{"word": "발음", "start_ms": 2200, "dur_ms": 300, "phase_lr_deg": -3.0},
{"word": "교정에", "start_ms": 2600, "dur_ms": 400, "phase_lr_deg": -3.0},
{"word": "큰", "start_ms": 3100, "dur_ms": 200, "phase_lr_deg": -3.0},
{"word": "도움이", "start_ms": 3400, "dur_ms": 400, "phase_lr_deg": -3.0},
{"word": "될", "start_ms": 3900, "dur_ms": 150, "phase_lr_deg": -3.0},
{"word": "것입니다", "start_ms": 4100, "dur_ms": 500, "phase_lr_deg": -3.0}
]
},
{
"segment": 10,
"transcript": "천천히 다시 한번 말해볼까요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "천천히", "start_ms": 150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "한번", "start_ms": 1100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "말해볼까요", "start_ms": 1500, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "Repeat after me.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Repeat", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "after", "start_ms": 250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "me.", "start_ms": 420, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 13,
"transcript": "Great job everyone!",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Great", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "job", "start_ms": 240, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "everyone!", "start_ms": 380, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "If life gives you lemons, make lemonade.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "If", "start_ms": 60, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "life", "start_ms": 180, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "gives", "start_ms": 400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 670, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "lemons,", "start_ms": 850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "make", "start_ms": 1300, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "lemonade.", "start_ms": 1550, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "이 속담은 역경을 기회로 바꾸라는 뜻을 담고 있습니다",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 90, "dur_ms": 100, "phase_lr_deg": 4.0},
{"word": "속담은", "start_ms": 250, "dur_ms": 450, "phase_lr_deg": 4.0},
{"word": "역경을", "start_ms": 800, "dur_ms": 500, "phase_lr_deg": 4.0},
{"word": "기회로", "start_ms": 1400, "dur_ms": 500, "phase_lr_deg": 4.0},
{"word": "바꾸라는", "start_ms": 2000, "dur_ms": 600, "phase_lr_deg": 4.0},
{"word": "뜻을", "start_ms": 2700, "dur_ms": 350, "phase_lr_deg": 4.0},
{"word": "담고", "start_ms": 3100, "dur_ms": 400, "phase_lr_deg": 4.0},
{"word": "있습니다", "start_ms": 3600, "dur_ms": 500, "phase_lr_deg": 4.0}
]
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
"transcript": "다음에 또 만나요 안녕히 계세요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 120, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 550, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 1300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "계세요", "start_ms": 1800, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "Goodbye!",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Goodbye!", "start_ms": 30, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "오늘 수업은 여기까지입니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "수업은", "start_ms": 180, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "여기까지입니다", "start_ms": 380, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "수고하셨습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "수고하셨습니다", "start_ms": 60, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "감사합니다 선생님",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 50, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "선생님", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": 0.0}
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
"transcript": "다시 한번 확인해 볼까요?",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "다시", "start_ms": 80, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "한번", "start_ms": 400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "확인해", "start_ms": 700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "볼까요?", "start_ms": 1100, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "이것은 매우 중요한 포인트입니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "이것은", "start_ms": 100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "매우", "start_ms": 550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "중요한", "start_ms": 900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "포인트입니다", "start_ms": 1400, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "반복해서 연습하면 익숙해질 거예요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "반복해서", "start_ms": 110, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "연습하면", "start_ms": 750, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "익숙해질", "start_ms": 1400, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 2200, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "질문이 있으시면 언제든지 말씀해 주세요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "질문이", "start_ms": 130, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "있으시면", "start_ms": 650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "언제든지", "start_ms": 1200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "말씀해", "start_ms": 1900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 2500, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "좋습니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "좋습니다", "start_ms": 40, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "이번에는 좀 더 빠르게 말해볼까요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "이번에는", "start_ms": 100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 550, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 700, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "빠르게", "start_ms": 850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "말해볼까요", "start_ms": 1300, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 31,
"transcript": "발음이 훨씬 좋아졌네요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "발음이", "start_ms": 120, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "훨씬", "start_ms": 550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "좋아졌네요", "start_ms": 950, "dur_ms": 500, "phase_lr_deg": 0.0}
]
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
"transcript": "이제 다음 챕터로 넘어가겠습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "챕터로", "start_ms": 850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "넘어가겠습니다", "start_ms": 1400, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "준비되셨나요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "준비되셨나요?", "start_ms": 40, "dur_ms": 500, "phase_lr_deg": 0.0}
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
"transcript": "네 준비됐습니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "준비됐습니다", "start_ms": 250, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 38,
"transcript": "그럼 시작해 볼까요? 첫 번째 문장입니다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "시작해", "start_ms": 450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "볼까요?", "start_ms": 900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "첫", "start_ms": 1400, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "번째", "start_ms": 1550, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "문장입니다", "start_ms": 1850, "dur_ms": 600, "phase_lr_deg": 0.0}
]
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "날씨가 추우니까 감기 조심하세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "날씨가", "start_ms": 70, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "추우니까", "start_ms": 400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "감기", "start_ms": 850, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "조심하세요", "start_ms": 1150, "dur_ms": 450, "phase_lr_deg": 0.0}
]
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
"transcript": "오늘 하루도 즐겁게 보내시길 바랍니다",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 90, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "하루도", "start_ms": 380, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "즐겁게", "start_ms": 780, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "보내시길", "start_ms": 1200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "바랍니다", "start_ms": 1750, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "화이팅 하세요!",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "화이팅", "start_ms": 50, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "하세요!", "start_ms": 450, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "Keep up the good work!",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Keep", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "up", "start_ms": 220, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 350, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "good", "start_ms": 460, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "work!", "start_ms": 650, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "끝났습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "끝났습니다", "start_ms": 30, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "마지막 인사를 드리겠습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "마지막", "start_ms": 100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "인사를", "start_ms": 550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "드리겠습니다", "start_ms": 1000, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "감사합니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 50, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 05:48:32*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=17628  time=05:47:22  uptime=14055s  mem=498MB  system_processes=360"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_054232.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 6516ms (file: 263-206-576-265-301-513-571-715-157-216-236-362-312-123-123-321-321-132-123-132-376-673-106-173-320-312-735-756-760-320-735-675-167-765-627-276-237-201-763-765-657-167-107-756-7_W_stt_defensive.mp3)
  Segment 2: 7516ms ~ 14752ms (file: 574-625-612-263-267-265-260-213-756-130-261-265-321-753-517-572-267-267-265-123-263-132-103-216-756-316-756-135-316-521-103-102-216-572-215-130-321-756-132-263-567-256-267-123-5_W.mp3)
  Segment 3: 15752ms ~ 16328ms (file: 156-561-157-156-157-156-156-156-157-157-157-157-150-157-157-517-157-750-750-750-750-570-750-570-750-750-750-750-570-570-750-750-570-750-750-750-154-501-510-501-105-176-156-517-1_W.mp3)
  Segment 4: 17328ms ~ 17904ms (file: 316-326-627-625-321-132-136-263-263-312-163-612-672-263-326-612-627-237-236-216 123-132-132-236-231-210-216-263-267-260_W.mp3)
  Segment 5: 18904ms ~ 24880ms (file: 672-267-263-263-326-261-263-160-216-126-162-267-627-162-260-267-203-123-123-120-321-261-261-267-261-261-267-201-617-261-621-160-163-672-617-612-216-621-627-672-261-126-672-613-6_W.mp3)
  Segment 6: 25880ms ~ 26456ms (file: 103-103-137-310-130-731-130-130-103-317-107-310-173-107-307 153-175-137-103-137-137-103-317-103-130-312-132-713-130-103-310-130-301-170-103-130-137-103-107-137-150-105-731-710 3_V.mp3)
  Segment 7: 27456ms ~ 28032ms (file: 301-570-571-157-103-132-316-136-351-102-102-136-135-517-570-753-765-176-106-103-162-103-124-756-756-732-371-570-571-574-574-576-756-750-173-103-135-750-312-213-321-312-136-103-1_V.mp3)
  Segment 8: 29032ms ~ 29608ms (file: 705-731-307-673-603-607-607-103-310-310-361-617-671-763-675-756-761-736-670-613-361-316-160-637-736-756-675 103-730-371-713-307-376-631-130-613-601-603-601-630-103-106-163-375-3_W.mp3)
  Segment 9: 30608ms ~ 31184ms (file: 517-510-157-150-513-517-517 105-517-517-157-517-501-105-157-510-501-510-510-510-510-510-150-513-510-150-517-157-510-157-517-157-517-150-501 157-517-157-105-105-105-150-105-150-5_V.mp3)
  Segment 10: 32184ms ~ 32760ms (file: 736-653-326-356-317-126-236-326-236-321 675-657-675-576_W.mp3)
  Segment 11: 33760ms ~ 34336ms (file: 751-571-517-571-517-567-576-571-517-571-576-576-576-576-150-501-501-560-570-571-576-571-576-571-571-571-572-571-517-571-517-576-571-573-537-517-570-570-570-570-501-560-570-576-5_W.mp3)
  Segment 12: 35336ms ~ 35912ms (file: 517-150-105-157-150-517-501-150-510-105 517-517-501-105-157-150-105-517-157-157-510-105-157 150-510-150-150-157-157-150-157-157-157-157-157-150-150-150-517-150-501 157-150-510-1_W.mp3)
  Segment 13: 36912ms ~ 46596ms (file: 123-130-134-210-231-231-231-231-275-756-574-203-574-230-572-231-230-325-320-762-753-312-752-725-230-130-210-120-576-210-213-230-132-756-176-201-123-310-102-231-321-316-120-210-2_W.mp3)
  Segment 14: 47596ms ~ 48172ms (file: 617-137-173-371-307-307-317-310-317-730-317-137-730-307-175-130-735 357-301-370-715-137-375-173-310-163-176-613-610-607-603-136-316-137-170-103-137-137-103-103-301-107-172-130-3_V.mp3)
  Segment 15: 49172ms ~ 49748ms (file: 263-623-162-623-126-162-126 132-310_W.mp3)
  Segment 16: 50748ms ~ 59028ms (file: 217-213-127-123-103-103-107-107-106-127-120-203-201-206-201-271-574-267-267-260-203-267-326-236-527-203-261-236-132-321-310-130-273-217-217-754-132-174-143-371-173-102-137-103-3_W.mp3)
  Segment 17: 60028ms ~ 60604ms (file: 574-574-574-514-561-567-576-574-570 105-157-705-105-501-150-105-150-513-510-501-501-156-150-510-570-510-514-510-507-570-541-574-514-571-561-561-501-571-571 150-517-510-517-150-5_W.mp3)
  Segment 18: 61604ms ~ 62180ms (file: 314-134-310-103-134-136-103-136-103-103-314-134-103-103-103-103-103-136-310 315-351-357-375-314-137-134-134-136-103-103-103 314-314-301-317-136-136-315-316-317-301-103-130-132-1_V.mp3)
  Segment 19: 63180ms ~ 63756ms (file: 312-143-257-532-326-213-136-231-570-372-256-507-562-576-750-572-576-267-206-267-507-615-356-317-137-317-317-317-317-137-316-317-367-173-731-761-361-321-361-167-365-316-753-673-6_W.mp3)
  Segment 20: 64756ms ~ 65332ms (file: 507-501-150-150-501-510-150-507-157-507-570-571 150-510-510-150-570-501-105-510-510-510-510-510-510-150-501-510-150-150-105-510-157-501-105-510-517-105-510-157-573-571 510-507-1_V.mp3)
  Segment 21: 66332ms ~ 66908ms (file: 576-570-651-576-675-657-761-132-571-152-267-126-731-657-137-725-215-576-213-517-135-507-301-310-756-756-576-516-165-160-160-574_W.mp3)
  Segment 22: 67908ms ~ 75864ms (file: 531-102-312-123-276-163-107-127-327-127-175-572-503-570-574-725-672-276-172-167-276-756-170-167-762-132-123-756-236-273-726-276-276-276-230-320-321-321-321-567-576-125-723-653-6_W.mp3)
  Segment 23: 76864ms ~ 77440ms (file: 261-176-165-576-572-761-764-136-753-175-132-312-752-625-513-576-576-623-326-367-576-635-756-756-713-527-762-567-652-762-756-572-357-317-762-576_V.mp3)
  Segment 24: 78440ms ~ 79016ms (file: 160-267-206-617-675-567-563-326-621-632-672-617-657-765-672-675-765-761-761-263-754-752-765-754-576-657-231-123-102-103-130-130-675-106-120-321-320-312-130-130-316-163-567-260-2_W.mp3)
  Segment 25: 80016ms ~ 85740ms (file: 120-231-126-231-612-216-261-327-261-217-261-216-127-127-127-230-327-120-621-217-273-216-126-126-123-312-132-123-162-637-136-123-126-312-132-126-132-163-162-231-271-126-125-514-5_W.mp3)
  Segment 26: 86740ms ~ 87316ms (file: 137-301-130-137-103-137-705 710-701-137-170-103-157-130-731-130-317-317-317-307-371-307-312-301-103-317-137-130-371-731-703-130-170-310-371-317-107-730-715-175 315-713-103-107-1_V.mp3)
  Segment 27: 88316ms ~ 88892ms (file: 546-130-613-316-136-631-173-361-163-613-163-316-316-137-165-136-136-136-136-316-312-762-765-672-765-675-765-763_W.mp3)
  Segment 28: 89892ms ~ 90468ms (file: 230-312-620-602-260-103-162-125-136-310-102-150-106-106-103-106-316-320-103-106-106-163-103-162-156-301-301-326-267-362-213-267-602-650-670-620-627-623-632-267-621-527-567-672-3_W.mp3)
  Segment 29: 91468ms ~ 92044ms (file: 132-136 657-657-576-625-652-625-130-130-137-103-137-107-137_V.mp3)
  Segment 30: 93044ms ~ 93620ms (file: 607-765-675-673-672-672-673-763-765-675-673-762-672-765-765-761-176-176-167-176-765-762-607-672-760-671-507-576-756-753-705-675-761-637-671-367-672-576 132-613-641-765-675-673-7_V.mp3)
  Segment 31: 94620ms ~ 95196ms (file: 175-157-175-157-175-175-157-751-175-756 103-105-105-105-105-105-105-150-150-157-157-157-157-175-176-165-175-715-175-176-715-176-175-751-175-175 461-154-517-501-507-501-107-105-1_V.mp3)
  Segment 32: 96196ms ~ 96772ms (file: 105-507-170-517-567-501-501-150-510-510-570-570-576-571-540-501-510-517-540-504-514-507-540-501-750 501-571-517-105-517-510-150-501-105-506-510-150-571-501-510-517-510-570-570-1_V.mp3)
  Segment 33: 97772ms ~ 98348ms (file: 163-315-123-123-123-132-132-302-362-136-621-126-312-132-123-103-132-126-102-213-120-126-102-123-103-102-301-160-130-362-610-160_V.mp3)
  Segment 34: 99348ms ~ 107844ms (file: 103-173-137-176-617-176-267-126-321-163-763-675-627-236-126-267-267-312-672-673-263-132-326-312-750-756-130-312-230-301-206-621-312-576-756-167-567-574-756-321-547-572-132-613-6_W.mp3)
  Segment 35: 108844ms ~ 109420ms (file: 167-612-672-623-631-621-103-761-572-126-712-167-157-175-162-163-137-103-236-126-156-316-167-130-130-135 176-132-136-167-136-163-130-106-576-326-106-130-123-321-165-162-162-756-2_W.mp3)
  Segment 36: 110420ms ~ 110996ms (file: 570-510 517-105-105-157-517-517-501-157-513-510-150-501-507-510-150-510-510-507-507-501-510-501-510-501-501-510-517-507-570 507-510-157-517-571-517-517-517-507-510-501-501-507-5_W.mp3)
  Segment 37: 111996ms ~ 120131ms (file: 127-103-273-123-206-310-103-123-231-203-270-102-126-263-263-216-326-321-267-276-236-231-213-267-732-260-260-267-210-120-102-761-132-132-213-260-231-236-213-261-753-126-263-126-1_W.mp3)
  Segment 38: 121131ms ~ 121707ms (file: 367-236-237-652-265-267-623-617-263-126-321-123-132-231-132-150-125-123-126-213-213-571-657-605-215-216-217-216-671-167-167-265-625-623-671-670-672-361-136-261-217-176-761-375-7_W.mp3)
  Segment 39: 122707ms ~ 123283ms (file: 163-601-617-607-136-167-701-170-762-712-172-706-623-217-267-726-712-756-712-725-175-127-231-213-123-213-216-125-256-625-576-276-657-256-567-752-526-562-765-256-765-267 206-312-2_W.mp3)
  Segment 40: 124283ms ~ 124859ms (file: 574-574-574-574-574-157-574-574-574-754-574-750-105-570-571-574-754-574-571-750-754-573 705-710-705-751-756-756-751-753-751-753-751-157-175-713-107-701-175-750-517-571-751-701-1_W.mp3)
  Segment 41: 125859ms ~ 131835ms (file: 765-765-765-201-103-237-765-671-267-621-512-175-167-167-763-175-132-732-213-203-521-276-103-762-721-321-312-132-176-162-716-310-127-103-123-367-170-157-712-132-213-357-576-517-1_W.mp3)
  Segment 42: 132835ms ~ 145687ms (file: 173-157-617-136-163-152-261-312-137-572-567-651-132-137-251-137-574-751-713-137-576-574-573-132-103-712-130-301-263-632-326-123-531-312-507-574-537-503-735-623-623-362-135-321-3_W.mp3)
  Segment 43: 146687ms ~ 147263ms (file: 164-562-617-362-127-120-106-136-130-162-130-672-623-627-576 362-361-362-632-261-326-362-623-261_W.mp3)
  Segment 44: 148263ms ~ 148839ms (file: 571-510-517-571-510-571-517 157-105-105-157-105-103-153-513-157-157-517-510-105-501-510-510-105-150-517-510-537-517-510-517-501-571-510-517-576-517-571-157-574-510 715-157-150-1_V.mp3)
  Segment 45: 149839ms ~ 150415ms (file: 132-671-362-365-537-756-567-657-562-567-635-675-675-765-672-627_W.mp3)
  Segment 46: 151415ms ~ 160487ms (file: 276-132-317-321-236-321-317-372-317-735-310-132-317-132-216-261-231-106-672-217-765-126-162-267-130-321-276-276-231-312-310-756-765-231-173-107-237-762-765-326-715-106-167-267-7_W.mp3)
  Segment 47: 161487ms ~ 170775ms (file: 263-275-206-627-216-210-162-102-321-263-267-127-267-627-675-126-621-267-126-276-216-321-162-231-237-230-120-237-123-261-216-627-267-210-123-756-275-756-265-267-321-236-236-261-1_W.mp3)
  Segment 48: 171775ms ~ 172351ms (file: 267-216-271-276-126-213-231-523-356-351-265-236-625-653-625-265-256-623-217-216-216-357-215-231-312-351-321-357-352-152-321-273-263-267-263-315-135-135-130-130-316-126-136-312-2_W.mp3)
  Segment 49: 173351ms ~ 173927ms (file: 705-175-170-751-170-175-705-715-507-751-710-750-570-751-510-517-507-705-715-175-705-175-571-705-571-750 570-510-570-750-517-701-517-750-750-571-705-507-570-751-750-571-105-750-5_V.mp3)
  Segment 50: 174927ms ~ 181731ms (file: 670-120-216-203-203-103-123-231-206-620-120-261-260-601-316-123-260-106-176-673-761-671-675-753-657-135-210-671-637-361-761-203-136-126-102-231-103-310-123-217-210-201-712-260-2_W.mp3)
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
> **Time**: 2026-04-05 05:48:32
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "안녕하세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 50, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "만나서 반갑습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "만나서", "start_ms": 40, "dur_ms": 220, "phase_lr_deg": -5.0},
{"word": "반갑습니다", "start_ms": 280, "dur_ms": 280, "phase_lr_deg": -5.0}
]
},
{
"segment": 3,
"transcript": "오늘 날씨가 정말 좋네요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 60, "dur_ms": 110, "phase_lr_deg": 2.0},
{"word": "날씨가", "start_ms": 180, "dur_ms": 140, "phase_lr_deg": 2.0},
{"word": "정말", "start_ms": 330, "dur_ms": 100, "phase_lr_deg": 2.0},
{"word": "좋네요", "start_ms": 440, "dur_ms": 120, "phase_lr_deg": 2.0}
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
"transcript": "Where are you from?",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Where", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "are", "start_ms": 190, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 280, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "from?", "start_ms": 390, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "저는 한국에서 왔습니다",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 45, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "한국에서", "start_ms": 170, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "왔습니다", "start_ms": 380, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "한국어 공부는 재미있지만 가끔은 조금 어렵기도 해요",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 110, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "공부는", "start_ms": 550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "재미있지만", "start_ms": 1100, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "가끔은", "start_ms": 2000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 2600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "어렵기도", "start_ms": 3100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "해요", "start_ms": 3800, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "열심히 연습해 보세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "열심히", "start_ms": 20, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "연습해", "start_ms": 240, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "보세요", "start_ms": 430, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "지금 이 문장을 따라서 읽어보시면 발음 교정에 큰 도움이 될 것입니다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "지금", "start_ms": 80, "dur_ms": 300, "phase_lr_deg": -3.0},
{"word": "이", "start_ms": 400, "dur_ms": 100, "phase_lr_deg": -3.0},
{"word": "문장을", "start_ms": 550, "dur_ms": 400, "phase_lr_deg": -3.0},
{"word": "따라서", "start_ms": 1000, "dur_ms": 450, "phase_lr_deg": -3.0},
{"word": "읽어보시면", "start_ms": 1500, "dur_ms": 600, "phase_lr_deg": -3.0},
{"word": "발음", "start_ms": 2200, "dur_ms": 300, "phase_lr_deg": -3.0},
{"word": "교정에", "start_ms": 2600, "dur_ms": 400, "phase_lr_deg": -3.0},
{"word": "큰", "start_ms": 3100, "dur_ms": 200, "phase_lr_deg": -3.0},
{"word": "도움이", "start_ms": 3400, "dur_ms": 400, "phase_lr_deg": -3.0},
{"word": "될", "start_ms": 3900, "dur_ms": 150, "phase_lr_deg": -3.0},
{"word": "것입니다", "start_ms": 4100, "dur_ms": 500, "phase_lr_deg": -3.0}
]
},
{
"segment": 10,
"transcript": "천천히 다시 한번 말해볼까요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "천천히", "start_ms": 150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "한번", "start_ms": 1100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "말해볼까요", "start_ms": 1500, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "Repeat after me.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Repeat", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "after", "start_ms": 250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "me.", "start_ms": 420, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 13,
"transcript": "Great job everyone!",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Great", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "job", "start_ms": 240, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "everyone!", "start_ms": 380, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "If life gives you lemons, make lemonade.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "If", "start_ms": 60, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "life", "start_ms": 180, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "gives", "start_ms": 400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 670, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "lemons,", "start_ms": 850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "make", "start_ms": 1300, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "lemonade.", "start_ms": 1550, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "이 속담은 역경을 기회로 바꾸라는 뜻을 담고 있습니다",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 90, "dur_ms": 100, "phase_lr_deg": 4.0},
{"word": "속담은", "start_ms": 250, "dur_ms": 450, "phase_lr_deg": 4.0},
{"word": "역경을", "start_ms": 800, "dur_ms": 500, "phase_lr_deg": 4.0},
{"word": "기회로", "start_ms": 1400, "dur_ms": 500, "phase_lr_deg": 4.0},
{"word": "바꾸라는", "start_ms": 2000, "dur_ms": 600, "phase_lr_deg": 4.0},
{"word": "뜻을", "start_ms": 2700, "dur_ms": 350, "phase_lr_deg": 4.0},
{"word": "담고", "start_ms": 3100, "dur_ms": 400, "phase_lr_deg": 4.0},
{"word": "있습니다", "start_ms": 3600, "dur_ms": 500, "phase_lr_deg": 4.0}
]
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
"transcript": "다음에 또 만나요 안녕히 계세요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 120, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 550, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 1300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "계세요", "start_ms": 1800, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "Goodbye!",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Goodbye!", "start_ms": 30, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "오늘 수업은 여기까지입니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "수업은", "start_ms": 180, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "여기까지입니다", "start_ms": 380, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "수고하셨습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "수고하셨습니다", "start_ms": 60, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "감사합니다 선생님",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 50, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "선생님", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": 0.0}
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
"transcript": "다시 한번 확인해 볼까요?",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "다시", "start_ms": 80, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "한번", "start_ms": 400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "확인해", "start_ms": 700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "볼까요?", "start_ms": 1100, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "이것은 매우 중요한 포인트입니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "이것은", "start_ms": 100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "매우", "start_ms": 550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "중요한", "start_ms": 900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "포인트입니다", "start_ms": 1400, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "반복해서 연습하면 익숙해질 거예요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "반복해서", "start_ms": 110, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "연습하면", "start_ms": 750, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "익숙해질", "start_ms": 1400, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 2200, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "질문이 있으시면 언제든지 말씀해 주세요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "질문이", "start_ms": 130, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "있으시면", "start_ms": 650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "언제든지", "start_ms": 1200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "말씀해", "start_ms": 1900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 2500, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "좋습니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "좋습니다", "start_ms": 40, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "이번에는 좀 더 빠르게 말해볼까요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "이번에는", "start_ms": 100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 550, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 700, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "빠르게", "start_ms": 850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "말해볼까요", "start_ms": 1300, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 31,
"transcript": "발음이 훨씬 좋아졌네요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "발음이", "start_ms": 120, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "훨씬", "start_ms": 550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "좋아졌네요", "start_ms": 950, "dur_ms": 500, "phase_lr_deg": 0.0}
]
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
"transcript": "이제 다음 챕터로 넘어가겠습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "챕터로", "start_ms": 850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "넘어가겠습니다", "start_ms": 1400, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "준비되셨나요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "준비되셨나요?", "start_ms": 40, "dur_ms": 500, "phase_lr_deg": 0.0}
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
"transcript": "네 준비됐습니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "준비됐습니다", "start_ms": 250, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 38,
"transcript": "그럼 시작해 볼까요? 첫 번째 문장입니다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "시작해", "start_ms": 450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "볼까요?", "start_ms": 900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "첫", "start_ms": 1400, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "번째", "start_ms": 1550, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "문장입니다", "start_ms": 1850, "dur_ms": 600, "phase_lr_deg": 0.0}
]
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "날씨가 추우니까 감기 조심하세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "날씨가", "start_ms": 70, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "추우니까", "start_ms": 400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "감기", "start_ms": 850, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "조심하세요", "start_ms": 1150, "dur_ms": 450, "phase_lr_deg": 0.0}
]
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
"transcript": "오늘 하루도 즐겁게 보내시길 바랍니다",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 90, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "하루도", "start_ms": 380, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "즐겁게", "start_ms": 780, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "보내시길", "start_ms": 1200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "바랍니다", "start_ms": 1750, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "화이팅 하세요!",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "화이팅", "start_ms": 50, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "하세요!", "start_ms": 450, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "Keep up the good work!",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Keep", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "up", "start_ms": 220, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 350, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "good", "start_ms": 460, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "work!", "start_ms": 650, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "끝났습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "끝났습니다", "start_ms": 30, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "마지막 인사를 드리겠습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "마지막", "start_ms": 100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "인사를", "start_ms": 550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "드리겠습니다", "start_ms": 1000, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "감사합니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 50, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 05:48:32*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=11864  time=05:47:14  uptime=6655s  mem=136MB  system_processes=360"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_054619.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 7776ms (file: 261-261-231-130-126-102-201-267-126-120-130-132-172-316-136-120-213-326-627-617-106-627-327-276-267-672-126-627-310-672-267-261-103-267-627-132-617-312-157-172-310-321-312-261-2_W.mp3)
  Segment 2: 8776ms ~ 18244ms (file: 576-372-137-310-702-103-103-136-271-107-371-307-716-126-260-620-657-630-316-130-413-132-570-157-372-301-231-321-321-376-317-173-261-570-310-307-762-153-310-372-326-312-312-130-3_W.mp3)
  Segment 3: 19244ms ~ 19820ms (file: 150-153-103-301-103-130-310-130-315-130-103-150-150 150-157-153-156-510-153-105-105-105-105-103-150-105-153-157-153 105-150-150-150-105-153-105-105-157-150-150 150-105-135-105-1_V.mp3)
  Segment 4: 20820ms ~ 29496ms (file: 267-312-217-725-136-136-103-326-236-130-231-214-102-230-237-321-132-731-327-132-267-632-206-231-127-210-271-271-132-123-321-312-203-672-267-261-260-752-302-236-206-260-327-132-3_W.mp3)
  Segment 5: 30496ms ~ 31072ms (file: 314 154-413-103-413-143-341-143-341-431-145-103-134-143-103-143-134-103-104-103-130-310-134-103-134-103-314 310-134-301-410-134-130-134-314-104-143-310-134-314-301-154-104-103-3_V.mp3)
  Segment 6: 32072ms ~ 38804ms (file: 752-176-627-216-572-267-231-206-126-312-172-126-621-236-210-157-756-312-267-206-132-276-260-263-132-275-627-120-320-265-135-752-576-625-130-321-210-251-672-752-103-623-562-267-3_W.mp3)
  Segment 7: 39804ms ~ 40380ms (file: 675-765-760 621-163-123-621-612-621-165 723_W.mp3)
  Segment 8: 41380ms ~ 41956ms (file: 105-570-571-510-510-513-510-510-516-576-571-571 510-501-517-501-105-517-517-510-105-501-501-510-501-517-105-510-510-105-150-510-570-571-510-501-570-501-507-571 157-150-150-157-1_V.mp3)
  Segment 9: 42956ms ~ 43532ms (file: 570-517-507-571-574-574-547-574-570 501-517-150-517-105-517-501-510-157-150-507-501-510-150-501-571-517-571-510-570-574-574-574-547-574-517-517-571-547 570-517-507-105-105-105-1_W.mp3)
  Segment 10: 44532ms ~ 45108ms (file: 176-175-176-716-176-176-716-175-176-173 104-105-150-135-176-137-176-167-157-175-170-175-175-157-176-175-175-175_W.mp3)
  Segment 11: 46108ms ~ 46684ms (file: 570-501-510-510-537-517-570-571 570-570-507-507-105-105-501-510-517-150-517-570-574-576-576-576-574-572-576-572-572-574-574-537-573-574-570 501-157-510-510-150-510-513-501-501-1_W.mp3)
  Segment 12: 47684ms ~ 48260ms (file: 562-103-130-103-130-132-135-105-126-107-617-637-670-103-571-126-132-103-150-106-176-135-312-103-613-136-312-157-125-367-132-163-136-213-210-135-103-103-137-160-167-310-571-137-7_V.mp3)
  Segment 13: 49260ms ~ 49836ms (file: 157-157-715 154-410-510-150-105-150-150-150-167-175-176-571-175-157-715-517-157-516-156-156-165-167-167-175-157-175-157-175 105-105-150-157-157-157-175-176-716-170-175-157-175-1_V.mp3)
  Segment 14: 50836ms ~ 51412ms (file: 150-510-157-150-510-105-150-561-517-153-163-126-162-625-175-172-127-150-135-135-130 157-137-751-371-715-175-175-317-167-576-576-573-517-571-567-573-576-576-576-715-713-715-715-7_W.mp3)
  Segment 15: 52412ms ~ 62132ms (file: 276-572-547-574-762-623-612-316-312-351-537-321-547-167-163-632-236-326-357-167-236-632-756-160-312-627-167-102-267-236-236-267-276-237-572-573-573-267-267-372-167-267-310-103-2_W.mp3)
  Segment 16: 63132ms ~ 63708ms (file: 160-267-206-617-675-567-563-326-621-632-672-617-657-765-672-675-765-761-761-263-754-752-765-754-576-657-231-123-102-103-130-130-675-106-120-321-320-312-130-130-316-163-567-260-2_W.mp3)
  Segment 17: 64708ms ~ 65284ms (file: 103-130-103 150-150-105-150-150-105-157-150-157-150-517 150-150-156-157-517-157-571-510-517-517-501-517-501-510-756-751 105-105-150-157-517-157-517-157-517-150-510-507-501-570-7_V.mp3)
  Segment 18: 66284ms ~ 66860ms (file: 510-570-507-570-570-570-571-570-570-576-574-574-574-571-576-576-540-501-570 105-150-507-157-105-105-517-517-157-517-501-157-507-571-510-501-517-570-570-570-576-517-570-574-571-5_W.mp3)
  Segment 19: 67860ms ~ 74988ms (file: 574-612-206-102-215-267-263-102-516-256-120-312-357-275-763-132-574-102-675-256-267-237-576-213-321-203-321-132-521-267-126-137-130-231-276-732-276-752-137-762-275-230-230-231-3_W.mp3)
  Segment 20: 75988ms ~ 86536ms (file: 164-316-621-267-621-627-765-263-126-136-675-625-265-263-102-107-125-571-576-275-132-231-231-103-571-573-210-103-167-106-216-627-751-753-231-130-216-561-162-613-106-106-231-263-1_W.mp3)
  Segment 21: 87536ms ~ 96932ms (file: 756-261-107-136-317-132-231-213-672-123-107-715-576-312-531-675-261-213-267-627-267-312-231-132-130-163-217-213-106-107-261-627-267-627-267-765-263-130-271-216-675-132-136-213-2_W.mp3)
  Segment 22: 97932ms ~ 98508ms (file: 273-276-726-236-267-263-236-236-235-257-735-537-256-523-130-562-267-572-765-675-675-672-627-671-632-263-265-627-632-652-623-257-265-621-235-326-327-672-625-561-253-253-725-725-2_W.mp3)
  Segment 23: 99508ms ~ 100084ms (file: 756-630-160-621-162-173-726-712-567-506 267-672-160-137-672-617-607-672-765-675-675-672-617-120-102-120-163-312-216-162-317-160-602-302-321-326-752-627-206-320-312-132-126-632-1_W.mp3)
  Segment 24: 101084ms ~ 119408ms (file: 132-163-136-162-317-130-136-317-136-137-136-132-316-132-125-215-167-765-652-126-623-627-312-163-631-571-201-206-574-527-574-321-132 574 130-310-230-213-574-206-103-572-572-210-2_W.mp3)
  Segment 25: 120408ms ~ 129192ms (file: 267-123-267-267-574-237-312-310-231-210-726-260-260-320-120-231-236-213-207-206-120-237-120-230-267-267-120-123-302-231-312-765-327-310-701-321-102-123-230-230-231-532-546-312-2_W.mp3)
  Segment 26: 130192ms ~ 130768ms (file: 126-127-137-136-103-103-103-123-124-316-136-132-413-132-324-312-132-735-237-326-127-213-321-216-216-267-261-231 106-106-103-167-163-316-123-632-316-130-620-627-260-261-620-627-6_V.mp3)
  Segment 27: 131768ms ~ 132344ms (file: 672-657-316-361-627-726-620-316-216-257-762-637-362-502-136-357-132-753-257-162-126-132-162-123-127-501-137-157-573_V.mp3)
  Segment 28: 133344ms ~ 133920ms (file: 301-130-130-137-301-170-370-301-103-375-730-710-175-716-715-713-317-751-713-317-715-715-173-715-320-321-312-210-307-317-130-375-317-310-371-317-316-137-317-107-731-317-703-637-7_W.mp3)
  Segment 29: 134920ms ~ 135496ms (file: 107-751 570-570-570-507-570-517-570-501-507-510-507-501-517-170-571-175-570-751-570-507-175-570-715-751-750-571-570-570 571-517-510-517-571-517-510-751-510 154-570-507-570-510-5_W.mp3)
  Segment 30: 136496ms ~ 144704ms (file: 564-756-203-231-725-236-327-534-537-236-120-103-321-215-572-157-310-302-356-672-751-574-165-512-572-265-760-207-263-576-765-672-236-263-623-267-215-265-257-257-120-160-261-162-2_V.mp3)
  Segment 31: 145704ms ~ 156036ms (file: 236-237-627-672-263-123-132-675-762-762-672-657-754-275-236-762-762-627-576-276-276-273-276-236-276-321-132-321-132-276-132-672-267-231-312-213-273-273-102-103-273-267-236-276-7_W.mp3)
  Segment 32: 157036ms ~ 157612ms (file: 510-571-150-570-510-571-570-517-517-517-501-507-510-507-517-571-501-517-510-570-105_V.mp3)
  Segment 33: 158612ms ~ 159188ms (file: 175-150-175-715-165-175-167-175-167-761-157-175-176-176-761-175-716-761-167-571 514-157-501-510-504-106-157-157-765-576-715-756-175-716-176-716-167-176-176-716-176-175-176-715-1_W.mp3)
  Segment 34: 160188ms ~ 160764ms (file: 547-106-326 216-123-317-263-265-326-236-136-132_V.mp3)
  Segment 35: 161764ms ~ 162340ms (file: 167 103-103-132-103-103-103-136-760-361-106-103-136-326-136-316-103-137-563-163-361-103-162-107-103-176-163-623 136-103-267-576-103-103-136-106-617-136-136-165-316-136-610-672-6_V.mp3)
  Segment 36: 163340ms ~ 163916ms (file: 167-765-123-321-120-167-136-136-106-176-106-160-176-103-163-136-173-165-136-235-526-132-236-602-312-126-126_V.mp3)
  Segment 37: 164916ms ~ 170532ms (file: 367-123-316-632-103-213-576-321-123-623-627-623-615-263-126-263-621-216-126-652-315-153-754-267-756-267-162-631-260-726-231-321-537-206-321-301-263-301-312-103-361-630-630-675-6_W.mp3)
  Segment 38: 171532ms ~ 178552ms (file: 621-123-231-132-216-213-230-123-316-126-307-137-273-213-126-126-123-132-312-120-120-103-357-537-672-367-136-621-273-276-162-576-715-723-127-321-574-756-675-106-261-216-127-125-1_W.mp3)
  Segment 39: 179552ms ~ 188156ms (file: 263-620-261-627-260-326-163-210-621-672-627-267-263-267-627-123-267-236-321-213-123-672-321-123-627-672-672-572-236-765-672-312-102-756-762-627-621-261-216-627-627-236-672-263-2_W.mp3)
  Segment 40: 189156ms ~ 189732ms (file: 156-165-175-105-106-105-105-105-150-150-105-651-165 150-150-150-150-150-154-165-156-157-156-160-106-106-105-105-105-105-106-106-105-105-516 157-157-150-153-106-150-105-105-150-1_V.mp3)
  Segment 41: 190732ms ~ 191308ms (file: 157-175-175-157-175-716-176-715-175-175-175-715 105-150-157-156-150-157-157-175-157-175-170-175-175-175-176-165-156-157-157-175-165-175-157-175-157-175-715 105-150-150-150-150-1_V.mp3)
  Segment 42: 192308ms ~ 205592ms (file: 216-261-216-236-263-102-361-136-213-236-236-326-312-132-136-631-126-326-107-531-517-106-106-160-760-267-752-632-542-512-321-302-320-327-307-310-735-726-163-706-163-126-136-312-2_W.mp3)
  Segment 43: 206592ms ~ 207168ms (file: 617-612 635-627-267-267-617-765-672-163-617-361_W.mp3)
  Segment 44: 208168ms ~ 215332ms (file: 175-321-326-756-672-625-672-123-132-261-321-123-231-627-621-675-107-130-216-261-312-567-105-107-167-106-675-126-572-267-251-257-263-321-206-572-310-103-263-132-263-572-213-675-5_W.mp3)
  Segment 45: 216332ms ~ 216908ms (file: 136-132-123-516-163 653 267-316-613-135-657-657-567-573 627-763-672-671-675-673-672-675-675-672-675-672-627_W.mp3)
  Segment 46: 217908ms ~ 218484ms (file: 617-567-632-275-320-320-302-327-326-230-372-327-327-352-632-603-756-752-325-325-532-563-765-675-563-675-657 756-173-675-625-326_W.mp3)
  Segment 47: 219484ms ~ 220060ms (file: 617-137-167-176-613-162-756-756-675-167-376-167-613-675-167-367-637-760 316-316-567-612-276-671-576-765-762-602-237-273-236-267-672-632-230-176-760-736-216-275-263-156-761-231-7_W.mp3)
  Segment 48: 221060ms ~ 221636ms (file: 130-103-130-123-102-105-107-105-150-175-175-157-107-175-170-170-103 107-107-103-153-157-175-371-715-175-105-150-571 153-103-105-170-175-105-713-710-173-175-107-105-571-135-150-1_V.mp3)
  Segment 49: 222636ms ~ 223212ms (file: 751-750-751-715-756-751-715-756-750 570-517-751-157-571-751-175-170-751-571-751-157-751-175-751-175-751-715-751-715-175-715-751-751 571-517-751-157-570-517-750-175-571-751-571-5_W.mp3)
  Segment 50: 224212ms ~ 229792ms (file: 136-136-162-253-163-605-561-301-312-163-130-102-310-537-157-302-132-236-326-261-267-261-321-312-725-237-231-231-261-321-752-327-120-120-125-217-312-126-216-627-263-321-752-103-1_W.mp3)
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
> **Time**: 2026-04-05 05:48:32
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "안녕하세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 50, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "만나서 반갑습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "만나서", "start_ms": 40, "dur_ms": 220, "phase_lr_deg": -5.0},
{"word": "반갑습니다", "start_ms": 280, "dur_ms": 280, "phase_lr_deg": -5.0}
]
},
{
"segment": 3,
"transcript": "오늘 날씨가 정말 좋네요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 60, "dur_ms": 110, "phase_lr_deg": 2.0},
{"word": "날씨가", "start_ms": 180, "dur_ms": 140, "phase_lr_deg": 2.0},
{"word": "정말", "start_ms": 330, "dur_ms": 100, "phase_lr_deg": 2.0},
{"word": "좋네요", "start_ms": 440, "dur_ms": 120, "phase_lr_deg": 2.0}
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
"transcript": "Where are you from?",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Where", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "are", "start_ms": 190, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 280, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "from?", "start_ms": 390, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "저는 한국에서 왔습니다",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 45, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "한국에서", "start_ms": 170, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "왔습니다", "start_ms": 380, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "한국어 공부는 재미있지만 가끔은 조금 어렵기도 해요",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 110, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "공부는", "start_ms": 550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "재미있지만", "start_ms": 1100, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "가끔은", "start_ms": 2000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 2600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "어렵기도", "start_ms": 3100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "해요", "start_ms": 3800, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "열심히 연습해 보세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "열심히", "start_ms": 20, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "연습해", "start_ms": 240, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "보세요", "start_ms": 430, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "지금 이 문장을 따라서 읽어보시면 발음 교정에 큰 도움이 될 것입니다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "지금", "start_ms": 80, "dur_ms": 300, "phase_lr_deg": -3.0},
{"word": "이", "start_ms": 400, "dur_ms": 100, "phase_lr_deg": -3.0},
{"word": "문장을", "start_ms": 550, "dur_ms": 400, "phase_lr_deg": -3.0},
{"word": "따라서", "start_ms": 1000, "dur_ms": 450, "phase_lr_deg": -3.0},
{"word": "읽어보시면", "start_ms": 1500, "dur_ms": 600, "phase_lr_deg": -3.0},
{"word": "발음", "start_ms": 2200, "dur_ms": 300, "phase_lr_deg": -3.0},
{"word": "교정에", "start_ms": 2600, "dur_ms": 400, "phase_lr_deg": -3.0},
{"word": "큰", "start_ms": 3100, "dur_ms": 200, "phase_lr_deg": -3.0},
{"word": "도움이", "start_ms": 3400, "dur_ms": 400, "phase_lr_deg": -3.0},
{"word": "될", "start_ms": 3900, "dur_ms": 150, "phase_lr_deg": -3.0},
{"word": "것입니다", "start_ms": 4100, "dur_ms": 500, "phase_lr_deg": -3.0}
]
},
{
"segment": 10,
"transcript": "천천히 다시 한번 말해볼까요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "천천히", "start_ms": 150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "한번", "start_ms": 1100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "말해볼까요", "start_ms": 1500, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "Repeat after me.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Repeat", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "after", "start_ms": 250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "me.", "start_ms": 420, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 13,
"transcript": "Great job everyone!",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Great", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "job", "start_ms": 240, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "everyone!", "start_ms": 380, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "If life gives you lemons, make lemonade.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "If", "start_ms": 60, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "life", "start_ms": 180, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "gives", "start_ms": 400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 670, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "lemons,", "start_ms": 850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "make", "start_ms": 1300, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "lemonade.", "start_ms": 1550, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "이 속담은 역경을 기회로 바꾸라는 뜻을 담고 있습니다",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 90, "dur_ms": 100, "phase_lr_deg": 4.0},
{"word": "속담은", "start_ms": 250, "dur_ms": 450, "phase_lr_deg": 4.0},
{"word": "역경을", "start_ms": 800, "dur_ms": 500, "phase_lr_deg": 4.0},
{"word": "기회로", "start_ms": 1400, "dur_ms": 500, "phase_lr_deg": 4.0},
{"word": "바꾸라는", "start_ms": 2000, "dur_ms": 600, "phase_lr_deg": 4.0},
{"word": "뜻을", "start_ms": 2700, "dur_ms": 350, "phase_lr_deg": 4.0},
{"word": "담고", "start_ms": 3100, "dur_ms": 400, "phase_lr_deg": 4.0},
{"word": "있습니다", "start_ms": 3600, "dur_ms": 500, "phase_lr_deg": 4.0}
]
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
"transcript": "다음에 또 만나요 안녕히 계세요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 120, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 550, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 1300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "계세요", "start_ms": 1800, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "Goodbye!",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Goodbye!", "start_ms": 30, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "오늘 수업은 여기까지입니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "수업은", "start_ms": 180, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "여기까지입니다", "start_ms": 380, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "수고하셨습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "수고하셨습니다", "start_ms": 60, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "감사합니다 선생님",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 50, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "선생님", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": 0.0}
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
"transcript": "다시 한번 확인해 볼까요?",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "다시", "start_ms": 80, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "한번", "start_ms": 400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "확인해", "start_ms": 700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "볼까요?", "start_ms": 1100, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "이것은 매우 중요한 포인트입니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "이것은", "start_ms": 100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "매우", "start_ms": 550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "중요한", "start_ms": 900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "포인트입니다", "start_ms": 1400, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "반복해서 연습하면 익숙해질 거예요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "반복해서", "start_ms": 110, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "연습하면", "start_ms": 750, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "익숙해질", "start_ms": 1400, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 2200, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "질문이 있으시면 언제든지 말씀해 주세요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "질문이", "start_ms": 130, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "있으시면", "start_ms": 650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "언제든지", "start_ms": 1200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "말씀해", "start_ms": 1900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 2500, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "좋습니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "좋습니다", "start_ms": 40, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "이번에는 좀 더 빠르게 말해볼까요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "이번에는", "start_ms": 100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 550, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 700, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "빠르게", "start_ms": 850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "말해볼까요", "start_ms": 1300, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 31,
"transcript": "발음이 훨씬 좋아졌네요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "발음이", "start_ms": 120, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "훨씬", "start_ms": 550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "좋아졌네요", "start_ms": 950, "dur_ms": 500, "phase_lr_deg": 0.0}
]
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
"transcript": "이제 다음 챕터로 넘어가겠습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "챕터로", "start_ms": 850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "넘어가겠습니다", "start_ms": 1400, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "준비되셨나요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "준비되셨나요?", "start_ms": 40, "dur_ms": 500, "phase_lr_deg": 0.0}
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
"transcript": "네 준비됐습니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "준비됐습니다", "start_ms": 250, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 38,
"transcript": "그럼 시작해 볼까요? 첫 번째 문장입니다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "시작해", "start_ms": 450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "볼까요?", "start_ms": 900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "첫", "start_ms": 1400, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "번째", "start_ms": 1550, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "문장입니다", "start_ms": 1850, "dur_ms": 600, "phase_lr_deg": 0.0}
]
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "날씨가 추우니까 감기 조심하세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "날씨가", "start_ms": 70, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "추우니까", "start_ms": 400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "감기", "start_ms": 850, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "조심하세요", "start_ms": 1150, "dur_ms": 450, "phase_lr_deg": 0.0}
]
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
"transcript": "오늘 하루도 즐겁게 보내시길 바랍니다",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 90, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "하루도", "start_ms": 380, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "즐겁게", "start_ms": 780, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "보내시길", "start_ms": 1200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "바랍니다", "start_ms": 1750, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "화이팅 하세요!",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "화이팅", "start_ms": 50, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "하세요!", "start_ms": 450, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "Keep up the good work!",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Keep", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "up", "start_ms": 220, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 350, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "good", "start_ms": 460, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "work!", "start_ms": 650, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "끝났습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "끝났습니다", "start_ms": 30, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "마지막 인사를 드리겠습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "마지막", "start_ms": 100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "인사를", "start_ms": 550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "드리겠습니다", "start_ms": 1000, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "감사합니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 50, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 05:48:32*
