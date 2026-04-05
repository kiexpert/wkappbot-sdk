# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=1700  time=11:10:25  uptime=23692s  mem=2026MB  system_processes=353"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_110703.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 136-316-317-731-130-310-301-317-163-137-537-103-310-103-751-317-170-715-751-175-153-571-137-317 173-375-371-710-371-371-107-301-163-137-307-103-137-137-137-103-137-301-103-321-5_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 623-631-632-162-132-167-316-621-576-567-627-362-146-132 257-320-132-312-673-637-136-103-360-623-367-637-321-362-627-173-231-672-672-672-672-673-765-607-763-761-367-637-163-312-2_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 132-103-130-103-132-312-316-301-106-301-316-106-613 672-675-627_V.mp3)
  Segment 4: 4728ms ~ 11172ms (file: 137-230-502-103-726-276-312-267-267-523-547-235-765-752-735-750-276-236-521-527-321-507-573-271-752-175-765-675-213-267-321-275-172-750-317-573-127-537-507-276-356-507-576-725-3_W.mp3)
  Segment 5: 12172ms ~ 12748ms (file: 136-130-136-103-315-103-175-630-126-157-310-163-163-106-123-610-126-123-173-137-107-516-310-617-160-106-103-617-130-720-237-326-231-123-106-103-617-167-675-175-675-765-231-657-1_V.mp3)
  Segment 6: 13748ms ~ 14324ms (file: 576-263-257-326-326-273-576-526-263-213-267-372-216-625-507-570-725-367-627-570-213-627-267-167-257-352-265-753-632-523-236-327-257-163-267-721-276-726-651-756-675-623-206-632-1_V.mp3)
  Segment 7: 15324ms ~ 22596ms (file: 627-167-263-132-621-261-163-267-276-356-163-130-261-237-273-236-627-621-173-102-135-132-132-325-312-123-102-132-107-521-106-106-231-675-106-267-132-120-321-167-103-201-213-301-2_W.mp3)
  Segment 8: 23596ms ~ 24172ms (file: 253-235-235-231-325-326-326-263-632-265-320-236-235-263-216-235-321-230-230-372-371-207-275-237-231-321-751-231-237-273-756-162-315-753-365-326-207-273-207-725-327-123-126-123-1_V.mp3)
  Segment 9: 25172ms ~ 25748ms (file: 130-103-107-671-670-607-762-672-760-607-576-756-765-312-326-260-206-320-327-236-706-713-760-716-173-715-175-175-213-706-721-123-172-103-103-103-134-136-134-132-705-372-361-631-1_V.mp3)
  Segment 10: 26748ms ~ 34056ms (file: 720-720-276-172-627-627-310-312-276-267-263-301-216-756-260-261-103-132-721-276-720-132-263-260-260-517-571-257-625-231-320-231-754-621-236-123-123-261-231-310-257-213-213-205-2_W.mp3)
  Segment 11: 35056ms ~ 35632ms (file: 751-751-750-751-705 507-157-517-513-517-510-517-170-107-105-501-571-507-754-175-175-135-157-305 571-715-173-751-175-571-105-675-721-726-736-762-762-671-637-762-627-762-673-627-2_W.mp3)
  Segment 12: 36632ms ~ 51392ms (file: 312-162-106-671-216-231-321-120-312-103-102-132-136 627-612-675 120-310-213-210-123-260-267-310-210-210-260 254-261-254-246-260-132-263-260-260-260-362-203-210-130-123-213-132-1_W.mp3)
  Segment 13: 52392ms ~ 52968ms (file: 603-627-602-621-162-571-570-507-206-725-675-675-167-106-136-130-103-312-103-137-267-276-260-275-763-612-106-107-103-231-263-517-710-571-570-756-765-172-167-316-302-657-627-267-6_W.mp3)
  Segment 14: 53968ms ~ 54544ms (file: 572-572-527-257-625-615-612-176-176-157-156-165-157-153-125-267-751-671-725-725-756-765-275-271-275-756-675-675-675-765-765-765-756-576-572-571-527-675-657-160-165-102-105-105-1_W.mp3)
  Segment 15: 55544ms ~ 56120ms (file: 120-132-167-765-765-763-765-763-765-765-765-763-761-763-763 165 321_W.mp3)
  Segment 16: 57120ms ~ 57696ms (file: 627-627-621-627-621-621-621-612-621 621-652-654-645-627-624-627-617-657-612-621-621-621-652-621-645-612-623 126-162-621-612-657-671-725-752-725-752-725-726-752-756 612-675-657-6_W.mp3)
  Segment 17: 58696ms ~ 59272ms (file: 123-567-152-751-137-537-137-715-271-137-312-153-517-231-132-713-571-136-750-750-217-517-132-257-576-572-253-573-273-752-326-327-567-752-625-127-321-706-267-273-521-754-576-750-1_V.mp3)
  Segment 18: 60272ms ~ 60848ms (file: 310-312-372-103-103-105-371-137-130-103-137-701-103-105-137-317-103 135-103-130-175-371-130-137-130-107-312-130-371-130-310-301-137-175-103-137-105-710-107-137-173-310-712-307 1_V.mp3)
  Segment 19: 61848ms ~ 82044ms (file: 230-263-106-231-261-154-236-321-263-213-230-326-102-213-263-260-236-267-230-310-230-267-312-260-230-321-263-103-321-261-231-263-312-261-260-320-236-627-751 312-236-267-265-574-3_W.mp3)
  Segment 20: 83044ms ~ 87580ms (file: 263-261-231-213-123-210-231-312-126-123-150-231-123-321-310-310-231-312-302-132-135-103-132-271-231-213-216-132-321-236-136-126-103-132-213-730-502-312-231-230-267-132-132-263-2_W.mp3)
  Segment 21: 88580ms ~ 89156ms (file: 175-157-157-157 703-305-153-105-105-157-130-173-173-317-715-175-175-715-175-715-753-157-175-135-517-157-157 157-531-175-571-315-135-103-103-130-157-157-157-157-157-157-175-135 1_V.mp3)
  Segment 22: 90156ms ~ 90732ms (file: 506-502-527-502-520-507-650-765-675_W.mp3)
  Segment 23: 91732ms ~ 92308ms (file: 203-230-302 321-136-162-167-167-547-572-762-672-627-267-106-671-316-136-106-601-627-627-602-263-230-260-261-627-162-706-765 517-157-574-756-736-203-576-520-163-106-316-310-301-2_V.mp3)
  Segment 24: 93308ms ~ 93884ms (file: 542-562 752-756-576-756-567-576-576-567-756-576-756-752-546-546-562-542-542-504-503-506-504-725-752-752-572-576-756-756-756-576-572-576-572-504-504-507-504-504-504-504-507-504-5_W.mp3)
  Segment 25: 94884ms ~ 106044ms (file: 231-321-130-103-173-321-127-231-236-321-132-213-126-716-135-236-132-160-671-627-261-172-126-257-327-321-236-102-123-173-260-276-627-267-276-321-263-132-132-123-275-527-236-231-1_W.mp3)
  Segment 26: 107044ms ~ 116476ms (file: 156-123-157-175-107-270-203-132-213-320-123-521-312-312-320-230-102-102-205-265-210-203-203-123-527-507-257-103-203-102-132-123-102-176-120-143-120-132-302-260-123-230-214-102-1_V.mp3)
  Segment 27: 117476ms ~ 127196ms (file: 123-321-302-157-623-623-652-612-132-103-130-361-317-316-137-103-137-136-137-103-213-132-132-136-163-106-136-310-130-130-104-132-132-320-261-623-132-327-310-137-103-213-176-670-6_W.mp3)
  Segment 28: 128196ms ~ 128772ms (file: 547-547-574-574-574-571-574-574-574-574 157-175-157-571-157-751-751-571-175-715-157-157-157-715-175-751-571-571-751 574-574-574-517-571-517-571-517-571-571-571-157-157-571-517-5_W.mp3)
  Segment 29: 129772ms ~ 138844ms (file: 576-123-321-261-213-120-726-263-326-236-321-267-267-765-672-260-231-162-627-260-756-157-267-576-576-216-260-267-261-261-752-213-312-756-574-267-267-236-263-231-263-137-273-261-7_W.mp3)
  Segment 30: 139844ms ~ 140420ms (file: 216-126-102-261-623-263-673-601-267-546-527-312-321-316-132-675-675-672-173-507-123-156-671-576-612-362-631-672-635-631-361-671-613-367-136-613-765-170-751-617-706-701-617-106-1_W.mp3)
  Segment 31: 141420ms ~ 141996ms (file: 157-175 105-105-104-105-510-510-175-157-150-517-150-105-105-175-150-157-517 105-105-105-105-105-105-157-175-176-170-107-175-150-156-105-157-150-105-510-175-170-175 105-150-105-1_V.mp3)
  Segment 32: 142996ms ~ 143572ms (file: 251-502-507-675-752-752-750-576-567-275-752-756-706-765-752-765-576-756-756-756-756-756-756-756 507-527-256-205-725-570-752-576-576-754-571-571-576-571-576-572-576-756 507-506-2_V.mp3)
  Segment 33: 144572ms ~ 150044ms (file: 213-173-513-102-361-537-576-260-567-576-621-627-265-574-236-153-237-267-572-132-312-326-576-576-260-213-235-756-256-320-265-576-302-321-263-570-236-156-123-321-504-506-201-312-1_W.mp3)
  Segment 34: 151044ms ~ 151620ms (file: 571-571-576-574-570-571-576-576-572-570-517-501-710-705-720-702-705-157-751-765-572-562-527-715-752-751-701 576-576-507-507-107-571-762-613-612-567-576-567 623-621-612-162-162-1_W.mp3)
  Segment 35: 152620ms ~ 159316ms (file: 260-256-574-267-130-203-103-136-231-172-123-157-236-623-613-316-130-231-216-162-621-107-632-120-762-576-631-267-267-206-605-761-132-103-105-602-176-163-136-725-213-102-756-167-3_V.mp3)
  Segment 36: 160316ms ~ 160892ms (file: 367-657-317-657-576-675-576-765-675-765-765-675-672-673-176-617-671-672-315-315-306-360-316-617-365-675-635-657-567-573-357-375-765-317-361-310-362 715-576-163-265-526-675-756-5_W.mp3)
  Segment 37: 161892ms ~ 162468ms (file: 672-672-362-631-163-167-137-371-137-173-167-361-361-175-136-312-362-321-372-632-716-312-367-315-312-321-132-316-761-316-316-236-231-327-316-315-132-327 517 650-130-163-367-675-6_W.mp3)
  Segment 38: 163468ms ~ 172468ms (file: 102-735-621-107-102-216-627-321-756-754-756-260-326-657-621-213-312-657-574-263-260-103-263-726-276-275-213-751-231-263-216-326-260-627-627-263-236-120-263-526-524-620-236-103-3_W.mp3)
  Segment 39: 173468ms ~ 174044ms (file: 752-275-753-237-527-602-236-675-257-752-765-725-675-275-273-521-572-756-530-756-701 752-570-627-576-507-725-576-673-163-620-123-325-706-176-631-361-312-761-236-750-136-132-326-2_V.mp3)
  Segment 40: 175044ms ~ 184368ms (file: 126-231-267-261-260-263-532-572-320-126-162-317-361-320-230-210-321-236-320-326-321-132-263-275-723-573-123-236-310-236-162-130-312-752-572-257-260-213-231-230-326-230-106-230-5_W.mp3)
  Segment 41: 185368ms ~ 185944ms (file: 765-672-267-627-671-316-316-163-632-762-627-627-627-672-316-763-762-672 672-672-621-632-621-652-562-547-546-564-526-256-265-627-623-627-231-236-267-627-562-625-362-326-263-261-3_W.mp3)
  Segment 42: 186944ms ~ 191264ms (file: 726-267-276-270-276-726-762-765-726-137-670-123-612-762-267-127-107-672-672-176-123-726-671-610-172-716-176-276-761-217-120-726-102-120-326-320-762-726-712-726-760-761-763-307-6_W.mp3)
  Segment 43: 192264ms ~ 192840ms (file: 705-751-701-107-751-710-701-751-705-751-750-170-715-750-105-751-751-705-751-750-570-750-756 750-715-710-570-710-750-107-170-170-756-705-701-705-701-571-751-750-750-571-750-750-5_W.mp3)
  Segment 44: 193840ms ~ 194416ms (file: 657-567-657-657-675-657-657-657-657-657_W.mp3)
  Segment 45: 195416ms ~ 195992ms (file: 571-517-157-751-157-571-574-751-574-571-751-751-571-571-517 576-574-571 571-573-574-751-517-175-751-751-751-157-571-571-571-175-715-175-571-751-135-135-312-135-715-571-571-751-7_W.mp3)
  Segment 46: 196992ms ~ 197568ms (file: 317-137-375-137-137-371-753-573-375-753 370-753-107-731-137-103-301-307-103-317-310-137-103-312-721-103-103-307-310-310-137-317-317-617-753-376-371-765-756-756 705-753-107-173-1_V.mp3)
  Segment 47: 198568ms ~ 205480ms (file: 127-103-123-216-120-127-120-210-206-132-756-260-256-236-571-312-310-231-512-150-517-213-231-213-271-351-127-312-321-203-753-270-206-206-326-123-574-230-547-172-271-267-213-310-2_W.mp3)
  Segment 48: 206480ms ~ 207056ms (file: 263-627-267-263-236-261-263-523-265-267-267-236-326-321-326-326-362-326-375-376-356-673-376-675-367-673-376-627-632-367-632-362-637-672-627-637-362-361-671-362-326-613-362-627-6_W.mp3)
  Segment 49: 208056ms ~ 208632ms (file: 751-751-756-751-751-751-751-751-754-751-175-751-715-715-751-751-751 105-150-153-150-105-571-751-751-751-715-751-715-751-715-175-175-715-715-751-753 150-510-157-175-157-150-105-1_W.mp3)
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
> **Time**: 2026-04-05 11:11:54
> **AI**: gemini

