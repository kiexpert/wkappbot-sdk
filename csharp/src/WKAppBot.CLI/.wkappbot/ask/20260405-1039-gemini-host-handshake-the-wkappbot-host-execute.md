# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=11716  time=10:37:14  uptime=31691s  mem=396MB  system_processes=359"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_103632.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 6768ms (file: 617-756-312-321-106-321-756-276-237-106-576-627-267-612-135-751-107-157-123-132-123-751-327-567-573-762-275-357-273-267-267-632-106-106-231-276-237-123-321-725-231-132-267-623-2_W.mp3)
  Segment 2: 7768ms ~ 8344ms (file: 170-170-170-751-170-130-170-170-170-107-103-130-173-107-107-107_V.mp3)
  Segment 3: 9344ms ~ 9920ms (file: 106-103-524-127-175-125-103-160-132-106-162-160-123-103-132-136-576-162-162-106-176-163-160-132-106-610-160-627-176-163-106-132-173-350-162-163-317-136-106-103-106-263-176-160-1_V.mp3)
  Segment 4: 10920ms ~ 22944ms (file: 607-130-261-230-317-105-160-156-321-365-321-321-321-213-756-651-753-317-106-310 674-674-267-321-762-620-321-320-103-132-761-760-267-213-762-201-102-372-321-367-321-547-206-302-2_W.mp3)
  Segment 5: 23944ms ~ 24520ms (file: 621-672-657-612-216 543-756-574-571 136-576-621-361-162-136-130-132-132-765-756-573-157-105-627-762-627-672-756-156-165-175-675-672-267-162 521-574-574-547-132-107-361-136-321-1_W.mp3)
  Segment 6: 25520ms ~ 34916ms (file: 321-103-731-310-213-130-276-276-130-263-236-102-123-103-754-675-672-263-127-123-754-273-237-132-260-326-527-256-132-102-203-301-765-260-102-203-273-731-237-572-273-130-231-321-3_W.mp3)
  Segment 7: 35916ms ~ 36492ms (file: 576-756-576-576-756-756-756-132-621-574-574-572-537-275-765-756-756-576-576-576-576-576-576-756-413-534-562-547-517-564-534-567-564 752-752-756-576-576-576-576-576-576-576-576-7_W.mp3)
  Segment 8: 37492ms ~ 38068ms (file: 163-163-136-127-605-765-167-103-167-137-103-137-163-130-301-132-170-103-163-630-162-156-103-132-160-136-106-103-103-312-106-176-167-137-160-126-176-106-127-103-106-126-136-106-1_V.mp3)
  Segment 9: 39068ms ~ 39644ms (file: 175-761-761-716-137-135-136-136-136-163-136-167-137-176-173-716-713-716-716-712-716-716-716-176-172-173-716-716-723-716-176-137-165-167-156-156-157-157-735-715-735-715-715-751-7_W.mp3)
  Segment 10: 40644ms ~ 48708ms (file: 172-175-312-137-137-137-123-271-172-271-132-176-172-547-213-765-120-260-475-735-367-672-210-230-602-675-132-547-172-107-123-157-613-137-132-123-127-175-156-157-175-137-513-157-5_W.mp3)
  Segment 11: 49708ms ~ 50284ms (file: 326-123-321-312-361-376-372-237-372-371-321-326-327-317-371-706-731-761-710-547-354-354-540-547-540-756-672-627-520-574-527-256-524-542-542-542-542-253-231-216-562-546-532-574-3_W.mp3)
  Segment 12: 51284ms ~ 51860ms (file: 673-673 132-163-362-762-621-163-574-576-237-132-123-175-312-301-576-576-576 765_V.mp3)
  Segment 13: 52860ms ~ 60240ms (file: 230-216-263-236-203-216-260-230-231-231-120-123-120-120-102-120-102-143-120-320-320-103-123-215-120-302-201-132-102-102-103-120-725-672-267-167-127-103-123-163-723-263-672-276-2_W.mp3)
  Segment 14: 61240ms ~ 61816ms (file: 621-267-263-132-102-763-760-762-765-762-765-756-765-276-267-263-267-267 267-276-126 671-672_W.mp3)
  Segment 15: 62816ms ~ 63392ms (file: 517-756-756-576-576-576-567-576-756-756-756 105-105-571-157-175-173-573-157-157-170-135-576-571-175-576-571-576-657-657-657-567-576-576 105-157-170-157-751-751-570-701-175-170-1_W.mp3)
  Segment 16: 64392ms ~ 64968ms (file: 730 103-157-107-370-103-317-135-631-601-163-601-607-106-167-163-312-601-603-310-301-107-301-317-173-310-301-713-137-137-137-370-517-371-137-703-705 710-317-173-130-735-713-317-6_V.mp3)
  Segment 17: 65968ms ~ 66544ms (file: 562-567-567-567-576-567-546-507-560-567-574-564-547-567-570-567-564-567-756-561-576-567-507-321-302-315-507-510-650-506-570-576-576-567-576-567-576-574-572-175-576-657-567-567-7_W.mp3)
  Segment 18: 67544ms ~ 77264ms (file: 263-102-326-126-120-260-206-216-231-316-120-267-271-267-267-267-765-627-326-102-612-261-120-263-263-752-126-267-231-263-263-231-321-726-126-263-236-312-267-103-236-236-123-302-7_W.mp3)
  Segment 19: 78264ms ~ 78840ms (file: 175-576-751-316-130-321-167-176-623-263-623-162-621-761-576-765-765-615-176-175-715-715-765-671-672-765-765-176-672-657-671-675-675-761-617-627-213-201-206-216-261-267-261-201-2_W.mp3)
  Segment 20: 79840ms ~ 80416ms (file: 713-312-573-756-375-163-561-357-652-736-356-536-536-765-736-756-573-762-576-756-173-730-361-136-167-167-756-571-157-761-517-152-152-561-526-516-567-576-756-756-751-756-715-513-6_W.mp3)
  Segment 21: 81416ms ~ 81992ms (file: 157-753-175-135-316-315-315-756-765 750-735-705-310-357-173-361-617-106-601-631-607-360-610-603-167-103-170-103-317-715-107-317-307-310-731-371-307-317-370-713-713-310 357-713-7_V.mp3)
  Segment 22: 82992ms ~ 83568ms (file: 651-135-675-657-365-653-652-652-567-576-537-657-657-576-675-675 651-615-165-163-165-615-163-316-613-163-613-165-653-615-653-765-756-675-675-675-765-576-765-756 502-576-526-572-6_W.mp3)
  Segment 23: 84568ms ~ 85144ms (file: 375-173-370-103 153-135-137-317-135-175-132-137-317-135-317-103-310-310-135-301-731-153 135-103-135-137-315-135-157-315-135-137-135-130-257-175-175-137-371-135-130-513-175-375-1_V.mp3)
  Segment 24: 86144ms ~ 91580ms (file: 216-276-263-267-236-263-237-276-273-276-237-267-236-237-236-126-132-276-257-236-213-231-326-326-321-273-231-237-327-312-102-123-231-237-236-237-236-276-236-236-236-237-127-127-1_W.mp3)
  Segment 25: 92580ms ~ 93156ms (file: 163-672-167-160-613-603-326-263-623-632-632-362-621-362-312-136-126-261-126-362-316-126-130-106-106-103-136-316-621-632-123-130-130-106-167-627-617-361-201-132-126-572-162-163-6_W.mp3)
  Segment 26: 94156ms ~ 94732ms (file: 157-156-175-156-571-157-156-516-517-516-517-175-571-517-156-165-756-571-576-175-754-756-754-754-754-754-756-754-754-754-756-754-754-754-754-753-754-754-105-150-150-150-157-517-5_W.mp3)
  Segment 27: 95732ms ~ 96308ms (file: 560-501-150-103-136-316-163-316-326-320-326-103-316-132-123-321-312-136-316-132-106-136-236-321-123-216-162-316-261-102-623-263-217-263-236-267-263-263 137-267-162-263-261-132-1_V.mp3)
  Segment 28: 97308ms ~ 97884ms (file: 372-106-165-614-671-162-632-326-231-621-263-567-632-106-126-136-163-613-163-231-263-126-621-167-162-165-136-106-163-574-165-167-651-675-657-671-516-657-765-165-136-615-103-152-5_V.mp3)
  Segment 29: 98884ms ~ 99460ms (file: 173-175-715-137-173-175-375-157-135-157_W.mp3)
  Segment 30: 100460ms ~ 101036ms (file: 576-567-567-751-675-657-657-657 570-750-571-750-576-567-657-657-651 570-571-570-751-574-574-574-574-576-576-753-571-574-570-574-570-574-574-517 517-501-507-517-510-157-517-517-5_W.mp3)
  Segment 31: 102036ms ~ 102612ms (file: 103-137-701-107-103-317-701-715-173-701-731-130-753-751-301-107-173-371-371 137-103-130-132-107-310-310-310-130-173-103-103-130-103-317-713-137-371-173-310-731-130-175-137-175-1_V.mp3)
  Segment 32: 103612ms ~ 104188ms (file: 175-175-173 105-105-105-105-105-105-105-157-107-175-517-157-176-157-175-571-175-715-715-175-175-173 504-504-501-105-510-150-157-510-175-157-157-175-107-175-157-175-175-175-157-1_V.mp3)
  Segment 33: 105188ms ~ 105764ms (file: 213-213-325-137-321-326-567-657 236-326-136-137-132-162-312-316-715-576-327-132-132-103-670-103 265-136-765-617 765-675 627-327-326-301-312-132-312-132-362-231-123-627-672-567-6_V.mp3)
  Segment 34: 106764ms ~ 108384ms (file: 627-162-751-765-521-357-156 103-130-167-165-176-156-561-162-162-156-157-136-576-465-361-103-136-560-316-756-623-312-623-132-321-312-236-132-132-756-567-105-105-105 326-132-532-3_W.mp3)
  Segment 35: 109384ms ~ 109960ms (file: 105-653-103-165-301-361-316-301-316-103-103-103-130-103-312-132-150-576-756-763-136-162-106-163-130-216-203-102-201-210-321-103-106-230-302-237-237-302-120-102-123-652-675-607-2_V.mp3)
  Segment 36: 110960ms ~ 111536ms (file: 175-715-573-301-173-106-176-137-130-613-307-360-607-675-637-167-617-617-167-175-135-175-156-176-167-137-167-176-137-756 130-317-317-301-307-317-310-310-137-371-301-317-301-301-3_V.mp3)
  Segment 37: 112536ms ~ 113112ms (file: 751-756-751-756-751-751-751-751-756-756-751-754-751-754-751-751 150-153-150-150-175-173-150-751-715-715-175-715-751-175-751-751-751-751-751-751-754-756 157-517-517-157-157-571-7_W.mp3)
  Segment 38: 114112ms ~ 114688ms (file: 570-574-547-572-547-574-547-352-503_W.mp3)
  Segment 39: 115688ms ~ 119684ms (file: 127-310-312-326-326-612-602-123-321-213-326-132-321-213-216-102-107-102-123-126-216-261-216-321-132-132-623-123-612-216-236-136-316-312-216-217-236-321-216-217-263-321-231-126-2_W.mp3)
  Segment 40: 120684ms ~ 127668ms (file: 231-627-236-231-321-231-236-231-103-325-312-532-253-123-312-321-213-752-236-237-312-276-267-276-230-203-675-263-102-231-231-102-120-310-132-310-621-760-102-231-230-103-312-516-7_W.mp3)
  Segment 41: 128668ms ~ 129244ms (file: 751 351-531-567-256-726-765-136-103-310-362-163-312-310-316-132-312-312-320-756-751-571-571-157-103-513-517-502-657-756-571-756-657-517-576-576-765-567-175-571_W.mp3)
  Segment 42: 130244ms ~ 130820ms (file: 157-157-571-751-157-571-751-751-571-751-571 571-517-510-157-715-571_W.mp3)
  Segment 43: 131820ms ~ 132396ms (file: 157-175-157-157-517-175-157-157-105-105-105 105-150-510-150-150-105-105-105-150-157-150-157-175-157-157-157-150-105-170-156-150-175-157 105-105-105-105-510-150-175-107-106-150-1_V.mp3)
  Segment 44: 133396ms ~ 133972ms (file: 765-675-657-756-675-567-756-136-135-135-136-163-156-135-156-153-156-153-135-135-153-762-672-625-765-572-672-576-567-567-756-756-657-765-257-617-652-765-652-576 136-163-152-162-6_W.mp3)
  Segment 45: 134972ms ~ 135548ms (file: 547-547-547-547-547-150-507-510-105-150-170-170-170-103-105-571-756-576-756-756-715-765-357-135-573-751-751-576-751-756-576-756-752-150-501-570-571-571-571-571-517-571-571-571-5_W.mp3)
  Segment 46: 136548ms ~ 137124ms (file: 132-132-102-126-136-126-106-106-156-561-165-125-152-125-152-157-105-102-127-123-761-120-156-156-156-152-512-156-152-156-216-126-123-216-126-106-123-217-123-162-165-165-170-135-1_V.mp3)
  Segment 47: 138124ms ~ 138700ms (file: 751-756 715-571-571-756-175-701-175-175-715-157-107-751-571-570 517-517-510-517 513_W.mp3)
  Segment 48: 139700ms ~ 140276ms (file: 135-105-150-150-510-517-150-156-157-150-157-150-150-106-107-157 105-150-150-150-106-105-150-156-510-150-517-156-157-150 150-105-150-105-150-105-150-150-106-156-156-150-510-157-5_V.mp3)
  Segment 49: 141276ms ~ 141852ms (file: 507-570-570-571-507-570-751 501-571-157-571-571-751-571-517-571-517-571-517 501-571-571-175-571-517-571-510-150-571-571-517-517-501-517-157-501-517-517-571 510-751-517-510-570-7_V.mp3)
  Segment 50: 142852ms ~ 143428ms (file: 137-156-650-163-162-103-563-163-137-103-103-103-167-165-716-176-103-103-163-106-153-175-260-261-127-105-102-103-126-760 203-216-312_V.mp3)
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
> **Time**: 2026-04-05 10:39:15
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "다음의 연속이고 제대로 된 길을 걷고 있는 생각으로 매주 기다려지고",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "다음의", "start_ms": 20, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "연속이고", "start_ms": 130, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "제대로", "start_ms": 260, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "된", "start_ms": 350, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "길을", "start_ms": 380, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "걷고", "start_ms": 430, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 470, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "생각으로", "start_ms": 500, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "매주", "start_ms": 560, "dur_ms": 10, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "이미 수강한 영상을 반복 공부하고 있습니다 그 어느 때보다 열정",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "이미", "start_ms": 10, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "수강한", "start_ms": 60, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "영상을", "start_ms": 130, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "반복", "start_ms": 220, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "공부하고", "start_ms": 290, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 400, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 490, "dur_ms": 10, "phase_lr_deg": 0.0},
{"word": "어느", "start_ms": 510, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "때보다", "start_ms": 540, "dur_ms": 30, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "냄새를 견딜 수 없는 수준에 이르자 농부들은 땅에 심은 감자를 캐보기로 합니다 농부들은 경악을 금치 못했습니다 땅에서 캔 감",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "냄새를", "start_ms": 50, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "견딜", "start_ms": 110, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 160, "dur_ms": 10, "phase_lr_deg": 0.0},
{"word": "없는", "start_ms": 180, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "수준에", "start_ms": 220, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "이르자", "start_ms": 270, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "농부들은", "start_ms": 320, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "땅에", "start_ms": 390, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "심은", "start_ms": 430, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "감자를", "start_ms": 470, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "캐보기로", "start_ms": 530, "dur_ms": 40, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "수익비가 엄청 좋은 게임을 할 수 있는 거기 때문에 음 다섯 번 실패해도 한 번만 성공하면 된다 그래도 수익 난다 그래서 자기가",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "수익비가", "start_ms": 30, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "엄청", "start_ms": 90, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 130, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "게임을", "start_ms": 170, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 220, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 250, "dur_ms": 10, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 270, "dur_ms": 30, "phase_lr_deg": 0.0}
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
"transcript": "세운 투자 시나리오랑 전략을 좀 집중할 수 있는 이런 충동적인 결정을 방지하는 데는 확실히 도움 되지 않을까 그런 생각이 드는 거",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "세운", "start_ms": 20, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "투자", "start_ms": 60, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "시나리오랑", "start_ms": 110, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "전략을", "start_ms": 200, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 260, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "집중할", "start_ms": 290, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "거 근데 이제 우리가 처음에 주식 투자 하다 보면은 대부분 이제 뭔 말인지 몰라가지고 아이 모르겠다 안 하고 그러는 경우가 많거든 근데 생각보다 자기 분야로 좁히다 보면은 이게 막 그렇게 어",
"speaker": "speaker_3",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "거", "start_ms": 40, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 70, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 110, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "우리가", "start_ms": 150, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "처음에", "start_ms": 200, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "해놓고 이제와 막대한 비용을 전쟁으로 피해 입은 동맹들에게 전가하는 형국인 겁니다 이런 상황이어서 여론은 계속 나빠져 매사추세츠와 육아부가 실시한 이",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "해놓고", "start_ms": 10, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "이제와", "start_ms": 70, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "막대한", "start_ms": 130, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "비용을", "start_ms": 200, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "이 표는 이제 IQ인지 EQ인지는 확인해야죠",
"speaker": "speaker_5",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 60, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "표는", "start_ms": 90, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 130, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "IQ인지", "start_ms": 170, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "EQ인지는", "start_ms": 250, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "확인해야죠", "start_ms": 340, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "한 400인분 정도 준비를 하고 있어요 저는 튀김 담당입니다 튀김 네 한 하루 종일 튀김만 튀기시는 거예요",
"speaker": "speaker_6",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "한", "start_ms": 30, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "400인분", "start_ms": 60, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "정도", "start_ms": 140, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "준비를", "start_ms": 190, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "자국이 직접 전쟁에 뛰어들어야만 가능한 것은 아닙니다 다른 나라가 제공하지 못하는 가치를 내놓기만 해도 시대의 흐름을 붙잡을 수 있다는 뜻입니다 러시아 우크라이나 전쟁과 중동 전쟁은 세계에겐 불행한",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "자국이", "start_ms": 15, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "직접", "start_ms": 75, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "전쟁에", "start_ms": 125, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "뛰어들어야만", "start_ms": 185, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "전쟁이 세가 공간의 일부로 인정받게 된 것인데요 그들은 그 배경을 설명하면서 한국 세탁기가 미국 집 구조를 바꿔놓았다라는 표현을 자연스럽게 하고 있었습니다 물론 이 변화가 오직 한국 때문",
"speaker": "speaker_7",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "전쟁이", "start_ms": 25, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "세가", "start_ms": 85, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "공간의", "start_ms": 135, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "좀 높고 배터리가 좀 빨리 닳아가지고 제가 굉장히 아쉽다라고 말을 한 이후에요 한 일주일 정도 지나서 펌웨어 업데이트가 나왔는데 그거 업데이트 하고",
"speaker": "speaker_8",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "좀", "start_ms": 40, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "높고", "start_ms": 70, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "배터리가", "start_ms": 110, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "늘리는 30일대로 늘리고 그 다음 해에는 47일 때까지 확대할 예정입니다 이런 무시무시한 자동화 양산 능력이야말로 납기 보장의 자신감이 되는 것입니다 흥미로운 점은 한국의 이재명 대통령이",
"speaker": "speaker_4",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "늘리는", "start_ms": 20, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "30일대로", "start_ms": 70, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "물을 만들면 되지 않는가 이 질문에서 탄생한 것이 컨테이너 기반의 밀폐형 수직 농장입니다 구조부터 살펴볼까요 이 시스템은 외부와 100퍼센트 차단된 특수 외부",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "물을", "start_ms": 35, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "만들면", "start_ms": 75, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "결국은 앞으로는 이제 레이어를 여러 가지 둬가지고 방어를 잘 해야 할 텐데 단순히 한쪽에서 두는 게 아니라",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "결국은", "start_ms": 10, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "앞으로는", "start_ms": 80, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
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
"transcript": "요즘 날씨가 정말 화창하네요 주말에 나들이 가기 딱 좋은 것 같아요",
"speaker": "speaker_9",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "요즘", "start_ms": 100, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 160, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 230, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "화창하네요", "start_ms": 290, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 10:39:15*
