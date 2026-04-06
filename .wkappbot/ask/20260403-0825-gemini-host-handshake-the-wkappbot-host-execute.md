# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=08:24:07  uptime=25492s  mem=1726MB  system_processes=448"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_082357.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 376-371-517-547-571-501-571-617-160-132-106-162-123-126-612-601-163-631-621 103-613 413 657-765-132-623-120-263-621-623-362-136-762-567-765-752-765-765-263-261-632-627-672-756-6_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 321-302-320-320-302-130-103-132-302-236-301-310-302-102-302-361-302-617-623-362-160-621-561-103-132-510-130-751-320-362-102-103-310-132-312-103-106-136-132-135-103-137-136-312-1_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 612-261-216-176-762-276-162-623-162-613-267-162-627-675-672-627-627-675-657-675-567-657-574-567-567-657-765-657-675-576 612-613-657-765-675-675-671-632_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 627-632 672 625-361-361-362-362-352-632-367-367-627-362-316-362-306 672-675_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 215-215-721-723-756-127-372-207-207-327-732-237-276-726-213-132-570-305-123-321-132-106-317-120-213-123-132-320-327-326-236-236-236-326-132-132-230-231-216-321-132-312-165-136-7_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 675-612-261-632-623-621-632-631-123-163-632-316-163-615-137-312-132-132-321-231-321-312-231-123-231-321-236-621-362-635-327-627-326-362-236-327-236-267-237-267-621-216-126-261-6_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 312-326-310-231-271-321-325-351-315-273-205-527-576-230-102-132-127-132-103-132-231-217-207-723-321-231-213-321-136-132-123-231-123-317-321-326-126-762-201-132-206-261-726-762-7_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 615-165-627-251-162-163-516-261-102-630-136-103-132-167-130-167-671-657-623-621-312-106-312-176 601-671-615-376-102-367-106-152-136-512-106-163-154-107-136-137-160-165-106-163-1_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 167-123-273-576-617-657-672-136-613-136-132-107-716-761-307-103-132-726-237-132-137-127-327-321-321-312-326-302-237-321-231-237-130-132-126-231-321-321-312-302-157-135-371-375-7_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 312-315-351-351-315-365-365-361-165-316-136-657-567-576-135-657-156-356-365-635-351-356-563-156-135-351-356-356-567-135-513-356-536-563-567-567-517-563-561-153-365-571-536-567-5_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 765-106-617-163-175-513-627-126-132-103-103-163-103-136-137-163-165-106-103-612-106-612-160-167-160-206-103-132-103-103-316-136-163-635-326-103-130-102-103-102-167-567-612-621-1_V.mp3)
  Segment 12: 17336ms ~ 19316ms (file: 567 216-162-627-627-136-132-103-103-672-261-206-126-361-310-153-263-201-312-132-123-103-132-102-126-136 267-625-123-310-320-670-123-310-103-132-312-312-123-620-621-265-657-657-1_V_stt_the_love_the.mp3)
  Segment 13: 20316ms ~ 20892ms (file: 730-753-753-753-672-672 657-672-765-763-736-267-657 167-162 620-136 136 576-576-576-576-576-657-637-675-675-675-567-637_W.mp3)
  Segment 14: 21892ms ~ 22468ms (file: 561-513-561-351-513-153-561-567-567-156-135-653-651-156-653-165-567-163-132-321-312-315-135-315-351-315-351-165-567-561-576-657-615-516-156-510-531-513-135-315-312-316-163-136-1_W.mp3)
  Segment 15: 23468ms ~ 24044ms (file: 621-162-612-310-301-310-756-753 756-763-765-763-760-760-765-765-765-756-751 132-312 756-735-576-756-753-763-103-130-160-610-316-361-751-736-730-301-760-726-765-715-137 316 756-1_W.mp3)
  Segment 16: 25044ms ~ 25620ms (file: 267-516-150-517-571-671-615-102-132-103-162-136-136-163-126-726-627-672-574-571-156-160-126-167-765-760-670-167-760-123-167-671-671-617-167-670-617-671-167-627-163-167-670-671_W.mp3)
  Segment 17: 26620ms ~ 27196ms (file: 574-576-571 130-106-106-103-312-230-236-230-260-203-602-260-705-706 621-367-631-216-163-162-136-126-123-214-217-213 136-103-123-132-213-263-261-627-627-260-260-206-267 216-236-3_V.mp3)
  Segment 18: 28196ms ~ 28772ms (file: 320-132-123-301-103-301-206-260-267-206-261-206-207-203-230-270-260-267-672-670-623-237-306-301-320-301-163-106-136-163-316-301-310-320-321-302-670-310-130-167-163-162-167-617-6_W.mp3)
  Segment 19: 29772ms ~ 30348ms (file: 165-261-260-675-123-137-136-136-675-160-136-613-130-132-136-107-216-657-756-162-126-610-126-102-312-312-106-132-136-103-103-312-756-362-671-167-103-103-132-613-163-136-102-726-6_V.mp3)
  Segment 20: 31348ms ~ 31924ms (file: 236-236-326 265-236-261-236-216-132-312-123-602-261 267 751-570-635-576-275-567-765-762-572-756-725-627-672-132-320-312-130-103-301-103-574-751-675-675-672-675-627-267-756-627-6_W.mp3)
  Segment 21: 32924ms ~ 33500ms (file: 261-621-123-136-136-103-167-103-176-675-167-106-137-136-617-126-267-276-162-613-163-167-561-256-162-127-127-612-612-610-612-261-620-672-627-136-623-617-612-620-631-163-103-163-1_V.mp3)
  Segment 22: 34500ms ~ 35076ms (file: 306-673-760-236-260-574-317-703-126 601-601-632-163-206-156-167-612-765-162-610-620-620-621-136-263-157-173-672-260-267-721-725-371-103-132 762-762 103-160-165-137-103-176-317-1_V.mp3)
  Segment 23: 36076ms ~ 36652ms (file: 617-361-675-136-126-623-326-261-160-625-256-623-627-263-267-263-263-263-175-756-103-326-136-106-617-103-126-106-135-637-167-107-167-176-106-103-103-106-106-567-165-625-756-167-1_V.mp3)
  Segment 24: 37652ms ~ 38228ms (file: 216-623-312-316-321-231-321-162-162-163-103-130-621-106-103-176-163-735-310-635-162-623-263-236-261-106-107-136-761-136-150-132-163-715-756-671-231-237-716-756-106-103-167-760-7_V.mp3)
  Segment 25: 39228ms ~ 39804ms (file: 206-607-726-576-762-260-206-267-206-263-103-106-106-103-302-230-203-207-136-756-765-705-367-103-106-216-123-321-130-267-627-623-762-672-672-213-130-103-201-602-627-263_W.mp3)
  Segment 26: 40804ms ~ 41380ms (file: 765-157-165-162-152-106-103-103-165-751-163-163-574-132-176-172-162 573-607-126-176-162-106-132-136-172-172-162-132-165-673-137-136-167-106-167-105-176-137-102-165-106-135-103-1_V.mp3)
  Segment 27: 42380ms ~ 42956ms (file: 263-651-106-310-632-362-623-102-160-106-106-751-107-130-310-301-163-362-361-261-236-213-312-301-103-301-103-136-675-673-760-631-317-312-312-135-107-106-103-312-376-670-753-736-1_V.mp3)
  Segment 28: 43956ms ~ 44532ms (file: 570-610-176-612-617-176-617-167-617-612-160-167-506-160-670-162-135-526-576-162 675-675-765-675-672-310-321-312-165-175 572-576-756-752-756-723-537-573-257-312 603_W.mp3)
  Segment 29: 45532ms ~ 46108ms (file: 126-765-612-627-162-654-126-156-157-675-157-132-132-137-754-106-163-613-132-751-627-627-165-302-302-302-321-312-376-756-735-763-632-306-312-316-132-124-103-261-312-712-756-132-7_V.mp3)
  Segment 30: 47108ms ~ 47684ms (file: 612-236-123-163-136-631-103-136-103-617-103-136-136-103-135-103-103-602-670-127-576-156-516-157_W.mp3)
  Segment 31: 48684ms ~ 49260ms (file: 103-132-102-132-103-136-657-627-627_V.mp3)
  Segment 32: 50260ms ~ 50836ms (file: 413-123-132 765-675-756-731-173-163-167-201-123_W.mp3)
  Segment 33: 51836ms ~ 52412ms (file: 750-750-750-751-716-517-756-765-107-135-137-135-136-160-657-567-765-617-107-571-672-106-103-203-321-672-203-216-206-261-136-621-762-756-267-627-675-160-167-715-312-137-126-132-3_V.mp3)
  Segment 34: 53412ms ~ 53988ms (file: 752-675-275-576-657-576-537-536-756-567-670-576-675-576-576-576-576-576-576-576-567-567-576-576-567-256-576-657-576-675-657-167-756-657-562-657-103-653-657-576-567-657-675-612-1_W.mp3)
  Segment 35: 54988ms ~ 55564ms (file: 130-360-756-765-132-130-167-231-201-216-312-132-213-176-172-315-136-132-213-206-267-267-574-570-301-306-206-620-263-627-263-261-501-156-156-156-671-607-607-106-632-326-302-310-1_V.mp3)
  Segment 36: 56564ms ~ 57140ms (file: 756-371 763-236-675-762-765-726-625-267-726-672-675-675-675-675-675-236-361-376-367-136-632-632-123-170-160-160-263-236-621-176-126-361-103-167-130-106-132-172-103-163-165-103-1_V.mp3)
  Segment 37: 58140ms ~ 58716ms (file: 675-726-576-352-621-574-574-756-750-756-167-102-103-105-103-126-321-132-310-301-132-102-130-261-620-136-130-126-602-263-167-123-102-132-123-213-312 143-312-635-632-672-621-637-6_V.mp3)
  Segment 38: 59716ms ~ 60292ms (file: 106-601-613-160-103-261-126-102-163-160-671-170-173-617-163-516-216-172-136-123-123-631-623-617-316-716-675-176-107-126-132-216-612-126-162 162-167-564-612-761-162-103-163-613-1_V.mp3)
  Segment 39: 61292ms ~ 61868ms (file: 103-106-106-610-670-763-754-765-706-706-267-572-750-175-206-261-126-132-163-106-615-607-601-106-670-607-602-206-265-527-567-106-310-210-302-130-631-106-106-107-103-130-310-672-7_V.mp3)
  Segment 40: 62868ms ~ 63444ms (file: 170-761-706-705-567-753-735 612-126-413-132-312-715-102-201-213-132-137-316-327-102-627-103-612-107-213-213-256-275-236-721-206-237-273-217-125-123-735-267-132-210-123-127-250-6_V.mp3)
  Segment 41: 64444ms ~ 65020ms (file: 610-106-103-106-167-160-123-651-123-516-316-165_V.mp3)
  Segment 42: 66020ms ~ 66596ms (file: 267-267-267-231-312-163-163-176-316-637-536-526-256-256-326-312-513-537-563-256-257-326-312-275-267-524-254-321-231-165-160-546-576-256-267-261-263-263-267-261-263-236-236-263-2_W.mp3)
  Segment 43: 67596ms ~ 68172ms (file: 326-326-526-576-572-567-536-576-532-625-672-261-576-576-567-576-576-756-152-156-567-567-576-567-567-567-125-125-123-163-123-312-312-123-135-152-513-513-215-325-213-312-312-216-1_W.mp3)
  Segment 44: 69172ms ~ 69748ms (file: 507-507-507-506-570-574-576-756-107-156-157-716-165-567-501-501-103-123-132 627-326-126-130-673-671-756-754-751_W.mp3)
  Segment 45: 70748ms ~ 71324ms (file: 102-123-160-106-176-162-165-160-163-103-675-265-173-613-103-162-176-136-136-736-130-576-623-126-162-106-167-160-165-136-106-162-106-103-610-175-103-176-126-216-623-612 621-631-6_V.mp3)
  Segment 46: 72324ms ~ 72900ms (file: 672-672-265-263-210-123-102-137-127-167-126-130-163-162-613-167-102-167-316-136-103-103-631-167-176-162-312-630-621-612-126-163-612-132-631-163-613-163-316-310-632-126-630-106-6_V.mp3)
  Segment 47: 73900ms ~ 74476ms (file: 612-163-261-302-312-301 261-236-567-762 136-671-326-127-613-631-765-765-657-675-675-672-361-765-675-167-657-621-621-567-657-675-576-576-576-576-572-576-675-567-576-756-567-765-7_W.mp3)
  Segment 48: 75476ms ~ 76052ms (file: 132-106-263-106-106-165-126-312-103-103-132-103-102-107-102-102-123-102-615-672-621-230-620-630-312-602-102-160-612-163-103-657-236-215-102-102-106-162-167-167-126-103-617-165-1_V.mp3)
  Segment 49: 77052ms ~ 77628ms (file: 507-574-750-762-267-627-632-310-130-150-510-571-570-570-507-607-620-206-621-631-136-613-136-306-362-632-507-507-507-103-103-103-301-310-567-567-657-216-126-526-567-657-627-612-1_V.mp3)
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
> **Time**: 2026-04-03 08:25:07
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "오늘 배운 문법을 대화에서 활용해 봅시다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "배운", "start_ms": 160, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "문법을", "start_ms": 290, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "대화에서", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "활용해", "start_ms": 580, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "봅시다.", "start_ms": 710, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "저는 한국 영화 보는 것을 정말 좋아해요.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 30, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "한국", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "영화", "start_ms": 280, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "보는", "start_ms": 410, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "것을", "start_ms": 520, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "정말", "start_ms": 630, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "좋아해요.", "start_ms": 750, "dur_ms": 190, "phase_lr_deg": 5.0}
]
},
{
"segment": 3,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 4,
"transcript": "어떤 장르의 영화를 가장 선호하시나요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "어떤", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "장르의", "start_ms": 170, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "영화를", "start_ms": 300, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 440, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "선호하시나요?", "start_ms": 560, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 6,
"transcript": "저는 스릴러나 액션 영화를 자주 봐요.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "스릴러나", "start_ms": 150, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "액션", "start_ms": 300, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "영화를", "start_ms": 420, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "자주", "start_ms": 550, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "봐요.", "start_ms": 670, "dur_ms": 120, "phase_lr_deg": 5.0}
]
},
{
"segment": 7,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 8,
"transcript": "긴장감 넘치는 전개가 정말 매력적이죠.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "긴장감", "start_ms": 60, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "넘치는", "start_ms": 200, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "전개가", "start_ms": 330, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 450, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "매력적이죠.", "start_ms": 570, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "한국 영화는 연기력이 아주 훌륭해서 몰입이 잘 돼요.",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "한국", "start_ms": 35, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "영화는", "start_ms": 155, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "연기력이", "start_ms": 295, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "아주", "start_ms": 445, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "훌륭해서", "start_ms": 555, "dur__ms": 150, "phase_lr_deg": 5.0},
{"word": "몰입이", "start_ms": 715, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "잘", "start_ms": 845, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "돼요.", "start_ms": 915, "dur_ms": 110, "phase_lr_deg": 5.0}
]
},
{
"segment": 10,
"transcript": "맞아요 배우들의 감정 표현이 정말 풍부하죠.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "맞아요", "start_ms": 45, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "배우들의", "start_ms": 175, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "감정", "start_ms": 325, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "표현이", "start_ms": 445, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 585, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "풍부하죠.", "start_ms": 695, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "나중에 유명한 한국 영화들을 같이 분석해 봐요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "나중에", "start_ms": 30, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "유명한", "start_ms": 170, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 310, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "영화들을", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 580, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "분석해", "start_ms": 690, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "봐요.", "start_ms": 830, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "네 선생님, 정말 기대됩니다. 오늘도 감사해요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 60, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "선생님,", "start_ms": 130, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "정말", "start_ms": 280, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "기대됩니다.", "start_ms": 400, "dur_ms": 210, "phase_lr_deg": 5.0},
{"word": "오늘도", "start_ms": 620, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "감사해요.", "start_ms": 760, "dur_ms": 180, "phase_lr_deg": 5.0}
]
},
{
"segment": 13,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 14,
"transcript": "수업 태도가 너무 좋아서 저도 즐거웠습니다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "수업", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "태도가", "start_ms": 160, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 300, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "좋아서", "start_ms": 400, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "저도", "start_ms": 530, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "즐거웠습니다.", "start_ms": 650, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 16,
"transcript": "오늘 수업은 여기서 모두 마칠게요. 수고하셨어요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 25, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "수업은", "start_ms": 145, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "여기서", "start_ms": 275, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "모두", "start_ms": 405, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "마칠게요.", "start_ms": 525, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "수고하셨어요.", "start_ms": 685, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "안녕히 가시고 다음에 또 만나요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "가시고", "start_ms": 200, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "다음에", "start_ms": 340, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 460, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "만나요.", "start_ms": 530, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "네 안녕히 가세요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 60, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "안녕히", "start_ms": 130, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "가세요.", "start_ms": 290, "dur_ms": 190, "phase_lr_deg": 5.0}
]
},
{
"segment": 19,
"transcript": "바이 바이.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "바이", "start_ms": 35, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "바이.", "start_ms": 205, "dur_ms": 371, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 21,
"transcript": "진짜 안녕.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 45, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "안녕.", "start_ms": 185, "dur_ms": 391, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 23,
"transcript": "다음에 봐요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 30, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "봐요.", "start_ms": 200, "dur_ms": 376, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "행복하세요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "행복하세요.", "start_ms": 50, "dur_ms": 526, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 26,
"transcript": "바이.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "바이.", "start_ms": 60, "dur_ms": 516, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "진짜 진짜 안녕.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 190, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "안녕.", "start_ms": 340, "dur_ms": 236, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 29,
"transcript": "수고.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "수고.", "start_ms": 25, "dur_ms": 551, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "고생.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "고생.", "start_ms": 55, "dur_ms": 521, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 32,
"transcript": "안녕히.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "안녕히.", "start_ms": 35, "dur_ms": 541, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 34,
"transcript": "잘 가.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "잘", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "가.", "start_ms": 165, "dur_ms": 411, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "끝.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "끝.", "start_ms": 30, "dur_ms": 546, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 37,
"transcript": "진짜 끝.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "끝.", "start_ms": 200, "dur_ms": 376, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "바이.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "바이.", "start_ms": 40, "dur_ms": 536, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "안녕.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "안녕.", "start_ms": 60, "dur_ms": 516, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 41,
"transcript": "수고.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "수고.", "start_ms": 35, "dur_ms": 541, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "고생.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "고생.", "start_ms": 45, "dur_ms": 531, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 44,
"transcript": "바이 바이.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "바이", "start_ms": 25, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "바이.", "start_ms": 195, "dur_ms": 351, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 46,
"transcript": "안녕.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "안녕.", "start_ms": 55, "dur_ms": 521, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 48,
"transcript": "진짜 끝.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "끝.", "start_ms": 190, "dur_ms": 386, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 50,
"transcript": "바이 바이.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "바이", "start_ms": 60, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "바이.", "start_ms": 210, "dur_ms": 366, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 08:25:07*
