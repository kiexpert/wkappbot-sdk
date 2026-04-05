# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=6944  time=11:17:32  uptime=34461s  mem=342MB  system_processes=358"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_111645.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 765-230-326-163-106-163-126-162-103-136-163-602-620-602-762-672-620-260-263-310-136-160-310-310-301-263-650-567-657-657-360-301-306-316-132-123-236-230-320-132-612-320-574-537-5_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 617-564-517-756 647-376-367-673-673-372-165-176-736-371-317-316-376-317-137-317-310-136-102-136-103-310-130-310-301-310-637-317-671-137-361-130-301-130-175-167-317-657-517-165-7_V.mp3)
  Segment 3: 3152ms ~ 13844ms (file: 312-203-312-302-570-761-123-130-207-203-752-267-325-351-256-527-574-573-257-756-103-756-130 321-756-276-276-267-206-260-263-320-312-735-237-267-326-276-210-103-104-321-106-675-6_W_stt_responsibilities_to_make_sure_that_these.mp3)
  Segment 4: 14844ms ~ 15420ms (file: 173-607-103-315-370-176-310-137-130-103-130-137-137-371-375 375-753-173-513-370-301-137-367-607-130-601-136-130-316-607-610-671-671-673-316-316-376-130-603-617-670-361-136-317-6_V.mp3)
  Segment 5: 16420ms ~ 16996ms (file: 762-736-376-756-537-765-574-756-137-136-517-176-673-137-176-136-317-135-317-136-137-163-176-173-167-137-137-137-136-137-103-715-627-371-137-752-735-137-715-316-136-156-573-175-1_W.mp3)
  Segment 6: 17996ms ~ 18572ms (file: 517-751-517-157-517-571-156 105-152-251-257-251-521-652-615-615-651-657-672_W.mp3)
  Segment 7: 19572ms ~ 20148ms (file: 170-107-175-175-167-176-175-176-176-167-761-761-176-716-176-175-765-175-175-751-176-176 546-105-105-150-150-105-105-150-150-157-175-173-710-175-107-175-761-175-176-176-176-173-7_W.mp3)
  Segment 8: 21148ms ~ 21724ms (file: 574-574-547-574-574-547-570-547-574-570-576-574-574-576-574-574-576-754-754-754-754-754-754-574-514-574-567-517-574-547-576-574-547-547-547-574-576-570-547-574-574-576-754-756-7_W.mp3)
  Segment 9: 22724ms ~ 31004ms (file: 362-362-236-317-310-312-230-362-136-312-361-236-753-673-273-176-765-132-315-310-301-213-137-217-132-103-132-213-123-203-230-102-267-260-312-102-201-207-201-230-273-132-301-301-2_W.mp3)
  Segment 10: 32004ms ~ 32580ms (file: 571-157-571-751-517-571-570-105-157-156-105-571-137-571-153-157-175-715-570-751-570-571-571-571-157-751-571-753-751-750-715-571-517-153-513-157-105-301-503-571-571-715-517-571-7_V.mp3)
  Segment 11: 33580ms ~ 34156ms (file: 517-173-175-175-715-175-751-175-175-175-157-751-751-157-175-175-750 510-510-501-105-105-150-153-175-715-175-751-175-175-715-175-157-715-175-753-715-175-175-715-175 150-510-103-1_W.mp3)
  Segment 12: 35156ms ~ 43040ms (file: 265-572-230-231-231-301-103-517-547-574-574-361-312-267-263-213-123-132-316 372-372-237-273-236-572-325-320-372-312-731-317-573-601-102-267-267-132-106-217-102-276-527-267-267-2_W.mp3)
  Segment 13: 44040ms ~ 52572ms (file: 602-706-267-267-213-607-752-261-206-620-206-126-754-752-165-762-206-275-105-576-260-102-127-201-127-673-267-302-321-203-571-261-237-230-201-312-326-320-230-102-327-260-201-267-3_V.mp3)
  Segment 14: 53572ms ~ 58144ms (file: 217-217-210-123-213-213-213-213-213-123-217-217-213-210-213-213-123-213-231-210-213-123-213-217-123-213-231-123-120-213-123-123-120-123-102-120-213-120-102-201-120-123-213-321-2_W.mp3)
  Segment 15: 59144ms ~ 68936ms (file: 102-270-320-267-203-120-231-230-267-270-207-527-270-132-230-102-102-754-103-130-102-172-754-120-173-231-267-207-230-657-276-320-263-236-576-276-276-267-276-267-260-230-102-103-7_W.mp3)
  Segment 16: 69936ms ~ 70512ms (file: 531-762-571-756-275-267-276-756-576-652-367-613-613-163-613-167-165-653-615-617-167-167-675-671_W.mp3)
  Segment 17: 71512ms ~ 77740ms (file: 265-263-310-103-263-527-524-261-126-312-735-571-574-231-765-756-236-316-261-261-261-132-310-156-716-312-216-671-761-725-725-756-756-756-731-321-735-273-275-275-615-573-327-132-1_W.mp3)
  Segment 18: 78740ms ~ 79316ms (file: 126-163-613-326-517-157-517-567-572-561-567-516-516-613-672-617-173-156-651-576-176-571-657-157-576-751-217-316-130-127-152-576-173-160-361-321-536-560-375-765-256-513-567-563-5_W.mp3)
  Segment 19: 80316ms ~ 80892ms (file: 517-157-517-517-157-571-157-571-570-570-751 625-526-527-750-205-752-751-571-571-571-517-157-517-517-571-517-571-157-517-571-517-157-571-517 625-562-650-570-520-752-572-570-571-5_V.mp3)
  Segment 20: 81892ms ~ 90928ms (file: 172-137-130-312-310-301-103-213-213-456-320-120-126-217-213-312-327-327-327-130-123-312-173-637-367-316-326-136-103-321-312-326-310-310-136-637-327-326-326-312-126-231-123-132-2_W.mp3)
  Segment 21: 91928ms ~ 103052ms (file: 326-312-357-167-165-321-132-765-276-120-672-237-172-102-267-267-267-267-237-236-267-267-267-657-102-217-321-130-104-217-123-376-756-276-327-130-236-267-170-103-627-231-267-213-1_W.mp3)
  Segment 22: 104052ms ~ 114024ms (file: 126-102-106-120-162-261-312-756-167-617-167-175-102-167-157-106-756-201-217-213-213-576-352-210-753-107-316-167-132-126-167-612-127-312-326-564-156-617-675-162-167-675-671-126-1_W.mp3)
  Segment 23: 115024ms ~ 122944ms (file: 613-162-612-162-163-123-135-321-376-672-261-235-326-370-306-312-315-231-312-321-307-360-314-261-712-672-627-762-720-761-672-623-267-621-267-261-627-123-162-261-617-176-170-167-1_W.mp3)
  Segment 24: 123944ms ~ 124520ms (file: 173-730-370-310-130-301-317-137-130-371-103-103-317-371-701-716-307-367-310-710-137-301-160-730-706-706-701-371-735-675-765-675 715-137-701-371-571-735-317-107-173-103-715-317-3_V.mp3)
  Segment 25: 125520ms ~ 126096ms (file: 517-751-571-750-754-710 574-574-571-574-574-570-157-175-157-574-570-574-507-571-570-574-570-750-750-754-574 571-517-570-571-150-710-570-571-107-170-571-750-507-751-751-105-175-5_V.mp3)
  Segment 26: 127096ms ~ 127672ms (file: 567-657-657-576-576-567-756-657-567-675-576-562-675-675-675-675-675 567-576-567-576-576-576-567-576-576-576-576-576-576-756-576-576-675-765-765-675-765-675 567-567-576-576-576-5_V.mp3)
  Segment 27: 128672ms ~ 129248ms (file: 312-671-602-602-261-320-631-316-316-310-103-167-176-103-136-170-106_V.mp3)
  Segment 28: 130248ms ~ 130824ms (file: 510-510-517-516-510-150-510-517 105-501-150-150-150-105-517-150-501-517-105-510-510-510-501-150-150-510-510-501-510-153-517-150-571-501-517-105-157-570 150-105-150-105-157-501-5_V.mp3)
  Segment 29: 131824ms ~ 132400ms (file: 301-312-162-761-125-352-136-135-136-623 765-675-361-267-132-652 427-301-106-301_V.mp3)
  Segment 30: 133400ms ~ 144992ms (file: 230-267-263-126-123-123-123-217-123-126-132-123-132-126-123-123-123-217-123-132-312-132-231-126-213-126-123-123-231-132-216-132-132-126-123-120-163-715-167-617-173-173-231-613-1_W.mp3)
  Segment 31: 145992ms ~ 146568ms (file: 576-756-725 752-756-567-576-756-576-576-756-756-756-756-576-576-576-576-756-752-564-524 752-756-756-576-576-756-756-756-765-762-531 752-756-567-756-576-567-576-576-576-756-576-7_W.mp3)
  Segment 32: 147568ms ~ 148144ms (file: 130-370-137-130-132-730-173-130-301-750-715-751-725 135-571-310-107-103-713-317-103-310-173-107-103-130-301-137-701-713-173-172-137-317-705-703-701-750-317-735-137-705-371 107-3_V.mp3)
  Segment 33: 149144ms ~ 149720ms (file: 576-570-157-570-715-715-750-751-754-754-754-751-715-175-175-175 571-571-751-715-751-751-175-751-175-751-715-170-175-175 571-157-715-750-570-571-715-751-175-751-751-751-175-751-7_W.mp3)
  Segment 34: 150720ms ~ 160404ms (file: 627-123-123-312-213-263-263-267-263-213-127-572-231-126-672-260-132-264-107-756-265-216-267-231-267-263-127-236-265-725-102-752-753-574-216-132-132-627-102-160-106-671-136-123-1_W.mp3)
  Segment 35: 161404ms ~ 161980ms (file: 765 502-562-570-507-526-526-705-657-572-765-756-765-675-756-765-765-675-657 267-257-527-527-527-257-527-257-257-527-756-675-756-576-576-657-567-675-675 527-257-526-572-257-256-2_V.mp3)
  Segment 36: 162980ms ~ 163556ms (file: 137-136 312-657-163-137-173-371-136-361-167-637-765-756-675-153-512-572-517-567-531-576-621-312-673-671-756-756-576-576-756-756-765_W.mp3)
  Segment 37: 164556ms ~ 174240ms (file: 132-301-263-126-261-316-541-413-736-367-301-310-312-103-126-213-207-271-210-123-126-103-236-210-765-671-231-320-176-203-103-213-102-302-203-231-321-126-102-207-216-216-671-102-1_V.mp3)
  Segment 38: 175240ms ~ 175816ms (file: 123-621-632-103-167-136-735-731-317-175-756-217-256-716-617-701-173-671-107-135-327-237-175-705-213-127-312-136-230-306-132-130-132-750-123-702-276-175-132-726-517-137-135 572-6_W.mp3)
  Segment 39: 176816ms ~ 177392ms (file: 302-231-351-267-365-361-326-352-216-236-263-625-263-327-130-302-362-312-231-321-503-132-571-615-156-561-567-603-657-576-567-360-321-136-503-301-307-237-236-236-627-637-673-361-1_W.mp3)
  Segment 40: 178392ms ~ 182568ms (file: 316-312 123-103-510-512-231-206-620-123-103-623-260-263-261-103-125 267-136-316-103-135-302-307-315-321-372-321_V.mp3)
  Segment 41: 183568ms ~ 188320ms (file: 213-216-621-263-312-162-213-326-572-236-263-261-126-123-316-216-260-265-260-206-201-123-201-206-230-123-231-576-630-236-302-103-327-130-230-231-203-103-103-217-261-263-231-321-2_W.mp3)
  Segment 42: 189320ms ~ 189896ms (file: 315-310-371-367-736-375-371-673-376-367-376-367-617-631-613-361-570-750-750-715-175-537-753-573-750-715-573-751-750-750-751-750-705-705-750-751-570-710-507-751-576-750-756-701-5_W.mp3)
  Segment 43: 190896ms ~ 191472ms (file: 570-571-507-503-756-756-675-756-765-751_W.mp3)
  Segment 44: 192472ms ~ 193048ms (file: 672-367-726-267-135-165-135-163-173-135-137-317-751-712-752-751-715-175-735-517-751-175-157-571-756-517-576-157-756-751-765-756-765-725-725-751-273-125-516-651-561-615-156-651-5_W.mp3)
  Segment 45: 194048ms ~ 203984ms (file: 132-716-237-123-102-271-167-273-327-716-710-106-270-273-132-201-275-127-320-123-102-132-203-321-230-103-301-327-273-237-231-153-230-321-756-756-302-237-320-210-132-267-267-126-1_W.mp3)
  Segment 46: 204984ms ~ 205560ms (file: 362-672-230-625-620-607-375-302-301-136-526-536-576-576-576-572-231-132-132-132-301-316-302-312-302-265-326-132-175-153-152-561-253-307-203-270-205-275-275-216-612 756-651-102-1_W.mp3)
  Segment 47: 206560ms ~ 207136ms (file: 137-312-136-316-137-156-517-135-172-176-163-163-167-167-176-176-163-176-167-167-176-167-167-165-136-163-163-651-136-130-312-621-261-612-126-136-136-216-231-126-213-126-263-163-1_W.mp3)
  Segment 48: 208136ms ~ 214868ms (file: 127-721-621-216-162-610-230-203-612-172-623-613-361-267-130-612-317-631-103-236-216-261-326-123-201-263-213-312-103-236-203-267-132-126-362-216-627-127-120-126-310-301-103-612-6_W.mp3)
  Segment 49: 215868ms ~ 216444ms (file: 517-517-105-517-105-517-157-105-150-150-150-150-105-501-510-517-510-150-105-153-105-150-153-105-150-153-105-105-510-513-510 510-105-507-501-507-501-570-501-510-510-501-157-510-1_V.mp3)
  Segment 50: 217444ms ~ 225688ms (file: 230-276-236-236-261-216-125-120-756-237-765-106-206-236-206-261-267-231-263-726-762-321-362-752-276-261-236-263-263-321-153-320-136-203-261-217-756-267-216-130-123-213-267-260-1_W.mp3)
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
> **Time**: 2026-04-05 11:19:28
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 2,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 3,
"transcript": "Our responsibility is to make sure that these folks are prepared for this. What could Penn State do? What would you do if you were in that position? Think bigger. Think about what kind of consciousness do you need students",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Our", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "responsibility", "start_ms": 200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 850, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 950, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "make", "start_ms": 1050, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "sure", "start_ms": 1200, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "that", "start_ms": 1400, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "these", "start_ms": 1550, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "folks", "start_ms": 1750, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "are", "start_ms": 2000, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "prepared", "start_ms": 2100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "for", "start_ms": 2500, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "this.", "start_ms": 2650, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "What", "start_ms": 3000, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "could", "start_ms": 3150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "Penn", "start_ms": 3300, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "State", "start_ms": 3500, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "do?", "start_ms": 3700, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "What", "start_ms": 4100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "would", "start_ms": 4250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 4400, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "do", "start_ms": 4500, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "if", "start_ms": 4650, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 4750, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "were", "start_ms": 4850, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 5000, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "that", "start_ms": 5100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "position?", "start_ms": 5250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "Think", "start_ms": 5900, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "bigger.", "start_ms": 6100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "Think", "start_ms": 6650, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "about", "start_ms": 6850, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "what", "start_ms": 7100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "kind", "start_ms": 7250, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "of", "start_ms": 7450, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "consciousness", "start_ms": 7550, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "do", "start_ms": 8200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 8350, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "need", "start_ms": 8450, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "students", "start_ms": 8650, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "동반자 관계 수준으로 격상한다고 발표했다 이재명 대표는 글로벌 현안에 대응하기 위해 한국과 프랑스의 대응을 조율하고자 하는 의지를 밝히는 한편 에마뉘엘 마크롱 대통령은",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "동반자", "start_ms": 110, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "관계", "start_ms": 550, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "수준으로", "start_ms": 800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "격상한다고", "start_ms": 1300, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "발표했다", "start_ms": 1900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이재명", "start_ms": 2550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "대표는", "start_ms": 2950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "글로벌", "start_ms": 3450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "현안에", "start_ms": 3850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "대응하기", "start_ms": 4300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "위해", "start_ms": 4800, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "한국과", "start_ms": 5150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "프랑스의", "start_ms": 5550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "대응을", "start_ms": 6050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "조율하고자", "start_ms": 6450, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 7100, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "의지를", "start_ms": 7400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "밝히는", "start_ms": 7800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "한편", "start_ms": 8200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "에마뉘엘", "start_ms": 8700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "마크롱", "start_ms": 9150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "대통령은", "start_ms": 9550, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "중동 전쟁을 TV를 꽂았으면은 이게 그냥 된다라는 거죠 원래 구독을 해야 되는 건데 그래서 X를 통해서 미하오 가핀스키라고 하는 요게",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "중동", "start_ms": 90, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "전쟁을", "start_ms": 400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "TV를", "start_ms": 950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "꽂았으면은", "start_ms": 1300, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 2000, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 2300, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "된다라는", "start_ms": 2600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "거죠", "start_ms": 3150, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "원래", "start_ms": 3550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "구독을", "start_ms": 3900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "해야", "start_ms": 4350, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "되는", "start_ms": 4650, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "건데", "start_ms": 4950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 5400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "X를", "start_ms": 5850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "통해서", "start_ms": 6200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "미하오", "start_ms": 6700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "가핀스키라고", "start_ms": 7150, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 7900, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "요게", "start_ms": 8200, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "강의 위상을 갖추고 있기 때문이라며 이를 대대적으로 극찬하고 나섰는데요 (French sentence)",
"speaker": "mixed",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "강의", "start_ms": 100, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "위상을", "start_ms": 400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "갖추고", "start_ms": 850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "있기", "start_ms": 1300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "때문이라며", "start_ms": 1650, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "이를", "start_ms": 2350, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "대대적으로", "start_ms": 2650, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "극찬하고", "start_ms": 3300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "나섰는데요", "start_ms": 3850, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 8,
"transcript": "현재 유리항아리 형태 생산물량 전부가 소진된 상황입니다 대량으로 쌓아두고 판매하는 제품이 아님에도 제 개인 이메일은 물론 심지어 연구소 전화까지",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "현재", "start_ms": 120, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "유리항아리", "start_ms": 520, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "형태", "start_ms": 1150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "생산물량", "start_ms": 1500, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "전부가", "start_ms": 2100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "소진된", "start_ms": 2500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "상황입니다", "start_ms": 2950, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "대량으로", "start_ms": 3700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "쌓아두고", "start_ms": 4250, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "판매하는", "start_ms": 4800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "제품이", "start_ms": 5300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "아님에도", "start_ms": 5700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "제", "start_ms": 6350, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "개인", "start_ms": 6550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이메일은", "start_ms": 6900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "물론", "start_ms": 7450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "심지어", "start_ms": 7800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "연구소", "start_ms": 8250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "전화까지", "start_ms": 8700, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "라고 말씀드렸었습니다 그리고 여기를 보시면 눌림이 나와주는 타이밍에 노란색에서 초록색으로 바뀌었습니다 시장이 불안할 때는 지",
"speaker": "speaker_5",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "라고", "start_ms": 100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "말씀드렸었습니다", "start_ms": 350, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 1350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "여기를", "start_ms": 1750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "보시면", "start_ms": 2150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "눌림이", "start_ms": 2700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "나와주는", "start_ms": 3150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "타이밍에", "start_ms": 3650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "노란색에서", "start_ms": 4150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "초록색으로", "start_ms": 4800, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "바뀌었습니다", "start_ms": 5400, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "시장이", "start_ms": 6300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "불안할", "start_ms": 6700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "때는", "start_ms": 7150, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "지", "start_ms": 7500, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "아 아뇨 친구 와 이거 한잔해야 돼 진짜 이거 만들다가 조금 쏟았잖아요 근데 버리기엔 어디 남기기 아깝더",
"speaker": "speaker_6",
"confidence": 0.91,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "아", "start_ms": 80, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "아뇨", "start_ms": 350, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "친구", "start_ms": 750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "와", "start_ms": 1150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이거", "start_ms": 1350, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "한잔해야", "start_ms": 1650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "돼", "start_ms": 2200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 2500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이거", "start_ms": 2850, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "만들다가", "start_ms": 3150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 3700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "쏟았잖아요", "start_ms": 4050, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 4800, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "버리기엔", "start_ms": 5100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "어디", "start_ms": 5650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "남기기", "start_ms": 6000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "아깝더", "start_ms": 6500, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "에서 가져오는 식이에요 이 과정에서 물론 AI의 비중이 엄청나게 커집니다 그러나 영업이라는 역할 자체는 사라지지 않는다는 거예요 자 가령 AI가 없던 시대에 직원 열 명이 있는 회사가 있다고 쳐",
"speaker": "speaker_7",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "에서", "start_ms": 110, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "가져오는", "start_ms": 350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "식이에요", "start_ms": 850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 1400, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "과정에서", "start_ms": 1600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "물론", "start_ms": 2150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "AI의", "start_ms": 2550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "비중이", "start_ms": 2950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "엄청나게", "start_ms": 3400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "커집니다", "start_ms": 3950, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "그러나", "start_ms": 4600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "영업이라는", "start_ms": 5050, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "역할", "start_ms": 5700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "자체는", "start_ms": 6050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "사라지지", "start_ms": 6500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "않는다는", "start_ms": 7050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 7600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "자", "start_ms": 8100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가령", "start_ms": 8300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "AI가", "start_ms": 8700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "없던", "start_ms": 9150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "시대에", "start_ms": 9500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "직원", "start_ms": 9950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "열", "start_ms": 10300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "명이", "start_ms": 10500, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 10800, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "회사가", "start_ms": 11100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "있다고", "start_ms": 11550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "쳐", "start_ms": 12000, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "만들어버리면서 앞으로 전진해요 그렇게 쇄빙선은 얼어붙은 바다를 부수고 스스로 뱃길을 만들어내죠 어 부엉이형 이거",
"speaker": "speaker_8",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "만들어버리면서", "start_ms": 100, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "앞으로", "start_ms": 950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "전진해요", "start_ms": 1350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "그렇게", "start_ms": 2000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "쇄빙선은", "start_ms": 2450, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "얼어붙은", "start_ms": 3050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "바다를", "start_ms": 3550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "부수고", "start_ms": 4000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "스스로", "start_ms": 4450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "뱃길을", "start_ms": 4900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "만들어내죠", "start_ms": 5350, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "어", "start_ms": 6150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "부엉이형", "start_ms": 6400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이거", "start_ms": 6950, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 14,
"transcript": "어 비주얼이 너무 예쁜데요 딱 한 5분도가 맞추면 될 것 같아 진짜 야 이거 보세요",
"speaker": "speaker_6",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "어", "start_ms": 90, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "비주얼이", "start_ms": 300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 800, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "예쁜데요", "start_ms": 1150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "딱", "start_ms": 1750, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 1950, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "5분도가", "start_ms": 2150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "맞추면", "start_ms": 2700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "될", "start_ms": 3150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 3350, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "같아", "start_ms": 3500, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 3850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "야", "start_ms": 4300, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "이거", "start_ms": 4550, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "보세요", "start_ms": 4850, "dur_ms": 400, "phase_lr_deg": 0.0}
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
"transcript": "일면 방패막이 역할인 셈입니다 결국 그것은 한국이 아니라 미국을 위한 배치입니다 둘째 한국은 이미 북한의 재래식 도발을 스스로 완벽하게 방어하고도 남을 만큼의 압도적인 국방력과 자주 국",
"speaker": "speaker_9",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "일면", "start_ms": 110, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "방패막이", "start_ms": 450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "역할인", "start_ms": 1000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "셈입니다", "start_ms": 1450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "결국", "start_ms": 2100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그것은", "start_ms": 2500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "한국이", "start_ms": 2950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 3400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "미국을", "start_ms": 3800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "위한", "start_ms": 4250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "배치입니다", "start_ms": 4600, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "둘째", "start_ms": 5350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "한국은", "start_ms": 5850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이미", "start_ms": 6350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "북한의", "start_ms": 6700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "재래식", "start_ms": 7150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "도발을", "start_ms": 7650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "스스로", "start_ms": 8100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "완벽하게", "start_ms": 8550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "방어하고도", "start_ms": 9100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "남을", "start_ms": 9750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "만큼의", "start_ms": 10100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "압도적인", "start_ms": 10550, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "국방력과", "start_ms": 11150, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "자주", "start_ms": 11750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "국", "start_ms": 12100, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "은 조종한다 그리고 에이전트는 실행한다 여기서 조종이 바로 하네스입니다 고삐를 잡고 방향을 정하고 울타리를 세우는 게 바로 저희가 조종하는 방법 즉 하네스",
"speaker": "speaker_10",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "은", "start_ms": 100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "조종한다", "start_ms": 300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "에이전트는", "start_ms": 1300, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "실행한다", "start_ms": 1950, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "여기서", "start_ms": 2600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "조종이", "start_ms": 3000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 3450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "하네스입니다", "start_ms": 3800, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "고삐를", "start_ms": 4650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "잡고", "start_ms": 5100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "방향을", "start_ms": 5450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "정하고", "start_ms": 5900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "울타리를", "start_ms": 6350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "세우는", "start_ms": 6850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 7300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 7500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "저희가", "start_ms": 7850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "조종하는", "start_ms": 8250, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "방법", "start_ms": 8800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "즉", "start_ms": 9250, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "하네스", "start_ms": 9500, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "포커스 양보가 필요합니다",
"speaker": "speaker_11",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "포커스", "start_ms": 50, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "양보가", "start_ms": 550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "필요합니다", "start_ms": 1000, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 20,
"transcript": "에 가서 우리 도지사 후보나 도의원 후보들이 함께하는데 의전을 해야 될 거 아니에요 당대표 오셨으면은 그 사진 한 장이 남았을 때 본인들한테 미치는 영향이나 이런 것들이 비춰봤을 때 이분들이 저한테 그런 말씀을 주시는데 이분들도",
"speaker": "speaker_12",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "에", "start_ms": 130, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가서", "start_ms": 330, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "우리", "start_ms": 700, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "도지사", "start_ms": 1000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "후보나", "start_ms": 1450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "도의원", "start_ms": 1900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "후보들이", "start_ms": 2350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "함께하는데", "start_ms": 2900, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "의전을", "start_ms": 3550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "해야", "start_ms": 4000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "될", "start_ms": 4350, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 4550, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "아니에요", "start_ms": 4700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "당대표", "start_ms": 5150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "오셨으면은", "start_ms": 5650, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 6350, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "사진", "start_ms": 6550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 6900, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "장이", "start_ms": 7100, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "남았을", "start_ms": 7400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 7850, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "본인들한테", "start_ms": 8100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "미치는", "start_ms": 8750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "영향이나", "start_ms": 9200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 9750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "것들이", "start_ms": 10100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "비춰봤을", "start_ms": 10550, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 11150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "이분들이", "start_ms": 11400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "저한테", "start_ms": 11950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "그런", "start_ms": 12400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "말씀을", "start_ms": 12750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "주시는데", "start_ms": 13200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이분들도", "start_ms": 13750, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "고추장은 듬뿍 맛있는 고추장에다가 여보 돼지를 볶아야 돼요 우와 이거 보세요",
"speaker": "speaker_6",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "고추장은", "start_ms": 110, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "듬뿍", "start_ms": 610, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "맛있는", "start_ms": 960, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "고추장에다가", "start_ms": 1410, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "여보", "start_ms": 2160, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "돼지를", "start_ms": 2460, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "볶아야", "start_ms": 2910, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "돼요", "start_ms": 3360, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "우와", "start_ms": 3660, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "이거", "start_ms": 3960, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "보세요", "start_ms": 4260, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "수출 중고도 요격 체계 천궁 2는 이란의 보복 공격이 시작되자 곧장 실전에 투입됐습니다 요격 성공률 96%의 성능이 확인되자 UAE가 요격탄의 이",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "수출", "start_ms": 120, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "중고도", "start_ms": 470, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "요격", "start_ms": 920, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "체계", "start_ms": 1270, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "천궁", "start_ms": 1620, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "2는", "start_ms": 1970, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "이란의", "start_ms": 2270, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "보복", "start_ms": 2670, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "공격이", "start_ms": 3020, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "시작되자", "start_ms": 3520, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "곧장", "start_ms": 4070, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "실전에", "start_ms": 4420, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "투입됐습니다", "start_ms": 4870, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "요격", "start_ms": 5620, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "성공률", "start_ms": 5970, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "96%의", "start_ms": 6470, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "성능이", "start_ms": 7070, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "확인되자", "start_ms": 7520, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "UAE가", "start_ms": 8070, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "요격탄의", "start_ms": 8570, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 9170, "dur_ms": 150, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 11:19:28*
