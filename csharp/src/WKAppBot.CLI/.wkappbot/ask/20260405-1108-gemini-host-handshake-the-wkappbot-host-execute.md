# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=30524  time=11:05:21  uptime=23633s  mem=485MB  system_processes=356"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_110414.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 150-175-150-157-175-175-157-175-715-175-173-715-176-170 105-150-510-517-510-517-150-157-571-157-751-157-157-751-715-751-716-175-716-175-715-175 105-150-510-157-157-157-175-157-1_W.mp3)
  Segment 2: 1576ms ~ 10432ms (file: 276-327-231-321-726-320-267-672-627-213-120-231-321-123-123-310-231-230-276-213-267-267-263-260-276-725-271-761-276-201-631-236-237-216-310-320-267-321-231-273-276-621-267-273-1_W.mp3)
  Segment 3: 11432ms ~ 17948ms (file: 213-236-216-126-103-263-263-762-267-326_W.mp3)
  Segment 4: 18948ms ~ 19524ms (file: 631-316-361-307-763-104-146-413-106-102-160-160-306-574-573-567-567-761-765-210-132-123-362 136_W.mp3)
  Segment 5: 20524ms ~ 21100ms (file: 576-576-576-756-756-576-756-576-576-756-576-576-756-752-512-504-504-506-506-506-507-503-506-506-567-506-506-507-527-756-576-576-756-567-576-756-576-756-756-756-756-756-756-752-5_W.mp3)
  Segment 6: 22100ms ~ 22676ms (file: 751-571-517-570-570-570-713-570-571-570-570-570-570-576-570-570-570-570-570-576-574 157-570-517-150-175-157-751-735-571-571-570-570-570-571-576-571-571-570-570-570-570-570-570-5_W.mp3)
  Segment 7: 23676ms ~ 24252ms (file: 236-263-263-261-672-627-627-326-263-326-263-162-263-627-672-672-675-621-613 162-106-627-612-627-627-627 627-267-672-625-627-627 672-627 762-762_W.mp3)
  Segment 8: 25252ms ~ 25828ms (file: 763-367-613-361-365-675-672-672-675-675-671-627-672-673 175-137-175-310-173-130-613-306-307-103-310-170-317-135-137-135-137-631-673-613-675-671-657-657-657-617-673 750-173-713-3_W.mp3)
  Segment 9: 26828ms ~ 27404ms (file: 137-317-136-137-163-136-176-361-671-376-176-761-705-715-675-310-715-137-107-571-754-753-305-571-546-167-176-163-316-103-137-176-173-376-167-735-371-375-176-617-367-107-176-107-7_W.mp3)
  Segment 10: 28404ms ~ 28980ms (file: 751-751-751-751-756-754-756-751-756-756-754-751 150-150-150-150-510-751-157-571-751-751-571-751-751-751-751-715-756-751-715-175-715-751-751-715-751-715 510-150-510-150-150-157-1_W.mp3)
  Segment 11: 29980ms ~ 30556ms (file: 102-102-143-126-137-103-176-176-267-612-675-675-675-627-206-206 312-301-321-302-123 326-106-160-571-107-137-731-163-137-362-321-321-130-564-316-571-132 130-136-106-160-163-106-1_V.mp3)
  Segment 12: 31556ms ~ 32132ms (file: 150-507-571-517-175-150-157-517-751-571-517-150-157-510-517-571-510-574-510 501-501-517-510-150-570-715-751-750-570-570-750-157-570-571-571-751-570-571-751-571 547-547-574-574-5_W.mp3)
  Segment 13: 33132ms ~ 33708ms (file: 167-317-135-675 571-570-750-576-756-756-715-175-761-317-317-175-657-567-615-153-713-170-175-317-316-617-763-376-316-163-675-637-317 750-705-107-761-301-376-301-603-601-603-630-6_W.mp3)
  Segment 14: 34708ms ~ 35284ms (file: 132-132-167 672-607 567-650-765-670-760-562-570-570-573-573-570-507-617-261-162-136-637-630-607-706-672-735-706-370-176-730-716-670-756-675-675-263 132-312-756-715-715-765_W.mp3)
  Segment 15: 36284ms ~ 45608ms (file: 132-312-132-103-213-321-102-132-620-260-623-267-132-126-102-106-263-236-263-235-320-357-132-123-215-321-213-260-123-130-267-273-361-106-130-761-761-167-607-107-571-517-504-547-5_W.mp3)
  Segment 16: 46608ms ~ 47184ms (file: 571-576-517-576-576-571-571-517 153-153-513-715-571-715-763-517-571-570-571-571-751-571-571-571-576-576-576-576-570-571-571-570-571-574 150-105-517-517-153-514-517-153-175-150-1_V.mp3)
  Segment 17: 48184ms ~ 48760ms (file: 170-103-135-170-715-170-710-710-170-170-170-170-137-701-103-130-137-137-170-137-173-107-710-750 135-157-175-170-137-170-173-107-130-130-173-137-103-137-170-173-175-175-173-170-1_W.mp3)
  Segment 18: 49760ms ~ 50336ms (file: 137-136 312-657-163-137-173-371-136-361-167-637-765-756-675-153-512-572-517-567-531-576-621-312-673-671-756-756-576-576-756-756-765_W.mp3)
  Segment 19: 51336ms ~ 51912ms (file: 156-516-615-163-156-571-537-573-567-561-172-167-123-127-613-156-156-657-156-571-153-156-162-167-162-167-167-167-165-165-162-136-163-136-612-162-163-615-653-615-165-136-316-162-1_W.mp3)
  Segment 20: 52912ms ~ 53488ms (file: 576-576-156-567-167-765-617-657-176-762-167-156-657-165-675-615-157-571-156-156-163-165-156-153-561-135-571-570-576-576-570-571-571-751-571-571-571-574-571-571-570-571-751-571-7_W.mp3)
  Segment 21: 54488ms ~ 55064ms (file: 576-567-756-165-173-167-761-627-756-752-765-765-657-765-765-675-617-675-617-176-167-172-173-712-762-761-176-176-173-706-756-762-756-572-576-716-761-165-156-561-617-165-615-576-6_W.mp3)
  Segment 22: 56064ms ~ 56640ms (file: 567-576-756-756-576-576-756-756 521-512-572-507-502-256-726-207-576-756-675-675-576-675-765-765-765-765-576-765-675-756-756-576-571-752-572-756-527-572-756-765-576 572-572-257-2_W.mp3)
  Segment 23: 57640ms ~ 58216ms (file: 167-235-136-132-120-217-213-203-123_V.mp3)
  Segment 24: 59216ms ~ 59792ms (file: 132 172-176-763-726-712-713-763-713-735-761-765-736-675-367-507-657-507-537-513-573-576-756-756-763-765-756-317-312-316-327-273-217-231-623-312-371-172-127-216-213-261-362-672-6_W.mp3)
  Segment 25: 60792ms ~ 67920ms (file: 310-263-132-132-627-312-265-265-276-257-375-132-572-574-256-572-215-231-231-236-275-267-267-526-352-167-136-321-263-267-621-625-126-574-267-267-312-263-627-265-267-265-542-321-2_W.mp3)
  Segment 26: 68920ms ~ 69496ms (file: 501-517-150-510-507-571-517-570-501-510-570-507-507-571-567-561-507-517-510-150-501-501-507-570-573 570-507-105-107-105-105-157-507-507-105-507-507-501-507-506-501-507-501-150-5_W.mp3)
  Segment 27: 70496ms ~ 71072ms (file: 645-652-654-651-654-651-654-654-654-654-654-651 621-625-651-654-654-654-654-654-654-564-654-564-654-564-654-625-564-605-653-265-250-562-516-502-502-512-512-501-502-541-504-504-5_W.mp3)
  Segment 28: 72072ms ~ 85320ms (file: 267-210-130-267-103-132-103-261-326-267-260-261-310-103-130-134-132-103-310-260-263-627-130-103-130-312-216-236-261-612-267-610-267-203-312-627-216-236-103-132-312-301-103-103-2_W.mp3)
  Segment 29: 86320ms ~ 94060ms (file: 765-675-763-736-765-316-316-301-671-326-103-136-765-735-657-261-206-321-632-301-130-310-257-120-203-132-132-310-103-716-673-671-627-627-316-106-571-702-273-527-572-172-713-130-1_W.mp3)
  Segment 30: 95060ms ~ 95636ms (file: 750 750-571-507-705-570-751-701-157-175-750-175-751-157-710-705-517-705-175-701-751-105-571-751-750-507-571-571-750-170-570-751 570-750-170-750-570-750-751-571-570-574-570-570-5_W.mp3)
  Segment 31: 96636ms ~ 97212ms (file: 150-150-150-507-150-501-105-157-157-154 105-105-150-105-105-154-510-150-157-157-150-150-157-517-517-157-157-571-157-517-157-105 105-150-150-105-150-157-157-517-157-157-157-150-1_V.mp3)
  Segment 32: 98212ms ~ 98788ms (file: 167-126-613 126 621-602-163 126-517_V.mp3)
  Segment 33: 99788ms ~ 100364ms (file: 132-671-617-167 710-102-103-130-612_W.mp3)
  Segment 34: 101364ms ~ 106836ms (file: 301-625-132-261-261-136-107-167-127-260-206-102-136-135-675-675-725-765-623-705-167-316-312-576-265-576-750-130-761-756-176-167-260-213-301-310-103-320-210-173-162-137-156-167-2_W.mp3)
  Segment 35: 107836ms ~ 115108ms (file: 321-512-237-572-235-102-216-231-261-123-312-215-132-321-263-627-623-213-263-267-216-572-216-267-263-756-265-102-170-132-751-312-263-261-312-261-163-572-150-765-265-106-176-571-2_W.mp3)
  Segment 36: 116108ms ~ 116684ms (file: 260-260-625-625-265-265-265-265-625-265-623-261-260-625-625-625-652-625-652-265-625-265-265 623-621-612-621-260-260-627-625-625-265-625-261-261-265-625-265-265-620-623-625-625-5_W.mp3)
  Segment 37: 117684ms ~ 118260ms (file: 543-574-563-316 627-162-617-672-265-165-613-371-176-130-317-163-216-261-126-132-157-715-713-756-157-167-572-375-751-627-120-167-123-320-213-751-157-750-325-712-753-326-167-351-5_W.mp3)
  Segment 38: 119260ms ~ 119836ms (file: 765-756-765-576-576-675-675-751-765-756-756-756-576-765-756-765-675-675-675-675-617-167-167-167-167-127-162-167-167-167-163-765-765-657-657-756-576-756-756-765-675-576-576-567-5_W.mp3)
  Segment 39: 120836ms ~ 121412ms (file: 751-751 571-570-751-576-576-756-751-753-715-753-751 175-751-715-175-715-175-175-715-751-715-715-715-175-751-751-175 570-571-571-715-751-175-715-750-175-715-175-751-750-715-713-7_V.mp3)
  Segment 40: 122412ms ~ 122988ms (file: 137-132-317-130-103-130-137-310-310-137-130-173-135-107-310 130-135-310-315-130-130-315-731-103-130-132-137-107-103-130-103-137-137 137-103-137-103-107-137-317-103-130-103-173-3_V.mp3)
  Segment 41: 123988ms ~ 133672ms (file: 103-132-215-130-527-237-231-123-617-231-273-237-675-107-103-321-267-762-106-102-123-102-236-621-103-137-320-126-216-231-236-120-120-236-231-126-120-637-106-103-321-213-263-236-2_W.mp3)
  Segment 42: 134672ms ~ 135248ms (file: 752-752-576-756-576-576-756-756 752-576-756-756-576-576-576-576-567-576-567-576-576-567-576-576-756-765-536-516-513-507-506-502-513-504-503-523-502-504-503-725-572-576-576-576-5_W.mp3)
  Segment 43: 136248ms ~ 146292ms (file: 162-312-316-216-527-207-132-123-102-312-705-316-276-271-267-237-507-627-137-312-761-276-302-312-172-130-107-213-203-716-761-521-302-236-753-721-372-215-572-752-267-732-127-761-7_V.mp3)
  Segment 44: 147292ms ~ 147868ms (file: 751-701-751-753-571-751-753-753-351 137-175-173-107-657-765-675-567-576-756-752-765-765-672-376-672-762-763-762-763-763-762-765-726-103-163-103-657-675-672-632-671-617-613-617-6_W.mp3)
  Segment 45: 148868ms ~ 149444ms (file: 175-137-173-175-713-736-173-170-176-671-671-173-170-107-107-175-107-105-715-107-175-107-107-713-715-170 107-173-137-107-173-703-173-160-130-173-716-175-103-103-173-761-173-713-7_V.mp3)
  Segment 46: 150444ms ~ 161748ms (file: 276-260-237-413-675-672-267-726-267-126-231-765-673-675-765-627-236-756-765-276-276-267-765-123-132-130-127-756-765-713-756-267-137-132-137-765-765-231-236-263-762-756-217-236-3_W.mp3)
  Segment 47: 162748ms ~ 170344ms (file: 672-627-267-576-237-102-102-107-267-627-276-276-267-126-102-102-130-160-136-136-126-574-263-523-524-237-326-213-263-327-357-160-617-527-576-321-617-625-327-762-271-130-327-302-3_W.mp3)
  Segment 48: 171344ms ~ 180164ms (file: 321-326-321-230-206-230-216-326-320-230-217-102-321-752-120-132-623-261-263-321-127-716-726-267-207-263-267-276-237-573-267-263-627-672-267-260-267-256-301-301-367-632-326-132-2_W.mp3)
  Segment 49: 181164ms ~ 181740ms (file: 157-154-157-517 570-501-507-105-517-157-157-153-150-105-517-517-571-510-571-517-570-570-574-576-574-574-576-567-576-574-576-574-574-570 150-105-105-501-571-571-150-510-157-501-7_V.mp3)
  Segment 50: 182740ms ~ 192532ms (file: 230-267-263-762-276-276-672-321-312-763-713-102-761-310-503-327-726-230-276-263-751-726-310-120-263-123-321-327-157-320-236-327-725-207-725-120-321-321-173-273-320-627-267-236-3_W.mp3)
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
> **Time**: 2026-04-05 11:08:22
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
"transcript": "결국 투자의 성패는 얼마나 오랫동안 시장에서 살아남느냐에 달려 있습니다. 자만하지 않고 겸손한 자세로 시장을 대하는 것이 가장 중요합니다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "결국", "start_ms": 100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "투자의", "start_ms": 450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "성패는", "start_ms": 850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "얼마나", "start_ms": 1300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "오랫동안", "start_ms": 1700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "시장에서", "start_ms": 2200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "살아남느냐에", "start_ms": 2700, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "달려", "start_ms": 3450, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "있습니다.", "start_ms": 3750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "자만하지", "start_ms": 4400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "않고", "start_ms": 4950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "겸손한", "start_ms": 5300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "자세로", "start_ms": 5800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "시장을", "start_ms": 6250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "대하는", "start_ms": 6700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "것이", "start_ms": 7150, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 7450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word

