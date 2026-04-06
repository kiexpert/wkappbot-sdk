# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=09:32:24  uptime=29590s  mem=1833MB  system_processes=445"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_093215.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 167-153-576-567-657-165-627-132-261-123-132-132-106-162-671-316-574-572-756-576-675-652-136-176-761-613-376-135-671-156-617 362-536-567-563-675-635-312-312-167-136-163-367-607-6_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 765-576-571-756-761-675-652-163-672-167-601-657-627-625-275-276-267-632-302-703-730-721-721-675-672-267-267-627-623-165-601-627-267-621-627-602-621-620-672-671-157-610-167-617-1_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 631-136-163-163-167-106-106-163-162-103-206-236-760-760-607-627 217-123_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 562-517-536-326-625-231-237-236-216-263-213-261-263-216-215-210-263-263-675-267-261-213-251-267-260-263-312-132-312-231-321-326-237-273-206-756-276-267-267-267 103-130-362-310-1_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 574-754-754-752-107-106-130-321-132-310 621-261-126-106-621-620-620-260-260-260-623-123-132-310-321-312-310-130-206-210-231-236-321-210-312-310-130-310-312-136-132-602-260-756-7_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 276-627-206-325-512-152-132-231-136-136-123-672-261-231-321-261-123-231-502-302-130-163-163-136-201-276-132-136-301-316-321-312-103-123-103-310-316-306-321-312-263-613-263-623-1_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 167-163-176-671-106-761-160-106-216-162-120-610-106-136-172-102-216-162-132-136-103-162-236-102-302-103-162-301-671-126-103-132-103-160-123-316-132-126-367-321-231-321-163-176-6_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 756-576-751-756-670-176-705-756-763-567-365-351-351-605-570-753-576-576-567-576-321-103-506-651-365-576-365-675-312-316-173 675-657-761-765-765-675-675-765-672-761 276-261-612-1_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 102-102-603-632-602-632-163-162-103-130-301-613-310-310-173-172-213-312-132-106-167-312-136-103-162-136-136-136-316-136-316-316-263-621-162-126-572-372-317-173-570-135-312-316-5_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 675-615-756-176-701-132-413-621-134-132-413-637-132-675-670-675-376-312-132-316-134-236-321-132-103-136-134-316-136-312-136-136-132-103-135-105-625-125-671-136-617-163-106-167-1_V.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 216-260-136-130-125-102-263-263 617-756-715-765-167-107-163-162-137-316-123-123_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 306-206-103-106-321 126-162-132-261-123-326-321-627-103-132 756-750-576-574-216 163-162-167-127-756-753-216-162-167-765-765-761-765-756-675-675-675-765-761-701-751-715-756-756-2_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 126-751-263 756-576-756-576-215-762-657-765-132-160-103-312-130-760-103-136-162-361-103-514-350-627-167-103-136-675-316-631-162-132-136-156-103-136-136-612-260-516-576-103-167-1_V.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 126-632-570-673-360-367-362-310-130-301-130-310-162-610-162-302 126-362-360-132-136-132-210-316-102-302-301-361-312-601-632-213-316-132-203-203-320-302-523-306-263-203-320-320-3_V.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 627-756-567-327-263-726-367-632-312-216-625-627-163-126-162-213-627-671-672-672-756-716-756-562-265-265-263 675-765-765-127-126-276-236-726-276-216-216-762-762-762-623-672-752-5_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 576-675-675-765-167-126-671-671-576-257-271-652 165-613-367-617-671-627-765-675-675-720-602-560-671-607-607-673-765-567-675-675-675-672-607-761-675-765-672-312-675-216-621-765-7_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 103-261-310-130-136-316-167-127-103-136-103-103-156-165-135-617-106-103-137-607-160-316-136-276-263-162-136-163-106-136-106-162-367-563-507-576-657-567-570-567-570-132-136-210-3_V.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 563-325-326-312-321-123-312-213-312-123-321-231-321-321-123-213-236-123-235-213-261-216-231-321-231-261-231-213-123-132-126-126-132-312-136-321-325-513-512-536-235-623-625-362-2_W.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 675-675-670-607-631-652-625-652-625 137 762-762-103-103-156-756-603-310-301-103-123-312-163-613-162-162-316-754-715-107 132-321-132-620-672-621-623-132-217-127 132-631-631-621-7_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 203-623-206-563-506-312-301-302-302-710-301-316-632-603-630-602-126-316-306-623-621-267-126-106-162-316-316-103-136-103-306-260-620-260-236-263-301-236-263-210-126-276-132-103-3_V.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 231-213-136-132-123 175-136-136-201-231-120-130-316-103-310-132-501-156-136-163-132-362-160-571-106-102-102-106-357-130-310-302-312-530-750-756-306-306-320-320-301-103-132-132-1_V.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 302-310-530-537-305-362-302-231-310-136-163-106-132-132-673-601-103-612-236-236-260-320-206-231-236-653-123-361-567-136-132-136-136-106-103-106-126-130-136-106-163-136-167-617-1_V.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 562-157-132-651-130-165-672-516-526-651-256-261-163-671-362-263-163-265-567-567-675-635-601-263-267-762-163-102-367-612-651-673-617-263-621-675-672-671-653-231-631-216-265-367-3_W.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 725-217-572-257-270-207-726-726-256-267-201-163-103-267-126-132-312-702-752-725-725-675-273-263-271-132-261-216-236-236-263-615-365-136-516-165-516-361-261-572-103-132-162-213-6_V.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 675-672-130-103-132-123-216-176-126-136-217-671-261-106-372-136-136-136-136-236-302-261-362-136-260-316-310-632-361-316-126-631-106-163-310-316-136-132-312-263-263-602-623-613-1_V.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 675-756-327-236-237-236-234-267-231 726-620-623-126-136-261-263-206-132-230-321-203-236-213-570-321-301-302 176-752-752-207_V.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 623-632-263-326-326 623-263-261-623-236 765-765-716_W.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 672-162-563-165-716-176-173-716-176-176-617-637-721-312-203-137-157-765-167-751-617-671-176-167-136-176-715-671-176-157-132-307-302-302-637-176-756-762-765-675-671-761-765-621-5_W.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 172-713-702-732-372-106-721-732-102-160-167-160-612-620-310-361-137-513-136-120-270-310-317-325-301-137-130-310-137-130-317-103-130-165-617-126-672-276-136-312-326-162-635-320-3_V.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 126-162-162-156-125-176-157-125-156-165-156-157-163-165-156-176-167-156-165-165-165-150-150-106-576-130-150-160-160-106-165-167-160-615-167-160-617-607-167-671-615-165-176-167-6_W.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 276-627-362-263-236-261-263-265-265-526-256-657-657-657-567-652-675-762-675-716-671-165-653-563-265-216-623-263-236-263-267-263-623-632-613-652-653-652-365-236-625-625-613-523-5_W.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 216-136-103-321-132-213-260-132-175-621-123-167 136-216-610-136-615-756-256-162-136-132-617-136-163-610-362-236-620-623-132-126-132-132-526-613 160-136-106-123-103-672-610-602-6_V.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 162-160-612-102-123-106-320-310-132-106-106-106-210-261-206-263-260-206-261-675-602-602-206-162-256-572-652-617-162-167-162-163-601-607-210-620-620-623-206-621-106-106-106-672-2_W.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 130-130-126-413-143-413-312-157-135-351-132-321-136-132-103-103-103-310_V.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 127-310-136-617-162-627-132-106-263-617-167-167-130-137-152-136-136-160-137-136-167-167-127-620-761-126-765 132-106-126-570-561-126-126-167-216-107-132-102-165-317-103-132-610-1_V.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 574-573-531-357-537-537-574-574-576-754-574-357-537-573-534-574-574-547-574-574-571-257-256-267-265-621-267-267-516-725-652-756-127-627-675-762-167 263-362-362-627-671-167-673-6_W.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 612-120-103-672-621-260-126-617-562-627-761-621-657-156-152-267-123-160-106-163-126-103-165-163-103-251-360-167-657-126-632-623-130-136-136-715-137-157-371 623_V.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 105-105-136-123-261-201-210-210-210-210-120-210-102-102-123-130-126-317-105-132-132-671-672-570-756-750-756-623 756 136-610 675-602-602-261-576-531-236_W.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 312-132-312-236-173-312-152-302-327-132-172-761-716-761-756-576-312-321-532-325-132-136-132-302-312-231-321-103-167-157-175-157-137-315-317-231-623-103-132-132-321-123-103-675-6_W.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 231-312-321-325-325-315-132-352-537-527-351-527-357-176-361-675-671-165-562-253-265-631-213-327-762-276-213-123-253-526-532-352-573-357-357-257-375-361-137-136-126-152-351-125-1_W.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 341-312-312-671-263-671-634-312-163-162-361-312-413-215-364-175-132-143-134-132-106-621-162-614-625-143-137-132-104-132-173-176-135-670-567-756-765-107-157-163-136-217-137-132-6_V.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 601-162-106-103-106-103-301-130-317-136-103-163-206-132-301-173-130-132-103-136-136-132-102-106-315-310-320-260-320-203-630-610-267-762-672-670-130-316-361-301-751-574-754-750-3_W.mp3)
  Segment 43: 66192ms ~ 66768ms (file: 106-210-176-132-152-103-103-517-261-157-176-126-276-326-213-627-263 265-362-267-263-260-236-623-267-263_W.mp3)
  Segment 44: 67768ms ~ 68344ms (file: 267-326-237-236-312-263-623-267-326-236-263-321-213-263-621-261-126-213-216-261-132-316-261-623-326-625-526-235-267-627-627-621-263-263-627-627-623-627-236-263-265-625-627-263-6_W.mp3)
  Segment 45: 69344ms ~ 69920ms (file: 610-163-167-367-761-136-136-103-136-132-103-156-571-561-176-176-167-716-162-167 617-671-617-672-263-621-216-620-263-263-261-621-160-137-136-136-163-673-102-132-170-136-106-103-1_V.mp3)
  Segment 46: 70920ms ~ 71496ms (file: 613-612-617-106-670-160-106-620-621-620-206-261-167-167-136-162-261-671-617-673-671-671-617-163-321-203-203-675-765-672-627-267-216-675-260-620-612-621-206-123-103-162-106-675-1_V.mp3)
  Segment 47: 72496ms ~ 73072ms (file: 762 125-672-673-613-326-312-361 326-623-362-623-136-167-176-103-107-132_W.mp3)
  Segment 48: 74072ms ~ 74648ms (file: 715-157-157-156-120-261-613-123-123-215-253-132-103-152-251-321-251-520-523-520-512-256-512-571-562-526-576-132-136-176-137-132-362-176-671-136-127-136-136-132-160-367-167-617-6_V.mp3)
  Segment 49: 75648ms ~ 76224ms (file: 673-132-123-670-672-361-316-567-576-156-152-751-317-731-175-107-167-153-176-167-165-651-675-620-602-261-612-613-372-325-625-362-316-316-315-751-175-106-160-567-653-136-132-103-1_W.mp3)
  Segment 50: 77224ms ~ 77800ms (file: 136-561-167-672-657-623-765-756-760-167-167-123-701-173-617-167-167-673-670-756-630-310-126-136-167-312-132-163-167-126-216-216-276-203-260-236-126-120-657-267-260-201-623-632-3_V.mp3)
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
> **Time**: 2026-04-03 09:33:16
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "포크와 양보가 필요합니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "포크와", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "양보가", "start_ms": 200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "필요합니다", "start_ms": 360, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "어디서 내려요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "어디서", "start_ms": 55, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "내려요", "start_ms": 265, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "이거 얼마예요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "이거", "start_ms": 35, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "얼마예요", "start_ms": 245, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "영수증 주세요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "영수증", "start_ms": 65, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 275, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "화장실 어디예요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "화장실", "start_ms": 45, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "어디예요", "start_ms": 255, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "도와주세요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "도와주세요", "start_ms": 60, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "조심하세요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "조심하세요", "start_ms": 40, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "길을 잃었어요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "길을", "start_ms": 70, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "잃었어요", "start_ms": 230, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "배고파요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "배고파요", "start_ms": 40, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "목말라요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 75,
"audio_type": "speech",
"words": [
{"word": "목말라요", "start_ms": 75, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "졸려요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "졸려요", "start_ms": 50, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "피곤해요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "피곤해요", "start_ms": 80, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "아파요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "아파요", "start_ms": 35, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "괜찮아요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "괜찮아요", "start_ms": 65, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "알겠어요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "알겠어요", "start_ms": 45, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "모르겠어요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "모르겠어요", "start_ms": 55, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "맞아요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "맞아요", "start_ms": 35, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "아니요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "아니요", "start_ms": 70, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "좋아요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "좋아요", "start_ms": 45, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "싫어요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 75,
"audio_type": "speech",
"words": [
{"word": "싫어요", "start_ms": 75, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "기뻐요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "기뻐요", "start_ms": 50, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "슬퍼요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "슬퍼요", "start_ms": 80, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "화나요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "화나요", "start_ms": 30, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "심심해요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "심심해요", "start_ms": 65, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "재밌어요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "재밌어요", "start_ms": 45, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "맛있어요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "맛있어요", "start_ms": 55, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "맛없어요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "맛없어요", "start_ms": 40, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "비싸요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "비싸요", "start_ms": 70, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "싸요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "싸요", "start_ms": 40, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "커요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 75,
"audio_type": "speech",
"words": [
{"word": "커요", "start_ms": 75, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "작아요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "작아요", "start_ms": 50, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "무거워요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "무거워요", "start_ms": 80, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "가벼워요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "가벼워요", "start_ms": 30, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "뜨거워요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "뜨거워요", "start_ms": 65, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "차거워요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "차거워요", "start_ms": 40, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "빠르다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "빠르다", "start_ms": 55, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "느리다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "느리다", "start_ms": 40, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "멀다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "멀다", "start_ms": 65, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "가깝다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "가깝다", "start_ms": 45, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "쉽다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "쉽다", "start_ms": 70, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "어렵다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "어렵다", "start_ms": 55, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "깨끗하다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "깨끗하다", "start_ms": 80, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "더럽다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "더럽다", "start_ms": 35, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "조용하다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "조용하다", "start_ms": 65, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "시끄럽다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "시끄럽다", "start_ms": 45, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "밝다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "밝다", "start_ms": 55, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "어둡다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "어둡다", "start_ms": 40, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "강하다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "강하다", "start_ms": 70, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "약하다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "약하다", "start_ms": 45, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "십",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 75,
"audio_type": "speech",
"words": [
{"word": "십", "start_ms": 75, "dur_ms": 300, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 09:33:16*
