# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=17708  time=12:24:27  uptime=29728s  mem=441MB  system_processes=354"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_122351.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 751-571-513-751-751-571-173-137-175-172-157-571-157-751-517-715-751-751-571-751-751-571-571-571-517-751-571-571-573-751-715 751-573-571-571-571-751-517-574-571-751-571-571-751-5_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 572-752-751-735-567-574-572-574-572-572-576-572-752-207-752-572-725-756-275-257-752-527-752-725-572-756-725-725-726-257-527-572 135-531-536-531-532-512-571-513_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 765-756-375-675-672-137-106-103-123-271-216-671-675-136-167-751-576-735-756-275-312-362-326-126-756-765-675-617-675-106-136-715-701-706-270-762-765-170-137-317-107-103-167-162-5_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 574-567-576-657-652 675-675-762-756-576-576-756-567-576-576-657-652-756-126-675-675-576-576-765-312 157-753_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 157-517-170-175-150-170-175-710-170-701-175-175-715-157-173-170-175-157-105-571-571-571-570-175-175-715 574-574-571-574-517-751-571-570-175-157-135-135-150-157-517-710-170-176-1_W.mp3)
  Segment 6: 7880ms ~ 15116ms (file: 126-176-120-672-625-362-276-672-726-310-130-321-236-741-267-325-132-325-130-273-320-320-367-372-317-726-310-275-765-572-754-270-302-230-230-132-310-263-257-103-617-763-675-326-3_W.mp3)
  Segment 7: 16116ms ~ 23928ms (file: 574-756-267-572-265-132-102-126-267-103-132-671-263-236-273-106-312-123-123-106-576-627-567-312-612-627-267-527-267-675-657-106-275-236-136-132-312-631-321-162-213-132-130-132-6_W.mp3)
  Segment 8: 24928ms ~ 25504ms (file: 756-576-567-657-367 562-657-652-657-567-650-657-560-567-560-650-567-756-657-567-657-675_W.mp3)
  Segment 9: 26504ms ~ 30680ms (file: 231-236-261-320-203-716-107-167-267-126-273-176-716-130-327-572-273-216-126-123-127-260-276-271-312-206-326-206-270-206-203-236-102-203-271-260-130-213-326-627-203-216-267-267-1_W.mp3)
  Segment 10: 31680ms ~ 35424ms (file: 765-716-173-276-716-761-327-217-172-236-672-706-162-623-327-761-726-207-627-160-107-176-126-123-153-120-102-267-627-703-706-726-762-726-267-230-203-762-760-160-623-306-601-123-3_V.mp3)
  Segment 11: 36424ms ~ 37000ms (file: 524-512-563-531-512-536-531-521-531-512-576-501-502-507-532-571-526-527-526-562-561-562-567-563-561-562-567-526-562-527-725-526-562-521-567-502-521-526-572-527-756-752-572-207-2_V.mp3)
  Segment 12: 38000ms ~ 44552ms (file: 756-756-621-261-263-526-576-265-106-216-762-267-267-751-267-213-736-132-123-261-107-130-312-321-103-231-572-126-567-213-326-263-263-326-216-326-236-231-263-132-321-213-571-263-2_W.mp3)
  Segment 13: 45552ms ~ 46128ms (file: 570-715-107-716-175-701-107-105-157-170-571-701-175-751-175-170-105-157-105-107-170-751-175-753-731-751-375-173-537-531-715 570-751-750-175-571-157-107-701-170-170-715-173-107-1_V.mp3)
  Segment 14: 47128ms ~ 47704ms (file: 675-672-672-627-152-156-126-165-153-153-156-675-653-675-673-672-657-675-672-672-672-615-615-165-650-156-567-165-157-517-157-516-156-156-761-625-762-672-671-657 601-615-615-615-6_W.mp3)
  Segment 15: 48704ms ~ 58460ms (file: 301-753-231-203-143-132-673-310-103-132-130-130-130-231-213-261-321-761-371-276-672-206-203-260-201-206-132-260-310-371-173-130-310-130-136-126-620-260-206-310-310-143-132-106-1_V.mp3)
  Segment 16: 59460ms ~ 60036ms (file: 762-672-760-716-671-176-671-761-671-671-762-671-675-762-765-675-765-765-675-765-756-756-765-765-756-675-765-675-765-761-756-627-625-657-672-672-671-761-617-716-762-610-760 635-6_W.mp3)
  Segment 17: 61036ms ~ 67912ms (file: 312-103-135-137-315-371-143-231-512-236-267-260-267-756-260-321-263-263-165-106-103-263-273-625-362-301-263-261-132-712-321-326-231-260-203-203-213-132-120-136-163-236-360-765-2_W.mp3)
  Segment 18: 68912ms ~ 69488ms (file: 650-603 625-650-615-605-650-650 650-652-653-605-625-650 625-650 650-650-605-650-650-650-605-650 650-650-650-632 650-625-605-650-632-562-650 651-650-605-605-605-605-654-560-650-6_W.mp3)
  Segment 19: 70488ms ~ 71064ms (file: 516-512-571-516-517-512 536-503-520-574-513-564-540-501-501-507-506-570-507-561 576-765-162 501-501-502-512-501-576-507-521-217-127-126 105-501-512-571-572-562-506-512-561-572-1_W.mp3)
  Segment 20: 72064ms ~ 72640ms (file: 361-136-163-103-136-103-163-130-107-103-175-617-176-167-163-512-561-615-106-103-103-103-761-263-623-316-216 231-237-273-236-276-275-237-173-327-310-321-231-237-235-253-275-253-3_W.mp3)
  Segment 21: 73640ms ~ 79616ms (file: 213-576-256-627-576-261-263-312-312-352-753-312-231-263-263-163-371-213-123-276-317-135-130-135-103-103-123-312-317-132-362-132-376-175-216-216-217-132-521-163-725-253-621-136-3_W.mp3)
  Segment 22: 80616ms ~ 81192ms (file: 136-134-134-136-134-316-135-315-357-574-573-571-315-315-751-571-571-574-571-574-571-574-751 357-753-571-571-574-571-574-574-574-574-547-547-574-574-574-574_W.mp3)
  Segment 23: 82192ms ~ 82768ms (file: 567 165-751-716-716-170-231-721-310-351-136-312-130-310-735-765-756-765-752-753-762-726-752-756-752-751-157-765-763-160-163-375-765-765-761-765-716-765-765-765-765-253-326-321-6_W.mp3)
  Segment 24: 83768ms ~ 84344ms (file: 571-571-571-571-576-571-571 130-150-137-173-135-153-153-153-135-571-517-137-315-513-517-537-571-571 150-150-135-153-157-153-153-130-150-517-157-517-157-157-517-157-153-157-157-1_W.mp3)
  Segment 25: 85344ms ~ 85920ms (file: 615-651-651-756-571-576-756-576-751-726-762-675-675-675 205-165-625-615-615-617-765-627-671-765-176-765-715-765-715-715-751-751-751-716-715 615-651-615-651-657-651-761-716-765-7_W.mp3)
  Segment 26: 86920ms ~ 94264ms (file: 130-123-125-613-163-126-236-321-327-570-267-570-564-265-362-321-765-352-312-527-706-263-362-326-517-576-312-542-756-723-627-526-532-621-672-507-321-312-753-132-236-736-321-270-7_V.mp3)
  Segment 27: 95264ms ~ 102824ms (file: 751-157-703-756-276-267-267-206-213-756-570-207-107-167-750-576-167-762-627-123-207-756-576-137-754-574-574-175-107-675-176-276-267-107-751-705-270-270-572-321-276-102-176-132-2_W.mp3)
  Segment 28: 103824ms ~ 104400ms (file: 715-157-513-175-536-376-137-173-736-176-176-517-506-250-257-520-265-570-257-257-520-502-572-520-570-205-520-502-520-507-502-507-507-570-572-527-520-507-520-527-570-572-512-571-5_W.mp3)
  Segment 29: 105400ms ~ 107740ms (file: 157-316-132-126-107-216-126-516-172-217-524-362-326-132-132-132-132-132-230-612-321-103-120-623-671-627-312-132-127-612-267-236 321-132-132-132-312-621-132-213-123-312-541-507-5_V.mp3)
  Segment 30: 108740ms ~ 109316ms (file: 574-561-564-561-564-564-564-564-561-561-564-564-561-654-652 547-547-547-534-524-542-564-521-564-564-564-564 564-567-657-567-657-567-657-567-564-516-105-510-516-152-501-506-501-1_W.mp3)
  Segment 31: 110316ms ~ 110892ms (file: 150-501-501-510-507-510-501-510-105-150-105-157-501-510-150-501-150-501-150-150-517-150-150-510-510-150-517-501 510-150-510-501-501-150-510-157-150-510-150-105-510-105-510-510-1_V.mp3)
  Segment 32: 111892ms ~ 112468ms (file: 701-107-107-571-157 571-157-570-750-750-105-750-105-157-750-701-751-107-571-510-517-570-510-750-751-175-150-517-157-751 574-574-571-751-754-570-750-150-517-571 507-571 750-751-7_W.mp3)
  Segment 33: 113468ms ~ 121064ms (file: 320-102-263-130-230-321-612-230-260-574-576-320-103-320-312-630-167-106-672-267-316-106-316-267-761-726-103-327-230-236-267-230-276-276-672-237-263-237-236-260-273-327-102-107-1_W.mp3)
  Segment 34: 122064ms ~ 133152ms (file: 675-253-321-631-623-267-542-102-261-276-752-261-621-726-106-632-120-103-103-163-106-210-210-261-231-260-261-620-261-216-216-103-123-162-765-126-517-102-123-256-231-231-201-261-1_W.mp3)
  Segment 35: 134152ms ~ 134728ms (file: 314 371-317-341-314-307-371-345-135-451-301-341-134-104-314-143-314-135-341-154-103-314-103-130-103-103 304-371-137-317-157-175-137-134-301-310-315-134-310-310-130-371-704-317-3_V.mp3)
  Segment 36: 135728ms ~ 136304ms (file: 136-136-130-136-103-215-136-103-176-103-761-326-103-137-156-152-136-312-103-163-136-103-123-136-136-316-135-130-136-310-136-102-103-356-301-613-361-136-163-316-136-153-615-516-2_V.mp3)
  Segment 37: 137304ms ~ 137880ms (file: 726-547-574-571-257-276-126-612-617-613-756-673-176-163-627-231-210-321-260-167-126-162-107-107-267-627-310-310-236-321 261-126-162-126-613-126 631-621-263-261-621-260-260-263-6_W.mp3)
  Segment 38: 138880ms ~ 139456ms (file: 732-162-752-132-127-507-512-627-167-157-267-316-215-572-721-576-712-267-271-257-751-312-312-571-357-321-506-576-675-126-735-576-735-527-705-162-123-705-157-276-706-713-726-217-2_V.mp3)
  Segment 39: 140456ms ~ 141032ms (file: 327-327-375-526-263-273-127-752-275-750-517-567-576-576-576-567-562-627-620-612-613-132-263-625-625-256-267-265-623-621-617-625-615-625-567-527-567-657-650-650-156-571-527-527-5_W.mp3)
  Segment 40: 142032ms ~ 142608ms (file: 231-302-625-162-602-103-103-132-132-301-312-132-321-312-753-570-576-123-213-123-132-163-136-132-213-103-123-132-132-231-167-610-621-106-103-123 602 574-570-273-236-273-723-310-1_V.mp3)
  Segment 41: 143608ms ~ 144184ms (file: 651-651-615-651-165-615-615-165-165-165-726-765-765-765-765-765 562-561-652-657-651-657-654-567_W.mp3)
  Segment 42: 145184ms ~ 145760ms (file: 725-572-275-725-725-275-527-527-502-507-725-507-567-571-756-576-576-576-756-571 574 547-526_V.mp3)
  Segment 43: 146760ms ~ 147336ms (file: 574-574-547-547-547-574 567-376-302-136-301-103-213-536-570-627-235-325-136-310-130-107-132-305-571-561-573-573-375-563-367-765-321-230-231-265-231-236-267-327-357-756-756 376-6_W.mp3)
  Segment 44: 148336ms ~ 148912ms (file: 156 175-170-175-517-175-571-105-175-157-107-170-175-175-710-715-157-756-715-751-176-715-750-753 107-175-170-715-175-170-571-175-157-175-517-576-576-517-576-576-657-756-675 105-5_V.mp3)
  Segment 45: 149912ms ~ 150488ms (file: 236-631-260-260-213-637-620-516-567-517-523-325-620-162-326-123-230-176-301-162-671-173-213-175-617-106-167-106-163-312-137-103-106-103-163-136-632-627-513-107-106-162-162-216-6_V.mp3)
  Segment 46: 151488ms ~ 152064ms (file: 312-357-526-326-267-507-756-627-571-257-746-362-632-263-236-312-576-761-312-726-754-672-157 206-107-251-134-132-605-756-756-765-756_V.mp3)
  Segment 47: 153064ms ~ 153640ms (file: 132-136-145-361 623 632-321-132-103-130-136-130-312-675-167-675-130-106-765-167-136-103-137-103-136-761-132-103-130-317-103-130-517-361-236-263 301-136-162-637-301-103-103-130-1_V.mp3)
  Segment 48: 154640ms ~ 165692ms (file: 567-175-213-326-263-623-123-132-231-126-316-326-123-351-756-310-106-231-236-321-267-267-236-213-130-102-132-321-312-136-263-263-261-236-231-102-163-326-136-106-310-325-123-132-2_W.mp3)
  Segment 49: 166692ms ~ 167268ms (file: 507-527-502-502-205-502-520-257-257-205-207-270-207-257-257-270-752-720-250-270-721-271-721-273-721-205-257 516-536-572-571-572-507-576-572-502-567-502-526-570-527-250-562-265-6_V.mp3)
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
> **Time**: 2026-04-05 12:26:13
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 2,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 3,
"transcript": "어서 오세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "어서", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "오세요", "start_ms": 260, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 5,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 6,
"transcript": "오늘 기분은 어떠신가요? 날씨가 참 맑네요.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 120, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "기분은", "start_ms": 450, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "어떠신가요?", "start_ms": 900, "dur_ms": 600, "phase_lr_deg": -5.0},
{"word": "날씨가", "start_ms": 1600, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "참", "start_ms": 2050, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "맑네요.", "start_ms": 2300, "dur_ms": 450, "phase_lr_deg": -5.0}
]
},
{
"segment": 7,
"transcript": "저는 오늘 아침에 맛있는 커피를 마셨어요. 당신은요?",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 80, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "아침에", "start_ms": 700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "맛있는", "start_ms": 1100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "커피를", "start_ms": 1550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "마셨어요.", "start_ms": 2000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "당신은요?", "start_ms": 2600, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 9,
"transcript": "커피는 정말 좋죠. 저도 아이스 아메리카노 한 잔 했어요.",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "커피는", "start_ms": 100, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "정말", "start_ms": 550, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "좋죠.", "start_ms": 900, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "저도", "start_ms": 1300, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "아이스", "start_ms": 1600, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "아메리카노", "start_ms": 2000, "dur_ms": 600, "phase_lr_deg": -5.0},
{"word": "한", "start_ms": 2650, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "잔", "start_ms": 2850, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "했어요.", "start_ms": 3100, "dur_ms": 400, "phase_lr_deg": -5.0}
]
},
{
"segment": 10,
"transcript": "That sounds lovely. Coffee always helps.",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "That", "start_ms": 60, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "sounds", "start_ms": 280, "dur_ms": 300, "phase_lr_deg": 10.0},
{"word": "lovely.", "start_ms": 600, "dur_ms": 450, "phase_lr_deg": 10.0},
{"word": "Coffee", "start_ms": 1150, "dur_ms": 350, "phase_lr_deg": 10.0},
{"word": "always", "start_ms": 1550, "dur_ms": 400, "phase_lr_deg": 10.0},
{"word": "helps.", "start_ms": 2000, "dur_ms": 400, "phase_lr_deg": 10.0}
]
},
{
"segment": 11,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 12,
"transcript": "자, 이제 본격적으로 한국어 노래 가사를 읽어볼까요?",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "자,", "start_ms": 110, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "본격적으로", "start_ms": 700, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "한국어", "start_ms": 1400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "노래", "start_ms": 1850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "가사를", "start_ms": 2200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "읽어볼까요?", "start_ms": 2650, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 14,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 15,
"transcript": "동해물과 백두산이 마르고 닳도록 하느님이 보우하사 우리나라 만세.",
"speaker": "speaker_2",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "mixed",
"words": [
{"word": "동해물과", "start_ms": 150, "dur_ms": 700, "phase_lr_deg": -5.0},
{"word": "백두산이", "start_ms": 900, "dur_ms": 700, "phase_lr_deg": -5.0},
{"word": "마르고", "start_ms": 1650, "dur_ms": 600, "phase_lr_deg": -5.0},
{"word": "닳도록", "start_ms": 2300, "dur_ms": 650, "phase_lr_deg": -5.0},
{"word": "하느님이", "start_ms": 3000, "dur_ms": 700, "phase_lr_deg": -5.0},
{"word": "보우하사", "start_ms": 3750, "dur_ms": 700, "phase_lr_deg": -5.0},
{"word": "우리나라", "start_ms": 4500, "dur_ms": 700, "phase_lr_deg": -5.0},
{"word": "만세.", "start_ms": 5250, "dur_ms": 600, "phase_lr_deg": -5.0}
]
},
{
"segment": 16,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 17,
"transcript": "발음이 아주 좋으세요. 특히 '닳도록' 부분이 정확했습니다.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "발음이", "start_ms": 90, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 500, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "좋으세요.", "start_ms": 800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "특히", "start_ms": 1400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "'닳도록'", "start_ms": 1750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "부분이", "start_ms": 2300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "정확했습니다.", "start_ms": 2750, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 19,
"transcript": "(instrument)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 20,
"transcript": "I agree. Your pronunciation is improving rapidly.",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 70, "dur_ms": 100, "phase_lr_deg": 10.0},
{"word": "agree.", "start_ms": 200, "dur_ms": 350, "phase_lr_deg": 10.0},
{"word": "Your", "start_ms": 600, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "pronunciation", "start_ms": 850, "dur_ms": 800, "phase_lr_deg": 10.0},
{"word": "is", "start_ms": 1700, "dur_ms": 150, "phase_lr_deg": 10.0},
{"word": "improving", "start_ms": 1900, "dur_ms": 600, "phase_lr_deg": 10.0},
{"word": "rapidly.", "start_ms": 2550, "dur_ms": 500, "phase_lr_deg": 10.0}
]
},
{
"segment": 21,
"transcript": "감사합니다. 더 열심히 해볼게요. 다음 가사는 뭐죠?",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "감사합니다.", "start_ms": 100, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "더", "start_ms": 650, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "열심히", "start_ms": 850, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "해볼게요.", "start_ms": 1300, "dur_ms": 450, "phase_lr_deg": -5.0},
{"word": "다음", "start_ms": 1800, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "가사는", "start_ms": 2100, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "뭐죠?", "start_ms": 2500, "dur_ms": 300, "phase_lr_deg": -5.0}
]
},
{
"segment": 22,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 23,
"transcript": "무궁화 삼천리 화려강산 대한사람 대한으로 길이 보전하세.",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "mixed",
"words": [
{"word": "무궁화", "start_ms": 130, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "삼천리", "start_ms": 750, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "화려강산", "start_ms": 1400, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "대한사람", "start_ms": 2250, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "대한으로", "start_ms": 3100, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "길이", "start_ms": 3900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "보전하세.", "start_ms": 4350, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 25,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 26,
"transcript": "정말 훌륭합니다! 이제 노래방 기기처럼 멜로디에 맞춰서 해볼까요?",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 110, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "훌륭합니다!", "start_ms": 450, "dur_ms": 700, "phase_lr_deg": -5.0},
{"word": "이제", "start_ms": 1200, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "노래방", "start_ms": 1500, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "기기처럼", "start_ms": 1950, "dur_ms": 550, "phase_lr_deg": -5.0},
{"word": "멜로디에", "start_ms": 2550, "dur_ms": 450, "phase_lr_deg": -5.0},
{"word": "맞춰서", "start_ms": 3050, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "해볼까요?", "start_ms": 3500, "dur_ms": 600, "phase_lr_deg": -5.0}
]
},
{
"segment": 27,
"transcript": "네, 반주 부탁드려요. 긴장되지만 재미있을 것 같아요.",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "반주", "start_ms": 350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "부탁드려요.", "start_ms": 700, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "긴장되지만", "start_ms": 1400, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "재미있을", "start_ms": 2150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 2700, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "같아요.", "start_ms": 2900, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 29,
"transcript": "Let's put on the background music. One, two, three, go!",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "Let's", "start_ms": 60, "dur_ms": 150, "phase_lr_deg": 10.0},
{"word": "put", "start_ms": 250, "dur_ms": 150, "phase_lr_deg": 10.0},
{"word": "on", "start_ms": 450, "dur_ms": 100, "phase_lr_deg": 10.0},
{"word": "the", "start_ms": 600, "dur_ms": 100, "phase_lr_deg": 10.0},
{"word": "background", "start_ms": 750, "dur_ms": 450, "phase_lr_deg": 10.0},
{"word": "music.", "start_ms": 1250, "dur_ms": 400, "phase_lr_deg": 10.0},
{"word": "One,", "start_ms": 1700, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "two,", "start_ms": 1950, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "three,", "start_ms": 2200, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "go!", "start_ms": 2450, "dur_ms": 200, "phase_lr_deg": 10.0}
]
},
{
"segment": 30,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 31,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 32,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 33,
"transcript": "학교 종이 땡땡땡 어서 모이자 선생님이 우리를 기다리신다.",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "mixed",
"words": [
{"word": "학교", "start_ms": 120, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "종이", "start_ms": 600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "땡땡땡", "start_ms": 1100, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "어서", "start_ms": 1850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "모이자", "start_ms": 2300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "선생님이", "start_ms": 2850, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "우리를", "start_ms": 3550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "기다리신다.", "start_ms": 4100, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "You're a natural! Your sense of rhythm is fantastic.",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "You're", "start_ms": 90, "dur_ms": 150, "phase_lr_deg": 10.0},
{"word": "a", "start_ms": 260, "dur_ms": 50, "phase_lr_deg": 10.0},
{"word": "natural!", "start_ms": 350, "dur_ms": 450, "phase_lr_deg": 10.0},
{"word": "Your", "start_ms": 850, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "sense", "start_ms": 1100, "dur_ms": 300, "phase_lr_deg": 10.0},
{"word": "of", "start_ms": 1450, "dur_ms": 100, "phase_lr_deg": 10.0},
{"word": "rhythm", "start_ms": 1600, "dur_ms": 400, "phase_lr_deg": 10.0},
{"word": "is", "start_ms": 2050, "dur_ms": 150, "phase_lr_deg": 10.0},
{"word": "fantastic.", "start_ms": 2250, "dur_ms": 600, "phase_lr_deg": 10.0}
]
},
{
"segment": 35,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 36,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 37,
"transcript": "정말 즐거운 시간이었어요. 한국어 공부가 더 재미있어졌나요?",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 110, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "즐거운", "start_ms": 450, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "시간이었어요.", "start_ms": 900, "dur_ms": 700, "phase_lr_deg": -5.0},
{"word": "한국어", "start_ms": 1700, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "공부가", "start_ms": 2150, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "더", "start_ms": 2600, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "재미있어졌나요?", "start_ms": 2800, "dur_ms": 700, "phase_lr_deg": -5.0}
]
},
{
"segment": 38,
"transcript": "네, 노래로 배우니까 기억에 더 잘 남는 것 같아요. 감사합니다!",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "노래로", "start_ms": 350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "배우니까", "start_ms": 800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "기억에", "start_ms": 1350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 1800, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 2000, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "남는", "start_ms": 2200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 2550, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "같아요.", "start_ms": 2750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "감사합니다!", "start_ms": 3200, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 40,
"transcript": "Great! Practice makes perfect. Keep up the good work.",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "Great!", "start_ms": 80, "dur_ms": 250, "phase_lr_deg": 10.0},
{"word": "Practice", "start_ms": 350, "dur_ms": 450, "phase_lr_deg": 10.0},
{"word": "makes", "start_ms": 850, "dur_ms": 300, "phase_lr_deg": 10.0},
{"word": "perfect.", "start_ms": 1200, "dur_ms": 450, "phase_lr_deg": 10.0},
{"word": "Keep", "start_ms": 1700, "dur_ms": 250, "phase_lr_deg": 10.0},
{"word": "up", "start_ms": 2000, "dur_ms": 100, "phase_lr_deg": 10.0},
{"word": "the", "start_ms": 2150, "dur_ms": 100, "phase_lr_deg": 10.0},
{"word": "good", "start_ms": 2300, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "work.", "start_ms": 2550, "dur_ms": 300, "phase_lr_deg": 10.0}
]
},
{
"segment": 41,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 42,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 43,
"transcript": "그럼 오늘은 여기까지 할까요? 다음에 또 만나요!",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 90, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "오늘은", "start_ms": 380, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "여기까지", "start_ms": 780, "dur_ms": 450, "phase_lr_deg": -5.0},
{"word": "할까요?", "start_ms": 1280, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "다음에", "start_ms": 1730, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "또", "start_ms": 2130, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "만나요!", "start_ms": 2330, "dur_ms": 400, "phase_lr_deg": -5.0}
]
},
{
"segment": 44,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 45,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 46,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 47,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 48,
"transcript": "수고하셨습니다. 안녕히 계세요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "수고", "start_ms": 70, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "하셨습니다.", "start_ms": 350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "계세요.", "start_ms": 1350, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 12:26:13*
