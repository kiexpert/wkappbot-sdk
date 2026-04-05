# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=24932  time=06:42:18  uptime=8623s  mem=439MB  system_processes=350"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_064135.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 11160ms (file: 574-103-206-607-675-502-231-210-201-312-130-201-201-213-130-301-103-130-132-201-216-260-103-301-263-310-574-210-203-574-301-310-574-510-201-213-132-103-130-107-107-571-574-132-1_W.mp3)
  Segment 2: 12160ms ~ 12736ms (file: 132-103-103-137-132-136-102-106-160-517-216-167-316-623_V.mp3)
  Segment 3: 13736ms ~ 22160ms (file: 547-215-320-123-263-102-601-103-106-120-276-567-256-627-627-267-236-321-213-126-123-230-231-231-263-162-102-130-106-106-132-321-213-231-276-276-273-127-276-756-276-120-132-136-6_W.mp3)
  Segment 4: 23160ms ~ 23736ms (file: 576-536-562-560-356-135-315-356-526-165-267-130-537-536-275-256-351-132-130-132-132-315-132-137-130-157-156 675-276_V.mp3)
  Segment 5: 24736ms ~ 34744ms (file: 143-762-725-213-756-357-754-120-527-205-217-123-236-230-735-765-576-765-267-571-547-321-352-620-572-572-753-735-163-136-267-163-276-230-163-137-123-256-235-571-572-352-103-541-5_W.mp3)
  Segment 6: 35744ms ~ 46940ms (file: 327-317-637-137-216-132-361-105-310-351-320-103-102-130-265-176-103-627-267-625-176-751-260-260-567-672-173-765-267-132-276-310-267-267-267-321-127-107-267-675-210-612-102-102-1_W.mp3)
  Segment 7: 47940ms ~ 55788ms (file: 230-267-216-261-213-627-260-153-156-315-167-762-267-276-260-201-172-123-267-621-236-123-321-721-231-236-123-316-230-362-263-236-236-206-321-310-103-102-231-261-236-261-263-231-2_W.mp3)
  Segment 8: 56788ms ~ 68596ms (file: 265-125-132-236-261-201-276-206-672-271-207-215-137-312-103-132-670-206-207-147-670-206-310-132-301-312-574-231-132-236-316-350-625-753-756-175-136-136-126-132-312-321-751-756-3_W.mp3)
  Segment 9: 69596ms ~ 70172ms (file: 763 672 637-267-127-721-167-163-637-176-176-132-124-123-312-321-326-312-175-167-162 167-621-162-176-176-671-362-136-163-162-671-672-572-576-574-371-371-327-321-513-514-514-153-5_V.mp3)
  Segment 10: 71172ms ~ 71748ms (file: 751-751-175-175-517-751-571-517-571-571-576-571-576-571-576-576-517-517-517-570-570-570-574-570-531-513-517-156-153-751-513-175-157-571-751-175-157-751-715-751-157-751-571-715-7_V.mp3)
  Segment 11: 72748ms ~ 73324ms (file: 705-760-765-675-765-750-571-175-715-731-713-701-715-753-701-705-701-751-701-705-173-705-715-710-751-731-753-751-751-705-370-375-756-765-756-756-763-765-765-756-765-735-756-756-7_W.mp3)
  Segment 12: 74324ms ~ 74900ms (file: 371-630-376-362-315-261-136-613-631-651-123_W.mp3)
  Segment 13: 75900ms ~ 81012ms (file: 260-206-207-102-710-203-123-721-216-123-102-205-102-713-102-137-203-210-201-207-103-231-237-302-207-106-207-267-162-106-106-231-320-123-231-120-102-175-163-312-321-137-132-320-2_W.mp3)
  Segment 14: 82012ms ~ 82588ms (file: 517-157-517-571-571-715-175-571-571-573-571-571-571-571-571-573-571 157-571-536-156-135-513-157-571-751-715-751-157-175-175-175-517-571-571-715-157-157-571-517-157-517-517-157-5_V.mp3)
  Segment 15: 83588ms ~ 92084ms (file: 102-231-203-231-267-132-210-106-236-106-310-210-260-263-102-263-321-326-267-260-106-216-612-631-362-236-230-260-106-237-216-236-320-216-216-132-132-610-126-263-231-165-261-612-1_W.mp3)
  Segment 16: 93084ms ~ 93660ms (file: 751-713-315-136-165-653-617-617-736 103-301-307-137-317-315-173-376-675-301-176-316-132-130-316-301-306-173-103-130-106-310-103-137-716-170-157-137-310-137-317 371-317-310-137-1_V.mp3)
  Segment 17: 94660ms ~ 95236ms (file: 675-765-765-516-675-657-751-567-617-672-657-615-652-162-165-627-675-657-167-167-167-632-675-765-765-167-165-163-136-163-165-175-165-651-617-763-165-176-165-157-165-163-136-136-1_W.mp3)
  Segment 18: 96236ms ~ 101276ms (file: 261-316-621-736-736-765-675-765-617-627-267-257-231-312-321-261-301-301-316-635-316-675-756-765-657-576-136-175-576-765-635-657-735-756-657-576-120-321-135-107-263-270-257-216-6_W.mp3)
  Segment 19: 102276ms ~ 102852ms (file: 653-657-657-672-625-625-625-625-627-672-635-672_W.mp3)
  Segment 20: 103852ms ~ 112924ms (file: 326-217-203-623-726-504-576-173-127-210-726-267-206-257-216-542-572-260-206-206-547-107-213-230-203-203-123-356-130-517-205-137-130-201-312-504-756-275-765-201-217-263-526-502-6_V.mp3)
  Segment 21: 113924ms ~ 120944ms (file: 231-132-751-371-137-361-273-276-751-673-172-276-236-237-756-672-762-102-236-356-736-752-502-123-520-361-136-123-673-705-763-230-213-206-326-123-260-745-753-320-203-130-130-621-1_W.mp3)
  Segment 22: 121944ms ~ 126840ms (file: 267-271-261-312-236-321-321-236-725-267-267-106-102-126-271-276-120-216-261-127-276-126-127-132-310-130-612-126-627-261-130-132-160-136-213-236-127-163-216-261-162-163-261-126-1_W.mp3)
  Segment 23: 127840ms ~ 136660ms (file: 172-651-156-527-725-761-312-126-321-567-132-215-265-321-315-301-310-137-201-216-213-203-130-302-306-362-251-157-756-573-213-103-132-176-163-173-162-201-310-132-261-123-126-137-1_V_stt_that_data_battle_of_bull_nail_down_all_d.mp3)
  Segment 24: 137660ms ~ 147380ms (file: 273-317-102-231-123-321-672-267-265-106-103-107-176-132-175-106-623-236-236-231-236-236-236-231-236-236-321-236-276-107-267-123-321-120-102-132-123-321-260-267-267-576-576-627-2_W.mp3)
  Segment 25: 148380ms ~ 155184ms (file: 310-750-512-576-675-163-573-765-267-621-312-576-265-157-576-172-625-617-751-261-216-261-106-312-362-123-136-236-263-136-103-206-574-267-267-576-130-627-751-756-257-572-613-706-6_W.mp3)
  Segment 26: 156184ms ~ 156760ms (file: 132-317-137-317-376-736-765-765-736-765-726-317-731-376-371-176-372-372-761-761-721-732-736-756-573-576-753-753-576-576-756-756-756-751-756-751-571-165-165-165-165-167-671-763-7_W.mp3)
  Segment 27: 157760ms ~ 158336ms (file: 765-137-765-750-573-750-715 107-710-130-735-310-103-301-173-716-103-310-372-307-310-310-367-317-730-301-130-731-710-736-310-173-307-316-376-160-361-310-136-735-317-137 731-173-3_V.mp3)
  Segment 28: 159336ms ~ 166068ms (file: 607-132-310-316-310-321-756-752-276-527-625-321-326-327-167-160-617-263-267-625-230-276-627-260-765-276-237-276-762-673-321-136-173-763-657-673-610-237-576-613-567-567-620-260-2_W.mp3)
  Segment 29: 167068ms ~ 171640ms (file: 726-321-267-620-632-263-263-261-261-612-621-627-263-620-623-630-256-162-206-267-260-267-276-267-267-267-137-136-132-102-176-126-236-631-617-126-617-612-567-673-365-123-106-320-2_W.mp3)
  Segment 30: 172640ms ~ 173216ms (file: 265-572-321-762-326-573-351-607-325-607-236-627-507-316-736-372-326-261-126-326-753-761-263-276-762-572-372-652-237-316-672-136-613-316-613-567-576-765-756-735-765-176-167-765-3_V.mp3)
  Segment 31: 174216ms ~ 178283ms (file: 710-120-312-132-137-621-126-127-126-123-213-726-276-627-621-627-267-267-627-216-126-267-167-127-216-126-267-126-136-261-132-713-217-162-367-306-167-672-172-123-261-716-671-310-3_W.mp3)
  Segment 32: 179283ms ~ 179859ms (file: 173-103-312-136-671-172-671-516-103-130-103-312-623-103-316-167-362-163-106-621-106-106-127-632-125-123-167-167-106-103-317-671-163-610-137-167-136-106-136-361-136-236-103-130-1_V.mp3)
  Segment 33: 180859ms ~ 181435ms (file: 571-570-501-501-501-570 501-507-501-517-157-517-150-150-105-517-150-507-501-506-501-516-570-150-570-501-517-501-571-517-517-571-571-513-357-531-531-153-510 105-150-105-157-517-1_V.mp3)
  Segment 34: 182435ms ~ 183011ms (file: 137-170-137-173-137-173-175-103-175-153-173-170 135-130-105-153-157-571-571-537-517-570-571-157-107-137-751-130-153-130-137-175-137-135-130-135 503-175-175-751-517-175-510-571-1_V.mp3)
  Segment 35: 184011ms ~ 194127ms (file: 150-156-261-571-751-107-175-751-756-360-362-536-103-130-371-756-765-107-571-301-576-576-102-310-620-103-312-620-361-167-106-756-765-107-621-165-756-702-203-120-103-310-751-753-2_W.mp3)
  Segment 36: 195127ms ~ 195703ms (file: 750-750-751-170-705-750-710-705-571 510-751-507-571-750-571-750-105-571-705-750-570-751-750-570-750-105-715-750-571-751-705-750-571-751-570-750-571_W.mp3)
  Segment 37: 196703ms ~ 197279ms (file: 137-715-713-312-163-316-163-167-167-176-317-163-317-316-317-317-137-275-257-325-315-356-315-356-567-657-537-573-163-136-310-367-763-763-637-673-673-673-763-756-752-756-725-735-7_W.mp3)
  Segment 38: 198279ms ~ 198855ms (file: 517-751-175-501-517-571-571-571-517-751-571-517-571-517-517-567-507-517-501-510-510-570-510-510-510-517-507-507-570-517-517-517-570-517-157-507-570-571-570-517-510-507-571-501-5_V.mp3)
  Segment 39: 199855ms ~ 200431ms (file: 521-512-512-152-516-615-615-165-163-536_W.mp3)
  Segment 40: 201431ms ~ 202007ms (file: 705-237-207-572-235-675-623-652-236-206-735-725-162-175-267-572-321-267-570-236-627-623-756-263-276-623-702-126-536-631-321-574-576-657_V.mp3)
  Segment 41: 203007ms ~ 211431ms (file: 610-160-613-160-601-162-136-621-102-136-231-230-320-756-675-263-102-321-503-765-137-102-321-763-276-102-627-230-762-726-273-267-237-236-167-132-765-132-231-130-123-102-102-260-2_W.mp3)
  Segment 42: 212431ms ~ 213007ms (file: 126-612-216-162-126-216-102-126_W.mp3)
  Segment 43: 214007ms ~ 214583ms (file: 105-157-105-105-517-157-105 157-157-157-175-157-157-510-156-571-157-571-157-517-157-157-751-750-751-751-751-750-754-754-756-754-756-756-754-754-756-752-751-103-103-105-105-165-1_W.mp3)
  Segment 44: 215583ms ~ 216159ms (file: 351-216-306-163-310-173-172 103-713-137-701-375-162-301-610-617-103-103-310-316-137-310-301-137-103-310-130-103-175-376-135-517 135-310-301-310-371-301-103-137-107-735-137-132-1_V.mp3)
  Segment 45: 217159ms ~ 225007ms (file: 137-521-635-576-573-163-136-617-617-176-163-167-167-136-130-312-675-637-675-675-163-310-637-637-675-136-103-261-231_W.mp3)
  Segment 46: 226007ms ~ 226583ms (file: 206-260-236-263-206-321-102-126-172-213-213-761-167-760-361-162-602-620-103-132-126-261-276-672-173-301-320-630-106-165-367-130-301-631-620-206-705-237-675-607-621-306-230-302-3_V.mp3)
  Segment 47: 227583ms ~ 234819ms (file: 102-517-235-132-761-137-265-102-201-574-756-752-621-765-120-623-762-574-675-675-756-527-762-236-102-176-310-576-632-657-560-675-675-167-320-623-102-167-574-572-675-320-672-670-2_W.mp3)
  Segment 48: 235819ms ~ 236395ms (file: 752-726-726-762-726-276-235-267-236-752-123-207-203-270-326-320-205-263-326-263-627-671-670-675-576-675-756-750-756-607-310-301-316-263-203-216-321-267-321-235-216-326-326-572-3_W.mp3)
  Segment 49: 237395ms ~ 237971ms (file: 570-570-570-501-501-507-150-150-510-517-576-150-517-570-507-570-501-501-150-510-570-571-517-571-517-753-751-571-175-150-571-571-175-175-157-571-751-751-571-715-157-517-571-751-5_V.mp3)
  Segment 50: 238971ms ~ 239547ms (file: 213-123-127-132-216-132-137-173 175-765-672-736-713-261-261-563-261-132-127-527-271-720-762-721-132-103-326-152-103-561-103-261-103-132-160-261-160-263-621-103-310-132-621-312-2_V.mp3)
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
> **Time**: 2026-04-05 06:43:22
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "안녕하세요 만나서 반갑습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 240,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 240, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "만나서", "start_ms": 1500, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 2400, "dur_ms": 1100, "phase_lr_deg": 0.0}
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
"transcript": "오늘 날씨가 정말 좋네요 그쵸",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "날씨가", "start_ms": 600, "dur_ms": 550, "phase_lr_deg": 5.0},
{"word": "정말", "start_ms": 1200, "dur_ms": 450, "phase_lr_deg": 5.0},
{"word": "좋네요", "start_ms": 1700, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "그쵸", "start_ms": 2300, "dur_ms": 300, "phase_lr_deg": 5.0}
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
"transcript": "네 맞아요 나들이 가기 딱 좋은 날씨예요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 110, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "맞아요", "start_ms": 400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "나들이", "start_ms": 1000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "가기", "start_ms": 1650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "딱", "start_ms": 2100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 2350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "날씨예요", "start_ms": 2750, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "혹시 근처에 맛있는 카페 알고 계신가요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "혹시", "start_ms": 180, "dur_ms": 450, "phase_lr_deg": -5.0},
{"word": "근처에", "start_ms": 700, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "맛있는", "start_ms": 1300, "dur_ms": 600, "phase_lr_deg": -5.0},
{"word": "카페", "start_ms": 2000, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "알고", "start_ms": 2500, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "계신가요", "start_ms": 2900, "dur_ms": 750, "phase_lr_deg": -5.0}
]
},
{
"segment": 7,
"transcript": "여기서 오 분 거리에 아주 유명한 곳이 있어요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "여기서", "start_ms": 200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "오", "start_ms": 750, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "분", "start_ms": 1000, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "거리에", "start_ms": 1250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 1800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "유명한", "start_ms": 2200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "곳이", "start_ms": 2850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 3250, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "거기 커피 맛이 정말 일품이라고 들었습니다",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 220,
"audio_type": "speech",
"words": [
{"word": "거기", "start_ms": 220, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "커피", "start_ms": 700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "맛이", "start_ms": 1150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 1600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "일품이라고", "start_ms": 2100, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "들었습니다", "start_ms": 3100, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 13,
"transcript": "Actually I prefer tea over coffee though",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "Actually", "start_ms": 140, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 800, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "prefer", "start_ms": 950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "tea", "start_ms": 1450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "over", "start_ms": 1800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "coffee", "start_ms": 2200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "though", "start_ms": 2750, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 15,
"transcript": "그 카페에 말차 라떼도 아주 훌륭합니다",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 190,
"audio_type": "speech",
"words": [
{"word": "그", "start_ms": 190, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "카페에", "start_ms": 450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "말차", "start_ms": 1000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "라떼도", "start_ms": 1500, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 2150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "훌륭합니다", "start_ms": 2600, "dur_ms": 850, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 17,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 18,
"transcript": "오 정말요 그럼 꼭 가봐야겠네요 고마워요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "오", "start_ms": 120, "dur_ms": 200, "phase_lr_deg": 2.0},
{"word": "정말요", "start_ms": 400, "dur_ms": 500, "phase_lr_deg": 2.0},
{"word": "그럼", "start_ms": 1000, "dur_ms": 400, "phase_lr_deg": 2.0},
{"word": "꼭", "start_ms": 1450, "dur_ms": 250, "phase_lr_deg": 2.0},
{"word": "가봐야겠네요", "start_ms": 1750, "dur_ms": 900, "phase_lr_deg": 2.0},
{"word": "고마워요", "start_ms": 2750, "dur_ms": 600, "phase_lr_deg": 2.0}
]
},
{
"segment": 19,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 20,
"transcript": "별말씀을요 나중에 후기 알려주세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 300,
"audio_type": "speech",
"words": [
{"word": "별말씀을요", "start_ms": 300, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "나중에", "start_ms": 1200, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "후기", "start_ms": 1800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "알려주세요", "start_ms": 2300, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "자 이제 공부를 시작해 볼까요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "자", "start_ms": 200, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "공부를", "start_ms": 900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "시작해", "start_ms": 1450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "볼까요", "start_ms": 2000, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "네 준비됐습니다 어떤 것부터 할까요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "준비됐습니다", "start_ms": 400, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 1350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "것부터", "start_ms": 1750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "할까요", "start_ms": 2250, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "First let's review the vocabulary from yesterday",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "First", "start_ms": 250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "let's", "start_ms": 700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "review", "start_ms": 1050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 1600, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "vocabulary", "start_ms": 1800, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "from", "start_ms": 2650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "yesterday", "start_ms": 3050, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "어제 배운 단어들이 꽤 많았죠 기억나세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "어제", "start_ms": 200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "배운", "start_ms": 650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "단어들이", "start_ms": 1100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "꽤", "start_ms": 1750, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "많았죠", "start_ms": 2000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "기억나세요", "start_ms": 2550, "dur_ms": 750, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "네 조금 헷갈리긴 하지만 복습하면 괜찮을 것 같아요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 280,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 280, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "헷갈리긴", "start_ms": 1000, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "하지만", "start_ms": 1700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "복습하면", "start_ms": 2300, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "괜찮을", "start_ms": 3100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 3600, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 3800, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "좋습니다 그럼 하나씩 확인해 봅시다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "좋습니다", "start_ms": 150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "그럼", "start_ms": 800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "하나씩", "start_ms": 1200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "확인해", "start_ms": 1750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "봅시다", "start_ms": 2300, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "첫 번째 단어는 사과입니다 영어로 뭘까요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "첫", "start_ms": 200, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "번째", "start_ms": 450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "단어는", "start_ms": 850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "사과입니다", "start_ms": 1350, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "영어로", "start_ms": 2200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "뭘까요", "start_ms": 2700, "dur_ms": 400, "phase_lr_deg": 0.0}
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
"transcript": "그건 쉽죠 정답은 애플입니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 220,
"audio_type": "speech",
"words": [
{"word": "그건", "start_ms": 220, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "쉽죠", "start_ms": 650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "정답은", "start_ms": 1150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "애플입니다", "start_ms": 1700, "dur_ms": 750, "phase_lr_deg": 0.0}
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
"transcript": "딩동댕 맞았습니다 아주 잘하시네요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "딩동댕", "start_ms": 180, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "맞았습니다", "start_ms": 1000, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 1850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "잘하시네요", "start_ms": 2250, "dur_ms": 800, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "다음 단어는 조금 어려울 수도 있어요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "다음", "start_ms": 120, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "단어는", "start_ms": 550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 1100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "어려울", "start_ms": 1600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "수도", "start_ms": 2100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 2450, "dur_ms": 450, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 44,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 45,
"transcript": "자신 있게 말씀해 보세요 틀려도 괜찮아요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "자신", "start_ms": 200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "있게", "start_ms": 650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "말씀해", "start_ms": 1100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "보세요", "start_ms": 1650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "틀려도", "start_ms": 2250, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "괜찮아요", "start_ms": 2800, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 47,
"transcript": "그럼 이제 다음 단계로 넘어가 볼까요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 1000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "단계로", "start_ms": 1450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "넘어가", "start_ms": 2000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "볼까요", "start_ms": 2550, "dur_ms": 450, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 50,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 06:43:22*
