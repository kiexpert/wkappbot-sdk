# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=17628  time=06:27:50  uptime=16483s  mem=326MB  system_processes=353"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_062717.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 517-570-157-175-173-153-157-173-175-175-173-513-175-107-175-173-107-137-175-710-570_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 517-517-571-157-517-571-517-571-571-517-517-571-517 153-135-153-517-130-150-157-153-175-157-157-157-157-157-157-517-157-517-571-571-571-571-571-571-517-571-751-571-751-571-571-7_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 316-132 163-260-165-136-132-127-176 621-167-261-362-612-671 213-132_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 135-175-761-175-176 132-172-316-321-321-132-356-361-136-501-206-571-571-573-576-573-167-651-301-136-132-103-160-130-136-103-326-316-136-637-136-136-103-106-163-103-136-163-312-1_W.mp3)
  Segment 5: 6304ms ~ 11416ms (file: 712-175-104-576-231-120-256-321-120-206-210-756-276-237-627-257-765-752-726-765-357-752-756-352-750-574-215-756-317-751-723-207-270-756-726-201-752-752-107-276-715-756-206-725-5_W.mp3)
  Segment 6: 12416ms ~ 20948ms (file: 751-123-261-120-231-123-574-526-627-231-261-751-547-263-765-132-106-471-256-236-542-216-267-231-623-102-316-263-261-621-754-754-130-213-267-216-512-231-261-263-236-213-754-261-2_W.mp3)
  Segment 7: 21948ms ~ 27852ms (file: 675-605-627-576-206-574-237-762-672-260-210-231-301-712-103-132-230-276-216-317-231-237-671-310-632-267-261-160-653-275-230-765-765-207-321-132-715-276-267-263-170-203-573-320-3_W.mp3)
  Segment 8: 28852ms ~ 29428ms (file: 176-176-715-176-175-175-170-170-176-761-176 105-105-105-105-105-105-150-105-105-170-170-175-176-170-176-175-170-107-175 105-150-105-105-103-105-105-157-105-175-157-157-561-165-1_V.mp3)
  Segment 9: 30428ms ~ 31004ms (file: 612-623-310-301-301-321-132-603-621-163-132-312-132-301-310-136-312-326-326-236 136-750 627-216 754-756-576 576-576-576-576-567-576-756-567-576-756-765-675-675-675-675-675-670-1_W.mp3)
  Segment 10: 32004ms ~ 42192ms (file: 106-710-671-762-137-370-754-106-130-753-170-132-103-317-673-237-731-547-103-756-103-712-310-237-574-754-275-754-715-720-756-756-756-103-137-103-603-571-531-571-301-603-361-307-7_W.mp3)
  Segment 11: 43192ms ~ 55144ms (file: 230-143-176-301-231-267-230-326-735-237-201-106-612-126-320-326-124-261-761-162-106-321-102-126-613-106-167-163-261-106-102-123-263-167-265-261-264-236-106-267-267-201-263-276-2_W.mp3)
  Segment 12: 56144ms ~ 59996ms (file: 316-130-136-162-326-162-321-672-276-216-321-126-136-130-301-201-102-130-136-132-312-216-320-123-136-621-132-126-267-162-621-162-261-126-162-216-263-312-162-130-123-136-627-216-1_W.mp3)
  Segment 13: 60996ms ~ 74352ms (file: 120-275-206-320-231-236-263-263-236-263-672-267-106-206-267-107-120-174-236-263-207-765-276-123-102-267-750-237-236-216-210-267-237-726-367-362-327-321-327-102-203-163-123-136-3_W.mp3)
  Segment 14: 75352ms ~ 75928ms (file: 620-627-263-367-673-367-673-167-317_W.mp3)
  Segment 15: 76928ms ~ 77504ms (file: 570-126-136-316-136-136-136-135-136-135-163-162-176-162-137-176-712-756-576-570-756-756-675-675-765-756-756-756-756-675-675-765-176-176-175-716-176-176-173 107-173-170-137-750 1_W.mp3)
  Segment 16: 78504ms ~ 86280ms (file: 132-321-231-312-627-231-132-130-321-261-321-263-216-102-123-231-123-102-102-213-123-256-615-165-312-130-216-260-216-260-213-312-312-623-326-321-231-257-216-123-130-130-312-312-3_W.mp3)
  Segment 17: 87280ms ~ 87856ms (file: 136-627-372-621-231-162-316-132-163-627-621-367-627-276-263_W.mp3)
  Segment 18: 88856ms ~ 89432ms (file: 756-213-756-375-752-372-312-321-536-570-735-602-352-623-312-762-621-263-526-657-756-523-273-275-275-217-312-576-326-735-751-756-731-153-751-257-237-502-217-167-574-275-753-237-2_V.mp3)
  Segment 19: 90432ms ~ 91008ms (file: 507-501-105-507-501-105-510-507-510-570-570-510-150-510-501-507 150-105-105-157-105-105-517-150-105-157-157-501-517-510-570-150-103-105-501-510-150-105-501-157-170-510-150-105-5_V.mp3)
  Segment 20: 92008ms ~ 92584ms (file: 174-576-537-132-631-163-136-163 312-103-136_V.mp3)
  Segment 21: 93584ms ~ 94160ms (file: 312-312-675-321-123-132-312-102-265-265-203-321-312-132-762-276-206-237-216-132-675-670-671-367-126-756-756-672-637-672-625-651-316-312-762-726-672-672-726-726-762-760-617-602-1_W.mp3)
  Segment 22: 95160ms ~ 95736ms (file: 750-751-705-750-750-705-750-751-750-751-170-750-105-710-175-175-501-570-517-170-710-170-107-750-175-710-750-570 570-570-750-751-570-750-751-571-507-570-157-571-701-751-715-105-1_V.mp3)
  Segment 23: 96736ms ~ 97312ms (file: 107-153-157-137 570-571-517-157-137 136-316-130-135-315-135-175-570-517-517-715-150-130-132-132-315-317-753-765-375-107-173-135-153-175-163-136-753-756-756-576-573-573-316-137-3_V.mp3)
  Segment 24: 98312ms ~ 98888ms (file: 135-135 547-754-574-167-576-576-576-716_W.mp3)
  Segment 25: 99888ms ~ 100464ms (file: 107-107-170-107-751-150-107-517-710-750-715-170-517-715 574-574-570-571-571-571-570-571-570-570-570-150-571-510-501-570-750-105-570-157-571-574-574-574-571_W.mp3)
  Segment 26: 101464ms ~ 102040ms (file: 263-574-574-357-731-675-620-213-132-120-123-123-150-657-657-162-216-265-671-265-567-657-321-236-267-625-654-650-236-326-236-265-321-765-627-267-526-564-540-675-547-574-756-576-5_W.mp3)
  Segment 27: 103040ms ~ 103616ms (file: 751-715-756-715-132-103-103-136-173-173-172-172-173-726-752-573-157-157-157-157-157-157-157-517-157-715-517-157-571-571-517-571-567-157-651-216-621-625-265-615-652-625-105-165-1_W.mp3)
  Segment 28: 104616ms ~ 105192ms (file: 517-567-765-615-176-361-367-573-563-675-375-167-716-537-365-315-615-136-615-135-365-361-671-156-631-167_W.mp3)
  Segment 29: 106192ms ~ 106768ms (file: 763-736-762-753-751-765-765-571-537-715-756-756-761-735-753-751-753-715-751-751-715-756-756-576-756-753-756-756-756-756-756-756-756 267-263-261-267-265-267-265-625-627-612-612 6_W.mp3)
  Segment 30: 107768ms ~ 108344ms (file: 130-135-312-203-302-132-260-260-260-260-203-567-623-263-261-213-132-310-312-312-103-126-103-103-235-132-103-103-236-263-236-263-260-612-620-206-206-720-126-123-261-312-132-312-1_W.mp3)
  Segment 31: 109344ms ~ 109920ms (file: 620-637-357-301-361-103-163-136-136-673-623-301-316-231-263-132-312-631-601-716-657-167-106-301-623-261-623-362-657-613-601-637-706-670-671-671-136-163-612-167-163-325-162-613-1_V.mp3)
  Segment 32: 110920ms ~ 111496ms (file: 574-576-576-576-574-547 574-547-574-547-574-574-574-570-574-574-574-576-574-576-574-576-756-574-574-574-576 574-574-574-574-570-574-754-754-754-753-750-754-754-754-754-512 576-5_W.mp3)
  Segment 33: 112496ms ~ 120596ms (file: 750-576-162-301-203-560-267-237-372-102-573-756-754-132-751-507-203-123-570-130-754-571-107-103-672-675-672-267-267-620-627-627-627-270-752-230-231-106-327-762-326-362-716-123-1_W.mp3)
  Segment 34: 121596ms ~ 132072ms (file: 651-136-310-130-362-302-620-306-362-316-310-132-301-102-361-106-631-613-130-310-130-163-163-106-574-137-574-574-574-130-310-302-123-572-752-573-135-657-574-157-135 130-130-213-3_V.mp3)
  Segment 35: 133072ms ~ 133648ms (file: 173-316-357-130-567-670-627-130-750-576-371-173-312-216-175-157-752-123-216-765-152-126-516-175-756-153-751 562-564-257-312-163-326-103-132-132-215-132-265-362-321-326-672-675-3_V.mp3)
  Segment 36: 134648ms ~ 135224ms (file: 513-105-150-150-105-150-130 510-150-150-105-501-517-107-105-153-157-105-517-510-150-105-157-105-150-170-105-150-150-501-157-517-501-501-517-510-150-501 105-150-105-105-105-153-1_V.mp3)
  Segment 37: 136224ms ~ 145584ms (file: 123-312-320-260-326-301-103-260-203-206-572-627-627-107-317-103-130-672-627-263-267-627-602-174-260-123-103-132-216-260-203-261-216-123-231-627-267-130-302-312-130-103-310-627-2_W.mp3)
  Segment 38: 146584ms ~ 147160ms (file: 351-653-625-567-567-351-623-312-231-130_W.mp3)
  Segment 39: 148160ms ~ 148736ms (file: 570-571-517-507-570-571 510-571-510-517-517-513-150-105-153-157-517-150-501-507-517-501-501-501-157-510-507-510-501-507-501-507-501-510-507-501-570-570 507-517-510-105-501-510-5_W.mp3)
  Segment 40: 149736ms ~ 150312ms (file: 753-753-571 315-314-315-135-143-103-301-134-136-137-314-135-153-135-103-134-135-103-103-135-103-135-105-103-153-135 357-573-573-753-751-573-751 316-314-341-301-341-314-134-301-1_V.mp3)
  Segment 41: 151312ms ~ 151888ms (file: 137-137-134-103-103-103-136-167-173-137_V.mp3)
  Segment 42: 152888ms ~ 153464ms (file: 723-261-572-236-570-267-276-752-637-572-235-571-267-265-507-576-135-265-162-126-126-216-263-261-527-521-153-157-261-621-267-720-162-617-563-126-527 267-726-726-325-560-256-152-2_V.mp3)
  Segment 43: 154464ms ~ 155040ms (file: 516-736-765-736-765-261-752-257-321-215-567-136-301-312-562-627-213-326-352-675-763-527-367-627-321-675-632-321-702-256-136-502-756-217-312-751-301-312-302-312-756-536-562-675-5_V.mp3)
  Segment 44: 156040ms ~ 156616ms (file: 572-571-527-572-576-573-562-576-572-521-527-572-572-752-175-167-170-710-107-167-176-176-716-176-765-716-716-765-673-712-372-752-527-571-532-753-537-275-572-725-752-173-137-710-1_W.mp3)
  Segment 45: 157616ms ~ 158192ms (file: 751-750 574-570-570-751-701-751-750-751-517-750-705-570-105-751-570-507-170-107-750-150-571-751-570-751-175-571-751-750-570-571-750-751_W.mp3)
  Segment 46: 159192ms ~ 159768ms (file: 507-750-570-576-570-570-570-570-570-570-576-576 574-570-570-751-507-570-510-507-105-570-751-570-510-750-570-157-750-750-570-750-570-751-571 751-751-750-705-150-507-507-751-570-5_V.mp3)
  Segment 47: 160768ms ~ 172000ms (file: 217-206-670-607-120-132-206-620-721-206-620-236-260-305-237-126-134-216-207-316-312-310-103-123-367-267-132-230-260-103-124-210-623-123-756-260-316-137-213-132-102-130-310-123-2_W.mp3)
  Segment 48: 173000ms ~ 182288ms (file: 623-261-756-273-276-273-326-326-653-537-536-567-327-576-532-237-236-765-713-756-573-756-261-263-236-237-102-126-132-231-123-321-231-320-302-326-623-362-732-132-123-213-326-167-6_W.mp3)
  Segment 49: 183288ms ~ 186816ms (file: 321-236-276-623-726-276-276-276-267-312-236-276-126-723-263-723-216-267-263-276-276-271-762-321-267-237-267-210-126-312-623-267-672-236-263-267-273-267-267-216-236-276-321-217-7_W.mp3)
  Segment 50: 187816ms ~ 195951ms (file: 265-752-765-765-206-765-672-756-576-103-132-576-231-123-102-231-302-175-716-167-137-263-267-236-236-120-107-326-236-260-261-735-321-265-527-102-126-102-130-102-765-123-162-106-1_W.mp3)
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
> **Time**: 2026-04-05 06:29:14
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
"transcript": "작게 하다가 조금씩 늘려나가는 식으로 매매를 하고 있습니다",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 132,
"audio_type": "speech",
"words": [
{"word": "작게", "start_ms": 132, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "하다가", "start_ms": 650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "조금씩", "start_ms": 1200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "늘려나가는", "start_ms": 1850, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "식으로", "start_ms": 3000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "매매를", "start_ms": 3700, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "하고", "start_ms": 4400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 4900, "dur_ms": 900, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "과거에 내가 최근 매매 내역들을 보면서 시장 분위기를 한 번 더",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 276,
"audio_type": "speech",
"words": [
{"word": "과거에", "start_ms": 276, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "내가", "start_ms": 900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "최근", "start_ms": 1400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "매매", "start_ms": 2000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "내역들을", "start_ms": 2500, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "보면서", "start_ms": 3400, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "시장", "start_ms": 4100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "분위기를", "start_ms": 4650, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 5500, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "번", "start_ms": 5800, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 6100, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "그렇죠 만약에 지수 상황은 별로 안 좋은데 예를 들어 막 테마",
"speaker": "speaker_2",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 136,
"audio_type": "speech",
"words": [
{"word": "그렇죠", "start_ms": 136, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "만약에", "start_ms": 850, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "지수", "start_ms": 1600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "상황은", "start_ms": 2100, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "별로", "start_ms": 2900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 3450, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "좋은데", "start_ms": 3750, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "예를", "start_ms": 4500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "들어", "start_ms": 4950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "막", "start_ms": 5500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "테마", "start_ms": 5900, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "볼펜으로 이거 쓰고 있어요 또 독일 사람이다 보니까 한자를 진짜 좋아해요",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 126,
"audio_type": "speech",
"words": [
{"word": "볼펜으로", "start_ms": 126, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "이거", "start_ms": 950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "쓰고", "start_ms": 1400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 1900, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 2600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "독일", "start_ms": 3000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "사람이다", "start_ms": 3550, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "보니까", "start_ms": 4400, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "한자를", "start_ms": 5100, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 5800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "좋아해요", "start_ms": 6300, "dur_ms": 800, "phase_lr_deg": 0.0}
]
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
"transcript": "The correct one was my second guess. Yes! Okay, I got that one right. Whew! All right. Yeah, that was easy, I got that right. Okay, so I got four out of five.",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 123,
"audio_type": "speech",
"words": [
{"word": "The", "start_ms": 123, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "correct", "start_ms": 300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "one", "start_ms": 750, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "was", "start_ms": 1000, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "my", "start_ms": 1300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "second", "start_ms": 1500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "guess", "start_ms": 2000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "Yes", "start_ms": 2600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "Okay", "start_ms": 3100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 3500, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "got", "start_ms": 3650, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "that", "start_ms": 3900, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "one", "start_ms": 4150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "right", "start_ms": 4400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "Whew", "start_ms": 4900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "All", "start_ms": 5400, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "right", "start_ms": 5650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "Yeah", "start_ms": 6100, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "that", "start_ms": 6400, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "was", "start_ms": 6650, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "easy", "start_ms": 6900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 7400, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "got", "start_ms": 7550, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "that", "start_ms": 7800, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "right", "start_ms": 8050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "Okay", "start_ms": 8500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "so", "start_ms": 8900, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 9150, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "got", "start_ms": 9300, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "four", "start_ms": 9550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "out", "start_ms": 9900, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "of", "start_ms": 10150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "five", "start_ms": 10350, "dur_ms": 400, "phase_lr_deg": 0.0}
]
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
"transcript": "맛있게 드세요 모두 모두 수고하셨습니다 감사합니다",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 257,
"audio_type": "speech",
"words": [
{"word": "맛있게", "start_ms": 257, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "드세요", "start_ms": 1000, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "모두", "start_ms": 1800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "모두", "start_ms": 2300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "수고하셨습니다", "start_ms": 2800, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 4100, "dur_ms": 1000, "phase_lr_deg": 0.0}
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
"transcript": "수해! 와! 좋아! 내가 보려고 만든 거야",
"speaker": "speaker_6",
"confidence": 0.88,
"language": "ko",
"silence_before_ms": 267,
"audio_type": "speech",
"words": [
{"word": "수해", "start_ms": 267, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "와", "start_ms": 1200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "좋아", "start_ms": 1800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "내가", "start_ms": 2700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "보려고", "start_ms": 3300, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "만든", "start_ms": 4000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "거야", "start_ms": 4600, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "국제 시장에 존재하는 거대한 공백 지대를 정확히 붙잡았기 때문입니다 많은 나라들은 5세대 전투기를 살 돈이 없거나 혹은 다른 나라들이 아예 팔지 않기 때문에 구할 수 없습니다 하지만 이들 국가는 낡은 F-",
"speaker": "speaker_7",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 135,
"audio_type": "speech",
"words": [
{"word": "국제", "start_ms": 135, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "시장에", "start_ms": 600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "존재하는", "start_ms": 1150, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "거대한", "start_ms": 1900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "공백", "start_ms": 2500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "지대를", "start_ms": 3050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "정확히", "start_ms": 3650, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "붙잡았기", "start_ms": 4350, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "때문입니다", "start_ms": 5200, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "많은", "start_ms": 6200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "나라들은", "start_ms": 6700, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "5세대", "start_ms": 7450, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "전투기를", "start_ms": 8150, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "살", "start_ms": 8950, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "돈이", "start_ms": 9300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "없거나", "start_ms": 9800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "혹은", "start_ms": 10500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "다른", "start_ms": 11000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "나라들이", "start_ms": 11450, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "아예", "start_ms": 12200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "팔지", "start_ms": 12700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "않기", "start_ms": 13250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "때문에", "start_ms": 13750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "구할", "start_ms": 14350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 14850, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "없습니다", "start_ms": 15150, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "하지만", "start_ms": 16000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이들", "start_ms": 16600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "국가는", "start_ms": 17050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "낡은", "start_ms": 17650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "F-", "start_ms": 18150, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "여행하던 중 한국의 고즈넉한 매력에 끌려 벌써 두 번째 한국을 찾았다는 파트리샤",
"speaker": "speaker_8",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 132,
"audio_type": "speech",
"words": [
{"word": "여행하던", "start_ms": 132, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "중", "start_ms": 950, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "한국의", "start_ms": 1300, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "고즈넉한", "start_ms": 1950, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "매력에", "start_ms": 2800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "끌려", "start_ms": 3500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "벌써", "start_ms": 4100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "두", "start_ms": 4650, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "번째", "start_ms": 5000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "한국을", "start_ms": 5500, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "찾았다는", "start_ms": 6200, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "파트리샤", "start_ms": 7000, "dur_ms": 800, "phase_lr_deg": 0.0}
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
"transcript": "바레인의 아마존 클라우드 센터와 두바이 소재 오라클 데이터 센터도 공격했다고 주장했습니다 MBC 뉴스 송서영입니다",
"speaker": "speaker_9",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 312,
"audio_type": "speech",
"words": [
{"word": "바레인의", "start_ms": 312, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "아마존", "start_ms": 1000, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "클라우드", "start_ms": 1650, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "센터와", "start_ms": 2400, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "두바이", "start_ms": 3100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "소재", "start_ms": 3700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "오라클", "start_ms": 4250, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "데이터", "start_ms": 4900, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "센터도", "start_ms": 5550, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "공격했다고", "start_ms": 6250, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "주장했습니다", "start_ms": 7150, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "MBC", "start_ms": 8300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "뉴스", "start_ms": 8900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "송서영입니다", "start_ms": 9400, "dur_ms": 900, "phase_lr_deg": 0.0}
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
"transcript": "해수부는 선원들 안전과 보급 상황을 확인하고 마지막 한 명이 내릴 때까지 최선을 다하겠다고 밝혔습니다 MBC 뉴스 김민혁입니다",
"speaker": "speaker_10",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 106,
"audio_type": "speech",
"words": [
{"word": "해수부는", "start_ms": 106, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "선원들", "start_ms": 850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "안전과", "start_ms": 1450, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "보급", "start_ms": 2150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "상황을", "start_ms": 2700, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "확인하고", "start_ms": 3400, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "마지막", "start_ms": 4250, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 4950, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "명이", "start_ms": 5300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "내릴", "start_ms": 5750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "때까지", "start_ms": 6300, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "최선을", "start_ms": 7000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "다하겠다고", "start_ms": 7700, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "밝혔습니다", "start_ms": 8650, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "MBC", "start_ms": 9800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "뉴스", "start_ms": 10400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "김민혁입니다", "start_ms": 10900, "dur_ms": 900, "phase_lr_deg": 0.0}
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
"transcript": "지금 몇 명의 선원이 고립돼 있는 겁니까? 지금",
"speaker": "speaker_11",
"confidence": 0.89,
"language": "ko",
"silence_before_ms": 270,
"audio_type": "speech",
"words": [
{"word": "지금", "start_ms": 270, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "몇", "start_ms": 800, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "명의", "start_ms": 1150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "선원이", "start_ms": 1600, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "고립돼", "start_ms": 2250, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 2950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "겁니까?", "start_ms": 3450, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 8500, "dur_ms": 400, "phase_lr_deg": 0.0}
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
"transcript": "미국이 이란에 대한 총공세를 예고했습니다 그래서 이번 주 주말 또는 다음 주에 예고가 되어 있고요 기존에 예고했던 데드라인 있죠 데드라인 연기를 했죠 그게 4월 6일 월요일입니다 자 그렇기 때문에 그때까지",
"speaker": "speaker_12",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 267,
"audio_type": "speech",
"words": [
{"word": "미국이", "start_ms": 267, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이란에", "start_ms": 850, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "대한", "start_ms": 1500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "총공세를", "start_ms": 2050, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "예고했습니다", "start_ms": 2900, "dur_ms": 950, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 4000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이번", "start_ms": 4600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "주", "start_ms": 5100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "주말", "start_ms": 5400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "또는", "start_ms": 5950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 6450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "주에", "start_ms": 6950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "예고가", "start_ms": 7450, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "되어", "start_ms": 8100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있고요", "start_ms": 8550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "기존에", "start_ms": 9100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "예고했던", "start_ms": 9700, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "데드라인", "start_ms": 10450, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "있죠", "start_ms": 11250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "데드라인", "start_ms": 11700, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "연기를", "start_ms": 12500, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "했죠", "start_ms": 13150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "그게", "start_ms": 13650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "4월", "start_ms": 14100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "6일", "start_ms": 14600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "월요일입니다", "start_ms": 15100, "dur_ms": 950, "phase_lr_deg": 0.0},
{"word": "자", "start_ms": 16200, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "그렇기", "start_ms": 16550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "때문에", "start_ms": 17150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "그때까지", "start_ms": 17750, "dur_ms": 650, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
*Generated by WKAppBot ask gemini — 2026-04-05 06:29:14*
