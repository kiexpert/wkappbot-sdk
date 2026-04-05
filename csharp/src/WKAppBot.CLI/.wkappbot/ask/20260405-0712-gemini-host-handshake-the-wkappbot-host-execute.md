# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=30524  time=07:10:31  uptime=9543s  mem=452MB  system_processes=356"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_071019.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 756-763-762-756-756-756-765-765-756-756-751-673-362-735-137-172-167-167-617-176-371-237-237-731-327-751-756-756-751-756-753-756-752-756-735-752-756-735-756-756-752-756-576-753-7_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 136-103-103-136-103-103-134 571-753-574 134-134-315-145-301-301-316-103-134-135-164-135-136-135-136-136-136-136-103-134-315-136-135-316-136-103-103-136-136 357-573-573-571 301-3_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 751-517-153-157-715-175-157-751-715-173-175-175-175-715-175-175-175-570-715-173-157-573 571-517-751-517-715-157-157-571-571-751-157-173-517-751-713-157-175-571-517-715-157-751-1_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 517-574-570-571-501-570-157-571-751-157-157-170-170-173-172-127-371-371-716-176-761-107-107-127-163-613-163-162-630-361-160-106-106 127-127-137-175-153-157-160-136-716-765-732-7_W.mp3)
  Segment 5: 6304ms ~ 14908ms (file: 102-103-106-261-106-136-236-127-126-136-163-176-613-136-167-167-230-312-276-720-702-713-732-713-132-310-317-236-263-320-321-301-312-312-312-132-302-173-317-132-103-103-602-230-2_W.mp3)
  Segment 6: 15908ms ~ 25520ms (file: 603-263-756-175-267-150-751-107-132-752-216-162-106-675-671-613-167-275-627-213-261-260-267-301-130-106-126-107-627-670-175-270-267-172-102-756-726-123-756-751-201-267-267-762-1_W.mp3)
  Segment 7: 26520ms ~ 34368ms (file: 763-136-574-103-130-302-320-230-573-327-301-273-302-321-302-357-325-231-762-623-321-302-102-570-321-230-725-230-327-206-201-231-576-574-756-312-132-302-206-203-320-231-230-132-2_V.mp3)
  Segment 8: 35368ms ~ 44152ms (file: 132-271-217-312-271-231-103-217-206-276-267-765-631-136-260-153-547-263-763-150-756-752-206-627-201-216-621-132-275-765-216-276-276-327-620-276-652-275-135-217-576-132-123-157-1_V.mp3)
  Segment 9: 45152ms ~ 45728ms (file: 560-263 162-132-167-617-675-672-765-571-175-761-657-637-756-765-715_W.mp3)
  Segment 10: 46728ms ~ 51840ms (file: 627-321-236-217-210-236-627-267-263-172-163-326-267-267-673-216-627-276-276-263-267-261-126-263-263-216-236-267-265-260-672-176-261-230-236-672-263-267-267-267-267-726-672-762-2_W.mp3)
  Segment 11: 52840ms ~ 53416ms (file: 167-612-261-126-160-160-163-612-361-316-103-326-301-302-301-312-321-216-302-320-320-310-706-625-652-762-762-726-762-762-167-163-267-260-260-230-260-203-120-572-521-126-267-263-6_V.mp3)
  Segment 12: 54416ms ~ 54992ms (file: 564-567-564-231-123-132-321-216-216-301-302-213-123-312-132-103-130-103-165-751-572-625-675-675-657-672-376-576-574-576-576-567-547-564-567-574-320-325-312-362-673-675-765-756-5_W.mp3)
  Segment 13: 55992ms ~ 63408ms (file: 126-321-310-123-206-231-672-546-254-132-310-301-120-130-312-132-132-760-756-276-132-215-267-572-256-267-310-130-571-302-132-275-217-213-251-257-670-672-216-132-312-517-257-201-7_W.mp3)
  Segment 14: 64408ms ~ 64984ms (file: 510-517-510-150-150-153-517-150-510-513-510-507-517-517-514 510-153-510-501-510-501-510-510-510-531-531-531-153-513-513-512-513-510-570-571-537-507 510-510-150-153-510-153-150-1_W.mp3)
  Segment 15: 65984ms ~ 66560ms (file: 136-107-271-615-617-316-160-106-657-316-657-615-130-612-162-675-165-132-123-103-613-103_V.mp3)
  Segment 16: 67560ms ~ 68136ms (file: 507-570-570-510-517-570-150-157 105 501-150-105-510-570-507-105-514-105-157-150-157 105-105-105-105-105-105-105-154-510-517-105 507-571-571-517-574-574-175-571-701-715-571-150-1_V.mp3)
  Segment 17: 69136ms ~ 78892ms (file: 310-132-731-316-231-132-732-376-163-103-273-320-613-320-273-130-632-632-162-267-627-273-236-326-236-576-657-276-236-312-236-312-765-320-103-130-236-132-103-326-321-762-237-320-2_W.mp3)
  Segment 18: 79892ms ~ 80468ms (file: 756-751-576-751-571-751-570-570-570-570-507-506-507-570-570-570-576-567-576-571-517-513-501-501-571-501-570-570-570-507-570-510-501-507-517-157-571-517-157-135-751-751-517-517-7_V.mp3)
  Segment 19: 81468ms ~ 82044ms (file: 316-312-231-103-760-671-751-736-130-137-136-216-201-263-261-574-756-671-231-203-206-103-231-320-132-312-675-671-637-603-630-260-652-576-601-602-607-657-167-106-150-106-167-126-2_V.mp3)
  Segment 20: 83044ms ~ 90892ms (file: 176-167-512-736-176-713-570-751-540-357-627-132-257-103-672-671-601-162-176-362-261-561-156-127-176-162-126-361-362-317-136-267-163-726-132-153-137-601-602-503-306-213-213-263-2_V.mp3)
  Segment 21: 91892ms ~ 92468ms (file: 762-603-617-217-316-651-621-761-160-716-765-267-267-267-627-621-736-276-130-751-135-673-327-231-723-257-765-671-761-236-132-317-123-123-632-130-765-763-762-316-765-567-763-261-1_W.mp3)
  Segment 22: 93468ms ~ 104412ms (file: 107-217-120-210-102-612-126-132-102-230-267-263-106-106-106-120-230-231-203-230-236-310-237-237-276-237-275-267-206-102-230-173-270-173-751-573-213-361-136-317-264-106-176-107-1_W.mp3)
  Segment 23: 105412ms ~ 105988ms (file: 706 706-675-127-315-321-271-721-621-726-312-206-261-751-751-157-570-625-231-136-762-137-631-657-507-573-371-356-576-576-136_W.mp3)
  Segment 24: 106988ms ~ 107564ms (file: 176-671-710-176-162-167-156-102-170-756-756-752-263-572_V.mp3)
  Segment 25: 108564ms ~ 115008ms (file: 201-130-207-132-102-107-173-176-135-103-103-501-321-576-206-312-126-176-235-201-152-153-123-213-251-135-127-723-721-312-103-132-627-571-572-231-176-572-573-127-103-627-135-132-5_V.mp3)
  Segment 26: 116008ms ~ 116584ms (file: 312-312 236 130-316-137-736-316-163-136-651-165-106-162-160-160-160-163-163-612-612-615-165-673-672-657-603-367-163-756-657-362 652 236-540 617 653-652-625 603-603-603-651-643-6_W.mp3)
  Segment 27: 117584ms ~ 125288ms (file: 132-176-162-705-263-107-176-517-236-326-172-261-267-762-321-367-627-263-237-123-137-756-167-160-267-276-267-326-321-765-756-675-162-756-267-216-267-236-627-316-652-127-736-231-7_W.mp3)
  Segment 28: 126288ms ~ 137736ms (file: 165-157-702-132-312-670-123-126-312-537-276-251-206-135-617-265-137-123-261-132-317-167-106-106-137-127-217-765-627-267-574-361-763-136-301-153-671-261-123-217 132-132-132-312-2_W.mp3)
  Segment 29: 138736ms ~ 139312ms (file: 716-137-130-657-367-371-107-371 370-701-730-307-753-136-170-301-137-103-310-601-673-710-752-753-753-573-751-753-307-301-137-170-370-130-137-130-103-107-173-170 715-137-107-705-5_V.mp3)
  Segment 30: 140312ms ~ 154244ms (file: 261-172-137-725-571-512-623-215-627-261-261-623-263-127-547-576-716-317-372-275-217-213-217-712-271-716-172-132-723-765-267-236-231-367-312-213-543-527-237-351-267-201-206-206-2_W.mp3)
  Segment 31: 155244ms ~ 155820ms (file: 510-504-514-510-510-150-153-150-150 150-510-517-157-510-517-517-517-510-571-157-153-137-150-175-175-173-175-105-170 105-150-510-157-157-175-157-157-175-715-715-175-157-153-170-1_V.mp3)
  Segment 32: 156820ms ~ 167332ms (file: 135-365-136-167-326-231-657-237-123-261-231-627-627-231-132-231-123-321-231-261-756-231-216-256-276-752-627-132-216-231-231-753-256-216-716-167-136-267-216-162-123-213-271-612-1_W.mp3)
  Segment 33: 168332ms ~ 168908ms (file: 765-765-762-765-671-765-761-716-715-716-763-716-756-762-167-165-175-157-157-135-137-165-163-126-672-725-762-716-765-765-763-716-763-762-763-761-671-617-671-675-617-653-651-567-6_W.mp3)
  Segment 34: 169908ms ~ 170484ms (file: 362-132-132-157-312-172-317-675-567-675-761-673-716-675-763-761-675-367-671-673-613-671 637-617-716-613-671-761-176-136-613-163-763-367-376-361-137-301-163-547-574-631 325-301 5_W.mp3)
  Segment 35: 171484ms ~ 172060ms (file: 617-123-617-103-136-123-126-657-126-163-610-167-165-150-136-160-103-136-671-651-576-756-562-651-165-163-106-516-106-136-316-541-132-561-562-572-120-106-167-631-162-617-613-165-1_V.mp3)
  Segment 36: 173060ms ~ 173636ms (file: 701-170-170-170-126-176-176-167-310-315-765-756-602-652-163-130-126-627-672-617-613_W.mp3)
  Segment 37: 174636ms ~ 181980ms (file: 132-201-527-312-705-216-271-723-765-726-207-706-652-260-206-312-257-267-752-675-236-132-267-576-130-236-102-217-572-257-263-120-271-205-205-250-130-312-572-237-312-761-762-326-2_W.mp3)
  Segment 38: 182980ms ~ 183556ms (file: 756-567-267-576-623-670-762-123-103-132-231-705-576-627-267-273-670-257-263-672-752-235-762-235-765-726-531-570-527-276-572-573-315-132-507-356-572-237-317-675-216-750-261-267-5_V.mp3)
  Segment 39: 184556ms ~ 194132ms (file: 157-276-273-712-723-270-276-213-107-301-231-201-230-267-316-652-607-613-627-721-107-107-210-271-167-164-270-230-231-217-231-130-315-571-713-237-270-237-271-327-107-103-236-231-1_W.mp3)
  Segment 40: 195132ms ~ 195708ms (file: 103-136-163-671-136-123-126-106-167-103-136-157-617-512-130-103-163-103-103-572-162-106-130-672-160-672-675-756-617-671-132-316-673-617-162-132-756-102-756-103-713-172-632 103-1_V.mp3)
  Segment 41: 196708ms ~ 197284ms (file: 607-137-765-761-617-672-765-263-301-160-306-657-675-765-371-576-570-672-675-637-670-673-675-675-673-136-137-375-765-675-765-765_W.mp3)
  Segment 42: 198284ms ~ 198860ms (file: 571-135-175-173-137-176-127-173-731-302 573-105-315-137-317-716-371-372-673-376-673-675-763 163_W.mp3)
  Segment 43: 199860ms ~ 200436ms (file: 573-537-753-157-375-163-756-173-137-170-175-107-175-105-135-150-130-157-150-135-357-537-573-573-753-756 561-567-567-576-570-501-150-510-576-756-751-756-571_W.mp3)
  Segment 44: 201436ms ~ 202012ms (file: 576-675-706-167-671-361-167-167-172-217-750-752-235-725-752-237-261-512-216-215-237-123-527-215-520-215-253-236-216-752-125-713-672-502-751-106-165-265-672-160-750-276-367-137-5_V.mp3)
  Segment 45: 203012ms ~ 203588ms (file: 135-367-302-162-106-130-301-123-162-137-136-267-213-123-210-320-301-312-305-350-350-530-351-602-302-231-321-206-263-302-632-276-236-230-321-203-236-236-203-203-203-320-306-302-2_V.mp3)
  Segment 46: 204588ms ~ 205164ms (file: 576-136-765-761-673-137-357-537-507-576-574 123-312-576-574-576-571-713-173-713-176-713-731-713-731-731-736-751-713-731 672 321_W.mp3)
  Segment 47: 206164ms ~ 206740ms (file: 510-507-105 105-157-105-157-105-513-501-105-507-501-510-517-517-507-510-574-570-501-504-570-507-153-510-150-150-510-517-150-517-507 501-157-105-105-105-510-150-157-517-105-510-1_V.mp3)
  Segment 48: 207740ms ~ 210872ms (file: 672-576-236-267-567-576-712-507-157-236-207 271-275-237-327-623-236-725 571-362-316-361-361-316-316-756-547-574-547-576-567-763-637-632-367-763-637-673-547-576-765-576_W.mp3)
  Segment 49: 211872ms ~ 221268ms (file: 534-571-312-315-152-126-130-214-754-367-613-546-605-760-541-105-132-206-260-261-126-312-135-132-312-102-103-102-510-135-513-206-205-257-235-302-325-263-527-102-126-173-103-527-3_V.mp3)
  Segment 50: 222268ms ~ 222844ms (file: 167-736-253-275-207-326-362-312-213-713-531-312-573-371-375-326-216-317-326-327-750-576-752-527-507-310-301-136-136-317-137-637-137-567-375-356-570-543-761-756-326-267-507-263-5_V.mp3)
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
> **Time**: 2026-04-05 07:12:55
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
"transcript": "내가 어떤 종목을 1년 2년 계속 지켜보고 매매를 하다 보면은",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "내가", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "종목을", "start_ms": 1000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "1년", "start_ms": 1600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "2년", "start_ms": 2100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 2600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "지켜보고", "start_ms": 3100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "매매를", "start_ms": 3800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "하다", "start_ms": 4400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "보면은", "start_ms": 4800, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "그 종목만의 특유의 리듬이 몸에 익어 말로 설명이 되게 어려운데",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "그", "start_ms": 120, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "종목만의", "start_ms": 300, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "특유의", "start_ms": 1000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "리듬이", "start_ms": 1500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "몸에", "start_ms": 2100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "익어", "start_ms": 2500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "말로", "start_ms": 3500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "설명이", "start_ms": 4000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "되게", "start_ms": 4600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "어려운데", "start_ms": 5000, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "어떻게 보면 감각적인 부분인 것 같아 이 친구는 내려가서 확인해보자 하고 내려가려는데",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "어떻게", "start_ms": 200, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "보면", "start_ms": 650, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "감각적인", "start_ms": 1100, "dur_ms": 650, "phase_lr_deg": -5.0},
{"word": "부분인", "start_ms": 1800, "dur_ms": 450, "phase_lr_deg": -5.0},
{"word": "것", "start_ms": 2300, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "같아", "start_ms": 2500, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "이", "start_ms": 3500, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "친구는", "start_ms": 3700, "dur_ms": 450, "phase_lr_deg": -5.0},
{"word": "내려가서", "start_ms": 4300, "dur_ms": 600, "phase_lr_deg": -5.0},
{"word": "확인해보자", "start_ms": 5000, "dur_ms": 800, "phase_lr_deg": -5.0},
{"word": "하고", "start_ms": 5900, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "내려가려는데", "start_ms": 6300, "dur_ms": 900, "phase_lr_deg": -5.0}
]
},
{
"segment": 8,
"transcript": "동물들이 따라오려다가 에잉 황자야 공주야 거기서 뭐해 다 내려오고",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "동물들이", "start_ms": 150, "dur_ms": 600, "phase_lr_deg": -5.0},
{"word": "따라오려다가", "start_ms": 800, "dur_ms": 900, "phase_lr_deg": -5.0},
{"word": "에잉", "start_ms": 2500, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "황자야", "start_ms": 3000, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "공주야", "start_ms": 3600, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "거기서", "start_ms": 4200, "dur_ms": 450, "phase_lr_deg": -5.0},
{"word": "뭐해", "start_ms": 4700, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "다", "start_ms": 5500, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "내려오고", "start_ms": 5800, "dur_ms": 700, "phase_lr_deg": -5.0}
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
"transcript": "외적 아름다움을 향한 투자는 결코 얄팍한 사치나 낭비가 아닙니다 그것은 곧 나의 자존감을 지키기 위한 방어전이자",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "외적", "start_ms": 100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "아름다움을", "start_ms": 500, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "향한", "start_ms": 1200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "투자는", "start_ms": 1600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "결코", "start_ms": 2200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "얄팍한", "start_ms": 2600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "사치나", "start_ms": 3100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "낭비가", "start_ms": 3600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아닙니다", "start_ms": 4100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "그것은", "start_ms": 5000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "곧", "start_ms": 5500, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "나의", "start_ms": 5800, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "자존감을", "start_ms": 6200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "지키기", "start_ms": 6900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "위한", "start_ms": 7400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "방어전이자", "start_ms": 7800, "dur_ms": 800, "phase_lr_deg": 0.0}
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
"transcript": "내 삶의 만족도를 최고조로 끌어올리기 위한 가치 있는 태도입니다 한국에서 사드나가 어떻게 된 줄 아세요",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "내", "start_ms": 150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "삶의", "start_ms": 350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "만족도를", "start_ms": 800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "최고조로", "start_ms": 1500, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "끌어올리기", "start_ms": 2200, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "위한", "start_ms": 3000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "가치", "start_ms": 3400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 3750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "태도입니다", "start_ms": 4100, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "한국에서", "start_ms": 5500, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "사드나가", "start_ms": 6200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 6900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "된", "start_ms": 7400, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "줄", "start_ms": 7650, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "아세요", "start_ms": 7900, "dur_ms": 500, "phase_lr_deg": 0.0}
]
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
"transcript": "진짜 표현 그대로 좋아서 정신 못 차릴 정도였다고 합니다 경복궁에 가서 한복 입고 공주놀이 하고",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 120, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "표현", "start_ms": 600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "그대로", "start_ms": 1100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "좋아서", "start_ms": 1700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "정신", "start_ms": 2300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "못", "start_ms": 2750, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "차릴", "start_ms": 3000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "정도였다고", "start_ms": 3400, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "합니다", "start_ms": 4200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "경복궁에", "start_ms": 5500, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "가서", "start_ms": 6200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "한복", "start_ms": 6700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "입고", "start_ms": 7200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "공주놀이", "start_ms": 7700, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "하고", "start_ms": 8400, "dur_ms": 350, "phase_lr_deg": 0.0}
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
"transcript": "BTS 지민 아버지 카페도 가고 멤버들이 자주 가던 식당도 가고 그 다음 날 튀기는 거예요",
"speaker": "speaker_4",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "BTS", "start_ms": 180, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "지민", "start_ms": 750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아버지", "start_ms": 1200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "카페도", "start_ms": 1800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "가고", "start_ms": 2400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "멤버들이", "start_ms": 3000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "자주", "start_ms": 3700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "가던", "start_ms": 4200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "식당도", "start_ms": 4700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "가고", "start_ms": 5300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 6000, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 6200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "날", "start_ms": 6700, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "튀기는", "start_ms": 7000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 7600, "dur_ms": 450, "phase_lr_deg": 0.0}
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
"transcript": "그럼 밤 새 걸리는 거네요 네 밤 새 튀기는데 그런데 한국에 직접 들어왔다는 걸 안 순간",
"speaker": "speaker_5",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "mixed",
"words": [
{"word": "그럼", "start_ms": 120, "dur_ms": 350, "phase_lr_deg": 10.0},
{"word": "밤", "start_ms": 550, "dur_ms": 250, "phase_lr_deg": 10.0},
{"word": "새", "start_ms": 850, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "걸리는", "start_ms": 1100, "dur_ms": 450, "phase_lr_deg": 10.0},
{"word": "거네요", "start_ms": 1600, "dur_ms": 450, "phase_lr_deg": 10.0},
{"word": "네", "start_ms": 2500, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "밤", "start_ms": 2800, "dur_ms": 250, "phase_lr_deg": 10.0},
{"word": "새", "start_ms": 3100, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "튀기는데", "start_ms": 3400, "dur_ms": 600, "phase_lr_deg": 10.0},
{"word": "그런데", "start_ms": 4500, "dur_ms": 450, "phase_lr_deg": 10.0},
{"word": "한국에", "start_ms": 5100, "dur_ms": 500, "phase_lr_deg": 10.0},
{"word": "직접", "start_ms": 5700, "dur_ms": 400, "phase_lr_deg": 10.0},
{"word": "들어왔다는", "start_ms": 6200, "dur_ms": 700, "phase_lr_deg": 10.0},
{"word": "걸", "start_ms": 7000, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "안", "start_ms": 7300, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "순간", "start_ms": 7600, "dur_ms": 450, "phase_lr_deg": 10.0}
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
"transcript": "분위기가 완전 뒤집혀 버린 거죠 도쿄 갔을 때는 단순 리메이크가 아니라 한국 제작진이 기획과 제작에 직접",
"speaker": "speaker_6",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "분위기가", "start_ms": 150, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "완전", "start_ms": 800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "뒤집혀", "start_ms": 1300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "버린", "start_ms": 1900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "거죠", "start_ms": 2350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "도쿄", "start_ms": 3200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "갔을", "start_ms": 3700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "때는", "start_ms": 4100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "단순", "start_ms": 4600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "리메이크가", "start_ms": 5100, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 5900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 6500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "제작진이", "start_ms": 7000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "기획과", "start_ms": 7700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "제작에", "start_ms": 8300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "직접", "start_ms": 8850, "dur_ms": 400, "phase_lr_deg": 0.0}
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
"transcript": "참여한 기술은 따로 있습니다 첫 번째는 분무수경 즉 에어로포닉스입니다 일반적으로 수경 재배는",
"speaker": "speaker_7",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "참여한", "start_ms": 100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "기술은", "start_ms": 650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "따로", "start_ms": 1200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 1700, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "첫", "start_ms": 2500, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "번째는", "start_ms": 2800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "분무수경", "start_ms": 3400, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "즉", "start_ms": 4200, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "에어로포닉스입니다", "start_ms": 4500, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "일반적으로", "start_ms": 5900, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "수경", "start_ms": 6800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "재배는", "start_ms": 7300, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "식물의 뿌리를 물에 담가 양분을 흡수시킵니다 그런데 에어로포닉스는 뿌리를 아예 공중에 매달아 놓고 그 아래에서",
"speaker": "speaker_7",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "식물의", "start_ms": 150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "뿌리를", "start_ms": 700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "물에", "start_ms": 1250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "담가", "start_ms": 1700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "양분을", "start_ms": 2200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "흡수시킵니다", "start_ms": 2800, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "그런데", "start_ms": 4200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "에어로포닉스는", "start_ms": 4800, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "뿌리를", "start_ms": 5900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "아예", "start_ms": 6450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "공중에", "start_ms": 6900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "매달아", "start_ms": 7500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "놓고", "start_ms": 8100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 8600, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "아래에서", "start_ms": 8850, "dur_ms": 600, "phase_lr_deg": 0.0}
]
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
"transcript": "우야 어 우야 어 어무이 이야 태도라는 게 진짜 중요하다고 생각되거든요 제가 그 이유가 제가 부모님부터",
"speaker": "speaker_8",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "mixed",
"words": [
{"word": "우야", "start_ms": 180, "dur_ms": 300, "phase_lr_deg": -10.0},
{"word": "어", "start_ms": 550, "dur_ms": 200, "phase_lr_deg": -10.0},
{"word": "우야", "start_ms": 800, "dur_ms": 300, "phase_lr_deg": -10.0},
{"word": "어", "start_ms": 1200, "dur_ms": 200, "phase_lr_deg": -10.0},
{"word": "어무이", "start_ms": 1500, "dur_ms": 500, "phase_lr_deg": -10.0},
{"word": "이야", "start_ms": 2200, "dur_ms": 400, "phase_lr_deg": -10.0},
{"word": "태도라는", "start_ms": 3000, "dur_ms": 600, "phase_lr_deg": -10.0},
{"word": "게", "start_ms": 3700, "dur_ms": 150, "phase_lr_deg": -10.0},
{"word": "진짜", "start_ms": 3950, "dur_ms": 400, "phase_lr_deg": -10.0},
{"word": "중요하다고", "start_ms": 4450, "dur_ms": 700, "phase_lr_deg": -10.0},
{"word": "생각되거든요", "start_ms": 5250, "dur_ms": 900, "phase_lr_deg": -10.0},
{"word": "제가", "start_ms": 6500, "dur_ms": 400, "phase_lr_deg": -10.0},
{"word": "그", "start_ms": 7000, "dur_ms": 150, "phase_lr_deg": -10.0},
{"word": "이유가", "start_ms": 7250, "dur_ms": 500, "phase_lr_deg": -10.0},
{"word": "제가", "start_ms": 7850, "dur_ms": 400, "phase_lr_deg": -10.0},
{"word": "부모님부터", "start_ms": 8350, "dur_ms": 800, "phase_lr_deg": -10.0}
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
"transcript": "전업을 하셨어요 그런데 예전부터 계속 보니까 항상 잘 될 때는 잘 되시다가 오 주식이라는 게 항상 잘 될 수만은 없잖아요",
"speaker": "speaker_8",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "전업을", "start_ms": 120, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "하셨어요", "start_ms": 650, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "그런데", "start_ms": 1600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "예전부터", "start_ms": 2150, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 2900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "보니까", "start_ms": 3400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "항상", "start_ms": 4000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 4500, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "될", "start_ms": 4750, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "때는", "start_ms": 5000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 5450, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "되시다가", "start_ms": 5750, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "오", "start_ms": 6800, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "주식이라는", "start_ms": 7100, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 7900, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "항상", "start_ms": 8150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 8700, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "될", "start_ms": 8950, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "수만은", "start_ms": 9200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "없잖아요", "start_ms": 9800, "dur_ms": 600, "phase_lr_deg": 0.0}
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
"transcript": "저도 항상 잘 될 수만은 당연히 없는 거고 그래서 안 좋을 때 양성자와 그 반입자인 반양성자의 성질이 완벽하게 대칭인지",
"speaker": "speaker_9",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "mixed",
"words": [
{"word": "저도", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "항상", "start_ms": 650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 1200, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "될", "start_ms": 1450, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "수만은", "start_ms": 1750, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "당연히", "start_ms": 2400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "없는", "start_ms": 3000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "거고", "start_ms": 3450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 4200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 4800, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "좋을", "start_ms": 5100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 5550, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "양성자와", "start_ms": 6200, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 7000, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "반입자인", "start_ms": 7250, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "반양성자의", "start_ms": 8000, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "성질이", "start_ms": 8950, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "완벽하게", "start_ms": 9600, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "대칭인지", "start_ms": 10400, "dur_ms": 650, "phase_lr_deg": 0.0}
]
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
"transcript": "확인하는 실험을 하는 팀이에요 어제와 오늘 3일선 부근까지 눌렸다가",
"speaker": "speaker_9",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "확인하는", "start_ms": 100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "실험을", "start_ms": 800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 1400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "팀이에요", "start_ms": 1850, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "어제와", "start_ms": 3000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 3600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "3일선", "start_ms": 4200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "부근까지", "start_ms": 4800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "눌렸다가", "start_ms": 5500, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "저점 대비 오늘 15% 이상 상승했습니다 이튿날 이른 아침부터 분주한 모습",
"speaker": "speaker_10",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "저점", "start_ms": 200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "대비", "start_ms": 700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 1200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "15%", "start_ms": 1700, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "이상", "start_ms": 2400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "상승했습니다", "start_ms": 2900, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "이튿날", "start_ms": 4500, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "이른", "start_ms": 5150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "아침부터", "start_ms": 5600, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "분주한", "start_ms": 6400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "모습", "start_ms": 6950, "dur_ms": 400, "phase_lr_deg": 0.0}
]
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
"transcript": "오늘이 바로 팜스테이의 연중 가장 큰 행사라 something down to the people that will come here. It's kind of leaving like a legacy. Yeah, that's correct.",
"speaker": "speaker_11",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "mixed",
"words": [
{"word": "오늘이", "start_ms": 150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "팜스테이의", "start_ms": 1300, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "연중", "start_ms": 2100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 2600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 3100, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "행사라", "start_ms": 3450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "something", "start_ms": 4500, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "down", "start_ms": 5150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 5600, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 5850, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "people", "start_ms": 6100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "that", "start_ms": 6650, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "will", "start_ms": 7000, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "come", "start_ms": 7350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "here.", "start_ms": 7800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "It's", "start_ms": 8500, "dur_ms": 300, "phase_lr_deg": 10.0},
{"word": "kind", "start_ms": 8900, "dur_ms": 300, "phase_lr_deg": 10.0},
{"word": "of", "start_ms": 9300, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "leaving", "start_ms": 9600, "dur_ms": 500, "phase_lr_deg": 10.0}
]
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
"transcript": "Kimchi legacy. 매년 해마다 찾아올 이방인들에게 전하는 작은 마음입니다 어느새",
"speaker": "speaker_12",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "Kimchi", "start_ms": 120, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "legacy.", "start_ms": 700, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "매년", "start_ms": 2000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "해마다", "start_ms": 2550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "찾아올", "start_ms": 3150, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "이방인들에게", "start_ms": 3800, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "전하는", "start_ms": 4800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "작은", "start_ms": 5400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "마음입니다", "start_ms": 5900, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "어느새", "start_ms": 7000, "dur_ms": 500, "phase_lr_deg": 0.0}
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
"transcript": "얼음들이 뭉쳐 있는 빙맥을 만나게 되면 잘 깨지지 않게 돼요 이때 쇄빙선은 이렇게 해요 얼음에 부딪혔다가 뒤로 갔다가 또 다시 전송",
"speaker": "speaker_13",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "얼음들이", "start_ms": 150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "뭉쳐", "start_ms": 850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 1350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "빙맥을", "start_ms": 1750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "만나게", "start_ms": 2350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "되면", "start_ms": 2950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 3500, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "깨지지", "start_ms": 3800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "않게", "start_ms": 4400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "돼요", "start_ms": 4850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이때", "start_ms": 5500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "쇄빙선은", "start_ms": 5950, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 6650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "해요", "start_ms": 7200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "얼음에", "start_ms": 7800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "부딪혔다가", "start_ms": 8350, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "뒤로", "start_ms": 9250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "갔다가", "start_ms": 9700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 10500, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 10800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "전송", "start_ms": 11300, "dur_ms": 450, "phase_lr_deg": 0.0}
]
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
"transcript": "날 존중해주고 나의 안전을 지켜주며 함께 정을 나눌 수 있는 마음의 안식처 같은 곳으로 한국이 기억되고 있습니다",
"speaker": "speaker_14",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "날", "start_ms": 120, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "존중해주고", "start_ms": 400, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "나의", "start_ms": 1250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "안전을", "start_ms": 1700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "지켜주며", "start_ms": 2250, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "함께", "start_ms": 3000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "정을", "start_ms": 3500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "나눌", "start_ms": 3950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 4450, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 4700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "마음의", "start_ms": 5100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "안식처", "start_ms": 5650, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "같은", "start_ms": 6300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "곳으로", "start_ms": 6800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "한국이", "start_ms": 7400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "기억되고", "start_ms": 8000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 8700, "dur_ms": 600, "phase_lr_deg": 0.0}
]
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
"transcript": "이도 되게 궁금하고 어떻게 발견하셨는지도 궁금하고 어 어떻게 그렇게 유연한 사고를 했는지도 되게 궁금합니다 저는 힘들었잖아요",
"speaker": "speaker_15",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "이도", "start_ms": 150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "되게", "start_ms": 550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "궁금하고", "start_ms": 1000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 1800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "발견하셨는지도", "start_ms": 2350, "dur_ms": 950, "phase_lr_deg": 0.0},
{"word": "궁금하고", "start_ms": 3400, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "어", "start_ms": 4200, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 4500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "그렇게", "start_ms": 5100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "유연한", "start_ms": 5650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "사고를", "start_ms": 6250, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "했는지도", "start_ms": 6850, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "되게", "start_ms": 7600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "궁금합니다", "start_ms": 8050, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "저는", "start_ms": 9000, "dur_ms": 350, "phase_lr_deg": 10.0},
{"word": "힘들었잖아요", "start_ms": 9450, "dur_ms": 850, "phase_lr_deg": 10.0}
]
},
{
"segment": 49,
"transcript": "내가 어떻게든 이걸 해내야지만 내가 일어서고 내 가족들하고 다 잘 부양을 할",
"speaker": "speaker_16",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "내가", "start_ms": 120, "dur_ms": 350, "phase_lr_deg": 10.0},
{"word": "어떻게든", "start_ms": 600, "dur_ms": 550, "phase_lr_deg": 10.0},
{"word": "이걸", "start_ms": 1250, "dur_ms": 350, "phase_lr_deg": 10.0},
{"word": "해내야지만", "start_ms": 1700, "dur_ms": 750, "phase_lr_deg": 10.0},
{"word": "내가", "start_ms": 2600, "dur_ms": 350, "phase_lr_deg": 10.0},
{"word": "일어서고", "start_ms": 3050, "dur_ms": 650, "phase_lr_deg": 10.0},
{"word": "내", "start_ms": 3800, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "가족들하고", "start_ms": 4100, "dur_ms": 700, "phase_lr_deg": 10.0},
{"word": "다", "start_ms": 4900, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "잘", "start_ms": 5200, "dur_ms": 250, "phase_lr_deg": 10.0},
{"word": "부양을", "start_ms": 5550, "dur_ms": 500, "phase_lr_deg": 10.0},
{"word": "할", "start_ms": 6150, "dur_ms": 300, "phase_lr_deg": 10.0}
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
*Generated by WKAppBot ask gemini — 2026-04-05 07:12:55*
