# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=09:22:55  uptime=29020s  mem=1779MB  system_processes=446"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_092245.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 132-124-315-164-367-125-164-127-162-134-367-126-237-173-136-132-316-312-132 713-641-176-213-126-123-136-312-214-213 162-675-652-162-326-153-164-136-127-372_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 327-302-136-725-236-321-602-632-631-162-123-162-617-125-123-361-132-301-136-163-162-123-123-167-210-237-310-123-137-123-173-137-301-316-172-123-602-123-132-310-316-317-752-273-3_V.mp3)
  Segment 3: 3152ms ~ 13160ms (file: 136-137-216-136-173-564-167-267-726-106-316-615-721-175-610-136-617-137-167-136-657-157-137-623-751-756-263-106-136-103-136-103-613-631-136-126-617-153-132-713-610-675-623-164-1_V.mp3)
  Segment 4: 14160ms ~ 18012ms (file: 120-670-571-526-265-326-623-632-623-623-623-620-263-623-567-756-160-136-312-321-362-362-312-263-261-120-321-231-263-321-132-312-312-123-123-123-210-213-621-132-103-103 352-632-6_W.mp3)
  Segment 5: 19012ms ~ 19588ms (file: 715-173 302-326-320-321-132-123-312-321-132-312-321-312-132-132 123-612 263-617-163-136-632-263-321-627-163 157-176-123-326 627-260-216-620-132-132-317-612-126-316 657-672-657-6_W.mp3)
  Segment 6: 20588ms ~ 21164ms (file: 715-750-175-573-517-571-157-157-517-571-137-175-735-375-715-753-276-257-725-572-576-576-576-516-157-715-756-574-571-574-571-754-715-715-751-765-765-327-362-125-127-237-265-326-6_W.mp3)
  Segment 7: 22164ms ~ 22740ms (file: 672-627-632 762-672-672 765-762_W.mp3)
  Segment 8: 23740ms ~ 24316ms (file: 320-123-613-103-123-631-163-126-316-361-316-273-276-267-627-576-507-576-567-157-517-372-326-312-260-620-362-136-163-302-350-305-312-321-312-132-316-316-612-163-612-301-106-123-6_W.mp3)
  Segment 9: 25316ms ~ 25892ms (file: 362-623-362-632-326-362-632-362-365-362-365-635-632-635-365-653-635-627-276-672-672-756-762-726-721-725-725-726-627-627-672-627-672-627-672-672-627-672-672-652-265-265-265-256-2_W.mp3)
  Segment 10: 26892ms ~ 27468ms (file: 103-163 163-132-236-261-725-706-326-631-620-132-136-312-106-103-126-132-206-623-756-705-275-316-326-236-230-163-163-163-267-263-123-136-163_V.mp3)
  Segment 11: 28468ms ~ 29044ms (file: 567-536-163-136-132-321-316-631-623-132-321-123-162-132 156-571-716-263-203-312-102-160-263-623-627-302-120-236-621-365-361-132-137-103-320-321-321-126-301-301-210-103-201-321-3_V.mp3)
  Segment 12: 30044ms ~ 30620ms (file: 751-156-612-627-621-627-261-261-132-670-672-670-760-312-263-621-623-612-261-267-621-612-261-103-136-302-301-326-362-261-623-621-317-136-132-163-136-132-631-632-362-106-617-136-1_V.mp3)
  Segment 13: 31620ms ~ 32196ms (file: 132-136-321-231-130-130-103-103-132-103-310-301-315-123-326-263-102-127-312-123-107-153-125-137-176-132-106-106-150-521-132-163-310-361-326-312-763-167-167-157-632-761-361-317-1_V.mp3)
  Segment 14: 33196ms ~ 33772ms (file: 265-265-265-256-265-526-524-512-540-527-256-275-267-152-523-352-153-231-523-537-537-537-574-572-325-352-357-534-543-325-532-537-532-350-352-325-265-265-265-256-265-267-275-572-5_W.mp3)
  Segment 15: 34772ms ~ 35348ms (file: 765-761-761-761-765-765 762-765-675 763_W.mp3)
  Segment 16: 36348ms ~ 36924ms (file: 321-132-312-231-210-326-316-326-321-123-316-126-325-160-316-126-326-206-261-362-302-263-236-326-230-260-231-230-210-230-205-236 567-207 132-132-103-561-136-613-163-316-176-103-1_W.mp3)
  Segment 17: 37924ms ~ 38500ms (file: 765-675-675 312-321 206-102-106-320-326-302-302 145-174-750-756-173-317-312-621-632-263-621-613-106-736-760-670-672-602-260-602-213-132-320-230-320-302-236-263-162-167-164-312-3_W.mp3)
  Segment 18: 39500ms ~ 40076ms (file: 136-623-213-321-123-236 627-627 621-126-126-263-236-567-312-321-263-260-261-231-326-320-216-261-162-612-623-326-236-231 631-675-671-316-326-312-621-267-672-627-672-162-632-312-6_W.mp3)
  Segment 19: 41076ms ~ 41652ms (file: 675-576-756-367-567-572-567-572-256-671-675-576-567-576-567-567-576-627-657-657-671-165-167-162-167-156-612-613-516-572-627-623-361 637 162-167-106-620-163-756-617-610-106-657-3_W.mp3)
  Segment 20: 42652ms ~ 43228ms (file: 752-576-361-153-321-571-103-517-752-120-157-107-675-106-615-612-163-160-574-167-137-103-135-163-162-162-172-576-617-512-103-617-132-621 160-167-163-162-170-306-172-317-132-107-1_V.mp3)
  Segment 21: 44228ms ~ 44804ms (file: 163-163-613-361-301-231-127-610-671-136-137-163-130-506-756-765-267-206-260-206-302-310-132-710-712-301-103-137-316-216-123-507-571-157-507-675-706-726-706-760-762-672-627-176-1_V.mp3)
  Segment 22: 45804ms ~ 46380ms (file: 765-765-761-765-761 765-762-765-765-760-765-675-763-765-762-762-574-574-756-754-756-765-765-765-761-765-673-326-765-731-671-617-617-167-163-162-176-617-632-126-617-617-731-765-7_W.mp3)
  Segment 23: 47380ms ~ 47956ms (file: 620-263-160-601-126-123-256-652-576-205-267-760-627-602-620-620-260-263-502-105-123-216-126-321-102-321-206-621-670-670-670-620-760-760-672-236-623-670-670-267-260-276-612-172-2_V.mp3)
  Segment 24: 48956ms ~ 49532ms (file: 765-675-765-765-765-765-765-765-765-675-675-675-675-670-361-362-176-376 301-132-130_W.mp3)
  Segment 25: 50532ms ~ 51108ms (file: 321-231-216-231-245-213-162-214-175_V.mp3)
  Segment 26: 52108ms ~ 52684ms (file: 567-657-675-675-657-657-675-672-627-675-627-267-267-627-627-627-675-765-673-632-657-573-576-563-657-576-365-653-567-571-576-567-567-576-536-567-256-265-625-562-567-257-526-526-5_W.mp3)
  Segment 27: 53684ms ~ 54260ms (file: 126-132-136-103-163-312-310-210-120-123-136-126-123-132-312-321-132-123-102-132_V.mp3)
  Segment 28: 55260ms ~ 55836ms (file: 326-132-312-312-325-537-573-507-325-573-507-132-302-570-132-132-312-302-132-237-672-521-256-123-326-321-312-132-321-321-310-320-326-275-235-201-312-132-256-675-213-267-312-305-2_V.mp3)
  Segment 29: 56836ms ~ 57412ms (file: 203-376-310-172-674-362-620-657-675-765-327-326-567-576-256-657-263-326 263-623-103-574-754-267-256-265-132-756-765-562-567-567-635-316-517-571-573-235-253-572-752-572-561-165-6_W.mp3)
  Segment 30: 58412ms ~ 58988ms (file: 675-657 567-356 310-136-132 362-316-621 706-627 760-761-763_W.mp3)
  Segment 31: 59988ms ~ 60564ms (file: 562-360-675-267-263-267-267-672-563-265-263-236-256-256-526-625-265-627-723-765-271-256-261-103-261-132-603-621-675-675-675-625-675-625-627-237-263-263-623-632-236-623-326-256-2_W.mp3)
  Segment 32: 61564ms ~ 62140ms (file: 123-123-607-620-203-236-260-765-762-706-670-203-236-213-213-203-502-265-276-726-276-672-326-321-375-320-230-320-530-534-310-136-301-312-762-526-250-206-302-231-301-302-237-260-5_V.mp3)
  Segment 33: 63140ms ~ 63716ms (file: 621-320-301-320-320-627-360-321-231-230-321-301-203-320-327-321-312-312-137-317-137-103-135-132-675-576-370-357-170-765-136-310-756-765-637-306-136-167-670-706-176-154-145-760-6_W.mp3)
  Segment 34: 64716ms ~ 65292ms (file: 162-261-263-263-236-236-236-237-176-137-361-236-234-327-261-267-623-623-623-613-167-671-671-610-167-317-362-623-263-632-623-623-621-263-621-326-263-265-263-321-267-267-265-526-5_W.mp3)
  Segment 35: 66292ms ~ 66868ms (file: 106-560-106-106-165-750-705-756-706-157-571-317-763-761-675-765-762-765-765-657-765-672-107-136-103-176-163-132-321-312-132-630-312-130-627-765-675-176-675-671-617-276-617-167-1_W.mp3)
  Segment 36: 67868ms ~ 68444ms (file: 562-362-132-126-163-316-106-316-136-136-163-106-136-315-612-160-136-610-136-162-136-136-610-263-632_V.mp3)
  Segment 37: 69444ms ~ 70020ms (file: 576-756-576-756-517-536-576-637-163-165-607-137-316-176-167-627-613-627-263-306-107-106-156-157-567-673-631-316-567-126-765-705-132-130-103-102-302-132-571 672-672-620-602-602-6_W.mp3)
  Segment 38: 71020ms ~ 71596ms (file: 672-236-632-673-672-723-627-216-257-265-536-567-265-267-276-627-726-672-627-627-670-670-627-672-670-675-567-526-256-632-621-637-267-762-267-627-276-267-312-325-537-256-267-276-2_W.mp3)
  Segment 39: 72596ms ~ 73172ms (file: 137-162-621-126-675-657-157-617-165-137-762-176-675-762-671 751-751 516 165-761-106-176-576-215-261-167-261-623-625-265-265 765-267-132-136-362-136-106-107-160-602-671-162-162-6_W.mp3)
  Segment 40: 74172ms ~ 74748ms (file: 167-765-510-325-325-326-302-302-321-302-321-260-206-132-301-312-132-301-132-301-617-106-613-106-507-756-175-607-765-607-576-576-163-216-351-570-263-216-675-673-130-103-167-106-6_V.mp3)
  Segment 41: 75748ms ~ 76324ms (file: 162-625-652-657-657-652-126-206-672-607-163-306-156-567-362-571-105-760-672-673-316-321-125-512-765-617-615-576-576-516-576-105-135-563-652-653-567-576-576-103-751-517-675-657-6_W.mp3)
  Segment 42: 77324ms ~ 77900ms (file: 621-716-651-716-671-716-761-176-567-617-761-673-162-567-765-761-675-761-173-167-176-176-167-715-762-763-671-165-153_W.mp3)
  Segment 43: 78900ms ~ 79476ms (file: 621-163-326-536-563-263-216-261-263-265-263-263-326-632-765-265-265-526-521-125-327-236-325-235-326-235-752-276-236-326-236-267-265-632-263-362-236-362-356-625-627-627-621-621-6_W.mp3)
  Segment 44: 80476ms ~ 81052ms (file: 621-621-632-621-657 175-172-751-751-615-715-756 673 621-621-620-672-671-671-675-675-672-613-574-105-152-152 673-514-574 246-123 756-756 163-716 621-136_W.mp3)
  Segment 45: 82052ms ~ 82628ms (file: 576-163-136-162-610-601-310-617-617-617 301-312-103-163-106-162-126-607-627-671-657-671-106-163-165-175-163-301-160-136-236-236-203-302-260-523-103-607-675-362-302-362-326-301-3_V.mp3)
  Segment 46: 83628ms ~ 84204ms (file: 162-203-207-201-123-213-102-316-130-163-132-123-126-123-213-210-216-267-126-123-106-136-167-107-716-671-210-126-316-132-312-136-162-103-106-106-301-306-302 675-103-302-132-167-1_V.mp3)
  Segment 47: 85204ms ~ 85780ms (file: 123-137-172-317-237-726-271-172-231-236-326-236-256-524-524-257-276-267-263-263-236-235-231-321-213-215-317-231-326-321-231-267-276-671-253-327-236-236-236-236-236-321-236-326-3_W.mp3)
  Segment 48: 86780ms ~ 90632ms (file: 316-167-305-175-621 103 102-103-126-102_V.mp3)
  Segment 49: 91632ms ~ 92208ms (file: 543-537-570-620-620-756-765-670-763-367-132-130-134-106-106-103-540-541-530-534-153-260-260-236-260 620-620-620-620-231-320-230-201-230-230-231-321-301-302-263-236-230-206-260-3_V.mp3)
  Segment 50: 93208ms ~ 93784ms (file: 674-756-765-236-327-726-627-572-375-376-372-726-367-547-315-361-136-315-365-316-317-362-213-236-326-253-267-267-361-361-237-236-265-263-236-123-326-213-216-123-132-312-321-321-1_W.mp3)
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
> **Time**: 2026-04-03 09:23:47
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "더럽다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "더럽다", "start_ms": 40, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "조용하다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "조용하다", "start_ms": 50, "dur_ms": 460, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "시끄럽다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "시끄럽다", "start_ms": 30, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "밝다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "밝다", "start_ms": 60, "dur_ms": 420, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "어둡다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "어둡다", "start_ms": 45, "dur_ms": 485, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "강하다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "강하다", "start_ms": 60, "dur_ms": 455, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "약하다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "약하다", "start_ms": 40, "dur_ms": 495, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "무겁다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "무겁다", "start_ms": 65, "dur_ms": 475, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "가볍다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "가볍다", "start_ms": 40, "dur_ms": 490, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "두껍다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "두껍다", "start_ms": 70, "dur_ms": 470, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "얇다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "얇다", "start_ms": 50, "dur_ms": 415, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "깊다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "깊다", "start_ms": 80, "dur_ms": 435, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "얕다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "얕다", "start_ms": 30, "dur_ms": 445, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "단단하다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "단단하다", "start_ms": 65, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "부드럽다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "부드럽다", "start_ms": 40, "dur_ms": 505, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "날카롭다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "날카롭다", "start_ms": 55, "dur_ms": 515, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "뭉툭하다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "뭉툭하다", "start_ms": 35, "dur_ms": 525, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "길다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "길다", "start_ms": 65, "dur_ms": 425, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "짧다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "짧다", "start_ms": 45, "dur_ms": 410, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "넓다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 75,
"audio_type": "speech",
"words": [
{"word": "넓다", "start_ms": 75, "dur_ms": 410, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "좁다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "좁다", "start_ms": 50, "dur_ms": 425, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "높다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "높다", "start_ms": 80, "dur_ms": 405, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "낮다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "낮다", "start_ms": 30, "dur_ms": 435, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "동그랗다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "동그랗다", "start_ms": 65, "dur_ms": 485, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "네모나다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네모나다", "start_ms": 40, "dur_ms": 515, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "세모나다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "세모나다", "start_ms": 55, "dur_ms": 505, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "빨갛다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "빨갛다", "start_ms": 35, "dur_ms": 495, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "파랗다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "파랗다", "start_ms": 70, "dur_ms": 505, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "노랗다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "노랗다", "start_ms": 40, "dur_ms": 515, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "초록색",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 75,
"audio_type": "speech",
"words": [
{"word": "초록색", "start_ms": 75, "dur_ms": 475, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "검정색",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "검정색", "start_ms": 50, "dur_ms": 485, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "하얀색",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "하얀색", "start_ms": 80, "dur_ms": 495, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "하나",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "하나", "start_ms": 30, "dur_ms": 385, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "둘",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "둘", "start_ms": 65, "dur_ms": 325, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "셋",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "셋", "start_ms": 40, "dur_ms": 335, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "넷",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "넷", "start_ms": 55, "dur_ms": 345, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "다섯",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "다섯", "start_ms": 35, "dur_ms": 415, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "여섯",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "여섯", "start_ms": 65, "dur_ms": 425, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "일곱",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "일곱", "start_ms": 40, "dur_ms": 435, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "여덟",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "여덟", "start_ms": 70, "dur_ms": 445, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "아홉",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "아홉", "start_ms": 55, "dur_ms": 425, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "열",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "열", "start_ms": 80, "dur_ms": 315, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "일",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "일", "start_ms": 30, "dur_ms": 285, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "이",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 60, "dur_ms": 295, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "삼",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "삼", "start_ms": 45, "dur_ms": 315, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "사",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "사", "start_ms": 55, "dur_ms": 325, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "오",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "오", "start_ms": 35, "dur_ms": 305, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "육",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "육", "start_ms": 70, "dur_ms": 345, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "칠",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "칠", "start_ms": 40, "dur_ms": 335, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "팔",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 75,
"audio_type": "speech",
"words": [
{"word": "팔", "start_ms": 75, "dur_ms": 325, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 09:23:47*
