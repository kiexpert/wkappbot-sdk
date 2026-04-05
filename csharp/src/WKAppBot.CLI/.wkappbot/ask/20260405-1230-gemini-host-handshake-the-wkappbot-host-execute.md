# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=11716  time=12:28:53  uptime=38390s  mem=344MB  system_processes=356"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_122810.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 501-150-170-157-517-157-751-715 507-510-501-105-175-157-170-170-175-157-150-157-107-570 571-571-510-517-510-150-510-150-507-105-517-507-157-157-517-157-153-751-501 517-751-571-7_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 106-316-163-612-637-163-631-612-631-617-167-162-167-167-615-167-567-165-615-157-615-165-675-756-567-657-567-657-675-125-612-162-165-162-156-165-165-165-165-163-165-162-612-675-6_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 501-507-510-517-510-501-571-571-570-571 150-510-510-501-150-507-501-510-501-510-501-510-517-507-571-510 510-150-105-510-150-510-501-510-501-510-150-570-501-517-517-150-510-150-5_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 160-103-132-167-136-106-126-356-163-651-130-106-136-675-136-617-163-761-160-765-236-162-163-103-136-135-173-657-175-125-167-126 167-130-163-106-103-103-165-106-671-132-120-657-6_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 617-367-361-316-316-362-716-607-673-715-675-356-163-637-365-631-326-126-163-375-356-653-753-576-576-576-573-136-357 312 716-137-173-176-165-175-756-175-617-165-167-163-617-162-6_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 510-150-507-750-517-510-150-570-571-150-571-150-570-510-517-571-150_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 567-654-654-654-564-654-564-654-564-654-564-654-654-654-167-613-134-163-167-163-613-625-652-651-651-657-651-654-564-564-564-654-564-564-564-564-564-564-564-654-654-654-671-643-6_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 502-510-501-512-521-572-576-503-507-507-502-507-504-506-502-502-572-501-507-571-570-571-571-571-571-572-572-576-576-571-527-572-572-570-572-512-521-713-213-526-570-572-572-576-5_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 627-621-563-175-217-674-613-163-361-137-135-301-316-156-653-163-167-671-603-176-621-617-631-136-123-761-526-136-103-617-516-610-672-632-236-613-127-162 130-135-602-620-260-302-1_W.mp3)
  Segment 10: 14184ms ~ 19296ms (file: 726-321-175-175-175-715-175-157-175-126-217-126-213-160-137-123-172-753-723-107-130-173-170-217-217-260-261-260-207-310-213-271-517-130-302-261-216-271-271-173-175-170-170-130-1_W.mp3)
  Segment 11: 20296ms ~ 27064ms (file: 752-756-627 312-607-621-261-627 120-236-620-267-620-623-231-532-153-213-260-263-527-521-235-524-541-120-531-160-231-573-527-321-576-357-103-102-321-123-132-231-621-621-213-106-1_W.mp3)
  Segment 12: 28064ms ~ 28640ms (file: 624-621-613-614-641-645-645-654-654-654-651-654-654-654-654-154-156-615-153-135-154-354-315-215-541-163 621-612-615-654-654-654-654-651-654-654-654-654-564-654-654-564-564-654-6_W.mp3)
  Segment 13: 29640ms ~ 30216ms (file: 725-572-576-576-756-576-572-576-756-576-572-752-453-504-546-504-564-504-506-506-560-560-570-506-567-506-507-560-506-271-752-576-675-576-756-756-756-756-126-612-362-612-263-504-5_V.mp3)
  Segment 14: 31216ms ~ 31792ms (file: 576-576-562-576-576-576-576-567-567-576-675-756-716-765-157-765-576-675-756-617-617-615-176-576-516-567-517-576-657-576-756-576-765-617-675-167-751-765-765-576-715-675-751-567-7_W.mp3)
  Segment 15: 32792ms ~ 33368ms (file: 756-576-571-576-572-672-612-610-160-541-160-103-603-160-631-163-136-167-167-176-671-176-167-617-106-756-617-725-705-750-760-765-137-167_W.mp3)
  Segment 16: 34368ms ~ 34944ms (file: 103-132-312-756-765-765-613 623-627-671-576-635-654-617-637-612 362-130-361-137-621-612-362-103-106-160-267-263-362-623-567-627-623-256-265-265-236-524-257_W.mp3)
  Segment 17: 35944ms ~ 36520ms (file: 276-672-276 507-507-504-507-507-507-507-574-570-570-570-570-576-570-507-570-507-507_W.mp3)
  Segment 18: 37520ms ~ 45980ms (file: 612-671-267-602-623-672-672-627-623-627-673-637-576-571-135-201-236-132-134-132-537-540-130-103-310-534-230-201-207-213-137-107-357-137-540-126-765-576-765-751-756-751-716-715-7_W.mp3)
  Segment 19: 46980ms ~ 47556ms (file: 253-576-672-276-512-752-572-173-732-706-123-276-573-231-713-573-573-576-652-123-127-237-267-123-367-741-173-312-725-275-362-750-157-357-621-327-673-132-273-263-507-572-235-627_V.mp3)
  Segment 20: 48556ms ~ 49132ms (file: 257-275-752-520-526-567-567-567-567-567 571-507-502-521-256-517-527-506-560-567-576 657-651-651-561-561-657-651-657-651-651-615-651-756-756-751-752-756-765-756-752-765-765-567-6_W.mp3)
  Segment 21: 50132ms ~ 50708ms (file: 130-210 251-256-265-231-123-576-574-167-135-756-561-153-657-567-152-315-216-675-671-573-576-517-157-751-567-135-576-576-576-572-517-157-517-571-571-571-376-356-361-261-302-367-3_W.mp3)
  Segment 22: 51708ms ~ 52284ms (file: 163-326-316-316-137-517-317-137-571-576-127-630-163-726-257-125-216-315-132-321-126-231-261-613-526-125-672-152-657-756-572-657 231-235-256-625-261-137-376-673-705-375-621-571-5_V.mp3)
  Segment 23: 53284ms ~ 58612ms (file: 301-201-216-130-236-126-167-102-102-712-230-201-152-761-176-720-712-127-752-351-310-316-157-107-132-312-531-125-267-260-163-120-207-751-105-320-256-512-135-153-627-576-167-201-2_W.mp3)
  Segment 24: 59612ms ~ 60188ms (file: 750-571-576-501-570 507-750-756-570-157-571-701-701-103-710-305-750-713-175-701-705-750-715-537-305-375-750-170-571-507-530-703-576-507-507-750-751-571-710-731-750-103-157-731-7_V.mp3)
  Segment 25: 61188ms ~ 61764ms (file: 567-576-567-576-567-576-756-756-762_W.mp3)
  Segment 26: 62764ms ~ 63340ms (file: 576-576-675-715-617-765-761-167-716-671-671-167-673-316-170-721-126-316-750-213-276-263-126-276-326-752-705-267-573-321-705-176-312-726-612-216-162-765-573-567-371-327-327-576-2_V.mp3)
  Segment 27: 64340ms ~ 68624ms (file: 103-103-132-312-316 102-123-102-135-201-203-260-201-132-263-627-267-260-103-310-216-726-167-126-263-163-326-310-153-765-756-751-762-752-736-765 517 574-574-150-501-507-701-175-7_W.mp3)
  Segment 28: 69624ms ~ 70200ms (file: 567-356-357-615-157-176-317-713-316-167-106-537-736-730-730-301-576-517-157-136-105-163-150-672 576-756-136-630-362-673 576 132 213-325-415-275-213-146-132_V.mp3)
  Segment 29: 71200ms ~ 71776ms (file: 576-175-756-675-761-567-675-765-657-671-657-675-132-376-726-317-316-317-317-371-127-371-273-172-726-316-763-172-173-167-137-317 751-761-172-317-371-317-317-157-751-573-751-317-1_W.mp3)
  Segment 30: 72776ms ~ 73352ms (file: 613-620-631-136-321-613-612-603-605-613-163-160-310-136-162-216-160-613-163-150-165-136-167-123-316-516-237-312-531-567-620-316-126-316-216-567-672-756-567-367-627-621-671-671-5_W.mp3)
  Segment 31: 74352ms ~ 74928ms (file: 570-507-527-572-561-576-756-756-571-517-257-275-572-752-572-576-572-576-572-257-527-257-527-257-752-526-275-572-572-257-752-752-502-571-570-576-576-567-576-576-731-516-576-571-5_W.mp3)
  Segment 32: 75928ms ~ 76504ms (file: 576-756-756-756-231-124-504 572-576-576-572-756-576-576-576-756-752 504 572-572-756-576-576-756-576-765-756-756-756-752-725 752-756-576-756-756-756-576-567-756-576-765-675-635-3_W.mp3)
  Segment 33: 77504ms ~ 78080ms (file: 652-657-567-657-657-657-657-657-657-567-567-657-657-657-657-657 675-675-576-567-567-576-576-567-567-567-567-576-567-657 567-561-561-657-567-567-567-567-567-567-567-567-567 572-5_W.mp3)
  Segment 34: 79080ms ~ 79656ms (file: 756-765-751 371-317-130-107-731-301-137-637-361-607-316-306-301-103-312-310-301-301-371-130-372-317-137-317-753-715-310-735-531-173 705-573-307-715-136-130-617-607-617-301-601-6_V.mp3)
  Segment 35: 80656ms ~ 81232ms (file: 625 625-625 652-652-651-652-654-652-651 562-657-657-657-652-651-657-657-657-652-651_W.mp3)
  Segment 36: 82232ms ~ 93068ms (file: 170-751-312-276-263-132-126-321-321-103-173-273-106-130-276-102-132-123-102-276-237-132-312-172-321-263-120-231-261-627-162-130-312-267-123-173-102-231-312-123-213-201-126-143-6_W.mp3)
  Segment 37: 94068ms ~ 94644ms (file: 547-175-571-321-672-761-760-716-167-673-761 631_W.mp3)
  Segment 38: 95644ms ~ 96220ms (file: 751-715-573-531-507-573-257-502-250-527-523-526-572-725-527-275-275-257-275-275-756-752-572-527-503-507-572-571-572-527-512-576-576-576-567-567-567-576-576-576-756-756-762-517-5_W.mp3)
  Segment 39: 97220ms ~ 100604ms (file: 132-132-321-132-132-312-230-132-320-216-102-160-162-261-132-612-260-216-102-120-260-201-216-203-260-213-216-216-216-210-126-213-321-263-236-320-236-230-231-231-320-213-312-210-1_W.mp3)
  Segment 40: 101604ms ~ 102180ms (file: 365-103-123-257-103-716-137-173-163-106-571-310-156-136-764-216 627-135-316-136-216-312-130 312-132_W.mp3)
  Segment 41: 103180ms ~ 103756ms (file: 574-756-576-576-756-136-103-672-136-617-136-176-206-162-162-362-671-637-165-103-132-167-167-126-510-576-256-126-162-132-567-716-672-163-176-610-761-175-623-620-673-261-612-607-1_W.mp3)
  Segment 42: 104756ms ~ 105332ms (file: 715-715-175-715-715-715-715-751-157 510-517-157-150-157-571-751-751-751-751-751-751-751-715-715-715-715-175-715-571-751-751 150-150-157-150-105-571-175-751-715-175-715-751-715-1_V.mp3)
  Segment 43: 106332ms ~ 114504ms (file: 213-216 267-267-261-237-320-236-524-257-250-205-130-321-725-207-752-276-216-236-261-320-267-203-302-301-103-132-762-752-312-301-521-512-236-312-310-213-260-321-132-524-103-103-1_W.mp3)
  Segment 44: 115504ms ~ 116080ms (file: 501-571-517-105-501-157-510-510-501-570-105-501-107-570-507-150-517-107-501-705-150-105-501-150-105-507-150-507-514-514 105-105-105-517-501-150-105-501-501-510-105-570-570-570-5_V.mp3)
  Segment 45: 117080ms ~ 117656ms (file: 165-165 506-256-276-207-257-750-527-257-257-257-527-521-251-135-152-125-156-156-135-156-165-165-165-156 215-257-512-205-325-502-507-576-527-752-751-120-132-135-153-157-153 257-2_V.mp3)
  Segment 46: 118656ms ~ 119232ms (file: 574-572-537-537-351-153-157-351-571-513-175-351-751-157-573-173-517 517-157-517-175-157-517-157-753-157-175-175-517-517-157-571-157-157-571-715-517-175-157-751 517-571-157-157-7_V.mp3)
  Segment 47: 120232ms ~ 120808ms (file: 521-653-560-651-615-657-657-675-657-617-367-367-631-637-653-615-567-527-567-567-361-165-576-157-156-516-615-567-652-567-135-153-153-153-135-135-315-153-135-153-156-361-136-613-1_W.mp3)
  Segment 48: 121808ms ~ 122384ms (file: 316 701-715-730-310-173-317-307-310-172-731-170-710-173-703-706-130-670-765-675-137-675-763-760-361-675-753-367-675-765-765-675-675-160-715 173-731-310-715-731-130-375-130-613-6_V.mp3)
  Segment 49: 123384ms ~ 123960ms (file: 675-175-675-167-615-167-671-761-176-170-760-761-107-750-176-175-715-157-716-175-157-175-157-175-175-157-531-513-561-562-516 516-517-560-506-570-570-751-176-576-751-756-105-710-6_V.mp3)
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
> **Time**: 2026-04-05 12:30:06
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "안녕하세요 반갑습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 45, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 350, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "오늘 날씨가 정말 좋네요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 30, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "날씨가", "start_ms": 160, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "정말", "start_ms": 310, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "좋네요", "start_ms": 430, "dur_ms": 130, "phase_lr_deg": 5.0}
]
},
{
"segment": 3,
"transcript": "점심 맛있게 드셨나요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "점심", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "맛있게", "start_ms": 200, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "드셨나요", "start_ms": 370, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "Where are you going now",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Where", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "are", "start_ms": 150, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 240, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "going", "start_ms": 330, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "now", "start_ms": 460, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "커피 한 잔 할까요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "커피", "start_ms": 25, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "한", "start_ms": 180, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "잔", "start_ms": 270, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "할까요", "start_ms": 380, "dur_ms": 170, "phase_lr_deg": -5.0}
]
},
{
"segment": 6,
"transcript": "다음에 봐요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 60, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "봐요", "start_ms": 300, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "주말에 뭐 하세요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "주말에", "start_ms": 35, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "뭐", "start_ms": 250, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "하세요", "start_ms": 360, "dur_ms": 190, "phase_lr_deg": 5.0}
]
},
{
"segment": 8,
"transcript": "영화 보러 가요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "영화", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "보러", "start_ms": 200, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "가요", "start_ms": 340, "dur_ms": 200, "phase_lr_deg": 5.0}
]
},
{
"segment": 9,
"transcript": "I love this song",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 55, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "love", "start_ms": 140, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "this", "start_ms": 260, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "song", "start_ms": 370, "dur_ms": 150, "phase_lr_deg": 5.0}
]
},
{
"segment": 10,
"transcript": "한국어 공부는 정말 재미있어요 하지만 가끔 어려워요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 100, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "공부는", "start_ms": 550, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "정말", "start_ms": 950, "dur_ms": 300, "phase_lr_deg": 5.0},
{"word": "재미있어요", "start_ms": 1300, "dur_ms": 800, "phase_lr_deg": 5.0},
{"word": "하지만", "start_ms": 2200, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "가끔", "start_ms": 2800, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "어려워요", "start_ms": 3300, "dur_ms": 700, "phase_lr_deg": 5.0}
]
},
{
"segment": 11,
"transcript": "Can you help me with this grammar point please",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "Can", "start_ms": 120, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "you", "start_ms": 350, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "help", "start_ms": 550, "dur_ms": 300, "phase_lr_deg": 5.0},
{"word": "me", "start_ms": 900, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "with", "start_ms": 1100, "dur_ms": 250, "phase_lr_deg": 5.0},
{"word": "this", "start_ms": 1400, "dur_ms": 250, "phase_lr_deg": 5.0},
{"word": "grammar", "start_ms": 1700, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "point", "start_ms": 2300, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "please", "start_ms": 2800, "dur_ms": 500, "phase_lr_deg": 5.0}
]
},
{
"segment": 12,
"transcript": "걱정 마세요 다 잘 될 거예요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "걱정", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "마세요", "start_ms": 190, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "다", "start_ms": 320, "dur_ms": 50, "phase_lr_deg": 5.0},
{"word": "잘", "start_ms": 380, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "될", "start_ms": 450, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "거예요", "start_ms": 520, "dur_ms": 50, "phase_lr_deg": 5.0}
]
},
{
"segment": 13,
"transcript": "이 노래 가사가 너무 좋아요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 45, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "노래", "start_ms": 110, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "가사가", "start_ms": 240, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 390, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "좋아요", "start_ms": 490, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "언제 시간이 괜찮으세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "언제", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "시간이", "start_ms": 190, "dur_ms": 180, "phase_lr_deg": 5.0},
{"word": "괜찮으세요", "start_ms": 380, "dur_ms": 190, "phase_lr_deg": 5.0}
]
},
{
"segment": 15,
"transcript": "내일 오후는 어때요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "내일", "start_ms": 40, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "오후는", "start_ms": 180, "dur_ms": 180, "phase_lr_deg": 5.0},
{"word": "어때요", "start_ms": 370, "dur_ms": 150, "phase_lr_deg": 5.0}
]
},
{
"segment": 16,
"transcript": "좋아요 그때 만나요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "좋아요", "start_ms": 50, "dur_ms": 160, "phase_lr_deg": 5.0},
{"word": "그때", "start_ms": 220, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "만나요", "start_ms": 370, "dur_ms": 150, "phase_lr_deg": 5.0}
]
},
{
"segment": 17,
"transcript": "감사합니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 100, "dur_ms": 450, "phase_lr_deg": 5.0}
]
},
{
"segment": 18,
"transcript": "(music)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 19,
"transcript": "새로운 단어를 외우는 건 힘들어요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "새로운", "start_ms": 60, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "단어를", "start_ms": 220, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "외우는", "start_ms": 370, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "건", "start_ms": 500, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "힘들어요", "start_ms": 550, "dur_ms": 25, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "매일 꾸준히 하는 게 중요하죠",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "매일", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "꾸준히", "start_ms": 160, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "하는", "start_ms": 310, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "게", "start_ms": 420, "dur_ms": 50, "phase_lr_deg": 5.0},
{"word": "중요하죠", "start_ms": 480, "dur_ms": 90, "phase_lr_deg": 5.0}
]
},
{
"segment": 21,
"transcript": "맞아요 포기하지 마세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "맞아요", "start_ms": 45, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "포기하지", "start_ms": 210, "dur_ms": 180, "phase_lr_deg": 5.0},
{"word": "마세요", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": 5.0}
]
},
{
"segment": 22,
"transcript": "어떤 음식을 가장 좋아해요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "어떤", "start_ms": 30, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "음식을", "start_ms": 170, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 320, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "좋아해요", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "저는 비빔밥을 정말 좋아해요 당신은요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 80, "dur_ms": 250, "phase_lr_deg": 5.0},
{"word": "비빔밥을", "start_ms": 350, "dur_ms": 600, "phase_lr_deg": 5.0},
{"word": "정말", "start_ms": 1000, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "좋아해요", "start_ms": 1400, "dur_ms": 600, "phase_lr_deg": 5.0},
{"word": "당신은요", "start_ms": 2200, "dur_ms": 600, "phase_lr_deg": 5.0}
]
},
{
"segment": 24,
"transcript": "저도 비빔밥 좋아해요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "저도", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "비빔밥", "start_ms": 200, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "좋아해요", "start_ms": 390, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "그럼 다음에 같이 먹으러 가요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "다음에", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "같이", "start_ms": 280, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "먹으러", "start_ms": 390, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "가요", "start_ms": 510, "dur_ms": 60, "phase_lr_deg": 5.0}
]
},
{
"segment": 26,
"transcript": "좋은 생각이에요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 60, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "생각이에요", "start_ms": 230, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "서울 여행 가고 싶어요 추천할 만한 곳 있나요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "서울", "start_ms": 90, "dur_ms": 300, "phase_lr_deg": 5.0},
{"word": "여행", "start_ms": 400, "dur_ms": 250, "phase_lr_deg": 5.0},
{"word": "가고", "start_ms": 660, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "싶어요", "start_ms": 870, "dur_ms": 300, "phase_lr_deg": 5.0},
{"word": "추천할", "start_ms": 1300, "dur_ms": 450, "phase_lr_deg": 5.0},
{"word": "만한", "start_ms": 1760, "dur_ms": 250, "phase_lr_deg": 5.0},
{"word": "곳", "start_ms": 2020, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "있나요", "start_ms": 2200, "dur_ms": 300, "phase_lr_deg": 5.0}
]
},
{
"segment": 28,
"transcript": "경복궁에 꼭 가보세요 정말 아름다워요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "경복궁에", "start_ms": 55, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "꼭", "start_ms": 420, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "가보세요", "start_ms": 530, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 900, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "아름다워요", "start_ms": 1200, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "야경은 남산타워가 최고예요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "야경은", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 5.0},
{"word": "남산타워가", "start_ms": 230, "dur_ms": 220, "phase_lr_deg": 5.0},
{"word": "최고예요", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 5.0}
]
},
{
"segment": 30,
"transcript": "와 정말 기대되네요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "와", "start_ms": 35, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "정말", "start_ms": 120, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "기대되네요", "start_ms": 270, "dur_ms": 280, "phase_lr_deg": 5.0}
]
},
{
"segment": 31,
"transcript": "재미있는 여행 되시길 바랄게요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "재미있는", "start_ms": 45, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "여행", "start_ms": 260, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "되시길", "start_ms": 370, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "바랄게요", "start_ms": 500, "dur_ms": 70, "phase_lr_deg": 5.0}
]
},
{
"segment": 32,
"transcript": "고맙습니다 잘 다녀올게요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "고맙습니다", "start_ms": 30, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "잘", "start_ms": 240, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "다녀올게요", "start_ms": 330, "dur_ms": 240, "phase_lr_deg": 5.0}
]
},
{
"segment": 33,
"transcript": "안녕히 계세요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "계세요", "start_ms": 260, "dur_ms": 200, "phase_lr_deg": 5.0}
]
},
{
"segment": 34,
"transcript": "안녕히 가세요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 260, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 36,
"transcript": "이번 시험 공부는 어떻게 하고 있나요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "이번", "start_ms": 70, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "시험", "start_ms": 230, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "공부는", "start_ms": 380, "dur_ms": 180, "phase_lr_deg": 5.0},
{"word": "어떻게", "start_ms": 570, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "하고", "start_ms": 730, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "있나요", "start_ms": 840, "dur_ms": 130, "phase_lr_deg": 5.0}
]
},
{
"segment": 37,
"transcript": "도서관에서 살고 있어요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "도서관에서", "start_ms": 40, "dur_ms": 280, "phase_lr_deg": 5.0},
{"word": "살고", "start_ms": 330, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "있어요", "start_ms": 450, "dur_ms": 120, "phase_lr_deg": 5.0}
]
},
{
"segment": 38,
"transcript": "정말 고생이 많네요 힘내세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 45, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "고생이", "start_ms": 175, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "많네요", "start_ms": 315, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "힘내세요", "start_ms": 435, "dur_ms": 140, "phase_lr_deg": 5.0}
]
},
{
"segment": 39,
"transcript": "I believe in you You can do it",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 60, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "believe", "start_ms": 150, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "in", "start_ms": 360, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "you", "start_ms": 470, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "You", "start_ms": 700, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "can", "start_ms": 830, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "do", "start_ms": 940, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "it", "start_ms": 1030, "dur_ms": 100, "phase_lr_deg": 5.0}
]
},
{
"segment": 40,
"transcript": "응원해 줘서 고마워요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "응원해", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "줘서", "start_ms": 250, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "고마워요", "start_ms": 380, "dur_ms": 190, "phase_lr_deg": 5.0}
]
},
{
"segment": 41,
"transcript": "시험 끝나면 맛있는 거 먹으러 가요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "시험", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "끝나면", "start_ms": 180, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "맛있는", "start_ms": 330, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "거", "start_ms": 490, "dur_ms": 40, "phase_lr_deg": 5.0},
{"word": "먹으러", "start_ms": 540, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "가요", "start_ms": 650, "dur_ms": 50, "phase_lr_deg": 5.0}
]
},
{
"segment": 42,
"transcript": "좋은 계획이네요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 45, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "계획이네요", "start_ms": 210, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "What is your favorite Korean drama",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "What", "start_ms": 110, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "is", "start_ms": 270, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "your", "start_ms": 380, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "favorite", "start_ms": 510, "dur_ms": 300, "phase_lr_deg": 5.0},
{"word": "Korean", "start_ms": 820, "dur_ms": 250, "phase_lr_deg": 5.0},
{"word": "drama", "start_ms": 1080, "dur_ms": 300, "phase_lr_deg": 5.0}
]
},
{
"segment": 44,
"transcript": "저는 도깨비를 제일 좋아해요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 60, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "도깨비를", "start_ms": 220, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "제일", "start_ms": 450, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "좋아해요", "start_ms": 570, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "그 드라마 정말 인기가 많았죠",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "그", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "드라마", "start_ms": 130, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 300, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "인기가", "start_ms": 420, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "많았죠", "start_ms": 560, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "공유 배우가 너무 멋있어요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "공유", "start_ms": 35, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "배우가", "start_ms": 185, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 345, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "멋있어요", "start_ms": 465, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "OST도 다 좋더라고요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "OST도", "start_ms": 45, "dur_ms": 180, "phase_lr_deg": 5.0},
{"word": "다", "start_ms": 235, "dur_ms": 70, "phase_lr_deg": 5.0},
{"word": "좋더라고요", "start_ms": 315, "dur_ms": 250, "phase_lr_deg": 5.0}
]
},
{
"segment": 48,
"transcript": "나중에 같이 정주행해요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "나중에", "start_ms": 50, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 220, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "정주행해요", "start_ms": 350, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "좋아요 벌써 기대되네요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "좋아요", "start_ms": 40, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "벌써", "start_ms": 210, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "기대되네요", "start_ms": 340, "dur_ms": 230, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 12:30:06*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=9236  time=12:30:28  uptime=38057s  mem=477MB  system_processes=355"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_122738.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 157-517-501-501-517-513-516-501-516-517-510-517-510-507-501-501-501-150-507-501-570-507-510-574-540-507-504-540-504-507-507 507-105-501-501-507-507-507-570-507-507-517-157-571-5_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 567-653-562-526-652-657-625 620-621-625-627-621-627-267-627-621-261-261-261-261-261-620 263-621-623-627-625-627-621-627-625-267-627-621-261-263-261-261 623-625-621-627-627-261-6_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 362-315-312-173-327-273-375-236-126-753-673-574-231-745-475-236-623-267-260-132-153-574-327-321-312-312-310-316-301-236-321-103-312-301-632 321-576-675-162-257-352-327-672-321-2_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 571-571-571 513-517-517-571-571-574-574-574-571-157-175-751-571-571-517-571-517-517-157-157-157-157-175-571-157-175-571-571-570-576 510-517-571-751-571-571-571-571-571-751-570-5_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 261-167-672-627-617-617-652-615-615-652-615-675-675-617-671-617-615-615-675-617-617-617_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 765-176-716-765-736-317-762-376-736-672-201-620-130-613-601-630-162-613-361-160-123-103-136 172_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 105-157-105-751-157-157-175-105-507-157-507-150-105-157-705-105-517-507-570-157-150-506-507-570-527-501-570 510-150-501-105-510-510-105-150-157-517-507-501-517-501-510-507-517-5_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 231-236-260-261-623-623-623-263-562-546-652-627-627-627-627-672-236-267-267-267-627-263-632-321-361-217-267-627-562-562-562-625-627-627-627-263-123-265-216-236-216-623-612-263-3_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 137-715-316-735-576-523-175-517-627-237-507-546-251-572-175-760-276-726-162-752-251-726-573-724-623-732-276-765-571-605-267-153-751-725-236-625-657-574-372-762-265-263-316-135-3_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 671-617-625-652-657-627-265-267-625-652-625-267-627-627-267-267-265-615-567-135-136-135-135-153-135-165-165-163-572-527-576-756-163-615-651-156-567-516-571-175-137-135-516-136-5_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 617-617-617-617-617-617-671-617-617-617-610-512-572-504-504-504-501-504-615-672-657-675-765-675-657-675-617-675-671-675-617-162-571-165-276-726-275-735-725-725-273-723-726-726-3_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 572-572-572-574-572-574-572-527-572-512-504-502-503-521-523-527-531-502-502-502-526-502-562-526-502-205-205-271-705-576-756-576-567-756-756-765-512-576-572-507-572-520-572-576-5_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 723-136-132-136-172-265-127 163 127 106-167-176-167-475-167-162-103-136-103-126-127-713-175-175-165-103-162 756-765-672-673-513-157-561-531-750-753-673-160-160 231-172-315-327-1_V.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 751-175-175-751-175-751-751-715-715-715-175-715-751-157-751-751-751-715-751 150-501-150-105-105-105-107-175-715-175-175-175-175-751-517-751-751-751-751-751-756-751-756-751-751-7_W.mp3)
  Segment 15: 22064ms ~ 30488ms (file: 310-130-132-130-120-301-102-132-320-130-132-103-102-106-103-762-302-623-260-620-102-103-210-216-267-276-726-627-267-267-126-162-236-102-167-326-120-612-136-106-367-136-106-106-1_V.mp3)
  Segment 16: 31488ms ~ 32064ms (file: 576 671-765-165-156-165-165-165-165-165-165-167-576-576-572-756-756 623-625-652-265-625-265-627-672-625-267-265-526-526-560-572-571_W.mp3)
  Segment 17: 33064ms ~ 33640ms (file: 307-301-316-316-310-301-312-302-301-312-130-103-312-103-312-317-132-312-321-163-765-670-672-627 132-143-136-106-134-132-137-136-136-675-675_V.mp3)
  Segment 18: 34640ms ~ 35216ms (file: 621-621-621-621-261 216-261-621-621-621-621-621-621-612-612-621-612 261-621-621-621-621-623-623-612-130-716-306-627-620-162-612-621-612-612-621-612-612-627-612-617-612-612-613-6_W.mp3)
  Segment 19: 36216ms ~ 36792ms (file: 216-261-261-260-126-261 504-502-501-503-534-503-503-504-501-507-652-625-651-561-657-651-652-501 567-106-135-163-165-156-165-156-510-516-567-561-562-526-526-157-512-156-156-651-1_W.mp3)
  Segment 20: 37792ms ~ 38368ms (file: 157-105-105-157-150-516 105-150-150-150-150-150-105-105-105-157-150-156-105-150-157-517 150-150-150-105-150-150-150-150-150-150-150-157-150-150-150 105-150-105-105-157-105-105-1_V.mp3)
  Segment 21: 39368ms ~ 46496ms (file: 310-263-132-132-627-312-265-265-276-257-375-132-572-574-256-572-215-231-231-236-275-267-267-526-352-167-136-321-263-267-621-625-126-574-267-267-312-263-627-265-267-265-542-321-2_W.mp3)
  Segment 22: 47496ms ~ 48072ms (file: 517-571-571-751-751-751-751-571-576-571-576-576-576-571-576-576-570-571-570-571-157-517-517-513-751-175-571-157-715-715-571-750-570-517-750-570-570-570-570-570-570-570-567-570-5_V.mp3)
  Segment 23: 49072ms ~ 49648ms (file: 752-572-527-572-521-521-527-521-527-527-527-571-571-572-576-576-576-571-576-576-751-751-561-572-527-571-576-567-576-576-507-507-507-705-502-507-570-570-756-572-572-576-752-257-2_W.mp3)
  Segment 24: 50648ms ~ 51224ms (file: 137-317-136-137-163-136-176-361-671-376-176-761-705-715-675-310-715-137-107-571-754-753-305-571-546-167-176-163-316-103-137-176-173-376-167-735-371-375-176-617-367-107-176-107-7_W.mp3)
  Segment 25: 52224ms ~ 52800ms (file: 154-501-517-506-507-571-501-517-175-756-715-705-170-501-170-175-701-701 501-507-157-501-510-501-705-751-570-517-157-501-105-571-570-571-571-756-756-756-756-716-517-570-157-105-5_V.mp3)
  Segment 26: 53800ms ~ 54376ms (file: 574-576-165-756-574-576-756-567-576-516-157-157-167-612-165-153-627-160-716-716-715-157-517-547-175-715-516-576-576-136-576-572-162-671-760 103-610-516-716-516-106-130-756-573-3_W.mp3)
  Segment 27: 55376ms ~ 55952ms (file: 351-130-150-130-135-135-153-315-513-510-153-157-513-157-571-517-571-517-513 105-153-157-130-153-517-517-150-170-130-130-310-317-157-513-517-517-573-571-571-571-571-571-571-751 1_V.mp3)
  Segment 28: 56952ms ~ 69660ms (file: 132-263-236-102-132-213-637-652-236-362-321-123-627-260-103-256-312-315-103-135-153-357-351-537-513-213-132-123-210-573-576-567-260-265-516-206-126-314-132-627-627-621-103-216-2_W.mp3)
  Segment 29: 70660ms ~ 71236ms (file: 571-157-157-571-571-751-571-571-751-173-137-137-135-315-173-137-175-137-317-135-137-317-175-137-137-173-315-137-715-721-317-137-173-173-137-132-175-172-317-173-132-213-162-712-1_W.mp3)
  Segment 30: 72236ms ~ 72812ms (file: 756-756-576-576-751-571-574-576-574-576-576-756-756-756-576-756-751-753-756-725-752-756-752-756-753-756-507-571-576-573-517-531-517-516-531-537-536-513-561-153-153-531-351_V.mp3)
  Segment 31: 73812ms ~ 74388ms (file: 175-710-705-107-107-715-705-107-170-715-170-175-135-173-751-170-751-175-175-571-750-517-157-517-751-157-170-157-517-751 510-571-170-715-750-705-751-170-701-710-170-517-175-153-1_W.mp3)
  Segment 32: 75388ms ~ 78880ms (file: 726-572-572-752-572-257-756-725-752-576-765-572-756-567-576-756-657-576-765-675-672-103-213-123-512-236-206-602-231-132-132-301-627-265-260-103-675-725-150-726-752-725 105-512-5_W.mp3)
  Segment 33: 79880ms ~ 80456ms (file: 752-570-570-570-570-725-526-572-570-506-502-503-507-502-572-572-527-572-572-572-572-572-572-572-572-752-762-513-573-516-514-513-527-571-523-527-523-527-523-513-512-513-523-537-5_W.mp3)
  Segment 34: 81456ms ~ 82032ms (file: 216 176-657-675-102-213-627-605-617-610-167_V.mp3)
  Segment 35: 83032ms ~ 83608ms (file: 605 625-265-652-520-562-560-650-250-256-526-652-625-562-526-562-520-502-520-256-502-265-526-652-526-256-256-526-526-256-625-256-256-256-256_W.mp3)
  Segment 36: 84608ms ~ 85184ms (file: 135-136-317-137-716-756 316-130-310-317-753-317-173-735-753-573-173-715-756 135-317-315-310-317-175-357-137-175-157-317-321-321 310-130-173-153-175-173-157-137-751-325-321-312-3_V.mp3)
  Segment 37: 86184ms ~ 86760ms (file: 576-576-576-756-756-756-756-651-541-504-503-504-504-507-504-504-503-504-501-572-752-576-576-756-756-576-756-576-576-576-576-756-751-546-506-504-506-506-506-501-506-503-506-501-5_W.mp3)
  Segment 38: 87760ms ~ 88336ms (file: 517-157-157-517-105-150-517-157-107-610-160-106-167-157-105-706-507 107-160-170-516-576-175-156-670-765-756 547-547-571-517-567-576-560-517-105-176-107-176-107-156-157-716-170-7_V.mp3)
  Segment 39: 89336ms ~ 89912ms (file: 675-263-267-203-236-260-236-326-257-752-372-132-106-106-310-160-231-123-216-126-763-310-316-310-316-360-301-326-263-607-301-130-103-310-326-203-326-106-163-103-102-103-326-210-6_W.mp3)
  Segment 40: 90912ms ~ 91488ms (file: 513-153-325-123-231-123-125-123-125-123-137-721-327-125-135-123-150-135-153-123-356-315-315-213-527-576-564-361-236-357-527-532-357-257-351-351-351-572-750-325-574-537-534-574-5_W.mp3)
  Segment 41: 92488ms ~ 96772ms (file: 103-103-132-312-316 102-123-102-135-201-203-260-201-132-263-627-267-260-103-310-216-726-167-126-263-163-326-310-153-765-756-751-762-752-736-765 517 574-574-150-501-507-701-175-7_W.mp3)
  Segment 42: 97772ms ~ 101768ms (file: 263-123-326-627-163-132-213-620-261-216-267-623-621-217-612-263-206-652-263-627-706-627-267-621-627-627-632-621-623-627-672-672-276-367-236-673-762-362-327-173-132-326-261-671-6_W.mp3)
  Segment 43: 102768ms ~ 103344ms (file: 541-547-563-531-541-541-543-547-506-546_V.mp3)
  Segment 44: 104344ms ~ 104920ms (file: 572-570-756-175-756 520-520-257-527-201-267-725-752-576-756-756-756-756-756-756-756-751-576-751-756-571-751 526-520-520-576-502-257-572-572-752-576-571-576-576-576-571-574-574-5_V.mp3)
  Segment 45: 105920ms ~ 112544ms (file: 752-273-237-372-321-237-217-271-614-641-761-613-641-163-326-574-756-156-153-301-367-210-725-735-372-167-671-326-237-127-312-126-752-125-102-210-107-102-230-260-260-260-237-125-1_W.mp3)
  Segment 46: 113544ms ~ 120996ms (file: 153-510-157-105-751-756-675-320-132-372-163-163-617-163-106-751-270-750-173-321-276-103-103-135-317-237-721-507-501-132-627-173-756-756-106-176-127-576-571-132-103-710-271-102-7_W.mp3)
  Segment 47: 121996ms ~ 128908ms (file: 573-573-571-576-573-175-357-153-260-267-726-360-103-362-263-261-216-260-261-206-126-261-107-136-673-162-126-120-236-167-637-627-132-306-631-316-607-163-716-607-670-213-216-267-1_W.mp3)
  Segment 48: 129908ms ~ 136856ms (file: 275-260-120-231-103-125-207-765-760-760-276-231-103-320-301-206-527-256-231-123-537-310-105-310-213-720-302-267-765-627-271-571-257-261-201-320-120-260-213-120-102-207-210-267-2_W.mp3)
  Segment 49: 137856ms ~ 142896ms (file: 231-236-267-602-627-106-236-236-672-627-762-263-326-236-267-216-126-236-263-627-672-627-267-621-267-267-326-261-362-231-621-261-321-312-632-236-213-162-621-261-621-263-126-267-2_W.mp3)
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
> **Time**: 2026-04-05 12:30:54
> **AI**: gemini

---

## Response

AnalysisAnalysisGemini의 응답

---
*Generated by WKAppBot ask gemini — 2026-04-05 12:30:54*
