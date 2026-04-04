# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=25708  time=04:22:42  uptime=11847s  mem=753MB  system_processes=364"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260404_042225.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 123-132-123 312-136-316-163-302-613-623-263-236 267-106-132-102-136-103-132-632-672_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 231-231-256-653-357-760-760-670-617-612 143-750-203-302-312-123-103-135-136-106-103-103-623-570-360-302-312-132-504-507-572-236-236-362-136-132-132-216-267-765-765-765-137 132-6_V.mp3)
  Segment 3: 3152ms ~ 7364ms (file: 621-623-321-361-163-136-756-753-135-130-312-167-316-673-765-736-376-107-673-763-736-762-376-376-362-702-671-176-260-756-765-267-753-713-720-763-321-372-762-762-763-760-706-726-6_W.mp3)
  Segment 4: 8364ms ~ 8940ms (file: 156-627-263-230-312-231-210-123-213-123-120_W.mp3)
  Segment 5: 9940ms ~ 10516ms (file: 632-362-621-362 213-163-326-361-132 326-621-316-361-613 623-263-263-627-261_W.mp3)
  Segment 6: 11516ms ~ 12092ms (file: 132-263-126-103-160-156-561-153-621-627-623-762-210-230-162-627-621-265-612-162-612-162-263-603-106-163-672-265-675-276-321-362-126-623-632-216-631-756-750-615-675-675-671-617-6_W.mp3)
  Segment 7: 13092ms ~ 13668ms (file: 103-102-103-103-103-106-102-105-137-167-150-153-517-751-571-756-576-576-576-576-571-571-527-576-572-547-576-574-576-576-576-576-756-756-756-627-627-627-672-620_W.mp3)
  Segment 8: 14668ms ~ 15244ms (file: 103-231-321 312-123-312-305-103-312-301-312-302-301-135-315-351-310-135-132-132-321-231-302-103-213-321-312-352-235 312-315-301-312-132-135-305-315-305-372-135-103-315-125-135-3_V.mp3)
  Segment 9: 16244ms ~ 16820ms (file: 102-361-103-316-103-127-120-137-632-561-263-153 130-103-567-165-671-103-103-276-102-173-102-126-106-103-671-165-631-670-106-125-103-106-157-613-316-613-136-615-162-260-675-657-6_V.mp3)
  Segment 10: 17820ms ~ 18396ms (file: 367-316-623-362-235-362-326-635-675-376-362-267-265-362-637-637-631-631-617-672-637-637-765-361-317-576-375-375-315-357-175-153-257-275-367-637-276-672-326 654-361-756-756-756-7_W.mp3)
  Segment 11: 19396ms ~ 19972ms (file: 362-237-273-236-623-213-612-612-712-123-627-625-652-652-625-623-326-261-123-761-167-316-216-103-106-106-153-302-752-106-572-756-157-567-526-527-572-756-752-167-621-137-152-132-1_W.mp3)
  Segment 12: 20972ms ~ 21548ms (file: 106-162-612 675-176 276-276-210-273-726-276-273-327-237-327-326-263-231-237-237-327-237-327-203-312-321-321-231-321 763-721-163-167-126-123-152-650-657-657-567-756-570-176-123-2_W.mp3)
  Segment 13: 22548ms ~ 30468ms (file: 172-130-261-213-216-276-627-627-106-107-310-673-672-627-123-216-201-102-201-120-132-103-132-126-136-236-627-162-273-120-107-105-231-213-213-267-106-126-102-213-123-102-231-321-2_W.mp3)
  Segment 14: 31468ms ~ 32044ms (file: 576-675-216-213-362-326-236-213-672-627-267-612 263-623-623-537-165-567-671-671-130-126-137-126-120-102-123-601-631-167-613-610-652-620-160-631-106-621-601-601-612-765-617-576-6_W.mp3)
  Segment 15: 33044ms ~ 39164ms (file: 326-326-671-736-317-723-367-327-312-316-726-723-321-736-627-326-216-571-572-261-627-267-267-576-527-507-307-231-572-162-216-206-574-267-267-267-256-540-265-320-216-627-261-267-6_W.mp3)
  Segment 16: 40164ms ~ 40740ms (file: 123-213-632-751-715-567-576-756-751 312-231-621-613-132-132 263-160-675-362-152-612-567-126-103-163-103-132-167-130-103-231-267-120-267-263-216-132-236-123-236-621-320-517-136-1_W.mp3)
  Segment 17: 41740ms ~ 42316ms (file: 671-561-137-163 130-103-167-103-617-137-574-135-613-130-572-753-561-637-163-316-103-137-627-602-621 574-576-576_V.mp3)
  Segment 18: 43316ms ~ 43892ms (file: 326-213-301-321-126-621-162-106-106-576-672-627 163-156-302-301-316-132-312-236-321-302-126-123-163-132-103-176-136-132-132-521-540-502-504-321-302-130-764-714-103-103-210-132-1_V.mp3)
  Segment 19: 44892ms ~ 52848ms (file: 574-574-301-576-572-762-310-317-753-751-276-765-756-136-574-135-135-310-732-752-720-574-540-312-371-301-237-102-310-513-756-301-756-752-725-163-236-263-275-203-601-162-126-267-6_W.mp3)
  Segment 20: 53848ms ~ 54424ms (file: 321-312-321-132-321-312-103-103-135-132-132-231-163-263-201-265-265-267-750-765-672-267-207-206-270-267-206-236-263-756-576-756-310-301-103-136-316-160-167-105-156-675-162-126-1_V.mp3)
  Segment 21: 55424ms ~ 56000ms (file: 567-561-561-516-567-653-615-671-635-356-567-563-561-567-561-657-167-652-651-531-361-657-567-653-651-657-615-165-167-165-516-165-627-632-615-651-135-165-165-160-120-165-150-150-1_V.mp3)
  Segment 22: 57000ms ~ 57576ms (file: 762-657-765-675-267-765-657-651-375-132-150-756-375-321-365-567-267-672-726-623-672-672-672-263-631-621-627-672-672-765-765-621-213-123-132-126-652-263-362-361-231-312-632-613-1_W.mp3)
  Segment 23: 58576ms ~ 66208ms (file: 213-761-231-205-301-103-261-203-260-270-752-102-103-213-102-132-321-231-123-210-203-120-201-725-320-570-213-213-123-103-613-765-160-165-132-763-310-132-631-652-631-103-312-301-1_W.mp3)
  Segment 24: 67208ms ~ 67784ms (file: 751-167-160-530-167-103-316-301-103-103-675-103-361-136-103-167-312-106-213-162-163-103-106-206-260-620-623-263-167-672-326 162-162-132-163-602-621-621-106-106-176-167-671-617-6_V.mp3)
  Segment 25: 68784ms ~ 69360ms (file: 621-132-514-136-312-135-103-165-351-571-130-106-103-103-136-163-106-130-136-135-156-316-615-316-103-316-163-175-321-136-103-136-103-361-136-316-130-362-163-103-103-130-132-135-1_V.mp3)
  Segment 26: 70360ms ~ 70936ms (file: 132-130-136-157-623-216-163-625-613-621-167-103-163-103-671-651-316-163-103-137-361-167 130-671-136-130-167-103-175-136-126-167-167-617-106-103-601-163-162-612-102-172-163-321-1_V.mp3)
  Segment 27: 71936ms ~ 72512ms (file: 526-256-627-602-613-136-210-132-673-672-312-312-321-312-213-276-602-627-267-627 621-216-612-316-657-362-132-302-261-213-625-216-107-126-167-675-657-653-362-263-236-263-627-267-1_W.mp3)
  Segment 28: 73512ms ~ 74088ms (file: 137 367-163 765-371-312 102-123-123-132-137-126-103-123-103-167-136-516-136-106-163-157-136-126-162-103-156-135-316-153-132-613-163-126-102-102-671-132-132-306-165-615-103-610-6_V.mp3)
  Segment 29: 75088ms ~ 83440ms (file: 132-765-762-756-103-765-576-173-106-751-271-321-321-753-672-267-260-276-726-276-270-752-756-276-267-267-762-627-210-203-126-267-236-326-326-765-107-106-261-263-310-312-326-267-2_W.mp3)
  Segment 30: 84440ms ~ 85016ms (file: 327-261-160-162-257-612-160-765-756-130-136-167-132-213 657-126 612-316-167-163-176-753-670-312-310-162-326-132-132-136-132-657-637-621-632-163-132-361-127-123-126-123-574-570-1_V.mp3)
  Segment 31: 86016ms ~ 86592ms (file: 260-672-673-672-672 167-650-670-132-163 143-765-765-675-675-765-675-765-706-675-765-760-716-163-165-275-723-231-547-234 732-675 357-451-625-165-612-345-326-635-617-617-637-607-6_W.mp3)
  Segment 32: 87592ms ~ 88168ms (file: 103-136-136-671-130-107-103-103-623-136-263-106-135-762-765-213-163-160-163-620 321-312 167 327-507 652-562-136-103-103-167-605-602-136-216_V.mp3)
  Segment 33: 89168ms ~ 89744ms (file: 613 316-623-236-621 321-321-637-673-326 376-675-765-326_W.mp3)
  Segment 34: 90744ms ~ 91320ms (file: 102-132-713-123 132-172-132-137-153 103-132-130-637-261-613-215-136-103-106-623-362-103-106-162-126-102-132_V.mp3)
  Segment 35: 92320ms ~ 92896ms (file: 172-103-567-625-362-132-123 136-315-136-652-103-132-136-103-160-103-103-136-713-361 162-136-103-765-631-163-316-316-103-136-261-173-715-103-136-150-316-103-103-376-136-170-136-1_V.mp3)
  Segment 36: 93896ms ~ 94472ms (file: 765-156-176-217-215-316-136-675-756 576-276-671-263_W.mp3)
  Segment 37: 95472ms ~ 96048ms (file: 162-137-317-127-126-132-126-132-612-621-613-312-136-316-631-163-213-162-760-132 165-312-301-254-312-167-167-136-263-267-267-126-621-261-624-236-236-326-631-134-312-735-326-142-3_V.mp3)
  Segment 38: 97048ms ~ 97624ms (file: 631-617-621 762-576-572-572-572-752-572-756-756-756-765-756-657-673-726_W.mp3)
  Segment 39: 98624ms ~ 99200ms (file: 132-214-735-357-375-536-571-751-736-763-765-756-576-576-675-576-576-576-316-612-176-163-173-671-316-132-175-176-576-167-312-751-135-705-175-715-152-572-375-753-312-357-312-507-6_V.mp3)
  Segment 40: 100200ms ~ 100776ms (file: 573-576-132-162-326-325-567-567 625-317-567-567 312-126-326-163-130-260-567-256-652-623-216-236-263-210-103-216-230-260-625-102-620-620-162 276-576-130-631-130-136-310-125-102-3_W.mp3)
  Segment 41: 101776ms ~ 102352ms (file: 176-176-617-167-312-107-107-736-726-762-765-762-765-762-726-123-206-261-756-756-167-172 765-576-576-574-574-751-216-301-123_W.mp3)
  Segment 42: 103352ms ~ 103928ms (file: 576-750-710-167-672-517-765-672-671-216-176-617-671-612 132-675-675-675-167-657-576-657-756-756-326-316-213-132-231-261-321-132-216 230-137-123-132-103-106-136-123-102-103-123-1_W.mp3)
  Segment 43: 104928ms ~ 105504ms (file: 260-106-162-160-153-156-761-163-103-162-103-136-236-103-106-103-136-167-136-172-136-103-173-136-532-153-156-317-103-136-135-617-136-137-103-106-576-135-102-103-136-361-103-136-1_V.mp3)
  Segment 44: 106504ms ~ 107080ms (file: 167-167-126-132-736-673-162-601-160-517-621-162-126_W.mp3)
  Segment 45: 108080ms ~ 116288ms (file: 260-607-620-620-310-206-527-123-120-123-302-213-657-260-102-607-201-201-756-201-321-130-206-261-123-754-572-206-126-123-132-162-120-203-123-270-103-261-627-217-201-627-102-213-1_W.mp3)
  Segment 46: 117288ms ~ 125064ms (file: 106-162-362-160-120-167-103-165-106-132-103-617-136-512-162-123-125-165_V.mp3)
  Segment 47: 126064ms ~ 128116ms (file: 362-306-760-376-367-736-376-635-367-321-312-320-671-132-103-357-203-103-173-157-573-537-756-153-175-573-576-570-503-537-351-317-571-570-351-137-570-356-350-260-362-312-132-365 5_W.mp3)
  Segment 48: 129116ms ~ 129692ms (file: 237 103-103 162-316-657-567-163-105-157-157-173-137-231-236-321-132-103-136-316-302-670-106-627-627-657-652-602-652-362-236-263-267-321-627-167-167-107-371-207-237-203-720-167-1_V.mp3)
  Segment 49: 130692ms ~ 143256ms (file: 615-567-756-576-756-175-574-173-750-765-637-627-103-267-756-175-103-132-756-372-236-123-125-230-623-312-765-267-106-163-576-507-573-675-730-135-315-103-130-301-132-375-317-137-3_W.mp3)
  Segment 50: 144256ms ~ 144832ms (file: 123-132-103-104-132-103-136-103-136-107-157-305-162-102-132-135-610-130-106-103-103-632-163-106-136-120-136-153-132-103-653-106-132 620-617-126-132-103-132-136-176-137-162-136-1_V.mp3)
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
> **Time**: 2026-04-04 04:23:48
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "하는 사족보행 로봇 요즘 뭐 많이 튀겨서 나오고 있는데 그것을 이제 우리도",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "하는", "start_ms": 10, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "사족보행", "start_ms": 360, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "로봇", "start_ms": 880, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "요즘", "start_ms": 1300, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 1680, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 1830, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "튀겨서", "start_ms": 2180, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "나오고", "start_ms": 2600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "있는데", "start_ms": 3050, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "그것을", "start_ms": 3460, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 3880, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "우리도", "start_ms": 4190, "dur_ms": 390, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "환율을 오틱으로 삼고 이제 모두가 K가 어디인지 안다라는 가사로 한국의 문화적 위상을 정면으로 언급하고",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "환율을", "start_ms": 20, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "오틱으로", "start_ms": 440, "dur_ms": 510, "phase_lr_deg": 0.0},
{"word": "삼고", "start_ms": 950, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 1330, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "모두가", "start_ms": 1640, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "K가", "start_ms": 2050, "dur_ms": 290, "phase_lr_deg": 0.0},
{"word": "어디인지", "start_ms": 2340, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "안다라는", "start_ms": 2790, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "가사로", "start_ms": 3310, "dur_ms": 390, "phase_lr_deg": 0.0},
{"word": "한국의", "start_ms": 3700, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "문화적", "start_ms": 4080, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "위상을", "start_ms": 4490, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "정면으로", "start_ms": 4910, "dur_ms": 510, "phase_lr_deg": 0.0},
{"word": "언급하고", "start_ms": 5420, "dur_ms": 530, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "박현 교수에게 늘 신선한 자극제가 되고 새로운 아이디어를 주었던 오준호 박사의 로봇",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "박현", "start_ms": 30, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "교수에게", "start_ms": 410, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "늘", "start_ms": 960, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "신선한", "start_ms": 1140, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "자극제가", "start_ms": 1620, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "되고", "start_ms": 2170, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "새로운", "start_ms": 2480, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "아이디어를", "start_ms": 2900, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "주었던", "start_ms": 3480, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "오준호", "start_ms": 3890, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "박사의", "start_ms": 4340, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "로봇", "start_ms": 4760, "dur_ms": 410, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "운을 쓰면 안 되고 준비가 될 때까지 실력을 쌓아야만 한다. 근데 이제 너는 미수 썼는데 왜 나는 안 되냐 이러면서 또 화를 내시는 거예요. 근데 이제 너는 5년의 과정이 있었잖아. 너 월천",
"speaker": "speaker_4",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "운을", "start_ms": 40, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "쓰면", "start_ms": 360, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 710, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "되고", "start_ms": 890, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "준비가", "start_ms": 1240, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "될", "start_ms": 1650, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "때까지", "start_ms": 1830, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "실력을", "start_ms": 2280, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "쌓아야만", "start_ms": 2700, "dur_ms": 510, "phase_lr_deg": 0.0},
{"word": "한다", "start_ms": 3210, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 3520, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 3830, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "너는", "start_ms": 4110, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "미수", "start_ms": 4390, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "썼는데", "start_ms": 4700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "왜", "start_ms": 5150, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "나는", "start_ms": 5330, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 5610, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "되냐", "start_ms": 5790, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "이러면서", "start_ms": 6110, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 6560, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "화를", "start_ms": 6740, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "내시는", "start_ms": 7050, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 7470, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 7820, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 8130, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "너는", "start_ms": 8410, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "5년의", "start_ms": 8690, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "과정이", "start_ms": 9070, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "있었잖아", "start_ms": 9490, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "너", "start_ms": 10040, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "월천", "start_ms": 10190, "dur_ms": 320, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "하반기에는 계약이 확정될 것으로 예상된다고 합니다. 그리고 KF-21 사업의 원래 공동 개발국이었던 인도네시아도 자금 문제로 계속 미적거리다가 필리핀이 먼저 생산 물량을 가져갈 수",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "하반기에는", "start_ms": 30, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "계약이", "start_ms": 580, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "확정될", "start_ms": 1000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "것으로", "start_ms": 1450, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "예상된다고", "start_ms": 1830, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "합니다", "start_ms": 2410, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 2730, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "KF-21", "start_ms": 3080, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "사업의", "start_ms": 3630, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "원래", "start_ms": 4010, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "공동", "start_ms": 4320, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "개발국이었던", "start_ms": 4640, "dur_ms": 780, "phase_lr_deg": 0.0},
{"word": "인도네시아도", "start_ms": 5420, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "자금", "start_ms": 6100, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "문제로", "start_ms": 6420, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 6800, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "미적거리다가", "start_ms": 7110, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "필리핀이", "start_ms": 7760, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "먼저", "start_ms": 8240, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "생산", "start_ms": 8560, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "물량을", "start_ms": 8880, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "가져갈", "start_ms": 9300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 9750, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "비중 13.9%까지 급등했습니다. 이 종목들 공통점이 하나 있습니다. 비슷한 차트",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "비중", "start_ms": 50, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "13.9%까지", "start_ms": 370, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "급등했습니다", "start_ms": 1220, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 1900, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "종목들", "start_ms": 2050, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "공통점이", "start_ms": 2470, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "하나", "start_ms": 2990, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 3310, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "비슷한", "start_ms": 3790, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "차트", "start_ms": 4210, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "전해지면서 미국을 포함한 주요 국가들이 적지 않은 충격과 놀라움을 감추지 못하고 있는 상황입니다. 이는 프랑스 정부의 이같은 행보가 단순한 외교적 초청이라고 보기에는 그 의미가 결코",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "전해지면서", "start_ms": 40, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "미국을", "start_ms": 620, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "포함한", "start_ms": 1040, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "주요", "start_ms": 1490, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "국가들이", "start_ms": 1810, "dur_ms": 510, "phase_lr_deg": 0.0},
{"word": "적지", "start_ms": 2320, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "않은", "start_ms": 2640, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "충격과", "start_ms": 2950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "놀라움을", "start_ms": 3400, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "감추지", "start_ms": 3880, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "못하고", "start_ms": 4290, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 4710, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "상황입니다", "start_ms": 4990, "dur_ms": 610, "phase_lr_deg": 0.0},
{"word": "이는", "start_ms": 5600, "dur_ms": 290, "phase_lr_deg": 0.0},
{"word": "프랑스", "start_ms": 5890, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "정부의", "start_ms": 6300, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "이같은", "start_ms": 6680, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "행보가", "start_ms": 7100, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "단순한", "start_ms": 7520, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "외교적", "start_ms": 7930, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "초청이라고", "start_ms": 8350, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "보기에는", "start_ms": 8930, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 9410, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "의미가", "start_ms": 9560, "dur_ms": 390, "phase_lr_deg": 0.0},
{"word": "결코", "start_ms": 9950, "dur_ms": 320, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "한국에서 박사 학위자가 연 2만 명에",
"speaker": "speaker_6",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "한국에서", "start_ms": 30, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "박사", "start_ms": 480, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "학위자가", "start_ms": 790, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "연", "start_ms": 1270, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "2만", "start_ms": 1450, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "명에", "start_ms": 1760, "dur_ms": 310, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "출금하는 거고 또 시장이 안 좋을 때는 비중 낮춰서 하고 이런 식으로 가늘고 길게 가야 되는 거야. 그러다가 또 네가 또 좋아하는 장 되면 자연스레 알아서 돈 벌려. 그때까지 인내하고 버티고 너를 누르고 그러면은 너는 더욱더 슈퍼 범고래가 될 수가 있어. 범고래가 될 때",
"speaker": "speaker_4",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "출금하는", "start_ms": 40, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "거고", "start_ms": 520, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 830, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "시장이", "start_ms": 1010, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 1390, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "좋을", "start_ms": 1570, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "때는", "start_ms": 1880, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "비중", "start_ms": 2160, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "낮춰서", "start_ms": 2470, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "하고", "start_ms": 2890, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 3200, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "식으로", "start_ms": 3480, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "가늘고", "start_ms": 3860, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "길게", "start_ms": 4280, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "가야", "start_ms": 4600, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "되는", "start_ms": 4910, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "거야", "start_ms": 5220, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "그러다가", "start_ms": 5500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 5950, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "네가", "start_ms": 6130, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 6410, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "좋아하는", "start_ms": 6590, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "장", "start_ms": 7070, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "되면", "start_ms": 7250, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "자연스레", "start_ms": 7560, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "알아서", "start_ms": 8040, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "돈", "start_ms": 8420, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "벌려", "start_ms": 8600, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "그때까지", "start_ms": 8910, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "인내하고", "start_ms": 9390, "dur_ms": 510, "phase_lr_deg": 0.0},
{"word": "버티고", "start_ms": 9900, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "너를", "start_ms": 10320, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "누르고", "start_ms": 10630, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "그러면은", "start_ms": 11050, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "너는", "start_ms": 11530, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "더욱더", "start_ms": 11810, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "슈퍼", "start_ms": 12190, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "범고래가", "start_ms": 12510, "dur_ms": 510, "phase_lr_deg": 0.0},
{"word": "될", "start_ms": 13020, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "수가", "start_ms": 13200, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "있어", "start_ms": 13510, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "범고래가", "start_ms": 13790, "dur_ms": 510, "phase_lr_deg": 0.0},
{"word": "될", "start_ms": 14300, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 14480, "dur_ms": 210, "phase_lr_deg": 0.0}
]
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
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-04 04:23:48*
