# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=29060  time=07:21:00  uptime=881s  mem=1050MB  system_processes=413"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_072018.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 11808ms (file: 261-132-176-167-130-562-576-572-103-571-512-321-756-273-203-754-276-326-257-276-712-157-137-310-321-572-130-756-756-726-756-576-276-576-756-231-236-326 675_W.mp3)
  Segment 2: 12808ms ~ 13384ms (file: 361-573-573-572-375-175-671-753-576-571-576-567-671-167-765-763-765-761-761-736-671-761-736-765-765-673-765-673-671-367-176-617-675-675-762-671-673-675-673-765-637-671-763-673-6_W.mp3)
  Segment 3: 14384ms ~ 14960ms (file: 367-367-316-316-317-731-175-753-715-756-765-750-756-753_W.mp3)
  Segment 4: 15960ms ~ 24060ms (file: 213-162-130-627-260-267-237-756-276-267-327-123-126-106-170-172-162-165-147-671-132-761-123-120-102-261-263-120-123-230-312-321-735-237-103-162-163-263-263-263-320-130-132-137-3_W.mp3)
  Segment 5: 25060ms ~ 25636ms (file: 257-317-261-231-267-672-306-765-516-361-675-613-357-503-567-576-537-750-152-756 570-567-574-765-756-765-376-376-713-716-761-736-713-741-413-745-754-751-753-756-754-756-673-163-1_V.mp3)
  Segment 6: 26636ms ~ 33188ms (file: 176-371-130-237-312-173-237-367-312-321-371-751-157-627-231-372-123-372-317-273-276-172-756-371-726-276-376-267-362-312-723-725-317-726-752-751-725-762-216-312-123-126-127-172-1_W.mp3)
  Segment 7: 34188ms ~ 42900ms (file: 230-203-236-321-130-102-107-201-120-261-123-310-123-120-102-267-102-207-207-527-216-102-213-210-571-120-132-103-120-102-102-710-170-123-213-275-275-267-127-320-213-267-201-201-2_W.mp3)
  Segment 8: 43900ms ~ 44476ms (file: 726-672-657-567-165-617 675-672-613-163-163-163-126-263-756-756-356_W.mp3)
  Segment 9: 45476ms ~ 52676ms (file: 103-157-105-310-106-136-236-752-137-617-106-106-361-173-130-326-326-735-106-167-176-106-167-267-267-263-562-765-672-671-716-715-130-310-237-752-107-362-276-327-321-173-716-167-2_W.mp3)
  Segment 10: 53676ms ~ 54252ms (file: 263-257-715-275-627-257-256-367-273-752-375-752-756-652-137-135-157-173-103 670-132-537-632-265-657-675-327-627-237 675-236-256-236-236-623-273-162-276-267 367-207-267-273-213-2_V.mp3)
  Segment 11: 55252ms ~ 55828ms (file: 750-752-751-673-167-671-321-376-613-137-130-613-675-761-724-761-103-572-512-701-627-672-513-731-105-765-625-726-267-627-136-751-273-725-725-237-312-312-312-135-317-152-675-135-7_W.mp3)
  Segment 12: 56828ms ~ 57404ms (file: 765-137-361-316-316-136-316-136-316-136-137-136-163-167-136-316-765-765-763-763-673-726-637-763-762-736-731-713-672_W.mp3)
  Segment 13: 58404ms ~ 66036ms (file: 132-123-143-675-627-627-267-126-132-172-231-321-672-267-627-263-261-213-231-106-167-176-762-267-273-312-236-120-123-372-302-106-310-732-120-263-103-106-167-765-267-103-627-103-1_W.mp3)
  Segment 14: 67036ms ~ 78088ms (file: 312-735-103-756-765-157-123-675-235-130-527-132-130-203-750-257-657-576-175-536-573-754-107-123-103-132-230-136-236-632-321-260-231-173-132-267-312-756-107-263-752-756-756-260-1_W.mp3)
  Segment 15: 79088ms ~ 79664ms (file: 765-275-267-526-537-276-327-237-537-752-326-756-237-731-257-573-723-725-637-317-371-236-321-675-623 617-165-163-167-167-756-751_V.mp3)
  Segment 16: 80664ms ~ 93732ms (file: 275-325-527-572-571-752-275-261-217-312-172-173-107-752-275-236-107-261-267-267-572-257-104-210-213-752-253-125-524-527-107-361-627-612-123-527-572-320-357-547-357-301-130-310-1_W.mp3)
  Segment 17: 94732ms ~ 105028ms (file: 602-675-176-630-167-106-167-316-103-267-217-320-107-137-206-203-627-371-260-203-267-216-362-136-103-107-310-120-173-176-756-321-301-230-310-132-103-137-176-173-132-713-260-160-1_V.mp3)
  Segment 18: 106028ms ~ 106604ms (file: 173-175-571-751-175-715-571-576-156-165-106-150-567-125-261-216-217-261-256-162-625-567-351-315-361-136-156-175-715-567-517-152-105-105-125-105-102-735-302-126-160-160-170-173-1_V.mp3)
  Segment 19: 107604ms ~ 108180ms (file: 150-356-657-563-362-362-574-235-302-630-310-103-105-315-123-132-653-312-312-521-632-126-652-326-231-316-312-265-236-312-130-130-130-130-136-316-130-132-625-517-157-154-136-136-1_V.mp3)
  Segment 20: 109180ms ~ 116056ms (file: 132-627-716-756-352-572-206-720-206-276-617-316-276-761-127-506-561-260-205-567-672-671-267-236-267-251-267-765-765-506-602-321-321-210-207-236-173-371-672-706-572-260-263-201-5_W.mp3)
  Segment 21: 117056ms ~ 117632ms (file: 753-765-576-761-576-567-617-176-267-735 172-560-631-316-715-370-637-163-725-716-623-573-357-725-256-231-527-753-175-726-567-752-675-376-236-526-376-135-574-265-570-526-726-235-2_V.mp3)
  Segment 22: 118632ms ~ 129144ms (file: 756-210-201-712-276-715-276-765-172-754-210-301-310-201-623-132-103-675-675-675-675-765-756-765-750-170-157-236-275-217-207-120-213-231-132-231-103-103-153-103-321-370-315-725-1_W.mp3)
  Segment 23: 130144ms ~ 135184ms (file: 231-102-571-527-517-103-627-261-136-217-213-231-317-120-312-672-671-576-657-267-126-267-762-176-627-267-672-316-765-236-321-236-265-123-267-263-126-561-236-237-120-162-675-735-7_W.mp3)
  Segment 24: 136184ms ~ 143348ms (file: 105-310-213-321-103-310-320-327-310-130-620-261-312-231-123-230-320-230-321-132-413-132-213-136-102-213-267-230-230-236-213-120-761-371-567-315-573-315-317-137-321-361-315-316-3_W.mp3)
  Segment 25: 144348ms ~ 151692ms (file: 123-132-230-137-132-103-143-726-103-123-216-672-574-130-103-103-136-210-752-752-205-102-327-725-327-321-106-102-276-276-267-267-321-267-261-276-123-753-137-321-275-267-276-276-7_W.mp3)
  Segment 26: 152692ms ~ 161044ms (file: 250-257-123-320-206-162-310-201-231-231-103-120-312-230-752-756-103-136-756-102-765-312-132-130-752-275-301-756-765-675-261-132-130-102-756-713-130-103-312-120-102-301-230-132-2_W.mp3)
  Segment 27: 162044ms ~ 171440ms (file: 261-267-132-765-623-132-132-135-517-105-317-210-752-753-371-130-103-132-612-671-653-362-263-230-752-756-752-260-547-573-751-256-260-756-103-267-260-672-265-265-321-735-215-265-7_W.mp3)
  Segment 28: 172440ms ~ 173016ms (file: 576-576-576-571-576-657-567-365-536-256-576-756-574-576-756-657-576-157-657-576-571-756-574-547-574-751-301-137-157-160_W.mp3)
  Segment 29: 174016ms ~ 179056ms (file: 320-327-103-327-752-235-260-627-627-206-302-213-165-627-302-276-207-106-267-132-562-317-753-106-612-176-312-726-263-123-127-123-631-162-162-127-725-172-273-237-571-106-106-215-2_W.mp3)
  Segment 30: 180056ms ~ 186788ms (file: 236-213-356-205-206-102-517-762-627-265-265-276-260-276-321-753-765-201-126-123-302-362-612-201-120-235-103-132-312-126-123-321-236-120-231-103-132-321-236-231-201-267-260-312-2_W.mp3)
  Segment 31: 187788ms ~ 196824ms (file: 206-320-260-752-725-574-753-765-231-531-231-260-267-107-175-751-753-235-231-275-167-103-130-123-127-275-103-132-107-756-170-103-132-130-130-137-576-175-132-132-163-123-756-276-7_W.mp3)
  Segment 32: 197824ms ~ 206176ms (file: 273-275-130-675-765-675-276-372-723-236-273-273-725-275-273-275-201-721-130-312-725-102-201-231-231-721-127-126-126-126-175-126-102-135-275-276-213-267-275-725-312-263-106-130-3_W.mp3)
  Segment 33: 207176ms ~ 207752ms (file: 157-157-571-137-135-137-317-135-751-517-571-751-135-571-157-157-157-157-157-175-517-516-157-517-157-157-517-517-157-715-573-175-715-753-517-573-517-105-103-157-175-175-157-157-5_V.mp3)
  Segment 34: 208752ms ~ 214476ms (file: 726-657-620-261-572-276-163-267-162-267-361-671-610-610-267-672-762-216-126-573-307-236-513-173-106-132-571-576-275-267-276-106-312-627-106-276-237-671-126-765-267-572-263-361-2_W.mp3)
  Segment 35: 215476ms ~ 220840ms (file: 276-206-672-167-310-627-216-312-267-317-130-312-765-267-267-210-276-756-765-576-756-216-163-167-732-137-301-710-526-236-167-607-267-207-765-635-263-123-356-560-153-573-567-132-2_W.mp3)
  Segment 36: 221840ms ~ 228932ms (file: 123-312-123-102-612-627-267-263-236-623-162-376-263-260-203-260-203-130-102-132-275-130-263-756-210-237-132-312-130-231-130-632-102-132-312-623-267-657-126-103-123-130-175-316-1_W.mp3)
  Segment 37: 229932ms ~ 238644ms (file: 213-276-137-316-376-327-257-352-103-167-102-132-726-107-756-532-237-275-172-657-102-675-567-576-157-513-132-413-732-237-321-321-320-237-130-721-271-672-172-713-625-103-137-321-2_W.mp3)
  Segment 38: 239644ms ~ 240220ms (file: 716-137-315-123-312-127-321-513-137-156_V.mp3)
  Segment 39: 241220ms ~ 241796ms (file: 625-265-726-321-251-760-576-627-317-235-526-326-275-203-231-263-236-320-261-627-506-625-357-352-632-276-527-502-362-267-570-735-632-503-312-253-327-576-572-132-275-273-705-623-2_V.mp3)
  Segment 40: 242796ms ~ 243372ms (file: 761-761-765-176-761-176-176-160-170-170-107-170-107-176 157-107-170-107-170-710-170-715-175-175-715-716-715-176-761-716-176-176-176-170-176-176-761 576-175-675-175-107-715-176-1_V.mp3)
  Segment 41: 244372ms ~ 244948ms (file: 175-175-175-157-157-157-175-157-576-751-751-175-176-715-176-176-167-175 130-173-713-175-157-517-517-175-176-175-157-517-715-751-571-175-756-716-175-175-576-715 130-137-317-175-7_W.mp3)
  Segment 42: 245948ms ~ 253004ms (file: 175-751-107-517-175-217-107-175-726-137-103-321-271-351-167-160-375-275-207-275-230-213-127-213-326-236-203-235-231-256-705-135-370-307-325-350-720-107-213-276-137-170-107-201-7_V.mp3)
  Segment 43: 254004ms ~ 261816ms (file: 267-237-173-123-321-130-230-213-231-120-132-123-756-765-201-103-137-321-130-132-263-267-231-267-627-103-360-132-103-231-132-321-230-273-321-632-163-163-312-132-312-310-321-312-3_W.mp3)
  Segment 44: 262816ms ~ 268504ms (file: 267-267-627-260-236-236-236-263-627-367-367-267-326-326-375-356-327-312-132-321-263-672-372-376-673-267-726-756-267-263-267-267-267-253-236-267-756-725-756-170-672-267-762-752-6_W.mp3)
  Segment 45: 269504ms ~ 275408ms (file: 102-574-107-103-107-203-260-572-123-236-235-106-123-235-620-231-106-213-321-765-713-106-260-276-320-273-162-627-621-236-721-207-506-572-256-275-260-574-657-103-126-721-257-236-3_V.mp3)
  Segment 46: 276408ms ~ 276984ms (file: 627-615-561-625-652 547-502-673-312-317-137-671-673-675-675-675-657-675-675-675-361-621-637-671-613-632-162-136_W.mp3)
  Segment 47: 277984ms ~ 278560ms (file: 321-570-573 756-726-536-273-617-150-627-751-750-326-754-716-213-265-206-527-537-213-750-271-261-756-267-527-326-132-675-126-621-570-621-236-371-157-516-675-673-765-657-675-576-6_W.mp3)
  Segment 48: 279560ms ~ 288272ms (file: 210-310-130-207-210-201-120-126-602-621-203-750-127-210-271-756-312-267-657-765-763-672-760-261-126-620-260-216-753-120-574-652-102-230-670-130-206-120-201-267-132-201-627-267-2_W.mp3)
  Segment 49: 289272ms ~ 298020ms (file: 107-123-210-301-571-152-752-257-130-213-157-153-176-257-370-312-132-130-270-123-316-310-132-102-103-206-167-176-176-107-231-162-106-130-217-270-230-230-260-207-103-102-163-675-3_W.mp3)
  Segment 50: 299020ms ~ 307984ms (file: 574-675-671-672-102-620-312-301-102-201-102-267-574-317-612-216-213-753-765-120-126-123-260-206-302-103-756-574-267-127-260-206-756-762-102-103-627-216-574-675-126-270-206-627-1_W.mp3)
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
> **Time**: 2026-04-06 07:22:28
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "으로 핫한 슈퍼스타가 될 것이라는 데 제가 가장 아끼고 사랑하는 30년 지기 친구의 소중한 왼팔을 걸겠습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "으로", "start_ms": 20, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "핫한", "start_ms": 250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "슈퍼스타가", "start_ms": 650, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "될", "start_ms": 1300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "것이라는", "start_ms": 1500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "데", "start_ms": 2000, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 2200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 2550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "아끼고", "start_ms": 2950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "사랑하는", "start_ms": 3400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "30년", "start_ms": 3950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "지기", "start_ms": 4450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "친구의", "start_ms": 4800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "소중한", "start_ms": 5250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "왼팔을", "start_ms": 5750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "걸겠습니다", "start_ms": 6250, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "로봇을 만든다는 개념은 미국에서 하고 실제적으로 상업적으로 각 공정별로 필요한 로봇은 쉽게 만들 수 있는 특화된 로봇으로 현재 개발 중이라는 겁니다 이 두 가지 트랙",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "로봇을", "start_ms": 40, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "만든다는", "start_ms": 450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "개념은", "start_ms": 1000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "미국에서", "start_ms": 1500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "하고", "start_ms": 2050, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "실제적으로", "start_ms": 2400, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "상업적으로", "start_ms": 3100, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "각", "start_ms": 3800, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "공정별로", "start_ms": 4000, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "필요한", "start_ms": 4600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "로봇은", "start_ms": 5050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "쉽게", "start_ms": 5550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "만들", "start_ms": 5900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 6250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 6450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "특화된", "start_ms": 6800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "로봇으로", "start_ms": 7350, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "현재", "start_ms": 7950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "개발", "start_ms": 8350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "중이라는", "start_ms": 8750, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "겁니다", "start_ms": 9350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 9850, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "두", "start_ms": 10050, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "가지", "start_ms": 10300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "트랙", "start_ms": 10650, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "해협으로의 군함 파견은 물론 나아가 전쟁 비용 분담 같은 노골적인 요구로 이어질 가능성이 있습니다 트럼프는 줄곧 엉뚱",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "해협으로의", "start_ms": 30, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "군함", "start_ms": 650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "파견은", "start_ms": 1050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "물론", "start_ms": 1550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "나아가", "start_ms": 1950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "전쟁", "start_ms": 2450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "비용", "start_ms": 2850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "분담", "start_ms": 3250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "같은", "start_ms": 3650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "노골적인", "start_ms": 4050, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "요구로", "start_ms": 4650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이어질", "start_ms": 5100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "가능성이", "start_ms": 5600, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 6200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "트럼프는", "start_ms": 6750, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "줄곧", "start_ms": 7350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "엉뚱", "start_ms": 7750, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "집을 하자 그래서 아예 안 하고 온돌을 안 깐 덕분에 에폭시가 덜 뜨지 않고 반질반질하긴 한데 어우 겨울에",
"speaker": "speaker_4",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "집을", "start_ms": 50, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "하자", "start_ms": 400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "아예", "start_ms": 1300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 1650, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "하고", "start_ms": 1850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "온돌을", "start_ms": 2200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 2700, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "깐", "start_ms": 2900, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "덕분에", "start_ms": 3150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "에폭시가", "start_ms": 3650, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "덜", "start_ms": 4250, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "뜨지", "start_ms": 4500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "않고", "start_ms": 4850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "반질반질하긴", "start_ms": 5200, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "한데", "start_ms": 6050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "어우", "start_ms": 6450, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "겨울에", "start_ms": 6750, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "안정을 찾거나 약간만 반등을 준다고 하더라도 제일 먼저 가장 빠르게 상승하는 편입니다 그래서 대한광통신 같은 경우에 아침에",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "안정을", "start_ms": 40, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "찾거나", "start_ms": 550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "약간만", "start_ms": 1050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "반등을", "start_ms": 1550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "준다고", "start_ms": 2050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "하더라도", "start_ms": 2500, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "제일", "start_ms": 3150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "먼저", "start_ms": 3500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 3900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "빠르게", "start_ms": 4300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "상승하는", "start_ms": 4800, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "편입니다", "start_ms": 5400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 5950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "대한광통신", "start_ms": 6450, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "같은", "start_ms": 7200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "경우에", "start_ms": 7600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "아침에", "start_ms": 8100, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "로봇이 로봇을 생산하는 이 기도도 일부 공정에서 실현될 수 있다라는 겁니다 그리고 2030년 전후의 로봇 산업은 지금과 달리 하드웨어 자체보다 피지컬",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "로봇이", "start_ms": 30, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "로봇을", "start_ms": 450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "생산하는", "start_ms": 900, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 1500, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "기도도", "start_ms": 1650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "일부", "start_ms": 2150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "공정에서", "start_ms": 2550, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "실현될", "start_ms": 3150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 3650, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있다라는", "start_ms": 3850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "겁니다", "start_ms": 4400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 4900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "2030년", "start_ms": 5350, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "전후의", "start_ms": 6050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "로봇", "start_ms": 6550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "산업은", "start_ms": 6950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "지금과", "start_ms": 7450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "달리", "start_ms": 7950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "하드웨어", "start_ms": 8350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "자체보다", "start_ms": 8900, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "피지컬", "start_ms": 9550, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "항에 연속으로 성공시켜버리고 이렇게 양산 1호기까지 성공적으로 완성을 시켜버렸습니다 이건 그냥 정말 대단하다는 것과 같은 평이한 표현으로는 그 확률적 위대함의 10분의 1도 제대로 표현할 수가 없는 그래서 우리나라에서는",
"speaker": "speaker_6",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "항에", "start_ms": 20, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "연속으로", "start_ms": 300, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "성공시켜버리고", "start_ms": 900, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 1750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "양산", "start_ms": 2250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "1호기까지", "start_ms": 2650, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "성공적으로", "start_ms": 3400, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "완성을", "start_ms": 4050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "시켜버렸습니다", "start_ms": 4550, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "이건", "start_ms": 5400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 5750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 6100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "대단하다는", "start_ms": 6500, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "것과", "start_ms": 7200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "같은", "start_ms": 7600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "평이한", "start_ms": 8000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "표현으로는", "start_ms": 8500, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 9200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "확률적", "start_ms": 9400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "위대함의", "start_ms": 9950, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "10분의", "start_ms": 10550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "1도", "start_ms": 11050, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "제대로", "start_ms": 11400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "표현할", "start_ms": 11900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "수가", "start_ms": 12400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "없는", "start_ms": 12750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 13150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "우리나라에서는", "start_ms": 13650, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "출근보다 더 즐거운 부부의 퇴근 시간입니다 오늘 하루도 정말 수고 많으셨습니다",
"speaker": "speaker_7",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "출근보다", "start_ms": 50, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 600, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "즐거운", "start_ms": 850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "부부의", "start_ms": 1350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "퇴근", "start_ms": 1800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "시간입니다", "start_ms": 2200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 2850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "하루도", "start_ms": 3250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 3700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "수고", "start_ms": 4100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "많으셨습니다", "start_ms": 4450, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "저 그거는 반대죠 어쨌든 너무 외진 곳에 갑자기 젊은 나이에 그냥 여기 와서 제가 농사짓는 것도 아니고요 그냥 오는 거 그건 아닌 거 같아갖고 신랑이 하고 싶은 거는 적극 도와주고 밀어주고 하지만 저 그 생활 공간은 따로 옆에 아파트",
"speaker": "speaker_4",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "저", "start_ms": 60, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "그거는", "start_ms": 250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "반대죠", "start_ms": 650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "어쨌든", "start_ms": 1150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 1650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "외진", "start_ms": 2000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "곳에", "start_ms": 2400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "갑자기", "start_ms": 2750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "젊은", "start_ms": 3250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "나이에", "start_ms": 3650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 4100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "여기", "start_ms": 4450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "와서", "start_ms": 4800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 5200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "농사짓는", "start_ms": 5550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "것도", "start_ms": 6100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "아니고요", "start_ms": 6450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 6950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "오는", "start_ms": 7300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 7650, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "그건", "start_ms": 7850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "아닌", "start_ms": 8200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 8550, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "같아갖고", "start_ms": 8750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "신랑이", "start_ms": 9300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "하고", "start_ms": 9750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "싶은", "start_ms": 10100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "거는", "start_ms": 10500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "적극", "start_ms": 10850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "도와주고", "start_ms": 11250, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "밀어주고", "start_ms": 11800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "하지만", "start_ms": 12350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "저", "start_ms": 12850, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 13050, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "생활", "start_ms": 13250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "공간은", "start_ms": 13650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "따로", "start_ms": 14150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "옆에", "start_ms": 14550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "아파트", "start_ms": 14950, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "는 분명 차이가 크다고 합니다 외국인 입장에서는 이게 한식이거나라는 부정적인 인식을 가지기에 충분한 여지가 있죠 물론 이걸 뭐라고",
"speaker": "speaker_8",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "는", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "분명", "start_ms": 250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "차이가", "start_ms": 650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "크다고", "start_ms": 1100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "합니다", "start_ms": 1550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "외국인", "start_ms": 2000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "입장에서는", "start_ms": 2500, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 3200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "한식이거나라는", "start_ms": 3550, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "부정적인", "start_ms": 4400, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "인식을", "start_ms": 5000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "가지기에", "start_ms": 5500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "충분한", "start_ms": 6050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "여지가", "start_ms": 6550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "있죠", "start_ms": 7000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "물론", "start_ms": 7400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이걸", "start_ms": 7800, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "모라고", "start_ms": 8150, "dur_ms": 400, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 07:22:28*
