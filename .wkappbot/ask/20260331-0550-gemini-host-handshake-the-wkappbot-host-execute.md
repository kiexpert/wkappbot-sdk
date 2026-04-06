# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43304  time=05:49:25  uptime=12353s  mem=1384MB  system_processes=440"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260331_054915.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 170-163-107-713-567-103-130-103-675-167-610-160-317-136-327-721-130-107-136-316-103-316-136-106-316-136-162-362-312-130-136-650-265-136-756-716-261-317 163-167-130-362-123-621-1_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 672-623-362-632-261-326-162-162-623-721-762-236-632-576-627-613-263-265-236-265 765-765 627-621-627-657-615-162-574-547-567-567-576-765-675-653-567-562-576-576-751-657-675-276-5_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 162-572-527-261-261-567-657 316-312 610-672-671-267-312-123-213-132-361-623-162-631-312-312-162-621-623-632-613-162-631-612-623-613-162-163-612-631-162-361-623-623-623-632-623-6_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 521-132-316-317-167-631-310-132-316-352-136-301-362-320-203-703-716-165-106-106-103-106-160-106-301-103-301-132-103-130-750-716-576-176-716-317-321-326-325-713-371-726-506-513-6_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 623-627 157-165-156 263-623 670-657-675 172-172-132-672-671-132-123-615-731-713-162-623-612-627-627-627-617-106-367-763-762-762-762 367-163-312-574-754-623-326-631-263 176 236-2_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 257-320-263-263-260-263 623-136-362-613-756-763-312-136-130-765-671-320-326-261-623-126-103-160-316-321-306-136-651-165-712-625-571-657-567-136-103-201-213-120-160-136-107-706-3_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 627-621-621-267-625-526-561-127-265-625-157-156-716-574-537-375-165-621-126-163-136-657-627-362-672-564-523-526-524-523-132-263-523-265-265-562-235-215-621-267-267-271-615-625-3_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 612-123-126-167-632-261-130-103-106-136-317-167-671-167 716-263-617-163-136-136-106-103-132-106-367-617-106-615-103-103-316-163-130-172-672-617-612-106-163-136-170-103-317-650-1_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 203-206-670-576-756-675-673-630-130-310-306-301-537-203-263-132-312-136-130-132-103-173-103-312-301-301-103-301-510-517-502-510-312-263-203-320-302-231-103-216-123-203-162-162-6_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 102-675-675-675-675-675-675-672-675-765-675-675_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 637-326-120-132-213-312-132-263-621 362-273-267-263-627-623-537-765-326-625-623-362-236-316-361-623-126-216-623-321-213-132-126-326-657-657-312-310-571-253-657-675-672-623-623-3_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 623-625-623-623-257-216-612-236-236-572-657-276-316-326 312 321-236-263-301-301 605-621-623_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 316 163-103-135-162-176-165-623-136-126-126-275-156-136-237-613-671-162-673-123-162-621 312-102-765-756-756-750-765-765-765-627-231 136-163-263_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 756-570-762-672-362-362-312-326-321-362-136-760-672-675-607-603-637-706-317-103-132-123-260-213-203-326-126-123-326-627-174-173-123-503-573-137-301-132-236-216-132-102-130-132-1_V.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 362-263-263-326-235-263-652-136-135-573-657-625-625-652-256-163-361-231-321-563-263-263-325-356-351-357-365-326-573-165-361-536-236-326-326-362-326-265-526-623-265-312-256-632-6_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 716-136-162-163-176-127-316-672-176-675-617-130-162-136-130-107-176-671-176-316-176-167-317-137-176-103-637-263-132-173-261-610-716-167-167-321-326-136-751-621-756-765-107-163_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 367-562-562-653-365-657-567-675-657-657-756-756-756-675-675-765-762-673-627-675 562-576-576-675-267-267-627-623-162-756-321-302-213-213-326-326-310-673-762-362-632-627-617-306-1_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 607-672-607-132-637-176-173-671-670-670-607-627-267-627-267-236-263-231-263-265-320-302-320-623-230-602-326-321-132-312-301-310-310-630-601-136-176-173-106-601-615-136-102-130-3_V.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 752-725-753-674-543-540-573-576-573-357-540-547-371-317-715-572-152-712-547-574-570-237-237-231-231-235-231-231-375-326-326-326-273-275-273-327-756-375-317-317-534-351-536-356-5_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 765-762-236-127-762-726-172-123 753 127-763-762-217-123_W.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 107-163-571 613-103-106-162-103-132-123-213-231-231-213-217-210-203-213-216-213-260-362-236-632-230-574-572-365-103-162-371-301-312-321-132-216-203-362-756-123-137-671-362-632-6_V.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 231-321-231-321-231-321-236-231-321-236-326-231-321-321-231-231-321-235-231-236-213-236-231-231-231-213-261-213-231-236-213-231-213-216-213-231-261-231-235-265-236-216-261-236-2_W.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 635-617-675-657-653-160-276-675-672-261-631-625-625-627-735-762-672-126-136-562-352-572-356-135-765-657-675-657-613-623-173-607-206-102-126-102-361-163-261-261-312-231-132-102-2_W.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 762-763-372-267-762-672-762-762-712-327-761-376-732-753-726-267-762-762-716-765-756-675-627-137-213-321-126-236-276-261-321-132-213-210-231-231-263-623-236-263-231-263-123-312-6_W.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 523-256-236-265-267-260-327-321-256-235-231-627-672-267-257-265-257-236-321-321-326-236-235-321-574-753-372-372-237-327-321-321-230-527-521-751-753-576-357-573-231-276-231-327-3_W.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 103-310-310-130-301-130-301-765-567-756-576-570-756-675-610-561-617-165-126-106-671-657-657-657-675-617-617-127-103-705-571-765-576-675-617-106-567-613-103-103-132-617-612-167-6_V.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 132-126-321-132-375-567-713-341-132-175-754-213-326-613-261-213-341-162-132-631-263-213-241-143-213-234-132-312-627-136 764-413-526-132-321-135-325-136-175-175-715-751-537-357-6_V.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 130-103-123-312-103-132-312-361-371-312-301-750-260-263-302-312-321-302-301-130-103 307-203-230-203-236-206 735-761-716-760-706-372-574-573-256-607-547-507-235-176-351-263-261-2_V.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 265-265-261-265-236-265-625-625-263 257-532-570-572-267-267-276-267-263-236-235-254-235-325-352-357-357 361-150-506-157-135-367-375-375-375-361-613-651-534-517-521-517-567-153-2_W.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 263-623-621-625-623-625-625-265-625-267-627-267-265-652-256-265-265-623-623-612-162-612-653-637-526-562-567-562-657-675-152-215-315-526-312-312-361-316-316-162-136-361-635-163-3_W.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 526-562-567-765-276-267-267-325-132-213-523-236-231-236-756-726-762-362-627-672-672-627-672-627-231-675-627-675-756-765-762-567-136-132-136-621-672-542-356-316-316-657-625-267-5_W.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 136-201-762-632-170-376 613-103-136-103-132-602-632-137-162-610-132-132-260-167-127-130-632-136-132-136-617-610-163-617-163-102-163-517-136-136 561-201-765-106-176-160-671-106-5_V.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 130-301-103-103-615-160-106-162-160-106-160-625-162-625-106-106-310-371-310-231-723-317-213-261-315-321-231-321-236-321-321-320-312-312-321-312-231-236-265-123-231-216-231-321-2_W.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 103-162-526-567-632-362-623-612-627-621-632-316-312-136-625-625-621-236-263-321-326-123-621-627-631-621-132-623-623-623-263-236-613-657-613-136-136-652-627-263-623-162-236-362-6_W.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 137-136-173-172-672-721-362-632-623-362-362-361-613-236-263-621-632-236-627-627-672-671-672-627-657-621-621-261-627-627-673-670-367-213-231-231-213-621-261-216-213-231-213-312-2_W.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 576-756-576-576-567-576-765-762-672-673-267-625-627-620-576-576-576-571-571-576-576-754-574-574-762-675-675-176-756-375-367-673-357-365-375-317-751-175-137-317-631-163-216-231-1_W.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 267-263-162-163-103-136-621-103-136-673-123-153-136-312-302-103-162-106-376-162-621-612-163-165-157-157-152-670-160-163-163-137-132-154-132-103-132-103-132-103-175-160-716-175-1_V.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 675-675-657-156-176 765 372-307-320-302-307-376-376-367-637 356-307-756-670-301-175_W.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 163-610-136-132-102-167-510-652-132-126-136-163-670-123-307-167-162-102-123-132-321-132-132-130-132-231-670-672-316-261-632-321-321-167-163-160-617-163-167-162-201-106-102-102-6_V.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 671-657-126-201-620-260-261-201-203-316-716-103-132-103-631-136-160-620-206-136-103-316-312-302-105-106-316-301-310-301-321-132-103-104-103-106-236-130-315-312-167-132-123-301-3_V.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 260-310-657-675-657-567-657-650-260-302-326-130-132-103-103-103-106-312-574-570-206-206-206-206-302-504-570-750-307-312-203-236-326-103-132-362-231-143-132-413-132-413-132-143-1_V.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 257-376-156-651-103-312-612-143-136-326-312 654-172-672-567-617-125-134-103-106-103-523-237-736-612-102-167-106-167-167-162-130-132-167-617-312-137-623-167-165-312-362-163-103-3_V.mp3)
  Segment 43: 66192ms ~ 66768ms (file: 135-153-153-154-153-315-135-351-153-153-136-135-516-213-237-213-231-236-231-236-236-263-235-236-253-527-576-352-257-236-576-615-361-261-213-216-261-231-326-623-621-132-361-657-3_W.mp3)
  Segment 44: 67768ms ~ 68344ms (file: 657-165-617-672-627-256-567-756-617-162-167-103-106-103-103-103-106-103-106-106-106-103-137-102-312-137-103-103-136-130-130-573-756-107-103-130-107-107-750-176-672-627-627-106-1_V.mp3)
  Segment 45: 69344ms ~ 69920ms (file: 763-761-576-576-157-173-762-675-376-167-160-136-103-123-312-236-206-261-231-621-623-267-612-621-612-261-216-623-637-126-623-136-312-762-726-267-263-623-627-627-625-657-627-265-6_W.mp3)
  Segment 46: 70920ms ~ 71496ms (file: 675-765-675-675-154-513-762-756-756-765-765-576-765-754-574-754-567-567-765-763-731-765-576-576-576-752-765-672-670-621-672-607-602-607-627-136-760-137-621-602-623-130-103-132 7_W.mp3)
  Segment 47: 72496ms ~ 73072ms (file: 312-102-267-261-621-163-320-302-312-310-312-132-130-130-103-103-136-103-107-103-103-173-107-137 361-136-672-617-162-613-631-123-267-326-126-136-163_W.mp3)
  Segment 48: 74072ms ~ 74648ms (file: 130-657-657-657-623-126-725-231-756-753 613-617-650-607-672-672-765-765-725-627-627-607-167-160-106-167-106-132-310-130-103-103-102-130-706-167-162-165-607-607-612-167-162-123-1_W.mp3)
  Segment 49: 75648ms ~ 76224ms (file: 103-576-576-576-310-361-132-163-102-103-156 103-106-103-103-163-136-316-130-136-136-132-621-162 136-162-130-132 231-236-163-125-312-736-703-301-132-103-137 132-312-132-312-123-7_V.mp3)
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
> **Time**: 2026-03-31 05:50:12
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "오늘 회의에서 발표할 내용을 정리해왔습니다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "회의에서", "start_ms": 165, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "발표할", "start_ms": 315, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "내용을", "start_ms": 435, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "정리해왔습니다.", "start_ms": 525, "dur_ms": 51, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "네, 같이 보면서 이야기해 볼까요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 30, "dur_ms": 60, "phase_lr_deg": -5.2},
{"word": "같이", "start_ms": 100, "dur_ms": 90, "phase_lr_deg": -5.0},
{"word": "보면서", "start_ms": 200, "dur_ms": 120, "phase_lr_deg": -5.3},
{"word": "이야기해", "start_ms": 330, "dur_ms": 140, "phase_lr_deg": -5.1},
{"word": "볼까요?", "start_ms": 480, "dur_ms": 96, "phase_lr_deg": -5.2}
]
},
{
"segment": 3,
"transcript": "이번 프로젝트의 핵심은 사용자 경험 개선입니다.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "이번", "start_ms": 60, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "프로젝트의", "start_ms": 160, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "핵심은", "start_ms": 300, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "사용자", "start_ms": 420, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "경험", "start_ms": 510, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "개선입니다.", "start_ms": 560, "dur_ms": 16, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "Great. What is the specific target audience?",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Great.", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": -4.8},
{"word": "What", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": -4.9},
{"word": "is", "start_ms": 250, "dur_ms": 60, "phase_lr_deg": -4.7},
{"word": "the", "start_ms": 320, "dur_ms": 40, "phase_lr_deg": -4.8},
{"word": "specific", "start_ms": 370, "dur_ms": 110, "phase_lr_deg": -4.9},
{"word": "target", "start_ms": 490, "dur_ms": 60, "phase_lr_deg": -4.7},
{"word": "audience?", "start_ms": 560, "dur_ms": 16, "phase_lr_deg": -4.8}
]
},
{
"segment": 5,
"transcript": "20대와 30대 직장인들을 주요 타겟으로 설정했습니다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "20대와", "start_ms": 50, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "30대", "start_ms": 190, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "직장인들을", "start_ms": 310, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "주요", "start_ms": 460, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "타겟으로", "start_ms": 530, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "설정했습니다.", "start_ms": 575, "dur_ms": 1, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "그들이 가장 불편해하는 점은 무엇인가요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "그들이", "start_ms": 35, "dur_ms": 110, "phase_lr_deg": -5.0},
{"word": "가장", "start_ms": 155, "dur_ms": 90, "phase_lr_deg": -5.1},
{"word": "불편해하는", "start_ms": 255, "dur_ms": 130, "phase_lr_deg": -4.9},
{"word": "점은", "start_ms": 395, "dur_ms": 90, "phase_lr_deg": -5.2},
{"word": "무엇인가요?", "start_ms": 495, "dur_ms": 81, "phase_lr_deg": -5.0}
]
},
{
"segment": 7,
"transcript": "복잡한 결제 과정과 느린 로딩 속도입니다.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "복잡한", "start_ms": 60, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "결제", "start_ms": 180, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "과정과", "start_ms": 280, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "느린", "start_ms": 400, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "로딩", "start_ms": 470, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "속도입니다.", "start_ms": 550, "dur_ms": 26, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "We should simplify the UI immediately.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "We", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": -4.5},
{"word": "should", "start_ms": 110, "dur_ms": 110, "phase_lr_deg": -4.6},
{"word": "simplify", "start_ms": 230, "dur_ms": 150, "phase_lr_deg": -4.4},
{"word": "the", "start_ms": 390, "dur_ms": 40, "phase_lr_deg": -4.5},
{"word": "UI", "start_ms": 440, "dur_ms": 60, "phase_lr_deg": -4.6},
{"word": "immediately.", "start_ms": 510, "dur_ms": 66, "phase_lr_deg": -4.4}
]
},
{
"segment": 9,
"transcript": "네, 디자인 팀과 협의하여 수정하겠습니다.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 55, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "디자인", "start_ms": 125, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "팀과", "start_ms": 245, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "협의하여", "start_ms": 345, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "수정하겠습니다.", "start_ms": 475, "dur_ms": 101, "phase_lr_deg": 0.0}
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
"transcript": "개발 일정은 무리가 없을까요?",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "개발", "start_ms": 30, "dur_ms": 90, "phase_lr_deg": -5.4},
{"word": "일정은", "start_ms": 130, "dur_ms": 110, "phase_lr_deg": -5.3},
{"word": "무리가", "start_ms": 250, "dur_ms": 110, "phase_lr_deg": -5.5},
{"word": "없을까요?", "start_ms": 370, "dur_ms": 206, "phase_lr_deg": -5.4}
]
},
{
"segment": 12,
"transcript": "추가 인력을 투입하면 가능합니다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "추가", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "인력을", "start_ms": 170, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "투입하면", "start_ms": 290, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "가능합니다.", "start_ms": 430, "dur_ms": 146, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "좋습니다. 비용 계획서도 작성해 주세요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "좋습니다.", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": -5.1},
{"word": "비용", "start_ms": 200, "dur_ms": 90, "phase_lr_deg": -5.2},
{"word": "계획서도", "start_ms": 300, "dur_ms": 140, "phase_lr_deg": -5.0},
{"word": "작성해", "start_ms": 450, "dur_ms": 90, "phase_lr_deg": -5.1},
{"word": "주세요.", "start_ms": 550, "dur_ms": 26, "phase_lr_deg": -5.2}
]
},
{
"segment": 14,
"transcript": "알겠습니다. 오후 회의 전까지 준비하겠습니다.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "알겠습니다.", "start_ms": 25, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "오후", "start_ms": 165, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "회의", "start_ms": 255, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "전까지", "start_ms": 345, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "준비하겠습니다.", "start_ms": 465, "dur_ms": 111, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "Thank you. Let's make this project a success.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "Thank", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": -4.2},
{"word": "you.", "start_ms": 165, "dur_ms": 60, "phase_lr_deg": -4.3},
{"word": "Let's", "start_ms": 235, "dur_ms": 100, "phase_lr_deg": -4.1},
{"word": "make", "start_ms": 345, "dur_ms": 90, "phase_lr_deg": -4.2},
{"word": "this", "start_ms": 445, "dur_ms": 50, "phase_lr_deg": -4.3},
{"word": "project", "start_ms": 505, "dur_ms": 60, "phase_lr_deg": -4.2},
{"word": "a", "start_ms": 570, "dur_ms": 5, "phase_lr_deg": -4.1},
{"word": "success.", "start_ms": 575, "dur_ms": 1, "phase_lr_deg": -4.3}
]
},
{
"segment": 16,
"transcript": "네, 최선을 다하겠습니다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 60, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "최선을", "start_ms": 130, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "다하겠습니다.", "start_ms": 280, "dur_ms": 296, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 18,
"transcript": "오늘 점심은 뭐 드실 건가요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "점심은", "start_ms": 170, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 290, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "드실", "start_ms": 360, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "건가요?", "start_ms": 460, "dur_ms": 116, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "I'm thinking about having some noodles.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "I'm", "start_ms": 35, "dur_ms": 80, "phase_lr_deg": -5.1},
{"word": "thinking", "start_ms": 125, "dur_ms": 140, "phase_lr_deg": -5.2},
{"word": "about", "start_ms": 275, "dur_ms": 90, "phase_lr_deg": -5.0},
{"word": "having", "start_ms": 375, "dur_ms": 110, "phase_lr_deg": -5.1},
{"word": "some", "start_ms": 495, "dur_ms": 50, "phase_lr_deg": -5.2},
{"word": "noodles.", "start_ms": 555, "dur_ms": 21, "phase_lr_deg": -5.0}
]
},
{
"segment": 20,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 21,
"transcript": "좋아요. 저도 면 요리 좋아해요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "좋아요.", "start_ms": 40, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "저도", "start_ms": 180, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "면", "start_ms": 280, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "요리", "start_ms": 350, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "좋아해요.", "start_ms": 470, "dur_ms": 106, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 23,
"transcript": "근처에 맛있는 칼국수 집이 있어요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "근처에", "start_ms": 60, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "맛있는", "start_ms": 200, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "칼국수", "start_ms": 320, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "집이", "start_ms": 470, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "있어요.", "start_ms": 550, "dur_ms": 26, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "Sounds perfect for this rainy day.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Sounds", "start_ms": 30, "dur_ms": 110, "phase_lr_deg": -4.2},
{"word": "perfect", "start_ms": 150, "dur_ms": 110, "phase_lr_deg": -4.3},
{"word": "for", "start_ms": 270, "dur_ms": 60, "phase_lr_deg": -4.1},
{"word": "this", "start_ms": 340, "dur_ms": 60, "phase_lr_deg": -4.2},
{"word": "rainy", "start_ms": 410, "dur_ms": 110, "phase_lr_deg": -4.3},
{"word": "day.", "start_ms": 530, "dur_ms": 46, "phase_lr_deg": -4.2}
]
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
"transcript": "비 오는 날에는 역시 따뜻한 국물이죠.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "비", "start_ms": 50, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "오는", "start_ms": 120, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "날에는", "start_ms": 210, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "역시", "start_ms": 330, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "따뜻한", "start_ms": 430, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "국물이죠.", "start_ms": 550, "dur_ms": 26, "phase_lr_deg": 0.0}
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
"transcript": "식당에 자리가 있을까요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "식당에", "start_ms": 45, "dur_ms": 130, "phase_lr_deg": -5.1},
{"word": "자리가", "start_ms": 185, "dur_ms": 110, "phase_lr_deg": -5.2},
{"word": "있을까요?", "start_ms": 305, "dur_ms": 271, "phase_lr_deg": -5.0}
]
},
{
"segment": 29,
"transcript": "보통 일찍 가면 자리가 있어요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "보통", "start_ms": 60, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "일찍", "start_ms": 180, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "가면", "start_ms": 280, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "자리가", "start_ms": 400, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "있어요.", "start_ms": 520, "dur_ms": 56, "phase_lr_deg": 0.0}
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
"transcript": "그럼 지금 바로 출발합시다.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 35, "dur_ms": 90, "phase_lr_deg": -4.6},
{"word": "지금", "start_ms": 135, "dur_ms": 110, "phase_lr_deg": -4.7},
{"word": "바로", "start_ms": 255, "dur_ms": 90, "phase_lr_deg": -4.5},
{"word": "출발합시다.", "start_ms": 355, "dur_ms": 221, "phase_lr_deg": -4.6}
]
},
{
"segment": 32,
"transcript": "우산 챙기는 거 잊지 마세요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "우산", "start_ms": 50, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "챙기는", "start_ms": 150, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 290, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "잊지", "start_ms": 340, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "마세요.", "start_ms": 440, "dur_ms": 136, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "I already have my umbrella ready.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": -5.1},

---
*Generated by WKAppBot ask gemini — 2026-03-31 05:50:12*
