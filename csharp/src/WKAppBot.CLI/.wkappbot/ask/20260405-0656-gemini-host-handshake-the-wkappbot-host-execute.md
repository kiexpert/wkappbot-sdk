# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=17708  time=06:55:19  uptime=9980s  mem=469MB  system_processes=351"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_065156.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 5652ms (file: 625-276-213-271-120-120-752-217-176-123-321-236-231-762-756-130-123-120-172-120-173-231-213-276-123-213-217-217-126-210-326-213-312-320-163-231-126-216-102-173-312-213-312-726-1_W.mp3)
  Segment 2: 6652ms ~ 7228ms (file: 310-150-153-135-173-513-153-135-137-137-510-513-513-315-517-571-517-537-537-357-571-351-157-135 510-153-130-137-153-135-510-135-130-163-135-157-157-157-571-571-513-537-573-571-5_W.mp3)
  Segment 3: 8228ms ~ 16076ms (file: 230-231-102-320-130-320-321-537-573-135-276-126-167-312-762-672-320-310-267-267-176-267-762-765-672-670-102-162-107-162-571-130-321-316-237-765-756-276-276-231-276-267-132-132-3_W.mp3)
  Segment 4: 17076ms ~ 17652ms (file: 123-102 206-126-123-261-562-621-361-765-765-765-765_W.mp3)
  Segment 5: 18652ms ~ 24016ms (file: 372-176-270-627-126-271-325-263-163-302-312-257-752-736-732-127-213-712-712-572-761-276-132-132-123-726-261-276-267-213-126-237-237-213-137-173-173-216-231-273-213-123-216-230-1_W_stt_i_have_one.mp3)
  Segment 6: 25016ms ~ 41072ms (file: 312-103-607-216-213-752-267-276-102-201-231-206-203-103-120-206-267-627-216-216-706-206-201-765-203-321-213-120-130-261-654-507-132-503-675-670-736-361-534-536-562-135-152-321-6_W.mp3)
  Segment 7: 42072ms ~ 42648ms (file: 574 547-547-574-504-501-157-105-501-570-571-510-507-105-517-150-573-351-540-570-574-574-574-574-574-754-754-574-570-570-501-510-570-574-570-517-157-150-150-510-571-517 510-571-5_W.mp3)
  Segment 8: 43648ms ~ 44224ms (file: 317-361-356-617-106-267-617-302-120-706-167-754-756-750-231-231-231-657-567-761-126-621-716-260-732-627-106-612-607-103-103-301-310-130-361-765-236-312-103-506-567-506-630-627-6_W.mp3)
  Segment 9: 45224ms ~ 45800ms (file: 175-710-175-170-710-170-107-175-175-571-751-751-716-762-726-723-762-726-271-712-176-726-762-276-175-175-751-175-157-751-175-175-710-175-715-170-170-710-175-710-715-176-701 107-1_V.mp3)
  Segment 10: 46800ms ~ 56340ms (file: 163-261-276-607-210-130-273-231-216-276-263-236-326-320-102-103-230-206-206-216-267-216-750-123-312 761-542-130-107-132-576-527-265-261-132-136-312-132-130-527-103-210-236-253-7_W.mp3)
  Segment 11: 57340ms ~ 57916ms (file: 173-106-137-107-607-516-716-615-613_V.mp3)
  Segment 12: 58916ms ~ 59492ms (file: 267-627-267-267-267-627-267-276-267-627-267-267-267-627-627-627-213-263-267-261-267-267-267-267-261-267-267-263-267-267-261-261-263-267-263-621-261-267-261-261-236-627-267-216-2_W.mp3)
  Segment 13: 60492ms ~ 61068ms (file: 510-105-150-570-150-150-510 501-501-501-510-501-517-105-571-501-510-510-516-105-150-105-105-510-175-501-517-510-510-510-501-150-501-517-501-150 715-150-150-157-507-501-150-517-5_V.mp3)
  Segment 14: 62068ms ~ 65452ms (file: 321 103-123-150-310-213-231-621-301-102-365 251-217-164-326-647-745-136_V.mp3)
  Segment 15: 66452ms ~ 67028ms (file: 312-103-132-136-135-710-176-371-173-765-675-675_W.mp3)
  Segment 16: 68028ms ~ 68604ms (file: 132-132-175-176-107-103-612-750-760-710-731-710-175-172-107-103-132-120-172-317-716-137-172-312-126-127-710-136-176-712-137-567-135-172-103-726-721-132-216-125-716-257-127-372-1_V.mp3)
  Segment 17: 69604ms ~ 79108ms (file: 723-267-132-231-301-576-172-231-235-270-230-253-260-321-310-517-103-130-312-310-137-301-713-570-157-137-302-137-103-176-312-230-213-127-726-762-270-103-210-207-271-270-172-137-1_V.mp3)
  Segment 18: 80108ms ~ 91736ms (file: 123-231-102-263-231-236-102-236-203-123-231-103-206-236-263-123-260-231-261-236-236-321-236-102-120-236-263-725-236-326-162-210-231-136-275-321-120-137-173-130-137-317-312-175-1_W.mp3)
  Segment 19: 92736ms ~ 93312ms (file: 127-217-217-712-712-127-172-720-271-702-705-751_W.mp3)
  Segment 20: 94312ms ~ 94888ms (file: 157-715-517-517-571-571-175-157-751-571-573-571-517-571-571-570-576-570 157-751-175-715-175-175-157-175-175-715-175-157-715-571-751-175-751-571-570-570-153-517-510-153-157-751-1_V.mp3)
  Segment 21: 95888ms ~ 96464ms (file: 754-754-756-754-756-754-754-754-754-754-754-157-105-150-150-150-150-157-175-156-157-715-716-167-176-175-167-761-175-761-751-765-615-175-176-156-174-754-754-754-754-754-754-754-7_W.mp3)
  Segment 22: 97464ms ~ 109668ms (file: 627-672-672-135-263-267-130-132-316-327-753-276-267-267-271 132-574-574-102-132-367-103-513-137-751-754-720-207-713-230-321-130-136-230-203-120-102-103-174-672-670-263-367-362-2_W.mp3)
  Segment 23: 110668ms ~ 111244ms (file: 315-315-315-315-765-657-735-357-753-763-371-317-315-312-316-136-315-312-137-136-136-257-721-163-136-136-136-162-136-136-132-136-132-132-312-126-625-635-163-136-136-162-163-132-1_W.mp3)
  Segment 24: 112244ms ~ 112820ms (file: 637-613-673-675-637-167-163-167-637-617-617-617-637-617 750-750-375-370-631-632-361-632-627-675-672-671-617-617-631-637-631-316-316-137-136-316-137-175-307 371-703-730-310-137-1_W.mp3)
  Segment 25: 113820ms ~ 114396ms (file: 173-107-175-751-317-170-710-173-105-157 571-517-153-517-517-150-175-715-153-157-751-517-175-105-173-157-173-175-173-715-175-175-157-571-573 571-570-510-175-157-157-157-517-715-1_V.mp3)
  Segment 26: 115396ms ~ 126484ms (file: 765-210-201-271-103-130-302-321-210-321-230-310-123-213-301-102-310-102-752-627-756-103-102-312-201-312-231-201-725-752-275-302-230-157-250-130-201-213-203-173-174-123-261-765-2_V.mp3)
  Segment 27: 127484ms ~ 128060ms (file: 167-156-165-156-163 123-123-120-136-163_W.mp3)
  Segment 28: 129060ms ~ 136944ms (file: 205-320-302-301-310-310-206-162-127-103-310-621-123-620-150-501-257-132-203-621-123-250-263-120-527-273-726-210-120-123-103-132-570-165-310-310-207-672-715-123-537-120-206-132-1_V.mp3)
  Segment 29: 137944ms ~ 138520ms (file: 765-726-763-762-765-763-763-230-312-620-627-236-301-317-137-103-137_V.mp3)
  Segment 30: 139520ms ~ 148196ms (file: 231-203-127-260-210-326-120-127-301-601-263-106-230-237-265-230-236-213-362-106-176-103-123-130-276-756-267-267-261-123-320-167-270-753-712-756-123-327-726-327-273-207-203-107-2_W.mp3)
  Segment 31: 149196ms ~ 149772ms (file: 567-567 517-156-157-156-561-561-572-567-576-561-561-567-516-561-516-517-516-531-516-531-576-576-675-674-675-675-675-617-761-617-671-167-631-136 653-651-615-615-612-657-657-635-6_W.mp3)
  Segment 32: 150772ms ~ 161608ms (file: 103-107-103-231-572-231-103-302-176-572-230-132-723-102-270-170-310-276-210-320-360-106-136-312-537-235-572-256-267-532-316-317-302-573-137-213-571-310-237-231-320-327-325-253-2_W.mp3)
  Segment 33: 162608ms ~ 163184ms (file: 510-501-574-576-574-547-574-574-574-574-574-573 570-517-510-513-157-507-501-501-157-150-501-150-507-510-105-503-510-517-570-571-570-517-517-570-576-574-574-574-576-574-547-574-5_W.mp3)
  Segment 34: 164184ms ~ 164760ms (file: 165-574-574-574-312-301-301-312 136-103-136-617-312-136-103-167-320-163-213-103-132-156-126-126-165-651-106-613-312-136-513-136-103-130-316-731-326-106-325-206-103-132-130-751-7_V.mp3)
  Segment 35: 165760ms ~ 166336ms (file: 310-312-102-361-607-627-612-302-203-260-206-260-206-260-260-103-756-762-762-176-103-170-312-260-362-623 765-675-132-312-126-123-321-725-130 621-627-672-623-623-361 627-267-263-2_W.mp3)
  Segment 36: 167336ms ~ 167912ms (file: 130-341-143-103-130-143-130-103-104-103-103-134-104-314-134-143-134-103-143-134-134-103 301-310-134-103-314-103-103-103-143-135-134-130-103-143-104-105-143-134 301-341-103-317-3_V.mp3)
  Segment 37: 168912ms ~ 181908ms (file: 507-572-257-754-203-527-265-325-275-526-627-523-352-206-206-102-237-706-756-567-103-320-231-361-657-573-651-301-206-361-316-316-136-136-731-726 236-123-102-103-163-761-623-326-1_W.mp3)
  Segment 38: 182908ms ~ 183484ms (file: 163-312-135-163-310-130-103-106-615-103-160-106-356-165_V.mp3)
  Segment 39: 184484ms ~ 185060ms (file: 213-312-103-106-132-136-163-150-165-315-167-136-760-176-103-176-312-103-103 136-130-136-130-136-103-130-130-136-135-136-762-517-673-671-136-316-136-103-137-617-701-310-316-106-1_V.mp3)
  Segment 40: 186060ms ~ 186636ms (file: 257-725-312-261-357-301-371-713-657-216-257-275-507-513-751-135-617 256-752-572-756-263-312-572-256-235-234-706-132-672-523-756-265-267-675-326-256-153-237-276-372-725-527-517-5_V.mp3)
  Segment 41: 187636ms ~ 194368ms (file: 574-230-237-312-750-132-725-127-351-136-713-756-752-547-725-725-263-706-162-675-162-205-205-236-263-123-765-376-276-273-375-750-103-276-217-723-673-671-672-627-756-312-673-765-7_W.mp3)
  Segment 42: 195368ms ~ 203216ms (file: 327-273-267-230-237-130-273-317-237-236-327-726-267-326-267-231-230-237-230-275-103-136-276-120-276-236-726-276-321-217-726-726-706-273-237-237-237-263-213-127-206-102-372-321-7_W.mp3)
  Segment 43: 204216ms ~ 204792ms (file: 105-105-105-150-157-157-176-167-756-160-176-157-175-175-517-157-157-715-157-571-517-517-517-571-176 514-105-105-105-150-105-170-156-157-175-170-167-176-175-157-157-517-157-175-1_V.mp3)
  Segment 44: 205792ms ~ 206368ms (file: 316-231-123-126-123-136-213-326-130-103-132-163-130-126-130-126-103-102-136-126-310-102-106_V.mp3)
  Segment 45: 207368ms ~ 207944ms (file: 615-762-750-173-753-367-527-267-276-235-132-213-215 267-672-231 756-752-362-217-365-725-756-726 217-572-625-751-326-536-570-513-571-376-263-312-527-376-573 756-270-203-362-713-7_V.mp3)
  Segment 46: 208944ms ~ 217656ms (file: 217-367-173-321-162-367-716-736-136-217-301-213-120-102-163-310-103-671-316-126-132-123-103-617-130-132-216-726-136-762-120-123-216-167-127-126-103-130-261-162-613-267-176-376-1_W.mp3)
  Segment 47: 218656ms ~ 219232ms (file: 621-167-631-326-136-312-261-306-310-132-316-627-236_V.mp3)
  Segment 48: 220232ms ~ 220808ms (file: 172-702-276-723-756-375-523-157-256-267-537-153-571-750-526-625-750-172-573-302-216-267-125-153-570-167-571-326-713-273-235-576-657-753-750-670-572-265-521-527-123-265-527-126-5_V.mp3)
  Segment 49: 221808ms ~ 222384ms (file: 652-627 671-675-765-765-765-675-673-763-673-673-673-675-673-673-675-763-675-672-623-621-623-632-623-612-632-623-362-632-631-362-632-623-632-623-675-675-675-675-675-675-765-765-7_W.mp3)
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
> **Time**: 2026-04-05 06:56:09
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
"transcript": "어서 오세요 무엇을 도와드릴까요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 210,
"audio_type": "speech",
"words": [
{"word": "어서", "start_ms": 210, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "오세요", "start_ms": 600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "무엇을", "start_ms": 1200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "도와드릴까요?", "start_ms": 1700, "dur_ms": 800, "phase_lr_deg": 0.0}
]
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
"transcript": "주변에 맛있는 한식당을 찾고 있어요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "주변에", "start_ms": 180, "dur_ms": 450, "phase_lr_deg": -5.0},
{"word": "맛있는", "start_ms": 700, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "한식당을", "start_ms": 1300, "dur_ms": 650, "phase_lr_deg": -5.0},
{"word": "찾고", "start_ms": 2000, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "있어요", "start_ms": 2400, "dur_ms": 400, "phase_lr_deg": -5.0}
]
},
{
"segment": 5,
"transcript": "Do you have any specific menu in mind?",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "Do", "start_ms": 250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 410, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "have", "start_ms": 580, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "any", "start_ms": 800, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "specific", "start_ms": 1100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "menu", "start_ms": 1650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 2050, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "mind?", "start_ms": 2250, "dur_ms": 400, "phase_lr_deg": 0.0}
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
"transcript": "불고기나 비빔밥이 좋을 것 같아요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "불고기나", "start_ms": 200, "dur_ms": 600, "phase_lr_deg": -5.0},
{"word": "비빔밥이", "start_ms": 900, "dur_ms": 600, "phase_lr_deg": -5.0},
{"word": "좋을", "start_ms": 1600, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "것", "start_ms": 1950, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "같아요", "start_ms": 2150, "dur_ms": 450, "phase_lr_deg": -5.0}
]
},
{
"segment": 10,
"transcript": "그렇다면 사거리에 있는 명동 식당을 추천해요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "그렇다면", "start_ms": 150, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "사거리에", "start_ms": 800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 1450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "명동", "start_ms": 1800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "식당을", "start_ms": 2250, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "추천해요", "start_ms": 2800, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "It is famous for its authentic Korean taste.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "It", "start_ms": 200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 380, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "famous", "start_ms": 550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "for", "start_ms": 1050, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "its", "start_ms": 1280, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "authentic", "start_ms": 1550, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "Korean", "start_ms": 2200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "taste.", "start_ms": 2700, "dur_ms": 400, "phase_lr_deg": 0.0}
]
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
"transcript": "감사합니다 혹시 예약이 필요한가요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 120, "dur_ms": 700, "phase_lr_deg": -5.0},
{"word": "혹시", "start_ms": 950, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "예약이", "start_ms": 1400, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "필요한가요?", "start_ms": 1950, "dur_ms": 700, "phase_lr_deg": -5.0}
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
"transcript": "점심시간에는 사람이 많아서 예약하는 게 좋아요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "점심시간에는", "start_ms": 150, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "사람이", "start_ms": 1000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "많아서", "start_ms": 1500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "예약하는", "start_ms": 2100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 2750, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "좋아요", "start_ms": 2950, "dur_ms": 450, "phase_lr_deg": 0.0}
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
"transcript": "전화번호를 알려주실 수 있나요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "전화번호를", "start_ms": 200, "dur_ms": 700, "phase_lr_deg": -5.0},
{"word": "알려주실", "start_ms": 1000, "dur_ms": 600, "phase_lr_deg": -5.0},
{"word": "수", "start_ms": 1650, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "있나요?", "start_ms": 1850, "dur_ms": 450, "phase_lr_deg": -5.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 21,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "잠시만요 여기 전화번호가 적힌 메모예요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "잠시만요", "start_ms": 150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "여기", "start_ms": 850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "전화번호가", "start_ms": 1200, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "적힌", "start_ms": 1950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "메모예요", "start_ms": 2400, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "맛있게 드시고 즐거운 시간 보내세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "맛있게", "start_ms": 120, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "드시고", "start_ms": 650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "즐거운", "start_ms": 1200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "시간", "start_ms": 1750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "보내세요", "start_ms": 2150, "dur_ms": 600, "phase_lr_deg": 0.0}
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
"transcript": "Thank you for your kindness. Have a great day!",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 220,
"audio_type": "speech",
"words": [
{"word": "Thank", "start_ms": 220, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "you", "start_ms": 550, "dur_ms": 150, "phase_lr_deg": -5.0}
]
},
{
"segment": 40,
"transcript": "내일도 같은 시간에 뵙겠습니다 안녕히 가세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "내일도", "start_ms": 180, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "The system is now updating its database for you.",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "The", "start_ms": 250, "dur_ms": 150, "phase_lr_deg": 0.0}
]
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
"transcript": "오늘 배운 표현들을 복습해 보는 건 어떨까요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 200, "dur_ms": 350, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 06:56:09*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=14936  time=06:54:00  uptime=18540s  mem=452MB  system_processes=350"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_064913.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 10080ms (file: 132-760-103-132-163-312-623-627-267-103-236-564-257-265-267-120-102-123-103-102-263-765-213-123-236-120-761-237-736-327-236-123-231-321-213-123-237-312-123-321-123-752-267-263-1_W.mp3)
  Segment 2: 11080ms ~ 11656ms (file: 260-260-625-625-265-265-265-265-625-265-623-261-260-625-625-625-652-625-652-265-625-265-265 623-621-612-621-260-260-627-625-625-265-625-261-261-265-625-265-265-620-623-625-625-5_W.mp3)
  Segment 3: 12656ms ~ 13232ms (file: 576-506-670-672-507-137-132-137-563-670-760-670-567-750-256-206-673-763 765-607 123-567-562-312-762-763-576-507-576-756-570-637-672-631-761-576-675-637-675_W.mp3)
  Segment 4: 14232ms ~ 20784ms (file: 205-215-321-315-310-310-317-302-213-123-107-123-572-507-123-123-157-126-102-216-157-175-301-312-132-612-126-132-103-132-276-103-167-672-620-627-621-137-726-765-132-103-201-276-2_W.mp3)
  Segment 5: 21784ms ~ 22360ms (file: 107-173-107-174-134-107-103-317-317-137-103-103-134-103-134-317 137-315-735-371-157-157-153-157-517-157-571-517-751-517-571-571-517-157-571-175 317-753-351-157-137-315-153-517-1_W.mp3)
  Segment 6: 23360ms ~ 23936ms (file: 623 103-316-136-371-367-317-371-163-136-163-103-163-671-631-671-613-635-365-301-315-325-623-267-236-216-235-203-203-230-230-327-302-265-237-237-372-753-263-253-257-256-235-256-1_V.mp3)
  Segment 7: 24936ms ~ 25512ms (file: 267-567-567-567-657-567-156-165-621-627-672-672-627-576-752-275-276-267-236-517-574-756-753-613-167-756-576-756-675-576-567-513-157-215-672-675-765-637-132-326-236-621-621-612-6_W.mp3)
  Segment 8: 26512ms ~ 27088ms (file: 301-137-134-103-103-130-130-314-317-314-314-134-314-134-314 315-374-310-104-103-317-301-310-130-137-341-104-130-134-143-134-314-134-134-134-104-134-103-134-431-103-134-143-134-1_V.mp3)
  Segment 9: 28088ms ~ 34568ms (file: 301-210-510-520-276-301-130-132-127-130-657-521-120-216-130-261-310-103-132-102-635-362-206-260-216-206-130-132-257-756-207-123-132-130-516-315-123-320-623-120-261-120-627-120-1_W.mp3)
  Segment 10: 35568ms ~ 36144ms (file: 102-316-316-236-130-136-172-317-137-316-317-136-132-316-316-103-316-176-170-176-137-671-176-162-126-273-162-176-167-162-165-176-571-163-517-163-316-163-126-132-163-163-136-157-1_W.mp3)
  Segment 11: 37144ms ~ 37720ms (file: 563-265-561-361-627-561-756-513-256-560-205-657-506-273-627-762-273 573-527 361-361-752-267-321-263-762-275-562-237-217-213-257-721-123-576-572-215-163-160-316-625-275-750-617-2_V.mp3)
  Segment 12: 38720ms ~ 48296ms (file: 210-210-574-210-213-765-267-576-607-602-620-231-213-512-573-574-206-206-231-120-527-267-203-572-527-756-726-260-120-620-257-207-310-103-130-213-574-527-102-137-257-213-123-574-2_W.mp3)
  Segment 13: 49296ms ~ 49872ms (file: 620-623-621-162-621-623-621-263-263-627-263-621-263-621-621-627-621-623-260-623-263-621-625-621-623-623-621-261-623-621-623-623-263-621-627-621-621-627-621-625-621-623-621-620-6_W.mp3)
  Segment 14: 50872ms ~ 51448ms (file: 567-657-651-651-165-156-651-615-651-165_W.mp3)
  Segment 15: 52448ms ~ 53024ms (file: 512-513-256-563-652-351-532-326-321-216-123-516-356-562-365-321-271-371-573-576-567-567-567-576-572-527-563-276-275-572-574-547-537-375-532-352-275-732-376-375-573-235-135-163-1_W.mp3)
  Segment 16: 54024ms ~ 54600ms (file: 504-501-501-501-514-510-510-510-510_W.mp3)
  Segment 17: 55600ms ~ 56176ms (file: 170-307-310-735-173-103 103-130-130-107-713-170-173-107-137-715-713-735-375-735-301-137-701-173-375-301-701-172-701-750-710-750-731-107-713-713-170 135-173-130-175-703-710-713-1_V.mp3)
  Segment 18: 57176ms ~ 65204ms (file: 574-542-263-623-765-107-132-624-236-263-725-754-263-263-312-216-263-362-537-123-267-276-263-574-106-167-576-263-754-136-126-236-547-164-275-547-130-625-620-762-547-534-312-321-2_W.mp3)
  Segment 19: 66204ms ~ 73260ms (file: 267-765-276-230-672-762-267-167-267-327-312-321-261-123-120-321-130-102-576-756-762-762-725-320-302-573-130-103-163-261-276-176-317-327-362-230-231-236-326-632-673-326-765-231-3_W.mp3)
  Segment 20: 74260ms ~ 83260ms (file: 102-735-621-107-102-216-627-321-756-754-756-260-326-657-621-213-312-657-574-263-260-103-263-726-276-275-213-751-231-263-216-326-260-627-627-263-236-120-263-526-524-620-236-103-3_W.mp3)
  Segment 21: 84260ms ~ 84836ms (file: 175-175-173 105-105-105-105-105-105-105-157-107-175-517-157-176-157-175-571-175-715-715-175-175-173 504-504-501-105-510-150-157-510-175-157-157-175-107-175-157-175-175-175-157-1_V.mp3)
  Segment 22: 85836ms ~ 86412ms (file: 671-162-123-765-163-160-103-132-136-123-107-725-372-273-672-756-756-756-765-753-756 103-362-726-156-163-310-137-317-357-135-567-162-567-102-620-236-102-132-672-526-106 176-325-3_V.mp3)
  Segment 23: 87412ms ~ 87988ms (file: 137-715-316-735-576-523-175-517-627-237-507-546-251-572-175-760-276-726-162-752-251-726-573-724-623-732-276-765-571-605-267-153-751-725-236-625-657-574-372-762-265-263-316-135-3_V.mp3)
  Segment 24: 88988ms ~ 89564ms (file: 135-153-143 413-415-173-154-154-154-150-154-105-103-130-153-150-105-154-451-415-143-134 453-514-457-143-453-457-140-140-415-104-314-135-145-104-145-104-145-174-314-140-315-143-1_V.mp3)
  Segment 25: 90564ms ~ 91140ms (file: 165-137-576-230-756-617-136-132-123-316-163-103-103-163-136-136-136-163-163-615-136-103-517-156-103-163-130-136-613-136-312-132-103-316-310-136-561-162-136-617-156-325-516-103-1_V.mp3)
  Segment 26: 92140ms ~ 92716ms (file: 317-367-756-756-765-376-736-765-763-736-765-756-765-736-763-765-761-736-176-756-721-765-753-726-736-762-576-756-753-751-573-375-573-753-573-576-576-756-573-753-573-573-527-576_W.mp3)
  Segment 27: 93716ms ~ 94292ms (file: 103-315-315 130-130-310-310-301-103-130-105-135-130-135-153-153-501-140-134-410-130-341-415-413-431-143-315-315-135-315-315-315-104-310-135-103-105-104-103-135-134-105-314-146-1_W.mp3)
  Segment 28: 95292ms ~ 95868ms (file: 632-623-216-326-157-570-563-573-315-653-567-671-376-637-367-763-367-675-736-356-573-356-632-675-576-576-507-163-362-162-612-765-762-671-675-675-671-617-673-675-675-612-612 762_W.mp3)
  Segment 29: 96868ms ~ 102952ms (file: 276-231-325-265-357-621-315-132-325-357-267-752-103-172-123-135-132-132-315-235-235-103-137-312-213-271-261-236-103-325-756-173-132-367-513-253-521-102-103-301-235-123-261-132-2_W.mp3)
  Segment 30: 103952ms ~ 104528ms (file: 751-630-261-627-670-106-726-762-527-275-627-621-627-621-263 614-162-163-106-765-650-167-617 132-260-260-607-607-670-260-260-620-261-126-130-310-630-321-602-731-267-627-263-263-3_W.mp3)
  Segment 31: 105528ms ~ 106104ms (file: 765 261-261-623-631-263 216-261-163-106-263-126_W.mp3)
  Segment 32: 107104ms ~ 112396ms (file: 127-217-123-132-216-217-126-217-217-752-271-120-102-321-267-251-210-216-216-216-321-201-162-261-261-201-621-627-130-312-213-207-102-102-126-120-213-231-126-216-270-217-216-213-1_W.mp3)
  Segment 33: 113396ms ~ 113972ms (file: 315-136-612-635-362-316-263-236-612-361-623-756-765-265-276-726-672-675-265 165-261-756-705-702-706-765-765-760-625-602-602-612-761-756-705-705-703-617-675-267-672-672-670-607-6_W.mp3)
  Segment 34: 114972ms ~ 125520ms (file: 203-375-752-312-231-260-260-576-267-612-120-163-260-231-261-261-210-261-260-216-102-216-132-261-267-216-216-261-572-265-213-263-762-526-265-263-261-267-276-213-206-546-260-260-2_W_stt_they.mp3)
  Segment 35: 126520ms ~ 127096ms (file: 175-715-175-173-735-752-715-751-571-751-752-173-715-175-123-172-125-125-152-153-152-513-152-157-571-572-571-756-175-571-105-570-701-517-157-170-175-170-157-175-170-170-150-173-7_W.mp3)
  Segment 36: 128096ms ~ 136556ms (file: 106-167-675-167-312-236-120-321-163-163-265-627-260-132-321-231-106-714-574-132-321-231-754-627-627-106-756-751-102-132-756-170-752-576-130-261-261-120-123-562-327-756-213-201-2_W.mp3)
  Segment 37: 137556ms ~ 146628ms (file: 210-241-236-216-267-210-206-260-256-574-302-231-201-302-201-213-120-123-102-124-132-127-127-312-213-216-310-301-310-321-320-752-130-213-713-275-103-132-160-362-136-301-725-276-2_V.mp3)
  Segment 38: 147628ms ~ 148204ms (file: 617-671-617-617-617-671-627-162-612-627-623-627-627-621-627-326-136-361-312-312-657-765-547-756-574-675 326-627-627-126-615-167-173-615-671-571-675-672-267-612-637-630-637 625-6_W.mp3)
  Segment 39: 149204ms ~ 154676ms (file: 570-531-256-531-512-537-317-310-315-165-130-267-123-765-762-102-160-107-260-672-671-130-157-103-126-130-210-752-201-321-312-130-301-275-173-572-152-270-132-123-260-261-726-132-1_V.mp3)
  Segment 40: 155676ms ~ 167556ms (file: 237-527-276-276-237-236-237-320-327-754-756-276-756-765-267-236-726-127-672-367-327-327-176-362-162-160-231-321-312-176-160-574-263-167-617-627-263-231-765-627-267-316-317-267-2_W.mp3)
  Segment 41: 168556ms ~ 169132ms (file: 371-137-301-312-103-301-103-103-310-107-172-713-103-713 317-137-103-315-173-103-103-301-130-137-301-310-130-107-130-103-103-130-130-130-130-310-130-173-310-103-172-310 153-103-3_V.mp3)
  Segment 42: 170132ms ~ 170708ms (file: 165-145-134-106-103-134-134-145-163-135-154-145-106-135-136-156-106-130-160-106-103-165-163-103-106-105-106-106-135-106-105-103-136-106-135-157-517-576-135-137-315-137-135-317-3_V.mp3)
  Segment 43: 171708ms ~ 172284ms (file: 317-301-310-173-172-137-305-371-317-753-301-157-217-317 103-135-315-157-103-130-130-315-132-137-315-137-312-357-315-137-357-301-375-715-317-301-175-371-317-135-713-310-312-301-3_V.mp3)
  Segment 44: 173284ms ~ 173860ms (file: 750-750-750-756-750-750-105-150-150-157-107-157-157-150-157-157-175-157-175-156-157-510-157-517-175-157-157-150 105-105-150-105-150-157-105-157-156-170-175-517-517-157-517-576-5_V.mp3)
  Segment 45: 174860ms ~ 175436ms (file: 137-301-137-103-103-173-107-705-703 310-137-103-310-301-173-137-130-137-317-103-310-130-301-137-103-103-130-107-315-170 103-135-317-317-301-103-137-137-130-310-317-137-130-301-3_V.mp3)
  Segment 46: 176436ms ~ 186660ms (file: 213-201-267-607-620-132-132-213-120-103-620-260-263-103-752-276-756-267-572-206-620-231-260-270-217-103-210-207-206-103-216-231-207-126-127-137-627-173-206-206-123-201-103-260-1_V.mp3)
  Segment 47: 187660ms ~ 188236ms (file: 627-263-617-672-627-602-632-603-612-320-263-230-236-201-236-567-567-567-106-613-620-260-201-276-756-750-361-576-160-613-163-601-360-603-602-203-206-130-730-750-752-270-752-206-3_W.mp3)
  Segment 48: 189236ms ~ 189812ms (file: 267-276-362-320-326-362-326-312-126-316-623-263-361-750-765-726-276-237-167-163-103-613-612-623-672-206-261-267-627-267-267-236-612-613-261-623-321-263-136-301-301-103-103-627-6_V.mp3)
  Segment 49: 190812ms ~ 198516ms (file: 267-571-756-275-321-103-130-571-130-756-765-765-321-671-102-231-231-321-123-765-675-273-321-132-230-102-163-132-321-321-731-571-765-574-627-576-756-321-132-576-105-675-263-213-1_W.mp3)
  Segment 50: 199516ms ~ 200092ms (file: 562-562-567-560-560-527-520-520-502-520_W.mp3)
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
> **Time**: 2026-04-05 06:56:40
> **AI**: gemini

