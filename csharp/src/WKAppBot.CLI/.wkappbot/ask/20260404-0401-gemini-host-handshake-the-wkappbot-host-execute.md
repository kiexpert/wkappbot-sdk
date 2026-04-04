# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=14716  time=04:00:16  uptime=12772s  mem=463MB  system_processes=367"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260404_040007.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 132-341 765-613-160-163-612-321-312-132-312-321-132-312-371-637-356-320-623-362-372-632-327-362-136-567-675-670-170-170-170-172-176-176-167-167-167-167-176-162-160-632-102-102-6_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 167-316-316-154 316-123-265-267-261-261-132-132-231-213-231-261-126-136-132-123-136-126-162-346-413-756-756-756-574-754-756-617-173-163 312-123-627-157-175-675-675-672-206-627-2_V.mp3)
  Segment 3: 3152ms ~ 10172ms (file: 123-132-135-751-137-765-576-157-123-326-365-162-231-372-106-315-316-132-317-673-123-263-627-753-612-231-236-321-367-261-263-627-167-612-136-137-376-106-137-612-201-213-123-271-2_W.mp3)
  Segment 4: 11172ms ~ 11748ms (file: 576-567 756 576-756-136-162 765-765-765-763 236-261-126-132-126-106-623-103-102 132 213-321-132 174-213-103-230-157-160-617-167-103-173-136-106-160-107-617-167-263-716-103-160-6_V.mp3)
  Segment 5: 12748ms ~ 13324ms (file: 126-761-367 136-106-316-675-617-106-761-160-105-172-617-103-167-136-163-103-130-103-571-163-160-136-103-106-261-176-123-106-106-163-160-176-136-130-617-103-316-126-106-102-165-7_V.mp3)
  Segment 6: 14324ms ~ 14900ms (file: 376-251-167-672-675-576 132-103-516-165-125-652-506-527-257-176-153-136-132-103-765-160-106-132-162-160-106-103-106-136-103-102-162-152-617-263-602-136-162-163-136-103-103-130-3_V.mp3)
  Segment 7: 15900ms ~ 16476ms (file: 103-167-310-132-160-106-106-103-163-127-176-653-761-136-671-126-312 137-103-167-126-761-167-163-652-716-136-103-541-123-103-163-103-365-310-312-136-163-103-163-160-102-316-167-3_V.mp3)
  Segment 8: 17476ms ~ 18052ms (file: 763-176-326-361-316-136-137-176-267 267 627-672-263-261_W.mp3)
  Segment 9: 19052ms ~ 19628ms (file: 506-570-567-574-732-627-312-761-763-671-761-176-176-716-761-671-126-123-120-326-621-623-617-632-621-275 641-361-361-341-231-132-671-761-760-765-765-716-765-762-621-657-657-675-6_W.mp3)
  Segment 10: 20628ms ~ 21204ms (file: 132-132-302-103-203-320-213-504-503-154-132-136-301-316-302-302-321-301-132-321-310-130 632-176-106-612-162-675-106-132-567-103-761-617-136-236 132-321_V.mp3)
  Segment 11: 22204ms ~ 22780ms (file: 612-106-106-167-236-261-123-312-106-617-301-203-103-132-136-132-317-751-765-715-136-123-162-326-736-621-132-136-163-621-673-132-312-132-107-167-751-631-106-167-106-136-361-301-3_V.mp3)
  Segment 12: 23780ms ~ 31952ms (file: 276-267-213-317-376-370-731-173-713-276-750-765-756-725-726-750-756-750-210-276-210-276-753-756-752-765-756-327-327-372-120-132-153-257-275-271-273-172-310-371-130-260-276-267-3_W.mp3)
  Segment 13: 32952ms ~ 33528ms (file: 325-351-352-361-362-352-765-376-356-365-376-327-537-567-567-520-504-250-206-206-267-357-351-154-520-560-256-526-150-165-627-672-576-256-534-573-203-523-532-265-652-256-270-357-3_W.mp3)
  Segment 14: 34528ms ~ 35104ms (file: 351-257-102-230-321-123-720-362-365-132-136_W.mp3)
  Segment 15: 36104ms ~ 36680ms (file: 567-621-126-102-103-657-213-251-267-321-231-162-625-105-236-261-215-132-263-263-657-235-206-602-236-627-632 263-216-726-631-103-102-312-123-103-312-132 716-617-103-103-736-163-1_V.mp3)
  Segment 16: 37680ms ~ 43260ms (file: 276-276-207-627-361-360-216-261-267-627-625-265-657-576-751-627-657-260-627-136-312-135-210-213-231-123-120-123-132-123-132-123-136-103-230-231-617-132-132-213-106-163-317-376-1_W.mp3)
  Segment 17: 44260ms ~ 44836ms (file: 562-652-256-652-675-625-657-632-267-265-627-623-620-261-627-620-260-625-265-625-265-265-265-265-267-625-650-625-265-625-652-256-625-657-652-625-652-657-675-627 625-265-621-621-6_W.mp3)
  Segment 18: 45836ms ~ 46412ms (file: 765-576-756-765-265-276-627-672-236-675-576-765-136-316-301-216-236-302-301-132-136-316-136-103-570-715-751-136-103-103-103-103-701-701-106-571-570-675-123-765-765-574-567-657-6_W.mp3)
  Segment 19: 47412ms ~ 47988ms (file: 312-237-216-326-312-321-231-321-362-632-326-321-326-167-371-173-106-132-306-316-136-130-573-132-671-106-106-130-127-361-130-132-103-671-675-126-167-167-126-103-715-103-156-176-1_W.mp3)
  Segment 20: 48988ms ~ 49564ms (file: 312-136-261-126-132-103-263-627-623 156-671-365 756-163-312-103-317-103-103-103-136-163-316-567-130-107-163-132-136-163-160-165-103-137-176-762-316-361-351-632-132-230-136-156-1_W.mp3)
  Segment 21: 50564ms ~ 51140ms (file: 327-103-132-136-301-132-312-163-357-136-567-632 132_V.mp3)
  Segment 22: 52140ms ~ 52716ms (file: 376-751-576-165-357-137-317-375-135-176-761-371-731-376-765-765-376-153-532-527-562-256-215-652-275-152-157-127-152-152-356-325-352-576-573-357-675-632-367-327-267-316-126-315-2_W.mp3)
  Segment 23: 53716ms ~ 54292ms (file: 216-162-761-106-160-617-165-516-135-761-602-576-132-651-103-160-106-173-157-167-103-173-173-157-731-731-176-731-263-276-276-726-217-162-156-156-162-132-123-231-213-213-126-316-3_V.mp3)
  Segment 24: 55292ms ~ 55868ms (file: 126-613-263-132-213-136-132-613-231-621-623-612-623_W.mp3)
  Segment 25: 56868ms ~ 57444ms (file: 621-135-137-103-351-716-165-103-615-165-617-132-160-103-752-156-167-316-316-163-756-137-756-137-136-267-623 103-572-167-761-103-136-530-613-136-103-106-163 620-672-136-103-756-1_V.mp3)
  Segment 26: 58444ms ~ 72988ms (file: 261-750-756-265-754-132-675-765-756-103-317-103-756-756-672-132-576-567-130-756-756-563-163-630-356-631-756-163-365-106-163-516-756-165-756-572-321-574-137-175-163-103-756-270-2_W.mp3)
  Segment 27: 73988ms ~ 74564ms (file: 263-127-217-217-271-276-271-612-263-625-265-625-216-236-265-261-265-321-762-726 310-136-617-132-162-132 657-213-613-132-652-562-625-162-236-263-267-236 726-761-627-236-260-236-2_W.mp3)
  Segment 28: 75564ms ~ 76140ms (file: 631-261-105-163-716-610-137-167-103-576-752-523-657-173-326-620-613-160-106-610-163-103-106-316-617-160-137-167-165-136-310-165-132-106-761-617-165-160-136-132-762-236-267-263_V.mp3)
  Segment 29: 77140ms ~ 77716ms (file: 136-617-163-613-316-162-130-560-103-132-126-326-136-106-132-137-106-163-106-162-312-756-651-516-261 123-120-321-621-136-621-137-103-136-561-276-671-362-137-617-621-675-136-103-6_V.mp3)
  Segment 30: 78716ms ~ 79292ms (file: 203-216-217-267-206-236-706-670-672-367-103-132-316-136-130-136-103-260-263-231-705-765-203-102-103-167-231-601-260-267-260-126-123-102-307-320-230-320-302-103-207-132-176-730-7_W.mp3)
  Segment 31: 80292ms ~ 80868ms (file: 574-564-546-567-165-517-765-675-752-675-765-672-756-176-627-675-673-162-136-132-321-326-213-361-360_W.mp3)
  Segment 32: 81868ms ~ 82444ms (file: 765-675 613 547-567-752-132-316 627-617-670 132-756-756-760-756-765-765-765-705-571-756-617-763-362-627-206-231-156-761-716-176-716-103-103-167-236-231-265-301-360-316-376-236_W.mp3)
  Segment 33: 83444ms ~ 84020ms (file: 613-163-613-657-650-601-601-601-576-375-301-362-301-106-163-613-617-613-617-547-574-754-756-750-705-670-762 120-310-316-361 130-173-130-312-263-172-106-632-163-126-312-207-321-1_V.mp3)
  Segment 34: 85020ms ~ 85596ms (file: 316-136-361-362-603-620-721-157-126-675-627-260-672-675-670-260-572-260-206-261-126-137-316-765-765-760-754-752-310-312-167-176-765-670-750-574-571-175-103-301-710-103-672-610-1_V.mp3)
  Segment 35: 86596ms ~ 87172ms (file: 163-136-126-306-127-615-163-106-136-136-103-132-136-136-162-103-617-106-103-136-103-130-136-132-160-160-162-163-167-106-165-132-163-316-152-157-136-163-103-170-167-103-162-103-1_V.mp3)
  Segment 36: 88172ms ~ 88748ms (file: 257-123-236-321-327-273-327-237-312-302-275-132-372-631-756-725-376-317-210-103-103-162-321-307-572-712-127-230-327-203-206-762-615-306-107-310-123-651-351-126-167-125-321-102-1_V.mp3)
  Segment 37: 89748ms ~ 90324ms (file: 675-657-216-321-517-651-621-623-612-613-103-160-603-635-603-103-316-136-163-132-103-136-103-103-541-163-602-167-631-165-162-526-167-301-673-761-107-610-163-167-163-761-310-176-1_V.mp3)
  Segment 38: 91324ms ~ 91900ms (file: 615-617-136-167-123-163-567-102-176-126-267-326-263-716-107-167-103-267-210-132-132-137-176-716-715-716-713-162-612 216-326_W.mp3)
  Segment 39: 92900ms ~ 93476ms (file: 163-756-756-750-703-137-513-756-756-103-106-106-103-106-103-103-103-132-103-130-103-312-310-130-103-103-130-136-612-162-127-617-136-163-613-106-160-163-627-261-160-136-521-126-1_V.mp3)
  Segment 40: 94476ms ~ 95052ms (file: 325-312-321-312-361-362-623-236-321-573-571-572-517-574-521-517-512-251-257-623-527-572-571-576-576-576-547-572-576-572-571-357-674-670-527-527-576-753-763-625-621-172-351-153-5_W.mp3)
  Segment 41: 96052ms ~ 105736ms (file: 132-312-623-320-132-132-506-706-751-617-361-237-756-751-751-751-751-175-576-706-170-210-720-715-207-721-762-721-702-217-217-127-127-267-267-261-267-260-107-267-267-126-312-261-2_W.mp3)
  Segment 42: 106736ms ~ 115520ms (file: 126-627-276-236-231-263-261-160-316-326-301-672-706-765-527-625-106-537-267-765-756-267-362-263-263-675-267-267-236-723-106-167-203-267-263-627-627-261-632-105-163-136-756-176-2_W.mp3)
  Segment 43: 116520ms ~ 117096ms (file: 326-316-132-312-321-312-361-216-321-120-132-263-236-263-216-263-625-236-625-267-263 617-267-672-627_W.mp3)
  Segment 44: 118096ms ~ 118672ms (file: 267-263-136-106-267-673-675-310-631-163-576-163-137-216-127-167-160-136-136-765-621-652-136-751-316-162-163-102-123-136-103-163-103-130-756-165-562 167-657-160-625-657-106-316-1_V.mp3)
  Segment 45: 119672ms ~ 120248ms (file: 102-102-216-103-102-132-213-120-210-120-102-120-102-102-256-517-567-167-172-735-162-715-275-176-102-276-127-217-721-106-136-103-106-165-105-137-761-316-137-103-107-160-621-716-6_V.mp3)
  Segment 46: 121248ms ~ 121824ms (file: 301-236-230-213-302-312-103-132-130-203-213-532-627-602-137-210-236-263-312-302-362-632-623-362-613-213-216-167-132-321-321-213-230-362-620-123-132-576-321-210-123-130-123-130-1_W.mp3)
  Segment 47: 122824ms ~ 123400ms (file: 163-106-103-213-213-162-216-130-103-261-176-312-132-132-143-341-276-715-574-562-625-127-136-130-263-123-137-762-102-210-120-103-650-261-764-657-760-670-672-670-756-675-670-321-6_V.mp3)
  Segment 48: 124400ms ~ 124976ms (file: 657-657-165-765-765-675-130-103-761-763-637-761-173-173-176-570-571-612-670-617-602-610-670-107-617-762-106-670 601-621-210_W.mp3)
  Segment 49: 125976ms ~ 126552ms (file: 163-106-102-672-132-316 627 671-137-605-765 612-162-613-167-167-167-163-261-162-162-106-132-261-236-612-163-132-261-361-673-261-621-765-671-675-627-567 216-671 675-756-756-670-5_W.mp3)
  Segment 50: 127552ms ~ 128128ms (file: 670-670-312-213-163-274-236-326-621-625-126-364 576-576-537-576-276-215-163-657-675-675-675-675-675-675-657-567-657-675-675-765-675-657-657-675-576-570-567-576-675-235-276-317-4_V.mp3)
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
> **Time**: 2026-04-04 04:01:12
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "사상 최고가를 돌파하면서 강한 상승세를 보이고 나서 최근에 박스권으로 움직였습니다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "사상", "start_ms": 10, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "최고가를", "start_ms": 55, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "돌파하면서", "start_ms": 120, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "강한", "start_ms": 205, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "상승세를", "start_ms": 250, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "보이고", "start_ms": 315, "dur_ms": 45, "phase_lr_deg": 0.0},
{"word": "나서", "start_ms": 365, "dur_ms": 35, "phase_lr_deg": 0.0},
{"word": "최근에", "start_ms": 410, "dur_ms": 45, "phase_lr_deg": 0.0},
{"word": "박스권으로", "start_ms": 460, "dur_ms": 65, "phase_lr_deg": 0.0},
{"word": "움직였습니다.", "start_ms": 530, "dur_ms": 40, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "너무 좋은 거야. 감동 그 리듬 너무 좋았잖아. 이게 모두 내공이 될 수 있잖아.",
"speaker": "speaker_2",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "너무", "start_ms": 20, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 55, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "거야.", "start_ms": 90, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "감동", "start_ms": 130, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 175, "dur_ms": 15, "phase_lr_deg": 0.0},
{"word": "리듬", "start_ms": 195, "dur_ms": 35, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 235, "dur_ms": 35, "phase_lr_deg": 0.0},
{"word": "좋았잖아.", "start_ms": 275, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 350, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "모두", "start_ms": 385, "dur_ms": 35, "phase_lr_deg": 0.0},
{"word": "내공이", "start_ms": 425, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "될", "start_ms": 480, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 505, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "있잖아.", "start_ms": 530, "dur_ms": 40, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 4,
"transcript": "맞아. 마치 내가 이렇게 주문한 것처럼.",
"speaker": "speaker_3",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "맞아.", "start_ms": 40, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "마치", "start_ms": 90, "dur_ms": 45, "phase_lr_deg": 0.0},
{"word": "내가", "start_ms": 140, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 185, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "주문한", "start_ms": 250, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "것처럼.", "start_ms": 325, "dur_ms": 75, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "애들이 지칠 때쯤.",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "애들이", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "지칠", "start_ms": 140, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "때쯤.", "start_ms": 220, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 7,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 8,
"transcript": "호호호호!",
"speaker": "speaker_4",
"confidence": 0.90,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "호호호호!", "start_ms": 30, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 10,
"transcript": "다시 돌고래로 변신을 했지.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "다시", "start_ms": 40, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "돌고래로", "start_ms": 100, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "변신을", "start_ms": 190, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "했지.", "start_ms": 270, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "누가 비중을 좀 많이 때렸을 때는 조금 슬럼프를 겪었는데.",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "누가", "start_ms": 30, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "비중을", "start_ms": 75, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 140, "dur_ms": 25, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 170, "dur_ms": 45, "phase_lr_deg": 0.0},
{"word": "때렸을", "start_ms": 220, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "때는", "start_ms": 285, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 330, "dur_ms": 45, "phase_lr_deg": 0.0},
{"word": "슬럼프를", "start_ms": 380, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "겪었는데.", "start_ms": 455, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "지금 비중을 낮추면서 다시 슬럼프를 극복하는 모습을 보고 나는 너무 기쁘다.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "지금", "start_ms": 20, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "비중을", "start_ms": 80, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "낮추면서", "start_ms": 150, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 240, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "슬럼프를", "start_ms": 310, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "극복하는", "start_ms": 390, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "모습을", "start_ms": 480, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "보고", "start_ms": 550, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "나는", "start_ms": 600, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 650, "dur_ms": 45, "phase_lr_deg": 0.0},
{"word": "기쁘다.", "start_ms": 705, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "하하. 어제 걱정해서 전화했잖아.",
"speaker": "speaker_3",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "하하.", "start_ms": 10, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "어제", "start_ms": 80, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "걱정해서", "start_ms": 140, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "전화했잖아.", "start_ms": 220, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "혹시나 H-테마 몰빵 안 했을까 봐.",
"speaker": "speaker_3",
"confidence": 0.90,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "혹시나", "start_ms": 20, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "H-테마", "start_ms": 90, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "몰빵", "start_ms": 180, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 250, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "했을까", "start_ms": 290, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "봐.", "start_ms": 360, "dur_ms": 40, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "어제 웃겼던 게 어제 대차거래소에서.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "어제", "start_ms": 30, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "웃겼던", "start_ms": 90, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 170, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "어제", "start_ms": 200, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "대차거래소에서.", "start_ms": 270, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 17,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 18,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 19,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 20,
"transcript": "귀여워.",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "귀여워.", "start_ms": 100, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "귀여워.",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "귀여워.", "start_ms": 80, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "이렇게 무리해서 저희가 서둘러서 간 곳은 어디일까요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "이렇게", "start_ms": 20, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "무리해서", "start_ms": 90, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "저희가", "start_ms": 180, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "서둘러서", "start_ms": 260, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "간", "start_ms": 350, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "곳은", "start_ms": 390, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "어디일까요?", "start_ms": 440, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "세림 이때만 해도 마트를 간다고 생각하고 있었습니다.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "세림", "start_ms": 30, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "이때만", "start_ms": 90, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "해도", "start_ms": 170, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "마트를", "start_ms": 220, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "간다고", "start_ms": 290, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "생각하고", "start_ms": 370, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "있었습니다.", "start_ms": 460, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "마트 가서.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "마트", "start_ms": 50, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "가서.", "start_ms": 120, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "대장주를 관심 종목에 넣습니다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "대장주를", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "관심", "start_ms": 140, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "종목에", "start_ms": 220, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "넣습니다.", "start_ms": 310, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "그리고 그 종목들 중에서 눌림이 예쁘게 나오거나 시장 때문에 일시적으로 급락했다가.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "그리고", "start_ms": 20, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 90, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "종목들", "start_ms": 130, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "중에서", "start_ms": 220, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "눌림이", "start_ms": 310, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "예쁘게", "start_ms": 390, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "나오거나", "start_ms": 470, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "시장", "start_ms": 560, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "때문에", "start_ms": 630, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "일시적으로", "start_ms": 710, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "급락했다가.", "start_ms": 820, "dur_ms": 120, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-04 04:01:12*
