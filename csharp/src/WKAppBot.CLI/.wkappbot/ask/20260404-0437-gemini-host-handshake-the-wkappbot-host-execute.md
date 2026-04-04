# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=14716  time=04:36:18  uptime=14935s  mem=759MB  system_processes=364"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260404_043532.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 103-136-615-567-571-276-756-652-176-130-157-317-130-617-631-103-136-361-765-167-316-167-163-516-135-502-316-136-361-317-136-163-136-136-136-610-153-103-136-103-132-516-136-136-3_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 351-105-537-513-570-571-130-103-137 162 321 657-236-672-672-627-627-627-627-627-623-627-627-672-627-627-627-672-672-627-627-267-627-267-576-106-301-130-312-123-132-162-165-162-6_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 573-357-621-632-163-103-132-123-751-125-137-136-103-163-106-316-136-103-136-103-167-106-167-106-107-103-126-630-652-136-136-106-651-362-312-167-103-176-130-512-263-267-267_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 102-163-173-162-176-176-765-162-136_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 132-326-162-672-672-763-765-675-765-167-136-361-632-365-137-137-263-620-362-671-637-623-356-562-615-125-312 576-675-627-562_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 675-176-671-167-176-136-135-126-301-103-106-103-263-261-125-517-517-517-571-127-216-732-726-715-731-271-721-217-257-765-671-765-172-127-765-672-756-765-756-756-756-567-567-537-6_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 163-631-360-362-361-256-236-361-623-326-263-263-267-261-265-265-625-623-362-236-623-625-362-263-265-625-257-265-265-265-265-265-623-265-236-325-357-357-576-657-267-625-267-325-2_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 615 167-610-765-617-167-163-163 716 761 574-132 236-321-312-326-321-236-321-213-321-321-321-317-317-315-531-536-321-306-317-371-312-673-367-370-763-160-610-613-613-632-672-367-7_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 523-132-312-316-136-326-352-526-156-351-123-213-567-357-536-365-135-351-326-132-231-312-136-136-325-213-321-123-231-321-260-267-216-261-325-265-235-263-352-321-256-567-265-205-3_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 106-126 132-613-326-271-276-126_V.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 312-162-106-236-265-265-236-231-312-132-351-152-361-136-637-361-361-136-136-361-162-162-326-625-632-352-235-625-263-261-263-625-675-657-657-657-163-623-162-531-562-657-576-537-2_W.mp3)
  Segment 12: 17336ms ~ 19280ms (file: 216-361-756-765-765-316-163-160-652-561-576-167 657-175-107-213-167-657-216-162-621-263-126-126-216-103-316-132-135-310-132-163_V.mp3)
  Segment 13: 20280ms ~ 20856ms (file: 326-137 531-132-617-156-136-132-653-612-103-321-316-132-103-103-163-571-315-165-106-267-517-103-103-751-165-160-136-310-135-136-136-103-136-610-132-106-103-613-165-136-136-163-3_V.mp3)
  Segment 14: 21856ms ~ 22432ms (file: 136-367-675-675-627-756-675-176-107-136-126-321-136-716-576-756-126-160-631-621-167-163-673-162-236-156-367-316-527-176-631-263-123-163-136-672-671-132-162-615-756-567-657-673-3_W.mp3)
  Segment 15: 23432ms ~ 24908ms (file: 673-316-312-103-132-132-507-573-705-573-574-571-571-312-301-315-321-504-504-531-152-123-125-132-315 203-102-362-123-735-735-327-230-320-301-302-320-356-106-162-126-675-607-670-2_V.mp3)
  Segment 16: 25908ms ~ 26484ms (file: 136-130-103-136-103-137-154-513-130-561-613 135-103-756-136-103-657-316-165-163-103-312-135-526-153-132-167-312-103-136-650-165-123-137-130-316-106-657-136-637-167-310-103-130-2_V.mp3)
  Segment 17: 27484ms ~ 28060ms (file: 135-512-327-215-157-175-375-321-207-207-275-270-752-675-765-325-263-235-273-327-325-325-352-725-532-573-527-754-125-123-372-675 162-621-671-127-215-236-105-153-231-106-531-352-3_W.mp3)
  Segment 18: 29060ms ~ 34028ms (file: 123-321-132-213-213-231-123-123-123-231-132-123-216-123-231-236-231-123-123-213-216-231-231-321-213-231-213-210-120-231-213-231-213-321-213-231-312-213-213-126-213-216-213-213-2_W.mp3)
  Segment 19: 35028ms ~ 35604ms (file: 361-607-632-271-206-326-620-132-362-312-762-762-726-276-276-276-276-276-267-273-273-267-275-257-251-165_W.mp3)
  Segment 20: 36604ms ~ 37180ms (file: 562-576-560-135-351-657-625-256-527-562-652-562-567-657-627-153-567-572-537-523-536-265-265-267-625-267-236-632-256-263-365-356-365-326-365-316-236-325-326-327-312-321-563-563-5_W.mp3)
  Segment 21: 38180ms ~ 38756ms (file: 763-765-761-763-672-675-765-756-751-756-175-675-765-763-765-751-765-621-621-263-756-756-756-765-765-756-756-756-765-756-753-754-765 756-750-167-735-756-765-765-765-761-761-172-1_W.mp3)
  Segment 22: 39756ms ~ 40332ms (file: 302-326-637-302-231-260-235-253-312-312-321-231-235-375-562-312-356-137-137-312-657-507-165-316-236-132-260-260-260-263-260-263-167-105-503-326-236-527-632-725-302-312-132-315-2_V.mp3)
  Segment 23: 41332ms ~ 41908ms (file: 103-160-601-106-601-136-163-102-160-610-106-567-163-316-601-132-103-317-103-132-125-130-103-103-102-106-130-621-160-123-132-120-163-312-310-132-612-601-136_V.mp3)
  Segment 24: 42908ms ~ 43484ms (file: 231-632-216-312-362-271-106-651-260-637-631-602-621-163-362-623-672-130-231-260-260-627-206-216-312-132-103-316-216-675-106-362-671-167-576-574-756-723-263-756-123-132-130-105-1_W.mp3)
  Segment 25: 44484ms ~ 45060ms (file: 163-657-163-160-160-312-312-160-517-612-167-502-506-625-617-162-160-165-612-762-136-567-561-160-623-623-126-106-651-612-621-316-163-123-236-261-623-567-761-176-137-761-106-162-5_W.mp3)
  Segment 26: 46060ms ~ 46636ms (file: 540-547-541-315-657-572-756-572-527-572-574-351-542-527-765-263-276-216-216-216-261-216-265-256-524-542-542-542-723-657-625-567-657-176-162-251-123-576-715-275-257-652-671-612-2_W.mp3)
  Segment 27: 47636ms ~ 48212ms (file: 167-271-132-103-106-671-316-631-162-352-136-571-675-756-765-675-317-671-361-312-576-675-376-571-623-123-372-756-103-173-756-756-367-762-567-763-576-675-632-623-657-236-762-162-5_W.mp3)
  Segment 28: 49212ms ~ 49788ms (file: 231-263-625-675-607-163-612-162-125-512-167-132-162-320-637-261-120-102-167-106-603-672-607-106-163-567-263-326-265-260-326-657-605-657-576-671-610-613 312-132-261-120-126-103-1_W.mp3)
  Segment 29: 50788ms ~ 51364ms (file: 136-107-762-365-315-361-103-312-263-361-126-137-160-153-630-376-612-136-546-176-136-136-263-126-721-712-276-762-103-763-631-136-160-103-176-132-167-136-175-152-610-361-612-670-1_V.mp3)
  Segment 30: 52364ms ~ 52940ms (file: 567-576-126-213 132 312-132-136-132-103-130-132_V.mp3)
  Segment 31: 53940ms ~ 64884ms (file: 301-206-312-230-201-302-261-213-217-130-102-216-236-302-130-302-312-136-310-260-267-130-261-130-216-216-103-103-106-621-160-312-263-321-203-260-260-132-260-213-540-130-160-132-5_V.mp3)
  Segment 32: 65884ms ~ 66460ms (file: 236-327-263 156-312-103-316-162-135-315-203-302-236-320-302-573-153-315-517-562-320-302-302-301-136-102-317-726-327-321-312-302-127-210-123-321-123-136-132-132-312-132-312-103-1_W.mp3)
  Segment 33: 67460ms ~ 68036ms (file: 136-316-213-216-267-216-203-261-612-716-671-672-756-756-763-765-670-761-627-672-762-630-627-671-672-673 610-126-213-234-230-201-263 137-213_W.mp3)
  Segment 34: 69036ms ~ 69612ms (file: 267-261-261-620-216-132 276-132 124-162-612-612-541-576-135-103-135-157-715-173-175-130-621-632-213-107 576-507-756-570-137-671-167-206-267-206-206-267-206-261-132-102-167-127-2_V.mp3)
  Segment 35: 70612ms ~ 71188ms (file: 136-620-206-236-306-123-136-312-132-136-103-106-106-673-756-365-631-362-301-362-321-251-562-621-231-123-126-102-126-132-312-316-153-167-103-102-156-536-513-576-567-576-576-671-7_W.mp3)
  Segment 36: 72188ms ~ 72764ms (file: 210-276-236-120-216-162-612-261-260-267-207-207-260-263-206-206-756-756-612-206-210-210-216-360-163-164-163-132-123-176-162-176-167-134-123-132 317-317-621-613-601-601-103-160-1_V.mp3)
  Segment 37: 73764ms ~ 83016ms (file: 230-231-130-627-260-276-130-103-612-312-310-312-230-321-267-236-257-267-267-256-267-267-267-130-103-267-712-210-231-132-132-263-321-231-230-310-102-231-260-312-130-123-130-312-3_W.mp3)
  Segment 38: 84016ms ~ 84592ms (file: 537-375-301-301-136-540-567-502-176-132-102-126-132-126-126-120-102-103-574-576-265-320-302-302-320-263-263-236-213-321-321-231-236-362-136-103-103-621-301-310-302-273-320-103-3_V.mp3)
  Segment 39: 85592ms ~ 86168ms (file: 576 761-716-726 627-326 213-231-321-160-103_W.mp3)
  Segment 40: 87168ms ~ 87744ms (file: 765-756-631-130-130-316-132-136-130-103-310-672-675-576-657-657-567-576-675-765-627-657-675-576-576-657-576-576-756-627-672-567-756-672-672-672-265-625-627-762-765-765-756-572-7_W.mp3)
  Segment 41: 88744ms ~ 98068ms (file: 102-265-256-123-260-576-256-261-216-210-321-236-120-201-213-231-263-213-263-120-765-267-276-217-670-207-267-627-216-260-123-260-267-602-721-261-627-320-756-765-265-210-263-267-2_W.mp3)
  Segment 42: 99068ms ~ 99644ms (file: 617-612-106-132-571-627-103-310-123-132-312-123 103-137-136-132-103-130-312 167-651-561-576-325-301-310-301-607-105-312-310-321-132-123-132-574-571-547-571-176-760-163-671-136-1_V.mp3)
  Segment 43: 100644ms ~ 101220ms (file: 103-716-106-627-176-612-106-126-105-126-107-156-106-163-172-517-156-103-603-176-613-102-756-130-167-162-103-132-103-103-103-103-162-126-167-103-175-126-632-106-163-165-106-617-1_V.mp3)
  Segment 44: 102220ms ~ 102796ms (file: 103-326-601-267-160-162-136-136-136-137-136-160-162-136-160-356-103-156-321-163 601-617-361-620-602-675-627-621-564-136-132-617-613-163-132-163-621-602-756-167-103-103-136-167-6_V.mp3)
  Segment 45: 103796ms ~ 104372ms (file: 301-316-537-362-132-312-163-132-163-132-312-413-132-143-132 760-765-761-762-762-671-167-612-312-130-103-621-671-167-671-671-627-263-236-760-765-675-675-126-102-263-263-276-762-3_V.mp3)
  Segment 46: 105372ms ~ 110268ms (file: 621-231-625-621-106-623-612-162-362-263-263-165-625-670-673-671-760-760-760-673-760-172-671-753-172-163-103-132-312-310-103-132-310-136-156-103-130-627-362-231-615-367-537-756-5_W.mp3)
  Segment 47: 111268ms ~ 111844ms (file: 103-123-132-103-301-316-136-312-126-135-102-132-126-136-160-106-213-761-312-163-165-167-631-130-102-103-160-136-135-175-176-165-136-106-651-136-163-651-162-103-163-106-160-152-2_V.mp3)
  Segment 48: 112844ms ~ 113420ms (file: 216-213-162-261-236-126-261-326-213-263-126-126 263-162-123-236-261-231-326-261-216-123-213-103_W.mp3)
  Segment 49: 114420ms ~ 123816ms (file: 132-263-261-261-654-623-236-231-365-135-125-231-315-375-367-357-315-123-123-132-135-123-231-312-310-320-136-321-102-106-312-326-260-237-267-260-263-261-576-265-260-260-260-260-2_W.mp3)
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
> **Time**: 2026-04-04 04:37:33
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "아시아의 가장 선진적인 산업 파트너 중 하나로 자리 잡고 있다 하지만 이런 안정감은 오히려 함정이 될 수 있다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "아시아의", "start_ms": 50, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 570, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "선진적인", "start_ms": 970, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "산업", "start_ms": 1570, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "파트너", "start_ms": 1920, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "중", "start_ms": 2370, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "하나로", "start_ms": 2520, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "자리", "start_ms": 2920, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "잡고", "start_ms": 3220, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "있다", "start_ms": 3470, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "하지만", "start_ms": 4200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 4650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "안정감은", "start_ms": 4950, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "오히려", "start_ms": 5550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "함정이", "start_ms": 5950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "될", "start_ms": 6400, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 6550, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있다", "start_ms": 6650, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "오늘날 세계에서는 관계의 중요성이 전통이나 지리적 가까움이 아니라 위기 상황에 함께 대응할 수 있는 능력으로 결정된다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "오늘날", "start_ms": 80, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "세계에서는", "start_ms": 530, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "관계의", "start_ms": 1230, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "중요성이", "start_ms": 1630, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "전통이나", "start_ms": 2180, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "지리적", "start_ms": 2780, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "가까움이", "start_ms": 3230, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 3730, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "위기", "start_ms": 4130, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "상황에", "start_ms": 4480, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "함께", "start_ms": 4930, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "대응할", "start_ms": 5230, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 5680, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 5780, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "능력으로", "start_ms": 6030, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "결정된다", "start_ms": 6580, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "잘 관리되고 있지만 구조적으로 중요한 관계는 아닌 상태라면 그 관계는 결국 뒤로 밀려날 수밖에 없다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "잘", "start_ms": 100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "관리되고", "start_ms": 300, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "있지만", "start_ms": 850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "구조적으로", "start_ms": 1250, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "중요한", "start_ms": 1900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "관계는", "start_ms": 2300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "아닌", "start_ms": 2750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "상태라면", "start_ms": 3050, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 3600, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "관계는", "start_ms": 3700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "결국", "start_ms": 4100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "뒤로", "start_ms": 4450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "밀려날", "start_ms": 4750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "수밖에", "start_ms": 5200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "없다", "start_ms": 5600, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "양국이 서로를 전략",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "양국이", "start_ms": 120, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "서로를", "start_ms": 570, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "전략", "start_ms": 970, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "다시 이렇게 하루 한 시간에 만 원씩 버는 노동을 비하하는 게 아니고 제가 할 수 있을까",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "다시", "start_ms": 150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "하루", "start_ms": 900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 1250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "시간에", "start_ms": 1400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "만", "start_ms": 1800, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "원씩", "start_ms": 2000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "버는", "start_ms": 2300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "노동을", "start_ms": 2600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "비하하는", "start_ms": 3050, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 3600, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "아니고", "start_ms": 3700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 4250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 4600, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 4800, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있을까", "start_ms": 4900, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "저는 이미 도박쟁이의 삶의 굴도 많이 있는 건데 제 인생",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이미", "start_ms": 550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "도박쟁이의", "start_ms": 850, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "삶의", "start_ms": 1550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "굴도", "start_ms": 1900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 2300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 2650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "건데", "start_ms": 2950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "제", "start_ms": 3400, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "인생", "start_ms": 3600, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "미리 설명드린 내용입니다 이번 종목은 LIG넥스원입니다 이건 초보자분들이 진짜 많이 틀리는 부분입니다",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 300,
"audio_type": "speech",
"words": [
{"word": "미리", "start_ms": 300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "설명드린", "start_ms": 650, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "내용입니다", "start_ms": 1200, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "이번", "start_ms": 2000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "종목은", "start_ms": 2350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "LIG넥스원입니다", "start_ms": 2800, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "이건", "start_ms": 4000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "초보자분들이", "start_ms": 4350, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 5150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 5550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "틀리는", "start_ms": 5900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "부분입니다", "start_ms": 6350, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "상단 부근을 돌파할 때 매수하면 단기 수익을 낼 가능성이 매우 높죠",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "상단", "start_ms": 100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "부근을", "start_ms": 450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "돌파할", "start_ms": 850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 1300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "매수하면", "start_ms": 1450, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "단기", "start_ms": 2100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "수익을", "start_ms": 2450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "낼", "start_ms": 2850, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가능성이", "start_ms": 3000, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "매우", "start_ms": 3550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "높죠", "start_ms": 3900, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "이 역시 어제 오늘 네프콘에서 아침",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 150, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "역시", "start_ms": 250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "어제", "start_ms": 600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "네프콘에서", "start_ms": 1300, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "아침", "start_ms": 2000, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "부터가 다리 관절을 움직이게 하는 건데요 이때 로봇 제어의",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "mixed",
"words": [
{"word": "부터가", "start_ms": 50, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "다리", "start_ms": 500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "관절을", "start_ms": 850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "움직이게", "start_ms": 1300, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 1850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "건데요", "start_ms": 2150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이때", "start_ms": 2700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "로봇", "start_ms": 3050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "제어의", "start_ms": 3400, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "님께서 길이 엇갈리기 전에 저희가 어서 마중 나가야 합니다 We better hurry so we don't miss each other",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "님께서", "start_ms": 80, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "길이", "start_ms": 530, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "엇갈리기", "start_ms": 880, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "전에", "start_ms": 1480, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "저희가", "start_ms": 1830, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "어서", "start_ms": 2280, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "마중", "start_ms": 2630, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "나가야", "start_ms": 2980, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "합니다", "start_ms": 3430, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "We", "start_ms": 4000, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "better", "start_ms": 4200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "hurry", "start_ms": 4550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "so", "start_ms": 4950, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "we", "start_ms": 5150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "don't", "start_ms": 5300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "miss", "start_ms": 5600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "each", "start_ms": 5900, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "other", "start_ms": 6150, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 13,
"transcript": "흐름이 나올 가능성이 높은지 이걸 구조 중심으로 정리해서 공유드리고 있습니다",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "흐름이", "start_ms": 120, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "나올", "start_ms": 570, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "가능성이", "start_ms": 920, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "높은지", "start_ms": 1520, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이걸", "start_ms": 2100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "구조", "start_ms": 2450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "중심으로", "start_ms": 2800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "정리해서", "start_ms": 3400, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "공유드리고", "start_ms": 4000, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 4700, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "불편한 거 같으니까 하지 말라고 했으면 그 다음부터 저도 뭔가를",
"speaker": "speaker_6",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "불편한", "start_ms": 100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 550, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "같으니까", "start_ms": 700, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "하지", "start_ms": 1400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "말라고", "start_ms": 1700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "했으면", "start_ms": 2150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 2700, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "다음부터", "start_ms": 2800, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "저도", "start_ms": 3350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "뭔가를", "start_ms": 3650, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 16,
"transcript": "엄청 많아요 그리고 계속 계속 메뉴가 바뀌면서 나와요 이게 떨어지면 다른 메뉴가 나와",
"speaker": "speaker_7",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "엄청", "start_ms": 50, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "많아요", "start_ms": 450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 1000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 1450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 1800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "메뉴가", "start_ms": 2150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "바뀌면서", "start_ms": 2600, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "나와요", "start_ms": 3200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 3750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "떨어지면", "start_ms": 4050, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "다른", "start_ms": 4600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "메뉴가", "start_ms": 4900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "나와", "start_ms": 5350, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "디자인하는 데 어 좀 더 초점이 맞춰져 있고요 또 저희가 이런 그 보행 로봇에 좀 특이한 뭐 기능이라든지 뭐 특이한 성능을 또 얻기 위해서는 로봇도 직접 설계하고 디자인하고 제작도 하고 있",
"speaker": "speaker_8",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "디자인하는", "start_ms": 100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "데", "start_ms": 700, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "어", "start_ms": 950, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 1150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 1350, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "초점이", "start_ms": 1550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "맞춰져", "start_ms": 2000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "있고요", "start_ms": 2450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 3000, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "저희가", "start_ms": 3200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 3600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 3900, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "보행", "start_ms": 4050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "로봇에", "start_ms": 4400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 4950, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "특이한", "start_ms": 5150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 5600, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "기능이라든지", "start_ms": 5750, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 6650, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "특이한", "start_ms": 6800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "성능을", "start_ms": 7250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 7700, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "얻기", "start_ms": 7900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "위해서는", "start_ms": 8250, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "로봇도", "start_ms": 8900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "직접", "start_ms": 9350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "설계하고", "start_ms": 9750, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "디자인하고", "start_ms": 10350, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "제작도", "start_ms": 11000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "하고", "start_ms": 11450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "있", "start_ms": 11750, "dur_ms": 200, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-04 04:37:33*