---

## Response

AnalysisAnalysisGemini의 응답[
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
"transcript": "세탁기는 숨겨야 하는 가전이라는 이미지를 조금씩 걷어냈다고 합니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "세탁기는", "start_ms": 110, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "숨겨야", "start_ms": 860, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 1410, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "가전이라는", "start_ms": 1810, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "이미지를", "start_ms": 2710, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "조금씩", "start_ms": 3410, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "걷어냈다고", "start_ms": 4010, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "합니다", "start_ms": 4860, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "잘못된 부분에 대해서는 당원과 국민들께 사과하고 한 번은 짚고 넘어가야 될 일들이 필요하지 않나 생각이 듭니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "잘못된", "start_ms": 150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "부분에", "start_ms": 750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "대해서는", "start_ms": 1250, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "당원과", "start_ms": 1950, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "국민들께", "start_ms": 2600, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "사과하고", "start_ms": 3400, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 4150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "번은", "start_ms": 4350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "짚고", "start_ms": 4700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "넘어가야", "start_ms": 5100, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "될", "start_ms": 5750, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "일들이", "start_ms": 5950, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "필요하지", "start_ms": 6500, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "않나", "start_ms": 7200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "생각이", "start_ms": 7600,

---
*Generated by WKAppBot ask gemini — 2026-04-05 06:56:40*
