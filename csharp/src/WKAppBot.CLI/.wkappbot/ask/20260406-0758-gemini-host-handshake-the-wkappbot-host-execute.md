# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=18336  time=07:57:03  uptime=15145s  mem=266MB  system_processes=416"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_075553.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 751-571-753-570-571-175-173-135-157-576-715-751 571-571-571-571-574-571-571-571-175-175-103-103-105-102-102-150-317-752-713-715-710 531-120-175-750-301-137-702-720-320-153-107-1_W.mp3)
  Segment 2: 1576ms ~ 9100ms (file: 267-267-276-206-261-260-531-157-173-137-762-310-175-715-201-237-132-136-321-310-123-671-765-175-173-301-173-174-173-167-172-260-201-203-206-301-103-752-362-236-753-605-102-321-2_V.mp3)
  Segment 3: 10100ms ~ 10676ms (file: 136-135-137-136-306-316-310-301-301-301-136-302-301-367-301-136-103-103-136-132-137-137-135-135-136-106-102-106-106-102-106-103-106-106-106-106-106 134-132-147-132 132-625-654-7_V.mp3)
  Segment 4: 11676ms ~ 12252ms (file: 357-165-615-576-152-105-107-715-170-107-710-130-710-761-705-756-715-715-751-756-751-750-751-705-576-576-652-651-162-567-576-210-216-163-162-132-167-165-251-126-120-162-126-567-5_W.mp3)
  Segment 5: 13252ms ~ 21136ms (file: 276-273-132-372-751-752-103-301-375-753-305-735-175-372-312-173-170-167-163-123-627-627-321-130-361-310-321-716-263-132-613-136-103-570-754-321-132-321-163-130-213-206-126-216-1_W.mp3)
  Segment 6: 22136ms ~ 27788ms (file: 130-123-130-310-123-231-130-312-231-203-123-123-210-123-120-102-120-103-321-523-165-715-150-310-130-172-210-230-203-103-102-102-130-201-103-132-320-312-312-130-217-321-213-103-3_V.mp3)
  Segment 7: 28788ms ~ 29364ms (file: 517-517-571-317-301-130-103-137-103-137-173-103-157-157-517-516-157-517-571-517-317-130-103-107-103-105-571-517-517-517-157-751-157-157-157-157-517-517-157-517-516-517-571-517-5_V.mp3)
  Segment 8: 30364ms ~ 30940ms (file: 631-623-261-756 123-723-312-502-512-321-216-231-217-267-126-572-257-725-236-176-205-261-271-123-650-621-567-627-312-574-627-276-263-267-216-257-762-236-267-267-572-276-521-132-6_V.mp3)
  Segment 9: 31940ms ~ 37736ms (file: 132-231-326-127-206-123-231-523-132-206-203-206-123-132-130-136-236-675-326-217-753-312-361-316-376-317-562-150-253-130-617-675-567-653-615-657-517-670-652-236-236-276-276-573-7_W.mp3)
  Segment 10: 38736ms ~ 39312ms (file: 574-574-576-574-574-574-574-574-576-574-576-576-574-574-576-576-576-574-571-576-571-576_W.mp3)
  Segment 11: 40312ms ~ 40888ms (file: 506-213-725-265-735-507-362-567-276-253-537-576-231-321-265-257-547-532-123-625-765-725-526-317-103-132-362-137-326-756-326-620-312-316-260-152-210-267_V.mp3)
  Segment 12: 41888ms ~ 47972ms (file: 320-372-123-215-173-136-175-130-123-671-103-653-105-160-317-321-376-361-327-352-675-763-765-756-756-237-253-723-376-357-176-231-263-573-657-376-367-120-135-173-130-103-765-103-2_W.mp3)
  Segment 13: 48972ms ~ 49548ms (file: 756-765-371-327-573-215-132-132-327-312-507 265-507-365-573-751-502-257-765-312-213-321-172-503-132-507-257-125-506-253-507-237-703-273-576-572-725-537-206-653-132-752-132-752-3_V.mp3)
  Segment 14: 50548ms ~ 58432ms (file: 203 103-561-321-231-652-537-572-154-715-132-123-735-376-173-623-120-103-517-756-321-126-216-126-123-360-203-130-753-715-760-126-102-120-210-123-321-231-103-123-126-231-132-231-2_W.mp3)
  Segment 15: 59432ms ~ 60008ms (file: 157-750-173-357-750-170-150-715-731-715-753-507-130-137-103-107-157-157-571-571-751-751-713-705 571-751-571-571-517-107-175-175-751-175-750-750-735-750-751-107-175-571-157-153-7_W.mp3)
  Segment 16: 61008ms ~ 61584ms (file: 173-317-736-675-671-657-637-673-675-160-576-573-105-153-506-567-507-576-675-765-765-762-765-765-765-765-765-135-135_W.mp3)
  Segment 17: 62584ms ~ 63160ms (file: 267-765-675-765-671-362-137-316-612-765-765-613-627-627-623-261-163-673-672-623-673-672-623-623-763-675-765-672-672-637-672 321 325-213-352-321-312-536-136-571-571-571-517-157-5_W.mp3)
  Segment 18: 64160ms ~ 64736ms (file: 627-621-762-265-572-623-325-321-507-725-625-213-716-527-572-716-713-302 560-236-273-123-265-263-573-265-570-713-371-713-731-736-162-765-765-572-325-756-276-756-167-236-231-610-6_V.mp3)
  Segment 19: 65736ms ~ 74556ms (file: 213-132-132-517-523-512-256-123-213-576-236-127-627-627-126-160-301-236-765-672-627-257-236-263-260-623-321-132-236-326-312-123-715-236-372-627-276-102-236-273-120-216-312-320-1_W.mp3)
  Segment 20: 75556ms ~ 76132ms (file: 316-321-312-231-657-576-715-756-571-357-326-320-123-162-765-703-123-321-372-172-752-251-315-325-705-732-217-123-132-317-715-317-750-265-612-570-253-612-520-312-507-751-756-162-3_W.mp3)
  Segment 21: 77132ms ~ 77708ms (file: 572-571-135-517-571-157-175-152-715-517 167-376-137-132-317-713-756-736-756-375-326-362-631-613-132-317-136-316-326-316-361-316-316-132-132-312-132-123-173-726-731-756-713-751-7_W.mp3)
  Segment 22: 78708ms ~ 79284ms (file: 267-306-725-265-571-371-702-276-265-532-623-263-261-756-257-275-276-206-267-752-572-673-236-627-213-321-526-257-756-137-725-276-507-237-726-267-276-527-275-235-265-263-560-765-2_V.mp3)
  Segment 23: 80284ms ~ 80860ms (file: 627-762-726-261-623-326-237-372-672-673-756-752-375-635-763-576-357-321-317-312-327-237-371-321-137-352-315-321-376-327-362-375-317-731-317-132-321-321-273-273-276-327-376-316-1_W.mp3)
  Segment 24: 81860ms ~ 82436ms (file: 602-605-602-620-602-620-620 351-627-602-603-310-375-372-162-362-206-741-264-602-621-132-326_V.mp3)
  Segment 25: 83436ms ~ 84012ms (file: 175-572-507-267-527-751-173-276-572-571-235-321-671-762-671-672-176-167-137-163-175-175-153-153-531-317-763-715-612-763-723_W.mp3)
  Segment 26: 85012ms ~ 85588ms (file: 312-130-675-103-320-256-217-761-137-163-756-372-213-157-716-132-276-316-130-310-301-130-310-135-135-135-315-135-315-153-153-150-175-105-130-103-135-517-175-135-130-103-310-137-3_W.mp3)
  Segment 27: 86588ms ~ 87164ms (file: 572-271-270-702-574-574 312-315-607-315-267-275-725-207-275-602-326-302-753-756-675-673-572-567-571-107-137-735-751-276-273-572-532-576-765-173-357-320_W.mp3)
  Segment 28: 88164ms ~ 105912ms (file: 103-267-260-207-267-756-765-275-542-571-301-754-574-527-231-207-524-275-267-267-572-567-710-213 712-213-201-730-172-173-260-267-230-572-210-103-657-732-702-276-132-210-546-210-2_W.mp3)
  Segment 29: 106912ms ~ 107488ms (file: 705 750-157-751-701-107-710-715-705-175-756-571-751-705-150-750-751 756-576-576-756-576-756-756-576-576-576-571-715-701-107-175-175-715-712-170-715-705-701-751-701-715-701-107-7_V.mp3)
  Segment 30: 108488ms ~ 114320ms (file: 207-321-230-213-231-230-231-325-230-103-137-102-723-370-370-376-137-312-143-137-624-206-103-123-327-702-723-716-370-720-672-607-607-672-276-276-267-203-237-276-236-326-260-316-3_W.mp3)
  Segment 31: 115320ms ~ 115896ms (file: 365-276-312-103-236-576-507-756-327-236-216-507-527-263-207-326-652-321-261-267-732 317-741-702-513-176-752-351-705-257-126-275-570-312-725-317-276 276-752-756-231-572-273-632-2_V.mp3)
  Segment 32: 116896ms ~ 117472ms (file: 637-657-563-301-351-672-306-321-312-130-130-753-765-675-527-576-130-735-316-672-632-326-136-162-761-750-756-765-271-327-372-327-160-315-351-576-726-762-762-275-725-376-372-672-7_W.mp3)
  Segment 33: 118472ms ~ 125888ms (file: 217-276-267-726-567-201-762-761-712-102-126-567-265-123-102-102-201-201-126-123-124-261-216-172-120-162-671-176-201-260-206-207-627-267-206-261-126-107-756-571-726-260-126-207-2_V.mp3)
  Segment 34: 126888ms ~ 127464ms (file: 263-267-756-576-672-762-756-756-672-415-261-261-201-130-316-361-326-267-627-315-317-547-657-327-107-362-263-267-103-103-267-267-627-546-547-267-627-627-201-126-627-624-267-103-3_W.mp3)
  Segment 35: 128464ms ~ 129040ms (file: 157-125-152-125-152-125-157-105-107-107-105-105-157-715-103-176-105-106-160-106 132-132-753-570-571-571-517-517-157-105-107-175-127-175-705-710-751-571-570-571-510-751 571-571-5_V.mp3)
  Segment 36: 130040ms ~ 136844ms (file: 230-725-325-310-310-617-126-236-237-275-371-651-123-735-276-265-260-326-316-103-203-206-753-320-203-260-627-620-756-765-675-265-250-320-263-320-210-756-231-267-263-762-265-260-2_W.mp3)
  Segment 37: 137844ms ~ 138420ms (file: 172-526-630-576-673-367-317-735-357-356-357-537-537-567-572-576-576-536-753 673-617-167-613-617 576-570-576-537-517-530-537-571-571-567-567-675-756-673-765-760-673-760-761-261-6_W.mp3)
  Segment 38: 139420ms ~ 145576ms (file: 751-176-321-170-756-762-132-137-256-706-762-723-263-207-720-371-376-762-267-571-267-173-351-756-576-107-130-672-267-267-267-762-263-267-106-715-721-267-267-276 762-172-576-715-1_W.mp3)
  Segment 39: 146576ms ~ 147152ms (file: 573-571-715-710-571-710-150-715-571-750-107-175-751 571-751-750-157-751-751-715-710-170-701-103-715-107-175-371-123-150-173-175-135-705-305-715-750 517-751-751-750-750-751-750-7_W.mp3)
  Segment 40: 148152ms ~ 148728ms (file: 753-715-756-736-716-172-175-176-751-157-157-751-572-751-572-375-753-736-521-157-751-751-571-573-576-157-157-751-756-756-750-574-754-572-750-756-756-571-752-725-675-763-576-750-7_W.mp3)
  Segment 41: 149728ms ~ 150304ms (file: 132-173-156-153-163-162-136-136-163-102-143-134-103-106-103-103-143-104-134-146-146-143_V.mp3)
  Segment 42: 151304ms ~ 158432ms (file: 203-627-263-107-103-726-102-672-213-265-120-123-126-260-267-310-267-206-312-236-723-123-321-236-231-267-267-321-236-236-120-201-321-236-253-732-123-132-210-762-126-612-617-236-7_W.mp3)
  Segment 43: 159432ms ~ 169548ms (file: 231-320-102-106-167-106-617-106-103-213-312-320-321-623-263-203-231-215-501-216-127-236-263-206-130-301-230-201-230-756-675-602-260-203-230-321-231-271-310-302-320-756-765-752-2_W.mp3)
  Segment 44: 170548ms ~ 179044ms (file: 236-230-320-630-167-603-102-137-320-236-623-231-273-320-130-710-136-106-130-132-760-376-326-267-327-231-103-310-103-102-150-312-312-134-103-132-310-132-321-320-231-230-326-203-1_W.mp3)
  Segment 45: 180044ms ~ 180620ms (file: 613-351-126-312-301-321-325-352-256-527-527-560-236-201-213-507-261-576-732-326-362-321-231_V.mp3)
  Segment 46: 181620ms ~ 191124ms (file: 217-321-310-103-201-120-627-103-102-267-275-263-753-120-127-756-216-261-103-231-270-206-201-123-756-756-720-217-620-206-126-216-217-706-126-207-620-672-627-627-261-754-675-263-1_W.mp3)
  Segment 47: 192124ms ~ 192700ms (file: 570-207-217-627-235-705-753-325-236-536-725-572-257-267-736-312-762-752-231-326-621-752-236-132-321-365-325-627-570-625-235-657-504-325-726-725-275-570-726-312-675-526-152-752-3_V.mp3)
  Segment 48: 193700ms ~ 207596ms (file: 126-310-231-321-312-132-310-310-672-213-216-215-257-213-320-230-321-312-107-312-320-231-267-627-760-207-276-130-163-173-271-237-236-103-267-327-572-572-675-172-127-312-132-137-1_W.mp3)
  Segment 49: 208596ms ~ 209172ms (file: 527-135-135-157-375-756-375-673-351-137-316-136-163-136-316-163-136-136-613-561-365-651-536-573-573-365-651-651-567-576-576-576-567-567-532-135-351-351-532-356-165-567-536-561-6_W.mp3)
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
> **Time**: 2026-04-06 07:58:24
> **AI**: gemini

