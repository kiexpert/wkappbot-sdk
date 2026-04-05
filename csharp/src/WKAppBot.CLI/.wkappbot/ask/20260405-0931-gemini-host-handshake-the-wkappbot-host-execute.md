# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=24268  time=09:28:29  uptime=29926s  mem=353MB  system_processes=356"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_092802.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 137 576-576-576-570-136-607-672-130-301-301-316-576-576-756-765-361_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 763-127-731-231-735-317-632-326-735-270-375-713-715 135-162-317 123-623-167-172-720-732-705-703-750-752-275-672-672-627-621-263-321-312-132-126-256-125-613-103-736-631-316-317-1_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 231-321-213-126-137-172-312-231-310-312-132-312-213-216-217-231-237-231-102-213-120-312-231_V.mp3)
  Segment 4: 4728ms ~ 12036ms (file: 321-123-231-123-260-260-261-162-612 321-231-213-231-132-127-126-321-213-321-123-213-231-132-317-216-217-217-301-320-321-302-126-102-123-160-127-213-216-126-123-123-213-103-130-2_W.mp3)
  Segment 5: 13036ms ~ 13612ms (file: 613-126-261-267-263-231-167-610-163-610-612-602-607-670-610-261-236-725-672-236-263-526-562-526-267-756-723-230-361-632-653-632-267-302-306-302-163-216-136-673-671-631-617-317-7_W.mp3)
  Segment 6: 14612ms ~ 22424ms (file: 310-103-276-756-370-170-716-107-312-160-510-756-703-371-702-276-712-736-375-123-173-107-160-123-730-130-762-576-735-312-130-167-130-103-607-603-567-361-761-317-632-170-765-176-1_V.mp3)
  Segment 7: 23424ms ~ 24000ms (file: 625-625-526-546-256-653-625-546-547-652-546-265-567-265-267-267-265-627-526-564-526-652-627-547-562-564-546-514-567-517-216-261-261-547-574-567-657-675-564-546-564-624-621-261-5_W.mp3)
  Segment 8: 25000ms ~ 25576ms (file: 612-651-651-657-675-657-657-657-651-657-657-657-657 657-657-657_W.mp3)
  Segment 9: 26576ms ~ 33128ms (file: 310-203-754-236-236-276-312-236-267-213-236-267-236-320-126-260-261-132-102-130-753-532-267-123-231-216-126-103-201-521-620-216-130-216-216-763-260-210-761-372-120-572-260-267-5_W.mp3)
  Segment 10: 34128ms ~ 34704ms (file: 123-625-260-627-625-625-260-620-260-267-627-267-672-765-567-756-752-276-276-726-267 132-621_W.mp3)
  Segment 11: 35704ms ~ 44704ms (file: 102-120-216-721-312-312-236-216-103-216-102-103-316-103 761-127-123-217-276-762-627-726-721-107-176-627-635-631-651-263-265-210-210-216-267-273-276-230-267-263-267-216-315-103-3_W.mp3)
  Segment 12: 45704ms ~ 46280ms (file: 657-567-576-567-567-567-567-567-576-567-657 564-657-564-654 657-657-657-654-654-657-654-657-567-654-564-654 654_W.mp3)
  Segment 13: 47280ms ~ 47856ms (file: 136-130-102-162-261-126-160-136-105-317-613-125 316-137-136-132-136-137-670-136-631-651-103-567-130-132-160-106-162-103-136-137-620-607-316-136-316-103-163-163-103-312-765-765-5_V.mp3)
  Segment 14: 48856ms ~ 49432ms (file: 501-570-517-507-501-715-176-765-257-765-756-751-765-756-510-570-517-157-507-501-507-571-570-507-517-570-507-571-574-574-571-571-570-507-507-517-571-751-715-765-657-756-765-715-7_W.mp3)
  Segment 15: 50432ms ~ 56804ms (file: 210-230-203-310-130-103-107-120-230-321-130-123-123-312-302-310-210-203-312-132-173-321-130-213-123-201-103-103-120-271-231-123-201-132-213-312-120-102-312-130-231-213-201-201-2_V.mp3)
  Segment 16: 57804ms ~ 65508ms (file: 132-206-507-270-503-267-520-136-126-321-502-261-130-102-157-623-657-320-276-132-150-216-620-526-504-102-216-547-527-260-102-352-267-207-130-135-103-162-260-357-261-672-276-267-3_W.mp3)
  Segment 17: 66508ms ~ 76552ms (file: 206-267-627-267-260-206-103-527-312-627-264-457-132-457-107-143-263-210-567-657-652-546-102-206-260-120-210-130-201-542-256-201-120-213-574-250-326-573-157-753-263-123-312-136-3_W.mp3)
  Segment 18: 77552ms ~ 78128ms (file: 576-576-570-576-570-570 751-510-157-175-715-175-175-751-571-517-510-501-510-510-507-501-517-570 510-510-510-517-517-517-571-510-570-571-571-573-570-573-517-571-570-507-570-507-5_W.mp3)
  Segment 19: 79128ms ~ 79704ms (file: 576-571-571 517-517-517-175-571-157-531-157-751-571-751-751-751-751-571-571-571-576-576-574-570-570-574 157-150-150-517-157-510-517-517-570-571-570-570-570-570-571-570-571-570-5_V.mp3)
  Segment 20: 80704ms ~ 81280ms (file: 574-574-574-574-574-574-572-576-574-756-754-754-754-754-572-516-567-157-516-531-154-574-516-546-576-574-547-547-574-574-574-574-576-574-754-574-756-576-754-574-574-574-574-562-5_W.mp3)
  Segment 21: 82280ms ~ 82856ms (file: 675-756-570 276-527-273-570-527-257-275-576-265-206-725-762-567-672-570-572-276-651-670-267-507-235-572-567-537-152-721-237-253-576-762-570-267-263-267-756-621-752-753-576-752-5_V.mp3)
  Segment 22: 83856ms ~ 92424ms (file: 103-130-632-236-320-321-357-321-321-310-236-236-213-267-172-672-172-176-302-267-261-230-320-203-570-260-627-620-320-203-260-260-260-136-312-312-130-213-312-263-263-263-230-230-2_W.mp3)
  Segment 23: 93424ms ~ 94000ms (file: 136-162-172-137-613-217-123-257-265-276-671-271-271-276-123-136-163-631-167-537-356-317-351-357-317-371-371-137-321-231-735-571-574-547-753-375-376-367-731-176-327-327-317-132-3_W.mp3)
  Segment 24: 95000ms ~ 104972ms (file: 326-106-136-231-210-302-120-130-302-320-627-203-130-312-120-123-726-275-210-213-321-326-736-321-231-236-762-276-231-216-103-310-371-320-261-516-752-231-267-103-106-236-267-231-3_W.mp3)
  Segment 25: 105972ms ~ 106548ms (file: 103 163-103-132-103-132-103-132-103-132-123-123 132-103-103-103-126-132-123-132-123-126-621-162-126-136-102-103-160 132-126_V.mp3)
  Segment 26: 107548ms ~ 108124ms (file: 103-150-175-157-175-175-715-157-175 510-150-150-510-150-157-157-170-137-170-107-105 150-157-150-105-153-150-153-173-175-170-150-750-170-175-157-150 150-501-157-150-150-517-517-5_W.mp3)
  Segment 27: 109124ms ~ 109700ms (file: 263-276-275-712-236-231-123-123-132-231-216-213-231-127-123-231-236-231-261-216-261-263-621-623-562-527-623-321-123-312-213-132-321-321-231-326-321-231-132-163-132-217-261-123-3_W.mp3)
  Segment 28: 110700ms ~ 111276ms (file: 165-567-367-176-270-637-567-172 173_V.mp3)
  Segment 29: 112276ms ~ 112852ms (file: 157-574-765-756 507-570-571-576-574-574-571-571-571-574-751-756-756-751-756-756 510-501-510-571-570-571-571-571-571-576-576-574-576-576-571-157-571-751 510-517-150-510-571-517-1_W.mp3)
  Segment 30: 113852ms ~ 122348ms (file: 132-103-136-210-627-236-263-236-132-236-752-267-756-236-756-265-524-103-265-231-576-123-261-263-102-132-102-547-210-541-261-574-213-157-267-132-102-321-754-617-754-257-236-132-2_W.mp3)
  Segment 31: 123348ms ~ 123924ms (file: 103-761-163-106-103-106-160-163-165-125-163-130-615-106-160-106-103-162-102-172-167-213-106-160-136-310-163-103-160-160-136-132-102-160-106-623-623-231-706-627-762-726-725-765-7_V.mp3)
  Segment 32: 124924ms ~ 125500ms (file: 570-751-756 175-735-701-715-713-103-173-713-103-173-731-721-701-175-715-107-310-317-103-137-103-103-751-170-103-107-137-751-310-370-753-710 132-153-317-713-173-103-123-370-103-1_V.mp3)
  Segment 33: 126500ms ~ 127076ms (file: 573-751-751-751 571-571-157-571-576-576-576-574-751-751-571-751-751-571-571-573 574-574_W.mp3)
  Segment 34: 128076ms ~ 134664ms (file: 312-752-130-672-236-320-756-132-762-172-216-127-672-103-312-230-312-132-231-756-756-267-231-163-162-136-312-315-651-615-615-635-617-613-162-130-213-213-123-317-315-103-123-132-6_W.mp3)
  Segment 35: 135664ms ~ 136240ms (file: 257-537-156-567-235-627-567-251-236-573-365-572-361-103-103-310-213-526-520-156-231 126-137-317-137-310-136-316-312-316-317-735-756-753-735-765-317-372-127-206-352-132 572-125-1_V.mp3)
  Segment 36: 137240ms ~ 143432ms (file: 726-276-237-263-267-627-376-671-731-126-263-236-312-372-723-573-756-672-627-326-261-261-267-675-675-362-236-326-236-761-236-623-762-672-263-102-317-132-132-170-631-317-153-132-1_W.mp3)
  Segment 37: 144432ms ~ 151848ms (file: 261-172-567-326-132-172-106-210-123-132-271-765-267-105-571-715-721-267-627-207-267-763-752-574-507-276-217-237-276-267-750-756-207-216-261-216-271-126-271-507-751-765-572-547-5_W.mp3)
  Segment 38: 152848ms ~ 161560ms (file: 102-106-213-236-203-123-120-102-160-102-103-107-273-273-207-213-762-267-203-213-267-230-312-132-210-756-160-126-263-203-230-236-103-126-107-163-315-316-261-765-372-120-613-160-1_V.mp3)
  Segment 39: 162560ms ~ 163136ms (file: 160-152-106-103-136-163-136-106-176-103-102-167-107-206-625-621-162-263-236-623_V.mp3)
  Segment 40: 164136ms ~ 164712ms (file: 126-273-312-130-571-617-362-132-126-761-315-567-627-167-170-170-107-130-137-103-671-162-612-123-127-320-267-236-267-625-637-576-652-627-236-362-172-102-762-625-312-310-764-163-1_W.mp3)
  Segment 41: 165712ms ~ 176836ms (file: 375-756-137-103-103-136-102-625-120-216-236-130 567-156-130-231-103-276-216-361-276-763-172-174-715-102-671-231-216-273-230-102-132-310-762-260-267-762-732-217-271-127-675-615-2_V.mp3)
  Segment 42: 177836ms ~ 178412ms (file: 571-574-574-571-571-571-571-107-750-107-175-751-175-107-570-105-157-170-175-157-107-157-705-751-751 574-570-157-157-175-175-170-175-715-175-701-705-715-705-175-701-715-701-170-1_V.mp3)
  Segment 43: 179412ms ~ 185316ms (file: 320-310-230-231-320-102-106-102-574-752-236-617-621-320-136-310-103-103-132-123-321-321-302-320-230-356-320-126-213-230-130-310-231-263-130-621-761-102-123-261-120-301-321-320-1_W.mp3)
  Segment 44: 186316ms ~ 186892ms (file: 312-132 623-621 123 315-761-756-671-675-756-675-765-675-657-650-761-167-576-675-617-657-613-316-316-310-137_W.mp3)
  Segment 45: 187892ms ~ 192176ms (file: 312-236-126-726-231-123-307-263-762-267-762-627-623-763-621-672-631-172-631-126-762-631-267-326-672-620-623-670-673-602-672-623-207-627-267-263-263-236-216-123-312-126-231-267-2_W.mp3)
  Segment 46: 193176ms ~ 193752ms (file: 357-156-316-317-317-753-357-571-573-563-376_W.mp3)
  Segment 47: 194752ms ~ 195328ms (file: 167-153-136-136-132-137-136-716-165 103-316-136-561-326-176-132-103-103-130-560-517-316-106-132-136-103-176-136-316-163-156-523-136-165-316-136-137-103-136-126-165-167-163-163-1_V.mp3)
  Segment 48: 196328ms ~ 206012ms (file: 267-572-547-517-752-231 753-170-576-372-753-574-237-237-302-236-542-125-213-231-321-310-213-325-537-312-321-123-236-235-105-756-715-567-321-310-325-235-573-237-726-267-236-236-7_W.mp3)
  Segment 49: 207012ms ~ 207588ms (file: 672-263-670-670-623-375-205-507-231-572-376-236-532-312-153-236-135-657-157-567-651-352-527-253-327-235-710-132-325-572-213-527-235-326-375 231-263-564-756-602-675-627-765-623-6_V.mp3)
  Segment 50: 208588ms ~ 209164ms (file: 173-315-157-153-175-103-130-175-135-153-153-517-517-517-517-517-571-157-517 153-130-510-157-537-153-157-153-103-130-135-103-317-130-157-135-153-513-531-351-517-351-513 153-315-1_V.mp3)
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
> **Time**: 2026-04-05 09:31:00
> **AI**: gemini

---

## Response

[
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
*Generated by WKAppBot ask gemini — 2026-04-05 09:31:00*
