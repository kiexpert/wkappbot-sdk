# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=15840  time=07:30:34  uptime=10304s  mem=451MB  system_processes=409"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_073016.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 750-701-751-175-750-750-750-751-750 765-761-715-750-710-750-705-701-706-750-701-710-750-705-715-705-701-715-701-751 574-173-710-570-571-105-750-701-750-750-750-750-750-750-751-7_V.mp3)
  Segment 2: 1576ms ~ 10720ms (file: 257-547-206-230-263-235-132-236-260-203-102-130-167-213-321-213-260-203-103-321-321-230-271-267-132-102-267-721-273-235-723-237-230-217-726-726-137-103-123-206-260-203-312-321-2_W.mp3)
  Segment 3: 11720ms ~ 20792ms (file: 120-210-120-574-231-271-217-126-702-201-312-260-132-302-130-216-263-670-276-206-206-231-102-627-203-206-752-126-320-210-751-672-123-276-621-754-725-217-127-627-126-627-261-627-1_W.mp3)
  Segment 4: 21792ms ~ 33240ms (file: 301-761-376-206-106-106-316-132-310-326-230-263-263-230-236-263 312-321-201-321-236-237-203-132-103-213-321-301-317-310-136-321-312-137-130-127-230-132-310-320-103-103-301-267-1_W.mp3)
  Segment 5: 34240ms ~ 41404ms (file: 136-136-123-716-103-312-127-230-130-312-260-253-203-103-123-321-761-263-231-231-310-726-720-621-623-623-623-263-132-312-123-756-271-210-321-106-210-765-756-627-267-213-162-715-3_W.mp3)
  Segment 6: 42404ms ~ 42980ms (file: 672-752-572-326-735-362-607-301-671-671-765-765-756-706-751-750-570-571-574-571-157-170-574-756-753-175-375-571-751-571_W.mp3)
  Segment 7: 43980ms ~ 44556ms (file: 327-231-576-572-513-165-750-236-175-256-217-371-613-317-321-276-235-623-206-275-632-315-165-672 312-562-256-231-157-321-576-125-215-513-756-236-261-672-325-276-276-126-267-162-2_V.mp3)
  Segment 8: 45556ms ~ 55492ms (file: 267-267-310-103-173-103-106-567-165-176-176-106-617-167-236-270-621-632-762-237-236-320-267-123-321-310-307-163-310-136-576-301-231-326-130-361-310-132-763-321-316-310-236-236-2_W.mp3)
  Segment 9: 56492ms ~ 57068ms (file: 570-571-570-570-501-570-517-157-751-517-751-357 571-571-571-571-571-571-571-750-507-570-570-570-517-571-570-571-751-571-571-175 751-571-756-571-571-571-571-571-751-751-571-571-7_W.mp3)
  Segment 10: 58068ms ~ 58644ms (file: 265-261 253-312-132-527-576-562-756-756-632-612-652-215-236-263-502-563-652-732-635-765-750-576-574-567-567-567-576-265-275-526-257-526-256-256-326-672-567-576-657-625-576-657-6_W.mp3)
  Segment 11: 59644ms ~ 65944ms (file: 167-160-571-756-276-706-310-175-231-620-316-316-276-312-134-613-326-675-163-102-106-237-631-267-276-617-576-723-627-627-576-130-173-213-310-167-206-263-276-627-627-267-236-326-7_W.mp3)
  Segment 12: 66944ms ~ 76196ms (file: 325-320-327-123-263-263-326-756-103-260-260-361-316-631-163-103-153-105-105-103-106-137-301-320-132-102-132-357-320-310-132-130-321-320-310-132-312-103-675-326-320-320-637-236-2_V.mp3)
  Segment 13: 77196ms ~ 84324ms (file: 102-216-213-123-310-137-170-671-362-312-312-573-317-103-320-310-310-310-162-127-132-571-715-310-321-106-103-163-167-312-103-163-517-230-361-320-321-316-706-206-715-175-312-276-6_W.mp3)
  Segment 14: 85324ms ~ 85900ms (file: 576-132 756-756-706-576-157-321-365-132-103-123-621-736-731-731-376-173-317-371_W.mp3)
  Segment 15: 86900ms ~ 87476ms (file: 765-756-765-765-761-765-765-675-765-675-765-671-617-167-176-176-715-715-715-756-765-765-765-765-675-765-765-765-765-675-765-765-765-765-765-765-765-765-765-765-765-716-175-167-1_W.mp3)
  Segment 16: 88476ms ~ 89052ms (file: 576-751-175-175-175-751-751-715-137 517-516-571-751-170-750-571-173-173-715-715-175-175-175-715-175-157 576-571-571-571-175-571-570-751-571-510-157-750-750-751-517-571-750-750-7_V.mp3)
  Segment 17: 90052ms ~ 90628ms (file: 657-273-572-723-250-267-137-576-562-756-267-570-576-562-527-231-752-267-326-675-276-257-672-362-627-627-765-657-567-620-157-765-756-132-623-317-302-132-625-627-560-532-652-267-7_V.mp3)
  Segment 18: 91628ms ~ 101492ms (file: 103-765-231-213-725-106-136-103-731-703-210-102-176-107-162-123-102-213-267-267-271-267-217-231-102-102-260-276-263-126-130-213-276-276-756-275-210-752-103-213-123-102-715-751-1_W.mp3)
  Segment 19: 102492ms ~ 108792ms (file: 672-173-312-506-105-261-327-750-602-205-120-261-321-627-627-625-547-457-657-675-130-260-726-267-206-263-231-130-623-562-537-573-527-157-517-321-130-753-103-173-735-715-175-327-1_W.mp3)
  Segment 20: 109792ms ~ 118648ms (file: 162-567-625-236-623-627-762-267-231-263-625-256-137-627-263-136-102-231-132-761-106-260-231-123-261-132-261-260-276-310-627-317-150-130-137-130-316-231-762-672-632-236-167-102-1_W.mp3)
  Segment 21: 119648ms ~ 120224ms (file: 735-756-576-576-756 671-637-765-672-213_W.mp3)
  Segment 22: 121224ms ~ 130260ms (file: 263-132-301-301-316-310-310-317-312-276-102-132-126-263-267-261-216-623-670-673-617-102-206-310-310-103-326-276-620-216-173-167-107-136-123-231-306-326-321-320-203-136-267-261-2_W.mp3)
  Segment 23: 131260ms ~ 131836ms (file: 765-756-756-756-576-567-576-573-756-756-765-765-627_W.mp3)
  Segment 24: 132836ms ~ 133412ms (file: 365-316-526-362-317-316-316-317-312-316-136-136-316-316-136-617-136-163-136-317-163-316-136-637-362-136-316-163-163-136-136-367-376-675-673-673-675-675-367 671-670_W.mp3)
  Segment 25: 134412ms ~ 141468ms (file: 726-103-231-627-103-263-613-627-527-120-261-103-623-627-526-321-321-120-160-120-572-627-120-263-123-236-537-136-756-125-263-236-130-762-672-231-263-120-132-231-257-574-123-574-5_W.mp3)
  Segment 26: 142468ms ~ 148840ms (file: 310-756-267-267-276-732-176-267-617-167-176-762-276-627-367-763-267-726-756-715-261-173-754-752-276-762-276-276-275-572-263-267-267-573-276-752-574-756-762-762-267-267-267-371-6_W.mp3)
  Segment 27: 149840ms ~ 158768ms (file: 761-130-103-132-321-136-320-320-517-301-630-621-612-231-102-320-103-130-103-312-102-213-176-176-765-176-175-267-136-105-157-751-132-312-302-362-367-231-216-362-320-210-230-320-1_W.mp3)
  Segment 28: 159768ms ~ 171576ms (file: 675-675-301-672-321-320-326-321-102-132-130-103-621-231-263-203-230-321-210-231-213-103-103-123-310-236-310-102-326-130-267-260-765-267-260-627-267-273-276-275-267-267-267-267-6_W.mp3)
  Segment 29: 172576ms ~ 176608ms (file: 326-236-236-537-561-310-312-165-163-163-123-316-316-132-317-321-231-162-136-165-126-162-163-657-610-217-257-756-357-253-271-765-657-675-657-653-657-653-657-657-657-652-657-571-3_W.mp3)
  Segment 30: 177608ms ~ 178184ms (file: 257-267-273-362-527-231-256-562-326-753-362-257-527-765-726-675-165-617-576-517_V.mp3)
  Segment 31: 179184ms ~ 186708ms (file: 176-103-137-137-176-176-267-236-236-320-312-312-132-130-237-327-267-273-206-260-204-210-751-267-206-230-216-261-216-210-210-204-230-231-230-263-265-263-263-236-206-203-260-260-2_W.mp3)
  Segment 32: 187708ms ~ 194512ms (file: 326-326-260-321-106-762-721-276-275-571-510-167-167-710-130-163-362-573-176-671-617-756-756-276-723-230-732-276-276-726-267-750-276-263-263-623-627-756-627-273-103-103-315-761-6_W.mp3)
  Segment 33: 195512ms ~ 196088ms (file: 170-710-756-750-705-750-705-756-576-576-567-657-567-571-156-137-157-175-761-756-576-516-571-165-135-561-651-162-256-216-215-261-261-607-315-310-135-106-175-167-107-701-705-701-1_V.mp3)
  Segment 34: 197088ms ~ 197664ms (file: 571-570-750-175-517-570-571-507-750-751-571-570-571-570-517 574-571-571-571-571-571-510-751-571-751-570-570-571-571-570-570-570-571 571-570-571-570-571-507-751-573-571-571-571-5_W.mp3)
  Segment 35: 198664ms ~ 199240ms (file: 216 326-321-627-372-316-135-315-130-301-103-620-136-103-310-231-301-312-351-653-517-675-547-561-627-623-576-176-613-316-316-362-361-316-617-765-765-576-657_W.mp3)
  Segment 36: 200240ms ~ 206936ms (file: 102-130-321-321-765-762-276-726-726-762-725-762-756-726-765-752-276-276-726-273-723-176-725-327-572-572-572-256-265-625-572-526-256-265-256-257-257-352-206-213-210-267-572-265-1_W.mp3)
  Segment 37: 207936ms ~ 214164ms (file: 126-764-271-206-601-723-763-267-627-237-623-213-672-610-761-623-260-761-261-263-726-267-623-563-261-731-276-276-310-203-756-127-276-321-316-310-103-267-136-237-753-765-756-267-3_W.mp3)
  Segment 38: 215164ms ~ 215740ms (file: 765-537-635-653-375-675-657-765-675-765-675-675-763-763-673-675-763-675-763-672-635-367-317-326-632-162-216-617-763-675-761-671-672-673-765-675-675-675-675-671-765-763-765-763-6_W.mp3)
  Segment 39: 216740ms ~ 217316ms (file: 123-135-315-357-573-571-573-751-573-573-751-753-571-571-571-572-571-571-571-517-572_W.mp3)
  Segment 40: 218316ms ~ 227064ms (file: 350-106-162-267-276-276-231-167-275-671-326-376-267-627-231-130-726-310-327-376-576-653-256-165-165-601-160-236-713-756-267-236-326-267-674-176-750-574-236-130-513-103-130-132-2_W.mp3)
  Segment 41: 228064ms ~ 228640ms (file: 620-213-263-236-263-632-123-507-657-762-321-672-756-263-263-301-763-705-325-527-216-571-753-657-627-576-216-523-275-506-523-352-762-321-562-267-572-752-627-327-267-263-657-206-5_V.mp3)
  Segment 42: 229640ms ~ 230216ms (file: 527-257-570-315-376-735-136-502-132-523-352-517-567-574-725-752-312-235-572-312-312-267-570-576-620-236-326-631-361-631-610-605-176-631-167-567-617-675-361-136-136-312-312-316-3_W.mp3)
  Segment 43: 231216ms ~ 237516ms (file: 261-320-716-527-756-206-572-321-123-326-231-521-231-765-537-267-107-205-627-263-267-210-321-675-265-705-327-213-230-301-203-265-751-753-130-256-163-301-123-126-362-627-573-765-7_W.mp3)
  Segment 44: 238516ms ~ 243412ms (file: 102-176-176-107-130-103-312-321-312-312-301-203-230-203-210-231-276-327-273-130-130-153-231-321-231-216-312-312-130-312-132-213-132-326-123-612-132-102-312-376-263-321-231-571-3_W.mp3)
  Segment 45: 244412ms ~ 254960ms (file: 617-612-263-623-312-162-213-132-123-260-261-627-267-632-631-175-310-236-326-310-103-623-263-574-263-106-610-627-267-130-213-263-613-136-610-267-231-263-130-261-267-267-265-601-6_W.mp3)
  Segment 46: 255960ms ~ 256536ms (file: 157-571-571-215-752-275-751-571-571-756-517-517-571-153-573-150-170-571-317-713-315-710-173-317-137-137-136-163-617-613-671-367-736-261-637-625-361-617-631-671-367-671-615-573-6_W.mp3)
  Segment 47: 257536ms ~ 258112ms (file: 167-167-651-631-176-651-650-560-567-675-567-657-657-675-657-675-765-756-762-672-673-627-267-126-172-102-103-735-725-725-231-231-762 762-762_W.mp3)
  Segment 48: 259112ms ~ 259688ms (file: 276-706-526-672-750-507-756-512-517-507-317-251-576-670-321-627-206-265-627-576-675-576-756-576-702-617-725-527-507-570-761-531-506-527-132-267-231-273-573-576-567-267-726-125-3_V.mp3)
  Segment 49: 260688ms ~ 261264ms (file: 546-301-312-312-103-136-321 763-132-123-132 673-637-574-572-251-126-263-765-576-756-571-576-756-753-756-613_W.mp3)
  Segment 50: 262264ms ~ 262840ms (file: 536-635-675-567-627-536-576-561 673-162-351-103_W.mp3)
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
> **Time**: 2026-04-06 07:31:35
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답TITLE: Audio Transcription Segment Analysis FILE_TITLE: study_batch_20260406_073016_transcription
[
{
"segment": 1,
"transcript": "이 단계에 있는 게 뜨거운 공기가 위로 올라오면 이 당연히 나오는데 예 그래서 이게 픽스 창이다 보니까 여기에 이제 환기를 할 수 있는 그 추가적인 공사가 필요하다는",
"speaker": "speaker_1",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 10, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "단계에", "start_ms": 110, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 360, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 510, "dur_ms": 66, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "나타나고 있다는 점입니다 리투아니아처럼 과거 대만과 협력을 확대하려 했던 나라들조차 기대했던 성과를 얻지 못했다며 공개적으로 실망을 표출하고 있는 상황입니다 이러한 흐름 속에 대만의 최근 외교 행보는 점점",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "나타나고", "start_ms": 50, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "있다는", "start_ms": 450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "점입니다", "start_ms": 750, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "맛있는 거 다 먹어봐야지 아 그러게 여기까지 왔는데 아 안녕하세요 아 저 아세요 아니요 몰라요 사탕 먹어 좋아요 사탕 좋아해 그렇지 이거는",
"speaker": "speaker_3",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "맛있는", "start_ms": 20, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 420, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "mucho más sosegado que no quiere participar en guerras ilegales 미국의 전통적 핵심 동맹국 영국 역시 마찬가지입니다 전쟁 초기부터",
"speaker": "speaker_4",
"confidence": 0.91,
"language": "es",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "mucho", "start_ms": 30, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "más", "start_ms": 330, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "와 근데 뭔가 얼굴 진짜 태욱 같아 아 귀엽게 그렸네 귀엽긴 한가 봐 제이홉 피부과 치료 좀 받아야 될 것 같아 아 아이고 이렇게 앉아서",
"speaker": "speaker_5",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "와", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 240, "dur_ms": 250, "phase_lr_deg": 0.0}
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
"transcript": "지금 제가 가지고 있는 M5 Pro 맥북 프로랑 M4 Pro 맥미니로 충분히 돌릴 수 있게 물론 제가 갖고 있는 기기들은 램 용량이 굉장히 넉넉합니다",
"speaker": "speaker_6",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "지금", "start_ms": 60, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 360, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "en",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 10,
"transcript": "Let's go Chef so this place is specializing in Galbi-tang so they're gonna blow the world with Galbi-tang Oh let's do it let's go",
"speaker": "speaker_7",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "Let's", "start_ms": 10, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "go", "start_ms": 210, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "그 병원에서는 지금 어떻게 할 수도 없대요 왜냐면은 너무 조산이라서 그 아기가 나오면 만약에 나오게 되면은 인공호흡기 달고 인큐베이터 들어가야 되는데 그",
"speaker": "speaker_8",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "그", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "병원에서는", "start_ms": 240, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "제 나이 스물아홉이었으니까요 첫째 음 그러니까 엄청 어린 나이에 애를 낳은 거예요 음 그러다 보니까 굉장히 건강하게 낳았는데 지금 제 나이가 서른여섯 살이니까 뭐 다 그런 건 아 일반화할 순 없지만 일반적으로",
"speaker": "speaker_9",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "제", "start_ms": 30, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "나이", "start_ms": 230, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "특별한 해법이 될 수 있는 기업이 현대차였다는 겁니다 이게 대단히 중요한 의미가 있습니다 글로벌 생산 체계와 공급망 운영 능력을 갖춰야",
"speaker": "speaker_10",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "특별한", "start_ms": 50, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "해법이", "start_ms": 450, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "유지될 수 있기 때문입니다 피지컬 AI의 중요성이 커진다고 해서 하드웨어 의미가 완전히 사라지는 것은 아니라고 했죠",
"speaker": "speaker_10",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "유지될", "start_ms": 20, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "따라서 가장 현실적인 전략은 하드웨어 경쟁력 플러스 데이터 AI 역량을 동시에",
"speaker": "speaker_10",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "따라서", "start_ms": 30, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "아 어떤 천국반들은 또 모아서 뭐 했대 아 아 이것도 무슨 지스 아 이거 제이홉이에요 어 맞아 맞아 오 마이 갓 제이홉",
"speaker": "speaker_11",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "아", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "천 명 이상의 평화유지군이 주둔하고 있습니다 이들은 이스라엘과 레바논 국경 지역에서 적대 행위를 감시하는 역할을 맡고 있습니다 하지만 전투가 격",
"speaker": "speaker_12",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "천", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "의 권력을 어떻게든 연장해 보려는 지독한 꼼수입니다 자신들의 밥그릇을 지키기 위해 언론이라는 스피커의 하층",
"speaker": "speaker_13",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "의", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "권한이 없는데 특사경의 수사 지휘 권한을 보니까 특사경이 하는 수사를 보니까 특사경이 뭐 무제한적으로 수사하지 않습니다 특사경은 특사경이 수사할 수 있는 것들이 딱 정해져 있습니다 법률로 그 범위가",
"speaker": "speaker_14",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "권한이", "start_ms": 50, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "액션 되고 그거를 그분들한테 맡겨서 그 재밌는 일을 이 표현이 좀 그런데 재밌는 일을 그 돈을 내가 더 줘서 매 그 돈을 가지고 내가 한번 재밌게 만들어 보자 집 디자인도 좀",
"speaker": "speaker_15",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "액션", "start_ms": 40, "dur_ms": 300, "phase_lr_deg": 0.0}
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
"transcript": "아 이거 뭐야 다 4,000원이네 어 엄청 잘해 주시네요 시민들 우리 지금 펭귄 마을 왔는데 가리나 자매가 많이",
"speaker": "speaker_16",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "아", "start_ms": 30, "dur_ms": 200, "phase_lr_deg": 0.0}
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
"transcript": "보인다면 장중에 상승과 하락을 반복하면서 수익 낼 수 있는 기회를 많이 주는 편입니다 그래서 다음 날 대양 광통신 매매 타점을",
"speaker": "speaker_17",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "보인다면", "start_ms": 40, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "공간이 있다라는 게 가장 큰 단점인 것 같아요 군대 있을 때 사둔 땅이라죠 이곳에",
"speaker": "speaker_18",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "공간이", "start_ms": 30, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "분노의 손흥민 모드를 다시 보는 듯한 느낌이었는데요 부진하네 어쩌네 하는 소리가 나오면 진심 모드의 손흥민이 발현되는데",
"speaker": "speaker_19",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "분노의", "start_ms": 40, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "왜 AI가 강한 구조냐 한 번 더 좀 짚어보면은 LPDDR을 많이 박은 상태잖아요 구조적으로 그런 구조가 되는 것이 애플 같은 경우에는 일단 기본적으로 CPU와 GPU를 부수지 않는 구조가 아니죠 애플이 처음에 이 유니파이드 구조를 가져가다 보니까 이",
"speaker": "speaker_20",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "왜", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "인 것인지 이스라엘군 작전 중 발생한 것인지 확인하기 위해 조사 중이라는 입장을 밝혔습니다 또 교전이 치열한 지역에서 발생한",
"speaker": "speaker_21",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "인", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0}
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
"transcript": "사람들은 얘네들 왜 이렇게 사나 이 좁은 집에서 애들 셋에",
"speaker": "speaker_22",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "사람들은", "start_ms": 20, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "배럴당 101.38달러로 1.46% 내려 4거래일 만에 하락했습니다 다만 미국 평균 휘발유 가격은 갤런당 4.018달러로 전쟁 이후 35% 올라 200",
"speaker": "speaker_23",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "배럴당", "start_ms": 40, "dur_ms": 400, "phase_lr_deg": 0.0}
]
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
*Generated by WKAppBot ask gemini — 2026-04-06 07:31:35*
