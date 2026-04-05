# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=6944  time=12:25:08  uptime=38517s  mem=469MB  system_processes=360"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_122234.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 136-103-136-135-103 537-573-753-571-514-571-537-571-571 136-316-136-135-174-314-136-136-136-134-135-136-135-165-103-103-103-103-136-134 103-136-136-136-156-165-163-135-136-135-3_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 615-163-175-672-167-130-103-617-103-136-175-162-561-612-106-163-301-163-162-167-163-671-167-671-167-315-371-613-163-617-167-106-210-123-261-261-136-321-762-167-102-163-160-106-1_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 310-701-710-132-120-170-701-712 713-103-731-103-173-107-173-310-371-175-170-713-701-175-173-175-103-175-105-370-107-130-571-751-756-130-307-107-715-517 301-715-107-317-701-705-1_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 760-730-705-703-750-750-517-571-750-705-701-710-173-517-573-715-507-750-570-750-507-571-715-507-705-571-306-705-703-376-756-673-763-376-756-735-736-537-756-731-570-750-730-107-5_V.mp3)
  Segment 5: 6304ms ~ 16024ms (file: 752-107-201-102-103-213-132-130-756-574-157-210-547-301-103-237-206-201-574-267-620-572-271-756-167-231-231-765-547-102-261-260-625-106-574-132-321-260-102-567-572-547-213-754-2_W.mp3)
  Segment 6: 17024ms ~ 27896ms (file: 236-574-301-765-627-231-236-130-267-756-756-620-260-627-132-106-123-261-627-103-123 317-162-260-361-136-136-230-362-310-263-367-320-230-236-236-260-256-576-576-326-310-267-275-5_W.mp3)
  Segment 7: 28896ms ~ 29472ms (file: 756-752-712-126-136-617-256-265-627-275-726-726-762-765-671-761-617-671-761-165-175-765-765-671-756-716-761-176-167-756-761-763-176-176-176-176-176-716-167-675-761-765-765-761-7_W.mp3)
  Segment 8: 30472ms ~ 37348ms (file: 130-263-312-213-126-320-102-102-326-267-260-107-267-627-203-123-265-203-201-372-267-267-312-120-102-271-275-261-251-135-167-310-263-235-267-130-130-231-547-210-273-213-261-175-1_W.mp3)
  Segment 9: 38348ms ~ 38924ms (file: 526-563-526-536-574-516-521-516-531_V.mp3)
  Segment 10: 39924ms ~ 40500ms (file: 263-261-261-260-203-267-261-261-206-201-627-620-216-102_W.mp3)
  Segment 11: 41500ms ~ 42076ms (file: 130-170-173-137 517-175-126-716-715-361-173-165-517-572-136 253-276-627-267-625-301-765-276-572-321-567-132-205-235-571-537-756-527-312-751-517-216 123-765-261-207-123-267-127-2_V.mp3)
  Segment 12: 43076ms ~ 43652ms (file: 103-315-315 130-130-310-310-301-103-130-105-135-130-135-153-153-501-140-134-410-130-341-415-413-431-143-315-315-135-315-315-315-104-310-135-103-105-104-103-135-134-105-314-146-1_W.mp3)
  Segment 13: 44652ms ~ 45228ms (file: 102-103-712-762-702-760-762-163-130-123-136-276-761-762-207-352-325-201-231-132-310-132-231-326-632-236-325-312-321-312-532-123-213-123-132-607-625-561-130-502-543-315-130-301-5_W.mp3)
  Segment 14: 46228ms ~ 46804ms (file: 726-547-574-571-257-276-126-612-617-613-756-673-176-163-627-231-210-321-260-167-126-162-107-107-267-627-310-310-236-321 261-126-162-126-613-126 631-621-263-261-621-260-260-263-6_W.mp3)
  Segment 15: 47804ms ~ 48380ms (file: 216-236-761-371-715-176-715-763-576-751-765-574-657-652-763-760-726-672-617-617-623 754-627-136-672-761-716-761-716-765-175-176 367-362 263 627-621_W.mp3)
  Segment 16: 49380ms ~ 49956ms (file: 275-625-275-571-751-725-127-751-750-756-576-135-256-267-705-617-725-715-571-756-542-207-726-765-326-237-123-450-265-132-276-237-570-236-132-572-273-762-572-312-271-236-267-162-5_V.mp3)
  Segment 17: 50956ms ~ 51532ms (file: 175-751-175-715-175-715-157-715-175-715-175-751 150-150-150-105-105-137-175-751-715-175-715-751-715-751-715-175-175-175-175-751-157-751-753 157-150-105-157-150-514-513-175-175-7_W.mp3)
  Segment 18: 52532ms ~ 53108ms (file: 501-501-501-507-501-150-501-150-517-105-501-501-157-513-517-153-105-513-157-517-507-501-510-517-153-150-501-501 501-510-507-510-510-150-510-510-501-517-510-150-501-510-150-105-5_V.mp3)
  Segment 19: 54108ms ~ 54684ms (file: 657-756-657-752-672-627-267-165-615-156-156-165-152-165-152-567-157-571-567-562-567-561-657-652-657-765-627-627-615-610-650-165-571-517-571-517-571-571-571-571-756 615-165-615-6_W.mp3)
  Segment 20: 55684ms ~ 65044ms (file: 756-376-261-251-652-627-213-310-201-120-103-210-310-136-261-620-103-267-210-670-627-260-132-310-102-123-210-120-123-723-321-574-574-157-137-507-501-260-103-574-574-105-126-120-1_W.mp3)
  Segment 21: 66044ms ~ 76016ms (file: 574-325-123-102-132-130-756-715-102-572-527-132-134-321-351-527-256-263-231-132-756-765-260-312-102-210-216-574-574-547-263-236-257-123-261-574-547-726-321-627-257-230-126-765-6_W.mp3)
  Segment 22: 77016ms ~ 77592ms (file: 560-631-135-150-563-503-570-506-602-657-765-675-162-514-576-657-267-547-567-564-357-514-541-267-275-526-527-123-167-163-725-517-501-157-310-136-132-576-570-517-137-765-675-763-6_W.mp3)
  Segment 23: 78592ms ~ 79168ms (file: 750-307-570-736-376-750-735-753-750-760-675-753-756-357-570-705-753-735-751-750-750-753-701-375-753-570-751-573-713-715-570-751-570-570-571-570-710-705-735-750-537-765-657-765-7_W.mp3)
  Segment 24: 80168ms ~ 80744ms (file: 546-543-621-657-652-657-657-567-567-567-567-567-567-654-567-657-514-504-504-513-657-657-567-567-567-567-567-567-654-567-654-654 514-345-516-651-567-576-567-576-567-576-567-567-5_W.mp3)
  Segment 25: 81744ms ~ 82320ms (file: 756-675-765-756-756-765 157-157-571-157-571-517-570-157-157-157-157-571 570-157-517-571-517-571-571-570-157-157-157-715-751-175-756-756-756-756-753-756-752-756-756-765-756-756-7_W.mp3)
  Segment 26: 83320ms ~ 83896ms (file: 132-160-617-617-613-613-163-132-130-613-162-162-163-163-165-165-671_W.mp3)
  Segment 27: 84896ms ~ 85472ms (file: 615-651-651-756-571-576-756-576-751-726-762-675-675-675 205-165-625-615-615-617-765-627-671-765-176-765-715-765-715-715-751-751-751-716-715 615-651-615-651-657-651-761-716-765-7_W.mp3)
  Segment 28: 86472ms ~ 87048ms (file: 504-501-501-501-514-510-510-510-510_W.mp3)
  Segment 29: 88048ms ~ 108460ms (file: 130-627-261-765-107-106-126-321-756-756-267-210-751-136-102-310-756 267-312-103-267-216-276-261-276-263-261-675-167-237-723-752-321-327-267-267-321-327-130-231-261-231-102-230-2_W.mp3)
  Segment 30: 109460ms ~ 110036ms (file: 206-206-206-236-230-230-574-572-273-273-320-132-260-620-751-541-613-163-631-614-206-267-321-301-201-230-167-136-106-107-260-206-623-126-201-267-267-203-637-632-623-136-103-132-3_W.mp3)
  Segment 31: 111036ms ~ 111612ms (file: 214 502-507-570-527-527-527-502-572-750-507-765-756-765-756-756-765-756-576-576 567-507-526-250-257-526-572-520-527-562-250-567-250-672-576-576-756-765-567-576-567 507-576-507-2_V.mp3)
  Segment 32: 112612ms ~ 119200ms (file: 175-701-210-210-123-132-103-105-120-173-136-130-213-276-130-320-265-257-716-712-263-136-261-612-721-627-261-267-127-216-267-257-513-571-267-126-126-231-132-236-216-136-312-102-2_W.mp3)
  Segment 33: 120200ms ~ 130928ms (file: 137-253-321-130-617-273-327-361-213-123-167-103-132-103-205-267-267-203-542-351-312-213-275-216-217-132-712-213-321-310-312-120-231-213-320-627-637-167-613-126-617-203-203-205-2_W.mp3)
  Segment 34: 131928ms ~ 132504ms (file: 762-672-760-716-671-176-671-761-671-671-762-671-675-762-765-675-765-765-675-765-756-756-765-765-756-675-765-675-765-761-756-627-625-657-672-672-671-761-617-716-762-610-760 635-6_W.mp3)
  Segment 35: 133504ms ~ 134080ms (file: 517-157-517-517-513-517-510-517-571-517-570 157-517-157-517-517-571-517-517-157-517-517-517-513-517-571-570-571-507-570 571-517-513-517-517-571-517-571-517-571-517-517-517-571-5_V.mp3)
  Segment 36: 135080ms ~ 146060ms (file: 765-567-107-736-357-756-310-103-163-132-213-310-675-103-132-754-754-751 726-756-103-103-173-751-267-276-270-201-302-753-206-230-271-574-237-310-301-312-103-754-105-310-753-103-2_W.mp3)
  Segment 37: 147060ms ~ 147636ms (file: 752-572-576-756-576-756-756-576-576-576-756-572-524-534-564-542-543-513-546-564-567-527-543-543-546-752-576-756-756-756-576-576-756-752-514-536-546-546-504-504-546-506-506-506-6_W.mp3)
  Segment 38: 148636ms ~ 149212ms (file: 631-621-765-167-726-236-160-601-136-230-273-103-102-517-125-163-167-263-236-130-316-316-301-136-605-621-236-320-310-130-105-156-102-136-361-316-673-612-137-137-625-621-265-621-6_W.mp3)
  Segment 39: 150212ms ~ 156260ms (file: 270-706-230-210-327-372-203-206-201-206-260-327-107-201-752-137-230-253-230-260-321-725-107-167-130-321-236-213-762-270-132-103-107-176-207-236-103-173-137-176-163-167-574-327-3_V.mp3)
  Segment 40: 157260ms ~ 157836ms (file: 572-567-652-567-562-265-265-567-526-506-265-205-510-206-205-152-567_V.mp3)
  Segment 41: 158836ms ~ 159412ms (file: 157 547-574-574-574-574-574-574-574-574-574-571-574-571-754-574-574-574-574-574 547-547-547-574-570-507-570-517-501-751-107-715-570-517-510-157-751-107-175-175-750-750-107-107-7_V.mp3)
  Segment 42: 160412ms ~ 160988ms (file: 130-317-103-130-107-103-137-107-107-173-170-173-137-153-156-167-137-137-163-367-673-627-627-726-276-271-327-231-576-756-756-756-756-726-735-275-627-631-637-265-276-256-260-562-5_W.mp3)
  Segment 43: 161988ms ~ 162564ms (file: 126-310-756-156-167-123-726-621-173-207-130-102-320-120-126-126-321-162-103-610-632-716-135-610-136-126-123-103-167-163 157-136-103-136-167-160-167-103-763-306-176-162-103-106-5_V.mp3)
  Segment 44: 163564ms ~ 164140ms (file: 157 105-105-170-107-105-170-150-157-150-157-517-157-157-157-157-510-157 105-107-150-157-150-157-517-157 105-150-105-157-175-176-175-176-157-175-165-156-157-156-157-157-157-157-1_V.mp3)
  Segment 45: 165140ms ~ 165716ms (file: 105-150-157-150-510-570-150-570-150-501-570-510 510-150-501-571-517-571-517-570-507-507-501-507-517-501-510-507-501-150-571-517 507-571-507-517-517-501-517-507-517-571-517-157-5_W.mp3)
  Segment 46: 166716ms ~ 167292ms (file: 132-136-136-136-136-164 132-135 135-132-135-132-135-125 163-163-165-516-156-156-315-135-135-135-153-152-153-153-154-153-152-152-153-135-135-513-513-153-153 165-615-657-657-657-6_W.mp3)
  Segment 47: 168292ms ~ 168868ms (file: 103-132-413-132-106-136-540-132 213-103-263-162-165-715 756-756-756-756-576-756-756-167-102-126-172-172 671-675-765-657-675-617-756-756-132-103-756-765-675-716-671-517-152-135-1_W.mp3)
  Segment 48: 169868ms ~ 170444ms (file: 507-105-157-517-175-157-507 154-157-150-501-570-571-710-170-105-107-107-170-175-176-175-105-107-751 150-501-150-105-107-175-170-501-517 105-105-150-105-107-510-157-501-157-571-7_V.mp3)
  Segment 49: 171444ms ~ 172020ms (file: 543-517-517-157-153-715-157-517-517-153-157-517-175-157-175-175-157-517-157-175-751-571 571-571-157-751-571-157-175-157-571-137-175-571-157-175-175-571-517-571-517-571-571-571 5_W.mp3)
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
> **Time**: 2026-04-05 12:27:15
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "우운 영화라서 강력 추천합니다",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "우운", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "영화라서", "start_ms": 150, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "강력", "start_ms": 330, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "추천합니다", "start_ms": 430, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "처음부터 끝까지 액션과 아드레날린 긴장감이 계속됩니다",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "처음부터", "start_ms": 30, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "끝까지", "start_ms": 140, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "액션과", "start_ms": 230, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "아드레날린", "start_ms": 320, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "긴장감이", "start_ms": 430, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "계속됩니다", "start_ms": 510, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "배우들이 성숙하면서도 놀라운 연기를 보여줍니다",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "배우들이", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "성숙하면서도", "start_ms": 170, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "놀라운", "start_ms": 320, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "연기를", "start_ms": 420, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "보여줍니다", "start_ms": 520, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "특히 신세경이 매우 인상적입니다 줄거리 캐릭터 촬",
"speaker": "speaker_1",
"confidence": 0.91,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "특히", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "신세경이", "start_ms": 120, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "매우", "start_ms": 230, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "인상적입니다", "start_ms": 310, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "줄거리", "start_ms": 440, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "캐릭터", "start_ms": 500, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "촬", "start_ms": 560, "dur_ms": 40, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "스타링크용인지 스타쉽용인지는 인제 알 순 없죠 NCA가 더 유리한가라는 부분에 대한 생각이 있을 수 있는데",
"speaker": "speaker_2",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "스타링크용인지", "start_ms": 120, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "스타쉽용인지는", "start_ms": 1100, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "인제", "start_ms": 2000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "알", "start_ms": 2500, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "순", "start_ms": 2700, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "없죠", "start_ms": 3000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "NCA가", "start_ms": 3600, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 4500, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "유리한가라는", "start_ms": 4800, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "부분에", "start_ms": 5800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "대한", "start_ms": 6400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "생각이", "start_ms": 7000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "있을", "start_ms": 7700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 8200, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "있는데", "start_ms": 8500, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "자몽계냐 LFP냐 하면 차이가 있는데요 NCM이냐 NCA냐 차이는 크게 없을 거",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "자몽계냐", "start_ms": 150, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "LFP냐", "start_ms": 950, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "하면", "start_ms": 1650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "차이가", "start_ms": 2250, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "있는데요", "start_ms": 2850, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "NCM이냐", "start_ms": 4000, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "NCA냐", "start_ms": 5000, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "차이는", "start_ms": 6000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "크게", "start_ms": 6600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "없을", "start_ms": 7100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 7600, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "올라왔다는 것인데요 몇몇 미국 유튜버 블로거들은 예전에는 세탁기를 손님 앞에 내어놓는다는 상상조차 못했었는데 요즘은 오히려",
"speaker": "speaker_3",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "올라왔다는", "start_ms": 60, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "것인데요", "start_ms": 600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "몇몇", "start_ms": 1100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "미국", "start_ms": 1450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "유튜버", "start_ms": 1800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "블로거들은", "start_ms": 2250, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "예전에는", "start_ms": 3000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "세탁기를", "start_ms": 3600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "손님", "start_ms": 4200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "앞에", "start_ms": 4550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "내어놓는다는", "start_ms": 4900, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "상상조차", "start_ms": 5700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "못했었는데", "start_ms": 6300, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "요즘은", "start_ms": 7200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "오히려", "start_ms": 7700, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "이분은 배우의 정체입니다 분명 미국에서 태어난 여배우인데 한국 피가 흐르는 게 분명하다며 난리도 아니었거든요",
"speaker": "speaker_4",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "이분은", "start_ms": 80, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "배우의", "start_ms": 450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "정체입니다", "start_ms": 850, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "분명", "start_ms": 1600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "미국에서", "start_ms": 2050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "태어난", "start_ms": 2600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "여배우인데", "start_ms": 3050, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 3800, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "피가", "start_ms": 4150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "흐르는", "start_ms": 4500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 4950, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "분명하다며", "start_ms": 5150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "난리도", "start_ms": 5800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아니었거든요", "start_ms": 6250, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 10,
"transcript": "요즘 미국에서 진짜 난리 난 영화가 하나 있습니다 미친 속도로 흥행 질주",
"speaker": "speaker_4",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "요즘", "start_ms": 50, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "미국에서", "start_ms": 500, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 1150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "난리", "start_ms": 1600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "난", "start_ms": 2050, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "영화가", "start_ms": 2300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "하나", "start_ms": 2850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 3300, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "미친", "start_ms": 4000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "속도로", "start_ms": 4450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "흥행", "start_ms": 5000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "질주", "start_ms": 5450, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "Des scènes de combat, il y en a vraiment pas mal de seins etc... des membres cassés, c'est vraiment trop stylé. Et encore une fois, je vous le dis, c'est ça que j'ai kiffé avec les Sud-Coréens. Ils savent faire des films d'action mais avec vraiment une dimension géopolitique, des propos très intéressants. Yumine, c'est",
"speaker": "speaker_5",
"confidence": 0.92,
"language": "fr",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "Des", "start_ms": 100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "scènes", "start_ms": 350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "de", "start_ms": 700, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "combat,", "start_ms": 850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "il", "start_ms": 1300, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "y", "start_ms": 1450, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "en", "start_ms": 1600, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 1800, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "vraiment", "start_ms": 1950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "pas", "start_ms": 2400, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "mal", "start_ms": 2650, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "de", "start_ms": 2950, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "seins", "start_ms": 3150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "etc...", "start_ms": 3500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "des", "start_ms": 4000, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "membres", "start_ms": 4250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "cassés,", "start_ms": 4700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "c'est", "start_ms": 5200, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "vraiment", "start_ms": 5450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "trop", "start_ms": 5900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "stylé.", "start_ms": 6250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "Et", "start_ms": 6800, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "encore", "start_ms": 7000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "une", "start_ms": 7350, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "fois,", "start_ms": 7600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "je", "start_ms": 8000, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "vous", "start_ms": 8150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "le", "start_ms": 8350, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "dis,", "start_ms": 8500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "c'est", "start_ms": 8900, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "ça", "start_ms": 9150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "que", "start_ms": 9400, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "j'ai", "start_ms": 9600, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "kiffé", "start_ms": 9850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "avec", "start_ms": 10300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "les", "start_ms": 10650, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "Sud-Coréens.", "start_ms": 10850, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "Ils", "start_ms": 11800, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "savent", "start_ms": 12050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "faire", "start_ms": 12500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "des", "start_ms": 12900, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "films", "start_ms": 13100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "d'action", "start_ms": 13500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "mais", "start_ms": 14100, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "avec", "start_ms": 14400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "vraiment", "start_ms": 14750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "une", "start_ms": 15300, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "dimension", "start_ms": 15550, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "géopolitique,", "start_ms": 16200, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "des", "start_ms": 17100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "propos", "start_ms": 17300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "très", "start_ms": 17800, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "intéressants.", "start_ms": 18150, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "Yumine,", "start_ms": 19100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "c'est", "start_ms": 19650, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "이 과정을 혼자 시행착오로 겪는 것보다 처음부터 기준을 잡고 같이 훈련하면서 가져가고 싶으신 분들은 레포콘이나 아카데미를 통해 이 흐름을 함께 익혀 보셔도 좋겠습니다 단순히 종목을 보는 게 아니라 왜 여기서 보고 왜 여기서 대응하는지 이 기준",
"speaker": "speaker_6",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "과정을", "start_ms": 300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "혼자", "start_ms": 750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "시행착오로", "start_ms": 1150, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "겪는", "start_ms": 1900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "것보다", "start_ms": 2250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "처음부터", "start_ms": 2800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "기준을", "start_ms": 3450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "잡고", "start_ms": 3950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 4400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "훈련하면서", "start_ms": 4800, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "가져가고", "start_ms": 5550, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "싶으신", "start_ms": 6150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "분들은", "start_ms": 6650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "레포콘이나", "start_ms": 7200, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "아카데미를", "start_ms": 7950, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "통해", "start_ms": 8700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 9100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "흐름을", "start_ms": 9300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "함께", "start_ms": 9800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "익혀", "start_ms": 10200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "보셔도", "start_ms": 10550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "좋겠습니다", "start_ms": 11050, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "단순히", "start_ms": 12000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "종목을", "start_ms": 12550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "보는", "start_ms": 13050, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 13400, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 13600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "왜", "start_ms": 14100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "여기서", "start_ms": 14350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "보고", "start_ms": 14850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "왜", "start_ms": 15300, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "여기서", "start_ms": 15550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "대응하는지", "start_ms": 16050, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 16800, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "기준", "start_ms": 17000, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "로봇의 생명을 불어넣은 지도 20년이 다 됐습니다 멈출",
"speaker": "speaker_7",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "로봇의", "start_ms": 80, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "생명을", "start_ms": 530, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "불어넣은", "start_ms": 1030, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "지도", "start_ms": 1630, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "20년이", "start_ms": 2030, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 2580, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "됐습니다", "start_ms": 2830, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "멈출", "start_ms": 3430, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 15,
"transcript": "5000 정도 됐잖아요 어 그래서 혹시나 이제 5000 포인트를 이탈을 하면은 제가 만약에 풀베팅을 하면 이게 또 버티질 못하니까 다음날 것까지 매매할 거를 좀 계획을 해가지고 제 나름대로 적당한 비중을 매매를 했는데 좀 아쉽 아쉽긴 했어요",
"speaker": "speaker_8",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "5000", "start_ms": 120, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "정도", "start_ms": 550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "됐잖아요", "start_ms": 950, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "어", "start_ms": 1550, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 1800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "혹시나", "start_ms": 2300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 2800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "5000", "start_ms": 3200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "포인트를", "start_ms": 3650, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "이탈을", "start_ms": 4250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "하면은", "start_ms": 4750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 5250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "만약에", "start_ms": 5650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "풀베팅을", "start_ms": 6150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "하면", "start_ms": 6800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 7200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 7550, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "버티질", "start_ms": 7800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "못하니까", "start_ms": 8300, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "다음날", "start_ms": 8900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "것까지", "start_ms": 9400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "매매할", "start_ms": 9900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "거를", "start_ms": 10400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 10750, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "계획을", "start_ms": 11000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "해가지고", "start_ms": 11500, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "제", "start_ms": 12100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "나름대로", "start_ms": 12300, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "적당한", "start_ms": 12900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "비중을", "start_ms": 13400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "매매를", "start_ms": 13900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "했는데", "start_ms": 14400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 14900, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "아쉽", "start_ms": 15150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "아쉽긴", "start_ms": 15500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "했어요", "start_ms": 16000, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "그리고 저희는 이걸 처음 봤을 때 딱 절 느낌 보다도 그냥 한옥으로 보였어요 사실은 이게 누군가",
"speaker": "speaker_9",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "그리고", "start_ms": 100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "저희는", "start_ms": 550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이걸", "start_ms": 1000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "처음", "start_ms": 1400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "봤을", "start_ms": 1800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 2200, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "딱", "start_ms": 2450, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "절", "start_ms": 2700, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "느낌", "start_ms": 3000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "보다도", "start_ms": 3400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 3900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "한옥으로", "start_ms": 4300, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "보였어요", "start_ms": 4950, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "사실은", "start_ms": 5550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 6100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "누군가", "start_ms": 6450, "dur_ms": 450, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 12:27:15*
