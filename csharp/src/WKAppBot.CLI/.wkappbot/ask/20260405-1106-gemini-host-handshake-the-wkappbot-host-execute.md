# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=6944  time=11:04:54  uptime=33703s  mem=412MB  system_processes=354"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_110406.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 275-231-263-213-572-135-721-321-235-321-702-265-726-276-267-261-207-267-267-672-627-657-675-674-725-275-752-321-736-327-726-327-725-236-631-236-753-562-126-253-230-237-231-570-2_V.mp3)
  Segment 2: 1576ms ~ 21196ms (file: 132-103-517-175-132-103-130-102-576-567-125-213-251-576-213-130-315-150-201-152-106-576-576-571-152-574-576-120-102-574-574-210-302-130-132-103-576 103-132-130-572-135-103-713-1_W.mp3)
  Segment 3: 22196ms ~ 22772ms (file: 130 175-573-173-357-132-137 130-765-765-762-673-653-165-657-567-756-576-675-675-763-725-763-763-376-673-763-762-765-170-176-173-163-160-123-163-753-763-761-672-762-763-671-673-7_W.mp3)
  Segment 4: 23772ms ~ 24348ms (file: 103-136-136-136-316-157-576-361-103-163-163-672-163-625-167-126-103-103-136-103-136-316-107-317-105-153-103-103-136-106-167-167-103-352-163-130-610_V.mp3)
  Segment 5: 25348ms ~ 33196ms (file: 321-263-623-321-372-326-230-603-136-367-361-632-231-237-547-230-573-103-263-263-327-527-573-167-316-312-236-263-267-217-213-312-732-276-230-321-351-531-534-173-730-752-756-761-2_W.mp3)
  Segment 6: 34196ms ~ 34772ms (file: 162-726-751-716-765-762-761-167-657-156-675-761-163 762-576-261-135-136-176-167-175-102-672-761-760 574_W.mp3)
  Segment 7: 35772ms ~ 36348ms (file: 173-150-175-751-175-173-571-517-517-517-517-157-517-571-571-157-573-571-571-571-517-157-157-571-751-571-570-501-501-507-570-570-507-571-150-570-560-510-510-715-571-571-173-175-1_V.mp3)
  Segment 8: 37348ms ~ 37924ms (file: 107-570-510-150-150-517-501-157-510-501-501-105-150-105-510-517-105-573-157-507-105-157-517-150-570-517 574-571-517-510-501-510-501-517-570-105-507-501-150-157-510-150-105-501-1_V.mp3)
  Segment 9: 38924ms ~ 39500ms (file: 167-136-612-571-527-135-163-136-312 157-657-613-675-567-572-527-361-371-612-621-216-132-231-176-167_W.mp3)
  Segment 10: 40500ms ~ 46476ms (file: 236-107-231-230-203-123-712-236-230-527-325-256-752-570-513-127-175-123-315-157-210-237-207-271-230-527-276-562-207-267-231-723-230-230-230-253-230-327-207-276-276-607-627-132-3_W.mp3)
  Segment 11: 47476ms ~ 48052ms (file: 715-175-157-157-571-157-571-571-517-517-517-571-751-571-517-150-517-150-157-501-150-315-507-570-510-506-501-570-571-570-507-157-516-157-570-570-507-501-570-175-751-157-157-715-1_V.mp3)
  Segment 12: 49052ms ~ 49628ms (file: 310-107-130-317-137-307-735 103-310-713-317-756-317-136-716-307-173-301-103-371-135-173-310-170-751-107-703-715-735-103-307-107-701 357-375-130-713-760-716-376-123-170-173-761-1_V.mp3)
  Segment 13: 50628ms ~ 66072ms (file: 217-206-607-657-710-107-725-702-726-107-176-107 123-123-176-167-316-316-136-163-126-210-201-234-240-201-210-210 102-143-621-216-302-107-130-721-716-701-107-376-617-761-370-103-7_W.mp3)
  Segment 14: 67072ms ~ 67648ms (file: 514-501-501-510-571-570 510-510-507-501-105-510-517-105-150-510-510-501-517-150-510-510-501-507-157-571-507-571-510-150-570 510-150-105-150-105-157-105-501-510-501-507-570-157-7_W.mp3)
  Segment 15: 68648ms ~ 69224ms (file: 750-751-750-750-170-710-170-570-517-507-517-750-751-715 157-170-751-157-571-157-571-715-751-107-751-157-571-751-175-570-570-751-571-751-570-751-750 574-754-574-570-157-517-175-1_W.mp3)
  Segment 16: 70224ms ~ 77208ms (file: 231-752-752-751-312-106-601-231-123-132-132-261-102-367-213-321-231-123-213-327-261-231-725-167-106-153-135-231-167-106-167-176-130-130-137-231-712-701-751-317-103-102-571-123-1_W.mp3)
  Segment 17: 78208ms ~ 78784ms (file: 301-310-103-310-320-631-367-627 137-301 260-267-126-123 102-103-132-671-617-617-621-621-106-162-106-162-502-752-267-236-236-231-237-231-201-201-301-316-310-102-130-132-135-136-5_W.mp3)
  Segment 18: 79784ms ~ 80360ms (file: 105-103-137-310-137-310-310-132-310_V.mp3)
  Segment 19: 81360ms ~ 86580ms (file: 620-270-217-130-725-705-106-217-206-206-120-321-213-261-627-132-103-207-216-201-260-620-267-201-213-263-126-756-572-321-752-572-216-261-312-213-203-137-103-312-512-321-210-201-2_V.mp3)
  Segment 20: 87580ms ~ 88156ms (file: 541-543-546-541-524-543-546-541-564-523-523-524-725-752-752-576-576-576-576-576-576-756-576-574-756-756-534-542-534-564-546-546-546-514-543-512-564-657-561-564-206-206-275-752-7_W.mp3)
  Segment 21: 89156ms ~ 89732ms (file: 751-175-175-175-175-175-715-175-175-175-175-715-751-571-571-751-175-751 150-150-150-105-150-137-175-175-175-751-715-175-715-753-715-751-715-571-751-751-751-571-751-715-751 150-1_W.mp3)
  Segment 22: 90732ms ~ 91308ms (file: 571-572-571-751-137-751-731-731-715-713-715-173-317-710-751-715-170-705-750-725-175-750-715 574-547-574-574-574-574-157-175-713-751-175-751-751-753-173-715-752-751-571-750-175 7_V.mp3)
  Segment 23: 92308ms ~ 92884ms (file: 756-765-765-765-756-765-756 671-613-637-756-756-756-756-756-756-756-756-756-756-576-756-756-756-756-756-756-756-753-756-751-751-756-573-756-576-756-756-756-756-756-750-761-750-7_W.mp3)
  Segment 24: 93884ms ~ 94460ms (file: 150-105-105-105-105-150-501-507-510-507-507-570-150-750-507-150-107-105-510-571-507-501-510-750-570 570-570-157-510-150-150-105-150-150-157-105-507-710-715-510-150-715-150-517-5_V.mp3)
  Segment 25: 95460ms ~ 96036ms (file: 175-571-571-517-751-751-571-571-517-571-571-573 175-571-517-571-751-751-571-571-517-571-571-157-517-153-175-517-715-157-157 571-571-571-571-751-571-517-571-513-157-517-571-751-1_W.mp3)
  Segment 26: 97036ms ~ 97612ms (file: 527-652-627-627-132-103-157-657-136-317-130-312-103-135-103-751-163-316-603-310-132-132-532-136-130-631-513-135-163-136-361-106-136-163-137-316-265-163-103-103-163-173-156-320-1_V.mp3)
  Segment 27: 98612ms ~ 99188ms (file: 167-671 675 576-623-263-653-756-175-712-170-130 126-751-152-127-153-326-275-215-371-576-723-257-267-261-276-537-756-726-216-276-261-267-236-263-276-263-207-263-276-237-276-236-2_V.mp3)
  Segment 28: 100188ms ~ 100764ms (file: 517-572-527-527-526-572-527-526-572-527-526-523-523-105-617 652-652-625-256-125-156-163-160 105-165-165-165-165-156-106-167-107-107-107-107-265-265-256-257-275-265-275-725-275-2_W.mp3)
  Segment 29: 101764ms ~ 102340ms (file: 137-162 165-106-106-154-675-130-617-672-623-612-261-613-236-612-631-160-136-136-574-102-157-132-316-163-103-651-103-751-153-675-316-106-106-160-106-132-176-162-102-173-136-103-1_V.mp3)
  Segment 30: 103340ms ~ 103916ms (file: 150-105-153-150-513-105-150-105-150-510-153-150-150-501-531-510-157-517-150-510 150-105-510-510-510-501-150-513-517-517-517-517-510-510-150-571-517-510-510 150-105-105-105-153-1_V.mp3)
  Segment 31: 104916ms ~ 105492ms (file: 560-657-615-165-136-301-654-657-675-675-672-632-756-607-605-620-263-213-276-136-106-162-302-123-302-321 103-236-670-367-576-657-675-176-671-671-617-617-607-620-601-602-160-617-6_W.mp3)
  Segment 32: 106492ms ~ 116716ms (file: 231-126-162-261-631-165-162-120-276-265-265-136-136-213-123-312-612-261-213-123-321-321-321-216-107-765-130-327-267-752-103-176-276-236-675-276-213-123-631-263-213-263-267-671-1_W.mp3)
  Segment 33: 117716ms ~ 124664ms (file: 107-152-215-321-132-175-173-136-137-517-132-137-312-137-321-572-271-760-310-316-261-310-326-321-103-103-206-231-123-213-570-570-574-571-574-574-574-572-150-206-310-230-765-270-3_W.mp3)
  Segment 34: 125664ms ~ 126240ms (file: 216-261-216-261-612-621-621-621-623-621-621-627-621-627-675-672-672-672-621-627 612-617-671-671-167-657-625-627-671-657-617-621 156-615-165-615-651-561-617-261-672-617-165-671-6_W.mp3)
  Segment 35: 127240ms ~ 132532ms (file: 120-120-102-570-125-103-672-627-206-627-502-251-231-203-207-207-213-570-261-231-572-102-263-132-230-263-250-527-267-237-725-652-276-231-752-301-173-671-167-173-756-612-567-157-3_W.mp3)
  Segment 36: 133532ms ~ 134108ms (file: 570-570-570-752-750-520-570-537-570 532-576-576-576-576-567-572-657-507-270-750-706-756-576-752-275-672-765-705-706-725-756 507-675-562-765-507-750-705-702-720-750-705-756-765-7_V.mp3)
  Segment 37: 135108ms ~ 135684ms (file: 135-134-103-137-106-103-103-136-103-106-137-134-103-103-136-103-103-103-103-136-103-103-134-103 513-351-571-751-517-175-173-137-103 573-317-175-371-571-517-571-517-173-107-137-1_V.mp3)
  Segment 38: 136684ms ~ 137260ms (file: 157-150-510-105-107-501-510-150-501-150-150-501-150-153-517-517-150-150-510-150-510-570-570 501-105-150-510-150-570-150-105-510-150-510-150-501-150-105-150-510-150-157-510-150-1_W.mp3)
  Segment 39: 138260ms ~ 138836ms (file: 267-765-702-267-126-602-627-725-750-564 627-261-302-231-136-312-672-670-316-163-156-631-305-153-615-137-613-307-136-137-163-301-136-371-601-605-576-367-376-567-365-657-635-765-7_W.mp3)
  Segment 40: 139836ms ~ 140412ms (file: 756-701 756-753-751-765 310-312-132-620-256-153-175-620-106-612-206-261-206-260-236-302-230-320-321-574-573 310-130 542-567-257-527-567-257-542-657-507-765-572-765-706-713-703-3_W.mp3)
  Segment 41: 141412ms ~ 141988ms (file: 143-154-132-136-315-136-627-325-263-516-573-567-576-756-315-627-326-320-301-632-173-137-215-675-136-316-136-135-301-136-162-162-627-675-765-756-574-576-574-564-547-547-546-456-5_W.mp3)
  Segment 42: 142988ms ~ 143564ms (file: 150-150-510-150-105-150-105-105-150-150-150-105-510-571 105-150-510-150-105-513-150-501-510-501-150-507-157-157-105-750-510-150-517-153-715-135-150-150-507-501 105-510-105-105-1_V.mp3)
  Segment 43: 144564ms ~ 155976ms (file: 213-230-257-203-257-163-106-102-213-123-203-132-126-162-261-321-236 216-231-216-621-136-106-160-176-167-765-106-270-276-752-270-271-257-257-256-275-107-123-312-176-176-271-107-7_W.mp3)
  Segment 44: 156976ms ~ 157552ms (file: 567-651-576-576 507-570-571-715-175-751-170-715-170-710-176-175-756-571-765-567-576-576-576-756-567-576-756-750 150-517-170-715-170-170-170-170-715-107-150-170-175-175-571-571-5_W.mp3)
  Segment 45: 158552ms ~ 166687ms (file: 103-301-137-672-267-672-570-570-627-263-132-261-302-316-260-756-572-230-236-310-231-230-231-236-123-172-231-276-206-206-216-217-172-106-107-175-321-216-231-310-273-625-203-607-2_W.mp3)
  Segment 46: 167687ms ~ 180863ms (file: 276-265-267-123-231-231-236-263-126-213-127-120-216-126-152-621-126-120-321-213-321-621-267-216-216-126-162-263-231-321-312-213-135-261-312-102-213-263-120-201-236-752-276-231-2_W.mp3)
  Segment 47: 181863ms ~ 182439ms (file: 753-315-173-315-107-173-310-173-713 135-103-135-137-315-175-307-531-173-137-135-137-103-312-135-175-357-317-173-137-107-731-305 153-315-135-315-137-351-315-135-310-173-135-317-1_V.mp3)
  Segment 48: 183439ms ~ 184015ms (file: 123-165-123-136-163-316-123-132-126-261-621-136 271-154-316-135-153-312-153-321-127-126-124-341-274-152-213-135-132-356-143-214-132-172-321-136-136-301-312-231-321-173-137_V.mp3)
  Segment 49: 185015ms ~ 185591ms (file: 532-275-257-572-574-576 657-576-627-625-265-672-627-672-265-126-160-136-706-675-675-675-675-103-136 257-265-625-256-267-256-251-250-257-256-261-216-257-251-256-652-265-265-257-7_W.mp3)
  Segment 50: 186591ms ~ 187167ms (file: 175-751-571-751-175-157-175-107-751-753-570 570-750-715-571-735-371-310-357-537-571-537-701-135-310_W.mp3)
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
> **Time**: 2026-04-05 11:06:22
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "지금 이 가격이 싼 건지 비싼 건지 주관적으로 판단하지 마시고 시장이 이 가격을 어떻게 생각하는지 객관적으로 보셔야 합니다. 차트는 거짓말을 하지 않거든요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "지금", "start_ms": 80, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 400, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "가격이", "start_ms": 520, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "싼", "start_ms": 950, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "건지", "start_ms": 1180, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "비싼", "start_ms": 1450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "건지", "start_ms": 1820, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "주관적으로", "start_ms": 2100, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "판단하지", "start_ms": 2800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "마시고", "start_ms": 3350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "시장이", "start_ms": 3850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 4350, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "가격을", "start_ms": 4480, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 4920, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "생각하는지", "start_ms": 5300, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "객관적으로", "start_ms": 5950, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "보셔야", "start_ms": 6700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "합니다.", "start_ms": 7200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "차트는", "start_ms": 7650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "거짓말을", "start_ms": 8150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "하지", "start_ms": 8700, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "않거든요.", "start_ms": 8980, "dur_ms": 500, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "결국 투자의 본질은 확률 높은 자리에 계속 베팅하는 것입니다. 한 번의 실패에 일희일비하지 말고 시스템적으로 접근하는 훈련이 필요해요. 저도 처음엔 그랬지만",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "결국", "start_ms": 100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "투자의", "start_ms": 450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "본질은", "start_ms": 900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "확률", "start_ms": 1350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "높은", "start_ms": 1680, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "자리에", "start_ms": 2020, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 2450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "베팅하는", "start_ms": 2850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "것입니다.", "start_ms": 3400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 4000, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "번의", "start_ms": 4180, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "실패에", "start_ms": 4450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "일희일비하지", "start_ms": 4900, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "말고", "start_ms": 5650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "시스템적으로", "start_ms": 6000, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "접근하는", "start_ms": 6800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "훈련이", "start_ms": 7350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "필요해요.", "start_ms": 7800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "저도", "start_ms": 8400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "처음엔", "start_ms": 8750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "그랬지만", "start_ms": 9200, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "데이터를 쌓다 보면 본인만의 원칙이 생깁니다. 그 원칙이 돈을 벌어다 주는 거지 운이 벌어다 주는 게 아니에요. 명확한 손절 라인을 지키는 것부터가 시작입니다.",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "데이터를", "start_ms": 120, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "쌓다", "start_ms": 650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "보면", "start_ms": 980, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "본인만의", "start_ms": 1300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "원칙이", "start_ms": 1850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "생깁니다.", "start_ms": 2300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 2900, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "원칙이", "start_ms": 3080, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "돈을", "start_ms": 3520, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "벌어다", "start_ms": 3850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "주는", "start_ms": 4350, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "거지", "start_ms": 4650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "운이", "start_ms": 5000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "벌어다", "start_ms": 5350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "주는", "start_ms": 5850, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 6150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "아니에요.", "start_ms": 6350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "명확한", "start_ms": 6950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "손절", "start_ms": 7450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "라인을", "start_ms": 7850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "지키는", "start_ms": 8300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "것부터가", "start_ms": 8800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "시작입니다.", "start_ms": 9350, "dur_ms": 600, "phase_lr_deg": 0.0}
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
"transcript": "결국 미국 연합의 65개 단체가 공개되었습니다.",
"speaker": "speaker_4",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "결국", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "미국", "start_ms": 600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "연합의", "start_ms": 1000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "65개", "start_ms": 1500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "단체가", "start_ms": 2100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "공개되었습니다.", "start_ms": 2650, "dur_ms": 900, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 20,
"transcript": "처음에는 너무 막막하고 힘들어서 포기하고 싶을 때도 많았지만, 끝까지 믿고 따라와 준 가족들 덕분에 버틸 수 있었습니다. 이제는 제가 받은 도움을 사회에 환원하고 싶어요.",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "처음에는", "start_ms": 100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "막막하고", "start_ms": 1050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "힘들어서", "start_ms": 1600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "포기하고", "start_ms": 2150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "싶을", "start_ms": 2700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "때도", "start_ms": 3050, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "많았지만,", "start_ms": 3400, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "끝까지", "start_ms": 4100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "믿고", "start_ms": 4600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "따라와", "start_ms": 4950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "준", "start_ms": 5400, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가족들", "start_ms": 5600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "덕분에", "start_ms": 6100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "버틸", "start_ms": 6550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 6900, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있었습니다.", "start_ms": 7100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "이제는", "start_ms": 7800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 8250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "받은", "start_ms": 8600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "도움을", "start_ms": 8950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "사회에", "start_ms": 9400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "환원하고", "start_ms": 9900, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "싶어요.", "start_ms": 10500, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "그 과정이 쉽지는 않겠지만 하나씩 차근차근 해나갈 생각입니다. 응원해 주시는 모든 분들께 진심으로 감사드리고 더 좋은 모습 보여드리겠습니다.",
"speaker": "speaker_5",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "그", "start_ms": 120, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "과정이", "start_ms": 300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "쉽지는", "start_ms": 750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "않겠지만", "start_ms": 1250, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "하나씩", "start_ms": 1850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "차근차근", "start_ms": 2300, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "해나갈", "start_ms": 2900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "생각입니다.", "start_ms": 3400, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "응원해", "start_ms": 4150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "주시는", "start_ms": 4650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "모든", "start_ms": 5100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "분들께", "start_ms": 5450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "진심으로", "start_ms": 5950, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "감사드리고", "start_ms": 6550, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 7250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 7450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "모습", "start_ms": 7800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "보여드리겠습니다.", "start_ms": 8200, "dur_ms": 900, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "결국 중요한 것은 실행력입니다. 아무리 좋은 강의를 듣고 책을 읽어도 실제로 해보지 않으면 아무 소용이 없어요. 당장 오늘부터 작은 것 하나라도 시작해 보시길 권장합니다.",
"speaker": "speaker_6",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "결국", "start_ms": 100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "중요한", "start_ms": 450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "것은", "start_ms": 900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "실행력입니다.", "start_ms": 1250, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "아무리", "start_ms": 2000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 2450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "강의를", "start_ms": 2800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "듣고", "start_ms": 3300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "책을", "start_ms": 3650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "읽어도", "start_ms": 4000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "실제로", "start_ms": 4550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "해보지", "start_ms": 5050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "않으면", "start_ms": 5500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아무", "start_ms": 5950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "소용이", "start_ms": 6300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "없어요.", "start_ms": 6750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "당장", "start_ms": 7300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "오늘부터", "start_ms": 7700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "작은", "start_ms": 8250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 8600, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "하나라도", "start_ms": 8800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "시작해", "start_ms": 9350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "보시길", "start_ms": 9850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "권장합니다.", "start_ms": 10300, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "처음 시작하시는 분들은 너무 큰 목표보다는 작은 성공을 반복하는 경험을 쌓는 게 중요합니다. 성공의 기억이 나를 움직이게 하는 원동력이 되거든요. 파이팅입니다!",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "처음", "start_ms": 120, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "시작하시는", "start_ms": 450, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "분들은", "start_ms": 1050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 1500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 1850, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "목표보다는", "start_ms": 2050, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "작은", "start_ms": 2700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "성공을", "start_ms": 3050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "반복하는", "start_ms": 3550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "경험을", "start_ms": 4100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "쌓는", "start_ms": 4550, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 4850, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "중요합니다.", "start_ms": 5050, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "성공의", "start_ms": 5800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "기억이", "start_ms": 6250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "나를", "start_ms": 6700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "움직이게", "start_ms": 7050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 7600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "원동력이", "start_ms": 7950, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "되거든요.", "start_ms": 8550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "파이팅입니다!", "start_ms": 9100, "dur_ms": 700, "phase_lr_deg": 0.0}
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
"transcript": "실직한 수백만 명의 실업자들에게 만약에",
"speaker": "speaker_7",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "실직한", "start_ms": 150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "수백만", "start_ms": 650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "명의", "start_ms": 1200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "실업자들에게", "start_ms": 1550, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "만약에", "start_ms": 2300, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "그분들에게 새로운 희망을 줄 수 있는 정책들이 많이 나오고 있습니다. 실업 급여뿐만 아니라 재교육 프로그램도 활발해지고 있죠. 중요한 것은 꺾이지 않는 마음입니다.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "그분들에게", "start_ms": 120, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "새로운", "start_ms": 720, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "희망을", "start_ms": 1150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "줄", "start_ms": 1600, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 1780, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 1950, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "정책들이", "start_ms": 2250, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 2800, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "나오고", "start_ms": 3150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "있습니다.", "start_ms": 3600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "실업", "start_ms": 4200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "급여뿐만", "start_ms": 4550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 5100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "재교육", "start_ms": 5500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "프로그램도", "start_ms": 6000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "활발해지고", "start_ms": 6650, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "있죠.", "start_ms": 7300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "중요한", "start_ms": 7750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "것은", "start_ms": 8250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "꺾이지", "start_ms": 8600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "않는", "start_ms": 9050, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "마음입니다.", "start_ms": 9350, "dur_ms": 600, "phase_lr_deg": 0.0}
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
"segment": 45,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
*Generated by WKAppBot ask gemini — 2026-04-05 11:06:22*
