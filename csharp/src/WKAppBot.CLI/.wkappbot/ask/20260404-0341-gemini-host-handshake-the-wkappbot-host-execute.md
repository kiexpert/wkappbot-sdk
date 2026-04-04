# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=16288  time=03:39:32  uptime=10983s  mem=730MB  system_processes=365"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260404_033517.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 567-574-574-123-213-263-236-612-623-752-752-625-206-135-301-130-301-623-102-301-651-561-130-132-162-132-136-102-126-127-524-316-136-126-136-316-163-106-167-103-176-170-376-136-1_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 761-160-632-167-136-136-132-103-103-106-130-163-312-102 167-216-207-135-103-132-216-132-261-276-137-132-601-267-623-167-163-312-165-360-756-572-571-715-102-162-263-123-132-136-2_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 163-617-167-126-671-631 762-276-326-216-621-216-260-263-261-123-216-263-213-236_W.mp3)
  Segment 4: 4728ms ~ 11784ms (file: 163-301-132-120-165-501-520-237-236-206-627-627-310-326-132-716-130-103-306-361-613-607-167-631-673-607-236-106-120-201-750-107-615-605-617-754-216-321-102-136-613-160-126-167-1_W.mp3)
  Segment 5: 12784ms ~ 13360ms (file: 517-507-570-126-132-216-160-617-160-672-263-126-501-531-571-573-132-103-136-132-536-132-617-103-167-136-136-102-162-361-310-301-136-106-132-136-316-716-163-157-162-621-236-103-1_V.mp3)
  Segment 6: 14360ms ~ 14936ms (file: 572-562-365-375-130-637-673-546-562-316-130-173-176-652-657-623-367-536-563-534-265-625-562-526-267-627-627-276-267-627-672-612-362-326-672-563-534-325-317-537-567-576-657-625-6_W.mp3)
  Segment 7: 15936ms ~ 16512ms (file: 162-130 162-162-162-671-126 263-316-672-165-672-621_V.mp3)
  Segment 8: 17512ms ~ 18088ms (file: 302-236-765-175-103-136-103-136-103-136-132-263-260-206-203-231-574-574-216-207-267-206-203-203-302-310-231-103-103-607-260-623-136-130-102-310-102-132-106-231-102-123-273-621-6_V.mp3)
  Segment 9: 19088ms ~ 19664ms (file: 267-301-530-103-136-106-136-163-162-126-137-206-321-103-623-236-375-137-261-312-713-132-312-314-163-136-132-637-613-163-301-312-631-607-623-672-162-103-613-316-537-530-302-301-3_V.mp3)
  Segment 10: 20664ms ~ 27396ms (file: 230-235-230-201-253-123-120-237-102-231-102-103-234-102-105-312-125-201-230-230-132-120-102-201-102-102-501-210-120-312-230-320-132-102-130-321-253-302-203-103-123-130-572-103-3_V.mp3)
  Segment 11: 28396ms ~ 28972ms (file: 301-103-213-312-632-632-123-230-514-162-526-160-103-163-136-316-136-165-160-162-102-163-136-601-163-613-162-216-103-167-136-165-152-126-156-162-132-172-167-103-162-165-126-103-1_V.mp3)
  Segment 12: 29972ms ~ 30548ms (file: 571-263-621-216-132-751-176-132-621-236-103-316-103-163-132-136-153 236_V.mp3)
  Segment 13: 31548ms ~ 32124ms (file: 672-672-627-672-673-672-762-672-627-627-672 567-267-261-267-263-126-132-574 627-263-136-362-321-312-621-632-362-675-675-657-625-567-567-756-165-167-135-136-132-231-326-123 136-1_W.mp3)
  Segment 14: 33124ms ~ 33700ms (file: 136-632-163-136-103-567-310-103-103-165-163-106-103-316-103-136-103-136-561-136-132-102-123-132-136-315-163-165-130-372-136-362-103-130-130-103-103-203 216-265-621-625-672-625-6_V.mp3)
  Segment 15: 34700ms ~ 35276ms (file: 310-153-216-621-172-261-132-162-106-103-126-136-617-176-106-627-126-162-163-106-136-361-136-613-756-103-106-106-157-612-163-261 123 175-163-613 162-162_V.mp3)
  Segment 16: 36276ms ~ 36852ms (file: 625-652-562-652 620-625-623-623-623-621-261-263-627-623-627-621-261-621-267-261-621-621-621-627-652-562-652-652-625-625-562-625-650 621-621-625-621-621-621-621-625-623-623-621-2_W.mp3)
  Segment 17: 37852ms ~ 38428ms (file: 574-576-756-572 716-761-507-705-756-706-756-756-216-213-716-715-576-576-751-675-127-172 136-162-136-132-216-152-103-674-106-136-163-132-125-136-132-567-163-103-173-103-103-761-7_W.mp3)
  Segment 18: 39428ms ~ 40004ms (file: 132-126-361-231-201-210-103-207-260-206-302-106-167-173-136-360-206-136-603-620-623-126-312-263-261-657-627-602-301-301-271-206-203-260-263-361-130-301-106-130-132-106-126-316-1_W.mp3)
  Segment 19: 41004ms ~ 52632ms (file: 265-567-576-625-620-627-210-257-201-210-123 135-103-620-206-627-675-106-276-267-267-362-176-756-175-215-210-123-674-617-361-715-261-602-260-601-106-675-267-562-625-652-260-326-3_W.mp3)
  Segment 20: 53632ms ~ 54208ms (file: 650-620-625-623-637 625-602-602-605-605-605-605-657-657-605-605-605-605-605-650-650-605-602-602-607-603-623 605-605-605-657-605-605-605-657-567-657-657-650-605-506-560-650-560-6_V.mp3)
  Segment 21: 55208ms ~ 55784ms (file: 176-167-167-126-672-751-130-301-301-316-136-103-137-107-615-657-762-237-574-574-507-513-507-625-627-672-576-572-256-756-756-675-625-672-726-267-750-572-756-572-106-136-143-124-1_W.mp3)
  Segment 22: 56784ms ~ 60312ms (file: 132-210-126-263 267-276-637-325-267-623-627-602-216-136-673_W.mp3)
  Segment 23: 61312ms ~ 61888ms (file: 136-213-502-567-651-162-621-206-267-126-627-126-326-126-321-301-301-301-321-623-162-132-103-130-310-756-670-672-260-206-310-127-310-317-156-132-361-605-675-576-625-672-625-621-6_V.mp3)
  Segment 24: 62888ms ~ 70592ms (file: 132-130-130-173-103-130-103-120-103-103-132-130-102-103-310-132-321-132-123-132-302-321-327-312-310-132-130-162-130-130-312-130-312-132-132-132-312-312-517-125-123-132-130-312-3_V.mp3)
  Segment 25: 71592ms ~ 72168ms (file: 672-576-756-571-571-571-570-752-752-705-163-173-123-712-763 265 675_V.mp3)
  Segment 26: 73168ms ~ 73744ms (file: 103 527-567-136-316-315-536-573-571-156-516-157-576-517-157-567-162-536-675-765-657-753-572-536-571-576-576-576-356-103 513-756-627-276-762-765-675-175-167-123-312-136-167-517-5_W.mp3)
  Segment 27: 74744ms ~ 75320ms (file: 132-301-170-106-547-574-570-256-547-547-540-715-176-613-675-675-136-137-132-312-132-132-103-312-103-103-310-310-136-310-130-203-762-761-561-206-672-160-106-165-176-657-762-327-1_W.mp3)
  Segment 28: 76320ms ~ 76896ms (file: 576-751-170-176-176-167 723-143-365-213-263 263-326-236_W.mp3)
  Segment 29: 77896ms ~ 87472ms (file: 310-571-321-103-103-754-672-257-210-130-527-162-621-754-756-105-237-273-541-735-351-542-152-310-571-237-321-573-301-276-132-136-765-735-715-103-173-107-310-756-350-732-312-715-7_W.mp3)
  Segment 30: 88472ms ~ 89048ms (file: 765-762-762-136-612 763 623 621 132-163 125-612 213-613-103 312-316_V.mp3)
  Segment 31: 90048ms ~ 90624ms (file: 567-657-576-567-567-576-576-756-576-576-567-136-756-625-675-623-632-576-316-356-576-672-651-165-765-627-625-162-263-613-631-165-365-502-130-135-127-672-621-517-135-576-352-567-7_W.mp3)
  Segment 32: 91624ms ~ 92200ms (file: 103-135 627-312-103-132-310-310-312-360-620-603-620-602-602-236-530-302-302-632-603-623-263-530-302-302-326-250-526-527-532-231-213-201-231-576-756-765-576-567-560-236-203-754-7_V.mp3)
  Segment 33: 93200ms ~ 93776ms (file: 103-132-103-723-127-625-765-631 627 321-312-273-237-320-236-316-321-236 562-652-572-576-564-652-156-567-756-670_W.mp3)
  Segment 34: 94776ms ~ 95352ms (file: 103-310-136-365-120-621-321-126-163-136-312-153-563-562-326-321-231-312-312-236-352-625-562-236-326-653-312-365-236-652-362-263-362-361-136-612-625-621-236-261-216-231-523-253-3_W.mp3)
  Segment 35: 96352ms ~ 96928ms (file: 307-302-301-132-103-132-132-132-167-167-167-312-103-136-106-103-103-207-203-210-570-574-567-675-705-761-756-176-137-103-130-312-302-103-231-130-321-321-231-312-132-106-160-706-7_V.mp3)
  Segment 36: 97928ms ~ 98504ms (file: 675-657-712-327-672-263-756-765-763-762-715-276-673-627-623-627-265-623-613-231-321-672-621-627-632-263-320-326-260-261-263-213-321-123-321-765-675-632-361-157-617-671-612-376-7_W.mp3)
  Segment 37: 99504ms ~ 100080ms (file: 316-361-261-132-103-126-130-310-321-310-103-320-105-165-276-612-625-567-765-136-176-761-673-672-675-672-762-267-576-657-672-167-162-562-632-675-632-631-675-672-657-675-623-657-6_W.mp3)
  Segment 38: 101080ms ~ 101656ms (file: 125-167 267-216-506-137-361-613-103-671-103-312-761-167-725-136-126-107-103-103-130-163-160-165-136-617-601-126-326-612-163-167-103-301-310-567-672-162-136-651-132-136-173-106 6_V.mp3)
  Segment 39: 102656ms ~ 103232ms (file: 316-165-657-756-651-163-106-310-617-136-163-132-165-612-176-651-106-103-175-106-617-163-301-103-163-130-163-627-176-160 716-137-106-567-103-136-106-567-671-165-314-761-263-167-1_V.mp3)
  Segment 40: 104232ms ~ 104808ms (file: 267-237-237 567-675-623-762-671 765-671-672-162-261-123-126-136-136-162-167-163-312-120-120-126-326-310-206-206-203-236-326-123-301-136-162-123-301-302-301-132-132-103 130-754-7_V.mp3)
  Segment 41: 105808ms ~ 106384ms (file: 231-321-173-136-135-126-213 672-602-134-135-312-103-103-126-103-103-106-136-120-361-167-126-163-106-135-561-163-102-752-102-167-130-610-126-136-132-123-132-132-132-102-163-103-3_V.mp3)
  Segment 42: 107384ms ~ 107960ms (file: 216-137 612 217-103-126-756-167-126-162-621-175-162-126-106-163-162-106-126-610-106-516-621-256 103-316-231-302-163-132-102-615-261-617-162-136-125-130-132-167-516-102-106-103-3_V.mp3)
  Segment 43: 108960ms ~ 109536ms (file: 547-327-532-123-127 106-163-163-136-132-123-160-653-625-625-265-265-265-263-623-612-621-631-216 613-362 620-621-612-167_W.mp3)
  Segment 44: 110536ms ~ 111112ms (file: 260-621-261-620-625-267-267-260-120-512-526-546-564-546-265-312-756-567-564-540-256-257-312-216-540-574-572-257-207-267-265-312-315-514-576-657-652-516-325-156-450-265-627-625-5_W.mp3)
  Segment 45: 112112ms ~ 112688ms (file: 632-635-536-635-261 541-576-751-574-756-362-236-162-625-261-263-213-521-576-576-756-576-574-253-265-627-725-567-657-621-267-526-256-576-526-621-615-564-352-572-657-253-325-572-5_W.mp3)
  Segment 46: 113688ms ~ 114264ms (file: 312-132-132-132-312-327-570-573-531-253-372-326-306-312-173-175-715-713-173-173-172-317-173-137-175-175-752-371-137-125-175-321-312-321-321-312-723-713-725-723-321-132-312-321-3_W.mp3)
  Segment 47: 115264ms ~ 115840ms (file: 325-325-312-321-163-613-136-610-316-132-103-136-136-125-132-103-132-610-316-321-136-671-761-760-531-523-315-107-316-321-327-327-675-153-157-153-156-513-132-103-153-137-256-756-7_V.mp3)
  Segment 48: 116840ms ~ 117416ms (file: 263-236-263-623-263-263-263-263-261-263-263-267-623-236-267-263-236-263-326-236-261-321-231-632-261-321-326-261-123-631-362-276-627-623-632-326-362-673-623-623-625-562-563-536-3_W.mp3)
  Segment 49: 118416ms ~ 118992ms (file: 610-671-162-103-162-103-103-132-130-137-167-106-136-162-160-132-136-132-261-165-107-263-326 213 162-137-106-136-103-316-561-625-167-136-136-130-163-312-162-136-130-312-103-561-1_V.mp3)
  Segment 50: 119992ms ~ 120568ms (file: 632-602-126-126-627-312-162-326-672-612-263-162-631-361-623-627-627-627-316-613 621-615-361-263-216-672-625-625-265-263-621-567-167-126-620-621-263-231-163-625-123-267-621-312-2_W.mp3)
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
> **Time**: 2026-04-04 03:41:13
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "이 아이로스 학회는 보통 한 논문 4000편 정도가 제출이 되고",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 20, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "아이로스", "start_ms": 70, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "학회는", "start_ms": 180, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "보통", "start_ms": 260, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 320, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "논문", "start_ms": 360, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "4000편", "start_ms": 410, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "정도가", "start_ms": 490, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "제출이", "start_ms": 530, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "되고", "start_ms": 560, "dur_ms": 16, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "그중에서 한 반 정도가 이제 억셉을 받고요 1000",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "그중에서", "start_ms": 30, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 150, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "반", "start_ms": 200, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "정도가", "start_ms": 260, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 340, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "억셉을", "start_ms": 390, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "받고요", "start_ms": 480, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "1000", "start_ms": 550, "dur_ms": 26, "phase_lr_deg": 0.0}
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
"transcript": "지금처럼 유튜브를 운영하고 사업을 만들고 좋은 기회들은 가능성 없었을 거예요 현재 저를 평가하는 기준은 학위가 아니라 지금까지 제가 만들어온 결과물들이기 때문입니다 옛날",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "지금처럼", "start_ms": 10, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "유튜브를", "start_ms": 400, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "운영하고", "start_ms": 820, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "사업을", "start_ms": 1210, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "만들고", "start_ms": 1570, "dur_ms": 360, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 1970, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "기회들은", "start_ms": 2290, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "가능성", "start_ms": 2680, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "없었을", "start_ms": 3040, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 3380, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "현재", "start_ms": 3770, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "저를", "start_ms": 4090, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "평가하는", "start_ms": 4380, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "기준은", "start_ms": 4740, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "학위가", "start_ms": 5060, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 5400, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "지금까지", "start_ms": 5720, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 6100, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "만들어온", "start_ms": 6360, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "결과물들이기", "start_ms": 6750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "때문입니다", "start_ms": 7140, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "옛날", "start_ms": 7530, "dur_ms": 22, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "생명을 구할 수 있어 다행히 하늘이 무너지라는 법은 없나 봅니다 폭설이 잦은 지역인 만큼 익숙한 듯 제설차가 나타나서 도로를 청소해 줍니다 제설",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "생명을", "start_ms": 20, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "구할", "start_ms": 60, "dur_ms": 25, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 90, "dur_ms": 15, "phase_lr_deg": 0.0},
{"word": "있어", "start_ms": 110, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "다행히", "start_ms": 140, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "하늘이", "start_ms": 180, "dur_ms": 35, "phase_lr_deg": 0.0},
{"word": "무너지라는", "start_ms": 220, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "법은", "start_ms": 270, "dur_ms": 25, "phase_lr_deg": 0.0},
{"word": "없나", "start_ms": 300, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "봅니다", "start_ms": 330, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "폭설이", "start_ms": 370, "dur_ms": 35, "phase_lr_deg": 0.0},
{"word": "잦은", "start_ms": 410, "dur_ms": 25, "phase_lr_deg": 0.0},
{"word": "지역인", "start_ms": 440, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "만큼", "start_ms": 480, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "익숙한", "start_ms": 510, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "듯", "start_ms": 545, "dur_ms": 10, "phase_lr_deg": 0.0},
{"word": "제설차가", "start_ms": 560, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "나타나서", "start_ms": 610, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "도로를", "start_ms": 660, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "청소해", "start_ms": 700, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "줍니다", "start_ms": 750, "dur_ms": 25, "phase_lr_deg": 0.0},
{"word": "제설", "start_ms": 780, "dur_ms": 20, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 7,
"transcript": "왔거든요",
"speaker": "speaker_4",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "왔거든요", "start_ms": 10, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "원래 옛날 관점으로",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "원래", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "옛날", "start_ms": 180, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "관점으로", "start_ms": 310, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "매매를 했으면 그날 아마 풀베팅을",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "매매를", "start_ms": 10, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "했으면", "start_ms": 160, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "그날", "start_ms": 290, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "아마", "start_ms": 400, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "풀베팅을", "start_ms": 490, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "해서 다음날 장렬히 전사했을 거예요 근데 이제 형님은 이제 비중 조절을 잘하시는데",
"speaker": "speaker_4",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "해서", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "다음날", "start_ms": 140, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "장렬히", "start_ms": 270, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "전사했을", "start_ms": 380, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 540, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 630, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 710, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "형님은", "start_ms": 780, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 880, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "비중", "start_ms": 950, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "조절을", "start_ms": 1040, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "잘하시는데", "start_ms": 1150, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "있을 때 내가 이제 비중 베팅을 하기 위해서 참고 있는 거다 근데 시나리오대로 하셨고 오늘 이제 말씀하신 대로 이제 일당 1억을 찍으셔 가지고 딱 원하시는 대로 다 됐잖아요 음 계획대로 기분 좋아 그거",
"speaker": "speaker_5",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "있을", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 130, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "내가", "start_ms": 190, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 260, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "비중", "start_ms": 330, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "베팅을", "start_ms": 410, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "하기", "start_ms": 510, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "위해서", "start_ms": 580, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "참고", "start_ms": 670, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 750, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "거다", "start_ms": 820, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 880, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "시나리오대로", "start_ms": 960, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "하셨고", "start_ms": 1100, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 1210, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 1280, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "말씀하신", "start_ms": 1340, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "대로", "start_ms": 1470, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 1540, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "일당", "start_ms": 1600, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "1억을", "start_ms": 1670, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "찍으셔", "start_ms": 1760, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "가지고", "start_ms": 1860, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "딱", "start_ms": 1960, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "원하시는", "start_ms": 2010, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "대로", "start_ms": 2120, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 2190, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "됐잖아요", "start_ms": 2240, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "음", "start_ms": 2350, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "계획대로", "start_ms": 2400, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "기분", "start_ms": 2520, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "좋아", "start_ms": 2590, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "그것", "start_ms": 2660, "dur_ms": 50, "phase_lr_deg": 0.0}
]
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 26,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
*Generated by WKAppBot ask gemini — 2026-04-04 03:41:13*
