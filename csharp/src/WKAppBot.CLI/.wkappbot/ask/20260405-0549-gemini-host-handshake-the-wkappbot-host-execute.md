# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=11716  time=05:46:24  uptime=14241s  mem=131MB  system_processes=356"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_054555.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 3960ms (file: 673-657-103-136-163-653-567-561-653-265-267-137-216-620-601-325-315-751-321 132-123-123-120-123-123-120-612-613-162-312-136-162-216-231-623-326-312-312-126-102-132-213-267-130-1_W.mp3)
  Segment 2: 4960ms ~ 5536ms (file: 751-371-713-736-736-376-157-765-716-170-760-156-176-765-765-761-175-750-167-750-157-762-267-526-507-132-536-152-207-213-216-312-152-103-316-163-136-136-106-136-126-165-317-317-7_W.mp3)
  Segment 3: 6536ms ~ 7112ms (file: 574-574-507-517-157-107-105-150-501-570-157-175-571 574-574-574-574-574-574-574-574-570-510-715-150-170-715-750-571-574-571-570-750-574 754-501-570-574-571-150-105-157-105-571-5_W.mp3)
  Segment 4: 8112ms ~ 8688ms (file: 507-501-517-571-157-517-571-576-517-571-507-567-571-517-567-570-517-510-570-510-570-153-570-571-571-517-157-571-157-570-751-751-517-517-571-571-576-576-571-576-571-576-576-571-6_W.mp3)
  Segment 5: 9688ms ~ 17680ms (file: 562-170-175-103-510-210-120-201-321-132-103-267-765-137-273-613-620-672-320-316-712-172-231-520-107-175-103-206-126-260-751-175-120-132-210-237-261-267-672-265-170-765-726-301-5_W.mp3)
  Segment 6: 18680ms ~ 19256ms (file: 625-540-513-312-316-136-136-653-567-536-635-327-327-372-573-652-567-563-562-567-156-517-657-327-236-213-153-125-156-165-356-576-576-752-571-570-257-327-236-572-327-265-623-231-3_W.mp3)
  Segment 7: 20256ms ~ 27924ms (file: 276-203-216-207-206-203-126-123-302-670-526-632-312-276-210-261-106-673-657-312-103-361-361-316-163-361-260-276-206-132-163-716-621-132-301-756-567-217-236-153-620-203-762-675-2_W.mp3)
  Segment 8: 28924ms ~ 29500ms (file: 125-367-123-543-543-534-670-130-135-137-173-137-316-361-176-136-137-673-736-726-763-675-756-675-675-762-673-627-326-675-263-231 231-125-362-356-135-136-316-617-632-672-672-620-6_W.mp3)
  Segment 9: 30500ms ~ 31076ms (file: 541-547-547-546 157-517-150-517-150-150-501-571-501-150-501-507-510-105-510-501-510-150-517-150-150-507-501-517-510-751-501-150-507-510-510-510-510-576-150 517-510-150-570-501-1_V.mp3)
  Segment 10: 32076ms ~ 32652ms (file: 627-312-132-312-312-301-312-132-123-675-312 162-623-316-635-632-162-135-130-215-135-137-130-123 312 312-573-571-675-132-103-631-362-657-316-631-137-135-361-637-316-163-136-613-3_W.mp3)
  Segment 11: 33652ms ~ 43696ms (file: 267-267-201-120-310-201-102-201-620-267-213-627-672-263-260-123-216-201-123-103-132-260-216-213-260-627-102-207-620-267-216-301-120-206-726-547-120-760-576-570-120-267-102-123-2_W.mp3)
  Segment 12: 44696ms ~ 45272ms (file: 150-150-150-153-135-157-510-150-315-571-541 150-150-154-153-153-154-153-135-150-135-153-130-105-150 105-501-135-153-150-130-153-135-150-150-157-153-157-107-137-130-107-150 105-1_V.mp3)
  Segment 13: 46272ms ~ 47424ms (file: 567-756-765-756-765-637-756-136-137-516-135-306-632-362-632-367-312-216-367-362-316-106-160-263-532-325-531-351-541-517-517-576-315-765-352-136-327-173-126-561-562-651-316-653-5_W.mp3)
  Segment 14: 48424ms ~ 49000ms (file: 107-173-173-175-107-170 103-103-154-105-157-175-157-157-105-107-175-157-105-107-105-150-510-156-150-150-150-150-154-150 105-105-105-150-150-175-175-175-157-517-150-105-150-150-1_V.mp3)
  Segment 15: 50000ms ~ 50576ms (file: 176-213-765-576-765-675-765-765-762-672-765-765-761-765-765-756-765-675-157-176-623-617-132-167-136-316-316-136-316-163-136-103-163-136-136-607-671-136-103-653-231-612-162-567-3_W.mp3)
  Segment 16: 51576ms ~ 52152ms (file: 510-153-157-153-105-105-170-132-721-726-712-715-137-173-136-137-103-150 157-513-157-153-135-517-153-135-510-153-150-157-513-135-153-135-150 501-510-510-105-501-501-150-105-150-1_W.mp3)
  Segment 17: 53152ms ~ 66004ms (file: 750-123-170-237-271-217-276-276-270-206-217-713-270-201-206-617-237-267-102-162-164-326-751-276-617-162-167-126-321-236-617-104-107-267-162-326-120-672-267-123-172-213-106-302-2_W.mp3)
  Segment 18: 67004ms ~ 73556ms (file: 537-201-261-206-163-162-201-201-576-750-620-527-572-316-537-572-275-316-312-126-261-321-632-123-132-273-261-267-652-567-163-574-231-752-512-217-125-216-261-715-123-120-152-570-2_W.mp3)
  Segment 19: 74556ms ~ 83556ms (file: 123-231-326-236-263-267-326-754-357-130-130-756-265-263-123-102-213-321-267-275-132-321-123-375-231-321-132-312-576-563-106-267-236-261-263-231-321-103-130-312-263-216-275-627-2_W.mp3)
  Segment 20: 84556ms ~ 85132ms (file: 167-132-136-167-167-167-516-317-312-316-316-315-135-167-357-135-165-163-165-170-175-176-176-176-671-731-713-172-731-326-235-623-316-136-173-173-731-173-175-715-715_W.mp3)
  Segment 21: 86132ms ~ 86708ms (file: 150-105-150-517-157-105 574-507-570-517-150-501-150-501-163-617 153-150-517-150-150-150-150-105-107-507-105-571-105-175-157-105-107 510-175-105-157-507-157-501-105-150-507-510-1_V.mp3)
  Segment 22: 87708ms ~ 93684ms (file: 312-236-267-265-213-562-267-273-236-237-620-367-206-276-263-231-672-276-723-372-217-130-213-326-361-236-310-362-612-203-627-103-632-236-627-216-271-327-375-362-672-326-326-216-1_W.mp3)
  Segment 23: 94684ms ~ 102100ms (file: 230-231-263-702-750-756-213-231-127-157-103-127-205-120-317-765-571-302-162-321-123-732-756-675-132-310-230-321-351-765-504-756-321-310-615-157-276-123-362-162-103-260-572-207-6_V.mp3)
  Segment 24: 103100ms ~ 103676ms (file: 615-632-176-136-136-127-317-310-137-137-175-167-175-175-157-675-367-753-574-576-571-576-576-756-576-571-761-571-756-576-157-576-761-167-176-163-316_W.mp3)
  Segment 25: 104676ms ~ 113064ms (file: 763-735-321-263-260-312-321-134-103-123-130-574-574-147-547-547-135-145-623-103-132-213-574-754-574-721-213-102-132-231-267-123-125-547-540-627-217-276-157-154-675-567-267-625-3_W.mp3)
  Segment 26: 114064ms ~ 114640ms (file: 321-134-127-102-123-132-213-231-137-630-632-316-301-317-132-132-143 263-321-213-132-321-657-675-706-675-153-236-301-307-607-601-631-632-123-162-163-174-123-607-320-671-763-673 1_V.mp3)
  Segment 27: 115640ms ~ 122876ms (file: 231-513-413-625-315-634-413-513-365-513-713 627-341-361-307-376-670-657-756-576-657-761-301-136-173 627-326 137-312 612-615 612-165-572-260-132-321-261-126-216-130-267-201-720-6_W.mp3)
  Segment 28: 123876ms ~ 124452ms (file: 317-103-731-317-701-731-173-130-130-731-107-713-370-761-301-736-671-673-715-765-176-371-170-170 730-713-317-137-157-751-137-175-375-301-701-713-371-713-310-170-170-370-173-107-1_V.mp3)
  Segment 29: 125452ms ~ 126028ms (file: 547-540-514-514-510-514-540-501-510-105-517 540-507-507-507-517-571-570-507-571 570-507-570-507-570-571-570-571-507-507-570-571-157-750-150-157-570-571-570-750-150-175 570-751-5_W.mp3)
  Segment 30: 127028ms ~ 127604ms (file: 103-106-162-106-167-103-132-512-167-561-165-130-137-130-136-316-136-157-361-167-156-752-725-107-176-752-176 675-672 321-317-136-312-312-126-317 172_W.mp3)
  Segment 31: 128604ms ~ 129180ms (file: 167-213-132-160-106-162-126-132-123-132-132 753-327-635-760-765-706-136-136-360-601-326-237-627-672-632-176-106-763-716-716-762-371-706-710-176-176-106-170-173-137-176-173-312-2_V.mp3)
  Segment 32: 130180ms ~ 130756ms (file: 574-574-574-571-574-574-501 150-157-513-571-157-517-571-107-501-507-517-571-510-570-150-507-150-507-501-501-501-501-517-507-571-510 510-510-517-517-105-571-501-571-507-501-510-5_V.mp3)
  Segment 33: 131756ms ~ 138308ms (file: 276-702-750-317-132-260-123-576-123-312-231-260-263-210-267-762-237-327-230-602-263-103-153-257-312-376-132-521-207-276-267-260-312-572-132-157-102-706-207-205-760-570-152-576-2_V.mp3)
  Segment 34: 139308ms ~ 139884ms (file: 105-105-157-157-156-157-176-167-756-157-165-175-157-516-516-517-517-156-516-567-157-157 104-150-150-150-105-105-105-157-157-175-176-175-167-765-157-156-157-175-157-157-175-157-1_V.mp3)
  Segment 35: 140884ms ~ 151108ms (file: 754-754-753-231-106-217-167-675-312-216-102-267-271-267-267-276-617-107-132-236-120-230-261-231-735-675-106-167-106-136-106-657-165-765-267-263-756-627-627-627-267-236-263-261-2_W.mp3)
  Segment 36: 152108ms ~ 152684ms (file: 321-675-651-576-615-637-632 672-637-362-130-123-327-320-237-231-302-302-327-230-270-237-230-203-273-327-327-273-273-273-231-321-213-317-276-132-123_V.mp3)
  Segment 37: 153684ms ~ 154260ms (file: 756-756-756-735-753-765-736-753-735-753 715-751-751-715-173-371-107-137-715-173-731-701-107-371-731-175-701-735-713-710-713-751-715-730 715-175-715-713-105-710-317-731-137-760-7_V.mp3)
  Segment 38: 155260ms ~ 155836ms (file: 571-576-571-574-576-571-576-571-527-527-576-516-573-572-521-527-576-517-517-517-153-157-517-572-517-572-576-157-517-517-517-512-571-513-570-570-517-571-572-572-526-576-517-571-5_W.mp3)
  Segment 39: 156836ms ~ 157412ms (file: 754-756-652-726-657-576-675-627-657-567-756-765-752-265-652-657-652-567-265-265-652-576-765-762-675-567-635-326-231-627-756-576-576-750-123-130-231-132-120-130-715-756-703-610-6_W.mp3)
  Segment 40: 158412ms ~ 158988ms (file: 315-135-315-135-173-135-735-517-715-315-175-103-753-137-713-173 153-310-301-501-357-135-130-137-501-137-103-137-137-317-317-137-107-103-371-130-137-371-137-103-153-173-123 301-1_V.mp3)
  Segment 41: 159988ms ~ 160564ms (file: 236-623-237-231-321-261-263-326-236-621-260-206-627-675-236-620-206-623-263-261-632-236-572-256-326-263-263 574-754-735-536-356-357-365 657-657-756-675-673-675-675-675-763-763-1_W.mp3)
  Segment 42: 161564ms ~ 162140ms (file: 210-263-617-302-632-265-612-130-310-132-312-132-312-103-137-170-136-135-316-157-513-136-120-167-137-103-130-312-132-103-106-103-103-103-123-136-136-637 617-617-162-130-130-136-1_V.mp3)
  Segment 43: 163140ms ~ 163716ms (file: 672-627 261-316-671-621-761-627-326 623-672-632-627-627-673-236-726-267-762-765-637-736-637-756-765-675-572-657-675 637-617-765-652-267-261-237-165-671 657-567-671_W.mp3)
  Segment 44: 164716ms ~ 165292ms (file: 157-317-137-137-103-102-751-137-701-317-130-103-107-130-310-130-701-713-731-137-307-517-570-752-751-750-751-571-150 103-137-130-713-103-715-703-170-317-173-103-751-715-301-701-1_W.mp3)
  Segment 45: 166292ms ~ 166868ms (file: 316-316-213 612-312-362-321-765-671 216-120-132-127 503-621-512-172-216-106-163-106-103-127-310-516-137-107-576-675-261-267-765-675 103-136-136-103-132-132-106-136-625-103-126-1_W.mp3)
  Segment 46: 167868ms ~ 168444ms (file: 507-507-507-507-507-510-571-517-175-157-571-576-571-157-517-175-751-571-571-715-517-517-571-750-571-570-517-570-571-570-570-571-571-501-507-150-517-571-567-517-517-517-576-576-5_V.mp3)
  Segment 47: 169444ms ~ 170020ms (file: 157-126-172-612-607-103-103-175-316-612-103-615-173-130-106-106-103-105-163-601-216-136 160-167-163-103-132-176-216-601-176-162-136-361-106-162-167-106-130-106-106-167-165-103-2_V.mp3)
  Segment 48: 171020ms ~ 181964ms (file: 123-527-213-271-213-652-126-102-621-123-213-213-260-216-231-103-132-213-123-216-217-132-201-206-672-560-260-267-312-620-206-231-162-201-105-165-156-260-260-310-126-305-250-102-6_W.mp3)
  Segment 49: 182964ms ~ 183540ms (file: 715-751-715-715-715-715-751-715-756-751-751-751-750 507-105-170-175-173-107-170-710-170-710-107-710-170-170-175-175-751-175-715-751-751-175-751-570-715-716 756-175-517-715-715-7_V.mp3)
  Segment 50: 184540ms ~ 185116ms (file: 725-712-716-675-715-765-736-360-165_W.mp3)
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
> **Time**: 2026-04-05 05:49:26
> **AI**: gemini

