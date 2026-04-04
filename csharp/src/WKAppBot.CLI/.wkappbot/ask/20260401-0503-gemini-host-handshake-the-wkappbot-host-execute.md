# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=14524  time=05:02:27  uptime=4738s  mem=1657MB  system_processes=755"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260401_050010.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 263-627-325-367-632-623-126-362-534-351-523-153-513-326-261-236-263-235-267-632-326-267-263-263-621-263-726-267-263-627-267-162-263-621-167-261-627-263-613-216-261-362-261-621-3_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 106-103-176-176-137-126-316-562-602-267-627-576-576-267-236-213-231-213-261-623-321-317-103-504-514-534-136-605-756-756-160-106-316-103-106-310-103-310-136-301-312-103-103-130-7_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 620-267-752-270-230-672-706-301-206-206-621-360-326-130-316-136-162-106-103-316-301-136-163-106-602-163-106-136-627-360-360-752-756-756-705-207-760-276-261-162-621-620-620-621-6_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 160-167-167-512-562-136-103-132-103-103-751-623-627-126-602-621-261-260-260-620-627-607-602-316-301-312-312-301-302-302-326-132-132-236-623-627-103-106-162-162-167-106-160-136-1_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 126-261-265-132 617-106-162-106-265-670-263-236-231-260-326-617-326-106-106-203-610-136-617-163-137-326-316-267-163-517-156-120-765-106-165-163-136-167-612-516-615-162-106-135-6_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 160-162 321-213-230-261-236-123-236 761-165-567-516-517-157-567-537-574-574-574-576-236-623-623 657-657-675-675-657-675-675_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 672 237-327-610-531-621-136-213-231-302-754-754_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 671-672-267 217-365-627-263-326-256-156-567-672-267 312_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 106-106-136-132-126-132-751-716-123-103-260-201-620-260-236-302-320-312-312-320-312-310-230-123-310-103-603-236-631-301-105-102-126-206-236-206-621-176-736-317-172-301-301-301-5_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 167-671-610-610-672-106-167-671-103-102_V.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 263-567-562-256-562-574-542-526-265-321-752-752-752-257-275-237-215-125-527-352-325-521-235-231-236-236-523-235-321-231-321-135-312-315-236-236-523-524-254-321-235-231-253-527-2_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 572-567-725-527-256-256-765-257-652-627-652-652-265-756-276-576-267-526-567-627-563-652-623-627-325-657-625-652-526-672-376-725-732-263-632-627-162-672-627-765-623-362-627-612-6_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 567-756-157 362-362-321-123-362-326-263-267-263-236-612-123-213-216-267-267-627-627-627-362-367-631-627-367-627-673-625-657-653 326-326-367-316-361-263-267-267-627-672-627-623-6_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 216-153-150-156-106-265-216-256-216-135-351-135-315-315-265-231-325-263-235-216-135-315-135-256-215-216-126-216-512-152-153-132-176-157-215-267-213-236-216-236-236-326-325-325-5_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 156-157-631 106-103-130-103-130-310-230-206-236-126-162-123-260-263-612-632-621-632-625-625-576-756-576-765-625-627-573-573-651-615-657-267-126-317-127-263-567-572-574-574 675-7_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 102-567-567-516-256-623-210-610-206-612-167-765-567-650-160-617-765-106-756-756-130-103-163-106-716-126-132-162-123-726-726-123-312-310-130-310-106-310-163-106-136-163-107-675-1_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 130-165-105-165-162-260-236-326-206-602-260-167-160-301-103-756-103-123-136-103-132-103-321-301-306-301-620-620-326-320-301-130-310-312-203-203-213-132-701-201-302-502-507-351-1_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 103-326-130-312-321-230-230-203-203-312-103-103-103-103-160-102-302-301-312-103-103-132-316-301-132-302-312-106-316-501-512-312-136-317-310-360-103-136-130-123-301-231-315-516-1_V.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 237-526-267-726-761-136-537-536-231-236-326-275-365-512-320-103-176-132-136-106-653-675-165-106-135-103-163-167-567-356-156-120-376 136-103-137-127-127-132 514-261-576-162-103-1_V.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 607-672-762-627-612-672-302-312-172-762-602-613-106-132-126-106-136-760-106-132-217-321_V.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 267-132-261-267-621-263-253-103-163-162-672 763-762-763-765-762-736-372-723 167-173-317-173-312-132-163-321-123-621-623-672-637-261-623-761-761-751-715-175-175-175-172-621-162-1_W.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 132-103-137-135-163-126-137-317-173-137-327_V.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 301 316-321-632-236-263-326-302-312-302-321-326-203-206-203-723-376-326-601-612-367-271-670-607-206-260-261-206-620-206-276-260-217-370-675-637-627-260-260-261-206-201-123-162 7_V.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 576 263-326-321-130-130-103-267-632-167-123-132-103-132-130-231-123-261-321-263-126-231-316-103 263-267-762-762-672-762-736-321-236-261-256-321-236-236-326-236-326-627-627-236-3_W.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 125-123-312 103-132-123-127-143-132-236-132-126-316-132-213-261-263-123-132-132-126-136-126-675-673-136-136-316-760-671-671-176-163-103-132-123-132-136-132-103 765-671-162-617-1_V.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 132-306-321-132-136-310-321-321 617-162-160-621-602-601-610-160-601-603-631-361-316-601-630-103-163-617-601-610-601-610-162-601-106-103-163-106-106-623-260-236-260-620-675-672-7_V.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 263-261-216-263-216-321-625-631-617-617-617-167-167-613-163-160 756-761-765-765 765-756-756-756-765-756-756-756-762_W.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 152-513-527-162-652-652-352-326-326-362-367-367-362-637-375-631-316-617-365-376-657-235-327-235-231-263-235-625-157-567-560-657-657-567-536-571-537-371-172-231-217-517-576-267-5_W.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 326-267-260-630-362-326-263-203-260-216-362-610-310-136-126-360-236-106-163-756-756-260-230-326-312-321-620-260-621-102-102-263-230-326-231-263-620-620-623-620-621-625-260-312-1_V.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 613-132-217-237-372-672-576-256-574-576-265-574-752-675-756-715-672-675-576-570-236-213-213-231-263-160-165-541-541-321-362-236-326-623-236-263-263-236-263-236-263-263-326-236-2_W.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 167-167-167-675-672-620-621-236-261-263-276-256-265-605-605-126-261-326-126-162-136-136-136-312-316-361-316-627-261-612-613-651-613-627-170-657-627-316-756-673-763-127-765 672-6_V.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 672-306-130-130-137-672-675-672-675-756-507-506-576-570-765-602-760-576-576-276-627-360-673-316-103-263-562-563-365-275-625-627-657-752-756-725-750-617-617-167-105-157-156-561-5_W.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 756-756-750-705-162-326-302-106-106-170-601-601-106-162-162-167-132-231-627-206-132-612-236-102-320-263-620-260-263-302-325-372-316-312-310-371-310-621-103-132-165-231-623-163-3_W.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 103 167-136-207-267-627-163-136-103-601-206-206-236-206-203-312-103-312-301-613-371-130-107-765-761-765-765-756-736-572-326-201-123-312-130-123-126-203-671-601-367-302-762-761-6_V.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 765-675-675-376-613-360-103-106-103-130-103-613-163-613-106-137-517-126-127-136-316-136-316-236-102-132-361-632-312-103-132-130-103-132-620-321-623-132-261-263-267-267-276-726-7_V.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 657-657-657-567-657-653-627-621-261-615-657-607-765-761-675-657-675-657-672-167-621-136-627-627-602-267-276-761-321-567-756-567-376-675-106-130-106-103-761-157-175-671-160-106-1_W.mp3)
  Segment 37: 56736ms ~ 60588ms (file: 672-167-263 612-632-627-672-627-627-672-672 126-162-126-132-621-216_W.mp3)
  Segment 38: 61588ms ~ 62164ms (file: 316-316-136-135-135-132 231-602-605-602-621-623-672-163-136-132-134-143-216-264-261-312-705-312-321-312-607-362_V.mp3)
  Segment 39: 63164ms ~ 63740ms (file: 310-130-102-132-316-132-136-132-236-261-316-106-261-610-561-513-516-563-561-157-276-156-513-576-567-607-762-675-126-675-675-675 201-123-102-132-103_W.mp3)
  Segment 40: 64740ms ~ 65316ms (file: 672-153-214-321-132-627-162-671-362-132-706-756-576-756-705-576-756-756-576-257-176-163-670-761-703-763-713-137-413-314-573-154-531-132-671-671-132-173-632-167-132-106-312-132-6_V.mp3)
  Segment 41: 66316ms ~ 66892ms (file: 567-576-130-120-756-170-150-517-176-106-267-230-762-627-612-176-675-675-705-652-716-761-710-102-106-310-126-106-162-102-132-123-123-726-627-236-263-310-216-263-362-627-267-627-1_W.mp3)
  Segment 42: 67892ms ~ 68468ms (file: 176-312-132-721-756-765-716-761-176-715-761-756-756-756-716-756-756-321-312-236-372-321-210-176-167-126-126-132-612-106-176-123-163-312-326-261-361-632-320-312-213-123-301-130-3_W.mp3)
  Segment 43: 69468ms ~ 70044ms (file: 256-765-123-321-123-216-103-357-532-352-375-317-357-574-574-574-547-265-275-265-264-240-264-245-254-527-527-365-125-231-362-312-562-536-136-123-356-351-236-567-356-213-326-163-1_W.mp3)
  Segment 44: 71044ms ~ 71620ms (file: 576-157-153-513-135-135-351-157-156-135-163-153-157-156-175-157-153-153-175-165-516-561-536-356-536-157-153-156-576-567-156-156-135-516-157-157-156-516-137-536-165-651-516-561-5_W.mp3)
  Segment 45: 72620ms ~ 73196ms (file: 167-176-157-135-135-162-621-163-216-126-162 136-167-761-271-172-176-127-620-103-132-671-167-176-210-612-360-316-137-176-716-106-162-167-617-106-102-316-136-710-675-160-617-765-2_W.mp3)
  Segment 46: 74196ms ~ 74772ms (file: 263-206-136-302-136-103-136-301-260-230-320-312-213-312-312-103-623-326-632-362-312-130-301-103-103-321-320-203-352-312-312-310-312-267-260-260-260-260-260-320-302-123-126-132-6_V.mp3)
  Segment 47: 75772ms ~ 76348ms (file: 216-623-126-216-152-250-253-520-526-126-126-752-574-574-574-574-574-547-574 620-725-572-567-756-756-756-756-756-672-136-126-162-102-126-132-163-163-610-617-126-751-163-132-357-3_W.mp3)
  Segment 48: 77348ms ~ 77924ms (file: 236-261-213-231-316-261-216-321-326-376-326-326-236-267-362-263-352-235-326-263-267-236-236-263-627-267-326-236-236-540-365-263-627-627-672-627-635-635-632-263-253-627-627-625-6_W.mp3)
  Segment 49: 78924ms ~ 79500ms (file: 576-536-651-651-153-165-163-132-163-605-156-561-561-562-567-576-567-567-571-561-156-157-516-567-567-572-512-561-576-561-516-561-567-561-516-561-567-651-567-567-567-613-163-561-6_W.mp3)
  Segment 50: 80500ms ~ 82264ms (file: 765-725-762-671-567-160-106-106-106-103-263-316-623-260-672-263-627-621-261-312-321-132-263-163-620-263-236-326-362-326-306-316-360-306-321-362-623-213-632-603-650-356-576-576-5_W_stt_the_glove.mp3)
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
> **Time**: 2026-04-01 05:03:19
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
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "포커스", "start_ms": 20, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "양보가", "start_ms": 210, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "필요합니다", "start_ms": 380, "dur_ms": 170, "phase_lr_deg": 0.0}
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
*Generated by WKAppBot ask gemini — 2026-04-01 05:03:19*
