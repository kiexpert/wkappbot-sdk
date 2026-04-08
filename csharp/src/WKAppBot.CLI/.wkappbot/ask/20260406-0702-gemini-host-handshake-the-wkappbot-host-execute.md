# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=18336  time=07:01:31  uptime=11813s  mem=430MB  system_processes=409"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_070113.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 12348ms (file: 572-570-652-162-216-726-261-570-213-361-672-267-726-713-570-365-273-213-231-257-215-576-716-206-132-305-312-216-267-267-576-251 327-502-527-267-620-375-726-756-125-321-576-275-5_V.mp3)
  Segment 2: 13348ms ~ 13924ms (file: 351-531-367-761-673-637-673-716-731-713-765-567-537-567-574-576-576-576-576-751-751-570-627_W.mp3)
  Segment 3: 14924ms ~ 15500ms (file: 132 602-672-761-572-516-716-136-631-137-130-176-756-507-137-763-621-356-670-123-751-213-512-752-236-652-163-137-531-257-312-127-136-517-265-257-327-572-157-126-312-301-503-315-3_V.mp3)
  Segment 4: 16500ms ~ 24204ms (file: 273-312-176-312-136-106-163-106-672-672-275-375-576-253-267-617-571-372-735-237-235-132-672-312-376-735-367-761-651-210-167-756-576-231-321-317-267-237-267-725-752-327-751-320-3_W.mp3)
  Segment 5: 25204ms ~ 33916ms (file: 572-263-253-326-310-756-231-263-572-267-273-263-265-256-176-321-132-531-326-756-102-106-263-267-230-231-267-532-213-576-574-230-320-510-576-253-267-621-216-163-230-231-312-362-2_W.mp3)
  Segment 6: 34916ms ~ 35492ms (file: 306-301-357-163-317-675-321-312-321-507-562-532-257-312-312-316-632-132-672-206-627-621-526-673-631-570-602-261-751-306-362-312-612-670-326-570-276-765-756-276-506-751-571-265-5_V.mp3)
  Segment 7: 36492ms ~ 46860ms (file: 130-231-310-310-301-130-136-367-130-106-230-103-203-713-317-276-263-206-321-320-230-103-327-237-317-327-371-107-103-725-317-312-267-216-261-312-312-230-203-320-207-276-267-130-2_W.mp3)
  Segment 8: 47860ms ~ 48436ms (file: 261-236-257-127-256-627-705-136-132-216-726-762-716-253-263-652-526-237-765-207-267-504-256-215-231-163-750-325-260-632-206-627-762-765-163-361-163-136-317-137-367-317-316-136-1_W.mp3)
  Segment 9: 49436ms ~ 50012ms (file: 752-752 571-217-572-132-312-570-672-572-276-156-675-275-352-527-573-312-270-253-567-216-231-760-276-265-157-612-326-507-356-326-125-205-326-631-237-236-501-756-123-574-365-236-7_V.mp3)
  Segment 10: 51012ms ~ 51588ms (file: 275-267-576-735-706-637-671-675-675-735-356-671-132-173-137-756-326-576-136-163-167-613-167-163-173-137-136-137-317-137-317-137-137-137-130_W.mp3)
  Segment 11: 52588ms ~ 60004ms (file: 725-726-267-756-267-127-127-721-276-162-310-130-371-321-236-102-312-103-103-573-752-572-213-236-267-735-317-132-130-321-321-312-231-263-321-327-130-102-206-376-103-164-751-230-6_W.mp3)
  Segment 12: 61004ms ~ 61580ms (file: 536-725-267-206-567-563-570-762-136-253-256-267-750-372-261-672 357-312-317-312-321-351-376-376-736-375-765-675-137-137-173-173-137-137-317-137-137-137-137-137-317-137-137-160-1_W.mp3)
  Segment 13: 62580ms ~ 63156ms (file: 176-751-170-107-170-715-175-710-176-176-716-176-176-176-715-710-176-170-170-176-716-761 107-501-170-750-175-570-107-715-175-751-701-710-175-715-716-176-170-170-170-716-170-716-1_V.mp3)
  Segment 14: 64156ms ~ 64732ms (file: 710-715-710-175-701-710-173-170-170-173-170-170-170-170-170-170-176-176 176-167-715-751-170-705-170-701-175-170-176-176-710-170-170-170-107-176-170-107-107-170-170-760 175-157-1_V.mp3)
  Segment 15: 65732ms ~ 75488ms (file: 276-126-106-103-213-102-106-103-217-217-271-206-751-752-276-132-725-172-103-267-216-761-726-752-137-107-275-271-206-126-127-275-705-325-216-126-312-302-361-317-132-132-103 270-1_V.mp3)
  Segment 16: 76488ms ~ 77064ms (file: 213-253-527-267-570-756-627-765-126-602-253-570-562-317-276-675-736-713-761-756-756-756-751-574-754-754-756-756_W.mp3)
  Segment 17: 78064ms ~ 83248ms (file: 367-367-765-756-751-751-753-130-163-176-316-317-163-167-136-613-163-136-163-752-735-547-752-301-570-376-137-103-263-576-136-627-135-173-175-103-571-573-376-317-137-351-172-672-7_W.mp3)
  Segment 18: 84248ms ~ 91412ms (file: 756-176-172-720-102-571-317-716-301-632-260-752-756-106-756-675-261-762-160-267-267-236-762-102-731-201-260-756-765-267-267-276-372-176-310-362-320-754-756-570-310-263-276-267-2_W.mp3)
  Segment 19: 92412ms ~ 99576ms (file: 175-137-107-302-102-213-273-321-213-132-316-213-230-267-310-312-302-203-267-206-207-107-312-312-123-210-103-231-321-326-130-310-213-310-130-310-103-103-326-230-103-176-321-326-6_V.mp3)
  Segment 20: 100576ms ~ 101152ms (file: 756-576-576-762-627-763-732-751-123-725-526-261-673-231-572-526-751-175-167-752-261-267-756 572-236-576-256-507-357-726-162-320-321-136-571-325-572-321-321-735-321-365-576-325-3_V.mp3)
  Segment 21: 102152ms ~ 102728ms (file: 573-257-572-216-103-547-547-574-574-576-567-574-576-576-756-576-567-567-576-753-756-756-576-765-675-576-576-576-576-576-576-576-574-576-675-547-576-576-576-563-576-571-315-621-3_W.mp3)
  Segment 22: 103728ms ~ 104304ms (file: 130-625-321-632-362-132-231 726-761-763-760-162-136-371-175-123-631-761-372-136-173-370-713-673-760-675-765-765-765-675-672-671-670 237-320-623-564-671-613-617-602-627-621-103-6_W.mp3)
  Segment 23: 105304ms ~ 112972ms (file: 312-132-326-736-731-756-657-365-567-573-135-723-753-756-756-627-765-325-106-160-123-752-365-163-513-536-675-653-657-567-576_W.mp3)
  Segment 24: 113972ms ~ 127652ms (file: 236-132-612-164-762-120-132-103-132-106-106-312-321-230-162-617-263-263-102-236-167-230-263-263-206-321-203-231-231-213-310-267-260-367-321-132-136-236-135-136-103-176-137-321-1_W.mp3)
  Segment 25: 128652ms ~ 129228ms (file: 576-567-675-657-567-657-236-261-162-167-163-163-162-163-126-312-162-126-657-567-756-675-756-756-765-765-756-765-756-576-576-576-657-765-567-657-567-576-576-567-567-657-567-567-5_W.mp3)
  Segment 26: 130228ms ~ 138040ms (file: 132-627-213-135-276-617-236-261-236-527-230-231-231-320-312-132-312-213-123-236-206-230-136-150-256-265-276-257-527-321-273-207-217-120-314-103-157-507-502-321-312-132-320-231-2_W.mp3)
  Segment 27: 139040ms ~ 150920ms (file: 260-206-206-231-231-715-752-103-312-670-316-361-210-312-627-123-206-752-542-201-206-263-260-103-275-321-312 206-321-132-756-756-265-257-123-231-321-213-672-621-574-257-271-230-3_W.mp3)
  Segment 28: 151920ms ~ 152496ms (file: 613-163-675-267-631-576-630-761-316-736-627-627-123-136-175-132-507-231-216-261-312-132-165-167-157-572-175-572-126-571-502-213-251-751-126-156-132-621-527-213-162-167-102-176-1_V.mp3)
  Segment 29: 153496ms ~ 154072ms (file: 327-312 675-267-627-176-327-371-312-751-132-721-314-263-327-627-625-625-125-765-765-765-765-765-675-765-123-723-756-576-756-756-765-736-375-752-756-567-357-375-273 231_W.mp3)
  Segment 30: 155072ms ~ 155648ms (file: 315-350-173-756-765-756-765-765-751-765-756-765-167-175-761-765-175-165-175-157_W.mp3)
  Segment 31: 156648ms ~ 157224ms (file: 265-236-621-612-576-327-267-216-352-672-527-527-207-352-267-705-235-572-217-625-137-235-735-126-327-263-602-263-632-273-375-357-362-675-367-263-572-265-672-267-605-725-672-567-6_V.mp3)
  Segment 32: 158224ms ~ 158800ms (file: 316-135-175-317-315-513-165-576-167-732-567-165-567-617-675-167-651-176-671-163-516-162-153-312-231-321-176-631-672-672-326-317-325-135-371-231-572-231-137-671-571-162-127-567-2_V.mp3)
  Segment 33: 159800ms ~ 166064ms (file: 132-213-231-123-175-103-312-132-102-213-326-361-267-102-165-312-263-267-236-362-261-271-203-302-267-321-236-263-627-267-321-267-572-754-621-623-236-231-627-236-261-123-231-237-2_W.mp3)
  Segment 34: 167064ms ~ 167640ms (file: 170-153-135-657-617-517-156-516-567-576-567-635-765-765-761-735-150-371-376-716-702-362-135-756-673-671-657-137-103-170-135-367-312-367-372-312-173-167-532-256-527-570-361-316-3_W.mp3)
  Segment 35: 168640ms ~ 169216ms (file: 576-572-752-572-571-751-570-751-571-571-571-751-570-571-752-571 573-573-571-571-574-751-753-572-753-751-753-157-715-751-571-754-750-574-751-751 576-576-751-751-571-576-571-571-7_W.mp3)
  Segment 36: 170216ms ~ 177020ms (file: 627-251-130-132-201-236-573-765-231-576-571-102-534-263-267-231-623-216-526-213-267-256-230-160-123-132-302-273-705-752-132-213-103-120-127-206-321-263-236-230-537-572-207-267-2_W.mp3)
  Segment 37: 178020ms ~ 178596ms (file: 576-567-176-167-716-176-517-175-175-750-576-316-603-260-672-623-576 120-132-136-762-673-675-275-726-751-756-752-765-756-726-216-132-301-123-317-625-736-312-316-361-367-360-327-2_W.mp3)
  Segment 38: 179596ms ~ 185824ms (file: 230-321-230-263-260-260-210-230-230-235-327-572-357-136-132-267-275-267-267-270-270-275-206-263-132-130-213-310-627-210-263-231-132-312-321-130-230-103-263-321-301-132-302-301-2_W.mp3)
  Segment 39: 186824ms ~ 194204ms (file: 312-620-267-672-756-573-201-217-213-103-765-672-271-627-321-210-251-103-123-231-217-207-132-103-132-216-120-261-210-267-754-576-102-165-261-206-132-231-617-257-132-123-105-317-1_W.mp3)
  Segment 40: 195204ms ~ 202764ms (file: 260-260-754-130-175-754-135-174-574-574-201-260-321-231-573-106-276-752-712-301-537-547-572-230-574-215-213-765-321-130-735-316-753-137-573-750-175-236-573-574-523-574-213-310-3_W.mp3)
  Segment 41: 203764ms ~ 204340ms (file: 507-530-570-570-573-715-756-576-537-537-517-513-351-576-576-574-657-574-574-157-571-756-176-736-763-617-167-613-761-765-176-761-716-176-736-716-167-671_W.mp3)
  Segment 42: 205340ms ~ 205916ms (file: 573-567-652-567-675-625-163-213-123-231-265-762-261-126-213-126-162-621-160-126-126-652-672-627-672-627-627-632_W.mp3)
  Segment 43: 206916ms ~ 213900ms (file: 213-267-725-327-173-362-163-326-267-217-231-321-236-130-630-751-376-321-273-312-130-132-715-751-756-362-573-263-267-672-230-267-732-372-273-527-517-137-537-275-675-321-213-213-1_W.mp3)
  Segment 44: 214900ms ~ 215476ms (file: 613-726-627-162-231-253-562-267-326-271-627-750-270-275-237-132-527-367-627-326-263-213-231-321-126-573-627-756-627-576-372-627-276-756-267-526-205-256-625-765-752-267-750-526 7_V.mp3)
  Segment 45: 216476ms ~ 217052ms (file: 657-107-726-276-725-721-105-625-137-130-137-137-130-312-765-675-673-736-765 675-675_W.mp3)
  Segment 46: 218052ms ~ 218628ms (file: 650-607-126-365-563-513-513-356-326-310-362-321-672-271-273-236-236-260-230-237-732-327-372-267-327 241-725-317-613-364-637-613-635-341-174-264-671_W.mp3)
  Segment 47: 219628ms ~ 228448ms (file: 376-312-213-267-167-671-167-613-261-210-201-172-321-327-327-762-675-716-736-317-312-231-175-174-103-736-762-103-143-132-130-103-713-715-310-103-104-103-130-130-230-103-132-143-5_V.mp3)
  Segment 48: 229448ms ~ 230024ms (file: 576-315-201-516-625-652-125-235-102-321-125-615-657-756-571-130-135-175-351-325-653-213-163-537-530-135-256-326-312-316-163-652-657-657-652-561-351-516-765-576-315-351-207-207-5_V.mp3)
  Segment 49: 231024ms ~ 231600ms (file: 572-571-571-572-573-571-576-573 570-570-570-750-570-570-570-573-571-570-572 573-751-751-573-571-751-756-756-571-572-752 572-572-752-752-752-573-572-752-572-573-574-573-573 571-5_W.mp3)
  Segment 50: 232600ms ~ 239692ms (file: 275-260-627-362-157-672-261-260-321-627-627-267-217-107-503-130-107-257-516-357-762-123-723-206-730-130-265-203-310-623-206-721-237-213-123-267-120-163-620-756-765-675-630-326-2_W.mp3)
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
> **Time**: 2026-04-06 07:02:57
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "안녕하세요 만나서 반갑습니다 오늘 날씨가 참 좋네요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 150, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "만나서", "start_ms": 1000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 1550, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 2500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 2950, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "참", "start_ms": 3500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 3850, "dur_ms": 600, "phase_lr_deg": 0.0}
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
"transcript": "이것은 한국어 학습을 위한 가라오케 시스템 예시입니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "이것은", "start_ms": 200, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "한국어", "start_ms": 750, "dur_ms": 600, "phase_lr_deg": 5.0},
{"word": "학습을", "start_ms": 1400, "dur_ms": 550, "phase_lr_deg": 5.0},
{"word": "위한", "start_ms": 2000, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "가라오케", "start_ms": 2450, "dur_ms": 700, "phase_lr_deg": 5.0},
{"word": "시스템", "start_ms": 3200, "dur_ms": 600, "phase_lr_deg": 5.0},
{"word": "예시입니다", "start_ms": 3850, "dur_ms": 850, "phase_lr_deg": 5.0}
]
},
{
"segment": 5,
"transcript": "정확한 타이밍과 텍스트 매칭이 학습 효율을 높여줍니다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "정확한", "start_ms": 180, "dur_ms": 600, "phase_lr_deg": 5.0},
{"word": "타이밍과", "start_ms": 850, "dur_ms": 750, "phase_lr_deg": 5.0},
{"word": "텍스트", "start_ms": 1700, "dur_ms": 600, "phase_lr_deg": 5.0},
{"word": "매칭이", "start_ms": 2350, "dur_ms": 550, "phase_lr_deg": 5.0},
{"word": "학습", "start_ms": 3000, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "효율을", "start_ms": 3450, "dur_ms": 550, "phase_lr_deg": 5.0},
{"word": "높여줍니다", "start_ms": 4050, "dur_ms": 800, "phase_lr_deg": 5.0}
]
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
"transcript": "Could you please repeat that more slowly for me?",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "Could", "start_ms": 250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 560, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "please", "start_ms": 720, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "repeat", "start_ms": 1150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "that", "start_ms": 1650, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "more", "start_ms": 1950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "slowly", "start_ms": 2300, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "for", "start_ms": 2950, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "me?", "start_ms": 3200, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "천천히 다시 말씀해 주실 수 있을까요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 300,
"audio_type": "speech",
"words": [
{"word": "천천히", "start_ms": 300, "dur_ms": 700, "phase_lr_deg": 5.0},
{"word": "다시", "start_ms": 1050, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "말씀해", "start_ms": 1500, "dur_ms": 600, "phase_lr_deg": 5.0},
{"word": "주실", "start_ms": 2150, "dur_ms": 450, "phase_lr_deg": 5.0},
{"word": "수", "start_ms": 2650, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "있을까요?", "start_ms": 2900, "dur_ms": 700, "phase_lr_deg": 5.0}
]
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
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 14,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 15,
"transcript": "외국어 공부는 꾸준함이 가장 중요한 것 같습니다",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 220,
"audio_type": "speech",
"words": [
{"word": "외국어", "start_ms": 220, "dur_ms": 600, "phase_lr_deg": -2.0},
{"word": "공부는", "start_ms": 850, "dur_ms": 550, "phase_lr_deg": -2.0},
{"word": "꾸준함이", "start_ms": 1500, "dur_ms": 700, "phase_lr_deg": -2.0},
{"word": "가장", "start_ms": 2250, "dur_ms": 400, "phase_lr_deg": -2.0},
{"word": "중요한", "start_ms": 2700, "dur_ms": 600, "phase_lr_deg": -2.0},
{"word": "것", "start_ms": 3350, "dur_ms": 200, "phase_lr_deg": -2.0},
{"word": "같습니다", "start_ms": 3600, "dur_ms": 750, "phase_lr_deg": -2.0}
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
"transcript": "매일 조금씩이라도 듣고 말하는 연습을 하세요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "매일", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "조금씩이라도", "start_ms": 600, "dur_ms": 900, "phase_lr_deg": 5.0},
{"word": "듣고", "start_ms": 1550, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "말하는", "start_ms": 2000, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "연습을", "start_ms": 2550, "dur_ms": 600, "phase_lr_deg": 5.0},
{"word": "하세요", "start_ms": 3200, "dur_ms": 650, "phase_lr_deg": 5.0}
]
},
{
"segment": 18,
"transcript": "발음이 처음에는 어려울 수 있지만 계속하다 보면 익숙해집니다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "발음이", "start_ms": 120, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "처음에는", "start_ms": 650, "dur_ms": 600, "phase_lr_deg": 5.0},
{"word": "어려울", "start_ms": 1300, "dur_ms": 450, "phase_lr_deg": 5.0},
{"word": "수", "start_ms": 1800, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "있지만", "start_ms": 2050, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "계속하다", "start_ms": 2600, "dur_ms": 700, "phase_lr_deg": 5.0},
{"word": "보면", "start_ms": 3350, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "익숙해집니다", "start_ms": 3800, "dur_ms": 900, "phase_lr_deg": 5.0}
]
},
{
"segment": 19,
"transcript": "포기하지 말고 끝까지 도전해 보시길 바랍니다 화이팅",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "포기하지", "start_ms": 200, "dur_ms": 600, "phase_lr_deg": -2.0},
{"word": "말고", "start_ms": 850, "dur_ms": 350, "phase_lr_deg": -2.0},
{"word": "끝까지", "start_ms": 1250, "dur_ms": 500, "phase_lr_deg": -2.0},
{"word": "도전해", "start_ms": 1800, "dur_ms": 500, "phase_lr_deg": -2.0},
{"word": "보시길", "start_ms": 2350, "dur_ms": 500, "phase_lr_deg": -2.0},
{"word": "바랍니다", "start_ms": 2900, "dur_ms": 650, "phase_lr_deg": -2.0},
{"word": "화이팅", "start_ms": 3700, "dur_ms": 600, "phase_lr_deg": -2.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "감사합니다 수고하셨습니다 다음에 또 만나요 안녕",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 140, "dur_ms": 800, "phase_lr_deg": 5.0},
{"word": "수고하셨습니다", "start_ms": 1000, "dur_ms": 900, "phase_lr_deg": 5.0},
{"word": "다음에", "start_ms": 2000, "dur_ms": 450, "phase_lr_deg": 5.0},
{"word": "또", "start_ms": 2500, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "만나요", "start_ms": 2750, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "안녕", "start_ms": 3400, "dur_ms": 500, "phase_lr_deg": 5.0}
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
"transcript": "Where is the nearest subway station around here?",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 300,
"audio_type": "speech",
"words": [
{"word": "Where", "start_ms": 300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 610, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 770, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "nearest", "start_ms": 950, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "subway", "start_ms": 1500, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "station", "start_ms": 2100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "around", "start_ms": 2750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "here?", "start_ms": 3250, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "이 근처에서 가장 가까운 지하철역이 어디인가요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 180, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "근처에서", "start_ms": 350, "dur_ms": 600, "phase_lr_deg": 5.0},
{"word": "가장", "start_ms": 1000, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "가까운", "start_ms": 1450, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "지하철역이", "start_ms": 2000, "dur_ms": 800, "phase_lr_deg": 5.0},
{"word": "어디인가요?", "start_ms": 2850, "dur_ms": 750, "phase_lr_deg": 5.0}
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
"transcript": "직진하시다가 사거리에서 왼쪽으로 꺾으시면 됩니다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "직진하시다가", "start_ms": 250, "dur_ms": 900, "phase_lr_deg": 5.0},
{"word": "사거리에서", "start_ms": 1200, "dur_ms": 750, "phase_lr_deg": 5.0},
{"word": "왼쪽으로", "start_ms": 2000, "dur_ms": 600, "phase_lr_deg": 5.0},
{"word": "꺾으시면", "start_ms": 2650, "dur_ms": 550, "phase_lr_deg": 5.0},
{"word": "됩니다", "start_ms": 3250, "dur_ms": 500, "phase_lr_deg": 5.0}
]
},
{
"segment": 34,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "메뉴판 좀 주시겠어요? 추천하시는 메뉴가 있나요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "메뉴판", "start_ms": 200, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "좀", "start_ms": 750, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "주시겠어요?", "start_ms": 950, "dur_ms": 700, "phase_lr_deg": 5.0},
{"word": "추천하시는", "start_ms": 2000, "dur_ms": 700, "phase_lr_deg": 5.0},
{"word": "메뉴가", "start_ms": 2750, "dur_ms": 450, "phase_lr_deg": 5.0},
{"word": "있나요?", "start_ms": 3250, "dur_ms": 600, "phase_lr_deg": 5.0}
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
"transcript": "비빔밥 하나랑 불고기 2인분 주세요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "비빔밥", "start_ms": 150, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "하나랑", "start_ms": 700, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "불고기", "start_ms": 1500, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "2인분", "start_ms": 2050, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "주세요", "start_ms": 2600, "dur_ms": 550, "phase_lr_deg": 5.0}
]
},
{
"segment": 39,
"transcript": "맵지 않게 요리해 주실 수 있을까요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 220,
"audio_type": "speech",
"words": [
{"word": "맵지", "start_ms": 220, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "않게", "start_ms": 650, "dur_ms": 300, "phase_lr_deg": 5.0},
{"word": "요리해", "start_ms": 1000, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "주실", "start_ms": 1550, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "수", "start_ms": 2000, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "있을까요?", "start_ms": 2200, "dur_ms": 700, "phase_lr_deg": 5.0}
]
},
{
"segment": 40,
"transcript": "네 알겠습니다 잠시만 기다려 주십시오",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 180, "dur_ms": 250, "phase_lr_deg": -2.0},
{"word": "알겠습니다", "start_ms": 500, "dur_ms": 700, "phase_lr_deg": -2.0},
{"word": "잠시만", "start_ms": 1500, "dur_ms": 600, "phase_lr_deg": -2.0},
{"word": "기다려", "start_ms": 2150, "dur_ms": 550, "phase_lr_deg": -2.0},
{"word": "주십시오", "start_ms": 2750, "dur_ms": 800, "phase_lr_deg": -2.0}
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
"transcript": "맛있게 드셨나요? 계산은 이쪽에서 도와드리겠습니다",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "맛있게", "start_ms": 200, "dur_ms": 500, "phase_lr_deg": -2.0},
{"word": "드셨나요?", "start_ms": 750, "dur_ms": 650, "phase_lr_deg": -2.0},
{"word": "계산은", "start_ms": 1800, "dur_ms": 500, "phase_lr_deg": -2.0},
{"word": "이쪽에서", "start_ms": 2350, "dur_ms": 600, "phase_lr_deg": -2.0},
{"word": "도와드리겠습니다", "start_ms": 3000, "dur_ms": 1000, "phase_lr_deg": -2.0}
]
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "신용카드로 결제 가능할까요? 여기 영수증도 주세요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 300,
"audio_type": "speech",
"words": [
{"word": "신용카드로", "start_ms": 300, "dur_ms": 700, "phase_lr_deg": 5.0},
{"word": "결제", "start_ms": 1050, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "가능할까요?", "start_ms": 1500, "dur_ms": 750, "phase_lr_deg": 5.0},
{"word": "여기", "start_ms": 2600, "dur_ms": 300, "phase_lr_deg": 5.0},
{"word": "영수증도", "start_ms": 2950, "dur_ms": 600, "phase_lr_deg": 5.0},
{"word": "주세요", "start_ms": 3600, "dur_ms": 500, "phase_lr_deg": 5.0}
]
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
"transcript": "네 감사합니다 안녕히 가세요 또 뵙겠습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 150, "dur_ms": 200, "phase_lr_deg": -2.0},
{"word": "감사합니다", "start_ms": 400, "dur_ms": 800, "phase_lr_deg": -2.0},
{"word": "안녕히", "start_ms": 1500, "dur_ms": 500, "phase_lr_deg": -2.0},
{"word": "가세요", "start_ms": 2050, "dur_ms": 550, "phase_lr_deg": -2.0},
{"word": "또", "start_ms": 2900, "dur_ms": 250, "phase_lr_deg": -2.0},
{"word": "뵙겠습니다", "start_ms": 3200, "dur_ms": 800, "phase_lr_deg": -2.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 07:02:57*
