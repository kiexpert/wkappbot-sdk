# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=18336  time=08:41:59  uptime=17841s  mem=119MB  system_processes=406"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_084051.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 205-215-752-562-567-570-532-762-752-576-627-675-576-326-367-506-135-276-375-576-574-627-572-623-573-235-537-270-675-173-153-267-756-361-275-567-526-265-564-657-261-351-572-265-7_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 752-271-162-721-276-127-135-735-572-570-352-756 263-537-567-537-236-532-573-635-756-213-756-136-751-265-135-570-267-671-236-521-236-263-371-317-207-765_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 312-517-653-157-163-573-765-673-567-576-754-574-576-567-576-756-576-576-574-756-657-765-675-576-657-756-756-517-576-576-562-574-567_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 265-326-327-326-137-137-317 132-136-351-735-135-176-167-137-612-162-132-675-716-671-672-612-160-610-216-126-216-261-216-126-162-261-615-517-576-567-567-576-765-527-527-521-527-2_W.mp3)
  Segment 5: 6304ms ~ 13324ms (file: 751-261-763-576-756-103-176-753-310-301-315-321-312-132-317-671-765-316-312-132-760-261-576-756-756-267-263-320-320-207-213-725-765-675-317-756-576-312-103-231-607-705-765-705-1_W.mp3)
  Segment 6: 14324ms ~ 14900ms (file: 750-235-206-276-231-705-637-270-627-576-257-261-507-756-657-527-507-256-265-372-137-362-672-263-603-615-176-657-607-163-135-156-637-671-617-675-760-657-367-316-617-326-263-567-3_W.mp3)
  Segment 7: 15900ms ~ 16476ms (file: 136-136-135-137-137-135-136-132-135-103-136-132-137-132-136-137-134-135-173-175-173-715-175-175-165-156-173-217-123-125-157 143-126-123-135-137-136-157-571-517-157_V.mp3)
  Segment 8: 17476ms ~ 18052ms (file: 675-763-765-763-765-167-371-317-137-317-137-716-176-761-761-765-763-765-756-765-765-765-765-765-761-765-765-763-713-763-735-715-753-756-765-736-731-756-375-375-371-375-731-751-2_W.mp3)
  Segment 9: 19052ms ~ 19628ms (file: 157-153-157-157-150-105-157-157-157-153-150-715-135-715-310-153-571-170-106-735-673-675-673-657-375-675-136-137-130-135-135-657-536-573-513-572-513-135-157-153-152-271-125-175-1_W.mp3)
  Segment 10: 20628ms ~ 21204ms (file: 571-571-571-574-570-571-574-570-571 574-571-574-574-574-574-574-576-571 517-576-571-571-517-517-510-517-157-573-571-571-571-572-175-571-571-571-571-751-756-753-571-573-572 517-5_W.mp3)
  Segment 11: 22204ms ~ 26524ms (file: 263-302-132-617-236-317-301-367-627-267-627-605-625-103-326-106-516-362-136-130-260-260-302-103-567-576-756-376-326-367-376-367-763-736-376-723-376-371-137-376-367-136-762-276-7_W.mp3)
  Segment 12: 27524ms ~ 34364ms (file: 731-716-132-310-723-270-120-201-210-217-217-123-127-201-312-213-236-312-127-162-106-126-126-127-172-172-312-123-657-273-375-756-175-173-236-231-672-312-231-261-237-207-762-312-3_W.mp3)
  Segment 13: 35364ms ~ 42744ms (file: 250-206-270-527-276-237-263-206-502-276-267-627-572-627-672-205-320-231-756-261-137-206-762-756-760-301-237-607-675-301-201-123-153-726-570-310-203-230-627-321-102-602-213-301-1_W.mp3)
  Segment 14: 43744ms ~ 50908ms (file: 675-762-672-675-176-762-675-765-756-621-612-267-231-672-627-267-236-652-362-725-250-203-267-726-126-621-103-657-321-310-206-206-216-576-526-564-201-205-257-217-267-107-257-123-2_W.mp3)
  Segment 15: 51908ms ~ 52484ms (file: 574-547-574-574-517-157-157-571-571_W.mp3)
  Segment 16: 53484ms ~ 54060ms (file: 571-517-571-572-571-517-517-571-571 570-576-571-517-175-517-751-517-175-571-175-715-571-715-175-751-153-731-510-751-571-517-573-157-517-510-517-570-507-750-571-517-572 517-571-5_V.mp3)
  Segment 17: 55060ms ~ 62548ms (file: 216-276-312-267-267-576-307-301-176-756-276-752-371-310-107-726-263-235-321-310-103-756-276-321-170-761-762-267-267-572-716-106-267-267-752-267-672-107-576-735-753-170-671-607-7_W.mp3)
  Segment 18: 63548ms ~ 64124ms (file: 176-716-176-761-715-761-761-176-761-176-716-763-761-761-761 750-750-107-715-713-715-176-176-170-176-716-715-716-176 751-517-175-705-710-701-170-176-170-716-761-716-176-716-716-7_V.mp3)
  Segment 19: 65124ms ~ 72720ms (file: 563-367-675-635-356-305-350-537-756-657-576-635-657-762-657-765-276-261-236-206-162-102-360-176-162-167-762-706-176-230-713-630-370-170-312-762-637-127-173-731-710-631-263-103-1_W.mp3)
  Segment 20: 73720ms ~ 81928ms (file: 321-720-123-267-230-527-756-275-207-276-267-547-206-236-231-230-132-176-627-576-165-721-107-132-627-361-106-617-576-672-671-615-506-365-157-571-536-753-576-756-576-576-657-567-3_W.mp3)
  Segment 21: 82928ms ~ 88544ms (file: 167-213-321-236-215-231-263-215-123-130-312-213-231-312-216-213-276-231-326-213-106-231-263-623-572-256-216-312-236-312-306-610-612-210-612-216-120-620-621-613-126-130-102-123-1_W.mp3)
  Segment 22: 89544ms ~ 104628ms (file: 210-201-123-320-312-302-310-547-201-260-261-201-260-310-132-312-132-326-176-321-137-213-260-236-572-752 275-527-754-765-756-576-107-127-210-216-725-137-725-752-267-217-217-725-7_W.mp3)
  Segment 23: 105628ms ~ 111820ms (file: 123-213-132-132-123-123-213-127-213-123-123-123-123-231-213-321-321-217-231-213-123-123-123-127-123-361-120-127-612-126-756-312-163-163-362-261-317-753-261-612-310-136-236-126-6_W.mp3)
  Segment 24: 112820ms ~ 113396ms (file: 176-173-716-175-157-751-715 135-130-130-157-175-157-175-157-157-517-157-517-571-756-576-675-751-576-657-175-176-751-751-716 103-130-130-137-175-157-175-157-167-751-567-671-167-1_V.mp3)
  Segment 25: 114396ms ~ 114972ms (file: 573-517-357-375-315-156-572-537-136-137-136-567-375-135-137-367-637-736-237-132-726-765-765-765-762_W.mp3)
  Segment 26: 115972ms ~ 116548ms (file: 765-671-576-513-357-176-136-310-310-130-102-170-106-160-675-675-765_W.mp3)
  Segment 27: 117548ms ~ 118124ms (file: 150-153-150-150-107-510-103-105-370-152-126-612-163-653-625-615-615-615-631-615-153-153-517-175-513-175-153-571-167-163-167-163-615-652-576-175-527-125-271-257-276-215-261-216-1_W.mp3)
  Segment 28: 119124ms ~ 119700ms (file: 574-570-571-570-517-571-517-571-157-517-571-751-157-517-571-157-572-715-751-571-571-571 570-571-571-751-571-751-751-517-751-517-753-571-571-571-573-571-517-571-715-571-175-751 5_V.mp3)
  Segment 29: 120700ms ~ 121276ms (file: 574-574-574-574-517-517-157-157-157-517-517-517-517-571-517-517-571-576 576-574-571-571-576-571-571-576-571-571-571-751-571-751-517-157-512-570-751-175-576-517-576-753-301-157-5_W.mp3)
  Segment 30: 122276ms ~ 122852ms (file: 765-765-637-715 761-731-763-713-731-716-763-716-763 132-312-310-173-316-301-103-136-106-130-761-760-716-657_W.mp3)
  Segment 31: 123852ms ~ 124428ms (file: 631-316-176-736-162-761-763-176-173-321-260-735-356-752-751-671-651-316-316-316-157-137-721-716-617-167-574-276-576-576-125-132-712-132-756-756-103-157-125-273-270-712-725-521-3_W.mp3)
  Segment 32: 125428ms ~ 126004ms (file: 567-756-376-753-726-763-367-675-136-637-236-326-653-352-361-316-216-327-263-567-537-357 316-256-576-325-765-256-321_W.mp3)
  Segment 33: 127004ms ~ 133916ms (file: 130-103-106-107-236-236-312-263-625-256-236-123-132-320-321-310-316-267-621-265-527-271-267-216-213-102-213-203-320-273-276-260-713-176-163-237-201-132-120-130-103-631-310-612-1_W.mp3)
  Segment 34: 134916ms ~ 142512ms (file: 132-163-106-620-652-216-261-542-612-261-610-720-207-127-276-671-127-721-176-762-103-671-675-216-257-627-726-726-160-271-627-725-627-752-721-126-132-213-216-201-261-276-267-267-7_W.mp3)
  Segment 35: 143512ms ~ 144088ms (file: 163-123-263-567-673-675-316-652-317-316-162-126-136-132-167-132-126-126-561-132-760-216-132-507-326-132-623-327-726-236-506-267-215-213-127-165-172-517-760 615-576-162-576-752-2_V.mp3)
  Segment 36: 145088ms ~ 145664ms (file: 517-517-153-157-570-571-510-517-517-517-517-517-571-571-517 574-574-574-517-571-157-157-517-157-517-517-507-571-152-751-571 571-510-517-175-517-150-510-510-571-571-517-510-153-1_V.mp3)
  Segment 37: 146664ms ~ 156420ms (file: 231-231-312-725-265-526-276-725-257-170-317-217-107-627-216-213-120-275-752-726-725-372-132-754-132-574-275-276-276-210-130-132-127-762-572-123-276-275-275-203-210-263-261-216-2_W.mp3)
  Segment 38: 157420ms ~ 163540ms (file: 103-273-273-123-621-326-160-261-276-261-326-120-517-125-231-263-106-710-276-126-213-273-257-275-267-235-215-617-671-260-621-261-237-256-102-312-102-536-302-106-320-267-256-267-2_W.mp3)
  Segment 39: 164540ms ~ 173576ms (file: 326-320-103-123-163-725-132-316-312-132-103-103-130-230-310-236-206-765-301-723-210-320-675-756-302-301-617-136-130-230-210-312-301-231-716-327-302-103-230-230-752-103-206-136-3_W.mp3)
  Segment 40: 174576ms ~ 175152ms (file: 135-620-652-507-573-125-672-620-653-736-130-170-670-765-761-675-761-673-637-756-675-765-765-765-163 316_W.mp3)
  Segment 41: 176152ms ~ 182380ms (file: 326-103-621-106-720-137-103-153-310-103-175-627-517-517-672-276-176-163-136-631-762-276-762-102-107-627-162-106-610-106-175-721-162-106-620-621-107-103-761-712-320-162-310-317-3_W.mp3)
  Segment 42: 183380ms ~ 183956ms (file: 316-173-137-376-765-751-761-361-316-310-321-123-675-765-763-763-216_W.mp3)
  Segment 43: 184956ms ~ 193704ms (file: 263-263-236-103-231-310-765-756-576-265-263-725-756-132-267-312-203-326-263-275-752-157-125-267-261-162-132-312-236-751-576-276-756-675-106-637-672-316-231-132-321-327-236-267-6_W_stt_the_ornate_doubt_superhuman_speed_and_ou.mp3)
  Segment 44: 194704ms ~ 195280ms (file: 576-571-574-576-574-573-513-175-574-132-637-765-675-132-173_W.mp3)
  Segment 45: 196280ms ~ 196856ms (file: 312-256-702-623-137-327-237-312-173-673-123-572-162-762-351-376-672-207-750-536-527-765-762-276-705-675-627-675-103-576-132-130-310-132-601-576-726-157-315-763-275-725-762-730-1_V.mp3)
  Segment 46: 197856ms ~ 198432ms (file: 312-761-763-672-763-761-671-576-675-751-567-576-657-567-652-673-621-620 513-615-576-537-356-132-132-532-531-170-316-672-361-637-736-637-612-635-735-357-756-765-720-602-670-637-6_W.mp3)
  Segment 47: 199432ms ~ 200008ms (file: 572-276-670-213-157-765-756-526-257-627-206-570-236-312-267-726-325-236-675-265-235-752-375-325-237-257-256-326-267-572-132-321-376-327-315-135-352_V.mp3)
  Segment 48: 201008ms ~ 201584ms (file: 613-106-261-132-160-312-216-132-162-673-612-675-650-631-736-761-765-176-763-756-765-716-765-523-574-547-574-170-765-756-756-576-765-735-715-357-657-573-367-567-657-567-675-576-7_W.mp3)
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
> **Time**: 2026-04-06 08:43:23
> **AI**: gemini

