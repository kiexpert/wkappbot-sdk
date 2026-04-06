# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=09:36:45  uptime=29850s  mem=1734MB  system_processes=446"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_093635.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 617-127-102-326-613 651-621-612-106-163-126-165-670-163-106-102-675-715-160-671-620-260-762-157-571-576-120-213-653-167-614-136-316-756-756-756-765-756-756-756-756-123-257-613-1_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 167-106-106-106-106-567-567-657-132-756-765-765-607-675-576-567-576-576-361-627-675-672-613-106-576-572-750-750-316-261-106-106-103-106-106-321-320-263-236-362-576-560-136-620-6_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 310-106-657-362-632-603-613-106-127-631-216-136-613-136-651-312-672-326-216-123-316-136-763-106-167-136-750-671-167-762-125-127-163-362-167-103-106-136-132-613-136-312-103-130-1_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 132-126-210-312-132-210-123-213-236-312-316-136-103-103-237-213-216-362-132-120-316-123-261-103_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 103-167-104-156-126-127-163-136-136-137-132-657-176-162-275-126-614-316-103-103-132-105-617-132-103-106-106-136-120-136-163-106-120-163-165-106-106-163-160-617-562-175-152-127-6_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 267-671-637-160-612-610-160-621-267-507-760-765-607-627-620-602-627-560-605-627-206-560-301-305-150-675-576-576-675-267-326-263-203-627-316-610-106-316-162-610-574-517-157-132-3_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 631-670-675-657-635-625-356-256-615-675-675-671-675-765-764-765-763-672-762-162-216-236-235-310-216-136-672-623-263-321 627-625-537-241-261-612-106-321-326-631-361-674-613-217-2_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 157-725-762-275-276-267-162-726-721-236-236-276-627-136-127-672-127-726-627-267-726-267-237-267-261-263-236-627-267-572-276-236-172-236-312-167-132-162-132-136-671-672-672-671-1_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 765-756-765-756-765-576-756-756-576-576-756-576-756-725-726-726-705-726-732-672-273-103-123-657 762-127-316-751-756-725-761-765-726-725-756 617-765-275-271-163-106-617 672_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 126-216-136-123-316-263-276-276-267-276-276-267-217-261-527-527-251-236-321-213-321-321-231-236-106-617 675-652-574-576 732 156-265-132-623-132-132-312-306-316-623-126-312-103-1_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 273-615-265-261-162-103-102-123-103-132-613-126-162-162 276 341-132-507-751-321-301-103-315-103-137-314-134-136-103-163-132-172-756-756-756-705-627-260-620-132-103 137-163-765-7_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 623-263-362-321-261-216-123-321-603-160-312-132-132-126-621-162-163-160-102 130-106-102-103-106-103-106-617-316-163-312-632-312-143-145-354-361-324-163-234-103-136-126-167-146-1_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 310-310 234-126-103-106-102-132-136-316-231 103-103-102-267-602-601-612-126-102-163-167-136-165-164-154-132-162-124-132-176-162-136-237-275-263-206-260-260-263-206-621 103-174-6_V.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 137-173-137-652-617-625-627-672-672-312-237-312-631-631-326-276-217-763-762-136-312-316-163-362-316-126-163-621-321-267-762-621-126-326-316-123-216-273-265-623-362-216-213-321-5_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 657-652-675-526-657-576-765-765-763-756-573-574-657-567-536-165-657-623-762-562-175-573-576-576-657-567-576-567-715-563-562-576-625-576-562-536-627-562-563-756-562-526-672-625-6_V.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 675-675 316-167-673-673-621-675-517-675-675-675-672-672-607-675-762_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 627-267-627-627-627-267-672-267-627-267-265-256-357-263-326-315-351-512-516-516-356-367-362-327-321-231-236-326-236-321-326-312-135-513-235-235-231-537-537-357-375-357-354-534-5_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 260-126-637-623-620-623-126-302-310-132-162-627-362-267-620-306-230-203-326-321-237-203-620-602-160-120-632-160-136-756-756-712-607-607-607-623-534-532-103-602-630-321-620-652-6_V.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 675-570-573-752-312-317-315-132-713-174-632-360-613-106-103-361-206-201-321-132-312-315-103-132-137-173-126-312-103-132-701-701-620-260-206-162-173-136-675-750-765-376-260-256-5_V.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 126-627-765-672-607-367-361-367-173-136-126-236-302-321-103-103-135-765-627-167-576-156-172-162-107-176-105-107-136-103-170-751-765-760-765-576-213-632-236-621-103-230-302-263-7_V.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 765-265-352-657-675-652-567-315-573-651-657-567-315-152-562-657-103-103-756-751-157-175-175-135-167-316-163-153-107-106-126-612-132-137-576-167-106-132-137-163 132-103-173-127_W.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 267-267 312-314-213-236-712-215-231-153-156-261-751-123-615-317-136-617-321-316-637-136-612-132-147-261-574-163-123-132-347-132-136-130-167-125-312 670-761_V.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 261-231-312-361-361-326-321-326-632-652-263-231-213-236-126-521-265-216-216-237-213-132-321-215-136-261-236-316-136-136-315-652-265-265-625-265-256-526-562-564-516-135-316-312-3_W.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 132-316-167-136-761-136-167-617-216-106-103-106-216-217-612-532-361-231-601-316-103-603-132-106-173-136-362-176-163-163-136-106-615-163-123-103-163-106-106-106-167-106-163-160-1_V.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 532-652-652-265-257-362 625-657-672-627-621-632-657-657-765-725-675-621-567-675-672-675-106-103_W.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 130-627-561 130 621-103-126-162-361-650-675-671-672-167-132-326-567-725-672-765-756-167-126-261-632-627-261-267-261-261-123-216-132-312-136-756-756-756-756-156-217-621-162-675-7_W.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 567-657-675-576-576-576-576-756-567-567-567-567-576-567-657-567-576-567-576-567-567-567-675-567-576-657-657-576-567-576-576-567-576-567-567-567-576-576-567-576-567-576-576-576-1_W.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 302-260-236-263-623-652-362-265-276-675-752-627-263-623-671-267 657_W.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 130-103-132-130-160-126-102-123-310-312-320-163-126-312-136-136-631-136-312-362-623-612-326-261-213-312-310-302-312-321-237-273-762-765-760-167-136-260-230-103-321-123-126-123-1_V.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 130-103-123-312-103-132-312-361-371-312-301-750-260-263-302-312-321-302-301-130-103 307-203-230-203-236-206 735-761-716-760-706-372-574-573-256-607-547-507-235-176-351-263-261-2_V.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 312-123-130-310-123-120-132-320-321-130-103-132_V.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 175-162-612-612-631-671-612-361-136-126-163-130-576-306-321-302-321-302-372-231-720-273-706-206-207-273-725-725-371-732-756-715-752 756-765-276-712-631-631-236-361-361 162-517-6_W.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 106-103-163-546-632-126-126-103-163-136-107-103-165-231-137-167-263 547-574-576 321-241-123-132-132-301-231 132-671-162-163-621-162-632-301-672-130-610-316-163-130-162 163-316-3_V.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 160-267-765-367-762-276-213-312-627-267-236-623-623-263-362-316-672-627-216-236-362-162-617-263-123-362-126-261-267-263-623-623-267-163-762-267-261-267-756-576-203-260-206-213-1_W.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 531-365-316-312-316-312-325-312-321-231-216-126-231-132-326-136-236-217-263-216-261-216-236-236-235-325-165-231-123-321-312-321-361-132-265-256-265-260-120-237-321-267-265-256-3_W.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 162 120-102-213-310-103-106-301-136-103-130-310-103-162-307-307-301-260-602-627-627-607-672-260-263-267-206-320-603-602-607-602-612-316-310-316-301-312-763-103-130-103-103-316-1_V.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 672-216-123-672-612-267-623-371-213 257 756-127-271 217-721-721-237-230-203-237-275-163-163-135-167-625-762-615-106-761-176-126-256-127-127 167-137-103-136-153-136-102-162-765-1_W.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 132-163-512-152-362-126-106-162-132-106-106-176-130-137-650-162-162-163-132-103-120-613-126-765-167-130-160-136-260-602-132-132-512-253-215-257-561-165-561-567-657-567-132-165-1_V.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 546-265-652-256-567-263-260-621-627-576-627-657-675-657-627-625-672-627-267-267-572-537-165-175-561-756-573-561-563-765-526-256-563-632-256-657-276-265-256-576-567-534-531-351-2_W.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 672-627-267-627-163-126-127-123-352-357-325-352-523-532-357-572-573-537-357-576-125-527-524-523-523-352-357-351-352-351-536-536-356-365-135-536-135-315-350-351-356-356-352-256-2_W.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 610 162-671-706-167-167-176-761-123-716-632-162-762-671-617-102-673-764-672-625-613-765-756-672-326-172-765-617-106-607-675-670-765-765-167-136-627-132-106-106-136-176-106-106-6_V.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 302-625-706-670-360-362-507-235-206-705-123-576-273-206-236-301-321-203-305-153-132-103-123-153-152-123-547-105-521-302-230-312-263-162-621-317-137-723-206-627-126-123-132-236-1_W.mp3)
  Segment 43: 66192ms ~ 66768ms (file: 213-123-236-765-576-671-176-136-106-731-105-120-136-103-136-162-162-136-176-621-130-130-163-137-162-321-136-167-623-361-137-103-502-631-123-325-261-263_V.mp3)
  Segment 44: 67768ms ~ 68344ms (file: 375-576-673-376-706-760-765-726-276-675-672-167-162-617-167-165-167 761-672-236-236-607-670-675-672-326-321-123 241-154-326-617-675-765-765-760-765-273-236-267-765-756-756-765-7_V.mp3)
  Segment 45: 69344ms ~ 69920ms (file: 675-756-567-675-756-675-756-576-756-715-316-136-312-130-206-263-627-632-103-617-163-123-123-162 675-625-672-127-617 172-617 135-130-132-162-103-170-157-567-701-637-316-361-176-7_W.mp3)
  Segment 46: 70920ms ~ 71496ms (file: 627-603-362-620 623-612-715-156-103-564-325-126-613-137-561-123-172-165-512-162-103-136-106-165-321-165-137-167-613-306-156-136-105-705-136-163-563-167-126-106-136-103-103-106-1_V.mp3)
  Segment 47: 72496ms ~ 73072ms (file: 103-165-657-615-612-260-260-260-265-126-516-765-675-602-260-675-263-230-216-126-162-132-276-650-572-570-175-265-162-106-316-263-260-620-630-612-612-106-620-126-106-103-106-136-3_V.mp3)
  Segment 48: 74072ms ~ 74648ms (file: 625-567-576-256-675-267-267-256-672-576-673-136-316-316-574-567-567-576-236-263-267-540-652-625-567-567-652-625-267-265-267-623-267-263-513-267-627-627-627-136-312-265-625-573-2_W.mp3)
  Segment 49: 75648ms ~ 76224ms (file: 760-567-756 162-623-163-762-761-675-672-672-762-765-103-716-675-721-763-765-176-172-271-275-172-627-762-762-765-756-765-761-765-762-762-756-765-675-675 765-765-756-765-765-763-7_W.mp3)
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
> **Time**: 2026-04-03 09:37:32
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "깨끗하다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "깨끗하다", "start_ms": 40, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "더럽다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "더럽다", "start_ms": 50, "dur_ms": 460, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "조용하다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "조용하다", "start_ms": 30, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "시끄럽다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "시끄럽다", "start_ms": 60, "dur_ms": 420, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "밝다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "밝다", "start_ms": 45, "dur_ms": 485, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "어둡다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "어둡다", "start_ms": 60, "dur_ms": 455, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "강하다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "강하다", "start_ms": 40, "dur_ms": 495, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "약하다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "약하다", "start_ms": 65, "dur_ms": 475, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "무겁다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "무겁다", "start_ms": 40, "dur_ms": 490, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "가볍다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "가볍다", "start_ms": 70, "dur_ms": 470, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "두껍다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "두껍다", "start_ms": 50, "dur_ms": 415, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "얇다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "얇다", "start_ms": 80, "dur_ms": 435, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "깊다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "깊다", "start_ms": 30, "dur_ms": 445, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "얕다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "얕다", "start_ms": 65, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "단단하다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "단단하다", "start_ms": 40, "dur_ms": 505, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "부드럽다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "부드럽다", "start_ms": 55, "dur_ms": 515, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "날카롭다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "날카롭다", "start_ms": 35, "dur_ms": 525, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "뭉툭하다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "뭉툭하다", "start_ms": 65, "dur_ms": 425, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "길다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "길다", "start_ms": 45, "dur_ms": 410, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "짧다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 75,
"audio_type": "speech",
"words": [
{"word": "짧다", "start_ms": 75, "dur_ms": 410, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "넓다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "넓다", "start_ms": 50, "dur_ms": 425, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "좁다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "좁다", "start_ms": 80, "dur_ms": 405, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "높다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "높다", "start_ms": 30, "dur_ms": 435, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "낮다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "낮다", "start_ms": 65, "dur_ms": 485, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "동그랗다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "동그랗다", "start_ms": 40, "dur_ms": 515, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "네모나다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "네모나다", "start_ms": 55, "dur_ms": 505, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "세모나다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "세모나다", "start_ms": 35, "dur_ms": 495, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "빨갛다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "빨갛다", "start_ms": 70, "dur_ms": 505, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "파랗다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "파랗다", "start_ms": 40, "dur_ms": 515, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "노랗다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 75,
"audio_type": "speech",
"words": [
{"word": "노랗다", "start_ms": 75, "dur_ms": 475, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "초록색",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "초록색", "start_ms": 50, "dur_ms": 485, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "검정색",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "검정색", "start_ms": 80, "dur_ms": 495, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "하얀색",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "하얀색", "start_ms": 30, "dur_ms": 385, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "하나",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "하나", "start_ms": 65, "dur_ms": 325, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "둘",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "둘", "start_ms": 40, "dur_ms": 335, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "셋",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "셋", "start_ms": 55, "dur_ms": 345, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "넷",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "넷", "start_ms": 35, "dur_ms": 415, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "다섯",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "다섯", "start_ms": 65, "dur_ms": 425, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "여섯",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "여섯", "start_ms": 40, "dur_ms": 435, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "일곱",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "일곱", "start_ms": 70, "dur_ms": 445, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "여덟",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "여덟", "start_ms": 55, "dur_ms": 425, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "아홉",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "아홉", "start_ms": 80, "dur_ms": 315, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "열",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "열", "start_ms": 30, "dur_ms": 285, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "일",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "일", "start_ms": 60, "dur_ms": 295, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "이",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 45, "dur_ms": 315, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "삼",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "삼", "start_ms": 55, "dur_ms": 325, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "사",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "사", "start_ms": 35, "dur_ms": 305, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "오",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "오", "start_ms": 70, "dur_ms": 345, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "육",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "육", "start_ms": 40, "dur_ms": 335, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "칠",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 75,
"audio_type": "speech",
"words": [
{"word": "칠", "start_ms": 75, "dur_ms": 300, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 09:37:32*
