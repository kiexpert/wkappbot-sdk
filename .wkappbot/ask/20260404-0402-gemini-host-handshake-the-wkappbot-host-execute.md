# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=29532  time=04:01:11  uptime=8965s  mem=1122MB  system_processes=365"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260404_035910.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 320-132-132-123-203-321-325-123-532-351-371-302-672-672-732-752-321-325-213-513-351-153-513-517-175-236-132-132-102-312-321-316-632-632-635-361-653-657-135-301-312-312-357-563-5_W.mp3)
  Segment 2: 1576ms ~ 9640ms (file: 756-756-576-620-276-261-263-627-621-132-621-627-267-267-157-574-573-216-261-216-213-103-320-130-327-137-731-310-316-261-315-136-513-103-276-523-574-267-267-103-267-753-753-765-3_W.mp3)
  Segment 3: 10640ms ~ 11216ms (file: 631-132-657-570 617-162-612-162-261-362-671 672 562-652-163 301-136-312-216-136-136-631-165-103-175-163-162-613-167-163-163-671-603-761-175-106-760-107-126-106-123-162-106-136-1_V.mp3)
  Segment 4: 12216ms ~ 12792ms (file: 361-621-136-165-106-136-105-106-612-617-167-156-136-106-301-301-360-320-130-320-306-306-320-230-263-623-306-320-302-302-132-267-310-103-103-103-163-135-705-320-301-302-731-371-2_V.mp3)
  Segment 5: 13792ms ~ 23152ms (file: 201-567-207-310-130-357-765-107-735-574-501-753-130-103-750-270-321-310-276-715-310-753-130-571-106-165-163-103-107-175-573-765-752-302-103-103-732-107-103-130-534-103-164-756-7_W.mp3)
  Segment 6: 24152ms ~ 24728ms (file: 765-765-763-763-751-756-760-672 765-761-713_W.mp3)
  Segment 7: 25728ms ~ 26304ms (file: 675-675-617-671 673-675-673-675-103-167-106-163-136-136-162-103-135-615-126-132-621-167-103-163-316-156-613-601-167-103-160-132-103-160-162-613-617-103-263_W.mp3)
  Segment 8: 27304ms ~ 27880ms (file: 576-576-576-576-567-576-675-576-576-576-576-576 567-576-576-576-576-576-576-657-567-576-576-576-576-576-576-567-576-675-675-675-675-675-675-567-576-567-567-576-576-576-576-576-5_W.mp3)
  Segment 9: 28880ms ~ 39320ms (file: 762-267-216-321-263-231-312-613-761-361-316-123-213-130-271-106-130-130-172-627-276-267-267-103-216-236-321-231-132-123-263-130-312-756-267-267-276-267-267-137-106-715-756-107-1_W.mp3)
  Segment 10: 40320ms ~ 40896ms (file: 326-231-132-236-310-625-265-267-236-263-236-326-623-623-267-726-123-136-132-612-672-627-670-675-706-765-576-756-756-627-167-267-132-102-136-106-162-167-612-263-230-723-735-310-3_W.mp3)
  Segment 11: 41896ms ~ 42472ms (file: 652-156-167-165-657-671-163-617-657-167-567-162-176-106-136-715-132-106-103-106-716-263-625-265-236-263-567-265-625-103-103-103-102-132-760_W.mp3)
  Segment 12: 43472ms ~ 44048ms (file: 132-312-176-132-371-732-627-321-106-537-317-137-312-312-132-120-361-163-123-215-153-136 135-132-123-132-761-675-671-765-136-263-130-613-312-103-136-167-136-136-103-132-751-623-6_V.mp3)
  Segment 13: 45048ms ~ 45624ms (file: 163-132-132-136-103-132-103-516-316-167-103-103-154-157-153-163-103-361-106-103-132-135-173-103-103-136-567-167-561-517-315-136-103-203-316-130-136-170-103-103-103-267_V.mp3)
  Segment 14: 46624ms ~ 47200ms (file: 260-102-321-103-123-132-260-260-260-312-301-310-216-310-130-170-731-307-301-627-623-162-216-106-631-613-316-306-620-630-632-632-163-613-163-167-162-163-163-106-136-127-765-106-2_V.mp3)
  Segment 15: 48200ms ~ 48776ms (file: 157-167-135-106-167-106-301-512-130-132-106-136-632-162-213 341-126-103-162-126-132-726-173-143-312-413-312-326_V.mp3)
  Segment 16: 49776ms ~ 53736ms (file: 321-706-260-236-231-673-725-236-156-312-621-637-136-231-316-132-163-136-316-316-130-617-631-613-617-621-637-617-637-617-671-631-136-327-317-176-310-320-765-632-362-106-136-123-6_W.mp3)
  Segment 17: 54736ms ~ 55312ms (file: 130-103-316-132-136-157-136-106-130-601-103-163-106-167-103-103-103-126-132-136-615-156-516-103-103-261-162-124-671-671-167-573-136-103-103-103-130-576-326-162-751-126-176-562-7_V.mp3)
  Segment 18: 56312ms ~ 65600ms (file: 203-102-132-607-670-671-670-607-671-120-210-765-672-716-237-230-102-765-762-706-761-312-623-206-213-123-132-310-106-361-134-712-130-310-130-103-236-103-103-127-276-302-321-126-6_W.mp3)
  Segment 19: 66600ms ~ 72180ms (file: 267-627-267-231-106-671-762-267-216-107-176-761-362-137-312-261-267-217-217-127-123-126-672-621-672-162-612-167-126-160-123-162-162-167-176-716-762-726-617-571-571-571-571-571-7_W.mp3)
  Segment 20: 73180ms ~ 73756ms (file: 103 162-103-157-102-167-165-103-263-236-627-123-326 652-167-103-123-312-136-137-567-576-561-725-175-623-672-315-752-627-576-573-256-752-257-265-625-572-762-275-123-267-567-752-2_V.mp3)
  Segment 21: 74756ms ~ 75332ms (file: 132-672-627-672-637-671-673-673-672-216-637 754-312-215-241-513-143-321-754-576-756-236-260-263-623-620-263-261-136-136-132-316 756-762-752-672-763-165-163-567-613-673-375-615-6_W.mp3)
  Segment 22: 76332ms ~ 76908ms (file: 576-576-576-576-576-576-576 675-657-576-576-567-576-576-576-576-576-576-567-576-675-675-675 567-576-567-576-576-576-576-576 657-657-576-576-567-576-576-576-576-576-756-576-576-7_V.mp3)
  Segment 23: 77908ms ~ 78484ms (file: 627-132-231-653-671-651-132-623 216-321 136 126-162 167-157-162-621-132-607-103-103-316-103-612-162-163-167-163-176-175-136-123-312-137-612-106-136-176-106-172 362-317-215 106-1_V.mp3)
  Segment 24: 79484ms ~ 80060ms (file: 506-154-152-672-612-612-617-621-671-621-263-260-206-562-216-132-620-620-627-627-312-321-316-316-301-316-362-607-627-267-206-263-203-203-231-103-136-136-132-136-132 756-753-765-6_W.mp3)
  Segment 25: 81060ms ~ 89664ms (file: 165-310-163-106-163-136-671-716-642-165-167-671-572-162-756-765_V.mp3)
  Segment 26: 90664ms ~ 101788ms (file: 230-203-261-527-254-203-213-756-236-627-236-672-216-261-163-261-216-231-126-217-701-120-103-132-752-275-765-267-675-276-132-102-103-263-376-106-103-123-106-126-132-301-103-712-1_W.mp3)
  Segment 27: 102788ms ~ 103364ms (file: 162-136-132-326-136-312-672-217-721-162 627 621_W.mp3)
  Segment 28: 104364ms ~ 104940ms (file: 267-625-627 627-163 167-261-621-267-567-621-620-601-512-132-120-675-176-213-216-261-132-263-312-126-162-123_W.mp3)
  Segment 29: 105940ms ~ 106516ms (file: 132-163-107-137-610-621-162-621-163-106-315-632-165-160-132-103-103-167-106-567-136-160-176-132-176-623-612-320-176-172-103-123-167 176_V.mp3)
  Segment 30: 107516ms ~ 108092ms (file: 302-326-362-627-326-257-156-132-451 172-705-675-754-756-276-576-547-574-754-175-216-236_W.mp3)
  Segment 31: 109092ms ~ 109668ms (file: 130-167-132-103-620-260-206-102-106-162-132-602-206-361-210-203-230-267 620-670-672-607-672-627-627-602-102-216-126-621-163-106-136-103-267-267-263-130-162-263-632-136-103-103-6_V.mp3)
  Segment 32: 110668ms ~ 111244ms (file: 652-136-561-153-536-576-567-132-361-513-357-356-561-531-657-315-567-576-567-567-576-576-576-576-576-567-576-567-576-576-567-576-567-567-576-567-567-567-576-576-567-567-576-567-5_W.mp3)
  Segment 33: 112244ms ~ 112820ms (file: 125-651-657 163-125-652-621-160-103-123-163-610-632-103-130-760-132-567-360-637-132-130-136-312-123-210-265-162-631-312-103-103-516-321-673-617-632-132-167-136-167-132-617-312-6_V.mp3)
  Segment 34: 113820ms ~ 114396ms (file: 130-163-136-163-130-103-763-765-675-623-236-326-301-316-620-316-302-302-163-126-516-156-132-312-123-126-263-216-123-132-127-571-175-765-576-756-725-310-163-316-312-130-632-362-2_V.mp3)
  Segment 35: 115396ms ~ 115972ms (file: 320-320-302-301-320-236-260-162-601-612-162-612-615-126-102-163-162-163-321-312-163-162-216-206-230-301-102-673-163-136-103-102-132-136-136-136-306-263-203-321-213-326-312-132-7_V.mp3)
  Segment 36: 116972ms ~ 117548ms (file: 675-675-657-756-756-572-652-537-567-163-132-231-265-617-657-657-567-576-567-567-130-132-106-365-630-361-136-167-103-160-136-312-631-310-136-103-163-153-136-715-136-105-617-316-1_W.mp3)
  Segment 37: 118548ms ~ 119124ms (file: 627-103-312-301-317-107-763-627-316-326-236-673-607-627-573-603-620-316-135-503-326-203-102-620-216-123-132-126-672-321-263-765-756-756-362-136-130-103-310-130-302-320-320-203-3_V.mp3)
  Segment 38: 120124ms ~ 120700ms (file: 316-765-675-765-765-765-765-673-765-657-765-675-576-765-675-576-756-675-675-675-675-657-675-675-765-675-576-675-567-576-576-567-576-627-265-627-627-762-672-627-756-576-675-576-7_W.mp3)
  Segment 39: 121700ms ~ 122276ms (file: 316-103-321-136-547-574-610-612-631-102-261-123-312-310-135-617-163-623-612-653-631-231-126-132-132-103-153-160-361-613-317-631-136-136-103-163-361-312-103-301-103-157-612-162-3_V.mp3)
  Segment 40: 123276ms ~ 128568ms (file: 103-103-132-136-312-301-310-310-230-206-302-103-123-103-132-312-132-263-206-206-513-315-136-601-613-612-136-260-263-302-301-321-132-537-317-103-261-213-123-320-361-301-162-320-2_V_stt_who_know_i.mp3)
  Segment 41: 129568ms ~ 130144ms (file: 267-312-321-325-132-723-721-271-712-132-172-173-173-172-261-206-236-126-326-321-123-312-230-203-231-132-321-231-321-312-312-132-172-132-213-172-751-231-621-570-120-132-263-132-3_V.mp3)
  Segment 42: 131144ms ~ 131720ms (file: 321-231-263-326-263-265-765-765-176-507-765-576-312-306-326-312-316-132-103-312-136-137-132-162-167-570-357-135-132-132-765-765-760-675-607-652-612-567-657-567-756-560-132-127-5_V.mp3)
  Segment 43: 132720ms ~ 133296ms (file: 310-130-235-237-231-675-671-651-162-620-672-672-607-702-762-541-503-135 321-321-321-705-705-762-123-132-136-170-107-126-176-231-265-173-132-167-167-132 132-326-623-725-357-637-3_W.mp3)
  Segment 44: 134296ms ~ 134872ms (file: 756-753-570-635-756-576-576-576-756-756-576-675-756-765-765-756-765-756-762-672-765-256-125-105-105-125-123-213-215-215-725-725-726-732-756-567-205-265-265-750-570-576-760-765-3_W.mp3)
  Segment 45: 135872ms ~ 136448ms (file: 316-316-316-316-317-316-367-312-316-163-163-136-163-316-136-673-167-371-136-371-327-372-132-321-362-321-236-126-621-326-216-123-321_W.mp3)
  Segment 46: 137448ms ~ 138024ms (file: 326-631-162-130-317-316-137-231-123-137-175-127-107-170-170-137-623-306-567-153-103-163-675-126-567-653-651-615-567-657-506-561-657-176-527-256-576-572-576-517-765-615-167-756-6_W.mp3)
  Segment 47: 139024ms ~ 139600ms (file: 326-236-207-260-260-206-260-260-260-270-276-265-263-267-625-320-201-102-705-571-275-207-201-206-206-206-370-106-103-136-103-710-761-103-162-216-261-176-761-706-176-137-107-134-1_V.mp3)
  Segment 48: 140600ms ~ 141176ms (file: 132-123-312 126-316-263-362-613-632-276-672-726-627-621-675-627-672-726-672-267-726-675-675-574-527-765-765-765-176-675-675-675-627-672-672-675-672-672-672-671-672-756-576-765-7_W.mp3)
  Segment 49: 142176ms ~ 142752ms (file: 613-312-160-106-623-612-162-267-106-672-716-157-507-130-725-176-136-312-317-163-136-153-103-312-132-103-132-107-167-105-156-517-531-153-103-501 302-637-751-753-176-761-756-231-3_W.mp3)
  Segment 50: 143752ms ~ 144328ms (file: 265-265-265-625-263-265-265-265-263-260-265-254-523-254-520-632-623-632-612-613-615-652-621-216-263-265-256-267-276-276-253-253-523-521-276-275-361-136-135-653-651-315-153-523-5_W.mp3)
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
> **Time**: 2026-04-04 04:02:52
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "안녕하세요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "안녕하세요.", "start_ms": 50, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "오늘 저희 매장 첫 손님이세요. 무엇을 도와드릴까요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "저희", "start_ms": 250, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "매장", "start_ms": 460, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "첫", "start_ms": 770, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "손님이세요.", "start_ms": 930, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "무엇을", "start_ms": 1600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "도와드릴까요?", "start_ms": 2050, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "저기... 스마트폰 좀 보려고 하는데요.",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "저기...", "start_ms": 60, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "스마트폰", "start_ms": 400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 910, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "보려고", "start_ms": 1020, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "하는데요.", "start_ms": 1380, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "네, 이쪽으로 오세요. 최신 기종들이 많이 있습니다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이쪽으로", "start_ms": 200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "오세요.", "start_ms": 610, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "최신", "start_ms": 1000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "기종들이", "start_ms": 1310, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 1770, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "있습니다.", "start_ms": 2030, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 6,
"transcript": "이 모델은 어떠세요? 카메라 성능이 아주 뛰어납니다.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "모델은", "start_ms": 160, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "어떠세요?", "start_ms": 520, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "카메라", "start_ms": 1100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "성능이", "start_ms": 1510, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 1870, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "뛰어납니다.", "start_ms": 2080, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "디자인도 깔끔하고 좋네요. 가격은 어떻게 되나요?",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "디자인도", "start_ms": 80, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "깔끔하고", "start_ms": 540, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "좋네요.", "start_ms": 1000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "가격은", "start_ms": 1400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 1760, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "되나요?", "start_ms": 2070, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 9,
"transcript": "할부 원금은 이 정도인데, 결합 할인을 받으시면 더 저렴해집니다.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "할부", "start_ms": 40, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "원금은", "start_ms": 350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 710, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "정도인데,", "start_ms": 820, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "결합", "start_ms": 1300, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "할인을", "start_ms": 1560, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "받으시면", "start_ms": 1920, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 2330, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "저렴해집니다.", "start_ms": 2490, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "생각보다 괜찮네요. 약정 기간은 어떻게 되죠?",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "생각보다", "start_ms": 70, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "괜찮네요.", "start_ms": 530, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "약정", "start_ms": 1000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "기간은", "start_ms": 1310, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 1670, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "되죠?", "start_ms": 1980, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "보통 24개월로 많이 하십니다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "보통", "start_ms": 30, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "24개월로", "start_ms": 340, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 850, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "하십니다.", "start_ms": 1110, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "음... 조금 더 고민해보고 다시 올게요.",
"speaker": "speaker_2",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "음...", "start_ms": 100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 550, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 810, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "고민해보고", "start_ms": 970, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 1580, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "올게요.", "start_ms": 1890, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "네, 편하게 둘러보시고 궁금한 점 있으면 말씀해 주세요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "편하게", "start_ms": 200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "둘러보시고", "start_ms": 610, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "궁금한", "start_ms": 1200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "점", "start_ms": 1610, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있으면", "start_ms": 1720, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "말씀해", "start_ms": 2080, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "주세요.", "start_ms": 2490, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 15,
"transcript": "안녕히 계세요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 60, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "계세요.", "start_ms": 420, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "네, 안녕히 가세요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "가세요.", "start_ms": 560, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 18,
"transcript": "방금 손님은 그냥 구경만 하고 가셨네.",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "방금", "start_ms": 50, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "손님은", "start_ms": 360, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 720, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "구경만", "start_ms": 980, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "하고", "start_ms": 1390, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "가셨네.", "start_ms": 1650, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "요즘 경기가 안 좋아서 그런지 고민하시는 분들이 많아요.",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "요즘", "start_ms": 70, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "경기가", "start_ms": 380, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 740, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "좋아서", "start_ms": 900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그런지", "start_ms": 1260, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "고민하시는", "start_ms": 1670, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "분들이", "start_ms": 2280, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "많아요.", "start_ms": 2640, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "그래도 꾸준히 홍보하면 좋아질 거야.",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "그래도", "start_ms": 50, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "꾸준히", "start_ms": 460, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "홍보하면", "start_ms": 870, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "좋아질", "start_ms": 1380, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "거야.", "start_ms": 1790, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "네, 기운 내야죠. 다음 손님 오시네요.",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "기운", "start_ms": 200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "내야죠.", "start_ms": 510, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "손님", "start_ms": 1260, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "오시네요.", "start_ms": 1570, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 23,
"transcript": "어서 오세요. 갤럭시 S 시리즈 찾으시나요?",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "어서", "start_ms": 30, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "오세요.", "start_ms": 290, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "갤럭시", "start_ms": 700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "S", "start_ms": 1110, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "시리즈", "start_ms": 1270, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "찾으시나요?", "start_ms": 1680, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "아니요, 저는 아이폰 모델을 좀 보고 싶어서요.",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "아니요,", "start_ms": 50, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "저는", "start_ms": 410, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "아이폰", "start_ms": 670, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "모델을", "start_ms": 1080, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 1440, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "보고", "start_ms": 1550, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "싶어서요.", "start_ms": 1810, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "아이폰은 이쪽 매대에 준비되어 있습니다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "아이폰은", "start_ms": 40, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이쪽", "start_ms": 550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "매대에", "start_ms": 860, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "준비되어", "start_ms": 1270, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "있습니다.", "start_ms": 1780, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 27,
"transcript": "이거 색상이 참 예쁘네요.",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "이거", "start_ms": 60, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "색상이", "start_ms": 370, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "참", "start_ms": 780, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "예쁘네요.", "start_ms": 940, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "네, 이번에 새로 나온 컬러라 인기가 아주 많아요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이번에", "start_ms": 200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "새로", "start_ms": 560, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "나온", "start_ms": 870, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "컬러라", "start_ms": 1130, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "인기가", "start_ms": 1540, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 1900, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "많아요.", "start_ms": 2110, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "재고는 바로 있나요?",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "재고는", "start_ms": 50, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 410, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "있나요?", "start_ms": 670, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "잠시만요, 확인해 보겠습니다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "잠시만요,", "start_ms": 30, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "확인해", "start_ms": 540, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "보겠습니다.", "start_ms": 900, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 32,
"transcript": "네, 마침 마지막 한 대가 남아 있네요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "마침", "start_ms": 200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "마지막", "start_ms": 510, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 920, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "대가", "start_ms": 1030, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "남아", "start_ms": 1290, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "있네요.", "start_ms": 1550, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "정말요? 운이 좋네요. 바로 개통해 주세요.",
"speaker": "speaker_4",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "정말요?", "start_ms": 60, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "운이", "start_ms": 470, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "좋네요.", "start_ms": 730, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 1100, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "개통해", "start_ms": 1360, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "주세요.", "start_ms": 1770, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "감사합니다. 이쪽에서 서류 작성 도와드리겠습니다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "감사합니다.", "start_ms": 30, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "이쪽에서", "start_ms": 640, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "서류", "start_ms": 1150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "작성", "start_ms": 1460, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "도와드리겠습니다.", "start_ms": 1770, "dur_ms": 900, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 36,
"transcript": "벌써 개통이 다 된 건가요?",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "벌써", "start_ms": 50, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "개통이", "start_ms": 360, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 770, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "된", "start_ms": 880, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "건가요?", "start_ms": 1040, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "네, 시스템이 빨라서 금방 끝납니다. 정보 옮기는 것도 도와드릴까요?",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "시스템이", "start_ms": 200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "빨라서", "start_ms": 710, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "금방", "start_ms": 1120, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "끝납니다.", "start_ms": 1430, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "정보", "start_ms": 1900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "옮기는", "start_ms": 2210, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "것도", "start_ms": 2620, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "도와드릴까요?", "start_ms": 2880, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 39,
"transcript": "아, 그러면 정말 감사하죠. 제가 기계를 잘 못 다뤄서요.",
"speaker": "speaker_4",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "아,", "start_ms": 60, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "그러면", "start_ms": 220, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 580, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "감사하죠.", "start_ms": 890, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 1400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "기계를", "start_ms": 1660, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 2070, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "못", "start_ms": 2230, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "다뤄서요.", "start_ms": 2390, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "걱정 마세요. 제가 다 세팅해 드릴게요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "걱정", "start_ms": 30, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "마세요.", "start_ms": 340, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 750, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 1010, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "세팅해", "start_ms": 1120, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "드릴게요.", "start_ms": 1580, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 42,
"transcript": "이제 다 됐습니다. 케이스랑 필름은 서비스로 해드렸어요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 40, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 350, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "됐습니다.", "start_ms": 460, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "케이스랑", "start_ms": 950, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "필름은", "start_ms": 1460, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "서비스로", "start_ms": 1870, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "해드렸어요.", "start_ms": 2380, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "우와, 정말 감사합니다! 친구들에게도 여기 추천할게요.",
"speaker": "speaker_4",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "우와,", "start_ms": 60, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 370, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "감사합니다!", "start_ms": 680, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "친구들에게도", "start_ms": 1400, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "여기", "start_ms": 2110, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "추천할게요.", "start_ms": 2420, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "말씀만이라도 고맙습니다. 사용하시다 불편한 점 있으면 언제든 오세요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "말씀만이라도", "start_ms": 30, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "고맙습니다.", "start_ms": 740, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "사용하시다", "start_ms": 1350, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "불편한", "start_ms": 1960, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "점", "start_ms": 2370, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있으면", "start_ms": 2480, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "언제든", "start_ms": 2840, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "오세요.", "start_ms": 3250, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "네, 안녕히 계세요!",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 210, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "계세요!", "start_ms": 570, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "안녕히 가세요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 30, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "가세요.", "start_ms": 390, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 48,
"transcript": "사장님, 방금 개통하신 분 기분 좋아 보이시던데요?",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "사장님,", "start_ms": 60, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "방금", "start_ms": 520, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "개통하신", "start_ms": 830, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "분", "start_ms": 1340, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "기분", "start_ms": 1450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "좋아", "start_ms": 1760, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "보이시던데요?", "start_ms": 2020, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "그러게 말이야. 정성껏 응대하니까 보람이 있네.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "그러게", "start_ms": 40, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "말이야.", "start_ms": 400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "정성껏", "start_ms": 760, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "응대하니까", "start_ms": 1170, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "보람이", "start_ms": 1780, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있네.", "start_ms": 2140, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "오늘 하루도 힘내서 일해봅시다!",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 30, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "하루도", "start_ms": 290, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "힘내서", "start_ms": 700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "일해봅시다!", "start_ms": 1110, "dur_ms": 650, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-04 04:02:52*
