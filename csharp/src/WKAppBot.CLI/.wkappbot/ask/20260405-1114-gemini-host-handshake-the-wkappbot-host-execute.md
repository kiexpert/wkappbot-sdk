# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=24268  time=11:12:56  uptime=36193s  mem=486MB  system_processes=353"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_110802.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 107-317-132-132-317-137-301-137-173-310-130-137-173-310-130-703-137-137-317 315-137-315-130-130-312-103-317-137-173-317-103-301-137-310-310-130-317-103-137-310-137-132-301-137-3_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 150-150-517-571-501-517-501-510-501-150-501-510-501-501-507-570-501-510-150-510-517-501-571-517-571-574-574-571-517 501-507-501-517-501-517-157-153-510-501-501-150-510-510-510-1_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 163-160-163-613-106-716-173-103-106-107-107-673-106-103-310-301-617-671-620-567-576-576-576-326-623-632-620-362-106-106-103-310-213-321-107-317-107-310-310-301-163-106-106-106-3_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 572-576-756-576-756-752-752-541-543-541-504-541-546-506-504-504-501-546-514-504-504-506-541-752-752-756-756-756-576-576-756-546-547-516-541-512-541-526-567-563-564-536-562-752-7_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 175-715-713 157-751-715-170-710-715-715-570-715-751-756-175-751-713-137-175-571-715-157-751-715-173-137-715-753 750-570-753-751-751-701-713-571-715-710-173-753-713-751-570-175-7_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 276-257-205-132-521-765 132-652-576-576-750-367-632-576-756-567-562-576-572-576-652-627 562-571-576-627-326-251-157-574-576-574-576-756-132-127 657 132-135-105-135-103-231-230 7_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 235-715-572-371-324-123-103-206-157-517-750-260-327-136-135-637-267-506-754-760-751-752-542-176-731-623-765-327-301-312-230-321-325-312-237-705-756-576-276-267-523-235-123-512-2_V.mp3)
  Segment 8: 11032ms ~ 21040ms (file: 267-127-102-765-756-675-267-276-267-236-237-237-172-123-237-751-756-176-761-270-132-726-217-627-102-732-312-321-672-106-102-103-321-372-107-136-120-736-137-731-731-317-102-321-3_W.mp3)
  Segment 9: 22040ms ~ 22616ms (file: 715-713-173-735-173-175-715-173-713-173-105 157-137-571-751-571-351-731-751-175-157-715-753-751-715-713-173-713-317-173-751-713-173-317-713-751 175-517-175-715-751-715-137-175-7_V.mp3)
  Segment 10: 23616ms ~ 30492ms (file: 765-576-267-213-107-130-103-130-756-106-103-571-756-321-321-216-136-257-231-261-261-576-135-312-367-316-361-213-263-321-357-165-160-261-623-173-107-317-136-236-610-715-267-312-2_W.mp3)
  Segment 11: 31492ms ~ 32068ms (file: 752-275-753-237-527-602-236-675-257-752-765-725-675-275-273-521-572-756-530-756-701 752-570-627-576-507-725-576-673-163-620-123-325-706-176-631-361-312-761-236-750-136-132-326-2_V.mp3)
  Segment 12: 33068ms ~ 33644ms (file: 216-201-321-136-301-312-132-167-106-260-203-320-301-307-312-623-627-321-132-751-751-106-103-310-213-302-203-230-163-103-103-102-132-203-216-310-310-321-203-302-302-320-326-326-6_V.mp3)
  Segment 13: 34644ms ~ 35220ms (file: 265-265-625-265-526-526-542-514-153-361-130-130-321-317-321-265-326-321-236-136-267-372-326-235-365-632-326-321-752-615-125-261-126-526-256-125-162-562-162-321-215-136-132-235-2_W.mp3)
  Segment 14: 36220ms ~ 36796ms (file: 321-327-123-123-130-160-160-316-123-132-261-162 137_W.mp3)
  Segment 15: 37796ms ~ 38372ms (file: 612-167-702-267-301-312-715-312-312-316-132-301-103-750-706-670-103-531-541-530-157-576-567-506-576-574-236-326-765-672-216-231-602-567-213-203-132-150-502-152-213-672-675-610-1_W.mp3)
  Segment 16: 39372ms ~ 39948ms (file: 765 130-103-731-371-301-307-375-170-371-370-103-310-103-713-302-107-107-103-301-107-301-317-310-753-103-350-375-103-107 175-173-173-176-137-307-107-710-137-103-317-731-137-310-1_V.mp3)
  Segment 17: 40948ms ~ 49012ms (file: 267-321-231-372-267-260-260-206-260-263-540-231-321-217-217-213-216-265-267-213-326-203-231-201-260-261-213-213-123-321-612-126-261-216-123-312-725-725-326-267-231-237-273-276-2_W.mp3)
  Segment 18: 50012ms ~ 67220ms (file: 201-103 103-375-130-231-132-750-201-237-102-102-132-103-765-310-302-320-207-103-132-413-203-301-104-103-230-132-302-236-203-203-123-102-230-103-103-301-301-132-310-312-134-203-1_V.mp3)
  Segment 19: 68220ms ~ 68796ms (file: 103 132-136-751-765-310-137-376-312-312-612-123 237-236-326-273-615-157-765-765-756-675-627-162-762-762-762-762-716-761-762-762-672-765-765-763-765_W.mp3)
  Segment 20: 69796ms ~ 70372ms (file: 625 625-620-602-260-260-625-625-261-625-625-265-625-627-625-625-265-625-652-625-652-652-625-653-652-652 621-620-620-621-261-621-621-267-625-265-625-625-627-265-625-265-627-625-6_W.mp3)
  Segment 21: 71372ms ~ 71948ms (file: 312-310-261-632-236-320-302-310-163-306-320-310-301-316-132-163-162-132-321-316 163-136-132-136-321-231-123-132-162-312-321-123-132-362-326-312-132-216-310-132-123-132-321-610 6_V.mp3)
  Segment 22: 72948ms ~ 73524ms (file: 651-651-561-651-615-561-561-156-561-165-516-156-561-156-561-561-156-651-561-561-615-561_W.mp3)
  Segment 23: 74524ms ~ 83272ms (file: 316-573-105-601-317-136-106-167-751-756-765-672-260-267-620-260-275-275-572-162-276-267-270-756-752-756-570-273-271-237-756-103-107-107-762-327-270-276-321-726-736-753-312-103-1_W.mp3)
  Segment 24: 84272ms ~ 84848ms (file: 105-517-570-705-105-571-571-570-507-571 574-574-574-574-754-571-517-157-175-170-571-750-574-754-571-157 574-570-571-150-507-570-571-574-574-574-574-574-574-574-574-571-517-571-5_W.mp3)
  Segment 25: 85848ms ~ 90132ms (file: 170-172-231-312-132-317-321-312-137-123-623-612-236-230-236-263-762-327-362-123-312-132-170-136-312-326-236-132-312-326-160-106-163-162-261-261-261-762-162-610-612-326-236-236-2_W.mp3)
  Segment 26: 91132ms ~ 91708ms (file: 137-135-103-137-317-175-310-301-310-103-301-137-103-310-753-312-103-371-731-137-130-710-317 315-103-137-713-301-310-107-130-137-301-372-170-107-137-107-130-170-310-713-701-173-7_V.mp3)
  Segment 27: 92708ms ~ 103472ms (file: 270-231-236-762-762-237-231-751-236-276-230-210-102-236-312-321-320-375-310-310-237-210-267-735-231-310-356-506-564-126-231-236-236-123-127-263-132-672-260-120-756-230-756-237-2_W.mp3)
  Segment 28: 104472ms ~ 108108ms (file: 213-217-217-126-123-213-271-213-217-231-213-213-231-213-120-123-231-213-217-213-231-213-210-213-231-271-213-231-231-132-231-231-123-231-213-237-213-231-213-276-231-123-217-217-2_W.mp3)
  Segment 29: 109108ms ~ 117352ms (file: 672-672-321-231-152-127-210-217-210-120-261-210-270-715-236-120-612-210-103-260-201-120-201-263-102-120-570-706-320-201-672-756-103-105-230-231-263-102-132-752-756-712-103-103-1_W.mp3)
  Segment 30: 118352ms ~ 118928ms (file: 751-751-571-751-715-751-753-756-570-570-153-517-157-173-157-175-715-571-751-751-175-751-157-517-175-157-571-175-576-576-571-751-105-105-150-157-571-150-501-510-165-150-513-156-1_W.mp3)
  Segment 31: 119928ms ~ 120504ms (file: 750-752-750-705-705-756 705-701-705-750-765 627-325-365-635 710-170-127-127_W.mp3)
  Segment 32: 121504ms ~ 130720ms (file: 231-132-263-231-267-263-265-132-312-103-236-103-576-267-267-263-672-126-267-260-321-542-231-352-537-261-231-103-132-275-752-213-263-267-267-231-201-132-276-231-103-263-321-167-5_W.mp3)
  Segment 33: 131720ms ~ 132296ms (file: 547-547-574-574-574-574-574-574-574-574-571 157-715-715-705-571-107-715-751-750-571-573-571 574-574-571-571 574-574-547-574-574-574-573-574-574-574-574-574-574-574-574-574-157 5_W.mp3)
  Segment 34: 133296ms ~ 133872ms (file: 657-567-657-756 517-571-501-705-715-750-752-751-261-761-215-275-125-127-527-175 102-123-125-517-576-571-572-571-517-570-570-572-570-512_V.mp3)
  Segment 35: 134872ms ~ 140848ms (file: 236-362-136-735-327-163-167-612-135-132-312-361-306-236-257-301-312-132-310-216-527-517-537-735-235-236-261-672-627-213-321-326-236-267-106-103-103-130-602-631-637-530-167-613-4_V.mp3)
  Segment 36: 141848ms ~ 142424ms (file: 163-130-176-167-136-612-107-236-216-236-172-127-127-127-103-102-671-136-310-312-623-136-130-136-760-517-163-132-103-135-163-312-157-610-763-613-136-160-130-517-163-130-156-157-6_V.mp3)
  Segment 37: 143424ms ~ 144000ms (file: 571-576-517-571-576-571-576-576-756-576 153-517-571-571-572-157-175-571-571-517-571-571-570-517-571-573 153-517-517-517-517-517-157-517-517-571-157-157-517_W.mp3)
  Segment 38: 145000ms ~ 145576ms (file: 536-513-531-531-512-571-571-507-570-570-507-502-750-765_V.mp3)
  Segment 39: 146576ms ~ 157124ms (file: 261-260-570-670-217-260-103-213-620-267-201-132-620-754-754-620-206-602-123-206-132-602-126-236-210-210-206-201-103-216-260-260-206-267-102-103-123-213-123-103-217-206-567-261-1_V.mp3)
  Segment 40: 158124ms ~ 164028ms (file: 362-321-623-637-176-623-623-321-327-137-617-175-153-175-157-756-637 172-176 136-103-132_W.mp3)
  Segment 41: 165028ms ~ 165604ms (file: 137-715-315-310-103-753-705-701 375-137-103-710-371-317-706-701-310-701-107-175-713-310-317-173-710-310-173-103-301-137-132-372-375-305-713-103-310-132-305-751-751-715 310-307-1_V.mp3)
  Segment 42: 166604ms ~ 167180ms (file: 570-765-367-567-156-137-672-327-576-573-135-765-217-573-237-631-705-136-602-250-213-271-752-132-765-275-327-576-512-635-632-256-512-567-127-725-526-261-702-207-130-316-367-107-6_W.mp3)
  Segment 43: 168180ms ~ 168756ms (file: 715-715-175-715-715-751-751-715-175-175-751 105-150-150-103-157-130-170-175-715-175-175-751-175-715-175-715-715-715-715-175-715-715-715-175 510-514-510-504-517-504-547-574-574-1_W.mp3)
  Segment 44: 169756ms ~ 170332ms (file: 107-170-710-751-175-517-157-175-170-175-570-517-501-570 570-571-750-570-571-571-751-157-715-157-175-570-157-170-137-150-710-175-750-157-705-157-150-573-570-750-570 153-513-150-1_W.mp3)
  Segment 45: 171332ms ~ 171908ms (file: 105-165-160-150 150-105-105-105-150-105-105-150-156-105-516-105-501-150 154-105-130-105-105-157-157-150-517-517-150 510-510-150-135-105-150-105-105-105-105-150 510-157-153-105-1_V.mp3)
  Segment 46: 172908ms ~ 173484ms (file: 301-130-134-103-134-143-134 345-431-435-315-410-135-134-137-103-310-413-104-104-103-103-130-130-310-104-143-143-134-143-143-130-103-134-103-134-301 345-510-451-154-104-103-103-3_V.mp3)
  Segment 47: 174484ms ~ 175060ms (file: 501-150-501-510-150-501-501-504-514-514-541_V.mp3)
  Segment 48: 176060ms ~ 176636ms (file: 710-761-706-710-701-170-751-756-750-701-705-710-701-710-701 176-107-175-173-107-716-170-107-705-716-701-701-170-701-710-170-170-170-701-751-750-570-175-715-701-710-701 715-710-7_V.mp3)
  Segment 49: 177636ms ~ 178212ms (file: 576-765-675-316-310-136-357-351-573-574-576-567-576-310-612-216-210-260-675-631-123-106-167-617-631_W.mp3)
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
> **Time**: 2026-04-05 11:14:27
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "안녕하세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 105,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 105, "dur_ms": 420, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "반갑습니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "반갑습니다", "start_ms": 80, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "오늘 날씨가 좋네요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 110, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "날씨가", "start_ms": 240, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "좋네요", "start_ms": 400, "dur_ms": 140, "phase_lr_deg": -5.0}
]
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
"transcript": "Where are you from?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Where", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "are", "start_ms": 210, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 300, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "from?", "start_ms": 410, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "저는 한국에서 왔어요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 90, "dur_ms": 100, "phase_lr_deg": 10.0},
{"word": "한국에서", "start_ms": 200, "dur_ms": 180, "phase_lr_deg": 10.0},
{"word": "왔어요", "start_ms": 390, "dur_ms": 150, "phase_lr_deg": 10.0}
]
},
{
"segment": 7,
"transcript": "감사합니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 120, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "천만에요 별말씀을요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "천만에요", "start_ms": 150, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "별말씀을요", "start_ms": 550, "dur_ms": 400, "phase_lr_deg": -5.0}
]
},
{
"segment": 9,
"transcript": "Excuse me",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Excuse", "start_ms": 40, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "me", "start_ms": 300, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "잠시만 기다려 주세요 바로 도와드릴게요",
"speaker": "speaker_3",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "잠시만", "start_ms": 200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "기다려", "start_ms": 650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 1050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 1500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "도와드릴게요", "start_ms": 1850, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "이것은 얼마인가요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "이것은", "start_ms": 100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "얼마인가요?", "start_ms": 320, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "만 원입니다",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "만", "start_ms": 130, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "원입니다", "start_ms": 250, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "너무 비싸요 깎아주세요",
"speaker": "speaker_1",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "너무", "start_ms": 80, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "비싸요", "start_ms": 240, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "깎아주세요", "start_ms": 450, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "안 돼요",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "안", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "돼요", "start_ms": 160, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "좋아요 살게요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "좋아요", "start_ms": 100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "살게요", "start_ms": 350, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "What is your name?",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "What", "start_ms": 60, "dur_ms": 100, "phase_lr_deg": -10.0},
{"word": "is", "start_ms": 170, "dur_ms": 50, "phase_lr_deg": -10.0},
{"word": "your", "start_ms": 230, "dur_ms": 120, "phase_lr_deg": -10.0},
{"word": "name?", "start_ms": 360, "dur_ms": 150, "phase_lr_deg": -10.0}
]
},
{
"segment": 17,
"transcript": "제 이름은 김철수입니다 만나서 반가워요",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "제", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이름은", "start_ms": 300, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "김철수입니다", "start_ms": 550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "만나서", "start_ms": 1050, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "반가워요", "start_ms": 1400, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "성함이 어떻게 되시나요? 저는 영희라고 해요",
"speaker": "speaker_2",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "성함이", "start_ms": 150, "dur_ms": 250, "phase_lr_deg": -10.0},
{"word": "어떻게", "start_ms": 450, "dur_ms": 200, "phase_lr_deg": -10.0},
{"word": "되시나요?", "start_ms": 700, "dur_ms": 300, "phase_lr_deg": -10.0},
{"word": "저는", "start_ms": 1100, "dur_ms": 200, "phase_lr_deg": -10.0},
{"word": "영희라고", "start_ms": 1350, "dur_ms": 350, "phase_lr_deg": -10.0},
{"word": "해요", "start_ms": 1750, "dur_ms": 200, "phase_lr_deg": -10.0}
]
},
{
"segment": 19,
"transcript": "잘 부탁드립니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "잘", "start_ms": 70, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "부탁드립니다", "start_ms": 180, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 21,
"transcript": "지금 몇 시예요?",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "지금", "start_ms": 110, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "몇", "start_ms": 280, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "시예요?", "start_ms": 370, "dur_ms": 150, "phase_lr_deg": 0.0}
]
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
"transcript": "오후 세 시 반입니다 수업 시작할 시간이에요",
"speaker": "speaker_3",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "오후", "start_ms": 140, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "세", "start_ms": 350, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "시", "start_ms": 460, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "반입니다", "start_ms": 570, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "수업", "start_ms": 1000, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "시작할", "start_ms": 1250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "시간이에요", "start_ms": 1600, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 25,
"transcript": "교과서 십 페이지를 펴세요",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "교과서", "start_ms": 100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "십", "start_ms": 420, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "페이지를", "start_ms": 510, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "펴세요", "start_ms": 880, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "따라 읽어 보세요",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "따라", "start_ms": 90, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "읽어", "start_ms": 250, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "보세요", "start_ms": 380, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "선생님 질문 있습니다 이 단어는 어떻게 읽나요?",
"speaker": "speaker_4",
"confidence": 0.91,
"language": "ko",
"silence_before_ms": 160,
"audio_type": "speech",
"words": [
{"word": "선생님", "start_ms": 160, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "질문", "start_ms": 450, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 680, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 1100, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "단어는", "start_ms": 1200, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 1500, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "읽나요?", "start_ms": 1750, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "그건 사과라고 읽어요 Apple이라는 뜻이죠",
"speaker": "speaker_3",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "그건", "start_ms": 120, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "사과라고", "start_ms": 280, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "읽어요", "start_ms": 600, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "Apple이라는", "start_ms": 950, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "뜻이죠", "start_ms": 1500, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "아하 그렇군요 이해했습니다 감사합니다",
"speaker": "speaker_4",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "아하", "start_ms": 100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "그렇군요", "start_ms": 260, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "이해했습니다", "start_ms": 550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 1050, "dur_ms": 400, "phase_lr_deg": 0.0}
]
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
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 32,
"transcript": "화장실이 어디에 있나요? 직진해서 우회전하세요",
"speaker": "mixed",
"confidence": 0.89,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "화장실이", "start_ms": 150, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "어디에", "start_ms": 550, "dur_ms": 250, "phase_lr_deg": 5.0},
{"word": "있나요?", "start_ms": 850, "dur_ms": 250, "phase_lr_deg": 5.0},
{"word": "직진해서", "start_ms": 1300, "dur_ms": 400, "phase_lr_deg": -10.0},
{"word": "우회전하세요", "start_ms": 1750, "dur_ms": 600, "phase_lr_deg": -10.0}
]
},
{
"segment": 33,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 34,
"transcript": "도와주세요!",
"speaker": "speaker_5",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "도와주세요!", "start_ms": 40, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "무슨 일 이신가요? 길을 잃어버렸어요",
"speaker": "mixed",
"confidence": 0.90,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "무슨", "start_ms": 120, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "일", "start_ms": 280, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이신가요?", "start_ms": 400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "길을", "start_ms": 850, "dur_ms": 200, "phase_lr_deg": 20.0},
{"word": "잃어버렸어요", "start_ms": 1100, "dur_ms": 500, "phase_lr_deg": 20.0}
]
},
{
"segment": 36,
"transcript": "지도를 보여드릴게요 여기 현재 위치입니다",
"speaker": "speaker_6",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "지도를", "start_ms": 130, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "보여드릴게요", "start_ms": 400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "여기", "start_ms": 950, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "현재", "start_ms": 1150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "위치입니다", "start_ms": 1400, "dur_ms": 400, "phase_lr_deg": 0.0}
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
"transcript": "정말 친절하시네요",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 60, "dur_ms": 200, "phase_lr_deg": 20.0},
{"word": "친절하시네요", "start_ms": 280, "dur_ms": 400, "phase_lr_deg": 20.0}
]
},
{
"segment": 39,
"transcript": "천만에요 조심히 가세요 안녕히 계세요",
"speaker": "mixed",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "천만에요", "start_ms": 150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "조심히", "start_ms": 500, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 800, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 1200, "dur_ms": 300, "phase_lr_deg": 20.0},
{"word": "계세요", "start_ms": 1550, "dur_ms": 300, "phase_lr_deg": 20.0}
]
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
"transcript": "Can I have some water?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Can", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 160, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "have", "start_ms": 220, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "some", "start_ms": 330, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "water?", "start_ms": 460, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "여기 있습니다 시원한 물이에요",
"speaker": "speaker_6",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "여기", "start_ms": 110, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 280, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "시원한", "start_ms": 650, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "물이에요", "start_ms": 950, "dur_ms": 300, "phase_lr_deg": 0.0}
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
"transcript": "맛있게 드세요",
"speaker": "speaker_6",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "맛있게", "start_ms": 80, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "드세요", "start_ms": 350, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "오늘 점심 메뉴는 무엇인가요? 비빔밥입니다",
"speaker": "mixed",
"confidence": 0.91,
"language": "ko",
"silence_before_ms": 160,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 160, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "점심", "start_ms": 330, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "메뉴는", "start_ms": 500, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "무엇인가요?", "start_ms": 800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "비빔밥입니다", "start_ms": 1300, "dur_ms": 500, "phase_lr_deg": -10.0}
]
},
{
"segment": 47,
"transcript": "와 맛있겠네요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "와", "start_ms": 90, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "맛있겠네요", "start_ms": 200, "dur_ms": 350, "phase_lr_deg": 0.0}
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
"transcript": "다음에 또 봐요 잘 가요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 100, "dur_ms": 180, "phase_lr_deg": -5.0},
{"word": "또", "start_ms": 300, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "봐요", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "잘", "start_ms": 650, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "가요", "start_ms": 780, "dur_ms": 150, "phase_lr_deg": -5.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 11:14:27*
