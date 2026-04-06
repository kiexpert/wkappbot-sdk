# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43304  time=05:15:14  uptime=10302s  mem=1249MB  system_processes=439"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260331_051504.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 356-365-356-365-365-635-165-156-163-136-531-356-156-135-516-517-513-513-157-156-156-351-153-156-517-517-517-571-157-576-517-576-536-537-536-576-576-576-576-567-571-561-576-536-5_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 106-617-671-631-107-123-613-103-130-136-162-106-106-163-163-103-107-103-176-132-601-613_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 167-106-102-167-167-103 547-567-573-751-675-672-673-675-671-576-573-540-564-657-675-165-517-571-753-627-672-326-765-672-673-617-601-261-261 312-672-670-670-627-673-165-673-615-1_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 321-573-574-753-754 523-523-532-356-312-312-316-326-235-362-327-326-326-263-362-627-627-627-267-267-627-672-627-762-672-627-627-276-162-236-612-567-365-236-273-276-326-540-541-5_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 153-316-126-675-635-653-316-652-316 257-725-567-526-105-176-106-176-673-160-176-316-310-163-137-137-361-612-162-126-130-163-132-157-621-625-130-316-126-125-103-106-106-263-312-1_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 576-574-546-573-564-547-564-547-546-567-210-612-263-312-130-310-120-231-132-123-210-321-312-103-132-310-310-130-102-673-637-601-316-102-310-103-163-136-306-310-103-756-367-310-1_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 765-761-761-761-765-765 762-765-675 763_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 756-756-765-765-753-765 321-321-312-123-120-136-162-627-761-316-132-123-126 672-267-326-231-321-310-213-621-672-765-675-765-756 103-163-361-132-167-176-160-713-106-130-103-130-1_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 361-310-231-261-236-263-267-210-216-260-263-206-201-203-216-126-123-217 576 260-260 632-362-123-302-260-302-230-206-362-236-213-206-230-203-206-316-236-361-360-302-321-132-326-2_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 602-602-603-602-623-632-610-163-312-310-231-360-326-613-361-706-750-573-570-135-163-167-163-162-126-123-236-215-132-103-103-621-103-613-106-160-106-103-137-312-516-136-137-167-1_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 106-123-152-157-102-765-136-137-137-163-106-731-136-561-135-756-765-756-765-765-765-765-753-735-517-675-671-163-163-167-163-716-135-132-103-236-271-732-162-132-653-627-132-516-7_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 671-621-261-236-361-263-672-672-627-672-672-637-276-276-672-627-762-671-673-627-263-316-371-371-371-317-371-317-371-372-312-231-216-276-572-576-672-637-673-213-263-236-267-206-2_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 623-102-716-103-761-102-176-562-136-103-167-103-126-713-163-162-106-132-136-132-103-130-126-126-601-657-601 176-265-132-167-176-160-675-167-106-106-154-651-765-163-163-263-176-1_V.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 236-621-261-263-627-261-230-320-623-260-206-302-206-230-302-302-302-620-620-632-302-321-137-127-106-210-203-302-301-312-132-670-607-670-627-675-670-267-570-570-326-320-673-326-3_V.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 167-316-631-316-306-302-362-320-360-602-316-301-130-163-130-316-603-360-632-631-627-326-620-602-621-261-621-261-216-126-162-163-756-276-276-672-527-265-207-610-167-735-267-652-7_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 312-123-123-321-162-165-125-165-517-657-576-576-576-571-265-326-127-627-625-652-126-657-657-157-157-725-267-261-263-263-236-123-326-236-213-312-163-231-261-136-513-132-231-361-3_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 102-306-651-136-163-652-165-165-156-156-657-165-615-165-610-675-652-762-653-635-615-576-675-576-613-156-513-651-615-675-653-671-657-607-157-632-651-561-567-357-506-650-652-651-6_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 756-751-751-765-762-106 157-163-126-213-216-236-621-612-136-163_W.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 271-276-672-267-756-761-627-276-627-675-672-627-623-263-267-627-627-621-216-261-267-326-213-263-236-263-237-273-276-672-672-726-627-627-627-672-675-675-672-670-671-672-267-261-2_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 160-176-671-761-163-167-173-127-627-162-751-156-163-106-132-103-136-106-132-360-123-103-152-103-136-103-165-103-103-136-315-163-103-135-153-621-126-167-136-137-136-106-657-312-2_V.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 275-572-751-673-235-256-572-523-573-573-253-257-574-520-352-251-537-527-254 576-751-563-571-574 657-567-567-571-561-547-536-536-534-354-354-351-354-312-325-365-165-132-361-316-3_W.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 263-136-623-265-617-760-657-176-107-173-167-567-657-673-176-167-175-675-567-657-675-163-620-620-620-130-103-106-176-163-362-103-132-173-132-157-132-765-567-675-175-103-103-132-2_V.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 671-163-103-156-130-103-156-126-217-132-123-316-106-136-160-617-130-617-163-103-103-103-136-163-136-176-627-623-126-671-103-163-136-162-751-126-631-103-160-136-167-621-267-632-6_V.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 326-632-576-317-137-321-231-326-257-275-273-312-567-567-756-756-267-236-756-756-756-756-756-756-756-765-756-735-326-312-316-651-564-576-756-167-167-317-321-312-316-263-267-276 2_W.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 627-267-627-236-216-627-756-163-123-216-132-675-672-627-672-675_W.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 231 167-156-165-157-632-312-127-102-150-126 713-715-173 765-765-675-765-765-765-765-316-321-632-326-236_W.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 163-106-136-316-162-263-231-621-137-726-762 136-126_W.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 167-567-615-106-103-132-321-310-623-375-750-752-206-260-765-754-754-176-176-260-623-203-302-302-135-765-670-103-160-716-675-627-657-627-627-632-613-107-173-132-263-213-302-301-3_V.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 765-675-625-261-612-126-103-106 572 170-137-562-106-107-165-102-710-610-612-617-715-176-672-716-671 216-726-726-726-276-721-726-723-271-726-231-652-206-236-632-326-261-273-621-3_W.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 561-356-376-523-523-362-275-276-267-123-132-123 264-261-261-267-276-672-267-542-547-547-547-537-157-156-157-165-135-167-162-576-571-574-517-574-576-521-125-126-621-612-517-571 3_W.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 621-316-362-326-321-621-326-231-263-623-623-635-263-261-632-652-320-652-627-267 130-103-130-136-231-126-312-261-267-267-361-621-361-631-316-625-627-675-106-136-132-613-603-602-6_W.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 301-263-657-237-621-120-123-321-120-132 671-672-623-762-267-632-316-213-123-230-162-326-620-216-162-162-316-301-312-263-261-261 672-612-321-132 263-362-326 103-123-102-103-130-1_V.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 653-652-652-652-165-613-615-162-612-156-632-652-567-675-632-165-106-150-160-160-316-135-615-615-651-615-615-612-625-165-162-165-615-165-167-610-650-651-162-231-126-216-162-612-2_W.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 231-236-216-321-213-271-263-276-275-327-237-263-162-123-236-231-216-267-263-627-627-627-671-671-167-326-231-267-267-540-574-756-576-756-321-236-231-216-230-231-267-672-627-167-6_W.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 567-765-315-536-162-627-617-671-623 762-627-675-263-261-216 673-627 637-123-167-167-672-326-612-362-627-326 321-150-105-617-627 132 103-236-236-326-326-326-326-236-321-236-162-1_W.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 517-106 621-321-726-637-136-126-162-172-102-120-132-130-126-102-123-126-260-132-120-316-162-126-102-621-213-321-167-127-123-216-126 620-213-136-231-326-567-675-267-256-326-623-5_W.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 235-235-256-265-213-236-325-325-231-231-236-263-126-213-163-236-216-276-276-273-273-263-267-267-627-216-231-321-263-376-327-637-627-376-375-376-756-263-271-213-672-672-672-673-6_W.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 271-132 765-706-130-765-765-637-673-762-671-671-657-671-627-765-675-671-617-267-236-361_W.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 327-326-357-537-537-752-732-237-267-267-263-276-267-267-267-267-276-276-756-756-765-756-573-537-253-236-136-162-261-267-574-573-751-567-365-326-573-357-371-567-765-672-627-627-2_W.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 725-567-657-567-756-567-572-567-567-657-576-657-156-576-567-625-623-625-657-576-567-526-573-657-576-356-576-567-567-567-572-567-756-657-623-162-657-576-567-651-567-567-756-567-5_W.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 265-256-523-256-256-256-253-526-326-236-326-263-623-623-265-625-265-265-625-260-265-625-627-627-265-263-236-126-213-263-623-526-256-236-263-236-263-526-526-265-236-236-265-625-2_W.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 216-126 351-512-153-107-103-172-571-517-510-157-321-267-725-672-216-627-126-126-267-260-126-157-102-671-152-251-251-325-216-625-217-627-673-627-763-731-376-376-763-367-372-237-5_W.mp3)
  Segment 43: 66192ms ~ 66768ms (file: 136-136-136-167-167-157-517-165-613-162-615-651-653-567-562-567-657-617-675-653-632-163-615-561-651-562-576-756-756-765-756-657-576-567-756-576-765-675-675-567-652-675-562-576-5_W.mp3)
  Segment 44: 67768ms ~ 68344ms (file: 135-132-103-361-621-620-207-260-267-216-203-102-301-170-126-620-261-126-106-761-103-175-130-751-576-765-267-362-312-316-136-367-627-627-621-623 756-726-216-267-312-362-362-362-1_W.mp3)
  Segment 45: 69344ms ~ 69920ms (file: 163-657-163-160-160-312-312-160-517-612-167-502-506-625-617-162-160-165-612-762-136-567-561-160-623-623-126-106-651-612-621-316-163-123-236-261-623-567-761-176-137-761-106-162-5_W.mp3)
  Segment 46: 70920ms ~ 71496ms (file: 235-275-216-275-732-273-723-273-732-536-251-263-637-612-627-675-756-657-375-716-673-531-574-547-574-572-574-753-540-514-354 136-136-162-316-567-567-567-563-536-517-651-635-635-6_W.mp3)
  Segment 47: 72496ms ~ 73072ms (file: 206-126-763-615-163-617-673-627-612-267-362-612-621-612-163 316-316-136-126-103-716-170-627-276-623-136-367-136-103-106-176-617-163-103-160-136-612-162-103-130-507-103-103-163-3_V.mp3)
  Segment 48: 74072ms ~ 74648ms (file: 126-106-620-260-106-103-673-617-671-761-761-176-167-136-130-136-106-167-610-106-106-107-620-627-756-623-620-675-627-632-623-106-231-302-731-107-130-610-607-602-106-672-620-260-2_W.mp3)
  Segment 49: 75648ms ~ 76224ms (file: 536-156-165-517-517-567-567-560-567-651-567-651-165-615-156-516-563-675-165-156-561-516-506-316-365-621-516-561-513-135-135-156-563-517-756-576-165-576-512-157-657-516-561-367-6_W.mp3)
  Segment 50: 77224ms ~ 77800ms (file: 126-560-361-652-312-123-231-537-573-601-162-106-136-361-163-617-106-167-163-136-301-316-173-137-163-132-127-653-652-126-631-126-620-260-621-602-261-501-105-103-163-756-316-163-3_V.mp3)
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
> **Time**: 2026-03-31 05:16:05
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 2,
"transcript": "오늘 저녁에 뭐 먹을까요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 55, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "저녁에", "start_ms": 175, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 325, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "먹을까요?", "start_ms": 395, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "삼겹살 어때요? 오랜만에 고기 먹고 싶어요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "삼겹살", "start_ms": 30, "dur_ms": 180, "phase_lr_deg": 4.5},
{"word": "어때요?", "start_ms": 220, "dur_ms": 110, "phase_lr_deg": 4.6},
{"word": "오랜만에", "start_ms": 340, "dur_ms": 120, "phase_lr_deg": 4.4},
{"word": "고기", "start_ms": 470, "dur_ms": 60, "phase_lr_deg": 4.5},
{"word": "먹고", "start_ms": 540, "dur_ms": 50, "phase_lr_deg": 4.6},
{"word": "싶어요", "start_ms": 600, "dur_ms": 0, "phase_lr_deg": 4.5}
]
},
{
"segment": 4,
"transcript": "좋아요 저도 삼겹살 좋아해요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "좋아요", "start_ms": 45, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "저도", "start_ms": 185, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "삼겹살", "start_ms": 285, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "좋아해요", "start_ms": 435, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "어디 맛있는 곳 알아요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "어디", "start_ms": 40, "dur_ms": 90, "phase_lr_deg": 4.8},
{"word": "맛있는", "start_ms": 140, "dur_ms": 130, "phase_lr_deg": 4.9},
{"word": "곳", "start_ms": 280, "dur_ms": 50, "phase_lr_deg": 4.7},
{"word": "알아요?", "start_ms": 340, "dur_ms": 150, "phase_lr_deg": 4.8}
]
},
{
"segment": 6,
"transcript": "회사 근처에 유명한 식당이 있어요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "회사", "start_ms": 60, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "근처에", "start_ms": 160, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "유명한", "start_ms": 280, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "식당이", "start_ms": 410, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 510, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 8,
"transcript": "웨이팅이 길지 않을까요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "웨이팅이", "start_ms": 35, "dur_ms": 180, "phase_lr_deg": 5.0},
{"word": "길지", "start_ms": 225, "dur_ms": 90, "phase_lr_deg": 5.1},
{"word": "않을까요?", "start_ms": 325, "dur_ms": 130, "phase_lr_deg": 4.9}
]
},
{
"segment": 9,
"transcript": "제가 미리 예약해 둘게요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "제가", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "미리", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "예약해", "start_ms": 250, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "둘게요", "start_ms": 390, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "정말요? 감사합니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "정말요?", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 5.2},
{"word": "감사합니다", "start_ms": 190, "dur_ms": 230, "phase_lr_deg": 5.3}
]
},
{
"segment": 11,
"transcript": "Great! See you there at 7.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "Great!", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "See", "start_ms": 165, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 255, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "there", "start_ms": 325, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "at", "start_ms": 415, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "7.", "start_ms": 465, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "네 7시에 봬요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 4.5},
{"word": "7시에", "start_ms": 110, "dur_ms": 140, "phase_lr_deg": 4.6},
{"word": "봬요", "start_ms": 260, "dur_ms": 120, "phase_lr_deg": 4.4}
]
},
{
"segment": 13,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 14,
"transcript": "고기가 정말 신선하네요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "고기가", "start_ms": 50, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 190, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "신선하네요", "start_ms": 310, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "된장찌개도 같이 주문할까요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "된장찌개도", "start_ms": 30, "dur_ms": 180, "phase_lr_deg": 4.2},
{"word": "같이", "start_ms": 220, "dur_ms": 110, "phase_lr_deg": 4.3},
{"word": "주문할까요?", "start_ms": 340, "dur_ms": 150, "phase_lr_deg": 4.1}
]
},
{
"segment": 16,
"transcript": "네 밥이랑 같이 먹으면 맛있겠어요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 60, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "밥이랑", "start_ms": 130, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 250, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "먹으면", "start_ms": 350, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "맛있겠어요", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0}
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
"transcript": "술 한잔 하실래요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "술", "start_ms": 40, "dur_ms": 50, "phase_lr_deg": 4.0},
{"word": "한잔", "start_ms": 100, "dur_ms": 110, "phase_lr_deg": 4.1},
{"word": "하실래요?", "start_ms": 220, "dur_ms": 230, "phase_lr_deg": 3.9}
]
},
{
"segment": 19,
"transcript": "아니요 내일 일찍 출근해야 해서요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "아니요", "start_ms": 55, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "내일", "start_ms": 185, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "일찍", "start_ms": 305, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "출근해야", "start_ms": 405, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "해서요", "start_ms": 545, "dur_ms": 30, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "I understand. Work comes first.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 30, "dur_ms": 60, "phase_lr_deg": 4.5},
{"word": "understand.", "start_ms": 100, "dur_ms": 180, "phase_lr_deg": 4.6},
{"word": "Work", "start_ms": 290, "dur_ms": 110, "phase_lr_deg": 4.4},
{"word": "comes", "start_ms": 410, "dur_ms": 110, "phase_lr_deg": 4.5},
{"word": "first.", "start_ms": 530, "dur_ms": 40, "phase_lr_deg": 4.6}
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
"transcript": "고마워요 다음에 금요일에 마셔요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "고마워요", "start_ms": 45, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "다음에", "start_ms": 195, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "금요일에", "start_ms": 325, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "마셔요", "start_ms": 465, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "네 그게 좋겠네요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 35, "dur_ms": 60, "phase_lr_deg": 4.8},
{"word": "그게", "start_ms": 105, "dur_ms": 90, "phase_lr_deg": 4.7},
{"word": "좋겠네요", "start_ms": 205, "dur_ms": 160, "phase_lr_deg": 4.9}
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
"transcript": "배가 너무 부르네요 산책 좀 할까요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "배가", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 170, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "부르네요", "start_ms": 270, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "산책", "start_ms": 400, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 490, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "할까요?", "start_ms": 540, "dur_ms": 30, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "좋아요 밤공기가 시원해요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "좋아요", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 5.1},
{"word": "밤공기가", "start_ms": 160, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "시원해요", "start_ms": 310, "dur_ms": 240, "phase_lr_deg": 5.2}
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
"transcript": "저기 예쁜 가로등이 있네요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "저기", "start_ms": 60, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "예쁜", "start_ms": 180, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "가로등이", "start_ms": 300, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "있네요", "start_ms": 440, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "분위기가 정말 좋군요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "분위기가", "start_ms": 30, "dur_ms": 160, "phase_lr_deg": 4.6},
{"word": "정말", "start_ms": 200, "dur_ms": 110, "phase_lr_deg": 4.7},
{"word": "좋군요", "start_ms": 320, "dur_ms": 210, "phase_lr_deg": 4.5}
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
"transcript": "오늘 즐거웠어요 조심히 들어가세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "즐거웠어요", "start_ms": 170, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "조심히", "start_ms": 340, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "들어가세요", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "네 덕분에 잘 먹었습니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 5.2},
{"word": "덕분에", "start_ms": 110, "dur_ms": 120, "phase_lr_deg": 5.3},
{"word": "잘", "start_ms": 240, "dur_ms": 60, "phase_lr_deg": 5.1},
{"word": "먹었습니다", "start_ms": 310, "dur_ms": 230, "phase_lr_deg": 5.2}
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
"transcript": "Good night. See you tomorrow.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "Good", "start_ms": 35, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "night.", "start_ms": 155, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "See", "start_ms": 295, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 415, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "tomorrow.", "start_ms": 505, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "안녕히 주무세요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 60, "dur_ms": 180, "phase_lr_deg": 4.5},
{"word": "주무세요", "start_ms": 250, "dur_ms": 300, "phase_lr_deg": 4.6}
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
"transcript": "어제 너무 많이 먹었나 봐요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "어제", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 165, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 265, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "먹었나", "start_ms": 355, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "봐요", "start_ms": 475, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "얼굴이 좀 부으셨네요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "얼굴이", "start_ms": 35, "dur_ms": 150, "phase_lr_deg": 5.4},
{"word": "좀", "start_ms": 195, "dur_ms": 60, "phase_lr_deg": 5.3},
{"word": "부으셨네요", "start_ms": 265, "dur_ms": 280, "phase_lr_deg": 5.5}
]
},
{
"segment": 39,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 40,
"transcript": "오늘은 가볍게 샐러드 먹을까요?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "오늘은", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "가볍게", "start_ms": 200, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "샐러드", "start_ms": 320, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "먹을까요?", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "Sounds good. Healthy lunch today.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Sounds", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 5.1},
{"word": "good.", "start_ms": 190, "dur_ms": 110, "phase_lr_deg": 5.2},
{"word": "Healthy", "start_ms": 310, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "lunch", "start_ms": 450, "dur_ms": 110, "phase_lr_deg": 5.1},
{"word": "today.", "start_ms": 570, "dur_ms": 0, "phase_lr_deg": 5.2}
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
"transcript": "이 샐러드 소스가 아주 독특하네요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 60, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "샐러드", "start_ms": 130, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "소스가", "start_ms": 270, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 390, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "독특하네요", "start_ms": 490, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "

---
*Generated by WKAppBot ask gemini — 2026-03-31 05:16:05*
