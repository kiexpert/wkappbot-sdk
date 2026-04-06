# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=09:16:33  uptime=28638s  mem=1912MB  system_processes=447"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_091623.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 162 612-162-612-651-654-162-310-301-631-362-613-526-675-631-163-341-765-576-132-126-321-132-761-132-654-123-167-136-567-617-236-173-716-612-627-103-310-623-637-620-261-260-231-2_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 105-610-136-136-675-675-617-317-175-173-137-173-671-716-175-715-176-173-107-137_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 361-160-136-321-103-130-136-103-613-561-126-167-632-103-106-136-160-103-610-106-615-103-130-167-205-701-672-267-263_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 165-156-123-127-725-652-156-571-357-357-367-632-265-265-265-572-623-267-637-675-357-351-657-375-756-765-762-672-765-365-374-376-371-213-673-617-176-716-765-372-673-376-137-735-6_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 261-362-237-367-326-236-276-601-607-106-103-261-261-263-612-263-621-716-627-265 267-260-627-236-623-236-162-326-362-236-672-562-675-670-607-163-167-106-106-362-637-136-136-756-1_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 106-162-106-106-103-163-106-172-167-517-753-671-132-206-762-321-615-167-127-162-175-174-174-136-312-301-163-301-571-321-206-620-617-512-135-720-570-571-672-260-206-601-561-615-6_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 623-236-627-672-617-576-567-567-106-162-136-103-306-623-326-627-673-620-612-617-620-620-620-206-621-672-627-627-675-620-267-627-672-615-165-615-612-160-263-320-316-162-612-631-6_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 657-567-361-567-675-360-213-132-567-653-572-570-103-673-576-260-260-260-260-675-675-760-107-106-103-103-103-103-310-301-547-574-567-576-236-123-672-657-621-627-765-765-765-136-1_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 136-156-576-103-106-217-230-201 234-234-257-257-132-103-132 627 263-263-236-372-216-132-137-130-310-130-103-316-376-632-130-326-602-603-627-670-607-620-607-627-276-627-267-267-2_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 167-617-651-675-216-362-163-623-632-631-675-627-621-672-620-621-675-675-657-671-173-136-316-326-132-320-162-162-103-106-106-317-137-132-130-320-312-103-301-673-762-672-732-732-3_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 607-632-623-623-612-632-326-632-362_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 103-123-672-653-162-316-137-136-103-617-123-126-156-671-163-736-103-160-316-106-163-163-526-316-103-106-163-623-263-130-125-106-156-637-163-105-132-163-167-137-143-103-127-517-5_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 326-312-231 257-256 124-136-175 317-160-136-136-103-613-637_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 326-352-352-231-236-236-236-326-231-236-236-267-267-265-326-236-321-236-325-523-352-351-321-132-256-265-257-205-250-265-205-123-231-263-260-216-263-253-523-352-253-356-312-213-3_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 267-237-267-267-267-267-627-267-263-267-623-267-267-627-267-267-263-263-267-236-263-236-236-237-237-236-263-236-623-672-625-726-672-267-326-326-325-315-135-153-123-136-162-162-1_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 216-312-132-357-713 137-632-367-631-236-163-362-312-102-362-327-362-765-765-137-312-103-136-163-132-136-173-126-231-143-136-136-126-321-123-712-103-615-165-165-162-152-613-160-1_V.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 610-126-625-263-652-163-765-675-657-675-567-675-651-657-567-163-613-675-165-567-675-657-657-567-657-576-571-567-571-756-762-265-625-163-160-675-765-675-675-106-167-176-765-675-1_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 670-176-127-176-715-756-571-760-603-603-613-163-361-316-301-762-620-260-621-610-160-560-163-302-203-657-620-602-630-260-260-627-326-312-302-102-126-261-132-136-267-320-162-310-5_V.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 312-123-312-162-163-617-617-627-163-631-312-623-263-162-612-362-312-236-623-263-621-621-613-156-157-756-716-371-763-316-132-301-136-231-321-261-126-753-537-103-102-102-312-136-1_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 130-103-163-612-627-162-106-106-106-576-715-310-310-362-307-307-163-167-170-201-206-326-302-302-362-306-162-163-103-260-206-612-216-635-360-321-260-263-231-321-213-162-613-612-6_V.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 612-216-136-162-312-316-136-163-137-761-571-537-517-175-157-167-513-135-156-167-165-123-261-267-267-267-267-267-267-267-261-216-261-216-267-261-621-261-261-162-126-216-261-126-2_W.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 571-675-567-625-135-135-132-137-106-160-173-107-106-160-136-106-130-361-632 627-627-103-137-312-321-137-137-137-136-103-310-320-203-321-301-301-307-167-160-160-167 106-172-135-1_V.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 627-612-612-632-672-263-361-613-675-617-765-372-760-736-730-761-631-632-362-236-132-213-517_W.mp3)
  Segment 24: 36248ms ~ 47120ms (file: 267-312-132-231-213-132 324-136-163-136-134-103-123-231-136-103-103-130-321-162-612 136-132-104 321-321 267-261-625-176 713-132-576-750-136-123_V.mp3)
  Segment 25: 48120ms ~ 48696ms (file: 123-261-301-136-156-127-675-362-136-362-261-165-167_V.mp3)
  Segment 26: 49696ms ~ 50272ms (file: 206-267-625-362-206-607-763-576-567-136_W.mp3)
  Segment 27: 51272ms ~ 51848ms (file: 326-236-576-237-237-257-301-312-123-132-312-231-250-273-231-127-167-302-137-315-317-371-173-137-312-317-137-132-321-320-123-756-675-675-627-672-675-574-754-756-576-756-106-602-7_W.mp3)
  Segment 28: 52848ms ~ 53424ms (file: 612-167-617-617-167-216-621-162-621-263-601-621-302-120-126-210-261-216-231-126-312-216-123-652-623-610-312-362-126-231-210-261-120-216-261-103-206-120-162-127-132-623-162-621-6_W.mp3)
  Segment 29: 54424ms ~ 55000ms (file: 362-236-103-160-760-631-103-601-162-601-613-612-761-137-612-136-106-136-103-103-103-703-236-320-132-123-176-137-302-130-160-103-310-103-130-106-163-623-236-567-163-650-617-602-6_V.mp3)
  Segment 30: 56000ms ~ 56576ms (file: 136-621-163-610-136-137-165-576-657-163-761-631-627-126-103-130-103-103-267 765-612-216 273-761-720-762-256-562-271-765 560-560-765_W.mp3)
  Segment 31: 57576ms ~ 58152ms (file: 261-162-263-230-213-631-362-216-126-203-301-302-312-321-132-602-126-136-102-103-123-132-123-162-326-123-216-310-130-310-132-136-132-172-106-306-163-612-132-162-216-632-167-716-1_V.mp3)
  Segment 32: 59152ms ~ 59728ms (file: 761-176-761-761-617-762-761-176-267-267-267-236-203-260-206-270-326-360-657-132-310-672-627-130-103-107-137-762-276-207-326-102-162-130-271-120-675-620-263-230-230-362-536-352-1_W.mp3)
  Segment 33: 60728ms ~ 61304ms (file: 310-310-320-320-361-130-301-310-361-631-672-163-310-103-137-301-301-213-301-216-312-301-326-312-210-206-261-167-617-167-167-160-167-621-106-126-137-301-371-237-623-260-260-267-2_V.mp3)
  Segment 34: 62304ms ~ 62880ms (file: 756-765-762-762-562-657-675-672-652-576-576-567-527-576-765-756-751-672-567-567-657-276-627-765-765-762-756-675-276-627-263-236-216-267-316-136-132-126-612-162-561-163-762-751-1_W.mp3)
  Segment 35: 63880ms ~ 64456ms (file: 132-175-756-217-261-135-126-612-657-263-670-165-675-312-621-132-162-675-672-675-675-675-675-637 576-537-576-612-163-162-167-263-132-136-103-132-103-106-163-162-103-103-671-106-1_W.mp3)
  Segment 36: 65456ms ~ 66032ms (file: 267-716-620-620-627-623-261-627-612-621-316-601-360-765-362-632-362-236-630-361-360-306-306-620-620-132-132-570-570-507-302-316-312-132-106-362-316-136-132-167-106-167-160-167-1_W.mp3)
  Segment 37: 67032ms ~ 67608ms (file: 136-103-137-570-574-617-617-207-326-302-132-136-163-716-267-276-231-126-312-132-613-670-316-362-263-362-572-756-576-607-672-602-631-612-613-163-671-627-162-627-756-567-506-657-1_V.mp3)
  Segment 38: 68608ms ~ 69184ms (file: 203-123-103-302-132-310-130-136-137-603-607-260-103-102-172-321-352-301-350-301-230-231-236-560-520-253-205-213-602-612-163-162-206-203-302-154-102-301-302-673-763-625-263-620-1_V.mp3)
  Segment 39: 70184ms ~ 70760ms (file: 265-256-263-261-162-276-624-615-526-576-576-675-576-571-574-765-672-621-276-267-623-261-621-263-267-572-215-572-576-576-516-517-175-265-627-576-576-573-576-576-537-756-367-763-6_W.mp3)
  Segment 40: 71760ms ~ 72336ms (file: 763-173-371-735-725-752 576-157-105-157-265-261-213-231-213-215-216-215 726-632-726 316 576_W.mp3)
  Segment 41: 73336ms ~ 73912ms (file: 630-612-162-261-263-263-267-230-260-265-256-650-627-627-267-627 106-167-167-601-106-102-120-103-231-126-260-620-602-562-564-503-360-162-126-162-576-576-657-615-621-361-316-631-6_W.mp3)
  Segment 42: 74912ms ~ 75488ms (file: 103-132-123-517-315-672-312-320-326-203-236-507-754-270-207-302-231-261-562-652-652-561-546-541-541-542-315-163-103-163-167-136-167-162-617-612-163-321-302-130-163-320-302-613-6_V.mp3)
  Segment 43: 76488ms ~ 77064ms (file: 231-213-132-123-312-123-530-531-765-136-163-316 302-312-321-572-572-576-517 102-103-173-302-301-316-231-761-716-170-726-540-540-501-507-570-756-547-574 174-162-165-163-617-574-5_V.mp3)
  Segment 44: 78064ms ~ 78640ms (file: 136-134-132 631-623 367-617-136-612 123-126-162-126-132 132-675-765-675-675-675-670 132-162-103-126-125-127-145 751-621-621-127-167-216 326-364-126-145-316 127-621-631-623-326-1_V.mp3)
  Segment 45: 79640ms ~ 80216ms (file: 621-612-126-615-160-671-763-130-132-672-617-126-132-261-132-321-362-236-126-261-576-312-362-163-163-136-163-631-132-136-316-136-613-201-106-132-301-301-310-103-107-107-137-103-1_W.mp3)
  Segment 46: 81216ms ~ 81792ms (file: 130-367-631-637-673-623-136-602-136-103-325-321 263-326-267-130-106-103-652-163-103-130-103-132-103-102-253-132-103-132-126-156-132-125-523-165-613-671-103-136-160-103-136-632-3_V.mp3)
  Segment 47: 82792ms ~ 83368ms (file: 637-652-627-265-612-637-632-623-637-671-731-761-762-120-607-126-312-301 627-623-267-627-625-627-621-327-723-762-752-753-120-132-123-621-126-123-123-123-256-271-123-120-136-132-1_W.mp3)
  Segment 48: 84368ms ~ 84944ms (file: 107-107-106-104-610-167-162-126-413-172-176-167-160-756-162-176-106-136-715-175-716 175-126-576-213-620-621-103-103-106 745-107-106-103-103-715-237-127-126-176-103-165-130-657-1_V.mp3)
  Segment 49: 85944ms ~ 86520ms (file: 620-321-203-103-213-273-763-715-716-756-706-312-175-507-651-520-756-132-123 756-501-320-231-501-532-623-253-627-261-312-703-562-302-321-526-256-173-173-201-327-321-327-713-621-3_W.mp3)
  Segment 50: 87520ms ~ 88096ms (file: 126-102-247 627-607-673-610-601-274-103-103-103-105-127-103-103-125-126-127-164-143-631-135 541-175-173-154-103-106-136-167 127-324 362-321-216-321-127-103-312-123-132-236-324-3_V.mp3)
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
> **Time**: 2026-04-03 09:17:25
> **AI**: gemini

