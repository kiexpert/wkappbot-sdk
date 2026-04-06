# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=10:00:27  uptime=31272s  mem=1942MB  system_processes=445"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_100017.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 765-763-136-136-236-261-136-316-136-130-103-321-123-213-321-326-206-620-623-103-312-312-136-126-160-162-631-673 756-576-756-567-576-756-756-576-576-576-756-765-576-567-750-576-7_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 603-603-321-132-310-103-162-126-216-206-203-263-123-302-326-620-260-263-612-564-562-572-526-627-627-627-267-627 756-765-675-765-756-576-672-276-576-567-657-356-517-126-123-542-6_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 716-167-106-130-106-310-132-312-620-620-620-623-320-231-326-312-301-206-206-230-203-206-201-320-302-321-231-320-231-572-370-307-301-310-130-301-310-130-310-321-230-627-162-206-2_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 132-670-125-173-173-371-623-635-631-765-731-136-623-126-162-316-213-126-103-136-136-123-106-236-261-176-162-130-167-162-136-613-160-301 263-261-617-672-624-132-413-132-631-623-2_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 215-326-361-362-361-356-356-365-365-653-635-653-657-657-657-653-635-365-230-231-231-231-213-215-263-231-263-653-367-357-657-625-657-675-351-316-317-375-375-356-352-367-357-375-3_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 175-362-213-136-136-123-321 763-136-163-167-165-165-617-132-362-132-316-130-126-362-712-216-603-132-167-162-601-106-106-153-162-160-130-623-631-163-163-102-132-216-612-127-126-1_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 236-257-756-765-276-675-657-276-316-362-316-531-571-325-236-256-265-265-216-265-625-256-763-673-762-276-312-137-125-275-327-326-276-265-512-356-236-257-562-257-652-536-356-531-5_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 103-751-176-175-103-160-670-107-163-167-176-167-136-317-136-612-230-230-261-123-762-762-762-760-760-706-760-760-706-607-671-613-260-203-261-271-765-671-617-617-167-106-106-301-6_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 765-576-567-105-126-601-650-561-130-321-302-321-206-260-620-260-620-657-506-627-617-653-263-627-103-136-103-130-132-135-135-526-103-103-103-137-357-571-574-574-574-572-571-571-3_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 163-216-261-267-623-136-627-621-637-162-657-657-675-267-172-726-276-267-731-317-127-723-617-627-672-627-672-607-627-276-623-623-362-231-516-153-672-756 263-216-213-672-263_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 607-672-675-601-613-176-516-160-107-105-176-163-672-754-765-571-107-541-317-123-762-120-160-106-176-126-671-657-617-675-672-672-675-671-671-672-612-621-156-361-162-163-163-627_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 325-326-257-253-270-260-256-621-623-261-231-236-263-263-362-256-562-756-567-576-571-572-572-672-671-671-672-657-625-672-627-623-726-612-316-516-217-123-217-671-627-562-672-312-6_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 546-361-362-326-263-236-361-536-540-524-265-263-265-563-652-653-326-236-547-576-162-216-215-621-263-236-326-321-325-352-524-572-765-540-623-263-265-265-562-564-456-546-623-623-2_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 163-132-134-132-136-103-137-103-132-231-234-206-206-267-261-132-312-301-137-174-176-176-310-301-320-675-675-675-672_V.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 236-126-312-350-132-753-351-316-103-206-236-103-675-103-231-576-103-132-571-571-176-721-267-176-172-376-176-671-160-612-652-236-756-756-561-176-561-607-657-657-136_V.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 657-567-765-675-675-675-576-675 312-623-103-136-103-136-106-163-156-506-175-162-126-167-132-167-106-561-126-261-126-102-103-132-137-103-106-165-612-162 163-627-261-103-326-163-1_V.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 752-576-567-267-213-103-132-514-103-127-156-136-160-165-136-167-136-765-160-617-215-276-176-152-103-165-156-136-103-167-650-617-156-132-106-157-167-152-132-167-623-167-621-162-1_V.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 631-162-176-103-167-106-576-276-572 231-672-627-627-213-620-160-620-625-621-672-675-601-627-627-762-657-765-675-627-675-657-675-567-765-675-765-675-765-675-675-671-675-675-675-6_W.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 176-123-571-327-635-163-106-106-631-162-167-657-260-103-106-612-162-103-136-175-106-105-153-561-716-162-173-167-106-167-126-106-765-176-106-167-106-756-176-167-160-162-167-165-1_V.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 167-672-627 513-276-362-327-237-275-237-231-267-132-265-672-675 136-756-765 762-672-321-630 671-671-672-621-267-632-631-263-621-162-136-172-512-562-263-261-257-265-631-263-312-7_W.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 136-605-265-163-167 601-765-670-632-126-162-316-136-132-130-102-126-103-106-167-156-120-156-103-321-635-103-103-136-521-631-261-176-162-103-123-106-167-216-136-102-107-615-615-6_V.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 675-675-675-756-625-263 105-570-576-576-756 302-206-320-602-260-602-230-321-310-130-310-206-320-260-321-206-230-301-231-276-301-102-132-307-237-312-263-162-163-312-132-231-576-1_V.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 105-150-315-651-123-321-216-312-361-231-316-123-613-326-123-132-173-351-173-175-576-571-756-321-317-321-325-312-235-312-326-312-312-235-316-326-312-317-316-123-671-315-571-167-1_W.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 503-735-103-570-105-107-571-573-257-732-203-307-321-360-263-623-635-623-576-230-250-756-230-320-673-302-203-732-302-236-231-320-236-263-326-362-362-263-326-327-301-521-321-321-1_W.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 621-201-106-106-623-126-261-263-706-301-163-302-612-301-136-103-103-137-103-312-261-123-301-321-316-321-657-657-650-675-617-312-103-106-136-103-103-134-103-137-174-176-167-756-7_V.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 132-102-103-106-167-127-123-103-312-120-176-152-673-625-163-103-103-106-136-126-562-765-170-612-132-567-321-163-657-103-102-120-201-172-612-162-103-132-103-136-137-176-176-132-3_V.mp3)
  Segment 27: 40976ms ~ 47852ms (file: 103-675-170-107-612-136-317-106-160-621-213-312_V.mp3)
  Segment 28: 48852ms ~ 55368ms (file: 213-163-263-261-162-206-103-762-671-167-106-167-276-263-106-106-217-176-165-160-136-671-176-106-136-106-106-237-360-152-126-315-103-167-163-103-103-617-106-206-102-216-152-103-1_V.mp3)
  Segment 29: 56368ms ~ 56944ms (file: 613-231-365-657-657-163-267-562-712-724-365-274-504-321-135-132-273-321-123-123-120-132-130-103-762-207-270-217-267-276-267-126-132-714-167-123-351-167-312-302-263-201-207-206-3_V.mp3)
  Segment 30: 57944ms ~ 58520ms (file: 203-260-613-162-107-123-126-321-760-607-670-670-756-765-706-756-206-206-236-321-302-321-167-301-312-321-103-213-231-107-123-263-267-216 206-320-320-302-302-267-206-276-103-106-3_V.mp3)
  Segment 31: 59520ms ~ 60096ms (file: 527-527-527-325-571-537-534-531-153-715-735-735-715-257-175-152-165-152-567-564-561-562-567-567-652-627-625-625-637-671-215-275-231-273-275-235-375-275-351-537-543-540-357-356-5_W.mp3)
  Segment 32: 61096ms ~ 61672ms (file: 670-607-607-607-607-672-672-621-261-621-301-631-637-157-627-216-631-162-631-160-173-126-167-613-376-361-176-107-130-312-312-123-106-321-126-620-267-236-637-652-620-263-627-623-6_W.mp3)
  Segment 33: 62672ms ~ 63248ms (file: 135-523-524-570-326-326-312-352-163-132-301-310-316-136-576-326-302-136-203-163-567-756-276-623-602-136-312-327-123-235-137-321-302-375-756-753-573-576-375-321-302-301-231-321-5_W.mp3)
  Segment 34: 64248ms ~ 64824ms (file: 132-263-623-367-321-103-136-130-612-126-267-206-236-326-261-213-163-612-415-503-126-765-307-132-312-263-263-162-321-213-326-103-301-102-261-621-650-261-316-321-312-132-163-213-2_V.mp3)
  Segment 35: 65824ms ~ 66400ms (file: 762-765-762-672-765-675-672-627-672-765-765-675-672-672-762-675-762-672-672-762-762-762-760 132-176-106-163-136-130-132-132-163-132_W.mp3)
  Segment 36: 67400ms ~ 67976ms (file: 267-632-260-261-210-716-201-120-321-132 316-132-625-675-756-670-657-507-562-672-671-621-163-136-176-617-627-613-103-316-103-162-620-621-762-627-627-672-627-627-360-360-632-136-5_V.mp3)
  Segment 37: 68976ms ~ 69552ms (file: 103-310-310-130-301-130-301-765-567-756-576-570-756-675-610-561-617-165-126-106-671-657-657-657-675-617-617-127-103-705-571-765-576-675-617-106-567-613-103-103-132-617-612-167-6_V.mp3)
  Segment 38: 70552ms ~ 83476ms (file: 106-136-106-103-176-672-712-106-671-316-106-632-217-106-136-160-163-126-176-612-326-312-576-756-756-657-765-715-157-756-657-671-671-675-617-162-617-103_W.mp3)
  Segment 39: 84476ms ~ 85052ms (file: 671 763-675-670-623-673-172-726-276 632 641-241-162-165-215-132-132-126-167-175-267-216-126-132-123-756-756 312-301-312 164-167-136-163-126-231-765-136-160-162-613-216-312-613-3_V.mp3)
  Segment 40: 86052ms ~ 86628ms (file: 265-265-261-263-261-261-265-236-326-261-265-236-263-236-231-327-132-312-261-261-235-216-236-231-236-326-326-236-216-231-231-231-236-267-267-231-326-326-231-321-236-236-326-321-2_W.mp3)
  Segment 41: 87628ms ~ 88204ms (file: 135-136-263-162-102-605-360-125-103-103-106-162-162-617-136-160-162-167-162-106-572-172-136-176-657-607-106-576-670-576-675-657-572-675-527-720-671-167-607-261-623-620-712-260-2_W.mp3)
  Segment 42: 89204ms ~ 89780ms (file: 134 136-103-160-512-106-136-610-103-761-160-752-275-517-102-103-215-103-136-103-103-167-165-165-562-516-120-162-263-261-216-527-576-576-574-574-576-567-576-751-751-576-756-576-5_V.mp3)
  Segment 43: 90780ms ~ 91356ms (file: 351-213-256-652-156-156-165-625-265-265-267-265-753-367-752-753-357-537-537-375-532-523-523-523-532-352-265-256-256-256-231-524-512-512-125-542-574-527-235-236-325-236-235-253-5_W.mp3)
  Segment 44: 92356ms ~ 92932ms (file: 621-621-621-263-627-267-621-627-263-162-621-612-621-267-267-276-627-623-623-261-261-267-263-621-267-132-102-132-120-261-106-163-631-613-163-267-672-627-627-671-625-625-657-621-6_W.mp3)
  Segment 45: 93932ms ~ 94508ms (file: 103-316-103-123-103-610-103-160-130-106-173-130-637-316-126-136-103-102-136-163-160-317-132-632-162-106-261-106-136-167-216-136-263-236-136-310-103-132-103-306-302-302-126-270-2_V.mp3)
  Segment 46: 95508ms ~ 96084ms (file: 157 165-672-351-316-673-617-316-675-675-765-617-615_W.mp3)
  Segment 47: 97084ms ~ 97660ms (file: 617-132-120-576-756-574-576-574-756-756-756-756-765 261-160-126-312-617-163-106-106-162-163-621-765-167-751-756-617-167-176-167-632-620-267-261-267-267-261-216-231-326-623-361-3_W.mp3)
  Segment 48: 98660ms ~ 99236ms (file: 176-175-106-176-125-167-601-610-106-162-617-263-126-621-103-136-176-106-325-167-126-103-132-103-710-107-103-106-157-126 103-316-106-163-175-103-127 103-765-632-756-671-103-137-1_V.mp3)
  Segment 49: 100236ms ~ 100812ms (file: 671-671-671-637 617-632-261-621 351-372-361-317-137-132-163-317-317-613-126-162-132-312-132-376-316-316-231-372-136-136-123-132-137-132-367-326-361-312-236-326-321-326-163-213-7_V.mp3)
  Segment 50: 101812ms ~ 102388ms (file: 613-613-162-612-620-260-623-126-765-756-756-576-652-602-162-167-761-675-607-106-165-106-602-631-613-750-716-715-312-162-756-706-672-675-675-627-160-621-612-617-617-675-605-650-6_W.mp3)
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
> **Time**: 2026-04-03 10:01:12
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
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
*Generated by WKAppBot ask gemini — 2026-04-03 10:01:12*
