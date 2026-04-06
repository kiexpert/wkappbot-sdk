# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=09:58:26  uptime=31151s  mem=1916MB  system_processes=445"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_095816.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 127-216-102-136-130-162-137-103-167-163-132-310-163-612-103-163-615-263-162-103-162-316-106-136-163-167-607-631-361-612-613-613-513-137-371-731-137-136-632-312-163-163-106-106-1_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 360-350-576-375-623-137-567-257-215-156-612-132-315-301-317-310-103-132-136-132-761-675-361-613-307-102-162-162-136-123-126-715-750-571-715-106-167-106-165-617-167-675-765-627-2_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 321-361-612-362-362-276-326-327-237-672-672-765-765-672-765-312-216-132-263-627-362-613-632-631-372-367-312-763-130-132-136-625-162-623-627-276-763-713-716-327-632-637-637-613-1_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 215-261-125-125-125-217-571-527-125-265-260-326-236-320-162-621-612-162-136-301-562-567-560-106-136-316 756-627-603-326-230-620-620-620-260-206-263-263-260-260-602-126-106-126-3_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 137-103-103-201-562-564-560-263-632-603-756-756-576-756-576-756-576-756-103-167-671-312-320-302-302-371-376-763-762-761-761-713-716-716-716-507-756-763-716-716-167-176-763-716-7_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 123-132-132-236-216-631-231-132-123-316-132-312 637-362-163-231-261-231-216-672-673-316-132-263-213-236-236-263-235-236-362-103-621-162-765-547-547-547-756-765-765-576-156-657-3_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 652-635-351-126-235-152-325-316-352-152-215-251-153-517-512-752-571-175-175-715-127-132-231-132-235-315-263-123-312-317-312-321-312-215-123-312-231-312-321-135-315-312-136-153-1_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 231-267-267-265-625-120-102-135-136-136-213-312-136-132-103-126-623-623-621-206-260-267-263-120-320-203-123-216-751-175-103-107-163-136-651-235-206-213-621-261-302-206-267-267-2_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 721-765-756-312-102-306-261-263-267-326-637-362-103-106-267-261-326-526-102-162-176-316-716 160-127-126-165-617-103-132-132-150-170-136 162-137-675-135-172-172-126-106-160-106-1_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 103-132-321-310-321-765-765-765-765-765-675-765-765-675 163-103-167-160-762-617-671-612-137-176-136-173-106-162-516-201-152-103-652-132-107-317-107-103-132-134-160-106-176-126-1_V.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 275-725-574-537-571-576-574-574-576-576-573-576-576-567-156-126-321-126-625-216-572-517-572-517-573-547-540-547-574-574-657-567-572-576-572-576-517-576-351-235-236-236-321-312-2_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 756-765-172-316-132-306-317-302-310-765-675-267-765-675-670-627-716-765-765-167-167-167-102-126-312-132-123-126-126-102-312-132-705-103-106-574-756-765-675-675-607-750-267-237-3_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 672-675-612-261-237-263-627-267-627-613-326-612-672-627-675-621-621-672-326-736-576-652-672-623-672-276-627-623-613-263-216-632-627-261-612-672-762-362-731-275-625-657-627-261-2_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 213-612-621-163-103-162-167-162-136-162-362-623-260-260-106-216-260-623-263-263-610-621-621-267-620-263-260-263-617-106-136-132-316-312-321-103-106-213-263-213-267-312-301-132-3_V.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 153-306-617-162-163-701-765-720-617-176-126-127-203-326-320-365-360-136-106-163-167-603-163-162-167-107-136-132-675-627-675-760-106-126-276-267-206-261-316-301-310-312-763-132-3_V.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 637-765-673-627-623-625-652-361-601-263-621-367-627-632-361-613-316-631-136-623-267-623-267-267-237-267-267-762-276-267-267-267-367-376-317-317-173-126-541-236-631-672-672-267-2_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 106-106-130-135-632-632-162-316-123-165-567-162-136-137-103-163-137-316-216-157-126-312-160-163-162-165-136-106-160-105-162-167-613-162-106-107-316-163-157-316-167-617-516-103-1_V.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 123-321-321-356-532-316-325-312-316-163-317-125-265-327-376-316-312-317-367-123-136-637-361-273-237-672-617-653-571-165-673-376-316-376-372-326-165-675-756-175-573-517-521-231-2_W.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 371-123-136-756-716-167-163-162-162-167-132-163-167-126-163-160-137-176-615-517-157-536-517-612-106-120-156-571-751-675-756-675-765-567-576-657-675-613-671-671-721_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 517-675-635-657-615-615-756-526-673-567-653-561-657-576-657-765-657-675-672-675-657-625-765-675-765-637-576-761-762-375-312-576-637-657-627-672-762-765-675-567-627-263-267-612-6_W.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 165-516-135-175-175-175-175-721-172-215-275-527_W.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 621-713-710-375-765-756-507-672-635-672-657-652-206-263-216-230-321-302-301-316-167-107-107-765-760-675-163-123-132-163-601-630-267-564-620-205-576-705-302-673-260-567-102-106-1_V.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 316-167-617-167-136-103-132-513-570-576-621-756-361-163-106-162-570-574-507-517-716-167-617-137-103-136-136-106-106-765-607-576-106-103-103-130-103-103-103-103-312-312-321-162-1_V.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 312-362-162-612-631-316-613-632-361-326-632-612-126-627-632-136-236-316-632-361-312-136-613-321-632-162-672-623-136-631-362-672-617 671-671_W.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 602-602-603-602-623-632-610-163-312-310-231-360-326-613-361-706-750-573-570-135-163-167-163-162-126-123-236-215-132-103-103-621-103-613-106-160-106-103-137-312-516-136-137-167-1_W.mp3)
  Segment 26: 39400ms ~ 42172ms (file: 267-216-126-216-216-312-176-216-206-216-201-210-261-207-725-257-725-627-627-613-263-627-276-321-302-126-362-210-231-321-326-273-321-321-132-136-621-263-632-762-765-765-671-132-3_W.mp3)
  Segment 27: 43172ms ~ 43748ms (file: 136-126-123-136-162-123-162-216-512-571-571-516-261-263-136-126-517-573-157-571-675-162-132-132-163-613-361-136-136-153-517-517-156-165-162-162-217-123-162-123-126-136-132-123-1_W.mp3)
  Segment 28: 44748ms ~ 45324ms (file: 165-136-576-130-167-176-103-761-136-160-103-150-701-715-165_V.mp3)
  Segment 29: 46324ms ~ 46900ms (file: 176-617-163-617-576-613-106-601-165-753-627-631-103-162-103-103-632-126-106-132-163-310-763-601-106-130-163-136-632-102-160-571-631-126-136-103-316-165-160-675-106-612-516-601-1_V.mp3)
  Segment 30: 47900ms ~ 48476ms (file: 267-267-267-267-267 536-103-312-132 106-130-103-671-514-627-536-132-761-167-103-106-316-137-316-125-615-103-126-675-360-362-136-765-130-176-132-652-671-127-130-103-156-602-615-6_V.mp3)
  Segment 31: 49476ms ~ 50052ms (file: 257-326-326-263-627-516-351-574-574-623-627-627-623-623-263-264-260-261-752-237-753-716-531-513-156-316-231-236-263-326-263-263-236-231-213-265-265-126-612-516-541-546-256-362-6_W.mp3)
  Segment 32: 51052ms ~ 51628ms (file: 312-102-123-120-132-514-754-706-765-701-726-276-271-276-162-167-163-136-153-261-651-623-316-312-267-206-762-267-623-326-376-576-365-312-637-136-103-107-216-216-103-316-137-623-6_V.mp3)
  Segment 33: 52628ms ~ 53204ms (file: 132-714-132-154-132-324 217-312-172-276-314-214-167-130-126-132-123-761-176-602-263-132-312-231-163-327-126-136-670-670-670-623-321-302 631-631 634-135-135-130-213-163 316 624-6_V.mp3)
  Segment 34: 54204ms ~ 54780ms (file: 123-103-571-302-306-653-267-321-132-321-126-126-312 132-132 103-103-326-231-216-236-623-623-362-231-321-123-312-325-352-231-123-126-123-126-123-123-163-657-627-620-627-263-562-5_W.mp3)
  Segment 35: 55780ms ~ 56356ms (file: 617-613-617-675-567-657-106-603-657-657-163-136-613-567-576-756-675-210-621-756-103-301-735-367-207-320-302 621-621-623-267-103-132-123 370-753-760-567-657-657-675-267-236 567-5_W.mp3)
  Segment 36: 57356ms ~ 57932ms (file: 103-136-103-136-130-163-761-163-302-675-163-671-261-216-163-361_V.mp3)
  Segment 37: 58932ms ~ 59508ms (file: 531-153-231-132-312-312-312-123-217-123-130-320-532-123-213-123-132-130-167-617-671-672-617-167-367-326-361-563-165-123-321-132-312-512-532-132-137-217-321-523-312-321-135-125-1_W.mp3)
  Segment 38: 60508ms ~ 61084ms (file: 236-321-231-135-106-103-130-301-301-103-316-571-130-165-106-175-160-316-106-621-126-176-106-163-130-132-312-102-167-127-130-130-103-563-265-617-162-671-137-130-102-231-103-126-1_V.mp3)
  Segment 39: 62084ms ~ 62660ms (file: 671-167-617-123-216-316-310-103-103-317-103-310-103-216-152-564-176-326-136-132-763-263-623-602-675-627-672-362-672-275-725-725-715-102-167-631-163-615-670-670-627-267-267-576 6_W.mp3)
  Segment 40: 63660ms ~ 64236ms (file: 136-316-126-162-617-106-162-162-163-156-261-765-176-602-135-103-135-103-621-216-267-726-126-312 526-756-362-631-671-126-162-106-103-163-137-751-157-167-167-126-162-106-137-154-1_V.mp3)
  Segment 41: 65236ms ~ 65812ms (file: 136-103-710-751-673-637-367-362-317-316-310-130-317-103-103-170-176-317-316-132-231-321-132-136-123-174-173-162-750-761-627-167-371-361-326-206-602-206-627-216-127-203-231-213-2_V.mp3)
  Segment 42: 66812ms ~ 67388ms (file: 657-567-671-672-617-670 657-675-617-267 312-136-136-361-632-312-631-136-261-312-123-256-312-203-765-756-657-576-675-261-627 137-157-165-516-564-326-612-136-123-157-301-130-167-6_W.mp3)
  Segment 43: 68388ms ~ 68964ms (file: 301-312-123-513-312-123 123-613-216-312 623-263-623-362-312-132-312-351-324-213-312-341-354-635-632-620-261-263-276-327-276-236-326-136-237-637-670-376-137-506-517-135-321-231-3_V.mp3)
  Segment 44: 69964ms ~ 70540ms (file: 301-320-320-370-302-736-756-730-237-312-103-132-132-132-750-736-376-750-765-765-627-206-203-236-231-123-601-617-612-570-507-573-302-312-236 765-765-576-574-275 623-362-762-376-2_V.mp3)
  Segment 45: 71540ms ~ 72116ms (file: 371-721-713-726-712-623-271-760-136-312-136-103-102-106-103-137-137-123-132-312-103-617-267-762-327-210-237-263-267-237-213-237-276-302-203-207-230-120-725-257-231-623-312-230-3_V.mp3)
  Segment 46: 73116ms ~ 73692ms (file: 263-236-267-672-762-726-302-320-312-123-732-162-321-275-312-312-316-326-320-326-306-627-165-635-675-576-352-302-206-230-201-764-723-320-132 123-125-316-163-536-540-135-305-352-2_W.mp3)
  Segment 47: 74692ms ~ 75268ms (file: 617-613-357-356-356-536-623-326-623-625-265-627-652-657-265 651-326-362-263-156-531-236-635-325-356-567-574-536_W.mp3)
  Segment 48: 76268ms ~ 76844ms (file: 157-135-721-321-132-231-327-103-136-130-320-312-320-312-120-320-321-312-312-123-157-175-175-173-137-327-302-321-157-132-123-132-163-321-327-132-312-123-312-310-130-310-316-132-1_V.mp3)
  Segment 49: 77844ms ~ 78420ms (file: 627-625-623-672-762-267-267-627-627-236-236-756-576-576-574-756-567-756-765-632-132-267-760-756-576-570-230-206-203-261-163-756-675-765-760-671-567-156-165-501-157-136-163-576-7_W.mp3)
  Segment 50: 79420ms ~ 89392ms (file: 765-176-106-103-167-106-103-671-617-103-165-105-106-152-516-126-106-106-103-136-125-167-160-671-165-137-132-152-105-127-160-163-106-167-156-130-130-137-136-107-517-167-160-715-1_V.mp3)
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
> **Time**: 2026-04-03 09:59:08
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
*Generated by WKAppBot ask gemini — 2026-04-03 09:59:08*
