# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43304  time=05:34:02  uptime=11430s  mem=1628MB  system_processes=440"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260331_053353.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 103-362-162-160-165-167-102-126-102-130-361-163-163-726-162-165-560-103-136-652-567-361-362-103-514-165-572-162-621-512-510-657-625-627-632-136-236-267-625-625-627-567-652-627-5_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 623-376-632-362-327-236-763-761-756-765-762-326-236-320-326-321-367-712-721-726-627-623-523-230-320 631-167-162-632-731-627-327-376-371-756-762-576-765-765-672-673-765-765-137-1_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 136-130-316-302-617-310-316-136-136-132-123-136-162-172-106-163-136-132-132-361-612-123-132-316-163-631-326-310-103-312-130-301-301-310-302-130-267-236-102-625-310-306-312-132-3_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 567-576-567-675-567-567-576-567-576-657-675-657-567-576-675-657_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 316-631-316-325-367-317-316-317-361-367-632-726-761-106-126-216-162-312-310-167-672-602-163-132-126-316-637-617-672-362-326-601-302_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 632-267-623-261-263-361-126-623 132-103 130-132-130-103-132-130-102-713-730-370-371-103-130-103-301-132_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 627-130-136-136-756-310-136-103-653-130-103-103-130-176-316-156-165-564-576-576-150-157-176-547-574-574-756-765-712-172-765-362-206-162-160-765-756-756-657-675-134-134-136-570-7_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 703-165-103-562-123-312-163-132-617-361-132-320-302-162-126-765-173-673-672-106-261-623-301-672-537-260-106-162-126-123-163-136-136-163-135-316-136-132-216-637-167-631-130-671-7_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 236-263-627-756-235-326-236-361-321-136-716-673-672-213-162-163-216-136-675-763-236-236-236-321-317-170-761-103-136-675-167-760-607-657-265-213-236-203-132-623-562-657-713-312-1_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 267-216-271-276-126-213-231-523-356-351-265-236-625-653-625-265-256-623-217-216-216-357-215-231-312-351-321-357-352-152-321-273-263-267-263-315-135-135-130-130-316-126-136-312-2_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 162-675-726-621-162-236-231-263-716-762-762-765-657-762-752-726-726_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 126-126-216-276-627-136-623-263-564-576-675-267-672-672-763-671-617-176-671-761-761-675-326-637-162-673-362-632-536-563-362-263-267-276-267-326-652-672-267-267-256-562-362-632-2_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 320-320-361-673-623-635-601-603-365-675-576-651-160-310-236-603-602-602-620-260-260-260-620-612-162-617-506-623-316-631-613-612-160-607-602-162-627-602-206-231-123-103-631-361-6_V.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 236-216-263-126-216-126-216-261-216-236-263-261-216-263-236-213-326-326-321-321-621-326-316-312-321-326-216-123-321-173-127-132-213-217-273-217-261-267-236-231-261-521-256-256-2_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 756-765-761-216-675-760-670-670-706-760-760-670-675-176-724-217-216-635-764-123-167-713-143-312-671-167-136-137-126-756-756-756-756-756-736-627-672-627-123-136-163-325-371-365 6_V.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 136-756-765-107-701-765-763-302-160-106-160-612-612-106-163-162-163-756-765-675-675-607-612-621-603-361-306-301-321-501-653-365-632-206-206-260-621-216-302-316-156-263-267-207-2_V.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 310-307-736-756-376-137-312-750-375-761-167-167-302-236-263-216-265-302-310-106-103-123-312-321-260-602-621-612-617-310-165-321-310-104-132-265-261-103-132-625-264-670-265-163-1_V.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 372 765-706-765-765-765-765-765-756-756-756-756-756-765-765-765-675-765-657-576-675-765-756-765-675-765-765-765-765-236-302-320-312-320-216-126-217-213-213-326_W.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 637-627 571-574-756-103-312-132-617-675-651-615-601-675-657-652 576-576-567 765-765-756-675-765-576 162-621-162-162-127-162-163-176-167-167-167-613-716-176-276-126-132-127-312-1_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 315-361-631-163-561-516-635-653-536-534-523-365-362-632-165-132-236-126-216-327-362-627-132-267-261-136-276-237-267-276-317-126-127-317-326-327-132-231-321-236-237-627-317-172-2_W.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 621-621-126-261-231-271-271-276-267-267-627-261-621-627-672-376-627-731-172-123-627-672-312-627-267-621-126-163-162-673-132-607-627-765-613-316-126-217-326-213-237-276-263-326-6_W.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 621-216-631-125-256-267-321-326-326-326-236-567-576-576-672-365-657-635-326-753-574-632-623-562-567-567-237-327-572-572-275-276-276-276-312-132-326-321-263-236-326-321-574-574-6_W.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 362-365-326-267-526-256-675-567-576-572-265-265-563-512-561-516-156-157-751-265-253-235-132-125-517-165-531-532-517-537-576-215-315-126-236-326-235-376-317-371-321-132-135-362-1_W.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 671-675-675-657-765-675-672-675-765-675-675-675-675-657-657-657-627-672-275-725-637-673-714-375-713_W.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 671-576-756-632-623-312-367-517-571-537-576-572-576-752-725-574-537-536-716-751-571-523-325-312-215-235-256-576-571-576-651-657-765-265-175-612-137-137-316-137-317-567-567-567-5_W.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 623-623-623-263-632-326-612-132-326-312-231-236-263-261-321-326-623-632-631-632-623-263-263-623-326-623-263-236-623-312-326-263-236-567-263-625-216-621-321-326-126-216-267-261-2_W.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 231-162-612-132-162 106-103-173-103-103-362-137-162-162-213-163-130-167-601-610-160-671-612-607-621-261-601-106-103-103-613-106-103-135-126 357-751-107-106-527-137-612 760-136-1_V.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 235-254-253-236-265-235-265-251-253-502-273-327-327-325-352-257-257-215-250-201-210-236-261-215-527-254-253-625-635-532-735-375-570-317-371-317-317-137-316-326-127-167-136-365-3_W.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 657-576-576-326-365-651-627-126-215-571-621-267-137-621-671-672-613-632-361-361-316-361-516-527-573-657-571-657-573-157-617-576-751-637-376-537-573-537-573-756-572-567-576-657-5_W.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 231-312-135-231-126-261-263-261-261-263-231-231-534-315-325-321-321-315-265-326-321-326-541-263-326-261-263-231-261-263-236-312-326-362-265-526-532-312-321-261-524-253-236-321-2_W.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 236-236-526-275-527-265-326-261-213-263-526-256-567-576-356-352-326-321-326-126-361-632-261-263-267-267-267-263-362-362-623-260-561-523-501-256-527-625-267-265-267-263-625-570-5_W.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 576-357-517-576-526-537-517-315-315-315-230-302-172-176-162-132-130-130-130-123-216-216-216-521-567-256-526-325-517-572-237-257-267-253-235-326-267-213-276-217-125-627-631-623-3_W.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 156-517-163-157-165-517-365-153-165-136-135-361-153-516-567-135-132-125-235-236-231-265-256-671-156-175-156-356-165-351-751-561-576-571-567-567-516-165-615-165-613-567-615-571-7_W.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 576-567-576-567-576-576-572-567-567-572-526-567-576-567-576-576-576-576-576-567-576-562-576-576-576-567-576-657-576-576-657-562-765-562-756-576-561-516-657-567-567-562-652-756-6_W.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 106-362-167-136-756-706-670-263-627-621-621-671-230-236-623-670-675-672-637-316-136-316-316-316-613-261-621-362-675-675-670-670-167-167-136-320-130-167-312-206-620-162-610-602-1_V.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 261-236 160-610-617-617-613-671-671-617-167-671-761-675-670-136-310-106-130-102-130-126-673-672 132-341-132-312-571-541-157 261-623-673-632-671-136-612-132-123 621-236-613-136-1_W.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 234-231-132-123-136-137-136-312-132-132-134-132-136-163-130-675-165-103-316-163-167-163-130-103-631-103-156-136-231-167-126-617-263-175-517-657-627-675-605-675-657-167-165-106-6_V.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 615-132-137-160-103-132-136-167-236-136-751-256-103-167-213-320-136-132-163-130-103-106-317-103-172-217-172-127-765-726-172-762-721-172-762-172-213-210-120-210-120_V.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 236-623-361-576-576-576-576-371-531-513-531-315-235-325-321-637-327-276-723-317-376-371-163-316-231-123-123-231-236-231-210-201-210-254-152-251-173-371-321-231-213-213-123-271-3_W.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 765-761-761-761-765-765 762-765-675 763_W.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 267-276-236-236 213-261-326-326-261-263-172-136 310-361-612-271-627-673-267-623-132-130-102-132-310-312-310-103-132 312 672-627-267 621-261-216_W.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 761-706-176-712-127-627-761-763-761-756-576-567-576-752-756-576-756-751-751-765-761-576-526-567-352-537-357-572-132-162-623-371-675-670-673-167-612-613_W.mp3)
  Segment 43: 66192ms ~ 66768ms (file: 136-176-617-163-102-106-617-106-162-167-106-106-167-516-126-620-123-317-103-167-651-521-615 132-106-163-162-130-103-167-617-163-576-126-126-162-136-103-103-103-136-163-675-562-7_V.mp3)
  Segment 44: 67768ms ~ 68344ms (file: 312-130-307-574-504-574-574-507-576-574-507-507-502-132-312-132-231-123-213-321-231-236-231-751-301-302-206-263-326-362-326-306-236-267-621-136-126-163-167-167-163-107-127-267-2_V.mp3)
  Segment 45: 69344ms ~ 69920ms (file: 310-671-760-762-671-672-627-672-672-627-617-627-631-627-672-631-627-637-673-672-627-627-672-672-672-673-627-672-627-672-627-672-672-672-672-276-672-672-627-627-637-672-627-632-6_W.mp3)
  Segment 46: 70920ms ~ 71496ms (file: 302-375-203-230-321-123-132-123-271-256-261-176-762-726-276-216-163-631-612-562-756-726-713-267-320-321-137-756-260-230-302-306-357-132-136-651-627-621-612-605-672-672-627-105-1_V.mp3)
  Segment 47: 72496ms ~ 73072ms (file: 103-136-574-136-106-136-361-137-631-361-103-136-216-162-160-163-612-136-167-136-130-163-517-362-621-163-316-103-136-715-137-576-130-167-136-103-362-136-637-312-163-103-163-136-1_V.mp3)
  Segment 48: 74072ms ~ 74648ms (file: 625-657-651-263-526-265-652-635-632-635-531-531-571-561-563-567-517-562-615-562-562-652-615-625-623-126-612-261-231-621-326-265-625-625-265-627-627-267-625-752-627-673-265-625-6_W.mp3)
  Segment 49: 75648ms ~ 76224ms (file: 263-267 136-165-167-103-103-106-123-132-176-152-571-157-135-106-103-163-103-136-176-617-167-106-106-216-162-715-321-651-106-107-103-175-175-176-106-103-236-567-567-526-651-765-7_W.mp3)
  Segment 50: 77224ms ~ 77800ms (file: 632-627-672-623-623-627-623-621-612-316-136-675-671-632 672-763_W.mp3)
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
> **Time**: 2026-03-31 05:35:00
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "다음 주에 여행 가기로 한 거 기억하죠?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "다음", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "주에", "start_ms": 165, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "여행", "start_ms": 295, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "가기로", "start_ms": 435, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 555, "dur_ms": 15, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 570, "dur_ms": 0, "phase_lr_deg": 0.0},
{"word": "기억하죠?", "start_ms": 570, "dur_ms": 6, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "그럼요! 벌써 짐도 다 싸 뒀어요.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "그럼요!", "start_ms": 30, "dur_ms": 180, "phase_lr_deg": -5.2},
{"word": "벌써", "start_ms": 220, "dur_ms": 110, "phase_lr_deg": -5.0},
{"word": "짐도", "start_ms": 340, "dur_ms": 90, "phase_lr_deg": -5.3},
{"word": "다", "start_ms": 440, "dur_ms": 40, "phase_lr_deg": -5.1},
{"word": "싸", "start_ms": 490, "dur_ms": 40, "phase_lr_deg": -5.2},
{"word": "뒀어요.", "start_ms": 540, "dur_ms": 36, "phase_lr_deg": -5.2}
]
},
{
"segment": 3,
"transcript": "제주도는 비행기 표가 비싸지 않나요?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "제주도는", "start_ms": 60, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "비행기", "start_ms": 250, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "표가", "start_ms": 380, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "비싸지", "start_ms": 460, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "않나요?", "start_ms": 550, "dur_ms": 26, "phase_lr_deg": 0.0}
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
"transcript": "미리 예약해서 싸게 샀어요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "미리", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": -4.8},
{"word": "예약해서", "start_ms": 160, "dur_ms": 180, "phase_lr_deg": -4.9},
{"word": "싸게", "start_ms": 350, "dur_ms": 110, "phase_lr_deg": -4.7},
{"word": "샀어요.", "start_ms": 470, "dur_ms": 106, "phase_lr_deg": -4.8}
]
},
{
"segment": 6,
"transcript": "That's great! Lucky you.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "That's", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "great!", "start_ms": 165, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "Lucky", "start_ms": 315, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "you.", "start_ms": 455, "dur_ms": 121, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "가서 맛있는 해산물 많이 먹으려고요.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "가서", "start_ms": 35, "dur_ms": 90, "phase_lr_deg": -5.0},
{"word": "맛있는", "start_ms": 135, "dur_ms": 130, "phase_lr_deg": -5.1},
{"word": "해산물", "start_ms": 275, "dur_ms": 140, "phase_lr_deg": -4.9},
{"word": "많이", "start_ms": 425, "dur_ms": 90, "phase_lr_deg": -5.2},
{"word": "먹으려고요.", "start_ms": 525, "dur_ms": 51, "phase_lr_deg": -5.0}
]
},
{
"segment": 8,
"transcript": "제주도 흑돼지도 유명하잖아요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "제주도", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "흑돼지도", "start_ms": 200, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "유명하잖아요.", "start_ms": 370, "dur_ms": 206, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "맞아요. 벌써부터 입에 침이 고이네요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "맞아요.", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": -4.5},
{"word": "벌써부터", "start_ms": 160, "dur_ms": 150, "phase_lr_deg": -4.6},
{"word": "입에", "start_ms": 320, "dur_ms": 80, "phase_lr_deg": -4.4},
{"word": "침이", "start_ms": 410, "dur_ms": 90, "phase_lr_deg": -4.5},
{"word": "고이네요.", "start_ms": 510, "dur_ms": 66, "phase_lr_deg": -4.6}
]
},
{
"segment": 10,
"transcript": "사진 많이 찍어서 보내주세요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "사진", "start_ms": 55, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 175, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "찍어서", "start_ms": 295, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "보내주세요.", "start_ms": 415, "dur_ms": 161, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "I'll take lots of pictures.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "I'll", "start_ms": 30, "dur_ms": 60, "phase_lr_deg": -5.4},
{"word": "take", "start_ms": 100, "dur_ms": 90, "phase_lr_deg": -5.3},
{"word": "lots", "start_ms": 200, "dur_ms": 110, "phase_lr_deg": -5.5},
{"word": "of", "start_ms": 320, "dur_ms": 60, "phase_lr_deg": -5.4},
{"word": "pictures.", "start_ms": 390, "dur_ms": 186, "phase_lr_deg": -5.5}
]
},
{
"segment": 12,
"transcript": "즐겁고 안전한 여행 되세요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "즐겁고", "start_ms": 50, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "안전한", "start_ms": 190, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "여행", "start_ms": 340, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "되세요.", "start_ms": 460, "dur_ms": 116, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "고마워요. 선물 사 올게요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "고마워요.", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": -5.1},
{"word": "선물", "start_ms": 230, "dur_ms": 140, "phase_lr_deg": -5.0},
{"word": "사", "start_ms": 380, "dur_ms": 60, "phase_lr_deg": -5.2},
{"word": "올게요.", "start_ms": 450, "dur_ms": 126, "phase_lr_deg": -5.1}
]
},
{
"segment": 14,
"transcript": "오! 기대하고 있을게요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "오!", "start_ms": 25, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "기대하고", "start_ms": 105, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "있을게요.", "start_ms": 295, "dur_ms": 281, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "See you in a week. Bye!",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 45, "dur_ms": 80, "phase_lr_deg": -4.2},
{"word": "you", "start_ms": 135, "dur_ms": 60, "phase_lr_deg": -4.3},
{"word": "in", "start_ms": 205, "dur_ms": 40, "phase_lr_deg": -4.1},
{"word": "a", "start_ms": 255, "dur_ms": 40, "phase_lr_deg": -4.2},
{"word": "week.", "start_ms": 305, "dur_ms": 110, "phase_lr_deg": -4.3},
{"word": "Bye!", "start_ms": 425, "dur_ms": 151, "phase_lr_deg": -4.2}
]
},
{
"segment": 16,
"transcript": "안녕히 가세요!",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 60, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "가세요!", "start_ms": 280, "dur_ms": 296, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "안녕하세요, 오늘도 날씨가 좋네요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "안녕하세요,", "start_ms": 50, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "오늘도", "start_ms": 270, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 390, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "좋네요.", "start_ms": 520, "dur_ms": 56, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 19,
"transcript": "네, 산책하기 딱 좋은 날씨예요.",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 35, "dur_ms": 80, "phase_lr_deg": 2.5},
{"word": "산책하기", "start_ms": 125, "dur_ms": 210, "phase_lr_deg": 2.4},
{"word": "딱", "start_ms": 345, "dur_ms": 60, "phase_lr_deg": 2.6},
{"word": "좋은", "start_ms": 415, "dur_ms": 80, "phase_lr_deg": 2.5},
{"word": "날씨예요.", "start_ms": 505, "dur_ms": 71, "phase_lr_deg": 2.4}
]
},
{
"segment": 20,
"transcript": "공원에 꽃이 많이 폈더라고요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "공원에", "start_ms": 60, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "꽃이", "start_ms": 200, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 320, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "폈더라고요.", "start_ms": 420, "dur_ms": 156, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "Really? I should go see them.",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Really?", "start_ms": 40, "dur_ms": 130, "phase_lr_deg": 2.8},
{"word": "I", "start_ms": 180, "dur_ms": 60, "phase_lr_deg": 2.7},
{"word": "should", "start_ms": 250, "dur_ms": 110, "phase_lr_deg": 2.9},
{"word": "go", "start_ms": 370, "dur_ms": 80, "phase_lr_deg": 2.8},
{"word": "see", "start_ms": 460, "dur_ms": 60, "phase_lr_deg": 2.7},
{"word": "them.", "start_ms": 530, "dur_ms": 46, "phase_lr_deg": 2.9}
]
},
{
"segment": 22,
"transcript": "점심 먹고 같이 가실래요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "점심", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "먹고", "start_ms": 170, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 270, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "가실래요?", "start_ms": 390, "dur_ms": 186, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "네, 좋아요. 커피도 한잔해요.",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": 3.0},
{"word": "좋아요.", "start_ms": 120, "dur_ms": 140, "phase_lr_deg": 3.1},
{"word": "커피도", "start_ms": 270, "dur_ms": 120, "phase_lr_deg": 2.9},
{"word": "한잔해요.", "start_ms": 400, "dur_ms": 176, "phase_lr_deg": 3.0}
]
},
{
"segment": 24,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 25,
"transcript": "그럼 1시에 로비에서 봐요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "1시에", "start_ms": 165, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "로비에서", "start_ms": 295, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "봐요.", "start_ms": 485, "dur_ms": 91, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "알겠습니다. 이따 뵙겠습니다.",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "알겠습니다.", "start_ms": 35, "dur_ms": 180, "phase_lr_deg": 4.5},
{"word": "이따", "start_ms": 225, "dur_ms": 110, "phase_lr_deg": 4.6},
{"word": "뵙겠습니다.", "start_ms": 345, "dur_ms": 231, "phase_lr_deg": 4.4}
]
},
{
"segment": 27,
"transcript": "주말에는 보통 뭐 하세요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "주말에는", "start_ms": 60, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "보통", "start_ms": 250, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 370, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "하세요?", "start_ms": 440, "dur_ms": 136, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "저는 집에서 영화 보는 걸 좋아해요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": -5.1},
{"word": "집에서", "start_ms": 170, "dur_ms": 120, "phase_lr_deg": -5.2},
{"word": "영화", "start_ms": 300, "dur_ms": 90, "phase_lr_deg": -5.0},
{"word": "보는", "start_ms": 400, "dur_ms": 80, "phase_lr_deg": -5.1},
{"word": "걸", "start_ms": 490, "dur_ms": 40, "phase_lr_deg": -5.2},
{"word": "좋아해요.", "start_ms": 540, "dur_ms": 36, "phase_lr_deg": -5.0}
]
},
{
"segment": 29,
"transcript": "I prefer reading books at home.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "prefer", "start_ms": 110, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "reading", "start_ms": 260, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "books", "start_ms": 420, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "at", "start_ms": 540, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "home.", "start_ms": 575, "dur_ms": 1, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "독서도 좋죠! 어떤 책을 읽으세요?",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "독서도", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": -5.4},
{"word": "좋죠!", "start_ms": 165, "dur_ms": 90, "phase_lr_deg": -5.5},
{"word": "어떤", "start_ms": 265, "dur_ms": 110, "phase_lr_deg": -5.3},
{"word": "책을", "start_ms": 385, "dur_ms": 100, "phase_lr_deg": -5.4},
{"word": "읽으세요?", "start_ms": 495, "dur_ms": 81, "phase_lr_deg": -5.5}
]
},
{
"segment": 31,
"transcript": "주로 소설이나 에세이를 읽어요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "주로", "start_ms": 55, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "소설이나", "start_ms": 175, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "에세이를", "start_ms": 365, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "읽어요.", "start_ms": 525, "dur_ms": 51, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "추천해주실 만한 책이 있나요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "추천해주실", "start_ms": 30, "dur_ms": 230, "phase_lr_deg": -4.2},
{"word": "만한", "start_ms": 270, "dur_ms": 110, "phase_lr_deg": -4.3},
{"word": "책이", "start_ms": 390, "dur_ms": 120, "phase_lr_deg": -4.1},
{"word": "있나요?", "start_ms": 520, "dur_ms": 56, "phase_lr_deg": -4.2}
]
},
{
"segment": 33,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 34,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 35,
"transcript": "이 소설 정말 감동적이에요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 60, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "소설", "start_ms": 110, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 250, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "감동적이에요.", "start_ms": 380, "dur_ms": 196, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "저도 한 번 빌려주실 수 있나요?",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "저도", "start_ms": 45, "dur_ms": 90, "phase_lr_deg": -5.1},
{"word": "한", "start_ms": 145, "dur_ms": 60, "phase_lr_deg": -5.2},
{"word": "번", "start_ms": 215, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "빌려주실", "start_ms": 285, "dur_ms": 180, "phase_lr_deg": -5.1},
{"word": "수", "start_ms": 475, "dur_ms": 40, "phase_lr_deg": -5.2},
{"word": "있나요?", "start_ms": 525, "dur_ms": 51, "phase_lr_deg": -5.0}
]
},
{
"segment": 37,
"transcript": "네, 다 읽고 나서 드릴게요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 35, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 105, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "읽고", "start_ms": 175, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "나서", "start_ms": 295, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "드릴게요.", "start_ms": 415, "dur_ms": 161, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "고마워요. 기대하고 있을게요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "고마워요.", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": -5.4},
{"word": "기대하고", "start_ms": 240, "dur_ms": 210, "phase_lr_deg": -5.3},
{"word": "있을게요.", "start_ms": 460, "dur_ms": 116, "phase_lr_deg": -5.5}
]
},
{
"segment": 39,
"transcript": "Enjoy your reading time.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Enjoy", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": -5.1},
{"word": "your", "start_ms": 190, "dur_ms": 60, "phase_lr_deg": -5.2},
{"word": "reading", "start_ms": 260, "dur_ms": 180, "phase_lr_deg": -5.0},
{"word": "time.", "start_ms": 450, "dur_ms": 126, "phase_lr_deg": -5.1}
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
"transcript": "감사합니다. 즐거운 주말 보내세요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "감사합니다.", "start_ms": 65, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "즐거운", "start_ms": 305, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "주말", "start_ms": 445, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "보내세요.", "start_ms": 545, "dur_ms": 31, "phase_lr_deg": 0.0}
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
"transcript": "네, 월요일에 밝은 모습으로 봐요.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 45, "dur_ms": 60, "phase_lr_deg": -5.1},
{"word": "월요일에", "start_ms": 115, "dur_ms": 150, "phase_lr_deg": -5.2},
{"word": "밝은", "start_ms": 275, "dur_ms": 110, "phase_lr_deg": -5.0},
{"word": "모습으로", "start_ms": 395, "dur_ms": 130, "phase_lr_deg": -5.1},
{"word": "봐요.", "start_ms": 535, "dur_ms": 41, "phase_lr_deg": -5.2}
]
},
{
"segment": 44,
"transcript": "Have a wonderful weekend everyone.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "en",
"silence_before_

---
*Generated by WKAppBot ask gemini — 2026-03-31 05:35:00*
