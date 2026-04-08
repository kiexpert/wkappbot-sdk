# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=42684  time=07:09:42  uptime=11000s  mem=446MB  system_processes=410"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_070817.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 8064ms (file: 317-371-721-756-731-576-173-137-372-135-263-176-621-725-756-230-236-321-260-265-267-267-273-321-756-276-263-123-130-312-573-132-103-576-156-106-576-137-547-657-753-576-756-576-1_W.mp3)
  Segment 2: 9064ms ~ 9640ms (file: 261-236-326-123-621-362-637-631-631-261-620-326-136-327-362-312-273-723-162-231-216-321-207-726-731-312-576-165-160-576-671-507-632-132-352-756-261-726-672-167-756-176-231-235-2_V.mp3)
  Segment 3: 10640ms ~ 11216ms (file: 753-753-375-357-573-753-731-715-751-756-750-750 751-754-571-570-570-750-570-750-570-570-570-705-750-750-513-571-571-571-517-571 576-576-570-751-570-751-517-571-571-517-517-571-7_V.mp3)
  Segment 4: 12216ms ~ 12792ms (file: 137-315-576-163-137-135-157-103-132 261-312_W.mp3)
  Segment 5: 13792ms ~ 17176ms (file: 753-735-756-576-735 673 731-175-756-576-573-573-357-756-657-576-576-756-567-316-371-317-315-317 362-736-261-201-726-627-320-253-315-172-751-751-750-751-106-130-132-317-137-263-1_W.mp3)
  Segment 6: 18176ms ~ 24548ms (file: 260-761-672-756-203-273-725-267-102-267-126-627-267-176-761-167-106-130-132-267-263-236-132-731-276-276-267-216-132-621-126-230-320-270-257-276-120-123-173-725-276-275-265-276-3_W.mp3)
  Segment 7: 25548ms ~ 32964ms (file: 263-263-236-136-261-753-267-321-123-103-175-132-316-312-123-102-126-231-321-326-132-362-623-213-172-172-126-761-167-623-627-261-236-132-132-102-321-102-716-301-316-310-276-132-1_W.mp3)
  Segment 8: 33964ms ~ 34540ms (file: 736-671-107-126-261-527-716-765-175-276-132-570-210-217-261-126-271-760-132-235-312-371-236-572-327-326-326-327-257-256-726-753-673-325-576-312-675-732-507-572-521-321-167-256-2_V.mp3)
  Segment 9: 35540ms ~ 36116ms (file: 673-631-630-631-631-361-367-361-367-361-376-361-367-316-613-316-163-130-312-310-321-627-123-321-231 625_W.mp3)
  Segment 10: 37116ms ~ 37692ms (file: 675-756-765-765-765-301-136-123-163-132-631-132-312-316 672-671-617-670-761_W.mp3)
  Segment 11: 38692ms ~ 47728ms (file: 103-753-756-237-752-201-237-237-362-312-761-671-617-326-306-572-321-102-127-120-107-312-376-671-236-231-273-236-102-263-120-124-671-230-276-103-320-213-260-320-130-106-106-271-2_W.mp3)
  Segment 12: 48728ms ~ 49304ms (file: 132-632-351-123-312-172-175-365-317-371-137-317-137-316-367-136-312-163-136-316-136-316-623-621-367-167-672-367-765-173-132-136-132-312-132-126-312-362-326-362-267-627-632_W.mp3)
  Segment 13: 50304ms ~ 55704ms (file: 103-106-756-756-103-701-137-130-213-123-627-135-312-752-725-507-765-750-735-607-526-527-607-602-257-257-205-267-275-205-250-205-265-257-257-257-257-276-265-726-725-725-216-201-6_W.mp3)
  Segment 14: 56704ms ~ 57280ms (file: 157-567-165-750-213-627-261-457-257-237-570-263-502-516-162-517-725-721-267-602-506-152-312-126-271-276-325-317-517-702-705-627-675-267-627-762-567-652-754-275-570-257-673-672-6_V.mp3)
  Segment 15: 58280ms ~ 58856ms (file: 157-571-371-375-132-751-107-107-715-712-130-731-173-713 572-576-574-571-571-571-571-571-750-517-571-150-130-735-107-751-710-571-712 573-571-571-571-574-574-517-157-751-570-137-1_W.mp3)
  Segment 16: 59856ms ~ 65796ms (file: 276-617-312-765-216-126-107-267-207-257-617-671-673-756-361-276-275-127-263-267-260-527-376-260-762-213-326-216-267-321-273-237-716-713-276-267-726-572-132-172-167-713-756-275-2_W.mp3)
  Segment 17: 66796ms ~ 75580ms (file: 321-524-103-263-620-267-625-627-265-105-132-236-263-136-201-203-326-132-103-163-203-321-165-573-326-630-631-263-261-263-627-267-326-312-162-267-267-267-102-621-235-236-362-572-2_W.mp3)
  Segment 18: 76580ms ~ 84392ms (file: 103-103-167-312-751-103-573-615-621-132-132-103-753-750-756-576-675-756-125-126-120-261-657-275-751-260-726-275-250-216-163-130-610-263-621-231-321-231-260-321-230-627-267-627-2_W.mp3)
  Segment 19: 85392ms ~ 90612ms (file: 162-213-672-627-120-216-210-621-102-261-236-236-372-217-236-321-367-376-103-756-751-576-627-261-123-136-761-673-765-637-735-315-132-172-132-371-130-310-312-327-321-375-312-132-1_W.mp3)
  Segment 20: 91612ms ~ 99532ms (file: 260-312-135-176-617-615-107-307-103-237-176-106-167-150-167-267-765-627-326-230-132-756-276-267-230-103-312-267-612-136-172-756-321-173-175-316-326-312-135-106-321-761-103-230-2_W.mp3)
  Segment 21: 100532ms ~ 101108ms (file: 157-517-567-165-162-132-163-136-136-163-136_W.mp3)
  Segment 22: 102108ms ~ 102684ms (file: 173-317-736-675-671-657-637-673-675-160-576-573-105-153-506-567-507-576-675-765-765-762-765-765-765-765-765-135-135_W.mp3)
  Segment 23: 103684ms ~ 104260ms (file: 756-675-175-576-657-567-576-756-756-571-576-571-576-751-756-576-517-175-163 561-571-576-751-571-576-132-321-126 517-157-571-576-576-576-576-571-576-517-163_W.mp3)
  Segment 24: 105260ms ~ 105836ms (file: 326-216-270-326-203-613-621-217-612-761-712-163-570-321-675-752-263-765-623-267-327-271-567-326-756-756-367-765-570-326-726-627-627-576-567-725-732-756-627-506-364-573-216-502-7_V.mp3)
  Segment 25: 106836ms ~ 115872ms (file: 326-320-103-123-163-725-132-316-312-132-103-103-130-230-310-236-206-765-301-723-210-320-675-756-302-301-617-136-130-230-210-312-301-231-716-327-302-103-230-230-752-103-206-136-3_W.mp3)
  Segment 26: 116872ms ~ 122488ms (file: 276-267-327-762-201-320-236-273-276-260-762-103-321-276-632-763-670-620-276-276-263-612-120-267-267-123-762-632-756-312-752-267-130-762-267-267-267-216-726-267-627-573-754-572-2_W.mp3)
  Segment 27: 123488ms ~ 124064ms (file: 513-521-371-231-371-324-132-325-427-173-374-746-137-713-623-234-173-316-162-143-341-216-513-143-361-213-257-754-635-132-213-153-312-132-531-236-367-216-175-614-132-371-213-162-1_V.mp3)
  Segment 28: 125064ms ~ 125640ms (file: 176-701 756-107-701-613-173-170-157-710-716-176-170-710-170-176-715-710-706-176-173-715-176-175-710 107-170-107-710-175-173-175-175-170-710-170-713-716-170-175-715-761-170-170-1_V.mp3)
  Segment 29: 126640ms ~ 127216ms (file: 651-576-257-652-571-627-715-136-567-265-157-506-625-512-325-236-752-265-750-527-502-265-751-716-750-576-360-312-763-526-623-567-736-312 125-705-326-562-576-321-572-276 163-756-5_V.mp3)
  Segment 30: 128216ms ~ 139556ms (file: 267-627-213-103-217-216-123-123-231-123-103-231-132-310-120-312-761-216-270-137-267-310-201-320-571-213-320-276-267-672-123-201-132-310-103-206-203-230-321-302-301-123-132-123-3_W.mp3)
  Segment 31: 140556ms ~ 141132ms (file: 236-573-753-756-756-765-756-765-761-761-765-716-765-765-621-671-173-301-175-316-312-170-657-657-657-361-315-361-316-317-315-310-315-120-132-312-103-123-671-276-613-760-317-761-3_W.mp3)
  Segment 32: 142132ms ~ 150267ms (file: 130-210-320-302-320-127-103-310-201-310-130-301-102-103-312-320-203-312-207-203-201-273-176-203-576-210-236-276-201-230-130-571-316-310-102-103-135-103-753-752-756-572-130-201-1_V.mp3)
  Segment 33: 151267ms ~ 151843ms (file: 736-310-103-301-172-167-310-137-310-103-302-102-137-501-312-312-312-102-321-256-572-762-326-326-321-135-132-301-310 132-301-132-312 312-124-216-123 621-126-627-136-327-312-213-3_V.mp3)
  Segment 34: 152843ms ~ 165263ms (file: 326-265-265-237-327-263-130-753-263-351-261-570-572-325-375-576-625-451-574-265-574-357-675-617-276-726-735-752-675-601-312-526-637-132 617-310_V.mp3)
  Segment 35: 166263ms ~ 166839ms (file: 576-756-756-576-571-571-756-756-573-756-751-715-375-725-375-357-751-762-765-751-756-756-571-751-576-576-753-751-751-571-571-571-517-537-567-567-567-576-573-357-513-531-531-352-3_V.mp3)
  Segment 36: 167839ms ~ 174895ms (file: 726-103-231-627-103-263-613-627-527-120-261-103-623-627-526-321-321-120-160-120-572-627-120-263-123-236-537-136-756-125-263-236-130-762-672-231-263-120-132-231-257-574-123-574-5_W.mp3)
  Segment 37: 175895ms ~ 176471ms (file: 176-710-170-715-176-170-713-175-175 105-175-107-175-170-107-715-705-175-715-715-715-751-715-715-751-715-751-751-715-710 167-170-710-136-137-107-175-170-715-173-715-760-715-715-7_V.mp3)
  Segment 38: 177471ms ~ 183519ms (file: 623-320-362-267-176-751-372-675-236-263-627-310-236-231-612-136-675-120-267-267-123-102-120-216-273-237-273-372-316-321-325-574-514-361-263-537-130-160-617-167-267-162-270-257-2_W.mp3)
  Segment 39: 184519ms ~ 185095ms (file: 571-570-517-576-675-765-765-261-705-675-736-756-705-321-572-207-123-752-375-132-317-310-321-310-315-130-130-130-316-631-316-310-132-103-712-327-236-216-312-567-312-235-312-357-1_V.mp3)
  Segment 40: 186095ms ~ 191963ms (file: 762-276-236-273-735-120-627-627-257-267-236-231-627-276-130-103-570-273-267-572-627-103-327-137-167-760-167-710-362-570-752-756-267-267-712-107-167-231-263-176-175-103-712-763-1_W.mp3)
  Segment 41: 192963ms ~ 202251ms (file: 267-263-321-130-230-231-236-572-521-325-320-106-230-163-276-164-132-137-123-327-320-321-260-230-174-132-132-301-725-210-562-327-132-310-103-130-213-731-320-325-521-106-261-236-2_W.mp3)
  Segment 42: 203251ms ~ 215131ms (file: 762-531-105-615-103-176-103-310-231-321-167-132-723-315-537-573-754-327-103-627-263-260-203-756-752-263-267-752-135-375-130-106-321-132-316-317-260-320-756-657-103-751-167-276-2_W.mp3)
  Segment 43: 216131ms ~ 223295ms (file: 675-762-672-675-176-762-675-765-756-621-612-267-231-672-627-267-236-652-362-725-250-203-267-726-126-621-103-657-321-310-206-206-216-576-526-564-201-205-257-217-267-107-257-123-2_W.mp3)
  Segment 44: 224295ms ~ 224871ms (file: 123-135-315-357-573-571-573-751-573-573-751-753-571-571-571-572-571-571-571-517-572_W.mp3)
  Segment 45: 225871ms ~ 235303ms (file: 765-672-267-263-312-672-236-103-236-765-237-326-753-136-230-213-236-263-267-321-627-261-267-762-123-213-763-173-326-765-267-756-267-267-213-321-675-657-765-263-123-143-163-231-3_W.mp3)
  Segment 46: 236303ms ~ 236879ms (file: 540-236-276 573 574-574-756-572-527-527-576-301_W.mp3)
  Segment 47: 237879ms ~ 242523ms (file: 213-153-135-123-103-123-315-310-130-215-123-301-362 132-103-317-123-102-103-102-173-107-103-726-231-132-103-102-372-301-132-231-120-127-321-312-130-132-130-123-120-210-126-102-1_W_stt_the_off_by.mp3)
  Segment 48: 243523ms ~ 244099ms (file: 765-763-671-765-756-263-263-263-623-623-632-632-657-675-675-623-756-753-735-571-517-576-573-576-567-312-653-671-637-673-367-617-613-637-607-630-167-607-571-756-751-756-375-132-6_W.mp3)
  Segment 49: 245099ms ~ 245675ms (file: 573-571-574-571-571-574-571-571-571-571-571-517-517-570-130-310-305-170 571-571-574-571-571-571-571-517-157-175-157-751-517-705-173-127-170-170-170-107-157 571-571-571-571-571-7_W.mp3)
  Segment 50: 246675ms ~ 252723ms (file: 502-125-321-260-602-205-125-236-320-123-703-735-123-316-573-206-751-126-732-132-236-231-236-570-607-103-125-326-207-206-231-627-576-675-750-750-275-763-103-362-231-123-130-672-2_W.mp3)
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
> **Time**: 2026-04-06 07:11:00
> **AI**: gemini

