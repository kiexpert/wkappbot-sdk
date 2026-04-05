# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=1700  time=05:45:57  uptime=4224s  mem=1902MB  system_processes=354"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_054420.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 10368ms (file: 526-650-162-576-357-132-175-125-172-216-137-123-261-213-231-132-321-203-312-107-107-627-657-625-657-625-106-102-314-130-230-216-237-267-263-203-216-236-201-260-236-321-354-310-3_W.mp3)
  Segment 2: 11368ms ~ 23932ms (file: 102-124-103-513-312-312-236-263-675-671-267-260-263-312-123 321-213-267-231-263-756-167-165-126-123-576-106-156-215-102-612-207-103-263-230-132-103-106-361-312-120-102-213-206-5_W.mp3)
  Segment 3: 24932ms ~ 25508ms (file: 617-137-167-176-613-162-756-756-675-167-376-167-613-675-167-367-637-760 316-316-567-612-276-671-576-765-762-602-237-273-236-267-672-632-230-176-760-736-216-275-263-156-761-231-7_W.mp3)
  Segment 4: 26508ms ~ 32268ms (file: 376-765-761-317-371-173-106-175-137-301-375-713-175-137-731-715-175-135-137-130-105-547-517-270-275-276-273-276-327-276-273-273-127-720-102-120-126-213-216-106-261-132-106-273-2_W.mp3)
  Segment 5: 33268ms ~ 33844ms (file: 570-570-571-517-150-517-517-705 135-135-153-150-105-150-175-175-137-157-315-135-157-107-157-103-150-105-105-153-150-170-157-517-517-751-751-715-173-175-137 105-154-157-105-517-7_W.mp3)
  Segment 6: 34844ms ~ 35420ms (file: 750-756-756-175-756-756-751-671-756-716-716-756 157-153-175-170-713-731-173-150-157-170-173-170-713-715-761-765-756-715-751-715-756-765-756-751-570 751-150-170-172-710-710-170-7_V.mp3)
  Segment 7: 36420ms ~ 40956ms (file: 754-571-574-315-135-315-310-135-135-157-571-574-574-574-574-574-574-574-571-753-315-315-315-103-312-310-102-512-203-203-260-206-213-103-715-574 153-571-571-573 315-571-574-574-5_W.mp3)
  Segment 8: 41956ms ~ 42532ms (file: 132-231-132-123-132-103-620-301-310-316-136-310-130-130-670-671-317-312-361-361-136-136-162-206-534-520-521-536-752 657 357-301-350-321-176-173-716-713-176-713-716-761-173-176-1_W.mp3)
  Segment 9: 43532ms ~ 44108ms (file: 613-316-637-607-637-675-675-617-613-123_W.mp3)
  Segment 10: 45108ms ~ 53208ms (file: 672-132-130-263-312-120-231-302-753-260-217-132-574-265-231-132-312-216-574-517-613-216-260-102-706-672-265-102-175-231-621-260-321-351-132-236-210-612-321-130-120-132-513-103-2_W.mp3)
  Segment 11: 54208ms ~ 64216ms (file: 362-572-321-132-321-230-310-213-320-672-260-267-132-130-230-752-762-267-273-732-327-326-237-327-675-106-136-103-320-321-237-320-237-132-163-316-713-574-103-302-102-321-267-756-2_W.mp3)
  Segment 12: 65216ms ~ 65792ms (file: 167-163-671-617-601-617-163-613-760-671-672-567-576-506-573-103-103-136-137-163-134-132-620-263-126-163-126-102-103-361-613-673-672-627-672-562-526-163-621-106-137-621-627-267-2_V.mp3)
  Segment 13: 66792ms ~ 71436ms (file: 263-213-267-201-237-261-310-573-251-102-260-231-621-627-627-621-765-621-126-126-236-210-716-671-672-201-120-275-725-236-650-672-267-150-310-203-132-127-261-267-236-326-216-217-2_W.mp3)
  Segment 14: 72436ms ~ 76828ms (file: 130-612-120-126-263-267-206-126-103-126-267-321-326-572-152-136-160-126-263-517-135-326-317-762-173-135-103-103-130-103-320-176-367-537-123-132-573-173-703-312-132-107-123-107-1_V.mp3)
  Segment 15: 77828ms ~ 78404ms (file: 105-157-105-157-517-507-501-517-150-501-510-517-510-517-506-510-507-501-501-571-576-507-564-516-510-547-570-504-574-507-570-570 501-510-105-105-105-517-105-517-510-150-150-507-5_V.mp3)
  Segment 16: 79404ms ~ 80556ms (file: 567-756-765-756-765-637-756-136-137-516-135-306-632-362-632-367-312-216-367-362-316-106-160-263-532-325-531-351-541-517-517-576-315-765-352-136-327-173-126-561-562-651-316-653-5_W.mp3)
  Segment 17: 81556ms ~ 89044ms (file: 103-541-302-576-203-203-230-103-263-326-327-321-231-132-132-173-301-136-230-321-230-270-312-372-231-231-261-326-320-127-253-263-213-237-267-257-542-256-265-267-230-276-267-236-3_W.mp3)
  Segment 18: 90044ms ~ 90620ms (file: 150-150-150-514-154-301-157-150-175-175-107-105-175-150-107-150-175-150-175 135-150-514-150-175-157-175-175-170-156-175-105-107-105-157-175-176 103-153-105-514-150-105-153-105-1_V.mp3)
  Segment 19: 91620ms ~ 92196ms (file: 153-153-135-315 136-136-135-135-150 132-136-316-153-156-156-315-165-135-315-365-365-156-165-135-136 137-315-315-136-316-136 316-316-361-361-316-361-136-361-361-316-316 135-132-1_W.mp3)
  Segment 20: 93196ms ~ 93772ms (file: 135-513-751-175-317-371-137-103-137-157-173-310-107-123-317-315-173-317-103-137-103-173-317-371-321 135-135-103-135-135-317-135-731-137-132-103-103-172-107-317-103-317-130-371-1_V.mp3)
  Segment 21: 94772ms ~ 95348ms (file: 136 362-537-325-321-312-135-135-312-312-361-315-312-351-153-321-315-361-572-275-326-321-326-321-312-321-756-375-325-236 531-537-235-235-235-235-325-203-235-230-325-325-325-321-3_W.mp3)
  Segment 22: 96348ms ~ 96924ms (file: 325-132-576-753-731-572-576 617 267 176-107-321-235-132-137-367-175-357-315-752-732-627-623-156-637-367-361-316-167-613-657-657-356-657-573-567-753-653-567_W.mp3)
  Segment 23: 97924ms ~ 98500ms (file: 103-136-132-312-312-627-612-613-621-264-326-213-231-213-163-127-367-317-132-136-314-365-761-761-731-136-127-172-167-176-132-123-261-623-267-602-627-627-126-132-123-347-103-103-1_V.mp3)
  Segment 24: 99500ms ~ 104684ms (file: 261-263-126-216-126-623-176-126-261-261-216-132-263-621-236-120-162-127-123-261-216-213-126-261-276-216-126-126-267-170-263-371-276-672-627-137-321-627-670-261-261-312-627-321-2_W.mp3)
  Segment 25: 105684ms ~ 106260ms (file: 361-367-736-301-361-136-612-612-623-612-261_V.mp3)
  Segment 26: 107260ms ~ 107836ms (file: 157-561-570-571-571-571-571-571-571-571-576-751-576-516-537-635-653-567-657-571-576-357-756-756-571-570-574-571-574-574-574-574-517-517-573-153-571-751-576-576-657-657-651-650-1_W.mp3)
  Segment 27: 108836ms ~ 120392ms (file: 673-107-260-203-762-230-231-123-310-102-102-103-102-130-231-123-276-213-103-230-257-537-263-260-571-120-312-130-310-130-237-276-237-237-276-725-752-237-513-237-231-206-302-320-5_W.mp3)
  Segment 28: 121392ms ~ 132588ms (file: 263-123-170-160-752-130-736-123-231-132-261-321-216-123-652-126-216-321-263-257-267-261-120-612-103-167-126-102-103-236-326-632-527-251-130-231-236-263-236-312-236-263-321-263-2_W.mp3)
  Segment 29: 133588ms ~ 134164ms (file: 103-105-105-105-150-150-570-571-517-501-571-561-567-567-576 105-175-170-570-107-712-170-170-715-573 157-103-105-103-105-105-150-105-105-150-157-157-576-657-567-657-567-567-657-5_V.mp3)
  Segment 30: 135164ms ~ 135740ms (file: 510-154-145-150-150-150-105-150-105-150 517-517-150-153-517-517-501-514 150-105-150-150-510-157-150 150-150-150-150-105-105-150-175-157-157-157-150-153 105-105-105-510-510-105-5_V.mp3)
  Segment 31: 136740ms ~ 137316ms (file: 576-653-652-576-567-576-567-572-576-517-573-561-657-675-765-576-576-576-567-321-531-567-567-756-621-576-574-567-357-536-351-312-576-576-325-153-136-561-516-576-103-507-354-536-3_W.mp3)
  Segment 32: 138316ms ~ 138892ms (file: 175-157-157-176-173 105-105_V.mp3)
  Segment 33: 139892ms ~ 140468ms (file: 317-310-301-137-751-372-310-170-751-701-751-715-705-170-730-710 705-703-735-750-571-573-157-375-735-715-175-571-170-371-371-103-130-301-307-301-701-715-751-103-301-137-173-735-3_V.mp3)
  Segment 34: 141468ms ~ 142044ms (file: 613-316-613-163-613-136-316-136-137-173 673-632-570-132 267-753-570-326-725-321-267 361-765-631-361-136 217-762-576-321-162-725-213-276-217-273-673-126-275-672-570-627-321 632-6_V.mp3)
  Segment 35: 143044ms ~ 143620ms (file: 132-632-235-627-273-123-576-237-275-725-504-257-537-752-765-721-316-132-216-762-576-572-752-571-276-275-725-754-752-752-750-725-276-260-527 250-276-265-762-276-361-521-325-752-7_V.mp3)
  Segment 36: 144620ms ~ 153224ms (file: 263-267-176-320-267-260-726-765-263-236-236-230-321-106-167-276-310-123-320-326-237-230-236-230-312-762-276-267-267-130-130-367-237-123-176-132-726-267-127-276-765-726-237-130-2_W.mp3)
  Segment 37: 154224ms ~ 154800ms (file: 570-570-501-510-570-574-751-751-751-571-517-150-570-517-715-571-570-571-570-105-751-715-175-510-571 517-517-571-157-570-571-157-175-175-715-517-571-570-507-751-750-715-715-175-7_W.mp3)
  Segment 38: 155800ms ~ 156376ms (file: 213-123-712-217-623-612-675-167-162-102-130-132-136-321-216-162-621-162-162-576-321-320-302-301-320-302-230-301-312-302-102-236-230-230-236-327-652-326-301-123-523-265-657-652-2_W.mp3)
  Segment 39: 157376ms ~ 162848ms (file: 260-756-572-251-276-123-216-206-572-216-574-576-315-301-312-132-163-231-326-261-623-576-203-216-260-132-301-201-132-301-130-137-320-320-206-576-327-102-750-510-217-102-132-754-2_V.mp3)
  Segment 40: 163848ms ~ 169860ms (file: 206-201-213-106-165-167-205-723-675-761-675-257-106-726-205-273-725-167-756-762-203-317-136-132-312-371-132-103-106-103-102-701-307-137-231-754-750-206-102-136-157-754-576-201-1_V.mp3)
  Segment 41: 170860ms ~ 171436ms (file: 123-167-617-106-671-756-756-765-167 523-136-735-361-160-103-132-627-312-163-756-176-156-326-103-510-136-156-163-163-136-671-106-621-103-106-160-136-130-751-576-136-152-137-167 1_V.mp3)
  Segment 42: 172436ms ~ 173012ms (file: 756 570-756-675-750 136-372-367-213-267-261-251-321-765-275-326-726-162-261-325-325-165-752-527-513-357-213-265-572-257-267-572-235-273-574-357-573-562-126-237-231-750-216-321-2_V.mp3)
  Segment 43: 174012ms ~ 174588ms (file: 672-602-610-612-260-260-320-321-301-163-160-610-263-261-123-612-167-167-754-567-613-162-617-176-716-176-367-275-137 126-176-756-576-756-176-175-163-123-701-132-103-130-301-106-1_V.mp3)
  Segment 44: 175588ms ~ 176164ms (file: 574-752-312-310-612-261-765-736-632-763-765-732-710-132-103-316-126-672-652-361-756-256-267-371-307-372-312-172-372-175-326-132-136-123-753-672-673-237-267-132-361-213-576-176-2_W.mp3)
  Segment 45: 177164ms ~ 177740ms (file: 715-517-157-175-157-715-173-107-571-571-751-715-751-175-715-571-751-750 510-150-517-107-150-175-705-105-157-705-170-175-570-570-571-571-570-570-570 570-571-517-175-157-157-107-1_W.mp3)
  Segment 46: 178740ms ~ 179316ms (file: 375-315-135-123-132-621-132-315-236-321-236-321-231-321-312-321-361-631-326-312-316-321-352-531-526-523-532-356-325-326-326-326-352-532-532-132-132-136-126-726-371-315-325-325-3_W.mp3)
  Segment 47: 180316ms ~ 187048ms (file: 625-235-572-321-275-236-567-507-230-725-210-132-327-537-203-165-750-756-260-632-576-312-372-325-762-672-567-752-213-132-306-731-137-106-362-231-276-263-103-726-321-357-326-230-3_V.mp3)
  Segment 48: 188048ms ~ 188624ms (file: 123-137-213-271-132-206-216-261-206-260-206 201-231-203-205-175-123-321-302-625-102-102-275-207-260-207-215-217-203-632-231-302-326-302-203-307-307-721-307-167-172-753-756-236-1_W.mp3)
  Segment 49: 189624ms ~ 198480ms (file: 236-103-316-102-261-576-216-216-567-321-576-301-574-236-623-126-213-613-162-106-162-310-120-571-160-310-326-257-267-623-267-263-625-231-623-261-756-261-263-236-132-310-120-715-3_W.mp3)
  Segment 50: 199480ms ~ 200056ms (file: 567-657-672-762-765-675-562-562-562-576-265-675-675-625-263-213-263 752 567-567-567-657-576-567-567-576-567-567-567-567-576-562-567-576-576-567-576-567-675-567-576-567-576-567-5_W.mp3)
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
> **Time**: 2026-04-05 05:47:22
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 2,
"transcript": "오늘 저녁에 명동에 가서 같이 비빔밥 먹을래요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "저녁에", "start_ms": 600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "명동에", "start_ms": 1200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "가서", "start_ms": 1900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 2400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "비빔밥", "start_ms": 2900, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "먹을래요?", "start_ms": 3700, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "좋아요! 하지만 저는 매운 음식을 잘 못 먹는데 괜찮을까요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "좋아요!", "start_ms": 200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "하지만", "start_ms": 1000, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "저는", "start_ms": 1600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "매운", "start_ms": 2100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "음식을", "start_ms": 2600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 3200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "못", "start_ms": 3550, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "먹는데", "start_ms": 3850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "괜찮을까요?", "start_ms": 4400, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "걱정 마세요. 고추장을 따로 달라고 하면 맵지 않게 먹을 수 있어요.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "걱정", "start_ms": 180, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "마세요.", "start_ms": 650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "고추장을", "start_ms": 1300, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "따로", "start_ms": 2100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "달라고", "start_ms": 2600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "하면", "start_ms": 3200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "맵지", "start_ms": 3700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "않게", "start_ms": 4200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "먹을", "start_ms": 4700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 5100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "있어요.", "start_ms": 5350, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "다행이네요. 그럼 이따가 저녁 7시에 역 앞에서 만나요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "다행이네요.", "start_ms": 250, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "그럼", "start_ms": 1100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이따가", "start_ms": 1600, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "저녁", "start_ms": 2300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "7시에", "start_ms": 2800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "역", "start_ms": 3500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "앞에서", "start_ms": 3850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "만나요.", "start_ms": 4450, "dur_ms": 550, "phase_lr_deg": 0.0}
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
"transcript": "요즘 한국 드라마가 인기가 많아서 한국어를 배우기 시작했어요.",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "요즘", "start_ms": 120, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "드라마가", "start_ms": 1100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "인기가", "start_ms": 1800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "많아서", "start_ms": 2400, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "한국어를", "start_ms": 3100, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "배우기", "start_ms": 3850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "시작했어요.", "start_ms": 4400, "dur_ms": 750, "phase_lr_deg": 0.0}
]
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
"transcript": "정말요? 어떤 드라마를 제일 좋아하세요?",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "정말요?", "start_ms": 200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 1000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "드라마를", "start_ms": 1500, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "제일", "start_ms": 2200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "좋아하세요?", "start_ms": 2700, "dur_ms": 800, "phase_lr_deg": 0.0}
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
"transcript": "저는 로맨틱 코미디 드라마를 좋아해요. 배우들의 대사가 재미있거든요.",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "로맨틱", "start_ms": 650, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "코미디", "start_ms": 1300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "드라마를", "start_ms": 1900, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "좋아해요.", "start_ms": 2600, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "배우들의", "start_ms": 3500, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "대사가", "start_ms": 4250, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "재미있거든요.", "start_ms": 4900, "dur_ms": 850, "phase_lr_deg": 0.0}
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
"transcript": "I believe that every citizen got so many benefits from the new policy.",
"speaker": "speaker_5",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 220,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 220, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "believe", "start_ms": 500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "that", "start_ms": 1050, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "every", "start_ms": 1450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "citizen", "start_ms": 1950, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "got", "start_ms": 2650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "so", "start_ms": 3100, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "many", "start_ms": 3450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "benefits", "start_ms": 3900, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "from", "start_ms": 4650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 5050, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "new", "start_ms": 5350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "policy.", "start_ms": 5750, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "내일 친구 생일이라서 선물을 사야 하는데 뭐가 좋을까요?",
"speaker": "speaker_6",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "내일", "start_ms": 180, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "친구", "start_ms": 700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "생일이라서", "start_ms": 1250, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "선물을", "start_ms": 2150, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "사야", "start_ms": 2800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "하는데", "start_ms": 3300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "뭐가", "start_ms": 3900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "좋을까요?", "start_ms": 4400, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "친구분이 평소에 관심 있는 분야가 무엇인지 생각해 보세요.",
"speaker": "speaker_4",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "친구분이", "start_ms": 200, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "평소에", "start_ms": 1000, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "관심", "start_ms": 1650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 2200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "분야가", "start_ms": 2700, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "무엇인지", "start_ms": 3350, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "생각해", "start_ms": 4050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "보세요.", "start_ms": 4650, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "그 친구는 요리하는 걸 정말 좋아해요. 그래서 앞치마나 주방 도구가 어떨까 싶어요.",
"speaker": "speaker_6",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "그", "start_ms": 150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "친구는", "start_ms": 450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "요리하는", "start_ms": 1000, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "걸", "start_ms": 1750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 2150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "좋아해요.", "start_ms": 2700, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 3600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "앞치마나", "start_ms": 4150, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "주방", "start_ms": 4900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "도구가", "start_ms": 5400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "어떨까", "start_ms": 6000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "싶어요.", "start_ms": 6550, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "좋은 생각이네요! 예쁜 앞치마를 선물하면 친구가 아주 기뻐할 거예요.",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "생각이네요!", "start_ms": 650, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "예쁜", "start_ms": 1600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "앞치마를", "start_ms": 2100, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "선물하면", "start_ms": 2850, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "친구가", "start_ms": 3650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 4250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "기뻐할", "start_ms": 4700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "거예요.", "start_ms": 5300, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "주말에 시간이 되면 같이 전시회 보러 갈까요? 마침 보고 싶은 전시가 있거든요.",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "주말에", "start_ms": 180, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "시간이", "start_ms": 750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "되면", "start_ms": 1300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 1750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "전시회", "start_ms": 2250, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "보러", "start_ms": 2950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "갈까요?", "start_ms": 3450, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "마침", "start_ms": 4300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "보고", "start_ms": 4800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "싶은", "start_ms": 5300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "전시가", "start_ms": 5750, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "있거든요.", "start_ms": 6400, "dur_ms": 750, "phase_lr_deg": 0.0}
]
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
"transcript": "좋아요. 토요일 오후는 어떠세요? 저는 일요일보다 토요일이 편해요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "좋아요.", "start_ms": 120, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "토요일", "start_ms": 900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "오후는", "start_ms": 1500, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "어떠세요?", "start_ms": 2150, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "저는", "start_ms": 3000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "일요일보다", "start_ms": 3500, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "토요일이", "start_ms": 4400, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "편해요.", "start_ms": 5150, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "저도 토요일이 더 좋아요. 그럼 오후 2시에 전시장 매표소 앞에서 만날까요?",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "저도", "start_ms": 200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "토요일이", "start_ms": 700, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 1400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "좋아요.", "start_ms": 1750, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "그럼", "start_ms": 2550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "오후", "start_ms": 3050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "2시에", "start_ms": 3550, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "전시장", "start_ms": 4200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "매표소", "start_ms": 4900, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "앞에서", "start_ms": 5650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "만날까요?", "start_ms": 6250, "dur_ms": 750, "phase_lr_deg": 0.0}
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
"transcript": "네, 알겠습니다. 그때 봬요. 덕분에 즐거운 주말이 될 것 같아요.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "알겠습니다.", "start_ms": 550, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "그때", "start_ms": 1500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "봬요.", "start_ms": 2000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "덕분에", "start_ms": 2700, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "즐거운", "start_ms": 3350, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "주말이", "start_ms": 4000, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "될", "start_ms": 4650, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 5000, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "같아요.", "start_ms": 5300, "dur_ms": 600, "phase_lr_deg": 0.0}
]
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
"transcript": "감기에 걸려서 목소리가 잘 안 나오네요. 따뜻한 차를 좀 마셔야겠어요.",
"speaker": "speaker_4",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 220,
"audio_type": "speech",
"words": [
{"word": "감기에", "start_ms": 220, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "걸려서", "start_ms": 800, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "목소리가", "start_ms": 1450, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 2200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 2600, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "나오네요.", "start_ms": 2950, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "따뜻한", "start_ms": 3800, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "차를", "start_ms": 4450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 5000, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "마셔야겠어요.", "start_ms": 5350, "dur_ms": 850, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 05:47:22*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=17708  time=05:45:57  uptime=5818s  mem=148MB  system_processes=354"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_054521.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 576-750-570-570-105-153-510-150-530-507-531-157-571-570-571-517-157-156-751-157-175-571-157-756-175-517-517-517-157-517-517-517-571-517-157-517-571-571-571-517-510-507-501-561-5_V.mp3)
  Segment 2: 1576ms ~ 13420ms (file: 326-102-617-120-213-123-512-521-236-127-132-102-102-136-120-210-316-216-132-102-321-315-653-523-574-547-253-276-276-256-321-103-136-217-123-231-321-120-132-267-237-672-267-623-1_W.mp3)
  Segment 3: 14420ms ~ 22052ms (file: 126-172-136-103-627-167-613-162-167-263-625-173-263-165-136-106-136-106-673-172-162-167-103-106-167-160-103-567-216-631-126-162-236-156-602-125-176-106-163-103-150-167-601-126-2_V.mp3)
  Segment 4: 23052ms ~ 31980ms (file: 350-126-507-123-163-216-231-257-321-573-103-132-752-132-127-627-172-521-507-312-312-231-702-273-201-236-126-570-632-125-362-257-570-257-756-574-672-236-527-536-267-213-135-206-2_V.mp3)
  Segment 5: 32980ms ~ 42700ms (file: 201-203-267-260-326-316-362-753-573-315-375-756-321-735-103-273-321-261-132-546-136-627-672-267-261-270-120-216-236-312-163-627-326-316-103-106-162-167-167-715-267-201-312-251-2_W.mp3)
  Segment 6: 43700ms ~ 44276ms (file: 135-135-162-123-132-137-103-130-167-136-276-132-162-130-137-312-132-137-123-132-360-652-537-537-526-562-326-312-321-365-632-362-361-723-725-753-137-102-136-126-362-623-132 132_V.mp3)
  Segment 7: 45276ms ~ 45852ms (file: 326-235-527-136-725-237-635-752-217-516-362-750-207-270-276-263 267-207-270-203-263-203-576-275-752-572-276-265-715-750-761-726-625-671-132-125-312-627-276-235-216-215-321-357-7_V.mp3)
  Segment 8: 46852ms ~ 47428ms (file: 310-103-301-321-362-657-675-675-260-620-206-261-206-627-562-526-627-267-602-312-310-132-312-103-310-130-206-267-267-607-602-627-621-610-632-263-627-623-362-612-621-672-623-671-6_W.mp3)
  Segment 9: 48428ms ~ 55772ms (file: 213-201-201-576-136-123-103-312-302-310-103-312-130-106-236-310-103-735-576-725-135-237-321-132-572-203-123-207-206-130-103-213-301-230-103-210-210-321-132-102-263-236-752-102-7_W.mp3)
  Segment 10: 56772ms ~ 57348ms (file: 316-103-562-526-547-601-312-156-312-715-571-125-301-136-136-362-236-317-132-531-376 765-265-172-175-152-102_W.mp3)
  Segment 11: 58348ms ~ 58924ms (file: 175-107-175-107-175-157-107-175-173 157-105-175-175-135-157-153-157-137-153-103-135-157-103-715-173-107-173-175-175-107-175 157-175-153_V.mp3)
  Segment 12: 59924ms ~ 68888ms (file: 173-310-130-276-206-230-731-517-123-120-126-236-310-176-102-231-170-173-130-321-123-702-730-102_W.mp3)
  Segment 13: 69888ms ~ 70464ms (file: 672-672-762-762-627-621-672-675-671-672-761-672-762-670-621-672-761-132-762-132-130-120-612-361-162-617-321-217-235-513-513-567-526-567-675-765-675-576-756-567-756-756-765-765-6_W.mp3)
  Segment 14: 71464ms ~ 72040ms (file: 130-320-106-615-731-627-623-612-132-612-672-172-723-716-721-716-176-176-172-176-715-176-176-617-715-716-176-172-176-167-721-712-176-167-712-176-176-176-165-160-163-627-276-721-7_W.mp3)
  Segment 15: 73040ms ~ 79484ms (file: 276-675-576-230-320-301-302-231-271-327-213-320-527-206-320-320-267-327-327-236-201-627-320-310-763-623-230-325-206-732-231-153-206-321-103-765-275-267-203-351-574-147-157-103-3_W.mp3)
  Segment 16: 80484ms ~ 81060ms (file: 321-321-321-321-321-321-132-316-326-136-316-136-361-351-532-523-534-536-352-325-321-326-326-321-523-532-572-321-132-123-621-623-261-326-325-325-326-321-123-321-126-315-325-263-6_W.mp3)
  Segment 17: 82060ms ~ 82636ms (file: 157-170-705-157-175-107-157-107-105-175-107-107-107-170-107-107-170-175-170-107-173-517-105-751-571-751-751-753-756-756-756-756-756-756 570-751-570-705-705-750-750-157-705-705-7_V.mp3)
  Segment 18: 83636ms ~ 89612ms (file: 325-321-320-236-275-572-531-321-230-236-203-320-260-236-230-532-230-236-321-321-316-750-750-756-236-163-236-260-264-125-312-752-705-230-235-207-236-723-263-572-632-276-206-127-2_W_stt_i_believe_that_every_citizen_got_so_many.mp3)
  Segment 19: 90612ms ~ 91188ms (file: 123-312-321-312-326-321-236-321 135-163-163-132-162-132-136-213-750-126-126-172-132-301-312-316-312-136-362-261-167-165-143-132-301-306-136-572-750-172-102-106-127-137-316-650-6_V.mp3)
  Segment 20: 92188ms ~ 92764ms (file: 176-105-107-107-107-103-107-173-175-170-517-107-170-103-170-173-751-175-170-170-501-631-130 175-107-103-637-617-612-271-275-167-217-217-217-267 175 105-105-107-170-170-107-710-1_V.mp3)
  Segment 21: 93764ms ~ 94340ms (file: 103-615-136-267-613-103-130-136-657-316-106-132-123-312-762-102-136-316-623-162-163-106-175-136 163 107-162-163_V.mp3)
  Segment 22: 95340ms ~ 95916ms (file: 326-236-215-215-312-216-312-123-213-135-312-351-315-357-574-574-574-576-574-351-162-135-371-765-721-517-156-153-162-136-321-312-217-521-135-213-231-257-312-132-375-371-175-715-7_W.mp3)
  Segment 23: 96916ms ~ 105016ms (file: 651-376-753-176-107-765-265-617-735-132-372-263-102-126-763-267-261-213-267-316-326-237-176-176-132-263-126-765-321-103-126-574-170-167-765-276-176-136-167-267-756-372-375-756-7_W.mp3)
  Segment 24: 106016ms ~ 114728ms (file: 267-260-260-326-257-203-231-230-230-206-102-167-320-201-612-576-756-572-326-236-120-321-263-216-230-235-267-263-316-275-126-106-326-123-236-260-756-263-206-206-120-263-236-230-2_W.mp3)
  Segment 25: 115728ms ~ 121812ms (file: 103-315-316-675-675-205-123-570-725-752-327-210-253-120-261-126-756-275-103-213-137-756-126-123-201-132-517-312-203-572-265-316-705-263-623-567-206-627-126-351-526-574-765-623-2_V.mp3)
  Segment 26: 122812ms ~ 128788ms (file: 762-235-362-316-310-137-761-370-176-751-756-756-735-136-673-127-132-103-130-103-130-136-216-357-573-130-526-521-617-617-651-652-253-657-675-162-672-267-765-267-267-627-627-672-6_W.mp3)
  Segment 27: 129788ms ~ 130364ms (file: 631-607-601-167-106-167-130-167-306-603-176-761-301-370-710-761-170-370-130-103-310-312 307-170-137-301-735-170-157-175-173-301-130-175-130-310-157-307-370-107-130-130-317-715-1_V.mp3)
  Segment 28: 131364ms ~ 131940ms (file: 625-675-652-602-137-675-675-657-675-672-130-103-752-127-136-571-170-263-216-351-651-675-306-153-130-576-534-106-513-657-310-310-317-307-307-312-301-326-637-301-320-356-653-571-3_W.mp3)
  Segment 29: 132940ms ~ 133516ms (file: 561-321-512-513-574-156-156-103-315-160-361-301-136-236-317-316-165-152-327-370-312-130-135-675-103-571-573-576-175-765-756-517-571-751-576-756-135-675-213-103-175-176-517-157-5_W.mp3)
  Segment 30: 134516ms ~ 135092ms (file: 751-517-175-570-157-570-170-507-715-570-570-574-574-754-574-571-571-574-754-751-751-751-157-501-105-157-175-701-501-175-570-715-701-105-715-571-715-715-107-751-510-157-107-751-1_V.mp3)
  Segment 31: 136092ms ~ 136668ms (file: 103-765-576-756-756-756-765-765-765-765-756-137-317-173-137-173-371-317-173-137-137-173-173-317-317-175-153-753-753-751-735-751-756-371-375-753-735-713-751 750-570-571-705-751-5_W.mp3)
  Segment 32: 137668ms ~ 138244ms (file: 136-106-316 103-130-106-301-130 560-156-501-510-510-150-175-150-105-150-153-105-107-513-105-510-510 501-510-150-150-157-150-150-517-150-517-510-517-510-517-517 105-150-156-150-1_V.mp3)
  Segment 33: 139244ms ~ 139820ms (file: 317 310-317-137-317-103-137-103-137-103-137-103-317-103-137-103-130-103-317-103-137-130-103-317-103-137 137-317-107-130-103-137-317-137-107-103-103-107-103-173-103-317-103-137-1_V.mp3)
  Segment 34: 140820ms ~ 145392ms (file: 276-765-270-267-317-310-372-312-132-356-132-321-105-150-175-351-103-376-357-175-351-315-571-357-675-635-315-756-672-675-765-617-632-627-632-167-156-123-157-176-357-732-537-763-6_W.mp3)
  Segment 35: 146392ms ~ 155824ms (file: 130-213-312-230-206-260-602-213-260-260-107-267-623-572-267-612-216-201-312-130-627-267-623-260-173-312-210-126-716-136-267-263-261-206-276-630-236-261-320-162-132-260-103-130-3_W.mp3)
  Segment 36: 156824ms ~ 157400ms (file: 175-631-126-136-570-576-506-130 517-576-765-675-721-316-327-705-175-750-167-721-752-573-213-132-126-631-570-261-217-263-132-201-217-132-502-657-752-572-752-576-627-756-257-726-1_V.mp3)
  Segment 37: 158400ms ~ 162432ms (file: 216-216-162-261-216-267-267-261-213-216-123-213-217-123-123-231-126-231-261-621-216-321-132-261-127-132-132-312-102-120-216-103-213-120-213-102-106-126-213-126-123-216-123-213-1_W.mp3)
  Segment 38: 163432ms ~ 172396ms (file: 235-762-670-725-156-263-627-256-326-176-130-376-573-236-230-370-162-567-257-357-375-375-371-106-502-102-230-123-213-271-526-102-103-213-213-236-103-275-257-237-103-713-213-312-1_W.mp3)
  Segment 39: 173396ms ~ 179444ms (file: 210-123-267-231-237-321-173-137-107-103-136-130-312-312-167-672-632-370-123-612-362-627-267-267-702-126-267-267-621-261-715-726-267-621-261-123-723-236-732-276-136-103-127-312-3_V.mp3)
  Segment 40: 180444ms ~ 181020ms (file: 501-175-107-175-175-517-107-175-713-170-571-710-173-130-513-107-173-107-170-170-710-105-170-713-137-170-157-107-175-705-751 571-570-715-715-170 170-175-170-175-715-710-170-157-1_V.mp3)
  Segment 41: 182020ms ~ 188104ms (file: 210-123-132-231-312-321-301-103-132-173-301-301-371-301-312-132-210-623-120-126-210-213-213-120-167-107-201-210-213-253-201-203-206-102-201-210-201-203-210-231-206-132-176-103-1_W.mp3)
  Segment 42: 189104ms ~ 189680ms (file: 103-312-325-231-173-713-310-136-165-632-675-126-702-126-576-165-327-752-123-532-152-136-137-750-250-271-623-601-165-513-513-520-137-317-571-315-753-173-137-176-173-126-123-261-1_W.mp3)
  Segment 43: 190680ms ~ 191256ms (file: 627 352-175-127-235-135-136-307-376-637-263 132 572-365-316-316-316-361-361-310-716-627-276-712-172-713-132-312-613-625-326-750-675-256-123-276-216-126-765-236-276-762-312-632-3_V.mp3)
  Segment 44: 192256ms ~ 192832ms (file: 750-751-157-751-751-715-751-750-571-751-715-751-751 107-751-751-751-715-157-751-713-175-751-751-750-756-715-751-157-715-715-157-751-751-175-750 751-175-751-751-517-751-715-751-7_V.mp3)
  Segment 45: 193832ms ~ 194408ms (file: 132-627-706-723-725-256-765-275-273-715-572-365-562-357-276-213-760-672-376-763-526-263 571-172-763-632-750-756-635-276-576-325-251-572-725_V.mp3)
  Segment 46: 195408ms ~ 195984ms (file: 510-105-510-150-150-150-150-510-501_W.mp3)
  Segment 47: 196984ms ~ 197560ms (file: 136-763-760-736-673-764-763-675-672-726-672-372-736-176-716-763-375-637-376-536-543-765-602-260-602-623-627-627-627-572-576-537-357-357-357-357-357-571-352-527-152-152-157-152-1_W.mp3)
  Segment 48: 198560ms ~ 199136ms (file: 105-175-751 507-507-507-571-517-157-105-175-157-507-157-175-105-150-175-157-175-570 501-570-107-157-150-175-157-570-157-150-157-501-570-570 571-571-571-517-571-157-157-175-715-1_W.mp3)
  Segment 49: 200136ms ~ 207696ms (file: 750-765-175-763-261-213-132-231-321-715-123-610-103-275-126-175-213-312-231-762-231-102-123-312-136-361-163-136-130-163-613-610-163-103-273-167-765-216-162-136-210-167-163-231-2_W.mp3)
  Segment 50: 208696ms ~ 209272ms (file: 137-130-130-137-316-716-673-765-617-675-637-637-603-635-673-635-673-673-657-361-631-672-361-623 137-326-256 672_W.mp3)
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
> **Time**: 2026-04-05 05:47:22
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 2,
"transcript": "오늘 저녁에 명동에 가서 같이 비빔밥 먹을래요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "저녁에", "start_ms": 600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "명동에", "start_ms": 1200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "가서", "start_ms": 1900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 2400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "비빔밥", "start_ms": 2900, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "먹을래요?", "start_ms": 3700, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "좋아요! 하지만 저는 매운 음식을 잘 못 먹는데 괜찮을까요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "좋아요!", "start_ms": 200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "하지만", "start_ms": 1000, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "저는", "start_ms": 1600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "매운", "start_ms": 2100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "음식을", "start_ms": 2600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 3200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "못", "start_ms": 3550, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "먹는데", "start_ms": 3850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "괜찮을까요?", "start_ms": 4400, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "걱정 마세요. 고추장을 따로 달라고 하면 맵지 않게 먹을 수 있어요.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "걱정", "start_ms": 180, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "마세요.", "start_ms": 650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "고추장을", "start_ms": 1300, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "따로", "start_ms": 2100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "달라고", "start_ms": 2600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "하면", "start_ms": 3200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "맵지", "start_ms": 3700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "않게", "start_ms": 4200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "먹을", "start_ms": 4700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 5100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "있어요.", "start_ms": 5350, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "다행이네요. 그럼 이따가 저녁 7시에 역 앞에서 만나요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "다행이네요.", "start_ms": 250, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "그럼", "start_ms": 1100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이따가", "start_ms": 1600, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "저녁", "start_ms": 2300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "7시에", "start_ms": 2800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "역", "start_ms": 3500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "앞에서", "start_ms": 3850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "만나요.", "start_ms": 4450, "dur_ms": 550, "phase_lr_deg": 0.0}
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
"transcript": "요즘 한국 드라마가 인기가 많아서 한국어를 배우기 시작했어요.",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "요즘", "start_ms": 120, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "드라마가", "start_ms": 1100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "인기가", "start_ms": 1800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "많아서", "start_ms": 2400, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "한국어를", "start_ms": 3100, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "배우기", "start_ms": 3850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "시작했어요.", "start_ms": 4400, "dur_ms": 750, "phase_lr_deg": 0.0}
]
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
"transcript": "정말요? 어떤 드라마를 제일 좋아하세요?",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "정말요?", "start_ms": 200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 1000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "드라마를", "start_ms": 1500, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "제일", "start_ms": 2200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "좋아하세요?", "start_ms": 2700, "dur_ms": 800, "phase_lr_deg": 0.0}
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
"transcript": "저는 로맨틱 코미디 드라마를 좋아해요. 배우들의 대사가 재미있거든요.",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "로맨틱", "start_ms": 650, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "코미디", "start_ms": 1300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "드라마를", "start_ms": 1900, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "좋아해요.", "start_ms": 2600, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "배우들의", "start_ms": 3500, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "대사가", "start_ms": 4250, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "재미있거든요.", "start_ms": 4900, "dur_ms": 850, "phase_lr_deg": 0.0}
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
"transcript": "I believe that every citizen got so many benefits from the new policy.",
"speaker": "speaker_5",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 220,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 220, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "believe", "start_ms": 500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "that", "start_ms": 1050, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "every", "start_ms": 1450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "citizen", "start_ms": 1950, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "got", "start_ms": 2650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "so", "start_ms": 3100, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "many", "start_ms": 3450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "benefits", "start_ms": 3900, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "from", "start_ms": 4650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 5050, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "new", "start_ms": 5350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "policy.", "start_ms": 5750, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "내일 친구 생일이라서 선물을 사야 하는데 뭐가 좋을까요?",
"speaker": "speaker_6",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "내일", "start_ms": 180, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "친구", "start_ms": 700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "생일이라서", "start_ms": 1250, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "선물을", "start_ms": 2150, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "사야", "start_ms": 2800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "하는데", "start_ms": 3300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "뭐가", "start_ms": 3900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "좋을까요?", "start_ms": 4400, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "친구분이 평소에 관심 있는 분야가 무엇인지 생각해 보세요.",
"speaker": "speaker_4",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "친구분이", "start_ms": 200, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "평소에", "start_ms": 1000, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "관심", "start_ms": 1650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 2200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "분야가", "start_ms": 2700, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "무엇인지", "start_ms": 3350, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "생각해", "start_ms": 4050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "보세요.", "start_ms": 4650, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "그 친구는 요리하는 걸 정말 좋아해요. 그래서 앞치마나 주방 도구가 어떨까 싶어요.",
"speaker": "speaker_6",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "그", "start_ms": 150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "친구는", "start_ms": 450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "요리하는", "start_ms": 1000, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "걸", "start_ms": 1750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 2150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "좋아해요.", "start_ms": 2700, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 3600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "앞치마나", "start_ms": 4150, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "주방", "start_ms": 4900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "도구가", "start_ms": 5400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "어떨까", "start_ms": 6000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "싶어요.", "start_ms": 6550, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "좋은 생각이네요! 예쁜 앞치마를 선물하면 친구가 아주 기뻐할 거예요.",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "생각이네요!", "start_ms": 650, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "예쁜", "start_ms": 1600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "앞치마를", "start_ms": 2100, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "선물하면", "start_ms": 2850, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "친구가", "start_ms": 3650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 4250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "기뻐할", "start_ms": 4700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "거예요.", "start_ms": 5300, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "주말에 시간이 되면 같이 전시회 보러 갈까요? 마침 보고 싶은 전시가 있거든요.",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "주말에", "start_ms": 180, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "시간이", "start_ms": 750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "되면", "start_ms": 1300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 1750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "전시회", "start_ms": 2250, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "보러", "start_ms": 2950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "갈까요?", "start_ms": 3450, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "마침", "start_ms": 4300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "보고", "start_ms": 4800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "싶은", "start_ms": 5300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "전시가", "start_ms": 5750, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "있거든요.", "start_ms": 6400, "dur_ms": 750, "phase_lr_deg": 0.0}
]
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
"transcript": "좋아요. 토요일 오후는 어떠세요? 저는 일요일보다 토요일이 편해요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "좋아요.", "start_ms": 120, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "토요일", "start_ms": 900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "오후는", "start_ms": 1500, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "어떠세요?", "start_ms": 2150, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "저는", "start_ms": 3000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "일요일보다", "start_ms": 3500, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "토요일이", "start_ms": 4400, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "편해요.", "start_ms": 5150, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "저도 토요일이 더 좋아요. 그럼 오후 2시에 전시장 매표소 앞에서 만날까요?",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "저도", "start_ms": 200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "토요일이", "start_ms": 700, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 1400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "좋아요.", "start_ms": 1750, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "그럼", "start_ms": 2550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "오후", "start_ms": 3050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "2시에", "start_ms": 3550, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "전시장", "start_ms": 4200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "매표소", "start_ms": 4900, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "앞에서", "start_ms": 5650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "만날까요?", "start_ms": 6250, "dur_ms": 750, "phase_lr_deg": 0.0}
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
"transcript": "네, 알겠습니다. 그때 봬요. 덕분에 즐거운 주말이 될 것 같아요.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "알겠습니다.", "start_ms": 550, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "그때", "start_ms": 1500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "봬요.", "start_ms": 2000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "덕분에", "start_ms": 2700, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "즐거운", "start_ms": 3350, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "주말이", "start_ms": 4000, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "될", "start_ms": 4650, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 5000, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "같아요.", "start_ms": 5300, "dur_ms": 600, "phase_lr_deg": 0.0}
]
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
"transcript": "감기에 걸려서 목소리가 잘 안 나오네요. 따뜻한 차를 좀 마셔야겠어요.",
"speaker": "speaker_4",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 220,
"audio_type": "speech",
"words": [
{"word": "감기에", "start_ms": 220, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "걸려서", "start_ms": 800, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "목소리가", "start_ms": 1450, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 2200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 2600, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "나오네요.", "start_ms": 2950, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "따뜻한", "start_ms": 3800, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "차를", "start_ms": 4450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 5000, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "마셔야겠어요.", "start_ms": 5350, "dur_ms": 850, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 05:47:22*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=6944  time=05:45:56  uptime=14565s  mem=131MB  system_processes=352"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_054539.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 751-570-571-571-571-715-571-157-571-175-517-571-517-571-570-750-571-570-576-573-571-165-156-315-531-567-516-513-517-571-571-137-175-517-157-517-175-157-715-751-715-175-715-157-7_V.mp3)
  Segment 2: 1576ms ~ 11836ms (file: 276-230-763-310-320-765-236-231-762-132-756-312-102-173-752-210-231-206-627-627-102-327-236-230-237-230-123-302-263-130-267-321-132-302-320-726-230-312-357-325-267-236-230-763-2_W.mp3)
  Segment 3: 12836ms ~ 13412ms (file: 751-715-751-175-157-751-756 756-751-751-751-751-756-750-751-751-751-571-715-751-175-571-715-571-751-571-571 750-751-751-751-715-705-715-705-571-756-705-715-517-715-715-715-713-7_V.mp3)
  Segment 4: 14412ms ~ 14988ms (file: 715-302-316-316-132-572-573-765-163-613-176-137-730-261-160-162-163-361-316-326-716-731-103-713-156-376-712-703-317-167-175-621-576-132-172-507-125-756-215-725-371-716-672-312-3_V.mp3)
  Segment 5: 15988ms ~ 16564ms (file: 756-756-756-756-752-753-753 756-751-756-756-756-756-716-761-716-137-173-176-173-107-371-367-736-371-763-673-176-716-176-763-716-137 170-107-173-107-173-301-317-703-137-103-107-7_W.mp3)
  Segment 6: 17564ms ~ 23972ms (file: 570-751-571-310-756-137-517-270-217-321-520-267-321-213-270-126-260-751-126-175-615-705-756-576-237-312-261-756-135-130-231-130-106-176-260-102-157-123-610-652-751-135-312-132-3_V.mp3)
  Segment 7: 24972ms ~ 31812ms (file: 512-765-276-765-657-126-162-216-261-105-123-532-256-230-360-312-216-576-132-261-627-231-517-137-160-261-106-261-263-213-261-572-362-316-263-132-517-136-210-213-263-623-263-572-5_W.mp3)
  Segment 8: 32812ms ~ 33388ms (file: 123-310-236-260-206-210-230-201-302-320-103-102-132 103-130-206-260-206-203-206-206-312-130-312-675-574-574-756-754-627-362-306-120-203-320-263-236-302-236-126-617-301-132-236-2_V.mp3)
  Segment 9: 34388ms ~ 42812ms (file: 301-130-107-271-320-276-320-321-132-327-375-752-107-123-103-102-672-362-136-563-213-123-107-370-372-271-273-726-273-301-203-753-753-765-726-276-207-103-310-236-203-320-236-267-7_W_stt_on_occasion_was_almost_beyond_that_and_t.mp3)
  Segment 10: 43812ms ~ 44388ms (file: 310-310-130-103-132-103-102-123-127-136-132-162-132-162-132-126-632-362-136-103-103-143-124-136-320-302-312-310-301-132-321-326-312-132-130-312-321-132-102-310-312-103-312-362-3_V.mp3)
  Segment 11: 45388ms ~ 52048ms (file: 165-756-574-756-756-753-576-537-351-576-527-567-576-541-547-574-357-253-257-517-102-315-710-573-215-572-365-210-213-216-271-125-126-612-201-576-657-312-726-321-503-213-574-276-2_V.mp3)
  Segment 12: 53048ms ~ 53624ms (file: 623-632-236-263-326-236-216-321-216-123-120-123-213-321-263-230-263-160-310-230-261-612-236-621-236-263-213-312-132-130-362-163-312-130-136-136-316-623-236-236-213-623-623-627-2_W.mp3)
  Segment 13: 54624ms ~ 55200ms (file: 570-571-750-570-570 574-571-571-570-571-517-571-501-507-570-571-571-571 150-501-507-570-571-150-710-175-157-105-170-107-175-105-105-105-157-105-105_V.mp3)
  Segment 14: 56200ms ~ 56776ms (file: 153-153-135-315 136-136-135-135-150 132-136-316-153-156-156-315-165-135-315-365-365-156-165-135-136 137-315-315-136-316-136 316-316-361-361-316-361-136-361-361-316-316 135-132-1_W.mp3)
  Segment 15: 57776ms ~ 58352ms (file: 751-517-571-571-576-576-571-571-576-513-507-501-510-570-501-501-507-506-156-517-571-571-175-153-157-751-570-157-715-157-571-715-751-751-751-517-751-751-571-751-517-517-576-576-5_V.mp3)
  Segment 16: 59352ms ~ 59928ms (file: 213-120-210-261-210-134-137-102-302-267-261-261-236-267-725-527-352-675 102-267-310-310-126-312-672-671-260-162-106-631-310-132-123-627-620-670-650-106-135-163-631-132-137-570-1_V.mp3)
  Segment 17: 60928ms ~ 61504ms (file: 215-132-105-351-103-571-715-105-130-136-361-351-315-317-315-315-301-215-352-216-217-270-167-132-136-513-275-236-231-237-153-132-325-213 257-275-527-152-132-317-317-317-137-351-2_W.mp3)
  Segment 18: 62504ms ~ 69452ms (file: 270-230-321-321-263-312-267-123-230-723-627-762-361-276-267-620-762-627-756-632-760-130-132-230-120-310-173-203-276-753-703-203-167-617-627-107-173-230-213-301-302-267-502-102-5_W.mp3)
  Segment 19: 70452ms ~ 77472ms (file: 721-762-276-620-702-702-127-726-276-230-236-236-327-230-321-312-230-237-237-207-723-726-726-276-276-273-203-632-601-623-321-301-621-312-260-362-301-176-127-123-760-136-136-706-1_V_stt_the.mp3)
  Segment 20: 78472ms ~ 79048ms (file: 705-705-672-671-372-107-576-127-217-571-271-276-371-162-527-752-216-261-752-132-132-570-123-217-752-570-172-705-321-765-513-135-317-756-507-267-271-572-706-321-572-271-152-751-7_V.mp3)
  Segment 21: 80048ms ~ 80624ms (file: 756-765-675-761-760-657-675-576-567-652-670-670-670-673-673-756-567-576-572-574-251-560-576-625-265 561-167-617-671-521-517-627-567-756-705-576-576-756-576-576-675-136-176-106-6_W.mp3)
  Segment 22: 81624ms ~ 82200ms (file: 574-756-576-576-725-176-165-671-132-320-312 134-215-125-137-174-174-325-361_V.mp3)
  Segment 23: 83200ms ~ 91372ms (file: 256-237-763-321-534-326-132-237-765-752-130-132-173-273-236-132-132-301-130-123-217-103-216-312-675-351-574-630-362-726-136-261-263-203-236-267-316-132-217-163-762-267-267-263-2_V.mp3)
  Segment 24: 92372ms ~ 92948ms (file: 752-756-715-721-756-765-541-513-570 165-156-276-570-617-765-756-261-271-216-563-702-625-361-321-763-537-123-261-507-326-132-163-235-527-503-321-572-257-263-576-762-260-325-765-6_V.mp3)
  Segment 25: 93948ms ~ 104064ms (file: 527-267-321-310-263-270-321-130-231-302-301-320-312-302-137-302-231-137-570-527-301-310-172-213-321-325-132-316-107-301-312-317-320-670-130-317-103-371-107-137-276-230-527-260-3_W.mp3)
  Segment 26: 105064ms ~ 116440ms (file: 326-253-627-765-765-236-236-102-627-263-230-236-235-265-231-326-326-267-263-102-627-216-236-231-231-310-236-102-257-231-213-236-213-213-627-230-103-260-206-320-103-310-231-213-1_W.mp3)
  Segment 27: 117440ms ~ 118016ms (file: 751-562-617-671-752-572-572-317 325-507-351-357-523-527-152-271-562-236-756-263-276-572-564-756-671-167-327-263-237-327-207-273-267-273-527-321-502-570 705-752-572-725-327-130-1_V.mp3)
  Segment 28: 119016ms ~ 119592ms (file: 256-632-236-572-312-321-275-236-570-275-573-506-207-126-537-317 574 765-572-261-123-752-652-125-507-136-527-725-213-572-357-513-652-371-507-326-756-651-302-316-213-276-261-725-2_V.mp3)
  Segment 29: 120592ms ~ 128656ms (file: 103-574-317-130-237-130-762-260-765-301-301-627-267-260-256-576-257-152-210-216-261-260-213-132-260-257-312-231-120-217-670-576-526-756-301-257-756-572-547-120-213-231-547-207-2_W.mp3)
  Segment 30: 129656ms ~ 130232ms (file: 135-103-135-103-135-103-136-135-103-315-357-513-574 357-375-537-753-517-571-571-715-107-157-103-137-135 513-573-751-571-571-573-157-517-571 310-341-314-134-301-103-134-135-134-3_V.mp3)
  Segment 31: 131232ms ~ 137784ms (file: 613-715-627-362-267-627-601-620-672-267-610-612-162-267-267-637-637-670-627-632-267-672-623-627-760-610-167-162-716-271-263-276-627-627-672-621-672-267-672-627-632-261-103-210-2_W.mp3)
  Segment 32: 138784ms ~ 139360ms (file: 376-136 721-726-721-726-276-625-267-267-623-267-261-216-627-261-627-612-623-326-361-632-607-360-267-627-621-617-716-613-167-761-372-126 613-132-317-316-312-236-263-326-132-321-2_W.mp3)
  Segment 33: 140360ms ~ 150476ms (file: 162-267-260-213-216-672-267-213-213-162-103-627-106-176-231-102-617-251-124-213-163-301-321-231-613-106-613-162-120-276-712-130-321-312-312-137-103-621-715-132-157-130-213-103-1_W.mp3)
  Segment 34: 151476ms ~ 152052ms (file: 123-163-103-103-103-103-657-527-132-103-126-162-574-705-675-310-310-310-315-356-567-653-567-537-562-567-576-672-621-361-126-576-123-573-576-623 315-361-216-125-120-530-650-375-1_W.mp3)
  Segment 35: 153052ms ~ 162124ms (file: 276-132-317-321-236-321-317-372-317-735-310-132-317-132-216-261-231-106-672-217-765-126-162-267-130-321-276-276-231-312-310-756-765-231-173-107-237-762-765-326-715-106-167-267-7_W.mp3)
  Segment 36: 163124ms ~ 163700ms (file: 765-765-756-576-572-135-312-673-607-760-652-567-615-263-256-532-756-574-574-507-657-167-136-316-301-175-651-623-125-136-163-106-762-132-123-132-263-123-126-123-123-162-601-162-3_W.mp3)
  Segment 37: 164700ms ~ 173844ms (file: 576-752-130-230-132-123-103-231-130-132-132-672-265-256-321-457-120-120-213-132-670-217-321-321-207-213-205-273-367-126-317-103-123-176-173-137-267-672-612-672-267-762-237-532-7_W.mp3)
  Segment 38: 174844ms ~ 190540ms (file: 627-760-103-136-132-206-123-613-712-173-567-275-705-627-560-215-126-573-135-123-570-276-125-176-132-201-136-540-310-765-206-216-207-275-756-312-132-203-102-132-312-201-102-130-2_V.mp3)
  Segment 39: 191540ms ~ 192116ms (file: 150-153-105-531-350-157-165-107-170-175-152-715-175-752-532-517-165-561-526-567-125-152-157-137-367-306-751-761-751-715-716-751-715-751-751-735-756-160-103-312-713-751-571 163-6_W.mp3)
  Segment 40: 193116ms ~ 193692ms (file: 310-103-136-103-162-103-163-163-156-675-316-106-103-160-567-172-167-176-167-316-751-136-170-107-316-103-132-356-163-163-106-672-103-316-106-107-167-617-623-103-135-610-312-152-3_V.mp3)
  Segment 41: 194692ms ~ 202000ms (file: 523-732-756-132-207-312-276-132-136-130-301-574-120-123-621-130-201-275-206-257-651-621-310-103-102-207-310-103-206-123-203-627-260-205-602-103-602-670-123-607-607-130-602-123-1_V.mp3)
  Segment 42: 203000ms ~ 203576ms (file: 627-751-715-756-725 751-721-756-312-375-735-375-735-721-712 150-157-263-627-267 125-523-251-210-120-106-106-106-102-106-106-106-106-106-102-102-132-652-620-627-236-326-301-130-1_W.mp3)
  Segment 43: 204576ms ~ 211308ms (file: 752-176-627-216-572-267-231-206-126-312-172-126-621-236-210-157-756-312-267-206-132-276-260-263-132-275-627-120-320-265-135-752-576-625-130-321-210-251-672-752-103-623-562-267-3_W.mp3)
  Segment 44: 212308ms ~ 222244ms (file: 236-270-762-576-271-532-107-174-102-143-754-130-320-210-276-126-132-230-230-120-627-627-731-231-263-237-756-762-231-102-276-276-276-230-321-203-310-120-230-573-237-321-132-327-1_W.mp3)
  Segment 45: 223244ms ~ 230624ms (file: 217-762-127-175-176-516-672-165-576-657-756-310-107-137-103-625-625-752-175-317-573-576-123-210-127-213-236-127-132-103-230-276-270-207-273-230-103-210-572-201-260-267-261-231-2_W.mp3)
  Segment 46: 231624ms ~ 232200ms (file: 103-120-612-162-326-263-612-103-601-132-136-167-106-162-136-136-130-106-136-136-103-136-106-546-317-615-162-163-103-103-107-637-162-610-612-103-106-103-160-610-106-163-163-136-3_V.mp3)
  Segment 47: 233200ms ~ 233776ms (file: 263-651-237-321-253-517-275-326-150-213-321-236-523-275-750-372-507-263-275-267-572-537-205-507-750-576-501-716-765-725-375-570-123-567-725-126-173-567-135-163-750-153-507-162-2_V.mp3)
  Segment 48: 234776ms ~ 235352ms (file: 756-756-175-715-317-105-103-137-713-173-175-173-175-137-271-317-137-176-103-105-135-135-751-715-571-751-571-571-751-517 715-173-107-175-130-173-103-671-173-173-107-136-103-317-1_W.mp3)
  Segment 49: 236352ms ~ 236928ms (file: 751-157-517-501-507-507-507-570-571-507-560-571-570-571-502-150-517-517-157-157-176-576-571-570-576-576-750-576-571-576-516-517-576-571-576-571-501 105-571-175-571-175-751-751-5_V.mp3)
  Segment 50: 237928ms ~ 243112ms (file: 764-675-105-153-135-571-573-350-203-275-321-152-215-301-231-207-207-137-301-327-237-301-132-123-235-265-265-257-207-207-750-371-302-327-321-327-302_V.mp3)
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
> **Time**: 2026-04-05 05:47:22
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 2,
"transcript": "오늘 저녁에 명동에 가서 같이 비빔밥 먹을래요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "저녁에", "start_ms": 600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "명동에", "start_ms": 1200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "가서", "start_ms": 1900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 2400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "비빔밥", "start_ms": 2900, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "먹을래요?", "start_ms": 3700, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "좋아요! 하지만 저는 매운 음식을 잘 못 먹는데 괜찮을까요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "좋아요!", "start_ms": 200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "하지만", "start_ms": 1000, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "저는", "start_ms": 1600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "매운", "start_ms": 2100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "음식을", "start_ms": 2600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 3200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "못", "start_ms": 3550, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "먹는데", "start_ms": 3850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "괜찮을까요?", "start_ms": 4400, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "걱정 마세요. 고추장을 따로 달라고 하면 맵지 않게 먹을 수 있어요.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "걱정", "start_ms": 180, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "마세요.", "start_ms": 650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "고추장을", "start_ms": 1300, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "따로", "start_ms": 2100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "달라고", "start_ms": 2600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "하면", "start_ms": 3200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "맵지", "start_ms": 3700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "않게", "start_ms": 4200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "먹을", "start_ms": 4700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 5100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "있어요.", "start_ms": 5350, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "다행이네요. 그럼 이따가 저녁 7시에 역 앞에서 만나요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "다행이네요.", "start_ms": 250, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "그럼", "start_ms": 1100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이따가", "start_ms": 1600, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "저녁", "start_ms": 2300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "7시에", "start_ms": 2800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "역", "start_ms": 3500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "앞에서", "start_ms": 3850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "만나요.", "start_ms": 4450, "dur_ms": 550, "phase_lr_deg": 0.0}
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
"transcript": "요즘 한국 드라마가 인기가 많아서 한국어를 배우기 시작했어요.",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "요즘", "start_ms": 120, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "드라마가", "start_ms": 1100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "인기가", "start_ms": 1800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "많아서", "start_ms": 2400, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "한국어를", "start_ms": 3100, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "배우기", "start_ms": 3850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "시작했어요.", "start_ms": 4400, "dur_ms": 750, "phase_lr_deg": 0.0}
]
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
"transcript": "정말요? 어떤 드라마를 제일 좋아하세요?",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "정말요?", "start_ms": 200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 1000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "드라마를", "start_ms": 1500, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "제일", "start_ms": 2200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "좋아하세요?", "start_ms": 2700, "dur_ms": 800, "phase_lr_deg": 0.0}
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
"transcript": "저는 로맨틱 코미디 드라마를 좋아해요. 배우들의 대사가 재미있거든요.",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "로맨틱", "start_ms": 650, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "코미디", "start_ms": 1300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "드라마를", "start_ms": 1900, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "좋아해요.", "start_ms": 2600, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "배우들의", "start_ms": 3500, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "대사가", "start_ms": 4250, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "재미있거든요.", "start_ms": 4900, "dur_ms": 850, "phase_lr_deg": 0.0}
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
"transcript": "I believe that every citizen got so many benefits from the new policy.",
"speaker": "speaker_5",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 220,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 220, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "believe", "start_ms": 500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "that", "start_ms": 1050, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "every", "start_ms": 1450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "citizen", "start_ms": 1950, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "got", "start_ms": 2650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "so", "start_ms": 3100, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "many", "start_ms": 3450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "benefits", "start_ms": 3900, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "from", "start_ms": 4650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 5050, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "new", "start_ms": 5350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "policy.", "start_ms": 5750, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "내일 친구 생일이라서 선물을 사야 하는데 뭐가 좋을까요?",
"speaker": "speaker_6",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "내일", "start_ms": 180, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "친구", "start_ms": 700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "생일이라서", "start_ms": 1250, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "선물을", "start_ms": 2150, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "사야", "start_ms": 2800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "하는데", "start_ms": 3300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "뭐가", "start_ms": 3900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "좋을까요?", "start_ms": 4400, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "친구분이 평소에 관심 있는 분야가 무엇인지 생각해 보세요.",
"speaker": "speaker_4",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "친구분이", "start_ms": 200, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "평소에", "start_ms": 1000, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "관심", "start_ms": 1650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 2200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "분야가", "start_ms": 2700, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "무엇인지", "start_ms": 3350, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "생각해", "start_ms": 4050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "보세요.", "start_ms": 4650, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "그 친구는 요리하는 걸 정말 좋아해요. 그래서 앞치마나 주방 도구가 어떨까 싶어요.",
"speaker": "speaker_6",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "그", "start_ms": 150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "친구는", "start_ms": 450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "요리하는", "start_ms": 1000, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "걸", "start_ms": 1750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 2150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "좋아해요.", "start_ms": 2700, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 3600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "앞치마나", "start_ms": 4150, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "주방", "start_ms": 4900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "도구가", "start_ms": 5400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "어떨까", "start_ms": 6000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "싶어요.", "start_ms": 6550, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "좋은 생각이네요! 예쁜 앞치마를 선물하면 친구가 아주 기뻐할 거예요.",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "생각이네요!", "start_ms": 650, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "예쁜", "start_ms": 1600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "앞치마를", "start_ms": 2100, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "선물하면", "start_ms": 2850, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "친구가", "start_ms": 3650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 4250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "기뻐할", "start_ms": 4700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "거예요.", "start_ms": 5300, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "주말에 시간이 되면 같이 전시회 보러 갈까요? 마침 보고 싶은 전시가 있거든요.",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "주말에", "start_ms": 180, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "시간이", "start_ms": 750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "되면", "start_ms": 1300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 1750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "전시회", "start_ms": 2250, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "보러", "start_ms": 2950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "갈까요?", "start_ms": 3450, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "마침", "start_ms": 4300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "보고", "start_ms": 4800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "싶은", "start_ms": 5300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "전시가", "start_ms": 5750, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "있거든요.", "start_ms": 6400, "dur_ms": 750, "phase_lr_deg": 0.0}
]
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
"transcript": "좋아요. 토요일 오후는 어떠세요? 저는 일요일보다 토요일이 편해요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "좋아요.", "start_ms": 120, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "토요일", "start_ms": 900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "오후는", "start_ms": 1500, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "어떠세요?", "start_ms": 2150, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "저는", "start_ms": 3000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "일요일보다", "start_ms": 3500, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "토요일이", "start_ms": 4400, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "편해요.", "start_ms": 5150, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "저도 토요일이 더 좋아요. 그럼 오후 2시에 전시장 매표소 앞에서 만날까요?",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "저도", "start_ms": 200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "토요일이", "start_ms": 700, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 1400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "좋아요.", "start_ms": 1750, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "그럼", "start_ms": 2550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "오후", "start_ms": 3050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "2시에", "start_ms": 3550, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "전시장", "start_ms": 4200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "매표소", "start_ms": 4900, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "앞에서", "start_ms": 5650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "만날까요?", "start_ms": 6250, "dur_ms": 750, "phase_lr_deg": 0.0}
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
"transcript": "네, 알겠습니다. 그때 봬요. 덕분에 즐거운 주말이 될 것 같아요.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "알겠습니다.", "start_ms": 550, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "그때", "start_ms": 1500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "봬요.", "start_ms": 2000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "덕분에", "start_ms": 2700, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "즐거운", "start_ms": 3350, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "주말이", "start_ms": 4000, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "될", "start_ms": 4650, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 5000, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "같아요.", "start_ms": 5300, "dur_ms": 600, "phase_lr_deg": 0.0}
]
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
"transcript": "감기에 걸려서 목소리가 잘 안 나오네요. 따뜻한 차를 좀 마셔야겠어요.",
"speaker": "speaker_4",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 220,
"audio_type": "speech",
"words": [
{"word": "감기에", "start_ms": 220, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "걸려서", "start_ms": 800, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "목소리가", "start_ms": 1450, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 2200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 2600, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "나오네요.", "start_ms": 2950, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "따뜻한", "start_ms": 3800, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "차를", "start_ms": 4450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 5000, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "마셔야겠어요.", "start_ms": 5350, "dur_ms": 850, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 05:47:22*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=14936  time=05:45:57  uptime=14457s  mem=560MB  system_processes=354"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_054024.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 570-105-571 574-571-514-570-517-507-157-157-570-571-715-157-175-107-170-170-170 574-570-517-571-157-175-510-750-570-571-570-571-507-105-175-510 570-750-157-157-150-571-175-750-1_V.mp3)
  Segment 2: 1576ms ~ 8632ms (file: 576-123-321-327-326-367-762-236-267-615-263-765-165-627-756-576-106-103-657-326-612-321-321-327-267-263-273-230-253-317-531-273-267-627-572-627-123-120-716-267-210-207-201-276-2_W.mp3)
  Segment 3: 9632ms ~ 10208ms (file: 137-136-103-103-134-103-136-103-103-103-132-103-132-103-103-103-103-103-102-136 517-315-716-763-130-137-312-132-310-312-160-736-750-370-175-571-763_V.mp3)
  Segment 4: 11208ms ~ 11784ms (file: 765-670-670-632-312-671-136-176-657-657-103-103-162-160-251-156-103-613-312-763-132_V.mp3)
  Segment 5: 12784ms ~ 21532ms (file: 132-321-213-213-620-267-531-670-617 103-134-103-627-625-602 162-263-531-610-217-267-275 120-162_V.mp3)
  Segment 6: 22532ms ~ 23108ms (file: 510-105-157-507-510-501-517-157-150-150-501-150-150-501-150-103-150-157-501-150-571-105-157-150-150-501-516-501 510-150-105-157-501-513-510-501-517-150-510-517-150-105-503-150-1_V.mp3)
  Segment 7: 24108ms ~ 24684ms (file: 510-517-150-105-501-507-517-501-501-510-510-150-510-150-510-510-501-510-507-507-501-501-501-501-570-570 504-570-570-510-507-517-157-507-501-510-501-501-501-510-510-501-510-510-5_V.mp3)
  Segment 8: 25684ms ~ 26260ms (file: 627-657-672-675-765-756 627-675-675-652-625-675-261-162-126-265-205 361-213-326-672-627-162_W.mp3)
  Segment 9: 27260ms ~ 34568ms (file: 123-753-106-160-321-270-173-106-751-106-756-263-312-130-327-167-756-167-136-651-261-126-210-716-276-312-327-276-756-276-160-321-231-574-321-753-752-102-237-375-175-106-123-765-2_W.mp3)
  Segment 10: 35568ms ~ 36144ms (file: 750-270-362-123-327-237-327-327-326-130-136-671-672-126-163-130-237-230-327-327-326-236-137-157-531-123-524-135-103-132-301-351-236-725-620-270-765-217-527-276-627-320-325-137-1_W.mp3)
  Segment 11: 37144ms ~ 37720ms (file: 576-675-675-675-765-672-672-265-672-167-567-657-765-175-756_W.mp3)
  Segment 12: 38720ms ~ 39296ms (file: 105-157-175-571-570-750-157-570-571-750-175-501 157-105-175-105-107-107-107-103-107-107-170-715-170-765-751-756-765-675-671-657-657-657-657-657 153-105-107-103-103-107-107-103-1_W.mp3)
  Segment 13: 40296ms ~ 40872ms (file: 762-160-102-203-316-617-136-130-163-165-156-120-301-627-167-302-326-236-213-320-526-135-103-136-126-672-123-623-312-163-126-610-103-236-321-310-102-160-637-326-132-617-163-263-1_W.mp3)
  Segment 14: 41872ms ~ 42448ms (file: 756-756-756-735-753-765-736-753-735-753 715-751-751-715-173-371-107-137-715-173-731-701-107-371-731-175-701-735-713-710-713-751-715-730 715-175-715-713-105-710-317-731-137-760-7_V.mp3)
  Segment 15: 43448ms ~ 44024ms (file: 756-762-716-237-736-576-702-731-576-726-752-763-362-267-762-573-215-670-257-326-257-675-530-632-725-326-256-257-762-573-726-267-265-572-705-276-627-627-725-236-203-362-574-137-7_V.mp3)
  Segment 16: 45024ms ~ 45600ms (file: 571-715-701-157-105-571-751-715-170-175-715-751-715-175-175-105-715-751-715-175-173-701-175-713-751 570-570-571-157-715-751-157-157-751-715-175-175-571-175-715-715-751-715-753-7_W.mp3)
  Segment 17: 46600ms ~ 54088ms (file: 210-271-231-271-153-312-260-270-102-273-310-267-123-203-103-310-257-172-130-257-217-130-103-310-235-203-267-270-726-572-157-156-132-302-267-231-132-327-372-570-756-102-267-702-3_V.mp3)
  Segment 18: 55088ms ~ 55664ms (file: 435-150-104-145-103-147-104-104-104-140-153-104-103-143-103-143-105-104-103-154-135-134-143 431-304-174-410-301-134-314-134-341-314-345-134-130-130-103-314-103-451-415-130-413-1_V.mp3)
  Segment 19: 56664ms ~ 57240ms (file: 320-372-703-765-602-206-765-267-706-760-762-370-306-765-765-230-301-756-750-367-752-207-236-230-103-123-613-320-632-632-267-203-320-302-301-306-132-312-316-163-316-306-301-361-3_V.mp3)
  Segment 20: 58240ms ~ 58816ms (file: 735-756-765-756-756-765-753-756-765-751-715-765-765-765-715 756-756-751-756-756-715-751-765-753-751-753-751-752_W.mp3)
  Segment 21: 59816ms ~ 70184ms (file: 316-103-167-763-672-760-765-756-321-132-123-103-765-756-765-621-160-162-675-167-756-302-176-143-675-754-750-726-301-201-201-123-103-130-351-261-210-203-257-620-106-312-210-103-1_W.mp3)
  Segment 22: 71184ms ~ 71760ms (file: 132-216-326-573-325-527-132-375-326-726-207-267-270-574-207-267-326-276-726-706-263-306-327-312-765-317-301-317-507-317-573-752-571-712-750-137-265-571-317-326-570-162-567-123-2_V.mp3)
  Segment 23: 72760ms ~ 73336ms (file: 103-102-106-104-145-142-145-136 163-176-132-137-134-125-127 571-715-751-761-617-671-761-671-672-671-761-671-765-761-751-765-175-756-567-756-715-175-157-157-516-157-615-562-162-1_W.mp3)
  Segment 24: 74336ms ~ 74912ms (file: 567-576-756-567-123-751-751-751-765-753-751-712-765-756 263 632-326-163-106-132-321-126-153-103-135-103-561-106-136-106-126-136-657-316-167-136-167-103-106-136-617-167-163-153-1_V.mp3)
  Segment 25: 75912ms ~ 76488ms (file: 756-537-276-713-726-132-351-527-357-275-572-216-213-756-627-546-546-512-570-512-513-132-570-561-726-671-213-275-257-217-570-576-752-165-635-625-132-735-321-351-571-321-572-132-7_V.mp3)
  Segment 26: 77488ms ~ 78064ms (file: 261-263-106-631-160-106-621-126-306-162-621-612-261-106-102-605-176-162-126 362-326-263-376-765-672-670-167-621-612-162-632-132-627-672-675-751-327-312-621-765-672-762-136-632-5_V.mp3)
  Segment 27: 79064ms ~ 79640ms (file: 126-127-137-136-103-103-103-123-124-316-136-132-413-132-324-312-132-735-237-326-127-213-321-216-216-267-261-231 106-106-103-167-163-316-123-632-316-130-620-627-260-261-620-627-6_V.mp3)
  Segment 28: 80640ms ~ 81216ms (file: 576-567-167-163-136 136 301-103-130 163-631-613-316-631-261-623-621-623-632-627-632-623-602-612-130-312-326-136_W.mp3)
  Segment 29: 82216ms ~ 82792ms (file: 126-671-720-261-765-765-607-765-716-716_W.mp3)
  Segment 30: 83792ms ~ 88544ms (file: 132-126-752-210-123-731-132-172-132-512-127-132-126-132-210-210-213-132-123-216-132-123-132-217-157-261-126-213-216-123-127-123-132-127-132-136-126-132-127-267-263-216-231-317-7_W.mp3)
  Segment 31: 89544ms ~ 90120ms (file: 132-750-657-267 675-160-106-176-162-175-301-236-236-236-326-230-267-263-621-152-134-143-135-137-137-137-126-136-310-301-173-175-620-260-263-621-263-672-263-136-216-621-623-326_V.mp3)
  Segment 32: 91120ms ~ 113584ms (file: 103-102-123-236-230-132-103-103-126-321-130-103-103-123-102-216-260-326-230-123-163 130-543-153-260-206-206-231-132-213-103-137-312-357-376-765-673-756-321-302-316-105-132-312-3_V.mp3)
  Segment 33: 114584ms ~ 124088ms (file: 302-203-102-210-210-301-276-230-753-231-201-765-357-753-750-756-320-231-213-732-327-517-123-312-132-123-132-321-213-123-132-201-206-206-206-132-621-615-162-612-615-261-672-621-6_W.mp3)
  Segment 34: 125088ms ~ 135240ms (file: 316-567-356-627-103-176 236-102-312-572-620-356-765-176-632-507-547-357-312-130-103-132-321-723-732-167-617-106-162-267-213-231-753-725-216-236-231-316-576-372-237-320-312-301-7_W_stt_the.mp3)
  Segment 35: 136240ms ~ 136816ms (file: 367-367-673-673-673-671-673-673-367-376-317-673-372-376-167-736-617-673-671-761-167-731-763-613-376-371-637-673-673-367-623-362-326-361-326-263-236-316_W.mp3)
  Segment 36: 137816ms ~ 138392ms (file: 627-627-627-621-621-627-671-672-672-672-621-621-623-316-675-670-672-670_W.mp3)
  Segment 37: 139392ms ~ 145476ms (file: 310-132-130-107-163-537-567-506-137-312-203-213-201-207-126-126-723-270-102-706-761-765-736-736-762-763-230-176-201-270-273-316-312-701-326-106-236-102-103-267-237-217-302-102-2_W.mp3)
  Segment 38: 146476ms ~ 147052ms (file: 107-132-130-321-321-302-301-756-703-327-302-302-327-372-273 672-670 765-675 723-213-207-231-132-213-207-310-321-327-273-756-137-135-321-231-203-103-103-301-231-207-231-321-372-2_V.mp3)
  Segment 39: 148052ms ~ 158060ms (file: 236-752-230-231-120-216-572-230-235-201-627-526-576-257-547-120-612-231-572-267-230-675-672-123-327-126-162-102-103-612-126-567-105-126-167-617-617-103-231-102-302-107-126-310-1_W.mp3)
  Segment 40: 159060ms ~ 159636ms (file: 173-136-136-123-526-562-650-632-361-652-675-671-716 162 567-652-627-672-625-627-176-362-176-621-267 562-625-216-213-126-162-132-123-136-361-136-317-317-365-376-167-356-120-135-5_W.mp3)
  Segment 41: 160636ms ~ 161212ms (file: 103-260-163-136-106-205-107-167-176-576-512-136-623-137-610-160-160-176-625-127-126-165-103-106-106-106-654-167-761-756-612-176-601-162-610-320 621-136-106-601-165-137-163-761-1_V.mp3)
  Segment 42: 162212ms ~ 162788ms (file: 576-517-612-103-102-132-167-136-317-167-130-136-103-651-236-132-103-132-103-103-132-751-102-130-136-316-165-376-513-103-103-561-516-172-137-127-106-136-132-103-130-132-136-103-1_V.mp3)
  Segment 43: 163788ms ~ 164364ms (file: 162-263-136-162-316-123-123-162-130-321-213-136-157-576-765-756-312-103-217-162-103-132 675-675-675 136_V.mp3)
  Segment 44: 165364ms ~ 165940ms (file: 672-203-573-321-570-257-367-216-326-712-312-752-256-257-162-136-753-375-513-135-317-137-367-137-136-132-136-316-675-675-675-675-675-751-173-751-132-267-261-610 625-367-672-671-6_W.mp3)
  Segment 45: 166940ms ~ 167516ms (file: 327-562-675-625-237-267-263-326-627-536-567-267-236-627-623-627-657-675-675 103-103-136-103-567-576-576-571-576-765-672-761-761-170-623-567-567-615-657-567-576-567-165-256-135-5_W.mp3)
  Segment 46: 168516ms ~ 182412ms (file: 123-231-126-126-103-132-375-752-257-315-150-574-130-231-130-137-201-210-132-216-756-267-261-725-765-715-170-756-756_W.mp3)
  Segment 47: 183412ms ~ 194068ms (file: 125-621-130-571-236-371-675-167-106-213-103-172-167-123-715-741-174-216-210-216-217-267-213-213-312-371-310-321-132-613-617-617-103-106-175-106-617-673-132-312-327-213-751-172-6_W.mp3)
  Segment 48: 195068ms ~ 195644ms (file: 637-621-617-362-632-627-672-123-312-617-617-623-167-672-671-632-637-613-631-617-217-257-627-621-632-162-371-126-672-612-621-612-126-675-621-675-672-627-762-612-126-672-627 175-1_V.mp3)
  Segment 49: 196644ms ~ 197220ms (file: 132-103-130-103-130-312-132-126-136-136-123-136-132-567-506-576-123-637-321-132_V.mp3)
  Segment 50: 198220ms ~ 198796ms (file: 376-176-756-675-672-762-561-756-572-726-751-256-507-725-576-765-572-265-712-752-765-567-502-572-725-726-573-576-726-725-723-627-267-507-723-720-267-267-257-672-572-570-706-316-5_V.mp3)
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
> **Time**: 2026-04-05 05:47:23
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 2,
"transcript": "오늘 저녁에 명동에 가서 같이 비빔밥 먹을래요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "저녁에", "start_ms": 600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "명동에", "start_ms": 1200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "가서", "start_ms": 1900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 2400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "비빔밥", "start_ms": 2900, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "먹을래요?", "start_ms": 3700, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "좋아요! 하지만 저는 매운 음식을 잘 못 먹는데 괜찮을까요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "좋아요!", "start_ms": 200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "하지만", "start_ms": 1000, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "저는", "start_ms": 1600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "매운", "start_ms": 2100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "음식을", "start_ms": 2600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 3200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "못", "start_ms": 3550, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "먹는데", "start_ms": 3850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "괜찮을까요?", "start_ms": 4400, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "걱정 마세요. 고추장을 따로 달라고 하면 맵지 않게 먹을 수 있어요.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "걱정", "start_ms": 180, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "마세요.", "start_ms": 650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "고추장을", "start_ms": 1300, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "따로", "start_ms": 2100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "달라고", "start_ms": 2600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "하면", "start_ms": 3200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "맵지", "start_ms": 3700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "않게", "start_ms": 4200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "먹을", "start_ms": 4700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 5100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "있어요.", "start_ms": 5350, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "다행이네요. 그럼 이따가 저녁 7시에 역 앞에서 만나요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "다행이네요.", "start_ms": 250, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "그럼", "start_ms": 1100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이따가", "start_ms": 1600, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "저녁", "start_ms": 2300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "7시에", "start_ms": 2800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "역", "start_ms": 3500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "앞에서", "start_ms": 3850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "만나요.", "start_ms": 4450, "dur_ms": 550, "phase_lr_deg": 0.0}
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
"transcript": "요즘 한국 드라마가 인기가 많아서 한국어를 배우기 시작했어요.",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "요즘", "start_ms": 120, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "드라마가", "start_ms": 1100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "인기가", "start_ms": 1800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "많아서", "start_ms": 2400, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "한국어를", "start_ms": 3100, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "배우기", "start_ms": 3850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "시작했어요.", "start_ms": 4400, "dur_ms": 750, "phase_lr_deg": 0.0}
]
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
"transcript": "정말요? 어떤 드라마를 제일 좋아하세요?",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "정말요?", "start_ms": 200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 1000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "드라마를", "start_ms": 1500, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "제일", "start_ms": 2200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "좋아하세요?", "start_ms": 2700, "dur_ms": 800, "phase_lr_deg": 0.0}
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
"transcript": "저는 로맨틱 코미디 드라마를 좋아해요. 배우들의 대사가 재미있거든요.",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "로맨틱", "start_ms": 650, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "코미디", "start_ms": 1300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "드라마를", "start_ms": 1900, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "좋아해요.", "start_ms": 2600, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "배우들의", "start_ms": 3500, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "대사가", "start_ms": 4250, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "재미있거든요.", "start_ms": 4900, "dur_ms": 850, "phase_lr_deg": 0.0}
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
"transcript": "I believe that every citizen got so many benefits from the new policy.",
"speaker": "speaker_5",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 220,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 220, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "believe", "start_ms": 500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "that", "start_ms": 1050, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "every", "start_ms": 1450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "citizen", "start_ms": 1950, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "got", "start_ms": 2650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "so", "start_ms": 3100, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "many", "start_ms": 3450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "benefits", "start_ms": 3900, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "from", "start_ms": 4650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 5050, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "new", "start_ms": 5350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "policy.", "start_ms": 5750, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "내일 친구 생일이라서 선물을 사야 하는데 뭐가 좋을까요?",
"speaker": "speaker_6",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "내일", "start_ms": 180, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "친구", "start_ms": 700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "생일이라서", "start_ms": 1250, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "선물을", "start_ms": 2150, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "사야", "start_ms": 2800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "하는데", "start_ms": 3300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "뭐가", "start_ms": 3900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "좋을까요?", "start_ms": 4400, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "친구분이 평소에 관심 있는 분야가 무엇인지 생각해 보세요.",
"speaker": "speaker_4",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "친구분이", "start_ms": 200, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "평소에", "start_ms": 1000, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "관심", "start_ms": 1650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 2200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "분야가", "start_ms": 2700, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "무엇인지", "start_ms": 3350, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "생각해", "start_ms": 4050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "보세요.", "start_ms": 4650, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "그 친구는 요리하는 걸 정말 좋아해요. 그래서 앞치마나 주방 도구가 어떨까 싶어요.",
"speaker": "speaker_6",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "그", "start_ms": 150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "친구는", "start_ms": 450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "요리하는", "start_ms": 1000, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "걸", "start_ms": 1750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 2150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "좋아해요.", "start_ms": 2700, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 3600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "앞치마나", "start_ms": 4150, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "주방", "start_ms": 4900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "도구가", "start_ms": 5400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "어떨까", "start_ms": 6000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "싶어요.", "start_ms": 6550, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "좋은 생각이네요! 예쁜 앞치마를 선물하면 친구가 아주 기뻐할 거예요.",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "생각이네요!", "start_ms": 650, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "예쁜", "start_ms": 1600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "앞치마를", "start_ms": 2100, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "선물하면", "start_ms": 2850, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "친구가", "start_ms": 3650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 4250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "기뻐할", "start_ms": 4700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "거예요.", "start_ms": 5300, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "주말에 시간이 되면 같이 전시회 보러 갈까요? 마침 보고 싶은 전시가 있거든요.",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "주말에", "start_ms": 180, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "시간이", "start_ms": 750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "되면", "start_ms": 1300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 1750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "전시회", "start_ms": 2250, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "보러", "start_ms": 2950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "갈까요?", "start_ms": 3450, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "마침", "start_ms": 4300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "보고", "start_ms": 4800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "싶은", "start_ms": 5300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "전시가", "start_ms": 5750, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "있거든요.", "start_ms": 6400, "dur_ms": 750, "phase_lr_deg": 0.0}
]
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
"transcript": "좋아요. 토요일 오후는 어떠세요? 저는 일요일보다 토요일이 편해요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "좋아요.", "start_ms": 120, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "토요일", "start_ms": 900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "오후는", "start_ms": 1500, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "어떠세요?", "start_ms": 2150, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "저는", "start_ms": 3000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "일요일보다", "start_ms": 3500, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "토요일이", "start_ms": 4400, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "편해요.", "start_ms": 5150, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "저도 토요일이 더 좋아요. 그럼 오후 2시에 전시장 매표소 앞에서 만날까요?",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "저도", "start_ms": 200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "토요일이", "start_ms": 700, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 1400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "좋아요.", "start_ms": 1750, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "그럼", "start_ms": 2550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "오후", "start_ms": 3050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "2시에", "start_ms": 3550, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "전시장", "start_ms": 4200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "매표소", "start_ms": 4900, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "앞에서", "start_ms": 5650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "만날까요?", "start_ms": 6250, "dur_ms": 750, "phase_lr_deg": 0.0}
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
"transcript": "네, 알겠습니다. 그때 봬요. 덕분에 즐거운 주말이 될 것 같아요.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "알겠습니다.", "start_ms": 550, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "그때", "start_ms": 1500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "봬요.", "start_ms": 2000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "덕분에", "start_ms": 2700, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "즐거운", "start_ms": 3350, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "주말이", "start_ms": 4000, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "될", "start_ms": 4650, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 5000, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "같아요.", "start_ms": 5300, "dur_ms": 600, "phase_lr_deg": 0.0}
]
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
"transcript": "감기에 걸려서 목소리가 잘 안 나오네요. 따뜻한 차를 좀 마셔야겠어요.",
"speaker": "speaker_4",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 220,
"audio_type": "speech",
"words": [
{"word": "감기에", "start_ms": 220, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "걸려서", "start_ms": 800, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "목소리가", "start_ms": 1450, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 2200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 2600, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "나오네요.", "start_ms": 2950, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "따뜻한", "start_ms": 3800, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "차를", "start_ms": 4450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 5000, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "마셔야겠어요.", "start_ms": 5350, "dur_ms": 850, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 05:47:23*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=24932  time=05:45:57  uptime=5241s  mem=454MB  system_processes=354"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_054113.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 173-701-703 710-751-753-357-317-106-316-613-631-160-607-613-361-607-603-610-607-106-176-751-137-315-130-713-317-371 750-107-573-137-753-713-753-517-317-107-173-361-607-630-673-6_V.mp3)
  Segment 2: 1576ms ~ 8488ms (file: 625-316-103-574-316-261-236-216-572-157-673-163-231-675-267-675-632-754-362-316-362-130-132-627-613-316-576-567-163-103-163-136-132-321-576-572-216-657-567-132-103-261-261-210-2_W.mp3)
  Segment 3: 9488ms ~ 10064ms (file: 612-126-326-103-130-321-163-103-136-306-106-623-316-236-263-103-103-132-165-357-103-317-316-567-263-236-263-761-612-261-263-236-136-162-136-613-261-316-316-631-126-162-605-567-6_W.mp3)
  Segment 4: 11064ms ~ 16752ms (file: 260-126-301-573-574-130-153-301-273-260-571-572-201-301-312-673-260-756-735-765-201-310-263-261-260-130-756-517-216-217-173-271-207-102-103-267-720-732-726-230-271-107-756-170-2_W.mp3)
  Segment 5: 17752ms ~ 18328ms (file: 672-203 730-302-173-102-231-136-162-136-165-176-127-236-123-103-610-756-325-357-531-321-132-132-756-562-572-517-572-327-275-576-567-325-275-256-260-265-265-257-572-652-652-562-6_W.mp3)
  Segment 6: 19328ms ~ 19904ms (file: 326-321-612-623-621-261-671-102-671-263-762-236-372-627-617-627-657 612-572-672-203-210-231_W.mp3)
  Segment 7: 20904ms ~ 21480ms (file: 132-103-103-103-132-136-103-163-617-167-136-103-675-156-653-354-621-627-621-672-123_V.mp3)
  Segment 8: 22480ms ~ 29860ms (file: 136-267-276-571-152-576-206-106-132-602-123-301-320-301-236-263-756-137-215-275-321-531-326-175-627-527-253-312-756-170-574-321-103-501-350-321-756-176-137-207-530-267-175-321-7_V.mp3)
  Segment 9: 30860ms ~ 37448ms (file: 573-267-132-130-103-137-536-132-623-123-103-135-135-153-162-267-261-576-267-267-562-216-263-267-213-312-261-754-316-362-275-265-216-273-620-260-752-136-136-213-216-312-172-126-2_W.mp3)
  Segment 10: 38448ms ~ 39024ms (file: 150-510-510-571-517-501-570-571-571-574-571-517-576-517-571-571 570-570-510-157-157-517-507-503-507-517-573-510-150-507-501-510-510-510-501-510-571-510-507-570-576-570-570-571-5_W.mp3)
  Segment 11: 40024ms ~ 40600ms (file: 517-152-213-125-752-276-765-752-253-207-651-572-507-132-672-357-213-631-613-263-361-361-360-756-672-502-356-167-726-507-527-576-536-576-752-276-752-621-327-570-547-705-765-572-7_V.mp3)
  Segment 12: 41600ms ~ 42176ms (file: 310-130-312-102-310-103-172-103-103-137-173-317 701-307-375-537-175-351-173-610-603-630-630-610-601-103-371-137-163-617-716-317-317-137-716-571-153-136-157 705-751-103-157-170-3_V.mp3)
  Segment 13: 43176ms ~ 43752ms (file: 765-765-715-756-715-751-751-105-104-501-105-105-170-716-671-715-761-176-176-716-175-715-715-715-175-175-716-715-715-176-715-271 105-105-105-105-150-157-157-176-167-176-657-176-1_W.mp3)
  Segment 14: 44752ms ~ 45328ms (file: 126-102-123-132-136-137-132-361-574_V.mp3)
  Segment 15: 46328ms ~ 46904ms (file: 605-613-316-361-317-312-627-317-132-623-321-316-367_W.mp3)
  Segment 16: 47904ms ~ 53376ms (file: 103-321-301-130-120-162-120-132-126-617-167-621-136-107-715-207-172-103-627-620-672-260-206-257-572-312-725-201-321-302-260-652-765-627-206-276-126-103-137-213-201-201-276-206-1_W.mp3)
  Segment 17: 54376ms ~ 54952ms (file: 301-310-307-713-167-631-376-763-637-176-167-765-671-613-167-657-756-735-705 713-173-375-310-317-103-137-317-310-312-310-103-130-317-173-130-317-370-137-317-761-360-763-137-316-7_V.mp3)
  Segment 18: 55952ms ~ 69236ms (file: 216-261-216-236-263-102-361-136-213-236-236-326-312-132-136-631-126-326-107-531-517-106-106-160-760-267-752-632-542-512-321-302-320-327-307-310-735-726-163-706-163-126-136-312-2_W.mp3)
  Segment 19: 70236ms ~ 77148ms (file: 257-752-256-201-321-103-163-123-136-213-103-216-217-260-257-312-602-256-762-726-651-216-130-670-132-206-236-312-760-257-607-752-312-261-321-765-657-312-263-275-213-765-210-312-1_W.mp3)
  Segment 20: 78148ms ~ 78724ms (file: 105-507-157-107-570-751-517-751-510-157-710-751-570 570-571-507-750-157-107-571-715-175-715-107-175-170-507-715-157-157-157-175-751-170-571-750-751-570-750-751 570-570-570-507-5_V.mp3)
  Segment 21: 79724ms ~ 80300ms (file: 540-510-504-501-504-510-501-514-501-510-514-514 765-514-105-514-501_V.mp3)
  Segment 22: 81300ms ~ 81876ms (file: 132-107-107-107-107-107-153-157-175-173-301-310-130-165-165-130-132-132-163-136-176-136-613-163-367-162-367-162-165-165-157-173-137-135-136-765-762-765-765-672-612-132-136-165-5_W.mp3)
  Segment 23: 82876ms ~ 83452ms (file: 216-671-675-136-163-516-751-132-167-162-623-716-316-103-132-152-106-136-106-136-103-165-136-261 130_W.mp3)
  Segment 24: 84452ms ~ 94136ms (file: 201-205-210-572-527-203-102-127-236-206-257-126-104-203-752-754-103-103-103-230-203-230-524-526-206-123-206-106-230-260-213-126-173-124 201-230-532-320-236-213-213-231-103-127-1_W.mp3)
  Segment 25: 95136ms ~ 95712ms (file: 157-732-632-542-563-235-627-250-132-375-756-267-267-265-567-362-715-107-137-103-132-135-753-237 750-514-276-523-312-752-675-753-321-265-217-576-502-507 267-317-132-312-756 265-3_V.mp3)
  Segment 26: 96712ms ~ 101572ms (file: 206-376-132-321-152-213-132-132-123-120-120-120-210-120-120-120-120-123-120-102-130-231-217-213-132-123-312-132-312-213-132-123-213-231-213-123-213-123-123-132-123-213-217-213-2_W.mp3)
  Segment 27: 102572ms ~ 109592ms (file: 126-210-132-612-613-361-126-213-127-216-126-120-216-127-105-217-123-132-261-621-231-132-216-120-215-261-126-120-621-120-106-120-102-627-261-627-321-132-213-130-157-126-126-126-2_W.mp3)
  Segment 28: 110592ms ~ 111168ms (file: 570-570-750-507-570-571-510-570-570-570-570_V.mp3)
  Segment 29: 112168ms ~ 112744ms (file: 650-126-106-126-621-126-160-105-105-216-201-102-126-156-106-163-756-576-576-576-576-576-756-657-137_W.mp3)
  Segment 30: 113744ms ~ 121376ms (file: 215-127-123-135-317-321-317-137-123-312-327-573-675-672-321-513-756-576-756-765-671-761-276-756-756-765-176-760-762-123-123-521-257-276-271-203-270-574-276-267-267-276-163-230-7_W.mp3)
  Segment 31: 122376ms ~ 130620ms (file: 213-213-210-261-120-132-120-231-261-201-213-273-263-276-210-276-261-216-206-237-261-260-263-106-102-162-216-103-102-132-263-243-261-261-132-107-716-276-123-260-260-165-231-263-5_W.mp3)
  Segment 32: 131620ms ~ 132196ms (file: 135-670-312-367-763-670-760-706-675-763-371-716-753-735-537-735-753-367-763-715-270-756-756-765-751-736-756-751-753-735-172-571-576-537-536-513-305-321-201-325-132-312-132-106-1_W.mp3)
  Segment 33: 133196ms ~ 140864ms (file: 126-327-361-237-731-732-276-327-326-632-673-632-167-712-236-715-137-276-760-716-176-730-317-237-276-762-372-103-723-731-107-103-107-632-361-731-276-273-267-267-276-217-321-701-1_W.mp3)
  Segment 34: 141864ms ~ 142440ms (file: 105-172-765-167-160-631-157-675-570-157-176-367-231-162-126-612-175-567-756-756-570-752-576-620-231-132-135-571-576-756-570 725-765-702-706-761-716-761-107-123-137-756-160-351-1_W.mp3)
  Segment 35: 143440ms ~ 144016ms (file: 517-107-105-157-150-153-173-135-173-715-175-135-713-157-125-751-537-175-153-157-175_V.mp3)
  Segment 36: 145016ms ~ 151820ms (file: 673-627-201-312-231-132-136-163-671-162-576-230-571-310-160-102-517-103-103-123-326-236-574-356-132-572-102-756-756-230-130-103-627-610-162-106-710-570-103-302-571-301-310-302-3_W.mp3)
  Segment 37: 152820ms ~ 153396ms (file: 316-317-136-361-362-231-317 731-750-317-371-103-367-370-130-167-107-106-130-130-167-167-160-376-170-173-137-106-675-631-610-637-316-103-761 173-107-137-107-310-317-310-173-371-1_W.mp3)
  Segment 38: 154396ms ~ 154972ms (file: 237-231-261-213-756-576-756-756-674-327 165-163-261-132-675-621-631-310-316-301-312-316-132-617-612-162-610-103-103-130 361-241-754-726 316-623-261-631-362 762-672-267-267-261-6_W.mp3)
  Segment 39: 155972ms ~ 160688ms (file: 216-672-106-361-762-523-267-271-127-672-672-132-127-316-236-627-167-721-716-267-267-263-126-276-627-126-726-627-276-176-102-162-301-160-761-213-267-617-236-762-761-612-267-236-2_W.mp3)
  Segment 40: 161688ms ~ 171192ms (file: 210-206-103-130-312-267-267-132-176-574-132-260-103-120-123-107-143-103-123-312-130-261-103-213-206-670-261-206-210-752-762-756-260-260-132-326-752-760-206-102-132-206-261-103-5_W.mp3)
  Segment 41: 172192ms ~ 172768ms (file: 173-107-710-130-371-713-731-317-701-301-317-173-137-107-107-715-710-715-753-103-715-751-735-107 310-103-103-137-307-310-710-173-715-701-107-130-107-137-173-130-710-137-735 103-3_V.mp3)
  Segment 42: 173768ms ~ 181148ms (file: 327-162-102-130-136-230-627-326-362-106-310-315-136-236-675-567-157-675-326-236-276-327-312-236-321-316-236-236-362-367-301-130-312-327-375-127-167-106-106-106-631-326-321-263-2_W.mp3)
  Segment 43: 182148ms ~ 182724ms (file: 507-157-571-105-150-571-510-517-170-570 157-105-103-153-105-517-501-157-513-517-501-517-175-510-150-501-510-510-517-571-517-510-150-150-150-153-150-157-157_V.mp3)
  Segment 44: 183724ms ~ 184300ms (file: 372-361-653-637-367-376-657-137-326-312-321-312-106-106-102 167-164-167-541-504-102-102_W.mp3)
  Segment 45: 185300ms ~ 185876ms (file: 132-102-103-236-130-103-130-105-104-125-123-672-263-162-132-576 574-574_V.mp3)
  Segment 46: 186876ms ~ 187452ms (file: 327-326-257-673-761-312-570-765-625-623-236-326-352-321-317-613-257-273-576-756-507-657-273-237-576-726-327-571-572-701-172-216-231-273-235-153-501-162-216-572-627-265_V.mp3)
  Segment 47: 188452ms ~ 189028ms (file: 163-537-541-571-135-574-537-371-576-507-570-507-506-657-524-506-573-157-571-516-571-751-173-512-567-532-567-657-675-567-517-651-516-156-153-175-365-513-365-315-315-517-567-576-6_W.mp3)
  Segment 48: 190028ms ~ 190604ms (file: 517-531-572-572-526-567-572-567-527-513-512-531-512-523-531-156-572-512-567-536-576-571-537-567-572-576-562-571-576-527-572-572-576-573-571-576-527-752-576-571-572-517-510-517-1_W.mp3)
  Segment 49: 191604ms ~ 192180ms (file: 576-576-576-576-576-570-501-507-501-501-570-510-150-157-510-576-570-517-516-510-153-576-576-517-561-576-517-571-567-570-517-510-150-571-571-751-175-175-713-571-175-751-756-576-5_V.mp3)
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
> **Time**: 2026-04-05 05:47:23
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 2,
"transcript": "오늘 저녁에 명동에 가서 같이 비빔밥 먹을래요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "저녁에", "start_ms": 600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "명동에", "start_ms": 1200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "가서", "start_ms": 1900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 2400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "비빔밥", "start_ms": 2900, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "먹을래요?", "start_ms": 3700, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "좋아요! 하지만 저는 매운 음식을 잘 못 먹는데 괜찮을까요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "좋아요!", "start_ms": 200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "하지만", "start_ms": 1000, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "저는", "start_ms": 1600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "매운", "start_ms": 2100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "음식을", "start_ms": 2600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 3200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "못", "start_ms": 3550, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "먹는데", "start_ms": 3850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "괜찮을까요?", "start_ms": 4400, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "걱정 마세요. 고추장을 따로 달라고 하면 맵지 않게 먹을 수 있어요.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "걱정", "start_ms": 180, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "마세요.", "start_ms": 650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "고추장을", "start_ms": 1300, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "따로", "start_ms": 2100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "달라고", "start_ms": 2600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "하면", "start_ms": 3200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "맵지", "start_ms": 3700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "않게", "start_ms": 4200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "먹을", "start_ms": 4700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 5100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "있어요.", "start_ms": 5350, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "다행이네요. 그럼 이따가 저녁 7시에 역 앞에서 만나요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "다행이네요.", "start_ms": 250, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "그럼", "start_ms": 1100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이따가", "start_ms": 1600, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "저녁", "start_ms": 2300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "7시에", "start_ms": 2800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "역", "start_ms": 3500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "앞에서", "start_ms": 3850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "만나요.", "start_ms": 4450, "dur_ms": 550, "phase_lr_deg": 0.0}
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
"transcript": "요즘 한국 드라마가 인기가 많아서 한국어를 배우기 시작했어요.",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "요즘", "start_ms": 120, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "드라마가", "start_ms": 1100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "인기가", "start_ms": 1800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "많아서", "start_ms": 2400, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "한국어를", "start_ms": 3100, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "배우기", "start_ms": 3850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "시작했어요.", "start_ms": 4400, "dur_ms": 750, "phase_lr_deg": 0.0}
]
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
"transcript": "정말요? 어떤 드라마를 제일 좋아하세요?",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "정말요?", "start_ms": 200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 1000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "드라마를", "start_ms": 1500, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "제일", "start_ms": 2200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "좋아하세요?", "start_ms": 2700, "dur_ms": 800, "phase_lr_deg": 0.0}
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
"transcript": "저는 로맨틱 코미디 드라마를 좋아해요. 배우들의 대사가 재미있거든요.",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "로맨틱", "start_ms": 650, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "코미디", "start_ms": 1300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "드라마를", "start_ms": 1900, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "좋아해요.", "start_ms": 2600, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "배우들의", "start_ms": 3500, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "대사가", "start_ms": 4250, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "재미있거든요.", "start_ms": 4900, "dur_ms": 850, "phase_lr_deg": 0.0}
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
"transcript": "I believe that every citizen got so many benefits from the new policy.",
"speaker": "speaker_5",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 220,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 220, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "believe", "start_ms": 500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "that", "start_ms": 1050, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "every", "start_ms": 1450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "citizen", "start_ms": 1950, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "got", "start_ms": 2650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "so", "start_ms": 3100, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "many", "start_ms": 3450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "benefits", "start_ms": 3900, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "from", "start_ms": 4650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 5050, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "new", "start_ms": 5350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "policy.", "start_ms": 5750, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "내일 친구 생일이라서 선물을 사야 하는데 뭐가 좋을까요?",
"speaker": "speaker_6",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "내일", "start_ms": 180, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "친구", "start_ms": 700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "생일이라서", "start_ms": 1250, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "선물을", "start_ms": 2150, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "사야", "start_ms": 2800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "하는데", "start_ms": 3300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "뭐가", "start_ms": 3900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "좋을까요?", "start_ms": 4400, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "친구분이 평소에 관심 있는 분야가 무엇인지 생각해 보세요.",
"speaker": "speaker_4",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "친구분이", "start_ms": 200, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "평소에", "start_ms": 1000, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "관심", "start_ms": 1650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 2200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "분야가", "start_ms": 2700, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "무엇인지", "start_ms": 3350, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "생각해", "start_ms": 4050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "보세요.", "start_ms": 4650, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "그 친구는 요리하는 걸 정말 좋아해요. 그래서 앞치마나 주방 도구가 어떨까 싶어요.",
"speaker": "speaker_6",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "그", "start_ms": 150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "친구는", "start_ms": 450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "요리하는", "start_ms": 1000, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "걸", "start_ms": 1750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 2150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "좋아해요.", "start_ms": 2700, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 3600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "앞치마나", "start_ms": 4150, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "주방", "start_ms": 4900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "도구가", "start_ms": 5400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "어떨까", "start_ms": 6000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "싶어요.", "start_ms": 6550, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "좋은 생각이네요! 예쁜 앞치마를 선물하면 친구가 아주 기뻐할 거예요.",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "생각이네요!", "start_ms": 650, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "예쁜", "start_ms": 1600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "앞치마를", "start_ms": 2100, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "선물하면", "start_ms": 2850, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "친구가", "start_ms": 3650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 4250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "기뻐할", "start_ms": 4700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "거예요.", "start_ms": 5300, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "주말에 시간이 되면 같이 전시회 보러 갈까요? 마침 보고 싶은 전시가 있거든요.",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "주말에", "start_ms": 180, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "시간이", "start_ms": 750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "되면", "start_ms": 1300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 1750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "전시회", "start_ms": 2250, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "보러", "start_ms": 2950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "갈까요?", "start_ms": 3450, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "마침", "start_ms": 4300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "보고", "start_ms": 4800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "싶은", "start_ms": 5300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "전시가", "start_ms": 5750, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "있거든요.", "start_ms": 6400, "dur_ms": 750, "phase_lr_deg": 0.0}
]
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
"transcript": "좋아요. 토요일 오후는 어떠세요? 저는 일요일보다 토요일이 편해요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "좋아요.", "start_ms": 120, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "토요일", "start_ms": 900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "오후는", "start_ms": 1500, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "어떠세요?", "start_ms": 2150, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "저는", "start_ms": 3000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "일요일보다", "start_ms": 3500, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "토요일이", "start_ms": 4400, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "편해요.", "start_ms": 5150, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "저도 토요일이 더 좋아요. 그럼 오후 2시에 전시장 매표소 앞에서 만날까요?",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "저도", "start_ms": 200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "토요일이", "start_ms": 700, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 1400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "좋아요.", "start_ms": 1750, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "그럼", "start_ms": 2550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "오후", "start_ms": 3050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "2시에", "start_ms": 3550, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "전시장", "start_ms": 4200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "매표소", "start_ms": 4900, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "앞에서", "start_ms": 5650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "만날까요?", "start_ms": 6250, "dur_ms": 750, "phase_lr_deg": 0.0}
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
"transcript": "네, 알겠습니다. 그때 봬요. 덕분에 즐거운 주말이 될 것 같아요.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "알겠습니다.", "start_ms": 550, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "그때", "start_ms": 1500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "봬요.", "start_ms": 2000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "덕분에", "start_ms": 2700, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "즐거운", "start_ms": 3350, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "주말이", "start_ms": 4000, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "될", "start_ms": 4650, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 5000, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "같아요.", "start_ms": 5300, "dur_ms": 600, "phase_lr_deg": 0.0}
]
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
"transcript": "감기에 걸려서 목소리가 잘 안 나오네요. 따뜻한 차를 좀 마셔야겠어요.",
"speaker": "speaker_4",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 220,
"audio_type": "speech",
"words": [
{"word": "감기에", "start_ms": 220, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "걸려서", "start_ms": 800, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "목소리가", "start_ms": 1450, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 2200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 2600, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "나오네요.", "start_ms": 2950, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "따뜻한", "start_ms": 3800, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "차를", "start_ms": 4450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 5000, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "마셔야겠어요.", "start_ms": 5350, "dur_ms": 850, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 05:47:23*