---

## Response

{
"segment": 1,
"transcript": "안녕하세요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 136,
"audio_type": "speech",
"words": [
{
"word": "안녕하세요",
"start_ms": 136,
"dur_ms": 316,
"phase_lr_deg": 0.0
}
]
},
{
"segment": 2,
"transcript": "반갑습니다.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 257,
"audio_type": "speech",
"words": [
{
"word": "반갑습니다",
"start_ms": 257,
"dur_ms": 320,
"phase_lr_deg": 0.0
}
]
},
{
"segment": 3,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 4,
"transcript": "오늘은 날씨가 참 좋네요. 이런 날에는 밖에 나가서 걷고 싶어요.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 137,
"audio_type": "speech",
"words": [
{
"word": "오늘은",
"start_ms": 137,
"dur_ms": 230,
"phase_lr_deg": 0.0
},
{
"word": "날씨가",
"start_ms": 502,
"dur_ms": 103,
"phase_lr_deg": 0.0
},
{
"word": "참",
"start_ms": 726,
"dur_ms": 276,
"phase_lr_deg": 0.0
},
{
"word": "좋네요",
"start_ms": 1102,
"dur_ms": 267,
"phase_lr_deg": 0.0
},
{
"word": "이런",
"start_ms": 1636,
"dur_ms": 547,
"phase_lr_deg": 0.0
},
{
"word": "날에는",
"start_ms": 2219,
"dur_ms": 752,
"phase_lr_deg": 0.0
},
{
"word": "밖에",
"start_ms": 2971,
"dur_ms": 735,
"phase_lr_deg": 0.0
},
{
"word": "나가서",
"start_ms": 3706,
"dur_ms": 750,
"phase_lr_deg": 0.0
},
{
"word": "걷고",
"start_ms": 4732,
"dur_ms": 521,
"phase_lr_deg": 0.0
},
{
"word": "싶어요",
"start_ms": 5253,
"dur_ms": 527,
"phase_lr_deg": 0.0
}
]
},
{
"segment": 5,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 6,
"transcript": "맞아요. 저도 동감이에요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 576,
"audio_type": "speech",
"words": [
{
"word": "맞아요",
"start_ms": 576,
"dur_ms": 263,
"phase_lr_deg": 0.0
},
{
"word": "저도",
"start_ms": 1096,
"dur_ms": 326,
"phase_lr_deg": 0.0
},
{
"word": "동감이에요",
"start_ms": 1748,
"dur_ms": 273,
"phase_lr_deg": 0.0
}
]
},
{
"segment": 7,
"transcript": "혹시 이번 주말에 특별한 계획 있으신가요? 없다면 같이 영화 보러 가실래요?",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 627,
"audio_type": "speech",
"words": [
{
"word": "혹시",
"start_ms": 627,
"dur_ms": 167,
"phase_lr_deg": 0.0
},
{
"word": "이번",
"start_ms": 957,
"dur_ms": 263,
"phase_lr_deg": 0.0
},
{
"word": "주말에",
"start_ms": 1352,
"dur_ms": 621,
"phase_lr_deg": 0.0
},
{
"word": "특별한",
"start_ms": 2133,
"dur_ms": 261,
"phase_lr_deg": 0.0
},
{
"word": "계획",
"start_ms": 2557,
"dur_ms": 163,
"phase_lr_deg": 0.0
},
{
"word": "있으신가요",
"start_ms": 2883,
"dur_ms": 267,
"phase_lr_deg": 0.0
},
{
"word": "없다면",
"start_ms": 3677,
"dur_ms": 356,
"phase_lr_deg": 0.0
},
{
"word": "같이",
"start_ms": 4196,
"dur_ms": 163,
"phase_lr_deg": 0.0
},
{
"word": "영화",
"start_ms": 4489,
"dur_ms": 130,
"phase_lr_deg": 0.0
},
{
"word": "보러",
"start_ms": 4751,
"dur_ms": 261,
"phase_lr_deg": 0.0
},
{
"word": "가실래요",
"start_ms": 5249,
"dur_ms": 237,
"phase_lr_deg": 0.0
}
]
},
{
"segment": 8,
"transcript": "좋아요. 무슨 영화 볼까요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 253,
"audio_type": "speech",
"words": [
{
"word": "좋아요",
"start_ms": 253,
"dur_ms": 235,
"phase_lr_deg": 0.0
},
{
"word": "무슨",
"start_ms": 723,
"dur_ms": 325,
"phase_lr_deg": 0.0
},
{
"word": "영화",
"start_ms": 1174,
"dur_ms": 326,
"phase_lr_deg": 0.0
},
{
"word": "볼까요",
"start_ms": 1762,
"dur_ms": 263,
"phase_lr_deg": 0.0
}
]
},
{
"segment": 9,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 10,
"transcript": "요즘 새로 나온 액션 영화가 있는데 평점이 꽤 높더라고요. 그거 어때요?",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 720,
"audio_type": "speech",
"words": [
{
"word": "요즘",
"start_ms": 720,
"dur_ms": 720,
"phase_lr_deg": 0.0
},
{
"word": "새로",
"start_ms": 1716,
"dur_ms": 276,
"phase_lr_deg": 0.0
},
{
"word": "나온",
"start_ms": 2118,
"dur_ms": 172,
"phase_lr_deg": 0.0
},
{
"word": "액션",
"start_ms": 2400,
"dur_ms": 627,
"phase_lr_deg": 0.0
},
{
"word": "영화가",
"start_ms": 3137,
"dur_ms": 627,
"phase_lr_deg": 0.0
},
{
"word": "있는데",
"start_ms": 3874,
"dur_ms": 310,
"phase_lr_deg": 0.0
},
{
"word": "평점이",
"start_ms": 4296,
"dur_ms": 312,
"phase_lr_deg": 0.0
},
{
"word": "꽤",
"start_ms": 4780,
"dur_ms": 276,
"phase_lr_deg": 0.0
},
{
"word": "높더라고요",
"start_ms": 5163,
"dur_ms": 267,
"phase_lr_deg": 0.0
},
{
"word": "그거",
"start_ms": 5800,
"dur_ms": 263,
"phase_lr_deg": 0.0
},
{
"word": "어때요",
"start_ms": 6219,
"dur_ms": 301,
"phase_lr_deg": 0.0
}
]
},
{
"segment": 11,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 12,
"transcript": "Okay, sounds good to me. I love action movies. let's meet in front of the theater at 2 p.m. on Saturday.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 312,
"audio_type": "speech",
"words": [
{
"word": "Okay",
"start_ms": 312,
"dur_ms": 162,
"phase_lr_deg": 0.0
},
{
"word": "sounds",
"start_ms": 580,
"dur_ms": 106,
"phase_lr_deg": 0.0
},
{
"word": "good",
"start_ms": 788,
"dur_ms": 671,
"phase_lr_deg": 0.0
},
{
"word": "to",
"start_ms": 1599,
"dur_ms": 216,
"phase_lr_deg": 0.0
},
{
"word": "me",
"start_ms": 1917,
"dur_ms": 231,
"phase_lr_deg": 0.0
},
{
"word": "I",
"start_ms": 2469,
"dur_ms": 321,
"phase_lr_deg": 0.0
},
{
"word": "love",
"start_ms": 2910,
"dur_ms": 120,
"phase_lr_deg": 0.0
},
{
"word": "action",
"start_ms": 3132,
"dur_ms": 312,
"phase_lr_deg": 0.0
},
{
"word": "movies",
"start_ms": 3547,
"dur_ms": 103,
"phase_lr_deg": 0.0
},
{
"word": "let's",
"start_ms": 4012,
"dur_ms": 102,
"phase_lr_deg": 0.0
},
{
"word": "meet",
"start_ms": 4250,
"dur_ms": 132,
"phase_lr_deg": 0.0
},
{
"word": "in",
"start_ms": 4484,
"dur_ms": 136,
"phase_lr_deg": 0.0
},
{
"word": "front",
"start_ms": 4752,
"dur_ms": 627,
"phase_lr_deg": 0.0
},
{
"word": "of",
"start_ms": 5410,
"dur_ms": 612,
"phase_lr_deg": 0.0
},
{
"word": "the",
"start_ms": 6097,
"dur_ms": 675,
"phase_lr_deg": 0.0
},
{
"word": "theater",
"start_ms": 7092,
"dur_ms": 120,
"phase_lr_deg": 0.0
},
{
"word": "at",
"start_ms": 7300,
"dur_ms": 310,
"phase_lr_deg": 0.0
},
{
"word": "2",
"start_ms": 7823,
"dur_ms": 213,
"phase_lr_deg": 0.0
},
{
"word": "p.m.",
"start_ms": 8206,
"dur_ms": 210,
"phase_lr_deg": 0.0
},
{
"word": "on",
"start_ms": 8639,
"dur_ms": 123,
"phase_lr_deg": 0.0
},
{
"word": "Saturday",
"start_ms": 8816,
"dur_ms": 260,
"phase_lr_deg": 0.0
}
]
},
{
"segment": 13,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 14,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 15,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 16,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 17,
"transcript": "그때 만나요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 123,
"audio_type": "speech",
"words": [
{
"word": "그때",
"start_ms": 123,
"dur_ms": 567,
"phase_lr_deg": 0.0
},
{
"word": "만나요",
"start_ms": 765,
"dur_ms": 152,
"phase_lr_deg": 0.0
}
]
},
{
"segment": 18,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 19,
"transcript": "Excuse me, where is the nearest subway station, please? I think I'm lost. can you help me?",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 230,
"audio_type": "speech",
"words": [
{
"word": "Excuse",
"start_ms": 230,
"dur_ms": 263,
"phase_lr_deg": 0.0
},
{
"word": "me",
"start_ms": 599,
"dur_ms": 106,
"phase_lr_deg": 0.0
},
{
"word": "where",
"start_ms": 825,
"dur_ms": 231,
"phase_lr_deg": 0.0
},
{
"word": "is",
"start_ms": 1106,
"dur_ms": 261,
"phase_lr_deg": 0.0
},
{
"word": "the",
"start_ms": 1471,
"dur_ms": 154,
"phase_lr_deg": 0.0
},
{
"word": "nearest",
"start_ms": 1708,
"dur_ms": 236,
"phase_lr_deg": 0.0
},
{
"word": "subway",
"start_ms": 2047,
"dur_ms": 321,
"phase_lr_deg": 0.0
},
{
"word": "station",
"start_ms": 2471,
"dur_ms": 263,
"phase_lr_deg": 0.0
},
{
"word": "please",
"start_ms": 2901,
"dur_ms": 213,
"phase_lr_deg": 0.0
},
{
"word": "I",
"start_ms": 3340,
"dur_ms": 230,
"phase_lr_deg": 0.0
},
{
"word": "think",
"start_ms": 3672,
"dur_ms": 326,
"phase_lr_deg": 0.0
},
{
"word": "I'm",
"start_ms": 4094,
"dur_ms": 102,
"phase_lr_deg": 0.0
},
{
"word": "lost",
"start_ms": 4296,
"dur_ms": 213,
"phase_lr_deg": 0.0
},
{
"word": "can",
"start_ms": 4739,
"dur_ms": 263,
"phase_lr_deg": 0.0
},
{
"word": "you",
"start_ms": 5078,
"dur_ms": 260,
"phase_lr_deg": 0.0
},
{
"word": "help",
"start_ms": 5431,
"dur_ms": 236,
"phase_lr_deg": 0.0
},
{
"word": "me",
"start_ms": 5780,
"dur_ms": 267,
"phase_lr_deg": 0.0
}
]
},
{
"segment": 20,
"transcript": "Sure, go straight and turn right at the corner. The station will be on your left.",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 263,
"audio_type": "speech",
"words": [
{
"word": "Sure",
"start_ms": 263,
"dur_ms": 261,
"phase_lr_deg": 0.0
},
{
"word": "go",
"start_ms": 626,
"dur_ms": 231,
"phase_lr_deg": 0.0
},
{
"word": "straight",
"start_ms": 910,
"dur_ms": 213,
"phase_lr_deg": 0.0
},
{
"word": "and",
"start_ms": 1226,
"dur_ms": 123,
"phase_lr_deg": 0.0
},
{
"word": "turn",
"start_ms": 1399,
"dur_ms": 210,
"phase_lr_deg": 0.0
},
{
"word": "right",
"start_ms": 1661,
"dur_ms": 231,
"phase_lr_deg": 0.0
},
{
"word": "at",
"start_ms": 1942,
"dur_ms": 312,
"phase_lr_deg": 0.0
},
{
"word": "the",
"start_ms": 2306,
"dur_ms": 126,
"phase_lr_deg": 0.0
},
{
"word": "corner",
"start_ms": 2482,
"dur_ms": 123,
"phase_lr_deg": 0.0
},
{
"word": "The",
"start_ms": 2831,
"dur_ms": 150,
"phase_lr_deg": 0.0
},
{
"word": "station",
"start_ms": 3054,
"dur_ms": 231,
"phase_lr_deg": 0.0
},
{
"word": "will",
"start_ms": 3335,
"dur_ms": 123,
"phase_lr_deg": 0.0
},
{
"word": "be",
"start_ms": 3531,
"dur_ms": 321,
"phase_lr_deg": 0.0
},
{
"word": "on",
"start_ms": 3935,
"dur_ms": 310,
"phase_lr_deg": 0.0
},
{
"word": "your",
"start_ms": 4277,
"dur_ms": 310,
"phase_lr_deg": 0.0
},
{
"word": "left",
"start_ms": 4619,
"dur_ms": 231,
"phase_lr_deg": 0.0
}
]
},
{
"segment": 21,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 22,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 23,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 24,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 25,
"transcript": "Thank you so much. Have a great day.",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 231,
"audio_type": "speech",
"words": [
{
"word": "Thank",
"start_ms": 231,
"dur_ms": 321,
"phase_lr_deg": 0.0
},
{
"word": "you",
"start_ms": 612,
"dur_ms": 130,
"phase_lr_deg": 0.0
},
{
"word": "so",
"start_ms": 772,
"dur_ms": 103,
"phase_lr_deg": 0.0
},
{
"word": "much",
"start_ms": 925,
"dur_ms": 173,
"phase_lr_deg": 0.0
},
{
"word": "Have",
"start_ms": 1219,
"dur_ms": 321,
"phase_lr_deg": 0.0
},
{
"word": "a",
"start_ms": 1560,
"dur_ms": 127,
"phase_lr_deg": 0.0
},
{
"word": "great",
"start_ms": 1707,
"dur_ms": 231,
"phase_lr_deg": 0.0
},
{
"word": "day",
"start_ms": 2005,
"dur_ms": 236,
"phase_lr_deg": 0.0
}
]
},
{
"segment": 26,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 27,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 28,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 29,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 30,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 31,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 32,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 33,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 34,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 35,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 36,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 37,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 38,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 39,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 40,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 41,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 42,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 43,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 44,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 45,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 46,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 47,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 48,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 49,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 11:11:54*
