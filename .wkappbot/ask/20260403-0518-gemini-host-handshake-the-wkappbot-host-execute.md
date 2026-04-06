# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=05:17:18  uptime=14283s  mem=1468MB  system_processes=445"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_051708.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 625-260-260-260-260-260-620-621-162-675-675-160-650-261-162-213-612-162-671-716-231-106-612-615-106-615-165-136-136-130-652-615-625-163-215-136-650-165-163-621-163-567-726-126-1_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 173-137-167-167-167-163-167-167-176-617-617-765-517-617-716-675-675-675-675-675-675-765-675-675-675-675_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 236-263-236-267-237-216-370-306-136-103-217-310-210-310-126-103-106-136-153-750-715-163-132-123-126-103-173-176-206-210-675-765-670-762-136-106-136 754-750-706-765-132-263-617-6_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 630-623-306-631-326-627-627 372-321-230-206-123-203-201-103-102-102-521-250-165-160-106-102-130-572-367-536-362-613-267-672-627-675-672-625-716-576-672-573-105-106-213-130-310-1_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 537-537-765-132-236-623-362-751-576-574-657-756-765-574-574-567-756-754-751-756-105-715-756-617-563-365-765-673-615-526-651-261-176-621-276-762-765-652-657-653-615-576-763-576-5_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 201-261-106-216-163-263-130-102-316-132-312-105-105-201-712-103-163-126-167-162-106-106-103-231-320-231-167-176-106-136-162-126-256-260-302-263-162-163-126-302-316-312-127-201-2_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 263-126-356-102-621-162-106-163-103-162-621-261-627-620 501-574-506 261-627-263_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 627-627-652-652-625-627-361-136-536-253-263-263-623-267-621-216-652-265-265-351-351-316-231-132-312-562-256-236-256-253-526-567-265-263-263-263-263-263-265-625-261-265-216-213-2_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 612-106-617-160-612-165-106-617-167-160-162-613-612-106-267-261_V.mp3)
  Segment 10: 14184ms ~ 16560ms (file: 576-756-756-765-765-765-756-765-736-375-576-576-675-756-570-547-547-756-765-657-160-165-136-132-613-312-547-576-567-675-672-137-136-136-570-547-657-657-657_W.mp3)
  Segment 11: 17560ms ~ 18136ms (file: 261-263-713-136-632-326-126-162-167-167-612-213-160-610-610-621-627-276-132-106-136-136-173-751-175-517-517-751-752-175-541-514-574-574-574-547-157-675-657-567-756-765_W.mp3)
  Segment 12: 19136ms ~ 19712ms (file: 301-367-632-321-362-361-312-315-132-130-310-316-103-751-732-721-651-265-152-675-657-213-362-260-231-162-312-102-106-102-203-201-235-251-301-321-123-125-106-263-126-312-103-263-2_W.mp3)
  Segment 13: 20712ms ~ 21288ms (file: 162-162-126-172-265-567-156-167-167-137-123-716-517-651-765-567-567-561-631-251-265-156-123-512-165-632-163-621-612-637-316-137-163-213-721-326-136-327-637-261-167-126-652-652 7_W.mp3)
  Segment 14: 22288ms ~ 22864ms (file: 103-561-612-103-216-165-165-617-103-106-167 132-365-120-615-706-612-106-671-167-106-156-567-136-103-675-312-136-136-102-132-106-167-671-165-103-137-617-136-157-167-125-736-103-1_V.mp3)
  Segment 15: 23864ms ~ 25412ms (file: 675-675-576-675-657-367-136-137-162-326-132-356-273-612-361 316-367 574-576-263-623-326-673-670-637-365-376-673-657-163-672-160-670-136-361-162-316-130 236-231-312-231-123 162-1_W.mp3)
  Segment 16: 26412ms ~ 26988ms (file: 105-106-163-560-163-103-617-130-106-167 107-132-106-617-165-163-176-163-165-136-630-160-163-103-615-167-617-162-102-126-326-267-621-362-761-237 316_W.mp3)
  Segment 17: 27988ms ~ 28564ms (file: 236-123-136-163-162-574-751-175-130-301-310-260-236-260-310-301-571-625-657-612-312-236-235-236-203-623-754-576-765-236-213-130-102-320-320-320-321-132-360-361-312-103-627-126-1_V.mp3)
  Segment 18: 29564ms ~ 30140ms (file: 576-765-673-576-256-276 627-627-632_W.mp3)
  Segment 19: 31140ms ~ 31716ms (file: 671-103-137-103-317-132-136-136-103-326-302-310-130-301-312-301-765-763-130-312-103-103-316-103-132-623-316-621-203-163-106-102-106-106-201-206-201-302-301-312-301-130-103-103-1_V.mp3)
  Segment 20: 32716ms ~ 33292ms (file: 371-312-321-312-132-136-725-762-726-732-705-726-127-103-136-163-102-310-671-672-621-261-630-361-103-136-163-106-630-625-621-562-670-627-602-625-165-162-132-361 103-103-137-203-3_V.mp3)
  Segment 21: 34292ms ~ 34868ms (file: 735-675-261-627-367-103-163-361-316-132-132-621-263-132-263-261 765-216-136-361-617-631-136-150-567-103-106-106-167-651-316-160-165-176-132-615-167-126-123-157-615-675-765-623-6_V.mp3)
  Segment 22: 35868ms ~ 36444ms (file: 316-630-613-316-103-136-562-132-521-301-213-132-621-316-126-132-173-123-132-132-103-261-206-261-132-176-176-130-132-103-132-162-320-132-362-126-123-136-162-126-163-163-630-167-1_V.mp3)
  Segment 23: 37444ms ~ 38020ms (file: 657-651-561-516-567-576-567-576-356-267-267-627-627-612-637-617-167-361-617-671-671-576-576-576-567-567-536-560-356-375-237-327-236-231-216-267-275-257-265-567-625-256-526-526-5_W.mp3)
  Segment 24: 39020ms ~ 39596ms (file: 267-612-312-321 261-261-216 612-612 675-627-632-126-312-127-163-312-123-130-126 326-261-316-132-216-123-621-621-326-312-106-162-136 576-756-675-567-567-675-673-576-657-675-675-7_W.mp3)
  Segment 25: 40596ms ~ 41172ms (file: 672 657-657-176-316-162-361-261-326-621-675-671-672-637-675-617-672-627-132-136-132-612-672 276-276 756-756 102-321-576-756-726-765_W.mp3)
  Segment 26: 42172ms ~ 42748ms (file: 326-321 120-263-672-670-126-231-136-761-236-312-613-672-675-165-547-567-657-627-321-136-132-102-627-632-627-627-627-136-613-136-621-612-120-126-120-152-301-103-213-312-160-103-3_W.mp3)
  Segment 27: 43748ms ~ 44324ms (file: 172-132-136-136-310-325-132-103-153-125-321-123-230-237-321-173-275-205-253-213-302-603-671-672-675-367-572-537-325-326-362-632-263-320-132-357-315-213-123-132-327-132-321-132-3_W.mp3)
  Segment 28: 45324ms ~ 45900ms (file: 127 162-156-652-123-652-671-167-126-512-315-657-612_W.mp3)
  Segment 29: 46900ms ~ 47476ms (file: 612 576-576-571-576-574-537-756-756-765-765-760-103-321-263-236-362-326-361-362-673-361-362-306-326-316-312-132-167 765 570_W.mp3)
  Segment 30: 48476ms ~ 49052ms (file: 371-765-673-136-210-573-301-301-302 612-625-103-103-520-230-301-103-627-132-120-210-231-361-350-310-532-523-350-320-310-302-302-301-302-316-613-136-162-132-163-362-136-516-536-5_V.mp3)
  Segment 31: 50052ms ~ 50628ms (file: 263-213-235-261-235-356-351-352-521-216-231-216-213-512-352-532-512-512-152-573-526-257-257-257-157-152-517-517-532-652-320-357-357-523-520-521-531-523-563-567-527-532-571-527-2_W.mp3)
  Segment 32: 51628ms ~ 60268ms (file: 135-103-106-132-167-250-106-106-167-163-162-103-125-527-675-675-715-756-751-756-571-576_V.mp3)
  Segment 33: 61268ms ~ 61844ms (file: 136-126-612-162-162-672-612-627-627-675-756-675-567-157-620-260-162-162-126-136-603-603-603-136-312-230-123-136-570-507-301-312-206-213-326-612-103-136-206-206-206-256-627-672-6_V.mp3)
  Segment 34: 62844ms ~ 63420ms (file: 532-652-526-572-571-574-352-350-301-576-574-570-573-576-672-512-567-576-576-567-567-576-576-514-651-567-567-657-657-615-561-167-675-612-657-613-765-615-567-574-567-561-517-576-5_W.mp3)
  Segment 35: 64420ms ~ 64996ms (file: 675-671-675-615-506-637-361-362-376-631-106-162-657-137-573-751-754-750-750-570-576-130-170-175-517-517-130-130-136-617-605-316-107-231-362-621-630-132-102-601-620-621-601-621-6_W.mp3)
  Segment 36: 65996ms ~ 66572ms (file: 765-765-672-670-365-657-576-576-312 756-756-370-372-320-231-162-627-625-126-231-765-106-761-613-163-162-571-517-623-263-236-675-673-376-765-326-236-623-162 261-163-672-672-672-7_W.mp3)
  Segment 37: 67572ms ~ 68148ms (file: 625-236-627-310-123-621-623-267-762-276-672-617-765-571-651-167-176-761-567-567-572-162-126-103-160-136-130-610-162-172-137-162-126-612-672-672-632 631-362-137-136-316-537-567 1_W.mp3)
  Segment 38: 69148ms ~ 69724ms (file: 571-257-236-231-231-213-540-541-543-357-372-375-235-325-327-376-362-326-352-325-236-231-632-362-325-532-576-567-362-657-356-365-365-326-326-236-326-362-321-263-263-267-236-263-2_W.mp3)
  Segment 39: 70724ms ~ 71300ms (file: 103-527-157-162-716-162-160-127-165-751-136-103-317-627-672 617-125-103-167-126-132-137-173-167-132-130-673-163-623-160-256-127-261-123 675-637-160-367-512-362-617-126-675-215-1_W.mp3)
  Segment 40: 72300ms ~ 72876ms (file: 230-320-301-312-301-312-312-130-103-236-231-103-203-365-570-357-351-103-312 325-327-231-302-321-320-231-102-136-163-136-127-163-314-147-301-213-132-301-132-132-312-314-153-103-1_V.mp3)
  Segment 41: 73876ms ~ 74452ms (file: 735-756-617-162-316-631-167-617-671-617-675-672-652-657-610-621-672-621-652-103-107-160-613-610-160-621-160-612-567-156-561-571-165-316-163-126-163-617-162-167-126-162-216-627-1_W.mp3)
  Segment 42: 75452ms ~ 76028ms (file: 326-362-623-231-312-126-123-231-321-263-326-321-231-216-125-237-276-321-321-362-263-263-263-263-236-362-326-216-263-231-216-251-276-125-123-106-651-362-326-236-267-261-627-276-7_W.mp3)
  Segment 43: 77028ms ~ 77604ms (file: 621-612-201-206-260-206-627-103-312-206-263-123-312-132-310-632-365-765-263-320-236-231-203-263-206-206-320-103-301-312-316-132-213-263-267-301-206-574-265-206-206-265-316-312-3_V.mp3)
  Segment 44: 78604ms ~ 79180ms (file: 562-257-265-260 574-547 136-612-162-316-312-106-673-316-621-672-120-206-261_W.mp3)
  Segment 45: 80180ms ~ 80756ms (file: 762 765-671-124-765-675-675-675-670-607-673-672-673-135-312-321-361-103-173-132-123-601-610-132-162-103-103 103-103-136-312-132-312_V.mp3)
  Segment 46: 81756ms ~ 82332ms (file: 623-103-137-105-132-175-627-103-165-507-257-257-152-160-216-316-136-612-160-136-615-312-176-152-102-120-103-163-156-715-615-103-136-546-137-162-167-162-103-106-547-751-167-106-1_W.mp3)
  Segment 47: 83332ms ~ 83908ms (file: 651-652-615-517-513-510-106-162-126-162-167-672-162-167-162-126-167-617-601-760 126 602-106-607-103-162-103-236-601-160-617-136-157-162-602-162-657-621-176-621-163-126-103-571-1_V.mp3)
  Segment 48: 84908ms ~ 93080ms (file: 751-167-613-136-103-160-357-613-137-713-163-167-132-103-261-617-761-167_V.mp3)
  Segment 49: 94080ms ~ 94656ms (file: 752-765-756-675-576-756-765-752-762-726-603-136-756-107-107-107-621-106-170 103-132-301-302-103-102-301-371 576-267-103-167-136-132-136-632-503-150-516-312-103-106-136-106-173-1_V.mp3)
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
> **Time**: 2026-04-03 05:18:24
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
*Generated by WKAppBot ask gemini — 2026-04-03 05:18:24*