---

## Response

{
"segment": 1,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 2,
"transcript": "안녕하세요 만나서 반갑습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 150, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "만나서", "start_ms": 1450, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 2350, "dur_ms": 1100, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 4,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 5,
"transcript": "오늘 날씨가 정말 화창하고 좋네요 산책 가고 싶어요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 750, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 1500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "화창하고", "start_ms": 2100, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 3100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "산책", "start_ms": 4200, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "가고", "start_ms": 4800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "싶어요", "start_ms": 5300, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "외국어 공부는 꾸준히 하는 것이 가장 중요합니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "외국어", "start_ms": 180, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "공부는", "start_ms": 950, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "꾸준히", "start_ms": 1650, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 2400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "것이", "start_ms": 2900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 3450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "중요합니다", "start_ms": 4050, "dur_ms": 1000, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 8,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 9,
"transcript": "영어나 일본어도 섞어서 연습해 보세요 효과가 좋습니다",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 220,
"audio_type": "speech",
"words": [
{"word": "영어나", "start_ms": 220, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "일본어도", "start_ms": 950, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "섞어서", "start_ms": 1850, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "연습해", "start_ms": 2550, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "보세요", "start_ms": 3350, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "효과가", "start_ms": 4200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "좋습니다", "start_ms": 4900, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 11,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 12,
"transcript": "Learning a new language is always a challenge but fun",
"speaker": "speaker_2",
"confidence": 0.92,
"language": "en",
"silence_before_ms": 300,
"audio_type": "speech",
"words": [
{"word": "Learning", "start_ms": 300, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 950, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "new", "start_ms": 1100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "language", "start_ms": 1500, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 2300, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "always", "start_ms": 2550, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 3150, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "challenge", "start_ms": 3300, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "but", "start_ms": 4200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "fun", "start_ms": 4600, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 14,
"transcript": "매일 조금씩이라도 듣고 말하는 연습을 반복하는 게 좋죠",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "매일", "start_ms": 150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "조금씩이라도", "start_ms": 650, "dur_ms": 950, "phase_lr_deg": 0.0},
{"word": "듣고", "start_ms": 1700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "말하는", "start_ms": 2200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "연습을", "start_ms": 2900, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "반복하는", "start_ms": 3650, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 4500, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "좋죠", "start_ms": 4800, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 16,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 17,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 18,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 19,
"transcript": "가끔은 노래를 통해서 배우는 것도 아주 좋은 방법입니다",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "가끔은", "start_ms": 250, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "노래를", "start_ms": 900, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "통해서", "start_ms": 1600, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "배우는", "start_ms": 2350, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "것도", "start_ms": 3000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 3500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 4000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "방법입니다", "start_ms": 4550, "dur_ms": 1100, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 21,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 22,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 23,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 24,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 25,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 26,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 27,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 28,
"transcript": "자신감을 가지고 크게 소리 내어 읽어보는 습관을 들여보세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "자신감을", "start_ms": 200, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "가지고", "start_ms": 1100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "크게", "start_ms": 1800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "소리", "start_ms": 2400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "내어", "start_ms": 2900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "읽어보는", "start_ms": 3400, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "습관을", "start_ms": 4250, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "들여보세요", "start_ms": 5000, "dur_ms": 900, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 30,
"transcript": "그러면 어느새 실력이 쑥쑥 늘어난 것을 느끼실 수 있을 거예요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "그러면", "start_ms": 150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "어느새", "start_ms": 750, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "실력이", "start_ms": 1450, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "쑥쑥", "start_ms": 2150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "늘어난", "start_ms": 2750, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "것을", "start_ms": 3500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "느끼실", "start_ms": 4000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 4700, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있을", "start_ms": 4900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 5350, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 32,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 33,
"transcript": "포기하지 말고 끝까지 화이팅 하세요 응원하겠습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "포기하지", "start_ms": 200, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "말고", "start_ms": 1000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "끝까지", "start_ms": 1550, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "화이팅", "start_ms": 2300, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "하세요", "start_ms": 3000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "응원하겠습니다", "start_ms": 3800, "dur_ms": 1100, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 35,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 36,
"transcript": "다음에 또 만나요 안녕히 계세요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 120, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 700, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 1000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 1800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "계세요", "start_ms": 2500, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 38,
"transcript": "See you next time, goodbye everyone!",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 600, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "next", "start_ms": 850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "time,", "start_ms": 1300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "goodbye", "start_ms": 2000, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "everyone!", "start_ms": 2750, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 40,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 41,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 42,
"transcript": "꾸준함이 답입니다 오늘도 수고 많으셨습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "꾸준함이", "start_ms": 180, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "답입니다", "start_ms": 1050, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "오늘도", "start_ms": 2000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "수고", "start_ms": 2700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "많으셨습니다", "start_ms": 3200, "dur_ms": 1000, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "여러분의 꿈을 위해 항상 노력하는 모습이 아름답습니다",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 220,
"audio_type": "speech",
"words": [
{"word": "여러분의", "start_ms": 220, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "꿈을", "start_ms": 1000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "위해", "start_ms": 1500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "항상", "start_ms": 2000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "노력하는", "start_ms": 2600, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "모습이", "start_ms": 3450, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "아름답습니다", "start_ms": 4150, "dur_ms": 1100, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "어려운 점이 있다면 언제든 물어보세요 도와드릴게요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "어려운", "start_ms": 150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "점이", "start_ms": 800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "있다면", "start_ms": 1300, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "언제든", "start_ms": 2000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "물어보세요", "start_ms": 2700, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "도와드릴게요", "start_ms": 3700, "dur_ms": 900, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 46,
"transcript": "함께 배우고 성장하는 기쁨을 느껴보시기 바랍니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "함께", "start_ms": 200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "배우고", "start_ms": 800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "성장하는", "start_ms": 1500, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "기쁨을", "start_ms": 2350, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "느껴보시기", "start_ms": 3050, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "바랍니다", "start_ms": 4050, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 48,
"transcript": "마지막까지 집중해서 들어주셔서 감사합니다 즐거운 하루 되세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "마지막까지", "start_ms": 180, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "집중해서", "start_ms": 1150, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "들어주셔서", "start_ms": 2000, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 2900, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "즐거운", "start_ms": 4000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "하루", "start_ms": 4700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "되세요", "start_ms": 5200, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 07:58:24*
