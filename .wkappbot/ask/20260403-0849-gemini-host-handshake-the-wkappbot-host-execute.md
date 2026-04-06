# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=08:48:31  uptime=26956s  mem=1513MB  system_processes=448"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_084821.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 164-136-163-152-761-261-167-612-236-136-162-765-367-167-123-126-167-320-162-106-103-162-106-103-106-165-160-160-617-263-213-163-132-103-175-716-260-162-613-263-627-163-726-276-2_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 563-163-230-201-201-207-270-250-250-301-576-517-576-576-657-567-106-627-216-260-260-620-206-237-567-301-165-103-107-157-135-136-570-571-157-137-136-132-137-137-316-360-326-326-7_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 103-567-615-106-526-136-136-125-617-612-103-163-176-671-106-152-123-261-156-107-173-176-751-617-162-103-132-312-106-627-762-267-207-327-132-130 103-136-132-136 765-765-756-753-7_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 576-567-576-567-576-576-572-567-567-572-526-567-576-567-576-576-576-576-576-567-576-562-576-576-576-567-576-657-576-576-657-562-765-562-756-576-561-516-657-567-567-562-652-756-6_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 672-672-267-527-573-752-672-261-127-127-126-762-261-271-267-256-267-362-127-127_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 137-127 132-106-136-103-103-152-617-106-617-176-123-106-103-132-130-163-136-106-103-165-136-132-567-547 123-206-201_V.mp3)
  Segment 7: 9456ms ~ 20364ms (file: 602-627-765-726-135-137-106-310-132-103-136-157-361-215-103-107-107-617-125-163-160 263-267-126-173-106-316-132-135-103-103-145-106-213-130-621-713-132-103-137-136-613-136-136-5_V.mp3)
  Segment 8: 21364ms ~ 21940ms (file: 206-156-132-103-574-301-103-134-132-103-310-167-137-137-130-132-126-162-670-167-623-627-567-567-657-657-657-567-567-657 175-765-315 673-672-673-205-132-321 276-623_V.mp3)
  Segment 9: 22940ms ~ 23516ms (file: 274-751-175-276-576-536-261-267-257-267-756-356-362-167-621-632-675-653-217-132-165-651-631-136-316-321-312-135-157-157-315-513-327-321-271-276-267-267-267-265-261-312-321-132-3_W.mp3)
  Segment 10: 24516ms ~ 25092ms (file: 372-321-317-153-316-326-362-135-317-132-316-531-517-571-351-351-316-136-316-316-316-136-156-651-126-236-231-321-231-312-126-312-132-123-135-513-514-513-152-153-531-547-534-351-1_W.mp3)
  Segment 11: 26092ms ~ 26668ms (file: 612-627-671-672-602-610-103-130-132-613-612-126-613-163-621-627-673-123-163-126-123-123-362-261-613 765-761-162-561-615-632-120-765-675-263-132-321-126-162-126-102-621-126-621-1_W.mp3)
  Segment 12: 27668ms ~ 28244ms (file: 103-167-516-172-132-126-210-137-631-765-756-765-761-106-126-103-671-761-176-760-761-761-761-316-312-276-130-310-516-163-136-176-160-213-321-602-316-130-176-731-176-673-137-167-6_V.mp3)
  Segment 13: 29244ms ~ 29820ms (file: 173-176-167-170-136-130-102-632-613-613-163-610-612-612-162-163-167-163-615-657-615-562-567-576-571-576-567-571-756-765-576-573-756-756-756-705-570 627-627-671-267-672-267-126-2_W.mp3)
  Segment 14: 30820ms ~ 31396ms (file: 160-172-136-761-367-765-106-103-150-126-215-613-612-756-213-103-617-756-175-576-156-160-103-651-316-165-137-163-103-103-623-263-162-132 765-675-672_W.mp3)
  Segment 15: 32396ms ~ 32972ms (file: 301-103-130-312-602-302-607-672-132-123-601-301-576-675-765-760-670-617-621-106-103-103-123-103-175-136-123-312-231-136-132-167-126-126-123-163-675-675-765-413-316-312-316-312-1_V.mp3)
  Segment 16: 33972ms ~ 34548ms (file: 137-106-167-652-103-102-162-756-276-276-260-263-216-123-216-260-306-260-260-260-260-627_W.mp3)
  Segment 17: 35548ms ~ 36124ms (file: 675-265-756-657-567-567-516-657 103-310-213-216-132-162-126-675-765-376-627-263-326-632-267-263 267-625-602-206-216-261-261-216-203-312-632-321-301-236-201-621-267-162-236-762-6_W.mp3)
  Segment 18: 37124ms ~ 37700ms (file: 135-365-362-365-367-367-361-631-631-613-631-316-613-362-163-316-326-316-361-315-352-365-326-236-362-537-752-351-653-153-524-514-537-352-357-356-132-315-312-132-253-573-537-571-5_W.mp3)
  Segment 19: 38700ms ~ 39276ms (file: 316-130-162-103-103-130-761-615-103-106-673-167-160-316-103-103-615-103-617-671-103-103-163-136-107-267-261-756-621-261-612-261-261-312-123-312-765-371-136-103-316-316-312-106-6_V.mp3)
  Segment 20: 40276ms ~ 48952ms (file: 103-367-136-613-136-623-162-163-103-316-106-671-126-103-671-167-260-760-627-632-761-137-632-637-623-612-613-136-312-316-103-317-107-106-765-576-756-163-176-657-106-105-105-175-1_V.mp3)
  Segment 21: 49952ms ~ 50528ms (file: 136-127-167-103-671-756-175-152-157_V.mp3)
  Segment 22: 51528ms ~ 52104ms (file: 106-670-167-167-106-105-601-521-163-136-137-103-165-106-657-103-615-106-675-312-127-312-175-107-106-106-103-103-167-617-726-671-761-106-137-106-165-236-671-312-167-167-163-564-1_V.mp3)
  Segment 23: 53104ms ~ 53680ms (file: 765-675-621-107-126-163-165-130-160-132-156-173-126-601-165-213-765-263-126-160-263-106-205-176-756-176-126-123-126-620-627-261-621-106-103-654-215-136-167-163-163-136-136-103-1_W.mp3)
  Segment 24: 54680ms ~ 55256ms (file: 675-617-765-675-576-756-756-617-671-163-671_W.mp3)
  Segment 25: 56256ms ~ 56832ms (file: 721-732-712-756-725-725-205-352-375-725-752-723-753-253-725-572-527-527-257-572-572-526-726-276-576 765_W.mp3)
  Segment 26: 57832ms ~ 58408ms (file: 576-576-576-576-567-573-576-576-527-576-567-572-576-576-576-567-567-576-576-572-567-576-567-576-576-753-573-571-517-576-567-567-567-657-567-567-567-657-567-576-567-567-567-576-5_W.mp3)
  Segment 27: 59408ms ~ 59984ms (file: 675 657-754-576-617-612-265-263-263-652-267-765-765 657-675-576-576-576-573 563-765-672-763-765-762-672-627-756-627-627-576-675_W.mp3)
  Segment 28: 60984ms ~ 61560ms (file: 176-312-213-301-236-542-526-203-260-236-321-320-271-725-301-310-316-531-623-673-362-673-263-623-306-301-321-137-617-132-236-376-756-732-273-542-527-230-123-126-617-612-216-312-3_W.mp3)
  Segment 29: 62560ms ~ 63136ms (file: 673-673-671-267-261-627-526-216-316-103-165-167-236-627-612 123-106-236-671-162-106-612-130-263-627-621-132-261-621-756-652-657-675-106-126-126-620-162 103-751-576-612-623-326-6_W.mp3)
  Segment 30: 64136ms ~ 64712ms (file: 236-320-620-672-167-263-260-120-162-132-312-327-625-625-106-106-163-710-176-216-132-137-301-103-213-761-132-126-301-301-312-316-310-132-312-130-260-612-201-106-301-123-106-610-3_V.mp3)
  Segment 31: 65712ms ~ 66288ms (file: 576-756-576-576-567-576-765-762-672-673-267-625-627-620-576-576-576-571-571-576-576-754-574-574-762-675-675-176-756-375-367-673-357-365-375-317-751-175-137-317-631-163-216-231-1_W.mp3)
  Segment 32: 67288ms ~ 67864ms (file: 150-106-310-312-301-301-310-301-570-756-756-207-267-237-625-162-301-130-103-312-260-260-206-263-263-360-623-260-265-576-765-627-260-263-263-261-321-623-250-567-657-236-762-326-2_W.mp3)
  Segment 33: 68864ms ~ 69440ms (file: 231-231-237-625-206-103-103-132-601-673-362-163-231-206-205-250-360-103-301-103-312-123-231-756-126-120-265-326-106-150-230-130-301-132-123-236-206-203-210-123-321-532-543-530-5_V.mp3)
  Segment 34: 70440ms ~ 71016ms (file: 126-126-521-215-213-213-213-217-213-271-271-267-265-675-671-675-627-261-126-126-217-127-261-132-217-236-125-215-126-261-263-621-267-654-267-236-326-312-263-326-126-512-546-562-6_W.mp3)
  Segment 35: 72016ms ~ 72592ms (file: 726-276-276-762-751-625-657-567-567-765-765-675-625-675-672-672-627-673-672-571-352-675-675-617-236-365-312-267-327-256-362-362-672-671-375-762-672-572-750-751-173-176-320-231-1_W.mp3)
  Segment 36: 73592ms ~ 74168ms (file: 362-326-237-235-237-235-321-235-231-237-301-123-320-203-167-132-321-312-321-312-231-231-253-567-726-267-362-675-362-321-612-301-305-315-236-321-231-302-203-573-713-732-752-735-1_W.mp3)
  Segment 37: 75168ms ~ 75744ms (file: 623-326-267-632-162-236-675-765-627-126-306-326-302-620-306-365-750-136-136-607-635-561-103-132-356-136-312-301-132-312-130-103-235-613-612-236-321-752-732-207-273-517-132-160-3_V.mp3)
  Segment 38: 76744ms ~ 77320ms (file: 620-562-132-612-512-671-106-132-136 275-103-103-132-362-173-126-176-103-132-103-102-165-130-103-162-163-132-103-172-673-106-160-126-103-132-601-167-106-136-571-132-316-621-167-6_V.mp3)
  Segment 39: 78320ms ~ 78896ms (file: 106-136-302-162-623-306-132-623-326-367-263-261-123-167-312-267-632-263-236-162-162-163-316 106-165-132-615-106-103-132-103-230-132-312-103-132-301-130-307-357-130-107-712-107-1_V.mp3)
  Segment 40: 79896ms ~ 80472ms (file: 672-672-675-672-671-637-675-672-672-612-657-615-173-132-136 572-671-627-230-213-231 751-730 135-172-176-563-123 763-607-302 765-672-263 367-205-540-540-560 756-156-261-261-132-1_W.mp3)
  Segment 41: 81472ms ~ 82048ms (file: 627-632-126-765-765-763-267-267-362_W.mp3)
  Segment 42: 83048ms ~ 83624ms (file: 123-237-126-302-102-123-132-362-217_V.mp3)
  Segment 43: 84624ms ~ 85200ms (file: 106-167-106-576-132-167-167-617-103-167-103-173-576-612-106-103-163-617-106-620-103-132-106-103-102-670-317-126-512-126-106-317-607-632-312-103-136-173-103-162-103-715-326-176-1_V.mp3)
  Segment 44: 86200ms ~ 86776ms (file: 173-176-125-106-134-176-137-263-127-126-163-103-612 163-671-765-167-176-126-176-130-106-163-103-163-651-135-675-623-623-106-103-106-650-615-517-160-162-126-613-103-162-137-103-1_V.mp3)
  Segment 45: 87776ms ~ 88352ms (file: 326-503-216-302-536-132-125-657-765-132-130-574-176-176-107-206-260-256-627-267-213-267-163-132-621-106-310-260-623-261-613-163-132-657-617-672-765-167-257-302-231-260-206-237-3_V.mp3)
  Segment 46: 89352ms ~ 89928ms (file: 162-136-162-621-672-617-206-162-167-627-132-136-621-162-321-301-150-361-136-270-537-527-205-215-162-126-216-120-263-102-126-132-130-321-620-206-102-261-103-102-126-123-206-210-7_W.mp3)
  Segment 47: 90928ms ~ 91504ms (file: 564-576-670-650-562-152-153-562-325-216-201-120-215-521-541-542-503-320-302-302-302-375-352-157-357-623-203-230-520-756-612-650-601-236-321-301-132-321-312-130-317-765-760-270-7_V.mp3)
  Segment 48: 92504ms ~ 96248ms (file: 132-312-261-132 576-106-125-621-671-617-567-567-176-765-126-106-316-123-136-123-765-163-160-160-162-163-106-167-516-136-610-612-160-156-162-162-160-103-173-106-613-106-612-613-1_W.mp3)
  Segment 49: 97248ms ~ 97824ms (file: 302-302-236-502-540-547-534-756-675-765-670 301-123-203-523-531-517-132-123-716-713-761-756-576-657-657-576-165 132-571-572 301-130-132-312-301-574-527-547-672-670-162-136-263-2_W.mp3)
  Segment 50: 98824ms ~ 99400ms (file: 132-310-137-103-562-162-672-617-162-167-163-102-163-106-765-167-671-306-132-106-163-612-160-160-316-136-163-106-102-103-265-167-612-716-102-651-136-103-561-631-160-316-107-316-3_V.mp3)
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
> **Time**: 2026-04-03 08:49:34
> **AI**: gemini

