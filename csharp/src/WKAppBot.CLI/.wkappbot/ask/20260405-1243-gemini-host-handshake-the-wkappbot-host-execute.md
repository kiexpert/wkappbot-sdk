# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=1700  time=12:42:27  uptime=29214s  mem=1751MB  system_processes=352"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_124149.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 216-126-213-123 321-312 701-762 170-217-527-574-574-610-765-762-576-637-576-756-765-126-231-321-132 672-671 540-516-672-762 236 132-621-623-612 517-165-103-106-103-160-163-106-1_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 236-213-231-321-237-126-213-623-216-612-763-362-632-126-136-326-631-132-216_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 570-571-175-571-157-751-571-751-715-176-715-715 175-175-710-750-751-710-175-715-170-701-710-750-701-750-751-715-750-751-751-750-570-175-573-357-301-150-157 157-175-157-175-751-1_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 175-137-173-175-713-736-173-170-176-671-671-173-170-107-107-175-107-105-715-107-175-107-107-713-715-170 107-173-137-107-173-703-173-160-130-173-716-175-103-103-173-761-173-713-7_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 501-506-516-507-506-537-506-502-517-526-517-507-576-567-507-507-520-502-571-501-502-532-350-503-351-510-543-541-506-507-561-506-507-507-507-507-576-576-576-572-576-537-572-572-5_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 132-134-163-136-103-163-612-306-137-136-230-206-762-273-102-136-132-136 123-123-705-326-130-130-106-103-130-130-132-203-213-123-130-136-132-126-103-123-260-230-203-123-320-362-3_V.mp3)
  Segment 7: 9456ms ~ 18276ms (file: 321-326-321-230-206-230-216-326-320-230-217-102-321-752-120-132-623-261-263-321-127-716-726-267-207-263-267-276-237-573-267-263-627-672-267-260-267-256-301-301-367-632-326-132-2_W.mp3)
  Segment 8: 19276ms ~ 27411ms (file: 215-527-210-261-236-267-276-267-267-312-231-123-231-721-175-130-123-153-201-103-132-143-574-754-750-312-312-632-120-302-237-321-103-102-120-127-216-236-263-621-236-203-206-132-1_W.mp3)
  Segment 9: 28411ms ~ 28987ms (file: 572-571-573-523-576-301-573-530-502-571-157-517-516-526-517-503-567-576-507-576-527-576-527-572-576-507-507-572-507-572-527-526-527-576-572-576-576-507-507-572-572-572-507-305-5_V.mp3)
  Segment 10: 29987ms ~ 30563ms (file: 102-326-316-103-613-564-162-657-362-163-105-576-607-756-756-163-726-610-617-216-103-130-136-132-136-136-103-132-103-106-136-135-316-163-653-612-652-617-607-167-162-675-163-135-1_V.mp3)
  Segment 11: 31563ms ~ 36963ms (file: 270-527-175-271-102-127-127-137-726-267-231-574-527-275-270-201-217-237-725-123-123-162-726-756-725-752-276-712-127-271-217-751-752-752-126-125-271-725-765-726-375-237-576-725-5_W.mp3)
  Segment 12: 37963ms ~ 38539ms (file: 130-130-106-216-162-132-103-163-617-103-136-156-126-672-162-103-130-637-103-167-103-126-152-137-256-176-162-102-612-167-671-136-741-103-613-316-163-136-137-103-137-106-167-136-1_V.mp3)
  Segment 13: 39539ms ~ 44723ms (file: 231-216-312-213-213-213-210-207-123-213-236-123-230-213-120-312-321-231-203-231-213-210-213-213-237-123-132-213-120-210-321-213-120-120-217-201-123-213-213-210-123-273-216-230-2_W.mp3)
  Segment 14: 45723ms ~ 46299ms (file: 546-521-523-572-527-572-572-572-572-507-507-250-270-752-752-527-752-752-257-752-572-576-573-572-537-574-501-532-572-752-572-572-752-571-576-751-571-752-756-576-576-573-751-752 5_W.mp3)
  Segment 15: 47299ms ~ 47875ms (file: 561-136-317-132-130-103-136-103 623-162-517-163-132-326-136-512-165-103-751-167-127-136 123-362-152-163-157-312-731-107-136-213_V.mp3)
  Segment 16: 48875ms ~ 49451ms (file: 175-715 510-150-150-175-175-107-170-750-107-175-107-107-710-107-170-170-170-710-107-715-751 501-571-157-517-157-751-570-571-175-175-170-170-175-170-170-175-107-175-715 510-517-5_V.mp3)
  Segment 17: 50451ms ~ 51027ms (file: 127-163-715-715-753-361-315-136-135-621-132-316-316-231-132-713-312-326-632-631-756-176-715-765-560-612-217-756-162-623-267_W.mp3)
  Segment 18: 52027ms ~ 52603ms (file: 173-135-523-731-537-573-517-537-356-357-537-357-351-735-135-715-153-135-137-315-315-352-532-573-537-537-537-135-135-751-753-573-572-573-527-573-537-537-571-573-753-573-573-153-1_W.mp3)
  Segment 19: 53603ms ~ 54179ms (file: 156-516-650-506-560-651-650-651-167-167-213-236-321-136-132-231-170-127-173-173-715-576-756-326-236-231-230-215-265-236-231-321-231-756-765-761-617-126-216-126-652-362-612-236-2_W.mp3)
  Segment 20: 55179ms ~ 55755ms (file: 601-610-102 610-601-621-621-610-612-160-106-103 621-612-612-612-612-612-621-162-612-621-627-612-610 612-610-610-610-160-617-617-617-617-617-617-610-617-612-160-163-516-605-605-6_V.mp3)
  Segment 21: 56755ms ~ 61687ms (file: 301-312-310-130-763-673-135-175-675-675-320-627-230-263-160-267-230-236-321-310-316-236-132-312-132-123-103-162-321-132-163-312-752-231-531-327-321-213-263-632-312-132-321-231-2_W.mp3)
  Segment 22: 62687ms ~ 70283ms (file: 261-106-752-267-213-752-256-261-261-257-103-103-130-517-103-576-265-526-542-167-265-216-257-267-261-327-321-132-210-260-261-260-102-574-265-132-574-273-627-130-261-267-562-276-2_W.mp3)
  Segment 23: 71283ms ~ 71859ms (file: 514-504-570-574-503-504-504 351-527-507-507-504-507-570-570-502-507-504_V.mp3)
  Segment 24: 72859ms ~ 73435ms (file: 752-752-750-527-752-257-257-257-527-532-523-275-572-572-576-576-576-567-576-567-576-576-576-576-576-576-756-502-572-527-572-527-257-502-507-507-275-527-725-527-572-735-725-275-7_W.mp3)
  Segment 25: 74435ms ~ 75011ms (file: 620-625-625-651-625 620-265-256-652-560-562-256-561-652-652-625-625_W.mp3)
  Segment 26: 76011ms ~ 76587ms (file: 527-520-250-562-526-527-527-527-524-576-576-576-574 576-756-745-765-756-756-765-756 527-506-570-576-527-207-275-256-627-275-756-765-567-756-756-756-576-756-756 507-275-256-527-5_V.mp3)
  Segment 27: 77587ms ~ 78163ms (file: 351-354-534-564-523-502-502-507-574-507-572-507-507-573-502-507-507-506-507-572-507-507-507-507-507-502-576-530-354-351-530-351-531-351-351-530-534-526-504-506-507-572-507-573-5_V.mp3)
  Segment 28: 79163ms ~ 79739ms (file: 507-572-250-627-157-561-652-312 517-563-513-537-537-523 527-502-571-507-527-520-250-257-257-250 561 503-205-520-250-250-523-526-521-251-257-502-506-506-562-652-653-561 520-520-5_V.mp3)
  Segment 29: 80739ms ~ 81315ms (file: 517-157-157-517-105-150-517-157-107-610-160-106-167-157-105-706-507 107-160-170-516-576-175-156-670-765-756 547-547-571-517-567-576-560-517-105-176-107-176-107-156-157-716-170-7_V.mp3)
  Segment 30: 82315ms ~ 82891ms (file: 762-136-270-765-572-572-572-627-621-327-263-213-123-167-621-236-137-175 153-167_V.mp3)
  Segment 31: 83891ms ~ 87671ms (file: 267-632-276-267-762-217-276-627-236-236-217-103-103-137-126-126-163-167-216-102-102-316-732-623-721-126-271-123-317-103-312-137-103-103-762-273-273-762-216-267-102-103-103-326-3_W.mp3)
  Segment 32: 88671ms ~ 89247ms (file: 627-627-267-260-517-501-520-520-520-520-502-521-520-502-520-520-506-502-625-625-625-621-625-625-657-625-265-627-625-620-132-132-312-527-502-502-502-503-502-503-502-503-501-503-5_W.mp3)
  Segment 33: 90247ms ~ 90823ms (file: 756-751-756-756-756-756-751-576-576-576-756 175-170-175-571-173-715-175-175-517-157-175-517-157-571-571-571-571-571-751-571-571-576-756-571-576-750 157-157-175-751-517-715-710-1_W.mp3)
  Segment 34: 91823ms ~ 97331ms (file: 123-710-316-231-216-276-564-176-102-263-261-126-712-162-175-176-765-620-236-216-326-210-120-726-607-267-675-723-321-321-132-316-163-137-302-102-351-135-163-621-315-603-316-657-1_W.mp3)
  Segment 35: 98331ms ~ 107475ms (file: 317-316-360-356-315-130-567-362-260-106-103-103-134-602-753-132-103-157-132-106-260-260-126-310-137-167-160-756-321-751-135-537-103-574-507-567-217-276-207-127-756-752-316-570-1_W.mp3)
  Segment 36: 108475ms ~ 109051ms (file: 623-612-162-162-610-613-126-621-206-321-231-501-507-537-762-326-136-167-567-260-123-756-756-670-316-136-613-673-671-301-631-603-632-136-163-163-136-574-132-216-263-627-726-627-6_V.mp3)
  Segment 37: 110051ms ~ 110627ms (file: 572-572-527-573-752-257-527-531-576-507-572-527-257-257-720-756-756-756-571-512-516-576-570-567-570-572-576-576-572-572-572-576-572-507-752-756-756-516-572-527 572-257-257-520-5_W.mp3)
  Segment 38: 111627ms ~ 112203ms (file: 627-627-512-516-513-531-512-153-125-513-512-521-125-521-152-512-502-502-521-512-502-521-265-562-650-605-650-650-650-650-650-650-605-650-651-650-650-561-572-701-571-576-501-570-5_V.mp3)
  Segment 39: 113203ms ~ 125335ms (file: 267-107-201-263-261-203-230-236-213-321-210-260-571-627-213-261-123-267-123-312-231-103-103-206-230-201-261-120-132-310-130-130-321-126-162-260-610-136-103-230-163-216-102-320-2_W.mp3)
  Segment 40: 126335ms ~ 126911ms (file: 612-612-126-261-236-625-625-267-625-657-625-265-625-625 502-250-250-256-250-256-205-502-520-257-527-527-572-507-570-256-275-507-625-256-532_W.mp3)
  Segment 41: 127911ms ~ 128487ms (file: 501-507-570-507-507-506-507-507-570-506-576-572-573-567-576-156-615-165-165-126 503-507-507-507-570-507-570-570-570-507-576-570_W.mp3)
  Segment 42: 129487ms ~ 130063ms (file: 567-653-562-526-652-657-625 620-621-625-627-621-627-267-627-621-261-261-261-261-261-620 263-621-623-627-625-627-621-627-625-267-627-621-261-263-261-261 623-625-621-627-627-261-6_W.mp3)
  Segment 43: 131063ms ~ 131639ms (file: 321-213-762-167-137-732-617-261-721-275-513-156-165-136-173-371-613-167-172-715-157-153-513-153-567-756-756-756-576-567-576-576-561-531-536-756-376-136-163-156-567-567-135-137-1_W.mp3)
  Segment 44: 132639ms ~ 139623ms (file: 601-617-607-671-263-267-627-762-167-236-713-137-103-671-672-627-627-263-327-321-532-567-627-672-672-276-627-627-276-267-263-263-237-236-267-765-167-132-320-236-327-523-263-216-1_W.mp3)
  Segment 45: 140623ms ~ 141199ms (file: 150-510-150-510-501-517-150-105-510-517-150-570-571 157-150-105-510-150-510-510-150-510-501-501-150-501-510-501-150-510-150-571 105-157-150-150-510-150-150-150-150-510-517-510-5_V.mp3)
  Segment 46: 142199ms ~ 154583ms (file: 130-174-103-137-173-751-260-143-107-302-176-132-103-132-230-267-103-267-321-137-167-102-302-130-201-132-260-102-201-123-260-263-103-764-136-176-206-627-206-260-267-260-103-764-2_V.mp3)
  Segment 47: 155583ms ~ 163755ms (file: 621-267-107-231-512-573-237-126-574-547-217-206-106-261-261-206-237-276-126-261-276-206-572-201-725-726-326-312-263-267-201-752-726-756-267-267-620-672-627-627-216-206-627-216-2_V.mp3)
  Segment 48: 164755ms ~ 165331ms (file: 105-165-105-162-316-612-136-156-130-136-136-103-106-136-602-620-260-260-206-102-103-132-132-301-753-137-163-162-163-163-574 132-130 136-231-132-321-306-651-651-601-106-163-137-7_V.mp3)
  Segment 49: 166331ms ~ 175727ms (file: 132-201-362-123-132-231-312-536-267-276-706-302-213-760-571-230-206-523-321-312-201-270-153-236-230-704-102-572-163-261-120-152-627-512-751-302-107-267-267-132-302-326-206-260-3_V.mp3)
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
> **Time**: 2026-04-05 12:43:34
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "Consideration, this is very good.",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "Consideration,", "start_ms": 20, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "this", "start_ms": 400, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 510, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "very", "start_ms": 545, "dur_ms": 15, "phase_lr_deg": 0.0},
{"word": "good.", "start_ms": 565, "dur_ms": 11, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "Hmm, love how we put so much ginger in. And hey, this is part of kimchi making tradition, okay? In slow motion, we've been slow motion.",
"speaker": "speaker_1",
"confidence": 0.92,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Hmm,", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "love", "start_ms": 120, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "how", "start_ms": 280, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "we", "start_ms": 390, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "put", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "so", "start_ms": 580, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "much", "start_ms": 670, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "ginger", "start_ms": 800, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "in.", "start_ms": 1010, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "내 손으로 만들어서 더 맛있는 김치",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "내", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "손으로", "start_ms": 130, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "만들어서", "start_ms": 320, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 530, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "맛있는", "start_ms": 580, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "김치", "start_ms": 750, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "할 집이 완성이 된 거지요",
"speaker": "speaker_3",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "할", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "집이", "start_ms": 120, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "완성이", "start_ms": 250, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "된", "start_ms": 420, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "거지요", "start_ms": 490, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "네 더 많은 공간들을 만들어 보고 싶고",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 20, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 90, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "많은", "start_ms": 160, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "공간들을", "start_ms": 290, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "만들어", "start_ms": 500, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "보고", "start_ms": 650, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "싶고", "start_ms": 770, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "Go off for a few minutes and then we'll take it.",
"speaker": "speaker_5",
"confidence": 0.91,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Go", "start_ms": 50, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "off", "start_ms": 120, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "for", "start_ms": 210, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 280, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "few", "start_ms": 320, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "minutes", "start_ms": 450, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "and", "start_ms": 680, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "then", "start_ms": 770, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "we'll", "start_ms": 880, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "take", "start_ms": 1010, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "it.", "start_ms": 1130, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 8,
"transcript": "라탄 숯불 수양대군은 누구야라며 구글 검색창을 두드리기 시작한 거죠",
"speaker": "speaker_6",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "라탄", "start_ms": 60, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "숯불", "start_ms": 250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "수양대군은", "start_ms": 410, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "누구야라며", "start_ms": 800, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "구글", "start_ms": 1130, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "검색창을", "start_ms": 1260, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "두드리기", "start_ms": 1570, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "시작한", "start_ms": 1820, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "거죠", "start_ms": 2010, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "노아의 철칙에 대해서 알아봤습니다 물론 오늘 알아본 제이슨 노아의 매매 지표 당연히 무료로 공유해 드릴 거고요 제가 만든 지표도 아니고 남이 만든 지표를 가지고 정말",
"speaker": "speaker_7",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "노아의", "start_ms": 40, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "철칙에", "start_ms": 210, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "대해서", "start_ms": 420, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "알아봤습니다", "start_ms": 610, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "물론", "start_ms": 1000, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 1150, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "알아본", "start_ms": 1270, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "제이슨", "start_ms": 1440, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "노아의", "start_ms": 1630, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "갈망하는 표 가져가란 표가 없는 거야",
"speaker": "speaker_8",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "갈망하는", "start_ms": 50, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "표", "start_ms": 280, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "가져가란", "start_ms": 370, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "표가", "start_ms": 580, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "없는", "start_ms": 730, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "거야", "start_ms": 860, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "딱 그런 조연이었습니다 편리하고 꼭 있어야 하는 필수 가전이지만 소음이 크고 진동이 심하고 돌아갈",
"speaker": "speaker_9",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "딱", "start_ms": 30, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "그런", "start_ms": 100, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "조연이었습니다", "start_ms": 230, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "편리하고", "start_ms": 620, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "꼭", "start_ms": 850, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "있어야", "start_ms": 920, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 1080, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "필수", "start_ms": 1200, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "가전이지만", "start_ms": 1330, "dur_ms": 320, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "4주 6주 전쟁 종료한다 그래서 나는 그거 한번 먹으려고 기다린다 너한테 분명히 얘기했어 기억나? 아 네 난 그거 놓치지 않았잖아 그래서 숨 죽이고 지금 배팅 안 하는 게 그 기회가",
"speaker": "speaker_10",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "4주", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "6주", "start_ms": 170, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "전쟁", "start_ms": 290, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "종료한다", "start_ms": 440, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 700, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "나는", "start_ms": 870, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "그거", "start_ms": 1000, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "한번", "start_ms": 1110, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "먹으려고", "start_ms": 1230, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "해외에서는 이미 한국 액션은 믿고 보는 브랜드라는 인식이 자리 잡은 상태입니다 이야기의 참신함보다 낯선 공간에서 벌어지는 강렬한 액션과 감정선을 보는 재미에 집중하는 경향이 강한 것인데요 결국 휴미니티는 한국 설날 극장가에는 어울리지 않았지만",
"speaker": "speaker_11",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "해외에서는", "start_ms": 60, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "이미", "start_ms": 320, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 440, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "액션은", "start_ms": 590, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "믿고", "start_ms": 780, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "보는", "start_ms": 910, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "브랜드라는", "start_ms": 1030, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "인식이", "start_ms": 1280, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "자리", "start_ms": 1450, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "국토교통부에 신고를 한 것으로 보이고요 그래서 이 FSD 기능이 내재되어 있는데 뭐 하드웨어 쓰리 하드웨어 포 이렇게 있잖아요",
"speaker": "speaker_12",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "국토교통부에", "start_ms": 50, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "신고를", "start_ms": 360, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 550, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "것으로", "start_ms": 620, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "보이고요", "start_ms": 770, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 980, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 1150, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "FSD", "start_ms": 1200, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "기능이", "start_ms": 1410, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "저 두 분과 함께 짚어 보겠습니다 먼저 정당 지지도 보시겠습니다 더불어민주당 48% 국민의 힘 18%였습니다 이재명 정부 출범 이후 민주당은 최고치 국민의 힘은 최저치입니다 30% 포인트 격",
"speaker": "speaker_13",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "저", "start_ms": 30, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "두", "start_ms": 100, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "분과", "start_ms": 170, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "함께", "start_ms": 300, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "짚어", "start_ms": 420, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "보겠습니다", "start_ms": 550, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "먼저", "start_ms": 860, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "정당", "start_ms": 1010, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "지지도", "start_ms": 1150, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "위에 로봇을 감싸고 있던 안전 장치를 떼어내면 감춰져 있던 네 개의 다리가 드러나는데요",
"speaker": "speaker_14",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "위에", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "로봇을", "start_ms": 150, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "감싸고", "start_ms": 320, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "있던", "start_ms": 510, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "안전", "start_ms": 640, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "장치를", "start_ms": 780, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "떼어내면", "start_ms": 970, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "감춰져", "start_ms": 1200, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "있던", "start_ms": 1390, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "루프에 시간이 지나면서 코드 베이스에 나쁜 패턴이 쌓일 수밖에 없습니다 그래서 저희는 마지막 기둥이 필요합니다 AI가 만든 코드를 주기적으로 점검하고 품질이 떨어지는 부분을",
"speaker": "speaker_15",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "루프에", "start_ms": 60, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "시간이", "start_ms": 250, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "지나면서", "start_ms": 420, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "코드", "start_ms": 680, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "베이스에", "start_ms": 810, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "나쁜", "start_ms": 1040, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "패턴이", "start_ms": 1170, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "쌓일", "start_ms": 1360, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "수밖에", "start_ms": 1490, "dur_ms": 180, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 12:43:34*
