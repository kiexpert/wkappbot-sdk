# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=05:52:08  uptime=16373s  mem=1565MB  system_processes=446"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_055158.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 561-651-615-650-165-165-175-215-216-263-261-213-123-213-206-201-526-615-635-265-652-652-607-613-163-160-105-160-162-167-167-167-167-165-167-615-612-651-652-567-561-652-652-562-2_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 312-103-162-163-102-715-751-132-312-136-712-620-602-126-613-103-105-564-623-163-167-106-103-152-162-637-103-135-130-651-106-675-516-765-613-267-672-670-675 621-672-627-627-627-2_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 302-316-316-326-261-216-236-260-206-261-132-132-312-201-206-230-576-657-561-632-567-563-562-571-761-617-106-623-310-132-630-630-620-162-617-621-612-671-607-627-621-627-627-361-3_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 762-762-612-136 263-236-623-261_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 672-670-760-762-760 671-672-672-612-627-617-167-163-651-261-617-617-675-167-716 216-623 675-263 672-731-173-103-102-160-162-167-163-167-167-617-136-361 176-612-672-617-617-316-7_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 617-136-160-103-136-167-152-127-216-103-130-103-612-106-603-160-126-316-162-632-672-167-163-623-612-136-102-103-136-132-130-136-317-162-130-103-215-617-132-162-176-152-136-130-1_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 103-621-712-160-132-130-163-103-126-231-172-127-621_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 176-176 162-310 542-562-562-561-150-132-236-260-260-206-206-162-162-132-132-601-612-546-567-547 316-175-610-176-716-715-176-713-316-317-310-367-360-367-607-307-360-136-301-306-3_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 765-672-621-132-316-132-316-123-136-132-213 735-716-176-103-136-615-610-160-163-136-176-163-126-306-301-103-326-126-261-267-547-571-167-320-765-765-762-762-671-675-672-756-753-1_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 760-706-765-167-672-670-607-765-713-756-706-706-761-413-103-765-765-765-125-163-361 617 761-763-567-756_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 130-617-126-163-621-162-617-107-163-136-162-162-612-261-126-136-162-154-153-157-170-637-130-103-130-316-132-301-103-312-136-312-126-157-167-261-163-213-126-216-123-301-312-316-6_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 143-126-132-312-132-103-316-135-172-162-137-132-164_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 162 165-106-157-126-516-615-215-523-532-623-621-236-316-312-203-236-726-276-162-216-237-671-120-362-623-326-231-132-231-621-216-321-263-261-631-627-627-362-612-132-163-132-612-2_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 103-156-620-316-675-675-617-627-612-361-103-136-136-612-631-136-102-167-657-165-136-132-106-165-612-216-163-106-165-136-574-156-621-106-760-103-627-632-136-632-165-167-103-167-6_V.mp3)
  Segment 15: 22064ms ~ 24656ms (file: 136-657-617-163-615-167-136-136-136-560-132-120-162-136-621-621-632-132-103-102-135-136-136-613-130-106-103-103-163-106-103-165-120-102-162-163-103-617-602-162-160-163-162-126-1_V.mp3)
  Segment 16: 25656ms ~ 26232ms (file: 253-527-752-312-562-625-625-267-326-327-312 152-357-736-576-762-317-326-265-625-265-652-625-625-165-265-562-216-126-123-152-156-165-527-523-560-237-276-162-127-256-261-310-126-1_W.mp3)
  Segment 17: 27232ms ~ 27808ms (file: 712-627-721-672-126-623-210-623-316-672-672-761-672-761-172-617-106-162-136-103-261-261-163_W.mp3)
  Segment 18: 28808ms ~ 29384ms (file: 627-251-256-561-516-312-610-261-126 672-672 103-160-571-167-160-103-103-312-136-103-167-106-163-621-103-163-176-152-267-165-657-567-657-163-576-576-576-576_W.mp3)
  Segment 19: 30384ms ~ 30960ms (file: 672-362-261-267-123-213-216-213-321-326-312-657-675-675-267-263-231-327-235-253-231-675-627-265-267-263-326-231-236-236-265-236-625-362-321-216-216-365-536-215-231-321-231-213-5_W.mp3)
  Segment 20: 31960ms ~ 32536ms (file: 134 132-103 673-762-762-675-673 637-613-632_W.mp3)
  Segment 21: 33536ms ~ 34112ms (file: 176-123-163-103-127-631-765-765-765-765-765-675-765-762-765-675-375-317-263-362-631-321-103 602-260-230-675-756-576-567-567-754-756-632-321-507-375-650-670-601-761-670-162-612-1_W.mp3)
  Segment 22: 35112ms ~ 35688ms (file: 765-765-260-206-623-236-675-625-307-372-325-675-621-623-613-650-756-716-302-301-103-136-103-103-106-132-162-602-607-601-567-625-623-756-760-371-107-162-167-312-137-103-137-176-1_V.mp3)
  Segment 23: 36688ms ~ 37264ms (file: 756-736-701-617-157-725-607-307-372-673-103-306-203-305-576-576-235-276-302-750-570-126-123-127-132-267-201-312-270-762-207-201-170-713-137-312-172-132-102-175-137-576-751-230-3_W.mp3)
  Segment 24: 38264ms ~ 38840ms (file: 302-107-312-130-137-703-763-706-763-726-723-137-510-532-236-203-605-236-321-130-132-617-672-627-167-612-621-162-216-103-102-123-201-206-213-216-127-362-162-132-301-326-306-230-2_W.mp3)
  Segment 25: 39840ms ~ 40416ms (file: 132-126-321-132-375-567-713-341-132-175-754-213-326-613-261-213-341-162-132-631-263-213-241-143-213-234-132-312-627-136 764-413-526-132-321-135-325-136-175-175-715-751-537-357-6_V.mp3)
  Segment 26: 41416ms ~ 41992ms (file: 670-130-175-170-361-162-617-103-107-152-103-675-152-751-170-521-123_V.mp3)
  Segment 27: 42992ms ~ 43568ms (file: 547-547-547-542-652-576-625-627-750-206-236-203-203-576-765-670-607-672-602-627-260-576-527-205-256-312-570-573-315-765-607-672-627-267-570-572-514-352-136-106-163-124-705-720-2_W.mp3)
  Segment 28: 44568ms ~ 45144ms (file: 260-210-213 567-576-567-576-567-567-567-576-567-576-756-756-576-567-567-561-651-576-576-756-576-576-576-576-756-576-567-576-657-567-657-657-567-657-576-657-657-567-576-657-576-5_W.mp3)
  Segment 29: 46144ms ~ 46720ms (file: 756-765-675-136-103-301-136-132-761-715-732-765-327-327-327-327-326-367-163-137-123-130-130-627-603-136-316-123-123-321-312-312-367-370 317-764-123_W.mp3)
  Segment 30: 47720ms ~ 48296ms (file: 620-306-627-306-326-132-376-723-273-613-362-261-672-162-263-201-321-532-231-230-567-263-236-260-120-106-301-302-302-306-326-362-326-320-320-523-532-527-573-105-162-162-162-263-2_W.mp3)
  Segment 31: 49296ms ~ 49872ms (file: 756-756 765-617-726-762-732-136-316-301-765-765-765-765-765-672-765-627-650-265-326-623-123-362-653-267-267-267-261-612-216-675-160-576-756-735-361-316-621-261-612-163-673-763-6_W.mp3)
  Segment 32: 50872ms ~ 51448ms (file: 152-152-352-356-275-532-571-576-562-627-625-627-627-362-316-351-357-357-267-267-267-267-265-312-516-236-536-351-532-231-251-362-154-157-153-153-156-135-156-132-136-316-312-132-3_W.mp3)
  Segment 33: 52448ms ~ 53024ms (file: 103-132 163-163-103-163-167-130-167-163-261-631-650-657-567-576 675-576-762-627 132_W.mp3)
  Segment 34: 54024ms ~ 54600ms (file: 267-627-612-623-312-162-132-136-102-126-576-752-765-267-267 672-670-672 765-567-136-706-107-753-321-715-376-761-106-312-132-132-316-210-126-216-670-657-617-123-103-216-316-213-3_W.mp3)
  Segment 35: 55600ms ~ 56176ms (file: 236-312-103-102-163-162-632-163-123-203-106-316-361-310-120-216-160-123-361-236-210-162-162-103-135-312-150-130-301-312-310-326-163-132-103-160-675-167-216-261-120-213-103-123-6_V.mp3)
  Segment 36: 57176ms ~ 57752ms (file: 675-671-137-103-136-132-154-132-126-750-705-752-172-167-162-164-134-132-134-301-327-375-136-137-173-236-316-321-325-256-562-726-132-612-176-106-601-103-236-632-673-675-675-673-7_V.mp3)
  Segment 37: 58752ms ~ 59328ms (file: 167-601-602-306-310-316-103-136-103-716-106-136-316-136-362-123-132-103-136-107-107-103-132-605-176-167-167-167-167-167-615-167-106-162-312-316-103-302-136-316-613-675-675-671-6_V.mp3)
  Segment 38: 60328ms ~ 61768ms (file: 205-132-106-126-102-170-706-160-106-160-761-136-567-621-120-102-103-103-106-103-136-106-617-106-236-126 132-137-176-706-163-103-213-312-560-756-765-670-316-310-631-360-130-162-7_V.mp3)
  Segment 39: 62768ms ~ 63344ms (file: 672-670-762-672-762-762-762-672-672-672_W.mp3)
  Segment 40: 64344ms ~ 64920ms (file: 671-652-321-237-230-623-237-756-765-756-263_W.mp3)
  Segment 41: 65920ms ~ 66496ms (file: 162-623-163-162-103-567-627-631-612-130-756-756-574-570-657-567-567-162-136-651-561-213-536-756-576-103-130-312-130-312-321-756-750-537-316-672-672-306-602-327-365-631-103-103-3_W.mp3)
  Segment 42: 67496ms ~ 68072ms (file: 301-312-103-310-105-103-103-352-306-376-320-617-673-631-617-376-165-175-716-617-132-326-135-137-163-126-125-725-753-312-176-167-267-753-167-172-271-267-751-617-267-263-176-167-6_W.mp3)
  Segment 43: 69072ms ~ 69648ms (file: 312-301-136-106-705-157-507-572-705-762-756-603-362-302 163-103-137-301-137-715-753-574-537-562-570-627-102-312-367-716-375-657-657-567-136-361-136-675-675-167 765-765-756-763-3_V.mp3)
  Segment 44: 70648ms ~ 71224ms (file: 260-123-312-326-231-263-261-326-326-103-103-136-136-326-302-350-315-574-576-623-320-301-623-267-267-267-602-263-507-301-301-301-354-103-137-173-103-103-105-570-627-620-260-621-6_V.mp3)
  Segment 45: 72224ms ~ 72800ms (file: 576-756-753-751-735-167-675-301-320-301-375-301-172-123-312-321-672-102-175-105-310-563-705-123-162-617-231-203-312-213-236-371-321-735-375-305-307-301-310-301-312-213-123-620-2_W.mp3)
  Segment 46: 73800ms ~ 74376ms (file: 137-674-674-617-310-312-231-103-310-312-301-326-123-521-265-312-164-576-107-103-301-132-301-103-316-135-326-607-607-602-350-753-501-754-571-162-623-267-627-261-102-213-103-621-6_V.mp3)
  Segment 47: 75376ms ~ 75952ms (file: 176-123-123-312-312-136-305-310-136-310-302-310-301-310-316-132-312-316-132-301-301-263-167-672-672-627-206-267-201-321-310-320-231-231-203-206-120-201-132-625-360-136-163-215-1_W.mp3)
  Segment 48: 76952ms ~ 77528ms (file: 361-263-123-215-132-135-365-263-623-362-326-315-351-315-315-351 326-362-326-375-367-375-357-357-375-372-237 361-321-135-315-352-652-236-263-627-635-623 357-375-357-574-574-574-5_W.mp3)
  Segment 49: 78528ms ~ 79104ms (file: 165-312 721-276-275-742-461-637-167-265-263-623-263-263-263-671-562-653-236-230-213_W.mp3)
  Segment 50: 80104ms ~ 80680ms (file: 130-321 617-765-576-173-716-167 657-216-132-163-613-103-671-167-516-165-672-163-671-617-263-206-206-316-106-163-261-612-632-167-706-761-617-632-765-601-167-736-763-316-670-617-6_W.mp3)
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
> **Time**: 2026-04-03 05:53:18
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "안녕하세요 처음 뵙겠습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 45, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "처음", "start_ms": 285, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "뵙겠습니다", "start_ms": 415, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "제 이름은 김민수입니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "제", "start_ms": 50, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "이름은", "start_ms": 120, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "김민수입니다", "start_ms": 280, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "만나서 정말 반가워요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "만나서", "start_ms": 60, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "정말", "start_ms": 210, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "반가워요", "start_ms": 330, "dur_ms": 200, "phase_lr_deg": 5.0}
]
},
{
"segment": 4,
"transcript": "감사합니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 100, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "Nice to meet you too",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Nice", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": -2.0},
{"word": "to", "start_ms": 130, "dur_ms": 50, "phase_lr_deg": -2.0},
{"word": "meet", "start_ms": 190, "dur_ms": 90, "phase_lr_deg": -2.0},
{"word": "you", "start_ms": 290, "dur_ms": 70, "phase_lr_deg": -2.0},
{"word": "too", "start_ms": 370, "dur_ms": 150, "phase_lr_deg": -2.0}
]
},
{
"segment": 6,
"transcript": "어디에 사시나요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "어디에", "start_ms": 55, "dur_ms": 180, "phase_lr_deg": 5.0},
{"word": "사시나요", "start_ms": 245, "dur_ms": 200, "phase_lr_deg": 5.0}
]
},
{
"segment": 7,
"transcript": "저는 서울에 살고 있어요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 45, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "서울에", "start_ms": 175, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "살고", "start_ms": 325, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 435, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "취미가 무엇인가요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "취미가", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 5.0},
{"word": "무엇인가요", "start_ms": 240, "dur_ms": 220, "phase_lr_deg": 5.0}
]
},
{
"segment": 9,
"transcript": "제 취미는 영화 감상이에요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "제", "start_ms": 60, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "취미는", "start_ms": 130, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "영화", "start_ms": 270, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "감상이에요", "start_ms": 380, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "(noise)",
"speaker": "null",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 11,
"transcript": "어떤 영화를 좋아하세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "어떤", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "영화를", "start_ms": 170, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "좋아하세요", "start_ms": 320, "dur_ms": 180, "phase_lr_deg": 5.0}
]
},
{
"segment": 12,
"transcript": "공포 영화를 좋아해요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "공포", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "영화를", "start_ms": 160, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "좋아해요", "start_ms": 290, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "액션 영화도 즐겨 봐요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "액션", "start_ms": 45, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "영화도", "start_ms": 175, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "즐겨", "start_ms": 325, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "봐요", "start_ms": 415, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "I love action movies too",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 30, "dur_ms": 40, "phase_lr_deg": -2.0},
{"word": "love", "start_ms": 80, "dur_ms": 90, "phase_lr_deg": -2.0},
{"word": "action", "start_ms": 180, "dur_ms": 120, "phase_lr_deg": -2.0},
{"word": "movies", "start_ms": 310, "dur_ms": 100, "phase_lr_deg": -2.0},
{"word": "too", "start_ms": 420, "dur_ms": 100, "phase_lr_deg": -2.0}
]
},
{
"segment": 15,
"transcript": "최근에 본 영화가 있나요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "최근에", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "본", "start_ms": 200, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "영화를", "start_ms": 270, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "있나요", "start_ms": 410, "dur_ms": 130, "phase_lr_deg": 5.0}
]
},
{
"segment": 16,
"transcript": "네 어제 극장에 다녀왔어요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 60, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "어제", "start_ms": 130, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "극장에", "start_ms": 250, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "다녀왔어요", "start_ms": 400, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "정말 재미있었습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 70, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "재미있었습니다", "start_ms": 200, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "가족들과 함께 보셨나요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "가족들과", "start_ms": 45, "dur_ms": 160, "phase_lr_deg": 5.0},
{"word": "함께", "start_ms": 215, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "보셨나요", "start_ms": 325, "dur_ms": 180, "phase_lr_deg": 5.0}
]
},
{
"segment": 19,
"transcript": "아니요 친구랑 같이 봤어요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "아니요", "start_ms": 55, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "친구랑", "start_ms": 185, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 335, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "봤어요", "start_ms": 435, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "(music)",
"speaker": "null",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 21,
"transcript": "주말에는 보통 뭐 하세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "주말에는", "start_ms": 40, "dur_ms": 160, "phase_lr_deg": 5.0},
{"word": "보통", "start_ms": 210, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "뭐", "start_ms": 330, "dur_ms": 50, "phase_lr_deg": 5.0},
{"word": "하세요", "start_ms": 390, "dur_ms": 140, "phase_lr_deg": 5.0}
]
},
{
"segment": 22,
"transcript": "저는 운동하는 것을 좋아해요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "운동하는", "start_ms": 160, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "것을", "start_ms": 330, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "좋아해요", "start_ms": 420, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "축구나 야구를 즐겨 합니다",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "축구나", "start_ms": 60, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "야구를", "start_ms": 210, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "즐겨", "start_ms": 340, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "합니다", "start_ms": 450, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "I also like playing soccer",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 35, "dur_ms": 40, "phase_lr_deg": -2.0},
{"word": "also", "start_ms": 85, "dur_ms": 80, "phase_lr_deg": -2.0},
{"word": "like", "start_ms": 175, "dur_ms": 70, "phase_lr_deg": -2.0},
{"word": "playing", "start_ms": 255, "dur_ms": 120, "phase_lr_deg": -2.0},
{"word": "soccer", "start_ms": 385, "dur_ms": 150, "phase_lr_deg": -2.0}
]
},
{
"segment": 25,
"transcript": "다음에 같이 운동해요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 55, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "같이", "start_ms": 205, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "운동해요", "start_ms": 315, "dur_ms": 200, "phase_lr_deg": 5.0}
]
},
{
"segment": 26,
"transcript": "네 좋습니다 연락 주세요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 65, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "좋습니다", "start_ms": 135, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "연락", "start_ms": 285, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 395, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "오늘 날씨가 참 좋네요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "날씨가", "start_ms": 160, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "참", "start_ms": 310, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "좋네요", "start_ms": 380, "dur_ms": 150, "phase_lr_deg": 5.0}
]
},
{
"segment": 28,
"transcript": "The weather is beautiful today",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "The", "start_ms": 25, "dur_ms": 50, "phase_lr_deg": -2.0},
{"word": "weather", "start_ms": 85, "dur_ms": 100, "phase_lr_deg": -2.0},
{"word": "is", "start_ms": 195, "dur_ms": 50, "phase_lr_deg": -2.0},
{"word": "beautiful", "start_ms": 255, "dur_ms": 150, "phase_lr_deg": -2.0},
{"word": "today", "start_ms": 415, "dur_ms": 130, "phase_lr_deg": -2.0}
]
},
{
"segment": 29,
"transcript": "봄바람이 살랑살랑 불어요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "봄바람이", "start_ms": 60, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "살랑살랑", "start_ms": 230, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "불어요", "start_ms": 420, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "꽃구경 가고 싶네요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "꽃구경", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 5.0},
{"word": "가고", "start_ms": 240, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "싶네요", "start_ms": 350, "dur_ms": 160, "phase_lr_deg": 5.0}
]
},
{
"segment": 31,
"transcript": "벚꽃이 곧 필 것 같아요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "벚꽃이", "start_ms": 45, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "곧", "start_ms": 195, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "필", "start_ms": 265, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 325, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 375, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "Shall we go for a walk?",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Shall", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": -2.0},
{"word": "we", "start_ms": 140, "dur_ms": 60, "phase_lr_deg": -2.0},
{"word": "go", "start_ms": 210, "dur_ms": 70, "phase_lr_deg": -2.0},
{"word": "for", "start_ms": 290, "dur_ms": 50, "phase_lr_deg": -2.0},
{"word": "a", "start_ms": 350, "dur_ms": 30, "phase_lr_deg": -2.0},
{"word": "walk?", "start_ms": 390, "dur_ms": 150, "phase_lr_deg": -2.0}
]
},
{
"segment": 33,
"transcript": "네 근처 공원에 가요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 65, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "근처", "start_ms": 135, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "공원에", "start_ms": 245, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "가요", "start_ms": 395, "dur_ms": 140, "phase_lr_deg": 5.0}
]
},
{
"segment": 34,
"transcript": "산책하기 정말 좋은 곳이에요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "산책하기", "start_ms": 55, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 225, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 345, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "곳이에요", "start_ms": 455, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "매일 산책하시나요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "매일", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "산책하시나요", "start_ms": 190, "dur_ms": 300, "phase_lr_deg": 5.0}
]
},
{
"segment": 36,
"transcript": "시간 날 때마다 오려고 노력해요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "시간", "start_ms": 70, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "날", "start_ms": 180, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "때마다", "start_ms": 250, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "오려고", "start_ms": 390, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "노력해요", "start_ms": 500, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "건강에 아주 좋을 것 같네요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "건강에", "start_ms": 45, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "아주", "start_ms": 195, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "좋을", "start_ms": 285, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "것", "start_ms": 395, "dur_ms": 40, "phase_lr_deg": 5.0},
{"word": "같네요", "start_ms": 445, "dur_ms": 120, "phase_lr_deg": 5.0}
]
},
{
"segment": 38,
"transcript": "It keeps me healthy and active",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "It", "start_ms": 35, "dur_ms": 40, "phase_lr_deg": -2.0},
{"word": "keeps", "start_ms": 85, "dur_ms": 100, "phase_lr_deg": -2.0},
{"word": "me", "start_ms": 195, "dur_ms": 60, "phase_lr_deg": -2.0},
{"word": "healthy", "start_ms": 265, "dur_ms": 140, "phase_lr_deg": -2.0},
{"word": "and", "start_ms": 415, "dur_ms": 70, "phase_lr_deg": -2.0},
{"word": "active", "start_ms": 495, "dur_ms": 120, "phase_lr_deg": -2.0}
]
},
{
"segment": 39,
"transcript": "(noise)",
"speaker": "null",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 40,
"transcript": "잠시 쉬었다 갈까요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "잠시", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "쉬었다", "start_ms": 180, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "갈까요", "start_ms": 330, "dur_ms": 180, "phase_lr_deg": 5.0}
]
},
{
"segment": 41,
"transcript": "네 저기 벤치에 앉아요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 60, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "저기", "start_ms": 130, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "벤치에", "start_ms": 240, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "앉아요", "start_ms": 390, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "커피 한 잔 마시고 싶네요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "커피", "start_ms": 55, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "한", "start_ms": 185, "dur_ms": 50, "phase_lr_deg": 5.0},
{"word": "잔", "start_ms": 245, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "마시고", "start_ms": 315, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "싶네요", "start_ms": 455, "dur_ms": 110, "phase_lr_deg": 5.0}
]
},
{
"segment": 43,
"transcript": "제가 사 올게요 기다리세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "제가", "start_ms": 65, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "사", "start_ms": 185, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "올게요", "start_ms": 245, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "기다리세요", "start_ms": 375, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "정말 고마워요 잘 마실게요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "고마워요", "start_ms": 150, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "잘", "start_ms": 300, "dur_ms": 70, "phase_lr_deg": 5.0},
{"word": "마실게요", "start_ms": 380, "dur_ms": 160, "phase_lr_deg": 5.0}
]
},
{
"segment": 45,
"transcript": "Thank you so much for the coffee",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Thank", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": -2.0},
{"word": "you", "start_ms": 120, "dur_ms": 50, "phase_lr_deg": -2.0},
{"word": "so", "start_ms": 180, "dur_ms": 60, "phase_lr_deg": -2.0},
{"word": "much", "start_ms": 250, "dur_ms": 80, "phase_lr_deg": -2.0},
{"word": "for", "start_ms": 340, "dur_ms": 50, "phase_lr_deg": -2.0},
{"word": "the", "start_ms": 400, "dur_ms": 40, "phase_lr_deg": -2.0},
{"word": "coffee", "start_ms": 450, "dur_ms": 120, "phase_lr_deg": -2.0}
]
},
{
"segment": 46,
"transcript": "아니에요 즐거운 시간이었어요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "아니에요", "start_ms": 70, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "즐거운", "start_ms": 200, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "시간이었어요", "start_ms": 350, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "다음에 또 봬요 안녕히 가세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "또", "start_ms": 200, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "봬요", "start_ms": 270, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "안녕히", "start_ms": 380, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "가세요", "start_ms": 510, "dur_ms": 60, "phase_lr_deg": 5.0}
]
},
{
"segment": 48,
"transcript": "네 조심히 들어가세요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 60, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "조심히", "start_ms": 130, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "들어가세요", "start_ms": 280, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "See you later alligator",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": -2.0},
{"word": "you", "start_ms": 110, "dur_ms": 50, "phase_lr_deg": -2.0},
{"word": "later", "start_ms": 170, "dur_ms": 120, "phase_lr_deg": -2.0},
{"word": "alligator", "start_ms": 300, "dur_ms": 250, "phase_lr_deg": -2.0}
]
},
{
"segment": 50,
"transcript": "(noise)",
"speaker": "null",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 05:53:18*
