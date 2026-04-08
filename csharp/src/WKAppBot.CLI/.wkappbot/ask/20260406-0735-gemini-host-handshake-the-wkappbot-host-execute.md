# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=30344  time=07:34:22  uptime=14078s  mem=423MB  system_processes=415"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_073410.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 7416ms (file: 103-126-231-230-103-175-231-231-321-132-216-126-132-372-267-126-317-137-175-321-215-102-123-321-107-275-213-152-217-132-123-310-130-137-301-376-316-754-375-574-325-325-213-103-3_W.mp3)
  Segment 2: 8416ms ~ 14212ms (file: 206-524-267-275-132-302-760-574-321-570-502-542-273-103-201-207-260-720-327-276-752-267-570-765-260-231-230-132-321-123-321-216-103-765-170-317-574-231-213-267-320-130-130-510-2_W.mp3)
  Segment 3: 15212ms ~ 15788ms (file: 576-576-765-675-657-567-671-675-316-631-617-675-657-675-675-675-672_W.mp3)
  Segment 4: 16788ms ~ 25500ms (file: 723-231-106-267-276-267-326-357-130-162-751-321-130-762-326-136-176-132-126-130-276-273-267-273-276-273-762-672-576-573-310-213-137-310-106-321-137-106-570-137-627-267-167-106-3_W.mp3)
  Segment 5: 26500ms ~ 27076ms (file: 301-361-617-617-671-631-136-763-675-671-763-763-671-167-657-637-253-675-765-620-216_W.mp3)
  Segment 6: 28076ms ~ 36211ms (file: 237-527-267-621-573-167-756-106-627-572-372-376-372-107-571-326-753-321-106-167-160-103-612-576-617-621-236-102-267-627-527-376-312-673-321-752-625-325-532-267-237-236-572-376-3_W.mp3)
  Segment 7: 37211ms ~ 37787ms (file: 765-716-256-671-762-625-216-123-570-152-756-725-125-572-156-162-206-126-726-126-507-126-726-627-162-507-657-572-627-135-157-102-301-657-657-657-567-627-361-765-675-675-756 312-2_V.mp3)
  Segment 8: 38787ms ~ 39363ms (file: 312-123-672-756-576-651-651-376-765-375-103-320-576-576-517-315-537-357-375-126-156-571-637-753-531-516-305-135-316-103-735-137-316-371-632-312-130-315-317-361-312-132-301-316-2_V.mp3)
  Segment 9: 40363ms ~ 45907ms (file: 321-130-716-251-157-513-213-210-712-173-103-321-213-103-213-103-236-312-123-327-103-103-170-712-107-107-120-126-276-267-102-362-162-310-103-160-126-276-216-176-201-130-705-230-1_V.mp3)
  Segment 10: 46907ms ~ 47483ms (file: 103-103-175-175-157-157-517-156-517-157-517-516-571-561-517-571-516-576-574-571-517-153-157-135-103-130-135-157-157-157-157-175-157-517-576-517-561-571-517-517-517-571-567-751-5_V.mp3)
  Segment 11: 48483ms ~ 57663ms (file: 210-756-753-103-310-203-312-762-103-756-756-716-756-310-310-310-675-627-167-312-103-103-310-130-132-301-132-756-270-201-765-103-230-230-231-675-130-203-765-132-573-574-276-120-2_W.mp3)
  Segment 12: 58663ms ~ 65971ms (file: 106-103-106-176-657-267-263-102-130-103-163-231-132-163-162-312-106-761-321-326-716-362-735-132-672-752-321-132-126-165-576-157-230-123-627-627-675-627-312-310-102-754-756-756-3_W.mp3)
  Segment 13: 66971ms ~ 67547ms (file: 175-753-751-753-715-753-735 571-571-571-571-571-170-170-715-157-571-175-705-137-307-137-137-715-705-710-371 572-572-572-572-572-572-571-571-571-507-517-751-157-175-173-107-170-7_V.mp3)
  Segment 14: 68547ms ~ 69123ms (file: 573-574 675-765-672-756-357-301-312-301-132-153-537-357-321-375-753-357-137-317-351 136-316-132-132-165-132-321-231-312-263-632-621-216-657_W.mp3)
  Segment 15: 70123ms ~ 70699ms (file: 156-137-175-175-176-167-165-567-576-571-125-162-267-567-165-561-156-157-576-567-675-125-125-276-715-157-127-165-126-176-176-120-167-615-567-576-652-165-152-125-127-651-572-157-1_W.mp3)
  Segment 16: 71699ms ~ 72275ms (file: 672-672-675-157 301-130-261-150-312-132 120_W.mp3)
  Segment 17: 73275ms ~ 81303ms (file: 301-312-321-235-526-301-321-312-103-103-231-256-263-625-163-106-102-103-106-620-627-213-256-302-361-230-361-136-263-103-217-130-675-150-720-321-267-267-207-203-321-206-507-263-3_W.mp3)
  Segment 18: 82303ms ~ 88747ms (file: 302-725-720-602-367-765-657-361-367-632-637-362-361-361-367-756-756-756-765-765-765-762-756-725-756-756-756-276-720-751-735-321-310-372-317-137-136-206-312-310-137-750-173-753-1_W.mp3)
  Segment 19: 89747ms ~ 97703ms (file: 261-132-312-130-310-307-126-132-306-132-351-316-361-136-317-362-203-105-567-136-625-261-203-567-672-675-507-206-213-316-230-103-370-315-130-756-361-237-310-130-106-106-561-175-3_W.mp3)
  Segment 20: 98703ms ~ 99279ms (file: 152-135-513-317-157-715-507 153-531-153-531-215-157-521-517-102-370-157-715-153-720-275-710-517-710-135-275-175-127-715-735-257-172-172-713-175-371 153-537-351-157-512-710-715-7_V.mp3)
  Segment 21: 100279ms ~ 110143ms (file: 572-137-752-547-312-317-326-301-132-263-236-657-631-326-123-257-237-123-312-753-567-536-327-321-321-327-325-321-326-316-163-630-362-120-301-123-620-376-306-362-325-237-523-326-3_W.mp3)
  Segment 22: 111143ms ~ 120539ms (file: 201-574-250-260-107-302-103-137-716-217-732-712-731-217-201-715-762-130-310-230-201-203-276-172-173-132-172-712-103-130-103-167-130-213-206-236-230-152-123-162-106-216-132-162-2_V.mp3)
  Segment 23: 121539ms ~ 122115ms (file: 325-527-521-652-257-563-572-570-372-706-132-703-136-752-632-520-267-657-675-627-567-671-653-165-153-561-317-316-137-736-756-761-176-761-371-673-672-751-671-167-673-763-637-673-7_W.mp3)
  Segment 24: 123115ms ~ 123691ms (file: 162-362-127-261-172-167-761-726-627-267-267-267-261-257-237-327-672-761-627-567-760-376-371-376-371-376-317-372-317-317-317-371-312-312-321-312-310-132-756-175-751-675-673-726-6_W.mp3)
  Segment 25: 124691ms ~ 125267ms (file: 762-507-532-271-570-253-325-321-312-231-230-672-367-267-620-627-632-564-567-502-236-627-236-267-216-527-612-762-267-365-376-756-267-756-571-560-572-236-326 672-627-672-263-237-2_V.mp3)
  Segment 26: 126267ms ~ 134295ms (file: 130-310-637-657-106-130-753-756-705-756-370-203-207-672-263-102-173-651-675-301-653-135-750-175-103-203-216-312-203-210-137-276-572-327-765-120-206-203-201-756-230-310-230-231-2_W.mp3)
  Segment 27: 135295ms ~ 139255ms (file: 756-752-103-152-157-130-130-175-756-276-567-165-165-675-567-275-265-257-267-576-752-275-276-562-576-527-752-572-576-265-567-257-657-265-527-256-256-257-256-652-675-625-123-726-1_W.mp3)
  Segment 28: 140255ms ~ 140831ms (file: 672-672-672-623-623-621-632-623-671-623-627-627-627-627-612-627-672-672-673-672-763-673-763-762-673-673-673-673-763-762-672-673-627-627-627-672-627-672-672-675-627-675-672-625-6_W.mp3)
  Segment 29: 141831ms ~ 142407ms (file: 157-571-517-157-157-517-157-157-571 107-130-103-175-157-571-157-157-517-517-157-517-157-517-517-517-571-517-517-571 107-105-150-103-135-175-175-157-571-517-157-517-157-157-517-5_V.mp3)
  Segment 30: 143407ms ~ 150067ms (file: 216-312-310-236-260-670-213-132-175-230-260-206-320-312-310-765-230-230-175-276-327-130-270-321-273-260-273-327-207-102-163-270-132-172-270-201-321-123-310-136-126-270-321-137-3_V.mp3)
  Segment 31: 151067ms ~ 151643ms (file: 132-103-132-132-132-132-132-312-132-103-312-132-312-132-132-132-132-312-103-132-137-312-132-137-312-135-312-132-132-312-132-132-132-132-132-132-132-103-132-136-132-317-137-132-3_V.mp3)
  Segment 32: 152643ms ~ 153219ms (file: 756-726-732-567-260-137-136-137-176-715-173-753-715-376-371-372-613-130-310-627-216-123-213-267-612-675-643-143-675-570-173-175-173-127-310-654-321-362-312 762-760-762-765-765-7_W.mp3)
  Segment 33: 154219ms ~ 163579ms (file: 312-236-231-103-301-271-762-263-132-213-261-231-762-547-120-574-207-203-206-203-260-207-203-132-730-173-213-672-301-126-123-310-307-263-627-206-267-527-320-720-260-206-206-130-5_V.mp3)
  Segment 34: 164579ms ~ 165155ms (file: 571-516-132-137-276-716-617-163-315-132-261-263-135-267-502-126-126-135-362-517-263-231-325-270-217-325-236-213-507-251-507-157-570-217-325 132-547-572-321-267-576-576-236-716-1_V.mp3)
  Segment 35: 166155ms ~ 166731ms (file: 136-162-137-173-231-572-176-132-612-657-162-216-132-750-137-123-632-567-762-572-621-762-276-267-321-572-725-526-231-367-236-176-517-571-751-125-725-236-576-570-627-675-321-617-6_V.mp3)
  Segment 36: 167731ms ~ 174535ms (file: 163-725-176-321-236-136-627-526-312-210-513-157-236-321-235-312-572-301-765-206-620-260-102-120-230-203-231-320-230-312-120-321-263-207-130-132-230-312-236-723-130-136-567-620-5_W.mp3)
  Segment 37: 175535ms ~ 176111ms (file: 576-125-725-253-257-235-256-564-312-513-132-673-167-576-752-627-302-657-571-573-571-576-567-675-617-167-163-617-176-671-167-167-736-761-731-176-173-763-136-316-376-731-713-736-7_W.mp3)
  Segment 38: 177111ms ~ 177687ms (file: 170-105-701-175-175-710-701-107-705-175-705-705-715-705-751-120-120 175-751-157-715-701-175-175-170-570-750-701-751-705-715-175-750-705-750-701-705-751 132-132-136-106-160-160-1_V.mp3)
  Segment 39: 178687ms ~ 179263ms (file: 705-705-576-567-506-570-765-657-752-756-675-576-567-765-756-765-756-750-765-627-765-675-672-675-576-576-756-754-756-576-756-756-675-675-765-765-756-756-576-750-570-570-750-750-7_V.mp3)
  Segment 40: 180263ms ~ 180839ms (file: 173-137-712-705-730-315-713-107-175-725-753-756-175-721 572-571-574-571-175-517-571-571-572-135-137-135-130-750-135-107-517-735-725-751-317 571-571-571-571-571-173-105-701-503-5_W.mp3)
  Segment 41: 181839ms ~ 182415ms (file: 716-136-671-716-167-167-671-761-716-761-571-756-675-531-153-657-576-567-675-657-165-631-137-351-653-152-153-531-536-567-567-576-563-756-576-567-635-316-571-574-571-573-673-756-7_W.mp3)
  Segment 42: 183415ms ~ 183991ms (file: 315 413-617-237-231-713 576-576-756-576-213-213-362-513-516-567-527-562-357-716-130-321-132-321-302-312-326-316-361-136-136-312-316-751-725-715-765-752-126-173-571-371-136-756-7_W.mp3)
  Segment 43: 184991ms ~ 185567ms (file: 672-731-352-765-756-176-652-276-675-176-267-572-627-705-263-237-273-321-765-720-276-375-732-723-276-762-623-371-675-217-756-507-527-627-762-762-362-356-536-762-752-763-502-376-3_V.mp3)
  Segment 44: 186567ms ~ 187143ms (file: 127-127-570-127-752-572-512-576-750 672-265-572-756-261-261-752-321-126-657-576-276-267-263-527-526-572-706-752-271-256-263-362-267-671-731-756-735-376-726-765-671-675-672-673-7_W.mp3)
  Segment 45: 188143ms ~ 196207ms (file: 130-263-310-326-237-273-260-267-263-107-103-371-321-106-107-136-103-136-103-103-276-312-321-130-267-320-310-302-670-170-167-706-670-267-126-620-602-267-102-130-321-620-260-132-3_W.mp3)
  Segment 46: 197207ms ~ 197783ms (file: 165-675-576-165-576-671-763-671-756-637-361 365-574-156-176-176-765-376-325-256-621-376-503-127-617-761-350-135-607-217-572-315-256-713-276-657-632-263-736-567-657-267-235-204-5_V.mp3)
  Segment 47: 198783ms ~ 199359ms (file: 530-315-571-571-135-173-765-761-173-175-137-706-103-730-572-312-756-673-721-216-123-621-276-732-321-632-715-102-716-167-750-265-123-236-652-562-627-237-126-236-253-725-517-126-7_V.mp3)
  Segment 48: 200359ms ~ 200935ms (file: 372-275-756-572-251-765-321-573-321-316-236-715-127-675-231-517-352-527-167-531-327-127-237-574-315-206-627-570-537-536-503-615-675-157-317-357-573-175-675-167-756-617-761-716-6_W.mp3)
  Segment 49: 201935ms ~ 207083ms (file: 230-321-276-572-257-230-231-712-260-130-527-302-301-103-123-237-267-132-210-276-201-172-651-576-675-576-756-756-675-657-750-607-765-176-175-327-675-670-102-320-574-275-132-275-2_W.mp3)
  Segment 50: 208083ms ~ 218523ms (file: 627-705-237-230-376-367-132-703-621-610-672-327-534-261-236-270-276-673-326-327-731-107-756-725-671-670-754-574-162-573-132-352-130-136-130-103-137-316-327-103-126-371-132-321-1_W.mp3)
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
> **Time**: 2026-04-06 07:35:31
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "그래서 막 가족이랑도 자주 오고 친구들이랑도 모임 하면 자주 오고 진짜 자주 와요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "그래서", "start_ms": 10, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "막", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가족이랑도", "start_ms": 580, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "자주", "start_ms": 1290, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "오고", "start_ms": 1600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "친구들이랑도", "start_ms": 1960, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "모임", "start_ms": 2770, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "하면", "start_ms": 3130, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "자주", "start_ms": 3440, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "오고", "start_ms": 3800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 4160, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "자주", "start_ms": 4520, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "와요", "start_ms": 4880, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "근데 조금 뭔가 이거 보면서 맥주 좀 마시면 아 그러게요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "근데", "start_ms": 20, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 380, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "뭔가", "start_ms": 740, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이거", "start_ms": 1100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "보면서", "start_ms": 1410, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "맥주", "start_ms": 1820, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 2180, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "마시면", "start_ms": 2390, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 2850, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "그러게요", "start_ms": 3010, "dur_ms": 500, "phase_lr_deg": 0.0}
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
"transcript": "펭귄 마을이 2014년부터 운영 중인데 매년 매출 신기록을 쓰고 있다고 할 정도죠 이것은 한류가 퍼지기 이전부터 전체 손님",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "펭귄", "start_ms": 40, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "마을이", "start_ms": 450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "2014년부터", "start_ms": 960, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "운영", "start_ms": 1870, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "중인데", "start_ms": 2230, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "매년", "start_ms": 2640, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "매출", "start_ms": 3000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "신기록을", "start_ms": 3360, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "쓰고", "start_ms": 3920, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "있다고", "start_ms": 4230, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 4640, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "정도죠", "start_ms": 4850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이것은", "start_ms": 5310, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "한류가", "start_ms": 5770, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "퍼지기", "start_ms": 6230, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이전부터", "start_ms": 6690, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "전체", "start_ms": 7300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "손님", "start_ms": 7660, "dur_ms": 400, "phase_lr_deg": 0.0}
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
"transcript": "있는 상황에서 미국의 확장 억제력에 기대고 있던 한국과 일본의 믿음이 흔들리기 시작했다는 겁니다 제프리 마우스는 특히 성주 사드 반출 문제를 주목했는데 이는 미국이 여러 전선",
"speaker": "speaker_4",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "있는", "start_ms": 30, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "상황에서", "start_ms": 340, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "미국의", "start_ms": 950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "확장", "start_ms": 1360, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "억제력에", "start_ms": 1720, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "기대고", "start_ms": 2280, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "있던", "start_ms": 2690, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "한국과", "start_ms": 3000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "일본의", "start_ms": 3460, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "믿음이", "start_ms": 3870, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "흔들리기", "start_ms": 4280, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "시작했다는", "start_ms": 4840, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "겁니다", "start_ms": 5500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "제프리", "start_ms": 5910, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "마우스는", "start_ms": 6370, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "특히", "start_ms": 6880, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "성주", "start_ms": 7240, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "사드", "start_ms": 7600, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "해 보면서 우선 모델 측면에서 구글의 제미나이 프로가 어떤 점이 특징적인지 그리고 애플이 MLX를 토대로 해 가지고 애플 디바이스에 최적화를 해 가지고 AI 모델을 굉장히 빠르게 지원할 수",
"speaker": "speaker_5",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "해", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "보면서", "start_ms": 180, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "우선", "start_ms": 590, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "모델", "start_ms": 950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "측면에서", "start_ms": 1310, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "구글의", "start_ms": 1870, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "제미나이", "start_ms": 2280, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "프로가", "start_ms": 2790, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 3250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "점이", "start_ms": 3560, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "특징적인지", "start_ms": 3870, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 4580, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "오 맥주 여기 또 엄청 유명하잖아 나 맥주 좋아해요 뭐 가끔씩 먹지 어 가끔씩",
"speaker": "speaker_6",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "오", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "맥주", "start_ms": 210, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "여기", "start_ms": 570, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 880, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "엄청", "start_ms": 1040, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "유명하잖아", "start_ms": 1400, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "정치적 기반이라고 강조하며 대만이 정치적 조작을 하고 있다고 비판했다 또한 중국 국무원 대만사무판공실의 주펑롄 대변인은 대만은 중국의 일부라며 하나의 중국 원칙이 국제 사회에서 널리 인정된 기본 규범이라고 주장",
"speaker": "speaker_7",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "정치적", "start_ms": 40, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "기반이라고", "start_ms": 500, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "강조하며", "start_ms": 1160, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "대만이", "start_ms": 1720, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "정치적", "start_ms": 2130, "dur_ms": 450, "phase_lr_deg": 0.0}
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
"transcript": "정도 진행되고 나면 현대차가 한국에서 휴머노이드 로봇을 자체적으로 생산하는 것은 기술적으로는 충분히 가능한 시나리오로 볼 수 있습니다 일단 핵심 부품은 계열사를 통",
"speaker": "speaker_8",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "정도", "start_ms": 30, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "진행되고", "start_ms": 340, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "나면", "start_ms": 900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "현대차가", "start_ms": 1210, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "우리 아버지 아들 딸 오 뭐야 뭐야 야 텐션 봐라 텐션 봐라 광주 잘 알 거 같아 이제 텐션 봐라 어우 오케이 오케이 좋아 오 홍보",
"speaker": "speaker_9",
"confidence": 0.91,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "우리", "start_ms": 50, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "아버지", "start_ms": 360, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "오게 되는 거 같아요 이제 기계 손을 타지 않는 사람의 글을 보면서 그 사람의 철학과 그 사람들의 생각",
"speaker": "speaker_10",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "오게", "start_ms": 20, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "되는", "start_ms": 330, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 640, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 800, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "술 한잔 부탁해요 설마 아무 생각이 없었다 그냥 그냥 사람마다 다르다 뭐 어떤 사람 저쪽 좋 좋아 어떤 사람 저쪽 좋아 상관없대",
"speaker": "speaker_11",
"confidence": 0.89,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "술", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "한잔", "start_ms": 250, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "안 나누는데 이렇게 많이 줘 여러 개서 이거를 살려고 하면 25유로 25유로 더 비싸 아 근데 진짜 이거 오버가 아니라 25유로 했어 진짜로 조금 저렴하면 25유로 어",
"speaker": "speaker_12",
"confidence": 0.90,
"language": "mixed",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "안", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "집이라고요 아무리 봐도 집이 아닌데 이거 창고 아니야 집이 아닌 거 같아 아 소장님 어 어 오셨어요 네 네 네 두 분 이리 오세요 어서 오세요",
"speaker": "speaker_13",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "집이라고요", "start_ms": 50, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "비판하고 보다 실질적인 대응이 필요하다고 주장했다 집권당인 민진당 의원들은 대체로 정부의 대응을 지지하면서도 외교적 민감성을 고려해 신중하게 접근해야 한다고 강조했다 왕딩이",
"speaker": "speaker_14",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "비판하고", "start_ms": 30, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "훨씬 맛있겠다",
"speaker": "speaker_15",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "훨씬", "start_ms": 40, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "맛있겠다", "start_ms": 450, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "애기 안 나오는 거야 그러니까 지금 브라부스가 여기 있는 이유가 자궁 입장이 열려 있는 상태라서 애가 언제 나올지 모릅니다 그래서 나 여기서",
"speaker": "speaker_16",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "애기", "start_ms": 30, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "학생수가 많다 필요한 조치라고 생각한다고 밝힌 장미셸 블랑케르 교육부 장관은 물론이고 마크롱 대통령까지 여자아이와 남자아이 모두를 위해 학교에서는 요구되는 단정한 옷차림을 입었으면 한다고 발언한 바 있죠 또한 여",
"speaker": "speaker_17",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "학생수가", "start_ms": 20, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "400발이 넘는 미사일과 수백 대의 드론을 발사했습니다 여기에 레바논의 헤즈볼라까지 가세하면서 이스라엘의 방공 부담은 더욱 커지",
"speaker": "speaker_18",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "400발이", "start_ms": 40, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "감사합니다 그 밑에 맞아요 여기 여기 광주 딸 광주에서 그 남편이랑 같이 잘 살고 있어요 비 지마이 가리나 응 비 지",
"speaker": "speaker_19",
"confidence": 0.91,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 30, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "매출은 1억 5천만 원에 육박한다 그동안 수차례 창업을 하고 또 문을",
"speaker": "speaker_20",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "매출은", "start_ms": 40, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "이 모델을 돌려보면은 아무래도 조금 더 느린 거 보이십니까 S 모델이기 때문에 액티베이션도 전체가 다 되는 구조죠 그래서 초당 15토큰 정도 이렇게 낮아지는 걸 볼 수가 있습니다 성능 요가 좀 더 좋습니다 근데 사실 이렇게 단발성으로 하는 거는 M4 Pro나 M5 Pro나 그렇게 그",
"speaker": "speaker_21",
"confidence": 0.94,
"language": "mixed",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 07:35:31*
