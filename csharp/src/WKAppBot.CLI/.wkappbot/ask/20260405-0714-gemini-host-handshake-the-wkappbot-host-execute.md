# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=9236  time=07:13:07  uptime=19016s  mem=468MB  system_processes=354"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_071033.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 7848ms (file: 267-627-765-675-605-206-267-236-267-527-271-572-210-725-213-216-126-275-574-210-260-102-162-231-312-130-102-162-103-263-206-267-263-213-237-170-123-257-275-205-275-273-320-267-3_W.mp3)
  Segment 2: 8848ms ~ 9424ms (file: 521-652-756-572-362-256-123-265-173-137-326-127-137-752-175 176-751_W.mp3)
  Segment 3: 10424ms ~ 11000ms (file: 135-156-136-135-136-153-157-175-156-175-136-135-165-150-167-150-156-165-156-573-573-576-571-576-576-571-576-756-315-571-107-517-571-576-675-567-561-567-576-576-567-516-576-567-6_W.mp3)
  Segment 4: 12000ms ~ 12576ms (file: 145-153-134-143-104-103-103-134-143-134 314-314-314-304-314-134-143-135-130-134-145-135-301-103-154-135-103-134-103-134-103-134 341-314-431-413-314-310-103-301-130-103-104-130-1_V.mp3)
  Segment 5: 13576ms ~ 14152ms (file: 176-671-736-671-671-671-763-173-176-203-301-120-103-136-167-613 316-106-103-103-501-501-506-632-356-316-126-130-216-236-320-236-320-376-307-301-302-320-230-123-260-267-627-310-3_V.mp3)
  Segment 6: 15152ms ~ 15728ms (file: 715-170-715-751-570-175-175-175-751-751-715-750 570-517-751-571-753-705-751-175-716-701-175-751-750-751-751-713-753-715-170-715-157-750-715-751-751-571-753-751-750 751-750-576-7_V.mp3)
  Segment 7: 16728ms ~ 17304ms (file: 672-671-673-607-670-162-167-761-612-716-217-312-761-765-756-765-761-765-675-761-761-167-610-160-613-162-671-617-671 763-762-627-763-675-175-176-627 672-264-263-236-623-126-162-1_W.mp3)
  Segment 8: 18304ms ~ 24856ms (file: 132-617-623-652-562-257-261-301-306-362-316-130-361-162-621-657-675-675-675-675-756-567-657-231 137-302-301-312-356-162-276-207-206-130-312-627-253-203-572-275-276-570-213-126-2_W.mp3)
  Segment 9: 25856ms ~ 26432ms (file: 135-143-134-145-103-103-103-103-134-135-130-103-134-137-134-103-134-143 341-310-134-104-104-104-103-137-104-104-104-170-104-107-103-103-104-134-103-104-103-104-103-103 310-134-1_V.mp3)
  Segment 10: 27432ms ~ 28008ms (file: 203-206-670-576-756-675-673-630-130-310-306-301-537-203-263-132-312-136-130-132-103-173-103-312-301-301-103-301-510-517-502-510-312-263-203-320-302-231-103-216-123-203-162-162-6_V.mp3)
  Segment 11: 29008ms ~ 29584ms (file: 657-657-576-756-657-672-301-317-372-731-723-372-176-675-567-753-675-576-657_W.mp3)
  Segment 12: 30584ms ~ 40160ms (file: 316-213-231-326-260-260-267-206-627-627-206-162-162-206-602-627-312-301-136-103-136-312-163-137-367-136-632-613-103-165-104-326-765-762-236-134-501-501-103-103-153-163-504-547-6_W.mp3)
  Segment 13: 41160ms ~ 41736ms (file: 751-571-157-571-571-517-576-756 157-751-517-571-571-170-107-176-170-175-576-576-751-571-576-765 107-170-570-157-751-715-710-175-175-175-750-157-571-175-715-751-765-175-175-176-6_W.mp3)
  Segment 14: 42736ms ~ 43312ms (file: 102-136-103-132 107-132-137-321-301-301-652-627-136-672 756-315-261-263-627-672-627-627-627-620-627-627-672-627-203-321-206-264-352-321-301-236-752-576-267-306-263-625-103-132-1_W.mp3)
  Segment 15: 44312ms ~ 51944ms (file: 132-504-625-235-725-754-754-257-573-316-132-316-132-675-657-675-367-376-316-763-765-675-763-756-635-736-573-716-670-316-236-320-367-376-637-362-365-675-675-136-375-237-320-367-5_W.mp3)
  Segment 16: 52944ms ~ 59640ms (file: 267-267-261-621-543-231-752-574-160-310-751-756-627-574-267-263-267-216-103-126-756-524-576-120-106-126-265-572-256-625-302-103-213-132-236-231-120-236-237-236-103-326-765-362-3_W.mp3)
  Segment 17: 60640ms ~ 61216ms (file: 571-517-517-715-751-751-756-751-517-571-751-507-750-576-756-571-513-175-157-157-175-537-175-576-157-157-163-157-751-512-172-157-157-576-371-375-175-571-157-516-157-756-156-526-5_W.mp3)
  Segment 18: 62216ms ~ 72980ms (file: 725-572-302-520-213-261-216-310-317-231-276-230-231-302-576-207-672-102-210-201-210-210-726-725-210-576-207-102-173-705-754-275-102-132-675-270-276-236-760-751-267-752-157-756-2_W.mp3)
  Segment 19: 73980ms ~ 82152ms (file: 263-567-127-267-574-321-276-321-523-753-712-237-375-573-573-571-537-352-237-230-123-230-762-672-576-327-312-230-321-230-327-357-675-375-320-267-316-230-231-132-310-230-236-321-2_W.mp3)
  Segment 20: 83152ms ~ 90640ms (file: 123-231-275-231-320-106-570-576-132-731-206-267-261-620-106-156-260-275-213-571-317-312-261-213-602-126-265-276-572-620-765-672-201-230-260-213-571-150-135-267-265-213-261-136-3_W.mp3)
  Segment 21: 91640ms ~ 92216ms (file: 501-501-501-501-150-510-506-510-150-510-501-517-510-507-570-567-510-501-576-571-570-510 501-150-501-501-570-571-105-517-157-517-501-501-150-501-501-105-510-517-501-510-501-501-5_V.mp3)
  Segment 22: 93216ms ~ 101244ms (file: 576-206-201-216-754-206-201-206-302-231-250-157-213-573-570-213-215-102-123-102-754-170-132-610-572-205-547-231-123-132-670-602-205-207-210-574-570-701-760-123-260-123-213-572-2_W.mp3)
  Segment 23: 102244ms ~ 102820ms (file: 762-137-135-135-163-156-135-135-135-156-725-725-756-765-752-756-752-751 175-175-715-756-675-175-576-756-567-751-761-756-167-675-657-567-657-567-576-567-567-567-657-657-657-716-7_W.mp3)
  Segment 24: 103820ms ~ 113252ms (file: 132-201-206-210-132-103-102-203-163-312-206-206-207-230-126-210-205-230-103-312-173-176-741-541-143-103-123-231-572-217-301-236-210-132-103-201-572-631-132-203-206-124-103-573-1_W.mp3)
  Segment 25: 114252ms ~ 120840ms (file: 176-261-621-267-627-567-230-213-542-521-102-153-256-716-310-506-153-217-213-123-561-132-567-267-206-210-267-210-312-123-547-217-210-206-267-752-261-527-170-137-160-137-260-206-7_W.mp3)
  Segment 26: 121840ms ~ 122416ms (file: 765-675-675-675-657-765-675-672-675-672-675-675-675-672-675-672-675-672-672-675-765-627-675-675-675-672-675-675-657-657-675-765-576-567-657-657-675-751-756-617-716-517-574-352-1_W.mp3)
  Segment 27: 123416ms ~ 136196ms (file: 207-102-102-173-132-756-107-102-310-751-103-123-103-173-127-231-123-102-123-107-107-217-206-216-102-752-276-230-230-126-123-107-103-175-716-574-217-210-756-754-567-103-236-210-2_V.mp3)
  Segment 28: 137196ms ~ 137772ms (file: 175-501-157-150-105-105-175-571-105-571-501-510-150-501-517-570-574 517-571-105-157-150-576-103-150-157-175-156-157-157-516-107-501-517-153-517-157-175-715-756-571-570-576-510-1_V.mp3)
  Segment 29: 138772ms ~ 139348ms (file: 751-765-713-271-217-213-275-273-127_W.mp3)
  Segment 30: 140348ms ~ 140924ms (file: 675-576-103-310-610-107-137-316-173-736-176-176-716-173-176-176-173-176-173 367-317-761-735-713-130-176-671-316-631-176-167-761-675-610-613-673-617-716-316-370-176-176-173-136-3_V.mp3)
  Segment 31: 141924ms ~ 142500ms (file: 562-650-507 576-726-576-652-213-263-236-261-271-356-253-267-216-562-275-127-751-376-316-576-235-765-276-256-261-317-137-321-273-560-312-273-237-762-256-276-567-657-572-267 267-5_V.mp3)
  Segment 32: 143500ms ~ 151168ms (file: 370-312-167-712-103-310-130-102-126-236-102-321-123-137-267-725-137-675-160-276-276-213-721-267-276-102-137-267-120-123-102-172-572-217-216-231-310-276-132-103-756-231-267-203-6_W.mp3)
  Segment 33: 152168ms ~ 160952ms (file: 130-231-120-231-261-102-120-213-160-106-567-231-263-263-216-123-547-160-163-371-213-321-123-612-163-162-310-137-102-231-754-263-261-457-236-120-523-620-236-103-213-623-261-621-6_W.mp3)
  Segment 34: 161952ms ~ 162528ms (file: 167-137-163-516-132-136-136-715-326-160-675-715-751-637-103-671-670-167 137_W.mp3)
  Segment 35: 163528ms ~ 164104ms (file: 326-213-615-265-625-625-265-672-260-534-536-523-235-356-325-537-574-571-531-524-257-251-253-135-123-136-137-136-156-516-576-576-567-576-765-576-567-357-356-357-365-651-365-365-6_W.mp3)
  Segment 36: 165104ms ~ 165680ms (file: 501-510-150-501-150-157-501-150-510-571-510-516-571-510-150-501-576 501-507-501-517-157-513-517-507-105-517-501-150-501-501-501-510-501-501-150-150-501-570-157-510-510-517-571-5_V.mp3)
  Segment 37: 166680ms ~ 167256ms (file: 615-576-176-163-136-712-275-275-316-361-135-135-136-135-153-517-152-153-163-613-163-263-263-315-317-157-162-310-130-167-156-175-162-120-172-765-726-176-375-765-765 321-371-357-5_W.mp3)
  Segment 38: 168256ms ~ 176788ms (file: 372-317-576-572-273-756-257-236-236-260-317-103-107-317-237-567-701-517-320-132-310-312-351-276-726-372-627-267-576-235-327-576-276-236-327-762-260-672-672-267-267-756-173-703-2_W.mp3)
  Segment 39: 177788ms ~ 178364ms (file: 571-571-517-510-570-571-150-150-157-517-150-516-152-576-175-561-157-156-357-531-157-513-517-150-517-157-576-153-516-571-517-157-517-516-517-157-571-517-175-571-173-715-751-157-7_W.mp3)
  Segment 40: 179364ms ~ 179940ms (file: 750-157-517-571-517-715-751-107-105-507-570-510-751-517-157-570-571-570-570-574-571-510-570-517-150-750-750-756-756-754-756-756-754-574-754-750-517 547-574-574-574-574-517-175-1_V.mp3)
  Segment 41: 180940ms ~ 181516ms (file: 156-150-156-156-150-156-150-150-501 150-105-105-105-107-150-105-150-150-510-150-510-150-105-105-105-150-156-105-156-106-150-501 105-150-150-157-173-160-176-107-137-136-106-165-1_V.mp3)
  Segment 42: 182516ms ~ 190328ms (file: 103-315-573-567-132-213-132-103-567-576-130-752-576-176-670-213-276-725-107-571-504-527-157-612-130-315-132-317-102-216-207-312-126-175-507-103-235-213-215-103-572-231-752-507-2_V.mp3)
  Segment 43: 191328ms ~ 191904ms (file: 753 715-701-703-751-176-715-173-175-173-170-710-170-173 753-571-751-705-173-703-731-705-750-705-570-701-750-750-570-705-705-705-750-703-710-175-705 175-715-573-731-137-713-571-1_V.mp3)
  Segment 44: 192904ms ~ 204352ms (file: 165-157-702-132-312-670-123-126-312-537-276-251-206-135-617-265-137-123-261-132-317-167-106-106-137-127-217-765-627-267-574-361-763-136-301-153-671-261-123-217 132-132-132-312-2_W.mp3)
  Segment 45: 205352ms ~ 205928ms (file: 167-163-613-726-321-302-132-517-601-130-132-213-321-302-536-302-537-751-305-275-256-235-301-302-103-301-126-315-136-203-213-203-230 206-263-203-207-236-213-325-231-275-326-203-3_W.mp3)
  Segment 46: 206928ms ~ 220428ms (file: 526-527-235-321-235-265-235-236-263-263-231-756-265-263-263-263-263-236-236-256-527-206-236-263-263-231-574-321-527-120-120 261-231-312-213-206-236-532-321-213-236-263-103-231-2_W.mp3)
  Segment 47: 221428ms ~ 222004ms (file: 143-134-314-134-135-134-134-143-134-104-134-143-145-314-103-301 154-435-154-104-105-145-104-135-104-140-174-103-104-104-134-104-145-147-134-314-104-134-134-145-134-103-104-134-1_V.mp3)
  Segment 48: 223004ms ~ 223580ms (file: 751 756-571-756-756-751-517-753-750-750-573-753-571-751-571-750-175-573-713-753-315-751-752-571-751-753-751-576-571-576 750-571-750-570-751-750-570-705-571-570-750-751-573-571-5_W.mp3)
  Segment 49: 224580ms ~ 229836ms (file: 276-732-276-123-127-123-312-123-312-326-237-712-273-263-321-132-312-271-327-217-276-102-326-301-123-217-275-231-321-123-127-317-572-753-573-317-710-376-130-137-357-750-537-276-2_W.mp3)
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
> **Time**: 2026-04-05 07:14:21
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "안녕하세요 오늘 날씨가 참 좋네요 그쵸",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 150, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 1100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 1550, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "참", "start_ms": 2200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 2550, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "그쵸", "start_ms": 3150, "dur_ms": 400, "phase_lr_deg": 0.0}
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
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 8,
"transcript": "Excuse me, where is the nearest subway station?",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 210,
"audio_type": "speech",
"words": [
{"word": "Excuse", "start_ms": 210, "dur_ms": 450, "phase_lr_deg": -5.0},
{"word": "me", "start_ms": 700, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "where", "start_ms": 1000, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "is", "start_ms": 1380, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "the", "start_ms": 1550, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "nearest", "start_ms": 1700, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "subway", "start_ms": 2250, "dur_ms": 450, "phase_lr_deg": -5.0},
{"word": "station", "start_ms": 2750, "dur_ms": 600, "phase_lr_deg": -5.0}
]
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 12,
"transcript": "이 근처에 맛있는 식당이 어디인가요 추천해주실 수 있나요",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 320,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 320, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "근처에", "start_ms": 550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "맛있는", "start_ms": 1100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "식당이", "start_ms": 1700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "어디인가요", "start_ms": 2250, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "추천해주실", "start_ms": 3100, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 3950, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있나요", "start_ms": 4150, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 14,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 15,
"transcript": "Wait, I think I'm lost. Let me check the map again.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "Wait", "start_ms": 180, "dur_ms": 400, "phase_lr_deg": 2.0},
{"word": "I", "start_ms": 700, "dur_ms": 150, "phase_lr_deg": 2.0},
{"word": "think", "start_ms": 880, "dur_ms": 300, "phase_lr_deg": 2.0},
{"word": "I'm", "start_ms": 1200, "dur_ms": 200, "phase_lr_deg": 2.0},
{"word": "lost", "start_ms": 1450, "dur_ms": 500, "phase_lr_deg": 2.0},
{"word": "Let", "start_ms": 2100, "dur_ms": 250, "phase_lr_deg": 2.0},
{"word": "me", "start_ms": 2400, "dur_ms": 180, "phase_lr_deg": 2.0},
{"word": "check", "start_ms": 2600, "dur_ms": 350, "phase_lr_deg": 2.0},
{"word": "the", "start_ms": 3000, "dur_ms": 120, "phase_lr_deg": 2.0},
{"word": "map", "start_ms": 3150, "dur_ms": 400, "phase_lr_deg": 2.0},
{"word": "again", "start_ms": 3600, "dur_ms": 550, "phase_lr_deg": 2.0}
]
},
{
"segment": 16,
"transcript": "도와주셔서 정말 감사합니다 좋은 하루 보내세요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "도와주셔서", "start_ms": 120, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 1300, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 2200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "하루", "start_ms": 2600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "보내세요", "start_ms": 3050, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 18,
"transcript": "Sure thing, if you need anything else just ask me.",
"speaker": "speaker_2",
"confidence": 0.93,
"language": "en",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "Sure", "start_ms": 250, "dur_ms": 400, "phase_lr_deg": -3.0},
{"word": "thing", "start_ms": 700, "dur_ms": 350, "phase_lr_deg": -3.0},
{"word": "if", "start_ms": 1200, "dur_ms": 150, "phase_lr_deg": -3.0},
{"word": "you", "start_ms": 1400, "dur_ms": 150, "phase_lr_deg": -3.0},
{"word": "need", "start_ms": 1600, "dur_ms": 300, "phase_lr_deg": -3.0},
{"word": "anything", "start_ms": 1950, "dur_ms": 550, "phase_lr_deg": -3.0},
{"word": "else", "start_ms": 2550, "dur_ms": 300, "phase_lr_deg": -3.0},
{"word": "just", "start_ms": 2900, "dur_ms": 250, "phase_lr_deg": -3.0},
{"word": "ask", "start_ms": 3200, "dur_ms": 300, "phase_lr_deg": -3.0},
{"word": "me", "start_ms": 3550, "dur_ms": 200, "phase_lr_deg": -3.0}
]
},
{
"segment": 19,
"transcript": "잠시만요 이거 얼마예요 조금 비싼 것 같은데",
"speaker": "speaker_3",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 400,
"audio_type": "speech",
"words": [
{"word": "잠시만요", "start_ms": 400, "dur_ms": 700, "phase_lr_deg": 10.0},
{"word": "이거", "start_ms": 1200, "dur_ms": 350, "phase_lr_deg": 10.0},
{"word": "얼마예요", "start_ms": 1600, "dur_ms": 650, "phase_lr_deg": 10.0},
{"word": "조금", "start_ms": 2400, "dur_ms": 400, "phase_lr_deg": 10.0},
{"word": "비싼", "start_ms": 2850, "dur_ms": 450, "phase_lr_deg": 10.0},
{"word": "것", "start_ms": 3350, "dur_ms": 150, "phase_lr_deg": 10.0},
{"word": "같은데", "start_ms": 3550, "dur_ms": 600, "phase_lr_deg": 10.0}
]
},
{
"segment": 20,
"transcript": "정찰제라 할인은 어렵습니다 죄송합니다",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 300,
"audio_type": "speech",
"words": [
{"word": "정찰제라", "start_ms": 300, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "할인은", "start_ms": 1200, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "어렵습니다", "start_ms": 1800, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "죄송합니다", "start_ms": 2750, "dur_ms": 900, "phase_lr_deg": 0.0}
]
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
"transcript": "그럼 그냥 이걸로 주세요 카드로 계산할게요",
"speaker": "speaker_3",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 200, "dur_ms": 350, "phase_lr_deg": 10.0},
{"word": "그냥", "start_ms": 600, "dur_ms": 350, "phase_lr_deg": 10.0},
{"word": "이걸로", "start_ms": 1000, "dur_ms": 500, "phase_lr_deg": 10.0},
{"word": "주세요", "start_ms": 1550, "dur_ms": 550, "phase_lr_deg": 10.0},
{"word": "카드로", "start_ms": 2300, "dur_ms": 500, "phase_lr_deg": 10.0},
{"word": "계산할게요", "start_ms": 2850, "dur_ms": 800, "phase_lr_deg": 10.0}
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
"transcript": "네 결제 완료되었습니다 여기 영수증입니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "결제", "start_ms": 400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "완료되었습니다", "start_ms": 900, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "여기", "start_ms": 2100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "영수증입니다", "start_ms": 2500, "dur_ms": 950, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "What time is the next bus to the airport?",
"speaker": "speaker_4",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 300,
"audio_type": "speech",
"words": [
{"word": "What", "start_ms": 300, "dur_ms": 300, "phase_lr_deg": -15.0},
{"word": "time", "start_ms": 650, "dur_ms": 350, "phase_lr_deg": -15.0},
{"word": "is", "start_ms": 1050, "dur_ms": 150, "phase_lr_deg": -15.0},
{"word": "the", "start_ms": 1250, "dur_ms": 120, "phase_lr_deg": -15.0},
{"word": "next", "start_ms": 1400, "dur_ms": 400, "phase_lr_deg": -15.0},
{"word": "bus", "start_ms": 1850, "dur_ms": 350, "phase_lr_deg": -15.0},
{"word": "to", "start_ms": 2250, "dur_ms": 150, "phase_lr_deg": -15.0},
{"word": "the", "start_ms": 2450, "dur_ms": 120, "phase_lr_deg": -15.0},
{"word": "airport", "start_ms": 2600, "dur_ms": 600, "phase_lr_deg": -15.0}
]
},
{
"segment": 26,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 27,
"transcript": "It leaves every thirty minutes from platform five.",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 220,
"audio_type": "speech",
"words": [
{"word": "It", "start_ms": 220, "dur_ms": 150, "phase_lr_deg": -3.0},
{"word": "leaves", "start_ms": 400, "dur_ms": 450, "phase_lr_deg": -3.0},
{"word": "every", "start_ms": 900, "dur_ms": 350, "phase_lr_deg": -3.0},
{"word": "thirty", "start_ms": 1300, "dur_ms": 500, "phase_lr_deg": -3.0},
{"word": "minutes", "start_ms": 1850, "dur_ms": 550, "phase_lr_deg": -3.0},
{"word": "from", "start_ms": 2450, "dur_ms": 300, "phase_lr_deg": -3.0},
{"word": "platform", "start_ms": 2800, "dur_ms": 650, "phase_lr_deg": -3.0},
{"word": "five", "start_ms": 3500, "dur_ms": 400, "phase_lr_deg": -3.0}
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
"transcript": "(mixed)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "mixed",
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
"transcript": "The ticket is about ten dollars if you buy it here.",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "The", "start_ms": 180, "dur_ms": 120, "phase_lr_deg": -3.0},
{"word": "ticket", "start_ms": 350, "dur_ms": 400, "phase_lr_deg": -3.0},
{"word": "is", "start_ms": 800, "dur_ms": 150, "phase_lr_deg": -3.0},
{"word": "about", "start_ms": 1000, "dur_ms": 350, "phase_lr_deg": -3.0},
{"word": "ten", "start_ms": 1400, "dur_ms": 300, "phase_lr_deg": -3.0},
{"word": "dollars", "start_ms": 1750, "dur_ms": 550, "phase_lr_deg": -3.0},
{"word": "if", "start_ms": 2350, "dur_ms": 150, "phase_lr_deg": -3.0},
{"word": "you", "start_ms": 2550, "dur_ms": 150, "phase_lr_deg": -3.0},
{"word": "buy", "start_ms": 2750, "dur_ms": 250, "phase_lr_deg": -3.0},
{"word": "it", "start_ms": 3050, "dur_ms": 120, "phase_lr_deg": -3.0},
{"word": "here", "start_ms": 3200, "dur_ms": 400, "phase_lr_deg": -3.0}
]
},
{
"segment": 33,
"transcript": "혹시 공항버스 어디서 타는지 아세요 저기 건너편인가요",
"speaker": "speaker_5",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 350,
"audio_type": "speech",
"words": [
{"word": "혹시", "start_ms": 350, "dur_ms": 450, "phase_lr_deg": -8.0},
{"word": "공항버스", "start_ms": 850, "dur_ms": 800, "phase_lr_deg": -8.0},
{"word": "어디서", "start_ms": 1700, "dur_ms": 500, "phase_lr_deg": -8.0},
{"word": "타는지", "start_ms": 2250, "dur_ms": 550, "phase_lr_deg": -8.0},
{"word": "아세요", "start_ms": 2850, "dur_ms": 500, "phase_lr_deg": -8.0},
{"word": "저기", "start_ms": 3400, "dur_ms": 400, "phase_lr_deg": -8.0},
{"word": "건너편인가요", "start_ms": 3850, "dur_ms": 900, "phase_lr_deg": -8.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "아뇨 여기서 타시면 돼요 6001번 버스입니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "아뇨", "start_ms": 200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "여기서", "start_ms": 550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "타시면", "start_ms": 1050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "돼요", "start_ms": 1600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "6001번", "start_ms": 1950, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "버스입니다", "start_ms": 3000, "dur_ms": 800, "phase_lr_deg": 0.0}
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
"transcript": "Thank you for your help, I really appreciate it.",
"speaker": "speaker_4",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "Thank", "start_ms": 250, "dur_ms": 300, "phase_lr_deg": -15.0},
{"word": "you", "start_ms": 580, "dur_ms": 150, "phase_lr_deg": -15.0},
{"word": "for", "start_ms": 750, "dur_ms": 180, "phase_lr_deg": -15.0},
{"word": "your", "start_ms": 950, "dur_ms": 200, "phase_lr_deg": -15.0},
{"word": "help", "start_ms": 1200, "dur_ms": 400, "phase_lr_deg": -15.0},
{"word": "I", "start_ms": 1700, "dur_ms": 150, "phase_lr_deg": -15.0},
{"word": "really", "start_ms": 1900, "dur_ms": 400, "phase_lr_deg": -15.0},
{"word": "appreciate", "start_ms": 2350, "dur_ms": 750, "phase_lr_deg": -15.0},
{"word": "it", "start_ms": 3150, "dur_ms": 250, "phase_lr_deg": -15.0}
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
"transcript": "천만에요 즐거운 여행 되시길 바랄게요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "천만에요", "start_ms": 150, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "즐거운", "start_ms": 900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "여행", "start_ms": 1400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "되시길", "start_ms": 1850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "바랄게요", "start_ms": 2400, "dur_ms": 700, "phase_lr_deg": 0.0}
]
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
"transcript": "혹시 이 근처에 환전소가 있을까요",
"speaker": "speaker_5",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 300,
"audio_type": "speech",
"words": [
{"word": "혹시", "start_ms": 300, "dur_ms": 450, "phase_lr_deg": -8.0},
{"word": "이", "start_ms": 800, "dur_ms": 150, "phase_lr_deg": -8.0},
{"word": "근처에", "start_ms": 1000, "dur_ms": 500, "phase_lr_deg": -8.0},
{"word": "환전소가", "start_ms": 1550, "dur_ms": 750, "phase_lr_deg": -8.0},
{"word": "있을까요", "start_ms": 2350, "dur_ms": 600, "phase_lr_deg": -8.0}
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
"transcript": "은행은 문을 닫았고 사설 환전소는 저쪽 골목에 있어요",
"speaker": "speaker_1",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 280,
"audio_type": "speech",
"words": [
{"word": "은행은", "start_ms": 280, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "문을", "start_ms": 850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "닫았고", "start_ms": 1200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "사설", "start_ms": 1900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "환전소는", "start_ms": 2350, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "저쪽", "start_ms": 3100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "골목에", "start_ms": 3550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 4100, "dur_ms": 450, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 07:14:21*
