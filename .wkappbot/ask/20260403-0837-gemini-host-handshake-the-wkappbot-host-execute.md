# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=08:36:11  uptime=26216s  mem=1849MB  system_processes=445"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_083601.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 216 103-103-132-103-136-170-106-152-176-716-163-132-130-132-103-736-136-754-103-137-162-162-316-653-263-716-126-312-316-314-326-214-164_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 357-527-573-507-750-750-720-732-127-127-102-127-231-213-723-137-172-107-173-137-123-123-132-132-135-134-143-132-132-123-123-316-132-312-167-627-106-160-302-326-301-312-132-216-2_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 162-716-715-120-132-136-657-657-607-672-165-162-167-610-360-136 637-132-607-657-627-126-713-671-715-765-167-716-673-675-675-612-167-715-127-612 231-236-213-263-126-263-623-265-2_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 326-623-621-621-263-260-267-210-216-261-621-621 136-103-136-106-136-671-673-123-123-106-103-673-675-675-756 627-623-675-562-627-623-672-132-312-621 162-160-613-137-136-672-657-6_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 156-126-126-263-201-651-136-672-261-613-721-267-260-267-276-236-267-756-572-326-260-260-163-301-231-132-365-261-263-132-301-103-136-102-316-312-130-306-376-371-301-167-136-136-1_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 632-263 657-765-321-316-153-165-167-162-162-167-617 327-316-617-167-617-167-163-167-726-312 672-617 165-617-165-163-613-326-623-326-632-362-621-615-167-617-617 612-361-613-631-6_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 627-627 263 263 267-236-260 631-302-130_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 160-160-103-612-610-103-132-163-160-362-617-672-762-675-610-617-102-162-106-637-167-607-160-162-675-675-675 134 627-162-612-762-672-672 547-312-130-301-263-632-267-657-576-312-1_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 103-103 621-576-260-632-672-617-165-162-562-625-265-627-726-657-762-756-657-567-657-652-572-652-725-562-526-567-562-512-625-526-657-267-256-625-325-562-263-260-762-765-761-761-1_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 260-762-216-675-762-206-261-260-125-102-106-136-162-162-621-620-162-160-136-162-567-627-670-607-163-602-612-271-612-167-617-716-672-675-615-617-612-167-162-672-263-726-765-756-5_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 136-103-106-765-621-651-136-165-103-163-103-623-167 167-136-103-157-612-127 514-156-572-136-765-136-126-172-216-136-165-163-130-761-130-103-167-152-132-176-162-103-162-132-136-1_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 126-176-176-132-132-137-123-132-365-312-136-162 106-167-671-163-136-163-165-103-103-103-123-327-167-106-136-103-103-162-365-321-136-137-763-673-174-103-132-632-126-162-312-123-2_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 615-571-563-756-527-136-613-756-756-756-375-367-567-754-756-756-574-216-136-671-621-265-267-263-256-265-265-675-675-135-675-576-765-756-576_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 230-326-126-320-320-302-350-163-137-163-163-617-164-132-136-231-310-325-321-132-163-765-706-765-675-627-267-216-163-765-130-301-310-130-103-163-761-620-627-612-132-103-621-260-2_V.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 263-126-621-326-132-715 205-132-216-306-326-360-216-130-123-130-315-534-532-301-310-301-320-350-125-206-321-326-362-623-236-216-562-761-621-617-627-627-621-321-362-162-623-603-3_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 260-230-321-321-326-326-163-263-236-310-103-103-167-301-645-516-173-754-316-302-301-132-321-167-206-267-267-130-103-301-203-260-206-203-103-130-312-132-103-136-135-165-163-136-1_V.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 253-327-327-327-732-375-235-532-523-527-574-570-753-750-356-356-312-352-325-162-621-516-352-573-527-276-725-726-257-257-236-257-537-753-753-275-753-574-537-573-536-517-572-527-2_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 126-651-362-136-613-136-103-106-163-165-127-276-106-136-103-127-167-160-162-637-163-130-610-136-130-567-612-136-106-167-201-631-621_V.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 351-354-514-251-521-521-512-521-514-513-531-513-531-251-523-521-351-531 357-356-354-357-356-354-357-351-351-357-357-357-356-354-354-354-354 357-357-352-325-352-325-325-325-357-3_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 162-103-132-137-102-156-102-312-125-613-160-136-103-623-563-162-316-263-236-213-617-362-632-162-136-130-301-123-613-103-103-130-136-326-301-125-621-165-765-136-613-163-136-132-1_V.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 135-106-163-106-106-561-612-162-126-163-103-136-162-106-103-130-103-163-163-175-613-621-156-132-163-106-671-167-103-106-167-163-126-165-612-136-562-167-103-106-136-106-613-167 7_V.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 137-127-153-631-614-167-167-675-106-160-106-163-130-135-576-132-216-236-123-123 263-267 627-672-671-675-675-316-134-143-142-123-136-132-162_V.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 376-675-312-365-362-362-361-362-367-367-637-136-361-362-326-326-326-365-365-351-350-537-653-356-356-365-365-315-136-367-317-571-576-175-157-756-715-756-751-157-571-570-576-567-5_W.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 162-126 762-672-627-127-671-167-106-176-170-172_W.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 317-175-137-317-173-731-713-701-710-731-712-276-672-726-726-271-723-721-726-723-372-327-273-732-372-732-701-327-327-723-726-723-273-276-276-276-276-276-276-276-276-237-321-231-2_W.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 132-173-106-163-710-756-751-135-130-130-123-132 765-675-672-172-617-761-107-176-750-760-750-716-675-657-716-106-107-103-102-103-130-310-120-102-103-710-143-123 751-657-567-567-6_W.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 617-671-675-675-675-761-672-567-657-756-756-675-675-675-567-625-675-627-625 162-631-617-657-623-201-240 136-132-376-167-637 671-176-367-326-312-612-567-652-621-376-765-762-216-6_W.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 573-517-312-123-132-512-213-132-312-213-513-574-576-357-237-327-327-752-753-567-265-321-523-576-572-573-537-572-752-753-752-375-273-752-752-574-573-752-273-327-237-372-356-153-1_W.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 206-260-526-256-320-320-301-310-301-132-316-306-302-320-360-365-365-213-165-754-750-672-765-574-571-153-310-675-675-130-103-103-362-103-613-632-301-316-312-301-321-301-265-276-3_V.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 672-637-673-163-316-126-607-672-671-601-612-610-160-673-756-576-567-671-106-657-753-312-167-126-675-276-153-657-765-762-765-726-762-756-765-723 672-672-672-672-672-675-672-672-6_W.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 610-607-631-176-601-715-130-623-102-167-612-361-620-126-612-127-761-162-132-617-126-263-612-671 361-321-617-612-617-162-627-312-136-362-132-564-123-621-160-167-621-167-263-562-1_W.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 230-206-236-231-236-320-230-312-103-760-671 623-632-362-326-613-613-365-265-623-326 750-312-132-762-761-761-167-125-315-127-316-370-753-756-137-316-602-620-267-263-236-326-103-1_V.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 567-572-256-607-163-621-162-761-763-670-160-576-756-720-207-207-203-327-312-123-312-213-670 673-671-573-136-167-163-106-136-103-136-136-706-607-617-613-316-103-132-103 302-302-3_V.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 612-326-231-321-312-326-321-326-216-213-312_W.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 103-301-301-312-132-321 260-267-260-236-574-657-263-320-236-216-102-106-160-106-106-103-312-130-310-310-756-762-637-321-120-260-206-126-132-130-106-106-106-103-103-310-321-362-3_V.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 673 750-765-675-657-617-167-136-632-132-137-136-132-312-132-103-132-127-176-163-617-163-163-316-316-136-160-163-156-167-103-136-136-134-106-156-137-130-103-136-103-312-132-103-1_V.mp3)
  Segment 37: 56736ms ~ 58140ms (file: 612-601-162-613-326-132-312-132-203-123-302-130-130-103-103-273-610-106-103-362-321-136-160-617-306-163-302-162-362-576-571-326-302-302-156-652-521-165-163-756-167-612-136-360-3_W.mp3)
  Segment 38: 59140ms ~ 59716ms (file: 260-302-526-763-132-316-312-167-736-752-132-362-235-673-731-753-617-521-657-567-136-576-567-756-631-130-673-756-751-576-573-260-160-130-310-301-310-162-316-267-627-617-365-721-2_W.mp3)
  Segment 39: 60716ms ~ 61292ms (file: 103-152-162-726-267-123-136-123-103-132-123-651-567-651-163-326-136-631-602-421-167-167-165-136-316-130-216-612 132-103-501-507-136-627-176-513-210-120-102-120-127-123-321-123-1_V.mp3)
  Segment 40: 62292ms ~ 62868ms (file: 627-263-621 302-206-310-123-326-162-206-260-263-620-524-263-236-236-261-260-260-126-261-621-623-267-627-263-263-312-312 657-625-625-261-621-627-672-632-617-615-621-612-162-326-2_W.mp3)
  Segment 41: 63868ms ~ 64444ms (file: 627-620-620-621-612-160-756-761-270-103-321-612-320-302-103-106-136-103-102 106-106-106-301-206-230-163-162-617-106-163-673-763-607-620-260-260-210-236-361-162-136-103-620-310-3_V.mp3)
  Segment 42: 65444ms ~ 66020ms (file: 567-562-526-526-652-526-265-265-526_W.mp3)
  Segment 43: 67020ms ~ 67596ms (file: 571-576-567-517-517-517-576-513-516-563-536-567-576-576-536-517-576-573-576-567-567-573-537-356-357-536-536-537-357-536-576-576-573-576-576-753-376-357-576-356-357-357-356-563-5_W.mp3)
  Segment 44: 68596ms ~ 69172ms (file: 267-162-213-265-267-267-261-263-267-261-326-260-263-627-620-623-632-621-126-367-623-617-163-106-106-136-637-165-163-610-601-136-107-136-103-156-516-574-152-156-716-126-627-321 3_W.mp3)
  Segment 45: 70172ms ~ 86120ms (file: 126-167-617-613-162-574-657-267-672-675-675-765-756-756-756-675-756-753-753-736-763-765-672-760-765-756-765-765-716-716-721-712-167-756-756-576-675-756-756-756-756-726-275 132-1_W.mp3)
  Segment 46: 87120ms ~ 89028ms (file: 602-620-607-652-567-302-623-106-103-103-126-136-135-103-167-132-126 315-137-675-621-617-613-103-163-316-132-107-135-123-567-165-137-571-316-132-103-602-617-251-106-136-106-675-6_V.mp3)
  Segment 47: 90028ms ~ 90604ms (file: 103-106-103-160-620-260-261-162-102-132-362-231-326-623-216-672-672-623-612-623-653-352-653-361-326-362-326-136-162-316-362-623-326-326-765-760-762-670-327 752-725-560-657-504-5_W.mp3)
  Segment 48: 91604ms ~ 92180ms (file: 312-316-351-310-310-301-612-312-130-301-310-130-132-310-103-103-106-316-310-301-123-726-321-127-120-120-106-267-627-623-625-276-627-657-657-231-623-206-537-137-216-126-130-316-1_V.mp3)
  Segment 49: 93180ms ~ 93756ms (file: 162-617-267-612-671-617-615-251-326-167-602-620-260-267-672-756-762-672-312-306-360-376-763-670-706-326-326-302-302-302-736-206-236 635-216-623-267-265-651-317-567-526-625-312-3_W.mp3)
  Segment 50: 94756ms ~ 95332ms (file: 132-726-163-167-130-361-162-306-163-106-103-517-126-126-316-103-126-136-321-713-123-765-506-167-103-126-103-135-671-106-162-623-106-521-130-620-157-103-103-106-312-106-617-103-1_V.mp3)
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
> **Time**: 2026-04-03 08:37:19
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "이거 어떻게 사용하는 거예요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "이거", "start_ms": 60, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 190, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "사용하는", "start_ms": 300, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "거예요?", "start_ms": 450, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "먼저 전원을 켜고 버튼을 누르세요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "먼저", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "전원을", "start_ms": 160, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "켜고", "start_ms": 290, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "버튼을", "start_ms": 380, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "누르세요", "start_ms": 490, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "그다음에는요?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "그다음에는요?", "start_ms": 40, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "화면에 나오는 지시를 따르시면 됩니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "화면에", "start_ms": 45, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "나오는", "start_ms": 170, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "지시를", "start_ms": 280, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "따르시면", "start_ms": 390, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "됩니다", "start_ms": 520, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "생각보다 아주 간단하네요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "생각보다", "start_ms": 55, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 210, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "간단하네요", "start_ms": 320, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "네 누구나 쉽게 할 수 있어요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "누구나", "start_ms": 110, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "쉽게", "start_ms": 240, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 350, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 420, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 8,
"transcript": "모르는 게 있으면 또 물어보세요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "모르는", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 180, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "있으면", "start_ms": 230, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 340, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "물어보세요", "start_ms": 410, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 10,
"transcript": "친절하게 설명해 주셔서 감사해요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "친절하게", "start_ms": 45, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "설명해", "start_ms": 200, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "주셔서", "start_ms": 330, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "감사해요", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "천만에요 도움이 되었다니 기쁘네요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "천만에요", "start_ms": 55, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "도움이", "start_ms": 210, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "되었다니", "start_ms": 320, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "기쁘네요", "start_ms": 450, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "Does this work automatically?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "Does", "start_ms": 70, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "this", "start_ms": 160, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "work", "start_ms": 230, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "automatically?", "start_ms": 320, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "Yes, it is very efficient.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Yes,", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "it", "start_ms": 150, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 200, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "very", "start_ms": 250, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "efficient.", "start_ms": 340, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "사용법을 잊어버리면 어떡하죠?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "사용법을", "start_ms": 45, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "잊어버리면", "start_ms": 230, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "어떡하죠?", "start_ms": 390, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "여기 사용 설명서가 들어있어요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "여기", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "사용", "start_ms": 140, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "설명서가", "start_ms": 250, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "들어있어요", "start_ms": 400, "dur_ms": 170, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "동영상으로 볼 수는 없나요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "동영상으로", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "볼", "start_ms": 250, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "수는", "start_ms": 320, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "없나요?", "start_ms": 410, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "큐알 코드를 찍으면 영상이 나와요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "큐알", "start_ms": 55, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "코드를", "start_ms": 140, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "찍으면", "start_ms": 250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "영상이", "start_ms": 360, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "나와요", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "정말 편리한 세상이네요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "편리한", "start_ms": 140, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "세상이네요", "start_ms": 270, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 20,
"transcript": "고장 나면 어디에 연락하죠?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "고장", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "나면", "start_ms": 150, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "어디에", "start_ms": 240, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "연락하죠?", "start_ms": 370, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "상자 뒤에 서비스 센터 번호가 있어요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "상자", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "뒤에", "start_ms": 140, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "서비스", "start_ms": 230, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "센터", "start_ms": 340, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "번호가", "start_ms": 430, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 540, "dur_ms": 30, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "일 년 동안 무상 수리가 가능해요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "일", "start_ms": 40, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "년", "start_ms": 90, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "동안", "start_ms": 140, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "무상", "start_ms": 230, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "수리가", "start_ms": 320, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "가능해요", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 24,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 25,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 26,
"transcript": "영수증을 꼭 챙겨야겠네요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "영수증을", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "꼭", "start_ms": 200, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "챙겨야겠네요", "start_ms": 270, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 28,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 29,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 30,
"transcript": "맞아요 보증서랑 같이 보관하세요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "맞아요", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "보증서랑", "start_ms": 180, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 330, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "보관하세요", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "알겠습니다 잘 쓸게요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "알겠습니다", "start_ms": 35, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 250, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "쓸게요", "start_ms": 320, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "오늘 점심은 뭐 먹을까요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "점심은", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 280, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "먹을까요?", "start_ms": 370, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "시원한 냉면 어때요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "시원한", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "냉면", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "어때요?", "start_ms": 290, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 35,
"transcript": "좋아요 비빔냉면 아니면 물냉면?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "좋아요", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "비빔냉면", "start_ms": 160, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "아니면", "start_ms": 310, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "물냉면?", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "저는 물냉면이 먹고 싶어요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 35, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "물냉면이", "start_ms": 140, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "먹고", "start_ms": 270, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "싶어요", "start_ms": 360, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "그럼 냉면집으로 갑시다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "냉면집으로", "start_ms": 150, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "갑시다", "start_ms": 300, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 39,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 40,
"transcript": "만두도 같이 시킬까요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "만두도", "start_ms": 45, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "시킬까요?", "start_ms": 290, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "네 만두가 빠지면 섭섭하죠",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 50, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "만두가", "start_ms": 120, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "빠지면", "start_ms": 250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "섭섭하죠", "start_ms": 360, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 43,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 44,
"transcript": "배가 많이 고파요 빨리 가요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "배가", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 130, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "고파요", "start_ms": 240, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "빨리", "start_ms": 370, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "가요", "start_ms": 480, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "(mixed)",
"speaker": "speaker_2",
"confidence": 0.85,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "mixed",
"words": [
{"word": "맛있게", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "먹고", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "옵시다", "start_ms": 290, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "거기 식당 줄 서야 할까요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "거기", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "식당", "start_ms": 150, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "줄", "start_ms": 260, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "서야", "start_ms": 330, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "할까요?", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "인기가 많아서 그럴 수도 있어요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "인기가", "start_ms": 55, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "많아서", "start_ms": 180, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "그럴", "start_ms": 310, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "수도", "start_ms": 420, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 510, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "그럼 서둘러야겠네요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 35, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "서둘러야겠네요", "start_ms": 140, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 50,
"transcript": "다 왔습니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "다", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "왔습니다", "start_ms": 140, "dur_ms": 400, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 08:37:19*
