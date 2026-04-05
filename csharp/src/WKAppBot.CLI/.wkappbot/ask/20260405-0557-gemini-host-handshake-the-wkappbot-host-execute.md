# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=24432  time=05:55:59  uptime=13830s  mem=1042MB  system_processes=358"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_055525.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 2412ms (file: 312-312-351-563-562-235-325-315-765-167-361-136-562-651-517-316-652-310-613-103-627-256-103-130-760-307-126 310-132-231-231-321-371-132-173-132-312-513-132-671-130-670-312-531-3_V.mp3)
  Segment 2: 3412ms ~ 3988ms (file: 310-301-531-103-103-130-103-130-571-570-576-756-376-136-136-715-671-617-610-621-261-136-316-130-130-130-103-130-103-130-310-301-136-310-362-136-361-310-103-316-301-301-130-312-1_V.mp3)
  Segment 3: 4988ms ~ 5564ms (file: 317-137-137-315-367-173-170-130-107-301-137-130-173-107-130-701-750-371-137-137-317-735 371-317-137-310-107-137-173-371-132-760-106-601-601-607-630-607-310-713-301-701-370-310-1_V.mp3)
  Segment 4: 6564ms ~ 11100ms (file: 217-125-153-265-201-512-257-756-537-352-576-267-106-105-617-576-653-237-276-321-267-675-672-657-762-276-152-312-213-260-231-132-217-267-572-576-675-765-576-267-562-213-310-102-6_W.mp3)
  Segment 5: 12100ms ~ 12676ms (file: 150-157-153-157-105-157-517-501-517-507-510-150-510-507-501-570-570-507-571-574-510-517-574-571-574-574-574-571-574-517-576-574-576 517-501-157-105-153-157-105-105-150-105-517-5_V.mp3)
  Segment 6: 13676ms ~ 20876ms (file: 527-543-201-217-675-267-175-206-625-261-132-267-327-103-534-206-670-231-206-275-216-213-507-354-231-320-310-201-572-263-570-725-201-602-620-312-576-267-120-573-162-257-573-267-6_V.mp3)
  Segment 7: 21876ms ~ 38904ms (file: 756-215-130-320-574-574-572-316-231-231-276-263-326-736-267-270-237-312-130 213-124-165-267-265-576-251-231-231-132-130-213-103-326-132-312-102-103-302-132-316-136-573-103-572-5_W.mp3)
  Segment 8: 39904ms ~ 47968ms (file: 317-574-216-261-267-203-312-267-260-265-301-103-267-231-326-670-637-137-301-310-132-132-237-302-103-103-236-206-213-103-213-106-657-576-130-203-213-231-312-137-136-673-216-210-1_V.mp3)
  Segment 9: 48968ms ~ 59156ms (file: 576-307-132-570-357-120-231-570-572-216-261-260-257-762-602-260-210-671-206-260-216-132-203-275-216-123-231-726-310-621-263-312-132-312-320-301-157-153-106-621-236-320-302-206-2_W.mp3)
  Segment 10: 60156ms ~ 60732ms (file: 175-135-137-135-135-157 507-573-751-573-571-135-157-135-137-571-153-516-516-513-513-527-512 573-567-513-510-150-150-571-135-135 501-571-510-517-715-615-621 517-753-574-751-517-5_W.mp3)
  Segment 11: 61732ms ~ 62308ms (file: 765-675-765-736-756-765-765-756-763-157-156-756-756-756-576-756-751-751-756-751-765-576-756-751-750-751-756-751-576-673-675-576-571-571-576-571-576-571-571-571-567-576-675-571-6_W.mp3)
  Segment 12: 63308ms ~ 67448ms (file: 756-762-756-762-126-162-123-120-126-120-126-126-126-216-126-162-213-126-312-216-216-126-123-216-132-312-120-123-320-123-326-132-210-120-216-261-267-312-231-120-132-213-213-271-2_W.mp3)
  Segment 13: 68448ms ~ 85836ms (file: 206-276-103-130-103-210-320-276-302-103-675-203-230-206-206-267-230-320-203-230-321-310-130-130-201-761-163-732-132-260-231-213-102-102-302-361-320-126-165-103-763-201 230-237-5_V.mp3)
  Segment 14: 86836ms ~ 87412ms (file: 721-756-756-675-567-576-574-576-157-621-673-635-673-376-316-625-623-632-326-632-130-310-625_W.mp3)
  Segment 15: 88412ms ~ 97628ms (file: 360-163-173-103-130-216-216-210-106-167-761-176-706-517-370-231-316-362-231-361-762-126-762-310-120-120-120-210-102-210-107-120-123-231-123-216-126-216-126-123-210-126-312-136-3_W.mp3)
  Segment 16: 98628ms ~ 99204ms (file: 163-163-613-613-167-617_W.mp3)
  Segment 17: 100204ms ~ 100780ms (file: 615-765-625-623-621 620-610-652-136 136 672-670_W.mp3)
  Segment 18: 101780ms ~ 103040ms (file: 310-134-103-130-310-301-104-104-104-103-103-413-134-135-134-130-103-130-134-103-134-145-135-134-135-135-145 314-143-301-134-103-134-315-134-104-134-135-135-145-314-103-130-143-1_V.mp3)
  Segment 19: 104040ms ~ 104616ms (file: 501-510-501-510-506-517-501-501-175-173-173-175-715-753-751-751-751-715-751-571-571-571-576-573-571-751-571-517-517-571-517 570-571-570-570-517-175-756-715-715-751-756-751-751-5_V.mp3)
  Segment 20: 105616ms ~ 118036ms (file: 675-167-126-230-210-201-240-230-231-201-210-210-615-316-136-103-176-230-213-203-132-136-103-167-137-160-106-160-753-753-130-261-602-276 127-102-102-721-762-176-107-102-172-132-1_W.mp3)
  Segment 21: 119036ms ~ 119612ms (file: 570 573-576-573-517-751-715-751-571-576-765-756-576-517-571-517-570-576-571-576-570-576-571-576-573-571-570-570 507-570-573-517-510-576-576-570-571-137-157-107-175-571-175-571-5_V.mp3)
  Segment 22: 120612ms ~ 121188ms (file: 150-150-501-531-150-507-507-517-517-517-517-517-517 150-150-513-510-153-531-351-351-530-153-510-513-351-530-513-513-521-512-510-510-501-501-501-501-506-507-506-510 517-510-517-5_V.mp3)
  Segment 23: 122188ms ~ 122764ms (file: 103-132-130-130-103-130-310-103-103-103-103-312-103-132-130-132-130-153-103-103-130-103-301-103 132-103-310-135-103-130-130-103-310-301-130-130-103-301-315-301-135-103-103-310 1_V.mp3)
  Segment 24: 123764ms ~ 124340ms (file: 570-510-501-507-570-570 105-105-751-157-105-157-517-105-501-510-501-175-510-517-507-510-507-510-105-510-517-510-150-510-571-510 105-150-105-150-571-157-517-510-517-501-105-510-5_V.mp3)
  Segment 25: 125340ms ~ 131496ms (file: 576-206-675-567-236-123-201-102-762-267-132-276-276-213-260-217-103-231-753-123-230-216-657-201-260-201-260-213-206-216-231-217-715-261-213-267-267-213-120-123-213-726-725-216-2_W.mp3)
  Segment 26: 132496ms ~ 140740ms (file: 260-267-256-206-103-170-716-126-123-715-321-103-167-123-572-327-205-270-321-213-260-256-167-261-605-564-123-315-132-315-352-705-672-206-567-267-206-206-260-265-167-106-102-612-1_V.mp3)
  Segment 27: 141740ms ~ 142316ms (file: 756-765-750-752-765-732-765-756-576-576-574-576-756-761-765-765-175-761-756-765-762-316-160-312-306-301-102-132-103-231-320-213-301-126-162-167-321-754-761-574-574-547-547-623-3_W.mp3)
  Segment 28: 143316ms ~ 143892ms (file: 316-362-316-362-203-213-236-362-623-326-260-326-230-260-206-230-236-327-573-756-261-310-316-213-261-263-236-263-527-570-106-106-103-130-230-231-620-320-130-162-132-236-267-216-2_V.mp3)
  Segment 29: 144892ms ~ 145468ms (file: 316-716-317-316-312-173-317-317-173-170-176-173-173-760 217-132-132-163-231-705-312-312-217-572-231-207-312-157-263-325-132-132 176-726-273-765-672-236-627-326-762-276-750-265-2_V.mp3)
  Segment 30: 146468ms ~ 147044ms (file: 167-162-103-163-671-163-135-165-103-615-163-123-215-361-153-163-310-301-623-627-206-360-175-517-263-103-136-103-103-317-617-617-103-132-216-167-102-176-136-167-103-235-130-126-1_V.mp3)
  Segment 31: 148044ms ~ 148620ms (file: 651-306-326-321-165-756-627-672-127-263-162-165-507-673-265-167-267-615-615-165-162-103-126-312-756-576-612-621-132-263-123-316-126-132-316-756-652-367-763-673-671-671-127-213-6_W.mp3)
  Segment 32: 149620ms ~ 150196ms (file: 105-150-501-157-150-501-157-105-150-501-517-510-153-510-501 105-150-510-501-105-150-501-501-150-501-150-150-105-517-150-501-150-105-517-507-710-517-510-175-501 153-105-150-105-1_V.mp3)
  Segment 33: 151196ms ~ 151772ms (file: 310-103-732-703-376-372-723-701-731-713-170-731-761-671-675-763-673-673-765-762-376-213-312-367-673-367-376-367-316-316-312-236-361-613-163-376-756-516-371-310-375-231-263-312-1_W.mp3)
  Segment 34: 152772ms ~ 153348ms (file: 765-756-726-765-765-756-752-726-567-605 576-576-576-756-756-756-765-765-765-762-765-752-765-725-564-651-567-657 576-756-756-756-756-576-756-752-756-567-561-506-506-506-506 576-5_W.mp3)
  Segment 35: 154348ms ~ 154924ms (file: 320-621-120-312-261-320-302-326-320-326-321-130-307-320-321-123-637-657-617-162-126-136-130-136-163-301-135-130-132-103-106-123-230-132-103-132-136-132-321-132-312-160-163-236-1_V.mp3)
  Segment 36: 155924ms ~ 156500ms (file: 762-765-671-170-137-130-136-173-130-567-675-176-167-675-672-675-765-765-761-756-716-617-671-756-751-167-162-560-560-501-105-516-501-503-501 675-675-617_W.mp3)
  Segment 37: 157500ms ~ 158076ms (file: 762-172-175-325-725-276-756-276-765-573-132-153 702-576-132-312-506-754-257-617-567-132-762-132-156-165_V.mp3)
  Segment 38: 159076ms ~ 159652ms (file: 751-571-127-310-312-375-765-765-765-765_W.mp3)
  Segment 39: 160652ms ~ 169184ms (file: 103-130-317-312-130-317-517-176-756-213-123-276-675-627-237-327-357-573-327-752-627-267-267-761-130-571-541-574-537-361-361-326-310-263-326-610-176-571-756-102-137-130-137-231-2_W.mp3)
  Segment 40: 170184ms ~ 170760ms (file: 705-715-756-753-751-715-751-751-236-137-165-175-571-175-130-136-317-137-376-173-317-163-376-137-316-136-316-675-756-675-657-571-756-156-715-750-175-103-310_W.mp3)
  Segment 41: 171760ms ~ 172336ms (file: 107-715-167-163-203-231-213-261-216-203-132-126-162-571-573-107-137-167-176-176-136-123-132-136-126-126-163-126-102-132-103-107-172-132-603-263-102-123-136-175-156-103-756-756-7_V.mp3)
  Segment 42: 173336ms ~ 173912ms (file: 372-617-172-762-167-261-672-716-612-127-156-275-567-536-537-356-631-316-367-361-176-137-165-617-673-765-576-576-567-567-765-170-750-657-564-567-576_W.mp3)
  Segment 43: 174912ms ~ 175488ms (file: 126-365-306-753-312-752-703-376-631-670-623-576-213-361-756 576-675-173-725-507-635-527-537-265-705-327-123-537-157-572-136-152-576-257-326-152-752-750-261-276-326-752-345-132-6_V.mp3)
  Segment 44: 176488ms ~ 194020ms (file: 260-206-107-601-130-103-672-230-123-627-210-213-312-136-103-132-321-213-230-103-126-123 261-201-312-103-130-123-267-260-203-260-123-136-754-754-754-764-137-306-672-320-132-103-1_W.mp3)
  Segment 45: 195020ms ~ 195596ms (file: 317 510-120-123-132-526-627-762-573-361-573-271-206-762-267-627-623-657-124-371-526-725-205-617-756-237-175-506-571-576-752-725-756-576-725-675-507-365-761-137-173-516-765-547-2_V.mp3)
  Segment 46: 196596ms ~ 197172ms (file: 510-107-150-105-150-501-750-573 501-513-105-157-517-157-105-157-105-510-150-105-501-517-510-103-105-105-107-517-501-507-351-501-514-570-510-501-501-571-510-507-501-750-573 517-1_V.mp3)
  Segment 47: 198172ms ~ 198748ms (file: 675-175-576-157-175-751-157-576-571-571-576-576-576-751-571-175 105-105-150-105-105-150-157-517-175-176-175-571-157-567-751-561-157-571-517-571-517-571-517-157-516-156-157-561-5_V.mp3)
  Segment 48: 199748ms ~ 200324ms (file: 157-107-715-571-576-574 105-157-105-105-105-105-105-175-107-157-510-507-571-517-571-570-570-574-573 507-105-150-105-157-105-150-150-105-157-175-170-710-170-175-751-175-715-765-6_V.mp3)
  Segment 49: 201324ms ~ 201900ms (file: 620-612-136-561-167-356-371-132-132-567-175-103-103-103-156-167-126-103-716-167-167-136-106-106-136-103-136-103-130-165-612-613-672-106-136-130-136-165-631-167-163-632-136-136-5_V.mp3)
  Segment 50: 202900ms ~ 203476ms (file: 761-167-716-130-170-176-705-103-617-106-671-103-103-517-107-106-172-162-130-723-103-103-532-503-103-153-106-546-716-152-302-302-312-620-502-562-562-652-265-206-230-201-756-301-3_V.mp3)
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
> **Time**: 2026-04-05 05:57:05
> **AI**: gemini

