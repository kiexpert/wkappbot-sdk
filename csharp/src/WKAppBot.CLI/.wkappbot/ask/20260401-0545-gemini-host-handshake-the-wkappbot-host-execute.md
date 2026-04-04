# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=14524  time=05:44:25  uptime=7256s  mem=2476MB  system_processes=841"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260401_054415.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 617-605-605-612-162-162-167-106-657-625-260-263-263-263-206-620-623-675-632-602-260-267-326-263-260-362-130-301-103-302-310-236-675-675-672-632-306-621-162-361-602-263-201-261-2_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 362-623-265-236-231-326-361-316-132-153-517-321-576-675-267-632-126-132-316-326-265-256-632-326-261-231-236-263-265-265-267-256-625-276-236-316-123-120-102-261-130-130-361-326-2_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 123-263-621-136 621-613-265-671-672-632 327-723-327-275-267-267-267-612-623-625-627-261-263-621-267-627-263-623-263-675-623-263-623-263-216-236-623-623_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 623-623-623-263-632-326-612-132-326-312-231-236-263-261-321-326-623-632-631-632-623-263-263-623-326-623-263-236-623-312-326-263-236-567-263-625-216-621-321-326-126-216-267-261-2_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 361-136-162-312-126-316-316-376-106-106-310-652-130-167-163-321-132-103-617-103-156-162-136-103-136-136-102-103-106-103-103-103-103-103-103-163-136-106-103-210-216-623-310-306-3_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 132-361-162-621-632-613-621-632-613-612-612-163-312-623-620-625-601-607-106-106-106-163-126-103-106-106-163-130-103-126-106-316-705-612-120-132-206-160-601-670-675-630-623-106-6_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 576-512-705-675-602 261-617-132-312_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 162-103-321-260-203-301-302-312-230-206-260-325-320-321-326-301-312-310-301-301-132-126-321-206-620-236-203-320-320-130-312-163-302-305-302-375-307-316-135-203-206-250-302-301-3_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 206-327-320-206-302-136-576-652-613-263-263-621-312-261-236-320-657-765-675-203-203-326-206-632-132-630-106-103-613-672-617-657-162-162-617-612-613-163-163-163-573-567-576-357-7_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 615-651-651-615-651-357-567-517-157-157-153-157-156-165-135-513-153-135-137-156-165-156-157-153-157-135-315-135-357-537-756-572-657-561-657-651-561-576-567-516-536-357-325-236-5_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 120-210-261-126-263-312-163-126-267-627-623-261-620 261-263-206-267-260-217-265-265-216-267-267-675-763-176-167-162-176-671-671-675-763-167-175-675-617-671-756-176-657-617-756-1_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 617-621-261-126-162-201-162-612-136-230-236-326-267-236-352-203-127-723-231-261-620-621-620-206-312-136-132-501-570-153-137-136-703-760-137-136-261-627-236-320-321-132-261-126-2_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 365-562-265-267-267-627-563-675-675-564-675-167-762-375-351-376-356-365-361-316-316-361-536-573-372-235-327-213-321-321-231-312-312-321-231-267-621-263-362-326-263-267-236-231-3_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 321-301 671-276-672-267-627-673-675-312-123-675-756-276-237-261-603_V.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 267-627-627-267-126-362-312-573-372-731-367-673-670-754-675-672-267-672-301-321-236-236-132-316-123-541-541-521 756-675-675-765-657-567-657-675-627-516-576-576-517-751-756-175-6_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 301-167-136-136-103-130-103-263-607 621-613-674-613-317-137-136-175-715-310-130-103-106-106-316-162-602-130-136-167-137-136-310-103-503-507-576-576-576-657-576-576_V.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 675-176-137-302-301 136-216-602-671-132 362-613-167-136-126-107-321-320-103-213-236-236-236-231-206-260-260-260-206-162-167-613-106-136-103-162-567-567-652-263-265-625-263-576-5_V.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 260-230-567-620-620-260-260-102-321-132-132-153-301-321-351-507-765-102-132-123-321-162-123-320-502-613-310-312-763-107-312-260-213-302-301-102-103-260-263-360-362-310-310-302-3_W.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 123-120-312-126-120-120-120-210-102-102-120-102-102-210-102-123-102-106-102-132-120-126-102-106-163-260-326-120-617-672-672-762-612-216-675-765-576 267-762-576-756-675-765-762-7_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 316-316-136 574-574 576-576-567-571-576-574-571-176-132-632-632-621-263-316-361-136-216-613-162-736-267-621-672-627-362-320-130-321-320-321-261-106-167-163-321-301 562-631-160-3_W.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 132-613-153-231-123-123-167-126-316-302-302-361-361-267-623-632-316-301-623-312-123-756-753-765-326-136-162-312-705-673-756-760-675-761-764-261 673-675-673-167-671-176-631-630-6_V.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 267-267-263-261 163-132-136-132-136-160-573-103-132-163-130-136-160-106-106-163-706-326-231-623-236-326-310-206-321-312-302-310-301-312-213-320-312-130-132-103-316-136-162-612-5_V.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 150-150-705-276-267 765-620-620-267-570-573-325-532-230-320-306-312-301-310-302-316-163-621-361-362-630-623-306-350-320-301-302-326-107-260-260-103-213-126-136-102-135-321-231-3_V.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 216-236-261-120-261-123-316-206-263-251-326-612-625-627-576-162-102-675-126-210-206-627-731-316-103-103-601-163-103-167-136-213-213-156-167-675-602-627-627-621-617-106-126-632-6_W.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 260-652-326-260-261-312-132-103-103-130-103-360-673-672-621-612 126-163-163-162-136_W.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 365-627-351-357-537-537-351-356-715-352-315-215-351-351-537-537-315-321-312-326-356-135-326-312-321-312-162-356-312-356-756-732-275-326-357-531-315-325-237-321-572-572-362-372-3_W.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 756-675-612-672-672-627-163-632-612-621-672-675-675-671-675-615-563-672-621-627-267-726-605 231-261-321-263-231-321-326-576-765-756-236-765-362-623-620-607-602-276-237-327-203-3_W.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 165-103-103-176-761-163-512-103-612-102-103-126-163-316-613-653-652-621 163-103-310-736-310-107-765-137-176-326-315-106-306-713-136-310-103-106-160-617-132-106-617-136-107-617-1_V.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 132-103-137-615-617-162-316-263-312-301-132-315-135-103-135-126-136-123-103-103-613-603-361-103-103-134-136-136-136-267-132 103 163-163-261-261-126-130-130-130-310-130-130-310-1_V.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 106-625-162-160-136-231-162-103-163-136-167-671-106-610-617-132-103-103-165-160-361-316-160-126-751-612-620-175-302-367-176-762-103-102-154-103-617-106-103-103-157-106-162-512-1_W.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 106-731-103-615-162-162-163-136-126-306-162-132-751-627-236-576-671-573-375-531-735-361-625-327-167-236-263-123-216-210-321-120-210-106-326-126-751-627-162-160-162-731-362-576-2_W.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 261-263-216-267-362-276-260-231-236-261-206-362-260-623-725-276-750-725-526-267-267-206-260-623-632-261-260-720-712-173-137-106-162-617-132-126-163-106-123-126-103-132-261-613-1_V.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 726-620-612-657-637-672-675-617-671-672-637-637-367-617-763-673-675-673-761-675-675-672-613-361-675-675-675-672-671-672-367-671-617-671-675-136-617-136-163-621-267-613-103-106-1_W.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 716-761-761-716-167-761-537-572-756-576-765-765-765-675-765-765-675-547-573-136-261-263-236-625-623-546-547-256-275-672-672-326-263-657-765-367-632-317-236-263-231-231-261-263-2_W.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 162-135-267-216-217-726-672-236-251-621-124-163-132-316-312-132-316-361-361-316-367-213-126-312-632-326-237-316-316-361 106 132-135 132-132-136-607-607 567-567-236-312-123-103-3_V.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 612-621-672-672-672-672-127-216-261-123-123-750-175-107-105-132-362-132-632-135-310-103-130-301-671-675-126-103-301-301-103-123-120-216-132-612-601-162-106-162-103-136-103-301-1_V.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 103-130-321-172-675-612-120-157-127-562-625-627-216-321-136-105-607-706-310-765-576-150-165-175-167-361-103-125-206-165-213-627-617-162-236-657 521-542-514-103-132-167-154-106-6_V.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 675-625-526-652-652-613-651-615-617-613-136-362-631-631-631-316-613-631-315-316-165-135-153-156-156-651-253-365-351-315-315-351-315-315-321-315-354-523-351-354-523-354-531-543-5_W.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 361-361-306-103-237-260-273-621-563-615-106-536-316-321-310-136-136-307-162-312-263-321-102-106-130-630-561-650-136-316-632-123-601-136-761-167-231-103-160-160-312-103-130-160-5_W.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 623-675-672-765-625-652-310-302-321-632-316-316-765-765-675-601-763-327-326-302-302-730-756-765-765-765 274-241-341-302-312-563-716-317-132-132-706-670-765-756-754-751 623-326-5_V.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 132-126-167-136-136-123-163-610-136-165-671-371-306-106-105-107-106-170-126-102-106-106-126-321-103-263-213-102-130-106-136-162-106-326-316-136-120-103-102-260-217-206-210-201-1_V.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 316 136-126-167 123-136 716-716-173-176-167-162-176-173-162-176-176-162-162-162-126-126-126-267-261-237-237-213-537-756_W.mp3)
  Segment 43: 66192ms ~ 66768ms (file: 106-103-136-103-103-103-601-163-106-103-126-176-176-612-126-675-103-167-517-165-106-135-106-163-106-163-321-136-132-106-176-106-167-617-163-103-260-126-102-675-765-671-613-316-1_V.mp3)
  Segment 44: 67768ms ~ 68344ms (file: 672-162-106-601-106-160-607-562-162-612-167-621-671-602-475-672-672-652-675-675-653-365-265-206-627-657-576-532-752-276-130-231-162-517-165-567-572-527-570-710-176-321 175-326 1_W.mp3)
  Segment 45: 69344ms ~ 69920ms (file: 567-156-163-136-516-567 576-567-657-567-576-567-756-735-753-573-537-317-326-236-267-267-267-267-231-123-126-576-754-671-326-527-526-752-265-236-136-326-263-236-326-326-561-351-6_W.mp3)
  Segment 46: 70920ms ~ 71496ms (file: 213-231-321-613-326-263 362-236-231-326-261-263_W.mp3)
  Segment 47: 72496ms ~ 73072ms (file: 251-523-352-352-325-321-352-325-523-215-231-123-213-532-523-251-352-325-237-326-236-523-265-231-236-230-231-231-263-236-265-625-267-265-257-526-256-265-627-623-267-326-632-326-3_W.mp3)
  Segment 48: 74072ms ~ 74648ms (file: 123-162-156-162-132-132-132-213-261-206-260-206-230-302-230-312-301-236-203-123-123-231-132-302-312-320-230-236-236-265-620-260-230-321-302-312-123-216-126-125-102-102-301-320-1_V.mp3)
  Segment 49: 75648ms ~ 76224ms (file: 263-325-756 546-567-756-576-756-576-675-765-756-765-756-657-675-765 172-537-356-753-576-671-612-162-621-261-765-765-675-567-675-657-567-657-567-576-756 567-574-567-136-312-132-2_W.mp3)
  Segment 50: 77224ms ~ 77800ms (file: 213-120-761-672-123-761-126-270-236-675-756-765-316 175-176 167_W.mp3)
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
> **Time**: 2026-04-01 05:45:51
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "안녕하세요, 만나서 반가워요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "안녕하세요,", "start_ms": 110, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "만나서", "start_ms": 310, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "반가워요.", "start_ms": 440, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "Hello, nice to meet you too.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 95,
"audio_type": "speech",
"words": [
{"word": "Hello,", "start_ms": 95, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "nice", "start_ms": 250, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 340, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "meet", "start_ms": 390, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 490, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "too.", "start_ms": 550, "dur_ms": 10, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "성함이 어떻게 되세요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "성함이", "start_ms": 120, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 280, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "되세요?", "start_ms": 420, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "What is your name?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "What", "start_ms": 100, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 200, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "your", "start_ms": 260, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "name?", "start_ms": 340, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "제 이름은 김민수입니다.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "제", "start_ms": 130, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "이름은", "start_ms": 200, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "김민수입니다.", "start_ms": 330, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "My name is Minsu Kim.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 105,
"audio_type": "speech",
"words": [
{"word": "My", "start_ms": 105, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "name", "start_ms": 195, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 315, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "Minsu", "start_ms": 375, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "Kim.", "start_ms": 505, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 8,
"transcript": "직업이 무엇인가요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "직업이", "start_ms": 140, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "무엇인가요?", "start_ms": 320, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "What is your job?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 115,
"audio_type": "speech",
"words": [
{"word": "What", "start_ms": 115, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 225, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "your", "start_ms": 285, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "job?", "start_ms": 365, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "저는 학생입니다.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 150, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "학생입니다.", "start_ms": 270, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "I am a student.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 120, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "am", "start_ms": 200, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 290, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "student.", "start_ms": 330, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "어디에 사세요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 135,
"audio_type": "speech",
"words": [
{"word": "어디에", "start_ms": 135, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "사세요?", "start_ms": 285, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "Where do you live?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "Where", "start_ms": 110, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "do", "start_ms": 240, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 320, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "live?", "start_ms": 390, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "서울에 살고 있어요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 145,
"audio_type": "speech",
"words": [
{"word": "서울에", "start_ms": 145, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "살고", "start_ms": 305, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "있어요.", "start_ms": 425, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "I live in Seoul.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 120, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "live", "start_ms": 210, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 320, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "Seoul.", "start_ms": 390, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "취미가 무엇인가요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "취미가", "start_ms": 140, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "무엇인가요?", "start_ms": 320, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "What is your hobby?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 115,
"audio_type": "speech",
"words": [
{"word": "What", "start_ms": 115, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 225, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "your", "start_ms": 285, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "hobby?", "start_ms": 365, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "제 취미는 요리입니다.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "제", "start_ms": 130, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "취미는", "start_ms": 200, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "요리입니다.", "start_ms": 340, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "My hobby is cooking.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 105,
"audio_type": "speech",
"words": [
{"word": "My", "start_ms": 105, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "hobby", "start_ms": 195, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 315, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "cooking.", "start_ms": 375, "dur_ms": 170, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "어떤 음식을 좋아하세요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 125,
"audio_type": "speech",
"words": [
{"word": "어떤", "start_ms": 125, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "음식을", "start_ms": 235, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "좋아하세요?", "start_ms": 375, "dur_ms": 170, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "What kind of food do you like?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "What", "start_ms": 100, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "kind", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "of", "start_ms": 310, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "food", "start_ms": 360, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "do", "start_ms": 450, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 520, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "like?", "start_ms": 570, "dur_ms": 6, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "저는 불고기를 좋아합니다.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 140, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "불고기를", "start_ms": 260, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "좋아합니다.", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "I like Bulgogi.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 110, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "like", "start_ms": 200, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "Bulgogi.", "start_ms": 330, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "함께 점심 먹으러 가실래요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "함께", "start_ms": 130, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "점심", "start_ms": 260, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "먹으러", "start_ms": 380, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "가실래요?", "start_ms": 520, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "Would you like to have lunch together?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 105,
"audio_type": "speech",
"words": [
{"word": "Would", "start_ms": 105, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 205, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "like", "start_ms": 275, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 365, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "have", "start_ms": 415, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "lunch", "start_ms": 485, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "together?", "start_ms": 575, "dur_ms": 1, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "네, 좋아요. 어디로 갈까요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 150, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "좋아요.", "start_ms": 220, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "어디로", "start_ms": 330, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "갈까요?", "start_ms": 450, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "Yes, sounds good. Where should we go?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "Yes,", "start_ms": 120, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "sounds", "start_ms": 200, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "good.", "start_ms": 300, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "Where", "start_ms": 380, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "should", "start_ms": 500, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "we", "start_ms": 570, "dur_ms": 6, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "학교 앞 식당이 어때요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 145,
"audio_type": "speech",
"words": [
{"word": "학교", "start_ms": 145, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "앞", "start_ms": 255, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "식당이", "start_ms": 315, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "어때요?", "start_ms": 445, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "How about the restaurant in front of the school?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "How", "start_ms": 110, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "about", "start_ms": 190, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 280, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "restaurant", "start_ms": 330, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 460, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "front", "start_ms": 510, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "of", "start_ms": 570, "dur_ms": 6, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "거기 음식 맛있어요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "거기", "start_ms": 130, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "음식", "start_ms": 240, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "맛있어요.", "start_ms": 360, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "The food there is delicious.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 105,
"audio_type": "speech",
"words": [
{"word": "The", "start_ms": 105, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "food", "start_ms": 175, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "there", "start_ms": 275, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 365, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "delicious.", "start_ms": 425, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "네, 갑시다!",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 160,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 160, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "갑시다!", "start_ms": 270, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "Yes, let's go!",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "Yes,", "start_ms": 120, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "let's", "start_ms": 240, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "go!", "start_ms": 380, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "식사 후에 카페에 가실래요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "식사", "start_ms": 140, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "후에", "start_ms": 260, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "카페에", "start_ms": 360, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "가실래요?", "start_ms": 480, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "Would you like to go to a cafe after lunch?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "Would", "start_ms": 110, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 200, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "like", "start_ms": 270, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 360, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "go", "start_ms": 410, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 470, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 510, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "cafe", "start_ms": 550, "dur_ms": 20, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "좋은 생각이에요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "생각이에요.", "start_ms": 280, "dur_ms": 290, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "That's a good idea.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 115,
"audio_type": "speech",
"words": [
{"word": "That's", "start_ms": 115, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 215, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "good", "start_ms": 265, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "idea.", "start_ms": 385, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "커피 마시면서 이야기해요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "커피", "start_ms": 130, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "마시면서", "start_ms": 250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이야기해요.", "start_ms": 410, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "Let's talk over coffee.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "Let's", "start_ms": 100, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "talk", "start_ms": 220, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "over", "start_ms": 330, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "coffee.", "start_ms": 450, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "알겠어요, 그럼 이따 봐요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "알겠어요,", "start_ms": 140, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "그럼", "start_ms": 300, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이따", "start_ms": 410, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "봐요.", "start_ms": 510, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "Okay, see you later.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "Okay,", "start_ms": 110, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "see", "start_ms": 210, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 310, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "later.", "start_ms": 390, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "내일 날씨가 어떨까요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 135,
"audio_type": "speech",
"words": [
{"word": "내일", "start_ms": 135, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 245, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "어떨까요?", "start_ms": 365, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "How will the weather be tomorrow?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 105,
"audio_type": "speech",
"words": [
{"word": "How", "start_ms": 105, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "will", "start_ms": 195, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 265, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "weather", "start_ms": 315, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "be", "start_ms": 435, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "tomorrow?", "start_ms": 485, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "내일은 비가 올 것 같아요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 145,
"audio_type": "speech",
"words": [
{"word": "내일은", "start_ms": 145, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "비가", "start_ms": 295, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "올", "start_ms": 395, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 455, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "같아요.", "start_ms": 505, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "It looks like it will rain tomorrow.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 115,
"audio_type": "speech",
"words": [
{"word": "It", "start_ms": 115, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "looks", "start_ms": 175, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "like", "start_ms": 275, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "it", "start_ms": 345, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "will", "start_ms": 405, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "rain", "start_ms": 475, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "tomorrow.", "start_ms": 575, "dur_ms": 1, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "우산을 챙겨야겠네요.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "우산을", "start_ms": 140, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "챙겨야겠네요.", "start_ms": 280, "dur_ms": 290, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "I should bring an umbrella.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 110, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "should", "start_ms": 190, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "bring", "start_ms": 310, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "an", "start_ms": 420, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "umbrella.", "start_ms": 480, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "네, 잊지 마세요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 150, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "잊지", "start_ms": 260, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "마세요.", "start_ms": 380, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "Yes, don't forget.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "Yes,", "start_ms": 120, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "don't", "start_ms": 230, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "forget.", "start_ms": 350, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "그럼 내일 봬요!",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 140,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 140, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "내일", "start_ms": 250, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "봬요!", "start_ms": 370, "dur_ms": 200, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-01 05:45:51*
