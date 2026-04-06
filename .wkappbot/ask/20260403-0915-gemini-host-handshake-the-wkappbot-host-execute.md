# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=09:14:19  uptime=28504s  mem=1859MB  system_processes=444"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_091409.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 571-560-305-301-320-230-132-312-321-230-321-301-326-206-302-302-321-130-162-163-136-306-320-632-206-236-302 203-102-126-103-230-203-210-203-206-230 132 230-230-203-203-132-123-1_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 672-752-671-361-765-675-165-176-306-103-103-130-106-132-260-362-312-126-326-162-326-162-316-312-513-216-321-310-123-263-126-231-213-236-267-263-263-267-617-316-672-160-310-312-1_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 136-301-136-150-571-261-326-567-651-651-657-613-321-362-231-572-576-567-576-657-106-163-601-306-567-567-561-613-623-362-623-603-612-613-670-675-162-605-620-620-620-521-570-576-6_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 675-302-320-603-102-201-623-620-312-136-132-612-610-130-615-106-167-671-162-103-163-620-106-610-312-312-316-162-136-613-731-621-372-136-103-136-103-231-163-263-621-123-231 132 7_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 671-671-671-762-765-672-627-672-765-672-756-375-367-561-571-756-137-501-103-106-102-132-106-106-103-132-576-675-765-675-132-163-754-750-756-750-705-675-765-675-675-750-756-670-6_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 234-231-132-123-136-137-136-312-132-132-134-132-136-163-130-675-165-103-316-163-167-163-130-103-631-103-156-136-231-167-126-617-263-175-517-657-627-675-605-675-657-167-165-106-6_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 672-267-625-627-362-632-621-623-632-302-104-102 103-576-756-765-736-726-672-761-672-675-672-672-670-675-675-765-612-673-165-652-625-652-762-637-267-627-362_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 261-136-362-126-123-126-103-612-206-102-316-163-136-762-136-132-347-312-736-132-103 267-602-127-675-657-675-756-637-670-602-602-167-261-263-263-206 176-610-106-160-601-610-162-1_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 267-162-132-312-273-762-762-672-126-123-316-162-632-163 321-325-326-235-261-213-236-213-216-231-263-261-267-276-237-267-206-206-206-210-201-621-206-231-102-176-162-261-326-326-2_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 132-312-621-263-627-260-261-762-627-301-603-637-673-637-671-673-632-627-671-627-675-675-673-675-673-672-675-672-637-632-627-750-672-617-765 263-216-267-326-123-312-612-267-137-6_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 102-302-312-301-302-302-263-320-206-230-362-236-302-302-531-302-326-261-260-532-230-320-302-302-162-102-102-103-102-103-206-260-206-203-547-574-574-574-574-574-574-574-574-574-3_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 537-567-537-563-576-516-563-563-563-563-563-516-536-651-516-561-563-567-563-576-567-576-567-657-156-576-571-561-567-561-657-653-657-615-567-567-567-536-657-657-567-516-576-567-5_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 754-631-162-621-261-167-106-651-715-106-175-105-157-570-517-157-715-751-572-572-571-571-567-675-613-612-320-617-365-615-672-126-231-512-576-276-762-762-361-671-627-613-136-316-5_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 216-627-216-217-276-627-167-162-136-312-237-127-275-627-127-217-123-126-176-176-672-712-716-172-127-657-675-263-627-162-671-273-172-751-652-672-652-617-672-162-126-172-761-167-6_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 137-231-132-672-627-267-276-267-276-627 167-127-267-617-271-162-120-136-321-132-326-123-641-312-756-705-736-376-162-136-134-317-231-765-627-260-572-574-574-572-326-321-302-675-3_V.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 132-107-316-107-103-126-601-620-163-106-106-136-176-103-130-162-132-136-137-106-137-716-136-176-176-132 301-137-301-312-321-312-132-216-126-176-576-576-651-357-137-165-137-163-1_V.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 132-312-160-217-153-162-163-570-675-670-572-573 621_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 137-671-576-756-657-650-576-156-167-601-675-165-361-301-315 765-670-657-756-617-620-203-236-306-260-261-620-675-670-765-706-236-206-201-263-236-260-206-620-627-267-203-162-672-3_V.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 263-130-103-162-132-670-670-160-670-106-136-106-167-601-650-672-670-670-670-627-670-627-620-267-260-162-607-672-617-671-612-621-617-163-167-162-167-160-167-167-167-147-107-107-1_V.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 302-231-103-601-106-160-321-670-137-613-612-312-163-136-216-175-137-513-601-621-136-132-360-675-156-316-312-163-312 163-103-130-107-517-213-731-236-136-523-506-675-136-167-103-1_V.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 321-302-103-312-320-203-320-302-236-231-316-123-316-302-706-756-307-574-756-675-267-312-132-143 765-675-760-176-165-165-136-316-756-756-765-726-267-267-267-623-263-623-605-615-6_V.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 206-613-130-715-756-756-652-207-127-601-260-213-231-261-623-231 321-627-263-261-621-627-613-621-613-216-612-632-620-623-132-301-631-160-617-631-675-675-765-136-123 675-670-765-3_V.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 135-136-301-132-203-230-206-203-231-213-231-230-761-763-316-317-367-312-576-312-213-231-126-312-716-576-762-576-625-607-603-756-765-607-762-123-132-123-312-132-106-301-301-136-6_W.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 267-672-136-532-327-362-623-361-623-632-621-675-675-675-675-675-675-675-657-675-657-675-675-625-657-672-627-627-673-631-167-637-675-675-675-672-627-627-675-675-675-675-675-672-6_W.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 750-576-750-312-305-573-351-123-132-371-123-106-130-751-751-163-132-106-705-176-236-362-362-673-637-610-167-163-173-312-327-632-632-362-327-123-173-172-123-123-127-756-675-256-2_W.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 310-315-510-265-267-236-213-263-162-260-263-504-567-657-260-312-237-267-570-567-756-637-627-627-627-627-621-361-263-326-216-126-547-574-574-627-627-215-674-674-106-260-236-231-1_W.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 635-567-765-657-653-152-316-163-562-561-657-657-156-671-761-162-631-623-613-621-612-612-657-567-165-167-165-613-612-631-361-635-126-123-621-312-627-763-576-576-107-132-623-625-6_W.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 765-761-731-756 751-765-167-320-132-213-123-213-120-120-120-132-123-613-132-123-126-263-162-126-162-763-756-567-167-167-176-753-576-576-567-756-752-576-567-576-527-752-726-167-6_W.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 567-657-156-512-615-136-163-163-635-562-652-560-653-652-560-652-652-625-652-672-675-657-675-657-675-652-627-625-657-560-605-562-650-650-365-361-653-356-653-215-315-612-562-652-5_V.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 127-132-312-123-102-123-163-132-132-312-362-203-106-136-103-216-126-126-102_V.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 263-216-567-523-253-675-265-725-657-562-756-567-576-516-576-576-627-267-132 675-326-326-362-263-261-263-261-236-265-762-263-163-621-613-263-756-756_W.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 567-356-351-310-237-362-351-536-567-567-576-573-325-321-631-263-316-316-365-316-316-635-356-635-361-675-251-572-576-572-527-567-316-312-312-356-361-361-531-361-275-362-537-537-3_W.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 265-653-215-265 574-754-572-735-217-675-576-765-756-574-765-710-756-175-173-173-731-376-670-657-756-765-570-571-753-750-765-271-217-170-170-317-315-751-750-750-735-751-754-715-3_W.mp3)
  Segment 34: 52008ms ~ 57768ms (file: 213-123-312-130-136-510-507-103-132-312-132-341-213-634-126-126-162-162-162-615-612-260-675-162-167-617-617-675-106-670-125-102-765-675-675-671-670-670-765-765-627-657-576-657-5_V.mp3)
  Segment 35: 58768ms ~ 59344ms (file: 216-326-312-675-617-167-213-132 321 126-632 542-576-617-261-631-123-310-612-106-310_W.mp3)
  Segment 36: 60344ms ~ 60920ms (file: 106-671-106-106-316-312-130-671-137-612-167-610-613-617-631-362-632-671-132-326-103-162-120-123-160-106-103-136-106-216-163-103-631-260-603-761-567-607-160-103-126-625-607-130-1_V.mp3)
  Segment 37: 61920ms ~ 62496ms (file: 367-164-362-634-163-135-162 261-613-123-621-312-123-106-261-621 351-672-627-627-621-371-741-713-312-571-241-612-264-236-624-265-137-574-162-312-764-347-315-371-132-175 237-213-6_V.mp3)
  Segment 38: 63496ms ~ 64072ms (file: 162-162-163-132-130-163-167-610-610-105-123-106-216-106-756-627-320-362-263-310-167_V.mp3)
  Segment 39: 65072ms ~ 65648ms (file: 263-576-567-567-567-567-567-567-163-136-572-165-316-635-163-631-163-136-316-103-165-613-316-136-160-176-162-167-176-106-326-217-162-326-602-106-316-132-163-165-136-103-103-103-1_W.mp3)
  Segment 40: 66648ms ~ 67224ms (file: 573-765-357-762-325-763-760-157-135-153-157-157-157-132-162-152-152-125-162-736-371-176-607-270-752-571-260-125-157-207-231-210-102-120-123-103-132-102-520-257-205-265-675-625-1_V.mp3)
  Segment 41: 68224ms ~ 68800ms (file: 621-627-627-627-632-627-602-627-632-132 761-673-675-675-675-132-621-160-612-601-163-206-263-261-260-276-265 671-762_W.mp3)
  Segment 42: 69800ms ~ 70376ms (file: 567-132-103-751-167-136-106-176-567-256-135-752-756-765-627-612-236-671-165-167-136-156-103-106-160-130-106-123-162-260-623-126-163-102-106-651-650-103-564-561-615-617-160-103-1_V.mp3)
  Segment 43: 71376ms ~ 71952ms (file: 136-167-126-327-621-167-156-651-567-576-756-750-756-675-103-765-675-574-576-675-657-657-760-706-627-216-302-376-312-261-632-612-321-756-765-760-765-670-602-631-623 625-265-265-1_W.mp3)
  Segment 44: 72952ms ~ 73528ms (file: 123-671 136-132-132-267-260-206-216 103-136-103-132-310-103-132-106-671-620-267-620-627-260-672-357-136-130-136-362-310 130-137-130-105-150-163-576-150-173-103-163-163-236-261 1_V.mp3)
  Segment 45: 74528ms ~ 75104ms (file: 657-675-657-160-312-123-136-163-615_V.mp3)
  Segment 46: 76104ms ~ 79920ms (file: 736-762-735-756-756-765-170-675-675-756-176-301-567-637-132-627-725-135-170-572-527-527-275-160-163-125-132-514-216-126 312-162-126_W.mp3)
  Segment 47: 80920ms ~ 81496ms (file: 137-123-312-132-571-137-237-312-302-321-327-572-735-123-503-275-327-753-270-627-301-132-267-203-312-173-173-132-231-750-754-752-752-301-362-362-163-162-125-125-132-132-132-512-2_V.mp3)
  Segment 48: 82496ms ~ 83072ms (file: 362-201-136-132-317-137-312-106-103-137-132 163-637-627-632-627_V.mp3)
  Segment 49: 84072ms ~ 84648ms (file: 136-165-105-501-156-132-103-103-103-132-126-312-576-675-650-602-632-623-132-321-736-327-574-573-376-627-574-573-576-573-317-126-136-132-136-320-675-672-761-136-675-601-167-701-5_W.mp3)
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
> **Time**: 2026-04-03 09:15:11
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "포크와",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "포크와", "start_ms": 40, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "양보가",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "양보가", "start_ms": 50, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "필요합니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "필요합니다", "start_ms": 30, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "어디서",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "어디서", "start_ms": 60, "dur_ms": 460, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "내려요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "내려요", "start_ms": 45, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "이거",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "이거", "start_ms": 55, "dur_ms": 420, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "얼마예요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "얼마예요", "start_ms": 35, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "영수증",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "영수증", "start_ms": 65, "dur_ms": 490, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "주세요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "주세요", "start_ms": 40, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "화장실",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "화장실", "start_ms": 70, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "어디예요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "어디예요", "start_ms": 50, "dur_ms": 490, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "도와주세요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "도와주세요", "start_ms": 80, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "조심하세요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "조심하세요", "start_ms": 30, "dur_ms": 530, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "길을",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "길을", "start_ms": 60, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "잃었어요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "잃었어요", "start_ms": 40, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "배고파요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "배고파요", "start_ms": 55, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "목말라요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "목말라요", "start_ms": 35, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "졸려요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "졸려요", "start_ms": 65, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "피곤해요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "피곤해요", "start_ms": 40, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "아파요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "아파요", "start_ms": 70, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "괜찮아요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "괜찮아요", "start_ms": 50, "dur_ms": 490, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "알겠어요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "알겠어요", "start_ms": 80, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "모르겠어요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "모르겠어요", "start_ms": 30, "dur_ms": 530, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "맞아요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "맞아요", "start_ms": 60, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "아니요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "아니요", "start_ms": 40, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "좋아요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "좋아요", "start_ms": 55, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "싫어요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "싫어요", "start_ms": 35, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "기뻐요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "기뻐요", "start_ms": 65, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "슬퍼요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "슬퍼요", "start_ms": 40, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "화나요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "화나요", "start_ms": 70, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "심심해요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "심심해요", "start_ms": 50, "dur_ms": 490, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "재밌어요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "재밌어요", "start_ms": 80, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "맛있어요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "맛있어요", "start_ms": 30, "dur_ms": 530, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "맛없어요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "맛없어요", "start_ms": 60, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "비싸요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "비싸요", "start_ms": 40, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "싸요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "싸요", "start_ms": 55, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "커요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "커요", "start_ms": 35, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "작아요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "작아요", "start_ms": 65, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "무거워요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "무거워요", "start_ms": 40, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "가벼워요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "가벼워요", "start_ms": 70, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "뜨거워요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "뜨거워요", "start_ms": 50, "dur_ms": 490, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "차거워요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "차거워요", "start_ms": 80, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "빠르다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "빠르다", "start_ms": 30, "dur_ms": 530, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "느리다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "느리다", "start_ms": 60, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "멀다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "멀다", "start_ms": 40, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "가깝다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "가깝다", "start_ms": 55, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "쉽다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "쉽다", "start_ms": 35, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "어렵다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "어렵다", "start_ms": 65, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "깨끗하다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "깨끗하다", "start_ms": 40, "dur_ms": 510, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 09:15:11*
