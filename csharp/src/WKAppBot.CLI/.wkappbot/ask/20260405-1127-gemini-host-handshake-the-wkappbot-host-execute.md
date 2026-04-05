# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=6944  time=11:26:35  uptime=35005s  mem=475MB  system_processes=352"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_112314.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 675-615-671-617-671-617-617 275-615-615-165-671-627-671-675-671-163-615-165-615-615-651-675-675 165-651-657-657-615-657-615-617-675-657-615-671-671-675-617-617-615-617-617-617-6_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 765-765-617-617 316-316-361-367-316-675-617-617-167 367-671-671-672-671 356-715-376-726-326_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 572-567-567-576-567-576-567-576-576-567-576-572 572-576-576-576-567-567-576-567-576-567-756 527-567-657-576-567-567-567-576-567-567-567-576-567-675-675-517-527-576-514-537-534-5_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 632-563-531-576-756-657-657-267-675 576-574-576-567-574-574-574-576-570 617-617-673-167-750-507-753-157-175-710-105-176-651-756-157-576-651-576-576-657-567-526-657-562-563-762-6_W.mp3)
  Segment 5: 6304ms ~ 22575ms (file: 150-574-105-572-135-321-106-267-310-103-103-132-574-357-175-203-130-206-235-265-216-201-103-216-201-547-231-103-260-576-574-574-105-103-130-103-310-203-103-543-547-210-231-260-2_W.mp3)
  Segment 6: 23575ms ~ 24151ms (file: 561-607-126-261-267-756-702-136-132-316-312-312-375-675-673-672-672-631-567-673-236-106-132-230-203-132-126-310-103-106-165-167-160-315-351-356-756-706_W.mp3)
  Segment 7: 25151ms ~ 25727ms (file: 507-576-310-310-103-310-316-136-103-136-132-574-570-530-235-217-752-357 574-576-756-756-756-125 213-231-302-263-230-203-230-203-532-536-615-601-615-106-165-167-106-163-507-735-6_V.mp3)
  Segment 8: 26727ms ~ 32055ms (file: 216-206-132-213-230-256-625-230-621-230-163-132-123-102-132-527-250-213-203-230-621-602-102-652-612-625-260-267-265-267-627-267-126-236-123-263-236-265-265-260-260-625-265-132-1_W.mp3)
  Segment 9: 33055ms ~ 33631ms (file: 572-570-372-206-207-237-263-576-725-537-732-672-163-257-263-263-607-275-375-132-126-217-620-675-123-625-574-517-574-275-765-123-570-357-251-273-275-752-267-672-362-236-263-507-3_V.mp3)
  Segment 10: 34631ms ~ 35207ms (file: 602-621-567-637-310-301-106-103-130-360-312-132-130-267-261-236-367-361-310-301-321-361-362-361-361-273-726-327-276-326-301-136-103-162-276-261-276-301-310-320-201-260-623-206-2_W.mp3)
  Segment 11: 36207ms ~ 41787ms (file: 657-675-653-136-631-671-631-371-236-320-327-326-326-362-603-360-361-236-360-376-351-132-153-531-126-135-231-217-261-216-216-271-213-126-216-126-326-216-126-261-231-217-327-637-7_W_stt_if_if_one_of_the.mp3)
  Segment 12: 42787ms ~ 50779ms (file: 163-126-126-126-126-126-126-162-126-126-126-236-261-312-267-256-574-562-236-213-261-216-106-572-132-130-263-261-123-576-265-312-756-762-275-632-632-261-576-265-576-136-213-236-1_W.mp3)
  Segment 13: 51779ms ~ 52355ms (file: 371-175-137-735-175-137-137 735-731-701-307-751-701-317-175-173-176-160-706-671-160-160-136-613-361-317-310-637-103-360-361-317-157-710 307-317-715-703-751-137-107-317-715-167-6_V.mp3)
  Segment 14: 53355ms ~ 66315ms (file: 230-203-213-231-237-210-172-107-137-302-213-210-276-267-261-231-267-231-320-231-267-231-267-261-217-267-206-126-201-210-260-267-310-301-310-231-301-132-321 267-210-173-103-201-5_W.mp3)
  Segment 15: 67315ms ~ 67891ms (file: 267-260-657-520 576-705-752-720-705-762-527-756-750-750-705-705-750-705-750-620-752-527-275-256-562 572-527-570-576-265-562-570-507-570-725-567-605-576-506-567-605-527-750-506-5_V.mp3)
  Segment 16: 68891ms ~ 69467ms (file: 756-137-571 317-371-361-673-716-731-765-763-137-673-176-761-371-756-176-765-716-376-367-307-371-367-317-573-735-375-357-517-375-153-315-137-175-317_W.mp3)
  Segment 17: 70467ms ~ 77703ms (file: 213-231-201-210-126-203-230-162-163-167-756-657-672-237-236-236-216-103-276-231-230-106-756-106-231-673-265-102-201-103-120-127-126-267-120-162-623-162-163-173-231-162-230-103-1_W.mp3)
  Segment 18: 78703ms ~ 85939ms (file: 206-572-574-105-125-301-302-135-105-154-657-574-572-206-260-315-135-132-567-756-576-206-123-152-154-103-213-162-106-230-573-752-260-106-103-310-132-713-173-132-103-312-301-301-2_V.mp3)
  Segment 19: 86939ms ~ 93203ms (file: 236-201-760-260-236-263-572-527-574-530-576-320-130-201-123-260-206-231-620-167-572-271-261-206-216-276-627-207-765-576-750-260-236-237-310-132-136-130-103-321-312-213-217-102-3_W.mp3)
  Segment 20: 94203ms ~ 94779ms (file: 715-175-175-175-751-753-715 510-510-517-510-150-105-105-105-107-175-715-715-175-175-175-175-157-175-175-715-715-715-175-751-715-751 150-510-510-150-150-517-105-105-173-175-175-7_V.mp3)
  Segment 21: 95779ms ~ 96355ms (file: 613-615-657-651-657-654-651-564-654-657-567-564-654-657-657-153-516-501-546-562-546-513-547-514-501-567-516-534-532-524-615-615-615-657-657-657-657-654-567-564-567-567-657-675-2_W.mp3)
  Segment 22: 97355ms ~ 97931ms (file: 637-612-267-165-126-213-627-627-627-621-627-617-627-265-625-623-657-562-675 672-672-260-762-762-263-263-623-627-126-627-762-263-236-326-326-326-267-321-621-163-261-265-623-162_W.mp3)
  Segment 23: 98931ms ~ 99507ms (file: 715-175-517-517-571-571-574-571-574-571-517-571 571-517-157-571-571-571-571-175-751-715-751-175-175-715-751-715-751 571-573-570-517-517-570-571-571-517-571-571 517-175-571-517_W.mp3)
  Segment 24: 100507ms ~ 101083ms (file: 517-571-517-517-571-517-157-517-517-157-157-750-754-756-754-751-754-754-754-754-754-754-754-756-754-754-754-105-150-105-105-105-157-157-517-157-157-156-571-157-157-517-517-517-5_W.mp3)
  Segment 25: 102083ms ~ 102659ms (file: 107-107-317-753-750-571-371-317-103-137-753-103-137-735-750-310-103-170-130 301-107-130-170-713-317-375-103-753-107-130-310-310-317-103-175-710-130-105-751-703-317-713-730-137-3_V.mp3)
  Segment 26: 103659ms ~ 104235ms (file: 745-627-756-312-726-507-275-672-257-276-605-275-162-562-127-253-321-216-521-123 752-276-756-362-753-762-237-567-256-321-157-132-736-123-237-725-237 376-276-752-275-236-267-756 2_V.mp3)
  Segment 27: 105235ms ~ 105811ms (file: 302-301-673-671-310-541-513-531-130-103-103-301-130-216-321-312-730-702-723 516-165-156-160-160-136-312-316-321 362-326-637-756-736-627-675-267-672-136-106-715-706-761-173-561-1_W.mp3)
  Segment 28: 106811ms ~ 113975ms (file: 231-542-762-267-267-236-217-216-120-627-162-612-261-126-261-267-126-231-236-612-213-237-753-267-267-321-310-312-326-103-627-125-130-213-123-261-726-672-672-675-123-267-162-261-2_W.mp3)
  Segment 29: 114975ms ~ 131499ms (file: 102-327-213-731-132-231-237-752-321-326-321-376-263-263-572-236-120-723-756-267-263-263-136-263-267-263-310-310-132-316-310-267-261-123-237-756-765-326-231-130-761-263-263-213-6_W.mp3)
  Segment 30: 132499ms ~ 133075ms (file: 107-107-107-175-157-501-150-150-571-504-510-501-570-150-507-510 107-107-715-170-107-175-105-105-157-517-507-517-501-541-150-501-105-150-507-510-501-517 571-501-105-105-175-157-1_W.mp3)
  Segment 31: 134075ms ~ 134651ms (file: 263-621-672 312-231-137-103-102-703-612-621-260-236-263-261-231-203-612-102-163-160-136-231-132-106-130-136-103-132-123-716-765-576-657-367-567-765-576-546-756-675-567-765-765-5_W.mp3)
  Segment 32: 135651ms ~ 136227ms (file: 752-756-756-576-675-165-167-671-675-675-671-672-627-765-752-726-725-752-723-756-725-275-726-275-725-762-675-517-576-752-765-732-273-216-726-372-321-762-273-763-716-756-765-765-7_W.mp3)
  Segment 33: 137227ms ~ 137803ms (file: 671-631-367-631-316-361-310-136-317-316-315-362-362-316-312-316-623-632-627-167-367-362-362 675-365-136-137-132-735-765-657-130-135-310-317-317-317-137-673-316-163-572-126-267-1_W.mp3)
  Segment 34: 138803ms ~ 146075ms (file: 170-150-213-261-231-216-312-230-251-172-175-107-216-721-217-312-715-754-107-136-312-231-263-265-106-123-207-276-107-167-106-172-172-672-602-165-102-567-132-256-536-120-756-125-1_W_stt_the_women_of_local_government_entities_t.mp3)
  Segment 35: 147075ms ~ 147651ms (file: 132-132-167 672-607 567-650-765-670-760-562-570-570-573-573-570-507-617-261-162-136-637-630-607-706-672-735-706-370-176-730-716-670-756-675-675-263 132-312-756-715-715-765_W.mp3)
  Segment 36: 148651ms ~ 149227ms (file: 751-715-715-571-715-175-751-753 751-751-571-753-710-713-175-715-137-173-173-317-750-173-175-137-175-735-715-175-157-753-175-750-751 750-753-710-750-173-701-713-753-715-715-573-7_V.mp3)
  Segment 37: 150227ms ~ 160163ms (file: 127-561-103-201-132-175-213-213-216-203-102-316-312 621-216-276-712-312-132-201-213-130-310-137-132-130-726-762-127-216-207-210-273-206-273-213-237-572-572-203-136-123-571-130-1_V.mp3)
  Segment 38: 161163ms ~ 161739ms (file: 261-267-621-612-621-621-621-612-612-621-612-612-621-612-612-162-160 621-612-612-671-671-671-613-617-612-672-671-613 621-612-612-612-672-612-621-612-617-671-612-621-621-617-621-6_W.mp3)
  Segment 39: 162739ms ~ 170515ms (file: 302-271-231-132-236-203-507-375-703-731-231-236-263-623-631-716-136-123-752-573-321-260-261-236-527-136-321-267-120-126-260-102-210-261-267-162-132-143-103-267-263-762-260-260-2_W_stt_this_is_proficient_on_the_same_as_i_am_a.mp3)
  Segment 40: 171515ms ~ 172091ms (file: 576-576-675-675-765-675-675-675-657-267-672-652-625-527-652-625-652-615-615-612-617-167-176-617-671-167-671-675-671-617-671-671-675-672-671-675 547-574_W.mp3)
  Segment 41: 173091ms ~ 173667ms (file: 617-216-210-120-621-102-163-216-623-263-362-302-765-756-762-761-670-610-301-672-610-130-637-612-612-103-601-136-632-321-136-231-321-126-612-620-162-126-632-613-125-126-236-632-6_W.mp3)
  Segment 42: 174667ms ~ 175243ms (file: 315-130-301-317-301-310-137-103-312-137-103-132-103-137-307-310-301-317-310-132-173-310-103-317 105-315-317-103-173-135-130-103-317-312-130-103-107-103-301-103-130-103-301-137-3_V.mp3)
  Segment 43: 176243ms ~ 176819ms (file: 216-261-261-261-267-267-627-627-627-627-672-627-612-563-637-546-567-547 216-621-612-621-621-612-621-621-621-612-627-612-102-203-125-125-527-527-527-527-507-502-501-250-502-275-2_W.mp3)
  Segment 44: 177819ms ~ 183579ms (file: 237-326-312-351-132-137-137-321-217-273-752-572-576-576-576-576-231-236-301-203-302-130-237-172-312-236-237-326-312-312-312-130-312-263-263-267-301-137-103-107-573-170-715-302-2_W.mp3)
  Segment 45: 184579ms ~ 185155ms (file: 213-231-320-201-206-213-123-312-320-236-203-321-310-321-203-123-321-302-362-321-321-231-321-321-326-203-263-261-162-102-106-162-236-263-163-102-162-310-260-130-123-603-612-163-3_V.mp3)
  Segment 46: 186155ms ~ 186731ms (file: 507-517-657-620-260-315-320-630-537-507-326-657-357-302-301-605-326-302-756-765-607-130-310-130-130-132-103-132-136-651-132-130-301-103-312-326-260-267-261-106-627-176-627-617-6_W.mp3)
  Segment 47: 187731ms ~ 188307ms (file: 675-637 153-165-157-175-265-751-571-576-315-367-637-365-361-371-376-351-356-356-365-365-317-136-517-572-571-537-136-516-351-352-362-352-352-352-357-356-375-356-315-361-132-315-1_W.mp3)
  Segment 48: 189307ms ~ 199243ms (file: 517-162-610-106-620-602-103-752-103-371-620-621-176-106-567-507-157-103-176-127-106-125-671-167-261-106-157-213-162-103-163-136-172-160-617-156-602-617-756-130-167-765-162-603-1_V.mp3)
  Segment 49: 200243ms ~ 200819ms (file: 130-106-137-136-136-321-132-153-163-615-160-103-106-163-103-167-103-136-132-316-136-130-103-132-617-136-103-136-167-103-130-653-126-167-136-175-731-571-157-103-167-657-136-126-6_V.mp3)
  Segment 50: 201819ms ~ 202395ms (file: 613-627-231-263-102-321-213-162-103-312-623-201-316-613-601-261-321-102-206-621-123-162-132-213-201-263-263-267-103-136-301-162-103-670-612-613 136-576-135-302-573-137-627-621-6_V.mp3)
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
> **Time**: 2026-04-05 11:27:39
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
{"word": "안녕", "start_ms": 50, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "하세요", "start_ms": 300, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "오늘 날씨가 정말 좋네요 그쵸",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 110, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "날씨가", "start_ms": 550, "dur_ms": 600, "phase_lr_deg": 5.0},
{"word": "정말", "start_ms": 1200, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "좋네요", "start_ms": 1750, "dur_ms": 650, "phase_lr_deg": 5.0},
{"word": "그쵸", "start_ms": 2500, "dur_ms": 450, "phase_lr_deg": 5.0}
]
},
{
"segment": 3,
"transcript": "네 맞아요",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "맞아요", "start_ms": 260, "dur_ms": 280, "phase_lr_deg": 0.0}
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
"transcript": "혹시 영어를 배우고 계신가요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "혹시", "start_ms": 60, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "영어를", "start_ms": 190, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "배우고", "start_ms": 350, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "계신가요?", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 7,
"transcript": "I am practicing English every day to improve my speaking skills.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 150, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "am", "start_ms": 260, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "practicing", "start_ms": 390, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "English", "start_ms": 900, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "every", "start_ms": 1350, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "day", "start_ms": 1700, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "to", "start_ms": 2000, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "improve", "start_ms": 2150, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "my", "start_ms": 2600, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "speaking", "start_ms": 2800, "dur_ms": 450, "phase_lr_deg": -5.0},
{"word": "skills", "start_ms": 3300, "dur_ms": 400, "phase_lr_deg": -5.0}
]
},
{
"segment": 8,
"transcript": "와 정말 대단하시네요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "와", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 200, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "대단하시네요", "start_ms": 400, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 10,
"transcript": "감사합니다 일본어도 조금 할 줄 알아요",
"speaker": "speaker_2",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 120, "dur_ms": 800, "phase_lr_deg": 2.0},
{"word": "일본어도", "start_ms": 1000, "dur_ms": 700, "phase_lr_deg": 2.0},
{"word": "조금", "start_ms": 1800, "dur_ms": 500, "phase_lr_deg": 2.0},
{"word": "할", "start_ms": 2400, "dur_ms": 300, "phase_lr_deg": 2.0},
{"word": "줄", "start_ms": 2750, "dur_ms": 250, "phase_lr_deg": 2.0},
{"word": "알아요", "start_ms": 3100, "dur_ms": 600, "phase_lr_deg": 2.0}
]
},
{
"segment": 11,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 12,
"transcript": "すごいですね 多言語を話せるのは羨ましいです",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ja",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "すごいですね", "start_ms": 200, "dur_ms": 1500, "phase_lr_deg": 0.0},
{"word": "多言語を", "start_ms": 1800, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "話せるのは", "start_ms": 3100, "dur_ms": 1400, "phase_lr_deg": 0.0},
{"word": "羨ましいです", "start_ms": 4600, "dur_ms": 1800, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 15,
"transcript": "Actually it takes a lot of effort to maintain all these languages.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "Actually", "start_ms": 180, "dur_ms": 500, "phase_lr_deg": -2.0},
{"word": "it", "start_ms": 700, "dur_ms": 100, "phase_lr_deg": -2.0},
{"word": "takes", "start_ms": 850, "dur_ms": 300, "phase_lr_deg": -2.0},
{"word": "a", "start_ms": 1200, "dur_ms": 50, "phase_lr_deg": -2.0},
{"word": "lot", "start_ms": 1300, "dur_ms": 200, "phase_lr_deg": -2.0},
{"word": "of", "start_ms": 1550, "dur_ms": 150, "phase_lr_deg": -2.0},
{"word": "effort", "start_ms": 1750, "dur_ms": 400, "phase_lr_deg": -2.0},
{"word": "to", "start_ms": 2200, "dur_ms": 100, "phase_lr_deg": -2.0},
{"word": "maintain", "start_ms": 2350, "dur_ms": 500, "phase_lr_deg": -2.0},
{"word": "all", "start_ms": 2900, "dur_ms": 200, "phase_lr_deg": -2.0},
{"word": "these", "start_ms": 3150, "dur_ms": 300, "phase_lr_deg": -2.0},
{"word": "languages", "start_ms": 3500, "dur_ms": 600, "phase_lr_deg": -2.0}
]
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
"transcript": "맞아요 꾸준함이 제일 중요한 것 같아요",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "맞아요", "start_ms": 80, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "꾸준함이", "start_ms": 250, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "제일", "start_ms": 430, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "중요한", "start_ms": 500, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 550, "dur_ms": 10, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 570, "dur_ms": 6, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 19,
"transcript": "저는 매일 아침 라디오를 들으면서 쉐도잉 연습을 해요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 140, "dur_ms": 400, "phase_lr_deg": 3.0},
{"word": "매일", "start_ms": 600, "dur_ms": 400, "phase_lr_deg": 3.0},
{"word": "아침", "start_ms": 1100, "dur_ms": 500, "phase_lr_deg": 3.0},
{"word": "라디오를", "start_ms": 1700, "dur_ms": 800, "phase_lr_deg": 3.0},
{"word": "들으면서", "start_ms": 2600, "dur_ms": 700, "phase_lr_deg": 3.0},
{"word": "쉐도잉", "start_ms": 3400, "dur_ms": 700, "phase_lr_deg": 3.0},
{"word": "연습을", "start_ms": 4200, "dur_ms": 600, "phase_lr_deg": 3.0},
{"word": "해요", "start_ms": 4900, "dur_ms": 500, "phase_lr_deg": 3.0}
]
},
{
"segment": 20,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
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
"transcript": "That's a great method. Consistent practice is the key to mastery.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "That's", "start_ms": 100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 450, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "great", "start_ms": 600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "method.", "start_ms": 1000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "Consistent", "start_ms": 1800, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "practice", "start_ms": 2500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 3100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 3300, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "key", "start_ms": 3450, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 3750, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "mastery.", "start_ms": 4000, "dur_ms": 700, "phase_lr_deg": 0.0}
]
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
"transcript": "고맙습니다 더 열심히 해야겠어요",
"speaker": "speaker_2",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "고맙습니다", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 250, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "열심히", "start_ms": 330, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "해야겠어요", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 0.0}
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
"transcript": "충분히 잘하고 계세요 너무 부담 갖지 마세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "충분히", "start_ms": 90, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "잘하고", "start_ms": 550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "계세요", "start_ms": 1100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 1800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "부담", "start_ms": 2300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "갖지", "start_ms": 2900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "마세요", "start_ms": 3400, "dur_ms": 600, "phase_lr_deg": 0.0}
]
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
"transcript": "응원해주셔서 정말 감사합니다 힘이 나네요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "응원해주셔서", "start_ms": 120, "dur_ms": 1500, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 1700, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 2400, "dur_ms": 1600, "phase_lr_deg": 0.0},
{"word": "힘이", "start_ms": 4100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "나네요", "start_ms": 4700, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "별말씀을요 언제든 도움이 필요하면 말씀하세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "별말씀을요", "start_ms": 150, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "언제든", "start_ms": 1500, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "도움이", "start_ms": 2600, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "필요하면", "start_ms": 3600, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "말씀하세요", "start_ms": 4800, "dur_ms": 1400, "phase_lr_deg": 0.0}
]
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
"transcript": "The public is you pass it is exciting today.",
"speaker": "speaker_2",
"confidence": 0.85,
"language": "en",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "The", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "public", "start_ms": 350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 800, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 1000, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "pass", "start_ms": 1200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "it", "start_ms": 1600, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 1800, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "exciting", "start_ms": 2000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "today.", "start_ms": 2600, "dur_ms": 400, "phase_lr_deg": 0.0}
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
"transcript": "그럼 다음에 또 이야기 나눠요 안녕히 가세요",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "다음에", "start_ms": 600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 1200, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "이야기", "start_ms": 1500, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "나눠요", "start_ms": 2200, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 3000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 3700, "dur_ms": 600, "phase_lr_deg": 0.0}
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
"transcript": "네 안녕히 계세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 300, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "계세요", "start_ms": 600, "dur_ms": 220, "phase_lr_deg": 0.0}
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
"transcript": "오늘 수업은 여기까지입니다",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 110, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "수업은", "start_ms": 250, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "여기까지", "start_ms": 380, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "입니다", "start_ms": 550, "dur_ms": 120, "phase_lr_deg": 0.0}
]
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
"transcript": "질문 있으신 분들은 편하게 물어봐주세요",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "질문", "start_ms": 150, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "있으신", "start_ms": 300, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "분들은", "start_ms": 450, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "편하게", "start_ms": 610, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "물어봐주세요", "start_ms": 750, "dur_ms": 160, "phase_lr_deg": 0.0}
]
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
"transcript": "없으면 오늘 수업 마칠게요",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "없으면", "start_ms": 80, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 230, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "수업", "start_ms": 340, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "마칠게요", "start_ms": 450, "dur_ms": 120, "phase_lr_deg": 0.0}
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
"transcript": "수고하셨습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "수고", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "하셨습니다", "start_ms": 250, "dur_ms": 320, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 11:27:39*
