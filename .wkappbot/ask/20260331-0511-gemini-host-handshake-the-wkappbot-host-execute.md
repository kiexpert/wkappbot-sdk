# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43304  time=05:10:16  uptime=10004s  mem=1667MB  system_processes=440"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260331_050921.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 312-136-162-203-320-203-132-206-230-253-270-206-326-230-236-327-765-206-230-236-102-132-302-230-321-236-236-132-103-312-103-143-275-162-312-136-362-263-260-206-320-203-103-132 3_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 123-103-132-705 167-163-103-310-206-320-673-321-260-236-576-570-507-765-763-756-136-615-765-602-602-612-610-650-160-617-106-617-657-671-617-312-731-765-763-105-176-137-103-106-1_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 263-631-623-362-162-621-236-263-263-632-126-216-132-132-162-372-762-672-671-763-762-763-726-162-361-621-160-612-261-123-162-672-163 267-261-267-675-675-671-671-671-673-162-361-3_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 516-127-621-136 127-231-136 635-635-617-236-267-263-362-263 765-765-765_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 137-267-172-637-236-502-613-312-302-132-162-103-103-103-132-135-130-136-107-260-260-206-617-617-167-167-103-160-621-132-230-236-230-413-132-103-162-231-130-102-230-231-102-230-2_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 672-167-160-103-321-312-321-321-312-317-135-531-534-534-351-315-351-312-123-213-152-527-537-512-537-517-715-716-213-523-512-523-512-521-123-213-312-237-273-237-230-230-217-237-2_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 302-301-306-230-206-260-620-576-526-706-706-702-762-237-207-263-203-276-176-106-167-162-267-263-276-267-725-206-236-206-756-675-720-723-376-312-231-302-321-321-132-103-132-132-1_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 627-623 675-657-657-657-627-657-657-632-213-623 312-136-136-103-132-136-136-601-136-175-136-627-612-120-103-231-136-102-612-162-361-136-316-361-103-316-136-136-136-103-125-136-5_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 130-136-103-130-106-163-136-103-621_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 357-253-351-153-251-517-136-132-312-315-351-135-215-231-165-367-365-136 352-327-327-375 317-371-375-371-153-157-317-574-315-573-573-574-754-754-357-751-754-574-571-157 635-213 6_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 231-120-126-103-136-651-205-103-126-637-162-607-136-106-361-165-627-162-136-126-106-103-163-106-163-136-105-163-163-136-176-163-162-632-132-261-651-162-136-167-316-671-612-163-1_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 162-326-671-765-621-256-562-571-635-362-632-625-627-632-261-321-362-162-136-123-762-612-756-576-756-756-726-263-607-671-627-617-627-617-612-613-136-260-162 267-263 162-126-765-7_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 524-356-516-157-351-135-531-351-153-236-361-357-521-527-523-167-137-671-617-513-537-540-532-356-632-326-625-321-632-263-650-652-625-652-625-235 572-531-154-574-540-516-153-754-1_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 356-625-526-527-572-526-537-547-527-256-256-265-265-267-276-217-256-231-237-627-367-362-321-726-657-675-617-672-617-631-316-637-531-526-256-265-175-261-627-260-135-315-375-376-3_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 732 127-126 275-276-213-123-123-723-273-276-127-132 321-236-321 261_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 631-103-756-167-167-103-675-123-652-761-176-176-617-163-106-167-106-567-263-126-564-620-162-106-103-216-623-165-601-617-672-103-610-165-106-163-165-136-163-136-126-671-617-167-7_V.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 132-160-216-617-162-126-160-127-132-312-126-761-360-603-716-601-261-231-162-165-167-152-167-326-326-361-571-576-675-625-576-567-576-132-163-126-231-167-672-673-162-126-126-136-1_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 160-610-670-627-672-567-567-265-261-672-267-263-261-657-136-135-601-163-167-176-167-317-173-143-134-132-103-314-132-627-675-672-162-167-126-157-154-670-652-627-260-260-612-326-6_V.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 137-173-175-156-175-126-126-162-136-162-126-137-135-567-567-623-376-762-316-357-315-367-375-375-237-326-376-362-756-754-621-312-351-315-375-316-317-361-136-317-367-637-376-315-3_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 567-576-567-576-657-567-576-567-576-567-756-657-576-576-657-567-567-567-657-657-567-657-657-567-765-675-657-765-675-567-576-657-657-132-312-132-312-735-321-231-123-132-576-653-5_W.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 267-672-726-723-273-732-237-273-273-273-237-327-237-237-327-237-123-327-327-321-321-253-523-523-253-321-327-327-327-237-237-267-326-237-267-236-326-326-321-236-236-326-263-326-2_W.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 716-765-765-765-765-675 672-675-671-651-657-637-637-316-631-671-316-627-326-367-163-612-617-163-316-163-657_W.mp3)
  Segment 23: 34672ms ~ 36976ms (file: 316 163-612-162-132-261 627-170-671-671-174-213-132-751-710-716-756-761-671-167-564-163-136-316-671 165-136-163-731-571-605-106-167-107-765-267-637-106-172-106-210-162-132-160-7_W.mp3)
  Segment 24: 37976ms ~ 38552ms (file: 326 265-123-123-312-216-103-136-306-163-160-163-152-321-306-362-126-302-103-302-321-326-213-312-132-231-236-213-231-502-523-315-503-162-321-136-132-164-132-162-106-716-167-213-2_V.mp3)
  Segment 25: 39552ms ~ 40128ms (file: 637-176-136-173-163-657-137-173-163-671-763-137-761-317-716-173-675-176-137-137-136-167-167-136-316-156-167-316-671-136-761-671-627-267-625-623-267-627-267-672-672-267-672-627-6_W.mp3)
  Segment 26: 41128ms ~ 41704ms (file: 163-610-613-136-162-167-103-103-103-137-106-715-167-163-126-132-127-136-167-617-154-165-102-162-103-574-132-625-562-160-106-132-675-617-602-627-672-163-167-103-710-361-137-167-1_V.mp3)
  Segment 27: 42704ms ~ 43280ms (file: 627-267-263-267-267-267-672-326-276-267-627-762-762-763-726-267-267-627-263-152-132-132-123-623-267-627-267-627-267-627-267-267-267-765-675-267-267-265-267-260-627-267-627-627-6_W.mp3)
  Segment 28: 44280ms ~ 44856ms (file: 560-546-547-621-261-206-263-320-302-103-301-312-301-302-231-651-241-165-163-103-136-173-576-570-316-167-107-756-750-103-163-106-106-160-106-167-106-106-167-136-312-136-601-106-1_V.mp3)
  Segment 29: 45856ms ~ 46432ms (file: 160-136-612-162-167-675-612-653-102-562-136-167-106-620-162-162-623-130-136-361-167-721-216-612-126-126_V.mp3)
  Segment 30: 47432ms ~ 48008ms (file: 632-257-263-320-236-236-326-321-231-263-321-621-617-326-162-260-206-206-132-326-362-362-326-362-362-362-306-310-326-102-103-263-320-327-263-326-321-326-623-623-265-236-602-260-2_V.mp3)
  Segment 31: 49008ms ~ 49584ms (file: 761-160-163-162-267-162-765-765-175-137-103-130-765-765-325-310-103-310-302-312-156-301-103-120-126-156-260-625-160-130-103-162-621-162-361-136-310-750-765-176-301-301-130-651-6_V.mp3)
  Segment 32: 50584ms ~ 51160ms (file: 754-632-672-675-675-625-625-607-672-617-267-721-631-126-316-132-132-627-326-261-672-265-623-621-260-627-260-206-201-263-321-261-126-150-210-163-612-621-126-136-261-312-627-230-3_W.mp3)
  Segment 33: 52160ms ~ 52736ms (file: 312-132-321-312-136-612-136-765-162-706-710-132-715-617-716-103-615-567-617-623-173-516-137-620-735-675-621-176-103-365-135-657-576-567-567-576-576-136-361-126-130-126-103-126-1_V.mp3)
  Segment 34: 53736ms ~ 54312ms (file: 103-620-623-623-263-625-625-621-216-261-362-316-361-376-236-326-301-307-307 516-563-512-506-521-150-501-516-150-275-275-275-275-257-251-257-256-256-265-561-123-612-621-625-162-6_W.mp3)
  Segment 35: 55312ms ~ 55888ms (file: 230-376-103-162-623-162-157-130-136-657-167-675 236-362-612-130-103-316-126-132-216-163-671-130-617-601-103-130-126-216-263-123-623-236-261-263-265-256-165-132-163-132-612 326-3_V.mp3)
  Segment 36: 56888ms ~ 57464ms (file: 567-576-576-657-576-576-756-201-120-302-203-102-321-132-576-576-762-576-657-576-756-657-576-576-657-567-756-576-657-567-657-765-675-765-576-561-576-657-576-756-657-657-657-567-5_W.mp3)
  Segment 37: 58464ms ~ 59040ms (file: 275-326-327-237-236-527-327-321-132-132-216-267-562-546-524-167-376-136-132-617-263-216-261-276-267-261-621-260-261-213-126-312-361-273-673-632-362-637-315-315-315-362-356-534-3_W.mp3)
  Segment 38: 60040ms ~ 60616ms (file: 513-130-132-163-132-106-726-163-316-126-761-651-715-103-103-612-162-132-132 317-103-167-136-317-261-103-136-163-136-607-613-651-103-162-601-160-102-103-103-106-162-167-103-310-1_V.mp3)
  Segment 39: 61616ms ~ 62192ms (file: 137-317-103-132-136-167-163-516-321-167-123-162-126-316-137-163 326-612-603-136-123-176-173-106-165-162-160-162-164-106-617-106-163-167-103-163-130-136-316-312-521-361-216-627-6_V.mp3)
  Segment 40: 63192ms ~ 63768ms (file: 213-261-236-652-301-617-752-103-132-172-102-136-132-503-163-102-317-316-103-132-130-136-132-136-617-561-362-126-765-576-765-576-731-130-167-106-671-163-761-716-136-165-103-712-1_V.mp3)
  Segment 41: 64768ms ~ 65344ms (file: 567-567-765-765-756-576-576-576-576-571-756-576-571-576-657-765-576-657-567-657-567-567-576-567-576-561-517-517-751-576-751-756-576-516-527-567-567-517-571-751-156-576-567-576-5_W.mp3)
  Segment 42: 66344ms ~ 66920ms (file: 136-653-657-653-651-610-623-362-623-613-632-601-231-521-257-256-267-257-675-576-675-327-326-657-653-167-716-276-267-267-267-726-317-725-372-162-615-615-165-537-321-613-673-267-3_W.mp3)
  Segment 43: 67920ms ~ 68496ms (file: 615-651-176 162-105-167-163-106-164-162-625-176-256-136-160 126-132-162-160-167-106-163-576-163-160-126-106-106-617-562-571-625-162-106-160-625-561-106-165-102-103-106-102-675-3_V.mp3)
  Segment 44: 69496ms ~ 70072ms (file: 103-610-167-160-617-130-136-651-132-617-163-106-765-173-156-236-167-163-163-165-617-167-173-613-130-623-163-613-126-136-762-721-617-103-267-602-103-160-136-136-103-103-132-312-6_V.mp3)
  Segment 45: 71072ms ~ 71648ms (file: 267-315-315-357-352-351-153-152-532-263-276-627-672-627-672-672-627-527-657-675-765-756-276-327-372-231-172-753-715-375-315-263-267-623-673-670-675-365-326-574-576-574-261-527-5_W.mp3)
  Segment 46: 72648ms ~ 73224ms (file: 176-126-263-236-236-231-235-235-235-321-136-312-721-312-351-357-762-137-276-237-231-267-210-273-534-152-132-631-613-136-312-123-256-325-236-532-152-216-123-123-132-136-132-527-5_W.mp3)
  Segment 47: 74224ms ~ 74800ms (file: 135-132-152-315-153-351-321-132-135-352-315-157-152-135-127-123-127-172-157-157-152-315-315-137-732-312-317-132-271-123-217-132-217-132-172-127-371-317-173-137-573-527-572-517-3_W.mp3)
  Segment 48: 75800ms ~ 76376ms (file: 617-127-617-107-617-623-326-621-162-137-106-613-167-103-765-517-165-123-675-576-675_V.mp3)
  Segment 49: 77376ms ~ 77952ms (file: 675-675-163-160-132-136-316-167-571-567-526-162-157-106-103-317-321-163-130-617-167-261-162-301-316-165-310-310-130-105-135-106-167-630-627-106-165-516-531-137-136-631-321-132-1_W.mp3)
  Segment 50: 78952ms ~ 82768ms (file: 162-761-136-107 107-213-132-127-172-172-172-127-107-132-126-103-103-263-213-621-231-732-302-376 517-154-671-167-764-167-617-627-627-627-621-361-316-316-316-631-163-635-135-134-1_V.mp3)
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
> **Time**: 2026-03-31 05:11:13
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
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 45, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "만나서 반갑습니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "만나서", "start_ms": 60, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 290, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "이름이 무엇입니까?",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "이름이", "start_ms": 30, "dur_ms": 210, "phase_lr_deg": 5.2},
{"word": "무엇입니까?", "start_ms": 250, "dur_ms": 310, "phase_lr_deg": 4.8}
]
},
{
"segment": 4,
"transcript": "제 이름은 김철수입니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "제", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "이름은", "start_ms": 140, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "김철수입니다", "start_ms": 310, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "Where are you from?",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Where", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": -3.1},
{"word": "are", "start_ms": 200, "dur_ms": 80, "phase_lr_deg": -2.8},
{"word": "you", "start_ms": 290, "dur_ms": 100, "phase_lr_deg": -3.0},
{"word": "from?", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": -3.2}
]
},
{
"segment": 6,
"transcript": "저는 미국에서 왔습니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 55, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "미국에서", "start_ms": 185, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "왔습니다", "start_ms": 405, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "한국어를 공부하고 있어요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "한국어를", "start_ms": 20, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "공부하고", "start_ms": 260, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "Is it difficult to learn?",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "Is", "start_ms": 35, "dur_ms": 60, "phase_lr_deg": -2.5},
{"word": "it", "start_ms": 105, "dur_ms": 60, "phase_lr_deg": -2.5},
{"word": "difficult", "start_ms": 175, "dur_ms": 210, "phase_lr_deg": -2.7},
{"word": "to", "start_ms": 395, "dur_ms": 60, "phase_lr_deg": -2.6},
{"word": "learn?", "start_ms": 465, "dur_ms": 100, "phase_lr_deg": -2.8}
]
},
{
"segment": 9,
"transcript": "조금 어려워요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "조금", "start_ms": 80, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "어려워요", "start_ms": 270, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 11,
"transcript": "하지만 재미있습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "하지만", "start_ms": 40, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "재미있습니다", "start_ms": 260, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "어떤 음식을 좋아하세요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "어떤", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 4.5},
{"word": "음식을", "start_ms": 190, "dur_ms": 160, "phase_lr_deg": 4.7},
{"word": "좋아하세요?", "start_ms": 360, "dur_ms": 210, "phase_lr_deg": 4.6}
]
},
{
"segment": 13,
"transcript": "비빔밥을 좋아합니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "비빔밥을", "start_ms": 50, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "좋아합니다", "start_ms": 340, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "매운 음식을 잘 드시네요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "매운", "start_ms": 25, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "음식을", "start_ms": 155, "dur_ms": 140, "phase_lr_deg": 5.1},
{"word": "잘", "start_ms": 305, "dur_ms": 80, "phase_lr_deg": 4.9},
{"word": "드시네요", "start_ms": 395, "dur_ms": 170, "phase_lr_deg": 5.0}
]
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
"transcript": "네, 아주 좋아해요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 60, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 170, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "좋아해요", "start_ms": 310, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "가장 좋아하는 가수는 누구예요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "가장", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 4.2},
{"word": "좋아하는", "start_ms": 170, "dur_ms": 180, "phase_lr_deg": 4.3},
{"word": "가수는", "start_ms": 360, "dur_ms": 120, "phase_lr_deg": 4.1},
{"word": "누구예요?", "start_ms": 490, "dur_ms": 150, "phase_lr_deg": 4.4}
]
},
{
"segment": 18,
"transcript": "저는 BTS를 좋아합니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 55, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "BTS를", "start_ms": 175, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "좋아합니다", "start_ms": 415, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "Do you like their music?",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Do", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": -3.5},
{"word": "you", "start_ms": 120, "dur_ms": 80, "phase_lr_deg": -3.4},
{"word": "like", "start_ms": 210, "dur_ms": 120, "phase_lr_deg": -3.6},
{"word": "their", "start_ms": 340, "dur_ms": 90, "phase_lr_deg": -3.5},
{"word": "music?", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": -3.7}
]
},
{
"segment": 20,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 21,
"transcript": "가사가 정말 아름다워요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "가사가", "start_ms": 45, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 235, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "아름다워요", "start_ms": 385, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "노래방에 가고 싶네요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "노래방에", "start_ms": 20, "dur_ms": 230, "phase_lr_deg": 4.8},
{"word": "가고", "start_ms": 260, "dur_ms": 120, "phase_lr_deg": 4.9},
{"word": "싶네요", "start_ms": 390, "dur_ms": 150, "phase_lr_deg": 4.7}
]
},
{
"segment": 23,
"transcript": "다음에 같이 갈까요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 150, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 420, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "갈까요?", "start_ms": 650, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "좋은 생각이에요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 5.1},
{"word": "생각이에요", "start_ms": 240, "dur_ms": 280, "phase_lr_deg": 5.3}
]
},
{
"segment": 25,
"transcript": "언제 시간이 괜찮으세요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "언제", "start_ms": 35, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "시간이", "start_ms": 175, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "괜찮으세요?", "start_ms": 335, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "이번 주말은 어때요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "이번", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 4.6},
{"word": "주말은", "start_ms": 190, "dur_ms": 170, "phase_lr_deg": 4.5},
{"word": "어때요?", "start_ms": 370, "dur_ms": 160, "phase_lr_deg": 4.7}
]
},
{
"segment": 27,
"transcript": "토요일 오후가 좋습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "토요일", "start_ms": 60, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "오후가", "start_ms": 250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "좋습니다", "start_ms": 410, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "그럼 그때 만나요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 30, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "그때", "start_ms": 160, "dur_ms": 140, "phase_lr_deg": 5.1},
{"word": "만나요", "start_ms": 310, "dur_ms": 210, "phase_lr_deg": 4.9}
]
},
{
"segment": 29,
"transcript": "알겠습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "알겠습니다", "start_ms": 70, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "See you soon!",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 45, "dur_ms": 120, "phase_lr_deg": -3.8},
{"word": "you", "start_ms": 175, "dur_ms": 100, "phase_lr_deg": -3.7},
{"word": "soon!", "start_ms": 285, "dur_ms": 200, "phase_lr_deg": -3.9}
]
},
{
"segment": 31,
"transcript": "안녕히 계세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 40, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "계세요", "start_ms": 280, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "오늘 날씨가 참 맑네요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": 4.1},
{"word": "날씨가", "start_ms": 165, "dur_ms": 150, "phase_lr_deg": 4.2},
{"word": "참", "start_ms": 325, "dur_ms": 60, "phase_lr_deg": 4.0},
{"word": "맑네요", "start_ms": 395, "dur_ms": 130, "phase_lr_deg": 4.2}
]
},
{
"segment": 33,
"transcript": "네, 산책하기 좋은 날씨예요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 50, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "산책하기", "start_ms": 150, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 380, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "날씨예요", "start_ms": 480, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "Would you like to walk together?",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Would", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": -3.2},
{"word": "you", "start_ms": 120, "dur_ms": 80, "phase_lr_deg": -3.1},
{"word": "like", "start_ms": 210, "dur_ms": 110, "phase_lr_deg": -3.3},
{"word": "to", "start_ms": 330, "dur_ms": 60, "phase_lr_deg": -3.2},
{"word": "walk", "start_ms": 400, "dur_ms": 90, "phase_lr_deg": -3.4},
{"word": "together?", "start_ms": 500, "dur_ms": 60, "phase_lr_deg": -3.5}
]
},
{
"segment": 35,
"transcript": "정말 좋아요, 갑시다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 65, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "좋아요,", "start_ms": 215, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "갑시다", "start_ms": 405, "dur_ms": 160, "phase_lr_deg": 0.0}
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
"transcript": "공원에 꽃이 많이 폈어요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "공원에", "start_ms": 45, "dur_ms": 180, "phase_lr_deg": 4.4},
{"word": "꽃이", "start_ms": 235, "dur_ms": 110, "phase_lr_deg": 4.5},
{"word": "많이", "start_ms": 355, "dur_ms": 120, "phase_lr_deg": 4.3},
{"word": "폈어요", "start_ms": 485, "dur_ms": 80, "phase_lr_deg": 4.5}
]
},
{
"segment": 38,
"transcript": "우와, 정말 예쁘네요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "우와,", "start_ms": 55, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 185, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "예쁘네요", "start_ms": 335, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "사진을 찍어 드릴까요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "사진을", "start_ms": 30, "dur_ms": 160, "phase_lr_deg": 4.9},
{"word": "찍어", "start_ms": 200, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "드릴까요?", "start_ms": 320, "dur_ms": 230, "phase_lr_deg": 4.8}
]
},
{
"segment": 40,
"transcript": "네, 부탁드려요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 60, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "부탁드려요", "start_ms": 170, "dur_ms": 380, "phase_lr_deg": 0.0}
]
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
"transcript": "치즈~ 웃어보세요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "치즈~", "start_ms": 40, "dur_ms": 250, "phase_lr_deg": 5.2},
{"word": "웃어보세요", "start_ms": 300, "dur_ms": 260, "phase_lr_deg": 5.1}
]
},
{
"segment": 43,
"transcript": "잘 나왔나요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "잘", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "나왔나요?", "start_ms": 180, "dur_ms": 370, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "완벽해요! 아주 멋져요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "완벽해요!", "start_ms": 20, "dur_ms": 260, "phase_lr_deg": 4.7},
{"word": "아주", "start_ms": 290, "dur_ms": 120, "phase_lr_deg": 4.8},
{"word": "멋져요", "start_ms": 420, "dur_ms": 140, "phase_lr_deg": 4.6}
]
},
{
"segment": 45,
"transcript": "고맙습니다, 이제 커피 마실까요?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "고맙습니다,", "start_ms": 45, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 305, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "커피", "start_ms": 405, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "마실까요?", "start_ms": 495, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "That sounds great.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "That", "start_ms": 35, "dur_ms": 100, "phase_lr_deg": -2.9},
{"word": "sounds", "start_ms": 145, "dur_ms": 180, "phase_lr_deg": -3.0},
{"word": "great.", "start_ms": 335, "dur_ms": 220, "phase_lr_deg": -3.1}
]
},
{
"segment": 47,
"transcript": "저기 예쁜 카페가 있네요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "저기", "start_ms": 50, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "예쁜", "start_ms": 190, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "카페가", "start_ms": 310, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "있네요", "start_ms": 460, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "가서 시원한 아이스 아메리카노 마셔요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "가서", "start_ms": 25, "dur_ms": 90, "phase_lr_deg": 4.5},
{"word": "시원한", "start_ms": 125, "dur_ms": 140, "phase_lr_deg": 4.6},
{"word": "아이스", "start_ms": 275, "dur_ms": 120, "phase_lr_deg": 4.4},
{"word": "아메리카노", "start_ms": 405, "dur_ms": 150, "phase_lr_deg": 4.6},
{"word": "마셔요", "start_ms": 565, "dur_ms": 0, "phase_lr_deg": 4.5}
]
},
{
"segment": 49,
"transcript": "좋습니다, 제가 살게요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "좋습니다,", "start_ms": 65, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 285, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "살게요", "start_ms": 415, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "오늘 정말 즐거운 하루였어요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 80, "dur_ms": 140, "phase_lr_deg": 5.4},
{"word": "정말", "start_ms": 230, "dur_ms": 130, "phase_lr_deg": 5.5},
{"word": "즐거운", "start_ms": 370, "dur_ms": 160, "phase_lr_deg": 5.3},
{"word": "하루였어요", "start_ms": 540, "dur_ms": 220, "phase_lr_deg": 5.5}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-03-31 05:11:13*
