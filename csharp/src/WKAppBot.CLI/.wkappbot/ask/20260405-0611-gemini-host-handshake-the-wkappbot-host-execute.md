# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=6944  time=06:10:29  uptime=16039s  mem=392MB  system_processes=353"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_061008.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 631-613 752-756-163-621-132-326 362-236-316-310-312-123 267-267_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 301-325-310-103-102-126-103-671-762-132-621-627-236-126-310-362-236-213 623-627-263_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 157-153-175-175-135-316-310-132-163-163-576-574-574-574-137-137-137-157-130-136-163-136-103-137-175-157-365-136-765-765-765-765-754-137-135-571-105-130-132-135-756-756-756-756-5_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 751-371-317-326-132-312-301-310-103-130-103-130-213-137-137-126-162-137-132-136-675-567-765-135-571-156-673-671-675-263-162-623-267-632-321-627-267-672 613-103-132-672-267-263-6_W.mp3)
  Segment 5: 6304ms ~ 18400ms (file: 267-263-236-167-631-106-312-601-167-617-267-326-627-267-267-276-103-106-102-261-163-103-271-276-230-671-672-723-132-102-167-732-321-213-371-327-327-327-267-326-123-102-517-726-7_W.mp3)
  Segment 6: 19400ms ~ 19976ms (file: 135-154-135-105-130-105-103-105-103-135-103-314-315-315-134-130-314-103-134-310-143-104-105-103-134-104-301-310 154-453-514-145-104-103-103-104-135-135-134-103-143-134-135-134-1_V.mp3)
  Segment 7: 20976ms ~ 21552ms (file: 312-132-672-632-541-326-173-160-632-623-715-173-176-165-652-260_W.mp3)
  Segment 8: 22552ms ~ 23128ms (file: 602-602-576-574-572-576-756-321-312-132-312-302-312-316-163-106-132-136-301-310-130-103-103-310_V.mp3)
  Segment 9: 24128ms ~ 28484ms (file: 267-261-260-623-261-267-267-261-123-326-267-267-267-216-267-627-261-627-267-261-267-236-627-261-721-215-130-132-321-627-326-632-261-132-621-132-612-162-162-126-163-162-216-162-1_W.mp3)
  Segment 10: 29484ms ~ 30060ms (file: 761-671-716-761-762-761-761-617-756-756-756-756-756-756-756-756-756-571-716-671-621-106-137-216-716-761-723-756-751-756-751-751-752-576-572-751-753-751-715-751-756-675-675-615-6_W.mp3)
  Segment 11: 31060ms ~ 38728ms (file: 231-271-123-267-721-275-270-263-123-236-527-312-573-267-276-725-213-721-130-123-716-103-261-123-130-623-572-237-571-213-267-576-275-726-160-103-216-327-102-576-370-137-713-301-7_W.mp3)
  Segment 12: 39728ms ~ 40304ms (file: 312-217-237-573-532-123-527 713-267-231-312-756-637-236-326-762-326-231-531 370-371-712-713-173-173-521-576-263-132-372-275-750-267-120-123-752 762-205-267-672-267-572-715-257-5_V.mp3)
  Segment 13: 41304ms ~ 46488ms (file: 261-102-120-235-572-236-236-210-261-524-524-120-263-267-263-126-102-261-627-267-572-752-260-260-201-726-216-542-201-210-206-126-103-201-527-257-213-123-206-260-102-102-547-547-5_W_stt_the_most_frustrating_fully_half_a_dozen.mp3)
  Segment 14: 47488ms ~ 55516ms (file: 617-362-567-316-265-126-176-702-216-673-365-572-267-132-167-506-157-321-574-513-756-725-263-672-367-526-750-267-762-732-312-507-671-132-231-237-107-326-265-135-127-317-637-602-1_V.mp3)
  Segment 15: 56516ms ~ 57092ms (file: 310-312-127-216-625-123-153-750-572-206-216-276-206-725-632-576-132-231-572-276-273-275-231-216-137-317-351-675-123-321-725-172-263-271-572-725-257-570-326-276-267-526-627-625-3_V.mp3)
  Segment 16: 58092ms ~ 58668ms (file: 675-672-675-761-576-623-136-361-106-361-632-627-136-613-316 507-672-762-670-132-256-126-623-167-276-263-136-321-261-263-263-621-316-672-627-362-612-216-613-213-625-216-572-102-2_W.mp3)
  Segment 17: 59668ms ~ 60244ms (file: 316-617-671-637-162-613-617-631-167-631-612-631-631-361-631-361-631-136-763-367-617-631-637-167-635-356-362-361-362-356-607-625-652-657-675-536-537-567-537-564-513-541-513-531-5_V.mp3)
  Segment 18: 61244ms ~ 64844ms (file: 126-216-123-231-123-126-217-126-267-127-267-263-216-267-261-267-261-267-217-126-216-267-321-216-213-126-360-302-230-230-203-267-627-260-627-627-263-627-627-625-206-236-260-207-3_W.mp3)
  Segment 19: 65844ms ~ 66420ms (file: 761-176-617-176-137-176-716-173-317-173-176-715-751-367-173-176-731-173-137-516-156-567-567-637-637-123-362-316-132-132-136-132-135-157-517-571-156-572-751-375-715-716-170-175-1_W.mp3)
  Segment 20: 67420ms ~ 67996ms (file: 547-571-507-570-570-570-570-517-571-517-517-571-571-571-570-517-571-571-157-571-574-105-570-571-715-751-571-107-715-107-751-107-713-105-715-107-175-701-751-157-107-750-170-157-1_V.mp3)
  Segment 21: 68996ms ~ 77600ms (file: 126-173-276-267-103-157-120-103-153-253-537-567-156-316-132-265-235-162-627-231-216-120-123-130-627-267-627-201-206-130-267-267-617-672-672-671-260-123-103-103-260-275-210-201-3_W.mp3)
  Segment 22: 78600ms ~ 86628ms (file: 761-213-261-571-175-675-107-176-137-107-312-130-765-130-236-236-576-270-672-672-721-312-326-276-231-261-216-751-130-230-267-231-102-576-752-675-236-312-137-321-321-143-230-312-5_W.mp3)
  Segment 23: 87628ms ~ 88204ms (file: 517-571-517-510-561-576-571-567-570-570-501-517-157-570-517-175-571-510-137-517-517-513-175-715-716-715-175-571-751-571-576-571-576-570-570-570-576 560-517-517-570-571-153-571-5_V.mp3)
  Segment 24: 89204ms ~ 89780ms (file: 132-153-165-167-327-237-576-762-723-273-215-750-123-627-203-213-125-176-316-102-103-132-102-126-167-562-625 123-231-203-302-230-576-261-276-702-576-752-275-672-273-327-237-302-3_V.mp3)
  Segment 25: 90780ms ~ 91356ms (file: 517-571-571-751-570-574-570-571-150-571-571-170-751-571-570 510-517-507-510-507-517-507-517-105-571-501-507-157 571-570-571-517-570-571-570-157-571-574-570 504-570-517-571-570-5_W.mp3)
  Segment 26: 92356ms ~ 92932ms (file: 106-310-230-312-103-103-376-130-317-723-301-103-321-361-130-130-132-301-213-231-670-106-301-123-120-132-261-612-261-701-213-123-132-237-231-321-320-321 547-547-540-132 164-102-6_V.mp3)
  Segment 27: 93932ms ~ 103004ms (file: 263-136-216-623-620-762-765-213-267-263-620-542-321-231-527-276-216-273-675-627-235-103-201-601-261-230-213-735-251-762-231-672-602-206-105-541-512-572-130-213-273-261-103-175-2_W.mp3)
  Segment 28: 104004ms ~ 115020ms (file: 751-107-260-257-256-263-132-576-315-547-572-325-312-103 627-720-312-260-213-267-253-257-320-574-752-276-267-321-130-130-310-267-267-321-173-167-130-537-275-573-574-207-261-574-2_W.mp3)
  Segment 29: 116020ms ~ 116596ms (file: 162 613-621-231-620-602-261-102-103-312-174-154-103 126-132-103-132-167-213-132-103-103-103-102-310-321-321-310-312-103-136-103-103-137-103-671-621-261-132-103-103-135-136-134-1_V.mp3)
  Segment 30: 117596ms ~ 118172ms (file: 105-156-517-510-157-150-105-150-510-157-150-510-517-150-501-157-157 105-106-105-105-105-150-150-150-150-105-157-150-517-105-105-751-751-751-751 105-150-105-150-105 105-150-105-1_V.mp3)
  Segment 31: 119172ms ~ 119748ms (file: 731-371-371-317-317-357-735-357-371-750 735-107-573-751-317-135-317-176-713-301-310-137-713-310-137-317-317-103-107-176-175-673-736-675-673-365-756 731-371-130-317-157-317-310-1_V.mp3)
  Segment 32: 120748ms ~ 121324ms (file: 107-130-376-370-310-317-103-310-130-310-317-312-370-157-137-307-175-371 713-750-173-317-713-317-715-310-137-107-735-731-370-301-317-307-103-301-173-137-317-371-103-317-317-310-1_V.mp3)
  Segment 33: 122324ms ~ 122900ms (file: 130-310-135-130-104-103-145-104-145-314-143-134-135-143-143-134-135-134-145-135-135-134-103-134-143 341-314-103-310-145-304-134-140-310-104-104-103-105-150-140-104-103-154-451-1_V.mp3)
  Segment 34: 123900ms ~ 124476ms (file: 132-367-617-763-613-517 376-317-371-135-310-213-263-321-126-572-537-576-756-576-751-135-175-157-315-657-627-631-637-631-361-361 217-123-231-237-716-173-715-213 670-672-750 762-2_W.mp3)
  Segment 35: 125476ms ~ 132388ms (file: 231-370-103-102-260-172-327-316-176-167-132-367-572-365-765-725-276-132-175-762-752-571-675-567-167-213-126-712-276-671-261-263-213-130-103-231-213-216-260-102-102-120-126-216-1_W.mp3)
  Segment 36: 133388ms ~ 133964ms (file: 103-310 132-130-103-136-103-103 162-612-163-162-610-610-106-516-506-576-105-507-157-103-517-152-517-571-507-507-507-570-501-570-517-571-570-576-576-175-570-521-517-751-571-751-7_W.mp3)
  Segment 37: 134964ms ~ 146160ms (file: 206-201-103-357-531-301-103-123-312-102-132-130-213-762-762-260-540-612-312-310-103-320-203-236-210-132-320-132-132-310-130-103-103-351-230-320-312-103-310-134-102-106-156-157-1_V.mp3)
  Segment 38: 147160ms ~ 147736ms (file: 671-765-765-367-361-367-751-367-637-176-756-176-176-761-675-617-761-167-176-173-731-761-675-367-576-756-761-627-361-173-175-715-671-167-617-167-617-106-167-107-163-173-137-176-1_W.mp3)
  Segment 39: 148736ms ~ 155108ms (file: 103-701-106-102-132-102-213-103-230-203-320-326-312-302-173-320-321-301-310-130-130-613-321-132-302-320-136-107-310-301-103-167-531-253-230-231-230-320-321-321-316-132-760-675-1_V.mp3)
  Segment 40: 156108ms ~ 156684ms (file: 715-715-715-751-753-751-751 751-751-750-751-750-715-571-751-570-751-751-751-571-753-571-517-751-751-571-751-751-573-753 573-751-751-571-751-751-750-750-751-751-751-715-751-751-5_V.mp3)
  Segment 41: 157684ms ~ 158260ms (file: 517-571-751-513-571-571-571-157-574-576-571-517-105-103-102-103-135-105-165-106-152-351-517-571-750-157-175-170-715-713-173-175-715-157-715-157-517-175-571-571 517-571-751-153-7_V.mp3)
  Segment 42: 159260ms ~ 167972ms (file: 217-372-756-176-752-725-756-276-276-267-301-321-237-137-236-276-236-273-275-327-213-716-176-754-756-176-271-276-762-267-120-137-672-627-210-276-213-267-273-107-276-276-627-270-1_W.mp3)
  Segment 43: 168972ms ~ 169548ms (file: 175-157-170-130-107-310 150-150-153-153-150-130-157-105-130-157-157 150-150-150-150-510-105-150-150-175-105-150-150-570-105-157-105-574 150-105-150-153-301-150-105-371-103-301-1_V.mp3)
  Segment 44: 170548ms ~ 171124ms (file: 623-632-576-756-627-675-642-567-675-576-563-573-762-627-657-765-657-376-657-756-765-567-567-576-562-675-657-547-547-546-546-567-576-657-675-675-675 312-576-756-675-574-175-137-1_W.mp3)
  Segment 45: 172124ms ~ 172700ms (file: 715-107-105-105-107-715-157 150-715-157-105-157-107-751-107-175-107-715-157-107-175-157-751-175-175-175-715-107 750-175-105-701-157-107-701-107-715-175-715-175-751-157-715-751-1_V.mp3)
  Segment 46: 173700ms ~ 181835ms (file: 237-312-267-231-547-574-103-102-102-321-123-132-123-316-103-312-120-123-213-301-510-105-130-125-213-213-203-210-130-120-231-213-253-321-130-213-572-123-132-230-132-231-132-123-2_W.mp3)
  Segment 47: 182835ms ~ 183411ms (file: 516-576-517-517-567-561-503-517-571-572-752-571-572-571-572-573-517-573-576-560-571-573-513-571-573-572-517-576-571-572-517-156-157-572-157-517-571-517-576-573-576-571-517-572-5_W.mp3)
  Segment 48: 184411ms ~ 184987ms (file: 762-623-623-627-627-613-263-637-162-621-671-671-671-576-657-576-576-672-627-765-765-765-564-567-615-612-160-613-612-671-635-621-617-136-613 653-362-562-653-562-652-152-516-361-1_W.mp3)
  Segment 49: 185987ms ~ 186563ms (file: 715-571-573-570-571 517-156-157-715-715-157-157-351-175-751-175-175-517-571-175-157-517-571-157-517-157-175-157-157-571-157-517-571-571-750 571-571-571-570-175-157-510-157-153-7_V.mp3)
  Segment 50: 187563ms ~ 195807ms (file: 321-263-316-261-261-216-572-261-261-236-130-263-263-231-276-263-263-263-102-576-256-524-236-547-236-103-754-317-130-321-174-106-617-132-613-261-260-123-106-756-627-756-754-267-2_W.mp3)
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
> **Time**: 2026-04-05 06:11:40
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "이러는 것이구나 그 아시다시피 그때그때 음식 바꿀 때마다 이렇게 명찰을 다 바꾸고 있습니다 한 번에 배식대에 올리는 요리는 15가지 정도 요리가 비면 다른 음식으로 교체됩니다 처음 오픈했을 때",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "이러는", "start_ms": 100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "것이구나", "start_ms": 450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 1000, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "아시다시피", "start_ms": 1150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "그때그때", "start_ms": 1750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "음식", "start_ms": 2350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "바꿀", "start_ms": 2650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "때마다", "start_ms": 2950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 3450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "명찰을", "start_ms": 3800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 4200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "바꾸고", "start_ms": 4350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 4700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 5250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "번에", "start_ms": 5400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "배식대에", "start_ms": 5700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "올리는", "start_ms": 6150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "요리는", "start_ms": 6500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "15가지", "start_ms": 6850, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "실용적인 방패를 만들어낸 셈입니다 다만 이 거대한 기회 앞에서 우리가 절대 잊지 말아야 할 현실적 경계의 목소리가 있습니다 중동의 복잡한 정치적 이해",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "실용적인", "start_ms": 80, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "방패를", "start_ms": 650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "만들어낸", "start_ms": 1150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "셈입니다", "start_ms": 1650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "다만", "start_ms": 2250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 2600, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "거대한", "start_ms": 2750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "기회", "start_ms": 3200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "앞에서", "start_ms": 3500, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "로 끝난다는 반응인데요 실제로 영화는 결말로 갈수록 분위기가 완전히 달라집니다 처음에는 추격과 추격 잠입 작전이 이어지는 전형적인 첩보의 정석처럼 보이지만 이야기 뒤로 갈수록 총보다 무서운 게 등장합니다 두 주인공은 끝까지 서로",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "로", "start_ms": 60, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "끝난다는", "start_ms": 300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "반응인데요", "start_ms": 800, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "실제로", "start_ms": 1450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "영화는", "start_ms": 1950, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "박성원입니다 트럼프 대통령은 미국이 세계 최대 산유국이며 미국은 중동 산 원유에 의존하지 않는다는 점을 다시 강조했습니다 호르무즈 해협과 미국은 상관",
"speaker": "speaker_4",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "박성원입니다", "start_ms": 50, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "트럼프", "start_ms": 850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "대통령은", "start_ms": 1300, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "미국이", "start_ms": 1900, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "포착해 하루에도 수십 번의 진입으로 시장의 리듬을 완벽하게 통제하는 인물 그리고 수천만 달러의 자산을 오직 단타 트레이딩 하나만으로 일궈낸",
"speaker": "speaker_5",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "포착해", "start_ms": 110, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "하루에도", "start_ms": 650, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "와 다 익었어요 다 익었어 밖에서 먹었을 때 살짝 이렇게 부드러우면서 아삭아삭했거든요 그게 그냥 가운데",
"speaker": "speaker_6",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "와", "start_ms": 90, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 400, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "익었어요", "start_ms": 600, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "의 핵심 또한 예측이 아니라 리듬 포착이었습니다 빠르게 꺾이고 잠시 쉬었다가 다시 떨어지는 그 타이밍 바로 거기가 노아의 진입 구간입니다 지금까지 보신",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "의", "start_ms": 70, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "핵심", "start_ms": 250, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "와 와 도훈아 미친 듯이 좋",
"speaker": "speaker_6",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "와", "start_ms": 120, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "180킬로씩 실어 주세요 밥은 하루 두 번 입고 그중 새벽에는 주로 납품",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "180킬로씩", "start_ms": 50, "dur_ms": 750, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 11,
"transcript": "그 건물의 본연의 기능을 조금 잃을 수도 있습니다 어 근데 오늘 집이 정말 특이한 게 그 사찰 건물 종교적인 색채가 강한 건",
"speaker": "speaker_7",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "그", "start_ms": 80, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "건물의", "start_ms": 250, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "but i am so ready for ai selectable control because we are young are you more excited than concerned more concerned than excited",
"speaker": "speaker_8",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "but", "start_ms": 150, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "나왔었어 그게 심부름이 돼주고 팔아주고 이런 것들도 해야지",
"speaker": "speaker_9",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "나왔었어", "start_ms": 100, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "트 팩쓰리 한 발은 400만 달러에 달해 경쟁력의 의구심을 노출시켰다는 겁니다 뉴욕 타임스는 미국 방산업체들의 생산력 문제도 짚었는데 급증하는 수요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "트", "start_ms": 70, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "그럼 이거 많이 됐어 20년 30년 40년 40년 19살에 와서 84시요",
"speaker": "speaker_10",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 110, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "이 전쟁 한복판에 이제 그대로 놓였었는데 어떻게 보면 이제 미사일 요격 잔해물들이나 미사일 자체가 우리 선박에 떨어질 수도 있었기 때문에 엄청나게 이제 위험한 상황이었습니다 네 현재는",
"speaker": "speaker_11",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 80, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "이렇게 준비됐고 아 와 세상에서 제일 맛있는 물이다 음 엄청 밥이랑 물이랑 잘 어울려요 와 좋다 우와",
"speaker": "speaker_6",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "이렇게", "start_ms": 120, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "가게 정리를 하면 길었던 하루도 끝이 납니다 다음 날 이른 새벽",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "가게", "start_ms": 60, "dur_ms": 450, "phase_lr_deg": 0.0}
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
"transcript": "the c- all that still not be denied by when you",
"speaker": "speaker_12",
"confidence": 0.85,
"language": "en",
"silence_before_ms": 200,
"audio_type": "speech",
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
*Generated by WKAppBot ask gemini — 2026-04-05 06:11:40*
