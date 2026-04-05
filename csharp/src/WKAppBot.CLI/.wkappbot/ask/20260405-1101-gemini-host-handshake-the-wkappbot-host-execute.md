# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=30524  time=11:00:27  uptime=23339s  mem=356MB  system_processes=353"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_110014.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 521-521-512-250-251-205-210-102-215-210-120-217-127-125-275-257-127-275-751_V.mp3)
  Segment 2: 1576ms ~ 15220ms (file: 320-321-237-231-327-273-270-371-237-163-135-175-103-103-256-325-275-726-237-325-276-754-263-136-167-132-750-753-756-127-176-573-106-321-376-726-231-317-267-207-237-672-102-106-7_W.mp3)
  Segment 3: 16220ms ~ 16796ms (file: 576-627-725 236-216-267-617-163-576-756-105-521-163-136-356-627-613-163-170-756-765-167-107-657-623 321-326-265-167-176-127-761-175-567-163-632-163-132-173-106-135-351 563-761-1_W.mp3)
  Segment 4: 17796ms ~ 23628ms (file: 320-263-321-102-236-123-127-321-320-230-213-230-267-267-726-125-214-230-102-236-361-160-136-213-103-371-361-163-136-132-160-167-137-213-216-136-617-217-127-320-263-251-756-610-3_W.mp3)
  Segment 5: 24628ms ~ 33664ms (file: 574-273-372-237-120-103-123-103-705-157-103-132-756-574-125-132-213-135-310-307-102-574-152-170-270-756-725-675-756-675-760-132-310-754-765-123-315-123-206-206-210-547-251-312-2_V.mp3)
  Segment 6: 34664ms ~ 35240ms (file: 510-507-510-571-570-571-570-571-571-751-751-570-750-750-157-570-571 574-570-150-510-517 105-315-571-157-571-570-107-751-715-713 507-507-507-570-571-705-751-750-571-571-570-571-5_W.mp3)
  Segment 7: 36240ms ~ 36816ms (file: 751-176-170 105-105-150-157-175-170-157-157-175-175-175-176-175-175-157-175-175-157-175-157-170-175-571 154-105-105-105-150-154-157-517-175-170-176-175-167-761-176-167-176-167-1_V.mp3)
  Segment 8: 37816ms ~ 42136ms (file: 267-326-527-756-275-271-126-126-126-720-231-160-276-761-712-671-721-762-127-701-276-271-216-217-213-216-126-126-126-123-123-120-126-126-120-126-210-216-123-123-120-123-132-321-3_V.mp3)
  Segment 9: 43136ms ~ 43712ms (file: 167-162-102-163-103-103-136-756-756-675 671-574-570-507-162-651-316-761-624-154-516-570-175-710-751-761-675 371-162-137-607-316-123-231-132-613-317-123 341-137-321-132-627-267-2_V.mp3)
  Segment 10: 44712ms ~ 45288ms (file: 157-517-751-751-517-715-157-715-157-751-157-576-751-571-571-571-571-567-567-567-576 150-571-715-751-507-175-517-510-517-571-571-571-576-571-756-751-756-571-576-576-576-517-576-5_V.mp3)
  Segment 11: 46288ms ~ 46864ms (file: 657-561-567-576-576-567-576-657-651-567-576-572-627-526-562-576-536-376-326-326-673-536-537-135-513-315-657-153-315-316-256-126-123-216-235-315-357-356-316-263-627-625-627-263-5_W.mp3)
  Segment 12: 47864ms ~ 48440ms (file: 167-165-175-165-617-167-165-176-156-126-165-163-612-612-652-165-165-165-563-651-531-156-351-562-325-132-312-136-156-152-156-156-156-165-165-651-365-136-365-615-652-365-351-135-1_W.mp3)
  Segment 13: 49440ms ~ 56352ms (file: 372-312-132-267-236-123-527-231-231-752-260-235-132-301-103-236-273-267-126-157-532-312-657-127-175-753-627-130-120-102-216-152-750-576-231-612-130-365-236-236-361-231-321-103-1_W.mp3)
  Segment 14: 57352ms ~ 63724ms (file: 175-153-315-105-705-107-157-106-251-123-103-157-276-173-715-627-102-154-756-301-216-621-175-756-167-231-507-756-270-725-157-106-132-621-357-357-156-175-672-261-310-571-312-765-7_W.mp3)
  Segment 15: 64724ms ~ 65300ms (file: 145-267-236-312-216-615-123-126-162-156-162-275-732-263-132-213-132-312-136 612-621-672-671-245-573-167-125-167-165-675-652-265-625-625 576-576-567-576 671-672-672-623-263-326-2_V.mp3)
  Segment 16: 66300ms ~ 69792ms (file: 567-657-567-675-657-657-651-617-651-621-657-275-567-675-562-567-165-567-516-652-206-206-620-213-216-623-620-276-206-301-206-756-576-576-576-576-567-576-756-657-756-567-652-657-6_W.mp3)
  Segment 17: 70792ms ~ 71368ms (file: 103-130-132-130-701-173-130-130-130-310-103-103-103-371-356 132-137-130-103-127-317-301-317-310-103-310-107-103-170-137-310-301-310-371-130-130-317-103-713 135-135-310-317-137-3_V.mp3)
  Segment 18: 72368ms ~ 72944ms (file: 576-517-657-517-576-517-517-571-501-510-570-576-571-570-570-520-576-570-571-501-517-571-571-571-571-576-513-571-570-501-570-517-756-517-157-173-175-173-571-756-571-751-517-175-7_W.mp3)
  Segment 19: 73944ms ~ 74520ms (file: 617-617-167-175-163-365-376-657-576-137-736-356-371-357 735-731-130-301-135-130-613-617-316-631-160-607-613-760-617-613-613-361-617-631-637-631-631-635-612-673-671-675-637-657-6_W.mp3)
  Segment 20: 75520ms ~ 88048ms (file: 261-103-120-236-312-130-310-574-362-362-135-372-762-310-736-267-123-123-126-103-213-231-172-260-237-263-372-130-263-574-263-321-132-312-130-312-321-130-153-135-160-103-263-163-2_W.mp3)
  Segment 21: 89048ms ~ 94844ms (file: 206-130-136-206-312-362-367-130-130-201-127-257-257-312-236-135-172-672-127-261-210-102-217-207-210-201-205-210-215-216-257-267-102-102-316-361-706-312-136-103-315-137-103-105-1_W.mp3)
  Segment 22: 95844ms ~ 96420ms (file: 750-627-725-312-526-627-513-352-275-371-132-231-572-672-237-735-576-652-567-725-572-457-265-312-267-572-245-132-321-206-257-756-236-623-260-613-576-623-263-726-675-765-657-764 2_V.mp3)
  Segment 23: 97420ms ~ 97996ms (file: 317-167-103-136-135-153-106-103-102-162-135-163-761-163-103-132-103-132-263-621_V.mp3)
  Segment 24: 98996ms ~ 99572ms (file: 216-213-271-157-157-275-271-123-126-103-267-265-572-756-756-756-567-765-265-163-631-163-165-167-175-715-715-157-175-571-756-765-576-756-275-627-671-672-675-567-576-576-756-576-7_W.mp3)
  Segment 25: 100572ms ~ 101148ms (file: 312-123-715-136-215-137-265-162-216-752-165-251-176-572-571-761-215-672-267-123-126-652-536-236-572-570-263-120-603-657-567-675-657-576-635_W.mp3)
  Segment 26: 102148ms ~ 102724ms (file: 572 716-231-362-263-136-316-317-137-362-623-267-627-265-625-625-567-574-540-524 762-357-357-162-632-263-261-621-257-325-263-514-540-542-215-365-235-165-265-215-236-761-172-672-2_W.mp3)
  Segment 27: 103724ms ~ 104300ms (file: 103-134-130-167-517-532-532-532-326-103-210-312-236-126-723-231-126 103-136-173-136-517-163-315-103-317-156-136-317-316-136-326-137-136-103-126-127-716-106-103-103-103-136-150-1_V.mp3)
  Segment 28: 105300ms ~ 105876ms (file: 567-576-751-756-576-175 710-715-170-175-170-107-175-170-170-175-756-517-567-576-576-756-716-761-576-756-716 150-501-507-571-157-107-150-107-170-130-137-170-170-710-170-705-710-1_W.mp3)
  Segment 29: 106876ms ~ 107452ms (file: 156-613-176 754-756-103-213-174-136-136-136-136-706-760-103-103_V.mp3)
  Segment 30: 108452ms ~ 109028ms (file: 351-631-362-362-326-650-376-362-267-567-563-317-615-157-576-375-123-267-257-765-132-673-607-315-317-137-370-736-361-362-716-173-132-137_V.mp3)
  Segment 31: 110028ms ~ 110604ms (file: 625-673 715-143-314-312-127-105-137-514-135-315-156-612-571-641-143-265-165-123-132-213-631-136-163-632-136-132-102_V.mp3)
  Segment 32: 111604ms ~ 112180ms (file: 231-231-321-213-231-201-231-670-216-130 120-172-672_W.mp3)
  Segment 33: 113180ms ~ 115700ms (file: 107-175-175-672-276-620-130-132-201 751-751-750-173-751-715-517-713-173-713-173-715-137-170-137-715-175-751-751-157-751-175-571-571-753-175-751-573-573-750-751-751 751-751-751-1_W.mp3)
  Segment 34: 116700ms ~ 127932ms (file: 123-167-106-617-765-260-762-573-130-267-627-107-167-162-671-271-261-312-102-163-102-123-326-236-261-231-751-167-103-106-130-123-132-231-312-132-273-752-573-157-517-103-237-273-6_W.mp3)
  Segment 35: 128932ms ~ 129508ms (file: 156-563-572-514-546-547-512-624-645-654-652-654-654-654-654-654-654-654-654-654-654-654-632-516-513-531-534-513-514-546-541-534-354-571-654-654-654-645-645-645-645-645-654-645-6_W.mp3)
  Segment 36: 130508ms ~ 131084ms (file: 572-752-752-750-756-756-756-752-752-756-756-756-756-756-576 576-507-520-250-572-570-527-570-250-750-752-705-756-752-750-752-750-756-756-756-756 507-502-275-726-267-257-267-520-5_V.mp3)
  Segment 37: 132084ms ~ 132660ms (file: 612-165-312-657-675-126-306-365-635-631-651-231_W.mp3)
  Segment 38: 133660ms ~ 134236ms (file: 751-753-750-236-567-357-132-713-562-620-312-367-317-731-312-132-136-132_V.mp3)
  Segment 39: 135236ms ~ 135812ms (file: 157-571-501-510-517-501-501 510-150-501-571-517-105-510-105-517-501-501-150-150-510-150-150-150-501-501-501-510-501-157-150-501-510-501-517-570-506 157-517-105-105-517-105-517-1_V.mp3)
  Segment 40: 136812ms ~ 137388ms (file: 167-752-751-175-176-167-176-752-756-576-657-567-567-567-576-657-567-756-576-675-675-765-567-657-675-657-761-617-675-615-576-562-576-576-572-572-756-572-572-652-527-167-157-157-1_W.mp3)
  Segment 41: 138388ms ~ 138964ms (file: 213-312-231-327-235-253-527-527-532-357-352-352-357-351-352-521-523-572-572-537-253-235-253-325-235-213-573-571-725-236-326-235-326-236-326-235-213-321-321-237-352-523-532-235-2_W.mp3)
  Segment 42: 139964ms ~ 140540ms (file: 176-576-726-705-267-502-326-756-675-237-327-236-501-136-325-137-257-257-765-357-356-502-705-357-756-540-267-103-201-176-137-173-256-123-267-236-157-137-127-265-507-261-570-726-2_V.mp3)
  Segment 43: 141540ms ~ 142116ms (file: 134-136-316-136-103-136-135-136-103-316 315-316-301-314-316-136-134-136-103-134-137-103-103-134-135-103-103-134-103-134-136-103-103-135-103-130-103 513-317-537-351-571-157-571 1_V.mp3)
  Segment 44: 143116ms ~ 154960ms (file: 276-263-316-130-726-762-627-267-321-231-326-623-265-273-270-126-102-162-123-130-321-572-672-627-273-127-763-107-107-713-231-103-231-103-132-143-106-132-231-231-326-216-276-103-2_W.mp3)
  Segment 45: 155960ms ~ 156536ms (file: 547-547-571-574-571-574-574-571-157-517-157-751-715-130-357-135-716-751-751-756-753-173-137-135-137-135-160-136-617-315-367-673-765-576-765-173-137-316-136-163-671-137-163-165-1_W.mp3)
  Segment 46: 157536ms ~ 158112ms (file: 156-136-163-136-165-136-137-176-157-157-175 105-510-576-517-157-576-756-751-571-756-716-165-163-371-176_W.mp3)
  Segment 47: 159112ms ~ 159688ms (file: 517-751-576-715-751-751-571-571-756-756-756-753-756-751-753 157-571-576-576-571-576-756-576-571-516-517-517-576-571-571-571-571-517-723-763-765-267-265 574-574-574-574-507-503-5_W.mp3)
  Segment 48: 160688ms ~ 161264ms (file: 216-126-136-132-710-765-760-610-260-267-263-326-361-763-130-126-751-735-132-316-361-756-675-761 263 132-132-310-130-312-231 765-756-765-260-260 571-135 627-103-765_W.mp3)
  Segment 49: 162264ms ~ 162840ms (file: 265-502-652-526-527-526-527-526-256-527 527-520-520-527-527-250-502-520-257-256-520-527-520-526-567-572-567-526_W.mp3)
  Segment 50: 163840ms ~ 164416ms (file: 510-150-105-501-105-105-501-510-501-570-574-510-510-501-507-517-517-507-517-517-541-541-514-517-514-517-576 510-105-501-715-157-105-501-571-507-501-507-510-150-570-510-517-561-5_W.mp3)
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
> **Time**: 2026-04-05 11:01:45
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
"transcript": "해서 잘못된 부분에 대해서는 당원과 국민들께 사과하고 한 번은 짚고 넘어가야 될 일들이 필요하지 않나 생각이 듭니다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 320,
"audio_type": "speech",
"words": [
{"word": "해서", "start_ms": 320, "dur_ms": 321, "phase_lr_deg": 0.0},
{"word": "잘못된", "start_ms": 641, "dur_ms": 237, "phase_lr_deg": 0.0},
{"word": "부분에", "start_ms": 878, "dur_ms": 231, "phase_lr_deg": 0.0},
{"word": "대해서는", "start_ms": 1109, "dur_ms": 327, "phase_lr_deg": 0.0},
{"word": "당원과", "start_ms": 1436, "dur_ms": 273, "phase_lr_deg": 0.0},
{"word": "국민들께", "start_ms": 1709, "dur_ms": 270, "phase_lr_deg": 0.0},
{"word": "사과하고", "start_ms": 1979, "dur_ms": 371, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 2350, "dur_ms": 237, "phase_lr_deg": 0.0},
{"word": "번은", "start_ms": 2587, "dur_ms": 163, "phase_lr_deg": 0.0},
{"word": "짚고", "start_ms": 2750, "dur_ms": 135, "phase_lr_deg": 0.0},
{"word": "넘어가야", "start_ms": 2885, "dur_ms": 175, "phase_lr_deg": 0.0},
{"word": "될", "start_ms": 3060, "dur_ms": 103, "phase_lr_deg": 0.0},
{"word": "일들이", "start_ms": 3163, "dur_ms": 103, "phase_lr_deg": 0.0},
{"word": "필요하지", "start_ms": 3266, "dur_ms": 256, "phase_lr_deg": 0.0},
{"word": "않나", "start_ms": 3522, "dur_ms": 325, "phase_lr_deg": 0.0},
{"word": "생각이", "start_ms": 3847, "dur_ms": 275, "phase_lr_deg": 0.0},
{"word": "듭니다.", "start_ms": 4122, "dur_ms": 726, "phase_lr_deg": 0.0}
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
"transcript": "한반도는 겨울 혹한에 풀이 죽어버리고 여름엔 폭우로 풀밭이 쑥대밭이 되는 계절의 진폭이 극단적인 땅입니다.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 320,
"audio_type": "speech",
"words": [
{"word": "한반도는", "start_ms": 320, "dur_ms": 263, "phase_lr_deg": 0.0},
{"word": "겨울", "start_ms": 583, "dur_ms": 321, "phase_lr_deg": 0.0},
{"word": "혹한에", "start_ms": 904, "dur_ms": 102, "phase_lr_deg": 0.0},
{"word": "풀이", "start_ms": 1006, "dur_ms": 236, "phase_lr_deg": 0.0},
{"word": "죽어버리고", "start_ms": 1242, "dur_ms": 123, "phase_lr_deg": 0.0},
{"word": "여름엔", "start_ms": 1365, "dur_ms": 127, "phase_lr_deg": 0.0},
{"word": "폭우로", "start_ms": 1492, "dur_ms": 321, "phase_lr_deg": 0.0},
{"word": "풀밭이", "start_ms": 1813, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "쑥대밭이", "start_ms": 2133, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "되는", "start_ms": 2363, "dur_ms": 213, "phase_lr_deg": 0.0},
{"word": "계절의", "start_ms": 2576, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "진폭이", "start_ms": 2806, "dur_ms": 267, "phase_lr_deg": 0.0},
{"word": "극단적인", "start_ms": 3073, "dur_ms": 267, "phase_lr_deg": 0.0},
{"word": "땅입니다.", "start_ms": 3340, "dur_ms": 726, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "자폭 드론이 날아온다고 생각하고 작은 선박의 움직임에도 혹시나 이제 공격받는 건 아닌지 그런 생각들을 많이 앉아 계시죠.",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 574,
"audio_type": "speech",
"words": [
{"word": "자폭", "start_ms": 574, "dur_ms": 273, "phase_lr_deg": 0.0},
{"word": "드론이", "start_ms": 847, "dur_ms": 372, "phase_lr_deg": 0.0},
{"word": "날아온다고", "start_ms": 1219, "dur_ms": 237, "phase_lr_deg": 0.0},
{"word": "생각하고", "start_ms": 1456, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "작은", "start_ms": 1576, "dur_ms": 103, "phase_lr_deg": 0.0},
{"word": "선박의", "start_ms": 1679, "dur_ms": 123, "phase_lr_deg": 0.0},
{"word": "움직임에도", "start_ms": 1802, "dur_ms": 103, "phase_lr_deg": 0.0},
{"word": "혹시나", "start_ms": 1905, "dur_ms": 705, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 2610, "dur_ms": 157, "phase_lr_deg": 0.0},
{"word": "공격받는", "start_ms": 2767, "dur_ms": 103, "phase_lr_deg": 0.0},
{"word": "건", "start_ms": 2870, "dur_ms": 132, "phase_lr_deg": 0.0},
{"word": "아닌지", "start_ms": 3002, "dur_ms": 756, "phase_lr_deg": 0.0},
{"word": "그런", "start_ms": 3758, "dur_ms": 574, "phase_lr_deg": 0.0},
{"word": "생각들을", "start_ms": 4332, "dur_ms": 125, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 4457, "dur_ms": 132, "phase_lr_deg": 0.0},
{"word": "앉아", "start_ms": 4589, "dur_ms": 213, "phase_lr_deg": 0.0},
{"word": "계시죠.", "start_ms": 4802, "dur_ms": 135, "phase_lr_deg": 0.0}
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
"transcript": "가장 큰 기업으로 볼 수 있는데 스페이스 X만 있는 건 아닙니다. 엄청 많이 생기고 있고 종류도 엄청 많다라는 겁니다.",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 267,
"audio_type": "speech",
"words": [
{"word": "가장", "start_ms": 267, "dur_ms": 326, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 593, "dur_ms": 527, "phase_lr_deg": 0.0},
{"word": "기업으로", "start_ms": 1120, "dur_ms": 756, "phase_lr_deg": 0.0},
{"word": "볼", "start_ms": 1876, "dur_ms": 275, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 2151, "dur_ms": 271, "phase_lr_deg": 0.0},
{"word": "있는데", "start_ms": 2422, "dur_ms": 126, "phase_lr_deg": 0.0},
{"word": "스페이스", "start_ms": 2548, "dur_ms": 126, "phase_lr_deg": 0.0},
{"word": "X만", "start_ms": 2674, "dur_ms": 126, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 2800, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "건", "start_ms": 3520, "dur_ms": 231, "phase_lr_deg": 0.0},
{"word": "아닙니다.", "start_ms": 3751, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "엄청", "start_ms": 3911, "dur_ms": 276, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 4187, "dur_ms": 761, "phase_lr_deg": 0.0},
{"word": "생기고", "start_ms": 4948, "dur_ms": 712, "phase_lr_deg": 0.0},
{"word": "있고", "start_ms": 5660, "dur_ms": 671, "phase_lr_deg": 0.0},
{"word": "종류도", "start_ms": 6331, "dur_ms": 721, "phase_lr_deg": 0.0},
{"word": "엄청", "start_ms": 7052, "dur_ms": 762, "phase_lr_deg": 0.0},
{"word": "많다라는", "start_ms": 7814, "dur_ms": 127, "phase_lr_deg": 0.0},
{"word": "겁니다.", "start_ms": 7941, "dur_ms": 701, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "이런 다양한 사이즈들 로켓들이 있는데 이 로켓들이 점점...",
"speaker": "speaker_4",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 167,
"audio_type": "speech",
"words": [
{"word": "이런", "start_ms": 167, "dur_ms": 162, "phase_lr_deg": 0.0},
{"word": "다양한", "start_ms": 329, "dur_ms": 102, "phase_lr_deg": 0.0},
{"word": "사이즈들", "start_ms": 431, "dur_ms": 163, "phase_lr_deg": 0.0},
{"word": "로켓들이", "start_ms": 594, "dur_ms": 103, "phase_lr_deg": 0.0},
{"word": "있는데", "start_ms": 697, "dur_ms": 103, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 800, "dur_ms": 136, "phase_lr_deg": 0.0},
{"word": "로켓들이", "start_ms": 936, "dur_ms": 756, "phase_lr_deg": 0.0},
{"word": "점점...", "start_ms": 1692, "dur_ms": 756, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 13,
"transcript": "뚫어지라 보던 외국인들이 갑자기 소리를 지릅니다. 오 마이 갓 이 아저씨 456번...",
"speaker": "speaker_5",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 372,
"audio_type": "speech",
"words": [
{"word": "뚫어지라", "start_ms": 372, "dur_ms": 312, "phase_lr_deg": 0.0},
{"word": "보던", "start_ms": 684, "dur_ms": 132, "phase_lr_deg": 0.0},
{"word": "외국인들이", "start_ms": 816, "dur_ms": 267, "phase_lr_deg": 0.0},
{"word": "갑자기", "start_ms": 1083, "dur_ms": 236, "phase_lr_deg": 0.0},
{"word": "소리를", "start_ms": 1319, "dur_ms": 123, "phase_lr_deg": 0.0},
{"word": "지릅니다.", "start_ms": 1442, "dur_ms": 527, "phase_lr_deg": 0.0},
{"word": "오", "start_ms": 1969, "dur_ms": 231, "phase_lr_deg": 0.0},
{"word": "마이", "start_ms": 2200, "dur_ms": 231, "phase_lr_deg": 0.0},
{"word": "갓", "start_ms": 2431, "dur_ms": 752, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 3183, "dur_ms": 260, "phase_lr_deg": 0.0},
{"word": "아저씨", "start_ms": 3443, "dur_ms": 235, "phase_lr_deg": 0.0},
{"word": "456번...", "start_ms": 3678, "dur_ms": 132, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "포커스 양보가 필요합니다.",
"speaker": "speaker_6",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 175,
"audio_type": "speech",
"words": [
{"word": "포커스", "start_ms": 175, "dur_ms": 153, "phase_lr_deg": 0.0},
{"word": "양보가", "start_ms": 328, "dur_ms": 315, "phase_lr_deg": 0.0},
{"word": "필요합니다.", "start_ms": 643, "dur_ms": 105, "phase_lr_deg": 0.0}
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
"transcript": "뭔가 합의된 내용이나 없으면은 총 공세를 할 수 있다 발전소도 공격할 수 있다 이렇게 트럼프가 선포를 했기 때문에...",
"speaker": "speaker_7",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 567,
"audio_type": "speech",
"words": [
{"word": "뭔가", "start_ms": 567, "dur_ms": 657, "phase_lr_deg": 0.0},
{"word": "합의된", "start_ms": 1224, "dur_ms": 567, "phase_lr_deg": 0.0},
{"word": "내용이나", "start_ms": 1791, "dur_ms": 675, "phase_lr_deg": 0.0},
{"word": "없으면은", "start_ms": 2466, "dur_ms": 657, "phase_lr_deg": 0.0},
{"word": "총", "start_ms": 3123, "dur_ms": 657, "phase_lr_deg": 0.0},
{"word": "공세를", "start_ms": 3780, "dur_ms": 651, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 4431, "dur_ms": 617, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 5048, "dur_ms": 651, "phase_lr_deg": 0.0},
{"word": "있다", "start_ms": 5699, "dur_ms": 621, "phase_lr_deg": 0.0},
{"word": "발전소도", "start_ms": 6320, "dur_ms": 657, "phase_lr_deg": 0.0},
{"word": "공격할", "start_ms": 6977, "dur_ms": 275, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 7252, "dur_ms": 567, "phase_lr_deg": 0.0},
{"word": "있다", "start_ms": 7819, "dur_ms": 675, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 8494, "dur_ms": 562, "phase_lr_deg": 0.0},
{"word": "트럼프가", "start_ms": 9056, "dur_ms": 165, "phase_lr_deg": 0.0},
{"word": "선포를", "start_ms": 9221, "dur_ms": 567, "phase_lr_deg": 0.0},
{"word": "했기", "start_ms": 9788, "dur_ms": 516, "phase_lr_deg": 0.0},
{"word": "때문에...", "start_ms": 10304, "dur_ms": 652, "phase_lr_deg": 0.0}
]
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
"transcript": "포커스 양보가 필요합니다. 노트로 전진해요. 즉 사람이 빠른 걸음으로 걷는 속도 그대로 얼음을 깨면서 앞으로 나가는 거예요. 여기에 스마트한 기술도 추가됐습니다.",
"speaker": "speaker_8",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 261,
"audio_type": "speech",
"words": [
{"word": "포커스", "start_ms": 261, "dur_ms": 103, "phase_lr_deg": 0.0},
{"word": "양보가", "start_ms": 364, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "필요합니다.", "start_ms": 484, "dur_ms": 236, "phase_lr_deg": 0.0},
{"word": "노트로", "start_ms": 720, "dur_ms": 312, "phase_lr_deg": 0.0},
{"word": "전진해요.", "start_ms": 1032, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "즉", "start_ms": 1162, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "사람이", "start_ms": 1472, "dur_ms": 574, "phase_lr_deg": 0.0},
{"word": "빠른", "start_ms": 2046, "dur_ms": 362, "phase_lr_deg": 0.0},
{"word": "걸음으로", "start_ms": 2408, "dur_ms": 362, "phase_lr_deg": 0.0},
{"word": "걷는", "start_ms": 2770, "dur_ms": 135, "phase_lr_deg": 0.0},
{"word": "속도", "start_ms": 2905, "dur_ms": 372, "phase_lr_deg": 0.0},
{"word": "그대로", "start_ms": 3277, "dur_ms": 762, "phase_lr_deg": 0.0},
{"word": "얼음을", "start_ms": 4039, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "깨면서", "start_ms": 4349, "dur_ms": 736, "phase_lr_deg": 0.0},
{"word": "앞으로", "start_ms": 5085, "dur_ms": 267, "phase_lr_deg": 0.0},
{"word": "나가는", "start_ms": 5352, "dur_ms": 123, "phase_lr_deg": 0.0},
{"word": "거예요.", "start_ms": 5475, "dur_ms": 123, "phase_lr_deg": 0.0},
{"word": "여기에", "start_ms": 5598, "dur_ms": 126, "phase_lr_deg": 0.0},
{"word": "스마트한", "start_ms": 5724, "dur_ms": 103, "phase_lr_deg": 0.0},
{"word": "기술도", "start_ms": 5827, "dur_ms": 213, "phase_lr_deg": 0.0},
{"word": "추가됐습니다.", "start_ms": 6040, "dur_ms": 231, "phase_lr_deg": 0.0}
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
"transcript": "그냥 50조씩 벌면은 올해 200조 벌겠네? 아니 2분기 3분기 더 벌어가지고 뭐 한 80조씩 번대. 그래서 뭐 300조가 넘어간대요 막 그런 얘기 계속 나오고 있거든요. 야 그럼 300조를 벌면 적어도 1500조는 가야 되는 게 아니냐 막 이런 내용이 나오면서 그러면 40% 더 오를 수...",
"speaker": "speaker_9",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 750,
"audio_type": "speech",
"words": [
{"word": "그냥", "start_ms": 750, "dur_ms": 627, "phase_lr_deg": 0.0},
{"word": "50조씩", "start_ms": 1377, "dur_ms": 725, "phase_lr_deg": 0.0},
{"word": "벌면은", "start_ms": 2102, "dur_ms": 312, "phase_lr_deg": 0.0},
{"word": "올해", "start_ms": 2414, "dur_ms": 526, "phase_lr_deg": 0.0},
{"word": "200조", "start_ms": 2940, "dur_ms": 627, "phase_lr_deg": 0.0},
{"word": "벌겠네?", "start_ms": 3567, "dur_ms": 513, "phase_lr_deg": 0.0},
{"word": "아니", "start_ms": 4080, "dur_ms": 352, "phase_lr_deg": 0.0},
{"word": "2분기", "start_ms": 4432, "dur_ms": 275, "phase_lr_deg": 0.0},
{"word": "3분기", "start_ms": 4707, "dur_ms": 371, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 5078, "dur_ms": 132, "phase_lr_deg": 0.0},
{"word": "벌어가지고", "start_ms": 5210, "dur_ms": 231, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 5441, "dur_ms": 572, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 6013, "dur_ms": 672, "phase_lr_deg": 0.0},
{"word": "80조씩", "start_ms": 6685, "dur_ms": 237, "phase_lr_deg": 0.0},
{"word": "번대.", "start_ms": 6922, "dur_ms": 735, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 7657, "dur_ms": 576, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 8233, "dur_ms": 652, "phase_lr_deg": 0.0},
{"word": "300조가", "start_ms": 8885, "dur_ms": 567, "phase_lr_deg": 0.0},
{"word": "넘어간대요", "start_ms": 9452, "dur_ms": 725, "phase_lr_deg": 0.0},
{"word": "막", "start_ms": 10177, "dur_ms": 572, "phase_lr_deg": 0.0},
{"word": "그런", "start_ms": 10749, "dur_ms": 457, "phase_lr_deg": 0.0},
{"word": "얘기", "start_ms": 11206, "dur_ms": 265, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 11471, "dur_ms": 312, "phase_lr_deg": 0.0},
{"word": "나오고", "start_ms": 11783, "dur_ms": 267, "phase_lr_deg": 0.0},
{"word": "있거든요.", "start_ms": 12050, "dur_ms": 572, "phase_lr_deg": 0.0},
{"word": "야", "start_ms": 12622, "dur_ms": 245, "phase_lr_deg": 0.0},
{"word": "그럼", "start_ms": 12867, "dur_ms": 132, "phase_lr_deg": 0.0},
{"word": "300조를", "start_ms": 12999, "dur_ms": 321, "phase_lr_deg": 0.0},
{"word": "벌면", "start_ms": 13320, "dur_ms": 206, "phase_lr_deg": 0.0},
{"word": "적어도", "start_ms": 13526, "dur_ms": 257, "phase_lr_deg": 0.0},
{"word": "1500조는", "start_ms": 13783, "dur_ms": 756, "phase_lr_deg": 0.0},
{"word": "가야", "start_ms": 14539, "dur_ms": 236, "phase_lr_deg": 0.0},
{"word": "되는", "start_ms": 14775, "dur_ms": 623, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 15398, "dur_ms": 260, "phase_lr_deg": 0.0},
{"word": "아니냐", "start_ms": 15658, "dur_ms": 613, "phase_lr_deg": 0.0},
{"word": "막", "start_ms": 16271, "dur_ms": 576, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 16847, "dur_ms": 623, "phase_lr_deg": 0.0},
{"word": "내용이", "start_ms": 17470, "dur_ms": 263, "phase_lr_deg": 0.0},
{"word": "나오면서", "start_ms": 17733, "dur_ms": 726, "phase_lr_deg": 0.0},
{"word": "그러면", "start_ms": 18459, "dur_ms": 675, "phase_lr_deg": 0.0},
{"word": "40%", "start_ms": 19134, "dur_ms": 765, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 19899, "dur_ms": 657, "phase_lr_deg": 0.0},
{"word": "오를", "start_ms": 20556, "dur_ms": 764, "phase_lr_deg": 0.0},
{"word": "수...", "start_ms": 21320, "dur_ms": 215, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 11:01:45*
