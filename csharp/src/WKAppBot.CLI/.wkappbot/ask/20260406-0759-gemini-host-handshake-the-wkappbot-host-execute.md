# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=40368  time=07:57:28  uptime=8718s  mem=158MB  system_processes=412"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_075443.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 630-671-673-675-627-627 310-367-103-731_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 265-605-267-507-275-257-765-276-726-627-672-726-257-602-750-127-207-520-267-627-276-517-571-750-750-275-571-725-725-572-152-756-572-357-705-632-762-275-267-312-352-267-257-576-6_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 106-170-130-103-130-130-130-130-130-130-130-130-130-130-130-130-370-137-713-371-731-731-736-713-763-713-173-103-130-103-130-130-103-130-130-130-130-130-130-130 130-317-370-731-3_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 135-315-315-135-312-607-130-751-137-317-317-517-327-321-312-103-127-271-137-213-231-321-312-103-132-312-130-317-317-236-312-367-357-351-506-675-672-627-627-267-762-375-371-361-6_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 170-103-317-137-173-137-172-137-731-173-271-723-276-172-761-726-326-613 571-517-751-157-710-173-130-710-713-713-701-751-716-173-715-762-371-753-756 571-571-157-517-571-135-710-1_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 735-751-517-127-216-613 135-135-537-571-725-715-713-170-173-175-173-723-703-153-371-137-712-715 576-571-517-507-315-570-513-705-175-751-157-571-753-571-753-705-107-150-517-715-7_W.mp3)
  Segment 7: 9456ms ~ 19644ms (file: 372-326-317-753-267-627-537-327-153-136-172-376-317-726-263-317-237-327-137-316-160-326-372-175-715-261-627-756-672-762-267-326-327-263-132-321-267-213-623-237-326-371-132-327-3_W.mp3)
  Segment 8: 20644ms ~ 27484ms (file: 672-762-275-276-356-576-576-574-157-752-574-315-130-206-327-726-327-627-267-762-627-236-657-672-675-572-317-217-206-267-762-275-576-571-672-165-156-163-356-265-637-675-371-325-2_W.mp3)
  Segment 9: 28484ms ~ 33668ms (file: 157-321-703-167-107-160-137-732-672-765-376-705-357-130-135-103-321-157-267-167-130-326-231-267-237-273-123-120-213-123-103-123-102-132-327-236-312-170-310-762-632-107-167-106-6_W.mp3)
  Segment 10: 34668ms ~ 42264ms (file: 351-352-130-236-267-130-321-735-137-231-537-236-236-752-512-267-701-720-713-137-312-537-321-632-627-126-102-276-327-537-751-715-132-132-731-315-726-237-123-231-627-267-176-132-2_W.mp3)
  Segment 11: 43264ms ~ 43840ms (file: 253-675-637-564-567-654-267-627-625-365-762-627-312-756-265-125-123-675-672-521-275-576-352-531-571-576-573-512-572-573-751-756-623-576-567-571-103-132-105-567-315-735-137-570-7_W.mp3)
  Segment 12: 44840ms ~ 53084ms (file: 230-320-130-316-310-371-130-230-175-571-250-126-216-206-203-230-103-301-310-236-206-756-765-657-260-160-163-102-572-576-752-301-316-316-320-230-103-753-273-103-310-167-612-602-3_W.mp3)
  Segment 13: 54084ms ~ 54660ms (file: 157-571-571-536-635-351-253-623-217-126-127-176-145-145-145-145-147-175-165-167-176-107-167-176-167-167-517-312-314-147-104-104-163-576-752-576-321-362-326-263-362-236-362-632-5_V.mp3)
  Segment 14: 55660ms ~ 56236ms (file: 507-631-261-125-607-725-267-302-310-301-310-310-136-312-261-605-236-236-263-763-321-237-706-567-672-572_V.mp3)
  Segment 15: 57236ms ~ 63356ms (file: 351-213-210-231-236-326-217-260-206-352-317-312-705-267-201-260-203-203-203-260-261-263-257-206-210-137-206-760-751-107-231-231-106-532-231-273-213-273-174-237-210-273-261-125-2_W.mp3)
  Segment 16: 64356ms ~ 64932ms (file: 123-612-671-673-716-756-753-507-321-571-572-573-537-571-576-576_W.mp3)
  Segment 17: 65932ms ~ 66508ms (file: 675 315-567-207-723-721-713-237-576-126-657-256-275-735-567-316-137-326-137-672-623-251-257-362-326-132-275-216-720-765-570-267-237-127-267-526-276 576-263-756-175-632-625-361-2_V.mp3)
  Segment 18: 67508ms ~ 73124ms (file: 172-612-127-261-751-102-120-302-321-312-320-236-237-265-120-236-276-102-120-372-574-102-213-321-263-162-261-231-216-236-267-237-231-217-216-210-312-327-327-756-173-132-132-321-2_W.mp3)
  Segment 19: 74124ms ~ 74700ms (file: 725-547-267-237-520-725-216-103-167-517-106-312-527-236-172 675-765-312-576-567-375-705-256-157-576-731-756-756-726-326-301-103-231-513-506-637-321-126-316-126-132-576-326-723-5_V.mp3)
  Segment 20: 75700ms ~ 76276ms (file: 571-750-751-275-257-275-572-754-725-762-726-672-652-652-675-762-725-327-265-263-265-235-523-275-712-125-261-265-652-625-725-752-752-573-532-653-625-652-256-754-753-752-572-756-1_W.mp3)
  Segment 21: 77276ms ~ 77852ms (file: 673-627-237-621-725-725-145-351-257-237-523-265-157-532-756-507-627-325-103-756-316-507-312-273-752-175-637-675-567-217-732-756-276-765-326-507-571-756-527-573-673-765-263-657-3_V.mp3)
  Segment 22: 78852ms ~ 79428ms (file: 571-571-571-570-571-517-517-571-570-570-571-751-705-715-175-750-701-571-750-751-751-571-571-751-571-570-517-751-517-751-571-571-517-571-751-571_W.mp3)
  Segment 23: 80428ms ~ 81004ms (file: 507-570-571-715-571-750-750-705-751-570-750-715-750-756-571 705-756-715-716-107-570 175-715-170-107-175-170-715-710-170-710-713-170-756-750 615-175-751-157-715-715-107-751-715-7_V.mp3)
  Segment 24: 82004ms ~ 88376ms (file: 321-320-132-762-276-213-320-217-172-732-103-137-317-321-371-735-751-275-325-137-713-320-721-712-106-162-162-316-317-726-321-327-123-260-236-263-236-236-276-273-132-716-157-167-6_W.mp3)
  Segment 25: 89376ms ~ 89952ms (file: 163-167-167-167-167-137-217-267-751-756-576-712-756-765-735-376-765-756-761-756-715-157-571-571-571-576-756-751-567-623-163-163-163-162-163-162-162-162-165-165-157-136-165-156-1_W.mp3)
  Segment 26: 90952ms ~ 91528ms (file: 756-567-261-547-627-527-263-275-236-276-762-265-276-762-326-570-235-231-215-275-251-236-567-627-602-675-765-765-756-756-756-756-576-135-567-576-756-567-567-576-576-576-576-576-5_W.mp3)
  Segment 27: 92528ms ~ 93104ms (file: 762-672-236-231-753-360-637-521-675-137-302 570-167-173-367-213-267-631-126-175-752-631-623-567-356-136-576-752-267-162-523-321-216-521-570-217-132-152-126-572-237-321-572-762-2_V.mp3)
  Segment 28: 94104ms ~ 106164ms (file: 153-102-103-134-132 106-721-102-173-213-162-150-123-126-612-230-237-237-362-170-573-127-713-573-361-765-756-657-672-106-671-617-206-260-625-265-260-267-762-271-236-103-167-162-1_W.mp3)
  Segment 29: 107164ms ~ 113932ms (file: 231-267-126-261-231-762-675-627-267-672-260-236-763-765-276-267-321-103-726-267-310-372-371-106-126-261-276-267-257-312-317-172-217-726-130-216-762-260-760-270-167-167-263-310-7_W.mp3)
  Segment 30: 114932ms ~ 115508ms (file: 671-671-673-765-763-637-671-637-673-137-173-163-316-367-617-167-317-617-361-367-617-167-761-671-761-167-617-675-671-167-761-675-671-761-761-716-765-673-675-765-765-671-765-570-5_W.mp3)
  Segment 31: 116508ms ~ 117084ms (file: 621-163-675-763-261-632-326-371-326-572-752-756-762-203-312 326-130_W.mp3)
  Segment 32: 118084ms ~ 118660ms (file: 756-576-576-567-750-617-751-175-715-715-716-716-715-715-715-576-157-167-156-176-574 167-163-103-715-710-170-705-710-710-701-701-701-715-170-175-170-701-710-715-701-705-710-715-7_V.mp3)
  Segment 33: 119660ms ~ 120236ms (file: 312 623-612-152 215-752-261-267-216-276-326-752-562-732-263-251-573-637-736-570-312-265-257-526-756-325-561-526-762-547-362-750-627-275-750-361-527-256-756-675-276-726-752-576-7_V.mp3)
  Segment 34: 121236ms ~ 121812ms (file: 135-136-135-315-135-132-132-135-132-132-132-135-135-135-253-315-351-132-315-321-312-357-156-317-137-136-165-163-106-137-316-163-136-176-167-162-127-136-126-316-136-573-153-173-1_W.mp3)
  Segment 35: 122812ms ~ 123388ms (file: 576-574-574-574-574-574-574-574-571-574-574-573-571-571-574-571-574 571-572-571-517-570-756-571-571-750-517-517-157-715-517-517-135-157-135-137-153-153-135-537-731-173-716-167-1_W.mp3)
  Segment 36: 124388ms ~ 124964ms (file: 627-627-126-162-132-137-713-317-173-137-576-756-320-237-231-601-623-106-231-326-632-657-362-136-657-546-531-657-651-106 675-731-167-173-715-137_V.mp3)
  Segment 37: 125964ms ~ 126540ms (file: 261-156-625-271-231-752-576-513-315-301-751-576-127-253-126-261-501-571-276-176-526-567-516-235-672-765-617-136-517-213-271-312-172-317-132-753 750-721-521-165-735-271-753-672-6_V.mp3)
  Segment 38: 127540ms ~ 134704ms (file: 236-103-176-715-312-376-163-163-103-273-325-137-167-107-756-570-130-573-574-230-176-716-671-672-371-310-236-320-701-570-136-327-267-267-752-217-273-263-107-327-756-167-236-527-5_W.mp3)
  Segment 39: 135704ms ~ 136280ms (file: 176-137 135-137-157-157-157-571-175-751-715-751-571-517-571-157-157-157-157-175-175-157-175-156-167-176 130-135-173-175-175-157-175-157-175-751-157-751-517-157-175-157-157-167-1_V.mp3)
  Segment 40: 137280ms ~ 137856ms (file: 367-136 261-531-132-216-362-312-721-137-132-362-261-312-130-157-351-316-752-567-275-705-312-712-756-672-163-502-516-125-537-213-205-721-752-265-316-172-627-253-261-321-507-156-7_V.mp3)
  Segment 41: 138856ms ~ 139432ms (file: 267-750-256-672-206-236-123-132-137-310-371-312-137-170-730-750-705-751-157-105_W.mp3)
  Segment 42: 140432ms ~ 141008ms (file: 736-671-107-126-261-527-716-765-175-276-132-570-210-217-261-126-271-760-132-235-312-371-236-572-327-326-326-327-257-256-726-753-673-325-576-312-675-732-507-572-521-321-167-256-2_V.mp3)
  Segment 43: 142008ms ~ 142584ms (file: 351-175-132-135-132-125-513-135-153-150-125-315-102-152-571-537-135-157-173-317-135-137-172-317-132-136-613-612-651-167-165-156-576-576-351-176-172-162-132-137-132-271-235-362-3_W.mp3)
  Segment 44: 143584ms ~ 144160ms (file: 376-637-367-376-763-376-763-136-371-376-376-376-376-673_W.mp3)
  Segment 45: 145160ms ~ 145736ms (file: 316-316-310-130-310 236-126-621-106-671-765-756-762-761-762-761-671_V.mp3)
  Segment 46: 146736ms ~ 147312ms (file: 567-512-571 125-516-572 517-571-571 152-517-165-136-516-513-531-516_W.mp3)
  Segment 47: 148312ms ~ 148888ms (file: 765-736-765-620-716-716-176-761-736-765-765-761-713-736-713-753-316-756-627-752-576-756-672-613-137-301-326-317-301-130-735-312-137-127-756-325-237-672-312-256-620-316-137-321-5_W.mp3)
  Segment 48: 149888ms ~ 150464ms (file: 376-537-135-231-623-537-376-705-327-732-132-123-231-756-263-576-627-256-602-327-572-752-276-612-761-312-321-127-276-627-725-657-612-261-756-625-526-762-507-762-560-612-305-253-6_V.mp3)
  Segment 49: 151464ms ~ 163416ms (file: 132-214-231-321-130-276-312-271-216-572-263-267-126-102-231-562-123-231-271-257-217-106-173-120-572-257-725-231-102-216-253-512-172-172-137-120-231-267-261-127-327-312-573-537-1_W.mp3)
  Segment 50: 164416ms ~ 173596ms (file: 123-267-231-321-231-263-103-257-301-123-263-527-260-310-310-276-215-216-526-120-260-263-230-236-620-260-623-263-231-562-103-126-715-320-357-236-652-652-263-310-320-120-562-253-1_W.mp3)
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
> **Time**: 2026-04-06 07:59:11
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
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 3,
"transcript": "안녕하세요 여러분",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 120, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "여러분", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "오늘도 한국어 공부를 시작해 볼까요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "오늘도", "start_ms": 80, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "한국어", "start_ms": 210, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "공부를", "start_ms": 320, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "시작해", "start_ms": 430, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "볼까요", "start_ms": 520, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 6,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 7,
"transcript": "이 문장을 큰 소리로 따라 읽어 보세요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 150, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "문장을", "start_ms": 1200, "dur_ms": 1500, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 2800, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "소리로", "start_ms": 3900, "dur_ms": 1500, "phase_lr_deg": 0.0},
{"word": "따라", "start_ms": 5500, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "읽어", "start_ms": 6800, "dur_ms": 1300, "phase_lr_deg": 0.0},
{"word": "보세요", "start_ms": 8200, "dur_ms": 1500, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "발음과 억양에 주의하면서 다시 한번 해볼까요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "발음과", "start_ms": 200, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "억양에", "start_ms": 1100, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "주의하면서", "start_ms": 2000, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 3300, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "한번", "start_ms": 4100, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "해볼까요", "start_ms": 5000, "dur_ms": 1200, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "How are you doing today? I hope you're ready.",
"speaker": "speaker_1",
"confidence": 0.92,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "How", "start_ms": 100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "are", "start_ms": 310, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 470, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "doing", "start_ms": 660, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "today?", "start_ms": 1070, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 1700, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "hope", "start_ms": 1860, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "you're", "start_ms": 2170, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "ready.", "start_ms": 2530, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "좋습니다 아주 잘하고 계세요 조금 더 힘을 내봐요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "좋습니다", "start_ms": 120, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 1000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "잘하고", "start_ms": 1700, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "계세요", "start_ms": 2600, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 3600, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 4400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "힘을", "start_ms": 4800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "내봐요", "start_ms": 5500, "dur_ms": 800, "phase_lr_deg": 0.0}
]
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
"transcript": "반복 학습만이 외국어 실력을 키우는 지름길입니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "반복", "start_ms": 100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "학습만이", "start_ms": 800, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "외국어", "start_ms": 1900, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "실력을", "start_ms": 2800, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "키우는", "start_ms": 3800, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "지름길입니다", "start_ms": 4700, "dur_ms": 1300, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 15,
"transcript": "매일 꾸준히 십 분씩만 투자해 보세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "매일", "start_ms": 120, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "꾸준히", "start_ms": 800, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "십", "start_ms": 1700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "분씩만", "start_ms": 2200, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "투자해", "start_ms": 3100, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "보세요", "start_ms": 4000, "dur_ms": 800, "phase_lr_deg": 0.0}
]
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
"transcript": "놀라운 변화가 나타날 거예요 화이팅",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "놀라운", "start_ms": 150, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "변화가", "start_ms": 1000, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "나타날", "start_ms": 1900, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 2700, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "화이팅", "start_ms": 3500, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "질문이 있다면 언제든지 댓글로 남겨주세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "질문이", "start_ms": 100, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "있다면", "start_ms": 900, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "언제든지", "start_ms": 1800, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "댓글로", "start_ms": 2900, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "남겨주세요", "start_ms": 3800, "dur_ms": 1200, "phase_lr_deg": 0.0}
]
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
"transcript": "친절하게 답변해 드릴게요 걱정하지 마세요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "친절하게", "start_ms": 120, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "답변해", "start_ms": 1100, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "드릴게요", "start_ms": 2000, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "걱정하지", "start_ms": 3000, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "마세요", "start_ms": 4000, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "오늘 수업은 여기까지입니다 다음에 또 만나요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "수업은", "start_ms": 800, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "여기까지입니다", "start_ms": 1600, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "다음에", "start_ms": 3000, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 3800, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 4200, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "Great job today! Keep practicing and see you soon.",
"speaker": "speaker_2",
"confidence": 0.93,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "Great", "start_ms": 100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "job", "start_ms": 450, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "today!", "start_ms": 750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "Keep", "start_ms": 1300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "practicing", "start_ms": 1650, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "and", "start_ms": 2300, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "see", "start_ms": 2550, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 2850, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "soon.", "start_ms": 3100, "dur_ms": 400, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 43,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 44,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 47,
"transcript": "언제나 여러분의 도전을 응원합니다 감사합니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "언제나", "start_ms": 150, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "여러분의", "start_ms": 900, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "도전을", "start_ms": 1800, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "응원합니다", "start_ms": 2600, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 3800, "dur_ms": 1100, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 49,
"transcript": "그럼 안녕히 계세요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 600, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "계세요", "start_ms": 1300, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "Goodbye! Take care!",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "Goodbye!", "start_ms": 120, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "Take", "start_ms": 700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "care!", "start_ms": 1100, "dur_ms": 400, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 07:59:11*
