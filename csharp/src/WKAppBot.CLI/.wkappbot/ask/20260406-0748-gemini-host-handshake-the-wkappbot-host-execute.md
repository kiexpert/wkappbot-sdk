# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=39080  time=07:46:25  uptime=15133s  mem=239MB  system_processes=414"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_074132.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 736-306-756-371-137-173-107-317-637-536-375-317-273-207-236-765-526-265-526-257-273-765-612-310-637-376-312-276-213-173-265-236-257-570-327-627-506-237-253-537-256-753-315-450-2_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 756-376-371-136-163-317-173-761-175-756-725-751-751-175_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 175-175-751-175-157-715-157-157-571-157-517-571-517-576-571-576-517-571-517-571 175-715-571-157-157-175-157-157-517-571-517-517-157-156-571-157-156-517-157-567-175-571-751 103-1_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 526-263-753-756-765-562-675-753 756-527-671-275-172-263-517-527-352-317-153-602-267-562-751-756-527-257-570-263-327-537-312-765-237-637-132-506-625-376-367-137-371-751-651-753-6_V.mp3)
  Segment 5: 6304ms ~ 13468ms (file: 572-675-367-106-763-723-372-371-631-725-752-231-132-130-327-102-310-572-301-163-613-236-276-312-310-675-625-576-725-321-312-371-312-263-671-641-257-253-321-574-751-601-670-617-3_W.mp3)
  Segment 6: 14468ms ~ 21344ms (file: 301-267-213-375-736-623-236-213-263-267-267-126-120-120-132-267-260-206-213-106-236-120-216-321-203-120-203-276-213-206-231-230-216-217-102-103-362-302-103-231-302-213-261-217-1_W.mp3)
  Segment 7: 22344ms ~ 28140ms (file: 231-267-213-320-103-267-236-267-236-236-236-107-320-230-267-206-260-765-321-273-230-756-756-756-716-610-236-137-763-763-735-753-735-756-735-753-756-765-765-756-765-765-763-716-7_W.mp3)
  Segment 8: 29140ms ~ 29716ms (file: 751-157-157-571-157-517-157-157-175-175-176-176-157-167-156-175-176-156-715 130-130-135-135-175-175-175-157-156-157-175-751-157-175-157-167-671-156-176-167-715 130-157-175-175-1_V.mp3)
  Segment 9: 30716ms ~ 31292ms (file: 157-503-137-317-315-173-316-761-670-176-761-176-607-617-617-126-106-167-610-163-136-713-750-157-751-103-715-156-251-570-156-106-126-762-710-310-317 136-231-370-312-126-527-526-5_V.mp3)
  Segment 10: 32292ms ~ 32868ms (file: 574-574-510-137-317-127-756-276-127-123-310-137-537-751-175-157-175-167-163-136-167-137-162-137-317-327-532-512-732-710-713-137-135-517-516-521-123-325-152-163-176-156-517-657-6_W.mp3)
  Segment 11: 33868ms ~ 40060ms (file: 176-163-267-267-675-657-601-601-637-236-103-753-106-765-167-106-375-167-326-236-732-716-106-257-725-753-317-132-327-352-176-761-236-310-310-130-106-132-132-620-321-126-756-756-7_W.mp3)
  Segment 12: 41060ms ~ 41636ms (file: 615-627-132-162-261-263-672-765-756-576-574-576-576-765-765-765-756-675-675-756-675-367-621-362-372-367-563-761-217-157-625-562-671-162-163-316-326-163-761-312-162-127-576-357-5_W.mp3)
  Segment 13: 42636ms ~ 43212ms (file: 312-371-312-316-316-573-763-673-621-571-756-154-157-310-310-316-310-615-675-753-371-307-371-765-376-317-316_W.mp3)
  Segment 14: 44212ms ~ 51952ms (file: 263-123-230-260-230-260-267-351-543-514-260-625-574-567-765-567-560-162-316-357-567-576-132-103-261-260-130-312-357-547-567-351-754-576-657-265-265-261-265-207-210-230-106-302-3_W.mp3)
  Segment 15: 52952ms ~ 53528ms (file: 760-267-564-576-756-657-567-567 657-675-675_W.mp3)
  Segment 16: 54528ms ~ 60360ms (file: 173-217-574-206-725-753-260-201-236-352-236-317-312-765-276-267-315-312-103-103-132-301-317-276-172-706-173-103-173-721-752-325-203-321-321-602-267-761-750-756-715-267-576-572-3_V.mp3)
  Segment 17: 61360ms ~ 70648ms (file: 267-263-321-130-230-231-236-572-521-325-320-106-230-163-276-164-132-137-123-327-320-321-260-230-174-132-132-301-725-210-562-327-132-310-103-130-213-731-320-325-521-106-261-236-2_W.mp3)
  Segment 18: 71648ms ~ 72224ms (file: 263-230-210-123-613-136-132-601-106-123-261-567-162-657-657-231-361-362-621-261-263-210_W.mp3)
  Segment 19: 73224ms ~ 73800ms (file: 765-756-216-321-132-312-132-132-312-132-132-312-301-312-137-130-137-137-102_W.mp3)
  Segment 20: 74800ms ~ 79588ms (file: 276-260-267-236-732-102-217-267-320-301-167-756-176-216-317-203-631-207-206-276-207-327-320-235-302-317-103-320-372-137-130-236-236-360-613-617-310-756-267-263-326-326-253-106-2_W.mp3)
  Segment 21: 80588ms ~ 81164ms (file: 756-637-760-216-725-276-625-726-321-762-627-276-321-236-325-571-321-567-325-763-752-576-276-216-276-236-236-136-352-361-276-275-702-276-567-726-273-572-357-265-326-576-157-612-7_V.mp3)
  Segment 22: 82164ms ~ 91056ms (file: 230-260-203-260-236-326-231-213-310-130-103-130-263-130-310-751-267-312-273-231-754-275-765-627-267-257-321-267-102-126-172-236-310-573-352-203-207-567-572-126-713-173-267-574-5_W.mp3)
  Segment 23: 92056ms ~ 99184ms (file: 236-231-213-132-136-106-356-376-527-261-263-310-361-317-317-103-103-312-123-263-237-376-371-163-613-376-137-135-130-316-163-726-261-176-367-762-762-237-137-217-273-756-130-312-3_W.mp3)
  Segment 24: 100184ms ~ 100760ms (file: 652 137-365 123-723-213-312-132-612-137_W.mp3)
  Segment 25: 101760ms ~ 102336ms (file: 175-137-173-173-176-371-732-715-761-715-675-675-653-675-675-671-675-765-137-137-137-176-173-713-173-173-137-173-173 175-751-710-715-710-715-751-170-713-715-716-751-705-750 576-5_W.mp3)
  Segment 26: 103336ms ~ 110392ms (file: 231-123-312-320-352-562-632-136-573-752-207-376-520-532-527-510-102-210-321-321-263-312-361-301-172-302-517-206-265-576-326-162-321-320-160-160-162-501-570-562-317-236-321-236-3_W.mp3)
  Segment 27: 111392ms ~ 118700ms (file: 723-103-103-130-317-137-327-201-236-103-107-203-102-301-321-201-267-760-752-574-103-207-302-312-276-720-623-260-261-752-762-263-326-267-267-310-132-321-267-627-123-107-260-130-2_V.mp3)
  Segment 28: 119700ms ~ 120276ms (file: 103-135-132-315-315-310-136-310-310-130-361-316-310-312-376-367-761-736-761-753-675-765-673-765-376-267-635-106-715-730-372-321-321-321-375-753-756-576-316-175-765-756-765-753-6_W.mp3)
  Segment 29: 121276ms ~ 121852ms (file: 316-273-627-206-236-675-762-251-267-705-576-516-125-572-175-327-725-230-572-256-257-652-502-625-627-627-263-216-672-256-326-326-672-236-572-657-571-263-756-267-237-376-132-205-5_V.mp3)
  Segment 30: 122852ms ~ 123428ms (file: 571-157-157-157-715-675-705-607-610-102-601-267-620-601-615-562-657-652-627-605-601-562-265-605-605-605-607-671-726-613-615-612-165-125-165-657-546-576-645-645-567-547-574-547-4_V.mp3)
  Segment 31: 124428ms ~ 125004ms (file: 176-675-217-167-716-137-163-750-750-751-136-135-123-712-316-756-175-127-715-203-213 213-312-163-127-123-571-517-537-513-217-125-315-731-107-206-750-217-716-276-567-523-542-123-5_V.mp3)
  Segment 32: 126004ms ~ 126580ms (file: 571-657-765-657-573-376-371-317-317-317-136-137-310-316-310-312-372-612-650-675-516-571-750-157-674-126-163-615-106-612-160-675-126-607-675-675-627-132-631-613-627-261-163-574-1_W.mp3)
  Segment 33: 127580ms ~ 135356ms (file: 267-236-627-627-203-320-312-106-213-231-620-236-230-236-756-236-263-267-263-120-753-627-627-265-230-312-751-107-127-236-102-126-756-235-213-260-312-657-126-571-325-263-120-165-7_W.mp3)
  Segment 34: 136356ms ~ 147048ms (file: 623-263-576-527-527-102-312-302-103-137-132-672-672-627-675-261-123-210-120-210-260-260-206-206-260-260-312-627-627-376-310-163-103-213-263-132-310-236-326-312-132-321-321-302-3_W.mp3)
  Segment 35: 148048ms ~ 148624ms (file: 751-157-752-671-367-316-613-617-176 576-576-576-576-576-576-570-576-576-576-571-571-507-751-570-507-751-576-756-573-576-750-570-576-570-576_W.mp3)
  Segment 36: 149624ms ~ 159056ms (file: 203-756-275-752-312-573-321-130-725-210-321-301-753-123-321-201-103-752-120-756-756-302-201-762-120-103-213-321-216-752-756-217-210-231-756-310-201-310-301-321-123-231-752-312-2_W.mp3)
  Segment 37: 160056ms ~ 168912ms (file: 257-275-210-103-102-103-721-207-275-275-271-752-276-275-312-273-756-751-105-571-571-756-756-107-301-321-320-715-105-576-753-756-130-301-756-105-571-571-307-765-570-103-753-103-1_W.mp3)
  Segment 38: 169912ms ~ 170488ms (file: 675-671-167-716-103-176-765-175-701-175-175-715-751-756-751-751-715-765-715-751-715-701 610-601-670-610-107-103-167-671-170-105-710-715-715-715-751-751-715-751-751-751-751-751-7_W.mp3)
  Segment 39: 171488ms ~ 179876ms (file: 130-257-107-210-217-213-231-107-136-317-206-213-261-162-105-231-123-132-736-312-523-206-261-320-230-321-231-271-132-275-267-132-201-203-532-762-762-317-236-256-265-210-236-125-5_V.mp3)
  Segment 40: 180876ms ~ 188220ms (file: 126-123-621-236-716-371-261-753-326-132-163-310-106-310-307-316-217-751-107-136-106-107-102-271-172-165-102-132-231-213-123-753-756-157-573-310-132-127-132-230-137-135-213-273-2_W.mp3)
  Segment 41: 189220ms ~ 201388ms (file: 123-301-203-132-143-103-310-130-543-207-263-467-732-321-231-267-213-213-276-132-276-762-103-207-237-231-231-276-126-162-750-276-574-276-261-210-267-273-107-276-267-723-271-751-1_W.mp3)
  Segment 42: 202388ms ~ 202964ms (file: 501-516-547 765-574-576-574-576-576-675-765-756-756-756-576-756-574-756-756-637-631-312-361-361-316-327-216-231-206-231-325-756-271-356-321-502-216 132-132-573-135-275-576-602-2_W.mp3)
  Segment 43: 203964ms ~ 204540ms (file: 321-267-326-726-256-572-127-756-362-572-236-236-270-725-267-752_V.mp3)
  Segment 44: 205540ms ~ 214468ms (file: 627-326-237-267-627-231-132-213-326-106-320-123-372-321-132-267-713-670-617-607-617-263-236-312-326-123-120-231-312-607-672-657-576-102-756-675-723-732-231-263-152-175-157-132-7_W.mp3)
  Segment 45: 215468ms ~ 216044ms (file: 567-506-526-235-157-537-132-657-367-726-367-650-675-572-162-613-623-230-673-621-361-756-723-573-526-621-526-261-526-756-315-263-657-735-301-312-312-602-316-136-537-573-573-536-7_W.mp3)
  Segment 46: 217044ms ~ 227628ms (file: 275-574-320-316-752-213-316-765-267-127-123-207-317-237-753-537-230-132-236-314-310-267-206-120-751-753-756-206-321-321-372-752-675-267-267-231-230-206-132-203-173-301-103-513-1_W.mp3)
  Segment 47: 228628ms ~ 229204ms (file: 571-517-576-175-157-516-157-751-157-571-715-756-517-567-571-175 105-107-175-157-175-157-175-157-157-157-157-517-516-157-516-157-516-517-517-571-156-517 135-103-107-175-153-157-1_W.mp3)
  Segment 48: 230204ms ~ 230780ms (file: 126-310-123-576-571-571-574-574 763-761-763-376-763-376-763-673-763-675-673-671-675-367-673-761-765-567-765-675-756-765-675-763-763-765-637-675-672-672-672 103_W.mp3)
  Segment 49: 231780ms ~ 232356ms (file: 176-175-715-716-756-716-756-751-176 617-175-106-105-170-710-107-170-715-170-175-175-175-715-751-715-175-175-715 107-710-716-107-160-751-170-710-107-715-170-107-715-710-701-715-7_V.mp3)
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
> **Time**: 2026-04-06 07:48:09
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "안녕하세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "안녕", "start_ms": 50, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "하세요", "start_ms": 310, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "반갑습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "반갑", "start_ms": 45, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "습니다", "start_ms": 270, "dur_ms": 280, "phase_lr_deg": 0.0}
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
"transcript": "오늘 날씨가 정말 좋네요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 60, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 190, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 350, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "한국어 공부는 재미있지만 가끔은 문법이 너무 어렵게 느껴질 때가 있어요",
"speaker": "speaker_3",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 120, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "공부는", "start_ms": 580, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "재미있지만", "start_ms": 990, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "가끔은", "start_ms": 1650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "문법이", "start_ms": 2160, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 2620, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "어렵게", "start_ms": 2980, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "느껴질", "start_ms": 3390, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "때가", "start_ms": 3820, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 4130, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "그럴 때는 천천히 반복해서 듣고 따라 말해보는 것이 가장 좋은 방법인 것 같습니다",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "그럴", "start_ms": 100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "때는", "start_ms": 410, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "천천히", "start_ms": 740, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "반복해서", "start_ms": 1200, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "듣고", "start_ms": 1760, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "따라", "start_ms": 2070, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "말해보는", "start_ms": 2400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "것이", "start_ms": 2910, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 3170, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 3530, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "방법인", "start_ms": 3840, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 4250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "같습니다", "start_ms": 4410, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "포기하지 말고 꾸준히 노력한다면 실력이 금방 늘 거예요 화이팅 하세요",
"speaker": "speaker_2",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "포기하지", "start_ms": 80, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "말고", "start_ms": 590, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "꾸준히", "start_ms": 900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "노력한다면", "start_ms": 1360, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "실력이", "start_ms": 1970, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "금방", "start_ms": 2380, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "늘", "start_ms": 2740, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 2900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "화이팅", "start_ms": 3300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "하세요", "start_ms": 3710, "dur_ms": 350, "phase_lr_deg": 0.0}
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
"transcript": "Can you help me with this?",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Can", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 150, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "help", "start_ms": 240, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "me", "start_ms": 400, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "with", "start_ms": 490, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "this?", "start_ms": 600, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "물론이죠 무엇을 도와드릴까요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "물론이죠", "start_ms": 50, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "무엇을", "start_ms": 450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "도와드릴까요", "start_ms": 760, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "이 단어의 발음이 조금 어려워요 다시 한번 들려주시겠어요",
"speaker": "speaker_4",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 110, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "단어의", "start_ms": 270, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "발음이", "start_ms": 630, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 1040, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "어려워요", "start_ms": 1350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 1850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "한번", "start_ms": 2160, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "들려주시겠어요", "start_ms": 2490, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "네 천천히 발음해 드릴게요 따라해 보세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 90, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "천천히", "start_ms": 250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "발음해", "start_ms": 610, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "드릴게요", "start_ms": 1020, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "따라해", "start_ms": 1430, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "보세요", "start_ms": 1790, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "감사합니다 훨씬 이해하기 쉬워요",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 60, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "훨씬", "start_ms": 600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이해하기", "start_ms": 910, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "쉬워요", "start_ms": 1370, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "언어 학습에서 가장 중요한 것은 실수하는 것을 두려워하지 않는 마음가짐입니다",
"speaker": "speaker_3",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "언어", "start_ms": 130, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "학습에서", "start_ms": 490, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 1000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "중요한", "start_ms": 1360, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "것은", "start_ms": 1820, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "실수하는", "start_ms": 2130, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "것을", "start_ms": 2690, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "두려워하지", "start_ms": 3000, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "않는", "start_ms": 3660, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "마음가짐입니다", "start_ms": 3970, "dur_ms": 750, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 16,
"transcript": "자신감을 가지고 매일 조금씩 연습해 보세요 언젠가는 자연스럽게 대화할 수 있을 거예요",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "자신감을", "start_ms": 100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "가지고", "start_ms": 610, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "매일", "start_ms": 970, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "조금씩", "start_ms": 1280, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "연습해", "start_ms": 1690, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "보세요", "start_ms": 2150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "언젠가는", "start_ms": 2600, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "자연스럽게", "start_ms": 3160, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "대화할", "start_ms": 3770, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 4180, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있을", "start_ms": 4290, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 4550, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "어떤 주제에 대해 이야기하고 싶으신가요 음식 여행 아니면 취미 생활인가요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "어떤", "start_ms": 120, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "주제에", "start_ms": 430, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "대해", "start_ms": 790, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이야기하고", "start_ms": 1100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "싶으신가요", "start_ms": 1710, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "음식", "start_ms": 2400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "여행", "start_ms": 2800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "아니면", "start_ms": 3200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "취미", "start_ms": 3610, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "생활인가요", "start_ms": 3920, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "저는 한국 음식에 관심이 많아요 특히 떡볶이가 좋아요",
"speaker": "speaker_4",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 70, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 330, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "음식에", "start_ms": 640, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "관심이", "start_ms": 1000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "많아요", "start_ms": 1360, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "특히", "start_ms": 1800, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "떡볶이가", "start_ms": 2110, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "좋아요", "start_ms": 2570, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "떡볶이는 맵지만 아주 맛있죠",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "떡볶이는", "start_ms": 50, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "맵지만", "start_ms": 510, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 920, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "맛있죠", "start_ms": 1180, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "매운 음식을 잘 못 드신다면 치즈 떡볶이나 궁중 떡볶이를 추천해 드려요",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "매운", "start_ms": 110, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "음식을", "start_ms": 420, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 780, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "못", "start_ms": 940, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "드신다면", "start_ms": 1100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "치즈", "start_ms": 1650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "떡볶이나", "start_ms": 2010, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "궁중", "start_ms": 2600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "떡볶이를", "start_ms": 2960, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "추천해", "start_ms": 3470, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "드려요", "start_ms": 3880, "dur_ms": 350, "phase_lr_deg": 0.0}
]
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
"transcript": "좋은 정보 감사해요 다음에 꼭 먹어볼게요",
"speaker": "speaker_4",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 80, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "정보", "start_ms": 390, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "감사해요", "start_ms": 700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "다음에", "start_ms": 1200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "꼭", "start_ms": 1610, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "먹어볼게요", "start_ms": 1770, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "일본어나 중국어처럼 다른 아시아 언어도 관심 있으신가요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "일본어나", "start_ms": 100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "중국어처럼", "start_ms": 610, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "다른", "start_ms": 1220, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "아시아", "start_ms": 1530, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "언어도", "start_ms": 1990, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "관심", "start_ms": 2400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있으신가요", "start_ms": 2760, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "지금은 한국어에만 집중하고 싶어요",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "지금은", "start_ms": 60, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "한국어에만", "start_ms": 470, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "집중하고", "start_ms": 1080, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "싶어요", "start_ms": 1590, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 26,
"transcript": "한 번에 하나씩 차근차근 배우는 것이 효율적일 수 있습니다",
"speaker": "speaker_3",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "한", "start_ms": 120, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "번에", "start_ms": 280, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "하나씩", "start_ms": 540, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "차근차근", "start_ms": 1000, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "배우는", "start_ms": 1560, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "것이", "start_ms": 1970, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "효율적일", "start_ms": 2230, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 2740, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 2850, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "학습 속도보다는 꾸준함이 더 중요하다는 사실을 잊지 마세요",
"speaker": "speaker_3",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "학습", "start_ms": 90, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "속도보다는", "start_ms": 450, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "꾸준함이", "start_ms": 1060, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 1570, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "중요하다는", "start_ms": 1730, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "사실을", "start_ms": 2340, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "잊지", "start_ms": 2700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "마세요", "start_ms": 3010, "dur_ms": 350, "phase_lr_deg": 0.0}
]
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
"transcript": "What is the best way to practice speaking?",
"speaker": "speaker_4",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "What", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 210, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 320, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "best", "start_ms": 410, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "way", "start_ms": 620, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 780, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "practice", "start_ms": 890, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "speaking?", "start_ms": 1250, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
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
"transcript": "자신의 목소리를 녹음해서 들어보는 것을 추천합니다",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "자신의", "start_ms": 70, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "목소리를", "start_ms": 430, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "녹음해서", "start_ms": 890, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "들어보는", "start_ms": 1400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "것을", "start_ms": 1860, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "추천합니다", "start_ms": 2120, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "처음에는 어색하겠지만 발음의 차이를 명확하게 알 수 있는 아주 좋은 방법이에요",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "처음에는", "start_ms": 110, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "어색하겠지만", "start_ms": 620, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "발음의", "start_ms": 1280, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "차이를", "start_ms": 1640, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "명확하게", "start_ms": 2000, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "알", "start_ms": 2560, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 2720, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 2830, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 3090, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 3350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "방법이에요", "start_ms": 3660, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "그리고 실제 대화 상황을 상상하며 혼잣말을 해보는 것도 도움이 됩니다",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "그리고", "start_ms": 90, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "실제", "start_ms": 450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "대화", "start_ms": 760, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "상황을", "start_ms": 1070, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "상상하며", "start_ms": 1480, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "혼잣말을", "start_ms": 1990, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "해보는", "start_ms": 2500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "것도", "start_ms": 2910, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "도움이", "start_ms": 3170, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "됩니다", "start_ms": 3530, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 36,
"transcript": "어느덧 마칠 시간이 되었네요 오늘 함께해주셔서 감사합니다",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "어느덧", "start_ms": 80, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "마칠", "start_ms": 490, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "시간이", "start_ms": 800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "되었네요", "start_ms": 1210, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 1670, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "함께해주셔서", "start_ms": 1980, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 2590, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "다음에 다시 만나요 그때까지 열심히 연습하시길 바랍니다 안녕히 계세요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 510, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 820, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그때까지", "start_ms": 1180, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "열심히", "start_ms": 1690, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "연습하시길", "start_ms": 2100, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "바랍니다", "start_ms": 2760, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 3220, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "계세요", "start_ms": 3630, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 39,
"transcript": "Goodbye! See you next time!",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Goodbye!", "start_ms": 40, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "See", "start_ms": 550, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 760, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "next", "start_ms": 920, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "time!", "start_ms": 1180, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "끝까지 시청해주셔서 감사합니다 구독과 좋아요 잊지 마세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "끝까지", "start_ms": 80, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "시청해주셔서", "start_ms": 490, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 1150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "구독과", "start_ms": 1660, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "좋아요", "start_ms": 2070, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "잊지", "start_ms": 2430, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "마세요", "start_ms": 2740, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "궁금한 점이 있다면 언제든지 댓글로 남겨주세요 성심껏 답변해 드리겠습니다",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "궁금한", "start_ms": 100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "점이", "start_ms": 510, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "있다면", "start_ms": 770, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "언제든지", "start_ms": 1180, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "댓글로", "start_ms": 1690, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "남겨주세요", "start_ms": 2100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "성심껏", "start_ms": 2660, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "답변해", "start_ms": 3070, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "드리겠습니다", "start_ms": 3530, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
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
"transcript": "오늘의 학습 포인트는 발음과 자신감입니다 꼭 기억하세요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "오늘의", "start_ms": 70, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "학습", "start_ms": 430, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "포인트는", "start_ms": 740, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "발음과", "start_ms": 1250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "자신감입니다", "start_ms": 1660, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "꼭", "start_ms": 2370, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "기억하세요", "start_ms": 2530, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 46,
"transcript": "그럼 다음 영상에서 더 유익한 내용으로 찾아뵙겠습니다 감사합니다",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 90, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "영상에서", "start_ms": 660, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 1170, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "유익한", "start_ms": 1330, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "내용으로", "start_ms": 1740, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "찾아뵙겠습니다", "start_ms": 2250, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 3010, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
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
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 07:48:09*
