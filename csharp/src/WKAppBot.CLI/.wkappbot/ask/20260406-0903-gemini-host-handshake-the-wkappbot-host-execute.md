# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=40368  time=09:02:19  uptime=12609s  mem=423MB  system_processes=412"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_085928.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 157-157-150-157-135-157-175-157-157-153-123-156-157-157-156 105-105-157-105-150-176_W.mp3)
  Segment 2: 1576ms ~ 8020ms (file: 267-230-627-123-203-231-237-320-206-621-260-263-316-321-623-765-321-572-627-267-620-625-126-263-326-260-572-267-253-216-263-126-263-106-621-231-263-265-236-523-362-236-256-236-2_W.mp3)
  Segment 3: 9020ms ~ 9596ms (file: 763-526-267-263-567-256-270-504-231-623-765-736-672-675-265-627-576-265-602-673-362-527-263-276-657-256-205-627-265-267-265-267-627-315-265-265-327-756-756-765-765-763-765-765-7_V.mp3)
  Segment 4: 10596ms ~ 11172ms (file: 547-675-156-102-253-312-315-352-257-132-310-320-103-103-630-362-160-132-173-163-106-265-167-176-361-163-631-613-613-367-673-576-576-672-672-657-756-263-657-672-623-672-627-132-3_W.mp3)
  Segment 5: 12172ms ~ 12748ms (file: 126-136-617-132-137-135-135-132-176-315-132-137-103-132-132-103-132-312-132-312-132-132-132-135-132-123-103-103-103-132-132-103-132-103-137-127-165-132-132-123-135-134-132-136-1_V.mp3)
  Segment 6: 13748ms ~ 14324ms (file: 517-517 157-105-107-175-725-716-175 150-157-107-157-715-165-261-627-621-176-173 157-175-157-157-751-157-157-105-107-107-173-176-172-137-176-173 157-510-157-517-715-517-751-105-5_V.mp3)
  Segment 7: 15324ms ~ 22956ms (file: 104-104-103-103-103-103-210-302-126-130-531-270-206-260-326-350-541-150-301-321-201-230-276-263-260-173-137-312-574-547-573-320-203-320-320-106-137-501-570-501-517-321-570-106-1_V.mp3)
  Segment 8: 23956ms ~ 24532ms (file: 701-705-170-105-710-715-750-752-751-715-130-123-162-160-160-162-106-102 123-132-716-715-175-105-107-175-157-157-751-527-175-175-175-570-751-715-175-715 571-571-715-507-570-751-5_V.mp3)
  Segment 9: 25532ms ~ 32156ms (file: 163-231-321-236-231-103-163-237-751-571-351-106-120-201-260-612-162-167-216-213-627-376-312-310-263-310-672-627-620-326-325-367-106-761-671-163-106-620-310-637-275-765-376-310-1_W.mp3)
  Segment 10: 33156ms ~ 33732ms (file: 571-517-157-153-351 135-513-517-537-571-571-753-571-157-571-150-513-730-573-576-576-573 153-137-317-317-315-317-132-135-371-317-132 315-531-517-571-571-517-570-571-157-753-571-5_W.mp3)
  Segment 11: 34732ms ~ 35308ms (file: 571-753-751-571-756-751-571-756-573-571-571-756-571-571 571-571-517-576-571-571-571-571-571-571-571-517-517-157-571-571-751-756-751-753 574-571-571-517-571-157-153-571-517-751-5_W.mp3)
  Segment 12: 36308ms ~ 49916ms (file: 213-143-750-726-756-752-726-720-137-107-103 103-371-173-751-705-753-735-302-756-107-725-731-137-761-756-267-370-713-107-276-126-715-175-103-176-753-756-103-107-321-132-103-104-7_W.mp3)
  Segment 13: 50916ms ~ 51492ms (file: 102-231-123-132-132-312-316-132-156-125-125-152-152-517-513-153-517-157-157-312-153-157-157-517-157-517-517-517-517-571-571-517-150-103-102-310-102-103-103-130-126-126-152-517-1_V.mp3)
  Segment 14: 52492ms ~ 60627ms (file: 576-756-756-752-750-705-753-735-375-107-756-756-375-130-107-751-103-310-310-675-675-756-170-715-517-715-735-137-316-137-317-731-137-136-137-175-106-132-753-375-756-372-672-673-7_W.mp3)
  Segment 15: 61627ms ~ 72643ms (file: 236-321-263-756-527-267-130-236-231-627-231-725-567-231-312-103-130-312-321-627-236-236-312-132-351-130-762-237-136-230-327-230-321-312-750-157-106-320-103-310-321-576-356-571-2_W.mp3)
  Segment 16: 73643ms ~ 74219ms (file: 765-675-756-765-756-567-576-753-536-352-357-356-316-716-617-675-765-756-516-751-756-754-752-753-756-752-756-752-563-754-756-756-756-756-756-753-756-765-765-657-675-375-756-765-3_W.mp3)
  Segment 17: 75219ms ~ 75795ms (file: 537-175-127-172-715-175-175-275-172-217-712-271-276-172-173-167-172-137-132-127-713-173-172-172-123-527-175-712-152-251-527-275-217-527-275-257-132-572-157-157-157-517-157-152-1_W.mp3)
  Segment 18: 76795ms ~ 77371ms (file: 705 570-170-710-170-710-701-705-715-716-570-571-715-710-710-705-750-750-750 570-571-715-175-715-710-701-705-750-710-750-705-701-705-750-750-750-705-750-750-750-751 570-750-701-7_V.mp3)
  Segment 19: 78371ms ~ 78947ms (file: 514-105-105-751-501 105-150-150-501-510-517-150 150-510-157-517-510-150-175-150-157-705-517-150-517-175-150-105-751-175-107-571-105-150 517-150-105-517-150-105-571-157-517-571-1_V.mp3)
  Segment 20: 79947ms ~ 89055ms (file: 754-754-574-130-753-107-317-273-217-102-312-130-103 103-765-163-143-413-132-203-167-107-675-657-154-105-213-173-107-106-137-132-103-173-170-725-135-576-632-267-265-326-301-175-1_V.mp3)
  Segment 21: 90055ms ~ 90631ms (file: 157-135-150-153-103-517-513-150-510 105-510-150-510-501-157-571-153-157-175-513-510-531-153-501-157-351-537-571-517-510-571-531-751-351-517-573-315-531-573 571-573-573-571-513-5_V.mp3)
  Segment 22: 91631ms ~ 92207ms (file: 572-571-574-574-517-517-571-573-571-571-517-517-517-157-517-571-571-571-571-573-573 574-517-571-574-571-571-576_W.mp3)
  Segment 23: 93207ms ~ 93783ms (file: 752-635-567-602-276-261-716-325-275-357-267-270-216-261-103-375-751-275-275-725-623-625-375-213-736-173-326-275-257-236-267-623-267-126-216-720-312-237-576-762-527-275-267-126-5_V.mp3)
  Segment 24: 94783ms ~ 101263ms (file: 260-261-132-210-267-301-321-263-312-213-260-267-206-316-310-361-301-236-263-263-237-237-316-307-601-165-517-375-236-763-260-762-716-107-106-107-157-352-176-107-137-216-731-710-2_W.mp3)
  Segment 25: 102263ms ~ 102839ms (file: 621-751-267-547-507-321-207-125-275-261-263-726-570-326-750-527-502-537-251-265-267-265-527-267-267-251-572-572-132-236-376-675-673-526-256-570-527-273-672-261-207-572-527-267-7_V.mp3)
  Segment 26: 103839ms ~ 112083ms (file: 132-230-230-130-231-103-302-312-132-123-130-230-310-103-132-123-102-310-310-310-301-230-321-230-157-107-230-203-132-120-231-102-201-261-265-265-320-231-320-574-507-230-302-103-3_V.mp3)
  Segment 27: 113083ms ~ 113659ms (file: 516-516-516-156-765-625-165-165-567-567-157-567-567-576-576-165-156-576-567-516-561-571-517-571-576-571-265-216-612-605-351-310-301-136-156-156-106-567-657-567-152-150-153-156-1_W.mp3)
  Segment 28: 114659ms ~ 115235ms (file: 571-570-570-576-570-570-573 756-572-574-574-571-571-751-573-571-571-571-570-751-571-571-571-576 573-751-750-751-571-751-570-753-751-571-571-570-573 571-751-751-751-751-570-750-5_W.mp3)
  Segment 29: 116235ms ~ 116811ms (file: 236-675-756-321-207-276-123-236-572-256-567-327-205-123-125-567-376-574-637-504-253-572-167-576-275-321-326-756-321-725-327-703-265-630-316-756-576-571-574-570_V.mp3)
  Segment 30: 117811ms ~ 126163ms (file: 312-310-312-210-216-317-130-320-312-132-301-321-301-310-103-132-103-203-312-316-321-201-123-206-302-312-210-312-132-312-253-106-312-302-231-317-103-617-130-203-132-263-623-652-1_V.mp3)
  Segment 31: 127163ms ~ 127739ms (file: 571-571-570-751-751 574-576-571-570-571-153-175-750-175-517-173-751-753-715-513-531-571-571-571-571 574-517-571-571-571-571-751-570-517-751-513-157-537-750-517-571-571-751 576-5_V.mp3)
  Segment 32: 128739ms ~ 137991ms (file: 130-312-132-206-103-143-130-123-126-216-210-301-321-327-102-213-135-213-230-302-320-321-130-230-231-230-123-261-756-765-260-206-206-321-106-167-106-106-201-130-730-627-261-576-2_W.mp3)
  Segment 33: 138991ms ~ 139567ms (file: 571-751-750-751 574-574-576-574-574-574-576-574-571-157-751-576-517-375-573-135-371-157 574-574-574-576-574-572-576-572-573-576-574-571-571-574-573-576-576 571-574-574-517-351-1_W.mp3)
  Segment 34: 140567ms ~ 141143ms (file: 573-275-265-637-175-326-657-627-526-267-672-576-326-526-576-216-675-236-527-275-376-275-672-352 157-571 627_W.mp3)
  Segment 35: 142143ms ~ 142719ms (file: 357-613-671-567-561-165-651-167-317-137-165-617-675-765-756-136-136-765-765-675-675-765-765-765-675-765-716-761-615-673-567-756-731-173-167-136-675-167-165-156-165-156-165-176-1_W.mp3)
  Segment 36: 143719ms ~ 144295ms (file: 170-170-170-170-175-176-715-175-715-175-716-715-761 750-157-176-107-715-701-170-170-751-175-175-175-176-175-175-176-175-175-176-715-175-176-175-715 105-175-103-710-170-173-170-1_V.mp3)
  Segment 37: 145295ms ~ 145871ms (file: 751-750-175-751-753-751-571-751-751-570-751-571-751 751-751-753-753-571-571-751-751-571-571-571-753 571-175-715-750-715-157-751-157-751-750-753-753 571-571-571-715-751-715-751-5_V.mp3)
  Segment 38: 146871ms ~ 147447ms (file: 327-267-267-275-215-502-326-237-750-236-657-605-267-567-163-705-571-237-567-765-267-671-675-760-725-265-726-751-752-756-725-752-752-706-271-672-576-756-657 256-562-231-257-750-7_V.mp3)
  Segment 39: 148447ms ~ 149023ms (file: 576-576-576-574-574-157-153-315-735-175-672-163-136-316-136-136-132-175-321-123-132-123-130-130-130-513-156-512-153-156-156-153-135-153-315-153-125-135-135-325-135-152_W.mp3)
  Segment 40: 150023ms ~ 150599ms (file: 310-130-310-130-173-175-170-157-715-175-517-175-132-130-517-571-517-517-157-137-165-125-175-156-526-621-625-657-316-102-103-136-135-172-317-312-237-732-327-317-712-715-571-157-7_W.mp3)
  Segment 41: 151599ms ~ 152175ms (file: 571-517-571-751-571-573-571-570-576 574-571-571-571-571-157-517-157-157-571-715-517-173-517-751-751-753-571-570-571-571-750-750-751 574-517-517-517-571-571-571-573-570-571-570-5_W.mp3)
  Segment 42: 153175ms ~ 153751ms (file: 571-537-752-176-613-137-671-136-132_W.mp3)
  Segment 43: 154751ms ~ 155327ms (file: 306-750-753-756-715-753-370-730 573-756-765-763-765-675-671-756-761_W.mp3)
  Segment 44: 156327ms ~ 156903ms (file: 175-157-571-574-574-574-576-571-157-571-576-573-571-751-571-572-517-756-576-173-750-756-753-750-571-751-756-571-756-576-517 157-571-576-751-576-571-571-571-753-573-576-756-751-7_W.mp3)
  Segment 45: 157903ms ~ 158479ms (file: 571-571-571-571-507-571-715-750-175-173-175-157-157-753-371-153-571-715-715-751-750-570-571-751-571-750-572-756-751 570-517-571-570-750-157-751-157-571-753-715-175-153-571-753-7_V.mp3)
  Segment 46: 159479ms ~ 160055ms (file: 752-172-263-570-316-137-762-261-723-127-756-576-521-762-213-276-263-216-570-263-327-576-657-261-506_V.mp3)
  Segment 47: 161055ms ~ 169299ms (file: 576-312-356-153-170-761-672-302-312-361-617-715-163-361-276-203-320-273-321-120-106-167-317-230-573-320-723-625-267-267-260-267-276-362-260-617-756-765-265-573-750-701-357-132-1_W.mp3)
  Segment 48: 170299ms ~ 170875ms (file: 736-175-136-623-132-123-132-132-312-372-172-317-726-765-762-752-267-672-735-765-367-725-723-357-521-356-675-351-357-315-517-572-163-136-126-123-372-723-657-752-216-351-257-253-1_W.mp3)
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
> **Time**: 2026-04-06 09:03:32
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "방공망이 잇따라 뚫리자",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "방공망이", "start_ms": 20, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "잇따라", "start_ms": 210, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "뚫리자", "start_ms": 370, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "미국이 폴란드에 패트리어트 시스템 한 대와 패트리어트 발사용 미사일을 요청했지만 폴란드 영공을 방어",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "미국이", "start_ms": 40, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "폴란드에", "start_ms": 400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "패트리어트", "start_ms": 860, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "시스템", "start_ms": 1370, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 1730, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "대와", "start_ms": 1840, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "패트리어트", "start_ms": 2200, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "발사용", "start_ms": 2760, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "미사일을", "start_ms": 3220, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "요청했지만", "start_ms": 3710, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "폴란드", "start_ms": 4480, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "영공을", "start_ms": 4890, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "방어", "start_ms": 5280, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "그렇다면 러셀 교수가 바라는 미래는 어떤 모습일까",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "그렇다면", "start_ms": 10, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "러셀", "start_ms": 140, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "교수가", "start_ms": 230, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "바라는", "start_ms": 340, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "미래는", "start_ms": 420, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 490, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "모습일까", "start_ms": 540, "dur_ms": 30, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "It is very difficult to specify explicitly",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "It", "start_ms": 15, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 70, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "very", "start_ms": 115, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "difficult", "start_ms": 200, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 325, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "specify", "start_ms": 370, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "explicitly", "start_ms": 485, "dur_ms": 75, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "정리를 해 보도록 하겠습니다 우선 구글에서 소개하고 있는 거 용량 대비 가장 강력한 성능의 오픈 모델 젬마 보트다 이런 얘기가 나오는 이유가 있죠 사실",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "정리를", "start_ms": 20, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "(music)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 7,
"transcript": "바로 프롬프트를 돌려서 프롬프트를 읽으라고 주시니깐 무한정 많은 소스를 저한테 다 주면서 이거를 그냥 이대로 다 고쳐 봐 라고 하시니까 정말 이게 제 자체가",
"speaker": "speaker_5",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "바로", "start_ms": 30, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "프롬프트를", "start_ms": 450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "돌려서", "start_ms": 980, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "프롬프트를", "start_ms": 1400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "읽으라고", "start_ms": 1880, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "주시니깐", "start_ms": 2250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "무한정", "start_ms": 2700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "많은", "start_ms": 3180, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "소스를", "start_ms": 3500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "저한테", "start_ms": 3950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 4320, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "주면서", "start_ms": 4500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이거를", "start_ms": 4950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 5320, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이대로", "start_ms": 5650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 6020, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "고쳐", "start_ms": 6200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "봐", "start_ms": 6520, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "라고", "start_ms": 6700, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "하시니까", "start_ms": 6920, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 7400, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "전문 투자자도 아니고 투자를 공부한 사람이 아니기 때문에 어떤 그런 지표와 어떤 어려운 영어 용어 같은 거 잘 이해하기가 힘들어요 근데 그거를 쉽게 설명해 주고 그 차트를",
"speaker": "speaker_6",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "전문", "start_ms": 15, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "때문에 50일 넘게 걸립니다 중동산의 두 배 이상입니다 더 비싼 운임을 들여 가져와도 우리 산업 구조에 잘 안 맞습니다 석유 화학 업계는 끈적한 중동산 중질유 위주로 정제 나프타와 벙커C",
"speaker": "speaker_7",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "때문에", "start_ms": 35, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "50일", "start_ms": 450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "넘게", "start_ms": 920, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "걸립니다", "start_ms": 1300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "중동산의", "start_ms": 1800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "두", "start_ms": 2320, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "배", "start_ms": 2500, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "이상입니다", "start_ms": 2720, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 3300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "비싼", "start_ms": 3480, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "운임을", "start_ms": 3850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "들여", "start_ms": 4280, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "가져와도", "start_ms": 4600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "우리", "start_ms": 5100, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "산업", "start_ms": 5380, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "구조에", "start_ms": 5750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 6120, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 6300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "맞습니다", "start_ms": 6480, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "사라졌답니다",
"speaker": "speaker_8",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "사라졌답니다", "start_ms": 10, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "인허가 비용 이런 것 기본적으로 500에서 1000만 원 정도 이렇게 나오더라고요 그러면 그 돈 필요해서 열심히 벌었어요",
"speaker": "speaker_8",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "인허가", "start_ms": 20, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "싸이죠 BTS예요 왜냐면 저도 강남스타일 저도 들어 봤어 싸이 저도 싸이 싸이 들어 봤는데 근데 관심 생긴 건",
"speaker": "speaker_9",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "싸이죠", "start_ms": 25, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "BTS예요", "start_ms": 550, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "왜냐면", "start_ms": 1150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "저도", "start_ms": 1580, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "강남스타일", "start_ms": 1900, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "저도", "start_ms": 2550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "들어", "start_ms": 2880, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "봤어", "start_ms": 3150, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "싸이", "start_ms": 3450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "저도", "start_ms": 3780, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "싸이", "start_ms": 4100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "싸이", "start_ms": 4450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "들어", "start_ms": 4780, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "봤는데", "start_ms": 5050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 5500, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "관심", "start_ms": 5780, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "생긴", "start_ms": 6150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "건", "start_ms": 6520, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "바뀐 거예요 오징어 게임 모양으로 구상을 했다가 제가 셀프로 작업을 하다 보니까 네 원형이 실제 아마추어가 작업하기엔 너무 힘들더라고요",
"speaker": "speaker_10",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "바뀐", "start_ms": 20, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "잘 가세요 바이 바이 간다 야 이거 오 야 야 머리 이거 내가 산 거랑 똑같은 양이야 아 너무 오 야 야 너무 감사하게",
"speaker": "speaker_11",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "잘", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "바이", "start_ms": 650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "바이", "start_ms": 980, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "간다", "start_ms": 1350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "야", "start_ms": 1750, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이거", "start_ms": 1920, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "오", "start_ms": 2200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "야", "start_ms": 2380, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "야", "start_ms": 2550, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "머리", "start_ms": 2720, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이거", "start_ms": 3100, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "내가", "start_ms": 3380, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "산", "start_ms": 3700, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "거랑", "start_ms": 3880, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "똑같은", "start_ms": 4200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "양이야", "start_ms": 4650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 5100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 5280, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "오", "start_ms": 5600, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "야", "start_ms": 5780, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "야", "start_ms": 5950, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 6120, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "감사하게", "start_ms": 6450, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "따르는 걸 느꼈고 아마 조만간 또 보수 공사가 있어야 될 것 같습니다 의외로 실링팬이 효과가 커요 아 그냥 실링팬 달아 놨어 실링팬을 저기서 이제",
"speaker": "speaker_12",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "따르는", "start_ms": 15, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "성능이 나오면서도 에이전트하게 여러 모델들을 쓸 때 일일이 API 호출하면서 돈을 막 낼 필요가 없이 온디바이스로 돌릴 수 있는 이런 모델들이 계속 나오고 있다라고 이해를 하시면 되겠죠 그리고 큰 모델도 마찬가지입니다 사실상",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "성능이", "start_ms": 20, "dur_ms": 100, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 09:03:32*
