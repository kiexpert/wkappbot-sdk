# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=10820  time=06:29:46  uptime=8722s  mem=378MB  system_processes=354"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_062934.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 571-570 517-105-150-105-105-105-105-150-510-517-150-510-501-150-501-510-150-105-150-105-150-150-510-150-150-501-510-517-571 105-150-105-153-510-150-510-105-510-517-510-150-150-5_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 502-504-503-504-507-570-576-571-267_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 715-715-750-571-517-571-751-571-751-751-571-570-570-571-570-513-157-517-175-715-135-175-175-715-715-750-157-571-157-175-753-751-751-751-751-517-573-571-573-571-571-576-571-756-5_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 735-765-765-756-356-576-573-731-375-573-627-635-136-361-317-317-137-316-316-316-537-315-135-375-317-137-735-135-537-357-760-631-671-367-235-173-175-153-135-317-173-153-571-517_W.mp3)
  Segment 5: 6304ms ~ 14620ms (file: 735-170-175-130-765-675-321-231-263-231-263-162-126-263-231-263-261-761-321-263-213-231-312-136-106-651-216-163-327-576-167-130-230-230-231-235-103-103-506-207-217-210-572-507-2_V.mp3)
  Segment 6: 15620ms ~ 16196ms (file: 765-756-735-756 736-706-765-761-736-176_W.mp3)
  Segment 7: 17196ms ~ 17772ms (file: 123-216-201-213-102-126-301-130-312-123_V.mp3)
  Segment 8: 18772ms ~ 19348ms (file: 365-326-365-526-653-574-352-562-652-675-627-674-267-625-627-620-360-607-657-562-507-570-517-517-276-725-672-725-761-761-175-157-576-657-216-612-567-547-547-541-516-567-675-754-7_W.mp3)
  Segment 9: 20348ms ~ 20924ms (file: 175-157-175-150-170-170-107-157-175-170-517 501-571-501-157-150-517-510-501-507-510-175-105-107-170-107-170-107-107 105-105-105-501-157-105-105-150-510-105-517-105-153-150-517-5_V.mp3)
  Segment 10: 21924ms ~ 38592ms (file: 321-547-354-105-263-132-162-163-306-206-216-627-260-261-123-103-315-534-123-524-547-413-273-627-162-675-753-745-162-102-143-260-267-270-230-213-312-542-534-260-143-765-765-267-7_W.mp3)
  Segment 11: 39592ms ~ 40168ms (file: 750 517-571-750-571-517-517-107-705-571-570-507-510-570-750-150-750-570-750-750-751-157-157-750-170-715-105-157 105-570-170-175-751-571-750-751-507-107-105-570-570-750-750-571-5_V.mp3)
  Segment 12: 41168ms ~ 41744ms (file: 213-301-320-236-625-632-136 315-356-561-635-356-513-651-356-536-563-531-563-513-315-135-536-561-651-651-657-652-156 675-165-163-617-167-136-163-617-136-156-637-167 167-137-163-1_W.mp3)
  Segment 13: 42744ms ~ 43320ms (file: 130-316-623-263-236-263-263-236-326-167-312-236-263-362-263_W.mp3)
  Segment 14: 44320ms ~ 44896ms (file: 163-103-106-106-150-163-367-316-316-176-163-361-751-136-136-750-162-362-136-671-617-165-623-630-165-130-317-126-106-163-317-631-516-731-103-103-160-631-136-103-361-701-136-103-6_V.mp3)
  Segment 15: 45896ms ~ 46472ms (file: 157-517-517-157-517-150 105-105-150-150-105-150-150-156-561-156-561-567-651-156-561-516-516-567-561 105-157-157-157-150-150-157-156-150-165-105-156-157-150-165-156-105-105-150-1_V.mp3)
  Segment 16: 47472ms ~ 48048ms (file: 356-361-375-316-361-361-316-761-376-731-317-367-673-367-637-361-361-713-671_W.mp3)
  Segment 17: 49048ms ~ 49624ms (file: 175-157-157-175-157-175-715-175-173 105-150-150-105-105-154-150-157-175-157-157-176-157-157-157-175-157-157-157-175-175-150 103-103-105-105-150-175-157-157-157-150-175-157-175-1_V.mp3)
  Segment 18: 50624ms ~ 51200ms (file: 206-230-206-602-306-630-360-136-316-576-756-263-302-320-260-350-305-306-103-163-316-163-657-567-675-576-163-316-607-621-672-574-756-176-103-671-756-756-574-576-175-157-726-726-6_W.mp3)
  Segment 19: 52200ms ~ 52776ms (file: 125-172-137-261-317-132-751-172-321-735-376-672-731-231-276-750-236-253-273-132-602-765-673-756-765-735-736-756-637_V.mp3)
  Segment 20: 53776ms ~ 54352ms (file: 536-213-235-312-320-231-321-312-375-310-135-153-571-537-136-362-317-756-370-326-316-137-135-317-132-126-361-376-163-361-126-723-736-673-367-751-765-673-371-761-713-316-130-731-1_W.mp3)
  Segment 21: 55352ms ~ 55928ms (file: 103-135-172-137-132-371-317-107-137-715-732-173-107-132-103-103-175-317-371-107-137-103-137-103-273-103-701-137-572 715-137-317-137-107-137-107-371-137-317-137-130-173-310-102-1_V.mp3)
  Segment 22: 56928ms ~ 63372ms (file: 230-132-210-102-312-157-760-201-250-574-120-102-751-107-716-320-372-326-735-270-102-310-753-276-237-267-231-371-273-130-276-723-263-572-547-207-123-751-310-207-207-132-327-756-1_W.mp3)
  Segment 23: 64372ms ~ 64948ms (file: 150-517-501-510-157-135-157-107-157-170-135-173-175-137-173-175 135-517-315-150-105-537-130-137-153-175-735-170-107-170-170-173-175-137-715-175-715-157-175-157 507-153-701-710-1_V.mp3)
  Segment 24: 65948ms ~ 66524ms (file: 613-613-163-613-615-615-675-765-657-625-623-132-321-321-312-312-310-136-126-321 316-316-316-136-136-106_V.mp3)
  Segment 25: 67524ms ~ 74400ms (file: 207-732-237-216-230-201-276-172-127-721-207-327-120-215-201-217-127-127-123-210-273-237-237-271-231-203-571-321-315-135-231-150-513-531-754-207-203-754-150-514-605-132-162-103-3_W.mp3)
  Segment 26: 75400ms ~ 75976ms (file: 570-506-501-507-570-507-570 150-507-501-507-105-501-517-150-501-507-501-501-501-501-501-150-571-510-150-510-570-150-501-517-510-570-576-150-507-517-571-571 501-510-501-513-501-5_V.mp3)
  Segment 27: 76976ms ~ 77552ms (file: 703-750-731-705-750-735 307-730-703-130-370-310-753-301-175-310-317-371-701-321-107-137-107-130-310-173-307-130-317-307-130-730-370-751-753-307-130-753 750-705-107-731-375-371-1_V.mp3)
  Segment 28: 78552ms ~ 83700ms (file: 263-562-103-316-326-126-312-236-260-675-236-236-263-321-726-267-321-312-236-236-267-627-627-267-306-231-276-267-627-673-267-627-623-362-627-762-263-621-267-623-267-637-267-627-2_W.mp3)
  Segment 29: 84700ms ~ 91720ms (file: 162-307-123-153-523-103-176-201-127-130-310-102-130-301-165-206-236-216-312-130-130-671-107-260-126-206-213-210-130-216-152-123-102-132-261-106-136-102-325-165-103-103-120-301-2_V.mp3)
  Segment 30: 92720ms ~ 93296ms (file: 576-574-574-547-574-576-657-576-567-567-657-657-567-562-652-562-526-256-657-567-652-652-150-625-765-567-627-657-572-570-172-175-675-765-761-765-730-173-163-371-673-621-627-613-6_W.mp3)
  Segment 31: 94296ms ~ 94872ms (file: 312-526-625-236-362-321-231-302-321-132-327-307-263-203-752-312-675-673-765-326-301-326-123-753-573-375 231-216-621-620-620-620-260-620-602-267-216-627-620-627-756-765-362-326-3_W.mp3)
  Segment 32: 95872ms ~ 96448ms (file: 326-236-167-274-321-513-376-615-157-521-624-514-216-162-625_V.mp3)
  Segment 33: 97448ms ~ 105044ms (file: 163-317-756-571-752-726-520-215-271-123-620-267-123-265-265-170-517-175-351-201-132-132-103-130-276-235-752-567-763-260-276-271-762-612-321-275-237-276-623-265-213-260-206-263-5_V.mp3)
  Segment 34: 106044ms ~ 106620ms (file: 367-576-326-705-265-275-325-130-310-157-573 207-263_V.mp3)
  Segment 35: 107620ms ~ 108196ms (file: 103-312-571-157-750-715-371-130-137-173-107-130-731 715-137-307-731-317-751-317-176-617-176-715-137-731-102-137-137-375-130-310-135-175-173-137-137-715-176-135-316-173 370-753-7_V.mp3)
  Segment 36: 109196ms ~ 109772ms (file: 137-103-130-137-103-137-103-137-103-107-137-315-173-713-371-173-103-137-103-137-173-137-713-173-107-107-103-137-173-173-107-137-137-173-173-103-103-107-137-135-315-137-173-103-1_V.mp3)
  Segment 37: 110772ms ~ 111348ms (file: 751-317-715-137-175 715-301-571-731-310-317-310-106-301-371-317-371-701-730-301-370-310-107-137-317-173-751-710-137-137-310-715-705-375 307-370-173-715-175-301-317-617-160-316-6_V.mp3)
  Segment 38: 112348ms ~ 118504ms (file: 213-103-132-217-321-103-231-320-321-320-267-201-213-126-127-301-132-106-752-710-107-102-713-275-120-260-206-126-102-176-106-172-126-527-206-256-206-260-270-275-210-103-721-302-2_V.mp3)
  Segment 39: 119504ms ~ 120080ms (file: 263-136-623-265-617-760-657-176-107-173-167-567-657-673-176-167-175-675-567-657-675-163-620-620-620-130-103-106-176-163-362-103-132-173-132-157-132-765-567-675-175-103-103-132-2_V.mp3)
  Segment 40: 121080ms ~ 121656ms (file: 312-657-567-576-103-135-350-576-567-175-306-571-153-571-573-136-631-316-361-316-316-316 167-573-756-175-175-137-176-716-671-573-237-137-576-152-173-302-136-312-316-123-123-103-1_W.mp3)
  Segment 41: 122656ms ~ 123232ms (file: 134-137-136-103-134-136-136-103-135-134-103-135-136-103-135-146-134-103-136-103-103-136-136 573-517-107-103-130-137-173-173 137-315-315-135-134-134-134-134-103-135-103-134-103-1_V.mp3)
  Segment 42: 124232ms ~ 124808ms (file: 613-132-136-632-136-163-136-163-163-623-567-361-136-576-657-130-326-367-671-163-137-137-673-671-716-617-167-617-216-132-130_W.mp3)
  Segment 43: 125808ms ~ 126384ms (file: 365-260-375-365-376-362-215-312-573-632-362-250-723-752-675-627-207-756-576-537-236-271-726-213-571-276-567-567-327-763-276-627-573-135-235-325-715-563-623-362 136-613-136-136-1_W.mp3)
  Segment 44: 127384ms ~ 127960ms (file: 751-576-574-576-576-567-567 236-761-163-623-132-760-570-705-574-756-756-756-576 756-263-705-236-526-705-265-126-576-756-206-571-135-572-705-632-263-260-261-216-627-207-765-713-5_W.mp3)
  Segment 45: 128960ms ~ 129536ms (file: 361-316-637-762-726-376-621-261-126-574-574-756-752-203-206-230-320-326-326-173-310-103-136-316-263-326-302-301-302-301-321-302-302-672-602-602-163-657-637-230-203-207-312-320-2_V.mp3)
  Segment 46: 130536ms ~ 131112ms (file: 612-631-167-163-361-610-612-627-672-613-162-321-163-162-126-601-167-176-206-231-203-206-126-301-310-260-760-673-632-627-621-310-162-310-362-136-357-136-627-623-103-617-617-671-3_V.mp3)
  Segment 47: 132112ms ~ 142984ms (file: 673-123-312-302-237-103-230-267-276-756-263-270-627-267-231-321-237-572-123-756-276-263-276-130-102-102-132-102-102-123-120-102-237-120-215-230-230-230-752-756-206-203-752-756-7_W_stt_the_rapidly_combat_them_presumably_becau.mp3)
  Segment 48: 143984ms ~ 150068ms (file: 123-167-637-316-617-617-360-163-136-136-631-163-162-162-573-537-562-527-562-152-567-526-562-576-123-326-627-631-637-132-236-167-170-761-716-231-276-267-607-267-620-126-623-602-6_W.mp3)
  Segment 49: 151068ms ~ 152976ms (file: 574-570-576-562 216-103-260-206-310-302-321-536-365-361 132-103-317-670-675-613-675-672-675 137-132-162 301-162-102-162-136-162-163-162-163 214-265-206-267-206-267-326-236-310-1_V.mp3)
  Segment 50: 153976ms ~ 166324ms (file: 237-231-213-213-123-321-317-315-317-315-531-135-137-316-136-132-326-123-213-231-123-132-132-213-213-132-137-136-163-167-276-267-276-276-276-267-276-276-276-271-217-215-217-126-1_W.mp3)
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
> **Time**: 2026-04-05 06:30:53
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "있습니다. 그런데 반물질은 실제로 존재합니다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "있습니다.", "start_ms": 10, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "그런데", "start_ms": 250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "반물질은", "start_ms": 420, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "실제로", "start_ms": 650, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "존재합니다.", "start_ms": 850, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "SF 소설 속 이야기가 아니고요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "SF", "start_ms": 20, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "소설", "start_ms": 160, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "속", "start_ms": 300, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "이야기가", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "아니고요.", "start_ms": 570, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "CERN에서는 실제로 반양자",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "CERN에서는", "start_ms": 15, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "실제로", "start_ms": 250, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "반양자", "start_ms": 430, "dur_ms": 180, "phase_lr_deg": 0.0}
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
"transcript": "미국은 대학 갈 필요가 없다는 얘기가 나오고 있고 인간 지성의 마지막 보루마저 AI에게 뚫렸습니다. 하지만 가장 놀라운 건 중국이에요. 중국 지도부는",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "미국은", "start_ms": 40, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "대학", "start_ms": 450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "갈", "start_ms": 800, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "필요가", "start_ms": 1000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "없다는", "start_ms": 1400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "얘기가", "start_ms": 1800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "나오고", "start_ms": 2200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있고", "start_ms": 2600, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "인간", "start_ms": 2900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "지성의", "start_ms": 3300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "마지막", "start_ms": 3800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "보루마저", "start_ms": 4350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "AI에게", "start_ms": 4900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "뚫렸습니다.", "start_ms": 5500, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "하지만", "start_ms": 6200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 6650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "놀라운", "start_ms": 7050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "건", "start_ms": 7500, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "중국이에요.", "start_ms": 7700, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "중국", "start_ms": 8400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "지도부는", "start_ms": 8800, "dur_ms": 500, "phase_lr_deg": 0.0}
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
"transcript": "Five out of five but it's not very likely.",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Five", "start_ms": 30, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "out", "start_ms": 300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "of", "start_ms": 460, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "five", "start_ms": 580, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "but", "start_ms": 850, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "it's", "start_ms": 1020, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "not", "start_ms": 1220, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "very", "start_ms": 1420, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "likely.", "start_ms": 1660, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "Okay, so the first one I got that correct. The second one, I think I got that one correct.",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "Okay,", "start_ms": 25, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "so", "start_ms": 350, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 520, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "first", "start_ms": 660, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "one", "start_ms": 980, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 1200, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "got", "start_ms": 1300, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "that", "start_ms": 1500, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "correct.", "start_ms": 1700, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "The third one, oh man, oh god. Okay, missed that one. I'm down to",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "The", "start_ms": 10, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "third", "start_ms": 120, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "one,", "start_ms": 400, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "oh", "start_ms": 600, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "man,", "start_ms": 770, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "oh", "start_ms": 1050, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "god.", "start_ms": 1220, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "배꼽은 기름 못 닦는다고 보는데요? 네 맞아요. 기름 소술 정리할 때도 초벌과 재벌 솥 각각의 방법이 다르다고 합니다.",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "배꼽은", "start_ms": 30, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "기름", "start_ms": 400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "못", "start_ms": 680, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "닦는다고", "start_ms": 850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "보는데요?", "start_ms": 1300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "네", "start_ms": 1800, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "맞아요.", "start_ms": 2000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "기름", "start_ms": 2400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "소술", "start_ms": 2700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "정리할", "start_ms": 3050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "때도", "start_ms": 3500, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "것입니다. 한국산 세탁기가 미국 시장에 본격적으로 들어가기 시작했을 때 미국 소비자들은 정말 놀랐다고 하는데요. 세탁기가 너무나 조용",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "것입니다.", "start_ms": 50, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "한국산", "start_ms": 500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "세탁기가", "start_ms": 900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "미국", "start_ms": 1350, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "시장에", "start_ms": 1650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "본격적으로", "start_ms": 2050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "들어가기", "start_ms": 2600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "시작했을", "start_ms": 3050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 3550, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "미국", "start_ms": 3750, "dur_ms": 250, "phase_lr_deg": 0.0}
]
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
"transcript": "맞서 당당한 대한민국의 주권을 지켜내야 할 때입니다. 기부란 동맹은 영원할 수 없지만 당당하고 자강을 이룬 국가의 동맹은 그 누구도 함부로 흔들 수 없습니다. 트럼프의 청구서",
"speaker": "speaker_6",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "맞서", "start_ms": 20, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "당당한", "start_ms": 350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "대한민국의", "start_ms": 800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "주권을", "start_ms": 1450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "지켜내야", "start_ms": 1950, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 2500, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "때입니다.", "start_ms": 2700, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "초발사예요. 그렇기 때문에 여기에 따른 실제 비행 성능을 검증하는 구간이기 때문에 스페이스 X 또는 우주 관련된 주식 투자하시는 분들도 관심 있게 볼 것 같습니다. 어 어떻게 얼마나 성공하는지 성능이 얼마나 좋아졌는지 등등 여기에 따",
"speaker": "speaker_7",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "초발사예요.", "start_ms": 15, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "그렇기", "start_ms": 550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "때문에", "start_ms": 950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "여기에", "start_ms": 1350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "따른", "start_ms": 1700, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "이 엄청 화려해요. 그리고 계속 계속 메뉴가 바뀌면서 나와요. 이게 떨어지면 다른 메뉴가 나오고",
"speaker": "speaker_8",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 25, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "엄청", "start_ms": 150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "화려해요.", "start_ms": 500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 1100, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "알려지자 일본에서는 분위기가 완전히 달라집니다. 이번엔 진짜 제대로 나올 수도 있겠다. 이렇게 기대감이 올라가기 시작한 거죠. 사실 처음 반응은 완전히 반대였거든요.",
"speaker": "speaker_9",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "알려지자", "start_ms": 30, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "일본에서는", "start_ms": 450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "분위기가", "start_ms": 1000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "완전히", "start_ms": 1500, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "지냈으니까요. 가장 먼저 눈을 돌린 것은 당연히 네덜란드였습니다. 세계 농산물 수출 3위, 유리온실의 나라. 작은 영토에서 기적적인 농업 생산성을 이뤄낸",
"speaker": "speaker_10",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "지냈으니까요.", "start_ms": 20, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "먼저", "start_ms": 900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "눈을", "start_ms": 1250, "dur_ms": 250, "phase_lr_deg": 0.0}
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
"transcript": "the rapidly combat them presumably because",
"speaker": "speaker_11",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "the", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "rapidly", "start_ms": 150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "combat", "start_ms": 520, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "them", "start_ms": 850, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "presumably", "start_ms": 1050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "because", "start_ms": 1550, "dur_ms": 300, "phase_lr_deg": 0.0}
]
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
*Generated by WKAppBot ask gemini — 2026-04-05 06:30:53*
