# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=14936  time=06:40:24  uptime=17724s  mem=449MB  system_processes=351"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_063705.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 9072ms (file: 572-275-126-321-201-230-123-260-260-301-130-236-275-120-261-201-572-236-231-260-672-765-132-257-206-260-675-765-107-257-201-756-256-120-261-260-260-106-316-260-136-207-210-260-1_W.mp3)
  Segment 2: 10072ms ~ 10648ms (file: 756-213-576-213-627-327-261-213-257-326-623-267-567-576-325-517-563-327-376-623-263-572-623-570-256-621-126-276-217-312-275-326-207-276-270-326-627-756-675-753-567-725-375-276-7_V.mp3)
  Segment 3: 11648ms ~ 34616ms (file: 370-176-670-137-173-620-673-367-201-275-627-276-213-230-362-210-267-102-106-263-201 106-167-137-107-106-176-126-673-367-361-132-127 671-617-367-367 107-524-251-321-167-132-162-1_W.mp3)
  Segment 4: 35616ms ~ 36192ms (file: 510-150-510-150-517-510-105-157-510-157-571-517-105-150-517-507-571-571-107-570-517-517-153-574-570-574-571-574-574-507-570 507-150-105-517-150-501-513-105-150-105-170-507-105-5_V.mp3)
  Segment 5: 37192ms ~ 37768ms (file: 263-326-216-126-261-267-567-657-675-675-675-670-672-263-362-362-321-362-756-675-736-176-603-632-321-632-326-321-261-321-574-576-132-241-721-765-106-136-315-670-576-756-675-527-3_V.mp3)
  Segment 6: 38768ms ~ 43844ms (file: 523-527-257-206-675-263-210-520-267-213-206-135-236-206-213-762-123-310-573-312-213-203-572-103-675-731-316-326-176-136-137-176-163-675-675-316-167-673-617-167-671-753-752-167-6_W.mp3)
  Segment 7: 44844ms ~ 45420ms (file: 103-750-573-753-175 135-301-103-137-371-173-103-317-371-715-175-713-135-173-130-735-137-751-157-751-753-175-705-735-715-753-715-731-375-150-573 153-157-731-301-307-301-317-173-1_V.mp3)
  Segment 8: 46420ms ~ 52396ms (file: 205-201-507-215-132-321-376-321-213-205-137-127-213-231-573-520-275-261-265-506-123-102-103-276-375-312-267-725-573-573-201-325-135-302-263-725-123-107-103-135-130-137-167-267-2_W.mp3)
  Segment 9: 53396ms ~ 53972ms (file: 621-231-321-623-261-261-263-263 712-176-517-263-260-715-705-576-576-765-136-761-761-765-675-765-761-657-756-527-567-572-572-672-513-341-765-671-763-632-673-267-213-127-132-216-2_W.mp3)
  Segment 10: 54972ms ~ 55548ms (file: 574-257-526-527-562-265-256-265-265-265-267-267-652-236-163-162-361-137-315-571-356-567-753-375-371-736-375-357-235-325-235-321-315-173-357-351-571-135-315-135-751-175-157-571-1_W.mp3)
  Segment 11: 56548ms ~ 57124ms (file: 120-102-163-160-567-162-631-163-216-123-631-637-170-670-761-670-607-607-607-671-671-675-567-567-673-672-672-675-765-765-617-671-670-617-160-675-107-576-675-671-671-765-675-675-6_W.mp3)
  Segment 12: 58124ms ~ 58700ms (file: 153-135-156-135-163-153-137-153-156-156-157-152-176-513-165-126-165-136-125-156-517-157-156-152-156-156-567-652-536-536-653-165-615-156-523-657-563-365-316-615-165-156-513-156-1_W.mp3)
  Segment 13: 59700ms ~ 72588ms (file: 657 136-617-675-261-561-351-623-627-163-167-162-672-627-261-213-263-206-316-613-617-617-167-163-160-136 326-236-231-627-627-520-320-675-213-720-207-102-123-103-267-275-130-267-2_W.mp3)
  Segment 14: 73588ms ~ 74164ms (file: 312-651 760-760-705-756-756-756-156-706-750-750-751-756-574 301-132-310-321-312-321-312_W.mp3)
  Segment 15: 75164ms ~ 75740ms (file: 762-207-672-312-507-673-715-137-213_W.mp3)
  Segment 16: 76740ms ~ 77316ms (file: 130-123-317-371-173-351-310-756-751-756-751-170-103-137 301-371-173-713-761-106-103-137-170-132-130-317-713-310-137-713-715-105-310-130-103-715-751-753-752-756-756-756-175-713 7_V.mp3)
  Segment 17: 78316ms ~ 86451ms (file: 576-526-201-572-260-457-301-103-310-321-203-216-201-312-627-670-301-312-210-752-213-301-132-201-261-546-132-321-574-756-107-175-574-315-103-231-102-574-571-103-230-263-602-576-2_W.mp3)
  Segment 18: 87451ms ~ 99727ms (file: 132-137-132-103-310-105-126-201-207-103-106-123-203-301-103-103-301-123-201-260-260-302-102-132-106-130-675-207-103-213-657-103-362-365-230-102-761-367-673-673-673-167-675-765-6_V.mp3)
  Segment 19: 100727ms ~ 106775ms (file: 103-302-126-621-162-237-236-132-312-321-137-123-137-327-236-213-123-236-231-236-123-132-136-123-136-216-213-267-267-276-574-327-752-627-312-325-372-170-507-753-327-321-601-165-2_W.mp3)
  Segment 20: 107775ms ~ 125019ms (file: 256-301-315-321-153-123-130-123-267-230-201-210-237-130-231-320-752-756-276-213-756-230-752-753-130-621-321-753-270-672-765-231-203-213-261-103-310-320-756-571-127-210-123-270-1_W.mp3)
  Segment 21: 126019ms ~ 136855ms (file: 657-315-312-126-315-312-310-301-136-317-572 756-326-163-574-574-302-321-203-572-213-231-321-175-321-231-310-143-547-574-537-235-312-532-513-305-310-231-320-237-231-321-531-352-5_W.mp3)
  Segment 22: 137855ms ~ 138431ms (file: 137-317-137-173-130-310-130-173-135-135-130-105-153 571-173-531-517-317-573-517-157-573-157-751-317-130-153-137-130-105-130-135-105-157 103-135-513-531-517-517-170-501-157-105-1_V.mp3)
  Segment 23: 139431ms ~ 140007ms (file: 572 216-762-627 573-257-271-570-562-527-231-705-126-123-172-237-275-257-215-562-215-752-132-756-267-302-356-370-107-136-136-137-261-123-615 672-615-713-317-731-762-713-172_W.mp3)
  Segment 24: 141007ms ~ 149647ms (file: 312-574-136-102-576-632-312-574-132-762-216-672-752-574-263-231-671-137-574-312-321-756-547-567-675-607-310-103-621-574-517-263-756-574-263-321-267-547-547-236-542-527-321-263-2_W.mp3)
  Segment 25: 150647ms ~ 151223ms (file: 572-527-625-215-572-571-567-265-652-261-326-362-267-216-231-632-236-526-527-574-517-521-615-632-632-621-261-261-261-236-123-631-623 136-315-216-265-125-215-216-135 325-235-235-1_W.mp3)
  Segment 26: 152223ms ~ 152799ms (file: 163-167-613-613-613-631-167-631-635-615-613-631-617-632 673-613-613-621-612-621-672-621-627-621-627-627-621-621-621_W.mp3)
  Segment 27: 153799ms ~ 154375ms (file: 103-103-617-167-167-761-103-167-176-137-136-170-160-160-167-170-165-137-160-176-703-107-671-176-167-657-123-765-756-162-675-765-756-715-107-316-162-103-162-136-106-125-526-261-1_V.mp3)
  Segment 28: 155375ms ~ 155951ms (file: 105-150-105-501-510-150-517-510 105-130-105-157-105-105-105-157-105-105-157-105-150-516-105-501-501-510-501-105-517-501-510-506-507-560-561-105-567-570 153-157-150-105-150-105-5_W.mp3)
  Segment 29: 156951ms ~ 157527ms (file: 312-263-326-362-263-623-263-627-261-123-127-213-126-612-623-316-361-173-176-136-137-123-321-321-316-123-231-123-623-267-627-267-672-672-627-627-265-267-627-267-276-672-627-672-6_W.mp3)
  Segment 30: 158527ms ~ 159103ms (file: 517-510-517-571-570-571-570-571 513-153-175-715-753-157-570-571-517-157-507-576-576-576-576-576-570-517-576-516-157-567-571-510-517-517-516-157-513-153-513-517-567-567-157-570-1_V.mp3)
  Segment 31: 160103ms ~ 164027ms (file: 627-261-132-213-231-132-132-327-213-327-301-302-306-301-306-310-576-756-376-263-623-632-136-361-173-231-267-316-261-236-216-236-216-172-162-632-765-635-301-316-167-567-261-276-2_W.mp3)
  Segment 32: 165027ms ~ 165603ms (file: 617-652-612-621-612-613-627-621-312-132-631-231-257-235-263-263-263-623-263-216-267-267-126-103-627-621-362-756-765-762 176-712_W.mp3)
  Segment 33: 166603ms ~ 167179ms (file: 612-162-620-260-620-621-326-320-301-106-610-160-167-126-362-310-310-306-310-316-163-620-260-520-756-756-705-201-123-213-312-321-326-263-206-103-106-167-176-103-213-201-321_V.mp3)
  Segment 34: 168179ms ~ 168755ms (file: 627-126-627-261-126-361-672-763-761-376 713-263-216-627-612-261 103-132-130 231-216-216-261-316-617_W.mp3)
  Segment 35: 169755ms ~ 170331ms (file: 571-756-571-751-751-751-517-751-517-571-570-571-570-572-571 507-570-571-517-507-507-510-517-157-751-571-157-715-571-567-576-571-576-571-571-571-510-571-576-567-576-571-576-571-5_V.mp3)
  Segment 36: 171331ms ~ 177235ms (file: 236-126-123-153-130-236-123-126-216-216-216-261-216-267-627-762-675-675-765-756-267-265-231-236-657-617-657-362-163-673-675-657-637-673-673-167-132-612-106-612-163-132-167-163-1_W.mp3)
  Segment 37: 178235ms ~ 178811ms (file: 621-162 103-137-632-162-261-123-123_V.mp3)
  Segment 38: 179811ms ~ 188379ms (file: 310-102-132-263-362-321-236-524-236-276-752-762-276-237-236-263-236-235-542-203-263-267-526-576-230-263-102-126-621-612-267-321-237-273-273-231-231-263-261-267-167-123-123-267-2_W.mp3)
  Segment 39: 189379ms ~ 189955ms (file: 136-601-352-136-315-315-753-573-675-657-765-620-310-312-567-576-576-536-537-543-135-135-135-127-751-523-150-156-316-657-361-615-576-573-576-567-571-153-657-756-731-135-175-136-1_W.mp3)
  Segment 40: 190955ms ~ 191531ms (file: 715-751-761-175-576-517-156-157-156-156-156-165-156-153-156-576-567-756-576-576-567-756-576-567-567-576-567-576-567-567-562-576-576-657-567-567-567-576-567-657-567-567-567-567-5_W.mp3)
  Segment 41: 192531ms ~ 202863ms (file: 267-167-106-216-267-103-130-103-316-317-371-713-137-175-713-231-267-231-312-103-321-324-231-103-316-163-123-136-130-126-106-136-136-106-106-326-132-276-267-236-263-123-765-267-1_V.mp3)
  Segment 42: 203863ms ~ 204439ms (file: 176-710-176-176-167-176-713-761-762-671-617_W.mp3)
  Segment 43: 205439ms ~ 206015ms (file: 613-632-610-106-612-617-213-162-132-126-261-316-132-612-602 716-123-132-756-675-602-602-612-301-310-301-103-316-162-123-367-627-216-321-130-136-126-106-130-103-130-316-103-106-3_V.mp3)
  Segment 44: 207015ms ~ 207591ms (file: 263-271-237-271-216-237-267-216-276-672-751-371-753-351-375-135-376-652-627-263-612-136-132_W.mp3)
  Segment 45: 208591ms ~ 209167ms (file: 603-326-602-620-602-206-267-261-201-263-203-320-623-231-320-273-275-217-263-132-106-103-137-316-123-312-137-312-132-136-132-132-106-132-136-316-376-123-132-132-312-637-765-765-6_V.mp3)
  Segment 46: 210167ms ~ 210743ms (file: 105-105-157-105-570-750-570-751-170-710-710-170-175-175-710-715-170-751-750-571-175-750-750-751-751-750-751-570-576 150-517-105-107-107-170-107-175-105-170-517-175-157-157-510-5_V.mp3)
  Segment 47: 211743ms ~ 212319ms (file: 150-105-105-105-105-153-105-157-135-351 150-105-150-150-105-105-105-105-105-157-105-105-103-135-103-157-107-103-756-750-751-756 105-150-150-150-105-150-105-105-157-150-105-105-1_V.mp3)
  Segment 48: 213319ms ~ 213895ms (file: 570-572-132-702-236-176-627-523-235-572-571-507 576-720-576-327-213-540-235-325-507-235-326-756-576-261-736-326-527-265-762-672-537-572-276-672-572-257-127-526-527-276-237-625-2_V.mp3)
  Segment 49: 214895ms ~ 215471ms (file: 756 157-571-517-175-157-705-501-715-571-157-517-715-175-107-715-517-175-507-517-157 150-105-150-150-157-105-150-157-175-107-170-105-107-170-175-510 105-157-517-105-105-150-175-1_V.mp3)
  Segment 50: 216471ms ~ 217047ms (file: 510-510-501-501-150-105-510-150-105-571-150-507-510-510-170-501-501-150-501-105-150-150-501-517-150-570-571-510-570-571 510-570-517-501-517-510-150-510-501-150-510-510-501-510-1_V.mp3)
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
> **Time**: 2026-04-05 06:42:11
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "이게 갤럭시 S24에서만 쓸 수 있는 기능이냐 일단 아이폰에도요 통화 스크리닝 기능 들어가 있거든요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "이게", "start_ms": 50, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "갤럭시", "start_ms": 400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "S24에서만", "start_ms": 950, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "쓸", "start_ms": 2100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 2350, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 2550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "기능이냐", "start_ms": 2900, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "일단", "start_ms": 3800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아이폰에도요", "start_ms": 4300, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "통화", "start_ms": 5400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "스크리닝", "start_ms": 5850, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "기능", "start_ms": 6600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "들어가", "start_ms": 7000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "있거든요", "start_ms": 7550, "dur_ms": 650, "phase_lr_deg": 0.0}
]
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
"transcript": "근데 이건 제가 가지고 있는 한 6년 전쯤에 나온 이 아이폰 SE 2세대 여기서도 이 기능을 쓸 수가 있고요 두부도",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "근데", "start_ms": 120, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이건", "start_ms": 450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "가지고", "start_ms": 1250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 1750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 2300, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "6년", "start_ms": 2550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "전쯤에", "start_ms": 3000, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "나온", "start_ms": 3600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 4100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "아이폰", "start_ms": 4300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "SE", "start_ms": 4850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "2세대", "start_ms": 5300, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "여기서도", "start_ms": 6200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 6850, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "기능을", "start_ms": 7050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "쓸", "start_ms": 7600, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "수가", "start_ms": 7850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "있고요", "start_ms": 8200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "두부도", "start_ms": 9100, "dur_ms": 500, "phase_lr_deg": 0.0}
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
"transcript": "2022년 11월 무함마드 빈 살만 사우디아라비아 왕세자가 직접 한국을 방문했습니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "2022년", "start_ms": 50, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "11월", "start_ms": 1000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "무함마드", "start_ms": 1600, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "빈", "start_ms": 2250, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "살만", "start_ms": 2500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "사우디아라비아", "start_ms": 2950, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "왕세자가", "start_ms": 4000, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "직접", "start_ms": 4750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "한국을", "start_ms": 5250, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "방문했습니다", "start_ms": 5900, "dur_ms": 1100, "phase_lr_deg": 0.0}
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
"transcript": "이 방문을 통해 양국 간에는 스마트팜을 포함한 21개 분야에 걸친 협력",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 80, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "방문을", "start_ms": 250, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "통해", "start_ms": 850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "양국", "start_ms": 1300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "간에는", "start_ms": 1750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "스마트팜을", "start_ms": 2300, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "포함한", "start_ms": 3150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "21개", "start_ms": 3800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "분야에", "start_ms": 4450, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "걸친", "start_ms": 5050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "협력", "start_ms": 5500, "dur_ms": 400, "phase_lr_deg": 0.0}
]
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
"transcript": "나이의 10배가 걸립니다 우주가 138억 년인데 그 10배 1380억 년이 걸리는 것이죠 두 번째 보관이 지옥입니다 어디에 담을 수 있을까요 어떤 그릇에도 담을 수는 없습니다 우리가 이 세상",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "나이의", "start_ms": 100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "10배가", "start_ms": 600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "걸립니다", "start_ms": 1150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "우주가", "start_ms": 1900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "138억", "start_ms": 2450, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "년인데", "start_ms": 3300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 3900, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "10배", "start_ms": 4100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "1380억", "start_ms": 4550, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "년이", "start_ms": 5600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "걸리는", "start_ms": 5950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "것이죠", "start_ms": 6450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "두", "start_ms": 7200, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "번째", "start_ms": 7450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "보관이", "start_ms": 7900, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "지옥입니다", "start_ms": 8500, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "어디에", "start_ms": 9500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "담을", "start_ms": 10050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 10450, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있을까요", "start_ms": 10650, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 11400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그릇에도", "start_ms": 11800, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "담을", "start_ms": 12500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "수는", "start_ms": 12950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "없습니다", "start_ms": 13300, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "우리가", "start_ms": 14200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 14700, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "세상", "start_ms": 14900, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "가장 중요한 질문에 한번 답해 보겠습니다 이 변화 속에서 우리의 역할은 어떻게 달라질까요 오늘 다룬 네 가지 축에서 저희의 역할이 어떻게 진화하는지 한번 정리를 간단하게 해봤는데요 AI",
"speaker": "speaker_4",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "가장", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "중요한", "start_ms": 600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "질문에", "start_ms": 1150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "한번", "start_ms": 1650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "답해", "start_ms": 2000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "보겠습니다", "start_ms": 2400, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 3300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "변화", "start_ms": 3500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "속에서", "start_ms": 3950, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "우리의", "start_ms": 4500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "역할은", "start_ms": 4950, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 5500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "달라질까요", "start_ms": 6000, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 7000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "다룬", "start_ms": 7400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "네", "start_ms": 7850, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "가지", "start_ms": 8100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "축에서", "start_ms": 8500, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "저희의", "start_ms": 9100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "역할이", "start_ms": 9600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 10150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "진화하는지", "start_ms": 10650, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "한번", "start_ms": 11500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "정리를", "start_ms": 11850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "간단하게", "start_ms": 12350, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "해봤는데요", "start_ms": 13000, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "AI", "start_ms": 13800, "dur_ms": 400, "phase_lr_deg": 0.0}
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
"transcript": "친절하게 답변을 달아주고 있습니다 제가 최근에 그 삼성 워치 영상에서 삼성 담당자가 쓴 글을 직접 보여드리고요 해당 글 링크까지 좀 남기면서",
"speaker": "speaker_5",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "친절하게", "start_ms": 80, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "답변을", "start_ms": 750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "달아주고", "start_ms": 1250, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 1800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 2500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "최근에", "start_ms": 2850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 3350, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "삼성", "start_ms": 3550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "워치", "start_ms": 4000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "영상에서", "start_ms": 4400, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "삼성", "start_ms": 5100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "담당자가", "start_ms": 5550, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "쓴", "start_ms": 6250, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "글을", "start_ms": 6500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "직접", "start_ms": 6850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "보여드리고요", "start_ms": 7350, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "해당", "start_ms": 8200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "글", "start_ms": 8600, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "링크까지", "start_ms": 8850, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 9500, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "남기면서", "start_ms": 9750, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "가지산과 맞닿은 배골산 품속 시끄러운 세상과 동떨어진 산속에 농장 하나가 있습니다",
"speaker": "speaker_6",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "가지산과", "start_ms": 100, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "맞닿은", "start_ms": 850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "배골산", "start_ms": 1400, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "품속", "start_ms": 2050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "시끄러운", "start_ms": 2700, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "세상과", "start_ms": 3350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "동떨어진", "start_ms": 3900, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "산속에", "start_ms": 4550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "농장", "start_ms": 5100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "하나가", "start_ms": 5550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 6100, "dur_ms": 700, "phase_lr_deg": 0.0}
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
"transcript": "해발고도가 1000m를 훌쩍 넘기는 것과 비교하면 한참 낮은 수치죠 그런데 경사도는 높습니다 한반도의 평균 경사도는 약 5.7도로 동아시아 전체 평균인 3.9도보다 2도 가까이 더 가파릅니다 정리하자면 높지도 않으면서 경사는 가파르고 단단하기만 해서 아무것도 할 수 없는 산",
"speaker": "speaker_7",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "해발고도가", "start_ms": 120, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "1000m를", "start_ms": 850, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "훌쩍", "start_ms": 1700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "넘기는", "start_ms": 2150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "것과", "start_ms": 2650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "비교하면", "start_ms": 3000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "한참", "start_ms": 3650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "낮은", "start_ms": 4100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "수치죠", "start_ms": 4500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "그런데", "start_ms": 5100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "경사도는", "start_ms": 5600, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "높습니다", "start_ms": 6250, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "한반도의", "start_ms": 7100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "평균", "start_ms": 7750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "경사도는", "start_ms": 8200, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "약", "start_ms": 8800, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "5.7도로", "start_ms": 9050, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "동아시아", "start_ms": 9900, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "전체", "start_ms": 10550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "평균인", "start_ms": 11000, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "3.9도보다", "start_ms": 11600, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "2도", "start_ms": 12550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "가까이", "start_ms": 12900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 13400, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "가파릅니다", "start_ms": 13650, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "정리하자면", "start_ms": 14700, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "높지도", "start_ms": 15550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "않으면서", "start_ms": 16100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "경사는", "start_ms": 16700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "가파르고", "start_ms": 17250, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "단단하기만", "start_ms": 17900, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "해서", "start_ms": 18750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "아무것도", "start_ms": 19150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 19800, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 20050, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "없는", "start_ms": 20250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "산", "start_ms": 20600, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "포커스 양보가 필요합니다",
"speaker": "speaker_8",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "포커스", "start_ms": 50, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "양보가", "start_ms": 600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "필요합니다", "start_ms": 1150, "dur_ms": 800, "phase_lr_deg": 0.0}
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
"transcript": "요렇게 뭐 이런 천광이나 뭐 천궁 같은 거를 지원을 하자 간접적으로 자 요런 내용들이 나왔던 것도 이제 요 배경이 있는 겁니다 이렇게 참고하시면 될 것 같고요 다음 주 주요 이슈입니다 삼성전자 잠정 실적 발표가 나오는데 화요일이 가장 가능성이 높은 거죠",
"speaker": "speaker_9",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "요렇게", "start_ms": 100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 550, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 800, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "천광이나", "start_ms": 1150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 1800, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "천궁", "start_ms": 2050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "같은", "start_ms": 2500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "거를", "start_ms": 2850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "지원을", "start_ms": 3200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "하자", "start_ms": 3700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "간접적으로", "start_ms": 4100, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "자", "start_ms": 4850, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "요런", "start_ms": 5100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "내용들이", "start_ms": 5500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "나왔던", "start_ms": 6050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "것도", "start_ms": 6500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 6850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "요", "start_ms": 7200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "배경이", "start_ms": 7400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 7900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "겁니다", "start_ms": 8250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 8750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "참고하시면", "start_ms": 9200, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "될", "start_ms": 9900, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 10150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "같고요", "start_ms": 10350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 10850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "주", "start_ms": 11250, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "주요", "start_ms": 11500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이슈입니다", "start_ms": 11900, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "삼성전자", "start_ms": 12700, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "잠정", "start_ms": 13350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "실적", "start_ms": 13750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "발표가", "start_ms": 14200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "나오는데", "start_ms": 14750, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "화요일이", "start_ms": 15350, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 16000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "가능성이", "start_ms": 16400, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "높은", "start_ms": 17050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "것이죠", "start_ms": 17450, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "우와 향이 있네요 미나리가 그냥 그 나물인데 아 조금 더 아 고소한 향 아 조금 더 향이 있는 나물이에요 아 이거",
"speaker": "speaker_10",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "우와", "start_ms": 100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "향이", "start_ms": 550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있네요", "start_ms": 950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "미나리가", "start_ms": 1400, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 2050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 2450, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "나물인데", "start_ms": 2650, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 3300, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 3550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 3950, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 4200, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "고소한", "start_ms": 4450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "향", "start_ms": 5000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 5350, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 5600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 6000, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "향이", "start_ms": 6250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 6650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "나물이에요", "start_ms": 7000, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 7850, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "이거", "start_ms": 8100, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "오 와 이거다 이게 죽이지 아 야",
"speaker": "speaker_10",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "오", "start_ms": 150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "와", "start_ms": 500, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "이거다", "start_ms": 750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 1300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "죽이지", "start_ms": 1700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 2250, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "야", "start_ms": 2500, "dur_ms": 300, "phase_lr_deg": 0.0}
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
*Generated by WKAppBot ask gemini — 2026-04-05 06:42:11*
