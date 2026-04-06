# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=06:27:06  uptime=18471s  mem=1604MB  system_processes=445"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_062657.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 103-157-126-517-172-263-267-213-236-576-517-574-623-536-136 132-123_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 210-316-310-103-601-601-601-613-610-610-621-612-750-705-260-263-506-562-260-567-362-263-231-203-205-203-310-130-103-132-136-132-103-316-236-320-302-136-321-520-132-136-120-123-6_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 657-265-263-526-635-672-675-607-672-726-762-126-526-257-236-261-263-265-267-267-263-265-152-576-163-163-316-210-132-136-126-136-126-627-326-263-236-326-326-236-632-265-236-256-7_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 302-307-210-120-132-176-107-317-312-132-137-132-763-713-206-231-321-137-132-126-123-126-132-302-627-132-236-312-132-123-132-132-107-312-123-302-207-706-705-736-726-617-167-132-3_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 175-375-173-176-675-675 617-136-312-567-156-362-156-531-517-513-567-527-571-357-152-165-135-217-132-521-156-531-652-652-156-567-567-567-756-567-576-675-576-567-563-126-657-126-5_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 765-762-672-672-672-162-103-103-136 576-675-675 761-671_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 130-641-136-162-621-126-263-231-320-236-126-316-267-236-620-236-620-267-261-267-621-326-623-612-571-517-301-316-361-163-136-570-576-176-176-670-132-103_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 167-716 132-312-213-231-637-671-362-162_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 671-621-163 103-163-132-176-513-527-571_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 216-136-127-103-603-176-376-132-361-301-130-160-136-567-602-127-612-316-103-102-130-136-672-263-130-312-612-163-612-306-130-103-163-236-623-126-162-136-103-106-167-167-103-617-1_V.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 132-613-574-607-613-672-756-750-705-132-765-756-750-765-607-620-627-602-163-163-214-231-326-625-621-132-143-164-761-607-761-672-326-132-756-170-175-312-713-672-621-132-136-132-1_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 231-135-372-327-132-132-765-312-137-726-265-621-627-207-206-267-312-312-321-136-372-231-321-321-321-326-320-132-312-132-126-123-132-136-326-625-620-230-230-302-320-567-316-231-3_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 102-312-613-631-135-657-312-103-106-136-106-136-301-132-301-231-302-130-103-130-310-132-302-320-302-230-302-136-613-312-631-362-236-136-310-106-612-106-102-132-123-132-231-326-2_V.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 315-657-756-165-517-571-315-175-375-123-263-326-213-271-153-157-176-126-712-726-765-175-751-761-276-175-126-217-176-517-136-571-135-135-163-615-261-216-231-213-216-126-136-136-3_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 547-574-547 230-103-601-321-206-326-607-657-762-564-510-621-261-612-206-672-136-612-263-621-203-362-302-123-613-316-631-621-637-673-361-613-617-617-576-675-673-637-136-237-302-3_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 763-763-762-763 132-762-762 752-317-371-376-617-165-173-673-763-763-723-106-136-367-673-763-167-163-365-163-613-103-106-127-175-761-103-163-106-106-160-136-671-103-361-106-156-1_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 126-162-126-765-675-765-572-653-765-675-657-675-657-675-756-567-756-675-576-756-574-576-574-576-576-576-576-547-657-576-765-562-671-675-673-316 167-123-612-617-316 756-756-756-6_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 671-672-671 362-162-617-163 367-613-621-612-162-316-162-103-130-132-103-130_V.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 130-103-710-120-103-601-627-573-527-236-236-230-162-506-567-576-576-672-612-167-706-617-671-756-765-672-726-706-136-735-701-713-120-102-126-126-102-316-127-105-561-106-163-136-7_V.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 260-206-260-267-627-267-237-127-132-316-162-236-207-257-260-263-326-726-217-230-132-623-263-310-123-302-126-206-206-263-206-321-123-213-213-213-213-263-230-130-130-132-123-316-2_W.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 270-705-172-652-623-623-672-265-265-502 725 547_W.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 106-637-106-160-102-130-617-165-651-765-106-136-165-105-516-310-103-136-135-103-156-175-107-103-165-136-103-136-103-671-160-675-675-756-675-635-103-631-567-320-675-162-602-103-3_V.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 621-163-623-623-632-312-312-236-201-126-162-126-162-126-567-625-576-176-705-706-671-672-612-302-312-302-327-627-206-263-263-301-321-326-632-632-612-206-621-632-162-301-310-312-3_V.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 761-176-213-130-632-263-261-612-261-615-672-716-160-163 267-267-267-267-276_W.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 126-167-162-102-167-167-103-136-103-167-751-162-716-156-103-106-130-103-103-263-236-752-236-576-572-753-752-576 326-216_V.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 621-326-236-721-632-263-632-623-230-236-627-627_W.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 125-631-163-526-106-167-610-675-326-612-306-106-136-103-130-165-263-627 132-167-521-632-132-136-163-103-130-756-263-172 235-261-102_V.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 243 206-270-726-572-765-176-170-107-106-167-127-215-163-163-163-613-136-631-631-612-613-613-163 726-216-126-713-602-127-103-261-120-617-210-130-671-301-103-103-136-103-136-306-1_W.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 567-567-567-567-562-567-672-276-265-625-132-562-362-536-536-562-567-652-567-576-576-567-256-267-256-267-657-567-657-576-627-251-526-567-657-567-567-657-567-567-756-765-576-567-5_W.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 103-103-130-130-301-136-316-136-136-132-134-132-621-620-602-612-601-602-621-206-206-623-126-264-612-163-617-163-620-601-231-261-670-623-632-106-106-147-612-132-627-602-607-602-6_V.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 103-132-103-132-136-152-125-107-175-627-123-162-261-263-236-321-132-105-157-157-137-761-123-312-301-301-510-136-137-103-317-516-160-162-316-163-623-623-372-375-672-736-756-765-7_V.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 632-106-157-612-103-316-103-107-106-106-106-106-267-267-725-726-726-672-675-675-326 542-254-254-274-132-213-260-206-132-130-103-106-106-162-160-160-103-130-203-230-236-527-573-2_V.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 103-102 173-107-316-103-132-136-130-312-601-167-163-102-103-106-167-103-130-132-103-571-175-675-567-507-531-136-162-163-167-165-652-675-615-135-160-120-715-756-675-127-607-617-6_V.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 263-751-765-765-702-726-167-102-610-160-613-617-165-167-167-105-125-105-163-136-126 106-103-103-163-160-163-126-106-103-137-106-126-162-106-163-617-163-621-621-326-612-261-160-1_W.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 321-231-206-231-206-620-602-607-326-103-162-106-201-102-231-321-573-206-216-126-203-301-207-203-102-706-671-163-316-163-176-103-137-103-173-103-136-126-176-162-167-176-136-130-1_V.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 563-257-267-362-326-621-375-517-562-615-136-564-567-564-546-653-632-632-627-627-675-567-651-653-513-237-327-215-564-567-564-657-564-653-623-621-621-651-672-657-567-265-125-567-5_W.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 203-230-231-236-231-216-672-362-613-130-263-756-170-702-132-132-210-160-316-163-132-163-130-162-321-163-627-760-371-163-706-761-617-163-601-163-362-675-167-623-132-132-130-106-1_W.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 627-623-627-672-671 105-132-136-613-165-621-176-163-163-123-132-103-167-132-132-126-123-103-167-106-106-103-562-165-162-671-123-157-123-612-137-103-103-167-576-126-103-160-106-2_V.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 621-765-765-675-615-671-675-675-672-612-612-102-160 715-750-735-162-621-263-160-130_W.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 102-651-263-675-607-673-671-132-713-132-316-651-165-103-156-165-165-675-160-106-176-617-167-670-106-671-231-625-567-165-106-617-362-360-301-301-167-160-103-671-160-362-326-156-1_V.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 517-567-562-516-156-561-561-135-165-651-365-631-152-163-265-231-621-615-623-625-652-621-625-136-163-165-165-617-165-125-162-216-126-156-165-156-516-651-156-156-615-156-156-150-1_W.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 163-617-673-671-617-106-671-615-362-601-613-106-152-125-106-106-216-261-126-602-621-312-413-103-160-167-102-126-601-316-632-261-260-261-160-612-236-327-637-316-312-602-657-612-2_V.mp3)
  Segment 43: 66192ms ~ 66768ms (file: 142-134-137-617-632-312-102-267-267-263 362 123-312 657-631 153-301-301-312-301-136-321-326-302-261-263-206-620-162-612-162-162-126-102-126-320-236-302-263-627-267-260-203-320-2_V.mp3)
  Segment 44: 67768ms ~ 68344ms (file: 756-576-576-657-657-561-657-561-563-576-576-567-657-156-576-563-567-653-536-657-653-657-576-567-657-756-165-657-567-561-567-567-756-567-567-576-567-657-675-657-567-561-615-361-5_W.mp3)
  Segment 45: 69344ms ~ 69920ms (file: 160-610-601 162-160-160 621-612-627-312_W.mp3)
  Segment 46: 70920ms ~ 71496ms (file: 312-213-316-631-362-316-316-321-132-136-163-316-362-316-256-267-517-257-576-351-235-321-352-526-537-576-561-103-130-310-261-235-216-321-231-213-540-564-536-236-125-216 512-321-1_W.mp3)
  Segment 47: 72496ms ~ 73072ms (file: 132-105-372-127-152-136-217-123-321-132_V.mp3)
  Segment 48: 74072ms ~ 74648ms (file: 326-103-136-136-276-173-126 160-657-106-103-175-157-167-165-756-567-657-761-756-756-762-716-765-136-617-123-625-167-105-125-165-153-671-612-261-107-136-126-162-123-103-132-312-1_W.mp3)
  Segment 49: 75648ms ~ 76224ms (file: 123-231-326-617 572-562-326-632-326_W.mp3)
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
> **Time**: 2026-04-03 06:28:23
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "오늘 컨디션이 어때요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 60, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "컨디션이", "start_ms": 180, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "어때요?", "start_ms": 340, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "잠을 푹 자서 아주 개운해요.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "잠을", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "푹", "start_ms": 160, "dur_ms": 50, "phase_lr_deg": 5.0},
{"word": "자서", "start_ms": 220, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "아주", "start_ms": 310, "dur_ms": 90, "phase_lr_deg": 5.0},
{"word": "개운해요.", "start_ms": 410, "dur_ms": 140, "phase_lr_deg": 5.0}
]
},
{
"segment": 3,
"transcript": "I feel great because I slept well.",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 40, "dur_ms": 40, "phase_lr_deg": -2.0},
{"word": "feel", "start_ms": 90, "dur_ms": 60, "phase_lr_deg": -2.0},
{"word": "great", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": -2.0},
{"word": "because", "start_ms": 250, "dur_ms": 100, "phase_lr_deg": -2.0},
{"word": "I", "start_ms": 360, "dur_ms": 30, "phase_lr_deg": -2.0},
{"word": "slept", "start_ms": 400, "dur_ms": 90, "phase_lr_deg": -2.0},
{"word": "well.", "start_ms": 500, "dur_ms": 60, "phase_lr_deg": -2.0}
]
},
{
"segment": 4,
"transcript": "그것참 다행이네요. 저는 좀 피곤해요.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "그것참", "start_ms": 55, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "다행이네요.", "start_ms": 185, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "저는", "start_ms": 325, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 405, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "피곤해요.", "start_ms": 455, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "어제 늦게까지 일을 하셨나요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "어제", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "늦게까지", "start_ms": 155, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "일을", "start_ms": 295, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "하셨나요?", "start_ms": 405, "dur_ms": 140, "phase_lr_deg": 5.0}
]
},
{
"segment": 6,
"transcript": "(noise)",
"speaker": "null",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 7,
"transcript": "네, 프로젝트 마감이라 좀 무리했어요.",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 70, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "프로젝트", "start_ms": 140, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "마감이라", "start_ms": 290, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 410, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "무리했어요.", "start_ms": 460, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "Oh no. You must be exhausted.",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "Oh", "start_ms": 35, "dur_ms": 30, "phase_lr_deg": -2.0},
{"word": "no.", "start_ms": 75, "dur_ms": 50, "phase_lr_deg": -2.0},
{"word": "You", "start_ms": 135, "dur_ms": 50, "phase_lr_deg": -2.0},
{"word": "must", "start_ms": 195, "dur_ms": 60, "phase_lr_deg": -2.0},
{"word": "be", "start_ms": 265, "dur_ms": 40, "phase_lr_deg": -2.0},
{"word": "exhausted.", "start_ms": 315, "dur_ms": 200, "phase_lr_deg": -2.0}
]
},
{
"segment": 9,
"transcript": "(noise)",
"speaker": "null",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 10,
"transcript": "오늘까지만 고생하면 주말엔 쉴 수 있어요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "오늘까지만", "start_ms": 60, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "고생하면", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "주말엔", "start_ms": 310, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "쉴", "start_ms": 420, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 470, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "있어요.", "start_ms": 510, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "주말에 특별한 계획이 있나요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "주말에", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "특별한", "start_ms": 160, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "계획이", "start_ms": 280, "dur_ms": 90, "phase_lr_deg": 5.0},
{"word": "있나요?", "start_ms": 380, "dur_ms": 160, "phase_lr_deg": 5.0}
]
},
{
"segment": 12,
"transcript": "그냥 집에서 잠만 잘 생각이에요.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "그냥", "start_ms": 50, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "집에서", "start_ms": 150, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "잠만", "start_ms": 260, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 360, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "생각이에요.", "start_ms": 410, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "(noise)",
"speaker": "null",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 14,
"transcript": "I'm just going to sleep all weekend.",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "I'm", "start_ms": 30, "dur_ms": 40, "phase_lr_deg": -2.0},
{"word": "just", "start_ms": 80, "dur_ms": 50, "phase_lr_deg": -2.0},
{"word": "going", "start_ms": 140, "dur_ms": 60, "phase_lr_deg": -2.0},
{"word": "to", "start_ms": 210, "dur_ms": 30, "phase_lr_deg": -2.0},
{"word": "sleep", "start_ms": 250, "dur_ms": 70, "phase_lr_deg": -2.0},
{"word": "all", "start_ms": 330, "dur_ms": 40, "phase_lr_deg": -2.0},
{"word": "weekend.", "start_ms": 380, "dur_ms": 150, "phase_lr_deg": -2.0}
]
},
{
"segment": 15,
"transcript": "그것도 좋지만 맛있는 거라도 챙겨 드세요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "그것도", "start_ms": 65, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "좋지만", "start_ms": 185, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "맛있는", "start_ms": 295, "dur_ms": 90, "phase_lr_deg": 5.0},
{"word": "거라도", "start_ms": 395, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "챙겨", "start_ms": 485, "dur_ms": 50, "phase_lr_deg": 5.0},
{"word": "드세요.", "start_ms": 545, "dur_ms": 30, "phase_lr_deg": 5.0}
]
},
{
"segment": 16,
"transcript": "(noise)",
"speaker": "null",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 17,
"transcript": "안 그래도 맛있는 배달 음식을 시켜 먹으려고요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "안", "start_ms": 50, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "그래도", "start_ms": 100, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "맛있는", "start_ms": 190, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "배달", "start_ms": 300, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "음식을", "start_ms": 390, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "시켜", "start_ms": 490, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "먹으려고요.", "start_ms": 550, "dur_ms": 20, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "피자나 치킨 어떠세요?",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "피자나", "start_ms": 80, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "치킨", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "어떠세요?", "start_ms": 310, "dur_ms": 200, "phase_lr_deg": 5.0}
]
},
{
"segment": 19,
"transcript": "I'm thinking of ordering pizza or fried chicken.",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "I'm", "start_ms": 40, "dur_ms": 40, "phase_lr_deg": -2.0},
{"word": "thinking", "start_ms": 90, "dur_ms": 80, "phase_lr_deg": -2.0},
{"word": "of", "start_ms": 180, "dur_ms": 30, "phase_lr_deg": -2.0},
{"word": "ordering", "start_ms": 220, "dur_ms": 110, "phase_lr_deg": -2.0},
{"word": "pizza", "start_ms": 340, "dur_ms": 70, "phase_lr_deg": -2.0},
{"word": "or", "start_ms": 420, "dur_ms": 30, "phase_lr_deg": -2.0},
{"word": "fried", "start_ms": 460, "dur_ms": 60, "phase_lr_deg": -2.0},
{"word": "chicken.", "start_ms": 530, "dur_ms": 40, "phase_lr_deg": -2.0}
]
},
{
"segment": 20,
"transcript": "둘 다 좋죠! 생각만 해도 행복하네요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "둘", "start_ms": 70, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 130, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "좋죠!", "start_ms": 170, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "생각만", "start_ms": 270, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "해도", "start_ms": 380, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "행복하네요.", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "(music)",
"speaker": "null",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 22,
"transcript": "푹 쉬고 에너지 충전해서 월요일에 봐요.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "푹", "start_ms": 55, "dur_ms": 50, "phase_lr_deg": 5.0},
{"word": "쉬고", "start_ms": 115, "dur_ms": 70, "phase_lr_deg": 5.0},
{"word": "에너지", "start_ms": 195, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "충전해서", "start_ms": 315, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "월요일에", "start_ms": 455, "dur_ms": 90, "phase_lr_deg": 5.0},
{"word": "봐요.", "start_ms": 555, "dur_ms": 20, "phase_lr_deg": 5.0}
]
},
{
"segment": 23,
"transcript": "네, 고마워요. 주말 잘 보내세요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 60, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "고마워요.", "start_ms": 120, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "주말", "start_ms": 240, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 350, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "보내세요.", "start_ms": 400, "dur_ms": 170, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "See you on Monday. Have a nice weekend.",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 45, "dur_ms": 50, "phase_lr_deg": -2.0},
{"word": "you", "start_ms": 105, "dur_ms": 40, "phase_lr_deg": -2.0},
{"word": "on", "start_ms": 155, "dur_ms": 30, "phase_lr_deg": -2.0},
{"word": "Monday.", "start_ms": 195, "dur_ms": 90, "phase_lr_deg": -2.0},
{"word": "Have", "start_ms": 295, "dur_ms": 60, "phase_lr_deg": -2.0},
{"word": "a", "start_ms": 365, "dur_ms": 20, "phase_lr_deg": -2.0},
{"word": "nice", "start_ms": 395, "dur_ms": 70, "phase_lr_deg": -2.0},
{"word": "weekend.", "start_ms": 475, "dur_ms": 100, "phase_lr_deg": -2.0}
]
},
{
"segment": 25,
"transcript": "(noise)",
"speaker": "null",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 26,
"transcript": "안녕히 가세요!",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 100, "dur_ms": 180, "phase_lr_deg": 5.0},
{"word": "가세요!", "start_ms": 290, "dur_ms": 280, "phase_lr_deg": 5.0}
]
},
{
"segment": 27,
"transcript": "조심히 들어가세요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "조심히", "start_ms": 80, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "들어가세요.", "start_ms": 240, "dur_ms": 330, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "(noise)",
"speaker": "null",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 29,
"transcript": "오랜만에 산에 오니까 정말 좋네요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "오랜만에", "start_ms": 60, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "산에", "start_ms": 200, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "오니까", "start_ms": 290, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 410, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "좋네요.", "start_ms": 510, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "공기가 정말 맑고 상쾌해요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "공기가", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "정말", "start_ms": 170, "dur_ms": 90, "phase_lr_deg": 5.0},
{"word": "맑고", "start_ms": 270, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "상쾌해요.", "start_ms": 380, "dur_ms": 190, "phase_lr_deg": 5.0}
]
},
{
"segment": 31,
"transcript": "The air is so fresh and clean.",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "The", "start_ms": 40, "dur_ms": 40, "phase_lr_deg": -2.0},
{"word": "air", "start_ms": 90, "dur_ms": 50, "phase_lr_deg": -2.0},
{"word": "is", "start_ms": 150, "dur_ms": 40, "phase_lr_deg": -2.0},
{"word": "so", "start_ms": 200, "dur_ms": 50, "phase_lr_deg": -2.0},
{"word": "fresh", "start_ms": 260, "dur_ms": 110, "phase_lr_deg": -2.0},
{"word": "and", "start_ms": 380, "dur_ms": 40, "phase_lr_deg": -2.0},
{"word": "clean.", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": -2.0}
]
},
{
"segment": 32,
"transcript": "정상까지 얼마나 남았나요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "정상까지", "start_ms": 55, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "얼마나", "start_ms": 195, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "남았나요?", "start_ms": 315, "dur_ms": 261, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "거의 다 왔어요. 조금만 더 힘내세요.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "거의", "start_ms": 45, "dur_ms": 70, "phase_lr_deg": 5.0},
{"word": "다", "start_ms": 125, "dur_ms": 40, "phase_lr_deg": 5.0},
{"word": "왔어요.", "start_ms": 175, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "조금만", "start_ms": 295, "dur_ms": 90, "phase_lr_deg": 5.0},
{"word": "더", "start_ms": 395, "dur_ms": 40, "phase_lr_deg": 5.0},
{"word": "힘내세요.", "start_ms": 445, "dur_ms": 131, "phase_lr_deg": 5.0}
]
},
{
"segment": 34,
"transcript": "We're almost there. Keep going!",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "We're", "start_ms": 35, "dur_ms": 60, "phase_lr_deg": -2.0},
{"word": "almost", "start_ms": 105, "dur_ms": 100, "phase_lr_deg": -2.0},
{"word": "there.", "start_ms": 215, "dur_ms": 90, "phase_lr_deg": -2.0},
{"word": "Keep", "start_ms": 315, "dur_ms": 80, "phase_lr_deg": -2.0},
{"word": "going!", "start_ms": 405, "dur_ms": 171, "phase_lr_deg": -2.0}
]
},
{
"segment": 35,
"transcript": "드디어 정상이에요! 경치가 정말 장관이네요.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "드디어", "start_ms": 65, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "정상이에요!", "start_ms": 175, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "경치가", "start_ms": 325, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 445, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "장관이네요.", "start_ms": 535, "dur_ms": 41, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "힘들게 올라온 보람이 있네요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "힘들게", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "올라온", "start_ms": 180, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "보람이", "start_ms": 320, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "있네요.", "start_ms": 440, "dur_ms": 136, "phase_lr_deg": 5.0}
]
},
{
"segment": 37,
"transcript": "It was worth the effort.",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "It", "start_ms": 40, "dur_ms": 30, "phase_lr_deg": -2.0},
{"word": "was", "start_ms": 80, "dur_ms": 50, "phase_lr_deg": -2.0},
{"word": "worth", "start_ms": 140, "dur_ms": 110, "phase_lr_deg": -2.0},
{"word": "the", "start_ms": 260, "dur_ms": 40, "phase_lr_deg": -2.0},
{"word": "effort.", "start_ms": 310, "dur_ms": 266, "phase_lr_deg": -2.0}
]
},
{
"segment": 38,
"transcript": "준비해온 도시락을 먹을까요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "준비해온", "start_ms": 60, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "도시락을", "start_ms": 210, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "먹을까요?", "start_ms": 350, "dur_ms": 226, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "네, 산 위에서 먹는 밥은 정말 꿀맛이죠.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 45, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "산", "start_ms": 115, "dur_ms": 40, "phase_lr_deg": 5.0},
{"word": "위에서", "start_ms": 165, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "먹는", "start_ms": 285, "dur_ms": 70, "phase_lr_deg": 5.0},
{"word": "밥은", "start_ms": 365, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "정말", "start_ms": 455, "dur_ms": 70, "phase_lr_deg": 5.0},
{"word": "꿀맛이죠.", "start_ms": 535, "dur_ms": 41, "phase_lr_deg": 5.0}
]
},
{
"segment": 40,
"transcript": "Food tastes so much better up here.",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "Food", "start_ms": 35, "dur_ms": 70, "phase_lr_deg": -2.0},
{"word": "tastes", "start_ms": 115, "dur_ms": 90, "phase_lr_deg": -2.0},
{"word": "so", "start_ms": 215, "dur_ms": 50, "phase_lr_deg": -2.0},
{"word": "much", "start_ms": 275, "dur_ms": 80, "phase_lr_deg": -2.0},
{"word": "better", "start_ms": 365, "dur_ms": 110, "phase_lr_deg": -2.0},
{"word": "up", "start_ms": 485, "dur_ms": 40, "phase_lr_deg": -2.0},
{"word": "here.", "start_ms": 535, "dur_ms": 41, "phase_lr_deg": -2.0}
]
},
{
"segment": 41,
"transcript": "(noise)",
"speaker": "null",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 42,
"transcript": "이제 하산해볼까요? 무릎 조심하세요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "하산해볼까요?", "start_ms": 140, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "무릎", "start_ms": 290, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "조심하세요.", "start_ms": 380, "dur_ms": 196, "phase_lr_deg": 5.0}
]
},
{
"segment": 43,
"transcript": "내려가는 길이 더 위험할 수 있어요.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "내려가는", "start_ms": 65, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "길이", "start_ms": 205, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 295, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "위험할", "start_ms": 345, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 455, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "있어요.", "start_ms": 495, "dur_ms": 81, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "The descent can be more dangerous.",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "The", "start_ms": 40, "dur_ms": 30, "phase_lr_deg": -2.0},
{"word": "descent", "start_ms": 80, "dur_ms": 110, "phase_lr_deg": -2.0},
{"word": "can", "start_ms": 200, "dur_ms": 50, "phase_lr_deg": -2.0},
{"word": "be", "start_ms": 260, "dur_ms": 40, "phase_lr_deg": -2.0},
{"word": "more", "start_ms": 310, "dur_ms": 90, "phase_lr_deg": -2.0},
{"word": "dangerous.", "start_ms": 410, "dur_ms": 166, "phase_lr_deg": -2.0}
]
},
{
"segment": 45,
"transcript": "(music)",
"speaker": "null",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 46,
"transcript": "무사히 다 내려왔네요. 오늘 정말 즐거웠어요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "무사히", "start_ms": 70, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 200, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "내려왔네요.", "start_ms": 250, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 400, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 500, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "즐거웠어요.", "start_ms": 570, "dur_ms": 6, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "네, 우리 다음에 또 같이 등산해요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 50, "dur_ms": 50, "phase_lr_deg": 5.0},
{"word": "우리", "start_ms": 110, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "다음에", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "또", "start_ms": 290, "dur_ms": 40, "phase_lr_deg": 5.0},
{"word": "같이", "start_ms": 340, "dur_ms": 70, "phase_lr_deg": 5.0},
{"word": "등산해요.", "start_ms": 420, "dur_ms": 156, "phase_lr_deg": 5.0}
]
},
{
"segment": 48,
"transcript": "Let's go hiking together again next time.",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "Let's", "start_ms": 45, "dur_ms": 60, "phase_lr_deg": -2.0},
{"word": "go", "start_ms": 115, "dur_ms": 40, "phase_lr_deg": -2.0},
{"word": "hiking", "start_ms": 165, "dur_ms": 90, "phase_lr_deg": -2.0},
{"word": "together", "start_ms": 265, "dur_ms": 110, "phase_lr_deg": -2.0},
{"word": "again", "start_ms": 385, "dur_ms": 80, "phase_lr_deg": -2.0},
{"word": "next", "start_ms": 475, "dur_ms": 60, "phase_lr_deg": -2.0},
{"word": "time.", "start_ms": 545, "dur_ms": 31, "phase_lr_deg": -2.0}
]
},
{
"segment": 49,
"transcript": "좋습니다! 조심히 들어가세요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "좋습니다!", "start_ms": 60, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "조심히", "start_ms": 220, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "들어가세요.", "start_ms": 370, "dur_ms": 206, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "(noise)",
"speaker": "null",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 06:28:23*
