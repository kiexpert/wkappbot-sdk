# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=08:11:15  uptime=24720s  mem=1752MB  system_processes=445"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_081106.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 310-130-310-316-312-326-231-321-263-236-213-261-263-126-213-327-651-521-126-623-236-321-265-657-326-261_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 103-312-512-126-621-713-107-753-137-160-103-136-163-106-103-561-621-613-152-106-316-173-103-610-163-756-716-716-561-136-107-716-263-162-103-125-602-607-650-162-165-173-657-657-6_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 627-132-126-631-567-576-625-612-163-310_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 627-576-657-625-136-302-370-367-137-715-167-176-167 762 213 236-362-370-321-132-603-602-620-602-632-310-316-765-672-675-651-657-517-561-153-617-672-163-362-162-675-672-627-625-6_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 765-367 765-617-765-756-762-672-104-103 765-137-132-103-672-617_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 326-302-312-231-362-326-136-631-203-306-160-261-267-216-623-602-316-163-631-163-612-602-625-657-620-265-625-623-620-620-621-632-306-312-316-301-603-637-617-163-637-763-367-376-1_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 765-765-765-765-765-673-567-576-675_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 615-521-632-167-163-716-321-176-103-130-263-672-102-675-136-613-316-361-103-136-316-163-130-106-316-163-610-106-163-103-523-672-172-106-126-162-160-136-130-173-130-103-136-106-7_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 103-136-143-103-321-302-672-675-657-765-657-675-564-371-273-136-167-106-173-371-302-162-106-102-216-231-120-126-163-162-106-162-106-167-561-652-263-260-263-652-675-675-103-137 1_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 675-675-276-236-721-326-127-130-720-732-317-372-736-312-316-312-320-316-162-312-201-136-612-123-321-236-301-310-134-132-123-160-163-136-132-132 573-320-675-574-576-275-217-316-1_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 671-627-627-627-672-267-627-672-627-762-627-672-267-627-276-672-167-102-201-213-216-261-206-216-132-216-231_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 315-321-361-362-316-531-536-365-156-514-573-153-135-637-236-637-670-263-261-521-523-572-534-537-574-657-326-253-326-326-326-326-321-236-321-321-231-321-235-235-316-361-326-312-3_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 156-615-617-106-671-103-617-561-176-706_V.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 167-670-671-617-327-326-564-106-310-315-150-165-612-675-672-306-136-362-203-621-201-216-261-612-106-547-321-320-236-320-302-632-620-162-216-162-162-627-621-637-361-123-765-765-7_V.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 632-163-136-106-103-103-621-627-632-621-613-106-106-132-103 310-623-621-162-106-106-136-106-265-126-106-130-130-627-106-103-613-132-103-103-165-106-176-715-103-106-136-123-123-7_V.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 756-765-760-316-130-103-301-671-735-165-102-761-765-671-310-132-263-126-126-631-167-376-763-765-762-163-567-263-263-263-306-163-617-620-603-670-627-637-627-671-672-675-671-673-6_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 623-263-263-762-763 726-672-762-760-761-167-136-136 672-126-136-312-132-312-162-312-615-136 672-312-316-163-607-607-674 126 312-156-760-675-675-627-671-167 703-574-753-375-750-7_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 132 312-136-632-312 136 132-574-574-106 547 136 315 261_V.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 512-125-372-317-376-103-120-132-275-103-712-321-132-125-302-321-312-130-310-130-310-210-302-230-302-512-123-312-321-231-153-206-213-123-325-326-325-675-672-326-236-123-132-276-3_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 673-126-102-162-123-132-136-617-163-617-602-623-130-107-167-163-132-160-613-756-623-612-163-136-136-103-136-163-136-106-102-103-601-132-210-123-263-261-102-126-125-130-163-126-6_V.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 671-107-106-167-213-761-765-726-736-312-316-631-753-765-675-756-576-756-765-617-176-617 132-163-164-103-126-156-176-163-106-651-610-165-163-136-103-136-103-135-621-137-106-136-1_V.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 637-361-316-306-316-321-310-312-361-301-316-312-567-567-560-527-672-607-607-670-617-627-267-263-625-627-623-265-267-267-126-165-516-573-567-567-567-576-576-576-576-576-576-576-7_W.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 301-312-672-162-312-132-321-230-321-137-312-312-312-312-301-130-130-137-137-317-730-130-310-316-301-103-312-316-306-361-316-765-756-301-316-632-135-156-620-326-123-132-315-130-2_V.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 672-675-312-316-312-213-126-173-127-216-371-123-167-375-326-670-675-675-672-607-672-760-706-673-673-612-612-126-361-361-316-321-361-623-153-637-706-762-713-136-167-123-130-103-1_V.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 126-670-160-163-653-163-607-126-157-573-567-201-132-106-613-314-320-321-312-310-103-301-301-136-136-610-136-321-203-102-163-321-312-316-623-261-367-163-136-152-176-620-623-165-1_V.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 302 236-321-130-107-103 103-130-301-301-310-627-602-372-306-123-103-132-103-236-312-321-312-132-312-756-750-310-756-716-176-137-301-132-103-136-570-315-137-134-137 756-576-756-7_V.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 267-362-263-627-625-623-326-362-326-362-236-236-326-326-263-326-326-321-326-321-327-326-327-326-327-312-312-372-273-237-231-327-237-237-237-267-236-263-267-263-263-236-623-267-6_W.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 632-263-310-312-132-312-132-576-572 263-231-230-132-312-326-621-623-163-310-310-103-130-310-310-301-130-103-103-312-132-301-132-312-301-123-632-602-621-306-302-301-310-327-236-3_V.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 103-132-103-312-231-210-301-301-103-231-210-236-203-203-206-203-267-261-263-263-623-362-263-267-362-231-361-106-312-301-312-301-320-103-102-102-310-315-152-136-530-267-627-612-1_W.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 106-103-302-136-163-103-106-136-365-106-263-175-316-765-263 273-261-216-236-231-263-657-567-672-625-361-623-263-276-621-217-123-675-623-620-261-106-106-143-316-162-103-601-162-3_V.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 671-163-103-156-130-103-156-126-217-132-123-316-106-136-160-617-130-617-163-103-103-103-136-163-136-176-627-623-126-671-103-163-136-162-751-126-631-103-160-136-167-621-267-632-6_V.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 132-136-126-213-163-167-175-752-735-562-576-567-576-157-365-637-567-156-756-567-156-765-657-367-165-637-365-612-567-253-756-567-613-513-625-365-567-673-657-621-613-635-716-576-7_W.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 627-627-130-130-316-312-130-102-123-623-627-627-672-675-613-123-312-765-765-756-765-675-765-657-765-765-756-572-752-572-573 761-176-761-761-671-762-675-756-716-762-763-276-267-3_W.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 176-572-162-103-601-103-601-160-162-106-162-631-126-761-167-163-107-136-175-617-267-571-576-675-631-617-675-627-526-267-267-263-236-267-621-263-267-263-267-672-627-576-567-576-5_W.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 126-126-316-124-126-213-127-126-267-123-132-326-213-627-237-261-276-375-617-365-235-761-142-364-614-571-671-425-276-351-251-123-651-674-513-451-461-235_V.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 517-157-571-103 263-261-216-276-627-276-627 756-675-673-375-376-275-236-316-623-267-627-263-675-673-316-672-637-267-613-316 621-627-123-751-132-751-106-130-106-170-157-316-102-7_W.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 136-516-561-351-362-132-213-236-231-230-230-321-301-316-130-316-136-361-103-163-163-613-162-602-306-306-361-301-602-106-106-617-167-167-136-231-321-123-163-106-162-106-106-160-6_V.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 253 712-762-672-162-613-631-632-162-625-175-372-627 163-103-136-103-105-601-103-160-167-130-103-615-106-617-657-103-136-123-132-162-102-103-106-130-163-130-167-106-671-362-130-6_V.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 610-601-607-670-675-267-625-612-602-632-362-260-260-261-612-162-167-106-167-167-106-651-601-673-617-167-706-107-360-132-263-602-602-310-312-160-126-126-123-312-263-132-310-137-1_V.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 175-571-157-127-671-670-675-567-567-657-612-261-671-627-673-672-671-627-651-675-671-670-761-765-675-765-761-760-670-672-670-672-657-152-126-126-127-172-123-173-176-176-761-712-6_W.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 361-602-627-672-136-106-103-516-127-762-176-657-103-576-172-162-126-761-103-103-651-106-132-136-132-312-163-103-105-156-167-167-162-132-136-312-312-106-167-672-132-576-610-267-6_W.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 162-123-132-176-673-617-316-167-361-761-176-163-126-136-163-123-176-761-173-765-176-167-671-136-103-106-312-173-103-763-761-671-761-671_V.mp3)
  Segment 43: 66192ms ~ 66768ms (file: 167-162-167-613-106-617-613-136-152-632-176-163-106-756-163-136-316-176-106-167-163-152-106-103-571-157-765-106-156-136-542-136-162-126-312-136-103-106-103-103-157-163-106-137-1_V.mp3)
  Segment 44: 67768ms ~ 68344ms (file: 301-127-756-756-765-237-127-675-765-657-567-675-675-106-162-126-132-126-162-632-123-612-361-206-126-213-362-163-162-176-567-576-756-756-536-132-312-103-130-123-765-675-765-765-7_W.mp3)
  Segment 45: 69344ms ~ 69920ms (file: 613-657-651-172-213-605-362-360-103-106-106-130-136-103-601-652-623-326-263-612-617-567-657-567-615-561-567-627-657-652-652-316-627-675-673-627-632-165-106-165-175-576-710-176-1_W.mp3)
  Segment 46: 70920ms ~ 71496ms (file: 103-136-156-153-136-156-167-163-136-162-103-103-165-136-167-136-103-103-106-156-153-130-165-130-617-103-526-165-160-137-261-267-365-123-723-726-271-672 675-673_V.mp3)
  Segment 47: 72496ms ~ 73072ms (file: 103-137-162-162-162-132-320-302-312-512-132-231-132-321-213-760-130-170-765-706-765-560-576-301-103-136-132-123-316-312-316-136-137-106-103-102-132-103-312-130-230-260-362-130-1_V.mp3)
  Segment 48: 74072ms ~ 74648ms (file: 754-756-573-576-570 341-132-136-136-316-136-607-675-607-160-106-620-621-621-612-163-163-103-762-627-326-312-351-501-513-631-261-603-617 672-602-612-617-621-623-361-362-631-261-2_V.mp3)
  Segment 49: 75648ms ~ 76224ms (file: 130-132-320-302-132-540 136-251-530-523-253-675-567-356-253 230-231-231-236-231 237-203-312-132-102-752-302-312-132-106-103-153-362-316-167-617-675 572-607-571-573-573-735-625-7_W.mp3)
  Segment 50: 77224ms ~ 77800ms (file: 257-576-576-567-657-106-613-612-610-106-106-162-132-756-106-167-106-316-361-163-106-106-167-106-617-167-310-231-320-502-532-203-502-102-126-603-206-620-627-612-162-163-136-106-1_V.mp3)
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
> **Time**: 2026-04-03 08:12:10
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "오늘 배운 문법을 문장으로 만들어 봅시다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "배운", "start_ms": 160, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "문법을", "start_ms": 290, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "문장으로", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "만들어", "start_ms": 580, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "봅시다", "start_ms": 710, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "저는 한국 드라마를 아주 좋아해요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 30, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "한국", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "드라마를", "start_ms": 280, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "아주", "start_ms": 440, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "좋아해요", "start_ms": 560, "dur_ms": 190, "phase_lr_deg": 5.0}
]
},
{
"segment": 3,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 4,
"transcript": "드라마에서 배운 표현이 있나요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "드라마에서", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "배운", "start_ms": 240, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "표현이", "start_ms": 360, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "있나요", "start_ms": 490, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 6,
"transcript": "정말 감사합니다 같은 말을 자주 들었어요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "감사합니다", "start_ms": 160, "dur_ms": 190, "phase_lr_deg": 5.0},
{"word": "같은", "start_ms": 360, "dur_ms": 90, "phase_lr_deg": 5.0},
{"word": "말을", "start_ms": 460, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "자주", "start_ms": 570, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "들었어요", "start_ms": 690, "dur_ms": 150, "phase_lr_deg": 5.0}
]
},
{
"segment": 7,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 8,
"transcript": "정확한 표현이네요 일상에서도 많이 쓰여요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "정확한", "start_ms": 60, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "표현이네요", "start_ms": 200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "일상에서도", "start_ms": 360, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 550, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "쓰여요", "start_ms": 650, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "이제는 자막 없이도 조금씩 들리기 시작해요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "이제는", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "자막", "start_ms": 165, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "없이도", "start_ms": 275, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "조금씩", "start_ms": 405, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "들리기", "start_ms": 525, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "시작해요", "start_ms": 665, "dur_ms": 140, "phase_lr_deg": 5.0}
]
},
{
"segment": 10,
"transcript": "정말 장족의 발전이네요 축하드려요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "장족의", "start_ms": 165, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "발전이네요", "start_ms": 305, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "축하드려요", "start_ms": 495, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "더 열심히 해서 유창하게 말하고 싶어요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "더", "start_ms": 30, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "열심히", "start_ms": 100, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "해서", "start_ms": 240, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "유창하게", "start_ms": 350, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "말하고", "start_ms": 500, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "싶어요", "start_ms": 630, "dur_ms": 140, "phase_lr_deg": 5.0}
]
},
{
"segment": 12,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 13,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 14,
"transcript": "그 마음 변치 말고 계속 노력해 봅시다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "그", "start_ms": 55, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "마음", "start_ms": 115, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "변치", "start_ms": 235, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "말고", "start_ms": 355, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 465, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "노력해", "start_ms": 585, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "봅시다", "start_ms": 725, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "네 선생님 오늘도 감사했습니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 25, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "선생님", "start_ms": 95, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "오늘도", "start_ms": 245, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "감사했습니다", "start_ms": 385, "dur_ms": 220, "phase_lr_deg": 5.0}
]
},
{
"segment": 16,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
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
"transcript": "오늘 수업은 여기까지입니다 고생하셨어요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "수업은", "start_ms": 160, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "여기까지입니다", "start_ms": 300, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "고생하셨어요", "start_ms": 550, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "조심히 들어가시고 다음에 만나요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "조심히", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "들어가시고", "start_ms": 200, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "다음에", "start_ms": 390, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 520, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "안녕히 가세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 60, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "가세요", "start_ms": 220, "dur_ms": 320, "phase_lr_deg": 5.0}
]
},
{
"segment": 21,
"transcript": "바이 바이",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "바이", "start_ms": 35, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "바이", "start_ms": 205, "dur_ms": 371, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 23,
"transcript": "안녕",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "안녕", "start_ms": 45, "dur_ms": 531, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 25,
"transcript": "진짜 안녕",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 30, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "안녕", "start_ms": 180, "dur_ms": 396, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 27,
"transcript": "다음에 봐요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 50, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "봐요", "start_ms": 220, "dur_ms": 356, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "행복하세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "행복하세요", "start_ms": 40, "dur_ms": 536, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 30,
"transcript": "바이",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "바이", "start_ms": 25, "dur_ms": 551, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "진짜 진짜 안녕",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 60, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 210, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "안녕", "start_ms": 360, "dur_ms": 216, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 33,
"transcript": "수고",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "수고", "start_ms": 35, "dur_ms": 541, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "고생",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "고생", "start_ms": 45, "dur_ms": 531, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 36,
"transcript": "안녕히",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 25, "dur_ms": 551, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "잘 가",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "잘", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "가", "start_ms": 160, "dur_ms": 416, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 39,
"transcript": "끝",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "끝", "start_ms": 50, "dur_ms": 526, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "진짜 끝",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 60, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "끝", "start_ms": 210, "dur_ms": 366, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 42,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 43,
"transcript": "바이",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "바이", "start_ms": 45, "dur_ms": 531, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "안녕",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "안녕", "start_ms": 30, "dur_ms": 546, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 46,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 47,
"transcript": "끝",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "끝", "start_ms": 40, "dur_ms": 536, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "수고",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "수고", "start_ms": 50, "dur_ms": 526, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 50,
"transcript": "진짜 안녕",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "안녕", "start_ms": 190, "dur_ms": 386, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 08:12:10*