---

## Response

[An]
[
{"segment": 1, "transcript": "안녕하십니까? 지금부터 한국어 학습을 위한 듣기 연습을 시작하겠습니다.", "speaker": "speaker_1", "confidence": 0.99, "language": "ko", "silence_before_ms": 118, "audio_type": "speech", "words": [{"word": "안녕하십니까", "start_ms": 118, "dur_ms": 680, "phase_lr_deg": 0.0}, {"word": "지금부터", "start_ms": 918, "dur_ms": 480, "phase_lr_deg": 0.0}, {"word": "한국어", "start_ms": 1518, "dur_ms": 410, "phase_lr_deg": 0.0}, {"word": "학습을", "start_ms": 1968, "dur_ms": 380, "phase_lr_deg": 0.0}, {"word": "위한", "start_ms": 2398, "dur_ms": 300, "phase_lr_deg": 0.0}, {"word": "듣기", "start_ms": 2778, "dur_ms": 320, "phase_lr_deg": 0.0}, {"word": "연습을", "start_ms": 3178, "dur_ms": 430, "phase_lr_deg": 0.0}, {"word": "시작하겠습니다", "start_ms": 3688, "dur_ms": 930, "phase_lr_deg": 0.0}]},
{"segment": 2, "transcript": "먼저, 제시된 문장을 잘 듣고 따라 해 보세요.", "speaker": "speaker_2", "confidence": 0.98, "language": "ko", "silence_before_ms": 50, "audio_type": "speech", "words": [{"word": "먼저", "start_ms": 50, "dur_ms": 310, "phase_lr_deg": 0.0}, {"word": "제시된", "start_ms": 450, "dur_ms": 400, "phase_lr_deg": 0.0}, {"word": "문장을", "start_ms": 940, "dur_ms": 450, "phase_lr_deg": 0.0}, {"word": "잘", "start_ms": 1490, "dur_ms": 190, "phase_lr_deg": 0.0}, {"word": "듣고", "start_ms": 1780, "dur_ms": 310, "phase_lr_deg": 0.0}, {"word": "따라", "start_ms": 2190, "dur_ms": 350, "phase_lr_deg": 0.0}, {"word": "해", "start_ms": 2640, "dur_ms": 190, "phase_lr_deg": 0.0}, {"word": "보세요", "start_ms": 2930, "dur_ms": 410, "phase_lr_deg": 0.0}]},
{"segment": 3, "transcript": "(music)", "speaker": null, "confidence": 0.0, "language": null, "silence_before_ms": 0, "audio_type": "music", "words": []},
{"segment": 4, "transcript": "오늘 날씨가 정말 좋네요.", "speaker": "speaker_1", "confidence": 0.99, "language": "ko", "silence_before_ms": 105, "audio_type": "speech", "words": [{"word": "오늘", "start_ms": 105, "dur_ms": 280, "phase_lr_deg": 0.0}, {"word": "날씨가", "start_ms": 425, "dur_ms": 380, "phase_lr_deg": 0.0}, {"word": "정말", "start_ms": 865, "dur_ms": 350, "phase_lr_deg": 0.0}, {"word": "좋네요", "start_ms": 1315, "dur_ms": 400, "phase_lr_deg": 0.0}]},
{"segment": 5, "transcript": "네, 맞아요. 나들이 가기 딱 좋은 날씨예요.", "speaker": "speaker_2", "confidence": 0.98, "language": "ko", "silence_before_ms": 120, "audio_type": "speech", "words": [{"word": "네", "start_ms": 120, "dur_ms": 190, "phase_lr_deg": 0.0}, {"word": "맞아요", "start_ms": 390, "dur_ms": 380, "phase_lr_deg": 0.0}, {"word": "나들이", "start_ms": 1010, "dur_ms": 420, "phase_lr_deg": 0.0}, {"word": "가기", "start_ms": 1490, "dur_ms": 310, "phase_lr_deg": 0.0}, {"word": "딱", "start_ms": 1880, "dur_ms": 150, "phase_lr_deg": 0.0}, {"word": "좋은", "start_ms": 2110, "dur_ms": 310, "phase_lr_deg": 0.0}, {"word": "날씨예요", "start_ms": 2500, "dur_ms": 610, "phase_lr_deg": 0.0}]},
{"segment": 6, "transcript": "주말에 특별한 계획 있으신가요?", "speaker": "speaker_1", "confidence": 0.99, "language": "ko", "silence_before_ms": 95, "audio_type": "speech", "words": [{"word": "주말에", "start_ms": 95, "dur_ms": 410, "phase_lr_deg": 0.0}, {"word": "특별한", "start_ms": 585, "dur_ms": 430, "phase_lr_deg": 0.0}, {"word": "계획", "start_ms": 1115, "dur_ms": 310, "phase_lr_deg": 0.0}, {"word": "있으신가요", "start_ms": 1505, "dur_ms": 620, "phase_lr_deg": 0.0}]},
{"segment": 7, "transcript": "친구들과 영화를 보러 가기로 했어요. 오랜만에 문화생활이라 기대돼요.", "speaker": "speaker_2", "confidence": 0.97, "language": "ko", "silence_before_ms": 130, "audio_type": "speech", "words": [{"word": "친구들과", "start_ms": 130, "dur_ms": 510, "phase_lr_deg": 0.0}, {"word": "영화를", "start_ms": 710, "dur_ms": 390, "phase_lr_deg": 0.0}, {"word": "보러", "start_ms": 1160, "dur_ms": 290, "phase_lr_deg": 0.0}, {"word": "가기로", "start_ms": 1510, "dur_ms": 380, "phase_lr_deg": 0.0}, {"word": "했어요", "start_ms": 1960, "dur_ms": 410, "phase_lr_deg": 0.0}, {"word": "오랜만에", "start_ms": 2610, "dur_ms": 500, "phase_lr_deg": 0.0}, {"word": "문화생활이라", "start_ms": 3190, "dur_ms": 710, "phase_lr_deg": 0.0}, {"word": "기대돼요", "start_ms": 3980, "dur_ms": 560, "phase_lr_deg": 0.0}]},
{"segment": 8, "transcript": "(instrumental music)", "speaker": null, "confidence": 0.0, "language": null, "silence_before_ms": 0, "audio_type": "instrument", "words": []},
{"segment": 9, "transcript": "Excuse me, how do I get to the city hall?", "speaker": "speaker_3", "confidence": 0.99, "language": "en", "silence_before_ms": 80, "audio_type": "speech", "words": [{"word": "Excuse", "start_ms": 80, "dur_ms": 350, "phase_lr_deg": 0.0}, {"word": "me", "start_ms": 460, "dur_ms": 150, "phase_lr_deg": 0.0}, {"word": "how", "start_ms": 710, "dur_ms": 210, "phase_lr_deg": 0.0}, {"word": "do", "start_ms": 940, "dur_ms": 120, "phase_lr_deg": 0.0}, {"word": "I", "start_ms": 1080, "dur_ms": 100, "phase_lr_deg": 0.0}, {"word": "get", "start_ms": 1200, "dur_ms": 180, "phase_lr_deg": 0.0}, {"word": "to", "start_ms": 1400, "dur_ms": 110, "phase_lr_deg": 0.0}, {"word": "the", "start_ms": 1530, "dur_ms": 120, "phase_lr_deg": 0.0}, {"word": "city", "start_ms": 1670, "dur_ms": 250, "phase_lr_deg": 0.0}, {"word": "hall", "start_ms": 1950, "dur_ms": 310, "phase_lr_deg": 0.0}]},
{"segment": 10, "transcript": "Take subway line two and get off at City Hall station.", "speaker": "speaker_4", "confidence": 0.98, "language": "en", "silence_before_ms": 110, "audio_type": "speech", "words": [{"word": "Take", "start_ms": 110, "dur_ms": 250, "phase_lr_deg": 0.0}, {"word": "subway", "start_ms": 410, "dur_ms": 380, "phase_lr_deg": 0.0}, {"word": "line", "start_ms": 820, "dur_ms": 200, "phase_lr_deg": 0.0}, {"word": "two", "start_ms": 1050, "dur_ms": 250, "phase_lr_deg": 0.0}, {"word": "and", "start_ms": 1390, "dur_ms": 150, "phase_lr_deg": 0.0}, {"word": "get", "start_ms": 1570, "dur_ms": 180, "phase_lr_deg": 0.0}, {"word": "off", "start_ms": 1780, "dur_ms": 190, "phase_lr_deg": 0.0}, {"word": "at", "start_ms": 2000, "dur_ms": 110, "phase_lr_deg": 0.0}, {"word": "City", "start_ms": 2130, "dur_ms": 220, "phase_lr_deg": 0.0}, {"word": "Hall", "start_ms": 2380, "dur_ms": 200, "phase_lr_deg": 0.0}, {"word": "station", "start_ms": 2610, "dur_ms": 480, "phase_lr_deg": 0.0}]},
{"segment": 11, "transcript": "(music)", "speaker": null, "confidence": 0.0, "language": null, "silence_before_ms": 0, "audio_type": "music", "words": []},
{"segment": 12, "transcript": "すみません、この近くに銀行はありますか。", "speaker": "speaker_5", "confidence": 0.99, "language": "ja", "silence_before_ms": 90, "audio_type": "speech", "words": [{"word": "すみません", "start_ms": 90, "dur_ms": 610, "phase_lr_deg": 0.0}, {"word": "この", "start_ms": 780, "dur_ms": 210, "phase_lr_deg": 0.0}, {"word": "近くに", "start_ms": 1020, "dur_ms": 400, "phase_lr_deg": 0.0}, {"word": "銀行は", "start_ms": 1500, "dur_ms": 390, "phase_lr_deg": 0.0}, {"word": "ありますか", "start_ms": 1950, "dur_ms": 500, "phase_lr_deg": 0.0}]},
{"segment": 13, "transcript": "はい、あそこのコンビニの隣にありますよ。", "speaker": "speaker_6", "confidence": 0.99, "language": "ja", "silence_before_ms": 120, "audio_type": "speech", "words": [{"word": "はい", "start_ms": 120, "dur_ms": 250, "phase_lr_deg": 0.0}, {"word": "あそこの", "start_ms": 580, "dur_ms": 410, "phase_lr_deg": 0.0}, {"word": "コンビニの", "start_ms": 1050, "dur_ms": 490, "phase_lr_deg": 0.0}, {"word": "隣に", "start_ms": 1610, "dur_ms": 320, "phase_lr_deg": 0.0}, {"word": "ありますよ", "start_ms": 2010, "dur_ms": 550, "phase_lr_deg": 0.0}]},
{"segment": 14, "transcript": "(noise)", "speaker": null, "confidence": 0.0, "language": null, "silence_before_ms": 0, "audio_type": "noise", "words": []},
{"segment": 15, "transcript": "(noise - microphone rustle)", "speaker": null, "confidence": 0.0, "language": null, "silence_before_ms": 0, "audio_type": "noise", "words": []},
{"segment": 16, "transcript": "(piano music)", "speaker": null, "confidence": 0.0, "language": null, "silence_before_ms": 0, "audio_type": "instrument", "words": []},
{"segment": 17, "transcript": "자, 이제 다음 단어를 듣고 큰 소리로 따라 읽어 보세요.", "speaker": "speaker_1", "confidence": 0.99, "language": "ko", "silence_before_ms": 150, "audio_type": "speech", "words": [{"word": "자", "start_ms": 150, "dur_ms": 190, "phase_lr_deg": 0.0}, {"word": "이제", "start_ms": 410, "dur_ms": 250, "phase_lr_deg": 0.0}, {"word": "다음", "start_ms": 710, "dur_ms": 280, "phase_lr_deg": 0.0}, {"word": "단어를", "start_ms": 1020, "dur_ms": 380, "phase_lr_deg": 0.0}, {"word": "듣고", "start_ms": 1450, "dur_ms": 310, "phase_lr_deg": 0.0}, {"word": "큰", "start_ms": 1820, "dur_ms": 180, "phase_lr_deg": 0.0}, {"word": "소리로", "start_ms": 2030, "dur_ms": 410, "phase_lr_deg": 0.0}, {"word": "따라", "start_ms": 2490, "dur_ms": 310, "phase_lr_deg": 0.0}, {"word": "읽어", "start_ms": 2850, "dur_ms": 290, "phase_lr_deg": 0.0}, {"word": "보세요", "start_ms": 3170, "dur_ms": 430, "phase_lr_deg": 0.0}]},
{"segment": 18, "transcript": "학교.", "speaker": "speaker_2", "confidence": 1.0, "language": "ko", "silence_before_ms": 300, "audio_type": "speech", "words": [{"word": "학교", "start_ms": 300, "dur_ms": 550, "phase_lr_deg": 0.0}]},
{"segment": 19, "transcript": "(silence)", "speaker": null, "confidence": 0.0, "language": null, "silence_before_ms": 0, "audio_type": "noise", "words": []},
{"segment": 20, "transcript": "선생님.", "speaker": "speaker_2", "confidence": 1.0, "language": "ko", "silence_before_ms": 280, "audio_type": "speech", "words": [{"word": "선생님", "start_ms": 280, "dur_ms": 610, "phase_lr_deg": 0.0}]},
{"segment": 21, "transcript": "(silence)", "speaker": null, "confidence": 0.0, "language": null, "silence_before_ms": 0, "audio_type": "noise", "words": []},
{"segment": 22, "transcript": "학생.", "speaker": "speaker_2", "confidence": 1.0, "language": "ko", "silence_before_ms": 310, "audio_type": "speech", "words": [{"word": "학생", "start_ms": 310, "dur_ms": 520, "phase_lr_deg": 0.0}]},
{"segment": 23, "transcript": "(silence)", "speaker": null, "confidence": 0.0, "language": null, "silence_before_ms": 0, "audio_type": "noise", "words": []},
{"segment": 24, "transcript": "공부.", "speaker": "speaker_2", "confidence": 1.0, "language": "ko", "silence_before_ms": 290, "audio_type": "speech", "words": [{"word": "공부", "start_ms": 290, "dur_ms": 580, "phase_lr_deg": 0.0}]},
{"segment": 25, "transcript": "(noise)", "speaker": null, "confidence": 0.0, "language": null, "silence_before_ms": 0, "audio_type": "noise", "words": []},
{"segment": 26, "transcript": "친구.", "speaker": "speaker_2", "confidence": 1.0, "language": "ko", "silence_before_ms": 305, "audio_type": "speech", "words": [{"word": "친구", "start_ms": 305, "dur_ms": 540, "phase_lr_deg": 0.0}]},
{"segment": 27, "transcript": "(silence)", "speaker": null, "confidence": 0.0, "language": null, "silence_before_ms": 0, "audio_type": "noise", "words": []},
{"segment": 28, "transcript": "사랑.", "speaker": "speaker_2", "confidence": 1.0, "language": "ko", "silence_before_ms": 295, "audio_type": "speech", "words": [{"word": "사랑", "start_ms": 295, "dur_ms": 560, "phase_lr_deg": 0.0}]},
{"segment": 29, "transcript": "(silence)", "speaker": null, "confidence": 0.0, "language": null, "silence_before_ms": 0, "audio_type": "noise", "words": []},
{"segment": 30, "transcript": "행복.", "speaker": "speaker_2", "confidence": 1.0, "language": "ko", "silence_before_ms": 300, "audio_type": "speech", "words": [{"word": "행복", "start_ms": 300, "dur_ms": 530, "phase_lr_deg": 0.0}]},
{"segment": 31, "transcript": "(noise - paper shuffle)", "speaker": null, "confidence": 0.0, "language": null, "silence_before_ms": 0, "audio_type": "noise", "words": []},
{"segment": 32, "transcript": "아주 잘하셨습니다. 이번에는 문장 연습을 해 보겠습니다.", "speaker": "speaker_1", "confidence": 0.99, "language": "ko", "silence_before_ms": 110, "audio_type": "speech", "words": [{"word": "아주", "start_ms": 110, "dur_ms": 310, "phase_lr_deg": 0.0}, {"word": "잘하셨습니다", "start_ms": 480, "dur_ms": 780, "phase_lr_deg": 0.0}, {"word": "이번에는", "start_ms": 1500, "dur_ms": 480, "phase_lr_deg": 0.0}, {"word": "문장", "start_ms": 2040, "dur_ms": 310, "phase_lr_deg": 0.0}, {"word": "연습을", "start_ms": 2390, "dur_ms": 400, "phase_lr_deg": 0.0}, {"word": "해", "start_ms": 2840, "dur_ms": 150, "phase_lr_deg": 0.0}, {"word": "보겠습니다", "start_ms": 3020, "dur_ms": 610, "phase_lr_deg": 0.0}]},
{"segment": 33, "transcript": "저는 한국어를 공부합니다.", "speaker": "speaker_2", "confidence": 0.99, "language": "ko", "silence_before_ms": 130, "audio_type": "speech", "words": [{"word": "저는", "start_ms": 130, "dur_ms": 310, "phase_lr_deg": 0.0}, {"word": "한국어를", "start_ms": 490, "dur_ms": 490, "phase_lr_deg": 0.0}, {"word": "공부합니다", "start_ms": 1030, "dur_ms": 710, "phase_lr_deg": 0.0}]},
{"segment": 34, "transcript": "(silence)", "speaker": null, "confidence": 0.0, "language": null, "silence_before_ms": 0, "audio_type": "noise", "words": []},
{"segment": 35, "transcript": "한국 노래를 좋아해요.", "speaker": "speaker_2", "confidence": 0.99, "language": "ko", "silence_before_ms": 140, "audio_type": "speech", "words": [{"word": "한국", "start_ms": 140, "dur_ms": 310, "phase_lr_deg": 0.0}, {"word": "노래를", "start_ms": 500, "dur_ms": 410, "phase_lr_deg": 0.0}, {"word": "좋아해요", "start_ms": 960, "dur_ms": 580, "phase_lr_deg": 0.0}]},
{"segment": 36, "transcript": "(silence)", "speaker": null, "confidence": 0.0, "language": null, "silence_before_ms": 0, "audio_type": "noise", "words": []},
{"segment": 37, "transcript": "한국 음식이 맵지만 맛있어요.", "speaker": "speaker_2", "confidence": 0.98, "language": "ko", "silence_before_ms": 125, "audio_type": "speech", "words": [{"word": "한국", "start_ms": 125, "dur_ms": 320, "phase_lr_deg": 0.0}, {"word": "음식이", "start_ms": 495, "dur_ms": 390, "phase_lr_deg": 0.0}, {"word": "맵지만", "start_ms": 935, "dur_ms": 430, "phase_lr_deg": 0.0}, {"word": "맛있어요", "start_ms": 1415, "dur_ms": 550, "phase_lr_deg": 0.0}]},
{"segment": 38, "transcript": "(silence)", "speaker": null, "confidence": 0.0, "language": null, "silence_before_ms": 0, "audio_type": "noise", "words": []},
{"segment": 39, "transcript": "가족들과 함께 여행을 가고 싶어요.", "speaker": "speaker_2", "confidence": 0.98, "language": "ko", "silence_before_ms": 130, "audio_type": "speech", "words": [{"word": "가족들과", "start_ms": 130, "dur_ms": 550, "phase_lr_deg": 0.0}, {"word": "함께", "start_ms": 730, "dur_ms": 310, "phase_lr_deg": 0.0}, {"word": "여행을", "start_ms": 1090, "dur_ms": 420, "phase_lr_deg": 0.0}, {"word": "가고", "start_ms": 1560, "dur_ms": 280, "phase_lr_deg": 0.0}, {"word": "싶어요", "start_ms": 1890, "dur_ms": 450, "phase_lr_deg": 0.0}]},
{"segment": 40, "transcript": "(silence)", "speaker": null, "confidence": 0.0, "language": null, "silence_before_ms": 0, "audio_type": "noise", "words": []},
{"segment": 41, "transcript": "오늘 저녁에 같이 밥 먹을까요?", "speaker": "speaker_1", "confidence": 0.99, "language": "ko", "silence_before_ms": 100, "audio_type": "speech", "words": [{"word": "오늘", "start_ms": 100, "dur_ms": 290, "phase_lr_deg": 0.0}, {"word": "저녁에", "start_ms": 420, "dur_ms": 390, "phase_lr_deg": 0.0}, {"word": "같이", "start_ms": 860, "dur_ms": 300, "phase_lr_deg": 0.0}, {"word": "밥", "start_ms": 1210, "dur_ms": 180, "phase_lr_deg": 0.0}, {"word": "먹을까요", "start_ms": 1440, "dur_ms": 510, "phase_lr_deg": 0.0}]},
{"segment": 42, "transcript": "(instrumental music - upbeat)", "speaker": null, "confidence": 0.0, "language": null, "silence_before_ms": 0, "audio_type": "instrument", "words": []},
{"segment": 43, "transcript": "죄송하지만 오늘은 약속이 있어서 안 돼요.", "speaker": "speaker_2", "confidence": 0.98, "language": "ko", "silence_before_ms": 120, "audio_type": "speech", "words": [{"word": "죄송하지만", "start_ms": 120, "dur_ms": 610, "phase_lr_deg": 0.0}, {"word": "오늘은", "start_ms": 780, "dur_ms": 380, "phase_lr_deg": 0.0}, {"word": "약속이", "start_ms": 1210, "dur_ms": 390, "phase_lr_deg": 0.0}, {"word": "있어서", "start_ms": 1650, "dur_ms": 350, "phase_lr_deg": 0.0}, {"word": "안", "start_ms": 2050, "dur_ms": 150, "phase_lr_deg": 0.0}, {"word": "돼요", "start_ms": 2250, "dur_ms": 310, "phase_lr_deg": 0.0}]},
{"segment": 44, "transcript": "(noise)", "speaker": null, "confidence": 0.0, "language": null, "silence_before_ms": 0, "audio_type": "noise", "words": []},
{"segment": 45, "transcript": "(fast-paced speech - indistinguishable chatter)", "speaker": null, "confidence": 0.5, "language": "ko", "silence_before_ms": 100, "audio_type": "mixed", "words": []},
{"segment": 46, "transcript": "(music - pop song intro)", "speaker": null, "confidence": 0.0, "language": null, "silence_before_ms": 0, "audio_type": "music", "words": []},
{"segment": 47, "transcript": "네, 그럼 다음에 같이 먹어요.", "speaker": "speaker_1", "confidence": 0.99, "language": "ko", "silence_before_ms": 115, "audio_type": "speech", "words": [{"word": "네", "start_ms": 115, "dur_ms": 180, "phase_lr_deg": 0.0}, {"word": "그럼", "start_ms": 350, "dur_ms": 250, "phase_lr_deg": 0.0}, {"word": "다음에", "start_ms": 650, "dur_ms": 350, "phase_lr_deg": 0.0}, {"word": "같이", "start_ms": 1050, "dur_ms": 290, "phase_lr_deg": 0.0}, {"word": "먹어요", "start_ms": 1390, "dur_ms": 410, "phase_lr_deg": 0.0}]},
{"segment": 48, "transcript": "(instrumental music - ending theme)", "speaker": null, "confidence": 0.0, "language": null, "silence_before_ms": 0, "audio_type": "instrument", "words": []},
{"segment": 49, "transcript": "수고하셨습니다. 이것으로 오늘의 학습을 마치겠습니다.", "speaker": "speaker_1", "confidence": 0.99, "language": "ko", "silence_before_ms": 120, "audio_type": "speech", "words": [{"word": "수고하셨습니다", "start_ms": 120, "dur_ms": 750, "phase_lr_deg": 0.0}, {"word": "이것으로", "start_ms": 1000, "dur_ms": 480, "phase_lr_deg": 0.0}, {"word": "오늘의", "start_ms": 1530, "dur_ms": 390, "phase_lr_deg": 0.0}, {"word": "학습을", "start_ms": 1970, "dur_ms": 380, "phase_lr_deg": 0.0}, {"word": "마치겠습니다", "start_ms": 2400, "dur_ms": 710, "phase_lr_deg": 0.0}]},
{"segment": 50, "transcript": "감사합니다. 안녕히 계세요.", "speaker": "speaker_2", "confidence": 0.99, "language": "ko", "silence_before_ms": 130, "audio_type": "speech", "words": [{"word": "감사합니다", "start_ms": 130, "dur_ms": 650, "phase_lr_deg": 0.0}, {"word": "안녕히", "start_ms": 950, "dur_ms": 390, "phase_lr_deg": 0.0}, {"word": "계세요", "start_ms": 1390, "dur_ms": 450, "phase_lr_deg": 0.0}]}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 07:11:00*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=15840  time=07:08:41  uptime=8991s  mem=739MB  system_processes=410"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_070210.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 621-352-316-312-573-357-537-257-723-632-576-726-760-130-321-312-370-256-207-162-352-576-537-316-356-756-360-675-657-301-325-754_V.mp3)
  Segment 2: 1576ms ~ 6220ms (file: 321-102-231-102-230-213-132-130-236-263-320-137-172-126-123-213-230-230-527-352-130-103-216-257-230-571-532-123-321-120-231-527-132-301-310-103-326-263-310-320-312-253-326-321-3_W.mp3)
  Segment 3: 7220ms ~ 16904ms (file: 132-574-756-574-574-537-752-172-537-762-502-625-345-537-702-276-413-754-507-573-571-526-705-673-760-576-750-625-765-560-576-231-235-312-301-576-653-367-162-132-176-231-713 123-1_V.mp3)
  Segment 4: 17904ms ~ 18480ms (file: 756-506-567-543-267-263-251-726-756-162-527-507-571 672-132-750-576-567-567-576-725-175-720-521-576-132-231-316-256-567-275-217-751-327-275-574-315-372-726-527-721-265-263-702-3_V.mp3)
  Segment 5: 19480ms ~ 20056ms (file: 326-267-327-623-267-321-207-215-657-520-275-327-172-657-130-156-576-576-153-612-675-756-657-675-675-756-756_W.mp3)
  Segment 6: 21056ms ~ 21632ms (file: 756-763-562-326-702-326-263-235-362-263-712-527-725-326-752-532-123-627-135-705-572-723-312-236-356-730-236-532-503-672-613-261-326-235-273-572-521-562-325-275-725-357-527-672-3_W.mp3)
  Segment 7: 22632ms ~ 32388ms (file: 672-163-160-716-263-231-152-107-167-102-716-312-623-276-310-765-267-263-236-127-102-127-312-103-263-103-103-316-236-321-263-261-326-762-273-213-231-213-132-103-216-120-765-261-2_W.mp3)
  Segment 8: 33388ms ~ 42064ms (file: 753-573-326-217-213-371-751-102-132-732-132-325-765-236-671-132-203-725-312-257-527-756-260-231-271-132-267-301-263-237-620-627-326-573-327-132-103-103-310-132-312-263-625-526-1_W.mp3)
  Segment 9: 43064ms ~ 52064ms (file: 754-571-213-570-275-257-215 271-206-132-302-756-276-321-103-260-310-130-136-206-312-132-173-107-710-107-312-236-123-260-236-127-750-725-260-617-172-320-216-302-175-675-567-675-1_V.mp3)
  Segment 10: 53064ms ~ 60516ms (file: 302-273-156-137-107-175-712-627-327-137-132-210-201-320-301-312-576-627-130-302-320-201-302-231-236-203-206-207-312-132-317-326-320-301-210-213-132-310-260-123-132-321-130-176-5_V.mp3)
  Segment 11: 61516ms ~ 62092ms (file: 527-362-236-650-527-726-275-253-753-261-267-127-527-672-621-762-627-765-572-276-326-312-567-706-236-675-671-316-301_V.mp3)
  Segment 12: 63092ms ~ 68312ms (file: 312-321-216-572-143-103-527-231-752-321-230-257-756-326-571-163-163-312-310-312-130-213-132-136-316-316-301-310-316-136-271-326-136-125-130-536-106-631-362-361-362-362-263-362-3_W.mp3)
  Segment 13: 69312ms ~ 69888ms (file: 135-326-326-362-372-327-321 763_W.mp3)
  Segment 14: 70888ms ~ 71464ms (file: 567-752-567-576-751-156-647-672-175-375-376-716-317-712-176-627-625-617-625-613-672-675-617-672-761-172-167-637-612-762-637-672-627-673-637-637-671-762-372-165-671-756-765-756-7_W.mp3)
  Segment 15: 72464ms ~ 73040ms (file: 670-672-237-672-135-574-571-657-570-765-751-547-576_W.mp3)
  Segment 16: 74040ms ~ 82896ms (file: 132-103-164-312-203-613-130-162-301-102-312-207-230-176-310-310-276-203-103-310-130-230-237-132-107-130-763-321-675-206-371-203-267-132-103-321-301-103-201-602-261-123-130-751-3_W.mp3)
  Segment 17: 83896ms ~ 84472ms (file: 217-132-502-623-312-623-573-365-357-235-572-517-257-726-725-627-526-153-607-162-127-265-137-367-265-235-507-567-725-573-756-750-627-725-123-132-725-217-125-765-156-576-367-602 7_V.mp3)
  Segment 18: 85472ms ~ 86048ms (file: 175-175-751-175-157-715-157-157-571-157-517-571-517-576-571-576-517-571-517-571 175-715-571-157-157-175-157-157-517-571-517-517-157-156-571-157-156-517-157-567-175-571-751 103-1_W.mp3)
  Segment 19: 87048ms ~ 87624ms (file: 130-351-675-356-536-316-630-312-310-162-103-763 136-163-163-165-136-136-136-176-612-162-172-216-162-162-367-167-621-162-126-216-126-162-126-261-612-261-621-612-756-123-236-730-6_W.mp3)
  Segment 20: 88624ms ~ 94060ms (file: 106-750-132-321-132-123-213-213-132-120-321-237-123-125-213-765-675-675-675-675-765-675-756-160-321-321-321-237-237-231-123-321-321-276-217-123-152-275-173-132-103-567-672-320-3_W.mp3)
  Segment 21: 95060ms ~ 102152ms (file: 167-325-736-267-726-375-263-276-672-574-351-537-102-123-102-132-125-263-236-237-760-637-320-236-562-235-321-517-713-237-125-103-271-231-263-267-157-310-103-321-261-132-132-763-2_W.mp3)
  Segment 22: 103152ms ~ 109488ms (file: 721-716-167-231-103-310-317-375-673-306-756-756-275-516-167-756-657-327-273-561-310-531-271-321-726-123-106-310-361-375-735-137-327-327-136-763-327-160-106-273-127-103-170-103-3_W.mp3)
  Segment 23: 110488ms ~ 116068ms (file: 271-601-620-654-645-267-264-260-107-106-162-216-213-213-132-217-217-132-127-721-170-672-572-726-103-120-310-137-126-627-107-321-725-175-752-130-301-637-672-672-672-672-672-723-2_W.mp3)
  Segment 24: 117068ms ~ 117644ms (file: 170-170-170-170-175-176-715-175-715-175-716-715-761 750-157-176-107-715-701-170-170-751-175-175-175-176-175-175-176-175-175-176-715-175-176-175-715 105-175-103-710-170-173-170-1_V.mp3)
  Segment 25: 118644ms ~ 119220ms (file: 765-675-635-765-756-576-675-765-756-756-675-375-765-675-756-657-657-756-756-756-567-756-753-576-756-657-576-756-576-547-567-756-753-754-526-365-350-163-261-361-175-235-546-546-5_W.mp3)
  Segment 26: 120220ms ~ 120796ms (file: 625-657-526-562-265-365-265-625-627-625-672-256-657-562-657-657-756-657-657-625-627-672-675-637-673-367-673-673-637-736-763-657-671-736-765-763-673-675-765-625-671-627-762-612-6_W.mp3)
  Segment 27: 121796ms ~ 122372ms (file: 217-137-365-527-231-735-362-372-276-572-230-312-752-725-732-275-573-752-756-312-576-352-371-137-317-325-352-231-321-372-762-672-576-125-705-675-750-570-325-356-317-673-702-253-7_V.mp3)
  Segment 28: 123372ms ~ 133560ms (file: 574-213-753-756-207-210-752-216-217-102-754-537-573-310-103-132-756-756-130-310-301-310-210-752-167-132-102-130-210-130-120-132-320-203-230-572-236-765-310-206-203-321-301-201-1_W.mp3)
  Segment 29: 134560ms ~ 143956ms (file: 301-130-726-765-537-130-132-362-231-327-321-103-736-236-513-357-762-762-326-312-103-261-231-123-213-132-213-327-231-276-216-213-312-130-301-176-371-237-762-310-362-103-132-573-2_W.mp3)
  Segment 30: 144956ms ~ 145532ms (file: 103-132-761-372-637-625-132-132-130 570-527-251-756-706-175 124 136-173-170-176-762-163 361-132-123-627-721-723-271-732-715-572-260-260-216-261-213-672-326-312-321-731-613-236-6_V.mp3)
  Segment 31: 146532ms ~ 147108ms (file: 753-753-573-571-175-570-571-756-571-517-571-570-750-570-571-571-517-571-571-756-175 571-570-570-571-570-571-751-715-715-750 103-715-175-715-715-715-715-750-715-715-710 570-570-1_W.mp3)
  Segment 32: 148108ms ~ 156316ms (file: 675-126-765-765-726-276-702-765-106-716-654-617-607-576-276-276-273-173-103-702-102-276-627-765-267-765-267-207-756-273-267-756-105-215-167-267-106-176-627-267-237-276-260-123-1_W.mp3)
  Segment 33: 157316ms ~ 167612ms (file: 602-675-176-630-167-106-167-316-103-267-217-320-107-137-206-203-627-371-260-203-267-216-362-136-103-107-310-120-173-176-756-321-301-230-310-132-103-137-176-173-132-713-260-160-1_V.mp3)
  Segment 34: 168612ms ~ 174840ms (file: 213-620-235-207-263-217-120-310-720-201-312-530-213-132-130-270-260-701-170-136-231-716-760-126-312-213-201-507-726-762-126-726-107-157-316-312-236-631-320-652-301-301-263-627-5_V.mp3)
  Segment 35: 175840ms ~ 176416ms (file: 153-531-756-715-361-312-317-316-316-362-317-312-673-657-367-635-375-765-657-673-675-376_W.mp3)
  Segment 36: 177416ms ~ 183284ms (file: 231-312-765-103-631-273-302-756-752-217-763-263-735-731-721-627-102-271-103-152-203-261-213-512-312-571-267-210-267-206-670-216-263-371-673-627-132-213-301-207-765-103-607-201-2_V_stt_dole.mp3)
  Segment 37: 184284ms ~ 184860ms (file: 506-516-756-561-567-567-657-576-751-567-765-763-736-756-567-576-576-576-653-715-756-571-625-362-725-736-217-132-136-316-371-361-136-123-136-174-217-367-132-671-136-132-136_V.mp3)
  Segment 38: 185860ms ~ 191872ms (file: 135-312-103-310-231-315-361-312-132-123-126-275-321-312-213-231-236-276-270-312-762-267-260-527-574-207-256-120-752-753-327-230-752-756-752-756-325-573-201-231-725-752-120-312-5_W_stt_the_thing_that_is_now_that_the_chances_o.mp3)
  Segment 39: 192872ms ~ 201584ms (file: 203-263-312-601-132-316-760-231-213-352-130-602-260-260-261-301-132-136-157-132-136-132-620-670-607-670-326-361-236-261-236-321-203-230-320-723-126-130-103-263-320-301-302-231-2_W.mp3)
  Segment 40: 202584ms ~ 210180ms (file: 132-163-106-620-652-216-261-542-612-261-610-720-207-127-276-671-127-721-176-762-103-671-675-216-257-627-726-726-160-271-627-725-627-752-721-126-132-213-216-201-261-276-267-267-7_W.mp3)
  Segment 41: 211180ms ~ 216328ms (file: 230-263-162-206-216-230-123-132-137-173-176-213-231-753-203-203-231-231-321-132-201-203-120-206-327-372-361-163-361-762-256-263-612-231-201-572-136-507-632-356-326-316-752-371-5_W.mp3)
  Segment 42: 217328ms ~ 217904ms (file: 765-756-216-321-132-312-132-132-312-132-132-312-301-312-137-130-137-137-102_W.mp3)
  Segment 43: 218904ms ~ 219480ms (file: 752-236-536-261-371-132-765-756-576-756-756-574-745-257-261-265-512-175-276-623-572-752-567-572-570-321-752 507-725-267-217-762-257-512-571-621-231-165-236-273-631-627-732-263-6_V.mp3)
  Segment 44: 220480ms ~ 221056ms (file: 715-731-137-631-752-731-276-756-726-162-706-163-167-516-231-750-521-172-231-273-215-512-216-507-321-625-276-762-256-527-567-157-275-765-236-576-725-721-261-756-321-521-216-352-1_V.mp3)
  Segment 45: 222056ms ~ 226340ms (file: 571-517-325-157-507-576-567-504-251-251-320-203-310-327-276-672-321-231-527-361-573-573-573-352-253-527-235-725-527-201-527-152-270-275-752-765-230-510-206-517-130-103-130-123-1_W.mp3)
  Segment 46: 227340ms ~ 234864ms (file: 263-236-236-320-321-231-230-231-230-213-230-203-230-230-201-203-230-302-302-275-275-130-312-270-321-123-207-573-572-170-213-275-257-321-321-201-230-231-102-102-213-213-261-213-2_W.mp3)
  Segment 47: 235864ms ~ 236440ms (file: 610-167-301-310-312-301-312-312-632-325-135_W.mp3)
  Segment 48: 237440ms ~ 238016ms (file: 276-702-760-253-267-263-720-623-236-173-276-276-253-752-237-753-125-572-725-375-762-574-516-213-516-235-571-751-235-725-527-326 627-361-236-607-675-675-657-361-167-317-763-371-1_V.mp3)
  Segment 49: 239016ms ~ 239592ms (file: 673-673-637-617-673-632-327-357-367-732-327-756-752-753_V.mp3)
  Segment 50: 240592ms ~ 248404ms (file: 613-617-617-163-613-631-671-361-761-263-321-532-567-132-172-621-102-130-312-132-237-123-102-263-231-216-235-621-263-265-263-326-236-236-236-267-627-756-326-103-310-712-312-120-1_W.mp3)
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
> **Time**: 2026-04-06 07:11:45
> **AI**: gemini

---

## Response

AnalysisAnalysisGemini의 응답[
{
"segment": 1,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 2,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 3,
"transcript": "6%나 급락했던 상황인데 이날 대한광통신 일봉을 보면 양봉으로 7% 상승해서 마감했습니다 일반적으로 이렇게 윗꼬리",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "6%나", "start_ms": 110, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "급락했던", "start_ms": 800, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "상황인데", "start_ms": 1550, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "이날", "start_ms": 2350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "대한광통신", "start_ms": 2800, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "일봉을", "start_ms": 3700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "보면", "start_ms": 4250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "양봉으로", "start_ms": 4650, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "7%", "start_ms": 5350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "상승해서", "start_ms": 5700, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "마감했습니다", "start_ms": 6500, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "일반적으로", "start_ms": 7350, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 8100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "윗꼬리", "start_ms": 8600, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "나기 시작했습니다 교복의 필요성에 대한 여론이 자연스럽게 형성됐고 정치인들이 이에 힘을 보탰기 때문인데요 교복을 입고",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "나기", "start_ms": 90, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "시작했습니다", "start_ms": 450, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "교복의", "start_ms": 1300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "필요성에", "start_ms": 1850, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "대한", "start_ms": 2550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "여론이", "start_ms": 3000, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "자연스럽게", "start_ms": 3600, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "형성됐고", "start_ms": 4400, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "정치인들이", "start_ms": 5100, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "이에", "start_ms": 5850, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "힘을", "start_ms": 6150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "보탰기", "start_ms": 6550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "때문인데요", "start_ms": 7100, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "교복을", "start_ms": 7850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "입고", "start_ms": 8350, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "Nice to meet you. 안녕하세요. Welcome. Nice to meet you.",
"speaker": "mixed_crowd",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 150,
"audio_type": "mixed",
"words": [
{"word": "Nice", "start_ms": 150, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "to", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "meet", "start_ms": 550, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "you.", "start_ms": 800, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "안녕하세요.", "start_ms": 1200, "dur_ms": 800, "phase_lr_deg": 10.0},
{"word": "Welcome.", "start_ms": 2100, "dur_ms": 450, "phase_lr_deg": -15.0},
{"word": "Nice", "start_ms": 2600, "dur_ms": 250, "phase_lr_deg": 5.0},
{"word": "to", "start_ms": 2850, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "meet", "start_ms": 3000, "dur_ms": 200, "phase_lr_deg": 5.0},
{"word": "you.", "start_ms": 3250, "dur_ms": 350, "phase_lr_deg": 5.0}
]
},
{
"segment": 6,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 7,
"transcript": "할 수 있겠다라는 용기가 이제 생겨나지.",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "할", "start_ms": 120, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 400, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "있겠다라는", "start_ms": 650, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "용기가", "start_ms": 1500, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 2100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "생겨나지.", "start_ms": 2550, "dur_ms": 750, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "우와 6미터에 달하는 (noise) 잠깐만요 감사합니다 와 와",
"speaker": "mixed_crowd",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "mixed",
"words": [
{"word": "우와", "start_ms": 80, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "6미터에", "start_ms": 500, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "달하는", "start_ms": 1150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "잠깐만요", "start_ms": 2200, "dur_ms": 650, "phase_lr_deg": -10.0},
{"word": "감사합니다", "start_ms": 2900, "dur_ms": 750, "phase_lr_deg": 10.0},
{"word": "와", "start_ms": 3700, "dur_ms": 300, "phase_lr_deg": 5.0},
{"word": "와", "start_ms": 4100, "dur_ms": 400, "phase_lr_deg": -5.0}
]
},
{
"segment": 9,
"transcript": "경기 안의 고강도 조사로 진행될 전망입니다 특히 특별사법경찰 인력도 투입돼 현장에서 불법 행위를 직접 수사하고 필요시",
"speaker": "speaker_4",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "

---
*Generated by WKAppBot ask gemini — 2026-04-06 07:11:45*