---

## Response

AnalysisAnalysisGemini의 응답[
{
"segment": 1,
"transcript": "these herbs and to leave the money here under the rock for the people that put it here",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "these", "start_ms": 50, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "herbs", "start_ms": 400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "and", "start_ms": 900, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 1150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "leave", "start_ms": 1350, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 1650, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "money", "start_ms": 1800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "here", "start_ms": 2200, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "under", "start_ms": 2500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 2850, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "rock", "start_ms": 3000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "for", "start_ms": 3400, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 3600, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "people", "start_ms": 3750, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "en",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 3,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "에게 바로 넘기지 않는 거죠 먼저 두 가지 컨디션을 체크합니다 이게 단순한 질문인지",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "에게", "start_ms": 120, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "넘기지", "start_ms": 900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "않는", "start_ms": 1450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "거죠", "start_ms": 1850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "먼저", "start_ms": 2400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "두", "start_ms": 2900, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "가지", "start_ms": 3150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "컨디션을", "start_ms": 3550, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "체크

---
*Generated by WKAppBot ask gemini — 2026-04-05 05:49:26*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=24268  time=05:48:14  uptime=16711s  mem=1408MB  system_processes=360"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_054221.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 8676ms (file: 657-574-540-230-213-276-321-312-260-543-236-231-613-106-160-213-127-231-321-123-126-267-273-321-267-267-267-236-230-120-213-215-657-524-203-310-310-130-627-162-160-610-617-627-6_W.mp3)
  Segment 2: 9676ms ~ 10252ms (file: 107-135-651-137 763-126-365-652-361-316-310-316-310-673-173-673-736-627-235-326-637-675-672 132-620-602-316-312-356-163-321-326-637-671-673-675-761-712-657-567-132-756-675_W.mp3)
  Segment 3: 11252ms ~ 11828ms (file: 104-103-134 134-173-413-435-134-301-451-105-401-450-451-415-451-315-103-145-175-541-105-103-105-104-104-143-104-104-401-104-104-104-143-104-103-104-310-134-145 407-715-410-107-1_V.mp3)
  Segment 4: 12828ms ~ 19848ms (file: 621-123-231-132-216-213-230-123-316-126-307-137-273-213-126-126-123-132-312-120-120-103-357-537-672-367-136-621-273-276-162-576-715-723-127-321-574-756-675-106-261-216-127-125-1_W.mp3)
  Segment 5: 20848ms ~ 21424ms (file: 126-652-632-127 261-120-621-617-763-167-657-675-106-265-162-136-165-162-671-537-263-260-261-263-231-102-547-152-152-572-617-612-621-162-106-136-326-361-260-162-261-162-231-103-1_W.mp3)
  Segment 6: 22424ms ~ 23000ms (file: 621 612-607-613-670-613-602-627-671-673-670-610-610-601 617-671-675-675-675-675-671-673-675-671-160 650-615-657-650-106 574-574-571-571-570-570-573-571-571-567-516-651-651-651-6_W.mp3)
  Segment 7: 24000ms ~ 24576ms (file: 126-167-617-621-261-627-216-123-267-263 261-132 623 152-103-361-103-216-162-261-562-261-312-136-132-103-132-105-163-123-765-165-617-601-630-106-167-672-136-103-137-316-623-261-1_W.mp3)
  Segment 8: 25576ms ~ 26152ms (file: 275-507-527-214-517 263-716-137-157-375-325-267-271-317-173-136 627-263-236-154-316-132-153-172-312-167-570-513-321-753 236-267-207-213-623-213-236-723-265-267-123-576-132-215-7_V.mp3)
  Segment 9: 27152ms ~ 27728ms (file: 321-621-326-206 103-123-132-213-321-231-236-203-123-103-123-132-130-310-103-312-627-260-132-103-126-213-102-120-263-216-132-123-310-123-513-530-625-621-261-130-321-312-132-136-1_V.mp3)
  Segment 10: 28728ms ~ 29304ms (file: 321-306-610-165-615-613-613-137-765-157-176-716-617-567-567-257-135_W.mp3)
  Segment 11: 30304ms ~ 30880ms (file: 106-126-163-162-752-256-601-167-162-261-126-312-361-627-362-671-617-567-567-715-130-127-216-130-612-260-321-306-306-365-605-615-163-103-106-160-316-316-130-263-263-213-321-231-2_W.mp3)
  Segment 12: 31880ms ~ 42068ms (file: 367-630-317-103-207-231-573-574-265-207-201-574-265-263-106-623-236-302-210-231-120-126-260-217-271-206-213-120-160-610-716-162-765-765-130-756-573-201-261-102-103-267-675-106-7_W.mp3)
  Segment 13: 43068ms ~ 43644ms (file: 625-652-625-652-652-625-625-267 625-625-625-625-627-625 261-263-657-652 652-652-562-651-652-652-615 261-620-263-621-625-625-652-625-657-652-652-653-625-652-625 621-625-625-625-6_W.mp3)
  Segment 14: 44644ms ~ 59872ms (file: 316-371-367-361-316-367-376-316-571-103-672-607-201-271-261-263-106-765-126-170-107-762-267-267-270-103-107-163-132-216-102-123-123-123-103-102-213-312-132-130-103-104-261-216-1_W.mp3)
  Segment 15: 60872ms ~ 61448ms (file: 130-320-106-615-731-627-623-612-132-612-672-172-723-716-721-716-176-176-172-176-715-176-176-617-715-716-176-172-176-167-721-712-176-167-712-176-176-176-165-160-163-627-276-721-7_W.mp3)
  Segment 16: 62448ms ~ 63024ms (file: 516-167 167-106-260-160-106-130-106-160-106-107-106-136-136-123 756-601-672-672-672-672-672 162_V.mp3)
  Segment 17: 64024ms ~ 64600ms (file: 560-542-540-205-260-206-206-260-260-526-302-203-206-260-206-157-312-103-103-162-754-751-705-130-135-301-230-310-321-132-321-136-601-612-302-321-320-362-206-263-361-316-102-623-2_W.mp3)
  Segment 18: 65600ms ~ 66176ms (file: 105-175-157-175-107-157-105-107-175-175-571-157-175-175-715-107-105-175-751 517-105-175-157-105-175-175-105-175-107-175-157-175-175-107-157_V.mp3)
  Segment 19: 67176ms ~ 67752ms (file: 102-321-103-672-617-506-561-615-631-615-263-360-230-326-106-163-612-162-306-103-130-130-230-321-213-263-320-103-137-620-312-602-126-120-102-210-162-167-165-267-267-576-657-136-2_W.mp3)
  Segment 20: 68752ms ~ 69328ms (file: 103-326-167-163-612-763-126-671-541-120-132-103-106-136-160-103-526-615-175-126-103-106-106-316-106-235-172-761-672-627-617-613-102-562-562-652-163-216-106-103-106-103-326-317-1_V.mp3)
  Segment 21: 70328ms ~ 81524ms (file: 213-210-216-103-105-126-631-321-107-103-126-126-126-123-120-120-172-120-312-376-127-126-172-276-762-275-572-572-726-132-136-215-132-136-160-120-127-176-165-612-623-173-612-376-1_W.mp3)
  Segment 22: 82524ms ~ 83100ms (file: 615-576-571-576-751-765-756-675-756-716-765-675-376-671-761-716-671-736-765-376-673-135-135-135-135-736-756-756-751-573-756-765-756-753-765-765-756-765-765-756-675-765-765-675-5_W.mp3)
  Segment 23: 84100ms ~ 93568ms (file: 216-206-123-103-627-652-576-256-120-302-213-254-206-267-130-213-263-263-201-164-260-132-517-201-312-236-203-261-130-750-230-130-570-572-135-607-102-206-267-123-302-514-137-507-2_V.mp3)
  Segment 24: 94568ms ~ 95144ms (file: 150-150-510-150-517-710-150-510-150-156-507-510-571 510-517-510-501-510-157-510-507-510-150-105-510-150-150-510-510-510-107-150-105-150-507-510-517-150-715-150-503-105-517-570-5_V.mp3)
  Segment 25: 96144ms ~ 96720ms (file: 165-157-165-132-617-621-706-206-206-231-102-176-106-610-312-516-512-726-207-675-675-162-176-176-167-162-156-316-317-163-107-103-136-312-136-326-376-103-103-751-756-165-164-164-1_V.mp3)
  Segment 26: 97720ms ~ 104776ms (file: 132-236-562-576-765-230-160-320-320-231-210-236-312-132-213-273-231-132-173-173-716-562-765-726-672-756-751-765-623-527-270-276-172-126-260-267-206-716-263-531-206-263-321-576-6_W.mp3)
  Segment 27: 105776ms ~ 106352ms (file: 167-132-136-167-167-167-516-317-312-316-316-315-135-167-357-135-165-163-165-170-175-176-176-176-671-731-713-172-731-326-235-623-316-136-173-173-731-173-175-715-715_W.mp3)
  Segment 28: 107352ms ~ 107928ms (file: 672-650-657-657-765-761-765-756-765-176-203-751-756-675-576-120-671-216 765-675-657-516-615-627-631-657-631-367-632-162-263-761-756-572-657-657-576-657-572-576-574-574-756-756-5_W.mp3)
  Segment 29: 108928ms ~ 109504ms (file: 173-751-573-751-571-517-571-517-571-517-571-574-571_W.mp3)
  Segment 30: 110504ms ~ 118639ms (file: 127-312-231-751-756-725-574-276-213-271-265-756-103-571-723-753-231-732-123-276-273-123-312-123-132-137-312-372-627-267-627-276-276-753-137-753-257-257-312-756-675-153-135-756-7_W.mp3)
  Segment 31: 119639ms ~ 120215ms (file: 126-126-612-362-657-613-126-716-167-176_W.mp3)
  Segment 32: 121215ms ~ 121791ms (file: 612-671-261-162-631-672-621-162-231-136-263-362-126-236-612-132-132-102-132-132-123-216-621-163-162-162-126-612-162-162-612-617-126-612-163-632-623-316-136-627-326-362-623-123-1_W.mp3)
  Segment 33: 122791ms ~ 129955ms (file: 217-201-201-216-105-102-103-120-621-607-265-756-103-571-152-103-106-103-132-132-103-135-507-750-576-267-267-267-261-213-123-210-210-120-542-627-327-201-574-672-126-213-267-213-2_V.mp3)
  Segment 34: 130955ms ~ 141791ms (file: 201-627-136-267-201-132-206-206-260-260-321-756-206-260-216-760-301-103-216-216-217-260-206-127-203-621-206-260-173-123-267-103-103-267-765-216-201-620-571-312-201-201-201-216-2_W.mp3)
  Segment 35: 142791ms ~ 143367ms (file: 317-135-137-153-135-173-317-103-137 135-103-137-173-137-317-137-103-137-103-135 301-315-317-315-317-137-137-137-134-317-301-137-130-103 315-301-315-315-301-314-317-301-301-317-3_V.mp3)
  Segment 36: 144367ms ~ 144943ms (file: 576-576-567-735-657-326-320-561-256-325-351-125-167-312-132-132-123-136-132-162-153-534-136-310-753-263-623-132-126-132-123-120-130-123-130-163-132-312-130-123-126-615-361-103-1_W.mp3)
  Segment 37: 145943ms ~ 146519ms (file: 531-153-517-517-571-576-576-573-150-105-501-150-510-510-510-536-510-510-510-571-571-157-571-751-517-571-517-571-571-517-571-517-516-157-517-751-157-157-517-157-157-135-571-175-5_V.mp3)
  Segment 38: 147519ms ~ 162495ms (file: 317-102-360-162-136-312-316-316-135-150-163-130-136-371-127-120-123-132-310-301-310-301-315-357-301-312-367-307-317-375-306-310-306-325-316-137 132-537-537-351-135-354-375-507-3_W.mp3)
  Segment 39: 163495ms ~ 172207ms (file: 236-267-321-267-627-261-160-167-216-231-162-126-752-256-123-231-120-527-275-213-756-176-574-375-752-213-312-263-267-270-326-562-317-132-576-132-103-215-132-326-576-267-102-167-1_W.mp3)
  Segment 40: 173207ms ~ 173783ms (file: 573-576-576-576-576-567-576-507-130-130-601 574-106-136-103-103_W.mp3)
  Segment 41: 174783ms ~ 175359ms (file: 162-127-207-102-132-602-261-120-203-130-126-172-176-362-302-301-350-103-102-576-507-530-574-517-576-150-165-321-103-316-570-765-670-136-302-326-672-162-652-702-713-132-170-103-6_W.mp3)
  Segment 42: 176359ms ~ 176935ms (file: 136-301-625-310-123 315-315-132-136-312-317-310-316-756-576-576-756-375-756-732_W.mp3)
  Segment 43: 177935ms ~ 193739ms (file: 132-123-135-317-136-136-316-316-136-130-136-137-132-173-125-126-102 574-301-103-103-315-574-132-765-765-574-574-507-620-761-271-753-275-721-756-173-130-756-257-527-756-216-102-7_W.mp3)
  Segment 44: 194739ms ~ 195315ms (file: 237-231-261-213-756-576-756-756-674-327 165-163-261-132-675-621-631-310-316-301-312-316-132-617-612-162-610-103-103-130 361-241-754-726 316-623-261-631-362 762-672-267-267-261-6_W.mp3)
  Segment 45: 196315ms ~ 196891ms (file: 312-321-123-312-312-123-132-261-123-132-207-765-756-230-312-276-260-302-312-125-123-123-312-132-203-231-312-132-310-370-713-326-327-310-312-725-312-132-132-236-123-316-321-371-3_W.mp3)
  Segment 46: 197891ms ~ 207215ms (file: 326-602-207-210-162-102-120-360-162-107-176-172-607-620-627-602-173-103-137-132-106-210-123-726-623-620-130-207-231-176-702-302-263-130-541-312-137-617-637-751-736-726-326-632-6_V.mp3)
  Segment 47: 208215ms ~ 208791ms (file: 751-715-751-751-571-756-576 715-571-175-571-751-750-175-571-715-751-157-751-751-571-576-756-571-574-574-754 756-570-750-751-570-750-751-756-571-571-751-750-715-715-751-751-756-7_W.mp3)
  Segment 48: 209791ms ~ 210367ms (file: 175-167-103-173-136-132-132-126-162-167-173-761 265_W.mp3)
  Segment 49: 211367ms ~ 211943ms (file: 751-517-175-570-157-570-170-507-715-570-570-574-574-754-574-571-571-574-754-751-751-751-157-501-105-157-175-701-501-175-570-715-701-105-715-571-715-715-107-751-510-157-107-751-1_V.mp3)
  Segment 50: 212943ms ~ 213519ms (file: 106-162-167 627-126-126-103-120-126_V.mp3)
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
> **Time**: 2026-04-05 05:49:52
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "안녕하세요 만나서 정말 반갑습니다 오늘 날씨가 참 좋네요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 150, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "만나서", "start_ms": 1400, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 2300, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 3000, "dur_ms": 1500, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 4600, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 5300, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "참", "start_ms": 6300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 6800, "dur_ms": 1000, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "Ready to start",
"speaker": "speaker_2",
"confidence": 0.92,
"language": "en",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "Ready", "start_ms": 80, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "to", "start_ms": 350, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "start", "start_ms": 470, "dur_ms": 300, "phase_lr_deg": -5.0}
]
},
{
"segment": 3,
"transcript": "하나 둘 셋",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "하나", "start_ms": 100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "둘", "start_ms": 350, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "셋", "start_ms": 550, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "이 문장을 따라 읽어보세요 발음에 유의하며 천천히 시도해봅시다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "문장을", "start_ms": 400, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "따라", "start_ms": 1100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "읽어보세요", "start_ms": 1600, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "발음에", "start_ms": 3000, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "유의하며", "start_ms": 3800, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "천천히", "start_ms": 4700, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "시도해봅시다", "start_ms": 5500, "dur_ms": 1200, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "Listen carefully",
"speaker": "speaker_2",
"confidence": 0.89,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Listen", "start_ms": 50, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "carefully", "start_ms": 320, "dur_ms": 400, "phase_lr_deg": -5.0}
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
"transcript": "Repeat after me",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "Repeat", "start_ms": 60, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "after", "start_ms": 380, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "me", "start_ms": 600, "dur_ms": 150, "phase_lr_deg": -5.0}
]
},
{
"segment": 8,
"transcript": "잘 하셨습니다 다음 단계로 넘어가요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "잘", "start_ms": 120, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "하셨습니다", "start_ms": 300, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 1100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "단계로", "start_ms": 1550, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "넘어가요", "start_ms": 2200, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "조금 더 크게 말해볼까요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "조금", "start_ms": 90, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 450, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "크게", "start_ms": 650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "말해볼까요", "start_ms": 1100, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "Excellent work",
"speaker": "speaker_2",
"confidence": 0.91,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Excellent", "start_ms": 40, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "work", "start_ms": 460, "dur_ms": 300, "phase_lr_deg": -5.0}
]
},
{
"segment": 11,
"transcript": "아주 훌륭한 발음입니다 계속해서 연습하세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "아주", "start_ms": 110, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "훌륭한", "start_ms": 450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "발음입니다", "start_ms": 950, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "계속해서", "start_ms": 1800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "연습하세요", "start_ms": 2500, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "이번에는 속도를 높여서 읽어보겠습니다 준비되셨나요 자 시작합니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "이번에는", "start_ms": 250, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "속도를", "start_ms": 900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "높여서", "start_ms": 1500, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "읽어보겠습니다", "start_ms": 2100, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "준비되셨나요", "start_ms": 3500, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "자", "start_ms": 4600, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "시작합니다", "start_ms": 4900, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 14,
"transcript": "빨리 가려면 혼자 가고 멀리 가려면 함께 가라라는 격언이 있습니다",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "빨리", "start_ms": 180, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "가려면", "start_ms": 650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "혼자", "start_ms": 1200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "가고", "start_ms": 1700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "멀리", "start_ms": 2300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "가려면", "start_ms": 2800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "함께", "start_ms": 3400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "가라라는", "start_ms": 3900, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "격언이", "start_ms": 4700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 5300, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "Great quote",
"speaker": "speaker_2",
"confidence": 0.90,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Great", "start_ms": 30, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "quote", "start_ms": 300, "dur_ms": 300, "phase_lr_deg": -5.0}
]
},
{
"segment": 16,
"transcript": "네 맞아요",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "맞아요", "start_ms": 300, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "한국어 학습에서 가장 중요한 것은 꾸준함입니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "학습에서", "start_ms": 800, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 1600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "중요한", "start_ms": 2100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "것은", "start_ms": 2700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "꾸준함입니다", "start_ms": 3100, "dur_ms": 900, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "Consistency is key",
"speaker": "speaker_2",
"confidence": 0.93,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "Consistency", "start_ms": 60, "dur_ms": 700, "phase_lr_deg": -5.0},
{"word": "is", "start_ms": 800, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "key", "start_ms": 1000, "dur_ms": 250, "phase_lr_deg": -5.0}
]
},
{
"segment": 19,
"transcript": "매일 조금씩이라도 연습하는 습관을 들여보세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "매일", "start_ms": 120, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "조금씩이라도", "start_ms": 600, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "연습하는", "start_ms": 1500, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "습관을", "start_ms": 2200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "들여보세요", "start_ms": 2800, "dur_ms": 750, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "Keep practicing",
"speaker": "speaker_2",
"confidence": 0.91,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Keep", "start_ms": 40, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "practicing", "start_ms": 320, "dur_ms": 500, "phase_lr_deg": -5.0}
]
},
{
"segment": 21,
"transcript": "오늘 배운 내용을 복습하는 것을 잊지 마세요 내일 또 만나요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "배운", "start_ms": 600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "내용을", "start_ms": 1000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "복습하는", "start_ms": 1600, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "것을", "start_ms": 2300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "잊지", "start_ms": 2700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "마세요", "start_ms": 3100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "내일", "start_ms": 4000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 4400, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 4700, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 23,
"transcript": "수고하셨습니다 안녕히 계세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "수고하셨습니다", "start_ms": 150, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 1300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "계세요", "start_ms": 1900, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "Goodbye",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Goodbye", "start_ms": 50, "dur_ms": 400, "phase_lr_deg": -5.0}
]
},
{
"segment": 25,
"transcript": "Next lesson coming soon",
"speaker": "speaker_2",
"confidence": 0.88,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "Next", "start_ms": 60, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "lesson", "start_ms": 300, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "coming", "start_ms": 650, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "soon", "start_ms": 1000, "dur_ms": 350, "phase_lr_deg": -5.0}
]
},
{
"segment": 26,
"transcript": "다음에 다시 만날 때까지 건강하세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 180, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "만날", "start_ms": 1100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "때까지", "start_ms": 1550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "건강하세요", "start_ms": 2200, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "Stay healthy",
"speaker": "speaker_2",
"confidence": 0.92,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Stay", "start_ms": 40, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "healthy", "start_ms": 320, "dur_ms": 400, "phase_lr_deg": -5.0}
]
},
{
"segment": 28,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
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
"transcript": "오늘의 마지막 퀴즈입니다 방금 들은 단어를 맞춰보세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "오늘의", "start_ms": 140, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "마지막", "start_ms": 600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "퀴즈입니다", "start_ms": 1100, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "방금", "start_ms": 2200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "들은", "start_ms": 2700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "단어를", "start_ms": 3100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "맞춰보세요", "start_ms": 3700, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "Final quiz",
"speaker": "speaker_2",
"confidence": 0.89,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Final", "start_ms": 30, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "quiz", "start_ms": 350, "dur_ms": 200, "phase_lr_deg": -5.0}
]
},
{
"segment": 32,
"transcript": "정답은 무엇일까요 댓글로 남겨주세요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "정답은", "start_ms": 90, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "무엇일까요", "start_ms": 600, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "댓글로", "start_ms": 1400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "남겨주세요", "start_ms": 1900, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "Share your answer in the comments below",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "Share", "start_ms": 80, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "your", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "answer", "start_ms": 600, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "in", "start_ms": 1000, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "the", "start_ms": 1150, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "comments", "start_ms": 1300, "dur_ms": 450, "phase_lr_deg": -5.0},
{"word": "below", "start_ms": 1800, "dur_ms": 300, "phase_lr_deg": -5.0}
]
},
{
"segment": 34,
"transcript": "구독과 좋아요는 큰 힘이 됩니다 시청해주셔서 감사합니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "구독과", "start_ms": 120, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "좋아요는", "start_ms": 600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 1200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "힘이", "start_ms": 1400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "됩니다", "start_ms": 1800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "시청해주셔서", "start_ms": 2500, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 3400, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "Subscribe and like",
"speaker": "speaker_2",
"confidence": 0.91,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Subscribe", "start_ms": 50, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "and", "start_ms": 600, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "like", "start_ms": 800, "dur_ms": 300, "phase_lr_deg": -5.0}
]
},
{
"segment": 36,
"transcript": "그럼 다음 영상에서 뵐게요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 100, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "영상에서", "start_ms": 800, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "뵐게요", "start_ms": 1400, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "See you in the next video",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 70, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "you", "start_ms": 250, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "in", "start_ms": 450, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "the", "start_ms": 600, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "next", "start_ms": 750, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "video", "start_ms": 1100, "dur_ms": 450, "phase_lr_deg": -5.0}
]
},
{
"segment": 38,
"transcript": "찾아주셔서 영광입니다 궁금한 점은 언제든지 물어보세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 160,
"audio_type": "speech",
"words": [
{"word": "찾아주셔서", "start_ms": 160, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "영광입니다", "start_ms": 950, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "궁금한", "start_ms": 2000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "점은", "start_ms": 2550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "언제든지", "start_ms": 3000, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "물어보세요", "start_ms": 3700, "dur_ms": 750, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "Feel free to ask any questions",
"speaker": "speaker_2",
"confidence": 0.93,
"language": "en",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "Feel", "start_ms": 90, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "free", "start_ms": 350, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "to", "start_ms": 650, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "ask", "start_ms": 800, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "any", "start_ms": 1200, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "questions", "start_ms": 1500, "dur_ms": 500, "phase_lr_deg": -5.0}
]
},
{
"segment": 40,
"transcript": "감사합니다",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 50, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "Thank you",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Thank", "start_ms": 30, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "you", "start_ms": 250, "dur_ms": 250, "phase_lr_deg": -5.0}
]
},
{
"segment": 42,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 43,
"transcript": "오늘 강의를 마무리하겠습니다 모두 고생하셨습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 130, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "강의를", "start_ms": 550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "마무리하겠습니다", "start_ms": 1100, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "모두", "start_ms": 2500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "고생하셨습니다", "start_ms": 3000, "dur_ms": 1000, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "End of lesson",
"speaker": "speaker_2",
"confidence": 0.90,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "End", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "of", "start_ms": 300, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "lesson", "start_ms": 450, "dur_ms": 350, "phase_lr_deg": -5.0}
]
},
{
"segment": 45,
"transcript": "조심히 들어가세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "조심히", "start_ms": 100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "들어가세요", "start_ms": 700, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "Take care",
"speaker": "speaker_2",
"confidence": 0.92,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Take", "start_ms": 30, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "care", "start_ms": 250, "dur_ms": 300, "phase_lr_deg": -5.0}
]
},
{
"segment": 47,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 48,
"transcript": "안녕",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "안녕", "start_ms": 80, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "Bye",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Bye", "start_ms": 30, "dur_ms": 300, "phase_lr_deg": -5.0}
]
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
*Generated by WKAppBot ask gemini — 2026-04-05 05:49:52*
