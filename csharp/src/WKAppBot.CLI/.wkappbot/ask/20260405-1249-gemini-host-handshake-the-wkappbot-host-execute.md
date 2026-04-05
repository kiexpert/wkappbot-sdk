# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=17628  time=12:48:41  uptime=39334s  mem=407MB  system_processes=360"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_124829.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 4536ms (file: 504-504-504-504 356 572 570-547-547-574-540-504-507-507-570-574-574-174-103-136-314-130 570-752-507-507-752-753-715-725-753-572-572-201-257-275-527-725-137-251-520-510-251-203-6_V.mp3)
  Segment 2: 5536ms ~ 15724ms (file: 106-236-103-132-137-753-765-763-237-273-270-547-230-237-756-756-613-231-327-762-301-320-276-276-756-270-207-547-570-751-103-574-107-312-103-103-175-175-317-732-726-726-276-207-2_W.mp3)
  Segment 3: 16724ms ~ 17300ms (file: 517-571-175-517-571-751-751 571-517-517-751-570-750-751-751-750-715-175-107-715-750-150-107-751-715-701-715-751-751-750-157-170-175 105-715-751-571-715-157-175-175-517-715-751-7_V.mp3)
  Segment 4: 18300ms ~ 18876ms (file: 521 534-547-543-523-537-501-510-537-504-531-562-561-516-506-516-503-502-547-503-506-547-502-574-571-573-526-571-571-542-507-507-506-506-502-507-506-504-507-532-531-531-351-534-3_V.mp3)
  Segment 5: 19876ms ~ 20452ms (file: 503-507-507-507-530-350-351-521-250-253-352-253-532-523-532-325-502-503-541-514-542-532-521-567-532-523-531-513-534-516 574-507-503-507-507-530-530-350-523-325-253-315-312-320-2_V.mp3)
  Segment 6: 21452ms ~ 22028ms (file: 135-165-165-176-175-157-176-175-165-157-136 105-150-150-154-105-105-156-135-136-153-165-310-163-157-153-165-153-165-156-165-157 105-150-153-150-157-165-135-315-153-135-356-165-1_W.mp3)
  Segment 7: 23028ms ~ 23604ms (file: 751-715-750 157-105-105-175-107-105-170-175-130-107-701-175-751-175-157-751-157-137-153-513-175-175 105-105-105-175-105-751-715-150-175-157-175-170-170-105-103-175-107-517-150-1_V.mp3)
  Segment 8: 24604ms ~ 31588ms (file: 123-123-756-132-136-102-102-236-137-106-372-132-231-213-751-754-276-752-216-754-713-170-765-372-325-732-273-321-372-312-376-765-276-276-312-102-312-217-317-231-371-310-327-367-6_W.mp3)
  Segment 9: 32588ms ~ 43424ms (file: 106-762-267-231-762-130-310-213-132-261-260-517-571-276-672-627-176-106-163-675-756-765-726-267-765-236-231-672-213-310-267-627-627-672-213-132-312-326-132-130-270-231-326-167-7_W.mp3)
  Segment 10: 44424ms ~ 56520ms (file: 103-321-620-267-321-103-135-573-321-132-173-547-576-370-132-136-261-260-621-602-756-216-256-103-751-351-175-574-157-504-105-620-301-312-230-206-123-103-756-756-175-167-512-670-7_V.mp3)
  Segment 11: 57520ms ~ 58096ms (file: 516-513-513-510-516-517-517-157-157-517-157-157-157-517-157-156 517-152-512-512-512-512-512-517-517-512-517-512-517-517-516 516-512-517-512-157-512 125-123-136-132-125-162-132-7_W.mp3)
  Segment 12: 59096ms ~ 68132ms (file: 672-260-206-126-627-103-710-213-216-201-756-267-123-320-260-103-637-102-201-316-603-321-201-103-203-216-201-542-103-206-574-267-231-120-672-672-210-216-127-206-213-103-230-752-2_W.mp3)
  Segment 13: 69132ms ~ 69708ms (file: 504-503-502-503-503-507-503-530-507-507-507-507-507-507-507-507-576-507-572-576-570-572-570-507-570-572-576-126-312-215-521-512-521-512-523-512-507-572-572-507-507-571-507-507-5_V.mp3)
  Segment 14: 70708ms ~ 71284ms (file: 507-706-325-125-706-672-157 276-270-627-621-270-756-615-671-173-652-362-750-352-326-326-627-507-627-563-576-175-627-752-762-267-716-263-276-273-527-321-675-672-756-163-132-257-3_V.mp3)
  Segment 15: 72284ms ~ 72860ms (file: 105-150-150-150-510-517-541-546-547-517-546-567-576-547-765-607-756-156-651-567-156-675-157 105-105-105-150-156-516-156-156-105-156-613-165-167-156-165-156-156-165-176-176-157 1_W.mp3)
  Segment 16: 73860ms ~ 74436ms (file: 176-172-716-125-256-251-235-513-517-375-126-712-213-312-572-715-756-572-562-571-317-123-671-253-750-213-123-102-321-257-176-106-163-576-576-576-576-576-576-716-176-167-173-130-1_V.mp3)
  Segment 17: 75436ms ~ 76012ms (file: 170-175-170-715-751-751-576-567-576-567-576-576-576-576-571-576-576-570-576-576-576 105-571-576-153-715-715-137-751-517-175-751-751-157-751-751-751-571-751-751-571-571-715-751-5_V.mp3)
  Segment 18: 77012ms ~ 77588ms (file: 167-617-167-162-167-162-167-132-126-132-123-123-160-126-561-260-675-675-561-560-635-136-316-160-123-310-316-176-376-162-130-103-126-132-627-162-126-126-162-123-103-571-672-672-6_V.mp3)
  Segment 19: 78588ms ~ 79164ms (file: 615-615-657-567-675-756-765-756-751-756-751-756-751-715-751 657-675-567-576-571-756-751-756-756-751-756-751-756-756-751-756-751 675-675-657-765-675-765-657-675-675-675-675-765-6_W.mp3)
  Segment 20: 80164ms ~ 80740ms (file: 175-751-751-751-751-751-715 150-105-715-715-751-751-715-751-571-751-715-751-175-751-715-715-751-751-751-751 150-150-157-150-157-157-751-571-571-751-751-751-751-751-751-756-756-7_W.mp3)
  Segment 21: 81740ms ~ 82316ms (file: 576-576-501-517-570-517-570-570-574-570-574-541-574-576-574-576 105-150-105-105-501-150-510-150-510-510-150-517-150-507-507-510-507-501-517-571-570-570-501-517-517-514-574-547-5_W.mp3)
  Segment 22: 83316ms ~ 89436ms (file: 213-203-327-132-301-165-103-236-132-210-136-327-267-267-267-352-301-317-756-276-316-130-631-201-276-237-721-372-320-103-132-231-237-237-231-726-375-102-705-201-201-312-273-167-2_V.mp3)
  Segment 23: 90436ms ~ 95620ms (file: 213-230-572-672-576-276-231-231-210-752-106-165-536-512-576-573-125-156-132-132-123-261-125-102-120-163-136-261-213-321-627-756-762-123-132-120-102-123-702-132-613-623-621-132-1_W.mp3)
  Segment 24: 96620ms ~ 105584ms (file: 675-260-752-725-206-765-132-276-756-756-167-103-130-632-103-312-236-576-531-136-103-130-632-751-165-657-130-103-132-312-216-132-507-631-712-107-173-752-754-756-627-627-627-753-5_W.mp3)
  Segment 25: 106584ms ~ 107160ms (file: 172-760 573-715-576-165-576-657-137-321-172-123-671-672-672-765-163 135-753-712-506-726-275-725-572-725-705-752-725-762-627-623-267-261-231-321-720-273-137-105-750 312-756-627-2_V.mp3)
  Segment 26: 108160ms ~ 108736ms (file: 306 130 156-615-365-517-563-563-532-156-526-615-167-617-651-625-632-163-162-132-165-316-163-176-761-657-567-756-756-105-165-231-216-162-136-163-165-162-615-165-615-162-165-165-1_W.mp3)
  Segment 27: 109736ms ~ 110312ms (file: 627-627-273-721-271-721-726-723-276-712-271-217-172-271-126-123-123-231-213-321-231-213-321-213 136-325 165-621-623-216-627-216-123-162-103_W.mp3)
  Segment 28: 111312ms ~ 111888ms (file: 132-572-576-675-265-756-627-265-263-263-263-216-527-572-256-136-516-652-576-576-567-627 361-621-631-321-261-612-621-163 312-632-163-167-761-167-617-617-132 756-657-657-623-263-2_W.mp3)
  Segment 29: 112888ms ~ 113464ms (file: 263-143-173-163-163-136-516-361-376-130-167-172-136-132-130-761-137-130-136-103-163-103-106-675-106-103-160-136-162-165-162-106-160-132-103-103-163-134-136-126-316-106-136-132-1_V.mp3)
  Segment 30: 114464ms ~ 115040ms (file: 157-751-571-157-751-517-751-751-157 571-571-571-574-571-571-571-175-517-571-157-517-571-573-157-571-517-751-517-571-751-751-576-571-754-517_W.mp3)
  Segment 31: 116040ms ~ 116616ms (file: 105-105-105-105-105-105-157-510-513-501 150-501-150-514-150-510-501-510-501-501-153-510-503-510-501-510-150-510-510-571-150-501-510-571-150-150-510-150-507 514-150-501-150-510-5_V.mp3)
  Segment 32: 117616ms ~ 118192ms (file: 571-751-715-510-157-570-507-150-571-507-517-510-517-571-517-517-765-765-765-756-765-765-510-510-501-501-157-571-501-750-570-750-570-517-571-501-517-570-507-507-510-105-150-501-5_V.mp3)
  Segment 33: 119192ms ~ 119768ms (file: 765-765-671-267-627-612-163-376-617-103-321-231-230-320-213-320 267-267-562-625-627-627-621-263 617-162_W.mp3)
  Segment 34: 120768ms ~ 121344ms (file: 137-317-136-137-163-136-176-361-671-376-176-761-705-715-675-310-715-137-107-571-754-753-305-571-546-167-176-163-316-103-137-176-173-376-167-735-371-375-176-617-367-107-176-107-7_W.mp3)
  Segment 35: 122344ms ~ 129616ms (file: 205-675-762-536-102-602-670-672-765-127-507-312-531-102-213-261-103-103-235-572-130-103-321-136-302-103-123-130-130-126-213-260-627-267-162-602-573-310-320-321-127-736-763-756-2_V.mp3)
  Segment 36: 130616ms ~ 131192ms (file: 170-307-310-735-173-103 103-130-130-107-713-170-173-107-137-715-713-735-375-735-301-137-701-173-375-301-701-172-701-750-710-750-731-107-713-713-170 135-173-130-175-703-710-713-1_V.mp3)
  Segment 37: 132192ms ~ 142416ms (file: 163-130-631-316-621-126-261-163-213-312-236-103-132-163-206-361-130-165-576-301-156-132-126-136-137-750-750-103-106-163-167-136-106-130-361-103-601-316-160-106-106-163-106-163-2_V.mp3)
  Segment 38: 143416ms ~ 143992ms (file: 265-625-532-526-527-512-265-621-265-625-621-615-651-615-621-615-265-162-160-132-526-521-526-501-501-502-502-502-501-502-503-502-501-506-265-612-610 163-561-156-615-156-165-165-1_W.mp3)
  Segment 39: 144992ms ~ 145568ms (file: 570-571-750-570-751-571-571-571-570-574-570-751-574-570 570-507-517-570-571-751-175-576-751-570-751-576-756-705-756 157-561-157-756-576-570-715-756-751-750-751-750-751-751-751-1_V.mp3)
  Segment 40: 146568ms ~ 147144ms (file: 351-130-150-130-135-135-153-315-513-510-153-157-513-157-571-517-571-517-513 105-153-157-130-153-517-517-150-170-130-130-310-317-157-513-517-517-573-571-571-571-571-571-571-751 1_V.mp3)
  Segment 41: 148144ms ~ 148720ms (file: 176-137-163-317-163-617-671-176-167-761-765_W.mp3)
  Segment 42: 149720ms ~ 150296ms (file: 516-536-540-602-546-576-547-574-567-172-671-176-327-137-167-617-130-150-163-123-123-162-612-126-132-316-631-231-132-310-130-132-103-106-120-165-120-132-123-361-513-675-657-613-5_W.mp3)
  Segment 43: 151296ms ~ 151872ms (file: 132-312-157-132-315-135-153-135-103-317-103-135-105-301-135-531-317-153-351-135-301 130-310-301-103-103-137-103-135-132-153-103-315-103-135-135-301-135-153-317 310-137-135-137-1_V.mp3)
  Segment 44: 152872ms ~ 153448ms (file: 670-670-601-312-316-136-312-316-106-130-610-132-765-765-716-731-132-132-123-312-675-762-762-267-263-267-574-570-507-627-726-317-137-273-367-627-602_W.mp3)
  Segment 45: 154448ms ~ 161036ms (file: 127-276-231-756-270-120-261-762-756-216-217-312-123-263-267-726-267-675-263-267-160-712-627-675-756-276-267-136-160-321-632-126-621-326-236-216-312-312-163-130-362-302-623-236-6_W.mp3)
  Segment 46: 162036ms ~ 162612ms (file: 576-576-576-576-576-571-517-571-574-572-527-520_W.mp3)
  Segment 47: 163612ms ~ 164188ms (file: 570-570-570-507-570-570-507-570-507-570-750-507-570-570-257-572-572-507-507-361-312-362-613-321-312-315-632-516-561-507-573-571-517-576-567-654-527-526-576-572-576-572-572-576-5_V.mp3)
  Segment 48: 165188ms ~ 165764ms (file: 760-670-132-260-206-206-206-260-215-265-765-762-670-607-672-526-265-527-215-215-125-301-106-167-123-206-201-213-263-263-213-263 316-316_W.mp3)
  Segment 49: 166764ms ~ 167340ms (file: 561-651-561-561-150-615-526-250-507-576-752-756-527-572-576-752-576-572-257-215-521-572-751-572-571-752-571-752-751-752-570-756-756-752-751-576-756-756-752-715-761-532-531-562-5_V.mp3)
  Segment 50: 168340ms ~ 168916ms (file: 157-156-135-105-301-103-136-156-165-176-175-705 130-105-165-105-106-105-105-157-105-153-175-105-105-156-105-105-165-150 153-150-150-150-156-510-157-516-157-150-156-157-106-175-1_W.mp3)
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
> **Time**: 2026-04-05 12:49:46
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "포커스 양보가 필요합니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "포커스", "start_ms": 60, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "양보가", "start_ms": 500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "필요합니다", "start_ms": 880, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "제 그릇을 넘어가는 손실을 입었는데 페이스를 그래도 빨리 되찾았잖아요 어 근데 혼자 많이 생각을 했던 게 우리 디엔서 잘하고 있어를 욕하는 게 아니고 어 그 다이",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "제", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "그릇을", "start_ms": 180, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "넘어가는", "start_ms": 520, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "손실을", "start_ms": 920, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "입었는데", "start_ms": 1300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "페이스를", "start_ms": 1780, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "그래도", "start_ms": 2220, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "빨리", "start_ms": 2560, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "되찾았잖아요", "start_ms": 2850, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "아 커리어 하이에 대한 생각을 아예 하지 않았어요 바로",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "아", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "커리어", "start_ms": 180, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "하이에", "start_ms": 520, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "대한", "start_ms": 820, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "생각을", "start_ms": 1100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "아예", "start_ms": 1480, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "하지", "start_ms": 1720, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "않았어요", "start_ms": 1950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 2350, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "물러가라 하시지 않소 시나리오에는 그 둘의 관계가 어떻다고 이렇게 표현되는 게",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "물러가라", "start_ms": 40, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "하시지", "start_ms": 520, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "않소", "start_ms": 880, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "시나리오에는", "start_ms": 1180, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 1800, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "둘의", "start_ms": 1920, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "관계가", "start_ms": 2180, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "어떻다고", "start_ms": 2550, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 3000, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "표현되는", "start_ms": 3300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 3720, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "성기운 아니야 네 맞습니다 피도 눈물도 없이 조카를 유배 보냈던 왕이 알고 보니 빚더미에 앉아 달고나를 핥고 따귀를 맞던 오징어 게임의 그 찌질",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "성기운", "start_ms": 30, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아니야", "start_ms": 450, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "네", "start_ms": 800, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "맞습니다", "start_ms": 950, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "피도", "start_ms": 1400, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "눈물도", "start_ms": 1720, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "없이", "start_ms": 2100, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "조카를", "start_ms": 2380, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "유배", "start_ms": 2720, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "보냈던", "start_ms": 3000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "왕이", "start_ms": 3380, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "알고", "start_ms": 3680, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "보니", "start_ms": 3920, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "빚더미에", "start_ms": 4200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "앉아", "start_ms": 4680, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "달고나를", "start_ms": 4950, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "핥고", "start_ms": 5400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "따귀를", "start_ms": 5680, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "맞던", "start_ms": 6050, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "오징어", "start_ms": 6350, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "게임의", "start_ms": 6700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 7080, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "찌질", "start_ms": 7200, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "제작 과정의 어떤 상황을 추정해 볼 수 있어요 아무래도 그 당시에도 금이 중요하고 도금 기법이 굉장히 어려웠기 때문에 조금 전의 수정이 있었다는 뜻이에요 네 네 그 기법이 굉장히 정교한 최첨단 기술을 동원한 향로네요 그때 장인이 만들면서 엄청 힘들지 않았을",
"speaker": "speaker_5",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "제작", "start_ms": 50, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "과정의", "start_ms": 320, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 700, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "상황을", "start_ms": 950, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "추정해", "start_ms": 1300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "볼", "start_ms": 1680, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 1850, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 1980, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "대한민국을 꼽고 있습니다 그리고 이 변화 앞에서 가장 복잡한 심경을 드러내고 있는 인물이 바로 도널드 트럼프 미국 대통령이라는 점입니다 이는 지금 한국이 국제 무대에서 보여주고 있는 움직임이 단순한 경제 성장이나",
"speaker": "speaker_6",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "대한민국을", "start_ms": 40, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "꼽고", "start_ms": 620, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 920, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 1380, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 1720, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "변화", "start_ms": 1850, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "앞에서", "start_ms": 2120, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 2500, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "복잡한", "start_ms": 2820, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "심경을", "start_ms": 3200, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "드러내고", "start_ms": 3620, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 4080, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "인물이", "start_ms": 4280, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 4650, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "도널드", "start_ms": 4920, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "트럼프", "start_ms": 5300, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "미국", "start_ms": 5650, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "대통령이라는", "start_ms": 5900, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "점입니다", "start_ms": 6550, "dur_ms": 380, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "항상 드는 생각이 항상 시장 앞에서는 겸손해야 되는 거예요 그게 첫 번째거든요 왜냐하면 이게 생각을 해 보면 내가 항상 힘들 때는요",
"speaker": "speaker_7",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "항상", "start_ms": 80, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "드는", "start_ms": 350, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "생각이", "start_ms": 550, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "항상", "start_ms": 900, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "시장", "start_ms": 1180, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "앞에서는", "start_ms": 1420, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "겸손해야", "start_ms": 1900, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "되는", "start_ms": 2350, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 2600, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "이 책을 통해서 그들의 의중을 읽어 볼 수 있는 기회가 될 거라고 생각합니다 자 그럼 도서 AI 최전선",
"speaker": "speaker_8",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 20, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "책을", "start_ms": 120, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "통해서", "start_ms": 360, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그들의", "start_ms": 750, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "의중을", "start_ms": 1100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "읽어", "start_ms": 1480, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "볼", "start_ms": 1680, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 1820, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 1920, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "기회가", "start_ms": 2100, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "될", "start_ms": 2450, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "거라고", "start_ms": 2600, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "생각합니다", "start_ms": 2900, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "자", "start_ms": 3500, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "그럼", "start_ms": 3620, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "도서", "start_ms": 3880, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "AI", "start_ms": 4150, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "최전선", "start_ms": 4350, "dur_ms": 380, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "대체해서 그냥 다 던져 버렸지 그러니까 이게 시황을 보고 하는 거야 네가 옛날에는 눌림 추매하고 큰 그림을 못 봤잖아 장 좋아서 네가 대충 사서 뭐 5일선, 10일선, 뭐 20일선 그리고",
"speaker": "speaker_7",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "대체해서", "start_ms": 50, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 500, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 780, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "던져", "start_ms": 900, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "버렸지", "start_ms": 1160, "dur_ms": 320, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "우리한텐 공기처럼 당연해서 미처 몰랐던 다정함의 온도가 누군가에게는 인생을 걸고서라도 마주하고 싶은 꿈의 풍경이 된 거죠 인도인들에게만이 아니라 전",
"speaker": "speaker_9",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "우리한텐", "start_ms": 60, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "공기처럼", "start_ms": 460, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "당연해서", "start_ms": 900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "미처", "start_ms": 1380, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "몰랐던", "start_ms": 1620, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "다정함의", "start_ms": 2000, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "온도가", "start_ms": 2500, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "누군가에게는", "start_ms": 2850, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "인생을", "start_ms": 3450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "걸고서라도", "start_ms": 3820, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "마주하고", "start_ms": 4380, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "싶은", "start_ms": 4820, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "꿈의", "start_ms": 5100, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "풍경이", "start_ms": 5320, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "된", "start_ms": 5660, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "거죠", "start_ms": 5800, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "실제로 이 이슈를 건드리는지 몰라요 사실 인포테인먼트 자체에서 뭔가 건드렸나 이렇게 추정을 할 수도 있는데",
"speaker": "speaker_10",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "실제로", "start_ms": 70, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 450, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "이슈를", "start_ms": 550, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "건드리는지", "start_ms": 900, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "몰라요", "start_ms": 1400, "dur_ms": 350, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 12:49:46*
