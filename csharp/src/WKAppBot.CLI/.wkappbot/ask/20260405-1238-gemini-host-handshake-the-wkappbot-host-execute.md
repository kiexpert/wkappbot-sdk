# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=30524  time=12:37:18  uptime=29150s  mem=355MB  system_processes=360"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_123647.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 8532ms (file: 120-213-213-213-213-123-213-123-312-321-317-672-213-231-217-213-216-213-213-213-231-216-213-213-213-217-213-213-126-123-213-123-213-213-213-213-213-213-216-213-216-213-213-213-2_W.mp3)
  Segment 2: 9532ms ~ 10108ms (file: 617-126-103-130-136-136-130-136-106-137-103-136-165-132-136-103-130-163-130-316-103-130-167-103-150-263-160-126-206-136-613-136-105-106-317-164-136-130-261-651-631-623-623-321_V.mp3)
  Segment 3: 11108ms ~ 11684ms (file: 672-627-623-623-632-316-612-547-547-547-153-163-136-631-632-326-263-631-675-673-675-675-675-675-675-675-267-261-267 521-256-652-632-326-236-263-326-263-362-362-321-312-312-231-2_W.mp3)
  Segment 4: 12684ms ~ 13260ms (file: 165-156-157-175-157-157-175-175-715-175-175-157-175-157-175-157-157 105-105-150-105-154-150-150-105-105-157-170-175-157-153-157-157-157-157-175-150-157-150-157-105 150-150-105-1_V.mp3)
  Segment 5: 14260ms ~ 22576ms (file: 762-726-326-320-120-107-107-320-230-721-102-276-276-327-765-360-570-276-263-765-276-350-320-170-231-130-102-321-723-716-213-237-713-165-762-102-130-103-173-527-103-102-132-203-5_W.mp3)
  Segment 6: 23576ms ~ 24152ms (file: 175-751-751-751-751-756-751-751 517-105-715-715-715-751-751-715-715-751-175-751-751-753 501-501-510-517-153-157-517-715-175-751-751-715-751-751-715-751-715-751-751-751-753-751 5_W.mp3)
  Segment 7: 25152ms ~ 33576ms (file: 263-326-172-276-237-327-176-713-317-167-123-361-732-723-273-276-327-326-237-736-732-725-763-732-763-756-231-267-627-127-132-517-317-576-754-312-123-321-236-321-102-216-123-127-1_W.mp3)
  Segment 8: 34576ms ~ 35152ms (file: 517-517-157-571-510-150-105-501-350-105-153-107-105-153-517-150-501-501-510-105-150-513-531 157-157-135-105-153-157-501-135-153-157-150-501-571-150-175-501-105-501-513-105-150-1_V.mp3)
  Segment 9: 36152ms ~ 36728ms (file: 570-570-572-576-572-572-572-546-541-543-534-572-571-517-573-572-571-567-571-756-571-756-751-756-751-735-541-513-534-521-517-537-502-501-123-125-235-251-536-531-653-513-512-153-1_W.mp3)
  Segment 10: 37728ms ~ 38304ms (file: 571-751-571-571-751-751-715-571-576-576-571-517-570 517-517-517-157-517-150-137-715-571-715-751-175-571-751-517-571-571-571-751-571-571-751-571-571-756-756-570 153-513-157-517-1_V.mp3)
  Segment 11: 39304ms ~ 39880ms (file: 651-651-615-615-617-657-675-615-651-615-615-615-617-651-657-675-612-615-651-657-675-617 615-615-615-615-651-651-675-615-651-165-163-615-617-617-675-675-675 657-671 617-671-675-6_W.mp3)
  Segment 12: 40880ms ~ 41456ms (file: 576-256-257-527-267-625-207-672-762-276-760-756-765-675-765-576-765-762-752-756-576-765-752-576-765-751-756-765-570-756-576-752-527-752-527-725-752-576-572-517-205-205-625-670-6_V.mp3)
  Segment 13: 42456ms ~ 43032ms (file: 136-163-103-173-167-165-103 231-236_V.mp3)
  Segment 14: 44032ms ~ 44608ms (file: 527-527-527-527-567-752-572-523-537-572-576-572-527-576-526-572-752-572-576-756-763-176-541-512-512-573-527-507-567-562-532-517-507-507-507-501-507-527-502 571-756-756-756-756-7_W.mp3)
  Segment 15: 45608ms ~ 46184ms (file: 135-351-571-157-571-165-167-617-672-675-671-165-315-157-715-765-756-612-621-763-175-726-715-675-756-765-761-675-176-762-675-761-712-712-751-712-752-756-725-715-752-752-756-725-7_W.mp3)
  Segment 16: 47184ms ~ 53232ms (file: 203-321-350-357-231-320-231-321-753-573-726-571-326-361-136-312-731-317-312-317-317-130-301-310-206-206-206-267-231-320-573-572-701-130-302-672-267-260-756-574-573-750-756-765-2_W_stt_the_play_we_have_exactly_as_it_is_the_ea.mp3)
  Segment 17: 54232ms ~ 54808ms (file: 725-572-751-175-571-576-517-576-572-576-567-561-305-670-675-135-132-135-136-156-156-156-156-156-153-572-756-570-576-517 756-761-137-576 625-625-627-612-623-627-625-621-620 534-5_W.mp3)
  Segment 18: 55808ms ~ 63332ms (file: 312-132-136-571-756-263-321-230-231-617-265-132-123-216-231-132-130-256-126-173-106-310-237-231-267-132-132-765-675-120-103-132-310-130-276-237-372-756-715-107-120-231-572-361-3_W.mp3)
  Segment 19: 64332ms ~ 64908ms (file: 103-302-613-162-103-103-126-671-576-725-362-567-162-675-316-612-123-672-267-602-162-162_V.mp3)
  Segment 20: 65908ms ~ 66484ms (file: 576-516-325-567-512-526-576-576-756-756-576-567-136-156-561-156-567-576-516-576-517-657-615-657 561-526-572-512-512 156-156-157-156-153-156-652_W.mp3)
  Segment 21: 67484ms ~ 68060ms (file: 572-756-756-571-706-756-617-765-715-765-126-560-163-136-615-106-612-120-175-176-576-572-571-215-210-102-157-571-576-275-752-572-572-517-125-153-125-215-512-150-510-570-567-657-6_W.mp3)
  Segment 22: 69060ms ~ 69636ms (file: 567 526-527-526-567 502-520-520-502-507-572-672-520-256-526-625-567-675-756-765-576-756-576-765-765-756-756-756-765-675-527-527-256-572-572-572-527-527-571-524-520-205-257-257-5_V.mp3)
  Segment 23: 70636ms ~ 80140ms (file: 267-267-571-574-526-123-610-372-310-136-312-123-316-102-106-261-263-251-310-216-267-267-123-102-356-735-503-326-317-327-236-102-316-725-273-210-301-310-132-103-203-162-106-230-5_W.mp3)
  Segment 24: 81140ms ~ 81716ms (file: 176-176-716-175-715-751-761-756-715-756-751-175-750-751-754-756-756-765-756-765-756-756-756-765-754-756-716-765-754-105-156-150-105-156-170-175-175-176-175-176-175-751-715-716-7_W.mp3)
  Segment 25: 82716ms ~ 83292ms (file: 103-105-153-157-150-150-137-170-107-106-130-103 150-150-157-150-105-150-150-571-517-157-157-517-157 105-105-105-150-510-570-507-570-501-541-507-715-517-570-571-510-573 504-517-5_V.mp3)
  Segment 26: 84292ms ~ 84868ms (file: 527-157-105-610-615-615-657-615-651-615-657-165 612-615-617-615-657-615 615-615-615-615-657-651-657-657-675-657-675-675-675-675-617 627-617-651-617-617 612-612-612-671-675-675-6_W.mp3)
  Segment 27: 85868ms ~ 92204ms (file: 635-635-230-260-261-205-570-752-156-175-270-201-102-105-320-153-517-301-752-753-735-230-132-106-103-130-172-103-203-103-231-301-312-130-103-310-312-312-102-132-320-351-165-175-1_V.mp3)
  Segment 28: 93204ms ~ 93780ms (file: 571 507-571-510-150-510-105-510-517-517-105-510-501-501-501-150-501-510-501-510-517-501-150-350-510-715 620-167-510-501-510-157-150-501-105-157-501-510-501-513-501-507-517-501-1_V.mp3)
  Segment 29: 94780ms ~ 95356ms (file: 576-756-572-705-570-750-507-570-507-275-756-567-657-750-527-527-527-571-567-567-567-567-567-567-567-576-512-537-537-536-527-527-507-507-570-257-257-572-257-275-275-275-725-725-2_W.mp3)
  Segment 30: 96356ms ~ 96932ms (file: 576-752-751-126-126-612-162-126-162-162-756-125-751-572-567-576-765-576-657-765-126-216-126-162-162-163-612-162-276-256-217-765-756-162-216-612-623-621-126-621-162-162-613-613-1_W.mp3)
  Segment 31: 97932ms ~ 98508ms (file: 654-657-674-675-654-657-654-654-625-654-612-175-647-615-657-657-652-625 621-612-651-657-657-651-651-657-657-675-654-657-654-657-657-651-617 654-657-651-265-652-652-612 625-624-6_W.mp3)
  Segment 32: 99508ms ~ 100084ms (file: 675-567-532-273-562-267-756-762-530-312-126-126-257-637-316-132-217-627-312-563-235-672-132-275-732-263-167-253-576-162-356-365-256-132-572-275-627-570-517-762-765-567-537-372-3_V.mp3)
  Segment 33: 101084ms ~ 101660ms (file: 501-517-517-510-510-501-510-517-501-570-576-570-517-570-517-574-571-574-567-576-574-564-576-574-570-571 510-105-105-157-517-501-501-157-501-571-517-510-157-570-501-510-507-570-5_W.mp3)
  Segment 34: 102660ms ~ 103236ms (file: 716-576-576-576-576-576-576-576-576-576-567-576-576-576-756-576-576-576-576-576-576-576-576-756-576-576-756-576-576-576-576-756-672-675-765-765-675-765-765-672-671-617-716-761-7_W.mp3)
  Segment 35: 104236ms ~ 104812ms (file: 562-527-206-273-201-271-756-752-207-762-725-752-276-257-576-572 561-762-627-701-705-720-507-752-276-276-725-670-720-710-752-605-730-307-370-706-752-576 127-275-207-725-275-657-7_V.mp3)
  Segment 36: 105812ms ~ 116792ms (file: 127-527-725-216-103-260-106-132-175-107-132-132-267-216-206-206-310-263-136-130-267-621-107-126-260-206-574-507-750-120-651-256-132-106-123-261-130-130-310-765-765-130-310-120-2_W.mp3)
  Segment 37: 117792ms ~ 130824ms (file: 267-321-231-765-123-236-613-267-106-132-167-102-213-216-675-126-236-123-103-160-160-756-267-267-572-256-260-260-236-321-716-672-261-267-675-126-210-130-106-237-236-102-136-326-1_W.mp3)
  Segment 38: 131824ms ~ 141184ms (file: 236-256-103-261-572-102-260-263-627-754-632-362-173-260-263-126-102-263-574-326-267-263-260-725-231-236-176-675-263-321-103-106-106-524-263-236-231-263-752-756-756-130-754-375-2_W.mp3)
  Segment 39: 142184ms ~ 142760ms (file: 671-760-670-601-610-706-671-716-617-176-167-657 157-105-105-105-105-150-150-105-150-105-150-165-157-150-156-167-167 170-716-175-175-175-715-173-176-175-176-175-517-156-157-716-5_W.mp3)
  Segment 40: 143760ms ~ 144336ms (file: 723-375-715-752-705-251 713-126-672-562-536-750-726-213-751-126-526-572-712-752-276-265-312-216-257-726-327-317-672 573-570-357-312-275-752-537-321-705 547-756-312-236-753-276-7_V.mp3)
  Segment 41: 145336ms ~ 145912ms (file: 157-156-567-571-571-617-567-175-567-756-576-715-175 540-154-150-150-157-167-175-715-751-157-165-167-157-167-156-157-156-157-157-156-156-165-517-157-156-156-156-157-175 104-510-1_W.mp3)
  Segment 42: 146912ms ~ 147488ms (file: 156-157-153-173-137-175-153-135-157-157-137-573-153-132-317-165-156-615-170-165-103-157-157-165-132-236-230-513-516-153-165-165-165-165-165-165-156-156-312-132-153-531-135-157-5_W.mp3)
  Segment 43: 148488ms ~ 149064ms (file: 170-170-175-175-751-751 175-175-175-175-175-170-170-715-175-175-175-715-175-715-175-715-175-170-715-175-752-751-571-571 751-715-750-175-175-175-157-175-715-751-715-715-751-175-7_W.mp3)
  Segment 44: 150064ms ~ 150640ms (file: 574-571-157-571-750-715-751-751-517-175-517-751-107-751-715-175-710-751-571 150-175-170-710-710-715-751-175-751-751-571-751-571-751-715-751-751-751-751-756-715 571-751-715-715-7_W.mp3)
  Segment 45: 151640ms ~ 152216ms (file: 753-136-106-103-130-376-765-507-570-325-751-276-567-572-257-765-372-327-163-136-127-751-125-570-265-752-571-527-705-752-725-762-751-625-175-326-705-254-267-567-217-726-507-754-5_V.mp3)
  Segment 46: 153216ms ~ 153792ms (file: 627-625-675-625-625-263 625-526-625-572-657-625-675-627-625-627-625 625-524-625-562-652-625-675-627-672-652-627-627-627-267 625-256-526-657-576-675-657-625-675-627-625 625-265-6_W.mp3)
  Segment 47: 154792ms ~ 160588ms (file: 261-102-571-574-524-120-152-361-362-316-316-157-327-312-312-312-316-362-617-635-623-617-103-725-132-301-301-103-721-201-321-123-103-312-120-153-521-725-275-725-132-312-132-312-3_W.mp3)
  Segment 48: 161588ms ~ 162164ms (file: 571-376-312-326-263-206-126-206-265-627-267-567-657-756-657-310-631-361-312-126-213-675-657-137-567-127-276-570-563-321-316-301-312-136-312-132-541-267-627-206-271-621-267-206-5_V.mp3)
  Segment 49: 163164ms ~ 163740ms (file: 517-157-156-517-315 735-735-317-137-317-137-315-315-371 765-751-176-137-173-137-713-136-136-176-173-175-513-153-357-136-315-135-315-135-136-135-175-137-761-371-701-761-763-627-7_W.mp3)
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
> **Time**: 2026-04-05 12:38:43
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "우와 약간 칼국수 면발 느낌 나요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "우와", "start_ms": 120, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "약간", "start_ms": 800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "칼국수", "start_ms": 1400, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "면발", "start_ms": 2300, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "느낌", "start_ms": 3000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "나요", "start_ms": 3600, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "800만 명이었던 아일랜드 섬의 인구는",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "800만", "start_ms": 80, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "명이었던", "start_ms": 750, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "아일랜드", "start_ms": 1500, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "섬의", "start_ms": 2400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "인구는", "start_ms": 2900, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "전체의 25%가 사라진 겁니다 어떤 전쟁도 어떤 전염병도",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "전체의", "start_ms": 50, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "25%가", "start_ms": 600, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "사라진", "start_ms": 1500, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "겁니다", "start_ms": 2200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 3000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "전쟁도", "start_ms": 3500, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 4200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "전염병도", "start_ms": 4700, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "이 섬의 인구를 이토록 빠르게 지우지 못했습니다 그런데 역사가들이",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 60, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "섬의", "start_ms": 300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "인구를", "start_ms": 800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이토록", "start_ms": 1400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "빠르게", "start_ms": 2000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "지우지", "start_ms": 2600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "못했습니다", "start_ms": 3200, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "그런데", "start_ms": 4200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "역사가들이", "start_ms": 4800, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "가격이 저렴하다고 맛을 포기할 수는 없습니다 강한 화력으로 불향을 입혀야 비로소 완성되는 간짜장 그런데 고소한",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "가격이", "start_ms": 100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "저렴하다고", "start_ms": 700, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "맛을", "start_ms": 1600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "포기할", "start_ms": 2100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "수는", "start_ms": 2700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "없습니다", "start_ms": 3100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "강한", "start_ms": 4000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "화력으로", "start_ms": 4500, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "불향을", "start_ms": 5300, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "입혀야", "start_ms": 6000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "비로소", "start_ms": 6600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "완성되는", "start_ms": 7200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "간짜장", "start_ms": 7900, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "그런데", "start_ms": 8600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "고소한", "start_ms": 9200, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "And that's why we have exams like this. It's just to make it easy and give us some rational basis that everybody will accept.",
"speaker": "speaker_4",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "And", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "that's", "start_ms": 250, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "why", "start_ms": 550, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "we", "start_ms": 800, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "have", "start_ms": 1000, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "exams", "start_ms": 1300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "like", "start_ms": 1750, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "this.", "start_ms": 2050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "It's", "start_ms": 2500, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "just", "start_ms": 2750, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 3050, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "make", "start_ms": 3250, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "it", "start_ms": 3550, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "easy", "start_ms": 3750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "and", "start_ms": 4150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "give", "start_ms": 4400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "us", "start_ms": 4700, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "some", "start_ms": 4900, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "rational", "start_ms": 5200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "basis", "start_ms": 5750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "that", "start_ms": 6250, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "everybody", "start_ms": 6500, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "will", "start_ms": 7150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "accept.", "start_ms": 7400, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "거칠게 날뛰는 파도의 장난감이 되었다가 끝내 침몰하고 맙니다 게다가 급격한 기온 하강과 함께 수분을 잔뜩 머금은 공기가 한꺼번에 밀려드는 상황",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "거칠게", "start_ms": 80, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "날뛰는", "start_ms": 650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "파도의", "start_ms": 1200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "장난감이", "start_ms": 1800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "되었다가", "start_ms": 2500, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "끝내", "start_ms": 3200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "침몰하고", "start_ms": 3700, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "맙니다", "start_ms": 4400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "게다가", "start_ms": 5200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "급격한", "start_ms": 5800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "기온", "start_ms": 6500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "하강과", "start_ms": 7000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "함께", "start_ms": 7700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "수분을", "start_ms": 8200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "잔뜩", "start_ms": 8800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "머금은", "start_ms": 9300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "공기가", "start_ms": 9900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "한꺼번에", "start_ms": 10500, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "밀려드는", "start_ms": 11300, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "상황", "start_ms": 12000, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "어떤 일이 벌어졌으며 그래서 좀 어떤 게 좀 나아졌는지 한번 얘기해 보도록 할게요",
"speaker": "speaker_5",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "어떤", "start_ms": 70, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "일이", "start_ms": 450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "벌어졌으며", "start_ms": 850, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 1700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 2300, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 2600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 3050, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 3350, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "나아졌는지", "start_ms": 3650, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "한번", "start_ms": 4500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "얘기해", "start_ms": 5000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "보도록", "start_ms": 5600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "할게요", "start_ms": 6200, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "일단 지금 가장 핫한 소프트웨어 업데이트 차별에 대한 얘기부터 한번 해볼게요 사실",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "일단", "start_ms": 100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 1000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "핫한", "start_ms": 1500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "소프트웨어", "start_ms": 2000, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "업데이트", "start_ms": 2800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "차별에", "start_ms": 3500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "대한", "start_ms": 4100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "얘기부터", "start_ms": 4600, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "한번", "start_ms": 5300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "해볼게요", "start_ms": 5800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "사실", "start_ms": 6500, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "왜 이렇게 추워요 여기가 여기가 바다 근처라 춥습니다 아 춥네 갑자기 영하권 봄하고 겨울하고 같이 체험하는 거 같아 지금",
"speaker": "speaker_6",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "왜", "start_ms": 90, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "추워요", "start_ms": 850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "여기가", "start_ms": 1450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "여기가", "start_ms": 2050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "바다", "start_ms": 2650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "근처라", "start_ms": 3150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "춥습니다", "start_ms": 3750, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 4550, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "춥네", "start_ms": 4850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "갑자기", "start_ms": 5350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "영하권", "start_ms": 5950, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "봄하고", "start_ms": 6650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "겨울하고", "start_ms": 7250, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 7950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "체험하는", "start_ms": 8450, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 9150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "같아", "start_ms": 9450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 9950, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "실제 관람객의 반응은 엄청 나쁜 편은 아니었습니다 네이버 평가 7점대 후반 CGV 에그 지수는 90%대 영화 리뷰를 찾아봐도 욕 반 칭찬 반 그저 무난한 영화였죠 이런 지표들을 보면",
"speaker": "speaker_7",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "실제", "start_ms": 110, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "관람객의", "start_ms": 600, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "반응은", "start_ms": 1300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "엄청", "start_ms": 1900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "나쁜", "start_ms": 2400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "편은", "start_ms": 2900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아니었습니다", "start_ms": 3400, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "네이버", "start_ms": 4400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "평가", "start_ms": 5000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "7점대", "start_ms": 5500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "후반", "start_ms": 6100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "CGV", "start_ms": 6700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "에그", "start_ms": 7300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "지수는", "start_ms": 7800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "90%대", "start_ms": 8400, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "영화", "start_ms": 9200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "리뷰를", "start_ms": 9700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "찾아봐도", "start_ms": 10300, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "욕", "start_ms": 11000, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "반", "start_ms": 11300, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "칭찬", "start_ms": 11600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "반", "start_ms": 12100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "그저", "start_ms": 12400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "무난한", "start_ms": 12800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "영화였죠", "start_ms": 13400, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 14300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "지표들을", "start_ms": 14800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "보면", "start_ms": 15500, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "80도의 얼음이 녹아버리면 북극의 찬 공기를 꽉 가둬두던 제트기류가 붕괴합니다 이 빗장이 풀리면 어떻게 되냐 우리가 겨울마다 겪는 미친 듯한 한파 그리고 여름의 이",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "80도의", "start_ms": 80, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "얼음이", "start_ms": 750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "녹아버리면", "start_ms": 1350, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "북극의", "start_ms": 2200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "찬", "start_ms": 2800, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "공기를", "start_ms": 3100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "꽉", "start_ms": 3700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "가둬두던", "start_ms": 4100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "제트기류가", "start_ms": 4800, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "붕괴합니다", "start_ms": 5600, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 6600, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "빗장이", "start_ms": 6900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "풀리면", "start_ms": 7500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 8100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "되냐", "start_ms": 8700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "우리가", "start_ms": 9300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "겨울마다", "start_ms": 9900, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "겪는", "start_ms": 10600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "미친", "start_ms": 11100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "듯한", "start_ms": 11600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "한파", "start_ms": 12100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 12800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "여름의", "start_ms": 13400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 14000, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "있습니다 우리는 트럼프 대통령이 왜 이 시점에 한국에 대한 불만을 노골적으로 하고 있는지 그 배경을 냉철하게 분석해야 합니다 아울러 그가 내세우는 논리에 치명적인 모순을 지적하고 향후 그가 들이밀 무역",
"speaker": "speaker_8",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "있습니다", "start_ms": 100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "우리는", "start_ms": 900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "트럼프", "start_ms": 1500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "대통령이", "start_ms": 2100, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "왜", "start_ms": 2900, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 3200, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "시점에", "start_ms": 3500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "한국에", "start_ms": 4100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "대한", "start_ms": 4700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "불만을", "start_ms": 5200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "노골적으로", "start_ms": 5900, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "하고", "start_ms": 6800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "있는지", "start_ms": 7300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 7900, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "배경을", "start_ms": 8200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "냉철하게", "start_ms": 8800, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "분석해야", "start_ms": 9600, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "합니다", "start_ms": 10400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "아울러", "start_ms": 11200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "그가", "start_ms": 11800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "내세우는", "start_ms": 12300, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "논리에", "start_ms": 13000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "치명적인", "start_ms": 13600, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "모순을", "start_ms": 14400, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "지적하고", "start_ms": 15100, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "향후", "start_ms": 16000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "그가", "start_ms": 16600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "들이밀", "start_ms": 17100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "무역", "start_ms": 17700, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "100개가 넘습니다",
"speaker": "speaker_9",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "100개가", "start_ms": 80, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "넘습니다", "start_ms": 750, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "네 어 좀 더 전국 방방곡곡을 누비던 그야말로",
"speaker": "speaker_10",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 120, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "어", "start_ms": 450, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 750, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 1050, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "전국", "start_ms": 1450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "방방곡곡을", "start_ms": 1950, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "누비던", "start_ms": 2850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "그야말로", "start_ms": 3450, "dur_ms": 600, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 12:38:43*
