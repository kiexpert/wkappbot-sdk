# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=09:48:41  uptime=30566s  mem=1914MB  system_processes=452"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_094831.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 267-162-163-765-762-672-675-671-675-673-765-765-765-367-276-362-621-162-762-762-765-761-675-756-761-765-726_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 572-576-576-567-576-576-567-567-576-576-576-567-572-576-567-567-576-576-567-657-576-567-576-567-572-567-756-321-312-263 267 657-672-671-362-256-256-625-235-263-756-726-762-652-3_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 671-670-617-673-671-657-617-637-670-136-216-136-567-756-237-176-602-571-123-617-751 765-657-675-752-675-653-567 167-167 123-132-312_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 671-607-760-576-756-756-765 241-321-132-123-123-102-362-136-263-162-130-132-130-120-103-130-216-126-132-621-612-132-126-612-753-752-765-762-126-761-726-726-672-276-612-361-126-1_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 671-127-756-362-326-362-675-612-167-756-765-756-762-726-671-123-123-312-230-201-163-163-167-620-236-510-312-321-102-527-612-123-160-105-570-561-615-650-132-632-567-637-263-623-2_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 623-362-103-136-756-571-236-213-574-574-756-756-756-752-762-276-263-267-236-231-312-276-267-132 236-623-231-623-263-267 102-120-162-751 316-267-123-736-130-236-621-312-103-136-1_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 312-162-652-621-261-123-637-167-167-761-627-261-763-376-673-623-123-756-570-301-327-137-316-267-263-216 132-301-317-672-623-632-675-263-672-627-576-657-675-576-576-657-675-175-6_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 162-126-312-326-201-102-126-203-201_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 162 123-310-103-102-136-163-167-132-231-163-126-321-163-623-263-362-216-162-567-657-165-637-317-176-762-762-376-237-236-527-250-201-236-123-126-312-126-216-326-136-316-136-315-2_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 263-263-261-576-267-267-267-263-267-267-627-267-265-267-236-431-213-143-126-612-263-246-143 627-632-631 526-570-576-657-103-103-162-632-236-732-170-673-765-675-756-675-756-765-7_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 527-157-275-237-756-352-627-376-735-137-130-152-132-132-231-261-527-157-517-513-514-510-153-516-567-567-736-574-530-257-356-532-351-315-352-532-315-325-265-231-326-237-216-236-2_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 301-136-132-173-731-371-703-316-312-301-130-103-136-102-132-231-132-136-103-103-310-103 206-206-216-632-612-136-627 136-132-143-136-167-132-136-163-162-163 302-321-130-301-302-6_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 567-657-657-623-637-675-615-312-631-351-367-321-362-632-625-576-567-567-567-657-576-562-567-675-637-156-561-627-657-675-261-367-653-653-625-657-576-657-567-576-567-657-567-652-5_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 165-103-163-103-130-730-316-162-167-106-136-103-163-106-106-167-106-136-601-157-167-621-621-163-602-623-620-267-261-267-264-103-136-157-132-106-135-160-157-106-172-126-162-136-1_V.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 135-627-632-165-367 103-675-103-163-675-671-167-167-326-123-127-126-162-620-675-106-260-627-620-623-627-216-610-765-157-675-167-176-167-167-167-657-156-360-715-165-103-102-103-1_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 326-326 657-165-716-657-671-167-163-312-312-671-167-136-106-617-106-106-163-265-162-135-627-263_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 547-547-370-730-753-372-327-372-327-216-127-172-123-127-712-172-170-170-170-715-765-156-157-675-657-657-635-362-326-326-261-263-267-236-362-163-216-132-317-231-312-526-256-271-2_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 263-326-632-612-613-305-621-320-621-675-276-136 623-163 751-765-756-765-765-756-765-751-671-627-361-527 167-106-572-152-150-132-132-132-521-162-162-137-176-157-572-756-756-576-5_W.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 160 301-160-621-130-163-130-213-231-160-270-216-276-267-267-267-765-756-765-765-765-675-613-607-613-621-260-627-612-762-627-527-652-657-672-612-267-263-231-263-675-627-756-756-6_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 267-321-302-725-257-357-157-162-103-106-172-173-167-210-123-102-157-153-123-263-326-321-320-231-231-236-362-631-632-361-631-631-260-206-206-312-362-312-302-206-236-231-231-132-2_W.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 162-602-263-216-126-216-612-672-671-610-612-621-620-623-260-326-630-360-361-160-136-106-163-316-316-361-310-312 673-627-617-756-765-765-576-756-765-765-675-106-120-106-326-120-1_W.mp3)
  Segment 22: 33096ms ~ 36192ms (file: 620-250-206-253-126-265-570-301-321 320-230-260-253-216-136-130-361-570-503-305-162-302-230-623-312-263-206-320-507-753-173-320-235-530-157-510-651-675-326-123-103-301-306-362-7_V.mp3)
  Segment 23: 37192ms ~ 37768ms (file: 132-132-132-132-136-162-162-120-123-310-312-316-123-312-132-312_V.mp3)
  Segment 24: 38768ms ~ 39344ms (file: 312-132-627-260-260-621-602-602-671-675-675-602-120-320-231-371-132-321-231-236-273-231-263-213-321-301-321-302-126-301-312-301-302-362-631-603-267-217-206-123-203-321-130-312-1_V.mp3)
  Segment 25: 40344ms ~ 40920ms (file: 206-260-206-102-130-103-320-132-130-126-613-620-362-310-301-103-301-306-310-310-310-310-312-310-321-230-231-213-261-236-236-236-321-132-130-132-130-103-130-102-231-312-213-132-1_W.mp3)
  Segment 26: 41920ms ~ 42496ms (file: 126-162 236-632-632-675-756-765-675-761-627-672-326-362-672-637-637-675-673-265-125-573-521-527-527-651 206-207 321-321-163-132-102-165-126-362-132 132-136-103-106_W.mp3)
  Segment 27: 43496ms ~ 44072ms (file: 713-176-612-612-163-167-103-103-602-167-312-671-130-103-625-123-160-613-163-103-615-361-236-261-167-106-163-126-617-165-123-216-623 105-103 672-627_V.mp3)
  Segment 28: 45072ms ~ 45648ms (file: 261-136-621-362-216-316-312-621-316-130-312-236-132-123-574-547-562-540-540-541-570-675-602-267-527-752-576-752-173-752-751-726-132-106-170-712-765-726-712-762-762-725-270-726-7_W.mp3)
  Segment 29: 46648ms ~ 47224ms (file: 615-130-562-756-657-362-632-162-160-506-761-163-612-271-167-173-670-617-617-632-617-617-670-103-107-103-103-125-160-167-137-263-267-623-126-162-107-102-201-123-310-132-321-312-1_W.mp3)
  Segment 30: 48224ms ~ 48800ms (file: 620-621-263-620-263-263-674-763-130_W.mp3)
  Segment 31: 49800ms ~ 50376ms (file: 231-321 132-630-612 376-362 267-623 574-576_W.mp3)
  Segment 32: 51376ms ~ 51952ms (file: 103-136-103-132-103-130-612-621-631-623-162_V.mp3)
  Segment 33: 52952ms ~ 53528ms (file: 126-623-236-263-736-762-627-620-620-263-326-302-726-237-236-320-203-260-123-130-132-130-631-207-360-103-312-132-137-103-173-106-132-570-750-275-732-236-326-305-530-327-321-320-3_W.mp3)
  Segment 34: 54528ms ~ 55104ms (file: 265-265-265-250-625-562-562-540-540-543-543-540-541-543-542-567-547-546-546-540-574-256-627-627-627-672-547-257-257-256-265-265-216-125-652-562-526-561-510-576-165-126-256-257-1_W.mp3)
  Segment 35: 56104ms ~ 56680ms (file: 316-136-316 263-236-326-263-623-623-632-316-316 657-675-672-627-671-627-627-623_W.mp3)
  Segment 36: 57680ms ~ 58256ms (file: 760-356-130-136-267-762-670-627-652-105-150-510-562-256-526-132-301-362-326-526-675-267-756-172-627-627-213-126-216-547-507-657-160-610-162-106-765-103-613-162-610-631-623-132-1_W.mp3)
  Segment 37: 59256ms ~ 59832ms (file: 165-156-156-562-657-561-756-576-567-165-512-135-102-130-106-261-136-216-263-132-650-127-153-163-362-612-762-136-213-162-132-371-731-762-723-763-726-763-736-732-271-123_W.mp3)
  Segment 38: 60832ms ~ 61408ms (file: 713-756-675-761-176-136-163-615-561-160-576-157 132-102-617-162-136-130-103-162-631_V.mp3)
  Segment 39: 62408ms ~ 62984ms (file: 106-705-765-670-675-106-160-627-765-671-671-136-157-175-627-675-167-605-613-623-617-136-163-157-671-103-136-612-206-602-203-302-132-527-572-576-271-632-216-765-106-107-102-102-1_W.mp3)
  Segment 40: 63984ms ~ 64560ms (file: 627-312 160-316-657-627-127-123-102-216-271-657-567-123-321-316-175-157-160-210-576-567-576-576-567-567-675-652-576-675-756-627-675-167-231-263-362-312-627-263-632-632-632-326-1_W.mp3)
  Segment 41: 65560ms ~ 66136ms (file: 345-235-174-736-152-421-167-316-231-326-631-371-174-132-127-123-162-315-361-673-152-316-213-126-312-623-236-126-215-126-321-312-326-261-624-321-312-261-231-217-213-321-236-261-3_V.mp3)
  Segment 42: 67136ms ~ 67712ms (file: 130-607-617-617-673-675-367-367-136-265-213-162-163-103-132-312-312-103-132-106-301-231-326-327-327-316-103-103-567-130-136-136-137-167-136-173-136-135-153-715-132-123-173-175-3_V.mp3)
  Segment 43: 68712ms ~ 69288ms (file: 612-102-160-106-160-762-672-620-267-362-163-126-216-230-276-267-231-612-163-607-620-621-302-320-320-326-326-236-326-102-103-162-132-163-103-167-765-765-267-167-167-136-612-136-3_W.mp3)
  Segment 44: 70288ms ~ 70864ms (file: 263-623-136-103-137-103-103-136-163-132-135-123-143-756-576-576 312-103-102-132-310-106-136-103-326-132-271-372-671-302-715-675-607-605-675-567-756-625-627-576-572-576-310-231-1_W.mp3)
  Segment 45: 71864ms ~ 72440ms (file: 120-236-102-132-312-312-237-321-357-325-756-756-735-753-137-136-671-321-312-261-632-162-675-675-207-302-132-135-123-312-276-270-723-370-103-132-123-132-132-321-317-173-123-130-3_W.mp3)
  Segment 46: 73440ms ~ 74016ms (file: 126-137-231-106-162-167-136 203-261-165-126-103-136-123-132-361-132-132-102-132-763-316 612-167-106-163-162-103-612-163-106-103-623-623-632-136-106-123-136 167-613-106-610-136-3_V.mp3)
  Segment 47: 75016ms ~ 75592ms (file: 165-123-136-153-135-315-310-310-316-310-132-315-305-351-312-352-356-352-305-130-103-130-102-506-501-507-506-503-506-506-561-156-150-603-653-631-316-350-536-563-567-563-536-563-5_W.mp3)
  Segment 48: 76592ms ~ 77168ms (file: 163-316-163-163-157-137-312-321-312-231-316-136-136-136-321-310-126-132-162-103-126-102-132-310-130-136-310-310-312-321-627-132-675-312-321-167-617-167-213-216-312-761-613-312-6_W.mp3)
  Segment 49: 78168ms ~ 78744ms (file: 126-102-213-702-326-102-126-132-125-321-675-675-623-316-102-103-102-126-601-310-230-167-130-302-106-103-106-201-216-261-321-312-612-765-675-167-612-620-612-362-236-612-261-261-3_V.mp3)
  Segment 50: 79744ms ~ 80320ms (file: 753-752-750-572-265-321-236-236-261-231-132 316 162-106-716-671-267-132-326-675-625-623-163-137-136-132-162-312-317-312-163-132-103-316-361-213-103-201-203-231-265-265-217-132-1_V.mp3)
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
> **Time**: 2026-04-03 09:49:47
> **AI**: gemini

