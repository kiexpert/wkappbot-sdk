# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43304  time=06:01:06  uptime=13054s  mem=1546MB  system_processes=440"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260331_060056.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 162-271-762-173-317-173-317-157-316-165-540-356-567-574-157-523-536-576-175-275-756-537-357-235-637-376-540-563-516-612-123-362-316-316-271-612-162-571-517-567-652-136-613-316-5_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 312-103-132-213 163 621-126-103-162-106-160-106-162-136-130-123-103-132-126-162-123-751-613-617-123-103-106_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 726-673-672-361-132-312-627-620-263-263-206-260-621-162-621-362-162-162-136-162-621-627-612-361-637-613-673-326-672-276-263-756-176-673-631-367 576-507-574-570-657-130 562-765-6_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 675-236-263-326-321-623-536-526-623-263-267-672-673-672-672-261-261-721-217-657-615-675-671-617-576-257-231-312-132-512-527-527-657-567-675-657-675-673-265-267-267-527-526-276-6_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 103-136-130-130-163-106-103-136-137-263-612-263-321-312-123-675-265-263-716-126-620 163-657-103-316-106-136-605-751-162-162 216-136-103-361-613-672-625 610-312-136-657-361-106-1_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 316-136-132-312-263-261-267-261-263-263-621-632-512-316-132-561-567-356-361-365-361-316-531-534-537-537-315-316-315-653-167-136-173-153-357-537-615-367-673-657-673-263-362-132-1_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 752-257-265-625-627-625-725-627-627-672-627-675-672-752-675-625-652-625-627-257-526-562-652-625-620-620-372-327-376-375-375-362-326-362-365-635-365-372-367-376-375-367-672-627-6_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 132-136-132-162-312-625-231-627-675-672-627-617-162-127-267-127-213-625-265 132-103 201-261 302-527-372-237-637-367-362-326-301-310-132-312-310-316-326-671-327-310-132-136-103-1_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 621-261-261-362-265-352-263-612-562-567-567-156-612-632-361-263-263-326-326-236-236-627-627-657-564-564-563-562-562-615-562-576-675-526-567-576-567-574-567-576-567-516-150-527-5_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 567-576-765-752-572-576-765-576 603-163 601-613-362-621-136-312-312_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 301-320-263-327-126-231-132-761-136-127-126-751-231-231-327-271-321-576-136-321-320-231-263-675-157-127-312-213-216-321-213-213-123-231-231-210-132-321-167-716-613-123-321-321-3_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 106-612-136-103-106-137-103-265-136-136-103-163-316-130-136-637-136-175-103-136-136-362-317-365-637-562-257-132-136-136-162-160-216-163-136-301-126-136-163-103-107-132-162-162-3_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 173-317-761-731-372-317-315-315-731-540-527-752-132-132-137-173-135-531-564-546-524-564-213-321-317-312-631-236-236-230-325-210-250-207-152-275-152-754-215-526-562-752-275-725-7_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 156-312-325-321-321-231-327-231-351-352-156-357-315-563-361-365-536-536-514-564-561-532-657-372-532-563-356-356-573-563-356-367-236-534-352-153-635-312-256-215-250-215-321-235-2_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 752-756-765-765-765-765-765-765-765-765-765-763-576-756-756-765-765-762-675-671-574-571-576-576-576 756-765 765-762-716-751 567-576-573-754-756-765-756-765-765-675-670-765-765-7_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 136-756-765-107-701-765-763-302-160-106-160-612-612-106-163-162-163-756-765-675-675-607-612-621-603-361-306-301-321-501-653-365-632-206-206-260-621-216-302-316-156-263-267-207-2_V.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 673-316-673-162-326-621 671-627 361-316-321-263-672-612-672-621-617-167-761-761-761-617-167-621-613-621-612-312_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 761-160-107-167-102-132-165-106-167-260-625-126-617-106-167-107-103-610-136-527-126-106-165-125-627-162-206-103-106-621-276-502-163-126-167-103-623-123-623-163-103-765-132-167-1_V.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 237-162-612-610-602-620-263-162-261-612-162-621-612-620-632-620-316-263-602-657-675-167-103-163-510-570-561-167-167-106-162-106-163-167-163-163-137-167-167-176-301-320-326-320-5_V.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 316-756-173-167-627-163-136-130-301-316-765-760-765 637-671-672-672-267-207-276-362-361-361-316-361-316-361-267-602-672-672-671-627-672-672-627-672-761-621-621-621-602-602-751-5_V.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 617-675-675-613-136-321-126-136 613_W.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 162-612-216-106-310-203-236-320-302-302-130-310 630-102-213-103-132-312-523-532-302-657-576-350-302-320-326-320-263-260-132-302-310-103-103-102-312-130-103-130-163-130-130-103-1_V.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 173-132-123-132 126 613-362-316-123 674-672 175-157-715-701-715-712-725-275-203-362_V.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 753-375-756-574-567-632-267-627-625-576-572-576-574-527-257-256-526-574-537-153-316-321-532-534-543-574-523-325-231-231-231-526-523-231-261-263-631-236-267-263-265-263-263-263-2_W.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 456-576-106-106-102-163-136-260-206-261-103-106-152-162-172-106-165-106-167-176-716-601-106-165-103-160-763-106-102-103-517-365-132-126-176-103-167-615-176-617-163-316-103-102-1_V.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 167-162-613-167-231-630-603-136-136-612-206-203-261-260-261-216-631-612-263-216-276-236-263-276-217-371-236-627-672-762-763-620-276-627-612-602-106-207-617-631-172-170-617-130-6_W.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 236-752-756-570-537-534-501-574-576-570-537-357-537-532-537-135-357-765-756-675-627-275-756-750-572-750-572-756-765-675-532-513-356-510-352-327-513-325-315-312-312-537-351-305-5_W.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 675-675-627-567-756-657-762-675-675-567-765-675-675-765-675-675-675-657-765-675-756 765-675-637-176-617-130-617-367-635-627-765-625-675-675-675-756-627-621-136-632-726-675-765-6_W.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 263-263-563-765-315-351-531-236-265-265-625-627-627-267-372-321-267-547-567-513-315-564-564-514-231-263-236-256-524-653-632-623-263-562-326-312-316-531-561-365-326-263-236-540-2_W.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 632-672-361-362-623-623-136-651-671-301-136-170-167-263-625-612-362-136-103-130-163-136-160-162-613-163-163-167-562-126-312-132-106-106-103-167-671-106-106-130-167-256-176-162_W.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 263-263-762-672 627-627-623-672-623-362_W.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 102-126-123-132-612-321-576-765-756 576-267-312-326-753-631-623-326-612-236 325-765-672-276-267 627-267-632-263-362-356-326-623-263-672-672-632-326-263-261-263-236-623-631-567-3_W.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 675-576-675-653-651-627-571-153-534-571-560-657-257-635-351-357-621-537-573-573-527-527-572-254-253-534-534-534-532-540 310 132_W.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 206-537-256-236-236-203-213-123-217-210-123-213-123-130-132-601-612-326-132-741-574-756-163-571-615-516-106-156-510-561-567-576-560-157-150-657-657-621-216-210-210-207-201-257-2_W.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 267-123-675-106-263-620-167-162-136-160-716-170-175-160-517-136-137-653-653-165-261-576-576-756-657-765-135_V.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 357-537-576-567-576-637-653-536-574-574-756-375-574-547-540-573-576-537-563-571-571-517-536-253-256-257-256-257-547-524-527-361-625-321-263-326-315-235-362-326-253-526-265-236-2_W.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 756-765-675-625-672-253-632-657-526-357-321-312-236-263-123-213-123-136-120-236-321-312-312-213-276-327-316_W.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 162-632-106-136-170-106-163-613-106-163-615-326-162-126-261-317-160-167-316-627-162-216-126-713-126-103-163-163-136-162-716-162-102 325-106-106-130-136-361-167-167-617-163-105-1_V.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 267-263-263-216-126-273-123-216-261-213-327-231-327-623-612-621-623-623-623-267-627-627-627-621-621-265-625-257-756-675-627-627-267-627-261-627-267-576-267-263-627-627-267-267-6_W.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 567-567-567-657-615-657-657-156-573-567-561-567-567-567-657-651-756-517-576-517-156-157-567-561-567-576-567-576-235-652-657-516-156-167-153-156-516-576-576-156-567-157-571-516-5_W.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 103-136-321-213-621-576-576-576-576-621-267-261-160-163-316-261-627-621-362-326-675-657-167-602-612-216-213-216-213-206-206-126-263-321-132-136-267-302-106-316-103-672-657-762-6_V.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 136-176-617-163-102-106-617-106-162-167-106-106-167-516-126-620-123-317-103-167-651-521-615 132-106-163-162-130-103-167-617-163-576-126-126-162-136-103-103-103-136-163-675-562-7_V.mp3)
  Segment 43: 66192ms ~ 66768ms (file: 132-130-103-162-136-570-175-365-136-165-126-132-123-612-103-132-361-136-130-130-136-631-123-123-102-130-160-103-261-123-321-132-617-126-372-607-130-103-123-173-132-316-627-261-2_V.mp3)
  Segment 44: 67768ms ~ 68344ms (file: 126-132-316-321-321-312-315-352-325-317-315-315-316-316-215-527-517-315-325-376-236-326-372-376-327-321-317-312-237-176-576-567-567-567-516-365-306-316-567-536-576-563-306-301-6_W.mp3)
  Segment 45: 69344ms ~ 69920ms (file: 657-657-607-750-672-267-627-263-657-627-267-627-260-612-625-562-617-607-671-136-326-623-630-607_W.mp3)
  Segment 46: 70920ms ~ 71496ms (file: 372-627-627-637-672-672-263-236-362-261-321-321-562-162-107-136-164-162-621-206-675-675-652-675-627-672-762-153-756-526-360-132-106-675-163-163-316-106-160-167-627-673-103-103-1_V.mp3)
  Segment 47: 72496ms ~ 73072ms (file: 123-132-123-132-162-362-103-361-365-602-173-162-160-623-126-162-162-613-136-136-615-136-621-103-167-162-137 103-165-167-106-162-671-516-163-167-106-135-327-726-123 163-103-136-1_V.mp3)
  Segment 48: 74072ms ~ 74648ms (file: 627-136-316-162-162-123-253-625-612-627-627 672-627-627 216-123-136 765-567-657-657-635_W.mp3)
  Segment 49: 75648ms ~ 76224ms (file: 756-763-671-673-762-675-136-106-137-136-765-762-103-137-317-730-173-163-621-216-136-712-103-132-136-173-326-321-126-316-136-136-136-317-136-103-163-613-571-103-102-621-621-602-6_V.mp3)
  Segment 50: 77224ms ~ 77800ms (file: 157-357-537-537-537-357-362-237-156-517-567-574-573-756-375-375-375-657-326-267-632-267-267-765-267-372-576-574-573-153-157-157-163-162-163-126-137-167-162-163-162-126-156-165-5_W.mp3)
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
> **Time**: 2026-03-31 06:01:56
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "다음 주 수요일에 시간이 괜찮으세요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "다음", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "주", "start_ms": 165, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "수요일에", "start_ms": 235, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "시간이", "start_ms": 375, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "괜찮으세요?", "start_ms": 475, "dur_ms": 101, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "네, 오후에는 괜찮을 것 같아요.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 30, "dur_ms": 60, "phase_lr_deg": -5.2},
{"word": "오후에는", "start_ms": 100, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "괜찮을", "start_ms": 260, "dur_ms": 110, "phase_lr_deg": -5.3},
{"word": "것", "start_ms": 380, "dur_ms": 40, "phase_lr_deg": -5.1},
{"word": "같아요.", "start_ms": 430, "dur_ms": 146, "phase_lr_deg": -5.2}
]
},
{
"segment": 3,
"transcript": "그럼 3시에 카페에서 만날까요?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 60, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "3시에", "start_ms": 160, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "카페에서", "start_ms": 280, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "만날까요?", "start_ms": 430, "dur_ms": 146, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "Sounds like a plan. Where is it?",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Sounds", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": -4.8},
{"word": "like", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": -4.9},
{"word": "a", "start_ms": 250, "dur_ms": 20, "phase_lr_deg": -4.7},
{"word": "plan.", "start_ms": 280, "dur_ms": 110, "phase_lr_deg": -4.8},
{"word": "Where", "start_ms": 400, "dur_ms": 90, "phase_lr_deg": -4.9},
{"word": "is", "start_ms": 500, "dur_ms": 40, "phase_lr_deg": -4.7},
{"word": "it?", "start_ms": 550, "dur_ms": 26, "phase_lr_deg": -4.8}
]
},
{
"segment": 5,
"transcript": "강남역 5번 출구 앞에 있는 곳이에요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "강남역", "start_ms": 50, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "5번", "start_ms": 190, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "출구", "start_ms": 310, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "앞에", "start_ms": 410, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 510, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "곳이에요.", "start_ms": 560, "dur_ms": 16, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "아, 거기 커피가 정말 맛있죠.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "아,", "start_ms": 35, "dur_ms": 40, "phase_lr_deg": -5.0},
{"word": "거기", "start_ms": 85, "dur_ms": 110, "phase_lr_deg": -5.1},
{"word": "커피가", "start_ms": 205, "dur_ms": 130, "phase_lr_deg": -4.9},
{"word": "정말", "start_ms": 345, "dur_ms": 90, "phase_lr_deg": -5.2},
{"word": "맛있죠.", "start_ms": 445, "dur_ms": 131, "phase_lr_deg": -5.0}
]
},
{
"segment": 7,
"transcript": "맞아요. 그날 봬요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "맞아요.", "start_ms": 60, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "그날", "start_ms": 180, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "봬요.", "start_ms": 330, "dur_ms": 246, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "See you then!",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": -4.5},
{"word": "you", "start_ms": 200, "dur_ms": 80, "phase_lr_deg": -4.6},
{"word": "then!", "start_ms": 290, "dur_ms": 286, "phase_lr_deg": -4.4}
]
},
{
"segment": 9,
"transcript": "안녕히 가세요.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 55, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "가세요.", "start_ms": 295, "dur_ms": 281, "phase_lr_deg": 0.0}
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
"transcript": "오늘 회의 자료 다 읽어봤어?",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 30, "dur_ms": 110, "phase_lr_deg": 2.5},
{"word": "회의", "start_ms": 150, "dur_ms": 110, "phase_lr_deg": 2.6},
{"word": "자료", "start_ms": 270, "dur_ms": 110, "phase_lr_deg": 2.4},
{"word": "다", "start_ms": 390, "dur_ms": 50, "phase_lr_deg": 2.5},
{"word": "읽어봤어?", "start_ms": 450, "dur_ms": 126, "phase_lr_deg": 2.6}
]
},
{
"segment": 12,
"transcript": "내용이 좀 많아서 아직 다 못 봤어.",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "내용이", "start_ms": 50, "dur_ms": 130, "phase_lr_deg": 4.1},
{"word": "좀", "start_ms": 190, "dur_ms": 60, "phase_lr_deg": 4.2},
{"word": "많아서", "start_ms": 260, "dur_ms": 110, "phase_lr_deg": 4.0},
{"word": "아직", "start_ms": 380, "dur_ms": 80, "phase_lr_deg": 4.1},
{"word": "다", "start_ms": 470, "dur_ms": 40, "phase_lr_deg": 4.2},
{"word": "못", "start_ms": 520, "dur_ms": 30, "phase_lr_deg": 4.0},
{"word": "봤어.", "start_ms": 560, "dur_ms": 16, "phase_lr_deg": 4.1}
]
},
{
"segment": 13,
"transcript": "I think the proposal is too long.",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 2.8},
{"word": "think", "start_ms": 110, "dur_ms": 110, "phase_lr_deg": 2.7},
{"word": "the", "start_ms": 230, "dur_ms": 40, "phase_lr_deg": 2.9},
{"word": "proposal", "start_ms": 280, "dur_ms": 150, "phase_lr_deg": 2.8},
{"word": "is", "start_ms": 440, "dur_ms": 40, "phase_lr_deg": 2.7},
{"word": "too", "start_ms": 490, "dur_ms": 40, "phase_lr_deg": 2.9},
{"word": "long.", "start_ms": 540, "dur_ms": 36, "phase_lr_deg": 2.8}
]
},
{
"segment": 14,
"transcript": "동감이야. 핵심만 요약할 필요가 있어.",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "동감이야.", "start_ms": 25, "dur_ms": 180, "phase_lr_deg": 4.4},
{"word": "핵심만", "start_ms": 215, "dur_ms": 110, "phase_lr_deg": 4.5},
{"word": "요약할", "start_ms": 335, "dur_ms": 130, "phase_lr_deg": 4.3},
{"word": "필요가", "start_ms": 475, "dur_ms": 90, "phase_lr_deg": 4.4},
{"word": "있어.", "start_ms": 570, "dur_ms": 6, "phase_lr_deg": 4.5}
]
},
{
"segment": 15,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 16,
"transcript": "오늘 점심은 뭐 먹을까?",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 60, "dur_ms": 110, "phase_lr_deg": 2.5},
{"word": "점심은", "start_ms": 180, "dur_ms": 120, "phase_lr_deg": 2.4},
{"word": "뭐", "start_ms": 310, "dur_ms": 60, "phase_lr_deg": 2.6},
{"word": "먹을까?", "start_ms": 380, "dur_ms": 196, "phase_lr_deg": 2.5}
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
"transcript": "어제 과음해서 해장이 필요해.",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "어제", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 4.7},
{"word": "과음해서", "start_ms": 170, "dur_ms": 150, "phase_lr_deg": 4.8},
{"word": "해장이", "start_ms": 330, "dur_ms": 110, "phase_lr_deg": 4.6},
{"word": "필요해.", "start_ms": 450, "dur_ms": 126, "phase_lr_deg": 4.7}
]
},
{
"segment": 19,
"transcript": "그럼 시원한 김치찌개 어때?",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 35, "dur_ms": 90, "phase_lr_deg": 3.0},
{"word": "시원한", "start_ms": 135, "dur_ms": 110, "phase_lr_deg": 3.1},
{"word": "김치찌개", "start_ms": 255, "dur_ms": 180, "phase_lr_deg": 2.9},
{"word": "어때?", "start_ms": 445, "dur_ms": 131, "phase_lr_deg": 3.0}
]
},
{
"segment": 20,
"transcript": "좋아! 생각만 해도 속이 풀리는 것 같아.",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "좋아!", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 4.5},
{"word": "생각만", "start_ms": 160, "dur_ms": 110, "phase_lr_deg": 4.6},
{"word": "해도", "start_ms": 280, "dur_ms": 60, "phase_lr_deg": 4.4},
{"word": "속이", "start_ms": 350, "dur_ms": 60, "phase_lr_deg": 4.5},
{"word": "풀리는", "start_ms": 420, "dur_ms": 90, "phase_lr_deg": 4.6},
{"word": "것", "start_ms": 520, "dur_ms": 30, "phase_lr_deg": 4.4},
{"word": "같아.", "start_ms": 560, "dur_ms": 16, "phase_lr_deg": 4.5}
]
},
{
"segment": 21,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 22,
"transcript": "오늘 퇴근하고 영화 보러 가실래요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "퇴근하고", "start_ms": 165, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "영화", "start_ms": 315, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "보러", "start_ms": 415, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "가실래요?", "start_ms": 495, "dur_ms": 81, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 24,
"transcript": "What kind of movie is it?",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "What", "start_ms": 35, "dur_ms": 80, "phase_lr_deg": -5.1},
{"word": "kind", "start_ms": 125, "dur_ms": 80, "phase_lr_deg": -5.2},
{"word": "of", "start_ms": 215, "dur_ms": 40, "phase_lr_deg": -5.0},
{"word": "movie", "start_ms": 265, "dur_ms": 110, "phase_lr_deg": -5.1},
{"word": "is", "start_ms": 385, "dur_ms": 60, "phase_lr_deg": -5.2},
{"word": "it?", "start_ms": 455, "dur_ms": 121, "phase_lr_deg": -5.0}
]
},
{
"segment": 25,
"transcript": "액션 영화예요. 재미있을 것 같아요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "액션", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "영화예요.", "start_ms": 170, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "재미있을", "start_ms": 310, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 460, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "같아요.", "start_ms": 520, "dur_ms": 56, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "I love action movies! Let's go.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": -4.2},
{"word": "love", "start_ms": 110, "dur_ms": 110, "phase_lr_deg": -4.3},
{"word": "action", "start_ms": 230, "dur_ms": 110, "phase_lr_deg": -4.1},
{"word": "movies!", "start_ms": 350, "dur_ms": 130, "phase_lr_deg": -4.2},
{"word": "Let's", "start_ms": 490, "dur_ms": 50, "phase_lr_deg": -4.3},
{"word": "go.", "start_ms": 550, "dur_ms": 26, "phase_lr_deg": -4.2}
]
},
{
"segment": 27,
"transcript": "그럼 6시 반에 극장 앞에서 봬요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 55, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "6시", "start_ms": 175, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "반에", "start_ms": 265, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "극장", "start_ms": 355, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "앞에서", "start_ms": 475, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "봬요.", "start_ms": 565, "dur_ms": 11, "phase_lr_deg": 0.0}
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
"transcript": "제주도 여행은 어떠셨나요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "제주도", "start_ms": 45, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "여행은", "start_ms": 195, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "어떠셨나요?", "start_ms": 335, "dur_ms": 241, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "바다가 정말 아름다웠어요.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "바다가", "start_ms": 35, "dur_ms": 130, "phase_lr_deg": -5.5},
{"word": "정말", "start_ms": 175, "dur_ms": 110, "phase_lr_deg": -5.3},
{"word": "아름다웠어요.", "start_ms": 295, "dur_ms": 281, "phase_lr_deg": -5.4}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 33,
"transcript": "The food was also delicious.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "The", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": -4.2},
{"word": "food", "start_ms": 110, "dur_ms": 110, "phase_lr_deg": -4.3},
{"word": "was", "start_ms": 230, "dur_ms": 60, "phase_lr_deg": -4.1},
{"word": "also", "start_ms": 300, "dur_ms": 110, "phase_lr_deg": -4.2},
{"word": "delicious.", "start_ms": 420, "dur_ms": 156, "phase_lr_deg": -4.3}
]
},
{
"segment": 34,
"transcript": "특히 갈치조림이 기억에 남네요.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "특히", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": -4.8},
{"word": "갈치조림이", "start_ms": 170, "dur_ms": 180, "phase_lr_deg": -4.9},
{"word": "기억에", "start_ms": 360, "dur_ms": 110, "phase_lr_deg": -4.7},
{"word": "남네요.", "start_ms": 480, "dur_ms": 96, "phase_lr_deg": -4.8}
]
},
{
"segment": 35,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 36,
"transcript": "나중에 저도 꼭 가봐야겠어요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "나중에", "start_ms": 65, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "저도", "start_ms": 205, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "꼭", "start_ms": 305, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "가봐야겠어요.", "start_ms": 375, "dur_ms": 201, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 38,
"transcript": "오늘 공부하느라 수고 많았어.",
"speaker": "speaker_5",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 35, "dur_ms": 110, "phase_lr_deg": 4.1},
{"word": "공부하느라", "start_ms": 155, "dur_ms": 140, "phase_lr_deg": 4.2},
{"word": "수고", "start_ms": 305, "dur_ms": 90, "phase_lr_deg": 4.0},
{"word": "많았어.", "start_ms": 405, "dur_ms": 171, "phase_lr_deg": 4.1}
]
},
{
"segment": 39,
"transcript": "시험 준비가 생각보다 어렵네.",
"speaker": "speaker_6",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "시험", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 5.4},
{"word": "준비가", "start_ms": 170, "dur_ms": 110, "phase_lr_deg": 5.3},
{"word": "생각보다", "start_ms": 290, "dur_ms": 130, "phase_lr_deg": 5.5},
{"word": "어렵네.", "start_ms": 430, "dur_ms": 146, "phase_lr_deg": 5.4}
]
},
{
"segment": 40,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 41,
"transcript": "You'll do great! Stay positive.",
"speaker": "speaker_5",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "You'll", "start_ms": 40, "dur_ms": 90, "phase_lr_deg": 5.1},
{"word": "do", "start_ms": 140, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "great!", "start_ms": 210, "dur_ms": 110, "phase_lr_deg": 5.2},
{"word": "Stay", "start_ms": 330, "dur_ms": 110, "phase_lr_deg": 5.1},
{"word": "positive.", "start_ms": 450, "dur_ms": 126, "phase_lr_deg": 5.0}
]
},
{
"segment": 42,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 43,
"transcript": "응, 조금만 더 힘내볼게.",
"speaker": "speaker_6",
"confidence": 0.98,

---
*Generated by WKAppBot ask gemini — 2026-03-31 06:01:56*