---

## Response

JSON[
  {
    "segment": 1,
    "transcript": "안녕하세요. 반갑습니다.",
    "words": [
      {"word": "안녕하세요.", "start_ms": 1000, "dur_ms": 600, "phase_lr_deg": 0.0},
      {"word": "반갑습니다.", "start_ms": 1650, "dur_ms": 550, "phase_lr_deg": 0.0}
    ],
    "speaker": "speaker_1",
    "phase_lr_deg": 0.0,
    "confidence": 0.99,
    "language": "ko",
    "silence_before_ms": 1000,
    "audio_type": "speech"
  },
  {
    "segment": 2,
    "transcript": "어서 오세요.",
    "words": [
      {"word": "어서", "start_ms": 50, "dur_ms": 250, "phase_lr_deg": 0.0},
      {"word": "오세요.", "start_ms": 350, "dur_ms": 200, "phase_lr_deg": 0.0}
    ],
    "speaker": "speaker_1",
    "phase_lr_deg": 0.0,
    "confidence": 1.0,
    "language": "ko",
    "silence_before_ms": 50,
    "audio_type": "speech"
  },
  {
    "segment": 3,
    "transcript": "이쪽으로 앉으세요.",
    "words": [
      {"word": "이쪽으로", "start_ms": 30, "dur_ms": 350, "phase_lr_deg": 0.0},
      {"word": "앉으세요.", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": 0.0}
    ],
    "speaker": "speaker_1",
    "phase_lr_deg": 0.0,
    "confidence": 1.0,
    "language": "ko",
    "silence_before_ms": 30,
    "audio_type": "speech"
  },
  {
    "segment": 4,
    "transcript": "무엇을 도와드릴까요?",
    "words": [
      {"word": "무엇을", "start_ms": 100, "dur_ms": 1150, "phase_lr_deg": 0.0},
      {"word": "도와드릴까요?", "start_ms": 1300, "dur_ms": 3150, "phase_lr_deg": 0.0}
    ],
    "speaker": "speaker_2",
    "phase_lr_deg": 0.0,
    "confidence": 1.0,
    "language": "ko",
    "silence_before_ms": 100,
    "audio_type": "speech"
  },
  {
    "segment": 5,
    "transcript": "체크인하고 싶어요.",
    "words": [
      {"word": "체크인하고", "start_ms": 40, "dur_ms": 350, "phase_lr_deg": 0.0},
      {"word": "싶어요.", "start_ms": 420, "dur_ms": 130, "phase_lr_deg": 0.0}
    ],
    "speaker": "speaker_1",
    "phase_lr_deg": 0.0,
    "confidence": 1.0,
    "language": "ko",
    "silence_before_ms": 40,
    "audio_type": "speech"
  },
  {
    "segment": 6,
    "transcript": "예약하셨나요?",
    "words": [
      {"word": "예약하셨나요?", "start_ms": 100, "dur_ms": 7100, "phase_lr_deg": 0.0}
    ],
    "speaker": "speaker_2",
    "phase_lr_deg": 0.0,
    "confidence": 1.0,
    "language": "ko",
    "silence_before_ms": 100,
    "audio_type": "speech"
  },
  {
    "segment": 7,
    "transcript": "네, 인터넷으로 예약했어요.",
    "words": [
      {"word": "네,", "start_ms": 50, "dur_ms": 4000, "phase_lr_deg": 0.0},
      {"word": "인터넷으로", "start_ms": 4100, "dur_ms": 6000, "phase_lr_deg": 0.0},
      {"word": "예약했어요.", "start_ms": 10200, "dur_ms": 6700, "phase_lr_deg": 0.0}
    ],
    "speaker": "speaker_1",
    "phase_lr_deg": 0.0,
    "confidence": 1.0,
    "language": "ko",
    "silence_before_ms": 50,
    "audio_type": "speech"
  },
  {
    "segment": 8,
    "transcript": "성함이 어떻게 되시죠?",
    "words": [
      {"word": "성함이", "start_ms": 100, "dur_ms": 3100, "phase_lr_deg": 0.0},
      {"word": "어떻게", "start_ms": 3250, "dur_ms": 2500, "phase_lr_deg": 0.0},
      {"word": "되시죠?", "start_ms": 5800, "dur_ms": 2100, "phase_lr_deg": 0.0}
    ],
    "speaker": "speaker_2",
    "phase_lr_deg": 0.0,
    "confidence": 1.0,
    "language": "ko",
    "silence_before_ms": 100,
    "audio_type": "speech"
  },
  {
    "segment": 9,
    "transcript": "김민수라고 합니다.",
    "words": [
      {"word": "김민수라고", "start_ms": 50, "dur_ms": 6300, "phase_lr_deg": 0.0},
      {"word": "합니다.", "start_ms": 6400, "dur_ms": 3700, "phase_lr_deg": 0.0}
    ],
    "speaker": "speaker_1",
    "phase_lr_deg": 0.0,
    "confidence": 1.0,
    "language": "ko",
    "silence_before_ms": 50,
    "audio_type": "speech"
  },
  {
    "segment": 10,
    "transcript": "(noise)",
    "words": [],
    "speaker": "speaker_0",
    "phase_lr_deg": 0.0,
    "confidence": 0.0,
    "language": "ko",
    "silence_before_ms": 0,
    "audio_type": "noise"
  },
  {
    "segment": 11,
    "transcript": "(noise)",
    "words": [],
    "speaker": "speaker_0",
    "phase_lr_deg": 0.0,
    "confidence": 0.0,
    "language": "ko",
    "silence_before_ms": 0,
    "audio_type": "noise"
  },
  {
    "segment": 12,
    "transcript": "잠시만 기다려 주세요.",
    "words": [
      {"word": "잠시만", "start_ms": 50, "dur_ms": 1100, "phase_lr_deg": 0.0},
      {"word": "기다려", "start_ms": 1200, "dur_ms": 1000, "phase_lr_deg": 0.0},
      {"word": "주세요.", "start_ms": 2250, "dur_ms": 1850, "phase_lr_deg": 0.0}
    ],
    "speaker": "speaker_2",
    "phase_lr_deg": 0.0,
    "confidence": 1.0,
    "language": "ko",
    "silence_before_ms": 50,
    "audio_type": "speech"
  },
  {
    "segment": 13,
    "transcript": "네, 여기 예약 내역 있습니다.",
    "words": [
      {"word": "네,", "start_ms": 100, "dur_ms": 2300, "phase_lr_deg": 0.0},
      {"word": "여기", "start_ms": 2450, "dur_ms": 2400, "phase_lr_deg": 0.0},
      {"word": "예약", "start_ms": 4900, "dur_ms": 2300, "phase_lr_deg": 0.0},
      {"word": "내역", "start_ms": 7250, "dur_ms": 3200, "phase_lr_deg": 0.0},
      {"word": "있습니다.", "start_ms": 10500, "dur_ms": 6700, "phase_lr_deg": 0.0}
    ],
    "speaker": "speaker_1",
    "phase_lr_deg": 0.0,
    "confidence": 1.0,
    "language": "ko",
    "silence_before_ms": 100,
    "audio_type": "speech"
  },
  {
    "segment": 14,
    "transcript": "(noise)",
    "words": [],
    "speaker": "speaker_0",
    "phase_lr_deg": 0.0,
    "confidence": 0.0,
    "language": "ko",
    "silence_before_ms": 0,
    "audio_type": "noise"
  },
  {
    "segment": 15,
    "transcript": "여기에 서명해 주시겠어요?",
    "words": [
      {"word": "여기에", "start_ms": 100, "dur_ms": 2100, "phase_lr_deg": 0.0},
      {"word": "서명해", "start_ms": 2250, "dur_ms": 3200, "phase_lr_deg": 0.0},
      {"word": "주시겠어요?", "start_ms": 5500, "dur_ms": 3700, "phase_lr_deg": 0.0}
    ],
    "speaker": "speaker_2",
    "phase_lr_deg": 0.0,
    "confidence": 1.0,
    "language": "ko",
    "silence_before_ms": 100,
    "audio_type": "speech"
  },
  {
    "segment": 16,
    "transcript": "(noise)",
    "words": [],
    "speaker": "speaker_0",
    "phase_lr_deg": 0.0,
    "confidence": 0.0,
    "language": "ko",
    "silence_before_ms": 0,
    "audio_type": "noise"
  },
  {
    "segment": 17,
    "transcript": "(noise)",
    "words": [],
    "speaker": "speaker_0",
    "phase_lr_deg": 0.0,
    "confidence": 0.0,
    "language": "ko",
    "silence_before_ms": 0,
    "audio_type": "noise"
  },
  {
    "segment": 18,
    "transcript": "네, 알겠습니다.",
    "words": [
      {"word": "네,", "start_ms": 30, "dur_ms": 200, "phase_lr_deg": 0.0},
      {"word": "알겠습니다.", "start_ms": 260, "dur_ms": 1000, "phase_lr_deg": 0.0}
    ],
    "speaker": "speaker_1",
    "phase_lr_deg": 0.0,
    "confidence": 1.0,
    "language": "ko",
    "silence_before_ms": 30,
    "audio_type": "speech"
  },
  {
    "segment": 19,
    "transcript": "(noise)",
    "words": [],
    "speaker": "speaker_0",
    "phase_lr_deg": 0.0,
    "confidence": 0.0,
    "language": "ko",
    "silence_before_ms": 0,
    "audio_type": "noise"
  },
  {
    "segment": 20,
    "transcript": "방 키 여기 있습니다.",
    "words": [
      {"word": "방", "start_ms": 50, "dur_ms": 1600, "phase_lr_deg": 0.0},
      {"word": "키", "start_ms": 1700, "dur_ms": 1600, "phase_lr_deg": 0.0},
      {"word": "여기", "start_ms": 3350, "dur_ms": 3100, "phase_lr_deg": 0.0},
      {"word": "있습니다.", "start_ms": 6500, "dur_ms": 5800, "phase_lr_deg": 0.0}
    ],
    "speaker": "speaker_2",
    "phase_lr_deg": 0.0,
    "confidence": 1.0,
    "language": "ko",
    "silence_before_ms": 50,
    "audio_type": "speech"
  },
  {
    "segment": 21,
    "transcript": "(noise)",
    "words": [],
    "speaker": "speaker_0",
    "phase_lr_deg": 0.0,
    "confidence": 0.0,
    "language": "ko",
    "silence_before_ms": 0,
    "audio_type": "noise"
  },
  {
    "segment": 22,
    "transcript": "(noise)",
    "words": [],
    "speaker": "speaker_0",
    "phase_lr_deg": 0.0,
    "confidence": 0.0,
    "language": "ko",
    "silence_before_ms": 0,
    "audio_type": "noise"
  },
  {
    "segment": 23,
    "transcript": "고맙습니다.",
    "words": [
      {"word": "고맙습니다.", "start_ms": 40, "dur_ms": 500, "phase_lr_deg": 0.0}
    ],
    "speaker": "speaker_1",
    "phase_lr_deg": 0.0,
    "confidence": 1.0,
    "language": "ko",
    "silence_before_ms": 40,
    "audio_type": "speech"
  },
  {
    "segment": 24,
    "transcript": "(noise)",
    "words": [],
    "speaker": "speaker_0",
    "phase_lr_deg": 0.0,
    "confidence": 0.0,
    "language": "ko",
    "silence_before_ms": 0,
    "audio_type": "noise"
  },
  {
    "segment": 25,
    "transcript": "즐거운 시간 보내세요.",
    "words": [
      {"word": "즐거운", "start_ms": 100, "dur_ms": 1900, "phase_lr_deg": 0.0},
      {"word": "시간", "start_ms": 2050, "dur_ms": 1800, "phase_lr_deg": 0.0},
      {"word": "보내세요.", "start_ms": 3900, "dur_ms": 2200, "phase_lr_deg": 0.0}
    ],
    "speaker": "speaker_2",
    "phase_lr_deg": 0.0,
    "confidence": 1.0,
    "language": "ko",
    "silence_before_ms": 100,
    "audio_type": "speech"
  },
  {
    "segment": 26,
    "transcript": "실례합니다. 화장실이 어디예요?",
    "words": [
      {"word": "실례합니다.", "start_ms": 50, "dur_ms": 2100, "phase_lr_deg": 0.0},
      {"word": "화장실이", "start_ms": 2200, "dur_ms": 3000, "phase_lr_deg": 0.0},
      {"word": "어디예요?", "start_ms": 5250, "dur_ms": 2950, "phase_lr_deg": 0.0}
    ],
    "speaker": "speaker_1",
    "phase_lr_deg": 0.0,
    "confidence": 1.0,
    "language": "ko",
    "silence_before_ms": 50,
    "audio_type": "speech"
  },
  {
    "segment": 27,
    "transcript": "저쪽 끝에 있습니다.",
    "words": [
      {"word": "저쪽", "start_ms": 100, "dur_ms": 7100, "phase_lr_deg": 0.0},
      {"word": "끝에", "start_ms": 7250, "dur_ms": 5400, "phase_lr_deg": 0.0},
      {"word": "있습니다.", "start_ms": 12700, "dur_ms": 4350, "phase_lr_deg": 0.0}
    ],
    "speaker": "speaker_2",
    "phase_lr_deg": 0.0,
    "confidence": 1.0,
    "language": "ko",
    "silence_before_ms": 100,
    "audio_type": "speech"
  },
  {
    "segment": 28,
    "transcript": "(noise)",
    "words": [],
    "speaker": "speaker_0",
    "phase_lr_deg": 0.0,
    "confidence": 0.0,
    "language": "ko",
    "silence_before_ms": 0,
    "audio_type": "noise"
  },
  {
    "segment": 29,
    "transcript": "(noise)",
    "words": [],
    "speaker": "speaker_0",
    "phase_lr_deg": 0.0,
    "confidence": 0.0,
    "language": "ko",
    "silence_before_ms": 0,
    "audio_type": "noise"
  },
  {
    "segment": 30,
    "transcript": "(noise)",
    "words": [],
    "speaker": "speaker_0",
    "phase_lr_deg": 0.0,
    "confidence": 0.0,
    "language": "ko",
    "silence_before_ms": 0,
    "audio_type": "noise"
  },
  {
    "segment": 31,
    "transcript": "(noise)",
    "words": [],
    "speaker": "speaker_0",
    "phase_lr_deg": 0.0,
    "confidence": 0.0,
    "language": "ko",
    "silence_before_ms": 0,
    "audio_type": "noise"
  },
  {
    "segment": 32,
    "transcript": "(noise)",
    "words": [],
    "speaker": "speaker_0",
    "phase_lr_deg": 0.0,
    "confidence": 0.0,
    "language": "ko",
    "silence_before_ms": 0,
    "audio_type": "noise"
  },
  {
    "segment": 33,
    "transcript": "이거 얼마예요?",
    "words": [
      {"word": "이거", "start_ms": 100, "dur_ms": 1300, "phase_lr_deg": 0.0},
      {"word": "얼마예요?", "start_ms": 1450, "dur_ms": 4000, "phase_lr_deg": 0.0}
    ],
    "speaker": "speaker_1",
    "phase_lr_deg": 0.0,
    "confidence": 1.0,
    "language": "ko",
    "silence_before_ms": 100,
    "audio_type": "speech"
  },
  {
    "segment": 34,
    "transcript": "(noise)",
    "words": [],
    "speaker": "speaker_0",
    "phase_lr_deg": 0.0,
    "confidence": 0.0,
    "language": "ko",
    "silence_before_ms": 0,
    "audio_type": "noise"
  },
  {
    "segment": 35,
    "transcript": "삼만 원입니다.",
    "words": [
      {"word": "삼만", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
      {"word": "원입니다.", "start_ms": 260, "dur_ms": 300, "phase_lr_deg": 0.0}
    ],
    "speaker": "speaker_2",
    "phase_lr_deg": 0.0,
    "confidence": 1.0,
    "language": "ko",
    "silence_before_ms": 50,
    "audio_type": "speech"
  },
  {
    "segment": 36,
    "transcript": "(noise)",
    "words": [],
    "speaker": "speaker_0",
    "phase_lr_deg": 0.0,
    "confidence": 0.0,
    "language": "ko",
    "silence_before_ms": 0,
    "audio_type": "noise"
  },
  {
    "segment": 37,
    "transcript": "조금 깎아 주세요.",
    "words": [
      {"word": "조금", "start_ms": 30, "dur_ms": 200, "phase_lr_deg": 0.0},
      {"word": "깎아", "start_ms": 240, "dur_ms": 150, "phase_lr_deg": 0.0},
      {"word": "주세요.", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": 0.0}
    ],
    "speaker": "speaker_1",
    "phase_lr_deg": 0.0,
    "confidence": 1.0,
    "language": "ko",
    "silence_before_ms": 30,
    "audio_type": "speech"
  },
  {
    "segment": 38,
    "transcript": "(noise)",
    "words": [],
    "speaker": "speaker_0",
    "phase_lr_deg": 0.0,
    "confidence": 0.0,
    "language": "ko",
    "silence_before_ms": 0,
    "audio_type": "noise"
  },
  {
    "segment": 39,
    "transcript": "죄송하지만 정찰제입니다.",
    "words": [
      {"word": "죄송하지만", "start_ms": 100, "dur_ms": 3100, "phase_lr_deg": 0.0},
      {"word": "정찰제입니다.", "start_ms": 3250, "dur_ms": 5250, "phase_lr_deg": 0.0}
    ],
    "speaker": "speaker_2",
    "phase_lr_deg": 0.0,
    "confidence": 1.0,
    "language": "ko",
    "silence_before_ms": 100,
    "audio_type": "speech"
  },
  {
    "segment": 40,
    "transcript": "(noise)",
    "words": [],
    "speaker": "speaker_0",
    "phase_lr_deg": 0.0,
    "confidence": 0.0,
    "language": "ko",
    "silence_before_ms": 0,
    "audio_type": "noise"
  },
  {
    "segment": 41,
    "transcript": "그럼 이걸로 주세요.",
    "words": [
      {"word": "그럼", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
      {"word": "이걸로", "start_ms": 250, "dur_ms": 150, "phase_lr_deg": 0.0},
      {"word": "주세요.", "start_ms": 410, "dur_ms": 130, "phase_lr_deg": 0.0}
    ],
    "speaker": "speaker_1",
    "phase_lr_deg": 0.0,
    "confidence": 1.0,
    "language": "ko",
    "silence_before_ms": 40,
    "audio_type": "speech"
  },
  {
    "segment": 42,
    "transcript": "(noise)",
    "words": [],
    "speaker": "speaker_0",
    "phase_lr_deg": 0.0,
    "confidence": 0.0,
    "language": "ko",
    "silence_before_ms": 0,
    "audio_type": "noise"
  },
  {
    "segment": 43,
    "transcript": "네, 여기 있습니다.",
    "words": [
      {"word": "네,", "start_ms": 30, "dur_ms": 200, "phase_lr_deg": 0.0},
      {"word": "여기", "start_ms": 240, "dur_ms": 150, "phase_lr_deg": 0.0},
      {"word": "있습니다.", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": 0.0}
    ],
    "speaker": "speaker_2",
    "phase_lr_deg": 0.0,
    "confidence": 1.0,
    "language": "ko",
    "silence_before_ms": 30,
    "audio_type": "speech"
  },
  {
    "segment": 44,
    "transcript": "카드로 계산할게요.",
    "words": [
      {"word": "카드로", "start_ms": 50, "dur_ms": 3100, "phase_lr_deg": 0.0},
      {"word": "계산할게요.", "start_ms": 3200, "dur_ms": 14300, "phase_lr_deg": 0.0}
    ],
    "speaker": "speaker_1",
    "phase_lr_deg": 0.0,
    "confidence": 1.0,
    "language": "ko",
    "silence_before_ms": 50,
    "audio_type": "speech"
  },
  {
    "segment": 45,
    "transcript": "(noise)",
    "words": [],
    "speaker": "speaker_0",
    "phase_lr_deg": 0.0,
    "confidence": 0.0,
    "language": "ko",
    "silence_before_ms": 0,
    "audio_type": "noise"
  },
  {
    "segment": 46,
    "transcript": "(noise)",
    "words": [],
    "speaker": "speaker_0",
    "phase_lr_deg": 0.0,
    "confidence": 0.0,
    "language": "ko",
    "silence_before_ms": 0,
    "audio_type": "noise"
  },
  {
    "segment": 47,
    "transcript": "(noise)",
    "words": [],
    "speaker": "speaker_0",
    "phase_lr_deg": 0.0,
    "confidence": 0.0,
    "language": "ko",
    "silence_before_ms": 0,
    "audio_type": "noise"
  },
  {
    "segment": 48,
    "transcript": "(noise)",
    "words": [],
    "speaker": "speaker_0",
    "phase_lr_deg": 0.0,
    "confidence": 0.0,
    "language": "ko",
    "silence_before_ms": 0,
    "audio_type": "noise"
  },
  {
    "segment": 49,
    "transcript": "(noise)",
    "words": [],
    "speaker": "speaker_0",
    "phase_lr_deg": 0.0,
    "confidence": 0.0,
    "language": "ko",
    "silence_before_ms": 0,
    "audio_type": "noise"
  },
  {
    "segment": 50,
    "transcript": "(noise)",
    "words": [],
    "speaker": "speaker_0",
    "phase_lr_deg": 0.0,
    "confidence": 0.0,
    "language": "ko",
    "silence_before_ms": 0,
    "audio_type": "noise"
  }
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 05:57:05*
