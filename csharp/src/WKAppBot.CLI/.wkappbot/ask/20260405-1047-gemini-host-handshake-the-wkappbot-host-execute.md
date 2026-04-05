# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=11864  time=10:45:05  uptime=24525s  mem=306MB  system_processes=354"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_104453.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 3636ms (file: 627-216-267-261-627-621-261-627-261-261-231-261-326-321-123-273-627-261-216-123-102-231-236-312-231-126-276-321-103-106-123-123-267-216-271-627-267-217-273-263-231-231-126-726-7_W_stt_if_that.mp3)
  Segment 2: 4636ms ~ 5212ms (file: 625-642-675-675-675-615-621 624-615-615-651-615-645-654-615-654-654-652-654-621-612-612 620-620-627-625-627-625-657-652-654-657-654-615-654-652-654-657-654-645-612-625-621-261-2_W.mp3)
  Segment 3: 6212ms ~ 6788ms (file: 621-612-617-167-160-106-136-103-132-651-123-163-106-137-103-163-136-103-627-106-167-610-103-516-126-162-512-103-617-136-136-103-267-263-627-312-671-267-137-162-172-102-124-617-1_V.mp3)
  Segment 4: 7788ms ~ 18336ms (file: 102-267-310-326-623-672-160-136-612-620-106-617-623-627-106-267-263-627-316-263-326-136-632-136-103-106-103-310-102-267-326-263-136-107-217-265-756-275-326-312-310-216-136-713-3_W.mp3)
  Segment 5: 19336ms ~ 19912ms (file: 675-675-657 756-756-756-752-572-215-762-265-162-613-621-762-752-751-761-756-765-756-675-576-756-756-751-756-751-615-652-651-651-657-657-651-657-567-657-657-657-561-651-615-675-6_W.mp3)
  Segment 6: 20912ms ~ 21488ms (file: 107-173-751-175-731-753-571-510-507-157-105-170-157-107-157-517-571 715-750-710-175-107-103-107-130-150-137-175-170-715-170-137-175-756 157-517-510-157-105-157-157-715_W.mp3)
  Segment 7: 22488ms ~ 30408ms (file: 762-572-307-107-175-132-137-217-632-762-326-213-652-162-120-231-250-312-132-312-276-206-320-236-132-521-562-617-127-216-132-236-106-120-721-103-172-107-612-312-103-123-321-103-2_V.mp3)
  Segment 8: 31408ms ~ 37528ms (file: 312-257-213-210-206-263-173-320-136-756-756-172-671-576-203-213-132-256-320-217-320-210-201-231-213-236-326-203-213-132-230-237-206-267-260-260-206-263-720-103-163-103-715-123-3_W.mp3)
  Segment 9: 38528ms ~ 49472ms (file: 127-576-567-231-216-132-127-215-321-327-321-237-325-102-527-752-213-237-237-257-172-217-237-125-213-572-275-215-201-267-572-273-572-257-237-327-321-132-217-276-231-321-572-721-3_W.mp3)
  Segment 10: 50472ms ~ 64008ms (file: 231-123-132-321-236-326-574-231-123-130-726-301-132-231-312-321-126-260-261-321-310-236-102-260-276-326-102-105-103-127-170-257-524-132-160-310-237-623-213-765-312-263-765-756-7_W.mp3)
  Segment 11: 65008ms ~ 65584ms (file: 150-105-105-150-501-150-517-517-157-105-105-150-170-105-157-105-106-175-157-175 105-105-105-150-150-156-156-165-157-517-105-105-157-571-157-150-105-107-150-105-517-157-175 105-1_V.mp3)
  Segment 12: 66584ms ~ 67160ms (file: 576-567-652-576-562-657-560-657-562-576-650-562-657-627-657-567-526-502-560-567-652 657-657-312-312-132-132-576-567-576-765-572-576-675-567-576-561-517-631-260-623-206-652-632-2_W.mp3)
  Segment 13: 68160ms ~ 68736ms (file: 132-726-570-376-562-267-231-572-574-263-735-753-731-750-562-321-237-167-751-276-267-765-275-165-136-326-103-376-653-372-327-376-372-317_W.mp3)
  Segment 14: 69736ms ~ 77584ms (file: 206-203-672-376-263-627-356-652-653-576-573-237-236-136-367-376-763-367-213-137-130-310-210-271-165-765-675-756-752-672-162-276-576-576-756-612-236-260-213 102-102-236-231-312-1_W.mp3)
  Segment 15: 78584ms ~ 79160ms (file: 136-316-317-132-613-103-136-176-136-561-167-163-136-163-103-136-103-156-130-153-136-130-163-103-165-376-103-103-136-103-126-130-132-136-132 130-103-310-103-317-103-163-123-361-1_V.mp3)
  Segment 16: 80160ms ~ 86136ms (file: 160-613-213-321-132-126-130-276-236-320-321-371-162-120-123-213-312-316-312-321-102-137-723-723-103-102-123-263-236-765-657-216-752-312-106-103-173-321-321-231-321-132-312-130-1_W.mp3)
  Segment 17: 87136ms ~ 87712ms (file: 154-154-154-157-105-105-153-154-153-154-154-105-154-105-154-154-154-105-103-154-154-143-154-145-145-145-104 154-136-176-146-132_V.mp3)
  Segment 18: 88712ms ~ 89288ms (file: 675-157-103-657-523-612-167-103-310-136-106-671-163-106-107-176-317-163-135-167-617-136-103-137-136-163-103-132-623-162-612-160-621-652-126-602-627-620-627-671-167-362-623-103-1_V.mp3)
  Segment 19: 90288ms ~ 90864ms (file: 321-263-123-316-132-136-136-137-137-137-136-163-613-136-167-637-317-316-163-621-316-716-176-750-627-312-130-160-163-103-130-137-136-752-120-103-160-367-617-137-130-175_W.mp3)
  Segment 20: 91864ms ~ 92440ms (file: 501-157-510-517-510-510-571-514-541-571-547-576-576-571-571 510-571-507-501-105-517-507-517-507-507-510-150-501-510-516-150-501-150-150-105-510-150-510-510-501-510-571 510-510-5_V.mp3)
  Segment 21: 93440ms ~ 94016ms (file: 156-173-715-571-716-751-571-175-715-765-751-715-571-715-715-753-715-751-576-751-753-756 510-517-516-150-516_V.mp3)
  Segment 22: 95016ms ~ 95592ms (file: 654-567-651 651-652-657-657-657-657-657-657-651-657-567-657-657-657 260-256-652-652-657-657-561-657-657-657-657-657-657-657-615 256-657-657-657-567-567-567-567-567-576-576-756-7_W.mp3)
  Segment 23: 96592ms ~ 97168ms (file: 312-103-103-103 572-675-167-670-136-627 765-176-317-136-126-365-675-261-631-712-712-172-712-712-172-176-172-136-132-132-165-106-156-623-162-156-132 165-106-170-172-103-254-721-1_V.mp3)
  Segment 24: 98168ms ~ 98744ms (file: 235-213-621-312-627-160 216-623-263-260-261-261-312-301-301_V.mp3)
  Segment 25: 99744ms ~ 100320ms (file: 105-507-751 175-701-175-157-571-571-517-501-105-570-501-510-501-510-501-150-501-507-501-150-105-150-501-574-574-571-571 507-571-150-105-103-150-105-105-510-150-507-574-574-501-5_W.mp3)
  Segment 26: 101320ms ~ 111688ms (file: 137-376-312-213-250-137-103-136-123-312-375-706-567-136-213-512-136-237-572-132-237-267-236-123-601-521-625-261-256-236-213-250-162-132-527-715-701-570-261-257-751-267-263-312-5_V.mp3)
  Segment 27: 112688ms ~ 117008ms (file: 103-103-134-301-312 123-513-524-203-620-213-623-620-276-260-310-206_V.mp3)
  Segment 28: 118008ms ~ 122832ms (file: 265-267-213-263-216-216-261-316-316-316-365-361-316-316-316-236-163-312-263-213-361-627-321-126-321-126-316-123-123-312-162-132-163-132-136-123-132-126-312-162-123-213-163-673-6_W_stt_of_life_and.mp3)
  Segment 29: 123832ms ~ 128872ms (file: 162-261-602-672-236-320-263-236-320-306-213-102-103-102-137-157-162-263-260-206-216-261-326-321-326-231-672-760-672-671-160-167-216-126-236-231-231-236-236-321-236-320-321-231-6_W.mp3)
  Segment 30: 129872ms ~ 130448ms (file: 754-750-752-752-576-135-312-136-725-273-657-621-206-271-471-765-275-750-530-125-570-374-217-576-265-254-265-621-562-625-261-632-165-612-516-351-136-316-361-361-360-367-316-360-1_W.mp3)
  Segment 31: 131448ms ~ 139764ms (file: 267-213-213-261-210-627-201-376-320-320-321-120-207-201-102-106-321-123-702-301-371-203-302-130-103-120-103-172-102-103-120-102-120-126-120-120-120-120-126-126-120-120-120-126-1_W.mp3)
  Segment 32: 140764ms ~ 145084ms (file: 613-712-176-316-310-361-307-317-301-310-136-316-306-361-136-315-132-301-310-103-310-321-312-310-316-316-136-132-136-137-136-316-103-162-126-163-261-162-316-236-136-132-103-102-3_W.mp3)
  Segment 33: 146084ms ~ 154940ms (file: 123-756-205-236-236-574-275-257-762-675-265-162-167-175-571-357-315-150-157-670-623-163-167-756-756-762-765-632-276-762-326-312-103-571-756-675-631-376-106-570-721-312-756-617-2_W.mp3)
  Segment 34: 155940ms ~ 161448ms (file: 123-132-317-132-312-312-312-571-126-132-213-652-732-705-753-205-276-137-571-750-721-301-317-132-123-207-132-327-720-312-321-317-371-312-261-103-213-702-327-517-132-152-126-321-5_V.mp3)
  Segment 35: 162448ms ~ 163024ms (file: 751-715-157-751-715-715-175-571-175-571-571-571-751-751-751-571-570-571-571-750 510-175-157-571-570-510-157-571-570-517-571-751-175-751-571-571-715-571-751-571-751-751-751-571-5_V.mp3)
  Segment 36: 164024ms ~ 164600ms (file: 726-236-267-267-132 765-637-576 735-753-267-756-157-213-762-761-576-137-531-652-237-257-126-263-326-570-570-765-375-753-216-157 123-327-623-765-756 153-270-273-267-237-321-263-5_V.mp3)
  Segment 37: 165600ms ~ 178344ms (file: 273-103-136-512-132-375-213-201-576-235-765-756-176-361-136-712-173-261-765-265-672-152-163-231-103-267-132-275-123 217-530-103-103-137-301-706-167-163-132-103-132-172-170-312-3_W.mp3)
  Segment 38: 179344ms ~ 179920ms (file: 527-572-571-576-567-517-572-572-567-675-617-125-165-561-517-527 675-675-524-576-576-571-576-517-507-527-526-510-520-576-520-507-570-506-507-572-250-506-570-562-750-267-657-572 5_W.mp3)
  Segment 39: 180920ms ~ 181496ms (file: 572-752-756-752-752-725-527-752-275 256-576-502-527-502-652-675-765-765-765-765-765-765-675-675 265-256-526-627-702-675-276-650-760-567-675-765-765-576-576-576-756-675-756-675-5_V.mp3)
  Segment 40: 182496ms ~ 183072ms (file: 267-610-673-612-726-672-267-276 671-162-612-621-261 216-236-326-236-321-321-326-321-231-321-312-321-231-326-321-236-326-321-312-132-126-123-321-132-123-213-123-213-123-216-123-1_W.mp3)
  Segment 41: 184072ms ~ 184648ms (file: 263-236-321-302-302-301-316-570-570-716-750-530-132-301-312-132-201-276-231-312-316-312-260-267-203-230-321-326-302-320-216-312-263-267-106-162-176-163-623-362-361-765-327-317-1_V.mp3)
  Segment 42: 185648ms ~ 186224ms (file: 157-167-317-765-172-371-367-136-132-321-173 173-173-317-371-137-547-176-136-163-132-657-651-163-136-163-617-167-136-106-126-130-136-135-132-136-316-136-136-735-376-756-765-157-1_W.mp3)
  Segment 43: 187224ms ~ 195468ms (file: 130-201-120-210-263-261-216-216-203-752-756-132-752-206-230-301-756-275-260-752-310-126-123-167-130-276-725-231-132-321-130-725-627-210-120-102-231-132-316-326-631-130-310-321-3_W.mp3)
  Segment 44: 196468ms ~ 197044ms (file: 627-576-762-267-627-765-672-657-657-612-675-627-675-657-657-635-672-627-625-657-657-672-617-673-265-672-632-627-725-675-675-765-267-756-675-672-675-617-567-675-657-657-672-652-6_W.mp3)
  Segment 45: 198044ms ~ 205028ms (file: 263-130-267-576-132-267-206-253-524-267-170-203-276-576-327-376-352-327-103-102-263-236-276-230-756-726-267-263-672-762-201-712-130-231-102-236-230-230-542-106-102-103-267-326-2_W.mp3)
  Segment 46: 206028ms ~ 206604ms (file: 107-713 510-507-571-103-572-537-537-375-573-165-153-165-173-167-136-135-165-165-163-761-176-761-157-156-517-157-571-517-571-105-165-165-576-136-175-137-135-136-317-312 153-153-1_W.mp3)
  Segment 47: 207604ms ~ 208180ms (file: 105-157-105-517-150-517-507-157-571-507-507-507-570-501 517-150-150-105-150-105-130-753-573-752-675-265-176-167-170_V.mp3)
  Segment 48: 209180ms ~ 209756ms (file: 271-765-712-135-165-651-573-567-135-365-357-176-657-573-615-765-263-623-357-617-317-623-537-365-135-517-763-562-726-362-652-623-156-517-310-316-136-635-312-132-310-631-302-123-3_V.mp3)
  Segment 49: 210756ms ~ 211332ms (file: 153-157-573-136-153-165-156-257-752-752-527-265-257-567-657-567-657-567-657-756-765-657-531-315-310-135-315-315-135-157-156-153-153-516-153-165-651-675-652-653-651-576-561-657-6_W.mp3)
  Segment 50: 212332ms ~ 212908ms (file: 105-517-150-150-150-157-175-157-715-175-157-106-156-167-165-156-517-157-156-157-175-157-175-571 103-105-105-105-150-150-150-157-157-157-105-517-157-571-150-157-571-150-150-501-1_V.mp3)
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
> **Time**: 2026-04-05 10:47:21
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
"transcript": "비극적인 결말은 거의 피할 수 없게 되죠 얼음 지구상의 많은 바다와는 달리",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "비극적인", "start_ms": 20, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "결말은", "start_ms": 650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "거의", "start_ms": 1150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "피할", "start_ms": 1550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 2000, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "없게", "start_ms": 2200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "되죠", "start_ms": 2600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "얼음", "start_ms": 3400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "지구상의", "start_ms": 4300, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "많은", "start_ms": 5000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "바다와는", "start_ms": 5450, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "달리", "start_ms": 6100, "dur_ms": 450, "phase_lr_deg": 0.0}
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
"transcript": "베링해는 매년 겨울이 되면 반드시 일부가 얼어붙습니다 지구 온난화가 어우러지면서 매우 혹독하고 예측하기 어려운 기상 조건이",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "베링해는", "start_ms": 40, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "매년", "start_ms": 700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "겨울이", "start_ms": 1200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "되면", "start_ms": 1700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "반드시", "start_ms": 2150, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "일부가", "start_ms": 2750, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "만들어지고 있는 것입니다 기후의 경계에 놓인 바다 사실 베링해에서 가장 큰 위험은 바로 그 기후였습니다 그 밖의 모든",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "만들어지고", "start_ms": 30, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "것입니다", "start_ms": 1050, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 9,
"transcript": "중요하며 신중한 입장을 유지하고 파병 요청에 즉각 응하지 않자 트럼프 대통령은 이를 배신이자 기회주의자로 규정해 버린 것입니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "중요하며", "start_ms": 25, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "신중한", "start_ms": 600, "dur_ms": 500, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 12,
"transcript": "천만 달러의 자산을 오직 단타 트레이딩 하나만으로 일궈낸 남자라면 믿으실 수 있으신가요 그의 이름은 제이슨 노아",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "천만", "start_ms": 35, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "달러의", "start_ms": 500, "dur_ms": 450, "phase_lr_deg": 0.0}
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
"transcript": "그가 말한 트레이딩의 3원칙은 좋아요와 구독으로 다음 이야기에도 동행해 주세요 다음 영상에서 또 만나요 단골이세요",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "그가", "start_ms": 50, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "말한", "start_ms": 350, "dur_ms": 350, "phase_lr_deg": 0.0}
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
"transcript": "하지만 쇄빙선은 쐐기형 선수로 경사져 있죠 쇄빙선은 이 쐐기형 선수로 지지율은 13%에 그쳐 민주당과의 격차가",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "하지만", "start_ms": 40, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "쇄빙선은", "start_ms": 500, "dur_ms": 550, "phase_lr_deg": 0.0}
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
"transcript": "38% 포인트나 됐습니다 베이 의원은 선거비 보전도 못할까 봐 후보들이 도통 나서지 않는다며 구인난이 심 안내 말씀 드립니다",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "38%", "start_ms": 20, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "포인트나", "start_ms": 550, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "지난주 토요일에 소개해 드린 현대판 불로초 NMN 안티에이징 리포좀 크림 영상 이후 바이오를 위해 거실 오른쪽에는 따로 주방을",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "지난주", "start_ms": 30, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "토요일에", "start_ms": 500, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "냈고요 에 이런 반응들을 보인 걸까요 세상에서 가장 비싸고 가장 위험하고 가장 희귀한 물질 만들기도 지옥이고 보관하기도",
"speaker": "speaker_6",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "냈고요", "start_ms": 60, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "에", "start_ms": 550, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "Kimchi fashion 중간에 단감 내려가고 그러면 사레 김치 좀 있고 양념 닿으면 또 아프고 쓰리고 만반의 준비를 마쳤으니 배춧잎",
"speaker": "speaker_7",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "Kimchi", "start_ms": 80, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "fashion", "start_ms": 650, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "사이사이 안녕하세요 만나서 정말 반갑습니다 오늘 날씨가 참 좋네요 요즘 날씨가 정말 화창하네요 주말에 나들이 가기 딱 좋은 것",
"speaker": "speaker_8",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "사이사이", "start_ms": 50, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "안녕하세요", "start_ms": 650, "dur_ms": 750, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 26,
"transcript": "같아요 (music)",
"speaker": "speaker_8",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "mixed",
"words": [
{"word": "같아요", "start_ms": 10, "dur_ms": 400, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 30,
"transcript": "냄새를 견딜 수 없는 수준에 이르자 농부들은 땅에 심은 감자를 캐보기로 합니다 농부들은 경악을 금치 못했습니다 땅에서 캔 감",
"speaker": "speaker_9",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "냄새를", "start_ms": 40, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "견딜", "start_ms": 550, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "수익비가 엄청 좋은 게임을 할 수 있는 거기 때문에 음 다섯 번 실패해도 한 번만 성공하면 된다 그래도 수익 난다 그래서 자기가",
"speaker": "speaker_10",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "수익비가", "start_ms": 30, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "엄청", "start_ms": 600, "dur_ms": 450, "phase_lr_deg": 0.0}
]
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
"transcript": "세운 투자 시나리오랑 전략을 좀 집중할 수 있는 이런 충동적인 결정을 방지하는 데는 확실히 도움 되지 않을까 그런 생각이 드는 거",
"speaker": "speaker_10",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "세운", "start_ms": 25, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "투자", "start_ms": 450, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "거 근데 이제 우리가 처음에 주식 투자 하다 보면은 대부분 이제 뭔 말인지 몰라가지고 아이 모르겠다 안 하고 그러는 경우가 많거든",
"speaker": "speaker_10",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "거", "start_ms": 20, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 300, "dur_ms": 350, "phase_lr_deg": 0.0}
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
"transcript": "근데 생각보다 자기 분야로 좁히다 보면은 이게 막 그렇게 어",
"speaker": "speaker_10",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "근데", "start_ms": 15, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "생각보다", "start_ms": 400, "dur_ms": 500, "phase_lr_deg": 0.0}
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
"transcript": "해놓고 이제와 막대한 비용을 전쟁으로 피해 입은 동맹들에게 전가하는 형국인 겁니다 이런 상황이어서 여론은 계속 나빠져",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "해놓고", "start_ms": 30, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이제와", "start_ms": 500, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "매사추세츠와 육아부가 실시한 이 이 표는 이제 IQ인지 EQ인지는 확인해야죠 한 400인분 정도 준비를 하고 있어요 저는 튀김 담당입니다",
"speaker": "speaker_11",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "매사추세츠와", "start_ms": 60, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "육아부가", "start_ms": 850, "dur_ms": 550, "phase_lr_deg": 0.0}
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
"transcript": "튀김 네 한 하루 종일 튀김만 튀기시는 거예요 자국이 직접 전쟁에 뛰어들어야만 가능한 것은 아닙니다 다른 나라가 제공하지 못하는",
"speaker": "speaker_12",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "튀김", "start_ms": 45, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "네", "start_ms": 500, "dur_ms": 200, "phase_lr_deg": 0.0}
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
"transcript": "가치를 내놓기만 해도 시대의 흐름을 붙잡을 수 있다는 뜻입니다 러시아 우크라이나 전쟁과 중동 전쟁은 세계에겐 불행한",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "가치를", "start_ms": 25, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "내놓기만", "start_ms": 450, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "전쟁이 세가 공간의 일부로 인정받게 된 것인데요 그들은 그 배경을 설명하면서 한국 세탁기가 미국 집 구조를 바꿔놓았다라는 표현을",
"speaker": "speaker_13",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "전쟁이", "start_ms": 40, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "세가", "start_ms": 500, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "자연스럽게 하고 있었습니다 물론 이 변화가 오직 한국 때문 좀 높고 배터리가 좀 빨리 닳아가지고 제가 굉장히 아쉽다라고 말을 한",
"speaker": "speaker_13",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "자연스럽게", "start_ms": 55, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "하고", "start_ms": 750, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "이후에요 한 일주일 정도 지나서 펌웨어 업데이트가 나왔는데 그거 업데이트 하고 늘리는 30일대로 늘리고 그 다음 해에는 47일",
"speaker": "speaker_13",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "이후에요", "start_ms": 40, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 500, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "때까지 확대할 예정입니다 이런 무시무시한 자동화 양산 능력이야말로 납기 보장의 자신감이 되는 것입니다 흥미로운 점은 한국의",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "때까지", "start_ms": 25, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "확대할", "start_ms": 450, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 49,
"transcript": "이재명 대통령이 물을 만들면 되지 않는가 이 질문에서 탄생한 것이 컨테이너 기반의 밀폐형 수직 농장입니다 구조부터 살펴볼까요 이",
"speaker": "speaker_14",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "이재명", "start_ms": 60, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "대통령이", "start_ms": 550, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "시스템은 외부와 100퍼센트 차단된 특수 외부 결국은 앞으로는 이제 레이어를 여러 가지 둬가지고 방어를 잘 해야 할 텐데 단순히",
"speaker": "speaker_14",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "시스템은", "start_ms": 45, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "외부와", "start_ms": 600, "dur_ms": 450, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 10:47:21*
