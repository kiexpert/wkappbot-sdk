# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=09:30:17  uptime=29462s  mem=1914MB  system_processes=444"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_093007.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 613-267-723-103-130-132-230-320-253-352-263-230-102-103-107-107-236-672-326-231-574-570-203-275-205-231-132-120-132-123-213-210-302-231-123-316-632-120-126-136-163-102-130-132-6_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 237-276-126-127-201-136-132-136-132-132-501-507-312-132-123-206-236-230-236-657-302-360-310-123-132-132-103-672-321-326-132-136-123-675-265-236-173-123-312-106-103-127-102-102-1_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 321-236-263-276-276-321-573-756-756-716-531-137-175-261-261-526-576-175-136-567-657-657-657-675-756-613-627-612-216-132-321-657-657-675-263-263-316-321-132-321-321-132-267-267-2_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 165-103-136-162-362-612-562-162-362-162 162-163-310-163-136-103-651-312-136-612-215-263-621-601-361-103-103-631-103-167-123-102 564-172-167-136-103-623-106-126-103-173-617-106-1_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 375-167-316-163-103-130-206-125-106-627-672-267-623-625-526-632-132-624 102-132-160-156-135-103-601-163-160-136-173-107-176-106-312-712-620-317-567-136-316-152-215-635-162-321-1_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 301-310-301-103-136-137-106-126-136-316-307-301-137-136-160-106-136-312-231-102-106-123-103-310-103-163-613-136-103-203-307-106-163-317-301-302-312-630-267-263-320-301-103-106-2_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 765-763-673-307-136-136-136-621-632-610-601-163-136-163-621-167-167-107-705-705-710-750-576-576-705-675-607-671-617-756-675-106-136-162-163-162-612-126-602-130-302-312-136-162-1_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 607-671-162-163-167-163-167-617-167-167-162-267-261-167-154-507-132 321-136-236-132-302-321-316-312-237_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 253-256-276-627-672-627-263-567-652-625-162-213-675-765-576-572-657-567-165-361-376-326-236 675-765-765-675-756-675-657-657-657-756-675-657-756-675-675-675-765-675-657-625-672-6_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 163 260-236-752-316-316-316-301-106-312-130-160-213-156-103-136-106-260-201-267-231-162-126-123-126-260-620-260-206-261-216-162-127-320-230-231-237-236-312-316-103-617-167-163-3_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 561-761-165-175-132-123-143-261-267-602-602-160-301-163-132-126-326-302-312-362-621-167-316-312-163-256-132-123-132-217-263-163 675-673-672-263-657-341_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 637-621 653-236-236-231-321-231-632-362-762-763-763-763-763-763-123-103-130-106-617-601-601-176-163-760-176-671-671-637-617-601-571-571-756-756-760-637-761-126-672-763-765-376-3_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 621-526-563-536-536-356-372-312-325-136-657-615-162-361-316-157-157-516-651-657-631-132-163-157-576-173-132-362-312-325-372-312-372-367-635-325-352-325-315-361-316-163-156-356-5_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 321 750-135-672-750-627-657-231-137-671-627-261-261-261-567-576-576-105-130-136-716-136-136-123-136-136-103-162-260-316-137-103-136-132-316-102-321-136-125-632-163-106-136-612-1_V.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 236-613-130-316-130 713-713-761-176-516-157-756-765-765-765 167-312-126-132-216-627-623 675-627-627-610-612-316-267-576-657-571-570-165-156-617-372-621-162-136-361-312-163-316-1_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 123-312-132-213-673-132-167-631 125-675 265-125-167-620-263-627-612-615-675-162-132-561-152-765-625-576-267-236-216-126_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 701-706-716-137-507-572-672-602-236-126-126-162-167-230-320-210-203-213-312-130-213-312-103-675-361-316-216-302-312-362-317-316-132-623-263-162-126-213-162-126-302-203-326-302-2_V.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 657-165-617-672-627-256-567-756-617-162-167-103-106-103-103-103-106-103-106-106-106-103-137-102-312-137-103-103-136-130-130-573-756-107-103-130-107-107-750-176-672-627-627-106-1_V.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 106-107-103-163-103-102-210-172-501-203-607-756-260-206-321-312-301-756-276-270-276-126-126-136-126-261-103-316-621-620-627-516-613-123-201-172-103-316-201-612-216-216-261-136-1_V.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 756-267-136-123-172-267-326-623-371-362-213-361-276-627-263-675-261-263-326-736-236-321-672-263-261-267-267-217-265-326-657-576-326-176-126-256-231-132-671-127-632-256-263-236-2_W.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 316-312-153-162-623-321-256-521-506-516-560-537-567-150-136-610-657-156-576-652 607-167-602-574-576-657-637-162-621-261-163-601-765-671-657-675-675-576-576-576-765-325-103-312-1_W.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 132-316-106-130-127-670-765-625-672-261-126-261-526-571-571-571-615-157-163-613-106-123-162-167-610-627-265-657-617-613-167-163 765-675-627-267-672-627-602-106-610-326-263-216 6_W.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 160-126-106-130-103-136-126-162-206-123-126-126-231-123-301-162-312-123-231-102-231-203-163-163-163-276-602-635-516-526-236-321-301-316-160-162-102-301-301-103-105-130-132-317-3_V.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 627-162-163-136-103-621-263-106-103-137-163-130-516-651-261-236-132-123-162-163-163-162-103-316-130-136-671-165-163-167-631-657-167-163-103-263-267-361-316 236-236-236-326-312-2_W.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 310-671-760-762-671-672-627-672-672-627-617-627-631-627-672-631-627-637-673-672-627-627-672-672-672-673-627-672-627-672-627-672-672-672-672-276-672-672-627-627-637-672-627-632-6_W.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 236-571-756-321-273-302-301-132-102-617-213-132_V.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 263-620-620-621-316 136-126-136-132-123-301-632-760-760-103-103-130-132-103-103_V.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 231-302-312-301-301-301-316-312-316-615-765-657-572-567-176-617-612 162-162-612-612-617-671-612-617-106-713-260-132-230-103-210-216-123-126-261-153-263-362-263-260-162-102-127-1_W.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 362-260 130 765-675-567 365-362-132-123 627-260-625-762-231-123-213-130-163-623-263_V.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 136-126-136-136-103-106-617-137-162-163-706-136-103-136-167-137-127-172-316-713-103-167-361-167-653-536-312-130-317-176-316-162-361-673-103-316-132-213-653-163-106-165-165-167-1_V.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 602-615-175-620-261-267-702-756-157-751-715-165-610-163-143-132-570-516-617-105-103-132-263-612-763-607-167-103-154-165-275-507-135-132-130-301-103-132-561-567-361-134-165-631-3_V.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 106-231-326-137-132-261-163-312-132-162-132-561-126-231-627-213-312-672 162-617-263-261 567-756-765-756-756-571-753-751-176-715-716-715-713-102-176-671-103-567-132-612-167-126-6_W.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 103-613-136 516-263-627-613-162-132-132-136-136-167-567-675-765-765-576-756-765-126-623-236-162-675-536-562-536-265-167-261-576-756-567-562-572-576-657-653-362-567-637-576-576-7_W.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 103-132-107-137-103-136-132-312-236-162-276-271-275-726-165-361-132-134-173-143-132-203-302-203-236-623-321-574-263-206-206-236-206-236-206-260-263-130-310-150-207-320-352-235-3_V.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 762-762-762-672-762-765-765-765-762-672-675-765-675-765-765-675-675-762-762-672-167-167-761-126-612-312-672-675-752-765-756-675-576-576-567-576-675-576-567-756-756-176-765-123-1_W.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 612-126-602-627-620-230-621-621-132-107-710-173-627-623-621-162-610-106-126-560-103-163-103-153-107-167-132-243-231-236-261-621-621-621-627-371-130-143-541-317-316-317-317-316-1_V.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 627-602-206-627-261-160-621-261 675 617-627-765-632-621 632-136-623_W.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 102-172-106-160-106-167-162-610-620-362-760-610-630-706-362-127-156-152-126-672-672-103-136-312-136-162-326-263-163-103-103-560-612-576-671-630-361-130-103-123-130-103-102-103-1_V.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 123-712-261-103-102-127-102-130-132-120-175-172-103-132-103-162-672-162-621-612-316-136-160-136-312-317-137-137-136-672-613-167-136-316-103 167-675-657-612-673-671-675-673_V.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 130-103-107-102-136-312-132-103-261-206-102-106-301-210-312-206-623-301-132-103-316-213-236-362-310-103-173-106-132-302-301-312-316-624-123-301-132-261-362-326-312-326-321-312-2_W.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 102-150-516-106-103-261-231-162-601-602-576-756-675-672-627-263-102-102-126-106-102-267-362-316-162-126-362-136-107-162-362-162-206-236-231-671-623-123-132-103-301-230-102-106-2_W.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 627-602-607-310-163-162-106-103-106-103-163-167-176-103-167-130-701-706-107-301-130-106-310-130-130-103-130-103-123-213-102-130-136-103-103-670-672-761-672-102-316-756-103-103-1_V.mp3)
  Segment 43: 66192ms ~ 66768ms (file: 756-561-632-362-326-123-751-130-167-103-126-130-123-132-130-163-103-137-162-713-361-316-310-312-652-651-162-312-362-632-132-312-312-162-103-136-132-130-123-621-263-106-103-230-3_V.mp3)
  Segment 44: 67768ms ~ 68344ms (file: 312-236-216-206-136-260-260-601-160-126-230-203-230-765-762-756-756-756-576-567-576-567-576-574-325-263-263-265-362-362-263-620-320-126-103-301-132-627-263-132-120-312-672-672-6_W.mp3)
  Segment 45: 69344ms ~ 69920ms (file: 231-321-302-231-603-163-132-632-103-130-107-107-507-705-615-362-203-320-320-352-267-236-231-130-735-726-765-670-670-320-263-263-263-106-103-213-103-570-675-657-602-562-501-235-2_V.mp3)
  Segment 46: 70920ms ~ 71496ms (file: 103-231-312-321-230-263-320-123-231-375-526-103-106-316-625-672-672-672-675-165-126-206-621-620-672-672-267-203-263-230-657-367-675-106-130-310-156-150-106-106-316-163-620-603-7_W.mp3)
  Segment 47: 72496ms ~ 73072ms (file: 301-312-576-576-103-103-136-620-260-620-623-230-263-260-672-136-106-163-126-236-263-326-321-132-602-260-316-360-236-320-632-203-206-236-203-231-203-230-102-216-126-132-163-310-1_V.mp3)
  Segment 48: 74072ms ~ 74648ms (file: 621-620-301-312-320-103-167-576-216-321-231-237-103-320-213-201-213-312-261-231-123-127-103-361-231-321-361-312-316-564-153-136-132-103-136-312-321-301-130-103-163-103-130-617-6_W.mp3)
  Segment 49: 75648ms ~ 76224ms (file: 517-156-132-162-123-710-317-321-320-237-237-236-627-301-231-361-321-217-326-263-260-263-267-260-236-263-261-756 316-612-376-231-302-312-321-312-321-327-327-312-132-316-621-301-3_V.mp3)
  Segment 50: 77224ms ~ 77800ms (file: 162-213-267-162-106-765-765-761-125 163-327-301-302-310-301-316-326-763-762-672-762-672-765-756-765-765-576-753-576-237-203-320-132-102-123-126-312-653-657-675-657-657-675-615-6_W.mp3)
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
> **Time**: 2026-04-03 09:31:04
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "깨끗하다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "깨끗하다", "start_ms": 40, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "더럽다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "더럽다", "start_ms": 50, "dur_ms": 460, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "조용하다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "조용하다", "start_ms": 30, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "시끄럽다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "시끄럽다", "start_ms": 60, "dur_ms": 420, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "밝다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "밝다", "start_ms": 45, "dur_ms": 485, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "어둡다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "어둡다", "start_ms": 60, "dur_ms": 455, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "강하다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "강하다", "start_ms": 40, "dur_ms": 495, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "약하다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "약하다", "start_ms": 65, "dur_ms": 475, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "무겁다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "무겁다", "start_ms": 40, "dur_ms": 490, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "가볍다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "가볍다", "start_ms": 70, "dur_ms": 470, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "두껍다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "두껍다", "start_ms": 50, "dur_ms": 415, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "얇다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "얇다", "start_ms": 80, "dur_ms": 435, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "깊다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "깊다", "start_ms": 30, "dur_ms": 445, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "얕다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "얕다", "start_ms": 65, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "단단하다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "단단하다", "start_ms": 40, "dur_ms": 505, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "부드럽다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "부드럽다", "start_ms": 55, "dur_ms": 515, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "날카롭다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "날카롭다", "start_ms": 35, "dur_ms": 525, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "뭉툭하다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "뭉툭하다", "start_ms": 65, "dur_ms": 425, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "길다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "길다", "start_ms": 45, "dur_ms": 410, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "짧다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 75,
"audio_type": "speech",
"words": [
{"word": "짧다", "start_ms": 75, "dur_ms": 410, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "넓다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "넓다", "start_ms": 50, "dur_ms": 425, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "좁다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "좁다", "start_ms": 80, "dur_ms": 405, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "높다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "높다", "start_ms": 30, "dur_ms": 435, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "낮다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "낮다", "start_ms": 65, "dur_ms": 485, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "동그랗다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "동그랗다", "start_ms": 40, "dur_ms": 515, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "네모나다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "네모나다", "start_ms": 55, "dur_ms": 505, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "세모나다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "세모나다", "start_ms": 35, "dur_ms": 495, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "빨갛다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "빨갛다", "start_ms": 70, "dur_ms": 505, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "파랗다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "파랗다", "start_ms": 40, "dur_ms": 515, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "노랗다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 75,
"audio_type": "speech",
"words": [
{"word": "노랗다", "start_ms": 75, "dur_ms": 475, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "초록색",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "초록색", "start_ms": 50, "dur_ms": 485, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "검정색",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "검정색", "start_ms": 80, "dur_ms": 495, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "하얀색",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "하얀색", "start_ms": 30, "dur_ms": 385, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "하나",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "하나", "start_ms": 65, "dur_ms": 325, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "둘",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "둘", "start_ms": 40, "dur_ms": 335, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "셋",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "셋", "start_ms": 55, "dur_ms": 345, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "넷",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "넷", "start_ms": 35, "dur_ms": 415, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "다섯",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "다섯", "start_ms": 65, "dur_ms": 425, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "여섯",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "여섯", "start_ms": 40, "dur_ms": 435, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "일곱",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "일곱", "start_ms": 70, "dur_ms": 445, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "여덟",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "여덟", "start_ms": 55, "dur_ms": 425, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "아홉",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "아홉", "start_ms": 80, "dur_ms": 315, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "열",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "열", "start_ms": 30, "dur_ms": 285, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "일",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "일", "start_ms": 60, "dur_ms": 295, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "이",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 45, "dur_ms": 315, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "삼",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "삼", "start_ms": 55, "dur_ms": 325, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "사",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "사", "start_ms": 35, "dur_ms": 305, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "오",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "오", "start_ms": 70, "dur_ms": 345, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "육",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "육", "start_ms": 40, "dur_ms": 335, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "칠",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 75,
"audio_type": "speech",
"words": [
{"word": "칠", "start_ms": 75, "dur_ms": 300, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 09:31:04*
