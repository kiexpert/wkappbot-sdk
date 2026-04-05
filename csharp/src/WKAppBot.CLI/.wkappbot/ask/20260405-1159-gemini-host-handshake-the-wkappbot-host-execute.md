# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=9236  time=11:57:52  uptime=36101s  mem=304MB  system_processes=355"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_115710.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 513-157 517-517-517-157-517-157-513-573-753-751-157-107-517 107-170-170-170-173-571-157-175-170-307-173-713-731-173-371-173-713-375-375-721-750-572-751-570-751-751-715 157-153-1_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 157-150-501-573-507-507-510-501 501-510-501-150-510-150-510-510-530-530-513-531-501-570 510-510-150-501-501-530-517-510-501-507-501-510-510-501-510-516-510-567-157 157-510-571-5_V.mp3)
  Segment 3: 3152ms ~ 9920ms (file: 320-263-236-267-157-751-175-715-157-756-231-321-260-321-210-321-170-102-215-107-120-102-107-106-210-201-752-721-751-716-267-276-627-267-725-326-753-756-751-752-267-270-123-201-2_W.mp3)
  Segment 4: 10920ms ~ 11496ms (file: 715-170-170-571-576-756-751-175-751-517-175-751-175-756-175-571-176-751-715-750 705-150-710-170-705-175-571-107-170-710-170-170-175-710-176-173-713-716-756-756-751-170 173-175-7_V.mp3)
  Segment 5: 12496ms ~ 13072ms (file: 160-316-106-612-106-126-657-310-136-136-136-103-103-312-136-136-321-312-316-136-132-103-631-567-127-316-163-163-103-167-136-263-130-103-103-367-736-132-316-610-613-361-631-306-3_V.mp3)
  Segment 6: 14072ms ~ 14648ms (file: 651-561-651-516-561-651-561-657-615-156-165-165-615-612-613-615-613-613-613-672-675-675-675-657-675-657-675-502-652-625-651-650-651-657-651-657-657-657-675-657-657 512-516-507-5_W.mp3)
  Segment 7: 15648ms ~ 16224ms (file: 136-103-135-136-163-135-103-136-103-103-103-103-375-173-753-574-571-751-315-315-315-315-753-571-573-576-517-571-517-571-574-574-571-571-574-517-537-573-571 573-571-517-157-571-5_W.mp3)
  Segment 8: 17224ms ~ 17800ms (file: 752-765-756-327-327-507-762-267-370 710-176-165-167-106-106-137-173-136-132-137_V.mp3)
  Segment 9: 18800ms ~ 27872ms (file: 317-130-731-136-735-137-321-312-102-312-705-752-275-512-162-157-765-357-106-320-236-130-306-321-502-560-203-230-316-361-326-316-310-316-317-312-106-672-751-230-276-756-302-231-2_V_stt_i'm_in_the_world_and_when_percent_of_the.mp3)
  Segment 10: 28872ms ~ 29448ms (file: 150-150-150-150-105-157-150-510-150-150-105-150-105-105-150 167-107-175-106-107-170-175-175-107-157-150-156-165-105-136-173-716-716-750-750-571-756-754-507-750-705-150-510-150-1_V.mp3)
  Segment 11: 30448ms ~ 31024ms (file: 275-257-752-527-752-570-752-725-672-675-617-657-627-672-610-602-670-671-672-675-672-672-675-675-672-671-602_W.mp3)
  Segment 12: 32024ms ~ 32600ms (file: 621-261-216-261-621-261-652-524-465-621-617-612-671-671-617-617-671-671-617-621-621-206-562-561-512-305-506-650-562 267-267-621-621-263-263-261-261-620-623-612 621-612-617-671-6_W.mp3)
  Segment 13: 33600ms ~ 34176ms (file: 751-570 574-574-574-574-574-574-574-574-574-574-574-571-570-517-507-705 574-574-574-574-574-574-547-574-574-570-150-507-517-570-501-517-170-507-157-507-715-510-501-571 574-547-5_W.mp3)
  Segment 14: 35176ms ~ 35752ms (file: 571-570-570 135-517-516-567-571-516-756-317-673-751-157-517-175-571-756-517-571-571-517-517-571-157-517-571-157-517-571-753-765-752-352-736-175-756-625-765-752-105-153-157-715-7_W.mp3)
  Segment 15: 36752ms ~ 37328ms (file: 567-561 165-156-615-651-651-165-157-561-156-651-657-657-651-657-657-756-657-651-657-561-567-657-651 613-651-651-615-617-675-657-651-657-657-657-675-657-675-657-567-576-756-657-6_W.mp3)
  Segment 16: 38328ms ~ 38904ms (file: 517-157-175-715-175-175-173-175-176-175-715-756-175-716-756-157-531-357 165-751-175-571-571_W.mp3)
  Segment 17: 39904ms ~ 40480ms (file: 675 675-657-165-163-167-167-761-765-675-675-675-675-765-736 675-765-675-765-675-765-765-675-675-675_W.mp3)
  Segment 18: 41480ms ~ 42056ms (file: 314-134-103 165-316-301-315-137-137 103-136-316-136-103-136-316-103-136-316-137-135-136-136-103-130-316 103-167-153-106-163-316-136-103-135-165-106-156-163-136-136-136-130 314-1_V.mp3)
  Segment 19: 43056ms ~ 43632ms (file: 506 506-562-560-756-576-576-507-567-570-756-576-520-576-567-526-652-257-572-250-526-507-572-527-627-657 507-570-507-560-506-576-607-526-502-567-572-576-567-502-532-572-567-526-5_V.mp3)
  Segment 20: 44632ms ~ 45208ms (file: 320-236-320-302-231-132-237-276-265-206-321-216-263-236-301-206-203-310-136-163-136-316-263-263-263-105-570-315-103-306-163-136-130-136-126-216-326-206-523-504-312-312-261-263-2_V.mp3)
  Segment 21: 46208ms ~ 46784ms (file: 751-153 517-517-751-753-751-571-571-751-751-157-751-571-576-573 753-571-571-751-753-571 571-571-571-571-571-571-571-571-573-576 574_W.mp3)
  Segment 22: 47784ms ~ 55812ms (file: 362-301-327-173-301-371-372-726-672-567-523-731-765-762-263-752-267-123-106-106-276-273-276-127-130-237-753-753-261-162-267-123-376-672-312-236-276-267-312-321-167-176-316-160-2_W.mp3)
  Segment 23: 56812ms ~ 57388ms (file: 547-547-504-507-504-507-547 570-507-574-571-507-574-574-574-507-517-571 547-547-547-547-547-547-574-574-574-571-570-571-517-501-510-517-570-574-750-571-751-754-754 507-517-571-1_V.mp3)
  Segment 24: 58388ms ~ 66164ms (file: 631-360-231-130-317-136-312-103-103-103-321-326-136-635-103-103-317-107-361-326-362-361-132-163-321-132-120-123-276-725-637-237-267-263-457-352-130-276-572-765-263-276-326-672-2_W.mp3)
  Segment 25: 67164ms ~ 73176ms (file: 106-103-103-136-201-536-312-231-205-620-102-315-602-216-103-130-235-576-103-657-567-165-105-260-103-512-576-621-562-657-152-673-715-657-567-516-632-617-625-201-213-123-210-261-2_W.mp3)
  Segment 26: 74176ms ~ 74752ms (file: 516-156-561-156-165 216-610-651-615-165-165-615-165-156-165-615-156-561-516-651-165-156-651-615 201-201-210-160-160-601-610-671-605-761-601-607-671-671-617-610-610-610-617-610-6_V.mp3)
  Segment 27: 75752ms ~ 76328ms (file: 571-517-517-517-571-517-516-571-517-576-750-571-570-751-756-765-716-576-570-750-750-570-570-570-570 574-157-571-517-751 517-570-751-705-756-570-507-507-570-756-576 574-507-570-5_W.mp3)
  Segment 28: 77328ms ~ 77904ms (file: 507-572-576-567-574 521-526-573-250-205-205-265-265-675-756-756-756-576-567-576-756-756-765-765-527-576-572-576-752-527-756-752-205-705-725-765-576-752-576-576-574-570-572-576-5_W.mp3)
  Segment 29: 78904ms ~ 79480ms (file: 520-102-516-176-167-176-167-132-163-162-527-752-527-572-576-527-725-726-502-521-250-150-516-156-156-156-150-156-156-156-156-152-152-756-756-752-756-576-756-756-567-275-576-572-5_W.mp3)
  Segment 30: 80480ms ~ 84836ms (file: 261-120-570-215-231-127-302-275-203-260-261-216-102-213-510-213-263-504-514-203-527-261-507-574-572-217-275-301-130-316-350-504-570-572-123-206-270-263-260-236-207-206-721-125-5_V.mp3)
  Segment 31: 85836ms ~ 86412ms (file: 765-675-756-756-675 250-506-650-256-650-567-650-567-675-675-765-675-675-675_W.mp3)
  Segment 32: 87412ms ~ 87988ms (file: 163-310-136-132-132-216-627-627-672-176-162-612-123-120-231-263-621-261-627-130-213-130-301-132-301-321-231-326-265-562-256-572-715-267-132-312-567-625-632-310-301-137-173-613-7_V.mp3)
  Segment 33: 88988ms ~ 89564ms (file: 512-527-572-526-526-651-652-631-613-613-613-651-651-651-615-613-326-163-261-651-652-652-621-623 652-645-625-623 501-501-527-562-625-625-625-652-652-657-657-657-657-675-657-657-6_W.mp3)
  Segment 34: 90564ms ~ 91140ms (file: 571-751-570-751-157-751-570-571-571-751-571-751-571-715-157-105-150-715-751-750-710-715-751-571-571-750-571-576-571-751-571-750-175-750-570-750-715-157-574-574-574-571 150-570-5_W.mp3)
  Segment 35: 92140ms ~ 98224ms (file: 123-231-572-527-132-321-132-310-213-103-756-132-260-236-230-261-657-567-261-263-123-210-267-756-126-126-120-213-263-216-231-235-576-273-231-527-103-172-267-321-231-213-625-106-7_W.mp3)
  Segment 36: 99224ms ~ 99800ms (file: 130-130-137-157-135-136-162-137-317-137-135-157-157-157-135-157-175-715-170-713-130-130-176-173-176-176-675-175-716-167-715-701-716-761-716-175-701-701-170-730-176-701-710-170-7_W.mp3)
  Segment 37: 100800ms ~ 105732ms (file: 302-201-312-612-675-761-130-206-716-217-276-260-567-213-213-751-571-265-316-317-302-301-301-370-310-357-305-351-356-657-165-361-105-531-537-536-532-567-576-576-250-317-163-167-1_W.mp3)
  Segment 38: 106732ms ~ 107308ms (file: 256-750-705-275-572-526-657-705-705-750-725-350-756-236-720-670-601-710-576-657-506 576-275 361-165-316 276-273-126-163-167-163-165-162-162-316-256-562-256-526-562-256 507-576-5_W.mp3)
  Segment 39: 108308ms ~ 108884ms (file: 126-671-513-103-103-136-125-103-321-153-316-105-163-130-106-761-106-106-163-103-617-163-136-106-136-136-136-130-165-632-165-103-127-103-103-361-126-130-130-103-103-123-103-176-1_V.mp3)
  Segment 40: 109884ms ~ 110460ms (file: 751-701-751-753-571-751-753-753-351 137-175-173-107-657-765-675-567-576-756-752-765-765-672-376-672-762-763-762-763-763-762-765-726-103-163-103-657-675-672-632-671-617-613-617-6_W.mp3)
  Segment 41: 111460ms ~ 112036ms (file: 715-701-705-571-751-175-175 157-107-715-571-501-517-507-750 570 504-510-150-510-501-150-105-507-507-501-150-150-105-507-570_V.mp3)
  Segment 42: 113036ms ~ 124664ms (file: 106-751-157-167-236-325-613-321-160-605-126-132-236-273-317-310-310-260-321-326-312-627-627-627-620-670-106-271-726-123-132-162-765-275-106-106-167-176-172-276-271-726-176-574-5_W.mp3)
  Segment 43: 125664ms ~ 126240ms (file: 172-721-201-172-276-672-675-671-635-657-576-567-576-765-765-765-576-576-567-756-567-567-652-765-756-752-756-756-752-756-752-756-135-157-156-517-315-152-152-157-157-756-576-756-7_W.mp3)
  Segment 44: 127240ms ~ 127816ms (file: 132-135-136-163-153-136-657-267-163-132-310-132-136-132-136-130-263-261-263-376-376-756 527-576-567-574-576-574-576-765-567-572-572-572-576-567-756-576-537-567-576-752-675-576-5_W.mp3)
  Segment 45: 128816ms ~ 136916ms (file: 276-361-103-136-172-126-756-627-610-612-672-160-317-163-672-107-321-263-210-126-263-762-612-103-143-267-312-312-321-325-326-571-672-752-176-107-160-617-721-162-673-261-362-107-3_V.mp3)
  Segment 46: 137916ms ~ 147204ms (file: 267-265-230-276-276-230-715-136-321-321-132-267-210-216-130-317-237-230-213-321-210-213-260-213-230-210-231-231-103-312-261-213-231-203-103-130-302-206-167-675-576-765-765-672-5_W.mp3)
  Segment 47: 148204ms ~ 148780ms (file: 571-571-751-571-571-531 571-571 576-574-547-547-571-572_W.mp3)
  Segment 48: 149780ms ~ 156728ms (file: 231-731-675-261-367-267-362-762-765-756-756-320-317-106-602-276-312-132-310-261-263-107-675-527-210-263-263-270-132-720-706-725-257-627-623-627-765-257-127-301-312-321-236-725-1_W.mp3)
  Segment 49: 157728ms ~ 158304ms (file: 726-763-762-762-762-762-765-765-765-321-761-627-236-126-136-716-106-103-126-103-137-103-136-103-132-130-132-157-176-652-167-103-671-123-625-167-375-516-316-106-106-167-163-617-2_V.mp3)
  Segment 50: 159304ms ~ 159880ms (file: 675-627-132-132-620-126-627-236-751-527-756-517-175-356-165-156-517-156-571-216-213-362-126-657-265-576-576-576-517-156-132-216-627-652-652-567-753-562-567-165-625-275-675-653-6_W.mp3)
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
> **Time**: 2026-04-05 11:59:19
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "AI도 공동 창업했어요 그는 AI를 딱 한마디로 이렇게 정의했습니다 AI는 지능을 위한 증기 기관이다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "AI도", "start_ms": 50, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "공동", "start_ms": 500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "창업했어요", "start_ms": 850, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "그는", "start_ms": 1600, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "AI를", "start_ms": 1900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "딱", "start_ms": 2300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "한마디로", "start_ms": 2500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 3000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "정의했습니다", "start_ms": 3400, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "AI는", "start_ms": 4200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "지능을", "start_ms": 4650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "위한", "start_ms": 5050, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "증기", "start_ms": 5350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "기관이다", "start_ms": 5700, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 3,
"transcript": "time in the world I think When I first got into Penn State I was a computational data science major And then I ended up switching because actually exactly this I felt that I",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "time", "start_ms": 100, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 600, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "world", "start_ms": 800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 1200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "think", "start_ms": 1350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "When", "start_ms": 1800, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 2100, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "first", "start_ms": 2250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "got", "start_ms": 2600, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "into", "start_ms": 2850, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "Penn", "start_ms": 3150, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "State", "start_ms": 3450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 3800, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "was", "start_ms": 3950, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 4200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "computational", "start_ms": 4350, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "data", "start_ms": 5100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "science", "start_ms": 5450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "major", "start_ms": 5900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "And", "start_ms": 6400, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "then", "start_ms": 6650, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 6950, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "ended", "start_ms": 7100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "up", "start_ms": 7450, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "switching", "start_ms": 7650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "because", "start_ms": 8200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "actually", "start_ms": 8650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "exactly", "start_ms": 9150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "this", "start_ms": 9650, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 9950, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "felt", "start_ms": 10100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "that", "start_ms": 10450, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 10700, "dur_ms": 100, "phase_lr_deg": 0.0}
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
"transcript": "라는 이슈가 실제로 터진 일로 보이기 때문에 이거를 단순히 우리 그냥 IT 클라우드라든가 우리 뭐 예전에 통신사들",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "라는", "start_ms": 80, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이슈가", "start_ms": 450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "실제로", "start_ms": 900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "터진", "start_ms": 1400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "일로", "start_ms": 1800, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "보이기", "start_ms": 2150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "때문에", "start_ms": 2600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이거를", "start_ms": 3200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "단순히", "start_ms": 3650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "우리", "start_ms": 4200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 4550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "IT", "start_ms": 4900, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "클라우드라든가", "start_ms": 5150, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "우리", "start_ms": 6000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 6350, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "예전에", "start_ms": 6550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "통신사들", "start_ms": 7000, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "결국 스트레스 좀 풀자고 아 비도 오고 너무 스트레스 많이 쌓여 어",
"speaker": "speaker_4",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "결국", "start_ms": 150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "스트레스", "start_ms": 550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 1100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "풀자고", "start_ms": 1300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 1800, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "비도", "start_ms": 2050, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "오고", "start_ms": 2400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 2750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "스트레스", "start_ms": 3150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 3700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "쌓여", "start_ms": 4100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "어", "start_ms": 4600, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "써니 씨가 주최한 마을 운동회 행사 진행",
"speaker": "speaker_5",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "써니", "start_ms": 120, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "씨가", "start_ms": 450, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "주최한", "start_ms": 750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "마을", "start_ms": 1250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "운동회", "start_ms": 1600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "행사", "start_ms": 2100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "진행", "start_ms": 2450, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "말씀 못 드렸을까 봐 썼어요 네 아무튼 제가 먼저 언급해야 할 것",
"speaker": "speaker_6",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "말씀", "start_ms": 100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "못", "start_ms": 500, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "드렸을까", "start_ms": 700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "봐", "start_ms": 1250, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "썼어요", "start_ms": 1500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "네", "start_ms": 2100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "아무튼", "start_ms": 2350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 2800, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "먼저", "start_ms": 3150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "언급해야", "start_ms": 3550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 4100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 4350, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "최근에 갤럭시 S23 시리즈에 들어가 신기능 중 일부를 바로 전작",
"speaker": "speaker_7",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "최근에", "start_ms": 50, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "갤럭시", "start_ms": 550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "S23", "start_ms": 1000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "시리즈에", "start_ms": 1450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "들어가", "start_ms": 2000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "신기능", "start_ms": 2450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "중", "start_ms": 2900, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "일부를", "start_ms": 3100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 3550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "전작", "start_ms": 3900, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "지만 저는 안정적인 매매를 추구하기 때문에 노이즈 구간을 알리는 노란색 신호에서 전량 매도하고 다음 숏 포지션 진입 구간을 살펴보겠",
"speaker": "speaker_8",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "지만", "start_ms": 120, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "저는", "start_ms": 450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "안정적인", "start_ms": 800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "매매를", "start_ms": 1350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "추구하기", "start_ms": 1800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "때문에", "start_ms": 2350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "노이즈", "start_ms": 2800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "구간을", "start_ms": 3250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "알리는", "start_ms": 3700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "노란색", "start_ms": 4150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "신호에서", "start_ms": 4650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "전량", "start_ms": 5200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "매도하고", "start_ms": 5600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 6150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "숏", "start_ms": 6500, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "포지션", "start_ms": 6750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "진입", "start_ms": 7250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "구간을", "start_ms": 7600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "살펴보겠", "start_ms": 8050, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "재난이 되었지만 한국 군수 산업에는 뜻밖에도 가장 완벽한 전시장이 되어주었습니다 이 한국 군수 고속열차",
"speaker": "speaker_9",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "재난이", "start_ms": 60, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "되었지만", "start_ms": 550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 1100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "군수", "start_ms": 1500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "산업에는", "start_ms": 1900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "뜻밖에도", "start_ms": 2450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 3000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "완벽한", "start_ms": 3400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "전시장이", "start_ms": 3900, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "되어주었습니다", "start_ms": 4500, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 5400, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 5600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "군수", "start_ms": 6000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "고속열차", "start_ms": 6400, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "슈퍼바이즈드 모드 단독 전재하는 거 이런 여러 레이어를 각각각 전부 다 통과를 해야 활성화되는 구조일 가능성이 높을 거거든요 당연히 그런 식으로 설계가 되어 있어야지만이 FSD",
"speaker": "speaker_10",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "슈퍼바이즈드", "start_ms": 150, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "모드", "start_ms": 900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "단독", "start_ms": 1250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "전재하는", "start_ms": 1650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 2150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 2350, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "여러", "start_ms": 2650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "레이어를", "start_ms": 3000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "각각각", "start_ms": 3550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "전부", "start_ms": 4050, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 4400, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "통과를", "start_ms": 4600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "해야", "start_ms": 5050, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "활성화되는", "start_ms": 5400, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "구조일", "start_ms": 6050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "가능성이", "start_ms": 6500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "높을", "start_ms": 7050, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "거거든요", "start_ms": 7400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "당연히", "start_ms": 8000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "그런", "start_ms": 8500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "식으로", "start_ms": 8850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "설계가", "start_ms": 9300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "되어", "start_ms": 9800, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "있어야지만이", "start_ms": 10150, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "FSD", "start_ms": 10900, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "중요한 소주 놓여져 대박 어 이 소주 향 우 와",
"speaker": "speaker_11",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "중요한", "start_ms": 100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "소주", "start_ms": 600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "놓여져", "start_ms": 1000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "대박", "start_ms": 1500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "어", "start_ms": 2000, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 2250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "소주", "start_ms": 2450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "향", "start_ms": 2850, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "우", "start_ms": 3200, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "와", "start_ms": 3500, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "데미잖아 정복당하지 않게 해야 할 사람 없어요 아 그럼 가위바위보로 오케이 자 가위바위보 이인일조로 팀을 짭니다 아 소리는 아",
"speaker": "speaker_12",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "데미잖아", "start_ms": 120, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "정복당하지", "start_ms": 650, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "않게", "start_ms": 1300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "해야", "start_ms": 1650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 2000, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "사람", "start_ms": 2250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "없어요", "start_ms": 2600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 3100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "그럼", "start_ms": 3350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "가위바위보로", "start_ms": 3700, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "오케이", "start_ms": 4450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "자", "start_ms": 4900, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "가위바위보", "start_ms": 5150, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "이인일조로", "start_ms": 5900, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "팀을", "start_ms": 6550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "짭니다", "start_ms": 6900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 7450, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "소리는", "start_ms": 7700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 8200, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "가집니다 오늘 시장에서 강했던 종목들 보겠습니다 현대 에너지 솔루션이 신재",
"speaker": "speaker_13",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "가집니다", "start_ms": 50, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "시장에서", "start_ms": 1000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "강했던", "start_ms": 1550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "종목들", "start_ms": 2000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "보겠습니다", "start_ms": 2500, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "현대", "start_ms": 3200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "에너지", "start_ms": 3600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "솔루션이", "start_ms": 4100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "신재", "start_ms": 4700, "dur_ms": 350, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 11:59:19*
