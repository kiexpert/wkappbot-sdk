# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=14524  time=04:36:51  uptime=3201s  mem=2241MB  system_processes=702"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260401_043641.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 237-726-267-675-672-261-216-321-162-312-263-627-672-176-127-120-106-126-136-267-612-761-721-261-621-672-627-276-762-671-672-675-672-675-672-672-627-627-627-627-672-672-672-672-1_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 263-260-627-621-612-362-251-762-762-310-130-312-136-236-231-321-562-302-316-326-620-620-610-163-106-130-310-312-135-106-160-321-267-627-625-657-652-607-567-576-756-675-576-612-1_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 103-105-165-132-163-162-103-156-271-132-316-160-130-160-106-103 175-621-601-136-137 516-362-256-671-506-623-103-130-762-620-621-130-103-613-136-123-132-326-167-561-310-310-312-1_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 256-756-573-236-623-527-752-172-102-103-103-132-173-137-750-576-623-563-562-602-362-206-236-326-260-203-206-203-261-203-230-310-123-130-705-175-715-176-301-317-372-132-136-715-7_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 230-321-236-236-231-231-263-326-236-236-321-210-236-321-236-231-261-236-231-132-263-263-230-213-312 132-136-316-137-301-130-103-326-632-621-623-162-326-601-326-621-632-720-137-1_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 215-103-671-160-136-103-176-621-167-136-620-312-132-103-612-126-103-103-130-160-136-321-106-137-103-165-136-137-163-621-127-567-165-106-512-761-137-271-167-136-103-136-167-216-6_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 621-621-126-261-231-271-271-276-267-267-627-261-621-627-672-376-627-731-172-123-627-672-312-627-267-621-126-163-162-673-132-607-627-765-613-316-126-217-326-213-237-276-263-326-6_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 502-652-126-163-510-316-160-103-106-351-567-576-260-620-267-627-605-201-206-625-206-516-503-562-321-260-513-750-217-230-162-165-563-156-167-136-103-103-102-103-132-136-157-761-5_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 621-126-675-637-621-236-621-671-261-201-236-613-127-136-326-321-320-632-123-167-312-167-132-267-162-167 261-753-107-165-123-132-612-617-236-132-613-263-267-162-631-613-365-612-3_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 162-127-176-172-175-167-617-167-162-165-617-167-612-162-176-162-165-162-162-162-165-167-162-123-261-612-126-162-165-165-162-162-127-167-162-163-152-165-157-671-657-517-675-765-7_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 613-130-103-625-675-163-617-167-162-765-621-103-173-176-302-156-761-130_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 132-317-107-231-132-123-163-321-321-267-623-721-625-261-576-132-756-765-627-623-627-162-762-657_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 623-627-261-623-216-261-213-632-213_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 127-167-136-671-657-657-675-567-157-516-613-617-672-162-613-623-123-651-625-765-675-106-103-136-156-612-106-106-136-167-671-165-136-103-671-675-576-621-617-362-120-263-625-260-5_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 623-376-632-362-327-236-763-761-756-765-762-326-236-320-326-321-367-712-721-726-627-623-523-230-320 631-167-162-632-731-627-327-376-371-756-762-576-765-765-672-673-765-765-137-1_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 167-710-561-125-657-165-167-136-106-574-715-571-751-157-756-675-657-671-671-675-574 542 132-126-130-571-517-153-126-132-547-176-231-127-316-137-163-137-657-132-617-736-103-726-2_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 362-362-362-627-623-671-672 756-756-672-167-362-326-321-543-537-276-267-267-267-627-627-267-267-362-326-237-231-237-326-236-267-263-236-276-267-627-627-627-567-326-613-613-617-6_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 102-136-106-317-136-316-312 164-123-162-136 621-637_V.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 132-103-136-103-136-153-105-756-367-236-675-267-236-132-310-302-302-361-306-320-301-312-321-326-106-162-136 132-301-321-230-236-132-102-260-260-320-132-123-301-302-203-250-302-2_V.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 306-236-320-760-730-230-206-203-203-206-751-752-572-207-320-102-102-301-236-265-576-562-365-763-362-672-762-716-275-327-621-263-267-236-216-621-173-172-127-165-251-261-126-126-2_W.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 326-327-327-723-236-230-312-327-237-273-543-543-541-762-720-263-326-236-362-152-165-156-165-165-165-163-153-167-167-765-675-136-136-123-632-361-361-631-365-365-657-365-367-615-1_W.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 216-567-765-312-607-631-317-103-710-176-761-612-167-106-732-716-103-630-106-163-613-107-761-276-176-106-167-761-165-167-657-167-137-130-123-160-706-167-163-316-163-163-103-617-6_V.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 653-567-562-657-657-567-657-653-567-576-567-567-657-675-657-567-652-652-723-613-653-517-657-657-367-625-256-657-563-657-672-625-657-567-637-653-567-765-375-726-651-567-526-567-5_W.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 162-651-130-632-126-103 671-615-527-621-671-675-160-671-627-157-371-160-162-362-163-103-136-617-601-106-163-132-316-103-321-103-516-632-612-176-106-167-672-136-130-231-675-672-1_V.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 621-132-312-132-136-632-136-321-621-103-217-126-203-765-625-627-165-613-126-127-257-632-621-267-762-756-576-705-567-507-537-127-612-576-751-765-675-547-576-567-576-127-126-572-5_W.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 613-620-610-672-673-627-760-756-106-160-670-607-167-670-106-136-106-136-123-526-617-625-132-547-605-162-136-671-103-132-164-107-125-753-317-617-163-103-673-673-163-106-701-107-7_V.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 316-271-627-672-672-672-361-326-273-672-361-362-632-326-602-312-361-362-362-672-674-613-574-654-346-321-760-756-132-617-675-106-102-102-130-310-302-306-136-162-106-301-317-103-1_V.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 576-567-576-537-356-365-356-356-375-537-567-567-567-675-657-657-653-657-106-576-567-163-316-537-573-507-315-130-103-132-103-103-710-701-301-132-106-105-106-107-136-105-103-135-1_W.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 627-672-672-672-672-627-672-316_W.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 316-236-326-267-526-267-765-762-103-103-312-130-102-162-167-175-176-106-163-136-671-362-302-316-312-362-312-361-301-761-637-132-301-310-312-321-162-123-312-236-326-623-130-312-3_W.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 627-627-657-167-365-673-673-657-607-671-176-167-517-156-310-136-576 613-627-673-657-620-123-132-132-326-213-620-236-216-362-236-163-167-613-627-163-671-761-765-167-326-312-326-1_W.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 573-574-527-162-236-236-631-672-637-527-517-517-672-540-571-572-257-257-751-275-270-257-527-537-165-312-357-357-537-537-531-567-326-236-623-362-631-325-352-532-321-573-576-576-5_W.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 176-162-213-127-127-120-120-120-102 652-621 130-617-162-106-567-132-637-216-756-632-106-132-103-136-316-136-132-173-106-126-326-671-132-236-103-136-361-106-103-136-162-316-163-1_V.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 137-103-201-137-316-761-765-706-167-106-132-163-132-176-103-136-526-612-576-217-125-162-216-312-312-325-123-570-517-135-613-132-163-164-136-301-316-675-726-267-675-765-675-671-2_W.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 310-675-675 657-657 621-213-312-312 153-162-130-103-175-167-136-126-610-631-326-162-671-671-102-671-103-126-132-103-176-137-160-103-162-216-103-654-216-163-160-612-612-163-126-6_V.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 162-326-123-621-162-620-261 321-671-765-756-651 123 261-263_W.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 267-672-617-627-672-652-567-675-672-627-657-623-321-126-326-671-625-362-653-672-576-675-627-675-627-562-652-572-756-576-657-672-625-657-672-623-167-617-367-163-136-135-531-675-7_W.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 675 627-671-673-632-756-576-671-706-576-765-361-675-673-637-762-673_W.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 267-267-267-217-120-210-120-201-201-201-210-201-120-120-120-102-210-102-512-132-126-136-106-136-310-137-132-103-163-162-163-137-136-165-316-132-316-102-176-617-136-106-163-173-1_V.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 163-361-130-136-103-130-163-612-167-160 631-136-716-176-160-103-163-136-316-130-136-136-163-162-136-621-312-162-607-671-103-321-601-163-103-163-673-106-136-136-316-267-623-567-3_V.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 632-132-106-103-610-630-631-302-260-603-621-126-260-236-261-236-231-106-175-612-512-612-102-671-136-167-765-176-103-163-157-167-176 574-574-756-623 627-267-267-261-231-263-263-2_W.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 672-375-317-362-176-172-176-137-176-671-756-765-765-761-765-761-716-765-765-765-756-574-126-216-126-621-627-756-576-756-765-756-572-175-567-576-657-267-765-567-672-671-627-623-6_W.mp3)
  Segment 43: 66192ms ~ 66768ms (file: 572-567-657-615-216-261-261-621-261-261-621-547-574-652-625-265-564-657-615-267-273-372-375-372-375-372-371-371-372-317-657-672-675-672-672-132-675-675-675-675-675-675-675-675-6_W.mp3)
  Segment 44: 67768ms ~ 68344ms (file: 321-326-236-263-362-362-236-326-326-637-625-623-632-312-632-162-632-623-632-326-362-236-362-263-237-632-263-326-236-623-632-362-231-362-623-326-326-362-326-362-265-265-526-321-3_W.mp3)
  Segment 45: 69344ms ~ 69920ms (file: 102-103-106-213-132-126-124-163-106-123-512-167-165-672-126-106-623-260-175-136-126-256-612-132-136-126-316-176-176-605-163-160-160-103-163-132-206-612-607-631-136-671-126-320-6_V.mp3)
  Segment 46: 70920ms ~ 71496ms (file: 621-162-167-160-615-617-651-165-165-167-137-176-167-671-617-356-365-315-231-213-236-216-326-612-351-156-125-136-231-213-263-261-261-267-263-261-263-562-567-562-652-627-260-261-2_W.mp3)
  Segment 47: 72496ms ~ 73072ms (file: 621-263-163-263-106-651-576-675-602-160-615-651-657-160-106-601-162-260-162-612-306-156-163-106-567-165-761-167-165-361-136-627-135-174-132-627-316-126-106-715-162-162-623-623-2_V.mp3)
  Segment 48: 74072ms ~ 74648ms (file: 164-613-621-126-261-263 267-267-137-312-316-163-567-567-576-547-576-123-261-321-672-260-312-231-765-671-617-167-126-602-627-672-762-672-627-107-107-176-617-213-312-132-756-267-6_V.mp3)
  Segment 49: 75648ms ~ 76224ms (file: 175-765-675-765-765-607-670-162-126-132-701-376-316-371-617-761-756-576-574-576-627_W.mp3)
  Segment 50: 77224ms ~ 77800ms (file: 715-701-750-713-137-302-706-761-307-307-675-675-765-765-302-302-302-267-103-570-603-360-602-672-301-306-302-302-130-310-206-230-236-213-310-301-306-307-206-765-756-762-762-607-7_W.mp3)
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
> **Time**: 2026-04-01 04:38:17
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "안녕하세요 여러분 반갑습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "여러분", "start_ms": 230, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 360, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "오늘 공부할 내용은 무엇인가요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 30, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "공부할", "start_ms": 150, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "내용은", "start_ms": 290, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "무엇인가요", "start_ms": 420, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "일상 대화 표현을 배워봅시다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "일상", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "대화", "start_ms": 160, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "표현을", "start_ms": 280, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "배워봅시다", "start_ms": 410, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "첫 번째 문장은 실례합니다입니다",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "첫", "start_ms": 25, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "번째", "start_ms": 85, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "문장은", "start_ms": 175, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "실례합니다입니다", "start_ms": 300, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "어디서나 자주 쓰이는 말이죠",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "어디서나", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "자주", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "쓰이는", "start_ms": 310, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "말이죠", "start_ms": 430, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "실례합니다 길 좀 물어볼게요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "실례합니다", "start_ms": 35, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "길", "start_ms": 225, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 285, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "물어볼게요", "start_ms": 345, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "네 아주 자연스러운 발음이에요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 110, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "자연스러운", "start_ms": 220, "dur_ms": 170, "phase_lr_deg": 0.0},
{"word": "발음이에요", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "다음은 얼마예요라는 질문입니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "다음은", "start_ms": 30, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "얼마예요라는", "start_ms": 160, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "질문입니다", "start_ms": 380, "dur_ms": 170, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "쇼핑할 때 필수적인 표현이죠",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "쇼핑할", "start_ms": 50, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 190, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "필수적인", "start_ms": 260, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "표현이죠", "start_ms": 430, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "이 사과 한 상자에 얼마예요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 35, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "사과", "start_ms": 95, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 195, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "상자에", "start_ms": 255, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "얼마예요", "start_ms": 385, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "참 잘하셨어요 응용도 훌륭해요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "참", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "잘하셨어요", "start_ms": 110, "dur_ms": 170, "phase_lr_deg": 0.0},
{"word": "응용도", "start_ms": 290, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "훌륭해요", "start_ms": 410, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "도움이 필요하면 언제든 말하세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "도움이", "start_ms": 30, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "필요하면", "start_ms": 150, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "언제든", "start_ms": 290, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "말하세요", "start_ms": 420, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "질문이 하나 더 있습니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "질문이", "start_ms": 60, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "하나", "start_ms": 190, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 300, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 370, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "네 어떤 질문인가요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 30, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 100, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "질문인가요", "start_ms": 210, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "한국 음식을 추천해 주세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "한국", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "음식을", "start_ms": 150, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "추천해", "start_ms": 270, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 400, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "비빔밥이나 불고기가 인기 많아요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "비빔밥이나", "start_ms": 35, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "불고기가", "start_ms": 225, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "인기", "start_ms": 375, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "많아요", "start_ms": 455, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "매운 음식을 좋아하신다면 떡볶이도 있죠",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "매운", "start_ms": 30, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "음식을", "start_ms": 130, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "좋아하신다면", "start_ms": 250, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "떡볶이도", "start_ms": 420, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "있죠", "start_ms": 540, "dur_ms": 30, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "와 다 맛있어 보이네요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "와", "start_ms": 50, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 120, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "맛있어", "start_ms": 180, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "보이네요", "start_ms": 310, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "나중에 꼭 한번 드셔 보세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "나중에", "start_ms": 40, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "꼭", "start_ms": 180, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "한번", "start_ms": 250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "드셔", "start_ms": 360, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "보세요", "start_ms": 450, "dur_ms": 110, "phase_lr_deg": 0.0}
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
"transcript": "좋은 추천 감사합니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 120, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "추천", "start_ms": 220, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 330, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "천만예요 수업을 계속할까요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "천만예요", "start_ms": 35, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "수업을", "start_ms": 200, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "계속할까요", "start_ms": 345, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "(music)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 24,
"transcript": "이 문장을 한번 읽어 보세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 45, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "문장을", "start_ms": 105, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "한번", "start_ms": 225, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "읽어", "start_ms": 325, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "보세요", "start_ms": 415, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "저는 한국 드라마를 좋아해요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 30, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 130, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "드라마를", "start_ms": 240, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "좋아해요", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "아주 잘 읽으셨어요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "아주", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 150, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "읽으셨어요", "start_ms": 230, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "드라마를 보면 듣기 연습에 좋아요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "드라마를", "start_ms": 35, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "보면", "start_ms": 185, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "듣기", "start_ms": 295, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "연습에", "start_ms": 385, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "좋아요", "start_ms": 505, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "추천해주실 드라마가 있나요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "추천해주실", "start_ms": 50, "dur_ms": 190, "phase_lr_deg": 0.0},
{"word": "드라마가", "start_ms": 250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있나요", "start_ms": 410, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "여러 가지가 있는데 메일로 리스트 보내줄게요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "여러", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "가지가", "start_ms": 150, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "있는데", "start_ms": 270, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "메일로", "start_ms": 380, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "리스트", "start_ms": 500, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "정말 감사합니다 선생님",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 140, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "선생님", "start_ms": 370, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "자 그럼 오늘 배운 걸 복습해 봅시다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "자", "start_ms": 45, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "그럼", "start_ms": 105, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 195, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "배운", "start_ms": 295, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "걸", "start_ms": 385, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "복습해", "start_ms": 445, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "봅시다", "start_ms": 555, "dur_ms": 10, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "기억나는 표현 세 가지만 말해 보세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "기억나는", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "표현", "start_ms": 190, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "세", "start_ms": 300, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "가지만", "start_ms": 360, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "말해", "start_ms": 480, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "보세요", "start_ms": 550, "dur_ms": 20, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "실례합니다 얼마예요 그리고 맛있어요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "실례합니다", "start_ms": 35, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "얼마예요", "start_ms": 225, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 375, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "맛있어요", "start_ms": 485, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "백 점 만점에 백 점입니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "백", "start_ms": 30, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "점", "start_ms": 100, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "만점에", "start_ms": 160, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "백", "start_ms": 280, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "점입니다", "start_ms": 350, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "정말 똑똑하시네요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "똑똑하시네요", "start_ms": 165, "dur_ms": 260, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "선생님이 잘 가르쳐주신 덕분이에요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "선생님이", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 200, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "가르쳐주신", "start_ms": 270, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "덕분이에요", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "이제 어느덧 헤어질 시간이네요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "어느덧", "start_ms": 150, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "헤어질", "start_ms": 270, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "시간이네요", "start_ms": 400, "dur_ms": 170, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "오늘 수업 어떠셨나요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "수업", "start_ms": 140, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "어떠셨나요", "start_ms": 240, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "매우 유익하고 즐거웠습니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "매우", "start_ms": 35, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "유익하고", "start_ms": 155, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "즐거웠습니다", "start_ms": 305, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "저도 함께해서 기뻤습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "저도", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "함께해서", "start_ms": 150, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "기뻤습니다", "start_ms": 300, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "다음 수업 때까지 안녕히 계세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "다음", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "수업", "start_ms": 140, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "때까지", "start_ms": 230, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 350, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "계세요", "start_ms": 480, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "선생님도 안녕히 가세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "선생님도", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 240, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 390, "dur_ms": 120, "phase_lr_deg": 0.0}
]
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
"transcript": "(music)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 45,
"transcript": "그럼 진짜 마칠게요 안녕",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 150, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "마칠게요", "start_ms": 270, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "안녕", "start_ms": 410, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "안녕히 가세요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 35, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 185, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "다음에 또 봐요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 50, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 190, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "봐요", "start_ms": 270, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "건강 유의하시고요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "건강", "start_ms": 40, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "유의하시고요", "start_ms": 180, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "행복한 하루 되세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "행복한", "start_ms": 30, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "하루", "start_ms": 180, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "되세요", "start_ms": 300, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "진심으로 응원합니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "진심으로", "start_ms": 45, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "응원합니다", "start_ms": 275, "dur_ms": 260, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-01 04:38:17*