---

## Response

{
"segment": 1,
"transcript": "안녕하세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "안녕", "start_ms": 110, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "하세요", "start_ms": 330, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "반갑습니다 오늘 날씨가 참 좋네요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "반갑습니다", "start_ms": 80, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 240, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 360, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "참", "start_ms": 490, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 530, "dur_ms": 40, "phase_lr_deg": 0.0}
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
"transcript": "Let's start the lesson",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "Let's", "start_ms": 150, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "start", "start_ms": 260, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "the", "start_ms": 380, "dur_ms": 50, "phase_lr_deg": 5.0},
{"word": "lesson", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 5.0}
]
},
{
"segment": 5,
"transcript": "먼저 단어부터 살펴볼까요? 기초적인 표현들입니다.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "먼저", "start_ms": 100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "단어부터", "start_ms": 260, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "살펴볼까요?", "start_ms": 570, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "기초적인", "start_ms": 950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "표현들입니다.", "start_ms": 1360, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "사과 먹고 싶어요",
"speaker": "speaker_3",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "사과", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": -10.0},
{"word": "먹고", "start_ms": 210, "dur_ms": 120, "phase_lr_deg": -10.0},
{"word": "싶어요", "start_ms": 340, "dur_ms": 200, "phase_lr_deg": -10.0}
]
},
{
"segment": 7,
"transcript": "Apple please",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "Apple", "start_ms": 120, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "please", "start_ms": 330, "dur_ms": 240, "phase_lr_deg": 5.0}
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
"transcript": "이것은 책입니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "이것은", "start_ms": 90, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "책입니다", "start_ms": 300, "dur_ms": 270, "phase_lr_deg": 0.0}
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
"transcript": "Where is the library? 도서관이 어디에요?",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "Where", "start_ms": 140, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 300, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 360, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "library?", "start_ms": 420, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "도서관이", "start_ms": 800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "어디에요?", "start_ms": 1160, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "저기 오른쪽으로 가시면 보일 거예요. 실례합니다.",
"speaker": "speaker_3",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "저기", "start_ms": 200, "dur_ms": 150, "phase_lr_deg": -10.0},
{"word": "오른쪽으로", "start_ms": 360, "dur_ms": 400, "phase_lr_deg": -10.0},
{"word": "가시면", "start_ms": 770, "dur_ms": 250, "phase_lr_deg": -10.0},
{"word": "보일", "start_ms": 1030, "dur_ms": 150, "phase_lr_deg": -10.0},
{"word": "거예요.", "start_ms": 1190, "dur_ms": 200, "phase_lr_deg": -10.0},
{"word": "실례합니다.", "start_ms": 1450, "dur_ms": 400, "phase_lr_deg": -10.0}
]
},
{
"segment": 13,
"transcript": "화장실이 급해요. 도와주실 수 있나요?",
"speaker": "speaker_4",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "화장실이", "start_ms": 110, "dur_ms": 350, "phase_lr_deg": 15.0},
{"word": "급해요.", "start_ms": 470, "dur_ms": 250, "phase_lr_deg": 15.0},
{"word": "도와주실", "start_ms": 800, "dur_ms": 350, "phase_lr_deg": 15.0},
{"word": "수", "start_ms": 1160, "dur_ms": 50, "phase_lr_deg": 15.0},
{"word": "있나요?", "start_ms": 1220, "dur_ms": 200, "phase_lr_deg": 15.0}
]
},
{
"segment": 14,
"transcript": "직진해서 편의점 옆에 있습니다. 찾기 쉬워요.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "직진해서", "start_ms": 130, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "편의점", "start_ms": 550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "옆에", "start_ms": 860, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있습니다.", "start_ms": 1020, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "찾기", "start_ms": 1350, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "쉬워요.", "start_ms": 1510, "dur_ms": 200, "phase_lr_deg": 0.0}
]
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
"transcript": "배가 너무 고파요. 맛있는 식당 추천해주세요.",
"speaker": "speaker_4",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "배가", "start_ms": 180, "dur_ms": 120, "phase_lr_deg": 15.0},
{"word": "너무", "start_ms": 310, "dur_ms": 110, "phase_lr_deg": 15.0},
{"word": "고파요.", "start_ms": 430, "dur_ms": 200, "phase_lr_deg": 15.0},
{"word": "맛있는", "start_ms": 700, "dur_ms": 250, "phase_lr_deg": 15.0},
{"word": "식당", "start_ms": 960, "dur_ms": 150, "phase_lr_deg": 15.0},
{"word": "추천해주세요.", "start_ms": 1120, "dur_ms": 500, "phase_lr_deg": 15.0}
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
"transcript": "근처에 비빔밥 잘하는 집이 있어요. 같이 가실래요?",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "근처에", "start_ms": 120, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "비빔밥", "start_ms": 380, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "잘하는", "start_ms": 640, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "집이", "start_ms": 900, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "있어요.", "start_ms": 1030, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 1250, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "가실래요?", "start_ms": 1380, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "좋아요. 저는 매운 음식을 아주 좋아합니다.",
"speaker": "speaker_4",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "좋아요.", "start_ms": 150, "dur_ms": 200, "phase_lr_deg": 15.0},
{"word": "저는", "start_ms": 400, "dur_ms": 120, "phase_lr_deg": 15.0},
{"word": "매운", "start_ms": 530, "dur_ms": 150, "phase_lr_deg": 15.0},
{"word": "음식을", "start_ms": 690, "dur_ms": 200, "phase_lr_deg": 15.0},
{"word": "아주", "start_ms": 900, "dur_ms": 110, "phase_lr_deg": 15.0},
{"word": "좋아합니다.", "start_ms": 1020, "dur_ms": 400, "phase_lr_deg": 15.0}
]
},
{
"segment": 21,
"transcript": "그럼 비빔밥에 고추장을 듬뿍 넣어 드릴게요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 100, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "비빔밥에", "start_ms": 230, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "고추장을", "start_ms": 540, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "듬뿍", "start_ms": 900, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "넣어", "start_ms": 1060, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "드릴게요.", "start_ms": 1170, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "정말 감사합니다. 한국 음식은 정말 맛있어요. 최고예요!",
"speaker": "speaker_4",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 140, "dur_ms": 150, "phase_lr_deg": 15.0},
{"word": "감사합니다.", "start_ms": 300, "dur_ms": 400, "phase_lr_deg": 15.0},
{"word": "한국", "start_ms": 750, "dur_ms": 150, "phase_lr_deg": 15.0},
{"word": "음식은", "start_ms": 910, "dur_ms": 250, "phase_lr_deg": 15.0},
{"word": "정말", "start_ms": 1170, "dur_ms": 150, "phase_lr_deg": 15.0},
{"word": "맛있어요.", "start_ms": 1330, "dur_ms": 300, "phase_lr_deg": 15.0},
{"word": "최고예요!", "start_ms": 1650, "dur_ms": 300, "phase_lr_deg": 15.0}
]
},
{
"segment": 23,
"transcript": "천만에요. 즐거운 식사 시간이 되길 바랍니다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "천만에요.", "start_ms": 120, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "즐거운", "start_ms": 450, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "식사", "start_ms": 660, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "시간이", "start_ms": 820, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "되길", "start_ms": 1030, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "바랍니다.", "start_ms": 1160, "dur_ms": 300, "phase_lr_deg": 0.0}
]
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
"transcript": "다음에 또 봐요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 260, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "봐요", "start_ms": 320, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "안녕히 계세요",
"speaker": "speaker_4",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 80, "dur_ms": 150, "phase_lr_deg": 15.0},
{"word": "계세요", "start_ms": 240, "dur_ms": 150, "phase_lr_deg": 15.0}
]
},
{
"segment": 27,
"transcript": "Goodbye everyone",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "Goodbye", "start_ms": 110, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "everyone", "start_ms": 320, "dur_ms": 250, "phase_lr_deg": 5.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 30,
"transcript": "실력이 많이 늘었네요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "실력이", "start_ms": 130, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 340, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "늘었네요", "start_ms": 470, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "꾸준히 연습하면 더 잘할 수 있어요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "꾸준히", "start_ms": 110, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "연습하면", "start_ms": 270, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 530, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "잘할", "start_ms": 590, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 720, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 770, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "화이팅 합시다",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "화이팅", "start_ms": 90, "dur_ms": 200, "phase_lr_deg": -10.0},
{"word": "합시다", "start_ms": 300, "dur_ms": 180, "phase_lr_deg": -10.0}
]
},
{
"segment": 33,
"transcript": "매일 10분씩만 투자해 보세요. 결과가 다를 거예요.",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "매일", "start_ms": 140, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "10분씩만", "start_ms": 260, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "투자해", "start_ms": 620, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "보세요.", "start_ms": 830, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "결과가", "start_ms": 1150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "다를", "start_ms": 1360, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "거예요.", "start_ms": 1520, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "언어 학습은 마라톤과 같습니다. 서두르지 마세요.",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 160,
"audio_type": "speech",
"words": [
{"word": "언어", "start_ms": 160, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "학습은", "start_ms": 320, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "마라톤과", "start_ms": 580, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "같습니다.", "start_ms": 940, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "서두르지", "start_ms": 1300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "마세요.", "start_ms": 1610, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "Keep going and never give up",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "Keep", "start_ms": 120, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "going", "start_ms": 280, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "and", "start_ms": 410, "dur_ms": 50, "phase_lr_deg": 5.0},
{"word": "never", "start_ms": 470, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "give", "start_ms": 630, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "up", "start_ms": 740, "dur_ms": 50, "phase_lr_deg": 5.0}
]
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
"transcript": "오늘 공부한 내용을 복습해 봅시다. 중요한 표현들이 많았죠?",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "공부한", "start_ms": 310, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "내용을", "start_ms": 520, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "복습해", "start_ms": 730, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "합시다.", "start_ms": 940, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "중요한", "start_ms": 1300, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "표현들이", "start_ms": 1560, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "많았죠?", "start_ms": 1870, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "질문 있으면 언제든지 말씀해 주세요. 감사합니다.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "질문", "start_ms": 130, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있으면", "start_ms": 290, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "언제든지", "start_ms": 450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "말씀해", "start_ms": 760, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "주세요.", "start_ms": 970, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "감사합니다.", "start_ms": 1300, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "다음에 또 만나요. 안녕!",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 260, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "만나요.", "start_ms": 320, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "안녕!", "start_ms": 600, "dur_ms": 150, "phase_lr_deg": 0.0}
]
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
"transcript": "마지막으로 복습 한번 더 할까요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "마지막으로", "start_ms": 120, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "복습", "start_ms": 480, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "한번", "start_ms": 640, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 770, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "할까요?", "start_ms": 830, "dur_ms": 200, "phase_lr_deg": 0.0}
]
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
"transcript": "The ornate doubt superhuman speed and power",
"speaker": "speaker_2",
"confidence": 0.91,
"language": "en",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "The", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "ornate", "start_ms": 290, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "doubt", "start_ms": 500, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "superhuman", "start_ms": 660, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "speed", "start_ms": 1070, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "and", "start_ms": 1280, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "power", "start_ms": 1390, "dur_ms": 250, "phase_lr_deg": 5.0}
]
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
"transcript": "이제 정말 끝났습니다. 수고하셨습니다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 110, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 270, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "끝났습니다.", "start_ms": 430, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "수고하셨습니다.", "start_ms": 850, "dur_ms": 500, "phase_lr_deg": 0.0}
]
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
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 08:43:23*
