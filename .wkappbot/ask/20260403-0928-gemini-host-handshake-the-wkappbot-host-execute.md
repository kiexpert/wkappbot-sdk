# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=09:28:07  uptime=29333s  mem=1886MB  system_processes=451"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_092758.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 132-135-132-132-132-321-132-132-135-132-132-135-613-753-756-573-756-756-752-756-567-567-576-567-567-657-123_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 102-760-170-651-167-132-103-136-102-127-261-615-236-213-162-163-163-603-126-103-132-106-162-167-106-103-106-167-261 216 103-132-617-267-567-715-735-137-317-136-167-160-106-163-3_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 321-130-362-203-236-316-713-106-217-125-615-132-716-132-103-163-301-136-126-167 230-203-132-310-103-301-265-167-675-526-132-103-103-103-715-756-106-123-125-321-263-125-657-263-3_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 317-317-137 675-367-671-672-672-672-671-673-672-670-673-673-673-670-674-672-657-675-671-672-617-671-671-617-672-673-671-671-671-675-675-637-674-674-672-673-631-673-627-672-675-6_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 167-165-617-167-612-627-672-765-576-576-576-576-756-567-576-561-576-576-657-567-675-765-576-576-657-637-765-576-765-756 752-367_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 631-672-632-312-136-320-236-263-263-231-126-263-263-267-263-627-265-762-672-216-267-261-326-236-261-621-312-316-362-631-317-136-136-361-267-762-623-760-765-765-765-765-765-756-7_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 106-163-671-675-631-370-136-701-761-672-672-610-173-576-547-162-301-312-627-132-162-162-326-260-623-263-126-321-231-571-160-162-547-376-167-756-735-130-321-756-701-170-236-320-3_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 670-657-163-157-721-761 627-231-671-613-675-621-657-267-263-162-675-756-120-123-216-326_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 136-627-163-126-132-137-136-761-715-163-106-167-162-107-103-103-137-130-106-167-612-512-623-675-261-263-263-132-167-576-756-762-312-317-176-126-162-163-506-507-547-130-576-576-6_V.mp3)
  Segment 10: 14184ms ~ 27432ms (file: 627-172-175-217-127-312-132-573-513-357-356-320-132-123-132-352-102-320-632-160-361-632-163-103-765-765-760-706-756-752-673-357-546-534-137-531-137-573-537-675-236-321-651-165-1_W.mp3)
  Segment 11: 28432ms ~ 29008ms (file: 176-657-201-362-361-632-236-213-231-263-265-752-267-263-625-261-216-263-261-263-621-162-216-301-126 675-760-675-137-231-123-132-126-612-627-167-657-726-672-672-761-716-271-627-6_W.mp3)
  Segment 12: 30008ms ~ 30584ms (file: 126-627-236-752-627-752-765-765-376-762-675-637-671-362-756-675-762-627-765-671-672-675-263-673-627-675-326-625-236-627-627-527-620-256-652-657-657-765-526-357-672-625-657-527-5_W.mp3)
  Segment 13: 31584ms ~ 32160ms (file: 231-130-621-621 127-126-123-203-236-231-132-201-210-203-216-261-602-216-123-312-312-320-321-130-312-132-132-123-301-210-260-210_V.mp3)
  Segment 14: 33160ms ~ 33736ms (file: 103-130-316-103-132-123-136-103-132-301-301-302-231-302-321-132-132-132-167-706-507-106-165-176 312-513-754-165-132-213-123-164-213-127-132-123-132-124-137-145-132-124-162-162-6_V.mp3)
  Segment 15: 34736ms ~ 35312ms (file: 162-163-103-165-103-612-163-160-167-136-612-162 126-107-762-126_V.mp3)
  Segment 16: 36312ms ~ 36888ms (file: 135-361-126 765-671-132 261-216-627 123-617-236-126-361-163 261-263-163-326_V.mp3)
  Segment 17: 37888ms ~ 38464ms (file: 130 102-312-162-126-136-132-136-136 167-673-136-670-675-657-675-167-675-715-675-615-672-627-576-652-576-672-216-103-613-162-106-261-673-167-602-103-106-136-106 235-526-216-601-1_W.mp3)
  Segment 18: 39464ms ~ 40040ms (file: 132-301-312 103-136-163-316-162-137-361-137-312-321-132-137-126-362-316-176-130-136-136-132-136-316-136-645-103-316-162-126-163-167-136-132-163-103-132-763-137-351-130-103-136-1_V.mp3)
  Segment 19: 41040ms ~ 41616ms (file: 764-765-765-675-576-576-576-765-765-135-673-312-736-760-675-263-132-103-612-602-216-106-162-612-617-162-516-231-132-765-176-136-157-571-136-132-102-320-127-612-136-163-136-136-1_W.mp3)
  Segment 20: 42616ms ~ 43192ms (file: 136-316-136-103-136-163-137-175-130-675-167-162_V.mp3)
  Segment 21: 44192ms ~ 44768ms (file: 706 713-132-765-237-713-246-136-162-126-321-213-231-213-231-325 231-312_W.mp3)
  Segment 22: 45768ms ~ 46344ms (file: 273-237-167-617-617-167-613-130-163-615-750-167-106-106-176-127-120-216-625-621-156-167-176-756-260-725-752-321-305-573-512-175-671-263-261-263-216-625-607-627-657-657-106-163-6_W.mp3)
  Segment 23: 47344ms ~ 47920ms (file: 761-715-175-715-123-312-326-123-165-261-106-761-126-620-621-671-316_V.mp3)
  Segment 24: 48920ms ~ 49496ms (file: 176-176-756-765-671-615-621-615-231-126-102-671-136-102-132-251-621-567-103-615-631-765-165-103-103-167-103-136-136-103-610-103-253-615-103-136-163-130-162-103-103-132-163-567-7_V.mp3)
  Segment 25: 50496ms ~ 51072ms (file: 103-160-617-253-670-102-136-162-130-162-126-160-132-106-167-103-103-617-326-261-103-103-150-103-136-657-126-176-263-216-312_V.mp3)
  Segment 26: 52072ms ~ 52648ms (file: 103-765-765-760-107-306-312-310-130-301-103-132-362-375-756-756-703-706-673-576-263-762-762-267-671-617-623-620-756-752-720-540-132-326-301-310-713-765-706-752-317-302-302-132 5_V.mp3)
  Segment 27: 53648ms ~ 54224ms (file: 167-567-615-106-103-132-321-310-623-375-750-752-206-260-765-754-754-176-176-260-623-203-302-302-135-765-670-103-160-716-675-627-657-627-627-632-613-107-173-132-263-213-302-301-3_V.mp3)
  Segment 28: 55224ms ~ 55800ms (file: 372-276-321-312-321-132-213-132-316-312-735-321-326-263-312-316-306-302-302-130-635-126-136-671-640-236-126-263-321-321-132-312-176-635-126-716-160-135-163-276-207-736-326-267-6_W.mp3)
  Segment 29: 56800ms ~ 57376ms (file: 163-135-127-312-103-632-162-130-316-163-162-106-136-153-613 163-123-160-103-163-130-136-136-261-617-136-136-103-160-106-136-103-103-103-156-172-316-106-167-106-716-671-106-316-1_V.mp3)
  Segment 30: 58376ms ~ 58952ms (file: 561-103-106-172-306-617-761-601-103-167-617-162-765-631-163-160-130-162-157-601-103-136-103-103-132-136-162-216-321-517-126-106-103-136-132-126-601-136-675-605-671-601-601-123-1_V.mp3)
  Segment 31: 59952ms ~ 60528ms (file: 672-765-673-627-763-361-132-361-236-321-326 210-213-231-230-213 132-612-136-106-136-132-312-540-543-541-163-316-362-312-316-706-617-106-136-316-136-571-537-756-657-705-607-617-6_W.mp3)
  Segment 32: 61528ms ~ 62104ms (file: 617-310-312-132-567-165-617-765-657-672-675-627-631-765-671-762-756-675-621-621-612-612-627-627-126-105-507-501-276 763_W.mp3)
  Segment 33: 63104ms ~ 63680ms (file: 716-726-627-670-675-576-756-276-672 267-672-267-621-612-621-621-261-260-123-216-261-106-106-106-602-620-620-620-267-726-627-712-670-670-607-627-263-261-620-620-602-620-610-163-1_V.mp3)
  Segment 34: 64680ms ~ 65256ms (file: 302-502-537-307-136-157-653-136-132-123-132-657-302-321-132-617-326-231-321-132-123-135-167-236-206-126-540-312-167-106-203-263-267-270-574-213-236-230-326-321-302-375-351-136-1_V.mp3)
  Segment 35: 66256ms ~ 66832ms (file: 762-765-765-706-765-765-756-756 756-765-765-762-136-130-163-671-567-106-107-162-103-157 362-361-312-103-163-672-672-671-617-152-521-132-201-120-671-103-130-316-137-670-672-617-6_W.mp3)
  Segment 36: 67832ms ~ 68408ms (file: 236-265-265-265-256-562-235-563-562-256-265-625-652-621-526-562-567-562-625-657-675-317-312-132-132-123-532-562-235-265-265-261-236-236-263-126-123-316-612-326-263-261-316-526-5_W.mp3)
  Segment 37: 69408ms ~ 69984ms (file: 132-130-175-135-632-103-612-136-103-132-103-176-127-136-136-637-103-136-106-136-137-130-163-103-163-673-103-106-137-317-103-175-136-162-160-152-126-631-106-167-106-103-103-136-1_V.mp3)
  Segment 38: 70984ms ~ 71560ms (file: 567-576-576 267-617-163-613-627-261-123-120-136-201-213-126-102-126-216-321-263-267-623-613-231-326-321-312-571-107-517-750-570 236-276-276-267-236-542-546-267-623-236-203-576-5_W.mp3)
  Segment 39: 72560ms ~ 73136ms (file: 731-372-327-102-623-132 136-627-123-163-675-756-756-756-765-675-675-657-657 670-657-756-756_W.mp3)
  Segment 40: 74136ms ~ 74712ms (file: 361-312-253-671-631-156-613-567-651-265-267-156-156-157-516-615-652-156-567-573-573-517-165-126-162-261-265-265-253-275-375-317-321-237-217-215-125-123-123-123-125-325-317-312-3_W.mp3)
  Segment 41: 75712ms ~ 76288ms (file: 103-103-130-201-321-261-201-301-312-102-601-103-610-106-103-136-130-136-103-103-137-136-130-301-310-132-350-306-230-320-301-123-320-102-132-310-301-130-102-106-103-120-263-632-6_V.mp3)
  Segment 42: 77288ms ~ 77864ms (file: 675-130-517-637-570-750-103-103-130-103-103-135-103-103-103-103-574-756-135-132-576-631-137-570-576-567-517-756-576-576-567-310-601-162-601-143-175-756-715-261-573-105-103-316-2_V.mp3)
  Segment 43: 78864ms ~ 79440ms (file: 301-310-316 270-270-276-162-321-106-163_V.mp3)
  Segment 44: 80440ms ~ 81016ms (file: 765-756-752 213-701-706-701-704-672_W.mp3)
  Segment 45: 82016ms ~ 82592ms (file: 657-765-301-302-305-367-637-623-302-312-321-507-507-305 162-316-167-206-263-206-126-574-567-203-230-260-230-203-326-230-320-326-203-203-263-302-302-602-620-326-302-326-302-302-3_V.mp3)
  Segment 46: 83592ms ~ 84168ms (file: 763-763 621-126-516-106-362-136-163-136-163-103-106-136-106-617-165-136-106-136-106-103-675-765-165-163-613-136-103-312-613-301-130-126-671-612-617-106-103-163-130-103-316-103-1_V.mp3)
  Segment 47: 85168ms ~ 85744ms (file: 765-675-765-637-136-320-302-572-570-750-260-756-752-376-130-137-103-362-306-362-260-621-670-106-106-675-167-601-167-617-602-620-621-176-165-316-312-601-615-267-260-263-267-267-1_V.mp3)
  Segment 48: 86744ms ~ 87320ms (file: 756-705-726-137-123-103-132-136-163-620-231-130-312-302-132-602-620-206-302-320-301-620-631-326-367-673-602-162-652-625-502-572-203-320-530-547-576-675-657-203-236-520-765-126-5_V.mp3)
  Segment 49: 88320ms ~ 88896ms (file: 537 527-561-312-316-361-326-132-627-632-621-126-132-261-126-316-670-671-301-301-326-302-320-576-352-651-601-236-263-320-326-302-765-675-637-163-206-260-610-603-165-126-612-162-6_V.mp3)
  Segment 50: 89896ms ~ 90472ms (file: 253-235-235-321-215-215-250-235-326-310-310-352-523-235-257-253-235-327-257-524-532-523-352-352-325-321-327-321-327-235-325-325-321-235-321-235-321-326-312-325-326-213-231-325-2_W.mp3)
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
> **Time**: 2026-04-03 09:28:57
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "포크와 양보가 필요합니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "포크와", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "양보가", "start_ms": 200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "필요합니다", "start_ms": 360, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "어디서 내려요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "어디서", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "내려요", "start_ms": 260, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "이거 얼마예요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "이거", "start_ms": 30, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "얼마예요", "start_ms": 240, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "영수증 주세요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "영수증", "start_ms": 60, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 270, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "화장실 어디예요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "화장실", "start_ms": 45, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "어디예요", "start_ms": 255, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "도와주세요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "도와주세요", "start_ms": 60, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "조심하세요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "조심하세요", "start_ms": 40, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "길을 잃었어요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "길을", "start_ms": 65, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "잃었어요", "start_ms": 225, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "배고파요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "배고파요", "start_ms": 40, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "목말라요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "목말라요", "start_ms": 70, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "졸려요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "졸려요", "start_ms": 50, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "피곤해요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "피곤해요", "start_ms": 80, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "아파요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "아파요", "start_ms": 30, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "괜찮아요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "괜찮아요", "start_ms": 65, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "알겠어요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "알겠어요", "start_ms": 40, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "모르겠어요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "모르겠어요", "start_ms": 55, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "맞아요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "맞아요", "start_ms": 35, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "아니요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "아니요", "start_ms": 65, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "좋아요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "좋아요", "start_ms": 45, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "싫어요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 75,
"audio_type": "speech",
"words": [
{"word": "싫어요", "start_ms": 75, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "기뻐요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "기뻐요", "start_ms": 50, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "슬퍼요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "슬퍼요", "start_ms": 80, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "화나요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "화나요", "start_ms": 30, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "심심해요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "심심해요", "start_ms": 65, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "재밌어요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "재밌어요", "start_ms": 40, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "맛있어요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "맛있어요", "start_ms": 55, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "맛없어요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "맛없어요", "start_ms": 35, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "비싸요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "비싸요", "start_ms": 70, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "싸요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "싸요", "start_ms": 40, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "커요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 75,
"audio_type": "speech",
"words": [
{"word": "커요", "start_ms": 75, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "작아요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "작아요", "start_ms": 50, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "무거워요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "무거워요", "start_ms": 80, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "가벼워요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "가벼워요", "start_ms": 30, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "뜨거워요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "뜨거워요", "start_ms": 65, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "차거워요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "차거워요", "start_ms": 40, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "빠르다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "빠르다", "start_ms": 55, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "느리다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "느리다", "start_ms": 35, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "멀다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "멀다", "start_ms": 65, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "가깝다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "가깝다", "start_ms": 40, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "쉽다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "쉽다", "start_ms": 70, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "어렵다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "어렵다", "start_ms": 55, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "깨끗하다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "깨끗하다", "start_ms": 80, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "더럽다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "더럽다", "start_ms": 30, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "조용하다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "조용하다", "start_ms": 60, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "시끄럽다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "시끄럽다", "start_ms": 45, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "밝다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "밝다", "start_ms": 55, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "어둡다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "어둡다", "start_ms": 35, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "강하다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "강하다", "start_ms": 70, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "약하다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "약하다", "start_ms": 40, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "칠",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 75,
"audio_type": "speech",
"words": [
{"word": "칠", "start_ms": 75, "dur_ms": 300, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 09:28:57*
