# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=10512  time=03:21:58  uptime=7051s  mem=186MB  system_processes=365"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260404_032101.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 263-236-231-236-231-123-236-231-326-321-316-310-231-236-326-136-326-123-312-210-321-306-361-326-632-321-123-312-362-261-132-261-230-123-360-326-302-321-321-326-321-132-312-362-6_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 106-163-126-136-163-610-316-136-361-132-103-103-306-326-301-301-130-106-210-302-326-260-620-260-620-602-602-602-620-302-320-326-203-326-362-126-103-163-136-162-613-650-167-123-1_V.mp3)
  Segment 3: 3152ms ~ 4592ms (file: 657-167-576 631-103-617 361-637-123_W.mp3)
  Segment 4: 5592ms ~ 6168ms (file: 726-572-567-650-576-657-567-527-627-265-267-627-523-235-261-213-132-312-103-132-601-163-132-715-576-576-756-627-675-167-526-657-675-675-673-672-672-675_W.mp3)
  Segment 5: 7168ms ~ 7744ms (file: 275-327-375-327-527-257-263-267-517-531-573-567-675-276-725-752-572-517-753-735-540-574-527-527-271-756-736-761-261-673-675-572-572-573-574-574-576-567-527-752-627-261-625-527-5_W.mp3)
  Segment 6: 8744ms ~ 9320ms (file: 672-123 672-672-672-672-672-602-172-376-736-736-732_V.mp3)
  Segment 7: 10320ms ~ 10896ms (file: 310-130-310-103-310-103-710-310-306-130-310-263-260-260-263-206-263-623-206-630-316-170-103-160-136-301-136-162-130-321-301-103-213-231-132-130-102-312-310-302-321-365-132-163-1_V.mp3)
  Segment 8: 11896ms ~ 18952ms (file: 712-132-236-216-326-312-321-163-612-127-173-716-362-127-637-103-120-612-102-213-267-120-326-236-352-503-315-206-203-230-312-312-136-653-512-321-267-712-756-623-106-135-231-362-7_W.mp3)
  Segment 9: 19952ms ~ 20528ms (file: 321-263-236-547-576-547-237-316-536-657-652-652-267-267-627-267-627-256-547-657-652-561-653-657-672-267-327-312-543-546-527-235-547-527-543-512-231-312-132-137-173-132-670-607-6_W.mp3)
  Segment 10: 21528ms ~ 22104ms (file: 576-675-574-574-102-312-310-765-765-760-760-765-672-265-657-576-675-762-267-316-657-756-672-671-617-567-512-501-560-562-562-162 625-652 176_W.mp3)
  Segment 11: 23104ms ~ 23680ms (file: 106-765-103-137-213-761-361-167-216-103-162-123-167-123-136-620-206-620-312-261-162-576-756-756-765-752-765-367-372-132-213-413-132-165-132-271-513-143-312-132-316-315-316-756-7_V.mp3)
  Segment 12: 24680ms ~ 25256ms (file: 123-162-160-106-105-521-125-132-216-123-153-152-165-156-514-513-153-156-251-231-123-651-651-361-156-126-165-162-126-561-215-532-321-321-325-125-321-351-325-321-312-132-152-521-5_W.mp3)
  Segment 13: 26256ms ~ 26832ms (file: 165-175-627-267 756 136-132-136-106-103-136-327-671-136-130-512-137-213-617-312-136-132-512-156-103-130-136-715-316-613-136-316-103-167-153-136-137-263-216-132-103-351-316-103-1_V.mp3)
  Segment 14: 27832ms ~ 28408ms (file: 620-620-625-625-625-625-265-625 620-627-621-625-267-625-627-267-267-267-261-261-263-261-263-621-620-623-623-265-625-625-625-625 620-625-265-625-625-625-265-267-267-265-267-267-2_W.mp3)
  Segment 15: 29408ms ~ 29984ms (file: 173-736-130-671-610-310-160-637-163-103-601-617-571-706-106-103-106-156-170-612-125-106-236-126-621-671-670_W.mp3)
  Segment 16: 30984ms ~ 31560ms (file: 715-765-106-260-172-162-261-301-103-103-136-106-670-163-106-517-136-103-135-163-167-261-162-153-132-132-610-153-361-176-631-136-123-351-162-120-106-136-631-103-162-230-126-216-7_V.mp3)
  Segment 17: 32560ms ~ 33136ms (file: 132-312-132 673-671-675-627-632-623-572-571-375-135-156-507-517-576-507-135-135-132-213-325-356-567-357-312-635-375-637-726-612-361-316-362-231-612-261-235-573-376-362-613-136-3_W.mp3)
  Segment 18: 34136ms ~ 34712ms (file: 576-567-276-273-726-567-576-576-567-567-572-723-716-136-301-130-637-632-631-136-673-617-136-362-130-360-123-130-673-103-316-130-132-136-103-301-103-126-103-312-123-136-216-301-1_V.mp3)
  Segment 19: 35712ms ~ 36288ms (file: 632-612-623-157-162-602-602-362-361-152-132-260-362-136-130 103-671-107-125-136-172-167-103-106-165-167-106-176-137-165-137-160-157-176-217-127-103-162-163-107-106-103-671-163-1_V.mp3)
  Segment 20: 37288ms ~ 37864ms (file: 163-261-236-326-320-103-163-130-165-312-617-235-162-162-327-631-103-326-167-610-261-351-752-725-302-320-132-321-526-163-106-361-675-675-617-126-102-103-173-106-132-103-317-753-5_W.mp3)
  Segment 21: 38864ms ~ 39440ms (file: 163 132-261-632-637-763-673-136-613-316-167-316-130-132-136-326-163-106-163-136-103-136-761-167-316-163-130-103-751-136-106-167-136-160-167-310-165-162-310-275-136-167-216-326-1_W.mp3)
  Segment 22: 40440ms ~ 41016ms (file: 160-132-103-132-657-576-752-263-167-671-136-324-106-167-163-362-576-312-106-130-136-103-106-103-671-576-176-612-617-106-631-617-610-165-165-163-517-326-627-137-163-136-106-163-7_V.mp3)
  Segment 23: 42016ms ~ 42592ms (file: 672-130-167-267-756-351-627-623-621-527-256-253-261-267-210-726-203-102-261-132-312-132-310-326-623-263-362-451-615-765-765-175-152-574-671-175-167-751-675-176-761-617-617-361-6_W.mp3)
  Segment 24: 43592ms ~ 44168ms (file: 761-160-607-106-670-176-730-107-107-617-167-163-167-107-163-160-167-617-765-163-162-130-701-107-173-106-623-132-360-160-574-127-137 103-136-613 546-637-167-160-670-167-130-163-1_V.mp3)
  Segment 25: 45168ms ~ 45744ms (file: 657-265-726-675-657-625-263-267-627-261-261-632-326-132-103-612-216-612-627-627-726-617-610-162-617-612-362-632-216-632-613-623-213-163-621-612-102-123-213 621-261-326-123-321-6_W.mp3)
  Segment 26: 46744ms ~ 47320ms (file: 367-236-257-765-675-765-765-675-765-765_W.mp3)
  Segment 27: 48320ms ~ 48896ms (file: 671-617-635-132-213-265-672-675-627-657-675-657-675-675-672-672-675-672-672-672-672-632-672-675-672-765-765-765-675-765-675-675-765-765-627-672-627-637-762-367-760-271-301-317-3_W.mp3)
  Segment 28: 49896ms ~ 50472ms (file: 103-752-275-326-362-750-213-263-725-261-217-527-132-627-263-327-312-236-761-132-317-172-103-137-716-712-517-316-567-576-713-572-567-572-562-250-273-206-265-523-627-627-672-652-6_V.mp3)
  Segment 29: 51472ms ~ 52048ms (file: 230-103-267-236-261-236-132-236-623-261-162-657-265-267-672-162-263-612-162-621-612-612-236-627-567-715_W.mp3)
  Segment 30: 53048ms ~ 53624ms (file: 576-574-576-576-670-167-167-167-167-164-621-362-106-261-312-675-671-613-670-756-671-574-576-675-302-130-310-754-756-637-206-362-163-670-567-657-756-621-567-260-726-756-705-703-1_W.mp3)
  Segment 31: 54624ms ~ 55200ms (file: 517-761-135-103-132-136-130-103-176-316 132-103-103-136-137-137-317-106-136-163-132-163-163-156-163-167 612-167-163-167-561-751-167 135-310-103-651-312-176-310-130-103-132-103-1_V.mp3)
  Segment 32: 56200ms ~ 56776ms (file: 657-675-657-657-156-165-136-157-150-157-576-765-163-136-316-152-153-157-156-175-137-531-571-175-157-135-371-175-157-157-157-135-137-175-312-317-137-136-315-315-315-315-531-315-1_W.mp3)
  Segment 33: 57776ms ~ 58352ms (file: 136-103 537-572-576-537 153-312-136-315-312-162-631-312-136-103-102-103-162-103-136-130-613-160-167-163-126-316-163-136-130-103_V.mp3)
  Segment 34: 59352ms ~ 59928ms (file: 103-163-130-310-136-136-135-132-106 653-561-613-136-106-106-130-165-362-123-657-635-106-130-607-103-316-103-136-160-106-546-163-316-103-134-103-103-516-516-156-130-136-153-625-5_V.mp3)
  Segment 35: 60928ms ~ 61504ms (file: 517-176-571-761 105-105-105 570 716_W.mp3)
  Segment 36: 62504ms ~ 70028ms (file: 132-132-312-312-312-726-267-627-263-267-635-356-375-637-365-371-163-172-231-213-273-620-106-130-103-301-302-320-267-372-157-107-267-765-762-576-123-173-162-160-617-123-312-362-6_W.mp3)
  Segment 37: 71028ms ~ 71604ms (file: 516-571-765-657-652-562-561-651-576-562-567-567-567-576-275-657-756-567-576-756-576-573-576-576-753-507-753-756-765-167-137-136-136-136-310-176-165-351-156-651-671-357-731-735-3_W.mp3)
  Segment 38: 72604ms ~ 73180ms (file: 302-312-325-267-502-542-132-130-103-132-103-136-203-250-523-103 213-321-327-362-361-136-162-362-316-263-236-210-621-612-637-107-360-306-312-132-126-132-316-326-263-362-263-263-2_V.mp3)
  Segment 39: 74180ms ~ 74756ms (file: 756-756-157-312-513-517-132 263-176-715-160-160-216_W.mp3)
  Segment 40: 75756ms ~ 76332ms (file: 143-103-103-103-137-132 612-261-126-261-263-103-102-103-132-107-136-176-756-756-675-627-263-231-273-213-123-602-206-263-623-627-263-136-316-263-321-312-627-627-607-607-602-137-1_V.mp3)
  Segment 41: 77332ms ~ 77908ms (file: 123-562-567-567-316-103-103-167-162-167-362-756-375-231-312-302-310-236-326-263-267-607-756-762-263-201-126-612-106-621-162-163-102-163-762-107-271-162-612-623-263-265-623-256-2_V.mp3)
  Segment 42: 78908ms ~ 79484ms (file: 672-376-127-123-612-621-163-123-312-137-632-623-321-310-361-321-267-621 362 567-152-125-103-315-321-310-327-357-312-310-317-130-135-305-356-130-513-563-560-562-132-321-231-327-3_W.mp3)
  Segment 43: 80484ms ~ 81060ms (file: 567-567-675-627-257-576-652-756-576-657-567-672-365-672-576-673-625-675-653-625-657-625-265-267-762-625-562-675-627-762-672-657-526-657-652-625-673-762-672-672-627-652-756-675-2_W.mp3)
  Segment 44: 82060ms ~ 91888ms (file: 325-637-326-136-231-132-705-623-172-326-302-312-705-213-320-302-730-765-206-237-276-765-271-312-372-321-752-756-673-321-130-310-750-671-126-261-756-756-756-627-263-102-765-267-2_W_stt_the_deal_that_she_hasn't_been_that_have.mp3)
  Segment 45: 92888ms ~ 103040ms (file: 132-176-167-103-137-103-103-162-702-720-572-301-617-617-132-316-103-126-162-216-123-106-716-162-162-602-132-216-103-137-651-321-260-316-326-103-132-172-316-157-103-261-126-103-1_V.mp3)
  Segment 46: 104040ms ~ 104616ms (file: 167-136-512-260-236-612-601-601-162-176-106-316-561-156-123-136-176-107-607-524-521-123-576-576-567-652-612-132-103-756-236-217-137-612-761-167-716-103-156-265-137-103-135-165-1_W.mp3)
  Segment 47: 105616ms ~ 106192ms (file: 103-135-132-326-267-316-134-130-103-231-213-103-162-216-231-321-216-231-132 574-754-276-376 574-574-547-574-576-765-576-570-576-263-710-576-572-751-276-165-132-132-175-167-125-1_V.mp3)
  Segment 48: 107192ms ~ 114464ms (file: 572-120-132-120-210-532-572-257-203-230-206-230-253-120-210-213-123-312-231-320-102-102-230-320-231-210-120-230-123-126-102-230-206-206-230-123-102-127-125-261-326-213-213-102-2_V.mp3)
  Segment 49: 115464ms ~ 116040ms (file: 362-312-675-163-602-627-302-164-167-103-125-102-231-236-201-216-126-163-103-130-572-103-163-136-136-136-163-103-103-156-107-120-652-261-620-267-675-261-765-756-762-126-216-672-7_W.mp3)
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
> **Time**: 2026-04-04 03:24:07
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
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
*Generated by WKAppBot ask gemini — 2026-04-04 03:24:07*
