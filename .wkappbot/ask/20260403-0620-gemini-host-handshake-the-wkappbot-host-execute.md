# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=06:19:02  uptime=17988s  mem=1620MB  system_processes=447"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_061853.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 236-206-126-103-160-673-607-312-165-103-261-623-261-126-261-106-120-102-103-107-107-103-103-302-230-302-203-230-203-265-265-126-657-231-520-502-263-230-253-157-162-150-105-103-1_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 670-675-162-165-651-160-675-675-672-675-675-675-637-612-631-615-627-675-762-672-625-635-652-657-672-620-261-612-162-627-263-326-136-673-267-617-670-673-312-625-216-602-612-263-2_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 132-163-120-132-123-312 206-620-132-103-263-613-130-163-160-162-103-136-106-136-162-126-136-132-276-162-163-765-607-163-612-613-102-672-623-123-136-627-672-135-315-625-612-361-6_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 136-376-317-376-367-276-732-527-576-567-526-265-265-265-267-256-652-562-567-562-625-326-236-265-356-326-236-265-263-321-325-253-253-526-526-562-526-526-256-615-125-126-512-257-2_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 342-354 623-263-632-627-627-627-620-602-261-241-134-136-132-471-241-413-135-413-136-132-237-214-132-127-132-125-123-134-513-132-753-213-321-125-341-451-341-213-724-314-271-254-2_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 361-261-126-623-675-632-136-126-612-657-657-653-612-163-136-623-627-623-621-162-216-321-652-632-627-627-263-236-263-263-236-362-326-326-623 123-132-213-263-326-321 236-263-236-2_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 175-150-107-267-602-206-267-621-106-120-263-316-705-632-106-106-165-162-162-162-106-162-716-362-630-130-306-310-302-163-103-136-312-103-136-531-510-320-320-130-301-361-301-623-3_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 260-326-306-302-301-317-206-260-620-654-156-216-576-275-176-167-607-102-213-167-103-316-613-316-102-102-162-621-126-613-123-162-173-174-103-713-621-316-301-132-167-631-106-175-7_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 167-165-716-165-361-617-571-176-157-167-761-671-163-765-136-130-136-106-167-172-162-360-231-162-613-165-721-615-761-167-126-165-613-137-130-165-617-103-165-106-176-126-176-176-1_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 263-206-623-263-276-620-573-307-301-132-103-320-302-276-261-123-610-612-302-321-312-175-571-751-105-756-312-325-573-327-321-352-301-637-120-231-201-276-207-236-326-362-310-320-3_V.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 263-261-621-261-261-267-627-267-261-267-627-621-620-261-260-261-326-236-632-276-267-263-236-261-231-263-263-236-263-267-627-627-261-263-623-261-216-213-132-260-624-260-261-263-6_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 572-106-167-106-623-126-162-765-760-276-726-362-760-675-675-672-567-567-765-732-375-731-675-607-167-762-176-136-132-756-167-136-362-312-731-713-765-716-173-613-715-756-174-713-7_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 176-163-160-163-160-163-103-136-132 123 261-617_V.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 631-617-630-106-163-103-316-375-756-506-750-576-526-576-167-162-176-241-605-206-261-102-312-136-135-316-351-301-163-103-130-130-103-103-167-103-567-510-231-130-130-316-102-673-1_V.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 765-760-730-762-306-362-632-163-162-136-163-136-136-621-632-206-236-267-326-136-316-132-132-612-602-627-143-136-127-132-143-765-672-675-310-163-136-613-607-267-263-627-627-672-6_V.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 576-576 756 756 675-613-670-607-623-602-672-627-621-623-760-671-132-301-762-623-632-132-361-126-310-312-132-132-615-376-736-736-763-376-163-706-136 312_V.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 627-361-365-162-126-267-632-627-631-672_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 675-627-312-213-301-320-602-601-610-320-203-206-320-627-260-206-160-601-612-617-671-617-617-617-674-671-167-613-631-301-362-326-236-620-236-623-206-320-260-230-236-236-376-673-7_W.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 756-576-756-567-756-576-576-675-675-576-576-576-576-567-576-576-576-576-576-756-675-576-765-765-675-567-756-574-576-576-756-756-576-576-576-675-756-754-576-756-267-267-752-756-2_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 103-163-512-621-127-126-103-631-126-176-623-612-103-756-736-175-162-130-132-102-361-613-316-621-671-516-625-103-103-126-153-165-123-623-617-160-213-126-175-132-102-163-163-106-1_V.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 132-123-103-236-321-312-321-327-371-361-160-261-567-567-261-632-672-367-362-620-316-603-362-670-675-675-675-652-657-657-675-321-362-632-673-367-672-267-726-321-312-132-316-132-1_W.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 167-136-106-163-567-632-516-162-162-136-160-601-601-632-612-612-106-103-165-627 106-163-136-132-106-136-765 613 137-263-623-103-163_V.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 132-160-516-132-103-657-356-176-106-172-167-761-167 156 276-126-601-175-136-103-162-163-623-621-216-167-637-106-167-615-263-136-132-105-156-103-163-164-571-152-623 103-165-126-1_V.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 123-126-231 621-312-627-216-126-136-312-261-621-623-621-623-623-621-621 126-132-123_W.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 675-765-765 710-701-706-765-765-760-706-765-675-362-132-756-765-765-741 571-571-705-570-617-671-637-236-527-527-572-357-136-165-163-613-716-706-765-706-701-760-760-716-760-765-7_W.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 673 756-652-167-176-612-126-123-137-127-316-137-136_W.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 652-607 765-765-650-635-765-576-675-762-765-761-752-765-765 267-236-756-176-261-671-261-312 576-570 756-756-765-765-756-765-751-610-617-613-765-672-762-675-765-750-765-756-756-7_W.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 103-167-106-610-671-670-176-627-103-561-615-165-162-621-602-162-160-106-160-201-602-672-675-603-106-261-620-620-620-620-602-672-602-621-602-176-675-621-216-201-130-521-570-103-1_W.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 571-751-753-573-756-567-756-175-107-276-312-103-361-165-617-162-163-127-107-103-103-156-136-327-216-126-326-671-310-163-617-103-132-130-103-132-371-106-125-103-512-657-173-162-1_V.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 312-514-234-713-157-713-614-612-654_V.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 574-725-762-723-547-572-235-327-327-263-543-574-537-275-653-675-547-542-257-256-265-265-265-762-726-265-625-765-765-763-637-654-574-567-652-762-765-253-256-265-263-267-726-236-2_W.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 765-765-756-576-165-126-574-765-576 675-623-267-621-267-263-236-657-765-675-672-162-617-756-567-756-567-576-756-576-576-517-567-567-576-651-576-627-567-756-657-675-675-756-756-5_W.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 712-761-176-712-257-157-517-512-561-751-257-527-267-326-263-123-127-123-157-135-107-156-176-106-167-617-103-517-106-671-613-126-617-267-263-267-103-136-136-163-162-326-617-763-1_V.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 213-320-260-206-120-103-162-162-706-762-671-671-176-167-126-167-176-127-217-301-230-206-261-306-261-132-326-320-206-203-216-521-752-231-571-317-106-316-103-312-230-321-302-310-1_V.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 126 123-105-576-675-756-752-756-756-765-765-170-217-267-312-276-216-213-276-276-120-213-103-210-163-627-107 163-103-136-156-635-103-165-162-162-361-672-361-162-176-103-167-160-1_V.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 612-612-621-261-321-162-632-617-160-675-231-172-102-316-623-216-103-316-653-235-631-672-672-657-576-675-756-260-675-627-637-762-206-317-627-672-602-610-607-612-261-261-612-261-2_W.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 312-132-216 362-267-263-236-623 236-231-326-261-326-362-132-130 625-625-627-627-627 126-612-120-102-620-620-132-756-327-326-263-231-326-267-627-261-261-632-360-623-206-236-260-2_W.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 126-132-321-236-132-163-162-136-601-671-632-236-326-670-672-632-637-762-723-276-276-260-132-136-123-136-627-461-132-301-321-732-576-510-175-176-106-167-301-302-327-302-307-701-1_V.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 130-657-657-657-623-126-725-231-756-753 613-617-650-607-672-672-765-765-725-627-627-607-167-160-106-167-106-132-310-130-103-103-102-130-706-167-162-165-607-607-612-167-162-123-1_W.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 265-263-765-765-765-207-172-521-571-756-756-267-632-507-531-351-135-310-312-371-327-206-263-236-327-326-620-103-126-514-524-521-621-621-602-621-631-136-506-105-106-120-120-102-1_V.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 675-765-762-267-206-260-627-260-620-620-260 167-162-106-103-301-106-572-547-756-756-705-765-671-103-107-103-103-137-231-675-602-102-163-106-216-126-710-106-167-136-163-450-130-3_V.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 267-632-276-362-263-637-326-362-271-237-237-237-327-237-237-273-237-237-237-273-237-273-372-370-372-327-327-573-537-315-357-527-527-521-572-157-573-754-715-720-132-162-637-261-2_W.mp3)
  Segment 43: 66192ms ~ 66768ms (file: 126-613-162-310-612-612-673-673-637-621-126-672-671-671-673-672-163-673-673-671-617-672-761-672-765-672-326-361-675-673-637-673-670-607-673-671-672-607-106-607-167-627-126-316-1_W.mp3)
  Segment 44: 67768ms ~ 68344ms (file: 672-261-621-162-612-672-627-326-163-123-173-627-267-327-263-672-627-762-672-673-672-267-267-265-765-275-576-263-276-126-623-627-652-657-625-627-263-263-362-276-672-632-627-213-2_W.mp3)
  Segment 45: 69344ms ~ 69920ms (file: 361-631-163-102-260-275-576-176-261-326-623-326-316-126-362-612-361-631-267-106-312-302-573-543-513-574-756-765-716-617-167-106-103-576-617-167-106-106-103 756 125-361 263-316 7_V.mp3)
  Segment 46: 70920ms ~ 71496ms (file: 132-130-310-130-312-301-312-126-165-672-106-567-167-106-167-136-162-132-263-276-123_V.mp3)
  Segment 47: 72496ms ~ 73072ms (file: 312-132-132-123-321-321-253-573-572-136-351-375-317-317-316-316-137-125-512-532-356-235-326-312-326-316-371-173-762-321-326-321-327-321-325-375-253-527-567-572-235-527-572-235-3_W.mp3)
  Segment 48: 74072ms ~ 74648ms (file: 106-613-617-613-612-617-167-162-206-160-750-576-576-176-102-123-103-172-132-316-301-136-137-316-312-312-361-163-516-547-510-517-106-106-216-601-136-172-132-103-316-136-756-156-7_V.mp3)
  Segment 49: 75648ms ~ 76224ms (file: 765-632-165-137-162-127-625-163-137-516-532-156 316-136-103-126-317-136-675-672-106-206-312-623-132-630-713-260-106-312-103-162-576-152-617-671-617-623-657-756-765-172-651-213-2_V.mp3)
  Segment 50: 77224ms ~ 79168ms (file: 163-134-126-132-132-137-132-136-132-123-162-132-126-123-127-135-137-672_V.mp3)
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
> **Time**: 2026-04-03 06:20:27
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "비가 오니까 파전이 생각나네요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "비가", "start_ms": 60, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "오니까", "start_ms": 180, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "파전이", "start_ms": 320, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "생각나네요.", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "막걸리도 한 잔 곁들이면 최고겠죠.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "막걸리도", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "한", "start_ms": 200, "dur_ms": 50, "phase_lr_deg": 5.0},
{"word": "잔", "start_ms": 260, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "곁들이면", "start_ms": 330, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "최고겠죠.", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 5.0}
]
},
{
"segment": 3,
"transcript": "퇴근하고 파전집에서 만날까요?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "퇴근하고", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "파전집에서", "start_ms": 200, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "만날까요?", "start_ms": 390, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "Pajeon and Makgeolli sound perfect for a rainy day.",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Pajeon", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": -2.0},
{"word": "and", "start_ms": 120, "dur_ms": 40, "phase_lr_deg": -2.0},
{"word": "Makgeolli", "start_ms": 170, "dur_ms": 100, "phase_lr_deg": -2.0},
{"word": "sound", "start_ms": 280, "dur_ms": 70, "phase_lr_deg": -2.0},
{"word": "perfect", "start_ms": 360, "dur_ms": 90, "phase_lr_deg": -2.0},
{"word": "for", "start_ms": 460, "dur_ms": 30, "phase_lr_deg": -2.0},
{"word": "a", "start_ms": 500, "dur_ms": 20, "phase_lr_deg": -2.0},
{"word": "rainy", "start_ms": 530, "dur_ms": 40, "phase_lr_deg": -2.0},
{"word": "day.", "start_ms": 570, "dur_ms": 6, "phase_lr_deg": -2.0}
]
},
{
"segment": 5,
"transcript": "해물파전이 맛있는 곳을 알고 있어요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "해물파전이", "start_ms": 70, "dur_ms": 160, "phase_lr_deg": 5.0},
{"word": "맛있는", "start_ms": 240, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "곳을", "start_ms": 370, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "알고", "start_ms": 460, "dur_ms": 70, "phase_lr_deg": 5.0},
{"word": "있어요.", "start_ms": 540, "dur_ms": 36, "phase_lr_deg": 5.0}
]
},
{
"segment": 6,
"transcript": "거기 김치전도 정말 바삭하고 맛있어요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "거기", "start_ms": 55, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "김치전도", "start_ms": 145, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 285, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "바삭하고", "start_ms": 385, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "맛있어요.", "start_ms": 505, "dur_ms": 71, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "Let's try both seafood and kimchi pancakes.",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "Let's", "start_ms": 45, "dur_ms": 60, "phase_lr_deg": -2.0},
{"word": "try", "start_ms": 115, "dur_ms": 50, "phase_lr_deg": -2.0},
{"word": "both", "start_ms": 175, "dur_ms": 60, "phase_lr_deg": -2.0},
{"word": "seafood", "start_ms": 245, "dur_ms": 100, "phase_lr_deg": -2.0},
{"word": "and", "start_ms": 355, "dur_ms": 40, "phase_lr_deg": -2.0},
{"word": "kimchi", "start_ms": 405, "dur_ms": 80, "phase_lr_deg": -2.0},
{"word": "pancakes.", "start_ms": 495, "dur_ms": 81, "phase_lr_deg": -2.0}
]
},
{
"segment": 8,
"transcript": "좋아요. 빨리 퇴근하고 싶네요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "좋아요.", "start_ms": 60, "dur_ms": 90, "phase_lr_deg": 5.0},
{"word": "빨리", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "퇴근하고", "start_ms": 250, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "싶네요.", "start_ms": 380, "dur_ms": 196, "phase_lr_deg": 5.0}
]
},
{
"segment": 9,
"transcript": "여섯 시 정각에 로비에서 봐요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "여섯", "start_ms": 50, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "시", "start_ms": 150, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "정각에", "start_ms": 200, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "로비에서", "start_ms": 320, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "봐요.", "start_ms": 460, "dur_ms": 116, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "See you at the lobby at six.",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 40, "dur_ms": 50, "phase_lr_deg": -2.0},
{"word": "you", "start_ms": 100, "dur_ms": 40, "phase_lr_deg": -2.0},
{"word": "at", "start_ms": 150, "dur_ms": 30, "phase_lr_deg": -2.0},
{"word": "the", "start_ms": 190, "dur_ms": 30, "phase_lr_deg": -2.0},
{"word": "lobby", "start_ms": 230, "dur_ms": 90, "phase_lr_deg": -2.0},
{"word": "at", "start_ms": 330, "dur_ms": 30, "phase_lr_deg": -2.0},
{"word": "six.", "start_ms": 370, "dur_ms": 206, "phase_lr_deg": -2.0}
]
},
{
"segment": 11,
"transcript": "알겠습니다. 늦지 않게 갈게요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "알겠습니다.", "start_ms": 65, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "늦지", "start_ms": 205, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "않게", "start_ms": 295, "dur_ms": 70, "phase_lr_deg": 5.0},
{"word": "갈게요.", "start_ms": 375, "dur_ms": 201, "phase_lr_deg": 5.0}
]
},
{
"segment": 12,
"transcript": "비가 제법 많이 오네요. 우산 챙기셨죠?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "비가", "start_ms": 55, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "제법", "start_ms": 145, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 235, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "오네요.", "start_ms": 325, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "우산", "start_ms": 425, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "챙기셨죠?", "start_ms": 505, "dur_ms": 71, "phase_lr_deg": 0.0}
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
"transcript": "네, 아침에 챙겨왔어요. 다행이에요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 45, "dur_ms": 50, "phase_lr_deg": 5.0},
{"word": "아침에", "start_ms": 105, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "챙겨왔어요.", "start_ms": 215, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "다행이에요.", "start_ms": 375, "dur_ms": 201, "phase_lr_deg": 5.0}
]
},
{
"segment": 15,
"transcript": "It's raining pretty hard. Glad I brought my umbrella.",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "It's", "start_ms": 35, "dur_ms": 40, "phase_lr_deg": -2.0},
{"word": "raining", "start_ms": 85, "dur_ms": 90, "phase_lr_deg": -2.0},
{"word": "pretty", "start_ms": 185, "dur_ms": 70, "phase_lr_deg": -2.0},
{"word": "hard.", "start_ms": 265, "dur_ms": 70, "phase_lr_deg": -2.0},
{"word": "Glad", "start_ms": 345, "dur_ms": 60, "phase_lr_deg": -2.0},
{"word": "I", "start_ms": 415, "dur_ms": 30, "phase_lr_deg": -2.0},
{"word": "brought", "start_ms": 455, "dur_ms": 70, "phase_lr_deg": -2.0},
{"word": "my", "start_ms": 535, "dur_ms": 30, "phase_lr_deg": -2.0},
{"word": "umbrella.", "start_ms": 570, "dur_ms": 6, "phase_lr_deg": -2.0}
]
},
{
"segment": 16,
"transcript": "조심히 오세요. 이따 봬요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "조심히", "start_ms": 70, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "오세요.", "start_ms": 210, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "이따", "start_ms": 330, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "봬요.", "start_ms": 430, "dur_ms": 146, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "(noise)",
"speaker": "null",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 18,
"transcript": "벌써 퇴근 시간이네요. 정리하고 일어날까요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "벌써", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "퇴근", "start_ms": 140, "dur_ms": 70, "phase_lr_deg": 5.0},
{"word": "시간이네요.", "start_ms": 220, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "정리하고", "start_ms": 340, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "일어날까요?", "start_ms": 470, "dur_ms": 106, "phase_lr_deg": 5.0}
]
},
{
"segment": 19,
"transcript": "(music)",
"speaker": "null",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 20,
"transcript": "네, 저도 다 끝났어요. 갑시다!",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 60, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "저도", "start_ms": 120, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 200, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "끝났어요.", "start_ms": 240, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "갑시다!", "start_ms": 350, "dur_ms": 170, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "It's finally six o'clock. Let's head out.",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "It's", "start_ms": 45, "dur_ms": 40, "phase_lr_deg": -2.0},
{"word": "finally", "start_ms": 95, "dur_ms": 80, "phase_lr_deg": -2.0},
{"word": "six", "start_ms": 185, "dur_ms": 60, "phase_lr_deg": -2.0},
{"word": "o'clock.", "start_ms": 255, "dur_ms": 90, "phase_lr_deg": -2.0},
{"word": "Let's", "start_ms": 355, "dur_ms": 60, "phase_lr_deg": -2.0},
{"word": "head", "start_ms": 425, "dur_ms": 50, "phase_lr_deg": -2.0},
{"word": "out.", "start_ms": 485, "dur_ms": 91, "phase_lr_deg": -2.0}
]
},
{
"segment": 22,
"transcript": "빗소리가 참 좋네요. 운치 있어요.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "빗소리가", "start_ms": 55, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "참", "start_ms": 195, "dur_ms": 50, "phase_lr_deg": 5.0},
{"word": "좋네요.", "start_ms": 255, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "운치", "start_ms": 365, "dur_ms": 70, "phase_lr_deg": 5.0},
{"word": "있어요.", "start_ms": 445, "dur_ms": 131, "phase_lr_deg": 5.0}
]
},
{
"segment": 23,
"transcript": "이런 날 파전에 막걸리라니, 완벽한 계획이에요.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "이런", "start_ms": 40, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "날", "start_ms": 120, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "파전에", "start_ms": 170, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "막걸리라니,", "start_ms": 280, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "완벽한", "start_ms": 420, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "계획이에요.", "start_ms": 520, "dur_ms": 56, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "I love the sound of rain. Perfect for pajeon.",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 30, "dur_ms": 30, "phase_lr_deg": -2.0},
{"word": "love", "start_ms": 70, "dur_ms": 60, "phase_lr_deg": -2.0},
{"word": "the", "start_ms": 140, "dur_ms": 30, "phase_lr_deg": -2.0},
{"word": "sound", "start_ms": 180, "dur_ms": 70, "phase_lr_deg": -2.0},
{"word": "of", "start_ms": 260, "dur_ms": 30, "phase_lr_deg": -2.0},
{"word": "rain.", "start_ms": 300, "dur_ms": 70, "phase_lr_deg": -2.0},
{"word": "Perfect", "start_ms": 380, "dur_ms": 90, "phase_lr_deg": -2.0},
{"word": "for", "start_ms": 480, "dur_ms": 40, "phase_lr_deg": -2.0},
{"word": "pajeon.", "start_ms": 530, "dur_ms": 46, "phase_lr_deg": -2.0}
]
},
{
"segment": 25,
"transcript": "빨리 가서 자리 잡아야겠어요. 사람 많을 텐데.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "빨리", "start_ms": 65, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "가서", "start_ms": 155, "dur_ms": 70, "phase_lr_deg": 5.0},
{"word": "자리", "start_ms": 235, "dur_ms": 70, "phase_lr_deg": 5.0},
{"word": "잡아야겠어요.", "start_ms": 315, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "사람", "start_ms": 455, "dur_ms": 70, "phase_lr_deg": 5.0},
{"word": "많을", "start_ms": 535, "dur_ms": 30, "phase_lr_deg": 5.0},
{"word": "텐데.", "start_ms": 570, "dur_ms": 6, "phase_lr_deg": 5.0}
]
},
{
"segment": 26,
"transcript": "(noise)",
"speaker": "null",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 27,
"transcript": "도착했어요! 다행히 자리가 하나 있네요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "도착했어요!", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "다행히", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "자리가", "start_ms": 310, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "하나", "start_ms": 430, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "있네요.", "start_ms": 520, "dur_ms": 56, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "우와, 고소한 냄새가 진동을 하네요.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "우와,", "start_ms": 60, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "고소한", "start_ms": 130, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "냄새가", "start_ms": 240, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "진동을", "start_ms": 360, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "하네요.", "start_ms": 470, "dur_ms": 106, "phase_lr_deg": 5.0}
]
},
{
"segment": 29,
"transcript": "It smells amazing in here. I'm so hungry.",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "It", "start_ms": 45, "dur_ms": 30, "phase_lr_deg": -2.0},
{"word": "smells", "start_ms": 85, "dur_ms": 90, "phase_lr_deg": -2.0},
{"word": "amazing", "start_ms": 185, "dur_ms": 100, "phase_lr_deg": -2.0},
{"word": "in", "start_ms": 295, "dur_ms": 30, "phase_lr_deg": -2.0},
{"word": "here.", "start_ms": 335, "dur_ms": 60, "phase_lr_deg": -2.0},
{"word": "I'm", "start_ms": 405, "dur_ms": 40, "phase_lr_deg": -2.0},
{"word": "so", "start_ms": 455, "dur_ms": 40, "phase_lr_deg": -2.0},
{"word": "hungry.", "start_ms": 505, "dur_ms": 71, "phase_lr_deg": -2.0}
]
},
{
"segment": 30,
"transcript": "이모님! 여기 해물파전 하나랑 지평 막걸리 주세요.",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "이모님!", "start_ms": 55, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "여기", "start_ms": 185, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "해물파전", "start_ms": 265, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "하나랑", "start_ms": 405, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "지평", "start_ms": 515, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "막걸리", "start_ms": 570, "dur_ms": 6, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "(noise)",
"speaker": "null",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 32,
"transcript": "막걸리부터 나왔네요. 자, 먼저 한 잔씩 합시다.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "막걸리부터", "start_ms": 60, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "나왔네요.", "start_ms": 210, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "자,", "start_ms": 330, "dur_ms": 40, "phase_lr_deg": 5.0},
{"word": "먼저", "start_ms": 380, "dur_ms": 70, "phase_lr_deg": 5.0},
{"word": "한", "start_ms": 460, "dur_ms": 30, "phase_lr_deg": 5.0},
{"word": "잔씩", "start_ms": 500, "dur_ms": 70, "phase_lr_deg": 5.0},
{"word": "합시다.", "start_ms": 576, "dur_ms": 0, "phase_lr_deg": 5.0}
]
},
{
"segment": 33,
"transcript": "짠! 고생 많으셨어요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 75,
"audio_type": "speech",
"words": [
{"word": "짠!", "start_ms": 75, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "고생", "start_ms": 145, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "많으셨어요.", "start_ms": 235, "dur_ms": 341, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "Cheers! Good job today everyone.",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Cheers!", "start_ms": 50, "dur_ms": 90, "phase_lr_deg": -2.0},
{"word": "Good", "start_ms": 150, "dur_ms": 70, "phase_lr_deg": -2.0},
{"word": "job", "start_ms": 230, "dur_ms": 60, "phase_lr_deg": -2.0},
{"word": "today", "start_ms": 300, "dur_ms": 80, "phase_lr_deg": -2.0},
{"word": "everyone.", "start_ms": 390, "dur_ms": 186, "phase_lr_deg": -2.0}
]
},
{
"segment": 35,
"transcript": "캬, 시원하네요! 이 맛에 퇴근하죠.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "캬,", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "시원하네요!", "start_ms": 110, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "이", "start_ms": 270, "dur_ms": 40, "phase_lr_deg": 5.0},
{"word": "맛에", "start_ms": 320, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "퇴근하죠.", "start_ms": 410, "dur_ms": 166, "phase_lr_deg": 5.0}
]
},
{
"segment": 36,
"transcript": "파전 나왔습니다! 와, 해물이 진짜 듬뿍 들어갔네요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "파전", "start_ms": 60, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "나왔습니다!", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "와,", "start_ms": 280, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "해물이", "start_ms": 340, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 430, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "듬뿍", "start_ms": 500, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "들어갔네요.", "start_ms": 570, "dur_ms": 6, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "Look at all that seafood! It looks so crispy.",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "Look", "start_ms": 45, "dur_ms": 60, "phase_lr_deg": -2.0},
{"word": "at", "start_ms": 115, "dur_ms": 30, "phase_lr_deg": -2.0},
{"word": "all", "start_ms": 155, "dur_ms": 40, "phase_lr_deg": -2.0},
{"word": "that", "start_ms": 205, "dur_ms": 50, "phase_lr_deg": -2.0},
{"word": "seafood!", "start_ms": 265, "dur_ms": 100, "phase_lr_deg": -2.0},
{"word": "It", "start_ms": 375, "dur_ms": 30, "phase_lr_deg": -2.0},
{"word": "looks", "start_ms": 415, "dur_ms": 70, "phase_lr_deg": -2.0},
{"word": "so", "start_ms": 495, "dur_ms": 40, "phase_lr_deg": -2.0},
{"word": "crispy.", "start_ms": 545, "dur_ms": 31, "phase_lr_deg": -2.0}
]
},
{
"segment": 38,
"transcript": "양념장에 콕 찍어서 드셔보세요. 환상적이에요.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "양념장에", "start_ms": 55, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "콕", "start_ms": 195, "dur_ms": 40, "phase_lr_deg": 5.0},
{"word": "찍어서", "start_ms": 245, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "드셔보세요.", "start_ms": 355, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "환상적이에요.", "start_ms": 485, "dur_ms": 91, "phase_lr_deg": 5.0}
]
},
{
"segment": 39,
"transcript": "음~ 겉바속촉의 정석이네요. 막걸리랑 찰떡궁합이에요.",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "음~", "start_ms": 65, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "겉바속촉의", "start_ms": 135, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "정석이네요.", "start_ms": 275, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "막걸리랑", "start_ms": 395, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "찰떡궁합이에요.", "start_ms": 505, "dur_ms": 71, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "It's crispy on the outside and soft on the inside. Perfect match with Makgeolli.",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "It's", "start_ms": 40, "dur_ms": 40, "phase_lr_deg": -2.0},
{"word": "crispy", "start_ms": 90, "dur_ms": 70, "phase_lr_deg": -2.0},
{"word": "on", "start_ms": 170, "dur_ms": 30, "phase_lr_deg": -2.0},
{"word": "the", "start_ms": 210, "dur_ms": 30, "phase_lr_deg": -2.0},
{"word": "outside", "start_ms": 250, "dur_ms": 90, "phase_lr_deg": -2.0},
{"word": "and", "start_ms": 350, "dur_ms": 40, "phase_lr_deg": -2.0},
{"word": "soft", "start_ms": 400, "dur_ms": 60, "phase_lr_deg": -2.0},
{"word": "on", "start_ms": 470, "dur_ms": 30, "phase_lr_deg": -2.0},
{"word": "the", "start_ms": 510, "dur_ms": 20, "phase_lr_deg": -2.0},
{"word": "inside.", "start_ms": 540, "dur_ms": 36, "phase_lr_deg": -2.0}
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
"transcript": "김치전도 하나 더 시킬까요? 뭔가 아쉬운데.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "김치전도", "start_ms": 50, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "하나", "start_ms": 190, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "더", "start_ms": 280, "dur_ms": 40, "phase_lr_deg": 5.0},
{"word": "시킬까요?", "start_ms": 330, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "뭔가", "start_ms": 450, "dur_ms": 70, "phase_lr_deg": 5.0},
{"word": "아쉬운데.", "start_ms": 530, "dur_ms": 46, "phase_lr_deg": 5.0}
]
},
{
"segment": 43,
"transcript": "좋아요! 먹는 김에 확실하게 먹어야죠.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "좋아요!", "start_ms": 60, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "먹는", "start_ms": 170, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "김에", "start_ms": 260, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "확실하게", "start_ms": 330, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "먹어야죠.", "start_ms": 460, "dur_ms": 116, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "Let's get a kimchi pancake too. No regrets!",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "Let's", "start_ms": 45, "dur_ms": 70, "phase_lr_deg": -2.0},
{"word": "get", "start_ms": 125, "dur_ms": 40, "phase_lr_deg": -2.0},
{"word": "a", "start_ms": 175, "dur_ms": 20, "phase_lr_deg": -2.0},
{"word": "kimchi", "start_ms": 205, "dur_ms": 80, "phase_lr_deg": -2.0},
{"word": "pancake", "start_ms": 295, "dur_ms": 90, "phase_lr_deg": -2.0},
{"word": "too.", "start_ms": 395, "dur_ms": 60, "phase_lr_deg": -2.0},
{"word": "No", "start_ms": 465, "dur_ms": 40, "phase_lr_deg": -2.0},
{"word": "regrets!", "start_ms": 515, "dur_ms": 61, "phase_lr_deg": -2.0}
]
},
{
"segment": 45,
"transcript": "배가 정말 찢어질 것 같아요. 근데 너무 행복하네요.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "배가", "start_ms": 55, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "정말", "start_ms": 145, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "찢어질", "start_ms": 235, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "것", "start_ms": 345, "dur_ms": 30, "phase_lr_deg": 5.0},
{"word": "같아요.", "start_ms": 385, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "근데", "start_ms": 475, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "너무", "start_ms": 545, "dur_ms": 31, "phase_lr_deg": 5.0}
]
},
{
"segment": 46,
"transcript": "비 오는 날은 역시 이게 진리인 것 같아요. 다음에 또 와요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "비", "start_ms": 70, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "오는", "start_ms": 110, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "날은", "start_ms": 180, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "역시", "start_ms": 260, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 340, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "진리인", "start_ms": 410, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 510, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "같아요.", "start_ms": 550, "dur_ms": 26, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "Definitely the best way to spend a rainy evening. Let's come back.",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Definitely", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": -2.0},
{"word": "the", "start_ms": 170, "dur_ms": 30, "phase_lr_deg": -2.0},
{"word": "best", "start_ms": 210, "dur_ms": 60, "phase_lr_deg": -2.0},
{"word": "way", "start_ms": 280, "dur_ms": 50, "phase_lr_deg": -2.0},
{"word": "to", "start_ms": 340, "dur_ms": 30, "phase_lr_deg": -2.0},
{"word": "spend", "start_ms": 380, "dur_ms": 70, "phase_lr_deg": -2.0},
{"word": "a", "start_ms": 460, "dur_ms": 20, "phase_lr_deg": -2.0},
{"word": "rainy", "start_ms": 490, "dur_ms": 60, "phase_lr_deg": -2.0},
{"word": "evening.", "start_ms": 560, "dur_ms": 16, "phase_lr_deg": -2.0}
]
},
{
"segment": 48,
"transcript": "이제 슬슬 일어날까요? 빗줄기가 좀 약해졌네요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 50, "dur_ms": 70, "phase_lr_deg": 5.0},
{"word": "슬슬", "start_ms": 130, "dur_ms": 70, "phase_lr_deg": 5.0},
{"word": "일어날까요?", "start_ms": 210, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "빗줄기가", "start_ms": 340, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "좀", "start_ms": 460, "dur_ms": 40, "phase_lr_deg": 5.0},
{"word": "약해졌네요.", "start_ms": 510, "dur_ms": 66, "phase_lr_deg": 5.0}
]
},
{
"segment": 49,
"transcript": "네, 조심히 들어들 가세요. 내일 봬요!",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 65, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "조심히", "start_ms": 125, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "들어들", "start_ms": 235, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "가세요.", "start_ms": 355, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "내일", "start_ms": 465, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "봬요!", "start_ms": 535, "dur_ms": 41, "phase_lr_deg": 0.0}
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
*Generated by WKAppBot ask gemini — 2026-04-03 06:20:27*
