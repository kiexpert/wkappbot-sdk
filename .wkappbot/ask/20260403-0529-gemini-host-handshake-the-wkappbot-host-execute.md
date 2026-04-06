# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=05:28:08  uptime=14934s  mem=1589MB  system_processes=452"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_052758.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 576-657 602 103-130-102-263-620-260-260-260-263-312-765-267-621-672-620-620-620-106-162-103-107-103-103-103-210-132-206-130-103-106-130-301-316-132-162-261-612-627-216-267-102-3_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 103-326-321-320-301-301-213-576-567-653-267-136-123-263-132-256-261-261-675-675-136-675-652-136-361-365-162-652-763-163-165-156-163-173-367-635-657 562 321-132-312-735-726-327-6_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 675-672-672-627-726-756-765-756-176-176-176-176 617_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 312-125-136-126-216-126-132-213-261-326-132-312-123-126-132-312-321-123-103-162_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 105-150-157-510-136-106-760-216-267-326-632-231-163-210-206-312 135-156 132-621-106 217-613-162 167-637-627-231-567-675 152-523-620-612-627-612-657_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 265-265-326-263-236-263-526-527-537-537-365-316-316-563-576-576-576-625-627-627-627-625-657-562-523-567-576-567-675-367-362-326-613-650-567-675-625-567-567-651-136-136-106-216-2_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 103-130-265-631-130-107-716-136-103-301-130-562-136-316-163-310-160-162-126-106-136-130-132-136-236-235-157-165-103-106-260-167-510-136-162-130-537-263-267-321-657-657-673-236-2_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 301-321-312-321-612-574-320-326-312-132-310-130-135-316-137-573-317-315-357-315-513-153-357-513-356-317-653-567-751-130-731-732-235-312-321-132-123-217-172-132-123-120-103-123-1_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 756-765-576-576-567-576-571-715-751 132-312-136-163-132-123_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 302-267-263-216-120-106-126-302-326-126-103-602-657-576-107-106-103-713-176-716-276-267-627-672-761-213-326-302-230-236-263-231-326-627-136-162-621-163-162 671-231-236-163-136-3_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 237-230-203-203-203-756-751-716-203-620-602-540-547-132-672-672-627-620 621-231-321-312-132-103 620-610-610-610-360-621-756-675-127-671-617-167-716-106-301-137-637-167-126-617-3_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 163-601-671-103-137-176-176-621-126-216-362-103-130-126-126-621-216-162-316-213-612-102-162-106-103-610-162-631-130-107-103-106-316-167-103-310-106-617-617-163-106-261-206-261-1_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 765-573-756-537-567-715-375-152-162_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 123-213-136-361-132-230-326-201-236-612 301_V.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 675-627-267-632-137-317-367-362-327-675-625-627-627-602-602-726-672-761-762-761-167-176-163-157-312-167-106-167-136-750-751-136-136-103-136-136-132-163-163-631-634-570-571-750 1_V.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 107-106-213-326-126-310-316-132-123-320-306-302-321-320-321-132-367-362-320-326-670-637-632-132-103-316-750-570-617-162-217-362-236-623-326-362-263-672-210-310-301-301-632-260-1_V.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 163-612-102-120-210-521-325-263-621-263-216-120-201-210-201-206-320-362-306-302-231-206-206-230-203-762-267-627-627-261-260-752-267-207-263-263-621-206-320-320-321-213-273-127-2_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 130-130-164-165-106-102-103-106-102-106-106-167-612-162-123-132-623-260-612-612-263-260-621-261-261-250-527-521-127-306-310-302-630-260-260-263-103-610-612-326-651-126-120-267-2_W.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 576-762-627-372-362-316-216-631-162-632-362-637-312-312-126-162-132-102-132-675-675-765-671-675-567-605-763-765-571-674-627-612 263-216-263-231-263-612-263-236-312-103-136-103-6_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 165-601-102-621-621-103-312-130-316-216-103-106-320-306-321-306-301-321-160-162-162-306-136-316-601-632-260-620-621-621-267-360-320-261-627-675-360-231-360-560-320-103-613-132-7_V.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 261-276-267-261-627-362-623-623-672-672-671-637-623-672-637-675-673-762-756-673-671-637-637-237-617-672-371-263-637-327-723-327-726-267-317-761-760-627-762-627-672-267-673-621-3_W.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 572-576-564-576-567-576-517-512-135-102-512-153-152-317-107-154-517-150-510-576-516-135-317-105-517-516-156-576-567-725-327-321-321-326-321-231-123-612-216-236-236-163-123-162-1_W.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 675-672-672-672-675-671-156-123-165_W.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 763-761 316-657-651-216-216 210-216-312-637-106-651-167-632-162-326-756-103-136-760-601-361-103-625-615-137-167-103-136-162-102-175-231-136-103-617-162-610-617-261-261-126-261-1_W.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 602-670-632-612-623-620-627-267-263-265-256-265-625-265-265-260-765-625-365-657-763-657-573-567-576-576-265-627-267-627-267-267-267-627-632-627-672-367-637-163-631-361-163-361-6_W.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 102-301 657-236-762-726-312-132-167_V.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 627 705-163-312-103-136-173-176-103-231-312-310-623-610-130-167-137-165-631-537-567-507-507-517-527-257-572-576-537-621-230-321-756-651-652-657-675-576-236-321-321-325-231-325-3_W.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 123-213-723-602-162-162-712-610-167-130-126-102-567 165-176-571-132-167-106-103-103-154-162-176-612-561-126-103-617-103-167-167-165-163-613-160-621-312-136-103-516-106-102-716-1_V.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 620-630-213-163-316-136-162-162-302-312-637-136-130-162-130-310-376-170-162-162-675-675-267-623-617-613-361-637-103-716-367-301-316-261-120-106-167-106-106-162-136-361-613-163-1_V.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 102-316-263-132-136-123-163-761-160-760-675-657-165-617-165-716-136-613-103-136-103-713-176-176-613-130-106-132-136-132-103-102-103-107-761-670-310-310-321-320-321-132-106-103-1_V.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 631-167-132-103-130-162-175-103-102-103-671-126-106-136-123-167-165-160-162-657-163-106-167-621-130-137-130-103-165-103_V.mp3)
  Segment 32: 48856ms ~ 50152ms (file: 675 627-672-672-136-526-102 756-576-357-671-765-675-671-673-617-321 216-120-263-632-517-152-132-756-651-167-317-160-367-612-126-103-130-165_W.mp3)
  Segment 33: 51152ms ~ 64976ms (file: 752-715-156-137-631-103-137-170-765-716-765-176-167-136-576-156-517-165-132-126-713-127-170-671-715-173-170-175-172-162-756-756-756-751-756-175-751-170-701-701-617-162-671-670-6_W.mp3)
  Segment 34: 65976ms ~ 66552ms (file: 130-316-216-126-123-260-206-206-756-103-210-231-210-216-321-103-312-321-316-106-310-301-231-310-103-132-203-302-312-326-302-106-106-163-136-310 576-236-130-206-263-236-230-230-3_V.mp3)
  Segment 35: 67552ms ~ 68128ms (file: 531-547-563-365-652-625 361-631-512-257-263-623-362-564-524-534-542-567-657-572-540-312-321-312-231-261-621-361-635-651-635-653-651-165-615-165-517-574-517-365-213_W.mp3)
  Segment 36: 69128ms ~ 69704ms (file: 326-216-621-362-261-632-263-261-132-136-136-312-326-263_W.mp3)
  Segment 37: 70704ms ~ 71280ms (file: 576-263-267-267-276-576-362-637-367-637_W.mp3)
  Segment 38: 72280ms ~ 72856ms (file: 652-532-632-235-236-362-637-765-621-162-265-623-265-627-632-623-367-623-623-263-326-263-236-612-627-623-632-263-631-673-657-362-263-627-726-276-632-301-312 627-627-632-672-263-6_W.mp3)
  Segment 39: 73856ms ~ 74432ms (file: 216-612-617-167-672-765-602-267-620-627-675-627-362-635-261-312-672-672-672-162-236-175-715-163-132-103-103-310-103-136-173-136-152-321-615-130-130-160-236-612-260-617-167-132-1_W.mp3)
  Segment 40: 75432ms ~ 76008ms (file: 312-613-362-367-163-672-236-367-621-162-160-675-675-675-167-652-657-617-162-216-632-765-576-621-162-672-216-127-173-361-675-762-765-623-672-136-672-612-617-752-672-671-216-236-6_W.mp3)
  Segment 41: 77008ms ~ 77584ms (file: 543-312-132-312-316-162-136-162-136-132-136-613-136-621-136-130-361-136-632-761-736-207-675-765-675-765-760-760-623-632-623-267-265-627-276-267-123-130-136-612-361-675-261-621-6_W.mp3)
  Segment 42: 78584ms ~ 79160ms (file: 126-230-627-627-623-301-613-306-106-165-165-106-761-607-136-167-160-756-615-106-162-512-125-260-170-216-165-130-103-136-517-167-106-163-670-236-175-605-126-154-103-103-103-651-1_V.mp3)
  Segment 43: 80160ms ~ 80736ms (file: 106-761-136-106-512-267-716 137-102-176-167-175-162-175-602-521-361-306-103-136-301-176-316-136-123-160-603-516-756-617-167-136-620-627-162-765-617-675-612-621-167-617-163-571-2_V.mp3)
  Segment 44: 81736ms ~ 82312ms (file: 765-756-756-675 257-236-236-302-130-231-103-102-237-367-103-310-175-105-671-652-562-627-315-123-132-531-326-361-136-156-163-163-631-163-567-561-657-271-267-132-516-756-675-167-5_W.mp3)
  Segment 45: 83312ms ~ 83888ms (file: 260-302-320-301-135-103-153 765-623-723-563-567-367-306-621-162 206-260-206-260-205-261-201-203-627-321-326-230-231-230-230-206-206-201-251-136-136-301-136-132-103-103-316-265-2_W.mp3)
  Segment 46: 84888ms ~ 85464ms (file: 106-103-631-103-167-150-306-175-126 617-127-175-176-175-275-217-271-271-126-216-263-126-261-267-216-123-132-316-126-216-175-675-675-627-167 612-601-106-132-103-135-103-261-165-1_V.mp3)
  Segment 47: 86464ms ~ 87040ms (file: 261-261-621-261-263-236-261-236-263-216-216-261-261-260-620-625-261-627-267-263-216-261-621-216-276-261-261-276-267-267-726-267-276-267-216-267-267-263-261-267-267-267-267-261-2_W.mp3)
  Segment 48: 88040ms ~ 88616ms (file: 132-162-103-261-261-321-231 627-675-765-163-136-137-106-671-127-712-127-317-130-173-126-213-260-176 162-103-132-132-103-312-132-301-134-106-516-710-167-136-167-352-123 105-751-1_V.mp3)
  Segment 49: 89616ms ~ 90192ms (file: 617-163-126-120-267-261-312-163-726-673-675-672 574-301-261-132-310-130-321-361-632-326-356-576-102-162-160-136-163-671-106-137-136-163-106-163-137 672-762-761 617-167-265-276-6_W.mp3)
  Segment 50: 91192ms ~ 91768ms (file: 261-132 216-162 765-675-256-756-567 675-675-261-263-261-672-162-627-162-261-627-657-623-321-623-613-162-756-567-576-160-236-612-261-162_W.mp3)
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
> **Time**: 2026-04-03 05:29:11
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "오늘의 명언을 들려드릴게요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "오늘의", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "명언을", "start_ms": 200, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "들려드릴게요", "start_ms": 390, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "삶이 있는 한 희망은 있다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "삶이", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 165, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 275, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "희망은", "start_ms": 365, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있다", "start_ms": 525, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 4,
"transcript": "While there is life, there is hope.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "While", "start_ms": 45, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "there", "start_ms": 205, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 315, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "life,", "start_ms": 405, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "there", "start_ms": 535, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 645, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "hope.", "start_ms": 735, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "키케로의 명언입니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "키케로의", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "명언입니다", "start_ms": 190, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "어떤 상황에서도 희망을 잃지 마세요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "어떤", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "상황에서도", "start_ms": 180, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "희망을", "start_ms": 370, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "잃지", "start_ms": 520, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "마세요", "start_ms": 630, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "Never lose hope in any situation.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Never", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "lose", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "hope", "start_ms": 310, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 440, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "any", "start_ms": 530, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "situation.", "start_ms": 640, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 9,
"transcript": "희망은 어둠 속에서도 빛을 발하니까요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "희망은", "start_ms": 60, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "어둠", "start_ms": 220, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "속에서도", "start_ms": 330, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "빛을", "start_ms": 490, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "발하니까요", "start_ms": 600, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "Hope shines even in the dark.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Hope", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "shines", "start_ms": 180, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "even", "start_ms": 340, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 450, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 540, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "dark.", "start_ms": 630, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "당신의 앞날에 항상 희망이 가득하기를 응원합니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "당신의", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "앞날에", "start_ms": 170, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "항상", "start_ms": 320, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "희망이", "start_ms": 430, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "가득하기를", "start_ms": 560, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "응원합니다", "start_ms": 750, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "I hope your future is always full of hope.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 50, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "hope", "start_ms": 110, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "your", "start_ms": 220, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "future", "start_ms": 310, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 460, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "always", "start_ms": 550, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "full", "start_ms": 680, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "of", "start_ms": 790, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "hope.", "start_ms": 850, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 14,
"transcript": "포기하지 않는 마음이 가장 소중합니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "포기하지", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "않는", "start_ms": 230, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "마음이", "start_ms": 340, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "소중합니다", "start_ms": 580, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "An unyielding heart is the most precious thing.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "An", "start_ms": 55, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "unyielding", "start_ms": 145, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "heart", "start_ms": 355, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 485, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 575, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "most", "start_ms": 635, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "precious", "start_ms": 745, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "thing.", "start_ms": 905, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "오늘의 명언이었습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "오늘의", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "명언이었습니다", "start_ms": 190, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 18,
"transcript": "감사합니다. 내일 또 만나요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "감사합니다.", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "내일", "start_ms": 250, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 380, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "만나요.", "start_ms": 470, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "Thank you. See you again tomorrow.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Thank", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "you.", "start_ms": 180, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "See", "start_ms": 270, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 380, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "again", "start_ms": 470, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "tomorrow.", "start_ms": 600, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 21,
"transcript": "안녕히 가세요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 30, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 220, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "Goodbye.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "Goodbye.", "start_ms": 35, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "(instrument)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 24,
"transcript": "행복한 하루 되세요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "행복한", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "하루", "start_ms": 190, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "되세요", "start_ms": 320, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "Have a happy day.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Have", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 150, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "happy", "start_ms": 210, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "day.", "start_ms": 340, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 27,
"transcript": "다음에 또 뵙겠습니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 35, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 195, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "뵙겠습니다", "start_ms": 305, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "See you again next time.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 150, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "again", "start_ms": 240, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "next", "start_ms": 370, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "time.", "start_ms": 480, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "고맙습니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "고맙습니다", "start_ms": 30, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "Thank you.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "Thank", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "you.", "start_ms": 165, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 32,
"transcript": "언제나 여러분의 곁에 있겠습니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "언제나", "start_ms": 60, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "여러분의", "start_ms": 220, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "곁에", "start_ms": 410, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있겠습니다", "start_ms": 520, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "We will always be by your side.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "We", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "will", "start_ms": 140, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "always", "start_ms": 230, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "be", "start_ms": 390, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "by", "start_ms": 480, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "your", "start_ms": 570, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "side.", "start_ms": 660, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 35,
"transcript": "행복하세요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "행복하세요", "start_ms": 35, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "Be happy.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Be", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "happy.", "start_ms": 120, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 38,
"transcript": "좋은 꿈 꾸세요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 35, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "꿈", "start_ms": 145, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "꾸세요", "start_ms": 235, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "Sweet dreams.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Sweet", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "dreams.", "start_ms": 170, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "평안한 밤 되시기 바랍니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "평안한", "start_ms": 45, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "밤", "start_ms": 205, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "되시기", "start_ms": 295, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "바랍니다", "start_ms": 455, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "Have a peaceful night.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Have", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 160, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "peaceful", "start_ms": 220, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "night.", "start_ms": 410, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "내일 아침에 다시 만나요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "내일", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "아침에", "start_ms": 170, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 320, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 430, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "See you again tomorrow morning.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "again", "start_ms": 250, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "tomorrow", "start_ms": 380, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "morning.", "start_ms": 540, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 45,
"transcript": "잘 자요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "잘", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "자요", "start_ms": 120, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "Sleep tight.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "Sleep", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "tight.", "start_ms": 165, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 48,
"transcript": "사랑합니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "사랑합니다", "start_ms": 30, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "I love you.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 35, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "love", "start_ms": 95, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "you.", "start_ms": 205, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 05:29:11*
