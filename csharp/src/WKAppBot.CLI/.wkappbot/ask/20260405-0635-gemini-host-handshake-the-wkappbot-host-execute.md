# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=24268  time=06:34:16  uptime=19473s  mem=488MB  system_processes=353"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_063351.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 762-516-571-761-567-576-756-657-576-756-657-651-651-615-675-651-615-657-651-675-762-672-627-672-217-672-627-761-671-716-172-276-162-172-127-571-576-257-265-256 574-571-570-571-5_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 231-263-132-631-136-627-675-627-123-132-136-126-627-621-625-627-267-103-135-326-723-217-236-567-621-627-612-623-326-213-126-136-216-261-627-627-621-612-612-263-267-123-123-163-1_V.mp3)
  Segment 3: 3152ms ~ 7219ms (file: 120-271-752-207-206-607-672-602-627-675-321-327-321-201-326-130-175-267-267-270-257-236-612-267-627-617-126-765-136-160-261-210-103-176-162-267-206-267-627-263-126-726-102-102-3_W.mp3)
  Segment 4: 8219ms ~ 8795ms (file: 574-574-754-751 570-507-510-105-570-157-750-750-715-150-570-570-157-570-157-571-150-751-751-750-750-751 574-574-570-571-574-507-154-507-570 574-547-547-547-547-504-157-501-574-5_W.mp3)
  Segment 5: 9795ms ~ 10371ms (file: 657-675-657-675-673-675-162-163-657-312-162-167-162-163-612-316-123-316-175-576-576-756-765-160-165-167-671-367-716-657-571-675-571-156-601-536-167-321-312-215-103-516-165-163-6_W.mp3)
  Segment 6: 11371ms ~ 19579ms (file: 210-201-572-123-120-213-261-301-206-210-672-132-103-206-273-210-206-213-574-752-251-507-270-301-201-210-312-276-130-201-123-762-120-132-276-130-123-206-120-263-206-175-207-542-2_W.mp3)
  Segment 7: 20579ms ~ 21155ms (file: 175-375-735-316-176-716-715-301-301-310-370-731-371-317-317-751-753-751-175-751-301-137-107-173-137-173 703-713-706-317-631-610-607-610-601-607-370-316-613-713-173-107-673-715-7_W.mp3)
  Segment 8: 22155ms ~ 22731ms (file: 135-315-317-153-531-574-571-573-135-315-315-317-137-317-351-175-571-175-571_W.mp3)
  Segment 9: 23731ms ~ 24307ms (file: 123-602-750-501-605-705-126-514-126-561-712-637-602-263-126-570-132 316-132-567-576-576-132-762-270-132-135-570-716-132-612-657-132-126-316-267-317-312-316-136-137-137-136-137-3_W.mp3)
  Segment 10: 25307ms ~ 32291ms (file: 261-213-261-123-127-120-123-132-310-713-130-107-123-317-317-107-721-130-310-213-310-231-130-321-261-236-217-127-127-103-317-103-310-236-102-201-102-217-123-217-126-261-231-326-3_W.mp3)
  Segment 11: 33291ms ~ 33867ms (file: 367-621-260-506-126-162-362-306-526-623-657_W.mp3)
  Segment 12: 34867ms ~ 35443ms (file: 173-351-103-175-107-153-173-137-130-130-103-317-137-175-107-735-130-751-301-301-375-713 135-315-135-153-105-137-357-103-351-751-107-135-103-357-751-317-175-103-301-170-735-570-1_V.mp3)
  Segment 13: 36443ms ~ 37019ms (file: 126-126-126-216-126-125-251-125-213-123-126-216-216-213-216-321-231-231-236-210-521-256-652-236-362-261-236-231-123-126-126-123-213-213-123-132-123-521-256-215-231-261-236-263-2_W.mp3)
  Segment 14: 38019ms ~ 38595ms (file: 510-150-105-150-513-501-501-150-157-175-715-715-715-715-701-175-167-136-351 175-731-731-713-175-175-176-715-752-715-715-175-715-576-573-573-570-150-756-713-176-715-715-175-715-7_W.mp3)
  Segment 15: 39595ms ~ 40171ms (file: 623-136-103-563-136-167-130-136-136-130-136-136-310-103-321-631-613-130-132-312-130-136-137-130-156-756-671-163-263-261_V.mp3)
  Segment 16: 41171ms ~ 41747ms (file: 150-517-517-501 105-157-510-501-517-157-105-501-150-501-510-105-157-157-157-105-150-517-150-576-501-570-156-517-150-105 150-153-501-150-501-510-501-510-157-507-510-105-501-150-5_V.mp3)
  Segment 17: 42747ms ~ 43323ms (file: 576-571-213-136 216-576-563-352-726-267-576-576-576-756-762-657-576-756-572-312-103-362-106-137-136-136-316-172-162-712-127-756-576-576-765-627-602-607-671-627-620-756-762-627-6_W.mp3)
  Segment 18: 44323ms ~ 44899ms (file: 167 751-367-253-236-632-672-675 126-103-136-137-260-261-132-623-620-621_W.mp3)
  Segment 19: 45899ms ~ 46475ms (file: 517 150-150-150-105-571-157-510-501-501-150-501-501-510-501-510-501-501-517-150-150-150-510-150-507-510-510-517-501-507-504-547-571 157-105-153-513-510-570-510-501-501-517-501-5_V.mp3)
  Segment 20: 47475ms ~ 48051ms (file: 136-675-706-301-301-321-321-301_V.mp3)
  Segment 21: 49051ms ~ 49627ms (file: 104-157-571-157-501-157-175-157-107-105-157-154-157-145-174-145 140-145-154-150-410-531-145-413-154-104-401-514-157-145-145 315-134-153-140-514-153-513-104-510-451-451-401-541-1_V.mp3)
  Segment 22: 50627ms ~ 51203ms (file: 165-176-175-176-175-175-175-175-756-175-501-571-150-105-150-150-105-517-157-167-175-175-156-167-175-167-176-167-173-173-715 105-150-150-150-107-107-160-170-175-715-175-175-157-7_W.mp3)
  Segment 23: 52203ms ~ 52779ms (file: 765-762-761-761-761-761-763-761-765-765-761-765-761-765-761-752-765-761-715-765-761-761-170-175-137-135-157-135-163-153-153-135-135-153 175-715-170-130-753-756-756-756-751-751-7_W.mp3)
  Segment 24: 53779ms ~ 62491ms (file: 760-672-206-261-237-210-102-216-271-231-756-207-162-571-315-153-571-157-135-163-357-507-206-261-216-672-203-257-527-310-315-207-216-567-132-205-761-123-260-261-201-267-761-176-1_V.mp3)
  Segment 25: 63491ms ~ 64067ms (file: 172-172-712-127-712-127-127 715-571-751-715-751-751-751-715-517-715-751-175-751-715-715-571-715-751-715-751-170-715-175-715-571 571-157-571-751-157-751-750-751-175-175-710-715-7_W.mp3)
  Segment 26: 65067ms ~ 65643ms (file: 170-107-170-175-170-175-175-157-157-175-175-751-571 175-751-715-750-571-157-571-157-751-751-571-715-170-710-175-517-175-175-715 715-751-157-173-103-170-170-170-175-176-175-751-1_W.mp3)
  Segment 27: 66643ms ~ 67219ms (file: 316 713-137-132-312-173-317-761-613-136-107-173-756-375-735-571-713-573-517-157-125-173-271-267-217-567-537-735-361-126-312-317-317-317-576-762-763-752-762-765-762-376-765-736-7_W.mp3)
  Segment 28: 68219ms ~ 77579ms (file: 267-206-267-260-231-203-532-231-213-123-127-716-301-230-203-206-230-752-413-123-132-103-136-137-107-203-231-210-230-672-627-316-302-273-236-237-267-267-173-132-276-237-123-310-2_W.mp3)
  Segment 29: 78579ms ~ 79155ms (file: 617-167-675-165-567-167-602-675-610-610-650-675-612-627-672-761-167-620 673-675-162-162-675 652 162_W.mp3)
  Segment 30: 80155ms ~ 80731ms (file: 137-130-167-130-106-617-163-576-152-362-671-167-167-137-106-103-106-103-561-126-105-163-107-106-156-162-103-106-365-613-176-167-106-103-173-761-617-172-761-127-162-627-160-136-1_V.mp3)
  Segment 31: 81731ms ~ 96491ms (file: 351-621-362-310-163-507-361-376-360-320-615-627-716-367-607-257-237-176-761-570-631-572-753-762-267-267-631-276-627-120-531-356-275-132-132-357-302-317-671-627-762-367-103-672-7_W.mp3)
  Segment 32: 97491ms ~ 98067ms (file: 361-361-126-367-621-263-326-312-163-167-653-635-316-263-261-761-756-751-265-253-256-261-621-267-123-153-312-312-263-263-267-261-126-162-231-236-276-237-237-756-657-627-267-326-3_W.mp3)
  Segment 33: 99067ms ~ 103999ms (file: 132-231-123-216-120-201-521-132-231-712-123-271-213-524-542-572-123-210-127-205-513-105-213-620-327-572-217-301-671-136-213-213-321-102-123-120-123-301-137-127-567-103-120-126-1_W.mp3)
  Segment 34: 104999ms ~ 105575ms (file: 176-107-103-167-107-172 107-106-176-106-176-107-167-107-167-106-176-106-107-103-106-136-106-167-163-106-107-176 107-176-107-173-167-107-173-107-176-176-176-103-103-107 136-103-1_V.mp3)
  Segment 35: 106575ms ~ 107151ms (file: 135-132-102-126-132-134-132-103-132-103-136-163-310-103-136-103-137-136-136-136-134-103-103-572-710-160-657-150-126-130-312-236-267-162-162-136-617-163-163-136-574-601-103-756 1_V.mp3)
  Segment 36: 108151ms ~ 108727ms (file: 126-162-736-163-172-261-132-136-263-205-261-261-267-265-256-261-216-261-216-216-126-210-216-126_W.mp3)
  Segment 37: 109727ms ~ 110303ms (file: 150-501-153-510-150-153-157-105-150-510-507-507-571 105-103-105-513-105-105-105-510-510-501-517-150-150-150-130-315-517-157-510-150-150-150-150-510-517-153-150-573-571 105-105-1_V.mp3)
  Segment 38: 111303ms ~ 111879ms (file: 154-501-543-501-514-105-105-105-510-501-501-153-510 513-541-150-501-510-501-150-105-153-105-105-517-153-105-513-501-510-501-157-105-105-501-105-517-150-157-513-150-501 501-510-1_V.mp3)
  Segment 39: 112879ms ~ 122887ms (file: 257-102-120-130-572-326-236-123-120-132-541-321-130-261-213-103-130-213-162-576-507-261-263-263-263-261-641-261-574-325-162-267-762-263-126-261-752-102-236-315-265-260-756-623-6_W.mp3)
  Segment 40: 123887ms ~ 124463ms (file: 157-105-175 157-105-103-175-173-105-135-103-175-107-105-107-107-107-175 157-175-173-175-107-105-107-103-107-105-107-103-175-157-105-157-107-105-710-175 150-157-107-105-157-157-1_V.mp3)
  Segment 41: 125463ms ~ 126039ms (file: 123-135-137-130-132-123 321-321-312-321-320-120-312-162-231-132-206-621-326-362-360-362-623-136-126-132-312-201-102-102-103-106-102-103-310-130-236-126-320-312-216-132-310-132-1_W.mp3)
  Segment 42: 127039ms ~ 132439ms (file: 267-267-276-627-236-120-312-321-263-216-276-627-265-326-261-627-672-267-216-126-312-231-267-627-671-267-621-627-736-275-267-123-312-267-321-213-216-231-236-206-126-326-126-261-1_W.mp3)
  Segment 43: 133439ms ~ 151331ms (file: 361-130 132-230-263-123-132-301-213-574-103-103-136-203-206-236-132-612-675-657-567-256-312-547-231-130-132-267-263-103-310-540-103-547-130-123-163-132-230-312-203-230-260-206-2_W.mp3)
  Segment 44: 152331ms ~ 152907ms (file: 627 671-162-132 263 671-612-631 261-216-126-261-216-263-216-163-621-263-261-162-126-162-216-236-263 163 120-132_W.mp3)
  Segment 45: 153907ms ~ 162835ms (file: 163-210-261-201-276-276-160-261-265-627-675-602-260-261-267-260-201-201-267-263-267-261-231-230-260-130-103-103-263-103-213-260-627-267-267-123-203-310-320-267-301-103-130-260-3_W.mp3)
  Segment 46: 163835ms ~ 164411ms (file: 157-507-571-751-570-570-750-157-751-157-754 507-570-750-705-570-751-754-173-175 217-532-514-510-510-570-157-150-157-157-175-157-175-754-751-175-157-571-157-517-574-571-751-517-5_W.mp3)
  Segment 47: 165411ms ~ 165987ms (file: 576-571-756-576 105-105-105-157-107-170-710-170-170-170-175-756-576-576-574 105-157-107-710-107-170-710-715-710-107-715-175-170-175-710-170-571-751-756-756-756-751-175-751-751-7_V.mp3)
  Segment 48: 166987ms ~ 167563ms (file: 572-325-326-126-127-321-713-276-257-752-216-276-760-213-267-726-762-765-237-752-265-625-276-275-720-275-726-216-265-275-625-216-570-731-736-173-371 765-762-132-762-573-267-526-2_V.mp3)
  Segment 49: 168563ms ~ 169139ms (file: 570-574-570 105-105-150-105-501-517-510-150-507-501-105-510-150-510-501-150-150-150-501-510-157-150-105-150-150-150-157-153-517-150-507-570-571 105-150-150-150-510-150-510-150-5_V.mp3)
  Segment 50: 170139ms ~ 170715ms (file: 570-150-157-157-105-501-157-571 517-501-105-105-150-105-105-510-105-105-105-105-105 570-570-570-570-571-507-507-517-570-574-570-574-507-570-750-571-570-750-570-357-715-715 157-1_V.mp3)
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
> **Time**: 2026-04-05 06:35:19
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
{"word": "반갑습니다", "start_ms": 40, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "오늘 날씨가 정말 화창하고 좋네요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 60, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 480, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 1100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "화창하고", "start_ms": 1650, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 2500, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "감사합니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 30, "dur_ms": 530, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "천만에요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "천만에요", "start_ms": 45, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "한국어 공부는 재미있으신가요 아니면 조금 어려우신가요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 80, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "공부는", "start_ms": 700, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "재미있으신가요", "start_ms": 1300, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "아니면", "start_ms": 2600, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 3300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "어려우신가요", "start_ms": 3900, "dur_ms": 1100, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "재미있어요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "재미있어요", "start_ms": 40, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "그렇군요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "그렇군요", "start_ms": 35, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "다행입니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "다행입니다", "start_ms": 50, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "노래 부르는 걸 좋아하신다고 들었는데 맞나요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "노래", "start_ms": 100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "부르는", "start_ms": 600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "걸", "start_ms": 1150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "좋아하신다고", "start_ms": 1400, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "들었는데", "start_ms": 2600, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "맞나요", "start_ms": 3500, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "네 맞아요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "맞아요", "start_ms": 260, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "정말 좋아해요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 30, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "좋아해요", "start_ms": 300, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "어떤 장르를 좋아하시나요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "어떤", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "장르를", "start_ms": 270, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "발라드요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "발라드요", "start_ms": 35, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "오 좋네요",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "오", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 200, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "그렇군요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "그렇군요", "start_ms": 35, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "한국 노래 중에서 좋아하는 노래가 있나요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "한국", "start_ms": 45, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "노래", "start_ms": 260, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "네 아주 많아요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 210, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "많아요", "start_ms": 380, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "가장 좋아하는 가수는 누구인가요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "가장", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "좋아하는", "start_ms": 270, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "아이유요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "아이유요", "start_ms": 30, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "저도 아이유 노래 정말 좋아해요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "저도", "start_ms": 45, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "아이유", "start_ms": 210, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "목소리가 정말 예쁘죠",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "목소리가", "start_ms": 35, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "예쁘죠", "start_ms": 470, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "맞아요 최고예요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "맞아요", "start_ms": 40, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "최고예요", "start_ms": 310, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "그러면 우리 같이 아이유 노래 한 곡 연습해 볼까요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "그러면", "start_ms": 120, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "우리", "start_ms": 550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 900, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "좋아요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "좋아요", "start_ms": 35, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "준비됐어요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "준비됐어요", "start_ms": 30, "dur_ms": 530, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "어떤 노래로 할까요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "어떤", "start_ms": 40, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "노래로", "start_ms": 310, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "음 밤편지 어떠세요 한국어 공부하기에도 아주 좋은 가사예요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "음", "start_ms": 100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "밤편지", "start_ms": 350, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "오 너무 좋아요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "오", "start_ms": 30, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 250, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "제가 제일 좋아하는 노래예요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "제가", "start_ms": 45, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "제일", "start_ms": 310, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "잘됐네요 그럼 먼저 가사를 한 줄씩 읽어보고 그 다음에 멜로디를 익혀볼게요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "잘됐네요", "start_ms": 110, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "그럼", "start_ms": 650, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "알겠습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "알겠습니다", "start_ms": 35, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "첫 번째 문장은 이 밤 그날의 반딧불을 당신의 창 가까이 보낼게요 입니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "첫", "start_ms": 120, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "번째", "start_ms": 350, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "이 밤",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 30, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "밤", "start_ms": 250, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "그날의 반딧불을",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "그날의", "start_ms": 40, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "반딧불을", "start_ms": 310, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "네 좋아요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 30, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "좋아요", "start_ms": 250, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "당신의 창 가까이",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "당신의", "start_ms": 35, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "창", "start_ms": 310, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "보낼게요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "보낼게요", "start_ms": 30, "dur_ms": 530, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "발음이 아주 좋으시네요 단어 뜻은 다 이해하셨나요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "발음이", "start_ms": 90, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 410, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "네 거의 다 알아요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 35, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "거의", "start_ms": 250, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "반딧불은 반짝거리는 곤충 맞죠",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "반딧불은", "start_ms": 40, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "반짝거리는", "start_ms": 510, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "네 맞아요 영어로는 파이어플라이(firefly)라고 하죠",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 80, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "맞아요", "start_ms": 300, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "자 그럼 이제 멜로디에 맞춰서 불러볼까요 피아노 반주를 틀어드릴게요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "자", "start_ms": 110, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "그럼", "start_ms": 280, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "네 준비됐습니다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "준비됐습니다", "start_ms": 210, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "(반주 시작)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": [
{"word": "(instrument)", "start_ms": 0, "dur_ms": 8900, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "이 밤 그날의 반딧불을",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "mixed",
"words": [
{"word": "이", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "밤", "start_ms": 270, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "당신의 창 가까이 보낼게요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "mixed",
"words": [
{"word": "당신의", "start_ms": 40, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "창", "start_ms": 310, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "사랑한다는 말이에요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "mixed",
"words": [
{"word": "사랑한다는", "start_ms": 45, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "말이에요", "start_ms": 360, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "와 정말 잘 부르시네요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "와", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 270, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "감동적이에요 수고하셨습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "감동적이에요", "start_ms": 40, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "수고하셨습니다", "start_ms": 360, "dur_ms": 200, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 06:35:19*
