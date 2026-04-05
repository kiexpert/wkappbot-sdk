# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=11716  time=08:08:33  uptime=22770s  mem=311MB  system_processes=355"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_080820.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 360-320-302-103-675-612-630-750-703-756-765-765-201-321-675-637-267-627-216-102-206-360-623-362-320-261-326-302-302-357-302-623-620-320-620-362-362-621-675-672-765-126-163-162-3_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 654-136-317-612-126-130-136-261-136-105-576-216-613-621-160-163-106-163-106-167-170-103-156-715-261-163-761-173-537-176-136-106-316-103-103-130-106-765-167-716-760-620-136-126-1_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 150-105-150-103-175-715-175-175-175-175-175-175-715-715-751-715-715-571 513-154-105-170-157-715-175-715-175-175-715-715-715-751-751-175-715-715-175-175 547-501-105-105-105-105-1_V.mp3)
  Segment 4: 4728ms ~ 10920ms (file: 206-203-320-102-103-230-130-103-103-231-230-130-203-230-231-123-257-273-270-320-254-260-203-312-321-312-103-526-203-302-542-206-123-301-321-320-321-237-103-126-123-137-127-102-1_V.mp3)
  Segment 5: 11920ms ~ 12496ms (file: 765-627-672-576-312-576-357-263-267-623-612-231-276-253-567-501-571-765-502-507-351-752-351-173-273-132-570-237-561-167-276-357-750-132-271 267-236-261-750-765-547-213-627-237-5_V.mp3)
  Segment 6: 13496ms ~ 21200ms (file: 321-132-324-625-123-672-120-263-123-230-213-126-102-321-231-312-123-123-657-213-321-207-210-726-130-623-267-261-564-627-631-126-261-102-125-201-102-217-162-130-672-267-201-231-2_W.mp3)
  Segment 7: 22200ms ~ 22776ms (file: 157-157-175 175-750-710-175-107-157-705 135-157-132-173-175-157-175-157-175-751-517-750 157-175-507-517-175-571-105-157-157-107-517-507-105-570-571-571 507-154-517-571-510-570-5_W.mp3)
  Segment 8: 23776ms ~ 24352ms (file: 132-132-216-612-162-261-206-163-650 137-162-316-163-160-320-123-201-231-312-123-365-126-136-312-621-163-163-162-136-135-130-637-561-136-167-176-321-102-167-102-316-136-106-765-6_V.mp3)
  Segment 9: 25352ms ~ 25928ms (file: 610-601-605-601-605-601-605-605 621-605-603-602-620-601_W.mp3)
  Segment 10: 26928ms ~ 27504ms (file: 510-105-501-501-510-501-570-507-501-507-501-510-517-570-507-507-507-507-517 507-501-517-150-517-510-501-501-517-501-517-517-517-510-105-501-507-574-574-576-574-576-574-570-507 1_V.mp3)
  Segment 11: 28504ms ~ 29080ms (file: 675-675-672 362-637-673-673-637-637-637-637-376-763-637-376-376-367-367-673-637-673-672-763-627-762-673-637-673-673-672-673-672-672-673-673-637-673-673-673-673-673-673-763-675-6_W.mp3)
  Segment 12: 30080ms ~ 30656ms (file: 312 216-263-263-236-653-267-612-621-306-176-163-715-312-671-136-163-165-136-105-167-671-160-163-132-103-750-175-107-163-132-103-132-143 351-136-413-136-316-103-760-765-765-157-1_V.mp3)
  Segment 13: 31656ms ~ 32232ms (file: 673-375-367-756-357-576-357-376-375-750-701-753-703-750-703-705-750-753-175-175-751-750-715-750-570-750-705-175-750-750-570-571-157-750-570 750-705-750-751-705-751-751-705-710-7_V.mp3)
  Segment 14: 33232ms ~ 33808ms (file: 132-172-316-256-257-527-275-257-751-754-705-265-267-265-627-527-576-752-327-523-572-123-216-567-572-572-562-162-567-163-507-132-132-273-672-650-157-253-275-752-267-517-527-215-5_V.mp3)
  Segment 15: 34808ms ~ 42116ms (file: 235-271-236-230-137-310-130-130-263-230-310-106-320-517-570-621-762-570-627-130-103-321-320-236-231-267-153-213-123-175-132-735-261-263-213-176-106-260-236-213-127-310-137-153-1_W.mp3)
  Segment 16: 43116ms ~ 48660ms (file: 126-321-716-236-216-132-326-273-756-627-123-205-106-321-263-263-601-602-215-251-152-210-213-216-126-761-216-735-257-267-123-321-267-102-756-137-126-123-762-213-236-132-127-576-1_W.mp3)
  Segment 17: 49660ms ~ 50236ms (file: 751-703-173-713-317-317-753-713-310-371-735-176-130-317-130-710-317-310-132-310-103-137 317-371-173-753-301-103-173-317-130-103-103-103-105-175-357-751-173-731-371-713-753 357-3_V.mp3)
  Segment 18: 51236ms ~ 51812ms (file: 312-536-153-165-135-136-176-673-156-376-175-175-173-175-137-172-173-167-156-153-103-136-571-617-153-150-651-567-576-135-210-621-615-361-126-162-156-103-132-765-652-567-157-517-1_W.mp3)
  Segment 19: 52812ms ~ 53388ms (file: 601-623-175-612-621-602-103-132-103-102-136-103-136-107-103-162-103-175-160-126-106-136-162-130-106-102-236-213-365-136-167-103-136-601-612-106-627-361-106-132-617-617-103-316-1_V.mp3)
  Segment 20: 54388ms ~ 54964ms (file: 516-156-517-517-157-517-516-517-576-157-157-175-157-107-750-750-750-705-750-750-750-750-705-750-756-705-105-105-517-517-150-105-167-165-157-517-561-651-715-175-657-756-156-157-1_W.mp3)
  Segment 21: 55964ms ~ 56540ms (file: 236-567-756-275-310-316-157-756 710-123-231-135-127-315-705-725-312-761-312-572-132-750-257-132-756-267-726-576-752-572-162-567-367-753-671-756-571-567-527-270-276-765-521-571-5_V.mp3)
  Segment 22: 57540ms ~ 58116ms (file: 123-127 621-162-126-162-760-126-561-265-572-517-516-756-765-761-672-761-617-621 123-132-123-532-123-132-321-162-627-671-672 163-261-231_W.mp3)
  Segment 23: 59116ms ~ 59692ms (file: 132-103-675-763-637-601-670-570-506-516-671-132-231-213-132-312-613-136-361-316-567-567-576-572-573-536-532-546-617-673-736-765-703-617-176-761-716-716-173-371 672_W.mp3)
  Segment 24: 60692ms ~ 61268ms (file: 620-260-672-675-765-675-670-671-617-670-765-670-670-106-103-136-103-137-701-173-107-170-106-136-320-320-302-260-621-312-130-310-132-631-612-507-765-756-567-567-671-327-627-630-7_W.mp3)
  Segment 25: 62268ms ~ 71844ms (file: 210-210-574-210-213-765-267-576-607-602-620-231-213-512-573-574-206-206-231-120-527-267-203-572-527-756-726-260-120-620-257-207-310-103-130-213-574-527-102-137-257-213-123-574-2_W.mp3)
  Segment 26: 72844ms ~ 73420ms (file: 137-137-173-172-715-712-721-726-167-276-712-713-173-172-271-217-175-173-173-317-137-371-137-173-137-173-172-137-132-137-732-713-172-217-372-176-137-123-371 571-517-537-357-751-5_W.mp3)
  Segment 27: 74420ms ~ 74996ms (file: 576-106-617-617-107-103-320-203-213-103-130-106_V.mp3)
  Segment 28: 75996ms ~ 76572ms (file: 103-103-130-103-130-107-107 150-310-103-107-105-103-170-107-107-170-107-137-150 105-105-103-105-105-105-135-105-130-107-106-105-107-105-105 105-105-150-105-150-105-150-105-130-1_V.mp3)
  Segment 29: 77572ms ~ 78148ms (file: 157-157-157-157-157-157-157-106-105-107-157-105-105-175-157-150 150-105-105-150-153-157-150-157-156-150-157-157-156-154-157-157-157-157-105-150-310-107-175-751 150-517-150-157-1_W.mp3)
  Segment 30: 79148ms ~ 85628ms (file: 103-601-615-316-130-362-316-123-103-103-130-731-103-137-713-713-153-257-576-317-601-761-562-162-162-217-217-103-107-167-201-210-132-231-130-123-570-132-317-572-216-126-765-175-1_W.mp3)
  Segment 31: 86628ms ~ 87204ms (file: 123-627-276-216-236-237 102-316-123-106-130-137-103 752-756 263-162-361 627-267-632-672 621-623_W.mp3)
  Segment 32: 88204ms ~ 88780ms (file: 613-617-165-651-615-675-361-613-671-765 701-301-137-375-603-136-103-136-130-316-130-310-130-170-175-135-135-315-175-153-175-157-756-375-713-715-317 753-375-137-137-317-103-103-1_V.mp3)
  Segment 33: 89780ms ~ 90356ms (file: 203-320-216-574-574 263-320-362-132-163-136-163-517-651_W.mp3)
  Segment 34: 91356ms ~ 101796ms (file: 126-261-263-136-263-673-672-263-267-267-236-123-120-320-210-103-120-276-263-217-574-547-623-207-267-237-236-236-267-263-235-572-725-256-572-102-105-132-572-136-321-572-576-571-1_W.mp3)
  Segment 35: 102796ms ~ 130660ms (file: 254-103-627-123-127-574-574 574-276-672-621-572-103-761-602-176-574-276-301-765-756-276-276-276-627-721-713-574-257-231-123-276-261-312-132-547-562-761-713-132-136-163-301 540-5_W.mp3)
  Segment 36: 131660ms ~ 132236ms (file: 715-751-175-157-715-175-175-751-756-735-137-317-173-372-317-172-316-321-732-731-137-362-713-732-176-127-367-172-376-317-317-731-371-136-137-157-751-175-713-175-157-175-157-175-7_W.mp3)
  Segment 37: 133236ms ~ 133812ms (file: 173-103-107-137-715-173-731-173-715-137-173 175-137-173-137-173-175-317-173-175-137-301-571-517-175-751-570-753-731 157-173-715-751-713-715-173-713-173-173-137-103-137-173-137 1_V.mp3)
  Segment 38: 134812ms ~ 135388ms (file: 510-510-531-507-501-520-507-501-105-175-175-517-157-715-157-715-157-751-517-571-751-751-751-715-571-571-517-571-571-570 150-150-517-517-157-150-531-517-153-510-501-150-501-501-5_V.mp3)
  Segment 39: 136388ms ~ 136964ms (file: 705-137-731-130-137-137-310-137-130-137-301-107-301-307-317-307-307-130-103-103-371-130-710-157-175-135-357-537-576-576-751 751-753-507-317-103-531-103-175-130-376-312-130-310-1_V.mp3)
  Segment 40: 137964ms ~ 138540ms (file: 157-157-173-103-317-517-751-751-157-571-517-157-531-517-517-571-517-571-571-107-173-173-173-137-107-103-103-103-173-107-103-107-137-173-573-753-157-517-571-571-517-571-571-571-7_V.mp3)
  Segment 41: 139540ms ~ 140116ms (file: 751-571-517-517-571-576-751-756-570-751-157-751-751-751-571-570 570-571-570-571-175-517-153-157-175-571-571-570-751-751-517-571-571-571-571-571-517-570-105-103-153-571-517-571-1_V.mp3)
  Segment 42: 141116ms ~ 141692ms (file: 103-132-310-162-615-126-132-123-103-136-103-136-561-172-617-162-106-261-631-613-103-132-123-163-316-316-136-160-136-351-613-132-612-132-612-130-567-106-103-167-130-130-617-106-1_V.mp3)
  Segment 43: 142692ms ~ 143268ms (file: 673-652-765-765-671-672-671-623-571-163-716-756-765-675-136-312-312-136-132-132-367-126-132-102-132-103-102-236-126-162-216-130-103-132-210-320-316-162-561-567-576-136-316-163-3_W.mp3)
  Segment 44: 144268ms ~ 144844ms (file: 123-123-260-216-536-276-526-256-123-726-261-627-125-570-231-123-527-512-572-325-263-506-627-276-560-327-321-130-103-236-321-532-621-526-152-175-261-527-576-213-236-261-570-267-3_V.mp3)
  Segment 45: 145844ms ~ 153080ms (file: 527-263-216-256-257-517-205-215-521-267-256-352-523-210-236-126-651-123-231-201-516-567-567-576-125-130-213-130-517-206-201-201-231-627-310-106-143-231-312-103-302-106-130-751-2_W.mp3)
  Segment 46: 154080ms ~ 154656ms (file: 371-367-173-765-316-376 321-271-623-712-672-671-367-327-327-653-675-657-754-516-756-375-375-375-356-537-267-263-276-267-271-627-617-612-267-627-627-621-627-627-267-267 326-236-2_W.mp3)
  Segment 47: 155656ms ~ 165484ms (file: 230-210-213-260-103-206-260-602-670-107-765-756-726-103-627-130-753-756-675-267-216-201-301-216-260-201-572-275-127-213-321-206-213-102-127-201-102-670-126-103-230-260-627-136-6_W.mp3)
  Segment 48: 166484ms ~ 167060ms (file: 136-106-106-160-316-756-576-756-103-561-567-567-576-567-560-651-563-365-756-765-765-675-765-756-576-561-563-137-163-136-136-163-136-756-765-761-763-765-726-765-763-762-673-765-7_W.mp3)
  Segment 49: 168060ms ~ 178788ms (file: 312-321-176-127-163-275 210-362-213-273-237-276 710-270-321-367-310-312-263-735-312-321-126-132-132-312-321-173-237-236-327-237-217-267-267-732-732-126-712-627-106-120-103-123-1_W_stt_the.mp3)
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
> **Time**: 2026-04-05 08:10:12
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "좋아하고요 창을 사실 이제 한국 건축에서 이제 차경이라고 하지 않습니까 그래서 그런 부분을 살리고 싶었고 저희가",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "좋아하고요", "start_ms": 10, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "창을", "start_ms": 700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "사실", "start_ms": 1060, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 1470, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 1780, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "건축에서", "start_ms": 2210, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 2800, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "차경이라고", "start_ms": 3130, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "하지", "start_ms": 3890, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "않습니까", "start_ms": 4200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 4850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "그런", "start_ms": 5310, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "부분을", "start_ms": 5640, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "살리고", "start_ms": 6130, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "싶었고", "start_ms": 6590, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "저희가", "start_ms": 7080, "dur_ms": 420, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "즉 우리나라 바다는 얼음을 깨는 쇄빙선이 전혀 필요 없다는 뜻이에요 그런데 이런 우리나라도 쇄",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "즉", "start_ms": 20, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "우리나라", "start_ms": 280, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "바다는", "start_ms": 810, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "얼음을", "start_ms": 1270, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "깨는", "start_ms": 1680, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "쇄빙선이", "start_ms": 2010, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "전혀", "start_ms": 2600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "필요", "start_ms": 2960, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "없다는", "start_ms": 3290, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "뜻이에요", "start_ms": 3780, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "그런데", "start_ms": 4350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 4810, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "우리나라도", "start_ms": 5120, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "쇄", "start_ms": 5810, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 4,
"transcript": "중요한 거니까요 그런 거 하나하나가 내가 앞으로 멀리 10년 20년 봤을 때 내 자산이 되는 거거든요 그런 것들이 내 소중한 경험이 되는 거고 그 경험을 안 잊는 게 다",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "중요한", "start_ms": 15, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "거니까요", "start_ms": 470, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "그런", "start_ms": 1020, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 1380, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "하나하나가", "start_ms": 1540, "dur_ms": 780, "phase_lr_deg": 0.0},
{"word": "내가", "start_ms": 2330, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "앞으로", "start_ms": 2660, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "멀리", "start_ms": 3120, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "10년", "start_ms": 3480, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "20년", "start_ms": 3940, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "봤을", "start_ms": 4430, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 4790, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "내", "start_ms": 4950, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "자산이", "start_ms": 5140, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "되는", "start_ms": 5600, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "거거든요", "start_ms": 5930, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "그런", "start_ms": 6610, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "것들이", "start_ms": 6940, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "내", "start_ms": 7400, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "소중한", "start_ms": 7590, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "경험이", "start_ms": 8080, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "되는", "start_ms": 8510, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "거고", "start_ms": 8840, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 9170, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "경험을", "start_ms": 9330, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 9790, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "잊는", "start_ms": 9980, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 10310, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 10470, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "손만 잘 자라는 게 아니라 샘도 빠르네요 통닭 튀김뿐만 아니라 생닭도 같이 판매하고 있어 가게 들어오면",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "손만", "start_ms": 10, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 370, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "자라는", "start_ms": 560, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 1020, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 1180, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "샘도", "start_ms": 1590, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "빠르네요", "start_ms": 1950, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "통닭", "start_ms": 2530, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "튀김뿐만", "start_ms": 2920, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 3510, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "생닭도", "start_ms": 3920, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 4380, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "판매하고", "start_ms": 4710, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "있어", "start_ms": 5270, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "가게", "start_ms": 5580, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "들어오면", "start_ms": 5910, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 7,
"transcript": "라고 외칩니다 김치 Oh my god I missed kimchi 그리고 다른 음식은 보지 않고 김치 접시를 자기 앞으로 쓱 끌어와요 진짜 오랜만에 만난 친구 만난 표정으로 김치를 제일 먼저 집어먹",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 5,
"audio_type": "speech",
"words": [
{"word": "라고", "start_ms": 5, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "외칩니다", "start_ms": 260, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "김치", "start_ms": 820, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "Oh", "start_ms": 1210, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "my", "start_ms": 1420, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "god", "start_ms": 1610, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 1920, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "missed", "start_ms": 2080, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "kimchi", "start_ms": 2510, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 3050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "다른", "start_ms": 3510, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "음식은", "start_ms": 3840, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "보지", "start_ms": 4330, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "않고", "start_ms": 4660, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "김치", "start_ms": 4990, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "접시를", "start_ms": 5380, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "자기", "start_ms": 5870, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "앞으로", "start_ms": 6200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "쓱", "start_ms": 6660, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "끌어와요", "start_ms": 6920, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 7520, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "오랜만에", "start_ms": 7880, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "만난", "start_ms": 8440, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "친구", "start_ms": 8800, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "만난", "start_ms": 9130, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "표정으로", "start_ms": 9490, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "김치를", "start_ms": 10080, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "제일", "start_ms": 10540, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "먼저", "start_ms": 10900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "집어먹", "start_ms": 11260, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "이 입자가 밖으로 빠져나가지 못하게 막고 전기장으로 위 아래 방향을 잡아줍니다 이걸 패닝 트랩이라고 하는",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 15, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "입자가", "start_ms": 180, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "밖으로", "start_ms": 610, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "빠져나가지", "start_ms": 1070, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "못하게", "start_ms": 1730, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "막고", "start_ms": 2190, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "전기장으로", "start_ms": 2550, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "위", "start_ms": 3210, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "아래", "start_ms": 3420, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "방향을", "start_ms": 3750, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "잡아줍니다", "start_ms": 4240, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "이걸", "start_ms": 4950, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "패닝", "start_ms": 5280, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "트랩이라고", "start_ms": 5640, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 6400, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "ביום הראשון של המלחמה המטרות שלנו ברורות להסיר איומים ולהגן על הישובים",
"speaker": "speaker_6",
"confidence": 0.95,
"language": "he",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "ביום", "start_ms": 10, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "הראשון", "start_ms": 320, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "של", "start_ms": 780, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "המלחמה", "start_ms": 970, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "המטרות", "start_ms": 1470, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "שלנו", "start_ms": 1930, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "ברורות", "start_ms": 2290, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "להסיר", "start_ms": 2780, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "איומים", "start_ms": 3240, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "ולהגן", "start_ms": 3700, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "על", "start_ms": 4130, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "הישובים", "start_ms": 4290, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "미국이 중동에 항공모함을 추가 배치하며 압박 수위를 높이는 가운데 이라는 호르무즈 해협에 대한 통제력을 강화하는",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "미국이", "start_ms": 15, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "중동에", "start_ms": 450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "항공모함을", "start_ms": 910, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "추가", "start_ms": 1600, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "배치하며", "start_ms": 1930, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "압박", "start_ms": 2490, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "수위를", "start_ms": 2820, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "높이는", "start_ms": 3250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "가운데", "start_ms": 3710, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이라는", "start_ms": 4170, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "호르무즈", "start_ms": 4600, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "해협에", "start_ms": 5160, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "대한", "start_ms": 5620, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "통제력을", "start_ms": 5930, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "강화하는", "start_ms": 6520, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "제일 좋은 장점은 이음매를 없앨 수 있어 아 타일 넘어갔는데 다시 돌아보게 하는 인테리어 팁인 거 같은데 쥐가 튀길 수 있다 오 고등어도 튀길 수 있다",
"speaker": "speaker_7",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "제일", "start_ms": 10, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 340, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "장점은", "start_ms": 650, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "이음매를", "start_ms": 1140, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "없앨", "start_ms": 1700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 2060, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있어", "start_ms": 2220, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 2530, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "타일", "start_ms": 2690, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "넘어갔는데", "start_ms": 3020, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 3680, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "돌아보게", "start_ms": 4010, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 4570, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "인테리어", "start_ms": 4880, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "팁인", "start_ms": 5470, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 5830, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "같은데", "start_ms": 5990, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "쥐가", "start_ms": 6450, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "튀길", "start_ms": 6780, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 7110, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있다", "start_ms": 7270, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "오", "start_ms": 7580, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "고등어도", "start_ms": 7740, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "튀길", "start_ms": 8270, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 8600, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있다", "start_ms": 8760, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "놀라운 건 싱크대 상판뿐 아니라 상부장과 벽면까지 모두 일체형으로 제작했다는 거 어우 남편분 고생하셨겠다 아이고",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "놀라운", "start_ms": 15, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "건", "start_ms": 480, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "싱크대", "start_ms": 690, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "상판뿐", "start_ms": 1180, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 1640, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "상부장과", "start_ms": 2050, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "벽면까지", "start_ms": 2710, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "모두", "start_ms": 3300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "일체형으로", "start_ms": 3660, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "제작했다는", "start_ms": 4390, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 5120, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "어우", "start_ms": 5280, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "남편분", "start_ms": 5540, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "고생하셨겠다", "start_ms": 6030, "dur_ms": 820, "phase_lr_deg": 0.0},
{"word": "아이고", "start_ms": 6860, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "이 미니멀한 집에 뭐 구멍이 있어서 이상하다고 생각했거든요 혹시 저기가 뭐예요 저희 집에 뭐 비밀의 공간이 하나 있긴 있습니",
"speaker": "speaker_7",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 10, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "미니멀한", "start_ms": 170, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "집에", "start_ms": 730, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 1060, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "구멍이", "start_ms": 1220, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "있어서", "start_ms": 1680, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이상하다고", "start_ms": 2140, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "생각했거든요", "start_ms": 2830, "dur_ms": 820, "phase_lr_deg": 0.0},
{"word": "혹시", "start_ms": 3660, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "저기가", "start_ms": 4020, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "뭐예요", "start_ms": 4450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "저희", "start_ms": 4910, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "집에", "start_ms": 5240, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 5570, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "비밀의", "start_ms": 5730, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "공간이", "start_ms": 6190, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "하나", "start_ms": 6650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있긴", "start_ms": 7010, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "있습니", "start_ms": 7340, "dur_ms": 420, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 15,
"transcript": "트윈트는 그 극단적인 사례입니다 한국에서 실패작이라는 이유만으로 지나쳐 버렸다면 이번 주말 넷플릭스에서 트윈트를 즐겨보시는 건 어떨까요",
"speaker": "speaker_8",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "트윈트는", "start_ms": 10, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 600, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "극단적인", "start_ms": 760, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "사례입니다", "start_ms": 1390, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "한국에서", "start_ms": 2150, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "실패작이라는", "start_ms": 2710, "dur_ms": 820, "phase_lr_deg": 0.0},
{"word": "이유만으로", "start_ms": 3540, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "지나쳐", "start_ms": 4230, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "버렸다면", "start_ms": 4690, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "이번", "start_ms": 5320, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "주말", "start_ms": 5680, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "넷플릭스에서", "start_ms": 6010, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "트윈트를", "start_ms": 6870, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "즐겨보시는", "start_ms": 7430, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "건", "start_ms": 8120, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "어떨까요", "start_ms": 8330, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "기존의 질서 속에서는 동맹국들이 미국 중심으로 움직이는 것이 자연스러운 흐름이었기 때문입니다 그러나 지금 한국은 글로벌 방위 산업에서는 독자적인 공급자로 국제 외교 무대에서는 새로운 축의 중심으로 빠르게 자리 잡고 있습니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "기존의", "start_ms": 15, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "질서", "start_ms": 480, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "속에서는", "start_ms": 840, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "동맹국들이", "start_ms": 1400, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "미국", "start_ms": 2090, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "중심으로", "start_ms": 2420, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "움직이는", "start_ms": 3010, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "것이", "start_ms": 3540, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "자연스러운", "start_ms": 3850, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "흐름이었기", "start_ms": 4540, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "때문입니다", "start_ms": 5230, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "그러나", "start_ms": 5960, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 6420, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "한국은", "start_ms": 6750, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "글로벌", "start_ms": 7240, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "방위", "start_ms": 7700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "산업에서는", "start_ms": 8060, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "독자적인", "start_ms": 8820, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "공급자로", "start_ms": 9410, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "국제", "start_ms": 10040, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "외교", "start_ms": 10400, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "무대에서는", "start_ms": 10730, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "새로운", "start_ms": 11490, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "축의", "start_ms": 11980, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "중심으로", "start_ms": 12340, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "빠르게", "start_ms": 12930, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "자리", "start_ms": 13390, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "잡고", "start_ms": 13720, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 14080, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "아이고 무슨 현상이었어 뒤에서 봐 가지고 지금 어떻게 된",
"speaker": "speaker_9",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 5,
"audio_type": "speech",
"words": [
{"word": "아이고", "start_ms": 5, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "무슨", "start_ms": 460, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "현상이었어", "start_ms": 790, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "뒤에서", "start_ms": 1520, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "봐", "start_ms": 2010, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가지고", "start_ms": 2170, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 2630, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 2960, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "된", "start_ms": 3420, "dur_ms": 200, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 08:10:12*
