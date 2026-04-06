# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=08:22:01  uptime=25366s  mem=1715MB  system_processes=447"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_082151.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 126-621-612 326-203-256-253-675-362-326_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 261-103-130-130-132-123-163-160-132-517 672_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 516-513-156-165-152-153-135-213-125-512-576-653-215-213-321-316-526-236-261-213-526-605-506-512-135-103-576-561-513-523-356-136-536-352-351-326-516-513-530-573-215-536-521-125-5_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 614 607-671-605-765-761-672-607-706-726-675-765-765-675-574-752 567-675 765-765-673-162-713-751-756-751_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 126-312-132-130-132-602-362-756-576-576-567-576-672-267-162-163-203-230 763-670-607-167-106-167-162-136-162-670-607-672-620-162-147-132-637-612-631-637-267-602-621-607-673-673-6_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 265-521-562-523-625-317-103-102-132-123-103-263-326-213-213-627-627-106-106-167-136-627-620-176-130-136-123 612 132-216-172-726-267-126-165-173-763-761-672-627-627-267-206-623-6_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 132-130-312-310-132-126-263-621-261-756-102-236-263-216-123-302-307-203-672-632-306-753-132-163-312-132-310-310-750-103-132-730-132-103-301-103-103-103-103-132-301-312-123-130-1_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 172-673-670-627-162-620-625-103-230-301-167-731-167-621-236-637-756-267-263-203-106-102-162-217-123-132-260-260-206-206-326-302-302-750-715-513-632-312-173-761-163-563-162-102-7_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 163-103-675-756-756-756-103-132-103-612-103-136-362-102-125-125-726-163-136-316-103-134-103-514-365-167-165-132-156-617-107-103-167-167-136-132-236-761-136-136-651-175-756-162-1_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 627 761 162 321-576-612-617-657-160-612-167-675-602-612-617-623-136-136-617-562-615-167-617-160-617-172-162-162-617-617-653-156-136-176-163-136-132-217-165-612-103-132-123-132-1_V.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 362-621-136-621-263-162-127-162-275-162-160-617-162-610-517-251-123-123-123-213-120-123-123-120-123-120_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 263-236-165-103-136-105-306-301-132-135-135-326-320-302-570-176-175-132-301-321-312-730-371-165-162-312-132-305-327-103-130-301-301-310-316-137-316-310-230-203-327-213-123-132-1_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 612-362-326-321-236_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 316-361-312-316-163-137-137-162-501-506-576-576-570-610-360-132-570-617-627-267-261-621-316-176-173-617-231-312-132-305-507-301-312-301-312-136-132-267-162-236-310-301-312-132-3_V.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 132-173-672-612-106-153-501-314-132-167-163-706-670-675-671-206-123-103-106-105-137-137-136-715-712-617-632-206-132-162-540-504-546-542-136-106-106-167-761-361-627-627-136-102-6_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 126-167-132-102-123-706-761-672-671-750-570-162-576-316-316-132-671-320-320-267-123-143-132-176-361-157-763-321-132-657-657-605-652-675-672-312-750-756-756-361-574-571-357-132-6_V.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 163-132-610-167-157-165-312-103-361-103-172-163-512-627-103-165-136-301-260-163-136-571-175-103-106-621-610-167-163-163-153-106-176-163-165-761-136-137-107-571-175-103-137-167-1_V.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 621-620-263-260-236-263-326-263-132-106-102-136-302-320-321-302-132-203-203-320-230-321-312-123-312-301_V.mp3)
  Segment 19: 28368ms ~ 38340ms (file: 167-106-162-132-162 143-735-167-163-107-160-163-167-106-163-165-136-126_V.mp3)
  Segment 20: 39340ms ~ 39916ms (file: 167-571-756-754-751-570-203-312-574-574-507-154-156-153-507 576-127 251 570-705-654-652-216-267-203-527-123-513-352-175-265-627 231-260-261-206-612-136-130-105-163-103-230-602-2_V.mp3)
  Segment 21: 40916ms ~ 41492ms (file: 123-132-130 321-213-627-627-627 675-671-617-672-623-672-726-672-672-617-612-612-130-106-103-671-267-627-671-316-301-123_W.mp3)
  Segment 22: 42492ms ~ 43068ms (file: 562-265-316-263-217-215-612-157-216-123-136-273-273-762-572-723-132-312-317-253-527-526-576-527-540-547-357-351-316-176-326-617-162-162-123-126-127-157-176-137-753-561-157-751-6_W.mp3)
  Segment 23: 44068ms ~ 44644ms (file: 326-164-236-203-523-132-103-137-130-130-103-136-163-132-103-521-123-326-312-231-310-301-536-132-136-316-231-321-321-312-327-132-103-310-310-103-136-671-167-671-167-106-103-310-1_V.mp3)
  Segment 24: 45644ms ~ 46220ms (file: 756-576-576-567-567-576-567-567-574-576-576-567-576-576-576-567-501-536-536-563-567-567-567-537-576-657-765-576-652-576-567-756-567-562-567-567-567-567-351-623-627-637-623-632-2_W.mp3)
  Segment 25: 47220ms ~ 47796ms (file: 672 671-672-627-617-167-617-671-670-617-167-613-162_W.mp3)
  Segment 26: 48796ms ~ 49372ms (file: 216-167-712-176-175-671-167-671-106-751-756-725-247-672-672 321-173-103-136-136-173-126-126_V.mp3)
  Segment 27: 50372ms ~ 50948ms (file: 361-310-231-261-236-263-267-210-216-260-263-206-201-203-216-126-123-217 576 260-260 632-362-123-302-260-302-230-206-362-236-213-206-230-203-206-316-236-361-360-302-321-132-326-2_W.mp3)
  Segment 28: 51948ms ~ 52524ms (file: 301-230-136-612-267-321-327-326-361-316-163-310-130-103-627-672-623-206-162-206-320-230-261-320-320-567-657-657-637-106-756-701-103-671-761-671-630-623-602-671-163-103-163-160-1_V.mp3)
  Segment 29: 53524ms ~ 54100ms (file: 301-301-327-367-367-543-357-137-572-257-572-517-574-562-326-130-103-307-130-160-156-765-150-501-546-514-547-357-657-657-657-625-564-546-562-574-564-561-657-654-635-365-651-625-6_W.mp3)
  Segment 30: 55100ms ~ 55676ms (file: 203-203-263-672-672 132-104-136-134-132-261-167-102-231-163-163-103-160-126-106-103-160-612-162-153-261-175-261-621-216-627-621-617-163-716-765-601-612-167-617-716-103-102-103-1_V.mp3)
  Segment 31: 56676ms ~ 57252ms (file: 657-675-672-651-621-657-567-321-613-362-312-263-625-265-267-762-263-263-672-572-570-753-752-751-750-751-751-751-751_W.mp3)
  Segment 32: 58252ms ~ 58828ms (file: 632-321 371-317-176-136-106-167-362-125_V.mp3)
  Segment 33: 59828ms ~ 60404ms (file: 715-765-652-753-175-162-615-716-765-167_W.mp3)
  Segment 34: 61404ms ~ 61980ms (file: 675-267-201-203-362-376-320-206-761-173-756-672-316 132-607-260-206-361-213-621-167-617-657-631-106-610-620-602-103-623-602-620-163-106-216-301-163-106-153-167-106-160-106-105-1_V.mp3)
  Segment 35: 62980ms ~ 63556ms (file: 631-106-125-361-165-610-103-263-163 574-657-671-316_W.mp3)
  Segment 36: 64556ms ~ 65132ms (file: 123 130-312-213-126-216 103-132 326-362-326-310-312-132-136-123-613 103-103-103-103-261-231_V.mp3)
  Segment 37: 66132ms ~ 66708ms (file: 172-126-615-570-527-527-526-657-546-567-652-657-635 570-751-657-765-576-652 263-260-267-263-260-130-571-517-675-236-267-263-236-325-507-127-632-123-306-632-362-236-601-602-362-3_W.mp3)
  Segment 38: 67708ms ~ 68284ms (file: 632-627-672-623-623-627-623-621-612-316-136-675-671-632 672-763_W.mp3)
  Segment 39: 69284ms ~ 69860ms (file: 130-613-261-132-162-321-301-165-365-103-136-236-176-160-106-103-167 316-106-167-750-135-165-106-627-651-507-507-136-163-136-103-547-163-160-136-136-103-130-167-103-163-103-561-1_V.mp3)
  Segment 40: 70860ms ~ 71436ms (file: 632-263-201-123-236-261-326 612 627-672_W.mp3)
  Segment 41: 72436ms ~ 73732ms (file: 310-237-126-607-132-574-256-527-256-167-571-321-326-236-236-267-623-321-326-235-560-536-756-375-376-273-236-237-352-572-527-152-135-571-573-756-751-573-357-751-375-370-576-231-1_W.mp3)
  Segment 42: 74732ms ~ 75308ms (file: 206-203-301-231-621-261-103-312-103-163-136-162-167-162-321-263-236-263-756-726-623-301-320-320-327-372-310-301-672-652-653-567-567-567-564-567-562-567-657-265-613-261-236-230-3_V.mp3)
  Segment 43: 76308ms ~ 76884ms (file: 623-625-316-132-623-236-263-126-631-612-263-625-673-635-316-136-326-321-236-623-361-163-126-163-612-316-132-316-132-310-265-263-312-125-103-162-216-231-136-132-621-623-632-213-1_W.mp3)
  Segment 44: 77884ms ~ 78460ms (file: 671-175-765-176-762-765-765-756-765-756-275-572-756-160_W.mp3)
  Segment 45: 79460ms ~ 80036ms (file: 763-675-671-671-672-631-761-762-673-762-763-760-671 167-671-163_W.mp3)
  Segment 46: 81036ms ~ 81612ms (file: 736-627-675-673-601-675-621-263-756-567-756-726-632-617-671-163-306-175-675-762-736-302-306-631 617-627-756-765-620-632-631-162-263-267-263-625-265-263-236 261-261-216-263-652-2_W.mp3)
  Segment 47: 82612ms ~ 83188ms (file: 527-572-572-573-253-532-521-523-572-275-725-752-752-275-725-721-276-725-752-527-251-215-263-276-267-276-271-275-275-276-276-276-275-275-527-352-315-321-126-216-326-263-362-231-3_W.mp3)
  Segment 48: 84188ms ~ 84764ms (file: 106-316-172-617-567-670-216-751-762-162-130-675-617-106-671-167-316-613-103-135-132-127-267-152 235-617-315-610-631-756-163-167-267-326-263-235 673-627-756-167-103-715-176-620-7_W.mp3)
  Segment 49: 85764ms ~ 86340ms (file: 203 341-145-613-316-627-672-123-637-621-765-576-765-576-576-671-672-623-627_V.mp3)
  Segment 50: 87340ms ~ 87916ms (file: 632-163-213-136-632-623-267-132-134-126-352-153-641-265-624-327-375-672-673-602-627-136-670-675-670-671-175-167-126-163-126-106-176 132-135-513-163-132-241-356-651-132-267-206-2_V.mp3)
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
> **Time**: 2026-04-03 08:23:00
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "수업을 마무리하면서 중요한 문법들을 다시 확인해 볼까요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "수업을", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "마무리하면서", "start_ms": 160, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "중요한", "start_ms": 320, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "문법들을", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 580, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "확인해", "start_ms": 680, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "볼까요?", "start_ms": 810, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "좋아요, 조사의 쓰임새가 가장 헷갈리지만 재미있어요.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "좋아요,", "start_ms": 30, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "조사의", "start_ms": 160, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "쓰임새가", "start_ms": 280, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "가장", "start_ms": 430, "dur_ms": 90, "phase_lr_deg": 5.0},
{"word": "헷갈리지만", "start_ms": 530, "dur_ms": 160, "phase_lr_deg": 5.0},
{"word": "재미있어요.", "start_ms": 700, "dur_ms": 180, "phase_lr_deg": 5.0}
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
"transcript": "받침의 유무를 잘 살피는 것이 첫 번째 단계예요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "받침의", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "유무를", "start_ms": 180, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 300, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "살피는", "start_ms": 370, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "것이", "start_ms": 500, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "첫", "start_ms": 600, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "번째", "start_ms": 650, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "단계예요.", "start_ms": 740, "dur_ms": 160, "phase_lr_deg": 0.0}
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
"transcript": "문장을 많이 읽어보니까 규칙이 조금씩 보여요.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "문장을", "start_ms": 40, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "많이", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "읽어보니까", "start_ms": 290, "dur_ms": 160, "phase_lr_deg": 5.0},
{"word": "규칙이", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "조금씩", "start_ms": 580, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "보여요.", "start_ms": 710, "dur_ms": 130, "phase_lr_deg": 5.0}
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
"transcript": "습관이 되면 자연스럽게 입에서 나올 거예요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "습관이", "start_ms": 60, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "되면", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "자연스럽게", "start_ms": 310, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "입에서", "start_ms": 480, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "나올", "start_ms": 610, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "거예요.", "start_ms": 710, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "꾸준히 복습하는 태도가 정말 멋지십니다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "꾸준히", "start_ms": 35, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "복습하는", "start_ms": 185, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "태도가", "start_ms": 345, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 475, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "멋지십니다.", "start_ms": 585, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "오늘 수업은 여기서 모두 마칠게요. 수고하셨어요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "수업은", "start_ms": 165, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "여기서", "start_ms": 295, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "모두", "start_ms": 425, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "마칠게요.", "start_ms": 545, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "수고하셨어요.", "start_ms": 710, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "네 선생님, 오늘도 유익한 수업 감사합니다.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 30, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "선생님,", "start_ms": 100, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "오늘도", "start_ms": 250, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "유익한", "start_ms": 390, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "수업", "start_ms": 520, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "감사합니다.", "start_ms": 630, "dur_ms": 230, "phase_lr_deg": 5.0}
]
},
{
"segment": 12,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 13,
"transcript": "안녕히 가시고 다음에 또 만나요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 55, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "가시고", "start_ms": 205, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "다음에", "start_ms": 335, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 455, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "만나요.", "start_ms": 525, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "네 안녕히 계세요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 25, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "안녕히", "start_ms": 95, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "계세요.", "start_ms": 255, "dur_ms": 190, "phase_lr_deg": 5.0}
]
},
{
"segment": 15,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 16,
"transcript": "조심히 들어가세요. 또 봐요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "조심히", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "들어가세요.", "start_ms": 190, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 410, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "봐요.", "start_ms": 480, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "행복한 시간 되시길 바랍니다.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "행복한", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "시간", "start_ms": 200, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "되시길", "start_ms": 320, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "바랍니다.", "start_ms": 460, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "바이 바이.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "바이", "start_ms": 60, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "바이.", "start_ms": 230, "dur_ms": 340, "phase_lr_deg": 0.0}
]
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
"transcript": "진짜 안녕.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "안녕.", "start_ms": 190, "dur_ms": 380, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 22,
"transcript": "오늘 수업도 정말 알찼습니다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "수업도", "start_ms": 165, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 295, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "알찼습니다.", "start_ms": 405, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "모두 건강 잘 챙기세요.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "모두", "start_ms": 30, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "건강", "start_ms": 150, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 270, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "챙기세요.", "start_ms": 340, "dur_ms": 210, "phase_lr_deg": 0.0}
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
"transcript": "다음에 다시 봬요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 50, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 220, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "봬요.", "start_ms": 340, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "바이.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "바이.", "start_ms": 60, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 28,
"transcript": "진짜 진짜 끝.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 190, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "끝.", "start_ms": 340, "dur_ms": 230, "phase_lr_deg": 0.0}
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
"transcript": "바이 바이.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "바이", "start_ms": 55, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "바이.", "start_ms": 225, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "수고.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "수고.", "start_ms": 25, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "고생.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "고생.", "start_ms": 45, "dur_ms": 530, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 34,
"transcript": "안녕.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "안녕.", "start_ms": 30, "dur_ms": 540, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "바이.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "바이.", "start_ms": 40, "dur_ms": 530, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 37,
"transcript": "진짜 끝.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "끝.", "start_ms": 200, "dur_ms": 370, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "안녕히.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "안녕히.", "start_ms": 60, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 40,
"transcript": "잘 가.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "잘", "start_ms": 35, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "가.", "start_ms": 155, "dur_ms": 420, "phase_lr_deg": 0.0}
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
"transcript": "바이.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "바이.", "start_ms": 45, "dur_ms": 530, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "안녕.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "안녕.", "start_ms": 25, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 45,
"transcript": "수고.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "수고.", "start_ms": 55, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "고생.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "고생.", "start_ms": 35, "dur_ms": 540, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 48,
"transcript": "바이 바이.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "바이", "start_ms": 40, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "바이.", "start_ms": 210, "dur_ms": 360, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 50,
"transcript": "진짜 끝.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 60, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "끝.", "start_ms": 210, "dur_ms": 360, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 08:23:00*
