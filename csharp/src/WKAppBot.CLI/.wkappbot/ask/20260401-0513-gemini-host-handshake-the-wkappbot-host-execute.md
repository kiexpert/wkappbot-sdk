# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=14524  time=05:12:49  uptime=5359s  mem=2445MB  system_processes=775"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260401_051239.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 613-132 302-326-302-302-326-321-623-206-206-260-203-231-321-231-230-230-213-236-230-206-203-231-206-263-206 620-260-263-203_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 524-265-265-625-265-267-263-625-267-267-627-265-231-267-264-765-762-361-547-532-123-621-267-627-762-156-216-263-267-627-623-263-236-321-512-263-254-235-264-267-260-657-627-672-6_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 356-576-236-326-263-236-265-326-625-261-236-356-253-256-326-321-367-546-521-256-625-326-632-621-216-516-163-537-523-576-256-126-326-312-537-532-253-267-625-621-260-376-231-235-5_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 320-321-132-312-321-362-312-321-312-312-321-312-316-362-312-316-312-310-312-321-312-316-321-132-312-312-321-312-312-132-312-312-123-132-312-312-316-312-130-316_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 123-213-163-176-143-135-614-362-251-162-123-712-671-365-145-375-471_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 762-672 765-760-703-137-756-756-761-175-756-761-672-671-671-612-715-765-130-751-176-173-752-756-167 316 317-321_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 136-132-126-123-326-124-627 261-621-310-372-157-163-162-216-126-260-627-106-106-102-103-130-627-602-130-162-263-261-102-136-107-763-673-260-126-106-106-103-103-136-127-576-302-3_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 627-623-612-176-623-623-632-360-620-623-362-103-136-326 627-627-756-756-756-576_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 752-521-712-612-326-762-627-362-612-621-216-567-567-256-625-610-213-312-132-321-672-672-267-273-217-217-321-271-276-216-136 125-150-136-123-263-132-176-130-162-651-162-132-103-1_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 576-675-562-652-652-576-657-572-567-652-576-657-562-567-576-567-567-567-567-576-562-562-576-563-567-562-567-576-356-576-526-532-653-536-562-657-657-567-657-576-657-567-537-576-6_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 612 675-672-675-625-672-627-627-621_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 132-325-725-725-725-750-572-257-135-317-731-172-137-321-725-276-726-123-123-132-127-712-276-261-271-273-276-261-627-265-163-271-562-123-621-627-621-617-216-216-627-261-261-265-2_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 607-756-753-326-603-362-106-163-312-607-602-260-362-360-326-632-762-627-267-627-103-143-104-103-137-157-703-516-514-302-530-573-176-361-136-103-106-160-612-617-675-620-627-260-6_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 623-102-716-103-761-102-176-562-136-103-167-103-126-713-163-162-106-132-136-132-103-130-126-126-601-657-601 176-265-132-167-176-160-675-167-106-106-154-651-765-163-163-263-176-1_V.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 760-106-276-271-237-216-213-162-125-267-320-302-310-603-132-126-216-175-213-260-267-267-261-602-263-127-260-263-126-162-615-761-670-670-671-103-156-106-102-106-136-176-671-617-1_V.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 267-275-236-236-261-267-261-321-123_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 672-675-675-260-620-612-675-672-671-610-106-163-160-163-613-601-620-627-671-675-765-675-321-675-261-621-672-267-236-320-210-123-162-621-610-623-261-216-723-372-312-571-157 132-1_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 637-632-621-672-612-756-106-160-602_W.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 567-316-162 123-132-103-103-132-130-103-576-567-536-106 106-102-103-102-102-102-164-163-132-134-103-132-134-135-301-310-301-321-230-203-210-201-103-620-620-602-602-160-613-163-6_V.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 163-160-361-361-167-106-106-176-130-612-657-362-326-103-160-136-103-106-136-163-601-136-163-103-136-361-671-623-103-162-126-163-176-761-652 163-106-162-103-610-136-352-765-615-1_V.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 103-576-576-576-310-361-132-163-102-103-156 103-106-103-103-163-136-316-130-136-136-132-621-162 136-162-130-132 231-236-163-125-312-736-703-301-132-103-137 132-312-132-312-123-7_V.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 316-106-103-123-130-627-671-106-126-260-621-632-761-765-756-765-765-765-312-312-316-316-163-167-367-631-627-361-627-260 310-137-361-162-103-731-612-213-362-627-672-267-632-132-3_W.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 103-310-617-163-671-167-175-163-106-106-625-321-621-176-237-367-167-103-162-625-675-216-326-163-361-613-361-126-162-167-672-167-671-163-156-106-261-620-103-130-312-230-361-136-7_V.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 617-163-106-130-162-163-162-631-163-163-512-172-132-126-103-163-106-137-167-160-106-617-176-167-160-615-136-163-127-126-103-126-613-516-160-136-706-132-167-163-106-167-106-136-1_V.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 106-765-162-106-103-612-601-106-615-765-236-362-263-602-602-106-136-310-163-103-316-213-203-201-103-231-132-103-123-302-302-231-302-320-756-675-236-312-132-312-132-327-106-310-2_V.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 617-617-167-672-762-761-671-762-761-167-761-761-613-651-756-765-167-271-167-160-617-671-162-617-167-162-612-123-162-167-163-102-230-203-230-230-230-237-302-326-302 126-671-613-3_W.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 143-132 162-675-143-132-143-132-126-156_V.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 165-750-170-107-106-167-160-617-105-167-176-107-106-136-106-163-106-601-701-564-564-561-657-657-615-617-601-651-156-567-576-751-756-765-576-175-517-163-126-671-675_W.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 762-756-126 635-136-136-103-610-136-613-105-157-564-627-165-162-126-103 653-675-263-673-106-126-130-301-132-615-160-761-601-106-136-617-167-612-167-623-652-321-612-162-165-106-1_V.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 576-576-576-567-567-657-765-567-567-567-516-165-615-156-615-567-156-516-561-576-516-571-517-517-517-567-576-576-576-561-561-517-517-576-567-567-576-567-576-157-517-576-576-517-5_W.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 316-753-675-760-705-361-316-106-301-301-132-132-326-320-372-754-574-157-107-751-620-260-602-601-160-601-160-236-326-312-312-765-675-672-602-607-106-617-617-617-671-574-735-605-2_V.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 675-675 361-263-321-672-672-672-602-671-627-627-276-765-756-756-765-670-263-260-126-102-102-102-103-163-103-106-103-750-716_W.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 362-213-132-123-132-153-316-132-102-201-261-206-657-651-167-261-123-132-316-321-173-126-602-102-136 610-163-130-310-163-130-310-316-316-160-106-312-130-106-136-312-132-103-103-1_V.mp3)
  Segment 34: 52008ms ~ 53556ms (file: 103-123-132 325-627-260-163 105-102-132-671-631-163-567-162-623-102-167-167-167-106-612-103-130-136-103-163-167-136-321-156-163-312-136-103-617-106-103-130-671-613-130-163-561-6_V.mp3)
  Segment 35: 54556ms ~ 55132ms (file: 236-263-236-236-623-236-261-216-136-132-362-631-163-627-672-621-162-631-136-316-602-620-623-632-263-362-362-627-267-362-635-632-361-316-126-637-362-361-637-367-612 132-314-413 1_W.mp3)
  Segment 36: 56132ms ~ 56708ms (file: 632-632-623-627-260 236-312-267_W.mp3)
  Segment 37: 57708ms ~ 58284ms (file: 620 126-267-573-576-576-567 671-617-162-623-263 675-673-673-761-625-623-621-253-102 627-216-267-623-215-102-652-301-103-130-132-136-162-132-763-167-613-127-126-167-123-167-136-1_V.mp3)
  Segment 38: 59284ms ~ 59860ms (file: 263-132-612-162 260-263 137-130-132-670-623-302-132-162-263-632-163-130-132-312-132-312-361-136-236-201-320-362-627-361-326-361-213-213-216-217-213-261-362-162-132-162-136-312-7_V.mp3)
  Segment 39: 60860ms ~ 61436ms (file: 126-152-152-521-562-563-165-613-651-156-167-165-165-765-576-651-651-651-531-567-567-567-567-567-567-567-576-567-576-652-536-351-562-526-675-571-567-537-152-625-526-351-315-315-1_W.mp3)
  Segment 40: 62436ms ~ 63012ms (file: 761 675-765-675 765-756-756-756-675-765-576-765-756-617-162 216-672-621 162-312 621-360-267-261_W.mp3)
  Segment 41: 64012ms ~ 64588ms (file: 163-163-103-265-672-512-617-106-106-136-106-306-136-167-106-167-124-103-103-103-162-130-152-312-167-136-261-103-163-651-165-671-106-106-165-751-216-130-107-103-103-316-103-235-1_V.mp3)
  Segment 42: 65588ms ~ 66164ms (file: 625-627-267-623-231-632-675-261-162-621-362_W.mp3)
  Segment 43: 67164ms ~ 67740ms (file: 651-513-156-540-574-572-256-236-325-321-356-315-371-327-321-326-213-325-635-321-321-312-312-321-132-123-326-325-315-312-237-237-236-236-263-236-236-231-273-273-273-217-275-235-2_W.mp3)
  Segment 44: 68740ms ~ 69316ms (file: 756-675-627-765-675-671-657-756-765-576-567-576-756-567-672-615-167-657-567-627-316-615-672-162-265-267-162-127-267-265-163-261-761-165-625-562-675-157-627-652-625-567-657-652-5_W.mp3)
  Segment 45: 70316ms ~ 70892ms (file: 132-132-321-320-103-567-526-526-571-576-532-576 567-675-326-132 176-765-152-637-623 172-176-321-231-574-517-276-721-716-173-761-726-267-632-326-312 564-362-617-132 123_V.mp3)
  Segment 46: 71892ms ~ 72468ms (file: 256-236-236-257-265-267-267-267-267-263-267-276-263-276-263-236-321-132-312-132-137-136-317-316-136-541-531-153-513-265-276-625-326-236-523-125-265-136-316-231-263-326-321-312-1_W.mp3)
  Segment 47: 73468ms ~ 74044ms (file: 206-632-261-103-106-102-103-102-107-716-120-103-102-103_V.mp3)
  Segment 48: 75044ms ~ 75620ms (file: 106-130-136-132-316-631-652-564-165-103-517-576-657-576-675-765-765-132-576-513-756-572-756-754-751-176-675-576-576-657-576-567-675-675-675-657-756-765-756-752-576-576-567-561-6_W.mp3)
  Segment 49: 76620ms ~ 77196ms (file: 317-312-137-301-312-570-571-175-617-615-265-260-126-163-103-132-312-132-213-103-143-130-675-617-165-137-762-103-615-106-163-107-564-167-157-126 301-312-326-543-574-153-507-153-1_V.mp3)
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
> **Time**: 2026-04-01 05:13:40
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "포커스 양보가 필요합니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "포커스", "start_ms": 10, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "양보가", "start_ms": 200, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "필요합니다", "start_ms": 370, "dur_ms": 190, "phase_lr_deg": 0.0}
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
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-01 05:13:40*
