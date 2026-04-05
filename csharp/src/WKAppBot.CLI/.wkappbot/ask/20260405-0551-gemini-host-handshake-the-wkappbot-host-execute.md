# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=9236  time=05:49:26  uptime=13995s  mem=466MB  system_processes=360"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_054839.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 153-351-531-513-571-357-157 103-105-157-157-137-175-756-175-173-713-175-175-176-173-105-150-157-715-750-571-750-175-170-710-517-157-571-175-751-157 105-105-715-571-751-157-715-1_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 163-176-657-651 367-756-167-270-267-206-267-312-267-267-320-267-157 671 317-312-276-310-312-130_V.mp3)
  Segment 3: 3152ms ~ 11360ms (file: 756-167-231-260-607-123-260-206-207-157-214-132-654-362-632-316-361-213-321-317-361-513-312-327-130-132-532-207-271-126-612-120-127-125-276-756-137-230-701-271-102-103-152-760-1_W.mp3)
  Segment 4: 12360ms ~ 12936ms (file: 301-137-713-315-130-175-375-317-135-753-715 753-751-731-731-731-371-713-317-301-731-103-175-132-310-310-731-170-137-173-375-317-135-517-175-173-135-317-137-175-703 701-705-107-1_V.mp3)
  Segment 5: 13936ms ~ 14512ms (file: 127-173-137-137 157-157-517-715-150-175-107-157-710-157-170-715-157-175-157-710-571-137-157-517-517-751-517-571-175-137-157-157 517-170-517-150-570-751-150-157-751-157-107-175-1_V.mp3)
  Segment 6: 15512ms ~ 16088ms (file: 175-107-701-173-175-710-713-701-710-307-730-735-370 173-307-103-301-753-310-301-173-103-107-715-371-715-130-301-137-701-135-107-701-317-735-107-157-573-137-751-170-735 137-301-1_V.mp3)
  Segment 7: 17088ms ~ 25223ms (file: 517-175-206-123-132-310-130-130-103-317-310-301-736-705-103-123-136-361-263-326-623-123-321-206-713-102-132-621-261-312-126-176-675-261-716-312-375-326-106-763-321-260-236-572-5_W.mp3)
  Segment 8: 26223ms ~ 26799ms (file: 574-103-107-107-106-136-315-325-251-165-710-306-360-321-736-103-361-623-136_V.mp3)
  Segment 9: 27799ms ~ 28375ms (file: 135-156-165-651-651-615-615-615-752-726-752-756-765-756-765-752-576-756-756-756-756-765-756-756-756-756-756-765-756-756-756 576-507-571-501-153-132-123-132_W.mp3)
  Segment 10: 29375ms ~ 29951ms (file: 317-721-123-760-762-627-267-103-102-130-156-135-135-135-315-573-567-576-567-567-576-576-567-657-650-657-162-136-103 371-312_W.mp3)
  Segment 11: 30951ms ~ 31527ms (file: 123-765-325-316-367-316-317-371-360-367-312-672-725-216-132-132-137-371-137-657-163-132-325-570-312-507-273-265-162-271-321-571-276-560-127-321-206-276-256-570-261-723-503-756-7_V.mp3)
  Segment 12: 32527ms ~ 33103ms (file: 627-654-507-175-362-301-312-132-602-320-371-132-162-601-603-620-260-203-103-360-632-301-301-310-301-132-312-103-103-132-137-167-261-126-261-516-201-312-301-103-107-137-310-312-1_W.mp3)
  Segment 13: 34103ms ~ 34679ms (file: 271-527-752-756-231-507-372-571-752-756-326-753-572-532-237-752-625-276-156-756-367-257-326-572-564-672-376-257-756-123-620-265 706-603-312-756-570-132 372-267-670-576-527-752-7_V.mp3)
  Segment 14: 35679ms ~ 36255ms (file: 136-103-103-215-671-361-671-574-570 153-105-715-751-754-751-574-571-157-751-752 302-302-136-132 102-751-163-132-136-136-312-326-573-765-267-276-645-517-256-216-126-632-165-162-1_V.mp3)
  Segment 15: 37255ms ~ 37831ms (file: 602-230-132-103-103-620-601-106-602-203-206-261-132-201-512-510-621-163-360-623-263-762-276-236-236-103-103-136-321-320-302-301-306-326-301-102-136-312-605-106-103-102-260-267-2_V.mp3)
  Segment 16: 38831ms ~ 39407ms (file: 157-156-517-501-150-517-517-157-157-170-174-157 105-105-105-105-105-150-105-150-150-150-150-140-107-105-150-105-150-150-105-105-150-150-105-105 105-105-105-105-105-105-105-107-1_V.mp3)
  Segment 17: 40407ms ~ 45519ms (file: 132-317-316-263-162-301-321-123-320-301-250-132-623-162-320-136-310-357-351-153-573-365-153-150-351-162-260-257-103-230-320-302-315-130-273-726-627-671-731-317-157-625-201-203-2_W.mp3)
  Segment 18: 46519ms ~ 47095ms (file: 571-157-571-751-571-571-571-571-751-501-510-153-571-517-153-173-175-715-175-751-715-715-751-751-751-576-571-576-576-517-571-576-576-517-571-571-570-573-570-507-517-513-517-105-1_V.mp3)
  Segment 19: 48095ms ~ 54323ms (file: 312-132-136-130-310-301-573-307-163-103-107-172-173-574-762-267-312-132-263-216-153-273-713-762-175-317-657-716-756-371-726-367-167-317-130-210-106-106-137-130-312-130-316-130-1_W.mp3)
  Segment 20: 55323ms ~ 55899ms (file: 756-751-175-756-751-751-175-571-571-175-157-137 105-105-105-150-175-105-175-175-176-715-175-751-756-751-571-175-751-571-576-751-571-751-175-157-756-754-756-756-751-756-105-105-1_W.mp3)
  Segment 21: 56899ms ~ 57475ms (file: 527-650-632-132-576-675-657-765-316-375-531-573-315-317-137-317-317-137-317-317-137-371-317-137-607-675-765-675-765-765-675-765-765-765-763-672-762_W.mp3)
  Segment 22: 58475ms ~ 59051ms (file: 615-610-162-167-765-671-576-167-162-162-126-167-157-103-625-617-126-216-502-132-312-132-162-653-315-123-163-162_V.mp3)
  Segment 23: 60051ms ~ 60627ms (file: 132-310-130-301-137-103-137-317-312-130-312-301-317-301-132-312-301-371-710-317-301 135-310-130-135-310-130-301-103-315-137-103-301-310-317-137-103-317-123-137-130-103-312-321-3_V.mp3)
  Segment 24: 61627ms ~ 62203ms (file: 621-120-267-276-260-621-267-261-263 623-765-672-672-627-627-263_W.mp3)
  Segment 25: 63203ms ~ 63779ms (file: 175-170-173-732-170-712-726-172-713-172-172-137-172 517-157-517-517-517-135-157-517-157-517-175-375-157-715-137-317-175-751-715-175-175-135-157-571-157-107 173-157-713-105-173-1_W.mp3)
  Segment 26: 64779ms ~ 75903ms (file: 107-120-172-102-103-137-213-267-236-137-316-103-103-103-107-312-102-716-132-123-130-321-103-103-130-213-216-103-327-261-160-103-721-215-217-103-310-103-173-123-361-137-765-130-3_V.mp3)
  Segment 27: 76903ms ~ 77479ms (file: 351-375-103-153-157-153-137-135-537-137-537-157-715-103-753 103-135-103-135-153-135-573-135-315-153-135-153-157-713-310-130-137-135-317-137-352-130-132-130-357 315-135-315-130-1_V.mp3)
  Segment 28: 78479ms ~ 79055ms (file: 160-136-106-103-106-163-132-321-263-267-126-162-310-136-261-762-276-136-162-671-671-163-132-103-136-167-163-106-163-132-162-206-521-572-167-763-162-136-613-136-126-317-612-216-6_V.mp3)
  Segment 29: 80055ms ~ 80631ms (file: 517-570-573-753-570-570-574-130-150-153-510-571-512-517-765-107-153-512-571-571-157-576-517-512-571-517-571-175-715-157-157-157-715-175-157-571-157-751-175-751-517-751-571-571-5_V.mp3)
  Segment 30: 81631ms ~ 82207ms (file: 715-175-571-157-170-175-517-517-715-517-157-753-571-175-756-751-756-751-756-576-576-567-765 107-170-157-175-170-175-715-571-571-517-567-576-571-571-715-567-157-571-576-567-576-7_V.mp3)
  Segment 31: 83207ms ~ 83783ms (file: 730-753-370-701-103-173-730 371-753-317-137-175-103-137-735-137-607-370-103-103-316-103-371-317-703-301-137-710-173-317-371-173-730-735-713 710-705-731-317-103-715-751-375-301-7_V.mp3)
  Segment 32: 84783ms ~ 85359ms (file: 352-623-623-362-246-127-157-612 167_V.mp3)
  Segment 33: 86359ms ~ 97051ms (file: 762-236-620-267-263-260-263-627-267-273-732-276-623-160-130-150-514-135-130-130-130-130-165-153-725-176-736-763-637-736-312-132-126_W.mp3)
  Segment 34: 98051ms ~ 98627ms (file: 213-236-362-367-361-376-637-637-673-367-637-637-672-673-637-673-637-637-367-637-673-617-621-637-632-362-367-360-360-632-367-367-627-657-265-132-573-531-547-537-571-156-567-567-6_W.mp3)
  Segment 35: 99627ms ~ 107583ms (file: 572-750-765-267-267-256-576-672-260-216-213-123-130-103-201-231-216-756-672-261-627-126-261-725-267-236-302-132-103-123-321-263-236-263-103-123-612-365-150-513-136-301-231-236-2_V.mp3)
  Segment 36: 108583ms ~ 109159ms (file: 230-321-312-312-567-203-371-176-765 576-265-517 375-235-706-265-762-657-165-517-156-561-756-507-213-150-136-136-762-267-312-571-627-236-527-213-506-257-675-706-517-132-672-751-3_V.mp3)
  Segment 37: 110159ms ~ 110735ms (file: 637-637-673-637-637-672-637-632-632-637-517-531-571-105-715-750-175-103-576-576-657-153-510-567-567 615-671-617-617-617-615-631-162-613-163-165-163-167-613-613-613-163_W.mp3)
  Segment 38: 111735ms ~ 117171ms (file: 312-312-321-302-673-763-760-760-672-726-716-753-231-132-260-231-723-172-302-326-312-302-310-326-320-753-263-276-207-173-256-230-263-370-137-230-276-276-276-321-103-310-103-302-7_W.mp3)
  Segment 39: 118171ms ~ 118747ms (file: 160-162-136-160-103-163-613-326-612-615-167-613-136-106-632-632-627-162-136-103-132-517-162-132-675-106-571-123-162-103-136-132-103-103-617-136-613-160-136-167-167-326-561-136-1_V.mp3)
  Segment 40: 119747ms ~ 120323ms (file: 102-617-762-670-761-761-361-362-613-213-671-670-670-365-130-310-672-120-673-763-167-716-671-760-167-163-106-310-132-132-213-130-167-316-213-231-321 627-367-130-301-310-310-312-6_W.mp3)
  Segment 41: 121323ms ~ 121899ms (file: 301-103-103-135-310-132-137-315-301-135-137-312-135-103-310-317-301-173-301 103-103-135-130-103-315-137-137-315-137-137-173-301-301-137-130-135-137-132-310-317-310-103-310-132-1_V.mp3)
  Segment 42: 122899ms ~ 123475ms (file: 150-157-157-150-105-507-570-571-510-517-513-530-573-513-517-571-517-501 105-517-157-517-157-157-517-507-510-105-517-105-501-105-150-105-510-517-157-517-105-150-501-517-153-507-5_W.mp3)
  Segment 43: 124475ms ~ 129299ms (file: 762-216-201-102-126-267-203-217-210-172-173-701-130-105-103-132-201-276-267-206-276-267-102-123-216-172-172-726-751-370-371-371-257-276-721-103-310-312-260-170-176-271-715-120-2_W.mp3)
  Segment 44: 130299ms ~ 130875ms (file: 756-756-716-137-130-736-672-136-732-132-756-756-317-531-176-321-726-217-716-127-217-572-317-312-153-750-571-756-271-507-125-541-723-705-621-573-705-576_V.mp3)
  Segment 45: 131875ms ~ 137203ms (file: 276-267-267-627-267-216-267-670-761-762-267-263-106-326-752-312-132-267-267-276-627-261-126-517-123-321-627-237-267-321-312-261-236-627-627-263-276-726-261-726-206-126-263-261-6_W.mp3)
  Segment 46: 138203ms ~ 145043ms (file: 327-321-617-321-632-623-321-231-201-312-321-725-231-321-213-126-167-627-761-623-216-623-123-751-362-267-216-231-756-173-123-574-327-321-751-715-263-263-267-267-236-617-102-312-3_W.mp3)
  Segment 47: 146043ms ~ 146619ms (file: 625-657 623-621-627-620-621-623-265-621-263-265-627-627-267-267-627-267-267-625-652-625-652-652-657-657-652-652-265-625 261-260-623-620-620-620-620-620-621-267-267-621-260-261-2_W.mp3)
  Segment 48: 147619ms ~ 148195ms (file: 612-671-261-162-631-672-621-162-231-136-263-362-126-236-612-132-132-102-132-132-123-216-621-163-162-162-126-612-162-162-612-617-126-612-163-632-623-316-136-627-326-362-623-123-1_W.mp3)
  Segment 49: 149195ms ~ 149771ms (file: 104-130-314-134-103-314-145-134-413-143-314-134-103-314-103 435-451-450-453-103-104-301-103-103-104-107-143-104-134-143-143-143-145-103-143-134-147-143-134-143-104-143 473-134-1_V.mp3)
  Segment 50: 150771ms ~ 151347ms (file: 263-123-126-132-213-123-126-132 542-125-617-637-602-637_V.mp3)
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
> **Time**: 2026-04-05 05:51:54
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
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
"transcript": "외신에 따르면",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "외신에", "start_ms": 110, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "따르면", "start_ms": 360, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "이 미사일의 목표물 요격률은 놀랍게도 무려 96%에 달했다고 합니다 이 한 번의 실전으로 한국의 방공 미사일은 단숨에 신급 평가를 받게 되었습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "미사일의", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "목표물", "start_ms": 550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "요격률은", "start_ms": 900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "놀랍게도", "start_ms": 1350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "무려", "start_ms": 1800, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "96%에", "start_ms": 2050, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "달했다고", "start_ms": 2650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "합니다", "start_ms": 3050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 3400, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 3500, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "번의", "start_ms": 3650, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "실전으로", "start_ms": 3850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "한국의", "start_ms": 4300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "방공", "start_ms": 4650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "미사일은", "start_ms": 4950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "단숨에", "start_ms": 5400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "신급", "start_ms": 5750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "평가를", "start_ms": 6100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "받게", "start_ms": 6450, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "되었습니다", "start_ms": 6650, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "더 치명적인 점은 가격이었습니다 천궁 투의 도입 비용과 운영 비용은",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "더", "start_ms": 80, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "치명적인", "start_ms": 230, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "점은", "start_ms": 630, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "가격이었습니다", "start_ms": 880, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "천궁", "start_ms": 1530, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "투의", "start_ms": 1830, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "도입", "start_ms": 2080, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "비용과", "start_ms": 2380, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "운영", "start_ms": 2730, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "비용은", "start_ms": 2980, "dur_ms": 350, "phase_lr_deg": 0.0}
]
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
"transcript": "미국 패트리어트 미사일의 3분의 1에",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "미국", "start_ms": 40, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "패트리어트", "start_ms": 290, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "미사일의", "start_ms": 690, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "3분의", "start_ms": 1090, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "1에", "start_ms": 1390, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "제품 그 사용했을 때 그 주름 개선 뭐 한 9% 가까이 그리고 또 미백 개선이 한 11% 그다음에 안면 리프팅",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "제품", "start_ms": 120, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 420, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "사용했을", "start_ms": 520, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 970, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 1120, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "주름", "start_ms": 1220, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "개선", "start_ms": 1520, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 1820, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 1970, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "9% 가까이", "start_ms": 2120, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 3120, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 3420, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "미백", "start_ms": 3570, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "개선이", "start_ms": 3870, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 4220, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "11%", "start_ms": 4370, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "그다음에", "start_ms": 5270, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "안면", "start_ms": 5670, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "리프팅", "start_ms": 5970, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "하셔야 할 것 같아 근데 또 제가 어떤 아이디어를 가지고 왔을 때 만약에 선배님",
"speaker": "speaker_3",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "하셔야", "start_ms": 60, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 410, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 560, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "같아", "start_ms": 660, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 1100, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 1350, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 1500, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 1700, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "아이디어를", "start_ms": 1950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "가지고", "start_ms": 2400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "왔을", "start_ms": 2700, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 2950, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "만약에", "start_ms": 3200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "선배님", "start_ms": 3550, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "님들은 무슨 생각 들어요 빨리 집에 가고 싶어요 하루 종일 기름 냄새",
"speaker": "speaker_4",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "님들은", "start_ms": 50, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "무슨", "start_ms": 400, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "생각", "start_ms": 600, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "들어요", "start_ms": 850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "빨리", "start_ms": 1400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "집에", "start_ms": 1700, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "가고", "start_ms": 1950, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "싶어요", "start_ms": 2200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "하루", "start_ms": 2700, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "종일", "start_ms": 2950, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "기름", "start_ms": 3200, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "냄새", "start_ms": 3450, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "있고 또 하나는 아까 발굴되었을 때 그 진흙과 물속에 있었다고 했잖아요 그게 산소와 차단된 밀폐 상태를 네 만들어서로 보고 있습니다",
"speaker": "speaker_5",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "있고", "start_ms": 90, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 340, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "하나는", "start_ms": 490, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "아까", "start_ms": 840, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "발굴되었을", "start_ms": 1090, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 1590, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 1740, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "진흙과", "start_ms": 1840, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "물속에", "start_ms": 2190, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있었다고", "start_ms": 2540, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "했잖아요", "start_ms": 2940, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "그게", "start_ms": 3440, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "산소와", "start_ms": 3690, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "차단된", "start_ms": 4040, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "밀폐", "start_ms": 4390, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "상태를", "start_ms": 4640, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "네", "start_ms": 5090, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "만들어서로", "start_ms": 5240, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "보고", "start_ms": 5740, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 5990, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "설거지 내면을 되게 싫어하거든요 이 계수대가 있고 조리하는 공간은 늘 냄새가 배어 있기 마련이고",
"speaker": "speaker_6",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "설거지", "start_ms": 110, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "내면을", "start_ms": 460, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "되게", "start_ms": 810, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "싫어하거든요", "start_ms": 1060, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 1860, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "계수대가", "start_ms": 1960, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "있고", "start_ms": 2410, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "조리하는", "start_ms": 2660, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "공간은", "start_ms": 3060, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "늘", "start_ms": 3410, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "냄새가", "start_ms": 3560, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "배어", "start_ms": 3910, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "있기", "start_ms": 4160, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "마련이고", "start_ms": 4410, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "그래서 고집스럽게 밀어붙인",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "그래서", "start_ms": 50, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "고집스럽게", "start_ms": 350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "밀어붙인", "start_ms": 850, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "알리가 되게 한국말 잘 하네요 잘해요 한국말 빨리 한국말 어디서 배우셨어요",
"speaker": "speaker_7",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "알리가", "start_ms": 70, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "되게", "start_ms": 420, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "한국말", "start_ms": 670, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 1020, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "하네요", "start_ms": 1170, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "잘해요", "start_ms": 1670, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "한국말", "start_ms": 2020, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "빨리", "start_ms": 2370, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "한국말", "start_ms": 2620, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "어디서", "start_ms": 2970, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "배우셨어요", "start_ms": 3270, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "퀘이크가 가장 효율적으로 작동하는 반응이라고 볼 수 있죠 수소폭탄도 반응 효율이 1%가 채 안 되는데 물질과 반물질",
"speaker": "speaker_8",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "퀘이크가", "start_ms": 120, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 570, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "효율적으로", "start_ms": 820, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "작동하는", "start_ms": 1320, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "반응이라고", "start_ms": 1720, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "볼", "start_ms": 2220, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 2370, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있죠", "start_ms": 2470, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "수소폭탄도", "start_ms": 2870, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "반응", "start_ms": 3420, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "효율이", "start_ms": 3670, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "1%가", "start_ms": 4020, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "채", "start_ms": 4370, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 4520, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "되는데", "start_ms": 4670, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "물질과", "start_ms": 5220, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "반물질", "start_ms": 5570, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "본다 그리고 에이전트는 실행한다라는 오픈 AI의 말이 있습니다 개발자는 비개발자는 AI 시대에 우리의 역할은",
"speaker": "speaker_9",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "본다", "start_ms": 100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "에이전트는", "start_ms": 800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "실행한다라는", "start_ms": 1250, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "오픈", "start_ms": 1850, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "AI의", "start_ms": 2100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "말이", "start_ms": 2400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 2650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "개발자는", "start_ms": 3250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "비개발자는", "start_ms": 3700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "AI", "start_ms": 4200, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "시대에", "start_ms": 4400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "우리의", "start_ms": 4750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "역할은", "start_ms": 5050, "dur_ms": 400, "phase_lr_deg": 0.0}
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
"transcript": "플래그들을 켜게 되는데 그냥 그 플래그들 이 지금 지역이 어떻냐 차량 구성이 어떻냐 운전자 상태가 어떻냐 이런 것들을 그런 플래그들을 바꾸면서 했을 때의 어떤 내부의 어떤 캔이라든가 플렉스레이라든가 이런 메시지",
"speaker": "speaker_10",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "플래그들을", "start_ms": 140, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "켜게", "start_ms": 640, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "되는데", "start_ms": 890, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 1390, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 1640, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "플래그들", "start_ms": 1740, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 2190, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 2290, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "지역이", "start_ms": 2540, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "어떻냐", "start_ms": 2840, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "차량", "start_ms": 3240, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "구성이", "start_ms": 3490, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "어떻냐", "start_ms": 3840, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "운전자", "start_ms": 4240, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "상태가", "start_ms": 4590, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "어떻냐", "start_ms": 4940, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 5340, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "것들을", "start_ms": 5590, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "그런", "start_ms": 5990, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "플래그들을", "start_ms": 6240, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "바꾸면서", "start_ms": 6740, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "했을", "start_ms": 7190, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "때의", "start_ms": 7440, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 7740, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "내부의", "start_ms": 7990, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 8340, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "캔이라든가", "start_ms": 8590, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "플렉스레이라든가", "start_ms": 9090, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 9790, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "메시지", "start_ms": 10040, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 19,
"transcript": "가격이 꽤 비쌀 거로 봅니다 그것까지 납품하는지는 모르겠지만 어느 정도 ESS에 가까운 기술들이 들어갈 거로 봐요 테슬라 경우에는 팔콘 여러 가지 종류의 비행체들이",
"speaker": "speaker_11",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "가격이", "start_ms": 80, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "꽤", "start_ms": 430, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "비쌀", "start_ms": 580, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "거로", "start_ms": 880, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "봅니다", "start_ms": 1130, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그것까지", "start_ms": 1630, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "납품하는지는", "start_ms": 2080, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "모르겠지만", "start_ms": 2680, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "어느", "start_ms": 3380, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "정도", "start_ms": 3630, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "ESS에", "start_ms": 3880, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "가까운", "start_ms": 4280, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "기술들이", "start_ms": 4630, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "들어갈", "start_ms": 5080, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "거로", "start_ms": 5430, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "봐요", "start_ms": 5680, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "테슬라", "start_ms": 6180, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "경우에는", "start_ms": 6530, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "팔콘", "start_ms": 6930, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "여러", "start_ms": 7330, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "가지", "start_ms": 7580, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "종류의", "start_ms": 7780, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "비행체들이", "start_ms": 8130, "dur_ms": 550, "phase_lr_deg": 0.0}
]
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
"transcript": "마지막 운동의 하이라이트 물 배양 이고 달리기 물을 흘리지 않고 잘 전달한 팀이 이기는 경기인데요 왠지 아슬아슬 조마조마합니다",
"speaker": "speaker_12",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "마지막", "start_ms": 110, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "운동의", "start_ms": 460, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "하이라이트", "start_ms": 760, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "물", "start_ms": 1310, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "배양", "start_ms": 1460, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이고", "start_ms": 1760, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "달리기", "start_ms": 2010, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "물을", "start_ms": 2610, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "흘리지", "start_ms": 2910, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "않고", "start_ms": 3260, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 3510, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "전달한", "start_ms": 3660, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "팀이", "start_ms": 4060, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "이기는", "start_ms": 4310, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "경기인데요", "start_ms": 4660, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "왠지", "start_ms": 5310, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "아슬아슬", "start_ms": 5610, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "조마조마합니다", "start_ms": 6060, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "대박 와 너무 예쁜데요 이거 두 번째 선수 이 이 게 술 맞겠죠 와 와 와 맞아요 우후 야 이거 다 마시면 사람 죽어",
"speaker": "speaker_13",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "대박", "start_ms": 90, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "와", "start_ms": 540, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 690, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "예쁜데요", "start_ms": 940, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이거", "start_ms": 1440, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "두", "start_ms": 1840, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "번째", "start_ms": 1940, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "선수", "start_ms": 2190, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 2540, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 2640, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 2740, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "술", "start_ms": 2840, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "맞겠죠", "start_ms": 2990, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "와", "start_ms": 3440, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "와", "start_ms": 3740, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "와", "start_ms": 4040, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "맞아요", "start_ms": 4340, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "우후", "start_ms": 4840, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "야", "start_ms": 5240, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이거", "start_ms": 5390, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 5690, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "마시면", "start_ms": 5840, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "사람", "start_ms": 6190, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "죽어", "start_ms": 6440, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 24,
"transcript": "안 된다면 어떻게 될까요 현재 물리학의 표준 모형이 흔들릴 수 있다는 징후가 많이 나오고 있습니다 또 빅뱅 직후에 왜 물질이 반물질보다 조금 더 많이 남았는지 그 수십억 년의 미스터리를 풀 실마리가 생길 수도 있겠죠 왜 우주가 반물질로",
"speaker": "speaker_8",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "안", "start_ms": 130, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "된다면", "start_ms": 280, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 730, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "될까요", "start_ms": 1080, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "현재", "start_ms": 1580, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "물리학의", "start_ms": 1880, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "표준", "start_ms": 2280, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "모형이", "start_ms": 2580, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "흔들릴", "start_ms": 2930, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 3280, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있다는", "start_ms": 3380, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "징후가", "start_ms": 3680, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 4030, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "나오고", "start_ms": 4280, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 4630, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 5230, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "빅뱅", "start_ms": 5380, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "직후에", "start_ms": 5730, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "왜", "start_ms": 6230, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "물질이", "start_ms": 6380, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "반물질보다", "start_ms": 6730, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 7330, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 7580, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 7730, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "남았는지", "start_ms": 7980, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 8530, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "수십억", "start_ms": 8630, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "년의", "start_ms": 8980, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "미스터리를", "start_ms": 9180, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "풀", "start_ms": 9680, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "실마리가", "start_ms": 9830, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "생길", "start_ms": 10280, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "수도", "start_ms": 10580, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "있겠죠", "start_ms": 10830, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "왜", "start_ms": 11330, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "우주가", "start_ms": 11480, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "반물질로", "start_ms": 11830, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "트럼프 대통령은 이란의 새로운 정권이 휴전을 요청했다고 주장했지만 이란은 트럼프의 발표는 거짓이라고 반박한 바 있습니다 이란이 장기전에 대비",
"speaker": "speaker_14",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "트럼프", "start_ms": 60, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "대통령은", "start_ms": 410, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이란의", "start_ms": 960, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "새로운", "start_ms": 1260, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "정권이", "start_ms": 1610, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "휴전을", "start_ms": 1960, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "요청했다고", "start_ms": 2310, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "주장했지만", "start_ms": 2810, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "이란은", "start_ms": 3460, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "트럼프의", "start_ms": 3810, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "발표는", "start_ms": 4210, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "거짓이라고", "start_ms": 4560, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "반박한", "start_ms": 5060, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "바", "start_ms": 5410, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 5510, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이란이", "start_ms": 6110, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "장기전에", "start_ms": 6460, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "대비", "start_ms": 6910, "dur_ms": 300, "phase_lr_deg": 0.0}
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
"transcript": "천궁이랑 또 추가적으로 천광도 있어요 천광을 레이저로 이제 드론을 요격하는 거예요 천궁이나 요런 것들은 이제 드론을 요격하기 어렵거든요 근데 천광은 굉장히 비용을 한 발 쏘는데 레이저 한 발 쏘는데 몇 천 원",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "천궁이랑", "start_ms": 110, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 560, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "추가적으로", "start_ms": 710, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "천광도", "start_ms": 1210, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 1560, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "천광을", "start_ms": 2010, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "레이저로", "start_ms": 2360, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 2760, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "드론을", "start_ms": 2960, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "요격하는", "start_ms": 3310, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 3710, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "천궁이나", "start_ms": 4210, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "요런", "start_ms": 4660, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "것들은", "start_ms": 4910, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 5260, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "드론을", "start_ms": 5460, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "요격하기", "start_ms": 5810, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "어렵거든요", "start_ms": 6260, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 6960, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "천광은", "start_ms": 7210, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "굉장히", "start_ms": 7560, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "비용을", "start_ms": 7860, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 8210, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "발", "start_ms": 8360, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "쏘는데", "start_ms": 8510, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "레이저", "start_ms": 9010, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 9360, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "발", "start_ms": 9510, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "쏘는데", "start_ms": 9660, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "몇", "start_ms": 10160, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "천", "start_ms": 10310, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "원", "start_ms": 10460, "dur_ms": 150, "phase_lr_deg": 0.0}
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
"transcript": "처럼 노하의 매매는 정말 단순하면서도 강력합니다 리듬이 만들어지고 거기에 반응만 하면 되니까요 하지만 이 전략에도 치명적인 단점이 있습니다",
"speaker": "speaker_9",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "처럼", "start_ms": 70, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "노하의", "start_ms": 320, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "매매는", "start_ms": 670, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 1020, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "단순하면서도", "start_ms": 1270, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "강력합니다", "start_ms": 1870, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "리듬이", "start_ms": 2520, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "만들어지고", "start_ms": 2870, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "거기에", "start_ms": 3370, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "반응만", "start_ms": 3670, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "하면", "start_ms": 4020, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "되니까요", "start_ms": 4220, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "하지만", "start_ms": 4770, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 5120, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "전략에도", "start_ms": 5220, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "치명적인", "start_ms": 5670, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "단점이", "start_ms": 6120, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 6470, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "A thriller full of action a thriller geo-political You are going to follow the story of North Korean and South Korean secret agents investigating human trafficking counter-espionage manipulation betrayal and all of that with a lot of action",
"speaker": "speaker_15",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "A", "start_ms": 120, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "thriller", "start_ms": 220, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "full", "start_ms": 570, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "of", "start_ms": 820, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "action", "start_ms": 970, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 1370, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "thriller", "start_ms": 1470, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "geo-political", "start_ms": 1820, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "You", "start_ms": 2570, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "are", "start_ms": 2770, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "going", "start_ms": 2920, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 3170, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "follow", "start_ms": 3320, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 3670, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "story", "start_ms": 3820, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "of", "start_ms": 4170, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "North", "start_ms": 4320, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "Korean", "start_ms": 4620, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "and", "start_ms": 4970, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "South", "start_ms": 5120, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "Korean", "start_ms": 5420, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "secret", "start_ms": 5770, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "agents", "start_ms": 6120, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "investigating", "start_ms": 6470, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "human", "start_ms": 7070, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "trafficking", "start_ms": 7370, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "counter-espionage", "start_ms": 8020, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "manipulation", "start_ms": 8820, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "betrayal", "start_ms": 9470, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "and", "start_ms": 9970, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "all", "start_ms": 10120, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "of", "start_ms": 10320, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "that", "start_ms": 10420, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "with", "start_ms": 10770, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 10920, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "lot", "start_ms": 11020, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "of", "start_ms": 11220, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "action", "start_ms": 11320, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "미국 가정에서 빠르게 입소문이 나기 시작했는데요 점점 더 많은 미국 가정이 세탁기를 지하실에서 일상 공간에 배치하기 시작",
"speaker": "speaker_16",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "미국", "start_ms": 90, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "가정에서", "start_ms": 340, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "빠르게", "start_ms": 740, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "입소문이", "start_ms": 1090, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "나기", "start_ms": 1490, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "시작했는데요", "start_ms": 1740, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "점점", "start_ms": 2490, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 2790, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "많은", "start_ms": 2940, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "미국", "start_ms": 3190, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "가정이", "start_ms": 3440, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "세탁기를", "start_ms": 3790, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "지하실에서", "start_ms": 4240, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "일상", "start_ms": 4740, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "공간에", "start_ms": 4990, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "배치하기", "start_ms": 5340, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "시작", "start_ms": 5790, "dur_ms": 300, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 05:51:54*
