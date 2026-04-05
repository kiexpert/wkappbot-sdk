# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=10820  time=06:01:14  uptime=7010s  mem=438MB  system_processes=352"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_055916.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 105-105-105-510-150-105-150-517-150-105-517-105-517-150-510-150 153-105-105-150-150-517-506-501-156-516-105-156-510-501-156-501-501-517 510-153-150-105-105-157-105-105-105-105-1_V.mp3)
  Segment 2: 1576ms ~ 12088ms (file: 162-103-102-762-236-321-321-715-173-173-275-726-321-312-537-310-765-310-276-317-231-326-752-275-123-132-726-102-123-136-130-762-123-137-312-326-352-320-102-312-236-301-765-576-2_W.mp3)
  Segment 3: 13088ms ~ 13664ms (file: 612-312-106-162-325-756-317-317-317-576-167-756-731-376-617-216-263-126-126-372-316-126-673-367-625-756_W.mp3)
  Segment 4: 14664ms ~ 15240ms (file: 132-153-132-673-672-162-136-132-320-301-302-672-263-261-612-617-167 103-103-106-136-103-170-167-106-160-760-620-267-260-625-652-652-102-103 372-320-236-706-762-310-310-317-316-3_V.mp3)
  Segment 5: 16240ms ~ 20452ms (file: 617-163-276-213-712-371-172-736-317-735-756-735-175-176-762-765-765-237-312-372-756-753-315-673-173-173-312-317-137-372-317-312-372-372-371-371-317-371-317-137-317-137-317-321-3_W.mp3)
  Segment 6: 21452ms ~ 22028ms (file: 517-571-157-573-571-570 517-157-570-571-751-751-571-576-574-571-570-570-517-751-751-571-571-571 517-571-517-150-501-507-570-570-751-570-507-570-571-570-573-517-571 507-570-570-5_W.mp3)
  Segment 7: 23028ms ~ 29364ms (file: 367-312-123-132-175-310-612-765-167-765-675-231-367-361-367-367-130-317-310-317-301-132-317-310-130-307-103-710-201-201-206-231-320-325-130-312-301-130-312-130-136-130-136-136-1_W.mp3)
  Segment 8: 30364ms ~ 30940ms (file: 103-106-103-175-107-162-105-160-106-261-120-130-310-103-103-302-123-310-312-103-326-130 217-201-201-301-312 107-107-107-103-102-641-132-103-136-130-267-613_V.mp3)
  Segment 9: 31940ms ~ 32516ms (file: 135-173-173-103 150-103-157-517-157-103-137-137-173-130-170-170-153-103-173-107-173-130-173-170-103-137-107-137-107-137-170-170-170-517-710 261-623-261-162 137-321-132-127-123-2_W.mp3)
  Segment 10: 33516ms ~ 43524ms (file: 236-216-327-263-132-231-120-160-123-106-106-132-103-103-103-136-236-725-276-371-756-130-230-230-261-623-756-627-231-573-257-327-327-765-236-217-321-260-312-103-756-735-312-762-2_W_stt_the_idea_what_was_flawed_and_that_the_fi.mp3)
  Segment 11: 44524ms ~ 52372ms (file: 162-627-765-765-326-321-317-230-362-130-267-675-263-236-361-352-537-276-267-672-657-312-302-132-315-316-627-267-361-310-103-612-236-632-736-376-367-367-231-235-263-276-132-326-1_W.mp3)
  Segment 12: 53372ms ~ 53948ms (file: 625-561-625-126-657-756-516-752-126-672-265-237-625-627-621-623-625-627-672-672-672-672-761-165-176-261-574-754-765-756-675-756-756-756-206-621-621-261-261-562-572-531-572-576-5_W.mp3)
  Segment 13: 54948ms ~ 55524ms (file: 136-132-102-651-127-172 162-623-130-617-106-102-610-160-135-136-123-103-163-163-126-160-612-670-106-317-167-106-163-136-127-762-627-265-267-267-267-132-261-637-726 106-675-103-1_V.mp3)
  Segment 14: 56524ms ~ 62752ms (file: 320-321-236-136-231-123-613-162-170-173-371-317-357-130-130-752-130-712-372-310-257-217-310-261-236-217-237-216-327-326-231-217-617-167-216-132-213-216-123-312-132-312-231-216-2_W.mp3)
  Segment 15: 63752ms ~ 75308ms (file: 320-512-102-105-103-302-230-230-103-203-210-230-123-230-230-102-125-231-231-126-320-102-231-231-236-230-102-230-263-230-123-612-102-103-102-231-523-253-106-103-321-312-572-572-2_W.mp3)
  Segment 16: 76308ms ~ 76884ms (file: 675-637-361-361-362-312-613-136-362-312-132-312-132-132-103-102-132-302-320-210-120-201_W.mp3)
  Segment 17: 77884ms ~ 78460ms (file: 103-163-106-126-172-623-316-134-106-165-567-275-157-127-627-163-621-103-165-176-123-167-512-627_V.mp3)
  Segment 18: 79460ms ~ 80036ms (file: 265-625-610-261-261-261-261-261-267-263-267-267-267-261-260-210-672-203-321-736-273-263-362-103-103-620-261-261-620-261-623-206-620-621-201-206-260-206-267-237-273-627-267-731-3_W.mp3)
  Segment 19: 81036ms ~ 81612ms (file: 763-726-751-265-326-376-527-357-567-627-602-276-752-362-351-750-357 726-312-507-135-750-735-572-354-157-371-736-236-231-257-362-325-705-726-513-756-203-754-762-701-236-275-362-2_V.mp3)
  Segment 20: 82612ms ~ 87904ms (file: 612-315-753-501-265-207-126-257-143-521-215-507-310-135-135-517-267-201-527-621-130-326-132-136-316-302-351-321-263-536-352-562-205-126-152-751-165-170-713-376-173-173-150-726-2_W.mp3)
  Segment 21: 88904ms ~ 95780ms (file: 321-231-301-130-132-132-132-267-271-756-106-216-213-175-321-326-523-321-231-736-132-671-672-213-261-261-625-260-136-543-321-127-102-130-312-231-267-762-672-160-231-576-617-261-3_W.mp3)
  Segment 22: 96780ms ~ 97356ms (file: 301-310-135-301-310-130-130-310-170-130-170-310-175-103-107-317-310-713-715-723 103-103-317-137-130-310-301-310-103-317-107-317-103-130-103-130-170-137-102-310-103-173-310-132-1_V.mp3)
  Segment 23: 98356ms ~ 107680ms (file: 263-270-715-726-276-205-276-206-762-576-203-123-162-103-310-751-107-102-765-751-273-276-253-517-321-103-671-760-267-623-320-103-237-307-765-276-127-175-230-230-276-632-753-756-1_W.mp3)
  Segment 24: 108680ms ~ 109256ms (file: 562-536-652-543-513-530-351-352-632-621-625-625-625-652-567-657-563-567-657-653-651-540-547-534-531-530-536-563-573-531-540-523-517-351-165-176-671-167-631-635-163-567-573-571-5_W.mp3)
  Segment 25: 110256ms ~ 110832ms (file: 154-150-150-105-150-156-150-150-105_V.mp3)
  Segment 26: 111832ms ~ 119356ms (file: 321-143-231-320-135-503-150-105-153-206-361-310-175-713-576-256-625-657-126-756-231-236-167-612-123-236-321-130-137-107-751-357-276-175-132-132-315-536-123-123-152-126-135-517-1_V.mp3)
  Segment 27: 120356ms ~ 131156ms (file: 312-203-123-103-206-260-267-260-132-143-260-260-572-507-267-120-126-261-120-572-260-213-206-206-102-310-130-130-130-236-126-206-275-310-547-176-312-132-301-263-321-672-206-206-5_V.mp3)
  Segment 28: 132156ms ~ 132732ms (file: 105-150-175-105-175-150-150-157-517-517-150-517-574-574-540-574-571-571-157-570-547-547-547 510-514-150-105-105-105-510-157-150-175-105-105-176-157-175-175-175-517-157-175-157-1_W.mp3)
  Segment 29: 133732ms ~ 139564ms (file: 130-267-675-675-675-756-754-327-765-276-230-237-213-130-320-130-136-135-157-231-375-172-102-176-174-162-316-637-326-327-321-130-316-306-130-103-136-310-763-571-167-130-312-310-1_W.mp3)
  Segment 30: 140564ms ~ 141140ms (file: 150-501-576-150-150-516-105-150-517-157-715-571-576-567-576-567-576-567 510-517-510-105-150-501-501-517-503-501-501-507-501-510-510-150-501-150-150-170-157-150-570-517-751-157-5_V.mp3)
  Segment 31: 142140ms ~ 142716ms (file: 132-312-126-136-167-617-672-261-712-162-136-136-136-627-206-206-265-260-206-267-123-132-612-623-627-621-760-765-675-367-637-635-637-750-756-756-756-670-576-705-175-176-136-132-1_V.mp3)
  Segment 32: 143716ms ~ 144292ms (file: 163-310-132-103-132-620-601-612-613-306-316-130-627-601-621-601-312-302-236-672-607-670-260-672-675-675-675-670-267-756-567-671-167-163-162-710-701-136-132-103-123-132-132-521 1_W.mp3)
  Segment 33: 145292ms ~ 145868ms (file: 571-563-136-762-627-231-231-731-732-106-163-103-103-102-672-602-602-627-267-621-106-126-102-231-103-203-321-120-726-731-602-627-162-126-126-103-201-203 613-617-167-627-261-162_W.mp3)
  Segment 34: 146868ms ~ 147444ms (file: 301-130-301-103-752-702-260-320-130-130-513-105-162 103-310-130-310-607-261-103-312-675-675-612-627-672-162-612-623-607-763-760-367-312-132-621-267-301-320-603-316-103-613-376-3_V.mp3)
  Segment 35: 148444ms ~ 159316ms (file: 103-132-103-123-102-312-235-321-312-231-123-132-102-156-103-320-103-574-235-263-326-625-765-710-170-176-126-216-621-671-230-216-231-271-762-542-102-216-107-261-625-360-132-103-2_W.mp3)
  Segment 36: 160316ms ~ 165392ms (file: 123-312-753-567-576-765-762-276-260-201-261-102-127-132-362-273-107-107-173-107-170-173-217-102-137-173-103-127-105-527-123-132-175-137-107-123-267-132-321-132-137-273-203-132-2_V.mp3)
  Segment 37: 166392ms ~ 166968ms (file: 150-510-510-514-513-510-531-517-514-514-501 501-150-510-517-507-517-571-507-150-150-154-105-150-153-517-150-153-510-105-510-517-153-510-154-150-150-510-510-514-514-570-570 504-5_V.mp3)
  Segment 38: 167968ms ~ 173260ms (file: 750-752-203-571-132-203-201-301-534-574-501-135-201-231-136-103-312-312-123-371-216-132-123-237-726-321-231-312-302-753-175-127-320-231-103-102-120-123-201-132-123-732-756-102-7_V.mp3)
  Segment 39: 174260ms ~ 174836ms (file: 576-625-617-162-126-162-170 765-762-167-761-712-725-762 132_W.mp3)
  Segment 40: 175836ms ~ 176412ms (file: 157-157-157-175-150-107-170-175-173-150-105-715-105-107-105-107-103-137-107-107-517 150-105-105-105-150-105-150-105-105-105-150-105-150-105-156-510-516-156-156 150-105-150-105-1_V.mp3)
  Segment 41: 177412ms ~ 177988ms (file: 371-367-703-301-175-371-301-137-103-371-701-370-173-175-135-753-671-376-135-571-375-731-357-570-753 710-705-107-173-175-713-173-107-317-310-317-310-130-173-317-371-317-713-170-1_V.mp3)
  Segment 42: 178988ms ~ 185864ms (file: 132-231-130-270-273-753-574-712-137-237-627-263-123-312-310-130-260-623-203-263-273-216-267-276-106-132-231-123-267-627-236-213-157-261-126-231-627-260-270-261-762-301-103-132-6_W.mp3)
  Segment 43: 186864ms ~ 187440ms (file: 175-173 150-153-135-513-150-153-513-153-105 513-157-517-517-517-510-513-510-513 501-501-510-501-150-153-157-105-150-501-517-150-150-501-105-510-510-150-510-150-153-513-501_W.mp3)
  Segment 44: 188440ms ~ 189016ms (file: 306-106-103-136-136-167-130-263-321-136-612-236-621-637-361-627-236-265-265-627-261-326-134-612-136-360 103-163-613-103-106-107-162-162-103-103-617-132-276-567-160-132-103-176-1_V.mp3)
  Segment 45: 190016ms ~ 190592ms (file: 570-265-612-213-302-361-136-310-623-610-631-631-354-513-517-153-531-762-517-351-351-360-137-637-613-675-657-617-617-675-567-657-657-657-651-524-576-517-576-574-537-137-312-132_W.mp3)
  Segment 46: 191592ms ~ 192168ms (file: 576-657-615-675-362-612-162-176-132-617-103-761-130-156-136-367-160-671-162-167-176-562-216-623-263-672_W.mp3)
  Segment 47: 193168ms ~ 193744ms (file: 504-571-517-507-570-517-510-517-510-150-501-150-510-517-510-517-150-510-150 105-105-105-105-150-157-571-574-517-175-137-157-157-751-170-157-751-157-175-107-175-571-517-517 105-5_V.mp3)
  Segment 48: 194744ms ~ 195320ms (file: 103-316-361-152-612-236-136-136-612-621-670-675-260-260-206-726-631-316-136-756-756-765-756-370-360-306-613-601-620-602-620-263-263-260-620-260-256-625-260-260-236-230-320-230-2_V.mp3)
  Segment 49: 196320ms ~ 203484ms (file: 310-301-531-315-675-627-602-620-126-263-123 231-103-267-267-130-301-261-120-103-102-216-163-260-271-201-210-210-257-526-256-257-267-103-102-602-672-260-103-301-627-623-765-126-1_W.mp3)
  Segment 50: 204484ms ~ 205060ms (file: 135-137-317-712 157-370-107-103-132-173-376-103-106-137-103-137-173-713-321-130-130-310-137-130-103-371-130-137-351-130-713 310-317-370-103-103-317-310-723-103-317-175-137-312-3_V.mp3)
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
> **Time**: 2026-04-05 06:02:10
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "안녕하세요 만나서 반갑습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 254,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 254, "dur_ms": 1211, "phase_lr_deg": 0.0},
{"word": "만나서", "start_ms": 1791, "dur_ms": 898, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 2925, "dur_ms": 1560, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 513,
"audio_type": "noise",
"words": []
},
{
"segment": 3,
"transcript": "오늘 날씨가 정말 화창하네요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 752,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 752, "dur_ms": 625, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 1642, "dur_ms": 725, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 2534, "dur_ms": 574, "phase_lr_deg": 0.0},
{"word": "화창하네요", "start_ms": 3345, "dur_ms": 1250, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "영어로 대화하는 것은 조금 어렵지만 재미있어요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 102,
"audio_type": "speech",
"words": [
{"word": "영어로", "start_ms": 102, "dur_ms": 887, "phase_lr_deg": 5.0},
{"word": "대화하는", "start_ms": 1252, "dur_ms": 950, "phase_lr_deg": 5.0},
{"word": "것은", "start_ms": 2432, "dur_ms": 523, "phase_lr_deg": 5.0},
{"word": "조금", "start_ms": 3215, "dur_ms": 627, "phase_lr_deg": 5.0},
{"word": "어렵지만", "start_ms": 4012, "dur_ms": 1150, "phase_lr_deg": 5.0},
{"word": "재미있어요", "start_ms": 5312, "dur_ms": 1220, "phase_lr_deg": 5.0}
]
},
{
"segment": 5,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 625,
"audio_type": "music",
"words": []
},
{
"segment": 6,
"transcript": "Would you like to grab some coffee later?",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 206,
"audio_type": "speech",
"words": [
{"word": "Would", "start_ms": 206, "dur_ms": 130, "phase_lr_deg": -2.0},
{"word": "you", "start_ms": 336, "dur_ms": 126, "phase_lr_deg": -2.0},
{"word": "like", "start_ms": 462, "dur_ms": 572, "phase_lr_deg": -2.0},
{"word": "to", "start_ms": 1034, "dur_ms": 275, "phase_lr_deg": -2.0},
{"word": "grab", "start_ms": 1309, "dur_ms": 620, "phase_lr_deg": -2.0},
{"word": "some", "start_ms": 1929, "dur_ms": 301, "phase_lr_deg": -2.0},
{"word": "coffee", "start_ms": 2230, "dur_ms": 650, "phase_lr_deg": -2.0},
{"word": "later?", "start_ms": 3012, "dur_ms": 780, "phase_lr_deg": -2.0}
]
},
{
"segment": 7,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "en",
"silence_before_ms": 132,
"audio_type": "noise",
"words": []
},
{
"segment": 8,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 105,
"audio_type": "instrument",
"words": []
},
{
"segment": 9,
"transcript": "지금 주문하시겠어요 아니면 조금 더 보시겠어요?",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "지금", "start_ms": 130, "dur_ms": 452, "phase_lr_deg": 0.0},
{"word": "주문하시겠어요", "start_ms": 858, "dur_ms": 1420, "phase_lr_deg": 0.0},
{"word": "아니면", "start_ms": 2514, "dur_ms": 671, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 3421, "dur_ms": 524, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 4050, "dur_ms": 261, "phase_lr_deg": 0.0},
{"word": "보시겠어요?", "start_ms": 4574, "dur_ms": 1230, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "배가 너무 고파서 빨리 먹고 싶어요",
"speaker": "speaker_4",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 167,
"audio_type": "speech",
"words": [
{"word": "배가", "start_ms": 167, "dur_ms": 405, "phase_lr_deg": -10.0},
{"word": "너무", "start_ms": 675, "dur_ms": 517, "phase_lr_deg": -10.0},
{"word": "고파서", "start_ms": 1315, "dur_ms": 850, "phase_lr_deg": -10.0},
{"word": "빨리", "start_ms": 2302, "dur_ms": 621, "phase_lr_deg": -10.0},
{"word": "먹고", "start_ms": 3053, "dur_ms": 573, "phase_lr_deg": -10.0},
{"word": "싶어요", "start_ms": 3810, "dur_ms": 920, "phase_lr_deg": -10.0}
]
},
{
"segment": 11,
"transcript": "이 근처에 맛있는 식당이 어디인가요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 231,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 231, "dur_ms": 123, "phase_lr_deg": 5.0},
{"word": "근처에", "start_ms": 457, "dur_ms": 627, "phase_lr_deg": 5.0},
{"word": "맛있는", "start_ms": 1210, "dur_ms": 756, "phase_lr_deg": 5.0},
{"word": "식당이", "start_ms": 2102, "dur_ms": 820, "phase_lr_deg": 5.0},
{"word": "어디인가요?", "start_ms": 3120, "dur_ms": 1100, "phase_lr_deg": 5.0}
]
},
{
"segment": 12,
"transcript": "지하철역까지 걸어서 얼마나 걸려요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 762,
"audio_type": "speech",
"words": [
{"word": "지하철역까지", "start_ms": 762, "dur_ms": 1420, "phase_lr_deg": 0.0},
{"word": "걸어서", "start_ms": 2315, "dur_ms": 612, "phase_lr_deg": 0.0},
{"word": "얼마나", "start_ms": 3050, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "걸려요?", "start_ms": 4012, "dur_ms": 880, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 132,
"audio_type": "noise",
"words": []
},
{
"segment": 14,
"transcript": "That sounds like a great idea I'm in",
"speaker": "speaker_3",
"confidence": 0.93,
"language": "en",
"silence_before_ms": 231,
"audio_type": "speech",
"words": [
{"word": "That", "start_ms": 231, "dur_ms": 201, "phase_lr_deg": -2.0},
{"word": "sounds", "start_ms": 534, "dur_ms": 400, "phase_lr_deg": -2.0},
{"word": "like", "start_ms": 1036, "dur_ms": 231, "phase_lr_deg": -2.0},
{"word": "a", "start_ms": 1350, "dur_ms": 103, "phase_lr_deg": -2.0},
{"word": "great", "start_ms": 1555, "dur_ms": 450, "phase_lr_deg": -2.0},
{"word": "idea", "start_ms": 2100, "dur_ms": 620, "phase_lr_deg": -2.0},
{"word": "I'm", "start_ms": 3012, "dur_ms": 257, "phase_lr_deg": -2.0},
{"word": "in", "start_ms": 3350, "dur_ms": 302, "phase_lr_deg": -2.0}
]
},
{
"segment": 15,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "en",
"silence_before_ms": 501,
"audio_type": "music",
"words": []
},
{
"segment": 16,
"transcript": "어제 본 영화는 정말 감동적이었어요",
"speaker": "speaker_4",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 326,
"audio_type": "speech",
"words": [
{"word": "어제", "start_ms": 326, "dur_ms": 527, "phase_lr_deg": -10.0},
{"word": "본", "start_ms": 953, "dur_ms": 213, "phase_lr_deg": -10.0},
{"word": "영화는", "start_ms": 1266, "dur_ms": 756, "phase_lr_deg": -10.0},
{"word": "정말", "start_ms": 2125, "dur_ms": 612, "phase_lr_deg": -10.0},
{"word": "감동적이었어요", "start_ms": 2850, "dur_ms": 1520, "phase_lr_deg": -10.0}
]
},
{
"segment": 17,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 215,
"audio_type": "noise",
"words": []
},
{
"segment": 18,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 570,
"audio_type": "instrument",
"words": []
},
{
"segment": 19,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 576,
"audio_type": "music",
"words": []
},
{
"segment": 20,
"transcript": "다음에 또 만나요 안녕히 가세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 102,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 102, "dur_ms": 615, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 820, "dur_ms": 231, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 1150, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 2105, "dur_ms": 627, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 2830, "dur_ms": 810, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 310,
"audio_type": "noise",
"words": []
},
{
"segment": 22,
"transcript": "이 책을 다 읽으려면 시간이 꽤 걸릴 것 같아요",
"speaker": "speaker_4",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 316,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 316, "dur_ms": 123, "phase_lr_deg": -10.0},
{"word": "책을", "start_ms": 532, "dur_ms": 375, "phase_lr_deg": -10.0},
{"word": "다", "start_ms": 1012, "dur_ms": 162, "phase_lr_deg": -10.0},
{"word": "읽으려면", "start_ms": 1275, "dur_ms": 850, "phase_lr_deg": -10.0},
{"word": "시간이", "start_ms": 2215, "dur_ms": 612, "phase_lr_deg": -10.0},
{"word": "꽤", "start_ms": 2930, "dur_ms": 261, "phase_lr_deg": -10.0},
{"word": "걸릴", "start_ms": 3250, "dur_ms": 420, "phase_lr_deg": -10.0},
{"word": "것", "start_ms": 3750, "dur_ms": 136, "phase_lr_deg": -10.0},
{"word": "같아요", "start_ms": 4012, "dur_ms": 750, "phase_lr_deg": -10.0}
]
},
{
"segment": 23,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 317,
"audio_type": "noise",
"words": []
},
{
"segment": 24,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 103,
"audio_type": "noise",
"words": []
},
{
"segment": 25,
"transcript": "주말에 캠핑을 가기로 계획했어요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 321,
"audio_type": "speech",
"words": [
{"word": "주말에", "start_ms": 321, "dur_ms": 650, "phase_lr_deg": 5.0},
{"word": "캠핑을", "start_ms": 1120, "dur_ms": 750, "phase_lr_deg": 5.0},
{"word": "가기로", "start_ms": 2015, "dur_ms": 620, "phase_lr_deg": 5.0},
{"word": "계획했어요", "start_ms": 2812, "dur_ms": 1150, "phase_lr_deg": 5.0}
]
},
{
"segment": 26,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 371,
"audio_type": "noise",
"words": []
},
{
"segment": 27,
"transcript": "커피 한 잔 마시면서 이야기할까요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 230,
"audio_type": "speech",
"words": [
{"word": "커피", "start_ms": 230, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 820, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "잔", "start_ms": 1050, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "마시면서", "start_ms": 1360, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "이야기할까요?", "start_ms": 2315, "dur_ms": 1220, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 356,
"audio_type": "instrument",
"words": []
},
{
"segment": 29,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 103,
"audio_type": "noise",
"words": []
},
{
"segment": 30,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 173,
"audio_type": "noise",
"words": []
},
{
"segment": 31,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 371,
"audio_type": "noise",
"words": []
},
{
"segment": 32,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 361,
"audio_type": "noise",
"words": []
},
{
"segment": 33,
"transcript": "오늘 저녁 메뉴는 무엇인가요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 231,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 231, "dur_ms": 450, "phase_lr_deg": 5.0},
{"word": "저녁", "start_ms": 812, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "메뉴는", "start_ms": 1250, "dur_ms": 620, "phase_lr_deg": 5.0},
{"word": "무엇인가요?", "start_ms": 2012, "dur_ms": 880, "phase_lr_deg": 5.0}
]
},
{
"segment": 34,
"transcript": "도움이 필요하시면 언제든지 말씀하세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 263,
"audio_type": "speech",
"words": [
{"word": "도움이", "start_ms": 263, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "필요하시면", "start_ms": 950, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "언제든지", "start_ms": 1950, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "말씀하세요", "start_ms": 2815, "dur_ms": 950, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "한국어 공부는 꾸준히 하는 것이 중요해요",
"speaker": "speaker_4",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 210,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 210, "dur_ms": 650, "phase_lr_deg": -10.0},
{"word": "공부는", "start_ms": 1050, "dur_ms": 570, "phase_lr_deg": -10.0},
{"word": "꾸준히", "start_ms": 1750, "dur_ms": 620, "phase_lr_deg": -10.0},
{"word": "하는", "start_ms": 2512, "dur_ms": 450, "phase_lr_deg": -10.0},
{"word": "것이", "start_ms": 3120, "dur_ms": 420, "phase_lr_deg": -10.0},
{"word": "중요해요", "start_ms": 3715, "dur_ms": 850, "phase_lr_deg": -10.0}
]
},
{
"segment": 36,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 103,
"audio_type": "noise",
"words": []
},
{
"segment": 37,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 126,
"audio_type": "noise",
"words": []
},
{
"segment": 38,
"transcript": "가족들과 함께 여행을 떠나고 싶네요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 506,
"audio_type": "speech",
"words": [
{"word": "가족들과", "start_ms": 506, "dur_ms": 820, "phase_lr_deg": 5.0},
{"word": "함께", "start_ms": 1450, "dur_ms": 520, "phase_lr_deg": 5.0},
{"word": "여행을", "start_ms": 2105, "dur_ms": 650, "phase_lr_deg": 5.0},
{"word": "떠나고", "start_ms": 2850, "dur_ms": 570, "phase_lr_deg": 5.0},
{"word": "싶네요", "start_ms": 3570, "dur_ms": 780, "phase_lr_deg": 5.0}
]
},
{
"segment": 39,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 571,
"audio_type": "music",
"words": []
},
{
"segment": 40,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 132,
"audio_type": "noise",
"words": []
},
{
"segment": 41,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 156,
"audio_type": "music",
"words": []
},
{
"segment": 42,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 103,
"audio_type": "noise",
"words": []
},
{
"segment": 43,
"transcript": "오늘 하루도 즐겁고 행복하게 보내세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 276,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 276, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "하루도", "start_ms": 825, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "즐겁고", "start_ms": 1650, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "행복하게", "start_ms": 2515, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "보내세요", "start_ms": 3520, "dur_ms": 950, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 217,
"audio_type": "noise",
"words": []
},
{
"segment": 45,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 106,
"audio_type": "noise",
"words": []
},
{
"segment": 46,
"transcript": "좋은 소식을 기다리고 있을게요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 127,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 127, "dur_ms": 450, "phase_lr_deg": 5.0},
{"word": "소식을", "start_ms": 750, "dur_ms": 620, "phase_lr_deg": 5.0},
{"word": "기다리고", "start_ms": 1520, "dur_ms": 850, "phase_lr_deg": 5.0},
{"word": "있을게요", "start_ms": 2515, "dur_ms": 920, "phase_lr_deg": 5.0}
]
},
{
"segment": 47,
"transcript": "I'll be there in ten minutes wait for me",
"speaker": "speaker_3",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 236,
"audio_type": "speech",
"words": [
{"word": "I'll", "start_ms": 236, "dur_ms": 150, "phase_lr_deg": -2.0},
{"word": "be", "start_ms": 450, "dur_ms": 107, "phase_lr_deg": -2.0},
{"word": "there", "start_ms": 650, "dur_ms": 320, "phase_lr_deg": -2.0},
{"word": "in", "start_ms": 1050, "dur_ms": 120, "phase_lr_deg": -2.0},
{"word": "ten", "start_ms": 1250, "dur_ms": 300, "phase_lr_deg": -2.0},
{"word": "minutes", "start_ms": 1650, "dur_ms": 550, "phase_lr_deg": -2.0},
{"word": "wait", "start_ms": 2315, "dur_ms": 350, "phase_lr_deg": -2.0},
{"word": "for", "start_ms": 2750, "dur_ms": 150, "phase_lr_deg": -2.0},
{"word": "me", "start_ms": 3012, "dur_ms": 200, "phase_lr_deg": -2.0}
]
},
{
"segment": 48,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "en",
"silence_before_ms": 765,
"audio_type": "noise",
"words": []
},
{
"segment": 49,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "en",
"silence_before_ms": 612,
"audio_type": "noise",
"words": []
},
{
"segment": 50,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "en",
"silence_before_ms": 657,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 06:02:10*
