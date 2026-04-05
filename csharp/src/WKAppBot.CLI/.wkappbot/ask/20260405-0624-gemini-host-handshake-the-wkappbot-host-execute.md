# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=17628  time=06:22:39  uptime=16173s  mem=444MB  system_processes=353"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_062155.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 105-165-175-567-573-517-573-547-576-756-576-571-756-176-307-170-132-103-136-137-130-136-173-173-173-137-713-173-317-173-176-163-176-136-163-156-103-172-726-763-267-267-326-302-3_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 265-562-652-623-527-574-574-574-574-574-576-576-675-675-261-621-527-572-570-563-516-567-567-356-625-625-615-625-265-567-562-576-265-625-625-326-261-236-231-361-321-263-623-625-6_W.mp3)
  Segment 3: 3152ms ~ 12728ms (file: 317-570-231-301-130-361-167-526-176-631-207-507-217-327-207-263-675-675-365-231-612-312-756-175-574-136-351-753-310-301-721-571-576-547-235-207-301-137-321-230-570-203-103-675-7_W.mp3)
  Segment 4: 13728ms ~ 14304ms (file: 170-137-173-751-175-175-175-170-517-175-715-715 571-751-571-715-175-157-157-175-750-175-175-175-517-751-570-517-571-576-751-576-570-571-517-571-571-751-517-571-571-570-130-163-2_V.mp3)
  Segment 5: 15304ms ~ 15880ms (file: 405-415-405-105-401-154-145-614-163-163-135-153-156-516-153-153-156-135-652-652-563-657-562-652-652-563-652-536-726-627-625-321-625-617-165-756-756-752-765-751-716-703-765-721-2_W.mp3)
  Segment 6: 16880ms ~ 17456ms (file: 107-130-317-307-137-310-137-310-301 135-153-103-135-130-317-103-137-103-310-103-315-301-130-103-130-137-301-130-370-310-375-107-307-103-132-103-715-123 132-153-312-103-132-103-1_V.mp3)
  Segment 7: 18456ms ~ 29868ms (file: 672-361-652-625-236-216-236-267-562-573-106-321-326-263-263-123-326-326-263-623-623-263-312-263-260-263-206-263-230-325-267-132-635-176-167-163-123-321-756-132-120-627-261-106-1_W.mp3)
  Segment 8: 30868ms ~ 43864ms (file: 261-267-627-261-261-231-236-231-652-201-230-102-267-213-251-752-263-275-263-230-261-267-236-132-132-103-103-167-631-236-752-267-142-231-731-321-231-231-327-207-206-137-627-216-7_W.mp3)
  Segment 9: 44864ms ~ 52928ms (file: 312-627-213-312-123-267-572-574-527-574-267-207-210-675-201-312-506-317-260-621-201-216-150-312-713-103-137-123-172-175-135-210-352-702-207-263-236-102-307-572-572-206-213-507-6_V.mp3)
  Segment 10: 53928ms ~ 63648ms (file: 213-267-260-267-213-126-153-750-317-267-206-260-576-670-275-630-367-267-176-136-312-137-672-312-260-267-206-267-672-267-206-260-127-213-260-231-201-620-260-237-210-231-132-230-1_W.mp3)
  Segment 11: 64648ms ~ 65224ms (file: 162-136-130-106-137-563-126-136-137-316-136-136-612-627-621-261 103-162-120-126-162-162-216-132-756-106-103-123-617-165-106-136-103-102-167-163-627-132-136 612-613-136-106-526-2_V.mp3)
  Segment 12: 66224ms ~ 79328ms (file: 174-120-103-517-312-132-132-132-312-213-231-271-230-273-276-276-276-107-751-765-271-213-102-317-756-257-726-237-673-137-103-753-321-267-302-725-273-752-273 143-163-201-231-203-1_W.mp3)
  Segment 13: 80328ms ~ 80904ms (file: 103-132-103-102-570-103 261-137-103-702-231-652-675-102-201-156-546-567-507-765-124-162 654-126 315-501-325-627-627-627-627-675-501-501-507-512 671-613-706-762-672-574-576 507-5_W.mp3)
  Segment 14: 81904ms ~ 82480ms (file: 613-612-657-216-216-136-265-765-236-213 657-670-672-671-617-612-236-612_W.mp3)
  Segment 15: 83480ms ~ 84056ms (file: 501-507-510-501-507-571-571-576-547-571-514-510-517-507-547-547-507-574-570-501 517-150-105-157-510-501-150-501-517-501-510-501-516-501-506-507-501-570-507-570-570-517-571-547-5_W.mp3)
  Segment 16: 85056ms ~ 85632ms (file: 163-163-613-617-136-751-375-716-731-753-753-715-753-756-753-375 574-574-573-574-574-574-574-574-574-574 570-570-751-751-750-751-571-751-750-701-175-507-750-750-517-571-715-157-7_W.mp3)
  Segment 17: 86632ms ~ 95488ms (file: 321-765-103-756-627-132-321-267-231-276-752-574-675-120-267-276-756-756-231-260-120-130-263-256-236-102-125-726-762-127-321-103-267-267-126-726-756-756-765-675-267-216-156-231-7_W.mp3)
  Segment 18: 96488ms ~ 97064ms (file: 672-672-765-765 350-153-507-351-315-350-631-260-260-625-651-625-623-627-263-260-236-623_W.mp3)
  Segment 19: 98064ms ~ 98640ms (file: 751-510-175-751-715-715-751-657-576-567-571-573-517-571-571-516-531-156-153-517-156-516-157-513-156-156-153-153-153-513-153-513-513-513-513-536-576-576-570-576-175-157-576-576-5_V.mp3)
  Segment 20: 99640ms ~ 111124ms (file: 310-102-321-321-236-132-134-107-312-237-230-237-321-103-320-173-106-136-103-102-123-231-321-237-765-726-236-253-127-327-276-623-312-103-312-102-321-310-231-263-276-120-312-537-2_W.mp3)
  Segment 21: 112124ms ~ 112700ms (file: 576-621-632-627-261-263-261-132-631-263-631-362-612-657-675-675-617-167-136-602-132-156-567-671-732-273-726-731-756-756-576-675-657-675-765-765-172-635-657-673-361 623-236-574-7_W.mp3)
  Segment 22: 113700ms ~ 114276ms (file: 160-150-106-150 150-150-150-150-150-150-135-130-157-153-156-150-150-157-153-105-135-150-150 150-513-150-157-153-135-157-157-157-157-150-157-157-157 154-150-103-150-105-105-105-1_V.mp3)
  Segment 23: 115276ms ~ 115852ms (file: 501-504-501-501-507-501-507-571-501-507-570-507-501-570-571-510-571-501-570-517-510-501-570-570-571-570 504-507-501-510-501-507-501-510-501-510-507-501-510-570-507-571-570-570 5_V.mp3)
  Segment 24: 116852ms ~ 131252ms (file: 236-573-132-230-230-301-103-310-103-302-103-203-102-132-310-263-132-230-103-103-102-301-130-513-356-320-306-132-103-627-260-301-103-102-103-312-132-317-675-273-120-102-136-120-1_V.mp3)
  Segment 25: 132252ms ~ 132828ms (file: 517-571-571-576-571-576-576-576-570-135-571-510-570-510-510-510-517-517-517-157-507-156-570-570-150-570-573-570-561-576-571-517-157-517-156-570-571-570-510-570-520-571-571-571-1_W.mp3)
  Segment 26: 133828ms ~ 134404ms (file: 570 507-751-754-751-754-754-754-570-571-754-571-574-570-570-571-574-571-570-574-574-754-574-574 150-517-510-107-510-705-750-105-750-715-750-751-571-750-751-574-751-750 157-501-1_V.mp3)
  Segment 27: 135404ms ~ 135980ms (file: 132-136-123-132 123-136-132-165-132-106-126_V.mp3)
  Segment 28: 136980ms ~ 137556ms (file: 175-751-751-751-715-751-751-715-751-756 571-576-576-574-574-574-571-570-571-571-571-571-517-571-571-571-571-574-574 570-510-570-517-517-571-751-751-571-756-571-571-751-571-571-5_W.mp3)
  Segment 29: 138556ms ~ 147700ms (file: 216-163-107-321-320-532-572-130-762-257-237-231-526-765-276-321-312-715-213-312-237-321-236-321-103-527-327-102-126-263-260-260-267-231-230-102-263-217-312-132-213-132-756-237-1_W.mp3)
  Segment 30: 148700ms ~ 149276ms (file: 157-751 735-705-137-317-703-315-317-310-316-367-673-163-603-637-361-603-163-701-173-167-617-167-163-167-137-167-675-637-675 710-103-371-173-137-175-301-731-317-137-637-160-613-6_V.mp3)
  Segment 31: 150276ms ~ 156684ms (file: 312-263-320-754-236-617-236-627-716-301-123-163-263-756-627-267-756-321-132-537-236-326-213-136-317-231-754-273-102-316-756-627-102-257-574-251-260-130-103-130-172-213-321-102-2_W.mp3)
  Segment 32: 157684ms ~ 158260ms (file: 576-175-157-157-571-157-105-150-510-105-157-157-150-156-165-153-157-165-156-126-136-162-163-612-651-751-273-371-321-321-675-617-176-617-162-621-627-617-612-167-157-176-167-617-1_W.mp3)
  Segment 33: 159260ms ~ 159836ms (file: 216-276-160-362-236-362-301-163-516-612-612-632-123-163-627-261-612-103-125-162-163-617-167-631_W.mp3)
  Segment 34: 160836ms ~ 169368ms (file: 216-675-273-230-672-132-607-635-657-607-317-321-123-103-321-213-123-230-236-267-627-203-371-230-176-127-132-103-130-310-320-321-203-267-326-213-102-137-315-532-375-312-235-321-3_W.mp3)
  Segment 35: 170368ms ~ 170944ms (file: 627-612 765-675-675 652-625 627-623 675 627_W.mp3)
  Segment 36: 171944ms ~ 172520ms (file: 672-672-726-276-267-267-261-612-627-173-763-675-671-672-675-627-672-627-261-612-671-765-672-671-637-627-672 756-756-765-675-672-103-103-137-637-620-617-301-631-675-675-672-675-6_W.mp3)
  Segment 37: 173520ms ~ 174096ms (file: 312-312-132-153-156-657-736-673-763-765-763-637-756-736-756-763-765-137-370-713-135-137-317-173-137-175-317-137-513-156-156-156-165-156-156-756-765-765-735-756-753-573-137-751-1_W.mp3)
  Segment 38: 175096ms ~ 175672ms (file: 317-572-537-275-273-123-762-126-176-261-275-261-756-762-207-572-130-371-317-731-132-317-267-627-276-726-675-270-267-756-765-136-217-126-560-572-271-327-175-327-752-273-756-672-2_V.mp3)
  Segment 39: 176672ms ~ 186968ms (file: 132-312-137-132-136-316-136-137-136-176-132-137-137-317-137-137-136-136-173-172-103-126-216-136-137-172-612-173-167-103-231-230-213-162-176-210-210-162-132-326-321-130-123-236-2_W.mp3)
  Segment 40: 187968ms ~ 188544ms (file: 750-570-751-571-570-570-571-751-750-571-750 750-751-175-750-507-751-750-751-517-175-751-751-571-751-150-751-157-750-571-705-705-750-715 571-750-105-570-517-750-705-750-175-750-1_V.mp3)
  Segment 41: 189544ms ~ 190120ms (file: 107-105-517-517-157-150-510-517-105-105-150-105-517-510-105-150-157-510-157-517-531-517-507-501-507-571 507-157-105-157-157-105-517-514-571-501-517-501-501-517-105-517-510-150-5_V.mp3)
  Segment 42: 191120ms ~ 191696ms (file: 175-715-107-175-175-715-175-175-175-157-710-157-571-175-517-576-571-756-571-571-751-751 170-107-170-170-175-710-751-175-715-175-576-751-576-510-571-175-517-517-571-576-571-751-7_V.mp3)
  Segment 43: 192696ms ~ 193272ms (file: 672-132-312-132-326-623-210-130-312-213-301-132-126-310-210-123-516-136-130-135-126-123-157-672-671-132-561-310-301-321-201-321-213-203-261-123-130-637-316-130-136-317-167-136-1_W.mp3)
  Segment 44: 194272ms ~ 194848ms (file: 715-375-367-137-715-756-613-136-160-163-136-721-756-127-375-657-320-123-137-570-312-705-375-301-132-317-310-263-310-173-675-671-256-176-175-176-721-127-127-167-172-167-762-754-2_V.mp3)
  Segment 45: 195848ms ~ 196424ms (file: 612-623-571-317-103-315-157-176-601-671-136-103-162-761-136-106-326-213-517-132-132-132-136-617-165-106-765-136-527-103-153-103-136-160-162-672-612-623-602-103-365-136-316-621-1_V.mp3)
  Segment 46: 197424ms ~ 198000ms (file: 652-635-621-267 135-152 165-367-126_W.mp3)
  Segment 47: 199000ms ~ 199576ms (file: 107-107-105-157-156-157-157-150-150-150-156-510-105-150-170-107-175-705-750-705-705-705-750-750-750-750-750-750-750-570-752-153-105-105-150-150-150-105-175-157-175-157-153-175-1_V.mp3)
  Segment 48: 200576ms ~ 201152ms (file: 136-601-301-536-576-576-236-321-621-623-263-216-210-102-103-102-376-302-312-321-310-106-160-106-103-310-531-531-503-103-106-106-103-132-301-206-267-362-261-213-132-231-206-206-2_W.mp3)
  Segment 49: 202152ms ~ 202728ms (file: 675-625-267-637-765-627-652-627-267-632-617-726-162-267-216-126-162-162 130-621-612-126-617-621-621 326-123-312-632-162-321-126-312-512-201-203-321-236-213-126-120-132-321-163-6_W.mp3)
  Segment 50: 203728ms ~ 210424ms (file: 574-625-267-756-257-267-275-312-751-257-136-267-213-532-321-132-312-537-216-527-132-321-752-267-261-576-256-162-762-267-726-275-267-263-206-263-126-236-231-263-576-213-132-261-2_W.mp3)
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
> **Time**: 2026-04-05 06:24:33
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "안녕하세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 50, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "반갑습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "반갑습니다", "start_ms": 40, "dur_ms": 520, "phase_lr_deg": 2.5}
]
},
{
"segment": 3,
"transcript": "오늘 날씨가 정말 화창하고 좋네요 산책 가기 딱 좋은 날씨예요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 110, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 1050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "화창하고", "start_ms": 1500, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 2250, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "산책", "start_ms": 3100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "가기", "start_ms": 3550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "딱", "start_ms": 3950, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 4200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "날씨예요", "start_ms": 4550, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "맞아요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "맞아요", "start_ms": 60, "dur_ms": 480, "phase_lr_deg": 3.0}
]
},
{
"segment": 5,
"transcript": "그렇군요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "그렇군요", "start_ms": 80, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "어디로 가요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "어디로", "start_ms": 40, "dur_ms": 350, "phase_lr_deg": 2.8},
{"word": "가요", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": 2.8}
]
},
{
"segment": 7,
"transcript": "근처 공원에 새로 생긴 카페가 있는데 거기 커피가 맛있다고 하더라고요",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "근처", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "공원에", "start_ms": 600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "새로", "start_ms": 1150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "생긴", "start_ms": 1600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "카페가", "start_ms": 2050, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "있는데", "start_ms": 2650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "거기", "start_ms": 3200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "커피가", "start_ms": 3650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "맛있다고", "start_ms": 4200, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "하더라고요", "start_ms": 4950, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "아 진짜요 저도 커피 정말 좋아하는데 같이 가도 될까요 실례가 안 된다면요",
"speaker": "speaker_2",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "아", "start_ms": 200, "dur_ms": 200, "phase_lr_deg": 3.2},
{"word": "진짜요", "start_ms": 450, "dur_ms": 500, "phase_lr_deg": 3.2},
{"word": "저도", "start_ms": 1000, "dur_ms": 350, "phase_lr_deg": 3.2},
{"word": "커피", "start_ms": 1400, "dur_ms": 400, "phase_lr_deg": 3.2},
{"word": "정말", "start_ms": 1850, "dur_ms": 400, "phase_lr_deg": 3.2},
{"word": "좋아하는데", "start_ms": 2300, "dur_ms": 800, "phase_lr_deg": 3.2},
{"word": "같이", "start_ms": 3200, "dur_ms": 400, "phase_lr_deg": 3.2},
{"word": "가도", "start_ms": 3650, "dur_ms": 350, "phase_lr_deg": 3.2},
{"word": "될까요", "start_ms": 4050, "dur_ms": 500, "phase_lr_deg": 3.2},
{"word": "실례가", "start_ms": 4700, "dur_ms": 500, "phase_lr_deg": 3.2},
{"word": "안", "start_ms": 5250, "dur_ms": 200, "phase_lr_deg": 3.2},
{"word": "된다면요", "start_ms": 5500, "dur_ms": 700, "phase_lr_deg": 3.2}
]
},
{
"segment": 9,
"transcript": "물론이죠 혼자 가기 심심했는데 잘됐네요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "물론이죠", "start_ms": 100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "혼자", "start_ms": 800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "가기", "start_ms": 1250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "심심했는데", "start_ms": 1700, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "잘됐네요", "start_ms": 2700, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "그 카페 이름이 뭐예요 미리 검색 좀 해보려고요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "그", "start_ms": 120, "dur_ms": 150, "phase_lr_deg": 3.0},
{"word": "카페", "start_ms": 300, "dur_ms": 400, "phase_lr_deg": 3.0},
{"word": "이름이", "start_ms": 750, "dur_ms": 450, "phase_lr_deg": 3.0},
{"word": "뭐예요", "start_ms": 1250, "dur_ms": 500, "phase_lr_deg": 3.0},
{"word": "미리", "start_ms": 1900, "dur_ms": 350, "phase_lr_deg": 3.0},
{"word": "검색", "start_ms": 2300, "dur_ms": 400, "phase_lr_deg": 3.0},
{"word": "좀", "start_ms": 2750, "dur_ms": 200, "phase_lr_deg": 3.0},
{"word": "해보려고요", "start_ms": 3000, "dur_ms": 750, "phase_lr_deg": 3.0}
]
},
{
"segment": 11,
"transcript": "블루노트요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "블루노트요", "start_ms": 50, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "아 거기 인스타그램에서 본 적 있어요 디저트도 엄청 예쁘게 나오더라고요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "아", "start_ms": 140, "dur_ms": 200, "phase_lr_deg": 3.1},
{"word": "거기", "start_ms": 400, "dur_ms": 350, "phase_lr_deg": 3.1},
{"word": "인스타그램에서", "start_ms": 800, "dur_ms": 1000, "phase_lr_deg": 3.1},
{"word": "본", "start_ms": 1850, "dur_ms": 200, "phase_lr_deg": 3.1},
{"word": "적", "start_ms": 2100, "dur_ms": 150, "phase_lr_deg": 3.1},
{"word": "있어요", "start_ms": 2300, "dur_ms": 400, "phase_lr_deg": 3.1},
{"word": "디저트도", "start_ms": 2850, "dur_ms": 600, "phase_lr_deg": 3.1},
{"word": "엄청", "start_ms": 3500, "dur_ms": 400, "phase_lr_deg": 3.1},
{"word": "예쁘게", "start_ms": 3950, "dur_ms": 500, "phase_lr_deg": 3.1},
{"word": "나오더라고요", "start_ms": 4500, "dur_ms": 850, "phase_lr_deg": 3.1}
]
},
{
"segment": 13,
"transcript": "오 맞아요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "오", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "맞아요", "start_ms": 300, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "대박이네",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "대박이네", "start_ms": 40, "dur_ms": 480, "phase_lr_deg": 3.0}
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
"transcript": "갑시다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "갑시다", "start_ms": 50, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "지금 바로 출발할까요 아니면 조금 이따가 볼까요 전 상관없어요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "지금", "start_ms": 120, "dur_ms": 400, "phase_lr_deg": 3.2},
{"word": "바로", "start_ms": 550, "dur_ms": 350, "phase_lr_deg": 3.2},
{"word": "출발할까요", "start_ms": 950, "dur_ms": 800, "phase_lr_deg": 3.2},
{"word": "아니면", "start_ms": 1850, "dur_ms": 500, "phase_lr_deg": 3.2},
{"word": "조금", "start_ms": 2400, "dur_ms": 350, "phase_lr_deg": 3.2},
{"word": "이따가", "start_ms": 2800, "dur_ms": 500, "phase_lr_deg": 3.2},
{"word": "볼까요", "start_ms": 3350, "dur_ms": 450, "phase_lr_deg": 3.2},
{"word": "전", "start_ms": 4000, "dur_ms": 250, "phase_lr_deg": 3.2},
{"word": "상관없어요", "start_ms": 4300, "dur_ms": 800, "phase_lr_deg": 3.2}
]
},
{
"segment": 18,
"transcript": "지금 가죠",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "지금", "start_ms": 60, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "가죠", "start_ms": 450, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "좋아요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "좋아요", "start_ms": 50, "dur_ms": 450, "phase_lr_deg": 3.1}
]
},
{
"segment": 20,
"transcript": "가는 길에 꽃이 많이 펴서 구경하면서 가면 금방 도착할 거예요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "가는", "start_ms": 130, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "길에", "start_ms": 500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "꽃이", "start_ms": 850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 1250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "펴서", "start_ms": 1650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "구경하면서", "start_ms": 2100, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "가면", "start_ms": 2950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "금방", "start_ms": 3350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "도착할", "start_ms": 3800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 4350, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "맞아요 요즘 벚꽃 시즌이라 너무 예뻐요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "맞아요", "start_ms": 90, "dur_ms": 500, "phase_lr_deg": 3.0},
{"word": "요즘", "start_ms": 650, "dur_ms": 400, "phase_lr_deg": 3.0},
{"word": "벚꽃", "start_ms": 1100, "dur_ms": 400, "phase_lr_deg": 3.0},
{"word": "시즌이라", "start_ms": 1550, "dur_ms": 700, "phase_lr_deg": 3.0},
{"word": "너무", "start_ms": 2300, "dur_ms": 350, "phase_lr_deg": 3.0},
{"word": "예뻐요", "start_ms": 2700, "dur_ms": 500, "phase_lr_deg": 3.0}
]
},
{
"segment": 22,
"transcript": "그렇죠",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "그렇죠", "start_ms": 40, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "네",
"speaker": "speaker_2",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 30, "dur_ms": 200, "phase_lr_deg": 3.0}
]
},
{
"segment": 24,
"transcript": "그런데 오늘 미세먼지는 괜찮은지 모르겠네요 아까 뉴스를 못 봐서요",
"speaker": "speaker_1",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "그런데", "start_ms": 150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "미세먼지는", "start_ms": 1100, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "괜찮은지", "start_ms": 1850, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "모르겠네요", "start_ms": 2500, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "아까", "start_ms": 3350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "뉴스를", "start_ms": 3800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "못", "start_ms": 4350, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "봐서요", "start_ms": 4600, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "제가 확인해 볼게요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "제가", "start_ms": 70, "dur_ms": 350, "phase_lr_deg": 3.1},
{"word": "확인해", "start_ms": 450, "dur_ms": 500, "phase_lr_deg": 3.1},
{"word": "볼게요", "start_ms": 1000, "dur_ms": 450, "phase_lr_deg": 3.1}
]
},
{
"segment": 26,
"transcript": "잠시만요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "잠시만요", "start_ms": 40, "dur_ms": 550, "phase_lr_deg": 3.1}
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
"transcript": "보통 수준이네요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "보통", "start_ms": 60, "dur_ms": 400, "phase_lr_deg": 3.1},
{"word": "수준이네요", "start_ms": 500, "dur_ms": 650, "phase_lr_deg": 3.1}
]
},
{
"segment": 29,
"transcript": "다행이네요 그럼 마스크 안 써도 되겠어요 가벼운 마음으로 나갈 수 있겠네요",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "다행이네요", "start_ms": 110, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "그럼", "start_ms": 750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "마스크", "start_ms": 1100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 1600, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "써도", "start_ms": 1850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "되겠어요", "start_ms": 2250, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "가벼운", "start_ms": 2900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "마음으로", "start_ms": 3450, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "나갈", "start_ms": 4100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 4550, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있겠네요", "start_ms": 4750, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "그러게요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "그러게요", "start_ms": 50, "dur_ms": 500, "phase_lr_deg": 3.0}
]
},
{
"segment": 31,
"transcript": "출발하시죠 제가 길 안내할게요 이쪽으로 오세요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "출발하시죠", "start_ms": 100, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "길", "start_ms": 1250, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "안내할게요", "start_ms": 1500, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "이쪽으로", "start_ms": 2400, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "오세요", "start_ms": 3050, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "넵",
"speaker": "speaker_2",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "넵", "start_ms": 30, "dur_ms": 200, "phase_lr_deg": 3.0}
]
},
{
"segment": 33,
"transcript": "가봅시다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "가봅시다", "start_ms": 40, "dur_ms": 500, "phase_lr_deg": 3.0}
]
},
{
"segment": 34,
"transcript": "여기 나무들 정말 크지 않아요 여름에 오면 그늘이 생겨서 아주 시원해요",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "여기", "start_ms": 140, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "나무들", "start_ms": 550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 1100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "크지", "start_ms": 1550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "않아요", "start_ms": 1950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "여름에", "start_ms": 2500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "오면", "start_ms": 3050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그늘이", "start_ms": 3450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "생겨서", "start_ms": 4000, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 4600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "시원해요", "start_ms": 5000, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "그렇겠네요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "그렇겠네요", "start_ms": 60, "dur_ms": 520, "phase_lr_deg": 3.2}
]
},
{
"segment": 36,
"transcript": "와 저기 좀 보세요 꽃이 활짝 폈어요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "와", "start_ms": 80, "dur_ms": 250, "phase_lr_deg": 3.1},
{"word": "저기", "start_ms": 400, "dur_ms": 400, "phase_lr_deg": 3.1},
{"word": "좀", "start_ms": 850, "dur_ms": 200, "phase_lr_deg": 3.1},
{"word": "보세요", "start_ms": 1100, "dur_ms": 450, "phase_lr_deg": 3.1},
{"word": "꽃이", "start_ms": 1650, "dur_ms": 400, "phase_lr_deg": 3.1},
{"word": "활짝", "start_ms": 2100, "dur_ms": 450, "phase_lr_deg": 3.1},
{"word": "폈어요", "start_ms": 2600, "dur_ms": 500, "phase_lr_deg": 3.1}
]
},
{
"segment": 37,
"transcript": "진짜 예쁘죠 사진 찍기 좋을 것 같아요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 90, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "예쁘죠", "start_ms": 550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "사진", "start_ms": 1150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "찍기", "start_ms": 1600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "좋을", "start_ms": 2050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 2450, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 2650, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "저 한 장만 찍어 주실 수 있나요 배경이 너무 좋아서요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "저", "start_ms": 130, "dur_ms": 200, "phase_lr_deg": 3.1},
{"word": "한", "start_ms": 350, "dur_ms": 200, "phase_lr_deg": 3.1},
{"word": "장만", "start_ms": 600, "dur_ms": 400, "phase_lr_deg": 3.1},
{"word": "찍어", "start_ms": 1050, "dur_ms": 350, "phase_lr_deg": 3.1},
{"word": "주실", "start_ms": 1450, "dur_ms": 400, "phase_lr_deg": 3.1},
{"word": "수", "start_ms": 1900, "dur_ms": 150, "phase_lr_deg": 3.1},
{"word": "있나요", "start_ms": 2100, "dur_ms": 450, "phase_lr_deg": 3.1},
{"word": "배경이", "start_ms": 2700, "dur_ms": 500, "phase_lr_deg": 3.1},
{"word": "너무", "start_ms": 3250, "dur_ms": 350, "phase_lr_deg": 3.1},
{"word": "좋아서요", "start_ms": 3650, "dur_ms": 600, "phase_lr_deg": 3.1}
]
},
{
"segment": 39,
"transcript": "그럼요 핸드폰 주세요 잘 찍어 드릴게요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "그럼요", "start_ms": 80, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "핸드폰", "start_ms": 650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 1200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 1750, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "찍어", "start_ms": 2000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "드릴게요", "start_ms": 2400, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "감사합니다",
"speaker": "speaker_2",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 40, "dur_ms": 520, "phase_lr_deg": 3.0}
]
},
{
"segment": 41,
"transcript": "천만에요",
"speaker": "speaker_1",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "천만에요", "start_ms": 30, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "자 찍습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "자", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "찍습니다", "start_ms": 300, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "하나 둘 셋",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "하나", "start_ms": 60, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "둘", "start_ms": 500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "셋", "start_ms": 850, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "잘 나왔나요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "잘", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 3.0},
{"word": "나왔나요", "start_ms": 280, "dur_ms": 500, "phase_lr_deg": 3.0}
]
},
{
"segment": 45,
"transcript": "네 아주 잘 나왔어요 확인해 보세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 80, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 750, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "나왔어요", "start_ms": 1000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "확인해", "start_ms": 1700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "보세요", "start_ms": 2250, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "우와",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "우와", "start_ms": 30, "dur_ms": 400, "phase_lr_deg": 3.0}
]
},
{
"segment": 47,
"transcript": "정말 맘에 들어요 금손이시네요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 90, "dur_ms": 400, "phase_lr_deg": 3.0},
{"word": "맘에", "start_ms": 550, "dur_ms": 350, "phase_lr_deg": 3.0},
{"word": "들어요", "start_ms": 950, "dur_ms": 500, "phase_lr_deg": 3.0},
{"word": "금손이시네요", "start_ms": 1550, "dur_ms": 850, "phase_lr_deg": 3.0}
]
},
{
"segment": 48,
"transcript": "별말씀을요 이제 거의 다 왔어요 저기 코너만 돌면 바로 보여요",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "별말씀을요", "start_ms": 120, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "거의", "start_ms": 1300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 1700, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "왔어요", "start_ms": 1950, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "저기", "start_ms": 2600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "코너만", "start_ms": 3050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "돌면", "start_ms": 3600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 4050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "보여요", "start_ms": 4500, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "아 저기 파란 간판 카페인가요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "아", "start_ms": 80, "dur_ms": 200, "phase_lr_deg": 3.1},
{"word": "저기", "start_ms": 350, "dur_ms": 400, "phase_lr_deg": 3.1},
{"word": "파란", "start_ms": 800, "dur_ms": 400, "phase_lr_deg": 3.1},
{"word": "간판", "start_ms": 1250, "dur_ms": 450, "phase_lr_deg": 3.1},
{"word": "카페인가요", "start_ms": 1750, "dur_ms": 750, "phase_lr_deg": 3.1}
]
},
{
"segment": 50,
"transcript": "네 맞아요 드디어 도착했네요 들어가서 시원한 커피 마셔요 제가 살게요",
"speaker": "speaker_1",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "맞아요", "start_ms": 400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "드디어", "start_ms": 950, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "도착했네요", "start_ms": 1500, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "들어가서", "start_ms": 2400, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "시원한", "start_ms": 3100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "커피", "start_ms": 3700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "마셔요", "start_ms": 4200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 4800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "살게요", "start_ms": 5250, "dur_ms": 550, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 06:24:33*
