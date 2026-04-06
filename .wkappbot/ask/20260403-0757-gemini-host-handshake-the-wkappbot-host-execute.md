# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=07:56:38  uptime=23843s  mem=1814MB  system_processes=446"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_075628.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 620-260-206-203-123-351 132-201-206-260-267-620-260-267-621-132-103-213 123 547-547-574-156-107-102-103-361-213-126-261-206-206-263-236-526-326-326-735-376-673-620-260-263-320-3_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 236-136-132-316-106-106-103-163-136-103-620-173-136-572-316-136-213-657-103-361-160-103-132-137-216-103-157-135-126-103-136-620-160-156-762-617-123-231-106-761-716-310-267-657-1_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 756-752-625-672-627-321-130-123-230-356-752-675-312-130-160-504-562-567-176-621-567-256-627-652-653-231-673-671-127-256-152 130-103-316-261-216-103-162-160-126-261-671-162-261-1_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 207-230-206-165-137-532-256-256-265-263-261-543-534-524-257-325-312-216-123-521-253-253-231-231-213-250-237-231-237-326-230-320-230-320-230-235-230-230-203-207-263-260-260-325-2_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 620-621-301-302-302-326-263-261-263-136-152-315-123-260-263-261-236-106-132-126-162 261-261-621-263-260-263-260-102-130-106-136-163-126-102-130-236-602-126-216-216-765-672-627-6_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 106-546-165-571-175-756-267-123-263-210 132-312-123-671-612-367-157-576-165-261-263-213-715-267-106 302-762-327-237-201-321-237 360-367-132-316-326-623-762-371-716-763-271-213-7_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 670-301-670-607-762-102-601-574-105-130-103-213-301-167-103-106-165-621-106-106-167-301-132-310-132-312-602-260-302-236-326-236-316-130-136-310-326-236-236-302-230-574-516-613-2_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 612-615-651-657-651-615-675-615-263-123-312-526-567-572-615-263-165-675-620-607-657-567-570-570-576-150-572-562-572-562-657-361-632-362-362-631-635-562-562-567-652-605-365-365-3_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 675-765-657-657-506-506 316-672-637-673-617-627-325-267-237-236_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 130-103-103-216-306-261-560-573-317-135-137-103-167-167-675-675-675_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 132-132-253-521-132-213-132-213-132-123-123-312-132-571-715-275-237-326-236-321-514-514-571-257-237-267-236-326-312-326-517-576-567-256-261-316-123-126-312-263-126-216-126-312-1_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 132-362-623-263-267-261-326-326-103-105-135-316-126-132-132-137-132-312-301-312-316-153-612-126-162-627-671-167-162-705-570-107-167-317-321-312 576-675-765-302-306-320-326-325-3_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 160-136-612-132-126-163-103-103-103-106-617-163-263-126-103-126-231-105-163-130-136-163-103-612-612-123-126-176-210-126-126-617-127-312-657-576-675-765-765-765-675-765-765-675-7_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 765-756-760-317-132-103-312-321-123-126-163-312-623-126-103-216-201-206-265-102-207-217-132-231-206-320-302-302-302-320-260-562-320-203-206-302-306-326-160-361-621-326-136-316-7_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 130-163-106-160-103-163-102-136-163-130-620-612-627-623-637-136-316-162-123-167-316_V.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 175-165-176-516-156-137-571-576-675-657-657-756-567-375-756-671-673-753-576-763-567-756-657-567-567-576-615-671-765-536-367-573-571-156-657-571-517-157-567-576-567-157-756-753-6_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 621-612-632-761-652-261-321-136-132-163-316-165-163-132-136-241-213-126-132-213-312-136-126-601-167-163-136-267-607-260-612-576-765-761-763-326-627-652-672-672-206-236-570-712-3_V.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 136-321-126-136-231-261-263-263-236-753-756-756-765-675-657-162-613-617-675-627-761-617-172-126-715-317-312-321-321-312-301-302-321-507-507-517-602-610-602-627-627-625-261-526-6_V.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 623-675-621-162-231-613-362-612-621-620-261-612-162-613-627-672-617-627 627 612-672-162-613-126-216-132-123-613-623-621-627-672-613-632_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 762-763 671-671-176-671-163-671 671-671_W.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 135-236-206-206-260-236-301-310-320-123-126-753-675-627-306-316-162-301-231-260-260-260-261-126-132-213-163-312-130-132-230-236-301-310-301-301-765-312-163-316-176-162-106-260-6_V.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 570-306-376-356-321-312-602-632-603-602-570-507-570-302-321-132-306-120-123-312-631-312-610-306-312-361-103-137-576-312-130-326-106-132-103-162-620-160-765-765-376-307 176-132-7_V.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 106-617-173-316-123-631-613-216-610-612-576-167-621-612-621-621-631-126-132-103-623-162-621-130-136-163-103-617-163-516-132-106-126-106-167-106-176-106-657-163-263-632 132-130-1_V.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 126-627-263-362-263-267-265-625-265-263-167-167-106-106 265-263-216-210-261-213-231-132-123-263-261-261-236-321-231-261-265-263-261_W.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 312-632-621-623-623-627-260-261-267-261-267-620-120-130-312-132-136-321-123-310-132-321-132-123-137-132-102-163-652-675-672-576-567-756-675-672-617-762-706-716-167-176-127-617-1_V.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 327-136-621-165-321-731-576-517-765-765-675-631-613-657-652-652-652-625-652-265-362-256-216-263 263-231-627 761-106-106-130-671-106-136-731-301-127-162-261-306-160-106-316-102-3_W.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 513-156-576-567-576-517-510-351-320-203-302-321-317-325-315-635-253-365-356-536-536-351-156-531-351-356-356-365-531-573-357-356-536-537-357-536-567-356-351-137-517-531-157-567-5_W.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 312-312-310-123-627-257-625-623-263-207-320-302-302-231-327-237-326-132-135-312-136-103-103-123-127-621-213-126-123-670-671-607-260-720-570-753-207-132-102-230-326-123-132-130-6_W.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 306-367-376-326-236-623-326-236-326-603-623-103-160-163-106-163-162-162-123-126-627-621-360-302-230-326-302-326-132-750-756-705-371-316-310-763-576-657-765-675-365-315-365-631-6_V.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 106-163-306-302-136-612-162-312-302-102-107-106-103-607-602-602-137-103-260-132-103-136-136-314-102-136-132-231 576-105-157-671-605-675-657-615-617-106-130-610-620-260-206-261-2_V.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 236-126-612-213-621-261-632-621-632-261-621-621-261-263-362-316-361-312-361-362-632-623-625-623-612-621-362-261-612-162-165-351-513-315-613-613-163-356-136-263-623-632-625-267-2_W.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 123-132-236-623-136-132-136-136-163-103-613-316-162-163-163-160-126-130-651-160-106-156-167-165-256-162 103-167-103-103-163-103-126-501-613 102-627-623-617-612-620-126-137-103-1_V.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 627-653-651-263-132-216-123-326-652-657-635-672-267-623-267_W.mp3)
  Segment 34: 52008ms ~ 53448ms (file: 762-762-675-765-657-267-625-625-276-762-615-156-572-756-762-756-375-576-617-763-675-132-756-657-326-572-513-576-675-756-675-765-765-756-756-756 316-150-513-316-107-103-361-103-2_W.mp3)
  Segment 35: 54448ms ~ 55024ms (file: 302-321-132-312-301-203-263-236-267-206-327-637-760-675-675-301-376-312-321-326-361-326-321-165-136-136-136-316-103-167-160-301-130-312-137-310-312-306-102-123-236-231-316-163-1_V.mp3)
  Segment 36: 56024ms ~ 56600ms (file: 123-123-132-123-132-123-321-123-132-126-132-312-123-132-102-123-132-103-126-123-132-127-123_V.mp3)
  Segment 37: 57600ms ~ 58176ms (file: 617-621-701-163-612-126-132-162-162-160-671-631-576-576-574-567-673-607-670-132-132-163-765-760-765-675-765-103-716-617-671-173-107-176-713-107-176-137-765-765-765-765-132-672-3_W.mp3)
  Segment 38: 59176ms ~ 59752ms (file: 126 751-751-132-312-231-126-732-176-357-736-321-732-721-132-372-213-136-136-136-163-162-136-136-710-163-103-316-103-136-601-170-163-103-751-136-106-137-167-632-621-163-103-130-1_W.mp3)
  Segment 39: 60752ms ~ 61328ms (file: 203-260-362-360-632-613-326-613-106-602-162-361-130-103-102-132-162-132-167-716-765-210-126-132-103-761-360-316-103-130-321-310-136-312-631-361-623-263-612-756-617-612-163-621-2_V.mp3)
  Segment 40: 62328ms ~ 62904ms (file: 172-261-162-132-316-617-103-136-756-617-165-130-106-165-106-136-103-153-612-765-612-263 617-672-671-756-765-675-576-762-765-765-571-573-756-206-316-120-130-602-206-206-261-752-3_W.mp3)
  Segment 41: 63904ms ~ 64480ms (file: 570-756-765-576-653-765-526-570-754-574-576-576-756-576-576-657-675-576-765-576-572-754-576-756-765-756-756-574-765-675-675-765-756-576-567-567-576_W.mp3)
  Segment 42: 65480ms ~ 66056ms (file: 301-301-301-260-230-627-607-607-627-231-620-260-627-621-167-162-613-260-261-260-260-620-260-623-267-206-206-203-206-547-765-706-760-657-310-301-310-132-524-357-302-310-312-132-1_W.mp3)
  Segment 43: 67056ms ~ 67632ms (file: 316-103-153-135-375-137-103-136-130-316-263-173-315-301-371-317-310-321-236-326-263-602-672-627-627-123-312-132-137-132-103-621-602-627-602-310-130-312-301-301-103-103-132-507-5_V.mp3)
  Segment 44: 68632ms ~ 69208ms (file: 567-567-576-537-564-231-235 612 231-137-761-671-103-361-167-325-167-162-637-267-136-312-617-312-103-130-135-103-160-135-356-103-632-317-310-312-652-137-312-321-315-310-310-213-1_V.mp3)
  Segment 45: 70208ms ~ 70784ms (file: 627-316-132-136-316-136-316-610-361-716-163-167-103-160-165-360-621-301-316-106-103-106-601-671-652-263-312-132-321-132-312-103-623-201-316-136-162-136-316-103-167-563-316-132-1_V.mp3)
  Segment 46: 71784ms ~ 72360ms (file: 625-265-263-213-213 231-236-321-236-263-326_W.mp3)
  Segment 47: 73360ms ~ 73936ms (file: 514-574-540-654-540-531 312-132-312-315-135-315-135-135-537-153-574-537-576-765-675-675-675-765-675-675-567-576-574-576-574-751-527-576-371-321-263-675-673-237-372-376-357-576-5_W.mp3)
  Segment 48: 74936ms ~ 75512ms (file: 123-213-125-321-231-321-126-312-312-125-143-321-123-237-326-162-326-321-321-327-275-324-231-316-231-321-236-236-216-267-213-236-231-213-123-236-231-321-123-126-216-214-213-127-2_V.mp3)
  Segment 49: 76512ms ~ 77088ms (file: 302-310-301-130-310-136-132-103-103-321-231-320-320-302-531-130-301-312-372-137-136-102-130-103-761-761-172-751-310-301-307-603-301-310-301-312-621-632-236-326-321-320-302-321-1_V.mp3)
  Segment 50: 78088ms ~ 78664ms (file: 310-312-132-312-132 312-321-132-312_V.mp3)
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
> **Time**: 2026-04-03 07:57:41
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "오늘 배운 내용을 복습해 볼까요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "배운", "start_ms": 160, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "내용을", "start_ms": 290, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "복습해", "start_ms": 430, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "볼까요", "start_ms": 550, "dur_ms": 20, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "좋아요 어떤 것부터 할까요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "좋아요", "start_ms": 30, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "어떤", "start_ms": 180, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "것부터", "start_ms": 300, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "할까요", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 5.0}
]
},
{
"segment": 3,
"transcript": "단어와 문장을 하나씩 읽어봐요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "단어와", "start_ms": 55, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "문장을", "start_ms": 195, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "하나씩", "start_ms": 345, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "읽어봐요", "start_ms": 475, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 5,
"transcript": "한국어 발음이 훨씬 좋아졌네요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "발음이", "start_ms": 200, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "훨씬", "start_ms": 350, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "좋아졌네요", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "꾸준히 연습한 보람이 있네요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "꾸준히", "start_ms": 35, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "연습한", "start_ms": 185, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "보람이", "start_ms": 325, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "있네요", "start_ms": 445, "dur_ms": 120, "phase_lr_deg": 5.0}
]
},
{
"segment": 7,
"transcript": "앞으로도 계속 힘내세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "앞으로도", "start_ms": 60, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 230, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "힘내세요", "start_ms": 350, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "격려해 주셔서 정말 고맙습니다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "격려해", "start_ms": 20, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "주셔서", "start_ms": 160, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "정말", "start_ms": 290, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "고맙습니다", "start_ms": 410, "dur_ms": 160, "phase_lr_deg": 5.0}
]
},
{
"segment": 9,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 10,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 11,
"transcript": "오늘 수업은 여기까지입니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "수업은", "start_ms": 170, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "여기까지입니다", "start_ms": 310, "dur_ms": 260, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "다음에 또 유익한 내용으로 만나요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 25, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 145, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "유익한", "start_ms": 215, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "내용으로", "start_ms": 345, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 495, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 14,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 15,
"transcript": "모두 안녕히 가세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "모두", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 160, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 320, "dur_ms": 250, "phase_lr_deg": 0.0}
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
"transcript": "안녕히 계세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 60, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "계세요", "start_ms": 220, "dur_ms": 350, "phase_lr_deg": 5.0}
]
},
{
"segment": 18,
"transcript": "그럼 수요일에 뵙겠습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "수요일에", "start_ms": 140, "dur_ms": 160, "phase_lr_deg": 5.0},
{"word": "뵙겠습니다", "start_ms": 310, "dur_ms": 260, "phase_lr_deg": 5.0}
]
},
{
"segment": 19,
"transcript": "네 그때 봐요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "그때", "start_ms": 110, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "봐요", "start_ms": 230, "dur_ms": 340, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 21,
"transcript": "공부하시느라 정말 고생 많으셨어요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "공부하시느라", "start_ms": 50, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 270, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "고생", "start_ms": 390, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "많으셨어요", "start_ms": 500, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "도움이 필요하시면 언제든지 연락 주세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "도움이", "start_ms": 25, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "필요하시면", "start_ms": 145, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "언제든지", "start_ms": 295, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "연락", "start_ms": 435, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 535, "dur_ms": 40, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "네 감사합니다 선생님",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 45, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "감사합니다", "start_ms": 115, "dur_ms": 240, "phase_lr_deg": 5.0},
{"word": "선생님", "start_ms": 365, "dur_ms": 210, "phase_lr_deg": 5.0}
]
},
{
"segment": 24,
"transcript": "안녕히 가세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 210, "dur_ms": 360, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "조심히 들어가세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "조심히", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "들어가세요", "start_ms": 190, "dur_ms": 380, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "오늘 배운 것들을 잊지 마세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 35, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "배운", "start_ms": 155, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "것들을", "start_ms": 275, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "잊지", "start_ms": 405, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "마세요", "start_ms": 505, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "집에서 꼭 복습해 보세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "집에서", "start_ms": 45, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "꼭", "start_ms": 185, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "복습해", "start_ms": 255, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "보세요", "start_ms": 405, "dur_ms": 170, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "수고하셨습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "수고하셨습니다", "start_ms": 60, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 32,
"transcript": "네 안녕히 가세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "안녕히", "start_ms": 110, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "가세요", "start_ms": 270, "dur_ms": 300, "phase_lr_deg": 5.0}
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
"transcript": "오늘 수업도 정말 알찼습니다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 70, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "수업도", "start_ms": 190, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "정말", "start_ms": 320, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "알찼습니다", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 5.0}
]
},
{
"segment": 35,
"transcript": "항상 정성껏 가르쳐 주셔서 고마워요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "항상", "start_ms": 30, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "정성껏", "start_ms": 160, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "가르쳐", "start_ms": 300, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "주셔서", "start_ms": 440, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "고마워요", "start_ms": 560, "dur_ms": 10, "phase_lr_deg": 5.0}
]
},
{
"segment": 36,
"transcript": "네 별말씀을요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 60, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "별말씀을요", "start_ms": 130, "dur_ms": 440, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "그럼 다음에 뵐게요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "다음에", "start_ms": 165, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "뵐게요", "start_ms": 315, "dur_ms": 260, "phase_lr_deg": 5.0}
]
},
{
"segment": 38,
"transcript": "잘 가요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "잘", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "가요", "start_ms": 160, "dur_ms": 410, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "바이 바이",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "바이", "start_ms": 35, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "바이", "start_ms": 205, "dur_ms": 370, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 41,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 42,
"transcript": "오늘도 행복한 하루 되세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "오늘도", "start_ms": 20, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "행복한", "start_ms": 170, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "하루", "start_ms": 330, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "되세요", "start_ms": 450, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "모두 건강 잘 챙기세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "모두", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "건강", "start_ms": 170, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 290, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "챙기세요", "start_ms": 360, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 45,
"transcript": "감사합니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 40, "dur_ms": 530, "phase_lr_deg": 0.0}
]
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
"transcript": "나중에 또 봐요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "나중에", "start_ms": 30, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 200, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "봐요", "start_ms": 290, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "잘 가요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "잘", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "가요", "start_ms": 170, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "안녕",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "안녕", "start_ms": 40, "dur_ms": 530, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 07:57:41*
