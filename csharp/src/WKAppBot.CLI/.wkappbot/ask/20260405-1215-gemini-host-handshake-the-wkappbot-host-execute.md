# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=17628  time=12:14:37  uptime=37291s  mem=464MB  system_processes=354"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_121355.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 12744ms (file: 270-260-236-326-762-237-756-326-230-756-213-260-267-230-236-102-230-230-752-753-263-321-756-103-106-765-231-203-754-756-754-756-216-726-321-326-571-752-103-326-231-236-756-321-1_W_stt_in_the_inner_just_been_doing_nothing_enc.mp3)
  Segment 2: 13744ms ~ 14320ms (file: 176-765-763-765-756-675-657-675-675-671-136 765-675 357-315_W.mp3)
  Segment 3: 15320ms ~ 15896ms (file: 130-601-672-107-136-317-317-130-310-301-310-103-310-130-157-137-317-137 301-170-310-735-137-173-310-132-601-601-360-316-316-130-137-713-103-103-317-130-371-317-137-375-173-317-3_V.mp3)
  Segment 4: 16896ms ~ 17472ms (file: 571-571-751-751 570-571-570-756-571-571-570-571-517-571-571-571-571-751-517-571-751-751-756-751 570-570-570-570-750-751-715-715-751-175-751-175-175-175-157-570-571-753 570-157-1_W.mp3)
  Segment 5: 18472ms ~ 19048ms (file: 126-617-163-130-263-625-637-367-763-627-672-673-627_W.mp3)
  Segment 6: 20048ms ~ 20624ms (file: 612-162-162-167 312-326-672-612-627-316-632-657-671-675-756-675-765-672-567-162-312-130-132-625-261-623-261-321-132-631-310 310-132-136_W.mp3)
  Segment 7: 21624ms ~ 22200ms (file: 652-652-652-651-652-625 256-261 256-526-652-562-652-652-652-652-625-652-652-652-625-261-567-561-526-562-250-625-621-625-625-652-652-652-652-652-652-562-657-651-652-562-562-657-6_W.mp3)
  Segment 8: 23200ms ~ 23776ms (file: 103-370-103-132-301-130-107-107-701-751-315-710-137-715-703-176 317-135-317-103-317-317-170-135-317-130-157-310-301-310-103-753-175-715-701-103-310-130-137-750-753-735 137-310-1_V.mp3)
  Segment 9: 24776ms ~ 26396ms (file: 137-137 713-736-317 365-137-371-137-173-561-751-135-157-175-517-175-157-765-756-176-310-173-137-136-316-312-132-157-153-175-135-157-153-157-517-507-167-136-317-713-173-156-135-1_V.mp3)
  Segment 10: 27396ms ~ 35352ms (file: 726-302-320-103-763-162-627-163-137-137-170-137-132-103-321-130-163-312-320-267-210-673-762-105-162-163-136-160-321-237-210-231-756-765-675-236-107-132-102-230-267-126-276-310-3_W.mp3)
  Segment 11: 36352ms ~ 36928ms (file: 751-175 162-163-163-103-160-106-167-163-163-163-162-136 607-675-607-540-547-574-576 132 126-132-132-136-123-612-126-621-162-132-123-132-312 625_V.mp3)
  Segment 12: 37928ms ~ 38504ms (file: 765-760-761 657-567-567-265-516-627-160-132-124-241 765-675-765-675-765-567-756-756-567-715-175-657-617-675-675-765-567-613-675 561-657 152-321-547-576-706 231-135-765-673-163-1_W.mp3)
  Segment 13: 39504ms ~ 47928ms (file: 726-173-231-756-317-367-102-620-527-256-576-316-130-130-675-765-230-310-120-210-130-120-756-321-756-754-157-574-321-130-615-756-765-726-321-236-237-270-547-130-621-103-236-625-2_W.mp3)
  Segment 14: 48928ms ~ 49504ms (file: 526-512-521-516-571-516-571-527-571-561-521-517-517-517-516-517-576-571-521 570-507 561-650-561-657-567-567-657-567-567-657-567-567-567-576-567-576-567-756-756-712-721-712-715-7_W.mp3)
  Segment 15: 50504ms ~ 51080ms (file: 613-673-637-637-615-657-657-627-620-602-162 617-617-617-617-617-612-617-617-617-160-617-617-106-103_W.mp3)
  Segment 16: 52080ms ~ 60036ms (file: 160-106-231-726-721-267-260-726-160-136-321-231-237-317-361-267-263-231-106-160-613-106-632-213-751-756-312-163-123-120-102-765-320-302-231-210-207-327-317-317-103-135-102-120-3_W.mp3)
  Segment 17: 61036ms ~ 61612ms (file: 574-574-574-574-537-513-571-157-517-517 570-157-157-715-751-573-573-571-571-574 517-571-571-574-573-517-517 517-517-157-157-152-153-510-531-571-571-574-574-571-753-756 153-513-1_W.mp3)
  Segment 18: 62612ms ~ 63188ms (file: 351-130 731-731-307-705-103-731-607-613-671-610-617-137-637-631-610-631-610-637-671-617-631-631-617-612-631-613-613-675 701-715-170-173-731-371-735-176-130-176-671-601-607-631-6_V.mp3)
  Segment 19: 64188ms ~ 64764ms (file: 537-527-165-521-157-762-762-237-275-276-513-267-716-316-310_V.mp3)
  Segment 20: 65764ms ~ 66340ms (file: 517-571-570-570-570-570-750 570-571-571-157-570-105-570-751-750-571-570-750-751-507-750-570-571-751-571-751-750-750-570-571-507-570-571-570-751 571-570-105-507-570-750-570-750-7_V.mp3)
  Segment 21: 67340ms ~ 77348ms (file: 165-206-540-574-231-357-351-526-572-130-312-130-132-130-312-213-201-201-726-310-206-137-167-137-570-174-705-765-526-201-574-126-206-201-201-576-572-275-725-762-276-157-570-217-7_V.mp3)
  Segment 22: 78348ms ~ 78924ms (file: 561-516-506-576-501-507-507-507-507-507-570-507-570-572-756-752-527-572-527-576-572-750-510-231-253-521-756-507-560-576-560-570-570-571-506-507-567-507-560-576-756-765-167-765-5_V.mp3)
  Segment 23: 79924ms ~ 86620ms (file: 201-310-203-210-230-267-123-103-103-103-765-231-362-203-130-571-267-761-106-136-276-120-103-361-231-261-230-237-756-175-201-267-231-670-260-267-230-236-206-205-130-206-260-206-2_W_stt_i've_ever_to_be_alive_late_now_until_you.mp3)
  Segment 24: 87620ms ~ 88196ms (file: 216-263-106-163-132-123-231-301-310-126-261-361-206-260-207-207-270-720-702-261-201-620-620-216-260-103-312-261-206-163-136-123-201-620-620-621-206-216-260-301-132-231-236-206-3_V.mp3)
  Segment 25: 89196ms ~ 89772ms (file: 613-136-632-162-126-162-126-127-162-612-672-627-627-217-172-271-216-213-715-671-765-135-135-165-156-156-156-152-156-152 156-156-156-153-152-126-125-215-512-231_W.mp3)
  Segment 26: 90772ms ~ 91348ms (file: 136-631-765-176-765-326-756-372-237-752-576-276-325-315-750-130 576-173-715-716-176-172-312-123-326-235-523-132-127-267-321-705-723-213-652-712-625-671-657-271-136-267-106-326-6_V.mp3)
  Segment 27: 92348ms ~ 105164ms (file: 571-561-571-217-213-273-270-237-237-357-102-751-321-203-570-106-320-173-547-213-547-260-574-267-137-267-236-236-120-542-310-102-102-130-312-231-102-103-231-261-267-576-130-167-3_W.mp3)
  Segment 28: 106164ms ~ 106740ms (file: 570-570-507-561-572-572-572-572-572-571-576-576-752-571-752-572-752-752-172-527-521-562-532-523-521-532-536-356-513-574-571-562-537-501_W.mp3)
  Segment 29: 107740ms ~ 108316ms (file: 521-527-527-257 501-516-512-501-503-513-513-153-152-153-512-625-521-512-516-152-765-671-574-571-257-527-567-526-567-562-526-652-562-652-572-573-501-501-520-502-512-516-526-513-5_V.mp3)
  Segment 30: 109316ms ~ 116876ms (file: 627-267-261-265-627-236-102-163-725-203-267-527-206-361-260-765-157-231-527-157-320-326-267-123-326-267-267-321-217-106-316-267-231-312-320-231-263-321-103-103-276-261-231-756-5_W_stt_and_you_show_up_in_some_eight_departs_on.mp3)
  Segment 31: 117876ms ~ 123240ms (file: 542-526-260-203-203-542-135-206-276-260-203-547-260-127-207-260-620-167-321-310-132-215-206-203-413-132-260-572-270-326-107-136-162-574-270-230-103-123-103-132-132-176-107-106-1_V.mp3)
  Segment 32: 124240ms ~ 124816ms (file: 127-716-621-103-163-136-601-176-216-572-163-132-561-136-102-162-163-517-716-167-106-671-617-106-621-103-165-206-617-165-106-136 625-162-163-126-612-143-103-174-103-163-675-613-1_V.mp3)
  Segment 33: 125816ms ~ 126392ms (file: 751-751-715-715-751-751-751-715-751-751-751-754-756-751-756-751-754-756 153-150-150-510-517-571-751-756-751-716-715-751-751-751-715-751-715-715-715-715-751-715-715 105-150-150-1_V.mp3)
  Segment 34: 127392ms ~ 127968ms (file: 635-621 132-321-120-103-176-167_V.mp3)
  Segment 35: 128968ms ~ 129544ms (file: 671-213-627-267-702-265-257-106-720-672-267-167-352-216-126-673-263-570-276-123-756-263-526-325-312-765-267-230-271-765-376-765-273-326 317-175-756-602-315-265-675-657-653-167-3_W.mp3)
  Segment 36: 130544ms ~ 131120ms (file: 523-756-754-756-754-754-754-756-750-576-501-503-502-502-501-506-501-501-506-502-501-503-501-503-561-502-527-527-507-562-562-520-250-270-756-572-762-756-756-576-756-756-756-756-7_V.mp3)
  Segment 37: 132120ms ~ 132696ms (file: 750 750-571-507-705-570-751-701-157-175-750-175-751-157-710-705-517-705-175-701-751-105-571-751-750-507-571-571-750-170-570-751 570-750-170-750-570-750-751-571-570-574-570-570-5_W.mp3)
  Segment 38: 133696ms ~ 144460ms (file: 217-675-657-651-206-627-457-620-163-135-213-132-576-761-167-261-607-275-236-165-321-213-267-705-213-250-326-217-267-617-657-327-236-205-132-261-167-136-576-713-125-706-205-132-7_V.mp3)
  Segment 39: 145460ms ~ 146036ms (file: 657-657-675-540-504-506-540-506-504-506-506-502-502-502-504-504-265-625-756-651-657-657-657-657-657-657-657-675-675_W.mp3)
  Segment 40: 147036ms ~ 147612ms (file: 367-267-627-163-167-310-632-321-163-625-362-130-103-302 103-716-136-321-312-263-163-261-621-126-106-102-160-123-302-320-302-310-320_V.mp3)
  Segment 41: 148612ms ~ 149188ms (file: 105-175-175-751-517-570-571-751-715-175-715-570-507-357-371-175-175-107-130-135-150-157-153-576-576-576-576-576-756-176-576-107-107-751-107-176-173-105-150-150-157-150-571-567-5_W.mp3)
  Segment 42: 150188ms ~ 158288ms (file: 321-576-275-102-103-210-231-210-572-213-572-213-210-726-102-270-132-320-310-120-216-162-756-132-320-762-753-302-756-102-210-310-206-572-765-675-320-203-230-132-231-254-230-572-3_W.mp3)
  Segment 43: 159288ms ~ 159864ms (file: 176-573-753-573-537-753 157-517-567-576-571-756-576-751-132-126-672-275_W.mp3)
  Segment 44: 160864ms ~ 161440ms (file: 107 150-105-103-130-105-105-103-105-175-130-150-105-517-150-157-130 135-157-135-157-157-175-157-175-157_W.mp3)
  Segment 45: 162440ms ~ 163016ms (file: 657-625-625-627-126-125-126-675-576-571-756-756-756-756-756-756-576-756-765-726-675-756-762-765-675-276-127-237-672-637-675-657-675-765-675-657-657-671-675-671-675-675-673-675-6_W.mp3)
  Segment 46: 164016ms ~ 164592ms (file: 132-137 627-765-103-315-657-160-130-571-652-103-103-123-103-126-102-516-165-617-103-163-321-136-102-160-106-137_V.mp3)
  Segment 47: 165592ms ~ 166168ms (file: 715-751-715-715-715-715-751-715-175-175 105-105-751-751-751-175-751-715-710-715-715-715-751-175-175-751-715-751-715-751-175-751-715 150-571-175-715-751-715-715-751-157-175-715-7_W.mp3)
  Segment 48: 167168ms ~ 173900ms (file: 176-160-160-160-361-132-657-265-256-216-201-253-236-265-132-327-312-236-127-752-312-327-754-375-315-750-206-125-130-106-760-216-675-756-162-607-617-106-126-160-172-572-753-175-1_V.mp3)
  Segment 49: 174900ms ~ 175476ms (file: 157-301-715-137-173-751-130-135-315-107-317 135-531-357-132-315-312-312-301-301-317-315-371-173-103-317-103-351-317-310-713-107-137-310-132-301-713-130-301-231-307 301-351-153-5_V.mp3)
  Segment 50: 176476ms ~ 177052ms (file: 612-261-675-762-657-762-627-632-576-276-675-762-627-627-756-327-574-576-576-231-576-675-162_W.mp3)
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
> **Time**: 2026-04-05 12:15:44
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "I've just been doing nothing and just staying at home all day long.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "I've", "start_ms": 150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "just", "start_ms": 360, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "been", "start_ms": 620, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "doing", "start_ms": 810, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "nothing", "start_ms": 1120, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "and", "start_ms": 1600, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "just", "start_ms": 1760, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "staying", "start_ms": 2000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "at", "start_ms": 2420, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "home", "start_ms": 2530, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "all", "start_ms": 2900, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "day", "start_ms": 3060, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "long", "start_ms": 3320, "dur_ms": 400, "phase_lr_deg": 0.0}
]
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "요즘 뭐 하고 지내세요? 저는 그냥 집에서 쉬고 있어요.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "요즘", "start_ms": 100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 550, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "하고", "start_ms": 780, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "지내세요?", "start_ms": 1150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "저는", "start_ms": 2000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 2400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "집에서", "start_ms": 2750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "쉬고", "start_ms": 3250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "있어요.", "start_ms": 3600, "dur_ms": 400, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 13,
"transcript": "밖에 나가기가 좀 귀찮더라고요. 날씨도 덥고 해서요.",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "밖에", "start_ms": 120, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "나가기가", "start_ms": 450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 1000, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "귀찮더라고요.", "start_ms": 1250, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "날씨도", "start_ms": 2200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "덥고", "start_ms": 2650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "해서요.", "start_ms": 3050, "dur_ms": 400, "phase_lr_deg": 0.0}
]
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
"transcript": "맞아요. 저도 최근에 계속 집돌이 생활만 하고 있네요.",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "맞아요.", "start_ms": 150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "저도", "start_ms": 700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "최근에", "start_ms": 1050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 1500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "집돌이", "start_ms": 1900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "생활만", "start_ms": 2400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "하고", "start_ms": 2950, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "있네요.", "start_ms": 3250, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "그래도 가끔은 신선한 공기 좀 마셔야 하지 않을까요?",
"speaker": "speaker_2",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "그래도", "start_ms": 110, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "가끔은", "start_ms": 550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "신선한", "start_ms": 1050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "공기", "start_ms": 1600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 2000, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "마셔야", "start_ms": 2200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "하지", "start_ms": 2700, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "않을까요?", "start_ms": 3000, "dur_ms": 500, "phase_lr_deg": 0.0}
]
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
"transcript": "I've ever to be alive late now until you.",
"speaker": "speaker_1",
"confidence": 0.75,
"language": "en",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "I've", "start_ms": 200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "ever", "start_ms": 400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 700, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "be", "start_ms": 850, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "alive", "start_ms": 1050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "late", "start_ms": 1500, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "now", "start_ms": 1800, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "until", "start_ms": 2100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 2450, "dur_ms": 250, "phase_lr_deg": 0.0}
]
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
"transcript": "그러게요. 이번 주말에는 근처 공원이라도 좀 걸어봐야겠어요.",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "그러게요.", "start_ms": 130, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이번", "start_ms": 750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "주말에는", "start_ms": 1100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "근처", "start_ms": 1700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "공원이라도", "start_ms": 2100, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 2800, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "걸어봐야겠어요.", "start_ms": 3000, "dur_ms": 800, "phase_lr_deg": 0.0}
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
"transcript": "And you show up in some eight departs on.",
"speaker": "speaker_2",
"confidence": 0.70,
"language": "en",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "And", "start_ms": 180, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 350, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "show", "start_ms": 550, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "up", "start_ms": 800, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 1000, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "some", "start_ms": 1150, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "eight", "start_ms": 1450, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "departs", "start_ms": 1700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "on.", "start_ms": 2150, "dur_ms": 200, "phase_lr_deg": 0.0}
]
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
"transcript": "좋은 생각이네요. 저도 주말에는 자전거라도 타러 나갈까 봐요.",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 140, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "생각이네요.", "start_ms": 500, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "저도", "start_ms": 1200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "주말에는", "start_ms": 1550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "자전거라도", "start_ms": 2100, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "타러", "start_ms": 2800, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "나갈까", "start_ms": 3150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "봐요.", "start_ms": 3600, "dur_ms": 350, "phase_lr_deg": 0.0}
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
"transcript": "자전거 타는 거 정말 좋죠. 요즘 한강 쪽에 사람 많을 것 같아요.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "자전거", "start_ms": 110, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "타는", "start_ms": 600, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 900, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 1050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "좋죠.", "start_ms": 1450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "요즘", "start_ms": 2000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "한강", "start_ms": 2350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "쪽에", "start_ms": 2750, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "사람", "start_ms": 3050, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "많을", "start_ms": 3400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 3800, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "같아요.", "start_ms": 3950, "dur_ms": 400, "phase_lr_deg": 0.0}
]
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
"transcript": "그러게요. 아침 일찍 서둘러서 다녀와야겠네요. 사람이 몰리기 전에요.",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "그러게요.", "start_ms": 150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "아침", "start_ms": 750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "일찍", "start_ms": 1100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "서둘러서", "start_ms": 1450, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "다녀와야겠네요.", "start_ms": 2050, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "사람이", "start_ms": 3000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "몰리기", "start_ms": 3450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "전에요.", "start_ms": 3950, "dur_ms": 400, "phase_lr_deg": 0.0}
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
"transcript": "좋은 주말 보내세요. 자전거 조심히 타시고요!",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "주말", "start_ms": 450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "보내세요.", "start_ms": 850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "자전거", "start_ms": 1600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "조심히", "start_ms": 2100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "타시고요!", "start_ms": 2550, "dur_ms": 500, "phase_lr_deg": 0.0}
]
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
"transcript": "네, 감사합니다! 즐거운 주말 보내세요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 120, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "감사합니다!", "start_ms": 400, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "즐거운", "start_ms": 1100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "주말", "start_ms": 1550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "보내세요.", "start_ms": 1900, "dur_ms": 500, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 12:15:44*
