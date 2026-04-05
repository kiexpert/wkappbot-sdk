# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=11716  time=12:39:37  uptime=39034s  mem=420MB  system_processes=355"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_123715.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 726-726-726-726-726-762-567-176-176-165-167-362-623-632-367-136-321-321_W.mp3)
  Segment 2: 1576ms ~ 8596ms (file: 230-230-321-102-230-301-132-230-230-601-263-230-236-260-265-102-572-256-206-265-106-102-106-257-521-203-752-237-302-235-256-203-253-230-257-236-106-265-627-206-236-102-263-263-2_W.mp3)
  Segment 3: 9596ms ~ 10172ms (file: 756-715 157-105-105-107-170-170-170-571-175-715-170-170-175-170-715-715-170-170-751-175-710-170 105-107-107-170-173-170-170-170-170-705-751-756-715-756-756-715-715-175-517 105-1_V.mp3)
  Segment 4: 11172ms ~ 11748ms (file: 715-513-715-157-750 517-571-157-517-571-157-175-175-715-715-137-103-132-713-710-715-170-715-713-175-175-715-170-731-175 157-571-517-571-135-157-157-157-175-751-137-173-175-175-1_V.mp3)
  Segment 5: 12748ms ~ 13324ms (file: 562-527-206-273-201-271-756-752-207-762-725-752-276-257-576-572 561-762-627-701-705-720-507-752-276-276-725-670-720-710-752-605-730-307-370-706-752-576 127-275-207-725-275-657-7_V.mp3)
  Segment 6: 14324ms ~ 14900ms (file: 631-316-361-307-763-104-146-413-106-102-160-160-306-574-573-567-567-761-765-210-132-123-362 136_W.mp3)
  Segment 7: 15900ms ~ 23136ms (file: 132-103-123-321-206-726-276-512-102-321-260-132-130-603-137-130-301-367-150-123-152-271-120-132-725-765-123-210-237-756-316-301-317-301-312-574-206-312-123-132-316-120-213-256-1_W.mp3)
  Segment 8: 24136ms ~ 24712ms (file: 567-576-576-576-756-756-715-504-543-512-521-504-574-534-516-561-561-567-543-574-561-516-506-567-506-527-756-576-756-576-576-756-576-756-756-712-213-507-501-504-501-503-501-506-5_W.mp3)
  Segment 9: 25712ms ~ 31796ms (file: 263-231-134-756-765-576-201-213-231-132-312-107-103-176-731-132-126-517-765-312-751-150-137-130-321-107-216-132-612-621-756-725-132-132-132-130-201-673-570-736-176-375-231-263-6_W.mp3)
  Segment 10: 32796ms ~ 33372ms (file: 150-105-105-130-135-153-513-517-513-153-517-157-153-571-517-175-571-537-375-315-137 105-107-175-130-315-135-150-105-135-150-613-137-135-315-135-153-153-153-157-357-513-153-517-1_V.mp3)
  Segment 11: 34372ms ~ 34948ms (file: 157-105-105-150-507-510-516-150-501-501-510-517-517-571-517-570-517-576-570-571-157-570-571-570-517-517-570 507-507-157-105-105-150-501-501-501-105-501-510-510-510-510-571-510-1_V.mp3)
  Segment 12: 35948ms ~ 45416ms (file: 137-175-753-173-137-671-206-127-130-357-302-312-175-517-135-231-623-361-263-231-210-756-672-627-627-627-627-267-123-210-106-263-267-576-206-230-236-231-236-326-231-320-132-321-2_W.mp3)
  Segment 13: 46416ms ~ 46992ms (file: 534-513-514-541-567-527 564-562-524-512-531-564 602-361-613-635-516-521-526-562-561-521-563-516-521-561-574-532-531-532-512-531-516-512-102-126-167-657-561-563-562-524-542-512-5_V.mp3)
  Segment 14: 47992ms ~ 48568ms (file: 126-102-312-103-135 126-231-671-126-267-265-203-572-520-526-257-265-216-217-620-627-261-621-261-213-236-413-154 721 135-136-312-160-163-173-621-613-103-650-165-612-601-316-671-1_V.mp3)
  Segment 15: 49568ms ~ 50144ms (file: 726-317-310-310-316-302-312-123-167-124-310-312-701-207-302-307-301-357-320-320-302-302-301-103-103-301-503-540-501-750-756-765-765-675-764-674-547-756-756-137-765-765-103-136-1_V.mp3)
  Segment 16: 51144ms ~ 51720ms (file: 715-571-571-571-751-751-751-751-175-751-715-175-175-751 510-150-715-715-157-715-751-170-751-571-157-175-175-175-751-157-175-175-157-175-175-751-756 570-571-517-157-517-175-750-7_W.mp3)
  Segment 17: 52720ms ~ 53296ms (file: 263 130-315-351-136-132-312-107-137-570-706-756-137-367-612-672-765-130-136-136-316-136-136-137-137-137-751-312-137-130-172-701-137-701-735-173-617-756-213-701-123-132-173-125-3_V.mp3)
  Segment 18: 54296ms ~ 54872ms (file: 675-756-765-673-765-675 527-256-625-642-645-624-645-642-624-624-624-624-642-621-624 625-624-624-621 213-216-213_W.mp3)
  Segment 19: 55872ms ~ 56448ms (file: 514-513-254-541-521-124-351-231-725-756-576-756-756-756-576-576-576-576-576-576-576-756-756-324-645-516-561-574-125-564-514-514-546-541-675-760-607-560-560-657-567-546-752-756-7_W.mp3)
  Segment 20: 57448ms ~ 73900ms (file: 670-675-627-310-103-574-310-103-104-103-135-301-103-310-301-132-103-203-102-321-132-103-153-120-132-310-103-103-130-573-756-105-103-321-310-132-301-132-201-230-301-103-103-106-1_V.mp3)
  Segment 21: 74900ms ~ 75476ms (file: 136-761 526-136-103-602-167-175-315-136-627-160-103-617-615-176-761-103-103-513-706-236-260-657-657-576-567-756-160-763_V.mp3)
  Segment 22: 76476ms ~ 86520ms (file: 102-276-123-270-273-752-276-276-130-236-261-236-231-136-103-276-231-102-275-236-236-263-231-326-136-136-267-130-267-321-326-130-267-236-236-231-123-237-136-176-765-675-765-756-7_W.mp3)
  Segment 23: 87520ms ~ 88096ms (file: 167-167-617-163-167-163-163-163-163-126-127-102-123-102-106-160-610-160-610-167-162-610-610-617-167-163-162-671-671_V.mp3)
  Segment 24: 89096ms ~ 89672ms (file: 753-576-537-573-753-576-357 157-517-756-753-715-175-751-571-751-570-751-751-751-157-751-571-751-135-135-137-137-317-137-137 175-751-751-571-571-571-573-571-751-175-517-751-751-7_W.mp3)
  Segment 25: 90672ms ~ 91248ms (file: 351-367-126-732-637-271-617-106-160-136-103-106-103-160-167-163-106-106-132-216-176-160-136-163-103-135-132-672-136-103-103-132-621-126-260-632-167-106-130-601-610-601-103-210-5_V.mp3)
  Segment 26: 92248ms ~ 92824ms (file: 574-571-756 567-576-576-750-752-756-756-571-751-754-574-574-571-754-574-751-574-574-751-574-571 527-506-256-752-572-571-750-750-576-752-750-572-751-571-574-754-574-754-571-571-5_W.mp3)
  Segment 27: 93824ms ~ 94400ms (file: 675-371-750-216-572-761-267-362-263-367-127-267-506-570-236-263-302-235-261-213-765-726-627-276-715-756-572-720-376-752-571-125-276-275-567-572-765-576-652-672-765-316-126-675-2_V.mp3)
  Segment 28: 95400ms ~ 95976ms (file: 165-236-612-162-576-657-567-675-627-326-231-627-726-671-576-576-675-517-571-632-612-162-162-103-613-130-613-167-612-316-571-130-132-123-316-132-123-361-176-157-156-617-316-106-1_W.mp3)
  Segment 29: 96976ms ~ 97552ms (file: 576-256-257-527-267-625-207-672-762-276-760-756-765-675-765-576-765-762-752-756-576-765-752-576-765-751-756-765-570-756-576-752-527-752-527-725-752-576-572-517-205-205-625-670-6_V.mp3)
  Segment 30: 98552ms ~ 102728ms (file: 635-672-637-157-105-156-175-120-162-127-132-216-260-126-260-267-206-627-276-260-267-162-623-360-260-267-627-672-162-126-261-267-627-627-627-620-627-672-160-160-620-326-301-362-3_W.mp3)
  Segment 31: 103728ms ~ 104304ms (file: 316-623-627-627-657-675-670-672-627 132-623-162-673-312-546-765-675-745-637-576-651-675-627-632-673-670-671-670 263-136-625-167 645 163-107-371-617-106-103-617-165-160-105-305-4_W.mp3)
  Segment 32: 105304ms ~ 105880ms (file: 157-156-157-510-501-157-157-105-157-175-157-157-175-175-175-175-157-571-157-157-175-756-756-754-756-756-756-756-756-756-756-756-754-765-756-756-756-756_W.mp3)
  Segment 33: 106880ms ~ 113936ms (file: 235-102-107-267-670-263-261-527-537-236-547-172-163-163-162-162-126-162-126-120-126-123-132-312-132-123-231-216-362-326-263-732-726-271-132-213-524-132-132-512-123-126-152-512-1_W.mp3)
  Segment 34: 114936ms ~ 115512ms (file: 167-750-756-267 567-275-276-576-715-750-652-627-627-267-527-321-135-705-756-517-152-572-137-620-726-762-236-327-302-602-271-702-273-627-507-235-527-762-756-236-216-723-567-276-2_V.mp3)
  Segment 35: 116512ms ~ 117088ms (file: 134-401-145-105-145-401-157-157-514-105-157-105-105-145-105-104-107-145-104-105-105-104 137-315-310-415-104-143-105-143-154-103-134-175-147-105-140-105-105-140-154-154-175-147-1_V.mp3)
  Segment 36: 118088ms ~ 118664ms (file: 372-130-173-130-317-301-137-317-310-157-137-135-175-751-765-670 107-731-103-317-137-107-360-603-670-160-610-160-312-310-317-307-310-137-135-320-137-310-701-107-317-317-135-351-3_V.mp3)
  Segment 37: 119664ms ~ 120240ms (file: 267-625-257-762-621-621-213-621-572-576-567-267-172-765-570-573-756-267-532-157-257-273-327-256-321-230-263-236-605-263-532-576-237-675-572-762-570-760-236-213-321-570-576-675-7_V.mp3)
  Segment 38: 121240ms ~ 121816ms (file: 517-517-157-571-510-150-105-501-350-105-153-107-105-153-517-150-501-501-510-105-150-513-531 157-157-135-105-153-157-501-135-153-157-150-501-571-150-175-501-105-501-513-105-150-1_V.mp3)
  Segment 39: 122816ms ~ 123392ms (file: 657-657-675-653-651-536-657-635-653-657-657 562-652-651-652-651-651-652-651-652-651-657-615-625-504-315-501-256-562-562-562-256-562-562-562-652-652-625-652-625-265_W.mp3)
  Segment 40: 124392ms ~ 127992ms (file: 627-316-736-627-216-261-261-213-301-132-312-726-261-267-261-263-276-261-621-261-627-276-267-302-201-230-236-136-106-612-132-126-215-216-132-120-132-206-627-672-123-612-726-672-7_W_stt_if_1_2.mp3)
  Segment 41: 128992ms ~ 129568ms (file: 527-507 503-564-502 513-507-507-501-547-523-516-517-514-541-504-507-526-506-514-536-503-512-517-523-531-503-531-530-350-534-541-543-543-542-534-547-523-534-546-546-547-534-504-5_V.mp3)
  Segment 42: 130568ms ~ 131144ms (file: 617-136-165-567-157-651-615-612-657 567-657-657-576-657-675-657-567 165-165-165-162-165-165-165-615-615-652-651-561-165-617-615-165-163-675-675-675-675-675-756-765-673-675 156-1_W.mp3)
  Segment 43: 132144ms ~ 139596ms (file: 153-510-157-105-751-756-675-320-132-372-163-163-617-163-106-751-270-750-173-321-276-103-103-135-317-237-721-507-501-132-627-173-756-756-106-176-127-576-571-132-103-710-271-102-7_W.mp3)
  Segment 44: 140596ms ~ 141172ms (file: 527-357-657-317-573-257-170-315-517-576-507-316-173-751-517-653-153-326-625-123-352-517-573-671-625-162-361-675-675-613-132-103-103-671-756-567-763-276-351-517-563-576-657-625-1_W.mp3)
  Segment 45: 142172ms ~ 147248ms (file: 123-231-231-213-301-213-123-213-210-127-123-127-213-123-123-132-123-132-123-312-210-123-120-132-127-127-127-217-217-126-120-123-321-132-123-132-123-123-123-123-321-312-132-132-3_W_stt_if.mp3)
  Segment 46: 148248ms ~ 148824ms (file: 610-162-231-657-672 671-123-123-627-627 271-624-326-231-327-231-216-675-761-761-103-132-176-136-613-136-615-613-631-371-127-625-134-321-264-317-341-765-751-756-612-162-263-216-1_V.mp3)
  Segment 47: 149824ms ~ 150400ms (file: 251-251-523-205-251-152-507-507 521-253-521-520-725-527-506-502-520-507-504-507-502-501-502-502-205-265-326-326-215-625-625-657-652-654-625-260-260-260-231-261-627 276-652-503-5_W.mp3)
  Segment 48: 151400ms ~ 151976ms (file: 753-756-576-576-756-576-756-576-756-752 253-236-537-572-570-250-567-567-567-567-567-567-657-567-567-657-567-657-657-567-657 621-752-576-756-756-576-576-576-756-576-756-576-756-5_W.mp3)
  Segment 49: 152976ms ~ 153552ms (file: 753-715-571-715-751-715 750-175-157-751-710-715-735-753-751-571-150-357-375-175-750-153-531 751-753-713-751-517-150-751-137-170-173-157-175-175-731-713-713-730-715-713-715-705-7_V.mp3)
  Segment 50: 154552ms ~ 159772ms (file: 725-231-132-710-132-573-207-762-102-620-263-273-230-273-263-612-571-267-267-256-276-267-276-762-765-756-762-672-263-756-675-267-162-132-613-617-230-574-576-715-571-107-106-751-7_W.mp3)
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
> **Time**: 2026-04-05 12:40:47
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
"transcript": "해서 계속 머무르게 되면 도태될 거고요 지금보다 의도적으로 더 높은 기준에 자신을 노출시킬지 이게 굉장히 중요한데 그건 본인의 선택이에요 평범한 커뮤니티나 뉴스",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "해서", "start_ms": 40, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 480, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "머무르게", "start_ms": 900, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "되면", "start_ms": 1470, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "도태될", "start_ms": 1820, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "거고요", "start_ms": 2300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "지금보다", "start_ms": 2750, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "의도적으로", "start_ms": 3350, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 4000, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "높은", "start_ms": 4180, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "기준에", "start_ms": 4550, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "자신을", "start_ms": 5000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "노출시킬지", "start_ms": 5450, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 6150, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "굉장히", "start_ms": 6420, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "중요한데", "start_ms": 6900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "그건", "start_ms": 7450, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "본인의", "start_ms": 7720, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "선택이에요", "start_ms": 8150, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
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
"transcript": "국으로 오기 쉽지 않았다고 하는데요 잠시 힘든 시간을 겪었지만 지금은 둘 다 한국에 온 걸 후회하지 않고 더욱 열심히 살고 있어요 어린 새아이는 오히려 한국에 더 잘",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "국으로", "start_ms": 30, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "오기", "start_ms": 450, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "쉽지", "start_ms": 800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "않았다고", "start_ms": 1180, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "하는데요", "start_ms": 1720, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "잠시", "start_ms": 2250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "힘든", "start_ms": 2680, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "시간을", "start_ms": 3050, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "겪었지만", "start_ms": 3500, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "지금은", "start_ms": 4100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "둘", "start_ms": 4580, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 4800, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "한국에", "start_ms": 4980, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "온", "start_ms": 5420, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "걸", "start_ms": 5620, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "후회하지", "start_ms": 5820, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "않고", "start_ms": 6400, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "더욱", "start_ms": 6750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "열심히", "start_ms": 7120, "dur_ms": 480, "phase_lr_deg": 0.0}
]
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
"transcript": "로 전 세계를 누비는 거 이거 유지 아닌가요 여러분이 생각하는 이정재의 인생 캐릭터는",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "로", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "전", "start_ms": 250, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "세계를", "start_ms": 480, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "누비는", "start_ms": 950, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 1400, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "이거", "start_ms": 1620, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "유지", "start_ms": 1950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "아닌가요", "start_ms": 2320, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "여러분이", "start_ms": 2900, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "생각하는", "start_ms": 3480, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "이정재의", "start_ms": 4100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "인생", "start_ms": 4680, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "캐릭터는", "start_ms": 5150, "dur_ms": 650, "phase_lr_deg": 0.0}
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
"transcript": "여쭤보러 왔거든요 별거 아닐 수 있는데 투자를 우리 하다 보면 이 종목이 나랑 잘 맞는다 싶은 종목들이 한 번쯤 있잖아요 그게 뭐 자기가 잘 아는 업종일 수도 있고 차트의 움직임이 자기랑 호흡이 잘 맞을 수도 있고 일단 본인이 잘 아는 업종 위주로 우리가 뭐 투자하거나 트레이딩 하다 보면은 그게 좋은 점이 일단 자기가 잘 아는 분야이기 때문에 해당 산업에서 어떤 부분이 앞으로 발전하겠다는 그니까 지금 뭔가 선도적인",
"speaker": "speaker_4",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "여쭤보러", "start_ms": 80, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "왔거든요", "start_ms": 650, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "별거", "start_ms": 1220, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "아닐", "start_ms": 1600, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 1950, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있는데", "start_ms": 2120, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "투자를", "start_ms": 2580, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "우리", "start_ms": 3100, "dur_ms": 300, "phase_lr_deg": 0.0}
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
"transcript": "고 좀 지지부진한 모습을 보이고 있는데 사실 모멘텀으로 움직였던 거는 로봇과 자율주행이었어요 그게 이번에도 얼마나 부각이 될지 다음 주 목요일인데 현대차 그룹이 자율주행과 휴머노이드 로봇 등 그룹 차",
"speaker": "speaker_5",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "고", "start_ms": 60, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 250, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "지지부진한", "start_ms": 480, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "모습을", "start_ms": 1250, "dur_ms": 450, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "어 그냥 똑같잖아요 그럼 손님들이 저희 닭을 살",
"speaker": "speaker_6",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "어", "start_ms": 100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "똑같잖아요", "start_ms": 800, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "그럼", "start_ms": 1500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "손님들이", "start_ms": 1850, "dur_ms": 550, "phase_lr_deg": 0.0}
]
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
"transcript": "모하기 위해 지은 서원입니다 오래된 기와에서 자라는 와송이 그 역사를 말해",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "모하기", "start_ms": 40, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "위해", "start_ms": 600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "지은", "start_ms": 1000, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "서원입니다", "start_ms": 1350, "dur_ms": 720, "phase_lr_deg": 0.0}
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
"transcript": "잘 자라는데 아날로그 하려고 하니까",
"speaker": "speaker_7",
"confidence": 0.91,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "잘", "start_ms": 60, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "자라는데", "start_ms": 350, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "아날로그", "start_ms": 1000, "dur_ms": 620, "phase_lr_deg": 0.0}
]
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
"transcript": "서 400만 원 정도 수익을 잘 내고 이제 저도 이제 어제 지수의 낙폭이 과하다 그리고 하이닉스가 80만 원 초반대까지 내려왔기 때문에 종가 직전에",
"speaker": "speaker_8",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "서", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "400만", "start_ms": 300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "원", "start_ms": 780, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "정도", "start_ms": 1000, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "수익을", "start_ms": 1350, "dur_ms": 450, "phase_lr_deg": 0.0}
]
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
"transcript": "선 같은 경우에는 어제 21개 정도 남아 있어서 이제 추가로 그 70",
"speaker": "speaker_8",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "선", "start_ms": 80, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "같은", "start_ms": 350, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "경우에는", "start_ms": 700, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "어제", "start_ms": 1250, "dur_ms": 350, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 12:40:47*