---
*Generated by WKAppBot ask gemini — 2026-04-05 11:08:22*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=24932  time=11:07:13  uptime=24518s  mem=395MB  system_processes=359"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_110149.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 571-570-750-705-107-170-170-157-751-751 750-507-105-751-517-705-175-701-715-107-157-175-751-517-107-170-175-750-507-570-751-571-750-150-507-751-750-571-570-570-751 750-157-750-7_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 756-756-570-507-705-570-750-570-705-750-571-571-570-756-765-756-765-756-756-765-752_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 672-250-705-675-765-751-572-571-572-572-571-571-571-756 256-752-625-276-576-570-571-751-576-756-576-751-756-765 206-605-527-765-765-576-576-576-756-576-576-756-756-756-576-756-5_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 175-175-751-751-751-715-175-751-715-751-751-175-715 507-175-510-150-150-150-153-175-715-751-751-751-751-751-751-751-715-751-715-751-715-751-751-751-751-751-751-751-750 157-150-5_W.mp3)
  Segment 5: 6304ms ~ 14872ms (file: 312-267-763-756-325-310-574-574-231-572-574-132-574-350-325-326-572-572-123-627-236-230-216-362-103-267-230-574-576-275-326-315-725-237-123-125-127-103-103-207-765-576-206-102-1_W.mp3)
  Segment 6: 15872ms ~ 16448ms (file: 517-517-571-517-570-570-517-567-510-576-517-517-517-173-137-751-751-751-751-751-571-574-571-576-571-576-576-571-576-576-576-576-157-570-570-570 135-513-517-156-157-570-571-571-7_V.mp3)
  Segment 7: 17448ms ~ 18024ms (file: 620-610-601-602-623-612-160-610-160-620-610_V.mp3)
  Segment 8: 19024ms ~ 19600ms (file: 761-126-276-127-750-136-257-627-217-267-261-762-751-213-602-752-261-136-132-132_V.mp3)
  Segment 9: 20600ms ~ 21176ms (file: 106-607-106-236-132-263-136-263-102-601-106-162-163-167-162-613-136-103-136-163-163-163-163-620-213-730-670-671-672-316-302-301-103-601-602-320-320-263-537-572-576-570-703-327-6_V.mp3)
  Segment 10: 22176ms ~ 22752ms (file: 756-756-756-756-756-756-675-671-576-576 765-675-657-756-765-675-756-765-576-756-576-756-756-756-756-756-756-675 752-576-756-576-576-756-576-576-567-756-756-675 752-572-752_W.mp3)
  Segment 11: 23752ms ~ 24328ms (file: 751 574-175-750-507-751-750-751-571-765-765-756-756-501-150-517-501-105-570-157-571-510-571-501-570-510-570-750-571-570-571-765-765-756-756-765-765-517-507-510-570-715-570-571-5_V.mp3)
  Segment 12: 25328ms ~ 31880ms (file: 721-176-276-514-201-627-765-107-310-276-210-230-123-120-132-317-710-753-751-514-576-320-237-307-576-576-572-257-762-756-765-572-570-251-275-275-760-257-120-123-260-106-217-726-2_W.mp3)
  Segment 13: 32880ms ~ 40116ms (file: 263-236-321-263-672-375-760-267-216-210-263-267-207-756-627-316-613-176-102-675-123-321-312-130-136-173-237-123-102-216-276-127-327-620-260-120-260-167-107-160-610-162-160-132-3_W.mp3)
  Segment 14: 41116ms ~ 41692ms (file: 751-751-715-175-753-751-715-173-715-751-751-731-173-751-751 751-753-715-715-751-751-715-713-715-715-571-715-751-157-715-173-753-751-571-751 750-756-751-754-751-701-157-715-173-1_W.mp3)
  Segment 15: 42692ms ~ 43268ms (file: 267-627-627-673-675-671-670-675-670-617-671-621-627-627 673-673-216-261-261-765-675-167-617-625 752-725 267-267 627-167-617-267-672-627 267-627 751-263-523-263-260 671-761-760-5_W.mp3)
  Segment 16: 44268ms ~ 44844ms (file: 567-567-657-567-567-657-567-567-567-657-657-567-657-675-312-371-173-371-367-564-534-542-534-534-514-504-504-504-541-541-546-514-576-567-576-567-567-567-657-567-576-567-657-657-5_W.mp3)
  Segment 17: 45844ms ~ 46420ms (file: 613-617-102-672-162-602-610-623-602-160-103-625-312-136-601-236-627-263-621-627-126-103-106-675-103-163-675-657-316-163-136-617-126-163-157-675-165-167-160-163-130-526-623-176 1_V.mp3)
  Segment 18: 47420ms ~ 53684ms (file: 572-275-273-236-312-571-156-231-571-230-230-751-752-230-572-572-753-702-102-275-725-567-752-276-135-175-203-326-203-231-120-157-725-132-175-261-507-507-572-231-236-275-756-723-2_V_stt_finally,_united_way_of_65,_revealed.mp3)
  Segment 19: 54684ms ~ 55260ms (file: 105-157-517-157-517-501-501-517-501-150-510-105-501-103-105-715-571-150-157-517-517-576-567-571-517-574-571 501-105-517-507-157-571-501-510-501-510-510-156-157-517-157-510-510-5_V.mp3)
  Segment 20: 56260ms ~ 67672ms (file: 130-270-720-206-207-207-203-203-201-132-203-673-236-237-132-137-302-320-276-267-263-267-103-320-574-765-270-126-326-102-236-230-325-176-126-765-726-237-756-762-213-310-103-213-1_W.mp3)
  Segment 21: 68672ms ~ 76952ms (file: 261-261-326-312-107-275-574-527-123-756-576-267-321-567-263-527-572-216-752-756-267-267-765-167-107-103-130-130-130-103-321-573-574-126-267-130-130-132-327-132-132-103-573-103-2_W.mp3)
  Segment 22: 77952ms ~ 78528ms (file: 102-561-176-132-132-160-160-163-162-132-103-163-736-301-310-326-602-623-672-167-126-163-705-607-607-231-320-231-612-201-132-361-671-176-126-206-327-607-263-206-260-562-605-671-7_V.mp3)
  Segment 23: 79528ms ~ 88204ms (file: 170-712-731-370-360-672-736 621-762-706-760-612-762-670-736-123-763-167-317-123-763-170-370-736-716-713-713-372-103-362-312-136-326-376-276-726-760-167-316-761-673-765-765-710-7_W_stt_clicking.mp3)
  Segment 24: 89204ms ~ 89780ms (file: 751-751-753-751-751-751-571 150-150-510-150-157-751-751-571-751-751-751-751-751-751-751-715-715-715-751-751-710-510-103-310-130-310-130-132-130-132-312-310-130-163-301-130-157-1_V.mp3)
  Segment 25: 90780ms ~ 91356ms (file: 105-105-105-153-153-576-576-576-235-657-765-675-132 167-362-623-562-562 367-562-173_W.mp3)
  Segment 26: 92356ms ~ 92932ms (file: 570-571-750-570-750-751-750-571-751-571-751-571-570-571-715-715-175-751-574-574-574-574-574-751_V.mp3)
  Segment 27: 93932ms ~ 94508ms (file: 267-765-702-267-126-602-627-725-750-564 627-261-302-231-136-312-672-670-316-163-156-631-305-153-615-137-613-307-136-137-163-301-136-371-601-605-576-367-376-567-365-657-635-765-7_W.mp3)
  Segment 28: 95508ms ~ 96084ms (file: 765-576-756-576-526-150-571-157-576-576-517-560-517-510-513-501-561-526-576-516-576-571-576-567-571-530-567-572-572-561-567-561-561-537-570-562-561-567-576-765-571-576-657-725-5_W.mp3)
  Segment 29: 97084ms ~ 97660ms (file: 756-765-756-756-751-765-756-765-756-756-756-756-756-751-756-756-756-104-157-107-176-170-715-751-175-751-175-156-751-175-715-751-571-175-756-157-715-175-175-756-751-756-751-745-7_W.mp3)
  Segment 30: 98660ms ~ 104492ms (file: 726-257-271-105-627-170-102-103-713-106-210-206-237-231-206-207-215-231-103-203-312-372-301-316-260-163-103-316-136-167-751-267-213-102-103-301-213-103-120-102-137-130-301-103-1_W.mp3)
  Segment 31: 105492ms ~ 106068ms (file: 130-510-153-103-103-127-132-203-302-106-106-130-260-321-236-123-162-136-361-136-132-137-267-301-126-102-236-206-103-136-135-130-130-301-130-631-136-136-136-361-136-136-301-326-1_V.mp3)
  Segment 32: 107068ms ~ 107644ms (file: 731-765-715-765-765-765-765-756-675-765-765-657-756-576-675-576-675-657-376-763-126-673-367-157-167-162-167-176-756-675 167-167-167-176-167-176-162-126-162-765-756-765-675-675-7_W.mp3)
  Segment 33: 108644ms ~ 109220ms (file: 135-156-136-123-136-102-126-123-136-563-627-361-657-671-623-612-106-106-132-162-173-365-615-617-163-103-657-175-130-613-167-162-170-103-316-371-107-103-167-106-136-167-103-136-6_V.mp3)
  Segment 34: 110220ms ~ 110796ms (file: 572-652-627-765-617-761-671-261-623-263-327-362-326-236-762_W.mp3)
  Segment 35: 111796ms ~ 117844ms (file: 276-752-273-320-672-276-176-764-576-210-213-762-102-132-173-301-751-130-102-103-321-273-103-761-671-517-576-173-137-317-653-275-301-306-613-326-356-235-260-763-362-230-231-671-3_W.mp3)
  Segment 36: 118844ms ~ 119420ms (file: 672-756-163-136-165-163-613-163-162-163-315-167-763-752-756-675-756-675-567-765-675-675-576-756-765-675-576-675-765-756-756-765-672-617-615-163-163-631-163-136-163-163-135-126-2_W.mp3)
  Segment 37: 120420ms ~ 127764ms (file: 130-136-756-231-123-130-123-543-301-136-130-301-236-132-712-127-321-102-103-216-320-230-312-130-106-136-137-271-231-230-361-123-301-132-301-130-136-671-617-756-506-574-231-256-1_V.mp3)
  Segment 38: 128764ms ~ 129340ms (file: 302-627-123-170-167-156-135-617-167-706-705-705-756-306-312-316-306-213-230-257-257-203-261-321-123-260-201-230-302-301-132-312-237-623-230-103-123-312-132-176-127-136-132-312-3_W.mp3)
  Segment 39: 130340ms ~ 130916ms (file: 507-250-256-506-265-526-250-257-567-576-756-567-765-756-756-756-756-765-756-765-756 572-570-572-756-257-520-502-765-567-756-675-756-765-756-756-576-756-756-567-752-765-675 520-5_V.mp3)
  Segment 40: 131916ms ~ 139260ms (file: 613-163-154-145-103-132-621-261-206-260-126-162-162-123-261-136-261-216-206-267-206-260-627-267-263-132-216-263-267-236-572-265-263-326-326-263-256-652-263-321-326-601-162-267-2_W_stt_off_his_millions_of_unemployed_that_if.mp3)
  Segment 41: 140260ms ~ 150700ms (file: 623-310-267-213-172-136-103-756-263-236-327-537-102-107-176-132-705-760-612-265-570-267-260-260-321-735-127-517-573-576-326-367-376-750-103-103 751-216-123-160-153-261-312-267-2_V.mp3)
  Segment 42: 151700ms ~ 152276ms (file: 312-317-276-725-237-327-310-372-267-362-361-657-675-305-167-617-736-367-376-327-316-301-312-103-301-310-263-621-261-216-623-126-130-102-130-162-261-621-213-132-326-316-106-176-1_W.mp3)
  Segment 43: 153276ms ~ 158928ms (file: 726-260-107-127-107-216-751-103-631-263-261-123-316-617-216-126-167-175-765-175-765-576-175-657-132-213-176-213-123-102-216-312-160-316-126-120-167-617-312-132-570-210-256-607-6_W.mp3)
  Segment 45: 160928ms ~ 168524ms (file: 567-571-107-275-236-163-136-312-312-162-213-130-132-625-756-136-301-257-157-263-265-261-236-126-652-675-265-256-675-567-627-263-572-572-261-236-265-216-527-265-267-257-567-216-3_W.mp3)
  Segment 46: 169524ms ~ 170100ms (file: 527-502-520-205-712-257-270-750-570-567-170-653-675-675-675-671-675_V.mp3)
  Segment 47: 171100ms ~ 171676ms (file: 137-710-173-135-317-135 735-103-371-705-135-173-716-310-736-603-613-607-671-761-173-130-312-173-301-301-317-130-173-137-137-152-156 317-173-713-173-175-137-310-310-613-107-301-1_V.mp3)
  Segment 48: 172676ms ~ 173252ms (file: 715-175-507-751-753-705-750-107-107-751-153-751-517 517-571-517-571-571-517-157-715-170-701-170-107-175-157-175-175-715-751-507-571-750-751-570 517-157-571-517-571-715-571-175-1_W.mp3)
  Segment 49: 174252ms ~ 174828ms (file: 632-576-276-235-753-672-321-326-726-136-650-127-135-237-317-265-762-602-752-267-276-520-612-756-167-756-127-251-571-107 276-752-761-156-137-514-752-725-752-256-567-507-236-725-7_V.mp3)
  Segment 50: 175828ms ~ 176404ms (file: 150-105-150-510-501-510-150-510-150-501-517-105 105-150-510-150-507-517-517-150-517-107-105-150-105-150-150-107-105-517-571-150-517-150-150-517-510-150-501-570-510 105-150-105-1_V.mp3)
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
> **Time**: 2026-04-05 11:08:23
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
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "안녕", "start_ms": 50, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "하세요", "start_ms": 300, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "반갑습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "반갑", "start_ms": 40, "dur_ms": 260, "phase_lr_deg": 0.0},
{"word": "습니다", "start_ms": 300, "dur_ms": 270, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "오늘 날씨가 좋네요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 60, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 240, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "Where are you from?",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Where", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "are", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 280, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "from?", "start_ms": 400, "dur_ms": 170, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "저는 한국에서 왔습니다. 지금은 서울에 살고 있어요.",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "한국에서", "start_ms": 550, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "왔습니다.", "start_ms": 1400, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "지금은", "start_ms": 2500, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "서울에", "start_ms": 3200, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "살고", "start_ms": 4000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "있어요.", "start_ms": 4600, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "Interesting.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "Interesting.", "start_ms": 80, "dur_ms": 490, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "(noise)",
"speaker": null,
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 8,
"transcript": "한국어 공부가 재미있어요?",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "공부가", "start_ms": 250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "재미있어요?", "start_ms": 400, "dur_ms": 170, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "네, 조금 어렵지만 보람차요.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 70, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 180, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "어렵지만", "start_ms": 310, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "보람차요.", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "That is great to hear.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "That", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 140, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "great", "start_ms": 220, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 340, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "hear.", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "감사합니다.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "감사", "start_ms": 50, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "합니다.", "start_ms": 300, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "혹시 추천해주실 만한 한국 드라마가 있을까요?",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "혹시", "start_ms": 120, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "추천해주실", "start_ms": 450, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "만한", "start_ms": 1300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 1900, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "드라마가", "start_ms": 2600, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "있을까요?", "start_ms": 3600, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "음, '나의 아저씨'나 '미생'을 추천해요. 일상적인 표현이 많거든요.",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "음,", "start_ms": 150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "'나의", "start_ms": 500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아저씨'나", "start_ms": 950, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "'미생'을", "start_ms": 1600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "추천해요.", "start_ms": 2200, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "일상적인", "start_ms": 3000, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "표현이", "start_ms": 4000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "많거든요.", "start_ms": 4700, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "I see, thank you.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "see,", "start_ms": 120, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "thank", "start_ms": 280, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "you.", "start_ms": 380, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "(music)",
"speaker": null,
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 16,
"transcript": "그럼 다음에 또 봐요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 60, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "다음에", "start_ms": 200, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 320, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "봐요.", "start_ms": 400, "dur_ms": 170, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "See you later!",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 30, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 150, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "later!", "start_ms": 250, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "Finally, United Way of 65, revealed.",
"speaker": "speaker_3",
"confidence": 0.92,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "Finally,", "start_ms": 110, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "United", "start_ms": 800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "Way", "start_ms": 1350, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "of", "start_ms": 1650, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "65,", "start_ms": 1850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "revealed.", "start_ms": 2300, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "안녕!",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "안녕!", "start_ms": 40, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "도움이 필요하면 언제든 물어보세요.",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "도움이", "start_ms": 90, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "필요하면", "start_ms": 600, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "언제든", "start_ms": 1300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "물어보세요.", "start_ms": 1900, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "알겠습니다. 정말 감사합니다.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "알겠습니다.", "start_ms": 120, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 1000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "감사합니다.", "start_ms": 1450, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "Bye for now.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Bye", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "for", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "now.", "start_ms": 300, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "(noise)",
"speaker": null,
"confidence": 0,
"language": "en",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 24,
"transcript": "좋은 하루 되세요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 60, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "하루", "start_ms": 200, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "되세요.", "start_ms": 320, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "You too!",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "You", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "too!", "start_ms": 180, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "다음에 또 만나요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 200, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "만나요.", "start_ms": 280, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "Have a nice weekend.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "Have", "start_ms": 60, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 180, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "nice", "start_ms": 260, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "weekend.", "start_ms": 380, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "주말 잘 보내세요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "주말", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "보내세요.", "start_ms": 300, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "Thank you, see you.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Thank", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "you,", "start_ms": 160, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "see", "start_ms": 260, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "you.", "start_ms": 360, "dur_ms": 170, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "어떤 음식을 가장 좋아하세요?",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "어떤", "start_ms": 100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "음식을", "start_ms": 550, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 1400, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "좋아하세요?", "start_ms": 2100, "dur_ms": 900, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "저는 비빔밥을 정말 좋아해요.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 80, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "비빔밥을", "start_ms": 230, "dur_ms": 170, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 400, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "좋아해요.", "start_ms": 500, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "Sounds delicious.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Sounds", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "delicious.", "start_ms": 220, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "다음에 같이 먹으러 가요.",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 70, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "먹으러", "start_ms": 300, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "가요.", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "좋아요!",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "좋아요!", "start_ms": 30, "dur_ms": 540, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "언제가 편하세요? 저는 주말이 괜찮아요.",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "언제가", "start_ms": 110, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "편하세요?", "start_ms": 800, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "저는", "start_ms": 1800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "주말이", "start_ms": 2400, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "괜찮아요.", "start_ms": 3200, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "Saturday is fine with me.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Saturday", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 230, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "fine", "start_ms": 310, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "with", "start_ms": 410, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "me.", "start_ms": 480, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "그럼 이번 주 토요일에 봬요. 장소는 나중에 정해요.",
"speaker": "speaker_2",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 130, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이번", "start_ms": 550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "주", "start_ms": 1050, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "토요일에", "start_ms": 1300, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "봬요.", "start_ms": 2200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "장소는", "start_ms": 2800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "나중에", "start_ms": 3500, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "정해요.", "start_ms": 4300, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "Great idea, see you then.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Great", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "idea,", "start_ms": 170, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "see", "start_ms": 320, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 420, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "then.", "start_ms": 500, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "(noise)",
"speaker": null,
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 40,
"transcript": "Off his millions of unemployed that if.",
"speaker": "speaker_3",
"confidence": 0.91,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "Off", "start_ms": 120, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "his", "start_ms": 550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "millions", "start_ms": 900, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "of", "start_ms": 1900, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "unemployed", "start_ms": 2200, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "that", "start_ms": 3500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "if.", "start_ms": 3900, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "한국 음식을 좋아하시는군요.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "한국", "start_ms": 100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "음식을", "start_ms": 550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "좋아하시는군요.", "start_ms": 1100, "dur_ms": 900, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "I love Korean food.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "love", "start_ms": 120, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "Korean", "start_ms": 270, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "food.", "start_ms": 450, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "특히 매운 음식을 잘 드시나요?",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "특히", "start_ms": 80, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "매운", "start_ms": 550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "음식을", "start_ms": 1100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 1700, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "드시나요?", "start_ms": 2000, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "조금 맵지만 좋아해요. 떡볶이도 좋아하고요.",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "조금", "start_ms": 110, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "맵지만", "start_ms": 700, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "좋아해요.", "start_ms": 1400, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "떡볶이도", "start_ms": 2300, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "좋아하고요.", "start_ms": 3300, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "That sounds spicy but tasty.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "That", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "sounds", "start_ms": 140, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "spicy", "start_ms": 260, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "but", "start_ms": 410, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "tasty.", "start_ms": 490, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "네, 정말 맛있어요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "맛있어요.", "start_ms": 300, "dur_ms": 270, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "Are you busy right now?",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Are", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 130, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "busy", "start_ms": 230, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "right", "start_ms": 350, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "now?", "start_ms": 450, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "아니요, 괜찮아요. 왜요?",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "아니요,", "start_ms": 60, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "괜찮아요.", "start_ms": 200, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "왜요?", "start_ms": 320, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "I was just wondering.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "was", "start_ms": 120, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "just", "start_ms": 220, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "wondering.", "start_ms": 340, "dur_ms": 230, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 11:08:23*
