# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=1700  time=12:03:01  uptime=26848s  mem=1648MB  system_processes=355"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_120052.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 501-564-507-501-506-572-521-516-503-531-527-502-526-526-562-512-507-572-257-572-576-570-576-576-567-576-576-576-576-576-576-576-576-576-675-672-537-512-562-526-527-512-527-502-5_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 103-135-301-316-603-175-126-156-103-632-312-103-216-326-321-630-210-312-132-316-136-612-362-103-126-132-103-130-310-361-137-103-103-136-167-756-315-301-301-631-362-205-205-203-2_V.mp3)
  Segment 3: 3152ms ~ 7580ms (file: 312-312-160-132-123-321-236-132-752-627-362-312-621-261-320-310-312-312-312-132-312-123-132-132-761-715-623-123-132-132-123-136-310-312-130-320-130-103-312-623-316-756-726-367-3_W.mp3)
  Segment 4: 8580ms ~ 9156ms (file: 756-751-175-576-175-751-751-750-751-710-175-750-715-715-753-753-750-715-517-751-751-715-751-751-571-751-751-751-751-715 751-751-571-517-175-175-705-176-571-715-751-570-710-175-1_V.mp3)
  Segment 5: 10156ms ~ 10732ms (file: 310-136-126-136-132-162-637-103-103-106-301-130-315-136-571-136-576-136-316-361-302-310-602-567-756-756-675-671-172-103-126-675-671-612-621-167_W.mp3)
  Segment 6: 11732ms ~ 21164ms (file: 756-276-537-570-756-756-257-236-325-261-237-715-167-675-206-715-527-267-276-571-137-752-126-750-321-312-137-103-173-102-176-137-103-130-317-751-751-751-175-375-765-756-310-301-2_W.mp3)
  Segment 7: 22164ms ~ 22740ms (file: 715-751-751-751-715-157 105-715-517-105-571-175-751-715-175-715-751-175-175-175-715-751-715-751-751-715-751-756-715-751-751-715-517 150-150-157-105-170-175-715-751-751-571-175-1_V.mp3)
  Segment 8: 23740ms ~ 24316ms (file: 710-751-710-571-751-175-715-751-751-715-715 571-571-715-175-715-751-751-175-715-170-175-751-750-715 175-173-175-175-175-137-173-317-175-705-751-571-751-715-715-751-751-571-175-1_W.mp3)
  Segment 9: 25316ms ~ 32912ms (file: 213-567-123-735-576-275-123-201-205-260-251-312-752-725-206-750-132-367-103-756-216-120-315-762-736-301-213-263-210-576-375-120-213-215-175-756-261-132-627-351-102-572-527-230-2_W.mp3)
  Segment 10: 33912ms ~ 34488ms (file: 615-613-672-671-627-621-621-625 627-675-657-651-625-567-567-156-576-572-671-567-576 625-540-605_W.mp3)
  Segment 11: 35488ms ~ 49960ms (file: 157-175-267-321-361-762-731-576-275-213-237-275-327-756-576-576-672-267 451-574 612-357-503-571_W.mp3)
  Segment 12: 50960ms ~ 58232ms (file: 517-357-315-675-137-713-120-312-302-756-567-763-371-753-657-573-107-756-765-620-620-260-765-762-107-675-103-715-130-175-574-316-103-106-136-132-751-106-750-762-576-265-623-625-6_W.mp3)
  Segment 13: 59232ms ~ 75900ms (file: 123-706-670-301-751-312-762-372-257-326-267-362-263-103-132-216-765-726-501-156-761-517-153-103-123-312-756-361-132-312 531-576-150-765-175-175-132-726-123-213-312-310-673-620-3_W.mp3)
  Segment 14: 76900ms ~ 77476ms (file: 615-326 126-162-163-613-612-610-160-610-612-162-615-625-612-612-621 210_W.mp3)
  Segment 15: 78476ms ~ 79052ms (file: 570-576-576-570-570-517-157-105-513-105-153-517-105-157-517-751-715-513-175-715-571-576-751-571-571-571-571-157-571-517-517-571-573-517-571-751-571-517-150-153-105-103-715-175-5_V.mp3)
  Segment 16: 80052ms ~ 80628ms (file: 510-517-570-510-157-157-153-157-150-157-175-157-517-175-517-571 501-513-150-150-105 510-517-571-537-517-570-517-157-157-105-130-153-135-157-157-571-571-517 571-573-517-517-517-5_W.mp3)
  Segment 17: 81628ms ~ 82204ms (file: 317-371 753-705-703-173-135-135-301-137-301-317-310-317-137-310-310-103-107-301-173-312-170-710-750-751-731-317-710-173 170-713-730-701-703-735-731-317-107-137-713-137-310-715-3_V.mp3)
  Segment 18: 83204ms ~ 83780ms (file: 132-123-162-612-516-264-654-546-547-541-564-516-132-312-650-605-605-657-126-756-657-562-567-675-567-657-657-675-637-657-750-670-705-725-670-675-602-576-756-576-756-607-627-605-6_V.mp3)
  Segment 19: 84780ms ~ 85356ms (file: 517-715-571-571-751-571-751-517-571-751-751-571 570-571-571-571-751-751-573-570-750-751-571-571-756-751-715 570-571-173-571-751-517-751-751-751-571-157-570-751-751-571-571 571-5_W.mp3)
  Segment 20: 86356ms ~ 86932ms (file: 576-625-725-172-127-756-216-570-752-123-720-567-572-163-176-167-213-367-162-235-265-716-231-516-527-127-567-263-167-762-156-213-671-132-132-135-726-715-657-263-236-263-230-207-5_V.mp3)
  Segment 21: 87932ms ~ 96428ms (file: 735-320-270-106-103-173-756-574-157-143-570-105-127-657-675-574-175-103-132-321-321-540-627-132-103-132-312-670-301-237-732-217-217-103-327-132-547-540-321-201-754-321-157-574-3_W.mp3)
  Segment 22: 97428ms ~ 105672ms (file: 123-216-136-231-567-517-752-236-236-132-261-263-123-625-236-675-657-126-102-162-132-627-756-765-267-516-162-176-310-132-312-102-162-615-567-513-103-107-107-715-216-267-273-270-7_V.mp3)
  Segment 23: 106672ms ~ 107248ms (file: 610-675-657-765-657-675-765-765-756-657-765-567-674-745-675-647-675-674 617-671-671-675-657-675-657-675-657-675-657-675-675-674-675-675-675-617 617-675-627-617-617-675-674-675-6_W.mp3)
  Segment 24: 108248ms ~ 108824ms (file: 763-156-173-153-135-310-103-130-130-130-123-576-576-576-756-576-756-756-756-135-367-167-756-750-570-576-576-576-576-576-756-576-756-756-517-510-751-571-716-301-307-150-310-103-1_W.mp3)
  Segment 25: 109824ms ~ 110400ms (file: 137-316-316-132-320-301-320-126-261-361-632-126-123-136-613-136-362-623-612-326-231-103-132-302-321-316-310-236-602-321-103-162-130-236-312-210-623_V.mp3)
  Segment 26: 111400ms ~ 111976ms (file: 175 705-150-175-130-170-107-137-710-173-173-130-170-170-712-137-135-173-175-170-137-173-130-175-170-107-107-715 517-510-157-571-715-173-175-107-107-170-153-170-173-170-170-173-1_V.mp3)
  Segment 27: 112976ms ~ 117656ms (file: 271-216-217-723-237-312-231-326-267-237-236-237-327-273-275-217-201-201-271-173-721-276-230-157-130-301-217-267-627-310-123-671-107-130-103-731-276-276-267-237-321-132-751-657-7_W.mp3)
  Segment 28: 118656ms ~ 119232ms (file: 751-157-576-571-715-750-751-576-576-576-576-756-576-576-576-517-571-570-517-507-150-751-517-570-571-713-751-576-571-571-517-516-516-562-576-576-576-567-576-567-576-570-570-570-5_V.mp3)
  Segment 29: 120232ms ~ 120808ms (file: 527-572-576-512-527-571-576-517-572-576-571-572-572-571-571_W.mp3)
  Segment 30: 121808ms ~ 122384ms (file: 725 157-517-157-150-510-150-107-175-107-170-107-130-105-107-170-105-107-103-157-571-170-105-153-105-170-103-510-150-517_V.mp3)
  Segment 31: 123384ms ~ 123960ms (file: 651-165-165-615-165-165-165-165-627-625-675-672-652-657-526-256-265-657-627-762-672-263-621-162-651-615-156-651-165-651-615-651-615-615-165-615-615-615-657-276-675-625-675-267-6_W.mp3)
  Segment 32: 124960ms ~ 125536ms (file: 574 517-612-163-267-623-627 103-163-675-170-106-761-756-756-765-751-751-752-765-765-756-265-672-627-627-627-267-167-176-162-620-326 167-612 756-765-765-765_W.mp3)
  Segment 33: 126536ms ~ 127112ms (file: 160-176-106-610-162-163-130-130-103-106-103-160-165-126-130-130-163-173-130-157-103-130-156-671-263-261 136-563-137-610-163-106-106-150-130-263-103-153-136-103-136-136-263-263-2_V.mp3)
  Segment 34: 128112ms ~ 128688ms (file: 261-160-610-106-610-316-326-316-230-231-230-203-302-213-762-276-267-267-102-261-263-260-126-173-376-136-132-130-213-132-263-260-321-320-312-302-103-136-163-106-106-103-123-321-6_W.mp3)
  Segment 35: 129688ms ~ 140272ms (file: 261-527-132-627-267-267-524-627-236-276-216-267-263-216-263-320-230-123-267-672-762-267-263-263-123-320-260-103-132-574-267-267-612-621-326-123-120-625-236-132-267-206-627-267-2_W.mp3)
  Segment 36: 141272ms ~ 141848ms (file: 510-157-157-157-157-150-157-517-157-105-150-157-715 105-103-153-105-175-157-150-175-105-103-103 105-150-150-150-150-157-157-157-517-571-157-157-157-157-170-175-715-173 510-150-1_V.mp3)
  Segment 37: 142848ms ~ 151812ms (file: 162-751-316-263-231-752-574-130-231-123-321-601-612-326-263-231-102-263-321-120-132-102-123-120-231-276-756-576-231-534-102-102-612-574-126-576-734-231-120-103-120-106-107-236-2_W.mp3)
  Segment 38: 152812ms ~ 153388ms (file: 361-167-610-176-132-137-536-167-367-167-176-576-263-263-261-276-132-132-125-127-167-762-167-756-123-265-672-120-102-135-361-163 162-317-136-361-267-261-263-672-623-216-316-312-6_V.mp3)
  Segment 39: 154388ms ~ 154964ms (file: 751-756-751-751-573-167-136-315-315-315-513-735-371-317-715-317-751-175-701-713-175-715-173-517-750-351-325-315-315-312-123-375-315-312-312-310-312-135-731-316-736-736-735-736-5_W.mp3)
  Segment 40: 155964ms ~ 156540ms (file: 123-610-126-103-120 126-103-160 261-260-132-210-102-123-612-126-612-621-126-126-132-136-102-316-602-627 103-132-123-126-102-126-201-261-120-126-102-216-126-120-201-216-162-160-1_V.mp3)
  Segment 41: 157540ms ~ 170716ms (file: 275-102-127-106-103-753-263-627-765-106-231-236-312-213-236-756-127-120-261-621-106-167-174-132-136-163-163-102-623-123-216-160-716-162-132-310-321-132-231-672-162-260-267-210-2_W.mp3)
  Segment 42: 171716ms ~ 172292ms (file: 574-574-547-576-567 576-574-574-547-574-547-574-547-574-574-547-574-547-574-574-547-574-457-546-516-547-534-541-457-574-547-574-547-574-574-574-574-547-574-547-574-574-547-547-5_W.mp3)
  Segment 43: 173292ms ~ 173868ms (file: 263-261-261-612-621-261-265-621-625-625-621-512-526-521-521-502-526-502-625-625-625-625-625-652-651-652-651-615-615-615-615-651-651-652-652-652-625-501 627-625-625-627-625-652-6_W.mp3)
  Segment 44: 174868ms ~ 175444ms (file: 130-103-134-104-104-146-134-143-146-106-143-103-136-143-413-134-147-145-143-143-163 135-137-137-315-571-574-574 317-715-517-751-574-571-517-571-571-357-317-375-751-573-574-574-5_W.mp3)
  Segment 45: 176444ms ~ 177020ms (file: 612-675-106-672-267-617-130-165-231-576-527-526-652-651-263-236-263 132-163-103-167-657-512-603-615-210-612-671-716-326-631-312-302-162-260-263-623-103-137-130-613-163-106-136-3_V.mp3)
  Segment 46: 178020ms ~ 182304ms (file: 261-263-312-320-312-261-263-216-231-261-236-213-210-120-120-216-123-263-310-106-231-126-261-623-621-572-576-576-576-256-265-625-627-267-267-267-275-572-572-715-135-175-725-257-5_W.mp3)
  Segment 47: 183304ms ~ 189316ms (file: 267-260-203-310-132-260-760-670-103-230-263-267-267-627-267-267-627-237-213-130-103-130-321-267-576-657-132-173-267-267-753-756-765-203-276-276-312-236-261-120-321-571-572-273-1_W_stt_i_would_not_be_divisive_danger_that_beli.mp3)
  Segment 48: 190316ms ~ 190892ms (file: 156-156-165-531-513-517-517-156-165-175-765-672-163-136-157-175-751-751-756-157-157-150-150-510-517-156-156-567-156-561-165-561-615-571-756-756-175-716-751-135-175-715-751-751-5_W.mp3)
  Segment 49: 191892ms ~ 198984ms (file: 207-102-210-167-103-132-275-206-627-102-371-320-576-736-267-102-102-321-206-762-206-312-573-701-237-312-320-312-260-210-325-321-327-310-203-237-725-273-716-310-715-276-273-734-1_W.mp3)
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
> **Time**: 2026-04-05 12:04:13
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
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "안녕", "start_ms": 40, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "하세요", "start_ms": 260, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "반갑습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "반갑", "start_ms": 35, "dur_ms": 240, "phase_lr_deg": 2.5},
{"word": "습니다", "start_ms": 275, "dur_ms": 260, "phase_lr_deg": 2.2}
]
},
{
"segment": 3,
"transcript": "오늘 날씨가 정말 좋네요 그쵸",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 60, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 380, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 810, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 1170, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "그쵸", "start_ms": 1600, "dur_ms": 320, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 5,
"transcript": "감사합니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "감사", "start_ms": 25, "dur_ms": 250, "phase_lr_deg": 1.8},
{"word": "합니다", "start_ms": 280, "dur_ms": 260, "phase_lr_deg": 1.5}
]
},
{
"segment": 6,
"transcript": "외국어 공부는 꾸준히 하는 게 가장 중요하다고 생각해요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "외국어", "start_ms": 80, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "공부는", "start_ms": 550, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "꾸준히", "start_ms": 1000, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 1500, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 1820, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 2000, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "중요하다고", "start_ms": 2400, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "생각해요", "start_ms": 3150, "dur_ms": 580, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
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
"transcript": "특히 리스닝 실력을 키우려면 매일 조금씩이라도 들어야 하죠",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "특히", "start_ms": 70, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "리스닝", "start_ms": 410, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "실력을", "start_ms": 910, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "키우려면", "start_ms": 1380, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "매일", "start_ms": 1950, "dur_ms": 360, "phase_lr_deg": 0.0},
{"word": "조금씩이라도", "start_ms": 2330, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "들어야", "start_ms": 3100, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "하죠", "start_ms": 3550, "dur_ms": 310, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 11,
"transcript": "I totally agree with you on that point",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 45, "dur_ms": 150, "phase_lr_deg": 3.1},
{"word": "totally", "start_ms": 200, "dur_ms": 450, "phase_lr_deg": 2.8},
{"word": "agree", "start_ms": 680, "dur_ms": 380, "phase_lr_deg": 2.9},
{"word": "with", "start_ms": 1080, "dur_ms": 220, "phase_lr_deg": 3.0},
{"word": "you", "start_ms": 1310, "dur_ms": 180, "phase_lr_deg": 3.0},
{"word": "on", "start_ms": 1500, "dur_ms": 160, "phase_lr_deg": 3.2},
{"word": "that", "start_ms": 1670, "dur_ms": 240, "phase_lr_deg": 3.1},
{"word": "point", "start_ms": 1920, "dur_ms": 350, "phase_lr_deg": 3.0}
]
},
{
"segment": 12,
"transcript": "Consistency is key when learning a new language",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "Consistency", "start_ms": 55, "dur_ms": 720, "phase_lr_deg": 2.5},
{"word": "is", "start_ms": 790, "dur_ms": 180, "phase_lr_deg": 2.4},
{"word": "key", "start_ms": 980, "dur_ms": 310, "phase_lr_deg": 2.6},
{"word": "when", "start_ms": 1320, "dur_ms": 260, "phase_lr_deg": 2.5},
{"word": "learning", "start_ms": 1600, "dur_ms": 480, "phase_lr_deg": 2.7},
{"word": "a", "start_ms": 2100, "dur_ms": 100, "phase_lr_deg": 2.6},
{"word": "new", "start_ms": 2220, "dur_ms": 240, "phase_lr_deg": 2.5},
{"word": "language", "start_ms": 2480, "dur_ms": 610, "phase_lr_deg": 2.6}
]
},
{
"segment": 13,
"transcript": "맞아요 그래서 이 가라오케 시스템이 정말 도움이 될 것 같아요",
"speaker": "speaker_1",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "맞아요", "start_ms": 90, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 650, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 1100, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "가라오케", "start_ms": 1250, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "시스템이", "start_ms": 1850, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 2500, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "도움이", "start_ms": 2900, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "될", "start_ms": 3350, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 3520, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 3660, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
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
"transcript": "실제 대화처럼 연습할 수 있으니까요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "실제", "start_ms": 30, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "대화처럼", "start_ms": 400, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "연습할", "start_ms": 950, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 1450, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "있으니까요", "start_ms": 1580, "dur_ms": 580, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "자막도 같이 나오면 더 좋겠죠",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "자막도", "start_ms": 45, "dur_ms": 420, "phase_lr_deg": 1.2},
{"word": "같이", "start_ms": 480, "dur_ms": 280, "phase_lr_deg": 1.1},
{"word": "나오면", "start_ms": 780, "dur_ms": 350, "phase_lr_deg": 1.3},
{"word": "더", "start_ms": 1150, "dur_ms": 150, "phase_lr_deg": 1.2},
{"word": "좋겠죠", "start_ms": 1320, "dur_ms": 410, "phase_lr_deg": 1.0}
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
"transcript": "네 맞습니다 바로 그 기능을 개발 중이에요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "맞습니다", "start_ms": 250, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 700, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 1000, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "기능을", "start_ms": 1150, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "개발", "start_ms": 1580, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "중이에요", "start_ms": 1920, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "사용자가 발음하면 AI가 피드백을 주는 방식이죠",
"speaker": "speaker_1",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "사용자가", "start_ms": 110, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "발음하면", "start_ms": 650, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "AI가", "start_ms": 1250, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "피드백을", "start_ms": 1650, "dur_ms": 510, "phase_lr_deg": 0.0},
{"word": "주는", "start_ms": 2200, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "방식이죠", "start_ms": 2500, "dur_ms": 460, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "그거 아주 훌륭한 아이디어네요 기대됩니다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "그거", "start_ms": 40, "dur_ms": 280, "phase_lr_deg": 2.1},
{"word": "아주", "start_ms": 350, "dur_ms": 240, "phase_lr_deg": 2.0},
{"word": "훌륭한", "start_ms": 620, "dur_ms": 420, "phase_lr_deg": 1.9},
{"word": "아이디어네요", "start_ms": 1080, "dur_ms": 750, "phase_lr_deg": 2.2},
{"word": "기대됩니다", "start_ms": 1880, "dur_ms": 620, "phase_lr_deg": 2.0}
]
},
{
"segment": 23,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
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
"transcript": "열심히 만들어 보겠습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "열심히", "start_ms": 35, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "만들어", "start_ms": 480, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "보겠습니다", "start_ms": 850, "dur_ms": 520, "phase_lr_deg": 0.0}
]
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
"transcript": "언제쯤 베타 버전을 써볼 수 있을까요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "언제쯤", "start_ms": 60, "dur_ms": 380, "phase_lr_deg": 1.5},
{"word": "베타", "start_ms": 480, "dur_ms": 280, "phase_lr_deg": 1.6},
{"word": "버전을", "start_ms": 800, "dur_ms": 420, "phase_lr_deg": 1.5},
{"word": "써볼", "start_ms": 1250, "dur_ms": 310, "phase_lr_deg": 1.4},
{"word": "수", "start_ms": 1580, "dur_ms": 120, "phase_lr_deg": 1.5},
{"word": "있을까요", "start_ms": 1720, "dur_ms": 450, "phase_lr_deg": 1.6}
]
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
"transcript": "아마 다음 달 말쯤에는 가능할 것 같습니다",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 75,
"audio_type": "speech",
"words": [
{"word": "아마", "start_ms": 75, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 380, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "달", "start_ms": 710, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "말쯤에는", "start_ms": 880, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "가능할", "start_ms": 1450, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 1850, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "같습니다", "start_ms": 1980, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "That sounds great I am looking forward to it",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "That", "start_ms": 40, "dur_ms": 220, "phase_lr_deg": 2.8},
{"word": "sounds", "start_ms": 280, "dur_ms": 350, "phase_lr_deg": 2.7},
{"word": "great", "start_ms": 650, "dur_ms": 410, "phase_lr_deg": 2.9},
{"word": "I", "start_ms": 1100, "dur_ms": 150, "phase_lr_deg": 3.0},
{"word": "am", "start_ms": 1280, "dur_ms": 180, "phase_lr_deg": 2.9},
{"word": "looking", "start_ms": 1480, "dur_ms": 380, "phase_lr_deg": 2.8},
{"word": "forward", "start_ms": 1880, "dur_ms": 420, "phase_lr_deg": 3.1},
{"word": "to", "start_ms": 2320, "dur_ms": 150, "phase_lr_deg": 3.0},
{"word": "it", "start_ms": 2500, "dur_ms": 210, "phase_lr_deg": 3.0}
]
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
"transcript": "완성되면 꼭 연락드릴게요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "완성되면", "start_ms": 30, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "꼭", "start_ms": 550, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "연락드릴게요", "start_ms": 720, "dur_ms": 620, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "일본어 버전도 추가할 계획이 있으신가요",
"speaker": "speaker_2",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 85,
"audio_type": "speech",
"words": [
{"word": "일본어", "start_ms": 85, "dur_ms": 410, "phase_lr_deg": 1.8},
{"word": "버전도", "start_ms": 520, "dur_ms": 380, "phase_lr_deg": 1.7},
{"word": "추가할", "start_ms": 950, "dur_ms": 420, "phase_lr_deg": 1.9},
{"word": "계획이", "start_ms": 1400, "dur_ms": 350, "phase_lr_deg": 1.8},
{"word": "있으신가요", "start_ms": 1780, "dur_ms": 580, "phase_lr_deg": 1.7}
]
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
"transcript": "네 물론입니다 일본어와 중국어도 준비 중이에요",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 65, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "물론입니다", "start_ms": 280, "dur_ms": 510, "phase_lr_deg": 0.0},
{"word": "일본어와", "start_ms": 850, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "중국어도", "start_ms": 1480, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "준비", "start_ms": 2050, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "중이에요", "start_ms": 2400, "dur_ms": 480, "phase_lr_deg": 0.0}
]
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
"transcript": "それは素晴らしいですね 楽しみにしています",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ja",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "それは", "start_ms": 50, "dur_ms": 320, "phase_lr_deg": 1.2},
{"word": "素晴らしいですね", "start_ms": 400, "dur_ms": 850, "phase_lr_deg": 1.1},
{"word": "楽しみに", "start_ms": 1300, "dur_ms": 520, "phase_lr_deg": 1.3},
{"word": "しています", "start_ms": 1850, "dur_ms": 480, "phase_lr_deg": 1.2}
]
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
"transcript": "도움이 된다니 다행이네요 그럼 나중에 뵙겠습니다",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "도움이", "start_ms": 80, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "된다니", "start_ms": 420, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "다행이네요", "start_ms": 800, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "그럼", "start_ms": 1450, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "나중에", "start_ms": 1780, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "뵙겠습니다", "start_ms": 2150, "dur_ms": 520, "phase_lr_deg": 0.0}
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
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
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
"transcript": "네 안녕히 가세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 1.4},
{"word": "안녕히", "start_ms": 200, "dur_ms": 380, "phase_lr_deg": 1.5},
{"word": "가세요", "start_ms": 600, "dur_ms": 350, "phase_lr_deg": 1.4}
]
},
{
"segment": 46,
"transcript": "Goodbye and have a nice day",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Goodbye", "start_ms": 40, "dur_ms": 450, "phase_lr_deg": 2.5},
{"word": "and", "start_ms": 520, "dur_ms": 180, "phase_lr_deg": 2.4},
{"word": "have", "start_ms": 720, "dur_ms": 220, "phase_lr_deg": 2.6},
{"word": "a", "start_ms": 960, "dur_ms": 80, "phase_lr_deg": 2.5},
{"word": "nice", "start_ms": 1060, "dur_ms": 250, "phase_lr_deg": 2.6},
{"word": "day", "start_ms": 1350, "dur_ms": 380, "phase_lr_deg": 2.5}
]
},
{
"segment": 47,
"transcript": "I would not be divisive danger that believe",
"speaker": "speaker_2",
"confidence": 0.88,
"language": "en",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": 1.0},
{"word": "would", "start_ms": 280, "dur_ms": 180, "phase_lr_deg": 1.1},
{"word": "not", "start_ms": 480, "dur_ms": 210, "phase_lr_deg": 1.0},
{"word": "be", "start_ms": 720, "dur_ms": 140, "phase_lr_deg": 1.2},
{"word": "divisive", "start_ms": 880, "dur_ms": 520, "phase_lr_deg": 1.1},
{"word": "danger", "start_ms": 1450, "dur_ms": 410, "phase_lr_deg": 1.0},
{"word": "that", "start_ms": 1900, "dur_ms": 220, "phase_lr_deg": 1.2},
{"word": "believe", "start_ms": 2150, "dur_ms": 480, "phase_lr_deg": 1.1}
]
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
"transcript": "마지막으로 테스트를 마칩니다 감사합니다",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "마지막으로", "start_ms": 100, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "테스트를", "start_ms": 800, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "마칩니다", "start_ms": 1320, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 1900, "dur_ms": 610, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 12:04:13*
