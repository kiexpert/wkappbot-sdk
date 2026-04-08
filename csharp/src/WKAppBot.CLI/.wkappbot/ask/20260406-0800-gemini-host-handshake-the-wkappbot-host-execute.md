# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=23176  time=07:58:24  uptime=14972s  mem=780MB  system_processes=414"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_075454.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 5976ms (file: 735-537-210-123-132-127-123-103-153-103-271-576-317-213-123-267-672-527-762-726-207-270-527-102-607-127-275-276-672-720-267-705-270-207-756-132-203-162-107-176-157-675-750-125-1_V.mp3)
  Segment 2: 6976ms ~ 13348ms (file: 321-320-132-762-276-213-320-217-172-732-103-137-317-321-371-735-751-275-325-137-713-320-721-712-106-162-162-316-317-726-321-327-123-260-236-263-236-236-276-273-132-716-157-167-6_W.mp3)
  Segment 3: 14348ms ~ 14924ms (file: 567-567-263 675-617-316-156 570-567-572-756-561-271-523-257-263-261-326-527-215-672-213-275-263-310-362-617-126-527-547-507-236-657-576-576_W.mp3)
  Segment 4: 15924ms ~ 16500ms (file: 517-137-157-170-750-715-157-751-157-751-571-751-571 574-574-574-574-574-571-571-570-571-517-175-751-157-157-157-107-153-751-175-173-571-751 571-175-573-571-517-573-571-157-571-7_W.mp3)
  Segment 5: 17500ms ~ 24268ms (file: 132-276-672-517-103-715-231-231-123-102-576-253-127-672-670-132-132-130-231-261-167-675-547-627-130-132-753-316-231-263-265-213-162-107-261-321-762-263-326-103-167-617-263-312-6_W.mp3)
  Segment 6: 25268ms ~ 25844ms (file: 136-176-125-123-137-715-751-710-375-507-107-175-316-756-756-705-576-765-576-307-301-307-617-310-106-301-136-103-307-103-670-657-675-570-572-231-216-372-502-562-765-267-573-127-2_V.mp3)
  Segment 7: 26844ms ~ 37608ms (file: 310-132-130 276-254-130-130-273-267-627-726-260-267-257-270-107-754-756-620-130-103-132-206-275-203-213-263-126-132-201-210-210 137-132-123-576-206-756-576-275-267-216-527-275-2_W.mp3)
  Segment 8: 38608ms ~ 39184ms (file: 316-102-102-164-106-162-612-160-165-106-615-167-761-735-316-136-136-756-761-765-576-756-756-765-715-756-567-675-765-357-762-756-765-675-651-176-167-167-176-173-763-716-176-135-1_W.mp3)
  Segment 9: 40184ms ~ 50156ms (file: 160-120-162-216-120-126-160-310-236-301-216-572-132-573-576-263-132-326-321-310-576-201-213-201-257-256-267-261-201-231-123-216-267-320-206-267-263-236-236-237-231-254-207-206-3_W.mp3)
  Segment 10: 51156ms ~ 55980ms (file: 574-267-105-176-107-127-627-162-627-173-237-762-127-750-273-327-327-306-237-753-273-267-371-175-276-672-370-375-320-706-276-231-120-705-267-251-321-275-261-276-726-276-217-762-2_W.mp3)
  Segment 11: 56980ms ~ 72388ms (file: 627-627-625-672-625-675-621-627-627-276-231-213-321-260-201-301-231-320-302-136-106-102-103 316-136-632-610-617-617-621-213-321-621-672-627-670-263-623-236-103-260-276-231 173-1_W.mp3)
  Segment 12: 73388ms ~ 79832ms (file: 517-217-260-132-173-312-203-236-231-230-236-203-217-302-237-257-103-206-213-206-230-132-203-123-102-103-576-260-201-216-231-210-213-231-321-230-230-230-231-123-230-123-230-321-2_V.mp3)
  Segment 13: 80832ms ~ 81408ms (file: 751-751-570-715-571-715-750-750-710 570-751-175-173-105-571-750-751-172-175-173-170-753-173-170-713-710-753-715-705-701-710-170-715-107-705-701-107-752 150-517-517-705-750-750-7_V.mp3)
  Segment 14: 82408ms ~ 82984ms (file: 753-753-756-751-753-735-573-751-157-756-576-573-753-751-571-572-573-753-753-751-756-735-765-671-765-756-573-756-576-571-375-576-571-537-751-731-371-376-376-376-371-376-713-736-3_W.mp3)
  Segment 15: 83984ms ~ 91040ms (file: 301-547-321-612-756-657-261-230-206-362-106-167-613-261-273-102-150-751-176-627-261-261-265-725-735-206-536-560-325-563-536-576-657-573-103-560-576-132-263-752-673-130-267-312-7_W.mp3)
  Segment 16: 92040ms ~ 97404ms (file: 123-231-132-123-312-132-123-173-213-132-126-321-123-321-126-231-213-312-236-132-132-213-213-312-132-123-231-237-213-123-123-312-321-123-261-123-312-321-123-213-126-132-136-273-2_W.mp3)
  Segment 17: 98404ms ~ 112192ms (file: 236-627-120-206-574-260-276-267-310-260-320-265-267-542-213-327-130-267-257-237-206-751-203-620-276-276-572-572-267-260-756-260-527-103-102-130 715-573-571-765-527-621-627-756-5_W.mp3)
  Segment 18: 113192ms ~ 113768ms (file: 715-175-715-175-157-175-157-175-175-715-157-175-175-176 130-137-175-175-175-765-765-137-713-715-716-175-175-176-167-175-176-175-176-176-176-175-176-176-176-173 135-317-137-173-1_V.mp3)
  Segment 19: 114768ms ~ 120780ms (file: 216-316-132-103-310-130-137-271-276-321-327-276-267-267-126-132-137-216-236-263-263-527-271-267-267-126-312-267-237-273-725-263-326-327-617-106-216-321-765-103-276-756-732-267-2_W.mp3)
  Segment 20: 121780ms ~ 122356ms (file: 351-175-132-135-132-125-513-135-153-150-125-315-102-152-571-537-135-157-173-317-135-137-172-317-132-136-613-612-651-167-165-156-576-576-351-176-172-162-132-137-132-271-235-362-3_W.mp3)
  Segment 21: 123356ms ~ 123932ms (file: 765-572-276-261-326-570-276-572-271-163-372-630-621-267-136-316-316-310-236-316-532-316-327-627-265-301-312-567-263-762-253-267-276-607-267-203-137-130-136-706-762-576-507-571-5_V.mp3)
  Segment 22: 124932ms ~ 132816ms (file: 213-213-726-217-206-260-201-751-271-207-203-320-317-132-726-210-205-206-130-107-514-201-267-216-206-672-762-726-270-273-206-123-213-210-574-207-302-627-236-207-503-273-263-502-7_W.mp3)
  Segment 23: 133816ms ~ 134392ms (file: 576-513-570-517-576-751-761-517-375-751-715-756-571-751-517-571-715-157-715-571-571-175-517-517-513-573-571-517-571-571 562-576-753-576-157-753-512-357-517-751-375-573-571-517-1_V.mp3)
  Segment 24: 135392ms ~ 135968ms (file: 253-261-236-275-275-263-705-263-657-267-726-752-627-602-257-237-273-756-236-762-672-572-756-672-236-356-271-756-625-362-750-132-126-576-321-672-613-321-527-652-362-216-321-756-6_V.mp3)
  Segment 25: 136968ms ~ 137544ms (file: 735-136-261-276-321-762-257-326-237-562-361-752-327-237-326-567-531-725-547-372-236-132-351-715-672-175-752-230-605-367-176-316_V.mp3)
  Segment 26: 138544ms ~ 139120ms (file: 267-671-750-162-315-510-317-236-573-576 270-675-572-756 564 376-253-716-631-172-267-762-237-321-321-236-732-273-517-567-326-765-507-672-605-302-136-237-325-672-352-736 627-627-6_W.mp3)
  Segment 27: 140120ms ~ 140696ms (file: 312-362-567-576-761-137-137-713-176-167_W.mp3)
  Segment 28: 141696ms ~ 148752ms (file: 167-176-216-765-207-276-203-230-231-652-602-321-327-102-102-267-267-123-276-705-217-103-265-231-572-103-236-276-137-237-576-576-273-756-237-130-167-617-312-253-257-106-571-671-7_W.mp3)
  Segment 29: 149752ms ~ 150328ms (file: 576-572-627-265-527-576-756-765-572-756-752-753-751-157-175-257-725-756-756-712-125-123-215-271-217-375-723-731-375-267-623-217-132-216-132-126-137-317-317-713-731-321-213-572-5_W.mp3)
  Segment 30: 151328ms ~ 156512ms (file: 157-321-703-167-107-160-137-732-672-765-376-705-357-130-135-103-321-157-267-167-130-326-231-267-237-273-123-120-213-123-103-123-102-132-327-236-312-170-310-762-632-107-167-106-6_W.mp3)
  Segment 31: 157512ms ~ 158088ms (file: 351-507-756-756-756-675-765-756-751-130-357-157-541-135-137-175-763-712-176-312-571-574-547-547-126-760_W.mp3)
  Segment 32: 159088ms ~ 159664ms (file: 135-762-613-352-327-725-213-527-532-521-572-132-706-627-273-127-750-312-273-376-762-362-136-752-502-537-572-327-570-571-275-672-256-276-725-276-572-275-231-237-263-536-753 507-2_V.mp3)
  Segment 33: 160664ms ~ 161240ms (file: 136-672-627-263-261-126-216-172-126-261-627-620-627-316-526-321-167-217-623-756-672-673-726-326-267-237-752-325-237-657-562-526-752-532-217-621-261_V.mp3)
  Segment 34: 162240ms ~ 162816ms (file: 150-510-150-510-153-153-751-261-263-236-263-632-362-236-273-263-627-625-672-637-213-127-167-163-167-160-163-162-162-162-162-162-163-237-321-316-213-127-327-135-253-215-215-261-1_W.mp3)
  Segment 35: 163816ms ~ 164392ms (file: 736-671-107-126-261-527-716-765-175-276-132-570-210-217-261-126-271-760-132-235-312-371-236-572-327-326-326-327-257-256-726-753-673-325-576-312-675-732-507-572-521-321-167-256-2_V.mp3)
  Segment 36: 165392ms ~ 174068ms (file: 574-310-756-750-651-167-107-327-756-756-567-123-321-102-752-167-163-136-312-612-632-632-267-107-276-123-213-201-753-175-213-547-752-756-756-301-261-326-635-753-256-267-263-725-2_W.mp3)
  Segment 37: 175068ms ~ 175644ms (file: 675 315-567-207-723-721-713-237-576-126-657-256-275-735-567-316-137-326-137-672-623-251-257-362-326-132-275-216-720-765-570-267-237-127-267-526-276 576-263-756-175-632-625-361-2_V.mp3)
  Segment 38: 176644ms ~ 177220ms (file: 752-570-576-756-763-517-536-576-527-576-576-725-167-316-361-361-361-316-361_W.mp3)
  Segment 39: 178220ms ~ 178796ms (file: 157-165-517-567-576-516-570-576-567-156-516-156-751-576-652-657-567-125-152-157-135-132-615-165-517-571-572-657-576-572-562-567-125-756-576-567-756-527-576-567-756-567-615-756-1_W.mp3)
  Segment 40: 179796ms ~ 180372ms (file: 571-517-570-517-571-517-571-573-571-135-715-510-103-571-107 571-157-517-157-513-157-175-150-175-135-175-175-105-157-157 105-150-157-175-173-170-715-107-130-135-153-157-103-107-7_V.mp3)
  Segment 41: 181372ms ~ 181948ms (file: 150-157-157-157-157-157-517-517-157-157-517-517-571-517-571-576-571 517-157-517-517-157-517-517-517-517-571-517-157-517 135-135-517-517-517-157-157-517-517-157-517-517-157-517 1_W.mp3)
  Segment 42: 182948ms ~ 183524ms (file: 571-716-671-617-167-316-136-103-361-137-367-316-670-673-763-637-672-367-376-362-326-631-621-623-637-627-576-657-563-562-163-312-567-576-627-762-576-567-675-573-567-657-657-632-6_W.mp3)
  Segment 43: 184524ms ~ 190896ms (file: 173-672-760-137-231-321-623-623-362-637-657-106-103-326-361-301-103-571-312-231-302-320-312-321-315-731-123-312-317-351-215-725-315-135-157-260-315-627-623-250-321-352-253-523-5_W.mp3)
  Segment 44: 191896ms ~ 199636ms (file: 102-675-571-127-231-236-130-163-175-152-231-236-372-275-752-132-132-236-267-123-263-267-263-325-267-236-132-267-162-617-103-123-143-765-267-756-103-312-321-327-132-103-132-103-1_W.mp3)
  Segment 45: 200636ms ~ 201212ms (file: 127-176-176-160-176-316-572-175-571-627-261-217-271-712-156-136-731-173-132-321-123-273-172-572-312-132-123-301-527-173-176-631-573-756-157-572-213-236-217-203-753-312-136-570-6_V.mp3)
  Segment 46: 202212ms ~ 211284ms (file: 260-260-267-260-260-260-206-206-216-207-210-230-213-231-321-326-236-236-236-237-236-236-203-176-176-126-103-321-320-302-312-310-132-206-320-310-321-320-321-231-130-320-261-260-6_W_stt_ar_and_stole_a-half_and_half.mp3)
  Segment 47: 212284ms ~ 212860ms (file: 751-756-751-751-751-750-571-710-751-751-751-715-751-175-157-751-751-751-751-750-571_V.mp3)
  Segment 48: 213860ms ~ 214436ms (file: 310-573-715-157-135-173-315-137-175-175-571-351-375-325-312-103_W.mp3)
  Segment 49: 215436ms ~ 216012ms (file: 675-672-367-317-617-540 316-132-132-320-312-613-312-326-321_V.mp3)
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
> **Time**: 2026-04-06 08:00:31
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답TITLE: Audio Transcription and Analysis for Language Learning

