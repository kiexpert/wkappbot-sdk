# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=30288  time=03:29:06  uptime=7790s  mem=462MB  system_processes=363"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260404_032537.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 632-623-607-167-751-137-561-613-510-501-653-136-136-136-130-103-106-176-612-152-167-136-216-160-612-710-150-673-103-136-316-103-103-310-136-132-514-605-165-516-103-103-136-102-1_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 213-236-162-326-316-162-625-126-126-216-512-765-312-136-132-316-163-136-163-612-631-362-623-612-162-135-152-136-126-126-126-126-236-632-615-312-132-236-326-125-263-213-326-216-2_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 612-621-167-176-164-163-162-362-263-236-625-672-261-132-362-673-162-623-263-672-623-263-260-206-267-206-203-627-126-635-657-657-675-657-102-621-206-126-201-261-675-675-601-621-2_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 310-136-103-361-163-132-103-136-613-613-132-132-163-361-136-163-103-670-160-160-617-657-675-657-670-567-657-607-756-675-675-576-765-765-765-163-671-362-316-136-312-567-617-615-6_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 173 361-103-165-156-167-162-103-653-136-157-165-102-175-162-106-162-617-162-163-105-150-176-571-675-106-106-517-127-160-167-106-613-163-167-160-610-610-106-165-617-617-163-356-4_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 136-312-321-367-367-673-367-130-102-132-123-102-120-321-310-142-326-321-130-160-610-361-136-126-316-310-103-310-302-631-362-613-307-306-632-627-612-617-672-632-672-627-312-103-5_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 351-235-253-352-325-325-532-350-352-250-213-325-325-325-250-352-352-325-325-350-321-321-160-260-350-653-536-576-253-357-312-315-136-156-135-152-231-321-135-235-257-231-325-235-2_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 123-312-321 317-231 761-765-362-267-267-672-716-736-762-720-276-627-726-726-762-675-657-657-205-257-762-706-276-726-765-762-576-576-675-756-765-752-765-765-765-756-762-536-132-1_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 267-627-267-627-267-627-267-267-261-267-267-627-621-267-261-627-261-276-267-267-261-627-267-627-261-267-627-267-627-267-267-267-627-267-267-267-267-267-267-267-267-627-627-267-2_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 153-316-126-675-635-653-316-652-316 257-725-567-526-105-176-106-176-673-160-176-316-310-163-137-137-361-612-162-126-130-163-132-157-621-625-130-316-126-125-103-106-106-263-312-1_V.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 670-365-456-506-257-514-571-251-372-503-574-517-175-176-260-307 267-267-276-267-762-752-715-176-713-173-107-107-176-136-316-361-263-276-751-756-574-754-760-726-762-136-132-136-1_V.mp3)
  Segment 12: 17336ms ~ 22772ms (file: 675-312-132-761-621-675-163-103-102-527-517-756-732-276-276-267-321-126-123-276-263-263-627-236-231-123-213-261-261-263-267-723-312-132-261-123-231-276-217-276-725-723-372-276-7_W.mp3)
  Segment 13: 23772ms ~ 24348ms (file: 107-106-167-362-316-263-631-610-160-610-657-610-163-312-103-136-561-657-265-126-103-301-136-137-134-574-136-170-561 136-136-170-167-516-103-130-103-103-132-136-136-123-617-105-1_V.mp3)
  Segment 14: 25348ms ~ 25924ms (file: 126-106-620-260-106-103-673-617-671-761-761-176-167-136-130-136-106-167-610-106-106-107-620-627-756-623-620-675-627-632-623-106-231-302-731-107-130-610-607-602-106-672-620-260-2_W.mp3)
  Segment 15: 26924ms ~ 27500ms (file: 623-562-216-612-612-562-612-216-120-312-321-162-261-162-675-162-163-163-312-236-312-631-621-612-162-621-362-132-163-231-326-326-326-162-261-361-316-213-132-263-263-267-362-231-1_W.mp3)
  Segment 16: 28500ms ~ 29076ms (file: 657-576-572-576-754-754-576-567-576-657-675-657-657-563-576-567-576-567-567-657-567-657-576-567-657-576-657-567-156-715-567-657-657-562-657-675-756-576-675-672-367-675-267-750-6_W.mp3)
  Segment 17: 30076ms ~ 30652ms (file: 762-670-136-175-162-716-137-132-126-127-123-127-162-162-123-132 206-201-102-123-203-213-261-607-627-267-126-126-316-503-570-517-351-123-321-132-325-613-362-367-623-310-630-603-6_V.mp3)
  Segment 18: 31652ms ~ 32228ms (file: 752-752 132-671-670-627 761-621-612-102-107-613-301-301-236-572-752-607-627-302-630-630-623-126-130-132-123-623-302-302-136-361-213-321-103_V.mp3)
  Segment 19: 33228ms ~ 33804ms (file: 572-357-625-326-267-625-623-672-625-362-623-263-326-362-263-623-356-523-356-362-635-625-367-632-236-367-327-263-623-623-627-632-361-362-312-632-263-632-623-326-623-362-361-236-3_W.mp3)
  Segment 20: 34804ms ~ 35380ms (file: 607-670-671-567-132-560-657 321-236-263-260-263-236-236-312-231-260-541-310-316-320-231-102-123-162-261-610-123-123-513-167-163-167-162-162-136-123-132-657-675-672-621-312-361-3_W.mp3)
  Segment 21: 36380ms ~ 36956ms (file: 627-576-657-672-267 672-627-621-267_W.mp3)
  Segment 22: 37956ms ~ 38532ms (file: 217-275-175-576 715-713-371-173-713-701-107-137-275-257-127-321-215-125-526-127 235-312-725-623-203-502-273-326-312-502-765-631-537-217-362-573-325-517-507-327-207-265-627-276-2_V.mp3)
  Segment 23: 39532ms ~ 40108ms (file: 312-136-301-130-103-236-236-326-236-362-620-620-627-632-132-123-301-132-216-126-312-132-123-132-132-637-126-216-602-360-316-230-230-230-321-631-106-316-160-163-632-260-326-632-1_V.mp3)
  Segment 24: 41108ms ~ 41684ms (file: 567-657-576-567-576-567-567-657-567-657-675-167-657-576-651-651-156-561-167-651-167-165-576-615-567-651-671-756-567-156-165-567-165-573-567-567-615-651-563-567-156-516-567-567-5_V.mp3)
  Segment 25: 42684ms ~ 43260ms (file: 765-675-760-213-567-263-163-316-576 317 316-312-316-512-106-612-123-103-136-362-163-162-173-276-162-126-123-132-130-210-271-261-172-372-731-671-760-203-612-216-675-672-327-672-6_W.mp3)
  Segment 26: 44260ms ~ 44836ms (file: 276-253-321-574-574-570-163-263-267-267-260-260-702-312-301-312-312-312-310-326-320-132-176-167-136-315-315-316-540-547-574-574-352-356-136-163-267-627-236-230-320-203-627-206-2_V.mp3)
  Segment 27: 45836ms ~ 47744ms (file: 106-136 132-321-160-620-602-103-126-260-126-602-607-106-163-106-106-106-106-610-610-601-103-326-126-156-167-132-504-570-574-574-576-576-132-136-312-310-132-123-162-173-130-102-1_V.mp3)
  Segment 28: 48744ms ~ 49320ms (file: 137-167-362-517-365-165-103-132-123-130-132-312-106-136-136 165-103-751-167-751-316-136-106-167-136-136-567-136-160-103-574-136-671-612-167-210-162-167-316-162-152-160-132-130-1_V.mp3)
  Segment 29: 50320ms ~ 50896ms (file: 571 621-167-160-160-106-102 137-137-216-105-156-601-130-150-165-156-103-132-657-263-621-306-136-367-176-136-106-637-136-361-130-167-132-172-176-106-362-261-163-637-123-103-132-1_V.mp3)
  Segment 30: 51896ms ~ 52472ms (file: 306-317-637-601-162-163-236-612-213-162-162-672-267-267-103-514-517-574-563-326-236-132-310-762-672-267-362-627-132 762-752-752-136-126-627-127-312-132-312-123_W.mp3)
  Segment 31: 53472ms ~ 54048ms (file: 750-756-760-620-620-103-132-132-321-302-201-230-320-236-326-302-230-617-312-132-675-675-675-316-136-135-153-132-143-132-134 736-753-310-321-302-320 574-574-302-312-623-162-621-1_V.mp3)
  Segment 32: 55048ms ~ 55624ms (file: 564-530-573-132-120-216-231-263-263-657-162-651-312-574-650-175-125-750-576-510-574-756-576-657-746-760-670-756-615-576-167-627-267-263-260-602-126-326-256-213-123-321-312-576-1_W.mp3)
  Segment 33: 56624ms ~ 57200ms (file: 231-321-357-375-372-725-702-273-132-173-327-236-267-203-325-672-130-157-756-573-576-537-572-576-576-576-576-573-236 316-263-762-276-267-245-276-576-567-576-574-547-310-326-276-5_W.mp3)
  Segment 34: 58200ms ~ 58776ms (file: 106-765-107-612-561-106-617-617-261-160-612-176-130-513-106-102-136-167-103-106-125-162-132-103-130-103-130-761-362-163-176-631-176-126-623-627_V.mp3)
  Segment 35: 59776ms ~ 60352ms (file: 167-163-136-132-231-263-612-312-362-621-312-127-107-123-236-162-361-163-105-163_V.mp3)
  Segment 36: 61352ms ~ 67580ms (file: 752-751-517-765-573-310-132-173-357-135-756-175-103-231-236-137-320-372-137-715-261-267-132-236-230-251-261-217-132-120-217-320-301-137-213-201-231-231-132-123-362-231-361-172-7_W.mp3)
  Segment 37: 68580ms ~ 69156ms (file: 165-167-263-236-267-126-132-671-213-527-153 136-106-162-216-216-275-217-132-623-326-213 163-617-135-130-167-130-106-657-163-103-623-362-672-672-620-652-326-625-136-103-136-135-1_W.mp3)
  Segment 38: 70156ms ~ 70732ms (file: 613-106-127-103-216-206-206-260-260-572-760-326-167-617-107-126-210-237-201-250-230-203-206-203-102-162-620-623-263-316-301-321-320-302-203-237-260-236-230-320-302-310-765-157-3_W.mp3)
  Segment 39: 71732ms ~ 72308ms (file: 260-265-267-326-302-532-105-210-102-163-617-671-671-671-163-617-367-127-325-367-176-317-173-715-735-675-756-675-756-756-310-301-610-106-627-123-106-105-621-263-216-136-165-165-1_W.mp3)
  Segment 40: 73308ms ~ 73884ms (file: 136-106-176-301-761-716-736 360-620-750 620-260 160-126-102-621-602-620-260-620-620 576-576-576-657_W.mp3)
  Segment 41: 74884ms ~ 75460ms (file: 325-352-325-235-327-253-532-524-532-523-357-327-372-321-376-263-267-627-273-723-273-237-236-217-216-326-236-213-231-321-327-312-217-213-237-217-213-213-132-213-210-231-321-215-2_W.mp3)
  Segment 42: 76460ms ~ 77036ms (file: 150-316-126-102-176-621-671-267-236-136-162-326-613-267-126-263-302-230-102-103-162-306-136-175-126-715-675-761-571-562-607-673-316-312-521-137-143-103-163-103 132_V.mp3)
  Segment 43: 78036ms ~ 78612ms (file: 613-165 302-132-162-213-236-136-263-623-126-102-716-756-756-756-756-576-765-321-132-231-312-627-612-126-326-236-136-157 136-165-762-631-317-132-102-102-103-132-163-130-310-103-1_W.mp3)
  Segment 44: 79612ms ~ 87676ms (file: 672-263-726-756-765-130-762-267-267-236-132-762-672-267-765-316-276-276-321-372-132-321-236-261-267-312-765-276-236-213-321-735-267-267-276-102-751-170-210-120-326-756-217-765-7_W.mp3)
  Segment 45: 88676ms ~ 98360ms (file: 132-312-102-123-123-201-201-132-130-201-123-132-260-132-762-126-267-132-137-123-312-260-206-612-260-627-627-261-265-102-106-102-163-132-201-261-102-175-310-310-753-132-126-123-1_V.mp3)
  Segment 46: 99360ms ~ 99936ms (file: 572-132-135-312-126-167-165-132-163-617-602-326-236-231-237-510-510-201-362-612-216-261-617-103-103-306-230-263-321-231-263-230-260-203-201-120-132-123-312-132-130-316-316-312-1_V.mp3)
  Segment 47: 100936ms ~ 101512ms (file: 176 635 672-576-106-261-236-136-623-673-120-321-231-312-132-123-312-361-576-567-153-312-123-136-123-132-126-136-562-130-132-621-673-671-675-651-765-265-627-612-157-273-126-216-2_W.mp3)
  Segment 48: 102512ms ~ 103088ms (file: 576-375-375-103 136-132-136-103-627-326-361-317-126-107 207-206-207-207-267-672-726-507-706-175-567-725-261-361-321 125-164-312-317-613-612-316-136-310-103 612-321_V.mp3)
  Segment 49: 104088ms ~ 104664ms (file: 613-123-132-136-163-163-617-617-617-671-672-627-136-167-163-167-136-162-261-265-236-617-610-617-617-136-326-321-326-263-263-263-236-326-265-235-231-136-613-631-617-671-617-670-7_W.mp3)
  Segment 50: 105664ms ~ 110524ms (file: 257-620-263-301-567-236-276-216-215-261-123-102-123-623-102-261-321-230-320-102-567-102-612-105-526-572-257-256-203-312-126-213-163-102-102-120-127-135-235-230-123-231-210-163-2_W.mp3)
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
> **Time**: 2026-04-04 03:30:39
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "뒤쪽으로 밀릴 만큼 주방은 비상입니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "뒤쪽으로", "start_ms": 20, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "밀릴", "start_ms": 165, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "만큼", "start_ms": 250, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "주방은", "start_ms": 325, "dur_ms": 105, "phase_lr_deg": 0.0},
{"word": "비상입니다", "start_ms": 435, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "요리를 하다 말고 급히 홀로 향하는",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "요리를", "start_ms": 30, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "하다", "start_ms": 145, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "말고", "start_ms": 210, "dur_ms": 75, "phase_lr_deg": 0.0},
{"word": "급히", "start_ms": 290, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "홀로", "start_ms": 365, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "향하는", "start_ms": 450, "dur_ms": 115, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "비어있는 홀",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "비어있는", "start_ms": 40, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "홀", "start_ms": 285, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "자 이제 준비가 다 됐으니까 식사 하세요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "자", "start_ms": 50, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 95, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "준비가", "start_ms": 160, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 255, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "됐으니까", "start_ms": 300, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "식사", "start_ms": 415, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "하세요", "start_ms": 490, "dur_ms": 75, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "기준이 잡히면 시장이 완전히 다르게 보이게 됩니다",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "기준이", "start_ms": 35, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "잡히면", "start_ms": 140, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "시장이", "start_ms": 235, "dur_ms": 95, "phase_lr_deg": 0.0},
{"word": "완전히", "start_ms": 335, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "다르게", "start_ms": 440, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "보이게", "start_ms": 515, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "됩니다", "start_ms": 590, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "지금까지 하승훈의 주식 투자였습니다 감사합니다",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "지금까지", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "하승훈의", "start_ms": 165, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "주식", "start_ms": 270, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "투자였습니다", "start_ms": 335, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 490, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "돌고래가 일단 일억을 찍고 이 충청도 애들이",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "돌고래가", "start_ms": 45, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "일단", "start_ms": 170, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "일억을", "start_ms": 245, "dur_ms": 85, "phase_lr_deg": 0.0},
{"word": "찍고", "start_ms": 335, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 410, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "충청도", "start_ms": 435, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "애들이", "start_ms": 520, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "어깨뽕이 올라서 과감히 일단 일억을 날렸습니다",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "어깨뽕이", "start_ms": 30, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "올라서", "start_ms": 165, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "과감히", "start_ms": 260, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "일단", "start_ms": 345, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "일억을", "start_ms": 410, "dur_ms": 75, "phase_lr_deg": 0.0},
{"word": "날렸습니다", "start_ms": 490, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 10,
"transcript": "십 년 전인 것을 감안을 한다면 현재 스탠퍼드 출신인 샘 올트먼부터",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "십", "start_ms": 45, "dur_ms": 35, "phase_lr_deg": 0.0},
{"word": "년", "start_ms": 85, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "전인", "start_ms": 120, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "것을", "start_ms": 185, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "감안을", "start_ms": 240, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "한다면", "start_ms": 325, "dur_ms": 75, "phase_lr_deg": 0.0},
{"word": "현재", "start_ms": 405, "dur_ms": 65, "phase_lr_deg": 0.0},
{"word": "스탠퍼드", "start_ms": 475, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "출신인", "start_ms": 570, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "샘", "start_ms": 645, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "올트먼부터", "start_ms": 690, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "앤스로픽 창업자인 다리오 아모데이 등 걸출한 천재들이",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "앤스로픽", "start_ms": 35, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "창업자인", "start_ms": 150, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "다리오", "start_ms": 245, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "아모데이", "start_ms": 320, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "등", "start_ms": 405, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "걸출한", "start_ms": 440, "dur_ms": 85, "phase_lr_deg": 0.0},
{"word": "천재들이", "start_ms": 530, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "안녕하세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "안녕", "start_ms": 100, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "하세요", "start_ms": 290, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "만나서 반갑습니다",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "만나서", "start_ms": 50, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 280, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "이것은 무엇입니까?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "이것은", "start_ms": 40, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "무엇입니까?", "start_ms": 260, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "그것은 책이에요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "그것은", "start_ms": 55, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "책이에요", "start_ms": 285, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "지금 몇 시예요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "지금", "start_ms": 60, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "몇", "start_ms": 250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "시예요?", "start_ms": 360, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "세 시 삼십 분입니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "세", "start_ms": 45, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "시", "start_ms": 135, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "삼십", "start_ms": 225, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "분입니다", "start_ms": 385, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "어디에 가세요?",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "어디에", "start_ms": 50, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "가세요?", "start_ms": 300, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "학교에 가요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "학교에", "start_ms": 40, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "가요", "start_ms": 330, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "오늘 날씨가 좋네요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 65, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 255, "dur_ms": 170, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 435, "dur_ms": 120, "phase_lr_deg": 0.0}
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
"transcript": "배가 고파요",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "배가", "start_ms": 55, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "고파요", "start_ms": 225, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "같이 밥 먹을까요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "같이", "start_ms": 45, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "밥", "start_ms": 195, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "먹을까요?", "start_ms": 305, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "좋아요, 갑시다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "좋아요,", "start_ms": 50, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "갑시다", "start_ms": 280, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "이거 얼마예요?",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "이거", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "얼마예요?", "start_ms": 230, "dur_ms": 260, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "만원입니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "만원", "start_ms": 60, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "입니다", "start_ms": 290, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "What is your name?",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "What", "start_ms": 30, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "your", "start_ms": 250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "name?", "start_ms": 360, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "제 이름은 민수입니다",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "제", "start_ms": 45, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "이름은", "start_ms": 135, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "민수입니다", "start_ms": 345, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "한국어 공부가 재미있어요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 50, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "공부가", "start_ms": 220, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "재미있어요", "start_ms": 370, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "정말요? 다행이네요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "정말요?", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "다행이네요", "start_ms": 230, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "Excuse me, where is the toilet?",
"speaker": "speaker_4",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "Excuse", "start_ms": 35, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "me,", "start_ms": 195, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "where", "start_ms": 285, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 395, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 455, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "toilet?", "start_ms": 505, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "화장실은 저기에 있어요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "화장실은", "start_ms": 40, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "저기에", "start_ms": 270, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "감사합니다",
"speaker": "speaker_4",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "감사", "start_ms": 60, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "합니다", "start_ms": 320, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "천만에요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "천만", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "에요", "start_ms": 260, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "도와드릴까요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "도와", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "드릴까요?", "start_ms": 230, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "네, 부탁합니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 55, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "부탁합니다", "start_ms": 185, "dur_ms": 360, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "조금 깎아 주세요",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "조금", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "깎아", "start_ms": 210, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 370, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "안 돼요, 정가예요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "안", "start_ms": 45, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "돼요,", "start_ms": 135, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "정가예요", "start_ms": 285, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "알겠습니다",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "알겠", "start_ms": 65, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "습니다", "start_ms": 305, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "다음에 또 오세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 40, "dur_ms": 170, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 220, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "오세요", "start_ms": 310, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "안녕히 계세요",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 50, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "계세요", "start_ms": 290, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "안녕히 가세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 45, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 295, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "취미가 뭐예요?",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "취미가", "start_ms": 60, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "뭐예요?", "start_ms": 290, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "저는 영화 보는 것을 좋아해요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 80, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "영화", "start_ms": 240, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "보는", "start_ms": 390, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "것을", "start_ms": 530, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "좋아해요", "start_ms": 660, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "어떤 영화를 좋아하세요?",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "어떤", "start_ms": 50, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "영화를", "start_ms": 190, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "좋아하세요?", "start_ms": 360, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "액션 영화를 좋아해요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "액션", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "영화를", "start_ms": 190, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "좋아해요", "start_ms": 350, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "가장 좋아하는 배우는 누구예요?",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "가장", "start_ms": 55, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "좋아하는", "start_ms": 165, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "배우는", "start_ms": 325, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "누구예요?", "start_ms": 455, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "송강호를 좋아해요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "송강호를", "start_ms": 45, "dur_ms": 260, "phase_lr_deg": 0.0},
{"word": "좋아해요", "start_ms": 315, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "생일이 언제예요?",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "생일이", "start_ms": 55, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "언제예요?", "start_ms": 245, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "제 생일은 오월 삼일입니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "제", "start_ms": 65, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "생일은", "start_ms": 135, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "오월", "start_ms": 285, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "삼일입니다", "start_ms": 395, "dur_ms": 170, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-04 03:30:39*
