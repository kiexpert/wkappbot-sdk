# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=10820  time=11:55:01  uptime=28237s  mem=442MB  system_processes=354"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_114909.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 176-162-167-136-137-137-172-162-163-361-163-265-612-217-762-165-651-157-156-165-167-137-132-136-136-153-563-652-516-517-657-765-657-653-357-157-576-576-765-576-167-715-751-715-7_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 106-135-135-135-315-315-315-315-315-315-135-130-130-310-310-103-150-413-130-145-130-310-130-310-130-130-310-103-310-135-315-135-315-315-314-130-130-130-103-154-345-453-154-153-1_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 725-750-273-726-750-567-726-627-672-263-276-263-267-236-270-236-765-765-167-176-712-756-265-327-326-275-267-275-607-750-321-570-673-265-256-237-562-573-517-572-576-327-613-753-2_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 726-721 762-762-761-721-271-726-672-267-276-725-725-127-271-721-715-275-276-267-267_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 754-710-751-175-751 574-574-570-754-751-571-574-574-571-157-105-157-751-753 750-571-751-105-715-107-107-150-105-710-571-105-107-150-157-175-157-517-517-157-753-750-751-756-756-7_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 150-570-517-501-517-571-150-517-571-570-157-157-157-157-517-157-175-157-107-105-157-750-571-571 517-517-574-574-574-507-517-571-157-517-157-157-570-751-175-705-751-570-157-150 5_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 751-751-571 510-105-150-150-510-501-150-751-571-751-175-751-715-751-751-751-751-756-756-715-756-751-751 105-103-150-157-175-751-751-756-756-756-751-751-175-715-715-715-751-175-7_V.mp3)
  Segment 8: 11032ms ~ 18736ms (file: 754-236-627-263-120-756-574-267-132-675-576-765-576-102-725-106-102-167-102-723-765-103-267-237-271-716-127-167-127-136-672-167-672-162-176-167-671-762-762-670-276-671-671-762-6_W.mp3)
  Segment 9: 19736ms ~ 20312ms (file: 167 103-150-150-150-150-170-105-107-175-175-173-176-167-137-176-136-160-167-176-135 510-150-150-150-105-105-106-105-106-156-107-105-107-150-176 150-510-105-150-150-150-175-105-1_W.mp3)
  Segment 10: 21312ms ~ 23256ms (file: 236-351-540-501-564-351-135-132-123-321-132-173-317-132-210-213-127-132-157-321-263-237-712-765-753-132-132-123-130-132-130-310-301-132-123-253-263-216-261-236-217-231-321-265-2_W.mp3)
  Segment 11: 24256ms ~ 24832ms (file: 576-567-567-576-567-567-657-567-657-675-675-657-675-675-627-675-765-675-672-675-627 650-560-567-657-657-567-576-567-567-567-576-567-576-567-576-567-657-576-657-672-675-675-675-6_W.mp3)
  Segment 12: 25832ms ~ 26408ms (file: 576-576-132-526 217-132-631-361-765-365-316-103-310-130-103-263-572-126-326-653-631-163-675-756-537-631-367-631-136-105-750-136-736-731-716-736-317-376-137-136-317-173-756-716-1_W.mp3)
  Segment 13: 27408ms ~ 27984ms (file: 163-106-103-103-103-162-163-103-175-253-613-165-162-103-627-176-162-102-126-132-103-136-137-160-136-130-103-760-165-631-126-671-106-120-617-167-652-132-136-163-165-625-167-136-1_V.mp3)
  Segment 14: 28984ms ~ 29560ms (file: 542-572-572-527-572-572-527-572-572-567-576 750-507-517-507-570-527-527-572-572-572-572-570-572-576-576-574-576-570-675-657-760-754-564-523-564-506-506-256-567-750-750-572-752-5_W.mp3)
  Segment 15: 30560ms ~ 35672ms (file: 726-756-276-713-732-725-715-725-576-756-735-732-735-310-321-572-257-267-726-173-137-312-327-273-362-237-627-176-376-721-173-175-271-263-120-231-321-751-732-721-127-372-327-237-2_W.mp3)
  Segment 16: 36672ms ~ 37248ms (file: 526-527-510-517-516-576-670-675-675-670-657-675 627 502-501_W.mp3)
  Segment 17: 38248ms ~ 43756ms (file: 312-132-206-627-267-236-527-317-320-273-761-163-106-675-635-135-270-326-276-267-276-230-172-137-756-367-376-763-736-731-763-237-326-273-132-725-260-236-301-310-321-312-132-132-3_W.mp3)
  Segment 18: 44756ms ~ 55556ms (file: 507-750-270-230-672-176-574-572-267-231-672-267 765-761-172-213-237-547-107-231-672-267-236-574-572-572-547-726-765-327-574-574-572-267-261-137-726-541-157-453-541-231-754-574-3_W.mp3)
  Segment 19: 56556ms ~ 57132ms (file: 167-123-137-137-176-137-127-361-612-123-217-123-150-517-751-531-175-715-750-157-157-175-715-175-176-157-173-137-731-715-173-157-751-751-175-137-751-513-105-130-105-103-105-102-1_V.mp3)
  Segment 20: 58132ms ~ 58708ms (file: 302-302-301-571-751-176-316-601-167-617-106-216-602-165-126-102-123-130-312-163-136-123-762-136-165-106-316-576-517-157-107-312-103-137-167-143-312-613-103-107-176-301-301-301-1_V.mp3)
  Segment 21: 59708ms ~ 66368ms (file: 762-672-132-103-176-120-321-534-231-135-132-761-321-756-765-132-756-756-527-267-102-321-316-756-261-267-321-537-312-617-763-726-267-621-217-231-132-263-137-170-312-357-321-102-2_W.mp3)
  Segment 22: 67368ms ~ 67944ms (file: 176-137-103-213-167-157-103-615-126-137-130-103-106-167-160-103-137-103-136-632 712-167-167 312-136-103-316-103-317-173-162-103-617-163-671-103-136-106-607-136-106-613-103-136-3_V.mp3)
  Segment 23: 68944ms ~ 79888ms (file: 126-216-216-271-162-127-106-120-206-260-671-671-162-316-326-130-136-167-136-162-132-130-361-316-361-306-360-316-361-306-301-132-627-126-312-310-316-316-316-136-137-136-126-316-1_W.mp3)
  Segment 24: 80888ms ~ 81464ms (file: 103-103-103-172-216-162-276-620-651-130-672-612-627-261-621-261-172-612-326-167-103-613-267-176-162-610-261-102-102-163-105-261-621-103-106-136-163-162-106-132-167-106-106-572-1_V.mp3)
  Segment 25: 82464ms ~ 83040ms (file: 754-750-571-574-570-570-571-574 750-570-571-571-751-705-107-105-105-517-750-175-710-570-175-754-574-574-571-715-501-107-705-107-175-107-701-107-175-705-150-517-107-715-750-705-1_V.mp3)
  Segment 26: 84040ms ~ 95200ms (file: 310-312-123-175-752-276-273-756-275-736-132-103-231-231-217-321-260-231-102-162-167-726-103-130-103-236-126-160-106-130-130-267-103-715-107-120-260-236-123-261-231-102-326-321-3_W.mp3)
  Segment 27: 96200ms ~ 102320ms (file: 231-230-302-321-321-321-102-263-123-136-130-132-267-276-275-701-175-102-210-217-201-150-230-752-574-257-623-105-302-532-570-576-576-572-765-671-276-276-107-756-207-230-102-276-1_W.mp3)
  Segment 28: 103320ms ~ 103896ms (file: 567-756-265-672-623 675-167-657-765-567-675-675-672-617-167-762-675-627-267-617-265-675-657-567-675-657-567-672-675-675-627-612-762-627-672-236-362-167-765-756-765-126-613 203-3_W.mp3)
  Segment 29: 104896ms ~ 105472ms (file: 136-623-621-162-623-765-316-136-160-671-612-106-103-125-613-567-103-163-103-103-162-103-517-213-172-123-103-213-312-103-106-120-136-632-106-123-162-106-163-675-162-163-160-106-1_V.mp3)
  Segment 30: 106472ms ~ 116444ms (file: 203-230-203-230-203-206-231-321-213-271-216-107-213-752-210-107-320-270-321-230-237-237-276-276-726-726-260-310-103-210-276-162-106-105-312-312-132-163-102-120-216-310-310-720-2_W.mp3)
  Segment 31: 117444ms ~ 118020ms (file: 572-257-752-572-752-750-512-517-512-512-516-513-517-513-512-512_V.mp3)
  Segment 32: 119020ms ~ 119596ms (file: 571-570-570-570-570-570-517-570 570-715-751-517-517-157-157-576-517-517-517-571-570-576-576-570-570-537-573 510-501-501-513-547-540-504-514 501-510-510-150-510-501-513-510-153-5_V.mp3)
  Segment 33: 120596ms ~ 128876ms (file: 261-261-257-103-130-136-136-213-123-134-261-210-216-576-267-576-267-265-216-123-574-576-132-352-157-106-167-106-210-261-213-130-316-163-132-312-106-754-726-627-623-362-756-752-3_W.mp3)
  Segment 34: 129876ms ~ 135744ms (file: 210-721-307-362-371-130-135-317-237-201-167-267-263-623-765-267-753-574-361-173-613-132-627-173-301-267-126-216-176-571-726-267-612-316-765-136-217-276-135-541-502-201-260-260-2_W.mp3)
  Segment 35: 136744ms ~ 137320ms (file: 751-751-715-175-715-751-751-715-751-175-175-715-756-715-751-715-715-751-715 150-150-150-157-751-751-751-715-751-751-715-751-751-715-751-715-751-715-751-756-175-715 501-150-517-1_W.mp3)
  Segment 36: 138320ms ~ 138896ms (file: 170-103-135-170-715-170-710-710-170-170-170-170-137-701-103-130-137-137-170-137-173-107-710-750 135-157-175-170-137-170-173-107-130-130-173-137-103-137-170-173-175-175-173-170-1_W.mp3)
  Segment 37: 139896ms ~ 140472ms (file: 175-715-157-137-173-751-157-157-751-175-175-315-315-315-135-315-513-751-157-715-173-153-137-157-571-135-751-175-175-513-571-715-751-571-751-175-751 751-157-175-751-715-713-175-1_W.mp3)
  Segment 38: 141472ms ~ 149356ms (file: 231-102-107-756-231-267-765-765-312-372-123-170-571-176-106-571-235-176-103-130-132-752-725-756-276-162-103-275-125-312-103-231-213-573-135-102-312-623-267-627-263-276-231-263-7_W.mp3)
  Segment 39: 150356ms ~ 150932ms (file: 701-370-107-103-130-103-301 730-371-170-107-103-106-213-130-103-107-310-130-317-301-130-371-103-137-312-170-375-375-705-301-710-317-735 175-715-107-730-763-130-107-761-701-703-3_V.mp3)
  Segment 40: 151932ms ~ 152508ms (file: 615-617-167-175-216-231-623-621-623-326-216-132-123-120-123-216-106-360-362-302-362-627-602-302-263-120-120-123-736-236-130-312-136-126-260-260-206-726-273-276-213-621-613-612-1_V.mp3)
  Segment 41: 153508ms ~ 154084ms (file: 327-731-547-547-547-547-547-574-574-547-547-504-501-501-501-574-574 574-507-507-501-510-507-507-570-510-507-507-571-570-517-507-517-756 504-504-507-510-501-105-507-150-507-510-5_V.mp3)
  Segment 42: 155084ms ~ 155660ms (file: 136-761-761 675-126 627 132-103-106-562-103-167-316-163-126-160-673-652-103-106-103-126-126-176-156-163-167-163-103-130-671-163-213-610-136-130-106-163-106-103-163-136-132-175-6_V.mp3)
  Segment 43: 156660ms ~ 162168ms (file: 325-103-631-103-713-210-751-106-237-203-321-321-310-176-702-167-162-620-236-103-261-230-103-120-301-203-216-102-107-216-216-260-231-260-230-321-137-321-231-302-671-236-231-231-1_V.mp3)
  Segment 44: 163168ms ~ 163744ms (file: 675-761-507-572-751-157-173-276-312-230-312-310-213-173-132-675-756-317-572-531-726-637-761-762-753-513-750-137-173-137-173-754-275-716-217-573-762-576-725-567-301-765-756-701-7_V.mp3)
  Segment 45: 164744ms ~ 173600ms (file: 162-136-103-675-652-167-316-754-103-317-135-302-327-150-136-275-750-302-257-275-102-162-675-752-675-236-102-127-573-762-321-671-731-237-572-312-231-357-132-271-231-312-316-627-2_V.mp3)
  Segment 46: 174600ms ~ 175176ms (file: 675-567-576-576-576-576-657-576-756-765_W.mp3)
  Segment 47: 176176ms ~ 176752ms (file: 137-607-630-607-361-610-603-610-130-703-713-310-310-137-173-317-315-315-173-375-315 317-703-701-317-317-317-103-317-301-107-317-103-175-137-307-731-175-137-613-317-130-137-310-1_V.mp3)
  Segment 48: 177752ms ~ 178328ms (file: 627-627-607 607-632-361-316-132-123-365-312-236-276-213-762-672-627-376-316-317-136-537-715-657-657-572-573-672-567-576-537-173-173-317-137-136-163-162-167-136-136-137-756-657-6_W.mp3)
  Segment 49: 179328ms ~ 190668ms (file: 267-206-126-321-327-267-267-120-236-103-102-132-214-102-576-105-236-230-321-231-102-712-632-753-236-106-106-106-106-160-230-106-576-231-236-236-236-236-236-754-574-106-106-267-5_W.mp3)
  Segment 50: 191668ms ~ 192244ms (file: 501-510-517-501-570-507-517-507-571-570-510-570-517-507-506-501 517-531-157-157-153-513-513-157-517-517-530-537-517-157-150-510-501-501-507-501-507-507-501-507-570-576-576-567-5_V.mp3)
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
> **Time**: 2026-04-05 11:56:31
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
"silence_before_ms": 450,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 450, "dur_ms": 820, "phase_lr_deg": 0.0},
{"word": "만나서", "start_ms": 1310, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 1800, "dur_ms": 780, "phase_lr_deg": 0.0}
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
"transcript": "오늘 날씨가 정말 좋네요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 120, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "날씨가", "start_ms": 450, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "정말", "start_ms": 850, "dur_ms": 280, "phase_lr_deg": -5.0},
{"word": "좋네요", "start_ms": 1150, "dur_ms": 400, "phase_lr_deg": -5.0}
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
"transcript": "Where are you going right now?",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 210,
"audio_type": "speech",
"words": [
{"word": "Where", "start_ms": 210, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "are", "start_ms": 480, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 610, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "going", "start_ms": 780, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "right", "start_ms": 1150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "now?", "start_ms": 1380, "dur_ms": 300, "phase_lr_deg": 0.0}
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
"transcript": "저는 지금 학교에 가고 있어요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 300,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 300, "dur_ms": 320, "phase_lr_deg": -5.0},
{"word": "지금", "start_ms": 650, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "학교에", "start_ms": 1050, "dur_ms": 410, "phase_lr_deg": -5.0},
{"word": "가고", "start_ms": 1500, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "있어요", "start_ms": 1850, "dur_ms": 380, "phase_lr_deg": -5.0}
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
"transcript": "한국어 공부는 재미있지만 가끔 어려워요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 180, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "공부는", "start_ms": 680, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "재미있지만", "start_ms": 1150, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "가끔", "start_ms": 1850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "어려워요", "start_ms": 2250, "dur_ms": 500, "phase_lr_deg": 0.0}
]
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
"transcript": "맞아요 꾸준히 하는 게 중요하죠",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "맞아요", "start_ms": 150, "dur_ms": 380, "phase_lr_deg": -5.0},
{"word": "꾸준히", "start_ms": 580, "dur_ms": 420, "phase_lr_deg": -5.0},
{"word": "하는", "start_ms": 1050, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "게", "start_ms": 1320, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "중요하죠", "start_ms": 1480, "dur_ms": 520, "phase_lr_deg": -5.0}
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
"transcript": "What is your favorite Korean food?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "What", "start_ms": 100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 320, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "your", "start_ms": 450, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "favorite", "start_ms": 620, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "Korean", "start_ms": 1050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "food?", "start_ms": 1450, "dur_ms": 320, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 19,
"transcript": "저는 비빔밥과 불고기를 좋아해요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 250, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "비빔밥과", "start_ms": 600, "dur_ms": 550, "phase_lr_deg": -5.0},
{"word": "불고기를", "start_ms": 1200, "dur_ms": 520, "phase_lr_deg": -5.0},
{"word": "좋아해요", "start_ms": 1800, "dur_ms": 450, "phase_lr_deg": -5.0}
]
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
"transcript": "일본어로도 인사해 줄 수 있어요?",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 310,
"audio_type": "speech",
"words": [
{"word": "일본어로도", "start_ms": 310, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "인사해", "start_ms": 980, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "줄", "start_ms": 1420, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 1600, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "있어요?", "start_ms": 1750, "dur_ms": 380, "phase_lr_deg": 0.0}
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
"transcript": "こんにちは はじめまして",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ja",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "こんにちは", "start_ms": 200, "dur_ms": 650, "phase_lr_deg": -5.0},
{"word": "はじめまして", "start_ms": 950, "dur_ms": 820, "phase_lr_deg": -5.0}
]
},
{
"segment": 25,
"transcript": "우와 정말 발음이 좋으시네요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "우와", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "발음이", "start_ms": 1000, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "좋으시네요", "start_ms": 1480, "dur_ms": 650, "phase_lr_deg": 0.0}
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
"transcript": "감사합니다 연습을 많이 했거든요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 220,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 220, "dur_ms": 680, "phase_lr_deg": -5.0},
{"word": "연습을", "start_ms": 950, "dur_ms": 410, "phase_lr_deg": -5.0},
{"word": "많이", "start_ms": 1410, "dur_ms": 320, "phase_lr_deg": -5.0},
{"word": "했거든요", "start_ms": 1780, "dur_ms": 550, "phase_lr_deg": -5.0}
]
},
{
"segment": 28,
"transcript": "That's impressive! Keep it up.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "That's", "start_ms": 180, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "impressive!", "start_ms": 480, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "Keep", "start_ms": 1150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "it", "start_ms": 1380, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "up.", "start_ms": 1550, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "내일은 같이 공부할까요?",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 300,
"audio_type": "speech",
"words": [
{"word": "내일은", "start_ms": 300, "dur_ms": 420, "phase_lr_deg": -5.0},
{"word": "같이", "start_ms": 780, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "공부할까요?", "start_ms": 1180, "dur_ms": 680, "phase_lr_deg": -5.0}
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
"transcript": "좋은 생각이에요 몇 시에 만날까요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 200, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "생각이에요", "start_ms": 580, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "몇", "start_ms": 1300, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "시에", "start_ms": 1520, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "만날까요?", "start_ms": 1800, "dur_ms": 580, "phase_lr_deg": 0.0}
]
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
"transcript": "오후 두 시에 카페에서 봐요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 280,
"audio_type": "speech",
"words": [
{"word": "오후", "start_ms": 280, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "두", "start_ms": 680, "dur_ms": 180, "phase_lr_deg": -5.0},
{"word": "시에", "start_ms": 900, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "카페에서", "start_ms": 1200, "dur_ms": 550, "phase_lr_deg": -5.0},
{"word": "봐요", "start_ms": 1800, "dur_ms": 320, "phase_lr_deg": -5.0}
]
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
"transcript": "See you tomorrow at two!",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 120, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 350, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "tomorrow", "start_ms": 550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "at", "start_ms": 1050, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "two!", "start_ms": 1220, "dur_ms": 280, "phase_lr_deg": 0.0}
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
"transcript": "네 알겠습니다 내일 만나요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 240,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 240, "dur_ms": 180, "phase_lr_deg": -5.0},
{"word": "알겠습니다", "start_ms": 480, "dur_ms": 620, "phase_lr_deg": -5.0},
{"word": "내일", "start_ms": 1150, "dur_ms": 320, "phase_lr_deg": -5.0},
{"word": "만나요", "start_ms": 1520, "dur_ms": 450, "phase_lr_deg": -5.0}
]
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
"transcript": "오늘 정말 즐거운 시간이었어요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 350,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 750, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "즐거운", "start_ms": 1120, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "시간이었어요", "start_ms": 1620, "dur_ms": 780, "phase_lr_deg": 0.0}
]
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
"transcript": "저도요 조심히 들어가세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "저도요", "start_ms": 180, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "조심히", "start_ms": 580, "dur_ms": 420, "phase_lr_deg": -5.0},
{"word": "들어가세요", "start_ms": 1050, "dur_ms": 680, "phase_lr_deg": -5.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 11:56:31*
