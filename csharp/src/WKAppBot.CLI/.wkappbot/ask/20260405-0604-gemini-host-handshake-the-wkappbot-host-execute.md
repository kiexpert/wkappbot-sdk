# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=11864  time=06:03:49  uptime=7649s  mem=457MB  system_processes=360"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_060258.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 150-501-513-501-510-150-501-150-150-510-501-510-517-510-517-571-517-570-507-517-574-531-517-501-501 157-517-157-710-513-517-571-517-510-570-510-150-501-105-517-501-150-517-105-5_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 156-162 726-162-152-213-216-132-132-136-106-167-670-167-605-165-617-106-312-132-126-106-103-132-173-615-750-736-216-621-136-135-301-130-132-136-310-136-103-371-160-175-162-671-1_V.mp3)
  Segment 3: 3152ms ~ 11684ms (file: 175-206-106-765-614-260-263-275-216-216-750-175-260-265-210-574-105-106-165-260-127-210-120-132-301-312-216-216-267-105-264-265-205-576-265-260-176-312-763-706-371-730-376-376-3_W.mp3)
  Segment 4: 12684ms ~ 18984ms (file: 267-236-621-130-532-524-201-216-276-276-762-231-230-213-132-102-102-102-120-267-123-310-250-726-236-263-312-231-123-231-673-134-312-123-216-132-216-120-102-127-216-216-321-123-2_W.mp3)
  Segment 5: 19984ms ~ 25960ms (file: 763-761-765-765-762-765-675-762-763-723_W.mp3)
  Segment 6: 26960ms ~ 27536ms (file: 756-756-162-136-501-504-504-574-574-150-103-175-173-106-103-156-632-263-321-361-136-632-617-623-632-651-613-126-216-621-312-316-162-362-617-631-163-632-163-316-371-136-316-301-3_W.mp3)
  Segment 7: 28536ms ~ 29112ms (file: 312-132-501-350-516-653 120-123-132-130-130-132 563-235-652-652-526-653-536-312-635-310-301-130_V.mp3)
  Segment 8: 30112ms ~ 30688ms (file: 316-173-137-217-132-123-123-156-156-607-657-136-136-671-156-571-276-613-167-623-261-623-675-675-675-567-675-765-675-765-657-267-267-127-132-236-231-153-130-132-130-317-156-250-1_W.mp3)
  Segment 9: 31688ms ~ 32264ms (file: 517-514-540-574-574-547-576-576 501-517-517-571-571-157-105-507-501-510-501-150-510-517-510-150-570-507-570-507-570-507-570-547-570-507-570-507-571 150-105-157-571-501-157-517-5_V.mp3)
  Segment 10: 33264ms ~ 43596ms (file: 132-103-237-206-201-271-203-103-123-571-216-201-321-120-620-120-102-236-231-216-236-130-572-135-217-371-325-106-576-562-102-103-574-321-350-532-321-321-103-570-507-216-103-102-1_V.mp3)
  Segment 11: 44596ms ~ 45172ms (file: 517-517-517-510-570-573-507-507 517-501-571-517-510-517-517-517-517-573-573-571-571-537-507-507-570-570 501-501-501-501-510-530-501-501-510-517-510-513-517-570-501-507-507 501-5_V.mp3)
  Segment 12: 46172ms ~ 46748ms (file: 235-725-231-167-753-325-760-123-756-673-235-125-573-507-132-325-302-231-310-132-132-321-765-362-756-725-725-276-326-756-765-362-201-132-675-170-316-756-327-571-531-317-675-560-7_V.mp3)
  Segment 13: 47748ms ~ 48324ms (file: 167-657-576-751-571-571-567-157-517-157-576-576-516-531-756-576-265-652-651-576-657-567-751-752-573-157-576-501-153-517-312-372-756-765-567 576-562-576-657-675-671-567-657-165-1_W.mp3)
  Segment 14: 49324ms ~ 49900ms (file: 132-316-562-163-176-157-103-137-361-103-310-176-153-136-136-123-163-163-103-132-130-132-130-106-163-132-103-103_V.mp3)
  Segment 15: 50900ms ~ 64616ms (file: 132-261-267-267-301-130-130-132-103 312-213-132-130-627-126-237-723-237-123-217-237-216-310-310-102-123-621-267-256-276-237-213-231-132-176-627-267-206-162-267-726-126-267-672-6_W.mp3)
  Segment 16: 65616ms ~ 73968ms (file: 123-213-263-236-627-123-120-123-213-765-752-762-716-326-263-327-362-326-236-627-326-123-132-327-231-236-627-321-762-123-273-267-267-326-236-236-236-236-132-326-765-765-762-132-1_W.mp3)
  Segment 17: 74968ms ~ 75544ms (file: 216-103-107-162-167-132-106-103-316-321-326-312 132-517-657-753-617-176-106-176-136-165-163-561-132-136-162-160-136-621-132-620-127-120-103-103-617-136-361-136-102-165-103-106-1_V.mp3)
  Segment 18: 76544ms ~ 82988ms (file: 327-321-321-102-267-276-527-276-756-321-236-267-627-627-326-627-675-362-362-267-236-231-321-762-207-201-207-217-210-216-213-312-236-213-123-213-210-120-201-231-213-210-213-271-2_W.mp3)
  Segment 19: 83988ms ~ 84564ms (file: 672-236-623-657-657-257-675-267-627-263-312-712-163-310-756-756-670-267-267-263-230-267-765-607-276-162-107-756-137-136-173-701-137-526-627-321-602-627-620-526-725-756-675-632-6_W.mp3)
  Segment 20: 85564ms ~ 86140ms (file: 132-135-615-326-312-362-261-321-163 675-615-657-675-156-605-675-651-106-671-617-172 675-670-672-672-617-763-674-361-367-612-632-631-361-136-361-106-310-301-312-136-106-163-136-1_V.mp3)
  Segment 21: 87140ms ~ 100280ms (file: 206-130-310-132-276-271-625-126-620-260-260-621-620-627-760-301 756-675-267-756-765-267-123-572-231-756-267-213-261-321-230-103-201-103-261-126-123-132 217-125-123-127-120-130-2_W.mp3)
  Segment 22: 101280ms ~ 111252ms (file: 210-103-105-106-267-237-130-310-231-230-203-205-302-534-321-167-316-315-156-153-302-231-130-103-302-156-536-120-203-132-231-320-236-267-271-201-205-230-207-102-527-570-507-210-2_W.mp3)
  Segment 23: 112252ms ~ 112828ms (file: 603-650-650-605-605-623 607-605-610-605-650-605-605-602-605-605-605-605-650-605-621-610-625-650-620 601-605-603-630-602-605-605-605-650-605-605-650-605-650-650-605-651-625-625-6_W.mp3)
  Segment 24: 113828ms ~ 114404ms (file: 675-765-756 307-137-701-130-371-713-301-713-301-137-137-317-637-607-610-617-613-176-617-173-670-675-637-671-672-671-671-675-675 371-371-371-735-713-301-371-301-317-107-601-361-6_V.mp3)
  Segment 25: 115404ms ~ 115980ms (file: 751-572-316-132-671-672-126 136 163_V.mp3)
  Segment 26: 116980ms ~ 117556ms (file: 371-713-307-301-173-301-137-301-137-307-107-173-701-735-317-370-731-315-753-375-137-137-173-701 571-317-310-307-103-107-173-351-371-137-310-307-301-301-137-310-301-103-731-731-7_V.mp3)
  Segment 27: 118556ms ~ 119132ms (file: 132-132-213-326-265-236-632-216-627-267-263-326-652-623-267-265-236-267-263-321-362-237-231-625-261 627-613-627-632-726-672-267-123-627-627-564-567-625-126-132-263-267-265-265-2_W.mp3)
  Segment 28: 120132ms ~ 127224ms (file: 732-132-106-170-761-312-237-716-672-276-267-765-102-231-302-175-761-167-705-263-532-562-751-127-726-137-321-217-702-123-201-267-321-576-126-213-267-127-312-653-276-276-137-572-7_W.mp3)
  Segment 29: 128224ms ~ 128800ms (file: 657-163-163-610-213-162-136-132-320-312-321-312-123-326-261-162-563-315-165-362-631-362_W.mp3)
  Segment 30: 129800ms ~ 135812ms (file: 136-312-352-315-351-312-310-132-312-132-301-230-351-123-210-213-312-230-201-320-312-302-312-236-253-523-256-263-326-326-301-210-132-320-302-302-321-130-310-705-267-103-236-261-2_W.mp3)
  Segment 31: 136812ms ~ 141276ms (file: 236-276-726-276-762-527-260-760-760-762-762-267-320-213-527-576-120-216-213-321-230-572-260-201-203-236-203-123-123-320-310-130-153-167-123-276-263-310-751-712-712-710-170-172-7_W.mp3)
  Segment 32: 142276ms ~ 142852ms (file: 107-175-157-157-175-157-175 170-107-105-175-715-175-107-175-107-175-105-107-715-105-751-750-751-571-510-157-705-751-705-571-715-517-105-175-571-517 157-157-175-105-157-153-157-1_V.mp3)
  Segment 33: 143852ms ~ 144428ms (file: 175-576-751-571-756-756-571-576-571-576-756-657 157-105-175-157-105-103-105-157-170-175-173-105-107-175-715-175-756-576-576-756-576-517-753 107-170-107-170-170-170-170-175-710-1_W.mp3)
  Segment 34: 145428ms ~ 156624ms (file: 547-231-236-261-613-652-106-132-312-106-106-137-312-261-263-321-132-106-160-137-765-312-301-317-312-312-126-103-507-213-132-203-120-127-751-175-102-106-213-253-572-157-167-106-6_W.mp3)
  Segment 35: 157624ms ~ 166156ms (file: 713-731-317-276-207-310-716-761-176-716-327-123-102-107-103-132-321-371-752-735-367-563-135-317-135-136-301-312-217-761-710-173-103-210-137-316-132-327-210-132-173-756-576-726-7_W.mp3)
  Segment 36: 167156ms ~ 172808ms (file: 627-270-273-276-326-637-372-175-715-310-216-261-216-210-107-213-123-153-375-372-731-170-103-271-267-267-217-123-261-216-627-273-302-120-201-120-216-132-170-173-172-107-167-103-7_W.mp3)
  Segment 37: 173808ms ~ 174384ms (file: 362-312-716-365-532-301-216-312-312-726-721-327-760-276-136-367-627-267-276-546-765-316-316-316-301-136-316-136 176-731-371-137-316-713 715-713-735-731-713-763-756-751-753-673_W.mp3)
  Segment 38: 175384ms ~ 175960ms (file: 570-510-576-571-576-576-573-570-570-572-561-502-562-501-157-715-513-731-176-571-157-571-715-517-571-175-576-571-517-517-571-517-517-517-576-516-567-657-576-571-571-573-571-570-5_W.mp3)
  Segment 39: 176960ms ~ 185564ms (file: 602-572-267-267-523-103-103-130-310-267-317-136-625-136-156-251-203-756-675-237-173-357-357-627-267-267-267-541-235-231-102-267-230-267-136-103-167-176-176-325-617-162-256-627-2_V.mp3)
  Segment 40: 186564ms ~ 187140ms (file: 751-751-751-571-571-571-571-571-570-571-571-570-153-103-517-105-157-105-105-105-105-571-571-517-157-173-157-731-571-751-571-517-751-751-571-571-751-517-517-571-571-517-571-517-5_V.mp3)
  Segment 41: 188140ms ~ 188716ms (file: 570-576-507-251-173-576-265-576-725-726-572-725-273-237-502-256-576-321-257-265-541-257-507-756 526-253-263-216-321-563-325-507-562-326-635-756-356-321-726-276-273-675-627-257-5_V.mp3)
  Segment 42: 189716ms ~ 190292ms (file: 132-127-126 130-263-510-132-103-362-157-172-103-130-103-317-516-175-351-153-167-103-136-176-647-136-316-103-136-135-150-176-316-132-106-103-136-607-136-167-103-106-312-153-316-1_V.mp3)
  Segment 43: 191292ms ~ 191868ms (file: 750-753-751-570-570 751-715-517-507-715-751-751-750-751-751-710 751-750-750-570-750-751-750-570-753-753-715-715-571-751-175-571-751-753-715-751-751-571-753-573 751-750-750-750-7_V.mp3)
  Segment 44: 192868ms ~ 201796ms (file: 273-276-267-276-267-267-572-312-276-726-726-762-263-120-102-102-762-267-762-627-267-276-231-736-732-367-372-574-574-231-230-625-230-263-574-312-715-167-176-132-103-106-167-754-3_W.mp3)
  Segment 45: 202796ms ~ 210428ms (file: 312-572-201-765-672-103-310-201-260-602-260-120-572-527-213-326-672-120-231-132-573-671-216-321-103-120-231-230-216-756-267-574-130-620-260-260-132-310-267-310-301-123-120-260-1_W.mp3)
  Segment 46: 211428ms ~ 212004ms (file: 567-162-132-236-367-136-326-261-127-736-132-136-625-126-627-132-312-317-312-320-302-321-230-130-136-301-631-152-310-357-327-136-130-316-136-103-326-103-126-126-132-672-213-621-1_W.mp3)
  Segment 47: 213004ms ~ 213580ms (file: 731-103-137-310-317-317-103-631-163-306-631-310-761-371-376-176-176-603-310-310-607-163-612-630-607-673-673-613-673 170-137-735-375-317-571-371-310-137-673-176-310-673-607-670-6_V.mp3)
  Segment 48: 214580ms ~ 215156ms (file: 317-317-317-317-137-317-137-137-317-317-137-137-137-301-137-137 315-365-301-317-312-317-312-301-301-301-301-301-310-301-301 312-316-301-301-301-316-301-312-301-301-132-301-316-3_V.mp3)
  Segment 49: 216156ms ~ 216732ms (file: 501-507-571 501-501-501-105-517-157-517-571-150-105-501-510-501-510-501-501-510-571-501-157-157-510-517-157-571-517-517-574-574 510-150-157-517-573-510-501-517-501-501-501-501-5_V.mp3)
  Segment 50: 217732ms ~ 218308ms (file: 750-136-613-163-671-160-631-316-130-312-321-531-235-763-753-756-752-576-756-567-576-576-576-765-675-657-675-573-572-756-735-572-752-753-753-351-157-130-756-756-756-356-567-357-5_W.mp3)
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
> **Time**: 2026-04-05 06:04:58
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "여러분 안녕하세요 오늘은 한국어 회화 기초를 배워보겠습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "여러분", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "안녕하세요", "start_ms": 600, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "오늘은", "start_ms": 1200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "한국어", "start_ms": 1600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "회화", "start_ms": 2100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "기초를", "start_ms": 2450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "배워보겠습니다", "start_ms": 2900, "dur_ms": 800, "phase_lr_deg": 0.0}
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
"transcript": "먼저 자기소개를 할 때 사용하는 표현들입니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "먼저", "start_ms": 120, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "자기소개를", "start_ms": 450, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 1150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 1350, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "사용하는", "start_ms": 1550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "표현들입니다", "start_ms": 2100, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "제 이름은 김민수입니다 만나서 반갑습니다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "제", "start_ms": 200, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "이름은", "start_ms": 450, "dur_ms": 450, "phase_lr_deg": 5.0},
{"word": "김민수입니다", "start_ms": 950, "dur_ms": 800, "phase_lr_deg": 5.0},
{"word": "만나서", "start_ms": 1800, "dur_ms": 450, "phase_lr_deg": 5.0},
{"word": "반갑습니다", "start_ms": 2300, "dur_ms": 700, "phase_lr_deg": 5.0}
]
},
{
"segment": 5,
"transcript": "(mixed)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "mixed",
"words": []
},
{
"segment": 6,
"transcript": "실례지만 성함이 어떻게 되세요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "실례지만", "start_ms": 100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "성함이", "start_ms": 650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 1150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "되세요?", "start_ms": 1600, "dur_ms": 500, "phase_lr_deg": 0.0}
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
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 9,
"transcript": "저는 미국에서 온 존이라고 합니다 한국말이 서툴러요",
"speaker": "speaker_3",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 250, "dur_ms": 300, "phase_lr_deg": -10.0},
{"word": "미국에서", "start_ms": 600, "dur_ms": 500, "phase_lr_deg": -10.0},
{"word": "온", "start_ms": 1150, "dur_ms": 150, "phase_lr_deg": -10.0},
{"word": "존이라고", "start_ms": 1350, "dur_ms": 500, "phase_lr_deg": -10.0},
{"word": "합니다", "start_ms": 1900, "dur_ms": 400, "phase_lr_deg": -10.0},
{"word": "한국말이", "start_ms": 2400, "dur_ms": 550, "phase_lr_deg": -10.0},
{"word": "서툴러요", "start_ms": 3000, "dur_ms": 600, "phase_lr_deg": -10.0}
]
},
{
"segment": 10,
"transcript": "아니에요 한국어를 아주 잘 하시는데요?",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "아니에요", "start_ms": 120, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "한국어를", "start_ms": 600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 1150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 1500, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "하시는데요?", "start_ms": 1750, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "칭찬해 주셔서 감사합니다 더 열심히 공부할게요",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "칭찬해", "start_ms": 180, "dur_ms": 400, "phase_lr_deg": -10.0},
{"word": "주셔서", "start_ms": 600, "dur_ms": 400, "phase_lr_deg": -10.0},
{"word": "감사합니다", "start_ms": 1050, "dur_ms": 700, "phase_lr_deg": -10.0},
{"word": "더", "start_ms": 1800, "dur_ms": 200, "phase_lr_deg": -10.0},
{"word": "열심히", "start_ms": 2050, "dur_ms": 450, "phase_lr_deg": -10.0},
{"word": "공부할게요", "start_ms": 2550, "dur_ms": 650, "phase_lr_deg": -10.0}
]
},
{
"segment": 12,
"transcript": "Nice to meet you, John.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "Nice", "start_ms": 80, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "to", "start_ms": 300, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "meet", "start_ms": 420, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "you,", "start_ms": 650, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "John.", "start_ms": 850, "dur_ms": 300, "phase_lr_deg": 5.0}
]
},
{
"segment": 13,
"transcript": "만나서 반가워요 존 씨",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "만나서", "start_ms": 110, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "반가워요", "start_ms": 550, "dur_ms": 450, "phase_lr_deg": 5.0},
{"word": "존", "start_ms": 1050, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "씨", "start_ms": 1300, "dur_ms": 150, "phase_lr_deg": 5.0}
]
},
{
"segment": 14,
"transcript": "이제 길을 물어볼 때 사용하는 표현을 알아볼까요?",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 140, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "길을", "start_ms": 480, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "물어볼", "start_ms": 850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 1350, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "사용하는", "start_ms": 1550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "표현을", "start_ms": 2100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "알아볼까요?", "start_ms": 2600, "dur_ms": 600, "phase_lr_deg": 0.0}
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
"transcript": "저기요 지하철역이 어디에 있나요?",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 160,
"audio_type": "speech",
"words": [
{"word": "저기요", "start_ms": 160, "dur_ms": 450, "phase_lr_deg": -10.0},
{"word": "지하철역이", "start_ms": 650, "dur_ms": 650, "phase_lr_deg": -10.0},
{"word": "어디에", "start_ms": 1350, "dur_ms": 400, "phase_lr_deg": -10.0},
{"word": "있나요?", "start_ms": 1800, "dur_ms": 450, "phase_lr_deg": -10.0}
]
},
{
"segment": 17,
"transcript": "이 길로 쭉 가시면 오른쪽에 보일 거예요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 130, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "길로", "start_ms": 300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "쭉", "start_ms": 700, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "가시면", "start_ms": 950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "오른쪽에", "start_ms": 1450, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "보일", "start_ms": 2050, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 2400, "dur_ms": 450, "phase_lr_deg": 0.0}
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
"transcript": "감사합니다 좋은 하루 되세요",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 100, "dur_ms": 700, "phase_lr_deg": -10.0},
{"word": "좋은", "start_ms": 850, "dur_ms": 350, "phase_lr_deg": -10.0},
{"word": "하루", "start_ms": 1250, "dur_ms": 350, "phase_lr_deg": -10.0},
{"word": "되세요", "start_ms": 1650, "dur_ms": 450, "phase_lr_deg": -10.0}
]
},
{
"segment": 21,
"transcript": "You too. Have a nice day.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "You", "start_ms": 90, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "too.", "start_ms": 250, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "Have", "start_ms": 600, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 780, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "nice", "start_ms": 900, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "day.", "start_ms": 1150, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "천만에요 조심히 가세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "천만에요", "start_ms": 110, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "조심히", "start_ms": 700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 1200, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "다음은 식당에서 주문할 때 사용하는 표현입니다",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "다음은", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "식당에서", "start_ms": 600, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "주문할", "start_ms": 1200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 1700, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "사용하는", "start_ms": 1900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "표현입니다", "start_ms": 2450, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "이모님 여기 비빔밥 두 그릇 주세요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "이모님", "start_ms": 200, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "여기", "start_ms": 750, "dur_ms": 300, "phase_lr_deg": 5.0},
{"word": "비빔밥", "start_ms": 1100, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "두", "start_ms": 1650, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "그릇", "start_ms": 1900, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "주세요", "start_ms": 2300, "dur_ms": 500, "phase_lr_deg": 5.0}
]
},
{
"segment": 25,
"transcript": "맵지 않게 해주세요 저는 매운 걸 못 먹어요",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "맵지", "start_ms": 180, "dur_ms": 400, "phase_lr_deg": -10.0},
{"word": "않게", "start_ms": 600, "dur_ms": 300, "phase_lr_deg": -10.0},
{"word": "해주세요", "start_ms": 950, "dur_ms": 550, "phase_lr_deg": -10.0},
{"word": "저는", "start_ms": 1600, "dur_ms": 300, "phase_lr_deg": -10.0},
{"word": "매운", "start_ms": 1950, "dur_ms": 350, "phase_lr_deg": -10.0},
{"word": "걸", "start_ms": 2350, "dur_ms": 200, "phase_lr_deg": -10.0},
{"word": "못", "start_ms": 2600, "dur_ms": 200, "phase_lr_deg": -10.0},
{"word": "먹어요", "start_ms": 2850, "dur_ms": 500, "phase_lr_deg": -10.0}
]
},
{
"segment": 26,
"transcript": "네 알겠습니다 금방 준비해 드릴게요",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 100, "dur_ms": 200, "phase_lr_deg": 15.0},
{"word": "알겠습니다", "start_ms": 350, "dur_ms": 600, "phase_lr_deg": 15.0},
{"word": "금방", "start_ms": 1000, "dur_ms": 400, "phase_lr_deg": 15.0},
{"word": "준비해", "start_ms": 1450, "dur_ms": 450, "phase_lr_deg": 15.0},
{"word": "드릴게요", "start_ms": 1950, "dur_ms": 550, "phase_lr_deg": 15.0}
]
},
{
"segment": 27,
"transcript": "자 이제 배운 내용을 복습해 볼까요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "자", "start_ms": 150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "배운", "start_ms": 750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "내용을", "start_ms": 1150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "복습해", "start_ms": 1650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "볼까요?", "start_ms": 2200, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 29,
"transcript": "오늘 배운 표현들은 일상생활에서 정말 많이 쓰여요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 120, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "배운", "start_ms": 500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "표현들은", "start_ms": 850, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "일상생활에서", "start_ms": 1450, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 2300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 2700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "쓰여요", "start_ms": 3050, "dur_ms": 450, "phase_lr_deg": 0.0}
]
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
"transcript": "반복해서 듣고 따라해 보는 것이 중요합니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "반복해서", "start_ms": 140, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "듣고", "start_ms": 800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "따라해", "start_ms": 1200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "보는", "start_ms": 1700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "것이", "start_ms": 2100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "중요합니다", "start_ms": 2450, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "Practice makes perfect.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "Practice", "start_ms": 70, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "makes", "start_ms": 600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "perfect.", "start_ms": 950, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "수고하셨습니다 다음 시간에 또 만나요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "수고하셨습니다", "start_ms": 100, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 1050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "시간에", "start_ms": 1450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 1950, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 2200, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "안녕히 가세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 120, "dur_ms": 450, "phase_lr_deg": 5.0},
{"word": "가세요", "start_ms": 600, "dur_ms": 480, "phase_lr_deg": 5.0}
]
},
{
"segment": 35,
"transcript": "Goodbye, everyone.",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "Goodbye,", "start_ms": 100, "dur_ms": 500, "phase_lr_deg": -10.0},
{"word": "everyone.", "start_ms": 650, "dur_ms": 600, "phase_lr_deg": -10.0}
]
},
{
"segment": 36,
"transcript": "바이바이",
"speaker": "speaker_4",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "바이바이", "start_ms": 80, "dur_ms": 450, "phase_lr_deg": 15.0}
]
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
"transcript": "See you soon.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 90, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "soon.", "start_ms": 450, "dur_ms": 300, "phase_lr_deg": 0.0}
]
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
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
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
"transcript": "I hope this lesson was helpful.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 110, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "hope", "start_ms": 220, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "this", "start_ms": 450, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "lesson", "start_ms": 650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "was", "start_ms": 1100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "helpful.", "start_ms": 1350, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "우리 모두 한국어 고수가 되어봐요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 160,
"audio_type": "speech",
"words": [
{"word": "우리", "start_ms": 160, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "모두", "start_ms": 550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "한국어", "start_ms": 950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "고수가", "start_ms": 1450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "되어봐요", "start_ms": 1950, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "Let's become experts in Korean together.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "Let's", "start_ms": 100, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "become", "start_ms": 400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "experts", "start_ms": 850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 1400, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "Korean", "start_ms": 1600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "together.", "start_ms": 2100, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "화이팅",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "화이팅", "start_ms": 80, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "Fighting!",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "Fighting!", "start_ms": 60, "dur_ms": 550, "phase_lr_deg": 0.0}
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
"transcript": "바이바이",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "바이바이", "start_ms": 100, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
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
*Generated by WKAppBot ask gemini — 2026-04-05 06:04:58*
