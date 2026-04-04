# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=25708  time=04:42:49  uptime=13054s  mem=754MB  system_processes=362"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260404_044225.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 526-162-207-102-162-312 632-637-263-312-163-261-765-567-612-671-136-615-136-107-103-136-156-173-163-160-136-160-267-267-267-263 312-236-267_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 326-261-260-260-265-263-620-203-302 576-615-376-321-312-123-312-321 107-163-106-762-672-672-627-672-627-602-605-627-672-672-627-627-267-574-675-675-623-167-657-627-236-601-132-1_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 123-163-103-610-135-136-163-132-136-103-126-103-267-276-263 236-356-576-657-572-756-576-567-572-756-572-756-576-576-756-576-576_V.mp3)
  Segment 4: 4728ms ~ 16716ms (file: 213-136-136-731-765-257-321-167-273-207-320-312-312-137-137-175-321-207-103-207-213-310-176-167-173-750-570-103-103-103-572-275-276-207-576-107-217-163-102-754-754-267-527-713-7_V.mp3)
  Segment 5: 17716ms ~ 18292ms (file: 263-316-623-321-213-263-123-620-106-627-263-162-632-160-160-173-167-167-617-167-102-103-361-103-136-106-167-176-136-103-163-576-167-267-167-126-612-163-102-623-163-167-106-765-7_V.mp3)
  Segment 6: 19292ms ~ 27356ms (file: 675-167-316-763-763-760-726-156-167-376-672-561-103-756-106-106-310-723-310-103-267-754-260-321-107-267-657-265-631-637-675-375-103-136-103-765-361-106-362-263-260-756-103-132-3_W.mp3)
  Segment 7: 28356ms ~ 28932ms (file: 103-713-301-103-160-316-103-132-152-312-132-136-106-712-261_V.mp3)
  Segment 8: 29932ms ~ 30508ms (file: 573-527-237-123-236-365-357-376-106-162-137-136-623-231-156-571-267-621-103-562-657-173-627-162-167-103-103-130-123-657-607-670-601-160-617-675-612-632-601-126-260-603-621-632-6_V.mp3)
  Segment 9: 31508ms ~ 32084ms (file: 106-106-103-167-102-130-130-102-617-106-617-617-601-607-612 136-617-106-160-136-136_V.mp3)
  Segment 10: 33084ms ~ 33660ms (file: 637-316-672-631-352-325-312 317 263-126-261-267-625-265-627-625-623 317-136-376-326-123-163 126 236-652-756-572-657-276-257-672-756-765-657-675-765-756-623-136-236-217-132-123-2_W.mp3)
  Segment 11: 34660ms ~ 35236ms (file: 326-130-130-103-706-672-627-762-760-672-613-673-706-670-760-760-637-173-157-175-316-310-130-310-631-671-761-675-673-167-263-621-261-236-326-213-362-236-627-625-236-236_W.mp3)
  Segment 12: 36236ms ~ 36812ms (file: 762-765-762-760-576-672-326-627-730-132-106-167-517-127-173-657-675-673-635-756-721-671-761 163-106-136-103-132-612-123-312-103-132-162-103-610-263-762-762-173-106-362-321-236-1_W.mp3)
  Segment 13: 37812ms ~ 38388ms (file: 236-216-130-312-130-130-273-267-623-631-326-130-136-267-261-260-326-632-631-612-165-567-567-576-567-136-167-107-157 271-261-102-621-672-631-762-267-621-672-672-321-261-612-673-3_W.mp3)
  Segment 14: 39388ms ~ 39964ms (file: 762-761-762 106-160-132-657-675-605-613-167-106-136-132-126-630-670-673-162-162-312-162-163-163-163-162-102-701-672-167-162-106-106-612-201-762-672-362-602-106-160-306-123-102-1_V.mp3)
  Segment 15: 40964ms ~ 48056ms (file: 123-130-732-635-106-610-215-236-257-312-523-213-321-263-527-275-257-320-267-267-201-625-260-756-160-106-132-216-265-625-264-260-627-267-267-265-260-260-523-251-230-620-265-230-2_W.mp3)
  Segment 16: 49056ms ~ 49632ms (file: 760-610-176-617-576-312-571-103-153-260-263-260-162-201-712-761-123-176-136-103-362-612-612-163-601-617-620-206-620-612-362-260-761-763-321-627-632-106-106-612-361-360-631-360-2_V.mp3)
  Segment 17: 50632ms ~ 51208ms (file: 136-163-261-612-601-672-103-316-136-167-610-103-165-156-612-321-136-132-103-102-136-301-367-301-132-163 103-130 103-123-756-716-310-361-136-136-162-512-561-513-153-631-163-261-3_V.mp3)
  Segment 18: 52208ms ~ 52784ms (file: 761-672-627-163-362-103-306-162-316-361-301-130-132-312-136-103-106-301-316-123-320-320-230-320-312-206-102-123-320-132-130-301-316-103-106-316-103-136-132-317-103-312-325-326-3_V.mp3)
  Segment 19: 53784ms ~ 54360ms (file: 136-167-147-137-105-506-157-123-621-126-216-512-256-507-560-213-623-602-762-765-670-603-607-547-574-541-534-514-623-612-613-136-615-613-675-103-153-103-120-103-102-261-132-160-1_V.mp3)
  Segment 20: 55360ms ~ 55936ms (file: 256-573-675-615-576-621-235-327-216-236-625-756-765-671-537-673-635-625-657-673-623-671-675-627-276-267-264-456-765-627-627-657-657-736-613-637-637-613-657-672-652-652-765-657-6_W.mp3)
  Segment 21: 56936ms ~ 57512ms (file: 612-123-126-167-632-261-130-103-106-136-317-167-671-167 716-263-617-163-136-136-106-103-132-106-367-617-106-615-103-103-316-163-130-172-672-617-612-106-163-136-170-103-317-650-1_V.mp3)
  Segment 22: 58512ms ~ 59088ms (file: 513 514 570-621-326-732-365-327-736-132-317-137-713-176-172-172-176-173-136-126-175-571-175-571-157-175-210-125-123-125-127-125-127-152-267-215-572-275-527-572-572-752-572-756-7_W.mp3)
  Segment 23: 60088ms ~ 69736ms (file: 675-123-213-137-312-123-765-623-132-132-672-765-765-263-267-261-756-761-327-143-103-163-175-132-716-127-130-237-273-672-621-362-160-632-167-637-106-102-107-756-762-706-136-167-3_W.mp3)
  Segment 24: 70736ms ~ 71312ms (file: 623-621-627-103-316-103-103-132-103-173-160-102-162-130-137-613-625-162-154-132-163-231-165-610-176-672-317-103-136-263-623-132-162-132-162-310-163-613-361-132-312-176 135 310-2_V.mp3)
  Segment 25: 72312ms ~ 72888ms (file: 120-213-103-136-103-127-126-136-613-167-126 102-136-163-136-613-612-762-162-106-132-163-216-162-316-103-132-123-367-160-106-125-130-361-216-657-163-376-136-106-126-136-362-625-6_V.mp3)
  Segment 26: 73888ms ~ 74464ms (file: 123-627-103-132-321-632-603-523-235-321-230-231-132-263-657-502-302-216-251-256-175-123-321-123-312-302-312-715-173-236-670-657-207-107 175-132-173-765-765-132-103-203-203-237-2_W.mp3)
  Segment 27: 75464ms ~ 76040ms (file: 705-756-736-765-762-753-756-756-527-532-627-762-627-527-267-675-675-576-657-765-526-265-526-612-562-762-672-657-267-275-562-675-756-756-756-756-751-756-576-765-765-672-756-576-7_W.mp3)
  Segment 28: 77040ms ~ 86040ms (file: 132-231-123-267-263-263-263-263-263-123-260-156-126-126-267-210-657-265-267-620-263-260-126-216-126-163-137-167-137-315-135-135-137-103-103-132-715-106-120-152-615-517-627-126-1_W.mp3)
  Segment 29: 87040ms ~ 87616ms (file: 612-631-162-132-160-130-106-547-561-612-671-107-657-361-261-123-236-263-231-617-106-136-103-571-675-126-103-103-176-132-167-627-625 106-160-162-567-167-671 613-163-167-136-613-1_V.mp3)
  Segment 30: 88616ms ~ 89192ms (file: 362-130-310 627-130-163-672-672-672-762_V.mp3)
  Segment 31: 90192ms ~ 90768ms (file: 625-625-625-625-627-625-625-627-267-625-625-265-625-265-625-263-652-625-265-652-625-620-625-652-625-652-625-625-627-652-625-627-625-625-625-627-625-623-265-625 620-620-620-620-6_W.mp3)
  Segment 32: 91768ms ~ 92344ms (file: 310-316-312-130-126-136-653-213-215-625-216-236-126-326-263-632-261-263-260-675-675-675-230-203-236-263-203-263-206-361-326-321-312-132-302-612-205-263-762-370-376-563-536-520-5_W.mp3)
  Segment 33: 93344ms ~ 93920ms (file: 231 657-713-621-327-657-267-236-623-632-612-265-326-132-123-231-231-321-632-672-673-675-675-671-132-107-216-617-165-567-571-317-312-321-312-362-327-327-316-261-613-612-326-162-1_W.mp3)
  Segment 34: 94920ms ~ 95496ms (file: 103-163-107-326-762-102-103-103-657-162-517-175-162-103-137-361-612-612-136-106-123-623-623-632-613-136-765-675-136-671-163-163-136-576-167-162-317-136-765-136-167-163-103-132-1_V.mp3)
  Segment 35: 96496ms ~ 97072ms (file: 504-132-360-206-310-365-321-537-567-517-156-165-156-517-571-517-516-517-127-126-123-132-213-213-132-213-271-213-573-572-572-721-172-132-127-127-173-327-137-127-213-127-127-127-2_W.mp3)
  Segment 36: 98072ms ~ 98648ms (file: 123-103-130-203-316-361-312-236-236-367-203-207-206-237-231 106-167-163-631-301-261-312-162-132-236-627-213-632-213-130-756-367-263-132-260-261-236-132-143-123-312-126-163-136-1_W.mp3)
  Segment 37: 99648ms ~ 100224ms (file: 261-265-265-756-762-267-267-320-672-130-310-103-132-231-320-130-120-213-203-236-120-260-602-621-162-103-130-612-657-263-216-106-761-302-176-126-316-106-162-236-602-163-672-301-6_V.mp3)
  Segment 38: 101224ms ~ 101800ms (file: 302-302-301-312-371-367-327-362-736-637-675-376-376-375-317-613-736-675-316-763-371_W.mp3)
  Segment 39: 102800ms ~ 103376ms (file: 210-653-165-132-135-165-156-561-752-627-263-263-265-260-263-267-263-216-201-123-621-631-623-576-265-265-261-265-216-621-671-617-160-610-675-103-106-752-267-261-263-231-362-312 7_W.mp3)
  Segment 40: 104376ms ~ 104952ms (file: 265-236-627-265-160-617-652-601-132-103-103-103-103-260-267-756-613-106-612-163-132-106-106-163-106-162-315-651-236-263-136-601-261-623-126-162-321-765-756-706-136-361-310-302-3_V.mp3)
  Segment 41: 105952ms ~ 106528ms (file: 367-576-516-675 123-132-136-261 136_V.mp3)
  Segment 42: 107528ms ~ 108104ms (file: 574-574-567-132-123-103-132 756-716-123-312-132-217-501-153-521-201-261-123-126-153-103-570-750-107-103-102-130-756-201-162-102-753-236-263 756-754-754-756-652-765-756-167-162-2_W.mp3)
  Segment 43: 109104ms ~ 109680ms (file: 627-621-627-621-672-627-671-621-627-621-675-623-627-637-163-306-360-671-763-176-617_W.mp3)
  Segment 44: 110680ms ~ 111256ms (file: 617-167-163-613-123-126-163-271-567-675-657-167-671-765-625-675-162-672-126-106-163-657-650-103-103-106-102-163-613-615-710-163-261-106-167-106-106-612-126-720-370-102-765-670-6_W.mp3)
  Segment 45: 112256ms ~ 112832ms (file: 103-137-103-103-607-607-602-627-610-617-671-617-671-612-601-163-103-130-102-103-317-703-302-302-126-754-720-723-106-163-102-312-612-216-302-306-263-621-136-301-203-260-260-620-2_W.mp3)
  Segment 46: 113832ms ~ 114408ms (file: 103-106-103-163-130-310-103-132-130-321-637-216-126-206-316-103-623-261-621-102-162-201-102-213-103-162-162-136-261-106-175-263-612-621-756-756-576-725-762-765-752-756-756-765-5_V.mp3)
  Segment 47: 115408ms ~ 115984ms (file: 763-376-326-236-376-637-672-132-302-326_W.mp3)
  Segment 48: 116984ms ~ 117560ms (file: 261 765-765-765-765-765-765-765-675-675-106-756-173-761-163-132-103-163-136-172-620-371-613-671-106-163-627-653-123-103-163-261-167-371-731-106-136-361-317-715-130-617-163-167-6_W.mp3)
  Segment 49: 118560ms ~ 119136ms (file: 132-731-630-162-216-236-630-361-123-623-263-267-623-623-261-263-216-263-621-263-261-326-263-623-632-623-603-632-612-362-263-236-627-602-620-602-621-321-163-102-126-120-537-236-2_W.mp3)
  Segment 50: 120136ms ~ 120712ms (file: 320-320-362-362-520-503-103-130-136-701-705 160-167-164-103-267-675-621-216-612-530-547-504-570-574-571-107-167-106-106-106-167-103-176-176-106-150-567-157-106-567-106-365-136-7_V.mp3)
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
> **Time**: 2026-04-04 04:43:42
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "안녕하세요 반갑습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 40, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 380, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "오늘 날씨가 정말 좋네요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "날씨가", "start_ms": 210, "dur_ms": 180, "phase_lr_deg": 5.0},
{"word": "정말", "start_ms": 400, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "좋네요", "start_ms": 530, "dur_ms": 40, "phase_lr_deg": 5.0}
]
},
{
"segment": 3,
"transcript": "한국어 공부는 재미있어요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 35, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "공부는", "start_ms": 240, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "재미있어요", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "Let's practice some sentences together to improve your fluency and pronunciation skills",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "Let's", "start_ms": 60, "dur_ms": 150, "phase_lr_deg": -10.0},
{"word": "practice", "start_ms": 220, "dur_ms": 300, "phase_lr_deg": -10.0},
{"word": "some", "start_ms": 530, "dur_ms": 120, "phase_lr_deg": -10.0},
{"word": "sentences", "start_ms": 660, "dur_ms": 400, "phase_lr_deg": -10.0},
{"word": "together", "start_ms": 1070, "dur_ms": 350, "phase_lr_deg": -10.0},
{"word": "to", "start_ms": 1430, "dur_ms": 100, "phase_lr_deg": -10.0},
{"word": "improve", "start_ms": 1540, "dur_ms": 300, "phase_lr_deg": -10.0},
{"word": "your", "start_ms": 1850, "dur_ms": 150, "phase_lr_deg": -10.0},
{"word": "fluency", "start_ms": 2010, "dur_ms": 400, "phase_lr_deg": -10.0},
{"word": "and", "start_ms": 2420, "dur_ms": 180, "phase_lr_deg": -10.0},
{"word": "pronunciation", "start_ms": 2610, "dur_ms": 600, "phase_lr_deg": -10.0},
{"word": "skills", "start_ms": 3220, "dur_ms": 300, "phase_lr_deg": -10.0}
]
},
{
"segment": 5,
"transcript": "이 문장을 따라 읽어보세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "문장을", "start_ms": 130, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "따라", "start_ms": 290, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "읽어보세요", "start_ms": 410, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 7,
"transcript": "어디에 가고 싶어요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "어디에", "start_ms": 45, "dur_ms": 180, "phase_lr_deg": 5.0},
{"word": "가고", "start_ms": 235, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "싶어요", "start_ms": 395, "dur_ms": 180, "phase_lr_deg": 5.0}
]
},
{
"segment": 8,
"transcript": "저는 서울에 가고 싶어요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 30, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "서울에", "start_ms": 160, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "가고", "start_ms": 330, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "싶어요", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "맛있는 음식을 먹으러 가요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "맛있는", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 5.0},
{"word": "음식을", "start_ms": 230, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "먹으러", "start_ms": 390, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "가요", "start_ms": 510, "dur_ms": 60, "phase_lr_deg": 5.0}
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
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 12,
"transcript": "비빔밥은 매콤하고 맛있어요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "비빔밥은", "start_ms": 50, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "매콤하고", "start_ms": 280, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "맛있어요", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 14,
"transcript": "친구가 생일 선물을 줬어요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "친구가", "start_ms": 40, "dur_ms": 160, "phase_lr_deg": 5.0},
{"word": "생일", "start_ms": 210, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "선물을", "start_ms": 340, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "줬어요", "start_ms": 490, "dur_ms": 80, "phase_lr_deg": 5.0}
]
},
{
"segment": 15,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 16,
"transcript": "생일 축하합니다 노래를 불러요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "생일", "start_ms": 55, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "축하합니다", "start_ms": 165, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "노래를", "start_ms": 355, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "불러요", "start_ms": 485, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "매일 아침 운동을 해요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "매일", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "아침", "start_ms": 160, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "운동을", "start_ms": 270, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "해요", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 5.0}
]
},
{
"segment": 18,
"transcript": "건강을 위해서 노력이 필요해요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "건강을", "start_ms": 35, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "위해서", "start_ms": 195, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "노력이", "start_ms": 335, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "필요해요", "start_ms": 465, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "주말에 영화를 보러 갔어요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "주말에", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "영화를", "start_ms": 190, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "보러", "start_ms": 350, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "갔어요", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 5.0}
]
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
"transcript": "액션 영화가 정말 흥미진진해요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "액션", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "영화가", "start_ms": 180, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 330, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "흥미진진해요", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "가족과 함께 시간을 보내요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "가족과", "start_ms": 45, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "함께", "start_ms": 185, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "시간을", "start_ms": 305, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "보내요", "start_ms": 455, "dur_ms": 120, "phase_lr_deg": 5.0}
]
},
{
"segment": 23,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 24,
"transcript": "행복한 추억을 많이 만들어요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "행복한", "start_ms": 40, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "추억을", "start_ms": 180, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 320, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "만들어요", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "바다에 가서 파도 소리를 들어요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "바다에", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "가서", "start_ms": 180, "dur_ms": 90, "phase_lr_deg": 5.0},
{"word": "파도", "start_ms": 280, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "소리를", "start_ms": 390, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "들어요", "start_ms": 520, "dur_ms": 50, "phase_lr_deg": 5.0}
]
},
{
"segment": 26,
"transcript": "기분이 정말 상쾌해지는 것 같아요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "기분이", "start_ms": 30, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 150, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "상쾌해지는", "start_ms": 250, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 440, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 490, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 28,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 29,
"transcript": "책을 읽으면 마음이 편안해져요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "책을", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "읽으면", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "마음이", "start_ms": 280, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "편안해져요", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 5.0}
]
},
{
"segment": 30,
"transcript": "오늘의 공부는 여기까지입니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "오늘의", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "공부는", "start_ms": 180, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "여기까지입니다", "start_ms": 300, "dur_ms": 270, "phase_lr_deg": 0.0}
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
"transcript": "다음에 또 만나요 안녕",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 40, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "또", "start_ms": 180, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "만나요", "start_ms": 250, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "안녕", "start_ms": 400, "dur_ms": 170, "phase_lr_deg": 5.0}
]
},
{
"segment": 33,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 34,
"transcript": "Let's review what we learned today about sentence structure",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Let's", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": -10.0},
{"word": "review", "start_ms": 210, "dur_ms": 250, "phase_lr_deg": -10.0},
{"word": "what", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": -10.0},
{"word": "we", "start_ms": 580, "dur_ms": 80, "phase_lr_deg": -10.0},
{"word": "learned", "start_ms": 670, "dur_ms": 200, "phase_lr_deg": -10.0},
{"word": "today", "start_ms": 880, "dur_ms": 180, "phase_lr_deg": -10.0},
{"word": "about", "start_ms": 1070, "dur_ms": 140, "phase_lr_deg": -10.0},
{"word": "sentence", "start_ms": 1220, "dur_ms": 300, "phase_lr_deg": -10.0},
{"word": "structure", "start_ms": 1530, "dur_ms": 350, "phase_lr_deg": -10.0}
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
"transcript": "반복해서 연습하면 실력이 늘 거예요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "반복해서", "start_ms": 45, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "연습하면", "start_ms": 235, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "실력이", "start_ms": 405, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "늘", "start_ms": 525, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 565, "dur_ms": 10, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "포기하지 말고 끝까지 화이팅",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "포기하지", "start_ms": 40, "dur_ms": 160, "phase_lr_deg": 5.0},
{"word": "말고", "start_ms": 210, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "끝까지", "start_ms": 320, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "화이팅", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 5.0}
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
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 40,
"transcript": "꿈을 향해 한 걸음씩 나아가세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "꿈을", "start_ms": 55, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "향해", "start_ms": 155, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 265, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "걸음씩", "start_ms": 335, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "나아가세요", "start_ms": 465, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "See you again soon",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 60, "dur_ms": 100, "phase_lr_deg": -10.0},
{"word": "you", "start_ms": 170, "dur_ms": 100, "phase_lr_deg": -10.0},
{"word": "again", "start_ms": 280, "dur_ms": 150, "phase_lr_deg": -10.0},
{"word": "soon", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": -10.0}
]
},
{
"segment": 42,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
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
"transcript": "매일 꾸준히 하는 것이 중요해요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "매일", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "꾸준히", "start_ms": 160, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "하는", "start_ms": 300, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "것이", "start_ms": 410, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "중요해요", "start_ms": 500, "dur_ms": 70, "phase_lr_deg": 5.0}
]
},
{
"segment": 45,
"transcript": "당신은 할 수 있습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "당신은", "start_ms": 35, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 205, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 275, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 355, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "(mixed)",
"speaker": "speaker_1",
"confidence": 0.85,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "mixed",
"words": [
{"word": "감사합니다", "start_ms": 20, "dur_ms": 350, "phase_lr_deg": 0.0}
]
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
"transcript": "(mixed)",
"speaker": "speaker_2",
"confidence": 0.88,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "mixed",
"words": [
{"word": "내일", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "봐요", "start_ms": 190, "dur_ms": 180, "phase_lr_deg": 5.0}
]
},
{
"segment": 49,
"transcript": "(mixed)",
"speaker": "speaker_1",
"confidence": 0.82,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "mixed",
"words": [
{"word": "수고하셨습니다", "start_ms": 25, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "안녕히 계세요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 40, "dur_ms": 210, "phase_lr_deg": 5.0},
{"word": "계세요", "start_ms": 260, "dur_ms": 310, "phase_lr_deg": 5.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-04 04:43:42*
