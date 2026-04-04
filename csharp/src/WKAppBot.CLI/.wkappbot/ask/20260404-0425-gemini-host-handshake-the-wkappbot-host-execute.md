# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=14716  time=04:24:05  uptime=14201s  mem=469MB  system_processes=368"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260404_042334.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 367-673-627-672-367-267-312-362-623-637-362-637-632-623-673-263-627-672-627-637-367-672-672-671-672-267-726-627-672-267-261-267-672-672-621-627-627-672-267-672-761-672-672-672-6_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 756-756-675-675-607-637-136-675-675-675 623-756-367-316-317-576-671-706-756-621-641-670-670-672-762-750-765-137-173-176-163-167-672-176-136-621-137-267-627-762-762-761-675-675-6_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 136-163-654-106-617-213-321-163-163-673-613-601-106-617-160-103-103-106-103-163-106-627-716-625-576-176-621-163-103-163-163-172-167-143-132-124-173-136-617-163-103-106-164-136-1_V.mp3)
  Segment 4: 4728ms ~ 19056ms (file: 130-157-175-153-132-217-126-752-157-570-120-137-132-231-216-103-260-260-102-621-621-260-260-120-216-321-216-210-216-620-103-210-203-260-102-210-201-216-123-207-267-201-263-230-7_W.mp3)
  Segment 5: 20056ms ~ 20632ms (file: 163-612-316-657-316-321-316-613-361-361-136-652-756-576-756-657-675-312-765-672-627-602-306-310-230-321-326-163-162-126-163-546-576-564-253-256 106-106-136-320-230-230-672-260-6_W.mp3)
  Segment 6: 21632ms ~ 29732ms (file: 267-237-267-207-316-603-267-621-623-653-652-625-726-267-256-260-260-360-263-260-265-267-267-620-260-261-263-623-263-263-572-627-627-210-257-126-216-260-261-563-356-367-365-365-5_W.mp3)
  Segment 7: 30732ms ~ 31308ms (file: 132-137-310-130-132-731-317-375-103-137-301-135-712-137-107-712 315-135-103-103-135-137-173-130-103-103-137-103-137-130-310-317-173-103-317-371-301-137-103-315-137-130-137-732 1_V.mp3)
  Segment 8: 32308ms ~ 32884ms (file: 103-102-521-257-572-576-657-765-672-231-231-523-157-231-256-256-230-320-310-312-316-360-352-532-574-537-536 573-576_W.mp3)
  Segment 9: 33884ms ~ 34460ms (file: 531-675-657-617-765-317-213-362-316 360-360-365-365-365-361-635-365-365-365-365-635-365-652-520-352-325-321-205-213-721-725-312-321-732-675_W.mp3)
  Segment 10: 35460ms ~ 36756ms (file: 576-765-756-756-765-321-216-631-231-162-362-132-321-316-310-163-130-103-136-631-613-361-160-106-106-307-735-107-321-326-302-210-721-263-126-261-231-231-126-627-621-162-163-621-2_V.mp3)
  Segment 11: 37756ms ~ 38332ms (file: 316-361-236-256-256-265-265-267-567-576-652-657-265-562-652-365-576-576-576-562-567-256-567-265-512-256-761-132-517-156-526-257-215-265-216-152-165-312-123-361-267-312-132-351-5_W.mp3)
  Segment 12: 39332ms ~ 39908ms (file: 576-756-567-567-567-576-576-576-756-756-576-765-725-762-602-672-173-670-675-576-705-576-167-317-312-312-203-206-260-260-260-320-201-126-613-320-103-231-170-132-103-620-206-320-2_W.mp3)
  Segment 13: 40908ms ~ 41484ms (file: 376-365-276-761-614-267-613-271-132-264-541-315-123-316-163-365-541-261-125-103-102-105-507-537-154-261-627-176-167-127-103-123-126-326-132-176-312-317-361-326-276-136-276-126-2_V.mp3)
  Segment 14: 42484ms ~ 43060ms (file: 312-326-213-236-261-263-216-617-316-132-312 765-716-617-361-627-607-130-102-612-136-162-236-230-610-103-263-120-126-165-610-617-103-123-316-103-103-615-625-621-102-163-163-610-6_V.mp3)
  Segment 15: 44060ms ~ 44636ms (file: 156-657-176-137-176-613-162-761-162-612-763-170-617-136-167-516-623-651-163-613-231-230-236-320-132-123-312-310-136-163-273-170-170-176-510-567-756-651-106-157-170-136-236-632-6_W.mp3)
  Segment 16: 45636ms ~ 46212ms (file: 231-215-257-135-536-653-135-210-325-203-375-320-302-365-627-657-576-576-267-520-572-657-657-675-675-576-756-756-576-756-213-132-305-365-267-670-763-675-326-267-126-103-310-302-5_W.mp3)
  Segment 17: 47212ms ~ 47788ms (file: 721-127-327-165-672-216-231-276-672-673-673-736-130-103-263-632-725-765-671-612-613-102-612-610-261-672-672-672-261-623-657-267-675-765-637-576-627-267-627-625-672-632-361-236-6_W.mp3)
  Segment 18: 48788ms ~ 49364ms (file: 316-623-613-321-612-653-761-613-637-613-362-632-312-136-273-123-627-612-627-615-675-765-167-613-362-326-126-561-627-621-627-675-567-567-265-267-236-267-126-612-652-672-267-623-6_W.mp3)
  Segment 19: 50364ms ~ 50940ms (file: 756-756-126-573-371-105-675-672-507-567-312-135-670-671-756-765_W.mp3)
  Segment 20: 51940ms ~ 52516ms (file: 261-167-671-103-126-102-123-675-176-675-627-267-627 627-265-625-627-267-625-627-675-672-627-675-672-672-675-672-671-167-673-617-675-675-671-167-167-610-675-765-765-765-167-316-2_W.mp3)
  Segment 21: 53516ms ~ 54092ms (file: 671-106-167-510-765-120 165-671-106-102-106-673-165-137-106-163-765-632-617-602-162-547-213-617-623-263-567-726-261-176-671-106-103-136-103-106-762-167_V.mp3)
  Segment 22: 55092ms ~ 55668ms (file: 762-136-136-607 160-617-175-175-175-163-630-175-576-632-571-513-526-516-513-152-527-527-526-527-106-126-312-261-237-627_W.mp3)
  Segment 23: 56668ms ~ 61996ms (file: 123-213-217-213-217-127-127-123-216-231-321-213-123-127-123-213-123-123-132-123-120-321-273-231-231-231-312-130-123-217-231-231-123-217-123-123-213-123-132-123-213-210-123-132-1_W_stt_if.mp3)
  Segment 24: 62996ms ~ 63572ms (file: 625-621-261-267-261-261-627-261-267-267-261-261-620-620-620-260-620-625-620-625-263-625-265-620-265-621-627-625-625-627-621-627-267-267-627-267-627-261-261-267-621-216-261-621-2_W.mp3)
  Segment 25: 64572ms ~ 65148ms (file: 167-672-671-627-236-706-762-267-206-613-267-106-167-106-106-162-751-175-765-706-756-765-203-260-206-602-620-361-136-310-610-162-126-301-132-510-507-235-325-532-267-103-150-153-3_W.mp3)
  Segment 26: 66148ms ~ 72160ms (file: 573-301-310-315-312-310-301-753-675-310-105-502-310-673-632-362-326-157-265-562-265-657-657-275-572-756-675-675-230-627-752-756-756-576-107-762-236-235-127-106-123-273-765-176-2_W.mp3)
  Segment 27: 73160ms ~ 73736ms (file: 216-756-576-631-657-657-602-312-657-576-574-756-157-136-316-361-361-316-316-613-367-357-316-316-361-316-361-361-367-316-361-361-361-361-136-713-763-716-375-375-732-317-375-317-7_W.mp3)
  Segment 28: 74736ms ~ 75312ms (file: 132-631-627-371-321-671 312 126 312 132-127 372-123 316-765-756-765-765-657-675-756-576-102-612-657-675 261_V.mp3)
  Segment 29: 76312ms ~ 76888ms (file: 162-132-126-765-760-127-302 213-623-136-126 126-263-276-762_W.mp3)
  Segment 30: 77888ms ~ 78464ms (file: 675-762-176-736-571-675-756-567-675-275-612-132-163-136-132-163-127-120-123-107-137-263-267-276-213-267-261-267-261-216-216-210-216-231-261-264_W.mp3)
  Segment 31: 79464ms ~ 80040ms (file: 132-132-261-726-271-362-570-165-327-673-136-612-326-627-267-206-263-267-263-267-236-263-267-260-623-625-675-672-136-136-675-603-306-306-376-370-672-672-637-236-236-326-261-206-2_W.mp3)
  Segment 32: 81040ms ~ 81616ms (file: 657-576-765-576-756-765-756-765-672-675-675-672-267 672-756-765-675-765-765-657-675-765-765-675-756-675-672-726-276-652-672-675-567-562-362-765-756-652-672-675-765-675-675-675-6_W.mp3)
  Segment 33: 82616ms ~ 83192ms (file: 623-326-106-103-136-103-103-103-103-163-321-326-763-723-376-673-321-302-321-312-320-310-603-637-673-607-602-627-670-627-632-361-130-726-361-301-162-126-162-106-203-320-132-312-3_W.mp3)
  Segment 34: 84192ms ~ 84768ms (file: 657-765-573-756-756-516-576-574-372-167-675-623-657-617-756-376-362-326-132-263 675-657-675-765-312 657-672-627-316-321-623-265-627-675-612-613-316-136-670-167-126-160-275-132-6_W.mp3)
  Segment 35: 85768ms ~ 86344ms (file: 132-310-301-312-102-132-123 123-156-167 627-620-753-753-120-167-103-103 670-276-321-352-237-632_V.mp3)
  Segment 36: 87344ms ~ 87920ms (file: 621-276-627-672-267-216-612-127-163-160-306-103-615-163-107-162-136-103-154-516-167-162-165-106 102-132-675-106-165-136-136-162-167-167-136-152-103-516-675-167-136-167-671-671-6_V.mp3)
  Segment 37: 88920ms ~ 89496ms (file: 675-723-627-265-265-625-765-267-365-652-625-623-623-236-623-652-627-623-673-236-263-276-612-267-265-263-265-762-765-675-625-625-672-256-256-526-567-657-625-567-567-625-627-657-5_W.mp3)
  Segment 38: 90496ms ~ 91072ms (file: 213-136-123-123 501-632-152-175-263-267 107-103-103-317-103-136-516-516-136-132-601-163-312-673-675-612-612-673-103-103-136-173-132-130-163-162-763-263 517-513 157_V.mp3)
  Segment 39: 92072ms ~ 92648ms (file: 576-106-136-106-516-135-106-103-167-130 576-106-310-571-176-315-132_V.mp3)
  Segment 40: 93648ms ~ 98976ms (file: 103-123-236-213-372-672-267-675-671-261-627-261-261-267-376-310-130-317-612-316-720-705-730-310-321-670-673-267-621-321-765-576-126-620-705-750-312-750-675-750-132-105-137-260-2_W.mp3)
  Segment 41: 99976ms ~ 100552ms (file: 276-217-571-572-752-762-762-672-362-302-103-310-132-103-213-106-306-326-163-312-301-567-537-625-267-236_W.mp3)
  Segment 42: 101552ms ~ 102128ms (file: 751-236-136-310-103-136-103-136-103-132-103-567-236-165 130-103-136-136-132-132-617-162-136-103 102-561-156_V.mp3)
  Segment 43: 103128ms ~ 103704ms (file: 576-576-576-576-576-576-657-576-756-576-765-675-675-576-657-657-675-657-567-567-576 576-576-576-576-576-576-756-576-567-567-567-657-657-765-567-765-576-567-576-675-567-756-756 5_W.mp3)
  Segment 44: 104704ms ~ 105280ms (file: 617-617-106-106-103-163-103-160-601-621-206-302-754-675-765-607-607-620-230-320-326-623-613-162-261-216-705-260-520-260-203-236-263-207-206-207-230-670-607-620-601-126-106-126-6_V.mp3)
  Segment 45: 106280ms ~ 106856ms (file: 230-703-732-627-203-326-520-126-123 625-162-517-103-123-132-123-102-316-301-236-263-203-231-321-132-152-231-263-231-321-675-675-672-607-540-543-237-257-125-216-763-127-157-231-2_V.mp3)
  Segment 46: 107856ms ~ 108432ms (file: 265-625-652-265-625-562 106-123-236-625-625-627-126-276-267-126-672-657-672-672-675-762-762 756-762-765-276-752-576-756-762-657-657-526-567_W.mp3)
  Segment 47: 109432ms ~ 110008ms (file: 132-162-673-163-213-265-675-163-312-167-103-103-132-617-617-163-623-612-602-163-612-132-162-316-312-167-103-670-163-127-613-163-613-562-163-136-132-103-316-615-162-167-617-613-1_V.mp3)
  Segment 48: 111008ms ~ 111584ms (file: 153-175 103-132-632-132-102-126-216-261-261-201-310-301-103-130-103-136-103-261-623-136-361-167-136-132-317-612-603-617-135-136-310-310-106-301-312-103-103-136-126-136-123_V.mp3)
  Segment 49: 112584ms ~ 113160ms (file: 230-137-130-301-301-325-206-520-306-263-325-365-362-653-163-302-302-501-350-325-537-531-120-213-236-302-261-753-570-570-507-507-573-570-756-756-574-576-365-326-756-765-376-173-7_W.mp3)
  Segment 50: 114160ms ~ 114736ms (file: 230-302-143-164-763-206-206-206-612-603-106-106-321-201-215-542-534-350-103-106-167-163-162-570-305-310-132-310-301-310-132-320-320-203-302-103-107-715-301-260-260-263-312-316-3_V.mp3)
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
> **Time**: 2026-04-04 04:25:11
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
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "안녕", "start_ms": 40, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "하세요", "start_ms": 260, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "반갑습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "반갑", "start_ms": 30, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "습니다", "start_ms": 270, "dur_ms": 310, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "제 이름은",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "제", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이름은", "start_ms": 210, "dur_ms": 330, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "한국어를 배우고 있는 학생입니다 만나서 정말 반갑습니다 앞으로 잘 부탁드립니다",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "한국어를", "start_ms": 60, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "배우고", "start_ms": 720, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 1150, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "학생입니다", "start_ms": 1440, "dur_ms": 780, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "어디 사세요",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "어디", "start_ms": 45, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "사세요", "start_ms": 290, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "저는 지금 서울에 살고 있습니다 서울은 정말 복잡하지만 재미있는 도시인 것 같아요",
"speaker": "speaker_1",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 70, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 390, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "서울에", "start_ms": 740, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "취미가 뭐예요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "취미가", "start_ms": 25, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "뭐예요", "start_ms": 310, "dur_ms": 260, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "음악 듣기요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "음악", "start_ms": 35, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "듣기요", "start_ms": 260, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "어떤 음악 좋아해요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "어떤", "start_ms": 40, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "음악", "start_ms": 260, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "좋아해요", "start_ms": 450, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "저는 케이팝을 주로 듣습니다",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 55, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "케이팝을", "start_ms": 360, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "주로", "start_ms": 890, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "듣습니다", "start_ms": 1120, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "가장 좋아하는 가수",
"speaker": "speaker_1",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "가장", "start_ms": 40, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "좋아하는", "start_ms": 260, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "가수", "start_ms": 510, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "비티에스 좋아해요",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "비티에스", "start_ms": 30, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "좋아해요", "start_ms": 390, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "영어도 할 줄 아세요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "영어도", "start_ms": 45, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 260, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "줄", "start_ms": 350, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "아세요", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "조금 할 수 있어요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "조금", "start_ms": 35, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 240, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 340, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "I can speak a bit",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "can", "start_ms": 140, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "speak", "start_ms": 260, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 430, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "bit", "start_ms": 480, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "일본어는 어때요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "일본어는", "start_ms": 40, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "어때요", "start_ms": 360, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "일본어도 공부하고 있어요",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "일본어도", "start_ms": 35, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "공부하고", "start_ms": 320, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 510, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "日本語が上手ですね",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ja",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "日本語が", "start_ms": 45, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "上手ですね", "start_ms": 290, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "아니에요 아직 멀었어요",
"speaker": "speaker_1",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "아니에요", "start_ms": 50, "dur_ms": 260, "phase_lr_deg": 0.0},
{"word": "아직", "start_ms": 320, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "멀었어요", "start_ms": 450, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "정말 잘하시는데요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 40, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "잘하시는데요", "start_ms": 290, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "감사합니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "감사", "start_ms": 20, "dur_ms": 260, "phase_lr_deg": 0.0},
{"word": "합니다", "start_ms": 290, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "오늘 날씨 참 좋죠",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 30, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "날씨", "start_ms": 220, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "참", "start_ms": 390, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "좋죠", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "네 나들이 가기 딱 좋은 날씨인 것 같아요 이런 날에는 공원에 가서 산책하면 참 좋겠어요",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 80, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "나들이", "start_ms": 240, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "가기", "start_ms": 600, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "맞아요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "맞아요", "start_ms": 35, "dur_ms": 540, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "커피 한 잔 어때요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "커피", "start_ms": 45, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 230, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "잔", "start_ms": 320, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "어때요", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "좋아요 근처에 맛있는 카페가 있는데 그리로 갈까요 제가 살게요",
"speaker": "speaker_1",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "좋아요", "start_ms": 65, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "근처에", "start_ms": 400, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "아니에요 제가 살게요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "아니에요", "start_ms": 40, "dur_ms": 260, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 310, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "살게요", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "괜찮아요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "괜찮아요", "start_ms": 25, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "고마워요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "고마워요", "start_ms": 20, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "별말씀을요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "별말씀을요", "start_ms": 35, "dur_ms": 540, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "다음에 또 봐요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 30, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 280, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "봐요", "start_ms": 400, "dur_ms": 170, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 33,
"transcript": "전화번호가 뭐예요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "전화번호가", "start_ms": 40, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "뭐예요", "start_ms": 370, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "나중에 연락할게요",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "나중에", "start_ms": 50, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "연락할게요", "start_ms": 340, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "잘 가요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "잘", "start_ms": 25, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "가요", "start_ms": 210, "dur_ms": 360, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "안녕히 계세요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 35, "dur_ms": 260, "phase_lr_deg": 0.0},
{"word": "계세요", "start_ms": 310, "dur_ms": 260, "phase_lr_deg": 0.0}
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
"transcript": "지금 몇 시예요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "지금", "start_ms": 40, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "몇", "start_ms": 260, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "시예요", "start_ms": 380, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "두 시 반입니다",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "두", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "시", "start_ms": 160, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "반입니다", "start_ms": 260, "dur_ms": 310, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "벌써 시간이 그렇게 됐나요 점심 먹은 지 얼마 안 된 것 같은데 시간이 참 빠르네요",
"speaker": "speaker_1",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "벌써", "start_ms": 90, "dur_ms": 260, "phase_lr_deg": 0.0},
{"word": "시간이", "start_ms": 360, "dur_ms": 310, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "배고프지 않으세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "배고프지", "start_ms": 30, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "않으세요", "start_ms": 320, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "조금 배고프네요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "조금", "start_ms": 40, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "배고프네요", "start_ms": 260, "dur_ms": 320, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 44,
"transcript": "뭐 좀 먹을까요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "뭐", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 160, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "먹을까요", "start_ms": 280, "dur_ms": 290, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "샌드위치 어때요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "샌드위치", "start_ms": 40, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "어때요", "start_ms": 360, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "그거 좋겠네요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "그거", "start_ms": 30, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "좋겠네요", "start_ms": 250, "dur_ms": 320, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "여기 샌드위치가 맛있대요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "여기", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "샌드위치가", "start_ms": 240, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "맛있대요", "start_ms": 600, "dur_ms": 320, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "정말요 기대되네요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "정말요", "start_ms": 40, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "기대되네요", "start_ms": 260, "dur_ms": 330, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "맛있게 드세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "맛있게", "start_ms": 25, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "드세요", "start_ms": 280, "dur_ms": 310, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "잘 먹겠습니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "잘", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "먹겠습니다", "start_ms": 180, "dur_ms": 400, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-04 04:25:11*
