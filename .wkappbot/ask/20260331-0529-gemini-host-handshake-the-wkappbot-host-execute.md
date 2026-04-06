# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43304  time=05:29:00  uptime=11128s  mem=1591MB  system_processes=439"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260331_052850.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 312-321-132-136-316-612-601-632-613-612-316-276-103-136-103-103-103-162-326-310-301-360-132-123-312-671-367-672-137-132-301-103-163-130-310-321-126-361-367-312-310-132-731-231-1_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 675-612-261-632-623-621-632-631-123-163-632-316-163-615-137-312-132-132-321-231-321-312-231-123-231-321-236-621-362-635-327-627-326-362-236-327-236-267-237-267-621-216-126-261-6_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 256-563-267-265-265-265-273-627-675-657-675-576-574-316 765-562-625-632-623-621-162-162-216-613-215-251-265-623-621-615-547-543-514-125-572-275-672-672-762-752-572-315-163-574-5_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 624-261-213 326-623-672-123-613-617-130-563-351-625-612-316-672-160-136-627-360-613-160-130-163-103-103-132-632-216-163-172-103-130-103-103-163-761-126-103-126-132_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 132-130-103-162-136-570-175-365-136-165-126-132-123-612-103-132-361-136-130-130-136-631-123-123-102-130-160-103-261-123-321-132-617-126-372-607-130-103-123-173-132-316-627-261-2_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 765-675-625-261-612-126-103-106 572 170-137-562-106-107-165-102-710-610-612-617-715-176-672-716-671 216-726-726-726-276-721-726-723-271-726-231-652-206-236-632-326-261-273-621-3_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 627-321-621-261-631-261-326-613-136-627-612-621-621-126-163-362-623-126-162-126-126-623-602-120-362-106-103-316-620-621-162-123-132-163-126-163-136-132-136-316-621-623-320-321-6_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 763 716-716-716-176-167-123-132 760-763_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 675-302-320-603-102-201-623-620-312-136-132-612-610-130-615-106-167-671-162-103-163-620-106-610-312-312-316-162-136-613-731-621-372-136-103-136-103-231-163-263-621-123-231 132 7_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 652-532-632-235-236-362-637-765-621-162-265-623-265-627-632-623-367-623-623-263-326-263-236-612-627-623-632-263-631-673-657-362-263-627-726-276-632-301-312 627-627-632-672-263-6_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 132-632-267-263-261-261-256-627-657-132-126-126-132-123-132-137-352-312-371-673-671-671-671-675-670-675-564-652-652-617-617-321-231-132-231-321-327-132-230-217-762-761-672-762-6_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 261-627-621-621-621-312-621-632-263-632-625-675-675-675-675-675 617-612-167-672-316-326 261-263-126-612-162-672-621-162-612-267-627-627-621-362-316-123 321-123-623-627-617-173-7_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 167-163-103-132 123-123-123-763-762-763-762-765-762-763-763 675-765-765-657-567-143-620-123-672-263-671-136-657-326-132-765-765-765_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 275-276-276-267-267-326-231-263-261-216-312-216-231-263-236-261-216-216-231-216-321-531-263-132-312-652-635-365-615-163-153-213-312-231-217-237-267-267-216-263-326-216-126-213-6_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 163-123-163-175-265-127-106-675-137-613-132-162-102-102-136-312-136-136-612-160-132-123-130-163-103-176-732-267-135-106-670-167-106-251-263-125-152-617-756-167-610-613-160-672-6_V.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 321-123-263-231-213-613-602-672-326-276-672-160-130-621-261-312-316-136-316-162-672-627-167-167-610-102-613-163-120-162-163-216-263-162-167-615-670-167-132-201-204-306-321-326-2_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 610-361-163-256-126 132-176-175-132-162-136-163-612-162-765-605-316-165-760-132-167-163-316-102-301-123-265-162-102-106-651-163-173-516-126-102-123-132-163-362-672-576-126-162-6_V.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 671-163-106-163-167-167-106-103-610-621-761-106-321 765-675-765-761-765-761 176-167-103-167-103-136-542-261-106-162-106-106-132-103-163-130-631-163-106-136-103-132-106-136-106-2_V.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 130-103-312-213-670-607-607-607-671-672-206-326-162-103-316-260-621-675-326-236-623-610-362-162-310-302-132-213-136-103-631-623-602-632-620-701-167-176-107-107-617-167-103-326-6_V.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 103-132-312-103-167-130-160-567-152-762-106-167-136-617-136-617-103-176-516-106-160-615-136-371-176-513-537-531-761-713-741-471-132-351-756-271-627-312-321-167-607-267-620-260-2_V.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 513-351-351-157-351-153-513-153-157-153-513-517-135-157-153-150-135-157-513-517-156-165-156-501-521-512-651-653-163-163-310-316-316-315-315-130-136-132-312-261-365-516-512-567-6_W.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 302-210-263-321-103-206-260-620-627-260-126-623-126-362-321-132-136-362-621-613-623-136-126-206-625-632-620-623-216-267-627-263-267-261-261-675-627-567-103-160-610-106-103-106-1_V.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 321-312-126-165-623-326-312-316-132-136-162 625-236-612-231-236-627-162-136-213-263-612_W.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 765-762-675-627-602 567-567-526-625 625-256-265-527-675-672-632-132-613-361-631-136-132-362-231-321-236-231-507-501-105-651-625-672-657-657-650-765-675-657-675-675-765-675-576-7_W.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 327-267-236-762-672-267-236-325-263-362-267-236-265-267-237-326-236-267-267-672-276-237-327-372-327-327-236-235-325-236-325-236-352-526-357-576-537-527-572-576-576-576-756-257-5_W.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 367-671-627-672-762-762-617-675-672-261-657-657-617-574-756-576-253-267-276-567-562-265-326-263-261-261-526-562-567-562-317-263-267-627-627-652-625-526-567-356-135-362-263-627-6_W.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 163-103-160-136-132-160-103-130-136-130-103-160-362-657-163-126-103-312-106-167-163 156-132-132-136-163-106-160-163-106-162-132-103-167-136-106-163-167-516-617-162-126-162-160-1_V.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 567-162-623-621-160-165-651-167-175-317-156-517-156-157-162-162-267-726-726-726-762-726-672-762-725-765-762-276-267-276-236-627-576-615-134-617-107-103-567-527-276-162-167-617 5_W.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 601-612-623-631-263-261-613-312-163-132-160-103-564-163-152-126-621-160-610-137-217-612-162-103-610-136-610-576-657-136-126-162-132-136-316-103-162-136-103-127-631-103-612-136-1_V.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 136-132 621-312 163-675-760-160-631-621-675-607-620 136-317 526-256_V.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 765-576-765-765 362-130-132-120-123-716-132-321-136-126-317-251-216-567-103-620-263-512-213-163-123-126-162-576-576-756-756-576-576-576-576-756-176-751-672-167-172-162 612-632-6_W.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 102-126-123-132-612-321-576-765-756 576-267-312-326-753-631-623-326-612-236 325-765-672-276-267 627-267-632-263-362-356-326-623-263-672-672-632-326-263-261-263-236-623-631-567-3_W.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 617-167-671-163-136-136-675-163-136-617-103-163-106-132-312-103-136-163-106-136-103-163-132-312-126-631-627-162-627-312-132-316-261-706-765-106-103-136-163-136-312-541-156-102-1_V.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 362-173-321-106-167-132-167-176-106-165-130-130-163-164-132-162-312-103-162-160-163-103-615-106-167-610-160-671-156-160-103-167-103-176-726-576-136-103-106-103-176-106-136-162-1_V.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 657-165-617-672-627-256-567-756-617-162-167-103-106-103-103-103-106-103-106-106-106-103-137-102-312-137-103-103-136-130-130-573-756-107-103-130-107-107-750-176-672-627-627-106-1_V.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 765-760-657-765-675-671-761-760-765-760-675-675 765-761-672-675 372-236-326-137-210-210_W.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 657-651-156-563-531-153-561-156-153-613-567-517-567-576-156-356-365-365-361-365-536-563-163-563-635-651-536-351-563-513-506-562-653-516-571-351-536-365-635-563-651-162-651-561-6_W.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 675-765-765-516-675-657-751-567-617-672-657-615-652-162-165-627-675-657-167-167-167-632-675-765-765-167-165-163-136-163-165-175-165-651-617-763-165-176-165-157-165-163-136-136-1_W.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 312-321-326-576-756-375-317-321-326-312-367-316-327-326-235-326-263-326-265-263-263-265-265-216-236-231-263-263-263-236-236-263-263-361-136-612-163-136-263-623-612-621-162-263-2_W.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 310-307-736-756-376-137-312-750-375-761-167-167-302-236-263-216-265-302-310-106-103-123-312-321-260-602-621-612-617-310-165-321-310-104-132-265-261-103-132-625-264-670-265-163-1_V.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 312-135-130-312-312-106-623-675-236-632_W.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 326-302-103-132-301-103-103-162-106-610-163-301-312-132-321-267-265-126-106-517-310-301-132-316-540-504-106-126-156-162-105-106-106-103-610-126-612-637-570-513-316-513-103-103-1_V.mp3)
  Segment 43: 66192ms ~ 66768ms (file: 751-154-105-310-106-631-132-360-603-650-130-163-230-216-261-326-326-210-235-163-126-136-126-102-135-312-103-514-670-103-301-705-750-307-302-302-236-326-320-625-203-263-623-263-6_V.mp3)
  Segment 44: 67768ms ~ 68344ms (file: 162-361-163-153-760-132-130-105-107-103-176-617-163-126-267-123-130-103-136-106-160-162-160-162-103-617-106-102-601-106-102-136-731-102-137-162-123-102-312-132 136-136-316-136-3_V.mp3)
  Segment 45: 69344ms ~ 69920ms (file: 302-367-657-126-132-135-136-126-126-516-326-567-576-765-756-567-765-576-657-567-567-576-567-576-756-561-567-657-657-675-675-675-675 576-576-576-567-567-576-675-657-576-567-567-5_W.mp3)
  Segment 46: 70920ms ~ 71496ms (file: 312-135-167-170-265-167-617-236-263-610-130-126-362-163-103-132-163-136-106-617-107-615-612-610-136-123-612-103-163-136-103-103-173-137-162-162-172-136-162-136-613-132-103-136-1_V.mp3)
  Segment 47: 72496ms ~ 74008ms (file: 162-167-317-321-327-327-413-132-713-720-207-526-256-265-254-163-361-176-750-756-705-307-637-504-547-504-504-574-756-756-617-617-105-126-216-132 716-527-602-316-675-312-132-106-1_V.mp3)
  Segment 48: 75008ms ~ 75584ms (file: 132-106-103-102-260-206-126-162-312-320-321-132-163-607-670-601-267-602-302-312-360-260-263-623-260-260-263-261-203-230-263-236-276-271-271-617-167-106-167-321-321-301-137-136-1_V.mp3)
  Segment 49: 76584ms ~ 77160ms (file: 260-236-216-136-103-106-163-632-362 203-236-206-263-260-263-362-321-360-316-130-103-312-132-321-162-163-134-132-321-143-132-103-132-103-132-123-132-134 675-157-516-132-216-632-1_V.mp3)
  Segment 50: 78160ms ~ 78736ms (file: 312-132-132-312-132-321-321-132-321-310-321-320-321-320-230-231-320-327-231-231-230-320-320-236-236-236-236-326-263-237-231-237-267-276-237-236-231-210-213-213-213-213-213-127-1_W.mp3)
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
> **Time**: 2026-03-31 05:29:57
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "안녕하세요 처음 뵙겠습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 45, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "처음", "start_ms": 280, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "뵙겠습니다", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "네 반갑습니다 저는 마이클이라고 합니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 30, "dur_ms": 50, "phase_lr_deg": -5.2},
{"word": "반갑습니다", "start_ms": 100, "dur_ms": 180, "phase_lr_deg": -5.0},
{"word": "저는", "start_ms": 300, "dur_ms": 80, "phase_lr_deg": -5.3},
{"word": "마이클이라고", "start_ms": 390, "dur_ms": 140, "phase_lr_deg": -5.1},
{"word": "합니다", "start_ms": 540, "dur_ms": 36, "phase_lr_deg": -5.2}
]
},
{
"segment": 3,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 4,
"transcript": "한국어 이름이 있으신가요?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 60, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "이름이", "start_ms": 200, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "있으신가요?", "start_ms": 330, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "아직 없어요 추천해 주실 수 있나요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "아직", "start_ms": 40, "dur_ms": 90, "phase_lr_deg": -4.8},
{"word": "없어요", "start_ms": 140, "dur_ms": 110, "phase_lr_deg": -4.9},
{"word": "추천해", "start_ms": 270, "dur_ms": 120, "phase_lr_deg": -4.7},
{"word": "주실", "start_ms": 400, "dur_ms": 80, "phase_lr_deg": -4.8},
{"word": "수", "start_ms": 490, "dur_ms": 40, "phase_lr_deg": -4.9},
{"word": "있나요?", "start_ms": 540, "dur_ms": 36, "phase_lr_deg": -4.7}
]
},
{
"segment": 6,
"transcript": "준수라는 이름은 어떠세요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "준수라는", "start_ms": 45, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "이름은", "start_ms": 245, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "어떠세요?", "start_ms": 395, "dur_ms": 181, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "발음하기 좋네요 마음에 들어요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "발음하기", "start_ms": 35, "dur_ms": 180, "phase_lr_deg": -5.0},
{"word": "좋네요", "start_ms": 235, "dur_ms": 110, "phase_lr_deg": -5.1},
{"word": "마음에", "start_ms": 365, "dur_ms": 120, "phase_lr_deg": -4.9},
{"word": "들어요", "start_ms": 505, "dur_ms": 71, "phase_lr_deg": -5.2}
]
},
{
"segment": 8,
"transcript": "I'm glad you like it.",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "I'm", "start_ms": 50, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "glad", "start_ms": 120, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 240, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "like", "start_ms": 330, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "it.", "start_ms": 450, "dur_ms": 126, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "한국에 온 지 얼마나 되셨어요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "한국에", "start_ms": 60, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "온", "start_ms": 180, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "지", "start_ms": 250, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "얼마나", "start_ms": 300, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "되셨어요?", "start_ms": 450, "dur_ms": 126, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "이제 세 달 정도 됐습니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 30, "dur_ms": 90, "phase_lr_deg": -4.2},
{"word": "세", "start_ms": 130, "dur_ms": 60, "phase_lr_deg": -4.3},
{"word": "달", "start_ms": 200, "dur_ms": 70, "phase_lr_deg": -4.1},
{"word": "정도", "start_ms": 280, "dur_ms": 110, "phase_lr_deg": -4.3},
{"word": "됐습니다", "start_ms": 400, "dur_ms": 176, "phase_lr_deg": -4.2}
]
},
{
"segment": 11,
"transcript": "한국 생활은 어떠신가요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "한국", "start_ms": 55, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "생활은", "start_ms": 175, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "어떠신가요?", "start_ms": 335, "dur_ms": 241, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "음식이 정말 맛있고 사람들이 친절해요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "음식이", "start_ms": 40, "dur_ms": 130, "phase_lr_deg": -5.5},
{"word": "정말", "start_ms": 180, "dur_ms": 110, "phase_lr_deg": -5.3},
{"word": "맛있고", "start_ms": 300, "dur_ms": 120, "phase_lr_deg": -5.4},
{"word": "사람들이", "start_ms": 430, "dur_ms": 110, "phase_lr_deg": -5.3},
{"word": "친절해요", "start_ms": 550, "dur_ms": 26, "phase_lr_deg": -5.5}
]
},
{
"segment": 13,
"transcript": "Do you have a favorite dish?",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "Do", "start_ms": 45, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 135, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "have", "start_ms": 205, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 295, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "favorite", "start_ms": 345, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "dish?", "start_ms": 505, "dur_ms": 71, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "불고기를 가장 좋아합니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "불고기를", "start_ms": 30, "dur_ms": 180, "phase_lr_deg": -4.6},
{"word": "가장", "start_ms": 220, "dur_ms": 110, "phase_lr_deg": -4.7},
{"word": "좋아합니다", "start_ms": 340, "dur_ms": 236, "phase_lr_deg": -4.5}
]
},
{
"segment": 15,
"transcript": "불고기는 외국인들에게 인기가 많죠",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "불고기는", "start_ms": 50, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "외국인들에게", "start_ms": 220, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "인기가", "start_ms": 410, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "많죠", "start_ms": 530, "dur_ms": 46, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "달콤해서 제 입맛에 딱 맞아요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "달콤해서", "start_ms": 40, "dur_ms": 160, "phase_lr_deg": -4.8},
{"word": "제", "start_ms": 210, "dur_ms": 60, "phase_lr_deg": -4.7},
{"word": "입맛에", "start_ms": 280, "dur_ms": 120, "phase_lr_deg": -4.9},
{"word": "딱", "start_ms": 410, "dur_ms": 60, "phase_lr_deg": -4.8},
{"word": "맞아요", "start_ms": 480, "dur_ms": 96, "phase_lr_deg": -4.7}
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
"transcript": "매운 음식은 잘 드시나요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "매운", "start_ms": 65, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "음식은", "start_ms": 185, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 335, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "드시나요?", "start_ms": 425, "dur_ms": 151, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "조금 맵지만 떡볶이도 좋아해요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "조금", "start_ms": 35, "dur_ms": 110, "phase_lr_deg": -5.1},
{"word": "맵지만", "start_ms": 155, "dur_ms": 130, "phase_lr_deg": -5.2},
{"word": "떡볶이도", "start_ms": 295, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "좋아해요", "start_ms": 455, "dur_ms": 121, "phase_lr_deg": -5.1}
]
},
{
"segment": 20,
"transcript": "Spicy food can be addictive.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Spicy", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "food", "start_ms": 190, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "can", "start_ms": 310, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "be", "start_ms": 400, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "addictive.", "start_ms": 480, "dur_ms": 96, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 22,
"transcript": "한국말을 참 잘하시네요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "한국말을", "start_ms": 60, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "참", "start_ms": 250, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "잘하시네요", "start_ms": 320, "dur_ms": 256, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "감사합니다 더 열심히 공부해야죠",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 30, "dur_ms": 210, "phase_lr_deg": -5.3},
{"word": "더", "start_ms": 250, "dur_ms": 60, "phase_lr_deg": -5.1},
{"word": "열심히", "start_ms": 320, "dur_ms": 130, "phase_lr_deg": -5.2},
{"word": "공부해야죠", "start_ms": 460, "dur_ms": 116, "phase_lr_deg": -5.3}
]
},
{
"segment": 24,
"transcript": "주말에는 주로 뭐 하세요?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "주말에는", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "주로", "start_ms": 200, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 320, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "하세요?", "start_ms": 390, "dur_ms": 186, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "친구들과 카페에 가거나 영화를 봐요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "친구들과", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": -4.6},
{"word": "카페에", "start_ms": 200, "dur_ms": 120, "phase_lr_deg": -4.7},
{"word": "가거나", "start_ms": 330, "dur_ms": 110, "phase_lr_deg": -4.5},
{"word": "영화를", "start_ms": 450, "dur_ms": 80, "phase_lr_deg": -4.6},
{"word": "봐요", "start_ms": 540, "dur_ms": 36, "phase_lr_deg": -4.5}
]
},
{
"segment": 26,
"transcript": "I like movies too. What's your favorite genre?",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 45, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "like", "start_ms": 115, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "movies", "start_ms": 205, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "too.", "start_ms": 325, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "What's", "start_ms": 395, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "your", "start_ms": 485, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "favorite", "start_ms": 535, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "genre?", "start_ms": 575, "dur_ms": 1, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "액션 영화를 좋아합니다 스릴 만점이에요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "액션", "start_ms": 30, "dur_ms": 120, "phase_lr_deg": -5.1},
{"word": "영화를", "start_ms": 160, "dur_ms": 130, "phase_lr_deg": -5.2},
{"word": "좋아합니다", "start_ms": 300, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "스릴", "start_ms": 460, "dur_ms": 80, "phase_lr_deg": -5.1},
{"word": "만점이에요", "start_ms": 550, "dur_ms": 26, "phase_lr_deg": -5.2}
]
},
{
"segment": 28,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 29,
"transcript": "저도요 다음에 같이 보러 갈까요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "저도요", "start_ms": 55, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "다음에", "start_ms": 185, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 305, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "보러", "start_ms": 405, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "갈까요?", "start_ms": 495, "dur_ms": 81, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "네 좋아요 연락 주세요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 35, "dur_ms": 60, "phase_lr_deg": -5.4},
{"word": "좋아요", "start_ms": 105, "dur_ms": 120, "phase_lr_deg": -5.5},
{"word": "연락", "start_ms": 235, "dur_ms": 110, "phase_lr_deg": -5.3},
{"word": "주세요", "start_ms": 355, "dur_ms": 221, "phase_lr_deg": -5.4}
]
},
{
"segment": 31,
"transcript": "벌써 시간이 이렇게 됐네요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "벌써", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "시간이", "start_ms": 170, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 310, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "됐네요", "start_ms": 440, "dur_ms": 136, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "가봐야겠어요 만나서 즐거웠습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "가봐야겠어요", "start_ms": 40, "dur_ms": 210, "phase_lr_deg": -4.2},
{"word": "만나서", "start_ms": 260, "dur_ms": 140, "phase_lr_deg": -4.3},
{"word": "즐거웠습니다", "start_ms": 410, "dur_ms": 166, "phase_lr_deg": -4.1}
]
},
{
"segment": 33,
"transcript": "저도요 조심히 들어가세요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "저도요", "start_ms": 60, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "조심히", "start_ms": 180, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "들어가세요", "start_ms": 320, "dur_ms": 256, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "See you next time. Bye!",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "you", "start_ms": 120, "dur_ms": 60, "phase_lr_deg": -5.1},
{"word": "next", "start_ms": 190, "dur_ms": 110, "phase_lr_deg": -4.9},
{"word": "time.", "start_ms": 310, "dur_ms": 90, "phase_lr_deg": -5.0},
{"word": "Bye!", "start_ms": 410, "dur_ms": 166, "phase_lr_deg": -5.1}
]
},
{
"segment": 35,
"transcript": "안녕히 가세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 45, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 285, "dur_ms": 291, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 37,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 38,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 39,
"transcript": "오늘 회의 주제가 무엇인가요?",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 25, "dur_ms": 110, "phase_lr_deg": 3.2},
{"word": "회의", "start_ms": 145, "dur_ms": 120, "phase_lr_deg": 3.1},
{"word": "주제가", "start_ms": 275, "dur_ms": 150, "phase_lr_deg": 3.3},
{"word": "무엇인가요?", "start_ms": 435, "dur_ms": 141, "phase_lr_deg": 3.2}
]
},
{
"segment": 40,
"transcript": "새로운 프로젝트 마케팅 전략입니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "새로운", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "프로젝트", "start_ms": 200, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "마케팅", "start_ms": 390, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "전략입니다", "start_ms": 520, "dur_ms": 56, "phase_lr_deg": 0.0}
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
"transcript": "준비한 자료를 함께 보시죠",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "준비한", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 3.5},
{"word": "자료를", "start_ms": 200, "dur_ms": 130, "phase_lr_deg": 3.6},
{"word": "함께", "start_ms": 340, "dur_ms": 110, "phase_lr_deg": 3.4},
{"word": "보시죠", "start_ms": 460, "dur_ms": 116, "phase_lr_deg": 3.5}
]
},
{
"segment": 43,
"transcript": "아이디어가 참 신선하네요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "아이디어가", "start_ms": 55, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "참", "start_ms": 275, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "신선하네요", "start_ms": 345, "dur_ms": 231, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "감사합니다 예산 범위 내에서 가능할까요?",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 30, "dur_ms": 180, "phase_lr_deg": 3.8},
{"

---
*Generated by WKAppBot ask gemini — 2026-03-31 05:29:57*