---

## Response

AnalysisAnalysisGemini의 응답[
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
{"word": "어둡다", "start_ms": 45, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "강하다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "강하다", "start_ms": 55, "dur_ms": 460, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "약하다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "약하다", "start_ms": 35, "dur_ms": 500, "phase_lr_deg": 0.0}
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
{"word": "무겁다", "start_ms": 65, "dur_ms": 480, "phase_lr_deg": 0.0}
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
{"word": "얇다", "start_ms": 50, "dur_ms": 410, "phase_lr_deg": 0.0}
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
{"word": "깊다", "start_ms": 80, "dur_ms": 430, "phase_lr_deg": 0.0}
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
{"word": "얕다", "start_ms": 30, "dur_ms": 440, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "단단하다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "단단하다", "start_ms": 60, "dur_ms": 480, "phase_lr_deg": 0.0}
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
{"word": "부드럽다", "start_ms": 40, "dur_ms": 500, "phase_lr_deg": 0.0}
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
{"word": "날카롭다", "start_ms": 55, "dur_ms": 510, "phase_lr_deg": 0.0}
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
{"word": "뭉툭하다", "start_ms": 35, "dur_ms": 520, "phase_lr_deg": 0.0}
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
{"word": "길다", "start_ms": 65, "dur_ms": 420, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "짧다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "짧다", "start_ms": 40, "dur_ms": 410, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "넓다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "넓다", "start_ms": 70, "dur_ms": 410, "phase_lr_deg": 0.0}
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
{"word": "좁다", "start_ms": 50, "dur_ms": 420, "phase_lr_deg": 0.0}
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
{"word": "높다", "start_ms": 80, "dur_ms": 400, "phase_lr_deg": 0.0}
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
{"word": "낮다", "start_ms": 30, "dur_ms": 430, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "동그랗다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "동그랗다", "start_ms": 60, "dur_ms": 480, "phase_lr_deg": 0.0}
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
{"word": "네모나다", "start_ms": 40, "dur_ms": 510, "phase_lr_deg": 0.0}
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
{"word": "세모나다", "start_ms": 55, "dur_ms": 500, "phase_lr_deg": 0.0}
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
{"word": "빨갛다", "start_ms": 35, "dur_ms": 490, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "파랗다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "파랗다", "start_ms": 65, "dur_ms": 500, "phase_lr_deg": 0.0}
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
{"word": "노랗다", "start_ms": 40, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "초록색",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "초록색", "start_ms": 70, "dur_ms": 470, "phase_lr_deg": 0.0}
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
{"word": "검정색", "start_ms": 50, "dur_ms": 480, "phase_lr_deg": 0.0}
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
{"word": "하얀색", "start_ms": 80, "dur_ms": 490, "phase_lr_deg": 0.0}
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
{"word": "하나", "start_ms": 30, "dur_ms": 380, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "둘",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "둘", "start_ms": 60, "dur_ms": 320, "phase_lr_deg": 0.0}
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
{"word": "셋", "start_ms": 40, "dur_ms": 330, "phase_lr_deg": 0.0}
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
{"word": "넷", "start_ms": 55, "dur_ms": 340, "phase_lr_deg": 0.0}
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
{"word": "다섯", "start_ms": 35, "dur_ms": 410, "phase_lr_deg": 0.0}
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
{"word": "여섯", "start_ms": 65, "dur_ms": 420, "phase_lr_deg": 0.0}
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
{"word": "일곱", "start_ms": 40, "dur_ms": 430, "phase_lr_deg": 0.0}
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
{"word": "여덟", "start_ms": 70, "dur_ms": 440, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "아홉",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "아홉", "start_ms": 50, "dur_ms": 420, "phase_lr_deg": 0.0}
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
{"word": "열", "start_ms": 80, "dur_ms": 310, "phase_lr_deg": 0.0}
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
{"word": "일", "start_ms": 30, "dur_ms": 280, "phase_lr_deg": 0.0}
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
{"word": "이", "start_ms": 60, "dur_ms": 290, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "삼",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "삼", "start_ms": 40, "dur_ms": 310, "phase_lr_deg": 0.0}
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
{"word": "사", "start_ms": 55, "dur_ms": 320, "phase_lr_deg": 0.0}
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
{"word": "오", "start_ms": 35, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "육",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "육", "start_ms": 65, "dur_ms": 340, "phase_lr_deg": 0.0}
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
{"word": "칠", "start_ms": 40, "dur_ms": 330, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "팔",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "팔", "start_ms": 70, "dur_ms": 320, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 09:17:25*