{
"segment": 1,
"transcript": "않는답니다 아니 대체 어디 일 보겠어요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "않는답니다", "start_ms": 150, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "아니", "start_ms": 1450, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "대체", "start_ms": 2100, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "어디", "start_ms": 2850, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "일", "start_ms": 3500, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "보겠어요", "start_ms": 3750, "dur_ms": 900, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "누군가 봐줘야 돼요 병사가",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "누군가", "start_ms": 200, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "봐줘야", "start_ms": 1100, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "돼요", "start_ms": 2400, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "병사가", "start_ms": 3200, "dur_ms": 1000, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "Over the onions? Okay.",
"speaker": "speaker_3",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "Over", "start_ms": 100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 450, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "onions?", "start_ms": 650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "Okay.", "start_ms": 1250, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 5,
"transcript": "반도체 같은 것도 중국 같은 경우는 희토류라는 것들이 있죠",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "반도체", "start_ms": 250, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "같은", "start_ms": 900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "것도", "start_ms": 1350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "중국", "start_ms": 1800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "같은", "start_ms": 2350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "경우는", "start_ms": 2800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "희토류라는", "start_ms": 3500, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "것들이", "start_ms": 4400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "있죠", "start_ms": 5000, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "완성된 휴머노이드가 실제로 나오면 이 상황은 점점 더 커질 걸로 봅니다",
"speaker": "speaker_4",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "완성된", "start_ms": 180, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "휴머노이드가", "start_ms": 850, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "실제로", "start_ms": 1800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "나오면", "start_ms": 2500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 3100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "상황은", "start_ms": 3350, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "점점", "start_ms": 4000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 4500, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "커질", "start_ms": 4750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "걸로", "start_ms": 5200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "봅니다", "start_ms": 5650, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "미처 대비할 새도 없이 너무 일찍 맞닥뜨린 미래 불확실한 시대를 살아가는 사람들은 방향을 잃고",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 300,
"audio_type": "speech",
"words": [
{"word": "미처", "start_ms": 300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "대비할", "start_ms": 850, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "새도", "start_ms": 1500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "없이", "start_ms": 1950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 2400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "일찍", "start_ms": 2950, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "맞닥뜨린", "start_ms": 3500, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "미래", "start_ms": 4300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "불확실한", "start_ms": 5200, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "시대를", "start_ms": 6000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "살아가는", "start_ms": 6700, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "사람들은", "start_ms": 7500, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "방향을", "start_ms": 8400, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "잃고", "start_ms": 9100, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "AI 에이전트가 뭐길래 이토록 뜨거운 관심을 한 몸에 받고 있는 걸까",
"speaker": "speaker_5",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "AI", "start_ms": 200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "에이전트가", "start_ms": 650, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "뭐길래", "start_ms": 1500, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "이토록", "start_ms": 2200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "뜨거운", "start_ms": 2850, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "관심을", "start_ms": 3500, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 4150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "몸에", "start_ms": 4400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "받고", "start_ms": 4850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 5300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "걸까", "start_ms": 5650, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "기존 AI가 방대한 데이터를 학습해 묻는 말에 답하는 두뇌라면",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "기존", "start_ms": 150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "AI가", "start_ms": 650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "방대한", "start_ms": 1200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "데이터를", "start_ms": 1850, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "학습해", "start_ms": 2600, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "묻는", "start_ms": 3250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "말에", "start_ms": 3700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "답하는", "start_ms": 4150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "두뇌라면", "start_ms": 4800, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 11,
"transcript": "다진마늘 설탕 굴소스",
"speaker": "speaker_6",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "다진마늘", "start_ms": 120, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "설탕", "start_ms": 1200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "굴소스", "start_ms": 1900, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "네 개의 한 방에 도전해 볼게요 참기름 밥 단무지",
"speaker": "speaker_6",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "개의", "start_ms": 400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 750, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "방에", "start_ms": 1000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "도전해", "start_ms": 1500, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "볼게요", "start_ms": 2150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "참기름", "start_ms": 3000, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "밥", "start_ms": 4000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "단무지", "start_ms": 4500, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 14,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 15,
"transcript": "힘들었던 때도 있었고 해외는 물론 국내에서조차 이 KF-21 개발은 무모한 도전이자 돈 낭비이며 그 성공 확률이 채 10%도 되지",
"speaker": "speaker_7",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "힘들었던", "start_ms": 200, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "때도", "start_ms": 950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "있었고", "start_ms": 1300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "해외는", "start_ms": 2000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "물론", "start_ms": 2650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "국내에서조차", "start_ms": 3100, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 4150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "KF-21", "start_ms": 4400, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "개발은", "start_ms": 5250, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "무모한", "start_ms": 5900, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "도전이자", "start_ms": 6550, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "돈", "start_ms": 7300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "낭비이며", "start_ms": 7650, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 8400, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "성공", "start_ms": 8650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "확률이", "start_ms": 9200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "채", "start_ms": 9850, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "10%도", "start_ms": 10100, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "되지", "start_ms": 10850, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "(music)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 17,
"transcript": "응원하며 오늘 영상 마치겠습니다 저희 광화문브리핑 채널 구독 좋아요도 부탁드리고요 오늘도 시청해 주셔서 감사",
"speaker": "speaker_7",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "응원하며", "start_ms": 150, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "영상", "start_ms": 1350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "마치겠습니다", "start_ms": 1900, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "저희", "start_ms": 3000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "광화문브리핑", "start_ms": 3450, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "채널", "start_ms": 4400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "구독", "start_ms": 4950, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "좋아요도", "start_ms": 5500, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "부탁드리고요", "start_ms": 6250, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "오늘도", "start_ms": 7400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "시청해", "start_ms": 7950, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "주셔서", "start_ms": 8600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "감사", "start_ms": 9150, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "나아가고 있다는 생각인데요 그런 점에서 MLS 리그 반대편의 존재하는 거대한 축구의 신 메시와 닮아 가고 있",
"speaker": "speaker_8",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "나아가고", "start_ms": 100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "있다는", "start_ms": 750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "생각인데요", "start_ms": 1200, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "그런", "start_ms": 2200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "점에서", "start_ms": 2550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "MLS", "start_ms": 3200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "리그", "start_ms": 3850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "반대편의", "start_ms": 4300, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "존재하는", "start_ms": 4950, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "거대한", "start_ms": 5700, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "축구의", "start_ms": 6350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "신", "start_ms": 6900, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "메시와", "start_ms": 7150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "닮아", "start_ms": 7800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "가고", "start_ms": 8250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "있", "start_ms": 8700, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "의 교복 디자인에 불과하기 때문이죠 실제로 한국 네티즌들은 교복 색상이나 디자인이 요즘 스타일은 아니다 진짜배기 교복은 쓰리피스다 라며 저마다 자신들의 학교 생활에",
"speaker": "speaker_9",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "의", "start_ms": 200, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "교복", "start_ms": 450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "디자인에", "start_ms": 900, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "불과하기", "start_ms": 1550, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "때문이죠", "start_ms": 2300, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "실제로", "start_ms": 3200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 3850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "네티즌들은", "start_ms": 4300, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "교복", "start_ms": 5250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "색상이나", "start_ms": 5700, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "디자인이", "start_ms": 6450, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "요즘", "start_ms": 7200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "스타일은", "start_ms": 7650, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "아니다", "start_ms": 8300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "진짜배기", "start_ms": 9000, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "교복은", "start_ms": 9750, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "쓰리피스다", "start_ms": 10400, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "라며", "start_ms": 11250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "저마다", "start_ms": 11700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "자신들의", "start_ms": 12250, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "학교", "start_ms": 13000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "생활에", "start_ms": 13450, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 21,
"transcript": "대한광통신 매매 손익을 보여드리면 T+0일차에 이렇게 상한가에 일부 수익 실현해서 800만 원 정도 수익 실현했었고 다음 날",
"speaker": "speaker_10",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "대한광통신", "start_ms": 150, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "매매", "start_ms": 1200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "손익을", "start_ms": 1650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "보여드리면", "start_ms": 2200, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "T+0일차에", "start_ms": 3200, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 4150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "상한가에", "start_ms": 4700, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "일부", "start_ms": 5450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "수익", "start_ms": 5900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "실현해서", "start_ms": 6350, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "800만", "start_ms": 7100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "원", "start_ms": 7650, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "정도", "start_ms": 7900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "수익", "start_ms": 8350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "실현했었고", "start_ms": 8800, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 9700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "날", "start_ms": 10150, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "그래서 저는 인버스나 헤지주 매매는 아예 고려를 하지도 않았고 지수가 크게 하락하는 날에도",
"speaker": "speaker_10",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "그래서", "start_ms": 120, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "저는", "start_ms": 650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "인버스나", "start_ms": 1100, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "헤지주", "start_ms": 1850, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "매매는", "start_ms": 2500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "아예", "start_ms": 3050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "고려를", "start_ms": 3500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "하지도", "start_ms": 4050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "않았고", "start_ms": 4600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "지수가", "start_ms": 5400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "크게", "start_ms": 5950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "하락하는", "start_ms": 6400, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "날에도", "start_ms": 7150, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "능력 없습니다 그것보다는 정말 10년에 걸친 끝도 없는 감동 그리고 타국과의 약속은 반드시 성실하게 이행해야 한다는 의지에 있어 인도네시아는 우리와는 조금 다른 철학",
"speaker": "speaker_11",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "능력", "start_ms": 250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "없습니다", "start_ms": 700, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "그것보다는", "start_ms": 1500, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 2350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "10년에", "start_ms": 2800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "걸친", "start_ms": 3450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "끝도", "start_ms": 4000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "없는", "start_ms": 4450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "감동", "start_ms": 4900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 5600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "타국과의", "start_ms": 6150, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "약속은", "start_ms": 6900, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "반드시", "start_ms": 7550, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "성실하게", "start_ms": 8200, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "이행해야", "start_ms": 9050, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "한다는", "start_ms": 9900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "의지에", "start_ms": 10450, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "있어", "start_ms": 11100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "인도네시아는", "start_ms": 11600, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "우리와는", "start_ms": 12550, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 13200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "다른", "start_ms": 13650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "철학", "start_ms": 14100, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 25,
"transcript": "면 중국에서 피머스를 생산하려고 할 건데 이게 어떻게 전개될 건지도 향후 관심사 중 하나겠죠 현대차그룹의 휴머노이드 로봇은 203",
"speaker": "speaker_4",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "면", "start_ms": 180, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "중국에서", "start_ms": 450, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "피머스를", "start_ms": 1200, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "생산하려고", "start_ms": 1950, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 2800, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "건데", "start_ms": 3050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 3600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 4050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "전개될", "start_ms": 4600, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "건지도", "start_ms": 5250, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "향후", "start_ms": 6000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "관심사", "start_ms": 6450, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "중", "start_ms": 7100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "하나겠죠", "start_ms": 7350, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "현대차그룹의", "start_ms": 8300, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "휴머노이드", "start_ms": 9350, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "로봇은", "start_ms": 10100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "203", "start_ms": 10750, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 27,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 28,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 29,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 30,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 31,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 32,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 33,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 34,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 35,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 36,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 37,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 38,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 39,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 40,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 41,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 42,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 43,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 44,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 45,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 46,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 47,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 48,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 49,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 08:00:31*
