# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=08:07:28  uptime=24494s  mem=1635MB  system_processes=448"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_080718.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 617-613-617-163-176-167-107-617-760-107-320-260-206-261-260-260-106-167-527-567-163-167-570-570-607-106 657-312-306-302-302-327-371-127-127-236-540-507-103-126-547-370-236-203-7_V.mp3)
  Segment 2: 1576ms ~ 13564ms (file: 132-213-217-132-132-172-132-132-123-132-132-312-132-162-132-312-132 167-713-136 752-765-672-627-672-675-657-675-675-567-136-201-157-312-126-271-207-207-201 546-546-260 207-275-2_V.mp3)
  Segment 3: 14564ms ~ 15140ms (file: 267-627-276-627-672-267-627-267-627-627-627-627-267-367-367-672-362-276-627-267-672-267-236-326-620-326-326-261-263-320-267-325-263-236-326-362-362-163-167-321-126-216-267-623-6_W.mp3)
  Segment 4: 16140ms ~ 16716ms (file: 106-136-130-301-130-574-750-750-732-130-103-136-167-176-574-547-507-715-657-765-170-203-572-520-203-102-132-103-132-136-103 627-602-627-267-263-572-752-752-576-756-765 657-675-7_W.mp3)
  Segment 5: 17716ms ~ 18292ms (file: 760-761-763-672-761-763-673 324 673-617-631-167-256_W.mp3)
  Segment 6: 19292ms ~ 19868ms (file: 620-261-621-132-120-231-251-573-576-576-576-576-567-657-567-675 763-163-136-163-316-657-651-567 310-316-136-567-572-576 312-625-263-623-265 620-570-362-326_W.mp3)
  Segment 7: 20868ms ~ 21444ms (file: 701-130-756-657-675-672-620-260-216-206-260-672-672-675-672-672-627-672-765-672-206-236-360-263-260-260-260-725-762-276-126 571-312-276-542-540-520-321-132-123 547-576-576-576 6_W.mp3)
  Segment 8: 22444ms ~ 23020ms (file: 105-312-302-203-302-231-201-302-321-312-571-576-657-357-321-136-320 627-621-725-365-523-576-563-312-231-136-132-132-612-162-612-632-361-631_W.mp3)
  Segment 9: 24020ms ~ 24596ms (file: 320-320-312 620-260-602-627-605-560-605-263-235-301-130-103-120-517-723-621-620-527-230-326-326-153-510-502-507-256-265-257-527-523-521-325-312-213-132-103-123-320-273-320-320-2_W.mp3)
  Segment 10: 25596ms ~ 26172ms (file: 175-617-163-761-123-103-620-671-106-317-162-612-230-657-167-176-106-632-156-176-107-103-176-103-106-134-260-175-162-136-103-103-136-657-163-764-167-517-126-137-176 716-130-326-1_V.mp3)
  Segment 11: 27172ms ~ 27748ms (file: 216-136-316-132-105-137-136-546-132-623-130-260-326-672-321 210-312-231 267-362-376-173-671-136-213-261-126-267-132-316-132-261-631 657 267-261-326-316-632-362 132-132-123-625-6_W.mp3)
  Segment 12: 28748ms ~ 29324ms (file: 106-105-261-260-620-621-260-602-620-620-620-620-602-267-620-623-621-163-206-261-206-261-203-162-162-106-106-657-756-706-275-126-136-570-570-574-167-167-163-621-627-612-627-617-1_W.mp3)
  Segment 13: 30324ms ~ 30900ms (file: 326-236-236-230-236-320-625-652-562-675-605-675-265-260-206-320-130-301-123-312-321-123-136-175-261-715-132-320-540-250-620-260-320-562-152-206-236-130-321-130-576-627-260-267-2_V.mp3)
  Segment 14: 31900ms ~ 32476ms (file: 130-613-673-670-675-675-765-765-607-675-627-260-267-207-137-143-132-162-162-163-267-631-132-613-132-675-627-602-506-560-506-602-130-310-306-261-206 136-620-621-312 123-302-312-1_V.mp3)
  Segment 15: 33476ms ~ 34052ms (file: 621-765-765-756-765-756-756-670-163 765_W.mp3)
  Segment 16: 35052ms ~ 35628ms (file: 562-650-265-130-136-137-627-623-602-126-106-130-130-516-132-136-103-136-132-310-150-106-136-651-130-103-103-103-137-316-301-301-362-312-136-103-163-132-167-163-163-312-213-201-2_V.mp3)
  Segment 17: 36628ms ~ 37204ms (file: 167 103-103-310-103-625-176-136-671-162-103-163-136-165-106-162-176-163 132-103-107-167-157-257-103-632-715-167-613-103-103-106-127-203-127-123-361-312-103-176-103-106-136-326-6_V.mp3)
  Segment 18: 38204ms ~ 38780ms (file: 612-260-137-617-312-126-613-603-675-635-236-216-601-162-126-120-326-672-673-632-362-302-320-301-361-251-623-162-326-671-765-627-267-267-123-312-301-107-753-103-123-167-316-103-1_V.mp3)
  Segment 19: 39780ms ~ 40356ms (file: 756-267-263-263-267-267-236-123-132-365-736-127-136-136-310-130-162-123-106-567-123-107-103-106-137-103-165-103-267-623-672-321-106-130-102-162-135-103-103-103-261-326-106-316-1_V.mp3)
  Segment 20: 41356ms ~ 41932ms (file: 312-302-735-756-672-627-265-526-257-265-236-601-613-120-136-312-610-672-316-236-716-576-302-230-103-103-103-106-163-106-176-136-137-621-103-103-136-102-617-157-160-601-126-762-2_V.mp3)
  Segment 21: 42932ms ~ 43508ms (file: 670-673-367-673-367-367-376-367-376-367-637-673-672-367-673-673-673-763-673-673-763-637-763-673-671-673-672-673-637-637-367-673-637-673-367-367-637-367-367-637-637-631 376-673-6_W.mp3)
  Segment 22: 44508ms ~ 45084ms (file: 372-103-310-607-675-607-670-762-276-267-126-160-316-136-610-136-132-136-102-132-301-306-320-362-706-132-163-137-130-136-716-715-705-670-621-173-170-216-602-261-302-321-326-372-2_V.mp3)
  Segment 23: 46084ms ~ 46660ms (file: 637-657-756-567-310-362-637-310-160-306_W.mp3)
  Segment 24: 47660ms ~ 48236ms (file: 576-571-536-572-567-527-567-526-572-752-362-621-216-127-620_W.mp3)
  Segment 25: 49236ms ~ 49812ms (file: 237-327-315-321-213-321-327-371-372-723-275-762-675-574-576-576-562-265-231-236-275-265-123-235-213-125-231-615-236-213-236-514-653-635-267-627-672-613-631-675-675-213-235-231-2_W.mp3)
  Segment 26: 50812ms ~ 51388ms (file: 132-126-510-513-150-175-167-316-173-135-175-130-316-316-236-105-130-132-136-130-310-623-127-126-675-167-103-163-162-263-132-761-517-173-631-136-136-130-136-103-175-574-160-176-1_V.mp3)
  Segment 27: 52388ms ~ 52964ms (file: 206-623-167-137-175-135-547-576-576-567-657-326-320-326-326-672-617-631-367-301-261-362-260-623-603-602-613-361-613-361-163-162-326-136-312-106-362-126-102-103-302-637-103-130-1_W.mp3)
  Segment 28: 53964ms ~ 54540ms (file: 507-573-302-302-301-160-201-261-216-103-126-612-162-153-306-603-306-263-263-206-203-203-162-162-261-267-267-541-574-562-602-203-132-312-531-530-257-312-217-524-152-546-564-263-3_W.mp3)
  Segment 29: 55540ms ~ 56116ms (file: 715-635-165-526-631-613-103-567-167-361-561-651-576-657-576-576-576-576-106_W.mp3)
  Segment 30: 57116ms ~ 57692ms (file: 132-571-125-126-125-103-657-126-613-163-765-105-726-621-103-103-165-103-617-162-167-613-160-562-513-176-215-216-765-152-106-106-610-103-106-136-126 574-754-751-765_V.mp3)
  Segment 31: 58692ms ~ 59268ms (file: 162-162-675-160-216-126-106-706-672-312-106-103-132-123-102-123-102-231-163-132-103-126-605-132-546-671-132-106-162-650-162-601-623-126-132-103-126-136-103-126-617-721-165-103-1_V.mp3)
  Segment 32: 60268ms ~ 60844ms (file: 173-307 617-610-601-326-132-126-324_V.mp3)
  Segment 33: 61844ms ~ 62420ms (file: 156-156-156-315-351-315-531-156-516-516-156-157-516-157-156-156-765-567-617-561-517-516-571-175-675-657-657-517-675-167-765-576-653-563-567-651-567-567-657-756-567-513-536-356-6_W.mp3)
  Segment 34: 63420ms ~ 63996ms (file: 561-103-135-103-651-613-105-657-136-312-326-316-132-631-106-175-613-136-176-136-672-627-261-623-576-751-765-675-162-165-712-576-507-537-612-316-236-315-312-137-312-132 312-361-1_W.mp3)
  Segment 35: 64996ms ~ 65572ms (file: 123 641-274-607 165 367-365-376-756 572-574-576-576 756-675-520-605-236-675-670-572-567-256-136-136-136-176-132-316 354-315-316-312-126-216-105-165-162-132-137-132-126-162-123-3_V.mp3)
  Segment 36: 66572ms ~ 67148ms (file: 265-265-265-256-265-625-567-562-537-576-376-765-576-576-567-540-547-576-762-267-627-273-632-625-365-156-567-651-567-576-576-576-567-567-576-573-762-627-623-273-327-627-231-321-6_W.mp3)
  Segment 37: 68148ms ~ 68724ms (file: 752-132-312-231-235-312-213-203-326-273-312-317-321-123-123-132-307-137-123-126-123-317-132-576-103-713-236-306-203-123-627-625-627-267-261-263-123-103-540-216-123-132-231-132-1_W.mp3)
  Segment 38: 69724ms ~ 70300ms (file: 130-106-671-162-167-167-601-167-106-106-106-165-360-162-103-615-162-213 315-103-103-130-103-263-162-126-652-106-263-362-126-132-231-162-263-263-567-126-130-127-615-163-362-106-1_V.mp3)
  Segment 39: 71300ms ~ 71876ms (file: 673-316-106-613-617-106-102-120-623-236-603-260-263-320-136-163-106-132-326-570-126-261-621-672-206-263-236-263-263-236-120-612-627-621-267-627-632-362-736-706-657-576-765-675-6_W.mp3)
  Segment 40: 72876ms ~ 74460ms (file: 230-203-236-123-312-632-263-326-603-632-310-320-560-567-102-102-156-153-547-574-576-507-576-765-756-756-756-571-610-765-617-675-756-675-671-673-713-765-675-765-675-765-756-675-7_W.mp3)
  Segment 41: 75460ms ~ 76036ms (file: 103-136-316-132-312-516-361-162-601-613-621-612-613-654-652-760-627-602-602-612-627-624 132 132-310-312-316-312-137-143-132-104-126-126-102-132-713-715-706-713-413-132-421-251-5_V.mp3)
  Segment 42: 77036ms ~ 77612ms (file: 627-627-126-671-617-136-130-670-136-126-103-106-106-312-160-176-132-713-316-671-670-107-603-103-103-132-163-321-172-126 103-176-132-172-172 761-607-601-261-610-632-620-623-762-1_V.mp3)
  Segment 43: 78612ms ~ 79188ms (file: 301-305-576-675-627-103-630-560-106-162-106-106-162-130-123-136-106-160-267-206-206-263-620-620-620-263-260-312-301-321-301-261-256-625-632-310-301-301-620-620-620-103-103-132-3_V.mp3)
  Segment 44: 80188ms ~ 80764ms (file: 625-627-623-652-627-627-231-216-261-261-254-527-572-263-136-672-672-671-671-326-236-172-671-716-671-671-672-623-126-217-312-317-317-371-713-317-371-371-370-371-732-327-367-367-1_W.mp3)
  Segment 45: 81764ms ~ 82340ms (file: 675-672-167-627-672-765-670-627-160-672-627-167-672-726-671-716-762-167-706-675-310-163-561-362-301-103-106-702-167-106-106-167-176-503-574-103-103-627-206-621-213-126-136-216-1_V.mp3)
  Segment 46: 83340ms ~ 83916ms (file: 265-123 675-765-765-675-765-765-765-765-176-517-756 623-756-756-765 765-765-710-715-751-756-765-761 765-763-715-761-716-706-760-763-765-762-765-765-765-756-762-756-751-765-756-7_W.mp3)
  Segment 47: 84916ms ~ 85492ms (file: 103-130-302-532-350-517-203-302-372-203-236-320-231-312-321-132-620-620-261-263-206-203-302-372-132-236-573-570-163-632-576 652-650-672-627-126-167-617-167-671-765-167-216-167-3_W.mp3)
  Segment 48: 86492ms ~ 87068ms (file: 130-312-123-216-102-615-126-160-127-106-103-103-132-312-103-163-106-176-107-106-123-103-103 302-301-326-263-627-230-126-236-162-163-164-621-263-617-512-163-716-761-763-136-103-2_V.mp3)
  Segment 49: 88068ms ~ 88644ms (file: 312-326-267-276-276-136-103-162-103-123-123-310-167-102-130-103-176-132-136-165-136-601-103-136-615-316-613-136-162-725-167-107-125-327-236-132 312-130-267-267_V.mp3)
  Segment 50: 89644ms ~ 90220ms (file: 627-276-216-612-216-326-316-162-163-201-136-623-361-136-631-162-260 163-162-761-613-672-167-167-671-762-671-163-132-301-103-136-102-130-132-103-301-136-132-261-162-106-162-623-6_W.mp3)
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
> **Time**: 2026-04-03 08:08:21
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "오늘 수업은 정말 여기까지입니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "수업은", "start_ms": 160, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 300, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "여기까지입니다", "start_ms": 420, "dur_ms": 156, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "질문이 있으시면 편하게 말씀해 주세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "질문이", "start_ms": 30, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "있으시면", "start_ms": 180, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "편하게", "start_ms": 340, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "말씀해", "start_ms": 470, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 610, "dur_ms": 140, "phase_lr_deg": 0.0}
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
"transcript": "오늘 배운 표현들을 꼭 복습해 보세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "배운", "start_ms": 180, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "표현들을", "start_ms": 300, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "꼭", "start_ms": 450, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "복습해", "start_ms": 520, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "보세요", "start_ms": 670, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "그럼 다음 시간에 또 뵙겠습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 150, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "시간에", "start_ms": 270, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 410, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "뵙겠습니다", "start_ms": 480, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "안녕히 가시고 행복한 하루 되세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 35, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "가시고", "start_ms": 185, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "행복한", "start_ms": 315, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "하루", "start_ms": 455, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "되세요", "start_ms": 575, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "네 선생님 감사합니다 안녕히 계세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 60, "dur_ms": 50, "phase_lr_deg": 5.0},
{"word": "선생님", "start_ms": 120, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "감사합니다", "start_ms": 280, "dur_ms": 240, "phase_lr_deg": 5.0},
{"word": "안녕히", "start_ms": 530, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "계세요", "start_ms": 690, "dur_ms": 180, "phase_lr_deg": 5.0}
]
},
{
"segment": 8,
"transcript": "조심히 들어가세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "조심히", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "들어가세요", "start_ms": 190, "dur_ms": 386, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "언제든지 궁금한 게 생기면 알려주세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "언제든지", "start_ms": 25, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "궁금한", "start_ms": 195, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 335, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "생기면", "start_ms": 385, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "알려주세요", "start_ms": 535, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "네 조심히 가세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 45, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "조심히", "start_ms": 115, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "가세요", "start_ms": 265, "dur_ms": 140, "phase_lr_deg": 5.0}
]
},
{
"segment": 11,
"transcript": "바이 바이",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "바이", "start_ms": 55, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "바이", "start_ms": 225, "dur_ms": 351, "phase_lr_deg": 0.0}
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
"segment": 14,
"transcript": "수고 많으셨습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "수고", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "많으셨습니다", "start_ms": 160, "dur_ms": 416, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 16,
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
"segment": 17,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 18,
"transcript": "행복하세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "행복하세요", "start_ms": 25, "dur_ms": 551, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
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
"transcript": "진짜 안녕",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 60, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "안녕", "start_ms": 210, "dur_ms": 366, "phase_lr_deg": 0.0}
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
"transcript": "끝",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "끝", "start_ms": 35, "dur_ms": 541, "phase_lr_deg": 0.0}
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
"transcript": "진짜 진짜 안녕",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 190, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "안녕", "start_ms": 340, "dur_ms": 236, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "바이 바이",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "바이", "start_ms": 55, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "바이", "start_ms": 225, "dur_ms": 351, "phase_lr_deg": 0.0}
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
"transcript": "수고",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "수고", "start_ms": 30, "dur_ms": 546, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
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
"segment": 30,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 31,
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
"transcript": "바이",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "바이", "start_ms": 50, "dur_ms": 526, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
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
"transcript": "안녕",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "안녕", "start_ms": 60, "dur_ms": 516, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 38,
"transcript": "바이 바이",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "바이", "start_ms": 30, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "바이", "start_ms": 200, "dur_ms": 376, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 40,
"transcript": "진짜 안녕",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 55, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "안녕", "start_ms": 205, "dur_ms": 371, "phase_lr_deg": 0.0}
]
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
"transcript": "수고",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "수고", "start_ms": 40, "dur_ms": 536, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "고생",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "고생", "start_ms": 35, "dur_ms": 541, "phase_lr_deg": 0.0}
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
"transcript": "안녕",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "안녕", "start_ms": 25, "dur_ms": 551, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 49,
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
"segment": 50,
"transcript": "진짜 끝",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "끝", "start_ms": 200, "dur_ms": 376, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 08:08:21*