---

## Response

AnalysisAnalysisGemini의 응답[
{
"segment": 1,
"transcript": "포커스 양보가 필요합니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "포커스", "start_ms": 20, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "양보가", "start_ms": 240, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "필요합니다", "start_ms": 410, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "다음 단계를 확인해 볼까요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "다음", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "단계를", "start_ms": 160, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "확인해", "start_ms": 300, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "볼까요", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "문서를 읽어주세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "문서를", "start_ms": 30, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "읽어주세요", "start_ms": 260, "dur_ms": 310, "phase_lr_deg": 0.0}
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
"transcript": "파일이 성공적으로 저장되었습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "파일이", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "성공적으로", "start_ms": 210, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "저장되었습니다", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "이 파일을 편집하시겠습니까",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 35, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "파일을", "start_ms": 115, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "편집하시겠습니까", "start_ms": 275, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "사용자 인터페이스를 분석 중입니다",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "사용자", "start_ms": 45, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "인터페이스를", "start_ms": 235, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "분석", "start_ms": 455, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "중입니다", "start_ms": 565, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "명령을 실행합니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "명령을", "start_ms": 40, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "실행합니다", "start_ms": 280, "dur_ms": 290, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "결과를 화면에 표시합니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "결과를", "start_ms": 55, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "화면에", "start_ms": 225, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "표시합니다", "start_ms": 375, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "시스템 로그를 확인하세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "시스템", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "로그를", "start_ms": 210, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "확인하세요", "start_ms": 360, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "오류가 발생했습니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "오류가", "start_ms": 60, "dur_ms": 210, "phase_lr_deg": 2.5},
{"word": "발생했습니다", "start_ms": 280, "dur_ms": 280, "phase_lr_deg": 2.5}
]
},
{
"segment": 12,
"transcript": "네트워크 연결을 확인해 주세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네트워크", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 2.5},
{"word": "연결을", "start_ms": 230, "dur_ms": 140, "phase_lr_deg": 2.5},
{"word": "확인해", "start_ms": 380, "dur_ms": 100, "phase_lr_deg": 2.5},
{"word": "주세요", "start_ms": 490, "dur_ms": 80, "phase_lr_deg": 2.5}
]
},
{
"segment": 13,
"transcript": "작업을 중단하시겠습니까",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "작업을", "start_ms": 35, "dur_ms": 210, "phase_lr_deg": 2.5},
{"word": "중단하시겠습니까", "start_ms": 255, "dur_ms": 300, "phase_lr_deg": 2.5}
]
},
{
"segment": 14,
"transcript": "설정을 변경해야 합니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "설정을", "start_ms": 45, "dur_ms": 160, "phase_lr_deg": 2.5},
{"word": "변경해야", "start_ms": 215, "dur_ms": 170, "phase_lr_deg": 2.5},
{"word": "합니다", "start_ms": 395, "dur_ms": 160, "phase_lr_deg": 2.5}
]
},
{
"segment": 15,
"transcript": "비밀번호를 입력하세요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "비밀번호를", "start_ms": 50, "dur_ms": 310, "phase_lr_deg": 2.5},
{"word": "입력하세요", "start_ms": 370, "dur_ms": 200, "phase_lr_deg": 2.5}
]
},
{
"segment": 16,
"transcript": "인증에 성공했습니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "인증에", "start_ms": 40, "dur_ms": 210, "phase_lr_deg": 2.5},
{"word": "성공했습니다", "start_ms": 260, "dur_ms": 300, "phase_lr_deg": 2.5}
]
},
{
"segment": 17,
"transcript": "새로운 업데이트가 있습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "새로운", "start_ms": 55, "dur_ms": 180, "phase_lr_deg": 2.5},
{"word": "업데이트가", "start_ms": 245, "dur_ms": 190, "phase_lr_deg": 2.5},
{"word": "있습니다", "start_ms": 445, "dur_ms": 120, "phase_lr_deg": 2.5}
]
},
{
"segment": 18,
"transcript": "프로그램을 재시작하세요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "프로그램을", "start_ms": 50, "dur_ms": 240, "phase_lr_deg": 2.5},
{"word": "재시작하세요", "start_ms": 300, "dur_ms": 260, "phase_lr_deg": 2.5}
]
},
{
"segment": 19,
"transcript": "데이터베이스를 업데이트합니다",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "데이터베이스를", "start_ms": 45, "dur_ms": 320, "phase_lr_deg": 2.5},
{"word": "업데이트합니다", "start_ms": 375, "dur_ms": 200, "phase_lr_deg": 2.5}
]
},
{
"segment": 20,
"transcript": "작업이 완료되었습니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "작업이", "start_ms": 60, "dur_ms": 180, "phase_lr_deg": 2.5},
{"word": "완료되었습니다", "start_ms": 250, "dur_ms": 320, "phase_lr_deg": 2.5}
]
},
{
"segment": 21,
"transcript": "스크린샷을 찍습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "스크린샷을", "start_ms": 40, "dur_ms": 290, "phase_lr_deg": 0.0},
{"word": "찍습니다", "start_ms": 340, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "이미지를 분석하고 있습니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "이미지를", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "분석하고", "start_ms": 240, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 410, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "텍스트를 추출합니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "텍스트를", "start_ms": 45, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "추출합니다", "start_ms": 275, "dur_ms": 290, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "번역을 시작합니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "번역을", "start_ms": 30, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "시작합니다", "start_ms": 250, "dur_ms": 320, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "언어를 감지 중입니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "언어를", "start_ms": 55, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "감지", "start_ms": 225, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "중입니다", "start_ms": 345, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 27,
"transcript": "음성 안내를 시작합니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "음성", "start_ms": 60, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "안내를", "start_ms": 210, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "시작합니다", "start_ms": 370, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "볼륨을 조절해 보세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "볼륨을", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "조절해", "start_ms": 240, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "보세요", "start_ms": 380, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "마이크가 켜져 있습니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "마이크가", "start_ms": 45, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "켜져", "start_ms": 265, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 405, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "주변 소음을 제거합니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "주변", "start_ms": 40, "dur_ms": 130, "phase_lr_deg": 2.5},
{"word": "소음을", "start_ms": 180, "dur_ms": 160, "phase_lr_deg": 2.5},
{"word": "제거합니다", "start_ms": 350, "dur_ms": 220, "phase_lr_deg": 2.5}
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
"transcript": "녹음을 시작하시겠습니까",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "녹음을", "start_ms": 55, "dur_ms": 200, "phase_lr_deg": 2.5},
{"word": "시작하시겠습니까", "start_ms": 265, "dur_ms": 300, "phase_lr_deg": 2.5}
]
},
{
"segment": 33,
"transcript": "파일 이름을 정해주세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "파일", "start_ms": 60, "dur_ms": 110, "phase_lr_deg": 2.5},
{"word": "이름을", "start_ms": 180, "dur_ms": 150, "phase_lr_deg": 2.5},
{"word": "정해주세요", "start_ms": 340, "dur_ms": 230, "phase_lr_deg": 2.5}
]
},
{
"segment": 34,
"transcript": "저장 경로를 선택하세요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "저장", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 2.5},
{"word": "경로를", "start_ms": 170, "dur_ms": 150, "phase_lr_deg": 2.5},
{"word": "선택하세요", "start_ms": 330, "dur_ms": 240, "phase_lr_deg": 2.5}
]
},
{
"segment": 35,
"transcript": "클라우드에 업로드합니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "클라우드에", "start_ms": 50, "dur_ms": 240, "phase_lr_deg": 2.5},
{"word": "업로드합니다", "start_ms": 300, "dur_ms": 270, "phase_lr_deg": 2.5}
]
},
{
"segment": 36,
"transcript": "동기화가 완료되었습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "동기화가", "start_ms": 45, "dur_ms": 220, "phase_lr_deg": 2.5},
{"word": "완료되었습니다", "start_ms": 275, "dur_ms": 300, "phase_lr_deg": 2.5}
]
},
{
"segment": 37,
"transcript": "계정 정보를 확인해 보세요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "계정", "start_ms": 55, "dur_ms": 120, "phase_lr_deg": 2.5},
{"word": "정보를", "start_ms": 185, "dur_ms": 150, "phase_lr_deg": 2.5},
{"word": "확인해", "start_ms": 345, "dur_ms": 120, "phase_lr_deg": 2.5},
{"word": "보세요", "start_ms": 475, "dur_ms": 100, "phase_lr_deg": 2.5}
]
},
{
"segment": 38,
"transcript": "로그인하시겠습니까",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "로그인하시겠습니까", "start_ms": 30, "dur_ms": 540, "phase_lr_deg": 2.5}
]
},
{
"segment": 39,
"transcript": "회원 가입이 필요합니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "회원", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 2.5},
{"word": "가입이", "start_ms": 160, "dur_ms": 160, "phase_lr_deg": 2.5},
{"word": "필요합니다", "start_ms": 330, "dur_ms": 240, "phase_lr_deg": 2.5}
]
},
{
"segment": 40,
"transcript": "이용 약관에 동의하세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "이용", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "약관에", "start_ms": 160, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "동의하세요", "start_ms": 320, "dur_ms": 250, "phase_lr_deg": 0.0}
]
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
"transcript": "본인 인증을 진행합니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "본인", "start_ms": 60, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "인증을", "start_ms": 210, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "진행합니다", "start_ms": 380, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "잠시만 기다려 주세요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "잠시만", "start_ms": 45, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "기다려", "start_ms": 265, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 405, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "완료될 때까지 기다리세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "완료될", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "때까지", "start_ms": 240, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "기다리세요", "start_ms": 400, "dur_ms": 170, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "뒤로 가시겠습니까",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "뒤로", "start_ms": 40, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "가시겠습니까", "start_ms": 180, "dur_ms": 390, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "홈 화면으로 이동합니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "홈", "start_ms": 35, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "화면으로", "start_ms": 115, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "이동합니다", "start_ms": 305, "dur_ms": 260, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "검색 결과가 없습니다",
"speaker":

---
*Generated by WKAppBot ask gemini — 2026-04-03 08:49:34*
