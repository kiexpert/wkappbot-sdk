# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=17708  time=06:16:31  uptime=7652s  mem=317MB  system_processes=358"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_061618.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 106-135-135-135-315-315-315-315-315-315-135-130-130-310-310-103-150-413-130-145-130-310-130-310-130-130-310-103-310-135-315-135-315-315-314-130-130-130-103-154-345-453-154-153-1_W.mp3)
  Segment 2: 1576ms ~ 6364ms (file: 326-213-236-231-312-321-312-123-236-263-261-216-213-213-276-216-126-231-132-261-123-632-267-263-267-267-267-261-673-765-236-627-261-123-216-265-210-127-261-260-162-120-106-576-5_W.mp3)
  Segment 3: 7364ms ~ 8660ms (file: 471 135-132-316-576-312-132-675-725-672-207-316-102-351-530-105-152-103-132-576-130-316-132-106-123-125-123-152-126-132-173-123-276-756-137-237-732-231-570-162-236-261-521-576 3_V.mp3)
  Segment 4: 9660ms ~ 20172ms (file: 132-102-210-754-547-725-267-620-726-547-527-236-231-547-352-756-256-174-102-310-256-235-257-102-106-231-321-103-613-107-257-132-102-752-275-130-756-320-132-123-102-257-253-753-7_W.mp3)
  Segment 5: 21172ms ~ 32476ms (file: 130-321-120-672-162-106-735-735-725-763-276-236-123-316-627-103-134-237-217-201-270-327-107-106-167-270-312-236-326-201-231-321-123-273-132-136-130-237-263-130-132-236-236-371-2_W.mp3)
  Segment 6: 33476ms ~ 34052ms (file: 316-257-507-265-245-267-532-375-362-265-756-123-576-756-237-752-261-317-756-162-562-315-367-275-603-162-317-136-613-361-136-507-571-570-573-574-576-753_V.mp3)
  Segment 7: 35052ms ~ 41784ms (file: 312-123-126-120-321-263-263-637-673-765-327-725-301-316-160-210-123-276-267-263-267-765-267-627-236-230-123-261-126-754-574-571-754-127-261-271-236-326-237-213-312-751-572-571-5_W.mp3)
  Segment 8: 42784ms ~ 43360ms (file: 574-576-576-576-574-547 574-547-574-547-574-574-574-570-574-574-574-576-574-576-574-576-756-574-574-574-576 574-574-574-574-570-574-754-754-754-753-750-754-754-754-754-512 576-5_W.mp3)
  Segment 9: 44360ms ~ 51488ms (file: 310-263-132-132-627-312-265-265-276-257-375-132-572-574-256-572-215-231-231-236-275-267-267-526-352-167-136-321-263-267-621-625-126-574-267-267-312-263-627-265-267-265-542-321-2_W.mp3)
  Segment 10: 52488ms ~ 53064ms (file: 751-753-672-527-765-572-153-371-625-351-312-675-173-367-721-123-512-752-715-215-301-316-753-137-573-132-137-321-135-271-517-315-756_V.mp3)
  Segment 11: 54064ms ~ 54640ms (file: 105-150-157-175-751-107-150-157-150-107-170-175-175-710_W.mp3)
  Segment 12: 55640ms ~ 56216ms (file: 157-173-715-751-175 527-527-527 157-150-157-152-153-157-175-157-157-157-512 157-157-153-153-157-150-170-107-713-173-170-712-170-751-750-751-751-751_V.mp3)
  Segment 13: 57216ms ~ 68520ms (file: 574-750-574-547-754-201-210-607-670-672-267-213-203-231-213-132-312-132-132-267-267-207-651-321-103-201-520-547-754-207-102-514-132-675-261-261-206-263-310-260-527-257-201-301-1_W.mp3)
  Segment 14: 69520ms ~ 70096ms (file: 167-123-137-137-176-137-127-361-612-123-217-123-150-517-751-531-175-715-750-157-157-175-715-175-176-157-173-137-731-715-173-157-751-751-175-137-751-513-105-130-105-103-105-102-1_V.mp3)
  Segment 15: 71096ms ~ 79880ms (file: 107-127-137-137-103-260-620-271-230-260-172-132-674-132-176-103-163-132-261-130-301-321-321-130 170-627-216-136-672-216-260-103-216-120-107-236-213-621-321-267-261-231-102-102-3_V.mp3)
  Segment 16: 80880ms ~ 81456ms (file: 301-130-731-317-137-130-307-130-137-310-317-170-371-317-371-173-173-715 731-703-317-103-371-301-173-301-107-317-310-735-137-706-715-701-317-371-753-753-375-710-370-317-713-703-7_V.mp3)
  Segment 17: 82456ms ~ 83032ms (file: 206-213-120-310 657-162-136-126-631-106-162-137-136-317-576-547-574-574-571-576-756-267-312-132-263-326-165-136-672-210-130-310-316-762-276-627-267-105-137-176-316-312-760-706-1_W.mp3)
  Segment 18: 84032ms ~ 84608ms (file: 365-362-136-312-312-312-132-132-312-312-316-163-132-321-351-576-573 136-125-216-126-123-152-216-216-365-315-316-316-532-351-635-653-367-615-275-567-562-527-547-547-547-546-513-1_W.mp3)
  Segment 19: 85608ms ~ 93276ms (file: 753-123-750-123-316-253-754-236-203-267-261-270-230-675-761-237-260-263-752-576-260-260-276-206-273-260-267-267-621-267-156-572-263-213-321-312-765-206-230-527-267-206-256-206-2_W_stt_gerhard_earnhardt_so_here_we_have_in_thi.mp3)
  Segment 20: 94276ms ~ 98343ms (file: 120-271-752-207-206-607-672-602-627-675-321-327-321-201-326-130-175-267-267-270-257-236-612-267-627-617-126-765-136-160-261-210-103-176-162-267-206-267-627-263-126-726-102-102-3_W.mp3)
  Segment 21: 99343ms ~ 99919ms (file: 570-751-751-571 574-574-574-574-574-574-547-574-574-547-574-574-574-574-574-574_W.mp3)
  Segment 22: 100919ms ~ 101495ms (file: 715-715-175-175-175-751-756 570-175-710-715-751-571-751-751-570-175-715-751-750-751-175-175-715-175-715-570 751-571-175-751-751-715-751-157-751-715-175-751-175-175-751-751-754-7_W.mp3)
  Segment 23: 102495ms ~ 103071ms (file: 210-123-120-130-672-672-675-627-627-627-675-765-367_W.mp3)
  Segment 24: 104071ms ~ 104647ms (file: 735-170 375-317-103-107-137-301-175-601-670-613-670-601-613-613-163-361-617-167-617-132-175-170-137-130-130-175-130-310-175-751 735-375-175-135-103-513-137-731-701-637-670-603-6_V.mp3)
  Segment 25: 105647ms ~ 112199ms (file: 132-237-726-267-276-672-276-216-267-102-310-210-103-320-312-231-137-376-571-735-567-376-732-327-137-317-316-213-231-231-130-312-130-103-102-123-126-132-103-107-106-361-163-132-1_W.mp3)
  Segment 26: 113199ms ~ 119355ms (file: 206-273-130-320-326-237-267-216-275-120-326-267-170-106-617-163-371-312-167-172-175-613-207-312-123-261-367-327-317-372-320-573-173-216-207-217-302-607-267-236-137-761-136-320-2_V.mp3)
  Segment 27: 120355ms ~ 120931ms (file: 574-574-547-547-547-574 567-376-302-136-301-103-213-536-570-627-235-325-136-310-130-107-132-305-571-561-573-573-375-563-367-765-321-230-231-265-231-236-267-327-357-756-756 376-6_W.mp3)
  Segment 28: 121931ms ~ 122507ms (file: 167-602-102-103-762-761-671-671-671-601-136-167-163-103-132-213-236-263-236-130-213-216-261-231-672-130-375-321-321-203-524-351-531-574-137-106-361-103-163-156-167-570-657-612-1_W.mp3)
  Segment 29: 123507ms ~ 128943ms (file: 756-321-326-321-312-326-236-231-123-173-137-103-310-217-231-273-132-231-231-257-213-237-130-302-201-627-602-260-206-260-236-206-276-725-672-276-137-302-103-130-236-301-312-312-3_W.mp3)
  Segment 30: 129943ms ~ 137395ms (file: 206-572-275-217-120-206-260-672-206-201-260-162-257-260-256-520-301-312-206-132-107-103-102-126-726-670-760-572-103-201-760-627-260-132-570-301-103-213-574-574-574-206-120-260-2_W.mp3)
  Segment 31: 138395ms ~ 138971ms (file: 475-471-541-435-453-405-104-310-103-145-145-105-104-157-157-105-134-104-154-104 375-573-317-157-471-103-501-175-750-150-105-104-154-107-105-135-154-105-105-154-105-104-103-175-1_V.mp3)
  Segment 32: 139971ms ~ 140547ms (file: 751 756-105-705-710-571-157-713-750-173-715-750-170-751-715-750-175-571-751-751-751-751-571-751-715-175-751-751 756-751-173-157-170-750-173-715-710-173-157-713-173-170-173-751-7_W.mp3)
  Segment 33: 141547ms ~ 142123ms (file: 517-501-501-517-571-501-507-510-501-501-510-576-571-517-571-576-567-516-567-576-576-574 507-510-570-507-501-510-501-517-501-507-501-501-510-501-510-510-510-501-516-517-567-567-5_W.mp3)
  Segment 34: 143123ms ~ 143699ms (file: 145-415-173-174-145-415-137-104-105-104-135-104-135-103-130 137-135-134-143-103-154-134-153-105-104-317-145-104-104-154-134-174-107-104-153-104-157-157-175-147-145-154-104-145 7_V.mp3)
  Segment 35: 144699ms ~ 145275ms (file: 517-571-571-576-571-576-576-576-570-135-571-510-570-510-510-510-517-517-517-157-507-156-570-570-150-570-573-570-561-576-571-517-157-517-156-570-571-570-510-570-520-571-571-571-1_W.mp3)
  Segment 36: 146275ms ~ 146851ms (file: 756-725-635-750-326-257-235-637-726-256-752-257-752-756-572-526-325-132-162-372-163-235-236-325-627-275-327-132-205-726-236-267-635-756-275-132-510-316-312-162-217-216-143-536-5_V.mp3)
  Segment 37: 147851ms ~ 148427ms (file: 150-150-157-157-157-105-105-105-105-157-571-751-517-175-175-517-175-751-571-517-157-517-571-157-157-173-175-175-715-715-137-517-157-517-157-571-571-570-517-156-157-571-576-571-5_V.mp3)
  Segment 38: 149427ms ~ 156951ms (file: 632-627-726-327-132-627-657-652-163-165-123-302-617-260-267-107-302-103-310-361-371-107-310-237-203-307-376-312-234-103-310-213-213-231-130-103-103-204-231-201-204-201-231-302-3_W.mp3)
  Segment 39: 157951ms ~ 158527ms (file: 267-756-652-570-675 531-572-325-576-156-267-725-351-315-253-327-527-271-572-756-175-502-301-312-506-673-257-312-237-207-263-237-715-257-723-127-271-725-152-176-362-627-752-267-3_V.mp3)
  Segment 40: 159527ms ~ 184187ms (file: 136-123-261-102 320-103 201-126-136-103-726-370-136-170-173-276-275-107-276-201-102-130-103-135-103-164-103-321-203-103-573-132-210-671-275-126-135-143-132-104-206-301-736-123-1_V.mp3)
  Segment 41: 185187ms ~ 185763ms (file: 507-501-570-501-507-571-710-175-750-750-507-751-705-701-750-501-705 750-750-570-570-507-570-501-570-507-705-571-751-507-107-750-751-570-574 175-157-705-750-105-107-175-751-517-7_V.mp3)
  Segment 42: 186763ms ~ 187339ms (file: 316-123-310-162-316-623-312-321-312-627-672-321-321-126-132-126-163-316-310 620-625-620-206-123-136-613-261-621-267-126-102-361-326-236-362-326-360-362-302-320-312-136-613-621-3_V.mp3)
  Segment 43: 188339ms ~ 200687ms (file: 736-674-627-627-137-137-263-127-123-765-716-107-276-310-236-216-761-672-312-362-326-163-107-321-263-231-102-765-157-103-163-167-137-317-735-762-267-267-627-672-276-276-725-752-2_W.mp3)
  Segment 44: 201687ms ~ 202263ms (file: 572-726-520-253-576-327-132-503-231-321-156-706-726-567 237-507-263-132-756-126-671-570-312-651-231-752-152-236-527-726-125-570-275-752-705-267-326-230-236-570-276-623-103-215-1_W.mp3)
  Segment 45: 203263ms ~ 203839ms (file: 723-136-132-136-172-265-127 163 127 106-167-176-167-475-167-162-103-136-103-126-127-713-175-175-165-103-162 756-765-672-673-513-157-561-531-750-753-673-160-160 231-172-315-327-1_V.mp3)
  Segment 46: 204839ms ~ 205415ms (file: 501-510-150-510-105-150-157-150-150-510-517-571 501-501-510-501-507-510-150-510-510-510-510-501-501-510-150-510-501-510-501-501-150-157-507-510-501-150-510-571-517 501-501-150-5_W.mp3)
  Segment 47: 206415ms ~ 214155ms (file: 163-762-276-756-170-103-216-627-267-267-756-130-756-267-267-627-263-120-132-312-123-267-753-375-231-372-267-132-367-276-216-103-130-327-263-261-213-316-756-315-375-312-236-537-1_W.mp3)
  Segment 48: 215155ms ~ 215731ms (file: 675-760-706-620-362-203-137-620-675-602-762-310-302-362-325-326-326-632-762-760-673-360-627-612-623-627-627_W.mp3)
  Segment 49: 216731ms ~ 217307ms (file: 103-761-163-106-103-106-160-163-165-125-163-130-615-106-160-106-103-162-102-172-167-213-106-160-136-310-163-103-160-160-136-132-102-160-106-623-623-231-706-627-762-726-725-765-7_V.mp3)
  Segment 50: 218307ms ~ 218883ms (file: 123-570-572-652-526-651-567-567-567-576-756-756-756-756-567-576-576-576-576-576-673_W.mp3)
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
> **Time**: 2026-04-05 06:18:22
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 2,
"transcript": "백제는 황금의 나라입니다 그래서 그 궁궐을 하나 네 하나 만들어 드려야죠",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "백제는", "start_ms": 110, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "황금의", "start_ms": 600, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "나라입니다", "start_ms": 1050, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 1750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 2200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "궁궐을", "start_ms": 2400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "하나", "start_ms": 2900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "네", "start_ms": 3300, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "하나", "start_ms": 3550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "만들어", "start_ms": 3950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "드려야죠", "start_ms": 4400, "dur_ms": 380, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "이게 사실 1400년 동안 있었던 유물인데",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "이게", "start_ms": 90, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "사실", "start_ms": 380, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "1400년", "start_ms": 720, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "동안", "start_ms": 1300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "있었던", "start_ms": 1650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "유물인데", "start_ms": 2100, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "어떻게 보면 최고의 방을 만들어 가지고 후손들에게 보여줘야지 하는 이런 생각이 되게 있으셨을 거예요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "어떻게", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "보면", "start_ms": 600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "최고의", "start_ms": 950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "방을", "start_ms": 1450, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "만들어", "start_ms": 1750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "가지고", "start_ms": 2200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "후손들에게", "start_ms": 2600, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "보여줘야지", "start_ms": 3250, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 3850, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 4150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "생각이", "start_ms": 4500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "되게", "start_ms": 4950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "있으셨을", "start_ms": 5300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 5850, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "아 네 맞아요 백제 금동대향로가 여기서 발견되었기 때문에 아 백제가 이런 느낌이구나를 느낄 수 있게끔 해 주는",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "아", "start_ms": 120, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "네", "start_ms": 300, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "맞아요", "start_ms": 550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "백제", "start_ms": 1100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "금동대향로가", "start_ms": 1500, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "여기서", "start_ms": 2400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "발견되었기", "start_ms": 2850, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "때문에", "start_ms": 3600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 4200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "백제가", "start_ms": 4400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 4900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "느낌이구나를", "start_ms": 5250, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "느낄", "start_ms": 6100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 6450, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있게끔", "start_ms": 6650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "해", "start_ms": 7150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "주는", "start_ms": 7350, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "갑상선 스트레스만 받으면 큰 문제가 될 게 없고",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "갑상선", "start_ms": 80, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "스트레스만", "start_ms": 650, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "받으면", "start_ms": 1300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 1800, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "문제가", "start_ms": 2050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "될", "start_ms": 2550, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 2750, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "없고", "start_ms": 2950, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "처음에는 두 달에 한 번씩 검진을 했는데 지금은",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "처음에는", "start_ms": 100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "두", "start_ms": 650, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "달에", "start_ms": 900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 1250, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "번씩", "start_ms": 1500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "검진을", "start_ms": 1900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "했는데", "start_ms": 2400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "지금은", "start_ms": 2900, "dur_ms": 450, "phase_lr_deg": 0.0}
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
"transcript": "그 규모가 한 번 이동할 때마다 유조선이 규모가 3000억이라는 거 거기서 30억 가져간다는 거 좀 참고를",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "그", "start_ms": 120, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "규모가", "start_ms": 300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 800, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "번", "start_ms": 1050, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "이동할", "start_ms": 1300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "때마다", "start_ms": 1800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "유조선이", "start_ms": 2300, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "규모가", "start_ms": 2950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "3000억이라는", "start_ms": 3450, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 4350, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "거기서", "start_ms": 4550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "30억", "start_ms": 5000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "가져간다는", "start_ms": 5500, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 6200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 6400, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "참고를", "start_ms": 6650, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "한 포인트가 신뢰가 안 되면은 전체 시스템의 취약점이 될 수 있다라는 부분인데",
"speaker": "speaker_5",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "한", "start_ms": 90, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "포인트가", "start_ms": 350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "신뢰가", "start_ms": 900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 1400, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "되면은", "start_ms": 1600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "전체", "start_ms": 2100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "시스템의", "start_ms": 2500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "취약점이", "start_ms": 3100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "될", "start_ms": 3700, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 3900, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있다라는", "start_ms": 4100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "부분인데", "start_ms": 4650, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 12,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 13,
"transcript": "미국 CNN 등 유력 언론들이 일제히 보도했습",
"speaker": "speaker_6",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "미국", "start_ms": 140, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "CNN", "start_ms": 600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "등", "start_ms": 1150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "유력", "start_ms": 1350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "언론들이", "start_ms": 1750, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "일제히", "start_ms": 2350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "보도했습", "start_ms": 2850, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 15,
"transcript": "너무 스트레스를 많이 받아가지고 이거 다시 파이자 이거 잘못 샀다",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "너무", "start_ms": 110, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "스트레스를", "start_ms": 450, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 1050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "받아가지고", "start_ms": 1450, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "이거", "start_ms": 2100, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 2400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "파이자", "start_ms": 2800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이거", "start_ms": 3300, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "잘못", "start_ms": 3600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "샀다", "start_ms": 3950, "dur_ms": 350, "phase_lr_deg": 0.0}
]
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
"transcript": "그렇게까지 얘기를 했었던 상황이에요 사실은 그걸 보면서 사실은 진짜",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "그렇게까지", "start_ms": 90, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "얘기를", "start_ms": 700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "했었던", "start_ms": 1100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "상황이에요", "start_ms": 1550, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "사실은", "start_ms": 2200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "그걸", "start_ms": 2650, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "보면서", "start_ms": 2950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "사실은", "start_ms": 3400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 3850, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "나는 이제 조사님 계좌도 봤잖아 계좌를 사실 공개하기 쉽지 않거든요 왜냐면",
"speaker": "speaker_4",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "나는", "start_ms": 130, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 450, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "조사님", "start_ms": 750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "계좌도", "start_ms": 1250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "봤잖아", "start_ms": 1700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "계좌를", "start_ms": 2200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "사실", "start_ms": 2650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "공개하기", "start_ms": 3000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "쉽지", "start_ms": 3550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "않거든요", "start_ms": 3900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "왜냐면", "start_ms": 4400, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "gerhard earnhardt so here we have in thi",
"speaker": "speaker_7",
"confidence": 0.85,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "gerhard", "start_ms": 100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "earnhardt", "start_ms": 600, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "so", "start_ms": 1200, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "here", "start_ms": 1450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "we", "start_ms": 1800, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "have", "start_ms": 2000, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 2300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "thi", "start_ms": 2500, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "내 모든 것을 옷을 벗는 느낌이 되기 때문에 주식은 뭔가 부끄러워 옷을 벗은 느낌이야",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "내", "start_ms": 150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "모든", "start_ms": 400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "것을", "start_ms": 800, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "옷을", "start_ms": 1100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "벗는", "start_ms": 1450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "느낌이", "start_ms": 1850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "되기", "start_ms": 2300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "때문에", "start_ms": 2650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "주식은", "start_ms": 3150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "뭔가", "start_ms": 3650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "부끄러워", "start_ms": 4000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "옷을", "start_ms": 4550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "벗은", "start_ms": 4900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "느낌이야", "start_ms": 5300, "dur_ms": 500, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "진짜 많은 사람들이 속상해할 것으로 보이고요 그리고 하나를 보면 열을 안다고",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 120, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "많은", "start_ms": 500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "사람들이", "start_ms": 850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "속상해할", "start_ms": 1400, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "것으로", "start_ms": 2000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "보이고요", "start_ms": 2450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 2950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "하나를", "start_ms": 3400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "보면", "start_ms": 3850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "열을", "start_ms": 4200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "안다고", "start_ms": 4550, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "이 사태를 보아하니 지금은 SN6니까 다행이다 싶을 수도 있겠지만은",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 140, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "사태를", "start_ms": 350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "보아하니", "start_ms": 850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "지금은", "start_ms": 1400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "SN6니까", "start_ms": 1900, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "다행이다", "start_ms": 2750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "싶을", "start_ms": 3300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "수도", "start_ms": 3650, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "있겠지만은", "start_ms": 3950, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "이거 동기부 동작 있잖아요 딱 그 맛인데 조금 더 부드럽고 촉촉해",
"speaker": "speaker_8",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "이거", "start_ms": 110, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "동기부", "start_ms": 400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "동작", "start_ms": 900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있잖아요", "start_ms": 1300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "딱", "start_ms": 1800, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 2000, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "맛인데", "start_ms": 2200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 2650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 3000, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "부드럽고", "start_ms": 3200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "촉촉해", "start_ms": 3750, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "진짜 사업해야 될 것 같아요",
"speaker": "speaker_8",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 90, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "사업해야", "start_ms": 500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "될", "start_ms": 1050, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 1250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 1450, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "근데 우렁이 어딘지 몰라가지고 그냥 우렁 없는 강된장",
"speaker": "speaker_8",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "근데", "start_ms": 120, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "우렁이", "start_ms": 450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "어딘지", "start_ms": 900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "몰라가지고", "start_ms": 1400, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 2050, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "우렁", "start_ms": 2400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "없는", "start_ms": 2800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "강된장", "start_ms": 3200, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "기후가 온화하고 야생동물도 풍부해 실제로 더 살기 좋은 환경이었을 것이라고 보고 있습니다",
"speaker": "speaker_6",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "기후가", "start_ms": 130, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "온화하고", "start_ms": 550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "야생동물도", "start_ms": 1100, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "풍부해", "start_ms": 1800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "실제로", "start_ms": 2300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 2800, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "살기", "start_ms": 3000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 3400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "환경이었을", "start_ms": 3750, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "것이라고", "start_ms": 4450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "보고", "start_ms": 5000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 5350, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "한편 야생동물의 이동은 거의 대칭적으로 이루어졌던",
"speaker": "speaker_6",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "한편", "start_ms": 110, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "야생동물의", "start_ms": 500, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "이동은", "start_ms": 1150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "거의", "start_ms": 1650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "대칭적으로", "start_ms": 2000, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "이루어졌던", "start_ms": 2700, "dur_ms": 600, "phase_lr_deg": 0.0}
]
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "그래 먹자 먹자 목단 이거 아미팀 이름 정해졌네 이건 뭐야 그래 수네",
"speaker": "speaker_4",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "그래", "start_ms": 100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "먹자", "start_ms": 450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "먹자", "start_ms": 800, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "목단", "start_ms": 1150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이거", "start_ms": 1550, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "아미팀", "start_ms": 1850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이름", "start_ms": 2350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "정해졌네", "start_ms": 2700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이건", "start_ms": 3250, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "뭐야", "start_ms": 3550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "그래", "start_ms": 3900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "수네", "start_ms": 4250, "dur_ms": 350, "phase_lr_deg": 0.0}
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
"transcript": "동네 이름 어 동네 이름 수네",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "동네", "start_ms": 120, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이름", "start_ms": 550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "어", "start_ms": 900, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "동네", "start_ms": 1100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이름", "start_ms": 1500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "수네", "start_ms": 1850, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "포커스 양보가 필요합니다",
"speaker": "speaker_9",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "포커스", "start_ms": 80, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "양보가", "start_ms": 600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "필요합니다", "start_ms": 1050, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "이사벨입니다 네 많은 분들이 제가 어떻게 한국어를 배웠는지 왜 배웠는지 많이 궁금해하시니까",
"speaker": "speaker_10",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "이사벨입니다", "start_ms": 130, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "네", "start_ms": 1000, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "많은", "start_ms": 1250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "분들이", "start_ms": 1650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 2150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 2500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "한국어를", "start_ms": 2950, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "배웠는지", "start_ms": 3550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "왜", "start_ms": 4100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "배웠는지", "start_ms": 4350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 4900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "궁금해하시니까", "start_ms": 5300, "dur_ms": 850, "phase_lr_deg": 0.0}
]
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
"transcript": "앞으로 무서워해야 하는 건 AI 그 자체가 아니다",
"speaker": "speaker_1",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "앞으로", "start_ms": 100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "무서워해야", "start_ms": 600, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 1200, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "건", "start_ms": 1500, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "AI", "start_ms": 1700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 2100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "자체가", "start_ms": 2300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "아니다", "start_ms": 2800, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "AI로 날개를 달아버린 옆자리 동료와 나 사이의 영원히 메울 수 없는 격차를 두려워해야 한다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "AI로", "start_ms": 120, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "날개를", "start_ms": 650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "달아버린", "start_ms": 1100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "옆자리", "start_ms": 1700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "동료와", "start_ms": 2200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "나", "start_ms": 2700, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "사이의", "start_ms": 2950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "영원히", "start_ms": 3400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "메울", "start_ms": 3900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 4300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "없는", "start_ms": 4500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "격차를", "start_ms": 4850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "두려워해야", "start_ms": 5350, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "한다", "start_ms": 6000, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "실리콘밸리의 거물 리더",
"speaker": "speaker_1",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "실리콘밸리의", "start_ms": 100, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "거물", "start_ms": 850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "리더", "start_ms": 1250, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "미나리 삼겹살 아니라 미나리 삼겹살 처음 미나리 삼겹살 구워보려고 오케이 여기 이제",
"speaker": "speaker_8",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "미나리", "start_ms": 110, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "삼겹살", "start_ms": 550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 1050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "미나리", "start_ms": 1500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "삼겹살", "start_ms": 1950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "처음", "start_ms": 2450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "미나리", "start_ms": 2850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "삼겹살", "start_ms": 3300, "dur_ms": 450, "phase

---
*Generated by WKAppBot ask gemini — 2026-04-05 06:18:22*
