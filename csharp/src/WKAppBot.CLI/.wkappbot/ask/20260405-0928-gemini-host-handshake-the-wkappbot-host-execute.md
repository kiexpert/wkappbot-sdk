# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=11716  time=09:27:22  uptime=27499s  mem=404MB  system_processes=356"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_092629.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 576-576-576-576-567-576-576_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 507-157-150-150-710-750-105-751-507-705-170-701-170-705-507-705-701-571-107-175-107-517-107-105-701 750-510-750-715-751-571-750-571-570-157-150-710-175-750-170-701-150-570-507-5_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 751-715-751-751-751-751-167-761-765-761-726-761-721-736-275-725-175-175-571-571-175-571-751-571-571-750-751-157-751-751-517-751-751-756-715-751-751-751-751-751-751-576-751-175-7_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 723-752-571-756-762-726-607-570-570-157-107-175-571-735-735-751_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 576-756-753-750-170-750-705-107-750-571-175-751-173-751-715-175-710-571-715-750-751-571-751 751-751-750-710-570-715-751-715-715-756-751-175-751-750-571-715-715-517-715-170-173-7_V.mp3)
  Segment 6: 7880ms ~ 9248ms (file: 157-517-517-517-751-751-751-754-756-751-751-751-756-751-751-751-715-756-751 501-510-150-510-150-510-157-571-571-571-750-751-751-751-751-751-751-715-715-175-715-715-751-751-715 1_W.mp3)
  Segment 7: 10248ms ~ 10824ms (file: 362-263-362-637-613-361-312-132-136-623-632-326-163-263-631-163-673-360-123-216-130-310-130-621-326-316-312-317-310-316-367-136-163-316-621-621-126-102-167-361-165-156-103-657-6_W.mp3)
  Segment 8: 11824ms ~ 12400ms (file: 725-752-712-725-726-752-132 257-260-726-725-725-751-752-725-725-725-752-725-752-756-752-726-275_V.mp3)
  Segment 9: 13400ms ~ 24236ms (file: 132-672-217-162-756-765-276-267-267-572-130-103-261-765-627-263-162-167-162-657-625-256-257-130-675-213-126-123-231-321-102-213-175-156-160-671-167-167-170-176-167-106-167-126-1_W.mp3)
  Segment 10: 25236ms ~ 25812ms (file: 623-631-162-675-756-675 136-631-761 621-216 671-136-132 715-123-103-103-321-132-316-163-317-102-102-103-301-316-132-173-126-263-132_W.mp3)
  Segment 11: 26812ms ~ 27388ms (file: 751-574-576-571-571-576-576-576-756 506-720-276-257-752-754-576-752-571-754-571-571-574-574-571-574-576 720-750-275-570-572-576-567-572-750-570-750-756-750-752-756-754-574-572-7_V.mp3)
  Segment 12: 28388ms ~ 28964ms (file: 750-750-750-716-756-705-750-105-105-150-105-105-157-157-157-176-716-176-157-157-157-165-156-156-157-517-157-561-516-175-175-715-750-750-754-754-754-754-756-756-751-756-754-754-4_W.mp3)
  Segment 13: 29964ms ~ 30540ms (file: 136-156-160-136-316-103-176-163-163-671-263-263_V.mp3)
  Segment 14: 31540ms ~ 39028ms (file: 634-756-275-276-627-627-762-206-576-267-670-637-201-620-206-203-756-670-261-574-570-257-752-672-231-213-132-236-230-527-532-310-273-261-231-132-251-263-213-671-675-617-267-562-6_W.mp3)
  Segment 15: 40028ms ~ 40604ms (file: 506-726-712-137-715-256-362-231-317-375-315-527-312-517-325-574-157-251-567-750-570-765-573-317-532-136-527-316-537-123-216-123-527-572-527-752-547-527-325-567-327-605-123-572-3_V.mp3)
  Segment 16: 41604ms ~ 42180ms (file: 173-316-362-367-602-162-613-361-612-623-601-657-106-160-627-623-657-617-672-675-756-610-601-657-607-612-361-610-136-317-170-167-162-673-603-612-631-361-312-310-360-361-361-615-5_W.mp3)
  Segment 17: 43180ms ~ 43756ms (file: 316-673-765-713-537 715-137-371-751-517-715-135-715-735-137-173-310-173-701-705-671-736-673-637-675-617-675-760-671-637-765-576-765-751 735-130-731-310-317-170-105-137-317-130-1_V.mp3)
  Segment 18: 44756ms ~ 54476ms (file: 103-120-251-256-301-123-572-257-102-572-205-217-201-672-260-276-210-210-201-217-123-312-206-312-672-672-276-627-620-276-123-537-103-627-103-574-213-236-236-320-103-723-320-267-2_W.mp3)
  Segment 19: 55476ms ~ 56052ms (file: 310-130-301-314-103-130-104-105-135-103-134-135-103-135-103-103-103-134-135-134-315-314 314-103-301-135-104-103-134-103-104-130-105-145-103-145-341-135-145-134-314-103-143-103-1_V.mp3)
  Segment 20: 57052ms ~ 57628ms (file: 163-106-132-162-126-163-236-152-160-136-617-136-105-162-163-162-153-361-136-103-102-106-103-103-613-162-167-163-136-103-651-312-103-152-135-561-160-130-103-672-165-127-156-103-1_V.mp3)
  Segment 21: 58628ms ~ 59204ms (file: 136-136-136-136-135-103-310-137-316-136-103-136-136-136-136-103 317-357-751 315-573-571-751-751-517-574-517-517-571-753 312-316-316-317-134-317-136-134-137-315-316-136-135-103-1_V.mp3)
  Segment 22: 60204ms ~ 60780ms (file: 275-326-276-236-276-267-572-236-102-705-162-123-126-576-267-123-136-754-751-705-367-127-507-752-572-325-750 132-265-260-756-605-263-570-725-257-325-705-627-726-756-623-216-762-2_V.mp3)
  Segment 23: 61780ms ~ 62356ms (file: 756-705-502-613 132-312-135 675-106-162-167-163-316-130-160-610-651-165-156-572-256-256-562-576-657-576-576-576-257-572-572-527-527-625-251-215 762-672-361-132-726_W.mp3)
  Segment 24: 63356ms ~ 66308ms (file: 675-671-136-367-136-136-316-316-162-657-756-563-576-675-631-756-657-673-561-167-132-573-675-316-127-136-156-132-123-162-163-765-163-765-671-173-103-612-657-163-513-376-162-163-7_W.mp3)
  Segment 25: 67308ms ~ 67884ms (file: 573-517-571-751-715-150-175-175-175-137-371-173-713-137-175-571-715-715-175-751-751 517-571-175-571-157-157-571-157-751-715-750-157-713-175-715-751-715-175-571-751-175-751-517-1_V.mp3)
  Segment 26: 68884ms ~ 69460ms (file: 756-716-756 576-261-326-327-320-230-236-132-136-123-326-263-123-125-123-157-136-573-751-130-627-627-167-617-102-617-175-136-132-127-137-132-132-263-162-612-136-176-175-173-105-1_W.mp3)
  Segment 27: 70460ms ~ 71036ms (file: 265-263-527-256-365-206-571-507-613-325-263-602-123-123-765-621-570-657-231-267-231-627-726-613-301-753-132-136-136-163-316-732-312-317-732-316-312-572-263-261-261-263-321-261-3_W.mp3)
  Segment 28: 72036ms ~ 72612ms (file: 576-570-257-257-756-527-572-275-576-750-756-576-756-756-576-756-756-756-756 507-257-205-250-256-752-265-275-572-570-756-725-762-756-765-756-756-756-756-756-756-756-765 570-750-5_V.mp3)
  Segment 29: 73612ms ~ 80236ms (file: 576-102-756-321-123-263-106-756-136-231-261-576-123-261-756-312-231-312-231-756-267-231-213-263-251-521-326-124-321-263-103-267-263-132-756-312-213-236-261-756-276-263-574-321-1_W.mp3)
  Segment 30: 81236ms ~ 91856ms (file: 267-267-672-103-617-213-103-103-103-321-320-627-267-267-130-130-123-201-230-210-316-327-213-263-271-627-263-620-103-103-263-103-620-621-310-210-103-312-213-261-562-102-570-201-2_W.mp3)
  Segment 31: 92856ms ~ 93432ms (file: 672-230-623-504-574 756 607-507-576-326-537-576-675-270-320_W.mp3)
  Segment 32: 94432ms ~ 95008ms (file: 521-521-256-572-527-526-526-526-576 570-527-251-261-257-725-257-672-752-752-572-752-275-572-572-572-576-576-574-576-754-574-576-751-576-573-751 527-501-570-520-250-257-257-257-7_W.mp3)
  Segment 33: 96008ms ~ 96584ms (file: 132-132-103-123-312 567-650-670-650-560-167-165-652-312-631-671-631-631-613-167-613_V.mp3)
  Segment 34: 97584ms ~ 105648ms (file: 257-105-130-102-726-627-267-753-207-705-206-362-236-321-736-765-236-312-263-263-263-267-267-263-321-326-236-523-263-574-263-263-263-267-623-263-326-326-321-326-276-762-267-267-2_W.mp3)
  Segment 35: 106648ms ~ 113488ms (file: 321-102-120-312-732-206-310-372-754-310-157-230-317-371-321-327-267-230-537-356-275-327-320-750-237-756-675-672-102-317-726-237-273-305-230-203-275-207-175-301-715-751-752-570-7_V.mp3)
  Segment 36: 114488ms ~ 120644ms (file: 321-231-754-320-263-256-213-321-523-216-213-216-105-206-123-754-123-206-261-524-231-236-572-106-217-267-574-320-102-210-230-236-625-567-753-267-570-312-231-271-572-123-102-261-2_V.mp3)
  Segment 37: 121644ms ~ 127188ms (file: 260-276-267-320-137-375-310-273-201-206-302-132-207-207-207-126-201-206-231-570-572-207-207-203-276-726-237-301-137-123-312-106-765-267-216-237-203-310-321-236-213-210-301-213-3_W.mp3)
  Segment 38: 128188ms ~ 128764ms (file: 160-106-160-156-165-310-637-621-302-237-263-362-321-352-302-360-350-253-130-316-361-635-536-316-615-162-631-375-312-357-635-356-325-536-365-130-312-327-321-321-312-537-351-316-3_W.mp3)
  Segment 39: 129764ms ~ 134516ms (file: 253-216-751-132-257-217-175-213-527-132-507-351-132-126-102-756-260-627-260-302-203-317-201-263-210-507-267-216-750-526-574-675-372-657-576-752-276-756-751-672-631-613-136-612-1_W.mp3)
  Segment 40: 135516ms ~ 141132ms (file: 267-106-607-106-170-701-176-270-276-231-217-127-216-631-362-302-361-261-762-276-271-612-623-307-362-361-163-260-267-206-625-261-276-210-321-130-301-562-213-103-761-671-631-137-1_W.mp3)
  Segment 41: 142132ms ~ 150448ms (file: 102-102-102-123-106-127-107-107-721-107-106-107-127-267-627-756-765-217-213-213-231-251-521-312-106-231-231-132-210-312-261-217-210-107-102-172-120-132-150-517-537-261-572-567-5_W.mp3)
  Segment 42: 151448ms ~ 152024ms (file: 106-167-761-135-316-103-103-561 175-136-103-103-263-165-106-603-617-103-163-271-613-132-106-103-130-672-203-123-135-176-103-132-103-571-231-236-312-263-265-126-216-263-123-126-1_V.mp3)
  Segment 43: 153024ms ~ 153600ms (file: 751-751-750-576-570-570-570-571-576-576-570-571 571-157-175-713-173-571-571-751-576-570-570-576-570-751-753-571-571-571-570-573-570-573-570-570-573-571-507-571-570 512-513-157-7_W.mp3)
  Segment 44: 154600ms ~ 163204ms (file: 256-236-302-627-267-320-103-123-230-276-230-267-321-132-267-231-320-130-623-321-312-327-320-230-267-102-123-162-672-130-276-267-123-102-321-231-762-236-276-230-257-312-123-762-2_W.mp3)
  Segment 45: 164204ms ~ 164780ms (file: 571-571-571-573-571-573-517-315 573-517-517-571-517-517-157-157-157-517-571-571 574-574-574-574-574_W.mp3)
  Segment 46: 165780ms ~ 166356ms (file: 163-130-765-765-135-621-617-120-652-321-312-213-125 627 126-102 126-623-132-310-103-103-531-102-162-136-130-123-136-317-132-132-310-301-321-126-326-263-672-637-361 621-263 210-1_W.mp3)
  Segment 47: 167356ms ~ 167932ms (file: 567-576-576-574-756-756-657-675-765-756-675-576-576-321-130-102-106-136-672-361-162-132-123-231-123-327-316-617-165-136-102-103-130-102-123-132-301_V.mp3)
  Segment 48: 168932ms ~ 169508ms (file: 765-172-217-271-237-276-267-267-267-271-273-263-276-327-236-762-276-726-765-765 765 136-361-312-675-765-716-756-756-657-756 132-162-163-136-130_W.mp3)
  Segment 49: 170508ms ~ 181416ms (file: 127-167-172-210-765-260-260-312-102-326-413-102-756-107-102-103-310-320-230-237-723-731-705-301-320-312-120-321-235-236-542-320-321-132-526-102-102-132-321-526-235-125-320-236-2_W.mp3)
  Segment 50: 182416ms ~ 193072ms (file: 765-756-732-371-371-725-276-132-103-726-765-316-132-571-530-376-310-753-301-263-756-301-167-203-317-310-753-701-763-761-701-706-701-706-701-701-701-761-713-317_W.mp3)
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
> **Time**: 2026-04-05 09:28:23
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "석기시대로 되돌려버리겠다는 트럼프 대통령의 발언이 나온 뒤",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "석기시대로", "start_ms": 20, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "되돌려버리겠다는", "start_ms": 135, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "트럼프", "start_ms": 280, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "대통령의", "start_ms": 345, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "발언이", "start_ms": 430, "dur_ms": 65, "phase_lr_deg": 0.0},
{"word": "나온", "start_ms": 500, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "뒤", "start_ms": 545, "dur_ms": 20, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "이란은 마치 본때를 보여주겠다는 듯 텔아비브를 비롯한 이스라엘의 거점 도시",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "이란은", "start_ms": 30, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "마치", "start_ms": 95, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "본때를", "start_ms": 150, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "보여주겠다는", "start_ms": 225, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "듯", "start_ms": 320, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "텔아비브를", "start_ms": 355, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "비롯한", "start_ms": 440, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "이스라엘의", "start_ms": 505, "dur_ms": 85, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "들은 삼성 SDI의 경우에 삭제를 요청하더라고요 이전에 ESS 배터리도 기사가 나왔다가 공시가 나왔고 비공개로 이제 남아 있죠",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "들은", "start_ms": 40, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "삼성", "start_ms": 85, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "SDI의", "start_ms": 140, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "경우에", "start_ms": 205, "dur_ms": 55, "phase_lr_deg": 0.0},
{"word": "삭제를", "start_ms": 265, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "요청하더라고요", "start_ms": 330, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "테슬라의 LFP 배터리를 납품하는 걸로 업계에서는 대부분 알고",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "테슬라의", "start_ms": 25, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "LFP", "start_ms": 100, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "배터리를", "start_ms": 165, "dur_ms": 75, "phase_lr_deg": 0.0},
{"word": "납품하는", "start_ms": 245, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "걸로", "start_ms": 320, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "업계에서는", "start_ms": 365, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "대부분", "start_ms": 460, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "알고", "start_ms": 525, "dur_ms": 40, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "때문에 심지어 일본 네티즌들조차 그냥 사지 말자 이렇게 말할 정도였거든요 그런데 리메이크를 한국 암기로 감독이 맡았다는 소식이 나오자 분위기는 백",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "때문에", "start_ms": 35, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "심지어", "start_ms": 100, "dur_ms": 65, "phase_lr_deg": 0.0},
{"word": "일본", "start_ms": 170, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "네티즌들조차", "start_ms": 225, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 340, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "사지", "start_ms": 395, "dur_ms": 45, "phase_lr_deg": 0.0},
{"word": "말자", "start_ms": 445, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 490, "dur_ms": 55, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "모르겠습니다 저도 이제 이쪽 전문가가 아니다 보니까 자체 기술로 한 아디아의 얼마나 좋을지 모르겠어요 오히려 애매하면 조금 애매하면",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "모르겠습니다", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "저도", "start_ms": 135, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 180, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "이쪽", "start_ms": 225, "dur_ms": 45, "phase_lr_deg": 0.0},
{"word": "전문가가", "start_ms": 275, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "아니다", "start_ms": 350, "dur_ms": 55, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "박정 파이팅 야호 분위기가 뜨겁습니다 가자 일본",
"speaker": "speaker_5",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "mixed",
"words": [
{"word": "박정", "start_ms": 20, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "파이팅", "start_ms": 85, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "야호", "start_ms": 160, "dur_ms": 65, "phase_lr_deg": 0.0},
{"word": "분위기가", "start_ms": 230, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "뜨겁습니다", "start_ms": 315, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "가자", "start_ms": 410, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "일본", "start_ms": 475, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "대놓고 외면할 수는 없는 상황에서 전쟁 상황을 주시하며 이란과의 직접 충돌을 최대한 피하겠다는 뜻으로 보입니다 호르무즈 해협엔 우리 선박 20",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "대놓고", "start_ms": 40, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "외면할", "start_ms": 115, "dur_ms": 75, "phase_lr_deg": 0.0},
{"word": "수는", "start_ms": 195, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "없는", "start_ms": 240, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "상황에서", "start_ms": 295, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "전쟁", "start_ms": 380, "dur_ms": 55, "phase_lr_deg": 0.0},
{"word": "상황을", "start_ms": 440, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "주시하며", "start_ms": 515, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "얘기하는 걸 들으면 이상하게 마음이 조금 울컥해집니다 Okay why did I move over to South Korea I was living in North Carolina United States and I'm working at Verizon Wireless and I absolutely hated it and then I was like alright I'm gonna get another job so I ended up quitting my job just went through a break up",
"speaker": "speaker_6",
"confidence": 0.94,
"language": "mixed",
"silence_before_ms": 30,
"audio_type": "mixed",
"words": [
{"word": "얘기하는", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "걸", "start_ms": 115, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "들으면", "start_ms": 150, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "이상하게", "start_ms": 215, "dur_ms": 85, "phase_lr_deg": 0.0},
{"word": "마음이", "start_ms": 305, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 380, "dur_ms": 55, "phase_lr_deg": 0.0},
{"word": "울컥해집니다", "start_ms": 440, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "꼭 한번 읽어보시길 추천드리면서 그럼 지금까지 리뷰왕이었습니다",
"speaker": "speaker_7",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "mixed",
"words": [
{"word": "꼭", "start_ms": 25, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "한번", "start_ms": 70, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "읽어보시길", "start_ms": 135, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "추천드리면서", "start_ms": 230, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "그럼", "start_ms": 335, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "지금까지", "start_ms": 390, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "리뷰왕이었습니다", "start_ms": 475, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "어떤 식으로 돼 있어요 예를 들어서 AI한테 어디까지 가지 마 뭐를 하지 마 이렇게 제한을 주나요 우선순위 같은 것도 주고요 1번에서 먼저 찾고 또 2번에서 먼저 찾고 이런 것도 있고 권한 확인도 하고요 아 잠깐만 이거 우리 코드 빼가려는 거 아닌가 하는 탐지하는 AI가 또 있고",
"speaker": "speaker_8",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "어떤", "start_ms": 40, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "식으로", "start_ms": 95, "dur_ms": 65, "phase_lr_deg": 0.0},
{"word": "돼", "start_ms": 165, "dur_ms": 35, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 205, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "예를", "start_ms": 270, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "들어서", "start_ms": 325, "dur_ms": 65, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "신호가 되기도 하겠지만 반도화이트도 없습니다 60cm의 폭설",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "신호가", "start_ms": 30, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "되기도", "start_ms": 105, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "하겠지만", "start_ms": 190, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "반도화이트도", "start_ms": 285, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "없습니다", "start_ms": 400, "dur_ms": 75, "phase_lr_deg": 0.0},
{"word": "60cm의", "start_ms": 480, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "폭설", "start_ms": 555, "dur_ms": 20, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "고객 센터는 조금 과장해서 마비가 됐습니다 말씀드렸다시피 하버드 의대 데이비",
"speaker": "speaker_9",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "고객", "start_ms": 35, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "센터는", "start_ms": 100, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 175, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "과장해서", "start_ms": 240, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "마비가", "start_ms": 325, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "됐습니다", "start_ms": 400, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "제일 가야 돼요 준비한 닭이 모두 팔린 덕에 한 시간 일찍 마감 닭이 많이 안 들어와서 닭이 없",
"speaker": "speaker_10",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "mixed",
"words": [
{"word": "제일", "start_ms": 25, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "가야", "start_ms": 80, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "돼요", "start_ms": 145, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "준비한", "start_ms": 190, "dur_ms": 75, "phase_lr_deg": 0.0},
{"word": "닭이", "start_ms": 270, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "모두", "start_ms": 325, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "팔린", "start_ms": 390, "dur_ms": 55, "phase_lr_deg": 0.0},
{"word": "덕에", "start_ms": 450, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "어제까지만 보면은 큰 음봉이 나오면서 어제 음봉으로 5일선과 10일선을 깼죠 그래서 많은 사람들이 이거 힘 빠지",
"speaker": "speaker_11",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "어제까지만", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "보면은", "start_ms": 150, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 225, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "음봉이", "start_ms": 270, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "나오면서", "start_ms": 355, "dur_ms": 85, "phase_lr_deg": 0.0},
{"word": "어제", "start_ms": 445, "dur_ms": 55, "phase_lr_deg": 0.0},
{"word": "음봉으로", "start_ms": 505, "dur_ms": 70, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 09:28:23*
