# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=29532  time=03:20:40  uptime=6534s  mem=1087MB  system_processes=364"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260404_032029.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 176-156-102-106-706-103-765-157-617-765-576-103-167-361-167-630-103-671-103-136-652-152-136-132-103-263 165-617_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 230-206-231-216-236-261-167-106-103-310-657-567-651-675-106-163-106-163-132-123-320-236-230 320 312-302-326-203_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 163-157-715-516-167-163-106-103-132-106-106-103-126-130-176-102-617-103-673-167-163-167-617-675-605-167-657-762-163-167-103-103-136-136-567-765-106-136-172-130-316-167-165-103-1_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 576-756-576-675-756-756-576-576-576-751-573-275-276-267-267-257-627-275-265-275-257-265-376-754-753-137-567-576-572-572-752-765-375-372-376-375-372-372 631-365-625-657 651-632-7_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 123-751-765-716-726 163-213-132-132-102-132-102-132-162-762-671-761-672-671-167-106 574-716-376-736_W.mp3)
  Segment 6: 7880ms ~ 17708ms (file: 672-671-267-321-132-216-263-310-326-132-327-231-267-312-267-103-213-260-201-136-321-260-261-236-206-102-123-261-263-106-103-574-276-261-213-267-102-123-216-267-301-103-267-267-1_W.mp3)
  Segment 7: 18708ms ~ 19284ms (file: 657-361-512-625-236-321-206-203-205-160-216-620-620-263-562-574-576-567-567-625-723-526-106-312-136-671-675-753-517-576-653-576-306-312-657-365-376-302-312-360-130-547-567-675-5_W.mp3)
  Segment 8: 20284ms ~ 25576ms (file: 260-325-102-265-327-526-273-130-103-315-573-357-765-126-735-765-376-327-130-276-207-725-132-310-312-513-312-103-361-137-315-136-231-236-230-236-321-137-315-263-206-125-105-567-2_W.mp3)
  Segment 9: 26576ms ~ 27152ms (file: 167-631-617 621-603-607-163-362-267-631 275-261-721-762-261-632 316-321-623-632-672-627-627-761-267-312 163-176 362_V.mp3)
  Segment 10: 28152ms ~ 28728ms (file: 615-516-513-615-615-165-657-615-653-651-106-106-301-126-652-567-526-653-657-657-567-657-652-652-653-326-365-562-356-652-560-567-527-572-526-576-762-675-675-653-356-657-563-561-3_W.mp3)
  Segment 11: 29728ms ~ 30304ms (file: 756-754-756-312-132-123-103-312-301-103-137-103-103-132-132 236 132-371-621-136-136-136-136-312-132-570-574-574-621-621-261 271-576-756-210-132-120-231-201-120-263-123-236-321-3_V.mp3)
  Segment 12: 31304ms ~ 39152ms (file: 263-321-312-172-127-231-213-263-231-137-765-130-267-236-231-261-267-216-267-267-176-170-756-276-672-236-123-261-267-261-321-267-267-752-765-132-237-276-627-567-267-267-276-756-3_W.mp3)
  Segment 13: 40152ms ~ 40728ms (file: 257-527-371-375-675-607-675-765-671-657-167-756-276-260-236-263-236-617-360-361-106-126-312-301-253-163-127-312-612-602-620-631-612-162-603-362-231-326-302-230-326-236-263-216-6_V.mp3)
  Segment 14: 41728ms ~ 52492ms (file: 267 723-236-123-312-257-273-275-271-213-102-123-103-270-203-103-130-765-756-672-267-756-275-627-267-725-276-267-267-231-136-721-763-362-260-263-261-765-230-276-126-765-276-567-6_W.mp3)
  Segment 15: 53492ms ~ 54068ms (file: 576 137-312-625-132-123-123-715-612-162-276-216-621-126-120-326-672-127-167-172-375-756-756-736-176-765-672-217-172-231-675-261-612-175-756-765-675-367-671-162-135-726-715-163-7_W.mp3)
  Segment 16: 55068ms ~ 55644ms (file: 103-213-176-371-163-136-176-176-574-125-136-165-671-362-263-165-167-106-130-126-102-731-763-236-136-136-615-160-612-635-657-123-546-162-627-162-613-136-513-125-216-106-561-265-5_W.mp3)
  Segment 17: 56644ms ~ 57220ms (file: 602 572-675-321-156-512-657-675-652-675-263-621-567_W.mp3)
  Segment 18: 58220ms ~ 58796ms (file: 136-132-136-132-167-627-167-675-136-163-102-620-623-132-316-136-130-103-136-615-517-263-316-136-753-136-136-163-103-567-623-106-163-130-167-351-612-167-136-123-765-136-103-103-1_V.mp3)
  Segment 19: 59796ms ~ 60372ms (file: 203-206-720-312-175-637-106-123-210-210-163-164-163-126-132-317-136-136-132-153-167-260-620-627-627-620-675-675-675-672-672-162-675-176-167-670-672-672-602-672-671 106-162-602-2_V.mp3)
  Segment 20: 61372ms ~ 67924ms (file: 215-271-723-127-761-751-267-273-751-317-715-276-267-130-130-310-312-213-103-261-206-137-107-310-267-726-276-762-726-756-756-752-752-752-672-301-302-310-130-167-137-271-671-627-1_W.mp3)
  Segment 21: 68924ms ~ 69500ms (file: 106-715-163-605-163-106-326-621-623-610-713-130-103-167-175-106-632-213-617-130-103-231-673 231-732 132-103-102-316-123-132-103-130-103-103-130-312 576-675-267-321-103-301-132-6_V.mp3)
  Segment 22: 70500ms ~ 71076ms (file: 163-103-130-163-130-312-103-516-613_V.mp3)
  Segment 23: 72076ms ~ 72652ms (file: 167-136-312-136-613 103-137-106-206-213-207-203-102-103-312-102-102-103-102-201-236-102-675-620-620-210-263-260-260-260-206-261-310-130-103 657-132-156-613-761-765-201-162-172-1_V.mp3)
  Segment 24: 73652ms ~ 74228ms (file: 672-765-675-672-567-567-576-756 316-135-163-213-312-312-321-612-263-623-126-163-623-357-356-357-375-376-365-375-371-327-621-316-310-321-312-316-136-356-351-365-516-105-301-205-3_W.mp3)
  Segment 25: 75228ms ~ 75804ms (file: 136-376-137-175-317-137-167-136-167-712-173-163-103-102-163-136-130-132-651-613 134-130-317-513-103-517-136-132-103-756-617-103-150-130-103-106-163-316-165-561-352-132-123-106-1_V.mp3)
  Segment 26: 76804ms ~ 80764ms (file: 721-753-751-217-562-357-753-751-756-567-517-571-765-735-376-762-672-367-160-136-610-236-671-673-765-716-170-675-672-627-627-267-673-716-132-761-167-563-367-630-216-657-652-627-5_W.mp3)
  Segment 27: 81764ms ~ 82340ms (file: 210-321-236-236-213-632-632-265-263-652-231-534-513-135-132-751-761-765-547-540-547-514-657-675-546-547-564-605-602-627-620-176-716-716-173-103-517-574-547-547-157-167-106-501-5_W.mp3)
  Segment 28: 83340ms ~ 83916ms (file: 312-321-132-136-316-612-601-632-613-612-316-276-103-136-103-103-103-162-326-310-301-360-132-123-312-671-367-672-137-132-301-103-163-130-310-321-126-361-367-312-310-132-731-231-1_W.mp3)
  Segment 29: 84916ms ~ 93772ms (file: 206-206-213-201-725-261-620-310-206-206-120-321-126-672-210-207-572-137-103-201-312-102-210-132-123-132-123-605-350-635-365-635-213-623-260-563-267-260-260-236-126-201-123-103-3_V.mp3)
  Segment 30: 94772ms ~ 95348ms (file: 137-102-103-310-132-326-316-310-620-312-123-657-125-312-137-127-123-310-102-153-135-360-750-176-132-512-756-751-126-132-132-137-207-123-123-216-237-162-132-312-123-137-761-327-3_W.mp3)
  Segment 31: 96348ms ~ 96924ms (file: 267-672-267-265-267-627-265-652-265-267-256-267-726-362-163-162-612-126-162-267-627-210-126-132-126-162-261-261-162-126-267-621-263-261-167-176-763-761-671-675-671-671-672-673-6_W.mp3)
  Segment 32: 97924ms ~ 98500ms (file: 261-216-326-263-203-620-672-672-762-273-312-103-132-103-136-503-517-263-413-132-167-206-206-627-612-216-216-231-312-621-671-316-136-612-623-162-617-136-765-763-716-625-173-312-5_V.mp3)
  Segment 33: 99500ms ~ 109220ms (file: 201-203-210-201-210-230-263-571-176-213-613-316-675-765-103-762-752-120-713-752-574-273-231-130-126-752-207-125-103-123-312-136-130-123-103-130-126-752-275-312-132-213-210-270-1_W.mp3)
  Segment 34: 110220ms ~ 110796ms (file: 672-637-675-672-761-761-673-320-325-321-325-302 132-321-732-267-756-627 103 156-675-670-371-137-132-137-123-127-231-126 165-567-260-156-576-236-326 752_W.mp3)
  Segment 35: 111796ms ~ 119032ms (file: 312-213-260-627-261-261-123-126-172-135-537-357-260-267-236-261-261-627-627-176-625-206-126-320-130-534-105-167-751-763-632-132-123-127-261-216-276-267-206-267-206-267-267-260-6_W.mp3)
  Segment 36: 120032ms ~ 127160ms (file: 102-237-312-132-320-301-621-625-625-167-213-302-236-567-265-265-265-230-263-120-260-126-261-126-123-102-263-103-160-206-263-230-265-132-235-216-162-265-260-627-236-123-126-120-2_W.mp3)
  Segment 37: 128160ms ~ 128736ms (file: 657-367-321-321-326-360-675-756-765-736-763-136-136-163-162-126-136-153-165-321-326-276-217-627-657-652-756-675-765-765-672-275-236-263-236-527-726-267-263-237-367-675-673-316-1_W.mp3)
  Segment 38: 129736ms ~ 135388ms (file: 130-265-103-127-267-574-756-157-275-216-206-267-215-752-627-271-526-276-672-672-201-507-153-617-751-265-162-610-601-612-752-251-152-120-123-517-517-157-517-517-153-357-157-175-1_W.mp3)
  Segment 39: 136388ms ~ 136964ms (file: 612-361-213-627-163-261-605-671-613-163-162-123 562-571-136-163-706-760-760-173-761-732-761-715-706-576-673-213-263-261-621-607-260-123-136-123-123-316-316-675-317 260-526-126-1_V.mp3)
  Segment 40: 137964ms ~ 138540ms (file: 103-132-103-106-132-671-627-167-627-657-160-130-675-675-675-236-126-312-321-316-362-620-361-735-360-367-625-652-623-306-316-361-621-612-675-675-672-672_W.mp3)
  Segment 41: 139540ms ~ 153508ms (file: 567-607-356-617-317-160-752-123-106-527-136-671-172-136-301-160-103-167-103-751-127-136-176-123-316-162-163-273-162-617-160-106-617-106-103-156-126-623-612-162-312-175 132-160-6_V.mp3)
  Segment 42: 154508ms ~ 155084ms (file: 760-763-376-371-567-567-360-537-672-627-263-623-620-263-236-301-312-320-132-103-103-571-156-106-167-136-167-167-126-216-201-260-260-267-260-260-260-607-567-567-105-231-123-102-1_V.mp3)
  Segment 43: 156084ms ~ 164832ms (file: 732-367-203-310-721-103-127-216-276-213-103-103-231-723-213-231-321-321-574-527-571-312-125-570-230-230-230-327-237-253-276-521-312-275-275-276-627-216-320-326-321-267-726-765-2_W.mp3)
  Segment 44: 165832ms ~ 166408ms (file: 761-763-765-725-367-126-163-102-312_W.mp3)
  Segment 45: 167408ms ~ 167984ms (file: 765-375-267-327-736-573-576-357-162-526-215-637-765-765-621-162 670-267-627-267-367-103-107-216-261-627-512-325-756-756-756-756-765-263-657-273-132-267-236-216-162-160-765-173-6_W.mp3)
  Segment 46: 168984ms ~ 177300ms (file: 276-716-162-106-175-310-271-163-261-263-756-172-512-547-542-231-653-370-752-237-372-216-106-130-275-132-273-123-236-735-312-627-263-321-321-126-130-210-261-102-236-321-231-160-1_W.mp3)
  Segment 47: 178300ms ~ 178876ms (file: 163-163-126-235-265-136-106-163-105-263-173-106-103-136-675-615-106-165-163-612-216-103-136-176-163-176-130-735-137-312-263-307-321-512-320-103-102-106-107-106-107-267-176 132_V.mp3)
  Segment 48: 179876ms ~ 180452ms (file: 576-621-132-167-163 123-132-657 167-216-672-672 162-156-625-123-176-167-236-162-126-136-623-167-167 312-317_W.mp3)
  Segment 49: 181452ms ~ 182028ms (file: 132-106-103-160-103-136-130-516-136-316-136-361-310-316-312-231-103-210-213-230-235-152-106-126-130-316-103-627-162-130-103-163-132-514-125-156-561-316-123-301-301-310-132-321-1_V.mp3)
  Segment 50: 183028ms ~ 183604ms (file: 651-356-675-653-536-567-651-567-653-567-651-657-765-567-651-765-375-615-615-675-651-657-657-562-653-365-153-631-165-675-675-657-165-615-653-165-156-165-156-516-516-561-657-567-6_W.mp3)
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
> **Time**: 2026-04-04 03:22:56
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "받아들였습니다 강태양 소년단의 이번 앨범 아리랑은 처음부터 끝까지 한국적인 정서를 품고 있습니다 하나의 팝 앨범이자 동시에 한국 문화에 대한 거대한 레퍼런스처럼 느껴질 정도인데요 전통 민요 아리랑의 선",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "받아들였습니다", "start_ms": 20, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "강태양", "start_ms": 720, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "소년단의", "start_ms": 1100, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "이번", "start_ms": 1550, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "앨범", "start_ms": 1780, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "아리랑은", "start_ms": 2050, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "처음부터", "start_ms": 2450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "끝까지", "start_ms": 2820, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "한국적인", "start_ms": 3170, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "정서를", "start_ms": 3650, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "품고", "start_ms": 3980, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 4220, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "하나의", "start_ms": 4600, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "팝", "start_ms": 4900, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "앨범이자", "start_ms": 5030, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "동시에", "start_ms": 5410, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 5750, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "문화에", "start_ms": 5980, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "대한", "start_ms": 6320, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "거대한", "start_ms": 6570, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "레퍼런스처럼", "start_ms": 6910, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "느껴질", "start_ms": 7520, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "정도인데요", "start_ms": 7860, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "전통", "start_ms": 8340, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "민요", "start_ms": 8600, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "아리랑의", "start_ms": 8840, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "선", "start_ms": 9250, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "법입니다 전업 투자자들이 하는 빠른 호흡의 매매가 아니더라도 주식을 할 수 있",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "법입니다", "start_ms": 20, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "전업", "start_ms": 400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "투자자들이", "start_ms": 680, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 1180, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "빠른", "start_ms": 1420, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "호흡의", "start_ms": 1690, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "매매가", "start_ms": 2030, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "아니더라도", "start_ms": 2380, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "주식을", "start_ms": 2860, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 3210, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 3350, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "있", "start_ms": 3470, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "는 종가 매매와 시가 매매 그리고 확률 높은 스케줄 트레이딩 등을 알차게 준비했습니다 자세한 내용은 고정",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "는", "start_ms": 30, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "종가", "start_ms": 170, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "매매와", "start_ms": 440, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "시가", "start_ms": 820, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "매매", "start_ms": 1090, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 1370, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "확률", "start_ms": 1720, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "높은", "start_ms": 1980, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "스케줄", "start_ms": 2250, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "트레이딩", "start_ms": 2670, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "등을", "start_ms": 3120, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "알차게", "start_ms": 3380, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "준비했습니다", "start_ms": 3750, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "자세한", "start_ms": 4450, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "내용은", "start_ms": 4830, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "고정", "start_ms": 5170, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "빵 먹으러 갈까 그 와중에도 아침 식사를 챙겨주겠다고 빵을 반으로 자르더니 동생에게 줄 접시를 잡고 인사를 와서 제대로",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "빵", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "먹으러", "start_ms": 190, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "갈까", "start_ms": 490, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 750, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "와중에도", "start_ms": 860, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "아침", "start_ms": 1230, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "식사를", "start_ms": 1460, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "챙겨주겠다고", "start_ms": 1790, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "빵을", "start_ms": 2460, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "반으로", "start_ms": 2720, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "자르더니", "start_ms": 3060, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "동생에게", "start_ms": 3460, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "줄", "start_ms": 3900, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "접시를", "start_ms": 4030, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "잡고", "start_ms": 4400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "인사를", "start_ms": 4670, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "와서", "start_ms": 5010, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "제대로", "start_ms": 5250, "dur_ms": 380, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "부탁드린다고 했는데 들어갔어요 이번에 1월달에 들어가지 아 아 예 예 감사합니다 언니 그 계산서 꼭 끊어주",
"speaker": "speaker_4",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "부탁드린다고", "start_ms": 30, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "했는데", "start_ms": 610, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "들어갔어요", "start_ms": 940, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이번에", "start_ms": 1420, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "1월달에", "start_ms": 1750, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "들어가지", "start_ms": 2190, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 2600, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 2710, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "예", "start_ms": 2820, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "예", "start_ms": 2950, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 3080, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "언니", "start_ms": 3620, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 3850, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "계산서", "start_ms": 3960, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "꼭", "start_ms": 4320, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "끊어주", "start_ms": 4460, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "린지 하지만 목이 메는 퍽퍽한 맛빵에 스카이는 짜증",
"speaker": "speaker_5",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "린지", "start_ms": 20, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "하지만", "start_ms": 250, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "목이", "start_ms": 590, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "메는", "start_ms": 830, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "퍽퍽한", "start_ms": 1090, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "맛빵에", "start_ms": 1490, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "스카이는", "start_ms": 1860, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "짜증", "start_ms": 2290, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "난 한국의 군수 산업 역량이 실제로는 대만이 감히 엄두조차 낼 수 없는 전혀 다른 차원으로 올라섰다며 대만 매체가 이를 대대적으로 극찬하고 나서면서 연일 한국을 상대로 날선 발등을 유발해 와 왔",
"speaker": "speaker_6",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "난", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "한국의", "start_ms": 170, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "군수", "start_ms": 500, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "산업", "start_ms": 760, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "역량이", "start_ms": 1030, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "실제로는", "start_ms": 1390, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "대만이", "start_ms": 1860, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "감히", "start_ms": 2200, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "엄두조차", "start_ms": 2440, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "낼", "start_ms": 2840, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 2970, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "없는", "start_ms": 3080, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "전혀", "start_ms": 3340, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "다른", "start_ms": 3590, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "차원으로", "start_ms": 3830, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "올라섰다며", "start_ms": 4200, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "대만", "start_ms": 4770, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "매체가", "start_ms": 5030, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "이를", "start_ms": 5360, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "대대적으로", "start_ms": 5590, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "극찬하고", "start_ms": 6190, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "나서면서", "start_ms": 6630, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "연일", "start_ms": 7130, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "한국을", "start_ms": 7390, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "상대로", "start_ms": 7760, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "날선", "start_ms": 8100, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "발등을", "start_ms": 8360, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "유발해", "start_ms": 8720, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "와", "start_ms": 9090, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "왔", "start_ms": 9220, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "레이더를 장착했고 적외선 탐색 및 추적 시스템과 통합 전자전 시스템까지 갖추고 있습니다 또한 동력 측면에서는 한국의 한화에어로스페이스가 미국 제너럴 일렉트릭과 이미 계약을 맺고 미국 본토에서 F414 엔진 40기를 생",
"speaker": "speaker_6",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "레이더를", "start_ms": 50, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "장착했고", "start_ms": 450, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "적외선", "start_ms": 890, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "탐색", "start_ms": 1260, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "및", "start_ms": 1560, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "추적", "start_ms": 1690, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "시스템과", "start_ms": 1950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "통합", "start_ms": 2420, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "전자전", "start_ms": 2670, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "시스템까지", "start_ms": 3040, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "갖추고", "start_ms": 3580, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 3940, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "또한", "start_ms": 4410, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "동력", "start_ms": 4670, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "측면에서는", "start_ms": 4940, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "한국의", "start_ms": 5440, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "한화에어로스페이스가", "start_ms": 5770, "dur_ms": 950, "phase_lr_deg": 0.0},
{"word": "미국", "start_ms": 6740, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "제너럴", "start_ms": 6970, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "일렉트릭과", "start_ms": 7310, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이미", "start_ms": 7780, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "계약을", "start_ms": 8010, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "맺고", "start_ms": 8370, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "미국", "start_ms": 8640, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "본토에서", "start_ms": 8880, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "F414", "start_ms": 9320, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "엔진", "start_ms": 9720, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "40기를", "start_ms": 9980, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "생", "start_ms": 10350, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "얘기 꺼내기가 어려웠을 것 같아요 근데 선배님이 아니 아니 너 하고 싶은 거 한번 다 해 라고 이제 선배님이 리액션 해주시니까 제",
"speaker": "speaker_7",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "얘기", "start_ms": 30, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "꺼내기가", "start_ms": 260, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "어려웠을", "start_ms": 630, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 1030, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 1160, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 1490, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "선배님이", "start_ms": 1730, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "아니", "start_ms": 2100, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "아니", "start_ms": 2300, "dur_ms": 170, "phase_lr_deg": 0.0},
{"word": "너", "start_ms": 2490, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "하고", "start_ms": 2620, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "싶은", "start_ms": 2850, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 3110, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "한번", "start_ms": 3240, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 3490, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "해", "start_ms": 3630, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "라고", "start_ms": 3800, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 4040, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "선배님이", "start_ms": 4240, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "리액션", "start_ms": 4610, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "해주시니까", "start_ms": 5010, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "제", "start_ms": 5550, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "방향을 못 잡고 있고요 그러면 시스템이 바뀌는 속도랑 내가 바뀌는 속도 중에서 어느 쪽이 더 빠를까요 스티븐 호킹은 이런 말을 하게 되었습니다 지성의",
"speaker": "speaker_8",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "방향을", "start_ms": 60, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "못", "start_ms": 390, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "잡고", "start_ms": 520, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "있고요", "start_ms": 780, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "그러면", "start_ms": 1120, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "시스템이", "start_ms": 1420, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "바뀌는", "start_ms": 1850, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "속도랑", "start_ms": 2190, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "내가", "start_ms": 2590, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "바뀌는", "start_ms": 2820, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "속도", "start_ms": 3160, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "중에서", "start_ms": 3420, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "어느", "start_ms": 3790, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "쪽이", "start_ms": 4020, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 4260, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "빠를까요", "start_ms": 4390, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "스티븐", "start_ms": 4860, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "호킹은", "start_ms": 5220, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 5590, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "말을", "start_ms": 5830, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "하게", "start_ms": 6090, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "되었습니다", "start_ms": 6320, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "지성의", "start_ms": 6890, "dur_ms": 310, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "듯한 남쪽 텍사스 시골 개들에게는 미네소타의 매운맛이 너무 강력했나 봅니다 눈물이 안 나고 모",
"speaker": "speaker_9",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "듯한", "start_ms": 20, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "남쪽", "start_ms": 280, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "텍사스", "start_ms": 580, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "시골", "start_ms": 950, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "개들에게는", "start_ms": 1280, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "미네소타의", "start_ms": 1820, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "매운맛이", "start_ms": 2320, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 2760, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "강력했나", "start_ms": 2990, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "봅니다", "start_ms": 3390, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "눈물이", "start_ms": 3730, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 4090, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "나고", "start_ms": 4230, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "모", "start_ms": 4490, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "가족들하고 오면 더 좋겠다 어디를 좀 부탁하지 민정이는 아버지하고 오기로 했고요 네 알겠습니다 예 감사합니다 가세요 어디 가시",
"speaker": "speaker_4",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "가족들하고", "start_ms": 50, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "오면", "start_ms": 550, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 810, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "좋겠다", "start_ms": 940, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "어디를", "start_ms": 1270, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 1610, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "부탁하지", "start_ms": 1750, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "민정이는", "start_ms": 2180, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "아버지하고", "start_ms": 2620, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "오기로", "start_ms": 3090, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "했고요", "start_ms": 3450, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "네", "start_ms": 3790, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "알겠습니다", "start_ms": 3920, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "예", "start_ms": 4490, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 4620, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 5160, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "어디", "start_ms": 5490, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "가시", "start_ms": 5750, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "면서 싼 데가 있고 어떤 데는 공산품이 같은 제품인데도 싼 데가 있고",
"speaker": "speaker_10",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "면서", "start_ms": 30, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "싼", "start_ms": 260, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "데가", "start_ms": 400, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "있고", "start_ms": 660, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 900, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "데는", "start_ms": 1150, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "공산품이", "start_ms": 1410, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "같은", "start_ms": 1810, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "제품인데도", "start_ms": 2070, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "싼", "start_ms": 2570, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "데가", "start_ms": 2710, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "있고", "start_ms": 2970, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "그래서 이제 고단한 몸을 이끌고 여러 곳을 돌아다니며 장을 보",
"speaker": "speaker_10",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "그래서", "start_ms": 20, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 320, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "고단한", "start_ms": 550, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "몸을", "start_ms": 910, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "이끌고", "start_ms": 1180, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "여러", "start_ms": 1510, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "곳을", "start_ms": 1770, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "돌아다니며", "start_ms": 2010, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "장을", "start_ms": 2510, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "보", "start_ms": 2770, "dur_ms": 110, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-04 03:22:56*
