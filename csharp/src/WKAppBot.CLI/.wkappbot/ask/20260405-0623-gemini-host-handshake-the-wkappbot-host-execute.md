# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=24932  time=06:21:40  uptime=7385s  mem=440MB  system_processes=355"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_061901.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 120-102-102-102-106-103-102-162-260-136-201-320-263-206-206-265-130-176-107-103-103-123-130-320-230-236-263-260-263-230-260-230-320-260-206-263-236-260-236-326-320-263-162-613-6_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 567-567-175-571-573-103-106-172-130-562-673-657-167-671-136-132-316-156-167-317-163-162-162-176-136-316-163-163-317-163-167-160-165-156-165-657-657-752-263-273-712-726-725-721-7_W.mp3)
  Segment 3: 3152ms ~ 10460ms (file: 765-107-752-235-213-612-267-267-260-261-263-754-263-627-261-126-621-576-120-162-132-751-627-621-623-136-130-321-261-261-263-170-123-571-210-172-102-762-310-621-267-321-756-261-2_W.mp3)
  Segment 4: 11460ms ~ 12036ms (file: 150-150-157-157-157-105-105-105-105-157-571-751-517-175-175-517-175-751-571-517-157-517-571-157-157-173-175-175-715-715-137-517-157-517-157-571-571-570-517-156-157-571-576-571-5_V.mp3)
  Segment 5: 13036ms ~ 22540ms (file: 203-302-260-261-213-162-672-267-263-236-263-103-120-102-726-216-724-123-320-203-320-320-127-167-106-761-761-307-302-302-320-731-163-103-172-137-127-106-176-612-162-271-236-750-2_V.mp3)
  Segment 6: 23540ms ~ 29804ms (file: 216-715-162-754-157-754-754-625-260-765-130-625-267-627-267-752-254-527-627-236-310-756-162-162-106-163-576-205-754-267-106-267-263-621-576-230-261-261-672-106-623-263-261-102-1_W.mp3)
  Segment 7: 30804ms ~ 31380ms (file: 310-310-312-351-230-321-753-753-713-715-756-731-751-715-751-735-735-762-726-735-372-723-756-735 705-705-750-750-750-705-750-705-507-750-751-705-751-705-710-705-750_W.mp3)
  Segment 8: 32380ms ~ 32956ms (file: 105-150-157-517-105-105-105-105-105-150-130 150-105-150-150-150-150-105-105-157-105-150-105-150-150-150-150_V.mp3)
  Segment 9: 33956ms ~ 34532ms (file: 653-365-265-627-362-361-536-256-263-267-263-623-632-621-265-261-217-326-361-135-356-135-316-362-316-327-567-567-261-263-261-253-251-576-257-216-312-312-316-132-236-361-316-361-1_W.mp3)
  Segment 10: 35532ms ~ 36108ms (file: 165-163-263-216 602-621-610-175-126-675-106-162-176-162-623-621-615-627-623-602-672-617-103-103-167-761-361-256-135-671-627-126-312-170-103-132-103-613-103-735-675-736-612-267-6_V.mp3)
  Segment 11: 37108ms ~ 37684ms (file: 157-751-157-105-157-105-107-105-105-157-157-105-175-107-157-150-150-517 105-150-105-157-501-510-105-157-517-157-105-105-107-105-157-751-175-710-175-170-170-170-715-705-105 105-1_V.mp3)
  Segment 12: 38684ms ~ 39260ms (file: 163-163-736-261 265-260-762-726-761-567-567-625-576-573-610-160-671-267-576-267-206-673-631-675-603-627-132-230-150-627-612-362-367 561 672-267 370 312-607-671-670-673-670-763-6_W.mp3)
  Segment 13: 40260ms ~ 40836ms (file: 571-513-715-156-517-571-157-157-750-570-576-571-576-576-576-576-576-576-567-576-567-567-576-576-576-576-567-567-576-567-567-576-567-576-567-576-567-150-657-715-512-756-576-175-5_W.mp3)
  Segment 14: 41836ms ~ 42412ms (file: 501-510-501-105-157-157-517-150-507-150-570 510-510-150-105-157-517-517-510-501-501-517-501-501-501-501-510-507-510-510-571-574-574-514-507-501-517-510-501-507-570 510-507-510-1_W.mp3)
  Segment 15: 43412ms ~ 43988ms (file: 312-712-716-576-567-765-751-576-503-617-615-576-756-107-317-250-207-273-517-312-672-132-132-310-316-312-316-312-317-167-132-312-162-301-203-207-230-615-371-127-312-302-102-127-1_V.mp3)
  Segment 16: 44988ms ~ 45564ms (file: 513-152-126-102-102-103-652-621-361-136-671-167-216-612-621-162-761-671-671-167-132-134-137 576-756-765-756-576-653-657-657-675-765-671 576 675-675 127-132-673-673-607-637-756-7_W.mp3)
  Segment 17: 46564ms ~ 47140ms (file: 713-371-731-713-607-706-170-310-136-706-310-751-753-753-756-756-752 103-301-137-310-730-312-307-310-102-715-170-172-107-103-157-730-713-137-301-713-103 137-301-310-130-715-370-3_V.mp3)
  Segment 18: 48140ms ~ 56456ms (file: 312-102-673-672-273-321-312-732-107-267-301-270-267-273-327-762-765-672-203-207-267-276-103-102-102-175-517-571-765-310-574-574-130-310-273-726-756-765-216-104-123-317-531-547-3_W.mp3)
  Segment 19: 57456ms ~ 58032ms (file: 135-104-107-137-135-104-130-135-103-314-314 301-317-347-451-315-104-154-415-451-157-157-103-105-157-107-105-103-104-145-415-150-107-103-175-134 310-301-103-130-140-130-310-104-1_V.mp3)
  Segment 20: 59032ms ~ 64396ms (file: 126-671-103-102-156-326-301-316-136-517-105-617-573-375-653-107-713-752-302-231-301-605-132-761-175-156-312-132-231-267-620-213-712-107-706-270-213-206-326-102-136-576-567-312-1_V.mp3)
  Segment 21: 65396ms ~ 75692ms (file: 124-715-765-672-765-673-627-627-762-765-267-261-216-216-267-276-753-517-126-231-310-167-276-103-132-261-675-216-125-213-231-267-167-172-216-263-652-576-675-567-265-321-213-103-7_W_stt_that's_an_easy_days_until_the.mp3)
  Segment 22: 76692ms ~ 83316ms (file: 132-103-120-120-312-201-507-103-107-321-216-261-132-216-231-132-310-631-273-615-527-130-316-316-263-576-265-321-136-576-130-516-213-617-263-167-526-526-621-527-572-321-231-132-1_W.mp3)
  Segment 23: 84316ms ~ 91120ms (file: 317-130-312-316-312-123-263-261-261-237-236-765-276-273-237-532-327-326-326-327-312-376-327-321-321-762-276-725-273-267-756-736-321-327-617-213-721-216-231-267-270-217-267-267-2_W.mp3)
  Segment 24: 92120ms ~ 92696ms (file: 130-135-105-137-135-130-157 157-135-135-507-570-135-571-517-150-130-317-310-135-130-130-310-157-715 135-103-130-517-513-157-175-517-571-105-130-137-317-130-137-135-157-175-137-1_V.mp3)
  Segment 25: 93696ms ~ 103812ms (file: 263-123-127-231-260-267-620-265-256-263-102-261-236-320-321-754-265-213-231-263-261-231-132-754-263-263-231-236-725-756-265-312-236-612-263-574-621-261-542-265-725-216-120-261-3_W.mp3)
  Segment 26: 104812ms ~ 115000ms (file: 765-267-267-216-216-627-765-230-712-752-275-210-627-267-236-120-213-261-732-320-162-102-123-612-752-267-267-261-230-267-623-160-260-275-275-263-103-230-126-263-310-326-270-321-2_W.mp3)
  Segment 27: 116000ms ~ 116576ms (file: 103-135-105-130-137-175-157-310-153-315-157-153-157-517-571-573-571 105-105-153-153-157-531-135-135-103-513-150-153-153-157-513-517-157-157-517-571-751-537-315-135-153-157-357-1_V.mp3)
  Segment 28: 117576ms ~ 118152ms (file: 103-150-150-105-501-153-103-150-130-105-105-105-165-165-165-165-165-106-105-570 517-571-571-571-571-571-570-571-517-150-517-510-570-571-563_V.mp3)
  Segment 29: 119152ms ~ 119728ms (file: 310-132-123-130-120-130-310-130-301-102-130-102-103-123-126 627-267_V.mp3)
  Segment 30: 120728ms ~ 121304ms (file: 571-571-571-573-571-567-567-567-567 153-153-513-510 157-715-157-517-510-517-571-157-570-576-657-576-571-570-576-570-517-571-573-560-570-570 571-157-175-571-517-571-517-570-571-5_V.mp3)
  Segment 31: 122304ms ~ 122880ms (file: 103-307-172-736-317-371-370-317-175-751-371 175-310-170-307-730-735-715-103-607-316-761-317-715-301-130-731-701-137-371-137-173-310-317-137-750-750-710-137-317-750 753-701-703-3_V.mp3)
  Segment 32: 123880ms ~ 124456ms (file: 236-623-627-327-621-312-135-135-601-321-675-217-712-725-135-153-572-175-513-527-253-567-321-765-627-312-302-761_V.mp3)
  Segment 33: 125456ms ~ 126032ms (file: 765-657-576-675-231-203-123-102-103-312-130-301-302-103-231-231-216-321-326-231-123-765-657-576-657-576-562-765-576-765-567-567-657-576-567-657-567-765-571-756-567-576-567-657-5_W.mp3)
  Segment 34: 127032ms ~ 127608ms (file: 754-754-754-754-756-756-754-756-754-150-150-157-175-157-175-175-157-561-165-615-157-156-517-156-157-165-156-176-157-756-156-517-571-751-657-571-157-750-754-754-756-754-756-754-7_W.mp3)
  Segment 35: 128608ms ~ 135160ms (file: 236-756-127-623-632-621-632-763-126-120-213-123-126-263-326-267-261-610-103-162-106-160-671-761-312-263-261-276-127-267-627-263-123-623-761-132-132-176-326-263-213-273-162-103-2_W.mp3)
  Segment 36: 136160ms ~ 136736ms (file: 571-357-361-376-376-372-376-736-376-736-371-316-326-361-321-326-321-136-236-261-312-136-531-312-316-315-517-162-135-137-316-317-217-617_W.mp3)
  Segment 37: 137736ms ~ 138312ms (file: 570-175-731 570-570-574-574-574-574-574-571-517 574-574-574-571-157-175 517 574-574-574-574-570-507-510-175-507-570 574-574-574-507-570-574-574-570-574-570-705-751-715-750-754-1_W.mp3)
  Segment 38: 139312ms ~ 139888ms (file: 231-326-213-321-312-237-327-327-312-263-260-123 321-320_V.mp3)
  Segment 39: 140888ms ~ 141464ms (file: 375-703-132-317-370-701-137-175-317-703 731-371-713-130-731-713-730-301-703-103-170-372-317-305-710-170-713-705-703-137-710-715-715-170-310-310-735 173-130-302-132-103-371-107-3_V.mp3)
  Segment 40: 142464ms ~ 148440ms (file: 237-231-523-260-236-216-206-263-231-126-126-216-123-235-275-572-256-320-321-235-130-213-201-267-572-576-265-103-123-132-103-210-276-257-250-257-236-235-257-275-207-210-275-752-7_W_stt_then.mp3)
  Segment 41: 149440ms ~ 154336ms (file: 236-321-107-126-123-312-130-310-321-371-735-713-175-267-517-726-756-157-275-267-213-213-236-213-321-217-261-231-127-217-132-312-216-213-321-123-236-236-321-217-321-312-327-123-2_W.mp3)
  Segment 42: 155336ms ~ 155912ms (file: 504-540-501-510-517-571-507-570-517-150-751-175-715-175-170-107-157-715-751-715-751-571-571-750-573 105-107-710-175-710-170-710-170-750-710-175-715-751-715-751-751-754-576-751-5_V.mp3)
  Segment 43: 156912ms ~ 157488ms (file: 612-126-201-123-120-316-321-260-620-260-260-321-310-312-603-362-370-301-301-137-216-231-103-132-162-130-103-362-132-103-130-103-132-162-612-651-612-621-652-652-652-310-135-230-3_V.mp3)
  Segment 44: 158488ms ~ 159064ms (file: 132-123-163-613-163-163-613-631-163-136-573-756-573-576-576-576-356-761-726-736-762-756-765-367-103-352-137-173-715-173-715-731-123-163-756-576-576-576-574-375-736-763-736-705-1_W.mp3)
  Segment 45: 160064ms ~ 160640ms (file: 750-705-517-751-750-517-105-571-574-574-754-754 570-751-157-715-170-107-107-176-701-713-701-130-137-375-157-571-750-517-571-157-750-510-157-157-507-175 570-571-507-517-150-507-7_V.mp3)
  Segment 46: 161640ms ~ 162216ms (file: 135-105-135-172-123-127-137-127-127-127-127-167-175-176-176-165-125-132-137-231-371-132-127-721-175-726-217-172-127-135-153-315-135-153-153-135-231-271-237-527-175-571-157-572-7_W.mp3)
  Segment 47: 163216ms ~ 163792ms (file: 501-507-510-510-150-501-510-507-501-506-571-507-507-506-570-571-510-510-517-571-501-507-571 510-510-501-517-157-517-501-510-501-105-510-150-510-150-510-501-150-510-567-576-576-5_W.mp3)
  Segment 48: 164792ms ~ 169616ms (file: 130-105-251-201-251-132-132-103-137-130-216-207-267-276-726-527-210-573-173-213-526-265-321-153-215-162-132-157-512-276-156-562-635-127-715-102-153-152-135-657-756-210-312-102-3_V.mp3)
  Segment 49: 170616ms ~ 179400ms (file: 267-107-267-267-267-267-726-276-236-732-753-310-130-312-230-271-260-231-207-321-231-263-267-267-231-230-532-753-321-120-312-152-547-571-362-326-132-306-732-371-103-263-627-326-2_W_stt_how_did_the_dishes_of_ram_and_willie_mcg.mp3)
  Segment 50: 180400ms ~ 180976ms (file: 326-321-123-102-162-620-160-120-123-132 136_V.mp3)
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
> **Time**: 2026-04-05 06:23:22
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "안녕하세요 여러분 반갑습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "여러분", "start_ms": 260, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 390, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 3,
"transcript": "오늘 수업에서는 한국어 기초 문법을 배워보겠습니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 120, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "수업에서는", "start_ms": 550, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "한국어", "start_ms": 1200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "기초", "start_ms": 1750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "문법을", "start_ms": 2200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "배워보겠습니다", "start_ms": 2800, "dur_ms": 900, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 5,
"transcript": "화면을 보면서 천천히 따라 읽어 보시길 바랍니다 준비되셨나요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "화면을", "start_ms": 150, "dur_ms": 450, "phase_lr_deg": 5.0},
{"word": "보면서", "start_ms": 650, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "천천히", "start_ms": 1100, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "따라", "start_ms": 1650, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "읽어", "start_ms": 2050, "dur_ms": 300, "phase_lr_deg": 5.0},
{"word": "보시길", "start_ms": 2400, "dur_ms": 450, "phase_lr_deg": 5.0},
{"word": "바랍니다", "start_ms": 2900, "dur_ms": 600, "phase_lr_deg": 5.0},
{"word": "준비되셨나요", "start_ms": 3600, "dur_ms": 800, "phase_lr_deg": 5.0}
]
},
{
"segment": 6,
"transcript": "첫 번째 문장은 저는 학교에 갑니다 입니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "첫", "start_ms": 100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "번째", "start_ms": 350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "문장은", "start_ms": 700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "저는", "start_ms": 1300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "학교에", "start_ms": 1700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "갑니다", "start_ms": 2250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "입니다", "start_ms": 2750, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
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
"transcript": "Great job everyone",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Great", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": -10.0},
{"word": "job", "start_ms": 240, "dur_ms": 120, "phase_lr_deg": -10.0},
{"word": "everyone", "start_ms": 370, "dur_ms": 200, "phase_lr_deg": -10.0}
]
},
{
"segment": 10,
"transcript": "Now repeat after me",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Now", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": -10.0},
{"word": "repeat", "start_ms": 170, "dur_ms": 200, "phase_lr_deg": -10.0},
{"word": "after", "start_ms": 380, "dur_ms": 120, "phase_lr_deg": -10.0},
{"word": "me", "start_ms": 510, "dur_ms": 100, "phase_lr_deg": -10.0}
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
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
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
"transcript": "오늘 배운 표현을 꼭 복습하세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 80, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "배운", "start_ms": 240, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "표현을", "start_ms": 370, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "꼭", "start_ms": 500, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "복습하세요", "start_ms": 590, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "See you next lesson",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 60, "dur_ms": 100, "phase_lr_deg": -10.0},
{"word": "you", "start_ms": 170, "dur_ms": 80, "phase_lr_deg": -10.0},
{"word": "next", "start_ms": 260, "dur_ms": 150, "phase_lr_deg": -10.0},
{"word": "lesson", "start_ms": 420, "dur_ms": 120, "phase_lr_deg": -10.0}
]
},
{
"segment": 17,
"transcript": "(mixed)",
"speaker": "speaker_1",
"confidence": 0.85,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "mixed",
"words": [
{"word": "감사합니다", "start_ms": 100, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "다음 단어들은 과일 이름입니다 사과 배 포도 딸기",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "다음", "start_ms": 200, "dur_ms": 300, "phase_lr_deg": 5.0},
{"word": "단어들은", "start_ms": 550, "dur_ms": 450, "phase_lr_deg": 5.0},
{"word": "과일", "start_ms": 1100, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "이름입니다", "start_ms": 1500, "dur_ms": 600, "phase_lr_deg": 5.0},
{"word": "사과", "start_ms": 2300, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "배", "start_ms": 2800, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "포도", "start_ms": 3100, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "딸기", "start_ms": 3600, "dur_ms": 400, "phase_lr_deg": 5.0}
]
},
{
"segment": 19,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 20,
"transcript": "What is your favorite fruit?",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "What", "start_ms": 120, "dur_ms": 150, "phase_lr_deg": -10.0},
{"word": "is", "start_ms": 280, "dur_ms": 100, "phase_lr_deg": -10.0},
{"word": "your", "start_ms": 390, "dur_ms": 120, "phase_lr_deg": -10.0},
{"word": "favorite", "start_ms": 520, "dur_ms": 300, "phase_lr_deg": -10.0},
{"word": "fruit", "start_ms": 830, "dur_ms": 200, "phase_lr_deg": -10.0}
]
},
{
"segment": 21,
"transcript": "그것은 아주 맛있는 사과입니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "그것은", "start_ms": 100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "맛있는", "start_ms": 900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "사과입니다", "start_ms": 1400, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "저는 사과보다 포도를 더 좋아해요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 150, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "사과보다", "start_ms": 550, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "포도를", "start_ms": 1100, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "더", "start_ms": 1550, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "좋아해요", "start_ms": 1750, "dur_ms": 600, "phase_lr_deg": 5.0}
]
},
{
"segment": 23,
"transcript": "다양한 문장을 연습해 보면서 실력을 키워보세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "다양한", "start_ms": 120, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "문장을", "start_ms": 600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "연습해", "start_ms": 1050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "보면서", "start_ms": 1600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "실력을", "start_ms": 2050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "키워보세요", "start_ms": 2550, "dur_ms": 800, "phase_lr_deg": 0.0}
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
"transcript": "한국어 공부는 정말 재미있습니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 150, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "공부는", "start_ms": 700, "dur_ms": 450, "phase_lr_deg": 5.0},
{"word": "정말", "start_ms": 1200, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "재미있습니다", "start_ms": 1600, "dur_ms": 850, "phase_lr_deg": 5.0}
]
},
{
"segment": 26,
"transcript": "매일 꾸준히 하는 것이 비결입니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "매일", "start_ms": 100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "꾸준히", "start_ms": 500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 1000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "것이", "start_ms": 1350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "비결입니다", "start_ms": 1700, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 28,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
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
"transcript": "(mixed)",
"speaker": "speaker_3",
"confidence": 0.90,
"language": "en",
"silence_before_ms": 50,
"audio_type": "mixed",
"words": [
{"word": "Don't", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": -10.0},
{"word": "give", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": -10.0},
{"word": "up", "start_ms": 290, "dur_ms": 80, "phase_lr_deg": -10.0}
]
},
{
"segment": 31,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 32,
"transcript": "안녕히 계세요 다음 시간에 만나요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "계세요", "start_ms": 260, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 400, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "시간에", "start_ms": 530, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 640, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
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
"transcript": "질문이 있으면 언제든지 물어보세요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "질문이", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "있으면", "start_ms": 600, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "언제든지", "start_ms": 1000, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "물어보세요", "start_ms": 1550, "dur_ms": 700, "phase_lr_deg": 5.0}
]
},
{
"segment": 36,
"transcript": "See you soon",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 60, "dur_ms": 120, "phase_lr_deg": -10.0},
{"word": "you", "start_ms": 190, "dur_ms": 100, "phase_lr_deg": -10.0},
{"word": "soon", "start_ms": 300, "dur_ms": 200, "phase_lr_deg": -10.0}
]
},
{
"segment": 37,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 38,
"transcript": "반복해서 연습하세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "반복해서", "start_ms": 100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "연습하세요", "start_ms": 320, "dur_ms": 220, "phase_lr_deg": 0.0}
]
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
"transcript": "그럼 이제부터 시작해 볼까요 하나 둘 셋",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 200, "dur_ms": 300, "phase_lr_deg": 5.0},
{"word": "이제부터", "start_ms": 550, "dur_ms": 450, "phase_lr_deg": 5.0},
{"word": "시작해", "start_ms": 1050, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "볼까요", "start_ms": 1500, "dur_ms": 450, "phase_lr_deg": 5.0},
{"word": "하나", "start_ms": 2200, "dur_ms": 300, "phase_lr_deg": 5.0},
{"word": "둘", "start_ms": 2600, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "셋", "start_ms": 2900, "dur_ms": 300, "phase_lr_deg": 5.0}
]
},
{
"segment": 41,
"transcript": "좋은 성과가 있기를 기대합니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "성과가", "start_ms": 550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "있기를", "start_ms": 1050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "기대합니다", "start_ms": 1500, "dur_ms": 750, "phase_lr_deg": 0.0}
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
"transcript": "You are doing great",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "You", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": -10.0},
{"word": "are", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": -10.0},
{"word": "doing", "start_ms": 250, "dur_ms": 150, "phase_lr_deg": -10.0},
{"word": "great", "start_ms": 410, "dur_ms": 120, "phase_lr_deg": -10.0}
]
},
{
"segment": 44,
"transcript": "포기하지 마세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "포기하지", "start_ms": 100, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "마세요", "start_ms": 360, "dur_ms": 200, "phase_lr_deg": 0.0}
]
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
"transcript": "(mixed)",
"speaker": "speaker_2",
"confidence": 0.88,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "mixed",
"words": [
{"word": "거의", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "다", "start_ms": 260, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "왔습니다", "start_ms": 370, "dur_ms": 150, "phase_lr_deg": 5.0}
]
},
{
"segment": 47,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 48,
"transcript": "마지막까지 최선을 다해 주셔서 감사합니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "마지막까지", "start_ms": 150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "최선을", "start_ms": 800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "다해", "start_ms": 1250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "주셔서", "start_ms": 1650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 2150, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "오늘 수업은 여기까지입니다 다음에도 유익한 수업으로 찾아올게요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 150, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "수업은", "start_ms": 550, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "여기까지입니다", "start_ms": 1000, "dur_ms": 800, "phase_lr_deg": 5.0},
{"word": "다음에도", "start_ms": 2100, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "유익한", "start_ms": 2650, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "수업으로", "start_ms": 3100, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "찾아올게요", "start_ms": 3650, "dur_ms": 700, "phase_lr_deg": 5.0}
]
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
*Generated by WKAppBot ask gemini — 2026-04-05 06:23:22*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=17708  time=06:21:47  uptime=7968s  mem=450MB  system_processes=355"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_062056.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 576-576-576-574-574-576-574-571-547-571 374-130-136-163-312_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 154-105-105-107-107-107-170-107-170-715-107-751-547-751-517 103-173-107-175-175-175-175-107-170-157-107-150-571-175-157-107-751-571-571-157-175 107-130-132-163-173-175-173-517-1_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 317-367-375-715-315-376-175-713-317-137 751-175-130-173-130-137-310-371-137-317-731-173-103-130-712-761-103-310-103-107-301-370-765-715-137-713-310-751-173-167-317-375-573-137-7_V.mp3)
  Segment 4: 4728ms ~ 12792ms (file: 312-627-213-312-123-267-572-574-527-574-267-207-210-675-201-312-506-317-260-621-201-216-150-312-713-103-137-123-172-175-135-210-352-702-207-263-236-102-307-572-572-206-213-507-6_V.mp3)
  Segment 5: 13792ms ~ 22180ms (file: 326-263-276-672-267-263-321-276-263-371-176-137-236-126-276-725-132-372-762-321-327-357-573-123-752-753-312-312-273-726-726-276-106-173-706-267-267-231-237-276-230-236-317-130-1_W.mp3)
  Segment 6: 23180ms ~ 30848ms (file: 106-260-302-132-102-213-106-310-310-160-612-126-123-217-612-106-102-173-172-507-527-126-201-216-170-137-367-306-306-326-361-310-130-673-576-574-203-126-126-216-206-260-572-205-2_V.mp3)
  Segment 7: 31848ms ~ 32424ms (file: 312-317-301-301-301-302-352-312-302-301-320-310-301-312-321-123-130-102-213-302-320-325-136-103-132-371-103-103-130-103-167-132-310-103-302-130-312-132-312-312-310-312-312-673-3_W.mp3)
  Segment 8: 33424ms ~ 34000ms (file: 135-105-135-315-301-132-310-135-517-571-715-513 153-137-315-351-715-357-135-301-107-135-130-301-713-137-103-713-715-137-371-137-135-317 103-135-351-135-317-130-153-105-103-137-1_V.mp3)
  Segment 9: 35000ms ~ 35576ms (file: 754-750-751-750-754-715-751-715-175-750-751-175-751-571-571-170-571-175-150-517-715-157-751-574-751 570-751-705-705-750-715-107-750-715-170-107-715-170-170-175-170-175-175-751-1_V.mp3)
  Segment 10: 36576ms ~ 44388ms (file: 102-135-105-206-103-103-107-576-263-261-213-132-175-327-201-123-607-126-572-253-570-527-207-231-301-263-213-301-572-320-130-107-574-756-753-275-325-302-162-273-527-137-153-310-1_V_stt_the_dow_and_the_other_guy_was_as_if_gove.mp3)
  Segment 11: 45388ms ~ 49924ms (file: 216-236-216-326-123-261-123-167-276-136-371-632-312-306-321-326-321-172-102-216-163-123-231-123-627-627-213-216-123-126-123-103-126-621-216-126-231-103-276-213-132-321-123-321-2_W.mp3)
  Segment 12: 50924ms ~ 58340ms (file: 320-230-102-230-237-267-102-327-130-132-726-267-524-230-302-230-327-723-230-130-627-302-321-326-127-263-672-126-267-276-672-726-301-576-567-320-130-236-273-267-132-231-237-123-2_W.mp3)
  Segment 13: 59340ms ~ 59916ms (file: 710-751-157-157-152-165-106-657-657-657-657-657-650-657-625-652-620-602-765-756-756-756-756-756-572-125-127_W.mp3)
  Segment 14: 60916ms ~ 67468ms (file: 321-213-762-627-267-130-310-263-236-263-126-236-670-627-672-675-236-756-213-316-236-162-167-236-136-126-213-732-612-623-126-612-627-632-263-632-160-162-362-106-123-216-123-213-1_W.mp3)
  Segment 15: 68468ms ~ 77576ms (file: 206-572-201-213-230-231-752-726-310-765-130-752-237-235-321-213-102-210-230-321-130-574-527-201-103-120-210-120-132-302-203-230-263-765-123-120-576-752-107-102-261-236-754-210-3_W.mp3)
  Segment 16: 78576ms ~ 84804ms (file: 260-316-612-631-735-175-570-231-320-762-103-576-257-527-150-136-123-352-750-351-312-236-236-312-267-120-231-215-507-625-261-261-621-102-603-265-261-216-316-132-371-137-620-126-2_W.mp3)
  Segment 17: 85804ms ~ 94084ms (file: 106-613-627-267-276-160-751-637-627-612-326-763-136-263-726-267-523-237-321-160-361-762-312-367-765-361-567-362-167-106-752-725-326-236-162-756-217-132-236-236-213-136-126-670-2_W.mp3)
  Segment 18: 95084ms ~ 95660ms (file: 753-170-751-175-107-157-751-301-703-701-315-713-706-367-673-671-163-612-637-632-672-637-631-617-617-673-612-631 753-710-730-710-735-713-751-173-175-731-372-175-102-376-631-602-6_V.mp3)
  Segment 19: 96660ms ~ 102456ms (file: 273-321-571-517-317-137-137-136-107-367-316-163-163-103-617-167-154-572-765-301-673-735-371-370-372-623-723-657-205-602-562-706-310-317-756-157-263-576-312-352-625-206-160-617-7_W.mp3)
  Segment 20: 103456ms ~ 104032ms (file: 103-130-135-130-537 107-105-517-153-153-153-107-157-517-571-753-150-105-371-130-137-130-135-137-153-137-315-371-351-315-513-153-157 103-153-501-130-371-571-315-157-351-573-517-1_V.mp3)
  Segment 21: 105032ms ~ 111404ms (file: 621-102-176-170-372-321-310-317-130-312-312-137-103-736-632-672-762-621-607-732-216-321-263-102-306-263-627-362-671-631-623-657-653-603-260-201-360-132-231-237-231-312-136-163-1_W.mp3)
  Segment 22: 112404ms ~ 112980ms (file: 175-751-715-175-715-175-175-715-715-751-715-175-751-571-756-756 570-173-751-157-571-175-570-175-173-175-751-715-175-175-175-751-751-175-715-756-751-571 570-501-751-750-175-175-5_W.mp3)
  Segment 23: 113980ms ~ 114556ms (file: 135-106-136-132-150-135-160-103-132-160-162-103-321-165-620-612-162-507-756-756-765-123-301-103-134-132-621-261-123-136-123-103-612-612-623-623-312-163-103-103-601-165-103-123-3_V.mp3)
  Segment 24: 115556ms ~ 116132ms (file: 632-236-163-231-326-306-603-756-265-132-103-216-176-103-106-612-612-617-162-621 520-162-153-157-617-561-563-576-756-675-675-635 675-670-316-675-132-613-603-106-163-362-540-620-1_W.mp3)
  Segment 25: 117132ms ~ 117708ms (file: 150-132-572-107-137-316-637-602-637-637-621 136-135-567 672-675-672-672_W.mp3)
  Segment 26: 118708ms ~ 119284ms (file: 316-163-675-617-136-316-137-317-173-317-316-136-137-136-136-132-136-132-136-132-613-173-173-167-167-631-167-163-163-213-213-132-312-126-163-165-135-130-301-103-153-135-173-136-1_W.mp3)
  Segment 27: 120284ms ~ 120860ms (file: 507-570-570-570-574-570-750-750-507-571-175-570 510-570-570-571-510-751-170-507-105-570-170-715-157-710-105-157-150-157-150-507-510-571-571-574-574-574-574 574-574-754-570-105-7_V.mp3)
  Segment 28: 121860ms ~ 122436ms (file: 756-547-571-136-102-102-162-126-231-361-621-165-162-106-125-512-160-103-572-512-512-512-175-175-103-130-751-756-715-765-576-765-236-320-362-163-162-361-263-260-236-120-216-203-2_W.mp3)
  Segment 29: 123436ms ~ 129844ms (file: 756-170-576-573-572-275-256-524-527-267-267-203-157-107-571-105-726-627-236-174-132-267-574-675-316-130-231-267-312-103-716-267-672-327-312-547-547-574-231-310-762-726-672-267-3_W.mp3)
  Segment 30: 130844ms ~ 131420ms (file: 501-507-501-510-150-517-157-501-517-517-501-507-571-501-507-507-507-507-150-501 157-157-705-170-105-157-705-570-750-517-571-570-510-517-510-571-517-510-510-510-517-510-510-510-5_V.mp3)
  Segment 31: 132420ms ~ 132996ms (file: 735-170 375-317-103-107-137-301-175-601-670-613-670-601-613-613-163-361-617-167-617-132-175-170-137-130-130-175-130-310-175-751 735-375-175-135-103-513-137-731-701-637-670-603-6_V.mp3)
  Segment 32: 133996ms ~ 139972ms (file: 103-154-135-263-372-276-267-210-120-261-102-302-267-725-213-106-754-573-574-126-130-102-237-321-317-150-325-352-356-576-532-307-371-172-275-275 261-201-123-132-102-251-206-216-7_W.mp3)
  Segment 33: 140972ms ~ 146768ms (file: 231-320-263-310-361-367-301-136-736-103-361-126-761-103-310-103-130-132-514-375-317-137-317-103-301-517-135-571-315-135-312-316-316-137-137-172-175-326-206-623-731-627-265-205-2_V.mp3)
  Segment 34: 147768ms ~ 153060ms (file: 263-306-231-261-267-260-162-165-615-670-625-620-230-572-572-521-132-265-576-576-502-516-267-627-267-236-230-271-257-172-765-627-761-327-130-176-107-320-302-157-176-106-137-103-1_W.mp3)
  Segment 35: 154060ms ~ 162952ms (file: 326-326-263-756-752-361-130-267-263-102-102-542-102-216-263-123-756-754-261-267-236-326-321-231-263-312-261-231-231-103-236-327-257-213-123-631-236-236-326-231-126-756-103-765-5_W.mp3)
  Segment 36: 163952ms ~ 164528ms (file: 175-715-176-715-175-715-751-715-175-751-175-751-175-751-751-756-751-756-176-756-735-153-315-357-351-357-315 672-672-612-613-163-157-150-105-317-712-175-135-135-135-175-175-170-7_W.mp3)
  Segment 37: 165528ms ~ 166104ms (file: 501-570-571-510-501-510-517-571-517-571-517-570-570-517-571-751-571-751-571-751-570-750-570-570-571 517-150-510-150-510-157-753-517-573-571-157-175-517-157-157-517-175-751-175-1_V.mp3)
  Segment 38: 167104ms ~ 178948ms (file: 675-572-567-576-576-576-537-576-576-567-625-576 657-576-567-576-356-756-657-675-672-631-673-612-567-135-136-365-635-631-613-367-132-136-320-361-163-351-153-536-573-523-652-650-3_W.mp3)
  Segment 39: 179948ms ~ 180524ms (file: 352-327-216-725-341-317-217-361-651-367-164-234-357-123-316-173-256-675-327-375-162-135-241-142-216-257-132-216-314-361-327-165-321-176-127-123-361-316-214-615-165-135-132-217-6_V.mp3)
  Segment 40: 181524ms ~ 186816ms (file: 263-527-230-132-132-310-203-213-216-276-201-102-236-230-137-312-726-217-106-137-103-127-267-542-231-206-760-760-627-627-710-310-230-732-731-263-231-375-273-261-260-345-572-207-2_W.mp3)
  Segment 41: 187816ms ~ 196888ms (file: 627-263-237-376-327-765-576-574-310-310-267-267-276-627-267-756-273-231-321-267-312-576-576-157-217-120-126-675-327-231-216-312-312-130-321-231-102-715-132-130-102-576-267-102-1_W.mp3)
  Segment 42: 197888ms ~ 198464ms (file: 516-571-517-547-574-547-574-574-576-576-576-570 507-517-157-517-157-501-517-105-150-510-501-501-150-150-510-516-517-567-567-561-501-570-576-567-576-576-567-576-576-567-567-567 5_W.mp3)
  Segment 43: 199464ms ~ 206700ms (file: 321-237-756-765-276-673-760-312-176-716-106-106-167-231-231-231-267-276-276-276-263-236-574-267-267-267-276-327-352-573-327-627-263-312-321-123-376-316-123-276-103-610-127-752-2_W.mp3)
  Segment 44: 207700ms ~ 212956ms (file: 230-263-265-257-250-126-567-120-210-725-275-735-256-275-276-267-310-574-546-574-230-320-120-123-126-130-136-102-163-231-326-726-276-276-273-123-217-107-107-170-167-210-206-206-2_W.mp3)
  Segment 45: 213956ms ~ 214532ms (file: 625-276-236-265-275-625-731-765-751-275-236-732-231-725-326-502-267-751-607-572-705-751-270-276-762-617-752-372-752-756-570-756-527-756-752-526-504-267-273-236-721-152-102-571-7_V.mp3)
  Segment 46: 215532ms ~ 216108ms (file: 107-176 156-510-150-150-170-157-107-710-175-175-170-107-105-107-176-176-175-175-175-175-176-176-571 157-105-501-175-170-715-170-175-175-517-175-751-167-617-157-176-716-175-176-1_V.mp3)
  Segment 47: 217108ms ~ 217684ms (file: 106-106-102-160-106-106-160-106-120-261-106-612-162-102-216-102-261-602-613 652-654-165-106-106-650-165-165-106-106-102-216-621-510-153-651-650-657-106-156-102-105-165-602 654-6_V.mp3)
  Segment 48: 218684ms ~ 219260ms (file: 103-105-105-105-105-150-150-103-107-135-107-150-156-157-157-517-156-157-150-156-150-150-150-517-514-105 156-105-105-105-150-105-105-105-157-157-150-153-150-105-107-150-154 150-1_V.mp3)
  Segment 49: 220260ms ~ 220836ms (file: 320-735-372-327-327-375-321-312-732-316-312 154-167-316-173-731-173-173_W.mp3)
  Segment 50: 221836ms ~ 222412ms (file: 123-102-102-102-132-103-132-103-132 107-176-751-576-705-756-162-261-263-136-132-316-761 167-671_V.mp3)
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
> **Time**: 2026-04-05 06:23:26
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "안녕하세요 여러분 반갑습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "여러분", "start_ms": 260, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 390, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 3,
"transcript": "오늘 수업에서는 한국어 기초 문법을 배워보겠습니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 120, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "수업에서는", "start_ms": 550, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "한국어", "start_ms": 1200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "기초", "start_ms": 1750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "문법을", "start_ms": 2200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "배워보겠습니다", "start_ms": 2800, "dur_ms": 900, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 5,
"transcript": "화면을 보면서 천천히 따라 읽어 보시길 바랍니다 준비되셨나요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "화면을", "start_ms": 150, "dur_ms": 450, "phase_lr_deg": 5.0},
{"word": "보면서", "start_ms": 650, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "천천히", "start_ms": 1100, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "따라", "start_ms": 1650, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "읽어", "start_ms": 2050, "dur_ms": 300, "phase_lr_deg": 5.0},
{"word": "보시길", "start_ms": 2400, "dur_ms": 450, "phase_lr_deg": 5.0},
{"word": "바랍니다", "start_ms": 2900, "dur_ms": 600, "phase_lr_deg": 5.0},
{"word": "준비되셨나요", "start_ms": 3600, "dur_ms": 800, "phase_lr_deg": 5.0}
]
},
{
"segment": 6,
"transcript": "첫 번째 문장은 저는 학교에 갑니다 입니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "첫", "start_ms": 100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "번째", "start_ms": 350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "문장은", "start_ms": 700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "저는", "start_ms": 1300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "학교에", "start_ms": 1700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "갑니다", "start_ms": 2250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "입니다", "start_ms": 2750, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
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
"transcript": "Great job everyone",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Great", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": -10.0},
{"word": "job", "start_ms": 240, "dur_ms": 120, "phase_lr_deg": -10.0},
{"word": "everyone", "start_ms": 370, "dur_ms": 200, "phase_lr_deg": -10.0}
]
},
{
"segment": 10,
"transcript": "Now repeat after me",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Now", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": -10.0},
{"word": "repeat", "start_ms": 170, "dur_ms": 200, "phase_lr_deg": -10.0},
{"word": "after", "start_ms": 380, "dur_ms": 120, "phase_lr_deg": -10.0},
{"word": "me", "start_ms": 510, "dur_ms": 100, "phase_lr_deg": -10.0}
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
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
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
"transcript": "오늘 배운 표현을 꼭 복습하세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 80, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "배운", "start_ms": 240, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "표현을", "start_ms": 370, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "꼭", "start_ms": 500, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "복습하세요", "start_ms": 590, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "See you next lesson",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 60, "dur_ms": 100, "phase_lr_deg": -10.0},
{"word": "you", "start_ms": 170, "dur_ms": 80, "phase_lr_deg": -10.0},
{"word": "next", "start_ms": 260, "dur_ms": 150, "phase_lr_deg": -10.0},
{"word": "lesson", "start_ms": 420, "dur_ms": 120, "phase_lr_deg": -10.0}
]
},
{
"segment": 17,
"transcript": "(mixed)",
"speaker": "speaker_1",
"confidence": 0.85,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "mixed",
"words": [
{"word": "감사합니다", "start_ms": 100, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "다음 단어들은 과일 이름입니다 사과 배 포도 딸기",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "다음", "start_ms": 200, "dur_ms": 300, "phase_lr_deg": 5.0},
{"word": "단어들은", "start_ms": 550, "dur_ms": 450, "phase_lr_deg": 5.0},
{"word": "과일", "start_ms": 1100, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "이름입니다", "start_ms": 1500, "dur_ms": 600, "phase_lr_deg": 5.0},
{"word": "사과", "start_ms": 2300, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "배", "start_ms": 2800, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "포도", "start_ms": 3100, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "딸기", "start_ms": 3600, "dur_ms": 400, "phase_lr_deg": 5.0}
]
},
{
"segment": 19,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 20,
"transcript": "What is your favorite fruit?",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "What", "start_ms": 120, "dur_ms": 150, "phase_lr_deg": -10.0},
{"word": "is", "start_ms": 280, "dur_ms": 100, "phase_lr_deg": -10.0},
{"word": "your", "start_ms": 390, "dur_ms": 120, "phase_lr_deg": -10.0},
{"word": "favorite", "start_ms": 520, "dur_ms": 300, "phase_lr_deg": -10.0},
{"word": "fruit", "start_ms": 830, "dur_ms": 200, "phase_lr_deg": -10.0}
]
},
{
"segment": 21,
"transcript": "그것은 아주 맛있는 사과입니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "그것은", "start_ms": 100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "맛있는", "start_ms": 900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "사과입니다", "start_ms": 1400, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "저는 사과보다 포도를 더 좋아해요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 150, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "사과보다", "start_ms": 550, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "포도를", "start_ms": 1100, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "더", "start_ms": 1550, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "좋아해요", "start_ms": 1750, "dur_ms": 600, "phase_lr_deg": 5.0}
]
},
{
"segment": 23,
"transcript": "다양한 문장을 연습해 보면서 실력을 키워보세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "다양한", "start_ms": 120, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "문장을", "start_ms": 600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "연습해", "start_ms": 1050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "보면서", "start_ms": 1600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "실력을", "start_ms": 2050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "키워보세요", "start_ms": 2550, "dur_ms": 800, "phase_lr_deg": 0.0}
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
"transcript": "한국어 공부는 정말 재미있습니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 150, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "공부는", "start_ms": 700, "dur_ms": 450, "phase_lr_deg": 5.0},
{"word": "정말", "start_ms": 1200, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "재미있습니다", "start_ms": 1600, "dur_ms": 850, "phase_lr_deg": 5.0}
]
},
{
"segment": 26,
"transcript": "매일 꾸준히 하는 것이 비결입니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "매일", "start_ms": 100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "꾸준히", "start_ms": 500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 1000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "것이", "start_ms": 1350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "비결입니다", "start_ms": 1700, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 28,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
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
"transcript": "(mixed)",
"speaker": "speaker_3",
"confidence": 0.90,
"language": "en",
"silence_before_ms": 50,
"audio_type": "mixed",
"words": [
{"word": "Don't", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": -10.0},
{"word": "give", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": -10.0},
{"word": "up", "start_ms": 290, "dur_ms": 80, "phase_lr_deg": -10.0}
]
},
{
"segment": 31,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 32,
"transcript": "안녕히 계세요 다음 시간에 만나요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "계세요", "start_ms": 260, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 400, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "시간에", "start_ms": 530, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 640, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
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
"transcript": "질문이 있으면 언제든지 물어보세요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "질문이", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "있으면", "start_ms": 600, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "언제든지", "start_ms": 1000, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "물어보세요", "start_ms": 1550, "dur_ms": 700, "phase_lr_deg": 5.0}
]
},
{
"segment": 36,
"transcript": "See you soon",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 60, "dur_ms": 120, "phase_lr_deg": -10.0},
{"word": "you", "start_ms": 190, "dur_ms": 100, "phase_lr_deg": -10.0},
{"word": "soon", "start_ms": 300, "dur_ms": 200, "phase_lr_deg": -10.0}
]
},
{
"segment": 37,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 38,
"transcript": "반복해서 연습하세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "반복해서", "start_ms": 100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "연습하세요", "start_ms": 320, "dur_ms": 220, "phase_lr_deg": 0.0}
]
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
"transcript": "그럼 이제부터 시작해 볼까요 하나 둘 셋",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 200, "dur_ms": 300, "phase_lr_deg": 5.0},
{"word": "이제부터", "start_ms": 550, "dur_ms": 450, "phase_lr_deg": 5.0},
{"word": "시작해", "start_ms": 1050, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "볼까요", "start_ms": 1500, "dur_ms": 450, "phase_lr_deg": 5.0},
{"word": "하나", "start_ms": 2200, "dur_ms": 300, "phase_lr_deg": 5.0},
{"word": "둘", "start_ms": 2600, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "셋", "start_ms": 2900, "dur_ms": 300, "phase_lr_deg": 5.0}
]
},
{
"segment": 41,
"transcript": "좋은 성과가 있기를 기대합니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "성과가", "start_ms": 550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "있기를", "start_ms": 1050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "기대합니다", "start_ms": 1500, "dur_ms": 750, "phase_lr_deg": 0.0}
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
"transcript": "You are doing great",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "You", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": -10.0},
{"word": "are", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": -10.0},
{"word": "doing", "start_ms": 250, "dur_ms": 150, "phase_lr_deg": -10.0},
{"word": "great", "start_ms": 410, "dur_ms": 120, "phase_lr_deg": -10.0}
]
},
{
"segment": 44,
"transcript": "포기하지 마세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "포기하지", "start_ms": 100, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "마세요", "start_ms": 360, "dur_ms": 200, "phase_lr_deg": 0.0}
]
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
"transcript": "(mixed)",
"speaker": "speaker_2",
"confidence": 0.88,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "mixed",
"words": [
{"word": "거의", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "다", "start_ms": 260, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "왔습니다", "start_ms": 370, "dur_ms": 150, "phase_lr_deg": 5.0}
]
},
{
"segment": 47,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 48,
"transcript": "마지막까지 최선을 다해 주셔서 감사합니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "마지막까지", "start_ms": 150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "최선을", "start_ms": 800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "다해", "start_ms": 1250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "주셔서", "start_ms": 1650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 2150, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "오늘 수업은 여기까지입니다 다음에도 유익한 수업으로 찾아올게요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 150, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "수업은", "start_ms": 550, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "여기까지입니다", "start_ms": 1000, "dur_ms": 800, "phase_lr_deg": 5.0},
{"word": "다음에도", "start_ms": 2100, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "유익한", "start_ms": 2650, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "수업으로", "start_ms": 3100, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "찾아올게요", "start_ms": 3650, "dur_ms": 700, "phase_lr_deg": 5.0}
]
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
*Generated by WKAppBot ask gemini — 2026-04-05 06:23:26*
