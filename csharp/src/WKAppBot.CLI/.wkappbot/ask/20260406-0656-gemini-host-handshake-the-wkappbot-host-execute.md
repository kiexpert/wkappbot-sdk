# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=27304  time=06:55:49  uptime=8525s  mem=430MB  system_processes=408"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_065511.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 701-750-175-170-175-715-710-173-127-120-120-120 574-574-574-574-570-571-705-107-751 576-571-573-750-751-715-751-750-705-750-715-701-170-175-750-705-175-123-162-163-136-312-130-3_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 237-271-253-372-253-756-263-275-752-573-215-621-507-726-267-762-207-621-206-621-567-127-267-672 265-672-570-157-156-315-316-735-752-567-567-567-576-765-756-576-567-576-576-567-6_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 576-576-574-573-571-756-735-751-736-765-753-571-765-675-516-157-157-751-375-754 162 103-310-617-361-316-613-316-310-657-625-627-672-203-102-517-753-371-162-315-712-231-160-135-3_W.mp3)
  Segment 4: 4728ms ~ 15024ms (file: 261-327-237-260-130-103-107-107-107-103-752-735-756-230-201-572-206-102-103-320-302-752-102-103-230-573 217-213-231-217-627-312-231-263-326-321-312-137-502-501-520-250-257-257-5_W.mp3)
  Segment 5: 16024ms ~ 22072ms (file: 735-325-172-136-102-105-103-756-362-632-205-235-260-320-216-326-231-621-705-103-213-213-652-216-621-352-231-217-203-231-256-750-203-230-231-567-507-103-105-276-265-276-570-367-2_V.mp3)
  Segment 6: 23072ms ~ 35564ms (file: 236-213-574-310-103-137-132-756-123-321-263-176-310-321-756-106-571-260-132-236-260-216-527-275-756-765-326-572-574-527-527-572 206-756-106-310-132-103-260-301-751-103-260-751-7_W.mp3)
  Segment 7: 36564ms ~ 37140ms (file: 123-137-710-172-126-627-173-172-317-376-753-573-751-721-327-162-765-326-327-732-235-713-217-176-127-137-160-713-132-312-316-651-623-137-361-617-316-130-567-576-321-632-253-362-3_W.mp3)
  Segment 8: 38140ms ~ 46816ms (file: 301-130-310-217-276-756-267-275-137-103-130-137-132-321-210-236-126-216-321-236-321-276-513-312-301-132-120-173-132-103-210-321-103-210-231-213-310-301-137-107-507-371-537-276-2_W.mp3)
  Segment 9: 47816ms ~ 48392ms (file: 321-156-751-567-327-673-752-712-507-257-752-267-263-157-716-752-513-756-721-507-612-657-256-276-265-257-257-673-526-756-576-276-576-127-756-126-261-215-625-275-265-572-572-705-3_V.mp3)
  Segment 10: 49392ms ~ 49968ms (file: 710-170-170-170-170-716-170-716-175-175-716-716-176-176-715-710 105-170-137-175-170-175-107-173-713-176-176-715-175-715-716-715-716-716-716-175-715-176-175 106-107-175-107-107-7_V.mp3)
  Segment 11: 50968ms ~ 51544ms (file: 657-326-263-265-257-126-263-317-562-705-672-672-260-321-137-216-274-213-263-237-571-527-123-267-521-125-135-275-263-162-216-127-260-625-675-672-217-213-321-527-316-315-572-267-5_V.mp3)
  Segment 12: 52544ms ~ 60716ms (file: 260-237-230-320-261-231-261-216-213-120-132-320-321-231-326-126-106-253-235-326-236-317-263-130-132-123-217-726-312-312-710-217-231-321-127-201-130-310-312-120-123-217-672-132-2_W.mp3)
  Segment 13: 61716ms ~ 70680ms (file: 106-102-162-310-623-613-126-130-261-527-572-205-201-675-675-206-310-203-750-756-726-756-275-130-231-162-573-756-317-107-310-206-310-312-203-231-102-106-130-130-203-103-132-103-1_V.mp3)
  Segment 14: 71680ms ~ 81184ms (file: 130-123-213-312-231-312-312-321-130-130-136-130-312-231-123-326-362-213-321-103-312-231-321-132-312-312-312-312-312-123-130-312-312-132-312-230-237-213-231-270-213-720-720-127-1_W.mp3)
  Segment 15: 82184ms ~ 87080ms (file: 326-356-617-731-152-317-267-651-201-765-257-103-267-160-367-365-203-237-365-753-236-237-236-157-356-573-735-736-652-351-136-135-365-675-756-567-376-367-376-371-372-370-231-732-3_W.mp3)
  Segment 16: 88080ms ~ 96540ms (file: 705-173-276-312-301 132-126-510-521-203-203-620-123-102-321-623-620-726-206-123-201-317-136 314-301-367-361-367 361-367-631-632-620-672-362-653-627-213-102-236-206-602-312-310-3_V.mp3)
  Segment 17: 97540ms ~ 98116ms (file: 236-136-567-532-126-132-753-123-602-276-123-673-576-270-213-625-231-276-572-236-732-257-273-506-576-572-627-502-326-562-273-562-135-753-365-671-327-576-256 765-123-312-652-756-2_V.mp3)
  Segment 18: 99116ms ~ 99692ms (file: 120-150-157-571-736-123-627-302-326_W.mp3)
  Segment 19: 100692ms ~ 112068ms (file: 317-547-312-367-231-321-726-327-236-267-237-675-273-267-123-127-753-276-537-357-312-317-765-273-170-237-523-301-231-263-132-375-276-263-310-132-132-103-213-706-706-574-167-163-6_W.mp3)
  Segment 20: 113068ms ~ 113644ms (file: 153-135-752-531-651-531-537-635-321-236-657-576-157-352-537-316-362-356-135-361-135-316-165-517-361-156-531-135-361-136-136-652-673-576-675-765-576-765-527-657-672-265-715-657-3_W.mp3)
  Segment 21: 114644ms ~ 115220ms (file: 675-365-673-165-637-136-236-617-260 631-275-572-327-763-725-507-125-216-657-167-275-237-730-627-526-253-327-357-731-765-760-170-752-237-750-725-502-265-236-261-752-276-623-732-2_V.mp3)
  Segment 22: 116220ms ~ 116796ms (file: 517-576-576-756-263-571-137-173-317-571-125-713-135-217-132-712-137-210-231-312-756-172-132-273-756-175-761-175-152-236-237-507-750-567-327-761-326-765-726-265-217-723-725-327-5_V.mp3)
  Segment 23: 117796ms ~ 126940ms (file: 263-321-137-132-102-673-617-263-671-763-617-753-754-275-120-310-310-126-672-675-263-756-236-756-321-230-267-263-267-267-671-675-671-612-617-176-752-236-273-752-320-375-102-236-7_W.mp3)
  Segment 24: 127940ms ~ 135392ms (file: 302-302-230-102-261-321-210-302-731-103-321-230-260-267-130-316-257-730-371-301-261-261-130-103-301-130-302-207-312-132-230-103-176-310-206-261-310-132-320-321-130-123-713-103-3_V.mp3)
  Segment 25: 136392ms ~ 141468ms (file: 263-236-132-213-213-106-120-321-321-321-157-216-726-762-763-237-327-170-120-672-327-231-237-236-312-562-625-120-102-321-263-256-210-126-230-523-312-517-756-657-235-231-213-120-2_W.mp3)
  Segment 26: 142468ms ~ 148048ms (file: 167-367-106-276-713-103-176-276-267-263-216-326-312-231-321-317-320-236-376-627-263-326-237-326-103-231-326-316-672-673-237-276-276-162-137-176-275-632-302-753-270-237-237-627-7_W.mp3)
  Segment 27: 149048ms ~ 157472ms (file: 301-310-312-321-230-213-130-203-203-320-206-107-102-312-372-236-321-230-320-236-320-312-213-230-201-136-163-107-106-321-132-130-130-137-130-301-320-320-320-320-321-263-306-236-1_W.mp3)
  Segment 28: 158472ms ~ 164628ms (file: 136-756-267-176-231-362-617-376-213-231-102-617-163-154-756-267-321-371-735-375-170-130-103-172-275-130-731-175-170-130-130-102-167-216-572-572-312-103-167-301-316-725-317-107-1_W.mp3)
  Segment 29: 165628ms ~ 166204ms (file: 103-756-765-671-657-312-130 527-176-265-261-376-712-675-675-671-675-617-706-762-527-267-231-765-132-705-126-123-326-675-732-267-753-236-205-236-612-327-235-753-362-527-267-607-2_V.mp3)
  Segment 30: 167204ms ~ 167780ms (file: 673-753-576-570-576-547-563-573-537-537-615_W.mp3)
  Segment 31: 168780ms ~ 174108ms (file: 123-312-120-132-123-321-123-213-123-213-213-123-213-231-123-213-123-210-213-120-132-132-312-132-123-506-157-571-570-576-157-157-103-570-561-157-570-156-157-153-123-135-153-570-5_V_stt_the.mp3)
  Segment 32: 175108ms ~ 175684ms (file: 765 132-316-361-316-631-361-613-361-631-637-361-361-136-567-617-672-672-623-672-632-362-627-632-136-613-612-621-632-631-623_W.mp3)
  Segment 33: 176684ms ~ 182876ms (file: 527-573-752-137-275-260-260-723-752-756-753-267-270-321-237-752-756-263-321-263-623-607-753-321-753-173-261-276-312-321-573-320-327-762-102-761-761-276-217-273-172-301-123-732-7_W.mp3)
  Segment 34: 183876ms ~ 184452ms (file: 673-736-672-736-367-316-673-673-673-762_W.mp3)
  Segment 35: 185452ms ~ 191896ms (file: 260-267-726-326-723-123-765-271-102-716-107-273-517-725-276-270-271-726-276-301-276-267-217-217-372-267-257-256-754-230-130-631-236-102-130-236-307-106-301-103-231-276-106-216-1_W.mp3)
  Segment 36: 192896ms ~ 193472ms (file: 752-756-176-751-725-567-517-126-751-765-257-376-156-167-253-761-361-167-653-213-365-726-162-132-756-157-732-576-751-175-516-271-132-725-721-753-735-173-765-675-176-167-675-625_V.mp3)
  Segment 37: 194472ms ~ 195048ms (file: 532-275-257-256-570-375-573-352-752-217-267-670-236-236-263-132-362-750-573-567-627-657-502-672-257-705-172-256-762-632-206-362-571-571-576-356-356-631-637-361-631-160-361-531-5_V.mp3)
  Segment 38: 196048ms ~ 203248ms (file: 230-572-103-310-203-260-236-312-231-260-213-201-210-217-572-537-765-321-130-103-120-210-201-201-120-302-260-120-752-130-213-267-321-753-574-756-206-752-103-312-103-103-103-750-1_W.mp3)
  Segment 39: 204248ms ~ 204824ms (file: 276-726-276-726-276-726-276-627-617-761-761-763-176-671-726-672-761-763-671_W.mp3)
  Segment 40: 205824ms ~ 206400ms (file: 716-715-176-507-137-315-617-173-730-317-176-173-173-731-371-176-761-537-260-207-231-352-765-175-715-712-175-516-157-172-715-172-715-716-716-170-617-163-136-716-760-716-673-170-1_V.mp3)
  Segment 41: 207400ms ~ 215932ms (file: 263-321-236-327-301-321-267-136-136-160-621-276-203-267-276-263-106-263-260-123-532-372-237-136-237-301-106-167-103-132-213-107-132-157-561-157-261-126-612-516-672-136-137-173-1_W.mp3)
  Segment 42: 216932ms ~ 224996ms (file: 127-106-172-126-673-260-752-153-130-103-275-527-120-210-260-102-672-263-754-137-310-103-751-102-516-573-276-276-276-173-175-751-572-270-230-123-310-310-263-267-526-216-627-276-1_W.mp3)
  Segment 43: 225996ms ~ 226572ms (file: 263-627-325-270-325-576-752-672-527-526-276-275-725-725-267-670-671-672-163-726-627-763-257-235-713-312-312-136-573-517-516-537-152-156-175-175-165-170-137-765-276-173-713-763_W.mp3)
  Segment 44: 227572ms ~ 235132ms (file: 573-567-564-263-623-275-627-172-547-576-672-672-672-762-672-562-627-672-762-627-263-362-627-267-236-263-206-627-216-132-321-263-627-261-546-362-263-572-267-620-612-267-321-612-5_W.mp3)
  Segment 45: 236132ms ~ 236708ms (file: 163-576-137-765-560-156-560-652-567-652-576-716-672-726-762-673-710-367-176-750-376-756-276-317-172-657-562-576-271-315-125-235-123-231-537-136-531-261-267-263-216-135-103-103-1_V.mp3)
  Segment 46: 237708ms ~ 244980ms (file: 126-210-261-627-130-230-216-103-261-230-572-103-120-576-576-260-754-570-670-201-230-103-260-210-203-276-267-260-627-276-102-216-571-120-126-570-176-701-203-670-123-120-765-670-2_W.mp3)
  Segment 47: 245980ms ~ 246556ms (file: 162-236-673-162-567-365-521-265-761-361-623-615-165-623-576-756-126-362-672-267-567-130 352-352-321-132-231-231-213-621-625-625-756-256-735 310_W.mp3)
  Segment 48: 247556ms ~ 252776ms (file: 103-120-216-127-210-260-231-127-231-261-123-123-132-327-312-312-123-216-126-206-237-102-517-271-123-216-317-302-312-213-312-632-132-123-126-210-215-201-271-123-213-172-103-123-1_W.mp3)
  Segment 49: 253776ms ~ 259392ms (file: 162-613-260-162-126-631-615-653-653-625-625-652-213-321-301-231-276-216-127-107-725-132-367-173-310-106-130-203-103-675-312-320-231-321-321-130-103-132-302-312-213-103-103-173-6_W.mp3)
  Segment 50: 260392ms ~ 270040ms (file: 231-753-761-756-270-572-237-756-732-130-130-230-301-675-106-574-756-756-207-752-132-210-573-310-231-130-573-103-301-102-132-572-153-571-310-106-715-751-715-102-102-207-752-257-2_W.mp3)
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
> **Time**: 2026-04-06 06:56:56
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "피와 땀과 눈물로 써낸 지난 10년간의",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "피와", "start_ms": 20, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "땀과", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "눈물로", "start_ms": 270, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "써낸", "start_ms": 380, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "지난", "start_ms": 460, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "10년간의", "start_ms": 510, "dur_ms": 40, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "그 핵심 개발 데이터들을 얻을 수 있었지만",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "그", "start_ms": 30, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "핵심", "start_ms": 80, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "개발", "start_ms": 140, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "데이터들을", "start_ms": 210, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "얻을", "start_ms": 360, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 420, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "있었지만", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "그 귀한 보물을 고작 1조 원의 돈과 맞바꿨고",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "그", "start_ms": 25, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "귀한", "start_ms": 65, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "보물을", "start_ms": 125, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "고작", "start_ms": 205, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "1조", "start_ms": 275, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "원의", "start_ms": 325, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "돈과", "start_ms": 365, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "맞바꿨고", "start_ms": 435, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "이제 KF-21이 보란 듯이 완벽하게 개발되었다고 말씀드리긴 어렵지만은 지금 상당한 물량이 계속 확보되고 있다",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "KF-21이", "start_ms": 250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "보란", "start_ms": 650, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "듯이", "start_ms": 800, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "완벽하게", "start_ms": 900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "개발되었다고", "start_ms": 1200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "말씀드리긴", "start_ms": 1700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "어렵지만은", "start_ms": 2100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 2500, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "상당한", "start_ms": 2700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "물량이", "start_ms": 3000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 3300, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "확보되고", "start_ms": 3500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "있다", "start_ms": 3800, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "오늘부터 자원 안보 위기 경보가 3단계 경계로 격상됐고 오는 8일부터 공공기관 승용차 5부제는 2부제로 강화되고 공영 주차장에도 승용차 5부제가 시작됩니다 MBC 뉴스 김민영입니다",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "오늘부터", "start_ms": 60, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "자원", "start_ms": 360, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "안보", "start_ms": 510, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "위기", "start_ms": 660, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "경보가", "start_ms": 810, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "3단계", "start_ms": 1060, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "경계로", "start_ms": 1260, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "격상됐고", "start_ms": 1460, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "오는", "start_ms": 1810, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "8일부터", "start_ms": 1960, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "다음 날도 상한가 그리고 또 그 다음 날에 24%까지 상승하면서 한 달쯤 전에 광통신 대장주로 상승했고 끼도 엄청",
"speaker": "speaker_4",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "다음", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "날도", "start_ms": 190, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "상한가", "start_ms": 340, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 590, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 790, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 890, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 940, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "준비됐어? 물론이죠 사실 저도 엄청 기대하고 있었어요",
"speaker": "speaker_5",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "준비됐어?", "start_ms": 80, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "물론이죠", "start_ms": 280, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "사실", "start_ms": 530, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "저도", "start_ms": 680, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "김문정 씨가 (laughter) 저 진짜 너무 해피해요",
"speaker": "speaker_6",
"confidence": 0.89,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "mixed",
"words": [
{"word": "김문정", "start_ms": 100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "씨가", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "저", "start_ms": 550, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 650, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "아 시장 와서 서비스 받는 이렇게 많이 서비스 받는 거 처음이에요 그거 바로 광주의 정이야 확실히",
"speaker": "speaker_7",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "아", "start_ms": 30, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "시장", "start_ms": 80, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "와서", "start_ms": 230, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "서비스", "start_ms": 380, "dur_ms": 250, "phase_lr_deg": 0.0}
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
},
{
"segment": 11,
"transcript": "중점입니다 국민을 막아라 미국 플로리다 올랜도 지역 매체 더 메인랜드는 올랜도가 이기기 위해서 꼭 해야 할 세 가지",
"speaker": "speaker_8",
"confidence": 0.91,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "중점입니다", "start_ms": 50, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "국민을", "start_ms": 400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "막아라", "start_ms": 650, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "최근 대만 방송을 통해 공개된 한국 방위 산업 관련 소식이 예상보다 훨씬 강한 파장을 일으키면서 대만 사회 전체가 큰 요동을 치고 있기 때문입니다 무엇보다 충격",
"speaker": "speaker_9",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "최근", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "대만", "start_ms": 190, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "국내산이네 아 그렇죠 국내산 너무 바삭바삭해 진짜 아 너무 바삭바삭해 뭔가 호떡이라기보다",
"speaker": "speaker_10",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "국내산이네", "start_ms": 60, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 410, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "에 오코노미야끼 하지 오오 위아갔네 진짜 신기하다 그치 여기 소금빵이 유명해 소금빵 아 여기가 소금빵으로 유명해 유명해요? 오",
"speaker": "speaker_11",
"confidence": 0.90,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "에", "start_ms": 20, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "오코노미야끼", "start_ms": 70, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "준비됐다고 경고했습니다 대미 협상 창구로 주목받는 탈리반 의회 의장은 또 다른 해상 요충지 바브 엘 만데브 해협 봉쇄를 우회적으로 거론했습니다 이 해협을 지나는 석유와 곡물 비료 물동량은",
"speaker": "speaker_12",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "준비됐다고", "start_ms": 30, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "경고했습니다", "start_ms": 380, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "지도자로 선출된 뒤 종적을 감춰 폭격으로 큰 부상을 입고 러시아에서 수술까지 받았다는 소문이 돌았",
"speaker": "speaker_13",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "지도자로", "start_ms": 40, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "선출된", "start_ms": 390, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "시작해 미사일 장착 및 발사 그리고 최근 성공한 주간 및 야간 공중 급유 테스트를 비롯한 수백 권도 넘는 어려운 난관과 테스트들을 단 한 번의 사고 그리고 심지어 실패도 없이 전부 다",
"speaker": "speaker_14",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "시작해", "start_ms": 20, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "미사일", "start_ms": 200, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "언급한 냉전형 생산 능력과 극강의 가성비 외에도 가장 핵심적이고 치명적인 경쟁력 하나가 더 있기 때문입니다 바로 아낌없는 기술 이전 전략입니다 국제 무기 거래에서 구매국들을 가장 괴롭히는 것은 판매국이 마치 도둑을 경계하",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "언급한", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "냉전형", "start_ms": 180, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "주... 주시하잖아요 너무 맛있네 그리고 막 그 껍질이가 고소하다 음 그치 대박 야 너무 맛있다 다행이네 맛있게 먹어서",
"speaker": "speaker_15",
"confidence": 0.88,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "주...", "start_ms": 20, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "주시하잖아요", "start_ms": 70, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "수도 약합니다 그래서 엔비디아 구글 딥마인드 등이 들어오는 거죠 서로 얻을 건 얻으면 되는 겁니다 이런 관점에서 테슬라 옵티머스는 한국 및 중국 생산이 가능할까요 이 부분도 상당히 애매합니다 이게 어떻게 될 건지 테슬라의 전략으로",
"speaker": "speaker_16",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "수도", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "약합니다", "start_ms": 140, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "삼각형 창이 된 거지? 야 이거 한번 물어봐야 되겠네 계세요? 계십니까 어 안녕하세요 아이 반갑습니다 안녕하세요",
"speaker": "speaker_17",
"confidence": 0.91,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "삼각형", "start_ms": 60, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "창이", "start_ms": 310, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "올리는 게 집 짓기 제일 큰 난관이었다죠 돈이 똑 떨어져서 처음으로 은행을 찾아야 했답니다 이게 돈도 원하는 만큼",
"speaker": "speaker_18",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "올리는", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 250, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "아 광주 토박이 오오 서른 살이세요? 엄청 어려 보이시는데 강줄 생각하면 마시는 거 뭐예요 호떡 아 광주 하면은 여기 송정이잖아요 저기 송정",
"speaker": "speaker_19",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "아", "start_ms": 30, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "광주", "start_ms": 80, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "나도 정확해 소니는 여전히 프리미어 리그에서 뛸 수 있어 내 생각에는 소니가 없었다면 부항가는 해트트릭할 수 없었을 것 같아 소니의 존재감이 엘",
"speaker": "speaker_20",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "나도", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "정확해", "start_ms": 190, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "가는 것 같은데요 서로의 공간을 존중했기에 누릴 수 있는 평화랄까요 지금 보시는 저 뒤의 집이 지금 어머니하고 누님이 살고 계시는 집이거든요 아 저기가 어머니 댁이에요? 네",
"speaker": "speaker_18",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "가는", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 200, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "맥북으로 나아졌다라고 하면은 그리고 맥북이라든가 최근 이런 컴퓨팅을 충분히 지원하면서 디램이 확보된 이런 맥북 프로 같은 디바이스들 아니면 맥 스튜디오 같은 디바이스들을 가져와서 처리를 하게 되면은 굉장히",
"speaker": "speaker_16",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "맥북으로", "start_ms": 40, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "나아졌다라고", "start_ms": 290, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "사실 이 부분 확인해 주지는 않습니다 하지만 정황상 그럴 가능성이 높다라는 거죠 최근 엔비디아 수출도 규제를 받고 있죠 꼭 이 조항이 포함돼 있든 안",
"speaker": "speaker_4",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "사실", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 170, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "Oh in the mu-jang! And this is the sauce for this. So before eating everything when you guys have to eat this ma-",
"speaker": "speaker_21",
"confidence": 0.87,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Oh", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 130, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "아니라 드론과 순항 미사일 탄도 미사일을 동시에 투입하는 방식으로 이스라엘을 공격하고 있는데요 여기에 다탄두 방식까지 결합해 하나의 목표물에 여러",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "아니라", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "드론과", "start_ms": 190, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "Make sure to break it apart with your chopsticks into some pieces. For this one make sure you guys just mix it a little bit one more time",
"speaker": "speaker_21",
"confidence": 0.91,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Make", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "sure", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "야 씨 나 혼자 왔을 때는 진짜 반응 이 정도 안 좋았는데 진짜 대박이죠 진짜 이거 맞아! 뭐예요?",
"speaker": "speaker_22",
"confidence": 0.86,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "야", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "씨", "start_ms": 130, "dur_ms": 100, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 06:56:56*
