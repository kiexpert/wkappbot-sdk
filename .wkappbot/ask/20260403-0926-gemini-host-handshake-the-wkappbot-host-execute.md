# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=09:25:38  uptime=29183s  mem=1809MB  system_processes=448"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_092528.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 123-132-102-126-132-162-106-312-126-260 672-167-672-672-672-263 273-726-725-726-725-712-726-712-715-751-735-675-713-372-237-675-625-275-257-275-625-526-571-675-765-627-367-627-6_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 612-106-162-231-236-261-213-261-203-213-201-261-102-267-627-107-316-570-602-620-603-623-623-602-326-103-163-132-310-312-132-136-132-137-312-301-312-163-106-316-173-627-621-312-1_V.mp3)
  Segment 3: 3152ms ~ 5888ms (file: 123-132 163-157-761 765-672 576-765-675-602-360-301-312-271-157-316-326-301-301-167-761-765-607-263-612-612-627-361-302-326-321 263-632-326-621-261-162-173-610-130-761-674-761-1_V.mp3)
  Segment 4: 6888ms ~ 7464ms (file: 756-765-706-276-327-321-231-623-670-627-672-672-627-617-675-671-627-675-675-756-675-607-672-617-613-162-361-326-673-763-306-307-763-367-367-163-125-163-167-605-627-561-132-675-6_W.mp3)
  Segment 5: 8464ms ~ 9040ms (file: 260-260-136-136-317-167-752-302-326-316-315-301-675-657-367-573-231-213-321-326-627-132-136-632-132-132-132-312-132-756-756-765-576-570-570-570-756-760-750-701-107-176-103-621-3_W.mp3)
  Segment 6: 10040ms ~ 10616ms (file: 315-321-132-312-123-321-312-312-312-132-321-312-126-316-312-316-531-537-537-623-627-623-263-621-261-217-263-236-236-326-361-367-361-263-267-267-263-521-527-361-263-312-321-163-1_W.mp3)
  Segment 7: 11616ms ~ 12192ms (file: 361-362-362-627-126-162-163-162-162-635-362-263-162-621-267-621-627-672-267-627-267-623-621-132-231-216-213-236-263-267-265-267-326-263-263-623-263-236-326-326-236-627-165-265-1_W.mp3)
  Segment 8: 13192ms ~ 13768ms (file: 162-106-130-675-136-103-103-106-156-136-657-156-160-612-132-165-163-627-617-103-167-175-756-103-160-765-516-135-137-167-761-571-107-167-176-167-601-106-162-263-671-562-671-657-5_V.mp3)
  Segment 9: 14768ms ~ 15344ms (file: 623-362-357-362-327-326-362-567-567-625-627-567-352-567-352-376-267-263-257-352-326-326-327-376-327-376-362-376-375-372-376-351-375-367-237-326-367-362-623-367-736-365-326-263-5_W.mp3)
  Segment 10: 16344ms ~ 16920ms (file: 627-671-621-673-671-632-362-326-632-613-627-672-675-657-657-671_W.mp3)
  Segment 11: 17920ms ~ 18496ms (file: 123-276-217-216-157-302-754-750-126-123-670-672-267-267-206-206-206-206-750-732-372-730-756-705-765-705-756-675-675-670-621-312-637-574-574-302-321-132-103-132-103-316-621-623-2_V.mp3)
  Segment 12: 19496ms ~ 20072ms (file: 576-567-576-567-567-567-570-567-675-765-657-576-567-567-567-657-607 321-126-136-236-206-201-260_W.mp3)
  Segment 13: 21072ms ~ 21648ms (file: 176-765-301-132-231-312-312-157-120-762-617-276-725-172-102-102-105-157-167-705-170-106-172-120-173-170-762-716-716-170-267-763-760-760-672-231-231-317-267-172-217-125-571-132-2_W.mp3)
  Segment 14: 22648ms ~ 23224ms (file: 613-162-607-623-625-362-637-623-607-610-613-625-615-163-632-107-637-367-671-627-267-625-167-173-671-672-603-136-162-567-612-672-362-263-261-236-625-627-657-652-672 574-752-206-2_W.mp3)
  Segment 15: 24224ms ~ 26168ms (file: 765-675-573-562-567-561-563-567-536-357-651-130-132-103-312 261 321-312-206-602-160 163 106-163_V.mp3)
  Segment 16: 27168ms ~ 27744ms (file: 657-132-261-627-621-610-602-627-623-625-673-675-675-762-623-167-623-613-672-675-567-627-762-267-627-675-672-765-673-672-675-675-621-672-675-627-627-671-675-672-673-675-765-672-6_W.mp3)
  Segment 17: 28744ms ~ 29320ms (file: 265-627-362-321-316-136-357-537-573-574-571-657-613-613-316-136-362-236-362-316-361-316-136-156-513-351-651-631-316-365-635-615-163-637-627-267-672-371-567-653-361-657-637-627-6_W.mp3)
  Segment 18: 30320ms ~ 30896ms (file: 607-602-607-607-672-607-607-607-670-670-607-607-607-627-627-267-623-267-621-326-302-230-731-731-501-302-130-136-103-103-231-132-163-167-316-705-673-671-561-561-165-106-715-173-6_W.mp3)
  Segment 19: 31896ms ~ 32472ms (file: 136-310-136-152-162-126-163-517-165-613-162-162-263-163-163-103-761-623-263_V.mp3)
  Segment 20: 33472ms ~ 34048ms (file: 756-751-531-270-176-571-756-361-130-320-320-316-302-623-360-103-136-301-136-103-107-135-132-312-207-326-273-325-312-326-261-236-726-751-765-712-236-263-523-527-572-574-576-627-6_W.mp3)
  Segment 21: 35048ms ~ 35624ms (file: 756-167-130-162-623-612-263-321-126-123-213-231 136-165-176-167-163-360-132-136-132-612-620-612-617-261-136-623-623-621-136-612-162 176-162-516-176-625-362-106-106-136-106-715-1_V.mp3)
  Segment 22: 36624ms ~ 37200ms (file: 143-175-130-132-136-132-167-163-157-132-617-136-150-173-134-106-102-136-103-160-657-631-756-136-162-172-156-103-106-160-163-173-310-106-163-632-103-761-160-316-612-162-765-163-1_V.mp3)
  Segment 23: 38200ms ~ 38776ms (file: 175-156-165-675-175-176-675-576-567-657-657-567-657-576-562-156-157-561-157-721-162-657-765-657-562-251-652-657-716-562-125-165-576-567-657-576-567-567-157-157-516-517-516-567-1_W.mp3)
  Segment 24: 39776ms ~ 40352ms (file: 756-627-260-612-361-613-216-376-713-617-163-312-132-325-302-321-312-302-312-576-765-765-563-306-203-756-725-735-376 736-376-173-657-675-576-756-360-326-362-103-132-103-301-314-6_V.mp3)
  Segment 25: 41352ms ~ 41928ms (file: 103-132-761-621-561-627-657-657-162-163-136-162-612-107-617-106-167-160-675-602-756-176-756-175-671-672-136-125-571-176-162-176-127-156-561-516-756-137-615-107-316-316-361-361-1_W.mp3)
  Segment 26: 42928ms ~ 43504ms (file: 625-623-625-657-546-526-265-265-625-625-267-256-265-162-165-163-361-365-635-365-362-326-534-534-354-365-365-615-216-136-163-671-567-653-365-153-537-536-536-537-561-361-316-316-1_W.mp3)
  Segment 27: 44504ms ~ 45080ms (file: 132-627-673-126-617-103-130-103-136-160-316-103-136-123-602-176-132-126 136-657-615-176-167-103-715-167-761-176-163-103-756-215-162-132-756-165-137-137-106-167-163-136-613-130-1_V.mp3)
  Segment 28: 46080ms ~ 46656ms (file: 102-263-362-610-106-613-163-173-317-167-162-617-163-671-602-627-162-106-132-143-103-761-163-615-671-137-172-167-137-163-673-765-617-165-167-165-163-126-105-163-156-163-216_V.mp3)
  Segment 29: 47656ms ~ 48232ms (file: 672-106-326-162-267-103-326-321-160-106-102-126-162-207-163-632-367-316-361-603-561-632-126-320-315-576-156-160-603-130-613-617-103-160-612-571-627-657-576-632-617-632-671-657-6_W.mp3)
  Segment 30: 49232ms ~ 49808ms (file: 756-765-761 157-576-167-615-156-652-621-162-167-613-312-176-320-321-263-312-627-216-625-725-206-126-621-261-217-167-106 267-217-201-217-361-132-126-321-312-632 126-312-163-125-1_V.mp3)
  Segment 31: 50808ms ~ 51384ms (file: 762-621-601-675-601-631-136 173-715-176 162-627-627-612-126-617-163-176-165-130-651-160-130-162-103-103-136-103-165-162-135-761-160-615-261-163-106-756-172-173-162-163-673-136-1_V.mp3)
  Segment 32: 52384ms ~ 52960ms (file: 317-157-671-617-621-126-152-657-106-631-160-162-605-261-627-627-261-517-162-627-132-162-163-136-103-106-106-106-602-514-162-103-504-176-761-671-654-675-163-675-136-673-617-160-1_W.mp3)
  Segment 33: 53960ms ~ 54536ms (file: 576-620-267-620-627-621-670-632-603-623-603-136-162-163-162-620-210-316-231-123-750-765-675-620-267-260-602-230-302-230-132-136-106-167-106-216-621-206-620-260-620-672-127-621-1_V.mp3)
  Segment 34: 55536ms ~ 56112ms (file: 132-162-163-214-765-765-675-675-765-761-132-143-132-756-765-765-716-763-762-576-756-735-732-327-120-216-302-361-136-675-607-675-567-705-765-736-136-317-316-132-573-560-527-546-3_V.mp3)
  Segment 35: 57112ms ~ 57688ms (file: 623-631-216-671-130-107-170-756-236-312-162-301-132-157-103-170-173-170-137-127-103-132-167_W.mp3)
  Segment 36: 58688ms ~ 59264ms (file: 312-316-326-362-321-310-301-312-103-310-301-567-576-506-652-625-312 360-103-103-136-312-261-620-102-261-621-126-123-136-130-103-136-310-310-327-326-106-206-203-201-564-260-206-6_W.mp3)
  Segment 37: 60264ms ~ 60840ms (file: 762-736-761-765-765-762-756-765-637-760-673-672-672-162-127-762-627-621-657-673-672-267-627-612-617-671-671-627-671-637-162-136-163-125-572-610-160-160-167-617-617-672-617-617-7_W.mp3)
  Segment 38: 61840ms ~ 62416ms (file: 261-263-210-612-216-671-716-627-632-672-672-103-762-176-270-260-263-152-165-625-206-132-754-260-265-163-136-126-126-567-672-602-617-612-321-362-760-672-760-316-106-167-156-135-1_V.mp3)
  Segment 39: 63416ms ~ 63992ms (file: 326-754-574-756-213-102-301-261 632-612-310-632-756-267-627-762-672-276-754-132-670-670-132-716-670-760-706-765-672-673-276-167-136-105-153-135-125-132-756-701-765-672-627-162-1_W.mp3)
  Segment 40: 64992ms ~ 65568ms (file: 762-276-627-267 617-632-623 132-132-103-136-260-326-231-312-103-130-132-302-310 570 102-671-617-617-612-316-615-261-612-617-657-627-617-167-316-136-163-136-106-610-361-613-621-6_W.mp3)
  Segment 41: 66568ms ~ 67144ms (file: 312-762-672-736-637 574 726-675-675_W.mp3)
  Segment 42: 68144ms ~ 68720ms (file: 102-567-617-130-610-103-163-106-163-216-136-136-567-236-136-163-617-163-561-621-621-263-203-620_V.mp3)
  Segment 43: 69720ms ~ 70296ms (file: 103-136-607-602-106-510-103-125-315-103-120-106-130-103-103-320-203-524-543-574-537-657-675-765-617-136-130-761-160 263-267-273-532-302-301-132-263-213-263-623-326-301-360-301-3_V.mp3)
  Segment 44: 71296ms ~ 71872ms (file: 762-320-302-321-321-306-126-132-103-312-321-176-163-623-162-261-236-673-310-312-276-627-762-623-267-263-267-236-206-236-326-263-203-130-135-103-132-102-102-123-213-123-126-123-1_V.mp3)
  Segment 45: 72872ms ~ 73448ms (file: 276-762-762-762-765-762-765-765-756-567-576-675-752-257-235-205-206-265-235-263-263-237-321-231-213-217-123-123-162-210-754-457-576 302 125-261-623-263 137-103-103-167 576-516-6_W.mp3)
  Segment 46: 74448ms ~ 75024ms (file: 517-512-152-152-527-547-524-257-726-173-157-153-153-153-153-567-315-532-521-152-152-125-517-251-251-523-574-572-547-512-532-563-537-574-531-574-567-754-735-637-627-570-537-576-5_W.mp3)
  Segment 47: 76024ms ~ 76600ms (file: 671-167-163-132-302-267-236-263-236-623-236-621-621-263-162-136-361-263-136-216-361-136-163-316-132-132-160-130-627-306-602-216-612-726-267-627-267-213-260-567-562-675-762-260-2_W.mp3)
  Segment 48: 77600ms ~ 78176ms (file: 760 627-261-267-625-560-560-561-326-126-210-612-536-653-312-326-310-362-126-230-103-623-576-372-327-317-301-630-321-132-150-125-231-102-126-206-263-301-326-621-672-672-576-301-2_V.mp3)
  Segment 49: 79176ms ~ 79752ms (file: 657-576-567-567-567-156-567-652-135-165-256-265-576-652-567-516-156-312-371-372-726-267-276-217-132-312-162-167-361-362-326-236-263-326-236-257-567-652-257-362-267-367-276-276-2_W.mp3)
  Segment 50: 80752ms ~ 81328ms (file: 627-167-136-162-635-103-132-312-123-103-163-163-651-165-103-123-103-124-130-751-106-136-163-136-162-106-136-103-526-125-621-125-106-132-162-106-615-361-136-561-167-675-167-136-1_V.mp3)
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
> **Time**: 2026-04-03 09:26:31
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
{"word": "칠", "start_ms": 75, "dur_ms": 325, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 09:26:31*