---

## Response

AnalysisAnalysisGemini의 응답[
{
"segment": 1,
"transcript": "포기하지 마세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 42,
"audio_type": "speech",
"words": [
{"word": "포기하지", "start_ms": 42, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "마세요", "start_ms": 330, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "꿈을 향해 나아가세요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 38,
"audio_type": "speech",
"words": [
{"word": "꿈을", "start_ms": 38, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "향해", "start_ms": 160, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "나아가세요", "start_ms": 300, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "당신은 할 수 있습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 52,
"audio_type": "speech",
"words": [
{"word": "당신은", "start_ms": 52, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 220, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 310, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 390, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "매일 조금씩 연습해요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "매일", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "조금씩", "start_ms": 165, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "연습해요", "start_ms": 330, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "실수를 두려워하지 마세요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "실수를", "start_ms": 60, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "두려워하지", "start_ms": 220, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "마세요", "start_ms": 410, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "꾸준함이 답입니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "꾸준함이", "start_ms": 35, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "답입니다", "start_ms": 260, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "자신을 믿으세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 48,
"audio_type": "speech",
"words": [
{"word": "자신을", "start_ms": 48, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "믿으세요", "start_ms": 200, "dur_ms": 310, "phase_lr_deg": 0.0}
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
"transcript": "새로운 시작을 응원합니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 42,
"audio_type": "speech",
"words": [
{"word": "새로운", "start_ms": 42, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "시작을", "start_ms": 180, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "응원합니다", "start_ms": 330, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "함께 노력해봐요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "함께", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "노력해봐요", "start_ms": 180, "dur_ms": 340, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "목표를 세워보세요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "목표를", "start_ms": 55, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "세워보세요", "start_ms": 210, "dur_ms": 310, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "작은 성공이 중요해요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "작은", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "성공이", "start_ms": 150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "중요해요", "start_ms": 310, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "오늘도 힘내세요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 38,
"audio_type": "speech",
"words": [
{"word": "오늘도", "start_ms": 38, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "힘내세요", "start_ms": 190, "dur_ms": 330, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "긍정적인 마음을 가져요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "긍정적인", "start_ms": 45, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "마음을", "start_ms": 280, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "가져요", "start_ms": 420, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "집중이 필요합니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "집중이", "start_ms": 50, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "필요합니다", "start_ms": 220, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "지치지 마세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 62,
"audio_type": "speech",
"words": [
{"word": "지치지", "start_ms": 62, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "마세요", "start_ms": 250, "dur_ms": 260, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "천 리 길도 한 걸음부터",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "천", "start_ms": 55, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "리", "start_ms": 145, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "길도", "start_ms": 225, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 325, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "걸음부터", "start_ms": 395, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "포기란 배추 셀 때나 쓰는 말",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "포기란", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "배추", "start_ms": 170, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "셀", "start_ms": 270, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "때나", "start_ms": 340, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "쓰는", "start_ms": 430, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "말", "start_ms": 520, "dur_ms": 40, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "거의 다 왔어요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "거의", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 165, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "왔어요", "start_ms": 255, "dur_ms": 270, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "당신은 최고입니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "당신은", "start_ms": 35, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "최고입니다", "start_ms": 210, "dur_ms": 320, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "오늘도 수고 많으셨어요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "오늘도", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "수고", "start_ms": 200, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "많으셨어요", "start_ms": 320, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "내일 또 만나요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 42,
"audio_type": "speech",
"words": [
{"word": "내일", "start_ms": 42, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 180, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 280, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "행복한 하루 되세요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "행복한", "start_ms": 60, "dur_ms": 170, "phase_lr_deg": 0.0},
{"word": "하루", "start_ms": 240, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "되세요", "start_ms": 370, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "건강이 최고입니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "건강이", "start_ms": 45, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "최고입니다", "start_ms": 220, "dur_ms": 310, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "항상 응원할게요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "항상", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "응원할게요", "start_ms": 190, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "좋은 꿈 꾸세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 55, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "꿈", "start_ms": 175, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "꾸세요", "start_ms": 265, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "감사하는 마음을 가져요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 42,
"audio_type": "speech",
"words": [
{"word": "감사하는", "start_ms": 42, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "마음을", "start_ms": 260, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "가져요", "start_ms": 410, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "함께라서 행복해요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 48,
"audio_type": "speech",
"words": [
{"word": "함께라서", "start_ms": 48, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "행복해요", "start_ms": 280, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "당신은 소중한 사람입니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "당신은", "start_ms": 35, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "소중한", "start_ms": 210, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "사람입니다", "start_ms": 370, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "포기하지 않는 당신이 아름다워요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 52,
"audio_type": "speech",
"words": [
{"word": "포기하지", "start_ms": 52, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "않는", "start_ms": 240, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "당신이", "start_ms": 340, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "아름다워요", "start_ms": 460, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "잘하고 있어요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "잘하고", "start_ms": 45, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 220, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "힘든 일은 지나갈 거예요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "힘든", "start_ms": 60, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "일은", "start_ms": 180, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "지나갈", "start_ms": 300, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 450, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "웃으면 복이 와요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "웃으면", "start_ms": 40, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "복이", "start_ms": 210, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "와요", "start_ms": 330, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "오늘의 노력이 내일의 결실로",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "오늘의", "start_ms": 55, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "노력이", "start_ms": 195, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "내일의", "start_ms": 325, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "결실로", "start_ms": 445, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "가장 빛나는 별은 당신입니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 42,
"audio_type": "speech",
"words": [
{"word": "가장", "start_ms": 42, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "빛나는", "start_ms": 160, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "별은", "start_ms": 310, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "당신입니다", "start_ms": 410, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "자신감을 가지세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 48,
"audio_type": "speech",
"words": [
{"word": "자신감을", "start_ms": 48, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "가지세요", "start_ms": 280, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "꿈은 이루어집니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "꿈은", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "이루어집니다", "start_ms": 165, "dur_ms": 360, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "실패는 성공의 어머니",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "실패는", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "성공의", "start_ms": 200, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "어머니", "start_ms": 340, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "마음먹기에 달렸어요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 42,
"audio_type": "speech",
"words": [
{"word": "마음먹기에", "start_ms": 42, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "달렸어요", "start_ms": 290, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "포기는 또 다른 시작",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "포기는", "start_ms": 55, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 205, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "다른", "start_ms": 295, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "시작", "start_ms": 395, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "도전을 멈추지 마세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "도전을", "start_ms": 45, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "멈추지", "start_ms": 215, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "마세요", "start_ms": 365, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "당신이 있어 든든해요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "당신이", "start_ms": 60, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "있어", "start_ms": 210, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "든든해요", "start_ms": 320, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "함께하면 이겨낼 수 있어요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 48,
"audio_type": "speech",
"words": [
{"word": "함께하면", "start_ms": 48, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "이겨낼", "start_ms": 240, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 370, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 440, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "기회는 준비된 자에게",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 52,
"audio_type": "speech",
"words": [
{"word": "기회는", "start_ms": 52, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "준비된", "start_ms": 192, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "자에게", "start_ms": 342, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "한계를 넘어서세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "한계를", "start_ms": 40, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "넘어서세요", "start_ms": 210, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "진심은 통하게 마련입니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "진심은", "start_ms": 55, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "통하게", "start_ms": 205, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "마련입니다", "start_ms": 345, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "작은 습관이 인생을 바꾼다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 42,
"audio_type": "speech",
"words": [
{"word": "작은", "start_ms": 42, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "습관이", "start_ms": 162, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "인생을", "start_ms": 302, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "바꾼다", "start_ms": 432, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "내일의 해는 다시 뜬다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 48,
"audio_type": "speech",
"words": [
{"word": "내일의", "start_ms": 48, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "해는", "start_ms": 198, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 318, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "뜬다", "start_ms": 438, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "믿는 대로 이루어질 거예요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "믿는", "start_ms": 35, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "대로", "start_ms": 155, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "이루어질", "start_ms": 255, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 425, "dur_ms": 110, "phase_lr_deg": 0.

---
*Generated by WKAppBot ask gemini — 2026-04-03 09:49:47*
